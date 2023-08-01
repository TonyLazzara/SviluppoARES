Imports System
Imports System.IO
Imports System.Web
Imports System.Web.UI

Partial Class GeneraFatturaNolo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' recuper il parametri di stampa dalla Session!
            Dim mie_dati As DatiStampaFatturaNolo = Session("DatiStampaFatturaNolo")
            Session("DatiStampaFatturaNolo") = Nothing

            Dim StampaDocumento As ClassGeneraHtmlToPdf = New ClassGeneraHtmlToPdf
            Dim ms As MemoryStream = StampaFatturaNolo.GeneraDocumento(mie_dati)
            'Dim pfile As String = Server.MapPath("\fatture_nolo/file.pdf")        'Path aggiunto 04.01.2021

            If Not ms Is Nothing Then

                Response.Buffer = True
                Response.Clear()
                Response.ContentType = "application/pdf"
                Response.AddHeader("content-disposition", "inline; filename=file.pdf")
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
