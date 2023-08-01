
Partial Class gestioneAssicurazioni
    Inherits System.Web.UI.Page


    Protected Sub btnImportAssicurazioniVeicoli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportAssicurazioniVeicoli.Click
        Response.Redirect("ImportAssicurazioniVeicoli.aspx")
    End Sub

    Protected Function getQuery() As String


        getQuery = ""

        If dropStato.SelectedValue = "1" Then
            'ATTIVE: SELEZIONO LE RIGHE DI ASSICURAZIONE SENZA UNA ESCLUSIONE EFFETTUATA
            getQuery = getQuery & " AND veicoli_assicurazioni.data_esclusione IS NULL"
        ElseIf dropStato.SelectedValue = "2" Then
            'ESCLUSE
            getQuery = getQuery & " AND NOT veicoli_assicurazioni.data_esclusione IS NULL"
        End If

        If dropCompagnia.SelectedValue <> "0" Then
            getQuery = getQuery & " AND veicoli_assicurazioni.id_compagnia='" & dropCompagnia.SelectedValue & "'"
        End If

        If txtCercaTarga.Text <> "" Then
            getQuery = getQuery & " AND veicoli.targa='" & Replace(txtCercaTarga.Text, "'", "''") & "'"
        End If

        If dropProprietario.SelectedValue <> "0" Then
            getQuery = getQuery & " AND veicoli.id_proprietario='" & dropProprietario.SelectedValue & "'"
        End If

        If dropVenditaA.SelectedValue <> "0" Then
            getQuery = getQuery & " AND veicoli.id_vendita_a='" & dropVenditaA.SelectedValue & "'"
        End If

        If txtVenditaNumero.Text <> "" And txtVenditaDel.Text = "" Then
            getQuery = getQuery & " AND '" & Replace(txtVenditaNumero.Text, "'", "''") & "' IN (SELECT num_vendita FROM fatture_vendita_veicoli WITH(NOLOCK) WHERE id_veicolo= veicoli_assicurazioni.id_parco)"

        ElseIf txtVenditaNumero.Text = "" And txtVenditaDel.Text <> "" Then
            Dim data_vendita As String = funzioni_comuni.GetDataSql(txtVenditaDel.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtVenditaDel.Text)

            getQuery = getQuery & " AND '" & data_vendita & "' IN (SELECT data_vendita FROM fatture_vendita_veicoli WITH(NOLOCK) WHERE id_veicolo= veicoli_assicurazioni.id_parco)"
        ElseIf txtVenditaNumero.Text <> "" And txtVenditaDel.Text <> "" Then
            Dim data_vendita As String = funzioni_comuni.GetDataSql(txtVenditaDel.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtVenditaDel.Text)

            getQuery = getQuery & " AND '" & Replace(txtVenditaNumero.Text, "'", "''") & "' IN (SELECT num_vendita FROM fatture_vendita_veicoli WITH(NOLOCK) WHERE id_veicolo= veicoli_assicurazioni.id_parco AND data_vendita= CONVERT(DateTime,'" & data_vendita & "',102))"
        End If

        If txtOrdineDa.Text <> "" And txtOrdineA.Text = "" Then
            Dim ordine_da As Integer
            Try
                ordine_da = CInt(txtOrdineDa.Text)
            Catch ex As Exception
                ordine_da = -1
                Libreria.genUserMsgBox(Me, "Campo di ricerca N. Ordine Da non corretto.")
            End Try

            If ordine_da > -1 Then
                getQuery = getQuery & " AND ordine >=" & ordine_da
            End If
        ElseIf txtOrdineDa.Text = "" And txtOrdineA.Text <> "" Then
            Dim ordine_a As Integer
            Try
                ordine_a = CInt(txtOrdineA.Text)
            Catch ex As Exception
                ordine_a = -1
                Libreria.genUserMsgBox(Me, "Campo di ricerca N. Ordine A non corretto.")
            End Try

            If ordine_a > -1 Then
                getQuery = getQuery & " AND ordine <=" & ordine_a
            End If
        ElseIf txtOrdineDa.Text <> "" And txtOrdineA.Text <> "" Then
            Dim ordine_da As Integer
            Dim ordine_a As Integer
            Try
                ordine_da = CInt(txtOrdineDa.Text)
            Catch ex As Exception
                ordine_da = -1
                Libreria.genUserMsgBox(Me, "Campo di ricerca N. Ordine Da non corretto.")
            End Try
            Try
                ordine_a = CInt(txtOrdineA.Text)
            Catch ex As Exception
                ordine_a = -1
                Libreria.genUserMsgBox(Me, "Campo di ricerca N. Ordine A non corretto.")
            End Try

            If ordine_a > -1 And ordine_da > -1 Then
                getQuery = getQuery & " AND ordine BETWEEN " & ordine_da & " AND " & ordine_a
            End If
        End If
        Dim dtsql As String
        If txtDataInclusioneDa.Text <> "" Then
            'Dim inclusione_da As String = funzioni_comuni.getDataDb_senza_orario(txtDataInclusioneDa.Text)
            dtsql = funzioni_comuni.GetDataSql(txtDataInclusioneDa.Text, 0)
            getQuery = getQuery & " AND data_inclusione >= CONVERT(DATETIME,'" & dtsql & "',102)"
        End If

        If txtDataInclusioneA.Text <> "" Then
            'Dim inclusione_a As String = funzioni_comuni.getDataDb_senza_orario(txtDataInclusioneA.Text)
            dtsql = funzioni_comuni.GetDataSql(txtDataInclusioneA.Text, 59)
            getQuery = getQuery & " AND data_inclusione <= CONVERT(DATETIME,'" & dtsql & "',102)"
        End If

        If txtDataEsclusioneDa.Text <> "" Then
            'Dim esclusione_da As String = funzioni_comuni.getDataDb_senza_orario(txtDataEsclusioneDa.Text)
            dtsql = funzioni_comuni.GetDataSql(txtDataEsclusioneDa.Text, 0)
            getQuery = getQuery & " AND data_esclusione >= CONVERT(DATETIME,'" & dtsql & "',102)"
        End If

        If txtDataEsclusioneA.Text <> "" Then
            Dim esclusione_a As String = funzioni_comuni.getDataDb_senza_orario(txtDataEsclusioneA.Text)
            dtsql = funzioni_comuni.GetDataSql(txtDataEsclusioneA.Text, 59)
            getQuery = getQuery & " AND data_esclusione <= CONVERT(DATETIME,'" & dtsql & "',102)"
        End If
    End Function

    Protected Sub setQuery()

        lblQuery.Text = "SELECT veicoli_assicurazioni.id_parco, veicoli.targa, veicoli.id As id_veicolo, veicoli_assicurazioni.id, veicoli_assicurazioni.id_compagnia,  compagnie_assicurative.compagnia, compagnie_assicurative.giorni_annuali_x_calcolo, " &
            "veicoli_assicurazioni.ordine, CONVERT(char(10),veicoli_assicurazioni.data_inclusione,103) As data_inclusione, modelli.cavalli, compagnie_assicurative.percentuale_IF_x_1000, compagnie_assicurative.valore_moltiplicativo_tassa, " &
            "CONVERT(char(10),veicoli_assicurazioni.data_esclusione,103) As data_esclusione, " &
            "veicoli_assicurazioni.valore_I_F, (ISNULL(operatori1.cognome,'') + ' ' + ISNULL(operatori1.nome,'')) As inclusa_da, " &
            "veicoli_assicurazioni.inclusa_il, (ISNULL(operatori2.cognome,'') + ' ' + ISNULL(operatori2.nome,'')) AS esclusa_da, esclusa_il " &
            "FROM veicoli_assicurazioni WITH(NOLOCK) INNER JOIN operatori As operatori1 WITH(NOLOCK) ON veicoli_assicurazioni.inclusa_da=operatori1.id " &
            "LEFT JOIN operatori As operatori2 WITH(NOLOCK) ON veicoli_assicurazioni.esclusa_da=operatori2.id " &
            "INNER JOIN compagnie_assicurative WITH(NOLOCK) ON veicoli_assicurazioni.id_compagnia=compagnie_assicurative.id " &
            "INNER JOIN veicoli WITH(NOLOCK) ON veicoli_assicurazioni.id_parco=veicoli.id  " &
            "INNER JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello WHERE veicoli_assicurazioni.id<>'0' "

        lblQuery.Text = lblQuery.Text & getQuery()

        sqlRigheAssicurazioni.SelectCommand = lblQuery.Text & " ORDER BY data_inclusione DESC"
    End Sub

    Protected Sub btnCercaVeicolo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCercaVeicolo.Click
        setQuery()
        listRigheAssicurazioni.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim accesso As String = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 6)

            If accesso = "1" Then
                Response.Redirect("default.aspx")
                'ElseIf accesso = "2" Then
                '    btnImportAssicurazioniVeicoli.Visible = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 38) <> "3" Then
                btnImportAssicurazioniVeicoli.Enabled = False
            End If

            lblQuery.Text = "SELECT veicoli_assicurazioni.id_parco, veicoli.targa, veicoli_assicurazioni.id, veicoli_assicurazioni.id_compagnia,  compagnie_assicurative.compagnia, veicoli_assicurazioni.ordine, veicoli_assicurazioni.data_inclusione, veicoli_assicurazioni.data_esclusione, veicoli_assicurazioni.valore_I_F, (ISNULL(operatori1.cognome,'') + ' ' + ISNULL(operatori1.nome,'')) As inclusa_da, veicoli_assicurazioni.inclusa_il, (ISNULL(operatori2.cognome,'') + ' ' + ISNULL(operatori2.nome,'')) AS esclusa_da, esclusa_il FROM veicoli_assicurazioni WITH(NOLOCK) INNER JOIN operatori As operatori1 WITH(NOLOCK) ON veicoli_assicurazioni.inclusa_da=operatori1.id LEFT JOIN operatori As operatori2 WITH(NOLOCK) ON veicoli_assicurazioni.esclusa_da=operatori2.id INNER JOIN compagnie_assicurative WITH(NOLOCK) ON veicoli_assicurazioni.id_compagnia=compagnie_assicurative.id INNER JOIN veicoli WITH(NOLOCK) ON veicoli_assicurazioni.id_parco=veicoli.id WHERE veicoli_assicurazioni.id='0'"
        End If

        sqlRigheAssicurazioni.SelectCommand = lblQuery.Text & " ORDER BY data_inclusione DESC"
    End Sub

    Protected Sub btnReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReport.Click
        If dropReport.SelectedValue = "0" Then
            'LISTA ASSICURAZIONI

            Session("url_print") = "stampe/assicurazioni/lista_assicurazioni.aspx?orientamento=orizzontale&query=" & Server.UrlEncode(getQuery()) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente")

            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('/stampe/assicurazioni/lista_assicurazioni.aspx','')", True)
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
            End If
        ElseIf dropReport.SelectedValue = "1" Then
            If txtDataEsclusioneRiferimento.Text <> "" Then
                Session("url_print") = "stampe/assicurazioni/data_esclusione_riferimento.aspx?orientamento=verticale&query=" & Server.UrlEncode(getQuery()) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&escl=" & txtDataEsclusioneRiferimento.Text

                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('/stampe/assicurazioni/lista_assicurazioni.aspx','')", True)
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                End If
            Else
                Libreria.genUserMsgBox(Me, "Specificare la data di esclusione di riferimento.")
            End If
        End If
    End Sub

    Protected Sub dropReport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropReport.SelectedIndexChanged
        If dropReport.SelectedValue = "0" Then
            txtDataEsclusioneRiferimento.Visible = False
        ElseIf dropReport.SelectedValue = "1" Then
            txtDataEsclusioneRiferimento.Visible = True
        End If
    End Sub

    Protected Sub listRigheAssicurazioni_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listRigheAssicurazioni.ItemDataBound

        Dim data_esclusione As Label = e.Item.FindControl("data_esclusione")
        Dim data_inclusione As Label = e.Item.FindControl("data_inclusione")
        Dim valore_I_F As Label = e.Item.FindControl("valore_I_F")
        Dim id_compagnia As Label = e.Item.FindControl("id_compagnia")
        Dim id_veicolo As Label = e.Item.FindControl("id_veicolo")
        Dim giorni_annuali_x_calcolo As Label = e.Item.FindControl("giorni_annuali_x_calcolo")
        Dim cavalli As Label = e.Item.FindControl("cavalli")
        Dim percentuale_IF_x_1000 As Label = e.Item.FindControl("percentuale_IF_x_1000")
        Dim valore_moltiplicativo_tassa As Label = e.Item.FindControl("valore_moltiplicativo_tassa")

        If valore_moltiplicativo_tassa.Text = "" Then
            valore_moltiplicativo_tassa.Text = "1"
        End If

        Dim totale_assicurazione As Label = e.Item.FindControl("totale_assicurazione")
        Dim tot_giorni As Label = e.Item.FindControl("tot_giorni")
        Dim totale_I_F As Label = e.Item.FindControl("totale_I_F")
        Dim totale_rca As Label = e.Item.FindControl("totale_rca")

        Dim giorni_annuali As Integer
        If giorni_annuali_x_calcolo.Text = "" Then
            giorni_annuali = "365"
        Else
            giorni_annuali = CInt(giorni_annuali_x_calcolo.Text)
        End If

        Dim giorni As Integer

        Try
            If data_esclusione.Text <> "" And valore_I_F.Text <> "" Then
                Dim data_escl As DateTime = funzioni_comuni.getDataDb_senza_orario2(data_esclusione.Text)
                Dim data_incl As DateTime = funzioni_comuni.getDataDb_senza_orario2(data_inclusione.Text)

                giorni = DateDiff(DateInterval.Day, data_incl, data_escl)

                Dim sqlStr As String = "SELECT ISNULL(compagnie_assicurative_costi.costo,0) FROM compagnie_assicurative_costi WITH(NOLOCK) WHERE " &
                             "(" & cavalli.Text & " BETWEEN da AND a) " &
                             " AND (compagnie_assicurative_costi.id_compagnia_assicurativa=" & id_compagnia.Text & ")"

                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                Dim costoRCA_annuale As Double = Cmd.ExecuteScalar

                Dim costoRCA As Double = (costoRCA_annuale / giorni_annuali * giorni) + ((CDbl(valore_I_F.Text) / 1000 * CDbl(percentuale_IF_x_1000.Text)) / giorni_annuali * giorni) + (CDbl(valore_moltiplicativo_tassa.Text) * ((CDbl(valore_I_F.Text) / 1000 * CDbl(percentuale_IF_x_1000.Text)) / giorni_annuali * giorni) / 100)
                'RESTANO DA AGGIUNGERE LE TASSE ALL'INCENDIO E FURTO
                'totale_assicurazione.Text = giorni & " " & giorni_annuali & " " & costoRCA
                tot_giorni.Text = giorni
                totale_rca.Text = FormatNumber((costoRCA_annuale / giorni_annuali * giorni), 2)
                totale_I_F.Text = FormatNumber(((CDbl(valore_I_F.Text) / 1000 * CDbl(percentuale_IF_x_1000.Text)) / giorni_annuali * giorni) + (CDbl(valore_moltiplicativo_tassa.Text) * ((CDbl(valore_I_F.Text) / 1000 * CDbl(percentuale_IF_x_1000.Text)) / giorni_annuali * giorni) / 100), 2)
                totale_assicurazione.Text = FormatNumber(costoRCA, 2)

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

            End If
        Catch ex As Exception
            Response.Write("Error listRigheAssicurazioni_ItemDataBound : " & ex.Message & "<br/>")
        End Try





    End Sub
    Protected Sub btncancellacampi_Click(sender As Object, e As EventArgs)
        Response.Redirect("gestioneAssicurazioni.aspx")
    End Sub
End Class
