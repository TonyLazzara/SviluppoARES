Imports System.IO
Imports iTextSharp.text.pdf

Public Class DatiStampaContrattoNew
    Public conducente1 As String
    Public conducente2 As String
    Public costo As String
    Public costounitarioscontato As String
    Public extra_e_penali As String
    Public fatturare_a As String
    Public note As String ' Inizia in  maiscolo adesso: Nota
    Public qta As String ' è stato modificato il nome: quantita
    Public ran As String
    Public resn As String
    Public rientro_previsto As String
    Public sconto As String
    Public totale As String
    Public uscita As String
    Public vari As String
    Public veicolo As String
    Public voucher As String
    Public voucher_gg As String
    Public voucher_n As String
    Public rientro As String
    Public sportello_ant_sx As String
    Public sportello_post_sx As String
    Public cerchione_ant_sx As String
    Public cerchione_post_sx As String
    Public ruota_ant_sx As String
    Public ruota_post_sx As String
    Public paraurti_ant_sx As String
    Public paraurti_ant_dx As String
    Public proiettore_ant_sx As String
    Public proiettore_ant_dx As String
    Public maschera_ant As String
    Public targa_ant As String
    Public cofano_ant As String
    Public parabrezza_ant As String
    Public tetto As String
    Public lunotto_post As String
    Public cofano_o_portello_post As String
    Public paraurti_post_sx As String
    Public paraurti_post_dx As String
    Public fanale_post_sx As String
    Public fanale_post_dx As String
    Public targa_post As String
    Public sportello_ant_dx As String
    Public sportello_post_dx As String
    Public cerchione_ant_dx As String
    Public cerchione_post_dx As String
    Public ruota_post_dx As String
    Public ruota_ant_dx As String
    Public danno_1 As String
    Public danno_2 As String

    Public accessori As String
    Public logo As iTextSharp.text.Image
End Class


