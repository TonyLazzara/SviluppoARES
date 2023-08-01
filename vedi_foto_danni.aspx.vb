Imports funzioni_comuni

Partial Class vedi_foto_danni
    Inherits System.Web.UI.Page
    Dim funzioni As New funzioni_comuni

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then            
            Dim ArrayDati(4)

            'Response.Write(Session("Dati"))
            'Response.End()

            ArrayDati = Split(Session("Dati"), "@")
            lb_num_documento1.Text = ArrayDati(0)
            lb_targa.Text = ArrayDati(1)
            lb_modello.Text = ArrayDati(2)
            lb_stazione.Text = ArrayDati(3)
            lb_km.Text = ArrayDati(4)
        End If
    End Sub

End Class
