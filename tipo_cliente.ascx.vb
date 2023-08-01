Imports funzioni_comuni

Partial Class tabelle_listini_tipo_cliente
    Inherits System.Web.UI.UserControl

    Dim funzioni As New funzioni_comuni

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler anagrafica_ditte.scegliDitta, AddressOf scegli_ditta

        If Not Page.IsPostBack Then

            livelloAccesso.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.tipoClienti)

            If livelloAccesso.Text = "3" Then
                btnSalva.Enabled = True
            End If

        End If
    End Sub

    Private Sub scegli_ditta(ByVal sender As Object, ByVal e As anagrafica_anagrafica_ditte.ScegliDittaEventArgs)
        txtDitta.Text = e.ragione_sociale
        id_ditta.Text = e.id_ditta

        anagrafica_ditte.Visible = False
        btnScegliDitta.Text = "Scegli"
    End Sub


    Protected Function tipoClienteNonPresente(ByVal tipo_cliente As String, ByVal id_x_modifica As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM clienti_tipologia WITH(NOLOCK) WHERE  descrizione='" & Replace(tipo_cliente, "'", "''") & "' And id<>'" & id_x_modifica & "'", Dbc)
        Dim test As String = Cmd.ExecuteScalar

        If test <> "" Then
            tipoClienteNonPresente = False
        Else
            tipoClienteNonPresente = True
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        If btnSalva.Text = "Salva" Then
            If Trim(txtTipoCliente.Text) = "" Then
                Libreria.genUserMsgBox(Page, "Specificare il nome della tipologia di cliente.")
            ElseIf dropClienteBroker.SelectedValue = "-1" Then
                Libreria.genUserMsgBox(Page, "Specificare se la tipologia di cliente si riferisce ad un Broker o meno.")
            ElseIf dropAgenziaDiViaggio.SelectedValue = "-1" Then
                Libreria.genUserMsgBox(Page, "Specificare se la tipologia di cliente si riferisce ad un Agenzia di Viaggio o meno.")
            ElseIf dropClienteBroker.SelectedValue = "1" And dropAgenziaDiViaggio.SelectedValue = "1" Then
                Libreria.genUserMsgBox(Page, "Attenzione: una tipologia di cliente non può essere contemporaneamente Broker ed Agenzia di viaggio.")
                'ElseIf dropClienteBroker.SelectedValue = "1" And id_ditta.Text = "0" Then
                '    Libreria.genUserMsgBox(Page, "Attenzione: nel caso di Broker è necessario specificare la ditta a cui intestare le fatture.")
            ElseIf anagrafica_ditte.Visible Then
                Libreria.genUserMsgBox(Page, "E' necessario completare o annullare la modifica della ditta prima di continuare.")
            Else
                If tipoClienteNonPresente(Trim(txtTipoCliente.Text), "0") Then
                    Dim estensione As String
                    Dim rimborsabili As String

                    If dropClienteBroker.SelectedValue = "0" Then
                        estensione = "NULL"
                        rimborsabili = "NULL"
                    ElseIf dropClienteBroker.SelectedValue = "1" Then
                        estensione = "'" & dropEstensione.SelectedValue & "'"
                        rimborsabili = "'" & dropGGRimborsabili.SelectedValue & "'"
                    End If

                    Dim idDitta As String
                    If id_ditta.Text <> "" Then
                        idDitta = "'" & id_ditta.Text & "'"
                    Else
                        idDitta = "NULL"
                    End If

                    Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()

                    Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO clienti_tipologia (descrizione,agenzia_di_viaggio, broker, estensioni_sempre_a_carico_del_broker,giorni_non_usufruiti_rimborsabili, id_ditta) VALUES ('" & Replace(txtTipoCliente.Text, "'", "''") & "','" & dropAgenziaDiViaggio.SelectedValue & "','" & dropClienteBroker.SelectedValue & "'," & estensione & "," & rimborsabili & "," & idDitta & ")", Dbc)
                    Cmd.ExecuteNonQuery()

                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing

                    id_ditta.Text = ""
                    txtDitta.Text = ""
                    txtTipoCliente.Text = ""
                    dropAgenziaDiViaggio.SelectedValue = "-1"
                    dropClienteBroker.SelectedValue = "-1"
                    dropEstensione.SelectedValue = "0"
                    listTipoClienti.DataBind()

                    Libreria.genUserMsgBox(Page, "Tipologia di Cliente salvata correttamente.")
                Else
                    Libreria.genUserMsgBox(Page, "Tipologia di Cliente già esistente.")
                End If
            End If
        ElseIf btnSalva.Text = "Modifica" Then
            If Trim(txtTipoCliente.Text) = "" Then
                Libreria.genUserMsgBox(Page, "Specificare il nome della tipologia di cliente.")
            ElseIf dropClienteBroker.SelectedValue = "-1" Then
                Libreria.genUserMsgBox(Page, "Specificare se la tipologia di cliente si riferisce ad un Broker o meno.")
            ElseIf dropAgenziaDiViaggio.SelectedValue = "-1" Then
                Libreria.genUserMsgBox(Page, "Specificare se la tipologia di cliente si riferisce ad un Agenzia di Viaggio o meno.")
            ElseIf dropClienteBroker.SelectedValue = "1" And dropAgenziaDiViaggio.SelectedValue = "1" Then
                Libreria.genUserMsgBox(Page, "Attenzione: una tipologia di cliente non può essere contemporaneamente Broker ed Agenzia di viaggio.")
                'ElseIf dropClienteBroker.SelectedValue = "1" And id_ditta.Text = "0" Then
                '    Libreria.genUserMsgBox(Page, "Attenzione: nel caso di Broker è necessario specificare la ditta a cui intestare le fatture.")
            ElseIf anagrafica_ditte.Visible Then
                Libreria.genUserMsgBox(Page, "E' necessario completare o annullare la modifica della ditta prima di continuare.")
            Else
                If tipoClienteNonPresente(Trim(txtTipoCliente.Text), idModifica.Text) Then
                    Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()

                    Dim estensione As String
                    Dim rimborsabili As String

                    If dropClienteBroker.SelectedValue = "0" Then
                        estensione = "NULL"
                        rimborsabili = "NULL"
                    ElseIf dropClienteBroker.SelectedValue = "1" Then
                        estensione = "'" & dropEstensione.SelectedValue & "'"
                        rimborsabili = "'" & dropGGRimborsabili.SelectedValue & "'"
                    End If

                    Dim idDitta As String
                    If id_ditta.Text <> "" Then
                        idDitta = "'" & id_ditta.Text & "'"
                    Else
                        idDitta = "NULL"
                    End If

                    Dim sqlStr As String = "UPDATE clienti_tipologia SET descrizione='" & Replace(txtTipoCliente.Text, "'", "''") & "'," & _
                    "agenzia_di_viaggio='" & dropAgenziaDiViaggio.SelectedValue & "', broker='" & dropClienteBroker.SelectedValue & "'," & _
                    "estensioni_sempre_a_carico_del_broker=" & estensione & ", giorni_non_usufruiti_rimborsabili=" & rimborsabili & "," & _
                    "id_ditta=" & idDitta & " " & _
                    "WHERE id='" & idModifica.Text & "'"

                    Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.ExecuteNonQuery()

                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing

                    pulisci_campi()

                    listTipoClienti.DataBind()

                    Libreria.genUserMsgBox(Page, "Tipologia di Cliente modificata correttamente.")
                Else
                    Libreria.genUserMsgBox(Page, "Tipologia di Cliente già esistente.")
                End If
            End If
        End If
    End Sub

    Protected Sub listTipoClienti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listTipoClienti.ItemDataBound
        Dim elimina As ImageButton = e.Item.FindControl("btnElimina")
        Dim broker As Label = e.Item.FindControl("broker")
        Dim estensioni As Label = e.Item.FindControl("estensioni")
        Dim agenzia_di_viaggio As Label = e.Item.FindControl("agenzia_di_viaggio")
        Dim rimborsabili As Label = e.Item.FindControl("rimborsabili")

        If agenzia_di_viaggio.Text = "True" Then
            agenzia_di_viaggio.Text = "SI"
        Else
            agenzia_di_viaggio.Text = "NO"
        End If

        If broker.Text = "True" Then
            broker.Text = "SI"
        Else
            broker.Text = "NO"
        End If

        If estensioni.Text = "True" Then
            estensioni.Text = "Sempre a carico del broker"
        ElseIf estensioni.Text = "False" Then
            estensioni.Text = "Cliente o Broker"
        End If

        If rimborsabili.Text = "True" Then
            rimborsabili.Text = "Si"
        ElseIf rimborsabili.Text = "False" Then
            rimborsabili.Text = "No"
        End If

        If livelloAccesso.Text = "2" Then
            elimina.Visible = False
        End If
    End Sub

    Protected Function eliminaTipoCliente(ByVal idTipoCliente As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM clienti_tipologia WHERE  id='" & idTipoCliente & "'", Dbc)

        Try
            Cmd.ExecuteNonQuery()
            eliminaTipoCliente = True
        Catch ex As Exception
            eliminaTipoCliente = False
        End Try

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub listTipoClienti_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listTipoClienti.ItemCommand
        If e.CommandName = "elimina" Then
            Dim idTipoCliente As Label = e.Item.FindControl("idLabel")
            If eliminaTipoCliente(idTipoCliente.Text) Then
                listTipoClienti.DataBind()
                Libreria.genUserMsgBox(Page, "Tipologia di cliente eliminata correttamente.")
            Else
                Libreria.genUserMsgBox(Page, "Impossibile eliminare.")
            End If
        ElseIf e.CommandName = "vedi" Then
            Dim idLabel As Label = e.Item.FindControl("idLabel")
            Dim broker As Label = e.Item.FindControl("broker")
            Dim estensioni As Label = e.Item.FindControl("estensioni_db")
            Dim descrizione As Label = e.Item.FindControl("descrizione")
            Dim agenzia_di_viaggio As Label = e.Item.FindControl("agenzia_di_viaggio")
            Dim rimborsabili As Label = e.Item.FindControl("rimborsabili_db")
            Dim idDitta As Label = e.Item.FindControl("id_ditta")
            Dim ditta As Label = e.Item.FindControl("ditta")

            id_ditta.Text = idDitta.Text
            txtDitta.Text = ditta.Text

            anagrafica_ditte.Visible = False
            btnScegliDitta.Text = "Scegli"

            idModifica.Text = idLabel.Text
            txtTipoCliente.Text = descrizione.Text

            If broker.Text = "SI" Then
                dropClienteBroker.SelectedValue = "1"
            Else
                dropClienteBroker.SelectedValue = "0"
            End If

            If agenzia_di_viaggio.Text = "SI" Then
                dropAgenziaDiViaggio.SelectedValue = "1"
            Else
                dropAgenziaDiViaggio.SelectedValue = "0"
            End If

            'E' POSSIBILE MODIFICARE L'INFORMAZIONE "BROKER SI/NO" / "AGENZIA DI VIAGGIO SI/NO" SOLO SE LA TIPOLOGIA DI CLIENTE 
            'NON E' GIA' STATA COLLEGATA AD UNA 
            'TARIFFA
            If tipologiaClienteCollegataATariffa(idLabel.Text) Then
                dropClienteBroker.Enabled = False
                dropAgenziaDiViaggio.Enabled = False
            End If

            If estensioni.Text = "True" Then
                dropEstensione.SelectedValue = "1"
            Else
                dropEstensione.SelectedValue = "0"
            End If

            If rimborsabili.Text = "True" Then
                dropGGRimborsabili.SelectedValue = "1"
            Else
                dropGGRimborsabili.SelectedValue = "0"
            End If

            listTipoClienti.Visible = False
            btnSalva.Text = "Modifica"
            btnAnnulla.Visible = True
        End If
    End Sub

    Protected Function tipologiaClienteCollegataATariffa(ByVal id_tipo_cliente As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM tariffe_X_fonti WHERE id_tipologia_cliente='" & id_tipo_cliente & "'", Dbc)
        Dim test As String = Cmd.ExecuteScalar

        If test = "" Then
            tipologiaClienteCollegataATariffa = False
        Else
            tipologiaClienteCollegataATariffa = True
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub pulisci_campi()
        idModifica.Text = ""
        txtTipoCliente.Text = ""
        dropClienteBroker.SelectedValue = "-1"
        dropClienteBroker.Enabled = True
        dropEstensione.SelectedValue = "0"
        dropGGRimborsabili.SelectedValue = "0"
        id_ditta.Text = ""
        txtDitta.Text = ""

        listTipoClienti.Visible = True
        btnSalva.Text = "Salva"
        btnAnnulla.Visible = False
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click
        pulisci_campi()
    End Sub

    Protected Sub btnScegliDitta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScegliDitta.Click
        If btnScegliDitta.Text = "Scegli" Then
            anagrafica_ditte.Visible = True
            btnScegliDitta.Text = "Annulla"
        ElseIf btnScegliDitta.Text = "Annulla" Then
            anagrafica_ditte.Visible = False
            btnScegliDitta.Text = "Scegli"
        End If

    End Sub
End Class
