Partial Class gestione_danni_sinistro_gestito_da
    Inherits System.Web.UI.UserControl

    Public Class ScegliGestitoDaEventArgs
        Inherits System.EventArgs

        Private my_id_gestito_da As String

        Public Property id_gestito_da() As String
            Get
                Return my_id_gestito_da
            End Get
            Set(ByVal value As String)
                my_id_gestito_da = value
            End Set
        End Property
    End Class

    Public Delegate Sub scegliTipologiaEventHandler(ByVal sender As Object, ByVal e As EventArgs)
    Event scegliGestitoDa As EventHandler

    Private Sub OnScegliGestitoDa(ByVal e As ScegliGestitoDaEventArgs)
        RaiseEvent scegliGestitoDa(Me, e)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblOrderBY.Text = " ORDER BY descrizione ASC"
            lblQuery.Text = "SELECT id, descrizione  FROM sinistri_gestito_Da WITH(NOLOCK) WHERE id>0 "

            livelloAccesso.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneSinistri)

            If livelloAccesso.Text = "3" Then
                btnSalva.Enabled = True
            End If

        End If
        sqlGestitoDa.SelectCommand = lblQuery.Text & lblOrderBY.Text
    End Sub

    Protected Sub listGestitoDa_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listGestitoDa.ItemDataBound
        Dim elimina As ImageButton = e.Item.FindControl("btnElimina")
        Dim vedi As ImageButton = e.Item.FindControl("btnVedi")

        If livelloAccesso.Text = "2" Then
            elimina.Visible = False
            vedi.Visible = False
        End If
    End Sub

    Protected Function gestitoDaNonEsistente(ByVal gestitoDa As String, Optional ByVal idModifica As String = "0") As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM sinistri_gestito_da WITH(NOLOCK) WHERE  descrizione='" & Replace(gestitoDa, "'", "''") & "' AND id<>'" & idModifica & "'", Dbc)
        Dim test As String = Cmd.ExecuteScalar

        If test <> "" Then
            gestitoDaNonEsistente = False
        Else
            gestitoDaNonEsistente = True
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function



    Protected Sub eliminaGestitoDa(ByVal idGestitoDa As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM sinistri_gestito_da WHERE  id='" & idGestitoDa & "'", Dbc)

        Try
            Cmd.ExecuteNonQuery()
            Libreria.genUserMsgBox(Page, "Elemento eliminato correttamente.")
        Catch ex As Exception
            Libreria.genUserMsgBox(Page, "Impossibile eliminare l'elemento specificato.")
        End Try

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub



    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        If btnSalva.Text = "Salva" Then
            If Trim(txtGestitoDa.Text) <> "" Then
                If gestitoDaNonEsistente(Trim(txtGestitoDa.Text)) Then
                    Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()

                    Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO sinistri_gestito_da (descrizione) VALUES ('" & Replace(txtGestitoDa.Text, "'", "''") & "')", Dbc)
                    Cmd.ExecuteNonQuery()

                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing

                    txtGestitoDa.Text = ""
                    listGestitoDa.DataBind()
                    Libreria.genUserMsgBox(Page, "Elemento memorizzato correttamente.")
                Else
                    Libreria.genUserMsgBox(Page, "Elemento già esistente.")
                End If
            Else
                Libreria.genUserMsgBox(Page, "Specificare il nome dell'elemento da memorizzare.")
            End If
        ElseIf btnSalva.Text = "Modifica" Then
            If Trim(txtGestitoDa.Text) <> "" Then
                If gestitoDaNonEsistente(Trim(txtGestitoDa.Text), id_modifica.Text) Then

                    Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()

                    Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE sinistri_gestito_da SET descrizione='" & Replace(txtGestitoDa.Text, "'", "''") & "' WHERE id='" & id_modifica.Text & "'", Dbc)
                    Cmd.ExecuteNonQuery()

                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing

                    annulla()
                    listGestitoDa.DataBind()
                    Libreria.genUserMsgBox(Page, "Elemento modificato correttamente.")
                Else
                    Libreria.genUserMsgBox(Page, "Elemento già esistente.")
                End If
            Else
                Libreria.genUserMsgBox(Page, "Specificare il nome dell'elemento da memorizzare.")
            End If
        End If

    End Sub

    Protected Sub cerca(ByVal order_by As String)
        listGestitoDa.Visible = True

        lblQuery.Text = "SELECT id, descrizione FROM sinistri_gestito_da WITH(NOLOCK) WHERE id>0 "

        If txtGestitoDa.Text <> "" Then
            lblQuery.Text = lblQuery.Text & " AND descrizione LIKE '" & Replace(txtGestitoDa.Text, "'", "''") & "%' "
        End If

        sqlGestitoDa.SelectCommand = lblQuery.Text & order_by
        lblOrderBY.Text = order_by
    End Sub


    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerca.Click
        cerca(lblOrderBY.Text)
    End Sub

    Protected Sub annulla()
        id_modifica.Text = ""
        txtGestitoDa.Text = ""
        listGestitoDa.Visible = True
        btnCerca.Visible = True
        btnAnnulla.Visible = False
        btnSeleziona.Visible = False
        btnSalva.Text = "Salva"
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click
        annulla()
    End Sub

    Protected Sub listGestitoDa_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listGestitoDa.ItemCommand
        If e.CommandName = "elimina" Then
            Dim idGestitoDa As Label = e.Item.FindControl("idLabel")

            eliminaGestitoDa(idGestitoDa.Text)
            listGestitoDa.DataBind()
        ElseIf e.CommandName = "vedi" Then
            Dim idMarca As Label = e.Item.FindControl("idLabel")
            Dim descrizione As Label = e.Item.FindControl("descrizioneLabel")
            Dim cod_casa As Label = e.Item.FindControl("cod_casa")

            id_modifica.Text = idMarca.Text
            txtGestitoDa.Text = descrizione.Text
            listGestitoDa.Visible = False
            btnCerca.Visible = False
            btnAnnulla.Visible = True
            btnSalva.Text = "Modifica"
            If Request.UrlReferrer.ToString.Contains("sinistri.aspx") Then
                btnSeleziona.Visible = True
            End If
        ElseIf e.CommandName = "order_by_descrizione" Then
            If lblOrderBY.Text = " ORDER BY descrizione DESC" Then
                cerca(" ORDER BY descrizione ASC")
            ElseIf lblOrderBY.Text = " ORDER BY descrizione ASC" Then
                cerca(" ORDER BY descrizione DESC")
            Else
                cerca(" ORDER BY descrizione ASC")
            End If
        End If
    End Sub

    Protected Sub btnSeleziona_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeleziona.Click
        Dim e1 As New ScegliGestitoDaEventArgs
        e1.id_gestito_da = id_modifica.Text

        annulla()
        OnScegliGestitoDa(e1)
    End Sub
End Class
