Imports funzioni_comuni

Partial Class parcoVeicoli2
    Inherits System.Web.UI.Page
    Dim funzioni As New funzioni_comuni

    Protected Sub AzzeraTab()
        'DatiGenerali.BackColor = Drawing.Color.Silver
        PanelDatiGenerali.Visible = False

        'Accessori.BackColor = Drawing.Color.Silver
        PanelAccessori.Visible = False

        'DatiAcquisto.BackColor = Drawing.Color.Silver
        PanelDatiAcquisto.Visible = False

        'DatiVendita.BackColor = Drawing.Color.Silver
        PanelDatiVendita.Visible = False

        'Assicurazione.BackColor = Drawing.Color.Silver
        PanelAssicurazione.Visible = False

        'Leasing.BackColor = Drawing.Color.Silver
        PanelLeasing.Visible = False

        'Manutenzione.BackColor = Drawing.Color.Silver
        PanelManutenzione.Visible = False
    End Sub

    Public Sub UserMsgBox(ByVal F As Object, ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sb = New StringBuilder()
            sb.Append(sMsg)
            For Each oFormObject In F.Controls
                If TypeOf oFormObject Is HtmlForm Then
                    Exit For
                End If
            Next
            oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DatiGenerali_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DatiGenerali.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 2) <> "1" Then
            AzzeraTab()
            'FillDatiCommerciali()
            PanelDatiGenerali.Visible = True
        Else
            UserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub Accessori_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Accessori.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 3) <> "1" Then
            AzzeraTab()
            'FillDatiCommerciali()
            PanelAccessori.Visible = True
        Else
            UserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub DatiAcquisto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DatiAcquisto.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 4) <> "1" Then
            AzzeraTab()
            'FillDatiCommerciali()
            PanelDatiAcquisto.Visible = True
        Else
            UserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub DatiVendita_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DatiVendita.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 5) <> "1" Then
            AzzeraTab()
            'FillDatiCommerciali()
            PanelDatiVendita.Visible = True
        Else
            UserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub Assicurazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Assicurazione.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 6) <> "1" Then
            AzzeraTab()
            'FillDatiCommerciali()
            PanelAssicurazione.Visible = True
        Else
            UserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub Leasing_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Leasing.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 7) <> "1" Then
            AzzeraTab()
            'FillDatiCommerciali()
            PanelLeasing.Visible = True
        Else
            UserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.Cookies("SicilyRentCar") Is Nothing Then
                Response.Redirect("LogIn.aspx")
            Else
                If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 2) <> "1" Then
                    AzzeraTab()
                    PanelDatiGenerali.Visible = True
                Else
                    Response.Redirect("default.aspx")
                End If
            End If

            txtQuery.Text = "SELECT veicoli.id, veicoli.targa, marche.descrizione As marca, modelli.descrizione As modello, alimentazione.descrizione As alimentazione, veicoli.colore, proprietari_veicoli.descrizione As proprietario FROM veicoli INNER JOIN modelli ON veicoli.id_modello = modelli.id INNER JOIN marche on modelli.id_marca = marche.id LEFT JOIN alimentazione ON veicoli.id_alimentazione = alimentazione.id LEFT JOIN proprietari_veicoli ON veicoli.id_proprietario = proprietari_veicoli.id WHERE id_marca>0"
        End If

        sqlVeicoli.SelectCommand = txtQuery.Text
        sqlVeicoli.DataBind()
    End Sub

    Protected Sub listVeicoli_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listVeicoli.ItemCommand
        If e.CommandName = "vedi" Then
            Dim auto As Label = e.Item.FindControl("idLabel")
            Response.Redirect("parcoVeicoli.aspx?veicolo=" & auto.Text)
        End If
    End Sub

    Protected Sub Manutenzione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Manutenzione.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 8) <> "1" Then
            AzzeraTab()
            PanelManutenzione.Visible = True
        Else
            UserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnNuovo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovo.Click
        Response.Redirect("parcoVeicoli.aspx")
    End Sub

    Protected Sub btnImportVeicoli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportVeicoli.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 20) <> "1" Then
            Response.Redirect("ImportVeicoli.aspx")
        Else
            UserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub dropCercaMarca_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropCercaMarca.SelectedIndexChanged
        dropCercaModello.Items.Clear()
        dropCercaModello.Items.Add("Seleziona...")
        dropCercaModello.Items(0).Value = 0
        dropCercaModello.DataBind()
    End Sub

    Protected Sub setQuery()
        If Trim(txtCercaTarga.Text) <> "" Then
            sqlVeicoli.SelectCommand = sqlVeicoli.SelectCommand & " AND veicoli.targa LIKE '%" & Trim(txtCercaTarga.Text) & "%'"
        End If

        If dropCercaMarca.SelectedValue > 0 Then
            sqlVeicoli.SelectCommand = sqlVeicoli.SelectCommand & " AND modelli.id_marca = '" & dropCercaMarca.SelectedValue & "'"
        End If

        If dropCercaModello.SelectedValue > 0 Then
            sqlVeicoli.SelectCommand = sqlVeicoli.SelectCommand & " AND veicoli.id_modello = '" & dropCercaModello.SelectedValue & "'"
        End If

        txtQuery.Text = sqlVeicoli.SelectCommand
        listVeicoli.DataBind()
    End Sub


    Protected Sub btnCercaVeicolo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCercaVeicolo.Click
        setQuery()
    End Sub
End Class
