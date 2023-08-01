
Partial Class tariffe_lista_tempo_km
    Inherits System.Web.UI.Page

    Protected Function getGruppi() As String()
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT sigla FROM gruppi ORDER BY sigla", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Dim gruppi(20) As String

        Dim i As Integer = 2

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
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(MAX(colonna),0) as numColonne FROM righe_tempo_km WHERE id_tempo_km='" & Replace(codice, "'", "''") & "'", Dbc)

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
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 da FROM righe_tempo_km WHERE id_tempo_km='" & lbl_id_codice.Text & "' AND colonna='" & colonna & "'", Dbc)

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
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 a FROM righe_tempo_km WHERE id_tempo_km='" & lbl_id_codice.Text & "' AND colonna='" & colonna & "'", Dbc)

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
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT valore FROM righe_tempo_km WHERE id_tempo_km='" & lbl_id_codice.Text & "' AND id_gruppo='" & getIdGruppo(gruppo) & "' AND colonna='" & colonna & "'", Dbc)
        getValore = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function


    Protected Sub memorizza_valore(ByVal gruppo As String, ByVal da As String, ByVal a As String, ByVal valore As String, ByVal colonna As String, ByVal packed As Char)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO righe_tempo_km (id_tempo_km,id_gruppo,da,a,pac,valore, colonna) VALUES ('" & lbl_id_codice.Text & "','" & getIdGruppo(gruppo) & "','" & da & "','" & a & "','" & packed & "','" & Replace(valore, ",", ".") & "','" & colonna & "')", Dbc)
       
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub modifica_valore(ByVal gruppo As String, ByVal valore As String, ByVal colonna As String, ByVal packed As Char)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE righe_tempo_km SET valore='" & Replace(valore, ",", ".") & "', pac='" & packed & "' WHERE colonna='" & colonna & "' AND id_gruppo='" & getIdGruppo(gruppo) & "' AND id_tempo_km='" & lbl_id_codice.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub


    'Protected Function codiceEsistente(ByVal codice As String) As Boolean
    '    Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '    Dbc.Open()

    '    Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM tempo_km WHERE codice='" & Replace(Trim(txtCodice.Text), "'", "''") & "'", Dbc)

    '    Dim test As String = Cmd.ExecuteScalar

    '    If test <> "" Then
    '        codiceEsistente = True
    '    Else
    '        codiceEsistente = False
    '    End If

    '    Cmd.Dispose()
    '    Cmd = Nothing
    '    Dbc.Close()
    '    Dbc.Dispose()
    '    Dbc = Nothing
    'End Function

    Protected Function esiste_extra() As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM righe_tempo_km WHERE id_tempo_km='" & lbl_id_codice.Text & "' AND a='999'", Dbc)

        Dim test As String = Cmd.ExecuteScalar

        If test <> "" Then
            esiste_extra = True
        Else
            esiste_extra = False
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub EliminaUltimaColonna()
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT MAX(colonna) FROM righe_tempo_km WHERE id_tempo_km='" & lbl_id_codice.Text & "'", Dbc)
        Dim colonna As String = Cmd.ExecuteScalar

        If colonna <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("DELETE FROM righe_tempo_km WHERE id_tempo_km='" & lbl_id_codice.Text & "' AND colonna='" & colonna & "'", Dbc)
            Cmd.ExecuteNonQuery()
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Function getPacked(ByVal colonna As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 pac FROM righe_tempo_km WHERE id_tempo_km='" & lbl_id_codice.Text & "' AND colonna='" & colonna & "'", Dbc)

        Dim test As String = Cmd.ExecuteScalar

        If test = "" Then
            getPacked = "False"
        Else
            If test = "True" Then
                getPacked = "True"
            Else
                getPacked = "False"
            End If
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

    End Function

    Protected Sub listTempoKm_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listTempoKm.ItemCommand
        If e.CommandName = "vedi" Then
            Dim id_tempo_km As Label = e.Item.FindControl("idLabel")
            Dim nome_tempo_km As Label = e.Item.FindControl("codiceLabel")

            lbl_id_codice.Text = id_tempo_km.Text
            lblCodice.Text = nome_tempo_km.Text

            tab_vedi.Visible = True
            tab_cerca.Visible = False

        End If
    End Sub

End Class
