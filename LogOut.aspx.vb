
Partial Class LogOut
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objCookie As HttpCookie = Request.Cookies("SicilyRentCar")
        If Not objCookie Is Nothing Then


            'registra logout
            Dim ip_address As String = Request.ServerVariables("REMOTE_ADDR")
            Dim id_user As String = Request.Cookies("SicilyRentCar")("IdUtente")

            registra_ip(ip_address, id_user, "logout")

            objCookie.Expires = DateTime.Now.AddDays(-1)
            'Response.AppendCookie(objCookie)
            Response.AppendCookie(objCookie)

            Response.Redirect("login.aspx")
        Else
            Response.Redirect("login.aspx")
        End If
    End Sub

    Protected Sub registra_ip(indirizzoip, idusr, tipoA)  'm

        Dim dd = Date.Now.Day
        Dim mm = Date.Now.Month
        Dim yy = Date.Now.Year
        Dim hh = Date.Now.Hour
        Dim mmin = Date.Now.Minute
        Dim ss = Date.Now.Second
        Dim tt = yy & "-" & mm & "-" & dd & " " & hh & ":" & mmin & ":" & ss & ".000"

        Dim sql As String = "INSERT INTO ip_access (ipaddress,dataora,iduser,datanowora, tipo_accesso) VALUES ('" & indirizzoip & "','" & Date.Now.ToString & "','" & idusr & "',CONVERT(DATETIME, '" & tt & "', 102),'" & tipoA & "')"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing


            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception

            Response.Write("error_registra_ip:" & ex.Message & "<br/>" & sql & "<br/>")

        End Try



    End Sub

End Class
