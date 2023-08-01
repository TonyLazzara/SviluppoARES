Imports funzioni_comuni

Partial Class allegati_contratti
    Inherits System.Web.UI.Page

    Dim funzioni As New funzioni_comuni

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblOrderBY.Text = " ORDER BY descrizione ASC"

            lblQuery.Text = "SELECT id_cnt_pren_allegati_tipo, descrizione FROM contratti_prenotazioni_allegati_tipo WITH(NOLOCK) WHERE id_cnt_pren_allegati_tipo>0"

            livelloAccesso.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Fatturazione)

            If livelloAccesso.Text <> "3" Then
                Response.Redirect("default.aspx")
            End If

        End If

        sqlAllegati.SelectCommand = lblQuery.Text & lblOrderBY.Text
    End Sub

    Protected Function allegatoNonEsistente(ByVal descrizione As String, Optional ByVal idModifica As String = "0") As Boolean

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id_cnt_pren_allegati_tipo FROM contratti_prenotazioni_allegati_tipo WITH(NOLOCK) WHERE  descrizione='" & Replace(descrizione, "'", "''") & "' AND id_cnt_pren_allegati_tipo<>'" & idModifica & "'", Dbc)
        Dim test As String = Cmd.ExecuteScalar

        If test <> "" Then
            allegatoNonEsistente = False
        Else
            allegatoNonEsistente = True
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub listAcquirenti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listAllegati.ItemDataBound
        Dim elimina As ImageButton = e.Item.FindControl("btnElimina")
        Dim vedi As ImageButton = e.Item.FindControl("btnVedi")

        If livelloAccesso.Text = "2" Then
            elimina.Visible = False
            vedi.Visible = False
        End If
    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        If btnSalva.Text = "Salva" Then
            If Trim(txtTipoAllegato.Text) <> "" Then
                If allegatoNonEsistente(Trim(txtTipoAllegato.Text)) Then
                    Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()

                    Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO contratti_prenotazioni_allegati_tipo (descrizione) VALUES ('" & Replace(txtTipoAllegato.Text, "'", "''") & "')", Dbc)
                    Cmd.ExecuteNonQuery()

                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing

                    txtTipoAllegato.Text = ""
                    listAllegati.DataBind()

                    Libreria.genUserMsgBox(Page, "Tipo Allegato memorizzato correttamente.")
                Else
                    Libreria.genUserMsgBox(Page, "Tipo Allegato già presente.")
                End If
            Else
                Libreria.genUserMsgBox(Page, "Specificare la nuova tipologia di allegato.")
            End If
        Else
            If Trim(txtTipoAllegato.Text) <> "" Then
                If allegatoNonEsistente(Trim(txtTipoAllegato.Text), id_modifica.Text) Then
                    Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()

                    Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE contratti_prenotazioni_allegati_tipo SET descrizione='" & Replace(txtTipoAllegato.Text, "'", "''") & "' WHERE id_cnt_pren_allegati_tipo='" & id_modifica.Text & "'", Dbc)
                    Cmd.ExecuteNonQuery()

                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing

                    annulla()
                    listAllegati.DataBind()
                    Libreria.genUserMsgBox(Page, "Tipo Allegato modificato correttamente.")
                Else
                    Libreria.genUserMsgBox(Page, "Tipo Allegato già presente.")
                End If
            Else
                Libreria.genUserMsgBox(Page, "Specificare la nuova tipologia di allegato.")
            End If
        End If
    End Sub

    Protected Sub eliminaAllegato(ByVal idAllegato As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM contratti_prenotazioni_allegati_tipo WHERE  id_cnt_pren_allegati_tipo='" & idAllegato & "'", Dbc)

        Try
            Cmd.ExecuteNonQuery()
            Libreria.genUserMsgBox(Page, "Tipo Allegato eliminato correttamente.")
        Catch ex As Exception
            Libreria.genUserMsgBox(Page, "Impossibile eliminare il Tipo Allegato specificato.")
        End Try

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub listAllegati_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listAllegati.ItemCommand
        If e.CommandName = "elimina" Then
            Dim idAllegato As Label = e.Item.FindControl("idLabel")
            eliminaAllegato(idAllegato.Text)
            listAllegati.DataBind()
        ElseIf e.CommandName = "vedi" Then
            Dim idAllegato As Label = e.Item.FindControl("idLabel")
            Dim descrizione As Label = e.Item.FindControl("descrizione")

            id_modifica.Text = idAllegato.Text
            txtTipoAllegato.Text = descrizione.Text

            listAllegati.Visible = False
            btnCerca.Visible = False
            btnAnnulla.Visible = True
            btnSalva.Text = "Modifica"
        ElseIf e.CommandName = "order_by_ragsoc" Then
            If lblOrderBY.Text = " ORDER BY descrizione DESC" Then
                cerca(" ORDER BY descrizione ASC")
            ElseIf lblOrderBY.Text = " ORDER BY descrizione ASC" Then
                cerca(" ORDER BY descrizione DESC")
            Else
                cerca(" ORDER BY descrizione ASC")
            End If
        End If
    End Sub


    Protected Sub cerca(ByVal order_by As String)
        listAllegati.Visible = True

        lblQuery.Text = "SELECT id_cnt_pren_allegati_tipo, descrizione FROM contratti_prenotazioni_allegati_tipo WITH(NOLOCK) WHERE id_cnt_pren_allegati_tipo>0"

        If txtTipoAllegato.Text <> "" Then
            lblQuery.Text = lblQuery.Text & " AND descrizione LIKE '" & Replace(txtTipoAllegato.Text, "'", "''") & "%'"
        End If

        sqlAllegati.SelectCommand = lblQuery.Text & order_by
        lblOrderBY.Text = order_by
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerca.Click
        cerca(lblOrderBY.Text)
    End Sub

    Protected Sub annulla()
        id_modifica.Text = ""
        txtTipoAllegato.Text = ""

        listAllegati.Visible = True
        btnCerca.Visible = True
        btnAnnulla.Visible = False
        btnSalva.Text = "Salva"
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click
        annulla()
    End Sub

End Class
