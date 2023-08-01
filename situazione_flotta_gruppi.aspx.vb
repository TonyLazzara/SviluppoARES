Imports funzioni_comuni

Partial Class situazione_flotta_gruppi
    Inherits System.Web.UI.Page

    Protected Function getStazione(ByVal idStazione As String) As String

        getStazione = ""
        Try
            Dim test As Integer = CInt(idStazione)
        Catch ex As Exception
            getStazione = "ERRORE STAZIONE INESISTENTE"
        End Try

        If getStazione = "" Then
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT nome_stazione FROM stazioni WITH(NOLOCK) WHERE id='" & idStazione & "'", Dbc)

            getStazione = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End If



    End Function

  
    Protected Function getIdGruppo(ByVal gruppo As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_gruppo FROM gruppi WITH(NOLOCK) WHERE cod_gruppo='" & gruppo & "'", Dbc)

        getIdGruppo = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function
End Class
