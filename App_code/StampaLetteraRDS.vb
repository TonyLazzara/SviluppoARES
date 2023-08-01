Imports System.IO
Imports iTextSharp.text.pdf


Public Enum tipo_linguaggio_rds
    italiano
    inglese
End Enum

Public Enum tipo_lettera_rds
    NoKasko = 1
    NoDichiarazione = 2
    Furto = 4
    ErratoRifornimento = 8
    Ruote = 16
    Imperizia = 32
    AttiVandalici = 64
    GuidaNonAutorizzata = 128
    SinistroAttivo = 256
    FranchigiaParziale = 512
    DichiarazioneParziale = 1024
    PaesiVietati = 2048
End Enum

Public Class DatiStampaLetteraRDS
    Public dati_conducente As String
    Public num_contratto As String
    Public data_contratto As String

    Public data_documento As String

    Public targa As String
    Public importo As String
    Public iva As String
    Public spese_postali As String
    Public num_rds As String

    Public motivazione As String
    Public linguaggio As tipo_linguaggio_rds

End Class

Public Class StampaLetteraRDS

    Public Shared Function GeneraDocumento(mie_dati As DatiStampaLetteraRDS) As MemoryStream
        Dim pdfTemplate As String

        If mie_dati.linguaggio = tipo_linguaggio_rds.inglese Then
            pdfTemplate = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "modelli_documenti\LetteraIngRDS.pdf"
        Else
            pdfTemplate = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "modelli_documenti\LetteraRDS.pdf"
        End If

        Dim PdfReader As PdfReader = New PdfReader(pdfTemplate)
        Dim PdfStamper As PdfStamper = Nothing
        Dim ms As MemoryStream = Nothing

        Try
            ms = New MemoryStream()
            PdfStamper = New PdfStamper(PdfReader, ms)
            Dim pdfFormFields As AcroFields = PdfStamper.AcroFields
            With mie_dati
                pdfFormFields.SetField("targa", .targa)
                pdfFormFields.SetField("dati_conducente", .dati_conducente)
                pdfFormFields.SetField("motivazione", .motivazione)
                pdfFormFields.SetField("importo", .importo)
                pdfFormFields.SetField("iva", .iva)
                pdfFormFields.SetField("spese_postali", .spese_postali)
                pdfFormFields.SetField("num_rds", .num_rds)
                pdfFormFields.SetField("num_contratto", .num_contratto)
                pdfFormFields.SetField("data_contratto", .data_contratto)
                pdfFormFields.SetField("data_documento", .data_documento)
            End With
            PdfStamper.FormFlattening = True
            PdfStamper.Close()

        Catch ex As Exception
            HttpContext.Current.Trace.Write("Errore in GeneraDocumento:" & ex.Message)
        End Try
        Return ms

    End Function
End Class
