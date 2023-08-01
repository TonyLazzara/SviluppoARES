'Imports funzioni_comuni
Partial Class gestione_conducenti
    Inherits System.Web.UI.Page

    'Dim funzioni As New funzioni_comuni

    'Public Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
    '    Dim sb As New StringBuilder()
    '    Dim oFormObject As System.Web.UI.Control = Nothing
    '    Try
    '        sMsg = sMsg.Replace("'", "\'")
    '        sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
    '        sMsg = sMsg.Replace(vbCrLf, "\n")
    '        sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
    '        sb = New StringBuilder()
    '        sb.Append(sMsg)
    '        For Each oFormObject In F.Controls
    '            If TypeOf oFormObject Is HtmlForm Then
    '                Exit For
    '            End If
    '        Next
    '        oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerca.Click
    '    query.Text = "SELECT id_conducente, nominativo, city, telefono, cell FROM conducenti"
    '    If Trim(txtCercaConducente.Text) <> "" Then
    '        query.Text = query.Text & " WHERE nominativo LIKE '%" & txtCercaConducente.Text & "%'"
    '    End If
    '    sqlConducenti.SelectCommand = query.Text
    '    listConducenti.DataBind()
    '    nuovo_conducente.Visible = False
    '    risultati.Visible = True
    '    btnNuovoConducente.Visible = True
    'End Sub

    'Protected Sub btnNuovoConducente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovoConducente.Click
    '    nuovo_conducente.Visible = True
    '    btnNuovoConducente.Visible = False
    '    risultati.Visible = False
    '    txtNome.Text = ""
    '    txtCognome.Text = ""
    '    txtIndirizzo.Text = ""
    '    txtCitta.Text = ""
    '    txtDataNascita.Text = ""
    '    txtLuogoNascita.Text = ""
    '    radioSessoM.Checked = False
    '    radioSessoF.Checked = False
    '    txtCodiceFiscale.Text = ""
    '    txtDomicilio.Text = ""
    '    txtTelefono.Text = ""
    '    txtFax.Text = ""
    '    txtCellulare.Text = ""
    '    txtEmail.Text = ""
    '    txtPatente.Text = ""
    '    txtScadenzaPatente.Text = ""
    '    txtDataRilascioPatente.Text = ""
    '    txtLuogoEmissionePatente.Text = ""
    '    txtAltriDocumenti.Text = ""
    '    btnSalva.Text = "Salva conducente"
    'End Sub

    'Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click
    '    nuovo_conducente.Visible = False
    '    btnNuovoConducente.Visible = True
    '    risultati.Visible = False
    'End Sub

    'Protected Sub listConducenti_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listConducenti.ItemCommand

    '    If e.CommandName = "ModificaConducente" Then
    '        Dim id_riga As Label = e.Item.FindControl("idLabel")
    '        id_conducente.Text = id_riga.Text
    '        risultati.Visible = False
    '        nuovo_conducente.Visible = True
    '        btnSalva.Text = "Modifica conducente"
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()

    '        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM conducenti WHERE id_conducente=" & id_riga.Text & "", Dbc)
    '        Cmd.ExecuteNonQuery()

    '        Dim Rs As Data.SqlClient.SqlDataReader
    '        Rs = Cmd.ExecuteReader()
    '        Rs.Read()
    '        txtNome.Text = Rs("nome") & ""
    '        txtCognome.Text = Rs("cognome") & ""
    '        txtIndirizzo.Text = Rs("indirizzo") & ""
    '        txtCitta.Text = Rs("city") & ""
    '        txtDataNascita.Text = Rs("data_nascita") & ""
    '        txtLuogoNascita.Text = Rs("luogo_nascita") & ""
    '        If Rs("sesso") & "" = "M" Then
    '            radioSessoM.Checked = True
    '            radioSessoF.Checked = False
    '        Else
    '            radioSessoF.Checked = True
    '            radioSessoM.Checked = False
    '        End If
    '        txtCodiceFiscale.Text = Rs("codfis") & ""
    '        txtDomicilio.Text = Rs("domicilio_locale") & ""
    '        txtTelefono.Text = Rs("telefono") & ""
    '        txtFax.Text = Rs("fax") & ""
    '        txtCellulare.Text = Rs("cell") & ""
    '        txtEmail.Text = Rs("email") & ""
    '        txtPatente.Text = Rs("patente") & ""
    '        If Rs("tipo_patente") & "" <> "" Then
    '            listTipoPatente.SelectedValue = Rs("tipo_patente")
    '        End If
    '        txtScadenzaPatente.Text = Rs("scadenza_patente") & ""
    '        txtDataRilascioPatente.Text = Rs("rilasciata") & ""
    '        txtLuogoEmissionePatente.Text = Rs("luogo_emissione") & ""
    '        txtAltriDocumenti.Text = Rs("altri_documenti") & ""
    '        If Rs("id_cliente_tipologia") & "" <> "" Then
    '            listConvenzioni.SelectedValue = Rs("id_cliente_tipologia")
    '        End If
    '        Rs.Close()
    '        Rs = Nothing

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing
    '    ElseIf e.CommandName = "EliminaConducente" Then
    '        Dim id_riga As Label = e.Item.FindControl("idLabel")
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()

    '        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM conducenti WHERE id_conducente=" & id_riga.Text & "", Dbc)
    '        Cmd.ExecuteNonQuery()

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing

    '        UserMsgBox(Page, "Conducente eliminato correttamente")

    '        listConducenti.DataBind()
    '    End If
    'End Sub

    'Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click

    '    If Request.ServerVariables("HTTP_HOST") = "sviluppo.sicilybycar.it" Then
    '        txtDataNascita.Text = Year(txtDataNascita.Text) & "-" & Day(txtDataNascita.Text) & "-" & Month(txtDataNascita.Text) & " 23:59:59"
    '    Else
    '        txtDataNascita.Text = Year(txtDataNascita.Text) & "-" & Month(txtDataNascita.Text) & "-" & Day(txtDataNascita.Text) & " 23:59:59"
    '    End If
    '    If Request.ServerVariables("HTTP_HOST") = "sviluppo.sicilybycar.it" Then
    '        txtScadenzaPatente.Text = Year(txtScadenzaPatente.Text) & "-" & Day(txtScadenzaPatente.Text) & "-" & Month(txtScadenzaPatente.Text) & " 23:59:59"
    '    Else
    '        txtScadenzaPatente.Text = Year(txtScadenzaPatente.Text) & "-" & Month(txtScadenzaPatente.Text) & "-" & Day(txtScadenzaPatente.Text) & " 23:59:59"
    '    End If
    '    If Request.ServerVariables("HTTP_HOST") = "sviluppo.sicilybycar.it" Then
    '        txtDataRilascioPatente.Text = Year(txtDataRilascioPatente.Text) & "-" & Day(txtDataRilascioPatente.Text) & "-" & Month(txtDataRilascioPatente.Text) & " 23:59:59"
    '    Else
    '        txtDataRilascioPatente.Text = Year(txtDataRilascioPatente.Text) & "-" & Month(txtDataRilascioPatente.Text) & "-" & Day(txtDataRilascioPatente.Text) & " 23:59:59"
    '    End If

    '    Dim gender As String
    '    Dim nomecompleto As String
    '    nomecompleto = txtNome.Text & " " & txtCognome.Text
    '    If radioSessoF.Checked = True Then
    '        gender = "F"
    '    Else
    '        gender = "M"
    '    End If

    '    If btnSalva.Text = "Modifica conducente" Then

    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()

    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE conducenti SET nome = '" & Replace(txtNome.Text, "'", "''") & "', cognome = '" & Replace(txtCognome.Text, "'", "''") & "', nominativo = '" & nomecompleto & "', indirizzo = '" & Replace(txtIndirizzo.Text, "'", "''") & "', id_comune = '" & listComune.SelectedValue & "', city = '" & Replace(txtCitta.Text, "'", "''") & "', id_nazione = '" & listNazione.SelectedValue & "', data_nascita = '" & Replace(txtDataNascita.Text, "'", "''") & "', luogo_nascita = '" & Replace(txtLuogoNascita.Text, "'", "''") & "', sesso = '" & gender & "', codfis = '" & Replace(txtCodiceFiscale.Text, "'", "''") & "', domicilio_locale = '" & Replace(txtDomicilio.Text, "'", "''") & "', telefono = '" & Replace(txtTelefono.Text, "'", "''") & "', fax = '" & Replace(txtFax.Text, "'", "''") & "', cell = '" & Replace(txtCellulare.Text, "'", "''") & "', email = '" & Replace(txtEmail.Text, "'", "''") & "', patente = '" & Replace(txtPatente.Text, "'", "''") & "', tipo_patente = '" & listTipoPatente.SelectedValue & "', scadenza_patente = '" & Replace(txtScadenzaPatente.Text, "'", "''") & "', rilasciata = '" & Replace(txtDataRilascioPatente.Text, "'", "''") & "', luogo_emissione = '" & Replace(txtLuogoEmissionePatente.Text, "'", "''") & "', altri_documenti = '" & Replace(txtAltriDocumenti.Text, "'", "''") & "' WHERE id_conducente = " & id_conducente.Text & "", Dbc)
    '        Cmd.ExecuteNonQuery()

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing

    '        UserMsgBox(Page, "Conducente modificato correttamente")

    '    Else

    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()

    '        Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO conducenti (nome, cognome, nominativo, indirizzo, id_comune, city, id_nazione, data_nascita, luogo_nascita, sesso, codfis, domicilio_locale, telefono, fax, cell, email, patente, tipo_patente, scadenza_patente, rilasciata, luogo_emissione, altri_documenti) VALUES ('" & Replace(txtNome.Text, "'", "''") & "','" & Replace(txtCognome.Text, "'", "''") & "','" & nomecompleto & "','" & Replace(txtIndirizzo.Text, "'", "''") & "','" & listComune.SelectedValue & "','" & Replace(txtCitta.Text, "'", "''") & "','" & listNazione.SelectedValue & "','" & Replace(txtDataNascita.Text, "'", "''") & "','" & Replace(txtLuogoNascita.Text, "'", "''") & "','" & gender & "','" & Replace(txtCodiceFiscale.Text, "'", "''") & "','" & Replace(txtDomicilio.Text, "'", "''") & "','" & Replace(txtTelefono.Text, "'", "''") & "','" & Replace(txtFax.Text, "'", "''") & "','" & Replace(txtCellulare.Text, "'", "''") & "','" & Replace(txtEmail.Text, "'", "''") & "','" & Replace(txtPatente.Text, "'", "''") & "','" & listTipoPatente.SelectedValue & "','" & Replace(txtScadenzaPatente.Text, "'", "''") & "','" & Replace(txtDataRilascioPatente.Text, "'", "''") & "','" & Replace(txtLuogoEmissionePatente.Text, "'", "''") & "','" & Replace(txtAltriDocumenti.Text, "'", "''") & "')", Dbc)
    '        Cmd.ExecuteNonQuery()

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing

    '        UserMsgBox(Page, "Conducente inserito correttamente")

    '    End If

    '    listConducenti.DataBind()
    '    nuovo_conducente.Visible = False
    '    risultati.Visible = True
    '    btnNuovoConducente.Visible = True
    'End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            If Request.Cookies("SicilyRentCar")("idUtente") = "" Then
                Response.Redirect("default.aspx")
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "116") = "1" Then
                Response.Redirect("default.aspx")
            End If
        End If
    End Sub
End Class
