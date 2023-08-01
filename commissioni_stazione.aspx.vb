Imports funzioni_comuni
Imports libreria

Partial Class commissioni_stazione
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Try
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Commissioni) = "1" Then
                    Response.Redirect("default.aspx")
                End If


                'riempie ddlanno

                For x = Year(Date.Now) To 2015 Step -1

                    ddl_anno.Items.Add(New ListItem(x.ToString, x.ToString))

                Next

                ddl_anno.SelectedValue = Year(Date.Now)
                ddl_mese.SelectedValue = Month(Date.Now)

            Catch ex As Exception

            End Try

        End If


    End Sub

    Protected Function condizione_where() As String
        'Dim da_data As String = funzioni_comuni.getDataDb_senza_orario(txtCercaDropOffDa.Text)
        'Dim a_data As String = funzioni_comuni.getDataDb_con_orario(txtCercaDropOffA.Text & " 23:59:59")

        Dim da_data As String = funzioni_comuni.GetDataSql(txtCercaDropOffDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaDropOffDa.Text)
        Dim a_data As String = funzioni_comuni.GetDataSql(txtCercaDropOffA.Text, 59) ' funzioni_comuni.getDataDb_con_orario(txtCercaDropOffA.Text & " 23:59:59")


        'condizione_where = " AND (contratti.id_stazione_uscita=" & dropCercaStazionePickUp.SelectedValue & ") AND (contratti.data_rientro BETWEEN '" & da_data & "' AND '" & a_data & "') "

        condizione_where = " AND (contratti.id_stazione_uscita=" & dropCercaStazionePickUp.SelectedValue & ") AND (contratti.data_rientro BETWEEN Convert(DateTime, '" & da_data & "', 102) AND CONVERT(DATETIME, '" & a_data & "', 102)) "


    End Function

    Protected Function controlla_filtri() As String
        Dim messaggio As String = ""

        If dropCercaStazionePickUp.SelectedValue = "0" Then
            messaggio = messaggio & "- Selezionare una stazione" & vbCrLf
        End If

        If txtPercentualeImponibile.Text = "" Then
            messaggio = messaggio & "- Specificare la percentuale sull'imponibile" & vbCrLf
        End If

        If txtPercentualeCommissione.Text = "" Then
            messaggio = messaggio & "- Specificare la percentuale della commmissione (da applicare sulla percentuale dell'imponibile)" & vbCrLf
        End If

        If txtCercaDropOffA.Text = "" Or txtCercaDropOffDa.Text = "" Then
            messaggio = messaggio & "- Specificare un'intervallo di date di drop off per effettuare il calcolo delle commissioni " & vbCrLf
        End If

        Return messaggio
    End Function

    Protected Function controlla_filtri_royalty() As String
        Dim messaggio As String = ""

        If dropCercaStazionePickUp.SelectedValue = "0" Then
            messaggio = "Selezionare una stazione" & vbCrLf

        Else
            'Pisa 10 , Firenze 11, Comiso 6, Trapani 5
            If dropCercaStazionePickUp.SelectedValue <> "10" And dropCercaStazionePickUp.SelectedValue <> "11" _
                And dropCercaStazionePickUp.SelectedValue <> "6" And dropCercaStazionePickUp.SelectedValue <> "5" And dropCercaStazionePickUp.SelectedValue <> "14" Then
                messaggio = "Report non presente per questo Aeroporto" & vbCrLf
            Else
                'verifica se dati presenti per quel mese/anno
                If funzioni_comuni_new.GetDatiRoyalty(dropCercaStazionePickUp.SelectedValue, ddl_mese.SelectedValue, ddl_anno.SelectedValue) = False Then
                    messaggio = "Non sono presenti dati per il periodo selezionato" & vbCrLf
                End If
            End If
        End If

        If ddl_mese.SelectedValue > Date.Now.Month And ddl_anno.SelectedValue >= Date.Now.Year Then
            messaggio += " - Il mese selezionato non è valido" & vbCrLf
        End If


        'If txtPercentualeImponibile.Text = "" Then
        '    messaggio = messaggio & "- Specificare la percentuale sull'imponibile" & vbCrLf
        'End If

        'If txtPercentualeCommissione.Text = "" Then
        '    messaggio = messaggio & "- Specificare la percentuale della commmissione (da applicare sulla percentuale dell'imponibile)" & vbCrLf
        'End If

        'If txtCercaDropOffA.Text = "" Or txtCercaDropOffDa.Text = "" Then
        '    messaggio = messaggio & "- Specificare un'intervallo di date di drop off per effettuare il calcolo delle commissioni " & vbCrLf
        'End If

        Return messaggio
    End Function


    Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click

        Dim sqlStr As String = ""

        Try

            Dim controllo_filtri As String = controlla_filtri()

            If controllo_filtri = "" Then
                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(MAX(codice_stampa),0) FROM commissioni_stazione_appoggio WITH(NOLOCK)", Dbc)

                Dim codice_stampa As Integer = Cmd.ExecuteScalar + 1

                For i = 0 To listCommissioniStazione.Items.Count - 1
                    Dim num_contratto As Label = listCommissioniStazione.Items(i).FindControl("num_contratto")

                    '<a href='contratti.aspx?nr=102213704' target='_blank'>102213704</a>'
                    Dim a() As String = Split(num_contratto.Text, ">")
                    Dim nco As String = Left(a(1), 9)

                    Dim num_calcolo As Label = listCommissioniStazione.Items(i).FindControl("num_calcolo")
                    Dim idLabel As Label = listCommissioniStazione.Items(i).FindControl("idLabel")
                    Dim data_rientro As Label = listCommissioniStazione.Items(i).FindControl("data_rientro")
                    Dim nazione As Label = listCommissioniStazione.Items(i).FindControl("nazione")
                    Dim imponibile_totale_originale As Label = listCommissioniStazione.Items(i).FindControl("imponibile_totale_originale")
                    Dim imponibile_commissione_originale As Label = listCommissioniStazione.Items(i).FindControl("imponibile_commissione_originale")
                    Dim imponibile_parziale_originale As Label = listCommissioniStazione.Items(i).FindControl("imponibile_parziale_originale")
                    Dim imponibile_netto_originale As Label = listCommissioniStazione.Items(i).FindControl("imponibile_netto_originale")

                    sqlStr = "INSERT INTO commissioni_stazione_appoggio (codice_stampa, id_contratto, num_calcolo, contratto, data_rientro, imponibile_contratto, imponibile_parziale, imponibile_netto, commissione, nazione) VALUES (" &
                        codice_stampa & "," & idLabel.Text & "," & num_calcolo.Text & ",'" & nco & "','" & data_rientro.Text & "'," & imponibile_totale_originale.Text.Replace(",", ".") & "," &
                        imponibile_parziale_originale.Text.Replace(",", ".") & "," & imponibile_netto_originale.Text.Replace(",", ".") & "," & imponibile_commissione_originale.Text.Replace(",", ".") &
                        ",'" & nazione.Text & "')"

                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.ExecuteNonQuery()
                Next

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                Dim condizione As String = condizione_where()

                'If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                '    Session("url_print") = "/stampe/contratti/commissioni_stazione.aspx?orientamento=verticale&query=" & Server.UrlEncode(condizione) & "&percentuale_percentuale=" & txtPercentualeCommissione.Text & "&percentuale_imponibile=" & txtPercentualeImponibile.Text & "&header_html=/stampe/contratti/header_commissioni_stazione.aspx?valore=" & Server.UrlEncode(dropCercaStazionePickUp.SelectedItem.Text.Replace(" ", "-") & "-" & txtCercaDropOffDa.Text & "-" & txtCercaDropOffA.Text & "--Percentuale-imponibile:-" & txtPercentualeImponibile.Text & "%--Percentuale-commissione:-" & txtPercentualeCommissione.Text & "%")
                '    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                '        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                '    End If
                'End If

                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    Session("url_print") = "/stampe/contratti/commissioni_stazione_new.aspx?orientamento=verticale&query=" & codice_stampa & "&percentuale_percentuale=" & txtPercentualeCommissione.Text & "&percentuale_imponibile=" & txtPercentualeImponibile.Text & "&header_html=/stampe/contratti/header_commissioni_stazione.aspx?valore=" & Server.UrlEncode(dropCercaStazionePickUp.SelectedItem.Text.Replace(" ", "-") & "-" & txtCercaDropOffDa.Text & "-" & txtCercaDropOffA.Text & "--Percentuale-imponibile:-" & txtPercentualeImponibile.Text & "%--Percentuale-commissione:-" & txtPercentualeCommissione.Text & "%")
                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                    End If
                End If
            Else
                Libreria.genUserMsgBox(Me, controllo_filtri)
            End If
        Catch ex As Exception
            Libreria.genUserMsgBox(Me, ex.Message)
        End Try

    End Sub



    Protected Sub btnStampaRoyalty_Click(sender As Object, e As System.EventArgs) Handles btnStampaRoyalty.Click

        Dim controllo_filtri As String = controlla_filtri_royalty()

        Dim sqlstr As String = ""

        Dim condizione As String = ""

        Dim stazione_rientro As String = dropCercaStazionePickUp.SelectedValue


        Dim da_data As String = funzioni_comuni.GetDataSql(txtCercaDropOffDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaDropOffDa.Text)
        Dim a_data As String = funzioni_comuni.GetDataSql(txtCercaDropOffA.Text, 59) ' funzioni_comuni.getDataDb_con_orario(txtCercaDropOffA.Text & " 23:59:59")

        Dim condizione_where As String = " AND (contratti.id_stazione_uscita=" & dropCercaStazionePickUp.SelectedValue & ") AND (contratti.data_rientro BETWEEN Convert(DateTime, '" & da_data & "', 102) AND CONVERT(DATETIME, '" & a_data & "', 102)) "


        Dim sta As String = stazione_rientro
        Dim m As String = ddl_mese.SelectedValue
        Dim y As String = ddl_anno.SelectedValue
        Dim mese As String = ddl_mese.SelectedItem.Text
        Dim apt As String = dropCercaStazionePickUp.SelectedItem.Text




        If controllo_filtri = "" Then


            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                Session("condizione_royalties") = condizione_where

                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('stampaReportRoyalties.aspx?sta=" & sta & "&mese=" & mese & "&apt=" & apt & "&m=" & m & "&y=" & y & "','')", True)
                End If
            End If

        Else
            Libreria.genUserMsgBox(Me, controllo_filtri)
        End If
    End Sub











    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click

        Dim controllo_filtri As String = controlla_filtri()
        Dim sql As String = "SELECT id, num_contratto, data_rientro, conducenti.nazione, contratti.num_calcolo, "
        sql += "0 As imponibile_commissione, 0 As imponibile_totale "
        sql += "FROM contratti INNER JOIN conducenti WITH(NOLOCK) ON contratti.id_primo_conducente=conducenti.id_conducente WHERE (attivo=1) AND (status=4 OR status=6 OR status=8) " & condizione_where() & " ORDER BY contratti.data_rientro, num_contratto "


        If controllo_filtri = "" Then
            sqlCommissioniStazione.SelectCommand = sql '"SELECT id, num_contratto, data_rientro, conducenti.nazione, contratti.num_calcolo, " & _
            '"0 As imponibile_commissione, " & _
            '"0 As imponibile_totale " & _
            '"FROM contratti INNER JOIN conducenti WITH(NOLOCK) ON contratti.id_primo_conducente=conducenti.id_conducente WHERE (attivo=1) AND (status=4 OR status=6 OR status=8) " & condizione_where() & " ORDER BY contratti.data_rientro"

            Try
                lblCommissioni.Text = 0
                listCommissioniStazione.DataBind()
            Catch ex As Exception
                Response.Write("Error_btnCerca_click_:" & ex.Message & "<br/>")
            End Try

        Else
            Libreria.genUserMsgBox(Me, controllo_filtri)
        End If
    End Sub

    Protected Sub listCommissioniStazione_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listCommissioniStazione.ItemDataBound
        Dim id_contratto As Label = e.Item.FindControl("idLabel")
        Dim imponibile_commissione As Label = e.Item.FindControl("imponibile_commissione")
        Dim num_calcolo As Label = e.Item.FindControl("num_calcolo")
        Dim imponibile_totale As Label = e.Item.FindControl("imponibile_totale")

        Dim num_contratto As Label = e.Item.FindControl("num_contratto") 'aggiunto salvo 11.01.2023


        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim sqlStr As String = "SELECT SUM(imponibile_scontato) FROM contratti_costi WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON contratti_costi.id_elemento=condizioni_elementi.id " & _
           "WHERE contratti_costi.id_documento=" & id_contratto.Text & " AND contratti_costi.num_calcolo=" & num_calcolo.Text & " AND condizioni_elementi.commissione_stazione=1 AND selezionato=1 AND ISNULL(omaggiato,0)=0"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        imponibile_commissione.Text = Cmd.ExecuteScalar & ""

        sqlStr = "(SELECT contratti_costi.imponibile_scontato FROM contratti_costi WITH(NOLOCK) " & _
            "WHERE contratti_costi.id_documento=" & id_contratto.Text & " AND contratti_costi.num_calcolo=" & num_calcolo.Text & " AND contratti_costi.nome_costo='TOTALE')"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        imponibile_totale.Text = Cmd.ExecuteScalar & ""

        If imponibile_totale.Text = "" Then
            imponibile_totale.Text = "0"
        End If

        Dbc.Close()
        Dbc = Nothing

        Dim data_rientro As Label = e.Item.FindControl("data_rientro")


        Dim imponibile_parziale As Label = e.Item.FindControl("imponibile_parziale")
        Dim imponibile_netto As Label = e.Item.FindControl("imponibile_netto")
        Dim imponibile_totale_originale As Label = e.Item.FindControl("imponibile_totale_originale")
        Dim imponibile_commissione_originale As Label = e.Item.FindControl("imponibile_commissione_originale")
        Dim imponibile_parziale_originale As Label = e.Item.FindControl("imponibile_parziale_originale")
        Dim imponibile_netto_originale As Label = e.Item.FindControl("imponibile_netto_originale")

        data_rientro.Text = Left(data_rientro.Text, 10)

        imponibile_totale_originale.Text = CDbl(imponibile_totale.Text)
        imponibile_totale.Text = FormatNumber(CDbl(imponibile_totale.Text), 2)

        If imponibile_commissione.Text = "" Then
            imponibile_commissione.Text = "0"
        End If


        lblCommissioni.Text = CDbl(lblCommissioni.Text) + ((CDbl(imponibile_commissione.Text) * txtPercentualeImponibile.Text) / 100) * txtPercentualeCommissione.Text / 100

        imponibile_parziale_originale.Text = CDbl(imponibile_commissione.Text)
        imponibile_netto_originale.Text = CDbl(imponibile_commissione.Text * txtPercentualeImponibile.Text) / 100
        imponibile_commissione_originale.Text = (((CDbl(imponibile_commissione.Text) * txtPercentualeImponibile.Text) / 100) * txtPercentualeCommissione.Text / 100)

        imponibile_parziale.Text = FormatNumber(imponibile_parziale_originale.Text, 2)
        imponibile_netto.Text = FormatNumber(imponibile_netto_originale.Text, 2)
        imponibile_commissione.Text = FormatNumber(imponibile_commissione_originale.Text, 2)

        'link per apertura in nuova pagina del contratto aggiunto salvo 11.01.2023

        Dim lnk As String = "<a href='contratti.aspx?nr=" & num_contratto.Text & "' target='_blank'>" & num_contratto.Text & "</a>"
        num_contratto.Text = lnk


    End Sub

    Protected Sub listCommissioniStazione_DataBound(sender As Object, e As System.EventArgs) Handles listCommissioniStazione.DataBound
        Try
            lblCommissioni.Text = FormatNumber(CDbl(lblCommissioni.Text), 2)

        Catch ex As Exception

        End Try

    End Sub
End Class
