
Partial Class MasterPageNoMenuXx
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

	If funzioni_comuni.sql_inj(Request.ServerVariables("SERVER_NAME") & Request.ServerVariables("PATH_INFO") & Request.ServerVariables("QUERY_STRING")) Then
            Response.Redirect("default.aspx")
        End If

        If Request.Cookies("SicilyRentCar") Is Nothing Then
            Response.Redirect("login.aspx")
        Else
            Dim objCookie As HttpCookie = Request.Cookies("SicilyRentCar")
            objCookie.Expires = DateAdd(DateInterval.Minute, 120, Now())
            Response.AppendCookie(objCookie)
        End If

        'CONTROLLO CHE IL COOKIE NON SIA STATO MODIFICATO--------------------------------------------------------------
        Dim test As Integer

        Try
            test = CInt(Request.Cookies("SicilyRentCar")("IdUtente"))
        Catch ex As Exception
            Dim objCookie As HttpCookie = Request.Cookies("SicilyRentCar")
            objCookie.Expires = DateTime.Now.AddDays(-1)
            Response.AppendCookie(objCookie)
            Response.Redirect("login.aspx")
        End Try
        '--------------------------------------------------------------------------------------------------------------
    End Sub
End Class

