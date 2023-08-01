
Partial Class movimenti_veicoli
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.MovimentiVeicoli) = "1" Then
                Response.Redirect("default.aspx")
            End If

            tipoMovimenti.DataBind()
            For i = 0 To tipoMovimenti.Items.Count - 1
                tipoMovimenti.Items(i).Selected = True
            Next
        Else
            sqlMovimentiTarga.SelectCommand = lblQuery.Text & " ORDER BY movimenti_targa.id DESC"
            sqlMovimentiTarga.DataBind()
        End If
    End Sub

    Protected Sub dropCercaMarca_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropCercaMarca.SelectedIndexChanged
        dropCercaModello.Items.Clear()
        dropCercaModello.Items.Add("Seleziona...")
        dropCercaModello.Items(0).Value = 0
        dropCercaModello.DataBind()
    End Sub

    Protected Function getIdVeicolo(ByVal targa As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(id,-1) FROM veicoli WHERE targa='" & Replace(targa, "'", "''") & "'", Dbc)
        Dbc.Open()

        getIdVeicolo = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getIdGps(ByVal gps As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(id,-1) FROM gps WHERE codice='" & Replace(gps, "'", "''") & "'", Dbc)
        Dbc.Open()

        getIdGps = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function condizioneWhere() As String
        Dim query As String = ""

        If cercaStazioneUscita.SelectedValue <> "0" Then
            query = query & " AND movimenti_targa.id_stazione_uscita='" & cercaStazioneUscita.SelectedValue & "'"
        End If

        If cercaStazioneRientro.SelectedValue <> "0" Then
            query = query & " AND movimenti_targa.id_stazione_rientro='" & cercaStazioneRientro.SelectedValue & "'"
        End If

        If cercaStazionePresuntoRientro.SelectedValue <> "0" Then
            query = query & " AND movimenti_targa.id_stazione_presunto_rientro='" & cercaStazionePresuntoRientro.SelectedValue & "'"
        End If

        If txtCercaUscitaDa.Text <> "" And txtCercaUscitaA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaUscitaDa.Text, 0) ' funzioni_comuni.getDataDb_senza_orario(txtCercaUscitaDa.Text)
            query += "  AND movimenti_targa.data_uscita>=CONVERT(DATETIME,'" & data1 & "',102)"
            'query = query & "  And movimenti_targa.data_uscita>='" & data1 & "'"
        End If

        If txtCercaUscitaDa.Text = "" And txtCercaUscitaA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaUscitaA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaUscitaA.Text & " 23:59:59")
            query += "  AND movimenti_targa.data_uscita<=CONVERT(DATETIME,'" & data2 & "',102)"
            'query = query & " AND movimenti_targa.data_uscita<='" & data2 & "'"
        End If

        If txtCercaUscitaDa.Text <> "" And txtCercaUscitaA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaUscitaDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaUscitaDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaUscitaA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaUscitaA.Text & " 23:59:59")
            query += " AND movimenti_targa.data_uscita BETWEEN CONVERT(DATETIME,'" & data1 & "',102) AND CONVERT(DATETIME,'" & data2 & "',102)"
            'query = query & " AND movimenti_targa.data_uscita BETWEEN '" & data1 & "' AND '" & data2 & "'"
        End If

        If txtCercaRientroPrDa.Text <> "" And txtCercaRientroPrA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaRientroPrDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaRientroPrDa.Text)
            'query = query & "  AND movimenti_targa.data_presunto_rientro>='" & data1 & "'"
            query += "  AND movimenti_targa.data_presunto_rientro>=CONVERT(DATETIME,'" & data1 & "',102)"
        End If

        If txtCercaRientroPrDa.Text = "" And txtCercaRientroPrA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaRientroPrA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaRientroPrA.Text & " 23:59:59")
            'query = query & " AND movimenti_targa.data_presunto_rientro<='" & data2 & "'"
            query += " AND movimenti_targa.data_presunto_rientro<=CONVERT(DATETIME,'" & data2 & "',102)"
        End If

        If txtCercaRientroPrDa.Text <> "" And txtCercaRientroPrA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaRientroPrDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaRientroPrDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaRientroPrA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaRientroPrA.Text & " 23:59:59")
            'query = query & " AND movimenti_targa.data_presunto_rientro BETWEEN '" & data1 & "' AND '" & data2 & "'"
            query += " AND movimenti_targa.data_presunto_rientro BETWEEN CONVERT(DATETIME,'" & data1 & "',102) AND CONVERT(DATETIME,'" & data2 & "',102)"
        End If

        If dropAutoRientrata.SelectedValue = "Si" Then
            query = query & " AND movimenti_targa.movimento_attivo='0'"
        ElseIf dropAutoRientrata.SelectedValue = "No" Then
            query = query & " AND movimenti_targa.movimento_attivo='1'"
        End If

        If txtCercaRientroDa.Text <> "" And txtCercaRientroA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaRientroDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaRientroDa.Text)
            'query = query & " AND movimenti_targa.data_rientro>='" & data1 & "'"
            query += " AND movimenti_targa.data_rientro>=CONVERT(DATETIME,'" & data1 & "',102)"
        End If

        If txtCercaRientroDa.Text = "" And txtCercaRientroA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaRientroA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaRientroA.Text & " 23:59:59")
            'query = query & " AND movimenti_targa.data_rientro<='" & data2 & "'"
            query = query & " AND movimenti_targa.data_rientro<=CONVERT(DATETIME,'" & data2 & "',102)"
        End If

        If txtCercaRientroDa.Text <> "" And txtCercaRientroA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaRientroDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaRientroDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaRientroA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaRientroA.Text & " 23:59:59")
            'query = query & " AND movimenti_targa.data_rientro BETWEEN '" & data1 & "' AND '" & data2 & "'"
            query = query & " AND movimenti_targa.data_rientro BETWEEN CONVERT(DATETIME,'" & data1 & "',102) AND CONVERT(DATETIME,'" & data2 & "',102)"
        End If

        If dropCercaMarca.SelectedValue <> "0" Then
            query = query & " AND modelli.id_casaAutomobilistica='" & dropCercaMarca.SelectedValue & "'"
        End If

        If dropCercaModello.SelectedValue <> "0" Then
            query = query & " AND modelli.id_modello='" & dropCercaModello.SelectedValue & "'"
        End If

        query = query & " AND (movimenti_targa.id_tipo_movimento=0 "
        For i = 0 To tipoMovimenti.Items.Count - 1
            If tipoMovimenti.Items(i).Selected Then
                query = query & " OR movimenti_targa.id_tipo_movimento='" & tipoMovimenti.Items(i).Value & "'"
            End If
        Next
        query = query & " OR movimenti_targa.id_tipo_movimento='13'"
        query = query & ")"

        If txtNumeroRifemento.Text <> "" Then
            query = query & " AND movimenti_targa.num_riferimento='" & Replace(txtNumeroRifemento.Text, "'", "''") & "'"
        End If

        If dropCercaVeicoloGps.SelectedValue = "0" Then
            'VEICOLO
            If txtTarga.Text <> "" Then
                query = query & " AND movimenti_targa.id_veicolo='" & getIdVeicolo(txtTarga.Text) & "'"
            End If
        ElseIf dropCercaVeicoloGps.SelectedValue = "1" Then
            'GPS
            If txtTarga.Text <> "" Then
                query = query & " AND movimenti_targa.id_gps='" & getIdGps(txtTarga.Text) & "'"
            End If
        End If


        condizioneWhere = query
    End Function

    Protected Sub cerca()
        Try
            listMovimentiTarga.Visible = True

            If dropCercaVeicoloGps.SelectedValue = "0" Then
                'VEICOLO
                sqlMovimentiTarga.SelectCommand = "SELECT movimenti_targa.id, movimenti_targa.num_riferimento, veicoli.targa, '' As gps, " &
                " modelli.descrizione As modello, CONVERT(Char(10), movimenti_targa.data_uscita, 103) As data_uscita, " &
                " CONVERT(Char(10), movimenti_targa.data_rientro, 103) As data_rientro, " &
                " CONVERT(Char(10), movimenti_targa.data_presunto_rientro, 103) As data_presunto_rientro, CONVERT(Char(8), movimenti_targa.data_presunto_rientro, 108) As ora_presunto_rientro, " &
                " CONVERT(Char(8), movimenti_targa.data_uscita, 108) As ora_uscita, tipologia_movimenti.descrzione As tipo_movimento, " &
                " CONVERT(Char(8), movimenti_targa.data_rientro, 108) As ora_rientro, " &
                " (stazioni1.codice + ' ' + stazioni1.nome_stazione) As stazione_uscita, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As stazione_rientro, (stazioni3.codice + ' ' + stazioni3.nome_stazione) As stazione_presunto_rientro, " &
                " km_uscita, km_rientro, serbatoio_uscita, serbatoio_rientro " &
                " FROM movimenti_targa WITH(NOLOCK) INNER JOIN veicoli WITH(NOLOCK) ON movimenti_targa.id_veicolo=veicoli.id " &
                " LEFT JOIN stazioni As stazioni1 WITH(NOLOCK) ON movimenti_targa.id_stazione_uscita=stazioni1.id " &
                " LEFT JOIN stazioni As stazioni2 WITH(NOLOCK) ON movimenti_targa.id_stazione_rientro=stazioni2.id " &
                " LEFT JOIN stazioni As stazioni3 WITH(NOLOCK) ON movimenti_targa.id_stazionE_presunto_rientro=stazioni3.id " &
                " INNER JOIN tipologia_movimenti WITH(NOLOCK) ON movimenti_targa.id_tipo_movimento=tipologia_movimenti.id " &
                " INNER JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello WHERE movimenti_targa.id>0 " & condizioneWhere()
            ElseIf dropCercaVeicoloGps.SelectedValue = "1" Then
                'GPS
                sqlMovimentiTarga.SelectCommand = "SELECT movimenti_targa.id, isnull(movimenti_targa.num_riferimento,'') as num_riferimento, '' As targa, gps.codice As gps, " &
               " 'GPS' As modello, CONVERT(Char(10), movimenti_targa.data_uscita, 103) As data_uscita, " &
               " CONVERT(Char(10), movimenti_targa.data_rientro, 103) As data_rientro, " &
               " CONVERT(Char(10), movimenti_targa.data_presunto_rientro, 103) As data_presunto_rientro, CONVERT(Char(8), movimenti_targa.data_presunto_rientro, 108) As ora_presunto_rientro, " &
               " CONVERT(Char(8), movimenti_targa.data_uscita, 108) As ora_uscita, tipologia_movimenti.descrzione As tipo_movimento, " &
               " CONVERT(Char(8), movimenti_targa.data_rientro, 108) As ora_rientro, " &
               " (stazioni1.codice + ' ' + stazioni1.nome_stazione) As stazione_uscita, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As stazione_rientro, (stazioni3.codice + ' ' + stazioni3.nome_stazione) As stazione_presunto_rientro, " &
               " isnull(km_uscita,0) as km_uscita , isnull(km_rientro,0) as km_rientro, isnull(serbatoio_uscita,0) as serbatoio_uscita, isnull(serbatoio_rientro,0) as serbatoio_rientro " &
               " FROM movimenti_targa WITH(NOLOCK) INNER JOIN gps WITH(NOLOCK) ON movimenti_targa.id_gps=gps.id " &
               " LEFT JOIN stazioni As stazioni1 WITH(NOLOCK) ON movimenti_targa.id_stazione_uscita=stazioni1.id " &
               " LEFT JOIN stazioni As stazioni2 WITH(NOLOCK) ON movimenti_targa.id_stazione_rientro=stazioni2.id " &
               " LEFT JOIN stazioni As stazioni3 WITH(NOLOCK) ON movimenti_targa.id_stazionE_presunto_rientro=stazioni3.id " &
               " INNER JOIN tipologia_movimenti WITH(NOLOCK) ON movimenti_targa.id_tipo_movimento=tipologia_movimenti.id " & condizioneWhere()
            End If

            lblQuery.Text = sqlMovimentiTarga.SelectCommand
            sqlMovimentiTarga.SelectCommand = lblQuery.Text & " ORDER BY movimenti_targa.id DESC"
            'Response.Write(sqlMovimentiTarga.SelectCommand)
        Catch ex As Exception
            Response.Write("errore_cerca_:" & ex.Message & "<br/>")
        End Try


    End Sub

    Protected Sub btnCercaVeicolo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCercaVeicolo.Click
        cerca()
    End Sub

    Protected Sub btnReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReport.Click
        'If dropReport.SelectedValue = "0" Then

        'ElseIf dropReport.SelectedValue = "1" Then
        '    'LISTA MOVIMENTI DIFFERENZA KM
        '    Dim passo As Integer
        '    If txtPasso.Text = "" Then
        '        passo = 0
        '    Else
        '        Try
        '            passo = CInt(txtPasso.Text)
        '        Catch ex As Exception
        '            passo = -1
        '        End Try
        '    End If

        '    If passo >= 0 Then
        '        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
        '            Session("url_print") = "/stampe/movimenti_veicoli/movimenti_differenza_km.aspx?orientamento=orizzontale&passo=" & passo & "&query=" & Server.UrlEncode(condizioneWhere()) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&header_html=/stampe/movimenti_veicoli/header_movimenti_differenza_km.aspx"
        '            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
        '                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
        '            End If
        '        End If
        '    Else
        '        Libreria.genUserMsgBox(Me, "Specificare correttamente la differenza minima chilometrica.")
        '    End If
        'ElseIf dropReport.SelectedValue = "2" Then
        '    'LISTA MOVIMENTI DIFFERENZA LITRI
        '    Dim passo As Integer
        '    If txtPasso.Text = "" Then
        '        passo = 0
        '    Else
        '        Try
        '            passo = CInt(txtPasso.Text)
        '        Catch ex As Exception
        '            passo = -1
        '        End Try
        '    End If

        '    If passo >= 0 Then
        '        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
        '            Session("url_print") = "/stampe/movimenti_veicoli/movimenti_differenza_litri.aspx?orientamento=orizzontale&passo=" & passo & "&query=" & Server.UrlEncode(condizioneWhere()) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&header_html=/stampe/movimenti_veicoli/header_movimenti_differenza_litri.aspx"
        '            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
        '                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
        '            End If
        '        End If
        '    Else
        '        Libreria.genUserMsgBox(Me, "Specificare correttamente la differenza minima della capacità del serbatoio.")
        '    End If
        'ElseIf dropReport.SelectedValue = "3" Then
        '    'SCADENZIARIO
        '    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
        '        Session("url_print") = "/stampe/movimenti_veicoli/scadenziario.aspx?orientamento=verticale&query=" & Server.UrlEncode(condizioneWhere()) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&header_html=/stampe/movimenti_veicoli/header_scadenziario.aspx?intestazione=" & Replace(cercaStazionePresuntoRientro.SelectedItem.Text & " AL " & txtCercaRientroPrDa.Text, " ", "-")
        '        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
        '            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
        '        End If
        '    End If
        'End If

        'Response.Write(lblTxtQuery.Text)
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=" & "Report" & Now & ".xls")
        Response.ContentType = "application/vnd.xls"
        Response.Charset = "UTF-8"

        Dim SqlStr As String
        'SqlStr = Replace(lblTxtQuery.Text, "select", "select top 30 ")
        SqlStr = lblQuery.Text & " ORDER BY movimenti_targa.id DESC"
        'Dim Dbc As New OleDbConnection(ConfigurationManager.ConnectionStrings("ConnectionStringL190").ConnectionString)
        'Dbc.Open()

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        'Dim Cmd As New OleDbCommand(SqlStr, Dbc)
        Dim Cmd As New Data.SqlClient.SqlCommand(SqlStr, Dbc)
        'Response.Write(Cmd.CommandText & "<br><br>")
        'Response.Write(lblTxtQuery.Text)
        'Response.End()

        'Dim Rs As Data.OleDb.OleDbDataReader
        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        With Response
            .Write("<table border=1>")
            .Write("<tr>")

            .Write("<td>")
            .Write("<b>Conducente</b>")
            .Write("</td>")           

            .Write("<td>")
            .Write("<b>Rif.</b>")
            .Write("</td>")

            .Write("<td>")
            .Write("<b>Tipo</b>")
            .Write("</td>")

            .Write("<td>")
            .Write("<b>Veicolo</b>")
            .Write("</td>")

            .Write("<td>")
            .Write("<b>Staz.Out</b>")
            .Write("</td>")

            .Write("<td>")
            .Write("<b>Data Out</b>")
            .Write("</td>")

            .Write("<td>")
            .Write("<b>Ora</b>")
            .Write("</td>")

            .Write("<td>")
            .Write("<b>Km Out</b>")
            .Write("</td>")

            .Write("<td>")
            .Write("<b>Lt Out</b>")
            .Write("</td>")

            .Write("<td>")
            .Write("<b>Staz.Pr.</b>")
            .Write("</td>")

            .Write("<td>")
            .Write("<b>Data Pr.</b>")
            .Write("</td>")

            .Write("<td>")
            .Write("<b>Ora</b>")
            .Write("</td>")

            .Write("<td>")
            .Write("<b>Staz.In</b>")
            .Write("</td>")

            .Write("<td>")
            .Write("<b>Data In</b>")
            .Write("</td>")

            .Write("<td>")
            .Write("<b>Ora</b>")
            .Write("</td>")

            .Write("<td>")
            .Write("<b>Km In</b>")
            .Write("</td>")

            .Write("<td>")
            .Write("<b>Lt In</b>")
            .Write("</td>")

            .Write("</tr>")
        End With



        With Response
            Do While Rs.Read
                .Write("<tr>")

                'Tony 23-02-2023
                If Rs("tipo_movimento") = "Noleggio" Then
                    Try
                        Dim DbcNominativo As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        DbcNominativo.Open()

                        Dim CmdNominativo As New Data.SqlClient.SqlCommand("select contratti.num_contratto,contratti.id_primo_conducente, CONDUCENTI.Nominativo from contratti,CONDUCENTI where contratti.id_primo_conducente = CONDUCENTI.ID_CONDUCENTE and  contratti.num_contratto='" & Rs("num_riferimento") & "' and attivo = 1", Dbc)
                        'Response.Write(CmdNominativo.CommandText & "<br><br>")
                        'Response.End()
                        Dim RsNominativo As Data.SqlClient.SqlDataReader
                        RsNominativo = CmdNominativo.ExecuteReader()
                        If RsNominativo.HasRows Then
                            Do While RsNominativo.Read
                                .Write("<td>")
                                .Write(RsNominativo("nominativo"))
                                .Write("</td>")
                            Loop
                        Else

                        End If

                        RsNominativo.Close()
                        DbcNominativo.Close()
                        RsNominativo = Nothing
                        DbcNominativo = Nothing

                    Catch ex As Exception
                        Libreria.genUserMsgBox(Me, ex.Message & " Errore in Nominativo --- Errore contattare amministratore del sistema.")
                    End Try
                Else
                    .Write("<td>")
                    .Write("")
                    .Write("</td>")
                End If
                'FINE Tony

                .Write("<td>")
                .Write(Rs("num_riferimento"))
                .Write("</td>")

                .Write("<td>")
                .Write(Rs("tipo_movimento"))
                .Write("</td>")

                .Write("<td>")
                .Write(Rs("targa"))
                .Write("</td>")

                .Write("<td>")
                .Write(Rs("stazione_uscita"))
                .Write("</td>")

                .Write("<td>")
                .Write(Rs("data_uscita"))
                .Write("</td>")

                .Write("<td>")
                .Write(Rs("ora_uscita"))
                .Write("</td>")

                .Write("<td>")
                .Write(Rs("km_uscita"))
                .Write("</td>")

                .Write("<td>")
                .Write(Rs("serbatoio_uscita"))
                .Write("</td>")

                .Write("<td>")
                .Write(Rs("stazione_presunto_rientro"))
                .Write("</td>")

                .Write("<td>")
                .Write(Rs("data_presunto_rientro"))
                .Write("</td>")

                .Write("<td>")
                .Write(Rs("ora_presunto_rientro"))
                .Write("</td>")

                .Write("<td>")
                .Write(Rs("stazione_rientro"))
                .Write("</td>")

                .Write("<td>")
                .Write(Rs("data_rientro"))
                .Write("</td>")

                .Write("<td>")
                .Write(Rs("ora_rientro"))
                .Write("</td>")

                .Write("<td>")
                .Write(Rs("km_rientro"))
                .Write("</td>")

                .Write("<td>")
                .Write(Rs("serbatoio_rientro"))
                .Write("</td>")

                


                .Write("</tr>")
            Loop
        End With
        Response.Write("</table>")
        Response.Write("<div>")
        Response.Flush()
        Response.End()

        Rs.Close()
        Dbc.Close()
        Rs = Nothing
        Dbc = Nothing
    End Sub

    Protected Sub dropReport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropReport.SelectedIndexChanged
        If dropReport.SelectedValue <> "1" And dropReport.SelectedValue <> "2" Then
            lblPasso.Visible = False
            txtPasso.Visible = False
        Else
            lblPasso.Visible = True
            txtPasso.Visible = True
        End If
    End Sub

    Protected Sub btnScadenziario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScadenziario.Click
        cercaStazioneRientro.SelectedValue = "0"
        cercaStazioneUscita.SelectedValue = "0"
        txtCercaUscitaDa.Text = ""
        txtCercaUscitaA.Text = ""
        txtCercaRientroDa.Text = ""
        txtCercaRientroA.Text = ""
        dropCercaMarca.SelectedValue = "0"
        dropCercaModello.SelectedValue = "0"
        txtNumeroRifemento.Text = ""
        txtTarga.Text = ""

        cercaStazionePresuntoRientro.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
        txtCercaRientroPrDa.Text = Format(Now(), "dd/MM/yyyy")
        txtCercaRientroPrA.Text = Format(Now(), "dd/MM/yyyy")
        dropAutoRientrata.SelectedValue = "No"
        dropReport.SelectedValue = "3"

        cerca()
    End Sub

    Protected Function GetLinkRA(numrif As String, tipo_doc As String) As String
        Dim ris As String = numrif

        Try

            If tipo_doc = "Noleggio" Then
                Dim LinkPath As String = "<a href=""contratti.aspx?nr=" & numrif & """ target=""_blank"">" & numrif & "</a>"
                ris = LinkPath
            End If



        Catch ex As Exception

        End Try

        Return ris





    End Function



End Class
