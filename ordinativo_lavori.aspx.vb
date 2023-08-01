Imports funzioni_comuni

Partial Class ordinativo_lavori
    Inherits System.Web.UI.Page

    Dim funzioni As New funzioni_comuni

    Protected Sub AzzeraTab()
        PanelRichiestaOrdinativo.Visible = False
    End Sub

    Protected Sub richiestaOrdinativo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles richiestaOrdinativo.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 31) <> "1" Then
            AzzeraTab()
            PanelRichiestaOrdinativo.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.Cookies("SicilyRentCar") Is Nothing Then
                Response.Redirect("LogIn.aspx")
            Else
                If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 30) = "1" Then
                    Response.Redirect("default.aspx")
                End If

            End If
        End If
    End Sub


End Class
