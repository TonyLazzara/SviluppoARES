
Partial Class MasterPage2
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If funzioni_comuni.sql_inj(Request.ServerVariables("SERVER_NAME") & Request.ServerVariables("PATH_INFO") & Request.ServerVariables("QUERY_STRING")) Then
            Response.Redirect("default.aspx")
        End If
    End Sub
End Class

