Imports System
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Runtime.InteropServices.Marshal
Partial Class elenco_rifornimenti

    Inherits System.Web.UI.Page


    Protected Sub dropCercaMarca_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropCercaMarca.SelectedIndexChanged
        dropCercaModello.Items.Clear()
        dropCercaModello.Items.Add("Seleziona...")
        dropCercaModello.Items(0).Value = 0
        dropCercaModello.DataBind()
    End Sub

    Protected Sub btnCercaVeicolo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCercaVeicolo.Click
        cerca()
    End Sub

    Protected Function condizioneWhere() As String

        Dim condizione As String = ""

        If dropStatoVendita.SelectedValue <> "0" Then
            If dropStatoVendita.SelectedValue = "1" Then
                condizione = condizione & " AND rifornimenti.data_uscita_parco is null "
            ElseIf dropStatoVendita.SelectedValue = "2" Then
                condizione = condizione & " AND rifornimenti.km_in is null AND rifornimenti.data_uscita_parco  is not null"
            ElseIf dropStatoVendita.SelectedValue = "3" Then
                condizione = condizione & " AND rifornimenti.km_in is not null AND rifornimenti.data_uscita_parco  is not null"
            ElseIf dropStatoVendita.SelectedValue = "4" Then
                condizione = condizione & " AND rifornimenti.km_in is not null AND rifornimenti.data_uscita_parco  is not null AND importo_rifornimento IS NULL"
            End If
        End If

        If DDLFornitore.SelectedValue <> "0" Then
            condizione = condizione & " AND rifornimenti.id_fornitore = '" & DDLFornitore.SelectedValue & "'"
        End If

        If dropCercaStazione.SelectedValue <> "0" Then
            If dropStatoVendita.SelectedValue = "1" Then
                condizione = condizione & " AND veicoli.id_stazione = '" & dropCercaStazione.SelectedValue & "'"
            Else
                condizione = condizione & " AND rifornimenti.id_stazione_out = '" & dropCercaStazione.SelectedValue & "'"
            End If

        End If

        If Trim(txtCercaTarga.Text) <> "" Then
            condizione = condizione & " AND veicoli.targa LIKE '" & Trim(txtCercaTarga.Text) & "%'"
        End If

        If Trim(txtCercaTelaio.Text) <> "" Then
            condizione = condizione & " AND veicoli.telaio LIKE '" & Trim(txtCercaTelaio.Text) & "%'"
        End If

        If dropCercaMarca.SelectedValue > 0 Then
            condizione = condizione & " AND modelli.id_CasaAutomobilistica = '" & dropCercaMarca.SelectedValue & "'"
        End If

        If dropCercaModello.SelectedValue > 0 Then
            condizione = condizione & " AND veicoli.id_modello = '" & dropCercaModello.SelectedValue & "'"
        End If

        'Data Rifornimento---------------------------------------------------------------------------------------------------------
        If TxtDataDal.Text <> "" And TxtDataAl.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(TxtDataDal.Text, 102)   'funzioni_comuni.getDataDb_senza_orario(TxtDataDal.Text)
            'condizione = condizione & " AND rifornimenti.data_rientro_parco >= '" & data1 & "'"
            condizione += " AND rifornimenti.data_rientro_parco >= CONVERT(DATETIME,'" & data1 & "',102)"
        End If

        If TxtDataDal.Text = "" And TxtDataAl.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(TxtDataAl.Text, 102)   'funzioni_comuni.getDataDb_con_orario(TxtDataAl.Text & " 23:59:59")
            condizione += " AND rifornimenti.data_rientro_parco <= CONVERT(DATETIME,'" & data2 & "',102)"
            ' condizione = condizione & " AND rifornimenti.data_rientro_parco <= '" & data2 & "'"
        End If

        If TxtDataDal.Text <> "" And TxtDataAl.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(TxtDataDal.Text, 102)   'funzioni_comuni.getDataDb_senza_orario(TxtDataDal.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(TxtDataAl.Text, 102)   'funzioni_comuni.getDataDb_con_orario(TxtDataAl.Text & " 23:59:59")
            'condizione = condizione & " AND rifornimenti.data_rientro_parco BETWEEN '" & data1 & "' AND '" & data2 & "'"
            condizione += " AND rifornimenti.data_rientro_parco BETWEEN CONVERT(DATETIME,'" & data1 & "',102) AND CONVERT(DATETIME,'" & data2 & "',102)"
        End If

        'condizione = condizione & " order by data_uscita_parco desc"

        '--------------------------------
        condizioneWhere = condizione
    End Function

    Protected Function getNumeroRisultati() As String
        Dim sql As String = "SELECT ISNULL(count(veicoli.id),0) FROM veicoli WITH(NOLOCK) LEFT JOIN modelli WITH(NOLOCK) ON veicoli.id_modello = modelli.id_modello LEFT JOIN marche WITH(NOLOCK) ON modelli.id_CasaAutomobilistica = marche.id LEFT JOIN proprietari_veicoli WITH(NOLOCK) ON veicoli.id_proprietario = proprietari_veicoli.id LEFT JOIN colori WITH(NOLOCK) ON veicoli.id_colore=colori.id WHERE veicoli.id>0 " & condizioneWhere()
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            'lblNumRisultati.Text = Cmd.ExecuteScalar

            'getNumeroRisultati = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_getNumeroRisultati" & "<br/>" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Function

    Protected Sub listRifornimenti_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listRifornimenti.ItemCommand
        Dim sql As String = ""
        Try
            If e.CommandName = "vedi" Then
                setSession()
                Dim rifornimento As Label = e.Item.FindControl("idLabel")
                Dim CSerbatoio As Label = e.Item.FindControl("capicitaserbatoioLabel")

                Response.Redirect("rifornimenti.aspx?rifornimento=" & rifornimento.Text & "&serb=" & CSerbatoio.Text)
            ElseIf e.CommandName = "elimina" Then
                Dim idLabel As Label = e.Item.FindControl("idLabel")

                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                sql = "DELETE FROM rifornimenti WHERE id=" & idLabel.Text & " AND data_uscita_parco is null"
                Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)
                Cmd.ExecuteNonQuery()

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                cerca()
            ElseIf e.CommandName = "order_by_Targa" Then
                If lblOrderBY.Text = " ORDER BY veicoli.targa DESC" Then
                    ordinaper(" ORDER BY veicoli.targa ASC")
                ElseIf lblOrderBY.Text = " ORDER BY veicoli.targa ASC" Then
                    ordinaper(" ORDER BY veicoli.targa DESC")
                Else
                    ordinaper(" ORDER BY veicoli.targa ASC")
                End If
            ElseIf e.CommandName = "order_by_Modello" Then
                If lblOrderBY.Text = " ORDER BY MODELLI.descrizione DESC" Then
                    ordinaper(" ORDER BY MODELLI.descrizione ASC")
                ElseIf lblOrderBY.Text = " ORDER BY MODELLI.descrizione ASC" Then
                    ordinaper(" ORDER BY MODELLI.descrizione DESC")
                Else
                    ordinaper(" ORDER BY MODELLI.descrizione ASC")
                End If
            ElseIf e.CommandName = "order_by_Data" Then
                If lblOrderBY.Text = " ORDER BY rifornimenti.data_uscita_parco DESC" Then
                    ordinaper(" ORDER BY rifornimenti.data_uscita_parco ASC")
                ElseIf lblOrderBY.Text = " ORDER BY rifornimenti.data_uscita_parco ASC" Then
                    ordinaper(" ORDER BY rifornimenti.data_uscita_parco DESC")
                Else
                    ordinaper(" ORDER BY rifornimenti.data_uscita_parco ASC")
                End If
            ElseIf e.CommandName = "order_by_DataIn" Then
                If lblOrderBY.Text = " ORDER BY rifornimenti.data_rientro_parco DESC" Then
                    ordinaper(" ORDER BY rifornimenti.data_rientro_parco ASC")
                ElseIf lblOrderBY.Text = " ORDER BY rifornimenti.data_rientro_parco ASC" Then
                    ordinaper(" ORDER BY rifornimenti.data_rientro_parco DESC")
                Else
                    ordinaper(" ORDER BY rifornimenti.data_rientro_parco ASC")
                End If
            ElseIf e.CommandName = "order_by_Importo" Then
                If lblOrderBY.Text = " ORDER BY rifornimenti.importo_rifornimento DESC" Then
                    ordinaper(" ORDER BY rifornimenti.importo_rifornimento ASC")
                ElseIf lblOrderBY.Text = " ORDER BY rifornimenti.importo_rifornimento ASC" Then
                    ordinaper(" ORDER BY rifornimenti.importo_rifornimento DESC")
                Else
                    ordinaper(" ORDER BY rifornimenti.importo_rifornimento ASC")
                End If
            ElseIf e.CommandName = "order_by_fornitore" Then
                If lblOrderBY.Text = " ORDER BY fornitore DESC" Then
                    ordinaper(" ORDER BY fornitore ASC")
                ElseIf lblOrderBY.Text = " ORDER BY fornitore ASC" Then
                    ordinaper(" ORDER BY fornitore DESC")
                Else
                    ordinaper(" ORDER BY fornitore ASC")
                End If
            End If


        Catch ex As Exception
            HttpContext.Current.Response.Write("error_listRifornimenti_ItemCommand" & "<br/>" & ex.Message & "<br/>" & Sql & "<br/>")
        End Try


    End Sub

    Protected Sub ordinaper(ByVal order_by As String)
        Dim sql As String = "SELECT anno_rifornimento, num_rifornimento, rifornimenti.id, stazioni.nome_stazione, veicoli.targa, MODELLI.descrizione, MODELLI.capacita_serbatoio, alimentazione.descrizione AS Alimentazione, "
        sql += "rifornimenti.data_uscita_parco, rifornimenti.serbatoio, rifornimenti.km_out, rifornimenti.data_rientro_parco, rifornimenti.km_in, rifornimenti.data_rifornimento, "
        sql += "rifornimenti.importo_rifornimento,veicoli.disponibile_nolo, alimentazione_fornitori_x_stazione.descrizione As fornitore "
        sql += "FROM  rifornimenti WITH(NOLOCK) INNER JOIN "
        sql += "veicoli WITH(NOLOCK) ON rifornimenti.id_veicolo = veicoli.id INNER JOIN "
        sql += "MODELLI WITH(NOLOCK) ON veicoli.id_modello = MODELLI.ID_MODELLO INNER JOIN "
        sql += "alimentazione WITH(NOLOCK) ON MODELLI.TipoCarburante = alimentazione.id INNER JOIN "
        sql += "stazioni WITH(NOLOCK) ON veicoli.id_stazione = stazioni.id "
        sql += " LEFT JOIN alimentazione_fornitori_x_stazione WITH(NOLOCK) ON rifornimenti.id_fornitore=alimentazione_fornitori_x_stazione.id "
        sql += "WHERE(1 = 1) AND veicoli.venduta='0' " & lblWhere.Text & lblOrderBY.Text

        Try
            lblOrderBY.Text = order_by
            lblWhere.Text = condizioneWhere()

            SqlRifornimenti.SelectCommand = sql
            'Response.Write(SqlRifornimenti.SelectCommand)
            'Response.End()

            'lblSql.Visible = True
            'lblWhere.Visible = True
            'lblOrderBY.Visible = True

            'lblNumRisultati.Text = getNumeroRisultati()
            listRifornimenti.DataBind()
            LblCaricaElenco.Text = "CaricaFile"
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_ordinaper" & "<br/>" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Sub

    Protected Sub elimina_doppie_righe()
        Dim sql As String = "DELETE FROM rifornimenti WHERE id IN (SELECT MAX(id) FROM rifornimenti WITH(NOLOCK) WHERE num_rifornimento IS NULL GROUP BY id_veicolo HAVING COUNT(id) > 1)"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_elimina_doppie_righe" & "<br/>" & ex.Message & "<br/>" & sql & "<br/>")
        End Try


    End Sub

    Protected Sub cerca()

        'PRIMA DELLA RICERCA ELIMINO EVENTUALI RIGHE DOPPIE
        Dim sql As String = "SELECT anno_rifornimento, num_rifornimento, rifornimenti.id, stazioni.nome_stazione, veicoli.targa, MODELLI.descrizione, MODELLI.capacita_serbatoio, alimentazione.descrizione AS Alimentazione, "
        sql += "rifornimenti.data_uscita_parco, rifornimenti.serbatoio, rifornimenti.km_out, rifornimenti.data_rientro_parco, rifornimenti.km_in, rifornimenti.data_rifornimento, "
        sql += "rifornimenti.importo_rifornimento,veicoli.disponibile_nolo, alimentazione_fornitori_x_stazione.descrizione As fornitore "
        sql += "FROM  rifornimenti WITH(NOLOCK) INNER JOIN "
        sql += "veicoli WITH(NOLOCK) ON rifornimenti.id_veicolo = veicoli.id INNER JOIN "
        sql += "MODELLI WITH(NOLOCK) ON veicoli.id_modello = MODELLI.ID_MODELLO INNER JOIN "
        sql += "alimentazione WITH(NOLOCK) ON MODELLI.TipoCarburante = alimentazione.id INNER JOIN "
        sql += "stazioni WITH(NOLOCK) ON veicoli.id_stazione = stazioni.id "
        sql += " LEFT JOIN alimentazione_fornitori_x_stazione WITH(NOLOCK) ON rifornimenti.id_fornitore=alimentazione_fornitori_x_stazione.id "
        sql += "WHERE(1 = 1) "

        Try
            elimina_doppie_righe()
            SqlRifornimenti.SelectCommand = sql

            lblWhere.Text = condizioneWhere()
            lblOrderBY.Text = " order by data_uscita_parco desc"

            txtQuery.Text = SqlRifornimenti.SelectCommand & lblWhere.Text & lblOrderBY.Text

            'Response.Write(txtQuery.Text & "<br/>")

            SqlRifornimenti.SelectCommand = txtQuery.Text

            lblSql.Text = SqlRifornimenti.SelectCommand
            Session("SQL_Ricerca") = lblSql.Text

            'lblSql.Visible = True
            'lblWhere.Visible = True

            lblNumRisultati.Text = getNumeroRisultati()
            listRifornimenti.DataBind()
            LblCaricaElenco.Text = "CaricaFile"

            If dropStatoVendita.SelectedValue = 3 Then 'Testo in menù tendina Riforniti
                BtnStampaCarburante.Visible = True
                If TxtDataDal.Text <> "" And DDLFornitore.SelectedValue <> "0" Then
                    BtnStampaCarburante.Enabled = True
                Else
                    BtnStampaCarburante.Enabled = False
                End If
            Else
                BtnStampaCarburante.Visible = False
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_cerca" & "<br/>" & ex.Message & "<br/>" & sql & "<br/>")
        End Try



    End Sub

    Protected Sub setSession()
        If dropCercaStazione.SelectedValue <> "" Then
            Session("rifornumento_stazione") = GetNomeStazione(dropCercaStazione.SelectedValue)
        End If
        If Trim(TxtDataDal.Text) <> "" Then
            Session("rifornumento_dal") = TxtDataDal.Text
        End If
        If Trim(TxtDataAl.Text) <> "" Then
            Session("rifornumento_al") = TxtDataAl.Text
        End If
        If Trim(txtCercaTarga.Text) <> "" Then
            Session("rifornumento_targa") = txtCercaTarga.Text
        End If
        If Trim(txtCercaTelaio.Text) <> "" Then
            Session("rifornumento_telaio") = txtCercaTelaio.Text
        End If
        If dropCercaMarca.SelectedValue <> "" Then
            Session("rifornumento_marca") = dropCercaMarca.SelectedValue
        End If
        If dropCercaModello.SelectedValue <> "" Then
            Session("rifornumento_modello") = dropCercaModello.SelectedValue
        End If
        If dropStatoVendita.SelectedValue <> "" Then
            Session("rifornumento_stato") = dropStatoVendita.SelectedValue
        End If
    End Sub

    Protected Function GetNomeStazione(ByVal id_stazione As String) As String

        Dim sql As String = "SELECT nome_stazione FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            'SELEZIONO L'ID DEL CONTRATTO
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            GetNomeStazione = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_GetNomeStazione" & "<br/>" & ex.Message & "<br/>" & sql & "<br/>")
        End Try


    End Function

    Protected Function getIdStazione(ByVal nome_stazione As String) As String
        Dim sql As String = "SELECT id FROM stazioni WITH(NOLOCK) WHERE nome_stazione='" & Replace(nome_stazione, "'", "''") & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            'SELEZIONO L'ID DEL CONTRATTO
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            getIdStazione = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_getIdStazione" & "<br/>" & ex.Message & "<br/>" & Sql & "<br/>")
        End Try


    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        If Not Page.IsPostBack Then

            livello_accesso.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Rifornimenti)
            livello_accesso_admin.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.RifornimentoAdmin)

            If livello_accesso.Text = "1" Then
                Response.Redirect("default.aspx")
            End If

            dropCercaStazione.DataBind()

            If livello_accesso_admin.Text = "1" Then
                dropCercaStazione.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")

                dropCercaStazione.Enabled = False
            Else
                If Session("rifornumento_stazione") <> "0" Then
                    dropCercaStazione.SelectedValue = getIdStazione(Session("rifornumento_stazione"))
                End If
            End If

            If livello_accesso_admin.Text <> "3" Then
                btnCreaRifornimento.Visible = False
            End If

            dropCercaMarca.DataBind()
            dropCercaModello.DataBind()


            If Session("rifornumento_dal") <> "" Then
                TxtDataDal.Text = Session("rifornumento_dal")
            End If
            If Session("rifornumento_al") <> "" Then
                TxtDataAl.Text = Session("rifornumento_al")
            End If
            If Session("rifornumento_targa") <> "" Then
                txtCercaTarga.Text = Session("rifornumento_targa")
            End If
            If Session("rifornumento_telaio") <> "" Then
                txtCercaTelaio.Text = Session("rifornumento_telaio")
            End If
            If Session("rifornumento_marca") <> "" Then
                dropCercaMarca.SelectedValue = Session("rifornumento_marca")
            End If
            If Session("rifornumento_modello") <> "" Then
                dropCercaModello.SelectedValue = Session("rifornumento_modello")
            End If
            If Session("rifornumento_stato") <> "" Then
                dropStatoVendita.SelectedValue = Session("rifornumento_stato")
            End If
            cerca()

            Session("rifornumento_stazione") = ""
            Session("rifornumento_dal") = ""
            Session("rifornumento_al") = ""
            Session("rifornumento_targa") = ""
            Session("rifornumento_telaio") = ""
            Session("rifornumento_marca") = ""
            Session("rifornumento_modello") = ""
            Session("rifornumento_stato") = ""
        Else
            Session("rifornumento_stazione") = ""
            Session("rifornumento_dal") = ""
            Session("rifornumento_al") = ""
            Session("rifornumento_targa") = ""
            Session("rifornumento_telaio") = ""
            Session("rifornumento_marca") = ""
            Session("rifornumento_modello") = ""
            Session("rifornumento_stato") = ""

            SqlRifornimenti.SelectCommand = txtQuery.Text
        End If



    End Sub

    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampa.Click
        Dim sql As String = ""
        Try
            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=Export_Rifornimenti.xls")
            Response.Charset = ""
            Response.ContentType = "application/vnd.xls"

            Dim SqlStr As String
            SqlStr = txtQuery.Text
            Dim Dbc As New Data.SqlClient.SqlConnection(SqlRifornimenti.ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(SqlStr, Dbc)
            'Response.Write(Cmd.CommandText & "<br><br>")
            'Response.End()

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            With Response
                .Write("<table border=1>")
                .Write("<tr>")
                .Write("<td>")
                .Write("Stazione")
                .Write("</td>")
                .Write("<td>")
                .Write("Targa")
                .Write("</td>")
                .Write("<td>")
                .Write("Modello")
                .Write("</td>")
                .Write("<td>")
                .Write("Capacit&agrave Serbatoio")
                .Write("</td>")
                .Write("<td>")
                .Write("Alimentazione")
                .Write("</td>")
                .Write("<td>")
                .Write("Data Out")
                .Write("</td>")
                .Write("<td>")
                .Write("Serbatoio Out")
                .Write("</td>")
                .Write("<td>")
                .Write("Km Out")
                .Write("</td>")
                .Write("<td>")
                .Write("Data In")
                .Write("</td>")
                .Write("<td>")
                .Write("Km In")
                .Write("</td>")
                .Write("<td>")
                .Write("Importo")
                .Write("</td>")
                .Write("</tr>")
            End With

            Do While Rs.Read
                'Response.Write("<strong>" & Rs("descrizione") & "</strong><br/>")
                'ContatoreCategoria = Rs("id")
                With Response
                    .Write("<tr>")
                    .Write("<td>")
                    .Write(Rs("nome_stazione"))
                    .Write("</td>")
                    .Write("<td>")
                    .Write(Rs("targa"))
                    .Write("</td>")
                    .Write("<td>")
                    .Write(Rs("descrizione"))
                    .Write("</td>")
                    .Write("<td>")
                    .Write(Rs("capacita_serbatoio"))
                    .Write("</td>")
                    .Write("<td>")
                    .Write(Rs("alimentazione"))
                    .Write("</td>")
                    .Write("<td>")
                    .Write(Rs("data_uscita_parco"))
                    .Write("</td>")
                    .Write("<td>")
                    .Write(Rs("serbatoio"))
                    .Write("</td>")
                    .Write("<td>")
                    .Write(Rs("km_out"))
                    .Write("</td>")
                    .Write("<td>")
                    .Write(Rs("data_rientro_parco"))
                    .Write("</td>")
                    .Write("<td>")
                    .Write(Rs("km_in"))
                    .Write("</td>")
                    .Write("<td>")
                    .Write(Rs("importo_rifornimento"))
                    .Write("</td>")
                    .Write("</tr>")
                End With
            Loop

            Response.Write("</table>")
            Response.Flush()
            Response.End()

            Rs.Close()
            Dbc.Close()
            Rs = Nothing
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("error_btnStampa_Click" & "<br/>" & ex.Message & "<br/>" & Sql & "<br/>")
        End Try




    End Sub

    Protected Function getFiltroRicerca() As String
        getFiltroRicerca = "id_stazione_out=" & dropCercaStazione.SelectedValue & "&" & _
        "DataDa=" & TxtDataDal.Text & "&" & _
         "DataA=" & TxtDataAl.Text & "&" & _
         "Targa=" & txtCercaTarga.Text & "&" & _
         "Marca=" & dropCercaMarca.SelectedValue & "&" & _
         "Fornitore=" & DDLFornitore.SelectedValue & "&" & _
         "Modello=" & dropCercaModello.SelectedValue & "&"
        '"QuerySql=" & lblSql.Text

        'Return Server.UrlEncode(getFiltroRicerca)

    End Function

    Protected Sub BtnStampaCarburante_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnStampaCarburante.Click
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then            
            Dim url_print As String = "/Stampe/Stampa_Scheda_Carburante.aspx?orientamento=verticale&" & getFiltroRicerca()
            'Response.Write(url_print)
            'Response.End()
            Trace.Write(url_print)
            Dim mio_random As String = Format((New Random).Next(), "0000000000")
            Trace.Write(url_print)
            Session("url_print") = url_print
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx?a=" & mio_random & "','')", True)
            End If
        End If
    End Sub

    Protected Sub btnPulisciCampi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPulisciCampi.Click
        dropCercaStazione.SelectedValue = "0"
        TxtDataDal.Text = ""
        TxtDataAl.Text = ""
        txtCercaTarga.Text = ""
        dropCercaMarca.SelectedValue = "0"
        dropCercaModello.SelectedValue = "0"
        dropStatoVendita.SelectedValue = "1"

        cerca()
    End Sub

    Protected Sub listRifornimenti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listRifornimenti.ItemDataBound
        Dim disponibile_nolo As Label = e.Item.FindControl("disponibile_nolo")

        If disponibile_nolo.Text = "True" Then
            disponibile_nolo.Text = "SI"
        ElseIf disponibile_nolo.Text = "False" Then
            disponibile_nolo.Text = "NO"
        End If

        Dim anno_rifornimento As Label = e.Item.FindControl("anno_rifornimento")

        If anno_rifornimento.Text <> "" Then
            Dim num_rifornimento As Label = e.Item.FindControl("num_rifornimento")
            Dim num_rif As Label = e.Item.FindControl("num_rif")

            'num_rif.Text = Right(anno_rifornimento.Text, 2) & "/" & num_rifornimento.Text      'tolto per visualizzare ID 22.01.2021
        End If

        Dim btnCancella As ImageButton = e.Item.FindControl("btnCancella")
        Dim dataUscitaParco As Label = e.Item.FindControl("dataRifLabel")

        If dataUscitaParco.Text.Trim <> "" Then
            btnCancella.Visible = False
        End If

        If livello_accesso_admin.Text <> "3" Then
            btnCancella.Visible = False
        End If
    End Sub

    Protected Sub dropCercaStazione_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropCercaStazione.SelectedIndexChanged
        If dropCercaStazione.SelectedValue = "0" Then
            DDLFornitore.Items.Clear()
            DDLFornitore.Items.Add("Seleziona...")
            DDLFornitore.Items(0).Value = "0"

            DDLFornitore.DataBind()
        Else
            DDLFornitore.Items.Clear()
            DDLFornitore.Items.Add("Seleziona...")
            DDLFornitore.Items(0).Value = "0"

            DDLFornitore.DataBind()
        End If
        
    End Sub

    Protected Function getIdVeicolo() As String
        Dim sql As String = "SELECT id FROM veicoli WITH(NOLOCK) WHERE targa='" & txtCercaTarga.Text & "' AND venduta='0' AND NOT id_stazione IS NULL"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            Dim id_veicolo As String = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            getIdVeicolo = id_veicolo

        Catch ex As Exception
            HttpContext.Current.Response.Write("error_getIdVeicolo_Click" & "<br/>" & ex.Message & "<br/>" & sql & "<br/>")
        End Try


    End Function

    Protected Function rifornimento_creabile(ByVal id_veicolo As String) As Boolean
        Dim sql As String = "SELECT TOP 1 id FROM rifornimenti WITH(NOLOCK) WHERE id_veicolo=" & id_veicolo & " AND (data_uscita_parco IS NULL OR (km_in is null AND rifornimenti.data_uscita_parco  is not null))"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            'SELEZIONO L'ID DEL CONTRATTO
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            Dim test As String = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            If test <> "" Then
                rifornimento_creabile = False
            Else
                rifornimento_creabile = True
            End If

        Catch ex As Exception
            HttpContext.Current.Response.Write("error_rifornimento_creabile_Click" & "<br/>" & ex.Message & "<br/>" & sql & "<br/>")
        End Try



    End Function

    Protected Sub crea_rifornimento(ByVal id_veicolo As String)
        Dim sql As String = "INSERT INTO rifornimenti (id_veicolo, serbatoio) VALUES (" & id_veicolo & ",(SELECT serbatoio_attuale FROM veicoli WITH(NOLOCK) WHERE id=" & id_veicolo & "))"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            'SELEZIONO L'ID DEL CONTRATTO
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_crea_rifornimento" & "<br/>" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Sub

    Protected Sub btnCreaRifornimento_Click(sender As Object, e As System.EventArgs) Handles btnCreaRifornimento.Click
        If txtCercaTarga.Text.Trim = "" Then
            Libreria.genUserMsgBox(Me, "Attenzione: specifiacare una targa")
        Else
            Dim id_veicolo As String = getIdVeicolo()
            If id_veicolo = "" Then
                Libreria.genUserMsgBox(Me, "Attenzione: veicolo non esistente o non rifornibile perchè venduto o non immesso in parco.")
            Else
                If Not rifornimento_creabile(id_veicolo) Then
                    Libreria.genUserMsgBox(Me, "Attenzione: riga rifornimento già esistente per questo veicolo.")
                Else
                    crea_rifornimento(id_veicolo)
                    Libreria.genUserMsgBox(Me, "Riga rifornimento salvata correttamente.")
                End If
            End If
        End If
    End Sub
End Class
