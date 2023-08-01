Imports System
Imports System.IO
Imports System.Web
Imports System.Web.UI

Partial Class GeneraModelloMulte
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' recuper il parametri di stampa dalla Session!
            Dim myDati As StampaModelloMulte = Session("StampaModelloMulte")
            Session("StampaModelloMulte") = Nothing
            If myDati Is Nothing Then
                myDati = New StampaModelloMulte
                With myDati
                    'gestire eventuale errore
                End With
            End If

            'Dim StampaDocumento As ClassGeneraHtmlToPdf = New ClassGeneraHtmlToPdf
            Dim ms As MemoryStream = myDati.GeneraDocumento()

            Trace.Write("ms: " & (ms Is Nothing))

            If Not ms Is Nothing Then
                ' Trace.Write("(1)")
                Response.Buffer = True
                ' Trace.Write("(2)")
                Response.Clear()
                ' Trace.Write("(3)")
                Response.ContentType = "application/pdf"
                ' Trace.Write("(4)")
                Response.AddHeader("content-disposition", "inline; filename=file.pdf")
                ' Trace.Write("(5)")

                Response.AddHeader("Content-Length", ms.GetBuffer().Length)
                ' Trace.Write("(6)")

                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
                ' Trace.Write("(7)")
                Response.OutputStream.Flush()
                ' Trace.Write("(8)")
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                'Response.End()
                ' Trace.Write("(9)")
                If myDati.SalvaComeAllegato = True Then
                    Dim path As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "gestione_multe\Allegati\" & myDati.Anno & "\"
                    If Directory.Exists(path) = False Then 'prima verifica che non esite già, altrimenti la creo
                        Directory.CreateDirectory(path)
                    End If
                    Dim NomeFile As String = myDati.IdMulta.PadLeft(6, "0"c) & "_" & myDati.Anno & "_" & myDati.TipoAllegato.PadLeft(2, "0"c) & "_" & myDati.NomeModello & ".pdf"
                    'salva il file
                    File.WriteAllBytes(path & NomeFile, ms.GetBuffer())

                    If File.Exists(path & NomeFile) Then
                        'qui il codice per registrare il percorso del file nell'apposita tabella degli allegati
                        Dim my_allegatiMulte As AllegatiMulte = New AllegatiMulte
                        With my_allegatiMulte
                            .DataCreazione = Now
                            .IdTipoDocumento = myDati.TipoAllegato
                            .NomeFile = NomeFile
                            .PercorsoFile = path
                            .IdMulta = myDati.IdMulta
                            my_allegatiMulte.InsertAllegatoMulta()
                        End With
                    End If
                End If

                Exit Sub
            End If
        Catch ex As Exception
            Trace.Write("Errore generazione pdf: " & ex.Message)
        End Try


    End Sub
End Class
