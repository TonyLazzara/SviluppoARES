Imports funzioni_comuni
Imports Libreria
Imports PermessiUtente
Partial Class contatori

    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            livelloAccesso.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Contatore_ra)

            If livelloAccesso.Text = "1" Then
                Response.Redirect("default.aspx")
            End If
            If livelloAccesso.Text = "3" Then
                btnSalva.Enabled = True
            End If

            get_last_num_ra()
            


        End If
    End Sub
    Sub get_last_num_ra()

        Try
            Dim data As Date = Date.Now
            Dim anno As String = data.Year
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 contatore FROM contatori WHERE tipo='fatture_nolo' and anno='" & anno & "'", Dbc)
            num_cont_RA.Text = Cmd.ExecuteScalar
            txt_cont_ra.Text = ""
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception

        End Try

    End Sub
    Function get_num_fattura_RA(ByVal anno As String) As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 contatore FROM contatori WHERE tipo='fatture_nolo' and anno='" & anno & "'", Dbc)
            get_num_fattura_RA = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception

        End Try


    End Function
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            Dim data As Date = Date.Now
            Dim anno As String = data.Year
            'If CInt(txt_cont_ra.Text) > CInt(get_num_fattura_RA(anno)) Then
            If True Then
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()


                Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE contatori SET contatore = '" & txt_cont_ra.Text & "' WHERE tipo='fatture_nolo' and anno = '" & anno & "'", Dbc)


                Dim dbaction As Integer = Cmd.ExecuteNonQuery()

                If dbaction > 0 Then
                    get_last_num_ra()
                Else

                End If

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
                'Response.Redirect(Request.RawUrl)
                'Response.Redirect("contatori.aspx")
            Else

                Libreria.genUserMsgBox(Page, "Il numero del nuovo contatore deve essere superiore al vecchio")

                get_last_num_ra()

            End If

        Catch ex As Exception
            'Libreria.genUserMsgBox(Page, "Si è verificato un errore, riprova.")
            Response.Write("error btnSalvaClick : " & ex.Message & "<br/>")
        End Try


    End Sub
End Class
