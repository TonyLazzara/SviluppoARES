Imports System.Diagnostics
Imports System.IO

Public Class ProcessLetturaSincrona
    Public Function EseguiExe(Eseguibile As String, Argomenti As String) As MemoryStream
        EseguiExe = Nothing

        'Argomenti.Replace("www.arestrasf.it", "10.0.88.80")

        HttpContext.Current.Trace.Write("EseguiExe " & Eseguibile & " - " & Argomenti)

        Dim myProcess As Process = Nothing
        Dim buffer(32768) As Byte
        Dim Memory As MemoryStream = Nothing

        Try

            Dim ProcessProperties As New ProcessStartInfo
            ProcessProperties.FileName = Eseguibile
            ProcessProperties.Arguments = Argomenti
            ProcessProperties.WindowStyle = ProcessWindowStyle.Hidden
            ProcessProperties.UseShellExecute = False
            ProcessProperties.CreateNoWindow = True


            ' imposto il valore per la redirezione dell'output
            ProcessProperties.RedirectStandardOutput = True

            myProcess = New Process()
            myProcess.StartInfo = ProcessProperties
            ' HttpContext.Current.Trace.Write("Prima di start")
            myProcess.Start()
            ' HttpContext.Current.Trace.Write("Dopo di start")
            myProcess.PriorityClass = ProcessPriorityClass.BelowNormal
            ' HttpContext.Current.Trace.Write("dopo priorità di start")

            Using ms As MemoryStream = New MemoryStream()
                Do While (True)
                    Dim read As Integer = myProcess.StandardOutput.BaseStream.Read(buffer, 0, buffer.Length)

                    If read <= 0 Then
                        Exit Do
                    End If
                    ms.Write(buffer, 0, read)
                Loop

                Memory = ms
            End Using
            HttpContext.Current.Trace.Write("fine corretta")

            myProcess.WaitForExit()

        Catch ex As Exception

            'funzioni_comuni.genUserMsgBox(Me, "error Esegui EXe ProcessLetturaSincrona: " & ex.Message)

            HttpContext.Current.Trace.Write("Errore: " & HttpContext.Current.Request.Url.ToString() & " --- " & ex.Message)

            funzioni_comuni_new.WriteLogError(ex.Message)



        Finally
            If Not myProcess Is Nothing Then
                myProcess.Close()
            End If
        End Try

        EseguiExe = Memory
    End Function
End Class
