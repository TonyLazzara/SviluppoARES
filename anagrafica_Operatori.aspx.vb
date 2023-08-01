'Imports funzioni_comuni
Partial Class anagrafica_operatori
    Inherits System.Web.UI.Page

   

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack() Then
            listStazioni.DataBind()

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "116") = "1" Then
                Response.Redirect("default.aspx")
            End If
        End If

        If Not Page.IsPostBack Then            
        Else
            
            
        End If

    End Sub
    Dim funzioni As New funzioni_comuni

    Public Class ScegliOperatoreEventArgs
        Inherits System.EventArgs

        Private id As String
        Private m_nome_operatore As String
        Private m_cognome_operatore As String
        Private m_email_operatore As String
        Private m_username_operatore As String



        Public Property id_operatore() As String
            Get
                Return id
            End Get
            Set(ByVal value As String)
                id = value
            End Set
        End Property

        Public Property nome_operatore() As String
            Get
                Return m_nome_operatore
            End Get
            Set(ByVal value As String)
                m_nome_operatore = value
            End Set
        End Property

        Public Property cognome_operatore() As String
            Get
                Return m_cognome_operatore
            End Get
            Set(ByVal value As String)
                m_cognome_operatore = value
            End Set
        End Property

        Public Property email_operatore() As String
            Get
                Return m_email_operatore
            End Get
            Set(ByVal value As String)
                m_email_operatore = value
            End Set
        End Property
        Public Property username_operatore() As String
            Get
                Return m_username_operatore
            End Get
            Set(ByVal value As String)
                m_username_operatore = value
            End Set
        End Property


    End Class

    Public Delegate Sub scegliOperatoreEventHandler(ByVal sender As Object, ByVal e As EventArgs)
    Event scegliOperatore As EventHandler

    Private Sub OnScegliUtente(ByVal e As ScegliOperatoreEventArgs)
        RaiseEvent scegliOperatore(Me, e)
    End Sub

    Protected Sub ricerca()
        If dropAttive.SelectedValue = -1 Then
            query.Text = "SELECT TOP 100 id, nome, cognome, username, password, id_stazione,mail FROM operatori WITH(NOLOCK) WHERE  id>0 "
        Else
            query.Text = "SELECT TOP 100 id, nome, cognome, username, password, id_stazione,mail FROM operatori WITH(NOLOCK) WHERE attivo = " & dropAttive.SelectedValue & " and id>0 "
        End If


        If Trim(txtCercaNome.Text) <> "" Then
            query.Text = query.Text & " AND nome LIKE '%" & Replace(txtCercaNome.Text, "'", "''") & "%'"
        End If
        If Trim(txtCercaCognome.Text) <> "" Then
            query.Text = query.Text & " AND cognome LIKE '%" & Replace(txtCercaCognome.Text, "'", "''") & "%'"
        End If
        If Trim(txtCercaUsername.Text) <> "" Then
            query.Text = query.Text & " AND username LIKE '%" & Replace(txtCercaUsername.Text, "'", "''") & "%'"
        End If


        If dropCercaStazione.SelectedValue <> "-1" Then
            query.Text = query.Text & " AND ISNULL(id_stazione,'-1')='" & dropCercaStazione.SelectedValue & "'"
        End If

        If Trim(txtCercaEmail.Text) <> "" Then
            query.Text = query.Text & " AND mail LIKE '%" & Replace(txtCercaEmail.Text, "'", "''") & "%'"
        End If

        query.Text = query.Text & " ORDER BY cognome"

        SqlOperatori.SelectCommand = query.Text
        'Response.Write(query.Text)

        listConducenti.DataBind()

        nuovo_operatore.Visible = False
        risultati.Visible = True
        btnNuovoOperatore.Visible = True
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerca.Click
        ricerca()
        duplica.Visible = False
    End Sub

    Protected Sub btnNuovoOperatore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovoOperatore.Click
        btnSeleziona.Visible = False
        nuovo_operatore.Visible = True
        btnNuovoOperatore.Visible = False
        risultati.Visible = False

        pulisciCampi()
        form_operatore.Visible = True

        btnSalva.Visible = True
        btnSalva.Text = "Salva operatore"
    End Sub

    Protected Sub pulisciCampi()

        txtNome.Text = ""
        txtCognome.Text = ""
        txtusername.Text = ""
        txtPassword.Text = ""
        txtEmail.Text = ""
        listStazioni.SelectedValue = -1

    End Sub

    Public Sub annulla()
        nuovo_operatore.Visible = False
        btnNuovoOperatore.Visible = True
        risultati.Visible = True


        pulisciCampi()
    End Sub

    Protected Sub pulisciCampiDuplica()

        txtCognomeduplica_operatore.Text = ""
        txtNomeduplica_operatore.Text = ""
        txtusernameduplica_operatore.Text = ""
        txtPasswordduplica_operatore.Text = ""
        txtEmailduplica_operatore.Text = ""
        listStazioniduplica_operatore.SelectedValue = -1

    End Sub

    Public Sub annullaDuplica()
        duplica.Visible = False
        btnNuovoOperatore.Visible = True
        risultati.Visible = True


        pulisciCampiDuplica()
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click
        annulla()


        pulisciCampi()

    End Sub

    Protected Sub DisabilitaCampi(Valore As Boolean)

        ' attenzione se devi utilizzare seriamente questo metodo
        ' devi rivedere la loggica dei campi comune, provincia, nazione ecc.
       
        txtNome.Enabled = Valore
        txtCognome.Enabled = Valore
       
        txtEmail.Enabled = Valore
       
        txtPassword.Enabled = Valore
        txtEmail.Enabled = Valore
        listStazioni.Enabled = Valore

    End Sub

    Public Sub VisualizzaOperatore(id_riga As String)
        Trace.Write("VisualizzaOperatore")

        ' carico i dati dell'operatore
        FillOperatore(id_riga)

        ' visualizzo solo il pannello dei dati operatore
        nuovo_operatore.Visible = True

        ' nascondo gli altri pannelli
        cerca_operatore.Visible = False
        risultati.Visible = False

        ' nascondo tutti i pulsanti di modifica...
        btnSeleziona.Visible = False
        btnSalva.Visible = False
        btnAnnulla.Visible = False

        tab_titolo.Width = "100%"
        table1.Width = "100%"
        table2.Width = "100%"
        table3.Width = "100%"

        'DisabilitaCampi(False)
    End Sub

    Protected Sub FillOperatore(id_riga As String)
        id_operatore.Text = id_riga
        form_operatore.Visible = True
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM operatori WHERE id=" & id_riga & "", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()
        Rs.Read()
        txtNome.Text = Rs("nome") & ""
        txtCognome.Text = Rs("cognome") & ""
        txtEmail.Text = Rs("mail") & ""
        txtPassword.Text = Rs("password") & ""
        txtusername.Text = Rs("username") & ""
        listStazioni.SelectedValue = Rs("id_stazione")
        txtEmail.Text = Rs("mail") & ""
        If Rs("attivo") Then
            chkBxAttivo.checked = True
        Else
            chkBxAttivo.checked = False
        End If

        Rs.Close()
        Rs = Nothing

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub



    Protected Sub listConducenti_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listConducenti.ItemCommand
        Dim idRiga As Label = e.Item.FindControl("idLabel")
        Dim Cognome As Label = e.Item.FindControl("cognomeLabel")
        Dim Nome As Label = e.Item.FindControl("nomeLabel")

        If e.CommandName = "ModificaOperatore" Then            
            risultati.Visible = False
            nuovo_operatore.Visible = True
            Dim id_riga As Label = e.Item.FindControl("idLabel")

            If Request.Url().ToString.Contains("prenotazioni.aspx") Or Request.Url().ToString.Contains("preventivi.aspx") Or Request.Url().ToString.Contains("contratti.aspx") Or Request.Url().ToString.Contains("gestione_multe.aspx") Then
                btnSeleziona.Visible = True
            End If


            btnSalva.Text = "Modifica operatore"
            FillOperatore(id_riga.Text)
        ElseIf e.CommandName = "DuplicaOperatore" Then
            'Response.Write(idRiga.Text)

            duplicaPermessi(idRiga.Text, Cognome.Text, Nome.Text)

        ElseIf e.CommandName = "EliminaOperatore" Then
            Dim id_riga As Label = e.Item.FindControl("idLabel")
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM operatori WHERE id=" & id_riga.Text & "", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            Libreria.genUserMsgBox(Page, "Operatore eliminato correttamente")

            listConducenti.DataBind()
        End If
    End Sub

    Protected Sub salvataggio_operatore(Optional ByVal provenienza As String = "salva")



        If btnSalva.Text = "Modifica operatore" Then

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String

            If chkBxAttivo.Checked = False Then
                sqlStr = "UPDATE operatori SET nome = '" & Replace(txtNome.Text, "'", "''") & "', cognome = '" & Replace(txtCognome.Text, "'", "''") & "', username = '" & Replace(txtusername.Text, "'", "''") & "', password = 'PassSRC1!', id_stazione='" & listStazioni.SelectedValue & "', mail = '" & Replace(txtEmail.Text, "'", "''") & "', attivo=0 "
                sqlStr = sqlStr & " WHERE id = " & id_operatore.Text & ""

            Else
                sqlStr = "UPDATE operatori SET nome = '" & Replace(txtNome.Text, "'", "''") & "', cognome = '" & Replace(txtCognome.Text, "'", "''") & "', username = '" & Replace(txtusername.Text, "'", "''") & "', password = '" & Replace(txtPassword.Text, "'", "''") & "', id_stazione='" & listStazioni.SelectedValue & "', mail = '" & Replace(txtEmail.Text, "'", "''") & "', attivo=1 "
                sqlStr = sqlStr & " WHERE id = " & id_operatore.Text & ""

            End If
            
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            If provenienza <> "seleziona" Then
                Libreria.genUserMsgBox(Page, "Operatore modificato correttamente")
            End If
        Else
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String



            sqlStr = "INSERT INTO operatori (nome, cognome, username, password, id_stazione, mail,attivo) VALUES ('" & Replace(txtNome.Text, "'", "''") & "','" & Replace(txtCognome.Text, "'", "''") & "','" & Replace(txtusername.Text, "'", "''") & "','" & Replace(txtPassword.Text, "'", "''") & "','" & listStazioni.SelectedValue & "','" & Replace(txtEmail.Text, "'", "''") & "','1')"
            'Response.Write(sqlStr)
            'Response.End()



            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            sqlStr = "SELECT @@IDENTITY FROM operatori WITH(NOLOCK)"

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            id_operatore.Text = Cmd.ExecuteScalar()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            If provenienza <> "seleziona" Then
                Libreria.genUserMsgBox(Page, "Operatore inserito correttamente")
            End If
        End If



        If Request.Url().ToString.Contains("prenotazioni.aspx") Or Request.Url().ToString.Contains("preventivi.aspx") Or Request.Url().ToString.Contains("contratti.aspx") Or Request.Url().ToString.Contains("gestione_multe.aspx") Then
            btnSeleziona.Visible = True
            btnSalva.Visible = False


        Else
            annulla()
        End If
        listConducenti.DataBind()
    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click

        salvataggio_operatore()
        btnSalva.Text = "Modifica Operatore"

        'SE STO SALVANDO DAL CALCOLO SELEZIONO DIRETTAMENTE L'OPERATORE

        Dim e1 As New ScegliOperatoreEventArgs
        e1.nome_operatore = txtNome.Text
        e1.cognome_operatore = txtCognome.Text
        e1.email_operatore = txtEmail.Text
        e1.username_operatore = txtusername.Text


        annulla()
        OnScegliUtente(e1)


    End Sub


    Public Sub cerca_esterno(ByVal testo As String)
        txtCercaNome.Text = testo

        ricerca()
    End Sub



    Protected Sub btnSeleziona_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeleziona.Click




        salvataggio_operatore("seleziona")

        Dim e1 As New ScegliOperatoreEventArgs
        e1.nome_operatore = txtNome.Text
        e1.cognome_operatore = txtCognome.Text
        e1.email_operatore = txtEmail.Text
        e1.username_operatore = txtusername.Text
        e1.id_operatore = id_operatore.Text
        annulla()
        OnScegliUtente(e1)

    End Sub

    Protected Sub listConducenti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listConducenti.ItemDataBound
        If Request.Url().ToString.Contains("prenotazioni.aspx") Or Request.Url().ToString.Contains("preventivi.aspx") Or Request.Url().ToString.Contains("contratti.aspx") Or Request.Url().ToString.Contains("gestione_multe.aspx") Then
            e.Item.FindControl("EliminaOperatore").Visible = False
        End If

        Dim id_stazione As Label = e.Item.FindControl("stazioneLabel")
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT nome_stazione FROM stazioni WHERE id=" & id_stazione.Text & "", Dbc)
        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()
        Rs.Read()
        If IsDBNull(Rs("nome_stazione")) = False Then
            If id_stazione.Text <> "-1" Then
                id_stazione.Text = Rs("nome_stazione")
            End If
        End If
        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        Dim idLabel As Label = e.Item.FindControl("idLabel")
        Dim Attivo As Label = e.Item.FindControl("lblAttivo")

        Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc2.Open()
        Dim Cmd2 As New Data.SqlClient.SqlCommand("SELECT * FROM operatori WHERE id=" & idLabel.Text & "", Dbc2)
        Dim Rs2 As Data.SqlClient.SqlDataReader
        Rs2 = Cmd2.ExecuteReader()
        If Rs2.HasRows Then
            Do While Rs2.Read
                If Rs2("attivo") Then
                    Attivo.Text = "Si"
                Else
                    Attivo.Text = "No"
                    Attivo.ForeColor = Drawing.Color.Red
                End If
            Loop
        Else

        End If
        
        Rs2.Close()
        Rs2 = Nothing
        Cmd2.Dispose()
        Cmd2 = Nothing
        Dbc2.Close()
        Dbc2.Dispose()
        Dbc2 = Nothing
    End Sub

    Protected Sub duplicaPermessi(ByVal idRiga As String, ByVal Cognome As String, ByVal Nome As String)
        risultati.Visible = False
        duplica.Visible = True

        pulisciCampiDuplica()
        form_duplica.Visible = True
        lblIdDuplicatoreScelto.Text = idRiga
        lblNominativo.Text = Cognome & " " & Nome        
    End Sub

    Protected Sub btnSalvaduplica_operatore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvaduplica_operatore.Click
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String

        sqlStr = "INSERT INTO operatori (nome, cognome, username, password, id_stazione, mail,attivo) VALUES ('" & Replace(txtNomeduplica_operatore.Text, "'", "''") & "','" & Replace(txtCognomeduplica_operatore.Text, "'", "''") & "','" & Replace(txtusernameduplica_operatore.Text, "'", "''") & "','" & Replace(txtPasswordduplica_operatore.Text, "'", "''") & "','" & listStazioniduplica_operatore.SelectedValue & "','" & Replace(txtEmailduplica_operatore.Text, "'", "''") & "','1')"
        'Response.Write(sqlStr)
        'Response.End()



        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        sqlStr = "SELECT @@IDENTITY FROM operatori WITH(NOLOCK)"

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        id_operatore.Text = Cmd.ExecuteScalar()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        'Duplicare i permessi       
        Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc2.Open()

        Dim Cmd2 As New Data.SqlClient.SqlCommand("SELECT * FROM permessi_operatori WITH(NOLOCK) WHERE id_operatore=" & lblIdDuplicatoreScelto.Text, Dbc2)
        'Response.Write(Cmd2.CommandText & "<br><br>")
        'Response.End()

        Dim Rs2 As Data.SqlClient.SqlDataReader
        Rs2 = Cmd2.ExecuteReader()
        If Rs2.HasRows Then
            Do While Rs2.Read
                Dim Dbc3 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc3.Open()

                Dim sqlStr3 As String

                sqlStr3 = "INSERT INTO permessi_operatori (id_operatore, id_funzionalita, id_livello_accesso) VALUES ('" & id_operatore.Text & "','" & Rs2("id_funzionalita") & "','" & Rs2("id_livello_accesso") & "')"
                'Response.Write(sqlStr3 & "<br>")
                'Response.End()

                Dim Cmd3 As New Data.SqlClient.SqlCommand(sqlStr3, Dbc3)
                Cmd3.ExecuteNonQuery()

                Cmd3.Dispose()
                Cmd3 = Nothing
                Dbc3.Close()
                Dbc3.Dispose()
                Dbc3 = Nothing
            Loop
        Else

        End If

        Rs2.Close()
        Dbc2.Close()
        Rs2 = Nothing
        Dbc2 = Nothing

        Dim e1 As New ScegliOperatoreEventArgs
        e1.nome_operatore = txtNomeduplica_operatore.Text
        e1.cognome_operatore = txtCognomeduplica_operatore.Text
        e1.email_operatore = txtEmailduplica_operatore.Text
        e1.username_operatore = txtusernameduplica_operatore.Text

        annullaDuplica()
        OnScegliUtente(e1)
    End Sub

    Protected Sub btnAnnullaDuplica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnullaDuplica.Click
        annullaDuplica()

        pulisciCampiDuplica()
    End Sub
End Class


