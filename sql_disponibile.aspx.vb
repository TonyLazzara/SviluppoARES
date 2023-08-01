Imports System.Net
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data
Imports System.Threading


Partial Class sql1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim i As Integer = funzioni_comuni.GetKmRestanti("FZ 041 TN")


        Label1.Text = i.ToString

    End Sub




    Protected Sub btn_go_Click(sender As Object, e As EventArgs)
        'If flag_avvia = True Then Exit Sub

        Dim targa As String = Request.QueryString("targa")
        If targa = "" And Len(targa) <> 7 Then
            lbl_lastid.Text = "Nessuna targa"
            Exit Sub
        End If
        If Request.QueryString("ss") <> "admin" Then
            lbl_lastid.Text = "Nessuna Targa inserita"
            Exit Sub
        End If



        targa = Left(targa, 2) & " " & Mid(targa, 3, 3) & " " & Right(targa, 2)

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)

        Dim sql As String

        sql = "update [veicoli] set noleggiata=0, disponibile_nolo=1 where [targa]='" & targa & "'"

        Dim Cmd As New Data.SqlClient.SqlCommand()

        Try

            Dbc.Open()


            Cmd.Connection = Dbc
            Cmd.CommandText = sql
            Cmd.ExecuteNonQuery()


            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()

            Dbc = Nothing

            lbl_lastid.Text = "operazione effettuata:" & "<br/>" & sql & "<br/>"

        Catch ex As Exception
            ' HttpContext.Current.Response.Write("UPDATE SQL: " & ex.Message & "<br/>" & sql & "<br/>")
            lbl_lastid.Text = "UPDATE SQL: " & ex.Message & "<br/>" & sql & "<br/>"
        End Try

        Label1.Text = Date.Now.ToString


        Exit Sub
    End Sub
End Class
