Imports System.IO

Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.tool.xml
Imports iTextSharp.tool.xml.html
Imports iTextSharp.tool.xml.parser
Imports iTextSharp.tool.xml.pipeline
Imports iTextSharp.tool.xml.pipeline.css
Imports iTextSharp.tool.xml.pipeline.end
Imports iTextSharp.tool.xml.pipeline.html


Public Class GeneraDocPdf


    Public Enum posizioneNumPagina
        NoNumerazione = 1
        AltoSinistra
        AltoCentro
        AltoDestro
        BassoSinistra
        BassoCentro
        BassoDestro
    End Enum

    Private Shared Function getPathRoot() As String
        getPathRoot = (HttpContext.Current.Server.MapPath("~")).Replace("\", "/")
    End Function

    Private Class myAbstractImageProvider
        Inherits AbstractImageProvider

        Public Overrides Function getImageRootPath() As String
            getImageRootPath = getPathRoot()
            HttpContext.Current.Trace.Write("getImageRootPath: " & getImageRootPath)
        End Function
    End Class

    Private Class myLinkProvider
        Implements ILinkProvider

        Public Function GetLinkRoot() As String Implements ILinkProvider.GetLinkRoot
            GetLinkRoot = getPathRoot()
            HttpContext.Current.Trace.Write("GetLinkRoot: " & GetLinkRoot)
        End Function
    End Class

    Private Class MyPageEventHandler
        Inherits PdfPageEventHelper

        ' esempio stampa header su: http://kuujinbo.info/cs/itext_img_hdr.aspx

        Private NumeroPagina As Integer = 0

        'Private _ImageHeader As iTextSharp.text.Image

        'Public Property ImageHeader() As iTextSharp.text.Image
        '    Get
        '        Return _ImageHeader
        '    End Get
        '    Set(ByVal value As iTextSharp.text.Image)
        '        _ImageHeader = value
        '    End Set
        'End Property

        Public posizione As posizioneNumPagina = posizioneNumPagina.BassoDestro

        Public Overrides Sub OnEndPage(writer As iTextSharp.text.pdf.PdfWriter, document As iTextSharp.text.Document)
            NumeroPagina = NumeroPagina + 1
            Dim page As Rectangle = document.PageSize

            ' intestazione
            Dim cellHeadHeight As Double = document.TopMargin
            Dim head As PdfPTable = New PdfPTable(3)
            head.TotalWidth = page.Width

            Dim c As PdfPCell

            If posizione = posizioneNumPagina.AltoSinistra Then
                c = New PdfPCell(New Phrase("" & NumeroPagina))
            Else
                c = New PdfPCell(New Phrase(""))
            End If
            c.Border = PdfPCell.NO_BORDER
            c.VerticalAlignment = Element.ALIGN_BOTTOM ' ALIGN_MIDDLE
            c.HorizontalAlignment = Element.ALIGN_CENTER
            c.FixedHeight = cellHeadHeight
            head.AddCell(c)

            If posizione = posizioneNumPagina.AltoCentro Then
                c = New PdfPCell(New Phrase("" & NumeroPagina))
            Else
                c = New PdfPCell(New Phrase(""))
            End If
            c.Border = PdfPCell.NO_BORDER
            c.VerticalAlignment = Element.ALIGN_BOTTOM ' ALIGN_MIDDLE
            c.HorizontalAlignment = Element.ALIGN_CENTER
            c.FixedHeight = cellHeadHeight
            head.AddCell(c)

            If posizione = posizioneNumPagina.AltoDestro Then
                c = New PdfPCell(New Phrase("" & NumeroPagina))
            Else
                c = New PdfPCell(New Phrase(""))
            End If
            c.Border = PdfPCell.NO_BORDER
            c.VerticalAlignment = Element.ALIGN_BOTTOM ' ALIGN_MIDDLE
            c.HorizontalAlignment = Element.ALIGN_CENTER
            c.FixedHeight = cellHeadHeight
            head.AddCell(c)

            head.WriteSelectedRows( _
                0, -1, _
                0, _
                 page.Height - cellHeadHeight + head.TotalHeight, _
                writer.DirectContent)
            '--------------------------------------------

            ' FOOTER
            Dim cellFooterHeight As Double = document.BottomMargin
            Dim footer As PdfPTable = New PdfPTable(3)
            footer.TotalWidth = page.Width

            If posizione = posizioneNumPagina.BassoSinistra Then
                c = New PdfPCell(New Phrase("" & NumeroPagina))
            Else
                c = New PdfPCell(New Phrase(""))
            End If
            c.Border = PdfPCell.NO_BORDER
            c.VerticalAlignment = Element.ALIGN_TOP ' ALIGN_MIDDLE
            c.HorizontalAlignment = Element.ALIGN_CENTER
            c.FixedHeight = cellFooterHeight
            footer.AddCell(c)

            If posizione = posizioneNumPagina.BassoCentro Then
                c = New PdfPCell(New Phrase("" & NumeroPagina))
            Else
                c = New PdfPCell(New Phrase(""))
            End If
            c.Border = PdfPCell.NO_BORDER
            c.VerticalAlignment = Element.ALIGN_TOP ' ALIGN_MIDDLE
            c.HorizontalAlignment = Element.ALIGN_CENTER
            c.FixedHeight = cellFooterHeight
            footer.AddCell(c)

            If posizione = posizioneNumPagina.BassoDestro Then
                c = New PdfPCell(New Phrase("" & NumeroPagina))
            Else
                c = New PdfPCell(New Phrase(""))
            End If
            c.Border = PdfPCell.NO_BORDER
            c.VerticalAlignment = Element.ALIGN_TOP ' ALIGN_MIDDLE
            c.HorizontalAlignment = Element.ALIGN_CENTER
            c.FixedHeight = cellFooterHeight
            footer.AddCell(c)

            footer.WriteSelectedRows( _
                0, -1, _
                0, _
                cellFooterHeight, _
                writer.DirectContent)
            '--------------------------------------------
            ' MyBase.OnEndPage(writer, document)
            HttpContext.Current.Trace.Write("Nuova pagina: " & NumeroPagina & " - " & posizione.ToString)
        End Sub
    End Class

    Public Shared Function GeneraDoc(ByVal inputHtml As MemoryStream, Optional ByVal orientamento As String = "verticale") As MemoryStream
        Dim ms As MemoryStream = Nothing
        Dim document As Document = Nothing
        Dim writer As PdfWriter = Nothing
        Dim htmlContext As HtmlPipelineContext = Nothing

        If Not inputHtml Is Nothing Then
            Try
                ms = New MemoryStream()

                HttpContext.Current.Trace.Write("GeneraDoc Inizio --------------------------- con numero pagina")

                ' non so se serve... dato che non li uso esplicitamente, serve a caricare i font di sistema
                ' FontFactory.RegisterDirectories()

                ' genero un oggetto documento pdf
                ' attenzione:
                ' il bordo Top e Bottom deve essere almeno di 20 affinché riesca a vedere il numero pagina....
                If orientamento = "verticale" Then
                    document = New Document(PageSize.A4, 10, 10, 20, 20) '(PageSize.A4).Rotate() ', 5, 5, 5, 5) ' esempio a4 landscape con bordi
                ElseIf orientamento = "orizzontale" Then
                    document = New Document(PageSize.A4.Rotate(), 10, 10, 20, 20) '(PageSize.A4 ', 5, 5, 5, 5) ' esempio a4 landscape con bordi
                End If


                ' generatore del documento pdf
                writer = PdfWriter.GetInstance(document, ms)
                Dim e As MyPageEventHandler = New MyPageEventHandler()
                e.posizione = posizioneNumPagina.BassoCentro
                writer.PageEvent = e
                document.Open()

                HttpContext.Current.Trace.Write("GeneraDoc Open")

                ' gestore dei tag HTML
                htmlContext = New HtmlPipelineContext()
                htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory())
                htmlContext.SetImageProvider(New myAbstractImageProvider())
                htmlContext.SetLinkProvider(New myLinkProvider())

                ' gestore dei fogli di stile
                Dim cssResolver As ICSSResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(True)

                ' pipeline che genera il documento pdf
                Dim pipeline As AbstractPipeline = _
                    New CssResolverPipeline(cssResolver, _
                        New HtmlPipeline(htmlContext, _
                            New PdfWriterPipeline(document, writer)))

                ' 
                Dim worker As XMLWorker = New XMLWorker(pipeline, True)

                HttpContext.Current.Trace.Write("XMLParser prima")

                ' Parser effettivo del documento XML che
                Dim p As XMLParser = New XMLParser(worker)
                p.Parse(inputHtml)

                HttpContext.Current.Trace.Write("XMLParser dopo")

                ' imposto Autore e creatore
                document.AddCreator("ARES")
                document.AddAuthor("Entermed")

                document.Close()

                HttpContext.Current.Trace.Write("GeneraDoc Fine ---------------------------")
            Catch ex As Exception
                HttpContext.Current.Trace.Write("ex.StackTrace: " & ex.StackTrace)
                HttpContext.Current.Trace.Write("ex.Message: " & ex.Message)
                ' è avvenuto qualche errore...
                ' non è stato generato alcun documento
                ' elimino lo stream di uscita non valido...
                ' almeno vedo la pagina html (che posso cmq stampare)
                ms = Nothing
            Finally
                If Not document Is Nothing Then
                    document = Nothing
                End If
                If Not writer Is Nothing Then
                    writer.Close()
                    writer = Nothing
                End If
                If Not htmlContext Is Nothing Then
                    htmlContext = Nothing
                End If
            End Try
        End If

        Return ms
    End Function
End Class
