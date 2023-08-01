
Partial Class modifica_targa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.ModificaKmVettura) <> "3" Then
                Response.Redirect("default.aspx")
            End If
        Else
            sqlMovimentiTarga.SelectCommand = lblQuery.Text & " ORDER BY movimenti_targa.id DESC"
            sqlMovimentiTarga.DataBind()
        End If
    End Sub

    Protected Sub btnCercaVeicolo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCercaVeicolo.Click
        cerca()
    End Sub

    Protected Function getIdVeicolo(ByVal targa As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(id,-1) FROM veicoli WHERE targa='" & Replace(targa, "'", "''") & "'", Dbc)
        Dbc.Open()

        getIdVeicolo = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function condizioneWhere() As String
        Dim query As String = ""


       If txtTarga.Text <> "" Then
            query = query & " AND movimenti_targa.id_veicolo='" & getIdVeicolo(txtTarga.Text.Trim) & "'"
        End If


        condizioneWhere = query
    End Function

    Protected Function disponibile_nolo(ByVal targa As String) As Boolean
        Dim non_disponibile As Boolean = False

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM veicoli WHERE targa='" & Replace(targa, "'", "''") & "'", Dbc)
        Dbc.Open()

        Dim test As String = Cmd.ExecuteScalar & ""

        If test <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT disponibile_nolo FROM veicoli WHERE targa='" & Replace(targa, "'", "''") & "'", Dbc)
            Dim test2 As Boolean = Cmd.ExecuteScalar

            If Not test2 Then
                non_disponibile = True
                txtKmAttuali.Text = ""
            Else
                Cmd = New Data.SqlClient.SqlCommand("SELECT km_attuali FROM veicoli WHERE targa='" & Replace(targa, "'", "''") & "'", Dbc)
                txtKmAttuali.Text = Cmd.ExecuteScalar & ""
            End If

           
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        Return Not non_disponibile
    End Function

    Protected Sub cerca()
        If txtTarga.Text.Trim <> "" Then
            listMovimentiTarga.Visible = True
            'VEICOLO
            sqlMovimentiTarga.SelectCommand = "SELECT movimenti_targa.id, movimenti_targa.num_riferimento, veicoli.targa, '' As gps, " & _
                " modelli.descrizione As modello, CONVERT(Char(10), movimenti_targa.data_uscita, 103) As data_uscita, " & _
                " CONVERT(Char(10), movimenti_targa.data_rientro, 103) As data_rientro, " & _
                " CONVERT(Char(10), movimenti_targa.data_presunto_rientro, 103) As data_presunto_rientro, CONVERT(Char(8), movimenti_targa.data_presunto_rientro, 108) As ora_presunto_rientro, " & _
                " CONVERT(Char(8), movimenti_targa.data_uscita, 108) As ora_uscita, tipologia_movimenti.descrzione As tipo_movimento, " & _
                " CONVERT(Char(8), movimenti_targa.data_rientro, 108) As ora_rientro, " & _
                " (stazioni1.codice + ' ' + stazioni1.nome_stazione) As stazione_uscita, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As stazione_rientro, (stazioni3.codice + ' ' + stazioni3.nome_stazione) As stazione_presunto_rientro, " & _
                " km_uscita, km_rientro, serbatoio_uscita, serbatoio_rientro " & _
                " FROM movimenti_targa WITH(NOLOCK) INNER JOIN veicoli WITH(NOLOCK) ON movimenti_targa.id_veicolo=veicoli.id " & _
                " LEFT JOIN stazioni As stazioni1 WITH(NOLOCK) ON movimenti_targa.id_stazione_uscita=stazioni1.id " & _
                " LEFT JOIN stazioni As stazioni2 WITH(NOLOCK) ON movimenti_targa.id_stazione_rientro=stazioni2.id " & _
                " LEFT JOIN stazioni As stazioni3 WITH(NOLOCK) ON movimenti_targa.id_stazionE_presunto_rientro=stazioni3.id " & _
                " INNER JOIN tipologia_movimenti WITH(NOLOCK) ON movimenti_targa.id_tipo_movimento=tipologia_movimenti.id " & _
                " INNER JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello WHERE disponibile_nolo=1 AND km_uscita <> '' AND km_rientro <> '' " & condizioneWhere()

            lblQuery.Text = sqlMovimentiTarga.SelectCommand
            sqlMovimentiTarga.SelectCommand = lblQuery.Text & " ORDER BY movimenti_targa.id DESC"

            If Not disponibile_nolo(txtTarga.Text.Trim) Then
                Libreria.genUserMsgBox(Me, "Attenzione: Km auto attualmente non modificabili")
            End If
        Else
            Libreria.genUserMsgBox(Me, "Specificare una targa")
        End If
    End Sub

    Protected Sub btnModificaMovimenti_Click(sender As Object, e As System.EventArgs) Handles btnModificaMovimenti.Click
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
        Dbc.Open()

        Dim txtKmUscita As TextBox
        Dim km_uscita As Label

        Dim targa As Label

        Dim txtKmRientro As TextBox
        Dim km_rientro As Label

        Dim idLabel As Label

        Dim valori_ok As Boolean = True

        Try
            For i = 0 To listMovimentiTarga.Items.Count - 1
                txtKmUscita = listMovimentiTarga.Items(i).FindControl("txtKmUscita")
                txtKmRientro = listMovimentiTarga.Items(i).FindControl("txtKmRientro")

                If txtKmUscita.Text.Trim = "" Or txtKmRientro.Text.Trim = "" Then
                    valori_ok = False
                End If

                Dim test As Integer = CInt(txtKmRientro.Text.Trim)
                test = CInt(txtKmUscita.Text.Trim)
            Next
        Catch ex As Exception
            valori_ok = False
        End Try
        
        If valori_ok Then
            For i = 0 To listMovimentiTarga.Items.Count - 1
                idLabel = listMovimentiTarga.Items(i).FindControl("idLabel")

                txtKmUscita = listMovimentiTarga.Items(i).FindControl("txtKmUscita")
                km_uscita = listMovimentiTarga.Items(i).FindControl("km_uscita")

                txtKmRientro = listMovimentiTarga.Items(i).FindControl("txtKmRientro")
                km_rientro = listMovimentiTarga.Items(i).FindControl("km_rientro")

                targa = listMovimentiTarga.Items(i).FindControl("targa")

                If txtKmUscita.Text.Trim <> km_uscita.Text.Trim Or txtKmRientro.Text.Trim <> km_rientro.Text.Trim Then
                    Cmd = New Data.SqlClient.SqlCommand("UPDATE movimenti_targa SET km_uscita='" & txtKmUscita.Text & "', km_rientro='" & txtKmRientro.Text & "' WHERE id=" & idLabel.Text, Dbc)
                    Cmd.ExecuteNonQuery()

                    If i = 0 And txtKmRientro.Text.Trim <> km_rientro.Text.Trim Then
                        Cmd = New Data.SqlClient.SqlCommand("UPDATE veicoli SET km_attuali='" & txtKmRientro.Text & "' WHERE targa='" & targa.Text & "'", Dbc)
                        Cmd.ExecuteNonQuery()
                    End If
                End If
            Next

            cerca()

            listMovimentiTarga.DataBind()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            Libreria.genUserMsgBox(Me, "Operazione Effettuata Correttamente")
        Else
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            Libreria.genUserMsgBox(Me, "Rilevati valori non corretti. Correggere e riprovare.")
        End If
        

        

        
    End Sub
End Class
