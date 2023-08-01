
Imports System.Data

Partial Class vbcode
    Inherits System.Web.UI.Page
    Dim idrec As String
    Dim id_user As String
    Dim service As String

    Dim tipo_indice As String

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If Request.Cookies("SicilyRentCar") Is Nothing Then
            Response.Redirect("default.aspx")
        End If


        id_user = Request.Cookies("SicilyRentCar").Item("id_utente")



        Dim sql As String

        If Not Page.IsPostBack Then


            Try
                sql = "SELECT condizioni_righe_costi_periodi.id, condizioni_righe_costi_periodi.id_elemento, condizioni_righe_costi_periodi.data_pickup, condizioni_righe_costi_periodi.applicabilita_da, condizioni_righe_costi_periodi.applicabilita_a, condizioni_righe_costi_periodi.costo, "
                sql += "condizioni_righe_costi_periodi.tipo_costo, condizioni_elementi.id AS Expr1, condizioni_elementi.descrizione "
                sql += "From condizioni_righe_costi_periodi INNER JOIN condizioni_elementi ON condizioni_righe_costi_periodi.id_elemento = condizioni_elementi.id "
                sql += "where condizioni_righe_costi_periodi.id_elemento=247 and data_pickup is not null "
                sql += "order by condizioni_righe_costi_periodi.id,applicabilita_da"

                sqldatasource1.SelectCommand = sql
                grid1.DataBind()
                Session("sql_tbl") = sql



            Catch ex As Exception
                Response.Write("error page_load:" & ex.Message & "<br/>" & sql & "<br/>")
            End Try

        Else

        End If

    End Sub
    Protected Sub grid1_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Try


            If e.Row.RowType = DataControlRowType.DataRow Then

                If (e.Row.RowState And DataControlRowState.Edit) > 0 Then


                    Dim txtDataPickup As TextBox = CType(e.Row.FindControl("txt_data_pickup"), TextBox)

                    If Not IsDBNull(txtDataPickup.Text) And txtDataPickup.Text <> "" Then
                        txtDataPickup.Text = FormatDateTime(txtDataPickup.Text, vbShortDate)
                    Else
                        txtDataPickup.Text = ""
                    End If



                Else
                    Dim txtDataPickup As Label = CType(e.Row.FindControl("txt_data_pickup"), Label)

                    If Not IsDBNull(txtDataPickup.Text) And txtDataPickup.Text <> "" Then
                        txtDataPickup.Text = FormatDateTime(txtDataPickup.Text, vbShortDate)
                    Else
                        txtDataPickup.Text = ""
                    End If






                End If
            End If


            'If e.Row.RowType = DataControlRowType.Header Thens

            'Else

            '    If e.Row.RowType = DataControlRowType.DataRow Then

            '    ElseIf e.Row.RowType = DataControlRowType.Footer Then

            '    End If

            'End If

        Catch ex As Exception
            Response.Write("error gridView1_RowDataBound :" & ex.Message & "<br/>")
        End Try

    End Sub
    Protected Sub grid1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)

        Dim dbaction As Integer = 0
        dbaction = 0

    End Sub

    Protected Sub btnmodifica_Click(sender As Object, e As EventArgs)

        Dim rowIndex As String = TryCast(sender, LinkButton).CommandArgument
        'Response.Redirect("indici_confronto.aspx?plan=" & rowIndex)

    End Sub







    Protected Sub grid1_RowEditing(sender As Object, e As GridViewEditEventArgs)

        grid1.EditIndex = e.NewEditIndex
        sqldatasource1.SelectCommand = Session("sql_tbl")
        grid1.DataBind()


    End Sub

    Protected Sub grid1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)


        Dim sql As String


        Try
            Dim row = grid1.Rows(e.RowIndex)
            Dim key As DataKey = grid1.DataKeys(e.RowIndex)
            Dim idrec = key.Value.ToString()

            Dim t_data_pickup As String = CType(grid1.Rows(e.RowIndex).FindControl("txt_data_pickup"), TextBox).Text
            Dim t_app_da As String = CType(grid1.Rows(e.RowIndex).FindControl("txt_applicabilita_da"), TextBox).Text
            Dim t_app_a As String = CType(grid1.Rows(e.RowIndex).FindControl("txt_applicabilita_a"), TextBox).Text
            Dim t_costo As String = CType(grid1.Rows(e.RowIndex).FindControl("txt_costo"), TextBox).Text


            'verifica campi
            If t_data_pickup = "" Or Not IsDate(t_data_pickup) Then
                Exit Sub
            End If
            If t_app_da = "" Or Not IsNumeric(t_app_da) Then
                funzioni_comuni.genUserMsgBox(Page, "Inserire solo valore numerico nel campo Applicabilità Da")
                lbl_error.Text = "Inserire solo valore numerico nel campo Applicabilità Da"
                lbl_error.Visible = True
                Exit Sub
            End If
            If t_app_a = "" Or Not IsNumeric(t_app_a) Then
                Exit Sub
            End If
            If t_costo = "" Or Not IsNumeric(t_costo) Then
                Exit Sub
            End If
            '

            lbl_error.Visible = False


            t_costo = t_costo.Replace(",", ".")
            t_data_pickup = CDate(t_data_pickup).Year & "-" & CDate(t_data_pickup).Month & "-" & CDate(t_data_pickup).Day & " 00:00:00"


            sql = "UPDATE condizioni_righe_costi_periodi SET "
            sql = sql & "[costo]='" & t_costo & "' "
            sql = sql & ",[data_pickup]=convert(datetime,'" & t_data_pickup & "',102) "
            sql = sql & ",[applicabilita_da]='" & t_app_da & "' "
            sql = sql & ",[applicabilita_a]='" & t_app_a & "' "

            sql = sql & " WHERE [id]=" & idrec & "; "

            sqldatasource1.UpdateCommand = sql
            sqldatasource1.Update()


        Catch ex As Exception
            Response.Write("error rowUpdating : " & ex.Message & "<br/>" & sql & "<br/>")
        End Try

        sqldatasource1.SelectCommand = Session("sql_tbl")
        grid1.DataBind()

        grid1.EditIndex = -1


    End Sub

    Protected Sub grid1_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)

        sqldatasource1.SelectCommand = Session("sql_tbl")
        grid1.DataBind()

    End Sub





End Class
