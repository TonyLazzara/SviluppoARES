
Partial Class riepilogo_pagamenti_pos
    Inherits System.Web.UI.Page

    Protected Sub DropDownCassa_DataBind()
        DropDownCassa.Items.Clear()
        DropDownCassa.Items.Add(New ListItem("Tutte", "0"))
        DropDownCassa.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sqlDettagliPagamento.SelectCommand = lb_sqlDettagliPagamento.Text & " " & lblOrderBY.Text

        If Not Page.IsPostBack() Then
            livello_accesso_dettaglio_pos.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzaDettaglioOperazionePOS)
            lb_Riepilogo_Pagamenti_POS.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Riepilogo_Pagamenti_POS)
            lb_Riepilogo_Pagamenti_POS_Admin.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Riepilogo_Pagamenti_POS_Admin)

            DropDownStazioni.DataBind()
            DropDownFunzioni.DataBind()
        End If
        If Integer.Parse(lb_Riepilogo_Pagamenti_POS.Text) <= 1 Then
            ' Libreria.genUserMsgBox(Page, "Non hai i diritti di accesso a questo modulo.") il messaggio non funziona... con il redirect non funziona
            Response.Redirect("default.aspx")
            Return
        End If

        If Integer.Parse(lb_Riepilogo_Pagamenti_POS_Admin.Text) <= 1 Then
            DropDownStazioni.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
            DropDownCassa_DataBind()
            DropDownStazioni.Enabled = False
        Else
            DropDownStazioni.Enabled = True
        End If

    End Sub


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
                    Dim primo_incasso_preaut As Label = e.Item.FindControl("primo_incasso_preaut")
                    Dim lb_scadenza_preaut As Label = e.Item.FindControl("lb_scadenza_preaut")

                    If primo_incasso_preaut.Text = "" Then
                        'IN QUESTO CASO NON E' STATO MAI TENTATO L'INCASSSO DELLA PREAUTORIZZAZIONE - CONTROLLO SE ' SCADUTA RISPETTO AL GIORNO ODIERNO
                        Dim data_scadenza As DateTime = funzioni_comuni.getDataDb_senza_orario2(lb_scadenza_preaut.Text)
                        If DateDiff(DateInterval.Minute, data_scadenza, Now()) < 0 Then
                            lblStato.Text = "APERTA"
                        Else
                            lblStato.Text = "SCADUTA/NON INCASS."
                        End If
                    Else
                        'SE IL PRIMO TENTATIVO DI INCASSO E' AVVENUTO DOPO LA SCADENZA DELLA PREAUTORIZZAZIONE LA STESSA E' COMUNQUE SCADUTA/NON INCASSATA
                        Dim data_scadenza As DateTime = funzioni_comuni.getDataDb_senza_orario2(lb_scadenza_preaut.Text)
                        Dim primo_tentativo As DateTime = funzioni_comuni.getDataDb_senza_orario2(primo_incasso_preaut.Text)

                        If DateDiff(DateInterval.Minute, data_scadenza, primo_tentativo) < 0 Then
                            lblStato.Text = "SCADUTA/AUT.NEGATA"
                        Else
                            lblStato.Text = "SCADUTA/INCASS.QUANDO SCADUTA"
                        End If

                    End If
                Else
                    lblStato.Text = "CHIUSA"
                End If
            End If
        End If

        If livello_accesso_dettaglio_pos.Text = "1" Then
            btnVedi.Visible = False
        End If

        Dim N_CONTRATTO_RIF As Label = e.Item.FindControl("N_CONTRATTO_RIF")
        Dim N_RDS_RIF As Label = e.Item.FindControl("N_RDS_RIF")
        Dim N_MULTA_RIF As Label = e.Item.FindControl("N_MULTA_RIF")
        Dim N_PREN_RIF As Label = e.Item.FindControl("N_PREN_RIF")

        If N_CONTRATTO_RIF.Text <> "" Then
            N_CONTRATTO_RIF.Text = "RA - " & N_CONTRATTO_RIF.Text
        ElseIf N_RDS_RIF.Text <> "" Then
            N_RDS_RIF.Text = " RDS - " & N_RDS_RIF.Text
        ElseIf N_MULTA_RIF.Text <> "" Then
            N_MULTA_RIF.Text = "MULTA - " & N_MULTA_RIF.Text
        ElseIf N_PREN_RIF.Text <> "" Then
            N_PREN_RIF.Text = "PREN - " & N_PREN_RIF.Text
        End If
    End Sub

    Protected Sub listPagamenti_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listPagamenti.ItemCommand
        If e.CommandName = "vedi" Then
            Try
                div_dettaglio.Visible = True

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
                    txtPOS_Cassa.Text = Libreria.getControCassa(Rs("CASSA") & "")
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
                End If

                Rs.Close()
                Rs = Nothing
                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            Catch ex As Exception
                Response.Write("err listPagamenti_ItemCommand : " & ex.Message & "<br/>" & "<br/>")
            End Try



        ElseIf e.CommandName = "order_by_data" Then
            If lblOrderBY.Text = " ORDER BY DATA_OPERAZIONE DESC" Then
                cerca(" ORDER BY DATA_OPERAZIONE ASC")
            ElseIf lblOrderBY.Text = " ORDER BY DATA_OPERAZIONE ASC" Then
                cerca(" ORDER BY DATA_OPERAZIONE DESC")
            Else
                cerca(" ORDER BY DATA_OPERAZIONE ASC")
            End If
        ElseIf e.CommandName = "order_by_importo" Then
            If lblOrderBY.Text = " ORDER BY PER_IMPORTO DESC" Then
                cerca(" ORDER BY PER_IMPORTO ASC")
            ElseIf lblOrderBY.Text = " ORDER BY PER_IMPORTO ASC" Then
                cerca(" ORDER BY PER_IMPORTO DESC")
            Else
                cerca(" ORDER BY PER_IMPORTO ASC")
            End If
        ElseIf e.CommandName = "order_by_scadenza_pre" Then
            If lblOrderBY.Text = " ORDER BY scadenza_preaut DESC" Then
                cerca(" ORDER BY scadenza_preaut ASC")
            ElseIf lblOrderBY.Text = " ORDER BY scadenza_preaut ASC" Then
                cerca(" ORDER BY scadenza_preaut DESC")
            Else
                cerca(" ORDER BY scadenza_preaut ASC")
            End If
        ElseIf e.CommandName = "order_by_cassa" Then
            If lblOrderBY.Text = " ORDER BY contro_cassa DESC" Then
                cerca(" ORDER BY contro_cassa ASC")
            ElseIf lblOrderBY.Text = " ORDER BY contro_cassa ASC" Then
                cerca(" ORDER BY contro_cassa DESC")
            Else
                cerca(" ORDER BY contro_cassa ASC")
            End If
        ElseIf e.CommandName = "order_by_stazione" Then
            If lblOrderBY.Text = " ORDER BY stazione DESC" Then
                cerca(" ORDER BY stazione ASC")
            ElseIf lblOrderBY.Text = " ORDER BY stazione ASC" Then
                cerca(" ORDER BY stazione DESC")
            Else
                cerca(" ORDER BY stazione ASC")
            End If
        ElseIf e.CommandName = "order_by_tipo_op" Then
            If lblOrderBY.Text = " ORDER BY funzione DESC" Then
                cerca(" ORDER BY funzione ASC")
            ElseIf lblOrderBY.Text = " ORDER BY funzione ASC" Then
                cerca(" ORDER BY funzione DESC")
            Else
                cerca(" ORDER BY funzione ASC")
            End If
        End If
    End Sub

    Protected Sub bt_chiudi_dettagio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_chiudi_dettagio.Click
        div_dettaglio.Visible = False
    End Sub

    Protected Function getSqlWhere(ByVal id_stazione As Integer, ByVal cassa As Integer, ByVal Fonte As Integer, ByVal num_documento As String, ByVal tipo_operazione As Integer, ByVal DaGiorno As String, ByVal AGiorno As String, ByVal ScPrDataDa As String, ByVal ScPrDataA As String, ByVal stato_stornato As Integer) As String
        Dim sqlStr As String = ""
        If id_stazione > 0 Then
            sqlStr += " AND pe.id_stazione = " & id_stazione
        End If
        If cassa > 0 Then
            sqlStr += " AND pe.cassa = " & cassa
        End If
        If Fonte = 1 Then
            sqlStr += " AND pe.N_CONTRATTO_RIF IS NOT NULL"
        ElseIf Fonte = 2 Then
            sqlStr += " AND pe.N_PREN_RIF IS NOT NULL"
        ElseIf Fonte = 3 Then
            sqlStr += " AND pe.N_MULTA_RIF IS NOT NULL"
        ElseIf Fonte = 4 Then
            sqlStr += " AND pe.N_RDS_RIF IS NOT NULL"
        End If
        If num_documento <> "" Then ' se il campo numero documento è valorizzato, deve anche essere fissata la fonte!
            If Fonte = 1 Then
                sqlStr += " AND pe.N_CONTRATTO_RIF = " & Libreria.formattaSqlTrim(num_documento)
            ElseIf Fonte = 2 Then
                sqlStr += " AND pe.N_PREN_RIF = " & Libreria.formattaSqlTrim(num_documento)
            ElseIf Fonte = 3 Then
                sqlStr += " AND pe.N_MULTA_RIF = " & Libreria.formattaSqlTrim(num_documento)
            ElseIf Fonte = 4 Then
                sqlStr += " AND pe.N_RDS_RIF = " & Libreria.formattaSqlTrim(num_documento)
            Else
                sqlStr += " AND (pe.N_CONTRATTO_RIF = " & Libreria.formattaSqlTrim(num_documento) & " OR pe.N_PREN_RIF = " & Libreria.formattaSqlTrim(num_documento) & " OR pe.N_MULTA_RIF = " & Libreria.formattaSqlTrim(num_documento) & " OR pe.N_RDS_RIF = " & Libreria.formattaSqlTrim(num_documento) & ")"
            End If
        End If
        If tipo_operazione > 0 Then
            sqlStr += " AND pe.id_pos_funzioni_ares = " & tipo_operazione
        End If
        Dim dtsql As String
        If DaGiorno <> "" Then
            'Dim DataDaGiorno As DateTime = New DateTime(Year(DaGiorno), Month(DaGiorno), Day(DaGiorno), 0, 0, 0)
            dtsql = funzioni_comuni.GetDataSql(DaGiorno, 0)
            sqlStr += " AND pe.DATA_OPERAZIONE >= CONVERT(DATETIME,'" & dtsql & "',102)"
        End If
        If AGiorno <> "" Then
            'Dim DataAGiorno As DateTime '= DateAdd(DateInterval.Day, 1, New DateTime(Year(AGiorno), Month(AGiorno), Day(AGiorno), 0, 0, 0))
            dtsql = funzioni_comuni.GetDataSql(AGiorno, 59)
            sqlStr += " AND pe.DATA_OPERAZIONE < CONVERT(DATETIME,'" & dtsql & "',102)"
        End If
        If ScPrDataDa <> "" Then
            'Dim DataDaGiorno As DateTime '= New DateTime(Year(ScPrDataDa), Month(ScPrDataDa), Day(ScPrDataDa), 0, 0, 0)
            dtsql = funzioni_comuni.GetDataSql(ScPrDataDa, 0)
            sqlStr += " AND pe.scadenza_preaut >= CONVERT(DATETIME,'" & dtsql & "',102)"
        End If
        If ScPrDataA <> "" Then
            'Dim DataAGiorno As DateTime '= DateAdd(DateInterval.Day, 1, New DateTime(Year(ScPrDataA), Month(ScPrDataA), Day(ScPrDataA), 0, 0, 0))
            dtsql = funzioni_comuni.GetDataSql(ScPrDataA, 59)
            sqlStr += " AND pe.scadenza_preaut < CONVERT(DATETIME,'" & dtsql & "',102)"
        End If

        If stato_stornato = 1 Then
            sqlStr += " AND (pe.operazione_stornata = 1)"
        ElseIf stato_stornato = 2 Then
            sqlStr += " AND (pe.operazione_stornata IS NULL OR pe.operazione_stornata = 0)"
        ElseIf stato_stornato = 3 Then
            sqlStr += " AND (pe.preaut_aperta='1')"
        ElseIf stato_stornato = 4 Then
            sqlStr += " AND (pe.preaut_aperta='0')"
        ElseIf stato_stornato = 5 Then
            sqlStr += " AND (pe.preaut_aperta='1' AND scadenza_preaut<=GetDate())"
        ElseIf stato_stornato = 6 Then
            sqlStr += " AND (pe.preaut_aperta='1' AND scadenza_preaut<=GetDate() AND primo_incasso_preaut IS NULL)"
        ElseIf stato_stornato = 7 Then
            sqlStr += " AND (pe.preaut_aperta='1' AND scadenza_preaut<=GetDate() AND primo_incasso_preaut<=scadenza_preaut)"
        ElseIf stato_stornato = 8 Then
            sqlStr += " AND (pe.preaut_aperta='1' AND scadenza_preaut<=GetDate() AND primo_incasso_preaut>scadenza_preaut)"
        End If

        If txtNrPreaut.Text <> "" Then
            sqlStr += " AND NR_PREAUT='" & txtNrPreaut.Text & "'"
        End If

        Return sqlStr
    End Function


    Protected Function getSqlElenco(ByVal id_stazione As Integer, ByVal cassa As Integer, ByVal Fonte As Integer, ByVal num_documento As String, ByVal tipo_operazione As Integer, ByVal DaGiorno As String, ByVal AGiorno As String, ByVal ScPrDataDa As String, ByVal ScPrDataA As String, ByVal stato_stornato As Integer, ByVal order_by As String) As String
        Dim sqlStr As String = "SELECT pf.funzione, pe.id_pos_funzioni_ares, pe.cassa, pos.contro_cassa," &
            " pe.DATA_OPERAZIONE, pe.PER_IMPORTO, pe.ID_CTR," &
            " pe.preaut_aperta, pe.operazione_stornata, pe.scadenza_preaut, pe.primo_incasso_preaut," &
            " pe.N_CONTRATTO_RIF, pe.N_RDS_RIF, pe.N_MULTA_RIF, pe.N_PREN_RIF," &
            " (s.codice + ' - ' + s.nome_stazione) As stazione" &
            " FROM PAGAMENTI_EXTRA pe WITH(NOLOCK)" &
            " INNER JOIN pos WITH(NOLOCK) ON pe.cassa = pos.cassa" &
            " INNER JOIN POS_Funzioni pf WITH(NOLOCK) ON pe.id_pos_funzioni_ares = pf.id" &
            " INNER JOIN stazioni s WITH(NOLOCK) ON pe.id_stazione = s.id" &
            " WHERE 1 = 1"

        sqlStr += getSqlWhere(id_stazione, cassa, Fonte, num_documento, tipo_operazione, DaGiorno, AGiorno, ScPrDataDa, ScPrDataA, stato_stornato)

        lblOrderBY.Text = order_by

        Return sqlStr
    End Function

    Protected Sub cerca(ByVal order_by As String)

        Dim sqlstr As String = ""
        Try
            sqlstr = getSqlElenco(Integer.Parse(DropDownStazioni.SelectedValue),
                                                                Integer.Parse(DropDownCassa.SelectedValue),
                                                                Integer.Parse(dropFonte.SelectedValue),
                                                                txtNumeroFonte.Text,
                                                                Integer.Parse(DropDownFunzioni.SelectedValue),
                                                                tx_DataDa.Text,
                                                                tx_DataA.Text,
                                                                tx_ScPrDataDa.Text,
                                                                tx_ScPrDataA.Text,
                                                                Integer.Parse(DropDownStornata.SelectedValue), order_by)

            lb_sqlDettagliPagamento.Text = sqlstr

            sqlDettagliPagamento.SelectCommand = lb_sqlDettagliPagamento.Text & " " & lblOrderBY.Text

            listPagamenti.DataBind()
        Catch ex As Exception
            Response.Write("err cerca : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try




    End Sub

    Protected Sub bt_cerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_cerca.Click
        cerca("ORDER BY DATA_OPERAZIONE DESC")
    End Sub

    Protected Sub DropDownStazioni_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownStazioni.SelectedIndexChanged
        DropDownCassa_DataBind()
    End Sub

    Protected Sub bt_stampa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_stampa.Click
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            Dim sql_str As String = Server.UrlEncode(lb_sqlDettagliPagamento.Text & " " & lblOrderBY.Text)
            Dim url_print As String = "/Stampe/stampa_elenco_pagamenti_pos.aspx?orientamento=orizzontale&sql_str=" & sql_str

            Dim mio_random As String = Format((New Random).Next(), "0000000000")

            Session("url_print") = url_print
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx?a=" & mio_random & "','')", True)
            End If
        End If
    End Sub

    Protected Sub btnScadenziario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScadenziario.Click
        'IMPOSTA I CAMPI PER EFFETTUARE LA RICERCA COME SCADENZIARIO
        DropDownStazioni.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
        DropDownCassa.SelectedValue = "0"
        DropDownFunzioni.SelectedValue = enum_tipo_pagamento_ares.Richiesta
        tx_DataA.Text = ""
        tx_DataDa.Text = ""
        tx_ScPrDataDa.Text = Format(Now().AddDays(-7), "dd/MM/yyyy")
        tx_ScPrDataA.Text = Format(Now().AddDays(5), "dd/MM/yyyy")
        dropFonte.SelectedValue = "0"
        txtNumeroFonte.Text = ""
        DropDownStornata.SelectedValue = "3"

        cerca("ORDER BY scadenza_preaut DESC")
    End Sub

    Protected Sub btnRiepilogoPagPosGiornata_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRiepilogoPagPosGiornata.Click
        'IMPOSTA I CAMPI PER EFFETTUARE LA RICERCA COME SCADENZIARIO
        DropDownStazioni.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
        DropDownCassa.SelectedValue = "0"
        DropDownFunzioni.SelectedValue = "0"
        tx_DataA.Text = Format(Now(), "dd/MM/yyyy")
        tx_DataDa.Text = Format(Now(), "dd/MM/yyyy")
        tx_ScPrDataDa.Text = ""
        tx_ScPrDataA.Text = ""
        dropFonte.SelectedValue = "0"
        txtNumeroFonte.Text = ""
        DropDownStornata.SelectedValue = "0"

        cerca(" ORDER BY scadenza_preaut DESC")
    End Sub
End Class
