Imports System.IO
Imports iTextSharp.text.pdf

Public Class DatiStampaModuloPagamenti
    Public autorizzazione As String = ""
    Public conto_anno As String = ""
    Public conto_mese As String = ""
    Public conto_numero As String = ""
    Public contratto_numero As String = ""
    Public data_anno As String = ""
    Public data_giorno As String = ""
    Public data_mese As String = ""
    Public documento_anno As String = ""
    Public documento_giorno As String = ""
    Public documento_mese As String = ""
    Public documento_numero As String = ""
    Public documento_rilasciato As String = ""
    Public documento_tipo As String = ""
    Public importo_centesimi As String = ""
    Public importo_euro As String = ""
    Public indirizzo As String = ""
    Public indirizzo_numero As String = ""
    Public localita As String = ""
    Public nato_a As String = ""
    Public nato_anno As String = ""
    Public nato_giorno As String = ""
    Public nato_mese As String = ""
    Public residenza As String = ""
    Public sottoscritto As String = ""
End Class

Public Class StampaModuloPagamenti

    Public Shared Function GeneraDocumento(ByVal mie_dati As DatiStampaModuloPagamenti, ByVal tipo_stampa As Integer) As MemoryStream
        'tipo_stampa =1 tutto
        '0= parziale

        Dim pdfTemplate As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\modelli_documenti\ModelloPagamentoTelefonico.pdf"
        Dim PdfReader As PdfReader = New PdfReader(pdfTemplate)
        Dim PdfStamper As PdfStamper = Nothing
        Dim ms As MemoryStream = Nothing

        Try
            ms = New MemoryStream()
            PdfStamper = New PdfStamper(PdfReader, ms)
            Dim pdfFormFields As AcroFields = PdfStamper.AcroFields
            With mie_dati
                pdfFormFields.SetField("autorizzazione", .autorizzazione)

                pdfFormFields.SetField("conto_anno", .conto_anno)
                pdfFormFields.SetField("conto_mese", .conto_mese)
                pdfFormFields.SetField("conto_numero", .conto_numero)
                pdfFormFields.SetField("contratto_numero", .contratto_numero)
                pdfFormFields.SetField("data_anno", .data_anno)
                pdfFormFields.SetField("data_giorno", .data_giorno)
                pdfFormFields.SetField("data_mese", .data_mese)
                If tipo_stampa = 1 Then pdfFormFields.SetField("documento_anno", .documento_anno)
                If tipo_stampa = 1 Then pdfFormFields.SetField("documento_giorno", .documento_giorno)
                If tipo_stampa = 1 Then pdfFormFields.SetField("documento_mese", .documento_mese)
                If tipo_stampa = 1 Then pdfFormFields.SetField("documento_numero", .documento_numero)
                If tipo_stampa = 1 Then pdfFormFields.SetField("documento_rilasciato", .documento_rilasciato)
                If tipo_stampa = 1 Then pdfFormFields.SetField("documento_tipo", .documento_tipo)
                pdfFormFields.SetField("importo_centesimi", .importo_centesimi)
                pdfFormFields.SetField("importo_euro", .importo_euro)
                If tipo_stampa = 1 Then pdfFormFields.SetField("indirizzo", .indirizzo)
                If tipo_stampa = 1 Then pdfFormFields.SetField("indirizzo_numero", .indirizzo_numero)
                pdfFormFields.SetField("localita", .localita)
                If tipo_stampa = 1 Then pdfFormFields.SetField("nato_a", .nato_a)
                If tipo_stampa = 1 Then pdfFormFields.SetField("nato_anno", .nato_anno)
                If tipo_stampa = 1 Then pdfFormFields.SetField("nato_giorno", .nato_giorno)
                If tipo_stampa = 1 Then pdfFormFields.SetField("nato_mese", .nato_mese)
                If tipo_stampa = 1 Then pdfFormFields.SetField("residenza", .residenza)
                If tipo_stampa = 1 Then pdfFormFields.SetField("sottoscritto", .sottoscritto)

            End With
            PdfStamper.FormFlattening = True
            PdfStamper.Close()

        Catch ex As Exception
            HttpContext.Current.Trace.Write("Errore in GeneraDocumento:" & ex.Message)
        End Try
        Return ms

    End Function
End Class
