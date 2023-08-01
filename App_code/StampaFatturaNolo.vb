Imports System.IO
Imports iTextSharp.text.pdf

Public Class DatiStampaFatturaNolo
    Public n_documento As String
    Public data_fattura As String
    Public cod_cliente As String
    Public rif_ra As String
    Public nome_cognome As String
    Public indirizzo As String
    Public cap As String
    Public citta As String
    Public rif_ric_fisc As String
    Public data_ric_fisc As String
    Public codfiscale_piva As String
    Public codice_fiscale As String
    Public modello As String
    Public pickup As String
    Public dropoff As String
    Public targa As String
    Public data_partenza As String
    Public data_arrivo As String
    Public conducente As String
    Public km_iniziali As String
    Public km_finali As String
    Public servizi As String
    Public quantita As String
    Public costo_unitario As String
    Public totale As String
    Public aliquota_iva As String
    Public pagamenti As String
    Public data_pagamenti As String
    Public modalita_pagamento As String
    Public tipo_pagamento As String
    Public importo As String
    Public imponibile As String
    Public aliquota As String
    Public iva As String
    Public totale_pagamenti As String
    Public imponibile_1 As String
    Public iva_1 As String
    Public totale_fattura As String
    Public a_saldo_fattura As String
    Public cod_cliente_talloncino As String
    Public ra_talloncino As String
    Public importo_1_talloncino As String
    Public n_fattura_talloncino As String
    Public n_voucher_talloncino As String
    Public n_prenotazione_talloncino As String 'ERRONEAMENTE NEL MODELLO IL CAMPO SI CHIAMA importo_2_talloncino
    Public addebito As String
    Public banche As String
    Public abbuoni As String
    Public totale_fattura_senza_abbuoni As String
    Public pec As String
    Public codice_sdi As String
End Class

