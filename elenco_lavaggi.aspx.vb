Imports System
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Runtime.InteropServices.Marshal
Partial Class elenco_lavaggi
    Inherits System.Web.UI.Page
    Dim sqla As String
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
                condizione = condizione & " AND lavaggi.data_uscita_parco is null "
            ElseIf dropStatoVendita.SelectedValue = "2" Then
                condizione = condizione & " AND lavaggi.km_in is null AND lavaggi.data_uscita_parco  is not null"
            ElseIf dropStatoVendita.SelectedValue = "3" Then
                condizione = condizione & " AND lavaggi.km_in is not null AND lavaggi.data_uscita_parco  is not null"
            ElseIf dropStatoVendita.SelectedValue = "4" Then
                condizione = condizione & " AND veicoli.venduta='1' "
            End If
        End If

        If dropCercaStazione.SelectedValue <> "0" Then
            condizione = condizione & " AND stazioni.nome_stazione = '" & dropCercaStazione.SelectedValue & "'"
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
            Dim data1 As String = funzioni_comuni.GetDataSql(TxtDataDal.Text, 0)

            condizione = condizione & " AND lavaggi.data_rientro_parco >= convert(datetime,'" & data1 & "',102)"
        End If

        If TxtDataDal.Text = "" And TxtDataAl.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(TxtDataAl.Text, 59)

            condizione = condizione & " AND lavaggi.data_rientro_parco <= convert(datetime,'" & data2 & "',102)"
        End If

        If TxtDataDal.Text <> "" And TxtDataAl.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSqlCnv(TxtDataDal.Text, 0)
            Dim data2 As String = funzioni_comuni.GetDataSqlCnv(TxtDataAl.Text, 59)

            condizione = condizione & " AND lavaggi.data_rientro_parco BETWEEN " & data1 & " AND " & data2 & ""
        End If

        'condizione = condizione & " order by data_uscita_parco desc"

        '--------------------------------
        condizioneWhere = condizione
    End Function

    Protected Function getNumeroRisultati() As String

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            sqla = "SELECT ISNULL(count(veicoli.id),0) FROM veicoli WITH(NOLOCK) LEFT JOIN modelli WITH(NOLOCK) ON veicoli.id_modello = modelli.id_modello LEFT JOIN marche WITH(NOLOCK) ON modelli.id_CasaAutomobilistica = marche.id LEFT JOIN proprietari_veicoli WITH(NOLOCK) ON veicoli.id_proprietario = proprietari_veicoli.id LEFT JOIN colori WITH(NOLOCK) ON veicoli.id_colore=colori.id WHERE veicoli.id>0 " & condizioneWhere()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            'lblNumRisultati.Text = Cmd.ExecuteScalar

            'getNumeroRisultati = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error elenco_lavaggi getNumeroRisultati " & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function

    Protected Sub listRifornimenti_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listRifornimenti.ItemCommand
        If e.CommandName = "vedi" Then
            setSession()
            Dim rifornimento As Label = e.Item.FindControl("idLabel")
            Dim CSerbatoio As Label = e.Item.FindControl("capicitaserbatoioLabel")

            Response.Redirect("lavaggi.aspx?rifornimento=" & rifornimento.Text & "&serb=" & CSerbatoio.Text)
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
            If lblOrderBY.Text = " ORDER BY lavaggi.data_uscita_parco DESC" Then
                ordinaper(" ORDER BY lavaggi.data_uscita_parco ASC")
            ElseIf lblOrderBY.Text = " ORDER BY lavaggi.data_uscita_parco ASC" Then
                ordinaper(" ORDER BY lavaggi.data_uscita_parco DESC")
            Else
                ordinaper(" ORDER BY lavaggi.data_uscita_parco ASC")
            End If
        ElseIf e.CommandName = "order_by_DataIn" Then
            If lblOrderBY.Text = " ORDER BY lavaggi.data_rientro_parco DESC" Then
                ordinaper(" ORDER BY lavaggi.data_rientro_parco ASC")
            ElseIf lblOrderBY.Text = " ORDER BY lavaggi.data_rientro_parco ASC" Then
                ordinaper(" ORDER BY lavaggi.data_rientro_parco DESC")
            Else
                ordinaper(" ORDER BY lavaggi.data_rientro_parco ASC")
            End If
        ElseIf e.CommandName = "order_by_Importo" Then
            If lblOrderBY.Text = " ORDER BY rifornimenti.importo_rifornimento DESC" Then
                ordinaper(" ORDER BY rifornimenti.importo_rifornimento ASC")
            ElseIf lblOrderBY.Text = " ORDER BY rifornimenti.importo_rifornimento ASC" Then
                ordinaper(" ORDER BY rifornimenti.importo_rifornimento DESC")
            Else
                ordinaper(" ORDER BY rifornimenti.importo_rifornimento ASC")
            End If
        End If

    End Sub

    Protected Sub ordinaper(ByVal order_by As String)
        sqla = "SELECT lavaggi.id, stazioni.nome_stazione, veicoli.targa, MODELLI.descrizione, MODELLI.capacita_serbatoio, alimentazione.descrizione AS Alimentazione, " &
                                                 "lavaggi.data_uscita_parco,  lavaggi.km_out, lavaggi.data_rientro_parco, lavaggi.km_in, lavaggi.data_lavaggio, " &
                                                 "veicoli.disponibile_nolo " &
                                             "FROM  lavaggi WITH(NOLOCK) INNER JOIN " &
                                                 "veicoli WITH(NOLOCK) ON rifornimenti.id_veicolo = veicoli.id INNER JOIN " &
                                                 "MODELLI WITH(NOLOCK) ON veicoli.id_modello = MODELLI.ID_MODELLO INNER JOIN " &
                                                 "alimentazione WITH(NOLOCK) ON MODELLI.TipoCarburante = alimentazione.id INNER JOIN " &
                                                 "stazioni WITH(NOLOCK) ON veicoli.id_stazione = stazioni.id " &
                                             "WHERE(1 = 1) " & lblWhere.Text & lblOrderBY.Text
        Try
            lblOrderBY.Text = order_by
            lblWhere.Text = condizioneWhere(sqla)

            SqlRifornimenti.SelectCommand = 

            'Response.Write(SqlRifornimenti.SelectCommand)
            'Response.End()

            'lblSql.Visible = True
            'lblWhere.Visible = True
            'lblOrderBY.Visible = True

            'lblNumRisultati.Text = getNumeroRisultati()
            listRifornimenti.DataBind()
            LblCaricaElenco.Text = "CaricaFile"
        Catch ex As Exception
            HttpContext.Current.Response.Write("error elenco_lavaggi ordinaper " & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Protected Sub cerca()

        Try
            SqlRifornimenti.SelectCommand = "SELECT lavaggi.id, stazioni.nome_stazione, veicoli.targa, MODELLI.descrizione, MODELLI.capacita_serbatoio, alimentazione.descrizione AS Alimentazione, " &
                                                       "lavaggi.data_uscita_parco,  lavaggi.km_out, lavaggi.data_rientro_parco, lavaggi.km_in, lavaggi.data_lavaggio, " &
                                                       "veicoli.disponibile_nolo " &
                                                   "FROM  lavaggi WITH(NOLOCK) INNER JOIN " &
                                                       "veicoli WITH(NOLOCK) ON lavaggi.id_veicolo = veicoli.id INNER JOIN " &
                                                       "MODELLI WITH(NOLOCK) ON veicoli.id_modello = MODELLI.ID_MODELLO INNER JOIN " &
                                                       "alimentazione WITH(NOLOCK) ON MODELLI.TipoCarburante = alimentazione.id INNER JOIN " &
                                                       "stazioni WITH(NOLOCK) ON veicoli.id_stazione = stazioni.id " &
                                                   "WHERE(1 = 1) "

            lblWhere.Text = condizioneWhere()
            lblOrderBY.Text = " order by data_uscita_parco desc"

            txtQuery.Text = SqlRifornimenti.SelectCommand & lblWhere.Text & lblOrderBY.Text

            SqlRifornimenti.SelectCommand = txtQuery.Text


            lblSql.Text = SqlRifornimenti.SelectCommand
            Session("SQL_Ricerca") = lblSql.Text
            'Response.Write(Session("SQL_Ricerca"))
            'Response.End()

            'lblSql.Visible = True
            'lblWhere.Visible = True

            lblNumRisultati.Text = getNumeroRisultati()
            listRifornimenti.DataBind()
            LblCaricaElenco.Text = "CaricaFile"
        Catch ex As Exception
            HttpContext.Current.Response.Write("error elenco_lavaggi cerca " & ex.Message & "<br/>" & SqlRifornimenti.SelectCommand & "<br/>")
        End Try



    End Sub

    Protected Sub setSession()
        If dropCercaStazione.SelectedValue <> "" Then
            Session("rifornumento_stazione") = dropCercaStazione.SelectedValue
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Response.Write("DD staz " & dropStatoVendita.SelectedValue & "<br/>")
            'Response.Write("sess " & Session("rifornumento_stato") & "<br/>")
            'Response.End()     
            dropCercaStazione.DataBind()
            dropCercaMarca.DataBind()
            dropCercaModello.DataBind()
            If Session("rifornumento_stazione") <> "0" Then
                dropCercaStazione.SelectedValue = Session("rifornumento_stazione")
            End If
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
        Else
            Session("rifornumento_stazione") = ""
            Session("rifornumento_dal") = ""
            Session("rifornumento_al") = ""
            Session("rifornumento_targa") = ""
            Session("rifornumento_telaio") = ""
            Session("rifornumento_marca") = ""
            Session("rifornumento_modello") = ""
            Session("rifornumento_stato") = ""
        End If
    End Sub

    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampa.Click


        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=Export_Lavaggi.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.xls"

        Dim SqlStr As String

        Try
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
                .Write("Km Out")
                .Write("</td>")
                .Write("<td>")
                .Write("Data In")
                .Write("</td>")
                .Write("<td>")
                .Write("Km In")
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
                    .Write(Rs("km_out"))
                    .Write("</td>")
                    .Write("<td>")
                    .Write(Rs("data_rientro_parco"))
                    .Write("</td>")
                    .Write("<td>")
                    .Write(Rs("km_in"))
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

            SqlStr = txtQuery.Text

        Catch ex As Exception
            HttpContext.Current.Response.Write("error btnstampa.click " & ex.Message & "<br/>" & SqlStr & "<br/>")
        End Try


    End Sub

    Protected Function getFiltroRicerca() As String
        getFiltroRicerca = "stazione=" & dropCercaStazione.SelectedValue & "&" & _
        "DataDa=" & TxtDataDal.Text & "&" & _
         "DataA=" & TxtDataAl.Text & "&" & _
         "Targa=" & txtCercaTarga.Text & "&" & _
         "Marca=" & dropCercaMarca.SelectedValue & "&" & _
         "Modello=" & dropCercaModello.SelectedValue & "&"
        '"QuerySql=" & lblSql.Text

        'Return Server.UrlEncode(getFiltroRicerca)

    End Function

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

End Class
