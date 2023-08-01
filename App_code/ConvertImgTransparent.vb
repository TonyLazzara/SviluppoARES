Imports System.Data
Imports System.Drawing
Imports System.IO


Public Class ConvertImgTransparent


    Public Shared Function ConvertImage(ByVal pathTofileSrc As String, ByVal pathTofileDst As String) As Boolean
        Dim ris As Boolean
        ris = False

        Try
            Dim pf_src As String
            pf_src = pathTofileSrc '@"D:\Siti2\Sicilirentcar.it.ares\firme_contratti\pick_up\202101584.png";
            Dim ni As Byte() = GetTransparentArrayFromFileWithDelete(pf_src)
            Dim pf_dst As String
            pf_dst = pathTofileDst '@"D:\Siti2\Sicilirentcar.it.ares\firme_contratti\pick_up\202101584_trasp.png";
            Dim fs As FileStream = New FileStream(pf_dst, FileMode.OpenOrCreate, FileAccess.Write)
            fs.Write(ni, 0, Convert.ToInt32(ni.Length))
            fs.Close()

            'verifica se la dimensione è maggiore di zero 13.06.2022 salvo

            Dim fi As FileInfo = New FileInfo(pf_dst)
            Dim fileSizeInBytes As Long = fi.Length

            If fileSizeInBytes = 0 Then

                'cancella il file a Zero 13.06.2022 salvo
                If File.Exists(pf_dst) Then
                    File.Delete(pf_dst)
                End If
                ris = False
            Else

                ris = True
            End If


        Catch ex As Exception
            ris = False
            HttpContext.Current.Response.Write("error ConvertImage: " & ex.Message & "<br/>")
        End Try

        Return ris

    End Function


    Public Shared Function GetTransparentArrayFromFileWithDelete(ByVal pathToFile As String) As Byte()
        Dim newImage As Byte() = New Byte(-1) {}
        Dim [error] As String = String.Empty

        Try

            Using bmp As Bitmap = New Bitmap(pathToFile)
                Dim pixel As Color = bmp.GetPixel(0, 0)

                If pixel.A <> 0 Then
                    ' Make backColor transparent for myBitmap.
                    bmp.MakeTransparent(Color.Transparent)
                    Dim converter As ImageConverter = New ImageConverter()
                    newImage = CType(converter.ConvertTo(bmp, GetType(Byte())), Byte())
                    bmp.Dispose()
                Else
                    Dim fs As FileStream = New FileStream(pathToFile, FileMode.OpenOrCreate, FileAccess.Read)
                    newImage = New Byte(fs.Length - 1) {}
                    fs.Read(newImage, 0, Convert.ToInt32(fs.Length))
                    fs.Close()
                End If

                'File.Delete(pathToFile);  //in produzione elimina il file originale
            End Using
        Catch ex As Exception

            HttpContext.Current.Response.Write("error GetTransparentArrayFromFileWithDelete: " & ex.Message & "<br/>")


        End Try

        Return newImage
    End Function





End Class
