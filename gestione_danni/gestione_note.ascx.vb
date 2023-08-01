

Partial Class gestione_danni_gestione_note
    Inherits System.Web.UI.UserControl

    Public Sub InitForm(ByVal id_tipo_documento As Integer, ByVal id_documento As Integer, Optional ByVal abilita_modifica_note As Boolean = False, Optional ByVal solo_lettura As Boolean = False, Optional ByVal intestazione_note As String = "Note")
        Trace.Write(">>>>>>>>>>> InitForm: " & id_tipo_documento & " " & id_documento & " " & abilita_modifica_note & " " & solo_lettura)

        lb_id_tipo.Text = id_tipo_documento
        lb_id_documento.Text = id_documento

        lb_lente.Text = abilita_modifica_note

        tr_salva_nota.Visible = Not solo_lettura

        lb_intestazione_note.Text = intestazione_note

        ListViewNote.DataBind()
    End Sub

    Protected Sub bt_Salva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_Salva.Click
        Trace.Write(">>>>>>>>>>> bt_Salva_Click: " & lb_id_tipo.Text & " " & lb_id_documento.Text & " " & lb_lente.Text & " " & tr_salva_nota.Visible & " " & tx_nota.Text)

        Dim mia_nota As note = New note

        If Integer.Parse(lb_id_documento.Text) <= 0 Then
            Libreria.genUserMsgBox(Page, "E' necessario prima salvare il documento, prima di poter inserire una nota.")
            Return
        End If

        With mia_nota
            .id = Integer.Parse(lb_id.Text)
            .id_tipo = Integer.Parse(lb_id_tipo.Text)
            .id_documento = Integer.Parse(lb_id_documento.Text)
            .nota = tx_nota.Text

            If .id = 0 Then
                .SalvaRecord()
            Else
                If Boolean.Parse(lb_lente.Text) Then
                    .AggiornaRecord()
                Else
                    .SalvaRecord()
                End If
            End If
        End With

        AzzeraRecord()

        ListViewNote.DataBind()
    End Sub

    Protected Sub FillRecord(ByVal mia_nota As note)
        With mia_nota
            lb_id.Text = .id
            tx_nota.Text = .nota

            If .id = 0 Then
                bt_Salva.Text = "Salva Nuova Nota"
                bt_Annulla.Visible = False
            Else
                If Boolean.Parse(lb_lente.Text) Then
                    bt_Salva.Text = "Modifica Nota"
                    bt_Annulla.Visible = True
                Else
                    bt_Salva.Text = "Salva Nuova Nota"
                    bt_Annulla.Visible = False
                End If
            End If
        End With
    End Sub

    Protected Sub AzzeraRecord()
        Dim mia_nota As note = New note

        With mia_nota
            .id = 0
            '.id_tipo = Integer.Parse(lb_id_tipo.Text)
            '.id_documento = Integer.Parse(lb_id_documento.Text)
            .nota = ""
        End With

        FillRecord(mia_nota)
    End Sub

    Protected Sub ListViewNote_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListViewNote.DataBound
        Dim th_lente As Control = ListViewNote.FindControl("th_lente")
        If th_lente IsNot Nothing Then
            th_lente.Visible = Boolean.Parse(lb_lente.Text)
        End If
    End Sub

    Protected Sub ListViewNote_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListViewNote.ItemCommand
        If e.CommandName = "lente" Then
            Dim lb_id As Label = e.Item.FindControl("lb_id")

            Dim mia_nota = note.getRecordDaId(Integer.Parse(lb_id.Text))

            FillRecord(mia_nota)

        End If
    End Sub

    Protected Sub bt_Annulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_Annulla.Click
        AzzeraRecord()
    End Sub
End Class
