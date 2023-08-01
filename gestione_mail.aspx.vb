Imports System.Net.Mail

Partial Class gestione_mail
    Inherits System.Web.UI.Page

    Private Enum DivVisibile
        Nessuno = 0
        Ricerca = 1
        ElencoTipoMail = 2
        ModificaTipoMail = 4
        ElencoMail = 8
        ModificaMail = 16
    End Enum

    Private Sub Visibilita(Valore As DivVisibile)
        Trace.Write("Visibilita: " & Valore.ToString)

        div_intestazione_tipo_mail.Visible = Valore And (DivVisibile.ElencoTipoMail Or DivVisibile.ModificaTipoMail)

        div_intestazione_mail.Visible = Valore And (DivVisibile.ElencoMail Or DivVisibile.ModificaMail Or DivVisibile.Ricerca)

        div_modifica_tipo_mail.Visible = Valore And DivVisibile.ModificaTipoMail

        div_elenco_tipo_mail.Visible = Valore And DivVisibile.ElencoTipoMail

        div_cerca_mail.Visible = Valore And DivVisibile.Ricerca

        div_modifica_mail.Visible = Valore And DivVisibile.ModificaMail

        div_elenco_mail.Visible = Valore And DivVisibile.ElencoMail
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Trace.Write("Page_Load --------- gestione_mail")

        If Not Page.IsPostBack Then
            DropDown_tipo_mail_DataBind()

            Visibilita(DivVisibile.Ricerca)
        End If
    End Sub

    Private Sub AzzeraTipoMail()
        Dim mio_record As mail_tipo = New mail_tipo
        With mio_record
            .id = 0
            .descrizione = ""
        End With

        FillTipoMail(mio_record)
    End Sub

    Private Sub FillTipoMail(mio_record As mail_tipo)
        With mio_record
            lb_id_tipo_mail.Text = .id
            tx_descrizione.Text = .descrizione
        End With
    End Sub

    Private Sub AzzeraMail()
        Dim mio_record As mail_destinatari = New mail_destinatari
        With mio_record
            .id = 0
            .id_tipo_invio = 0
            .mail = ""
            .nome = ""
        End With

        FillMail(mio_record)
    End Sub

    Private Sub FillMail(mio_record As mail_destinatari)
        With mio_record
            lb_id_mail.Text = .id
            tx_mail.Text = .mail
            tx_nome.Text = .nome
            DropDownTipoInvio.SelectedValue = .id_tipo_invio
        End With
    End Sub

    Protected Sub bt_elenco_tipo_mail_Click(sender As Object, e As System.EventArgs) Handles bt_elenco_tipo_mail.Click
        lb_stato_edit.Text = "0"

        listView_elenco_tipo_mail.DataBind()

        Visibilita(DivVisibile.ElencoTipoMail)
    End Sub

    Protected Sub bt_chiudi_form_Click(sender As Object, e As System.EventArgs) Handles bt_chiudi_form.Click
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub bt_cerca_mail_Click(sender As Object, e As System.EventArgs) Handles bt_cerca_mail.Click
        lb_id_tipo_mail.Text = DropDown_tipo_mail.SelectedValue
        lb_descrizione_tipo_mail.Text = DropDown_tipo_mail.SelectedItem.Text

        listView_elenco_mail.DataBind()

        Visibilita(DivVisibile.ElencoMail)
    End Sub

    Protected Sub Aggiorna_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Aggiorna.Click
        lb_stato_edit.Text = 1

        AzzeraTipoMail()

        Visibilita(DivVisibile.ModificaTipoMail)
    End Sub

    Protected Sub bt_nuova_mail_Click(sender As Object, e As System.EventArgs) Handles bt_nuova_mail.Click
        AzzeraMail()

        Visibilita(DivVisibile.ModificaMail)
    End Sub

    Protected Sub bt_chiudi_elenco_mail_Click(sender As Object, e As System.EventArgs) Handles bt_chiudi_elenco_mail.Click
        Visibilita(DivVisibile.Ricerca)
    End Sub

    Protected Sub bt_chiudi_mail_Click(sender As Object, e As System.EventArgs) Handles bt_chiudi_mail.Click
        Visibilita(DivVisibile.ElencoMail)
    End Sub

    Protected Sub bt_salva_mail_Click(sender As Object, e As System.EventArgs) Handles bt_salva_mail.Click
        Dim id As Integer = Integer.Parse(lb_id_mail.Text)
        Dim mio_record As mail_destinatari = New mail_destinatari
        With mio_record
            .id = id
            .id_mail = Integer.Parse(lb_id_tipo_mail.Text)
            .id_tipo_invio = Integer.Parse(DropDownTipoInvio.SelectedValue)
            .mail = tx_mail.Text
            .nome = tx_nome.Text

            If id = 0 Then
                .SalvaRecord()
            Else
                .UpdateRecord()
            End If
        End With

        Libreria.genUserMsgBox(Me, "Mail salvata correttamente.")

        listView_elenco_mail.DataBind()

        Visibilita(DivVisibile.ElencoMail)
    End Sub

    Protected Sub bt_chiudi_tipo_mail_Click(sender As Object, e As System.EventArgs) Handles bt_chiudi_tipo_mail.Click
        Visibilita(DivVisibile.Ricerca)
    End Sub

    Protected Sub bt_nuovo_tipo_mail_Click(sender As Object, e As System.EventArgs) Handles bt_nuovo_tipo_mail.Click
        lb_stato_edit.Text = 0

        AzzeraTipoMail()

        Visibilita(DivVisibile.ModificaTipoMail)
    End Sub

    Protected Sub bt_chiudi_modifica_tipo_mail_Click(sender As Object, e As System.EventArgs) Handles bt_chiudi_modifica_tipo_mail.Click
        If lb_stato_edit.Text = "0" Then
            Visibilita(DivVisibile.ElencoTipoMail)
        Else
            DropDown_tipo_mail_DataBind()

            Visibilita(DivVisibile.Ricerca)
        End If
    End Sub

    Protected Sub DropDown_tipo_mail_DataBind()
        DropDown_tipo_mail.Items.Clear()

        DropDown_tipo_mail.Items.Add(New ListItem("Seleziona...", "0"))
        DropDown_tipo_mail.DataBind()
    End Sub

    Protected Sub bt_salva_modifica_tipo_mail_Click(sender As Object, e As System.EventArgs) Handles bt_salva_modifica_tipo_mail.Click
        Dim id As Integer = Integer.Parse(lb_id_tipo_mail.Text)
        Dim mio_record As mail_tipo = New mail_tipo
        With mio_record
            .id = id
            .descrizione = tx_descrizione.Text
            
            If id = 0 Then
                lb_id_tipo_mail.Text = .SalvaRecord(True)
            Else
                .UpdateRecord()
            End If
        End With

        Libreria.genUserMsgBox(Me, "Tipo Mail salvato correttamente.")

        If lb_stato_edit.Text = "0" Then
            listView_elenco_tipo_mail.DataBind()

            Visibilita(DivVisibile.ElencoTipoMail)
        Else
            lb_descrizione_tipo_mail.Text = mio_record.id

            DropDown_tipo_mail_DataBind()

            DropDown_tipo_mail.SelectedValue = mio_record.id
            Visibilita(DivVisibile.Ricerca)
        End If
    End Sub

    Protected Sub listView_elenco_tipo_mail_ItemCommand(sender As Object, e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listView_elenco_tipo_mail.ItemCommand
        Trace.Write("listView_elenco_tipo_mail_ItemCommand: " & e.CommandName)

        If e.CommandName = "Lente" Then
            Dim lb_id As Label = e.Item.FindControl("lb_id")
            Dim lb_descrizione As Label = e.Item.FindControl("lb_descrizione")

            Dim mio_record As mail_tipo = New mail_tipo
            With mio_record
                .id = Integer.Parse(lb_id.Text)
                .descrizione = lb_descrizione.Text
            End With

            FillTipoMail(mio_record)

            Visibilita(DivVisibile.ModificaTipoMail)
        ElseIf e.CommandName = "Elimina" Then
            Dim lb_id As Label = e.Item.FindControl("lb_id")

            If mail_tipo.EliminaRecord(Integer.Parse(lb_id.Text)) Then
                listView_elenco_tipo_mail.DataBind()

                Libreria.genUserMsgBox(Page, "Tipo mail cancellata correttamente.")
            Else
                Libreria.genUserMsgBox(Page, "Errore nella cancellazione.")
            End If
        End If
    End Sub

    Protected Sub listView_elenco_mail_ItemCommand(sender As Object, e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listView_elenco_mail.ItemCommand
        Trace.Write("listView_elenco_tipo_mail_ItemCommand: " & e.CommandName)

        If e.CommandName = "Lente" Then
            Dim lb_id As Label = e.Item.FindControl("lb_id")
            Dim lb_mail As Label = e.Item.FindControl("lb_mail")
            Dim lb_nome As Label = e.Item.FindControl("lb_nome")
            Dim lb_id_tipo_invio As Label = e.Item.FindControl("lb_id_tipo_invio")

            Dim mio_record As mail_destinatari = New mail_destinatari
            With mio_record
                .id = Integer.Parse(lb_id.Text)
                .id_tipo_invio = Integer.Parse(lb_id_tipo_invio.Text)
                .mail = lb_mail.Text
                .nome = lb_nome.Text
            End With

            FillMail(mio_record)

            Visibilita(DivVisibile.ModificaMail)
        ElseIf e.CommandName = "Elimina" Then
            Dim lb_id As Label = e.Item.FindControl("lb_id")

            If mail_destinatari.EliminaRecord(Integer.Parse(lb_id.Text)) Then
                listView_elenco_tipo_mail.DataBind()

                Libreria.genUserMsgBox(Page, "Mail cancellata correttamente.")
            Else
                Libreria.genUserMsgBox(Page, "Errore nella cancellazione.")
            End If
        End If
    End Sub

    Protected Sub listView_elenco_mail_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listView_elenco_mail.ItemDataBound
        Dim lb_id_tipo_invio As Label = e.Item.FindControl("lb_id_tipo_invio")
        Dim lb_des_id_tipo_invio As Label = e.Item.FindControl("lb_des_id_tipo_invio")

        Dim Tipo As TipoInvio = Integer.Parse(lb_id_tipo_invio.Text)

        lb_des_id_tipo_invio.Text = Tipo.ToString.Replace("_", "")
    End Sub

    Protected Sub listView_elenco_tipo_mail_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listView_elenco_tipo_mail.ItemDataBound
        Dim lvi As ListViewDataItem = e.Item

        'function ConfermaCancellazione(eliminabile) {
        '    if (eliminabile == 'False') {
        '        alert("Non è possibile eliminare questo tipo mail perché gestito dal sistema!");
        '    Return False;
        '    }
        '    else {
        '        return window.confirm('Confermi la cancellazione del tipo mail?');
        '    }
        '}

        If Not lvi Is Nothing Then
            Dim Elimina As ImageButton = CType(lvi.FindControl("Elimina"), ImageButton)
            Dim lb_eliminabile As Label = CType(lvi.FindControl("lb_eliminabile"), Label)

            If (Not (Elimina) Is Nothing) And (Not (lb_eliminabile) Is Nothing) Then
                If Boolean.Parse(lb_eliminabile.Text) Then
                    Elimina.OnClientClick = "javascript: return (window.confirm('Confermi la cancellazione del tipo mail?'))"
                Else
                    Elimina.OnClientClick = "javascript: alert('Non è possibile eliminare questo tipo mail perché gestito dal sistema!'); return false"
                End If

            End If
        End If
    End Sub
End Class
