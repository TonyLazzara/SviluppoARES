Imports System.Net.Mail
Imports funzioni_comuni
Imports classi_pagamento
Imports Pagamenti
Imports iTextSharp

Imports iTextSharp.text.pdf
Imports iTextSharp.text.xml

Imports System.IO
Imports System
Imports System.Diagnostics.Trace

'salvo 08.03.2023 10.55

Partial Class preventivi
    Inherits System.Web.UI.Page

    Dim funzioni As New funzioni_comuni
    Dim cPagamenti As New Pagamenti


    Dim id_elires As Boolean
    Dim id_pplus As Boolean
    Dim id_rd As Boolean
    Dim id_rf As Boolean


    Protected Function MiaStringa() As String
        MiaStringa = "onLoad=""javascript:alert('hello world44');"""
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler anagrafica_conducenti.scegliConducente, AddressOf scegli_conduente
        AddHandler anagrafica_ditte.scegliDitta, AddressOf scegli_ditta

        Try
            If funzioni_comuni.sql_inj(Request.ServerVariables("SERVER_NAME") & Request.ServerVariables("PATH_INFO") & Request.ServerVariables("QUERY_STRING")) Then
                Response.Redirect("default.aspx")
            End If


            'AddHandler Scambio_Importo1.ScambioImportoClose, AddressOf ScambioImportoClose
            'AddHandler Scambio_Importo1.ScambioImportoTransazioneEseguita, AddressOf ScambioImportoTransazioneEseguita

            'Trace.Write("Page_Load-----------------------")
            'Dim htmlheader As String = CType(Me.Page.Controls(0), LiteralControl).Text
            'Trace.Write(htmlheader)
            'Trace.Write("Page_Load-----------------------")
            'htmlheader = htmlheader.Replace("<body", "<body onLoad=""javascript:alert('hello world33');"" ")

            ultimo_gruppo.Text = ""

            If Not Page.IsPostBack() Then

                lbl_msg_tariffe_diverse.Visible = False
                lbl_msg_tariffe_diverse.Text = ""
                lbl_msg_tariffe_diverse.BackColor = Drawing.Color.Transparent


                lblOrderBY_cnt.Text = " ORDER BY contratti.data_uscita DESC"
                lblOrderBY_pren.Text = " ORDER BY prenotazioni.prdata_out ASC, prenotazioni.ore_uscita ASC, prenotazioni.minuti_uscita ASC"
                lblOrderBY_prev.Text = " ORDER BY preventivi.data_uscita DESC, preventivi.ore_uscita DESC, preventivi.minuti_uscita DESC"

                txtoraPartenza.Attributes.Add("onchange", "document.getElementById('" & txtOraRientro.ClientID & "').value = document.getElementById('" & txtoraPartenza.ClientID & "').value;")

                '# multiple submit 23.03.2022
                btnSalvaPrenotazione.Attributes.Add("onclick", " this.disabled = true; " & Page.ClientScript.GetPostBackEventReference(btnSalvaPrenotazione, "") & ";")
                '@ multiple submit 23.03.2022

                'txtDaData.Attributes.Add("onchange", "document.getElementById('" & txtNumeroGiorni.ClientID & "').value = giorni_differenza(" & txtDaData.Text & ").value,document.getElementById('" & txtAData.ClientID & "').value);")
                'txtAData.Attributes.Add("onchange", "document.getElementById('" & txtNumeroGiorni.ClientID & "').value = giorni_differenza(document.getElementById('" & txtDaData.ClientID & "').value,document.getElementById('" & txtAData.ClientID & "').value);")

                setPadding("cerca")

                dropTipoDocumento.Focus()

                dropStazionePickUp.DataBind()
                dropStazioneDropOff.DataBind()
                dropTipoCliente.DataBind()
                dropPrenotazioniPrepagate.DataBind()
                cercaPrenotazioniRibaltamento.DataBind()
                dropCercaTipoCliente.DataBind()
                dropTipoDocumento.DataBind()
                cercaStazionePickUp.DataBind()
                dropGruppoDaConsegnare.DataBind()
                dropFonteCommissionabile.DataBind()

                'RECUPERO IL LIVELLO DI ACCESSO PER L'APPLICAZIONE DELLO SCONTO E PER L'OMAGGIABILITA' DEGLI ACCESSORI
                livello_accesso_sconto.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.ApplicareSconto)
                livello_accesso_omaggi.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.OmaggiareAccessori)
                livello_accesso_broker.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzareCostiBroker)
                livello_accesso_fatturazione.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Fatturazione)

                'SE LA PROVENIENZA E' DALLA PAGINA prenotazioni.aspx SETTO I PARAMETRI DI RICERCA PRECEDENTI
                If Session("provenienza") = "prenotazioni.aspx" Then
                    dropTipoDocumento.SelectedValue = Session("tipo_documento")
                    Session("tipo_documento") = ""

                    If Session("errore_pren") <> "" Then
                        Libreria.genUserMsgBox(Me, Session("errore_pren"))
                        Session("errore_pren") = ""
                    End If

                    If Session("num_pre_staz") <> "" Then
                        txtCercaNumStaz.Text = Session("num_pre_staz")
                        Session("num_pre_staz") = ""
                    End If

                    If Session("num_pre_num") <> "" Then
                        txtCercaNumInterno.Text = Session("num_pre_num")
                        Session("num_pre_num") = ""
                    End If

                    If Session("riferimento") <> "" Then
                        txtCercaRiferimento.Text = Session("riferimento")
                        Session("riferimento") = ""
                    End If
                    If Session("nome") <> "" Then
                        txtCercaNome.Text = Session("nome")
                        Session("nome") = ""
                    End If
                    If Session("cognome") <> "" Then
                        txtCercaCognome.Text = Session("cognome")
                        Session("cognome") = ""
                    End If
                    If Session("pickUpDa") <> "" Then
                        txtCercaPickUpDa.Text = Session("pickUpDa")
                        Session("pickUpDa") = ""
                    End If
                    If Session("pickUpA") <> "" Then
                        txtCercaPickUpA.Text = Session("pickUpA")
                        Session("pickUpA") = ""
                    End If
                    If Session("dropOffDa") <> "" Then
                        txtCercaDropOffDa.Text = Session("dropOffDa")
                        Session("dropOffDa") = ""
                    End If
                    If Session("dropOffA") <> "" Then
                        txtCercaDropOffA.Text = Session("dropOffA")
                        Session("dropOffA") = ""
                    End If
                    If Session("stazRientro") <> "" Then
                        cercaStazioneDropOff.SelectedValue = Session("stazRientro")
                        Session("stazRientro") = ""
                    End If
                    If Session("stazUscita") <> "" Then
                        cercaStazionePickUp.SelectedValue = Session("stazUscita")
                        Session("stazUscita") = ""
                    End If
                    If Session("prepag") <> "" Then
                        dropPrenotazioniPrepagate.SelectedValue = Session("prepag")
                        Session("prepag") = ""
                    End If
                    If Session("pren_rib") <> "" Then
                        cercaPrenotazioniRibaltamento.SelectedValue = Session("pren_rib")
                        Session("pren_rib") = ""
                    End If
                    If Session("pren_tipo_cliente") <> "" Then
                        dropCercaTipoCliente.SelectedValue = Session("pren_tipo_cliente")
                    End If

                    dropStatoPrenotazione.SelectedValue = Session("statoDoc")
                    Session("statoDoc") = ""

                    Session("provenienza") = ""

                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, lblOrderBY_cnt.Text)

                ElseIf Session("provenienza") = "contratti.aspx" Then

                    dropTipoDocumento.SelectedValue = Session("tipo_documento")
                    Session("tipo_documento") = ""

                    dropStatoContratto.SelectedValue = Session("stato_documento")
                    Session("stato_documento") = ""

                    setta_pannello_ricerca()

                    If Session("riferimento") <> "" Then
                        txtCercaRiferimento.Text = Session("riferimento")
                        Session("riferimento") = ""
                    End If
                    If Session("nome") <> "" Then
                        txtCercaNome.Text = Session("nome")
                        Session("nome") = ""
                    End If
                    If Session("cognome") <> "" Then
                        txtCercaCognome.Text = Session("cognome")
                        Session("cognome") = ""
                    End If
                    If Session("pickUpDa") <> "" Then
                        txtCercaPickUpDa.Text = Session("pickUpDa")
                        Session("pickUpDa") = ""
                    End If
                    If Session("pickUpA") <> "" Then
                        txtCercaPickUpA.Text = Session("pickUpA")
                        Session("pickUpA") = ""
                    End If
                    If Session("dropOffDa") <> "" Then
                        txtCercaDropOffDa.Text = Session("dropOffDa")
                        Session("dropOffDa") = ""
                    End If
                    If Session("dropOffA") <> "" Then
                        txtCercaDropOffA.Text = Session("dropOffA")
                        Session("dropOffA") = ""
                    End If
                    If Session("stazRientro") <> "" Then
                        cercaStazioneDropOff.SelectedValue = Session("stazRientro")
                        Session("stazRientro") = ""
                    End If
                    If Session("stazUscita") <> "" Then
                        cercaStazionePickUp.SelectedValue = Session("stazUscita")
                        Session("stazUscita") = ""
                    End If
                    If Session("prepag") <> "" Then
                        dropPrenotazioniPrepagate.SelectedValue = Session("prepag")
                        Session("prepag") = ""
                    End If
                    If Session("pren_rib") <> "" Then
                        cercaPrenotazioniRibaltamento.SelectedValue = Session("pren_rib")
                        Session("pren_rib") = ""
                    End If
                    If Session("pren_tipo_cliente") <> "" Then
                        dropCercaTipoCliente.SelectedValue = Session("pren_tipo_cliente")
                        Session("pren_tipo_cliente") = ""
                    End If

                    If Session("pres_rientro") <> "" Then
                        cercaStazionePresuntoRientro.SelectedValue = Session("pres_rientro")
                        Session("pres_rientro") = ""
                    End If

                    If Session("pres_r_da") <> "" Then
                        txtCercaPresRDa.Text = Session("pres_r_da")
                        Session("pres_r_da") = ""
                    End If

                    If Session("pres_r_a") <> "" Then
                        txtCercaPresRA.Text = Session("pres_r_a")
                        Session("pres_r_a") = ""
                    End If

                    If Session("cnt_gruppo") <> "" Then
                        dropCercaGruppoContratto.DataBind()

                        dropCercaGruppoContratto.SelectedValue = Session("cnt_gruppo")
                        Session("cnt_gruppo") = ""
                    End If

                    If Session("cnt_targa") <> "" Then
                        txtCercaTargaContratto.Text = Session("cnt_targa")
                        Session("cnt_targa") = ""
                    End If

                    Session("provenienza") = ""

                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, lblOrderBY_cnt.Text)


                    lbl_new_tariffa.Text = "" 'azzero etichetta test label new tariffa salvo 01.12.2022




                Else '

                    'SE NON PROVENGO DA PRENOTAZIONI O CONTRATTI AZZERO QUESTI VALORI 
                    Session("stazUscita") = ""
                    Session("stazRientro") = ""
                    Session("tipo_documento") = ""
                    Session("provenienza") = ""
                    Session("errore_pren") = ""
                    Session("num_pre_staz") = ""
                    Session("num_pre_num") = ""
                    Session("riferimento") = ""
                    Session("nome") = ""
                    Session("cognome") = ""
                    Session("pickUpDa") = ""
                    Session("pickUpA") = ""
                    Session("dropOffDa") = ""
                    Session("dropOffA") = ""
                    Session("statoDoc") = ""
                    Session("prepag") = ""
                    Session("pres_rientro") = ""
                    Session("stato_documento") = ""
                    Session("pres_r_da") = ""
                    Session("pres_r_a") = ""
                    Session("cnt_gruppo") = ""
                    Session("cnt_targa") = ""

                    'listPreventivi.Visible = False
                    'listPrenotazioni.Visible = False

                    'NON PROVENENDO DA PRENOTAZIONI MOSTRO LE PRENOTAZIONI DELLA GIORNATA



                    If Request.QueryString("p") = "np" Then
                        EseguiNuovoPreventivo()
                    Else
                        If Request.Cookies("SicilyRentCar")("stazione") <> "88" Then
                            cercaStazionePickUp.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")

                            If cercaStazionePickUp.SelectedValue = "88" Then
                                cercaStazionePickUp.SelectedValue = "0"
                            End If

                            txtCercaPickUpDa.Text = Format(Now(), "dd/MM/yyyy")
                            txtCercaPickUpA.Text = Format(Now(), "dd/MM/yyyy")

                            'se Salvo Costa visualizza Contratti - aggiornato 14.09.2022 Salvo
                            If Request.Cookies("SicilyRentCar")("IdUtente") = "25" And Request.QueryString("idu") = "" Then

                                Response.Redirect("preventivi.aspx?idu=25")

                            ElseIf Request.QueryString("idu") = "25" Then 'se Salvo Costa Contratti Tutti - aggiornato 14.09.2022 Salvo

                                dropTipoDocumento.SelectedValue = "3"
                                setta_pannello_ricerca()
                                dropStatoContratto.SelectedIndex = 0
                                txtCercaPickUpDa.Text = ""
                                txtCercaPickUpA.Text = ""
                                cercaStazionePickUp.SelectedValue = "0"
                                ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, lblOrderBY_cnt.Text)

                            Else
                                ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                            End If

                        End If

                    End If

                End If

                'salvo 03.01.2023 prima apertura della pagina 
                Session("preventivo_load") = "1"
                listVecchioCalcolo.Visible = False
                '@


                'Aggiorna lista degli anni - salvo 11.01.2023 
                GetList_anno(0)


            Else   'page postback




                'DOPO AVER FATTO UN CERCA...
                If divPrenotazioni.Visible Then
                    sqlPrenotazioni.SelectCommand = query_cerca_pren.Text


                ElseIf divPreventivi.Visible Then

                    sqlPreventivi.SelectCommand = query_cerca_prev.Text

                    'da lente 14.04.2023 salvo
                    '# verifica etichetta sconto applicabile salvo 07.04.2023
                    If txtSconto.Text <> "" Then
                        If CDbl(txtSconto.Text) > 0 Then 'se sconto passato nel campo
                            If lbl_Importo_Sconto.Text <> "" Then
                                If CDbl(lbl_Importo_Sconto.Text) = 0 Then 'se sconto calcolato=0 
                                    'imposta msg a nessuno sconto
                                    lblMxSconto.Text = "Nessuno sconto applicabile"
                                    lblMxSconto.ForeColor = Drawing.Color.Green
                                    lblMxSconto.Visible = True
                                Else
                                    lblMxSconto.Text = "Applicato il massimo sconto " & txtSconto.Text & "%"
                                    lblMxSconto.Visible = True
                                End If
                            Else
                                lblMxSconto.Text = "Applicato il massimo sconto " & txtSconto.Text & "%"
                                lblMxSconto.Visible = True
                            End If
                        Else



                            lblMxSconto.Text = ""  'se campo sconto vuoto o zero nessun testo

                        End If
                    End If
                    '@end salvo

                ElseIf divContratti.Visible Then
                        sqlContratti.SelectCommand = query_cerca_cnt.Text
                End If

                'rimosso temporaneamente x test 21.04.2023
                '# salvo 24.01.2023 - richiama per il vecchio calcolo la precedente riga preventivi
                'sqlUltimoPreventiviCosti.SelectCommand = sqlUltimoPreventiviCosti.SelectCommand
                'listVecchioCalcolo.DataBind()
                '@end 


            End If

            dropStazionePickUp.Attributes.Add("onChange", "return copia_valore(this);")

            If Request.Cookies("SicilyRentCar")("idUtente") = "5" Then
                lbl_Importo_Sconto.Visible = True
                lbl_message.Visible = True
            Else
                lbl_Importo_Sconto.Visible = False
                lbl_message.Visible = False
            End If

            'disabilitato perchè la visualizzazione è automatica - salvo 21.04.2023
            btnVediUltimoCalcolo.Visible = False




        Catch ex As Exception
            HttpContext.Current.Response.Write("error load " & ex.Message & "<br/><br/>")

        End Try

        'salva_log()
    End Sub

    Protected Sub salva_log()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO utenti_clog (id_utente, nominativo, data, pagina, ip) VALUES ('" & Request.Cookies("SicilyRentCar")("IdUtente") & "',(SELECT ISNULL(cognome,'') + ' ' + ISNULL(nome,'') FROM operatori WHERE id='" & Request.Cookies("SicilyRentCar")("IdUtente") & "'),GetDate(),'" & Replace(Request.CurrentExecutionFilePath, "'", "''") & "','" & Replace(Request.UserHostAddress(), "'", "''") & "')", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub setPadding(ByVal tipo As String)
        'Trace.Write("setPadding--------------------------------> " & tabella_ricerca.Style.Item("clientHeight") & " - " & tabella_ricerca.Style.Item("offsetHeight"))
        'If tipo = "cerca" Then
        '     literal_padding_superiore.Text = "<div style='margin-top:0px;padding-top:55px;' >"
        '     literal_div_warning.Text = "<div style='margin-top:0px;padding-top:230px;' >"
        'ElseIf tipo = "preventivo" Then
        '    literal_padding_superiore.Text = "<div style='margin-top:0px;padding-top:0px;' >"
        '    literal_div_warning.Text = "<div style='margin-top:0px;padding-top:230px;' >"
        'ElseIf tipo = "preventivo_salva" Then
        '    literal_padding_superiore.Text = "<div style='margin-top:0px;padding-top:0px;' >"
        '    literal_div_warning.Text = "<div style='margin-top:0px;padding-top:30px;' >"
        'ElseIf tipo = "preventivo_continua_da_warning" Then
        '    literal_padding_superiore.Text = "<div style='margin-top:0px;padding-top:0px;' >"
        '    literal_div_warning.Text = "<div style='margin-top:0px;padding-top:260px;' >"
        'End If
    End Sub

    'Private Sub ScambioImportoClose(ByVal sender As Object, ByVal e As EventArgs)
    '    tab_pagamento.Visible = False
    '    tab_cerca_tariffe.Visible = True
    'End Sub

    'Public Sub ScambioImportoTransazioneEseguita(ByVal sender As Object, ByVal e As tabelle_pos_Scambio_Importo.TransazioneEseguitaEventArgs)
    '    tab_pagamento.Visible = False
    '    tab_cerca_tariffe.Visible = True
    '    Select Case e.Transazione.IDFunzione
    '        Case Is = enum_tipo_pagamento_ares.Richiesta
    '            'Dim tr As classi_pagamento.TransazionePreautorizzazione = e.Transazione
    '            'cPagamenti.registra_preautorizzazione(tr, numero_prenotazione.Text, "", "", "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
    '        Case Is = enum_tipo_pagamento_ares.Vendita
    '            'Dim tr As classi_pagamento.TransazioneVendita = e.Transazione
    '            'cPagamenti.registra_vendita(tr, numero_prenotazione.Text, "", "", "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
    '        Case Is = enum_tipo_pagamento_ares.Integrazione
    '            'Dim tr As classi_pagamento.TransazioneIntegrazione = e.Transazione
    '            'cPagamenti.registra_integrazione(tr, numero_prenotazione.Text, "", "", "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
    '        Case Is = enum_tipo_pagamento_ares.Chiusura
    '            'Dim tr As classi_pagamento.TransazioneChiusura = e.Transazione
    '            'cPagamenti.registra_chiusura(tr, numero_prenotazione.Text, "", "", "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
    '        Case Is = enum_tipo_pagamento_ares.Rimborso
    '            'Dim tr As classi_pagamento.TransazioneRimborso = e.Transazione
    '            'cPagamenti.registra_rimborso(tr, numero_prenotazione.Text, "", "", "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
    '        Case Is = enum_tipo_pagamento_ares.Storno_Ultima_Operazione
    '            'Dim tr As classi_pagamento.TransazioneStorno = e.Transazione
    '            'cPagamenti.registra_storno(tr, numero_prenotazione.Text, "", "", "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
    '    End Select

    '    'Libreria.genUserMsgBox(Me, "Ricevuto evento Transazione su terminal ID " & e.Transazione.TerminalID)
    'End Sub

    Private Sub scegli_ditta(ByVal sender As Object, ByVal e As anagrafica_anagrafica_ditte.ScegliDittaEventArgs)
        txtNomeDitta.Text = e.ragione_sociale
        id_ditta.Text = e.id_ditta

        Dim id_tariffe_righe As String = ""

        If dropTariffeGeneriche.SelectedValue <> "0" Then
            id_tariffe_righe = dropTariffeGeneriche.SelectedValue
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            id_tariffe_righe = dropTariffeParticolari.SelectedValue
        End If

        Dim sconto As Double
        If dropTipoSconto.SelectedValue = "0" Then
            sconto = CDbl(txtSconto.Text)
        ElseIf dropTipoSconto.SelectedValue = "1" Then
            sconto = 0
        End If

        Dim id_elemento_spese As String = funzioni_comuni.get_id_spese_spedizione_postali()

        If id_elemento_spese <> "0" Then
            Dim accessorio_esistente As Boolean = Not funzioni_comuni.accessorioExtraNonAggiunto(id_elemento_spese, id_gruppo_auto_scelto.Text, idPreventivo.Text, "", "", numCalcolo.Text)
            'SE LA DITTA COLLEGATA PREVEDE LA SPEDIZIONE POSTALE DELLA FATTURA AGGIUNGO IL COSTO 'SPEDE DI SPEDIZIONE POSTALI
            If e.metodo_spedizione = "P" And Not accessorio_esistente Then

                funzioni.aggiungi_accessorio_obbligatorio(id_elemento_spese, dropStazionePickUp.SelectedValue, dropStazioneDropOff.SelectedValue, id_gruppo_auto_scelto.Text, txtNumeroGiorni.Text, 0, False, Nothing, "", "", sconto, id_tariffe_righe, idPreventivo.Text, "", "", "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                listPreventiviCosti.DataBind()
            ElseIf e.metodo_spedizione <> "P" And accessorio_esistente Then
                'IN QUESTO CASO L'ACCESSORIO ERA STATO AGGIUNTO MA ADESSO L'AZIENDA SELEZIONATA NON RICHIEDE LA SPEDIZIONE PER POSTA
                Dim omaggiato As Boolean
                Dim id_ele As Label

                'CONTROLLO SE ERA STATO OMAGGIATO
                For i = 0 To listPreventiviCosti.Items.Count - 1
                    id_ele = listPreventiviCosti.Items(i).FindControl("id_elemento")
                    If id_ele.Text = id_elemento_spese Then
                        Dim chkOldOmaggio As CheckBox = listPreventiviCosti.Items(i).FindControl("chkOldOmaggio")
                        If chkOldOmaggio.Checked Then
                            omaggiato = True
                        Else
                            omaggiato = False
                        End If
                        Exit For
                    End If
                Next

                If Not omaggiato Then
                    funzioni.rimuovi_costo_accessorio(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppo_auto_scelto.Text, id_elemento_spese, "", "EXTRA", dropTipoCommissione.SelectedValue)
                Else
                    funzioni.omaggio_accessorio(False, False, True, idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppo_auto_scelto.Text, id_elemento_spese, "", dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, "", "")
                End If

                listPreventiviCosti.DataBind()
            End If
        End If

        anagrafica_ditte.Visible = False
    End Sub

    Private Sub scegli_conduente(ByVal sender As Object, ByVal e As anagrafica_anagrafica_conducenti.ScegliConducenteEventArgs)
        'IN QUESTA PAGINA SI PUO' SCEGLIERE IL CONDUCENTE PRIMA DI SALVARE LA PRENOTAZIONE.
        id_conducente.Text = e.id_conducente
        txtNomeConducente.Text = e.nome_conducente
        txtNomeConducente.ReadOnly = True
        txtCognomeConducente.Text = e.cognome_conducente
        txtCognomeConducente.ReadOnly = True
        txtMailConducente.Text = e.email_conducente

        txtIndirizzoConducente.Text = e.indirizzo_conducente

        txtDataDiNascita.Text = e.data_nascita
        txtDataDiNascita.ReadOnly = True

        'SE E' STATO SELEZIONATO UN UTENTE E' NECESSARIO CONTROLLARNE L'ETA' E SE DEVE ESSERE AGGIUNTO/RIMOSSO LO YOUNG DRIVER -----------
        If txtDataDiNascita.Text <> "" Then
            Dim test_eta As Integer
            Dim month_nascita As Integer = Month(txtDataDiNascita.Text)
            Dim day_nascita As Integer = Day(txtDataDiNascita.Text)
            Dim data_nascita As DateTime = funzioni_comuni.getDataDb_con_orario2(txtDataDiNascita.Text & " 00:00:00")

            test_eta = DateDiff(DateInterval.Year, data_nascita, Now())

            If Month(Now()) < month_nascita Then
                test_eta = CInt(test_eta) - 1
            ElseIf Month(Now()) = month_nascita And Day(Now()) < day_nascita Then
                test_eta = CInt(test_eta) - 1
            End If

            'SE L'ETA' E' LA STESSA SEGNALATA AD INIZIO PROCEDURA NULLA DEVE ESSERE FATTO
            If CStr(test_eta) <> txtEtaPrimo.Text Then
                Dim check_eta As String = funzioni_comuni.gruppo_vendibile_eta_guidatori(id_gruppo_auto_scelto.Text, test_eta, "", "", "", "", "", "", "", False)
                If check_eta = "0" Then
                    'L'AUTO NON E' VENDIBILE - NON E' POSSIBILE COLLEGARE IL GUIDATORE ALLA PRENOTAZIONE DA SALVARE
                    id_conducente.Text = ""

                    txtNomeConducente.ReadOnly = False
                    txtNomeConducente.Text = ""

                    txtCognomeConducente.ReadOnly = False
                    txtCognomeConducente.Text = ""

                    txtMailConducente.Text = ""
                    txtIndirizzoConducente.Text = ""

                    txtDataDiNascita.ReadOnly = False
                    txtDataDiNascita.Text = ""

                    Libreria.genUserMsgBox(Me, "Impossibile selezionare il guidatore scelto: gruppo auto non vendibile a causa dell'età.")
                ElseIf check_eta = "1" Then
                    'IN QUESTO CASO IL GRUPPO AUTO E' VENDIBILE MA CON SUPPLEMENTO YOUNG DRIVER CHE DEVE ESSERE AGGIUNTO AUTOMATICAMENTE
                    nuovo_accessorio(get_id_young_driver(), id_gruppo_auto_scelto.Text, "YOUNG PRIMO", test_eta, "")

                    listPreventiviCosti.DataBind()

                    txtEtaPrimo.Text = test_eta
                    Libreria.genUserMsgBox(Me, "Gruppo auto vendibile con supplemento Young Driver.")
                ElseIf check_eta = "4" Then
                    'VENDIBILE SENZA LO YOUNG DRIVER - QUALORA FOSSE STATO PRECEDENTEMENTE AGGIUNTO PROVVEDO ALLA SUA RIMOZIONE
                    txtEtaPrimo.Text = test_eta
                    If funzioni.esiste_young_driver_primo_guidatore(get_id_young_driver(), id_gruppo_auto_scelto.Text, numCalcolo.Text, idPreventivo.Text, "", "", "") Then
                        funzioni.rimuovi_costo_accessorio(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppo_auto_scelto.Text, get_id_young_driver(), "1", "EXTRA", dropTipoCommissione.SelectedValue)
                        listPreventiviCosti.DataBind()
                        Libreria.genUserMsgBox(Me, "Rimosso il costo dello Young Driver per il primo guidatore.")
                    End If
                End If
            End If
        End If

        '---------------------------------------------------------------------------------------------------------------------------------
        anagrafica_conducenti.Visible = False
    End Sub

    Protected Sub elimina_warning(ByVal idPreventivo As String, ByVal numCalcolo As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM preventivi_warning WHERE id_documento='" & idPreventivo & "' AND num_calcolo='" & numCalcolo & "'", Dbc)

        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Function cerca_fase1() As Integer
        'PRIMA FASE DELLA RICERCA TARIFFA - DATA/STAZIONE/FONTE
        'RESTITUISCE 1 SE SONO PRESENTI DEI WARNING - RESTITUISCE 0 SE NON E' STATO GENERATO ALCUN WARNING
        txtSconto.Text = ""

        fonte_stop_sell.Visible = False

        Dim daData As String = txtDaData.Text
        Dim aData As String = txtAData.Text


        Try
            Dim data_pick_up_db As String = funzioni_comuni.getDataDb_senza_orario(daData, Request.ServerVariables("HTTP_HOST"))

            Dim stazione_permette_VAL_verso_altre_stazioni As Boolean = True
            Dim stazione_accetta_VAL_da_altre_stazioni As Boolean = True

            Dim num_gruppi_selezionati As Integer = 0

            'CONTROLLO SU APERTURA PICK UP
            Dim stazione_aperta_pick_up As String = funzioni_comuni.stazione_aperta_pick_up(dropStazionePickUp.SelectedValue, daData, ore1.Text, minuti1.Text, idPreventivo.Text, "", "", "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))

            Dim stazione_aperta_drop_off As String = funzioni_comuni.stazione_aperta_drop_off(dropStazioneDropOff.SelectedValue, aData, ore2.Text, minuti2.Text, idPreventivo.Text, "", "", "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))

            Dim pick_up_on_request As String = funzioni_comuni.stazione_pick_up_on_request(dropStazionePickUp.SelectedValue, idPreventivo.Text, "", "", "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
            Dim drop_off_on_request As String = funzioni_comuni.stazione_drop_off_on_request(dropStazioneDropOff.SelectedValue, idPreventivo.Text, "", "", "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))

            'CONTROLLO EVENTUALE VAL 
            If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
                stazione_permette_VAL_verso_altre_stazioni = funzioni_comuni.stazione_permette_VAL_verso_altre_stazioni(dropStazionePickUp.SelectedValue, idPreventivo.Text, "", "", "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
                stazione_accetta_VAL_da_altre_stazioni = funzioni_comuni.stazione_accetta_VAL_da_altre_stazioni(dropStazioneDropOff.SelectedValue, idPreventivo.Text, "", "", "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
            End If

            'CONTROLLO SE LA STAZIONE DI PICK UP O IL SINGOLO GRUPPO SONO IN STOP SELL
            Dim stazione_in_stop_sell As Boolean = funzioni_comuni.stazioneInStopSell(dropStazionePickUp.SelectedValue, data_pick_up_db, idPreventivo.Text, "", "", "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))

            'SE E' STATA SELEZIONATA UNA FONTE CONTROLLO SE E' IN STOP SELL
            Dim fonte_in_stop_sell As Boolean
            If dropTipoCliente.SelectedValue <> "0" Then
                fonte_in_stop_sell = funzioni_comuni.fonteInStopSell(dropStazionePickUp.SelectedValue, dropTipoCliente.SelectedValue, data_pick_up_db, idPreventivo.Text, "", "", "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))

                If fonte_in_stop_sell Then
                    fonte_stop_sell.Visible = True
                End If
            End If

            If CInt(stazione_aperta_pick_up) < 2 Or CInt(stazione_aperta_drop_off) < 2 Or Not stazione_permette_VAL_verso_altre_stazioni Or Not stazione_accetta_VAL_da_altre_stazioni Or stazione_in_stop_sell Or fonte_in_stop_sell Then
                cerca_fase1 = 1
            Else
                cerca_fase1 = 0
            End If

            If idPreventivo.Text <> "" Then
                listWarningPickPreventivi.DataBind()
                listWarningDropPreventivi.DataBind()
            End If

        Catch ex As Exception
            HttpContext.Current.Response.Write("error cerca_fase1  : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try




    End Function

    Protected Sub setPrecedentiGruppiETariffe()
        'SELEZIONE DELL'EVENTUALE TARIFFA SCELTA NELL'ULTIMA RICERCA ---------------------------------------------------------
        If tariffa_ultima_ricerca.Text <> "" Then
            Try
                dropTariffeGeneriche.SelectedValue = tariffa_ultima_ricerca.Text
            Catch ex As Exception

            End Try
            If dropTariffeGeneriche.SelectedValue = "0" Then
                Try
                    dropTariffeParticolari.SelectedValue = tariffa_ultima_ricerca.Text
                Catch ex As Exception

                End Try
            End If

            tariffa_ultima_ricerca.Text = ""
        End If
        '---------------------------------------------------------------------------------------------------------------------
        'SELEZIONE DEL GRUPPO VELOCE -----------------------------------------------------------------------------------------
        If gruppoVeloce.SelectedValue <> "0" Then
            For i = 0 To listGruppi.Items.Count - 1
                Dim id_gruppo As Label = listGruppi.Items(i).FindControl("id_gruppo")
                Dim sel_gruppo As CheckBox = listGruppi.Items(i).FindControl("sel_gruppo")
                If id_gruppo.Text = gruppoVeloce.Text Then
                    If sel_gruppo.Enabled Then
                        sel_gruppo.Checked = True
                    Else
                        sel_gruppo.Checked = False
                    End If
                Else
                    sel_gruppo.Checked = False
                End If
            Next
        End If
        'gruppoVeloce.SelectedValue = "0"
        '----------------------------------------------------------------------------------------------------------------------
        'SELEZIONE DEGLI EVENTUALI GRUPPI DELL'ULTIMA RICERCA - A MENO CHE NON E' STATO RICHIESTO SPECIFICATAMENTE UN GRUPPO NELLA PRIMA FASE DI RICERCA
        If gruppi_ultima_ricerca.Text <> "" And gruppoVeloce.SelectedValue = "0" Then
            Dim lista_ultimi_gruppi() As String = Split(gruppi_ultima_ricerca.Text, "-")

            For i = 0 To lista_ultimi_gruppi.Length - 1
                For j = 0 To listGruppi.Items.Count - 1
                    Dim gruppo As Label = listGruppi.Items(j).FindControl("gruppo")
                    If gruppo.Text = lista_ultimi_gruppi(i) Then
                        Dim sel_gruppo As CheckBox = listGruppi.Items(j).FindControl("sel_gruppo")
                        If sel_gruppo.Enabled Then
                            sel_gruppo.Checked = True
                        End If
                        Exit For
                    End If
                Next
            Next

            gruppi_ultima_ricerca.Text = ""
        End If
        '----------------------------------------------------------------------------------------------------------------------
    End Sub

    Protected Sub cercaPreventivoFase1()

        Dim err_n As Integer = 0
        Dim ricerca As Integer

        Try

            ricerca = cerca_fase1()

            'Response.Write("ricerca=" & ricerca.ToString & "<br/>")
            'Response.End()

            err_n = 1

            div_dettaglio_gruppi.Visible = True


            Dim broker As Boolean = False
            Dim agenzia As Boolean = False

            If ricerca = 1 Then
                'SE CI SONO WARNING
                table_warning.Visible = True
                table_gruppi.Visible = False
                table_tariffe.Visible = False

                'setPadding("preventivo")
            ElseIf ricerca = 0 Then
                'SE NON CI SONO WARNING
                table_warning.Visible = False
                setQueryTariffePossibili(0)
                err_n = 2
                table_gruppi.Visible = True
                table_tariffe.Visible = True

                listGruppi.DataBind()
                err_n = 3
                'SE LA TIPOLOGIA DI CLIENTE E' UN'AGENZIA DI VIAGGIO VENGONO MOSTRATE LE FONTI COMMISSIONABILI 
                If dropTipoCliente.SelectedValue <> "0" Then
                    broker = cliente_is_broker(dropTipoCliente.SelectedValue)
                    If Not broker Then
                        agenzia = cliente_is_agenzia_di_viaggio(dropTipoCliente.SelectedValue)
                    End If
                    'SE IL CLIENTE E' UN'AGENZIA DI VIAGGIO
                    If agenzia Then
                        lblFonteCommissionabile.Visible = True
                        dropFonteCommissionabile.Visible = True
                        dropFonteCommissionabile.Enabled = True
                        dropTipoCommissione.Visible = True
                        dropTipoCommissione.Enabled = True
                        txtPercentualeCommissionabile.Visible = True
                        lblPercentualeCommissionabile.Visible = True
                        btnAggiornaFontiCommissionabili.Visible = True
                        'dropTariffeGeneriche.Enabled = False
                        dropTariffeGeneriche.Enabled = True
                    Else
                        lblFonteCommissionabile.Visible = False
                        dropFonteCommissionabile.Visible = False
                        dropFonteCommissionabile.SelectedValue = "0"
                        dropTipoCommissione.Visible = False
                        dropTipoCommissione.SelectedValue = "0"
                        txtPercentualeCommissionabile.Visible = False
                        txtPercentualeCommissionabile.Text = ""
                        lblPercentualeCommissionabile.Visible = False
                        btnAggiornaFontiCommissionabili.Visible = False
                        dropTariffeGeneriche.Enabled = True
                    End If
                    'SE IL CLIENTE E' UN BROKER
                    err_n = 4
                    If broker Then
                        'SE NON CI SONO TARIFFE PARTICOLARI VENDIBILI E' POSSIBILE SALVARE LA PRENOTAZIONE SENZA TARIFFA
                        If dropTariffeParticolari.Items.Count = 1 Then
                            btnSalvaPrenNoTariffa.Visible = True
                        Else
                            btnSalvaPrenNoTariffa.Visible = False
                        End If
                    Else
                        btnSalvaPrenNoTariffa.Visible = False
                    End If
                Else
                    err_n = 5
                    lblFonteCommissionabile.Visible = False
                    dropFonteCommissionabile.Visible = False
                    dropFonteCommissionabile.SelectedValue = "0"
                    dropTipoCommissione.Visible = False
                    dropTipoCommissione.SelectedValue = "0"
                    txtPercentualeCommissionabile.Visible = False
                    txtPercentualeCommissionabile.Text = ""
                    lblPercentualeCommissionabile.Visible = False
                    btnAggiornaFontiCommissionabili.Visible = False
                    dropTariffeGeneriche.Enabled = True
                    btnSalvaPrenNoTariffa.Visible = False
                End If
                err_n = 6
                'SE SIAMO IN QUESTO STATO SELEZIONO GLI EVENTUALI GRUPPI E TARIFFE SCELTE PRECEDENTEMENTE
                setPrecedentiGruppiETariffe()
            End If
            err_n = 7
            'DISABILITO I PULSANTI PER LA RICERCA DELLA TARIFFA ---------------------------------------------------------------------------------
            btnCerca.Visible = False
            dropStazionePickUp.Enabled = False
            dropStazioneDropOff.Enabled = False
            txtoraPartenza.Enabled = False
            txtOraRientro.Enabled = False
            minuti1.Enabled = False
            ore1.Enabled = False
            ore2.Enabled = False
            minuti2.Enabled = False
            dropTipoCliente.Enabled = False
            txtDaData.Enabled = False
            txtAData.Enabled = False
            txtEtaPrimo.Enabled = False
            txtEtaSecondo.Enabled = False
            txtNumeroGiorni.Enabled = False
            txtCodiceCliente.Enabled = False
            btnAnnulla0.Visible = False
            gruppoVeloce.Enabled = False
            '-----------------------------------------------------------------------------------------------------------------------------------
            err_n = 8
            If ricerca = 0 Then
                Dim sel_gruppo As CheckBox
                Dim old_sel As CheckBox

                'SE IN QUESTA FASE TARIFFE E GRUPPI SONO GIA' STATI SELEZIONATI ALLORA PASSO DIRETTAMENTE ALLA RICERCA DEI COSTI
                Dim almeno_un_gruppo_selezionato As Boolean = False
                For j = 0 To listGruppi.Items.Count - 1
                    sel_gruppo = listGruppi.Items(j).FindControl("sel_gruppo")
                    old_sel = listGruppi.Items(j).FindControl("old_sel_gruppo")
                    If sel_gruppo.Checked Then
                        almeno_un_gruppo_selezionato = True

                        If sel_gruppo.Checked Then
                            old_sel.Checked = True
                        Else
                            old_sel.Checked = False
                        End If
                    End If
                Next
                err_n = 9

                If txtCodiceCliente.Text <> "" And dropTipoCliente.SelectedValue <> "0" Then
                    'E' STATO SPECIFICATO UN CODICE CLIENTE (EDP) E IL TIPO CLIENTE E' STATO TROVAO (NON E' GENERICO) - SE E' PRESENTE E SE E' E' 
                    'PRESENTE UNA SOLA TARIFFA PARTICOLARE LA SELEZIONO IN MODO DA POTER SALTARE ALLA FASE SUCCESSIVA QUALORA SIA ANCHE STATO
                    'SELEZIONATO UN GRUPPO ATUO
                    If dropTariffeParticolari.Items.Count = 2 Then
                        dropTariffeParticolari.Items(1).Selected = True
                    End If
                ElseIf dropTipoCliente.SelectedValue <> "0" And Not agenzia Then
                    'TRANNE NEL CASO DI AGENZIA DI VIAGGIO (PER CUI DEVE ESSERE SPECIFICATA LA FONTE COMMISSIONABILE) POSSO PASSARE LA PRIMA FASE SE ESISTE UNA SOLA TARIFFA PARTICOLARE
                    If dropTariffeParticolari.Items.Count = 2 Then
                        dropTariffeParticolari.Items(1).Selected = True
                    End If
                End If
                err_n = 10
                If (almeno_un_gruppo_selezionato) And (dropTariffeGeneriche.SelectedValue <> "0" Or dropTariffeParticolari.SelectedValue <> "0") Then
                    setPadding("preventivo")
                    vedi_tariffe()
                End If
            End If
            err_n = 11
        Catch ex As Exception
            HttpContext.Current.Response.Write("error cercaPreventivoFase1 : <br/>" & err_n & "<br/>" & ex.Message & "<br/>" & "<br/>")
        End Try





    End Sub

    Protected Function getTipoCliente_SetNomeDitta(ByVal codEDP As String) As String
        Dim sqla As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            sqla = "SELECT Rag_Soc FROM ditte WITH(NOLOCK) WHERE [CODICE EDP]='" & codEDP & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim nome_ditta As String = Cmd.ExecuteScalar & ""

            If nome_ditta = "" Then
                'CODICE EDP NON ESISTENTE
                getTipoCliente_SetNomeDitta = "X"
            Else
                'CODICE EDP ESISTENTE - SELEZIONO (SE VALORIZZATO) LA TIPOLOGIA DI CLIENTE ASSOCIATA AL CLIENTE
                sqla = "SELECT id_tipo_cliente FROM ditte WITH(NOLOCK) WHERE [CODICE EDP]='" & codEDP & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)

                lblNomeDitta.Text = nome_ditta
                getTipoCliente_SetNomeDitta = Cmd.ExecuteScalar & ""
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error getTipoCliente_SetNomeDitta  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerca.Click

        '# reset lbl e session aggiunto salvo 19.01.2023
        lbl_valore_senza_sconto.Text = ""
        HttpContext.Current.Session("valore_preventivo") = ""
        lbl_valore_con_sconto.Text = ""
        HttpContext.Current.Session("valore_preventivo_finale") = ""
        lbl_sconti_tariffe.Text = ""
        HttpContext.Current.Session("perc_sconto_tariffa_tutte") = ""
        HttpContext.Current.Session("list_tariffe") = ""
        HttpContext.Current.Session("list_tariffe_tempoKM") = ""
        lbl_list_tariffe.Text = ""
        lbl_list_tariffe_tempoKM.Text = ""
        txt_sconto_new.Text = "0"
        txtSconto.Text = "0"
        lbl_imp_sconto.Text = "0"

        '# salvo 01.08.2023 aggiunto - reset session in caso di cambio tariffa
        HttpContext.Current.Session("cambiatariffanp") = ""
        '@ end salvo 01.08.2023


        'solo x test visibili
        Dim LblVisibile As Boolean = False

        If Request.Cookies("SicilyRentCar")("idUtente") = "5" Then
            LblVisibile = True
        End If

        lbl_valore_senza_sconto.Visible = LblVisibile
        lbl_valore_con_sconto.Visible = LblVisibile
        lbl_sconti_tariffe.Visible = LblVisibile
        lbl_imp_sconto.Visible = LblVisibile
        lbl_list_tariffe.Visible = LblVisibile
        lbl_list_tariffe_tempoKM.Visible = LblVisibile


        txtSconto.Visible = False 'campo sconto precedente che sarà sempre a zero
        txtSconto.Text = "0"
        '@end  reset lbl e session aggiunto salvo 19.01.2023


        'CONTROLLO CHE LA DATA DI USCITA E' UGUALE A SUCCESSIVA ALLA DATA ODIERNA
        Dim err_n As Integer = 0
        Try
            Dim uscita As DateTime = funzioni_comuni.getDataDb_con_orario2(txtDaData.Text & " 00:00:00")
            err_n = 1
            Dim rientro As DateTime = funzioni_comuni.getDataDb_con_orario2(txtAData.Text & " 23:59:59")
            err_n = 2
            Dim oggi As DateTime = funzioni_comuni.getDataDb_con_orario2(Year(Now()) & "-" & Month(Now()) & "-" & Day(Now()) & " 23:59:59")
            err_n = 3
            txtNumeroGiorni.Text = DateDiff(DateInterval.Day, uscita, rientro)
            err_n = 4
            If DateDiff(DateInterval.Day, oggi, uscita) >= 0 Then
                err_n = 5
                'CONTROLLO ANCHE CHE LA DATA FINALE SIA SUCCESSIVA A QUELLA INIZIALE
                If (DateDiff(DateInterval.Day, uscita, rientro) > 0) Or (DateDiff(DateInterval.Day, uscita, rientro) = 0 And Hour(txtOraRientro.Text) > Hour(txtoraPartenza.Text)) Or (DateDiff(DateInterval.Day, uscita, rientro) = 0 And Hour(txtOraRientro.Text) = Hour(txtoraPartenza.Text) And Minute(txtOraRientro.Text) > Minute(txtoraPartenza.Text)) Then
                    Dim tipo_cliente As String = ""
                    err_n = 6
                    If txtCodiceCliente.Text <> "" Then
                        tipo_cliente = getTipoCliente_SetNomeDitta(txtCodiceCliente.Text)
                    Else
                        lblNomeDitta.Text = ""
                    End If
                    err_n = 7
                    If tipo_cliente <> "X" Then
                        If tipo_cliente <> "" Then
                            'IN QUESTO CASO E' STATO SPECIFICATO IL CODICE CLIENTE ED IL CLIENTE IN EFFETTI E' COLLEGATO AD UNA TARIFFA PARTICOLARE -
                            dropTipoCliente.SelectedValue = tipo_cliente
                        ElseIf tipo_cliente = "" And txtCodiceCliente.Text <> "" Then
                            'IN QUESTO CASO E' STATO SPECIFICATO IL CODICE CLIENTE MA QUESTI NON HA UNA TARIFFA ASSOCIATA - PER EVITARE ERRORI
                            'SELEZIONO "CLIENTE GENERICO"
                            dropTipoCliente.SelectedValue = "0"
                        End If
                        err_n = 8
                        ore1.Text = Hour(txtoraPartenza.Text)
                        minuti1.Text = Minute(txtoraPartenza.Text)
                        err_n = 9
                        ore2.Text = Hour(txtOraRientro.Text)
                        minuti2.Text = Minute(txtOraRientro.Text)
                        err_n = 10
                        If tipo_preventivo.Text = "nuovo" Then
                            If Len(ore1.Text) = 1 Then
                                ore1.Text = "0" & ore1.Text
                            End If
                            If Len(ore2.Text) = 1 Then
                                ore2.Text = "0" & ore2.Text
                            End If
                            If Len(minuti1.Text) = 1 Then
                                minuti1.Text = "0" & minuti1.Text
                            End If
                            If Len(minuti2.Text) = 1 Then
                                minuti2.Text = "0" & minuti2.Text
                            End If
                            err_n = 11
                            'RICERCA PER UN NUOVO PREVENTIVO
                            cercaPreventivoFase1()
                            err_n = 12
                        End If
                    Else
                        lblNomeDitta.Text = ""
                        Libreria.genUserMsgBox(Me, "Codice cliente inesistente.")
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Attenzione: la data di rientro è precedente a quella di uscita.")
                End If
            Else
                Libreria.genUserMsgBox(Me, "Attenzione: la data di uscita è precedente alla data odierna.")
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error  btnCerca_Click " & ex.Message & "<br/>err_n:" & err_n & "<br/>")
        End Try



    End Sub

    Protected Sub setQueryTariffePossibili(ByVal id_prev As Integer)

        Dim sqla As String
        Dim err_n As Integer = 0

        Try
            'SE VIENE PASSATO UN id_tariffa ESEGUO LA RICERCA SOLAMENTE PER LA TARIFFA RICHIESTA (SERVE PER QUANDO SI RICHIAMA UN PREVENTIVO)
            Dim daData As String = funzioni_comuni.getDataDb_senza_orario(txtDaData.Text)
            Dim aData As String = funzioni_comuni.getDataDb_senza_orario(txtAData.Text)

            Dim condizione_id_prev As String = ""
            If id_prev <> 0 Then
                condizione_id_prev = " AND tariffe.id=" & id_prev
            End If

            'QUERY: MODIFICARE LA QUERY ANCHE IN PRENOTAZIONI - CONTRATTI (DENTRO LA FUNZIONE tariffa_vendibile)

            'PARTE 1: SELEZIONO LE TARIFFE GENERICHE (PER CUI TUTTE LE FONTI SONO POSSIBILI - TUTTE LE STAZIONI SONO POSSIBILI)
            'COME ID SELEZIONO DIRETTAMENTE LA RIGA DI tariffe_righe DA CUI POI POSSO SUBITO OTTENERE LA CONDIZIONE E IL TEMPO-KM
            'DA UTILIZZARE

            sqla = "(SELECT tariffe_righe.id,tariffe.codice FROM tariffe WITH(NOLOCK) " &
            "INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
            "WHERE tariffe.attiva='1' " & condizione_id_prev & " AND convert(datetime,'" & daData & "',102) BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " &
            "AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a " &
            "AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_X_stazioni WITH(NOLOCK) WHERE id_tariffa=tariffe.id) " &
            "AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_X_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id) " &
            "AND convert(datetime,'" & aData & "',102) <= ISNULL(max_data_rientro, '3000-12-12 23:59:59')) ORDER BY codice" 'MODIFICARE LA QUERY ANCHE IN RIBALTAMENTO.ASPX

            sqlTariffeGeneriche.SelectCommand = sqla

            'Response.Write(sqlTariffeGeneriche.SelectCommand)
            'Response.End()
            'PARTE 2: SELEZIONO LE TARIFFE VENDIBILI PER LA STAZIONE SPECIFICATA E PER L'EVENTUALE FONTE 
            'COME ID SELEZIONO DIRETTAMENTE LA RIGA DI tariffe_righe DA CUI POI POSSO SUBITO OTTENERE LA CONDIZIONE E IL TEMPO-KM
            'DA UTILIZZARE

            'SE SPECIFICATO, NEL CASO DI TARIFFA PARTICOLARE, SELEZIONO IL NOME PER FONTE
            Dim condizione_nome_tariffa_fonte As String = "NULL"
            If dropTipoCliente.SelectedValue > 0 Then
                sqla = "(SELECT nome_tariffa FROM tariffe_X_fonti WHERE tariffe_x_fonti.id_tariffa=tariffe.id AND id_tipologia_cliente='" & dropTipoCliente.SelectedValue & "')"
                condizione_nome_tariffa_fonte = sqla
            End If

            sqla = "SELECT tariffe_righe.id, ISNULL(" & condizione_nome_tariffa_fonte & ",tariffe.codice) As codice FROM tariffe WITH(NOLOCK) " &
            "INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
            "WHERE tariffe.attiva='1' " & condizione_id_prev & " AND convert(datetime,'" & daData & "',102) BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " &
            "AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a " &
            "AND convert(datetime,'" & aData & "',102) <= ISNULL(max_data_rientro, '3000-12-12 23:59:59') " &
            "AND ( (" &
            "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & dropStazionePickUp.SelectedValue & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND " &
            "(tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & dropStazioneDropOff.SelectedValue & "' AND tipo='DROP' AND id_tariffa=tariffe.id))) " &
            " OR " &
            "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & dropStazionePickUp.SelectedValue & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND " &
            "(tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='DROP' AND id_tariffa=tariffe.id))) " &
            " OR " &
            "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & dropStazioneDropOff.SelectedValue & "' AND tipo='DROP' AND id_tariffa=tariffe.id)) AND " &
            "(tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='PICK' AND id_tariffa=tariffe.id))) ) " 'MODIFICARE LA QUERY ANCHE IN RIBALTAMENTO.ASPX
            sqlTariffeParticolari.SelectCommand = sqla
            err_n = 1
            If dropTipoCliente.SelectedValue > 0 Then
                'SE E' STATA SELEZIONATA UNA FONTE CONTROLLO ANCHE TRA I REQUISITI PER FONTE DELLE TARIFFE
                'UNENDO IN OR (L'ULTIMO) LA POSSIBILITA CHE LA TARIFFA NON SPECIFICHI ALCUNA STAZIONE MA SPECIFICHI UNA O PIU' FONTI

                sqla = sqlTariffeParticolari.SelectCommand & "" &
                "AND (tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tipologia_cliente='" & dropTipoCliente.SelectedValue & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da))" &
                "OR tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id)) " &
                "OR (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='PICK' AND id_tariffa=tariffe.id) AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='DROP' AND id_tariffa=tariffe.id) AND tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tipologia_cliente='" & dropTipoCliente.SelectedValue & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da)) )) ORDER BY codice"
                sqlTariffeParticolari.SelectCommand = sqla
            Else
                'SE NON E' STATA SELEZIONATA UNA FONTE PRELEVO SOLAMENTE LE TARIFFE CHE NON HANNO ALCUN PREREQUISITO DI FONTE
                sqla = sqlTariffeParticolari.SelectCommand & "" &
                "AND (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id))) ORDER BY codice"
                sqlTariffeParticolari.SelectCommand = sqla
            End If
            err_n = 2
            dropTariffeGeneriche.Items.Clear()
            dropTariffeGeneriche.Items.Add("Seleziona...")
            dropTariffeGeneriche.Items(0).Value = 0

            dropTariffeParticolari.Items.Clear()
            dropTariffeParticolari.Items.Add("Seleziona...")
            dropTariffeParticolari.Items(0).Value = 0
            err_n = 3
            dropTariffeGeneriche.DataBind()
            err_n = 4
            dropTariffeParticolari.DataBind()
            err_n = 5

        Catch ex As Exception
            HttpContext.Current.Response.Write("error setQueryTariffePossibili  : <br/>err: " & err_n & "<br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try


    End Sub

    Protected Sub listWarningPickPreventivi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listWarningPickPreventivi.ItemDataBound
        Dim tipo As Label = e.Item.FindControl("tipo")
        If tipo.Text = "PICK INFO" Then
            Dim warning As Label = e.Item.FindControl("warning")
            warning.ForeColor = Drawing.Color.Red   'Yellow
            warning.Font.Bold = True
        ElseIf tipo.Text = "PICK" Then
            Dim warning As Label = e.Item.FindControl("warning")
            warning.ForeColor = Drawing.Color.Red
            warning.Font.Bold = True
        End If
    End Sub

    Protected Sub listWarningDropPreventivi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listWarningDropPreventivi.ItemDataBound
        Dim tipo As Label = e.Item.FindControl("tipo")
        If tipo.Text = "DROP INFO" Then
            Dim warning As Label = e.Item.FindControl("warning")
            warning.ForeColor = Drawing.Color.Red 'Yellow
            warning.Font.Bold = True
        ElseIf tipo.Text = "DROP" Then
            Dim warning As Label = e.Item.FindControl("warning")
            warning.ForeColor = Drawing.Color.Red
            warning.Font.Bold = True
        End If
    End Sub

    Protected Sub btnAggiornaFontiCommissionabili_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAggiornaFontiCommissionabili.Click
        dropFonteCommissionabile.Items.Clear()
        dropFonteCommissionabile.Items.Add("Seleziona...")
        dropFonteCommissionabile.Items(0).Value = "0"
        dropFonteCommissionabile.DataBind()
    End Sub

    Protected Sub btnContinua_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinua.Click
        setPadding("preventivo_continua_da_warning")

        btnAnnulla1.Visible = False
        table_warning.Visible = False

        setQueryTariffePossibili(0)

        table_gruppi.Visible = True
        table_tariffe.Visible = True

        listGruppi.DataBind()

        'SE LA TIPOLOGIA DI CLIENTE E' UN'AGENZIA DI VIAGGIO VENGONO MOSTRATE LE FONTI COMMISSIONABILI - PER QUESTA TIPOLOGIA NON E' 
        'POSSIBILE VENDERE TARIFFE GENERICHE
        If dropTipoCliente.SelectedValue <> "0" Then
            If cliente_is_agenzia_di_viaggio(dropTipoCliente.SelectedValue) Then
                lblFonteCommissionabile.Visible = True
                dropFonteCommissionabile.Visible = True
                dropFonteCommissionabile.Enabled = True
                dropTipoCommissione.Visible = True
                txtPercentualeCommissionabile.Visible = True
                lblPercentualeCommissionabile.Visible = True
                btnAggiornaFontiCommissionabili.Visible = True
                dropTariffeGeneriche.Enabled = False
            Else
                lblFonteCommissionabile.Visible = False
                dropFonteCommissionabile.Visible = False
                dropFonteCommissionabile.SelectedValue = "0"
                dropTipoCommissione.Visible = False
                dropTipoCommissione.SelectedValue = "0"
                txtPercentualeCommissionabile.Visible = False
                txtPercentualeCommissionabile.Text = ""
                lblPercentualeCommissionabile.Visible = False
                btnAggiornaFontiCommissionabili.Visible = False
                dropTariffeGeneriche.Enabled = True
            End If
            'SE IL CLIENTE E' UN BROKER
            If cliente_is_broker(dropTipoCliente.SelectedValue) Then
                'SE NON CI SONO TARIFFE PARTICOLARI VENDIBILI E' POSSIBILE SALVARE LA PRENOTAZIONE SENZA TARIFFA
                If dropTariffeParticolari.Items.Count = 1 Then
                    btnSalvaPrenNoTariffa.Visible = True
                Else
                    btnSalvaPrenNoTariffa.Visible = False
                End If
            Else
                btnSalvaPrenNoTariffa.Visible = False
            End If
        Else
            btnSalvaPrenNoTariffa.Visible = False
            lblFonteCommissionabile.Visible = False
            dropFonteCommissionabile.Visible = False
            dropFonteCommissionabile.SelectedValue = "0"
            dropTipoCommissione.Visible = False
            dropTipoCommissione.SelectedValue = "0"
            txtPercentualeCommissionabile.Visible = False
            txtPercentualeCommissionabile.Text = ""
            lblPercentualeCommissionabile.Visible = False
            btnAggiornaFontiCommissionabili.Visible = False
            dropTariffeGeneriche.Enabled = True
        End If

        setPrecedentiGruppiETariffe()
    End Sub

    Protected Sub listGruppi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listGruppi.ItemDataBound
        Dim id_gruppo As Label = e.Item.FindControl("id_gruppo")

        'SE SONO PRESENTI WARNING FACCIO IN MODO CHE IL GRUPPO NON SIA CHECKATO (POTREBBE ESSERLO PER GRUPPO VELOCE O SELEZIONE GRUPPI PRECEDENTE RICERCA)

        If funzioni_comuni.gruppo_vendibile_eta_guidatori(id_gruppo.Text, Trim(txtEtaPrimo.Text), Trim(txtEtaSecondo.Text), "", "", "", "", "", "", False) <> 0 Then
            If Not funzioni_comuni.gruppo_vendibile_pick_up(dropStazionePickUp.SelectedValue, id_gruppo.Text, "", "", "", "", "", "", False) Then
                Dim punto1 As Image = e.Item.FindControl("punto1")
                Dim pick As Label = e.Item.FindControl("pick")

                punto1.Visible = True
                pick.Visible = True
            End If

            If Not funzioni_comuni.gruppo_vendibile_drop_off(dropStazioneDropOff.SelectedValue, id_gruppo.Text, "", "", "", "", "", "", False) Then
                Dim punto2 As Image = e.Item.FindControl("punto2")
                Dim drop As Label = e.Item.FindControl("drop")

                punto2.Visible = True
                drop.Visible = True
            End If

            If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
                If Not funzioni_comuni.gruppo_vendibile_val(dropStazioneDropOff.SelectedValue, id_gruppo.Text, "", "", "", "", "", "", False) Then
                    Dim punto3 As Image = e.Item.FindControl("punto3")
                    Dim val As Label = e.Item.FindControl("val")

                    punto3.Visible = True
                    val.Visible = True
                End If
            End If

            Dim data_pick As String = txtDaData.Text 'funzioni_comuni.getDataDb_senza_orario(txtDaData.Text)
            data_pick = Year(data_pick) & "-" & Month(data_pick) & "-" & Day(data_pick)

            If funzioni_comuni.gruppoInStopSell(dropStazionePickUp.SelectedValue, id_gruppo.Text, data_pick, "", "", "", "", "", "", False) Then
                Dim punto4 As Image = e.Item.FindControl("punto4")
                Dim stop_sale As Label = e.Item.FindControl("stop_sale")

                punto4.Visible = True
                stop_sale.Visible = True
            End If

            'SE E' STATA SELEZIONATA UNA FONTE CONTROLLO SE LA FONTE E' IN STOP SELL PER IL SINGOLO GRUPPO
            If dropTipoCliente.SelectedValue <> "0" Then
                If funzioni_comuni.gruppoInStopSellPerFonte(dropStazionePickUp.SelectedValue, dropTipoCliente.SelectedValue, id_gruppo.Text, data_pick, "", "", "", "", "", "", False) Then
                    Dim punto5 As Image = e.Item.FindControl("punto5")
                    Dim stop_sale_fonte As Label = e.Item.FindControl("stop_sale_fonte")

                    punto5.Visible = True
                    stop_sale_fonte.Visible = True
                End If
            End If
        Else
            'IL CONTROLLO SULL'ETA' DEI GUIDATORI E' BLOCCANTE: DISABILITO I GRUPPI NON VENDIBILI
            Dim sel_gruppo As CheckBox = e.Item.FindControl("sel_gruppo")

            sel_gruppo.Enabled = False

            Dim punto6 As Image = e.Item.FindControl("punto6")
            Dim eta_guidatore As Label = e.Item.FindControl("eta_guidatore")

            punto6.Visible = True
            eta_guidatore.Visible = True
        End If
    End Sub

    Protected Sub vedi_tariffe(Optional cancella_calcolo_precedente As Boolean = True)
        'test()


        Try

            Dim data_creazione As String = Date.Now.ToShortDateString & " " & Date.Now.ToLongTimeString  'aggiunto salvo 11.01.2023

            'ELIMINO LE PRECEDENTI RIGHE DI CALCOLO (PER IL NUM. CALCOLO ATTUALE)---------------------------------------------------------------
            '# verificare se richiamato da apri preventivo NON deve eliminare il precedente calcolo - salvo 24.01.2023
            cancella_calcolo_precedente = False 'test
            If cancella_calcolo_precedente = True Then
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM preventivi_costi WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
                Cmd.ExecuteNonQuery()

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            Else
                'se FALSE non elimina il calcolo precedente(attuale su questa visualizzazione) 
                'incrementa il numCalcolo per lasciare il precedente
                numCalcolo.Text = CInt(numCalcolo.Text) + 1     'incrementa il numero di calcolo per inserirlo
            End If
            '@end verificare se richiamato da apri preventivo NON deve eliminare il precedente calcolo - salvo 24.01.2023



            'Assegna num_calcolo alla session salvo 18.07.2023
            HttpContext.Current.Session("num_calcolo_preventivo") = numCalcolo.Text



            '-----------------------------------------------------------------------------------------------------------------------------------

            'CALCOLO DEL PREZZO PER OGNI GRUPPO SELEZIONATO

            'CONTROLLO CHE E' STATO SELEZIONATO ALMENO UN GRUPPO
            Dim almeno_un_gruppo_selezionato As Boolean = False

            For i = 0 To listGruppi.Items.Count - 1
                Dim sel_gruppo As CheckBox = listGruppi.Items(i).FindControl("sel_gruppo")
                If sel_gruppo.Checked Then
                    almeno_un_gruppo_selezionato = True
                    Exit For
                End If
            Next

            If almeno_un_gruppo_selezionato Then
                If (dropTariffeGeneriche.SelectedValue <> "0" And dropTariffeParticolari.SelectedValue = "0") Or (dropTariffeGeneriche.SelectedValue = "0" And dropTariffeParticolari.SelectedValue <> "0") Then
                    Dim id_tariffe_righe As String = ""

                    'verifica se tariffe generiche o tariffe particolari 
                    If dropTariffeGeneriche.SelectedValue <> "0" Then
                        id_tariffe_righe = dropTariffeGeneriche.SelectedValue
                    ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                        id_tariffe_righe = dropTariffeParticolari.SelectedValue
                    End If

                    'CONTROLLO CHE LA TARIFFA RISPETTI EVENTUALI VINCOLI DI MINIMO/MASSIMO GIORNI DI NOLEGGIO
                    Dim numero_giorni As Integer = 0

                    Dim min_giorni_nolo As String = funzioni_comuni.getMinGiorniNolo(id_tariffe_righe)
                    Dim max_giorni_nolo As String = funzioni_comuni.getMaxGiorniNolo(id_tariffe_righe)

                    If min_giorni_nolo <> "-1" Or max_giorni_nolo <> "-1" Then
                        numero_giorni = funzioni_comuni.getGiorniDiNoleggio(txtDaData.Text, txtAData.Text, ore1.Text, minuti1.Text, ore2.Text, minuti2.Text, id_tariffe_righe)
                        If min_giorni_nolo <> "-1" Then
                            lblMinGiorniNolo.Text = "La tariffa prevede un minimo di " & min_giorni_nolo & " giorni/o di noleggio"
                        Else
                            lblMinGiorniNolo.Text = ""
                        End If
                    Else
                        lblMinGiorniNolo.Text = ""
                    End If

                    If (min_giorni_nolo = "-1" And max_giorni_nolo = "-1") Or (min_giorni_nolo = "-1" And CInt(numero_giorni) <= CInt(max_giorni_nolo) And max_giorni_nolo <> "-1") Or (CInt(min_giorni_nolo) <= CInt(numero_giorni) And max_giorni_nolo = "-1" And min_giorni_nolo <> "-1") Or (CInt(min_giorni_nolo) <= CInt(numero_giorni) And CInt(max_giorni_nolo) >= CInt(numero_giorni) And min_giorni_nolo <> "-1" And max_giorni_nolo <> "-1") Then
                        'BLOCCO LA POSSIBILITA' DI MODIFICARE TARIFFA E SCONTO - PER FARLO SARA' NECESSARIO AGIRE SULL'APPOSITO PULSANTE--------------------
                        dropTariffeGeneriche.Enabled = False
                        dropTariffeParticolari.Enabled = False

                        txtSconto.ReadOnly = True
                        txt_sconto_new.ReadOnly = True  'aggiunto salvo 19.01.2023

                        dropTipoSconto.Enabled = False
                        btnCambiaTariffa.Visible = True

                        If dropFonteCommissionabile.Visible Then
                            dropFonteCommissionabile.Enabled = False
                            dropTipoCommissione.Enabled = False
                            btnAggiornaFontiCommissionabili.Visible = False
                        End If
                        '-----------------------------------------------------------------------------------------------------------------------------------

                        table_accessori_extra.Visible = True



                        'SELEZIONO GLI ELEMENTI EXTRA (NON OBBLIGATORI E CONTRASSEGNATI COME "NON VALORIZZARE" IN condizioni_elementi)
                        sqlElementiExtra.SelectCommand = funzioni.getQueryElementiExtra(id_tariffe_righe, False)

                        dropElementiExtra.Items.Clear()
                        dropElementiExtra.Items.Add("Seleziona...")
                        dropElementiExtra.Items(0).Value = "0"
                        dropElementiExtra.DataBind()


                        '# se richiamata da apertura preventivo ricalcola con nuovo metodo - salvo 24.01.2023
                        calcolaTariffe(id_tariffe_righe, data_creazione)

                        tariffa_broker.Text = funzioni_comuni.is_tariffa_broker(id_tariffe_righe)

                        'lblFocus.Focus()

                        listPreventiviCosti.DataBind()

                    Else
                        Dim msg As String = "Attenzione: i giorni di noleggio sono " & numero_giorni & "; la tariffa scelta prevede"
                        If min_giorni_nolo <> "-1" Then
                            msg = msg & " minimo " & min_giorni_nolo & " giorni/o di nolo - "
                        End If
                        If max_giorni_nolo <> "-1" Then
                            msg = msg & " massimo " & max_giorni_nolo & " giorni/o di nolo "
                        End If

                        Libreria.genUserMsgBox(Page, msg)
                    End If

                    'TONY 27/03/2023
                    IncrementaContatoreVediTariffa()
                    'FINE Tony
                Else
                    Libreria.genUserMsgBox(Page, "Selezionare una tariffa generica oppure una tariffa particolare.")
                End If
            Else
                Libreria.genUserMsgBox(Page, "Selezionare almeno un gruppo.")
            End If
        Catch ex As Exception
            Response.Write("Errore vedi_tariffe(preventivi.aspx.vb): " & ex.Message & "<br/>")
        End Try
    End Sub

    'Tony 27-03-2023
    Protected Sub IncrementaContatoreVediTariffa()
        Dim ArrayGruppiSelezionati(15)

        For i = 0 To listGruppi.Items.Count - 1
            Dim sel_gruppo As CheckBox = listGruppi.Items(i).FindControl("sel_gruppo")
            Dim gruppo As Label = listGruppi.Items(i).FindControl("gruppo")
            If sel_gruppo.Checked Then
                ArrayGruppiSelezionati(i) = gruppo.Text
                'Response.Write("Gruppo " & i & ": " & ArrayGruppiSelezionati(i) & "<br>")
            End If
        Next

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
        Dim Sql As String
        Dim SqlQuery As String

        Try
            Dim GruppiSelezionati As String = ""
            For j = 0 To UBound(ArrayGruppiSelezionati)
                If ArrayGruppiSelezionati(j) <> "" Then
                    GruppiSelezionati = ArrayGruppiSelezionati(j) & ";" & GruppiSelezionati & ";"
                End If

            Next

            Dim Tariffa As String = ""
            If dropTariffeParticolari.SelectedValue <> 0 Then
                Tariffa = dropTariffeParticolari.SelectedItem.Text
            Else
                Tariffa = dropTariffeGeneriche.SelectedItem.Text
            End If

            Dim ArrayDataTime(1) As String
            Dim ArrayData(2) As String

            Dim DataOggi As String = ""
            Dim DataDal As String = ""
            Dim DataAl As String = ""


            ArrayDataTime = Split(Now, " ")
            ArrayData = Split(ArrayDataTime(0), "/")
            DataOggi = ArrayData(2) & "-" & ArrayData(1) & "-" & ArrayData(0) & " " & ArrayDataTime(1)

            ArrayData = Split(txtDaData.Text, "/")
            DataDal = ArrayData(2) & "-" & ArrayData(1) & "-" & ArrayData(0) & " " & ArrayDataTime(1)

            ArrayData = Split(txtAData.Text, "/")
            DataAl = ArrayData(2) & "-" & ArrayData(1) & "-" & ArrayData(0) & " " & ArrayDataTime(1)


            Sql = "insert into contatore_vedi_tariffe (data,id_operatore,operatore,tariffa,gruppo,ngiorni_nolo,dal,al) values('" & DataOggi & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Tariffa & "','" & GruppiSelezionati & "','" & txtNumeroGiorni.Text & "','" & DataDal & "','" & DataAl & "')"
            'Response.Write(Sql & "<br/>")
            'Response.End()
            Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
            Cmd.ExecuteNonQuery()

            SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
            'Response.Write(SqlQuery & "<br/>")
            Cmd.CommandText = SqlQuery
            Cmd.ExecuteNonQuery()
        Catch ex As Exception
            Libreria.genUserMsgBox(Me, ex.Message & " Elimina Documenti --- Errore contattare amministratore del sistema.")
        End Try

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

    End Sub

    Protected Sub btnProsegui_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProsegui.Click


        'Azzera session se prima è stato aperto qualche preventivo salvo 18.07.2023
        HttpContext.Current.Session("apre_preventivo") = ""

        'SE IL TIPO CLIENTE EVENTUALMENTE SELEZIONATO E' UN'AGENZIA DI VIAGGIO DEVE ESSERE SCELTA LA FONTE COMMISSIONABILE
        '(SE IL SELETTORE DELLE FONTI COMMISSIONABILI E' VISIBILE VUOL DIRE CHE DEVE ESSERE SCELTO)
        If (Not dropFonteCommissionabile.Visible) Or (dropFonteCommissionabile.Visible And dropFonteCommissionabile.SelectedValue <> "0" And dropTipoCommissione.SelectedValue <> "0") Then
            Dim sel As CheckBox
            Dim old_sel As CheckBox

            For i = 0 To listGruppi.Items.Count - 1
                sel = listGruppi.Items(i).FindControl("sel_gruppo")
                old_sel = listGruppi.Items(i).FindControl("old_sel_gruppo")
                If sel.Checked Then
                    old_sel.Checked = True
                Else
                    old_sel.Checked = False
                End If
            Next

            If dropFonteCommissionabile.Visible Then
                txtPercentualeCommissionabile.Text = getPercentualeCommissionabile(dropFonteCommissionabile.SelectedValue)
            End If

            '# reset session salvo 19.01.2023
            'HttpContext.Current.Session("valore_preventivo") = ""
            'HttpContext.Current.Session("valore_preventivo_finale") = ""
            '@ reset session salvo 19.01.2023
            'lbl_valore_senza_sconto.Text = "" 'HttpContext.Current.Session("valore_preventivo")
            'lbl_valore_con_sconto.Text = "" 'HttpContext.Current.Session("valore_preventivo_finale")
            'lbl_sconti_tariffe.Text = "" 'HttpContext.Current.Session("perc_sconto_tariffa_tutte")

            vedi_tariffe()

            '# test verifica valore senza sconto salvo 19.01.2023
            'x registrazione dello sconto sulla tabella costi
            Try
                lbl_valore_senza_sconto.Text = HttpContext.Current.Session("valore_preventivo")
                lbl_valore_con_sconto.Text = HttpContext.Current.Session("valore_preventivo_finale")
                lbl_sconti_tariffe.Text = HttpContext.Current.Session("perc_sconto_tariffa_tutte")
                lbl_list_tariffe.Text = HttpContext.Current.Session("list_tariffe")
                lbl_list_tariffe_tempoKM.Text = HttpContext.Current.Session("list_tariffe_tempoKM")
                Dim ListTariffePeriodo As String = HttpContext.Current.Session("list_tariffe_periodo")
                Dim ListTariffeNome As String = HttpContext.Current.Session("list_tariffe_nome")
                '# qui dovrebbe aggiornare i campi relativi alle tariffe assegnate salvo
                Dim aggiornaListTariffe As String = funzioni_comuni_new.SalvaListTariffe("Preventivi", idPreventivo.Text, lbl_list_tariffe.Text, lbl_list_tariffe_tempoKM.Text, ListTariffePeriodo, ListTariffeNome, lbl_sconti_tariffe.Text)

                '@ 


                '# per aggiornare colonna SCONTO in preventivi_costi salvo 
                If lbl_valore_con_sconto.Text <> "" And lbl_valore_senza_sconto.Text <> "" Then

                    Dim valore_senza_sconto As Double = CDbl(lbl_valore_senza_sconto.Text)
                    Dim valore_con_sconto As Double = CDbl(lbl_valore_con_sconto.Text)
                    'idPreventivo.text   'id_documento in preventivi_costi
                    'numCalcolo.text        'numCalcolo in preventivi_costi

                    If valore_con_sconto < valore_senza_sconto Then 'è stato applicato uno sconto
                        'deve aggiornare tabella costi
                        Dim imp_sconto As String = funzioni_comuni_new.AggiornaCostiSconto(idPreventivo.Text, numCalcolo.Text, "P", valore_senza_sconto, valore_con_sconto)
                        lbl_imp_sconto.Text = imp_sconto

                        'visualizza etichetta sconto
                        listPreventiviCosti.DataBind()

                        'visualizza il massimo sconto applicabile sulla lbl msg 20.01.2023 salvo
                        'se lo sconto è pari o superiore a quello inserito
                        Dim max_sconto_applicabile() = Split(HttpContext.Current.Session("perc_sconto_tariffa_tutte"), ",")
                        Dim iMaxSconto As Double = 0
                        For xm = 0 To UBound(max_sconto_applicabile)
                            Dim i As Double = max_sconto_applicabile(xm)
                            If i >= iMaxSconto Then
                                iMaxSconto = i
                            End If
                        Next
                        'se lo sconto inserito è maggiore di quello MAx stabilito per quella tariffa
                        'visualizza l'etichetta
                        If CDbl(txt_sconto_new.Text) > iMaxSconto Then
                            lblMxSconto.Text = "Massimo sconto applicabile " & iMaxSconto.ToString & "%"
                            lblMxSconto.Visible = True
                            lblMxSconto.Font.Bold = True
                            lblMxSconto.ForeColor = Drawing.Color.Red
                            txt_sconto_new.Text = iMaxSconto.ToString
                        Else
                            lblMxSconto.Text = "Sconto applicato " & txt_sconto_new.Text & "%"
                            lblMxSconto.Visible = True
                            lblMxSconto.Font.Bold = True
                            lblMxSconto.ForeColor = Drawing.Color.Green
                        End If

                    Else
                        lbl_imp_sconto.Text = "0"
                        lblMxSconto.Visible = False

                    End If


                End If


                '@ test verifica valore senza sconto salvo 19.01.2023
            Catch ex As Exception

            End Try






        Else
            Libreria.genUserMsgBox(Me, "E' necessario specificare la fonte commissionabile ed il tipo di commissione.")
        End If
    End Sub

    Protected Sub nuovo_accessorio(ByVal id_accessorio As String, ByVal id_gruppo As String, ByVal tipo As String, ByVal eta_primo As String, ByVal eta_secondo As String)
        Dim id_tariffe_righe As String = ""

        If dropTariffeGeneriche.SelectedValue <> "0" Then
            id_tariffe_righe = dropTariffeGeneriche.SelectedValue
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            id_tariffe_righe = dropTariffeParticolari.SelectedValue
        End If

        Dim sconto As Double
        If dropTipoSconto.SelectedValue = "0" Then
            sconto = CDbl(txtSconto.Text)
        ElseIf dropTipoSconto.SelectedValue = "1" Then
            sconto = 0
        End If

        If tipo = "YOUNG" Then
            funzioni.calcola_costo_joung_driver_secondo_guidatore(id_accessorio, dropStazionePickUp.SelectedValue, dropStazioneDropOff.SelectedValue, eta_primo, eta_secondo, id_gruppo, txtNumeroGiorni.Text, 0, False, sconto, id_tariffe_righe, idPreventivo.Text, "", "", "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
        ElseIf tipo = "YOUNG PRIMO" Then
            funzioni.calcola_costo_joung_driver_primo_guidatore(id_accessorio, dropStazionePickUp.SelectedValue, dropStazioneDropOff.SelectedValue, eta_primo, eta_secondo, id_gruppo, txtNumeroGiorni.Text, 0, False, sconto, id_tariffe_righe, idPreventivo.Text, "", "", "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
        ElseIf tipo = "VAL_GPS" Then
            funzioni.aggiungi_val_gps(dropStazionePickUp.SelectedValue, dropStazioneDropOff.SelectedValue, id_gruppo, txtNumeroGiorni.Text, 0, False, Nothing, "", "", sconto, id_tariffe_righe, idPreventivo.Text, "", "", "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
        ElseIf tipo = "ELEMENTO" Then
            funzioni.calcola_costo_elemento_extra(id_accessorio, dropStazionePickUp.SelectedValue, dropStazioneDropOff.SelectedValue, eta_primo, eta_secondo, id_gruppo, txtNumeroGiorni.Text, 0, False, "", "", sconto, id_tariffe_righe, idPreventivo.Text, "", "", "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
        End If
    End Sub

    Protected Sub calcolaTariffe(ByVal id_tariffe_righe As String, Optional data_creazione As String = "")
        If txtSconto.Text <> "" Then
            Dim max_sconto As Double = funzioni_comuni.checkMaxSconto(id_tariffe_righe, txtSconto.Text, "", "", "", "", "", "", False)

            If max_sconto <> -1 Then
                txtSconto.Text = max_sconto
                lblMxSconto.Visible = True
            Else
                lblMxSconto.Visible = False
            End If

        Else
            txtSconto.Text = "0"
            lblMxSconto.Visible = False
        End If

        'CONOSCENDO L'ID DELLA TABELLA tariffe righe (E' IL SELECTED VALUE DELLE DUE DROP DOWN LIST CHE VISUALIZZANO LE TARIFFE) CONOSCO
        '+ id_tempo_km
        '+ id_condizione
        '+ id_tariffa_madre
        '+ minuti_di_ritardo (minuti di ritardo consentiti oltre i quali scatta la giornata extra)

        'CALCOLO IL NUMERO DI GIORNI DI NOLEGGIO:----------------------------------------------------------------------------------------
        Dim numero_giorni As Integer = funzioni_comuni.getGiorniDiNoleggio(txtDaData.Text, txtAData.Text, ore1.Text, minuti1.Text, ore2.Text, minuti2.Text, id_tariffe_righe)
        txtNumeroGiorni.Text = numero_giorni
        '--------------------------------------------------------------------------------------------------------------------------------

        Dim primo_calcolo_commissioni As Boolean
        If dropFonteCommissionabile.Visible Then
            primo_calcolo_commissioni = True
        Else
            primo_calcolo_commissioni = False
        End If

        'PER OGNI GRUPPO CALCOLO I COSTI------------------------------------------------------------------------


        '#Blocco x Nuovo calcolo tariffe x periodi salvo 10.12.2022
        Dim tipoCli As String = dropTipoCliente.SelectedValue
        Dim descTariffa As String

        'verifica se tariffe generiche o tariffe particolari 
        'e ricava valore dal dropdown
        Dim TipoTariffa As String
        If dropTariffeGeneriche.SelectedValue <> "0" Then
            id_tariffe_righe = dropTariffeGeneriche.SelectedValue
            descTariffa = dropTariffeGeneriche.SelectedItem.ToString
            TipoTariffa = "G"
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            id_tariffe_righe = dropTariffeParticolari.SelectedValue
            descTariffa = dropTariffeParticolari.SelectedItem.ToString
            TipoTariffa = "P"
        End If
        '@ end Blocco x Nuovo calcolo tariffe x periodi salvo 10.12.2022

        '# salvo 24.01.2023 - da verificare se qui deve incrementare il num di calcolo per lasciare il precedente
        Dim NewNumCalcolo As String = CInt(numCalcolo.Text) '+ 1
        '@end salvo 24.01.2023 - da verificare se qui deve incrementare il num di calcolo per lasciare il precedente

        For i = 0 To listGruppi.Items.Count - 1
            Dim sel_gruppo As CheckBox = listGruppi.Items(i).FindControl("sel_gruppo")
            Dim id_gruppo As Label = listGruppi.Items(i).FindControl("id_gruppo")
            If sel_gruppo.Checked Then
                funzioni.calcolaTariffa_x_gruppo(dropStazionePickUp.SelectedValue, txtDaData.Text, CInt(ore1.Text), CInt(minuti1.Text),
                                                 dropStazioneDropOff.SelectedValue, id_tariffe_righe, id_gruppo.Text, "",
                                                 numero_giorni, 0, False, 0, CDbl(txtSconto.Text), dropTipoSconto.SelectedValue, 0, 0,
                                                 txtEtaPrimo.Text, txtEtaSecondo.Text, idPreventivo.Text, "", "", "", NewNumCalcolo,
                                                 Request.Cookies("SicilyRentCar")("idUtente"), "", dropFonteCommissionabile.SelectedValue,
                                                 txtPercentualeCommissionabile.Text, dropTipoCommissione.SelectedValue, primo_calcolo_commissioni, "", "",
                                                 TipoTariffa, descTariffa, txtAData.Text, tipoCli, data_creazione, txt_sconto_new.Text)
                'inserita ultima riga con parametri x nuovo calcolo tariffe x periodi - salvo 06.12.2022
            End If
        Next

        ultimo_gruppo.Text = ""
    End Sub

    Protected Sub listVecchioCalcolo_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listVecchioCalcolo.ItemDataBound

        Dim gruppo As Label = e.Item.FindControl("gruppo")
        Dim id_a_carico_di As Label = e.Item.FindControl("id_a_carico_di")
        Dim nome_costo As Label = e.Item.FindControl("nome_costo")
        Dim obbligatorio As Label = e.Item.FindControl("obbligatorio")
        Dim id_metodo_stampa As Label = e.Item.FindControl("id_metodo_stampa")
        Dim sconto As Label = e.Item.FindControl("lblSconto")
        Dim valore_costo As Label = e.Item.FindControl("valore_costoLabel")
        Dim costo_scontato As Label = e.Item.FindControl("costo_scontato")
        Dim omaggiabile As Label = e.Item.FindControl("omaggiabile")
        Dim omaggiato As CheckBox = e.Item.FindControl("chkOldOmaggio")
        Dim chkOmaggio As CheckBox = e.Item.FindControl("chkOmaggio")
        Dim chkScegli As CheckBox = e.Item.FindControl("chkScegli")
        Dim id_elemento As Label = e.Item.FindControl("id_elemento")

        Dim nCalcolo As Label = e.Item.FindControl("lbl_num_calcolo")

        If obbligatorio.Text = "False" And id_a_carico_di.Text <> Costanti.id_accessorio_incluso Then
            e.Item.FindControl("chkScegli").Visible = True

            'NEL CASO IN CUI L'ELEMENTO NON E' SELEZIONATO NON FACCIO VEDERE LA COLONNA SCONTO
        End If

        If sconto.Text = "0,00" Then
            sconto.Text = ""
            valore_costo.Visible = False
        Else
            sconto.Text = sconto.Text & " €"
            sconto.Visible = True
        End If

        ''TEST SEMPRE VISIBILE 21.04.2023 salvo
        ' sconto.Text = "nclc:" & nCalcolo.Text
        'sconto.Visible = True

        If Not chkScegli.Checked And chkScegli.Visible Then
            valore_costo.Visible = False
            sconto.Visible = False
            costo_scontato.Visible = False
        End If

        If omaggiato.Checked Then
            valore_costo.Text = "0,00 €"
            costo_scontato.Text = "0,00 €"
            sconto.Text = ""
        Else
            valore_costo.Text = valore_costo.Text & " €"
            costo_scontato.Text = costo_scontato.Text & " €"
        End If

        If LCase(nome_costo.Text) = "valore tariffa" Or LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Or LCase(nome_costo.Text) = "sconto" Then
            nome_costo.Font.Bold = True
        End If

        If id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore Then
            If id_a_carico_di.Text = Costanti.id_accessorio_incluso And LCase(nome_costo.Text) <> "valore tariffa" Then
                'PER GLI ELEMENTI INCLUSI TRANNE IL TEMPO KM NON MOSTRO IL PREZZO (CHE E' SEMPRE ZERO)
                e.Item.FindControl("lblIncluso").Visible = True
                valore_costo.Visible = False
                sconto.Visible = False
                costo_scontato.Visible = False
            ElseIf id_a_carico_di.Text <> Costanti.id_accessorio_incluso And obbligatorio.Text = "True" And LCase(nome_costo.Text) <> "valore tariffa" And LCase(nome_costo.Text) <> "sconto" Then
                e.Item.FindControl("lblObbligatorio").Visible = True
            End If
        Else
            e.Item.FindControl("lblInformativa").Visible = True
        End If

        If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
            If tariffa_broker.Text = "1" Then
                costo_scontato.Text = CDbl(costo_scontato.Text) - getCostoACaricoDelBroker(CInt(numCalcolo.Text) - 1)
            End If
            costo_scontato.Text = FormatNumber(costo_scontato.Text, 2, , , TriState.False)
            valore_costo.Visible = False
            costo_scontato.ForeColor = Drawing.Color.Blue
            costo_scontato.Font.Size = 12
            costo_scontato.Font.Bold = True
            e.Item.FindControl("lblObbligatorio").Visible = False
        End If

        'Response.Write(nome_costo.Text & "<br>")

        'salvo aggiunto 21.04.2023
        If Session("prev_valori_uguali") = "KO" Then

        End If

        If gruppo.Text = old_ultimo_gruppo.Text Then
            e.Item.FindControl("riga_gruppo").Visible = False             'salvo 22.12.2022
            e.Item.FindControl("riga_intestazione").Visible = False
        Else
            e.Item.FindControl("riga_gruppo").Visible = True
            e.Item.FindControl("riga_intestazione").Visible = True
            old_ultimo_gruppo.Text = gruppo.Text
        End If



        If omaggiabile.Text = "True" And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore Then
            chkOmaggio.Visible = True
            chkOmaggio.Enabled = False
        End If

        'TARIFFE BROKER: SE L'UTENTE NON HA IL PERMESSO VENGONO NASCOSTI TUTTI I PREZZI E VENGONO VISUALIZZATI SOLO GLI ACCESSORI (SENZA PREZZO)
        If tariffa_broker.Text = "1" Then
            e.Item.FindControl("preventivo").Visible = False
            'If livello_accesso_broker.Text <> "3" Then
            If id_elemento.Text = Costanti.ID_tempo_km Then
                sconto.Visible = False
                costo_scontato.Visible = False
            End If
            'End If

            'If (obbligatorio.Text = "False" And id_a_carico_di.Text <> Costanti.id_accessorio_incluso) Or (LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale)) Then
            '    'PER GLI ELEMENTI A SCELTA (O PER LA RIGA TOTALE DOVE E' PRESENTE IL PULSANTE AGGIUNGI ACCESSORIO) NASCONDO I COSTI
            '    valore_costo.Visible = False
            '    sconto.Visible = False
            '    costo_scontato.Visible = False

            '    If (LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale)) Then
            '        nome_costo.Text = ""
            '    End If
            'Else
            '    e.Item.FindControl("preventivo").Visible = False
            '    e.Item.FindControl("riga_elementi").Visible = False
            'End If
        End If
    End Sub

    Protected Sub listPreventiviCosti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listPreventiviCosti.ItemDataBound

        Dim gruppo As Label = e.Item.FindControl("gruppo")
        Dim id_a_carico_di As Label = e.Item.FindControl("id_a_carico_di")
        Dim nome_costo As Label = e.Item.FindControl("nome_costo")
        Dim obbligatorio As Label = e.Item.FindControl("obbligatorio")
        Dim id_metodo_stampa As Label = e.Item.FindControl("id_metodo_stampa")
        Dim aggiorna As Button = e.Item.FindControl("aggiorna")
        Dim sconto As Label = e.Item.FindControl("lblSconto")
        Dim valore_costo As Label = e.Item.FindControl("valore_costoLabel")
        Dim costo_scontato As Label = e.Item.FindControl("costo_scontato")
        Dim omaggiabile As Label = e.Item.FindControl("omaggiabile")
        Dim Oldomaggiato As CheckBox = e.Item.FindControl("chkOldOmaggio")
        Dim omaggiato As CheckBox = e.Item.FindControl("chkOmaggio")
        Dim chkScegli As CheckBox = e.Item.FindControl("chkScegli")
        Dim id_elemento As Label = e.Item.FindControl("id_elemento")

        If id_elemento.Text = "283" Then
            id_elemento.Text = "283"




        End If




        If tariffa_broker.Text = "1" Then
            e.Item.FindControl("vediCalcolo").Visible = False
        End If

        If obbligatorio.Text = "False" And id_a_carico_di.Text <> Costanti.id_accessorio_incluso Then
            chkScegli.Visible = True

            'NEL CASO IN CUI L'ELEMENTO NON E' SELEZIONATO NON FACCIO VEDERE LA COLONNA SCONTO
        End If

        If sconto.Text = "0,00" Then
            sconto.Text = ""
            valore_costo.Visible = False
        ElseIf Not dropFonteCommissionabile.Visible Then
            'PER QUESTIONI DI SPAZIO SE E' STATA SPECIFICATA UNA FONTE COMMISSIONABILE NON MOSTRO L'EVENTUALE SCONTO IN QUESTA SCHERMATA PER POTER MOSTRARE LE COMMISSIONI 
            '(IN OGNI CASO NON DOVREBBE ESSERE EFFETTUATO SCONTO IN QUESTI CASI)
            sconto.Text = sconto.Text & " €"
            sconto.Visible = True
        End If





        If dropFonteCommissionabile.Visible Then
            'MOSTRO LE COMMISSIONI PER IL TEMPO KM
            e.Item.FindControl("labelSconto").Visible = False
            e.Item.FindControl("labelCommissioni").Visible = True

            Dim lblCommissioni As Label = e.Item.FindControl("lblCommissioni")
            If lblCommissioni.Text <> "0,00" Then
                lblCommissioni.Visible = True
            End If
            If nome_costo.Text = Costanti.testo_elemento_totale Then
                lblCommissioni.Font.Bold = True
                lblCommissioni.Font.Size = 12
            End If

            '#integra il blocco precedente per l'aggiunta del nuovo campo txt_sconto_new - salvo 19.01.2023

            '@end integra il blocco precedente per l'aggiunta del nuovo campo txt_sconto_new - salvo 19.01.2023


        End If

        If Not chkScegli.Checked And chkScegli.Visible Then
            'GLI ACCESSORI NON SELEZIONATI NON MOSTRANO IL PREZZO
            valore_costo.Visible = False
            sconto.Visible = False
            costo_scontato.Visible = False
        End If

        If Oldomaggiato.Checked Then
            valore_costo.Text = "0,00 €"
            costo_scontato.Text = "0,00 €"
            sconto.Text = ""
        Else
            valore_costo.Text = valore_costo.Text & " €"
            costo_scontato.Text = costo_scontato.Text & " €"
        End If

        If LCase(nome_costo.Text) = "valore tariffa" Or LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Or LCase(nome_costo.Text) = "sconto" Then
            nome_costo.Font.Bold = True
        End If



        If id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore Then
            If id_a_carico_di.Text = Costanti.id_accessorio_incluso And LCase(nome_costo.Text) <> "valore tariffa" Then
                'PER GLI ELEMENTI INCLUSI TRANNE IL TEMPO KM NON MOSTRO IL PREZZO (CHE E' SEMPRE ZERO)
                e.Item.FindControl("lblIncluso").Visible = True
                valore_costo.Visible = False
                sconto.Visible = False
                costo_scontato.Visible = False
            ElseIf id_a_carico_di.Text <> Costanti.id_accessorio_incluso And obbligatorio.Text = "True" And LCase(nome_costo.Text) <> "valore tariffa" And LCase(nome_costo.Text) <> "sconto" Then
                e.Item.FindControl("lblObbligatorio").Visible = True
            End If
        Else
            e.Item.FindControl("lblInformativa").Visible = True
        End If

        If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
            If tariffa_broker.Text = "1" Then
                costo_scontato.Text = CDbl(costo_scontato.Text) - getCostoACaricoDelBroker(numCalcolo.Text)
            End If

            costo_scontato.Text = FormatNumber(costo_scontato.Text, 2, , , TriState.False)
            valore_costo.Visible = False

            costo_scontato.Font.Bold = True
            costo_scontato.ForeColor = Drawing.Color.Blue
            costo_scontato.Font.Size = 12
            e.Item.FindControl("lblObbligatorio").Visible = False
            aggiorna.Visible = True
        End If

        'Response.Write(nome_costo.Text & "<br>")




        If gruppo.Text = ultimo_gruppo.Text Then
            e.Item.FindControl("riga_gruppo").Visible = False
            e.Item.FindControl("riga_intestazione").Visible = False
        Else
            e.Item.FindControl("riga_gruppo").Visible = True
            e.Item.FindControl("riga_intestazione").Visible = True
            ultimo_gruppo.Text = gruppo.Text
            'NEL CASO IN CUI SI STA RICHIAMANDO UN PREVENTIVO NASCONDO IL PULSANTE "PREVENTIVO"
            If tipo_preventivo.Text = "richiama" Then
                e.Item.FindControl("preventivo").Visible = False
            End If
            If numero_prenotazione.Text <> "" Then
                'DOPO IL SALVATAGGIO DELLA PRENOTAZIONE NON MOSTRO I PULSANTI PRENOTAZIONE E PREVENTIVO 
                e.Item.FindControl("preventivo").Visible = False
                e.Item.FindControl("prenotazione").Visible = False
            End If
            'SE LA DATA DI USCITA NON E' QUELLA ODIERNA NON E' POSSIBILE CLICCARE SU CONTRATTO
            'Dim uscita As DateTime
            'If txtDaData.Text <> "" Then
            '    uscita = funzioni_comuni.getDataDb_senza_orario2(txtDaData.Text)
            'Else
            '    uscita = Date.Now
            'End If
            Dim uscita As DateTime = funzioni_comuni.getDataDb_senza_orario2(txtDaData.Text)
            Dim oggi As DateTime = funzioni_comuni.getDataDb_senza_orario2(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()))

            If DateDiff(DateInterval.Day, oggi, uscita) <> 0 Or dropStazionePickUp.SelectedValue <> Request.Cookies("SicilyRentCar")("stazione") Then
                'UNA PRENOTAZIONE ATTIVA PUO' DIVENTARE CONTRATTO SOLO SE E' DEL GIORNO ODIERNO
                e.Item.FindControl("contratto").Visible = False
            End If
        End If

        If omaggiabile.Text = "True" And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore Then
            'ABILITO L'OMAGGIABILITA' DI UN ELEMENTO A SCELTA SOLAMENTE SE L'UTENTE HA IL RELATIVO PERMESSO - SOLO PER OBBLIGATORI ED ACCESSORI
            If livello_accesso_omaggi.Text = "3" Then
                omaggiato.Visible = True
            Else
                omaggiato.Visible = True
                omaggiato.Enabled = False
            End If
        End If

        'TARIFFE BROKER: VENGONO NASCOSTI I PREZZI A CARICO DEL BROKER (VALORE_TARIFFA) E VENGONO VISUALIZZATI SOLO I COSTI DEGLI ACCESSORI (PERO' C'E' IL PERMESSO)
        If tariffa_broker.Text = "1" Then
            e.Item.FindControl("preventivo").Visible = False
            If livello_accesso_broker.Text <> "3" Then
                If id_elemento.Text = Costanti.ID_tempo_km Then
                    sconto.Visible = False
                    costo_scontato.Visible = False
                End If
            Else
                If id_elemento.Text = Costanti.ID_tempo_km Then
                    nome_costo.Text = nome_costo.Text & " (Broker)"
                End If
                If (LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale)) Then
                    nome_costo.Text = nome_costo.Text & " CLIENTE"
                End If
            End If

            'If (obbligatorio.Text = "False" And id_a_carico_di.Text <> Costanti.id_accessorio_incluso) Or (LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale)) Then
            '    'PER GLI ELEMENTI A SCELTA (O PER LA RIGA TOTALE DOVE E' PRESENTE IL PULSANTE AGGIUNGI ACCESSORIO) NASCONDO I COSTI
            '    valore_costo.Visible = False
            '    sconto.Visible = False
            '    costo_scontato.Visible = False

            '    If (LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale)) Then
            '        nome_costo.Text = ""
            '    End If
            'Else
            '    e.Item.FindControl("preventivo").Visible = False
            '    e.Item.FindControl("riga_elementi").Visible = False
            'End If
        End If

        '28.12.2021
        If id_elemento.Text = "223" And chkScegli.Checked = True Then
            id_elires = True
        End If
        If id_elemento.Text = "248" And chkScegli.Checked = True Then
            id_pplus = True
        End If
        If id_elemento.Text = "100" And chkScegli.Checked = True Then
            id_rd = True
        End If
        If id_elemento.Text = "170" And chkScegli.Checked = True Then
            id_rf = True
        End If


        If costo_scontato.Text = "800,00 €" Then
            ' costo_scontato.Text = "800,00 €"
        End If

        Dim iddoc As String = ""
        Dim impDepCalcolato As String = ""

        'se RD RF attive senza PPLUS 
        If id_elemento.Text = "2830" Then

            impDepCalcolato = GetValoreDepositoCauzionaleDefault(id_gruppo_auto_scelto.Text)


            If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "100", "ck") = True _
            And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "170", "ck") = True _
            And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = False Then
                impDepCalcolato = "300,00 €"
                costo_scontato.Text = impDepCalcolato
            End If

            'se eliminazione responsabilità
            If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "223", "ck") = True Then
                impDepCalcolato = "200,00 €"
                costo_scontato.Text = impDepCalcolato
            End If

            'se PPLUS
            If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = True Then
                impDepCalcolato = "200,00 €"
                costo_scontato.Text = impDepCalcolato
            End If

            'se tutte le condizioni non attive x deposito riporta valore a default 19.01.22
            If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "223", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "100", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "170", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "234", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = False Then
                'recupera valore originario del deposito cauzionale
                impDepCalcolato = GetValoreDepositoCauzionaleDefault(id_gruppo_auto_scelto.Text)
                costo_scontato.Text = impDepCalcolato
            End If

            'se attiva riduzione 50% deposito
            If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "234", "ck") = True Then
                impDepCalcolato = FormatNumber((CDbl(impDepCalcolato) / 2), 2)
                costo_scontato.Text = impDepCalcolato
            End If

            costo_scontato.Text = FormatNumber(costo_scontato.Text, 2) & " €"


        End If 'se si tratta di deposito cauzionale 19.01.22


        'Tony 27/10/2022
        'TARIFFE BROKER: VENGONO NASCOSTI I PREZZI A CARICO DEL BROKER (VALORE_TARIFFA) E VENGONO VISUALIZZATI SOLO I COSTI DEGLI ACCESSORI (PERO' C'E' IL PERMESSO)
        If tariffa_broker.Text = "1" Then
            Dim CostoValoreTariffaBroker As String

            If (LCase(nome_costo.Text) = LCase("Valore Tariffa (Broker)")) Then
                CostoValoreTariffaBroker = Replace(costo_scontato.Text, " €", "")
                'Response.Write(CostoValoreTariffaBroker & "<br>")
            End If
            'Response.Write("OK2" & nome_costo.Text & "<br>")
            If (LCase(nome_costo.Text) = LCase("TOTALE CLIENTE")) Then
                'costo_scontato.Text = "0,00"
                costo_scontato.Text = CDbl(costo_scontato.Text) + CDbl(getCostoACaricoDelBroker("1"))
                nome_costo.Text = "TOTALE BROKER"
            End If
        Else
            'Response.Write("KO2<br>")
        End If


        '# aggiunto salvo 19.01.2023
        'visualizza campo sconto e inserisce l'importo dello sconto
        If txt_sconto_new.Text <> "0" And LCase(nome_costo.Text) = "valore tariffa" Then
            sconto.Visible = True
            sconto.Text = lbl_imp_sconto.Text & " €"


        End If
        '@end aggiunto salvo 19.01.2023



        'Salvo 17.02.2023
        If LCase(nome_costo.Text) = "valore tariffa" Then
            If lbl_Importo_Sconto.Text <> "0" Then
                sconto.Visible = True
                'recupera importo sconto  e lo scrive per recuperarlo nella list
                Dim val_imp_sconto As String = funzioni_comuni_new.GetImportoScontoPreventivo(idPreventivo.Text, numCalcolo.Text)

                If val_imp_sconto <> "0" Then
                    lbl_Importo_Sconto.Text = val_imp_sconto
                    sconto.Text = val_imp_sconto & " €"   'lbl_Importo_Sconto.Text
                Else

                End If
            Else
                sconto.Visible = False
            End If
        End If
        '@salvo









    End Sub

    Protected Function getCostoACaricoDelBroker(ByVal num_calcolo As String) As Double
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT imponibile_scontato + iva_imponibile_scontato FROM preventivi_costi WITH(NOLOCK) WHERE id_elemento='" & Costanti.ID_tempo_km & "' AND id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & num_calcolo & "'", Dbc)

        getCostoACaricoDelBroker = Cmd.ExecuteScalar()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub aggiungi_warning_gruppo_auto(ByVal gruppo_scelto As String, ByVal id_preventivo As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal numero_calcolo As String)
        Dim tabella As String
        Dim id_documento As String

        If id_preventivo <> "" Then
            tabella = "preventivi_warning"
            id_documento = id_preventivo
        ElseIf id_prenotazione <> "" Then
            tabella = "prenotazioni_warning"
            id_documento = id_prenotazione
        ElseIf id_contratto <> "" Then
            tabella = "contratti_warning"
            id_documento = id_contratto
        End If

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        'ELIMINO EVENTUALI ALTRI WARNING DI GRUPPO SALVATI (DALLA PAGINA DEI PREVENTIVI I WARNING PER GRUPPO VENGONO SCRITTI SOLO DA QUESTA
        'FUNZIONE
        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM " & tabella & " WHERE id_documento='" & id_documento & "' AND num_calcolo='" & numero_calcolo & "' AND tipo='GRUPPO'", Dbc)
        Cmd.ExecuteNonQuery()

        For i = 0 To listGruppi.Items.Count - 1
            Dim id_gruppo_lista As Label = listGruppi.Items(i).FindControl("id_gruppo")
            If id_gruppo_lista.Text = gruppo_scelto Then
                Dim pick As Label = listGruppi.Items(i).FindControl("pick")
                If pick.Visible Then
                    Cmd = New Data.SqlClient.SqlCommand("INSERT INTO " & tabella & " (id_documento, num_calcolo, warning, id_operatore, tipo) VALUES ('" & id_documento & "','" & numero_calcolo & "','" & Replace("GRUPPO - " & pick.Text, "'", "''") & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "','" & "GRUPPO" & "')", Dbc)
                    Cmd.ExecuteNonQuery()
                End If

                Dim drop As Label = listGruppi.Items(i).FindControl("drop")
                If drop.Visible Then
                    Cmd = New Data.SqlClient.SqlCommand("INSERT INTO " & tabella & " (id_documento, num_calcolo, warning, id_operatore, tipo) VALUES ('" & id_documento & "','" & numero_calcolo & "','" & Replace("GRUPPO - " & drop.Text, "'", "''") & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "','" & "GRUPPO" & "')", Dbc)
                    Cmd.ExecuteNonQuery()
                End If

                Dim val As Label = listGruppi.Items(i).FindControl("val")
                If val.Visible Then
                    Cmd = New Data.SqlClient.SqlCommand("INSERT INTO " & tabella & " (id_documento, num_calcolo, warning, id_operatore, tipo) VALUES ('" & id_documento & "','" & numero_calcolo & "','" & Replace("GRUPPO - " & val.Text, "'", "''") & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "','" & "GRUPPO" & "')", Dbc)
                    Cmd.ExecuteNonQuery()
                End If

                Dim stop_sale As Label = listGruppi.Items(i).FindControl("stop_sale")
                If stop_sale.Visible Then
                    Cmd = New Data.SqlClient.SqlCommand("INSERT INTO " & tabella & " (id_documento, num_calcolo, warning, id_operatore, tipo) VALUES ('" & id_documento & "','" & numero_calcolo & "','" & Replace("GRUPPO - " & stop_sale.Text, "'", "''") & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "','" & "GRUPPO" & "')", Dbc)
                    Cmd.ExecuteNonQuery()
                End If

                Dim stop_sale_fonte As Label = listGruppi.Items(i).FindControl("stop_sale_fonte")
                If stop_sale_fonte.Visible Then
                    Cmd = New Data.SqlClient.SqlCommand("INSERT INTO " & tabella & " (id_documento, num_calcolo, warning, id_operatore, tipo) VALUES ('" & id_documento & "','" & numero_calcolo & "','" & Replace("GRUPPO - " & stop_sale_fonte.Text, "'", "''") & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "','" & "GRUPPO" & "')", Dbc)
                    Cmd.ExecuteNonQuery()
                End If
            End If
        Next

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Function get_id_elemento(ByVal id_preventivi_costi As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_elemento FROM preventivi_costi WITH(NOLOCK) WHERE id='" & id_preventivi_costi & "'", Dbc)

        get_id_elemento = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getIdDitta(ByVal codiceEdp As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_ditta FROM ditte WITH(NOLOCK) WHERE [codice edp]='" & codiceEdp & "'", Dbc)

        getIdDitta = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing


    End Function


    Public Function VerificaOpzione(id_ele As String) As Boolean


        Dim check_attuale As CheckBox
        Dim check_old As CheckBox
        Dim id_elemento As Label
        Dim omaggiato As CheckBox
        Dim num_elemento As Label

        Dim ris As Boolean = False

        For i = 0 To listPreventiviCosti.Items.Count - 1
            check_attuale = listPreventiviCosti.Items(i).FindControl("chkScegli")
            check_old = listPreventiviCosti.Items(i).FindControl("chkOldScegli")
            id_elemento = listPreventiviCosti.Items(i).FindControl("id_elemento")
            omaggiato = listPreventiviCosti.Items(i).FindControl("chkOmaggio")

            If id_elemento.Text = id_ele Then 'protezione plus   16.11.2021
                ris = True
                Exit For
            End If

        Next


    End Function



    Public Function GetResponseCk(ByVal idgr As String, ByRef lbl_idgr_label As Label) As String


        Dim ris As String = " § "
        Dim idgruppo As String
        Dim idgruppoT As String
        Dim id_gruppoSceltoT As Label = lbl_idgr_label '= e.Item.FindControl("id_gruppoLabel") 'Non necessario perchè ricontrollo quale gruppo richiamato
        Dim id_gruppoT As Label
        Dim check_attualeT As CheckBox
        Dim check_oldT As CheckBox
        Dim id_elementoT As Label
        Dim omaggiatoT As CheckBox
        Dim old_omaggiatoT As CheckBox

        Dim num_elementoT As Label
        Dim tipologia_franchigiaT As Label
        Dim sottotipologia_franchigiaT As Label

        Dim old_selT As CheckBox
        Dim id_gruppoListT As Label

        Dim protezione_plusT As Boolean = False  'aggiunto 16.11.2021
        Dim Eliminazione_resT As Boolean = False  'aggiunto 16.11.2021

        Dim tresponse As String = ""        'stringa con tutti i ck attivati
        Dim tresponseOld As String = ""     'stringa con tutti i ck OLD attivati

        For i = 0 To listPreventiviCosti.Items.Count - 1

            id_gruppoT = listPreventiviCosti.Items(i).FindControl("id_gruppoLabel")

            If id_gruppoT.Text = idgr Then 'id_gruppoSceltoT.Text Then
                idgruppoT = id_gruppoT.Text
                idgruppo = id_gruppoT.Text   'aggiunto 16.11.2021
                check_attualeT = listPreventiviCosti.Items(i).FindControl("chkScegli")
                check_oldT = listPreventiviCosti.Items(i).FindControl("chkOldScegli")
                id_elementoT = listPreventiviCosti.Items(i).FindControl("id_elemento")
                omaggiatoT = listPreventiviCosti.Items(i).FindControl("chkOmaggio")
                'is_gpsT = listPreventiviCosti.Items(i).FindControl("is_gps")
                num_elementoT = listPreventiviCosti.Items(i).FindControl("num_elemento")
                tipologia_franchigiaT = listPreventiviCosti.Items(i).FindControl("tipologia_franchigia")
                sottotipologia_franchigiaT = listPreventiviCosti.Items(i).FindControl("sottotipologia_franchigia")

                If id_elementoT.Text = "248" Then 'CK protezione plus Attivo e visibile 16.11.2021
                    protezione_plusT = True
                End If

                If id_elementoT.Text = "223" Then 'eliminazione responsabilità  CK Eliminazione Resp. Attivo -  16.11.2021
                    Eliminazione_resT = True
                End If

                If id_elementoT.Text = "203" Then 'Riduzione Danni   16.11.2021
                    id_elementoT.Text = "203"
                End If

                If id_elementoT.Text = "204" Then 'Riduzione Furto   16.11.2021
                    id_elementoT.Text = "204"
                End If

                If check_attualeT.Checked = True Then
                    tresponse += id_elementoT.Text & ","        'creo stringa con i ck attivi
                End If

                If check_oldT.Checked = True Then
                    tresponseOld += id_elementoT.Text & ","     'creo stringa con i ck OLD
                End If

            End If


        Next

        ''solo x test
        'Response.Write("ck:<br/>" & tresponse & "<br/>")
        'Response.Write("<br/>ckOLD:<br/>" & tresponseOld & "<br/>")

        ris = tresponse & "§" & tresponseOld

        Return ris





    End Function



    Public Sub btnAggiorna(ByVal idgr As String, ByRef lbl_idgr_label As Label, ByVal PPLUS As Boolean)
        'procedura richiamata dal pulsante aggiorna

        Dim idgruppo As String


        idgruppo = idgr       'imposta il gruppo passato
        'test x Verifica Ck e MSG
        Dim idgruppoT As String
        Dim id_gruppoSceltoT As Label = lbl_idgr_label '= e.Item.FindControl("id_gruppoLabel") 'Non necessario perchè ricontrollo quale gruppo richiamato
        Dim id_gruppoT As Label
        Dim check_attualeT As CheckBox
        Dim check_oldT As CheckBox
        Dim id_elementoT As Label
        Dim omaggiatoT As CheckBox
        Dim old_omaggiatoT As CheckBox

        Dim num_elementoT As Label
        Dim tipologia_franchigiaT As Label
        Dim sottotipologia_franchigiaT As Label

        Dim old_selT As CheckBox
        Dim id_gruppoListT As Label

        Dim protezione_plusT As Boolean = False  'aggiunto 16.11.2021
        Dim Eliminazione_resT As Boolean = False  'aggiunto 16.11.2021

        Dim tresponse As String = ""        'stringa con tutti i ck attivati
        Dim tresponseOld As String = ""     'stringa con tutti i ck OLD attivati

        For i = 0 To listPreventiviCosti.Items.Count - 1

            id_gruppoT = listPreventiviCosti.Items(i).FindControl("id_gruppoLabel")

            If id_gruppoT.Text = idgr Then 'id_gruppoSceltoT.Text Then
                idgruppoT = id_gruppoT.Text
                idgruppo = id_gruppoT.Text   'aggiunto 16.11.2021
                check_attualeT = listPreventiviCosti.Items(i).FindControl("chkScegli")
                check_oldT = listPreventiviCosti.Items(i).FindControl("chkOldScegli")
                id_elementoT = listPreventiviCosti.Items(i).FindControl("id_elemento")
                omaggiatoT = listPreventiviCosti.Items(i).FindControl("chkOmaggio")
                'is_gpsT = listPreventiviCosti.Items(i).FindControl("is_gps")
                num_elementoT = listPreventiviCosti.Items(i).FindControl("num_elemento")
                tipologia_franchigiaT = listPreventiviCosti.Items(i).FindControl("tipologia_franchigia")
                sottotipologia_franchigiaT = listPreventiviCosti.Items(i).FindControl("sottotipologia_franchigia")

                If id_elementoT.Text = "248" Then 'CK protezione plus Attivo e visibile 16.11.2021

                    protezione_plusT = True
                End If

                If id_elementoT.Text = "223" Then 'eliminazione responsabilità  CK Eliminazione Resp. Attivo -  16.11.2021
                    Eliminazione_resT = True
                End If

                If id_elementoT.Text = "203" Then 'Riduzione Danni   16.11.2021
                    id_elementoT.Text = "203"
                End If

                If id_elementoT.Text = "204" Then 'Riduzione Furto   16.11.2021
                    id_elementoT.Text = "204"
                End If

                If check_attualeT.Checked = True Then
                    tresponse += id_elementoT.Text & ","        'creo stringa con i ck attivi
                End If

                If check_oldT.Checked = True Then
                    tresponseOld += id_elementoT.Text & ","     'creo stringa con i ck OLD
                End If

            End If


        Next

        ''solo x test
        'Response.Write("ck:<br/>" & tresponse & "<br/>")
        'Response.Write("<br/>ckOLD:<br/>" & tresponseOld & "<br/>")

        '#####################
        ''Verifica e Messaggi  di controllo
        '#####################

        'se presente PPLUS verifico se sono presenti entrambe le
        'riduzioni altrimenti msg
        Dim ck_active As CheckBox


        Try

            If numero_prenotazione.Text = "" And preventivo_puo_diventare_prenotazione(idPreventivo.Text) Then


                'AGGIORNA IL COSTO DEL PREVENTIVO (SU RICHIESTA ESEGUENDO LE VARIAZIONI SU TUTTI I GRUPPI (MAIL DEL 31/10/12)) AGGIUNGENDO O RIMUOVENDO IN COSTO DI UNO O PIU' ACCESSORI SELEZIOANTI
                Dim id_gruppoScelto As Label = id_gruppoSceltoT  'e.Item.FindControl("id_gruppoLabel")   'modificato il 18.11.2021 passato il valore di sopra
                Dim id_gruppo As Label
                Dim check_attuale As CheckBox
                Dim check_old As CheckBox
                Dim id_elemento As Label
                Dim omaggiato As CheckBox
                Dim old_omaggiato As CheckBox
                Dim is_gps As Label
                Dim num_elemento As Label
                Dim tipologia_franchigia As Label
                Dim sottotipologia_franchigia As Label

                Dim old_sel As CheckBox
                Dim id_gruppoList As Label

                Dim protezione_plus As Boolean = False  'aggiunto 16.11.2021
                Dim Eliminazione_res As Boolean = False  'aggiunto 16.11.2021

                'SCORRO LA LISTA CERCANDO, ALL'INTERNO DEL GRUPPO SELEZIONATO, GLI ELEMENTI SELEZIONABILI (OVVERO OVE LA CHECKBOX E' SELEZIONATA)
                'PER QUESTI ELEMENTI CONTROLLO SE E' STATO SELEZIONATO (E NON LO ERA PRECEDENTEMENTE) O SE E' STATO DESELEZIONATO (RISPETTO
                'ALLA VOLTA PRECEDENTE) AGGIORNANDO IL TOTALE



                For i = 0 To listPreventiviCosti.Items.Count - 1

                    id_gruppo = listPreventiviCosti.Items(i).FindControl("id_gruppoLabel")

                    If id_gruppo.Text = id_gruppoScelto.Text Then

                        idgruppo = id_gruppo.Text   'aggiunto 16.11.2021
                        check_attuale = listPreventiviCosti.Items(i).FindControl("chkScegli")
                        check_old = listPreventiviCosti.Items(i).FindControl("chkOldScegli")
                        id_elemento = listPreventiviCosti.Items(i).FindControl("id_elemento")
                        omaggiato = listPreventiviCosti.Items(i).FindControl("chkOmaggio")
                        is_gps = listPreventiviCosti.Items(i).FindControl("is_gps")
                        num_elemento = listPreventiviCosti.Items(i).FindControl("num_elemento")
                        tipologia_franchigia = listPreventiviCosti.Items(i).FindControl("tipologia_franchigia")
                        sottotipologia_franchigia = listPreventiviCosti.Items(i).FindControl("sottotipologia_franchigia")

                        If id_elemento.Text = "248" Then 'protezione plus   16.11.2021
                            If check_attuale.Checked = True Then
                                protezione_plus = True
                            Else
                                protezione_plus = False
                            End If

                        End If

                        If id_elemento.Text = "223" Then 'eliminazione responsabilità   16.11.2021
                            If check_attuale.Checked = True Then
                                Eliminazione_res = True
                            Else
                                Eliminazione_res = False
                            End If
                        End If

                        If id_elemento.Text = "203" Then 'eliminazione responsabilità   16.11.2021
                            id_elemento.Text = "203"
                        End If

                        If id_elemento.Text = "204" Then 'eliminazione responsabilità   16.11.2021
                            id_elemento.Text = "204"
                        End If

                        If check_attuale.Visible Or (Not check_attuale.Visible And omaggiato.Visible) Then
                            old_omaggiato = listPreventiviCosti.Items(i).FindControl("chkOldOmaggio")
                            'SE E' E' UNA NUOVA SELEZIONE O SE E' STATO RICHIESTO L'OMAGGIO SENZA SELEZIONARE IL CHECK 'seleziona' (A MENO CHE L'ELEMENTO NON SIA STATO PRECEDENTEMENTE SELEZIONATO)
                            If (check_attuale.Checked And Not check_old.Checked) Or (omaggiato.Checked And Not old_omaggiato.Checked And Not (check_attuale.Checked And check_old.Checked)) Then
                                'AGGIUNGO IL COSTO DELL'ACCESSORIO AL TOTALE O LO OMAGGIO

                                check_attuale.Checked = True 'NEL CASO IN CUI SIA STATO SELEZIONATO SOLO 'OMAGGIO'

                                If omaggiato.Checked Then
                                    'funzioni.omaggio_accessorio(True, False, False, idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text)
                                    If listGruppi.Visible Then
                                        For j = 0 To listGruppi.Items.Count - 1
                                            old_sel = listGruppi.Items(j).FindControl("old_sel_gruppo")
                                            If old_sel.Checked Then
                                                id_gruppoList = listGruppi.Items(j).FindControl("id_gruppo")
                                                funzioni.omaggio_accessorio(True, False, False, idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoList.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text, sottotipologia_franchigia.Text)
                                            End If
                                        Next
                                    Else
                                        funzioni.omaggio_accessorio(True, False, False, idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text, sottotipologia_franchigia.Text)
                                    End If
                                Else
                                    'aggiungi_costo_accessorio(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, "", "", "")
                                    If listGruppi.Visible Then
                                        For j = 0 To listGruppi.Items.Count - 1
                                            old_sel = listGruppi.Items(j).FindControl("old_sel_gruppo")
                                            If old_sel.Checked Then
                                                id_gruppoList = listGruppi.Items(j).FindControl("id_gruppo")
                                                aggiungi_costo_accessorio(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoList.Text, id_elemento.Text, "", "", "", False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                                            End If
                                        Next
                                    Else
                                        aggiungi_costo_accessorio(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, "", "", "", False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                                    End If
                                End If

                                'SE E' STATO AGGIUNTO IL SECONDO GUIDATORE DEVO AGGIUNGERE, SE NECESSARIO, IL COSTO DELLO YOUNG DRIVER
                                If id_elemento.Text = Costanti.Id_Secondo_Guidatore Then
                                    'nuovo_accessorio(get_id_young_driver(), id_gruppoScelto.Text, "YOUNG", txtEtaPrimo.Text, txtEtaSecondo.Text)
                                    If listGruppi.Visible Then
                                        For j = 0 To listGruppi.Items.Count - 1
                                            old_sel = listGruppi.Items(j).FindControl("old_sel_gruppo")
                                            If old_sel.Checked Then
                                                id_gruppoList = listGruppi.Items(j).FindControl("id_gruppo")
                                                nuovo_accessorio(get_id_young_driver(), id_gruppoList.Text, "YOUNG", txtEtaPrimo.Text, txtEtaSecondo.Text)
                                            End If
                                        Next
                                    Else
                                        nuovo_accessorio(get_id_young_driver(), id_gruppoScelto.Text, "YOUNG", txtEtaPrimo.Text, txtEtaSecondo.Text)
                                    End If
                                End If

                                'SE E' STATO AGGINTO IL GPS AGGIUNGO EVENTUALMENTE IL VAL
                                If is_gps.Text = "True" Then
                                    If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
                                        If listGruppi.Visible Then
                                            For j = 0 To listGruppi.Items.Count - 1
                                                old_sel = listGruppi.Items(j).FindControl("old_sel_gruppo")
                                                If old_sel.Checked Then
                                                    id_gruppoList = listGruppi.Items(j).FindControl("id_gruppo")
                                                    nuovo_accessorio("", id_gruppoList.Text, "VAL_GPS", txtEtaPrimo.Text, txtEtaSecondo.Text)
                                                End If
                                            Next
                                        Else
                                            nuovo_accessorio("", id_gruppoScelto.Text, "VAL_GPS", txtEtaPrimo.Text, txtEtaSecondo.Text)
                                        End If
                                    End If
                                End If
                            ElseIf (check_attuale.Checked And check_old.Checked) And (omaggiato.Checked And Not old_omaggiato.Checked) Then
                                'DOPO AVER AGGIUNTO IL COSTO DELL'ACCESSORIO AL TOTALE SI DECIDE DI OMAGGIARLO
                                'funzioni.omaggio_accessorio(True, False, False, idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text)
                                If listGruppi.Visible Then
                                    For j = 0 To listGruppi.Items.Count - 1
                                        old_sel = listGruppi.Items(j).FindControl("old_sel_gruppo")
                                        If old_sel.Checked Then
                                            id_gruppoList = listGruppi.Items(j).FindControl("id_gruppo")
                                            funzioni.omaggio_accessorio(True, False, False, idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoList.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text, sottotipologia_franchigia.Text)
                                        End If
                                    Next
                                Else
                                    funzioni.omaggio_accessorio(True, False, False, idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text, sottotipologia_franchigia.Text)
                                End If
                            ElseIf (check_attuale.Checked And check_old.Checked) And (Not omaggiato.Checked And old_omaggiato.Checked) Then
                                'DOPO AVER AGGIUNTO IL COSTO DELL'ACCESSORIO E AVERLO OMAGGIATO TOLGO L'OMAGGIO
                                'funzioni.omaggio_accessorio(False, True, False, idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text)
                                If listGruppi.Visible Then
                                    For j = 0 To listGruppi.Items.Count - 1
                                        old_sel = listGruppi.Items(j).FindControl("old_sel_gruppo")
                                        If old_sel.Checked Then
                                            id_gruppoList = listGruppi.Items(j).FindControl("id_gruppo")
                                            funzioni.omaggio_accessorio(False, True, False, idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoList.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text, sottotipologia_franchigia.Text)
                                        End If
                                    Next
                                Else
                                    funzioni.omaggio_accessorio(False, True, False, idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text, sottotipologia_franchigia.Text)
                                End If
                            ElseIf Not check_attuale.Checked And check_old.Checked And Not omaggiato.Checked Then
                                'TOLGO IL COSTO DELL'ACCESSORIO DAL TOTALE A MENO CHE NON ERA OMAGGIATO
                                If old_omaggiato.Checked Then
                                    'funzioni.omaggio_accessorio(False, False, True, idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text)
                                    If listGruppi.Visible Then
                                        For j = 0 To listGruppi.Items.Count - 1
                                            old_sel = listGruppi.Items(j).FindControl("old_sel_gruppo")
                                            If old_sel.Checked Then
                                                id_gruppoList = listGruppi.Items(j).FindControl("id_gruppo")
                                                funzioni.omaggio_accessorio(False, False, True, idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoList.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text, sottotipologia_franchigia.Text)
                                            End If
                                        Next
                                    Else
                                        funzioni.omaggio_accessorio(False, False, True, idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text, sottotipologia_franchigia.Text)
                                    End If
                                Else
                                    'funzioni.rimuovi_costo_accessorio(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, "", "SCELTA")
                                    If listGruppi.Visible Then
                                        For j = 0 To listGruppi.Items.Count - 1
                                            old_sel = listGruppi.Items(j).FindControl("old_sel_gruppo")
                                            If old_sel.Checked Then
                                                id_gruppoList = listGruppi.Items(j).FindControl("id_gruppo")
                                                funzioni.rimuovi_costo_accessorio(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoList.Text, id_elemento.Text, "", "SCELTA", dropTipoCommissione.SelectedValue)
                                            End If
                                        Next
                                    Else
                                        funzioni.rimuovi_costo_accessorio(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, "", "SCELTA", dropTipoCommissione.SelectedValue)
                                    End If
                                End If

                                If id_elemento.Text = Costanti.Id_Secondo_Guidatore Then
                                    Dim id_young_driver As String = get_id_young_driver()
                                    'RIMUOVO IL COSTO DELLO JOUNG DRIVER NEL CASO IN CUI SI RIMUOVE "SECONDO GUIDATORE"
                                    If funzioni.esiste_young_driver_secondo_guidatore(id_young_driver, id_gruppoScelto.Text, numCalcolo.Text, idPreventivo.Text, "", "", "") Then
                                        'funzioni.rimuovi_costo_accessorio(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoScelto.Text, id_young_driver, "2", "EXTRA")
                                        If listGruppi.Visible Then
                                            For j = 0 To listGruppi.Items.Count - 1
                                                old_sel = listGruppi.Items(j).FindControl("old_sel_gruppo")
                                                If old_sel.Checked Then
                                                    id_gruppoList = listGruppi.Items(j).FindControl("id_gruppo")
                                                    funzioni.rimuovi_costo_accessorio(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoList.Text, id_young_driver, "2", "EXTRA", dropTipoCommissione.SelectedValue)
                                                End If
                                            Next
                                        Else
                                            funzioni.rimuovi_costo_accessorio(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoScelto.Text, id_young_driver, "2", "EXTRA", dropTipoCommissione.SelectedValue)
                                        End If
                                    End If
                                End If

                                'SE E' STATO RIMOSSO IL GPS RIMUOVO L'EVENTUALE VAL
                                If is_gps.Text = "True" And dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
                                    Dim id_val_gps As String = funzioni_comuni.getIdValGps()
                                    If id_val_gps <> "" Then
                                        If listGruppi.Visible Then
                                            For j = 0 To listGruppi.Items.Count - 1
                                                old_sel = listGruppi.Items(j).FindControl("old_sel_gruppo")
                                                If old_sel.Checked Then
                                                    id_gruppoList = listGruppi.Items(j).FindControl("id_gruppo")
                                                    funzioni.rimuovi_costo_accessorio(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoList.Text, id_val_gps, "", "EXTRA", dropTipoCommissione.SelectedValue)
                                                End If
                                            Next
                                        Else
                                            funzioni.rimuovi_costo_accessorio(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppoScelto.Text, id_val_gps, "", "EXTRA", dropTipoCommissione.SelectedValue)
                                        End If

                                    End If
                                End If

                            End If
                        End If


                    End If
                Next
                'lblFocus.Focus()



                '# VERIFICHE PER LE CONDIZIONI DI PPLUS 07.04.2022
                'tolgo il parametro gruppo da visualizza e nascondi franchigie in modo
                'da aggiornare a tutti i gruppi presenti nel calcolo
                'idgruppo = ""

                '23.11.2021
                'se ck PPlus Disattivo o non visibile/ Rid Furto Attiva / rid Danni Attiva
                If tresponse.IndexOf("100") > -1 And (tresponse.IndexOf("248") = -1 Or protezione_plusT = False) And tresponse.IndexOf("170") > -1 Then
                    'deve anche assegnare la franchigia_attiva x 181 e toglie la 204 (ridotta)
                    funzioni_comuni.visualizza_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "204", "")      'visualizza intera
                    funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "181", "")        'nasconde la ridotta

                    'attiva la Fra rid danni x 100 
                    funzioni_comuni.visualizza_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "203", "")      'visualizza la ridotta
                    funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "180", "")        'nasconde franc Intera

                End If



                '23.11.2021
                'se CK Protezione Plus o Eliminazione Respons. Attivata 
                If tresponse.IndexOf("248") > -1 Or tresponse.IndexOf("223") > -1 Then
                    'nasconde le franchigie
                    funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "204", "")
                    funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "181", "")
                    funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "203", "")
                    funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "180", "")
                End If


                '23.11.2021
                'se CK Protezione Plus Attivata 
                If tresponse.IndexOf("248") > -1 Then
                    'imposta il valore selezionato sui ck Riduzioni
                    funzioni_comuni.Aggiorna_Ck(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "170", "1")
                    funzioni_comuni.Aggiorna_Ck(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "100", "1")

                    'aggiorna Totale includendo il costo delle riduzioni quando presente PPLUS
                    'se non selezionate
                    If tresponse.IndexOf("100") = -1 Then   'costo del riduzione danni se nn presente
                        visualizza_franchigie_Aggiorna_Costo(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "100")
                    End If
                    If tresponse.IndexOf("170") = -1 Then   'costo del riduzione furto se nn presente
                        visualizza_franchigie_Aggiorna_Costo(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "170")
                    End If

                    'aggiorna selezionato e  ck PPLUS
                    funzioni_comuni.Aggiorna_Ck(idPreventivo.Text, "", "", "", numCalcolo.Text, "", "248", "1") 'aggiornato 07.04.2022
                End If


                If tresponse.IndexOf("223") > -1 Then

                    'aggiorna Totale includendo il costo delle riduzioni quando presente PPLUS
                    'se non selezionate
                    If tresponse.IndexOf("100") > -1 Then   'costo del riduzione danni se nn presente
                        'visualizza_franchigie_Aggiorna_Costo(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "100")
                        'funzioni_comuni.SetOpzione(listPreventiviCosti, "100", True, False, False)
                    End If
                    If tresponse.IndexOf("170") > -1 Then   'costo del riduzione furto se nn presente
                        'visualizza_franchigie_Aggiorna_Costo(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "170")
                        'funzioni_comuni.SetOpzione(listPreventiviCosti, "170", True, False, False)
                    End If


                End If

                'aggiorna la ListPreventivi Costi
                listPreventiviCosti.DataBind()

                'recupera valore originario del deposito cauzionale 19.01.22
                Dim impDepDefault As String = GetValoreDepositoCauzionaleDefault(idgruppo)
                Dim impDepCalcolato As String = impDepDefault


                ''Impostazioni delle franchigie PRIMA del databind




                If id_rd = True Then
                    funzioni_comuni.visualizza_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "203", "")
                    funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "180", "")
                End If
                If id_rf = True Then
                    funzioni_comuni.visualizza_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "204", "")
                    funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "181", "")
                End If

                'se PPLUS Attiva
                If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = True Then
                    funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "203", "")
                    funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "180", "")
                    funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "204", "")
                    funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "181", "")

                    'Riduce importo Deposito cauzionale 19.01.22
                    impDepCalcolato = "200"
                    funzioni_comuni.aggiorna_deposito_cauzionale(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)

                End If


                'modificata 12.01.2022
                If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = False And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "223", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "100", "ck") = False Or funzioni_comuni.VerificaOpzione(listPreventiviCosti, "170", "ck") = False Then

                    If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "100", "ck") = True Then
                        funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "180", "")
                        funzioni_comuni.visualizza_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "203", "")
                    Else
                        funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "203", "")
                        funzioni_comuni.visualizza_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "180", "")
                    End If
                    If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "170", "ck") = True Then
                        funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "181", "")
                        funzioni_comuni.visualizza_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "204", "")
                    Else
                        funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "204", "")
                        funzioni_comuni.visualizza_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "181", "")
                    End If
                End If

                'aggiunta 12.01.2022
                If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = False And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "223", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "100", "ck") = True And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "170", "ck") = True Then

                    If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "100", "ck") = True Then
                        funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "180", "")
                        funzioni_comuni.visualizza_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "203", "")
                    Else
                        funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "203", "")
                        funzioni_comuni.visualizza_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "180", "")
                    End If

                    If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "170", "ck") = True Then
                        funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "181", "")
                        funzioni_comuni.visualizza_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "204", "")
                    Else
                        funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "204", "")
                        funzioni_comuni.visualizza_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "181", "")
                    End If
                End If

                '16.01.22 se ELIRES nasconde franchigie
                If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "223", "ck") = True Then
                    funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "203", "")
                    funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "180", "")
                    funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "204", "")
                    funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "181", "")

                    '19.01.22
                    impDepCalcolato = "200"
                    funzioni_comuni.aggiorna_deposito_cauzionale(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
                End If


                'se RIDUZIONI ATTIVE e PPLUS disabilitate riduce valore deposito cauzionale 19.01.22
                ' se attive ma senza PPLUS
                If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "100", "ck") = True _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "170", "ck") = True _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = False Then
                    impDepCalcolato = "300"
                    funzioni_comuni.aggiorna_deposito_cauzionale(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
                End If

                ''se RIDUZIONI ATTIVE e PPLUS Abilitata riduce valore deposito cauzionale 19.01.22 (non serve perchè si attiva con PPLUS
                'If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "100", "ck") = True _
                '    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "170", "ck") = True _
                '    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = True Then
                '    impDepCalcolato = "200"
                '    funzioni_comuni.aggiorna_deposito_cauzionale(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
                'End If


                'se opzione 'Riduzione Deposito 50%' importo = deposito / 2
                If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "234", "ck") = True Then
                    impDepCalcolato = FormatNumber((CDbl(impDepCalcolato) / 2), 2)
                    funzioni_comuni.aggiorna_deposito_cauzionale(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
                End If


                'se tutte le condizioni non attive x deposito riporta valore a default 19.01.22
                If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "223", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "100", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "170", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "234", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = False Then
                    'recupera valore originario del deposito cauzionale
                    impDepCalcolato = impDepDefault
                    '19.01.22
                    funzioni_comuni.aggiorna_deposito_cauzionale(idPreventivo.Text, "", "", "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
                End If


                'reset variabili
                id_elires = False
                id_pplus = False
                id_rd = False
                id_rf = False

                listPreventiviCosti.DataBind()



                'deve ciclare x tutti ck della list 07.04.2022



                'Assegnazione dei CK dopo il databind
                If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "223", "ck") = True Then
                    SetOpzione(listPreventiviCosti, "248", False, False, False)
                End If

                If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = True Then
                    SetOpzione(listPreventiviCosti, "223", False, False, False)
                End If

                'aggiunta 12.01.2022
                If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = False And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "223", "ck") = False Then
                    SetOpzione(listPreventiviCosti, "223", False, True, False)
                End If




            Else 'verifica su prenotazione o contratto inesistente

                listPreventiviCosti.DataBind()
                Libreria.genUserMsgBox(Me, "Prenotazione o contratto già salvato oppure preventivo non più esistente - Richiamare la prenotazione o il contratto per modificare gli accessori.")

            End If


        Catch ex As Exception
            HttpContext.Current.Response.Write("error itemCommand Aggiorna: <br/>" & ex.Message & "<br/>" & "" & "<br/>")
        End Try

    End Sub

    Public Sub VerificaCk(ByVal tResponse As String, ByVal tResponseOld As String, ByRef id_gruppoSceltoT As Label)
        '24.11.2021 aggiunta procedura x controllo condizioni dei ck attivati
        '23.11.2021
        'verifica tutte le condizioni per l'abilitazione o disabilitazione dei ck
        'solo x visualizzazione - se bisogna intervenire sui calcoli modificare nel blocco prima del listPreventiviCosti.DATABIND()

        Dim id_gruppoScelto As Label = id_gruppoSceltoT  'e.Item.FindControl("id_gruppoLabel")   'modificato il 18.11.2021 passato il valore di sopra
        Dim id_gruppo As Label
        Dim check_attuale As CheckBox
        Dim check_old As CheckBox
        Dim id_elemento As Label
        Dim omaggiato As CheckBox
        Dim old_omaggiato As CheckBox
        Dim is_gps As Label
        Dim num_elemento As Label
        Dim tipologia_franchigia As Label
        Dim sottotipologia_franchigia As Label

        Dim old_sel As CheckBox
        Dim id_gruppoList As Label

        Dim protezione_plus As Boolean = False  'aggiunto 16.11.2021
        Dim Eliminazione_res As Boolean = False  'aggiunto 16.11.2021


        For i = 0 To listPreventiviCosti.Items.Count - 1

            check_attuale = listPreventiviCosti.Items(i).FindControl("chkScegli")
            check_old = listPreventiviCosti.Items(i).FindControl("chkOldScegli")
            id_elemento = listPreventiviCosti.Items(i).FindControl("id_elemento")
            omaggiato = listPreventiviCosti.Items(i).FindControl("chkOmaggio")
            is_gps = listPreventiviCosti.Items(i).FindControl("is_gps")
            num_elemento = listPreventiviCosti.Items(i).FindControl("num_elemento")
            tipologia_franchigia = listPreventiviCosti.Items(i).FindControl("tipologia_franchigia")
            sottotipologia_franchigia = listPreventiviCosti.Items(i).FindControl("sottotipologia_franchigia")

            'se PPLUS Attiva disabilita ELiRes
            If tResponse.IndexOf("248") > -1 And id_elemento.Text = "223" Then
                check_attuale.Enabled = False
            End If
            'se EliRes Attiva disabilita PPLUS
            If tResponse.IndexOf("223") > -1 And id_elemento.Text = "248" Then
                check_attuale.Enabled = False
            End If
            'se PPLUS Attiva e RD nnAttiva abilita ck PPLUS
            If tResponse.IndexOf("248") > -1 And tResponse.IndexOf("100") = -1 And id_elemento.Text = "248" Then
                check_attuale.Enabled = True
                check_attuale.Checked = False
            End If
            'se PPLUS Attiva e RD Attiva abilita ck RD disable
            If tResponse.IndexOf("248") > -1 And tResponse.IndexOf("100") > -1 And tResponse.IndexOf("170") > -1 And id_elemento.Text = "100" Then
                check_attuale.Enabled = False
            End If
            'se PPLUS Attiva e RD Attiva abilita ck RF disable
            If tResponse.IndexOf("248") > -1 And tResponse.IndexOf("100") > -1 And tResponse.IndexOf("170") > -1 And id_elemento.Text = "170" Then
                check_attuale.Enabled = False
            End If
            'se PPLUS nnAttiva e RD Attiva e RF attiva abilita ck RD enable
            If tResponse.IndexOf("248") = -1 And tResponse.IndexOf("100") > -1 And tResponse.IndexOf("170") > -1 And id_elemento.Text = "100" Then
                check_attuale.Enabled = True
            End If
            'se PPLUS nnAttiva e RD Attiva e RF attiva abilita ck RF enable
            If tResponse.IndexOf("248") = -1 And tResponse.IndexOf("100") > -1 And tResponse.IndexOf("170") > -1 And id_elemento.Text = "170" Then
                check_attuale.Enabled = True
            End If
            'se PPLUS Attiva e RD nnAttiva e abilita ck RD CKTrue / Disable
            If tResponse.IndexOf("248") > -1 And tResponse.IndexOf("100") = -1 And id_elemento.Text = "100" Then
                check_attuale.Checked = True
                check_attuale.Enabled = False
            End If
            'se PPLUS Attiva e RF nnAttiva e abilita ck RF CKTrue / Disable
            If tResponse.IndexOf("248") > -1 And tResponse.IndexOf("170") = -1 And id_elemento.Text = "170" Then
                check_attuale.Checked = True
                check_attuale.Enabled = False
            End If

            ''se EliRes Attiva e RD noCK -> CkFalse / Disabled
            'If tResponse.IndexOf("223") > -1 And tResponse.IndexOf("100") = -1 And id_elemento.Text = "100" Then
            '    check_attuale.Checked = False
            '    check_attuale.Enabled = False
            'End If

            ''se EliRes Attiva e RF noCK -> CkFalse / Disabled
            'If tResponse.IndexOf("223") > -1 And tResponse.IndexOf("170") = -1 And id_elemento.Text = "170" Then
            '    check_attuale.Checked = False
            '    check_attuale.Enabled = False
            'End If


        Next  'ciclo x verifica delle condizioni di visualizzazione dei CK su ListPreventiviCosti 23.11.2021



    End Sub


    Protected Sub listPreventiviCosti_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles listPreventiviCosti.ItemCommand

        Dim idgruppo As String = ""

        If e.CommandName = "Aggiorna" Then


            ''Creata procedura Aggiorna e separata 18.11.2021
            Dim lbl_idgr As Label = e.Item.FindControl("id_gruppoLabel")
            idgruppo = lbl_idgr.Text
            btnAggiorna(idgruppo, lbl_idgr, False)

            ''Fine codice per command "Aggiorna"


        ElseIf e.CommandName = "preventivo" Then


            'setPadding("preventivo_salva")

            Try
                If tipo_preventivo.Text = "nuovo" Then

                    'E' STATO RICHIESTO DI SALVARE IL PREVENTIVO PER UN SINGOLO GRUPPO.-------------------------------------------------------------
                    'PER PRIMA COSA ELIMINO GLI ALTRI GRUPPI DA preventivi_costi

                    lblTipoDocumento.Text = ""
                    lblNumPreventivo.Text = ""
                    lblOperatore.Text = ""
                    Dim id_gruppo_scelto As Label = e.Item.FindControl("id_gruppoLabel")
                    id_gruppo_auto_scelto.Text = id_gruppo_scelto.Text

                    'FACCIO IN MODO CHE NELLA LISTA DEI GRUPPI SIA SELEZIONATO SOLO QUELLO SCELTO---------------------------------------------------
                    table_gruppi.Visible = True

                    For i = 0 To listGruppi.Items.Count - 1
                        Dim id_gr As Label = listGruppi.Items(i).FindControl("id_gruppo")
                        Dim check As CheckBox = listGruppi.Items(i).FindControl("sel_gruppo")
                        If id_gr.Text <> id_gruppo_scelto.Text Then
                            check.Checked = False
                        Else
                            check.Checked = True
                        End If
                    Next
                    '-------------------------------------------------------------------------------------------------------------------------------
                    'ELIMINO LE RIGHE DI CALCOLO RELATIVE AGLI ALTRI GRUPPI ------------------------------------------------------------------------
                    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()
                    Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM preventivi_costi WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "' AND id_gruppo<>'" & id_gruppo_scelto.Text & "'", Dbc)
                    Cmd.ExecuteNonQuery()

                    listPreventiviCosti.DataBind()

                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing
                    '------------------------------------------------------------------------------------------------------------------------------

                    'SE E' STATO SELEZIONATO UN GRUPPO PER CUI SONO STATI TROVATI DEI WARNING LI AGGIUNGO ALLA LISTA DI WARNING NELL'APPOSITA TABELLA

                    aggiungi_warning_gruppo_auto(id_gruppo_scelto.Text, idPreventivo.Text, "", "", numCalcolo.Text)

                    table_gruppi.Visible = False 'QUESTA RIGA DEVE STARE SOTTO aggiungi_warning_gruppo_auto(...)

                    tab_preventivo.Visible = True
                    tab_prenotazioni.Visible = False

                    listWarningGruppi.DataBind()

                Else
                    Libreria.genUserMsgBox(Me, "Preventivo già salvato.")
                End If
            Catch ex As Exception
                HttpContext.Current.Response.Write("error itemCommand Preventivo : <br/>" & ex.Message & "<br/>" & "" & "<br/>")
            End Try

        ElseIf e.CommandName = "prenotazione" Then

            setPadding("preventivo_salva")

            Session("salva_prenotazione_da_preventivi") = ""  'svuota session 22.03.2022

            'aggiunto il 24.11.2021 0956

            Dim tresponse As String, tresponseOld As String
            Dim lbl_idgr As Label = e.Item.FindControl("id_gruppoLabel")
            idgruppo = lbl_idgr.Text
            Dim tt As String = GetResponseCk(lbl_idgr.Text, lbl_idgr)
            Dim tris() As String

            tris = Split(tt, "§")
            tresponse = tris(0)
            tresponseOld = tris(1)


            Try

                If preventivo_puo_diventare_prenotazione(idPreventivo.Text) Then

                    Dim id_gruppo_scelto As Label = e.Item.FindControl("id_gruppoLabel")
                    id_conducente.Text = ""
                    txtNomeConducente.Text = ""
                    txtCognomeConducente.Text = ""
                    txtMailConducente.Text = ""
                    txtIndirizzoConducente.Text = ""
                    txtRiferimentoTO.Text = ""
                    lblRifToOld.Text = ""
                    txtVoloOut.Text = ""
                    txtVoloPr.Text = ""
                    txtNote.Text = ""
                    txtDataDiNascita.Text = ""

                    'FACCIO IN MODO CHE NELLA LISTA DEI GRUPPI SIA SELEZIONATO SOLO QUELLO SCELTO---------------------------------------------------
                    For i = 0 To listGruppi.Items.Count - 1
                        Dim id_gr As Label = listGruppi.Items(i).FindControl("id_gruppo")
                        Dim check As CheckBox = listGruppi.Items(i).FindControl("sel_gruppo")
                        If id_gr.Text <> id_gruppo_scelto.Text Then
                            check.Checked = False
                        Else
                            check.Checked = True
                        End If
                    Next
                    table_gruppi.Visible = False
                    '-------------------------------------------------------------------------------------------------------------------------------

                    'ELIMINO LE RIGHE DI CALCOLO RELATIVE AGLI ALTRI GRUPPI ------------------------------------------------------------------------
                    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()
                    Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM preventivi_costi WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "' AND id_gruppo<>'" & id_gruppo_scelto.Text & "'", Dbc)
                    Cmd.ExecuteNonQuery()

                    listPreventiviCosti.DataBind()

                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing
                    '-------------------------------------------------------------------------------------------------------------------------------

                    txtCognomeConducente.Text = txtCognome.Text
                    txtNomeConducente.Text = txtNome.Text
                    txtMailConducente.Text = txtMail.Text
                    txtRifTel.Text = txtTelefono.Text

                    tab_preventivo.Visible = False
                    tab_prenotazioni.Visible = True
                    btnSalvaPrenotazione.Visible = True

                    dropGruppoDaConsegnare.SelectedValue = id_gruppo_scelto.Text

                    'SELEZIONO OBBLIGATORIAMENTE LA DITTA SENZA POTERLA CAMBIARE SE E' STATO SPECIFICATO IL CODICE EDP:

                    If txtCodiceCliente.Text <> "" Then
                        txtNomeDitta.Text = lblNomeDitta.Text
                        id_ditta.Text = getIdDitta(txtCodiceCliente.Text)
                        btnModificaDitta.Visible = False
                    Else
                        txtNomeDitta.Text = ""
                        id_ditta.Text = ""
                        btnModificaDitta.Visible = True
                    End If

                    id_gruppo_auto_scelto.Text = id_gruppo_scelto.Text

                    'dropGruppoDaConsegnare.SelectedValue = id_gruppo_auto_scelto.Text

                    'verifica condizioni CK se attivati 24.11.2021 0950
                    VerificaCk(tresponse, tresponseOld, id_gruppo_scelto)

                    'se PPLUS attiva RD+RF 04.01.2022
                    If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = True Then
                        funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppo_auto_scelto.Text, "203", "204")
                        funzioni_comuni.SetOpzione(listPreventiviCosti, "223", False, False, False)
                        funzioni_comuni.SetOpzione(listPreventiviCosti, "100", True, True, False)
                        funzioni_comuni.SetOpzione(listPreventiviCosti, "170", True, True, False)
                    End If

                Else
                    Libreria.genUserMsgBox(Me, "Prenotazione o contratto già salvato oppure preventivo non più esistente.")
                End If


            Catch ex As Exception
                HttpContext.Current.Response.Write("error itemCommand Prenotazione: <br/>" & ex.Message & "<br/>" & "" & "<br/>")
            End Try

        ElseIf e.CommandName = "contratto" Then


            'aggiunto il 24.11.2021 0956
            'Da Verificare se il blocco seguente è necessario nel caso che si passi a contratti
            'visto che c'è un redirect
            '' BLOCCO VERIFICA
            'Dim tresponse As String, tresponseOld As String
            'Dim lbl_idgr As Label = e.Item.FindControl("id_gruppoLabel")
            'idgruppo = lbl_idgr.Text
            'Dim tt As String = GetResponseCk(lbl_idgr.Text, lbl_idgr)
            'Dim tris() As String

            'tris = Split(tt, "§")
            'tresponse = tris(0)
            'tresponseOld = tris(1)
            '' END BLOCCO VERIFICA

            Try
                If preventivo_puo_diventare_contratto(idPreventivo.Text) Then
                    'SALVO L'ATTUALE RIGA DI CALCOLO IN CONTRATTI
                    Dim id_gruppo_scelto As Label = e.Item.FindControl("id_gruppoLabel")
                    id_gruppo_auto_scelto.Text = id_gruppo_scelto.Text

                    'ELIMINO LE RIGHE DI CALCOLO RELATIVE AGLI ALTRI GRUPPI ------------------------------------------------------------------------
                    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()
                    Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM preventivi_costi WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "' AND id_gruppo<>'" & id_gruppo_scelto.Text & "'", Dbc)
                    Cmd.ExecuteNonQuery()

                    listPreventiviCosti.DataBind()
                    '------------------------------------------------------------------------------------------------------------------------------
                    'SALVO UNA RIGA NON ATTIVA IN CONTRATTI FINO A QUANDO DALL'APPOSITA SCHERMATA NON SI SALVA IL CONTRATTO NON ASSEGNO UN NUOVO NUMERO
                    Dim id_contratto As String
                    Dim data_uscita As String = getDataDb_con_orario(txtDaData.Text & " " & ore1.Text & ":" & minuti1.Text & ":00", Request.ServerVariables("HTTP_HOST"))
                    Dim data_rientro As String = getDataDb_con_orario(txtAData.Text & " " & ore2.Text & ":" & minuti2.Text & ":00", Request.ServerVariables("HTTP_HOST"))

                    Dim id_tariffe_righe As Integer
                    Dim id_tariffa As Integer
                    Dim tipo_tariffa As String = ""
                    Dim codice_tariffa As String

                    If dropTariffeGeneriche.SelectedValue <> "0" Then
                        id_tariffe_righe = dropTariffeGeneriche.SelectedValue
                        codice_tariffa = "'" & Replace(dropTariffeGeneriche.SelectedItem.Text, "'", "''") & "'"
                        tipo_tariffa = "generica"
                    ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                        id_tariffe_righe = dropTariffeParticolari.SelectedValue
                        codice_tariffa = "'" & Replace(dropTariffeParticolari.SelectedItem.Text, "'", "''") & "'"
                        tipo_tariffa = "fonte"
                    End If

                    Dim num_preventivo As String
                    Dim num_prenotazione As String

                    If tipo_preventivo.Text = "richiama" Then
                        'PREVENTIVO SALVATO CHE DIVENTA CONTRATTO - MEMORIZZO L'INFORMAZIONE
                        num_preventivo = "'" & lblNumPreventivo.Text & "'"
                    Else
                        num_preventivo = "NULL"
                    End If

                    If numero_prenotazione.Text <> "" Then
                        'PRENOTAZIONA SALVATA CHE DIVENTA SUBITO CONTRATTO
                        num_prenotazione = "'" & numero_prenotazione.Text & "'"
                    Else
                        num_prenotazione = "NULL"
                    End If

                    Cmd = New Data.SqlClient.SqlCommand("SELECT id_tariffa FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)
                    id_tariffa = Cmd.ExecuteScalar

                    'Cmd = New Data.SqlClient.SqlCommand("SELECT codice FROM tariffe WITH(NOLOCK) WHERE id='" & id_tariffa & "'", Dbc)
                    'codice_tariffa = Cmd.ExecuteScalar

                    'SE E' STATA SPECIFICATA UNA DITTA (ATTRAVERSO IL CODICE EDP, IN QUESTA FASE, VISTO CHE NON E' STATA SALVATA UNA PRENOTAZIONE)
                    'SALVO IL SUO ID IN CONTRATTI MA NON SALVO IL CODICE EDP, ALTRIMENTI DA CONTRATTI BLOCCHEREI LA SCELTA DELLA DITTA A CUI
                    'FATTURARE
                    Dim idDitta As String
                    If txtCodiceCliente.Text <> "" Then
                        idDitta = "'" & getIdDitta(txtCodiceCliente.Text) & "'"
                    Else
                        idDitta = "NULL"
                    End If

                    Dim a_carico_del_broker As String
                    Dim giorni_to As String

                    If tariffa_broker.Text = "1" Then
                        a_carico_del_broker = "'" & Replace(getCostoACaricoDelBroker(numCalcolo.Text), ",", ".") & "'"
                        giorni_to = "'" & txtNumeroGiorni.Text & "'"
                    Else
                        a_carico_del_broker = "NULL"
                        giorni_to = "NULL"
                    End If

                    Dim condizione_commissioni As String = ""
                    If dropFonteCommissionabile.Visible Then
                        condizione_commissioni = ",'" & dropFonteCommissionabile.SelectedValue & "','" & dropTipoCommissione.SelectedValue & "','" & Replace(txtPercentualeCommissionabile.Text, ",", ".") & "','" & txtNumeroGiorni.Text & "'"
                    Else
                        condizione_commissioni = ",NULL,NULL,NULL,NULL"
                    End If

                    Dim sqlStr As String = "INSERT INTO contratti (num_contratto, num_calcolo, status, attivo, id_stazione_uscita, id_stazione_presunto_rientro, " &
                    "data_uscita, data_presunto_rientro, id_gruppo_auto, giorni, giorni_to, " &
                    "ID_GRUPPO_APP, eta_primo_guidatore, eta_secondo_guidatore, id_fonte, id_cliente, id_tariffa, id_tariffe_righe, tariffa_rack_utilizzata, tipo_tariffa, sconto_applicato, tipo_sconto, importo_a_carico_del_broker," &
                    "CODTAR, id_operatore_creazione, data_creazione, num_preventivo, num_prenotazione, id_fonte_commissionabile, tipo_commissione, commissione_percentuale, giorni_commissioni)"

                    sqlStr = sqlStr & " VALUES "

                    sqlStr = sqlStr & " ('','0','0','0','" & dropStazionePickUp.SelectedValue & "','" & dropStazioneDropOff.SelectedValue & "'," &
                    "convert(datetime,'" & data_uscita & "',102),convert(datetime,'" & data_rientro & "',102),'" & id_gruppo_scelto.Text & "','" & txtNumeroGiorni.Text & "'," & giorni_to & "," &
                    "'" & id_gruppo_scelto.Text & "','" & txtEtaPrimo.Text & "','" & txtEtaSecondo.Text & "','" & dropTipoCliente.SelectedValue & "'," & idDitta & "," &
                    "'" & id_tariffa & "','" & id_tariffe_righe & "','0','" & tipo_tariffa & "','" & txt_sconto_new.Text & "','" & dropTipoSconto.SelectedValue & "'," & a_carico_del_broker & "," & codice_tariffa & ", " &
                    "'" & Request.Cookies("SicilyRentCar")("IdUtente") & "',convert(datetime,GetDate(),102)," & num_preventivo & "," & num_prenotazione & condizione_commissioni & ")"

                    'Response.Write(sqlStr & "---" & Request.ServerVariables("HTTP_HOST"))
                    'Response.End()
                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.ExecuteNonQuery()

                    Cmd = New Data.SqlClient.SqlCommand("SELECT @@IDENTITY FROM contratti WITH(NOLOCK)", Dbc)
                    id_contratto = Cmd.ExecuteScalar
                    idContratto.Text = id_contratto

                    '#salvo aggiunto 29.06.2023 
                    'se campi list presenti in prenotazione li aggiunge in contratti
                    Dim ris() As String = funzioni_comuni_new.getVerificaValoriList("preventivi", idPreventivo.Text, "0")
                    If ris(0) <> "" Then 'valori presenti nei campi list di prenotazioni
                        Dim utbl As String = funzioni_comuni_new.SalvaListTariffe("contratti", id_contratto, ris(0), ris(1), ris(2), ris(4), ris(5))
                    End If
                    '@end salvo

                    'SALVO LA RIGA DI CALCOLO E DI WARNING NELLE TABELLE CONTRATTI_COSTI E CONTRATTI_WARNING
                    sqlStr = "INSERT INTO contratti_costi (id_documento, num_calcolo, ordine_stampa, id_gruppo, id_elemento, num_elemento, nome_costo," &
                    "selezionato,omaggiato,prepagato, valore_costo,valore_percentuale,imponibile,iva_imponibile,imponibile_scontato,iva_imponibile_scontato," &
                    "imponibile_onere, iva_onere,aliquota_iva,iva_inclusa,scontabile,omaggiabile,acquistabile_nolo_in_corso,id_a_carico_di,id_metodo_stampa,obbligatorio, franchigia_attiva, id_unita_misura,qta, packed, commissioni_imponibile, commissioni_iva, commissioni_imponibile_originale, commissioni_iva_originale) " &
                    "(SELECT '" & id_contratto & "','0', ordine_stampa, id_gruppo,id_elemento,num_elemento,nome_costo," &
                    "selezionato,omaggiato, prepagato, valore_costo,valore_percentuale,imponibile,iva_imponibile,imponibile_scontato,iva_imponibile_scontato," &
                    "imponibile_onere, iva_onere, aliquota_iva,iva_inclusa,scontabile,omaggiabile,acquistabile_nolo_in_corso,id_a_carico_di,id_metodo_stampa,obbligatorio, franchigia_attiva, id_unita_misura,qta, packed, commissioni_imponibile, commissioni_iva, commissioni_imponibile_originale, commissioni_iva_originale FROM preventivi_costi WITH(NOLOCK) WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "')"

                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.ExecuteScalar()

                    'NON SALVO I WARNING - IN QUESTO MOMENTO ENTRANDO SU CONTRATTO DA PREVENTIVO O DA PRENOTAZIONE IL SISTEMA RICALCOLA TUTTI
                    'I WARNING
                    'sqlStr = "INSERT INTO contratti_warning (id_documento, num_calcolo, warning, id_operatore, tipo) " & _
                    '"(SELECT '" & id_contratto & "','1',warning,id_operatore,tipo FROM preventivi_warning WITH(NOLOCK) WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "')"

                    'Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    'Cmd.ExecuteScalar()

                    'SE E' STATO SALVATO DIRETTAMENTE IL CONTRATTO SENZA EFFETTUARE IL PREVENTIVO ELIMINO LE RIGHE DA PREVENTIVI - PREVENTIVI_COSTI E PREVENTIVI_WARNING
                    If tipo_preventivo.Text = "nuovo" Then
                        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi_costi WHERE id_documento='" & idPreventivo.Text & "'", Dbc)
                        Cmd.ExecuteScalar()
                        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi_warning WHERE id_documento='" & idPreventivo.Text & "'", Dbc)
                        Cmd.ExecuteScalar()
                        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi WHERE id='" & idPreventivo.Text & "'", Dbc)
                        Cmd.ExecuteScalar()
                    End If

                    If tipo_preventivo.Text = "nuovo" And numero_prenotazione.Text = "" Then
                        'SE E' STATO SELEZIONATO UN GRUPPO PER CUI SONO STATI TROVATI DEI WARNING LI AGGIUNGO ALLA LISTA DI WARNING NELL'APPOSITA TABELLA
                        '(VIENE FATTO SOLO SE NON E' STATO SALVATO UN PREVENTIVO O UNA PRENOTAZIONE: IN QUESTO CASO IL WARNING ERA GIA' PRESENTE 
                        'NELLA TABELLA DI WARNING CHE E' STATA COPIATA
                        aggiungi_warning_gruppo_auto(id_gruppo_scelto.Text, "", "", id_contratto, "1")
                    End If

                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing

                    Session("id_contratto_from_preventivi") = id_contratto

                    If tipo_preventivo.Text = "richiama" Then
                        Session("idPreventivo") = idPreventivo.Text
                    End If

                    'If txtNumPreventivo.Text <> "" Then
                    '    Session("num_pre") = num_prenotazione
                    'End If

                    setta_session_x_contratto()

                    'imposta sessione per sconto se presente salvo 18.02.2023
                    Session("sconto_x_contratto_da_preventivi") = txt_sconto_new.Text




                    Response.Redirect("contratti.aspx")
                Else
                    Libreria.genUserMsgBox(Me, "Contratto già salvato oppure preventivo non più esistente.")
                End If
            Catch ex As Exception
                HttpContext.Current.Response.Write("error itemCommand Contratto : <br/>" & ex.Message & "<br/>" & "" & "<br/>")

            End Try

        End If







    End Sub

    Protected Sub setta_session_x_contratto()

        'TRASPORTO GLI EVENTUALI DATI PER LA RICERCA IN MODO DA TORNARE IN QUESTA PAGINA CON LO STESSO STATO---------------------

        Session("tipo_documento") = dropTipoDocumento.SelectedValue
        Session("stato_documento") = dropStatoContratto.SelectedValue

        If cercaStazionePresuntoRientro.SelectedValue <> "0" Then
            Session("pres_rientro") = cercaStazionePresuntoRientro.SelectedValue
        End If

        If txtCercaNumStaz.Text <> "" Then
            Session("num_pre_staz") = txtCercaNumStaz.Text
        End If

        If txtCercaNumInterno.Text <> "" Then
            Session("num_pre_num") = txtCercaNumInterno.Text
        End If

        If txtCercaNome.Text <> "" Then
            Session("nome") = txtCercaNome.Text
        End If
        If txtCercaCognome.Text <> "" Then
            Session("cognome") = txtCercaCognome.Text
        End If
        If txtCercaPickUpDa.Text <> "" Then
            Session("pickUpDa") = txtCercaPickUpDa.Text
        End If
        If txtCercaPickUpA.Text <> "" Then
            Session("pickUpA") = txtCercaPickUpA.Text
        End If
        If txtCercaDropOffDa.Text <> "" Then
            Session("dropOffDa") = txtCercaDropOffDa.Text
        End If
        If txtCercaDropOffA.Text <> "" Then
            Session("dropOffA") = txtCercaDropOffA.Text
        End If
        If cercaStazioneDropOff.SelectedValue <> "0" Then
            Session("stazRientro") = cercaStazioneDropOff.SelectedValue
        End If
        If cercaStazionePickUp.SelectedValue <> "0" Then
            Session("stazUscita") = cercaStazionePickUp.SelectedValue
        End If
        If dropPrenotazioniPrepagate.SelectedValue <> "-1" Then
            Session("prepag") = dropPrenotazioniPrepagate.SelectedValue
        End If
        If cercaPrenotazioniRibaltamento.SelectedValue <> "0" Then
            Session("pren_rib") = cercaPrenotazioniRibaltamento.SelectedValue
        End If
        If dropCercaTipoCliente.SelectedValue <> "-1" Then
            Session("pren_tipo_cliente") = dropCercaTipoCliente.SelectedValue
        End If

        If txtCercaPresRDa.Text <> "" Then
            Session("pres_r_da") = txtCercaPresRDa.Text
        End If

        If txtCercaPresRA.Text <> "" Then
            Session("pres_r_a") = txtCercaPresRA.Text
        End If

        If dropCercaGruppoContratto.SelectedValue <> "0" Then
            Session("cnt_gruppo") = dropCercaGruppoContratto.SelectedValue
        End If

        If txtCercaTargaContratto.Text <> "" Then
            Session("cnt_targa") = txtCercaTargaContratto.Text
        End If


        '------------------------------------------------------------------------------------------------------------------------
    End Sub

    Protected Function preventivo_puo_diventare_prenotazione(ByVal id_prev As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM preventivi WITH(NOLOCK) WHERE id='" & id_prev & "'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT num_prenotazione FROM preventivi WITH(NOLOCK) WHERE id='" & id_prev & "'", Dbc)
            test = Cmd.ExecuteScalar & ""
            If test = "" Then
                Cmd = New Data.SqlClient.SqlCommand("SELECT num_contratto FROM preventivi WITH(NOLOCK) WHERE id='" & id_prev & "'", Dbc)
                test = Cmd.ExecuteScalar & ""

                If test = "" Then
                    preventivo_puo_diventare_prenotazione = True
                Else
                    'IL PREVENTIVO E' STATO TRASFORMATO IN CONTRATTO
                End If
            Else
                'IL PREVENTIVO E' GIA' DIVENTATO PRENOTAZIONE 
                preventivo_puo_diventare_prenotazione = False
            End If
        Else
            'PREVENTIVO NON PIU' ESISTENTE (L'UTENTE E' TORNATO INDIETRO COL TASTO DI NAVIGAZIONE DEL BROWSER)
            preventivo_puo_diventare_prenotazione = False
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function preventivo_puo_diventare_contratto(ByVal id_prev As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM preventivi WITH(NOLOCK) WHERE id='" & id_prev & "'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT num_contratto FROM preventivi WITH(NOLOCK) WHERE id='" & id_prev & "'", Dbc)
            test = Cmd.ExecuteScalar & ""

            If test = "" Then
                preventivo_puo_diventare_contratto = True
            Else
                'IL PREVENTIVO E' STATO TRASFORMATO IN CONTRATTO
            End If
        Else
            'PREVENTIVO NON PIU' ESISTENTE (L'UTENTE E' TORNATO INDIETRO COL TASTO DI NAVIGAZIONE DEL BROWSER)
            preventivo_puo_diventare_contratto = False
        End If


        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function


    Protected Sub EseguiNuovoPreventivo()
        'aggiunto 07.04.2022




        Dim sqla As String = "INSERT INTO preventivi (id_operatore_creazione,data_creazione) VALUES('" & Request.Cookies("SicilyRentCar")("IdUtente") & "',convert(datetime,getDate(),102))"


        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()
            sqla = "SELECT @@IDENTITY FROM preventivi WITH(NOLOCK)"
            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)

            idPreventivo.Text = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            'SETTO CORRETTAMENTE LA SEZIONE DI RICERCA COSTO PER UN NUOVO PREVENTIVO-----------------------------------------------------------
            setPadding("preventivo")

            lblAvvisoBlackList.Text = "0"

            gruppi_ultima_ricerca.Text = ""
            tariffa_ultima_ricerca.Text = ""

            lblNumPreventivo.Text = ""
            lblTipoDocumento.Text = ""
            lblOperatore.Text = ""

            lblData.Text = ""
            lblDataPreventivo.Text = ""

            numero_prenotazione.Text = ""
            idPrenotazione.Text = ""
            idContratto.Text = ""

            listPreventiviCosti.DataBind()

            btnModificaConducente.Visible = True
            btnAggiornaPrenotazione.Visible = False
            'btnPagamento.Visible = False
            dropTariffeGeneriche.Enabled = True
            dropTariffeParticolari.Enabled = True
            txtSconto.ReadOnly = False

            txt_sconto_new.ReadOnly = False     'aggiunto salvo 19.01.2023

            dropTipoSconto.Enabled = True
            btnCambiaTariffa.Visible = False

            lblMxSconto.Visible = False

            'listVecchioCalcolo.Visible = False             'da verificare 22.12.2022 salvo

            dropTipoCliente.SelectedValue = "0"

            Try
                dropStazionePickUp.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
            Catch ex As Exception
                dropStazionePickUp.SelectedValue = "0"
            End Try

            Try
                dropStazioneDropOff.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
            Catch ex As Exception
                dropStazioneDropOff.SelectedValue = "0"
            End Try

            gruppoVeloce.Enabled = True
            gruppoVeloce.SelectedValue = "0"
            txtoraPartenza.Text = ""
            txtOraRientro.Text = ""
            ore1.Text = ""
            minuti1.Text = ""
            ore2.Text = ""
            minuti2.Text = ""
            txtDaData.Text = Format(Now(), "dd/MM/yyyy")
            txtAData.Text = Format(Now().AddDays(1), "dd/MM/yyyy")
            txtNome.Text = ""
            txtCognome.Text = ""
            txtTelefono.Text = ""
            txtMail.Text = ""
            txtEtaPrimo.Text = ""
            txtEtaSecondo.Text = ""
            txtNumeroGiorni.Text = ""
            txtCodiceCliente.Text = ""
            lblNomeDitta.Text = ""

            tab_ricerca.Visible = False
            tab_cerca_tariffe.Visible = True

            lblMinGiorniNolo.Text = ""

            tab_preventivo.Visible = False
            tab_prenotazioni.Visible = False
            table_tariffe.Visible = False
            table_accessori_extra.Visible = False

            table_gruppi.Visible = False

            btnVediUltimoCalcolo.Visible = False
            btnInviaMailPreventivo.Visible = False

            btnSalvaPreventivo.Visible = True
            btnCerca.Visible = True
            btnAnnulla0.Visible = True
            dropStazionePickUp.Enabled = True
            dropStazioneDropOff.Enabled = True
            txtoraPartenza.Enabled = True
            txtOraRientro.Enabled = True
            minuti1.Enabled = True
            ore1.Enabled = True
            ore2.Enabled = True
            minuti2.Enabled = True
            dropTipoCliente.Enabled = True
            txtDaData.Enabled = True
            txtAData.Enabled = True
            txtEtaPrimo.Enabled = True
            txtEtaSecondo.Enabled = True
            txtNumeroGiorni.Enabled = True
            txtCodiceCliente.Enabled = True

            dropTariffeGeneriche.Enabled = True
            dropTariffeParticolari.Enabled = True

            btnAnnulla3.Visible = True
            btnAnnulla4.Visible = True
            btnAnnulla6.Visible = True
            btnAnnulla7.Visible = True
            btnAnnulla8.Visible = False
            btnAnnulla9.Visible = False

            lblGruppoNoTariffa.Text = ""

            'ABILITO LO SCONTO SOLAMENTE SE IL LIVELLO ACCESSO DEL RELATIVO PERMESSO E' 3
            If livello_accesso_sconto.Text = "3" Then
                txtSconto.Enabled = True
                dropTipoSconto.Enabled = True
            Else
                txtSconto.Enabled = False
                dropTipoSconto.Enabled = False
            End If

            numCalcolo.Text = "1"
            tipo_preventivo.Text = "nuovo"
            listWarningGruppi.DataBind()
        Catch ex As Exception
            HttpContext.Current.Response.Write("error btnNuovoPreventivo  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try




    End Sub



    Protected Sub btnNuovoPreventivo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovoPreventivo.Click

        EseguiNuovoPreventivo()

        '----------------------------------------------------------------------------------------------------------------------------------
    End Sub

    Protected Sub btnAnnulla1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla1.Click
        'RIABILITO I PULSANTI PER LA RICERCA DELLA TARIFFA ---------------------------------------------------------------------------------
        btnCerca.Visible = True
        dropStazionePickUp.Enabled = True
        dropStazioneDropOff.Enabled = True
        txtoraPartenza.Enabled = True
        txtOraRientro.Enabled = True
        minuti1.Enabled = True
        ore1.Enabled = True
        ore2.Enabled = True
        minuti2.Enabled = True
        dropTipoCliente.Enabled = True
        txtDaData.Enabled = True
        txtAData.Enabled = True
        txtEtaPrimo.Enabled = True
        txtEtaSecondo.Enabled = True
        txtNumeroGiorni.Enabled = True
        txtCodiceCliente.Enabled = True
        btnAnnulla0.Visible = True
        gruppoVeloce.Enabled = True
        '-----------------------------------------------------------------------------------------------------------------------------------

        'ELIMINO I DATI DI WARNING ---------------------------------------------------------------------------------------------------------
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM preventivi_warning WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        table_warning.Visible = False
        listWarningDropPreventivi.DataBind()
        listWarningPickPreventivi.DataBind()
        fonte_stop_sell.Visible = False
        '-----------------------------------------------------------------------------------------------------------------------------------
    End Sub

    Protected Sub btnAnnulla0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla0.Click

        '# salvo 01.08.2023 aggiunto - reset session in caso di cambio tariffa
        HttpContext.Current.Session("cambiatariffanp") = ""
        '@ end salvo 01.08.2023

        If tipo_preventivo.Text = "nuovo" Then
            setPadding("cerca")

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM preventivi_warning WHERE id_documento='" & idPreventivo.Text & "'", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi_costi WHERE id='" & idPreventivo.Text & "'", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi WHERE id='" & idPreventivo.Text & "'", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            tab_cerca_tariffe.Visible = False
            tab_ricerca.Visible = True
        End If
    End Sub

    Protected Sub annulla_torna_al_primo_step()
        'CONSERVO I GRUPPI E LA TARIFFA SCELTI DALL'ULTIMA RICERCA IN MODO DA POTERLI RIAPPLICARE VELOCEMENTE-------------------------------
        gruppi_ultima_ricerca.Text = ""

        For i = 0 To listGruppi.Items.Count - 1
            Dim sel_gruppo As CheckBox = listGruppi.Items(i).FindControl("sel_gruppo")
            If sel_gruppo.Checked Then
                Dim gruppo As Label = listGruppi.Items(i).FindControl("gruppo")
                gruppi_ultima_ricerca.Text = gruppi_ultima_ricerca.Text & gruppo.Text & "-"
            End If
        Next
        If gruppi_ultima_ricerca.Text <> "" Then
            gruppi_ultima_ricerca.Text = Left(gruppi_ultima_ricerca.Text, Len(gruppi_ultima_ricerca.Text) - 1)
        End If

        If dropTariffeGeneriche.SelectedValue <> "0" Then
            tariffa_ultima_ricerca.Text = dropTariffeGeneriche.SelectedValue
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            tariffa_ultima_ricerca.Text = dropTariffeParticolari.SelectedValue
        End If

        '-----------------------------------------------------------------------------------------------------------------------------------

        'RIABILITO I PULSANTI PER LA RICERCA DELLA TARIFFA ---------------------------------------------------------------------------------
        btnCerca.Visible = True
        dropStazionePickUp.Enabled = True
        dropStazioneDropOff.Enabled = True
        txtoraPartenza.Enabled = True
        txtOraRientro.Enabled = True
        minuti1.Enabled = True
        ore1.Enabled = True
        ore2.Enabled = True
        minuti2.Enabled = True
        dropTipoCliente.Enabled = True
        txtDaData.Enabled = True
        txtAData.Enabled = True
        txtEtaPrimo.Enabled = True
        txtEtaSecondo.Enabled = True
        txtNumeroGiorni.Enabled = True
        txtCodiceCliente.Enabled = True
        gruppoVeloce.Enabled = True
        btnAnnulla0.Visible = True
        gruppoVeloce.Enabled = True

        dropTariffeGeneriche.Enabled = True
        dropTariffeParticolari.Enabled = True
        txtSconto.ReadOnly = False
        dropTipoSconto.Enabled = True
        btnCambiaTariffa.Visible = False

        table_accessori_extra.Visible = False

        btnAnnulla1.Visible = True
        table_warning.Visible = True
        table_gruppi.Visible = False
        table_tariffe.Visible = False

        lblGruppoNoTariffa.Text = ""
        '-----------------------------------------------------------------------------------------------------------------------------------

        'ELIMINO I DATI DI WARNING E DEI COSTI ---------------------------------------------------------------------------------------------
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM preventivi_warning WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi_costi WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        table_warning.Visible = False
        listWarningDropPreventivi.DataBind()
        listWarningPickPreventivi.DataBind()
        listPreventiviCosti.DataBind()
        fonte_stop_sell.Visible = False
        '-----------------------------------------------------------------------------------------------------------------------------------
    End Sub

    Protected Sub btnAnnulla2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla2.Click


        lbl_new_tariffa.Text = ""       'salvo 01.12.2022

        annulla_torna_al_primo_step()



    End Sub

    Protected Sub btnAnnulla3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla3.Click
        annulla_torna_al_primo_step()
        tab_prenotazioni.Visible = False
        tab_preventivo.Visible = False

        ''DA QUI SI RITORNA DIRETTAMENTE ALLA RICERCA DEI PREVENTIVI - ELIMINO I DATI DEL PREVENTIVO APPENA CREATO

        'Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        'Dbc.Open()
        'Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM preventivi_warning WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        'Cmd.ExecuteNonQuery()
        'Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi_costi WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        'Cmd.ExecuteNonQuery()

        'If tipo_preventivo.Text = "nuovo" Then
        '    Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi WHERE id='" & idPreventivo.Text & "'", Dbc)
        '    Cmd.ExecuteNonQuery()
        'End If

        'Cmd.Dispose()
        'Cmd = Nothing
        'Dbc.Close()
        'Dbc.Dispose()
        'Dbc = Nothing

        'tab_cerca_tariffe.Visible = False
        'tab_ricerca.Visible = True
    End Sub

    Protected Sub btnSalvaPreventivo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvaPreventivo.Click
        Dim sqlStr As String = ""

        Try
            If tipo_preventivo.Text = "nuovo" Then
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                sqlStr = "SELECT ISNULL(MAX(num_preventivo),0) FROM preventivi WITH(NOLOCK)"
                Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                Dim num_preventivo As Integer = Cmd.ExecuteScalar + 1

                'TROVO L'ID-TARIFFA SCELTA --- NEL MENU A TENDINA IL SELECTED VALUE E' L'ID DI tariffe_righe
                Dim id_tariffe_righe As Integer
                Dim id_tariffa As Integer
                Dim tipo_tariffa As String = ""
                Dim TAR_VAL_DAL As String
                Dim TAR_VAL_AL As String
                Dim nome_tariffa As String
                Dim cod As String
                Dim codice_edp As String

                If dropTariffeGeneriche.SelectedValue <> "0" Then
                    id_tariffe_righe = dropTariffeGeneriche.SelectedValue
                    nome_tariffa = "'" & Replace(dropTariffeGeneriche.SelectedItem.Text, "'", "''") & "'"
                    tipo_tariffa = "generica"
                ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                    id_tariffe_righe = dropTariffeParticolari.SelectedValue
                    nome_tariffa = "'" & Replace(dropTariffeParticolari.SelectedItem.Text, "'", "''") & "'"
                    tipo_tariffa = "fonte"
                End If

                If txtCodiceCliente.Text <> "" Then
                    codice_edp = "'" & txtCodiceCliente.Text & "'"
                Else
                    codice_edp = "NULL"
                End If
                sqlStr = "SELECT id_tariffa FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                id_tariffa = Cmd.ExecuteScalar
                sqlStr = "SELECT vendibilita_da FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                TAR_VAL_DAL = Cmd.ExecuteScalar
                sqlStr = "SELECT vendibilita_A FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                TAR_VAL_AL = Cmd.ExecuteScalar

                TAR_VAL_DAL = funzioni_comuni.getDataDb_senza_orario(TAR_VAL_DAL)
                TAR_VAL_AL = funzioni_comuni.getDataDb_senza_orario(TAR_VAL_AL)

                'Cmd = New Data.SqlClient.SqlCommand("SELECT codice FROM tariffe WITH(NOLOCK) WHERE id='" & id_tariffa & "'", Dbc)
                'nome_tariffa = Cmd.ExecuteScalar
                sqlStr = "SELECT CODTAR FROM tariffe WITH(NOLOCK) WHERE id='" & id_tariffa & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                cod = Cmd.ExecuteScalar

                Dim data_uscita As String = getDataDb_senza_orario(txtDaData.Text, Request.ServerVariables("HTTP_HOST"))
                Dim data_rientro As String = getDataDb_senza_orario(txtAData.Text, Request.ServerVariables("HTTP_HOST"))

                Dim update_commissionabile As String = ""
                If dropFonteCommissionabile.Visible Then
                    update_commissionabile = ", id_fonte_commissionabile='" & dropFonteCommissionabile.SelectedValue & "', tipo_commissione='" & dropTipoCommissione.SelectedValue & "', commissione_percentuale='" & Replace(txtPercentualeCommissionabile.Text, ",", ".") & "', giorni_commissioni='" & txtNumeroGiorni.Text & "' "
                End If


                Dim Tipo_Sconto As String = "1" 'aggiunto salvo 14.04.2023 - sconto sempre e solo su valore_tariffa - ex : dropTipoSconto.SelectedValue

                sqlStr = "UPDATE preventivi SET num_preventivo='" & num_preventivo & "',num_calcolo='" & numCalcolo.Text & "',id_stazione_uscita='" & dropStazionePickUp.SelectedValue & "'," &
                "id_stazione_rientro='" & dropStazioneDropOff.SelectedValue & "',data_uscita=convert(datetime,'" & data_uscita & "',102),ore_uscita='" & ore1.Text & "'," &
                "minuti_uscita='" & minuti1.Text & "',data_rientro=convert(datetime,'" & data_rientro & "',102),ore_rientro='" & ore2.Text & "'," &
                "minuti_rientro='" & minuti2.Text & "',id_gruppo_auto='" & id_gruppo_auto_scelto.Text & "',id_utente=NULL," &
                "nome_conducente='" & Replace(txtNome.Text, "'", "''") & "',cognome_conducente='" & Replace(txtCognome.Text, "'", "''") & "'," &
                "telefono_conducente='" & Replace(txtTelefono.Text, "'", "''") & "'," &
                "eta_primo_guidatore='" & txtEtaPrimo.Text & "',eta_secondo_guidatore='" & txtEtaSecondo.Text & "'," &
                "mail_conducente='" & Replace(txtMail.Text, "'", "''") & "',id_fonte='" & dropTipoCliente.SelectedValue & "'," &
                "codice_edp=" & codice_edp & "," &
                "id_tariffa='" & id_tariffa & "',tipo_tariffa='" & tipo_tariffa & "',sconto='" & Replace(txt_sconto_new.Text, ",", ".") & "', tipo_sconto='" & Tipo_Sconto & "'," &
                "CODTAR=" & nome_tariffa & ", codice='" & Replace(cod, "'", "''") & "'," &
                "TAR_VAL_DAL=convert(datetime,'" & TAR_VAL_DAL & "',102), TAR_VAL_AL=convert(datetime,'" & TAR_VAL_AL & "',102) " & update_commissionabile &
                "WHERE id='" & idPreventivo.Text & "'"

                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteNonQuery()

                lblTipoDocumento.Text = "Preventivo Num.:"
                lblNumPreventivo.Text = num_preventivo

                btnSalvaPreventivo.Visible = False
                btnAnnulla3.Visible = False
                btnAnnulla4.Visible = False
                btnAnnulla8.Visible = True

                'salva gli importi del deposito cauzionale a seconda delle opzioni selezionate 19.01.2021
                Dim impDepDefault As String = funzioni_comuni.GetValoreDepositoCauzionaleDefault(id_gruppo_auto_scelto.Text)
                Dim impDepCalcolato As String = impDepDefault

                Dim iddoc As String = ""
                'se RD RF attive senza PPLUS 
                If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "100", "ck") = True _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "170", "ck") = True _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = False Then
                    impDepCalcolato = "300"
                    iddoc = idPreventivo.Text 'getNumPreventivo(num_preventivo)
                    funzioni_comuni.aggiorna_deposito_cauzionale(iddoc, "", "", "", "", "", "283", impDepCalcolato)
                End If
                'se PPLUS
                If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = True Then
                    impDepCalcolato = "200"
                    iddoc = idPreventivo.Text 'getNumPreventivo(num_preventivo)
                    funzioni_comuni.aggiorna_deposito_cauzionale(iddoc, "", "", "", "", "", "283", impDepCalcolato)
                End If

                If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "223", "ck") = True Then
                    impDepCalcolato = "200"
                    iddoc = idPreventivo.Text 'getNumPreventivo(num_preventivo)
                    funzioni_comuni.aggiorna_deposito_cauzionale(iddoc, "", "", "", "", "", "283", impDepCalcolato)
                End If

                'se tutte le condizioni non attive x deposito riporta valore a default 19.01.22
                If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "223", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "100", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "170", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "234", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = False Then
                    'recupera valore originario del deposito cauzionale
                    impDepCalcolato = impDepDefault
                    '19.01.22
                    funzioni_comuni.aggiorna_deposito_cauzionale(idPreventivo.Text, "", "", "", "", "", "283", impDepCalcolato)
                End If


                'Ultima verifica se opzione 'Riduzione Deposito 50%' importo = deposito / 2
                If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "234", "ck") = True Then
                    impDepCalcolato = FormatNumber((CDbl(impDepCalcolato) / 2), 2)
                    funzioni_comuni.aggiorna_deposito_cauzionale(idPreventivo.Text, "", "", "", "", "", "283", impDepCalcolato)
                    listPreventiviCosti.DataBind()
                End If

                'fine verifica condizioni deposito cauzionale





                Libreria.genUserMsgBox(Me, "Preventivo salvato correttamente. Numero Preventivo: " & num_preventivo)

                table_note.Visible = True
                gestione_note.InitForm(enum_note_tipo.note_preventivo, num_preventivo, False, False)

                tipo_preventivo.Text = "richiama"

                If txtMail.Text <> "" Then
                    Try
                        inviaMailPreventivo(txtMail.Text)
                    Catch ex As Exception
                        HttpContext.Current.Response.Write("error inviaMailPreventivo  : <br/>" & ex.Message & "<br/>")
                    End Try
                End If

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error btnSalvaPreventivo_Click  : <br/>" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try



    End Sub

    Protected Sub listPreventiviCosti_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles listPreventiviCosti.DataBinding
        ultimo_gruppo.Text = ""
        '04.01.2022
        'If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = True Then
        '    funzioni_comuni.SetOpzione(listPreventiviCosti, "223", False, False, False)
        'End If
    End Sub

    Protected Function getNomeDittaFromEdp(ByVal codEDP As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT Rag_Soc FROM ditte WITH(NOLOCK) WHERE [CODICE EDP]='" & codEDP & "'", Dbc)

        getNomeDittaFromEdp = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getNomeDittaFromId(ByVal id_cliente As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT Rag_Soc FROM ditte WITH(NOLOCK) WHERE id_ditta='" & id_cliente & "'", Dbc)

        getNomeDittaFromId = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getOperatoreFromId(ByVal id_operatore As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT cognome + ' ' + nome FROM operatori WITH(NOLOCK) WHERE id='" & id_operatore & "'", Dbc)

        getOperatoreFromId = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub richiama_preventivo(ByVal num_preventivo As String)

        'RICHIAMO IL PREVENTIVO RILANCIANDO LA RICERCA CON I PARAMETRI PRECEDENTEMENTE SALVATI
        'OGNI FASE CORRISPONDE AD UN PASSAGGIO CHE, SE NON COMPLETATO (AD ESEMPIO PERCHE' IL PREVENTIVO E' TROPPO VECCHIO E NON SONO PIU'
        'DISPONIBILI ELEMENTI COME GRUPPO AUTO SCELTO / STAZIONE SCELTA / TARIFFA SCELTA), NON PERMETTE LA CONTINUAZIONE DEL PROCESSO DI 
        'RICALCOLO DEL PREVENTIVO ORIGINARIO. IN QUESTO CASO VIENE MOSTRATO UN MESSAGGIO DI ERRORE.
        Dim err_n As Integer = 0

        numero_prenotazione.Text = ""
        Dim sqla As String = "SELECT * FROM preventivi WITH(NOLOCK) WHERE num_preventivo='" & num_preventivo & "'"
        Dim richiamo_a_buon_fine As Boolean = True
        Dim id_gruppo_auto As String
        Dim id_tariffe_righe As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            Rs.Read()

            If Rs.HasRows() Then
                If (Rs("num_prenotazione") & "") = "" Then
                    err_n = 1
                    gruppi_ultima_ricerca.Text = ""
                    tariffa_ultima_ricerca.Text = ""
                    idPrenotazione.Text = ""
                    idContratto.Text = ""
                    tipo_preventivo.Text = "richiama"
                    idPreventivo.Text = Rs("id")
                    numCalcolo.Text = CInt(Rs("num_calcolo")) + 1
                    lblTipoDocumento.Text = "Preventivo Num.:"
                    lblNumPreventivo.Text = Rs("num_preventivo")

                    id_gruppo_auto_scelto.Text = Rs("id_gruppo_auto")

                    id_gruppo_auto = Rs("id_gruppo_auto")
                    lblData.Text = "Data Preventivo: "
                    lblDataPreventivo.Text = Rs("data_creazione") & ""
                    err_n = 2
                    lblOperatore.Text = "Operatore Creazione:"
                    lblOperatoreCreazione.Text = getOperatoreFromId(Rs("id_operatore_creazione") & "")
                    err_n = 3
                    sqla = sqlPreventiviCosti.SelectCommand
                    listPreventiviCosti.DataBind()
                    ultimo_gruppo.Text = ""
                    err_n = 4
                    btnModificaConducente.Visible = True
                    btnAggiornaPrenotazione.Visible = False
                    'btnPagamento.Visible = False
                    btnCambiaTariffa.Visible = False
                    lblMxSconto.Visible = False


                    'listVecchioCalcolo.Visible = False  'da verificare salvo 22.12.2022

                    btnAnnulla6.Visible = False
                    btnAnnulla7.Visible = False
                    btnAnnulla9.Visible = True
                    btnAnnulla3.Visible = False
                    btnAnnulla4.Visible = False
                    btnAnnulla8.Visible = True

                    Dim errori As String = ""

                    'FASE 1 - DATE/STAZIONI/FONTI ------------------------------------------------------------------------------------------------
                    Try
                        If (Rs("id_fonte_commissionabile") & "") <> "" Then
                            dropFonteCommissionabile.SelectedValue = Rs("id_fonte_commissionabile")
                            dropTipoCommissione.SelectedValue = Rs("tipo_commissione")
                            txtPercentualeCommissionabile.Text = Rs("commissione_percentuale")
                        End If

                        dropStazionePickUp.SelectedValue = Rs("id_stazione_uscita")
                        dropStazioneDropOff.SelectedValue = Rs("id_stazione_rientro")
                        dropTipoCliente.SelectedValue = Rs("id_fonte")
                        If dropTipoCliente.SelectedValue <> "0" Then
                            If cliente_is_agenzia_di_viaggio(dropTipoCliente.SelectedValue) Then
                                lblFonteCommissionabile.Visible = True
                                dropFonteCommissionabile.Visible = True
                                dropFonteCommissionabile.Enabled = False
                                txtPercentualeCommissionabile.Visible = True
                                lblPercentualeCommissionabile.Visible = True
                                dropTipoCommissione.Visible = True
                                dropTipoCommissione.Enabled = False
                                btnAggiornaFontiCommissionabili.Visible = True
                                dropTariffeGeneriche.Enabled = False
                            Else
                                lblFonteCommissionabile.Visible = False
                                dropFonteCommissionabile.Visible = False
                                dropFonteCommissionabile.SelectedValue = "0"
                                dropTipoCommissione.Visible = False
                                dropTipoCommissione.SelectedValue = "0"
                                txtPercentualeCommissionabile.Visible = False
                                txtPercentualeCommissionabile.Text = ""
                                lblPercentualeCommissionabile.Visible = False
                                btnAggiornaFontiCommissionabili.Visible = False
                                dropTariffeGeneriche.Enabled = True
                            End If
                        Else
                            lblFonteCommissionabile.Visible = False
                            dropFonteCommissionabile.Visible = False
                            dropFonteCommissionabile.SelectedValue = "0"
                            dropTipoCommissione.Visible = False
                            dropTipoCommissione.SelectedValue = "0"
                            txtPercentualeCommissionabile.Visible = False
                            txtPercentualeCommissionabile.Text = ""
                            lblPercentualeCommissionabile.Visible = False
                            btnAggiornaFontiCommissionabili.Visible = False
                            dropTariffeGeneriche.Enabled = True
                        End If
                    Catch ex As Exception
                        'SE LA/LE STAZIONI O/E LA FONTE NON E' PIU' DISPONIBILE QUANDO SI RICHIAMA IL PREVENTIVO NON SARA' POSSIBILE RICHIAMARE
                        'IL PREVENTIVO RICHIESTO

                        errori = errori & "Stazione o tipologia di cliente non più esistente. "
                        richiamo_a_buon_fine = False
                    End Try
                    err_n = 5
                    If richiamo_a_buon_fine Then
                        txtDaData.Text = Day(Rs("data_uscita")) & "/" & Month(Rs("data_uscita")) & "/" & Year(Rs("data_uscita"))
                        txtAData.Text = Day(Rs("data_rientro")) & "/" & Month(Rs("data_rientro")) & "/" & Year(Rs("data_rientro"))

                        txtoraPartenza.Text = Rs("ore_uscita") & ":" & Rs("minuti_uscita")
                        txtOraRientro.Text = Rs("ore_rientro") & ":" & Rs("minuti_rientro")

                        ore1.Text = Rs("ore_uscita")
                        minuti1.Text = Rs("minuti_uscita")
                        ore2.Text = Rs("ore_rientro")
                        minuti2.Text = Rs("minuti_rientro")
                        txtEtaPrimo.Text = Rs("eta_primo_guidatore")
                        txtEtaSecondo.Text = Rs("eta_secondo_guidatore")
                        err_n = 6
                        If (Rs("codice_edp") & "") <> "" Then
                            txtCodiceCliente.Text = Rs("codice_edp")
                            lblNomeDitta.Text = getNomeDittaFromEdp(Rs("codice_edp"))

                            id_ditta.Text = getIdDitta(Rs("codice_edp"))
                            txtNomeDitta.Text = getNomeDittaFromEdp(Rs("codice_edp"))

                            btnModificaDitta.Visible = False
                        Else
                            txtCodiceCliente.Text = ""
                            lblNomeDitta.Text = ""
                        End If
                        err_n = 7
                        dropStazionePickUp.Enabled = False
                        dropStazioneDropOff.Enabled = False
                        txtDaData.Enabled = False
                        txtAData.Enabled = False
                        txtEtaPrimo.Enabled = False
                        txtEtaSecondo.Enabled = False
                        txtNumeroGiorni.Enabled = False
                        txtCodiceCliente.Enabled = False
                        txtoraPartenza.Enabled = False
                        txtOraRientro.Enabled = False
                        ore1.Enabled = False
                        minuti1.Enabled = False
                        ore2.Enabled = False
                        minuti2.Enabled = False
                        dropTipoCliente.Enabled = False
                        btnCerca.Visible = False
                        btnAnnulla0.Visible = False

                        btnVediUltimoCalcolo.Visible = False  'modificato 21.04.2023 salvo
                        btnInviaMailPreventivo.Visible = True
                        err_n = 8
                        cerca_fase1()

                        tab_ricerca.Visible = False
                        tab_cerca_tariffe.Visible = True
                        div_dettaglio_gruppi.Visible = True

                        tab_prenotazioni.Visible = False
                        '------------------------------------------------------------------------------------------------------------------------------
                        'FASE 2 - RICERCA DELLA RIGA DI TARIFFA CORRISPONDENTE ALLA TARIFFA SCELTA ----------------------------------------------------
                        Dim tipo_cliente_collegato_a_ditta As String = ""
                        err_n = 9
                        txtCodiceCliente.Text = Rs("codice_edp") & ""

                        If txtCodiceCliente.Text <> "" Then
                            tipo_cliente_collegato_a_ditta = getTipoCliente_SetNomeDitta(txtCodiceCliente.Text)
                        End If

                        table_tariffe.Visible = True
                        setQueryTariffePossibili(Rs("id_tariffa"))
                        err_n = 10
                        If Rs("tipo_tariffa") = "generica" Then
                            If dropTariffeGeneriche.Items.Count > 1 Then
                                dropTariffeGeneriche.Items(1).Selected = True
                                dropTariffeGeneriche.Enabled = False
                                dropTariffeParticolari.Enabled = False
                            Else
                                'SE LA TARIFFA AL GIORNO ODIERNO DI RICHIAMO NON E' VENDIBILE ALLORA NON POSSO PROSEGUIRE COL RICHIAMO DEL PREVENTIVO
                                richiamo_a_buon_fine = False
                                errori = errori & "Tariffa non più vendibile. "
                            End If
                        ElseIf Rs("tipo_tariffa") = "fonte" Then
                            'SE ERA STATO SPECIFICATO UN CODICE EDP E LA TARIFFA NON E' GENERICA CONTROLLO CHE L'AZIENDA IN QUESTIONE SIA ANCORA
                            'COLLEGATA ALLA CONVENZIONE
                            If txtCodiceCliente.Text <> "" Then
                                If Rs("id_fonte") <> tipo_cliente_collegato_a_ditta Then
                                    'IN QUESTO CASO LA FONTE PRECEDENTEMENTE SALVATA NON COMBACIA CON QUELLA ATTUALMENTE ASSOCIATA AL CLIENTE -
                                    'IL PREVENTIVO E' DA RIFARE.
                                    richiamo_a_buon_fine = False
                                    errori = errori & " Non e' più applicabile la precedente convenzione all'azienda."
                                End If
                            End If

                            If dropTariffeParticolari.Items.Count > 1 Then
                                dropTariffeParticolari.Items(1).Selected = True
                                dropTariffeParticolari.Enabled = False
                                dropTariffeGeneriche.Enabled = False
                            Else
                                'SE LA TARIFFA AL GIORNO ODIERNO DI RICHIAMO NON E' VENDIBILE ALLORA NON POSSO PROSEGUIRE COL RICHIAMO DEL PREVENTIVO
                                richiamo_a_buon_fine = False
                                errori = errori & "Tariffa non più vendibile. "
                            End If
                        End If

                        err_n = 11
                        txtSconto.Text = Rs("sconto")
                        txt_sconto_new.Text = Rs("sconto") 'aggiunto salvo 14.04.2023

                        If (Rs("tipo_sconto") & "") <> "" Then 'SOLO PER COMPATIBILITA' CON DATI PRECEDENTI - IL TIPO SCONTO DEVE ESSERE SALVATO
                            dropTipoSconto.SelectedValue = Rs("tipo_sconto")
                        End If
                        txtSconto.Enabled = False
                        dropTipoSconto.Enabled = False
                        '------------------------------------------------------------------------------------------------------------------------------
                        err_n = 12
                        If richiamo_a_buon_fine Then
                            'FASE 3 - SELEZIONE DEL GRUPPO -----------------------------------------------------------------------------------------
                            listGruppi.DataBind()
                            table_gruppi.Visible = True

                            richiamo_a_buon_fine = False

                            For i = 0 To listGruppi.Items.Count - 1
                                Dim id_gruppo_lista As Label = listGruppi.Items(i).FindControl("id_gruppo")
                                If id_gruppo_lista.Text = Rs("id_gruppo_auto") Then
                                    'CONTROLLO SE IL GRUPPO AUTO E' ANCORA VENDIBILE PER QUANTO RIGUARDA L'ETA' DEL GUIDATORE (NELL'EVENTO 
                                    'ITEM DATA BOUND DELLA TABELLA listGruppi IL CHECKBOX CORRISPONDENTE AL GRUPPO VIENE DISABILITATO SE LO STESSO
                                    'NON E' VENDIBILE)
                                    Dim check_gruppo As CheckBox = listGruppi.Items(i).FindControl("sel_gruppo")
                                    If check_gruppo.Enabled Then
                                        check_gruppo.Checked = True
                                        richiamo_a_buon_fine = True
                                    Else
                                        richiamo_a_buon_fine = False
                                        errori = errori & "Il gruppo auto scelto non è più vendibile. L'età di uno dei guidatori è troppo bassa o troppo alta."
                                    End If
                                End If
                            Next
                            '-----------------------------------------------------------------------------------------------------------------------
                            err_n = 13
                            If richiamo_a_buon_fine Then
                                'FASE 4 - E' POSSIBILE ESEGUIRE IL CALCOLO DELLA TARIFFA - TUTTO E' STATO CARICATO CORRETTAMENTE -------------------
                                If dropTariffeGeneriche.SelectedValue <> "0" Then
                                    id_tariffe_righe = dropTariffeGeneriche.SelectedValue
                                ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                                    id_tariffe_righe = dropTariffeParticolari.SelectedValue
                                End If

                                Dim min_giorni_nolo As String = funzioni_comuni.getMinGiorniNolo(id_tariffe_righe)
                                If min_giorni_nolo <> "-1" Then
                                    lblMinGiorniNolo.Text = "La tariffa prevede un minimo di " & min_giorni_nolo & " giorni/o di noleggio"
                                Else
                                    lblMinGiorniNolo.Text = ""
                                End If

                                tariffa_broker.Text = funzioni_comuni.is_tariffa_broker(id_tariffe_righe)

                                table_accessori_extra.Visible = True
                                sqlElementiExtra.SelectCommand = funzioni.getQueryElementiExtra(id_tariffe_righe, False)
                                dropElementiExtra.Items.Clear()
                                dropElementiExtra.Items.Add("Seleziona...")
                                dropElementiExtra.Items(0).Value = "0"
                                dropElementiExtra.DataBind()
                                err_n = 14

                                '# salvo 27.01.2023 se preventivo richiamato deve sempre creare tariffa con 
                                'nuovo calcolo indipendentemente da qual'è la data di creazione - da Francesco
                                Dim data_creazione As String = Date.Now.ToShortDateString & " " & Date.Now.ToLongTimeString



                                calcolaTariffe(id_tariffe_righe, data_creazione) 'aggiornato con data creazione salvo 19.01.2023

                                'ha creato nuove righe su preventivi_costi e incrementato numCalcolo 21.04.2023

                                listPreventiviCosti.DataBind()
                                err_n = 15
                                '-------------------------------------------------------------------------------------------------------------------
                                'FASE 5 - COMPILAZIONE DATI ANAGRAFICI DEL CLIENTE (DA PREVENTIVO) -------------------------------------------------
                                tab_preventivo.Visible = True
                                txtNome.Text = Rs("nome_conducente")
                                txtCognome.Text = Rs("cognome_conducente")
                                txtTelefono.Text = Rs("telefono_conducente")
                                txtMail.Text = Rs("mail_conducente")

                                btnSalvaPreventivo.Visible = False
                                '-------------------------------------------------------------------------------------------------------------------
                                'FASE 6 - AGGIUNTA DEGLI ACCESSORI PRECEDENTEMENTE SELEZIONATI -----------------------------------------------------
                                Dbc.Close()
                                Dbc.Open()
                                err_n = 16
                                'Dim id_gruppo_auto As Label
                                For i = 0 To listPreventiviCosti.Items.Count - 1
                                    'SE L'ELEMENTO E' UNO DEGLI ELEMENTI A SCELTA CONTROLLO SE PER IL NUMERO DI CALCOLO PRECENDENTE ERA STATO SELEZIONATO
                                    'SE SI LO SELEZIONO ED AGGIUNGO IL COSTO AL TOTALE
                                    Dim chkScegli As CheckBox = listPreventiviCosti.Items(i).FindControl("chkScegli")
                                    'id_gruppo_auto = listPreventiviCosti.Items(i).FindControl("id_gruppoLabel")

                                    If listPreventiviCosti.Items(i).FindControl("chkScegli").Visible Or (Not listPreventiviCosti.Items(i).FindControl("chkScegli").Visible And listPreventiviCosti.Items(i).FindControl("chkOmaggio").Visible) Then
                                        Dim id_elemento As Label = listPreventiviCosti.Items(i).FindControl("id_elemento")

                                        Cmd = New Data.SqlClient.SqlCommand("SELECT selezionato FROM preventivi_costi WITH(NOLOCK) WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "' AND id_elemento='" & id_elemento.Text & "'", Dbc)

                                        Dim precedentemente_selezionato As String = Cmd.ExecuteScalar

                                        If precedentemente_selezionato = "True" Then
                                            'CONTROLLO SE ERA STATO OMAGGIATO E SE L'ACCESSORIO E' ANCORA OMAGGIABILE
                                            Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(omaggiato,'False') As omaggiato FROM preventivi_costi WITH(NOLOCK) WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "' AND id_elemento='" & id_elemento.Text & "'", Dbc)
                                            Dim precedentemente_omaggiato As Boolean = Cmd.ExecuteScalar
                                            Cmd = New Data.SqlClient.SqlCommand("SELECT omaggiabile FROM preventivi_costi WITH(NOLOCK) WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) & "' AND id_elemento='" & id_elemento.Text & "'", Dbc)
                                            Dim omaggiabile As Boolean = Cmd.ExecuteScalar
                                            Dim is_gps As Label = listPreventiviCosti.Items(i).FindControl("is_gps")

                                            If precedentemente_omaggiato And omaggiabile Then
                                                'SE L'ACCESSORIO ERA STATO OMAGGIATO ED E' ANCORA OMAGGIABILE VIENE NUOVAMENTE OMAGGIATO (SIA 
                                                'PER ACCESSORI A SCELTA CHE PER ACCESSORI OBBLIGATORI)
                                                funzioni.omaggio_accessorio(True, False, False, idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppo_auto, id_elemento.Text, "", dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, "", "")
                                            Else
                                                If (listPreventiviCosti.Items(i).FindControl("chkScegli").Visible) Then
                                                    'AGGIUNGO IL COSTO SOLAMENTE SE SI TRATTA DI UN ACCESSORIO A SCELTA (SE E' UN ELEMENTO
                                                    'OBBLIGATORIO IL COSTO E' GIA' STATO CALCOLATO QUANDO E' STATA ANALIZZATA LA CONDIZIONE
                                                    aggiungi_costo_accessorio(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppo_auto, id_elemento.Text, "", "", "", False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                                                End If
                                            End If
                                            'SE SI TRATTA DI SECONDO GUIDATORE AGGIUNGO, SE NECESSARIO, IL COSTO DELLO YOUNG DRIVER
                                            If id_elemento.Text = Costanti.Id_Secondo_Guidatore Then
                                                nuovo_accessorio(get_id_young_driver(), id_gruppo_auto, "YOUNG", txtEtaPrimo.Text, txtEtaSecondo.Text)
                                            End If
                                            'SE SI TRATTA DI GPS AGGIUNGO L'EVENTUALE VAL 
                                            If is_gps.Text = "True" And dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
                                                nuovo_accessorio("", id_gruppo_auto, "VAL_GPS", txtEtaPrimo.Text, txtEtaSecondo.Text)
                                            End If
                                        End If
                                    End If
                                Next
                                err_n = 17
                                'E' NECESSARIO ADESSO AGGIUNGERE GLI ACCESSORI EXTRA PRECEDENTEMENTE SELEZIONATI - OMAGGIO SE LO ERA PRECEDENTEMENTE
                                'SE L'ACCESSORIO E' ANCORA OMAGGIABILE
                                Cmd = New Data.SqlClient.SqlCommand("SELECT id_elemento, ISNULL(omaggiato,'False') As omaggiato, condizioni_elementi.omaggiabile FROM preventivi_costi WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON preventivi_costi.id_elemento=condizioni_elementi.id WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "' AND obbligatorio='0' AND selezionato='1' AND valorizza='0'", Dbc)
                                Rs = Cmd.ExecuteReader()

                                Do While Rs.Read()
                                    nuovo_accessorio(Rs("id_elemento"), id_gruppo_auto, "ELEMENTO", txtEtaPrimo.Text, txtEtaSecondo.Text)
                                    Dim precedentemente_omaggiato As Boolean = Rs("omaggiato")
                                    Dim omaggiabile As Boolean = Rs("omaggiabile")
                                    If precedentemente_omaggiato Then
                                        If omaggiabile Then
                                            funzioni.omaggio_accessorio(True, False, False, idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppo_auto, Rs("id_elemento"), "", dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, "", "")
                                        End If
                                    End If
                                Loop
                                err_n = 18
                                ultimo_gruppo.Text = ""
                                listPreventiviCosti.DataBind()
                                '-------------------------------------------------------------------------------------------------------------------
                                'FASE 7 - AGGIUNTA DEGLI EVENTUALI WARNING PER IL GRUPPO AUTO SELEZIONATO-------------------------------------------
                                aggiungi_warning_gruppo_auto(id_gruppo_auto, idPreventivo.Text, "", "", numCalcolo.Text)
                                table_gruppi.Visible = False
                                listWarningGruppi.DataBind()
                                '-------------------------------------------------------------------------------------------------------------------
                                err_n = 19

                            End If
                        End If
                    End If

                    Dbc.Close()
                    Dbc.Open()
                    err_n = 20
                    If richiamo_a_buon_fine Then
                        setPadding("preventivo_salva")
                        err_n = 21
                        'IN QUESTO CASO AGGIORNO LA RIGA DI PREVENTIVO SALVANDO IL NUOVO NUMERO DI CALCOLO - ELIMINO LE RIGHE DI CALCOLO E DI 
                        'WARNING NON PIU' NECESSARIO (QUELLE PRECEDENTI ALLA PENULTIMA TRANNE LA PRIMA) - AGGIORNO LE INFORMAZIONI DI UTENTE E DATA
                        'DI ULTIMA E PENULTIMA MODIFICA - AGGIORNO INOLTRE LA DATA DI VALIDITA' DELLA TARIFFA (QUALORA FOSSE CAMBIATA)

                        Dim TAR_VAL_DAL As String
                        Dim TAR_VAL_AL As String
                        Cmd = New Data.SqlClient.SqlCommand("SELECT vendibilita_da FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)
                        TAR_VAL_DAL = Cmd.ExecuteScalar

                        Cmd = New Data.SqlClient.SqlCommand("SELECT vendibilita_A FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)
                        TAR_VAL_AL = Cmd.ExecuteScalar

                        TAR_VAL_DAL = funzioni_comuni.getDataDb_senza_orario(TAR_VAL_DAL)

                        TAR_VAL_AL = funzioni_comuni.getDataDb_senza_orario(TAR_VAL_AL)
                        sqla = "UPDATE preventivi SET num_calcolo='" & numCalcolo.Text & "', id_operatore_penultima_modifica=id_operatore_ultima_modifica, data_penultima_modifica=data_ultima_modifica, id_operatore_ultima_modifica='" & Request.Cookies("SicilyRentCar")("idUtente") & "', data_ultima_modifica=convert(datetime,GetDate(),102), TAR_VAL_DAL=convert(datetime,'" & TAR_VAL_DAL & "',102), TAR_VAL_AL=convert(datetime,'" & TAR_VAL_AL & "',102) WHERE id='" & idPreventivo.Text & "'"
                        Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                        Cmd.ExecuteNonQuery()


                        'il nuovo numerodicalcolo è stato aggiornato su preventivi salvo 21.04.2023

                        '# salvo 24.01.2023 - verificare se deve eliminare calcolo precedente
                        'modificato da >3 a >6 e elimina da CInt(numCalcolo.Text) - 3
                        'modificato in >12 23.04.2023 salvo
                        If CInt(numCalcolo.Text) > 12 Then
                            'salvo 24.01.2023 - invece di eliminare il precedente calcolo ( CInt(numCalcolo.Text) - 1) elimina dal  CInt(numCalcolo.Text) - 2
                            Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi_warning WHERE num_calcolo<'" & CInt(numCalcolo.Text) - 3 & "' AND num_calcolo<>'1' AND id_documento='" & idPreventivo.Text & "'", Dbc)
                            Cmd.ExecuteNonQuery()
                            Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi_costi WHERE num_calcolo<'" & CInt(numCalcolo.Text) - 3 & "' AND num_calcolo<>'1' AND id_documento='" & idPreventivo.Text & "'", Dbc)
                            Cmd.ExecuteNonQuery()
                        End If

                        table_note.Visible = True
                        gestione_note.InitForm(enum_note_tipo.note_preventivo, lblNumPreventivo.Text, False, False)

                    Else

                        err_n = 22
                        'IN QUESTO CASO ELIMINO LE RIGHE DI CALCOLO E MOSTRO UN MESSAGGIO DI ERRORE
                        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi_warning WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
                        Cmd.ExecuteNonQuery()
                        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi_costi WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
                        Cmd.ExecuteNonQuery()
                        tab_cerca_tariffe.Visible = False
                        div_dettaglio_gruppi.Visible = False
                        tab_ricerca.Visible = True
                        Libreria.genUserMsgBox(Me, "Impossibile recuperare il preventivo. " & errori)
                    End If


                    'se PPlus disabilito ELIRES 04.01.2022
                    If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = True Then
                        funzioni_comuni.nascondi_franchigie(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppo_auto, "203", "204")
                        listPreventiviCosti.DataBind()
                        funzioni_comuni.SetOpzione(listPreventiviCosti, "223", False, False, False)
                    End If


                    'verifica condizioni deposito cauzionale 19.01.2022
                    Dim impDepDefault As String = GetValoreDepositoCauzionaleDefault(id_gruppo_auto)
                    Dim impDepCalcolato As String = impDepDefault

                    If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = True Then
                        impDepCalcolato = "200"
                        funzioni_comuni.aggiorna_deposito_cauzionale(idPreventivo.Text, "", "", "", "", "", "283", impDepCalcolato)
                        listPreventiviCosti.DataBind()
                    End If

                    If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "223", "ck") = True Then
                        impDepCalcolato = "200"
                        funzioni_comuni.aggiorna_deposito_cauzionale(idPreventivo.Text, "", "", "", "", "", "283", impDepCalcolato)
                        listPreventiviCosti.DataBind()
                    End If


                    'se RIDUZIONI ATTIVE e PPLUS disabilitate riduce valore deposito cauzionale 19.01.22
                    ' se attive ma senza PPLUS
                    If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "100", "ck") = True _
                        And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "170", "ck") = True _
                        And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = False Then
                        impDepCalcolato = "300"
                        funzioni_comuni.aggiorna_deposito_cauzionale(idPreventivo.Text, "", "", "", "", "", "283", impDepCalcolato)
                        listPreventiviCosti.DataBind()
                    End If


                    'se tutte le condizioni non attive x deposito riporta valore a default 19.01.22
                    If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "223", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "100", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "170", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "234", "ck") = False _
                    And funzioni_comuni.VerificaOpzione(listPreventiviCosti, "248", "ck") = False Then
                        'recupera valore originario del deposito cauzionale
                        impDepCalcolato = impDepDefault
                        '19.01.22
                        funzioni_comuni.aggiorna_deposito_cauzionale(idPreventivo.Text, "", "", "", "", "", "283", impDepCalcolato)
                    End If

                    'Ultima verifica se opzione 'Riduzione Deposito 50%' importo = deposito / 2
                    If funzioni_comuni.VerificaOpzione(listPreventiviCosti, "234", "ck") = True Then
                        impDepCalcolato = FormatNumber((CDbl(impDepCalcolato) / 2), 2)
                        funzioni_comuni.aggiorna_deposito_cauzionale(idPreventivo.Text, "", "", "", "", "", "283", impDepCalcolato)
                        listPreventiviCosti.DataBind()
                    End If


                    '#aggiunto Salvo 20.04.2023 
                    'verifico se valore del TOTALE dell'Ultimo numero di calcolo appena registrato è uguale a quello precedente
                    'se uguale elimino l'ultimo num_calcolo appena creato in modo da non duplicare righe uguali

                    'verifica il valore dell'importo del nome_costo 'TOTALE' dell'ultimo calcolo
                    Dim lastNumCalcolo As String = (CInt(numCalcolo.Text) - 1).ToString
                    Dim valore_totale_questo_num_calcolo As String = "0"

                    Dim valore_totale_ultimo_prev As String = funzioni_comuni_new.GetTotaleUltimoPreventivoValido(num_preventivo, lastNumCalcolo)

                    Dim FlagValoriUguali As Boolean = False

                    For i = 0 To listPreventiviCosti.Items.Count - 1
                        Dim lblvalore_totale_questo_num_calcolo As Label = listPreventiviCosti.Items(i).FindControl("costo_scontato")
                        Dim nome_costo_totale_questo_num_calcolo As Label = listPreventiviCosti.Items(i).FindControl("nome_costo")
                        If nome_costo_totale_questo_num_calcolo.Text = "TOTALE" Then
                            valore_totale_questo_num_calcolo = lblvalore_totale_questo_num_calcolo.Text
                            If CDbl(lblvalore_totale_questo_num_calcolo.Text) = CDbl(valore_totale_ultimo_prev) Then
                                FlagValoriUguali = True
                                Exit For
                            End If
                        End If

                    Next

                    'se i valori dei TOTALE dei num di calcolo sono uguali


                    If FlagValoriUguali = True Then

                        ''elimina l'ultimo/corrente numcalcolo
                        Dim xdel As Integer = funzioni_comuni_new.DeletePreventiviCostiNumCalcolo(idPreventivo.Text, numCalcolo.Text)

                        Dim NumCalcoloPrec As String = (CInt(numCalcolo.Text) - 1).ToString

                        'riporta il numcalcolo precedente alla riga del preventivo
                        Dim xpdt As Integer = funzioni_comuni_new.UpdatePreventiviCostiNumCalcolo(idPreventivo.Text, numCalcolo.Text, NumCalcoloPrec)
                        Session("prev_valori_uguali") = "OK"

                        ''e imposta come corrente il precedente
                        numCalcolo.Text = NumCalcoloPrec

                        'e ricarica le due list corrente e penultima
                        Dim sqlprev As String = "SELECT preventivi_costi.num_calcolo, preventivi_costi.id, preventivi_Costi.id_documento, gruppi.id_gruppo, "
                        sqlprev += "gruppi.cod_gruppo As gruppo,preventivi_costi.id_elemento, preventivi_costi.nome_costo, condizioni_elementi.descrizione_lunga, "
                        sqlprev += "condizioni_elementi.tipologia_franchigia, condizioni_elementi.sottotipologia_franchigia, condizioni_elementi.is_gps, "
                        sqlprev += "ISNULL((imponibile+iva_imponibile+ISNULL(imponibile_onere,0)+ISNULL(iva_onere,0)),0) As valore_costo, "
                        sqlprev += "ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, "
                        sqlprev += "ISNULL(commissioni_imponibile_originale,0)+ISNULL(commissioni_iva_originale,0) As commissioni, id_a_carico_di,"
                        sqlprev += "preventivi_costi.obbligatorio, id_metodo_stampa, ISNULL(selezionato,'False') As selezionato, "
                        sqlprev += "ISNULL(preventivi_costi.omaggiato,'False') As omaggiato, ISNULL(preventivi_costi.omaggiabile,'False') As omaggiabile, "
                        sqlprev += "preventivi_costi.num_elemento, preventivi_costi.imponibile_scontato, preventivi_costi.iva_imponibile_scontato  "
                        sqlprev += "FROM preventivi_costi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON preventivi_costi.id_gruppo=gruppi.id_gruppo LEFT JOIN "
                        sqlprev += "condizioni_elementi WITH(NOLOCK) ON preventivi_costi.id_elemento=condizioni_elementi.id "

                        Dim sWHE = "WHERE ((id_documento = '" & idPreventivo.Text & "') And (num_calcolo = '" & NumCalcoloPrec & "')) And ordine_stampa<>'5' "

                        Dim sqlprevWHE = "And ISNULL(franchigia_attiva,'1')='1' AND (NOT (preventivi_costi.ordine_stampa='7' AND preventivi_costi.franchigia_attiva IS NULL) "
                        sqlprevWHE += "Or condizioni_elementi.tipologia='KM_EXTRA') AND ISNULL(condizioni_elementi.tipologia,'')<>'RIMUOVI_RIFORNIMENTO' AND "
                        sqlprevWHE += "Not (Not valore_percentuale Is NULL And ordine_stampa<>'2') "
                        sqlprevWHE += "ORDER BY id_gruppo,ordine_stampa,ISNULL(condizioni_elementi.ordinamento_vis,0) DESC, nome_costo "

                        sqlPreventiviCosti.SelectCommand = sqlprev & sWHE & sqlprevWHE
                        listPreventiviCosti.DataBind()

                        NumCalcoloPrec = (CInt(numCalcolo.Text) - 1).ToString
                        sWHE = "WHERE ((id_documento = '" & idPreventivo.Text & "') And (num_calcolo = '" & NumCalcoloPrec & "')) And ordine_stampa<>'5' "
                        sqlUltimoPreventiviCosti.SelectCommand = sqlprev & sWHE & sqlprevWHE
                        listVecchioCalcolo.DataBind()

                        listVecchioCalcolo.Visible = False

                    Else ' il valore dei preventivi corrente e ultimo sono diversi

                        'visualizza ultimo calcolo e msg
                        Session("prev_valori_uguali") = "KO"

                        'visualizza Msg
                        Dim ris As String = ""
                        ris = ris

                        If CDbl(valore_totale_questo_num_calcolo) > CDbl(valore_totale_ultimo_prev) Then
                            ris = "Attenzione la nuova tariffa prenotabile è superiore a quella inviata precedentemente tramite preventivo."
                        ElseIf CDbl(valore_totale_questo_num_calcolo) < CDbl(valore_totale_ultimo_prev) Then
                            ris = "Attenzione la nuova tariffa prenotabile è inferiore a quella inviata precedentemente tramite preventivo, in alternativa puoi elaborare una nuova quotazione."
                        End If

                        If ris <> "" Then

                            lbl_msg_tariffe_diverse.Text = ris
                            lbl_msg_tariffe_diverse.Visible = True
                            lbl_msg_tariffe_diverse.BackColor = Drawing.Color.White
                            lbl_msg_tariffe_diverse.ForeColor = Drawing.Color.Red


                            listVecchioCalcolo.Visible = True

                            Dim sqlprev As String = "SELECT preventivi_costi.num_calcolo, preventivi_costi.id, preventivi_Costi.id_documento, gruppi.id_gruppo, "
                            sqlprev += "gruppi.cod_gruppo As gruppo,preventivi_costi.id_elemento, preventivi_costi.nome_costo, condizioni_elementi.descrizione_lunga, "
                            sqlprev += "condizioni_elementi.tipologia_franchigia, condizioni_elementi.sottotipologia_franchigia, condizioni_elementi.is_gps, "
                            sqlprev += "ISNULL((imponibile+iva_imponibile+ISNULL(imponibile_onere,0)+ISNULL(iva_onere,0)),0) As valore_costo, "
                            sqlprev += "ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, "
                            sqlprev += "ISNULL(commissioni_imponibile_originale,0)+ISNULL(commissioni_iva_originale,0) As commissioni, id_a_carico_di,"
                            sqlprev += "preventivi_costi.obbligatorio, id_metodo_stampa, ISNULL(selezionato,'False') As selezionato, "
                            sqlprev += "ISNULL(preventivi_costi.omaggiato,'False') As omaggiato, ISNULL(preventivi_costi.omaggiabile,'False') As omaggiabile, "
                            sqlprev += "preventivi_costi.num_elemento, preventivi_costi.imponibile_scontato, preventivi_costi.iva_imponibile_scontato  "
                            sqlprev += "FROM preventivi_costi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON preventivi_costi.id_gruppo=gruppi.id_gruppo LEFT JOIN "
                            sqlprev += "condizioni_elementi WITH(NOLOCK) ON preventivi_costi.id_elemento=condizioni_elementi.id "

                            Dim sWHE = "WHERE ((id_documento = '" & idPreventivo.Text & "') And (num_calcolo = '" & (CInt(numCalcolo.Text) - 1) & "')) And ordine_stampa<>'5' "

                            Dim sqlprevWHE = "And ISNULL(franchigia_attiva,'1')='1' AND (NOT (preventivi_costi.ordine_stampa='7' AND preventivi_costi.franchigia_attiva IS NULL) "
                            sqlprevWHE += "Or condizioni_elementi.tipologia='KM_EXTRA') AND ISNULL(condizioni_elementi.tipologia,'')<>'RIMUOVI_RIFORNIMENTO' AND "
                            sqlprevWHE += "Not (Not valore_percentuale Is NULL And ordine_stampa<>'2') "
                            sqlprevWHE += "ORDER BY id_gruppo,ordine_stampa,ISNULL(condizioni_elementi.ordinamento_vis,0) DESC, nome_costo "


                            old_ultimo_gruppo.Text = ""

                            sqlUltimoPreventiviCosti.SelectCommand = sqlprev & sWHE & sqlprevWHE
                            'sqlUltimoPreventiviCosti.DataBind()

                            listVecchioCalcolo.DataBind()

                            'visualizza le righe
                            listVecchioCalcolo.Visible = True
                            'listVecchioCalcolo.FindControl("riga_gruppo").Visible = True
                            'listVecchioCalcolo.FindControl("riga_intestazione").Visible = True

                        Else
                            lbl_msg_tariffe_diverse.Text = ris
                            lbl_msg_tariffe_diverse.Visible = False
                        End If

                    End If

                    '@end salvo


                    'test salvo 21.04.2023
                    'num_calcolo_test.Text = numCalcolo.Text
                    'If Request.Cookies("SicilyRentCar")("idutente") = "5" Then 'Salvo
                    '    num_calcolo_test.Visible = True

                    'Else
                    '    num_calcolo_test.Visible = False
                    'End If

                    'aggiunto salvo 18.07.2023 
                    HttpContext.Current.Session("num_calcolo_preventivo") = numCalcolo.Text

                Else
                    Libreria.genUserMsgBox(Me, "Impossibile richiamare il preventivo in quanto è collegato alla prenotazione N. " & Rs("num_prenotazione"))
                End If
            Else
                Libreria.genUserMsgBox(Me, "Preventivo non trovato.")
            End If
            err_n = 23
            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error richiama_preventivo  : <br/>errN:" & err_n & "<br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try





    End Sub

    Protected Sub richiama_contratto(ByVal id_contratto As String)

        Session("carica_contratto") = id_contratto

        setta_session_x_contratto()
        'Response.Redirect("contratti_new.aspx")
        Response.Redirect("contratti.aspx")


    End Sub

    Protected Sub richiama_prenotazione(ByVal num_prenotazione As String)
        Session("num_prenotazione_from_preventivi") = num_prenotazione

        'TRASPORTO GLI EVENTUALI DATI PER LA RICERCA IN MODO DA TORNARE IN QUESTA PAGINA CON LO STESSO STATO---------------------
        Session("tipo_documento") = dropTipoDocumento.SelectedValue

        If txtCercaNumStaz.Text <> "" Then
            Session("num_pre_staz") = txtCercaNumStaz.Text
        End If

        If txtCercaNumInterno.Text <> "" Then
            Session("num_pre_num") = txtCercaNumInterno.Text
        End If

        If txtCercaRiferimento.Text <> "" Then
            Session("riferimento") = txtRiferimentoTO.Text
        End If
        If txtCercaNome.Text <> "" Then
            Session("nome") = txtCercaNome.Text
        End If
        If txtCercaCognome.Text <> "" Then
            Session("cognome") = txtCercaCognome.Text
        End If
        If txtCercaPickUpDa.Text <> "" Then
            Session("pickUpDa") = txtCercaPickUpDa.Text
        End If
        If txtCercaPickUpA.Text <> "" Then
            Session("pickUpA") = txtCercaPickUpA.Text
        End If
        If txtCercaDropOffDa.Text <> "" Then
            Session("dropOffDa") = txtCercaDropOffDa.Text
        End If
        If txtCercaDropOffA.Text <> "" Then
            Session("dropOffA") = txtCercaDropOffA.Text
        End If
        If cercaStazioneDropOff.SelectedValue <> "0" Then
            Session("stazRientro") = cercaStazioneDropOff.SelectedValue
        End If
        If cercaStazionePickUp.SelectedValue <> "0" Then
            Session("stazUscita") = cercaStazionePickUp.SelectedValue
        End If
        If dropPrenotazioniPrepagate.SelectedValue <> "-1" Then
            Session("prepag") = dropPrenotazioniPrepagate.SelectedValue
        End If
        If cercaPrenotazioniRibaltamento.SelectedValue <> "0" Then
            Session("pren_rib") = cercaPrenotazioniRibaltamento.SelectedValue
        End If
        If dropCercaTipoCliente.SelectedValue <> "-1" Then
            Session("pren_tipo_cliente") = dropCercaTipoCliente.SelectedValue
        End If
        Session("statoDoc") = dropStatoPrenotazione.SelectedValue
        '------------------------------------------------------------------------------------------------------------------------

        Response.Redirect("prenotazioni.aspx")
    End Sub

    'Protected Sub btnRichiamaPreventivo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRichiamaPreventivo.Click
    '    Dim numero_ok As Boolean = False
    '    Dim test As Integer

    '    Try
    '        test = CInt(txtNumPreventivo.Text)
    '        numero_ok = True
    '    Catch ex As Exception
    '        numero_ok = False
    '    End Try

    '    If numero_ok Then
    '        If dropTipoDocumento.SelectedValue = "2" Then
    '            richiama_preventivo(txtNumPreventivo.Text)
    '        ElseIf dropTipoDocumento.SelectedValue = "1" Then
    '            richiama_prenotazione(txtNumPreventivo.Text)
    '        End If
    '    Else
    '        Libreria.genUserMsgBox(Me, "Specificare il numero del documento da richiamare.")
    '    End If
    'End Sub

    Protected Function getNumPrenotazione(ByVal idPren As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT numpren FROM prenotazioni WITH(NOLOCK) WHERE Nr_Pren='" & idPren & "'", Dbc)

        getNumPrenotazione = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function
    Protected Function getNumPreventivo(ByVal idPrev As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT [id] FROM preventivi WITH(NOLOCK) WHERE num_preventivo ='" & idPrev & "'", Dbc)

        getNumPreventivo = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

    End Function
    Protected Function getNumContratto(ByVal idPrev As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT num_contratto FROM prenotazioni WITH(NOLOCK) WHERE id='" & idPrev & "'", Dbc)

        getNumContratto = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub btnAnnulla4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla4.Click
        'DA QUI SI RITORNA DIRETTAMENTE ALLA RICERCA DEI PREVENTIVI - ELIMINO I DATI DEL PREVENTIVO APPENA CREATO

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM preventivi_warning WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()
        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi_costi WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        If tipo_preventivo.Text = "nuovo" Then
            Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi WHERE id='" & idPreventivo.Text & "'", Dbc)
            Cmd.ExecuteNonQuery()
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        tab_cerca_tariffe.Visible = False
        tab_ricerca.Visible = True
        'tab_cerca_tariffe.Visible = False
        tab_prenotazioni.Visible = False
        tab_preventivo.Visible = False
        div_dettaglio_gruppi.Visible = False

        'setPadding("cerca")
    End Sub

    Protected Function VediUltimoCalcolo(idtipo As Integer) As String

        '1 visualizza sempre
        '0 visualizza secondo condizioni

        'aggiunto Salvo 22.12.2022
        Dim ris As String = ""

        Try



            ''vecchia visualizzazione salvo 03.01.2023
            'old_ultimo_gruppo.Text = ""
            'listVecchioCalcolo.Visible = True
            'listVecchioCalcolo.DataBind()
            'btnVediUltimoCalcolo.Visible = False

            'Exit Function 'test




            'Dim numprev As String = lblNumPreventivo.Text
            'Dim numCalcoloPrev As String = numCalcolo.Text
            'Dim idDOC As String = idPreventivo.Text


            'Dim sqlstr As String = "Select gruppi.id_gruppo, gruppi.cod_gruppo As gruppo, preventivi_costi.id_elemento, "
            'sqlstr += "preventivi_costi.nome_costo, ISNULL((imponibile+iva_imponibile+ISNULL(imponibile_onere,0)+ISNULL(iva_onere,0)),0) As valore_costo, "
            'sqlstr += "ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, id_a_carico_di,preventivi_costi.obbligatorio, "
            'sqlstr += "id_metodo_stampa, ISNULL(selezionato,'False') As selezionato, ISNULL(preventivi_costi.omaggiato,'False') As omaggiato, "
            'sqlstr += "ISNULL(preventivi_costi.omaggiabile,'False') As omaggiabile, ISNULL(commissioni_imponibile,0)+ISNULL(commissioni_iva,0) As commissioni "
            'sqlstr += "FROM preventivi_costi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON preventivi_costi.id_gruppo=gruppi.id_gruppo "
            'sqlstr += "LEFT JOIN condizioni_elementi WITH(NOLOCK) ON preventivi_costi.id_elemento=condizioni_elementi.id "

            'sqlstr += "WHERE ((id_documento = " & idDOC & ") And (num_calcolo = " & CInt(numCalcoloPrev) - 1 & ")) And ordine_stampa<>'5' "

            ''sqlstr += "WHERE ((id_documento = @id_preventivo) And (num_calcolo = @num_calcolo_preventivo-1)) And ordine_stampa<>'5' "
            'sqlstr += "And ISNULL(franchigia_attiva,'1')='1' AND (NOT (preventivi_costi.ordine_stampa='7' AND preventivi_costi.franchigia_attiva Is NULL) "
            'sqlstr += "Or condizioni_elementi.tipologia='KM_EXTRA') AND ISNULL(condizioni_elementi.tipologia,'')<>'RIMUOVI_RIFORNIMENTO' "
            'sqlstr += "And Not (NOT valore_percentuale IS NULL AND ordine_stampa<>'2') ORDER BY id_gruppo,ordine_stampa, "
            'sqlstr += "ISNULL(condizioni_elementi.ordinamento_vis,0) DESC, nome_costo "


            'sqlUltimoPreventiviCosti.SelectCommand = sqlstr
            'sqlUltimoPreventiviCosti.DataBind()
            'listVecchioCalcolo.DataBind()



            'preventivo precedente


            ris = ris


            If Not IsDBNull(listVecchioCalcolo.FindControl("ctl00")) Then '.FindControl("ctl00").FindControl("costo_scontato")) Then

                'If Not IsDBNull(listVecchioCalcolo.FindControl("ctl00")) Then '.FindControl("ctl00").FindControl("costo_scontato")) Then
                '    Exit Function
                'End If



                Dim lbl As Label
                lbl = listVecchioCalcolo.FindControl("ctl00").FindControl("costo_scontato")

                Dim ImportoPrecedente As Double = 0
                If lbl.Text <> "" Then
                    ImportoPrecedente = CDbl(lbl.Text)
                End If

                'preventivo attuale
                lbl = listPreventiviCosti.FindControl("ctl00").FindControl("costo_scontato")
                Dim ImportoAttuale As Double = 0
                If lbl.Text <> "" Then
                    ImportoAttuale = CDbl(lbl.Text)
                End If

                Dim txtMsg As String = ""

                If ImportoAttuale > ImportoPrecedente Then

                    txtMsg = "Attenzione la nuova tariffa prenotabile è superiore a quella inviata precedentemente tramite preventivo."
                    'lbl_msg_tariffe_diverse.Text = txtMsg
                    'lbl_msg_tariffe_diverse.Visible = True
                    'lbl_msg_tariffe_diverse.BackColor = Drawing.Color.White
                    'lbl_msg_tariffe_diverse.ForeColor = Drawing.Color.Red
                    'listVecchioCalcolo.Visible = True

                    ris = txtMsg

                ElseIf ImportoAttuale < ImportoPrecedente Then

                    txtMsg = "Attenzione la nuova tariffa prenotabile è inferiore a quella inviata precedentemente tramite preventivo, in alternativa puoi elaborare una nuova quotazione."
                    'lbl_msg_tariffe_diverse.Text = txtMsg
                    'lbl_msg_tariffe_diverse.Visible = True
                    'lbl_msg_tariffe_diverse.BackColor = Drawing.Color.White
                    'lbl_msg_tariffe_diverse.ForeColor = Drawing.Color.Red
                    'listVecchioCalcolo.Visible = True
                    ris = txtMsg


                Else
                    'definitivo
                    lbl_msg_tariffe_diverse.Text = ""
                    'lbl_msg_tariffe_diverse.Visible = False
                    'lbl_msg_tariffe_diverse.BackColor = Drawing.Color.Transparent
                    'listVecchioCalcolo.Visible = False
                    ris = ""
                    '@ definitivo

                    'test
                    'txtMsg = "Vecchia Tariffa Uguale"
                    'lbl_msg_tariffe_diverse.Text = txtMsg
                    'lbl_msg_tariffe_diverse.Visible = True
                    'lbl_msg_tariffe_diverse.BackColor = Drawing.Color.White
                    'lbl_msg_tariffe_diverse.ForeColor = Drawing.Color.Green
                    'listVecchioCalcolo.Visible = True
                    'ris = "1"
                    '@ test



                End If




            Else


                lbl_msg_tariffe_diverse.Text = ""
                'lbl_msg_tariffe_diverse.Visible = False
                'lbl_msg_tariffe_diverse.BackColor = Drawing.Color.Transparent
                'listVecchioCalcolo.Visible = False
                ris = ""


            End If 'If Not IsNothing(lbl) Then

            ris = ris


        Catch ex As Exception

            'Libreria.genUserMsgBox(Page, "VediUltiCalcolo error : " & ex.Message)
            ris = "0"

        End Try

        Return ris


    End Function


    Protected Sub btnVediUltimoCalcolo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVediUltimoCalcolo.Click

        old_ultimo_gruppo.Text = ""
        listVecchioCalcolo.Visible = True
        listVecchioCalcolo.DataBind()
        'btnVediUltimoCalcolo.Visible = False 'sempre visibile 

        If btnVediUltimoCalcolo.Text = "Vedi dett. precedente" Then

            btnVediUltimoCalcolo.Text = "Nascondi dett. precedente"
            old_ultimo_gruppo.Text = ""

            'verifica importi precedenti
            Dim ris As String = VediUltimoCalcolo(0)
            ris = ris

            If ris <> "" Then
                lbl_msg_tariffe_diverse.Text = ris
                lbl_msg_tariffe_diverse.Visible = True
                lbl_msg_tariffe_diverse.BackColor = Drawing.Color.White
                lbl_msg_tariffe_diverse.ForeColor = Drawing.Color.Red
            Else
                lbl_msg_tariffe_diverse.Text = ris
                lbl_msg_tariffe_diverse.Visible = False
            End If

            'sqlUltimoPreventiviCosti.FilterExpression = "num_calcolo=45"


            listVecchioCalcolo.Visible = True
            listVecchioCalcolo.DataBind()
            'btnVediUltimoCalcolo.Visible = False 'sempre visibile 



        Else
            btnVediUltimoCalcolo.Text = "Vedi dett. precedente"
            old_ultimo_gruppo.Text = ""
            listVecchioCalcolo.Visible = False
            listVecchioCalcolo.DataBind()

            'btnVediUltimoCalcolo.Visible = False 

        End If







        'If Session("btnVediCalcolo") = "" Then  'aggiunto salvo 03.01.2023

        '    Exit Sub
        'End If

        ' Session("btnVediCalcolo") = "1" 'aggiunto salvo 03.01.2023

        'VediUltimoCalcolo(0) 'aggiunto salvo 03.01.2023



    End Sub

    Protected Sub btnAggiungiExtra_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAggiungiExtra.Click

        Dim Riduzione_furto As Boolean = False
        Dim Riduzione_danni As Boolean = False
        Dim Eliminazione_Res As Boolean = False
        Dim idgruppo As String
        Dim id_gruppo_Label As Label
        Dim id_gruppoT As Label

        For i = 0 To listGruppi.Items.Count - 1
            Dim sel_gruppo As CheckBox = listGruppi.Items(i).FindControl("sel_gruppo")
            id_gruppoT = listGruppi.Items(i).FindControl("id_gruppo")
            id_gruppo_Label = listGruppi.Items(i).FindControl("id_gruppoLabel")
            If sel_gruppo.Checked Then
                idgruppo = id_gruppoT.Text
            End If
        Next

        If dropElementiExtra.SelectedValue = "248" Then

            'test richiama il pulsante Aggiorna


            btnAggiorna(idgruppo, id_gruppoT, True)

            'Exit Sub
            'fine test



        End If


        'Exit Sub 'TEST

        If numero_prenotazione.Text = "" Then
            If dropElementiExtra.SelectedValue <> "0" Then
                For i = 0 To listGruppi.Items.Count - 1
                    Dim sel_gruppo As CheckBox = listGruppi.Items(i).FindControl("sel_gruppo")
                    Dim id_gruppo As Label = listGruppi.Items(i).FindControl("id_gruppo")
                    If sel_gruppo.Checked Then
                        'AGGIUNGO L'ACCESSORIO SE NON E' STATO GIA' SPECIFICATO
                        If funzioni_comuni.accessorioExtraNonAggiunto(dropElementiExtra.SelectedValue, id_gruppo.Text, idPreventivo.Text, "", "", numCalcolo.Text) Then
                            nuovo_accessorio(dropElementiExtra.SelectedValue, id_gruppo.Text, "ELEMENTO", txtEtaPrimo.Text, txtEtaSecondo.Text)
                            'NEL CASO IN CUI L'ACCESSORIO EXTRA SIA IL GPS AGGIUNGO EVENTUALMENTE IL VAL
                            'SE E' STATO AGGINTO IL GPS AGGIUNGO EVENTUALMENTE IL VAL
                            If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
                                If funzioni_comuni.is_gps(dropElementiExtra.SelectedValue) Then
                                    nuovo_accessorio("", id_gruppo.Text, "VAL_GPS", txtEtaPrimo.Text, txtEtaSecondo.Text)
                                End If
                            End If
                        Else
                            Libreria.genUserMsgBox(Me, "L'accessorio è già stato aggiunto.")
                        End If
                    End If
                Next

                ultimo_gruppo.Text = ""
                listPreventiviCosti.DataBind()



            End If
        Else
            Libreria.genUserMsgBox(Me, "Per modificare la prenotazione appena effettuata utilizzare la funzionalità 'Prenotazioni'")
        End If


    End Sub

    Protected Sub btnAnnulla6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla6.Click
        'DA QUI SI RITORNA DIRETTAMENTE ALLA RICERCA DEI PREVENTIVI - ELIMINO I DATI DEL PREVENTIVO APPENA CREATO

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM preventivi_warning WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()
        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi_costi WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        If tipo_preventivo.Text = "nuovo" Then
            Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi WHERE id='" & idPreventivo.Text & "'", Dbc)
            Cmd.ExecuteNonQuery()
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        tab_cerca_tariffe.Visible = False
        tab_ricerca.Visible = True
        'tab_cerca_tariffe.Visible = False
        tab_prenotazioni.Visible = False
        tab_preventivo.Visible = False
        div_dettaglio_gruppi.Visible = False

        'setPadding("cerca")
    End Sub

    'Protected Sub btnAnnulla5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla5.Click
    '    tab_cerca_tariffe.Visible = False
    '    tab_ricerca.Visible = True
    'End Sub

    Protected Function getImportoPreventivo(ByVal id_preventivo As String, ByVal num_calcolo As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT imponibile_scontato+iva_imponibile FROM preventivi_costi WITH(NOLOCK) WHERE id_documento='" & id_preventivo & "' AND num_calcolo='" & num_calcolo & "' AND ordine_stampa='6'", Dbc)

        getImportoPreventivo = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub btnModificaConducente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaConducente.Click
        If btnModificaConducente.Text = "Scegli" Then
            If id_conducente.Text <> "" Then
                'SE L'UTENTE ERA STATO PRECEDENTEMENTE SELEZIONATO
                '1) L'ETA' VIENE AZZERATA
                txtEtaPrimo.Text = ""
                'SE PER L'UTENTE ERA STATO AGGIUNTO LO YOUNG DRIVER LO RIMUOVO
                If funzioni.esiste_young_driver_primo_guidatore(get_id_young_driver(), id_gruppo_auto_scelto.Text, numCalcolo.Text, idPreventivo.Text, "", "", "") Then
                    funzioni.rimuovi_costo_accessorio(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppo_auto_scelto.Text, get_id_young_driver(), "1", "EXTRA", dropTipoCommissione.SelectedValue)
                    listPreventiviCosti.DataBind()
                    Libreria.genUserMsgBox(Me, "Rimosso il costo dello Young Driver per il primo guidatore.")
                End If
            End If

            id_conducente.Text = ""

            'txtNomeConducente.Text = ""
            txtNomeConducente.ReadOnly = False
            'txtCognomeConducente.Text = ""
            txtCognomeConducente.ReadOnly = False
            'txtMailConducente.Text = ""
            'txtIndirizzoConducente.Text = ""
            txtMailConducente.ReadOnly = False

            'txtDataDiNascita.Text = ""
            txtDataDiNascita.ReadOnly = False

            btnModificaConducente.Text = "Chiudi"

            anagrafica_conducenti.Visible = True
        ElseIf btnModificaConducente.Text = "Chiudi" Then
            anagrafica_conducenti.Visible = False
            btnModificaConducente.Text = "Scegli"
        End If
    End Sub

    Protected Sub btnCambiaTariffa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCambiaTariffa.Click

        '# aggiunto salvo 19.01.2023
        txt_sconto_new.Enabled = True
        txt_sconto_new.ReadOnly = False
        '@end aggiunto salvo 19.01.2023

        '# aggiunto salvo 01.08.2023 per ricalcolo elemento320
        Session("cambiatariffanp") = "ok"
        '@end salvo 01.08.2023



        'ABILITO I CONTROLLI PER LA MODIFICA DELLA TARIFFA E DELLO SCONTO ED ELIMINO LE ATTUALI RIGHE DI CALCOLO
        dropTariffeGeneriche.Enabled = True
        dropTariffeParticolari.Enabled = True
        txtSconto.ReadOnly = False
        dropTipoSconto.Enabled = True
        btnCambiaTariffa.Visible = False

        If dropFonteCommissionabile.Visible Then
            dropFonteCommissionabile.Enabled = True
            dropTipoCommissione.Enabled = True
            btnAggiornaFontiCommissionabili.Visible = True
            dropTipoCommissione.Visible = True
        End If

        table_accessori_extra.Visible = False

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM preventivi_costi WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi_warning WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        listPreventiviCosti.DataBind()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub


    'Protected Sub btnModificaPrenotazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaPrenotazione.Click
    '    numCalcolo.Text = CInt(numCalcolo.Text) + 1

    '    'PER EVENTUALI CALCOLI PRECEDENTI ELIMINO LE RIGHE DI CALCOLO SUCCESSIVE ----------------------------------------------------------
    '    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '    Dbc.Open()
    '    Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM preventivi_costi WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
    '    Cmd.ExecuteNonQuery()

    '    Cmd.Dispose()
    '    Cmd = Nothing
    '    Dbc.Close()
    '    Dbc.Dispose()
    '    Dbc = Nothing
    '    '----------------------------------------------------------------------------------------------------------------------------------

    '    'SETTO CORRETTAMENTE LA SEZIONE DI RICERCA COSTO PER UN NUOVO PREVENTIVO-----------------------------------------------------------
    '    lblNumPreventivo.Text = ""
    '    lblTipoDocumento.Text = ""
    '    listPreventiviCosti.DataBind()

    '    dropTariffeGeneriche.Enabled = True
    '    dropTariffeParticolari.Enabled = True
    '    txtSconto.ReadOnly = False
    '    btnCambiaTariffa.Visible = False

    '    lblMxSconto.Visible = False
    '    listVecchioCalcolo.Visible = False
    '    'dropTipoCliente.SelectedValue = "0"
    '    'dropStazionePickUp.SelectedValue = "0"
    '    'dropStazioneDropOff.SelectedValue = "0"
    '    'dropOre1.SelectedValue = "11"
    '    'dropMinuti1.SelectedValue = "00"
    '    'dropOre2.SelectedValue = "11"
    '    'dropMinuti2.SelectedValue = "00"
    '    'txtDaData.Text = ""
    '    'txtAData.Text = ""
    '    'txtNome.Text = ""
    '    'txtCognome.Text = ""
    '    'txtMail.Text = ""
    '    'txtEtaPrimo.Text = ""
    '    'txtEtaSecondo.Text = ""
    '    'txtNumeroGiorni.Text = ""

    '    tab_ricerca.Visible = False
    '    tab_cerca_tariffe.Visible = True

    '    tab_preventivo.Visible = False
    '    'tab_prenotazioni.Visible = False
    '    table_tariffe.Visible = False
    '    table_accessori_extra.Visible = False

    '    table_gruppi.Visible = False


    '    btnModificaPrenotazione.Visible = False
    '    btnVediUltimoCalcolo.Visible = False
    '    btnSalvaPreventivo.Visible = True
    '    btnAnnulla3.Visible = True
    '    btnAnnulla4.Visible = False
    '    btnCerca.Visible = True
    '    'btnAnnulla0.Visible = True
    '    dropStazionePickUp.Enabled = True
    '    dropStazioneDropOff.Enabled = True
    '    dropMinuti1.Enabled = True
    '    dropOre1.Enabled = True
    '    dropOre2.Enabled = True
    '    dropMinuti2.Enabled = True
    '    dropTipoCliente.Enabled = True
    '    txtDaData.Enabled = True
    '    txtAData.Enabled = True
    '    txtEtaPrimo.Enabled = True
    '    txtEtaSecondo.Enabled = True
    '    txtNumeroGiorni.Enabled = True

    '    dropTariffeGeneriche.Enabled = True
    '    dropTariffeParticolari.Enabled = True

    '    'ABILITO LO SCONTO SOLAMENTE SE IL LIVELLO ACCESSO DEL RELATIVO PERMESSO E' 3
    '    If livello_accesso_sconto.Text = "3" Then
    '        txtSconto.Enabled = True
    '    Else
    '        txtSconto.Enabled = False
    '    End If
    'End Sub

    Protected Sub salvaPrenotazione()

        Dim sqla As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim provenienza As String

            Dim codice_stazione As String = Request.Cookies("SicilyRentCar")("codice_stazione")



            If Request.Cookies("SicilyRentCar")("stazione") = Costanti.id_stazione_sede Then
                provenienza = dropStazionePickUp.SelectedValue
            Else
                provenienza = Request.Cookies("SicilyRentCar")("stazione")
            End If

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(MAX(NUMPREN),0) FROM prenotazioni WITH(NOLOCK) WHERE codice_provenienza='" & provenienza & "'", Dbc)

            Dim num_prenotazione As Integer = Cmd.ExecuteScalar + 1

            If num_prenotazione = 1 Then
                'SE E' LA PRIMA PRENOTAZIONE DELLA STAZIONE RESTITUISCO IL PRIMO NUMERO
                'deve inserire il codice dell'operatore a meno che non si a SEDE che rimane PickUp
                num_prenotazione = codice_stazione & "000001"
                'num_prenotazione = CInt(Left(dropStazionePickUp.SelectedItem.Text, 2) & "000001")
            End If

            Dim id_prenotazione As String

            'TROVO L'ID-TARIFFA SCELTA --- NEL MENU A TENDINA IL SELECTED VALUE E' L'ID DI tariffe_righe
            Dim id_tariffe_righe As String
            Dim id_tariffa As String
            Dim tipo_tariffa As String = ""
            Dim codice_tariffa As String
            Dim cod As String
            Dim codice_edp As String
            Dim idDitta As String

            Dim prenotazione_no_tariffa As Boolean = False
            Dim pren_no_tariffa As String = "'0'"

            If dropTariffeGeneriche.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeGeneriche.SelectedValue
                codice_tariffa = "'" & Replace(dropTariffeGeneriche.SelectedItem.Text, "'", "''") & "'"
                tipo_tariffa = "generica"
            ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeParticolari.SelectedValue
                codice_tariffa = "'" & Replace(dropTariffeParticolari.SelectedItem.Text, "'", "''") & "'"
                tipo_tariffa = "fonte"
            Else
                'SOLO NEL CASO DI BROKER
                id_tariffe_righe = "NULL"
                tipo_tariffa = ""
                prenotazione_no_tariffa = True
                pren_no_tariffa = "'1'"
            End If

            'SE E' STATO SPECIFICATO UN CODICE EDP ALLORA COLLEGO LA PRENOTAZIONE ALLA DITTA
            If txtCodiceCliente.Text <> "" Then
                codice_edp = "'" & txtCodiceCliente.Text & "'"

                Cmd = New Data.SqlClient.SqlCommand("SELECT id_ditta FROM ditte WITH(NOLOCK) WHERE [CODICE EDP]='" & txtCodiceCliente.Text & "'", Dbc)
                idDitta = "'" & Cmd.ExecuteScalar & "'"
            ElseIf id_ditta.Text <> "" Then
                codice_edp = "NULL"
                idDitta = "'" & id_ditta.Text & "'"
            ElseIf id_ditta.Text = "" Then
                codice_edp = "NULL"
                idDitta = "'1'"
            End If

            If Not prenotazione_no_tariffa Then
                Cmd = New Data.SqlClient.SqlCommand("SELECT id_tariffa FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)
                id_tariffa = "'" & Cmd.ExecuteScalar & "'"

                'Cmd = New Data.SqlClient.SqlCommand("SELECT codice FROM tariffe WITH(NOLOCK) WHERE id=" & id_tariffa, Dbc)
                'codice_tariffa = "'" & Replace(Cmd.ExecuteScalar, "'", "''") & "'"

                Cmd = New Data.SqlClient.SqlCommand("SELECT CODTAR FROM tariffe WITH(NOLOCK) WHERE id=" & id_tariffa, Dbc)
                cod = "'" & Replace(Cmd.ExecuteScalar, "'", "''") & "'"
            Else
                id_tariffa = "NULL"
                codice_tariffa = "NULL"
                cod = "NULL"
            End If

            Dim data_uscita As String = txtDaData.Text
            Dim data_rientro As String = txtAData.Text

            data_uscita = getDataDb_senza_orario(data_uscita, Request.ServerVariables("HTTP_HOST"))
            data_rientro = getDataDb_senza_orario(data_rientro, Request.ServerVariables("HTTP_HOST"))

            Dim data_di_nascita As String = txtDataDiNascita.Text

            If data_di_nascita = "" Then
                data_di_nascita = "NULL"
            Else
                data_di_nascita = "'" & getDataDb_senza_orario(txtDataDiNascita.Text, Request.ServerVariables("HTTP_HOST")) & "'"
            End If

            Dim conducente As String
            If id_conducente.Text = "" Then
                conducente = "NULL"
            Else
                conducente = "'" & id_conducente.Text & "'"
            End If

            Dim id_preven As String
            If tipo_preventivo.Text = "richiama" Then
                id_preven = "'" & idPreventivo.Text & "'"
            Else
                id_preven = "NULL"
            End If

            'CAMPI DA GENERARE PER IL FUNZIONAMENTO DI P1000---------------------------------------------------------------------------------
            'PRORA_OUT - PRORA_PR: 01/01/1900 HH:mm:00
            Dim prora_out As String  'ORARIO USCITA
            Dim prora_pr As String   'ORARIO PRESUNTO RIENTRO
            prora_out = "1900-01-01 " & ore1.Text & ":" & minuti1.Text
            prora_pr = "1900-01-01 " & ore2.Text & ":" & minuti2.Text
            'TAR_VAL_DAL - TAR_VAL_AL
            Dim TAR_VAL_DAL As String
            Dim TAR_VAL_AL As String

            If Not prenotazione_no_tariffa Then
                Cmd = New Data.SqlClient.SqlCommand("SELECT vendibilita_da FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)
                TAR_VAL_DAL = Cmd.ExecuteScalar

                Cmd = New Data.SqlClient.SqlCommand("SELECT vendibilita_A FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)
                TAR_VAL_AL = Cmd.ExecuteScalar

                TAR_VAL_DAL = "'" & funzioni_comuni.getDataDb_senza_orario(TAR_VAL_DAL) & "'"

                TAR_VAL_AL = "'" & funzioni_comuni.getDataDb_senza_orario(TAR_VAL_AL) & "'"
            Else
                TAR_VAL_DAL = "NULL"
                TAR_VAL_AL = "NULL"
            End If

            'IMPORTO PREVENTIVO
            Dim importo_preventivo As String
            If tipo_preventivo.Text = "richiama" Then
                importo_preventivo = "'" & Replace(getImportoPreventivo(idPreventivo.Text, numCalcolo.Text), ",", ".") & "'"
            Else
                importo_preventivo = "NULL"
            End If

            'SE LA TARIFFA E' BROKER SALVO IL COSTO A CARICO DEL BROKER
            Dim a_carico_del_broker As String
            Dim giorni_to As String

            If tariffa_broker.Text = "1" And Not prenotazione_no_tariffa Then
                a_carico_del_broker = "'" & Replace(getCostoACaricoDelBroker(numCalcolo.Text), ",", ".") & "'"
                giorni_to = "'" & txtNumeroGiorni.Text & "'"
            Else
                a_carico_del_broker = "NULL"
                giorni_to = "NULL"
            End If

            Dim da_consegnare As String
            If dropGruppoDaConsegnare.SelectedValue <> "0" Then
                da_consegnare = "'" & dropGruppoDaConsegnare.SelectedValue & "'"
            Else
                da_consegnare = "NULL"
            End If

            If id_tariffe_righe <> "NULL" Then
                id_tariffe_righe = "'" & id_tariffe_righe & "'"
            End If

            Dim condizione_commissioni As String = ""
            If dropFonteCommissionabile.Visible Then
                condizione_commissioni = ",'" & dropFonteCommissionabile.SelectedValue & "','" & dropTipoCommissione.SelectedValue & "','" & Replace(txtPercentualeCommissionabile.Text, ",", ".") & "','" & txtNumeroGiorni.Text & "'"
            Else
                condizione_commissioni = ",NULL,NULL,NULL,NULL"
            End If
            '--------------------------------------------------------------------------------------------------------------------------------
            'STATUS: 0 - PRENOTATO
            '        1 - EFFETTUATO (DIVENTATO CONTRATTO)
            '        2 - NO SHOW
            '        3 - REFUSAL (ANNULLATO DA OPERATORE)
            '        4 - CANCEL (ANNULLATO DA CLIENTE)

            txtSconto.Text = txt_sconto_new.Text 'valorizza campo sconto per relativo salvataggio salvo 17.02.2023
            If dropTipoSconto.SelectedValue = "0" Then
                dropTipoSconto.SelectedValue = "1"  'valore tariffa
            End If
            '@end salvo

            sqla = "INSERT INTO prenotazioni (NUMPREN,num_calcolo,status,attiva,provenienza,codice_provenienza,PRID_stazione_out,PRID_stazione_pr," &
        "PRDATA_OUT,ore_uscita,minuti_uscita,PRDATA_PR,ore_rientro,minuti_rientro,ID_GRUPPO,ID_GRUPPO_APP,id_conducente,nome_conducente," &
        "cognome_conducente,eta_primo_guidatore,eta_secondo_guidatore,data_nascita,mail_conducente,indirizzo_conducente,riferimento_telefono,id_fonte,codice_edp,id_cliente,id_tariffa,id_tariffe_righe,tipo_tariffa,pren_broker_no_tariffa,sconto_applicato, tipo_sconto," &
        "id_preventivo,id_operatore_creazione,DATAPREN,PRORA_OUT,PRORA_PR,giorni,CODTAR,codice,TAR_VAL_DAL,TAR_VAL_AL,N_VOLOOUT,N_VOLOPR," &
        "IMPORTO_PREVENTIVO, importo_a_carico_del_broker, rif_to, giorni_to, id_fonte_commissionabile, tipo_commissione, commissione_percentuale, giorni_commissioni, commissioni_da_assegnare_web)" &
        " VALUES " &
        "('" & num_prenotazione & "','1',0,'1','banco','" & provenienza & "','" & dropStazionePickUp.SelectedValue & "','" & dropStazioneDropOff.SelectedValue & "'," &
        "convert(datetime,'" & data_uscita & "',102),'" & ore1.Text & "','" & minuti1.Text & "',convert(datetime,'" & data_rientro & "',102)," &
        "'" & ore2.Text & "','" & minuti2.Text & "','" & id_gruppo_auto_scelto.Text & "'," &
        da_consegnare & "," & conducente & "," &
        "'" & Replace(txtNomeConducente.Text, "'", "''") & "','" & Replace(txtCognomeConducente.Text, "'", "''") & "'," &
        "'" & txtEtaPrimo.Text & "','" & txtEtaSecondo.Text & "',convert(datetime," & data_di_nascita & ",102),'" & Replace(txtMailConducente.Text, "'", "''") & "','" & Replace(txtIndirizzoConducente.Text, "'", "''") & "','" & Replace(txtRifTel.Text, "'", "''") & "'," &
        "'" & dropTipoCliente.SelectedValue & "'," & codice_edp & "," & idDitta & "," & id_tariffa & "," & id_tariffe_righe & ",'" & tipo_tariffa & "'," & pren_no_tariffa & ",'" & Replace(txtSconto.Text, ",", ".") & "','" & dropTipoSconto.SelectedValue & "'," & id_preven & "," &
        "'" & Request.Cookies("SicilyRentCar")("idUtente") & "',convert(datetime,GetDate(),102)," &
        "'" & prora_out & "','" & prora_pr & "','" & txtNumeroGiorni.Text & "'," & codice_tariffa & "," & cod & ",convert(datetime," &
        TAR_VAL_DAL & ",102),convert(datetime," & TAR_VAL_AL & ",102),'" & Replace(txtVoloOut.Text, "'", "''") & "'," &
        "'" & Replace(txtVoloPr.Text, "'", "''") & "'," & importo_preventivo & "," & a_carico_del_broker & ",'" & Replace(txtRiferimentoTO.Text, "'", "''") & "'," & giorni_to & condizione_commissioni & ",'0')"

            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()

            'SALVATAGGIO DELLE NOTE (SE VALORIZZATE)
            If Trim(txtNote.Text) <> "" Then
                Dim mia_nota As note = New note
                With mia_nota
                    .id_documento = num_prenotazione
                    .id_tipo = enum_note_tipo.note_prenotazione
                    .nota = txtNote.Text
                    .SalvaRecord()
                End With
            End If

            lblNumPreventivo.Text = num_prenotazione
            lblTipoDocumento.Text = "Prenotazione Num.:"

            btnSalvaPrenotazione.Visible = False
            btnAnnulla6.Visible = True

            Cmd = New Data.SqlClient.SqlCommand("SELECT @@IDENTITY FROM prenotazioni WITH(NOLOCK)", Dbc)
            id_prenotazione = Cmd.ExecuteScalar

            idPrenotazione.Text = id_prenotazione

            'SE ERA STATO RICHIAMATO UN PREVENTIVO CHE E' STATO TRASFORMATO IN PRENOTAZIONE AGGIORNO IL PREVENTIVO COL NUMERO DI PRENOTAZIONE E LA PRENOTAZIONE
            'CON L'ID PREVENTIVO
            If tipo_preventivo.Text = "richiama" Then
                Cmd = New Data.SqlClient.SqlCommand("UPDATE preventivi SET num_prenotazione='" & num_prenotazione & "' WHERE id='" & idPreventivo.Text & "'", Dbc)
                Cmd.ExecuteScalar()
            End If

            If Not prenotazione_no_tariffa Then
                'SALVO LA RIGA DI CALCOLO E DI WARNING NELLE TABELLE PRENOTAZIONI_COSTI E PRENOTAZIONI_WARNING
                sqla = "INSERT INTO prenotazioni_costi (id_documento, num_calcolo, ordine_stampa, id_gruppo, id_elemento, num_elemento, nome_costo," &
            "selezionato,omaggiato, prepagato, valore_costo,valore_percentuale,imponibile,iva_imponibile,imponibile_scontato,iva_imponibile_scontato," &
            "imponibile_onere, iva_onere, aliquota_iva,iva_inclusa,scontabile,omaggiabile,acquistabile_nolo_in_corso,id_a_carico_di,id_metodo_stampa,obbligatorio, franchigia_attiva, id_unita_misura, qta, packed, commissioni_imponibile, commissioni_iva, commissioni_imponibile_originale, commissioni_iva_originale) " &
            "(SELECT '" & id_prenotazione & "','1', ordine_stampa, id_gruppo,id_elemento,num_elemento,nome_costo," &
            "selezionato,omaggiato,prepagato, valore_costo,valore_percentuale,imponibile,iva_imponibile,imponibile_scontato,iva_imponibile_scontato," &
            "imponibile_onere, iva_onere, aliquota_iva,iva_inclusa,scontabile,omaggiabile,acquistabile_nolo_in_corso,id_a_carico_di,id_metodo_stampa,obbligatorio, franchigia_attiva, id_unita_misura,qta, packed, commissioni_imponibile, commissioni_iva, commissioni_imponibile_originale, commissioni_iva_originale FROM preventivi_costi WITH(NOLOCK) WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "')"

                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                Cmd.ExecuteScalar()

                sqla = "INSERT INTO prenotazioni_warning (id_documento, num_calcolo, warning, id_operatore, tipo) " &
            "(SELECT '" & id_prenotazione & "','1',warning,id_operatore,tipo FROM preventivi_warning WITH(NOLOCK) WHERE id_documento='" & idPreventivo.Text & "' AND num_calcolo='" & numCalcolo.Text & "')"

                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                Cmd.ExecuteScalar()

                'SALVO LE RIGHE NELLA TABELLA commissioni_operatore

                sqla = "INSERT INTO commissioni_operatore (num_prenotazione, num_contratto, id_operatore, id_condizioni_elementi, nome_costo) " &
                "(SELECT '" & num_prenotazione & "','0','" & Request.Cookies("SicilyRentCar")("idUtente") & "', id_elemento, nome_costo FROM prenotazioni_costi WITH(NOLOCK) " &
                "WHERE id_documento='" & id_prenotazione & "' AND selezionato='1' AND (id_a_carico_di=2 OR id_elemento='" & Costanti.ID_tempo_km & "'))"

                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                Cmd.ExecuteScalar()
            End If
            'SE E' STATO SALVATO DIRETTAMENTE LA PRENOTAZIONE SENZA EFFETTUARE IL PREVENTIVO ELIMINO LE RIGHE DA PREVENTIVI - PREVENTIVI_COSTI E PREVENTIVI_WARNING
            If tipo_preventivo.Text = "nuovo" Then
                Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi_costi WHERE id_documento='" & idPreventivo.Text & "'", Dbc)
                Cmd.ExecuteScalar()
                Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi_warning WHERE id_documento='" & idPreventivo.Text & "'", Dbc)
                Cmd.ExecuteScalar()
                Cmd = New Data.SqlClient.SqlCommand("DELETE FROM preventivi WHERE id='" & idPreventivo.Text & "'", Dbc)
                Cmd.ExecuteScalar()
            End If

            numero_prenotazione.Text = num_prenotazione

            Libreria.genUserMsgBox(Me, "Prenotazione salvata correttamente. Numero Prenotazione: " & num_prenotazione)

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("error preventivi salvaPrenotazione  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")

        End Try





    End Sub

    Protected Function check_rif_to() As Boolean
        If Trim(txtRiferimentoTO.Text) <> "" Then
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 Nr_Pren FROM prenotazioni WITH(NOLOCK) WHERE rif_to='" & Replace(txtRiferimentoTO.Text, "'", "''") & "' AND id_fonte='" & dropTipoCliente.SelectedValue & "' AND status<>'1' AND status<>'2' AND attiva='1'", Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test = "" Then
                check_rif_to = True
            Else
                check_rif_to = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Else
            check_rif_to = True
        End If
    End Function

    Protected Function check_black_list() As Boolean
        'RESTITUISCE TRUE SE E' POSSIBILE SALVARE LA PRENOTAZIONE
        If id_conducente.Text <> "" Then
            'SE E' STATO SELEZIONATO UN UTENTE DA ANAGRAFICA QUESTO E' DI CERTO SELEZIONABILE (IL CONTROLLO E' BLOCCANTE)
            check_black_list = True
        ElseIf lblAvvisoBlackList.Text = txtCognomeConducente.Text & " " & txtNomeConducente.Text Then
            'L'AVVISO ERA GIA' STATO MOSTRATO - NOME E COGNOME NON SONO STATI VARIATI
            check_black_list = True
        Else
            'IN TUTTI GLI ALTRI CASI CONTROLLO CHE NON ESISTA IN ANAGRAFICA UN UTENTE (CONTROLLANDO NOME E COGNOME) CHE SIA IN BLACK LIST
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id_conducente FROM CONDUCENTI WITH(NOLOCK) WHERE cognome='" & Replace(txtCognomeConducente.Text, "'", "''") & "' AND nome='" & Replace(txtNomeConducente.Text, "'", "''") & "' AND black_list='1'", Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test = "" Then
                check_black_list = True
            Else
                check_black_list = False
            End If

            lblAvvisoBlackList.Text = txtCognomeConducente.Text & " " & txtNomeConducente.Text

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End If
    End Function


    Protected Sub Salva_Prenotazione()


        'PER PRIMA COSA, SE SPECIFICATO, VIENE CONTROLLATA L'UNICITA' DEL NUMERO DI RIFERIMENTO DEL TOUR OPERATOR
        Dim sqla As String = ""

        'inserito per evitare il doppio click
        If Session("salva_prenotazione_da_preventivi") = "OK" Then
            Libreria.genUserMsgBox(Page, "Salvataggio prenotazione in corso...")
            Exit Sub
        End If


        Try
            If check_rif_to() Or lblRifToOld.Text = txtRiferimentoTO.Text Then

                lblRifToOld.Text = txtRiferimentoTO.Text
                lblRiferimentoEsistente.Visible = False
                If (id_conducente.Text <> "") Or (txtNomeConducente.Text <> "" And txtCognomeConducente.Text <> "") Then
                    'CONTROLLO IN BLACK LIST : CERTAMENTE SE IL CLIENTE E' STATO SELEZIONATO DA DB NON LO PUO' ESSERE - CONTROLLO A MENO CHE 
                    'NON SIA GIA' STATO MOSTRATO L'AVVISO O SE E' STATO MOSTRATO MA E' STATO VARIATO IL NOME E COGNOME
                    If check_black_list() Then
                        Dim check_eta As String = ""
                        If id_conducente.Text = "" And txtDataDiNascita.Text <> "" Then
                            'SE NON E' STATO SELEZIONATO UN CONDUCENTE MA E' STATA SPECIFICATA UNA DATA DI NASCITA E' NECESSARIO CONTROLLARE SE C'E' 
                            'VARIAZIONE SULL'ETA' 
                            Dim test_eta As Integer
                            Dim month_nascita As Integer = Month(txtDataDiNascita.Text)
                            Dim day_nascita As Integer = Day(txtDataDiNascita.Text)
                            Dim data_nascita As DateTime = funzioni_comuni.getDataDb_con_orario2(txtDataDiNascita.Text & " 00:00:00")

                            test_eta = DateDiff(DateInterval.Year, data_nascita, Now())

                            If Month(Now()) < month_nascita Then
                                test_eta = CInt(test_eta) - 1
                            ElseIf Month(Now()) = month_nascita And Day(Now()) < day_nascita Then
                                test_eta = CInt(test_eta) - 1
                            End If

                            If CStr(test_eta) <> txtEtaPrimo.Text Then
                                check_eta = funzioni_comuni.gruppo_vendibile_eta_guidatori(id_gruppo_auto_scelto.Text, test_eta, "", "", "", "", "", "", "", False)

                                If check_eta = "0" Then
                                    'L'AUTO NON E' VENDIBILE - NON E' POSSIBILE COLLEGARE IL GUIDATORE ALLA PRENOTAZIONE DA SALVARE

                                ElseIf check_eta = "1" Then
                                    'IN QUESTO CASO IL GRUPPO AUTO E' VENDIBILE MA CON SUPPLEMENTO YOUNG DRIVER CHE DEVE ESSERE AGGIUNTO AUTOMATICAMENTE
                                    If Not funzioni.esiste_young_driver_primo_guidatore(get_id_young_driver(), id_gruppo_auto_scelto.Text, numCalcolo.Text, idPreventivo.Text, "", "", "") Then
                                        nuovo_accessorio(get_id_young_driver(), id_gruppo_auto_scelto.Text, "YOUNG PRIMO", test_eta, "")

                                        listPreventiviCosti.DataBind()

                                        txtEtaPrimo.Text = test_eta
                                        Libreria.genUserMsgBox(Me, "Aggiunto supplemento Young Driver.")
                                    End If
                                ElseIf check_eta = "4" Then
                                    'VENDIBILE SENZA LO YOUNG DRIVER - QUALORA FOSSE STATO PRECEDENTEMENTE AGGIUNTO PROVVEDO ALLA SUA RIMOZIONE
                                    txtEtaPrimo.Text = test_eta
                                    If funzioni.esiste_young_driver_primo_guidatore(get_id_young_driver(), id_gruppo_auto_scelto.Text, numCalcolo.Text, idPreventivo.Text, "", "", "") Then
                                        funzioni.rimuovi_costo_accessorio(idPreventivo.Text, "", "", "", numCalcolo.Text, id_gruppo_auto_scelto.Text, get_id_young_driver(), "1", "EXTRA", dropTipoCommissione.SelectedValue)
                                        listPreventiviCosti.DataBind()
                                        txtEtaPrimo.Text = test_eta
                                        Libreria.genUserMsgBox(Me, "Rimosso il costo dello Young Driver per il primo guidatore.")
                                    End If
                                End If
                            End If
                        End If

                        If check_eta <> "0" Then

                            Session("salva_prenotazione_da_preventivi") = "OK"      '22.03.2022

                            salvaPrenotazione()

                            If txtMailConducente.Text <> "" And tariffa_broker.Text <> "1" Then

                                Try

                                    genera_prenotazione()
                                    'invia email prenotazione a cliente e booking e stazione
                                    inviaMailPrenotazione(txtMailConducente.Text)

                                Catch ex As Exception

                                End Try

                            Else 'altre condizioni IF

                                Try

                                    'Aggiunta 14.01.2021 
                                    genera_prenotazione()
                                    'invia email prenotazione a booking e stazione
                                    'se email cliente presente invia anche a cliente
                                    Dim destinatario As String = txtMailConducente.Text
                                    If destinatario = "" Then
                                        destinatario = "booking@sicilyrentcar.it"
                                    End If
                                    inviaMailPrenotazione(destinatario)

                                Catch ex As Exception
                                    Libreria.genUserMsgBox(Me, "errore nella generazione del pdf: ")
                                End Try

                            End If

                            'btnPagamento.Visible = True

                            btnAggiornaPrenotazione.Visible = True
                            btnModificaConducente.Visible = False
                            btnAnnulla7.Visible = False
                            btnAnnulla6.Visible = False
                            btnAnnulla9.Visible = True


                            'aggiorna campi List_tariffe aggiunto salvo 27.06.2023
                            lbl_valore_senza_sconto.Text = HttpContext.Current.Session("valore_preventivo")
                            lbl_valore_con_sconto.Text = HttpContext.Current.Session("valore_preventivo_finale")
                            lbl_sconti_tariffe.Text = HttpContext.Current.Session("perc_sconto_tariffa_tutte")
                            lbl_list_tariffe.Text = HttpContext.Current.Session("list_tariffe")
                            lbl_list_tariffe_tempoKM.Text = HttpContext.Current.Session("list_tariffe_tempoKM")
                            Dim ListTariffePeriodo As String = HttpContext.Current.Session("list_tariffe_periodo")
                            Dim ListTariffeNome As String = HttpContext.Current.Session("list_tariffe_nome")
                            '# qui dovrebbe aggiornare i campi relativi alle tariffe assegnate salvo
                            Dim aggiornaListTariffe As String = funzioni_comuni_new.SalvaListTariffe("prenotazioni", idPrenotazione.Text, lbl_list_tariffe.Text, lbl_list_tariffe_tempoKM.Text, ListTariffePeriodo, ListTariffeNome, lbl_sconti_tariffe.Text)

                            '@ 









                        Else
                            Libreria.genUserMsgBox(Me, "Impossibile procedere: gruppo auto non vendibile a causa dell'età.")
                        End If
                    Else
                        Libreria.genUserMsgBox(Me, "Attenzione: esiste un cliente con lo stesso nome e cognome che si trova in BLACK LIST. E' possibile comunque salvare la prenotazione, tuttavia non sarà possibile successivamente collegare questo cliente alla prenotazione e/o al contratto.")
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "E' necessario selezionare un conducente o specificare nome e cognome per salvare una prenotazione.")
                End If

            Else
                lblRiferimentoEsistente.Visible = True
                lblRifToOld.Text = txtRiferimentoTO.Text
                Libreria.genUserMsgBox(Me, "Attenzione: esiste un'altra prenotazione con lo stesso numero di riferimento del TO. Cliccando nuovamente su SALVA la prenotazione verrà memorizzata ugualmente.")
            End If



        Catch ex As Exception
            HttpContext.Current.Response.Write("error Salva_Prenotazione : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")

        End Try



    End Sub





    Protected Sub btnSalvaPrenotazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvaPrenotazione.Click

        Salva_Prenotazione()

        'Tony 27/10/2022
        If cliente_is_broker(dropTipoCliente.SelectedValue) Then
            AggiornaDatiPerBroker()
        End If
        'FINE Tony

        'Tony 20-07-2023 - Mail Ins Pren Web manuale
        If dropTipoCliente.SelectedValue = "3" Then
            InvioMail()
        End If
    End Sub

    Protected Sub InvioMail()
        Dim StrArrayDati(4) As String

        Try
            Dim myMail As New MailMessage()

            Dim mySmtp As New SmtpClient("smtps.aruba.it")
            mySmtp.Port = 587
            mySmtp.Credentials = New System.Net.NetworkCredential("supporto@trinakriaservizi.it", "MailSupp@1")
            mySmtp.EnableSsl = True

            'Dim allegato As String = "C:\alazzara\comune" & AttualeFoglioPolarita & ".xlsx"

            Dim StringaEmailDestinatari As String = "contabilita@sicilyrentcar.it,it-support@sicilyrentcar.it"
            'Dim StringaEmailDestinatari As String = "it-support@sicilyrentcar.it"

            myMail = New MailMessage()
            myMail.From = New MailAddress("noreply@sicilyrentcar.it")
            myMail.To.Add(StringaEmailDestinatari)
            'myMail.Bcc.Add("tonyboyscoutlzz@gmail.com")
            myMail.Subject = "Prenotazione web manuale - " & lblNumPreventivo.Text
            'myMail.Attachments.Add(New Attachment(allegato))
            myMail.IsBodyHtml = True

            myMail.Body = ""

            mySmtp.Send(myMail)
            Console.WriteLine("Email inviata")

        Catch e As Exception
            Console.WriteLine(e.ToString)
        End Try
    End Sub

    Protected Sub genera_prenotazione()


        Dim sqla As String = "SELECT numpren, datapren, num_calcolo, (stazioni1.codice + ' - ' + stazioni1.nome_stazione) As stazione_pick, (stazioni2.codice + ' - ' + stazioni2.nome_stazione) As stazione_drop," &
             "CONVERT(char(10), prdata_out, 103) As prdata_out, CONVERT(char(10), prdata_pr, 103) As prdata_pr, ore_uscita, minuti_uscita, ore_rientro, minuti_rientro, " &
             "clienti_tipologia.descrizione As fonte, eta_primo_guidatore, eta_secondo_guidatore, giorni, giorni_to, cognome_conducente, nome_conducente, " &
             "mail_conducente, indirizzo_conducente, CONVERT(char(10), data_nascita, 103) As data_nascita, cod_gruppo_app, gruppi.cod_gruppo As codice_gruppo, " &
             "N_VOLOOUT, N_VOLOPR, rif_to, riferimento_telefono, id_cliente, codice_edp " &
             "FROM prenotazioni WITH(NOLOCK) INNER JOIN stazioni As stazioni1 WITH(NOLOCK) ON prenotazioni.prid_stazione_out=stazioni1.id " &
             "INNER JOIN stazioni As stazioni2 WITH(NOLOCK) ON prenotazioni.prid_stazione_pr=stazioni2.id " &
             "LEFT JOIN clienti_tipologia WITH(NOLOCK) ON prenotazioni.id_fonte=clienti_tipologia.id " &
             "INNER JOIN gruppi WITH(NOLOCK) ON prenotazioni.id_gruppo=gruppi.id_gruppo " &
             "WHERE numpren='" & lblNumPreventivo.Text & "' AND num_calcolo='1' AND prenotazioni.attiva='1'"


        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()


            If Rs.Read() Then
                Dim myPdfReader As PdfReader
                Dim pdfFormFields As AcroFields

                Dim newFile As String = Server.MapPath("\prenotazioni\" & "prenotazione_" & Rs("numpren") & ".pdf")
                Using file_stream As FileStream = New FileStream(newFile, FileMode.Create)
                    myPdfReader = New PdfReader(Server.MapPath("\modelli_documenti\prenotazione.pdf"))
                    Using myPdfStamper As PdfStamper = New PdfStamper(myPdfReader, file_stream)


                        pdfFormFields = myPdfStamper.AcroFields

                        pdfFormFields.SetField("Text1", "Numero prenotazione:   " & Rs("numpren"))
                        pdfFormFields.SetField("Text2", "Data Prenotazione:" & Rs("datapren"))
                        pdfFormFields.SetField("pickup_location", Rs("stazione_pick") & " " & Rs("prdata_out") & " " & Rs("ore_uscita") & ":" & Rs("minuti_uscita"))
                        pdfFormFields.SetField("dropoff_location", Rs("stazione_drop") & " " & Rs("prdata_pr") & " " & Rs("ore_rientro") & ":" & Rs("minuti_rientro"))
                        pdfFormFields.SetField("fonte", Rs("fonte") & "")
                        pdfFormFields.SetField("cod_convenzione", "")
                        pdfFormFields.SetField("eta1", Rs("eta_primo_guidatore") & "")
                        pdfFormFields.SetField("eta2", Rs("eta_secondo_guidatore") & "")
                        pdfFormFields.SetField("gg", Rs("giorni") & "")
                        pdfFormFields.SetField("gg_to", Rs("giorni_to") & "")
                        pdfFormFields.SetField("cognome", Rs("cognome_conducente") & "")
                        pdfFormFields.SetField("nome", Rs("nome_conducente") & "")
                        pdfFormFields.SetField("email", Rs("mail_conducente") & "")
                        pdfFormFields.SetField("indirizzo", Rs("indirizzo_conducente") & "")
                        pdfFormFields.SetField("nascita", Rs("data_nascita") & "")
                        If (Rs("cod_gruppo_app") & "") <> "" Then
                            pdfFormFields.SetField("gruppo", Rs("cod_gruppo_app"))
                        Else
                            pdfFormFields.SetField("gruppo", Rs("codice_gruppo"))
                        End If
                        pdfFormFields.SetField("nvolo1", Rs("N_VOLOOUT") & "")
                        pdfFormFields.SetField("nvolo2", Rs("N_VOLOPR") & "")
                        pdfFormFields.SetField("riferimento", Rs("rif_to") & "")
                        pdfFormFields.SetField("riftel", Rs("riferimento_telefono") & "")
                        If (Rs("codice_edp") & "") <> "" Then
                            pdfFormFields.SetField("fatturare_a", getNomeDittaFromEdp(Rs("codice_edp")))
                        ElseIf (Rs("id_cliente") & "") <> "" Then
                            pdfFormFields.SetField("fatturare_a", getNomeDittaFromId(Rs("id_cliente")))
                        End If


                        Dim miei_dati As DatiStampaPrenotazione = New DatiStampaPrenotazione
                        With miei_dati

                            'DETTAGLI COSTI ---------------------------------------------------------------------------------------------------------------
                            Dim lblIncluso As Label
                            Dim lblObbligatorio As Label
                            Dim lblInformativa As Label
                            Dim nome_costo As Label
                            Dim costo_scontato As Label
                            Dim chkOldOmaggio As CheckBox
                            Dim chkScegli As CheckBox
                            Dim chkOldScegli As CheckBox
                            Dim a_carico_to As Label

                            Dim prima_informativa As Boolean = True

                            For i = 0 To listPreventiviCosti.Items.Count - 1
                                lblIncluso = listPreventiviCosti.Items(i).FindControl("lblIncluso")
                                lblObbligatorio = listPreventiviCosti.Items(i).FindControl("lblObbligatorio")
                                lblInformativa = listPreventiviCosti.Items(i).FindControl("lblInformativa")
                                nome_costo = listPreventiviCosti.Items(i).FindControl("nome_costo")
                                costo_scontato = listPreventiviCosti.Items(i).FindControl("costo_scontato")
                                chkOldOmaggio = listPreventiviCosti.Items(i).FindControl("chkOldOmaggio")
                                chkScegli = listPreventiviCosti.Items(i).FindControl("chkScegli")
                                chkOldScegli = listPreventiviCosti.Items(i).FindControl("chkOldScegli")

                                'aggiunto 14.01.2021 x compatibilità con prenotazioni x generazione PDF da preventivi
                                a_carico_to = listPreventiviCosti.Items(i).FindControl("a_carico_to")


                                If lblIncluso.Visible Or lblObbligatorio.Visible Or lblInformativa.Visible Then
                                    If lblInformativa.Visible And prima_informativa Then
                                        prima_informativa = False
                                        .n_dettaglio_nome = .n_dettaglio_nome & vbCrLf
                                        .n_dettaglio_costo = .n_dettaglio_costo & vbCrLf
                                        .n_dettaglio_omaggio = .n_dettaglio_omaggio & vbCrLf
                                        .n_dettaglio_costo_to = .n_dettaglio_costo_to & vbCrLf
                                    End If

                                    .n_dettaglio_nome = .n_dettaglio_nome & nome_costo.Text & vbCrLf
                                    If costo_scontato.Visible Then
                                        .n_dettaglio_costo = .n_dettaglio_costo & costo_scontato.Text & vbCrLf
                                    Else
                                        .n_dettaglio_costo = .n_dettaglio_costo & vbCrLf
                                    End If

                                    If chkOldOmaggio.Checked Then
                                        .n_dettaglio_omaggio = .n_dettaglio_omaggio & "X" & vbCrLf
                                    Else
                                        .n_dettaglio_omaggio = .n_dettaglio_omaggio & vbCrLf
                                    End If

                                    .n_dettaglio_costo_to = .n_dettaglio_costo_to & vbCrLf
                                ElseIf chkScegli.Visible And chkOldScegli.Checked Then
                                    .n_dettaglio_nome = .n_dettaglio_nome & nome_costo.Text & vbCrLf
                                    .n_dettaglio_costo = .n_dettaglio_costo & costo_scontato.Text & vbCrLf
                                    If chkOldOmaggio.Checked Then
                                        .n_dettaglio_omaggio = .n_dettaglio_omaggio & "X" & vbCrLf
                                    Else
                                        .n_dettaglio_omaggio = .n_dettaglio_omaggio & vbCrLf
                                    End If

                                    .n_dettaglio_costo_to = .n_dettaglio_costo_to & vbCrLf
                                ElseIf Not chkScegli.Visible Then
                                    .n_dettaglio_nome = .n_dettaglio_nome & nome_costo.Text & vbCrLf
                                    If costo_scontato.Visible Then
                                        .n_dettaglio_costo = .n_dettaglio_costo & costo_scontato.Text & vbCrLf
                                    Else
                                        .n_dettaglio_costo = .n_dettaglio_costo & vbCrLf
                                    End If

                                    If tariffa_broker.Text = "1" Then
                                        .n_dettaglio_costo_to = .n_dettaglio_costo_to & a_carico_to.Text & vbCrLf
                                    Else
                                        .n_dettaglio_costo_to = .n_dettaglio_costo_to & vbCrLf
                                    End If

                                    .n_dettaglio_omaggio = .n_dettaglio_omaggio & vbCrLf
                                End If
                            Next
                            '------------------------------------------------------------------------------------------------------------------------------

                            pdfFormFields.SetField("dettaglio_nome", .n_dettaglio_nome)
                            pdfFormFields.SetField("dettaglio_costo", .n_dettaglio_costo)
                            pdfFormFields.SetField("dettaglio_omaggio", .n_dettaglio_omaggio)
                            pdfFormFields.SetField("dettaglio_costo_to", .n_dettaglio_costo_to)
                        End With


                    End Using
                End Using
                'myPdfStamper.FormFlattening = True
                'myPdfStamper.Close()
                'myPdfStamper.Dispose()

                'file_stream.Flush()
                'file_stream.Dispose()
                'file_stream.Close()

                myPdfReader.Close()
                'myPdfReader = Nothing


            End If


            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error preventivi genera_prenotazione : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")

        End Try


    End Sub

    'Protected Sub setta_pannello_pagamento()
    '    tab_pagamento.Visible = True
    '    tab_cerca_tariffe.Visible = False

    '    Dim Dati As New tabelle_pos_Scambio_Importo.DatiInizializzazione()
    '    Dati.IDStazione = Request.Cookies("SicilyRentCar")("stazione")
    '    Dati.NumeroDocumento = lblNumPreventivo.Text
    '    Dati.TipoDocumento = tabelle_pos_Scambio_Importo.TipoDocumento.Prenotazione
    '    Dati.ListaPreautorizzazioni = New System.Collections.Generic.List(Of tabelle_pos_Scambio_Importo.Preautorizzazione)

    '    'RECUPERO EVENTUALI PREAUTORIZZAZIONI 

    '    Dim preautorizzazioni(50) As String
    '    preautorizzazioni = cPagamenti.getListPreautorizzazioni(lblNumPreventivo.Text, "", "", "")

    '    Dim i As Integer = 0

    '    Dim pre As tabelle_pos_Scambio_Importo.Preautorizzazione
    '    pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()

    '    Do While preautorizzazioni(i) <> "0"
    '        pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()
    '        pre.Numero = preautorizzazioni(i)
    '        Dati.ListaPreautorizzazioni.Add(pre)
    '        i = i + 1
    '    Loop

    '    Dati.Importo = 5

    '    Dati.ImportoMassimoRimborsabile = 100   '<<<----- IMPOSTARE

    '    'Dati.PreSelectIDEnte = 13
    '    'Dati.PreSelectIDAcquireCircuito = 64
    '    'Dati.PreSelectIDFunzione = Costanti.POS_FunzioneIntegrazione
    '    'Dati.PreSelectPOSID = 16
    '    'Dati.PreSelectNumeroPreautorizzazione = "363320898"
    '    Dati.TestMode = False

    '    Dati.TipoPagamentoContanti = FiltroTipoPagamentoContanti.Prenotazione

    '    Scambio_Importo1.InizializzazioneDati(Dati)
    'End Sub

    'Protected Sub btnPagamento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPagamento.Click
    '    If id_conducente.Text <> "" Then
    '        setta_pannello_pagamento()
    '    Else
    '        Libreria.genUserMsgBox(Me, "E' necessario selezionare un guidatore dall'anagrafica per poter effettuare un pagamento.")
    '    End If

    'End Sub

    Protected Sub listPreventivi_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listPreventivi.ItemCommand

        Try
            If e.CommandName = "vedi" Then

                Dim data_uscita As Label = e.Item.FindControl("data_uscita")
                Dim uscita As DateTime = funzioni_comuni.getDataDb_con_orario2(data_uscita.Text & " 00:00:00")
                Dim oggi As DateTime = funzioni_comuni.getDataDb_con_orario2(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()) & " 23:59:59")

                '# aggiunto salvo 19.01.2023
                Dim data_creazione As Label = e.Item.FindControl("lbl_data_creazione")
                lbl_data_creazione.Text = data_creazione.Text
                '@

                ''########## SOLO PER TEST RESETTA IMPOSTA IMPORTO del prev test
                ' Dim xtest As Integer = funzioni_comuni_new.UpdateTest()

                'session apre preventivo da lista salvo 15.06.2023
                HttpContext.Current.Session("apre_preventivo") = "apre_preventivo"


                If DateDiff(DateInterval.Day, oggi, uscita) >= 0 Then

                    Dim num_preventivo As Label = e.Item.FindControl("num_preventivoLabel")

                    lbl_message.Text = Now.ToString 'aggiunto salvo

                    richiama_preventivo(num_preventivo.Text)

                    lbl_message.Text += " --- " & Now.ToString 'aggiunto salvo

                    'apre preventivo 03.01.2023 salvo
                    Session("apre_preventivo") = "apre_preventivo"
                    'listVecchioCalcolo.Visible = False

                    '# salvo 24.01.2023
                    'deve ricalcolare i costi SEMPRE con nuovo metodo 
                    'aggiungendo una nuova riga su Preventivi_costi 
                    'in questo caso richiama la procedura passando il valore opzionale FALSE
                    'per non cancellare il calcolo precedente
                    'aggiorna le righe solo una volta
                    'If Session("nuovo_calcolo_preventivo") = False Then
                    '    vedi_tariffe(False)  'richiamo a procedura aggiunto 
                    '    Session("nuovo_calcolo_preventivo") = True
                    'End If
                    '@end salvo 24.01.2023

                    'Dim msg As String = VediUltimoCalcolo(0)
                    'If msg <> "" Then
                    '    lbl_msg_tariffe_diverse.Visible = True
                    '    lbl_msg_tariffe_diverse.Text = msg
                    '    listVecchioCalcolo.Visible = True
                    'Else
                    '    lbl_msg_tariffe_diverse.Visible = False
                    '    lbl_msg_tariffe_diverse.Text = ""
                    '    listVecchioCalcolo.Visible = False
                    'End If

                Else
                    Libreria.genUserMsgBox(Me, "Preventivo scaduto.")
                End If
            ElseIf e.CommandName = "order_by_num" Then
                If lblOrderBY_prev.Text = " ORDER BY preventivi.num_preventivo DESC" Then
                    ricerca(" ORDER BY preventivi.num_preventivo ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_prev.Text = " ORDER BY preventivi.num_preventivo ASC" Then
                    ricerca(" ORDER BY preventivi.num_preventivo DESC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                Else
                    ricerca(" ORDER BY preventivi.num_preventivo ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_uscita" Then
                If lblOrderBY_prev.Text = " ORDER BY staz_uscita DESC" Then
                    ricerca(" ORDER BY staz_uscita ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_prev.Text = " ORDER BY staz_uscita ASC" Then
                    ricerca(" ORDER BY staz_uscita DESC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                Else
                    ricerca(" ORDER BY staz_uscita ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_data_uscita" Then
                If lblOrderBY_prev.Text = " ORDER BY preventivi.data_uscita DESC, preventivi.ore_uscita DESC, preventivi.minuti_uscita DESC" Then
                    ricerca(" ORDER BY preventivi.data_uscita ASC, preventivi.ore_uscita ASC, preventivi.minuti_uscita ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_prev.Text = " ORDER BY preventivi.data_uscita ASC, preventivi.ore_uscita ASC, preventivi.minuti_uscita ASC" Then
                    ricerca(" ORDER BY preventivi.data_uscita DESC, preventivi.ore_uscita DESC, preventivi.minuti_uscita DESC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                Else
                    ricerca(" ORDER BY preventivi.data_uscita ASC, preventivi.ore_uscita ASC, preventivi.minuti_uscita ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_rientro" Then
                If lblOrderBY_prev.Text = " ORDER BY staz_rientro DESC" Then
                    ricerca(" ORDER BY staz_rientro ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_prev.Text = " ORDER BY staz_rientro ASC" Then
                    ricerca(" ORDER BY staz_rientro DESC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                Else
                    ricerca(" ORDER BY staz_rientro ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_data_rientro" Then
                If lblOrderBY_prev.Text = " ORDER BY preventivi.data_rientro DESC, preventivi.ore_rientro DESC, preventivi.minuti_rientro DESC" Then
                    ricerca(" ORDER BY preventivi.data_rientro ASC, preventivi.ore_rientro ASC, preventivi.minuti_rientro ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_prev.Text = " ORDER BY preventivi.data_rientro ASC, preventivi.ore_rientro ASC, preventivi.minuti_rientro ASC" Then
                    ricerca(" ORDER BY preventivi.data_rientro DESC, preventivi.ore_rientro DESC, preventivi.minuti_rientro DESC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                Else
                    ricerca(" ORDER BY preventivi.data_rientro ASC, preventivi.ore_rientro ASC, preventivi.minuti_rientro ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_gruppo" Then
                If lblOrderBY_prev.Text = " ORDER BY cod_gruppo DESC" Then
                    ricerca(" ORDER BY cod_gruppo ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_prev.Text = " ORDER BY cod_gruppo ASC" Then
                    ricerca(" ORDER BY cod_gruppo DESC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                Else
                    ricerca(" ORDER BY cod_gruppo ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_tariffa" Then
                If lblOrderBY_prev.Text = " ORDER BY tariffa DESC" Then
                    ricerca(" ORDER BY tariffa ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_prev.Text = " ORDER BY tariffa ASC" Then
                    ricerca(" ORDER BY tariffa DESC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                Else
                    ricerca(" ORDER BY tariffa ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_tipo_cliente" Then
                If lblOrderBY_prev.Text = " ORDER BY tipo_cliente DESC" Then
                    ricerca(" ORDER BY tipo_cliente ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_prev.Text = " ORDER BY tariffa ASC" Then
                    ricerca(" ORDER BY tipo_cliente DESC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                Else
                    ricerca(" ORDER BY tipo_cliente ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_conducente" Then
                If lblOrderBY_prev.Text = " ORDER BY cognome_conducente DESC" Then
                    ricerca(" ORDER BY cognome_conducente ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_prev.Text = " ORDER BY cognome_conducente ASC" Then
                    ricerca(" ORDER BY cognome_conducente DESC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                Else
                    ricerca(" ORDER BY cognome_conducente ASC", lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
                End If
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error ListPreventivi_ItemCommand : <br/>" & ex.Message & "<br/>")
        End Try


    End Sub

    Protected Sub listPrenotazioni_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listPrenotazioni.ItemCommand

        Try
            If e.CommandName = "vedi" Then
                Dim num_prenotazione As Label = e.Item.FindControl("NUMPREN")
                richiama_prenotazione(num_prenotazione.Text)
            ElseIf e.CommandName = "order_by_num" Then
                If lblOrderBY_pren.Text = " ORDER BY prenotazioni.numpren DESC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY prenotazioni.numpren ASC", lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_pren.Text = " ORDER BY prenotazioni.numpren ASC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY prenotazioni.numpren DESC", lblOrderBY_cnt.Text)
                Else
                    ricerca(lblOrderBY_prev.Text, " ORDER BY prenotazioni.numpren ASC", lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_rif_to" Then
                If lblOrderBY_pren.Text = " ORDER BY prenotazioni.rif_to DESC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY prenotazioni.rif_to ASC", lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_pren.Text = " ORDER BY prenotazioni.rif_to ASC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY prenotazioni.rif_to DESC", lblOrderBY_cnt.Text)
                Else
                    ricerca(lblOrderBY_prev.Text, " ORDER BY prenotazioni.rif_to ASC", lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_conducente" Then
                If lblOrderBY_pren.Text = " ORDER BY cognome_conducente DESC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY cognome_conducente ASC", lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_pren.Text = " ORDER BY cognome_conducente ASC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY cognome_conducente DESC", lblOrderBY_cnt.Text)
                Else
                    ricerca(lblOrderBY_prev.Text, " ORDER BY cognome_conducente ASC", lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_stato" Then
                If lblOrderBY_pren.Text = " ORDER BY status DESC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY status ASC", lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_pren.Text = " ORDER BY status ASC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY status DESC", lblOrderBY_cnt.Text)
                Else
                    ricerca(lblOrderBY_prev.Text, " ORDER BY status ASC", lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_uscita" Then
                If lblOrderBY_pren.Text = " ORDER BY staz_uscita DESC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY staz_uscita ASC", lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_pren.Text = " ORDER BY staz_uscita ASC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY staz_uscita DESC", lblOrderBY_cnt.Text)
                Else
                    ricerca(lblOrderBY_prev.Text, " ORDER BY staz_uscita ASC", lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_data_uscita" Then
                If lblOrderBY_prev.Text = " ORDER BY prenotazioni.prdata_out DESC, prenotazioni.ore_uscita DESC, prenotazioni.minuti_uscita DESC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY prenotazioni.prdata_out ASC, prenotazioni.ore_uscita ASC, prenotazioni.minuti_uscita ASC", lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_pren.Text = " ORDER BY prenotazioni.prdata_out ASC, prenotazioni.ore_uscita ASC, prenotazioni.minuti_uscita ASC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY prenotazioni.prdata_out DESC, prenotazioni.ore_uscita DESC, prenotazioni.minuti_uscita DESC", lblOrderBY_cnt.Text)
                Else
                    ricerca(lblOrderBY_prev.Text, " ORDER BY prenotazioni.prdata_out ASC, prenotazioni.ore_uscita ASC, prenotazioni.minuti_uscita ASC", lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_rientro" Then
                If lblOrderBY_pren.Text = " ORDER BY staz_rientro DESC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY staz_rientro ASC", lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_pren.Text = " ORDER BY staz_rientro ASC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY staz_rientro DESC", lblOrderBY_cnt.Text)
                Else
                    ricerca(lblOrderBY_prev.Text, " ORDER BY staz_rientro ASC", lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_data_rientro" Then
                If lblOrderBY_prev.Text = " ORDER BY prenotazioni.prdata_pr DESC, prenotazioni.ore_rientro DESC, prenotazioni.minuti_rientro DESC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY prenotazioni.prdata_pr ASC, prenotazioni.ore_rientro ASC, prenotazioni.minuti_rientro ASC", lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_pren.Text = " ORDER BY prenotazioni.prdata_pr ASC, prenotazioni.ore_rientro ASC, prenotazioni.minuti_rientro ASC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY prenotazioni.prdata_pr DESC, prenotazioni.ore_rientro DESC, prenotazioni.minuti_rientro DESC", lblOrderBY_cnt.Text)
                Else
                    ricerca(lblOrderBY_prev.Text, " ORDER BY prenotazioni.prdata_pr ASC, prenotazioni.ore_rientro ASC, prenotazioni.minuti_rientro ASC", lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_gruppo" Then
                If lblOrderBY_pren.Text = " ORDER BY cod_gruppo DESC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY cod_gruppo ASC", lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_pren.Text = " ORDER BY cod_gruppo ASC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY cod_gruppo DESC", lblOrderBY_cnt.Text)
                Else
                    ricerca(lblOrderBY_prev.Text, " ORDER BY cod_gruppo ASC", lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_targa" Then
                If lblOrderBY_pren.Text = " ORDER BY targa_gruppo_speciale DESC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY targa_gruppo_speciale ASC", lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_pren.Text = " ORDER BY targa_gruppo_speciale ASC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY targa_gruppo_speciale DESC", lblOrderBY_cnt.Text)
                Else
                    ricerca(lblOrderBY_prev.Text, " ORDER BY targa_gruppo_speciale ASC", lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_tariffa" Then
                If lblOrderBY_pren.Text = " ORDER BY tariffa DESC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY tariffa ASC", lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_pren.Text = " ORDER BY tariffa ASC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY tariffa DESC", lblOrderBY_cnt.Text)
                Else
                    ricerca(lblOrderBY_prev.Text, " ORDER BY tariffa ASC", lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_prepag" Then
                If lblOrderBY_pren.Text = " ORDER BY prepagata DESC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY prepagata ASC", lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_pren.Text = " ORDER BY prepagata ASC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY prepagata DESC", lblOrderBY_cnt.Text)
                Else
                    ricerca(lblOrderBY_prev.Text, " ORDER BY prepagata ASC", lblOrderBY_cnt.Text)
                End If
            ElseIf e.CommandName = "order_by_tipo_cliente" Then
                If lblOrderBY_pren.Text = " ORDER BY tipo_cliente DESC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY tipo_cliente ASC", lblOrderBY_cnt.Text)
                ElseIf lblOrderBY_pren.Text = " ORDER BY tipo_cliente ASC" Then
                    ricerca(lblOrderBY_prev.Text, " ORDER BY tipo_cliente DESC", lblOrderBY_cnt.Text)
                Else
                    ricerca(lblOrderBY_prev.Text, " ORDER BY tipo_cliente ASC", lblOrderBY_cnt.Text)
                End If
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error listPrenotazioni_itemcommand  : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try


    End Sub

    Protected Sub listContratti_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listContratti.ItemCommand
        Try
            If e.CommandName = "vedi" Then
                Dim idLabel As Label = e.Item.FindControl("idLabel")
                richiama_contratto(idLabel.Text)
            ElseIf e.CommandName = "order_by_num" Then
                If lblOrderBY_cnt.Text = " ORDER BY contratti.num_contratto DESC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY contratti.num_contratto ASC")
                ElseIf lblOrderBY_cnt.Text = " ORDER BY contratti.num_contratto ASC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY contratti.num_contratto DESC")
                Else
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY contratti.num_contratto ASC")
                End If
            ElseIf e.CommandName = "order_by_rif_to" Then
                If lblOrderBY_cnt.Text = " ORDER BY contratti.rif_to DESC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY contratti.rif_to ASC")
                ElseIf lblOrderBY_cnt.Text = " ORDER BY contratti.rif_to ASC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY contratti.rif_to DESC")
                Else
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY contratti.rif_to ASC")
                End If
            ElseIf e.CommandName = "order_by_conducente" Then
                If lblOrderBY_cnt.Text = " ORDER BY cognome_primo_conducente DESC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY cognome_primo_conducente ASC")
                ElseIf lblOrderBY_cnt.Text = " ORDER BY cognome_primo_conducente ASC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY cognome_primo_conducente DESC")
                Else
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY cognome_primo_conducente ASC")
                End If
            ElseIf e.CommandName = "order_by_stato" Then
                If lblOrderBY_cnt.Text = " ORDER BY status DESC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY status ASC")
                ElseIf lblOrderBY_cnt.Text = " ORDER BY status ASC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY status DESC")
                Else
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY status ASC")
                End If
            ElseIf e.CommandName = "order_by_uscita" Then
                If lblOrderBY_cnt.Text = " ORDER BY staz_uscita DESC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY staz_uscita ASC")
                ElseIf lblOrderBY_cnt.Text = " ORDER BY staz_uscita ASC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY staz_uscita DESC")
                Else
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY staz_uscita ASC")
                End If
            ElseIf e.CommandName = "order_by_data_uscita" Then
                If lblOrderBY_cnt.Text = " ORDER BY contratti.data_uscita DESC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY contratti.data_uscita ASC")
                ElseIf lblOrderBY_cnt.Text = " ORDER BY contratti.data_uscita ASC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY contratti.data_uscita DESC")
                Else
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY contratti.data_uscita ASC")
                End If
            ElseIf e.CommandName = "order_by_presunto_rientro" Then
                If lblOrderBY_cnt.Text = " ORDER BY staz_presunto_rientro DESC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY staz_presunto_rientro ASC")
                ElseIf lblOrderBY_cnt.Text = " ORDER BY staz_presunto_rientro ASC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY staz_presunto_rientro DESC")
                Else
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY staz_presunto_rientro ASC")
                End If
            ElseIf e.CommandName = "order_by_data_presunto_rientro" Then
                If lblOrderBY_cnt.Text = " ORDER BY contratti.data_presunto_rientro DESC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY contratti.data_presunto_rientro ASC")
                ElseIf lblOrderBY_cnt.Text = " ORDER BY contratti.data_presunto_rientro ASC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY contratti.data_presunto_rientro DESC")
                Else
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY contratti.data_presunto_rientro ASC")
                End If
            ElseIf e.CommandName = "order_by_rientro" Then
                If lblOrderBY_cnt.Text = " ORDER BY staz_rientro DESC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY staz_rientro ASC")
                ElseIf lblOrderBY_cnt.Text = " ORDER BY staz_rientro ASC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY staz_rientro DESC")
                Else
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY staz_rientro ASC")
                End If
            ElseIf e.CommandName = "order_by_data_rientro" Then
                If lblOrderBY_cnt.Text = " ORDER BY data_rientro DESC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY data_rientro ASC")
                ElseIf lblOrderBY_cnt.Text = " ORDER BY data_rientro ASC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY data_rientro DESC")
                Else
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY data_rientro ASC")
                End If
            ElseIf e.CommandName = "order_by_veicolo" Then
                If lblOrderBY_cnt.Text = " ORDER BY veicolo DESC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY veicolo ASC")
                ElseIf lblOrderBY_cnt.Text = " ORDER BY veicolo ASC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY veicolo DESC")
                Else
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY veicolo ASC")
                End If
            ElseIf e.CommandName = "order_by_prepag" Then
                If lblOrderBY_cnt.Text = " ORDER BY prepagata DESC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY prepagata ASC")
                ElseIf lblOrderBY_cnt.Text = " ORDER BY prepagata ASC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY prepagata DESC")
                Else
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY prepagata ASC")
                End If
            ElseIf e.CommandName = "order_by_tipo_cliente" Then
                If lblOrderBY_cnt.Text = " ORDER BY tipo_cliente DESC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY tipo_cliente ASC")
                ElseIf lblOrderBY_cnt.Text = " ORDER BY tipo_cliente ASC" Then
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY tipo_cliente DESC")
                Else
                    ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, " ORDER BY tipo_cliente ASC")
                End If
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error listContratti_ItemCommand : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Sub btnCercaIniziale_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCercaIniziale.Click

        Session("preventivo_load") = "2"
        Session("btnVediCalcolo") = ""

        ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, lblOrderBY_cnt.Text)



    End Sub

    Protected Function condizione_where_contratti() As String
        Dim condizione As String = ""

        Try
            If dropStatoContratto.SelectedValue <> "Tutti" And dropStatoContratto.SelectedValue <> "900" Then
                condizione = condizione & " AND contratti.status='" & dropStatoContratto.SelectedValue & "'"
            ElseIf dropStatoContratto.SelectedValue = "900" Then
                condizione = condizione & " AND contratti.status<>7 AND NOT contratti.giorni_to IS NULL AND NOT (contratti.num_contratto IN (SELECT TOP 1 pagamenti_extra.N_CONTRATTO_RIF FROM pagamenti_extra WHERE pagamenti_extra.N_CONTRATTO_RIF=contratti.num_contratto AND pagamenti_extra.pagamento_broker=1) OR ISNULL(contratti.num_prenotazione,'-1') IN (SELECT TOP 1 pagamenti_extra.n_pren_rif FROM pagamenti_extra WHERE pagamenti_extra.N_PREN_RIF=ISNULL(contratti.num_prenotazione,'-1') AND pagamenti_extra.pagamento_broker=1))"
            End If

            If txtCercaNumInterno.Text <> "" Then
                Dim numero_cnt As String

                If txtCercaNumStaz.Text <> "" Then
                    numero_cnt = txtCercaNumStaz.Text & "000000"
                    numero_cnt = CInt(numero_cnt) + CInt(txtCercaNumInterno.Text)

                    condizione = condizione & " AND contratti.num_contratto='" & numero_cnt & "'"
                Else
                    condizione = condizione & " AND contratti.num_contratto LIKE '%" & Trim(txtCercaNumInterno.Text) & "'"
                End If
            ElseIf txtCercaNumStaz.Text <> "" Then
                condizione = condizione & " AND contratti.num_contratto LIKE '" & Trim(txtCercaNumStaz.Text) & "%'"
            End If

            If txtCercaCognome.Text <> "" Then
                'condizione = condizione & " AND (contratti_ditte.rag_soc LIKE '" & Replace(Trim(txtCercaCognome.Text), "'", "''") & "%')" 'old1
                condizione = condizione & " AND (conducenti1.cognome LIKE '" & Replace(Trim(txtCercaCognome.Text), "'", "''") & "%' OR conducenti2.cognome LIKE '" & Replace(Trim(txtCercaCognome.Text), "'", "''") & "%' OR contratti_ditte.rag_soc LIKE '" & Replace(Trim(txtCercaCognome.Text), "'", "''") & "%')" 'old2
                'condizione += " AND CONDUCENTI.COGNOME LIKE '" & Replace(Trim(txtCercaCognome.Text), "'", "''") & "%' "  'modificato salvo 11.04.2023
            End If
            If txtCercaNome.Text <> "" Then
                condizione = condizione & " AND (conducenti1.nome LIKE '" & Replace(Trim(txtCercaNome.Text), "'", "''") & "%' OR conducenti2.nome LIKE '" & Replace(Trim(txtCercaNome.Text), "'", "''") & "%')"
            End If

            If txtCercaPickUpDa.Text <> "" And txtCercaPickUpA.Text = "" Then
                Dim da_data As String = getDataDb_senza_orario(txtCercaPickUpDa.Text)
                condizione = condizione & " AND contratti.data_uscita >= convert(datetime,'" & da_data & "',102) "
            ElseIf txtCercaPickUpDa.Text = "" And txtCercaPickUpA.Text <> "" Then
                Dim a_data As String = getDataDb_con_orario(txtCercaPickUpA.Text & " 23:59:59")
                condizione = condizione & " AND contratti.data_uscita <= convert(datetime,'" & a_data & "',102) "
            ElseIf txtCercaPickUpDa.Text <> "" And txtCercaPickUpA.Text <> "" Then
                Dim da_data As String = getDataDb_senza_orario(txtCercaPickUpDa.Text)
                Dim a_data As String = getDataDb_con_orario(txtCercaPickUpA.Text & " 23:59:59")
                condizione = condizione & " AND contratti.data_uscita BETWEEN  convert(datetime,'" & da_data & "',102) AND  convert(datetime,'" & a_data & "',102) "
            End If

            If txtCercaDropOffDa.Text <> "" And txtCercaDropOffA.Text = "" Then
                Dim da_data As String = getDataDb_senza_orario(txtCercaDropOffDa.Text)
                condizione = condizione & " AND contratti.data_rientro >= convert(datetime,'" & da_data & "',102) "
            ElseIf txtCercaDropOffDa.Text = "" And txtCercaDropOffA.Text <> "" Then
                Dim a_data As String = getDataDb_con_orario(txtCercaDropOffA.Text & " 23:59:59")
                condizione = condizione & " AND contratti.data_rientro <= convert(datetime,'" & a_data & "',102) "
            ElseIf txtCercaDropOffDa.Text <> "" And txtCercaDropOffA.Text <> "" Then
                Dim da_data As String = getDataDb_senza_orario(txtCercaDropOffDa.Text)
                Dim a_data As String = getDataDb_con_orario(txtCercaDropOffA.Text & " 23:59:59")
                condizione = condizione & " AND contratti.data_rientro BETWEEN  convert(datetime,'" & da_data & "',102) AND  convert(datetime,'" & a_data & "',102) "
            End If

            If txtCercaPresRDa.Text <> "" And txtCercaPresRA.Text = "" Then
                Dim da_data As String = getDataDb_senza_orario(txtCercaPresRDa.Text)
                condizione = condizione & " AND contratti.data_presunto_rientro >= convert(datetime,'" & da_data & "',102) "
            ElseIf txtCercaPresRDa.Text = "" And txtCercaPresRA.Text <> "" Then
                Dim a_data As String = getDataDb_con_orario(txtCercaPresRA.Text & " 23:59:59")
                condizione = condizione & " AND contratti.data_presunto_rientro <= convert(datetime,'" & a_data & "',102) "
            ElseIf txtCercaPresRDa.Text <> "" And txtCercaPresRA.Text <> "" Then
                Dim da_data As String = getDataDb_senza_orario(txtCercaPresRDa.Text)
                Dim a_data As String = getDataDb_con_orario(txtCercaPresRA.Text & " 23:59:59")
                condizione = condizione & " AND contratti.data_presunto_rientro BETWEEN  convert(datetime,'" & da_data & "',102) AND  convert(datetime,'" & a_data & "',102) "
            End If

            If cercaStazionePickUp.SelectedValue <> "0" Then
                condizione = condizione & " AND contratti.id_stazione_uscita='" & cercaStazionePickUp.SelectedValue & "'"
            End If

            If cercaStazioneDropOff.SelectedValue <> "0" Then
                condizione = condizione & " AND contratti.id_stazione_rientro='" & cercaStazioneDropOff.SelectedValue & "'"
            End If

            If cercaStazionePresuntoRientro.SelectedValue <> "0" Then
                condizione = condizione & " AND contratti.id_stazione_presunto_rientro='" & cercaStazionePresuntoRientro.SelectedValue & "'"
            End If

            If dropPrenotazioniPrepagate.SelectedValue <> "-1" Then
                condizione = condizione & " AND contratti.prenotazione_prepagata='" & dropPrenotazioniPrepagate.SelectedValue & "'"
            End If

            If dropCercaTipoCliente.SelectedValue <> "-1" Then
                condizione = condizione & " AND contratti.id_fonte='" & dropCercaTipoCliente.SelectedValue & "'"
            End If

            If txtCercaTargaContratto.Text <> "" Then
                condizione = condizione & " AND contratti.targa='" & Replace(Trim(txtCercaTargaContratto.Text), "'", "''") & "'"
            End If

            If dropCercaGruppoContratto.Text <> "0" Then
                condizione = condizione & " AND ISNULL(ID_GRUPPO_APP,id_gruppo_auto)='" & dropCercaGruppoContratto.SelectedValue & "'"
            End If

            If txtCercaRiferimento.Text <> "" Then
                condizione = condizione & " AND contratti.rif_to='" & Replace(Trim(txtCercaRiferimento.Text), "'", "''") & "'"
            End If

            If dropCercaOperatore.SelectedValue <> "0" Then
                condizione = condizione & " AND contratti.id_operatore_creazione='" & dropCercaOperatore.SelectedValue & "'"
            End If

            If txtCercaCreazioneDa.Text <> "" And txtCercaCreazioneA.Text = "" Then
                Dim da_data As String = getDataDb_senza_orario(txtCercaCreazioneDa.Text)
                condizione = condizione & " AND contratti.data_creazione >= convert(datetime,'" & da_data & "',102) "
            ElseIf txtCercaCreazioneDa.Text = "" And txtCercaCreazioneA.Text <> "" Then
                Dim a_data As String = getDataDb_con_orario(txtCercaCreazioneA.Text & " 23:59:59")

                condizione = condizione & " AND contratti.data_creazione <= convert(datetime,'" & a_data & "',102) "
            ElseIf txtCercaCreazioneDa.Text <> "" And txtCercaCreazioneA.Text <> "" Then
                Dim da_data As String = getDataDb_senza_orario(txtCercaCreazioneDa.Text)
                Dim adt As String = DateAdd("d", 1, CDate(txtCercaCreazioneA.Text))
                Dim a_data As String = getDataDb_con_orario(adt & " 00:00:00")

                condizione = condizione & " AND contratti.data_creazione BETWEEN  convert(datetime,'" & da_data & "',102) AND  convert(datetime,'" & a_data & "',102) "

            End If

            If txtCercaNunFattura.Text <> "" Then
                Try
                    Dim test As Integer = CInt(txtCercaNunFattura.Text)
                    condizione = condizione & " AND contratti.num_contratto IN (SELECT TOP 1 fatture_nolo.num_contratto_rif FROM fatture_nolo WITH(NOLOCK) WHERE fatture_nolo.num_contratto_rif=contratti.num_contratto AND num_fattura='" & txtCercaNunFattura.Text & "' AND YEAR(data_fattura)='" & dropCercaAnnoFattura.SelectedItem.Text & "') "
                Catch ex As Exception
                    txtCercaNunFattura.Text = ""
                End Try


            End If


            'aggiunta ricerca x ditta 24.04.2021 Ipertext LIKE %%
            If txt_cerca_ditta.Text <> "" Then
                condizione += " AND contratti_ditte.rag_soc LIKE '%" & Replace(Trim(txt_cerca_ditta.Text), "'", "''") & "%'"
            End If




            condizione_where_contratti = condizione


        Catch ex As Exception





        End Try







    End Function

    Protected Function condizione_where_prenotazione() As String
        Dim condizione As String = ""

        If txtCercaNumInterno.Text <> "" Then
            Dim numero_pren As String

            If txtCercaNumStaz.Text <> "" Then
                numero_pren = Trim(txtCercaNumStaz.Text) & "000000"
                numero_pren = CInt(numero_pren) + CInt(txtCercaNumInterno.Text)

                condizione = condizione & " AND prenotazioni.NUMPREN='" & numero_pren & "'"
            Else
                condizione = condizione & " AND prenotazioni.NUMPREN LIKE '%" & Trim(txtCercaNumInterno.Text) & "'"
            End If
        ElseIf txtCercaNumStaz.Text <> "" Then
            condizione = condizione & " AND prenotazioni.NUMPREN LIKE '" & Trim(txtCercaNumStaz.Text) & "%'"
        End If

        If dropStatoPrenotazione.SelectedValue = "X" Then
            'PRENOTAZIONI IN STATO 'RICHIESTA DI ANNULLAMENTO'
            condizione = condizione & " AND (prenotazioni.status='0' AND NOT prenotazioni.id_motivo_annullamento IS NULL)"
        ElseIf dropStatoPrenotazione.SelectedValue = "D" Then
            'RICERCA SU PRENOTAZIONI DOPPIE
            condizione = condizione & " AND (prenotazioni.status='0' AND prenotazioni.PRDATA_OUT IN " &
                "(SELECT TOP 1 prenotazioni_2.PRDATA_OUT FROM prenotazioni As prenotazioni_2 WHERE prenotazioni_2.attiva='1' AND prenotazioni_2.status='0' AND prenotazioni.NR_pren<>prenotazioni_2.NR_pren AND prenotazioni.PRDATA_OUT=prenotazioni_2.PRDATA_OUT AND " &
                "prenotazioni.PRID_stazione_out=prenotazioni_2.PRID_stazione_out AND ((LOWER(prenotazioni.nome_conducente)=LOWER(prenotazioni_2.nome_conducente) AND " &
                "LOWER(prenotazioni.cognome_conducente)=LOWER(prenotazioni_2.cognome_conducente)) OR prenotazioni.id_conducente=prenotazioni_2.id_conducente) " &
                 "))"
        ElseIf dropStatoPrenotazione.SelectedValue = "5" Then
            condizione = condizione & " AND prenotazioni.aperto_da_ra_void='1' AND prenotazioni.status='0'"
        ElseIf dropStatoPrenotazione.SelectedValue = "0" Then
            condizione = condizione & " AND ISNULL(prenotazioni.aperto_da_ra_void,'0')='0' AND prenotazioni.status='0'"
        ElseIf dropStatoPrenotazione.SelectedValue <> "Tutti" Then
            condizione = condizione & " AND prenotazioni.status='" & dropStatoPrenotazione.SelectedValue & "'"

        End If

        If txtCercaCognome.Text <> "" Then
            condizione = condizione & " AND prenotazioni.cognome_conducente LIKE '" & Replace(Trim(txtCercaCognome.Text), "'", "''") & "%'"
        End If
        If txtCercaNome.Text <> "" Then
            condizione = condizione & " AND prenotazioni.nome_conducente LIKE '" & Replace(Trim(txtCercaNome.Text), "'", "''") & "%'"
        End If

        If txtCercaPickUpDa.Text <> "" And txtCercaPickUpA.Text = "" Then
            Dim da_data As String = getDataDb_senza_orario(txtCercaPickUpDa.Text)
            condizione = condizione & " AND prenotazioni.PRDATA_OUT >= convert(datetime,'" & da_data & "',102) "
        ElseIf txtCercaPickUpDa.Text = "" And txtCercaPickUpA.Text <> "" Then
            Dim a_data As String = getDataDb_con_orario(txtCercaPickUpA.Text & " 23:59:59")
            condizione = condizione & " AND prenotazioni.PRDATA_OUT <= convert(datetime,'" & a_data & "',102) "
        ElseIf txtCercaPickUpDa.Text <> "" And txtCercaPickUpDa.Text <> "" Then
            Dim da_data As String = getDataDb_senza_orario(txtCercaPickUpDa.Text)
            Dim a_data As String = getDataDb_con_orario(txtCercaPickUpA.Text & " 23:59:59")
            condizione = condizione & " AND prenotazioni.PRDATA_OUT BETWEEN convert(datetime,'" & da_data & "',102) AND convert(datetime,'" & a_data & "',102) "
        End If

        If txtCercaDropOffDa.Text <> "" And txtCercaDropOffA.Text = "" Then
            Dim da_data As String = getDataDb_senza_orario(txtCercaDropOffDa.Text)
            condizione = condizione & " AND prenotazioni.PRDATA_PR >= convert(datetime,'" & da_data & "',102) "
        ElseIf txtCercaDropOffDa.Text = "" And txtCercaDropOffA.Text <> "" Then
            Dim a_data As String = getDataDb_con_orario(txtCercaDropOffA.Text & " 23:59:59")
            condizione = condizione & " AND prenotazioni.PRDATA_PR <= convert(datetime,'" & a_data & "',102) "
        ElseIf txtCercaDropOffDa.Text <> "" And txtCercaDropOffA.Text <> "" Then
            Dim da_data As String = getDataDb_senza_orario(txtCercaDropOffDa.Text)
            Dim a_data As String = getDataDb_con_orario(txtCercaDropOffA.Text & " 23:59:59")
            condizione = condizione & " AND prenotazioni.PRDATA_PR BETWEEN convert(datetime,'" & da_data & "',102) AND convert(datetime,'" & a_data & "',102) "
        End If

        If cercaStazionePickUp.SelectedValue <> "0" Then
            condizione = condizione & " AND prenotazioni.PRID_stazione_out='" & cercaStazionePickUp.SelectedValue & "'"
        End If

        If cercaStazioneDropOff.SelectedValue <> "0" Then
            condizione = condizione & " AND prenotazioni.PRID_stazione_pr='" & cercaStazioneDropOff.SelectedValue & "'"
        End If

        If cercaPrenotazioniRibaltamento.SelectedValue <> "0" Then
            condizione = condizione & " AND prenotazioni.codice_provenienza='" & cercaPrenotazioniRibaltamento.SelectedValue & "'"
        End If

        If dropCercaTipoCliente.SelectedValue <> "-1" Then
            condizione = condizione & " AND prenotazioni.id_fonte='" & dropCercaTipoCliente.SelectedValue & "'"
        End If

        If dropPrenotazioniPrepagate.SelectedValue <> "-1" Then
            condizione = condizione & " AND prenotazioni.prepagata='" & dropPrenotazioniPrepagate.SelectedValue & "'"
        End If

        If txtCercaRiferimento.Text <> "" Then
            condizione = condizione & " AND prenotazioni.rif_to='" & Replace(Trim(txtCercaRiferimento.Text), "'", "''") & "'"
        End If

        If dropCercaGruppoPrenotazione.SelectedValue <> "0" Then
            condizione = condizione & " AND (prenotazioni.id_gruppo='" & dropCercaGruppoPrenotazione.SelectedValue & "' OR prenotazioni.id_gruppo_app='" & dropCercaGruppoPrenotazione.SelectedValue & "')"
        End If

        If txtCercaTargaPrenotazione.Text <> "" Then
            condizione = condizione & " AND prenotazioni.targa_gruppo_speciale='" & Replace(Trim(txtCercaTargaPrenotazione.Text), "'", "''") & "'"
        End If

        If dropCercaOperatore.SelectedValue <> "0" Then
            condizione = condizione & " AND prenotazioni.id_operatore_creazione='" & dropCercaOperatore.SelectedValue & "'"
        End If

        If txtCercaCreazioneDa.Text <> "" And txtCercaCreazioneA.Text = "" Then
            Dim da_data As String = getDataDb_senza_orario(txtCercaCreazioneDa.Text)
            condizione = condizione & " AND prenotazioni.DATAPREN >= convert(datetime,'" & da_data & "',102) "
        ElseIf txtCercaCreazioneDa.Text = "" And txtCercaCreazioneA.Text <> "" Then
            Dim a_data As String = getDataDb_con_orario(txtCercaCreazioneA.Text & " 23:59:59")
            condizione = condizione & " AND prenotazioni.DATAPREN <= convert(datetime,'" & a_data & "',102) "
        ElseIf txtCercaCreazioneDa.Text <> "" And txtCercaCreazioneA.Text <> "" Then
            Dim da_data As String = getDataDb_senza_orario(txtCercaCreazioneDa.Text)
            Dim a_data As String = getDataDb_con_orario(txtCercaCreazioneA.Text & " 23:59:59")
            condizione = condizione & " AND prenotazioni.DATAPREN BETWEEN convert(datetime,'" & da_data & "',102) AND convert(datetime,'" & a_data & "',102) "
        End If

        'aggiunto salvo 11.04.2023
        If txt_cerca_ditta.Text <> "" Then
            condizione += " AND Ditte.Rag_soc like '%" & txt_cerca_ditta.Text & "%'"
        End If




        Return condizione
    End Function

    Protected Sub ricerca(ByVal order_prev As String, ByVal order_pren As String, ByVal order_cnt As String)

        Dim sqla As String
        Dim error_n As Integer = 0

        Try
            If dropTipoDocumento.SelectedValue = "2" Then
                'CERCA SU PREVENTIVO
                Dim condizione As String = ""
                error_n = 1


                'If txt_cerca_ditta.Text <> "" Then 'aggiunto il 24.04.2021 su email FScalia
                '    condizione = condizione & " AND preventivi.cognome_conducente LIKE '" & Replace(Trim(txtCercaCognome.Text), "'", "''") & "%'"
                'End If



                If txtCercaNumInterno.Text <> "" Then
                    condizione = condizione & " AND preventivi.num_preventivo='" & Trim(txtCercaNumInterno.Text) & "'"
                End If
                If txtCercaCognome.Text <> "" Then
                    condizione = condizione & " AND preventivi.cognome_conducente LIKE '" & Replace(Trim(txtCercaCognome.Text), "'", "''") & "%'"
                End If

                If txtCercaNome.Text <> "" Then
                    condizione = condizione & " AND preventivi.nome_conducente LIKE '" & Replace(Trim(txtCercaNome.Text), "'", "''") & "%'"
                End If

                If txtCercaPickUpDa.Text <> "" And txtCercaPickUpA.Text = "" Then
                    Dim da_data As String = getDataDb_senza_orario(txtCercaPickUpDa.Text, Request.ServerVariables("HTTP_HOST"))
                    condizione = condizione & " AND preventivi.data_uscita >=convert(datetime,'" & da_data & "',102) "
                ElseIf txtCercaPickUpDa.Text = "" And txtCercaPickUpA.Text <> "" Then
                    Dim a_data As String = getDataDb_con_orario(txtCercaPickUpA.Text & " 23:59:59")
                    condizione = condizione & " AND preventivi.data_uscita <=convert(datetime,'" & a_data & "',102) "
                ElseIf txtCercaPickUpDa.Text <> "" And txtCercaPickUpDa.Text <> "" Then
                    Dim da_data As String = getDataDb_senza_orario(txtCercaPickUpDa.Text, Request.ServerVariables("HTTP_HOST"))
                    Dim a_data As String = getDataDb_con_orario(txtCercaPickUpA.Text & " 23:59:59")
                    condizione = condizione & " AND preventivi.data_uscita BETWEEN convert(datetime,'" & da_data & "',102) AND convert(datetime,'" & a_data & "',102) "
                End If

                If txtCercaDropOffDa.Text <> "" And txtCercaDropOffA.Text = "" Then
                    Dim da_data As String = getDataDb_senza_orario(txtCercaDropOffDa.Text, Request.ServerVariables("HTTP_HOST"))
                    condizione = condizione & " AND preventivi.data_rientro >=convert(datetime,'" & da_data & "',102) "
                ElseIf txtCercaDropOffDa.Text = "" And txtCercaDropOffA.Text <> "" Then
                    Dim a_data As String = getDataDb_con_orario(txtCercaDropOffA.Text & " 23:59:59")
                    condizione = condizione & " AND preventivi.data_rientro <=convert(datetime,'" & a_data & "',102) "
                ElseIf txtCercaDropOffDa.Text <> "" And txtCercaDropOffA.Text <> "" Then
                    Dim da_data As String = getDataDb_senza_orario(txtCercaDropOffDa.Text, Request.ServerVariables("HTTP_HOST"))
                    Dim a_data As String = getDataDb_con_orario(txtCercaDropOffA.Text & " 23:59:59")
                    condizione = condizione & " AND preventivi.data_rientro BETWEEN convert(datetime,'" & da_data & "',102) AND convert(datetime,'" & a_data & "',102) "
                End If

                If cercaStazionePickUp.SelectedValue <> "0" Then
                    condizione = condizione & " AND preventivi.id_stazione_uscita='" & cercaStazionePickUp.SelectedValue & "'"
                End If

                If cercaStazioneDropOff.SelectedValue <> "0" Then
                    condizione = condizione & " AND preventivi.id_stazione_rientro='" & cercaStazioneDropOff.SelectedValue & "'"
                End If

                If dropCercaTipoCliente.SelectedValue <> "-1" Then
                    condizione = condizione & " AND preventivi.id_fonte='" & dropCercaTipoCliente.SelectedValue & "'"
                End If

                If dropCercaOperatore.SelectedValue <> "0" Then
                    condizione = condizione & " AND preventivi.id_operatore_creazione='" & dropCercaOperatore.SelectedValue & "'"
                End If

                If txtCercaCreazioneDa.Text <> "" And txtCercaCreazioneA.Text = "" Then
                    Dim da_data As String = getDataDb_senza_orario(txtCercaCreazioneDa.Text)
                    condizione = condizione & " AND preventivi.data_creazione >= convert(date,'" & da_data & "',102) "
                ElseIf txtCercaCreazioneDa.Text = "" And txtCercaCreazioneA.Text <> "" Then
                    Dim a_data As String = getDataDb_con_orario(txtCercaCreazioneA.Text & " 23:59:59")
                    condizione = condizione & " AND preventivi.data_creazione <= convert(date,'" & a_data & "',102) "
                ElseIf txtCercaCreazioneDa.Text <> "" And txtCercaCreazioneA.Text <> "" Then
                    Dim da_data As String = getDataDb_senza_orario(txtCercaCreazioneDa.Text)
                    'Dim a_data As String = getDataDb_con_orario(txtCercaCreazioneA.Text & " 23:59:59")
                    'aggiungere +1 a data per la ricerca data 21.12.2020
                    Dim adt As String = DateAdd("d", 1, CDate(txtCercaCreazioneA.Text))
                    Dim a_data As String = getDataDb_con_orario(adt & " 00:00:00")

                    condizione = condizione & " AND preventivi.data_creazione BETWEEN convert(date,'" & da_data & "',102) AND convert(date,'" & a_data & "',102) "
                End If

                sqla = "SELECT preventivi.data_creazione, preventivi.id, preventivi.num_preventivo, (stazioni1.codice + ' ' + stazioni1.nome_stazione) As staz_uscita, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As staz_rientro, "
                sqla += "CONVERT(char(10),preventivi.data_uscita,103) As data_uscita, preventivi.ore_uscita, preventivi.minuti_uscita, CONVERT(Char(10),preventivi.data_rientro,103) As data_rientro, "
                sqla += "preventivi.ore_rientro, preventivi.minuti_rientro, gruppi.cod_gruppo, preventivi.cognome_conducente, preventivi.nome_conducente, clienti_tipologia.descrizione As tipo_cliente, "
                sqla += "(preventivi.CODTAR + ' ' + CONVERT(char(10), preventivi.TAR_VAL_DAL,103) + ' - ' + CONVERT(char(10), preventivi.TAR_VAL_AL,103)) As tariffa FROM preventivi WITH(NOLOCK) "
                sqla += "LEFT JOIN clienti_tipologia WITH(NOLOCK) ON preventivi.id_fonte=clienti_tipologia.id INNER JOIN stazioni AS stazioni1 WITH(NOLOCK) ON preventivi.id_stazione_uscita=stazioni1.id "
                sqla += "INNER JOIN stazioni As stazioni2 WITH(NOLOCK) ON preventivi.id_stazione_rientro=stazioni2.id INNER JOIN gruppi WITH(NOLOCK) ON preventivi.id_gruppo_auto=gruppi.id_gruppo "
                sqla += "WHERE (Not num_preventivo Is NULL) And (num_prenotazione Is NULL)  " & condizione & order_prev

                query_cerca_prev.Text = sqla

                lblOrderBY_prev.Text = order_prev

                sqlPreventivi.SelectCommand = query_cerca_prev.Text

                divPrenotazioni.Visible = False
                divPreventivi.Visible = True
                divContratti.Visible = False

                listPreventivi.DataBind()



            ElseIf dropTipoDocumento.SelectedValue = "1" Then

                'CERCA SU PRENOTAZIONI
                error_n = 2
                sqla = "SELECT prenotazioni_status.descrizione As status, prenotazioni.prepagata, prenotazioni.Nr_pren, prenotazioni.NUMPREN, "
                sqla += "(stazioni1.codice + ' ' + stazioni1.nome_stazione) As staz_uscita, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As staz_rientro, "
                sqla += "CONVERT(char(10),prenotazioni.PRDATA_OUT,103) As data_uscita, prenotazioni.ore_uscita, prenotazioni.minuti_uscita, "
                sqla += "CONVERT(Char(10),prenotazioni.PRDATA_PR,103) As data_rientro, prenotazioni.ore_rientro, prenotazioni.minuti_rientro, "
                sqla += "gruppi.cod_gruppo + '/' + gruppi2.cod_gruppo As cod_gruppo, prenotazioni.cognome_conducente, prenotazioni.nome_conducente, "
                sqla += "clienti_tipologia.descrizione As tipo_cliente, (prenotazioni.CODTAR + ' ' + CONVERT(char(10), "
                sqla += "prenotazioni.TAR_VAL_DAL,103) + ' - ' + CONVERT(char(10), prenotazioni.TAR_VAL_AL,103)) As tariffa, prenotazioni.rif_to, "
                sqla += "prenotazioni.targa_gruppo_speciale FROM prenotazioni WITH(NOLOCK) LEFT JOIN clienti_tipologia WITH(NOLOCK) "
                sqla += "ON prenotazioni.id_fonte=clienti_tipologia.id LEFT JOIN stazioni AS stazioni1 WITH(NOLOCK) ON prenotazioni.PRID_stazione_out=stazioni1.id "
                sqla += "LEFT JOIN stazioni As stazioni2 WITH(NOLOCK) ON prenotazioni.PRID_stazione_pr=stazioni2.id LEFT JOIN gruppi WITH(NOLOCK) ON prenotazioni.id_gruppo=gruppi.id_gruppo "
                sqla += "LEFT JOIN gruppi AS gruppi2 WITH(NOLOCK) ON prenotazioni.id_gruppo_app=gruppi2.id_gruppo LEFT JOIN prenotazioni_status WITH(NOLOCK) ON prenotazioni.status=prenotazioni_status.id "
                sqla += "WHERE prenotazioni.attiva='1' AND ISNULL(da_confermare, '0')='0' " & condizione_where_prenotazione() & order_pren

                query_cerca_pren.Text = sqla
                'Response.Write(query_cerca_pren.Text)
                'Response.End()

                lblOrderBY_pren.Text = order_pren
                sqlPrenotazioni.SelectCommand = query_cerca_pren.Text

                divPreventivi.Visible = False
                divPrenotazioni.Visible = True
                divContratti.Visible = False

                listPrenotazioni.DataBind()

            ElseIf dropTipoDocumento.SelectedValue = "3" Then
                'CONTRATTI
                error_n = 3
                'sqla = "Select contratti.id, contratti_status.descrizione As status, contratti.num_contratto, (stazioni1.codice + ' ' + stazioni1.nome_stazione) As staz_uscita, "
                'sqla += "(stazioni2.codice + ' ' + stazioni2.nome_stazione) As staz_rientro, (stazioni3.codice + ' ' + stazioni3.nome_stazione) As staz_presunto_rientro, "
                'sqla += "CONVERT(char(10),contratti.data_uscita,103) As data_uscita, DATEPART(hh,contratti.data_uscita) As ore_uscita, DATEPART(mi,contratti.data_uscita) As minuti_uscita, "
                'sqla += "CONVERT(Char(10),contratti.data_rientro,103) As data_rientro, DATEPART(hh,contratti.data_rientro) As ore_rientro, DATEPART(mi,contratti.data_rientro) As minuti_rientro, "
                'sqla += "CONVERT(Char(10),contratti.data_presunto_rientro,103) As data_presunto_rientro, DATEPART(hh,contratti.data_presunto_rientro) As ore_presunto_rientro, "
                'sqla += "DATEPART(mi,contratti.data_presunto_rientro) As minuti_presunto_rientro, veicoli.targa As veicolo, conducenti1.cognome As cognome_primo_conducente, "
                'sqla += "conducenti1.nome As nome_primo_conducente, conducenti2.cognome As cognome_secondo_conducente, conducenti2.nome As nome_secondo_conducente, prenotazione_prepagata As prepagata, "
                'sqla += "clienti_tipologia.descrizione As tipo_cliente, contratti.rif_to FROM contratti WITH(NOLOCK) LEFT JOIN stazioni AS stazioni1 WITH(NOLOCK) ON contratti.id_stazione_uscita=stazioni1.id "
                'sqla += "LEFT JOIN stazioni As stazioni2 WITH(NOLOCK) ON contratti.id_stazione_rientro=stazioni2.id LEFT JOIN stazioni As stazioni3 WITH(NOLOCK) ON "
                'sqla += "contratti.id_stazione_presunto_rientro=stazioni3.id LEFT JOIN veicoli WITH(NOLOCK) ON contratti.id_veicolo=veicoli.id LEFT JOIN conducenti As conducenti1 WITH(NOLOCK) "
                'sqla += "ON contratti.id_primo_conducente=conducenti1.id_conducente LEFT JOIN conducenti As conducenti2 WITH(NOLOCK) ON contratti.id_secondo_conducente=conducenti2.id_conducente "
                'sqla += "LEFT JOIN contratti_status WITH(NOLOCK) ON contratti.status=contratti_status.id LEFT JOIN clienti_tipologia WITH(NOLOCK) ON contratti.id_fonte=clienti_tipologia.id "
                'sqla += "WHERE contratti.attivo='1' " & condizione_where_contratti()


                'sqla = "Select contratti.id, contratti_status.descrizione As status, contratti.num_contratto, stazioni1.codice + ' ' + stazioni1.nome_stazione AS staz_uscita, stazioni2.codice + ' ' + stazioni2.nome_stazione AS staz_rientro, "
                'sqla += "stazioni3.codice + ' ' + stazioni3.nome_stazione AS staz_presunto_rientro, CONVERT(char(10), contratti.data_uscita, 103) AS data_uscita, DATEPART(hh, contratti.data_uscita) AS ore_uscita, DATEPART(mi, "
                'sqla += "contratti.data_uscita) AS minuti_uscita, CONVERT(Char(10), contratti.data_rientro, 103) AS data_rientro, DATEPART(hh, contratti.data_rientro) AS ore_rientro, DATEPART(mi, contratti.data_rientro) AS minuti_rientro, "
                'sqla += "Convert(Char(10), contratti.data_presunto_rientro, 103) As data_presunto_rientro, DATEPART(hh, contratti.data_presunto_rientro) As ore_presunto_rientro, DATEPART(mi, contratti.data_presunto_rientro) "
                'sqla += "AS minuti_presunto_rientro, veicoli.targa AS veicolo, conducenti1.COGNOME AS cognome_primo_conducente, conducenti1.nome AS nome_primo_conducente, "
                'sqla += "conducenti2.COGNOME AS cognome_secondo_conducente, conducenti2.nome As nome_secondo_conducente, contratti.prenotazione_prepagata As prepagata, clienti_tipologia.descrizione As tipo_cliente, "
                'sqla += "contratti.rif_to, GRUPPI.cod_gruppo "
                'sqla += "From contratti WITH (NOLOCK) INNER Join "
                'sqla += "GRUPPI On contratti.id_gruppo_auto = GRUPPI.ID_gruppo LEFT OUTER Join "
                'sqla += "stazioni As stazioni1 WITH (NOLOCK) ON contratti.id_stazione_uscita = stazioni1.id LEFT OUTER Join "
                'sqla += "stazioni As stazioni2 WITH (NOLOCK) ON contratti.id_stazione_rientro = stazioni2.id LEFT OUTER Join "
                'sqla += "stazioni As stazioni3 WITH (NOLOCK) ON contratti.id_stazione_presunto_rientro = stazioni3.id LEFT OUTER Join "
                'sqla += "veicoli WITH (NOLOCK) ON contratti.id_veicolo = veicoli.id LEFT OUTER Join "
                'sqla += "CONDUCENTI As conducenti1 WITH (NOLOCK) ON contratti.id_primo_conducente = conducenti1.ID_CONDUCENTE LEFT OUTER Join "
                'sqla += "CONDUCENTI As conducenti2 WITH (NOLOCK) ON contratti.id_secondo_conducente = conducenti2.ID_CONDUCENTE LEFT OUTER Join "
                'sqla += "contratti_status WITH (NOLOCK) ON contratti.status = contratti_status.id LEFT OUTER Join "
                'sqla += "clienti_tipologia WITH (NOLOCK) ON contratti.id_fonte = clienti_tipologia.id "
                'sqla += "WHERE contratti.attivo='1' " & condizione_where_contratti()

                'sqla = "Select contratti.id, contratti_status.descrizione As status, contratti.num_contratto, stazioni1.codice + ' ' + stazioni1.nome_stazione AS staz_uscita, stazioni2.codice + ' ' + stazioni2.nome_stazione AS staz_rientro,  "
                'sqla += "stazioni3.codice + ' ' + stazioni3.nome_stazione AS staz_presunto_rientro, CONVERT(char(10), contratti.data_uscita, 103) AS data_uscita, DATEPART(hh, contratti.data_uscita) AS ore_uscita, DATEPART(mi, "
                'sqla += "contratti.data_uscita) AS minuti_uscita, CONVERT(Char(10), contratti.data_rientro, 103) AS data_rientro, DATEPART(hh, contratti.data_rientro) AS ore_rientro, DATEPART(mi, contratti.data_rientro) AS minuti_rientro, "
                'sqla += "Convert(Char(10), contratti.data_presunto_rientro, 103) As data_presunto_rientro, DATEPART(hh, contratti.data_presunto_rientro) As ore_presunto_rientro, DATEPART(mi, contratti.data_presunto_rientro) "
                'sqla += "AS minuti_presunto_rientro, veicoli.targa AS veicolo, conducenti1.COGNOME AS cognome_primo_conducente, conducenti1.nome AS nome_primo_conducente, "
                'sqla += "conducenti2.COGNOME AS cognome_secondo_conducente, conducenti2.nome As nome_secondo_conducente, contratti.prenotazione_prepagata As prepagata, clienti_tipologia.descrizione As tipo_cliente, "
                'sqla += "contratti.rif_to, GRUPPI.cod_gruppo From MODELLI INNER Join "
                'sqla += "veicoli WITH (NOLOCK) ON MODELLI.ID_MODELLO = veicoli.id_modello INNER Join "
                'sqla += "GRUPPI On MODELLI.ID_Gruppo = GRUPPI.ID_gruppo RIGHT OUTER Join "
                'sqla += "contratti WITH (NOLOCK) LEFT OUTER Join "
                'sqla += "stazioni As stazioni1 WITH (NOLOCK) ON contratti.id_stazione_uscita = stazioni1.id LEFT OUTER Join "
                'sqla += "stazioni As stazioni2 WITH (NOLOCK) ON contratti.id_stazione_rientro = stazioni2.id LEFT OUTER Join "
                'sqla += "stazioni As stazioni3 WITH (NOLOCK) ON contratti.id_stazione_presunto_rientro = stazioni3.id ON veicoli.id = contratti.id_veicolo LEFT OUTER Join "
                'sqla += "CONDUCENTI As conducenti1 WITH (NOLOCK) ON contratti.id_primo_conducente = conducenti1.ID_CONDUCENTE LEFT OUTER Join "
                'sqla += "CONDUCENTI As conducenti2 WITH (NOLOCK) ON contratti.id_secondo_conducente = conducenti2.ID_CONDUCENTE LEFT OUTER Join "
                'sqla += "contratti_status WITH (NOLOCK) ON contratti.status = contratti_status.id LEFT OUTER Join "
                'sqla += "clienti_tipologia WITH (NOLOCK) ON contratti.id_fonte = clienti_tipologia.id "


                'modifica del 24.04.2021 inserimento ricerca per ragionesociale/ditta
                sqla = "Select contratti.id, contratti_status.descrizione As status, contratti.num_contratto, stazioni1.codice + ' ' + stazioni1.nome_stazione AS staz_uscita, stazioni2.codice + ' ' + stazioni2.nome_stazione AS staz_rientro, " &
                         "stazioni3.codice + ' ' + stazioni3.nome_stazione AS staz_presunto_rientro, CONVERT(char(10), contratti.data_uscita, 103) AS data_uscita, DATEPART(hh, contratti.data_uscita) AS ore_uscita, DATEPART(mi, contratti.data_uscita) " &
                         "AS minuti_uscita, CONVERT(Char(10), contratti.data_rientro, 103) AS data_rientro, DATEPART(hh, contratti.data_rientro) AS ore_rientro, DATEPART(mi, contratti.data_rientro) AS minuti_rientro, CONVERT(Char(10), " &
                         "contratti.data_presunto_rientro, 103) AS data_presunto_rientro, DATEPART(hh, contratti.data_presunto_rientro) AS ore_presunto_rientro, DATEPART(mi, contratti.data_presunto_rientro) AS minuti_presunto_rientro, " &
                         "veicoli.targa AS veicolo, conducenti1.COGNOME As cognome_primo_conducente, conducenti1.nome As nome_primo_conducente, conducenti2.COGNOME As cognome_secondo_conducente, " &
                         "conducenti2.nome AS nome_secondo_conducente, contratti.prenotazione_prepagata As prepagata, clienti_tipologia.descrizione As tipo_cliente, contratti.rif_to, GRUPPI.cod_gruppo, contratti_ditte.rag_soc " &
                         "From stazioni As stazioni3 WITH (NOLOCK) RIGHT OUTER Join " &
              "contratti WITH (NOLOCK) LEFT OUTER Join " &
              "contratti_ditte On contratti.num_contratto = contratti_ditte.num_contratto LEFT OUTER Join " &
              "stazioni As stazioni1 WITH (NOLOCK) ON contratti.id_stazione_uscita = stazioni1.id LEFT OUTER Join " &
              "stazioni As stazioni2 WITH (NOLOCK) ON contratti.id_stazione_rientro = stazioni2.id ON stazioni3.id = contratti.id_stazione_presunto_rientro LEFT OUTER Join " &
              "MODELLI INNER Join " &
              "veicoli WITH (NOLOCK) ON MODELLI.ID_MODELLO = veicoli.id_modello INNER Join " &
              "GRUPPI On MODELLI.ID_Gruppo = GRUPPI.ID_gruppo ON contratti.id_veicolo = veicoli.id LEFT OUTER Join " &
                "CONDUCENTI As conducenti1 WITH (NOLOCK) ON contratti.id_primo_conducente = conducenti1.ID_CONDUCENTE LEFT OUTER Join " &
              "CONDUCENTI As conducenti2 WITH (NOLOCK) ON contratti.id_secondo_conducente = conducenti2.ID_CONDUCENTE LEFT OUTER Join " &
              "contratti_status WITH (NOLOCK) ON contratti.status = contratti_status.id LEFT OUTER Join " &
              "clienti_tipologia WITH (NOLOCK) ON contratti.id_fonte = clienti_tipologia.id "


                sqla += "Where (contratti.attivo = '1') " & condizione_where_contratti()

                query_cerca_cnt.Text = sqla

                lblOrderBY_cnt.Text = order_cnt

                'aggiunto salvo 11.04.2023 - se da pulisci campi non restituisce dati con query senza dati
                If Session("pulisci_campi_contratti") = "OK" Then
                    sqlContratti.SelectCommand = "" 'sqla & " AND contratti.id=-10" 'query_cerca_cnt.Text & lblOrderBY_cnt.Text
                    listContratti.Items.Clear()
                Else
                    sqlContratti.SelectCommand = query_cerca_cnt.Text & lblOrderBY_cnt.Text
                    listContratti.DataBind()
                End If
                'Response.Write(query_cerca_cnt.Text & lblOrderBY_cnt.Text)

                Session("pulisci_campi_contratti") = ""  'aggiunto 11.04.2023 salvo

                divPreventivi.Visible = False
                divPrenotazioni.Visible = False
                divContratti.Visible = True

            End If

            'Aggiunto il 19.12.2020 x verifica string sql
            lbl_message.Text = sqla
            lbl_message.Visible = False

            'Response.Write("Query " & condizione_where_contratti() & "<br><br>")


            'Response.Write("Query " & query_cerca_cnt.Text & lblOrderBY_cnt.Text)


        Catch ex As Exception
            Response.Write("error ricerca: " & ex.Message & "</br>" & error_n & "<br/>" & sqla & "<br/>")
        End Try




    End Sub


    Protected Sub btnAggiornaPrenotazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAggiornaPrenotazione.Click
        richiama_prenotazione(lblNumPreventivo.Text)

        'Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        'Dbc.Open()

        'Dim conducente As String
        'If id_conducente.Text = "" Then
        '    conducente = "NULL"
        'Else
        '    conducente = "'" & id_conducente.Text & "'"
        'End If

        'Dim sqlStr As String = "UPDATE prenotazioni SET id_conducente='" & conducente & "', nome_conducente='" & Replace(txtNomeConducente.Text, "'", "''") & "'," & _
        '"cognome_conducente='" & Replace(txtCognomeConducente.Text, "'", "''") & "', eta_primo_guidatore='" & txtEtaPrimo.Text & "'," & _
        '"mail_conducente='" & Replace(txtMailConducente.Text, "'", "''") & "', NOTE='" & Replace(txtNote.Text, "'", "''") & "'," & _
        '"N_VOLOOUT='" & Replace(txtVoloOut.Text, "'", "''") & "', N_VOLOPR='" & Replace(txtVoloPr.Text, "'", "''") & "'," & _
        '"rif_to='" & Replace(txtRiferimentoTO.Text, "'", "''") & "' WHERE "

        '",,,
        'Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

        'Cmd.Dispose()
        'Cmd = Nothing
        'Dbc.Close()
        'Dbc.Dispose()
        'Dbc = Nothing
    End Sub

    Protected Sub listPrenotazioni_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listPrenotazioni.ItemDataBound
        Dim prepagata As Label = e.Item.FindControl("prepagata")
        Dim lblPrepagato As Label = e.Item.FindControl("lblPrepagato")

        If prepagata.Text = "True" Then
            lblPrepagato.Text = "SI"
        Else
            lblPrepagato.Text = "NO"
        End If
    End Sub

    Protected Sub listContratti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listContratti.ItemDataBound
        Dim prepagata As Label = e.Item.FindControl("prepagata")
        Dim lblPrepagato As Label = e.Item.FindControl("lblPrepagato")

        If prepagata.Text = "True" Then
            lblPrepagato.Text = "SI"
        Else
            lblPrepagato.Text = "NO"
        End If

        Dim ore_uscitaLabel As Label = e.Item.FindControl("ore_uscitaLabel")
        Dim minuti_uscitaLabel As Label = e.Item.FindControl("minuti_uscitaLabel")

        If ore_uscitaLabel.Text.Length = 1 Then
            ore_uscitaLabel.Text = "0" & ore_uscitaLabel.Text
        End If

        If minuti_uscitaLabel.Text.Length = 1 Then
            minuti_uscitaLabel.Text = "0" & minuti_uscitaLabel.Text
        End If

        Dim ore_presunto_rientro As Label = e.Item.FindControl("ore_presunto_rientro")
        Dim minuti_presunto_rientro As Label = e.Item.FindControl("minuti_presunto_rientro")

        If ore_presunto_rientro.Text.Length = 1 Then
            ore_presunto_rientro.Text = "0" & ore_presunto_rientro.Text
        End If

        If minuti_presunto_rientro.Text.Length = 1 Then
            minuti_presunto_rientro.Text = "0" & minuti_presunto_rientro.Text
        End If

        Dim ore_rientroLabel As Label = e.Item.FindControl("ore_rientroLabel")
        Dim minuti_rientroLabel As Label = e.Item.FindControl("minuti_rientroLabel")

        If ore_rientroLabel.Text.Length = 1 Then
            ore_rientroLabel.Text = "0" & ore_rientroLabel.Text
        End If

        If minuti_rientroLabel.Text.Length = 1 Then
            minuti_rientroLabel.Text = "0" & minuti_rientroLabel.Text
        End If
    End Sub

    Protected Sub btnAnnulla7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla7.Click
        annulla_torna_al_primo_step()
        tab_prenotazioni.Visible = False
        tab_preventivo.Visible = False
    End Sub

    Protected Sub btnModificaDitta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaDitta.Click
        If Not anagrafica_ditte.Visible Then
            id_ditta.Text = ""
            txtNomeDitta.Text = ""

            anagrafica_conducenti.Visible = False
            anagrafica_ditte.Visible = True
        Else
            anagrafica_ditte.Visible = False
        End If
    End Sub

    Protected Sub btnAnnulla8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla8.Click
        tab_cerca_tariffe.Visible = False
        tab_ricerca.Visible = True
        'tab_cerca_tariffe.Visible = False
        tab_prenotazioni.Visible = False
        tab_preventivo.Visible = False
        div_dettaglio_gruppi.Visible = False
        table_note.Visible = False

        'reset salvo 03.01.2023
        listVecchioCalcolo.Visible = False
        lbl_msg_tariffe_diverse.Visible = False
        lbl_msg_tariffe_diverse.Text = ""
        btnVediUltimoCalcolo.Text = "Vedi dett. precedente"
        btnVediUltimoCalcolo.Visible = False
        '@ reset


    End Sub

    Protected Sub btnAnnulla9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla9.Click
        tab_cerca_tariffe.Visible = False
        tab_ricerca.Visible = True
        'tab_cerca_tariffe.Visible = False
        tab_prenotazioni.Visible = False
        tab_preventivo.Visible = False
        div_dettaglio_gruppi.Visible = False
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        'Trace.Write("Page_PreRender")
        'Dim htmlheader As String = CType(Me.Page.Controls(0), LiteralControl).Text
        'htmlheader = htmlheader.Replace("<body", "<body onLoad=""javascript:alert('hello world!');"" ")
    End Sub

    Protected Sub btnInviaMailPreventivo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInviaMailPreventivo.Click
        If txtMail.Text <> "" Then
            Try
                inviaMailPreventivo(txtMail.Text)
            Catch ex As Exception

            End Try
        Else
            Libreria.genUserMsgBox(Me, "Specificare l'indirizzo e-mail del cliente.")
        End If
    End Sub

    Protected Sub Page_PreRenderComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRenderComplete
        'Trace.Write("Page_PreRender complete")
        'Dim htmlheader As String = CType(Me.Page.Controls(0), LiteralControl).Text
        'htmlheader = htmlheader.Replace("<body", "<body onLoad=""javascript:alert('hello world 2');"" ")

        'Libreria.genUserMsgBox(Page, "complete")






    End Sub

    Protected Sub inviaMailPrenotazione(ByVal mail_conducente As String)
        Dim mail As New MailMessage()

        mail.To.Add(mail_conducente)

        'Imposta l'oggetto della Mail
        Dim oggmail As String = "Conferma Prenotazione / Booking Confirmation N. " & lblNumPreventivo.Text
        mail.Subject = "Conferma Prenotazione / Booking Confirmation N. " & lblNumPreventivo.Text

        'Imposta la priorità  della Mail
        mail.Priority = MailPriority.High

        mail.IsBodyHtml = True

        Dim corpoMessaggio As String
        corpoMessaggio = "Gentile Cliente / Dear Client," & "<br /><br />" & "Sicily Rent Car comunica che la sua prenotazione è avvenuta con successo / Sicily Rent Car announced that your reservation was successful:<br><br>" &
        "<b>N. Prenotazione / Reservation: </b>" & lblNumPreventivo.Text & "<br>" &
        "<b>Conducente / Driver: </b>" & txtCognomeConducente.Text & " " & txtNomeConducente.Text & "<br>"


        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(cod_gruppo,'') + ' ' + ISNULL(descrizione,'') FROM gruppi WITH(NOLOCK) WHERE id_gruppo='" & id_gruppo_auto_scelto.Text & "'", Dbc)


        Dim nomeStazioneNoCode As String = funzioni_comuni.GetNomeStazioneNoCode(dropStazionePickUp.SelectedItem.Text)

        corpoMessaggio = corpoMessaggio & "<b>Veicolo / Vehicle: </b> " & "Gruppo " & Cmd.ExecuteScalar & " o similare<br><br>" &
        "<b>Ritiro / Pick-Up </b><br>" &
        nomeStazioneNoCode & "<br>" &
        txtDaData.Text & " ore " & txtoraPartenza.Text & "<br>"



        Cmd = New Data.SqlClient.SqlCommand("SELECT indirizzo, telefono, cellulare, email, testo_mail FROM stazioni WITH(NOLOCK) WHERE id='" & dropStazionePickUp.SelectedValue & "'", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()
        Rs.Read()

        Dim email_stazione As String = Rs("email") & ""

        Dim testo_mail As String = Rs("testo_mail") & ""


        'aggiunto x email Fco del 08.01.2021 13:36
        'Dim id_stazione_operatore As String = Request.Cookies("SicilyRentCar")("stazione")  'se sede invia come booking@sicilyrentcar
        'If id_stazione_operatore = "1" Then
        '    email_stazione = "booking@sicilyrentcar.it"
        'End If

        Try
            mail.From = New MailAddress(email_stazione)
            mail.To.Add(email_stazione)
        Catch ex As Exception
            mail.From = New MailAddress("info@sicilyrentcar.it")
        End Try

        corpoMessaggio = corpoMessaggio & Rs("indirizzo") & ""

        If (Rs("telefono") & "") <> "" Then
            corpoMessaggio = corpoMessaggio & " Telefono " & Rs("telefono")
        End If

        'If (Rs("cellulare") & "") <> "" Then
        '    corpoMessaggio = corpoMessaggio & " (Cellulare " & Rs("cellulare") & ")"
        'End If
        nomeStazioneNoCode = funzioni_comuni.GetNomeStazioneNoCode(dropStazioneDropOff.SelectedItem.Text)
        corpoMessaggio = corpoMessaggio & "<br><br><b>Riconsegna / Drop Off</b><br>" &
        nomeStazioneNoCode & "<br>" &
        txtAData.Text & " ore " & txtOraRientro.Text & "<br>"

        Dbc.Close()
        Dbc.Open()

        Cmd = New Data.SqlClient.SqlCommand("SELECT id, indirizzo, telefono, cellulare, email FROM stazioni WITH(NOLOCK) WHERE id='" & dropStazioneDropOff.SelectedValue & "'", Dbc)
        Rs = Cmd.ExecuteReader()
        Rs.Read()

        Dim idstazione As String = Rs!id        'salvo 10.10.2022

        corpoMessaggio = corpoMessaggio & Rs("indirizzo") & ""

        If (Rs("telefono") & "") <> "" Then
            corpoMessaggio = corpoMessaggio & " Telefono " & Rs("telefono")
        End If

        'If (Rs("cellulare") & "") <> "" Then
        '    corpoMessaggio = corpoMessaggio & " (Cellulare " & Rs("cellulare") & ")"
        'End If


        corpoMessaggio += "<br><br> <b>Giorni / Days:</b> " & txtNumeroGiorni.Text


        'Insermento delle Condizioni Incluse 02.08.2021
        Dim Condizioni_incluse As String = ""
        Dim lblIncluso As Label
        Dim lblObbligatorio As Label
        Dim nome_costo_incluso As Label
        Dim check_attuale_incluso As CheckBox

        corpoMessaggio += "<br><br> <b>Condizioni incluse / Conditions included</b>"

        For i = 0 To listPreventiviCosti.Items.Count - 1
            nome_costo_incluso = listPreventiviCosti.Items(i).FindControl("nome_costo")
            lblIncluso = listPreventiviCosti.Items(i).FindControl("lblIncluso")
            lblObbligatorio = listPreventiviCosti.Items(i).FindControl("lblObbligatorio")
            check_attuale_incluso = listPreventiviCosti.Items(i).FindControl("chkScegli")
            If lblIncluso.Visible = True Then
                Condizioni_incluse += "<br>" & nome_costo_incluso.Text
            End If
            If lblObbligatorio.Visible = True Then
                Condizioni_incluse += "<br>" & nome_costo_incluso.Text
            End If
        Next

        If Condizioni_incluse = "" Then
            corpoMessaggio += "<br>-" 'al  posto di Nessuno
        Else
            corpoMessaggio += Condizioni_incluse
        End If

        ' Exit Sub 'TEST  RIMUOVERE
        ' fine inserimento Condizioni Incluse 02.08.2021



        corpoMessaggio += "<br><br><b>Supplementi / Extra:</b>"


        Dim supplementi As String = ""
        Dim check_attuale As CheckBox
        Dim nome_costo As Label
        Dim check_old_scegli As CheckBox
        Dim importo_totale As Label
        Dim franchigie As String = ""
        Dim lblInformativa As Label
        Dim costo_franchigie As Label
        Dim tipologia_franchigia As Label
        Dim sottotipologia_franchigia As Label

        Dim id_elemento As Label   '30.12.2021

        'Inserite per la visualizzazione del deposito cauzionale 26.01.2022
        Dim costo_scontato As Label                 '26.01.2022
        Dim deposito_cauzionale As String = ""      '26.01.2022
        Dim cf As String = ""                       '26.01.2022





        For i = 0 To listPreventiviCosti.Items.Count - 1
            check_attuale = listPreventiviCosti.Items(i).FindControl("chkScegli")
            nome_costo = listPreventiviCosti.Items(i).FindControl("nome_costo")
            check_old_scegli = listPreventiviCosti.Items(i).FindControl("chkOldScegli")
            lblInformativa = listPreventiviCosti.Items(i).FindControl("lblInformativa")
            tipologia_franchigia = listPreventiviCosti.Items(i).FindControl("tipologia_franchigia")
            sottotipologia_franchigia = listPreventiviCosti.Items(i).FindControl("sottotipologia_franchigia")
            'SE check_attuale E' VISIBILE SIGNIFICA CHE L'ACCESSORIO E' A SCELTA, MENTRE CONTROLLO check_old_scegli PER ESSERE SICURO
            'CHE L'ACCESSORIO E' STATO AGGIUNTO AL PREZZO E NON SIA STATO SEMPLICEMENTE SELEZIONATO SENZA SALVARE

            id_elemento = listPreventiviCosti.Items(i).FindControl("id_elemento")     '30.12.2021

            costo_scontato = listPreventiviCosti.Items(i).FindControl("costo_scontato")   'Inserite per la visualizzazione del deposito cauzionale 26.01.2022



            If check_attuale.Visible And check_old_scegli.Checked Then
                supplementi = supplementi & "<br>" & nome_costo.Text
            End If

            'NE APPROFITTO PER RECUPERARE IL COSTO DEL TOTALE CHE MI SERVIRA' DOPO
            If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
                importo_totale = listPreventiviCosti.Items(i).FindControl("costo_scontato")
            End If

            'CONSERVO LE INFORMATIVE
            If lblInformativa.Visible Then
                Try
                    If id_elemento.Text <> "248" And id_elemento.Text <> "283" Then   '30.12.2021 e 26.01.2022
                        costo_franchigie = listPreventiviCosti.Items(i).FindControl("costo_scontato")
                        cf = costo_franchigie.Text
                        cf = cf.Replace("€", "")
                        cf = "€ " & cf
                        franchigie = franchigie & "<b>" & nome_costo.Text & " / " & get_nome_franchigia_inglese(listPreventiviCosti.Items(i).FindControl("id_elemento")) & "</b> " & cf & "<br />"

                    End If

                Catch ex As Exception

                End Try

            End If

            If tipologia_franchigia.Text = "ASSICURAZIONE" Then
                If check_attuale.Visible And check_old_scegli.Checked Then
                    If sottotipologia_franchigia.Text = "TOTALE" Then
                        franchigie = franchigie & "<b>Franchigia danni / Damage excess:</b>€ 0,00 <br />" &
                        "<b>Franchigia furto e incendio / Theft and fire excess reduced:</b>€ 0,00<br />"
                    End If
                End If
            End If



            'se PPLUS 30.12.2021 (da verificare e casomai togliere)
            If id_elemento.Text = "248" And check_attuale.Checked = True Then
                franchigie = franchigie & "<b>Franchigia danni / Damage excess:</b> € 0,00<br />" &
                       "<b>Franchigia furto e incendio / Theft and fire excess:</b> € 0,00<br />"
            End If


            'se deposito cauzionale visualizza riga 26.01.2022
            If id_elemento.Text = "283" And lblInformativa.Visible = True Then
                cf = costo_scontato.Text
                cf = cf.Replace("€", "")
                cf = "€ " & cf
                deposito_cauzionale = "<br/><b>Deposito cauzionale / Security deposit: </b>" & cf
            End If



        Next


        If supplementi = "" Then
            corpoMessaggio = corpoMessaggio & "<br>-" 'modifica 21.04.2021 al  posto di Nessuno
        Else
            corpoMessaggio = corpoMessaggio & supplementi
        End If

        If dropTariffeGeneriche.SelectedValue <> "0" Then
            corpoMessaggio = corpoMessaggio & "<br><br> <b>Tariffa applicata / Rate applied:</b> " & dropTariffeGeneriche.SelectedItem.Text
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            corpoMessaggio = corpoMessaggio & "<br><br> <b>Tariffa applicata / Rate applied:</b> " & dropTariffeParticolari.SelectedItem.Text
        End If



        'modificato 19.01.2021
        Try
            If InStr(1, Request.ServerVariables("URL"), "preventivi.aspx", 1) = 0 Then
                corpoMessaggio = corpoMessaggio & "<br><br><b>Totale noleggio / Total:</b> € " & importo_totale.Text
            Else
                Dim imp_tot As String = "0"
                'da verificare se broker = 0 altrimenti importo del noleggio 19.01.2021

                If Not IsDBNull(importo_totale.Text) Then       'modificato salvo 13.12.2022
                    If importo_totale.Text <> "" Then
                        imp_tot = importo_totale.Text
                    Else
                        imp_tot = 0
                    End If
                Else
                    imp_tot = 0
                End If

                corpoMessaggio = corpoMessaggio & "<br><br><b>Totale noleggio / Total:</b> € " & imp_tot

            End If
        Catch ex As Exception

        End Try



        If franchigie <> "" Then
            corpoMessaggio += "<br><br>" & franchigie
        End If

        'Deposito Cauzionale 18.01.22
        If deposito_cauzionale <> "" Then
            corpoMessaggio += deposito_cauzionale
        End If


        corpoMessaggio = corpoMessaggio & "<br><br><b><a href='https://www.sicilyrentcar.it/condizioni-noleggio-auto/'>Termini e condizioni</a>&nbsp;/&nbsp;<a href='https://www.sicilyrentcar.it/en/rental-conditions/'>Terms and conditions</a></b>"

        'inserire qui FAQ email Francesco - salvo 25.11.2022            
        'corpoMessaggio += "<br/><br/><b>Hai qualche dubbio? Leggi le nostre <a href='https://www.sicilyrentcar.it/faq-noleggio-auto/'>FAQ</a></b>&nbsp;&nbsp;/&nbsp;&nbsp;<b>Do you have any doubts? Read our <a href='https://www.sicilyrentcar.it/en/car-rental-faq/'>FAQ</a></b>"
        'Modificata 21-07-2023 Tony
        corpoMessaggio += "<br/><br/>Hai qualche dubbio? Leggi le nostre <a href='https://www.sicilyrentcar.it/faq-noleggio-auto/'><b>FAQ</a></b>&nbsp;&nbsp;/&nbsp;&nbsp;Do you have any doubts? Read our <a href='https://www.sicilyrentcar.it/en/car-rental-faq/'><b>FAQ</a></b>"
        '@ end nuovo inserimento 

        'corpoMessaggio = corpoMessaggio & "<br><br><b>Per effettuare il pagamento clicca qui > <a href='https://www.sicilyrentcar.it/servizi-noleggio-auto/'>Modulo autorizzazione utilizzo carta</a>&nbsp;/&nbsp; To make the payment click here > <a href='https://www.sicilyrentcar.it/en/car-rental-services/'>Card use authorization form</a></b>"
        'Modificata 21-07-2023 Tony
        corpoMessaggio = corpoMessaggio & "<br><br><b>Per effettuare il pagamento anticipato clicca qui > <a href='https://www.sicilyrentcar.it/servizi-noleggio-auto/'>Modulo autorizzazione utilizzo carta</a>&nbsp;/&nbsp; To pay in advance click here > <a href='https://www.sicilyrentcar.it/en/car-rental-services/'>Card use authorization form</a></b>"

        'nuovo inserimento da email di Francesco del 26.05.2021
        corpoMessaggio = corpoMessaggio & "<br><br>Se la tariffa applicata prevede il pagamento anticipato, entro 72 ore dalla conferma della prenotazione, sarà necessario procedere con il saldo totale del servizio. In caso di mancato pagamento entro i suddetti termini la tariffa potrebbe subire delle variazioni."
        'corpoMessaggio = corpoMessaggio & "<br><br>If the rate applied requires prepayment of the rental, within 72 hours of confirmation of the booking, you must pay the full balance of the service. Failure to pay may result in a change in the rate."  ''
        'Modificata 21-07-2023 Tony
        corpoMessaggio = corpoMessaggio & "<br><br>If the rate applied requires payment in advance, within 72 hours of confirmation of the booking, you must pay the full balance of the service. Failure to pay may result in a change in the rate."  ''

        'Inserita  21-07-2023 Tony
        corpoMessaggio = corpoMessaggio & "<br><br>Se la tariffa applicata prevede il pagamento del noleggio all'arrivo, entro 3 giorni dalla ricezione della presente mail e comunque 3 giorni prima del ritiro della vettura, è necessario fornire, tramite il seguente <a href='https://www.sicilyrentcar.it/servizi-autonoleggio/'>link</a>, una carta di credito, debito o carta prepagata a garanzia della prenotazione, <b>nessun costo verrà addebitato anticipatamente</b>. In caso di mancata ricezione del modulo contenente i dati della carta nonché in assenza di comunicazione da parte del Cliente, SRC Rent Car potrebbe procedere alla cancellazione della prenotazione senza alcun preavviso."  ''

        'Inserita 21-07-2023 Tony
        corpoMessaggio = corpoMessaggio & "<br><br>If the rate applied requires payment at your arrival, within 3 days of receipt of this email and in any case 3 days prior to pick-up of the car, you must provide, via the following <a href='https://www.sicilyrentcar.it/servizi-autonoleggio/'>link</a>, a credit, debit or prepaid card as a guarantee for your booking, <b>no fee will be charged in advance</b>. If the form with the guarantee card is not received, SRC Rent Car may proceed to the cancellation of the reservation without prior notice."  ''


        corpoMessaggio = corpoMessaggio & "<br><br><b>Ricorda</b>: se sei un nuovo Cliente Sicily Rent Car puoi effettuare la registrazione dei tuoi dati direttamente online collegandoti alla pagina <a href='http://www.sicilyrentcar.it/web-check-in-noleggio-auto/'>Web check-in</a>, un’esclusiva che ti consentirà di risparmiare tempo nella procedura di consegna dell'auto."
        corpoMessaggio = corpoMessaggio & "<br><br><b>Remember</b>: if you are a new Customer of Sicily Rent Car you can now register your data directly online connecting to the page <a href='http://www.sicilyrentcar.it/en/web-check-in-car-rental/'>Web check-in</a>, an exclusive that will allow you to save time on delivery of the car."

        ''''
        'Il testo tra A e B e stato sostituito da quello tra C e D - salvo 10.10.2022
        'A
        'corpoMessaggio = corpoMessaggio & testo_mail & ""

        'corpoMessaggio = corpoMessaggio & "<br><br>Cordiali saluti / Best regards." &
        '"<br><br><br>Sicily Rent Car - Servizio Prenotazioni<br> " &
        '"<br>e-mail: " & email_stazione

        'corpoMessaggio = corpoMessaggio & "<br/><br/><a href='https://www.sicilyrentcar.it/'><img src= 'http://ares.sicilyrentcar.it/img/SRC_logo.jpg' ></a>"
        'B

        'C
        'TESTO NUOVO 29.06.2022 da campo testo_mail salvo
        corpoMessaggio += funzioni_comuni_new.getTestoMailImportante(idstazione)    'recupera il testo che va prima della firma

        'Tony 27-07-2022
        'Posto qui testo con firma
        'corpoMessaggio = corpoMessaggio & testo_mail & ""  'Salvo 10.10.2022 modificato senza testo_email perchè già inserito nella riga 6831 mod. del 29.06.2022
        corpoMessaggio = corpoMessaggio & ""

        'corpoMessaggio = corpoMessaggio & "<br><br>Cordiali saluti / Best regards" &
        '"<br><br><br>SRC Rent Car - Centro Prenotazioni<br> " &
        '"<br>e-mail: " & email_stazione

        corpoMessaggio += "<br/><br/><a href='https://www.sicilyrentcar.it/'>"
        corpoMessaggio += "<img src='http://ares.sicilyrentcar.it/img/SRC_logo.jpg'></a>"
        'D

        ''''

        mail.Body = Replace(corpoMessaggio, "!", "")


        Dim attachment As New System.Net.Mail.Attachment(Server.MapPath("\prenotazioni\" & "prenotazione" & "_" & lblNumPreventivo.Text & ".pdf"))
        Dim fileallegato As String = Server.MapPath("\prenotazioni\" & "prenotazione" & "_" & lblNumPreventivo.Text & ".pdf")
        mail.Attachments.Add(attachment)

        'Imposta il server smtp di posta da utilizzare        
        Dim client As New Net.Mail.SmtpClient("", 587)
        'client.Credentials = New System.Net.NetworkCredential("", "")
        'client.EnableSsl = True

        'client.Host = ""

        'Invia l'e-mail
        Dim sm As New sendmailcls
        'Try                'rimosso da email di Fco del 08.01.2021 15:52
        '    sm.sendmail(email_stazione, "Sicily Rent Car", email_stazione, oggmail, corpoMessaggio, True, fileallegato)
        '    'client.Send(mail)
        '    'Libreria.genUserMsgBox(Me, "E-Mail inviata correttamente.")
        'Catch ex As Exception
        '    Libreria.genUserMsgBox(Me, "Si è verificato un errore nell'invio della mail.")
        'End Try

        'invia email al cliente email del 12.01.2021
        Dim mittente As String = ""
        'se operatore sede il mittente è booking 
        If Request.Cookies("SicilyRentCar")("stazione") = "1" Then
            mittente = "booking@sicilyrentcar.it"
        Else
            mittente = email_stazione
        End If

        'il destinatario è bookink significa che non è stato inserito indirizzo email in prenotazione salta perchè sotto 
        'invia comunque a booking

        'aggiunta intestazione html 18.01.22
        'Dim prebody As String = "<!DOCTYPE html><html xmlns = ""http://www.w3.org/1999/xhtml""<head>meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""/></head><body>"
        'Dim endbody As String = "</body></html>"
        'corpoMessaggio = prebody & corpoMessaggio & endbody
        '#

        If mail_conducente <> "booking@sicilyrentcar.it" Then
            Try
                sm.sendmail(mittente, "Sicily Rent Car", mail_conducente, oggmail, corpoMessaggio, True, fileallegato)
                Libreria.genUserMsgBox(Me, "E-Mail inviata correttamente.")
            Catch ex As Exception
                Libreria.genUserMsgBox(Me, "Si è verificato un errore nell'invio della mail al cliente.")
            End Try
        End If

        'invia email al booking     aggiunto 05.01.2021  e email del 12.01.2021
        Try
            sm.sendmail(mittente, "Sicily Rent Car", "booking@sicilyrentcar.it", oggmail, corpoMessaggio, True, fileallegato)
            'Libreria.genUserMsgBox(Me, "E-Mail inviata correttamente.")
        Catch ex As Exception
            Libreria.genUserMsgBox(Me, "Si è verificato un errore nell'invio della mail a booking@sicilyrentcar.it")
        End Try

        Try 'a stazione aggiunto wapp Fco 11.01.2021 e email del 12.01.2021

            sm.sendmail(mittente, "Sicily Rent Car", email_stazione, oggmail, corpoMessaggio, True, fileallegato)

            'se stazione Cinisi invia anche a PAAPT 10.02.2022
            If dropStazionePickUp.SelectedItem.Text.IndexOf("cinisi") > -1 Then
                sm.sendmail(email_stazione, "Sicily Rent Car", "palermoapt@sicilyrentcar.it", oggmail, corpoMessaggio, True, "")
            End If

            Libreria.genUserMsgBox(Me, "E-Mail inviata correttamente.")
        Catch ex As Exception
            Libreria.genUserMsgBox(Me, "Si è verificato un errore nell'invio della mail alla stazione.")
        End Try

        attachment.Dispose()

        Try
            File.Delete(Server.MapPath("\prenotazioni\" & "prenotazione" & "_" & lblNumPreventivo.Text & ".pdf"))
        Catch ex As Exception

        End Try

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Function get_nome_franchigia_inglese(ByVal id_elemento As Label) As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String = "SELECT descrizione_en FROM condizioni_elementi WITH(NOLOCK) WHERE id=" & id_elemento.Text
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            get_nome_franchigia_inglese = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error (get_nome_franchigia_inglese) : " & ex.Message & "<br/>")
        End Try


    End Function

    Protected Sub inviaMailPreventivo(ByVal mail_conducente As String)

        If mail_conducente = "" Then    'campo email vuoto non invia
            Exit Sub
        End If


        Dim mail As New MailMessage()
        'Tony 18-04-2023
        Dim NumTelefono As String = ""

        'Dichiato il mittente
        mail.From = New MailAddress("noreply@sicilyrentcar.it")

        mail.To.Add(mail_conducente)

        'mail.To.Add(mail_conducente)
        'mail.Bcc.Add("booking@sicilyrentcar.it")

        'Imposta l'oggetto della Mail
        Dim oggmail As String = "Richiesta Preventivo / Quote N. " & lblNumPreventivo.Text
        mail.Subject = "Richiesta Preventivo - N. " & lblNumPreventivo.Text

        'Imposta la priorità  della Mail
        mail.Priority = MailPriority.High

        mail.IsBodyHtml = True

        Dim corpoMessaggio As String
        corpoMessaggio = "Gentile Cliente, Dear Client<br><br>Le inviamo copia del preventivo, si ricorda che le tariffe sono soggette a variazioni senza preavviso / We send you a quote copy, reminds rates are subject to change without notice.<br><br>" &
        "<b>N. Preventivo / Quote: </b>" & lblNumPreventivo.Text & "<br>"

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(cod_gruppo,'') + ' - ' + ISNULL(descrizione,'') FROM gruppi WITH(NOLOCK) WHERE id_gruppo='" & id_gruppo_auto_scelto.Text & "'", Dbc)

        Dim nomeStazioneNoCode As String = funzioni_comuni.GetNomeStazioneNoCode(dropStazionePickUp.SelectedItem.Text)

        corpoMessaggio = corpoMessaggio & "<b>Categoria auto / Car group: </b> " & Cmd.ExecuteScalar & " o similare / or similar<br><br>" &
        "<b>Ritiro / Pick-Up</b><br>" &
        nomeStazioneNoCode & "<br>" &
        txtDaData.Text & " ore " & txtoraPartenza.Text & "<br>"

        Cmd = New Data.SqlClient.SqlCommand("SELECT indirizzo, telefono, cellulare, email FROM stazioni WITH(NOLOCK) WHERE id='" & dropStazionePickUp.SelectedValue & "'", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()
        Rs.Read()

        corpoMessaggio = corpoMessaggio & Rs("indirizzo") & ""

        'Tony 18-04-2023
        'If (Rs("telefono") & "") <> "" Then
        '    corpoMessaggio = corpoMessaggio & " Telefono " & Rs("telefono")
        'End If
        NumTelefono = Rs("telefono")
        'FINE Tony

        Dim email_stazione As String = Rs("email") & ""

        'If (Rs("cellulare") & "") <> "" Then
        '    corpoMessaggio = corpoMessaggio & " (Cellulare " & Rs("cellulare") & ")"
        'End If
        nomeStazioneNoCode = funzioni_comuni.GetNomeStazioneNoCode(dropStazioneDropOff.SelectedItem.Text)
        corpoMessaggio = corpoMessaggio & "<br><br><b>Riconsegna / Drop Off</b><br>" &
        nomeStazioneNoCode & "<br>" &
        txtAData.Text & " ore " & txtOraRientro.Text & "<br>"

        Dbc.Close()
        Dbc.Open()

        Cmd = New Data.SqlClient.SqlCommand("SELECT indirizzo, telefono, cellulare FROM stazioni WITH(NOLOCK) WHERE id='" & dropStazioneDropOff.SelectedValue & "'", Dbc)
        Rs = Cmd.ExecuteReader()
        Rs.Read()

        corpoMessaggio = corpoMessaggio & Rs("indirizzo") & ""

        'Tony 18-04-2023
        'If (Rs("telefono") & "") <> "" Then
        '    corpoMessaggio = corpoMessaggio & " Telefono " & Rs("telefono")
        'End If
        'FINE Tony

        'If (Rs("cellulare") & "") <> "" Then
        '    corpoMessaggio = corpoMessaggio & " (Cellulare " & Rs("cellulare") & ")"
        'End If


        corpoMessaggio += "<br><br> <b>Giorni / Days:</b> " & txtNumeroGiorni.Text


        'Insermento delle Condizioni Incluse 02.08.2021
        Dim Condizioni_incluse As String = ""
        Dim lblIncluso As Label
        Dim lblObbligatorio As Label
        Dim nome_costo_incluso As Label
        Dim check_attuale_incluso As CheckBox

        Dim id_elemento As Label   '30.12.2021


        corpoMessaggio += "<br><br> <b>Condizioni incluse / Conditions included</b>"

        For i = 0 To listPreventiviCosti.Items.Count - 1
            nome_costo_incluso = listPreventiviCosti.Items(i).FindControl("nome_costo")
            lblIncluso = listPreventiviCosti.Items(i).FindControl("lblIncluso")
            lblObbligatorio = listPreventiviCosti.Items(i).FindControl("lblObbligatorio")
            check_attuale_incluso = listPreventiviCosti.Items(i).FindControl("chkScegli")
            If lblIncluso.Visible = True Then
                Condizioni_incluse += "<br>" & nome_costo_incluso.Text
            End If
            If lblObbligatorio.Visible = True Then
                Condizioni_incluse += "<br>" & nome_costo_incluso.Text
            End If

            id_elemento = listPreventiviCosti.Items(i).FindControl("id_elemento")     '30.12.2021

        Next

        If Condizioni_incluse = "" Then
            corpoMessaggio += "<br>-" 'al  posto di Nessuno
        Else
            corpoMessaggio += Condizioni_incluse
        End If

        'Exit Sub 'test
        ' fine inserimento Condizioni Incluse


        corpoMessaggio += "<br><br><b>Supplementi / Extras:</b>"

        Dim supplementi As String = ""
        Dim check_attuale As CheckBox
        Dim nome_costo As Label
        Dim check_old_scegli As CheckBox
        Dim importo_totale As Label
        Dim franchigie As String = ""
        Dim lblInformativa As Label
        Dim costo_franchigie As Label
        Dim tipologia_franchigia As Label
        Dim sottotipologia_franchigia As Label

        'Inserite per la visualizzazione del deposito cauzionale 26.01.2022
        Dim costo_scontato As Label                 '26.01.2022
        Dim deposito_cauzionale As String = ""      '26.01.2022
        Dim cf As String = ""                       '26.01.2022


        For i = 0 To listPreventiviCosti.Items.Count - 1
            check_attuale = listPreventiviCosti.Items(i).FindControl("chkScegli")
            nome_costo = listPreventiviCosti.Items(i).FindControl("nome_costo")
            check_old_scegli = listPreventiviCosti.Items(i).FindControl("chkOldScegli")
            lblInformativa = listPreventiviCosti.Items(i).FindControl("lblInformativa")
            tipologia_franchigia = listPreventiviCosti.Items(i).FindControl("tipologia_franchigia")
            sottotipologia_franchigia = listPreventiviCosti.Items(i).FindControl("sottotipologia_franchigia")

            id_elemento = listPreventiviCosti.Items(i).FindControl("id_elemento")     '30.12.2021

            costo_scontato = listPreventiviCosti.Items(i).FindControl("costo_scontato")   'Inserite per la visualizzazione del deposito cauzionale 26.01.2022

            'SE check_attuale E' VISIBILE SIGNIFICA CHE L'ACCESSORIO E' A SCELTA, MENTRE CONTROLLO check_old_scegli PER ESSERE SICURO
            'CHE L'ACCESSORIO E' STATO AGGIUNTO AL PREZZO E NON SIA STATO SEMPLICEMENTE SELEZIONATO SENZA SALVARE

            If check_attuale.Visible And check_old_scegli.Checked Then
                supplementi = supplementi & "<br>" & nome_costo.Text
            End If

            'NE APPROFITTO PER RECUPERARE IL COSTO DEL TOTALE CHE MI SERVIRA' DOPO
            If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
                importo_totale = listPreventiviCosti.Items(i).FindControl("costo_scontato")
            End If

            'CONSERVO LE INFORMATIVE



            If lblInformativa.Visible Then
                Try

                    'se elemento diverso da ProtezionePlus 248 visualizza le altre Franchigie 30.12.2021
                    If id_elemento.Text <> "248" And id_elemento.Text <> "283" Then   '30.12.2021 e 26.01.2021
                        costo_franchigie = listPreventiviCosti.Items(i).FindControl("costo_scontato")
                        cf = costo_franchigie.Text
                        cf = cf.Replace("€", "")
                        cf = "€ " & cf
                        franchigie += "<b>" & nome_costo.Text & " / " & get_nome_franchigia_inglese(listPreventiviCosti.Items(i).FindControl("id_elemento")) & ":</b> " & cf & "<br />"
                    End If

                Catch ex As Exception

                End Try

            End If

            If tipologia_franchigia.Text = "ASSICURAZIONE" Then
                If check_attuale.Visible And check_old_scegli.Checked Then
                    If sottotipologia_franchigia.Text = "TOTALE" Then
                        franchigie = franchigie & "<b>Franchigia danni / Damage excess:</b> € 0,00 <br />" &
                        "<b>Franchigia furto e incendio / Theft and fire excess reduced:</b> € 0,00 <br />"
                    End If
                End If
            End If

            'se PPLUS 30.12.2021 (da verificare e casomai togliere)
            If id_elemento.Text = "248" And check_attuale.Checked = True Then
                franchigie = franchigie & "<b>Franchigia danni / Damage excess:</b> € 0,00<br />" &
                       "<b>Franchigia furto e incendio / Theft and fire excess:</b> € 0,00<br />"
            End If


            'se deposito cauzionale visualizza riga 26.01.2022
            If id_elemento.Text = "283" And lblInformativa.Visible = True Then
                cf = costo_scontato.Text
                cf = cf.Replace("€", "")
                cf = "€ " & cf
                deposito_cauzionale = "<br/><b>Deposito cauzionale / Security deposit: </b>" & cf
            End If

        Next 'cicla x tutti gli elementi



        If supplementi = "" Then
            corpoMessaggio = corpoMessaggio & "<br>Nessuno"
        Else
            corpoMessaggio = corpoMessaggio & supplementi
        End If



        If dropTariffeGeneriche.SelectedValue <> "0" Then
            corpoMessaggio = corpoMessaggio & "<br><br> <b>Tariffa applicata / Rate applied:</b> " & dropTariffeGeneriche.SelectedItem.Text
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            corpoMessaggio = corpoMessaggio & "<br><br> <b>Tariffa applicata / Rate applied:</b> " & dropTariffeParticolari.SelectedItem.Text
        End If




        corpoMessaggio = corpoMessaggio & "<br><br><b>Totale noleggio / Total:</b> € " & importo_totale.Text

        If franchigie <> "" Then
            corpoMessaggio = corpoMessaggio & "<br><br>" & franchigie
        End If

        'Deposito Cauzionale 18.01.22
        If deposito_cauzionale <> "" Then
            corpoMessaggio += deposito_cauzionale
        End If


        'corpoMessaggio = corpoMessaggio & "<br><br>Cordiali saluti / Best Regards" &
        '"<br><br><br>Sicily Rent Car - Centro Prenotazioni<br><br>e-mail: " & email_stazione & "<br>Internet: https://www.sicilyrentcar.it"

        'Tony 18-04-2023
        corpoMessaggio = corpoMessaggio & "<br><br>Cordiali saluti / Best regards"
        corpoMessaggio = corpoMessaggio & "<br/><br/><a href='https://www.sicilyrentcar.it/'>"
        corpoMessaggio += "<img src='http://ares.sicilyrentcar.it/img/logo_mail.png'></a>"
        corpoMessaggio = corpoMessaggio & "<br>SRC Rent Car - Booking<br> " &
        "Telefono: +39 091203374"


        'FINE Tony

        'corpoMessaggio = corpoMessaggio & "<br/><br/><a href='https://www.sicilyrentcar.it/'>"
        'corpoMessaggio += "<img src='http://ares.sicilyrentcar.it/img/SRC_logo.jpg'></a>"

        'Exit Sub 'TEST


        mail.Body = Replace(corpoMessaggio, "!", "")

        'Imposta il server smtp di posta da utilizzare        
        Dim client As New Net.Mail.SmtpClient("", 25)
        'client.Credentials = New System.Net.NetworkCredential("", "")
        'client.EnableSsl = True

        'client.Host = ""

        'Invia l'e-mail
        Dim sm As New sendmailcls

        'Try 'eliminata stazione da email Fco del 08. e 09.01 2021
        '    sm.sendmail(email_stazione, "Sicily Rent Car", email_stazione, oggmail, corpoMessaggio, True, "")
        '    'client.Send(mail)
        '    'Libreria.genUserMsgBox(Me, "E-Mail inviata correttamente.")
        'Catch ex As Exception
        '    Libreria.genUserMsgBox(Me, "Si è verificato un errore nell'invio della mail alla stazione.")
        'End Try

        'Preventivo inserito da ARES o preme il pulsante "invia email" invia email solo al cliente come da email di Fco del 08.01 e 09.01.2021
        'verificato 12.01.2021
        Try
            'client.Send(mail)

            ' 9.01.2021 se operatore loggato come sede che invia la stazione diventa booking@sicilyrentcar.it
            If Request.Cookies("SicilyRentCar")("stazione") = "1" Then
                email_stazione = "booking@sicilyrentcar.it"
            End If

            'mail_conducente = "dimatteo@xinformatica.it"    'SOLO TEST
            sm.sendmail(email_stazione, "SRC Rent Car", mail_conducente, oggmail, corpoMessaggio, True, "")


            'se stazione Cinisi invia anche a PAAPT 10.02.2022
            If dropStazionePickUp.SelectedItem.Text.IndexOf("cinisi") > -1 Then
                sm.sendmail(email_stazione, "SRC Rent Car", "palermoapt@sicilyrentcar.it", oggmail, corpoMessaggio, True, "")
            End If


            Libreria.genUserMsgBox(Me, "E-Mail inviata correttamente.")
        Catch ex As Exception
            Libreria.genUserMsgBox(Me, "Si è verificato un errore nell'invio della mail al cliente.")
        End Try


        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub setta_pannello_ricerca()

        Try
            If dropTipoDocumento.SelectedValue = "2" Then
                'PREVENTIVO
                lblStato.Visible = False
                dropStatoPrenotazione.Visible = False
                dropStatoContratto.Visible = False
                txtCercaNumStaz.Visible = False
                txtCercaNumInterno.Visible = True
                lblTipoNumero.Text = "Num. Preventivo"
                lblRifTo.Visible = False
                txtCercaRiferimento.Visible = False
                lblDaRibaltamento.Visible = False
                cercaPrenotazioniRibaltamento.Visible = False
                lblPrepagate.Visible = False
                dropPrenotazioniPrepagate.Visible = False
                lblPresRientro.Visible = False
                cercaStazionePresuntoRientro.Visible = False
                lblPresRDa.Visible = False
                lblPresRA.Visible = False
                txtCercaPresRDa.Visible = False
                txtCercaPresRA.Visible = False
                lblCercaGruppoContratto.Visible = False
                dropCercaGruppoContratto.Visible = False
                txtCercaTargaContratto.Visible = False
                dropCercaGruppoPrenotazione.Visible = False
                txtCercaTargaPrenotazione.Visible = False
                lblCercaGruppoTargaPrenot.Visible = False

                txtCercaNunFattura.Visible = False
                dropCercaAnnoFattura.Visible = False
                lblNumFattura.Visible = False

                btnStampaPrenotazioni.Visible = False
                btnStampaFatturazione.Visible = False
                btnReportExcel.Visible = False




            ElseIf dropTipoDocumento.SelectedValue = "1" Then
                'PRENOTAZIONE
                lblStato.Visible = True
                lblStato.Text = "\Stato prenotazione"
                dropStatoPrenotazione.Visible = True
                dropStatoContratto.Visible = False
                txtCercaNumStaz.Visible = True
                txtCercaNumInterno.Visible = True
                lblTipoNumero.Text = "Staz.\Num. Pren."
                lblRifTo.Visible = True
                lblRifTo.Text = "Rif. T.O."
                txtCercaRiferimento.Visible = True
                lblDaRibaltamento.Visible = True
                cercaPrenotazioniRibaltamento.Visible = True
                lblPrepagate.Visible = True
                lblPrepagate.Text = "Prenot.Prepagate"
                dropPrenotazioniPrepagate.Visible = True
                lblPresRientro.Visible = False
                cercaStazionePresuntoRientro.Visible = False
                lblPresRDa.Visible = False
                lblPresRA.Visible = False
                txtCercaPresRDa.Visible = False
                txtCercaPresRA.Visible = False
                lblCercaGruppoContratto.Visible = False
                dropCercaGruppoContratto.Visible = False
                txtCercaTargaContratto.Visible = False
                dropCercaGruppoPrenotazione.Visible = True
                txtCercaTargaPrenotazione.Visible = True
                lblCercaGruppoTargaPrenot.Visible = True

                txtCercaNunFattura.Visible = False
                dropCercaAnnoFattura.Visible = False
                lblNumFattura.Visible = False

                btnStampaPrenotazioni.Visible = True
                btnStampaFatturazione.Visible = False
                btnReportExcel.Visible = True

            ElseIf dropTipoDocumento.SelectedValue = "3" Then

                'CONTRATTO
                lblStato.Visible = True
                lblStato.Text = "\Stato contratto"
                dropStatoPrenotazione.Visible = False
                dropStatoContratto.Visible = True
                txtCercaNumStaz.Visible = True
                txtCercaNumInterno.Visible = True
                lblTipoNumero.Text = "Staz.\Num. CNT"
                lblRifTo.Visible = True
                lblRifTo.Text = "Rif. T.O."
                txtCercaRiferimento.Visible = True
                lblDaRibaltamento.Visible = False
                cercaPrenotazioniRibaltamento.Visible = False
                lblPrepagate.Visible = True
                lblPrepagate.Text = "Da Prenot.Prepagate"
                dropPrenotazioniPrepagate.Visible = True
                lblPresRientro.Visible = True
                cercaStazionePresuntoRientro.Visible = True
                lblPresRDa.Visible = True
                lblPresRA.Visible = True
                txtCercaPresRDa.Visible = True
                txtCercaPresRA.Visible = True
                lblCercaGruppoContratto.Visible = True
                dropCercaGruppoContratto.Visible = True
                txtCercaTargaContratto.Visible = True
                dropCercaGruppoPrenotazione.Visible = False
                txtCercaTargaPrenotazione.Visible = False
                lblCercaGruppoTargaPrenot.Visible = False

                txtCercaNunFattura.Visible = True
                dropCercaAnnoFattura.Visible = True
                lblNumFattura.Visible = True

                btnStampaPrenotazioni.Visible = False
                If livello_accesso_fatturazione.Text = "3" Then
                    btnStampaFatturazione.Visible = True
                Else
                    btnStampaFatturazione.Visible = False
                End If

                btnReportExcel.Visible = True

                dropCercaAnnoFattura.SelectedValue = Date.Now.Year.ToString

            End If
        Catch ex As Exception
            Response.Write("error setta_pannello_ricerca: " & ex.Message & "<br/>")
        End Try

    End Sub


    Protected Sub dropTipoDocumento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropTipoDocumento.SelectedIndexChanged



        setta_pannello_ricerca()

        Session("preventivo_load") = "" 'reset session  salvo 03.01.2023

        ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, lblOrderBY_cnt.Text)




    End Sub

    Protected Sub btnPulisciCampi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPulisciCampi.Click

        If dropTipoDocumento.SelectedValue = "2" Then
            'PREVENTIVO
            txtCercaNumStaz.Text = ""
            txtCercaNumInterno.Text = ""
            txtCercaCognome.Text = ""
            txtCercaNome.Text = ""
            cercaStazioneDropOff.SelectedValue = "0"
            txtCercaDropOffDa.Text = ""
            txtCercaDropOffA.Text = ""
            dropCercaTipoCliente.SelectedValue = "-1"

            txtCercaCreazioneDa.Text = "" '18.12.2020
            txtCercaCreazioneA.Text = ""    '18.12.2020

            If Request.Cookies("SicilyRentCar")("stazione") <> "88" Then
                cercaStazionePickUp.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")

                If cercaStazionePickUp.SelectedValue = "88" Then
                    cercaStazionePickUp.SelectedValue = "0"
                End If

                txtCercaPickUpDa.Text = Format(Now(), "dd/MM/yyyy")
                txtCercaPickUpA.Text = Format(Now(), "dd/MM/yyyy")
            Else
                txtCercaPickUpDa.Text = ""
                txtCercaPickUpA.Text = ""
                cercaStazionePickUp.SelectedValue = "0"
            End If

            ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
        ElseIf dropTipoDocumento.SelectedValue = "1" Then
            'PRENOTAZIONE
            txtCercaNumStaz.Text = ""
            txtCercaNumInterno.Text = ""
            txtCercaCognome.Text = ""
            txtCercaNome.Text = ""
            cercaStazioneDropOff.SelectedValue = "0"
            txtCercaDropOffDa.Text = ""
            txtCercaDropOffA.Text = ""
            dropCercaTipoCliente.SelectedValue = "-1"
            lblRifTo.Text = ""
            txtCercaRiferimento.Text = ""
            cercaPrenotazioniRibaltamento.SelectedValue = "0"
            dropPrenotazioniPrepagate.SelectedValue = "-1"
            dropStatoPrenotazione.SelectedValue = "0"
            txtCercaTargaPrenotazione.Text = ""
            dropCercaGruppoPrenotazione.SelectedValue = "0"

            cercaStazionePickUp.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")

            If cercaStazionePickUp.SelectedValue = "88" Then
                cercaStazionePickUp.SelectedValue = "0"
            End If

            txtCercaPickUpDa.Text = Format(Now(), "dd/MM/yyyy")
            txtCercaPickUpA.Text = Format(Now(), "dd/MM/yyyy")

            txtCercaCreazioneDa.Text = "" '18.12.2020
            txtCercaCreazioneA.Text = ""    '18.12.2020

            ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, lblOrderBY_cnt.Text)

        ElseIf dropTipoDocumento.SelectedValue = "3" Then

            'CONTRATTO
            txtCercaNumStaz.Text = ""
            txtCercaNumInterno.Text = ""
            txtCercaCognome.Text = ""
            txtCercaNome.Text = ""
            cercaStazioneDropOff.SelectedValue = "0"
            txtCercaDropOffDa.Text = ""
            txtCercaDropOffA.Text = ""
            dropCercaTipoCliente.SelectedValue = "-1"
            dropPrenotazioniPrepagate.SelectedValue = "-1"
            cercaStazionePresuntoRientro.SelectedValue = "0"
            txtCercaPresRDa.Text = ""
            txtCercaPresRA.Text = ""
            dropCercaGruppoContratto.SelectedValue = "0"
            txtCercaTargaContratto.Text = ""
            dropStatoContratto.SelectedValue = "2"

            txtCercaNunFattura.Text = ""

            cercaStazionePickUp.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")

            If cercaStazionePickUp.SelectedValue = "88" Then
                cercaStazionePickUp.SelectedValue = "0"
            End If

            txtCercaPickUpDa.Text = Format(Now(), "dd/MM/yyyy")
            txtCercaPickUpA.Text = Format(Now(), "dd/MM/yyyy")

            ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
        End If

    End Sub

    Protected Sub btnPulisciCampi1_Click(sender As Object, e As System.EventArgs) Handles btnPulisciCampi1.Click
        If dropTipoDocumento.SelectedValue = "2" Then
            'PREVENTIVO
            txtCercaNumStaz.Text = ""
            txtCercaNumInterno.Text = ""
            txtCercaCognome.Text = ""
            txtCercaNome.Text = ""
            cercaStazioneDropOff.SelectedValue = "0"
            txtCercaDropOffDa.Text = ""
            txtCercaDropOffA.Text = ""
            dropCercaTipoCliente.SelectedValue = "-1"
            txtCercaPickUpDa.Text = ""
            txtCercaPickUpA.Text = ""
            cercaStazionePickUp.SelectedValue = "0"
            txtCercaCreazioneDa.Text = "" '18.12.2020
            txtCercaCreazioneA.Text = ""    '18.12.2020

            ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
        ElseIf dropTipoDocumento.SelectedValue = "1" Then
            'PRENOTAZIONE
            txtCercaNumStaz.Text = ""
            txtCercaNumInterno.Text = ""
            txtCercaCognome.Text = ""
            txtCercaNome.Text = ""
            cercaStazioneDropOff.SelectedValue = "0"
            txtCercaDropOffDa.Text = ""
            txtCercaDropOffA.Text = ""
            dropCercaTipoCliente.SelectedValue = "-1"
            lblRifTo.Text = ""
            txtCercaRiferimento.Text = ""
            cercaPrenotazioniRibaltamento.SelectedValue = "0"
            dropPrenotazioniPrepagate.SelectedValue = "-1"
            dropStatoPrenotazione.SelectedValue = "0"
            txtCercaTargaPrenotazione.Text = ""
            dropCercaGruppoPrenotazione.SelectedValue = "0"
            cercaStazionePickUp.SelectedValue = "0"
            dropStatoPrenotazione.SelectedValue = "Tutti"

            txtCercaPickUpDa.Text = ""
            txtCercaPickUpA.Text = ""
            txtCercaCreazioneDa.Text = "" '18.12.2020
            txtCercaCreazioneA.Text = ""    '18.12.2020

            ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, lblOrderBY_cnt.Text)
        ElseIf dropTipoDocumento.SelectedValue = "3" Then
            'CONTRATTO
            txtCercaNumStaz.Text = ""
            txtCercaNumInterno.Text = ""
            txtCercaCognome.Text = ""
            txtCercaNome.Text = ""
            cercaStazioneDropOff.SelectedValue = "0"
            txtCercaDropOffDa.Text = ""
            txtCercaDropOffA.Text = ""
            dropCercaTipoCliente.SelectedValue = "-1"
            dropPrenotazioniPrepagate.SelectedValue = "-1"
            cercaStazionePresuntoRientro.SelectedValue = "0"
            txtCercaPresRDa.Text = ""
            txtCercaPresRA.Text = ""
            dropCercaGruppoContratto.SelectedValue = "0"
            txtCercaTargaContratto.Text = ""
            dropStatoContratto.SelectedValue = "Tutti"

            txtCercaNunFattura.Text = ""

            cercaStazionePickUp.SelectedValue = "0"

            txtCercaPickUpDa.Text = ""
            txtCercaPickUpA.Text = ""
            txtCercaCreazioneDa.Text = "" '18.12.2020
            txtCercaCreazioneA.Text = ""    '18.12.2020

            Session("pulisci_campi_contratti") = "OK" 'aggiunto salvo 11.04.2023


            ricerca(lblOrderBY_prev.Text, lblOrderBY_pren.Text, lblOrderBY_cnt.Text)



        End If

    End Sub

    Protected Sub btnStampaTKm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaTKm.Click
        If dropTariffeGeneriche.SelectedValue = "0" And dropTariffeParticolari.SelectedValue = "0" Then
            Libreria.genUserMsgBox(Me, "Selezionare una tariffa.")
        ElseIf dropTariffeGeneriche.SelectedValue <> "0" And dropTariffeParticolari.SelectedValue <> "0" Then
            Libreria.genUserMsgBox(Me, "Selezionare una tariffa generica oppure una tariffa particolare.")
        Else
            Dim id_tariffe_righe As String
            If dropTariffeGeneriche.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeGeneriche.SelectedValue
            ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeParticolari.SelectedValue
            End If
            Dim tariffa_broker As Boolean = is_tariffa_broker(id_tariffe_righe)



            '# NUOVO recupera la lista delle tariffe salvo 07.12.2022

            Dim id_tempo_km As String = "" '=funzioni_comuni.getIdTempoKm(id_tariffe_righe) 'vecchio calcolo salvo 07.12.2022

            Dim idStazionePickUp As String = dropStazionePickUp.SelectedValue
            Dim idStazioneDropOff As String = dropStazioneDropOff.SelectedValue

            Dim dataPickup As String = txtDaData.Text   '"21/12/2022"
            Dim dataDropOff As String = txtAData.Text   '"24/12/2022"
            Dim gnolo As Integer = DateDiff("d", CDate(dataPickup), CDate(dataDropOff))

            Dim tipoCli As String = dropTipoCliente.SelectedValue
            Dim idGruppo As String = "24" 'nn serve a nulla ma solo per riempire il parametro salvo 07.12.2022

            'verifica se tariffe generiche o tariffe particolari 
            'e ricava valore dal dropdown
            Dim TipoTariffa As String
            Dim descTariffa As String
            If dropTariffeGeneriche.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeGeneriche.SelectedValue
                descTariffa = dropTariffeGeneriche.SelectedItem.ToString
                TipoTariffa = "G"
            ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeParticolari.SelectedValue
                descTariffa = dropTariffeParticolari.SelectedItem.ToString
                TipoTariffa = "P"
            End If

            'lista delle tariffe - salvo 08.12.2022

            Dim idTariffa As String = id_tariffe_righe
            descTariffa = funzioni_comuni_new.GetCodiceTariffa(idTariffa) 'aggiunto salvo 29.12.2022

            If listPreventiviCosti.Visible = False Then 'se nessuna listpreventivi ricalcola
                id_tempo_km = funzioni_comuni_new.GetNewTariffaTempoKmPeriodiPDFTariffe(idStazionePickUp, idStazioneDropOff, dataPickup,
                                                                                                              dataDropOff, tipoCli, idGruppo, TipoTariffa, descTariffa, idTariffa, txtNumeroGiorni.Text)

            Else
                'altrimenti prende i valori già presenti nelle lbl della pagina - salvo 28.01.2023
                Dim list_tariffe As String = lbl_list_tariffe_tempoKM.Text
                id_tempo_km = list_tariffe & "&idtar=" & lbl_list_tariffe.Text

            End If

            id_tempo_km += "&tsc=" & lbl_sconti_tariffe.Text    'aggiunto salvo 20.01.2023
            '@ end modifica per lista tariffe


            'Exit Sub ' test

            '#aggiunto salvo 16.06.2023 
            'per reindirizzare su nuovo file di stampa in PDF delle tariffe
            'secondo il nuovo metodo che recupera i valori registrati nelle righe
            'se preventivo salvato va sulla nuova visualizzazione
            Dim newPDF As String = "0"  '0=no  / 1=Si (va su 'stampa_tempo_km_new.aspx')
            Dim fileStampa As String = "stampa_tempo_km.aspx"
            'se il preventivo non è salvato il lblnumPreventivo non esiste esiste solo idPreventivo
            If lblNumPreventivo.Text <> "" Then  'aggiunto 27.06.2023 salvo
                'verifica se si tratta di nuovo/old sistema di recupero salvo 27.06.2023
                'cerca il valore nel campo list_tariffe
                If funzioni_comuni_new.VerificaListTariffePDF("preventivi", idPreventivo.Text) = True Then
                    newPDF = "1"
                    fileStampa = "stampa_tempo_km_new.aspx"
                End If
            End If
            '@ aggiunto salvo 16.06.2023

            If tariffa_broker Then

                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzareValoreTariffaBroker) <> "1" Then

                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        Session("url_print") = "/stampe/" & fileStampa & "?pagina=verticale&newpdf=" & newPDF & "&id_tempo_km=" & id_tempo_km & "&nro=" & lblNumPreventivo.Text & "&iddoc=" & idPreventivo.Text & "&tbl=preventivi"
                        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                        End If
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Non hai le autorizzazioni per visualizzare i Valori Tariffa per le Tariffe Broker.")
                End If
            Else
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzareValoreTariffa) <> "1" Then


                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        Session("url_print") = "/stampe/" & fileStampa & "?pagina=verticale&newpdf=" & newPDF & "&id_tempo_km=" & id_tempo_km & "&nro=" & lblNumPreventivo.Text & "&iddoc=" & idPreventivo.Text & "&tbl=preventivi"
                        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                        End If
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Non hai le autorizzazioni per visualizzare i Valori Tariffa.")
                End If
            End If
        End If
    End Sub

    Protected Sub btnStampaCondizioni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaCondizioni.Click
        If dropTariffeGeneriche.SelectedValue = "0" And dropTariffeParticolari.SelectedValue = "0" Then
            Libreria.genUserMsgBox(Me, "Selezionare una tariffa.")
        ElseIf dropTariffeGeneriche.SelectedValue <> "0" And dropTariffeParticolari.SelectedValue <> "0" Then
            Libreria.genUserMsgBox(Me, "Selezionare una tariffa generica oppure una tariffa particolare.")
        Else
            Dim id_tariffe_righe As String
            If dropTariffeGeneriche.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeGeneriche.SelectedValue
            ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeParticolari.SelectedValue
            End If
            Dim tariffa_broker As Boolean = is_tariffa_broker(id_tariffe_righe)

            If tariffa_broker Then
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzareCondizioniTariffeBroker) <> "1" Then
                    Dim id_condizione As String = funzioni_comuni.getIdCondizione(id_tariffe_righe)
                    Dim id_condizione_madre As String = funzioni_comuni.getIdCondizioneMadre(id_tariffe_righe)


                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        Session("url_print") = "/stampe/stampa_tariffa.aspx?pagina=verticale&id_tariffe_righe=" & id_tariffe_righe & "&id_cond=" & id_condizione & "&id_cond_madre=" & id_condizione_madre
                        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                        End If
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Non hai le autorizzazioni per visualizzare le condizioni per le Tariffe Broker.")
                End If
            Else
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzareCondizioniTariffa) <> "1" Then
                    Dim id_condizione As String = funzioni_comuni.getIdCondizione(id_tariffe_righe)
                    Dim id_condizione_madre As String = funzioni_comuni.getIdCondizioneMadre(id_tariffe_righe)

                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        Session("url_print") = "/stampe/stampa_tariffa.aspx?pagina=verticale&id_tariffe_righe=" & id_tariffe_righe & "&id_cond=" & id_condizione & "&id_cond_madre=" & id_condizione_madre
                        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                        End If
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Non hai le autorizzazioni per visualizzare le condizioni di tariffa.")
                End If
            End If
        End If
    End Sub

    Protected Sub btnSalvaPrenNoTariffa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvaPrenNoTariffa.Click
        dropTariffeGeneriche.SelectedValue = "0"

        'CONTROLLO CHE E' STATO SELEZIONATO ALMENO UN GRUPPO
        Dim almeno_un_gruppo_selezionato As Boolean = False
        Dim piu_di_un_gruppo_selezionato As Boolean = False
        Dim id_gruppo As String
        Dim gruppo As String

        For i = 0 To listGruppi.Items.Count - 1
            Dim sel_gruppo As CheckBox = listGruppi.Items(i).FindControl("sel_gruppo")
            If sel_gruppo.Checked Then
                If almeno_un_gruppo_selezionato Then
                    piu_di_un_gruppo_selezionato = True
                End If
                Dim id_gr As Label = listGruppi.Items(i).FindControl("id_gruppo")
                Dim gr As Label = listGruppi.Items(i).FindControl("gruppo")
                id_gruppo = id_gr.Text
                gruppo = gr.Text
                almeno_un_gruppo_selezionato = True
            End If
        Next

        If almeno_un_gruppo_selezionato Then
            If Not piu_di_un_gruppo_selezionato Then
                setPadding("preventivo_salva")

                If preventivo_puo_diventare_prenotazione(idPreventivo.Text) Then
                    id_conducente.Text = ""
                    txtNomeConducente.Text = ""
                    txtCognomeConducente.Text = ""
                    txtMailConducente.Text = ""
                    txtIndirizzoConducente.Text = ""
                    txtRiferimentoTO.Text = ""
                    lblRifToOld.Text = ""
                    txtVoloOut.Text = ""
                    txtVoloPr.Text = ""
                    txtNote.Text = ""
                    txtDataDiNascita.Text = ""

                    txtCognomeConducente.Text = txtCognome.Text
                    txtNomeConducente.Text = txtNome.Text
                    txtMailConducente.Text = txtMail.Text
                    txtRifTel.Text = txtTelefono.Text

                    tab_preventivo.Visible = False
                    tab_prenotazioni.Visible = True
                    btnSalvaPrenotazione.Visible = True

                    'SELEZIONO OBBLIGATORIAMENTE LA DITTA SENZA POTERLA CAMBIARE SE E' STATO SPECIFICATO IL CODICE EDP:

                    If txtCodiceCliente.Text <> "" Then
                        txtNomeDitta.Text = lblNomeDitta.Text
                        id_ditta.Text = getIdDitta(txtCodiceCliente.Text)
                        btnModificaDitta.Visible = False
                    Else
                        txtNomeDitta.Text = ""
                        id_ditta.Text = ""
                        btnModificaDitta.Visible = True
                    End If

                    id_gruppo_auto_scelto.Text = id_gruppo
                    lblGruppoNoTariffa.Text = "Gruppo: " & gruppo

                    table_gruppi.Visible = False

                    'BLOCCO LA POSSIBILITA' DI MODIFICARE TARIFFA E SCONTO - PER FARLO SARA' NECESSARIO AGIRE SULL'APPOSITO PULSANTE--------------------
                    dropTariffeGeneriche.Enabled = False
                    dropTariffeParticolari.Enabled = False
                    txtSconto.ReadOnly = True
                    dropTipoSconto.Enabled = False
                    btnCambiaTariffa.Visible = True

                    If dropFonteCommissionabile.Visible Then
                        dropFonteCommissionabile.Enabled = False
                        dropTipoCommissione.Enabled = False
                        btnAggiornaFontiCommissionabili.Visible = False
                    End If
                    '-----------------------------------------------------------------------------------------------------------------------------------

                    tariffa_broker.Text = "1"
                Else
                    Libreria.genUserMsgBox(Me, "Prenotazione o contratto già salvato oppure preventivo non più esistente.")
                End If
            Else
                Libreria.genUserMsgBox(Me, "Attenzione: selezionare un solo gruppo per poter salvare la prenotazione.")
            End If
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: specificare il gruppo auto da prenotare.")
        End If
    End Sub

    Protected Function getPercentualeCommissionabile(ByVal id_fonte_commissionabile As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "SELECT ISNULL(percentuale,0) FROM fonti_commissionabili WITH(NOLOCK) WHERE id=" & id_fonte_commissionabile

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        getPercentualeCommissionabile = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function


    Protected Function get_numero_prenotazioni(ByVal condizione As String) As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String = "SELECT ISNULL(count(nr_pren),0) FROM prenotazioni WITH(NOLOCK) LEFT JOIN clienti_tipologia WITH(NOLOCK) ON prenotazioni.id_fonte=clienti_tipologia.id LEFT JOIN stazioni AS stazioni1 WITH(NOLOCK) ON prenotazioni.PRID_stazione_out=stazioni1.id LEFT JOIN stazioni As stazioni2 WITH(NOLOCK) ON prenotazioni.PRID_stazione_pr=stazioni2.id LEFT JOIN gruppi WITH(NOLOCK) ON prenotazioni.id_gruppo=gruppi.id_gruppo LEFT JOIN prenotazioni_status WITH(NOLOCK) ON prenotazioni.status=prenotazioni_status.id WHERE prenotazioni.attiva='1' " & condizione

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            get_numero_prenotazioni = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error (get_numero_prenotazioni) : " & ex.Message & "<br/>")
        End Try

    End Function

    Protected Sub btnStampaPrenotazioni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaPrenotazioni.Click
        Dim condizione As String = condizione_where_prenotazione()
        Dim num_prenotazioni As String = get_numero_prenotazioni(condizione)

        If CInt(num_prenotazioni) <= 1000 Then
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                Session("url_print") = "/stampe/prenotazioni/lista_prenotazioni.aspx?orientamento=verticale&query=" & Server.UrlEncode(condizione & " " & lblOrderBY_pren.Text) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&header_html=/stampe/prenotazioni/header_lista_prenotazioni.aspx?valore=" & num_prenotazioni
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                End If
            End If
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: è possibile stampare fino a 1000 prenotazioni in un'unica stampa.")
        End If
    End Sub

    Protected Sub btnStampaFatturazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaFatturazione.Click
        Dim condizione As String = condizione_where_contratti()

        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            Session("url_print") = "/stampe/contratti/controllo_fatturazione.aspx?orientamento=orizzontale&query=" & Server.UrlEncode(condizione) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&header_html=/stampe/contratti/header_controllo_fatturazione.aspx?orientamento"
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
            End If
        End If
    End Sub

    Protected Sub btnReportExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReportExcel.Click
        If dropTipoDocumento.SelectedValue = "1" Then
            Dim condizione As String = condizione_where_prenotazione() & " " & lblOrderBY_pren.Text

            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                Session("condizione") = condizione

                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('stampaReportPrenotazioni.aspx','')", True)
                End If
            End If
        ElseIf dropTipoDocumento.SelectedValue = "3" Then
            Dim condizione As String = condizione_where_contratti() & " " & lblOrderBY_cnt.Text

            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                Session("condizione") = condizione

                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('stampaReportContratti.aspx','')", True)
                End If
            End If
        End If

    End Sub



    Protected Sub txtCercaPickUpDa_TextChanged(sender As Object, e As EventArgs)
        txtAData.Text = txtDaData.Text


    End Sub

    Private Sub listPreventiviCosti_Init(sender As Object, e As EventArgs) Handles listPreventiviCosti.Init


    End Sub

    Private Sub listPreventiviCosti_EditCommand(source As Object, e As DataListCommandEventArgs) Handles listPreventiviCosti.EditCommand

    End Sub

    Protected Sub btn_tariffe_new_Click(sender As Object, e As EventArgs)
        Dim data_pick As String = txtDaData.Text
        Dim data_dropoff As String = txtAData.Text
        Dim id_gruppo As String = "25"
        Dim id_elemento = "78"
        Dim tipo_cliente As String = dropTipoCliente.SelectedValue
        Dim id_stazione As String = dropStazionePickUp.SelectedValue


        Dim nome_tariffa As String = ""

        If dropTariffeGeneriche.SelectedValue <> "0" Then
            nome_tariffa = dropTariffeGeneriche.SelectedItem.ToString
        Else
            If dropTariffeParticolari.SelectedValue <> "0" Then
                nome_tariffa = dropTariffeParticolari.SelectedItem.ToString
            End If
        End If

        If nome_tariffa = "" Then
            Libreria.genUserMsgBox(Page, "Selezionare una tariffa")
            Exit Sub

        End If



        Dim valore As Double = funzioni_comuni_new.getValoreTariffeNew(id_stazione, tipo_cliente, data_pick, data_dropoff, id_gruppo, id_elemento, nome_tariffa)

        Libreria.genUserMsgBox(Page, "Importo Tariffa: " & valore.ToString)


    End Sub

    'Tony 27/10/2022
    Protected Sub AggiornaDatiPerBroker()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
        Dim Sql As String
        Dim Sql2 As String
        Dim SqlQuery As String

        Try
            Sql = "update prenotazioni_costi set id_a_carico_di ='5' WHERE (id_documento = '" & idPrenotazione.Text & "') AND (selezionato = 1)"

            'Response.Write(Sql & "<br>")
            'Response.End()

            Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
            Cmd.ExecuteNonQuery()


            SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
            Cmd.CommandText = SqlQuery
            'Response.Write(Cmd.CommandText & "<br/>")
            'Response.End()
            Cmd.ExecuteNonQuery()


            Dim ris As String = ""

            Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc2.Open()

            Dim sqlStr As String = "SELECT SUM(valore_costo) AS somma from prenotazioni_costi WHERE (id_documento = '" & idPrenotazione.Text & "') AND (selezionato = 1)"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                Sql = "update prenotazioni set importo_a_carico_del_broker ='" & Replace(Rs1("somma"), ",", ".") & "' WHERE (Nr_Pren = '" & idPrenotazione.Text & "')"

                'Response.Write(Sql & "<br>")
                'Response.End()

                Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
                Cmd.ExecuteNonQuery()

                SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                Cmd.CommandText = SqlQuery
                'Response.Write(Cmd.CommandText & "<br/>")
                'Response.End()
                Cmd.ExecuteNonQuery()

                Sql = "update prenotazioni_costi set id_a_carico_di ='5' WHERE nome_costo ='TOTALE' and (id_documento = '" & idPrenotazione.Text & "')"

                'Response.Write(Sql & "<br>")
                'Response.End()

                Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
                Cmd.ExecuteNonQuery()

                'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                'Session("residenza_virtuale") = Cmd.ExecuteScalar

                SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                Cmd.CommandText = SqlQuery
                'Response.Write(Cmd.CommandText & "<br/>")
                'Response.End()
                Cmd.ExecuteNonQuery()
            End If

            Rs1.Close()
            Cmd1.Dispose()
            Dbc2.Close()
            Rs1 = Nothing
            Cmd1 = Nothing
            Dbc2 = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("AggiornaDatiPerBroker Errore contattare amministratore del sistema. <br/>" & ex.Message & "<br/>")
        End Try

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub
    'FINE Tony

    Protected Sub btnTestnewTariffa_Click(sender As Object, e As EventArgs)



        Dim sqlstr As String

        Try

            Dim idStazionePickUp As String = dropStazionePickUp.SelectedValue
            Dim idStazioneDropOff As String = dropStazioneDropOff.SelectedValue

            Dim dataPickup As String = txtDaData.Text   '"21/12/2022"
            Dim dataDropOff As String = txtAData.Text   '"24/12/2022"
            Dim giorniNoleggio As Integer = DateDiff("d", CDate(dataPickup), CDate(dataDropOff))

            Dim tipoCli As String = dropTipoCliente.SelectedValue

            Dim descTariffa As String

            Dim idGruppo As String = "24"
            Dim id_tariffe_righe As String

            'verifica se tariffe generiche o tariffe particolari 
            'e ricava valore dal dropdown
            Dim TipoTariffa As String
            If dropTariffeGeneriche.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeGeneriche.SelectedValue
                descTariffa = dropTariffeGeneriche.SelectedItem.ToString
                TipoTariffa = "G"
            ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeParticolari.SelectedValue
                descTariffa = dropTariffeParticolari.SelectedItem.ToString
                TipoTariffa = "P"
            End If
            'descTariffa = Replace(descTariffa, "'", "''")      'viene eventualmente corretto nella funzione

            Dim idTariffa As String = id_tariffe_righe

            Dim valore_importo As Double = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(idStazionePickUp, idStazioneDropOff, dataPickup,
                                                                                           dataDropOff, tipoCli, idGruppo, TipoTariffa, descTariffa, giorniNoleggio, id_tariffe_righe, False, "", "", txtSconto.Text)

            Dim ListTariffe As String = funzioni_comuni_new.GetNewTariffaTempoKmPeriodiPDFTariffe(idStazionePickUp, idStazioneDropOff, dataPickup,
                                                                                           dataDropOff, tipoCli, idGruppo, TipoTariffa, descTariffa, idTariffa, txtNumeroGiorni.Text)

            funzioni_comuni.genUserMsgBox(Page, "Importo Totale Tariffa: € " & valore_importo.ToString & "\nListTariffe: " & ListTariffe)

            lbl_new_tariffa.Text = "New Tariffa: € " & valore_importo.ToString & " - Tariffe: " & ListTariffe



        Catch ex As Exception

            funzioni_comuni.genUserMsgBox(Page, "btnTestnewTariffa_click error: " & ex.Message)

        End Try




    End Sub

    Private Sub preventivi_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        Dim ris As String = ""

        ''If Session("btnVediCalcolo") <> "1" Then
        'Try
        '    ris = VediUltimoCalcolo(0)
        'Catch ex As Exception
        '    ris = ris
        'End Try
        '''End If

        'btnVediUltimoCalcolo.Visible = True



    End Sub

    Protected Sub GetList_anno(id)

        'a seconda della data visualizzo elenco
        Dim ycur As Integer = Year(Date.Now)

        Dim xc As Integer
        Try

            For xc = ycur To 2013 Step -1

                Dim l As New ListItem(xc, xc, True)
                dropCercaAnnoFattura.Items.Add(l)
            Next

            dropCercaAnnoFattura.SelectedValue = ycur


        Catch ex As Exception
            Response.Write("error getList_anno: " & ex.Message & "<br/>")
        End Try


    End Sub



End Class
