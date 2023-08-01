
Partial Class gestione_gps
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            dropStazione.DataBind()
            dropProprietario.DataBind()
            dropOldStazione.DataBind()

            livello_permesso.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneGps)
            livello_permesso_dismissione.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneGpsDismissione)

            If livello_permesso.Text = "1" Then
                Response.Redirect("default.aspx")
            ElseIf livello_permesso.Text = "2" Then
                btnNuovo.Visible = False
            End If
        Else
            sqlGPS.SelectCommand = lblQuery.Text
            sqlGPS.DataBind()
        End If
    End Sub

    Protected Function condizioneWhere() As String
        Dim condizione As String = ""

        If txtCercaCodice.Text <> "" Then
            condizione += "AND gps.codice='" & txtCercaCodice.Text.Replace("'", "''") & "' "
        End If
        If txtCercaSeriale.Text <> "" Then
            condizione += "AND gps.seriale LIKE '" & txtCercaSeriale.Text.Replace("'", "''") & "%' "
        End If
        If dropCercaStazione.SelectedValue <> "0" Then
            condizione += "AND gps.id_stazione_attuale='" & dropCercaStazione.SelectedValue & "' "
        End If
        If dropCercaStato.SelectedValue <> "0" Then
            If dropCercaStato.SelectedValue = "1" Then 'se in parco visualizza anche quelli in nolo email aggiunto 01.07.2022 salvo
                condizione += "AND (gps.id_gps_status='" & dropCercaStato.SelectedValue & "' OR gps.id_gps_status='2')"
            Else
                condizione += "AND gps.id_gps_status='" & dropCercaStato.SelectedValue & "'"
            End If

        End If
        If dropCercaProprietario.SelectedValue <> "0" Then
            condizione += "AND gps.id_proprietario='" & dropCercaProprietario.SelectedValue & "'"
        End If


        If txtCercaDataIngressoDa.Text <> "" And txtCercaDataIngressoA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaDataIngressoDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaDataIngressoDa.Text)
            'condizione = condizione & " AND gps.data_ingresso_in_parco>='" & data1 & "'"
            condizione += " AND gps.data_ingresso_in_parco>=Convert(DateTime, '" & data1 & "', 102)'"
        End If

        If txtCercaDataIngressoDa.Text = "" And txtCercaDataIngressoA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaDataIngressoA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaDataIngressoA.Text & " 23:59:59")

            'condizione = condizione & " AND gps.data_ingresso_in_parco<='" & data2 & "'"
            condizione += " AND gps.data_ingresso_in_parco<=Convert(DateTime, '" & data2 & "', 102)'"
        End If

        If txtCercaDataIngressoDa.Text <> "" And txtCercaDataIngressoA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaDataIngressoDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaDataIngressoDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaDataIngressoA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaDataIngressoA.Text & " 23:59:59")

            'condizione = condizione & " AND gps.data_ingresso_in_parco BETWEEN '" & data1 & "' AND '" & data2 & "'"
            condizione += " AND gps.data_ingresso_in_parco BETWEEN Convert(DateTime, '" & data1 & "', 102) AND CONVERT(DATETIME, '" & data2 & "', 102) "

        End If

        If txtCercaDataUscitaDa.Text <> "" And txtCercaDataUscitaA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaDataUscitaDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaDataUscitaDa.Text)
            'condizione = condizione & " AND gps.data_uscita_parco>='" & data1 & "'"
            condizione += " AND gps.data_uscita_parco>=Convert(DateTime, '" & data1 & "', 102)'"
        End If

        If txtCercaDataUscitaDa.Text = "" And txtCercaDataUscitaA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaDataUscitaA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaDataUscitaA.Text & " 23:59:59")
            condizione += " AND gps.data_uscita_parco<=Convert(DateTime, '" & data2 & "', 102)'"
            'condizione = condizione & " AND gps.data_uscita_parco<='" & data2 & "'"
        End If

        If txtCercaDataUscitaDa.Text <> "" And txtCercaDataUscitaA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaDataUscitaDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaDataUscitaDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaDataUscitaA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaDataUscitaA.Text & " 23:59:59")

            'condizione = condizione & " AND gps.data_uscita_parco BETWEEN '" & data1 & "' AND '" & data2 & "'"
            condizione += " AND gps.data_uscita_parco BETWEEN Convert(DateTime, '" & data1 & "', 102) AND CONVERT(DATETIME, '" & data2 & "', 102) "

        End If

        condizioneWhere = condizione
    End Function

    Protected Sub cerca()
        Try
            risultati.Visible = True
            Dim sql As String = "SELECT gps.id, gps.codice, gps.seriale, gps.id_stazione_attuale,  (stazioni.codice + ' ' + stazioni.nome_stazione) As stazione,"
            sql += "gps.id_proprietario, proprietari_veicoli.descrizione As proprietario, gps.id_gps_status, gps_status.descrizione As gps_status, "
            sql += "CONVERT(Char(10), gps.data_ingresso_in_parco, 103) As data_ingresso_in_parco,CONVERT(Char(10), gps.data_uscita_parco, 103) As data_uscita_parco "
            sql += "FROM gps WITH(NOLOCK) LEFT JOIN stazioni ON gps.id_stazione_attuale=stazioni.id "
            sql += "INNER JOIN proprietari_veicoli WITH(NOLOCK) ON gps.id_proprietario=proprietari_veicoli.id "
            sql += "INNER JOIN gps_status WITH(NOLOCK) ON gps.id_gps_status=gps_status.id WHERE gps.id>0 " & condizioneWhere()
            sqlGPS.SelectCommand = sql
            lblQuery.Text = sqlGPS.SelectCommand
            sqlGPS.SelectCommand = lblQuery.Text & " ORDER BY codice"
        Catch ex As Exception
            Response.Write("error_cerca:" & ex.Message & "<br/>")
        End Try

    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        cerca()
    End Sub

    Protected Sub btnNuovo_Click(sender As Object, e As System.EventArgs) Handles btnNuovo.Click
        Try
            stato_nuovo()
            risultati.Visible = False
            cerca_gps.Visible = False
            nuovo_gps.Visible = True
            pulisci_campi()
        Catch ex As Exception
            Response.Write("error_btnNuovo_click:" & ex.Message & "<br/>")
        End Try



    End Sub

    Protected Sub pulisci_campi()
        id_modifica.Text = ""
        txtCodice.Text = ""
        txtSeriale.Text = ""
        dropStazione.SelectedValue = "0"
        dropProprietario.SelectedIndex = "0"
        txtDataImmissione.Text = ""
        txtDataUscita.Text = ""
        dropOldStazione.SelectedValue = "0"
    End Sub

    Protected Sub stato_modifica()
        btnSalva.Text = "Modifica GPS"

        dropStazione.Enabled = True
    End Sub

    Protected Sub stato_nuovo()
        btnSalva.Text = "Salva GPS"

        id_modifica.Text = ""
        dropStazione.Enabled = True
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Dim check_errori As String = checkErrori()

        If check_errori = "" Then
            If check_codice_e_seriale(id_modifica.Text) Then
                If id_modifica.Text = "" Then
                    salva_nuovo_gps()
                    stato_modifica()
                    Libreria.genUserMsgBox(Me, "GPS salvato correttamente.")
                Else
                    modifica_gps()
                    Libreria.genUserMsgBox(Me, "GPS modificato correttamente.")
                End If
            Else
                Libreria.genUserMsgBox(Me, "ATTENZIONE: codice e/o seriale già registrati su sistema. Impossibile salvare il GPS.")
            End If
        Else
            Libreria.genUserMsgBox(Me, check_errori)
        End If

    End Sub

    Protected Sub modifica_gps()

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String = "UPDATE gps SET codice='" & txtCodice.Text.Replace("'", "''") & "', seriale='" & txtSeriale.Text.Replace("'", "''") & "', "
            sqlStr += "id_proprietario='" & dropProprietario.SelectedValue & "', id_stazione_attuale = " & dropStazione.SelectedValue & " WHERE id='" & id_modifica.Text & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            If dropStazione.SelectedValue <> dropOldStazione.SelectedValue Then
                'REGISTRAZIONE CAMBIO STAZIONE
                sqlStr = "INSERT INTO movimenti_targa (id_gps, id_tipo_movimento, data_rientro, id_stazione_uscita, id_stazione_rientro, id_operatore, "
                sqlStr += "data_registrazione, movimento_attivo) VALUES ('" & id_modifica.Text & "','" & Costanti.tipologia_movimenti.movimento_interno & "', "
                sqlStr += "convert(datetime,getDate(),102),'" & dropOldStazione.SelectedValue & "','" & dropStazione.SelectedValue & "','" & Request.Cookies("SicilyRentCar")("idUtente") & "',convert(datetime,getDate(),102),'0')"

                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteNonQuery()

                dropOldStazione.SelectedValue = dropStazione.SelectedValue
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error_modifica_gps:" & ex.Message & "<br/>")
        End Try

    End Sub

    Protected Function gpsTrasferibile() As Boolean

        Try
            Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            conn.Open()
            Dim strQuery As String
            strQuery = "SELECT id FROM gps WITH(NOLOCK) WHERE id= " & id_modifica.Text
            strQuery += " AND id_gps_status='" & Costanti.stato_gps.in_parco & "' AND id_stazione_attuale='" & dropOldStazione.SelectedValue & "' ORDER BY codice"

            'Response.Write(strQuery)
            'Response.End()

            Dim cmd As New Data.SqlClient.SqlCommand(strQuery, conn)

            Dim test As String = cmd.ExecuteScalar & ""

            If test <> "" Then
                gpsTrasferibile = True
            Else
                gpsTrasferibile = False
            End If

            cmd.Dispose()
            cmd = Nothing
            conn.Close()
            conn.Dispose()
            conn = Nothing
        Catch ex As Exception
            Response.Write("error_gpstrasferibile:" & ex.Message & "<br/>")
        End Try

    End Function

    Protected Sub salva_nuovo_gps()
        'SALVATAGGIO NUOVO GPS - IMMISSIONE IN PARCO 
        Dim sql1 As String, sql2 As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String = "INSERT INTO gps (codice, seriale, id_proprietario, id_stazione_attuale, id_gps_status, data_ingresso_in_parco, id_operatore_ingresso_in_parco) VALUES "
            sqlStr += "('" & txtCodice.Text.Replace("'", "''") & "','" & txtSeriale.Text.Replace("'", "''") & "','" & dropProprietario.SelectedValue & "', "
            sqlStr += "'" & dropStazione.SelectedValue & "','" & Costanti.stato_gps.in_parco & "',CONVERT(DATETIME,getDate(),102),'" & Request.Cookies("SicilyRentCar")("idUtente") & "')"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            sql1 = sqlStr
            Cmd.ExecuteNonQuery()
            sql1 = ""


            sqlStr = "SELECT @@IDENTITY FROM gps WITH(NOLOCK)"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            sql2 = sqlStr
            Dim id_gps As String = Cmd.ExecuteScalar
            id_modifica.Text = id_gps

            sqlStr = "INSERT INTO movimenti_targa (id_gps, id_tipo_movimento, data_rientro, id_stazione_rientro, id_operatore, "
            sqlStr += "data_uscita, data_registrazione, movimento_attivo) VALUES ('" & id_gps & "','" & Costanti.tipologia_movimenti.immissione_in_parco & "', "
            sqlStr += "CONVERT(DATETIME,getDate(),102),'" & dropStazione.SelectedValue & "','" & Request.Cookies("SicilyRentCar")("idUtente") & "',convert(datetime,getDate(),102),convert(datetime,getDate(),102),'0')"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            txtDataImmissione.Text = Format(Now(), "dd/MM/yyyy")

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            'Response.Write("error_salva_nuovo_gps:" & ex.Message & "<br/>sql1: " & sql1 & "<br/>" & "sql2: " & sql2 & "<br/>")
        End Try




    End Sub

    Protected Function check_codice_e_seriale(ByVal id_modifica As String) As Boolean
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String = "SELECT id FROM gps WITH(NOLOCK) WHERE (codice='" & txtCodice.Text.Replace("'", "''") & "' OR seriale='" & txtSeriale.Text.Replace("'", "''") & "') "
            sqlStr += "AND id<>'" & id_modifica & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test = "" Then
                check_codice_e_seriale = True
            Else
                check_codice_e_seriale = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_check_codice e seriale:" & ex.Message & "<br/>")
        End Try

    End Function

    Protected Function checkErrori() As String
        Dim messaggio As String = ""

        If Trim(txtCodice.Text) = "" Then
            messaggio = messaggio & "Specificare il codice interno del GPS." & vbCrLf
        End If

        If Trim(txtSeriale.Text) = "" Then
            messaggio = messaggio & "Specificare il numero seriale del GPS." & vbCrLf
        End If

        If dropStazione.SelectedValue = "0" Then
            messaggio = messaggio & "Specificare la stazione di appartenenza del GPS." & vbCrLf
        End If

        If dropProprietario.SelectedValue = "0" Then
            messaggio = messaggio & "Specificare il proprietario del GPS." & vbCrLf
        End If

        If id_modifica.Text <> "" And dropOldStazione.SelectedValue <> dropStazione.SelectedValue AndAlso Not gpsTrasferibile() Then
            messaggio = messaggio & "ATTENZIONE: IMPOSSIBILE TRASFERIRE IL GPS IN QUANTO NON E' ATTUALMENTE DISPONIBILE IN STAZIONE." & vbCrLf

            dropStazione.SelectedValue = dropOldStazione.SelectedValue
        End If

        checkErrori = messaggio
    End Function

    Protected Sub listGPS_ItemCommand(ByVal sender As Object, e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listGPS.ItemCommand
        If e.CommandName = "modifica" Then
            Dim id As Label = e.Item.FindControl("id")
            Dim codice As Label = e.Item.FindControl("codice")
            Dim seriale As Label = e.Item.FindControl("seriale")
            Dim id_stazione_attuale As Label = e.Item.FindControl("id_stazione_attuale")
            Dim id_proprietario As Label = e.Item.FindControl("id_proprietario")
            Dim data_ingresso_in_parco As Label = e.Item.FindControl("data_ingresso_in_parco")
            Dim data_uscita_parco As Label = e.Item.FindControl("data_uscita_parco")

            id_modifica.Text = id.Text
            txtCodice.Text = codice.Text
            txtSeriale.Text = seriale.Text
            dropStazione.SelectedValue = id_stazione_attuale.Text
            dropOldStazione.SelectedValue = id_stazione_attuale.Text

            dropProprietario.SelectedValue = CInt(id_proprietario.Text)
            txtDataImmissione.Text = data_ingresso_in_parco.Text
            txtDataUscita.Text = data_uscita_parco.Text

            risultati.Visible = False
            cerca_gps.Visible = False

            nuovo_gps.Visible = True

            If livello_permesso.Text = "3" Then
                btnSalva.Visible = True
            Else
                btnSalva.Visible = False
            End If

            If livello_permesso_dismissione.Text = "3" Then
                btnDismissGps.Visible = True
            Else
                btnDismissGps.Visible = False
            End If

            stato_modifica()
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        risultati.Visible = True
        cerca_gps.Visible = True

        nuovo_gps.Visible = False

        pulisci_campi()

        cerca()
    End Sub


    Protected Function getStatoGps() As String
        Try
            Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            conn.Open()
            Dim strQuery As String
            strQuery = "SELECT id_gps_status FROM gps WITH(NOLOCK) WHERE id=" & id_modifica.Text

            Dim cmd As New Data.SqlClient.SqlCommand(strQuery, conn)

            getStatoGps = cmd.ExecuteScalar & ""

            cmd.Dispose()
            cmd = Nothing
            conn.Close()
            conn.Dispose()
            conn = Nothing
        Catch ex As Exception
            Response.Write("error_getStatoGps:" & ex.Message & "<br/>")
        End Try


    End Function

    Protected Sub btnDismissGps_Click(sender As Object, e As System.EventArgs) Handles btnDismissGps.Click
        Dim sqlStr As String
        Try
            Dim stato_gps As String = getStatoGps()

            If stato_gps = Costanti.stato_gps.in_nolo Then
                Libreria.genUserMsgBox(Me, "Attenzione: il GPS è attualmente noleggiato. Impossibile proseguire con la dismissione.")
            ElseIf stato_gps = Costanti.stato_gps.dismesso Then
                Libreria.genUserMsgBox(Me, "Attenzione: il GPS è già stato dismesso.")
            ElseIf stato_gps = Costanti.stato_gps.in_parco Then
                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                sqlStr = "UPDATE gps SET id_gps_status=" & Costanti.stato_gps.dismesso & ", data_uscita_parco=CONVERT(DATE,getDate(),102), id_operatore_uscita_parco=" & Request.Cookies("SicilyRentCar")("idUtente") & " "
                sqlStr += " WHERE id=" & id_modifica.Text
                Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteNonQuery()

                sqlStr = "INSERT INTO movimenti_targa (id_gps, id_tipo_movimento, data_uscita, id_stazione_uscita, id_operatore, "
                sqlStr += "data_registrazione, movimento_attivo) VALUES ('" & id_modifica.Text & "','" & Costanti.tipologia_movimenti.dismissione & "', "
                sqlStr += "CONVERT(DATE,getDate(),102),'" & dropStazione.SelectedValue & "','" & Request.Cookies("SicilyRentCar")("idUtente") & "',CONVERT(DATE,getDate(),102),'0')"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteNonQuery()

                txtDataUscita.Text = Format(Now(), "dd/MM/yyyy")
                btnDismissGps.Visible = False
                btnSalva.Visible = False

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                Libreria.genUserMsgBox(Me, "Dismissione effettuata correttamente.")
            Else
                Libreria.genUserMsgBox(Me, "Attenzione: si è verificato un errore, rivolgersi ad un amministratore.")
            End If
        Catch ex As Exception
            Response.Write("error_btnDismissGps_Click:" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try





    End Sub
End Class
