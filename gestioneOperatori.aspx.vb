Imports funzioni_comuni

Partial Class gestioneOperatori
    Inherits System.Web.UI.Page

    Dim funzioni As New funzioni_comuni

    Protected Function getAccesso(ByVal idMacro As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_livello_accesso FROM permessi_operatori WITH(NOLOCK) WHERE id_operatore='" & dropOperatori.SelectedValue & "' AND id_funzionalita='" & idMacro & "'", Dbc)
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
            End If
        End If
    End Sub

    Protected Sub checkPermessi(ByVal id_operatore As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Dbc1 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc1.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM funzionalita WITH(NOLOCK) WHERE id NOT IN (SELECT id_funzionalita FROM permessi_operatori WITH(NOLOCK) WHERE id_operatore='" & id_operatore & "')", Dbc)

        Dim Cmd1 As New Data.SqlClient.SqlCommand("", Dbc1)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Do While Rs.Read
            Cmd1 = New Data.SqlClient.SqlCommand("INSERT INTO permessi_operatori (id_operatore,id_funzionalita,id_livello_accesso) VALUES ('" & id_operatore & "','" & Rs("id") & "','1')", Dbc1)
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

    Protected Sub dropOperatori_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropOperatori.SelectedIndexChanged
        If dropOperatori.SelectedValue > 0 Then
            'PER PRIMA COSA SE CI SONO FUNZIONALITA' NON MEMORIZZATE PER L'UTENTE IN QUESTIONE LE INSERISCO NELLA TABELLA permessi_operatori
            checkPermessi(dropOperatori.SelectedValue)

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

            lista_funzionalita.Visible = True

            'dropProfili.SelectedValue = "0"
        Else
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


            lista_funzionalita.Visible = False
        End If
    End Sub

    'Protected Sub aggiornaParcoVeicoli()
    '    If checkParcoVeicoli.Checked Then
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 3 L'ACCESSO PER LA FUNZIONALITA' PARCO VEICOLI (ID=1)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='3' WHERE id_funzionalita='1' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listPermessiParcoVeicoli.Items.Count - 1
    '            Dim idRiga As Label = listPermessiParcoVeicoli.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listPermessiParcoVeicoli.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='" & dropLivelloAccesso.SelectedValue & "' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing
    '    Else
    '        'IN QUESTO CASO DEVO DISABILITARE L'INTERA FUNZIONALITA'-----------------------------------------------------------------------
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 1 L'ACCESSO PER LA FUNZIONALITA' PARCO VEICOLI (ID=1)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id_funzionalita='1' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listPermessiParcoVeicoli.Items.Count - 1
    '            Dim idRiga As Label = listPermessiParcoVeicoli.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listPermessiParcoVeicoli.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing

    '        listPermessiParcoVeicoli.DataBind()

    '    End If
    'End Sub

    'Protected Sub aggiornaTabelle()
    '    If checkTabelle.Checked Then
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 3 L'ACCESSO PER LA FUNZIONALITA' TABELLE (ID=9)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='3' WHERE id_funzionalita='9' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listPermessiTabelle.Items.Count - 1
    '            Dim idRiga As Label = listPermessiTabelle.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listPermessiTabelle.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='" & dropLivelloAccesso.SelectedValue & "' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing
    '    Else
    '        'IN QUESTO CASO DEVO DISABILITARE L'INTERA FUNZIONALITA'-----------------------------------------------------------------------
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 1 L'ACCESSO PER LA FUNZIONALITA' TABELLE (ID=9)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id_funzionalita='9' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listPermessiTabelle.Items.Count - 1
    '            Dim idRiga As Label = listPermessiTabelle.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listPermessiTabelle.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing

    '        listPermessiTabelle.DataBind()

    '    End If
    'End Sub

    'Protected Sub aggiornaGestioneOperatori()
    '    If checkGestioneOperatori.Checked Then
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 3 L'ACCESSO PER LA FUNZIONALITA' GESTIONE OPERATORI (ID=21)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='3' WHERE id_funzionalita='21' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing
    '    Else
    '        'IN QUESTO CASO DEVO DISABILITARE L'INTERA FUNZIONALITA'-----------------------------------------------------------------------
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 1 L'ACCESSO PER LA FUNZIONALITA' GESTIONE OPERATORI (ID=21)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id_funzionalita='21' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing

    '        listPermessiTabelle.DataBind()

    '    End If
    'End Sub

    'Protected Sub aggiornaOrdinativoLavori()
    '    If checkOrdinativoLavori.Checked Then
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 3 L'ACCESSO PER LA FUNZIONALITA' ORDINATIVO LAVORI (ID=30)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='3' WHERE id_funzionalita='138' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listPermessiOrdinativoLavori.Items.Count - 1
    '            Dim idRiga As Label = listPermessiOrdinativoLavori.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listPermessiOrdinativoLavori.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='" & dropLivelloAccesso.SelectedValue & "' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing
    '    Else
    '        'IN QUESTO CASO DEVO DISABILITARE L'INTERA FUNZIONALITA'-----------------------------------------------------------------------
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 1 L'ACCESSO PER LA FUNZIONALITA' ORDINATIVO LAVORI (ID=30)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id_funzionalita='138' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listPermessiOrdinativoLavori.Items.Count - 1
    '            Dim idRiga As Label = listPermessiOrdinativoLavori.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listPermessiOrdinativoLavori.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing

    '        listPermessiTabelle.DataBind()

    '    End If
    'End Sub

    'Protected Sub aggioraFunzioniVeicoli()
    '    If chkFunzioniVeicoli.Checked Then
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 3 L'ACCESSO PER LA FUNZIONALITA' FUNZIONI VEICOLI (ID=33)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='3' WHERE id_funzionalita='33' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listPermessiFunzioniVeicoli.Items.Count - 1
    '            Dim idRiga As Label = listPermessiFunzioniVeicoli.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listPermessiFunzioniVeicoli.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='" & dropLivelloAccesso.SelectedValue & "' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing
    '    Else
    '        'IN QUESTO CASO DEVO DISABILITARE L'INTERA FUNZIONALITA'-----------------------------------------------------------------------
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 1 L'ACCESSO PER LA FUNZIONALITA' FUNZIONI VEICOLI (ID=33)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id_funzionalita='33' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listPermessiFunzioniVeicoli.Items.Count - 1
    '            Dim idRiga As Label = listPermessiFunzioniVeicoli.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listPermessiFunzioniVeicoli.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing

    '        listPermessiTabelle.DataBind()

    '    End If
    'End Sub

    'Protected Sub aggiornaGestioneListini()
    '    If checkGestioneListini.Checked Then
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 3 L'ACCESSO PER LA FUNZIONALITA' GESTIONE LISTINI (ID=44)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='3' WHERE id_funzionalita='44' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listGestioneListini.Items.Count - 1
    '            Dim idRiga As Label = listGestioneListini.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listGestioneListini.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='" & dropLivelloAccesso.SelectedValue & "' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing
    '    Else
    '        'IN QUESTO CASO DEVO DISABILITARE L'INTERA FUNZIONALITA'-----------------------------------------------------------------------
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 1 L'ACCESSO PER LA FUNZIONALITA' GESTIONE LISTINI (ID=44)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id_funzionalita='44' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listGestioneListini.Items.Count - 1
    '            Dim idRiga As Label = listGestioneListini.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listGestioneListini.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing

    '        listPermessiTabelle.DataBind()

    '    End If
    'End Sub

    'Protected Sub aggiornaTabelleListini()
    '    If checkTabelleListini.Checked Then
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 3 L'ACCESSO PER LA FUNZIONALITA' TABELLE LISTINI (ID=39)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='3' WHERE id_funzionalita='39' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listTabelleListini.Items.Count - 1
    '            Dim idRiga As Label = listTabelleListini.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listTabelleListini.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='" & dropLivelloAccesso.SelectedValue & "' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing
    '    Else
    '        'IN QUESTO CASO DEVO DISABILITARE L'INTERA FUNZIONALITA'-----------------------------------------------------------------------
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 1 L'ACCESSO PER LA FUNZIONALITA' TABELLE LISTINI (ID=39)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id_funzionalita='39' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listTabelleListini.Items.Count - 1
    '            Dim idRiga As Label = listTabelleListini.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listTabelleListini.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing

    '        listPermessiTabelle.DataBind()

    '    End If
    'End Sub


    'Protected Sub aggiornaGestioneStazioni()
    '    If checkGestioneStazioni.Checked Then
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 3 L'ACCESSO PER LA FUNZIONALITA' GESTIONE STAZIONI (ID=51)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='3' WHERE id_funzionalita='51' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listPermessiStazioni.Items.Count - 1
    '            Dim idRiga As Label = listPermessiStazioni.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listPermessiStazioni.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='" & dropLivelloAccesso.SelectedValue & "' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing
    '    Else
    '        'IN QUESTO CASO DEVO DISABILITARE L'INTERA FUNZIONALITA'-----------------------------------------------------------------------
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 1 L'ACCESSO PER LA FUNZIONALITA' Gestione Stazioni (ID=51)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id_funzionalita='51' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listPermessiStazioni.Items.Count - 1
    '            Dim idRiga As Label = listPermessiStazioni.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listPermessiStazioni.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing

    '        listPermessiStazioni.DataBind()

    '    End If
    'End Sub

    'Protected Sub aggiornaGestioneVAL()
    '    If checkGestioneVAL.Checked Then
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 3 L'ACCESSO PER LA FUNZIONALITA' GESTIONE STAZIONI (ID=58)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='3' WHERE id_funzionalita='58' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listPermessiGestioneVAL.Items.Count - 1
    '            Dim idRiga As Label = listPermessiGestioneVAL.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listPermessiGestioneVAL.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='" & dropLivelloAccesso.SelectedValue & "' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing
    '    Else
    '        'IN QUESTO CASO DEVO DISABILITARE L'INTERA FUNZIONALITA'-----------------------------------------------------------------------
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 1 L'ACCESSO PER LA FUNZIONALITA' Gestione Stazioni (ID=51)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id_funzionalita='58' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listPermessiGestioneVAL.Items.Count - 1
    '            Dim idRiga As Label = listPermessiGestioneVAL.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listPermessiGestioneVAL.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing

    '        listPermessiGestioneVAL.DataBind()

    '    End If
    'End Sub

    'Protected Sub aggiornaGestioneMulte()
    '    If checkGestioneMulte.Checked Then
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 3 L'ACCESSO PER LA FUNZIONALITA' GESTIONE STAZIONI (ID=89)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='3' WHERE id_funzionalita='89' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listGestioneMulte.Items.Count - 1
    '            Dim idRiga As Label = listGestioneMulte.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listGestioneMulte.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='" & dropLivelloAccesso.SelectedValue & "' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing
    '    Else
    '        'IN QUESTO CASO DEVO DISABILITARE L'INTERA FUNZIONALITA'-----------------------------------------------------------------------
    '        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Dbc.Open()
    '        'SETTO A LIVELLO 1 L'ACCESSO PER LA FUNZIONALITA' Gestione Stazioni (ID=51)
    '        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id_funzionalita='89' AND id_operatore='" & dropOperatori.SelectedValue & "'", Dbc)
    '        Cmd.ExecuteScalar()

    '        For i = 0 To listGestioneMulte.Items.Count - 1
    '            Dim idRiga As Label = listGestioneMulte.Items(i).FindControl("idLabel")
    '            Dim dropLivelloAccesso As DropDownList = listGestioneMulte.Items(i).FindControl("dropPermessi")

    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id='" & idRiga.Text & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        Next

    '        Cmd.Dispose()
    '        Cmd = Nothing
    '        Dbc.Close()
    '        Dbc.Dispose()
    '        Dbc = Nothing

    '        listGestioneMulte.DataBind()

    '    End If
    'End Sub


    Protected Sub aggiornaPannello(ByVal CKFunzionalità As CheckBox, ByVal Lista As ListView, ByVal IDFunzionalita As Integer)
        If CKFunzionalità.Checked Then
            zUpdatefunction(3, IDFunzionalita, dropOperatori.SelectedValue)

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand()
            Cmd.Connection = Dbc
            For i = 0 To Lista.Items.Count - 1
                Dim idRiga As Label = Lista.Items(i).FindControl("idLabel")
                Dim dropLivelloAccesso As DropDownList = Lista.Items(i).FindControl("dropPermessi")

                Cmd.CommandText = "UPDATE permessi_operatori SET id_livello_accesso=" & dropLivelloAccesso.SelectedValue & " WHERE id=" & idRiga.Text
                Cmd.ExecuteNonQuery()
            Next

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Else
            'IN QUESTO CASO DEVO DISABILITARE L'INTERA FUNZIONALITA'-----------------------------------------------------------------------
            zUpdatefunction(1, IDFunzionalita, dropOperatori.SelectedValue)
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand()
            Cmd.Connection = Dbc

            For i = 0 To Lista.Items.Count - 1
                Dim idRiga As Label = Lista.Items(i).FindControl("idLabel")
                Dim dropLivelloAccesso As DropDownList = Lista.Items(i).FindControl("dropPermessi")

                Cmd.CommandText = "UPDATE permessi_operatori SET id_livello_accesso='1' WHERE id='" & idRiga.Text & "'"
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

    Private Sub zUpdatefunction(ByVal LivelloDiAccesso As Integer, ByVal IDFunzionalita As Integer, ByVal IDOperatore As Integer)
        Dim Sql As String
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        'SETTO A LIVELLO 3 L'ACCESSO PER LA FUNZIONALITA' 
        Sql = "UPDATE permessi_operatori SET id_livello_accesso=" & LivelloDiAccesso & " WHERE id_funzionalita=" & IDFunzionalita & " AND id_operatore='" & dropOperatori.SelectedValue & "'"
        Dim Cmd As New Data.SqlClient.SqlCommand(Sql, Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Open()
        Dbc = Nothing
    End Sub

    Protected Sub btnAggiorna_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAggiorna.Click
        'aggiornaParcoVeicoli()
        'aggiornaTabelle()
        'aggiornaOrdinativoLavori()
        'aggioraFunzioniVeicoli()
        'aggiornaGestioneListini()
        'aggiornaTabelleListini()
        'aggiornaGestioneStazioni()
        'aggiornaGestioneVAL()
        'aggiornaGestioneMulte()

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

        Libreria.genUserMsgBox(Me, "Salvataggio effettuato correttamente.")
    End Sub

End Class
