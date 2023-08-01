
Partial Class PagTmp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Sql As String

        Sql = "select ID, num_contratto, status, attivo from contratti WITH(NOLOCK) where num_contratto =" & Request("parametro") & " and attivo=1"

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand(Sql, Dbc)
        'Response.Write(Cmd.CommandText & "<br><br>")
        'Response.End()

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()
        Do While Rs.Read
            Session("carica_contratto") = "" & Rs("ID") & ""
            Response.Redirect("contratti.aspx")
        Loop

        Cmd.Dispose()
        Cmd = Nothing

        Rs.Close()
        Rs.Dispose()
        Rs = Nothing

        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub
End Class
