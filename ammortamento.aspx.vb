Imports funzioni_comuni

Partial Class fondo_ammortamento
    Inherits System.Web.UI.Page

    Dim funzioni As New funzioni_comuni

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim accesso As String = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 34)

            If accesso = "1" Then
                Response.Redirect("default.aspx")
            ElseIf accesso = "2" Then
                btnAggiorna.Enabled = False
            End If

            txtPrimaQuery.Text = sqlVeicoliAmmortamento.SelectCommand
            txtQuery.Text = sqlVeicoliAmmortamento.SelectCommand

            txtValore.Text = "25"
        End If

        sqlVeicoliAmmortamento.SelectCommand = txtQuery.Text
    End Sub

    Protected Sub ammortamentoPercentuale()
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim idLabel As Label
        Dim ammortamento As Label
        Dim adeguato As CheckBox

        Dim sqlStr As String = ""

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        For i = 0 To listAutoAmmortamento.Items.Count - 1
            idLabel = listAutoAmmortamento.Items(i).FindControl("idLabel")
            ammortamento = listAutoAmmortamento.Items(i).FindControl("fondoPrevisto")
            adeguato = listAutoAmmortamento.Items(i).FindControl("chkAdeguato")
            sqlStr = "UPDATE veicoli SET fondo_ammortamento='" & Replace(FormatNumber(ammortamento.Text, 2, , , TriState.False), ",", ".") & "', data_ammortamento='" & Year(txtDataAmmortamento.Text) & "', ammortamento_adeguato='" & adeguato.Checked & "' WHERE id='" & idLabel.Text & "'"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()
        Next

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub ammortamentoPercentuale_ODL()
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Dbc1 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc1.Open()

        Dim sqlStr As String = "SELECT id, ISNULL(fondo_ammortamento,0) As fondo_ammortamento FROM veicoli WITH(NOLOCK) WHERE escludi_ammortamento='0'"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Dim Cmd1 As New Data.SqlClient.SqlCommand("", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Dim valore As Double

        Do While Rs.Read()
            sqlStr = "SELECT ISNULL(SUM(imponibile_acquisto),0) As tot_acquisto FROM fatture_acquisto_veicoli WITH(NOLOCK) WHERE id_veicolo='" & Rs("id") & "'"
            Cmd1 = New Data.SqlClient.SqlCommand(sqlStr, Dbc1)

            valore = Cmd1.ExecuteScalar

            valore = (valore * CDbl(txtValore.Text) / 100) + CDbl(Rs("fondo_ammortamento"))

            sqlStr = "UPDATE veicoli SET fondo_ammortamento='" & Replace(valore, ",", ".") & "', data_ammortamento='" & Year(txtDataAmmortamento.Text) & "' WHERE id='" & Rs("id") & "'"
            Cmd1 = New Data.SqlClient.SqlCommand(sqlStr, Dbc1)
            Cmd1.ExecuteNonQuery()
        Loop

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Cmd1.Dispose()
        Cmd1 = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
        Dbc1.Close()
        Dbc1.Dispose()
        Dbc1 = Nothing
    End Sub

    Protected Function ammortamentoNonCalcolato() As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "SELECT TOP(1) data_ammortamento FROM veicoli WITH(NOLOCK) WHERE CAST(ISNULL(data_ammortamento,'0') As Int) >=" & Year(txtDataAmmortamento.Text) & ""

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Dim test As String = Cmd.ExecuteScalar

        If test = "" Then
            ammortamentoNonCalcolato = True
        Else
            ammortamentoNonCalcolato = False
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub btnAggiorna_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAggiorna.Click
        If txtDataAmmortamento.Text <> "" And txtValore.Text <> "" Then

            Dim test As Double

            Try
                test = CDbl(txtValore.Text)
                If test < 0 Or test > 100 Then
                    test = -1
                End If
            Catch ex As Exception
                test = -1
            End Try

            If test <> -1 Then

                If Day(txtDataAmmortamento.Text) = "31" And Month(txtDataAmmortamento.Text) = "12" Then
                    'L'AMMORTAMENTO PUO' ESSERE CALCOLATO SOLAMENTE ALL'ULTIMO GIORNO DI OGNI ANNO E SOLAMENTE UNA VOLTA L'ANNO
                    If True Then 'If ammortamentoNonCalcolato() Then
                        ammortamentoPercentuale()
                        ricercaNoFiltri()

                        Libreria.genUserMsgBox(Me, "Operazione effettuata correttamente.")
                    Else
                        Libreria.genUserMsgBox(Me, "Ammortamento già calcolato per l'anno specificato")
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "L'ammortamento può essere calcolato solamente all'ultimo giorno dell'anno.")
                End If
            Else
                Libreria.genUserMsgBox(Me, "Specificare correttamente il campo 'Valore ammortamento'")
            End If
        Else
            Libreria.genUserMsgBox(Me, "Specificare i campi 'Data ammortamento' e 'Valore ammortamento'")
        End If
    End Sub

    Protected Sub listAutoAmmortamento_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listAutoAmmortamento.ItemDataBound
        Dim idVeicolo As Label = e.Item.FindControl("idLabel")
        Dim numFattura As Label = e.Item.FindControl("lblNFattAcq")
        Dim dataFattura As Label = e.Item.FindControl("dataFattAcq")

        Dim fondoPrevisto As Label = e.Item.FindControl("fondoPrevisto")
        Dim ammPrevisto As Label = e.Item.FindControl("ammPrevisto")

        Dim fondoAmmortamento As Label = e.Item.FindControl("fondo_ammortamentoLabel")
        Dim leasing As Label = e.Item.FindControl("leasing")
        'Dim chkLeasing As CheckBox = e.Item.FindControl("chkLeasing")

        Dim chkAdeguato As CheckBox = e.Item.FindControl("chkAdeguato")

        Dim totFatture As Label = e.Item.FindControl("lblTotFatt")
        Dim totNC As Label = e.Item.FindControl("lblTotNC")
        Dim totAcquisto As Label = e.Item.FindControl("lblTotAcq")

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        'DATI PRIMA FATTURA ACQUISTO-------------------------------------------------------------------------------------------------------

        Dim sqlStr As String = "SELECT TOP(1) CONVERT(Char(10),data_acquisto,105) As data_acquisto, num_acquisto FROM fatture_acquisto_veicoli WITH(NOLOCK) WHERE id_veicolo='" & idVeicolo.Text & "' AND tipo='FATTURA' ORDER BY data_acquisto DESC"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Rs.Read()
        If Rs.HasRows Then
            numFattura.Text = Rs("num_acquisto") & ""
            dataFattura.Text = Rs("data_acquisto") & ""
        End If

        '-----------------------------------------------------------------------------------------------------------------------------------
        'TOTALE FATTURE-TOTALE NOTE DI CREDITO-TOTALE ACQUISTO -AMMORTAMENTO PREVISTO-------------------------------------------------------

        Dbc.Close()
        Dbc.Open()

        sqlStr = " SELECT (SELECT ISNULL(SUM(imponibile_acquisto),0) FROM fatture_acquisto_veicoli WITH(NOLOCK) WHERE id_veicolo='" & idVeicolo.Text & "' AND tipo='FATTURA') As totFatture, (SELECT ISNULL(SUM(imponibile_acquisto),0) FROM fatture_acquisto_veicoli WITH(NOLOCK) WHERE id_veicolo='" & idVeicolo.Text & "' AND tipo='NOTA') As totNC FROM fatture_acquisto_Veicoli WITH(NOLOCK) "
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Rs = Cmd.ExecuteReader()

        Rs.Read()
        If Rs.HasRows Then
            totFatture.Text = FormatNumber(Rs("totFatture"), 2)
            totNC.Text = FormatNumber(Rs("totNC") * -1, 2)
            totAcquisto.Text = FormatNumber(Rs("totFatture") + Rs("totNC"), 2)  'SUL DB IL CAMPO NOTA DI CREDITO E' NEGATIVO

            If Trim(txtValore.Text) <> "" Then
                ammPrevisto.Text = (CDbl(Rs("totFatture") + Rs("totNC")) * CDbl(txtValore.Text)) / 100

                If fondoAmmortamento.Text <> "" Then
                    fondoPrevisto.Text = CDbl(ammPrevisto.Text) + CDbl(fondoAmmortamento.Text)
                Else
                    fondoPrevisto.Text = CDbl(ammPrevisto.Text)
                End If

                'SE IL FONDO PREVISTO SUPERA IL PREZZO DI ACQUISTO ALLORA CORREGGO IL VALORE
                If CDbl(fondoPrevisto.Text) > CDbl(totAcquisto.Text) Then
                    ammPrevisto.Text = CDbl(totAcquisto.Text) - CDbl(fondoAmmortamento.Text)

                    fondoPrevisto.Text = totAcquisto.Text
                End If

                totaleFondoPrevisto.Text = CDbl(totaleFondoPrevisto.Text) + CDbl(fondoPrevisto.Text)

                totaleAmmPrevisto.Text = CDbl(ammPrevisto.Text) + CDbl(totaleAmmPrevisto.Text)
                ammPrevisto.Text = FormatNumber(ammPrevisto.Text, 2)

                fondoPrevisto.Text = FormatNumber(fondoPrevisto.Text, 2)

                If fondoPrevisto.Text = totAcquisto.Text Then
                    chkAdeguato.Checked = True
                End If

            End If

            totaleAcquisti.Text = CDbl(totaleAcquisti.Text) + Rs("totFatture") + Rs("totNC")
        End If

        '-----------------------------------------------------------------------------------------------------------------------------------
        'ALTRE OPERAZIONI-------------------------------------------------------------------------------------------------------------------
        If Trim(fondoAmmortamento.Text) <> "" Then
            totaleFondoAttuale.Text = CDbl(totaleFondoAttuale.Text) + CDbl(fondoAmmortamento.Text)

            fondoAmmortamento.Text = FormatNumber(fondoAmmortamento.Text, 2)
        Else
            fondoAmmortamento.Text = "0,00"
        End If

        totaleFondoAttuale.Text = CDbl(totaleFondoAttuale.Text)

        'If leasing.Text <> "SBC" And leasing.Text <> "" Then
        '    chkLeasing.Checked = True
        'End If

        '-----------------------------------------------------------------------------------------------------------------------------------

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub listAutoAmmortamento_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles listAutoAmmortamento.DataBound

        totaleAcquisti.Text = FormatNumber(totaleAcquisti.Text, 2)
        totaleFondoAttuale.Text = FormatNumber(totaleFondoAttuale.Text, 2)

        totaleFondoPrevisto.Text = FormatNumber(totaleFondoPrevisto.Text, 2)
        totaleAmmPrevisto.Text = FormatNumber(totaleAmmPrevisto.Text, 2)
    End Sub

    Protected Function getQuery() As String
        Dim query As String = " AND escludi_ammortamento='0' AND veicoli.auto_in_leasing='0' AND (venduta_da_fattura='0' OR (venduta_da_fattura='1' AND (SELECT top 1 ISNULL(YEAR(data_vendita),1000) FROM fatture_vendita_veicoli WITH(NOLOCK) WHERE fatture_vendita_veicoli.id_veicolo=veicoli.id AND tipo='FATTURA' ORDER BY data_vendita ASC) > '" & Year(txtDataAmmortamento.Text) & "' )) AND furto='0' " 'AND (veicoli.auto_in_leasing_SBC='0')
        'AND (venduta='0' OR (venduta='1' AND data_atto_vendita IS NULL))


        ' OR (venduta_da_fattura='1' AND (SELECT top 1 ISNULL(YEAR(data_vendita),1000) FROM fatture_vendita_veicoli WHERE fatture_vendita_veicoli.id_veicolo=veicoli.id AND tipo='FATTURA' ORDER BY data_vendita ASC) <= '" & Year(txtDataAmmortamento.Text) & "' )

        'AND escludi_ammortamento='0' AND veicoli.auto_in_leasing='0' AND (venduta_da_fattura='0' OR (venduta_da_fattura='1' AND ISNULL(YEAR(data_prima_fattura_vendita),5000) <='" & Year(txtDataAmmortamento.Text) & "')) AND furto='0'

        'If dropLeasing.SelectedValue > 0 Then
        '    query = query & " AND veicoli.id_proprietario='" & dropLeasing.SelectedValue & "' "
        'End If

        If acquistoDa.Text <> "" And acquistoA.Text = "" Then
            Dim data1 As String = funzioni_comuni.getDataDb_senza_orario(acquistoDa.Text)

            query = query & " AND veicoli.data_acquisto >= '" & data1 & "'"
        End If

        If acquistoDa.Text = "" And acquistoA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.getDataDb_senza_orario(acquistoA.Text)

            query = query & " AND veicoli.data_acquisto <= '" & data2 & "'"
        End If

        If acquistoDa.Text <> "" And acquistoA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.getDataDb_senza_orario(acquistoDa.Text)
            Dim data2 As String = funzioni_comuni.getDataDb_senza_orario(acquistoA.Text)

            query = query & " AND veicoli.data_acquisto BETWEEN '" & data1 & "' AND '" & data2 & "'"
        End If

        If immDa.Text <> "" And immA.Text = "" Then
            Dim data1 As String = funzioni_comuni.getDataDb_senza_orario(immDa.Text)

            query = query & " AND veicoli.data_immatricolazione >= '" & data1 & "'"
        End If

        If immDa.Text = "" And immA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.getDataDb_senza_orario(immA.Text)

            query = query & " AND veicoli.data_immatricolazione <= '" & data2 & "'"
        End If

        If immDa.Text <> "" And immA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.getDataDb_senza_orario(immDa.Text)
            Dim data2 As String = funzioni_comuni.getDataDb_senza_orario(immA.Text)

            query = query & " AND veicoli.data_immatricolazione BETWEEN '" & data1 & "' AND '" & data2 & "'"
        End If

        'PROPRIETARI----------------------
        If dropCercaProprietario.SelectedValue <> "0" Then
            query = query & " AND veicoli.id_proprietario='" & dropCercaProprietario.SelectedValue & "'"
        End If

        If dropCercaMarca.SelectedValue > 0 Then
            query = query & " AND modelli.id_CasaAutomobilistica = '" & dropCercaMarca.SelectedValue & "'"
        End If

        If dropCercaModello.SelectedValue > 0 Then
            query = query & " AND veicoli.id_modello = '" & dropCercaModello.SelectedValue & "'"
        End If

        getQuery = query

    End Function

    Protected Sub setQuery()
        sqlVeicoliAmmortamento.SelectCommand = txtPrimaQuery.Text

        'If dropLeasing.SelectedValue > 0 Then
        '    sqlVeicoliAmmortamento.SelectCommand = sqlVeicoliAmmortamento.SelectCommand & " AND veicoli.id_proprietario='" & dropLeasing.SelectedValue & "' "
        'End If

        'If acquistoDa.Text <> "" And acquistoA.Text = "" Then
        '    Dim data1 As String

        '    If Request.ServerVariables("HTTP_HOST") = "sviluppo.sicilybycar.it" Then
        '        data1 = Year(acquistoDa.Text) & "-" & Day(acquistoDa.Text) & "-" & Month(acquistoDa.Text)
        '    Else
        '        data1 = Year(acquistoDa.Text) & "-" & Month(acquistoDa.Text) & "-" & Day(acquistoDa.Text)
        '    End If


        '    sqlVeicoliAmmortamento.SelectCommand = sqlVeicoliAmmortamento.SelectCommand & " AND veicoli.data_acquisto >= '" & data1 & "'"
        'End If

        'If acquistoDa.Text = "" And acquistoA.Text <> "" Then
        '    Dim data2 As String

        '    If Request.ServerVariables("HTTP_HOST") = "sviluppo.sicilybycar.it" Then
        '        data2 = Year(acquistoA.Text) & "-" & Day(acquistoA.Text) & "-" & Month(acquistoA.Text)
        '    Else
        '        data2 = Year(acquistoA.Text) & "-" & Month(acquistoA.Text) & "-" & Day(acquistoA.Text)
        '    End If

        '    sqlVeicoliAmmortamento.SelectCommand = sqlVeicoliAmmortamento.SelectCommand & " AND veicoli.data_acquisto <= '" & data2 & "'"
        'End If

        'If acquistoDa.Text <> "" And acquistoA.Text <> "" Then
        '    Dim data1 As String
        '    Dim data2 As String

        '    If Request.ServerVariables("HTTP_HOST") = "sviluppo.sicilybycar.it" Then
        '        data1 = Year(acquistoDa.Text) & "-" & Day(acquistoDa.Text) & "-" & Month(acquistoDa.Text)
        '        data2 = Year(acquistoA.Text) & "-" & Day(acquistoA.Text) & "-" & Month(acquistoA.Text)
        '    Else
        '        data1 = Year(acquistoDa.Text) & "-" & Month(acquistoDa.Text) & "-" & Day(acquistoDa.Text)
        '        data2 = Year(acquistoA.Text) & "-" & Month(acquistoA.Text) & "-" & Day(acquistoA.Text)
        '    End If

        '    sqlVeicoliAmmortamento.SelectCommand = sqlVeicoliAmmortamento.SelectCommand & " AND veicoli.data_acquisto BETWEEN '" & data1 & "' AND '" & data2 & "'"
        'End If


        'If immDa.Text <> "" And immA.Text = "" Then
        '    Dim data1 As String

        '    If Request.ServerVariables("HTTP_HOST") = "sviluppo.sicilybycar.it" Then
        '        data1 = Year(immDa.Text) & "-" & Day(immDa.Text) & "-" & Month(immDa.Text)
        '    Else
        '        data1 = Year(immDa.Text) & "-" & Month(immDa.Text) & "-" & Day(immDa.Text)
        '    End If

        '    sqlVeicoliAmmortamento.SelectCommand = sqlVeicoliAmmortamento.SelectCommand & " AND veicoli.data_immatricolazione >= '" & data1 & "'"
        'End If

        'If immDa.Text = "" And immA.Text <> "" Then
        '    Dim data2 As String

        '    If Request.ServerVariables("HTTP_HOST") = "sviluppo.sicilybycar.it" Then
        '        data2 = Year(immA.Text) & "-" & Day(immA.Text) & "-" & Month(immA.Text)
        '    Else
        '        data2 = Year(immA.Text) & "-" & Month(immA.Text) & "-" & Day(immA.Text)
        '    End If

        '    sqlVeicoliAmmortamento.SelectCommand = sqlVeicoliAmmortamento.SelectCommand & " AND veicoli.data_immatricolazione <= '" & data2 & "'"
        'End If

        'If immDa.Text <> "" And immA.Text <> "" Then
        '    Dim data1 As String
        '    Dim data2 As String

        '    If Request.ServerVariables("HTTP_HOST") = "sviluppo.sicilybycar.it" Then
        '        data1 = Year(immDa.Text) & "-" & Day(immDa.Text) & "-" & Month(immDa.Text)
        '        data2 = Year(immA.Text) & "-" & Day(immA.Text) & "-" & Month(immA.Text)
        '    Else
        '        data1 = Year(immDa.Text) & "-" & Month(immDa.Text) & "-" & Day(immDa.Text)
        '        data2 = Year(immA.Text) & "-" & Month(immA.Text) & "-" & Day(immA.Text)
        '    End If


        '    sqlVeicoliAmmortamento.SelectCommand = sqlVeicoliAmmortamento.SelectCommand & " AND veicoli.data_immatricolazione BETWEEN '" & data1 & "' AND '" & data2 & "'"
        'End If

        sqlVeicoliAmmortamento.SelectCommand = sqlVeicoliAmmortamento.SelectCommand & getQuery() & " ORDER BY targa"

        'Response.Write(sqlVeicoliAmmortamento.SelectCommand)

        txtQuery.Text = sqlVeicoliAmmortamento.SelectCommand

        If listAutoAmmortamento.Visible Then
            'ESEGUO IL BIND SOLO SE LA LISTA E' GIA' VISIBILE, ALTRIMENTI IL BIND VIENE FATTO AUTOMATICAMENTE AL PASSAGGIO A VISIBLE='TRUE'
            listAutoAmmortamento.DataBind()
        End If

    End Sub

    Protected Sub ricercaNoFiltri()
        totaleAcquisti.Text = "0"
        totaleFondoAttuale.Text = "0"
        totaleFondoPrevisto.Text = "0"
        totaleAmmPrevisto.Text = "0"

        'acquistoA.Text = ""
        'acquistoDa.Text = ""
        'immDa.Text = ""
        'immA.Text = ""

        setQuery()

        RigaTotaleRicerca.Visible = True
        listAutoAmmortamento.Visible = True

    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRicerca.Click
        If txtDataAmmortamento.Text <> "" Then
            totaleAcquisti.Text = "0"
            totaleFondoAttuale.Text = "0"
            totaleFondoPrevisto.Text = "0"
            totaleAmmPrevisto.Text = "0"


            setQuery()

            RigaTotaleRicerca.Visible = True
            listAutoAmmortamento.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Specificare la data di ammortamento.")
        End If

    End Sub

    Protected Sub dropTipoAmmortamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropTipoAmmortamento.SelectedIndexChanged
        If dropTipoAmmortamento.SelectedValue = 0 Then
            txtValore.Text = "25"
            txtValore.ReadOnly = True
        ElseIf dropTipoAmmortamento.SelectedValue = 1 Then
            txtValore.Text = "12,5"
            txtValore.ReadOnly = True
        ElseIf dropTipoAmmortamento.SelectedValue = 2 Then
            txtValore.Text = "50"
            txtValore.ReadOnly = True
        Else
            txtValore.Text = ""
            txtValore.ReadOnly = False
        End If
    End Sub

    Protected Sub btnReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Dim test As Double
        Try
            test = CDbl(txtValore.Text)
            'If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            '    Session("valore") = txtValore.Text
            '    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "NewWindow", "window.open('/stampe/ammortamento_fisso.aspx','')", True)
            'End If
        Catch ex As Exception
            Libreria.genUserMsgBox(Me, "Specificare un valore corretto per il campo 'Valore'")
            test = -1
        End Try

        If test <> -1 Then
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                Session("url_print") = "/stampe/ammortamento.aspx?orientamento=verticale&valore=" & txtValore.Text & "&query=" & Server.UrlEncode(getQuery()) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&header_html=/stampe/header_ammortamento.aspx?valore=" & txtValore.Text
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                End If
            End If
        End If

    End Sub

    Protected Sub dropCercaMarca_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropCercaMarca.SelectedIndexChanged
        dropCercaModello.Items.Clear()
        dropCercaModello.Items.Add("Seleziona...")
        dropCercaModello.Items(0).Value = 0
        dropCercaModello.DataBind()
    End Sub
End Class
