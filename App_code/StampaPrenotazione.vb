Imports System.IO
Imports iTextSharp.text.pdf

Public Class DatiStampaPrenotazione
    Public n_pickup_location As String
    Public n_dropoff_location As String
    Public n_fonte As String
    Public n_cod_convenzione As String
    Public n_eta1 As String
    Public n_eta2 As String
    Public n_gg As String
    Public n_gg_to As String
    Public n_cognome As String
    Public n_nome As String
    Public n_email As String
    Public n_indirizzo As String
    Public n_nascita As String
    Public n_gruppo As String
    Public n_nvolo1 As String
    Public n_nvolo2 As String
    Public n_riferimento As String
    Public n_riftel As String
    Public n_fatturare_a As String
    Public n_dettaglio_nome As String
    Public n_dettaglio_costo_to As String
    Public n_dettaglio_costo As String
    Public n_dettaglio_omaggio As String
    Public n_note As String
    Public n_num_prenotazione As String
    Public n_dettagli_prenotazione As String
End Class

Public Class StampaPrenotazione
    Public Shared Function GeneraDocumento(ByVal mie_dati As DatiStampaPrenotazione) As MemoryStream
        Dim pdfTemplate As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\modelli_documenti\prenotazione.pdf"
        Dim PdfReader As PdfReader = New PdfReader(pdfTemplate)
        Dim PdfStamper As PdfStamper = Nothing
        Dim ms As MemoryStream = Nothing

        Try
            ms = New MemoryStream()
            PdfStamper = New PdfStamper(PdfReader, ms)
            Dim pdfFormFields As AcroFields = PdfStamper.AcroFields
            With mie_dati
                pdfFormFields.SetField("pickup_location", .n_pickup_location)
                pdfFormFields.SetField("dropoff_location", .n_dropoff_location)
                pdfFormFields.SetField("fonte", .n_fonte)
                pdfFormFields.SetField("cod_convenzione", .n_cod_convenzione)
                pdfFormFields.SetField("eta1", .n_eta1)
                pdfFormFields.SetField("eta2", .n_eta2)
                pdfFormFields.SetField("gg", .n_gg)
                pdfFormFields.SetField("gg_to", .n_gg_to)
                pdfFormFields.SetField("cognome", .n_cognome)
                pdfFormFields.SetField("nome", .n_nome)
                pdfFormFields.SetField("email", .n_email)
                pdfFormFields.SetField("indirizzo", .n_indirizzo)
                pdfFormFields.SetField("nascita", .n_nascita)
                pdfFormFields.SetField("gruppo", .n_gruppo)
                pdfFormFields.SetField("nvolo1", .n_nvolo1)
                pdfFormFields.SetField("nvolo2", .n_nvolo2)
                pdfFormFields.SetField("riferimento", .n_riferimento)
                pdfFormFields.SetField("riftel", .n_riftel)
                pdfFormFields.SetField("fatturare_a", .n_fatturare_a)
                pdfFormFields.SetField("dettaglio_nome", .n_dettaglio_nome)
                pdfFormFields.SetField("dettaglio_costo_to", .n_dettaglio_costo_to)
                pdfFormFields.SetField("dettaglio_costo", .n_dettaglio_costo)
                pdfFormFields.SetField("dettaglio_omaggio", .n_dettaglio_omaggio)
                pdfFormFields.SetField("note", .n_note)
                pdfFormFields.SetField("Text1", .n_num_prenotazione)
                pdfFormFields.SetField("Text2", .n_dettagli_prenotazione)
            End With
            PdfStamper.FormFlattening = True
            PdfStamper.Close()

        Catch ex As Exception
            HttpContext.Current.Trace.Write("Errore in GeneraDocumento:" & ex.Message)
        End Try
        Return ms
    End Function
End Class
