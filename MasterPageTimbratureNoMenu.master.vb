
Partial Class MasterPageTimbratureNoMenu
    Inherits System.Web.UI.MasterPage

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("SRC_timbrature") Is Nothing Then
            'Response.Write("Non Loggato")
            'salva_log("non loggato")
        Else
            'Response.Write("Loggato")
            'salva_log("loggato")
        End If
    End Sub

    'Protected Sub salva_log(ByVal condizione As String)
    '    Dim Sql As String

    '    If condizione = "loggato" Then
    '        Sql = "INSERT INTO utenti_log (id_utente, nominativo, data, pagina, ip) VALUES ('" & Request.Cookies("BoutiqueDocce")("IdUtente") & "','" & Replace(Request.Cookies("BoutiqueDocce")("nome"), "'", "''") & "','" & Now & "','" & Replace(Request.CurrentExecutionFilePath, "'", "''") & "','" & Replace(Request.UserHostAddress(), "'", "''") & "')"
    '    Else
    '        Sql = "INSERT INTO utenti_log (id_utente, nominativo, data, pagina, ip) VALUES ('0','NL','" & Now & "','" & Replace(Request.CurrentExecutionFilePath, "'", "''") & "','" & Replace(Request.UserHostAddress(), "'", "''") & "')"
    '    End If

    '    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString)
    '    Dbc.Open()
    '    Dim Cmd As New Data.SqlClient.SqlCommand(Sql, Dbc)
    '    'Response.Write(Cmd.CommandText)
    '    'Response.End()

    '    Cmd.ExecuteNonQuery()

    '    Cmd.Dispose()
    '    Cmd = Nothing
    '    Dbc.Close()
    '    Dbc.Dispose()
    '    Dbc = Nothing
    'End Sub
End Class

