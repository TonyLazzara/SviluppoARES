Imports funzioni_comuni


Partial Class contratti_new
    Inherits System.Web.UI.Page

    Dim funzioni As New funzioni_comuni
    Dim cPagamenti As New Pagamenti
 

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler gestione_checkin.ChiusuraForm, AddressOf gestione_danni_ChiusuraForm
        AddHandler gestione_checkin.SalvaCheckIn, AddressOf gestione_danni_SalvataggioCheckIn
        AddHandler gestione_checkin.PagamentoDanno, AddressOf gestione_danni_PagamentoDanno

        AddHandler anagrafica_conducenti1.scegliConducente, AddressOf scegli_conduente
        AddHandler anagrafica_ditte.scegliDitta, AddressOf scegli_ditta
        AddHandler Scambio_Importo1.ScambioImportoClose, AddressOf ScambioImportoClose
        AddHandler Scambio_Importo1.ScambioImportoTransazioneEseguita, AddressOf ScambioImportoTransazioneEseguita
        AddHandler Scambio_Importo1.CassaPagamentoEseguito, AddressOf CassaPagamentoEseguito

        ScriptManager.GetCurrent(Me.Page).Scripts.Add(New ScriptReference("lytebox.js"))

        If Not Page.IsPostBack() Then
            dropStazionePickUp.DataBind()
            dropStazioneDropOff.DataBind()
            dropTipoCliente.DataBind()
            gruppoDaCalcolare.DataBind()
            gruppoDaConsegnare.DataBind()
            DDLConducenti.DataBind()
            dropGruppoDaConsegnare.DataBind()
            dropGruppoPrenotato.DataBind()

            livello_accesso_sconto.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.ApplicareSconto)
            livello_accesso_omaggi.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.OmaggiareAccessori)
            livello_accesso_dettaglio_pos.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzaDettaglioOperazionePOS)
            livello_accesso_broker.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzareCostiBroker)
            livello_accesso_modifica_targa.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.TargaSempreModificabile)
            livello_accesso_annulla_quick.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.AnnullareQuickCheckIn)
            livello_accesso_admin.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Fatturazione)

            If Session("id_contratto_from_prenotazioni") <> "" And Session("idPrenotazione") <> "" Then
                'IN QUESTO CASO PROVENGO DA PRENOTAZIONI - CONTRATTO DA CREARE DA PRENOTAZIONE SALVATA
                provenienza.Text = "prenotazioni.aspx"
                idContratto.Text = Session("id_contratto_from_prenotazioni")
                idPrenotazione.Text = Session("idPrenotazione")
                lblNumContratto.Text = "0"

                Session("idPrenotazione") = ""
                Session("id_contratto_from_prenotazioni") = ""

                lblTestoDaPreautorizzare.Visible = True
                lblDaPreautorizzare.Visible = True
                lblEuroDaPreautorizzare.Visible = True

                lblDaPrenotazione.Visible = True
                lblNumPren.Visible = True
                lblNumPren.Text = getNumeroPrenotazione(idPrenotazione.Text)

                nuovoContrattoDaPrenotazione(idContratto.Text, idPrenotazione.Text)
                'IN PARTICOLARI CASI SI POTREBBE GIA' PASSARE ALLA FASE DI PAGAMENTO
                check_possibile_pagare()
            ElseIf Session("id_contratto_from_preventivi") <> "" Then
                provenienza.Text = "preventivi.aspx"
                idContratto.Text = Session("id_contratto_from_preventivi")
                idPrenotazione.Text = ""
                lblNumContratto.Text = "0"

                If Session("idPreventivo") <> "" Then
                    'CONSERVO L'ID DEL PREVENTIVO CHE ERA STATO SALVATO
                    idPreventivo.Text = Session("idPreventivo")
                    Session("idPreventivo") = ""
                Else
                    idPreventivo.Text = ""
                End If

                Session("id_contratto_from_preventivi") = ""

                lblTestoDaPreautorizzare.Visible = True
                lblDaPreautorizzare.Visible = True
                lblEuroDaPreautorizzare.Visible = True

                nuovoContrattoDaPreventivo(idContratto.Text, idPreventivo.Text)
                'IN PARTICOLARI CASI SI POTREBBE GIA' PASSARE ALLA FASE DI PAGAMENTO
                check_possibile_pagare()
            ElseIf Session("carica_contratto_da_gestione_rds") <> "" Then
                idContratto.Text = Session("carica_contratto_da_gestione_rds")
                Session("carica_contratto_da_gestione_rds") = ""
                lb_gestione_rds.Text = "FILTRO Per rientro sul form RDS!"
                bt_Gestione_RDS.Visible = True

                caricaContratto(idContratto.Text, "", "")
            ElseIf Session("carica_contratto") <> "" Then
                idContratto.Text = Session("carica_contratto")
                Session("carica_contratto") = ""

                caricaContratto(idContratto.Text, "", "")
            Else
                'Response.Redirect("default.aspx")
            End If

            DDLFornitore.DataBind()
        Else
            If Session("sel_vettura") = "SI" Then
                Libreria.genUserMsgBox(Me, Request.QueryString("sel_vettura"))

                Session("sel_vettura") = ""
            End If
        End If
    End Sub

    Protected Sub gestione_danni_SalvataggioCheckIn(ByVal sender As Object, ByVal e As EventoNuovoRecord)
        tab_contratto.Visible = True
        div_edit_danno.Visible = False

        If e.Valore = numCrv.Text Then
            'IN QUESTO CASO STO FACENDO IL CHECK PER L'AUTO COLLEGATA ATTUALMENTE AL CONTRATTO
            salva_rientro()

            If lblIdGps.Text <> "" Then
                'SE E' STATO SELEZIONATO UN GPS REGISTRO IL SUO RIENTRO
                registra_rientro_gps()
            End If

            btnSalvaRientro.Text = "Vedi check"
            btnPagamento.Visible = True
            btnGeneraContrattoRientro.Visible = True
            btnAnnullaQuickCheckIn.Visible = False

            If livello_accesso_admin.Text <> "3" Then
                btnFattDaControllare.Visible = True
                btnDaFatturare.Visible = False
            Else
                btnFattDaControllare.Visible = False
                btnDaFatturare.Visible = True
                btnModificaAdmin.Visible = True
            End If

            txtSerbatoioRientro.ReadOnly = True
            txtKmRientro.ReadOnly = True

            If numCrv.Text <> "0" Then
                listCrv.DataBind()
            Else
                'SE NON C'E MAI STATO UN CRV ED E' POSSIBILE ANCORA ANNULLARE IL CONTRATTO ABILITO IL RELATIVO PULSANTE
                If funzioni_comuni.contratto_settabile_void(idContratto.Text, Request.Cookies("SicilyRentCar")("stazione")) Then
                    btnVoid.Visible = True
                End If
            End If

            'ADDEBITO LE INFORMATIVE QUALORA FOSSE STATO SALVATO UN RDS CON PERDITA ACCESSORI
            addebita_informative()

            lblSaldo.Text = "Saldo: " & FormatNumber(getTotaleDaPagare(), 2, , , TriState.False) & " €"
        Else
            'IN QUESTO CASO STO EFFETTUANDO IL CHECK IN SICURAMENTE PER UN VEICOLO SOSTITUITO CON CRV
            aggiungi_costi_crv(e.Valore)

            listCrv.DataBind()
        End If
    End Sub

    Protected Sub aggiungi_costi_crv(ByVal numero_crv As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        '----AGGIUNTA COSTO REFUEL-------------------------------------------------------------------------------------------------------
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT veicoli.targa, contratti_crv_veicoli.serbatoio_uscita, contratti_crv_veicoli.serbatoio_rientro, modelli.TipoCarburante, modelli.id_gruppo FROM contratti_crv_veicoli WITH(NOLOCK) INNER JOIN veicoli WITH(NOLOCK) ON contratti_crv_veicoli.id_veicolo=veicoli.id INNER JOIN modelli ON veicoli.id_modello=modelli.id_modello WHERE contratti_crv_veicoli.num_contratto='" & lblNumContratto.Text & "' AND num_crv='" & numero_crv & "'", Dbc)
        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()
        Rs.Read()

        Dim targa As String = Rs("targa")
        Dim differenza_litri As Integer = Rs("serbatoio_uscita") - Rs("serbatoio_rientro")
        Dim id_gruppo As String = Rs("id_gruppo")
        Dim id_alim As String = Rs("TipoCarburante")

        'SE IL CLIENTE NON HA ACQUISTATO IL PIENO CARBURANTE ALL'USCITA E SE ESISTE L'INFORMATIVA SERVIZIO RIFORNIMENTO 
        'E LA DIFFERENZA DI CARBURANTE TRA USCITA E RIENTRO E' SUPERIORE AL MARGINE DI TOLLERANZA SETTATO VIENE AGGIUNTO AL TOTALE IL SERVIZIO RIFORNIMENTO 
        'IN MANIERA AUTOMATICA E IL COSTO DEL RIFORNIMENTO. QUESTO SOLAMENTE SE L'AUTO ESCE COL PIENO, ALTRIMENTI IL SERVIZIO RIFORNIMENTO
        'NON VIENE ADDEBITATO
        If Not funzioni_comuni.pieno_carburante_selezionato(idContratto.Text, numCalcolo.Text) Then
            If differenza_litri > 0 Then
                funzioni.addebita_refuel(idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("stazione"), gruppoDaCalcolare.Text, id_alim, differenza_litri, " Targa " & targa)
            End If

            listContrattiCosti.DataBind()
            aggiorna_informazioni_dopo_modifica_costi()
        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub gestione_danni_PagamentoDanno(ByVal sender As Object, ByVal e As EventoNuovoRecord)
        lb_id_evento.Text = e.Valore
        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(e.Valore)

        pagamento_rds(mio_evento)
    End Sub

    Protected Sub gestione_danni_ChiusuraForm(ByVal sender As Object, ByVal e As System.EventArgs)
        tab_contratto.Visible = True
        div_edit_danno.Visible = False
    End Sub

    Protected Sub check_possibile_pagare()
        'LA PROCEDURA, RICHIAMATA AD OGNI REFRESH DI PAGINA, CONTROLLA SE E' POSSIBILE ABILITARE IL PULSANTE DI PAGAMENTO
        If statoContratto.Text = "0" Then
            ' Response.Write("PRIMO COND: " & idPrimoConducente.Text & " - AUTO: " & auto_collegata.Text & "- DITTA: " & idDitta.Text)
            'NEL CASO IN CUI SIAMO IN FASE DI GENERAZIONE DI CONTRATTO

            'DEVE ESSERE STATO SELEZIONATO IL PRIMO GUIDATORE
            'DEVE ESSERE STATO SELEZIONATO IL VEICOLO
            'DEVE ESSERE STATA SPECIFICATA LA DITTA A CUI FATTURARE - NO!!!! ANCHE CASH
            'GRUPPO AUTO SCELTA CONGRUENTE A QUELLO CALCOLATO 

            If idPrimoConducente.Text <> "" And auto_collegata.Text = "1" And idDitta.Text <> "" And ((gruppoDaConsegnare.SelectedValue = 0 And gruppoDaCalcolare.SelectedValue = id_gruppo_auto_selezionata.Text) Or (gruppoDaConsegnare.SelectedValue <> 0 And gruppoDaConsegnare.SelectedValue = id_gruppo_auto_selezionata.Text)) Then
                btnPagamento.Visible = True

                If full_credit.Text = "1" Then
                    'SE IL CONTRATTO E' FULL CREDIT POSSO EFFETTUARE IL CHECK OUT DEL VEICOLO SENZA EFFETTUARE PREAUTORIZZAZIONE
                    btnGeneraContratto.Visible = True
                End If
            Else
                btnPagamento.Visible = False
                btnGeneraContratto.Visible = False
            End If
        End If
    End Sub

    Protected Sub stato_contratto_in_corso()
        settaStatoContratto("2")

        'GUIDATORI E DITTE NON PIU' MODIFICABILI
        btnScegliPrimoGuidatore.Visible = False
        btnScegliSecondoConducente.Visible = False
        btnScegliDitta.Visible = False

        'GPS NON MODIFICABILE CON PROCEDURA PRE-USCITA VEICOLO
        btnCercaGps.Visible = False

        'NASCONDO IL TOTALE DA PREAUTORIZZARE 
        lblTestoDaPreautorizzare.Visible = False
        lblDaPreautorizzare.Visible = False
        lblEuroDaPreautorizzare.Visible = False

        'MOSTRO LA DIFFERENZA RISPETTO LA PRENOTAZIONE
        lblTestoDaPrenotazione.Visible = True
        lblDifferenzaDaPrenotazione.Visible = True
        lblEuroDaPrenotazione.Visible = True

        aggiorna_informazioni_dopo_modifica_costi()

        btnPagamento.Visible = True
        btnGeneraContratto.Visible = True

        btnTrovaTarga.Visible = False

        'RICALCOLO
        btnRicalcolaDaPrenotazione.Visible = False
        btnRicalcolaDaPreventivo.Visible = False
        btnRicalcolaModificaContratto.Visible = True

        btnAggiungiExtra.Visible = False

        'QUICK CHECK IN 
        btnQuickCheckIn.Visible = True

        'MODIFICHE PRECEDENTI
        elenco_modifiche.Visible = True
        btnAnnullaDocumento.Text = "Chiudi"

        'VISIBILITA' DEI PULSANTI E DELLE LABEL NASCOSTE PER UN CONTRATTO APERTO
        statoContratto.Text = "2"
        statoModificaContratto.Text = "0"


        'ACCESSORI EXTRA: VISUALIZZATI SOLO QUELLI ACQUISTABILI NOLO IN CORSO
        Dim id_tariffe_righe As String = ""
        If dropTariffeGeneriche.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
        End If

        sqlElementiExtra.SelectCommand = funzioni.getQueryElementiExtra(id_tariffe_righe, True)
        dropElementiExtra.Items.Clear()
        dropElementiExtra.Items.Add("Seleziona...")
        dropElementiExtra.Items(0).Value = "0"
        dropElementiExtra.DataBind()
    End Sub

    Protected Sub settaStatoContratto(ByVal stato_contratto As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE contratti SET status='" & stato_contratto & "' WHERE id='" & idContratto.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc = Nothing
    End Sub

    Protected Sub omaggia_tutto_x_complimentary()
        'NEL CASO DI COMPLIMENTARY OMAGGIO TUTTI I COSTI 

        Dim id_gruppoLabel As Label
        Dim id_a_carico_di As Label
        Dim id_elemento As Label
        Dim num_elemento As Label
        Dim nome_costo As Label
        Dim obbligatorio As Label
        Dim tipologia_franchigia As Label

        For i = 0 To listContrattiCosti.Items.Count - 1
            id_gruppoLabel = listContrattiCosti.Items(i).FindControl("id_gruppoLabel")
            id_a_carico_di = listContrattiCosti.Items(i).FindControl("id_a_carico_di")
            id_elemento = listContrattiCosti.Items(i).FindControl("id_elemento")
            nome_costo = listContrattiCosti.Items(i).FindControl("nome_costo")
            obbligatorio = listContrattiCosti.Items(i).FindControl("obbligatorio")
            tipologia_franchigia = listContrattiCosti.Items(i).FindControl("tipologia_franchigia")

            'VALORE TARIFFA
            If id_a_carico_di.Text = Costanti.id_accessorio_incluso And id_elemento.Text = Costanti.ID_tempo_km Then
                num_elemento = listContrattiCosti.Items(i).FindControl("num_elemento")

                funzioni.omaggio_accessorio(True, False, False, "", "", "", idContratto.Text, numCalcolo.Text, id_gruppoLabel.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text)
            End If

            'OBBLIGATORI
            If id_a_carico_di.Text <> Costanti.id_accessorio_incluso And obbligatorio.Text = "True" And nome_costo.Text <> Costanti.testo_elemento_totale Then
                num_elemento = listContrattiCosti.Items(i).FindControl("num_elemento")

                funzioni.omaggio_accessorio(True, False, False, "", "", "", idContratto.Text, numCalcolo.Text, id_gruppoLabel.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text)
            End If


            'ACCESSORI
            Dim chkScegli As CheckBox = listContrattiCosti.Items(i).FindControl("chkScegli")
            If listContrattiCosti.Items(i).FindControl("chkScegli").Visible Or (Not listContrattiCosti.Items(i).FindControl("chkScegli").Visible And listContrattiCosti.Items(i).FindControl("chkOmaggio").Visible) Then
                Dim chkOldScegli As CheckBox = listContrattiCosti.Items(i).FindControl("chkOldScegli")
                If chkOldScegli.Checked Then
                    num_elemento = listContrattiCosti.Items(i).FindControl("num_elemento")

                    funzioni.omaggio_accessorio(True, False, False, "", "", "", idContratto.Text, numCalcolo.Text, id_gruppoLabel.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text)
                End If
            End If
        Next
    End Sub

    Protected Sub CassaPagamentoEseguito(ByVal sender As Object, ByVal e As EventoConOggetto)
        tab_pagamento.Visible = False
        tab_contratto.Visible = True

        Dim mio_record As PAGAMENTI_EXTRA = CType(e.mioOggetto, PAGAMENTI_EXTRA)

        With mio_record
            Select Case .ID_TIPPAG
                Case enum_tipo_pagamento_p1000.COMPLIMENTARY
                    complimentary.Text = "1"
                    lblComplimentary.Visible = True

                    omaggia_tutto_x_complimentary()

                    listContrattiCosti.DataBind()
                Case enum_tipo_pagamento_p1000.FC_FULL_CREDIT
                    full_credit.Text = "1"
                    lblFullCredit.Visible = True
            End Select
        End With

        If Not tab_dettagli_pagamento.Visible Then
            tab_dettagli_pagamento.Visible = True
        End If

        listPagamenti.DataBind()
        ImpostaPannelloPagamento(lblNumPren.Text, lblNumContratto.Text)

        'RICALCOLO IL TOTALE DA PREAUTORIZZARE
        getTotaleDaPreautorizzare()

        tab_pagamento.Visible = False
        tab_contratto.Visible = True

        If statoContratto.Text = "4" Or statoContratto.Text = "8" Then
            lblSaldo.Text = "Saldo: " & FormatNumber(getTotaleDaPagare(), 2, , , TriState.False) & " €"

            'LO STATO POTREBBE ESSERE CAMBIATO DOPO L'AVVENUTO PAGAMENTO (LA FUNZIONE DI PAGAMENTO SI OCCUPA DI VARIARE LO STATO DEL CONTRATTO)
            If statoContratto.Text = "4" Then
                If getStatoContratto() = "8" Then
                    lblStatoFatturazione.Text = "Da Fatturare"
                    statoContratto.Text = "8"
                    btnDaFatturare.Text = "Non Fatturare"

                    btnGeneraFattura.OnClientClick = "javascript: return(window.confirm ('Attenzione - Sei sicuro di voler generare la fattura per questo contratto?'));"

                    btnGeneraFattura.Visible = True
                    lblDataFattura.Visible = True
                    txtDataFattura.Visible = True
                    lblNumFattura.Visible = True
                    txtNumFattura.Visible = True
                End If
            End If
        End If
    End Sub

    Public Sub ScambioImportoTransazioneEseguita(ByVal sender As Object, ByVal e As tabelle_pos_Scambio_Importo.TransazioneEseguitaEventArgs)

        Dim contratto As String
        Dim rds As String

        If lb_tipo_pagamento.Text = "rds" Then
            contratto = ""
            Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(lb_id_evento.text)

            rds = mio_evento.id_rds
        ElseIf lb_tipo_pagamento.Text = "contratto" Then
            contratto = lblNumContratto.Text
            rds = ""
        End If

        Select Case e.Transazione.IDFunzione
            Case Is = enum_tipo_pagamento_ares.Richiesta
                'Dim tr As classi_pagamento.TransazionePreautorizzazione = e.Transazione
                'cPagamenti.registra_preautorizzazione(tr, "", rds, "", contratto, Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))

                If contratto <> "" Then
                    'NASCONDO IL TOTALE DA PREAUTORIZZARE (SE HO EFFETTUATO UNA PREAUTORIZZAZIONE)
                    lblTestoDaPreautorizzare.Visible = False
                    lblDaPreautorizzare.Visible = False
                    lblEuroDaPreautorizzare.Visible = False

                    '    btnGeneraContratto.Visible = True
                    '    'LA TRANSAZIONE E' ANDATA A BUON FINE - SETTO LO STATO DEL CONTRATTO AD APERTO (SE LO STATO E' "SALVATO E MAI PREATORIZZATO"
                    '    'QUINDI CERTAMENTE NON ANCORA STAMPATO
                    '    If statoContratto.Text = "1" Then
                    '        stato_contratto_in_corso()
                    '        listContrattiCosti.DataBind()
                    '        bt_Check_Out.Visible = True
                    '        btnCRV.Visible = True
                    '        gestione_checkin.InitFormCheckOut(tipo_documento.Contratto, Integer.Parse(lblNumContratto.Text), Integer.Parse(numCrv.Text))
                    '        div_edit_danno.Visible = True
                    '        tab_contratto.Visible = False
                    '    End If
                End If
            Case Is = enum_tipo_pagamento_ares.Vendita
                'Dim tr As classi_pagamento.TransazioneVendita = e.Transazione
                'cPagamenti.registra_vendita(tr, "", rds, "", contratto, Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
            Case Is = enum_tipo_pagamento_ares.Integrazione
                'Dim tr As classi_pagamento.TransazioneIntegrazione = e.Transazione
                'cPagamenti.registra_integrazione(tr, "", rds, "", contratto, Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
            Case Is = enum_tipo_pagamento_ares.Chiusura
                'Dim tr As classi_pagamento.TransazioneChiusura = e.Transazione
                'cPagamenti.registra_chiusura(tr, "", rds, "", contratto, Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))

                aggiorna_informazioni_dopo_modifica_costi()
            Case Is = enum_tipo_pagamento_ares.Rimborso
                'Dim tr As classi_pagamento.TransazioneRimborso = e.Transazione
                'cPagamenti.registra_rimborso(tr, "", rds, "", contratto, Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
            Case Is = enum_tipo_pagamento_ares.Storno_Ultima_Operazione
                'Dim tr As classi_pagamento.TransazioneStorno = e.Transazione
                'cPagamenti.registra_storno(tr, "", rds, "", contratto, Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
        End Select
        If rds <> "" Then
            gestione_checkin.AggiornaPagamenti()

            tab_pagamento.Visible = False
            gestione_checkin.Visible = True

        End If
        If contratto <> "" Then
            If Not tab_dettagli_pagamento.Visible Then
                tab_dettagli_pagamento.Visible = True
            End If

            listPagamenti.DataBind()
            ImpostaPannelloPagamento(lblNumPren.Text, lblNumContratto.Text)
            'RICALCOLO IL TOTALE DA PREAUTORIZZARE
            getTotaleDaPreautorizzare()

            tab_pagamento.Visible = False
            tab_contratto.Visible = True

        End If

        If statoContratto.Text = "4" Or statoContratto.Text = "8" Then
            lblSaldo.Text = "Saldo: " & FormatNumber(getTotaleDaPagare(), 2, , , TriState.False) & " €"

            'LO STATO POTREBBE ESSERE CAMBIATO DOPO L'AVVENUTO PAGAMENTO (LA FUNZIONE DI PAGAMENTO SI OCCUPA DI VARIARE LO STATO DEL CONTRATTO)
            If statoContratto.Text = "4" Then
                If getStatoContratto() = "8" Then
                    lblStatoFatturazione.Text = "Da Fatturare"
                    statoContratto.Text = "8"
                    btnDaFatturare.Text = "Non Fatturare"

                    btnGeneraFattura.OnClientClick = "javascript: return(window.confirm ('Attenzione - Sei sicuro di voler generare la fattura per questo contratto?'));"

                    btnGeneraFattura.Visible = True
                    lblDataFattura.Visible = True
                    txtDataFattura.Visible = True
                    lblNumFattura.Visible = True
                    txtNumFattura.Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub ScambioImportoClose(ByVal sender As Object, ByVal e As EventArgs)
        tab_pagamento.Visible = False
        tab_contratto.Visible = True
    End Sub

    Protected Function check_spese_postali(ByVal metodo_spedizione As String) As Boolean
        'RESTITUISCE TRUE SE IL COSTO VIENE AGGIUNTO O SE VIENE RIMOSSO
        check_spese_postali = False

        Dim id_tariffe_righe As String = ""

        If dropTariffeGeneriche.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
        End If

        Dim sconto As Double
        If dropTipoSconto.SelectedValue = "0" Then
            sconto = CDbl(txtSconto.Text)
        ElseIf dropTipoSconto.SelectedValue = "1" Then
            sconto = 0
        End If

        Dim id_elemento_spese As String = funzioni_comuni.get_id_spese_spedizione_postali()

        If id_elemento_spese <> "0" Then
            Dim accessorio_esistente As Boolean = Not funzioni_comuni.accessorioExtraNonAggiunto(id_elemento_spese, CInt(gruppoDaCalcolare.SelectedValue), "", "", idContratto.Text, numCalcolo.Text)
            'SE LA DITTA COLLEGATA PREVEDE LA SPEDIZIONE POSTALE DELLA FATTURA AGGIUNGO IL COSTO 'SPEDE DI SPEDIZIONE POSTALI
            If metodo_spedizione = "P" And Not accessorio_esistente Then
                funzioni.aggiungi_accessorio_obbligatorio(id_elemento_spese, CInt(dropStazionePickUp.SelectedValue), CInt(dropStazioneDropOff.SelectedValue), CInt(gruppoDaCalcolare.SelectedValue), txtNumeroGiorni.Text, 0, False, Nothing, "", "", sconto, id_tariffe_righe, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                listContrattiCosti.DataBind()
                check_spese_postali = True
            ElseIf metodo_spedizione <> "P" And accessorio_esistente Then
                'IN QUESTO CASO L'ACCESSORIO ERA STATO AGGIUNTO MA ADESSO L'AZIENDA SELEZIONATA NON RICHIEDE LA SPEDIZIONE PER POSTA
                Dim omaggiato As Boolean
                Dim id_ele As Label

                'CONTROLLO SE ERA STATO OMAGGIATO
                For i = 0 To listContrattiCosti.Items.Count - 1
                    id_ele = listContrattiCosti.Items(i).FindControl("id_elemento")
                    If id_ele.Text = id_elemento_spese Then
                        Dim chkOldOmaggio As CheckBox = listContrattiCosti.Items(i).FindControl("chkOldOmaggio")
                        If chkOldOmaggio.Checked Then
                            omaggiato = True
                        Else
                            omaggiato = False
                        End If
                        Exit For
                    End If
                Next

                If Not omaggiato Then
                    funzioni.rimuovi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), id_elemento_spese, "", "EXTRA", dropTipoCommissione.SelectedValue)
                    check_spese_postali = True
                Else
                    funzioni.omaggio_accessorio(False, False, True, "", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), id_elemento_spese, "", dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, "")
                    check_spese_postali = True
                End If

                listContrattiCosti.DataBind()
            End If
        End If
    End Function

    Private Sub scegli_ditta(ByVal sender As Object, ByVal e As anagrafica_anagrafica_ditte.ScegliDittaEventArgs)
        txtNomeDitta.Text = e.ragione_sociale
        idDitta.Text = e.id_ditta
        txtCodiceEdp.Text = e.codice_edp

        check_spese_postali(e.metodo_spedizione)

        anagrafica_ditte.Visible = False
        btnScegliPrimoGuidatore.Visible = True
        btnScegliSecondoConducente.Visible = True
        btnScegliDitta.Visible = True
        btnAnnullaScegliDitta.Visible = False

        vediFattturareA.HRef = "contratto_vedi_ditta.aspx?idDitta=" & e.id_ditta
        image_fatturare_a.Visible = True

        check_possibile_pagare()
    End Sub

    Protected Sub setQueryTariffePossibili(ByVal id_prev As Integer)
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

        sqlTariffeGeneriche.SelectCommand = "(SELECT tariffe_righe.id,tariffe.codice FROM tariffe WITH(NOLOCK) " &
        "INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
        "WHERE tariffe.attiva='1' " & condizione_id_prev & " AND '" & daData & "' BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " &
        "AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a " &
        "AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_X_stazioni WITH(NOLOCK) WHERE id_tariffa=tariffe.id) " &
        "AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_X_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id) " &
        "AND '" & aData & "' <= ISNULL(max_data_rientro, '3000-12-12 23:59:59'))" 'MODIFICARE LA QUERY ANCHE IN RIBALTAMENTO.ASPX

        'PARTE 2: SELEZIONO LE TARIFFE VENDIBILI PER LA STAZIONE SPECIFICATA E PER L'EVENTUALE FONTE 
        'COME ID SELEZIONO DIRETTAMENTE LA RIGA DI tariffe_righe DA CUI POI POSSO SUBITO OTTENERE LA CONDIZIONE E IL TEMPO-KM
        'DA UTILIZZARE

        Dim condizione_nome_tariffa_fonte As String = "NULL"
        If dropTipoCliente.SelectedValue > 0 Then
            condizione_nome_tariffa_fonte = "(SELECT nome_tariffa FROM tariffe_X_fonti WHERE tariffe_x_fonti.id_tariffa=tariffe.id AND id_tipologia_cliente='" & CInt(dropTipoCliente.SelectedValue) & "')"
        End If

        sqlTariffeParticolari.SelectCommand = "SELECT tariffe_righe.id, ISNULL(" & condizione_nome_tariffa_fonte & ",tariffe.codice) As codice FROM tariffe WITH(NOLOCK) " &
        "INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
        "WHERE tariffe.attiva='1' " & condizione_id_prev & " AND '" & daData & "' BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " &
        "AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a " &
        "AND '" & aData & "' <= ISNULL(max_data_rientro, '3000-12-12 23:59:59') " &
        "AND ( (" &
        "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & CInt(dropStazionePickUp.SelectedValue) & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND " &
        "(tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & CInt(dropStazioneDropOff.SelectedValue) & "' AND tipo='DROP' AND id_tariffa=tariffe.id))) " &
        " OR " &
        "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & CInt(dropStazionePickUp.SelectedValue) & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND " &
        "(tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='DROP' AND id_tariffa=tariffe.id))) " &
        " OR " &
        "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & CInt(dropStazioneDropOff.SelectedValue) & "' AND tipo='DROP' AND id_tariffa=tariffe.id)) AND " &
        "(tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='PICK' AND id_tariffa=tariffe.id))) ) " 'MODIFICARE LA QUERY ANCHE IN RIBALTAMENTO.ASPX

        If dropTipoCliente.SelectedValue > 0 Then
            'SE E' STATA SELEZIONATA UNA FONTE CONTROLLO ANCHE TRA I REQUISITI PER FONTE DELLE TARIFFE
            'UNENDO IN OR (L'ULTIMO) LA POSSIBILITA CHE LA TARIFFA NON SPECIFICHI ALCUNA STAZIONE MA SPECIFICHI UNA O PIU' FONTI
            sqlTariffeParticolari.SelectCommand = sqlTariffeParticolari.SelectCommand & "" &
            "AND (tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tipologia_cliente='" & CInt(dropTipoCliente.SelectedValue) & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da) )" &
            "OR tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id)) " &
            "OR (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='PICK' AND id_tariffa=tariffe.id) AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='DROP' AND id_tariffa=tariffe.id) AND tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tipologia_cliente='" & CInt(dropTipoCliente.SelectedValue) & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da)) )) "
        Else
            'SE NON E' STATA SELEZIONATA UNA FONTE PRELEVO SOLAMENTE LE TARIFFE CHE NON HANNO ALCUN PREREQUISITO DI FONTE
            sqlTariffeParticolari.SelectCommand = sqlTariffeParticolari.SelectCommand & "" &
            "AND (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK))))"
        End If

        If statoContratto.Text <> "0" Then
            'SIAMO NEL CASO DI MODIFICA ADMIN CON RICHIESTA DI MODIFICA TARIFFA 
            'DO LA POSSIBILITA' DI MANTENERE LA TARIFFA DI PRENOTAZIONE OPPURE DI SCEGLIERE LA VERSIONE AGGIORNATA O UN'ALTRA TARIFFA
            Dim tar_attuale_part As String = ""
            Dim id_tar_attuale_part As String = ""

            Dim tar_attuale_gen As String = ""
            Dim id_tar_attuale_gen As String = ""

            If dropTariffeParticolari.SelectedValue <> "0" Then
                'SE C'E' PIU' DI UNA TARIFFA VUOL DIRE CHE HO GIA' EFFETTUATO UNA MODIFICA - SELEZIONO QUELLA DI PRENOTAZIONE (POTREBBE NON ESSERE
                'SELEZIONATA
                If dropTariffeParticolari.Items.Count = 2 Then
                    tar_attuale_part = dropTariffeParticolari.SelectedItem.Text.Replace(" (RA)", "") & " (RA)"
                    id_tar_attuale_part = CInt(dropTariffeParticolari.SelectedValue)
                Else
                    For i = 0 To dropTariffeParticolari.Items.Count - 1
                        If dropTariffeParticolari.Items(i).Text.Contains(" (RA)") Then
                            tar_attuale_part = dropTariffeParticolari.Items(i).Text
                            id_tar_attuale_part = dropTariffeParticolari.Items(i).Value
                        End If
                    Next
                End If
            End If

            If dropTariffeGeneriche.SelectedValue <> "0" Then
                If dropTariffeGeneriche.Items.Count = 2 Then
                    tar_attuale_gen = dropTariffeGeneriche.SelectedItem.Text.Replace(" (RA)", "") & " (RA)"
                    id_tar_attuale_gen = CInt(dropTariffeGeneriche.SelectedValue)
                Else
                    For i = 0 To dropTariffeGeneriche.Items.Count - 1
                        If dropTariffeGeneriche.Items(i).Text.Contains(" (RA)") Then
                            tar_attuale_gen = dropTariffeGeneriche.Items(i).Text
                            id_tar_attuale_gen = dropTariffeGeneriche.Items(i).Value
                        End If
                    Next
                End If
            End If

            dropTariffeGeneriche.Items.Clear()
            dropTariffeGeneriche.Items.Add("Seleziona...")
            dropTariffeGeneriche.Items(0).Value = 0
            dropTariffeParticolari.Items.Clear()
            dropTariffeParticolari.Items.Add("Seleziona...")
            dropTariffeParticolari.Items(0).Value = 0

            If tar_attuale_part <> "" Then
                dropTariffeParticolari.Items.Add(tar_attuale_part)
                dropTariffeParticolari.Items(1).Value = id_tar_attuale_part
            End If

            If tar_attuale_gen <> "" Then
                dropTariffeGeneriche.Items.Add(tar_attuale_gen)
                dropTariffeGeneriche.Items(1).Value = id_tar_attuale_gen
            End If

            dropTariffeGeneriche.DataBind()
            dropTariffeParticolari.DataBind()

            'RIMUOVO UN EVENTUALE DUPLICATO SE LA TARIFFA PRENOTATA E' ANCORA VENDIBILE E QUINDI E' PRESENTE DUE VOLTE
            If tar_attuale_part <> "" Then
                For i = 2 To dropTariffeParticolari.Items.Count - 1
                    If dropTariffeParticolari.Items(i).Value = id_tar_attuale_part Then
                        dropTariffeParticolari.Items.RemoveAt(i)
                        Exit For
                    End If
                Next
            End If

            If tar_attuale_gen <> "" Then
                For i = 2 To dropTariffeGeneriche.Items.Count - 1
                    If dropTariffeGeneriche.Items(i).Value = id_tar_attuale_gen Then
                        dropTariffeGeneriche.Items.RemoveAt(i)
                        Exit For
                    End If
                Next
            End If
        Else
            'NEL CASO DI CONTRATTO IN STATO 0
            dropTariffeGeneriche.Items.Clear()
            dropTariffeGeneriche.Items.Add("Seleziona...")
            dropTariffeGeneriche.Items(0).Value = 0

            dropTariffeParticolari.Items.Clear()
            dropTariffeParticolari.Items.Add("Seleziona...")
            dropTariffeParticolari.Items(0).Value = 0

            dropTariffeGeneriche.DataBind()
            dropTariffeParticolari.DataBind()
        End If
    End Sub

    Private Function secondo_guidatore_non_selezionato() As Boolean
        secondo_guidatore_non_selezionato = True

        For i = 0 To listContrattiCosti.Items.Count - 1
            Dim id_elemento As Label = listContrattiCosti.Items(i).FindControl("id_elemento")
            If id_elemento.Text = Costanti.Id_Secondo_Guidatore Then
                Dim chkScegli As CheckBox = listContrattiCosti.Items(i).FindControl("chkScegli")

                If chkScegli.Checked Then
                    secondo_guidatore_non_selezionato = False
                End If
            End If
        Next
    End Function

    Private Sub scegli_conduente(ByVal sender As Object, ByVal e As anagrafica_anagrafica_conducenti.ScegliConducenteEventArgs)
        If (conducente_da_variare.Text = "2" And e.id_conducente <> idPrimoConducente.Text) Or (conducente_da_variare.Text = "1" And e.id_conducente <> idSecondoConducente.Text) Then

            Dim data_di_nascita As String = e.data_nascita
            Dim old_eta As String

            If conducente_da_variare.Text = "1" Then
                If idPrimoConducente.Text <> "" Then
                    old_eta = txtEtaPrimo.Text
                Else
                    old_eta = -1
                End If
            ElseIf conducente_da_variare.Text = "2" Then
                If idSecondoConducente.Text <> "" Then
                    old_eta = txtEtaSecondo.Text
                Else
                    old_eta = "-1"
                End If
            End If

            'SE E' STATO SELEZIONATO UN UTENTE E' NECESSARIO CONTROLLARNE L'ETA' E SE DEVE ESSERE AGGIUNTO/RIMOSSO LO YOUNG DRIVER -----------
            Dim test_eta As Integer
            Dim month_nascita As Integer = Month(data_di_nascita)
            Dim day_nascita As Integer = Day(data_di_nascita)
            Dim data_nascita As DateTime = funzioni_comuni.getDataDb_senza_orario2(data_di_nascita)

            test_eta = DateDiff(DateInterval.Year, data_nascita, Now())

            If Month(Now()) < month_nascita Then
                test_eta = CInt(test_eta) - 1
            ElseIf Month(Now()) = month_nascita And Day(Now()) < day_nascita Then
                test_eta = CInt(test_eta) - 1
            End If

            'SE L'ETA' E' LA STESSA SEGNALATA AD INIZIO PROCEDURA NULLA DEVE ESSERE FATTO - ALTRIMENTI IN QUESTA FASE SI AVVERTE SOLAMENTE
            'CIRCA L'EVENTUALE MODIFICHE DA EFFETTUARE CIRCA LO YOUNG DRIVER
            If CStr(test_eta) <> old_eta Then

                'Response.Write("Id Gruppo" & CInt(gruppoDaCalcolare.SelectedValue) & "--- Eta" & test_eta)
                'Response.End()

                Dim check_eta As String = funzioni_comuni.gruppo_vendibile_eta_guidatori(CInt(gruppoDaCalcolare.SelectedValue), test_eta, "", "", "", "", "", "", "", False)
                If check_eta = "0" Then
                    'L'AUTO NON E' VENDIBILE - NON E' POSSIBILE COLLEGARE IL GUIDATORE ALLA PRENOTAZIONE DA SALVARE

                    Libreria.genUserMsgBox(Me, "Impossibile selezionare il guidatore scelto: gruppo auto non vendibile a causa dell'età.")
                ElseIf check_eta = "1" Then
                    'IN QUESTO CASO IL GRUPPO AUTO E' VENDIBILE MA CON SUPPLEMENTO YOUNG DRIVER CHE DEVE ESSERE AGGIUNTO AUTOMATICAMENTE
                    If conducente_da_variare.Text = "1" Then
                        nuovo_accessorio(get_id_young_driver(), CInt(gruppoDaCalcolare.SelectedValue), "YOUNG PRIMO", test_eta, "", "", "")
                        idPrimoConducente.Text = e.id_conducente

                        vediPrimoGuidatore.HRef = "contratto_vedi_guidatore.aspx?idUtente=" & idPrimoConducente.Text

                        image_primo_guidatore.Visible = True

                        txtNomePrimoConducente.Text = e.nome_conducente
                        txtCognomePrimoConducente.Text = e.cognome_conducente
                        txtCittaPrimoConducente.Text = e.citta_conducente
                        txtIndirizzoPrimoConducente.Text = e.indirizzo_conducente
                        txtPatentePrimoConducente.Text = e.patente_conducente & "(" & e.scadenza_patente_conducente & ")"
                        txtDocumentoPrimoConducente.Text = e.documento_conducente

                        txtEtaPrimo.Text = test_eta
                    ElseIf conducente_da_variare.Text = "2" Then
                        'SE SI AGGIUNGE IL SECONDO CONDUCENTE E' NECESSARIO SELEZIONARE L'ACCESSORIO 'SECONDO GUIDATORE' (SE GIA' NON SELEZIONATO)

                        If secondo_guidatore_non_selezionato() Then
                            aggiungi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), Costanti.Id_Secondo_Guidatore, "", "", "", False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                        End If

                        nuovo_accessorio(get_id_young_driver(), CInt(gruppoDaCalcolare.SelectedValue), "YOUNG", "", test_eta, "", "")
                        idSecondoConducente.Text = e.id_conducente

                        vediSecondoGuidatore.HRef = "contratto_vedi_guidatore.aspx?idUtente=" & idSecondoConducente.Text
                        image_secondo_guidatore.Visible = True

                        txtNomeSecondoConducente.Text = e.nome_conducente
                        txtCognomeSecondoConducente.Text = e.cognome_conducente
                        txtCittaSecondaConducente.Text = e.citta_conducente
                        txtIndirizzoSecondoConducente.Text = e.indirizzo_conducente
                        txtPatenteSecondoConducente.Text = e.patente_conducente & "(" & e.scadenza_patente_conducente & ")"
                        txtDocumentoSecondoConducente.Text = e.documento_conducente

                        txtEtaSecondo.Text = test_eta
                    End If
                    listContrattiCosti.DataBind()

                    aggiorna_informazioni_dopo_modifica_costi()

                    Libreria.genUserMsgBox(Me, "Gruppo auto vendibile con supplemento Young Driver.")
                ElseIf check_eta = "4" Then
                    'VENDIBILE SENZA LO YOUNG DRIVER - QUALORA FOSSE STATO PRECEDENTEMENTE AGGIUNTO PROVVEDO ALLA SUA RIMOZIONE
                    If conducente_da_variare.Text = "1" Then
                        If funzioni.esiste_young_driver_primo_guidatore(get_id_young_driver(), CInt(gruppoDaCalcolare.SelectedValue), numCalcolo.Text, "", "", idContratto.Text, "") Then
                            funzioni.rimuovi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), get_id_young_driver(), "1", "EXTRA", dropTipoCommissione.SelectedValue)
                            Libreria.genUserMsgBox(Me, "Rimosso lo Young driver per il primo guidatore.")
                        End If
                        idPrimoConducente.Text = e.id_conducente

                        vediPrimoGuidatore.HRef = "contratto_vedi_guidatore.aspx?idUtente=" & idPrimoConducente.Text
                        image_primo_guidatore.Visible = True

                        txtNomePrimoConducente.Text = e.nome_conducente
                        txtCognomePrimoConducente.Text = e.cognome_conducente
                        txtCittaPrimoConducente.Text = e.citta_conducente
                        txtIndirizzoPrimoConducente.Text = e.indirizzo_conducente
                        txtPatentePrimoConducente.Text = e.patente_conducente & "(" & e.scadenza_patente_conducente & ")"
                        txtDocumentoPrimoConducente.Text = e.documento_conducente

                        txtEtaPrimo.Text = test_eta
                    ElseIf conducente_da_variare.Text = "2" Then
                        'SE SI AGGIUNGE IL SECONDO CONDUCENTE E' NECESSARIO SELEZIONARE L'ACCESSORIO 'SECONDO GUIDATORE' (SE GIA' NON SELEZIONATO)
                        If secondo_guidatore_non_selezionato() Then
                            aggiungi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), Costanti.Id_Secondo_Guidatore, "", "", "", False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                        End If

                        If funzioni.esiste_young_driver_secondo_guidatore(get_id_young_driver(), CInt(gruppoDaCalcolare.SelectedValue), numCalcolo.Text, "", "", idContratto.Text, "") Then
                            funzioni.rimuovi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), get_id_young_driver(), "2", "EXTRA", dropTipoCommissione.SelectedValue)
                        End If

                        idSecondoConducente.Text = e.id_conducente

                        vediSecondoGuidatore.HRef = "contratto_vedi_guidatore.aspx?idUtente=" & idSecondoConducente.Text
                        image_secondo_guidatore.Visible = True

                        txtNomeSecondoConducente.Text = e.nome_conducente
                        txtCognomeSecondoConducente.Text = e.cognome_conducente
                        txtCittaSecondaConducente.Text = e.citta_conducente
                        txtIndirizzoSecondoConducente.Text = e.indirizzo_conducente
                        txtPatenteSecondoConducente.Text = e.patente_conducente & "(" & e.scadenza_patente_conducente & ")"
                        txtDocumentoSecondoConducente.Text = e.documento_conducente

                        txtEtaSecondo.Text = test_eta
                    End If
                    listContrattiCosti.DataBind()
                    aggiorna_informazioni_dopo_modifica_costi()
                End If
            Else
                'IN QUESTO CASO E' STATO RISELEZIONATO LO STESSO CONDUCENTE E L'ETA' NON E' VARIATA (ANCHE NEL CASO IN CUI L'ETA' SAREBBE
                'VARIABILE) 
                If conducente_da_variare.Text = "1" Then
                    idPrimoConducente.Text = e.id_conducente

                    vediPrimoGuidatore.HRef = "contratto_vedi_guidatore.aspx?idUtente=" & idPrimoConducente.Text
                    image_primo_guidatore.Visible = True

                    txtNomePrimoConducente.Text = e.nome_conducente
                    txtCognomePrimoConducente.Text = e.cognome_conducente
                    txtCittaPrimoConducente.Text = e.citta_conducente
                    txtIndirizzoPrimoConducente.Text = e.indirizzo_conducente
                    txtPatentePrimoConducente.Text = e.patente_conducente & "(" & e.scadenza_patente_conducente & ")"
                    txtDocumentoPrimoConducente.Text = e.documento_conducente
                ElseIf conducente_da_variare.Text = "2" Then
                    'SE SI AGGIUNGE IL SECONDO CONDUCENTE E' NECESSARIO SELEZIONARE L'ACCESSORIO 'SECONDO GUIDATORE' (SE GIA' NON SELEZIONATO)
                    idSecondoConducente.Text = e.id_conducente

                    vediSecondoGuidatore.HRef = "contratto_vedi_guidatore.aspx?idUtente=" & idSecondoConducente.Text
                    image_secondo_guidatore.Visible = True

                    txtNomeSecondoConducente.Text = e.nome_conducente
                    txtCognomeSecondoConducente.Text = e.cognome_conducente
                    txtCittaSecondaConducente.Text = e.citta_conducente
                    txtIndirizzoSecondoConducente.Text = e.indirizzo_conducente
                    txtPatenteSecondoConducente.Text = e.patente_conducente & "(" & e.scadenza_patente_conducente & ")"
                    txtDocumentoSecondoConducente.Text = e.documento_conducente
                End If
            End If
            '---------------------------------------------------------------------------------------------------------------------------------

            anagrafica_conducenti1.Visible = False
            btnScegliPrimoGuidatore.Visible = True
            btnScegliSecondoConducente.Visible = True
            If ditta_non_modificabile.Text = "False" Then
                btnScegliDitta.Visible = True
            End If
            btnAnnullaScegliPrimoConducente.Visible = False
            btnAnnullaScegliSecondoConducente.Visible = False

            conducente_da_variare.Text = ""
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: è stato selezionato un guidatore già collegato al contratto.")
        End If

        check_possibile_pagare()
    End Sub

    Private Function gps_selezionato() As Boolean
        gps_selezionato = False

        For i = 0 To listContrattiCosti.Items.Count - 1
            Dim is_gps As Label = listContrattiCosti.Items(i).FindControl("is_gps")
            If is_gps.Text = "True" Then
                Dim chkScegli As CheckBox = listContrattiCosti.Items(i).FindControl("chkScegli")

                If chkScegli.Checked Then
                    gps_selezionato = True
                End If
            End If
        Next
    End Function

    Protected Sub nuovoContrattoDaPreventivo(ByVal id_contratto As String, ByVal id_preventivo As String)
        'PROVENENDO DA PRENOTAZIONI SI DEVE CARICARE QUANTO IVI SALVATO IN CONTRATTI ED AGGIUNGERE I DETTAGLI SALVATI IN PRENOTAZIONE
        '(NOTE - DETTAGLI DI VOLO - UTENTE - DITTA)
        listWarning.DataBind()
        numCalcolo.Text = "0"
        'STATO 0: CONTRATTO IN COMPILAZIONE/NON SALVATO
        'STATO 2: CONTRATTO APERTO (NOLEGGIO IN CORSO)

        statoContratto.Text = "0"
        dettagli_da_prenotazione.Visible = False
        dettagli_da_preventivo.Visible = True
        tab_dettagli_pagamento.Visible = False

        caricaContratto(id_contratto, "", id_preventivo)

        listContrattiCosti.DataBind()

        'SE TRA GLI ACCESSORI IL GPS E' SELEZIONATO ALLORA ABILITO LA SEZIONE PER LA SCELTA DEL GPS
        If gps_selezionato() Then
            div_gps.Visible = True
        End If

        'RICALCOLO DEI WARNING - 
        controlla_warning()

        'VISUALIZZO IL TOTALE DA PREAUTORIZZARE
        getTotaleDaPreautorizzare()
    End Sub

    Protected Sub nuovoContrattoDaPrenotazione(ByVal id_contratto As String, ByVal id_prenotazione As String)
        'PROVENENDO DA PRENOTAZIONI SI DEVE CARICARE QUANTO IVI SALVATO IN CONTRATTI ED AGGIUNGERE I DETTAGLI SALVATI IN PRENOTAZIONE
        '(NOTE - DETTAGLI DI VOLO - UTENTE - DITTA)
        listWarning.DataBind()
        numCalcolo.Text = "0"
        'STATO 0: CONTRATTO IN COMPILAZIONE/NON SALVATO
        'STATO 1: CONTRATTO APERTO (NOLEGGIO IN CORSO)

        statoContratto.Text = "0"
        dettagli_da_prenotazione.Visible = True
        dettagli_da_preventivo.Visible = False

        caricaContratto(id_contratto, id_prenotazione, "")
        listContrattiCosti.DataBind()

        'SE TRA GLI ACCESSORI IL GPS E' SELEZIONATO ALLORA ABILITO LA SEZIONE PER LA SCELTA DEL GPS
        If gps_selezionato() Then
            div_gps.Visible = True
        End If

        'SE LA TARGA E' STATA PRECARICATA (GRUPPO SPECIALE) PROVO A COLLEGARLA AL CONTRATTO
        If txtTarga.Text <> "" Then
            btn_ScegliTarga()
        End If

        'RICALCOLO DEI WARNING - 
        controlla_warning()

        'VISUALIZZO IL TOTALE DA PREAUTORIZZARE
        getTotaleDaPreautorizzare()

        If prenotazione_prepagata.Text = "True" Then
            lblPrepagata1.Visible = True
            'lblPrepagata2.Visible = True
            'lblEuroDaIncassare.Visible = True
            'lblDaIncassare.Visible = True
            'lblDaIncassare.Text = getTotaleDaPagare()
            'lblDaIncassare.Text = FormatNumber(lblDaIncassare.Text, 2, , , TriState.False)
        End If
    End Sub

    Protected Sub caricaContratto(ByVal id_contratto As String, ByVal id_prenotazione As String, ByVal id_preventivo As String)
        'SIA CHE SI VENGA DA PRENOTAZIONI CHE DA PREVENTIVI, SIA SALVATO CHE NON, ESISTE GIA' UNA RIGA (NELLA TABELLA DEI CONTRATTI) 
        'NON ATTIVA CON QUANTO SERVE PER CARICARE I DATI DEL NUOVO CONTRATTO
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT alimentazione.descrizione As alimentazione, alimentazione.cod_carb, alimentazione.id As id_Serb, * FROM contratti WITH(NOLOCK) LEFT JOIN alimentazione WITH(NOLOCK) ON contratti.id_alimentazione=alimentazione.id WHERE contratti.id='" & id_contratto & "'", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()
        Rs.Read()

        'FACCIO IN MODO CHE SIA NEL CASO IN CUI SI CREI UN NUOVO CONTRATTO DA PRENOTAZIONE CHE NEL CASO IN CUI SI CARICHI UN CONTRATTO 
        'SALVATO CHE PROVENIVA DE PRENOTAZIONE, DENTRO LA VARIABILE idPrenotazione TROVO L'ID CORRISPONDENTE
        idPrenotazione.Text = id_prenotazione 'NEL CASO IN CUI SI CARICA UN CONTRATTO QUESTA VARIABILE SARA' VUOTA E VERRA' CARICATA DOPO
        'MENTRE NEL CASO DI NUOVO CONTRATTO DA PRENTOAZIONE SARA' VALORIZZATA IN QUESTO MOMENTO

        Dim contratto_trovato As Boolean = False
        Dim fatt_da_controllare As String
        Dim non_fatturare As Boolean

        If Rs.HasRows() Then
            prenotazione_prepagata.Text = Rs("prenotazione_prepagata") & ""
            If (Rs("giorni_prepagati") & "") <> "" Then
                'lblPrepagata2.Text = lblPrepagata1.Text & " - GG: " & Rs("giorni_prepagati")

                lblGiorniPrepagati.Visible = True
                txtGiorniPrepagati.Visible = True
                txtGiorniPrepagati.Text = Rs("giorni_prepagati")
            End If

            contratto_trovato = True

            If (Rs("fatturazione_da_controllare") & "") <> "" Then
                If Rs("fatturazione_da_controllare") Then
                    fatt_da_controllare = "1"
                Else
                    fatt_da_controllare = "0"
                End If
            Else
                fatt_da_controllare = "0"
            End If

            If (Rs("non_fatturare") & "") <> "" Then
                If Rs("non_fatturare") Then
                    non_fatturare = True
                Else
                    non_fatturare = False
                End If
            Else
                non_fatturare = False
            End If

            If (Rs("id_fonte_commissionabile") & "") <> "" Then
                riga_commissioni.Visible = True
                dropFonteCommissionabile.SelectedValue = Rs("id_fonte_commissionabile")
                dropTipoCommissione.SelectedValue = Rs("tipo_commissione")
                txtPercentualeCommissionabile.Text = Rs("commissione_percentuale")
                lblGGcommissioniOriginali.Text = Rs("giorni_commissioni")
                riga_commissioni.Visible = True
                dropTipoCommissione.Enabled = False
                dropFonteCommissionabile.Enabled = False
            Else
                txtPercentualeCommissionabile.Text = ""
                lblGGcommissioniOriginali.Text = ""
                dropTipoCommissione.SelectedValue = "0"
            End If

            If id_prenotazione = "" And id_preventivo = "" Then
                'IN QUESTO CASO E' STATO RICHIESTO IL CARICAMENTO DI UN CONTRATTO SALVATO - RECUPERO LE INFORMAZIONI UTILI AL SUO CARICAMENTO
                statoContratto.Text = Rs("status")
                statoModificaContratto.Text = "0"
                numCalcolo.Text = Rs("num_calcolo")

                numCrv.Text = Rs("num_crv") & ""
                If numCrv.Text <> "0" Then
                    lblCrv.Text = " - CRV " & numCrv.Text
                End If

                lblNumContratto.Text = Rs("num_contratto")

                contratto_num.Text = Rs("num_contratto")

                lblDataContratto.Text = Rs("data_creazione")
                lblOperatoreCreazione.Text = funzioni_comuni.getNomeOperatore(Rs("id_operatore_creazione"))

                If Rs("tariffa_rack_utilizzata") Then
                    rack_utilizzata.Text = "1"
                Else
                    rack_utilizzata.Text = "0"
                End If

                If (Rs("num_prenotazione") & "") <> "" Then
                    dettagli_da_prenotazione.Visible = True
                    lblNumPren.Text = Rs("num_prenotazione")

                    lblNumPren.Visible = True
                    lblDaPrenotazione.Visible = True

                    idPrenotazione.Text = getIdPrenotazione(Rs("num_prenotazione"))
                    prenotazione_prepagata.Text = Rs("prenotazione_prepagata") & ""

                    If Pagamenti.is_full_credit(Rs("num_prenotazione"), Rs("num_contratto")) Then
                        full_credit.Text = "1"
                        lblFullCredit.Visible = True
                    End If
                    If Pagamenti.is_complimentary(Rs("num_prenotazione"), Rs("num_contratto")) Then
                        complimentary.Text = "1"
                        lblComplimentary.Visible = True
                    End If
                Else
                    If Pagamenti.is_full_credit("", Rs("num_contratto")) Then
                        full_credit.Text = "1"
                        lblFullCredit.Visible = True
                    End If
                    If Pagamenti.is_complimentary("", Rs("num_contratto")) Then
                        complimentary.Text = "1"
                        lblComplimentary.Visible = True
                    End If

                    dettagli_da_prenotazione.Visible = False
                    lblNumPren.Text = "0"
                End If
                dettagli_da_preventivo.Visible = False

                txtNoteContratto.Text = Rs("Note_contratto") & ""
                txtNoteContratto.ReadOnly = True

                'GPS - CON CONTRATTO SALVATO
                If (Rs("id_gps") & "") <> "" Then
                    div_gps.Visible = True

                    lblIdGps.Text = Rs("id_gps")
                    txtCodiceGps.Text = Rs("codice_gps")
                    txtCodiceGps.Enabled = False
                End If
            ElseIf id_prenotazione <> "" Then
                If Pagamenti.is_full_credit(Rs("num_prenotazione"), "") Then
                    full_credit.Text = "1"
                    lblFullCredit.Visible = True
                End If
                If Pagamenti.is_complimentary(Rs("num_prenotazione"), "") Then
                    complimentary.Text = "1"
                    lblComplimentary.Visible = True
                End If
            End If

            Try
                dropStazionePickUp.SelectedValue = Rs("id_stazione_uscita")
            Catch ex As Exception
                'QUALORA LA STAZIONE NON ESISTE PERCHE' NON PIU' ATTIVA -
                dropStazionePickUp.SelectedIndex = "0"
            End Try

            Try
                If statoContratto.Text = "0" Or statoContratto.Text = "1" Or statoContratto.Text = "2" Or statoContratto.Text = "5" Or (statoContratto.Text = "7" And (Rs("data_rientro") & "") = "") Then
                    dropStazioneDropOff.SelectedValue = Rs("id_stazione_presunto_rientro")
                Else
                    dropStazioneRientroPresunto.SelectedValue = Rs("id_stazione_presunto_rientro")
                    dropStazioneDropOff.SelectedValue = Rs("id_stazione_rientro")

                    If statoContratto.Text = "7" Then
                        'NE APPROFITTO PER ABILITARE ALCUNI CAMPI CHE DEVONO ESSERLO QUANDO IL CONTRATTO E' STATO ANNULLATO DOPO AVER FATTO
                        'USCIRE IL VEICOLO
                        riga_rientro_veicolo.Visible = True
                        btnSalvaRientro.Visible = True
                        riga_rientro.Visible = True
                    End If
                End If

                If statoContratto.Text = "0" Then
                    id_stazione_drop_off_prenotazione.Text = Rs("id_stazione_presunto_rientro")
                Else
                    id_stazione_drop_off_prenotazione.Text = Rs("id_stazione_presunto_rientro_originale")
                End If
            Catch ex As Exception
                'QUALORA LA STAZIONE NON ESISTE PERCHE' NON PIU' ATTIVA -
                dropStazioneDropOff.SelectedIndex = "0"
            End Try

            dropTipoCliente.SelectedValue = Rs("id_fonte")
            txtNumeroGiorni.Text = Rs("giorni") & ""

            If (Rs("giorno_extra_abbuonato") & "") <> "" Then
                chkAbbuonaGiornoExtra.Checked = Rs("giorno_extra_abbuonato")
            End If

            If statoContratto.Text = "0" Then
                'SALVO IL NUMERO DI GIORNI INIZIALI IN UNA VARIABILE NASCOSTA - SERVE IN CASO DI MODIFICA DEI GIORNI DI NOLEGGIO
                'QUESTO SE E' NUOVO CONTRATTO
                txtNumeroGiorniIniziali.Text = Rs("giorni") & ""
            Else
                'SE INVECE STO CARICANDO UN CONTRATTO PREESISTENTE 
                txtNumeroGiorniIniziali.Text = Rs("giorni_originale")
            End If

            gruppoDaCalcolare.SelectedValue = Rs("id_gruppo_auto")

            If statoContratto.Text = "0" Then
                'MEMORIZZO IL GRUPPO DA PRENOTAZIONE PER IL COMPORTAMENTO "MODIFICA GRUPPO" CON TARIFFA NON PIU' VENDIBILE
                gruppo_da_calcolare_originale.Text = Rs("id_gruppo_auto")
            Else
                'CARICO IL GRUPPO ORIGINALE (DA PRENOTAZIONE OPPURE SALVATO) - NEL CASO IN CUI IL CONTRATTO SIA IN STATO "1" (SALVATO MA NON
                'PAGATO) - ALTRIMENTI IL GRUPPO NON E' COMUNQUE MODIFICIABILE
                gruppo_da_calcolare_originale.Text = Rs("id_gruppo_auto_originale")
            End If

            If ((Rs("id_gruppo_app") & "") = "") Then
                gruppoDaConsegnare.SelectedValue = "0"
            ElseIf (Rs("id_gruppo_app") = Rs("id_gruppo_auto")) Then
                gruppoDaConsegnare.SelectedValue = "0"
            Else
                gruppoDaConsegnare.SelectedValue = Rs("id_gruppo_app")
            End If

            gruppoDaCalcolare.Enabled = False
            gruppoDaConsegnare.Enabled = False

            txtDaData.Text = Day(Rs("data_uscita")) & "/" & Month(Rs("data_uscita")) & "/" & Year(Rs("data_uscita"))

            If statoContratto.Text = "0" Or statoContratto.Text = "1" Or statoContratto.Text = "2" Or statoContratto.Text = "5" Or (statoContratto.Text = "7" And (Rs("data_rientro") & "") = "") Then
                txtAData.Text = Day(Rs("data_presunto_rientro")) & "/" & Month(Rs("data_presunto_rientro")) & "/" & Year(Rs("data_presunto_rientro"))
                ore2.Text = Hour(Rs("data_presunto_rientro"))
                minuti2.Text = Minute(Rs("data_presunto_rientro"))
            Else
                txtAData.Text = Day(Rs("data_rientro")) & "/" & Month(Rs("data_rientro")) & "/" & Year(Rs("data_rientro"))
                txtADataPresunto.Text = Day(Rs("data_presunto_rientro")) & "/" & Month(Rs("data_presunto_rientro")) & "/" & Year(Rs("data_presunto_rientro"))
                ore2.Text = Hour(Rs("data_rientro"))
                minuti2.Text = Minute(Rs("data_rientro"))
                ore2_presunto.Text = Hour(Rs("data_presunto_rientro"))
                minuti2_presunto.Text = Minute(Rs("data_presunto_rientro"))
            End If

            'MEMORIZZO IN UNA LABEL NASCOSTA IL VALORE ORIGINALE DI DATA DI CONSEGNA PER SAPERE SE UN'EVENTUALE VARIAZIONE RISPETTO LA PRENOTAZIONE
            'AVVIENE ALL'INTERNO DELLA DATA DI DROP OFF; STESSA COSA PER IL PICK UP

            If statoContratto.Text = "0" Then
                txtDADataOld.Text = txtDaData.Text
                txtADataOld.Text = txtAData.Text
            Else
                txtDADataOld.Text = Day(Rs("data_uscita_originale")) & "/" & Month(Rs("data_uscita_originale")) & "/" & Year(Rs("data_uscita_originale"))
                txtADataOld.Text = Day(Rs("data_presunto_rientro_originale")) & "/" & Month(Rs("data_presunto_rientro_originale")) & "/" & Year(Rs("data_presunto_rientro_originale"))
            End If
            'txtDADataOld.Text = IIf(Len(CStr(Day(Rs("data_uscita")))) = 1, "0" & Day(Rs("data_uscita")), Day(Rs("data_uscita"))) & "/" & IIf(Len(CStr(Month(Rs("data_uscita")))) = 1, "0" & Month(Rs("data_uscita")), Month(Rs("data_uscita"))) & "/" & Year(Rs("data_uscita"))
            'txtADataOld.Text = IIf(Len(CStr(Day(Rs("data_presunto_rientro")))) = 1, "0" & Day(Rs("data_presunto_rientro")), Day(Rs("data_presunto_rientro"))) & "/" & IIf(Len(CStr(Month(Rs("data_presunto_rientro")))) = 1, "0" & Month(Rs("data_presunto_rientro")), Month(Rs("data_presunto_rientro"))) & "/" & Year(Rs("data_presunto_rientro"))

            ore1.Text = Hour(Rs("data_uscita"))
            minuti1.Text = Minute(Rs("data_uscita"))

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

            If txtADataPresunto.Text <> "" Then
                If Len(ore2_presunto.Text) = 1 Then
                    ore2_presunto.Text = "0" & ore2_presunto.Text
                End If
                If Len(minuti2_presunto.Text) = 1 Then
                    minuti2_presunto.Text = "0" & minuti2_presunto.Text
                End If

                txtOraRientroPresunta.Text = ore2_presunto.Text & ":" & minuti2_presunto.Text
            End If

            txtoraPartenza.Text = ore1.Text & ":" & minuti1.Text
            txtOraRientro.Text = ore2.Text & ":" & minuti2.Text

            txtEtaPrimo.Text = Rs("eta_primo_guidatore")
            txtEtaSecondo.Text = Rs("eta_secondo_guidatore")

            dropStazionePickUp.Enabled = False
            dropStazioneDropOff.Enabled = False
            txtDaData.Enabled = False
            txtAData.Enabled = False

            txtNumeroGiorni.Enabled = False
            txtoraPartenza.Enabled = False
            txtOraRientro.Enabled = False
            ore1.Enabled = False
            minuti1.Enabled = False
            ore2.Enabled = False
            minuti2.Enabled = False
            dropTipoCliente.Enabled = False

            If statoContratto.Text = "0" Then
                'NUOVO CONTRATTO
                'DITTA-----------------------------------------------------------------------------------------------------------------
                If (Rs("codice_edp") & "") <> "" Then
                    txtCodiceEdp.Text = Rs("codice_edp")
                    'NEL CASO IN CUI E' STATO SPECIFICATO IL CODICE EDP LA DITTA E' PER FORZA QUELLA COLLEGATA AD ESSO E NON E' MODIFICABILE
                    idDitta.Text = Rs("id_cliente") & ""

                    vediFattturareA.HRef = "contratto_vedi_ditta.aspx?idDitta=" & Rs("id_cliente")
                    txtNomeDitta.Text = getNomeDittaFromEdp(Rs("codice_edp"))

                    ditta_non_modificabile.Text = "True"
                    btnScegliDitta.Visible = False
                ElseIf (Rs("id_cliente") & "") <> "" Then
                    'E' STATA SPECIFICATA UNA DITTA A CUI FATTURARE
                    idDitta.Text = Rs("id_cliente")
                    txtNomeDitta.Text = getNomeDittaFromId(idDitta.Text)
                    txtCodiceEdp.Text = getCodiceEdp(idDitta.Text)
                    vediFattturareA.HRef = "contratto_vedi_ditta.aspx?idDitta=" & Rs("id_cliente")
                    ditta_non_modificabile.Text = "False"
                Else
                    'SE NON E' STATA SPECIFICATA NESSUNA DITTA SELEZIONO PER DEFAULT LA CASH
                    idDitta.Text = funzioni_comuni.id_cliente_cash()
                    If idDitta.Text <> "0" Then
                        txtNomeDitta.Text = "CLIENTE CASH"
                        txtCodiceEdp.Text = Costanti.codice_cash
                    Else
                        idDitta.Text = ""
                    End If

                    btnScegliDitta.Visible = True

                    ditta_non_modificabile.Text = "False"
                    image_fatturare_a.Visible = False
                End If
                '------------------------------------------------------------
                If id_prenotazione <> "" Then
                    'SE STO EFFETTUANDO UN NUOVO CONTRATTO DA PRENOTAZIONE INIZIALIZZO L'EVENTUALE VETTURA DI GRUPPO SPECIALE SPECIFICATA IN FASE DI PRENOTAZIONE
                    If (Rs("targa_gruppo_speciale_pren") & "") <> "" Then
                        txtTarga.Text = Rs("targa_gruppo_speciale_pren")
                    End If
                End If
            Else
                'CONTRATTO SALVATO DA CARICARE
                txtCodiceEdp.Text = Rs("codice_edp")
                idDitta.Text = Rs("id_cliente") & ""
                If idDitta.Text <> "" Then
                    txtNomeDitta.Text = getNomeDittaFromId(idDitta.Text)
                    ditta_non_modificabile.Text = "True"
                    btnScegliDitta.Visible = False
                    vediFattturareA.HRef = "contratto_vedi_ditta.aspx?num_contratto=" & Rs("num_contratto") & "&idDitta=" & Rs("id_cliente")
                End If
            End If

            'TARIFFA - SCONTO -----------------------------------------------------------------------------------------------------------
            txtSconto.Text = Rs("sconto_applicato")
            If (Rs("tipo_sconto") & "") <> "" Then
                dropTipoSconto.SelectedValue = Rs("tipo_sconto")
            End If

            txtScontoRack.Text = Rs("sconto_su_rack") & ""

            txtSconto.Enabled = False
            dropTipoSconto.Enabled = False
            txtScontoRack.Enabled = False

            Dim id_tariffe_righe As String = ""

            If (id_prenotazione <> "") Or (id_prenotazione = "" And id_preventivo = "") Then
                'SE SI STA CREANDO UN CONTRATTO PARTENDO DA UNA PRENOTAZIONE OPPURE SE SI STA CARICANDO UN CONTRATTO GIA' SALVATO
                'SI IMPOSTA LA TARIFFA SCELTA AL MOMENTO DELLA PRENOTAZIONE O COMUNQUE QUELLA SALVATA SU RIGA DI CONTRATTO
                If (Rs("tipo_tariffa") & "") = "generica" Then
                    dropTariffeGeneriche.Items.Add(Rs("codtar"))
                    dropTariffeGeneriche.Items(1).Value = Rs("id_tariffe_righe")

                    dropTariffeGeneriche.Items(0).Selected = False
                    dropTariffeGeneriche.Items(1).Selected = True
                    dropTariffeGeneriche.Enabled = False
                    dropTariffeParticolari.Enabled = False

                    id_tariffe_righe = Rs("id_tariffe_righe")
                ElseIf (Rs("tipo_tariffa") & "") = "fonte" Then
                    dropTariffeParticolari.Items.Add(Rs("codtar"))
                    dropTariffeParticolari.Items(1).Value = Rs("id_tariffe_righe")

                    dropTariffeParticolari.Items(0).Selected = False
                    dropTariffeParticolari.Items(1).Selected = True
                    dropTariffeParticolari.Enabled = False
                    dropTariffeGeneriche.Enabled = False

                    id_tariffe_righe = Rs("id_tariffe_righe")
                End If
            ElseIf provenienza.Text = "preventivi.aspx" Then
                'SE PROVENGO DA PREVENTIVI, INVECE, E' POSSIBILE MODIFICARE LA TARIFFA ASSOCIATA (LA TARIFFA E' CERTAMENTE VENDIBILE)
                setQueryTariffePossibili(0)

                If (Rs("tipo_tariffa") & "") = "generica" Then
                    dropTariffeGeneriche.SelectedValue = Rs("id_tariffe_righe")
                    dropTariffeGeneriche.Enabled = False
                    dropTariffeParticolari.Enabled = False

                    id_tariffe_righe = Rs("id_tariffe_righe")
                ElseIf (Rs("tipo_tariffa") & "") = "fonte" Then
                    dropTariffeParticolari.SelectedValue = Rs("id_tariffe_righe")
                    id_tariffe_righe = Rs("id_tariffe_righe")
                    dropTariffeGeneriche.Enabled = False
                    dropTariffeParticolari.Enabled = False
                End If
            End If

            'IMPOSTO L'EVENTUALE INFORMAZIONE DEI MINIMI GIORNI DI NOLEGGIO
            Dim min_giorni_nolo As String = funzioni_comuni.getMinGiorniNolo(id_tariffe_righe)
            If min_giorni_nolo <> "-1" Then
                lblMinGiorniNolo.Text = "La tariffa prevede un minimo di " & min_giorni_nolo & " giorni/o di noleggio"
            Else
                lblMinGiorniNolo.Text = ""
            End If

            'CONTROLLO SE LA TARIFFA E' DI TIPO BROKER
            If (Rs("importo_a_carico_del_broker") & "") <> "" Then
                'SE QUESTO VALORE E' VALORIZZATO LA TARIFFA E' BROKER, ALTRIMENTO NON LO E', QUESTO IN QUANTO
                'NON E' POSSIIBLE TRASFORMARE UNA TARIFFA DA BROKER A NON BROKER SE E' GIA' STATA UTILIZZATA
                tariffa_broker.Text = "1"
                a_carico_del_broker.Text = Rs("importo_a_carico_del_broker")
                txtNumeroGiorniTO.Text = Rs("giorni_to") & ""
                lblGiorniToOld.Text = Rs("giorni_to") & ""
            Else
                tariffa_broker.Text = "0"
                lblGiorniTO.Visible = False
                txtNumeroGiorniTO.Visible = False
                a_carico_del_broker.Text = ""
            End If

            If statoContratto.Text = "0" Or statoContratto.Text = "1" Then
                sqlElementiExtra.SelectCommand = funzioni.getQueryElementiExtra(id_tariffe_righe, False)
                dropElementiExtra.Items.Clear()
                dropElementiExtra.Items.Add("Seleziona...")
                dropElementiExtra.Items(0).Value = "0"
                dropElementiExtra.DataBind()
            ElseIf statoContratto.Text = "2" Or statoContratto.Text = "5" Then
                'PER UN CONTRATTO IN CORSO SETTO GLI ELEMENTI EXTRA VENDIBILI A NOLO IN CORSO
                sqlElementiExtra.SelectCommand = funzioni.getQueryElementiExtra(id_tariffe_righe, True)
                dropElementiExtra.Items.Clear()
                dropElementiExtra.Items.Add("Seleziona...")
                dropElementiExtra.Items(0).Value = "0"
                dropElementiExtra.DataBind()
            End If

            '------------------------------------------------------------------------------------------------------------------------------
            'SE E' STATO EFFETTUATA ALMENO UN'OPERAZIONE POS (E PROVENGO DA PRENOTAZIONI O CON CONTRATTO SALVATO) 
            'MOSTRO IL TOTALE E LA SEZIONE DI PAGAMENTO E IMPOSTO LE INFORMAZIONI RIGUARDANTI IL TOTALE DA PRENOTAZIONE E LE INFORMAZIONI DI PREPAGAMENTO
            If id_prenotazione <> "" Or statoContratto.Text <> "0" Then
                listPagamenti.Visible = True
                listPagamenti.DataBind()

                ImpostaPannelloPagamento(Rs("num_prenotazione") & "", Rs("num_contratto") & "")

                totale_prenotazione.Text = Rs("totale_costo_prenotazione") & ""
            End If
            '--------------------------------------------------------------------------------------------------------------

            If (Rs("id_primo_conducente") & "") <> "" Then
                idPrimoConducente.Text = Rs("id_primo_conducente")
            End If
            If (Rs("id_secondo_conducente") & "") <> "" Then
                idSecondoConducente.Text = Rs("id_secondo_conducente")
            End If

            If (Rs("id_veicolo") & "") <> "" Then
                'IL VEICOLO SARA' VALORIZZATO SOLO NEL CASO DI CONTRATTO IN CARICAMENTO (TRANNE NELLO STATUS "5" CRV - ATTESA SOSTITUAZIONE) O NEL CASO DI VEICOLO ASSEGNATO IN FASE DI PRENOTAZIONE
                '(MEZZI SPECIALI)
                id_auto_selezionata.Text = Rs("id_veicolo")
                id_alimentazione.Text = Rs("id_alimentazione")
                lblTipoSerbatoio.Text = Rs("cod_carb") & ""
                lblTipoSerbatoio.ToolTip = Rs("alimentazione") & ""
                txtTarga.Text = Rs("targa")
                txtKm.Text = Rs("km_uscita")
                txtSerbatoio.Text = Rs("litri_uscita")
                lblSerbatoioMax.Text = Rs("serbatoio_max")
                lblSerbatoioMaxRientro.Text = Rs("serbatoio_max")
                txtModello.Text = Rs("modello")

                If gruppoDaConsegnare.SelectedValue <> "0" Then
                    txtGruppo.Text = gruppoDaConsegnare.SelectedItem.Text
                    id_gruppo_auto_selezionata.Text = CInt(gruppoDaConsegnare.SelectedValue)
                Else
                    txtGruppo.Text = gruppoDaCalcolare.SelectedItem.Text
                    id_gruppo_auto_selezionata.Text = CInt(gruppoDaCalcolare.SelectedValue)
                End If

                txtTarga.ReadOnly = True
                btnScegliTarga.Text = "Modifica"

                If statoContratto.Text = "4" Or statoContratto.Text = "6" Or statoContratto.Text = "8" Or (statoContratto.Text = "7" And riga_rientro_veicolo.Visible) Then
                    'A CONTRATTO CHIUSO MOSTRO I DATI DI RIENTRO
                    txtKmRientro.Text = Rs("km_rientro")
                    txtSerbatoioRientro.Text = Rs("litri_rientro")
                End If
            ElseIf statoContratto.Text = "5" Then
                'NEL CASO DI STATO "CRV - ATTESA SOSTITUZIONE" NELLA RIGA NON HO LE INFORMAZIONI DEL VEICOLO MA HO LE INFORMAZIONI DEL GRUPPO
                id_auto_selezionata.Text = ""
                id_alimentazione.Text = ""
                lblTipoSerbatoio.Text = ""
                txtTarga.Text = ""
                txtKm.Text = ""
                txtSerbatoio.Text = ""
                lblSerbatoioMax.Text = ""
                lblSerbatoioMaxRientro.Text = ""
                txtModello.Text = ""
                txtGruppo.Text = ""

                If gruppoDaConsegnare.SelectedValue <> "0" Then
                    id_gruppo_auto_selezionata.Text = CInt(gruppoDaConsegnare.SelectedValue)
                Else
                    id_gruppo_auto_selezionata.Text = CInt(gruppoDaCalcolare.SelectedValue)
                End If

                txtTarga.ReadOnly = False
                btnScegliTarga.Text = "Seleziona"
            End If
        Else
            Libreria.genUserMsgBox(Me, "Si è verificato un errore. Impossibile richiamare il contratto")
        End If

        Dbc.Close()
        Dbc.Open()

        If contratto_trovato Then
            'DETTAGLI DA PRENOTAZIONE ----------------------------------------------------------------------------------------------------
            If idPrenotazione.Text <> "" Then
                Cmd = New Data.SqlClient.SqlCommand("SELECT * FROM prenotazioni WITH(NOLOCK) WHERE Nr_Pren='" & idPrenotazione.Text & "'", Dbc)

                Rs = Cmd.ExecuteReader()
                Rs.Read()

                If Rs.HasRows() Then
                    image_secondo_guidatore.Visible = False

                    If (Rs("data_nascita") & "") <> "" Then
                        txtDataDiNascita.Text = Day(Rs("data_nascita")) & "/" & Month(Rs("data_nascita")) & "/" & Year(Rs("data_nascita"))
                    End If

                    'DETTAGLI PRENOTAZIONE
                    id_conducente_prenotazione.Text = Rs("id_conducente") & ""
                    txtNomeConducente.Text = Rs("nome_conducente") & ""
                    txtCognomeConducente.Text = Rs("cognome_conducente") & ""
                    txtMailConducente.Text = Rs("mail_conducente") & ""
                    txtIndirizzoConducente.Text = Rs("indirizzo_conducente") & ""
                    txtRiferimentoTO.Text = Rs("rif_to") & ""
                    txtRifTel.Text = Rs("riferimento_telefono") & ""

                    If (Rs("id_gruppo_app") & "") <> "" Then
                        dropGruppoDaConsegnare.SelectedValue = Rs("id_gruppo_app") & ""
                    Else
                        dropGruppoDaConsegnare.SelectedValue = 0
                    End If

                    If (Rs("id_gruppo") & "") <> "" Then
                        dropGruppoPrenotato.SelectedValue = Rs("id_gruppo") & ""
                    Else
                        dropGruppoPrenotato.SelectedValue = 0
                    End If

                    txtVoloOut.Text = Rs("N_VOLOOUT") & ""
                    txtVoloPr.Text = Rs("N_VOLOPR") & ""

                    gestione_note.InitForm(enum_note_tipo.note_prenotazione, lblNumPren.Text, False, True, "Note da prenotazione")
                End If

                'DETTAGLI ANAGRAFICI DEL PRIMO CONDUCENTE (SE SPECIFICATO SU PRENOTAZIONE SOLO NEL CASO DI NUOVO CONTRATTO)
                If (Rs("id_conducente") & "") <> "" And statoContratto.Text = "0" Then
                    vediPrimoGuidatore.HRef = "contratto_vedi_guidatore.aspx?idUtente=" & idPrimoConducente.Text

                    Cmd = New Data.SqlClient.SqlCommand("SELECT cognome, nome, city, comuni_ares.comune As citta_da_tabella_comuni, indirizzo, patente, CONVERT(Char(10),scadenza_patente,103) As scadenza_patente, altri_documenti FROM conducenti WITH(NOLOCK) LEFT JOIN comuni_ares WITH(NOLOCK) ON conducenti.id_comune_ares=comuni_ares.id WHERE ID_CONDUCENTE='" & Rs("id_conducente") & "'", Dbc)

                    Rs.Close()
                    Rs = Nothing
                    Dbc.Close()
                    Dbc.Open()

                    Rs = Cmd.ExecuteReader()
                    Rs.Read()

                    If Rs.HasRows() Then
                        txtCognomePrimoConducente.Text = Rs("cognome") & ""
                        txtNomePrimoConducente.Text = Rs("nome") & ""
                        If (Rs("citta_da_tabella_comuni") & "") <> "" Then
                            txtCittaPrimoConducente.Text = Rs("citta_da_tabella_comuni")
                        Else
                            txtCittaPrimoConducente.Text = Rs("city") & ""
                        End If
                        txtIndirizzoPrimoConducente.Text = Rs("indirizzo") & ""
                        txtPatentePrimoConducente.Text = Rs("patente") & ""
                        If (Rs("scadenza_patente") & "") <> "" Then
                            txtPatentePrimoConducente.Text = txtPatentePrimoConducente.Text & " (" & Rs("scadenza_patente") & ")"
                        End If
                        txtDocumentoPrimoConducente.Text = Rs("altri_documenti") & ""
                    End If
                Else
                    image_primo_guidatore.Visible = False
                End If

                Rs.Close()
                Rs = Nothing
                Dbc.Close()
                Dbc.Open()

                btnRicalcolaDaPrenotazione.Visible = True
                '----------------------------------------------------------------------------------------------------------------------------
            ElseIf provenienza.Text = "preventivi.aspx" Then
                image_secondo_guidatore.Visible = False
                image_primo_guidatore.Visible = False
                image_fatturare_a.Visible = False

                'DETTAGLI DA PREVENTIVO (SE ERA STATO SALVATO)-------------------------------------------------------------------------------
                Cmd = New Data.SqlClient.SqlCommand("SELECT nome_conducente, cognome_conducente, mail_conducente FROM preventivi WITH(NOLOCK) WHERE id='" & id_preventivo & "'", Dbc)

                Rs = Cmd.ExecuteReader()
                Rs.Read()

                If Rs.HasRows() Then
                    txtNomeConducentePreventivo.Text = Rs("nome_conducente") & ""
                    txtCognomeConducentePreventivo.Text = Rs("cognome_conducente") & ""
                    txtEmailConducentePreventivo.Text = Rs("mail_conducente") & ""
                End If

                Rs.Close()
                Rs = Nothing
                Dbc.Close()
                Dbc.Open()

                '----------------------------------------------------------------------------------------------------------------------------
                'COMPORTAMENTO DA PREVENTIVO ------------------------------------------------------------------------------------------------
                'SE PROVENGO DALLA PAGINA DEI PREVENTIVI ABILITO I PULSANTI CHE GESTISCONO LA MODIFICA DI UN CONTRATTO IN MANIERA OPPORTUNA

                btnRicalcolaDaPreventivo.Visible = True
                '----------------------------------------------------------------------------------------------------------------------------
            End If

            If statoContratto.Text <> "0" Then
                'OPERAZIONI DA EFFETTUARE NEL CASO DI CONTRATTO GIA' SALVATO-----------------------------------------------------------------
                'PRIMO CONDUCENTE (C'E' PER FORZA)
                vediPrimoGuidatore.HRef = "contratto_vedi_guidatore.aspx?idUtente=" & idPrimoConducente.Text & "&num_contratto=" & lblNumContratto.Text
                image_primo_guidatore.Visible = True

                Cmd = New Data.SqlClient.SqlCommand("SELECT cognome, nome, city, comuni_ares.comune As citta_da_tabella_comuni, indirizzo, patente, CONVERT(Char(10),scadenza_patente,103) As scadenza_patente, altri_documenti FROM contratti_conducenti WITH(NOLOCK) LEFT JOIN comuni_ares WITH(NOLOCK) ON contratti_conducenti.id_comune_ares=comuni_ares.id WHERE id_conducente='" & idPrimoConducente.Text & "' AND num_contratto='" & lblNumContratto.Text & "'", Dbc)

                'Rs.Close()
                'Rs = Nothing
                'Dbc.Close()
                'Dbc.Open()

                Rs = Cmd.ExecuteReader()
                Rs.Read()

                If Rs.HasRows() Then
                    txtCognomePrimoConducente.Text = Rs("cognome") & ""
                    txtNomePrimoConducente.Text = Rs("nome") & ""
                    If (Rs("citta_da_tabella_comuni") & "") <> "" Then
                        txtCittaPrimoConducente.Text = Rs("citta_da_tabella_comuni")
                    Else
                        txtCittaPrimoConducente.Text = Rs("city") & ""
                    End If
                    txtIndirizzoPrimoConducente.Text = Rs("indirizzo") & ""
                    txtPatentePrimoConducente.Text = Rs("patente") & ""
                    If (Rs("scadenza_patente") & "") <> "" Then
                        txtPatentePrimoConducente.Text = txtPatentePrimoConducente.Text & " (" & Rs("scadenza_patente") & ")"
                    End If
                    txtDocumentoPrimoConducente.Text = Rs("altri_documenti") & ""
                End If

                Rs.Close()
                Rs = Nothing
                Dbc.Close()
                Dbc.Open()

                If idSecondoConducente.Text <> "" Then
                    'PRIMO CONDUCENTE (C'E' PER FORZA)
                    vediSecondoGuidatore.HRef = "contratto_vedi_guidatore.aspx?idUtente=" & idSecondoConducente.Text & "&num_contratto=" & lblNumContratto.Text
                    image_secondo_guidatore.Visible = True

                    Cmd = New Data.SqlClient.SqlCommand("SELECT cognome, nome, city, comuni_ares.comune As citta_da_tabella_comuni, indirizzo, patente, CONVERT(Char(10),scadenza_patente,103) As scadenza_patente, altri_documenti FROM contratti_conducenti WITH(NOLOCK) LEFT JOIN comuni_ares WITH(NOLOCK) ON contratti_conducenti.id_comune_ares=comuni_ares.id WHERE id_conducente='" & idSecondoConducente.Text & "' AND num_contratto='" & lblNumContratto.Text & "'", Dbc)

                    Rs = Cmd.ExecuteReader()
                    Rs.Read()

                    If Rs.HasRows() Then
                        txtCognomeSecondoConducente.Text = Rs("cognome") & ""
                        txtNomeSecondoConducente.Text = Rs("nome") & ""
                        If (Rs("citta_da_tabella_comuni") & "") <> "" Then
                            txtCittaSecondaConducente.Text = Rs("citta_da_tabella_comuni")
                        Else
                            txtCittaSecondaConducente.Text = Rs("city") & ""
                        End If
                        txtIndirizzoSecondoConducente.Text = Rs("indirizzo") & ""
                        txtPatenteSecondoConducente.Text = Rs("patente") & ""
                        If (Rs("scadenza_patente") & "") <> "" Then
                            txtPatenteSecondoConducente.Text = txtPatenteSecondoConducente.Text & " (" & Rs("scadenza_patente") & ")"
                        End If
                        txtDocumentoSecondoConducente.Text = Rs("altri_documenti") & ""
                    End If

                    Rs.Close()
                    Rs = Nothing
                    Dbc.Close()
                    Dbc.Open()
                Else
                    image_secondo_guidatore.Visible = False
                End If

                If statoContratto.Text = "1" Then
                    'SALVATO MA CHECK OUT DA EFFETTUARE
                    btnAnnullaDocumento.Text = "Chiudi"

                    btnVoid.Visible = True

                    aggiorna_informazioni_dopo_modifica_costi()

                    'NASCONDO IL TOTALE DA PREAUTORIZZARE SE HO EFFETTUATO LA PREAUTORIZZAZIONE * DA FARE * 

                    lblTestoDaPreautorizzare.Visible = False
                    lblDaPreautorizzare.Visible = False
                    lblEuroDaPreautorizzare.Visible = False

                    btnGeneraContratto.Visible = True

                    'RICALCOLO
                    btnRicalcolaDaPrenotazione.Visible = False
                    btnRicalcolaDaPreventivo.Visible = False
                    btnRicalcolaModificaContratto.Visible = False

                    'VEICOLO (MODIFICABILE IN QUANTO CHECK OUT NON EFFETTUATO)
                    btnTrovaTarga.Visible = False

                    btnScegliDitta.Visible = True
                    btnCRV.Visible = False
                    btnPagamento.Visible = True
                    txtNoteInterne.Visible = False
                    intestazione_note.Visible = False
                    gestione_note_contratto.Visible = True
                    gestione_note_contratto.InitForm(enum_note_tipo.note_contratto, lblNumContratto.Text, False, False, "Note - Uso Interno")
                ElseIf statoContratto.Text = "2" Then
                    'CONTRATTO IN CORSO
                    btnVoid.Visible = False
                    'GUIDATORI E DITTE NON PIU' MODIFICABILI
                    btnScegliPrimoGuidatore.Visible = False
                    btnScegliSecondoConducente.Visible = False
                    btnScegliDitta.Visible = False

                    'NASCONDO IL TOTALE DA PREAUTORIZZARE 
                    lblTestoDaPreautorizzare.Visible = False
                    lblDaPreautorizzare.Visible = False
                    lblEuroDaPreautorizzare.Visible = False

                    'GPS
                    btnCercaGps.Visible = False

                    'MOSTRO LA DIFFERENZA RISPETTO LA PRENOTAZIONE
                    lblTestoDaPrenotazione.Visible = True
                    lblDifferenzaDaPrenotazione.Visible = True
                    lblEuroDaPrenotazione.Visible = True

                    aggiorna_informazioni_dopo_modifica_costi()

                    btnPagamento.Visible = True
                    btnGeneraContratto.Visible = True

                    btnTrovaTarga.Visible = False

                    'CRV
                    btnCRV.Visible = True
                    If numCrv.Text <> "0" Then
                        div_crv.Visible = True
                        listCrv.DataBind()
                    End If

                    'RICALCOLO
                    btnRicalcolaDaPrenotazione.Visible = False
                    btnRicalcolaDaPreventivo.Visible = False
                    btnRicalcolaModificaContratto.Visible = True

                    btnAggiungiExtra.Visible = False

                    'QUICK CHECK IN 
                    btnQuickCheckIn.Visible = True

                    'MODIFICHE PRECEDENTI
                    elenco_modifiche.Visible = True

                    btnAnnullaDocumento.Text = "Chiudi"

                    'CHECK OUT VISIBILE
                    bt_Check_Out.Visible = True

                    txtNoteInterne.Visible = False
                    intestazione_note.Visible = False
                    gestione_note_contratto.Visible = True
                    gestione_note_contratto.InitForm(enum_note_tipo.note_contratto, lblNumContratto.Text, False, False, "Note - Uso Interno")
                ElseIf statoContratto.Text = "5" Then
                    'LO STATO 5 (attesa sostituazione CRV) E' COME IL 2 MA NON MODIFICABILE, FINO A QUANDO NON VERRA' SELEZIONATA LA NUOVA VETTURA
                    btnVoid.Visible = False
                    'GUIDATORI E DITTE NON PIU' MODIFICABILI
                    btnScegliPrimoGuidatore.Visible = False
                    btnScegliSecondoConducente.Visible = False
                    btnScegliDitta.Visible = False

                    'NASCONDO IL TOTALE DA PREAUTORIZZARE 
                    lblTestoDaPreautorizzare.Visible = False
                    lblDaPreautorizzare.Visible = False
                    lblEuroDaPreautorizzare.Visible = False

                    'MOSTRO LA DIFFERENZA RISPETTO LA PRENOTAZIONE
                    lblTestoDaPrenotazione.Visible = True
                    lblDifferenzaDaPrenotazione.Visible = True
                    lblEuroDaPrenotazione.Visible = True

                    'GPS
                    btnCercaGps.Visible = False

                    aggiorna_informazioni_dopo_modifica_costi()

                    btnPagamento.Visible = True
                    btnGeneraContratto.Visible = False

                    btnTrovaTarga.Visible = False
                    btnCRV.Visible = False
                    btnAnnullaCRV.Visible = True

                    'RICALCOLO
                    btnRicalcolaDaPrenotazione.Visible = False
                    btnRicalcolaDaPreventivo.Visible = False
                    btnRicalcolaModificaContratto.Visible = False

                    btnAggiungiExtra.Visible = False

                    'QUICK CHECK IN 
                    btnQuickCheckIn.Visible = False

                    'MODIFICHE PRECEDENTI
                    elenco_modifiche.Visible = True

                    btnAnnullaDocumento.Text = "Chiudi"

                    'CHECK OUT NON VISIBILE
                    bt_Check_Out.Visible = False

                    div_crv.Visible = True
                    listCrv.Visible = True
                    btnTrovaTarga.Visible = True

                    txtNoteInterne.Visible = False
                    intestazione_note.Visible = False
                    gestione_note_contratto.Visible = True
                    gestione_note_contratto.InitForm(enum_note_tipo.note_contratto, lblNumContratto.Text, False, False, "Note - Uso Interno")
                ElseIf statoContratto.Text = "3" Then
                    'QUICK CHECK IN 
                    btnVoid.Visible = False
                    'GUIDATORI - TARGA - DITTA NON PIU' MODIFICABILI
                    btnScegliPrimoGuidatore.Visible = False
                    btnScegliSecondoConducente.Visible = False
                    btnScegliDitta.Visible = False
                    btnScegliTarga.Visible = False

                    'NASCONDO IL TOTALE DA PREAUTORIZZARE 
                    lblTestoDaPreautorizzare.Visible = False
                    lblDaPreautorizzare.Visible = False
                    lblEuroDaPreautorizzare.Visible = False

                    'MOSTRO LA DIFFERENZA RISPETTO LA PRENOTAZIONE
                    lblTestoDaPrenotazione.Visible = True
                    lblDifferenzaDaPrenotazione.Visible = True
                    lblEuroDaPrenotazione.Visible = True

                    'GPS
                    btnCercaGps.Visible = False

                    aggiorna_informazioni_dopo_modifica_costi()

                    btnPagamento.Visible = False
                    btnGeneraContratto.Visible = True
                    btnSalvaRientro.Visible = True

                    btnTrovaTarga.Visible = False

                    'CRV
                    btnCRV.Visible = False
                    If numCrv.Text <> "0" Then
                        div_crv.Visible = True
                        listCrv.DataBind()
                    End If

                    'RICALCOLO
                    btnRicalcolaDaPrenotazione.Visible = False
                    btnRicalcolaDaPreventivo.Visible = False
                    btnRicalcolaModificaContratto.Visible = False

                    btnAggiungiExtra.Visible = False

                    'QUICK CHECK IN 
                    btnQuickCheckIn.Visible = False
                    riga_rientro_veicolo.Visible = True
                    riga_rientro.Visible = True

                    btnAnnullaQuickCheckIn.Visible = True
                    If livello_accesso_annulla_quick.Text <> "3" Then
                        btnAnnullaQuickCheckIn.Enabled = False
                    End If

                    btnGeneraContrattoRientro.Visible = True

                    'MODIFICHE PRECEDENTI
                    elenco_modifiche.Visible = True

                    btnAnnullaDocumento.Text = "Chiudi"

                    txtNoteInterne.Visible = False
                    intestazione_note.Visible = False
                    gestione_note_contratto.Visible = True
                    gestione_note_contratto.InitForm(enum_note_tipo.note_contratto, lblNumContratto.Text, False, False, "Note - Uso Interno")
                ElseIf statoContratto.Text = "4" Or statoContratto.Text = "8" Then
                    'CHIUSO DA INCASSARE
                    If funzioni_comuni.contratto_settabile_void(idContratto.Text, Request.Cookies("SicilyRentCar")("stazione")) Then
                        btnVoid.Visible = True
                    End If
                    'GUIDATORI - TARGA - DITTA NON PIU' MODIFICABILI
                    btnScegliPrimoGuidatore.Visible = False
                    btnScegliSecondoConducente.Visible = False
                    btnScegliDitta.Visible = False
                    btnScegliTarga.Visible = False

                    'GPS
                    btnCercaGps.Visible = False

                    'NASCONDO IL TOTALE DA PREAUTORIZZARE 
                    lblTestoDaPreautorizzare.Visible = False
                    lblDaPreautorizzare.Visible = False
                    lblEuroDaPreautorizzare.Visible = False

                    'MOSTRO LA DIFFERENZA RISPETTO LA PRENOTAZIONE
                    lblTestoDaPrenotazione.Visible = True
                    lblDifferenzaDaPrenotazione.Visible = True
                    lblEuroDaPrenotazione.Visible = True

                    aggiorna_informazioni_dopo_modifica_costi()

                    btnPagamento.Visible = True
                    btnGeneraContratto.Visible = True
                    btnGeneraContrattoRientro.Visible = True

                    'E' POSSIBILE VISUALIZZARE IL CHEK IN SALVATO PER IL VEICOLO
                    btnSalvaRientro.Visible = True
                    btnSalvaRientro.Text = "Vedi check"


                    'RICALCOLO
                    btnRicalcolaDaPrenotazione.Visible = False
                    btnRicalcolaDaPreventivo.Visible = False
                    btnRicalcolaModificaContratto.Visible = False

                    'RICALCOLO E AZIONI ADMIN: SOTTO PERMESSO
                    If livello_accesso_admin.Text = "3" Then
                        btnModificaAdmin.Visible = True
                        lblAbbuonaGiornoExtra.Visible = True
                        chkAbbuonaGiornoExtra.Visible = True
                        chkAbbuonaGiornoExtra.Enabled = False

                        btnDaFatturare.Visible = True

                        If statoContratto.Text = "8" Then
                            btnGeneraFattura.Visible = True
                            lblDataFattura.Visible = True
                            txtDataFattura.Visible = True
                            lblNumFattura.Visible = True
                            txtNumFattura.Visible = True
                        End If
                    End If

                    btnAggiungiExtra.Visible = False

                    btnTrovaTarga.Visible = False

                    'CRV
                    btnCRV.Visible = False
                    If numCrv.Text <> "0" Then
                        div_crv.Visible = True
                        listCrv.DataBind()
                    End If

                    'QUICK CHECK IN 
                    btnQuickCheckIn.Visible = False
                    riga_rientro.Visible = True
                    'RIENTRO VEICOLO
                    txtSerbatoioRientro.ReadOnly = True
                    txtKmRientro.ReadOnly = True
                    riga_rientro_veicolo.Visible = True

                    'MODIFICHE PRECEDENTI
                    elenco_modifiche.Visible = True

                    btnAnnullaDocumento.Text = "Chiudi"

                    If fatt_da_controllare = "0" Then
                        If livello_accesso_admin.Text <> "3" Then
                            btnFattDaControllare.Visible = True
                        Else
                            btnFattDaControllare.Visible = False
                        End If
                        lblFattDaControllare.Visible = False
                    Else
                        btnFattDaControllare.Visible = False
                        lblFattDaControllare.Visible = True
                    End If


                    txtNoteInterne.Visible = False
                    intestazione_note.Visible = False
                    gestione_note_contratto.Visible = True
                    gestione_note_contratto.InitForm(enum_note_tipo.note_contratto, lblNumContratto.Text, False, False, "Note - Uso Interno")

                    riga_fatturazione.Visible = True

                    If statoContratto.Text = "4" Then
                        lblStatoFatturazione.Text = "Da Incassare"
                        btnDaFatturare.Text = "Da Fatturare"

                        btnGeneraFattura.OnClientClick = "javascript: return(window.confirm ('Attenzione - Sei sicuro di voler generare la fattura per questo contratto?'));"
                    ElseIf statoContratto.Text = "8" Then
                        If non_fatturare Then
                            btnDaFatturare.Text = "Da Fatturare"
                            lblStatoFatturazione.Text = "Da Fatturare - NON FATTURARE"
                            btnGeneraFattura.OnClientClick = "javascript: return(window.confirm ('Attenzione - Sei sicuro di voler generare la fattura anche se il contratto è stato indicato come da NON FATTURARE?'));"
                        Else
                            btnDaFatturare.Text = "Non Fatturare"
                            lblStatoFatturazione.Text = "Da Fattuare"
                            btnGeneraFattura.OnClientClick = "javascript: return(window.confirm ('Attenzione - Sei sicuro di voler generare la fattura per questo contratto?'));"
                        End If
                    End If

                    lblSaldo.Text = "Saldo: " & FormatNumber(getTotaleDaPagare(), 2, , , TriState.False) & " €"
                ElseIf statoContratto.Text = "6" Then
                    'CHIUSO FATTURATO
                    visibilita_fatturato()

                    lblStatoFatturazione.Text = "Fatturato"
                    btnDaFatturare.Visible = False
                    lblSaldo.Text = "Saldo: " & FormatNumber(getTotaleDaPagare(), 2, , , TriState.False) & " €"
                ElseIf statoContratto.Text = "7" Then
                    'VOID
                    visibilita_void()
                End If
            End If
        End If
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub



    Protected Sub ImpostaPannelloPagamento(ByVal num_pren As String, ByVal num_contratto As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        If num_pren = "" Then
            num_pren = "0"
        End If

        If num_contratto = "" Then
            num_contratto = "0"
        End If

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 Nr_Contratto FROM pagamenti_extra WITH(NOLOCK) WHERE N_PREN_RIF='" & num_pren & "' OR N_CONTRATTO_RIF='" & num_contratto & "'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test = "" Then
            'NESSUNA TRANSAZIONE
            tab_dettagli_pagamento.Visible = False
        Else
            tab_dettagli_pagamento.Visible = True
            'SE ESISTE ALMENO UNA TRANSAZIONE MOSTRO IL TOTALE PREAUTORIZZATO E IL TOTALE INCASSATO
            Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(SUM(ISNULL(PER_IMPORTO,0)),0) FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE (N_PREN_RIF='" & num_pren & "' OR N_CONTRATTO_RIF='" & num_contratto & "') AND ((id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Richiesta & "' AND preaut_aperta='1' AND operazione_stornata='0') OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Integrazione & "' AND preaut_aperta='1' AND operazione_stornata='0') OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Autorizzazione & "') )", Dbc)
            txtPOS_TotPreaut.Text = FormatNumber(Cmd.ExecuteScalar, 2, , , TriState.False)

            'N_PREN_RIF='" & num_pren & "' OR    - PREPAGATE - IL TOTALE INCASSATO NON DEVE CONSIDERARE PIU' L'INCASSO SU PRENOTAZIONE
            Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(SUM(ISNULL(PER_IMPORTO,0)),0) FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE (N_CONTRATTO_RIF='" & num_contratto & "') AND ((id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Vendita & "' AND operazione_stornata='0') OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Chiusura & "' AND operazione_stornata='0') OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso_su_RA & "') OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Deposito_su_RA & "') OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Attivo & "') OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Passivo & "') OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.MCR_Pagamento_Cash & "') OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso & "') OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Pagamento_Contanti & "') OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso & "') )", Dbc)
            txtPOS_TotIncassato.Text = FormatNumber(Cmd.ExecuteScalar, 2, , , TriState.False)
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub listContrattiCosti_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles listContrattiCosti.DataBinding
        'SE IL CONTRATTO E' NELLO STATO "APERTO" OGNI VOLTA CHE AGGIORNO LA TABELLA AGGIORNO, PER GLI ACCESSORI, L'INFORMAZIONE CIRCA
        'LA VENDIBILITA' NOLO IN CORSO (PER FAR SI CHE, SE SU RICHIESTA SE NE ABILITA UNO O PIU', L'UTENTE, AGGIORNANDO LA LISTA CON L'APPOSITO
        'PULSANTE POSSA DOPO SELEZIONARE GLI ACCESSORI AGGIUNTI)


        If statoContratto.Text = "2" Then
            funzioni_comuni.aggiorna_accessori_acquistabili_nolo_in_corso(idContratto.Text, numCalcolo.Text)

            'AGGIORNO GLI ELEMENTI EXTRA
            Dim id_tariffe_righe As String = ""
            If dropTariffeGeneriche.SelectedValue <> "0" Then
                id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
            ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
            End If
            sqlElementiExtra.SelectCommand = funzioni.getQueryElementiExtra(id_tariffe_righe, True)
            dropElementiExtra.Items.Clear()
            dropElementiExtra.Items.Add("Seleziona...")
            dropElementiExtra.Items(0).Value = "0"
            dropElementiExtra.DataBind()
        End If

        ultimo_gruppo.Text = ""
        intestazione_informativa_da_visualizzare.Text = "1"
    End Sub

    Protected Sub listContrattiCosti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listContrattiCosti.ItemDataBound
        Dim gruppo As Label = e.Item.FindControl("gruppo")
        Dim id_a_carico_di As Label = e.Item.FindControl("id_a_carico_di")
        Dim nome_costo As Label = e.Item.FindControl("nome_costo")
        Dim obbligatorio As Label = e.Item.FindControl("obbligatorio")
        Dim id_metodo_stampa As Label = e.Item.FindControl("id_metodo_stampa")
        Dim sconto As Label = e.Item.FindControl("lblSconto")
        'Dim valore_costo As Label = e.Item.FindControl("valore_costoLabel")
        Dim costo_scontato As Label = e.Item.FindControl("costo_scontato")
        Dim omaggiabile As Label = e.Item.FindControl("omaggiabile")
        Dim Oldomaggiato As CheckBox = e.Item.FindControl("chkOldOmaggio")
        Dim omaggiato As CheckBox = e.Item.FindControl("chkOmaggio")
        Dim prepagato As Label = e.Item.FindControl("prepagato")
        Dim id_unita_misura As Label = e.Item.FindControl("id_unita_misura")
        Dim packed As Label = e.Item.FindControl("packed")
        Dim costo_unitario As Label = e.Item.FindControl("lbl_costo_unitario")
        Dim qta As Label = e.Item.FindControl("qta")
        Dim id_elemento As Label = e.Item.FindControl("id_elemento")
        Dim imponibile As Label = e.Item.FindControl("imponibile")
        Dim onere As Label = e.Item.FindControl("onere")
        Dim iva As Label = e.Item.FindControl("iva")
        Dim tipologia As Label = e.Item.FindControl("tipologia")
        Dim chkScegli As CheckBox = e.Item.FindControl("chkScegli")


        'COLONNA COMMISSIONI
        If riga_commissioni.Visible Then
            e.Item.FindControl("labelCommissioni").Visible = True

            Dim lblCommissioni As Label = e.Item.FindControl("lblCommissioni")
            If lblCommissioni.Text <> "0,00" Then
                lblCommissioni.Visible = True
            End If
            If nome_costo.Text = Costanti.testo_elemento_totale Then
                lblCommissioni.Font.Bold = True
                lblCommissioni.Font.Size = 12
            End If
        End If

        If prepagato.Text = "True" Or (txtGiorniPrepagati.Visible And nome_costo.Text = Costanti.testo_elemento_totale) Then
            chkScegli.Enabled = False
            e.Item.FindControl("costo_prepagato").Visible = True
            e.Item.FindControl("labelPrepagato").Visible = True

            If nome_costo.Text = Costanti.testo_elemento_totale Then
                Dim costo_prepagato As Label = e.Item.FindControl("costo_prepagato")
                costo_prepagato.Font.Bold = True
                costo_prepagato.Font.Size = 12
            End If

            If costo_scontato.Text = "0,00" Then
                costo_unitario.Visible = False
                qta.Visible = False
                costo_scontato.Visible = False
                onere.Visible = False
                e.Item.FindControl("aliquota_iva").Visible = False
                iva.Visible = False
                imponibile.Visible = False
            End If
        End If

        'REGOLE SU ACCESSORI A SCELTA ----------------------------------------------------------------------------------------------------
        If obbligatorio.Text = "False" And id_a_carico_di.Text <> Costanti.id_accessorio_incluso Then

            chkScegli.Visible = True
            'NEL CASO IN CUI IL CONTRATTO SIA NELLO STATO "2" (APERTO)
            'SI POSSONO AGGIUNGERE SOLAMENTE ACCESSORI TRA QUELLI CHE SONO STATI SETTATI COME "ACQUISTABILI A NOLO IN CORSO"
            'SE LO STATO E' "3" o "4" (chiuso senzo/con quick check in) - SOLO CHI HA IL PERMESSO DI MODIFICA ADMIN SALTA QUESTO CONTROLLO
            If statoContratto.Text = "2" Or statoContratto.Text = "5" Or statoContratto.Text = "4" Or statoContratto.Text = "8" Then
                Dim acquistabile_nolo_in_corso As Label = e.Item.FindControl("acquistabile_nolo_in_corso")
                If (acquistabile_nolo_in_corso.Text = "False" And livello_accesso_admin.Text <> "3") Then
                    chkScegli.Enabled = False
                End If
                If (statoContratto.Text = "2" And statoModificaContratto.Text = "0") Or ((statoContratto.Text = "4" Or statoContratto.Text = "8") And statoModificaContratto.Text = "0") Then
                    chkScegli.Enabled = False
                End If
            ElseIf statoContratto.Text = "3" Or statoContratto.Text = "7" Or statoContratto.Text = "6" Then
                chkScegli.Enabled = False
            End If

            'GLI ACCESSORI NON SELEZIONATI NON MOSTRANO IL PREZZO------------------------------------------------------------------------------
            If Not chkScegli.Checked Then
                'valore_costo.Visible = False
                sconto.Visible = False
                costo_scontato.Visible = False
                qta.Visible = False
                costo_unitario.Visible = False
                imponibile.Visible = False
                onere.Visible = False
                iva.Visible = False
                e.Item.FindControl("aliquota_iva").Visible = False
            End If
            '----------------------------------------------------------------------------------------------------------------------------------
            'IN OGNI CASO GLI ACCESSORI PREPAGATI NON SONO RIMUOVIBILI
            If prepagato.Text = "True" Then
                chkScegli.Enabled = False
            End If
        End If


        If prepagato.Text = "True" Then
            If LCase(nome_costo.Text) <> "valore tariffa" Then
                e.Item.FindControl("lblPrepagato").Visible = True
            End If
        End If
        '---------------------------------------------------------------------------------------------------------------------------------

        If sconto.Text = "0,00" Then
            sconto.Text = ""
        Else
            sconto.Text = sconto.Text & " €"
        End If

        If packed.Text = "True" Then
            'SE L'ACCESSORIO E' PACKED ALLORA LA QUANTITA E' SEMPRE 1
            'qta.Text = "1"
            costo_unitario.Text = imponibile.Text
        ElseIf id_unita_misura.Text = "0" Then
            'SE L'UNITA' DI MISURA NON E' SPECIFICATA ALLORA L'ACCESSORIO HA COSTO UNITARIO A PRESCINDERE DAL CAMPO PACKED
            'qta.Text = "1"
            costo_unitario.Text = imponibile.Text
        ElseIf (id_unita_misura.Text = Costanti.id_unita_misura_giorni) Or (tipologia.Text = "KM_EXTRA" And obbligatorio.Text = "True") Then
            'SE L'L'UNITA' DI MISURA E' AL GIORNO (ACCESSORIO CERTAMENTE NON PACKED) - OPPURE SE SI TRATTA DI KM_EXTRA ADDEBITATI (OBBLIGATORIO)
            'qta.Text = txtNumeroGiorni.Text
            If txtGiorniPrepagati.Visible And id_unita_misura.Text = Costanti.id_unita_misura_giorni And prepagato.Text = "True" Then
                qta.Text = CInt(qta.Text) - CInt(txtGiorniPrepagati.Text)
            End If
            If CInt(qta.Text) = 0 Then
                qta.Text = "1"
            End If
            costo_unitario.Text = FormatNumber(CDbl(imponibile.Text) / CInt(qta.Text), 2, , , TriState.False)

            'If CInt(qta.Text) <> 0 Then
            '    costo_unitario.Text = FormatNumber(CDbl(imponibile.Text) / CInt(qta.Text), 2, , , TriState.False)
            'Else
            '    costo_unitario.Text = "0,00"
            'End If

        ElseIf id_unita_misura.Text <> Costanti.id_unita_misura_giorni Then
            'SE L'UNITA' DI MISURA NON E' GIORNALIERA ATTUALMENTE LO TRATTO COME FOSSE PACKED
            'qta.Text = "1"
            costo_unitario.Text = imponibile.Text & " €"
        ElseIf packed.Text = "" And id_unita_misura.Text = "" Then
            qta.Text = ""
            costo_unitario.Text = ""
        End If


        'CALCOLO DEL COSTO UNITARIO-------------------------------------------------------------------------------------------------------- 
        If Oldomaggiato.Checked Then
            'valore_costo.Text = "0,00 €"
            If id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore Then
                'PER LE INFORMATIVE NON NASCONDO IL COSTO - PER QUESTI ACCESSORI L'OMAGGIABILITA' EQUIVALE AD UN PAGAMENTO IN UN SECONDO MOMENTO
                costo_scontato.Text = "0,00 €"
            End If

            sconto.Text = ""
            costo_unitario.Visible = False
            qta.Visible = False
            imponibile.Visible = False
            onere.Visible = False
            iva.Visible = False
            e.Item.FindControl("aliquota_iva").Visible = False
        Else
            'valore_costo.Text = valore_costo.Text & " €"
            costo_scontato.Text = costo_scontato.Text & " €"
        End If
        '-----------------------------------------------------------------------------------------------------------------------------------

        If id_elemento.Text = Costanti.ID_tempo_km Or LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Or LCase(nome_costo.Text) = "sconto" Then
            nome_costo.Font.Bold = True
        End If

        If id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore Then
            If id_a_carico_di.Text = Costanti.id_accessorio_incluso And id_elemento.Text <> Costanti.ID_tempo_km Then
                'PER GLI ELEMENTI INCLUSI TRANNE IL TEMPO KM NON MOSTRO IL PREZZO (CHE E' SEMPRE ZERO)
                e.Item.FindControl("lblIncluso").Visible = True
                'valore_costo.Visible = False
                sconto.Visible = False
                costo_scontato.Visible = False
                costo_unitario.Visible = False
                qta.Visible = False
                imponibile.Visible = False
                onere.Visible = False
                iva.Visible = False
                e.Item.FindControl("aliquota_iva").Visible = False
            ElseIf id_a_carico_di.Text <> Costanti.id_accessorio_incluso And obbligatorio.Text = "True" And id_elemento.Text <> Costanti.ID_tempo_km And LCase(nome_costo.Text) <> "sconto" Then
                e.Item.FindControl("lblObbligatorio").Visible = True
            End If
        Else
            Dim servizio_rifornimento_tolleranza As Label = e.Item.FindControl("servizio_rifornimento_tolleranza")

            'PER LE INFORMATIVE NON MOSTRO IL COSTO UNITARIO E LA QUANTITA'
            costo_unitario.Visible = False
            qta.Visible = False

            imponibile.Visible = False
            onere.Visible = False
            iva.Visible = False
            e.Item.FindControl("aliquota_iva").Visible = False


            'A CONTRATTO CHIUSO (QUICK-CHECK IN E CHIUSO) LA LABEL INFORMATIVA VIENE SOSTITUITO COL CHECK BOX 
            If statoContratto.Text <> "3" And statoContratto.Text <> "4" And statoContratto.Text <> "8" And statoContratto.Text <> "6" Then
                e.Item.FindControl("lblInformativa").Visible = True
            End If

            'IN FASE DI CONTRATTO CHIUSO PER CUI E' POSSIBILE INSERIRE LE INFORMATIVE MOSTRO L'INTESTAZIONE - DO LA POSSIBILITA' DI 
            'SCEGLIERE L'INFORMATIVA PER AGGIUNGERE IL COSTO AL TOTALE
            If (statoContratto.Text = "3" Or statoContratto.Text = "4" Or statoContratto.Text = "6" Or statoContratto.Text = "8") Then
                Dim chkOldScegli As CheckBox = e.Item.FindControl("chkOldScegli")

                If intestazione_informativa_da_visualizzare.Text = "1" Then
                    e.Item.FindControl("riga_intestazione_informative").Visible = True
                    intestazione_informativa_da_visualizzare.Text = "0"
                End If

                chkScegli.Visible = True
                omaggiato.Visible = True

                If chkOldScegli.Checked Then
                    'PER LE INFORMATIVE SELEZIONATE MOSTRO IL DETTAGLIO DEL COSTO
                    imponibile.Visible = True
                    onere.Visible = True
                    iva.Visible = True
                    e.Item.FindControl("aliquota_iva").Visible = True

                    costo_scontato.Font.Bold = True
                End If

                'L'INFORMATIVA "SERVIZIO RIFORNIMENTO", SE SETTATO COME DA GESTIRE IN MANIERA AUTOMATICA (CARBURANTE DENTRO TABELLE LISTINI)
                'NON E' GESTIBILE DALL'UTENTE. SE SETTATO DA GESTIRE IN MANIERA AUTOMATICA, L'EMENENTO IN condizioni_elementi AVRA' IL CAMPO
                'servizio_rifornimento_tolleranza DIVERSO DA NULL  
                'L'INFORMATIVA KM EXTRA NON E' A SCELTA

                If servizio_rifornimento_tolleranza.Text <> "" Then
                    chkScegli.Enabled = False
                End If

                If tipologia.Text = "KM_EXTRA" Then
                    chkScegli.Visible = False
                    omaggiato.Visible = False
                End If

                If statoContratto.Text = "6" Then
                    chkScegli.Enabled = False
                    omaggiato.Enabled = False
                End If
            End If

            'SE E' STATO ACQUISTATO L'ACCESSORIO PIENO CARBURANTE NON MOSTRO LA PENALITA' SERVIZIO RIFORNIMENTO
            If servizio_rifornimento_tolleranza.Text <> "" Then
                If pieno_carburante_selezionato(idContratto.Text, numCalcolo.Text) Then
                    e.Item.FindControl("riga_elementi").Visible = False
                End If
            End If
        End If

        If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
            'NEL CASO DI BROKER IL TOTALE CHE DEVE PAGARE IL CLIENTE E' IL TOTALE CALCOLATO DA SISTEMA MENO IL VALORE TARIFFA A CARICO
            'DEL BROKER (LE ESTENSIONI DEL VALORE TARIFFA POSSONO O MENO ESSERE A CARICO DEL CLIENTE). AGGIORNO QUINDI IL COSTO COL VALORE
            'DA MOSTRARE

            'If tariffa_broker.Text = "1" Then
            '    costo_scontato.Text = CDbl(costo_scontato.Text) - getCostoACaricoDelBroker()
            'End If

            If tariffa_broker.Text = "1" Then
                Dim aCaricoBroker(4) As Double
                aCaricoBroker = getCostiACaricoDelBroker()

                If (btnRicalcolaDaPreventivo.Text <> "Modifica" Or btnRicalcolaDaPrenotazione.Text <> "Modifica" Or btnRicalcolaModificaContratto.Text <> "Modifica/Estensione" Or btnModificaAdmin.Text <> "Modifica Admin") And dropVariazioneACaricoDi.SelectedValue = "1" Then
                    'NEL CASO IN CUI SIAMO NELLO STATO DI MODIFICA CONTRATTO
                    'SE LA MODIFICA E' A CARICO DEL BROKER RIMUOVO DIRETTAMENTE IL COSTO DEL VALORE TARIFFA (A CARICO DEL BROKER)
                    Dim id_gruppoLabel As Label = e.Item.FindControl("id_gruppoLabel")

                    imponibile.Text = CDbl(imponibile.Text) - aCaricoBroker(0)
                    onere.Text = CDbl(onere.Text) - aCaricoBroker(1)
                    iva.Text = CDbl(iva.Text) - aCaricoBroker(2)
                    costo_scontato.Text = CDbl(costo_scontato.Text) - aCaricoBroker(3)
                Else
                    'SE SIAMO IN FASE DI MODIFICA E SE LA VARIAZIONE E' A CARICO DEL CLIENTE MA IL COSTO E' DIMINUITO TRATTO IL CASO 
                    'COME FOSSE A CARICO DEL BROKER - SI ENTRA QUI DENTRO ANCHE SE NON SIAMO IN ALCUNO STATO DI MODIFICA

                    If CDbl(a_carico_del_broker.Text) < aCaricoBroker(3) Then
                        costo_scontato.Text = CDbl(costo_scontato.Text) - CDbl(a_carico_del_broker.Text)

                        Dim netto_broker As Double = CDbl(a_carico_del_broker.Text) / (1 + CDbl(aCaricoBroker(4)) / 100)
                        Dim iva_broker As Double = CDbl(a_carico_del_broker.Text) - netto_broker

                        iva.Text = CDbl(iva.Text) - iva_broker
                        imponibile.Text = CDbl(imponibile.Text) - netto_broker
                    Else
                        imponibile.Text = CDbl(imponibile.Text) - aCaricoBroker(0)
                        onere.Text = CDbl(onere.Text) - aCaricoBroker(1)
                        iva.Text = CDbl(iva.Text) - aCaricoBroker(2)
                        costo_scontato.Text = CDbl(costo_scontato.Text) - aCaricoBroker(3)
                    End If
                End If

                imponibile.Text = FormatNumber(imponibile.Text, 2, , , TriState.False)
                onere.Text = FormatNumber(onere.Text, 2, , , TriState.False)
                iva.Text = FormatNumber(iva.Text, 2, , , TriState.False)
            End If

            'valore_costo.Visible = False
            costo_scontato.Text = FormatNumber(costo_scontato.Text, 2, , , TriState.False)
            costo_scontato.Font.Bold = True
            costo_scontato.ForeColor = Drawing.Color.Blue
            costo_scontato.Font.Size = 12
            e.Item.FindControl("lblObbligatorio").Visible = False
            qta.Visible = False
            costo_unitario.Visible = False
            e.Item.FindControl("aliquota_iva").Visible = False

            If (Not (statoContratto.Text = "2" And statoModificaContratto.Text = "0")) And (Not statoContratto.Text = "5") And (Not statoContratto.Text = "7") And (Not statoContratto.Text = "6") Then
                'AGGIORNAMENTO DEGLI ACCESSORI NON VISIBILE SOLO SE IL CONTRATTO E' APERTO MA NON SIAMO NELLO STATO DI MODIFICA O NELLO STATO DI CRV - ATTESA VEICOLO
                Dim aggiorna As Button = e.Item.FindControl("aggiorna")
                aggiorna.Visible = True
            End If
        End If

        'Response.Write(nome_costo.Text & "<br>")

        If gruppo.Text = ultimo_gruppo.Text Then
            e.Item.FindControl("riga_gruppo").Visible = False
            e.Item.FindControl("riga_intestazione").Visible = False
        Else
            e.Item.FindControl("riga_gruppo").Visible = True
            e.Item.FindControl("riga_intestazione").Visible = True
            ultimo_gruppo.Text = gruppo.Text
        End If

        'OMAGGIABILITA' DI ACCESSORI E OBBLIGATORI --------------------------------------------------------------------------------------
        If (omaggiabile.Text = "True" Or complimentary.Text = "1" Or (livello_accesso_admin.Text = "3" And (id_elemento.Text <> Costanti.ID_tempo_km Or tariffa_broker.Text = "0"))) And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore And Not e.Item.FindControl("lblIncluso").Visible And LCase(nome_costo.Text) <> LCase(Costanti.testo_elemento_totale) Then
            'ABILITO L'OMAGGIABILITA' DI UN ELEMENTO A SCELTA SOLAMENTE SE L'UTENTE HA IL RELATIVO PERMESSO - SOLO PER OBBLIGATORI ED ACCESSORI

            If (livello_accesso_omaggi.Text = "3" Or livello_accesso_admin.Text = "3" Or complimentary.Text = "1") Then
                If prepagato.Text = "False" Then
                    omaggiato.Visible = True
                End If

                If statoContratto.Text = "2" Or statoContratto.Text = "3" Or statoContratto.Text = "4" Or statoContratto.Text = "5" Or statoContratto.Text = "6" Or statoContratto.Text = "7" Or statoContratto.Text = "8" Then
                    'PER I CONTRATTI APERTI O CHIUSI NON PERMETTO DI VARIARE L'OMAGGIABILITA' (PER GLI ACCESSORI A SCELTA RESTA ABILITATA SE
                    'E' ACQUISTABILE A NOLO IN CORSO) - SOLO CHI HA IL PERMESSO DI MODIFICA ADMIN SALTA QUESTO CONTROLLO
                    If (statoContratto.Text = "2" And statoModificaContratto.Text = "0") Or statoContratto.Text = "6" Or statoContratto.Text = "7" Or (statoContratto.Text = "8" And livello_accesso_admin.Text <> "3") Then
                        If complimentary.Text <> "1" Then
                            omaggiato.Enabled = False
                        End If
                    End If

                    If obbligatorio.Text = "False" Then
                        Dim chkOldScegli As CheckBox = e.Item.FindControl("chkOldScegli")
                        Dim acquistabile_nolo_in_corso As Label = e.Item.FindControl("acquistabile_nolo_in_corso")

                        If livello_accesso_admin.Text = "3" And (statoContratto.Text = "4" Or statoContratto.Text = "8") Then
                            'CASO MODIFICA ADMIN
                            If statoModificaContratto.Text = "0" Then
                                omaggiato.Enabled = False
                            End If
                        ElseIf ((acquistabile_nolo_in_corso.Text = "False" And livello_accesso_admin.Text <> "3") And Not chkOldScegli.Checked And (statoContratto.Text <> "0" And statoContratto.Text <> "1")) Or (Not chkOldScegli.Checked And statoContratto.Text = "3") Or (Not chkOldScegli.Checked And statoContratto.Text = "4") Or (complimentary.Text = "1" And Not chkScegli.Checked) Then
                            omaggiato.Enabled = False
                        End If
                    Else
                        'OBBLIGATORI: SIAMO QUI DENTRO NEL CASO DI PERMESSI ADMIN (PER CUI E' TUTTO OMAGGIABILE - ANCHE IL VALORE TARIFFA SE NON BROKER)
                        If livello_accesso_admin.Text = "3" And (statoContratto.Text = "3" Or statoContratto.Text = "4" Or statoContratto.Text = "8") Then
                            'CASO MODIFICA ADMIN
                            If statoModificaContratto.Text = "0" Then
                                omaggiato.Enabled = False
                            End If
                        End If
                    End If
                End If
            Else
                omaggiato.Visible = True
                omaggiato.Enabled = False
            End If
        End If

        '----------------------------------------------------------------------------------------------------------------------------------
        'TARIFFE(BROKER) : SE L'UTENTE NON HA IL PERMESSO VENGONO NASCOSTI TUTTI I PREZZI E VENGONO VISUALIZZATI SOLO GLI ACCESSORI (SENZA PREZZO)

        If tariffa_broker.Text = "1" Then
            If id_elemento.Text = Costanti.ID_tempo_km Then
                costo_unitario.Visible = False
                qta.Visible = False
                sconto.Visible = False
                'IL COSTO SCONTATO NON E' VISIBILE SE COINCIDE COL COSTO A CARICO DEL BROKER, ALTRIMENTI VISUALIZZO SOLO LA PARTE A 
                'CARICO DEL CLIENTE - SE, IN FASE DI NUOVO CALCOLO, IL COSTO E' A CARICO DEL BROKER COMUNQUE NASCONDO IL COSTO
                If CDbl(costo_scontato.Text) = CDbl(a_carico_del_broker.Text) Or dropVariazioneACaricoDi.SelectedValue = "1" Then
                    costo_scontato.Visible = False
                    imponibile.Visible = False
                    e.Item.FindControl("onere").Visible = False
                    e.Item.FindControl("iva").Visible = False
                    e.Item.FindControl("aliquota_iva").Visible = False
                Else
                    'SE IL COSTO E' DIMINUITO TRATTO IL CASO COME SE FOSSE A CARICO DEL BROKER
                    If CDbl(costo_scontato.Text) >= CDbl(a_carico_del_broker.Text) Then
                        costo_scontato.Text = FormatNumber(CDbl(costo_scontato.Text) - CDbl(CDbl(a_carico_del_broker.Text)), 2, , , TriState.False)

                        Dim aliquota_iva As Label = e.Item.FindControl("aliquota_iva")
                        Dim netto_broker As Double = CDbl(a_carico_del_broker.Text) / (1 + CDbl(Replace(aliquota_iva.Text, " %", "")) / 100)
                        Dim iva_broker As Double = CDbl(a_carico_del_broker.Text) - netto_broker

                        iva.Text = CDbl(iva.Text) - iva_broker
                        imponibile.Text = CDbl(imponibile.Text) - netto_broker

                        iva.Text = FormatNumber(iva.Text, 2, , , TriState.False)
                        imponibile.Text = FormatNumber(imponibile.Text, , , TriState.False)

                        costo_unitario.Visible = True
                        qta.Visible = True
                        qta.Text = "1"
                        costo_unitario.Text = imponibile.Text
                    Else
                        costo_scontato.Visible = False
                        imponibile.Visible = False
                        e.Item.FindControl("onere").Visible = False
                        e.Item.FindControl("iva").Visible = False
                        e.Item.FindControl("aliquota_iva").Visible = False
                    End If
                End If
            End If

            If tariffa_broker.Text = "1" Then
                'COSTI BROKER VISUALIZZATI SOLO CON PERMESSO
                If (livello_accesso_broker.Text = "3" Or livello_accesso_broker.Text = "2") Then
                    'L'UNICO ACCESSORIO A CARICO DEL BROKER E' IL VALORE TARIFFA
                    If id_elemento.Text = Costanti.ID_tempo_km Then
                        Dim a_carico_to As Label = e.Item.FindControl("a_carico_to")
                        e.Item.FindControl("labelTO").Visible = True
                        a_carico_to.Visible = True
                        If dropVariazioneACaricoDi.SelectedValue = "1" Then
                            a_carico_to.Text = FormatNumber(Replace(costo_scontato.Text, "€", ""), 2, , , TriState.False)
                        Else
                            a_carico_to.Text = FormatNumber(Replace(a_carico_del_broker.Text, "€", ""), 2, , , TriState.False)
                        End If
                    End If
                    If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
                        Dim a_carico_to As Label = e.Item.FindControl("a_carico_to")
                        a_carico_to.Visible = True
                        If dropVariazioneACaricoDi.SelectedValue = "1" Then
                            'NEL CASO DI IMPORTO TOTALMENTE A CARICO DEL BROKER L'INTERNO VALORE TARIFFA E' IL TOTALE DA PAGARE
                            a_carico_to.Text = FormatNumber(Replace(get_tempo_km(), "€", ""), 2, , , TriState.False)
                        Else
                            a_carico_to.Text = FormatNumber(Replace(a_carico_del_broker.Text, "€", ""), 2, , , TriState.False)
                        End If
                        a_carico_to.Font.Bold = True
                        a_carico_to.Font.Size = 12
                    End If
                End If

            End If

            'If (obbligatorio.Text = "False" And id_a_carico_di.Text <> Costanti.id_accessorio_incluso) Or (LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale)) Then
            '    'PER GLI ELEMENTI A SCELTA (O PER LA RIGA TOTALE DOVE E' PRESENTE IL PULSANTE AGGIUNGI ACCESSORIO) NASCONDO I COSTI
            '    'valore_costo.Visible = False
            '    sconto.Visible = False
            '    costo_scontato.Visible = False
            '    costo_unitario.Visible = False
            '    qta.Visible = False

            '    If (LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale)) Then
            '        nome_costo.Visible = False
            '    End If
            'Else
            '    e.Item.FindControl("riga_elementi").Visible = False
            'End If


            txtNoteContratto.Focus()
        End If
    End Sub

    Protected Function get_tempo_km() As String
        Dim id_elemento As Label
        For i = 0 To listContrattiCosti.Items.Count - 1
            id_elemento = listContrattiCosti.Items(i).FindControl("id_elemento")
            If id_elemento.Text = Costanti.ID_tempo_km Then
                Dim costo_scontato As Label = listContrattiCosti.Items(i).FindControl("costo_scontato")
                get_tempo_km = costo_scontato.Text
                Exit For
            End If
        Next
    End Function

    Protected Function getCostoACaricoDelBroker() As Double
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT imponibile_scontato + iva_imponibile_scontato + ISNULL(imponibile_onere,0) + ISNULL(iva_onere,0) FROM contratti_costi WITH(NOLOCK) WHERE id_elemento='" & Costanti.ID_tempo_km & "' AND id_documento='" & idContratto.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)

        getCostoACaricoDelBroker = Cmd.ExecuteScalar()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getCostiACaricoDelBroker() As Double()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT imponibile_scontato, ISNULL(imponibile_onere,0) As imponibile_onere, iva_imponibile+ISNULL(iva_onere,0) As iva ,ISNULL((imponibile_scontato+iva_imponibile_scontato+ISNULL(imponibile_onere,0)+ISNULL(iva_onere,0)),0) As costo_scontato, ISNULL(aliquota_iva,0) As aliquota_iva FROM contratti_costi WITH(NOLOCK) WHERE id_elemento='" & Costanti.ID_tempo_km & "' AND id_documento='" & idContratto.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)

        '0) IMPONIBILE
        '1) ONERE
        '2) IVA
        '3) TOT
        '4) Aliquota iva
        Dim costiBroker(4) As Double

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()
        Rs.Read()

        If Rs.HasRows() Then
            costiBroker(0) = Rs("imponibile_scontato")
            costiBroker(1) = Rs("imponibile_onere")
            costiBroker(2) = Rs("iva")
            costiBroker(3) = Rs("costo_scontato")
            costiBroker(4) = Rs("aliquota_iva")
        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        getCostiACaricoDelBroker = costiBroker
    End Function

    Protected Sub listPagamenti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listPagamenti.ItemDataBound
        Dim preaut_aperta As Label = e.Item.FindControl("preaut_aperta")
        Dim operazione_stornata As Label = e.Item.FindControl("operazione_stornata")
        Dim lblStato As Label = e.Item.FindControl("lblStato")
        Dim id_pos_funzioni_ares As Label = e.Item.FindControl("id_pos_funzioni_ares")
        Dim btnVedi As ImageButton = e.Item.FindControl("vedi")

        If (operazione_stornata.Text & "") = "True" Then
            lblStato.Text = "STORNATA"
        Else
            If id_pos_funzioni_ares.Text = enum_tipo_pagamento_ares.Richiesta Then
                If (preaut_aperta.Text & "") = "True" Then
                    lblStato.Text = "APERTA"
                Else
                    lblStato.Text = "CHIUSA"
                End If
            End If
        End If

        If livello_accesso_dettaglio_pos.Text = "1" Then
            btnVedi.Visible = False
        End If
    End Sub

    Protected Sub listPagamenti_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listPagamenti.ItemCommand
        If e.CommandName = "vedi" Then
            riga_pagamento_pos.Visible = True

            Dim id_pagamento_extra As Label = e.Item.FindControl("ID_CTRLabel")
            Dim funzione As Label = e.Item.FindControl("funzione")
            Dim DATA_OPERAZIONELabel As Label = e.Item.FindControl("DATA_OPERAZIONELabel")
            Dim lblStato As Label = e.Item.FindControl("lblStato")

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE ID_CTR='" & id_pagamento_extra.Text & "'", Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            Rs.Read()

            If Rs.HasRows() Then
                txtPOS_Funzione.Text = funzione.Text
                txtPOS_Stazione.Text = Rs("ID_STAZIONE") & ""
                txtPOS_Cassa.Text = Rs("CASSA") & ""
                txtPOS_Carta.Text = Rs("titolo") & ""
                txtPOS_Intestatario.Text = Rs("intestatario") & ""
                txtPOS_Scadenza.Text = Rs("scadenza") & ""
                txtPOS_NrAut.Text = Rs("nr_aut") & ""
                txtPOS_Operatore.Text = funzioni_comuni.getNomeOperatore(Rs("id_operatore_ares") & "")
                txtPOS_DataOperazione.Text = DATA_OPERAZIONELabel.Text
                txtPOS_TerminalID.Text = Rs("TERMINAL_ID") & ""
                txtPOS_BATCH.Text = Rs("NR_BATCH") & ""
                txtPOS_NrPreaut.Text = Rs("NR_PREAUT") & ""
                txtPOS_ScadenzaPreaut.Text = Rs("scadenza_preaut") & ""
                txtPOS_AcquireID.Text = Rs("acquire_id") & ""
                txtPOS_TransationType.Text = Rs("transation_type") & ""
                txtPOS_ActionCode.Text = Rs("action_code") & ""
                txtPOS_Stato.Text = lblStato.Text

                txtPOS_TotIncassato.Focus()
            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End If
    End Sub

    Protected Sub btnAnnulla7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla7.Click
        riga_pagamento_pos.Visible = False
    End Sub

    Protected Sub nuovo_accessorio(ByVal id_accessorio As String, ByVal id_gruppo As String, ByVal tipo As String, ByVal eta_primo As String, ByVal eta_secondo As String, ByVal giorni_restanti_x_nolo_in_corso As String, ByVal data_aggiunta_x_nolo_in_corso As String)
        Dim id_tariffe_righe As String = ""

        If dropTariffeGeneriche.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
        End If

        Dim sconto As Double
        If dropTipoSconto.SelectedValue = "0" Then
            sconto = CDbl(txtSconto.Text)
        ElseIf dropTipoSconto.SelectedValue = "1" Then
            sconto = 0
        End If

        If tipo = "YOUNG" Then
            Dim prepagato As Boolean
            Dim giorni_prepagati As Integer
            If txtGiorniPrepagati.Visible Then
                prepagato = True
                giorni_prepagati = txtGiorniPrepagati.Text
            Else
                prepagato = False
                giorni_prepagati = 0
            End If
            funzioni.calcola_costo_joung_driver_secondo_guidatore(id_accessorio, CInt(dropStazionePickUp.SelectedValue), CInt(dropStazioneDropOff.SelectedValue), eta_primo, eta_secondo, id_gruppo, txtNumeroGiorni.Text, giorni_prepagati, prepagato, sconto, id_tariffe_righe, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
        ElseIf tipo = "YOUNG PRIMO" Then
            Dim prepagato As Boolean
            Dim giorni_prepagati As Integer
            If txtGiorniPrepagati.Visible Then
                prepagato = True
                giorni_prepagati = txtGiorniPrepagati.Text
            Else
                prepagato = False
                giorni_prepagati = 0
            End If
            funzioni.calcola_costo_joung_driver_primo_guidatore(id_accessorio, CInt(dropStazionePickUp.SelectedValue), CInt(dropStazioneDropOff.SelectedValue), eta_primo, eta_secondo, id_gruppo, txtNumeroGiorni.Text, giorni_prepagati, prepagato, sconto, id_tariffe_righe, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
        ElseIf tipo = "VAL_GPS" Then
            Dim prepagato As Boolean
            Dim giorni_prepagati As Integer
            If txtGiorniPrepagati.Visible Then
                prepagato = True
                giorni_prepagati = txtGiorniPrepagati.Text
            Else
                prepagato = False
                giorni_prepagati = 0
            End If
            funzioni.aggiungi_val_gps(CInt(dropStazionePickUp.SelectedValue), CInt(dropStazioneDropOff.SelectedValue), id_gruppo, txtNumeroGiorni.Text, giorni_prepagati, prepagato, Nothing, "", "", sconto, id_tariffe_righe, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
        ElseIf tipo = "ELEMENTO" Then
            Dim prepagato As Boolean
            Dim giorni_prepagati As Integer
            If txtGiorniPrepagati.Visible Then
                prepagato = True
                giorni_prepagati = txtGiorniPrepagati.Text
            Else
                prepagato = False
                giorni_prepagati = 0
            End If


            funzioni.calcola_costo_elemento_extra(id_accessorio, CInt(dropStazionePickUp.SelectedValue), CInt(dropStazioneDropOff.SelectedValue), eta_primo, eta_secondo, id_gruppo, txtNumeroGiorni.Text, giorni_prepagati, prepagato, giorni_restanti_x_nolo_in_corso, data_aggiunta_x_nolo_in_corso, sconto, id_tariffe_righe, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
        End If
    End Sub

    Protected Sub listContrattiCosti_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles listContrattiCosti.ItemCommand
        If e.CommandName = "Aggiorna" Then
            Dim msg As String = ""

            Dim stato_contratto As String = getStatoContratto()

            Dim sconto As Double
            If dropTipoSconto.SelectedValue = "0" Then
                sconto = CDbl(txtSconto.Text)
            ElseIf dropTipoSconto.SelectedValue = "1" Then
                sconto = 0
            End If

            If stato_contratto = statoContratto.Text Then
                If stato_contratto = "0" Or stato_contratto = "1" Or stato_contratto = "2" Or stato_contratto = "3" Or stato_contratto = "4" Or stato_contratto = "8" Then
                    'AGGIORNA IL COSTO DEL CONTRATTO (PER SINGOLO GRUPPO) AGGIUNGENDO O RIMUOVENDO IN COSTO DI UNO O PIU' ACCESSORI SELEZIOANTI
                    Dim id_gruppoScelto As Label = e.Item.FindControl("id_gruppoLabel")
                    Dim id_gruppo As Label
                    Dim check_attuale As CheckBox
                    Dim check_old As CheckBox
                    Dim id_elemento As Label
                    Dim omaggiato As CheckBox
                    Dim old_omaggiato As CheckBox
                    Dim data_aggiunta_nolo_in_corso As Label
                    Dim is_gps As Label
                    Dim num_elemento As Label
                    Dim tipologia_franchigia As Label

                    Dim id_pieno_carburante As String = funzioni_comuni.get_id_pieno_carburante()

                    'SCORRO LA LISTA CERCANDO, ALL'INTERNO DEL GRUPPO SELEZIONATO, GLI ELEMENTI SELEZIONABILI (OVVERO OVE LA CHECKBOX E' SELEZIONATA)
                    'PER QUESTI ELEMENTI CONTROLLO SE E' STATO SELEZIONATO (E NON LO ERA PRECEDENTEMENTE) O SE E' STATO DESELEZIONATO (RISPETTO
                    'ALLA VOLTA PRECEDENTE) AGGIORNANDO IL TOTALE

                    For i = 0 To listContrattiCosti.Items.Count - 1
                        id_gruppo = listContrattiCosti.Items(i).FindControl("id_gruppoLabel")
                        If id_gruppo.Text = id_gruppoScelto.Text Then
                            check_attuale = listContrattiCosti.Items(i).FindControl("chkScegli")
                            check_old = listContrattiCosti.Items(i).FindControl("chkOldScegli")
                            id_elemento = listContrattiCosti.Items(i).FindControl("id_elemento")
                            omaggiato = listContrattiCosti.Items(i).FindControl("chkOmaggio")
                            is_gps = listContrattiCosti.Items(i).FindControl("is_gps")
                            num_elemento = listContrattiCosti.Items(i).FindControl("num_elemento")
                            tipologia_franchigia = listContrattiCosti.Items(i).FindControl("tipologia_franchigia")

                            If check_attuale.Visible Or (Not check_attuale.Visible And omaggiato.Visible) Then
                                old_omaggiato = listContrattiCosti.Items(i).FindControl("chkOldOmaggio")
                                'SE E' E' UNA NUOVA SELEZIONE O SE E' STATO RICHIESTO L'OMAGGIO SENZA SELEZIONARE IL CHECK 'seleziona' (A MENO CHE L'ELEMENTO NON SIA STATO PRECEDENTEMENTE SELEZIONATO)
                                If (check_attuale.Checked And Not check_old.Checked) Or (omaggiato.Checked And Not old_omaggiato.Checked And Not (check_attuale.Checked And check_old.Checked)) Then
                                    'AGGIUNGO IL COSTO DELL'ACCESSORIO AL TOTALE O LO OMAGGIO - IL SECONDO GUIDATORE LO SI AGGIUNGE UNICAMENTE
                                    'SELEZIONANDONE UNO (DA QUESTA PAGINA) - PRIMA DI SELEZIONARE IL PIENO CARBURANTE E' NECESSARIO SELEZIONARE
                                    'UNA VETTURA
                                    If (id_elemento.Text <> Costanti.Id_Secondo_Guidatore) And Not (id_elemento.Text = id_pieno_carburante And id_auto_selezionata.Text = "") Then

                                        If id_elemento.Text = id_pieno_carburante Then
                                            'E' NECESSARIO AGGIORNARE IL COSTO DEL PIENO CARBURANTE PRIMA DI AGGIUNGERE IL COSTO O DI OMAGGIARLO
                                            aggiorna_costo_pieno_carburante(idContratto.Text, numCalcolo.Text, CInt(dropStazionePickUp.SelectedValue), id_alimentazione.Text, CInt(lblSerbatoioMax.Text), id_pieno_carburante, sconto)
                                        End If

                                        check_attuale.Checked = True 'NEL CASO IN CUI SIA STATO SELEZIONATO SOLO 'OMAGGIO'

                                        If omaggiato.Checked Then
                                            'NEL CASO DI SERVIZIO RIFORNIMENTO (DA GESTIRE AUTOMATICAMENTE) NON E' POSSIBILE 
                                            'SELEZIONARE NON PAG. SE L'ACCESSORIO NON E' STATO AGGIUNTO AUTOMATICAMENTE AL COSTO
                                            Dim servizio_rifornimento_tolleranza As Label = listContrattiCosti.Items(i).FindControl("servizio_rifornimento_tolleranza")
                                            If servizio_rifornimento_tolleranza.Text <> "" Then
                                                msg = msg & "- Il SERVIZIO RIFORNIMENTO deve essere aggiunto automaticamente al totale dal sistema prima di poter selezionare DA NON PAGARE." & vbCrLf
                                            Else
                                                funzioni.omaggio_accessorio(True, False, False, "", "", "", idContratto.Text, numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text)
                                            End If
                                        Else
                                            If stato_contratto <> "2" Then
                                                aggiungi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, "", "", "", False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                                            Else
                                                'NEL CASO IN CUI IL NOLEGGIO SIA IN CORSO E' NECESSARIO PASSARE ALLA FUNZIONE I GIORNI DI NOLEGGIO
                                                'RESTANTI: NEL CASO IN CUI L'ACCESSORIO E' A COSTO GIORNALIERO VERRANNO CALCOLATI SOLAMENTE I GIORNI
                                                'DI NOLEGGIO RESTANTI.
                                                Dim id_unita_misura As Label = listContrattiCosti.Items(i).FindControl("id_unita_misura")
                                                If id_unita_misura.Text = Costanti.id_unita_misura_giorni Then
                                                    data_aggiunta_nolo_in_corso = listContrattiCosti.Items(i).FindControl("data_aggiunta_nolo_in_corso")
                                                    If data_aggiunta_nolo_in_corso.Text = "" Then
                                                        'SE IL COSTO E' GIORNALIERO CALCOLO I GIORNI RESTANTI (DAL MOMENTO DELL'AGGIUNTA DEL COSTO
                                                        'FINO ALLA DATA DI DROP OFF) - QUESTO NEL CASO IN CUI IL COSTO VIENE AGGIUNTO PER LA PRIMA VOLTA
                                                        Dim id_tariffe_righe As String

                                                        If dropTariffeGeneriche.SelectedValue <> "0" Then
                                                            id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
                                                        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                                                            id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
                                                        End If

                                                        Dim giorni_restanti As Integer = getGiorniDiNoleggio(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()), txtAData.Text, Hour(Now()), Minute(Now()), ore2.Text, minuti2.Text, id_tariffe_righe)

                                                        If giorni_restanti = txtNumeroGiorni.Text Then
                                                            'SE IL NUMERO DI GIORNI CALCOLATO E' IDENTICO AI GIORNI EFFETTUO IL CALCOLO NORMALMENTE
                                                            funzioni_comuni.aggiungi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, "", "", "", False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                                                        Else
                                                            'ALTRIMENTI FACCIO IN MODO DI CALCOLARE IL COSTO PER I GIORNI RESTANTI
                                                            funzioni_comuni.aggiungi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, giorni_restanti, sconto, Now(), False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                                                        End If
                                                    Else
                                                        'IN QUESTO CASO IL COSTO ERA GIA' STATO AGGIUNTO IN UNO DEI CALCOLI PRECEDENTI PER CUI
                                                        'CONSIDERO QUELLA SALVATA COME DATA INIZIALE
                                                        Dim id_tariffe_righe As String

                                                        If dropTariffeGeneriche.SelectedValue <> "0" Then
                                                            id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
                                                        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                                                            id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
                                                        End If

                                                        Dim giorni_restanti As Integer = getGiorniDiNoleggio(Day(data_aggiunta_nolo_in_corso.Text) & "/" & Month(data_aggiunta_nolo_in_corso.Text) & "/" & Year(data_aggiunta_nolo_in_corso.Text), txtAData.Text, Hour(data_aggiunta_nolo_in_corso.Text), Minute(data_aggiunta_nolo_in_corso.Text), ore2.Text, minuti2.Text, id_tariffe_righe)

                                                        If giorni_restanti = txtNumeroGiorni.Text Then
                                                            'SE IL NUMERO DI GIORNI CALCOLATO E' IDENTICO AI GIORNI EFFETTUO IL CALCOLO NORMALMENTE
                                                            funzioni_comuni.aggiungi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, "", "", "", False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                                                        Else
                                                            'ALTRIMENTI FACCIO IN MODO DI CALCOLARE IL COSTO PER I GIORNI RESTANTI
                                                            funzioni_comuni.aggiungi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, giorni_restanti, sconto, data_aggiunta_nolo_in_corso.Text, False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                                                        End If
                                                    End If
                                                Else
                                                    'SE L'UNITA' DI MISURA DEL COSTO NON E' GIORNALIERA AGGIUNGO NORMLAMENTE IL COSTO
                                                    '(NON DIPENDENDO DAI GIORNI DI NOLEGGIO AGGIUNGO IL COSTO PER INTERO)
                                                    funzioni_comuni.aggiungi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, "", "", "", False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                                                End If
                                            End If
                                        End If
                                        'SE E' STATO AGGIUNTO IL SECONDO GUIDATORE DEVO AGGIUNGERE, SE NECESSARIO, IL COSTO DELLO YOUNG DRIVER
                                        If id_elemento.Text = Costanti.Id_Secondo_Guidatore Then
                                            nuovo_accessorio(get_id_young_driver(), id_gruppoScelto.Text, "YOUNG", txtEtaPrimo.Text, txtEtaSecondo.Text, "", "")
                                        End If
                                        'SE E' STATO AGGINTO IL GPS AGGIUNGO EVENTUALMENTE IL VAL - MOSTRO LA SEZIONE GPS
                                        If is_gps.Text = "True" Then
                                            div_gps.Visible = True

                                            If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
                                                nuovo_accessorio("", id_gruppoScelto.Text, "VAL_GPS", txtEtaPrimo.Text, txtEtaSecondo.Text, "", "")
                                            End If
                                        End If
                                    Else
                                        '(id_elemento.Text <> Costanti.Id_Secondo_Guidatore) And Not (id_elemento.Text = id_pieno_carburante And id_auto_selezionata.Text = "")
                                        If id_elemento.Text = Costanti.Id_Secondo_Guidatore Then
                                            msg = msg & "- Selezionare il secondo guidatore per aggiungere l'accessorio corrispondente." & vbCrLf
                                        ElseIf (id_elemento.Text = id_pieno_carburante And id_auto_selezionata.Text = "") Then
                                            msg = msg & "- Selezionare un veicolo prima di aggiungere il pieno carburante." & vbCrLf
                                        End If

                                        check_attuale.Checked = False
                                    End If
                                ElseIf (check_attuale.Checked And check_old.Checked) And (omaggiato.Checked And Not old_omaggiato.Checked) Then
                                    'DOPO AVER AGGIUNTO IL COSTO DELL'ACCESSORIO AL TOTALE SI DECIDE DI OMAGGIARLO
                                    funzioni.omaggio_accessorio(True, False, False, "", "", "", idContratto.Text, numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text)
                                ElseIf (check_attuale.Checked And check_old.Checked) And (Not omaggiato.Checked And old_omaggiato.Checked) Then
                                    'DOPO AVER AGGIUNTO IL COSTO DELL'ACCESSORIO E AVERLO OMAGGIATO TOLGO L'OMAGGIO
                                    funzioni.omaggio_accessorio(False, True, False, "", "", "", idContratto.Text, numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text)
                                ElseIf Not check_attuale.Checked And check_old.Checked And Not omaggiato.Checked Then
                                    'TOLGO IL COSTO DELL'ACCESSORIO DAL TOTALE A MENO CHE NON ERA OMAGGIATO
                                    If old_omaggiato.Checked Then
                                        funzioni.omaggio_accessorio(False, False, True, "", "", "", idContratto.Text, numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text)
                                    Else
                                        funzioni.rimuovi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, id_gruppoScelto.Text, id_elemento.Text, "", "SCELTA", dropTipoCommissione.SelectedValue)
                                    End If

                                    If id_elemento.Text = Costanti.Id_Secondo_Guidatore Then
                                        msg = msg & "- Secondo guidatore rimosso" & vbCrLf

                                        txtCognomeSecondoConducente.Text = ""
                                        txtNomeSecondoConducente.Text = ""
                                        txtCittaSecondaConducente.Text = ""
                                        txtIndirizzoSecondoConducente.Text = ""
                                        txtPatenteSecondoConducente.Text = ""
                                        txtDocumentoSecondoConducente.Text = ""

                                        txtEtaSecondo.Text = ""
                                        idSecondoConducente.Text = ""

                                        image_secondo_guidatore.Visible = False

                                        Dim id_young_driver As String = get_id_young_driver()
                                        'RIMUOVO IL COSTO DELLO JOUNG DRIVER NEL CASO IN CUI SI RIMUOVE "SECONDO GUIDATORE"
                                        If funzioni.esiste_young_driver_secondo_guidatore(id_young_driver, id_gruppoScelto.Text, numCalcolo.Text, "", "", idContratto.Text, "") Then
                                            funzioni.rimuovi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, id_gruppoScelto.Text, id_young_driver, "2", "EXTRA", dropTipoCommissione.SelectedValue)
                                        End If
                                    End If

                                    If is_gps.Text = "True" Then
                                        'RIMOZIONE DEL GPS - RIMUOVO LA SEZIONE DELLA SCELTA GPS
                                        div_gps.Visible = False
                                        lblIdGps.Text = ""
                                        txtCodiceGps.Text = ""
                                        txtCodiceGps.Enabled = True

                                        'SE IL CONTRATTO E' NELLO STATO check_out PER CUI IL CONTRATTO E' SALVATO MA SI ASPETTA L'USCITA DEL MEZZO E' ANCORA POSSIBILE MODIFICARE
                                        'IL GPS COLLEGATO AL CONTRATTO. RIMUOVENDO L'ACCESSORIO SATELLITARE E' NECESSARIO SALVARE L'INFORMAZIONE SU DB
                                        If stato_contratto = Costanti.stato_contratto.check_out Then
                                            modifica_gps_cnt_prima_del_check_out()

                                            msg = msg & "- GPS rimosso correttamente dal contratto." & vbCrLf
                                        End If

                                        If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
                                            'RIMOZIONE DEL VAL GPS
                                            Dim id_val_gps As String = funzioni_comuni.getIdValGps()
                                            If id_val_gps <> "" Then
                                                funzioni.rimuovi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, id_gruppoScelto.Text, id_val_gps, "", "EXTRA", dropTipoCommissione.SelectedValue)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Next
                    listContrattiCosti.DataBind()

                    aggiorna_informazioni_dopo_modifica_costi()

                    If stato_contratto = Costanti.stato_contratto.da_incassare Or stato_contratto = Costanti.stato_contratto.da_fatturare Then
                        'IN QUESTO CASO AGGIORNO IL TOTALE DA PAGARE SALVATO SULLA RIGA DI CONTRATTO
                        lblSaldo.Text = FormatNumber(getTotaleDaPagare(), 2, , , TriState.False)
                        aggiorna_totale_da_pagare(lblSaldo.Text)

                        lblSaldo.Text = "Saldo: " & lblSaldo.Text & " €"
                    End If
                End If
            Else
                msg = "Attenzione: è necessario ricaricare il contratto prima di poter apportare modifiche. Ritornare alla pagina di ricerca." & stato_contratto
            End If

            If msg <> "" Then
                Libreria.genUserMsgBox(Me, msg)
            End If
            txtNoteContratto.Focus()
        End If
    End Sub

    Protected Sub aggiorna_totale_da_pagare(ByVal totale_da_pagare As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE contratti SET totale_da_incassare='" & Replace(totale_da_pagare, ",", ".") & "' WHERE id='" & idContratto.Text & "'", Dbc)

        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub getTotaleDaPreautorizzare()
        'E' NECESSARIO PREAUTORIZZARE: FRANCHIGIE + TOTALE + NUM GIORNI DI NOLEGGIO PRELEVATI DA TABELLA contratti_giorni_da_preautorizzare
        '1 PIENO DI BENZINA. QUESTO BLOCCO AGGIORNA LA DROPDOWN LIST DEL TOTALE DA PREAUTORIZZARE IN BASE AGLI SCONTI SU FRANCHIGIA DISPONIBILI

        'CERCO LE EVENTUALI FRANCHIGIE DA PREAUTORIZZARE - E' IL PIU' ALTO COSTO TRA LE FRANCHGIE ATTIVE
        Dim franchigia_da_preautorizzare As Double = 0
        Dim importo_totale As Double = 0
        Dim giorni_extra_da_preautorizzare As Double = 0
        Dim importo_benzina As Double = 0

        Dim numero_giorni_da_preautorizzare As String

        Dim franchigia_attiva As Label
        Dim costo_scontato As Label
        Dim nome_costo As Label

        For i = 0 To listContrattiCosti.Items.Count - 1
            nome_costo = listContrattiCosti.Items(i).FindControl("nome_costo")
            franchigia_attiva = listContrattiCosti.Items(i).FindControl("franchigia_attiva")
            If franchigia_attiva.Text = "True" Then
                costo_scontato = listContrattiCosti.Items(i).FindControl("costo_scontato")
                If CDbl(costo_scontato.Text) > franchigia_da_preautorizzare Then
                    franchigia_da_preautorizzare = CDbl(costo_scontato.Text)
                End If
            End If
            'NE APPROFITTO PER RECUPERARE IL COSTO DEL TOTALE CHE MI SERVIRA' DOPO
            If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
                costo_scontato = listContrattiCosti.Items(i).FindControl("costo_scontato")
                importo_totale = CDbl(costo_scontato.Text)
            End If
        Next

        'CALCOLO I NUMERO DI GIORNI DI NOLEGGIO DA PREAUTORIZZARE - PRIMA CONTROLLO SE C'E' UNA REGOLA SPECIFICA PER LA STAZIONE DI PICK UP
        'ALTRIMENTI SELZIONO LA REGOLA GENERICA--------------------------------------------------------------------------------------------
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT numero_giorni FROM contratti_giorni_da_preautorizzare WITH(NOLOCK) WHERE id_stazione='" & CInt(dropStazionePickUp.SelectedValue) & "'", Dbc)

        numero_giorni_da_preautorizzare = Cmd.ExecuteScalar & ""

        If numero_giorni_da_preautorizzare = "" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(numero_giorni,1) FROM contratti_giorni_da_preautorizzare WITH(NOLOCK) WHERE id_stazione IS NULL", Dbc)
            numero_giorni_da_preautorizzare = Cmd.ExecuteScalar
        End If

        giorni_extra_da_preautorizzare = (importo_totale / CInt(txtNumeroGiorni.Text)) * numero_giorni_da_preautorizzare
        '----------------------------------------------------------------------------------------------------------------------------------
        'IMPORTO BENZINA DA PREAUTORIZZARE ------------------------------------------------------------------------------------------------
        'NON E' NECESSARIO PREAUTORIZZARE IL PIENO BENZIA SE E' STATO ACQUISTATO L'ACCESSORIO 'PIENO BENZINA'
        ' RIMOSSO SU RICHIESTA DI FRANCESCO SCALIA
        'If id_auto_selezionata.Text <> "" Then
        '    If Not funzioni_comuni.pieno_carburante_selezionato(idContratto.Text, numCalcolo.Text) Then
        '        If lblSerbatoioMax.Text = "" Then
        '            lblSerbatoioMax.Text = "0"
        '        End If

        '        importo_benzina = CDbl(lblSerbatoioMax.Text) * getCostoCarburante_x_litro(CInt(dropStazionePickUp.SelectedValue), id_alimentazione.Text)
        '    End If
        'End If
        '----------------------------------------------------------------------------------------------------------------------------------

        lblDaPreautorizzare.Items.Clear()
        lblDaPreautorizzare.Items.Add(FormatNumber(franchigia_da_preautorizzare + importo_totale + giorni_extra_da_preautorizzare + importo_benzina, 2, , , TriState.False))

        If franchigia_da_preautorizzare <> 0 Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT sconto FROM contratti_sconto_franchigia WITH(NOLOCK) WHERE id_stazione='" & CInt(dropStazionePickUp.SelectedValue) & "' ORDER BY sconto ASC", Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Dim franchigia_scontata As Double

            If Rs.HasRows Then
                'TROVATA REGOLA PER STAZIONE
                Do While Rs.Read()
                    franchigia_scontata = franchigia_da_preautorizzare - (franchigia_da_preautorizzare * Rs("sconto") / 100)
                    lblDaPreautorizzare.Items.Add(FormatNumber(franchigia_scontata + importo_totale + giorni_extra_da_preautorizzare + importo_benzina, 2, , , TriState.False))
                Loop
                Rs.Close()
                Rs = Nothing
            Else
                Rs.Close()
                Rs = Nothing
                Dbc.Close()
                Dbc.Open()
                Cmd = New Data.SqlClient.SqlCommand("SELECT sconto FROM contratti_sconto_franchigia WITH(NOLOCK) WHERE id_stazione IS NULL ORDER BY sconto ASC", Dbc)
                Rs = Cmd.ExecuteReader()
                If Rs.HasRows() Then
                    'TROVATA REGOLA GENERICA
                    Do While Rs.Read()
                        franchigia_scontata = franchigia_da_preautorizzare - (franchigia_da_preautorizzare * Rs("sconto") / 100)
                        lblDaPreautorizzare.Items.Add(FormatNumber(franchigia_scontata + importo_totale + giorni_extra_da_preautorizzare + importo_benzina, 2, , , TriState.False))
                    Loop
                End If

                Rs.Close()
                Rs = Nothing
            End If
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub aggiorna_informazioni_dopo_modifica_costi()
        'IMPORTO DA PREAUTORIZZARE (SE IL CONTRATTO NON E' IN CORSO)
        If statoContratto.Text = "0" Or statoContratto.Text = "1" Then
            If lblDaPreautorizzare.Visible Then
                getTotaleDaPreautorizzare()
            End If
        End If

        'TOTALE DA INCASSARE NEL CASO DI PRENOTAZIONE PREPAGATA
        If prenotazione_prepagata.Text = "True" Then
            lblPrepagata1.Visible = True
            'lblPrepagata2.Visible = True
            'lblEuroDaIncassare.Visible = True
            'lblDaIncassare.Visible = True

            'lblDaIncassare.Text = getTotaleDaPagare()
            'lblDaIncassare.Text = FormatNumber(lblDaIncassare.Text, 2, , , TriState.False)
        End If

        'DIFFERENZA COSTI RISPETTO PRENOTAZIONE 
        If idPrenotazione.Text <> "" Then
            Dim nome_costo As Label
            Dim totale_contratto As Double
            Dim costo_scontato As Label
            For i = 0 To listContrattiCosti.Items.Count - 1
                nome_costo = listContrattiCosti.Items(i).FindControl("nome_costo")

                If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
                    costo_scontato = listContrattiCosti.Items(i).FindControl("costo_scontato")
                    totale_contratto = CDbl(costo_scontato.Text)
                    'If tariffa_broker.Text = "1" Then
                    '    'PER IL CORRETTO CALCOLO, NEL CASO DI BROKER, DEVO RIAGGIUNGERE AL TOTALE IL VALORE A CARICO DEL BROKER CHE VIENE TOLTO
                    '    'DALLA TABELLA PER MOSTRARE IL COSTO A CARICO DEL CLIENTE
                    '    totale_contratto = totale_contratto + getCostoACaricoDelBroker()
                    'End If
                End If
            Next

            If totale_contratto <> CDbl(totale_prenotazione.Text) Then
                lblTestoDaPrenotazione.Visible = True
                lblDifferenzaDaPrenotazione.Visible = True
                lblEuroDaPrenotazione.Visible = True

                lblDifferenzaDaPrenotazione.Text = FormatNumber(totale_contratto - CDbl(totale_prenotazione.Text), 2, , , TriState.False)
            Else
                lblTestoDaPrenotazione.Visible = False
                lblDifferenzaDaPrenotazione.Visible = False
                lblEuroDaPrenotazione.Visible = False

                lblDifferenzaDaPrenotazione.Text = "0"
            End If
        Else
            lblTestoDaPrenotazione.Visible = False
            lblDifferenzaDaPrenotazione.Visible = False
            lblEuroDaPrenotazione.Visible = False

            lblDifferenzaDaPrenotazione.Text = "0"
        End If
    End Sub

    Protected Sub btnScegliPrimoGuidatore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScegliPrimoGuidatore.Click
        anagrafica_conducenti1.Visible = True

        If txtCognomeConducente.Text <> "" Then
            anagrafica_conducenti1.cerca_esterno(txtCognomeConducente.Text & " " & txtNomeConducente.Text)
        ElseIf txtCognomeConducentePreventivo.Text <> "" Then
            anagrafica_conducenti1.cerca_esterno(txtCognomeConducentePreventivo.Text & " " & txtNomeConducentePreventivo.Text)
        End If

        btnScegliPrimoGuidatore.Visible = False
        btnScegliSecondoConducente.Visible = False
        btnScegliDitta.Visible = False
        btnAnnullaScegliPrimoConducente.Visible = True

        conducente_da_variare.Text = "1"
    End Sub

    Protected Sub btnAnnullaScegliPrimoConducente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnullaScegliPrimoConducente.Click
        anagrafica_conducenti1.Visible = False
        btnScegliPrimoGuidatore.Visible = True
        btnScegliSecondoConducente.Visible = True
        If ditta_non_modificabile.Text = "False" Then
            btnScegliDitta.Visible = True
        End If
        btnAnnullaScegliPrimoConducente.Visible = False



        conducente_da_variare.Text = ""
    End Sub

    Protected Sub btnScegliSecondoConducente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScegliSecondoConducente.Click
        If txtCognomeConducente.Text <> "" Then
            anagrafica_conducenti1.cerca_esterno(txtCognomeConducente.Text & " " & txtNomeConducente.Text)
        ElseIf txtCognomeConducentePreventivo.Text <> "" Then
            anagrafica_conducenti1.cerca_esterno(txtCognomeConducentePreventivo.Text & " " & txtNomeConducentePreventivo.Text)
        End If

        anagrafica_conducenti1.Visible = True
        btnScegliPrimoGuidatore.Visible = False
        btnScegliSecondoConducente.Visible = False
        btnScegliDitta.Visible = False
        btnAnnullaScegliSecondoConducente.Visible = True

        conducente_da_variare.Text = "2"
    End Sub

    Protected Sub btnAnnullaScegliSecondoConducente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnullaScegliSecondoConducente.Click
        anagrafica_conducenti1.Visible = False
        btnScegliPrimoGuidatore.Visible = True
        btnScegliSecondoConducente.Visible = True
        If ditta_non_modificabile.Text = "False" Then
            btnScegliDitta.Visible = True
        End If
        btnAnnullaScegliSecondoConducente.Visible = False

        conducente_da_variare.Text = ""
    End Sub

    Protected Function tariffa_vendibile(ByVal id_tariffe_righe As String) As Boolean

        Dim daData As String = funzioni_comuni.getDataDb_senza_orario(txtDaData.Text)
        Dim aData As String = funzioni_comuni.getDataDb_senza_orario(txtAData.Text)

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "(SELECT tariffe_righe.id FROM tariffe WITH(NOLOCK) " &
        "INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
        "WHERE tariffe.attiva='1' AND tariffe_righe.id=" & id_tariffe_righe & " AND '" & daData & "' BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " &
        "AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a " &
        "AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_X_stazioni WITH(NOLOCK)) " &
        "AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_X_fonti WITH(NOLOCK)) " &
        "AND '" & aData & "' <= ISNULL(max_data_rientro, '3000-12-12 23:59:59'))" 'MODIFICARE LA QUERY ANCHE IN RIBALTAMENTO.ASPX

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test <> "" Then
            tariffa_vendibile = True
        Else
            'PARTE 2: CONTROLLO LE TARIFFE VENDIBILI PER LA STAZIONE SPECIFICATA E PER L'EVENTUALE FONTE 
            'COME ID SELEZIONO DIRETTAMENTE LA RIGA DI tariffe_righe DA CUI POI POSSO SUBITO OTTENERE LA CONDIZIONE E IL TEMPO-KM
            'DA UTILIZZARE
            sqlStr = "SELECT tariffe_righe.id FROM tariffe WITH(NOLOCK) " &
            "INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
            "WHERE tariffe.attiva='1' AND tariffe_righe.id=" & id_tariffe_righe & " AND '" & daData & "' BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " &
            "AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a " &
            "AND '" & aData & "' <= ISNULL(max_data_rientro, '3000-12-12 23:59:59') " &
            "AND ( (" &
            "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & CInt(dropStazionePickUp.SelectedValue) & "' AND tipo='PICK')) AND " &
            "(tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & CInt(dropStazioneDropOff.SelectedValue) & "' AND tipo='DROP'))) " &
            " OR " &
            "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & CInt(dropStazionePickUp.SelectedValue) & "' AND tipo='PICK')) AND " &
            "(tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='DROP'))) " &
            " OR " &
            "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & CInt(dropStazioneDropOff.SelectedValue) & "' AND tipo='DROP')) AND " &
            "(tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='PICK'))) ) " 'MODIFICARE LA QUERY ANCHE IN RIBALTAMENTO.ASPX

            If dropTipoCliente.SelectedValue > 0 Then
                'SE E' STATA SELEZIONATA UNA FONTE CONTROLLO ANCHE TRA I REQUISITI PER FONTE DELLE TARIFFE
                'UNENDO IN OR (L'ULTIMO) LA POSSIBILITA CHE LA TARIFFA NON SPECIFICHI ALCUNA STAZIONE MA SPECIFICHI UNA O PIU' FONTI
                sqlStr = sqlStr & "" &
                "AND (tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tipologia_cliente='" & CInt(dropTipoCliente.SelectedValue) & "' )" &
                "OR tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK))) " &
                "OR (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='PICK') AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='DROP') AND tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tipologia_cliente='" & CInt(dropTipoCliente.SelectedValue) & "' ) )) "
            Else
                'SE NON E' STATA SELEZIONATA UNA FONTE PRELEVO SOLAMENTE LE TARIFFE CHE NON HANNO ALCUN PREREQUISITO DI FONTE
                sqlStr = sqlStr & "" &
                "AND (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK))))"
            End If
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            test = Cmd.ExecuteScalar & ""

            If test <> "" Then
                tariffa_vendibile = True
            Else
                tariffa_vendibile = False
            End If
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    'Protected Sub modifica_calcolo_cnt_in_corso()
    '    'A CONTRATTO IN CORSO E' POSSIBILE VARIARE UNICAMENTE DATA - STAZIONE - ORARIO DI RIENTRO
    '    Dim drop_off As String = funzioni_comuni.stazione_aperta_drop_off(dropStazioneDropOff.SelectedValue, txtAData.Text, Hour(txtOraRientro.Text), Minute(txtOraRientro.Text), "", "", "", "", "", Request.Cookies("SicilyRentCar")("idUtente"))

    '    If drop_off = "2" Then
    '        Libreria.genUserMsgBox(Me, " Stazione di drop off chiusa.")
    '    End If

    '    ore2.Text = Hour(txtOraRientro.Text)
    '    minuti2.Text = Minute(txtOraRientro.Text)

    '    esegui_ricalcolo()
    'End Sub

    Protected Function modifica_calcolo() As Boolean
        Dim pick_up As String = funzioni_comuni.stazione_aperta_pick_up(CInt(dropStazionePickUp.SelectedValue), txtDaData.Text, Hour(txtoraPartenza.Text), Minute(txtoraPartenza.Text), "", "", "", "", "", Request.Cookies("SicilyRentCar")("idUtente"))
        Dim drop_off As String = funzioni_comuni.stazione_aperta_drop_off(CInt(dropStazioneDropOff.SelectedValue), txtAData.Text, Hour(txtOraRientro.Text), Minute(txtOraRientro.Text), "", "", "", "", "", Request.Cookies("SicilyRentCar")("idUtente"))

        ore1.Text = Hour(txtoraPartenza.Text)
        minuti1.Text = Minute(txtoraPartenza.Text)

        ore2.Text = Hour(txtOraRientro.Text)
        minuti2.Text = Minute(txtOraRientro.Text)

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

        Dim gruppo_vendibile As String = funzioni_comuni.gruppo_vendibile_eta_guidatori(CInt(gruppoDaCalcolare.SelectedValue), Trim(txtEtaPrimo.Text), Trim(txtEtaSecondo.Text), "", "", "", "", "", "", False)

        Dim messaggio As String = ""

        'ESEGUO IN OGNI CASO IL RICALCOLO - POTREBBERO ESSERE AGGIUNTI COSTI A PRESCINDERE DALLA VARIAZIONE DEI GIORNI DI NOLEGGIO
        'IL WARNING SULL'ETA' DEI GUIDATORI E' BLOCCANTE!!!
        If gruppo_vendibile <> "0" Then
            If (dropTariffeGeneriche.SelectedValue <> "0" And dropTariffeParticolari.SelectedValue <> "0") Or (dropTariffeGeneriche.SelectedValue = "0" And dropTariffeParticolari.SelectedValue = "0") Then
                messaggio = messaggio & " Specificare una tariffa generica oppure una particolare."
                modifica_calcolo = False
            Else
                modifica_calcolo = True
            End If
        Else
            modifica_calcolo = False
        End If

        Dim ricalcolo_effettuato As Boolean

        If modifica_calcolo Then
            ricalcolo_effettuato = esegui_ricalcolo()

            If Not ricalcolo_effettuato Then
                modifica_calcolo = False
            End If
        End If

        If gruppo_vendibile = "0" Then
            messaggio = messaggio & " Gruppo non vendibile a causa dell'età di uno o entrambi i guidatori."
        Else
            If ricalcolo_effettuato Then
                If ((gruppoDaConsegnare.SelectedValue = 0 And gruppoDaCalcolare.SelectedValue <> id_gruppo_auto_selezionata.Text) Or (gruppoDaConsegnare.SelectedValue <> 0 And gruppoDaConsegnare.SelectedValue <> id_gruppo_auto_selezionata.Text)) And (id_gruppo_auto_selezionata.Text <> "") Then
                    messaggio = messaggio & " Attenzione: gruppo auto calcolato diverso da quello della vettura selezionata.  "
                End If

                'STAMPO QUESTI MESSAGGI DI INFORMATIVA SOLAMENTE SE IL RICALCOLO (DENTRO esegui_ricalcolo) E' STATO EFFETTUATO)
                If gruppo_vendibile = "1" Or gruppo_vendibile = "2" Then
                    messaggio = messaggio & " Gruppo vendibile con supplemento Young Driver."
                ElseIf gruppo_vendibile = "3" Then
                    messaggio = messaggio & " Gruppo vendibile con supplemento Young Driver per entrambi i guidatori."
                End If

                If pick_up <> "2" And drop_off = "2" Then
                    messaggio = messaggio & " Stazione di pick up chiusa."
                    listWarning.DataBind()
                ElseIf pick_up = "2" And drop_off <> "2" Then
                    messaggio = messaggio & " Stazione di drop off chiusa."
                    listWarning.DataBind()
                ElseIf pick_up <> "2" And drop_off <> "2" Then
                    messaggio = messaggio & " Stazione di di pick up e di drop off chiuse."
                    listWarning.DataBind()
                End If
            End If
        End If

        If messaggio <> "" Then
            Libreria.genUserMsgBox(Page, messaggio)
        End If
    End Function

    Protected Sub esegui_ricalcolo_nolo_in_corso(Optional ByVal ricalcolo_per_stato As String = "2")
        'POSSONO ESSERE VARIATI SOLAMENTE I DATI DI RIENTRO----------------------------------------------------------------------------------
        ore2.Text = Hour(txtOraRientro.Text)
        minuti2.Text = Minute(txtOraRientro.Text)

        If Len(ore2.Text) = 1 Then
            ore2.Text = "0" & ore2.Text
        End If
        If Len(minuti2.Text) = 1 Then
            minuti2.Text = "0" & minuti2.Text
        End If
        '--------------------------------------------------------------------------------------------------
        Dim id_tariffe_righe As String

        If dropTariffeGeneriche.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
        End If

        Dim sconto As Double
        If dropTipoSconto.SelectedValue = "0" Then
            sconto = CDbl(txtSconto.Text)
        ElseIf dropTipoSconto.SelectedValue = "1" Then
            sconto = 0
        End If

        'CALCOLO IL NUMERO DI GIORNI DI NOLEGGIO - PER IL RIENTRO DELLA VETTURA CONSIDERO ANCHE I MINUTI DI TOLLERANZA EXTRA 
        Dim numero_giorni As Integer
        If ricalcolo_per_stato = "3" Or ricalcolo_per_stato = "4" Or ricalcolo_per_stato = "8" Or ricalcolo_per_stato = "6" Then
            numero_giorni = funzioni_comuni.getGiorniDiNoleggio(txtDaData.Text, txtAData.Text, ore1.Text, minuti1.Text, ore2.Text, minuti2.Text, id_tariffe_righe, True)
        Else
            numero_giorni = funzioni_comuni.getGiorniDiNoleggio(txtDaData.Text, txtAData.Text, ore1.Text, minuti1.Text, ore2.Text, minuti2.Text, id_tariffe_righe, False)
        End If

        'SE L'OPERATORE HA DECISO DI ABBUONARE IL GIORNO EXTRA
        If chkAbbuonaGiornoExtra.Checked Then
            If numero_giorni = 1 Then
                chkAbbuonaGiornoExtra.Checked = False
            Else
                numero_giorni = numero_giorni - 1
            End If
        End If

        'CALCOLO GLI EVENTUALI GIORNI MINIMI E MASSIMI DI NOLEGGIO
        Dim min_giorni_nolo As String = funzioni_comuni.getMinGiorniNolo(id_tariffe_righe)
        Dim max_giorni_nolo As String = funzioni_comuni.getMaxGiorniNolo(id_tariffe_righe)

        'WARNING (CALCOLO SOLAMENTE I WARNING CHE POTREBBERO ESSERE AGGIUNTI O RIMOSSI CON LE MODIFICHE EFFETTUABILI SU CNT)-----------------
        'Dim stazione_aperta_pick_up As String = funzioni_comuni.stazione_aperta_pick_up(dropStazionePickUp.SelectedValue, txtDaData.Text, ore1.Text, minuti1.Text, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
        Dim stazione_aperta_drop_off As String = funzioni_comuni.stazione_aperta_drop_off(CInt(dropStazioneDropOff.SelectedValue), txtAData.Text, ore2.Text, minuti2.Text, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))

        'Dim pick_up_on_request As String = funzioni_comuni.stazione_pick_up_on_request(dropStazionePickUp.SelectedValue, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
        Dim drop_off_on_request As String = funzioni_comuni.stazione_drop_off_on_request(CInt(dropStazioneDropOff.SelectedValue), "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))

        Dim data_pick As String = funzioni_comuni.getDataDb_senza_orario(txtDaData.Text)

        'funzioni_comuni.gruppo_vendibile_pick_up(dropStazionePickUp.SelectedValue, gruppoDaCalcolare.SelectedValue, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), True)
        funzioni_comuni.gruppo_vendibile_drop_off(CInt(dropStazioneDropOff.SelectedValue), CInt(gruppoDaCalcolare.SelectedValue), "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), True)

        If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
            funzioni_comuni.gruppo_vendibile_val(CInt(dropStazioneDropOff.SelectedValue), CInt(gruppoDaCalcolare.SelectedValue), "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), True)

            funzioni_comuni.stazione_permette_VAL_verso_altre_stazioni(CInt(dropStazionePickUp.SelectedValue), "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
            funzioni_comuni.stazione_accetta_VAL_da_altre_stazioni(CInt(dropStazioneDropOff.SelectedValue), "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
        End If
        'funzioni_comuni.stazioneInStopSell(dropStazionePickUp.SelectedValue, data_pick, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
        'funzioni_comuni.gruppoInStopSell(dropStazionePickUp.SelectedValue, gruppoDaCalcolare.SelectedValue, data_pick, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), True)

        'If dropTipoCliente.SelectedValue <> "0" Then
        '    funzioni_comuni.gruppoInStopSellPerFonte(dropStazionePickUp.SelectedValue, dropTipoCliente.SelectedValue, gruppoDaCalcolare.SelectedValue, data_pick, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), True)

        '    funzioni_comuni.fonteInStopSell(dropStazionePickUp.SelectedValue, dropTipoCliente.SelectedValue, data_pick, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
        'End If
        '-----------------------------------------------------------------------------------------------------------------------------------

        'SCONTO APPLICATO IN FASE DI SALVATAGGIO (SCONTO NON MODIFICABILE)
        If txtSconto.Text = "" Then
            txtSconto.Text = "0"
        End If

        'SCONTO APPLICATO SU RACK (SCONTO NON MODIFICABILE)
        If txtScontoRack.Text = "" Then
            txtScontoRack.Text = "0"
        End If

        'SE C'E' UNA RESTRIZIONE DI GIORNI DI NOLEGGIO ED IL NUMERO DI GIORNI EFFETTUATI E' MINORE DI QUELLI MINIMI RICHIESTI, FORZO IL VALORE
        'CON QUELLO MINIMO (QUESTO SOLO SE STO EFFETTUANDO IL QUICK CHECK IN O L'USCITA DEL VEICOLO (A NOLO IN CORSO INVECE NON POSSO RICALCOLARE SE IL VINCOLO 
        'NON E' RISPETTATO
        If (ricalcolo_per_stato = "3" Or ricalcolo_per_stato = "4") And numero_giorni < min_giorni_nolo Then
            txtNumeroGiorni.Text = min_giorni_nolo
            Libreria.genUserMsgBox(Me, "La tariffa prevede minimo " & min_giorni_nolo & " giorni/o di noleggio.")
            'ElseIf ricalcolo_per_stato = "3" And numero_giorni < min_giorni_nolo Then
            '    Libreria.genUserMsgBox(Me, "La tariffa prevede minimo " & min_giorni_nolo & " giorni/o di noleggio.")
        Else
            txtNumeroGiorni.Text = numero_giorni
        End If

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

        If tariffa_broker.Text = "1" AndAlso (ricalcolo_per_stato = "3" Or ricalcolo_per_stato = "1") AndAlso CInt(numero_giorni) < CInt(txtNumeroGiorniTO.Text) Then
            'T.O.: SU QUICK CHECK O SU USCITA VEICOLO SE I GIORNI DI NOLO SONO INFERIORI AI GIORNI VOUCHER ALLORA CONRTOLLO SE I GIORNI EXTRA 
            'SONO RIMBORSABILI O MENO IN QUESTA FASE QUESTA SCELTA E' OBBLIGATORIA - SUCCESSIVAMENTE CON LA MODIFICA ADMIN SI PUO' 
            'CAMBIARE MANUALEMNTE IL COMPORTAMENTO DELLA TARIFFA.
            Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(giorni_non_usufruiti_rimborsabili,'0') FROM clienti_tipologia WHERE id='" & CInt(dropTipoCliente.SelectedValue) & "'", Dbc)

            Dim giorni_non_usufruiti_rimborsabili As Boolean = Cmd.ExecuteScalar

            If giorni_non_usufruiti_rimborsabili Then
                dropVariazioneACaricoDi.SelectedValue = "1"
                dropVariazioneACaricoDi.Enabled = False
            Else
                dropVariazioneACaricoDi.SelectedValue = "0"
                dropVariazioneACaricoDi.Enabled = False
            End If
        ElseIf tariffa_broker.Text = "1" AndAlso ricalcolo_per_stato = "2" AndAlso CInt(numero_giorni) < CInt(txtNumeroGiorniTO.Text) Then
            'NOLO IN CORSO: SE I GIORNI DI NOLO STANNO DIMINUENDO COL NUOVO CALCOLO E L'OPERATORE HA SELEZIONATO A CARICO DEL BROKER VUOL DIRE
            'CHE STA SALVANDO UN NUOVO VOUCHER CHE HA RICEVUTO. SE INVECE HA SELEZIONATO A CARICO DEL CLIENTE CONTROLLO SE I GIORNI NON USUFRUITI
            'SONO RIMBORSABILI. SE LO SONO ALLORA IMPOSTO AUTOMATICAMENTE LA VARIAZIONE A CARICO DEL BROKER (QUELLO CHE STA FACENDO 
            'L'OPERATORE NON HA SENSO).

            If dropVariazioneACaricoDi.SelectedValue = "0" Then
                Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(giorni_non_usufruiti_rimborsabili,'0') FROM clienti_tipologia WHERE id='" & CInt(dropTipoCliente.SelectedValue) & "'", Dbc)

                Dim giorni_non_usufruiti_rimborsabili As Boolean = Cmd.ExecuteScalar

                If giorni_non_usufruiti_rimborsabili Then
                    dropVariazioneACaricoDi.SelectedValue = "1"
                    dropVariazioneACaricoDi.Enabled = False
                End If
            End If
        End If

        If tariffa_broker.Text = "1" Then
            If dropVariazioneACaricoDi.SelectedValue = "1" Then
                'BROKER
                txtNumeroGiorniTO.Text = txtNumeroGiorni.Text
                'txtNumeroGiorniTO.Text = numero_giorni
            ElseIf dropVariazioneACaricoDi.SelectedValue = "0" Then
                'CLIENTE
                txtNumeroGiorniTO.Text = lblGiorniToOld.Text
            End If
        End If

        Dim tariffaVendibile As Boolean

        If tariffa_broker.Text = "1" Then
            'VENDIBILITA' TARIFFA - CASO BROKER SE L'ESTENSIONE E' A CARICO DEL BROKER SI UTLIZZA LA TARIFFA ORIGINARIA - SE L'ESTENSIONE E'
            'A CARICO DEL CLIENTE SI UTLIZZA LA RACK PER I GIORNI RESTANTI
            If dropVariazioneACaricoDi.SelectedValue = "1" Then
                tariffaVendibile = True
            Else
                tariffaVendibile = False
            End If
        ElseIf txtGiorniPrepagati.Visible Then
            'PER LE PREPAGATE SI DEVE SEMPRE USARE LA RACK
            tariffaVendibile = False
        Else
            'VENDIBILITA' TARIFFA - CASO TARIFFA NON BROKER - CALCOLO LA VENDIBILITA'
            If statoContratto.Text <> "4" And statoContratto.Text <> "8" Then
                If rack_utilizzata.Text = "0" Then
                    tariffaVendibile = tariffa_vendibile(id_tariffe_righe)
                Else
                    'SE NEL CALCOLO PRECEDENTE ERA STATA UTILIZZATA LA RACK ALLORA NECESSARIAMENTE DEVO UTILIZZARLA
                    tariffaVendibile = False
                End If
            Else
                'A NOLO CONCLUSO (MODIFICA ADMIN - TARIFFA NON BROKER) SEMPLICEMENTE SE HO UTLIZZATO LA RACK PER IL CALCOLO 
                'LA DEVO UTILIZZARE, ALTRIMENTI NO
                If rack_utilizzata.Text = "0" Then
                    tariffaVendibile = True
                Else
                    tariffaVendibile = False
                End If
            End If
        End If

        'ELIMINO LE RIGHE DI CALCOLO PER IL NUMERO DI CALCOLO ATTUALE ---------------------------------------------------------------------
        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM contratti_warning WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM contratti_costi WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        '----------------------------------------------------------------------------------------------------------------------------------
        'If numCrv.Text <> "0" Then
        'NEL CASO IN SI E' EFFETTUATO UN CRV E' NECESSARIO AGGIUNGERE GLI EVENTUALI COSTI DEL CARBURANTE DELLE AUTO PRECEDENTI DAL 
        'PRECEDENTE CALCOLO - MODIFICA: DEVE ESSERE FATTO SEMPRE!!! NON SOLO IN CASO DI CRV
        funzioni.aggiungi_refuel_calcolo_precedente(idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue))
        'End If

        Dim giorni_prepagati As Integer = 0
        Dim prepagata As Boolean = False

        If txtGiorniPrepagati.Visible Then
            giorni_prepagati = txtGiorniPrepagati.Text
            prepagata = True
        End If

        'SE LA TARIFFA E' ANCORA VENDIBILE ESEGUO IL CALCOLO CON LA STESSA TARIFFA ANCHE PER EVENTUALI GIORNI EXTRA
        ',PASSANDO 0 PER IL VALORE giorni_noleggio_extra_rack, E PER CAMBIO DI GRUPPO
        'SE NON E' PIU' VENDIBILE IN CASO DI RICHIESTA DI ESTENSIONE DI GIORNI DI NOLEGGIO DEVO UTILIZZARE IL VALORE TARIFFA RACK PER I GIORNI
        'EXTRA (PASSANDO I GIORNI DI ESTENSIONE PER IL PARAMETRO giorni_noleggio_extra_rack). NON UTILIZZO LA TARIFFA RACK SE LA 
        'TARIFFA NON E' VENDIBILE UNICAMENTE QUANDO LA VARIAZIONE DI GIORNI AVVIENE CAMBIANDO UNICAMENTE L'ORARIO ALL'INTERNO DELLE
        'GIORNATE DI PICK UP E DROP OFF DA PRENOTAZIONE E NIENT'ALTRO RISPETTO LA PRENOTAZIONE (SOLO NEL CASO DI TARIFFA NON BROKER - IN QUESTO
        'CASO LA TARIFFA RACK SI APPLICA COMUNQUE QUANDO LA VARIAZIONE E' A CARICO DEL CLIENTE)

        If (Not txtGiorniPrepagati.Visible) AndAlso ((tariffaVendibile) OrElse (tariffa_broker.Text = "1" AndAlso CInt(txtNumeroGiorniTO.Text) > CInt(txtNumeroGiorni.Text)) OrElse (DateDiff(DateInterval.Day, CDate(funzioni_comuni.getDataDb_senza_orario(txtDaData.Text)), CDate(funzioni_comuni.getDataDb_senza_orario(txtDADataOld.Text))) = 0 And DateDiff(DateInterval.Day, CDate(funzioni_comuni.getDataDb_senza_orario(txtAData.Text)), CDate(funzioni_comuni.getDataDb_senza_orario(txtADataOld.Text))) = 0 And gruppoDaCalcolare.SelectedValue = gruppo_da_calcolare_originale.Text And dropStazioneDropOff.SelectedValue = id_stazione_drop_off_prenotazione.Text And tariffa_broker.Text = "0")) Then
            funzioni.calcolaTariffa_x_gruppo(CInt(dropStazionePickUp.SelectedValue), txtDaData.Text, CInt(ore1.Text), CInt(minuti1.Text), CInt(dropStazioneDropOff.SelectedValue), id_tariffe_righe, CInt(gruppoDaCalcolare.SelectedValue), "", CInt(txtNumeroGiorni.Text), giorni_prepagati, prepagata, 0, CDbl(txtSconto.Text), dropTipoSconto.SelectedValue, 0, 0, txtEtaPrimo.Text, txtEtaSecondo.Text, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), idDitta.Text, dropFonteCommissionabile.SelectedValue, txtPercentualeCommissionabile.Text, dropTipoCommissione.SelectedValue, False, lblGGcommissioniOriginali.Text)
        Else
            Dim giorni_extra_rack As Integer
            If txtGiorniPrepagati.Visible Then
                giorni_extra_rack = (CInt(txtNumeroGiorni.Text) - CInt(txtGiorniPrepagati.Text))
            Else
                giorni_extra_rack = CInt(txtNumeroGiorni.Text) - CInt(txtNumeroGiorniIniziali.Text)
            End If
            If giorni_extra_rack < 0 Then
                giorni_extra_rack = 0
            End If
            funzioni.calcolaTariffa_x_gruppo(CInt(dropStazionePickUp.SelectedValue), txtDaData.Text, CInt(ore1.Text), CInt(minuti1.Text), CInt(dropStazioneDropOff.SelectedValue), id_tariffe_righe, CInt(gruppoDaCalcolare.SelectedValue), gruppo_da_calcolare_originale.Text, CInt(txtNumeroGiorni.Text), giorni_prepagati, prepagata, giorni_extra_rack, CDbl(txtSconto.Text), dropTipoSconto.SelectedValue, 0, CDbl(txtScontoRack.Text), txtEtaPrimo.Text, txtEtaSecondo.Text, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), idDitta.Text, dropFonteCommissionabile.SelectedValue, txtPercentualeCommissionabile.Text, dropTipoCommissione.SelectedValue, False, lblGGcommissioniOriginali.Text)
        End If

        If statoContratto.Text = "4" Or statoContratto.Text = "8" Then
            'SE SIAMO NEL CASO DI MODIFICA ADMIN SETTO A IL CAMPO secondo_ordine_stampa PER FAR VISUALIZZARE IL TOTALE COME ULTIMO ELEMENTO
            Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET secondo_ordine_stampa='1' WHERE id_documento='" & idContratto.Text & "' And num_calcolo='" & numCalcolo.Text & "' AND nome_costo='" & Costanti.testo_elemento_totale & "'", Dbc)
            Cmd.ExecuteNonQuery()

            'SEMPRE NEL CASO DI MODIFICA ADMIN SE LA TARIFFA E' A CHILOMETRAGGIO ILLIMITATO RICALCOLO IL COSTO
            If tariffa_km_limitati() Then
                'SE LA TARIFFA E' A KM LIMITATI AGGIUNGO EVENTUALMENTE IL COSTO DEI KM EXTRA
                'CALCOLO DEI KM TOTALI DI NOLO
                Dim km_percorsi As Integer
                If numCrv.Text = "0" Then
                    'NESSUN CRV
                    km_percorsi = CInt(txtKmRientro.Text) - CInt(txtKm.Text)
                Else
                    km_percorsi = 0
                    Dim km_uscita As Label
                    Dim km_rientro As Label
                    For i = 0 To listCrv.Items.Count - 2
                        km_uscita = listCrv.Items(i).FindControl("km_uscita")
                        km_rientro = listCrv.Items(i).FindControl("km_rientro")

                        km_percorsi = km_percorsi + CInt(km_rientro.Text) - CInt(km_uscita.Text)
                    Next
                    km_percorsi = km_percorsi + CInt(txtKmRientro.Text) - CInt(txtKm.Text)
                End If

                funzioni.addebita_km_extra(idContratto.Text, numCalcolo.Text, km_percorsi, txtNumeroGiorni.Text, sconto, dropStazionePickUp.SelectedValue)
            End If
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        listContrattiCosti.DataBind()

        'E' NECESSARIO AGGIUNGERE GLI ACCESSORI PRECEDENTI ------------------------------------------------------------------------------
        aggiungi_accessori_precedente_calcolo()
        '--------------------------------------------------------------------------------------------------------------------------------

        'SE LA PRENOTAZIONE E' PREPAGATA E' NECESSARIO RIPORTARE I COSTI PREPAGATI DAL CALCOLO PRECEDENTE
        If prepagata Then
            'funzioni_comuni.riporta_costi_prepagati("", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), "", "")
        End If

        listContrattiCosti.DataBind()

        aggiorna_informazioni_dopo_modifica_costi()
    End Sub


    Protected Sub controlla_warning()
        'WARNING SU VINCOLI DI APERTURA DELLE STAZIONI-------------------------------------------------------------------------------------
        Dim stazione_aperta_pick_up As String = funzioni_comuni.stazione_aperta_pick_up(CInt(dropStazionePickUp.SelectedValue), txtDaData.Text, ore1.Text, minuti1.Text, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
        Dim stazione_aperta_drop_off As String = funzioni_comuni.stazione_aperta_drop_off(CInt(dropStazioneDropOff.SelectedValue), txtAData.Text, ore2.Text, minuti2.Text, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))

        Dim pick_up_on_request As String = funzioni_comuni.stazione_pick_up_on_request(CInt(dropStazionePickUp.SelectedValue), "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
        Dim drop_off_on_request As String = funzioni_comuni.stazione_drop_off_on_request(CInt(dropStazioneDropOff.SelectedValue), "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
        '-----------------------------------------------------------------------------------------------------------------------------------

        'If provenienza.Text = "preventivi.aspx" Then
        Dim data_pick As String = funzioni_comuni.getDataDb_senza_orario(txtDaData.Text)
        'NEL CASO DI PROVENIENZA DA PREVENTIVI DEVO CONTROLLARE ANCHE ALTRI WARNING - NO DEVO CONTROLLARE SEMPRE!!!
        funzioni_comuni.gruppo_vendibile_pick_up(CInt(dropStazionePickUp.SelectedValue), CInt(gruppoDaCalcolare.SelectedValue), "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), True)
        funzioni_comuni.gruppo_vendibile_drop_off(CInt(dropStazioneDropOff.SelectedValue), CInt(gruppoDaCalcolare.SelectedValue), "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), True)

        If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
            funzioni_comuni.gruppo_vendibile_val(CInt(dropStazioneDropOff.SelectedValue), CInt(gruppoDaCalcolare.SelectedValue), "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), True)

            funzioni_comuni.stazione_permette_VAL_verso_altre_stazioni(CInt(dropStazionePickUp.SelectedValue), "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
            funzioni_comuni.stazione_accetta_VAL_da_altre_stazioni(CInt(dropStazioneDropOff.SelectedValue), "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
        End If

        funzioni_comuni.stazioneInStopSell(CInt(dropStazionePickUp.SelectedValue), data_pick, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))

        funzioni_comuni.gruppoInStopSell(CInt(dropStazionePickUp.SelectedValue), CInt(gruppoDaCalcolare.SelectedValue), data_pick, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), True)

        If dropTipoCliente.SelectedValue <> "0" Then
            funzioni_comuni.gruppoInStopSellPerFonte(CInt(dropStazionePickUp.SelectedValue), CInt(dropTipoCliente.SelectedValue), CInt(gruppoDaCalcolare.SelectedValue), data_pick, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), True)

            funzioni_comuni.fonteInStopSell(CInt(dropStazionePickUp.SelectedValue), CInt(dropTipoCliente.SelectedValue), data_pick, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
        End If
        'End If
    End Sub

    Protected Function esegui_ricalcolo() As Boolean
        'NEL CASO IN CUI SIAMO NEL CASO DI MODIFICA DA PRENOTAZIONE, NON E' POSSIBILE EFFETTUARE LA MODIFICA SE SIAMO NEL CASO DI DOWNSELL
        '(OVVIAMENTE CONTROLLO SOLAMENTE SE E' STATA RICHIESTA LA MODIFICA DEL GRUPPO)
        Dim id_tariffe_righe As String

        If dropTariffeGeneriche.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
        End If

        Dim tariffaVendibile As Boolean
        Dim gruppoModificabileUpsell_oppure_gruppo_non_modificato As Boolean = True

        'CALCOLO IL NUMERO DI GIORNI DI NOLEGGIO
        Dim numero_giorni As Integer = funzioni_comuni.getGiorniDiNoleggio(txtDaData.Text, txtAData.Text, ore1.Text, minuti1.Text, ore2.Text, minuti2.Text, id_tariffe_righe)

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

        'CONTROLLO SE VI SONO VINCOLI DI MINIMO/MASSIMO GIORNI DI NOLEGGIO DA RISPETTARE
        Dim min_giorni_nolo As String = funzioni_comuni.getMinGiorniNolo(id_tariffe_righe)
        Dim max_giorni_nolo As String = funzioni_comuni.getMaxGiorniNolo(id_tariffe_righe)

        If (min_giorni_nolo = "-1" And max_giorni_nolo = "-1") Or (min_giorni_nolo = "-1" And CInt(numero_giorni) <= CInt(max_giorni_nolo) And max_giorni_nolo <> "-1") Or (CInt(min_giorni_nolo) <= CInt(numero_giorni) And max_giorni_nolo = "-1" And min_giorni_nolo <> "-1") Or (CInt(min_giorni_nolo) <= CInt(numero_giorni) And CInt(max_giorni_nolo) >= CInt(numero_giorni) And min_giorni_nolo <> "-1" And max_giorni_nolo <> "-1") Then
            If tariffa_broker.Text = "1" AndAlso CInt(numero_giorni) < CInt(txtNumeroGiorniTO.Text) Then
                'SE I GIORNI DI NOLO STANNO DIMINUENDO COL NUOVO CALCOLO E L'OPERATORE HA SELEZIONATO A CARICO DEL BROKER VUOL DIRE
                'CHE STA SALVANDO UN NUOVO VOUCHER CHE HA RICEVUTO. SE INVECE HA SELEZIONATO A CARICO DEL CLIENTE CONTROLLO SE I GIORNI NON USUFRUITI
                'SONO RIMBORSABILI. SE LO SONO ALLORA IMPOSTO AUTOMATICAMENTE LA VARIAZIONE A CARICO DEL BROKER (QUELLO CHE STA FACENDO 
                'L'OPERATORE NON HA SENSO).

                If dropVariazioneACaricoDi.SelectedValue = "0" Then
                    Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(giorni_non_usufruiti_rimborsabili,'0') FROM clienti_tipologia WHERE id='" & CInt(dropTipoCliente.SelectedValue) & "'", Dbc)

                    Dim giorni_non_usufruiti_rimborsabili As Boolean = Cmd.ExecuteScalar

                    If giorni_non_usufruiti_rimborsabili Then
                        dropVariazioneACaricoDi.SelectedValue = "1"
                        dropVariazioneACaricoDi.Enabled = False
                    End If
                End If
            End If


            If provenienza.Text = "prenotazioni.aspx" Or statoContratto.Text = "2" Then
                If tariffa_broker.Text = "1" Then
                    If dropVariazioneACaricoDi.SelectedValue = "1" Then
                        tariffaVendibile = True
                    Else
                        tariffaVendibile = False
                    End If
                ElseIf txtGiorniPrepagati.Visible Then
                    'PER LE PREPAGATE SI DEVE SEMPRE USARE LA RACK
                    tariffaVendibile = False
                Else
                    tariffaVendibile = tariffa_vendibile(id_tariffe_righe)
                End If


                If gruppoDaCalcolare.SelectedValue <> gruppo_da_calcolare_originale.Text Then
                    gruppoModificabileUpsell_oppure_gruppo_non_modificato = funzioni_comuni.check_upsell(id_tariffe_righe, CInt(gruppoDaCalcolare.SelectedValue), gruppo_da_calcolare_originale.Text, CInt(numero_giorni), tariffaVendibile, idContratto.Text)
                End If
            End If

            If gruppoModificabileUpsell_oppure_gruppo_non_modificato Then
                numCalcolo.Text = CInt(numCalcolo.Text) + 1
                'ELIMINO LE PRECEDENTI RIGHE DI CALCOLO (PER IL NUM. CALCOLO ATTUALE)---------------------------------------------------------------
                Cmd = New Data.SqlClient.SqlCommand("DELETE FROM contratti_costi WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
                Cmd.ExecuteNonQuery()

                Cmd = New Data.SqlClient.SqlCommand("DELETE FROM contratti_warning WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
                Cmd.ExecuteNonQuery()
                '-----------------------------------------------------------------------------------------------------------------------------------

                controlla_warning()

                'SE E' STATO SELEZIONATO UN SCONTO CONTROLLO SE SUPERA LO SCONTO MASSIMO APPLICABILE. SE SI SOSTITUISCO IL VALORE COL VALORE MASSIMO
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

                'SE E' STATO SELEZIONATO UN SCONTO SU RACK CONTROLLO SE SUPERA LO SCONTO MASSIMO APPLICABILE. SE SI SOSTITUISCO IL VALORE COL VALORE MASSIMO
                If txtScontoRack.Text <> "" Then
                    Dim max_sconto As Double = funzioni_comuni.checkMaxScontoRack(id_tariffe_righe, txtScontoRack.Text, "", "", "", "", "", "", False)

                    If max_sconto <> -1 Then
                        txtScontoRack.Text = max_sconto
                        lblMxScontoRack.Visible = True
                    Else
                        lblMxScontoRack.Visible = False
                    End If
                Else
                    txtScontoRack.Text = "0"
                    lblMxScontoRack.Visible = False
                End If

                txtNumeroGiorni.Text = numero_giorni


                If tariffa_broker.Text = "1" Then
                    If dropVariazioneACaricoDi.SelectedValue = "1" Then
                        'A CARICO DEL BROKER
                        txtNumeroGiorniTO.Text = numero_giorni
                    Else
                        txtNumeroGiorniTO.Text = lblGiorniToOld.Text
                    End If
                End If

                'CALCOLO DEI COSTI
                'Libreria.genUserMsgBox(Me, tariffa_vendibile(id_tariffe_righe))

                Dim giorni_prepagati As Integer = 0
                Dim prepagata As Boolean = False

                If txtGiorniPrepagati.Visible Then
                    giorni_prepagati = txtGiorniPrepagati.Text
                    prepagata = True
                End If

                If provenienza.Text = "prenotazioni.aspx" Then
                    'SE LA TARIFFA E' ANCORA VENDIBILE ESEGUO IL CALCOLO CON LA STESSA TARIFFA ANCHE PER EVENTUALI GIORNI EXTRA
                    ',PASSANDO 0 PER IL VALORE giorni_noleggio_extra_rack, E PER CAMBIO DI GRUPPO
                    'SE NON E' PIU' VENDIBILE IN CASO DI RICHIESTA DI ESTENSIONE DI GIORNI DI NOLEGGIO DEVO UTILIZZARE IL VALORE TARIFFA RACK PER I GIORNI
                    'EXTRA (PASSANDO I GIORNI DI ESTENSIONE PER IL PARAMETRO giorni_noleggio_extra_rack). NON UTILIZZO LA TARIFFA RACK SE LA 
                    'TARIFFA NON E' VENDIBILE UNICAMENTE QUANDO LA VARIAZIONE DI GIORNI AVVIENE CAMBIANDO UNICAMENTE L'ORARIO ALL'INTERNO DELLE
                    'GIORNATE DI PICK UP E DROP OFF DA PRENOTAZIONE E NIENT'ALTRO RISPETTO LA PRENOTAZIONE
                    If (Not txtGiorniPrepagati.Visible) AndAlso ((tariffaVendibile) OrElse (tariffa_broker.Text = "1" AndAlso CInt(txtNumeroGiorniTO.Text) > CInt(txtNumeroGiorni.Text)) OrElse (DateDiff(DateInterval.Day, CDate(funzioni_comuni.getDataDb_senza_orario(txtDaData.Text)), CDate(funzioni_comuni.getDataDb_senza_orario(txtDADataOld.Text))) = 0 And DateDiff(DateInterval.Day, CDate(funzioni_comuni.getDataDb_senza_orario(txtAData.Text)), CDate(funzioni_comuni.getDataDb_senza_orario(txtADataOld.Text))) = 0 And gruppoDaCalcolare.Text = gruppo_da_calcolare_originale.Text And dropStazioneDropOff.SelectedValue = id_stazione_drop_off_prenotazione.Text And tariffa_broker.Text = "0")) Then
                        funzioni.calcolaTariffa_x_gruppo(CInt(dropStazionePickUp.SelectedValue), txtDaData.Text, CInt(ore1.Text), CInt(minuti1.Text), CInt(dropStazioneDropOff.SelectedValue), id_tariffe_righe, CInt(gruppoDaCalcolare.SelectedValue), "", numero_giorni, giorni_prepagati, prepagata, 0, CDbl(txtSconto.Text), dropTipoSconto.SelectedValue, 0, 0, txtEtaPrimo.Text, txtEtaSecondo.Text, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), idDitta.Text, dropFonteCommissionabile.SelectedValue, txtPercentualeCommissionabile.Text, dropTipoCommissione.SelectedValue, False, lblGGcommissioniOriginali.Text)
                    Else
                        Dim giorni_Extra_rack As Integer
                        If txtGiorniPrepagati.Visible Then
                            giorni_Extra_rack = (CInt(txtNumeroGiorni.Text) - CInt(txtGiorniPrepagati.Text))
                        Else
                            giorni_Extra_rack = CInt(txtNumeroGiorni.Text) - CInt(txtNumeroGiorniIniziali.Text)
                        End If
                        If giorni_Extra_rack < 0 Then
                            giorni_Extra_rack = 0
                        End If

                        funzioni.calcolaTariffa_x_gruppo(CInt(dropStazionePickUp.SelectedValue), txtDaData.Text, CInt(ore1.Text), CInt(minuti1.Text), CInt(dropStazioneDropOff.SelectedValue), id_tariffe_righe, CInt(gruppoDaCalcolare.SelectedValue), gruppo_da_calcolare_originale.Text, numero_giorni, giorni_prepagati, prepagata, giorni_Extra_rack, CDbl(txtSconto.Text), dropTipoSconto.SelectedValue, 0, CDbl(txtScontoRack.Text), txtEtaPrimo.Text, txtEtaSecondo.Text, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), idDitta.Text, dropFonteCommissionabile.SelectedValue, txtPercentualeCommissionabile.Text, dropTipoCommissione.SelectedValue, False, lblGGcommissioniOriginali.Text)
                    End If
                ElseIf provenienza.Text = "preventivi.aspx" Then
                    'NEL CASO DI PROVENIENZA DA PREVENTIVO (PER CUI LA TARIFFA E' CERTAMENTE VENDIBILE) 
                    funzioni.calcolaTariffa_x_gruppo(CInt(dropStazionePickUp.SelectedValue), txtDaData.Text, CInt(ore1.Text), CInt(minuti1.Text), CInt(dropStazioneDropOff.SelectedValue), id_tariffe_righe, CInt(gruppoDaCalcolare.SelectedValue), "", numero_giorni, 0, False, 0, CDbl(txtSconto.Text), dropTipoSconto.SelectedValue, 0, CDbl(txtScontoRack.Text), txtEtaPrimo.Text, txtEtaSecondo.Text, "", "", "", idContratto.Text, numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), idDitta.Text, dropFonteCommissionabile.SelectedValue, txtPercentualeCommissionabile.Text, dropTipoCommissione.SelectedValue, True, lblGGcommissioniOriginali.Text)
                End If

                listContrattiCosti.DataBind()

                'E' NECESSARIO AGGIUNGERE GLI ACCESSORI PRECEDENTI ------------------------------------------------------------------------------
                aggiungi_accessori_precedente_calcolo()
                '--------------------------------------------------------------------------------------------------------------------------------

                'SE LA PRENOTAZIONE E' PREPAGATA E' NECESSARIO RIPORTARE I COSTI PREPAGATI DAL CALCOLO PRECEDENTE
                If prepagata Then
                    'funzioni_comuni.riporta_costi_prepagati("", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), "", "")
                End If

                'ELIMINAZIONE DI PRECEDENTI RIGHE DI CALCOLO: SE IL CONTRATTO E' IN COMPILAZIONE NON E' NECESSARIO MANTENERE LE PRECEDENTI RIGHE DI
                'CALCOLO
                If statoContratto.Text = "0" Then
                    Cmd = New Data.SqlClient.SqlCommand("DELETE FROM contratti_costi WHERE id_documento='" & idContratto.Text & "' AND num_calcolo<>'" & numCalcolo.Text & "'", Dbc)
                    Cmd.ExecuteNonQuery()

                    Cmd = New Data.SqlClient.SqlCommand("DELETE FROM contratti_warning WHERE id_documento='" & idContratto.Text & "' AND num_calcolo<>'" & numCalcolo.Text & "'", Dbc)
                    Cmd.ExecuteNonQuery()
                End If
                '--------------------------------------------------------------------------------------------------------------------------------

                listContrattiCosti.DataBind()

                aggiorna_informazioni_dopo_modifica_costi()


                esegui_ricalcolo = True
            Else
                esegui_ricalcolo = False
                Libreria.genUserMsgBox(Me, "Impossibile modificare la prenotazione: downsell non permesso.")
            End If
        Else
            Dim msg As String = "Attenzione: i giorni di noleggio sono " & numero_giorni & "; la tariffa scelta prevede"
            If min_giorni_nolo <> "-1" Then
                msg = msg & " minimo " & min_giorni_nolo & " giorni/o di nolo - "
            End If
            If max_giorni_nolo <> "-1" Then
                msg = msg & " massimo " & max_giorni_nolo & " giorni/o di nolo "
            End If

            Libreria.genUserMsgBox(Page, msg)

            btnSalvaModifiche.Enabled = False
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub aggiungi_accessori_precedente_calcolo()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

        Dim id_elemento As Label
        Dim precedentemente_selezionato As String
        Dim omaggiabile As Boolean
        Dim precedentemente_omaggiato As Boolean
        Dim data_aggiunta_nolo_in_corso As String
        Dim id_unita_misura As Label
        Dim giorni_restanti As String
        Dim data_agg As String
        Dim tipologia As Label
        Dim tipologia_franchigia As Label

        Dim id_tariffe_righe As String
        If dropTariffeGeneriche.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
        End If

        Dim sconto As Double
        If dropTipoSconto.SelectedValue = "0" Then
            sconto = CDbl(txtSconto.Text)
        ElseIf dropTipoSconto.SelectedValue = "1" Then
            sconto = 0
        End If

        Dim id_pieno_carburante As String = get_id_pieno_carburante()

        For i = 0 To listContrattiCosti.Items.Count - 1
            'SE L'ELEMENTO E' UNO DEGLI ELEMENTI A SCELTA CONTROLLO SE PER IL NUMERO DI CALCOLO PRECENDENTE ERA STATO SELEZIONATO
            'SE SI LO SELEZIONO ED AGGIUNGO IL COSTO AL TOTALE - NON DEVE MAI ESSERE AGGIUNTO (CASO NOLO IN CORSO) IL COSTO CHILOMETRICO CHE VIENE SEMPRE RICALCOLATO
            Dim chkScegli As CheckBox = listContrattiCosti.Items(i).FindControl("chkScegli")
            tipologia_franchigia = listContrattiCosti.Items(i).FindControl("tipologia_franchigia")
            tipologia = listContrattiCosti.Items(i).FindControl("tipologia")

            If (listContrattiCosti.Items(i).FindControl("chkScegli").Visible Or (Not listContrattiCosti.Items(i).FindControl("chkScegli").Visible And listContrattiCosti.Items(i).FindControl("chkOmaggio").Visible)) And tipologia.Text <> "KM_EXTRA" Then
                id_elemento = listContrattiCosti.Items(i).FindControl("id_elemento")
                Cmd = New Data.SqlClient.SqlCommand("SELECT selezionato FROM contratti_costi WITH(NOLOCK) WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "' AND id_elemento='" & id_elemento.Text & "'", Dbc)
                precedentemente_selezionato = Cmd.ExecuteScalar

                If precedentemente_selezionato = "True" Then
                    'SE L'ELEMENTO E' IL PIENO BENZINA E' NECESSARIO VALORIZZARNE IL COSTO--------------------------------------------------
                    If id_elemento.Text = id_pieno_carburante Then
                        If statoContratto.Text = "0" Or statoContratto.Text = "1" Then
                            'SE IL CONTRATTO NON E' ANCORA NELLO STATO ATTIVO ALLORA AGGIORNO IL COSTO CON IL COSTO DELLA BENZINA AGGIORNATO
                            '(SOLAMENTE SE E' STATO SELEZIONATO UN MEZZO)
                            If id_auto_selezionata.Text <> "" Then
                                funzioni_comuni.aggiorna_costo_pieno_carburante(idContratto.Text, numCalcolo.Text, CInt(dropStazionePickUp.SelectedValue), id_alimentazione.Text, lblSerbatoioMax.Text, id_pieno_carburante, sconto)
                            End If
                        Else
                            'SE IL CONTRATTO E' IN CORSO, INVECE, IL COSTO DEVE ESSERE BLOCCATO E QUINDI UGUALE AL CALCOLO PREEDENTE
                            funzioni_comuni.aggiorna_costo_pieno_carburante_da_calcolo_precedente(idContratto.Text, numCalcolo.Text, id_pieno_carburante)
                        End If
                    End If
                    '-----------------------------------------------------------------------------------------------------------------------
                    'CONTROLLO SE ERA STATO OMAGGIATO E SE L'ACCESSORIO E' ANCORA OMAGGIABILE
                    Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(omaggiato,'False') As omaggiato FROM contratti_costi WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "' AND id_elemento='" & id_elemento.Text & "'", Dbc)
                    precedentemente_omaggiato = Cmd.ExecuteScalar

                    If complimentary.Text = "1" Then
                        omaggiabile = True
                    Else
                        Cmd = New Data.SqlClient.SqlCommand("SELECT omaggiabile FROM contratti_costi WITH(NOLOCK) WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) & "' AND id_elemento='" & id_elemento.Text & "'", Dbc)
                        omaggiabile = Cmd.ExecuteScalar
                    End If

                    If precedentemente_omaggiato And (omaggiabile Or statoContratto.Text = "2") Then
                        'SE L'ACCESSORIO ERA STATO OMAGGIATO ED E' ANCORA OMAGGIABILE VIENE NUOVAMENTE OMAGGIATO (SIA 
                        'PER ACCESSORI A SCELTA CHE PER ACCESSORI OBBLIGATORI) - A CONTRATTO IN CORSO, NEL CASO DI RICALCOLO, IGNORO IL FATTO
                        'CHE L'ACCESSORIO NON E' PIU' OMAGGIABILE SE ERA STATO OMAGGIATO

                        'SALVARE omaggiabile='TRUE' A CONTRATTO IN CORSO SE ERA OMAGGIAIBLE PER IL PRECEDENTE CALCOLO E ADESSO NON LO E' PIU'
                        If statoContratto.Text = "2" And Not omaggiabile Then
                            Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET omaggiabile='1' WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) & "' AND id_elemento='" & id_elemento.Text & "'", Dbc)
                            Cmd.ExecuteNonQuery()
                        End If

                        funzioni.omaggio_accessorio(True, False, False, "", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), id_elemento.Text, "", dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text)
                    Else
                        If (listContrattiCosti.Items(i).FindControl("chkScegli").Visible) Then
                            'AGGIUNGO IL COSTO SOLAMENTE SE SI TRATTA DI UN ACCESSORIO A SCELTA (SE E' UN ELEMENTO
                            'OBBLIGATORIO IL COSTO E' GIA' STATO CALCOLATO QUANDO E' STATA ANALIZZATA LA CONDIZIONE)
                            'NEL CASO DI CONTRATTO IN CORSO DEVO CONTROLLARE PRIMA DI AGGIUNGERE IL COSTO SE E' STATO AGGIUNTO A NOLO IN CORSO
                            'IN CASO POSITIVO (NEL CASO DI COSTO GIORNALIERO) VERRA' CALCOLATO IL NUMERO DI GIORNI RESTANTE - IN QUESTO CASO
                            If statoContratto.Text <> "2" Then
                                aggiungi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), id_elemento.Text, "", "", "", False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                            Else
                                'data_aggiunta_nolo_in_corso
                                id_unita_misura = listContrattiCosti.Items(i).FindControl("id_unita_misura")
                                If id_unita_misura.Text = Costanti.id_unita_misura_giorni Then
                                    'SE IL COSTO E' GIORNALIERO CALCOLO I GIORNI RESTANTI (DAL MOMENTO DELL'AGGIUNTA DEL COSTO FINO ALLA DATA DI DROP OFF)
                                    'CONTROLLO SE DAL CALCOLO PRECEDENTE L'ACCESSORIO ERA STATO AGGIUNTO NOLO IN CORSO
                                    Cmd = New Data.SqlClient.SqlCommand("SELECT data_aggiunta_nolo_in_corso FROM contratti_costi WITH(NOLOCK) WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "' AND id_elemento='" & id_elemento.Text & "'", Dbc)
                                    data_aggiunta_nolo_in_corso = Cmd.ExecuteScalar & ""

                                    If data_aggiunta_nolo_in_corso <> "" Then
                                        giorni_restanti = getGiorniDiNoleggio(Day(data_aggiunta_nolo_in_corso) & "/" & Month(data_aggiunta_nolo_in_corso) & "/" & Year(data_aggiunta_nolo_in_corso), txtAData.Text, Hour(data_aggiunta_nolo_in_corso), Minute(data_aggiunta_nolo_in_corso), ore2.Text, minuti2.Text, id_tariffe_righe)
                                    Else
                                        giorni_restanti = txtNumeroGiorni.Text
                                    End If

                                    If giorni_restanti = txtNumeroGiorni.Text Then
                                        aggiungi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), id_elemento.Text, "", "", "", False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                                    Else
                                        aggiungi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), id_elemento.Text, giorni_restanti, sconto, Day(data_aggiunta_nolo_in_corso) & "/" & Month(data_aggiunta_nolo_in_corso) & "/" & Year(data_aggiunta_nolo_in_corso) & " " & Hour(data_aggiunta_nolo_in_corso) & ":" & Minute(data_aggiunta_nolo_in_corso), False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                                    End If
                                Else
                                    aggiungi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), id_elemento.Text, "", "", "", False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                                End If
                            End If
                        End If
                    End If

                    'SE SI TRATTA DI SECONDO GUIDATORE AGGIUNGO, SE NECESSARIO, IL COSTO DELLO YOUNG DRIVER
                    If id_elemento.Text = Costanti.Id_Secondo_Guidatore Then
                        Dim id_young_driver As String = get_id_young_driver()
                        nuovo_accessorio(id_young_driver, CInt(gruppoDaCalcolare.SelectedValue), "YOUNG", txtEtaPrimo.Text, txtEtaSecondo.Text, "", "")
                        'SE ERA STATO OMAGGIATO PRECEDENTEMENTE (YOUNG DRIVER PER IL SECONDO GUIDATORE, num_elemento=2) LO OMAGGIO
                        'SE L'ELEMENTO E' STATO CALCOLATO
                        Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM contratti_costi WITH(NOLOCK) WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) & "' AND id_elemento='" & id_young_driver & "' AND num_elemento='2'", Dbc)
                        Dim test As String = Cmd.ExecuteScalar & ""
                        If test <> "" Then
                            'LO YOUNG DRIVER PER IL SECONDO GUIDATORE E' STATO AGGIUNTO CONTROLLO SE PER IL CALCOLO PRECEDENTE ERA STATO OMAGGIATO
                            Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(omaggiato,'False') As omaggiato FROM contratti_costi WITH(NOLOCK) WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "' AND id_elemento='" & id_young_driver & "' AND num_elemento='2'", Dbc)
                            precedentemente_omaggiato = Cmd.ExecuteScalar
                            If precedentemente_omaggiato Then
                                funzioni.omaggio_accessorio(True, False, False, "", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), id_young_driver, "2", dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text)
                            End If
                        End If
                    End If

                    'SE E' STATO AGGINTO IL GPS AGGIUNGO EVENTUALMENTE IL VAL
                    Dim is_gps As Label = listContrattiCosti.Items(i).FindControl("is_gps")
                    If is_gps.Text = "True" Then
                        If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
                            nuovo_accessorio("", CInt(gruppoDaCalcolare.SelectedValue), "VAL_GPS", txtEtaPrimo.Text, txtEtaSecondo.Text, "", "")
                        End If
                    End If
                End If
            End If
        Next

        'E' NECESSARIO ADESSO AGGIUNGERE GLI ACCESSORI EXTRA PRECEDENTEMENTE SELEZIONATI - OMAGGIO SE ERA OMAGGIATO PRECEDENTEMENTE
        Cmd = New Data.SqlClient.SqlCommand("SELECT id_elemento, ISNULL(condizioni_elementi.is_gps,'False') As is_gps, ISNULL(contratti_costi.omaggiato,'False') As omaggiato, condizioni_elementi.omaggiabile, data_aggiunta_nolo_in_corso FROM contratti_costi WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON contratti_Costi.id_elemento=condizioni_elementi.id WHERE contratti_costi.id_documento='" & idContratto.Text & "' AND contratti_costi.num_calcolo='" & CInt(numCalcolo.Text) - 1 & "' AND contratti_costi.obbligatorio='0' AND contratti_costi.selezionato='1' AND condizioni_elementi.valorizza='0'", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Do While Rs.Read()
            giorni_restanti = ""
            data_agg = ""

            If statoContratto.Text = "2" And (Rs("data_aggiunta_nolo_in_corso") & "") <> "" Then
                'SE SIAMO A CONTRATTO IN CORSO CONTROLLO SE NEL CALCOLO PRECEDENTE L'ACCESSORIO ERA STATO AGGIUNTO A NOLO IN CORSO - SE SI
                'ALLORA DEVO FARE IN MODO CHE nuovo_accessorio CALCOLI IL COSTO DELL'ACCESSORIO, SE E' A COSTO GIORNALIERO, SOLO PER I GIORNI
                'RESTANTI
                data_agg = Rs("data_aggiunta_nolo_in_corso")
                giorni_restanti = getGiorniDiNoleggio(Day(data_agg) & "/" & Month(data_agg) & "/" & Year(data_agg), txtAData.Text, Hour(data_agg), Minute(data_agg), ore2.Text, minuti2.Text, id_tariffe_righe)

                If giorni_restanti = txtNumeroGiorni.Text Then
                    'NON EFFETTUO IL CALCOLO PARTICOLARE NEL CASO IN CUI I GIORNI DA CALCOLARE COINCIDANO COL NUMERO DI GIORNI DI NOLO
                    giorni_restanti = ""
                    data_agg = ""
                End If
            End If

            nuovo_accessorio(Rs("id_elemento"), CInt(gruppoDaCalcolare.SelectedValue), "ELEMENTO", txtEtaPrimo.Text, txtEtaSecondo.Text, giorni_restanti, data_agg)
            precedentemente_omaggiato = Rs("omaggiato")
            omaggiabile = Rs("omaggiabile")
            If precedentemente_omaggiato Then
                If omaggiabile Or statoContratto.Text = "2" Then
                    'ANCHE SE L'ACCESSORIO NON E' PIU' OMAGGIABILE MA SIAMO A NOLO IN CORSO ED ERA STATO OMAGGATO PRECEDENTEMENTE (QUINDI 
                    'QUANDO LO ERA) OMAGGIO LO STESSO E SETTO L'ACCESSORIO COME OMAGGIABILE
                    'SALVARE omaggiabile='TRUE' A CONTRATTO IN CORSO SE ERA OMAGGIAIBLE PER IL PRECEDENTE CALCOLO E ADESSO NON LO E' PIU'

                    If statoContratto.Text = "2" And Not omaggiabile Then
                        set_accessorio_omaggiabile(Rs("id_elemento"))
                    End If

                    funzioni.omaggio_accessorio(True, False, False, "", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), Rs("id_elemento"), "", dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, "")
                End If
            End If

            'SE L'ELEMENTO EXTRA E' UN GPS
            If Rs("is_gps") Then
                If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
                    nuovo_accessorio("", CInt(gruppoDaCalcolare.SelectedValue), "VAL_GPS", txtEtaPrimo.Text, txtEtaSecondo.Text, "", "")
                End If
            End If
        Loop

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub set_accessorio_omaggiabile(ByVal id_elemento As String)
        'UTILIZZO LA FUNZIONE ESTERNA PER SETTARE QUESTO PARAMENTRO SOLO IN CASI PARTICOLARI (OVVERO QUANDO ESISTE GIA' UNA CONNESSIONE APERTA)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET omaggiabile='1' WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) & "' AND id_elemento='" & id_elemento & "'", Dbc)

        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub modifica_contratto_duplica_righe_calcolo()
        'AMENTO IL NUMERO DI CALCOLO
        numCalcolo.Text = CInt(numCalcolo.Text) + 1

        'DUPLICO LE RIGHE DI CONTRATTI - CONTRATTI COSTI - CONTRATTI WARNING
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        'ELIMINO LE EVENTUALI PRECEDENTI RIGHE DI CALCOLO (QUALORA L'UTENTE FOSSE USCITO DALLA SCHERMATA SENZA CLICCARE SU ANNULLA
        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM contratti_warning WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM contratti_costi WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()


        'Dim sqlStr As String = "INSERT INTO contratti (num_contratto, num_calcolo, status, attivo, id_stazione_uscita, id_stazione_presunto_rientro, id_stazione_presunto_rientro_originale,  id_stazione_rientro, data_uscita, data_uscita_originale, data_presunto_rientro, data_presunto_rientro_originale, data_rientro, id_gruppo_auto,  id_gruppo_auto_originale, giorni, giorni_originale, ID_GRUPPO_APP, id_primo_conducente, id_secondo_conducente, eta_primo_guidatore, eta_secondo_guidatore,  id_fonte, codice_edp, id_cliente, id_tariffa, id_tariffe_righe, tipo_tariffa, sconto_applicato, sconto_su_rack, CODTAR, id_veicolo, serbatoio_max, targa, modello, km_uscita, km_rientro, litri_uscita, litri_rientro, NOTE_da_prenotazione, N_VOLOOUT, N_VOLOPR, rif_to, id_operatore_creazione, data_creazione,  id_operatore_ultima_modifica, data_ultima_modifica, num_preventivo, num_prenotazione, giorni_noleggio_da_prenotazione, data_uscita_da_prenotazione, data_presunto_rientro_da_prenotazione, id_gruppo_da_prenotazione, id_stazione_drop_off_da_prenotazione, totale_costo_prenotazione, prenotazione_prepagata, importo_prepagato, id_gruppo_danni_uscita, id_gruppo_danni_rientro) " & _
        '"(SELECT num_contratto, " & numCalcolo.Text & ", status, '0', id_stazione_uscita, id_stazione_presunto_rientro, id_stazione_presunto_rientro_originale,  id_stazione_rientro, data_uscita, data_uscita_originale, data_presunto_rientro, data_presunto_rientro_originale, data_rientro, id_gruppo_auto,  id_gruppo_auto_originale, giorni, giorni_originale, ID_GRUPPO_APP, id_primo_conducente, id_secondo_conducente, eta_primo_guidatore, eta_secondo_guidatore,  id_fonte, codice_edp, id_cliente, id_tariffa, id_tariffe_righe, tipo_tariffa, sconto_applicato, sconto_su_rack, CODTAR, id_veicolo, serbatoio_max, targa, modello, km_uscita, km_rientro, litri_uscita, litri_rientro, NOTE_da_prenotazione, N_VOLOOUT, N_VOLOPR, rif_to, id_operatore_creazione, data_creazione,  id_operatore_ultima_modifica, data_ultima_modifica, num_preventivo, num_prenotazione, giorni_noleggio_da_prenotazione, data_uscita_da_prenotazione, data_presunto_rientro_da_prenotazione, id_gruppo_da_prenotazione, id_stazione_drop_off_da_prenotazione, totale_costo_prenotazione, prenotazione_prepagata, importo_prepagato, id_gruppo_danni_uscita, id_gruppo_danni_rientro FROM contratti As contratti_1 WHERE id='" & idContratto.Text & "')"
        'Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        'Cmd.ExecuteNonQuery()

        'Dim id_ctr As String

        'sqlStr = "SELECT @@IDENTITY FROM contratti"

        'Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        'id_ctr = Cmd.ExecuteScalar()

        Dim sqlStr As String = "INSERT INTO contratti_costi (id_documento, num_calcolo, ordine_stampa, secondo_ordine_stampa, id_gruppo, id_elemento, num_elemento, nome_costo, selezionato," &
            "omaggiato, prepagato, franchigia_attiva,  valore_costo, valore_percentuale, imponibile, iva_imponibile, imponibile_scontato, iva_imponibile_scontato, imponibile_onere," &
            "iva_onere, imponibile_onere_broker_incluso, iva_onere_broker_incluso, aliquota_iva, codice_iva, iva_inclusa, scontabile, omaggiabile,  acquistabile_nolo_in_corso," &
            "id_a_carico_di, id_metodo_stampa, obbligatorio, id_unita_misura, qta, packed, data_aggiunta_nolo_in_corso, imponibile_scontato_prepagato, iva_imponibile_scontato_prepagato, " &
            "imponibile_onere_prepagato, iva_onere_prepagato, sconto_su_imponibile_prepagato, commissioni_imponibile, commissioni_iva, commissioni_imponibile_originale, commissioni_iva_originale) " &
            "(SELECT  '" & idContratto.Text & "', '" & numCalcolo.Text & "', ordine_stampa, secondo_ordine_stampa, id_gruppo, id_elemento, num_elemento, nome_costo, selezionato, omaggiato, prepagato," &
            "franchigia_attiva,  valore_costo, valore_percentuale, imponibile, iva_imponibile, imponibile_scontato, iva_imponibile_scontato, imponibile_onere, iva_onere," &
            "imponibile_onere_broker_incluso, iva_onere_broker_incluso,  aliquota_iva, codice_iva, iva_inclusa, scontabile, omaggiabile,  acquistabile_nolo_in_corso, id_a_carico_di," &
            "id_metodo_stampa, obbligatorio, id_unita_misura, qta, packed, data_aggiunta_nolo_in_corso , imponibile_scontato_prepagato, iva_imponibile_scontato_prepagato," &
            "imponibile_onere_prepagato, iva_onere_prepagato, sconto_su_imponibile_prepagato, commissioni_imponibile, commissioni_iva, commissioni_imponibile_originale, commissioni_iva_originale " &
            "FROM contratti_costi As contratti_costi_1 WITH(NOLOCK) " &
            "WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "')"


        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        If statoContratto.Text = "4" Or statoContratto.Text = "8" Then
            'SE SIAMO NEL CASO DI MODIFICA ADMIN SETTO A IL CAMPO secondo_ordine_stampa PER FAR VISUALIZZARE IL TOTALE COME ULTIMO ELEMENTO
            sqlStr = "UPDATE contratti_costi SET secondo_ordine_stampa='1' WHERE id_documento='" & idContratto.Text & "' And num_calcolo='" & numCalcolo.Text & "' AND nome_costo='" & Costanti.testo_elemento_totale & "'"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()
        End If

        sqlStr = "INSERT INTO contratti_warning (id_documento, num_calcolo, warning, id_operatore, tipo) " &
        "(SELECT '" & idContratto.Text & "', '" & numCalcolo.Text & "',warning, id_operatore, tipo FROM contratti_warning As contratti_warning_1 WITH(NOLOCK) WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "')"

        listContrattiCosti.DataBind()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Function getStatoContratto() As String
        'RESTITUISCE LO STATO DEL CONTRATTO ATTUALE (QUINDI PARTENDO DAL NUMERO DI CONTRATTRO)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT status FROM contratti WITH(NOLOCK) WHERE num_contratto='" & contratto_num.Text & "' AND attivo='1'", Dbc)

        Dim stato As String = Cmd.ExecuteScalar & ""

        If stato = "" Then
            'IN QUESTO CASO SICURAMENTE IL CONTRATTO E' IN STATO 0
            stato = "0"
        End If

        getStatoContratto = stato

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getNumCrvAttuale() As String
        'RESTITUISCE IL NUMERO DI CRV ATTUALE
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT num_crv FROM contratti WITH(NOLOCK) WHERE num_contratto='" & contratto_num.Text & "' AND attivo='1'", Dbc)

        getNumCrvAttuale = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub btnRicalcolaModificaContratto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRicalcolaModificaContratto.Click
        'PER EVITARE CHE L'UTENTE, DOPO AVER EFFETTUATO IL QUICK CHECK IN O FASI SUCCESSIVE, TORNANDO INDIETRO PROVI A MODIFICARE IL CONTRATTO
        'CONTROLLO CHE SI TROVI IN EFFETTI NELLO STATO DI CONTRATTO APERTO
        If getStatoContratto() = "2" Then
            If btnRicalcolaModificaContratto.Text = "Modifica/Estensione" Then
                lblVariazioneACarico.Visible = False
                dropVariazioneACaricoDi.Visible = False

                txtOraRientro.Enabled = True
                txtAData.Enabled = True
                dropStazioneDropOff.Enabled = True

                btnGeneraContratto.Visible = False
                btnPagamento.Visible = False
                btnQuickCheckIn.Visible = False

                btnAnnullaModificaContratto.Visible = True
                btnSalvaModifiche.Visible = True

                btnAggiungiExtra.Visible = True

                btnScegliTarga.Visible = False

                txtNoteContratto.ReadOnly = False

                btnCRV.Visible = False
                bt_Check_Out.Visible = False

                If livello_accesso_admin.Text = "3" Then
                    btnModificaTariffaAdmin.Visible = True
                End If

                btnRicalcolaModificaContratto.Text = "Ricalcola"
                statoModificaContratto.Text = "1"

                data_drop_off_attuale.Text = txtAData.Text
                id_stazione_drop_off_attuale.Text = dropStazioneDropOff.SelectedValue
                orario_drop_off_attuale.Text = txtOraRientro.Text
                numero_giorni_attuale.Text = txtNumeroGiorni.Text

                If dropTariffeGeneriche.SelectedValue <> "0" Then
                    id_tariffa_attuale.Text = "G" & dropTariffeGeneriche.SelectedValue
                Else
                    id_tariffa_attuale.Text = "P" & dropTariffeParticolari.SelectedValue
                End If

                'CONSERVO I DATI PER CONTROLLARE IN CASO DI SALVATAGGIO SE L'OPERATORE HA EFFETTUATO IL RICALCOLO
                mod_data_drop_off.Text = txtAData.Text
                mod_id_stazione_drop_off.Text = dropStazioneDropOff.SelectedValue
                mod_orario_drop_off.Text = txtOraRientro.Text

                'SE LA TARIFFA E' BROKER
                If tariffa_broker.Text = "1" Then
                    lblVariazioneACarico.Visible = True
                    dropVariazioneACaricoDi.Visible = True

                    lblOldACaricoDi.Text = dropVariazioneACaricoDi.SelectedValue

                    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()
                    Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(estensioni_sempre_a_carico_del_broker,'0') FROM clienti_tipologia WHERE id='" & CInt(dropTipoCliente.SelectedValue) & "'", Dbc)

                    Dim sempre_a_carico_del_broker As Boolean = Cmd.ExecuteScalar

                    If sempre_a_carico_del_broker Then
                        dropVariazioneACaricoDi.SelectedValue = "1"
                        dropVariazioneACaricoDi.Enabled = False
                    Else
                        If txtNumeroGiorni.Text = txtNumeroGiorniTO.Text Then
                            dropVariazioneACaricoDi.SelectedValue = "1"
                        Else
                            dropVariazioneACaricoDi.SelectedValue = "0"
                        End If
                        dropVariazioneACaricoDi.Enabled = True
                    End If

                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing
                End If

                'PER LA MODIFICA DEL CONTRATTO DUPLICO SUBITO LE RIGHE DI contratti_costi IN MODO CHE QUALSIASI MODIFICA DEBBA ESSERE CONFERMATA
                '(ANCHE PER GLI ACCESSORI)
                modifica_contratto_duplica_righe_calcolo()
            ElseIf btnRicalcolaModificaContratto.Text = "Ricalcola" Then
                Dim data_test As String
                Try
                    data_test = Year(txtAData.Text) & "-" & Month(txtAData.Text) & "-" & Day(txtAData.Text) & " " & Hour(txtOraRientro.Text) & ":" & Minute(txtOraRientro.Text)
                    'Or ((Year(txtAData.Text) < Year(Now())) Or (Year(txtAData.Text) = Year(Now()) And Month(txtAData.Text) < Month(Now())) Or (Year(txtAData.Text) = Year(Now()) And Month(txtAData.Text) = Month(Now()) And Day(txtAData.Text) < Day(Now())))
                    'NON CONTROLLO SE LA DATA E' PRECEDENTE A QUELLA ODIERNA: IN CERTI CASI E' NECESSARIO PRIMA SALVARE IL VOUCHER ORIGINALE
                    '(IN CASO, AD ESEMPIO, DI ERRORI DELL'OPERATORE) PRIMA DI POTER SALVARE L'ESTENSIONE VOLUTA.
                    If ((Year(txtAData.Text) < Year(txtDaData.Text)) Or (Year(txtAData.Text) = Year(txtDaData.Text) And Month(txtAData.Text) < Month(txtDaData.Text)) Or (Year(txtAData.Text) = Year(txtDaData.Text) And Month(txtAData.Text) = Month(txtDaData.Text) And Day(txtAData.Text) < Day(txtDaData.Text))) Then
                        data_test = "-1"
                    End If
                Catch ex As Exception
                    data_test = "-1"
                End Try

                If (dropTariffeGeneriche.SelectedValue <> "0" And dropTariffeParticolari.SelectedValue = "0") Or (dropTariffeGeneriche.SelectedValue = "0" And dropTariffeParticolari.SelectedValue <> "0") Then
                    If data_test <> "-1" Then
                        If dropStazioneDropOff.SelectedValue <> "0" Then
                            If dropVariazioneACaricoDi.SelectedValue <> "-1" Or tariffa_broker.Text <> "1" Then
                                dropVariazioneACaricoDi.Enabled = False
                                esegui_ricalcolo_nolo_in_corso()

                                mod_data_drop_off.Text = txtAData.Text
                                mod_id_stazione_drop_off.Text = dropStazioneDropOff.SelectedValue
                                If dropTariffeGeneriche.SelectedValue <> "0" Then
                                    mod_id_tariffa.Text = dropTariffeGeneriche.SelectedValue
                                Else
                                    mod_id_tariffa.Text = dropTariffeParticolari.SelectedValue
                                End If

                                mod_orario_drop_off.Text = txtOraRientro.Text
                            Else
                                Libreria.genUserMsgBox(Me, "Specificare se la variazione è a carico del broker o a carico del cliente.")
                            End If
                        Else
                            Libreria.genUserMsgBox(Me, "Specificare la stazione di drop off.")
                        End If
                    Else
                        Libreria.genUserMsgBox(Me, "Specificare correttamente data ed orario di drop off.")
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Selezionare una tariffa generica oppure una tariffa particolare.")
                End If
            End If
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: il contratto non e' più modificabile.")
        End If
    End Sub

    Protected Sub btnAnnullaModificaContratto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnullaModificaContratto.Click
        'VISIBILITA'/INTERIGIBILITA' DEI PULSANTI E DEI CAMPI TESTUALI -----------------------------------------------------------------
        txtOraRientro.Enabled = False
        txtAData.Enabled = False
        dropStazioneDropOff.Enabled = False

        btnGeneraContratto.Visible = True
        btnPagamento.Visible = True
        btnQuickCheckIn.Visible = True

        btnAnnullaModificaContratto.Visible = False
        btnSalvaModifiche.Visible = False

        btnAggiungiExtra.Visible = False
        btnScegliTarga.Visible = True

        btnAnnullaDocumento.Visible = True

        dropVariazioneACaricoDi.Enabled = False

        btnCRV.Visible = True
        bt_Check_Out.Visible = True

        btnModificaTariffaAdmin.Visible = False

        btnRicalcolaModificaContratto.Text = "Modifica/Estensione"
        statoModificaContratto.Text = "0"

        txtNoteContratto.ReadOnly = True
        '---------------------------------------------------------------------------------------------------------------------------------
        'REIMPOSTO I DATI MODIFICABILI CON QUELLI DEL CALCOLO ATTUALMENTE SALVATO --------------------------------------------------------
        txtAData.Text = data_drop_off_attuale.Text
        dropStazioneDropOff.SelectedValue = id_stazione_drop_off_attuale.Text
        txtNumeroGiorni.Text = numero_giorni_attuale.Text
        If tariffa_broker.Text = "1" Then
            txtNumeroGiorniTO.Text = lblGiorniToOld.Text
        End If

        If tariffa_broker.Text = "1" Then
            dropVariazioneACaricoDi.SelectedValue = lblOldACaricoDi.Text
        End If

        txtOraRientro.Text = orario_drop_off_attuale.Text
        ore2.Text = Hour(txtOraRientro.Text)
        minuti2.Text = Minute(txtOraRientro.Text)

        If Len(ore2.Text) = 1 Then
            ore2.Text = "0" & ore2.Text
        End If

        If Len(minuti2.Text) = 1 Then
            minuti2.Text = "0" & minuti2.Text
        End If

        If Left(id_tariffa_attuale.Text, 1) = "G" Then
            'GENERICA
            dropTariffeGeneriche.SelectedValue = Replace(id_tariffa_attuale.Text, "G", "")
            Dim id_tar As String = Replace(id_tariffa_attuale.Text, "G", "")
            Dim cod_tar As String = dropTariffeGeneriche.SelectedItem.Text.Replace(" (RA)", "")
            dropTariffeGeneriche.Items.Clear()
            dropTariffeGeneriche.Items.Add("Seleziona...")
            dropTariffeGeneriche.Items(0).Value = "0"
            dropTariffeGeneriche.Items.Add(cod_tar)
            dropTariffeGeneriche.Items(1).Value = id_tar

            dropTariffeGeneriche.SelectedValue = id_tar
        ElseIf Left(id_tariffa_attuale.Text, 1) = "P" Then
            'PARTICOLARE
            dropTariffeParticolari.SelectedValue = Replace(id_tariffa_attuale.Text, "P", "")
            Dim id_tar As String = Replace(id_tariffa_attuale.Text, "P", "")
            Dim cod_tar As String = dropTariffeParticolari.SelectedItem.Text.Replace(" (RA)", "")
            dropTariffeParticolari.Items.Clear()
            dropTariffeParticolari.Items.Add("Seleziona...")
            dropTariffeParticolari.Items(0).Value = "0"
            dropTariffeParticolari.Items.Add(cod_tar)
            dropTariffeParticolari.Items(1).Value = id_tar

            dropTariffeParticolari.SelectedValue = id_tar
        End If

        dropTariffeGeneriche.Enabled = False
        dropTariffeParticolari.Enabled = False

        data_drop_off_attuale.Text = ""
        id_stazione_drop_off_attuale.Text = ""
        orario_drop_off_attuale.Text = ""
        numero_giorni_attuale.Text = ""
        id_tariffa_attuale.Text = ""

        mod_data_drop_off.Text = ""
        mod_id_stazione_drop_off.Text = ""
        orario_drop_off_attuale.Text = ""
        mod_id_tariffa.Text = ""
        '-------------------------------------------------------------------------------------------------------------------------------
        'ELIMINAZIONE DELLE RIGHE DI CALCOLO -------------------------------------------------------------------------------------------
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM contratti_warning WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM contratti_costi WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        numCalcolo.Text = CInt(numCalcolo.Text) - 1

        listContrattiCosti.DataBind()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
        '--------------------------------------------------------------------------------------------------------------------------------
        aggiorna_informazioni_dopo_modifica_costi()
    End Sub

    Protected Sub btnSalvaModifiche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvaModifiche.Click
        If statoContratto.Text <> "4" And statoContratto.Text <> "8" And statoContratto.Text <> "6" Then
            'SALVATAGGIO DELLE MODIFICHE DI UN CONTRATTO NOLO IN CORSO
            'VISIBILITA'/INTERIGIBILITA' DEI PULSANTI E DEI CAMPI TESTUALI -----------------------------------------------------------------
            If (Day(txtAData.Text) = Day(mod_data_drop_off.Text) And Month(txtAData.Text) = Month(mod_data_drop_off.Text) And Year(txtAData.Text) = Year(mod_data_drop_off.Text)) And dropStazioneDropOff.SelectedValue = mod_id_stazione_drop_off.Text And (Hour(txtOraRientro.Text) = Hour(mod_orario_drop_off.Text) And Minute(txtOraRientro.Text) = Minute(mod_orario_drop_off.Text)) And ((dropTariffeGeneriche.SelectedValue = mod_id_tariffa.Text And dropTariffeParticolari.SelectedValue = "0") Or (dropTariffeGeneriche.SelectedValue = "0" And dropTariffeParticolari.SelectedValue = mod_id_tariffa.Text)) Then
                txtOraRientro.Enabled = False
                txtAData.Enabled = False
                dropStazioneDropOff.Enabled = False

                btnGeneraContratto.Visible = True
                btnPagamento.Visible = True
                btnQuickCheckIn.Visible = True

                btnAnnullaModificaContratto.Visible = False
                btnSalvaModifiche.Visible = False

                btnAggiungiExtra.Visible = False
                txtNoteContratto.ReadOnly = True

                btnScegliTarga.Visible = True

                btnCRV.Visible = True
                bt_Check_Out.Visible = True

                btnModificaTariffaAdmin.Visible = False
                dropTariffeGeneriche.Enabled = False
                dropTariffeParticolari.Enabled = False

                btnRicalcolaModificaContratto.Text = "Modifica/Estensione"
                statoModificaContratto.Text = "0"
                '---------------------------------------------------------------------------------------------------------------------------------
                salva_contratto_nolo_in_corso("2")

                'AGGIORNO GLI ACCESSORI EVENTUAMENTE SCEGLI IN CORSO DI CONTRATTO AGGIUNGENDOLI AL CHECK-OUT
                gestione_checkin.aggiornaAcessori(tipo_documento.Contratto, Integer.Parse(lblNumContratto.Text), Integer.Parse(numCrv.Text))

                elenco_modifiche.Visible = True
                listModifiche.DataBind()

                mod_data_drop_off.Text = ""
                mod_id_stazione_drop_off.Text = ""
                mod_orario_drop_off.Text = ""
                mod_gruppo_calcolato.Text = ""
                mod_giorno_extra_omaggiato.Text = ""
                mod_id_tariffa.Text = ""
            Else
                Libreria.genUserMsgBox(Me, "Attenzione: è necessario ricalcolare prima di poter salvare le modifiche.")
            End If
        Else
            'MODIFICA CONTRATTO ADMIN (NOLO CONLCUSO)
            If (Day(txtAData.Text) = Day(mod_data_drop_off.Text) And Month(txtAData.Text) = Month(mod_data_drop_off.Text) And Year(txtAData.Text) = Year(mod_data_drop_off.Text)) And dropStazioneDropOff.SelectedValue = mod_id_stazione_drop_off.Text And (Hour(txtOraRientro.Text) = Hour(mod_orario_drop_off.Text) And Minute(txtOraRientro.Text) = Minute(mod_orario_drop_off.Text)) And mod_gruppo_calcolato.Text = gruppoDaCalcolare.SelectedValue And mod_giorno_extra_omaggiato.Text = chkAbbuonaGiornoExtra.Checked And ((dropTariffeGeneriche.SelectedValue = mod_id_tariffa.Text And dropTariffeParticolari.SelectedValue = "0") Or (dropTariffeGeneriche.SelectedValue = "0" And dropTariffeParticolari.SelectedValue = mod_id_tariffa.Text)) Then
                visibilita_nolo_concluso_admin()

                salva_contratto_nolo_in_corso(getStatoContratto())

                mod_data_drop_off.Text = ""
                mod_id_stazione_drop_off.Text = ""
                mod_orario_drop_off.Text = ""
                mod_gruppo_calcolato.Text = ""
                mod_giorno_extra_omaggiato.Text = ""
                mod_id_tariffa.Text = ""
            Else
                Libreria.genUserMsgBox(Me, "Attenzione: è necessario ricalcolare prima di poter salvare le modifiche.")
            End If
        End If
    End Sub

    Protected Sub btnRicalcolaDaPrenotazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRicalcolaDaPrenotazione.Click
        If btnAnnullaScegliDitta.Visible Or btnAnnullaScegliPrimoConducente.Visible Or btnAnnullaScegliSecondoConducente.Visible Then
            Libreria.genUserMsgBox(Me, "Comletare la modifica dei dati del cliente.")
        Else
            If riga_vedi_auto.Visible Then
                riga_vedi_auto.Visible = False
                id_scegli_auto_gruppo.Text = "0"
                id_scegli_auto_stazione.Text = "0"
            End If

            If btnRicalcolaDaPrenotazione.Text = "Modifica" Then
                txtoraPartenza.Enabled = True
                txtOraRientro.Enabled = True
                txtAData.Enabled = True
                txtSconto.Enabled = True
                dropTipoSconto.Enabled = True
                txtScontoRack.Enabled = True
                gruppoDaCalcolare.Enabled = True
                gruppoDaConsegnare.Enabled = True
                dropStazioneDropOff.Enabled = True

                btnScegliPrimoGuidatore.Visible = False
                btnScegliSecondoConducente.Visible = False
                btnScegliDitta.Visible = False

                btnAnnullaDocumento.Visible = False

                btnTrovaTarga.Visible = False
                btnScegliTarga.Visible = False

                btnPagamento.Visible = False

                btnRicalcolaDaPrenotazione.Text = "Ricalcola"

                'SE LA TARIFFA E' BROKER
                If tariffa_broker.Text = "1" Then
                    lblVariazioneACarico.Visible = True
                    dropVariazioneACaricoDi.Visible = True
                    dropVariazioneACaricoDi.Enabled = True

                    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()
                    Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(estensioni_sempre_a_carico_del_broker,'0') FROM clienti_tipologia WHERE id='" & CInt(dropTipoCliente.SelectedValue) & "'", Dbc)

                    Dim sempre_a_carico_del_broker As Boolean = Cmd.ExecuteScalar

                    If sempre_a_carico_del_broker Then
                        dropVariazioneACaricoDi.SelectedValue = "1"
                        dropVariazioneACaricoDi.Enabled = False
                    Else
                        dropVariazioneACaricoDi.SelectedValue = "-1"
                        dropVariazioneACaricoDi.Enabled = True
                    End If

                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing
                End If
            Else
                If dropVariazioneACaricoDi.SelectedValue <> "-1" Or tariffa_broker.Text <> "1" Then
                    If modifica_calcolo() Then
                        lblVariazioneACarico.Visible = False
                        dropVariazioneACaricoDi.Visible = False

                        txtoraPartenza.Enabled = False
                        txtOraRientro.Enabled = False
                        txtAData.Enabled = False
                        txtSconto.Enabled = False
                        dropTipoSconto.Enabled = False
                        txtScontoRack.Enabled = False
                        gruppoDaCalcolare.Enabled = False
                        gruppoDaConsegnare.Enabled = False
                        dropStazioneDropOff.Enabled = False

                        btnScegliPrimoGuidatore.Visible = True
                        btnScegliSecondoConducente.Visible = True

                        If ditta_non_modificabile.Text = "False" Then
                            btnScegliDitta.Visible = True
                        End If

                        btnAnnullaDocumento.Visible = True

                        If id_auto_selezionata.Text = "" Then
                            btnTrovaTarga.Visible = True
                        End If

                        btnPagamento.Visible = True
                        btnScegliTarga.Visible = True

                        btnRicalcolaDaPrenotazione.Text = "Modifica"

                        check_possibile_pagare()
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Specificare se la variazione è a carico del cliente o a carico del broker.")
                End If
            End If

        End If
    End Sub


    Protected Sub btnRicalcolaDaPreventivo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRicalcolaDaPreventivo.Click
        If btnAnnullaScegliDitta.Visible Or btnAnnullaScegliPrimoConducente.Visible Or btnAnnullaScegliSecondoConducente.Visible Then
            Libreria.genUserMsgBox(Me, "Comletare la modifica dei dati del cliente.")
        Else
            If riga_vedi_auto.Visible Then
                riga_vedi_auto.Visible = False
                id_scegli_auto_gruppo.Text = "0"
                id_scegli_auto_stazione.Text = "0"
            End If

            If btnRicalcolaDaPreventivo.Text = "Modifica" Then
                txtoraPartenza.Enabled = True
                txtOraRientro.Enabled = True
                txtAData.Enabled = True
                gruppoDaCalcolare.Enabled = True
                gruppoDaConsegnare.Enabled = True
                dropStazioneDropOff.Enabled = True
                dropTariffeGeneriche.Enabled = True
                dropTariffeParticolari.Enabled = True
                txtSconto.Enabled = True
                dropTipoSconto.Enabled = True
                dropTipoCliente.Enabled = False

                btnScegliPrimoGuidatore.Visible = False
                btnScegliSecondoConducente.Visible = False
                btnScegliDitta.Visible = False

                btnAnnullaDocumento.Visible = False

                btnTrovaTarga.Visible = False
                btnScegliTarga.Visible = False

                If tariffa_broker.Text = "1" Then
                    lblVariazioneACarico.Visible = True
                    dropVariazioneACaricoDi.Visible = True
                    dropVariazioneACaricoDi.Enabled = True

                    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()
                    Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(estensioni_sempre_a_carico_del_broker,'0') FROM clienti_tipologia WHERE id='" & CInt(dropTipoCliente.SelectedValue) & "'", Dbc)

                    Dim sempre_a_carico_del_broker As Boolean = Cmd.ExecuteScalar

                    If sempre_a_carico_del_broker Then
                        dropVariazioneACaricoDi.SelectedValue = "1"
                        dropVariazioneACaricoDi.Enabled = False
                    Else
                        dropVariazioneACaricoDi.SelectedValue = "-1"
                        dropVariazioneACaricoDi.Enabled = True
                    End If

                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing
                End If

                btnRicalcolaDaPreventivo.Text = "Aggiorna tariffe"
            ElseIf btnRicalcolaDaPreventivo.Text = "Aggiorna tariffe" Then
                'E' NECESSARIO QUESTO PASSAGGIO INTERMEDIO IN QUANTO, ANCHE VOLENDO AGGIORNARE LE TARIFFE SOLO QUANDO I PARAMETRI DI RICERCA
                'INDICANO CHE LA TARIFFA ORIGINALE NON E' PIU' VENDIBILE, NON SAREBBE COMUNQUE POSSIBILE (SEMPRE) TORNARE INDIETRO SELEZIONANDO
                'I VECCHI PARAMETRI DI RICERCA - RENDO NON MODIFICABILI I CAMPI CHE POTREBBERO CAUSARE UNA VARIAZIONE DI TARIFFA
                txtoraPartenza.Enabled = False
                txtOraRientro.Enabled = False
                txtAData.Enabled = False
                dropStazioneDropOff.Enabled = False
                dropTipoCliente.Enabled = False

                setQueryTariffePossibili(0)
                btnRicalcolaDaPreventivo.Text = "Ricalcola"

                lblVariazioneACarico.Visible = False
                dropVariazioneACaricoDi.Visible = False

                If tariffa_broker.Text = "1" Then
                    dropTariffeGeneriche.Enabled = False
                End If
            ElseIf btnRicalcolaDaPreventivo.Text = "Ricalcola" Then
                If modifica_calcolo() Then
                    gruppoDaCalcolare.Enabled = False
                    gruppoDaConsegnare.Enabled = False

                    dropTariffeGeneriche.Enabled = False
                    dropTariffeParticolari.Enabled = False

                    txtSconto.Enabled = False
                    dropTipoSconto.Enabled = False

                    btnScegliPrimoGuidatore.Visible = True
                    btnScegliSecondoConducente.Visible = True
                    If ditta_non_modificabile.Text = "False" Then
                        btnScegliDitta.Visible = True
                    End If

                    btnAnnullaDocumento.Visible = True

                    If id_auto_selezionata.Text = "" Then
                        btnTrovaTarga.Visible = True
                    End If

                    btnScegliTarga.Visible = True

                    btnRicalcolaDaPreventivo.Text = "Modifica"

                    check_possibile_pagare()
                Else
                    'SE NON E' POSSIBILE ESEGUIRE IL RICALCOLO ALLORA RIABILITO I CAMPI DI RICERCA 
                    '- L'OPERATORE DEVE SELEZIONARE DEI PARAMETRI CORRETTI
                    txtoraPartenza.Enabled = True
                    txtOraRientro.Enabled = True
                    txtAData.Enabled = True
                    dropStazioneDropOff.Enabled = True
                    dropTipoCliente.Enabled = True

                    btnRicalcolaDaPreventivo.Text = "Aggiorna tariffe"

                    If tariffa_broker.Text = "1" Then
                        lblVariazioneACarico.Visible = True
                        dropVariazioneACaricoDi.Visible = True
                        dropVariazioneACaricoDi.Enabled = True

                        dropTariffeGeneriche.Enabled = False
                    End If
                End If
            End If
        End If
    End Sub

    Protected Function getCodiceEdp(ByVal id_cliente As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT [CODICE EDP] FROM ditte WITH(NOLOCK) WHERE id_ditta='" & id_cliente & "'", Dbc)

        getCodiceEdp = Cmd.ExecuteScalar & ""

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

    Protected Sub btnScegliDitta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScegliDitta.Click
        anagrafica_ditte.Visible = True
        btnScegliPrimoGuidatore.Visible = False
        btnScegliSecondoConducente.Visible = False
        btnScegliDitta.Visible = False
        btnAnnullaScegliDitta.Visible = True

    End Sub

    Protected Sub btnAnnullaScegliDitta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnullaScegliDitta.Click
        anagrafica_ditte.Visible = False
        btnScegliPrimoGuidatore.Visible = True
        btnScegliSecondoConducente.Visible = True
        If ditta_non_modificabile.Text = "False" Then
            btnScegliDitta.Visible = True
        End If
        btnAnnullaScegliDitta.Visible = False
    End Sub

    Protected Sub seleziona_targa()
        Dim risultato(13) As String
        risultato = funzioni_comuni.scegli_targa_x_contratto(txtTarga.Text)

        '0 = id del veicolo
        '1 = messaggio di errore se targa non selezionabile
        '2 = serbatoio attuale
        '3 = serbatoio massimo
        '4 = km attuali
        '5 = id stazione attuale
        '6 = id_gruppo
        '7 = Modello
        '8 = gruppo
        '9 = id_alimentazione
        '10 = 1 se da rifornire
        '11 = 1 se da lavare
        '12 = codice carburante
        '13 = descrizione carburante

        auto_collegata.Text = "0"

        Dim sconto As Double
        If dropTipoSconto.SelectedValue = "0" Then
            sconto = CDbl(txtSconto.Text)
        ElseIf dropTipoSconto.SelectedValue = "1" Then
            sconto = 0
        End If

        'SE C'E' UN MESSAGGIO DI ERRORE IN POSIZIONE 1 VUOL DIRE CHE IL VEICOLO NON E' SELEZIONABILE (MOTIVI BLOCCANTI)
        If risultato(1) <> "" Then
            Libreria.genUserMsgBox(Me, risultato(1))
        Else
            'ANCHE SE NON VIENE RESTITUITO UN ERRORE BLOCCANTE DEVONO ESSERE ESEGUITI ALCUNI CONTROLLI:
            '1)AUTO NON IN PARCO: DEVE ESSERE POSSIBILE EFFETTUARE L'IMMISSIONE IN PARCO
            If (gruppoDaConsegnare.SelectedValue <> "0" And gruppoDaConsegnare.SelectedValue <> risultato(6)) And statoContratto.Text <> "5" Then
                Libreria.genUserMsgBox(Me, "Gruppo auto (" & risultato(8) & ") diverso dal gruppo auto da consegnare.")
            ElseIf (gruppoDaConsegnare.SelectedValue = "0" And gruppoDaCalcolare.SelectedValue <> risultato(6)) And statoContratto.Text <> "5" Then
                Libreria.genUserMsgBox(Me, "Gruppo auto (" & risultato(8) & ") diverso dal gruppo auto calcolato.")
            ElseIf risultato(5) = "" Then
                id_auto_selezionata.Text = risultato(0)
                lblSerbatoioMax.Text = risultato(3)
                lblSerbatoioMaxRientro.Text = risultato(3)
                lblTipoSerbatoio.Text = risultato(12)
                lblTipoSerbatoio.ToolTip = risultato(13)
                Libreria.genUserMsgBox(Me, "Auto non in parco. Cliccare sull'apposito pulsante per effettuare l'immissione in parco e selezionare il veicolo.")
                btnImmissioneInParco.Visible = True

                If statoContratto.Text <> "0" And statoContratto.Text <> "1" Then
                    'CNT APERTO: L'AUTO SELEZIONATA E' DA IMMETTERE IN PARCO - MOMENTANEAMENTE NASCONDO IL PULSANTE PER SALVARE
                    btnScegliTarga.Text = "Seleziona"
                    btnScegliTarga.Visible = False
                End If

                txtKm.ReadOnly = False
                txtSerbatoio.ReadOnly = False
            ElseIf (risultato(5) <> dropStazionePickUp.SelectedValue) And statoContratto.Text <> "5" Then
                Libreria.genUserMsgBox(Me, "Attenzione: l'auto risulta in una stazione diversa da quella di uscita.")
            ElseIf risultato(10) = "1" And risultato(11) = "" Then
                'AUTO NELLO STATO "DA RIFORNIRE"
                id_auto_selezionata.Text = risultato(0)
                lblSerbatoioMax.Text = risultato(3)
                lblSerbatoioMaxRientro.Text = risultato(3)
                txtKm.Text = risultato(4)
                txtSerbatoio.Text = risultato(2)
                txtModello.Text = risultato(7)
                txtGruppo.Text = risultato(8)
                id_alimentazione.Text = risultato(9)
                lblTipoSerbatoio.Text = risultato(12)
                lblTipoSerbatoio.ToolTip = risultato(13)

                If statoContratto.Text <> "0" And statoContratto.Text <> "1" Then
                    'CNT APERTO: L'AUTO SELEZIONATA E' RIFORNIRE - MOMENTANEAMENTE NASCONDO IL PULSANTE PER SALVARE
                    btnScegliTarga.Text = "Seleziona"
                    btnScegliTarga.Visible = False
                End If

                rifornimento.Visible = True
                fill_rifornimento(id_auto_selezionata.Text)
            ElseIf risultato(10) = "" And risultato(11) = "1" Then
                'AUTO NELLO STATO "DA LAVARE"

                Libreria.genUserMsgBox(Me, "Auto da lavare - registrare l'operazione di lavaggio.")
            ElseIf risultato(10) = "1" And risultato(11) = "1" Then
                'AUTO SIA "DA RIFORNIRE" CHE "DA LAVARE"
                id_auto_selezionata.Text = risultato(0)
                lblSerbatoioMax.Text = risultato(3)
                lblSerbatoioMaxRientro.Text = risultato(3)
                txtKm.Text = risultato(4)
                txtSerbatoio.Text = risultato(2)
                txtModello.Text = risultato(7)
                txtGruppo.Text = risultato(8)
                id_alimentazione.Text = risultato(9)
                lblTipoSerbatoio.Text = risultato(12)
                lblTipoSerbatoio.ToolTip = risultato(13)

                If statoContratto.Text <> "0" And statoContratto.Text <> "1" Then
                    'CNT APERTO: L'AUTO SELEZIONATA E' RIFORNIRE - MOMENTANEAMENTE NASCONDO IL PULSANTE PER SALVARE
                    btnScegliTarga.Text = "Seleziona"
                    btnScegliTarga.Visible = False
                End If

                rifornimento.Visible = True

                fill_rifornimento(id_auto_selezionata.Text)
            Else
                'TUTTO OK - COLLEGO L'AUTO
                auto_collegata.Text = "1"

                id_auto_selezionata.Text = risultato(0)
                txtKm.Text = risultato(4)
                txtSerbatoio.Text = risultato(2)
                lblSerbatoioMax.Text = risultato(3)
                lblSerbatoioMaxRientro.Text = risultato(3)
                txtModello.Text = risultato(7)
                txtGruppo.Text = risultato(8)
                id_alimentazione.Text = risultato(9)
                lblTipoSerbatoio.Text = risultato(12)
                lblTipoSerbatoio.ToolTip = risultato(13)

                id_gruppo_auto_selezionata.Text = risultato(6)

                txtTarga.ReadOnly = True

                If statoContratto.Text = "0" Or statoContratto.Text = "1" Then
                    btnScegliTarga.Text = "Modifica"
                ElseIf statoContratto.Text = "5" Then
                    btnScegliTarga.Text = "Salva"
                    btnScegliTarga.Visible = True
                    btnImmissioneInParco.Visible = False
                    btnAnnullaSelezioneTargaCrv.Visible = True
                    Libreria.genUserMsgBox(Me, "- Auto selezionata correttamente. E' possibile salvare per confermare l'uscita del veicolo.")
                Else
                    'IN FASE DI CONTRATTO IN CORSO UNA VOLTA COLLEGATA L'AUTO, E' NECESSARIO SALVARE PER CONFERMARE LA SCELTA
                    btnScegliTarga.Text = "Salva"
                    btnScegliTarga.Visible = True
                    btnImmissioneInParco.Visible = False
                    Libreria.genUserMsgBox(Me, "- Auto selezionata correttamente.")
                End If

                'LE OPERAZIONI SUCCESSIVE DEVONO ESSERE EFFETTUATE SOLAMENTE SE IL CONTRATTO NON E' IN CORSO - ALTRIMENTI SIAMO NELLO STATO
                'DI MODIFICA TARGA CAUSA ERRORE - NON DEVE ESSERE RICALCOLATO IL COSTO DEL PIENO CARBURANTE E NON DEVE ESSERE CALCOLATO IL 
                'TOTALE DA PREAUTORIZZARE

                If statoContratto.Text = "0" Or statoContratto.Text = "1" Then
                    Dim msg As String = ""

                    'SE ERA STATO SELEZIONATO L'ACCESSORIO PIENO CARBURANTE E' NECESSARIO AGGIORNARNE IL COSTO
                    If funzioni_comuni.pieno_carburante_selezionato(idContratto.Text, numCalcolo.Text) Then
                        'RIMUOVO IL COSTO ATTUALE - AGGIORNO IL COSTO DEL PIENO CARBURANTE - AGGIUNGO IL NUOVO COSTO
                        Dim id_pieno_carburante As String = funzioni_comuni.get_id_pieno_carburante()
                        funzioni.rimuovi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), id_pieno_carburante, "", "SCELTA", dropTipoCommissione.SelectedValue)
                        aggiorna_costo_pieno_carburante(idContratto.Text, numCalcolo.Text, CInt(dropStazionePickUp.SelectedValue), id_alimentazione.Text, CInt(lblSerbatoioMax.Text), id_pieno_carburante, sconto)
                        aggiungi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), id_pieno_carburante, "", "", "", False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)

                        listContrattiCosti.DataBind()
                        aggiorna_informazioni_dopo_modifica_costi()

                        msg = msg & "- Aggiornato il costo del pieno carburante."
                    End If

                    'RICALCOLO IL TOTALE DA PAGARE IN QUANTO SELEZIONANDO L'AUTO DEVE ESSERE AGGIUNTO OD AGGIORNATO IL CALCOLO DEL PIENO DI BENZINA
                    If lblDaPreautorizzare.Visible Then
                        getTotaleDaPreautorizzare()
                    End If

                    If statoContratto.Text = "1" Then
                        'IN QUESTO CASO E' STATA VARIATA L'AUTO DOPO IL SALVATAGGIO DEL CONTRATTO MA PRIMA DEL CHECK IN - DEVO SALVARE NELLA RIGA DI CONTRATTO ATTUALE
                        'I DATI DELLA NUOVA VETTURA
                        modifica_auto_cnt_prima_del_check_out()
                    End If

                    btnTrovaTarga.Visible = False
                    msg = "- Auto selezionata correttamente." & vbCrLf & msg

                    Libreria.genUserMsgBox(Me, msg)
                End If
            End If
        End If
    End Sub

    Protected Sub fill_rifornimento(ByVal id_veicolo As String)
        'CONTROLLO SE L'AUTO E' NELLO STATO "DA RIFORNIRE" O "IN RIFORNIMENTO"
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "SELECT * FROM rifornimenti WHERE id_veicolo='" & id_veicolo & "' AND rifornimenti.data_rientro_parco IS NULL"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        If Rs.Read Then

            btnRegistraPieno.Visible = True
            id_rifornimento.Text = Rs("id")

            txtRifornimentoUscita.Text = ""
            txtOraRifornimentoUscita.Text = ""
            txtRifornimentoRientro.Text = ""
            txtOraRifornimentoRientro.Text = ""
            txtKmRientroRifornimento.Text = ""
            txtLitriRiforniti.Text = ""
            txtImportoRifornimento.Text = ""
            DDLFornitore.SelectedValue = "0"

            If (Rs("data_uscita_parco") & "") = "" Then
                'AUTO DA RIFORNIRE E NON ANCORA IN RIFORNIMENTO
                txtRifornimentoUscita.Enabled = True
                txtOraRifornimentoUscita.Enabled = True
                DDLConducenti.Enabled = True
                in_rifornimento.Text = "0"

                Libreria.genUserMsgBox(Me, "Auto da rifornire: registrare l'operazione di PIENO CARBURANTE oppure selezionare un'altra vettura.")
            Else
                'AUTO GIA' IN RIFORNIMENTO
                txtRifornimentoUscita.Text = Day(Rs("data_uscita_parco")) & "/" & Month(Rs("data_uscita_parco")) & "/" & Year(Rs("data_uscita_parco"))
                txtRifornimentoUscita.Enabled = False
                txtOraRifornimentoUscita.Text = Hour(Rs("data_uscita_parco")) & "." & Minute(Rs("data_uscita_parco"))
                txtOraRifornimentoUscita.Enabled = False
                DDLConducenti.SelectedValue = Rs("id_conducente")
                DDLConducenti.Enabled = False
                in_rifornimento.Text = "1"

                Libreria.genUserMsgBox(Me, "Auto in rifornimento: registrare l'operazione di rientro da PIENO CARBURANTE oppure selezionare un'altra vettura.")
            End If
        Else
            id_rifornimento.Text = "0"
            in_rifornimento.Text = ""
            btnRegistraPieno.Visible = False
            Libreria.genUserMsgBox(Me, "Attenzione si è verificato un errore. Riselezionare l'auto oppure sceglierne un'altra.")
        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub



    Protected Sub scegli_targa()
        If statoContratto.Text = "0" Or statoContratto.Text = "1" Then
            'NEL CASO IN CUI VI E' IL PULSANTE PER L'IMMISSIONE IN PARCO ABILITATO E L'UTENTE CLICCA NUOVAMENTE SU "SELEZIONA" (POSSIBILMENTE CON
            'UN'ALTRA TARGA) SETTO PREVENTIVAMENTE A NON MODIFICABILI I DATI DI KM E SERBATOIO E NASCONDO IL PULSANTE PER L'IMMISSIONE IN PARCO
            txtKm.ReadOnly = True
            txtSerbatoio.ReadOnly = True
            btnImmissioneInParco.Visible = False
            '----------------------------------------------------------------------------------------------------------------------------------

            id_auto_selezionata.Text = ""
            txtGruppo.Text = ""
            txtModello.Text = ""
            txtKm.Text = ""
            txtSerbatoio.Text = ""
            lblSerbatoioMax.Text = ""
            lblSerbatoioMaxRientro.Text = ""
            txtTarga.ReadOnly = False
            txtModello.Text = ""
            txtGruppo.Text = ""
            id_gruppo_auto_selezionata.Text = ""
            id_alimentazione.Text = ""
            lblTipoSerbatoio.Text = ""

            If btnScegliTarga.Text = "Seleziona" Then
                seleziona_targa()
            ElseIf btnScegliTarga.Text = "Modifica" Then
                btnScegliTarga.Text = "Seleziona"
                txtTarga.Text = ""
                btnTrovaTarga.Visible = True

                aggiorna_informazioni_dopo_modifica_costi()
            End If
            check_possibile_pagare()
        ElseIf statoContratto.Text = "5" Then
            'SELEZIONE DI UNA NUOVA AUTO IN CASO DI CRV
            'NEL CASO IN CUI VI E' IL PULSANTE PER L'IMMISSIONE IN PARCO ABILITATO E L'UTENTE CLICCA NUOVAMENTE SU "SELEZIONA" (POSSIBILMENTE CON
            'UN'ALTRA TARGA) SETTO PREVENTIVAMENTE A NON MODIFICABILI I DATI DI KM E SERBATOIO E NASCONDO IL PULSANTE PER L'IMMISSIONE IN PARCO
            txtKm.ReadOnly = True
            txtSerbatoio.ReadOnly = True
            btnImmissioneInParco.Visible = False
            '----------------------------------------------------------------------------------------------------------------------------------

            id_auto_selezionata.Text = ""
            txtGruppo.Text = ""
            txtModello.Text = ""
            txtKm.Text = ""
            txtSerbatoio.Text = ""
            lblSerbatoioMax.Text = ""
            lblSerbatoioMaxRientro.Text = ""
            txtTarga.ReadOnly = False
            txtModello.Text = ""
            txtGruppo.Text = ""
            id_gruppo_auto_selezionata.Text = ""
            id_alimentazione.Text = ""
            lblTipoSerbatoio.Text = ""

            If btnScegliTarga.Text = "Seleziona" Then
                seleziona_targa()
            End If
        ElseIf statoContratto.Text = "2" Then
            If btnScegliTarga.Text = "Modifica" Then
                If livello_accesso_modifica_targa.Text = "3" Or funzioni_comuni.targaModificabile(idContratto.Text, Request.Cookies("SicilyRentCar")("stazione")) Then
                    'IN QUESTO CASO L'UTENTE HA IL PERMESSO DI MODIFICARE SEMPRE LA TARGA COLLEGATA AD UN CONTRATTO OPPURE
                    'LA TARGA E' MODIFICABILE IN QUANTO NON SONO PASSATI GLI X MINUTI (SPECIFICATI IN MANIERA GENERICA O ANCHE PER
                    'STAZIONE NELLA TABELLA contratti_minuti_modifica_targa) DAL SALVATAGGIO DEL CONTRATTO, SI PUO' PROCEDERE CON LA MODIFICA

                    id_auto_selezionata.Text = ""
                    txtKm.Text = ""
                    txtSerbatoio.Text = ""
                    lblSerbatoioMax.Text = ""
                    lblSerbatoioMaxRientro.Text = ""
                    txtTarga.ReadOnly = False
                    txtTarga.Text = ""
                    txtModello.Text = ""
                    txtGruppo.Text = ""
                    id_gruppo_auto_selezionata.Text = ""
                    id_alimentazione.Text = ""
                    lblTipoSerbatoio.Text = ""
                    btnScegliTarga.Text = "Seleziona"

                    btnAnnullaModificaTargaNoloInCorso.Visible = True
                    btnRicalcolaModificaContratto.Visible = False
                    btnGeneraContratto.Visible = False
                    bt_Check_Out.Visible = False
                    btnPagamento.Visible = False
                    btnQuickCheckIn.Visible = False
                Else
                    Libreria.genUserMsgBox(Me, "Attenzione: non è più possibile modificare il veicolo collegato al contratto.")
                End If
            ElseIf btnScegliTarga.Text = "Seleziona" Then
                seleziona_targa()

            ElseIf btnScegliTarga.Text = "Salva" Then
                'L'UTENTE HA SCELTO DI MODIFICARE L'AUTO SALVATA SU CONTRATTO
                modifica_auto()

                btnScegliTarga.Text = "Modifica"
                btnAnnullaModificaTargaNoloInCorso.Visible = False
                btnRicalcolaModificaContratto.Visible = True
                btnGeneraContratto.Visible = True
                bt_Check_Out.Visible = True
                btnPagamento.Visible = True
                btnQuickCheckIn.Visible = True

                Libreria.genUserMsgBox(Me, "Veicolo modificato correttamente.")
            End If
        End If
    End Sub

    Protected Sub modifica_auto()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        'ELIMINO I DATI DI CHECK OUT DEL VEICOLO ATTUALE--------------------------------------------------------------------------------
        Dim sqlStr As String = "SELECT id_gruppo_danni_uscita FROM contratti WITH(NOLOCK) WHERE id='" & idContratto.Text & "'"
        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Dim id_gruppo_danni As String = Cmd.ExecuteScalar

        sqlStr = "SELECT id_veicolo FROM contratti WITH(NOLOCK) WHERE id='" & idContratto.Text & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Dim id_veicolo_salvato As String = Cmd.ExecuteScalar

        sqlStr = "UPDATE contratti SET id_gruppo_danni_uscita=NULL WHERE num_contratto='" & lblNumContratto.Text & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        sqlStr = "DELETE FROM veicoli_gruppo_danni WHERE id_evento_apertura='" & id_gruppo_danni & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        sqlStr = "DELETE FROM veicoli_gruppo_accessori WHERE id_evento_apertura='" & id_gruppo_danni & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        sqlStr = "DELETE FROM veicoli_gruppo_evento WHERE id='" & id_gruppo_danni & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()
        '--------------------------------------------------------------------------------------------------------------------------------
        'ELIMINO IL MOVIMENTO_TARGA E SETTO L'AUTO COME DISPONIBILE----------------------------------------------------------------------
        sqlStr = "DELETE FROM movimenti_targa WHERE num_riferimento='" & lblNumContratto.Text & "' AND num_crv_contratto='" & numCrv.Text & "' AND id_veicolo='" & id_veicolo_salvato & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        sqlStr = "UPDATE veicoli SET disponibile_nolo='1', noleggiata='0' WHERE id='" & id_veicolo_salvato & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()
        '--------------------------------------------------------------------------------------------------------------------------------
        'COPIO IL CONTRATTO ATTUALE IN UN NUOVO NUMERO DI CALCOLO PER LASCIARE TRACCIA DELLA MODIFICA DEL VEICOLO - CONTESTUALMENTE SALVO LA NUOVA VETTURA

        numCalcolo.Text = CInt(numCalcolo.Text) + 1

        sqlStr = "INSERT INTO contratti (num_contratto, num_calcolo, num_crv, status, attivo, id_stazione_uscita, id_stazione_presunto_rientro, id_stazione_presunto_rientro_originale,  id_stazione_rientro, data_uscita, data_uscita_originale, data_presunto_rientro, data_presunto_rientro_originale, data_rientro, id_gruppo_auto,  id_gruppo_auto_originale, giorni, giorni_originale, ID_GRUPPO_APP, id_primo_conducente, id_secondo_conducente, eta_primo_guidatore, eta_secondo_guidatore,   id_fonte, codice_edp, id_cliente, id_tariffa, id_tariffe_righe, tariffa_rack_utilizzata, id_tempo_km_rack, tipo_tariffa, sconto_applicato, tipo_sconto, sconto_web_prepagato, sconto_su_rack, CODTAR, id_gps, codice_gps, id_veicolo, serbatoio_max, id_alimentazione, targa, modello, km_uscita, km_rientro, litri_uscita, litri_rientro, NOTE_da_prenotazione, N_VOLOOUT, N_VOLOPR, rif_to, id_operatore_creazione, data_creazione,   id_operatore_ultima_modifica, data_ultima_modifica, num_preventivo, num_prenotazione, giorni_noleggio_da_prenotazione, data_uscita_da_prenotazione, data_presunto_rientro_da_prenotazione, id_gruppo_da_prenotazione, id_stazione_drop_off_da_prenotazione, totale_costo_prenotazione, prenotazione_prepagata,  importo_prepagato, giorni_prepagati, id_gruppo_danni_uscita, id_gruppo_danni_rientro, NOTE_contratto, importo_a_carico_del_broker, importo_a_carico_del_broker_ribaltato, gg_a_carico_del_broker_ribaltato, id_fonte_commissionabile, tipo_commissione, commissione_percentuale, giorni_commissioni) " &
        " (SELECT num_contratto, " & numCalcolo.Text & ", num_crv ,status, attivo, id_stazione_uscita, id_stazione_presunto_rientro," &
        "id_stazione_presunto_rientro_originale, id_stazione_rientro, data_uscita, data_uscita_originale, data_presunto_rientro," &
        " data_presunto_rientro_originale, data_rientro, id_gruppo_auto,  id_gruppo_auto_originale, giorni," &
        "giorni_originale, ID_GRUPPO_APP, id_primo_conducente, id_secondo_conducente, eta_primo_guidatore, eta_secondo_guidatore, " &
        "id_fonte, codice_edp, id_cliente, id_tariffa, id_tariffe_righe, tariffa_rack_utilizzata, id_tempo_km_rack, tipo_tariffa, sconto_applicato,tipo_sconto, sconto_web_prepagato, " &
        "sconto_su_rack, CODTAR, id_gps, codice_gps,'" & id_auto_selezionata.Text & "', '" & lblSerbatoioMax.Text & "', '" & id_alimentazione.Text & "', '" & Replace(txtTarga.Text, "'", "''") & "', '" & Replace(txtModello.Text, "'", "''") & "', '" & txtKm.Text & "', km_rientro, '" & Replace(txtSerbatoio.Text, ",", ".") & "', litri_rientro," &
        "NOTE_da_prenotazione, N_VOLOOUT, N_VOLOPR, rif_to, id_operatore_creazione, data_creazione, " &
        "'" & Request.Cookies("SicilyRentCar")("IdUtente") & "', GetDate(), num_preventivo, num_prenotazione," &
        "giorni_noleggio_da_prenotazione, data_uscita_da_prenotazione, data_presunto_rientro_da_prenotazione, id_gruppo_da_prenotazione," &
        "id_stazione_drop_off_da_prenotazione, totale_costo_prenotazione, prenotazione_prepagata,  importo_prepagato, giorni_prepagati," &
        "id_gruppo_danni_uscita, id_gruppo_danni_rientro, NOTE_contratto, importo_a_carico_del_broker, importo_a_carico_del_broker_ribaltato, gg_a_carico_del_broker_ribaltato, id_fonte_commissionabile, tipo_commissione, commissione_percentuale, giorni_commissioni FROM contratti  AS contratti1 WITH(NOLOCK) WHERE id='" & idContratto.Text & "')"

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        Dim id_ctr_old As String = idContratto.Text

        sqlStr = "SELECT @@IDENTITY FROM contratti WITH(NOLOCK)"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        idContratto.Text = Cmd.ExecuteScalar

        sqlStr = "UPDATE contratti SET attivo='0' WHERE id='" & id_ctr_old & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        sqlStr = "INSERT INTO contratti_costi (id_documento, num_calcolo, ordine_stampa, id_gruppo, id_elemento, num_elemento, nome_costo, selezionato, omaggiato," &
            "franchigia_attiva,  valore_costo, valore_percentuale, imponibile, iva_imponibile, imponibile_scontato, iva_imponibile_scontato, imponibile_onere, iva_onere," &
            "imponibile_onere_broker_incluso, iva_onere_broker_incluso, aliquota_iva, iva_inclusa, scontabile, omaggiabile,  acquistabile_nolo_in_corso, id_a_carico_di, id_metodo_stampa, obbligatorio, id_unita_misura, qta, packed," &
            "data_aggiunta_nolo_in_corso, imponibile_scontato_prepagato, iva_imponibile_scontato_prepagato, imponibile_onere_prepagato, iva_onere_prepagato, sconto_su_imponibile_prepagato, " &
            "commissioni_imponibile, commissioni_iva, commissioni_imponibile_originale, commissioni_iva_originale) " &
        "(SELECT  '" & idContratto.Text & "', '" & numCalcolo.Text & "', ordine_stampa, id_gruppo, id_elemento, num_elemento, nome_costo, selezionato, omaggiato," &
        "franchigia_attiva,  valore_costo, valore_percentuale, imponibile, iva_imponibile, imponibile_scontato, iva_imponibile_scontato, imponibile_onere, iva_onere," &
        "imponibile_onere_broker_incluso, iva_onere_broker_incluso, aliquota_iva, iva_inclusa, scontabile, omaggiabile,  acquistabile_nolo_in_corso, id_a_carico_di, id_metodo_stampa, obbligatorio, id_unita_misura, qta, packed," &
        "data_aggiunta_nolo_in_corso, imponibile_scontato_prepagato, iva_imponibile_scontato_prepagato, imponibile_onere_prepagato, iva_onere_prepagato, sconto_su_imponibile_prepagato, " &
        "commissioni_imponibile, commissioni_iva, commissioni_imponibile_originale, commissioni_iva_originale " &
        "FROM contratti_costi As contratti_costi_1 WITH(NOLOCK) " &
        "WHERE id_documento='" & id_ctr_old & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "')"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        sqlStr = "INSERT INTO contratti_warning (id_documento, num_calcolo, warning, id_operatore, tipo) " &
        "(SELECT '" & idContratto.Text & "', '" & numCalcolo.Text & "',warning, id_operatore, tipo FROM contratti_warning As contratti_warning_1 WITH(NOLOCK) WHERE id_documento='" & id_ctr_old & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "')"
        '-------------------------------------------------------------------------------------------------------------------------------
        'REGISTRAZIONE DEL MOVIMENTO TARGA - SETTAGGIO DELL'AUTO COME NON DISPONIBILE ------------
        Dim data_uscita As String = getDataDb_con_orario(txtDaData.Text & " " & ore1.Text & ":" & minuti1.Text & ":00")

        sqlStr = "insert into movimenti_targa (num_riferimento, num_crv_contratto, id_veicolo, id_tipo_movimento, data_uscita, id_stazione_uscita, " &
        " km_uscita, serbatoio_uscita, id_operatore, data_registrazione, movimento_attivo) " &
        "VALUES" &
        " ('" & lblNumContratto.Text & "','" & numCrv.Text & "','" & id_auto_selezionata.Text & "','" & Costanti.idMovimentoNoleggio & "','" & data_uscita & "','" & CInt(dropStazionePickUp.SelectedValue) & "'," &
        "'" & txtKm.Text & "','" & txtSerbatoio.Text & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "',GetDate(),'1')"

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()
        'SETTAGGIO DEL VEICOLO COME NOLEGGIATO
        sqlStr = "UPDATE veicoli SET disponibile_nolo='0', noleggiata='1' WHERE id='" & id_auto_selezionata.Text & "'"

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()
        '--------------------------------------------------------------------------------------------------------------------------------

        listContrattiCosti.DataBind()
        listModifiche.DataBind()

        'CHECK OUT PER CREARE UN NUOVO SET DI DANNI DI USCITA ---------------------------------------------------------------------------
        gestione_checkin.InitFormCheckOut(tipo_documento.Contratto, Integer.Parse(lblNumContratto.Text), Integer.Parse(numCrv.Text))
        div_edit_danno.Visible = True
        tab_contratto.Visible = False
        '--------------------------------------------------------------------------------------------------------------------------------

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub btnAnnullaModificaTargaNoloInCorso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnullaModificaTargaNoloInCorso.Click
        'DEVO RICARICARE I DATI DELL'AUTO ATTUALMENTE SALVATA
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "SELECT id_veicolo, id_alimentazione, targa, km_uscita, litri_uscita, serbatoio_max, modello, alimentazione.descrizione As alimentazione, alimentazione.cod_carb " &
        "FROM contratti LEFT JOIN alimentazione WITH(NOLOCK) ON contratti.id_alimentazione=alimentazione.id WITH(NOLOCK) WHERE id='" & idContratto.Text & "'"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()
        Rs.Read()

        id_auto_selezionata.Text = Rs("id_veicolo")
        id_alimentazione.Text = Rs("id_alimentazione")
        txtTarga.Text = Rs("targa")
        txtKm.Text = Rs("km_uscita")
        txtSerbatoio.Text = Rs("litri_uscita")
        lblSerbatoioMax.Text = Rs("serbatoio_max")
        lblSerbatoioMaxRientro.Text = Rs("serbatoio_max")
        txtModello.Text = Rs("modello")
        lblTipoSerbatoio.Text = Rs("cod_carb") & ""
        lblTipoSerbatoio.ToolTip = Rs("alimentazione") & ""

        If gruppoDaConsegnare.SelectedValue <> "0" Then
            txtGruppo.Text = gruppoDaConsegnare.SelectedItem.Text
            id_gruppo_auto_selezionata.Text = gruppoDaConsegnare.SelectedValue
        Else
            txtGruppo.Text = gruppoDaCalcolare.SelectedItem.Text
            id_gruppo_auto_selezionata.Text = gruppoDaCalcolare.SelectedValue
        End If

        btnAnnullaModificaTargaNoloInCorso.Visible = False
        btnImmissioneInParco.Visible = False
        txtTarga.ReadOnly = True
        btnScegliTarga.Text = "Modifica"
        btnScegliTarga.Visible = True
        btnRicalcolaModificaContratto.Visible = True
        btnGeneraContratto.Visible = True
        bt_Check_Out.Visible = True
        btnPagamento.Visible = True
        btnQuickCheckIn.Visible = True


        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub btn_ScegliTarga()
        If statoContratto.Text = "5" And btnScegliTarga.Text = "Salva" Then
            'IN QUESTO CASO SIAMO NELLO STATO DI SELEZIONE AUTO SU CRV - L'UTENTE HA SCELTO DI SALVARE L'AUTO SELEZIONATA
            memorizza_veicolo_crv()
        ElseIf Trim(txtTarga.Text) <> "" Then
            riga_vedi_auto.Visible = False
            rifornimento.Visible = False
            pulizia.Visible = False

            id_rifornimento.Text = ""
            in_rifornimento.Text = ""
            id_pulizia.Text = ""
            in_pulizia.Text = ""
            id_scegli_auto_gruppo.Text = "0"
            id_scegli_auto_stazione.Text = "0"
            scegli_targa()
        Else
            Libreria.genUserMsgBox(Me, "Specificare la targa del veicolo da selzionare")
        End If
    End Sub

    Protected Sub btnScegliTarga_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScegliTarga.Click
        btn_ScegliTarga()
    End Sub

    Protected Sub registra_uscita_veicolo()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim data_uscita As String = getDataDb_con_orario(txtDaData.Text & " " & ore1.Text & ":" & minuti1.Text & ":00")
        Dim data_rientro As String = getDataDb_con_orario(txtAData.Text & " " & ore2.Text & ":" & minuti2.Text & ":00")

        Dim sqlStr As String = "insert into movimenti_targa (num_riferimento, num_crv_contratto, id_veicolo, id_tipo_movimento, data_uscita, id_stazione_uscita, " &
        " km_uscita, serbatoio_uscita, id_stazione_presunto_rientro, data_presunto_rientro, id_operatore, data_registrazione, movimento_attivo) " &
        "VALUES" &
        " ('" & lblNumContratto.Text & "','" & numCrv.Text & "','" & id_auto_selezionata.Text & "','" & Costanti.idMovimentoNoleggio & "','" & data_uscita & "','" & CInt(dropStazionePickUp.SelectedValue) & "'," &
        "'" & txtKm.Text & "','" & txtSerbatoio.Text & "','" & CInt(dropStazioneDropOff.SelectedValue) & "','" & data_rientro & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "',GetDate(),'1')"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)


        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()
        'SETTAGGIO DEL VEICOLO COME NOLEGGIATO
        sqlStr = "UPDATE veicoli SET disponibile_nolo='0', noleggiata='1' WHERE id='" & id_auto_selezionata.Text & "'"

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()


        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Function veicolo_disponibile_nolo(ByVal id_auto As String, ByVal km_attuali As String, ByVal serbatoio_attuale As String, ByVal stazione_attuale As String) As Boolean
        'IL CHECK OUT DEL VEICOLO VIENE EFFETTUATO SUL PRIMO CLICK DELLA STAMPA CONTRATTO. VISTO CHE PUO' PASSARE TEMPO TRA IL SALVATAGGIO DEL
        'CONTRATTO (EFFETTUATO CLICCANDO SU "PAGAMENTO") E L'EFFETTIVA USCITA DEL VEICOLO, E' NECESSARIO CONTROLLARE CHE L'AUTO COLLEGATA
        'SIA EFFETTIVAMENTE ANCORA DISPONIBILE AL NOLO - CHE NON SIA STATA USATA NEL FRATTEMPO, E QUINDI SERBATOIO E/O KM SONO VARIATI

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "SELECT TOP 1 id FROM veicoli WHERE id='" & id_auto & "' AND km_attuali='" & km_attuali & "' AND serbatoio_attuale='" & serbatoio_attuale & "' AND id_stazione='" & stazione_attuale & "'"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test <> "" Then
            veicolo_disponibile_nolo = True
        Else
            veicolo_disponibile_nolo = False
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function cauzione_versata() As Boolean
        'PER POTER APRIRE UN CONTRATTO NORMALE E' NECESSARIO CHE IL CLIENTE ABBIA VERSATO UNA CAUZIONE. ATTUALMENTE SI CONTROLLA
        '- Preautorizzazione (non chiusa - non stornata)
        '- Deposito CASH/ASSEGNO su RA
        '- Preautorizzazione Telefonica
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "SELECT TOP 1 Nr_Contratto FROM PAGAMENTI_EXTRA WHERE " &
        "((id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Richiesta & "' AND preaut_aperta='1' AND operazione_stornata='0') OR " &
        "(id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Autorizzazione & "') OR " &
        "(id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Deposito_su_RA & "')) AND N_CONTRATTO_RIF='" & lblNumContratto.Text & "'"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test <> "" Then
            cauzione_versata = True
        Else
            cauzione_versata = False
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub registra_uscita_gps()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        'IMPOSTO IL GPS COME NOLEGGIATO
        Dim sqlStr As String = "UPDATE gps SET id_gps_status='" & Costanti.stato_gps.in_nolo & "' WHERE id='" & CInt(lblIdGps.Text) & "'"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        Dim data_pr_rientro As String = getDataDb_con_orario(txtAData.Text & " " & ore2.Text & ":" & minuti2.Text & ":00")

        'CREO LA RIGA DI MOVIMENTO IN movimenti_targa
        sqlStr = "INSERT INTO movimenti_targa (num_riferimento, id_gps, id_tipo_movimento, data_uscita, id_stazione_uscita, data_presunto_rientro, id_stazione_presunto_rientro," &
            " id_operatore, data_registrazione, movimento_attivo) " &
            " VALUES " &
            "('" & lblNumContratto.Text & "','" & lblIdGps.Text & "','" & Costanti.tipologia_movimenti.noleggio & "', getDate(),'" & CInt(dropStazionePickUp.SelectedValue) & "'," &
            "'" & data_pr_rientro & "','" & dropStazioneDropOff.SelectedValue & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "', getDate(),'1')"

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub registra_rientro_gps()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        'IMPOSTO IL GPS COME NOLEGGIATO
        Dim sqlStr As String = "UPDATE gps SET id_gps_status='" & Costanti.stato_gps.in_parco & "', id_stazione_attuale='" & dropStazionePickUp.SelectedValue & "' " &
            "WHERE id='" & CInt(lblIdGps.Text) & "'"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        Dim data_rientro As String = getDataDb_con_orario(txtAData.Text & " " & ore2.Text & ":" & minuti2.Text & ":00")

        'CREO LA RIGA DI MOVIMENTO IN movimenti_targa
        sqlStr = "UPDATE movimenti_targa SET data_rientro='" & data_rientro & "', id_stazione_rientro='" & dropStazionePickUp.SelectedValue & "', movimento_attivo='0'," &
            "id_operatore_rientro='" & Request.Cookies("SicilyRentCar")("IdUtente") & "', data_registrazione_rientro=getDate() " &
            "WHERE id_gps='" & lblIdGps.Text & "' AND movimento_attivo='1'"

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub btnGeneraContratto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGeneraContratto.Click
        Dim stato_contratto As String = getStatoContratto()

        If stato_contratto = "1" Then
            If Not (cauzione_versata() Or full_credit.Text = "1") Then
                Libreria.genUserMsgBox(Me, "Attenzione: è necessario incassare una cauzione per poter aprire il contratto.")
            ElseIf Not veicolo_disponibile_nolo(id_auto_selezionata.Text, txtKm.Text, txtSerbatoio.Text, CInt(dropStazionePickUp.SelectedValue)) Then
                Libreria.genUserMsgBox(Me, "Attenzione: il veicolo non è più disponibile per il noleggio oppure serbatoio e/o km variati rispetto a quanto mostrato attualmente. Cliccare su 'Modifica' per cambiare la targa associata al contratto o per selezionare nuovamente la stessa.")
            ElseIf div_gps.Visible And lblIdGps.Text = "" Then
                'CONTROLLO SE' E' STATO SELEZIONATO IL GPS (QUALORA L'ACCESSORIO SIA STATO SCELTO - IN QUESTO CASO IL div_gps RISULTA VISIBILE)
                Libreria.genUserMsgBox(Me, "E' necessario selezionare un GPS prima di procedere.")
            ElseIf div_gps.Visible AndAlso Not GpsDisponibile() Then
                'DOPO AVERO COLLEGATO IL GPS AD UN CONTRATTO (PRIMA DEL SALVATAGGIO) CONTROLLO SE E', O MENO, ANCORA DISPONIBILE
                Libreria.genUserMsgBox(Me, "Attenzione: il GPS selezionato non è più disponibile. Selezionarne un'altro oppure rimuovere l'accessorio.")
            Else
                'IN OCCASIONE DELLA PRIMA STAMPA DEL CONTRATTO EFFETTUO L'USICTA DEL VEICOLO RICALCOLANDO I COSTI 
                'CONTROLLO SE VI SONO VINCOLI DI MINIMO/MASSIMO GIORNI DI NOLEGGIO DA RISPETTARE - SE NON SONO RISPETATI NON POSSO EFFETTUARE L'USCITA
                Dim id_tariffe_righe As String

                If dropTariffeGeneriche.SelectedValue <> "0" Then
                    id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
                ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                    id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
                End If

                Dim numero_giorni As Integer = funzioni_comuni.getGiorniDiNoleggio(Format(Now(), "dd/MM/yyyy"), txtAData.Text, Hour(Now()), Minute(Now()), ore2.Text, minuti2.Text, id_tariffe_righe)

                Dim min_giorni_nolo As String = funzioni_comuni.getMinGiorniNolo(id_tariffe_righe)
                Dim max_giorni_nolo As String = funzioni_comuni.getMaxGiorniNolo(id_tariffe_righe)

                If (min_giorni_nolo = "-1" And max_giorni_nolo = "-1") Or (min_giorni_nolo = "-1" And CInt(numero_giorni) <= CInt(max_giorni_nolo) And max_giorni_nolo <> "-1") Or (CInt(min_giorni_nolo) <= CInt(numero_giorni) And max_giorni_nolo = "-1" And min_giorni_nolo <> "-1") Or (CInt(min_giorni_nolo) <= CInt(numero_giorni) And CInt(max_giorni_nolo) >= CInt(numero_giorni) And min_giorni_nolo <> "-1" And max_giorni_nolo <> "-1") Then
                    ricalcola_uscita_veicolo()

                    stato_contratto_in_corso()
                    listContrattiCosti.DataBind()
                    bt_Check_Out.Visible = True
                    btnCRV.Visible = True
                    btnVoid.Visible = False
                    gestione_checkin.InitFormCheckOut(tipo_documento.Contratto, Integer.Parse(lblNumContratto.Text), Integer.Parse(numCrv.Text))
                    div_edit_danno.Visible = True
                    tab_contratto.Visible = False

                    If lblIdGps.Text <> "" Then
                        'SE E' STATO SELEZIONATO UN GPS REGISTRO LA SUA USCITA DAL PARCO ED IL RELATIVO MOVIMENTO
                        registra_uscita_gps()
                    End If

                    stampa_contratto("uscita")
                Else
                    Dim msg As String = "Attenzione: i giorni di noleggio sono " & numero_giorni & "; la tariffa scelta prevede"
                    If min_giorni_nolo <> "-1" Then
                        msg = msg & " minimo " & min_giorni_nolo & " giorni/o di nolo - "
                    End If
                    If max_giorni_nolo <> "-1" Then
                        msg = msg & " massimo " & max_giorni_nolo & " giorni/o di nolo "
                    End If

                    Libreria.genUserMsgBox(Page, msg)

                    btnSalvaModifiche.Enabled = False
                End If
            End If


        ElseIf (statoContratto.Text = "0" And stato_contratto = "" And full_credit.Text = "1") Then
            'SIAMO NEL CASO FULL CREDIT, L'UTENTE STA EFFETTUANDO IN CHECK OUT DEL VEICOLO SENZA AVER CLICCATO SU PAGAMENTO

            'CONTROLLO CHE, NEL CASO IN CUI L'UTENTE PROVENGA DA PREVENTIVI O PRENOTAZIONI CON LA SECONDA GUIDA SELEZIONATA, SIA STATO
            'SELEZIONATO IL SECONDO GUIDATORE DA ANAGRAFIA
            If Not (secondo_guidatore_a_scelta_selezionato() And idSecondoConducente.Text = "") Then
                'CONTROLLO SE VI SONO VINCOLI DI MINIMO/MASSIMO GIORNI DI NOLEGGIO DA RISPETTARE - SE NON SONO RISPETATI NON POSSO EFFETTUARE L'USCITA
                Dim id_tariffe_righe As String

                If dropTariffeGeneriche.SelectedValue <> "0" Then
                    id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
                ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                    id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
                End If

                Dim numero_giorni As Integer = funzioni_comuni.getGiorniDiNoleggio(Format(Now(), "dd/MM/yyyy"), txtAData.Text, Hour(Now()), Minute(Now()), ore2.Text, minuti2.Text, id_tariffe_righe)

                Dim min_giorni_nolo As String = funzioni_comuni.getMinGiorniNolo(id_tariffe_righe)
                Dim max_giorni_nolo As String = funzioni_comuni.getMaxGiorniNolo(id_tariffe_righe)
                If (min_giorni_nolo = "-1" And max_giorni_nolo = "-1") Or (min_giorni_nolo = "-1" And CInt(numero_giorni) <= CInt(max_giorni_nolo) And max_giorni_nolo <> "-1") Or (CInt(min_giorni_nolo) <= CInt(numero_giorni) And max_giorni_nolo = "-1" And min_giorni_nolo <> "-1") Or (CInt(min_giorni_nolo) <= CInt(numero_giorni) And CInt(max_giorni_nolo) >= CInt(numero_giorni) And min_giorni_nolo <> "-1" And max_giorni_nolo <> "-1") Then
                    'PAGAMENTO PER UN CONTRATTO DA SALVARE (USCITA VEICOLO) - PRIMA SALVO IL CONTRATTO (GENERANDONE IL NUMERO) E POI PROCEDO CON LA 
                    'PREAUTORIZZAZIONE
                    salva_contratto()

                    btnRicalcolaDaPrenotazione.Visible = False
                    btnRicalcolaDaPreventivo.Visible = False
                    btnGeneraContratto.Visible = True

                    btnAnnullaDocumento.Text = "Chiudi"

                    ricalcola_uscita_veicolo()

                    stato_contratto_in_corso()
                    listContrattiCosti.DataBind()
                    bt_Check_Out.Visible = True
                    btnCRV.Visible = True
                    gestione_checkin.InitFormCheckOut(tipo_documento.Contratto, Integer.Parse(lblNumContratto.Text), Integer.Parse(numCrv.Text))
                    div_edit_danno.Visible = True
                    tab_contratto.Visible = False

                    stampa_contratto("uscita")
                Else
                    Dim msg As String = "Attenzione: i giorni di noleggio sono " & numero_giorni & "; la tariffa scelta prevede"
                    If min_giorni_nolo <> "-1" Then
                        msg = msg & " minimo " & min_giorni_nolo & " giorni/o di nolo - "
                    End If
                    If max_giorni_nolo <> "-1" Then
                        msg = msg & " massimo " & max_giorni_nolo & " giorni/o di nolo "
                    End If

                    Libreria.genUserMsgBox(Page, msg)

                    btnSalvaModifiche.Enabled = False
                End If
            Else
                Libreria.genUserMsgBox(Me, "Attenzione: selezionare il secondo guidatore da anagrafica oppure eliminare l'accessorio 'Secondo guidatore'")
            End If
        Else
            stampa_contratto("uscita")
        End If
    End Sub

    Protected Sub stampa_fattura(ByVal id_fattura As String)
        Session("DatiStampaFatturaNolo") = StampaFatturaNolo.genera_dati_stampa_fattura(id_fattura)

        Dim Generator As System.Random = New System.Random()

        Dim num_random As String = Format(Generator.Next(100000000), "000000000")


        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraFatturaNolo.aspx?a=" & num_random & "','')", True)
            End If
        End If


    End Sub

    Protected Sub stampa_contratto(ByVal tipo As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
        Dim Rs As Data.SqlClient.SqlDataReader

        Dim id_staz_rientro As String
        Dim data_rientro As String
        Dim orario_rientro As String
        Dim numero_giorni As String

        Dim idContratto_uscita As String
        Dim numCalcolo_uscita As String
        Dim idContratto_temp As String
        Dim numCalcolo_temp As String


        If statoContratto.Text = "1" Or statoContratto.Text = "2" Or statoContratto.Text = "7" Or tipo = "rientro" Then
            id_staz_rientro = dropStazioneDropOff.SelectedValue
            data_rientro = txtAData.Text
            orario_rientro = txtOraRientro.Text
            numero_giorni = txtNumeroGiorni.Text
        Else
            'NEL CASO DI CONTRATTO NON PIU' APERTO CON RICHIESTA DI STAMPA DI CONTRATTO DI USCITA CON QUESTA FUNZIONALITA' VERRA' 
            'STAMPATO IL CONTRATTO DI USCITA, CORRISPONDENTE ALL'ULTIMA RIGA SALVATA PRIMA DI EFFETTUARE IL QUICK CHECK IN 
            '(IN QUESTA SEZIONE PRELEVO I DATI DAL DB PER TUTTI QUEGLI ELEMENTI CHE POSSONO VARIARE DURANTE IL CONTRATTO IN CORSO
            Cmd = New Data.SqlClient.SqlCommand("SELECT TOP 1 * FROM contratti WITH(NOLOCK) WHERE num_contratto='" & lblNumContratto.Text & "' AND status='2'  ORDER BY num_calcolo DESC", Dbc)
            Rs = Cmd.ExecuteReader()
            Rs.Read()

            id_staz_rientro = Rs("id_stazione_presunto_rientro")
            data_rientro = Day(Rs("data_presunto_rientro")) & "/" & Month(Rs("data_presunto_rientro")) & "/" & Year(Rs("data_presunto_rientro"))

            If Len(Hour(Rs("data_presunto_rientro"))) = 1 Then
                orario_rientro = "0" & Hour(Rs("data_presunto_rientro")) & ":"
            Else
                orario_rientro = Hour(Rs("data_presunto_rientro")) & ":"
            End If

            If Len(Minute(Rs("data_presunto_rientro"))) = 1 Then
                orario_rientro = orario_rientro & "0" & Minute(Rs("data_presunto_rientro"))
            Else
                orario_rientro = orario_rientro & Minute(Rs("data_presunto_rientro"))
            End If

            numero_giorni = Rs("giorni")
            idContratto_uscita = Rs("id")
            numCalcolo_uscita = Rs("num_calcolo")

            Rs.Close()
            Rs = Nothing
            Dbc.Close()
            Dbc.Open()
        End If

        Dim miei_dati As DatiStampaContrattoNew = New DatiStampaContrattoNew
        With miei_dati
            'STAZIONE DI USCITA ----------------------------------------------------------------------------------------------------------
            Cmd = New Data.SqlClient.SqlCommand("SELECT NOME_STAZIONE, codice, telefono, indirizzo, citta FROM stazioni WITH(NOLOCK) WHERE id='" & dropStazionePickUp.Text & "'", Dbc)
            Rs = Cmd.ExecuteReader()
            Rs.Read()

            .uscita = Rs("codice") & " - " & Rs("nome_stazione")
            If (Rs("telefono") & "") <> "" Then
                .uscita = .uscita & " - " & Rs("telefono")
            End If
            .uscita = .uscita & vbCrLf & Rs("indirizzo") & " - " & Rs("citta") &
            vbCrLf & txtDaData.Text & " " & txtoraPartenza.Text
            '------------------------------------------------------------------------------------------------------------------------------
            'STAZIONE DI RIENTRO ----------------------------------------------------------------------------------------------------------
            Dbc.Close()
            Dbc.Open()
            Rs.Close()

            Rs = Nothing
            Cmd = New Data.SqlClient.SqlCommand("SELECT NOME_STAZIONE, codice, telefono, indirizzo, citta FROM stazioni WITH(NOLOCK) WHERE id='" & id_staz_rientro & "'", Dbc)
            Rs = Cmd.ExecuteReader()
            Rs.Read()

            .rientro_previsto = Rs("codice") & " - " & Rs("nome_stazione")
            If (Rs("telefono") & "") <> "" Then
                .rientro_previsto = .rientro_previsto & " - " & Rs("telefono")
            End If
            .rientro_previsto = .rientro_previsto & vbCrLf & Rs("indirizzo") & " - " & Rs("citta") &
            vbCrLf & data_rientro & " " & orario_rientro
            '------------------------------------------------------------------------------------------------------------------------------
            'VEICOLO ----------------------------------------------------------------------------------------------------------------------
            .veicolo = txtModello.Text & vbCrLf & txtTarga.Text & vbCrLf
            If gruppoDaConsegnare.SelectedValue = 0 Then
                .veicolo = .veicolo & "Gruppo " & gruppoDaCalcolare.SelectedItem.Text & " - Gruppo applicato " & gruppoDaCalcolare.SelectedItem.Text & vbCrLf
            Else
                .veicolo = .veicolo & "Gruppo " & gruppoDaConsegnare.SelectedItem.Text & " - Gruppo applicato " & gruppoDaCalcolare.SelectedItem.Text & vbCrLf
            End If
            .veicolo = .veicolo & "Km Out: " & txtKm.Text & " - Lt Out: " & txtSerbatoio.Text & "/" & lblSerbatoioMax.Text

            If tipo = "rientro" And (statoContratto.Text = "4" Or statoContratto.Text = "8" Or statoContratto.Text = "6") Then
                .veicolo = .veicolo & vbCrLf &
                "Km In: " & txtKmRientro.Text & " - Lt In: " & txtSerbatoioRientro.Text & "/" & lblSerbatoioMaxRientro.Text
            ElseIf tipo = "rientro" And statoContratto.Text = "3" Then
                .veicolo = .veicolo & vbCrLf &
                "Km e Lt di rientro non disponibili: il cliente non ha voluto attendere il completamento " & vbCrLf & " della procedura di chiusura contratto."
            End If

            If tipo = "rientro" Then
                .rientro = "RIENTRO"
            Else
                .rientro = "PREVISTO RIENTRO"
            End If

            '------------------------------------------------------------------------------------------------------------------------------
            'DATI FATTURAZIONE ------------------------------------------------------------------------------------------------------------
            Dbc.Close()
            Dbc.Open()
            Rs.Close()
            Rs = Nothing
            Cmd = New Data.SqlClient.SqlCommand("SELECT rag_soc, tel, indirizzo, ditte.cap, comuni_ares.comune As comune_ares, citta, email, Piva, c_fis, nazioni.nazione FROM ditte WITH(NOLOCK) LEFT JOIN comuni_ares WITH(NOLOCK) ON ditte.id_comune_ares=comuni_ares.id LEFT JOIN nazioni WITH(NOLOCK) ON ditte.nazione=nazioni.ID_NAZIONE WHERE id_ditta='" & idDitta.Text & "'", Dbc)
            Rs = Cmd.ExecuteReader()
            Rs.Read()

            .fatturare_a = Rs("rag_soc")
            If (Rs("tel") & "") <> "" Then
                .fatturare_a = .fatturare_a & " - " & Rs("tel")
            End If
            .fatturare_a = .fatturare_a & vbCrLf & Rs("indirizzo") & vbCrLf & Rs("cap") & " "

            If (Rs("comune_ares") & "") <> "" Then
                .fatturare_a = .fatturare_a & Rs("comune_ares")
            Else
                .fatturare_a = .fatturare_a & Rs("citta")
            End If

            If (Rs("email") & "") <> "" Then
                .fatturare_a = .fatturare_a & vbCrLf & Rs("email")
            End If

            .fatturare_a = .fatturare_a & vbCrLf & Rs("Piva") & " (P.iva)"

            If (Rs("c_fis") & "") <> "" Then
                .fatturare_a = .fatturare_a & vbCrLf & Rs("c_fis")
            End If

            .fatturare_a = .fatturare_a & vbCrLf & Rs("nazione") & ""
            '------------------------------------------------------------------------------------------------------------------------------
            'PRIMO CONDUCENTE -------------------------------------------------------------------------------------------------------------
            Dbc.Close()
            Dbc.Open()
            Rs.Close()
            Rs = Nothing
            Cmd = New Data.SqlClient.SqlCommand("SELECT cognome, nome, telefono, CONVERT(char(10), data_nascita, 103) As data_nascita, luogo_nascita, indirizzo, cap, comuni_ares.comune As comune_ares, city, email, patente, CONVERT(char(10), scadenza_patente, 103) As scadenza_patente, CODFIS, nazioni.nazione FROM conducenti WITH(NOLOCK) LEFT JOIN comuni_ares WITH(NOLOCK) ON conducenti.id_comune_ares=comuni_ares.id LEFT JOIN nazioni WITH(NOLOCK) ON conducenti.nazione=nazioni.ID_NAZIONE WHERE id_conducente='" & idPrimoConducente.Text & "'", Dbc)
            Rs = Cmd.ExecuteReader()
            Rs.Read()

            .conducente1 = Rs("nome") & " " & Rs("cognome")
            If (Rs("telefono") & "") <> "" Then
                .conducente1 = .conducente1 & " " & Rs("telefono")
            End If
            .conducente1 = .conducente1 & vbCrLf & Rs("data_nascita") & " " & Rs("luogo_nascita") & vbCrLf & Rs("indirizzo") & vbCrLf & Rs("cap") & " "

            If (Rs("comune_ares") & "") <> "" Then
                .conducente1 = .conducente1 & Rs("comune_ares")
            Else
                .conducente1 = .conducente1 & Rs("city")
            End If

            If (Rs("email") & "") <> "" Then
                .conducente1 = .conducente1 & vbCrLf & Rs("email")
            End If

            .conducente1 = .conducente1 & vbCrLf & Rs("patente") & " " & Rs("scadenza_patente") &
            vbCrLf & Rs("codfis") & vbCrLf & Rs("nazione")
            '------------------------------------------------------------------------------------------------------------------------------
            'SECONDO CONDUCENTE -----------------------------------------------------------------------------------------------------------
            If idSecondoConducente.Text <> "" Then
                Dbc.Close()
                Dbc.Open()
                Rs.Close()
                Rs = Nothing
                Cmd = New Data.SqlClient.SqlCommand("SELECT cognome, nome, telefono, CONVERT(char(10), data_nascita, 103) As data_nascita, luogo_nascita, indirizzo, cap, comuni_ares.comune As comune_ares, city, email, patente, CONVERT(char(10), scadenza_patente, 103) As scadenza_patente, CODFIS, nazioni.nazione FROM conducenti WITH(NOLOCK) LEFT JOIN comuni_ares WITH(NOLOCK) ON conducenti.id_comune_ares=comuni_ares.id LEFT JOIN nazioni WITH(NOLOCK) ON conducenti.nazione=nazioni.ID_NAZIONE WHERE id_conducente='" & idSecondoConducente.Text & "'", Dbc)
                Rs = Cmd.ExecuteReader()
                Rs.Read()

                .conducente2 = Rs("nome") & " " & Rs("cognome")
                If (Rs("telefono") & "") <> "" Then
                    .conducente2 = .conducente2 & " " & Rs("telefono")
                End If
                .conducente2 = .conducente2 & vbCrLf & Rs("data_nascita") & " " & Rs("luogo_nascita") & vbCrLf & Rs("indirizzo") & vbCrLf & Rs("cap") & " "

                If (Rs("comune_ares") & "") <> "" Then
                    .conducente2 = .conducente2 & Rs("comune_ares")
                Else
                    .conducente2 = .conducente2 & Rs("city")
                End If

                If (Rs("email") & "") <> "" Then
                    .conducente2 = .conducente2 & vbCrLf & Rs("email")
                End If

                .conducente2 = .conducente2 & vbCrLf & Rs("patente") & " " & Rs("scadenza_patente") &
                vbCrLf & Rs("codfis") & vbCrLf & Rs("nazione")
            End If
            '------------------------------------------------------------------------------------------------------------------------------
            'NUMERO GIORNI ----------------------------------------------------------------------------------------------------------------
            .voucher_gg = numero_giorni
            '------------------------------------------------------------------------------------------------------------------------------
            'VOUCHER ----------------------------------------------------------------------------------------------------------------------

            If tariffa_broker.Text = "1" Then
                .voucher_n = txtRiferimentoTO.Text
                .voucher = dropTipoCliente.SelectedItem.Text
            Else
                .voucher_n = ""
                .voucher = ""
            End If

            '------------------------------------------------------------------------------------------------------------------------------
            'NUMERO DI PRENOTAZIONE -------------------------------------------------------------------------------------------------------
            If idPrenotazione.Text <> "" Then
                .resn = lblNumPren.Text
            End If
            '------------------------------------------------------------------------------------------------------------------------------
            'NUMERO RA --------------------------------------------------------------------------------------------------------------------
            .ran = lblNumContratto.Text
            '------------------------------------------------------------------------------------------------------------------------------
            'DETTAGLI COSTI ---------------------------------------------------------------------------------------------------------------
            'SE IN FASE DI CONTRATTO CHIUSO VIENE RICHIESTA LA STAMPA DEL CONTRATTO DI USCITA RICARICO I COSTI PER POTERLI STAMPARE
            If statoContratto.Text <> "1" And statoContratto.Text <> "2" And statoContratto.Text <> "7" And tipo = "uscita" Then
                idContratto_temp = idContratto.Text
                numCalcolo_temp = numCalcolo.Text

                idContratto.Text = idContratto_uscita
                numCalcolo.Text = numCalcolo_uscita

                listContrattiCosti.DataBind()
            End If

            'RECUPERO LE PENALITA' COLLEGATE AGLI ACCESSORI
            Dim penalita_collegate(100) As String
            Dim accessori_acquistati As New Collection

            Dim j As Integer
            j = 0

            Dbc.Close()
            Dbc.Open()
            Rs.Close()
            Rs = Nothing
            Cmd = New Data.SqlClient.SqlCommand("SELECT id, id_elemento_da_addebitare_se_perso FROM condizioni_elementi WITH(NOLOCK) WHERE NOT id_elemento_da_addebitare_se_perso IS NULL", Dbc)

            Rs = Cmd.ExecuteReader()

            Do While Rs.Read()
                j = j + 1
                penalita_collegate(j) = Rs("id_elemento_da_addebitare_se_perso") & "-" & Rs("id")

                'Response.Write(" 0 " & Rs("id_elemento_da_addebitare_se_perso") & "-" & Rs("id"))
            Loop

            Dim id_a_carico_di As Label
            Dim nome_costo As Label
            Dim costo_scontato As Label
            Dim costo_scontato_inv As Label
            Dim obbligatorio As Label
            Dim costo_unitario As Label
            Dim valore_costo As Label
            Dim qta As Label
            Dim id_metodo_stampa As Label
            Dim tipologia_franchigia As Label
            Dim omaggiato As CheckBox
            Dim chkScegli As CheckBox
            Dim id_perso As Label
            Dim id_elemento As Label
            Dim id_penalita_da_addebitare As Label
            Dim totale_x_complimentary As Double = 0

            Dim penali As String = ""

            Dim assicurazione_acquistata As Boolean = False

            For i = 0 To listContrattiCosti.Items.Count - 1
                id_a_carico_di = listContrattiCosti.Items(i).FindControl("id_a_carico_di")
                nome_costo = listContrattiCosti.Items(i).FindControl("nome_costo")
                costo_scontato = listContrattiCosti.Items(i).FindControl("costo_scontato")
                costo_scontato_inv = listContrattiCosti.Items(i).FindControl("costo_scontato_inv")
                obbligatorio = listContrattiCosti.Items(i).FindControl("obbligatorio")
                chkScegli = listContrattiCosti.Items(i).FindControl("chkScegli")
                costo_unitario = listContrattiCosti.Items(i).FindControl("lbl_costo_unitario")
                valore_costo = listContrattiCosti.Items(i).FindControl("valore_costoLabel")
                qta = listContrattiCosti.Items(i).FindControl("qta")
                id_metodo_stampa = listContrattiCosti.Items(i).FindControl("id_metodo_stampa")
                omaggiato = listContrattiCosti.Items(i).FindControl("chkOldOmaggio")
                costo_scontato.Text = costo_scontato.Text.Replace(" €", "")
                tipologia_franchigia = listContrattiCosti.Items(i).FindControl("tipologia_franchigia")
                id_perso = listContrattiCosti.Items(i).FindControl("id_addebitoLabel")
                id_elemento = listContrattiCosti.Items(i).FindControl("id_elemento")
                id_penalita_da_addebitare = listContrattiCosti.Items(i).FindControl("id_penalita_da_addebitare")


                If id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore Then
                    If id_a_carico_di.Text = Costanti.id_accessorio_incluso And LCase(nome_costo.Text) = "valore tariffa" And (tariffa_broker.Text = "0" Or (tariffa_broker.Text = "1" And costo_scontato.Visible)) Then
                        'VALORE TARIFFA: unico accessorio incluso con costo visibile; inoltre è il primo elemento - NON NEL CASO DI BROKER a meno che il costo scontato sia visibile e quindi corrisponda alla differenza da pagare al banco

                        '  If id_condizione_elemento.Text <> "" Then
                        '  insieme_id_elementi(i) = id_condizione_elemento.Text
                        'End If
                        .vari = nome_costo.Text

                        If txtSconto.Text <> "0" Then
                            .sconto = txtSconto.Text & " %"
                        Else
                            .sconto = ""
                        End If
                        If tariffa_broker.Text = "0" Then
                            .costo = valore_costo.Text
                        End If
                        .qta = qta.Text
                        .costounitarioscontato = costo_unitario.Text
                        If complimentary.Text = "1" Then
                            .totale = costo_scontato_inv.Text

                            totale_x_complimentary = totale_x_complimentary + CDbl(costo_scontato_inv.Text)
                        Else
                            .totale = costo_scontato.Text
                        End If
                    ElseIf LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
                        'TOTALE - E' L'ULTIMO COSTO PRIMA DELLE INFORMATIVE
                        .vari = .vari & vbCrLf & nome_costo.Text
                        .sconto = .sconto & vbCrLf
                        .costo = .costo & vbCrLf
                        .qta = .qta & vbCrLf
                        .costounitarioscontato = .costounitarioscontato & vbCrLf

                        If complimentary.Text = "1" Then
                            'NEL CASO DI COMPLIMENTARY IL TOTALE E' LA SOMMA DEI COSTI PRECEDENTI (CALCOLATA MANUALMENTE IN QUANTO IL TOTALE
                            'IN contratti_costi E' QUELLO CHE EFFETTIVAMENTE DEVE PAGARE IL CLIENTE) - INOLTRE AGGIUNGO LA RIGA 
                            'COMPLIMENTARY DOPO IL TOTALE CHE INDICA QUANTO EFFETTIVAMENTE VIENE SCONTATO
                            .totale = .totale & vbCrLf & totale_x_complimentary

                            'RIGA COMPLIMENTARY
                            .vari = .vari & vbCrLf & "COMPLIMENTARY"
                            .sconto = .sconto & vbCrLf
                            .costo = .costo & vbCrLf
                            .qta = .qta & vbCrLf
                            .costounitarioscontato = .costounitarioscontato & vbCrLf
                            .totale = .totale & vbCrLf & FormatNumber(totale_x_complimentary - CDbl(costo_scontato.Text), 2, , , TriState.False)
                        Else
                            .totale = .totale & vbCrLf & costo_scontato.Text
                        End If
                    ElseIf id_a_carico_di.Text = Costanti.id_accessorio_incluso And LCase(nome_costo.Text) <> "valore tariffa" Then
                        'INCLUSI (SENZA COSTO)
                        .vari = .vari & vbCrLf & nome_costo.Text
                        .sconto = .sconto & vbCrLf
                        .costo = .costo & vbCrLf
                        .qta = .qta & vbCrLf
                        .costounitarioscontato = .costounitarioscontato & vbCrLf
                        .totale = .totale & vbCrLf
                    ElseIf id_a_carico_di.Text <> Costanti.id_accessorio_incluso And obbligatorio.Text = "True" And nome_costo.Text <> Costanti.testo_elemento_totale And LCase(nome_costo.Text) <> "valore tariffa" Then
                        'OBBLIGATORI (CON COSTO)
                        .vari = .vari & vbCrLf & nome_costo.Text

                        If Not omaggiato.Checked Or complimentary.Text = "1" Then
                            'SE L'ACCESSORIO NON E' STATO OMAGGIATO
                            If txtSconto.Text <> "0" Then
                                .sconto = .sconto & vbCrLf & txtSconto.Text & " %"
                            Else
                                .sconto = .sconto & vbCrLf
                            End If
                            .costo = .costo & vbCrLf & valore_costo.Text
                            .qta = .qta & vbCrLf & qta.Text
                            .costounitarioscontato = .costounitarioscontato & vbCrLf & costo_unitario.Text

                            If complimentary.Text = "1" Then
                                .totale = .totale & vbCrLf & costo_scontato_inv.Text
                                totale_x_complimentary = totale_x_complimentary + CDbl(costo_scontato_inv.Text)
                            Else
                                .totale = .totale & vbCrLf & costo_scontato.Text
                            End If
                        Else
                            'OMAGGIATO
                            .costo = .costo & vbCrLf & "0,00"
                            .sconto = .sconto & vbCrLf
                            .qta = .qta & vbCrLf & "1"
                            .costounitarioscontato = .costounitarioscontato & vbCrLf & "0"

                            .totale = .totale & vbCrLf & costo_scontato.Text
                        End If
                        'ElseIf chkScegli.Checked And chkScegli.Visible Then
                    ElseIf chkScegli.Checked And LCase(nome_costo.Text) <> "valore tariffa" Then
                        'Response.Write(" 1 " & id_elemento.Text)
                        accessori_acquistati.Add(id_elemento.Text, id_elemento.Text)

                        .vari = .vari & vbCrLf & nome_costo.Text

                        If tipologia_franchigia.Text = "ASSICURAZIONE" Then
                            assicurazione_acquistata = True
                        End If

                        If Not omaggiato.Checked Or complimentary.Text = "1" Then
                            'SE L'ACCESSORIO NON E' STATO OMAGGIATO
                            If txtSconto.Text <> "0" Then
                                .sconto = .sconto & vbCrLf & txtSconto.Text & " %"
                            Else
                                .sconto = .sconto & vbCrLf
                            End If
                            .costo = .costo & vbCrLf & valore_costo.Text
                            .qta = .qta & vbCrLf & qta.Text
                            .costounitarioscontato = .costounitarioscontato & vbCrLf & costo_unitario.Text
                            If complimentary.Text = "1" Then
                                .totale = .totale & vbCrLf & costo_scontato_inv.Text
                                totale_x_complimentary = totale_x_complimentary + CDbl(costo_scontato_inv.Text)
                            Else
                                .totale = .totale & vbCrLf & costo_scontato.Text
                            End If
                        Else
                            'OMAGGIATO
                            .costo = .costo & vbCrLf & "0,00"
                            .sconto = .sconto & vbCrLf
                            .qta = .qta & vbCrLf & "1"
                            .costounitarioscontato = .costounitarioscontato & vbCrLf & "0"

                            .totale = .totale & vbCrLf & costo_scontato.Text
                        End If
                    End If
                Else
                    'INFORMATIVE MEMORIZZO IL TESTO PER STAMPARLO NELLA SEZIONE SPECIFICA - PER UN CONTRATTO CHIUSO INVECE LE INFORMATIVE 
                    'SELEZIONATE VANNO NELLA SEZIONE COSTI (NON MOSTRO LE EVENTUALI RIGHE NON VISIBILI)
                    Dim k As Integer
                    Dim elemento_trovato As Boolean = False
                    Dim accessorio_venduto As Boolean = False
                    Dim ele1 As String = ""
                    Dim ele2 As String = ""

                    For k = 1 To j
                        ele1 = penalita_collegate(k).Split("-")(0)
                        ele2 = penalita_collegate(k).Split("-")(1)

                        'Response.Write(" 2 " & ele1 & " - " & ele2)

                        If ele1 = id_elemento.Text Then
                            elemento_trovato = True
                            accessorio_venduto = accessori_acquistati.Contains(ele2)
                        End If
                    Next

                    If elemento_trovato = False Or (elemento_trovato And accessorio_venduto) Then
                        'STAMPO LA PENALE SE NON E' COLLEGATA AD UN ACCESSORIO OPPURE SE E' COLLEGATA AD ESSO E L'ACCESSORIO E' ACQUISTATO
                        penali = penali & UCase(nome_costo.Text) & " "

                        If id_metodo_stampa.Text = Costanti.id_stampa_informativa_con_valore Then
                            penali = penali & costo_scontato.Text & " - "
                        Else
                            penali = penali & "- "
                        End If
                    End If

                    If (statoContratto.Text = "3" Or statoContratto.Text = "4" Or statoContratto.Text = "8" Or statoContratto.Text = "6") And tipo = "rientro" Then
                        If chkScegli.Checked And chkScegli.Visible And Not omaggiato.Checked Then
                            .vari = .vari & vbCrLf & nome_costo.Text
                            .sconto = .sconto & vbCrLf
                            .costo = .costo & vbCrLf & costo_scontato.Text
                            .qta = .qta & vbCrLf & "1"
                            .costounitarioscontato = .costounitarioscontato & vbCrLf & costo_scontato.Text
                            .totale = .totale & vbCrLf & costo_scontato.Text
                        End If
                    End If
                End If
            Next

            '------------------------------------------------------------------------------------------------------------------------------


            Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc2.Open()
            Dim Cmd2 As New Data.SqlClient.SqlCommand("SELECT POS_Funzioni.funzione, PAGAMENTI_EXTRA.id_pos_funzioni_ares,PAGAMENTI_EXTRA.data, PAGAMENTI_EXTRA.DATA_OPERAZIONE, PAGAMENTI_EXTRA.PER_IMPORTO, PAGAMENTI_EXTRA.ID_CTR, PAGAMENTI_EXTRA.preaut_aperta, PAGAMENTI_EXTRA.NR_PREAUT, PAGAMENTI_EXTRA.operazione_stornata, (CASE WHEN N_CONTRATTO_RIF IS NULL THEN 'PREN ' ELSE 'RA ' END) AS provenienza FROM PAGAMENTI_EXTRA WITH(NOLOCK) INNER JOIN POS_Funzioni WITH(NOLOCK) ON PAGAMENTI_EXTRA.id_pos_funzioni_ares=POS_funzioni.id WHERE N_CONTRATTO_RIF='" & lblNumContratto.Text & "' ORDER BY DATA_OPERAZIONE DESC", Dbc2)

            Dim Rs2 As Data.SqlClient.SqlDataReader
            Rs2 = Cmd2.ExecuteReader()


            .vari = .vari & vbCrLf & vbCrLf & "PAGAMENTI" & vbCrLf & vbCrLf
            Do While Rs2.Read

                If Rs2.HasRows() Then

                    If Rs2("provenienza") = "RA " Then
                        .vari = .vari & Rs2("data") & " "

                        .vari = .vari & Rs2("funzione") & " "


                        .vari = .vari & FormatCurrency(Rs2("PER_IMPORTO"), 2) & "  "
                        If Rs2("funzione") = "Richiesta" Then
                            .vari = .vari & "Nr.Pr. " & Rs2("NR_PREAUT") & " "
                        End If
                        .vari = .vari & vbCrLf

                    End If

                End If
            Loop
            Rs2.Close()
            Rs2 = Nothing
            Cmd2.Dispose()
            Cmd2 = Nothing
            Dbc2.Close()
            Dbc2.Dispose()
            Dbc2 = Nothing


            'PENALI (INFORMATIVE) ---------------------------------------------------------------------------------------------------------

            'If assicurazione_acquistata Then
            '    penali = penali & " FCDW-Responsabilità DANNI: 0 € "
            'End If

            'If assicurazione_acquistata Then
            '    penali = penali & " FCDW-Responsabilità DANNI: 0 € - FTLW-Responsabilità FURTO: 0 € "
            'End If




            '-------------------------------------------------------------------------------------------------------------------------------

            'MAPPATURA AUTO IN PDF---------------------------------------------------------------------------------------------------------


            Dim Dbc3 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc3.Open()

            Dim sqlMap As String = "SELECT DISTINCT veicoli_posizione_danno.descrizione, veicoli_tipo_danno.descrizione AS descrizione_tipo_danno, veicoli_posizione_danno.id,veicoli_danni.descrizione as descrizione_danno FROM veicoli_gruppo_danni WITH(NOLOCK) INNER JOIN" &
            " veicoli_tipo_danno WITH(NOLOCK) ON veicoli_gruppo_danni.id_tipo_danno = veicoli_tipo_danno.id INNER JOIN" &
            "  veicoli WITH(NOLOCK) ON veicoli_gruppo_danni.id_veicolo = veicoli.id INNER JOIN" &
            " veicoli_posizione_danno WITH(NOLOCK) ON veicoli_gruppo_danni.id_posizione_danno = veicoli_posizione_danno.id" &
            " INNER JOIN veicoli_danni WITH(NOLOCK) ON veicoli_tipo_danno.id = veicoli_danni.id_tipo_danno AND veicoli_posizione_danno.id = veicoli_danni.id_posizione_danno" &
            " WHERE veicoli.targa='" & txtTarga.Text & "'"



            Dim Cmd3 As New Data.SqlClient.SqlCommand(sqlMap, Dbc3)

            Dim Rs3 As Data.SqlClient.SqlDataReader
            Rs3 = Cmd3.ExecuteReader()
            Dim contatore As Integer = 1
            Do While Rs3.Read

                If Rs3.HasRows() Then

                    If (contatore Mod 2) <> 0 Then

                        .danno_1 = .danno_1 & Rs3("descrizione") & " " & Rs3("descrizione_tipo_danno") & vbCrLf
                        .danno_1 = .danno_1 & Rs3("descrizione_danno") & vbCrLf & vbCrLf

                    ElseIf (contatore Mod 2) = 0 Then
                        .danno_2 = .danno_2 & Rs3("descrizione") & " " & Rs3("descrizione_tipo_danno") & vbCrLf
                        .danno_2 = .danno_2 & Rs3("descrizione_danno") & vbCrLf & vbCrLf

                    End If

                    '        If Rs3("id") = "75" Then
                    '            .sportello_ant_sx = contatore
                    '        End If
                    '        If Rs3("id") = "76" Then
                    '            .sportello_post_sx = contatore
                    '        End If
                    '        If Rs3("id") = "74" Then
                    '            .cerchione_ant_sx = contatore
                    '        End If
                    '        If Rs3("id") = "78" Then
                    '            .cerchione_post_sx = contatore
                    '        End If
                    '        If Rs3("id") = "73" Then
                    '            .ruota_ant_sx = contatore
                    '        End If
                    '        If Rs3("id") = "79" Then
                    '            .ruota_post_sx = contatore
                    '        End If
                    '        If Rs3("id") = "62" Then
                    '            .paraurti_ant_sx = contatore
                    '        End If
                    '        If Rs3("id") = "57" Then
                    '            .paraurti_ant_dx = contatore
                    '        End If
                    '        If Rs3("id") = "61" Then
                    '            .proiettore_ant_sx = contatore
                    '        End If
                    '        If Rs3("id") = "58" Then
                    '            .proiettore_ant_dx = contatore
                    '        End If
                    '        If Rs3("id") = "59" Then
                    '            .maschera_ant = contatore
                    '        End If
                    '        If Rs3("id") = "60" Then
                    '            .targa_ant = contatore
                    '        End If
                    '        If Rs3("id") = "63" Then
                    '            .cofano_ant = contatore
                    '        End If
                    '        If Rs3("id") = "64" Then
                    '            .parabrezza_ant = contatore
                    '        End If
                    '        If Rs3("id") = "65" Then
                    '            .tetto = contatore
                    '        End If
                    '        If Rs3("id") = "66" Then
                    '            .lunotto_post = contatore
                    '        End If
                    '        If Rs3("id") = "67" Then
                    '            .cofano_o_portello_post = contatore
                    '        End If
                    '        If Rs3("id") = "72" Then
                    '            .paraurti_post_sx = contatore
                    '        End If
                    '        If Rs3("id") = "68" Then
                    '            .paraurti_post_dx = contatore
                    '        End If
                    '        If Rs3("id") = "71" Then
                    '            .fanale_post_sx = contatore
                    '        End If
                    '        If Rs3("id") = "69" Then
                    '            .fanale_post_dx = contatore
                    '        End If
                    '        If Rs3("id") = "70" Then
                    '            .targa_post = contatore
                    '        End If
                    '        If Rs3("id") = "54" Then
                    '            .sportello_ant_dx = contatore
                    '        End If
                    '        If Rs3("id") = "77" Then
                    '            .sportello_post_dx = contatore
                    '        End If
                    '        If Rs3("id") = "53" Then
                    '            .cerchione_ant_dx = contatore
                    '        End If
                    '        If Rs3("id") = "55" Then
                    '            .cerchione_post_dx = contatore
                    '        End If
                    '        If Rs3("id") = "56" Then
                    '            .ruota_post_dx = contatore
                    '        End If
                    '        If Rs3("id") = "52" Then
                    '            .ruota_ant_dx = contatore
                    '        End If



                End If
                contatore = contatore + 1
            Loop

            Rs3.Close()
            Rs3 = Nothing
            Cmd3.Dispose()
            Cmd3 = Nothing
            Dbc3.Close()
            Dbc3.Dispose()
            Dbc3 = Nothing
            'Dim WorkingFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

            ' ''//The file that we are creating
            'Dim WorkingFile = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\modelli_documenti\ModuloDiNoleggioNew.pdf"

            'Dim SampleImage = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\modelli_documenti\fine_Eemergenza_lieve.png"

            ' ''//Create our file with an exclusive writer lock
            'Using FS As New FileStream(WorkingFile, FileMode.Create, FileAccess.Write, FileShare.None)
            '    ''//Create our PDF document
            '    Using Doc As New Document(PageSize.LETTER)
            '        ''//Bind our PDF object to the physical file using a PdfWriter
            '        Using Writer = PdfWriter.GetInstance(Doc, FS)
            '            ''//Open our document for writing
            '            Doc.Open()

            '            ''//Insert a blank page
            '            Doc.NewPage()

            '            ''//Add an image to a document. This does not scale the image or anything so if your image is large it might go off the canvas
            '            Doc.Add(iTextSharp.text.Image.GetInstance(SampleImage))

            '            ''//Close our document
            '            Doc.Close()
            '        End Using
            '    End Using
            'End Using




            '-------------------------------------------------------------------------------------------------------------------------------



            '.extra_e_penali = "Possibili extra ed eventuali penalità a carico del cliente:" & vbCrLf & penali & vbCrLf & vbCrLf & _
            '"Qualora durante il periodo di noleggio mi siano state elevate delle sanzioni amministrative per infrazioni al codice della strada, " & _
            '"Sicily By Car - Auto Europa provvederà a fornire i miei dati alle Autorità che mi hanno contestato la violazione, al fine di " & _
            '"rendere possibile la rinotifica delle sanzioni nei miei confronti, addebitando sulla mia carta di credito l'importo di € 60,60 IVA inclusa " & _
            '"a titolo di rimborso per le spese di gestione pratica. Qualora io sia a conoscenzadi una sanzione per infrazione al Codice della " & _
            '"Strada a me elevata, provvederò al pagamento della stessa al fine di evitare l'addebbito di € 60,50 IVA inclusa per le spese di " & _
            '"gestione pratica." & vbCrLf & _
            '"Nel caso in cui io sia possessore del pass riservato ai disabili, sono obbligato a comunicare tempestivamente la targa del veicolo " & _
            '"noleggiato direttamente all'Autorità competente al fine di evitare l'addebito per le spese di notifica per le sanzioni amministrative " & _
            '"elevate a mezzo telecamera per infrazioni del codice della strada." & vbCrLf & _
            '"Prendo atto che, dove vige l'obbligo, utilizzerò le catene da neve." & vbCrLf & _
            '"Mi obbligo in montagna con le basse temperature, ad immettere nel serbatoio il liquido antigelo. Prendo atto che sono responsabile " & _
            '"di eventuali danni per guida su strade non asfaltate e di eventuali danni causati da vegetazione, entrambi non coperti da nessuna assicurazione." & vbCrLf & _
            '"Mi obbligo a condurre il veicolo solo ed esclusivamente in paesi appartenenti all'Unione Europea." & vbCrLf & _
            '"Prendo atto che la Car Protection Plus non trova completa applicazione in caso di furto totale/parziale o incendio del veicolo " & _
            '"qualora avvenga in Campania, Puglia, Calabria e Sicilia, per i quali la quota addebito furto non viene totalmente eliminata " & _
            '"ma ridotta agli importi prestabiliti come quota non eliminabile." & vbCrLf & vbCrLf & _
            '"Dichiaro che, se dovessi incorrere, in un incidente stradale, solo obbligato a compilare il modulo CAI con esattezza e " & _
            '"consegnato al mio rientro al personale di banco Sicily By Car - Auto Europa. Se ad inizio nolo ho sottoscritto la " & _
            '"Car Protection Plus e la PAI Plus, non mi saranno addebitate le penalità, sempre che abbia rispettato tutte le condizioni da " & _
            '"me sottoscritte. Prendo atto che la Car Protection Plus e/o PAI Plus non operano nel caso in cui il mio comportamento " & _
            '"non sia stato diligente ai sensi dell'art. 1176 Cod Civ. ed in ogni caso non potrò avvalermi di alcuna limitazione della " & _
            '"responsabilità in caso di negligenza, dolo o colpa grave ai sensi dell'art. 1229 Cod Civ. Prendo atto inoltre che in ogni caso " & _
            '"sarò responsabile dei danni al veicolo qualora non fornisca una completa dichiarazione relativa al sinistro occorso durante il mio noleggio." & vbCrLf & vbCrLf & _
            '"SERVICE DELAYED CHARGE - CONTRATTO PER ADDEBITI SUCCESSIVI AL NOLEGGIO" & vbCrLf & _
            '"Riconosco ed accetto tutte le spese: carburante, penalità assicurative, danni rilevati o riscontrati dopo la riconsegna, " & _
            '"del veicolo, e € 60,50 IVA inclusa per il rimborso spese gestione pratica relativa a singola multa o pedaggio autostradale da me " & _
            '"non pagati ed autorizzo Sicily By Car - Autoeuropa ad addebitare sulla mia carta di credito."

            If Not assicurazione_acquistata Then
                .extra_e_penali = .extra_e_penali & vbCrLf & vbCrLf &
                        "Non avendo sottoscritto la Car Protection Plus e la PAI Plus, io titolare della carta di credito sopra riportata autorizzo " &
                        "la Sicily By Car - Autoeuropa ad addebitare sulla mia carta di credito l'importo della penalità prevista."
            End If
            '------------------------------------------------------------------------------------------------------------------------------
            .note = txtNoteContratto.Text

            'SE IN FASE DI CONTRATTO CHIUSO VIENE RICHIESTA LA STAMPA DEL CONTRATTO DI USCITA RIPRISTINO I COSTI ATTUALI
            If statoContratto.Text <> "1" And statoContratto.Text <> "2" And tipo = "uscita" Then
                idContratto.Text = idContratto_temp
                numCalcolo.Text = numCalcolo_temp

                listContrattiCosti.DataBind()
            End If
        End With

        Session("DatiStampaContratto") = miei_dati

        Dim Generator As System.Random = New System.Random()

        Dim num_random As String = Format(Generator.Next(100000000), "000000000")


        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraContrattoNew.aspx?a=" & num_random & "','')", True)
            End If
        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
    End Sub
    Function trova_penale() As Array



    End Function
    Protected Function getNumeroPrenotazione(ByVal id_pren As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT numpren FROM prenotazioni WITH(NOLOCK) WHERE Nr_Pren='" & id_pren & "'", Dbc)

        getNumeroPrenotazione = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub pagamento_nuovo_contratto()
        lb_tipo_pagamento.Text = "contratto"

        tab_pagamento.Visible = True
        tab_contratto.Visible = False

        Dim Dati As New tabelle_pos_Scambio_Importo.DatiInizializzazione()
        Dati.IDStazione = Request.Cookies("SicilyRentCar")("stazione")
        Dati.NumeroDocumento = lblNumContratto.Text
        Dati.TipoDocumento = tabelle_pos_Scambio_Importo.TipoDocumento.Contratto
        Dati.ListaPreautorizzazioni = New System.Collections.Generic.List(Of tabelle_pos_Scambio_Importo.Preautorizzazione)

        'RECUPERO EVENTUALI PREAUTORIZZAZIONI APERTE SE C'E' UNA PRENOTAZIONE (ALTRIMENTI DI SICURO NON E' MAI STATO FATTO ALCUN PAGAMENTO)

        Dim preautorizzazioni(50) As String

        If idPrenotazione.Text <> "" Then
            preautorizzazioni = cPagamenti.getListPreautorizzazioni(lblNumPren.Text, "", "", lblNumContratto.Text)
        Else
            preautorizzazioni = cPagamenti.getListPreautorizzazioni("", "", "", lblNumContratto.Text)
        End If

        Dim i As Integer = 0

        Dim pre As tabelle_pos_Scambio_Importo.Preautorizzazione
        pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()

        Do While preautorizzazioni(i) <> "0"
            pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()
            pre.Numero = preautorizzazioni(i)
            Dati.ListaPreautorizzazioni.Add(pre)
            i = i + 1
        Loop

        If statoContratto.Text = "0" Or statoContratto.Text = "1" Then
            Dati.Importo = lblDaPreautorizzare.SelectedValue
        Else
            Dati.Importo = getTotaleDaPagare()
        End If

        If Dati.Importo < 0 Then
            Dati.importo = 0
        End If

        If dropTest.SelectedValue = "0" Then
            Dati.importo_non_modificabile_preautorizzazione = False
            Dati.TestMode = True
        ElseIf dropTest.SelectedValue = "1" Then
            Dati.importo_non_modificabile_preautorizzazione = False
            Dati.TestMode = False
        End If

        Dati.ImportoMassimoRimborsabile = 100   '<<<----- IMPOSTARE

        'Dati.PreSelectIDEnte = 13
        'Dati.PreSelectIDAcquireCircuito = 64
        'Dati.PreSelectIDFunzione = Costanti.POS_FunzioneIntegrazione
        'Dati.PreSelectPOSID = 16
        'Dati.PreSelectNumeroPreautorizzazione = "363320898"

        If tariffa_broker.Text = "1" Or prenotazione_prepagata.Text = "True" Or complimentary.Text = "1" Then
            Dati.complimentary_abilitato = False
        End If

        If full_credit.Text = "1" Then
            Dati.full_credit_abilitato = False
        End If

        Dati.TipoPagamentoContanti = FiltroTipoPagamentoContanti.ChiusuraContratto

        Scambio_Importo1.InizializzazioneDati(Dati)
    End Sub

    Protected Sub pagamento_rds(ByVal mio_evento As veicoli_evento_apertura_danno)
        lb_tipo_pagamento.Text = "rds"

        tab_pagamento.Visible = True
        div_edit_danno.Visible = False

        With mio_evento
            Dim Dati As New tabelle_pos_Scambio_Importo.DatiInizializzazione()
            Dati.IDStazione = Request.Cookies("SicilyRentCar")("stazione")
            Dati.NumeroDocumento = .id_rds
            Dati.TipoDocumento = tabelle_pos_Scambio_Importo.TipoDocumento.RDS
            Dati.ListaPreautorizzazioni = New System.Collections.Generic.List(Of tabelle_pos_Scambio_Importo.Preautorizzazione)

            'RECUPERO EVENTUALI PREAUTORIZZAZIONI APERTE SE C'E' UNA PRENOTAZIONE (ALTRIMENTI DI SICURO NON E' MAI STATO FATTO ALCUN PAGAMENTO)

            Dim preautorizzazioni(50) As String


            preautorizzazioni = cPagamenti.getListPreautorizzazioni("", .id_rds, "", "")

            Dim i As Integer = 0

            Dim pre As tabelle_pos_Scambio_Importo.Preautorizzazione
            pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()

            Do While preautorizzazioni(i) <> "0"
                pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()
                pre.Numero = preautorizzazioni(i)
                Dati.ListaPreautorizzazioni.Add(pre)
                i = i + 1
            Loop


            Dati.Importo = "960"

            If dropTest.SelectedValue = "0" Then
                Dati.importo_non_modificabile_preautorizzazione = True
                Dati.TestMode = True
            ElseIf dropTest.SelectedValue = "1" Then
                Dati.importo_non_modificabile_preautorizzazione = False
                Dati.TestMode = False
            End If

            Dati.ImportoMassimoRimborsabile = 0   '<<<----- IMPOSTARE

            'Dati.PreSelectIDEnte = 13
            'Dati.PreSelectIDAcquireCircuito = 64
            'Dati.PreSelectIDFunzione = Costanti.POS_FunzioneIntegrazione
            'Dati.PreSelectPOSID = 16
            'Dati.PreSelectNumeroPreautorizzazione = "363320898"

            Scambio_Importo1.InizializzazioneDati(Dati)

        End With
    End Sub

    Protected Sub salva_contratto_nolo_in_corso(ByVal status As String)
        'PASSARE STATUS = 2 PER MODIFICA DI CONTRATTO IN CORSO 
        'PASSARE STATUS = 3 PER SALVATAGGIO SU QUICK CHECK IN
        'PASSARE STATUS = 4 PER QUALSIASI STATO SUCCESSIVO ALLA CHIUSURA (MODIFICA ADMIN)
        'PASSARE STATUS = 5 PER SALVATAGGIO SU CREAZIONE CRV

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        'LA TARIFFA POTREBBE VARIARE - OPERATORE ADMIN
        Dim id_tariffe_righe As Integer
        Dim id_tariffa As Integer
        Dim tipo_tariffa As String = ""
        Dim codice_tariffa As String

        If dropTariffeGeneriche.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
            codice_tariffa = dropTariffeGeneriche.SelectedItem.Text
            tipo_tariffa = "generica"
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
            codice_tariffa = dropTariffeParticolari.SelectedItem.Text
            tipo_tariffa = "fonte"
        End If

        'FACCIO ANCHE IN MODO CHE LA TARIFFA SCELTA SIA L'UNICA PRESENTE NEL MENU' A TENDINA (SERVE PER SUCCESSIVE MODIFICHE)
        If dropTariffeGeneriche.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
            codice_tariffa = dropTariffeGeneriche.SelectedItem.Text.Replace(" (RA)", "")
            tipo_tariffa = "generica"

            dropTariffeGeneriche.Items.Clear()
            dropTariffeGeneriche.Items.Add("Seleziona...")
            dropTariffeGeneriche.Items(0).Value = "0"

            dropTariffeGeneriche.Items.Add(codice_tariffa)
            dropTariffeGeneriche.Items(1).Value = id_tariffe_righe

            dropTariffeGeneriche.SelectedValue = CInt(id_tariffe_righe)
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
            codice_tariffa = dropTariffeParticolari.SelectedItem.Text.Replace(" (RA)", "")
            tipo_tariffa = "fonte"

            dropTariffeParticolari.Items.Clear()
            dropTariffeParticolari.Items.Add("Seleziona...")
            dropTariffeParticolari.Items(0).Value = "0"

            dropTariffeParticolari.Items.Add(codice_tariffa)
            dropTariffeParticolari.Items(1).Value = id_tariffe_righe

            dropTariffeParticolari.SelectedValue = CInt(id_tariffe_righe)
        End If

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_tariffa FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)
        id_tariffa = Cmd.ExecuteScalar

        'LA DATA DI USCITA NORMALMENTE NON VARIA E NON DEVE ESSERE SALVATA ECCETTO NEL CASO IN CUI SI EFFETTUA IL SALVATAGGIO 
        'IN FASE DI USCITA DEL VEICOLO

        Dim data_uscita As String = getDataDb_con_orario(txtDaData.Text & " " & ore1.Text & ":" & minuti1.Text & ":00")
        Dim data_rientro As String
        Dim data_presunto_rientro As String
        Dim id_stazione_presunto_rientro As String
        Dim id_stazione_rientro As String

        If status = "3" Or status = "4" Or status = "8" Then
            'NEL CASO DI SALVATAGGIO SU QUICK CHECK IN DEVO SALVARE CORETTAMENTE IL PRESUNTO RIENTRO E IL RIENTRO REALE
            data_presunto_rientro = getDataDb_con_orario(txtADataPresunto.Text & " " & ore2_presunto.Text & ":" & minuti2_presunto.Text & ":00")
            data_rientro = "'" & getDataDb_con_orario(txtAData.Text & " " & ore2.Text & ":" & minuti2.Text & ":00") & "'"
            id_stazione_presunto_rientro = CInt(dropStazioneRientroPresunto.SelectedValue)
            id_stazione_rientro = "'" & CInt(dropStazioneDropOff.SelectedValue) & "'"
        Else
            data_presunto_rientro = getDataDb_con_orario(txtAData.Text & " " & ore2.Text & ":" & minuti2.Text & ":00")
            data_rientro = "NULL"
            id_stazione_presunto_rientro = CInt(dropStazioneDropOff.SelectedValue)
            id_stazione_rientro = "NULL"
        End If

        Dim id_gruppo_auto_originale As String
        Dim giorni_originale As String
        Dim data_uscita_originale As String
        Dim data_presunto_rientro_originale As String
        Dim id_stazione_presunto_rientro_originale As String
        Dim tariffa_rack_utilizzata As String

        If tariffa_broker.Text = "1" Then
            'CASO BROKER: SE LA VARIAZIONE E' A CARICO DEL BROKER ALLORA SALVO I DATI ATTUALI COME QUELLI ORIGINALI, ALTRIMENTI SI UTLIZZA
            'LA RACK E QUINDI LASCIO MEMORIZZATI I DATI ORIGINALI
            If dropVariazioneACaricoDi.SelectedValue = "1" Then
                'RACK NON UTLIZZATA
                id_gruppo_auto_originale = "id_gruppo_auto_originale"
                giorni_originale = "'" & txtNumeroGiorni.Text & "'"
                data_uscita_originale = "data_uscita_originale"
                If status = "3" Or status = "4" Or status = "8" Then
                    data_presunto_rientro_originale = data_rientro
                Else
                    data_presunto_rientro_originale = "'" & data_presunto_rientro & "'"
                End If

                id_stazione_presunto_rientro_originale = "'" & CInt(dropStazioneDropOff.SelectedValue) & "'"
                tariffa_rack_utilizzata = "tariffa_rack_utilizzata"

                'AGGIORNO I DATI MEMORIZZATI IN MODO TALE DA POTER PROSEGUIRE CON SUCCESSIVE MODIFICHE FIN DA SUBITO
                txtNumeroGiorniIniziali.Text = txtNumeroGiorni.Text
                txtADataOld.Text = txtAData.Text
                id_stazione_presunto_rientro_originale = CInt(dropStazioneDropOff.SelectedValue)
            Else
                'RACK UTILIZZATA
                id_gruppo_auto_originale = "id_gruppo_auto_originale"
                giorni_originale = "'" & txtNumeroGiorniTO.Text & "'"
                data_uscita_originale = "data_uscita_originale"
                data_presunto_rientro_originale = "data_presunto_rientro_originale"
                id_stazione_presunto_rientro_originale = "id_stazione_presunto_rientro_originale"
                tariffa_rack_utilizzata = "tariffa_rack_utilizzata"

                txtNumeroGiorniIniziali.Text = txtNumeroGiorniTO.Text
            End If
        Else
            'CASO NON BROKER
            If rack_utilizzata.Text = "1" Then
                'SE PER IL CALCOLO PRECEDENTE HO GIA' UTILIZZATO LA RACK NON DEVO SALVARE NULLA DI NUOVO CIRCA I DATI ORIGINALI
                id_gruppo_auto_originale = "id_gruppo_auto_originale"
                giorni_originale = "giorni_originale"
                data_uscita_originale = "data_uscita_originale"
                data_presunto_rientro_originale = "data_presunto_rientro_originale"
                id_stazione_presunto_rientro_originale = "id_stazione_presunto_rientro_originale"
                tariffa_rack_utilizzata = "tariffa_rack_utilizzata"
            ElseIf rack_utilizzata.Text = "0" Then
                'SE PER IL CALCOLO PRECEDENTE LA RACK NON ERA STATA UTILIZZATA CONTROLLO SE ORA E' STATA USATA
                If tariffa_vendibile(id_tariffe_righe) Then
                    'SE LA TARIFFA E' ANCORA VENDIBILE DI CERTO NON HO UTILIZZATO LA RACK PER CUI SALVO I DATI ATTUALI
                    id_gruppo_auto_originale = "id_gruppo_auto_originale"
                    giorni_originale = "'" & txtNumeroGiorni.Text & "'"
                    data_uscita_originale = "data_uscita_originale"
                    If status = "3" Or status = "4" Or status = "8" Then
                        data_presunto_rientro_originale = data_rientro
                    Else
                        data_presunto_rientro_originale = "'" & data_presunto_rientro & "'"
                    End If
                    id_stazione_presunto_rientro_originale = "'" & CInt(dropStazioneDropOff.SelectedValue) & "'"
                    tariffa_rack_utilizzata = "tariffa_rack_utilizzata"

                    'AGGIORNO I DATI MEMORIZZATI IN MODO TALE DA POTER PROSEGUIRE CON SUCCESSIVE MODIFICHE FIN DA SUBITO
                    txtNumeroGiorniIniziali.Text = txtNumeroGiorni.Text
                    txtADataOld.Text = txtAData.Text
                    id_stazione_presunto_rientro_originale = CInt(dropStazioneDropOff.SelectedValue)
                Else
                    'SE LA TARIFFA NON E' PIU' VENDIBILE CONTROLLO SE E' STATA USATA LA RACK
                    If (gruppoDaCalcolare.SelectedValue <> gruppo_da_calcolare_originale.Text) Or (CInt(txtNumeroGiorni.Text) > CInt(txtNumeroGiorniIniziali.Text)) Then
                        'IN QUESTO CASO LA RACK E' STATA CERTAMENTE UTILIZZATA - LASCIO I DATI ORIGINALI ATTUALMENTE SALVATI
                        id_gruppo_auto_originale = "id_gruppo_auto_originale"
                        giorni_originale = "giorni_originale"
                        data_uscita_originale = "data_uscita_originale"
                        data_presunto_rientro_originale = "data_presunto_rientro_originale"
                        id_stazione_presunto_rientro_originale = "id_stazione_presunto_rientro_originale"
                        tariffa_rack_utilizzata = "'1'"

                        rack_utilizzata.Text = "1"
                    Else
                        'TARIFFA NON VENDIBILE MA RACK NON UTILIZZATA - SALVO I DATI ATTUALI
                        id_gruppo_auto_originale = "id_gruppo_auto_originale"
                        giorni_originale = "'" & txtNumeroGiorni.Text & "'"
                        data_uscita_originale = "data_uscita_originale"
                        If status = "3" Or status = "4" Or status = "8" Then
                            data_presunto_rientro_originale = "'" & data_rientro & "'"
                        Else
                            data_presunto_rientro_originale = "'" & data_presunto_rientro & "'"
                        End If
                        id_stazione_presunto_rientro_originale = "'" & CInt(dropStazioneDropOff.SelectedValue) & "'"
                        tariffa_rack_utilizzata = "tariffa_rack_utilizzata"

                        'AGGIORNO I DATI MEMORIZZATI IN MODO TALE DA POTER PROSEGUIRE CON SUCCESSIVE MODIFICHE FIN DA SUBITO
                        txtNumeroGiorniIniziali.Text = txtNumeroGiorni.Text
                        txtADataOld.Text = txtAData.Text
                        id_stazione_presunto_rientro_originale = dropStazioneDropOff.SelectedValue
                    End If
                End If
            End If
        End If


        Dim imp_a_carico_del_broker As String
        Dim giorni_to As String

        If a_carico_del_broker.Text <> "" Then
            If dropVariazioneACaricoDi.Text = "1" Then
                'SE E' A CARICO DEL BROKER
                a_carico_del_broker.Text = getCostoACaricoDelBroker()
                imp_a_carico_del_broker = "'" & Replace(a_carico_del_broker.Text, ",", ".") & "'"
            Else
                'SE E' A CARICO DEL CLIENTE IL COSTO A CARICO DEL BROKER E' QUELLO PRECEDENTE
                imp_a_carico_del_broker = "'" & Replace(a_carico_del_broker.Text, ",", ".") & "'"
            End If
            giorni_to = "'" & txtNumeroGiorniTO.Text & "'"
            lblGiorniToOld.Text = txtNumeroGiorniTO.Text
        Else
            imp_a_carico_del_broker = "NULL"
            giorni_to = "NULL"
        End If

        Dim num_crv As String
        Dim id_veicolo As String
        Dim serbatoio_max As String
        Dim id_alimentazione As String
        Dim targa As String
        Dim modello As String
        Dim km_uscita As String
        Dim km_rientro As String
        Dim litri_uscita As String
        Dim litri_rientro As String
        Dim id_gruppo_danni_uscita As String

        If status = "5" Then
            'SE STO SALVANDO UN CRV - L'INSERT VIENE FATTA QUANDO ANCORA L'AUTO SUCCESSIVA NON E' STATA SELEZIONATA - UNA VOLTA CHE LA SI
            'SELEZIONA VERRA' FATTO UN UPDATE SULLA RIGA CHE SI STA CREANDO
            num_crv = "num_crv+1"
            id_veicolo = "NULL"
            serbatoio_max = "NULL"
            id_alimentazione = "NULL"
            targa = "NULL"
            modello = "NULL"
            km_uscita = "NULL"
            km_rientro = "NULL"
            litri_uscita = "NULL"
            litri_rientro = "NULL"
            id_gruppo_danni_uscita = "NULL"
        Else
            num_crv = "num_crv"
            id_veicolo = "id_veicolo"
            serbatoio_max = "serbatoio_max"
            id_alimentazione = "id_alimentazione"
            targa = "targa"
            modello = "modello"
            km_uscita = "km_uscita"
            km_rientro = "km_rientro"
            litri_uscita = "litri_uscita"
            litri_rientro = "litri_rientro"
            id_gruppo_danni_uscita = "id_gruppo_danni_uscita"
        End If

        Dim sqlStr As String = "INSERT INTO contratti (num_contratto, num_calcolo, num_crv, status, attivo, id_stazione_uscita," &
        " id_stazione_presunto_rientro, id_stazione_presunto_rientro_originale,  id_stazione_rientro, data_uscita," &
        " data_uscita_originale, data_presunto_rientro, data_presunto_rientro_originale, data_rientro, id_gruppo_auto," &
        " id_gruppo_auto_originale, giorni, giorni_to, giorni_originale, giorno_extra_abbuonato, ID_GRUPPO_APP, id_primo_conducente, id_secondo_conducente," &
        " eta_primo_guidatore, eta_secondo_guidatore,   id_fonte, codice_edp, id_cliente, id_tariffa, id_tariffe_righe," &
        " tariffa_rack_utilizzata, id_tempo_km_rack,  tipo_tariffa, sconto_applicato, tipo_sconto, sconto_web_prepagato, sconto_su_rack, CODTAR, id_gps, codice_gps, id_veicolo, serbatoio_max, id_alimentazione," &
        " targa, modello, km_uscita, km_rientro, litri_uscita, litri_rientro, NOTE_da_prenotazione, N_VOLOOUT, N_VOLOPR, rif_to," &
        " id_operatore_creazione, data_creazione,   id_operatore_ultima_modifica, data_ultima_modifica, num_preventivo, num_prenotazione," &
        " giorni_noleggio_da_prenotazione, data_uscita_da_prenotazione, data_presunto_rientro_da_prenotazione, id_gruppo_da_prenotazione," &
        " id_stazione_drop_off_da_prenotazione, totale_costo_prenotazione, prenotazione_prepagata,  importo_prepagato, giorni_prepagati," &
        " id_gruppo_danni_uscita, id_gruppo_danni_rientro, NOTE_contratto, importo_a_carico_del_broker, importo_a_carico_del_broker_ribaltato, " &
        " gg_a_carico_del_broker_ribaltato, fatturazione_da_controllare, id_operatore_fatt_controllare, " &
        "id_fonte_commissionabile, tipo_commissione, commissione_percentuale, giorni_commissioni)" &
        " (SELECT num_contratto, " & numCalcolo.Text & "," & num_crv & ",'" & status & "', attivo, id_stazione_uscita,'" & id_stazione_presunto_rientro & "'," &
        id_stazione_presunto_rientro_originale & "," & id_stazione_rientro & ",'" & data_uscita & "', " & data_uscita_originale & ",'" & data_presunto_rientro & "'," &
        data_presunto_rientro_originale & "," & data_rientro & ", id_gruppo_auto,  " & id_gruppo_auto_originale & "," & txtNumeroGiorni.Text & "," & giorni_to & "," &
        giorni_originale & ",'" & chkAbbuonaGiornoExtra.Checked & "', ID_GRUPPO_APP, id_primo_conducente, id_secondo_conducente, eta_primo_guidatore, eta_secondo_guidatore, " &
        "id_fonte, codice_edp, id_cliente,'" & id_tariffa & "','" & id_tariffe_righe & "', " & tariffa_rack_utilizzata & ",id_tempo_km_rack,'" & tipo_tariffa & "', sconto_applicato, tipo_sconto, sconto_web_prepagato," &
        "sconto_su_rack,'" & Replace(codice_tariffa, "'", "''") & "', id_gps, codice_gps, " & id_veicolo & ", " & serbatoio_max & ", " & id_alimentazione & "," &
        targa & ", " & modello & "," & km_uscita & "," & km_rientro & "," & litri_uscita & "," & litri_rientro & "," &
        "NOTE_da_prenotazione, N_VOLOOUT, N_VOLOPR, rif_to, id_operatore_creazione, data_creazione, " &
        "'" & Request.Cookies("SicilyRentCar")("IdUtente") & "', GetDate(), num_preventivo, num_prenotazione," &
        "giorni_noleggio_da_prenotazione, data_uscita_da_prenotazione, data_presunto_rientro_da_prenotazione, id_gruppo_da_prenotazione," &
        "id_stazione_drop_off_da_prenotazione, totale_costo_prenotazione, prenotazione_prepagata,  importo_prepagato, giorni_prepagati," &
        id_gruppo_danni_uscita & ", id_gruppo_danni_rientro,note_contratto," & imp_a_carico_del_broker & ", importo_a_carico_del_broker_ribaltato, gg_a_carico_del_broker_ribaltato, " &
        "fatturazione_da_controllare, id_operatore_fatt_controllare, id_fonte_commissionabile, tipo_commissione, commissione_percentuale, giorni_commissioni " &
        "FROM contratti  AS contratti1 WITH(NOLOCK) WHERE id='" & idContratto.Text & "')"

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        Dim id_ctr_old As String = idContratto.Text

        sqlStr = "SELECT @@IDENTITY FROM contratti WITH(NOLOCK)"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        idContratto.Text = Cmd.ExecuteScalar

        sqlStr = "UPDATE contratti SET attivo='0' WHERE id='" & id_ctr_old & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        sqlStr = "UPDATE contratti_costi SET id_documento='" & idContratto.Text & "' WHERE id_documento='" & id_ctr_old & "' And num_calcolo='" & numCalcolo.Text & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        If status = "3" Or status = "4" Then
            'NEL CASO IN CUI E' STATO EFFETTUATO IL QUICK CHECK IN SETTO IL CAMPO secondo_ordine_stampa PER FARE IN MODO CHE IL TOTALE
            'COMPAIA DOPO LE INFORMATIVE, VISTO CHE DA QUESTO MOMENTO IN POI E' POSSIBILE AGGIUNGERE LE INFORMATIVE AL TOTALE
            sqlStr = "UPDATE contratti_costi SET secondo_ordine_stampa='1' WHERE id_documento='" & idContratto.Text & "' And num_calcolo='" & numCalcolo.Text & "' AND nome_costo='" & Costanti.testo_elemento_totale & "'"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()
        End If

        sqlStr = "UPDATE contratti_warning SET id_documento='" & idContratto.Text & "' WHERE id_documento='" & id_ctr_old & "' And num_calcolo='" & numCalcolo.Text & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        statoContratto.Text = status

        If status = "2" Then
            'NEL CASO DI MODIFICA A NOLO IN CORSO AGGIORNO IL PRESUNTO RIENTRO IN movimenti_targa
            sqlStr = "UPDATE movimenti_targa SET id_stazione_presunto_rientro='" & CInt(dropStazioneDropOff.SelectedValue) & "', data_presunto_rientro='" & data_presunto_rientro & "' " &
                     "WHERE num_riferimento='" & contratto_num.Text & "' AND num_crv_contratto='" & CInt(numCrv.Text) & "' AND id_veicolo='" & id_auto_selezionata.Text & "' AND movimento_attivo='1'"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub modifica_auto_cnt_prima_del_check_out()
        'AVENDO MODIFICATO LA TARGA ASSOCIATA AL CONTRATTO PRIMA DI EFFETTUARE CHECK OUT DEL VEICOLO DEVONO ESSERE SALVATI I DATI DEL VEICOLO NEL CONTRATTO
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim id_gruppo_da_consegnare As String
        If gruppoDaConsegnare.SelectedValue <> "0" Then
            id_gruppo_da_consegnare = "'" & CInt(gruppoDaConsegnare.SelectedValue) & "'"
        Else
            id_gruppo_da_consegnare = "NULL"
        End If

        Dim sqlStr As String = "UPDATE contratti SET id_gruppo_app=" & id_gruppo_da_consegnare & ", id_veicolo='" & id_auto_selezionata.Text & "'," &
            "targa='" & Replace(txtTarga.Text, "'", "''") & "'," &
            "serbatoio_max='" & lblSerbatoioMax.Text & "',modello='" & Replace(txtModello.Text, "'", "''") & "'," &
            "km_uscita='" & txtKm.Text & "', litri_uscita='" & Replace(txtSerbatoio.Text, ",", ".") & "',id_alimentazione='" & id_alimentazione.Text & "' " &
            " WHERE id='" & CInt(idContratto.Text) & "' AND attivo='1'"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub modifica_gps_cnt_prima_del_check_out()
        'AVENDO MODIFICATO IL GPS ASSOCIATO AL CONTRATTO PRIMA DI EFFETTUARE CHECK OUT DEL VEICOLO DEVONO ESSERE SALVATI I DATI DEL GPS NEL CONTRATTO
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim id_gps As String
        Dim cod_gps As String

        If div_gps.Visible = True And lblIdGps.Text <> "" Then
            id_gps = "'" & CInt(lblIdGps.Text) & "'"
            cod_gps = "'" & Replace(txtCodiceGps.Text, "'", "''") & "'"
        Else
            id_gps = "NULL"
            cod_gps = "NULL"
        End If

        Dim sqlStr As String = "UPDATE contratti SET id_gps=" & id_gps & ", codice_gps=" & cod_gps &
            " WHERE id='" & CInt(idContratto.Text) & "' AND attivo='1'"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()


        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub salva_contratto()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        'SALVATAGGIO DEI DATI DI CONTRATTO -------------------------------------------------------------------------------------------------
        Dim data_uscita As String = getDataDb_con_orario(txtDaData.Text & " " & ore1.Text & ":" & minuti1.Text & ":00")
        Dim data_rientro As String = getDataDb_con_orario(txtAData.Text & " " & ore2.Text & ":" & minuti2.Text & ":00")

        Dim id_gruppo_da_consegnare As String
        If gruppoDaConsegnare.SelectedValue <> "0" Then
            id_gruppo_da_consegnare = "'" & CInt(gruppoDaConsegnare.SelectedValue) & "'"
        Else
            id_gruppo_da_consegnare = "NULL"
        End If

        Dim id_tariffe_righe As Integer
        Dim id_tariffa As Integer
        Dim tipo_tariffa As String = ""
        Dim codice_tariffa As String

        If dropTariffeGeneriche.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
            codice_tariffa = dropTariffeGeneriche.SelectedItem.Text
            tipo_tariffa = "generica"
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
            codice_tariffa = dropTariffeParticolari.SelectedItem.Text
            tipo_tariffa = "fonte"
        End If

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_tariffa FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)
        id_tariffa = Cmd.ExecuteScalar

        'Cmd = New Data.SqlClient.SqlCommand("SELECT codice FROM tariffe WITH(NOLOCK) WHERE id='" & id_tariffa & "'", Dbc)
        'codice_tariffa = Cmd.ExecuteScalar

        'SALVATAGGIO--------------------------

        Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(MAX(num_contratto),0) FROM contratti WITH(NOLOCK) WHERE id_stazione_uscita='" & CInt(dropStazionePickUp.SelectedValue) & "'", Dbc)

        Dim num_contratto As Integer = Cmd.ExecuteScalar + 1

        If num_contratto = 1 Then
            'SE E' LA PRIMA PRENOTAZIONE DELLA STAZIONE RESTITUISCO IL PRIMO NUMERO
            num_contratto = dropStazionePickUp.SelectedValue & "0000001"
        End If

        contratto_num.Text = num_contratto

        Dim imp_a_carico_del_broker As String
        Dim giorni_to As String

        If a_carico_del_broker.Text <> "" Then
            If dropVariazioneACaricoDi.Text = "1" Then
                'SE E' A CARICO DEL BROKER
                a_carico_del_broker.Text = getCostoACaricoDelBroker()
                imp_a_carico_del_broker = "'" & Replace(a_carico_del_broker.Text, ",", ".") & "'"
            Else
                'SE E' A CARICO DEL CLIENTE IL COSTO A CARICO DEL BROKER E' QUELLO PRECEDENTE
                imp_a_carico_del_broker = "'" & Replace(a_carico_del_broker.Text, ",", ".") & "'"
            End If
            giorni_to = "'" & txtNumeroGiorniTO.Text & "'"
            lblGiorniToOld.Text = txtNumeroGiorniTO.Text
        Else
            imp_a_carico_del_broker = "NULL"
            giorni_to = "NULL"
        End If

        Dim ditta As String
        If idDitta.Text <> "" Then
            ditta = "'" & idDitta.Text & "'"
        Else
            ditta = "NULL"
        End If

        numCrv.Text = "0"

        Dim id_gps As String
        Dim cod_gps As String

        If div_gps.Visible = True And lblIdGps.Text <> "" Then
            id_gps = "'" & CInt(lblIdGps.Text) & "'"
            cod_gps = "'" & Replace(txtCodiceGps.Text, "'", "''") & "'"
        Else
            id_gps = "NULL"
            cod_gps = "NULL"
        End If

        Dim sqlStr As String = "UPDATE contratti SET num_contratto='" & num_contratto & "', num_calcolo='0', num_crv='0', "

        'SETTO IL CONTRATTO COME APERTO E DA PREAUTORIZZARE
        sqlStr = sqlStr & "status='1', attivo='1', id_stazione_presunto_rientro='" & CInt(dropStazioneDropOff.SelectedValue) & "'," &
        "data_uscita='" & data_uscita & "', data_presunto_rientro='" & data_rientro & "', id_gruppo_auto='" & CInt(gruppoDaCalcolare.SelectedValue) & "'," &
        "giorni='" & txtNumeroGiorni.Text & "', giorni_to='" & txtNumeroGiorniTO.Text & "', id_gruppo_app=" & id_gruppo_da_consegnare & ", id_primo_conducente='" & idPrimoConducente.Text & "',"

        If idSecondoConducente.Text <> "" Then
            sqlStr = sqlStr & "id_secondo_conducente='" & idSecondoConducente.Text & "', "
        End If

        sqlStr = sqlStr & "eta_primo_guidatore='" & txtEtaPrimo.Text & "', eta_secondo_guidatore='" & txtEtaSecondo.Text & "'," &
        "id_fonte='" & CInt(dropTipoCliente.SelectedValue) & "', codice_edp='" & txtCodiceEdp.Text & "', id_cliente=" & ditta & "," &
        "id_tariffa='" & id_tariffa & "', id_tariffe_righe='" & id_tariffe_righe & "', tipo_tariffa='" & tipo_tariffa & "'," &
        "sconto_applicato='" & Replace(txtSconto.Text, ",", ".") & "', tipo_sconto='" & dropTipoSconto.SelectedValue & "', sconto_su_rack='" & Replace(txtScontoRack.Text, ",", ".") & "'," &
        "id_gps=" & id_gps & ", codice_gps=" & cod_gps & "," &
        "codtar='" & Replace(codice_tariffa, "'", "''") & "', id_veicolo='" & id_auto_selezionata.Text & "', targa='" & Replace(txtTarga.Text, "'", "''") & "'," &
        "serbatoio_max='" & lblSerbatoioMax.Text & "',modello='" & Replace(txtModello.Text, "'", "''") & "'," &
        "km_uscita='" & txtKm.Text & "', litri_uscita='" & Replace(txtSerbatoio.Text, ",", ".") & "',id_alimentazione='" & id_alimentazione.Text & "'," &
        "note_contratto='" & Replace(txtNoteContratto.Text, "'", "''") & "'," &
        "data_creazione=GetDate(), importo_a_carico_del_broker=" & imp_a_carico_del_broker & ", "

        'SALVATAGGIO DEI DATI INIZIALI - IN CASO DI CONTRATTO DA PRENOTAZIONE E TARIFFA SCADUTA 
        ' I DATI INIZIALI SONO QUELLI DA PRENOTAZIONE, ALTRIMENTI CORRISPONDONO AI DATI DEL PRIMO SALVATAGGIO DEL CONTRATTO 
        If idPrenotazione.Text <> "" And Not tariffa_vendibile(id_tariffe_righe) Then
            sqlStr = sqlStr & " id_gruppo_auto_originale=id_gruppo_da_prenotazione, giorni_originale=giorni_noleggio_da_prenotazione," &
            "data_uscita_originale=data_uscita_da_prenotazione, data_presunto_rientro_originale=data_presunto_rientro_da_prenotazione, " &
            "id_stazione_presunto_rientro_originale=id_stazione_drop_off_da_prenotazione, "
            'SE I GIORNI DI NOLEGGIO SONO DIVERSI DA QUELLI DI PRENOTAZIONE OPPURE IL GRUPPO AUTO PRENOTATO E' STATO VARIATO ALLORA HO 
            'UTILIZZATO LA TARIFFA RACK: SALVO QUESTA INFORMAZIONE IN MODO DA POTER REPLICARE IL CALCOLO CORRETTAMENTE IN CASO DI MODIFICA
            'A NOLO IN CORSO. INOLTRE DA QUESTO MOMENTO IN POI VERRA' UTILIZZATA SEMPRE LA TARIFFA RACK ANCHE SE LA TARIFFA VIENE RESA
            'NUOVAMENTE DISPONIBILE (QUESTO PERCHE' ALTRIMENTI UNA MODIFICA A CONTRATTO IN CORSO NON DAREBBE GLI STESSI RISULTATI RISPETTO 
            'AI DATI SALVATI, AVENDO UTILIZZATO NUOVAMENTE LA TARIFFA ORIGINALE ANCHE PER I GIORNI DI NOLEGGIO O PER IL GRUPPO PER CUI ERA
            'STATA UTILIZZATA LA RACK).
            If (gruppoDaCalcolare.SelectedValue <> gruppo_da_calcolare_originale.Text) Or (CInt(txtNumeroGiorni.Text) > CInt(txtNumeroGiorniIniziali.Text)) Then
                sqlStr = sqlStr & " tariffa_rack_utilizzata='1'"
                rack_utilizzata.Text = "1"
            Else
                sqlStr = sqlStr & " tariffa_rack_utilizzata='0'"
                rack_utilizzata.Text = "0"
            End If
        Else
            sqlStr = sqlStr & " id_gruppo_auto_originale='" & CInt(gruppoDaCalcolare.SelectedValue) & "', giorni_originale='" & txtNumeroGiorni.Text & "'," &
            "data_uscita_originale='" & data_uscita & "', data_presunto_rientro_originale='" & data_rientro & "', " &
            "id_stazione_presunto_rientro_originale='" & CInt(dropStazioneDropOff.SelectedValue) & "', tariffa_rack_utilizzata='0'"

            rack_utilizzata.Text = "0"
        End If

        sqlStr = sqlStr & " WHERE id='" & CInt(idContratto.Text) & "'"

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        'SE SONO PRESENTI DELLE NOTE LE REGISTRO
        If Trim(txtNoteInterne.Text) <> "" Then
            Dim mia_nota As note = New note
            With mia_nota
                .id_documento = num_contratto
                .id_tipo = enum_note_tipo.note_contratto
                .nota = txtNoteInterne.Text
                .SalvaRecord()
            End With
        End If

        txtNoteInterne.Visible = False
        txtNoteContratto.ReadOnly = True
        intestazione_note.Visible = False
        gestione_note_contratto.Visible = True
        gestione_note_contratto.InitForm(enum_note_tipo.note_contratto, num_contratto, False, False, "Note - Uso Interno")

        'AGGIORNO IL NUMERO DI CALCOLO IN WARNING E IN COSTI

        Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET num_calcolo='0' WHERE id_documento='" & idContratto.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()
        Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_warning SET num_calcolo='0' WHERE id_documento='" & idContratto.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        lblNumContratto.Text = num_contratto
        numCalcolo.Text = "0"

        'IL CONTRATTO E' SALVATO E QUINDI GIA' ATTIVO. SETTO LO STATO COME "DA EFFETTUARE CHECK OUT"
        statoContratto.Text = "1"
        '----------------------------------------------------------------------------------------------------------------------------------
        'SALVATAGGIO DEI DATI DEL o DEI CONDUCENTI AL MOMENTO DEL CONTRATTO NELLA TABELLA contratti_conducenti ----------------------------
        Cmd = New Data.SqlClient.SqlCommand("SELECT * FROM conducenti WITH(NOLOCK) WHERE id_conducente='" & idPrimoConducente.Text & "'", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        sqlStr = ""

        If Rs.Read() Then
            sqlStr = "INSERT INTO contratti_conducenti (num_conducente, num_contratto, id_conducente, cognome, nome, indirizzo, city," &
            "id_comune_ares, provincia, cap, nazione, data_nascita, eta, luogo_nascita, codfis, patente, tipo_patente, scadenza_patente," &
            "rilasciata_il, luogo_emissione, altri_documenti, domicilio_locale, telefono, email, cell)"

            Dim id_comune_ares As String
            If (Rs("id_comune_ares") & "") <> "" Then
                id_comune_ares = "'" & Rs("id_comune_ares") & "'"
            Else
                id_comune_ares = "NULL"
            End If

            Dim id_nazione As String
            If (Rs("nazione") & "") <> "" Then
                id_nazione = "'" & Rs("nazione") & "'"
            Else
                id_nazione = "NULL"
            End If

            Dim data_nascita As String
            If (Rs("data_nascita") & "") <> "" Then
                data_nascita = "'" & funzioni_comuni.getDataDb_senza_orario(Rs("data_nascita")) & "'"
            Else
                data_nascita = "NULL"
            End If

            Dim scadenza_patente As String
            If (Rs("scadenza_patente") & "") <> "" Then
                scadenza_patente = "'" & funzioni_comuni.getDataDb_senza_orario(Rs("scadenza_patente")) & "'"
            Else
                scadenza_patente = "NULL"
            End If

            Dim rilasciata_il As String
            If (Rs("rilasciata_il") & "") <> "" Then
                rilasciata_il = "'" & funzioni_comuni.getDataDb_senza_orario(Rs("rilasciata_il")) & "'"
            Else
                rilasciata_il = "NULL"
            End If

            sqlStr = sqlStr & " VALUES (" &
            "1,'" & lblNumContratto.Text & "','" & idPrimoConducente.Text & "','" & Replace(Rs("cognome") & "", "'", "''") & "'," &
            "'" & Replace(Rs("nome") & "", "'", "''") & "','" & Replace(Rs("indirizzo") & "", "'", "''") & "','" & Replace(Rs("city") & "", "'", "''") & "'," &
            id_comune_ares & ",'" & Replace(Rs("provincia") & "", "'", "''") & "','" & Replace(Rs("cap") & "", "'", "''") & "'," &
            id_nazione & "," & data_nascita & ",'" & txtEtaPrimo.Text & "','" & Replace(Rs("luogo_nascita") & "", "'", "''") & "'," &
            "'" & Replace(Rs("codfis") & "", "'", "''") & "','" & Replace(Rs("patente") & "", "'", "''") & "','" & Replace(Rs("tipo_patente") & "", "'", "''") & "'," &
            scadenza_patente & "," & rilasciata_il & ",'" & Replace(Rs("luogo_emissione") & "", "'", "''") & "'," &
            "'" & Replace(Rs("altri_documenti") & "", "'", "''") & "','" & Replace(Rs("domicilio_locale") & "", "'", "''") & "'," &
            "'" & Replace(Rs("telefono") & "", "'", "''") & "','" & Replace(Rs("email") & "", "'", "''") & "'," &
            "'" & Replace(Rs("cell") & "", "'", "''") & "')"
        End If
        '----------------------------------------------------------------------------------------------------------------------------------

        Rs.Close()
        Rs = Nothing
        Dbc.Close()
        Dbc.Open()

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        'SECONDO CONDUCENTE, SE SPECIFICATO
        If idSecondoConducente.Text <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT * FROM conducenti WITH(NOLOCK) WHERE id_conducente='" & idSecondoConducente.Text & "'", Dbc)
            Rs = Cmd.ExecuteReader()

            sqlStr = ""

            If Rs.Read() Then
                sqlStr = "INSERT INTO contratti_conducenti (num_conducente, num_contratto, id_conducente, cognome, nome, indirizzo, city," &
                "id_comune_ares, provincia, cap, nazione, data_nascita, eta, luogo_nascita, codfis, patente, tipo_patente, scadenza_patente," &
                "rilasciata_il, luogo_emissione, altri_documenti, domicilio_locale, telefono, email, cell)"

                Dim id_comune_ares As String
                If (Rs("id_comune_ares") & "") <> "" Then
                    id_comune_ares = "'" & Rs("id_comune_ares") & "'"
                Else
                    id_comune_ares = "NULL"
                End If

                Dim id_nazione As String
                If (Rs("nazione") & "") <> "" Then
                    id_nazione = "'" & Rs("nazione") & "'"
                Else
                    id_nazione = "NULL"
                End If

                Dim data_nascita As String
                If (Rs("data_nascita") & "") <> "" Then
                    data_nascita = "'" & funzioni_comuni.getDataDb_senza_orario(Rs("data_nascita")) & "'"
                Else
                    data_nascita = "NULL"
                End If

                Dim scadenza_patente As String
                If (Rs("scadenza_patente") & "") <> "" Then
                    scadenza_patente = "'" & funzioni_comuni.getDataDb_senza_orario(Rs("scadenza_patente")) & "'"
                Else
                    scadenza_patente = "NULL"
                End If

                Dim rilasciata_il As String
                If (Rs("rilasciata_il") & "") <> "" Then
                    rilasciata_il = "'" & funzioni_comuni.getDataDb_senza_orario(Rs("rilasciata_il")) & "'"
                Else
                    rilasciata_il = "NULL"
                End If

                sqlStr = sqlStr & " VALUES (" &
                "2,'" & lblNumContratto.Text & "','" & idSecondoConducente.Text & "','" & Replace(Rs("cognome") & "", "'", "''") & "'," &
                "'" & Replace(Rs("nome") & "", "'", "''") & "','" & Replace(Rs("indirizzo") & "", "'", "''") & "','" & Replace(Rs("city") & "", "'", "''") & "'," &
                id_comune_ares & ",'" & Replace(Rs("provincia") & "", "'", "''") & "','" & Replace(Rs("cap") & "", "'", "''") & "'," &
                id_nazione & "," & data_nascita & ",'" & txtEtaPrimo.Text & "','" & Replace(Rs("luogo_nascita") & "", "'", "''") & "'," &
                "'" & Replace(Rs("codfis") & "", "'", "''") & "','" & Replace(Rs("patente") & "", "'", "''") & "','" & Replace(Rs("tipo_patente") & "", "'", "''") & "'," &
                scadenza_patente & "," & rilasciata_il & ",'" & Replace(Rs("luogo_emissione") & "", "'", "''") & "'," &
                "'" & Replace(Rs("altri_documenti") & "", "'", "''") & "','" & Replace(Rs("domicilio_locale") & "", "'", "''") & "'," &
                "'" & Replace(Rs("telefono") & "", "'", "''") & "','" & Replace(Rs("email") & "", "'", "''") & "'," &
                "'" & Replace(Rs("cell") & "", "'", "''") & "')"
            End If
            '----------------------------------------------------------------------------------------------------------------------------------            
            Rs.Close()
            Rs = Nothing
            Dbc.Close()
            Dbc.Open()

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()
        End If

        If idDitta.Text <> "" Then
            'SALVATAGGIO DEI DATI DELLA DITTA AL MOMENTO DEL CONTRATTO NELLA TABELLA contratti_ditte -------------------------------------------
            Cmd = New Data.SqlClient.SqlCommand("SELECT * FROM ditte WITH(NOLOCK) WHERE id_ditta='" & idDitta.Text & "'", Dbc)
            Rs = Cmd.ExecuteReader()

            sqlStr = ""

            If Rs.Read() Then
                Dim id_comune_ares As String
                If (Rs("id_comune_ares") & "") <> "" Then
                    id_comune_ares = "'" & Rs("id_comune_ares") & "'"
                Else
                    id_comune_ares = "NULL"
                End If

                Dim id_nazione As String
                If (Rs("nazione") & "") <> "" Then
                    id_nazione = "'" & Rs("nazione") & "'"
                Else
                    id_nazione = "NULL"
                End If

                Dim id_tipo_cliente As String
                If (Rs("id_tipo_cliente") & "") <> "" Then
                    id_tipo_cliente = "'" & Rs("id_tipo_cliente") & "'"
                Else
                    id_tipo_cliente = "NULL"
                End If

                Dim tipo_spedizione_fattura As String
                If (Rs("tipo_spedizione_fattura") & "") <> "" Then
                    tipo_spedizione_fattura = "'" & Rs("tipo_spedizione_fattura") & "'"
                Else
                    tipo_spedizione_fattura = "NULL"
                End If

                Dim invio_email As String
                If (Rs("invio_email") & "") <> "" Then
                    invio_email = "'" & Rs("invio_email") & "'"
                Else
                    invio_email = "NULL"
                End If

                Dim invio_email_cc As String
                If (Rs("invio_email_cc") & "") <> "" Then
                    invio_email_cc = "'" & Rs("invio_email_cc") & "'"
                Else
                    invio_email_cc = "NULL"
                End If

                Dim invio_email_statement As String
                If (Rs("invio_email_statement") & "") <> "" Then
                    invio_email_statement = "'" & Rs("invio_email_statement") & "'"
                Else
                    invio_email_statement = "NULL"
                End If

                sqlStr = "INSERT INTO contratti_ditte (num_contratto, id_ditta, codice_edp, id_tipo_cliente, rag_soc, PIva, NAZIONE," &
                "provincia, citta, id_comune_ares, indirizzo, cap, PIva_ESTERA, c_fis, fax, tel, tipo_spedizione_fattura," &
                "invio_email, email, invio_email_cc, email_cc, invio_email_statement, email_statement) VALUES (" &
                "'" & lblNumContratto.Text & "','" & idDitta.Text & "','" & Rs("CODICE EDP") & "'," & id_tipo_cliente & "," &
                "'" & Replace(Rs("rag_soc") & "", "'", "''") & "','" & Replace(Rs("PIva") & "", "'", "''") & "'," & id_nazione & "," &
                "'" & Replace(Rs("provincia") & "", "'", "''") & "','" & Replace(Rs("citta") & "", "'", "''") & "'," & id_comune_ares & "," &
                "'" & Replace(Rs("indirizzo") & "", "'", "''") & "','" & Replace(Rs("cap") & "", "'", "''") & "'," &
                "'" & Replace(Rs("PIva_ESTERA") & "", "'", "''") & "','" & Replace(Rs("c_fis") & "", "'", "''") & "'," &
                "'" & Replace(Rs("fax") & "", "'", "''") & "','" & Replace(Rs("tel") & "", "'", "''") & "'," & tipo_spedizione_fattura & "," &
                invio_email & ",'" & Replace(Rs("email") & "", "'", "''") & "'," & invio_email_cc & ",'" & Replace(Rs("email_cc") & "", "'", "''") & "'," &
                invio_email_statement & ",'" & Replace(Rs("email_statement") & "", "'", "''") & "')"

            End If
            Rs.Close()
            Rs = Nothing
            Dbc.Close()
            Dbc.Open()
        End If

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()
        '-----------------------------------------------------------------------------------------------------------------------------------
        'REGISTRAZIONE DEL MOVIMENTO DI NOLO IN CORSO PER IL VEICOLO -----------------------------------------------------------------------
        'sqlStr = "insert into movimenti_targa (num_riferimento, num_crv_contratto, id_veicolo, id_tipo_movimento, data_uscita, id_stazione_uscita, " & _
        '" km_uscita, serbatoio_uscita, id_operatore, data_registrazione, movimento_attivo) " & _
        '"VALUES" & _
        '" ('" & lblNumContratto.Text & "','" & numCrv.Text & "','" & id_auto_selezionata.Text & "','" & Costanti.idMovimentoNoleggio & "','" & data_uscita & "','" & dropStazionePickUp.SelectedValue & "'," & _
        '"'" & txtKm.Text & "','" & txtSerbatoio.Text & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "',GetDate(),'1')"

        'Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        'Cmd.ExecuteNonQuery()
        ''SETTAGGIO DEL VEICOLO COME NOLEGGIATO
        'sqlStr = "UPDATE veicoli SET disponibile_nolo='0', noleggiata='1' WHERE id='" & id_auto_selezionata.Text & "'"

        'Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        'Cmd.ExecuteNonQuery()
        '-------------------------------------------------------------------------------------------------------------------------------
        'SE IL CONTRATTO PROVIENE DA UNA PRENOTAZIONE, SETTO LA PRENOTAZIONE NELLO STATO '3' (TRASFORMATA IN CONTRATTO)------------------
        If idPrenotazione.Text <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("UPDATE prenotazioni SET status='3', num_contratto='" & lblNumContratto.Text & "' WHERE Nr_Pren='" & idPrenotazione.Text & "'", Dbc)
            Cmd.ExecuteNonQuery()
        End If
        '--------------------------------------------------------------------------------------------------------------------------------

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Function orario_uscita_corretto() As Boolean
        Dim data_uscita As DateTime = getDataDb_con_orario(txtDaData.Text & " " & ore1.Text & ":" & minuti1.Text & ":00")

        If Math.Abs(DateDiff(DateInterval.Minute, data_uscita, Now())) > Costanti.cnt_minuti_tolleranza_uscita Then
            orario_uscita_corretto = False
        Else
            orario_uscita_corretto = True
        End If
    End Function

    Protected Sub ricalcola_uscita_veicolo()
        'RICALCOLO CONSIDERANDO DATA E ORARIO E STAZIONE DI USCITA 
        modifica_contratto_duplica_righe_calcolo()

        txtDaData.Text = Format(Now(), "dd/MM/yyyy")

        ore1.Text = Hour(Now())
        minuti1.Text = Minute(Now())

        If Len(ore1.Text) = 1 Then
            ore1.Text = "0" & ore1.Text
        End If
        If Len(minuti1.Text) = 1 Then
            minuti1.Text = "0" & minuti1.Text
        End If

        txtoraPartenza.Text = ore1.Text & ":" & minuti1.Text

        'SE LA TARIFFA E' BROKER
        If tariffa_broker.Text = "1" Then
            lblVariazioneACarico.Visible = True
            dropVariazioneACaricoDi.Visible = True
            dropVariazioneACaricoDi.Enabled = True

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(estensioni_sempre_a_carico_del_broker,'0') FROM clienti_tipologia WHERE id='" & CInt(dropTipoCliente.SelectedValue) & "'", Dbc)

            Dim sempre_a_carico_del_broker As Boolean = Cmd.ExecuteScalar

            If sempre_a_carico_del_broker Then
                dropVariazioneACaricoDi.SelectedValue = "1"
                dropVariazioneACaricoDi.Enabled = False
            Else
                'IN FASE DI USCITA VEICOLO SE LE VARIAZIONI NON SONO SEMPRE A CARICO DEL BROKER ALLORA IL TUTTO DEVE ESSERE PAGATO DAL CLIENTE
                dropVariazioneACaricoDi.SelectedValue = "0"
                dropVariazioneACaricoDi.Enabled = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End If

        esegui_ricalcolo_nolo_in_corso("1") 'EFFETTUO IL RICALCOLO 
        salva_contratto_nolo_in_corso("2")
        registra_uscita_veicolo()
    End Sub

    Protected Function GpsDisponibile() As Boolean
        'DOPO AVERO COLLEGATO IL GPS AD UN CONTRATTO (PRIMA DEL SALVATAGGIO) CONTROLLO SE E', O MENO, ANCORA DISPONIBILE
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_gps_status FROM gps WITH(NOLOCK) WHERE id='" & CInt(lblIdGps.Text) & "'", Dbc)

        Dim stato As String = Cmd.ExecuteScalar & ""

        If stato = Costanti.stato_gps.in_parco Then
            GpsDisponibile = True
        Else
            GpsDisponibile = False
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function secondo_guidatore_a_scelta_selezionato() As Boolean
        'CONTROLLO SE COME ACCESSORIO A SCELTA IL SECONDO GUIDATORE E' SELEZIONATO - USATO PER IL CONTROLLO DELLA NECESSITA' DI SELEZIONARE IL SECONDO GUIDATORE

        secondo_guidatore_a_scelta_selezionato = False

        For i = 0 To listContrattiCosti.Items.Count - 1
            Dim id_elemento As Label = listContrattiCosti.Items(i).FindControl("id_elemento")
            If id_elemento.Text = Costanti.Id_Secondo_Guidatore Then
                Dim chkScegli As CheckBox = listContrattiCosti.Items(i).FindControl("chkScegli")

                If chkScegli.Checked And chkScegli.Visible Then
                    secondo_guidatore_a_scelta_selezionato = True
                End If
            End If
        Next
    End Function

    Protected Sub btnPagamento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPagamento.Click
        Dim stato_contratto As String = getStatoContratto()

        If (statoContratto.Text = "0" And stato_contratto = "0") Then

            If (secondo_guidatore_a_scelta_selezionato() And idSecondoConducente.Text = "") Then
                'CONTROLLO CHE, NEL CASO IN CUI L'UTENTE PROVENGA DA PREVENTIVI O PRENOTAZIONI CON LA SECONDA GUIDA SELEZIONATA, SIA STATO
                'SELEZIONATO IL SECONDO GUIDATORE DA ANAGRAFIA
                Libreria.genUserMsgBox(Me, "Attenzione: selezionare il secondo guidatore da anagrafica oppure eliminare l'accessorio 'Secondo guidatore'")
            ElseIf div_gps.Visible And lblIdGps.Text = "" Then
                'CONTROLLO SE' E' STATO SELEZIONATO IL GPS (QUALORA L'ACCESSORIO SIA STATO SCELTO - IN QUESTO CASO IL div_gps RISULTA VISIBILE)
                Libreria.genUserMsgBox(Me, "E' necessario selezionare un GPS prima di procedere.")
            ElseIf div_gps.Visible AndAlso Not GpsDisponibile() Then
                'DOPO AVERO COLLEGATO IL GPS AD UN CONTRATTO (PRIMA DEL SALVATAGGIO) CONTROLLO SE E', O MENO, ANCORA DISPONIBILE
                Libreria.genUserMsgBox(Me, "Attenzione: il GPS selezionato non è più disponibile. Selezionarne un'altro oppure rimuovere l'accessorio.")
            Else
                'PAGAMENTO PER UN CONTRATTO DA SALVARE (USCITA VEICOLO) - PRIMA SALVO IL CONTRATTO (GENERANDONE IL NUMERO) E POI PROCEDO CON LA 
                'PREAUTORIZZAZIONE
                salva_contratto()

                btnRicalcolaDaPrenotazione.Visible = False
                btnRicalcolaDaPreventivo.Visible = False
                btnGeneraContratto.Visible = True
                btnVoid.Visible = True

                btnAnnullaDocumento.Text = "Chiudi"

                pagamento_nuovo_contratto()
            End If
        ElseIf stato_contratto = "5" Then
            pagamento_nuovo_contratto()
        Else
            If tutti_check_in_effettuati() Then
                pagamento_nuovo_contratto()
            Else
                Libreria.genUserMsgBox(Me, "ATTEZIONE: è necessario effettuare il check in di tutte le vetture per poter chiudere il contratto.")
            End If
        End If
    End Sub

    Protected Function getTotaleDaPagare() As String
        Dim costo_scontato As Label
        Dim nome_costo As Label
        Dim importo_totale As Double = 0
        For i = 0 To listContrattiCosti.Items.Count - 1
            'NE APPROFITTO PER RECUPERARE IL COSTO DEL TOTALE CHE MI SERVIRA' DOPO
            nome_costo = listContrattiCosti.Items(i).FindControl("nome_costo")
            If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
                costo_scontato = listContrattiCosti.Items(i).FindControl("costo_scontato")
                importo_totale = CDbl(costo_scontato.Text)
            End If
        Next
        If txtPOS_TotIncassato.Text <> "" Then
            importo_totale = importo_totale - CDbl(txtPOS_TotIncassato.Text)
        End If
        getTotaleDaPagare = importo_totale
    End Function

    Protected Sub btnImmissioneInParco_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImmissioneInParco.Click
        If txtKm.Text = "" Then
            Libreria.genUserMsgBox(Me, "Specificare i km attuali del veicolo.")
        ElseIf txtSerbatoio.Text = "" Then
            Libreria.genUserMsgBox(Me, "Specificare l'attuale livello del serbatoio.")
        Else
            If CInt(txtSerbatoio.Text) <= CInt(lblSerbatoioMax.Text) Then
                'IMMISSIONE IN PARCO: PER PRIMA COSA SETTO I DATI NELLA TABELLA VEICOLI, POI REGISTRO IL MOVIMENTO DI IMMISSIONE IN PARCO

                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE veicoli SET id_stazione='" & CInt(dropStazionePickUp.SelectedValue) & "', km_attuali='" & txtKm.Text & "', serbatoio_attuale='" & txtSerbatoio.Text & "', disponibile_nolo='1' WHERE id='" & id_auto_selezionata.Text & "'", Dbc)
                Cmd.ExecuteNonQuery()

                'REGISTRO IL MOVIMENTO DI IMMISSIONE IN PARCO ----------------------------------------------------------------------------------
                Dim sqlStr As String = "insert into movimenti_targa (num_riferimento, id_veicolo, id_tipo_movimento, data_rientro, id_stazione_rientro, " &
                " km_rientro, serbatoio_rientro, id_operatore, data_registrazione, movimento_attivo) " &
                "VALUES" &
                " ('" & id_auto_selezionata.Text & "','" & id_auto_selezionata.Text & "','" & Costanti.IdImmissioneInParco & "',getDate(),'" & CInt(dropStazionePickUp.SelectedValue) & "','" & txtKm.Text & "', " &
                " '" & txtSerbatoio.Text & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "',getDate(),'0')"

                'Response.Write(sqlStr)
                'Response.End()

                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteNonQuery()
                '-------------------------------------------------------------------------------------------------------------------------------

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                scegli_targa()
            Else
                Libreria.genUserMsgBox(Me, "Attenzione: il serbatoio attuale non può essere superiore rispetto a quello massimo.")
            End If
        End If
    End Sub

    Protected Sub btnAggiungiExtra_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAggiungiExtra.Click
        If dropElementiExtra.SelectedValue <> "0" Then
            If funzioni_comuni.accessorioExtraNonAggiunto(CInt(dropElementiExtra.SelectedValue), CInt(gruppoDaCalcolare.SelectedValue), "", "", idContratto.Text, numCalcolo.Text) Then
                'PER EVITARE CASI PARTICOLARI CONTROLLO PRIMA DI AGGIUNGERE UN ACCESSORIO SE IN EFFETTI E' ACQUISTABILE A NOLO IN CORSO (QUESTO
                'SOLO SE IL CONTRATTO E' IN CORSO)

                If (statoContratto.Text <> "2") Or (statoContratto.Text = "2" And funzioni_comuni.accessorio_acquistabile_nolo_in_corso(CInt(dropElementiExtra.SelectedValue))) Then
                    'NEL CASO IN CUI L'ACCESSORIO VIENE ACQUISTATO A NOLO IN CORSO PASSO ALLA FUNZIONE IL NUMERO DI GIORNI DA CALCOLARE
                    'QUALORA L'ACCESSORIO SIA A COSTO GIORNALIERO. IN QUESTO CASO DEVONO ESSERE CALCOLATI SOLAMENTE I GIORNI RESTANTI
                    Dim giorni_restanti As String = ""
                    Dim data_agg As String = ""

                    If statoContratto.Text = "2" Then
                        'A NOLO IN CORSO DEVO CONTROLLARE SE, PER IL CALCOLO PRECEDENTE, L'ACCESSORIO ERA GIA' STATO AGGINTO IN MODO DA CALCOLARE
                        'I GIORNI RESTANTI DALL'ORARIO DI PRIMO INSERIMENTO
                        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        Dbc.Open()
                        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT data_aggiunta_nolo_in_corso FROM contratti_costi WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON contratti_Costi.id_elemento=condizioni_elementi.id WHERE contratti_costi.id_documento='" & idContratto.Text & "' AND contratti_costi.num_calcolo='" & CInt(numCalcolo.Text) - 1 & "' AND contratti_costi.selezionato='1' AND id_elemento='" & CInt(dropElementiExtra.SelectedValue) & "'", Dbc)
                        Dim data_aggiunta_nolo_in_corso As String = Cmd.ExecuteScalar & ""

                        Cmd.Dispose()
                        Cmd = Nothing
                        Dbc.Close()
                        Dbc.Dispose()
                        Dbc = Nothing

                        Dim id_tariffe_righe As String

                        If dropTariffeGeneriche.SelectedValue <> "0" Then
                            id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
                        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                            id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
                        End If

                        If data_aggiunta_nolo_in_corso = "" Then
                            'SE PER IL CALCOLO PRECEDENTE L'ACCESSORIO NON ERA STATO AGGIUNTO ALLORA LO AGGIUNGO CON DATA ATTUALE
                            giorni_restanti = getGiorniDiNoleggio(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()), txtAData.Text, Hour(Now()), Minute(Now()), ore2.Text, minuti2.Text, id_tariffe_righe)
                            data_agg = Now()
                        Else
                            'SE PER IL CALCOLO PRECEDENTE ERA STATO GIA' AGGIUNTO CONSIDERO LA DATA INIZIALE
                            giorni_restanti = getGiorniDiNoleggio(Day(data_aggiunta_nolo_in_corso) & "/" & Month(data_aggiunta_nolo_in_corso) & "/" & Year(data_aggiunta_nolo_in_corso), txtAData.Text, Hour(data_aggiunta_nolo_in_corso), Minute(data_aggiunta_nolo_in_corso), ore2.Text, minuti2.Text, id_tariffe_righe)
                            data_agg = data_aggiunta_nolo_in_corso
                        End If

                        If giorni_restanti = txtNumeroGiorni.Text Then
                            'NON EFFETTUO IL CALCOLO PARTICOLARE NEL CASO IN CUI I GIORNI DA CALCOLARE COINCIDANO COL NUMERO DI GIORNI DI NOLO
                            giorni_restanti = ""
                            data_agg = ""
                        End If
                    End If

                    nuovo_accessorio(CInt(dropElementiExtra.SelectedValue), CInt(gruppoDaCalcolare.SelectedValue), "ELEMENTO", txtEtaPrimo.Text, txtEtaSecondo.Text, giorni_restanti, data_agg)
                    'NEL CASO IN CUI L'ACCESSORIO EXTRA SIA IL GPS AGGIUNGO EVENTUALMENTE IL VAL
                    'SE E' STATO AGGINTO IL GPS AGGIUNGO EVENTUALMENTE IL VAL
                    If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
                        If funzioni_comuni.is_gps(CInt(dropElementiExtra.SelectedValue)) Then
                            nuovo_accessorio("", CInt(gruppoDaCalcolare.SelectedValue), "VAL_GPS", txtEtaPrimo.Text, txtEtaSecondo.Text, "", "")
                        End If
                    End If
                Else
                    'AGGIORNO GLI ELEMENTI EXTRA
                    Dim id_tariffe_righe As String = ""
                    If dropTariffeGeneriche.SelectedValue <> "0" Then
                        id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
                    ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                        id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
                    End If
                    sqlElementiExtra.SelectCommand = funzioni.getQueryElementiExtra(id_tariffe_righe, True)
                    dropElementiExtra.Items.Clear()
                    dropElementiExtra.Items.Add("Seleziona...")
                    dropElementiExtra.Items(0).Value = "0"
                    dropElementiExtra.DataBind()

                    Libreria.genUserMsgBox(Me, "Accessorio non acquistabile a nolo in corso")
                End If
            Else
                Libreria.genUserMsgBox(Me, "L'accessorio è già stato aggiunto.")
            End If
        End If

        'ultimo_gruppo.Text = ""
        listContrattiCosti.DataBind()

        aggiorna_informazioni_dopo_modifica_costi()
    End Sub

    Protected Function getIdPrenotazione(ByVal num_prenotazione As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT Nr_Pren FROM prenotazioni WITH(NOLOCK) WHERE NUMPREN='" & num_prenotazione & "' AND attiva='1'", Dbc)

        getIdPrenotazione = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub btnQuickCheckIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuickCheckIn.Click
        If getStatoContratto() = "2" Then
            quick_check_in()
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: non è più possibile effettuare il quick check in.")
        End If
    End Sub



    Protected Sub quick_check_in()
        'TRASFERISCO I DATI DI RIENTRO ATTUALI NELLA SEZIONE PRESUNTO RIENTRO CHE MOSTRO VISIBILE, PRIMA DI AGGIORNARLI CON QUELLI REALI DI RIENTRO
        riga_rientro.Visible = True
        dropStazioneRientroPresunto.DataBind()
        dropStazioneRientroPresunto.SelectedValue = dropStazioneDropOff.SelectedValue

        ore2_presunto.Text = ore2.Text
        minuti2_presunto.Text = minuti2.Text
        If Len(ore2_presunto.Text) = 1 Then
            ore2_presunto.Text = "0" & ore2_presunto.Text
        End If
        If Len(minuti2_presunto.Text) = 1 Then
            minuti2_presunto.Text = "0" & minuti2_presunto.Text
        End If
        txtADataPresunto.Text = txtAData.Text
        txtOraRientroPresunta.Text = ore2_presunto.Text & ":" & minuti2_presunto.Text
        '------------------------------------------------------------------------------------------------------------------------------------
        'RICALCOLO CONSIDERANDO DATA, ORARIO E STAZIONE DI RIENTRO 
        modifica_contratto_duplica_righe_calcolo()

        txtAData.Text = Format(Now(), "dd/MM/yyyy")

        ore2.Text = Hour(Now())
        minuti2.Text = Minute(Now())

        If Len(ore2.Text) = 1 Then
            ore2.Text = "0" & ore2.Text
        End If
        If Len(minuti2.Text) = 1 Then
            minuti2.Text = "0" & minuti2.Text
        End If

        txtOraRientro.Text = ore2.Text & ":" & minuti2.Text
        dropStazioneDropOff.SelectedValue = CInt(Request.Cookies("SicilyRentCar")("stazione"))

        'SE LA TARIFFA E' BROKER
        If tariffa_broker.Text = "1" Then
            'ATTENZIONE: SE LA TARIFFA E' BROKER E I GIORNI DI NOLEGGIO SONO DIMINUITI RISPETTO A QUELLI DI VOUCHER, DENTRO esegui_ricalcolo_nolo_in_corso("3")
            'VERRA' CONTROLLATO (DOPO AVER CALCOLATO I GIORNI DI NOLEGGIO) SE IL BROKER HA I GIORNI NON USUFRIUTI RIMBORSABILI O MENO - 
            'IN QUESTO CASO estensioni_sempre_a_carico_del_broker CALCOLATO IN QUESTO PUNTO NON AVRA' EFFETTO E VERRA' SOSTITUITO COL RISULTATO
            'DELLA NUOVA CONDIZIONE
            lblVariazioneACarico.Visible = True
            dropVariazioneACaricoDi.Enabled = True

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(estensioni_sempre_a_carico_del_broker,'0') FROM clienti_tipologia WHERE id='" & CInt(dropTipoCliente.SelectedValue) & "'", Dbc)

            Dim sempre_a_carico_del_broker As Boolean = Cmd.ExecuteScalar

            If sempre_a_carico_del_broker Then
                dropVariazioneACaricoDi.SelectedValue = "1"
                dropVariazioneACaricoDi.Enabled = False
            Else
                dropVariazioneACaricoDi.SelectedValue = "0"
                dropVariazioneACaricoDi.Enabled = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End If

        esegui_ricalcolo_nolo_in_corso("3")

        salva_contratto_nolo_in_corso("3")

        'VISIBILITA'
        btnVoid.Visible = False
        btnRicalcolaModificaContratto.Visible = False
        btnQuickCheckIn.Visible = False
        btnScegliTarga.Visible = False
        btnPagamento.Visible = False
        btnSalvaRientro.Visible = True
        riga_rientro_veicolo.Visible = True
        btnGeneraContrattoRientro.Visible = True
        btnCRV.Visible = False

        btnAnnullaQuickCheckIn.Visible = True
        If livello_accesso_annulla_quick.Text <> "3" Then
            btnAnnullaQuickCheckIn.Enabled = False
        End If
        listModifiche.DataBind()
        bt_Check_Out.Visible = False
    End Sub

    Protected Function tutti_check_in_effettuati() As Boolean
        tutti_check_in_effettuati = True

        If numCrv.Text <> "0" Then
            Dim check_in_effettuato As Label
            For i = 0 To listCrv.Items.Count - 1
                check_in_effettuato = listCrv.Items(i).FindControl("check_in_effettuato")
                If check_in_effettuato.Text = "False" Then
                    tutti_check_in_effettuati = False
                End If
            Next
        End If

    End Function

    Protected Sub btnGeneraContrattoRientro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGeneraContrattoRientro.Click
        If tutti_check_in_effettuati() Then
            stampa_contratto("rientro")
        Else
            Libreria.genUserMsgBox(Me, "ATTEZIONE: è necessario effettuare il check in di tutte le vetture per poter chiudere il contratto.")
        End If
    End Sub

    'Protected Sub btnSalvaRientro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvaRientro.Click
    '    Dim test As Integer
    '    Try
    '        test = CInt(txtKmRientro.Text)
    '    Catch ex As Exception
    '        txtKmRientro.Text = ""
    '    End Try
    '    Try
    '        test = CInt(txtSerbatoioRientro.Text)
    '    Catch ex As Exception
    '        txtSerbatoioRientro.Text = ""
    '    End Try

    '    If txtKmRientro.Text = "" And txtSerbatoioRientro.Text = "" Then
    '        Libreria.genUserMsgBox(Me, "Specificare i Km e il livello di rientro del serbatoio.")
    '    ElseIf txtKmRientro.Text = "" Then
    '        Libreria.genUserMsgBox(Me, "Specificare i Km di rientro del veicolo.")
    '    ElseIf txtSerbatoioRientro.Text = "" Then
    '        Libreria.genUserMsgBox(Me, "Specificare il livello di rientro del serbatoio")
    '    ElseIf CInt(txtKm.Text) >= CInt(txtKmRientro.Text) Then
    '        Libreria.genUserMsgBox(Me, "Attenzione: i KM di rientro devono essere superiori a quelli di uscita.")
    '    Else
    '        salva_rientro()

    '        btnSalvaRientro.Visible = False
    '        btnPagamento.Visible = True
    '        btnGeneraContrattoRientro.Visible = True

    '        txtSerbatoioRientro.ReadOnly = True
    '        txtKmRientro.ReadOnly = True
    '    End If
    'End Sub

    Protected Function get_servizio_rifornimento_tolleranza() As String
        get_servizio_rifornimento_tolleranza = ""
        Dim servizio_rifornimento_tolleranza As Label
        For i = 0 To listContrattiCosti.Items.Count - 1
            servizio_rifornimento_tolleranza = listContrattiCosti.Items(i).FindControl("servizio_rifornimento_tolleranza")
            If servizio_rifornimento_tolleranza.Text <> "" Then
                get_servizio_rifornimento_tolleranza = servizio_rifornimento_tolleranza.Text
            End If
        Next
    End Function

    Protected Function tariffa_km_limitati() As Boolean
        'LA TARIFFA E' A KM LIMITATI SE CONTIENE L'ACCESSORIO (FiNO A QUESTO MOMENTO INFORMATIVA) CON tipologia=KM_EXTRA
        listContrattiCosti.DataBind()

        tariffa_km_limitati = False

        Dim tipologia As Label
        For i = 0 To listContrattiCosti.Items.Count - 1
            tipologia = listContrattiCosti.Items(i).FindControl("tipologia")
            If tipologia.Text = "KM_EXTRA" Then
                tariffa_km_limitati = True
                Exit For
            End If
        Next
    End Function

    Protected Sub salva_rientro()
        Dim sconto As Double
        If dropTipoSconto.SelectedValue = "0" Then
            sconto = CDbl(txtSconto.Text)
        ElseIf dropTipoSconto.SelectedValue = "1" Then
            sconto = 0
        End If

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        'RECUPERO I VALORI DI KM E SERBATOIO DELL'AUTO
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT km_attuali FROM veicoli WITH(NOLOCK) WHERE id='" & id_auto_selezionata.Text & "'", Dbc)
        txtKmRientro.Text = Cmd.ExecuteScalar
        Cmd = New Data.SqlClient.SqlCommand("SELECT serbatoio_attuale FROM veicoli WITH(NOLOCK) WHERE id='" & id_auto_selezionata.Text & "'", Dbc)
        txtSerbatoioRientro.Text = Cmd.ExecuteScalar


        'SE IL CLIENTE NON HA ACQUISTATO IL PIENO CARBURANTE ALL'USCITA E SE ESISTE L'INFORMATIVA SERVIZIO RIFORNIMENTO 
        'E LA DIFFERENZA DI CARBURANTE TRA USCITA E RIENTRO E' SUPERIORE AL MARGINE DI TOLLERANZA SETTATO VIENE AGGIUNTO AL TOTALE IL SERVIZIO RIFORNIMENTO 
        'IN MANIERA AUTOMATICA E IL COSTO DEL RIFORNIMENTO. QUESTO SOLAMENTE SE L'AUTO ESCE COL PIENO, ALTRIMENTI IL SERVIZIO RIFORNIMENTO
        'NON VIENE ADDEBITATO
        If Not funzioni_comuni.pieno_carburante_selezionato(idContratto.Text, numCalcolo.Text) Then
            'SE LA FUNZIONE RESTITUISCE STRINGA VUOTA VUOL DIRE CHE NON E' STATO SPECIFICATO IL COSTO PER IL SERVIZIO RIFORNIMENTO DA AGGIUNGERE
            'AUTOMATICAMENTE
            Dim tolleranza As String = get_servizio_rifornimento_tolleranza()
            If tolleranza <> "" Then
                If CInt(txtSerbatoioRientro.Text) <= CInt(txtSerbatoio.Text) And (CInt(txtSerbatoio.Text) - CInt(txtSerbatoioRientro.Text) > tolleranza) And (CInt(txtSerbatoio.Text) = CInt(lblSerbatoioMax.Text)) Then
                    aggiungi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), funzioni_comuni.get_id_servizio_rifornimento(), "", "", "", False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                End If
            End If
            'IN  OGNI CASO VIENE ADDEBITATO IL COSTO DEL REFUEL (SE IL CLIENTE NON E' RIENTRATO COL PIENO, E COMUNQUE FINO AL LIVELLO DI USCITA
            'DEL VEICOLO
            If CInt(txtSerbatoioRientro.Text) < CInt(txtSerbatoio.Text) Then
                funzioni.addebita_refuel(idContratto.Text, numCalcolo.Text, CInt(dropStazionePickUp.SelectedValue), gruppoDaCalcolare.Text, id_alimentazione.Text, CInt(txtSerbatoio.Text) - CInt(txtSerbatoioRientro.Text), " Targa " & txtTarga.Text)
            End If

            listContrattiCosti.DataBind()
            aggiorna_informazioni_dopo_modifica_costi()
        End If

        If tariffa_km_limitati() Then
            'SE LA TARIFFA E' A KM LIMITATI AGGIUNGO EVENTUALMENTE IL COSTO DEI KM EXTRA
            'CALCOLO DEI KM TOTALI DI NOLO
            Dim km_percorsi As Integer
            If numCrv.Text = "0" Then
                'NESSUN CRV
                km_percorsi = CInt(txtKmRientro.Text) - CInt(txtKm.Text)
            Else
                km_percorsi = 0
                Dim km_uscita As Label
                Dim km_rientro As Label
                For i = 0 To listCrv.Items.Count - 2
                    km_uscita = listCrv.Items(i).FindControl("km_uscita")
                    km_rientro = listCrv.Items(i).FindControl("km_rientro")

                    km_percorsi = km_percorsi + CInt(km_rientro.Text) - CInt(km_uscita.Text)
                Next
                km_percorsi = km_percorsi + CInt(txtKmRientro.Text) - CInt(txtKm.Text)
            End If

            funzioni.addebita_km_extra(idContratto.Text, numCalcolo.Text, km_percorsi, txtNumeroGiorni.Text, sconto, dropStazionePickUp.SelectedValue)

            listContrattiCosti.DataBind()
            aggiorna_informazioni_dopo_modifica_costi()
        End If

        Dim totale_da_pagare As String = getTotaleDaPagare()

        '2 - IMPOSTO IL CONTRATTO COME CHIUSO
        'LO STATO DEL CONTRATTO E' 4 SE NON C'E' ALCUN PAGAMENTO EFFETTUATO
        'SE C'E' UN PAGAMENTO O IL CONTRATTO E' FULL CREDIT ALLORA LO STATO E' DIRETTAMENTE DA FATTUARARE (STATO 8)
        If full_credit.Text = "1" Then
            statoContratto.Text = "8"
        Else
            Dim sqlStr As String = "SELECT TOP 1 ID_CTR FROM PAGAMENTI_EXTRA WITH(NOLOCK) " &
            "WHERE N_CONTRATTO_RIF='" & lblNumContratto.Text & "' AND (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Chiusura & "' " &
            "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Vendita & "' " &
            "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Pagamento_Contanti & "' " &
            "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso & "') " &
            "AND ISNULL(operazione_stornata,'0')='0'"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim test As String = Cmd.ExecuteScalar & ""

            If test <> "" Then
                'C'E' UN PAGAMENTO - IL CONTRATTO E' DIRETTAMENTE DA FATTURARE
                statoContratto.Text = "8"

                riga_fatturazione.Visible = True
                btnDaFatturare.Text = "Non Fatturare"
                lblStatoFatturazione.Text = "Da Fatturare"

                btnGeneraFattura.OnClientClick = "javascript: return(window.confirm ('Attenzione - Sei sicuro di voler generare la fattura per questo contratto?'));"
            Else
                'SE NON C'E' UN PAGAMENTO MA IL TOTALE CHE IL CLIENTE DEVE PAGARE E' 0 (TOUR OPERATOR - PREPAGATI SENZA ADDEBBITI PER IL CLIENTE)
                'IL CONTRATTO E' DIRETTAMENRTE DA FATTURARE
                If CDbl(totale_da_pagare) = 0 Then
                    statoContratto.Text = "8"

                    riga_fatturazione.Visible = True
                    btnDaFatturare.Text = "Non Fatturare"
                    lblStatoFatturazione.Text = "Da Fatturare"

                    btnGeneraFattura.OnClientClick = "javascript: return(window.confirm ('Attenzione - Sei sicuro di voler generare la fattura per questo contratto?'));"
                Else
                    statoContratto.Text = "4"

                    riga_fatturazione.Visible = True
                    btnDaFatturare.Text = "Da Fatturare"
                    lblStatoFatturazione.Text = "Da Incassare"

                    btnGeneraFattura.OnClientClick = "javascript: return(window.confirm ('Attenzione - Sei sicuro di voler generare la fattura per questo contratto?'));"
                End If
            End If
        End If
        Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti SET status='" & statoContratto.Text & "', km_rientro='" & txtKmRientro.Text & "', litri_rientro='" & txtSerbatoioRientro.Text & "', totale_da_incassare='" & Replace(totale_da_pagare, ",", ".") & "' WHERE id='" & idContratto.Text & "'", Dbc)
        Cmd.ExecuteScalar()


        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub listModifiche_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listModifiche.ItemDataBound
        Dim numCalcoloLabel As Label = e.Item.FindControl("numCalcoloLabel")

        If numCalcoloLabel.Text = "1" Then
            Dim operatore As Label = e.Item.FindControl("operatore")
            operatore.Text = lblOperatoreCreazione.Text
            Dim dataOperazione_Label As Label = e.Item.FindControl("dataOperazione_Label")
            dataOperazione_Label.Text = lblDataContratto.Text
        End If

        If numCalcoloLabel.Text = numCalcolo.Text And statoContratto.Text = "2" Then
            'NASCONDO L'ULTIMO CALCOLO SOLO QUANDO LO STATO E' A NOLO IN CORSO
            e.Item.FindControl("vediCalcolo").Visible = False
        End If
    End Sub

    Protected Sub btnTrovaTarga_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTrovaTarga.Click
        txtTarga.Text = ""
        txtGruppo.Text = ""
        txtModello.Text = ""
        txtKm.Text = ""
        txtSerbatoio.Text = ""
        id_auto_selezionata.Text = ""
        auto_collegata.Text = ""
        id_alimentazione.Text = ""
        lblTipoSerbatoio.Text = ""

        btnImmissioneInParco.Visible = False
        riga_vedi_auto.Visible = False
        rifornimento.Visible = False
        pulizia.Visible = False

        id_rifornimento.Text = ""
        in_rifornimento.Text = ""

        id_pulizia.Text = ""
        in_pulizia.Text = ""

        If gruppoDaConsegnare.SelectedValue <> "0" Then
            id_scegli_auto_gruppo.Text = CInt(gruppoDaConsegnare.SelectedValue)
            id_scegli_auto_stazione.Text = CInt(dropStazionePickUp.SelectedValue)
        Else
            id_scegli_auto_gruppo.Text = CInt(gruppoDaCalcolare.SelectedValue)
            id_scegli_auto_stazione.Text = CInt(dropStazionePickUp.SelectedValue)
        End If

        listScegliVeicolo.DataBind()
        riga_vedi_auto.Visible = True

        If statoContratto.Text = "0" Then
            btnPagamento.Visible = False 'AVENDO DESELEZIONATO L'AUTO DI SICURO NON E' POSSIBILE SALVARE IL CONTRATTO
        ElseIf statoContratto.Text = "5" Then
            btnAnnullaSelezioneTargaCrv.Visible = False
            btnScegliTarga.Text = "Seleziona"
            txtTarga.ReadOnly = False
        End If

        txtTarga.Focus()
    End Sub

    Protected Sub listScegliVeicolo_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listScegliVeicolo.ItemCommand
        If e.CommandName = "scegli" Then
            txtTarga.Text = e.CommandArgument

            riga_vedi_auto.Visible = False
            id_scegli_auto_gruppo.Text = "0"
            id_scegli_auto_stazione.Text = "0"

            scegli_targa()
        ElseIf e.CommandName = "scegli_cambia_gruppo" Then
            Dim id_gruppo As Label = e.Item.FindControl("id_gruppo")
            gruppoDaConsegnare.SelectedValue = id_gruppo.Text

            If gruppoDaConsegnare.SelectedValue = gruppoDaCalcolare.SelectedValue Then
                gruppoDaConsegnare.SelectedValue = "0"
            End If

            txtTarga.Text = e.CommandArgument

            riga_vedi_auto.Visible = False
            id_scegli_auto_gruppo.Text = "0"
            id_scegli_auto_stazione.Text = "0"

            scegli_targa()
        End If
    End Sub

    Protected Sub listScegliVeicolo_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listScegliVeicolo.ItemDataBound
        Dim gruppoRegolare As String

        If gruppoDaConsegnare.SelectedValue <> "0" Then
            gruppoRegolare = gruppoDaConsegnare.SelectedValue
        Else
            gruppoRegolare = gruppoDaCalcolare.SelectedValue
        End If

        Dim id_gruppo As Label = e.Item.FindControl("id_gruppo")

        If id_gruppo.Text = gruppoRegolare Or statoContratto.Text = "5" Then
            e.Item.FindControl("btnScegli").Visible = True
        Else
            e.Item.FindControl("btnScegliAltroGruppo").Visible = True
        End If
    End Sub

    Protected Sub btnAnnullaDocumento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnullaDocumento.Click
        If statoContratto.Text = "0" Then
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM contratti_costi WHERE id_documento='" & idContratto.Text & "'", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd = New Data.SqlClient.SqlCommand("DELETE FROM contratti_warning WHERE id_documento='" & idContratto.Text & "'", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd = New Data.SqlClient.SqlCommand("DELETE FROM contratti WHERE id='" & idContratto.Text & "'", Dbc)

            Cmd.ExecuteNonQuery()



            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            Session("provenienza") = "contratti.aspx"
            Response.Redirect("preventivi.aspx")
        Else
            Session("provenienza") = "contratti.aspx"
            Response.Redirect("preventivi.aspx")
        End If
    End Sub

    Protected Sub bt_Check_Out_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_Check_Out.Click
        gestione_checkin.InitFormCheckOut(tipo_documento.Contratto, Integer.Parse(lblNumContratto.Text), Integer.Parse(numCrv.Text))
        div_edit_danno.Visible = True
        tab_contratto.Visible = False
    End Sub

    Protected Sub btnSalvaRientro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvaRientro.Click
        'Dim id_gruppo_danni_uscita As String = get_id_gruppo_danni_uscita(Integer.Parse(lblNumContratto.Text))
        'If id_gruppo_danni_uscita = "" Then
        '    Libreria.genUserMsgBox(Page, "Del documento corrente non è stato effettuato il Check Out.")
        '    Return
        'End If

        gestione_checkin.InitFormCheckIn(tipo_documento.Contratto, Integer.Parse(lblNumContratto.Text), Integer.Parse(numCrv.Text))
        tab_contratto.Visible = False
        div_edit_danno.Visible = True
    End Sub

    'Protected Function get_id_gruppo_danni_uscita(ByVal num_contratto As Integer) As String
    '    Dim sqlStr As String = "SELECT id_gruppo_danni_uscita FROM contratti" & _
    '        " WHERE num_contratto = " & num_contratto & _
    '        " AND attivo = 1"

    '    Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '            Dbc.Open()
    '            get_id_gruppo_danni_uscita = Cmd.ExecuteScalar() & ""
    '        End Using
    '    End Using
    'End Function


    Protected Sub listWarning_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listWarning.ItemDataBound
        Dim tipo As Label = e.Item.FindControl("tipo")
        If tipo.Text = "PICK INFO" Or tipo.Text = "DROP INFO" Then
            Dim warning As Label = e.Item.FindControl("warning")
            warning.ForeColor = Drawing.Color.Red 'Yellow
            warning.Font.Bold = True
        ElseIf tipo.Text = "PICK" Or tipo.Text = "DROP" Then
            Dim warning As Label = e.Item.FindControl("warning")
            warning.ForeColor = Drawing.Color.Red
            warning.Font.Bold = True
        End If
    End Sub

    Protected Sub annulla_quick_check_in()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM contratti_costi WHERE id_documento='" & idContratto.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM contratti_warning WHERE id_documento='" & idContratto.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM contratti WHERE id='" & idContratto.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM contratti WITH(NOLOCK) WHERE num_contratto='" & lblNumContratto.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "'", Dbc)
        Dim id_ctr As String = Cmd.ExecuteScalar

        Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti SET attivo='1' WHERE id='" & id_ctr & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        Session("carica_contratto") = id_ctr

        Response.Redirect("contratti.aspx")
    End Sub

    Protected Sub btnAnnullaQuickCheckIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnullaQuickCheckIn.Click
        If getStatoContratto() = "3" Then
            annulla_quick_check_in()
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: non è più possibile annullare il quick check in.")
        End If
    End Sub

    Protected Sub btnNoRifornimento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNoRifornimento.Click
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        'CONTROLLO CHE L'AUTO NON SIA ANCHE DA LAVARE - SE LO E' MODIFICO SOLO da_rifornire
        Dim sqlStr As String = "SELECT ISNULL(da_lavare,'0') FROM veicoli WHERE id ='" & id_auto_selezionata.Text & "'"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        Dim da_lavare As Boolean = Cmd.ExecuteScalar

        If da_lavare Then
            sqlStr = "UPDATE veicoli SET da_rifornire='0' " &
                     "where id ='" & id_auto_selezionata.Text & "'"
        Else
            sqlStr = "UPDATE veicoli SET disponibile_nolo='1', da_rifornire='0' " &
                     "where id ='" & id_auto_selezionata.Text & "'"
        End If

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()


        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        rifornimento.Visible = False
        id_rifornimento.Text = ""
        in_rifornimento.Text = ""

        scegli_targa()
    End Sub

    Protected Sub btnRegistraPieno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistraPieno.Click
        'CONTROLLO ANCHE CHE LA DATA FINALE SIA SUCCESSIVA A QUELLA INIZIALE
        Dim uscita As DateTime = funzioni_comuni.getDataDb_con_orario(txtRifornimentoUscita.Text & " 00:00:00")
        Dim DataOraRientro As String = funzioni_comuni.getDataDb_con_orario(txtRifornimentoRientro.Text & " " & Replace(txtOraRifornimentoRientro.Text, ".", ":") & ":00")

        Dim DataOraRientro_datetime As DateTime = funzioni_comuni.getDataDb_con_orario(txtRifornimentoRientro.Text & " " & Replace(txtOraRifornimentoRientro.Text, ".", ":") & ":00")

        Dim uscita_minuti As DateTime = funzioni_comuni.getDataDb_con_orario(txtRifornimentoUscita.Text & " " & Replace(txtOraRifornimentoUscita.Text, ".", ":") & ":00")
        Dim uscita_minutiString As String = funzioni_comuni.getDataDb_con_orario(txtRifornimentoUscita.Text & " " & Replace(txtOraRifornimentoUscita.Text, ".", ":") & ":00")
        Dim rientro As DateTime = funzioni_comuni.getDataDb_con_orario(txtRifornimentoRientro.Text & " 23:59:59")
        Dim oggi As DateTime = Now()

        If DateDiff(DateInterval.Minute, uscita_minuti, oggi) >= 0 Then
            If (DateDiff(DateInterval.Day, uscita, rientro) > 0) Or (DateDiff(DateInterval.Day, uscita, rientro) = 0 And Hour(txtOraRifornimentoRientro.Text) > Hour(txtOraRifornimentoUscita.Text)) Or (DateDiff(DateInterval.Day, uscita, rientro) = 0 And Hour(txtOraRifornimentoRientro.Text) = Hour(txtOraRifornimentoUscita.Text) And Minute(txtOraRifornimentoRientro.Text) > Minute(txtOraRifornimentoUscita.Text)) Then
                If CInt(txtKmRientroRifornimento.Text) > CInt(txtKm.Text) Then
                    If CDbl(txtLitriRiforniti.Text) <= CDbl(lblSerbatoioMax.Text) Then
                        If in_rifornimento.Text = "0" Then
                            'L'INTERO MOVIMENTO DI PIENO DEVE ESSERE REGISTRATO
                            Dim anno As String = Year(txtRifornimentoUscita.Text)
                            Dim numero_rifornimento As String = funzioni_comuni.getNumRifornimento(CInt(dropStazionePickUp.SelectedValue), anno)

                            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                            Dbc.Open()

                            Dim importo As String
                            If txtImportoRifornimento.Text = "" Then
                                importo = "NULL"
                            Else
                                importo = "'" & Replace(txtImportoRifornimento.Text, ",", ".") & "'"
                            End If

                            Dim sqlStr As String = "UPDATE rifornimenti SET anno_rifornimento='" & anno & "', num_rifornimento='" & numero_rifornimento & "', data_uscita_parco ='" & uscita_minutiString & "', km_out ='" & txtKm.Text & "', id_conducente ='" & CInt(DDLConducenti.SelectedValue) & "', id_stazione_out ='" & CInt(dropStazioneDropOff.SelectedValue) & "'," &
                                  "id_operatore_apertura='" & Request.Cookies("SicilyRentCar")("idUtente") & "', data_operazione_apertura=GetDate(), data_rientro_parco ='" & DataOraRientro & "', data_rifornimento='" & DataOraRientro & "', km_in ='" & txtKmRientroRifornimento.Text & "', litri_riforniti='" & Replace(txtLitriRiforniti.Text, ",", ".") & "', importo_rifornimento =" & importo & ", id_stazione_in ='" & CInt(dropStazionePickUp.SelectedValue) & "'," &
                                  "id_operatore_chiusura='" & Request.Cookies("SicilyRentCar")("idUtente") & "', data_operazione_chiusura=GetDate(), " &
                                  " registrato_in_contratto_id='" & idContratto.Text & "', costo_benzina_litro='" & Replace(getCostoCarburante_x_litro(CInt(dropStazionePickUp.SelectedValue), id_alimentazione.Text), ",", ".") & "', id_fornitore='" & CInt(DDLFornitore.SelectedValue) & "'" &
                                  " where id = '" & id_rifornimento.Text & "'"

                            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                            Cmd.ExecuteNonQuery()

                            'REGISTRAZIONE DEL MOVIMENTO DEL VEICOLO

                            'REGISTRATO_IN_CONTRATTO_IN: QUESTO CAMPO SERVE PER MEMORIZZARE CHE IL MOVIMENTO E' STATO CREATO ALL'INTERNO DI UN 
                            'CONTRATTO. IL CONTRATTO POTREBBE VENIRE ANNULLATO, E QUINDI L'ID SI RIFERIREBBE A NESSUNA RIGA: RICORDARSI DI 
                            'QUESTO ASPETTO E GESTIRLO QUANDO QUESTO CAMPO DOVREBBE ESSERE LETTO. RICORDARSI INOLTRE DI NON CREARE NEL DB
                            'IL COLLEGAMENTO TRA QUESTO CAMPO E LA TABELLA CONTRATTI.
                            sqlStr = "insert into movimenti_targa (num_riferimento,id_veicolo,id_tipo_movimento,data_uscita,id_stazione_uscita" &
                            ",km_uscita,serbatoio_uscita, movimento_attivo,data_registrazione, id_operatore," &
                            "data_rientro,data_registrazione_rientro, id_operatore_rientro, id_stazione_rientro, km_rientro, serbatoio_rientro) " &
                            "VALUES ('" & id_rifornimento.Text & "','" & id_auto_selezionata.Text & "','" & Costanti.idMovimentoRifornimento & "'," &
                            "'" & uscita_minutiString & "','" & CInt(dropStazioneDropOff.SelectedValue) & "','" & txtKm.Text & "','" & txtSerbatoio.Text & "'," &
                            "'0',GetDate(),'" & Request.Cookies("SicilyRentCar")("idUtente") & "','" & DataOraRientro & "',GetDate()," &
                            "'" & Request.Cookies("SicilyRentCar")("idUtente") & "', '" & CInt(dropStazionePickUp.SelectedValue) & "'," &
                            "'" & txtKmRientroRifornimento.Text & "','" & lblSerbatoioMax.Text & "' )"

                            'Response.Write(sqlStr)
                            'Response.End()

                            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                            Cmd.ExecuteNonQuery()


                            Dim disponibile_nolo As String
                            If id_pulizia.Text = "" Then
                                'IN QUESTO CASO L'AUTO NON E' DA PULIRE QUINDI SARA' SUBITO RESA DISPONIBILE
                                disponibile_nolo = "'1'"
                            Else
                                'IN QUESTO CASO L'AUTO E' ANCHE DA PULIRE
                                disponibile_nolo = "'0'"
                            End If

                            sqlStr = "UPDATE veicoli SET disponibile_nolo=" & disponibile_nolo & ", da_rifornire='0', km_attuali='" & txtKmRientroRifornimento.Text & "', serbatoio_attuale='" & lblSerbatoioMax.Text & "' " &
                                        "where id ='" & id_auto_selezionata.Text & "'"
                            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                            Cmd.ExecuteNonQuery()


                            Cmd.Dispose()
                            Cmd = Nothing
                            Dbc.Close()
                            Dbc.Dispose()
                            Dbc = Nothing

                            rifornimento.Visible = False
                            id_rifornimento.Text = ""
                            in_rifornimento.Text = ""

                            scegli_targa()

                            Libreria.genUserMsgBox(Me, "Pieno benzina registrato correttamente.")
                        ElseIf in_rifornimento.Text = "1" Then
                            'DEVE ESSERE REGITRATO IL SOLO RIENTRO DEL MOVIMENTO DI RIFORNIMENTO
                            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                            Dbc.Open()

                            Dim importo As String
                            If txtImportoRifornimento.Text = "" Then
                                importo = "NULL"
                            Else
                                importo = "'" & Replace(txtImportoRifornimento.Text, ",", ".") & "'"
                            End If

                            'CHISURA DELLA RIGA DI RIFORNIMENTO

                            'REGISTRATO_IN_CONTRATTO_IN: QUESTO CAMPO SERVE PER MEMORIZZARE CHE IL MOVIMENTO E' STATO CREATO ALL'INTERNO DI UN 
                            'CONTRATTO. IL CONTRATTO POTREBBE VENIRE ANNULLATO, E QUINDI L'ID SI RIFERIREBBE A NESSUNA RIGA: RICORDARSI DI 
                            'QUESTO ASPETTO E GESTIRLO QUANDO QUESTO CAMPO DOVREBBE ESSERE LETTO. RICORDARSI INOLTRE DI NON CREARE NEL DB
                            'IL COLLEGAMENTO TRA QUESTO CAMPO E LA TABELLA CONTRATTI.
                            Dim sqlStr As String = "update rifornimenti set data_rientro_parco ='" & DataOraRientro & "', km_in ='" & txtKmRientroRifornimento.Text & "', importo_rifornimento =" & importo & ", id_stazione_in ='" & CInt(dropStazionePickUp.SelectedValue) & "'," &
                               "id_operatore_chiusura='" & Request.Cookies("SicilyRentCar")("idUtente") & "', data_operazione_chiusura=GetDate(), " &
                               " registrato_in_contratto_id='" & idContratto.Text & "',litri_riforniti='" & Replace(txtLitriRiforniti.Text, ",", ".") & "', costo_benzina_litro='" & Replace(getCostoCarburante_x_litro(CInt(dropStazionePickUp.SelectedValue), id_alimentazione.Text), ",", ".") & "', id_fornitore='" & CInt(DDLFornitore.SelectedValue) & "'" &
                               " where id = '" & id_rifornimento.Text & "'"
                            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                            Cmd.ExecuteNonQuery()

                            'CHIUSURA DELLA RIGA DI MOVIMENTO VEICOLO
                            sqlStr = "UPDATE movimenti_targa SET data_rientro ='" & DataOraRientro & "', id_stazione_rientro = '" & CInt(dropStazionePickUp.SelectedValue) & "', km_rientro ='" & txtKmRientroRifornimento.Text & "', serbatoio_rientro ='" & lblSerbatoioMax.Text & "', movimento_attivo='0', " &
                                     "data_registrazione_rientro=GetDate(),id_operatore_rientro='" & Request.Cookies("SicilyRentCar")("idUtente") & "' " &
                                     "WHERE id_veicolo='" & id_auto_selezionata.Text & "' AND id_tipo_movimento='" & Costanti.idMovimentoRifornimento & "' AND movimento_attivo='1'"

                            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                            Cmd.ExecuteNonQuery()

                            Dim disponibile_nolo As String
                            If id_pulizia.Text = "" Then
                                'IN QUESTO CASO L'AUTO NON E' DA PULIRE QUINDI SARA' SUBITO RESA DISPONIBILE
                                disponibile_nolo = "'1'"
                            Else
                                'IN QUESTO CASO L'AUTO E' ANCHE DA PULIRE
                                disponibile_nolo = "'0'"
                            End If

                            sqlStr = "UPDATE veicoli SET disponibile_nolo=" & disponibile_nolo & ", da_rifornire='0', km_attuali='" & txtKmRientroRifornimento.Text & "', serbatoio_attuale='" & lblSerbatoioMax.Text & "' " &
                                        "where id ='" & id_auto_selezionata.Text & "'"
                            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                            Cmd.ExecuteNonQuery()

                            Cmd.Dispose()
                            Cmd = Nothing
                            Dbc.Close()
                            Dbc.Dispose()
                            Dbc = Nothing

                            rifornimento.Visible = False
                            id_rifornimento.Text = ""
                            in_rifornimento.Text = ""

                            scegli_targa()

                            Libreria.genUserMsgBox(Me, "Pieno benzina registrato correttamente.")
                        End If
                    Else
                        Libreria.genUserMsgBox(Me, "Attenzione: i litri riforniti non possono superare il serbatoio massimo.")
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Attenzione: i KM di rientro dal movimento di rifornimento devono essere superiori ai km di uscita.")
                End If
            Else
                Libreria.genUserMsgBox(Me, "Attenzione: la data di uscita del movimento di rifornimento non può essere successiva alla data di rientro del rifornimento.")
            End If
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: la data di uscita del movimento di rifornimento non può essere successiva alla data odierna.")
        End If
    End Sub

    Protected Sub bt_Gestione_RDS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_Gestione_RDS.Click
        gestione_checkin.InitFormRDS(tipo_documento.Contratto, Integer.Parse(lblNumContratto.Text), Integer.Parse(numCrv.Text))
        tab_contratto.Visible = False
        div_edit_danno.Visible = True
    End Sub

    Protected Sub listCrv_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listCrv.ItemDataBound
        Dim lbl_data_sostituzione As Label = e.Item.FindControl("lbl_data_sostituzione")
        'LA DATA SOSTITUZIONE E' ASSENTE PER IL VEICOLO ATTUALMENTE ATTIVO 

        Dim check_in_effettuato As Label = e.Item.FindControl("check_in_effettuato")
        Dim btnCheckIn As Button = e.Item.FindControl("btnCheckIn")
        Dim btnVediCheck As Button = e.Item.FindControl("btnVediCheck")
        Dim btnCheckOut As Button = e.Item.FindControl("btnCheckOut")
        Dim btnStampaCrv As Button = e.Item.FindControl("btnStampaCrv")

        If btnStampaCrv.Text = "" Then
            btnStampaCrv.Visible = False
        End If

        If lbl_data_sostituzione.Text <> "" Then
            'VECCHIO VEICOLO
            If check_in_effettuato.Text = "False" Then
                btnCheckIn.Visible = True
                btnCheckOut.Visible = True
                btnVediCheck.Visible = False
            Else
                btnCheckIn.Visible = False
                btnCheckOut.Visible = False
                btnVediCheck.Visible = True
            End If
        Else
            'VEICOLO ATTUALE
            btnCheckIn.Visible = False
            btnCheckOut.Visible = False
            btnVediCheck.Visible = False
        End If
    End Sub

    Protected Sub annulla_crv()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()


        '1) ELIMINO LA RIGA ATTUALE DI CRV NEL CASO IN CUI SIA IL PRIMO CRV
        Dim sqlStr As String
        If CInt(numCrv.Text) - 1 = 0 Then
            sqlStr = "DELETE FROM contratti_crv_veicoli WHERE  num_contratto='" & contratto_num.Text & "' AND num_crv='" & CInt(numCrv.Text) - 1 & "'"
        Else
            sqlStr = "UPDATE contratti_crv_veicoli SET data_sostituzione=NULL WHERE num_contratto='" & contratto_num.Text & "' AND num_crv='" & CInt(numCrv.Text) - 1 & "'"
        End If

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        '2) RIPRISTINO DEL NUMERO DI CALCOLO PRECEDENTE
        sqlStr = "DELETE FROM contratti_costi WHERE  id_documento='" & idContratto.Text & "' AND num_calcolo='" & numCalcolo.Text & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()
        sqlStr = "DELETE FROM contratti_warning WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & numCalcolo.Text & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()
        sqlStr = "DELETE FROM contratti WHERE id='" & idContratto.Text & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        sqlStr = "SELECT contratti.id, id_veicolo, id_alimentazione, targa, km_uscita, litri_uscita, serbatoio_max, modello, alimentazione.descrizione As alimentazione, alimentazione.cod_carb FROM contratti WITH(NOLOCK) INNER JOIN alimentazione ON contratti.id_alimentazione=alimentazione.id WHERE num_contratto='" & contratto_num.Text & "' AND num_crv='" & CInt(numCrv.Text) - 1 & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        If Rs.Read() Then
            idContratto.Text = Rs("id")
            id_auto_selezionata.Text = Rs("id_veicolo")
            id_alimentazione.Text = Rs("id_alimentazione")
            lblTipoSerbatoio.Text = Rs("cod_carb") & ""
            lblTipoSerbatoio.ToolTip = Rs("alimentazione") & ""
            txtTarga.Text = Rs("targa")
            txtKm.Text = Rs("km_uscita")
            txtSerbatoio.Text = Rs("litri_uscita")
            lblSerbatoioMax.Text = Rs("serbatoio_max")
            lblSerbatoioMaxRientro.Text = Rs("serbatoio_max")
            txtModello.Text = Rs("modello")

            If gruppoDaConsegnare.SelectedValue <> "0" Then
                txtGruppo.Text = gruppoDaConsegnare.SelectedItem.Text
                id_gruppo_auto_selezionata.Text = gruppoDaConsegnare.SelectedValue
            Else
                txtGruppo.Text = gruppoDaCalcolare.SelectedItem.Text
                id_gruppo_auto_selezionata.Text = gruppoDaCalcolare.SelectedValue
            End If

            txtTarga.ReadOnly = True
            btnScegliTarga.Text = "Modifica"
        End If

        Dbc.Close()
        Dbc.Open()

        sqlStr = "UPDATE contratti SET attivo='1' WHERE id='" & idContratto.Text & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        '3) ELIMINARE RIENTRO DA movimenti_targa
        sqlStr = "UPDATE movimenti_targa SET data_rientro=NULL, id_stazione_rientro=NULL, km_rientro=NULL, serbatoio_rientro=NULL," &
        "id_operatore_rientro=NULL, data_registrazione_rientro=NULL, movimento_attivo='1' " &
        "WHERE num_riferimento='" & contratto_num.Text & "' AND num_crv_contratto='" & CInt(numCrv.Text) - 1 & "' AND id_veicolo='" & id_auto_selezionata.Text & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()


        numCrv.Text = CInt(numCrv.Text) - 1
        lblCrv.Text = " - CRV " & numCrv.Text
        numCalcolo.Text = CInt(numCalcolo.Text) - 1

        statoContratto.Text = "2"

        ripristina_campi_dopo_crv()

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub salva_crv()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String

        'SE E' IL PRIMO CRV LA DATA DI USCITA DEL VEICOLO CORRISPONDE ALLA DATA DI USCITA DEL CONTRATTO - INSERISCO PER LA PRIMA VOLTA
        'UNA RIGA IN CONTRATTI_CRV_VEICOLI
        Dim data_uscita As String
        If numCrv.Text = "0" Then
            data_uscita = getDataDb_con_orario(txtDaData.Text & " " & ore1.Text & ":" & minuti1.Text & ":00")
            sqlStr = "INSERT INTO contratti_crv_veicoli (num_contratto, num_crv, id_veicolo, data_uscita, data_sostituzione, " &
            "km_uscita, serbatoio_uscita, check_in_effettuato) VALUES (" &
            "'" & lblNumContratto.Text & "','" & CInt(numCrv.Text) & "','" & id_auto_selezionata.Text & "','" & data_uscita & "'," &
            "GetDate(),'" & txtKm.Text & "','" & txtSerbatoio.Text & "','0')"
        Else
            'IN QUESTO CASO IL VEICOLO ATTUALE SI TROVA GIA' NELLA TABELLA - E' NECESSARIO SALVARE SOLO LA DATA DI SOSTITUZIONE
            sqlStr = "UPDATE contratti_crv_veicoli SET data_sostituzione=GetDate() WHERE num_contratto='" & lblNumContratto.Text & "' AND num_crv='" & numCrv.Text & "' AND id_veicolo='" & id_auto_selezionata.Text & "'"
        End If

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        'MODIFICA DEL CONTRATTO - STATUS='5' E RIMOZIONE DATI DEL VEICOLO
        modifica_contratto_duplica_righe_calcolo()

        salva_contratto_nolo_in_corso("5")

        'CHIUSURA PARZIALE DEL MOVIMENTO DI RIENTRO - SALVO TUTTO TRANNE I KM E I LITRI DI RIENTRO. SARANNO SPECIFICATI EFFETTUANDO IL CHECK IN
        sqlStr = "UPDATE movimenti_targa SET data_rientro=GetDate(), id_stazione_rientro='" & Request.Cookies("SicilyRentCar")("stazione") & "', " &
        "id_operatore_rientro='" & Request.Cookies("SicilyRentCar")("idUtente") & "', data_registrazione_rientro=GetDate(), movimento_attivo='0' " &
        "WHERE num_riferimento='" & lblNumContratto.Text & "' AND id_veicolo='" & id_auto_selezionata.Text & "' AND id_tipo_movimento='" & Costanti.idMovimentoNoleggio & "' AND movimento_attivo='1'"

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        'ELIMINO IL COLLEGAMENTO ALL'AUTO PRECEDENTE---------------------------
        id_auto_selezionata.Text = ""
        lblSerbatoioMax.Text = ""
        id_alimentazione.Text = ""
        lblTipoSerbatoio.Text = ""
        txtTarga.Text = ""
        txtGruppo.Text = ""
        txtModello.Text = ""
        txtKm.Text = ""
        txtSerbatoio.Text = ""
        '----------------------------------------------------------------------
        'NASCONDO I PULANTI NON PIU' DISPONIBILI IN QUESTO STATO --------------
        btnVoid.Visible = False
        btnQuickCheckIn.Visible = False
        btnRicalcolaModificaContratto.Visible = False
        btnGeneraContratto.Visible = False
        bt_Check_Out.Visible = False
        btnCRV.Visible = False
        btnAnnullaCRV.Visible = True
        txtTarga.ReadOnly = False
        btnTrovaTarga.Visible = True
        btnScegliTarga.Text = "Seleziona"
        '----------------------------------------------------------------------

        numCrv.Text = CInt(numCrv.Text) + 1

        lblCrv.Text = " - CRV " & numCrv.Text

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Function crvAnnullabile() As Boolean
        'IL CRV E' ANNULABILE SE NON E' ANCORA STATO EFFETTUATO IL CHECK IN DEL VEICOLO
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "SELECT ISNULL(check_in_effettuato,'1') FROM contratti_crv_veicoli WHERE num_contratto='" & contratto_num.Text & "' AND num_crv='" & CInt(numCrv.Text - 1) & "'"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Dim test As Boolean = Cmd.ExecuteScalar()

        If test Then
            crvAnnullabile = False
        Else
            crvAnnullabile = True
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub btnAnnullaCRV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnullaCRV.Click
        If getStatoContratto() = "5" And numCrv.Text = getNumCrvAttuale() Then
            If crvAnnullabile() Then
                annulla_crv()
                If numCrv.Text = "0" Then
                    div_crv.Visible = False
                End If
                listCrv.DataBind()
            Else
                Libreria.genUserMsgBox(Me, "Attenzione: non è più possibile annullare il CRV in quanto è stato già effettuato il check in del veicolo. E' possibile comunque selezionare nuovamente la stessa vettura.")
            End If
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: non è più possibile annullare il CRV.")
        End If
    End Sub

    Protected Sub btnCRV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCRV.Click
        If getStatoContratto() = "2" Then
            'SALVATAGGIO DEL CRV
            salva_crv()

            div_crv.Visible = True
            listCrv.DataBind()
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: non è più possibile effettuare un CRV per questo contratto.")
        End If
    End Sub

    Protected Sub listCrv_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listCrv.ItemCommand
        If e.CommandName = "checkIn" Then
            'CHECK PER UNO DEI VEICOLI IN LISTA
            Dim lblNumCrv As Label = e.Item.FindControl("lblNumCrv")
            If lblNumCrv.Text = "" Then
                lblNumCrv.Text = "0"
            End If

            gestione_checkin.InitFormCheckIn(tipo_documento.Contratto, Integer.Parse(lblNumContratto.Text), Integer.Parse(lblNumCrv.Text))
            tab_contratto.Visible = False
            div_edit_danno.Visible = True
        ElseIf e.CommandName = "checkOut" Then
            Dim lblNumCrv As Label = e.Item.FindControl("lblNumCrv")
            If lblNumCrv.Text = "" Then
                lblNumCrv.Text = "0"
            End If
            gestione_checkin.InitFormCheckOut(tipo_documento.Contratto, Integer.Parse(lblNumContratto.Text), Integer.Parse(lblNumCrv.Text))
            div_edit_danno.Visible = True
            tab_contratto.Visible = False
        ElseIf e.CommandName = "vediCheck" Then
            Dim lblNumCrv As Label = e.Item.FindControl("lblNumCrv")
            If lblNumCrv.Text = "" Then
                lblNumCrv.Text = "0"
            End If

            gestione_checkin.InitFormCheckIn(tipo_documento.Contratto, Integer.Parse(lblNumContratto.Text), Integer.Parse(lblNumCrv.Text))
            tab_contratto.Visible = False
            div_edit_danno.Visible = True
        ElseIf e.CommandName = "stampa_crv" Then
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                Session("url_print") = "/stampe/contratti/stampa_crv.aspx?orientamento=verticale&contratto=" & lblNumContratto.Text & "&num_crv=" & e.CommandArgument
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                End If
            End If
        End If
    End Sub

    Protected Sub memorizza_veicolo_crv()
        'IL VEICOLO VIENE MEMORIZZATO NELLA RIGA ATTUALE DI CONTRATTO - DOPO QUESTA OPERAZIONE IL CONTRATTO RITORNA NELLO STATO 2
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "UPDATE contratti SET id_veicolo='" & id_auto_selezionata.Text & "', serbatoio_max='" & txtSerbatoio.Text & "', " &
        "id_alimentazione='" & id_alimentazione.Text & "', targa='" & Replace(txtTarga.Text, "'", "''") & "'," &
        "modello='" & Replace(txtModello.Text, "'", "''") & "', km_uscita='" & txtKm.Text & "', litri_uscita='" & txtSerbatoio.Text & "', " &
        "status='2' WHERE id='" & idContratto.Text & "'"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        'INSERISCO LA RIGA DEL VEICOLO APPENA SOSTITUITO IN contratti_crv_veicoli
        sqlStr = "INSERT INTO contratti_crv_veicoli (num_contratto, num_crv, id_veicolo, data_uscita, data_sostituzione, " &
                 "km_uscita, serbatoio_uscita, check_in_effettuato) VALUES (" &
                 "'" & lblNumContratto.Text & "','" & numCrv.Text & "','" & id_auto_selezionata.Text & "',GetDate()," &
                 "NULL,'" & txtKm.Text & "','" & txtSerbatoio.Text & "','0')"

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        'REGISTRAZIONE DEL MOVIMENTO DI NOLO IN CORSO PER IL VEICOLO -----------------------------------------------------------------------
        sqlStr = "insert into movimenti_targa (num_riferimento, num_crv_contratto, id_veicolo, id_tipo_movimento, data_uscita, id_stazione_uscita, " &
        " km_uscita, serbatoio_uscita, id_operatore, data_registrazione, movimento_attivo, id_stazione_presunto_rientro, data_presunto_rientro) " &
        "VALUES" &
        " ('" & lblNumContratto.Text & "','" & numCrv.Text & "','" & id_auto_selezionata.Text & "','" & Costanti.idMovimentoNoleggio & "',GetDate(),'" & Request.Cookies("SicilyRentCar")("stazione") & "'," &
        "'" & txtKm.Text & "','" & txtSerbatoio.Text & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "',GetDate(),'1','" & dropStazioneDropOff.SelectedValue & "','" & funzioni_comuni.getDataDb_con_orario(txtAData.Text & " " & txtOraRientro.Text) & "')"

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()
        'SETTAGGIO DEL VEICOLO COME NOLEGGIATO
        sqlStr = "UPDATE veicoli SET disponibile_nolo='0', noleggiata='1' WHERE id='" & id_auto_selezionata.Text & "'"

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        listCrv.DataBind()

        ripristina_campi_dopo_crv()

        Libreria.genUserMsgBox(Me, "Salvataggio effettuato correttamente.")

        'CHECK OUT DEL VEICOLO SELEZIONATO
        gestione_checkin.InitFormCheckOut(tipo_documento.Contratto, Integer.Parse(lblNumContratto.Text), Integer.Parse(numCrv.Text))
        div_edit_danno.Visible = True
        tab_contratto.Visible = False

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub ripristina_campi_dopo_crv()
        btnQuickCheckIn.Visible = True
        btnRicalcolaModificaContratto.Visible = True
        btnGeneraContratto.Visible = True
        bt_Check_Out.Visible = True
        btnCRV.Visible = True
        btnAnnullaCRV.Visible = False
        btnScegliTarga.Text = "Modifica"
        statoContratto.Text = "2"
        btnAnnullaSelezioneTargaCrv.Visible = False
        btnTrovaTarga.Visible = False
    End Sub

    Protected Sub btnAnnullaSelezioneTargaCrv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnullaSelezioneTargaCrv.Click
        id_auto_selezionata.Text = ""
        txtTarga.Text = ""
        txtGruppo.Text = ""
        txtModello.Text = ""
        txtKm.Text = ""
        txtSerbatoio.Text = ""
        lblSerbatoioMax.Text = ""
        lblSerbatoioMaxRientro.Text = ""
        txtTarga.ReadOnly = False
        txtModello.Text = ""
        txtGruppo.Text = ""
        id_gruppo_auto_selezionata.Text = ""
        id_alimentazione.Text = ""
        lblTipoSerbatoio.Text = ""

        btnAnnullaSelezioneTargaCrv.Visible = False
        btnScegliTarga.Text = "Seleziona"
    End Sub

    Protected Function get_id_gruppo_danni_rientro(ByVal idCnt As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_gruppo_danni_rientro FROM contratti WHERE id='" & idCnt & "'", Dbc)

        get_id_gruppo_danni_rientro = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub addebita_informative()
        Dim mio_gruppo_evento As veicoli_gruppo_evento = veicoli_gruppo_evento.getRecordDaId(get_id_gruppo_danni_rientro(idContratto.Text))
        If Not mio_gruppo_evento.id_evento Is Nothing Then
            'SELEZIONO, TRA TUTTI I DANNI SALVATI, GLI ACCESSORI E LE DOTAZIONI MANCANTI - SOLAMENTE SE PER QUESTI E' STATO SPECIFICATO
            'LA RELATIVA PENALITA'.
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            'UTILIZZO LA CLAUSOLA DISTINCT per evitare che più elementi puntino allo stesso da addebitare - questo modo non devo fare alcun
            'controllo se ogni elemento e' già stato addebitato o meno

            Dim sqlStr As String = "(SELECT DISTINCT condizioni_elementi.id_elemento_da_addebitare_se_perso As id_elemento FROM veicoli_danni WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) " &
            "ON veicoli_danni.id_acessori=condizioni_elementi.id WHERE veicoli_danni.id_evento_apertura='" & mio_gruppo_evento.id_evento & "' " &
            "AND veicoli_danni.attivo='1' AND veicoli_danni.tipo_record='5' AND condizioni_elementi.accessorio_check='1' " &
            "AND NOT condizioni_elementi.id_elemento_da_addebitare_se_perso IS NULL) UNION " &
            "(SELECT DISTINCT accessori.id_elemento_da_addebitare_se_perso As id_elemento FROM veicoli_danni WITH(NOLOCK) INNER JOIN accessori WITH(NOLOCK) " &
            "ON veicoli_danni.id_dotazione=accessori.id WHERE veicoli_danni.id_evento_apertura='" & mio_gruppo_evento.id_evento & "' " &
            "AND veicoli_danni.attivo='1' AND veicoli_danni.tipo_record='4' " &
            "AND NOT accessori.id_elemento_da_addebitare_se_perso IS NULL)"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            'IL COSTO VIENE ADDEBITATO SOLAMENTE SE NELLA TARIFFA ATTUALE E' STATO SPECIFICATO E SE E' UN'INFORMATIVA

            Dim insieme_elementi As Collection = New Collection

            Do While Rs.Read()
                insieme_elementi.Add(Rs("id_elemento"), Rs("id_elemento"))
            Loop

            'CERCO TRA LE INFORMATIVE SE VE NE SONO DA ADDEBITARE

            Dim id_metodo_stampa As Label
            Dim id_elemento As Label
            Dim chkOldScegli As CheckBox

            For i = 0 To listContrattiCosti.Items.Count - 1
                id_metodo_stampa = listContrattiCosti.Items(i).FindControl("id_metodo_stampa")
                If id_metodo_stampa.Text = Costanti.id_stampa_informativa_con_valore Or id_metodo_stampa.Text = Costanti.id_stampa_informativa_senza_valore Then
                    id_elemento = listContrattiCosti.Items(i).FindControl("id_elemento")

                    If insieme_elementi.Contains(id_elemento.Text) Then
                        chkOldScegli = listContrattiCosti.Items(i).FindControl("chkOldScegli")
                        If Not chkOldScegli.Checked Then
                            aggiungi_costo_accessorio("", "", "", idContratto.Text, numCalcolo.Text, CInt(gruppoDaCalcolare.SelectedValue), id_elemento.Text, "", "", "", False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                        End If
                    End If
                End If
            Next

            listContrattiCosti.DataBind()

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
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
                id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
            ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
            End If
            Dim tariffa_broker As Boolean = is_tariffa_broker(id_tariffe_righe)

            If tariffa_broker Then
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzareValoreTariffaBroker) <> "1" Then
                    Dim id_tempo_km As String = funzioni_comuni.getIdTempoKm(id_tariffe_righe)

                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        Session("url_print") = "/stampe/stampa_tempo_km.aspx?pagina=verticale&id_tempo_km=" & id_tempo_km
                        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                        End If
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Non hai le autorizzazioni per visualizzare i Valori Tariffa per le Tariffe Broker.")
                End If
            Else
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzareValoreTariffa) <> "1" Then
                    Dim id_tempo_km As String = funzioni_comuni.getIdTempoKm(id_tariffe_righe)

                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        Session("url_print") = "/stampe/stampa_tempo_km.aspx?pagina=verticale&id_tempo_km=" & id_tempo_km
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
                id_tariffe_righe = CInt(dropTariffeGeneriche.SelectedValue)
            ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                id_tariffe_righe = CInt(dropTariffeParticolari.SelectedValue)
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

    Protected Sub visibilita_void()
        btnRicalcolaDaPrenotazione.Visible = False
        btnRicalcolaDaPreventivo.Visible = False
        btnScegliPrimoGuidatore.Visible = False
        btnScegliSecondoConducente.Visible = False
        btnScegliDitta.Visible = False
        btnScegliTarga.Visible = False
        btnTrovaTarga.Visible = False
        btnAggiungiExtra.Visible = False
        btnVoid.Visible = False
        btnGeneraContratto.Visible = False
        btnGeneraContrattoRientro.Visible = False
        btnAnnullaDocumento.Text = "Chiudi"
        btnPagamento.Visible = True
        riga_fatturazione.Visible = False
        'GPS
        btnCercaGps.Visible = False
    End Sub

    Protected Sub visibilita_nolo_concluso_admin()
        txtOraRientro.Enabled = False
        txtAData.Enabled = False

        gruppoDaCalcolare.Enabled = False

        btnGeneraContratto.Visible = True
        btnPagamento.Visible = True

        btnAnnullaModificheAdmin.Visible = False
        btnSalvaModifiche.Visible = False

        btnAggiungiExtra.Visible = False

        txtSconto.Enabled = False
        dropTipoSconto.Enabled = False
        txtScontoRack.Enabled = False

        chkAbbuonaGiornoExtra.Enabled = False

        btnAnnullaDocumento.Visible = True

        btnModificaTariffaAdmin.Visible = False
        dropTariffeGeneriche.Enabled = False
        dropTariffeParticolari.Enabled = False

        bt_Check_Out.Visible = True

        btnGeneraContratto.Visible = True
        btnGeneraContrattoRientro.Visible = True

        btnDaFatturare.Visible = True

        If statoContratto.Text = "8" Then
            btnGeneraFattura.Visible = True
            lblDataFattura.Visible = True
            txtDataFattura.Visible = True
            lblNumFattura.Visible = True
            txtNumFattura.Visible = True
        End If

        btnModificaAdmin.Text = "Modifica Admin"
        statoModificaContratto.Text = "0"

        dropVariazioneACaricoDi.Enabled = False

        btnVoid.Visible = True

        txtNoteContratto.ReadOnly = True
    End Sub

    Protected Sub setta_contratto_void()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "UPDATE contratti SET status='7', id_operatore_annullamento='" & Request.Cookies("SicilyRentCar")("IdUtente") & "'," &
        "data_annullamento=GetDate() WHERE id='" & idContratto.Text & "'"
        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        statoContratto.Text = "7"

        listContrattiCosti.DataBind()

        'SE IL CONTRATTO PROVIENE DA UNA PRENOTAZIONE, SETTO LA PRENOTAZIONE NELLO STATO ATTIVA -----------------------------------------
        If idPrenotazione.Text <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("UPDATE prenotazioni SET status='0', num_contratto=NULL, aperto_da_ra_void='1' WHERE Nr_Pren='" & idPrenotazione.Text & "'", Dbc)
            Cmd.ExecuteNonQuery()
        End If
        '--------------------------------------------------------------------------------------------------------------------------------

        'VISIBILITA' DEI PULSANTI ---------------
        visibilita_void()
        '----------------------------------------

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub btnVoid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVoid.Click
        Dim stato_attuale As String = getStatoContratto()
        If stato_attuale = "1" Then
            'ANNULLAMENTO DI UN CONTRATTO DI CUI NON E' ANCORA STATO FATTO IL CHECK OUT DEL VEICOLO
            setta_contratto_void()
        ElseIf (stato_attuale = "4" Or stato_attuale = "8") And funzioni_comuni.contratto_settabile_void(idContratto.Text, Request.Cookies("SicilyRentCar")("stazione")) Then
            setta_contratto_void()
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: non è più possibile rendere void questo contratto.")
            btnVoid.Visible = False
        End If
    End Sub

    Protected Sub btnFattDaControllare_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFattDaControllare.Click
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "UPDATE contratti SET fatturazione_da_controllare='1'," &
        " id_operatore_fatt_controllare='" & Request.Cookies("SicilyRentCar")("IdUtente") & "' " &
        " WHERE id='" & idContratto.Text & "'"
        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        lblFattDaControllare.Visible = True
        btnFattDaControllare.Visible = False

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub btnModificaAdmin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaAdmin.Click
        'PER EVITARE CHE IL CONTRATTO VENGA MODIFICATO UNA VOLTA CHE E' STATO FATTURATO (TORNANDO INDIETRO COI TASTI DEL BROWSER) CONTROLLO
        'L'EFFETTIVO STATO DEL CONTRATTO
        Dim statoContratto As String = getStatoContratto()
        If (statoContratto = "4" Or statoContratto = "8") Then
            If btnModificaAdmin.Text = "Modifica Admin" Then
                lblVariazioneACarico.Visible = False
                dropVariazioneACaricoDi.Visible = False

                txtOraRientro.Enabled = True
                txtAData.Enabled = True

                txtoraPartenza.Enabled = True
                txtDaData.Enabled = True

                txtSconto.Enabled = True
                dropTipoSconto.Enabled = True
                txtScontoRack.Enabled = True

                chkAbbuonaGiornoExtra.Enabled = True

                gruppoDaCalcolare.Enabled = True

                btnGeneraContratto.Visible = False
                btnPagamento.Visible = False

                btnModificaTariffaAdmin.Visible = True

                btnAnnullaModificheAdmin.Visible = True
                btnSalvaModifiche.Visible = True

                btnAggiungiExtra.Visible = True

                txtNoteContratto.ReadOnly = False

                bt_Check_Out.Visible = False
                btnSalvaRientro.Visible = False

                btnGeneraContratto.Visible = False
                btnGeneraContrattoRientro.Visible = False

                btnVoid.Visible = False

                btnDaFatturare.Visible = False
                btnGeneraFattura.Visible = False
                lblDataFattura.Visible = False
                txtDataFattura.Visible = False
                lblNumFattura.Visible = False
                txtNumFattura.Visible = False

                btnModificaAdmin.Text = "Ricalcola"
                statoModificaContratto.Text = "1"

                data_pick_up_attuale.Text = txtDaData.Text
                orario_pick_up_attuale.Text = txtoraPartenza.Text
                data_drop_off_attuale.Text = txtAData.Text
                orario_drop_off_attuale.Text = txtOraRientro.Text
                numero_giorni_attuale.Text = txtNumeroGiorni.Text
                gruppo_calcolato_attuale.Text = gruppoDaCalcolare.SelectedValue
                giorno_extra_omaggiato_attuale.Text = chkAbbuonaGiornoExtra.Checked


                If dropTariffeGeneriche.SelectedValue <> "0" Then
                    id_tariffa_attuale.Text = "G" & dropTariffeGeneriche.SelectedValue
                Else
                    id_tariffa_attuale.Text = "P" & dropTariffeParticolari.SelectedValue
                End If

                'CONSERVO I DATI PER CONTROLLARE IN CASO DI SALVATAGGIO SE L'OPERATORE HA EFFETTUATO IL RICALCOLO
                mod_data_drop_off.Text = txtAData.Text
                mod_id_stazione_drop_off.Text = dropStazioneDropOff.SelectedValue
                mod_orario_drop_off.Text = txtOraRientro.Text
                mod_gruppo_calcolato.Text = gruppoDaCalcolare.SelectedValue
                mod_giorno_extra_omaggiato.Text = chkAbbuonaGiornoExtra.Checked

                If dropTariffeGeneriche.SelectedValue <> "0" Then
                    mod_id_tariffa.Text = dropTariffeGeneriche.SelectedValue
                Else
                    mod_id_tariffa.Text = dropTariffeParticolari.SelectedValue
                End If

                'SE LA TARIFFA E' BROKER
                If tariffa_broker.Text = "1" Then
                    lblVariazioneACarico.Visible = True
                    dropVariazioneACaricoDi.Visible = True

                    lblOldACaricoDi.Text = dropVariazioneACaricoDi.SelectedValue

                    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()
                    Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(estensioni_sempre_a_carico_del_broker,'0') FROM clienti_tipologia WHERE id='" & CInt(dropTipoCliente.SelectedValue) & "'", Dbc)

                    Dim sempre_a_carico_del_broker As Boolean = Cmd.ExecuteScalar

                    If sempre_a_carico_del_broker Then
                        dropVariazioneACaricoDi.SelectedValue = "1"
                        dropVariazioneACaricoDi.Enabled = False
                    Else
                        If txtNumeroGiorni.Text = txtNumeroGiorniTO.Text Then
                            dropVariazioneACaricoDi.SelectedValue = "1"
                        Else
                            dropVariazioneACaricoDi.SelectedValue = "0"
                        End If
                        dropVariazioneACaricoDi.Enabled = True
                    End If

                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing
                End If

                'PER LA MODIFICA DEL CONTRATTO DUPLICO SUBITO LE RIGHE DI contratti_costi IN MODO CHE QUALSIASI MODIFICA DEBBA ESSERE CONFERMATA
                '(ANCHE PER GLI ACCESSORI)
                modifica_contratto_duplica_righe_calcolo()
            ElseIf btnModificaAdmin.Text = "Ricalcola" Then
                Dim data_test As String
                Try
                    data_test = Year(txtAData.Text) & "-" & Month(txtAData.Text) & "-" & Day(txtAData.Text) & " " & Hour(txtOraRientro.Text) & ":" & Minute(txtOraRientro.Text)

                    If ((Year(txtAData.Text) < Year(txtDaData.Text)) Or (Year(txtAData.Text) = Year(txtDaData.Text) And Month(txtAData.Text) < Month(txtDaData.Text)) Or (Year(txtAData.Text) = Year(txtDaData.Text) And Month(txtAData.Text) = Month(txtDaData.Text) And Day(txtAData.Text) < Day(txtDaData.Text))) Then
                        data_test = "-1"
                    End If
                Catch ex As Exception
                    data_test = "-1"
                End Try

                If (dropTariffeGeneriche.SelectedValue <> "0" And dropTariffeParticolari.SelectedValue = "0") Or (dropTariffeGeneriche.SelectedValue = "0" And dropTariffeParticolari.SelectedValue <> "0") Then
                    If data_test <> "-1" Then
                        If dropStazioneDropOff.SelectedValue <> "0" Then
                            If dropVariazioneACaricoDi.SelectedValue <> "-1" Or tariffa_broker.Text <> "1" Then
                                dropVariazioneACaricoDi.Enabled = False
                                esegui_ricalcolo_nolo_in_corso("4")

                                'CONFERMO IL RICALCOLO DEI DATI
                                mod_data_drop_off.Text = txtAData.Text
                                mod_id_stazione_drop_off.Text = dropStazioneDropOff.SelectedValue
                                mod_orario_drop_off.Text = txtOraRientro.Text
                                mod_gruppo_calcolato.Text = gruppoDaCalcolare.SelectedValue
                                mod_giorno_extra_omaggiato.Text = chkAbbuonaGiornoExtra.Checked
                                If dropTariffeGeneriche.SelectedValue <> "0" Then
                                    mod_id_tariffa.Text = dropTariffeGeneriche.SelectedValue
                                Else
                                    mod_id_tariffa.Text = dropTariffeParticolari.SelectedValue
                                End If

                                lblSaldo.Text = "Saldo: " & FormatNumber(getTotaleDaPagare(), 2, , , TriState.False) & " €"
                            Else
                                Libreria.genUserMsgBox(Me, "Specificare se la variazione è a carico del broker o a carico del cliente.")
                            End If
                        Else
                            Libreria.genUserMsgBox(Me, "Specificare la stazione di drop off.")
                        End If
                    Else
                        Libreria.genUserMsgBox(Me, "Specificare correttamente data ed orario di drop off.")
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Selezionare una tariffa generica oppure una tariffa particolare.")
                End If
            End If
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: il contratto non e' più modificabile.")
        End If
    End Sub

    Protected Sub btnAnnullaModificheAdmin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnullaModificheAdmin.Click
        'VISIBILITA'/INTERIGIBILITA' DEI PULSANTI E DEI CAMPI TESTUALI -----------------------------------------------------------------
        visibilita_nolo_concluso_admin()
        '---------------------------------------------------------------------------------------------------------------------------------
        'REIMPOSTO I DATI MODIFICABILI CON QUELLI DEL CALCOLO ATTUALMENTE SALVATO --------------------------------------------------------
        txtDaData.Text = data_pick_up_attuale.Text
        txtAData.Text = data_drop_off_attuale.Text
        gruppoDaCalcolare.SelectedValue = gruppo_calcolato_attuale.Text
        txtNumeroGiorni.Text = numero_giorni_attuale.Text
        chkAbbuonaGiornoExtra.Checked = giorno_extra_omaggiato_attuale.Text

        If tariffa_broker.Text = "1" Then
            txtNumeroGiorniTO.Text = lblGiorniToOld.Text
        End If

        If tariffa_broker.Text = "1" Then
            dropVariazioneACaricoDi.SelectedValue = lblOldACaricoDi.Text
        End If

        txtoraPartenza.Text = orario_pick_up_attuale.Text
        ore1.Text = Hour(txtoraPartenza.Text)
        minuti1.Text = Hour(txtoraPartenza.Text)

        If Len(ore1.Text) = 1 Then
            ore1.Text = "0" & ore1.Text
        End If

        If Len(minuti1.Text) = 1 Then
            minuti1.Text = "0" & minuti1.Text
        End If

        txtOraRientro.Text = orario_drop_off_attuale.Text
        ore2.Text = Hour(txtOraRientro.Text)
        minuti2.Text = Minute(txtOraRientro.Text)


        If Len(ore2.Text) = 1 Then
            ore2.Text = "0" & ore2.Text
        End If

        If Len(minuti2.Text) = 1 Then
            minuti2.Text = "0" & minuti2.Text
        End If

        If Left(id_tariffa_attuale.Text, 1) = "G" Then
            'GENERICA
            dropTariffeGeneriche.SelectedValue = Replace(id_tariffa_attuale.Text, "G", "")
            Dim id_tar As String = Replace(id_tariffa_attuale.Text, "G", "")
            Dim cod_tar As String = dropTariffeGeneriche.SelectedItem.Text.Replace(" (RA)", "")
            dropTariffeGeneriche.Items.Clear()
            dropTariffeGeneriche.Items.Add("Seleziona...")
            dropTariffeGeneriche.Items(0).Value = "0"
            dropTariffeGeneriche.Items.Add(cod_tar)
            dropTariffeGeneriche.Items(1).Value = id_tar

            dropTariffeGeneriche.SelectedValue = id_tar
        ElseIf Left(id_tariffa_attuale.Text, 1) = "P" Then
            'PARTICOLARE
            dropTariffeParticolari.SelectedValue = Replace(id_tariffa_attuale.Text, "P", "")
            Dim id_tar As String = Replace(id_tariffa_attuale.Text, "P", "")
            Dim cod_tar As String = dropTariffeParticolari.SelectedItem.Text.Replace(" (RA)", "")
            dropTariffeParticolari.Items.Clear()
            dropTariffeParticolari.Items.Add("Seleziona...")
            dropTariffeParticolari.Items(0).Value = "0"
            dropTariffeParticolari.Items.Add(cod_tar)
            dropTariffeParticolari.Items(1).Value = id_tar

            dropTariffeParticolari.SelectedValue = id_tar
        End If

        dropTariffeGeneriche.Enabled = False
        dropTariffeParticolari.Enabled = False

        data_drop_off_attuale.Text = ""
        gruppo_calcolato_attuale.Text = ""
        orario_drop_off_attuale.Text = ""
        numero_giorni_attuale.Text = ""
        giorno_extra_omaggiato_attuale.Text = ""
        id_tariffa_attuale.Text = ""

        mod_data_drop_off.Text = ""
        mod_id_stazione_drop_off.Text = ""
        mod_orario_drop_off.Text = ""
        mod_gruppo_calcolato.Text = ""
        mod_giorno_extra_omaggiato.Text = ""
        mod_id_tariffa.Text = ""
        '-------------------------------------------------------------------------------------------------------------------------------
        'ELIMINAZIONE DELLE RIGHE DI CALCOLO -------------------------------------------------------------------------------------------
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM contratti_warning WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM contratti_costi WHERE id_documento='" & idContratto.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        numCalcolo.Text = CInt(numCalcolo.Text) - 1

        listContrattiCosti.DataBind()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
        '--------------------------------------------------------------------------------------------------------------------------------
        aggiorna_informazioni_dopo_modifica_costi()

        lblSaldo.Text = "Saldo: " & FormatNumber(getTotaleDaPagare(), 2, , , TriState.False) & " €"
    End Sub

    Protected Sub btnModificaTariffaAdmin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaTariffaAdmin.Click

        If tariffa_broker.Text = "0" Or (tariffa_broker.Text = "1" And dropVariazioneACaricoDi.SelectedValue = "1") Then
            txtoraPartenza.Enabled = False
            txtOraRientro.Enabled = False
            txtAData.Enabled = False
            dropStazioneDropOff.Enabled = False
            dropTipoCliente.Enabled = False
            gruppoDaCalcolare.Enabled = False
            chkAbbuonaGiornoExtra.Enabled = False

            setQueryTariffePossibili(0)
            dropTariffeParticolari.Enabled = True

            If tariffa_broker.Text = "1" Then
                dropTariffeGeneriche.Enabled = False
            Else
                dropTariffeGeneriche.Enabled = True
            End If

            btnModificaTariffaAdmin.Visible = False
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: per poter modificare la tariffa è necessario prima salvare il voucher originale e dopo eseguire l'estensione a carico del cliente.")
        End If
    End Sub

    Protected Sub btnDaFatturare_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDaFatturare.Click
        'NELLO STATO CONTRATTO 4: SE ADMIN CLICCA QUESTO PULSANTE VUOL DIRE CHE IL CONTRATTO DEVE PASSARE ALLO STATO "DA FATTURARE"
        'QUEST'AZIONE ANNULLA ANCHE UN'EVENTUALE "CONTRATTO DA CONTROLLARE" CLICCATO DALL'OPERATORE DI BANCO.
        'NELLO STATO 8: IL PULSANTE SERVE PER INDICARE AL SISTEMA CHE NON DEVE ESSERE FATTURATO O PER TORNARE ALLO STATO DA FATTURARE.
        Dim stato_contratto As String = getStatoContratto()

        If stato_contratto = "4" Then
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE contratti SET status='8', fatturazione_da_controllare='0' WHERE num_contratto='" & lblNumContratto.Text & "' AND attivo='1'", Dbc)
            Cmd.ExecuteNonQuery()

            statoContratto.Text = "8"
            lblStatoFatturazione.Text = "Da Fatturare"
            btnDaFatturare.Text = "Non Fatturare"
            lblFattDaControllare.Visible = False
            btnGeneraFattura.Visible = True
            lblDataFattura.Visible = True
            txtDataFattura.Visible = True
            lblNumFattura.Visible = True
            txtNumFattura.Visible = True

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            btnGeneraFattura.OnClientClick = "javascript: return(window.confirm ('Attenzione - Sei sicuro di voler generare la fattura per questo contratto?'));"
        ElseIf stato_contratto = "8" Then
            If btnDaFatturare.Text = "Da Fatturare" Then
                'IL CONTRATTO NON DEVE ATTUALMENTE ESSERE FATTURATO
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE contratti SET non_fatturare='0' WHERE num_contratto='" & lblNumContratto.Text & "' AND attivo='1'", Dbc)
                Cmd.ExecuteNonQuery()

                lblStatoFatturazione.Text = "Da Fatturare"
                btnDaFatturare.Text = "Non Fatturare"

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                btnGeneraFattura.OnClientClick = "javascript: return(window.confirm ('Attenzione - Sei sicuro di voler generare la fattura per questo contratto?'));"
            ElseIf btnDaFatturare.Text = "Non Fatturare" Then
                'IL CONTRATTO PUO' ESSERE FATTURATO
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE contratti SET non_fatturare='1' WHERE num_contratto='" & lblNumContratto.Text & "' AND attivo='1'", Dbc)
                Cmd.ExecuteNonQuery()

                lblStatoFatturazione.Text = "Da Fatturare - NON FATTURARE"
                btnDaFatturare.Text = "Da Fatturare"

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                btnGeneraFattura.OnClientClick = "javascript: return(window.confirm ('Attenzione - Sei sicuro di voler generare la fattura anche se il contratto è stato indicato come da NON FATTURARE?'));"
            End If
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: il contratto è già fatturato. Impossibile completare l'operazione.")
        End If
    End Sub

    Protected Function contratto_fatturabile() As String
        'RESTITUISCE IL TESTO DI ERRORE QUALORA PER UN QUALSIASI MOTIVO NON E' POSSIBILE FATTURARE IL CONTRATTO
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM contratti WITH(NOLOCK) WHERE id='" & idContratto.Text & "' AND attivo='1' AND status='8'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test <> "" Then
            contratto_fatturabile = ""
        Else
            contratto_fatturabile = "Attenzione: non è possibile fatturare il contratto. Ricarica il contratto dalla pagina di ricerca prima di riprovare."
        End If

        'SE IL CONTRATTO DA FATTURARE E' STATO TROVATO ESEGUO SU DI ESSO I VARI CONTROLLI
        If contratto_fatturabile = "" Then
            'CONTROLLO SE LA TIPOLOGIA DI CLIENTE, QUALORA FOSSE BROKER, ABBIA ASSOCIATA UN'ANAGRAFICA DITTA
            If tariffa_broker.Text = "1" Then
                Cmd = New Data.SqlClient.SqlCommand("SELECT id_ditta FROM clienti_tipologia WITH(NOLOCK) WHERE id='" & dropTipoCliente.SelectedValue & "'", Dbc)
                test = Cmd.ExecuteScalar & ""

                If test <> "" Then
                    contratto_fatturabile = ""
                Else
                    contratto_fatturabile = " - Attenzione: non è associata alcuna anagrafica al BROKER selezionato. Impossibile fatturare il contratto." & vbCrLf
                End If
            End If

            'SE IL CLIENTE ASSOCIATO E' CASH E IL CONTRATTO PRESENTA UN SALDO NON E' POSSIBILE GENERARE FATTURA
            If FormatNumber(getTotaleDaPagare(), 2, , , TriState.False) <> 0 And txtCodiceEdp.Text = Costanti.codice_cash Then
                contratto_fatturabile = contratto_fatturabile & " - Attenzione: il contratto ha un saldo diverso da 0 - Il cliente è CASH. Impossibile fatturare." & vbCrLf
            End If

            If txtNumFattura.Text <> "" Then
                'SE E' STATO SPECIFICATO UN NUMERO DI FATTURA CONTROLLO CHE QUESTO SIA DISPONIBILE
                Dim data_fattura As String
                If txtDataFattura.Text <> "" Then
                    data_fattura = txtDataFattura.Text
                Else
                    data_fattura = txtAData.Text
                End If

                Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM fatture_nolo WHERE attiva='1' AND num_fattura='" & txtNumFattura.Text & "' AND YEAR(data_fattura)='" & Year(data_fattura) & "'", Dbc)
                test = Cmd.ExecuteScalar & ""

                If test <> "" Then
                    contratto_fatturabile = contratto_fatturabile & "Attenzione: il numero fattura specificato non è disponibile." & vbCrLf
                ElseIf test = "" And tariffa_broker.Text = "1" Then
                    'SE IL NUMERO E' DISPONIBILE MA LA TARIFFA E' BROKER CI SI DEVE ACCERTARE CHE ANCHE IL SUCCESSIVO NUMERO DI FATTURA SIA DISPONIBILE
                    Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM fatture_nolo WHERE attiva='1' AND num_fattura='" & CInt(txtNumFattura.Text) + 1 & "' AND YEAR(data_fattura)='" & Year(data_fattura) & "'", Dbc)
                    test = Cmd.ExecuteScalar & ""

                    If test <> "" Then
                        contratto_fatturabile = contratto_fatturabile & "Attenzione: il numero fattura specificato non è disponibile." & vbCrLf
                    End If
                End If

                'CONTROLLO ANCHE CHE NON VENGA ASSEGNATO UN NUMERO SUCCESSIVO AL PRIMO DISPONIBILE
                If test = "" Then
                    Cmd = New Data.SqlClient.SqlCommand("SELECT contatore FROM contatori WHERE tipo='fatture_nolo' AND anno='" & Year(data_fattura) & "'", Dbc)
                    test = Cmd.ExecuteScalar & ""
                    If test = "" OrElse CInt(txtNumFattura.Text) >= CInt(test) OrElse (tariffa_broker.Text = 1 And CInt(txtNumFattura.Text) + 1 >= CInt(test)) Then
                        contratto_fatturabile = contratto_fatturabile & "Attenzione: il numero fattura specificato è uguale o succesivo al primo disponibile. Utilizzare l'assegnazione automatica." & vbCrLf
                    End If
                End If
            End If
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub visibilita_fatturato()
        btnRicalcolaDaPrenotazione.Visible = False
        btnScegliDitta.Visible = False
        btnScegliPrimoGuidatore.Visible = False
        btnScegliSecondoConducente.Visible = False
        btnScegliTarga.Visible = False
        btnTrovaTarga.Visible = False
        btnAggiungiExtra.Visible = False
        btnVoid.Visible = False
        btnGeneraContratto.Visible = False
        btnGeneraContrattoRientro.Visible = False
        btnAnnullaDocumento.Text = "Chiudi"
        btnPagamento.Visible = False
        riga_fatturazione.Visible = True

        'GPS
        btnCercaGps.Visible = False

        lblFattDaControllare.Visible = False
        lblStatoFatturazione.Text = "Fatturato"

        btnDaFatturare.Visible = False
        btnFattDaControllare.Visible = False
        btnGeneraFattura.Visible = False
        lblDataFattura.Visible = False
        txtDataFattura.Visible = False
        lblNumFattura.Visible = False
        txtNumFattura.Visible = False

        btnModificaAdmin.Visible = False

        If livello_accesso_admin.Text <> "1" Then
            btnDuplicaContratto.Visible = True

            btnStampaFatturaCliente.Visible = True
            btnStampaFatturaCliente.Text = "Stampa Fattura Cliente - Num. " & fatturazione_nolo.getNumeroFatturaCliente(lblNumContratto.Text, True)
            btnEliminaFatture.Visible = True

            If tariffa_broker.Text = "1" Then
                btnStampaFatturaBroker.Visible = True
                btnStampaFatturaBroker.Text = "Stampa Fattura Broker - Num. " & fatturazione_nolo.getNumeroFatturaBroker(lblNumContratto.Text, True)
            ElseIf txtGiorniPrepagati.Visible Then
                btnStampaFatturaPrepagato.Visible = True
                btnStampaFatturaPrepagato.Text = "Stampa Fattura Prepagato - Num. " & fatturazione_nolo.getNumeroFatturaCostiPrepagati(lblNumContratto.Text, True)
            End If
        End If
    End Sub

    Protected Sub btnGeneraFattura_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGeneraFattura.Click
        'LA GENERAZIONE DELLA FATTURA VIENE EFFETTUATA SULLE RIGHE DI CALCOLO SALVATE SU DATABASE. PER ESSERE SICURI CHE SI STA FATTURANDO
        'QUELLO CHE L'OPERATORE VEDE A VIDEO CONTROLLO CHE EFFETTIVAMENTE L'ID CONTRATTO SALVATO SU PAGINA CORRISPONDA A QUELLO DEL CONTRATTO 
        'ATTIVO - CONTESTUALMENTE ANCHE CHE SIA NELLO STATO CORRETTO
        If getStatoContratto() <> "8" Then
            Libreria.genUserMsgBox(Me, "Attenzione: il contratto è già stato fatturato. Impossibile procedere.")
        Else
            Dim errore_fatturabilita As String = contratto_fatturabile()
            If errore_fatturabilita = "" Then
                Dim data_fattura As String
                If Trim(txtDataFattura.Text) <> "" Then
                    data_fattura = txtDataFattura.Text
                Else
                    data_fattura = txtAData.Text
                End If
                Dim numFattura As String = ""
                If Trim(txtNumFattura.Text) <> "" Then
                    numFattura = txtNumFattura.Text
                End If

                If numFattura <> "" And Trim(txtDataFattura.Text) = "" Then
                    Libreria.genUserMsgBox(Me, "Specificare la data della fattura.") 'RICHIESTA DI SANTINO D'ANGELO - MAIL FABIO DEL 16/10/2013
                Else
                    fatturazione_nolo.genera_fattura_ra(idContratto.Text, numCalcolo.Text, data_fattura, numFattura)

                    visibilita_fatturato()
                    statoContratto.Text = "6"
                    listContrattiCosti.DataBind()

                    Libreria.genUserMsgBox(Me, "Contratto Fatturato correttamente.")
                End If
            Else
                Libreria.genUserMsgBox(Me, errore_fatturabilita)
            End If
        End If
    End Sub

    Protected Sub btnEliminaFatture_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminaFatture.Click
        If Not fatturazione_nolo.elimina_fatture_ra(lblNumContratto.Text) Then
            Libreria.genUserMsgBox(Me, "Attenzione: le fatture del contratto sono state già inviate in contabilita. Impossibile procedere con l'eliminazione.")
        Else
            Session("carica_contratto") = idContratto.Text
            Response.Redirect("contratti.aspx")
        End If
    End Sub

    Protected Sub btnStampaFatturaCliente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaFatturaCliente.Click
        stampa_fattura(fatturazione_nolo.get_id_fattura_cliente(idContratto.Text))
        'Dim file As String = fatturazione_nolo.genera_flusso_fattura_ottico(Year(txtAData.Text), num_fattura_cliente, num_fattura_cliente)
        'fatturazione_nolo.genera_flusso_fatture_nolo(Year(txtAData.Text), "1", "10")
        'fatturazione_nolo.genera_flusso_clienti(Year(txtAData.Text), "1", "10")
        'fatturazione_nolo.genera_flusso_fattura_ottico(Year(txtAData.Text), "1", "10")
        'fatturazione_nolo.genera_flusso_pagamenti(Year(txtAData.Text), "1", "14")
    End Sub

    Protected Sub btnStampaFatturaBroker_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaFatturaBroker.Click
        stampa_fattura(fatturazione_nolo.get_id_fattura_broker(idContratto.Text))
        'Dim file As String = fatturazione_nolo.genera_flusso_fattura_ottico(Year(txtAData.Text), num_fattura_broker, num_fattura_broker)
    End Sub

    Protected Sub btnStampaFatturaPrepagato_Click(sender As Object, e As System.EventArgs) Handles btnStampaFatturaPrepagato.Click
        stampa_fattura(fatturazione_nolo.get_id_fattura_prepagato(idContratto.Text))
    End Sub


    Protected Sub btnCercaGps_Click(sender As Object, e As System.EventArgs) Handles btnCercaGps.Click
        If txtCodiceGps.Enabled Then
            If txtCodiceGps.Text <> "" Then
                Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dim strQuery As String
                strQuery = "SELECT id, codice FROM gps WITH(NOLOCK) WHERE codice LIKE '" & txtCodiceGps.Text & "%'  AND id_gps_status='" & Costanti.stato_gps.in_parco & "' " &
                    "AND id_stazione_attuale='" & Request.Cookies("SicilyRentCar")("stazione") & "' ORDER BY codice"

                Dim cmd As New Data.SqlClient.SqlCommand(strQuery, conn)
                Dim da As New Data.SqlClient.SqlDataAdapter(cmd)
                Dim ds As New Data.DataSet()
                da.Fill(ds)

                listGps.DataSource = ds
                listGps.DataBind()
                listGps.Visible = True
                txtCodiceGps.Focus()

                cmd.Dispose()
                cmd = Nothing
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If
        Else
            txtCodiceGps.Text = ""
            lblIdGps.Text = ""
            txtCodiceGps.Enabled = True
            txtCodiceGps.Focus()
        End If
    End Sub

    Protected Sub listGps_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles listGps.SelectedIndexChanged
        lblIdGps.Text = listGps.SelectedValue
        txtCodiceGps.Text = listGps.SelectedItem.Text
        txtCodiceGps.Enabled = False
        listGps.Visible = False

        If statoContratto.Text = Costanti.stato_contratto.check_out AndAlso getStatoContratto() = Costanti.stato_contratto.check_out Then
            'NELLO STATO check_out (PER CUI IL CONTRATTO E' STATO SALVATO MA L'AUTO NON E' ANCORA USCITA) E' ANCORA POSSIBILE CAMBIARE IL GPS COLLEGATO AL CONTRATTO.
            'FACENDO QUESTO TUTTAVIA E' NECESSARIO SALVARE IL NUOVO GPS
            modifica_gps_cnt_prima_del_check_out()

            Libreria.genUserMsgBox(Me, "- GPS modificato correttamente.")
        End If
    End Sub

    Protected Sub duplica_contratto()
        'DUPLICAZIONE DELLE TABELLE

    End Sub
    
    Protected Sub btnDuplicaContratto_Click(sender As Object, e As System.EventArgs) Handles btnDuplicaContratto.Click
        duplica_contratto()
    End Sub



    Protected Sub lblNumPren_Click(sender As Object, e As System.EventArgs) Handles lblNumPren.Click
        Session("num_prenotazione_from_preventivi") = lblNumPren.Text

        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('prenotazioni.aspx','')", True)
            End If
        End If
    End Sub
End Class
