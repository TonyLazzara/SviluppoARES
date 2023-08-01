
Partial Class gestioneTariffe
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Select Case Request("tab")
                Case Is = "1"
                    tabGestTariffe.ActiveTab = tabDatiTempoKm
                Case Is = "2"
                    tabGestTariffe.ActiveTab = tabDatiCondizioni
                Case Is = "3"
                    tabGestTariffe.ActiveTab = tabDatiTariffe
            End Select
        End If        
    End Sub
End Class
