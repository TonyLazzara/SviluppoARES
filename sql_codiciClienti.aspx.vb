Imports System.Net
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data
Imports System.Threading


Partial Class sql1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim i As Integer = getMAXcode()

        Label1.Text = "Ultimo Codice_cliente attuale: " & i.ToString

    End Sub




    Protected Sub btn_go_Click(sender As Object, e As EventArgs)
        'If flag_avvia = True Then Exit Sub

        Dim cc As String = Request.QueryString("cc")
        If cc <> "8" Then
            lbl_lastid.Text = "Attenzione Inserire CC"
            lbl_lastid.ForeColor = Drawing.Color.Red
            Exit Sub
        Else
            lbl_lastid.ForeColor = Drawing.Color.Black
        End If


        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)

        Dim sql As String


        Dim Cmd As New Data.SqlClient.SqlCommand()
        Dim newcode As Integer = 0

        Try

            Dbc.Open()


            Dim maxcode As Integer = getMAXcode()
            If maxcode = 0 Then
                Exit Sub
                lbl_lastid.Text = "errore in getmaxcode:" & "<br/>" & sql & "<br/>"
            End If
            Dim ncodici As Integer = CInt(ddl_newcodici.SelectedValue)
            For x = 1 To ncodici
                newcode = maxcode + x
                sql = "INSERT INTO [codice_cliente] ([codice_cliente]) VALUES ('" & newcode & "')"

                Cmd.Connection = Dbc
                Cmd.CommandText = sql
                Cmd.ExecuteNonQuery()

                Response.Write("crato code --> " & (newcode).ToString & "<br/>")

            Next



            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()

            Dbc = Nothing

            lbl_lastid.Text = "operazione effettuata:" & "<br/>" & sql & "<br/>Ultimo code: " & newcode.ToString



        Catch ex As Exception
            ' HttpContext.Current.Response.Write("UPDATE SQL: " & ex.Message & "<br/>" & sql & "<br/>")
            lbl_lastid.Text = "Insert SQL: " & ex.Message & "<br/>" & sql & "<br/>"
        End Try

        Label1.Text = Date.Now.ToString


        Exit Sub
    End Sub

    Protected Function getMAXcode() As Integer

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT MAX(codice_cliente) as MaxId  FROM codice_cliente", Dbc)

            Dim i As Integer = Cmd.ExecuteScalar

            getMAXcode = i

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            getMAXcode = 0
        End Try




    End Function

End Class
