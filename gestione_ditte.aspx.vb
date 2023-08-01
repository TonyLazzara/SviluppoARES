'Imports funzioni_comuni
Partial Class gestione_ditte
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

    'Protected Sub btnNuovaDitta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovaDitta.Click
    '    nuova_ditta.Visible = True
    '    risultati.Visible = False
    '    btnNuovaDitta.Visible = False

    '    txtRagioneSociale.Text = ""
    '    txtIndirizzo.Text = ""
    '    listComune.SelectedValue = "0"

    '    txtPartitaIva.Text = ""
    '    txtPartitaIvaEstera.Text = ""
    '    txtCodiceFiscale.Text = ""
    '    txtFax.Text = ""
    '    txtTelefono.Text = ""
    '    listSconto.SelectedValue = "0"
    '    listCategoria.SelectedValue = "0"
    '    checkFullCredit.Checked = False
    '    listStatoCliente.SelectedValue = "0"
    '    listProduttore.SelectedValue = "0"
    '    checkTourOperator.Checked = False
    '    listArticoloEsenzione.SelectedValue = "0"
    '    checkSenzaIva.Checked = False
    '    listModalitaPagamento.SelectedValue = "0"
    '    listPagamento.SelectedValue = "0"
    '    checkInvioEmail.Checked = False
    '    txtEmail.Text = ""
    '    checkInvioEmailCC.Checked = False
    '    txtEmailCC.Text = ""
    '    checkInvioEmailStatement.Checked = False
    '    txtEmailStatement.Text = ""
    '    radioSpedizioneFattura.ClearSelection()
    '    btnSalva.Text = "Salva ditta"
    'End Sub

    'Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click
    '    nuova_ditta.Visible = False
    '    risultati.Visible = False
    '    btnNuovaDitta.Visible = True
    'End Sub

    'Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerca.Click
    '    query.Text = "SELECT id_cliente, rag_soc, indirizzo, piva, piva_estera FROM ditte"
    '    If Trim(txtCercaDitta.Text) <> "" Then
    '        If IsNumeric(txtCercaDitta.Text) Then
    '            query.Text = query.Text & " WHERE piva LIKE '%" & txtCercaDitta.Text & "%' OR piva_estera LIKE '%" & txtCercaDitta.Text & "%'"
    '        Else
    '            query.Text = query.Text & " WHERE rag_soc LIKE '%" & txtCercaDitta.Text & "%'"
    '        End If
    '    End If
    '    sqlDitte.SelectCommand = query.Text
    '    listViewDitte.DataBind()
    '    risultati.Visible = True
    '    btnNuovaDitta.Visible = True
    '    nuova_ditta.Visible = False
    'End Sub

    'Protected Sub listViewDitte_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listViewDitte.ItemCommand
    '    If e.CommandName = "ModificaDitta" Then

    '        Dim id_riga As Label = e.Item.FindControl("idLabel")
    '        id_ditta.Text = id_riga.Text

    '        risultati.Visible = False
    '        btnNuovaDitta.Visible = True
    '        nuova_ditta.Visible = True

    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()

    '        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM ditte WHERE id_cliente=" & id_riga.Text & "", Dbc)
    '        Cmd.ExecuteNonQuery()

    '        Dim Rs As Data.SqlClient.SqlDataReader
    '        Rs = Cmd.ExecuteReader()
    '        Rs.Read()

    '        txtRagioneSociale.Text = Rs("rag_soc") & ""
    '        txtIndirizzo.Text = Rs("indirizzo") & ""
    '        If Rs("id_comune") & "" <> "" Then
    '            listComune.SelectedValue = Rs("id_comune") & ""
    '        Else
    '            listComune.SelectedValue = "0"
    '        End If
    '        txtPartitaIva.Text = Rs("piva") & ""
    '        txtPartitaIvaEstera.Text = Rs("piva_estera") & ""
    '        txtCodiceFiscale.Text = Rs("c_fis") & ""
    '        txtFax.Text = Rs("fax") & ""
    '        txtTelefono.Text = Rs("tel") & ""
    '        If Rs("id_sconto") & "" <> "" Then
    '            listSconto.SelectedValue = Rs("id_sconto") & ""
    '        Else
    '            listSconto.SelectedValue = "0"
    '        End If
    '        If Rs("categoria") & "" <> "" Then
    '            listCategoria.SelectedValue = Rs("categoria") & ""
    '        Else
    '            listCategoria.SelectedValue = "0"
    '        End If
    '        If Rs("full_credit") & "" = True Then
    '            checkFullCredit.Checked = True
    '        End If
    '        If Rs("stato_cli") & "" <> "" Then
    '            listStatoCliente.SelectedValue = Rs("stato_cli") & ""
    '        Else
    '            listStatoCliente.SelectedValue = "0"
    '        End If
    '        If Rs("produttore") & "" <> "" Then
    '            listProduttore.SelectedValue = Rs("produttore") & ""
    '        Else
    '            listProduttore.SelectedValue = "0"
    '        End If
    '        If Rs("tour_op") & "" = True Then
    '            checkTourOperator.Checked = True
    '        End If
    '        If Rs("art_es") & "" <> "" Then
    '            listArticoloEsenzione.SelectedValue = Rs("art_es") & ""
    '        Else
    '            listArticoloEsenzione.SelectedValue = "0"
    '        End If
    '        If Rs("s_iva") & "" = True Then
    '            checkSenzaIva.Checked = True
    '        End If
    '        If Rs("id_modpag") & "" <> "" Then
    '            listModalitaPagamento.SelectedValue = Rs("id_modpag") & ""
    '        Else
    '            listModalitaPagamento.SelectedValue = "0"
    '        End If
    '        If Rs("id_pagamento") & "" <> "" Then
    '            listPagamento.SelectedValue = Rs("id_pagamento") & ""
    '        Else
    '            listPagamento.SelectedValue = "0"
    '        End If
    '        'listConvenzioni.SelectedValue = Rs("id_convenzione") & ""
    '        If Rs("tipo_spedizione_fattura") & "" = "N" Then
    '            radioSpedizioneFattura.SelectedValue = "N"
    '        ElseIf Rs("tipo_spedizione_fattura") & "" = "M" Then
    '            radioSpedizioneFattura.SelectedValue = "M"
    '        ElseIf Rs("tipo_spedizione_fattura") & "" = "D" Then
    '            radioSpedizioneFattura.SelectedValue = "D"
    '        ElseIf Rs("tipo_spedizione_fattura") & "" = "P" Then
    '            radioSpedizioneFattura.SelectedValue = "P"
    '        End If
    '        If Rs("invio_mail") & "" <> "" Then
    '            checkInvioEmail.Checked = True
    '        End If
    '        txtEmail.Text = Rs("email") & ""
    '        If Rs("invio_email_cc") & "" <> "" Then
    '            checkInvioEmailCC.Checked = True
    '        End If
    '        txtEmailCC.Text = Rs("email_cc") & ""
    '        If Rs("invio_email_statement") & "" <> "" Then
    '            checkInvioEmailStatement.Checked = True
    '        End If
    '        txtEmailStatement.Text = Rs("email_statement") & ""

    '        btnSalva.Text = "Modifica ditta"

    '    ElseIf e.CommandName = "EliminaDitta" Then
    '        Dim id_riga As Label = e.Item.FindControl("idLabel")

    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()

    '        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM ditte WHERE id_cliente=" & id_riga.Text & "", Dbc)
    '        Cmd.ExecuteNonQuery()

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing

    '        UserMsgBox(Page, "Ditta eliminata correttamente")

    '        listViewDitte.DataBind()
    '    End If
    'End Sub

    'Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click

    '    Dim data_ultima_email As String
    '    Dim data_ultima_email_marketing As String

    '    If Request.ServerVariables("HTTP_HOST") = "sviluppo.sicilybycar.it" Then
    '        data_ultima_email = "2011-30-12"
    '        data_ultima_email_marketing = "2011-30-12"
    '    Else
    '        data_ultima_email = "2011-12-30"
    '        data_ultima_email_marketing = "2011-12-30"
    '    End If

    '    Dim fullcredit As String
    '    Dim tour_operator As String
    '    Dim senza_iva As String
    '    Dim invio_email As String
    '    Dim invio_email_cc As String
    '    Dim invio_email_statement As String

    '    If btnSalva.Text = "Salva ditta" Then

    '        If checkFullCredit.Checked = True Then
    '            fullcredit = "1"
    '        Else
    '            fullcredit = "0"
    '        End If
    '        If checkTourOperator.Checked = True Then
    '            tour_operator = "1"
    '        Else
    '            tour_operator = "0"
    '        End If
    '        If checkSenzaIva.Checked = True Then
    '            senza_iva = "1"
    '        Else
    '            senza_iva = "0"
    '        End If
    '        If checkInvioEmail.Checked = True Then
    '            invio_email = "1"
    '        Else
    '            invio_email = "0"
    '        End If
    '        If checkInvioEmailCC.Checked = True Then
    '            invio_email_cc = "1"
    '        Else
    '            invio_email_cc = "0"
    '        End If
    '        If checkInvioEmailStatement.Checked = True Then
    '            invio_email_statement = "1"
    '        Else
    '            invio_email_statement = "0"
    '        End If

    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()

    '        Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO ditte (codice_edp, rag_soc, indirizzo, id_comune, piva, piva_estera, c_fis, fax, tel, id_sconto, categoria, full_credit, stato_cli, produttore, tour_op, art_es, s_iva, id_modpag, id_pagamento, tipo_spedizione_fattura, invio_mail, email, invio_email_cc, email_cc, invio_email_statement, email_statement, ultimo_invio_email, ultimo_invio_email_marketing, id_cliente_tipologia) VALUES ('" & 0 & "', '" & Replace(txtRagioneSociale.Text, "'", "''") & "', '" & Replace(txtIndirizzo.Text, "'", "''") & "', '" & listComune.SelectedValue & "', '" & Replace(txtPartitaIva.Text, "'", "''") & "', '" & Replace(txtPartitaIvaEstera.Text, "'", "''") & "', '" & Replace(txtCodiceFiscale.Text, "'", "''") & "', '" & Replace(txtFax.Text, "'", "''") & "', '" & Replace(txtTelefono.Text, "'", "''") & "', '" & listSconto.SelectedValue & "', '" & listCategoria.SelectedValue & "', '" & fullcredit & "', '" & listStatoCliente.SelectedValue & "', '" & listProduttore.SelectedValue & "', '" & tour_operator & "', '" & listArticoloEsenzione.SelectedValue & "', '" & senza_iva & "', '" & listModalitaPagamento.SelectedValue & "', '" & listPagamento.SelectedValue & "', '" & radioSpedizioneFattura.SelectedValue & "', '" & invio_email & "', '" & Replace(txtEmail.Text, "'", "''") & "', '" & invio_email_cc & "', '" & Replace(txtEmailCC.Text, "'", "''") & "', '" & invio_email_statement & "', '" & Replace(txtEmailStatement.Text, "'", "''") & "', '" & data_ultima_email & "', '" & data_ultima_email_marketing & "', '" & 1 & "')", Dbc)
    '        Cmd.ExecuteNonQuery()

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing

    '        UserMsgBox(Page, "Ditta inserita correttamente")

    '        nuova_ditta.Visible = False
    '        cerca_ditta.Visible = True
    '        risultati.Visible = True
    '        btnNuovaDitta.Visible = True
    '        listViewDitte.DataBind()

    '    Else

    '        If checkFullCredit.Checked = True Then
    '            fullcredit = "1"
    '        Else
    '            fullcredit = "0"
    '        End If
    '        If checkTourOperator.Checked = True Then
    '            tour_operator = "1"
    '        Else
    '            tour_operator = "0"
    '        End If
    '        If checkSenzaIva.Checked = True Then
    '            senza_iva = "1"
    '        Else
    '            senza_iva = "0"
    '        End If
    '        If checkInvioEmail.Checked = True Then
    '            invio_email = "1"
    '        Else
    '            invio_email = "0"
    '        End If
    '        If checkInvioEmailCC.Checked = True Then
    '            invio_email_cc = "1"
    '        Else
    '            invio_email_cc = "0"
    '        End If
    '        If checkInvioEmailStatement.Checked = True Then
    '            invio_email_statement = "1"
    '        Else
    '            invio_email_statement = "0"
    '        End If

    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()

    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE ditte SET rag_soc = '" & Replace(txtRagioneSociale.Text, "'", "''") & "', indirizzo = '" & Replace(txtIndirizzo.Text, "'", "''") & "', id_comune = '" & listComune.SelectedValue & "', piva = '" & Replace(txtPartitaIva.Text, "'", "''") & "', piva_estera = '" & Replace(txtPartitaIvaEstera.Text, "'", "''") & "', c_fis = '" & Replace(txtCodiceFiscale.Text, "'", "''") & "', fax = '" & Replace(txtFax.Text, "'", "''") & "', tel = '" & Replace(txtTelefono.Text, "'", "''") & "', id_sconto = '" & listSconto.SelectedValue & "', categoria = '" & listCategoria.SelectedValue & "', full_credit = '" & fullcredit & "', stato_cli = '" & listStatoCliente.SelectedValue & "', produttore = '" & listProduttore.SelectedValue & "', tour_op = '" & tour_operator & "', art_es = '" & listArticoloEsenzione.SelectedValue & "', s_iva = '" & senza_iva & "', id_modpag = '" & listModalitaPagamento.SelectedValue & "', id_pagamento = '" & listPagamento.SelectedValue & "', tipo_spedizione_fattura = '" & radioSpedizioneFattura.SelectedValue & "', invio_mail = '" & invio_email & "', email = '" & Replace(txtEmail.Text, "'", "''") & "', invio_email_cc = '" & invio_email_cc & "', email_cc = '" & Replace(txtEmailCC.Text, "'", "''") & "', invio_email_statement = '" & invio_email_statement & "', email_statement = '" & Replace(txtEmailStatement.Text, "'", "''") & "', ultimo_invio_email = '" & data_ultima_email & "', ultimo_invio_email_marketing = '" & data_ultima_email_marketing & "' WHERE id_cliente = " & id_ditta.Text & "", Dbc)
    '        Cmd.ExecuteNonQuery()

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing

    '        UserMsgBox(Page, "Ditta modificata correttamente")

    '        nuova_ditta.Visible = False
    '        cerca_ditta.Visible = True
    '        risultati.Visible = True
    '        btnNuovaDitta.Visible = True
    '        listViewDitte.DataBind()

    '    End If
    'End Sub

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            If Not Page.IsPostBack() Then
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "117") = "1" Then
                    Response.Redirect("default.aspx")
                End If
            End If
        End If
    End Sub
End Class