Public Class StampaFatturaNolo

    Public Shared Function GeneraDocumento(ByVal mie_dati As DatiStampaFatturaNolo) As MemoryStream

        Dim pdfTemplate As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\modelli_documenti\FatturaSRC.pdf"
        Dim PdfReader As PdfReader = New PdfReader(pdfTemplate)
        Dim PdfStamper As PdfStamper = Nothing
        Dim ms As MemoryStream = Nothing





        Try
            ms = New MemoryStream()
            PdfStamper = New PdfStamper(PdfReader, ms)
            Dim pdfFormFields As AcroFields = PdfStamper.AcroFields
            With mie_dati

                'recupera piva se estera 10.08.2022 salvo
                Dim nco As String = .rif_ra
                Dim piva_EE As String = funzioni_comuni_new.getPivaEE(nco)


                Dim codfis_piva As String = .codfiscale_piva
                If piva_EE <> "" Then
                    codfis_piva = piva_EE
                End If


                pdfFormFields.SetField("n_documento", .n_documento)
                pdfFormFields.SetField("data_fattura", .data_fattura)
                pdfFormFields.SetField("cod_cliente", .cod_cliente)
                pdfFormFields.SetField("nome_cognome", .nome_cognome)
                pdfFormFields.SetField("indirizzo", .indirizzo)
                pdfFormFields.SetField("rif_ra", .rif_ra)
                pdfFormFields.SetField("cap", .cap)
                pdfFormFields.SetField("citta", .citta)
                pdfFormFields.SetField("rif_ric_fisc", .rif_ric_fisc)
                pdfFormFields.SetField("data_ric_fisc", .data_ric_fisc)
                pdfFormFields.SetField("codfiscale_piva", codfis_piva)
                pdfFormFields.SetField("codice_fiscale", .codice_fiscale)
                pdfFormFields.SetField("modello", .modello)
                pdfFormFields.SetField("pickup", .pickup)
                pdfFormFields.SetField("dropoff", .dropoff)
                pdfFormFields.SetField("targa", .targa)
                pdfFormFields.SetField("data_partenza", .data_partenza)
                pdfFormFields.SetField("data_arrivo", .data_arrivo)
                pdfFormFields.SetField("conducente", .conducente)
                pdfFormFields.SetField("km_iniziali", .km_iniziali)
                pdfFormFields.SetField("km_finali", .km_finali)
                pdfFormFields.SetField("servizi", .servizi)
                pdfFormFields.SetField("quantita", .quantita)
                pdfFormFields.SetField("costo_unitario", .costo_unitario)
                pdfFormFields.SetField("totale", .totale)
                pdfFormFields.SetField("aliquota_iva", .aliquota_iva)
                pdfFormFields.SetField("pagamenti", .pagamenti)
                pdfFormFields.SetField("data_pagamenti", .data_pagamenti)
                pdfFormFields.SetField("modalita_pagamento", .modalita_pagamento)
                pdfFormFields.SetField("tipo_pagamento", .tipo_pagamento)
                pdfFormFields.SetField("importo", .importo)
                pdfFormFields.SetField("imponibile", .imponibile)
                pdfFormFields.SetField("aliquota", .aliquota)
                pdfFormFields.SetField("iva", .iva)
                pdfFormFields.SetField("totale_pagamenti", .totale_pagamenti)
                pdfFormFields.SetField("imponibile_1", .imponibile_1)
                pdfFormFields.SetField("iva_1", .iva_1)
                pdfFormFields.SetField("totale_fattura", .totale_fattura)
                pdfFormFields.SetField("a_saldo_fattura", .a_saldo_fattura)
                pdfFormFields.SetField("cod_cliente_talloncino", .cod_cliente_talloncino)
                pdfFormFields.SetField("ra_talloncino", .ra_talloncino)
                pdfFormFields.SetField("n_fattura_talloncino", .n_fattura_talloncino)
                pdfFormFields.SetField("n_voucher_talloncino", .n_voucher_talloncino)
                pdfFormFields.SetField("importo_1_talloncino", .importo_1_talloncino)
                pdfFormFields.SetField("importo_2_talloncino", .n_prenotazione_talloncino) 'ERRONEAMENTE NEL MODELLO IL CAMPO SI CHIAMA importo_2_talloncino
                pdfFormFields.SetField("addebito", .addebito)
                pdfFormFields.SetField("banche", .banche)
                pdfFormFields.SetField("abbuoni", .abbuoni)
                pdfFormFields.SetField("totale_fattura2", .totale_fattura_senza_abbuoni)
                pdfFormFields.SetField("pec", .pec)
                pdfFormFields.SetField("codice_sdi", .codice_sdi)
            End With
            PdfStamper.FormFlattening = True
            PdfStamper.Close()





        Catch ex As Exception
            HttpContext.Current.Trace.Write("Errore in GeneraDocumento:" & ex.Message)
        End Try
        Return ms
    End Function

    Public Shared Function GeneraDocumentoInvioMail(ByVal mie_dati As DatiStampaFatturaNolo, ByVal pathfile As String, ByVal destinatario As String) As Boolean

        Dim pdfTemplate As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\modelli_documenti\FatturaSRC.pdf"
        Dim PdfReader As PdfReader = New PdfReader(pdfTemplate)
        Dim PdfStamper As PdfStamper = Nothing
        Dim ms As MemoryStream = Nothing


        Try
            ms = New MemoryStream()
            PdfStamper = New PdfStamper(PdfReader, ms)
            Dim pdfFormFields As AcroFields = PdfStamper.AcroFields
            With mie_dati
                pdfFormFields.SetField("n_documento", .n_documento)
                pdfFormFields.SetField("data_fattura", .data_fattura)
                pdfFormFields.SetField("cod_cliente", .cod_cliente)
                pdfFormFields.SetField("nome_cognome", .nome_cognome)
                pdfFormFields.SetField("indirizzo", .indirizzo)
                pdfFormFields.SetField("rif_ra", .rif_ra)
                pdfFormFields.SetField("cap", .cap)
                pdfFormFields.SetField("citta", .citta)
                pdfFormFields.SetField("rif_ric_fisc", .rif_ric_fisc)
                pdfFormFields.SetField("data_ric_fisc", .data_ric_fisc)
                pdfFormFields.SetField("codfiscale_piva", .codfiscale_piva)
                pdfFormFields.SetField("codice_fiscale", .codice_fiscale)
                pdfFormFields.SetField("modello", .modello)
                pdfFormFields.SetField("pickup", .pickup)
                pdfFormFields.SetField("dropoff", .dropoff)
                pdfFormFields.SetField("targa", .targa)
                pdfFormFields.SetField("data_partenza", .data_partenza)
                pdfFormFields.SetField("data_arrivo", .data_arrivo)
                pdfFormFields.SetField("conducente", .conducente)
                pdfFormFields.SetField("km_iniziali", .km_iniziali)
                pdfFormFields.SetField("km_finali", .km_finali)
                pdfFormFields.SetField("servizi", .servizi)
                pdfFormFields.SetField("quantita", .quantita)
                pdfFormFields.SetField("costo_unitario", .costo_unitario)
                pdfFormFields.SetField("totale", .totale)
                pdfFormFields.SetField("aliquota_iva", .aliquota_iva)
                pdfFormFields.SetField("pagamenti", .pagamenti)
                pdfFormFields.SetField("data_pagamenti", .data_pagamenti)
                pdfFormFields.SetField("modalita_pagamento", .modalita_pagamento)
                pdfFormFields.SetField("tipo_pagamento", .tipo_pagamento)
                pdfFormFields.SetField("importo", .importo)
                pdfFormFields.SetField("imponibile", .imponibile)
                pdfFormFields.SetField("aliquota", .aliquota)
                pdfFormFields.SetField("iva", .iva)
                pdfFormFields.SetField("totale_pagamenti", .totale_pagamenti)
                pdfFormFields.SetField("imponibile_1", .imponibile_1)
                pdfFormFields.SetField("iva_1", .iva_1)
                pdfFormFields.SetField("totale_fattura", .totale_fattura)
                pdfFormFields.SetField("a_saldo_fattura", .a_saldo_fattura)
                pdfFormFields.SetField("cod_cliente_talloncino", .cod_cliente_talloncino)
                pdfFormFields.SetField("ra_talloncino", .ra_talloncino)
                pdfFormFields.SetField("n_fattura_talloncino", .n_fattura_talloncino)
                pdfFormFields.SetField("n_voucher_talloncino", .n_voucher_talloncino)
                pdfFormFields.SetField("importo_1_talloncino", .importo_1_talloncino)
                pdfFormFields.SetField("importo_2_talloncino", .n_prenotazione_talloncino) 'ERRONEAMENTE NEL MODELLO IL CAMPO SI CHIAMA importo_2_talloncino
                pdfFormFields.SetField("addebito", .addebito)
                pdfFormFields.SetField("banche", .banche)
                pdfFormFields.SetField("abbuoni", .abbuoni)
                pdfFormFields.SetField("totale_fattura2", .totale_fattura_senza_abbuoni)
                pdfFormFields.SetField("pec", .pec)
                pdfFormFields.SetField("codice_sdi", .codice_sdi)
            End With
            PdfStamper.FormFlattening = True
            PdfStamper.Close()








            'Dim file As New FileStream(pathfile, FileMode.OpenOrCreate, FileAccess.Write)
            'ms.WriteTo(file)
            'File.Close()

            '# Invio email
            'dim sm as new sendmailcls 
            '    Dim mm As MailMessage = New MailMessage("ares_sbc@xinformatica.it", "dimatteo@xinformatica.it") With {
            '            .Subject = "subject",
            '            .IsBodyHtml = True,
            '            .Body = "body"
            '        }
            '    mm.Attachments.Add(New Attachment(ms, "filename.pdf"))
            '    Dim smtp As SmtpClient = New SmtpClient With {
            '    .Host = "smtp.xinformatica.it",
            '    .Port = 25,
            '    .EnableSsl = False,
            '    .Credentials = New NetworkCredential("ares_sbc@xinformatica.it", "Sbc!2020")
            '}
            '    smtp.Send(mm)

            'HttpContext.Current.ApplicationInstance.CompleteRequest()

            'End If

            Return True

        Catch ex As Exception
            HttpContext.Current.Trace.Write("Errore in GeneraDocumentoInvioEmail:" & ex.Message)
            Return False
        End Try


        'Return ms
    End Function



    Public Shared Function GeneraDocumento2(ByVal mie_dati As DatiStampaFatturaNolo) As MemoryStream
        Dim pdfTemplate As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\modelli_documenti\FatturaMulta.pdf"
        Dim PdfReader As PdfReader = New PdfReader(pdfTemplate)
        Dim PdfStamper As PdfStamper = Nothing
        Dim ms As MemoryStream = Nothing





        Try
            ms = New MemoryStream()
            PdfStamper = New PdfStamper(PdfReader, ms)
            Dim pdfFormFields As AcroFields = PdfStamper.AcroFields
            With mie_dati





                pdfFormFields.SetField("n_documento", .n_documento)
                pdfFormFields.SetField("data_fattura", .data_fattura)
                pdfFormFields.SetField("cod_cliente", .cod_cliente)
                pdfFormFields.SetField("nome_cognome", .nome_cognome)
                pdfFormFields.SetField("indirizzo", .indirizzo)
                pdfFormFields.SetField("rif_ra", .rif_ra)                   'da querystring 26.07.2022 salvo ex .rif_ra
                pdfFormFields.SetField("cap", .cap)
                pdfFormFields.SetField("citta", .citta)
                pdfFormFields.SetField("rif_ric_fisc", .rif_ric_fisc)
                pdfFormFields.SetField("data_ric_fisc", .data_ric_fisc)
                pdfFormFields.SetField("codfiscale_piva", .codfiscale_piva)
                pdfFormFields.SetField("codice_fiscale", .codice_fiscale)
                pdfFormFields.SetField("modello", .modello)
                pdfFormFields.SetField("pickup", .pickup)
                pdfFormFields.SetField("dropoff", .dropoff)
                pdfFormFields.SetField("targa", .targa)
                pdfFormFields.SetField("data_partenza", .data_partenza)
                pdfFormFields.SetField("data_arrivo", .data_arrivo)
                pdfFormFields.SetField("conducente", .conducente)
                pdfFormFields.SetField("km_iniziali", .km_iniziali)
                pdfFormFields.SetField("km_finali", .km_finali)
                pdfFormFields.SetField("servizi", .servizi)
                pdfFormFields.SetField("quantita", .quantita)
                pdfFormFields.SetField("costo_unitario", .costo_unitario)
                pdfFormFields.SetField("totale", .totale)
                pdfFormFields.SetField("aliquota_iva", .aliquota_iva)
                pdfFormFields.SetField("pagamenti", .pagamenti)
                pdfFormFields.SetField("data_pagamenti", .data_pagamenti)
                pdfFormFields.SetField("modalita_pagamento", .modalita_pagamento)
                pdfFormFields.SetField("tipo_pagamento", .tipo_pagamento)
                pdfFormFields.SetField("importo", .importo)
                pdfFormFields.SetField("imponibile", .imponibile)
                pdfFormFields.SetField("aliquota", .aliquota)
                pdfFormFields.SetField("iva", .iva)
                pdfFormFields.SetField("totale_pagamenti", .totale_pagamenti)
                pdfFormFields.SetField("imponibile_1", .imponibile_1)
                pdfFormFields.SetField("iva_1", .iva_1)
                pdfFormFields.SetField("totale_fattura", .totale_fattura)
                pdfFormFields.SetField("a_saldo_fattura", .a_saldo_fattura)
                pdfFormFields.SetField("cod_cliente_talloncino", .cod_cliente_talloncino)
                pdfFormFields.SetField("ra_talloncino", .ra_talloncino)
                pdfFormFields.SetField("n_fattura_talloncino", .n_fattura_talloncino)
                pdfFormFields.SetField("n_voucher_talloncino", .n_voucher_talloncino)
                pdfFormFields.SetField("importo_1_talloncino", .importo_1_talloncino)
                pdfFormFields.SetField("importo_2_talloncino", .n_prenotazione_talloncino) 'ERRONEAMENTE NEL MODELLO IL CAMPO SI CHIAMA importo_2_talloncino
                pdfFormFields.SetField("addebito", .addebito)
                pdfFormFields.SetField("banche", .banche)
                pdfFormFields.SetField("abbuoni", .abbuoni)
                pdfFormFields.SetField("totale_fattura2", .totale_fattura_senza_abbuoni)
                pdfFormFields.SetField("pec", .pec)
                pdfFormFields.SetField("codice_sdi", .codice_sdi)
            End With
            PdfStamper.FormFlattening = True
            PdfStamper.Close()

        Catch ex As Exception
            HttpContext.Current.Trace.Write("Errore in GeneraDocumento:" & ex.Message)
        End Try
        Return ms
    End Function

    Public Shared Function genera_dati_stampa_fattura(ByVal id_fattura As String, Optional ByVal num_contratto As String = "") As DatiStampaFatturaNolo


        Try

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM fatture_nolo WHERE id='" & id_fattura & "'", Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            Dim miei_dati As DatiStampaFatturaNolo = New DatiStampaFatturaNolo

            If Rs.Read() Then
                With miei_dati
                    .n_documento = Rs("num_fattura")
                    .data_fattura = Format(Rs("data_fattura"), "dd/MM/yyyy")
                    .cod_cliente = Rs("cod_edp")
                    .rif_ra = Rs("num_contratto_rif")
                    .nome_cognome = Rs("intestazione")
                    .indirizzo = Rs("indirizzo")
                    .cap = Rs("cap")
                    .citta = Rs("citta")
                    If (Rs("provincia") & "") <> "" Then
                        .citta = .citta & "   (" & Rs("provincia") & ")"
                    End If
                    .rif_ric_fisc = Rs("num_rf")
                    .data_ric_fisc = Format(Rs("data_rf"), "dd/MM/yyyy")
                    .codfiscale_piva = Rs("piva") & ""
                    .codice_fiscale = Rs("codice_fiscale") & ""
                    .modello = Rs("modello")
                    .pickup = Rs("stazione_uscita")
                    .dropoff = Rs("stazione_rientro")
                    .targa = Rs("targa")
                    .data_partenza = Rs("data_uscita")
                    .data_arrivo = Rs("data_rientro")
                    .conducente = Rs("conducente")
                    .km_iniziali = Rs("km_uscita")
                    .km_finali = Rs("km_rientro")
                    .totale_pagamenti = FormatNumber(Rs("totale_pagamenti"), 2, , , TriState.False)
                    .imponibile_1 = FormatNumber(Rs("imponibile"), 2, , , TriState.False)
                    .iva_1 = FormatNumber(Rs("iva"), 2, , , TriState.False)
                    .totale_fattura = FormatNumber(Rs("totale_fattura"), 2, , , TriState.False)
                    .a_saldo_fattura = FormatNumber(Rs("saldo"), 2, , , TriState.False)
                    .cod_cliente_talloncino = Rs("cod_edp")
                    .ra_talloncino = Rs("num_contratto_rif")
                    .importo_1_talloncino = FormatNumber(Rs("saldo"), 2, , , TriState.False)
                    .n_fattura_talloncino = Rs("num_fattura")
                    .n_voucher_talloncino = Rs("num_voucher")
                    .pec = Rs("email_pec") & ""
                    .codice_sdi = Rs("codice_sdi") & ""
                    .n_prenotazione_talloncino = Rs("num_prenotazione_rif")
                    Dim N_CONTRATTO_RIF As String = Rs("num_contratto_rif")
                    Dim N_PREN_RIF As String = Rs("num_prenotazione_rif") & ""

                    Dim is_prepagato As Boolean = Rs("fattura_costi_prepagati")

                    Dbc.Close()
                    Dbc.Open()

                    'ADDEBITO - BANCHE
                    Cmd = New Data.SqlClient.SqlCommand("SELECT testo FROM fatture_nolo_stampa WHERE tipo='1'", Dbc)
                    .addebito = Cmd.ExecuteScalar & ""

                    Cmd = New Data.SqlClient.SqlCommand("SELECT testo FROM fatture_nolo_stampa WHERE tipo='2'", Dbc)
                    Rs = Cmd.ExecuteReader()

                    .banche = ""
                    Do While Rs.Read()
                        .banche = .banche & Rs("testo") & vbCrLf
                    Loop


                    Dbc.Close()
                    Dbc.Open()

                    'RIGHE DI FATTURA
                    Cmd = New Data.SqlClient.SqlCommand("SELECT * FROM fatture_nolo_righe WHERE id_fattura='" & id_fattura & "'", Dbc)
                    Rs = Cmd.ExecuteReader()

                    Do While Rs.Read()
                        .servizi = .servizi & Rs("descrizione") & vbCrLf
                        .quantita = .quantita & Rs("quantita") & vbCrLf
                        .costo_unitario = .costo_unitario & FormatNumber(Rs("costo_unitario"), 2, , , TriState.False) & vbCrLf
                        .totale = .totale & FormatNumber(Rs("totale"), 2, , , TriState.False) & vbCrLf
                        ' .aliquota_iva = .aliquota_iva & Rs("codice_iva") & "" & vbCrLf
                        If (Rs("aliquota_iva") & "") <> "" Then
                            If Rs("aliquota_iva") = "0" Then
                                .aliquota_iva = .aliquota_iva & "" & "" & vbCrLf
                            Else
                                .aliquota_iva = .aliquota_iva & Rs("aliquota_iva") & "%" & "" & vbCrLf
                            End If
                        Else
                            .aliquota_iva = .aliquota_iva & "22%" & "" & vbCrLf
                        End If
                    Loop

                    Dbc.Close()
                    Dbc.Open()

                    Cmd = New Data.SqlClient.SqlCommand("SELECT * FROM fatture_nolo_pagamenti WHERE id_fattura_nolo='" & id_fattura & "'", Dbc)
                    Rs = Cmd.ExecuteReader()

                    Do While Rs.Read()
                        .data_pagamenti = .data_pagamenti & Rs("data_pagamento") & vbCrLf
                        .modalita_pagamento = .modalita_pagamento & Rs("modalita_pagamento") & vbCrLf
                        .tipo_pagamento = .tipo_pagamento & Rs("tipo_pagamento") & vbCrLf
                        .importo = .importo & FormatNumber(Rs("importo"), 2, , , TriState.False) & vbCrLf
                    Loop
                    Dbc.Close()
                    Dbc.Open()

                    If is_prepagato Then
                        Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(SUM(ISNULL(PER_IMPORTO,0)),0) FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE (N_PREN_RIF='" & N_PREN_RIF & "') AND ((id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Attivo & "') OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Passivo & "'))", Dbc)
                        .abbuoni = FormatNumber(Cmd.ExecuteScalar, 2, , , TriState.False)
                    Else
                        Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(SUM(ISNULL(PER_IMPORTO,0)),0) FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE (N_CONTRATTO_RIF='" & N_CONTRATTO_RIF & "') AND ((id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Attivo & "') OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Passivo & "'))", Dbc)
                        .abbuoni = FormatNumber(Cmd.ExecuteScalar, 2, , , TriState.False)
                    End If


                    .totale_fattura_senza_abbuoni = FormatNumber(CDbl(.totale_fattura) + CDbl(.abbuoni), 2, , , TriState.False)
                    .a_saldo_fattura = FormatNumber(.totale_fattura_senza_abbuoni - (CDbl(.totale_pagamenti) - CDbl(.abbuoni)), 2, , , TriState.False)

                    Dbc.Close()
                    Dbc.Open()

                    If Not is_prepagato Then


                        'RIGA RDS
                        Dim sqlPag As String = "SELECT * FROM PAGAMENTI_EXTRA WITH(NOLOCK) LEFT JOIN MOD_PAG WITH(NOLOCK)" &
                        "ON pagamenti_extra.ID_ModPag=MOD_PAG.ID_ModPag LEFT JOIN POS_Funzioni WITH(NOLOCK) ON PAGAMENTI_EXTRA.id_pos_funzioni_ares=POS_Funzioni.id " &
                        "LEFT JOIN codici_contabili WITH(NOLOCK) ON pagamenti_extra.ID_ModPag=codici_contabili.modpag AND pagamenti_extra.ID_TipPag=codici_contabili.tippag " &
                    "WHERE pagamenti_extra.N_RDS_RIF IN (SELECT id_rds FROM veicoli_evento_apertura_danno WITH(NOLOCK) WHERE id_documento_apertura = '" & num_contratto & "') " &
                    "AND (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Chiusura & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Vendita & "' " &
                "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Pagamento_Contanti & "' " &
                "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso & "' " &
                "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Attivo & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Passivo & "' " &
                "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Deposito_su_RA & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso_su_RA & "') " &
                "AND NOT operazione_stornata='1' AND N_RDS_RIF IS NOT NULL "

                        Cmd = New Data.SqlClient.SqlCommand(sqlPag, Dbc)


                        Rs = Cmd.ExecuteReader()

                        Dim importo_rds As Double = 0
                        Dim iva_scorporata_rds As Double = 0
                        Dim imponibile_rds As Double = 0
                        Dim divisore_iva_rds As String = "1," & Costanti.iva_default

                        Do While Rs.Read()
                            importo_rds = importo_rds + CDbl(Rs("PER_IMPORTO"))
                        Loop

                        If importo_rds <> 0 Then
                            .servizi = .servizi & "Addebito danno + spese apertura pratica sinistro" & vbCrLf
                            .quantita = .quantita & "1" & vbCrLf

                            imponibile_rds = importo_rds / CDbl(divisore_iva_rds)
                            iva_scorporata_rds = importo_rds - imponibile_rds

                            .costo_unitario = .costo_unitario & FormatNumber(imponibile_rds, 2, , , TriState.False) & vbCrLf
                            .totale = .totale & FormatNumber(imponibile_rds, 2, , , TriState.False) & vbCrLf
                            .aliquota_iva = .aliquota_iva & "22%" & vbCrLf


                            .imponibile_1 = FormatNumber(CDbl(.imponibile_1) + imponibile_rds, 2, , , TriState.False)
                            .iva_1 = FormatNumber(CDbl(.iva_1) + iva_scorporata_rds, 2, , , TriState.False)
                            .totale_pagamenti = FormatNumber(CDbl(.totale_pagamenti) + importo_rds, 2, , , TriState.False)
                            .totale_fattura = FormatNumber(CDbl(.totale_fattura) + importo_rds, 2, , , TriState.False)
                            .totale_fattura_senza_abbuoni = FormatNumber(CDbl(.totale_fattura_senza_abbuoni) + importo_rds, 2, , , TriState.False)
                        End If
                    End If

                    If Pagamenti.is_complimentary("", .rif_ra) Then
                        .servizi = .servizi & " Cessione gratuita art.2 DPR 633/72 senza obbligo di rivalsa art.18 DPR 633/72 " & vbCrLf
                    End If
                End With
            End If

            genera_dati_stampa_fattura = miei_dati

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("error stampaFatturaNolo genera_dati_stampa_fattura")
        End Try

    End Function
End Class
