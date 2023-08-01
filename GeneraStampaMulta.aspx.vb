Imports System
Imports System.IO
Imports System.Web
Imports System.Web.UI


Partial Class GeneraStampaMulta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' recuper il parametri di stampa dalla Session!
            Dim mie_dati As DatiStampaFatturaNolo = Session("DatiStampaMulta")
            Session("DatiStampaMulta") = Nothing
          

            Dim nazione As String = ""
            If Request.QueryString("lang") <> "" Then
                nazione = Request.QueryString("lang")
            End If
            Dim StampaDocumento As ClassGeneraHtmlToPdf = New ClassGeneraHtmlToPdf
            Dim ms As MemoryStream = StampaFatturaNolo.GeneraDocumento2(mie_dati)

            Trace.Write("ms: " & (ms Is Nothing))

            If Not ms Is Nothing Then
                Trace.Write("(1)")
                Response.Buffer = True
                Trace.Write("(2)")
                Response.Clear()
                Trace.Write("(3)")
                Response.ContentType = "application/pdf"
                Trace.Write("(4)")
                Response.AddHeader("content-disposition", "inline; filename=file.pdf")
                Trace.Write("(5)")

                ' forse se commento questa riga è meglio...
                Response.AddHeader("Content-Length", ms.GetBuffer().Length)
                Trace.Write("(6)")

                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
                Trace.Write("(7)")
                Response.OutputStream.Flush()
                Trace.Write("(8)")
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                'Response.End()
                Trace.Write("(9)")
                Exit Sub
            End If
        Catch ex As Exception
            Trace.Write("Errore generazione pdf: " & ex.Message)
        End Try
    End Sub
End Class