Public Class StampaContrattoNew

    Public Shared Function GeneraDocumento(mie_dati As DatiStampaContrattoNew) As MemoryStream
        Dim pdfTemplate As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\modelli_documenti\ModuloDiNoleggioNew.pdf"
        Dim PdfReader As PdfReader = New PdfReader(pdfTemplate)
        Dim PdfStamper As PdfStamper = Nothing
        Dim ms As MemoryStream = Nothing

        Try
            ms = New MemoryStream()
            PdfStamper = New PdfStamper(PdfReader, ms)
            Dim pdfFormFields As AcroFields = PdfStamper.AcroFields
            With mie_dati
                pdfFormFields.SetField("conducente1", .conducente1)
                pdfFormFields.SetField("conducente2", .conducente2)
                pdfFormFields.SetField("costo", .costo)
                pdfFormFields.SetField("costounitarioscontato", .costounitarioscontato)
                pdfFormFields.SetField("extra_e_penali", .extra_e_penali)
                pdfFormFields.SetField("fatturare_a", .fatturare_a)
                pdfFormFields.SetField("Note", .note)
                pdfFormFields.SetField("quantita", .qta)
                pdfFormFields.SetField("ran", .ran)
                pdfFormFields.SetField("resn", .resn)
                pdfFormFields.SetField("rientro_previsto", .rientro_previsto)
                pdfFormFields.SetField("sconto", .sconto)
                pdfFormFields.SetField("totale", .totale)
                pdfFormFields.SetField("uscita", .uscita)
                pdfFormFields.SetField("vari", .vari)
                pdfFormFields.SetField("veicolo", .veicolo)
                pdfFormFields.SetField("voucher", .voucher)
                pdfFormFields.SetField("voucher_gg", .voucher_gg)
                pdfFormFields.SetField("voucher_n", .voucher_n)
                pdfFormFields.SetField("rientro", .rientro)
                pdfFormFields.SetField("danno_1", .danno_1)
                pdfFormFields.SetField("danno_2", .danno_2)

                pdfFormFields.SetField("accessori", .accessori)




                '  Dim chartLoc As String = "C:\Sviluppo\src.entermed.it\htdocs\modelli_documenti\fine_Eemergenza_lieve.png"
                '  HttpContext.Current.Trace.Write(chartLoc)
                '  Dim test As System.Drawing.Image = System.Drawing.Image.FromHbitmap(chartLoc)
                '  Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(test, System.Drawing.Imaging.ImageFormat.Png)









                ' pdfFormFields.SetField("sportello_ant_sx", "1")
                ' If .pulsante = "true" Then

                ' pdfFormFields.SetFieldProperty("pulsante", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'pdfFormFields.SetFieldProperty("pulsante", "setfflags", BaseField.VISIBLE, Nothing)
                ' End If

                'If .sportello_ant_sx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_sportello_ant_sx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("sportello_ant_sx", .sportello_ant_sx)
                'End If
                'If .sportello_post_sx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_sportello_posteriore_sx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("sportello_post_sx", .sportello_post_sx)
                'End If
                'If .cerchione_ant_sx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_cerchione_ant_sx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("cerchione_ant_sx", .cerchione_ant_sx)
                'End If
                'If .cerchione_post_sx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_cerchione_post_sx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("cerchione_post_sx", .cerchione_post_sx)
                'End If
                'If .ruota_ant_sx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_ruota_ant_sx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("ruota_ant_sx", .ruota_ant_sx)
                'End If
                'If .ruota_post_sx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_ruota_post_sx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("ruota_post_sx", .ruota_post_sx)
                'End If
                'If .paraurti_ant_sx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_paraurti_ant_sx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("paraurti_ant_sx", .paraurti_ant_sx)
                'End If
                'If .paraurti_ant_dx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_paraurti_ant_dx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("paraurti_ant_dx", .paraurti_ant_dx)
                'End If
                'If .proiettore_ant_sx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_proiettore_ant_sx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("proiettore_ant_sx", .proiettore_ant_sx)
                'End If
                'If .proiettore_ant_dx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_proiettore_ant_dx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("proiettore_ant_dx", .proiettore_ant_dx)
                'End If
                'If .maschera_ant = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_maschera_ant", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("maschera_ant", .maschera_ant)
                'End If
                'If .targa_ant = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_targa_ant", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("targa_ant", .targa_ant)
                'End If
                'If .cofano_ant = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_cofano_ant", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("cofano_ant", .cofano_ant)
                'End If
                'If .tetto = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_tetto", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("tetto", .tetto)
                'End If
                'If .lunotto_post = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_lunotto_post", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("lunotto_post", .lunotto_post)
                'End If
                'If .cofano_o_portello_post = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_cofano_o_portello_post", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("cofano_o_portello_post", .cofano_o_portello_post)
                'End If
                'If .paraurti_post_sx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_paraurti_post_sx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("paraurti_post_sx", .paraurti_post_sx)
                'End If
                'If .paraurti_post_dx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_paraurti_post_dx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("paraurti_post_dx", .paraurti_post_dx)
                'End If
                'If .fanale_post_sx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_fanale_post_sx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("fanale_post_sx", .fanale_post_sx)
                'End If
                'If .fanale_post_dx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_fanale_post_dx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("fanale_post_dx", .fanale_post_dx)
                'End If
                'If .targa_post = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_targa_post", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("targa_post", .targa_post)
                'End If
                'If .sportello_ant_dx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_sportello_ant_dx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("sportello_ant_dx", .sportello_ant_dx)
                'End If
                'If .sportello_post_dx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_sportello_post_dx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("sportello_post_dx", .sportello_post_dx)
                'End If
                'If .cerchione_ant_dx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_cerchione_ant_dx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("cerchione_ant_dx", .cerchione_ant_dx)
                'End If
                'If .cerchione_post_dx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_cerchione_post_dx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("cerchione_post_dx", .cerchione_post_dx)
                'End If
                'If .ruota_post_dx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_ruota_post_dx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("ruota_post_dx", .ruota_post_dx)
                'End If
                'If .ruota_ant_dx = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_ruota_ant_dx", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("ruota_ant_dx", .ruota_ant_dx)
                'End If
                'If .parabrezza_ant = "" Then
                '    pdfFormFields.SetFieldProperty("pulsante_parabrezza_ant", "setflags", PdfAnnotation.FLAGS_HIDDEN, Nothing)
                'Else
                '    pdfFormFields.SetField("parabrezza_ant", .parabrezza_ant)
                'End If



            End With
            PdfStamper.FormFlattening = True
            PdfStamper.Close()

        Catch ex As Exception
            HttpContext.Current.Trace.Write("Errore GeneraDocumento:<br/>" & ex.Message & "<br/>")
        End Try
        Return ms

    End Function
End Class
