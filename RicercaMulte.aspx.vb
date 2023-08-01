Imports System.Data
Imports System.Data.SqlClient

Partial Class RicercaMulte
    Inherits System.Web.UI.Page

    Protected Sub btnTabelle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTabelle.Click
        Response.Redirect("tabelle_multe.aspx")
    End Sub

    Protected Sub btnRiepilogoInc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRiepilogoInc.Click
        VisulRiepilogoInc.Visible = True
        VisualRicercaMulte.Visible = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.Cookies("SicilyRentCar") Is Nothing Then
                Response.Redirect("LogIn.aspx")
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 93) = "1" Then
                Response.Redirect("default.aspx")
            End If
        End If
        AddHandler RiepilogoIncassi1.RiepilogoIncassiClose, AddressOf RiepilogoIncassiClose
    End Sub

    Private Sub RiepilogoIncassiClose(ByVal sender As Object, ByVal e As EventArgs)
        VisulRiepilogoInc.Visible = False
        VisualRicercaMulte.Visible = True
    End Sub

    Protected Sub btnMulteLocatori_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMulteLocatori.Click
        Response.Redirect("MulteLocatori.aspx")
    End Sub
End Class
