Imports System
Imports System.IO
Imports System.Web
Imports System.Web.UI

Partial Class GeneraPdf
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Dim Context As HttpContext = HttpContext.Current
        Dim sw As StringWriter = New StringWriter()

        ' Path relativa del mio server
        Dim MiaPath As String = Context.Request.ApplicationPath
        Trace.Write("<p>MiaPath: " & MiaPath & "</p>")

        ' Documento da stampare con il modulo che genera i Pdf
        Dim DocPdf As String = Request.Params("DocPdf")
        Trace.Write("DocPdf: (" & DocPdf & ")")

        If DocPdf Is Nothing Then
            Trace.Write("RedirectLocation: " & MiaPath & "ErroriPdf.aspx?CodiceErrore=NoFile")
            Response.Redirect(MiaPath & "ErroriPdf.aspx?CodiceErrore=NoFile")
            Exit Sub
        End If

        Dim DocPdfPath As String = MiaPath & DocPdf & ".aspx"
        Trace.Write("DocPdfPath: (" & DocPdfPath & ")")
        Try
            ' Richiamo la pagina HTML/ASPX da generare 
            ' con il contesto corrente (nota la variabile Context!)
            ' e viene scritto l'output sulla variabile sw (non sul client!)
            Context.Server.Execute(DocPdfPath, sw)
            Trace.Write("richiesta di DocPdfPath: (" & DocPdfPath & ") eseguita!")
        Catch ex As Exception
            Trace.Write("RedirectLocation: " & MiaPath & "ErroriPdf.aspx?CodiceErrore=FileNoExists")
            Response.Redirect(MiaPath & "ErroriPdf.aspx?CodiceErrore=FileNoExists")
            Exit Sub
        End Try

        Trace.Write("<p>Documento processato</p>")

        Try
            Dim inputHtml As MemoryStream = New MemoryStream(Encoding.ASCII.GetBytes(sw.ToString()))

            ' genero il documento pdf a partire dallo stream html
            Dim ms As MemoryStream = GeneraDocPdf.GeneraDoc(inputHtml)

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

        ' sto ipotizando che la richiesta è stata processata correttamente,
        ' con il contesto corretto e la variabile sw è valorizzata!
        ' Ma la generazione del pdf è andata in errore!
        ' Restituisco all'utente almeno l'html (che può stampare dal browser)
        Trace.Write("(--1--)")
        Response.Buffer = True
        Trace.Write("(--2--)")
        Response.Clear()
        Trace.Write("(--3--)")
        Response.Write(sw)
        Trace.Write("(--4--)")
        Response.OutputStream.Flush()
        Trace.Write("(--5--)")
        Response.End()
        Trace.Write("(--6--)")
    End Sub
End Class
