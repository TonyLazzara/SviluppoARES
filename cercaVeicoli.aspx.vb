Imports funzioni_comuni

Partial Class cercaVeicoli
    Inherits System.Web.UI.Page

    Dim funzioni As New funzioni_comuni

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ScriptManager.GetCurrent(Me.Page).RegisterPostBackControl(btnEsportaFatture)

        If Not Page.IsPostBack Then
            lblOrderBY.Text = " ORDER BY veicoli.targa ASC"

            If Request.Cookies("SicilyRentCar") Is Nothing Then
                Response.Redirect("LogIn.aspx")
            End If

            'SE LA FUNZIONALITA' DATI GENERALI E' INACCESSIBILE O SOLA LETTURA L'UTENTE NON PUO' CREARE UN NUOVO MEZZO
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 2) <> "3" Then
                btnNuovo.Enabled = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 20) <> "3" Then
                btnImportVeicoli.Enabled = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 84) <> "3" Then
                btnImportDismissioni.Enabled = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 38) <> "3" Then
                btnImportAssicurazioniVeicoli.Enabled = False
            End If

            txtQuery.Text = "SELECT veicoli.id, veicoli.telaio, veicoli.targa, marche.descrizione As marca, modelli.descrizione As modello, colori.descrizione As colore, proprietari_veicoli.descrizione As proprietario FROM veicoli LEFT JOIN gruppi WITH(NOLOCK) ON modelli.id_gruppo=gruppi.id_gruppo LEFT JOIN modelli WITH(NOLOCK) ON veicoli.id_modello = modelli.id_modello LEFT JOIN marche WITH(NOLOCK) on modelli.id_CasaAutomobilistica = marche.id LEFT JOIN proprietari_veicoli WITH(NOLOCK) ON veicoli.id_proprietario = proprietari_veicoli.id LEFT JOIN colori WITH(NOLOCK) ON veicoli.id_colore=colori.id LEFT JOIN gruppi WITH(NOLOCK) ON modelli.id_gruppo=gruppi.id_gruppo WHERE veicoli.id>0"

            'SETTAGGIO DEI FILTRI DI RICERCA NEL CASO IN CUI SI RITORNA DALLA MASCHERA DI UN VEICOLO------------------------------------
            If Session("provenienza") = "parco_veicoli" Then
                dropCercaMarca.DataBind()
                dropStatoVendita.DataBind()

                If Session("veicoli_targa") <> "" Then
                    txtCercaTarga.Text = Session("veicoli_targa")
                    Session("veicoli_targa") = ""
                End If
                If Session("veicoli_telaio") <> "" Then
                    txtCercaTelaio.Text = Session("veicoli_telaio")
                    Session("veicoli_telaio") = ""
                End If
                If Session("veicoli_marca") <> "" Then
                    dropCercaMarca.SelectedValue = Session("veicoli_marca")
                    Session("veicoli_marca") = ""
                    dropCercaModello.DataBind()

                    If Session("veicoli_modello") <> "" Then
                        dropCercaModello.SelectedValue = Session("veicoli_modello")
                        Session("veicoli_modello") = ""
                    End If
                End If
                If Session("veicoli_stato_vendita") <> "" Then
                    dropStatoVendita.SelectedValue = Session("veicoli_stato_vendita")
                    Session("veicoli_stato_vendita") = ""
                End If
                If Session("veicoli_bolla_da") <> "" Then
                    txtBollaDa.Text = Session("veicoli_bolla_da")
                    Session("veicoli_bolla_da") = ""
                End If
                If Session("veicoli_bolla_a") <> "" Then
                    txtBollaA.Text = Session("veicoli_bolla_a")
                    Session("veicoli_bolla_a") = ""
                End If
                If Session("veicoli_atto_da") <> "" Then
                    txtAttoDa.Text = Session("veicoli_atto_da")
                    Session("veicoli_atto_da") = ""
                End If
                If Session("veicoli_atto_a") <> "" Then
                    txtAttoA.Text = Session("veicoli_atto_a")
                    Session("veicoli_atto_a") = ""
                End If
                If Session("veicoli_fatt_da") <> "" Then
                    txtFattDa.Text = Session("veicoli_fatt_da")
                    Session("veicoli_fatt_da") = ""
                End If
                If Session("veicoli_fatt_a") <> "" Then
                    txtFattA.Text = Session("veicoli_fatt_a")
                    Session("veicoli_fatt_a") = ""
                End If
                If Session("veicoli_imm_da") <> "" Then
                    txtImmDa.Text = Session("veicoli_imm_da")
                    Session("veicoli_imm_da") = ""
                End If
                If Session("veicoli_imm_a") <> "" Then
                    txtImmA.Text = Session("veicoli_imm_a")
                    Session("veicoli_imm_a") = ""
                End If
                If Session("veicoli_acquisto_da") <> "" Then
                    txtAcquistoDa.Text = Session("veicoli_acquisto_da")
                    Session("veicoli_acquisto_da") = ""
                End If
                If Session("veicoli_acquisto_a") <> "" Then
                    txtAcquistoA.Text = Session("veicoli_acquisto_a")
                    Session("veicoli_acquisto_a") = ""
                End If
                If Session("veicoli_imm_flotta_da") <> "" Then
                    txtImmFlottaDa.Text = Session("veicoli_imm_flotta_da")
                    Session("veicoli_imm_flotta_da") = ""
                End If
                If Session("veicoli_imm_flotta_a") <> "" Then
                    txtImmFlottaA.Text = Session("veicoli_imm_flotta_a")
                    Session("veicoli_imm_flotta_a") = ""
                End If

                If Session("parco_accesso_negato") = "SI" Then
                    Libreria.genUserMsgBox(Page, "Accesso negato.")
                End If

                cerca(lblOrderBY.Text)

                Session("provenienza") = ""
                Session("veicoli_targa") = ""
                Session("veicoli_telaio") = ""
                Session("veicoli_marca") = ""
                Session("veicoli_modello") = ""
                Session("veicoli_stato_vendita") = ""
                Session("veicoli_bolla_da") = ""
                Session("veicoli_bolla_a") = ""
                Session("veicoli_atto_da") = ""
                Session("veicoli_atto_a") = ""
                Session("veicoli_fatt_da") = ""
                Session("veicoli_fatt_a") = ""
                Session("veicoli_imm_da") = ""
                Session("veicoli_imm_a") = ""
                Session("veicoli_acquisto_da") = ""
                Session("veicoli_acquisto_a") = ""
                Session("veicoli_imm_flotta_da") = ""
                Session("veicoli_imm_flotta_a") = ""
                Session("parco_accesso_negato") = ""
            Else
                'SE NON PROVENGO DA PARCO VEICOLI AZZERO LA SESSION QUALORA L'UTENTE SIA USCITO DA QUELLA SCHERMATA IN MANIERA ANOMALA
                Session("provenienza") = ""
                Session("veicoli_targa") = ""
                Session("veicoli_telaio") = ""
                Session("veicoli_marca") = ""
                Session("veicoli_modello") = ""
                Session("veicoli_stato_vendita") = ""
                Session("veicoli_bolla_da") = ""
                Session("veicoli_bolla_a") = ""
                Session("veicoli_atto_da") = ""
                Session("veicoli_atto_a") = ""
                Session("veicoli_fatt_da") = ""
                Session("veicoli_fatt_a") = ""
                Session("veicoli_imm_da") = ""
                Session("veicoli_imm_a") = ""
                Session("veicoli_acquisto_da") = ""
                Session("veicoli_acquisto_a") = ""
                Session("veicoli_imm_flotta_da") = ""
                Session("veicoli_imm_flotta_a") = ""
                Session("parco_accesso_negato") = ""
            End If
            '---------------------------------------------------------------------------------------------------------------------------
        Else
            sqlVeicoli.SelectCommand = txtQuery.Text & lblOrderBY.Text
            sqlVeicoli.DataBind()
        End If
    End Sub

    Protected Sub setSession()
        If Trim(txtCercaTarga.Text) <> "" Then
            Session("veicoli_targa") = txtCercaTarga.Text
        End If
        If Trim(txtCercaTelaio.Text) <> "" Then
            Session("veicoli_telaio") = txtCercaTelaio.Text
        End If
        If dropCercaMarca.SelectedValue <> "" Then
            Session("veicoli_marca") = dropCercaMarca.SelectedValue
        End If
        If dropCercaModello.SelectedValue <> "" Then
            Session("veicoli_modello") = dropCercaModello.SelectedValue
        End If
        If dropStatoVendita.SelectedValue <> "" Then
            Session("veicoli_stato_vendita") = dropStatoVendita.SelectedValue
        End If
        If Trim(txtBollaDa.Text) <> "" Then
            Session("veicoli_bolla_da") = txtBollaDa.Text
        End If
        If Trim(txtBollaA.Text) <> "" Then
            Session("veicoli_bolla_a") = txtBollaA.Text
        End If
        If Trim(txtAttoDa.Text) <> "" Then
            Session("veicoli_atto_da") = txtAttoDa.Text
        End If
        If Trim(txtAttoA.Text) <> "" Then
            Session("veicoli_atto_a") = txtAttoA.Text
        End If
        If Trim(txtFattDa.Text) <> "" Then
            Session("veicoli_fatt_da") = txtFattDa.Text
        End If
        If Trim(txtFattA.Text) <> "" Then
            Session("veicoli_fatt_a") = txtFattA.Text
        End If
        If Trim(txtImmDa.Text) <> "" Then
            Session("veicoli_imm_da") = txtImmDa.Text
        End If
        If Trim(txtImmA.Text) <> "" Then
            Session("veicoli_imm_a") = txtImmA.Text
        End If
        If Trim(txtAcquistoDa.Text) <> "" Then
            Session("veicoli_acquisto_da") = txtAcquistoDa.Text
        End If
        If Trim(txtAcquistoA.Text) <> "" Then
            Session("veicoli_acquisto_a") = txtAcquistoA.Text
        End If
        If Trim(txtImmFlottaDa.Text) <> "" Then
            Session("veicoli_imm_flotta_da") = txtImmFlottaDa.Text
        End If
        If Trim(txtImmFlottaA.Text) <> "" Then
            Session("veicoli_imm_flotta_a") = txtImmFlottaA.Text
        End If
    End Sub

    Protected Sub btnNuovo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovo.Click
        setSession()

        Response.Redirect("parcoVeicoli.aspx")
    End Sub

    Protected Sub btnImportVeicoli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportVeicoli.Click
        Response.Redirect("ImportVeicoli.aspx")
    End Sub

    Protected Function condizioneWhere() As String

        Dim condizione As String = ""

        If dropStatoVendita.SelectedValue <> "0" Then
            If dropStatoVendita.SelectedValue = "1" Then
                condizione += " AND ISNULL(veicoli.venduta,'0')='0' AND ISNULL(veicoli.in_vendita,'0')='0' AND ISNULL(veicoli.furto,'0')='0' AND ISNULL(veicoli.buy_back,'0')='0' "
            ElseIf dropStatoVendita.SelectedValue = "2" Then
                condizione += " AND ISNULL(veicoli.disponibile_nolo,'0')='1' "
            ElseIf dropStatoVendita.SelectedValue = "3" Then
                condizione += " AND ISNULL(veicoli.in_vendita,'0')='1' "
            ElseIf dropStatoVendita.SelectedValue = "4" Then
                condizione += " AND ISNULL(veicoli.venduta,'0')='1' "
            ElseIf dropStatoVendita.SelectedValue = "5" Then
                condizione += " AND ISNULL(veicoli.furto,'0')='1' "
            ElseIf dropStatoVendita.SelectedValue = "6" Then
                condizione += " AND ISNULL(veicoli.buy_back,'0')='1' "
            ElseIf dropStatoVendita.SelectedValue = "7" Then
                condizione += " AND ISNULL(veicoli.noleggiata,'0')='1' "
            End If
        End If

        'Response.Write(dropCercaStazione.SelectedValue)

        If dropCercaStazione.SelectedValue = "0" Then
            condizione += " AND veicoli.id_stazione IS NULL"
        ElseIf dropCercaStazione.SelectedValue = "1" Then
            condizione += " AND veicoli.id_stazione IS NOT NULL"
        ElseIf dropCercaStazione.SelectedValue <> "-1" Then
            condizione += " AND veicoli.id_stazione='" & dropCercaStazione.SelectedValue & "' "
        End If

        If Trim(txtCercaTarga.Text) <> "" Then
            condizione += " AND veicoli.targa LIKE '" & Trim(txtCercaTarga.Text) & "%'"
        End If

        If Trim(txtCercaTelaio.Text) <> "" Then
            condizione += " AND veicoli.telaio LIKE '" & Trim(txtCercaTelaio.Text) & "%'"
        End If

        If dropCercaMarca.SelectedValue > 0 Then
            condizione += " AND modelli.id_CasaAutomobilistica = '" & dropCercaMarca.SelectedValue & "'"
        End If

        If dropCercaModello.SelectedValue > 0 Then
            condizione += " AND veicoli.id_modello = '" & dropCercaModello.SelectedValue & "'"
        End If

        If dropCercaGruppo.SelectedValue <> "0" Then
            condizione += " AND gruppi.id_gruppo='" & dropCercaGruppo.SelectedValue & "'"
        End If

        'IMMATRICOLAZIONE---------------------------------------------------------------------------------------------------------
        If txtImmDa.Text <> "" And txtImmA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtImmDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtImmDa.Text)
            condizione += " AND veicoli.data_immatricolazione >= Convert(DateTime, '" & data1 & "', 102)"
            'condizione += " AND veicoli.data_immatricolazione >= '" & data1 & "'"
        End If

        If txtImmDa.Text = "" And txtImmA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtImmA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtImmA.Text & " 23:59:59")
            condizione += " AND veicoli.data_immatricolazione <= Convert(DateTime, '" & data2 & "', 102)"
            'condizione += " AND veicoli.data_immatricolazione <= '" & data2 & "'"
        End If

        If txtImmDa.Text <> "" And txtImmA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtImmDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtImmDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtImmA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtImmA.Text & " 23:59:59")
            condizione += " AND veicoli.data_immatricolazione BETWEEN Convert(DateTime, '" & data1 & "', 102) AND CONVERT(DATETIME, '" & data2 & "', 102)"
            'condizione += " AND veicoli.data_immatricolazione BETWEEN '" & data1 & "' AND '" & data2 & "'"
        End If
        '--------------------------------------------------------------------------------------------------------------------------------
        'BOLLA DI VENDITA----------------------------------------------------------------------------------------------------------------
        If txtBollaDa.Text <> "" And txtBollaA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtBollaDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtBollaDa.Text)
            condizione += " AND veicoli.data_bolla >= Convert(DateTime, '" & data1 & "', 102)"
            'condizione += " AND veicoli.data_bolla >= '" & data1 & "'"
        End If

        If txtBollaDa.Text = "" And txtBollaA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtBollaA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtBollaA.Text & " 23:59:59")
            condizione += " AND veicoli.data_bolla <= Convert(DateTime, '" & data2 & "', 102)"
            'condizione += " AND veicoli.data_bolla <= '" & data2 & "'"
        End If

        If txtBollaDa.Text <> "" And txtBollaA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtBollaDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtBollaDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtBollaA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtBollaA.Text & " 23:59:59")
            condizione += " AND veicoli.data_bolla BETWEEN Convert(DateTime, '" & data1 & "', 102) AND CONVERT(DATETIME, '" & data2 & "', 102)"
            'condizione += " AND veicoli.data_bolla BETWEEN '" & data1 & "' AND '" & data2 & "'"
        End If

        If dropCercaBolle.SelectedValue = "1" Then
            condizione += " AND NOT veicoli.data_bolla IS NULL"
        ElseIf dropCercaBolle.SelectedValue = "2" Then
            condizione += " AND veicoli.data_bolla IS NULL"
        End If
        '--------------------------------------------------------------------------------------------------------------------------------
        'ATTO DI VENDITA-----------------------------------------------------------------------------------------------------------------
        If txtAttoDa.Text <> "" And txtAttoA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtAttoDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtAttoDa.Text)
            condizione += " AND veicoli.data_atto_vendita >= Convert(DateTime, '" & data1 & "', 102)"
            'condizione += " AND veicoli.data_atto_vendita >= '" & data1 & "'"
        End If

        If txtAttoDa.Text = "" And txtAttoA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtAttoA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtAttoA.Text & " 23:59:59")
            condizione += " AND veicoli.data_atto_vendita <= Convert(DateTime, '" & data2 & "', 102)"
            'condizione += " AND veicoli.data_atto_vendita <= '" & data2 & "'"
        End If

        If txtAttoDa.Text <> "" And txtAttoA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtAttoDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtAttoDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtAttoA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtAttoA.Text & " 23:59:59")
            condizione += " AND veicoli.data_atto_vendita BETWEEN Convert(DateTime, '" & data1 & "', 102) AND CONVERT(DATETIME, '" & data2 & "', 102)"
            'condizione += " AND veicoli.data_atto_vendita BETWEEN '" & data1 & "' AND '" & data2 & "'"
        End If

        If dropCercaAttoVendita.SelectedValue = "1" Then
            condizione += " AND NOT veicoli.data_atto_vendita IS NULL"
        ElseIf dropCercaAttoVendita.SelectedValue = "2" Then
            condizione += " AND veicoli.data_atto_vendita IS NULL"
        End If
        '--------------------------------------------------------------------------------------------------------------------------------
        'FATTURA DI VENDITA--------------------------------------------------------------------------------------------------------------
        If txtFattDa.Text <> "" And txtFattA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtFattDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtFattDa.Text)
            condizione += " AND veicoli.id IN (SELECT id_veicolo FROM fatture_vendita_veicoli WITH(NOLOCK) WHERE tipo='FATTURA' AND data_vendita >= Convert(DateTime, '" & data1 & "', 102) AND id_veicolo=veicoli.id)"
            'condizione += " AND veicoli.id IN (SELECT id_veicolo FROM fatture_vendita_veicoli WITH(NOLOCK) WHERE tipo='FATTURA' AND data_vendita >= '" & data1 & "' AND id_veicolo=veicoli.id)"
        End If

        If txtFattDa.Text = "" And txtFattA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtFattA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtFattA.Text & " 23:59:59")
            condizione += " AND veicoli.id IN (SELECT id_veicolo FROM fatture_vendita_veicoli WITH(NOLOCK) WHERE tipo='FATTURA' AND data_vendita <= Convert(DateTime, '" & data2 & "', 102) AND id_veicolo=veicoli.id)"
        End If

        If txtFattDa.Text <> "" And txtFattA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtFattDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtFattDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtFattA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtFattA.Text & " 23:59:59")
            condizione += " AND veicoli.id IN (SELECT id_veicolo FROM fatture_vendita_veicoli WITH(NOLOCK) WHERE tipo='FATTURA' AND data_vendita BETWEEN Convert(DateTime, '" & data1 & "', 102) AND CONVERT(DATETIME, '" & data2 & "', 102) AND id_veicolo=veicoli.id)"
        End If
        '--------------------------------------------------------------------------------------------------------------------------------
        'FATTURA DI ACQUISTO-------------------------------------------------------------------------------------------------------------
        If txtFatturaAcquistoDa.Text <> "" And txtFatturaAcquistoA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtFatturaAcquistoDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtFatturaAcquistoDa.Text)
            condizione += " AND veicoli.id IN (SELECT id_veicolo FROM fatture_acquisto_veicoli WITH(NOLOCK) WHERE tipo='FATTURA' AND data_acquisto >= Convert(DateTime, '" & data1 & "', 102)  AND id_veicolo=veicoli.id)"
        End If

        If txtFatturaAcquistoDa.Text = "" And txtFatturaAcquistoA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtFatturaAcquistoA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtFatturaAcquistoA.Text & " 23:59:59")
            condizione += " AND veicoli.id IN (SELECT id_veicolo FROM fatture_acquisto_veicoli WITH(NOLOCK) WHERE tipo='FATTURA' AND data_acquisto <= Convert(DateTime, '" & data2 & "', 102) AND id_veicolo=veicoli.id)"
        End If

        If txtFatturaAcquistoDa.Text <> "" And txtFatturaAcquistoA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtFatturaAcquistoDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtFatturaAcquistoDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtFatturaAcquistoA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtFatturaAcquistoA.Text & " 23:59:59")
            condizione += " AND veicoli.id IN (SELECT id_veicolo FROM fatture_acquisto_veicoli WITH(NOLOCK) WHERE tipo='FATTURA' AND data_acquisto BETWEEN Convert(DateTime, '" & data1 & "', 102) AND CONVERT(DATETIME, '" & data2 & "', 102) AND id_veicolo=veicoli.id)"
        End If
        '--------------------------------------------------------------------------------------------------------------------------------
        'DATA DI ACQUISTO----------------------------------------------------------------------------------------------------------------
        If txtAcquistoDa.Text <> "" And txtAcquistoA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtAcquistoDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtAcquistoDa.Text)
            condizione += " AND veicoli.data_acquisto >= Convert(DateTime, '" & data1 & "', 102)"
        End If

        If txtAcquistoDa.Text = "" And txtAcquistoA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtAcquistoA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtAcquistoA.Text & " 23:59:59")
            condizione += " AND veicoli.data_acquisto <= Convert(DateTime, '" & data2 & "', 102)"
        End If

        If txtAcquistoDa.Text <> "" And txtAcquistoA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtAcquistoDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtAcquistoDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtAcquistoA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtAcquistoA.Text & " 23:59:59")
            condizione += " AND veicoli.data_acquisto BETWEEN Convert(DateTime, '" & data1 & "', 102) AND CONVERT(DATETIME, '" & data2 & "', 102)"
        End If
        '--------------------------------------------------------------------------------------------------------------------------------
        'DATA DI IMMISSIONE IN PARCO-----------------------------------------------------------------------------------------------------
        If txtImmFlottaDa.Text <> "" And txtImmFlottaA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtImmFlottaDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtImmFlottaDa.Text)
            condizione += " AND veicoli.id IN (SELECT movimenti_targa.id_veicolo FROM movimenti_targa WITH(NOLOCK) WHERE movimenti_targa.id_veicolo=veicoli.id AND movimenti_targa.id_tipo_movimento='" & Costanti.IdImmissioneInParco & "' AND movimenti_targa.data_rientro>=Convert(DateTime, '" & data1 & "', 102))"
        End If
        If txtImmFlottaDa.Text = "" And txtImmFlottaA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtImmFlottaA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtImmFlottaA.Text & " 23:59:59")
            condizione += " AND veicoli.id IN (SELECT movimenti_targa.id_veicolo FROM movimenti_targa WITH(NOLOCK) WHERE movimenti_targa.id_veicolo=veicoli.id AND movimenti_targa.id_tipo_movimento='" & Costanti.IdImmissioneInParco & "' AND movimenti_targa.data_rientro<=Convert(DateTime, '" & data2 & "', 102))"
        End If
        If txtImmFlottaDa.Text <> "" And txtImmFlottaA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtImmFlottaDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtImmFlottaDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtImmFlottaA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtImmFlottaA.Text & " 23:59:59")

            condizione += " AND veicoli.id IN (SELECT movimenti_targa.id_veicolo FROM movimenti_targa WITH(NOLOCK) WHERE movimenti_targa.id_veicolo=veicoli.id AND movimenti_targa.id_tipo_movimento='" & Costanti.IdImmissioneInParco & "' AND movimenti_targa.data_rientro BETWEEN Convert(DateTime, '" & data1 & "', 102) AND CONVERT(DATETIME, '" & data2 & "', 102))"
        End If
        '--------------------------------------------------------------------------------------------------------------------------------
        'ACQUIRENTE E VENDITORE -----------------------------------------------------------------------------------------------------------
        If dropAcquirenti.SelectedValue <> "0" Then
            condizione += " AND veicoli.id_acquirente='" & dropAcquirenti.SelectedValue & "'"
        End If
        If dropVenditori.SelectedValue <> "0" Then
            condizione += " AND veicoli.id_venditore='" & dropVenditori.SelectedValue & "'"
        End If
        '----------------------------------------------------------------------------------------------------------------------------------
        'VEICOLI ESCLUSI/INCLUSI DA AMMORTAMENTO ------------------------------------------------------------------------------------------
        If dropAmmortamento.SelectedValue <> "-1" Then
            condizione += " AND veicoli.escludi_ammortamento='" & dropAmmortamento.SelectedValue & "'"
        End If
        '----------------------------------------------------------------------------------------------------------------------------------
        'AUTO IN LEASING O DI PROPRIETA'---------------------------------------------------------------------------------------------------
        If dropLeasing.SelectedValue <> "-1" Then
            condizione += " AND veicoli.auto_in_leasing='" & dropLeasing.SelectedValue & "'"
        End If
        '----------------------------------------------------------------------------------------------------------------------------------
        'SCADENZA LEASING------------------------------------------------------------------------------------------------------------------
        If txtScadenzaLeasingDa.Text <> "" And txtScadenzaLeasingA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtScadenzaLeasingDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtScadenzaLeasingDa.Text)
            condizione += " AND veicoli.data_fine_leasing >= Convert(DateTime, '" & data1 & "', 102)"
        End If

        If txtScadenzaLeasingDa.Text = "" And txtScadenzaLeasingA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtImmFlottaA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtScadenzaLeasingA.Text & " 23:59:59")
            condizione += " AND veicoli.data_fine_leasing <= Convert(DateTime, '" & data2 & "', 102)"
        End If

        If txtScadenzaLeasingDa.Text <> "" And txtScadenzaLeasingA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtScadenzaLeasingDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtScadenzaLeasingDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtScadenzaLeasingA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtScadenzaLeasingA.Text & " 23:59:59")
            condizione += " AND veicoli.data_fine_leasing BETWEEN Convert(DateTime, '" & data1 & "', 102) AND CONVERT(DATETIME, '" & data2 & "', 102)"
        End If
        '----------------------------------------------------------------------------------------------------------------------------------
        'PROPRIETARI----------------------
        If dropCercaProprietario.SelectedValue <> "0" Then
            condizione += " AND veicoli.id_proprietario='" & dropCercaProprietario.SelectedValue & "'"
        End If
        '--------------------------------
        'Response.Write("<br><br>Condizione " & condizione)       
        condizioneWhere = condizione

        'Tony 24-09-2022
        'If dropStatoVendita.SelectedValue = "2" Then

        '    If dropCercaStazione.SelectedValue = "0" Then
        '        condizione += " OR (veicoli.da_rifornire='1' AND veicoli.id_stazione IS NULL)"
        '    ElseIf dropCercaStazione.SelectedValue = "1" Then
        '        condizione += " OR (veicoli.da_rifornire='1' AND veicoli.id_stazione IS NOT NULL)"
        '    ElseIf dropCercaStazione.SelectedValue <> "-1" Then
        '        condizione += " OR (veicoli.da_rifornire='1' AND veicoli.id_stazione='" & dropCercaStazione.SelectedValue & "') "
        '    End If

        'End If
        'FINE Tony


    End Function

    Protected Function getNumeroRisultati() As String

        Dim sqlStr As String = "SELECT ISNULL(count(veicoli.id),0) FROM veicoli WITH(NOLOCK) LEFT JOIN modelli WITH(NOLOCK) ON veicoli.id_modello = modelli.id_modello LEFT JOIN marche WITH(NOLOCK) on modelli.id_CasaAutomobilistica = marche.id LEFT JOIN proprietari_veicoli WITH(NOLOCK) ON veicoli.id_proprietario = proprietari_veicoli.id LEFT JOIN colori WITH(NOLOCK) ON veicoli.id_colore=colori.id LEFT JOIN gruppi WITH(NOLOCK) ON modelli.id_gruppo=gruppi.id_gruppo "

        'Tony 24-09-2022
        'If dropStatoVendita.SelectedValue = "2" Then
        '    If dropCercaStazione.SelectedValue = "0" Then
        '        sqlStr += "WHERE (veicoli.id>0 " & condizioneWhere() & ") OR (veicoli.da_rifornire='1' AND veicoli.id_stazione IS NULL) "
        '    ElseIf dropCercaStazione.SelectedValue = "1" Then
        '        sqlStr += "WHERE (veicoli.id>0 " & condizioneWhere() & ") OR (veicoli.da_rifornire='1' AND veicoli.id_stazione IS NOT NULL) "
        '    ElseIf dropCercaStazione.SelectedValue <> "-1" Then
        '        sqlStr += "WHERE (veicoli.id>0 " & condizioneWhere() & ") OR (veicoli.da_rifornire='1' AND veicoli.id_stazione='" & dropCercaStazione.SelectedValue & "') "
        '    End If

        'Else
        '    sqlStr += "WHERE veicoli.id>0 " & condizioneWhere()
        'End If
        'FINE Tony

        sqlStr += "WHERE veicoli.id>0 " & condizioneWhere()


        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            'lblNumRisultati.Text = Cmd.ExecuteScalar

            getNumeroRisultati = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("Error_GetNumeroRisultati_:" & ex.Message & "<br/>")
        End Try

    End Function

    Protected Sub setQuery(ByVal order_by As String)

        Try
            'Tony 24-09-2022
            'sqlVeicoli.SelectCommand = "SELECT veicoli.id, veicoli.targa, veicoli.telaio, marche.descrizione As marca, modelli.descrizione As modello, colori.descrizione As colore, proprietari_veicoli.descrizione As proprietario, gruppi.cod_gruppo As gruppo, veicoli.km_attuali, veicoli.serbatoio_attuale, modelli.capacita_serbatoio FROM veicoli WITH(NOLOCK) LEFT JOIN modelli WITH(NOLOCK) ON veicoli.id_modello = modelli.id_modello LEFT JOIN marche WITH(NOLOCK) on modelli.id_CasaAutomobilistica = marche.id LEFT JOIN proprietari_veicoli WITH(NOLOCK) ON veicoli.id_proprietario = proprietari_veicoli.id LEFT JOIN colori WITH(NOLOCK) ON veicoli.id_colore=colori.id LEFT JOIN gruppi WITH(NOLOCK) ON modelli.id_gruppo=gruppi.id_gruppo WHERE "
            sqlVeicoli.SelectCommand = "SELECT veicoli.id, veicoli.targa, veicoli.telaio, marche.descrizione As marca, modelli.descrizione As modello, colori.descrizione As colore, proprietari_veicoli.descrizione As proprietario, gruppi.cod_gruppo As gruppo, veicoli.km_attuali, veicoli.serbatoio_attuale, modelli.capacita_serbatoio, veicoli.disponibile_nolo FROM veicoli WITH(NOLOCK) LEFT JOIN modelli WITH(NOLOCK) ON veicoli.id_modello = modelli.id_modello LEFT JOIN marche WITH(NOLOCK) on modelli.id_CasaAutomobilistica = marche.id LEFT JOIN proprietari_veicoli WITH(NOLOCK) ON veicoli.id_proprietario = proprietari_veicoli.id LEFT JOIN colori WITH(NOLOCK) ON veicoli.id_colore=colori.id LEFT JOIN gruppi WITH(NOLOCK) ON modelli.id_gruppo=gruppi.id_gruppo WHERE "
            'FINE Tony

            Dim whe As String '= "veicoli.id>0 " & condizioneWhere()

            'Tony 24-09-2022
            'If dropStatoVendita.SelectedValue = "2" Then
            '    If dropCercaStazione.SelectedValue = "0" Then
            '        whe += "(veicoli.id>0 " & condizioneWhere() & ") OR (veicoli.da_rifornire='1' AND veicoli.id_stazione IS NULL) "
            '    ElseIf dropCercaStazione.SelectedValue = "1" Then
            '        whe += "(veicoli.id>0 " & condizioneWhere() & ") OR (veicoli.da_rifornire='1' AND veicoli.id_stazione IS NOT NULL) "
            '    ElseIf dropCercaStazione.SelectedValue <> "-1" Then
            '        whe += "(veicoli.id>0 " & condizioneWhere() & ") OR (veicoli.da_rifornire='1' AND veicoli.id_stazione='" & dropCercaStazione.SelectedValue & "') "
            '    End If

            'Else
            '    whe = "veicoli.id>0 " & condizioneWhere()
            'End If
            'FINE Tony

            whe = "veicoli.id>0 " & condizioneWhere()

            txtQuery.Text = sqlVeicoli.SelectCommand & whe

            'Response.Write(txtQuery.Text & order_by)
            'Response.End()
            sqlVeicoli.SelectCommand = txtQuery.Text & order_by
            lblOrderBY.Text = order_by
        Catch ex As Exception
            Response.Write("Error_setquery_:" & ex.Message & "<br/>")
        End Try


    End Sub

    Protected Sub cerca(ByVal order_by As String)
        Try            
            setQuery(order_by)
            lblNumRisultati.Text = getNumeroRisultati()
            listVeicoli.DataBind()
            LblCaricaElenco.Text = "CaricaFile"
        Catch ex As Exception
            Response.Write("Error_cerca_:" & ex.Message & "<br/>")
        End Try

    End Sub

    Protected Sub btnCercaVeicolo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCercaVeicolo.Click
        Try
            cerca(lblOrderBY.Text)
        Catch ex As Exception
            Response.Write("Error_btnCercaVeicolo_Click_:" & ex.Message & "<br/>")
        End Try


    End Sub

    Protected Sub listVeicoli_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listVeicoli.ItemCommand
        If e.CommandName = "vedi" Then
            setSession()
            Dim auto As Label = e.Item.FindControl("idLabel")
            Response.Redirect("parcoVeicoli.aspx?veicolo=" & auto.Text)
        ElseIf e.CommandName = "elimina" Then
            Dim auto As Label = e.Item.FindControl("idLabel")
            eliminaVeicolo(auto.Text)
        ElseIf e.CommandName = "order_by_targa" Then
            If lblOrderBY.Text = " ORDER BY veicoli.targa DESC" Then
                cerca(" ORDER BY veicoli.targa ASC")
            ElseIf lblOrderBY.Text = " ORDER BY veicoli.targa ASC" Then
                cerca(" ORDER BY veicoli.targa DESC")
            Else
                cerca(" ORDER BY veicoli.targa ASC")
            End If
        ElseIf e.CommandName = "order_by_Telaio" Then
            If lblOrderBY.Text = " ORDER BY veicoli.telaio DESC" Then
                cerca(" ORDER BY veicoli.targa ASC")
            ElseIf lblOrderBY.Text = " ORDER BY veicoli.telaio ASC" Then
                cerca(" ORDER BY veicoli.telaio DESC")
            Else
                cerca(" ORDER BY veicoli.telaio ASC")
            End If
        ElseIf e.CommandName = "order_by_modello" Then
            If lblOrderBY.Text = " ORDER BY modelli.descrizione DESC" Then
                cerca(" ORDER BY modelli.descrizione ASC")
            ElseIf lblOrderBY.Text = " ORDER BY modelli.descrizione ASC" Then
                cerca(" ORDER BY modelli.descrizione DESC")
            Else
                cerca(" ORDER BY modelli.descrizione ASC")
            End If
        ElseIf e.CommandName = "order_by_modello" Then
            If lblOrderBY.Text = " ORDER BY modelli.descrizione DESC" Then
                cerca(" ORDER BY modelli.descrizione ASC")
            ElseIf lblOrderBY.Text = " ORDER BY modelli.descrizione ASC" Then
                cerca(" ORDER BY modelli.descrizione DESC")
            Else
                cerca(" ORDER BY modelli.descrizione ASC")
            End If
        ElseIf e.CommandName = "order_by_Colore" Then
            If lblOrderBY.Text = " ORDER BY colori.descrizione DESC" Then
                cerca(" ORDER BY colori.descrizione ASC")
            ElseIf lblOrderBY.Text = " ORDER BY colori.descrizione ASC" Then
                cerca(" ORDER BY colori.descrizione DESC")
            Else
                cerca(" ORDER BY colori.descrizione ASC")
            End If
        ElseIf e.CommandName = "order_by_Colore" Then
            If lblOrderBY.Text = " ORDER BY colori.descrizione DESC" Then
                cerca(" ORDER BY colori.descrizione ASC")
            ElseIf lblOrderBY.Text = " ORDER BY colori.descrizione ASC" Then
                cerca(" ORDER BY colori.descrizione DESC")
            Else
                cerca(" ORDER BY colori.descrizione ASC")
            End If
        ElseIf e.CommandName = "order_by_propr" Then
            If lblOrderBY.Text = " ORDER BY proprietari_veicoli.descrizione DESC" Then
                cerca(" ORDER BY proprietari_veicoli.descrizione ASC")
            ElseIf lblOrderBY.Text = " ORDER BY proprietari_veicoli.descrizione ASC" Then
                cerca(" ORDER BY proprietari_veicoli.descrizione DESC")
            Else
                cerca(" ORDER BY proprietari_veicoli.descrizione ASC")
            End If
        ElseIf e.CommandName = "order_by_gruppo" Then
            If lblOrderBY.Text = " ORDER BY gruppi.cod_gruppo DESC" Then
                cerca(" ORDER BY gruppi.cod_gruppo ASC")
            ElseIf lblOrderBY.Text = " ORDER BY gruppi.cod_gruppo ASC" Then
                cerca(" ORDER BY gruppi.cod_gruppo DESC")
            Else
                cerca(" ORDER BY gruppi.cod_gruppo ASC")
            End If
        ElseIf e.CommandName = "order_by_km" Then
            If lblOrderBY.Text = " ORDER BY veicoli.km_attuali DESC" Then
                cerca(" ORDER BY veicoli.km_attuali ASC")
            ElseIf lblOrderBY.Text = " ORDER BY veicoli.km_attuali ASC" Then
                cerca(" ORDER BY veicoli.km_attuali DESC")
            Else
                cerca(" ORDER BY veicoli.km_attuali ASC")
            End If
        ElseIf e.CommandName = "order_by_litri" Then
            If lblOrderBY.Text = " ORDER BY veicoli.serbatoio_attuale DESC" Then
                cerca(" ORDER BY veicoli.serbatoio_attuale ASC")
            ElseIf lblOrderBY.Text = " ORDER BY veicoli.serbatoio_attuale ASC" Then
                cerca(" ORDER BY veicoli.serbatoio_attuale DESC")
            Else
                cerca(" ORDER BY veicoli.serbatoio_attuale ASC")
            End If
        End If
    End Sub

    Protected Sub eliminaVeicolo(ByVal id_auto As String)

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

            Try
                Cmd = New Data.SqlClient.SqlCommand("DELETE FROM veicoli WHERE id='" & id_auto & "'", Dbc)
                Cmd.ExecuteScalar()

                Cmd = New Data.SqlClient.SqlCommand("DELETE FROM movimenti_targa WHERE id_veicolo='" & id_auto & "'", Dbc)
                Cmd.ExecuteScalar()

                Libreria.genUserMsgBox(Me, "Veicolo eliminato correttamente.")
                cerca(lblOrderBY.Text)
            Catch ex As Exception
                Libreria.genUserMsgBox(Me, "Impossibile eliminare questo veicolo.")
            End Try
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("Error_eliminaVeicolo_:" & ex.Message & "<br/>")
        End Try

    End Sub

    Protected Sub btnReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReport.Click
        If CInt(getNumeroRisultati()) < 10000 Then
            If dropReport.SelectedValue = "0" Then
                'LISTA TARGHE RIDOTTE
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    Session("url_print") = "/stampe/stampa_elenco.aspx?orientamento=orizzontale&query=" & Server.UrlEncode(condizioneWhere()) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&header_html=/stampe/header_stampa_elenco.aspx"
                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                    End If
                End If
            ElseIf dropReport.SelectedValue = "1" Then
                'LISTA TARGHE RIDOTTE CON DETTAGLIO
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    Session("url_print") = "/stampe/lista_ridotta_dettaglio.aspx?orientamento=verticale&query=" & Server.UrlEncode(condizioneWhere()) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&header_html=/stampe/header_lista_ridotta_dettaglio.aspx"
                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                    End If
                End If
            ElseIf dropReport.SelectedValue = "2" Then
                'LISTA TARGHE CON DETTAGLIO FATTURE
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    Session("url_print") = "/stampe/lista_targhe_fatture.aspx?orientamento=verticale&query=" & Server.UrlEncode(condizioneWhere()) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&header_html=stampe/header_lista_targhe_fatture.aspx"
                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                    End If
                End If
            ElseIf dropReport.SelectedValue = "3" Then
                'LISTA TARGHE CON DETTAGLIO FATTURE
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    Session("url_print") = "/stampe/lista_targhe_venditore.aspx?orientamento=verticale&query=" & Server.UrlEncode(condizioneWhere()) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&header_html=/stampe/header_lista_targhe_venditore.aspx"
                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                    End If
                End If
            ElseIf dropReport.SelectedValue = "4" Then
                'LISTA TARGHE AUTO x CESPITI
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    Session("url_print") = "/stampe/stampa_lista_targhe_X_cespiti.aspx?orientamento=verticale&query=" & Server.UrlEncode(condizioneWhere()) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&header_html=/stampe/header_stampa_lista_targhe_X_cespiti.aspx"
                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                    End If
                End If
            ElseIf dropReport.SelectedValue = "5" Then
                'LISTA TARGHE AUTO IN VENDITA O VENDUTE
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    Session("url_print") = "/stampe/auto_in_vendita_o_vendute.aspx?orientamento=verticale&query=" & Server.UrlEncode(condizioneWhere()) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&header_html=/stampe/header_auto_in_vendita_o_vendute.aspx"
                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                    End If
                End If
            ElseIf dropReport.SelectedValue = "6" Then
                'LISTA TARGHE AUTO IN VENDITA O VENDUTE
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    Session("url_print") = "/stampe/auto_in_vendita_o_vendute_x_venditore.aspx?orientamento=verticale&query=" & Server.UrlEncode(condizioneWhere()) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&header_html=/stampe/header_auto_in_vendita_o_vendute_x_venditore.aspx"
                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                    End If
                End If
            End If
        Else
            Libreria.genUserMsgBox(Me, "Ridurre il numero di dati da stampare. (Limite: 10.000)")
        End If

        
    End Sub

    Protected Sub btnImportAssicurazioniVeicoli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportAssicurazioniVeicoli.Click
        Response.Redirect("ImportAssicurazioniVeicoli.aspx")
    End Sub

    Protected Sub dropCercaMarca_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropCercaMarca.SelectedIndexChanged
        dropCercaModello.Items.Clear()
        dropCercaModello.Items.Add("Seleziona...")
        dropCercaModello.Items(0).Value = 0
        dropCercaModello.DataBind()
    End Sub

    Protected Sub btnImportDismissioni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportDismissioni.Click
        Response.Redirect("ImportDismissioni.aspx")
    End Sub

    Protected Sub btnEsportaFatture_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEsportaFatture.Click
        If (txtFattDa.Text <> "" And txtFattA.Text <> "") Or (txtFatturaAcquistoDa.Text <> "" And txtFatturaAcquistoA.Text <> "") Then
            EsportaFatture()
        Else
            Libreria.genUserMsgBox(Me, "Specificare un intervallo di fatture di vendita o un intervallo di fatture di acquisto.")
        End If
    End Sub

    Protected Function getData(ByVal mia_data As String) As String
        If mia_data <> "" Then
            getData = Left(mia_data, 6) & Right(mia_data, 2)
        Else
            getData = "        "
        End If
    End Function

    Protected Sub EsportaFatture()
        'TRACCIATO ATTUALE:
        'TARGA(8);MODELLO(24);IMMATRICOLAZIONE(8);IMMISSIONE IN FLOTTA(8);DATA_ATTO_DI_VENDITA(8);DATA_ACQUISTO;
        '3 FATTURE DI ACQUISTO: NUMERO(8);IMPORTO(8);DATA(8);NUMERO(8);DATA(8);IMPORTO(8);NUMERO(8);DATA(8);IMPORTO(8);
        '2 NOTE DI CREDITO DI ACQUISTO: NUMERO(8);DATA(8);IMPORTO(8);NUMERO(8);DATA(8);IMPORTO(8);
        'DATA_ATTO_DI_VENDITA(8);
        '2 FATTURE DI VENDITA: NUMERO(8);DATA(8);IMPORTO(8);NUMERO(8);DATA(8);IMPORTO(8);
        '1 NOTA DI CREDITO DI VENDITA: NUMERO(8);DATA(8);IMPORTO(8);
        ' FONDO DI AMMORTAMENTO(8);TELAIO(29);MARCA(29)
        'PER LE FATTURE E NOTE CREDITO DI ACQUISTO METTERE 0 NELL'IMPORTO SE CI SONO MENO FATTURE/NC DI QUELLI PREVISTI DAL TRACCIATO
        'PER LE FATTURE E NOTE CREDITO DI VENDITA METTERE 0 SIA NEL NUMERO CHE NELL'IMPORTO SE CI SONO MENO FATTURE/NC DI QUELLI PREVISTI DAL TRACCIATO

        Try
            Dim filePath As String
            filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\docs"

            Dim fileTesto As String = "FATTURE_" & Day(Now()) & Month(Now()) & Year(Now()) & ".txt"

            Dim fs = CreateObject("Scripting.FileSystemObject")
            Dim filetxt = fs.CreateTextFile(filePath & "\" & fileTesto, True) 'CREA IL FILE DI TESTO

            'filetxt.writeline("ciao;")

            Dim data1 As String = funzioni_comuni.getDataDb_senza_orario(txtFattDa.Text)
            Dim data2 As String = funzioni_comuni.getDataDb_con_orario(txtFattA.Text & " 23:59:59")

            'SELEZIONO I VEICOLI PER CUI E' PRESENTE UNA FATTURA DI VENDITA PER IL PERIODO CONSIDERATO ---------------------------------------
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc2.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT veicoli.id As id_veicolo, CAST(veicoli.targa As char(8)) As targa, CAST(veicoli.telaio As Char(29)) As telaio, CAST(ISNULL(veicoli.fondo_ammortamento,'0') As Char(8)) As fondo_ammortamento, CAST(marche.descrizione As Char(29)) As Marca,  CAST(modelli.descrizione As char(24)) As modello, CONVERT(char(10), veicoli.data_immatricolazione,103) As data_immatricolazione, CONVERT(char(10), veicoli.data_atto_vendita,103) As data_atto_vendita,CONVERT(char(10), veicoli.data_bolla,103) As data_bolla, CONVERT(char(10), veicoli.data_acquisto,103) As data_acquisto, (SELECT CONVERT(Char(10),movimenti_targa.data_rientro,103) FROM movimenti_targa WITH(NOLOCK) WHERE movimenti_targa.id_veicolo=veicoli.id AND movimenti_targa.id_tipo_movimento='" & Costanti.IdImmissioneInParco & "') As data_immissione_in_flotta FROM veicoli WITH(NOLOCK) INNER JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello INNER JOIN marche WITH(NOLOCK) ON modelli.id_CasaAutomobilistica=marche.id WHERE veicoli.id>0 " & condizioneWhere() & " ORDER BY targa", Dbc)
            Dim Cmd2 As New Data.SqlClient.SqlCommand("", Dbc2)

            Dim Rs As Data.SqlClient.SqlDataReader
            Dim Rs2 As Data.SqlClient.SqlDataReader

            Rs = Cmd.ExecuteReader()

            Dim riga As String = ""
            Dim data_provv As String = ""

            Dim sezione1 As String = ""

            Dim fatture_acquisto_sezione_2 As String = ""
            Dim note_credito_acquisto_sezione_2 As String = ""

            Dim sezione_3 As String = ""

            Dim fatture_vendita_sezione_4 As String = ""
            Dim note_credito_vendita_sezione_4 As String = ""

            Dim sezione_5 As String = ""

            Dim numero_fatture_acquisto As Integer
            Dim numero_note_credito_acquisto As Integer

            Dim numero_fatture_vendita As Integer
            Dim numero_note_credito_vendita As Integer


            Do While Rs.Read()
                'SEZIONE 1-----------------------------------------------------------------------------------------------------------------
                sezione1 = Rs("targa") & ";" & Rs("modello") & ";" & getData(Rs("data_immatricolazione") & "") & ";" & getData(Rs("data_immissione_in_flotta") & "") & ";"

                sezione1 = sezione1 & getData(Rs("data_bolla") & "") & ";" & getData(Rs("data_acquisto") & "") & ";"
                '--------------------------------------------------------------------------------------------------------------------------
                'SEZIONE 2 - FATTURE DI ACQUISTO (3 FATTURE + 2 NOTE DI CREDITO) ----------------------------------------------------------
                Dbc2.Close()
                Dbc2.Open()

                Cmd2 = New Data.SqlClient.SqlCommand("SELECT CONVERT(char(10), data_acquisto,103) As data_acquisto, CAST(ABS(imponibile_acquisto) As Char(8)) As imponibile_acquisto, CAST(num_acquisto As Char(8)) As num_acquisto, tipo FROM fatture_acquisto_veicoli WITH(NOLOCK) WHERE id_veicolo='" & Rs("id_veicolo") & "' ORDER BY tipo, data_acquisto DESC", Dbc2)
                Rs2 = Cmd2.ExecuteReader()

                numero_fatture_acquisto = 0
                numero_note_credito_acquisto = 0

                fatture_acquisto_sezione_2 = ""
                note_credito_acquisto_sezione_2 = ""

                Do While Rs2.Read()
                    If Rs2("tipo") = "FATTURA" And numero_fatture_acquisto < 3 Then
                        If numero_fatture_acquisto = 0 Then
                            fatture_acquisto_sezione_2 = Rs2("num_acquisto") & ";" & Replace(Rs2("imponibile_acquisto"), ".", ",") & ";" & getData(Rs2("data_acquisto") & "") & ";"
                        ElseIf numero_fatture_acquisto = 1 Then
                            fatture_acquisto_sezione_2 = fatture_acquisto_sezione_2 & Rs2("num_acquisto") & ";" & getData(Rs2("data_acquisto") & "") & ";" & Replace(Rs2("imponibile_acquisto"), ".", ",") & ";"
                        ElseIf numero_fatture_acquisto = 2 Then
                            fatture_acquisto_sezione_2 = fatture_acquisto_sezione_2 & getData(Rs2("data_acquisto") & "") & ";" & Rs2("num_acquisto") & ";" & Replace(Rs2("imponibile_acquisto"), ".", ",") & ";"
                        End If
                        numero_fatture_acquisto = numero_fatture_acquisto + 1
                    ElseIf Rs2("tipo") = "NOTA" And numero_note_credito_acquisto < 2 Then
                        note_credito_acquisto_sezione_2 = note_credito_acquisto_sezione_2 & Rs2("num_acquisto") & ";" & getData(Rs2("data_acquisto") & "") & ";" & Replace(Rs2("imponibile_acquisto"), ".", ",") & ";"
                        numero_note_credito_acquisto = numero_note_credito_acquisto + 1
                    End If
                Loop

                'RIEMPO I RIMANENTI SPAZI COME VUOTI
                For numero_fatture = numero_fatture_acquisto To 2
                    fatture_acquisto_sezione_2 = fatture_acquisto_sezione_2 & "        ;" & "        ;" & "0       ;"
                Next
                For numero_note_credito = numero_note_credito_acquisto To 1
                    note_credito_acquisto_sezione_2 = note_credito_acquisto_sezione_2 & "        ;" & "        ;" & "0       ;"
                Next

                Rs2.Close()
                '---------------------------------------------------------------------------------------------------------------------------
                'SEZIONE 3 -----------------------------------------------------------------------------------------------------------------
                sezione_3 = getData(Rs("data_atto_vendita") & "") & ";"
                '---------------------------------------------------------------------------------------------------------------------------
                'SEZIONE 4 FATTURE DI VENDITA (2 FATTURE + 1 NOTE DI CREDITO)---------------------------------------------------------------
                Dbc2.Close()
                Dbc2.Open()

                Cmd2 = New Data.SqlClient.SqlCommand("SELECT CONVERT(char(10), data_vendita,103) As data_vendita, CAST(ABS(imponibile_vendita) As Char(8)) As imponibile_vendita, CAST(num_vendita As Char(8)) As num_vendita, tipo FROM fatture_vendita_veicoli WITH(NOLOCK) WHERE id_veicolo='" & Rs("id_veicolo") & "' ORDER BY tipo, data_vendita DESC", Dbc2)
                Rs2 = Cmd2.ExecuteReader()

                numero_fatture_vendita = 0
                numero_note_credito_vendita = 0

                fatture_vendita_sezione_4 = ""
                note_credito_vendita_sezione_4 = ""

                Do While Rs2.Read()
                    If Rs2("tipo") = "FATTURA" And numero_fatture_vendita < 2 Then
                        fatture_vendita_sezione_4 = fatture_vendita_sezione_4 & getData(Rs2("data_vendita") & "") & ";" & Rs2("num_vendita") & ";" & Replace(Rs2("imponibile_vendita"), ".", ",") & ";"

                        numero_fatture_vendita = numero_fatture_vendita + 1
                    ElseIf Rs2("tipo") = "NOTA" And numero_note_credito_vendita < 1 Then
                        note_credito_vendita_sezione_4 = note_credito_vendita_sezione_4 & getData(Rs2("data_vendita") & "") & ";" & Rs2("num_vendita") & ";" & Replace(Rs2("imponibile_vendita"), ".", ",") & ";"

                        numero_note_credito_vendita = numero_note_credito_vendita + 1
                    End If
                Loop

                'RIEMPO I RIMANENTI SPAZI COME VUOTI

                For numero_fatture_ven = numero_fatture_vendita To 1
                    fatture_vendita_sezione_4 = fatture_vendita_sezione_4 & "        ;" & "0       ;" & "0       ;"
                Next
                For numero_note_credito_ven = numero_note_credito_vendita To 0
                    note_credito_vendita_sezione_4 = note_credito_vendita_sezione_4 & "        ;" & "0       ;" & "0       ;"
                Next

                Rs2.Close()
                '--------------------------------------------------------------------------------------------------------------------------
                'SEZIONE 5 ----------------------------------------------------------------------------------------------------------------
                sezione_5 = Replace(Rs("fondo_ammortamento"), ".", ",") & ";" & Rs("telaio") & ";" & Rs("marca")
                '--------------------------------------------------------------------------------------------------------------------------

                'SEZIONE 6 - SALVATAGGIO DATI----------------------------------------------------------------------------------------------
                riga = sezione1 & fatture_acquisto_sezione_2 & note_credito_acquisto_sezione_2 & sezione_3 & fatture_vendita_sezione_4 & note_credito_vendita_sezione_4 & sezione_5
                filetxt.writeline(riga)
                '--------------------------------------------------------------------------------------------------------------------------
            Loop

            ''SCRIVO L'ULTIMA RIGA SU FILE STREAM -----------------------------------------------------------------------------------------
            'For numero_fatture_ven = numero_fatture_vendita To 1
            '    fatture_vendita_sezione_4 = fatture_vendita_sezione_4 & "        ;" & "0       ;" & "0       ;"
            'Next
            'For numero_note_credito_ven = numero_note_credito_vendita To 0
            '    note_credito_vendita_sezione_4 = note_credito_vendita_sezione_4 & "        ;" & "0       ;" & "0       ;"
            'Next

            'riga = sezione1 & fatture_acquisto_sezione_2 & note_credito_acquisto_sezione_2 & sezione_3 & fatture_vendita_sezione_4 & note_credito_vendita_sezione_4 & sezione_5
            'filetxt.writeline(riga)
            ''----------------------------------------------------------------------------------------------------------------------------------

            Rs2 = Nothing
            Cmd2.Dispose()
            Cmd2 = Nothing
            Dbc2.Close()
            Dbc2.Dispose()
            Dbc2 = Nothing

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
            '---------------------------------------------------------------------------------------------------------------------------------

            filetxt.close()
            fs = Nothing

            'TRASFERIMENTO FILE AL CLIENT ----------------------------------------------------------------------------------------------------
            DownloadFile(fileTesto)
            '---------------------------------------------------------------------------------------------------------------------------------
        Catch ex As Exception
            Response.Write("Error_EsportaFatture_:" & ex.Message & "br/>")
        End Try


    End Sub

    Protected Sub DownloadFile(ByVal name As String)
        Try
            Dim _path As String = Request.PhysicalApplicationPath & "Docs/" & name
            Dim _file As System.IO.FileInfo = New System.IO.FileInfo(_path)

            If _file.Exists Then
                Response.Clear()

                Response.AddHeader("Content-Disposition", "attachment; filename=" & _file.Name)
                Response.AddHeader("Content-Length", _file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(_file.FullName)
                Response.End()
            Else
                Libreria.genUserMsgBox(Me, " ERRORE - File non generato.")
            End If
        Catch ex As Exception
            Response.Write("Error_DownloadFile_:" & ex.Message & "br/>")
        End Try


    End Sub

    Protected Sub btnAggiornaVeicoli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAggiornaVeicoli.Click
        Response.Redirect("ImportAggiornamentoVeicoli.aspx")
    End Sub

    Private Sub listVeicoli_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles listVeicoli.ItemDataBound
        Dim kmattuali As Label = TryCast(e.Item.FindControl("Label3"), Label)
        Dim targa As Label = TryCast(e.Item.FindControl("targaLabel"), Label)

        'Dim km_effettivi_disponibili As Boolean = funzioni_comuni.GetKmDisponibili(targa.Text)             'spostata su funzioni_comuni_new
        Dim km_effettivi_disponibili As Boolean = funzioni_comuni_new.GetKmDisponibili_new(targa.Text)

        If km_effettivi_disponibili = False Then        'se non ci sono km disponibili
            kmattuali.ForeColor = Drawing.Color.Red
        Else
            kmattuali.ForeColor = Drawing.Color.Black
        End If

    End Sub
End Class
