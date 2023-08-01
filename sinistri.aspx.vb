
Partial Class sinistri
    Inherits System.Web.UI.Page

    Protected Function get_idSinistro(ByVal anno As String, ByVal protocollo_interno As String) As String

        Dim sqlstr As String = "SELECT id FROM sinistri WITH(NOLOCK) WHERE anno='" & anno & "' AND numero_protocollo_interno='" & protocollo_interno & "' AND attivo='1'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            get_idSinistro = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("err get_idSinistro : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try

    End Function

    Protected Sub scegli_tipologia(ByVal sender As Object, ByVal e As gestione_danni_sinistro_tipologia.ScegliTipologiaEventArgs)

        div_tipologia.Visible = False
        div_pagina.Visible = True

        dropTipologia.Items.Clear()
        dropTipologia.Items.Add("Seleziona")
        dropTipologia.Items(0).Value = "0"
        dropTipologia.DataBind()

        dropTipologia.SelectedValue = e.id_tipologia


    End Sub

    Protected Sub scegli_gestito_da(ByVal sender As Object, ByVal e As gestione_danni_sinistro_gestito_da.ScegliGestitoDaEventArgs)
        div_gestitoDa.Visible = False
        div_pagina.Visible = True

        dropGestitoDa.Items.Clear()
        dropGestitoDa.Items.Add("Seleziona")
        dropGestitoDa.Items(0).Value = "0"
        dropGestitoDa.DataBind()

        dropGestitoDa.SelectedValue = e.id_gestito_da
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler tipologia.scegliTipologia, AddressOf scegli_tipologia
        AddHandler gestito_da.scegliGestitoDa, AddressOf scegli_gestito_da

        If Not Page.IsPostBack() Then
            permesso_accesso.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneSinistri)

            If permesso_accesso.Text <> "1" Then
                For i As Integer = 2011 To Year(Now())
                    dropCercaAnno.Items.Add(i)
                Next
                dropCercaAnno.SelectedValue = Year(Now())

                dropCercaTipologia.DataBind()
                dropTipologia.DataBind()
                dropGestitoDa.DataBind()

                iva_attuale.Text = Costanti.iva_default

                If Session("sin_anno") <> "" And Session("sin_proto") <> "" Then
                    'SINISTRO  RICHIAMATO
                    div_dettaglio.Visible = True
                    'ID SINISTRO ATTIVO E STATO DA RECUPERARE DA ANNO E PROTOCOLLO INTERNO
                    id_sinistro.Text = get_idSinistro(Session("sin_anno"), Session("sin_proto"))

                    If id_sinistro.Text <> "" Then
                        Dim errore As String = Session("sin_err")

                        Session("sin_err") = ""
                        Session("sin_anno") = ""
                        Session("sin_proto") = ""

                        btnSalva.Text = "Modifica"

                        fill_dati_sinistro(id_sinistro.Text)

                        If errore <> "" Then
                            Libreria.genUserMsgBox(Me, errore)
                        End If
                    Else
                        'NON DOVREBBE MAI CAPITARE - SIAMO QUI SE IL DOCUMENTO RISULTA SALVATO MA NON C'E' ALCUN RECORD ATTIVO CORRISPONDENTE
                        'ALL'ANNO ED AL NUMERO DI PROTOCOLLO INTERNO
                        Session("sin_err") = ""
                        Session("sin_anno") = ""
                        Session("sin_proto") = ""
                        Response.Redirect("default.aspx")
                    End If
                ElseIf Session("snx_id_cnt") <> "" And Session("snx_id_rds") <> "" And Session("snx_sin") = "ATTIVO" Then
                    'NUOVO SINISTRO ATTIVO - NON DEFINITO
                    div_dettaglio.Visible = True
                    lblTipoSinistro.Text = "NUOVO SINISTRO ATTIVO - NON DEFINITO"
                    stato.Text = "0"

                    txtLiquidato.ReadOnly = True
                    txtDifferenza.ReadOnly = True

                    div_importi_sinistro_attivo.Visible = True
                    txtAnno.Text = Year(Now())
                    txtDataAperturaSinistro.Text = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())
                    txtGestitaDaInData.Text = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())


                    If Session("snx_id_cnt") <> Session("snx_id_rds") Then
                        'ID CONTRATTO - PASSATO DALLA SESSION?
                        id_contratto.Text = Session("snx_id_cnt")
                        'ID_RDS - DA SESSION
                        id_rds.Text = Session("snx_id_rds")
                    Else
                        'IN QUESTO CASO SI TRATTA DI RDS SU PIAZZALE
                        id_contratto.Text = "-1"
                        'ID_RDS - DA SESSION
                        id_rds.Text = Session("snx_id_rds")
                    End If


                    Session("snx_id_cnt") = ""
                    Session("snx_id_rds") = ""
                    Session("snx_sin") = ""

                    fill_dati_nuovo_sinistro()
                ElseIf Session("snx_id_cnt") <> "" And Session("snx_id_rds") <> "" And Session("snx_sin") = "PASSIVO" Then
                    div_dettaglio.Visible = True
                    lblTipoSinistro.Text = "NUOVO SINISTRO PASSIVO"
                    stato.Text = "-1"

                    div_importi_sinistro_attivo.Visible = False

                    txtAnno.Text = Year(Now())
                    txtDataAperturaSinistro.Text = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())
                    txtGestitaDaInData.Text = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())

                    If Session("snx_id_cnt") <> Session("snx_id_rds") Then
                        'ID CONTRATTO - PASSATO DALLA SESSION?
                        id_contratto.Text = Session("snx_id_cnt")
                        'ID_RDS - DA SESSION
                        id_rds.Text = Session("snx_id_rds")
                    Else
                        'IN QUESTO CASO SI TRATTA DI RDS SU PIAZZALE
                        id_contratto.Text = "-1"
                        'ID_RDS - DA SESSION
                        id_rds.Text = Session("snx_id_rds")
                    End If


                    Session("snx_id_cnt") = ""
                    Session("snx_id_rds") = ""
                    Session("snx_sin") = ""

                    fill_dati_nuovo_sinistro()
                Else
                    'PAGINA DI RICERCA
                    lblOrderBY.Text = " ORDER BY sinistri.id ASC"
                    div_ricerca.Visible = True
                    ricerca(lblOrderBY.Text)
                End If
            Else
                Response.Redirect("default.aspx")
            End If

        Else
            sqlSinistri.SelectCommand = query_cerca.Text & " " & lblOrderBY.Text
        End If

    End Sub

    Protected Sub fill_dati_sinistro(ByVal idSinistro As String)

        Dim sqlstr As String = "SELECT * FROM sinistri WITH(NOLOCK) WHERE id='" & idSinistro & "'"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            'RECUPERO DATI DA SINISTRI
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.Read() Then
                stato.Text = Rs("id_stato")
                If stato.Text = "1" Then
                    lblTipoSinistro.Text = "SINISTRO ATTIVO - NON DEFINITO"
                    div_importi_sinistro_attivo.Visible = True

                    btnStampaDatiLegale.Visible = True
                    btnStampaSinistro.Visible = True
                    txtLiquidato.ReadOnly = False
                    txtDifferenza.ReadOnly = False
                ElseIf stato.Text = "2" Then
                    lblTipoSinistro.Text = "SINISTRO ATTIVO - DEFINITO"
                    div_importi_sinistro_attivo.Visible = True

                    btnStampaDatiLegale.Visible = True
                    btnStampaSinistro.Visible = True
                    txtLiquidato.ReadOnly = False
                    txtDifferenza.ReadOnly = False
                ElseIf stato.Text = "3" Then
                    lblTipoSinistro.Text = "SINISTRO PASSIVO"
                    btnStampaDatiLegale.Visible = False
                    btnStampaSinistro.Visible = False
                    div_importi_sinistro_attivo.Visible = False
                End If

                btnSalvaNota.Visible = True

                txtAnno.Text = Rs("anno")
                txtNumeroSinistro.Text = Rs("numero")
                dropTipologia.SelectedValue = Rs("id_tipologia")
                If (Rs("data_evento") & "") <> "" Then
                    txtDataEvento.Text = Day(Rs("data_evento")) & "/" & Month(Rs("data_evento")) & "/" & Year(Rs("data_evento"))
                Else
                    txtDataEvento.Text = ""
                End If
                txtDataAperturaSinistro.Text = Day(Rs("data_apertura_sinistro")) & "/" & Month(Rs("data_apertura_sinistro")) & "/" & Year(Rs("data_apertura_sinistro"))
                id_rds.Text = Rs("id_rds")
                txtNumeroRDS.Text = Rs("numero_rds")
                txtProtocollo.Text = Rs("numero_protocollo_interno")

                listNote.DataBind()

                txtNumContratto.Text = Rs("num_contratto") & ""
                txtNumSinistroCompagnia.Text = Rs("numero_sinistro_compagnia") & ""

                id_veicolo.Text = Rs("id_veicolo")
                txtTarga.Text = Rs("targa")
                txtModello.Text = Rs("modello")
                id_proprietario_veicolo.Text = Rs("id_proprietario_veicolo")
                lblProprietarioVeicolo.Text = Rs("proprietario_veicolo")
                id_stazione.Text = Rs("id_stazione")
                txtStazione.Text = Rs("stazione")
                txtGuidatore.Text = Rs("guidatore") & ""
                txtControparte.Text = Rs("controparte") & ""
                txtCompagniaControparte.Text = Rs("compagnia_controparte") & ""
                txtFranchigiaCompagnia.Text = Rs("franchigia_compagnia") & ""
                If (Rs("franchigia_compagnia_pagata_il") & "") <> "" Then
                    txtFranchigiaCompagniaPagataIl.Text = Day(Rs("franchigia_compagnia_pagata_il")) & "/" & Month(Rs("franchigia_compagnia_pagata_il")) & "/" & Year(Rs("franchigia_compagnia_pagata_il"))
                Else
                    txtFranchigiaCompagniaPagataIl.Text = ""
                End If
                txtFranchigieContestate.Text = Rs("franchigie_contestate") & ""

                If txtFranchigieContestate.Text <> "" Then
                    txtFranchigieContestate.Text = FormatNumber(txtFranchigieContestate.Text, 2, , , TriState.False)
                End If

                txtAddebitoApplicato.Text = Rs("addebito_applicato") & ""

                If txtAddebitoApplicato.Text <> "" Then
                    txtAddebitoApplicato.Text = FormatNumber(txtAddebitoApplicato.Text, 2, , , TriState.False)
                End If

                If (Rs("data_rimborso") & "") <> "" Then
                    txtDataRimborso.Text = Day(Rs("data_rimborso")) & "/" & Month(Rs("data_rimborso")) & "/" & Year(Rs("data_rimborso"))
                Else
                    txtDataRimborso.Text = ""
                End If

                txtImportoRimborso.Text = Rs("importo_rimborso") & ""

                If txtImportoRimborso.Text <> "" Then
                    txtImportoRimborso.Text = FormatNumber(txtImportoRimborso.Text, 2, , , TriState.False)
                End If

                txtSko.Text = Rs("sko") & ""

                If txtSko.Text <> "" Then
                    txtSko.Text = FormatNumber(txtSko.Text, 2, , , TriState.False)
                End If

                If (Rs("data_rimborso_a_cliente_sbc") & "") <> "" Then
                    txtDataRimborsoClienteSBC.Text = Day(Rs("data_rimborso_a_cliente_sbc")) & "/" & Month(Rs("data_rimborso_a_cliente_sbc")) & "/" & Year(Rs("data_rimborso_a_cliente_sbc"))
                Else
                    txtDataRimborsoClienteSBC.Text = ""
                End If

                txtImportoRimborsoAClienteSbc.Text = Rs("importo_rimborso_a_cliente_sbc") & ""
                If txtImportoRimborsoAClienteSbc.Text <> "" Then
                    txtImportoRimborsoAClienteSbc.Text = FormatNumber(txtImportoRimborsoAClienteSbc.Text, 2, , , TriState.False)
                End If

                If Rs("id_stato") <> "3" Then
                    dropGestitoDa.Text = Rs("id_gestito_da")
                    txtGestitaDaInData.Text = Day(Rs("gestito_da_in_data")) & "/" & Month(Rs("gestito_da_in_data")) & "/" & Year(Rs("gestito_da_in_data"))
                    If (Rs("data_definizione") & "") <> "" Then
                        txtDataDefinizione.Text = Day(Rs("data_definizione")) & "/" & Month(Rs("data_definizione")) & "/" & Year(Rs("data_definizione"))
                    Else
                        txtDataDefinizione.Text = ""
                    End If
                    txtPercentualeConcorsuale.Text = Rs("percentuale_concorsuale") & ""

                    txtAccontoLiquidazioneSinistro.Text = Rs("acconto_liquidazione_sinistro") & ""
                    If txtAccontoLiquidazioneSinistro.Text <> "" Then
                        txtAccontoLiquidazioneSinistro.Text = FormatNumber(txtAccontoLiquidazioneSinistro.Text, 2, , , TriState.False)
                    End If

                    txtRichiestaLiquidazioneDanni.Text = Rs("richiesta_liquidazione_danni") & ""
                    If txtRichiestaLiquidazioneDanni.Text <> "" Then
                        txtRichiestaLiquidazioneDanni.Text = FormatNumber(txtRichiestaLiquidazioneDanni.Text, 2, , , TriState.False)
                    End If

                    txtDifferenza.Text = Rs("differenza") & ""
                    If txtDifferenza.Text <> "" Then
                        txtDifferenza.Text = FormatNumber(txtDifferenza.Text, 2, , , TriState.False)
                    End If

                    txtSorte.Text = Rs("sorte") & ""
                    If txtSorte.Text <> "" Then
                        txtSorte.Text = FormatNumber(txtSorte.Text, 2, , , TriState.False)
                    End If

                    txtSvalutazione.Text = Rs("svalutazione") & ""
                    If txtSvalutazione.Text <> "" Then
                        txtSvalutazione.Text = FormatNumber(txtSvalutazione.Text, 2, , , TriState.False)
                    End If

                    txtFermoMacchina.Text = Rs("fermo_macchina") & ""
                    If txtFermoMacchina.Text <> "" Then
                        txtFermoMacchina.Text = FormatNumber(txtFermoMacchina.Text, 2, , , TriState.False)
                    End If

                    txtTotale.Text = Rs("totale") & ""
                    If txtTotale.Text <> "" Then
                        txtTotale.Text = FormatNumber(txtTotale.Text, 2, , , TriState.False)
                    End If

                    'txtCompensoSBC.Text = Rs("compenso_sbc") & ""
                    'If txtCompensoSBC.Text <> "" Then
                    '    txtCompensoSBC.Text = FormatNumber(txtCompensoSBC.Text, 2, , , TriState.False)
                    'End If

                    txtSpese.Text = Rs("spese") & ""
                    If txtSpese.Text <> "" Then
                        txtSpese.Text = FormatNumber(txtSpese.Text, 2, , , TriState.False)
                    End If

                    txtOnorarioAvvocato.Text = Rs("onorario_avvocato") & ""
                    If txtOnorarioAvvocato.Text <> "" Then
                        txtOnorarioAvvocato.Text = FormatNumber(txtOnorarioAvvocato.Text, 2, , , TriState.False)
                    End If

                    txtLiquidato.Text = Rs("importo_liquidato") & ""
                    If txtLiquidato.Text <> "" Then
                        txtLiquidato.Text = FormatNumber(txtLiquidato.Text, 2, , , TriState.False)
                    End If

                    txtCompetenzeAEC.Text = Rs("competenze_aec") & ""
                    If txtCompetenzeAEC.Text <> "" Then
                        txtCompetenzeAEC.Text = FormatNumber(txtCompetenzeAEC.Text, 2, , , TriState.False)
                    End If

                    txtTotaleParziale.Text = Rs("totale_parziale") & ""
                    If txtTotaleParziale.Text <> "" Then
                        txtTotaleParziale.Text = FormatNumber(txtTotaleParziale.Text, 2, , , TriState.False)
                    End If

                    txtSpesePerizie.Text = Rs("spese_aec_perizie") & ""
                    If txtSpesePerizie.Text <> "" Then
                        txtSpesePerizie.Text = FormatNumber(txtSpesePerizie.Text, 2, , , TriState.False)
                    End If

                    txtSpeseFotografie.Text = Rs("spese_aec_fotografie") & ""
                    If txtSpeseFotografie.Text <> "" Then
                        txtSpeseFotografie.Text = FormatNumber(txtSpeseFotografie.Text, 2, , , TriState.False)
                    End If

                    txtSpesePostali.Text = Rs("spese_aec_postali") & ""
                    If txtSpesePostali.Text <> "" Then
                        txtSpesePostali.Text = FormatNumber(txtSpesePostali.Text, 2, , , TriState.False)
                    End If

                    txtSpeseVisure.Text = Rs("spese_aec_visure") & ""
                    If txtSpeseVisure.Text <> "" Then
                        txtSpeseVisure.Text = FormatNumber(txtSpeseVisure.Text, 2, , , TriState.False)
                    End If

                    txtSpeseCompensi.Text = Rs("spese_aec_compensi") & ""
                    If txtSpeseCompensi.Text <> "" Then
                        txtSpeseCompensi.Text = FormatNumber(txtSpeseCompensi.Text, 2, , , TriState.False)
                    End If

                    txtTotaleSpeseAEC.Text = Rs("totale_spese_aec") & ""
                    If txtTotaleSpeseAEC.Text <> "" Then
                        txtTotaleSpeseAEC.Text = FormatNumber(txtTotaleSpeseAEC.Text, 2, , , TriState.False)
                    End If

                    txtTotaleGlobale.Text = Rs("totale_globale") & ""
                    If txtTotaleGlobale.Text <> "" Then
                        txtTotaleGlobale.Text = FormatNumber(txtTotaleGlobale.Text, 2, , , TriState.False)
                    End If

                    txtSortePagata.Text = Rs("sorte_pagato") & ""
                    If txtSortePagata.Text <> "" Then
                        txtSortePagata.Text = FormatNumber(txtSortePagata.Text, 2, , , TriState.False)
                    End If

                    txtSvalutazionePagata.Text = Rs("svalutazione_pagato") & ""
                    If txtSvalutazionePagata.Text <> "" Then
                        txtSvalutazionePagata.Text = FormatNumber(txtSvalutazionePagata.Text, 2, , , TriState.False)
                    End If

                    txtFermoMacchinaPagata.Text = Rs("fermo_macchina_pagato") & ""
                    If txtFermoMacchinaPagata.Text <> "" Then
                        txtFermoMacchinaPagata.Text = FormatNumber(txtFermoMacchinaPagata.Text, 2, , , TriState.False)
                    End If

                    txtTotalePagata.Text = Rs("totale_pagato") & ""
                    If txtTotalePagata.Text <> "" Then
                        txtTotalePagata.Text = FormatNumber(txtTotalePagata.Text, 2, , , TriState.False)
                    End If

                    txtCompetenzeAECPagato.Text = Rs("competenze_aec_pagato") & ""
                    If txtCompetenzeAECPagato.Text <> "" Then
                        txtCompetenzeAECPagato.Text = FormatNumber(txtCompetenzeAECPagato.Text, 2, , , TriState.False)
                    End If

                    txtTotaleGlobalePagato.Text = Rs("totale_globale_pagato") & ""
                    If txtTotaleGlobalePagato.Text <> "" Then
                        txtTotaleGlobalePagato.Text = FormatNumber(txtTotaleGlobalePagato.Text, 2, , , TriState.False)
                    End If

                    If (Rs("anticipo_spese_il") & "") <> "" Then
                        txtAnticipoSpeseIl.Text = Day(Rs("anticipo_spese_il")) & "/" & Month(Rs("anticipo_spese_il")) & "/" & Year(Rs("anticipo_spese_il"))
                    Else
                        txtAnticipoSpeseIl.Text = ""
                    End If

                    txtAnticipoPerImporto.Text = Rs("anticipo_spese_importo") & ""
                    If txtAnticipoPerImporto.Text <> "" Then
                        txtAnticipoPerImporto.Text = FormatNumber(txtAnticipoPerImporto.Text, 2, , , TriState.False)
                    End If

                    txtAnticipoSuImporto.Text = Rs("anticipo_spese_su_totale") & ""
                    If txtAnticipoSuImporto.Text <> "" Then
                        txtAnticipoSuImporto.Text = FormatNumber(txtAnticipoSuImporto.Text, 2, , TriState.False)
                    End If

                    If (Rs("rimborso_spese_il") & "") <> "" Then
                        txtRimborsoSpeseIl.Text = Day(Rs("rimborso_spese_il")) & "/" & Month(Rs("rimborso_spese_il")) & "/" & Year(Rs("rimborso_spese_il"))
                    Else
                        txtRimborsoSpeseIl.Text = ""
                    End If
                    txtRimborsoSpeseImporto.Text = Rs("rimborso_spese_importo") & ""
                    If (Rs("atto_citazione_inviato_il") & "") <> "" Then
                        txtAttoCitazioneInviatoIl.Text = Day(Rs("atto_citazione_inviato_il")) & "/" & Month(Rs("atto_citazione_inviato_il")) & "/" & Year(Rs("atto_citazione_inviato_il"))
                    Else
                        txtAttoCitazioneInviatoIl.Text = ""
                    End If

                    'FATTURE
                    txtImportoFatturaSBC.Text = Rs("importo_fattura_sbc") & ""
                    If txtImportoFatturaSBC.Text <> "" Then
                        txtImportoFatturaSBC.Text = FormatNumber(txtImportoFatturaSBC.Text, 2, , , TriState.False)
                    End If

                    If (Rs("data_fattura_sbc") & "") <> "" Then
                        txtFatturaSBCData.Text = Day(Rs("data_fattura_sbc")) & "/" & Month(Rs("data_fattura_sbc")) & "/" & Year(Rs("data_fattura_sbc"))
                    Else
                        txtFatturaSBCData.Text = ""
                    End If
                    txtFatturaSbcNumero.Text = Rs("numero_fattura_sbc") & ""


                    txtImportoFatturaAvv.Text = Rs("importo_fattura_avv") & ""
                    If txtImportoFatturaAvv.Text <> "" Then
                        txtImportoFatturaAvv.Text = FormatNumber(txtImportoFatturaAvv.Text, 2, , , TriState.False)
                    End If
                    If (Rs("data_fattura_avv") & "") <> "" Then
                        txtFatturaAvvData.Text = Day(Rs("data_fattura_avv")) & "/" & Month(Rs("data_fattura_avv")) & "/" & Year(Rs("data_fattura_avv"))
                    Else
                        txtFatturaAvvData.Text = ""
                    End If
                    txtFatturaAvvNumero.Text = Rs("numero_fattura_avv") & ""


                    txtImportoFatturaOnorarioAvvocato.Text = Rs("importo_fattura_onorario_lordo") & ""
                    If txtImportoFatturaOnorarioAvvocato.Text <> "" Then
                        txtImportoFatturaOnorarioAvvocato.Text = FormatNumber(txtImportoFatturaOnorarioAvvocato.Text, 2, , , TriState.False)
                    End If
                    If (Rs("data_fattura_onorario_lordo") & "") <> "" Then
                        txtDataFatturaOnorarioAvvocato.Text = Day(Rs("data_fattura_onorario_lordo")) & "/" & Month(Rs("data_fattura_onorario_lordo")) & "/" & Year(Rs("data_fattura_onorario_lordo"))
                    Else
                        txtDataFatturaOnorarioAvvocato.Text = ""
                    End If
                    txtNumeroFatturaOnorarioAvvocato.Text = Rs("numero_fattura_onorario_lordo") & ""

                    txtImportoFatturaOnorarioAvvocatoNetto.Text = Rs("importo_onorario_netto") & ""
                    If txtImportoFatturaOnorarioAvvocatoNetto.Text <> "" Then
                        txtImportoFatturaOnorarioAvvocatoNetto.Text = FormatNumber(txtImportoFatturaOnorarioAvvocatoNetto.Text, 2, , , TriState.False)
                    End If
                    If (Rs("data_pagamento_onorario_netto") & "") <> "" Then
                        txtDataPagamentoOnorarioNetto.Text = Day(Rs("data_pagamento_onorario_netto")) & "/" & Month(Rs("data_pagamento_onorario_netto")) & "/" & Year(Rs("data_pagamento_onorario_netto"))
                    Else
                        txtDataPagamentoOnorarioNetto.Text = ""
                    End If
                End If

            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("err fill_dati_sinistro : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try


    End Sub

    Protected Sub formatta_campi()

        If txtFranchigieContestate.Text <> "" Then
            txtFranchigieContestate.Text = FormatNumber(txtFranchigieContestate.Text, 2, , , TriState.False)
        End If

        If txtAddebitoApplicato.Text <> "" Then
            txtAddebitoApplicato.Text = FormatNumber(txtAddebitoApplicato.Text, 2, , , TriState.False)
        End If

        If txtImportoRimborso.Text <> "" Then
            txtImportoRimborso.Text = FormatNumber(txtImportoRimborso.Text, 2, , , TriState.False)
        End If

        If txtSko.Text <> "" Then
            txtSko.Text = FormatNumber(txtSko.Text, 2, , , TriState.False)
        End If

        If txtImportoRimborsoAClienteSbc.Text <> "" Then
            txtImportoRimborsoAClienteSbc.Text = FormatNumber(txtImportoRimborsoAClienteSbc.Text, 2, , , TriState.False)
        End If

        If txtAccontoLiquidazioneSinistro.Text <> "" Then
            txtAccontoLiquidazioneSinistro.Text = FormatNumber(txtAccontoLiquidazioneSinistro.Text, 2, , , TriState.False)
        End If

        If txtRichiestaLiquidazioneDanni.Text <> "" Then
            txtRichiestaLiquidazioneDanni.Text = FormatNumber(txtRichiestaLiquidazioneDanni.Text, 2, , , TriState.False)
        End If

        If txtDifferenza.Text <> "" Then
            txtDifferenza.Text = FormatNumber(txtDifferenza.Text, 2, , , TriState.False)
        End If

        If txtSorte.Text <> "" Then
            txtSorte.Text = FormatNumber(txtSorte.Text, 2, , , TriState.False)
        End If

        If txtSvalutazione.Text <> "" Then
            txtSvalutazione.Text = FormatNumber(txtSvalutazione.Text, 2, , , TriState.False)
        End If

        If txtFermoMacchina.Text <> "" Then
            txtFermoMacchina.Text = FormatNumber(txtFermoMacchina.Text, 2, , , TriState.False)
        End If

        If txtTotale.Text <> "" Then
            txtTotale.Text = FormatNumber(txtTotale.Text, 2, , , TriState.False)
        End If

        'If txtCompensoSBC.Text <> "" Then
        '    txtCompensoSBC.Text = FormatNumber(txtCompensoSBC.Text, 2, , , TriState.False)
        'End If

        If txtSpese.Text <> "" Then
            txtSpese.Text = FormatNumber(txtSpese.Text, 2, , , TriState.False)
        End If

        If txtOnorarioAvvocato.Text <> "" Then
            txtOnorarioAvvocato.Text = FormatNumber(txtOnorarioAvvocato.Text, 2, , , TriState.False)
        End If

        If txtLiquidato.Text <> "" Then
            txtLiquidato.Text = FormatNumber(txtLiquidato.Text, 2, , , TriState.False)
        End If

        If txtCompetenzeAEC.Text <> "" Then
            txtCompetenzeAEC.Text = FormatNumber(txtCompetenzeAEC.Text, 2, , , TriState.False)
        End If

        If txtTotaleParziale.Text <> "" Then
            txtTotaleParziale.Text = FormatNumber(txtTotaleParziale.Text, 2, , , TriState.False)
        End If

        If txtSpesePerizie.Text <> "" Then
            txtSpesePerizie.Text = FormatNumber(txtSpesePerizie.Text, 2, , , TriState.False)
        End If

        If txtSpeseFotografie.Text <> "" Then
            txtSpeseFotografie.Text = FormatNumber(txtSpeseFotografie.Text, 2, , , TriState.False)
        End If

        If txtSpesePostali.Text <> "" Then
            txtSpesePostali.Text = FormatNumber(txtSpesePostali.Text, 2, , , TriState.False)
        End If

        If txtSpeseVisure.Text <> "" Then
            txtSpeseVisure.Text = FormatNumber(txtSpeseVisure.Text, 2, , , TriState.False)
        End If

        If txtSpeseCompensi.Text <> "" Then
            txtSpeseCompensi.Text = FormatNumber(txtSpeseCompensi.Text, 2, , , TriState.False)
        End If

        If txtTotaleSpeseAEC.Text <> "" Then
            txtTotaleSpeseAEC.Text = FormatNumber(txtTotaleSpeseAEC.Text, 2, , , TriState.False)
        End If

        If txtTotaleGlobale.Text <> "" Then
            txtTotaleGlobale.Text = FormatNumber(txtTotaleGlobale.Text, 2, , , TriState.False)
        End If

        If txtSortePagata.Text <> "" Then
            txtSortePagata.Text = FormatNumber(txtSortePagata.Text, 2, , , TriState.False)
        End If


        If txtSvalutazionePagata.Text <> "" Then
            txtSvalutazionePagata.Text = FormatNumber(txtSvalutazionePagata.Text, 2, , , TriState.False)
        End If

        If txtFermoMacchinaPagata.Text <> "" Then
            txtFermoMacchinaPagata.Text = FormatNumber(txtFermoMacchinaPagata.Text, 2, , , TriState.False)
        End If

        If txtTotalePagata.Text <> "" Then
            txtTotalePagata.Text = FormatNumber(txtTotalePagata.Text, 2, , , TriState.False)
        End If


        If txtCompetenzeAECPagato.Text <> "" Then
            txtCompetenzeAECPagato.Text = FormatNumber(txtCompetenzeAECPagato.Text, 2, , , TriState.False)
        End If

        If txtTotaleGlobalePagato.Text <> "" Then
            txtTotaleGlobalePagato.Text = FormatNumber(txtTotaleGlobalePagato.Text, 2, , , TriState.False)
        End If

        If txtAnticipoPerImporto.Text <> "" Then
            txtAnticipoPerImporto.Text = FormatNumber(txtAnticipoPerImporto.Text, 2, , , TriState.False)
        End If

        If txtAnticipoSuImporto.Text <> "" Then
            txtAnticipoSuImporto.Text = FormatNumber(txtAnticipoSuImporto.Text, 2, , TriState.False)
        End If

        If txtRimborsoSpeseImporto.Text <> "" Then
            txtRimborsoSpeseImporto.Text = FormatNumber(txtRimborsoSpeseImporto.Text, 2, , TriState.False)
        End If

        'FATTURE
        If txtImportoFatturaSBC.Text <> "" Then
            txtImportoFatturaSBC.Text = FormatNumber(txtImportoFatturaSBC.Text, 2, , , TriState.False)
        End If

        If txtImportoFatturaAvv.Text <> "" Then
            txtImportoFatturaAvv.Text = FormatNumber(txtImportoFatturaAvv.Text, 2, , , TriState.False)
        End If

        If txtImportoFatturaOnorarioAvvocato.Text <> "" Then
            txtImportoFatturaOnorarioAvvocato.Text = FormatNumber(txtImportoFatturaOnorarioAvvocato.Text, 2, , , TriState.False)
        End If

        If txtImportoFatturaOnorarioAvvocatoNetto.Text <> "" Then
            txtImportoFatturaOnorarioAvvocatoNetto.Text = FormatNumber(txtImportoFatturaOnorarioAvvocatoNetto.Text, 2, , , TriState.False)
        End If
    End Sub

    Protected Sub fill_dati_nuovo_sinistro()

        Dim sqlstr As String = "SELECT contratti.num_contratto, contratti.id_veicolo, contratti.targa, contratti.modello, "
        sqlstr += "contratti.id_stazione_rientro, (stazioni.codice + ' - ' + stazioni.nome_stazione) As stazione, proprietari_veicoli.descrizione As proprietario, veicoli.id_proprietario As id_proprietario_veicolo FROM contratti WITH(NOLOCK) INNER JOIN stazioni WITH(NOLOCK) ON contratti.id_stazione_rientro=stazioni.id INNER JOIN veicoli WITH(NOLOCK) ON contratti.id_veicolo=veicoli.id LEFT JOIN proprietari_veicoli WITH(NOLOCK) ON veicoli.id_proprietario=proprietari_veicoli.id WHERE contratti.id='" & id_contratto.Text & "'"
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
        Dim Rs As Data.SqlClient.SqlDataReader


        Try

            'RECUPERO DATI DA CONTRATTO
            Rs = Cmd.ExecuteReader()

            If Rs.Read() Then
                txtNumContratto.Text = Rs("num_contratto") & ""
                txtTarga.Text = Rs("targa")
                txtModello.Text = Rs("modello")
                id_stazione.Text = Rs("id_stazione_rientro")
                txtStazione.Text = Rs("stazione")
                id_proprietario_veicolo.Text = Rs("id_proprietario_veicolo")
                lblProprietarioVeicolo.Text = Rs("proprietario")
                id_veicolo.Text = Rs("id_veicolo")
            Else
                'DANNO DA PIAZZALE - RECUPERO I DATI DEL VEICOLO DALL'RDS
                Dbc.Close()
                Dbc.Open()
                Cmd = New Data.SqlClient.SqlCommand("SELECT  veicoli_evento_apertura_danno.id_veicolo, modelli.descrizione As modello, veicoli.targa, veicoli.id_stazione, (stazioni.codice + ' - ' + stazioni.nome_stazione) As stazione, proprietari_veicoli.descrizione As proprietario, veicoli.id_proprietario As id_proprietario_veicolo FROM veicoli_evento_apertura_danno WITH(NOLOCK) INNER JOIN veicoli WITH(NOLOCK) ON veicoli_evento_apertura_danno.id_veicolo=veicoli.id INNER JOIN modelli ON veicoli.id_modello=modelli.id_modello INNER JOIN stazioni WITH(NOLOCK) ON veicoli.id_stazione =stazioni.id  LEFT JOIN proprietari_veicoli WITH(NOLOCK) ON veicoli.id_proprietario=proprietari_veicoli.id WHERE veicoli_evento_apertura_danno.id='" & id_rds.Text & "'", Dbc)
                Rs = Cmd.ExecuteReader()
                If Rs.Read() Then
                    txtTarga.Text = Rs("targa")
                    txtNumContratto.Text = ""
                    txtModello.Text = Rs("modello")
                    id_stazione.Text = Rs("id_stazione")
                    txtStazione.Text = Rs("stazione")
                    id_proprietario_veicolo.Text = Rs("id_proprietario_veicolo")
                    lblProprietarioVeicolo.Text = Rs("proprietario")
                    id_veicolo.Text = Rs("id_veicolo")
                Else
                    Rs.Close()
                    Rs = Nothing
                    Dbc.Close()
                    Dbc.Open()

                    Response.Redirect("default.aspx")
                End If
            End If

            'RECUPERO DATI DA RDS (IMPORTO E DATA INCIDENTE POSSONO NON ESSERE VALORIZZATI)
            Rs.Close()
            Rs = Nothing
            Dbc.Close()


        Catch ex As Exception
            Response.Write("err ill_dati_nuovo_sinistro 1 : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try


        Try
            Dbc.Open()
            Cmd = New Data.SqlClient.SqlCommand("SELECT id_rds, data_incidente, importo FROM veicoli_evento_apertura_danno WITH(NOLOCK) WHERE id='" & id_rds.Text & "'", Dbc)
            Cmd.ExecuteNonQuery()

            Rs = Cmd.ExecuteReader()

            If Rs.Read() Then
                txtNumeroRDS.Text = Rs("id_rds")
                If (Rs("data_incidente") & "") <> "" Then
                    txtDataEvento.Text = Day(Rs("data_incidente")) & "/" & Month(Rs("data_incidente")) & "/" & Year(Rs("data_incidente"))
                End If
                txtAddebitoApplicato.Text = Rs("importo") & ""
            Else
                Response.Redirect("default.aspx")
            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("err ill_dati_nuovo_sinistro 2 : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try

    End Sub

    Protected Function check_record() As Boolean
        'LA FUNZIONE CONTROLLA CHE IL RECORD CHE SI STA CERCANDO DI SALVARE SIA QUELLO ATTUALMENTE ATTIVO - NON LO E' QUANDO L'UTENTE 
        'TORNA INDIENTRO COL TASTO NAVIGAZIONE DEL BROWSER

        Dim sqlstr As String = "SELECT id FROM sinistri WITH(NOLOCK) WHERE ISNULL(num_contratto,'')='" & Trim(txtNumContratto.Text) & "' AND numero_rds='" & txtNumeroRDS.Text & "' AND attivo='1'"

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Try
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            ' Response.Write("SELECT id FROM sinistri WITH(NOLOCK) WHERE num_contratto='" & txtNumContratto.Text & "' AND numero_rds='" & txtNumeroRDS.Text & "' AND attivo='1'")
            'Response.End()
            Dim test As String = Cmd.ExecuteScalar & ""

            If test = "" And (stato.Text = "0" Or stato.Text = "-1") Then
                'IL DOCUMENTO NON E' ANCORA STATO SALVATO PERCHE' NON ESISTE NESSUN RECORD ATTIVO CON I DATI SPECIFICATI
                check_record = True
            ElseIf test = "" And (stato.Text = "1" Or stato.Text = "2" Or stato.Text = "3") Then
                'IL DOCUMENTO NON ESISTE PIU' (CANCELLATO?) - NON DOVREBBE MAI SUCCEDERE
                check_record = False
            ElseIf test <> "" Then
                If test = id_sinistro.Text Then
                    'IL RECORD CHE SI STA MODIFICANDO E' QUELLO EFFETTIVAMENTE ATTIVO
                    check_record = True
                Else
                    'L'UTENTE E' TORNATO INDIETRO COI TASTI DEL BROWSER - LA RIGA NON E' PIU' ATTIVA!!!
                    check_record = False
                End If
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("err check_record : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try


    End Function

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click


        If txtDataEvento.Text <> "" Then
            If dropTipologia.SelectedValue <> "0" Then
                If (dropGestitoDa.SelectedValue <> "0") Or (Not div_importi_sinistro_attivo.Visible) Then
                    If check_record() Then
                        '---------------------------------------------------------------------------------------------------------------------
                        If stato.Text = "0" Then
                            'SALVATAGGIO DI UN NUOVO SINISTRO ATTIVO
                            salva_sinistro_attivo()

                            formatta_campi()
                        ElseIf stato.Text = "1" Then
                            'MODIFICA DI UN SINISTRO ATTIVO - NON DEFINITO
                            'SE IL SINISTRO DEVE DIVENTARE DEFINITO DEVE AVERE I QUATTRO CAMPI RELATIVI TUTTI VALORIZZATI
                            If (txtRichiestaLiquidazioneDanni.Text = "" And txtDifferenza.Text = "" And txtLiquidato.Text = "") Then
                                salva_sinistro_attivo()
                                formatta_campi()
                            Else
                                If (txtRichiestaLiquidazioneDanni.Text <> "" And txtDifferenza.Text <> "" And txtLiquidato.Text <> "") Then
                                    salva_sinistro_attivo()
                                    formatta_campi()
                                Else
                                    Dim msg As String = "Attenzione: è necessario speficiare i campi " & vbCrLf & "- Richiesta di liquidazione danni" & vbCrLf &
                                    "- Differenza " & vbCrLf &
                                    "- Liquidato" & vbCrLf &
                                    "per poter salvare il sinistro come DEFINITO"
                                    Libreria.genUserMsgBox(Me, msg)
                                End If
                            End If
                        ElseIf stato.Text = "2" Then
                            'MODIFICA DI UN SINISTRO ATTIVO - DEFINITO
                            If (txtRichiestaLiquidazioneDanni.Text <> "" And txtDifferenza.Text <> "" And txtLiquidato.Text <> "") Then
                                salva_sinistro_attivo()
                                formatta_campi()
                            Else
                                Dim msg As String = "Attenzione: è necessario speficiare i campi " & vbCrLf & "- Richiesta di liquidazione danni" & vbCrLf &
                                    "- Differenza " & vbCrLf &
                                    "- Liquidato" & vbCrLf &
                                    "per poter modificare l'attuale sinistro DEFINITO."
                                Libreria.genUserMsgBox(Me, msg)
                            End If

                        ElseIf stato.Text = "-1" Then
                            'SALVATAGGIO DI UN SINISTRO PASSIVO
                            salva_sinistro_passivo()
                            formatta_campi()
                        ElseIf stato.Text = "3" And id_sinistro.Text <> "" Then
                            'MODIFICA DI UN SINISTRO PASSIVO
                            salva_sinistro_passivo()
                            formatta_campi()
                        End If
                        '----------------------------------------------------------------------------------------------------------------------
                    Else
                        'L'UTENTE E' TORNATO INDIETRO COL TASTO DI NAVIGAZIONE DEL BROWSER E STA TENTANDO DI MODIFICARE UN CAMPO NON PIU' ATTIVO
                        Dim anno As String = txtAnno.Text
                        Dim protocollo_interno As String
                        If stato.Text = "0" Or stato.Text = "-1" Then
                            'NEL CASO IN CUI LO STATO VECCHIO CORRISPONDA AD UN DOCUMENTO CHE ERA ANCORA DA SALVARE RECUPER IL PROTOCOLLO INTERNO
                            'AD ESSO ASSEGANTO
                            protocollo_interno = getProtocolloInterno(txtNumContratto.Text, txtNumeroRDS.Text)
                        Else
                            protocollo_interno = txtProtocollo.Text
                        End If


                        Session("sin_anno") = anno
                        Session("sin_proto") = protocollo_interno
                        Session("sin_err") = "Si è verificato un errore: è stato ripristinato il sinistro attualmente salvato."

                        Response.Redirect("sinistri.aspx")
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Specificare chi sta gestendo la pratica.")
                End If
            Else
                Libreria.genUserMsgBox(Me, "Specificare la tipologia di sinistro.")
            End If
        Else
            Libreria.genUserMsgBox(Me, "Specificare la data del sinistro.")
        End If
    End Sub

    Protected Function getProtocolloInterno(ByVal contratto As String, ByVal rds As String) As String

        Dim sqlstr As String = "SELECT numero_protocollo_interno FROM sinistri WITH(NOLOCK) WHERE num_contratto='" & contratto & "' AND numero_rds='" & txtNumeroRDS.Text & "' AND attivo='1'"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            getProtocolloInterno = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("err getProtocolloInterno : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try


    End Function

    Protected Function get_valore_data(ByVal campo As TextBox) As String
        If Trim(campo.Text) = "" Then
            get_valore_data = "NULL"
        Else
            get_valore_data = "convert(datetime,'" & funzioni_comuni.getDataDb_senza_orario(campo.Text) & "',102)"
        End If
    End Function

    Protected Function get_stringa(ByVal campo As TextBox) As String
        If Trim(campo.Text) = "" Then
            get_stringa = "NULL"
        Else
            get_stringa = "'" & Replace(campo.Text, "'", "''") & "'"
        End If
    End Function

    Protected Function get_double(ByVal campo As TextBox) As String
        If Trim(campo.Text) = "" Then
            get_double = "NULL"
        Else
            get_double = "'" & Replace(campo.Text, ",", ".") & "'"
        End If
    End Function


    Protected Sub salva_sinistro_attivo()


        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

        Dim sqlStr As String
        Dim sqlStrOldRecord As String = ""

        Try
            If stato.Text = "0" Then
                'SOLO SU PRIMO SALVATAGGIO

                'SELEZIONE DEL NUMERO DI SINISTRO - UNICO PER ANNO
                sqlStr = "SELECT ISNULL(MAX(NUMERO),0) FROM sinistri WITH(NOLOCK) WHERE anno='" & txtAnno.Text & "' AND id_proprietario_veicolo='" & id_proprietario_veicolo.Text & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                txtNumeroSinistro.Text = CInt(Cmd.ExecuteScalar) + 1

                'SELEZIONE DEL NUMERO DI PROTOCOLLO INTERNO - UNICO PER ANNO E PROPRIETARIO
                sqlStr = "SELECT ISNULL(MAX(numero_protocollo_interno),0) FROM sinistri WITH(NOLOCK) WHERE anno='" & txtAnno.Text & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                txtProtocollo.Text = CInt(Cmd.ExecuteScalar) + 1

                stato.Text = "1"

                'IL SINISTRO VERRA' SALVATO E PUO', DA QUESTO MOMENTO IN POI, DIVENTARE DEFINITO
                lblTipoSinistro.Text = "SINISTRO ATTIVO - NON DEFINITO"
            ElseIf stato.Text = "1" And (txtRichiestaLiquidazioneDanni.Text = "" And txtDifferenza.Text = "" And txtLiquidato.Text = "") Then
                'MODIFICA DI SINISTRO ATTIVO CHE RESTA NON DEFINITO

                'SETTO IL RECORD ATTUALE COME NON PIU' ATTIVO
                sqlStrOldRecord = "UPDATE sinistri SET attivo='0' WHERE id='" & id_sinistro.Text & "'"


            ElseIf stato.Text = "1" And (txtRichiestaLiquidazioneDanni.Text <> "" And txtDifferenza.Text <> "" And txtLiquidato.Text <> "") Then
                'IL SINISTRO DIVENTA DEFINITO
                stato.Text = "2"
                lblTipoSinistro.Text = "SINISTRO ATTIVO - DEFINITO"
                txtDataDefinizione.Text = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())

                'SETTO IL RECORD ATTUALE COME NON PIU' ATTIVO
                sqlStrOldRecord = "UPDATE sinistri SET attivo='0' WHERE id='" & id_sinistro.Text & "'"

            ElseIf stato.Text = "2" Then
                'IL SINISTRO E' DIFINITO E RESTA TALE

                sqlStrOldRecord = "UPDATE sinistri SET attivo='0' WHERE id='" & id_sinistro.Text & "'"

            End If

            sqlStr = "INSERT INTO sinistri (attivo,id_stato, id_tipologia ,anno , numero_protocollo_interno, numero, numero_sinistro_compagnia, id_veicolo, data_evento, data_apertura_sinistro," &
        "id_rds,numero_rds, num_contratto,targa, modello, id_proprietario_veicolo, proprietario_veicolo, id_stazione, stazione, guidatore, controparte, compagnia_controparte," &
        "franchigia_compagnia, franchigia_compagnia_pagata_il, franchigie_contestate, addebito_applicato, data_rimborso," &
        "importo_rimborso, sko, id_gestito_da, gestito_da_in_data, sorte, svalutazione, fermo_macchina, totale, " &
        "competenze_aec, totale_parziale, spese_aec_perizie, spese_aec_fotografie, spese_aec_postali, spese_aec_visure, " &
        "spese_aec_compensi, totale_spese_aec, totale_globale, sorte_pagato, svalutazione_pagato, fermo_macchina_pagato, " &
        "totale_pagato, competenze_aec_pagato, totale_globale_pagato," &
        "spese, onorario_avvocato, importo_liquidato, percentuale_concorsuale," &
        "anticipo_spese_il, anticipo_spese_importo, anticipo_spese_su_totale, rimborso_spese_il, rimborso_spese_importo, atto_citazione_inviato_il," &
        "acconto_liquidazione_sinistro, richiesta_liquidazione_danni, differenza, " &
        "data_rimborso_a_cliente_sbc, importo_rimborso_a_cliente_sbc, " &
        "importo_fattura_sbc, data_fattura_sbc, numero_fattura_sbc, importo_fattura_avv, data_fattura_avv, numero_fattura_avv," &
        "importo_fattura_onorario_lordo, data_fattura_onorario_lordo, numero_fattura_onorario_lordo, " &
        "importo_onorario_netto, data_pagamento_onorario_netto," &
        "id_operatore_creazione, data_creazione, data_definizione) VALUES "


            sqlStr = sqlStr & "('1','" & stato.Text & "','" & dropTipologia.SelectedValue & "'," & get_stringa(txtAnno) & ",'" & txtProtocollo.Text & "','" & txtNumeroSinistro.Text & "'," & get_stringa(txtNumSinistroCompagnia) & ",'" & id_veicolo.Text & "'," &
        get_valore_data(txtDataEvento) & "," & get_valore_data(txtDataAperturaSinistro) & ",'" & id_rds.Text & "'," & get_stringa(txtNumeroRDS) & "," &
        get_stringa(txtNumContratto) & "," & get_stringa(txtTarga) & "," & get_stringa(txtModello) & ",'" & id_proprietario_veicolo.Text & "','" & Replace(lblProprietarioVeicolo.Text, "'", "''") & "','" & id_stazione.Text & "'," &
        get_stringa(txtStazione) & "," & get_stringa(txtGuidatore) & "," & get_stringa(txtControparte) & "," & get_stringa(txtCompagniaControparte) & "," &
        get_double(txtFranchigiaCompagnia) & "," & get_valore_data(txtFranchigiaCompagniaPagataIl) & "," & get_double(txtFranchigieContestate) & "," &
        get_double(txtAddebitoApplicato) & "," & get_valore_data(txtDataRimborso) & "," & get_double(txtImportoRimborso) & "," &
        get_double(txtSko) & ",'" & dropGestitoDa.SelectedValue & "'," & get_valore_data(txtGestitaDaInData) & "," &
        get_double(txtSorte) & "," & get_double(txtSvalutazione) & "," & get_double(txtFermoMacchina) & "," & get_double(txtTotale) & "," &
        get_double(txtCompetenzeAEC) & "," & get_double(txtTotaleParziale) & "," & get_double(txtSpesePerizie) & "," &
        get_double(txtSpeseFotografie) & "," & get_double(txtSpesePostali) & "," & get_double(txtSpeseVisure) & "," &
        get_double(txtSpeseCompensi) & "," & get_double(txtTotaleSpeseAEC) & "," & get_double(txtTotaleGlobale) & "," &
        get_double(txtSortePagata) & "," & get_double(txtSvalutazionePagata) & "," & get_double(txtFermoMacchinaPagata) & "," &
        get_double(txtTotalePagata) & "," & get_double(txtCompetenzeAECPagato) & "," & get_double(txtTotaleGlobalePagato) & "," &
        get_double(txtSpese) & "," & get_double(txtOnorarioAvvocato) & "," & get_double(txtLiquidato) & "," & get_double(txtPercentualeConcorsuale) & "," &
        get_valore_data(txtAnticipoSpeseIl) & "," & get_double(txtAnticipoPerImporto) & "," & get_double(txtAnticipoSuImporto) & "," &
        get_valore_data(txtRimborsoSpeseIl) & "," & get_double(txtRimborsoSpeseImporto) & "," & get_valore_data(txtAttoCitazioneInviatoIl) & "," &
        get_double(txtAccontoLiquidazioneSinistro) & "," & get_double(txtRichiestaLiquidazioneDanni) & "," & get_double(txtDifferenza) & "," &
        get_valore_data(txtDataRimborsoClienteSBC) & "," & get_double(txtImportoRimborsoAClienteSbc) & "," &
        get_double(txtImportoFatturaSBC) & "," & get_valore_data(txtFatturaSBCData) & "," & get_stringa(txtFatturaSbcNumero) & "," &
        get_double(txtImportoFatturaAvv) & "," & get_valore_data(txtFatturaAvvData) & "," & get_stringa(txtFatturaAvvNumero) & "," &
        get_double(txtImportoFatturaOnorarioAvvocato) & "," & get_valore_data(txtDataFatturaOnorarioAvvocato) & "," & get_stringa(txtNumeroFatturaOnorarioAvvocato) & "," &
        get_double(txtImportoFatturaOnorarioAvvocatoNetto) & "," & get_valore_data(txtDataPagamentoOnorarioNetto) & "," &
        "'" & Request.Cookies("SicilyRentCar")("IdUtente") & "',CONVERT(DATETIME,GetDate(),102)," & get_valore_data(txtDataDefinizione) & ")"


            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            sqlStr = "SELECT @@IDENTITY FROM sinistri WITH(NOLOCK)"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            id_sinistro.Text = Cmd.ExecuteScalar

            'AGGIORNO LA RIGA PRECEDENTE COME NON PIU' ATTIVA
            If sqlStrOldRecord <> "" Then
                Cmd = New Data.SqlClient.SqlCommand(sqlStrOldRecord, Dbc)
                Cmd.ExecuteNonQuery()
            End If

            'SALVATAGGIO INFORMAZIONI SU TABELLA RDS
            veicoli_evento_apertura_danno.Salva_Sinistro(id_rds.Text, txtAnno.Text, txtProtocollo.Text)

            'SE SPECIFICATA SALVO LA NOTA
            If Trim(txtNote.Text) <> "" Then
                salva_nota()
                listNote.DataBind()
            End If
            btnSalvaNota.Visible = True

            txtLiquidato.ReadOnly = False
            txtDifferenza.ReadOnly = False
            btnStampaDatiLegale.Visible = True
            btnStampaSinistro.Visible = True

            Libreria.genUserMsgBox(Me, "Sinistro salvato correttamente.")

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("err salva_sinistro_attivo : " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try



    End Sub

    Protected Sub salva_sinistro_passivo()

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

        Dim sqlStr As String
        Dim sqlStrOldRecord As String = ""

        Try
            If stato.Text = "-1" Then
                'SOLO SU PRIMO SALVATAGGIO

                'SELEZIONE DEL NUMERO DI SINISTRO - UNICO PER ANNO
                sqlStr = "SELECT ISNULL(MAX(NUMERO),0) FROM sinistri WITH(NOLOCK) WHERE anno='" & txtAnno.Text & "' AND id_proprietario_veicolo='" & id_proprietario_veicolo.Text & "'"

                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                txtNumeroSinistro.Text = CInt(Cmd.ExecuteScalar) + 1

                'SELEZIONE DEL NUMERO DI PROTOCOLLO INTERNO - UNICO PER ANNO E PROPRIETARIO
                sqlStr = "SELECT ISNULL(MAX(numero_protocollo_interno),0) FROM sinistri WITH(NOLOCK) WHERE anno='" & txtAnno.Text & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                txtProtocollo.Text = CInt(Cmd.ExecuteScalar) + 1

                stato.Text = "3"
            Else
                sqlStrOldRecord = "UPDATE sinistri SET attivo='0' WHERE id='" & id_sinistro.Text & "'"
            End If

            sqlStr = "INSERT INTO sinistri (attivo,id_stato, id_tipologia ,anno , numero_protocollo_interno, numero, numero_sinistro_compagnia, id_veicolo, data_evento, data_apertura_sinistro," &
            "id_rds,numero_rds, num_contratto,targa, modello, id_proprietario_veicolo, proprietario_veicolo, id_stazione, stazione, guidatore, controparte, compagnia_controparte," &
            "franchigia_compagnia, franchigia_compagnia_pagata_il, franchigie_contestate, addebito_applicato, data_rimborso," &
            "importo_rimborso, sko,id_operatore_creazione, data_creazione) VALUES "

            sqlStr = sqlStr & "('1','" & stato.Text & "','" & dropTipologia.SelectedValue & "'," & get_stringa(txtAnno) & ",'" & txtProtocollo.Text & "','" & txtNumeroSinistro.Text & "'," & get_stringa(txtNumSinistroCompagnia) & ",'" & id_veicolo.Text & "'," &
            get_valore_data(txtDataEvento) & "," & get_valore_data(txtDataAperturaSinistro) & ",'" & id_rds.Text & "'," & get_stringa(txtNumeroRDS) & "," &
            get_stringa(txtNumContratto) & "," & get_stringa(txtTarga) & "," & get_stringa(txtModello) & ",'" & id_proprietario_veicolo.Text & "','" & Replace(lblProprietarioVeicolo.Text, "'", "''") & "','" & id_stazione.Text & "'," &
            get_stringa(txtStazione) & "," & get_stringa(txtGuidatore) & "," & get_stringa(txtControparte) & "," & get_stringa(txtCompagniaControparte) & "," &
            get_double(txtFranchigiaCompagnia) & "," & get_valore_data(txtFranchigiaCompagniaPagataIl) & "," & get_double(txtFranchigieContestate) & "," &
            get_double(txtAddebitoApplicato) & "," & get_valore_data(txtDataRimborso) & "," & get_valore_data(txtImportoRimborso) & "," &
            get_double(txtSko) & ",'" & Request.Cookies("SicilyRentCar")("IdUtente") & "',convert(datetime,GetDate(),102))"

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            sqlStr = "SELECT @@IDENTITY FROM sinistri WITH(NOLOCK)"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            id_sinistro.Text = Cmd.ExecuteScalar

            'SETTO IL RECORD PRECEDENTE COME NON PIU' ATTOVI
            If sqlStrOldRecord <> "" Then
                Cmd = New Data.SqlClient.SqlCommand(sqlStrOldRecord, Dbc)
                Cmd.ExecuteNonQuery()
            End If

            'SALVATAGGIO INFORMAZIONI SU TABELLA RDS
            veicoli_evento_apertura_danno.Salva_Sinistro(id_rds.Text, txtAnno.Text, txtProtocollo.Text)

            'SE SPECIFICATA SALVO LA NOTA
            If Trim(txtNote.Text) <> "" Then
                salva_nota()
                listNote.DataBind()
            End If
            btnSalvaNota.Visible = True

            Libreria.genUserMsgBox(Me, "Sinistro salvato correttamente.")

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("err salva_sinistro_passivo : " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try


    End Sub

    Protected Sub btnCalcola_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalcola.Click
        If txtRichiestaLiquidazioneDanni.Text = "" Or txtLiquidato.Text = "" Then
            Libreria.genUserMsgBox(Me, "Specificare sia la richiesta di liquidazione danni che l'importo liquidato per calcolare la differenza.")
        Else
            txtDifferenza.Text = CDbl(txtLiquidato.Text) - CDbl(txtRichiestaLiquidazioneDanni.Text)
            'txtCompensoSBC.Text = FormatNumber(CDbl(txtDifferenza.Text) / 2 + (CDbl(txtDifferenza.Text) / 2 * Costanti.iva_default / 100), 2, , , TriState.False)
            'txtCompensoSBC.Text = FormatNumber(CDbl(txtDifferenza.Text) / 2, 2, , , TriState.False)
        End If
    End Sub

    Protected Function condizioneWhere() As String
        Dim condizione As String = ""

        If dropCercaAnno.SelectedValue <> "0" Then
            condizione = condizione & " AND anno='" & dropCercaAnno.SelectedValue & "'"
        End If

        If txtCercaProtocollo.Text <> "" Then
            condizione = condizione & " AND numero_protocollo_interno='" & txtCercaProtocollo.Text & "'"
        End If

        If txtCercaNumeroSinistro.Text <> "" Then
            condizione = condizione & " AND numero='" & txtCercaNumeroSinistro.Text & "'"
        End If

        If txtCercaTarga.Text <> "" Then
            condizione = condizione & " AND sinistri.targa='" & txtCercaTarga.Text & "'"
        End If

        If dropCercaTipologia.SelectedValue <> "0" Then
            condizione = condizione & " AND id_tipologia='" & dropCercaTipologia.SelectedValue & "'"
        End If

        If dropCercaStato.SelectedValue <> "0" Then
            If dropCercaStato.SelectedValue <> "4" Then
                condizione = condizione & " AND id_stato='" & dropCercaStato.SelectedValue & "'"
            Else
                condizione = condizione & " AND (id_stato='1' OR id_stato='2')"
            End If
        End If

        If dropCercaProprietario.SelectedValue <> "0" Then
            condizione = condizione & " AND id_proprietario_veicolo='" & dropCercaProprietario.SelectedValue & "'"
        End If

        If dropCercaGestitaDa.SelectedValue <> "0" Then
            condizione = condizione & " AND id_gestito_da='" & dropCercaGestitaDa.SelectedValue & "'"
        End If

        If dropCercaStazione.SelectedValue <> "0" Then
            condizione = condizione & " AND id_stazione='" & dropCercaStazione.SelectedValue & "'"
        End If

        'DATA SINISTRO-----------------------------------------------------------------------------------------------------------
        If txtCercaDaDataSinistro.Text <> "" And txtCercaADataSinistro.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaDaDataSinistro.Text, 0)
            condizione = condizione & " AND data_evento >= CONVERT(DATETIME, '" & data1 & "',102)"
        End If

        If txtCercaDaDataSinistro.Text = "" And txtCercaADataSinistro.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaADataSinistro.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaADataSinistro.Text & " 23:59:59")
            condizione = condizione & " AND data_evento <= CONVERT(DATETIME, '" & data2 & "',102)"
        End If

        If txtCercaDaDataSinistro.Text <> "" And txtCercaADataSinistro.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaDaDataSinistro.Text, 0)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaADataSinistro.Text, 59)
            condizione = condizione & " AND data_evento BETWEEN CONVERT(DATETIME, '" & data1 & "',102) AND CONVERT(DATETIME, '" & data2 & "',102)"
        End If
        '----------------------------------------------------------------------------------------------------------------------------

        'DATA DEFINIZIONE-----------------------------------------------------------------------------------------------------------
        If txtCercaDaDataDefinizione.Text <> "" And txtCercaADataDefinizione.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaDaDataDefinizione.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaDaDataDefinizione.Text)
            condizione = condizione & " AND data_definizione >= CONVERT(DATETIME, '" & data1 & "',102)"
        End If

        If txtCercaDaDataDefinizione.Text = "" And txtCercaADataDefinizione.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaADataDefinizione.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaADataDefinizione.Text & " 23:59:59")
            condizione = condizione & " AND data_definizione <= CONVERT(DATETIME, '" & data2 & "',102)"
        End If

        If txtCercaDaDataDefinizione.Text <> "" And txtCercaADataDefinizione.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaDaDataDefinizione.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaDaDataDefinizione.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaADataDefinizione.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaADataDefinizione.Text & " 23:59:59")
            condizione = condizione & " AND data_definizione BETWEEN CONVERT(DATETIME, '" & data1 & "',102) AND CONVERT(DATETIME, '" & data2 & "',102)"
        End If
        '----------------------------------------------------------------------------------------------------------------------------
        If Trim(txtCercaConducente.Text) <> "" Then
            condizione = condizione & " AND guidatore LIKE '" & Replace(txtCercaConducente.Text, "'", "''") & "%'"
        End If
        If Trim(txtCercaControparte.Text) <> "" Then
            condizione = condizione & " AND controparte LIKE '" & Replace(txtCercaControparte.Text, "'", "''") & "%'"
        End If
        If Trim(txtCercaCompagniaControparte.Text) <> "" Then
            condizione = condizione & " AND compagnia_controparte LIKE '" & Replace(txtCercaCompagniaControparte.Text, "'", "''") & "%'"
        End If

        If txtCercaContratto.Text <> "" Then
            condizione = condizione & " AND num_contratto='" & txtCercaContratto.Text & "'"
        End If

        If txtCercaRds.Text <> "" Then
            condizione = condizione & " AND numero_rds='" & txtCercaRds.Text & "'"
        End If

        If Trim(txtCercaNumSinistroCompagnia.Text) <> "" Then
            condizione = condizione & " AND sinistri.numero_sinistro_compagnia='" & Replace(txtCercaNumSinistroCompagnia.Text, "'", "''") & "'"
        End If

        condizioneWhere = condizione
    End Function

    Protected Sub ricerca(ByVal order_by As String)
        Dim sqlstr As String = "SELECT sinistri.id, anno, numero_protocollo_interno, numero, targa, proprietario_veicolo, CONVERT(Char(10), data_evento, 103) As data_evento, guidatore, controparte, sinistri_stato.descrizione As stato, sinistri_tipologia.descrizione As tipologia FROM sinistri WITH(NOLOCK) INNER JOIN sinistri_stato WITH(NOLOCK) ON sinistri.id_stato=sinistri_stato.id INNER JOIN sinistri_tipologia WITH(NOLOCK) ON sinistri.id_tipologia=sinistri_tipologia.id WHERE attivo='1' " & condizioneWhere()
        Try
            query_cerca.Text = sqlstr
            sqlSinistri.SelectCommand = query_cerca.Text & " " & order_by

            lblOrderBY.Text = order_by

            listSinistri.DataBind()

        Catch ex As Exception
            Response.Write("err ricerca : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try

    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerca.Click
        Try
            ricerca(lblOrderBY.Text)
        Catch ex As Exception
            Response.Write("err btnCerca_Click : " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Sub btnAzzera_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAzzera.Click
        dropCercaAnno.SelectedValue = "0"
        txtCercaProtocollo.Text = ""
        txtCercaNumeroSinistro.Text = ""
        txtCercaTarga.Text = ""
        dropCercaProprietario.SelectedValue = "0"
        dropCercaStato.SelectedValue = "0"
        dropCercaTipologia.SelectedValue = "0"
        dropCercaGestitaDa.SelectedValue = "0"
        txtCercaDaDataDefinizione.Text = ""
        txtCercaADataDefinizione.Text = ""
        txtCercaDaDataSinistro.Text = ""
        txtCercaADataSinistro.Text = ""
        dropCercaStazione.SelectedValue = "0"
        txtCercaCompagniaControparte.Text = ""
        txtCercaConducente.Text = ""
        txtCercaControparte.Text = ""
        txtCercaContratto.Text = ""
        txtCercaRds.Text = ""
    End Sub

    Protected Sub richiama_sinistro(ByVal anno As String, ByVal protocollo As String)
        'SINISTRO  RICHIAMATO
        div_dettaglio.Visible = True
        div_ricerca.Visible = False

        'ID SINISTRO ATTIVO E STATO DA RECUPERARE DA ANNO E PROTOCOLLO INTERNO
        id_sinistro.Text = get_idSinistro(anno, protocollo)

        If id_sinistro.Text <> "" Then
            btnSalva.Text = "Modifica"

            fill_dati_sinistro(id_sinistro.Text)
        Else
            'NON DOVREBBE MAI CAPITARE - SIAMO QUI SE IL DOCUMENTO RISULTA SALVATO MA NON C'E' ALCUN RECORD ATTIVO CORRISPONDENTE
            'ALL'ANNO ED AL NUMERO DI PROTOCOLLO INTERNO
            Response.Redirect("default.aspx")
        End If
    End Sub

    Protected Sub listSinistri_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listSinistri.ItemCommand
        If e.CommandName = "vedi" Then
            Dim annoLabel As Label = e.Item.FindControl("annoLabel")
            Dim protocolloLabel As Label = e.Item.FindControl("protocolloLabel")

            richiama_sinistro(annoLabel.Text, protocolloLabel.Text)
            If permesso_accesso.Text = "2" Then
                btnSalva.Visible = False
                btnSalvaNota.Visible = False
                btnVediTipologia.Visible = False
                btnVediGestitaDa.Visible = False
                btnCalcola.Visible = False
            End If
        ElseIf e.CommandName = "order_by_anno" Then
            If lblOrderBY.Text = " ORDER BY sinistri.anno DESC" Then
                ricerca(" ORDER BY sinistri.anno ASC")
            ElseIf lblOrderBY.Text = " ORDER BY sinistri.anno ASC" Then
                ricerca(" ORDER BY sinistri.anno DESC")
            Else
                ricerca(" ORDER BY sinistri.anno ASC")
            End If
        ElseIf e.CommandName = "order_by_num_int" Then
            If lblOrderBY.Text = " ORDER BY sinistri.numero_protocollo_interno DESC" Then
                ricerca(" ORDER BY sinistri.numero_protocollo_interno ASC")
            ElseIf lblOrderBY.Text = " ORDER BY sinistri.numero_protocollo_interno ASC" Then
                ricerca(" ORDER BY sinistri.numero_protocollo_interno DESC")
            Else
                ricerca(" ORDER BY sinistri.numero_protocollo_interno ASC")
            End If
        ElseIf e.CommandName = "order_by_numero" Then
            If lblOrderBY.Text = " ORDER BY sinistri.numero DESC" Then
                ricerca(" ORDER BY sinistri.numero ASC")
            ElseIf lblOrderBY.Text = " ORDER BY sinistri.numero ASC" Then
                ricerca(" ORDER BY sinistri.numero DESC")
            Else
                ricerca(" ORDER BY sinistri.numero ASC")
            End If
        ElseIf e.CommandName = "order_by_stato" Then
            If lblOrderBY.Text = " ORDER BY stato DESC" Then
                ricerca(" ORDER BY stato ASC")
            ElseIf lblOrderBY.Text = " ORDER BY stato ASC" Then
                ricerca(" ORDER BY stato DESC")
            Else
                ricerca(" ORDER BY stato ASC")
            End If
        ElseIf e.CommandName = "order_by_tipologia" Then
            If lblOrderBY.Text = " ORDER BY tipologia DESC" Then
                ricerca(" ORDER BY stato ASC")
            ElseIf lblOrderBY.Text = " ORDER BY tipologia ASC" Then
                ricerca(" ORDER BY tipologia DESC")
            Else
                ricerca(" ORDER BY tipologia ASC")
            End If
        ElseIf e.CommandName = "order_by_targa" Then
            If lblOrderBY.Text = " ORDER BY sinistri.targa DESC" Then
                ricerca(" ORDER BY stato ASC")
            ElseIf lblOrderBY.Text = " ORDER BY sinistri.targa ASC" Then
                ricerca(" ORDER BY sinistri.targa DESC")
            Else
                ricerca(" ORDER BY sinistri.targa ASC")
            End If
        ElseIf e.CommandName = "order_by_proprietario" Then
            If lblOrderBY.Text = " ORDER BY sinistri.proprietario_veicolo DESC" Then
                ricerca(" ORDER BY stato ASC")
            ElseIf lblOrderBY.Text = " ORDER BY sinistri.proprietario_veicolo ASC" Then
                ricerca(" ORDER BY sinistri.proprietario_veicolo DESC")
            Else
                ricerca(" ORDER BY sinistri.proprietario_veicolo ASC")
            End If
        ElseIf e.CommandName = "order_by_data_evento" Then
            If lblOrderBY.Text = " ORDER BY sinistri.data_evento DESC" Then
                ricerca(" ORDER BY stato ASC")
            ElseIf lblOrderBY.Text = " ORDER BY sinistri.data_evento ASC" Then
                ricerca(" ORDER BY sinistri.data_evento DESC")
            Else
                ricerca(" ORDER BY sinistri.data_evento ASC")
            End If
        ElseIf e.CommandName = "order_by_guidatore" Then
            If lblOrderBY.Text = " ORDER BY sinistri.guidatore DESC" Then
                ricerca(" ORDER BY stato ASC")
            ElseIf lblOrderBY.Text = " ORDER BY sinistri.guidatore ASC" Then
                ricerca(" ORDER BY sinistri.guidatore DESC")
            Else
                ricerca(" ORDER BY sinistri.guidatore ASC")
            End If
        ElseIf e.CommandName = "order_by_controparte" Then
            If lblOrderBY.Text = " ORDER BY sinistri.controparte DESC" Then
                ricerca(" ORDER BY stato ASC")
            ElseIf lblOrderBY.Text = " ORDER BY sinistri.controparte ASC" Then
                ricerca(" ORDER BY sinistri.controparte DESC")
            Else
                ricerca(" ORDER BY sinistri.controparte ASC")
            End If
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click
        div_dettaglio.Visible = False
        div_ricerca.Visible = True
        Try
            ricerca(lblOrderBY.Text)

        Catch ex As Exception
            Response.Write("err btnAnnulla_Click : " & ex.Message & "<br/>" & "<br/>")
        End Try


    End Sub

    Protected Sub btnStampaDatiLegale_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaDatiLegale.Click
        Try
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                Session("url_print") = "/stampe/sinistri/dati_legale.aspx?orientamento=verticale&stampa_legale_anno=" & txtAnno.Text & "&stampa_legale_proto=" & txtProtocollo.Text
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                End If
            End If
        Catch ex As Exception
            Response.Write("err btnStampaDatiLegale_Click : " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Sub btnStampaSinistro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaSinistro.Click
        Try
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                Session("url_print") = "/stampe/sinistri/stampa_sinistro.aspx?orientamento=verticale&stampa_legale_anno=" & txtAnno.Text & "&stampa_legale_proto=" & txtProtocollo.Text
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                End If
            End If
        Catch ex As Exception
            Response.Write("err btnStampaSinistro_Click : " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Sub salva_nota()
        Dim sqlstr As String = "INSERT INTO sinistri_note (descrizione, id_operatore, data, anno, protocollo) VALUES ('" & Replace(txtNote.Text, "'", "''") & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "',CONVERT(DATETIME,GetDate(),102),'" & txtAnno.Text & "','" & txtProtocollo.Text & "')"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            Cmd.ExecuteNonQuery()

            txtNote.Text = ""
            listNote.DataBind()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("err salva_nota : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try

    End Sub

    Protected Sub btnSalvaNota_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvaNota.Click
        If Trim(txtNote.Text) <> "" Then

            salva_nota()

            Libreria.genUserMsgBox(Me, "Nota salvata correttamente.")
        Else
            Libreria.genUserMsgBox(Me, "Specificare un testo da salvare.")
        End If
    End Sub

    Protected Sub btnReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReport.Click
        If dropReport.SelectedValue = "0" Then
            'LISTA SINISTTRI ATTVI - VIENE FISSATO IL PROPRIETARIO
            If dropCercaProprietario.SelectedValue <> "0" Then
                If dropCercaStato.SelectedValue <> "0" And dropCercaStato.SelectedValue <> "3" Then
                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        Session("url_print") = "/stampe/sinistri/lista_sinistri_attivi.aspx?orientamento=orizzontale&query=" & Server.UrlEncode(condizioneWhere()) & "&order_by=" & Server.UrlEncode(lblOrderBY.Text) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&header_html=/stampe/sinistri/header_lista_sinistri_attivi.aspx"
                        Trace.Write(Session("url_print"))
                        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                        End If
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Selezionare la tipologia tra quelle attive.")
                End If
            Else
                Libreria.genUserMsgBox(Me, "Specificare il proprietario.")
            End If
        ElseIf dropReport.SelectedValue = "1" Then
            'LISTA SINISTRI PASSIVI - VIENE FISSATO IL PROPRIETARIO
            If dropCercaProprietario.SelectedValue <> "0" Then
                If dropCercaStato.SelectedValue = "3" Then
                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        Session("url_print") = "/stampe/sinistri/lista_sinistri_attivi.aspx?orientamento=orizzontale&query=" & Server.UrlEncode(condizioneWhere()) & "&order_by=" & Server.UrlEncode(lblOrderBY.Text) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&header_html=/stampe/sinistri/header_lista_sinistri_passivi.aspx"
                        Trace.Write(Session("url_print"))
                        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                        End If
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Impostare la tipologia 'PASSIVA'.")
                End If
            Else
                Libreria.genUserMsgBox(Me, "Specificare il proprietario.")
            End If
        End If
    End Sub

    Protected Sub btnVediTipologia_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVediTipologia.Click
        div_pagina.Visible = False
        div_tipologia.Visible = True
    End Sub

    Protected Sub btnVediGestitaDa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVediGestitaDa.Click
        div_pagina.Visible = False
        div_gestitoDa.Visible = True
    End Sub
End Class
