
Partial Class esporta_fatture_nolo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'lblOrderBY.Text = " ORDER BY contratti.data_rientro DESC"

            For i As Integer = 2009 To Year(Now())
                dropAnnoFattura.Items.Add(i)
            Next

            dropAnnoFattura.SelectedValue = Year(Now())

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Fatturazione) = "1" Then
                Response.Redirect("default.aspx")
            End If

            'ricerca(lblOrderBY.Text)
        Else
            sqlFatture.SelectCommand = query_cerca.Text & " ORDER BY num_fattura ASC"
        End If
    End Sub

    Protected Function getIdContratto(ByVal num_contratto As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM contratti WITH(NOLOCK) WHERE num_contratto='" & num_contratto & "' AND attivo='1'", Dbc)

        getIdContratto = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub listDaFatturare_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listDaFatturare.ItemCommand
        If e.CommandName = "vedi_fattura" Then

            Session("DatiStampaFatturaNolo") = StampaFatturaNolo.genera_dati_stampa_fattura(e.CommandArgument)

            Dim Generator As System.Random = New System.Random()

            Dim num_random As String = Format(Generator.Next(100000000), "000000000")


            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraFatturaNolo.aspx?a=" & num_random & "','')", True)
                End If
            End If
        ElseIf e.CommandName = "vedi_ra" Then
            Dim num_contratto As LinkButton = e.Item.FindControl("num_contratto")
            Session("carica_contratto") = getIdContratto(num_contratto.Text)

            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('contratti.aspx','')", True)
            End If
        End If
    End Sub

    Protected Sub btnClientiFase1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClientiFase1.Click
        If txtNumeroFatturaDa.Text = "" Or txtNumeroFatturaA.Text = "" Then
            Libreria.genUserMsgBox(Me, "Specificare un range di numeri di fattura da esportare")
        ElseIf txtDataFatturaDa.Text <> "" Or txtDataFatturaA.Text <> "" Then
            Libreria.genUserMsgBox(Me, "Attenzione: specificare solamente anno e numeri di fattura da esportare. Non verranno presi in considerazione altri filtri.")
        Else
            fatturazione_nolo.genera_flusso_clienti(dropAnnoFattura.SelectedValue, txtNumeroFatturaDa.Text, txtNumeroFatturaA.Text)
            Libreria.genUserMsgBox(Me, "Esportazione clienti completata correttamente.")
        End If
    End Sub

    Protected Sub btnEsportaFatture_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEsportaFatture.Click
        If txtNumeroFatturaDa.Text = "" Or txtNumeroFatturaA.Text = "" Then
            Libreria.genUserMsgBox(Me, "Specificare un range di numeri di fattura da esportare")
        ElseIf txtDataFatturaDa.Text <> "" Or txtDataFatturaA.Text <> "" Then
            Libreria.genUserMsgBox(Me, "Attenzione: specificare solamente anno e numeri di fattura da esportare. Non verranno presi in considerazione altri filtri.")
        Else
            fatturazione_nolo.genera_flusso_fatture_nolo(dropAnnoFattura.SelectedValue, txtNumeroFatturaDa.Text, txtNumeroFatturaA.Text)
            Libreria.genUserMsgBox(Me, "Esportazione delle fatture completata correttamente.")
        End If
    End Sub

    Protected Sub btnEsportaPagamenti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEsportaPagamenti.Click
        If txtNumeroFatturaDa.Text = "" Or txtNumeroFatturaA.Text = "" Then
            Libreria.genUserMsgBox(Me, "Specificare un range di numeri di fattura da esportare")
        ElseIf txtDataFatturaDa.Text <> "" Or txtDataFatturaA.Text <> "" Then
            Libreria.genUserMsgBox(Me, "Attenzione: specificare solamente anno e numeri di fattura da esportare. Non verranno presi in considerazione altri filtri.")
        Else
            fatturazione_nolo.genera_flusso_pagamenti(dropAnnoFattura.SelectedValue, txtNumeroFatturaDa.Text, txtNumeroFatturaA.Text)
            Libreria.genUserMsgBox(Me, "Esportazione dei pagamenti completata correttamente.")
        End If
    End Sub

    Protected Sub btnEsportaOttico_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEsportaOttico.Click
        If txtNumeroFatturaDa.Text = "" Or txtNumeroFatturaA.Text = "" Then
            Libreria.genUserMsgBox(Me, "Specificare un range di numeri di fattura da esportare")
        ElseIf txtDataFatturaDa.Text <> "" Or txtDataFatturaA.Text <> "" Then
            Libreria.genUserMsgBox(Me, "Attenzione: specificare solamente anno e numeri di fattura da esportare. Non verranno presi in considerazione altri filtri.")
        Else
            fatturazione_nolo.genera_flusso_fattura_ottico(dropAnnoFattura.SelectedValue, txtNumeroFatturaDa.Text, txtNumeroFatturaA.Text)
            Libreria.genUserMsgBox(Me, "Esportazione fatture ottico completata correttamente.")
        End If
    End Sub

    Protected Sub btnClientiFase2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClientiFase2.Click
        'UGUALE ALLA PRIMA FASE MA ABILITA ALLA FINE LA div CHE CONTIENE LA GESTIONE DELLA VARIAZIONE CLIENTI SU CONTRATTO
        If txtNumeroFatturaDa.Text = "" Or txtNumeroFatturaA.Text = "" Then
            Libreria.genUserMsgBox(Me, "Specificare un range di numeri di fattura da esportare")
        ElseIf txtDataFatturaDa.Text <> "" Or txtDataFatturaA.Text <> "" Then
            Libreria.genUserMsgBox(Me, "Attenzione: specificare solamente anno e numeri di fattura da esportare. Non verranno presi in considerazione altri filtri.")
        Else
            fatturazione_nolo.genera_flusso_clienti(dropAnnoFattura.SelectedValue, txtNumeroFatturaDa.Text, txtNumeroFatturaA.Text)
            div_cerca.Visible = False
            div_fase2.Visible = True
            listFase2.Visible = False
            btnEseguiModifiche.Visible = False
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click
        div_cerca.Visible = True
        div_fase2.Visible = False
    End Sub

    Protected Sub btnLeggiClicon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLeggiClicon.Click
        Dim leggi As String = fatturazione_nolo.leggi_cli_con(Request.Cookies("SicilyRentCar")("idUtente"))

        If leggi = "-1" Then
            Libreria.genUserMsgBox(Me, "Attenzione: il file cli_con.txt non è stato trovato.")
        ElseIf leggi = "1" Then
            listFase2.Visible = True
            btnEseguiModifiche.Visible = True
            listFase2.DataBind()
        End If
    End Sub

    Protected Sub btnEseguiModifiche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEseguiModifiche.Click
        'CONTROLLO CHE PER TUTTE LE RIGHE SIA STATA EFFETTUATA UNA SCELTA
        Dim possibile_procedere As Boolean = True
        Dim radioClienteFattura As RadioButton
        Dim radioClientePrecedente As RadioButton

        For i = 0 To listFase2.Items.Count - 1
            radioClienteFattura = listFase2.Items(i).FindControl("radioClienteFattura")
            radioClientePrecedente = listFase2.Items(i).FindControl("radioClientePrecedente")

            If Not radioClienteFattura.Checked And Not radioClientePrecedente.Checked Then
                possibile_procedere = False

                Exit For
            End If
        Next

        If Not possibile_procedere Then
            libreria.genUserMsgBox(Me, "Attenzione: specificare per ogni riga quale anagrafica esportare in contabilità.")
        Else
            'PER OGNI RIGA PER CUI SI E' DECISO DI SOSTITUIRE 
            Dim id_cliente_precedente As Label
            Dim id_cliente_fattura As Label
            Dim codice_edp As Label
            Dim intestazione As Label
            Dim indirizzo As Label
            Dim citta As Label
            Dim cap As Label
            Dim provincia As Label
            Dim id_nazione As Label
            Dim nazione As Label
            Dim piva As Label
            Dim c_fis As Label

            Dim sqlStr As String

            For i = 0 To listFase2.Items.Count - 1
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)



                radioClienteFattura = listFase2.Items(i).FindControl("radioClienteFattura")
                radioClientePrecedente = listFase2.Items(i).FindControl("radioClientePrecedente")

                If radioClientePrecedente.Checked Then
                    'SOSTITUZIONE DEL CLIENTE NEL CONTRATTO
                    id_cliente_precedente = listFase2.Items(i).FindControl("id_cliente_precedente")
                    id_cliente_fattura = listFase2.Items(i).FindControl("id_cliente_fattura")
                    codice_edp = listFase2.Items(i).FindControl("codice_edp")
                    intestazione = listFase2.Items(i).FindControl("rag_soc2")
                    indirizzo = listFase2.Items(i).FindControl("indirizzo2")
                    citta = listFase2.Items(i).FindControl("citta2")
                    cap = listFase2.Items(i).FindControl("cap")
                    provincia = listFase2.Items(i).FindControl("provincia")
                    id_nazione = listFase2.Items(i).FindControl("id_nazione")
                    nazione = listFase2.Items(i).FindControl("nazione")
                    piva = listFase2.Items(i).FindControl("piva2")
                    c_fis = listFase2.Items(i).FindControl("codice_fiscale2")

                    sqlStr = "UPDATE contratti SET codice_edp_old_fatturazione=codice_edp, id_cliente_old_fatturazione=id_cliente," & _
                        " codice_edp='" & codice_edp.Text & "', id_cliente='" & id_cliente_precedente.Text & "' " & _
                        " WHERE num_contratto IN (SELECT DISTINCT num_contratto_rif FROM fatture_nolo WITH(NOLOCK) WHERE" & _
                        " fatture_nolo.num_fattura BETWEEN " & CInt(txtNumeroFatturaDa.Text) & " AND " & CInt(txtNumeroFatturaA.Text) & " AND fatture_nolo.attiva='1'" & _
                        " AND YEAR(fatture_nolo.data_fattura)='" & dropAnnoFattura.SelectedItem.Text & "' AND contratti.id_cliente='" & id_cliente_fattura.Text & "')"
                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.ExecuteNonQuery()

                    'SOSTITUZIONE DEI DATI NELLA FATTURA 
                    sqlStr = "UPDATE fatture_nolo SET id_ditta='" & id_cliente_precedente.Text & "', cod_edp='" & codice_edp.Text & "', " & _
                        "intestazione='" & intestazione.Text.Replace("'", "''") & "', indirizzo='" & indirizzo.Text.Replace("'", "''") & "', " & _
                        "citta='" & citta.Text.Replace("'", "''") & "', cap='" & cap.Text.Replace("'", "''") & "', provincia='" & provincia.Text.Replace("'", "''") & "', " & _
                        "nazione='" & nazione.Text.Replace("'", "''") & "', piva='" & piva.Text.Replace("'", "''") & "', codice_fiscale='" & c_fis.Text.Replace("'", "''") & "' " & _
                        "WHERE fatture_nolo.num_fattura BETWEEN " & CInt(txtNumeroFatturaDa.Text) & " AND " & CInt(txtNumeroFatturaA.Text) & " AND fatture_nolo.attiva='1' " & _
                        "AND YEAR(fatture_nolo.data_fattura)='" & dropAnnoFattura.SelectedItem.Text & "' AND fatture_nolo.id_ditta='" & id_cliente_fattura.Text & "'"

                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.ExecuteNonQuery()
                End If

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            Next
        End If
    End Sub

    Protected Function condizione_where() As String
        'CONDIZIONE WHERE --------------------
        Dim condizione As String = ""

        condizione = condizione & " AND YEAR(fatture_nolo.data_fattura)=" & dropAnnoFattura.SelectedValue

        If Trim(txtNumeroFatturaDa.Text) <> "" Then
            condizione = condizione & " AND num_fattura>=" & txtNumeroFatturaDa.Text
        End If

        If Trim(txtNumeroFatturaA.Text) <> "" Then
            condizione = condizione & " AND num_fattura<=" & txtNumeroFatturaA.Text
        End If

        If txtDataFatturaDa.Text <> "" And txtDataFatturaA.Text = "" Then
            Dim da_data As String = funzioni_comuni.getDataDb_senza_orario(txtDataFatturaDa.Text)
            condizione = condizione & " AND fatture_nolo.data_fattura >='" & da_data & "' "
        ElseIf txtDataFatturaDa.Text = "" And txtDataFatturaA.Text <> "" Then
            Dim a_data As String = funzioni_comuni.getDataDb_con_orario(txtDataFatturaA.Text & " 23:59:59")
            condizione = condizione & " AND fatture_nolo.data_fattura <='" & a_data & "' "
        ElseIf txtDataFatturaDa.Text <> "" And txtDataFatturaA.Text <> "" Then
            Dim da_data As String = funzioni_comuni.getDataDb_con_orario(txtDataFatturaDa.Text)
            Dim a_data As String = funzioni_comuni.getDataDb_con_orario(txtDataFatturaA.Text & " 23:59:59")
            condizione = condizione & " AND fatture_nolo.data_fattura BETWEEN '" & da_data & "' AND '" & a_data & "' "
        End If

        condizione_where = condizione
    End Function

    Protected Sub ricerca()
        query_cerca.Text = "SELECT id, num_fattura, num_contratto_rif, fattura_cliente, fattura_broker, fattura_costi_prepagati, num_prenotazione_rif, " & _
          "CONVERT(Char(10), prenotazioni.datapren,103) As datapren,CONVERT(Char(10), fatture_nolo.data_fattura,103) As data_fattura, CAST(cod_edp As NVARCHAR(20)) + ' - ' + intestazione As ditta, fatture_nolo.saldo " & _
          "FROM fatture_nolo WITH(NOLOCK) LEFT JOIN prenotazioni WITH(NOLOCK) ON fatture_nolo.num_prenotazione_rif=prenotazioni.numpren " & _
          "WHERE fatture_nolo.attiva=1 AND da_esportare_contabilita=1 AND ISNULL(prenotazioni.attiva,1)=1  " & _
                   condizione_where() & " "


        sqlFatture.SelectCommand = query_cerca.Text & " ORDER BY num_fattura ASC"

        listDaFatturare.DataBind()
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        ricerca()
    End Sub

    Protected Sub listDaFatturare_ItemDataBound(ByVal sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listDaFatturare.ItemDataBound
        Dim chkCliente As CheckBox = e.Item.FindControl("chkCliente")
        Dim chkBroker As CheckBox = e.Item.FindControl("chkBroker")
        Dim chkPrepagato As CheckBox = e.Item.FindControl("chkPrepagato")
        Dim lblTipoFattura As Label = e.Item.FindControl("lblTipoFattura")

        If chkCliente.Checked Then
            lblTipoFattura.Text = "Cliente"
        ElseIf chkBroker.Checked Then
            lblTipoFattura.Text = "Broker"
        ElseIf chkPrepagato.Checked Then
            lblTipoFattura.Text = "Prepag."
        End If

    End Sub
End Class
