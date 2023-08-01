
Partial Class tempo_km
    Inherits System.Web.UI.Page

    Protected Function getGruppi() As String()
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT sigla FROM gruppi ORDER BY sigla", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Dim gruppi(20) As String

        Dim i As Integer = 1

        Do While Rs.Read()
            gruppi(i) = Rs("sigla")
            i = i + 1
        Loop
        gruppi(i) = "000"
        getGruppi = gruppi

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getIdGruppo(ByVal gruppo As String) As Integer
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM gruppi WHERE sigla='" & gruppo & "'", Dbc)

        getIdGruppo = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getNumColonne(ByVal codice As String) As Integer
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(MAX(colonna),0) as numColonne FROM tempo_km WHERE codice='" & Replace(codice, "'", "''") & "'", Dbc)

        getNumColonne = Cmd.ExecuteScalar + 1

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getColonnaDa(ByVal codice As String, ByVal colonna As String) As Integer
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 da FROM tempo_km WHERE codice='" & Replace(codice, "'", "''") & "' AND colonna='" & colonna & "'", Dbc)

        getColonnaDa = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getColonnaA(ByVal codice As String, ByVal colonna As String) As Integer
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 a FROM tempo_km WHERE codice='" & Replace(codice, "'", "''") & "' AND colonna='" & colonna & "'", Dbc)

        getColonnaA = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getValore(ByVal codice As String, ByVal gruppo As String, ByVal colonna As Integer) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT valore FROM tempo_km WHERE codice='" & Replace(codice, "'", "''") & "' AND id_gruppo='" & getIdGruppo(gruppo) & "' AND colonna='" & colonna & "'", Dbc)
        getValore = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function


    Protected Sub memorizza_valore(ByVal gruppo As String, ByVal da As String, ByVal a As String, ByVal valore As String, ByVal colonna As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO tempo_km (data_creazione,codice,id_gruppo,da,a,pac,valore, colonna) VALUES (getDate(),'" & Replace(Trim(txtCodice.Text), "'", "''") & "','" & getIdGruppo(gruppo) & "','" & da & "','" & a & "',NULL,'" & Replace(valore, ",", ".") & "','" & colonna & "')", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub modifica_valore(ByVal gruppo As String, ByVal valore As String, ByVal colonna As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE tempo_km SET valore='" & Replace(valore, ",", ".") & "' WHERE colonna='" & colonna & "' AND id_gruppo='" & getIdGruppo(gruppo) & "' AND codice='" & Replace(txtCodice.Text, "'", "''") & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub dropTempoKm_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropTempoKm.SelectedIndexChanged
        If dropTempoKm.SelectedValue <> "0" Then
            txtCodice.Text = dropTempoKm.SelectedValue
            txtCodice.ReadOnly = True
        Else
            txtCodice.Text = ""
            txtCodice.ReadOnly = False
        End If

    End Sub

    Protected Function esisteCodice(ByVal codice As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM tempo_km WHERE codice='" & Replace(Trim(codice), "'", "''") & "'", Dbc)
        Response.Write("SELECT TOP 1 id FROM tempo_km WHERE codice='" & Replace(Trim(codice), "'", "''") & "'")
        Dim test As String = Cmd.ExecuteScalar

        Response.Write(test & "A")

        If test <> "" Then
            esisteCodice = True
        Else
            esisteCodice = False
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If Trim(txtCodice.Text) <> "" And dropTempoKm.SelectedValue = "0" Then
            Response.Write("1")
            If esisteCodice(txtCodice.Text) Then
                Response.Write(txtCodice.Text)
            End If
        End If
    End Sub
End Class
