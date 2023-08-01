Imports System
Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Diagnostics

Partial Class generaPdf3
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' recuper il parametri di stampa dalla Session!
            Dim url_print As String = Session("url_print")
            Session("url_print") = ""


            Dim newPDF As String = Request.QueryString("newPDF")        'aggiunto salvo 16.06.2023 x nuovo reindirizzamento se =1

            'inserito x test nuova creazione PDF
            'Dim myhtml2pdf As New MyHTML2PDF()
            'Dim data As String = Date.Now.ToShortDateString & "-" & Date.Now.ToShortTimeString
            'Dim pdf As String = myhtml2pdf.converti(data, url_print)
            'Exit Sub

            Dim StampaDocumento As ClassGeneraHtmlToPdf = New ClassGeneraHtmlToPdf

            If InStr(1, Request.Url.ToString, "//ares.sicilyrentcar.it", 1) > 0 Then
                url_print = "http://ares.sicilyrentcar.it/" & url_print
            ElseIf InStr(1, Request.Url.ToString, "//formazione.sicilyrentcar.it", 1) > 0 Then
                url_print = "http://formazione.sicilyrentcar.it/" & url_print
            ElseIf InStr(1, Request.Url.ToString, "//sviluppo.sicilyrentcar.it", 1) > 0 Then
                url_print = "http://sviluppo.sicilyrentcar.it/" & url_print
            ElseIf InStr(1, Request.Url.ToString, "//sviluppoares.sicilyrentcar.it", 1) > 0 Then
                url_print = "http://sviluppoares.sicilyrentcar.it/" & url_print
            Else

                Dim lh As String = Request.Url.ToString()
                Dim a() As String = Split(lh, "/")
                url_print = "http://" & a(2) & "/" & url_print

            End If


            'Response.Write(Request.Url.ToString() & "<br/>")
            'Response.Write(url_print & "<br/>")
            'Response.End()

            Dim ms As MemoryStream = StampaDocumento.GeneraHtmlToPdf(url_print)

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
            'Trace.Write("Errore generazione pdf: " & ex.Message)
            Response.Write("Error_generaPF3_:" & ex.Message & "<br/>")
        End Try
    End Sub

    'Public Sub Scrivi(ByVal NomeFile As String, ByVal Str As StringWriter)
    '    Try
    '        Using fs1 As FileStream = New FileStream(NomeFile, FileMode.CreateNew, FileAccess.Write)
    '            Using s1 As BinaryWriter = New BinaryWriter(fs1)

    '                s1.Write(Str.ToString)

    '            End Using
    '        End Using
    '    Catch ex As Exception
    '        Console.Write(ex.Message)
    '    End Try
    'End Sub

    'Private Function GeneraSuFile(ByVal DocPdfPath As String) As String
    '    Dim sw As StringWriter = New StringWriter()
    '    Dim NomeFile As String = System.Guid.NewGuid.ToString() & ".html"
    '    Dim NomeCompleto = "C:\Ribaltamento\wkhtmltopdf\" & NomeFile

    '    Try
    '        ' Richiamo la pagina HTML/ASPX da generare 
    '        ' con il contesto corrente (nota la variabile Context!)
    '        ' e viene scritto l'output sulla variabile sw (non sul client!)
    '        Context.Server.Execute(DocPdfPath, sw)
    '        Trace.Write("richiesta di DocPdfPath: (" & DocPdfPath & ") eseguita!")

    '        ' salvo il documento generato su file temporaneo... 
    '        Scrivi(NomeCompleto, sw)

    '        Trace.Write("Documento processato")
    '    Catch ex As Exception
    '        Trace.Write("RedirectLocation: " & MiaPath & "ErroriPdf.aspx?CodiceErrore=FileNoExists")
    '        Response.Redirect(MiaPath & "ErroriPdf.aspx?CodiceErrore=FileNoExists")
    '        Return ""
    '    End Try

    '    Return NomeCompleto
    'End Function

End Class
Class MyHTML2PDF
    Public Function converti(ByVal data As String, ByVal filehtml As String) As String
        converti = ""
        data = data.Replace("/", "")
        data = data.Replace(":", "_")

        Try
            Dim wrk = HttpContext.Current.Server.MapPath("~/wkhtmltopdf/wkhtmltopdf.exe")

            'se localhost
            filehtml = "http://localhost:41440/" & filehtml

            Dim filePDF = HttpContext.Current.Server.MapPath("cassaPDF/")

            Dim parametri = filehtml & " " & filePDF & data & ".pdf"

            Dim p As New Process()
            p.StartInfo.UseShellExecute = False
            p.StartInfo.CreateNoWindow = True
            p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.RedirectStandardError = True
            p.StartInfo.RedirectStandardInput = True
            p.StartInfo.WorkingDirectory = HttpContext.Current.Server.MapPath("~/")
            p.StartInfo.FileName = wrk
            p.StartInfo.Arguments = parametri
            p.Start()
            converti = "/fatture_nolo/" & data & ".pdf"

        Catch ex As Exception

        End Try


    End Function


End Class
