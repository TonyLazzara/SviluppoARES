Imports funzioni_comuni

Partial Class tabelle_pos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("SicilyRentCar") Is Nothing Then
            Response.Redirect("/default.aspx")
        Else
            Dim objCookie As HttpCookie = Request.Cookies("SicilyRentCar")
            objCookie.Expires = DateAdd(DateInterval.Minute, 120, Now())
            Response.AppendCookie(objCookie)
        End If

        If Not Page.IsPostBack() Then 'NOT PostBack


        Else 'PostBack


        End If

    End Sub

    Protected Sub btnCensimentoPOS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCensimentoPOS.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 59) <> "1" Then
            Response.Redirect("\tabelle_pos\pos_gestione_pos.aspx")
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If

    End Sub

    Protected Sub btnCensimentoEntiProprietari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCensimentoEntiProprietari.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 63) <> "1" Then
            Response.Redirect("\tabelle_pos\pos_enti.aspx")
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnCensimentoCircuiti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCensimentoCircuiti.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 64) <> "1" Then
            Response.Redirect("\tabelle_pos\pos_circuiti.aspx")
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnFunzionalita_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFunzionalita.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 69) <> "1" Then
            Response.Redirect("\tabelle_pos\pos_funzionalita.aspx")
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub
End Class
