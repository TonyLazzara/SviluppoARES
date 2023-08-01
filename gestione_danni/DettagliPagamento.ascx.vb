


Partial Class gestione_danni_DettagliPagamento
    Inherits System.Web.UI.UserControl

    Public Delegate Sub EventoPagamento_RDS(ByVal sender As Object, ByVal e As EventoNuovoRecord)
    Event Pagamento_RDS As EventHandler

    Public Delegate Sub EventoPagamento_RDS_RA(ByVal sender As Object, ByVal e As EventoNuovoRecord)
    Event Pagamento_RDS_RA As EventHandler

    Public Delegate Sub EventoPagamento_RA(ByVal sender As Object, ByVal e As EventoNuovoRecord)
    Event Pagamento_RA As EventHandler

    Public Sub InitForm(Optional num_pren As String = "", Optional num_contratto As String = "", Optional num_rds As String = "", Optional num_multa As String = "", Optional AbilitaPagamento As Boolean = False)
        Trace.Write("________________________________________________________________")
        Trace.Write("gestione_danni_DettagliPagamento.InitForm: " & num_pren & " - " & num_contratto & " - " & num_rds & " - " & num_multa)
        Trace.Write("________________________________________________________________")

        If num_pren = "" Then
            lb_num_pren.Text = -1
        Else
            lb_num_pren.Text = num_pren
        End If
        If num_contratto = "" Then
            lb_num_contratto.Text = -1
        Else
            lb_num_contratto.Text = num_contratto
        End If
        If num_rds = "" Then
            lb_num_rds.Text = -1
        Else
            lb_num_rds.Text = num_rds
        End If
        If num_multa = "" Then
            lb_num_multa.Text = -1
        Else
            lb_num_multa.Text = num_multa
        End If

        lb_th_pagamento.Text = AbilitaPagamento

        If AbilitaPagamento Then
            If num_rds <> "" Then
                lb_PreautorizzazioneRDS.Text = VerificaPreautorizzazioneRDS(num_rds)
            End If
        Else
            lb_PreautorizzazioneRDS.Text = False
        End If

        listPagamenti.DataBind()
        ImpostaTotaliPagamento(num_pren, num_contratto, num_rds, num_multa)

        tab_dettagli_pagamento.Visible = True
        riga_pagamento_pos.Visible = False
    End Sub

    Public Sub AggiornaTabella()
        Trace.Write("________________________________________________________________")
        Trace.Write("AggiornaTabella")
        Trace.Write("________________________________________________________________")
        If Boolean.Parse(lb_th_pagamento.Text) Then
            If lb_num_rds.Text <> "" Then
                lb_PreautorizzazioneRDS.Text = VerificaPreautorizzazioneRDS(lb_num_rds.Text)
            End If
        Else
            lb_PreautorizzazioneRDS.Text = False
        End If
        listPagamenti.DataBind()
        ImpostaTotaliPagamento(lb_num_pren.Text, lb_num_contratto.Text, lb_num_rds.Text, lb_num_multa.Text)
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            livello_accesso_eliminare_pagamenti.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.EliminarePagamenti)

            lb_th_pagamento.Text = False

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzaDettaglioOperazionePOS) = "1" Then
                lb_th_lente.Text = False
            Else
                lb_th_lente.Text = True
            End If
        End If
    End Sub

    Protected Sub listPagamenti_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles listPagamenti.DataBound
        Dim th_lente As Control = listPagamenti.FindControl("th_lente")
        If th_lente IsNot Nothing Then
            th_lente.Visible = Boolean.Parse(lb_th_lente.Text)
        End If
        Dim th_pagamento As Control = listPagamenti.FindControl("th_pagamento")
        If th_pagamento IsNot Nothing Then
            th_pagamento.Visible = Boolean.Parse(lb_th_pagamento.Text)
        End If
    End Sub

    Protected Sub listPagamenti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listPagamenti.ItemDataBound
        Dim preaut_aperta As Label = e.Item.FindControl("preaut_aperta")
        Dim operazione_stornata As Label = e.Item.FindControl("operazione_stornata")
        Dim lblStato As Label = e.Item.FindControl("lblStato")
        Dim id_pos_funzioni_ares As Label = e.Item.FindControl("id_pos_funzioni_ares")
        Dim btnVedi As ImageButton = e.Item.FindControl("vedi")
        Dim id_tippag As Label = e.Item.FindControl("ID_TIPPAG")
        Dim lb_Des_ID_ModPag As Label = e.Item.FindControl("lb_Des_ID_ModPag")

        If id_tippag.Text = "1011098650" And lb_Des_ID_ModPag.Text = "" Then
            lb_Des_ID_ModPag.Text = "C.CREDITO"
        End If

        If (operazione_stornata.Text & "") = "True" Then
            lblStato.Text = "STORNATA"
        Else
            If id_pos_funzioni_ares.Text = enum_tipo_pagamento_ares.Richiesta _
                Or id_pos_funzioni_ares.Text = enum_tipo_pagamento_ares.Integrazione _
                Or id_pos_funzioni_ares.Text = enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Autorizzazione Then
                If (preaut_aperta.Text & "") = "True" Then
                    lblStato.Text = "APERTA"
                    ' -------------------------------------
                    ' gestione pulsanti pagamenti
                    If Boolean.Parse(lb_th_pagamento.Text) Then
                        Dim lblProvenienza As Label = e.Item.FindControl("lblProvenienza")
                        Dim bt_pag_rds As Button = e.Item.FindControl("bt_pag_rds")
                        Dim bt_pag_rds_ra As Button = e.Item.FindControl("bt_pag_rds_ra")
                        Dim bt_pag_ra As Button = e.Item.FindControl("bt_pag_ra")

                        If lblProvenienza.Text = "RDS" Then
                            bt_pag_rds.Visible = True
                        End If
                        If lblProvenienza.Text = "RA" Then
                            If Boolean.Parse(lb_PreautorizzazioneRDS.Text) Then
                                bt_pag_ra.Visible = True
                            Else
                                bt_pag_rds_ra.Visible = True
                            End If
                        End If
                    End If
                    ' -------------------------------------
                Else
                    lblStato.Text = "CHIUSA"
                End If
            End If
        End If
    End Sub

    Private Sub ValorizzaDettaglio(id_pagamento_extra As String, funzione As String, data_operazione As String, stato As String)

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT cassa, titolo, intestatario, scadenza, id_operatore_ares, note,  terminal_id, NR_PREAUT, scadenza_preaut, stazioni.codice FROM PAGAMENTI_EXTRA WITH(NOLOCK) LEFT JOIN stazioni WITH(NOLOCK) ON pagamenti_extra.id_stazione=stazioni.id WHERE ID_CTR='" & id_pagamento_extra & "'", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()
        Rs.Read()

        If Rs.HasRows() Then
            idPagamentoExtra.Text = id_pagamento_extra

            txtNote.Text = Rs("note") & ""
            txtPOS_Funzione.Text = funzione
            txtPOS_Stazione.Text = Rs("codice") & ""
            txtPOS_Cassa.Text = Rs("CASSA") & ""
            If (Rs("titolo") & "") <> "" Then
                txtPOS_Carta.Text = "XXXX XXXX XXXX XXXX"
                btnVisualizzaCC.Visible = True
                lblPasswordCC.Visible = True
                txtPasswordCC.Visible = True
            Else
                btnVisualizzaCC.Visible = False
                lblPasswordCC.Visible = False
                txtPasswordCC.Visible = False
                txtPOS_Carta.Text = ""
            End If
            txtPOS_Intestatario.Text = Rs("intestatario") & ""
            txtPOS_Scadenza.Text = Rs("scadenza") & ""

            txtPOS_Operatore.Text = funzioni_comuni.getNomeOperatore(Rs("id_operatore_ares") & "")
            txtPOS_DataOperazione.Text = data_operazione
            txtPOS_TerminalID.Text = Rs("TERMINAL_ID") & ""

            txtPOS_NrPreaut.Text = Rs("NR_PREAUT") & ""
            txtPOS_ScadenzaPreaut.Text = Rs("scadenza_preaut") & ""

            txtPOS_Stato.Text = stato

            txtPOS_TotIncassato.Focus()
        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub listPagamenti_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listPagamenti.ItemCommand
        Trace.Write("listPagamenti_ItemCommand: ****************************************** " & e.CommandName)

        If e.CommandName = "vedi" Then
            Dim id_pagamento_extra As Label = e.Item.FindControl("ID_CTRLabel")
            Dim funzione As Label = e.Item.FindControl("funzione")
            Dim DATA_OPERAZIONELabel As Label = e.Item.FindControl("DATA_OPERAZIONELabel")
            Dim lblStato As Label = e.Item.FindControl("lblStato")

            ValorizzaDettaglio(id_pagamento_extra.Text, funzione.Text, DATA_OPERAZIONELabel.Text, lblStato.Text)

            riga_pagamento_pos.Visible = True

            If livello_accesso_eliminare_pagamenti.Text = 3 Then
                btnEliminaPagamento.Visible = True
                btnModificaDataPagamento.Visible = True
                txtPOS_DataOperazione.ReadOnly = False
            Else
                btnEliminaPagamento.Visible = False
                btnModificaDataPagamento.Visible = False
                txtPOS_DataOperazione.ReadOnly = True
            End If

        ElseIf e.CommandName = "bt_pag_rds" Then
            Dim id_pagamento_extra As Label = e.Item.FindControl("ID_CTRLabel")
            Dim ex As EventoNuovoRecord = New EventoNuovoRecord
            ex.Valore = Integer.Parse(id_pagamento_extra.Text)

            RaiseEvent Pagamento_RDS(Me, ex)

        ElseIf e.CommandName = "bt_pag_ra" Then
            Dim id_pagamento_extra As Label = e.Item.FindControl("ID_CTRLabel")
            Dim ex As EventoNuovoRecord = New EventoNuovoRecord
            ex.Valore = Integer.Parse(id_pagamento_extra.Text)

            RaiseEvent Pagamento_RA(Me, ex)

        ElseIf e.CommandName = "bt_pag_rds_ra" Then
            Dim id_pagamento_extra As Label = e.Item.FindControl("ID_CTRLabel")
            Dim ex As EventoNuovoRecord = New EventoNuovoRecord
            ex.Valore = Integer.Parse(id_pagamento_extra.Text)

            RaiseEvent Pagamento_RDS_RA(Me, ex)

        End If
    End Sub

    Protected Sub btn_chiudi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_chiudi.Click
        txtPOS_Carta.Text = ""
        idPagamentoExtra.Text = ""
        riga_pagamento_pos.Visible = False
    End Sub

    Protected Sub ImpostaTotaliPagamento(ByVal num_pren As String, ByVal num_contratto As String, ByVal num_rds As String, ByVal num_multa As String)
        Dim sqlStr As String = ""

        Using Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            sqlStr = "SELECT ISNULL(SUM(ISNULL(PER_IMPORTO,0)),0)" &
                " FROM PAGAMENTI_EXTRA WITH(NOLOCK)" &
                " WHERE (N_PREN_RIF='" & num_pren & "' OR N_CONTRATTO_RIF='" & num_contratto & "' OR N_RDS_RIF='" & num_rds & "' OR N_MULTA_RIF='" & num_multa & "')" &
                " AND ((id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Richiesta & "' AND preaut_aperta='1' AND operazione_stornata='0')" &
                " OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Integrazione & "' AND preaut_aperta='1' AND operazione_stornata='0')" &
                " OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Autorizzazione & "' AND preaut_aperta='1' AND operazione_stornata='0'))"
            Trace.Write(sqlStr)

            Using Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                txtPOS_TotPreaut.Text = FormatNumber(Cmd.ExecuteScalar, 2, , , TriState.False)
            End Using

            sqlStr = "SELECT ISNULL(SUM(ISNULL(PER_IMPORTO,0)),0)" &
                " FROM PAGAMENTI_EXTRA WITH(NOLOCK)" &
                " WHERE (N_PREN_RIF='" & num_pren & "' OR N_CONTRATTO_RIF='" & num_contratto & "' OR N_RDS_RIF='" & num_rds & "' OR N_MULTA_RIF='" & num_multa & "')" &
                " AND operazione_stornata='0'" &
                " AND id_pos_funzioni_ares IN ('" & enum_tipo_pagamento_ares.Vendita & "'," &
                " '" & enum_tipo_pagamento_ares.Chiusura & "'," &
                " '" & enum_tipo_pagamento_ares.MCR_Pagamento_Cash & "'," &
                " '" & enum_tipo_pagamento_ares.Rimborso_su_RA & "'," &
                " '" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso & "'," &
                " '" & enum_tipo_pagamento_ares.Deposito_su_RA & "'," &
                " '" & enum_tipo_pagamento_ares.Rimborso_su_RA & "'," &
                " '" & enum_tipo_pagamento_ares.Abbuono_Attivo & "'," &
                " '" & enum_tipo_pagamento_ares.Pagamento_Contanti & "'," &
                " '" & enum_tipo_pagamento_ares.Abbuono_Passivo & "')"
            Trace.Write(sqlStr)

            Using Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                txtPOS_TotIncassato.Text = FormatNumber(Cmd.ExecuteScalar, 2, , , TriState.False)
            End Using
        End Using
    End Sub


    Public Function VerificaPreautorizzazioneRDS(ByVal num_rds As String) As Boolean
        Dim num_record As Integer = 0
        Dim sqlStr As String
        sqlStr = "SELECT COUNT(*)" &
                " FROM PAGAMENTI_EXTRA WITH(NOLOCK)" &
                " WHERE (N_RDS_RIF='" & num_rds & "')" &
                " AND ((id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Richiesta & "' AND preaut_aperta='1' AND operazione_stornata='0')" &
                " OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Integrazione & "' AND preaut_aperta='1' AND operazione_stornata='0')" &
                " OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Deposito_su_RA & "' AND preaut_aperta='1' AND operazione_stornata='0')" &
                " OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Autorizzazione & "' AND preaut_aperta='1' AND operazione_stornata='0'))"
        Trace.Write(sqlStr)
        Using Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()

                If Cmd.ExecuteScalar IsNot DBNull.Value Then
                    num_record = Cmd.ExecuteScalar
                End If

            End Using
        End Using

        Return (num_record > 0)
    End Function

    Protected Function CheckPasswordCC() As Boolean
        Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        conn.Open()

        Dim strQuery As String
        strQuery = "SELECT id FROM operatori WHERE id=" & Request.Cookies("SicilyRentCar")("IdUtente") & " AND password='" & Trim(txtPasswordCC.Text) & "'"

        Dim cmd As New Data.SqlClient.SqlCommand(strQuery, conn)

        Dim test As String = cmd.ExecuteScalar & ""

        If test <> "" Then
            strQuery = "INSERT INTO log_cc (id_utente, id_pagamenti_extra, data, status) VALUES ('" & Request.Cookies("SicilyRentCar")("IdUtente") & "'," & idPagamentoExtra.Text & ", GetDate(),'OK')"
            cmd = New Data.SqlClient.SqlCommand(strQuery, conn)
            cmd.ExecuteNonQuery()

            CheckPasswordCC = True

            txtPasswordCC.Text = ""
        Else
            strQuery = "INSERT INTO log_cc (id_utente, id_pagamenti_extra, data, status) VALUES ('" & Request.Cookies("SicilyRentCar")("IdUtente") & "'," & idPagamentoExtra.Text & ", GetDate(),'PWD ERRATA')"
            cmd = New Data.SqlClient.SqlCommand(strQuery, conn)
            cmd.ExecuteNonQuery()

            CheckPasswordCC = False

            txtPasswordCC.Text = ""
        End If


        cmd.Dispose()
        cmd = Nothing
        conn.Close()
        conn.Dispose()
        conn = Nothing
    End Function

    Protected Sub btnVisualizzaCC_Click(sender As Object, e As System.EventArgs) Handles btnVisualizzaCC.Click
        txtPOS_Carta.Focus()

        If Trim(txtPasswordCC.Text) = "" Then
            Libreria.genUserMsgBox(Page, "Specificare la password del proprio account per poter visualizzare il numero della carta di credito.")
        ElseIf Not CheckPasswordCC() Then
            Libreria.genUserMsgBox(Page, "Attenzione: password errata. Il tentativo di visualizzare il numero della carta di credito è stato registrato.")
        Else
            Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            conn.Open()

            Dim strQuery As String
            strQuery = "SELECT titolo FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE ID_CTR='" & idPagamentoExtra.Text & "'"

            Dim cmd As New Data.SqlClient.SqlCommand(strQuery, conn)

            Dim test As String = cmd.ExecuteScalar & ""

            With New security
                txtPOS_Carta.Text = .decryptString(test)
            End With


            cmd.Dispose()
            cmd = Nothing
            conn.Close()
            conn.Dispose()
            conn = Nothing

            btnVisualizzaCC.Visible = False
            lblPasswordCC.Visible = False
            txtPasswordCC.Visible = False
        End If
    End Sub

    Protected Sub btnEliminaPagamento_Click(sender As Object, e As System.EventArgs) Handles btnEliminaPagamento.Click
        Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        conn.Open()

        Dim strQuery As String
        strQuery = "DELETE FROM PAGAMENTI_EXTRA WHERE ID_CTR=" & idPagamentoExtra.Text

        Dim cmd As New Data.SqlClient.SqlCommand(strQuery, conn)

        Try
            cmd.ExecuteNonQuery()

            listPagamenti.DataBind()
            ImpostaTotaliPagamento(lb_num_pren.Text, lb_num_contratto.Text, lb_num_rds.Text, lb_num_multa.Text)

            txtPOS_Carta.Text = ""
            idPagamentoExtra.Text = ""
            riga_pagamento_pos.Visible = False

            Libreria.genUserMsgBox(Page, "Pagamento eliminato correttamente.")
        Catch ex As Exception
            Libreria.genUserMsgBox(Page, "Impossibile eliminare questo pagamento")
        End Try


        cmd.Dispose()
        cmd = Nothing
        conn.Close()
        conn.Dispose()
        conn = Nothing
    End Sub

    Protected Sub btnModificaDataPagamento_Click(sender As Object, e As System.EventArgs) Handles btnModificaDataPagamento.Click
        If Trim(txtPOS_DataOperazione.Text) = "" Then
            Libreria.genUserMsgBox(Page, "Specificare la data dell'operazione")
        Else
            Try
                Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                conn.Open()

                Dim data_pagamento As String = funzioni_comuni.getDataDb_con_orario(txtPOS_DataOperazione.Text.Replace(".", ":"))
                Dim data_pagamento_no_ora As String = funzioni_comuni.getDataDb_senza_orario(txtPOS_DataOperazione.Text.Replace(".", ":"))

                Dim strQuery As String
                strQuery = "UPDATE PAGAMENTI_EXTRA SET DATA='" & data_pagamento_no_ora & "', DATA_OPERAZIONE='" & data_pagamento & "' WHERE  ID_CTR=" & idPagamentoExtra.Text

                Dim cmd As New Data.SqlClient.SqlCommand(strQuery, conn)


                cmd.ExecuteNonQuery()

                Libreria.genUserMsgBox(Page, "Pagamento modificato correttamente.")

                cmd.Dispose()
                cmd = Nothing
                conn.Close()
                conn.Dispose()
                conn = Nothing

                listPagamenti.DataBind()
                txtPOS_DataOperazione.Focus()
            Catch ex As Exception
                Libreria.genUserMsgBox(Page, "Si è verificato un errore: controllare la data e l'ora specificati.")
            End Try



        End If

    End Sub
End Class
