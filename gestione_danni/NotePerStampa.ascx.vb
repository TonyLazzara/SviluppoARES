

Partial Class gestione_danni_NotePerStampa
    Inherits System.Web.UI.UserControl

    Public Sub InitForm(ByVal id_tipo_documento As enum_note_tipo, ByVal id_documento As Integer)
        lb_id_tipo.Text = id_tipo_documento
        lb_id_documento.Text = id_documento

        ListViewNote.DataBind()
    End Sub

End Class
