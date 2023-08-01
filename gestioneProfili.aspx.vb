Imports funzioni_comuni

Partial Class gestioneProfili
    Inherits System.Web.UI.Page

    Dim funzioni As New funzioni_comuni

    Protected Function getAccesso(ByVal idMacro As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_livello_accesso FROM permessi_profili_funzionalita WITH(NOLOCK) WHERE id_permessi_profili='" & id_profilo.Text & "' AND id_funzionalita='" & idMacro & "'", Dbc)
        Dim livello As String = Cmd.ExecuteScalar

        If livello = "1" Then
            getAccesso = False
        Else
            getAccesso = True
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc = Nothing
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            livello_accesso.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.PermessiOperatori)

            If livello_accesso.Text = "1" Then
                Response.Redirect("default.aspx")
            ElseIf livello_accesso.Text = "2" Then
                btnNuovoProfilo.Visible = False
                btnAggiorna.Enabled = False
            End If
        End If
    End Sub

    Protected Sub checkPermessi(ByVal id_profilo As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Dbc1 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc1.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM funzionalita WITH(NOLOCK) WHERE id NOT IN (SELECT id_funzionalita FROM permessi_profili_funzionalita WITH(NOLOCK) WHERE id_permessi_profili='" & id_profilo & "')", Dbc)

        Dim Cmd1 As New Data.SqlClient.SqlCommand("", Dbc1)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Do While Rs.Read
            Cmd1 = New Data.SqlClient.SqlCommand("INSERT INTO permessi_profili_funzionalita (id_permessi_profili,id_funzionalita,id_livello_accesso) VALUES ('" & id_profilo & "','" & Rs("id") & "','1')", Dbc1)
            Cmd1.ExecuteNonQuery()
        Loop

        Cmd1.Dispose()
        Cmd1 = Nothing
        Dbc1.Close()
        Dbc1.Dispose()
        Dbc1 = Nothing

        Cmd.Dispose()
        Cmd = Nothing
        Rs.Close()
        Dbc.Close()
        Rs = Nothing
        Dbc = Nothing
    End Sub

    Protected Sub fillPermessiProfili_blank()
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Dbc1 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc1.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO permessi_profili (attivo) VALUES ('0')", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd = New Data.SqlClient.SqlCommand("SELECT @@IDENTITY FROM permessi_profili WITH(NOLOCK)", Dbc)
        id_profilo.Text = Cmd.ExecuteScalar

        Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM funzionalita WITH(NOLOCK)", Dbc)
        Dim Cmd1 As New Data.SqlClient.SqlCommand("", Dbc1)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Do While Rs.Read
            Cmd1 = New Data.SqlClient.SqlCommand("INSERT INTO permessi_profili_funzionalita (id_permessi_profili,id_funzionalita,id_livello_accesso) VALUES ('" & id_profilo.Text & "','" & Rs("id") & "','1')", Dbc1)
            Cmd1.ExecuteNonQuery()
        Loop

        Cmd1.Dispose()
        Cmd1 = Nothing
        Dbc1.Close()
        Dbc1.Dispose()
        Dbc1 = Nothing

        Cmd.Dispose()
        Cmd = Nothing
        Rs.Close()
        Dbc.Close()
        Rs = Nothing
        Dbc = Nothing
    End Sub


    
    Protected Sub aggiornaPannello(ByVal CKFunzionalità As CheckBox, ByVal Lista As ListView, ByVal IDFunzionalita As Integer)
        If CKFunzionalità.Checked Then
            'If IDFunzionalita = "71" Then
            '    Response.Write(id_profilo.Text)
            'End If
            zUpdatefunction(3, IDFunzionalita, id_profilo.Text)

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand()
            Cmd.Connection = Dbc
            For i = 0 To Lista.Items.Count - 1
                Dim idRiga As Label = Lista.Items(i).FindControl("idLabel")
                Dim dropLivelloAccesso As DropDownList = Lista.Items(i).FindControl("dropPermessi")

                Cmd.CommandText = "UPDATE permessi_profili_funzionalita SET id_livello_accesso=" & dropLivelloAccesso.SelectedValue & " WHERE id=" & idRiga.Text
                Cmd.ExecuteNonQuery()
            Next

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Else
            'IN QUESTO CASO DEVO DISABILITARE L'INTERA FUNZIONALITA'-----------------------------------------------------------------------
            zUpdatefunction(1, IDFunzionalita, id_profilo.Text)

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand()
            Cmd.Connection = Dbc

            For i = 0 To Lista.Items.Count - 1
                Dim idRiga As Label = Lista.Items(i).FindControl("idLabel")
                Dim dropLivelloAccesso As DropDownList = Lista.Items(i).FindControl("dropPermessi")

                Cmd.CommandText = "UPDATE permessi_profili_funzionalita SET id_livello_accesso='1' WHERE id='" & idRiga.Text & "'"
                Cmd.ExecuteNonQuery()
            Next

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            Lista.DataBind()

        End If
    End Sub

    Private Sub zUpdatefunction(ByVal LivelloDiAccesso As Integer, ByVal IDFunzionalita As Integer, ByVal idProfilo As Integer)
        Dim Sql As String
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        'SETTO A LIVELLO 3 L'ACCESSO PER LA FUNZIONALITA' 
        Sql = "UPDATE permessi_profili_funzionalita SET id_livello_accesso=" & LivelloDiAccesso & " WHERE id_funzionalita=" & IDFunzionalita & " AND id_permessi_profili='" & idProfilo & "'"
        Dim Cmd As New Data.SqlClient.SqlCommand(Sql, Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Open()
        Dbc = Nothing
    End Sub

    Protected Sub update_profilo()
        Dim Sql As String
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Sql = "UPDATE permessi_profili SET nome_profilo='" & Replace(txtNomeProfilo.Text, "'", "''") & "', attivo='1' WHERE id='" & id_profilo.Text & "'"
        Dim Cmd As New Data.SqlClient.SqlCommand(Sql, Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Open()
        Dbc = Nothing
    End Sub

    Protected Sub btnAggiorna_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAggiorna.Click
        If Trim(txtNomeProfilo.Text) = "" Then
            Libreria.genUserMsgBox(Me, "Specificare il nome del profilo.")
        Else
            aggiornaPannello(checkParcoVeicoli, listPermessiParcoVeicoli, PermessiUtente.ParcoVeicoli) 'PARCO VEICOLI
            aggiornaPannello(checkTabelle, listPermessiTabelle, PermessiUtente.TabelleVeicoli) 'TABELLE VEICOLI
            aggiornaPannello(checkOrdinativoLavori, listPermessiOrdinativoLavori, PermessiUtente.ODL) 'ODL
            aggiornaPannello(chkFunzioniVeicoli, listPermessiFunzioniVeicoli, PermessiUtente.FunzioniVeicoli) 'FUNZIONI VEICOLI
            aggiornaPannello(checkGestioneListini, listGestioneListini, PermessiUtente.GestioneListini) 'GESTIONE LISTINI
            aggiornaPannello(checkTabelleListini, listTabelleListini, PermessiUtente.TabelleListini) 'TABELLE LISTINI
            aggiornaPannello(checkGestioneStazioni, listPermessiStazioni, PermessiUtente.GestioneStazioni) 'GESTIONE STAZIONI
            aggiornaPannello(checkGestioneVAL, listPermessiGestioneVAL, PermessiUtente.GestioneVal) 'GESTIONE VAL
            aggiornaPannello(checkGestioneMulte, listGestioneMulte, PermessiUtente.GestioneMulte) 'GESTIONE MULTE

            aggiornaPannello(checkPrevenvitiPrenotazioniContratti, listPreventiviPrenotazioniContratti, PermessiUtente.PreventiviPrenotazioni) 'PREVENTIVI/PRENOTAZIONI/CONTRATTI
            aggiornaPannello(checkGestionePOS, listGestionePOS, PermessiUtente.GestionePos) ' Gestione POS
            aggiornaPannello(checkGestioneDanni, listGestioneDanni, PermessiUtente.GestioneDanni) 'GESTIONE DANNI
            aggiornaPannello(checkAnagarfica, listAnagrafica, PermessiUtente.Anagrafica) 'ANAGRAFICA
            aggiornaPannello(checkGestioneOperatori, listGestioneOperatori, PermessiUtente.GestioneOperatori) 'GESTIONE OPERATORI

            update_profilo()

            dropProfili.Items.Clear()
            dropProfili.Items.Add("Seleziona...")
            dropProfili.Items(0).Value = "0"

            dropProfili.DataBind()

            Libreria.genUserMsgBox(Me, "Salvataggio effettuato correttamente.")
        End If
    End Sub

    Protected Sub btnNuovoProfilo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovoProfilo.Click
        fillPermessiProfili_blank()

        txtNomeProfilo.Text = ""
        dropProfili.SelectedValue = "0"
        dropProfili.Enabled = False

        riga_intestazione.Visible = True

        lista_funzionalita.Visible = True
    End Sub

    Protected Sub dropProfili_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropProfili.SelectedIndexChanged
        If dropProfili.SelectedValue > 0 Then
            'PER PRIMA COSA SE CI SONO FUNZIONALITA' NON MEMORIZZATE PER L'UTENTE IN QUESTIONE LE INSERISCO NELLA TABELLA permessi_operatori
            id_profilo.Text = dropProfili.SelectedValue

            checkPermessi(dropProfili.SelectedValue)

            checkParcoVeicoli.Checked = getAccesso("1")
            checkTabelle.Checked = getAccesso("9")
            checkGestioneOperatori.Checked = getAccesso(PermessiUtente.GestioneOperatori)
            checkOrdinativoLavori.Checked = getAccesso("138")
            chkFunzioniVeicoli.Checked = getAccesso("33")
            checkTabelleListini.Checked = getAccesso("39")
            checkGestioneListini.Checked = getAccesso("44")
            checkGestioneStazioni.Checked = getAccesso("51")
            checkGestioneVAL.Checked = getAccesso("58")
            checkGestionePOS.Checked = getAccesso("61")
            checkPrevenvitiPrenotazioniContratti.Checked = getAccesso("71")
            checkGestioneMulte.Checked = getAccesso("89")
            checkGestioneDanni.Checked = getAccesso("103")
            checkAnagarfica.Checked = getAccesso("119")

            If livello_accesso.Text = "3" Then
                btnAggiorna.Enabled = True
            Else
                btnAggiorna.Visible = False
            End If

            txtNomeProfilo.Text = dropProfili.SelectedItem.Text

            riga_intestazione.Visible = True
            lista_funzionalita.Visible = True
        Else
            id_profilo.Text = ""

            btnAggiorna.Enabled = False
            checkParcoVeicoli.Checked = False
            checkTabelle.Checked = False
            checkOrdinativoLavori.Checked = False
            checkTabelleListini.Checked = False
            checkGestioneListini.Checked = False
            checkGestioneStazioni.Checked = False
            checkGestioneVAL.Checked = False
            checkPrevenvitiPrenotazioniContratti.Checked = False
            checkGestioneMulte.Checked = False
            checkGestioneDanni.Checked = False
            checkAnagarfica.Checked = False

            riga_intestazione.Visible = False
            lista_funzionalita.Visible = False
        End If
    End Sub

    Protected Sub elimina_righe_non_attive()
        Dim Sql As String
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Sql = "DELETE from permessi_profili_funzionalita WHERE id_permessi_profili='" & id_profilo.Text & "'"
        Dim Cmd As New Data.SqlClient.SqlCommand(Sql, Dbc)
        Cmd.ExecuteNonQuery()

        Sql = "DELETE permessi_profili WHERE id='" & id_profilo.Text & "'"
        Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Open()
        Dbc = Nothing
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click
        If dropProfili.SelectedValue = "0" Then
            'IN QUESTO CASO SI E' CLICCATO ANNULLA AVENDO CLICCATO SU NUOVO PROFILO - PULISCO LE RIGHE NON ATTIVE
            elimina_righe_non_attive()
        End If

        txtNomeProfilo.Text = ""
        dropProfili.Enabled = True

        riga_intestazione.Visible = False

        lista_funzionalita.Visible = False
    End Sub
End Class

