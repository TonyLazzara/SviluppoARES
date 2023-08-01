Imports System
Imports System.IO
Imports System.Web
Imports System.Web.UI

Partial Class generaStampaPrenotazione
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            ' recuper il parametri di stampa dalla Session!
            Dim mie_dati As DatiStampaPrenotazione = Session("DatiStampaPrenotazione")
            Session("DatiStampaFatturaNolo") = Nothing

            Dim StampaDocumento As ClassGeneraHtmlToPdf = New ClassGeneraHtmlToPdf
            Dim ms As MemoryStream = StampaPrenotazione.GeneraDocumento(mie_dati)

            If Not ms Is Nothing Then

                Response.Buffer = True

                Response.Clear()

                Response.ContentType = "application/pdf"

                Response.AddHeader("content-disposition", "inline; filename=file.pdf")


                ' forse se commento questa riga è meglio...
                Response.AddHeader("Content-Length", ms.GetBuffer().Length)


                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)

                Response.OutputStream.Flush()

                HttpContext.Current.ApplicationInstance.CompleteRequest()
                'Response.End()

                Exit Sub
            End If
        Catch ex As Exception
            Trace.Write("Errore generazione pdf: " & ex.Message)
        End Try
    End Sub
End Class
