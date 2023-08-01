Imports System.IO
Imports iTextSharp.text.pdf

Public Class DatiStampaAttoNotorio
    Public natoil As String
    Public residentea As String
    Public indirizzo As String
    Public natoa As String
    Public sottoscritto As String
    Public cap As String
End Class

Public Class StampaAttoNotorio

    Public Shared Function GeneraDocumento(mie_dati As DatiStampaAttoNotorio) As MemoryStream
        Dim pdfTemplate As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\modelli_documenti\attodinotorieta.pdf"
        Dim PdfReader As PdfReader = New PdfReader(pdfTemplate)
        Dim PdfStamper As PdfStamper = Nothing
        Dim ms As MemoryStream = Nothing

        Try
            ms = New MemoryStream()
            PdfStamper = New PdfStamper(PdfReader, ms)
            Dim pdfFormFields As AcroFields = PdfStamper.AcroFields
            With mie_dati
                pdfFormFields.SetField("natoil", .natoil)
                pdfFormFields.SetField("residentea", .residentea)
                pdfFormFields.SetField("indirizzo", .indirizzo)
                pdfFormFields.SetField("natoa", .natoa)
                pdfFormFields.SetField("sottoscritto", .sottoscritto)
                pdfFormFields.SetField("cap", .cap)
            End With
            PdfStamper.FormFlattening = True
            PdfStamper.Close()

        Catch ex As Exception
            HttpContext.Current.Trace.Write("Errore in GeneraDocumento:" & ex.Message)
        End Try
        Return ms

    End Function
End Class
