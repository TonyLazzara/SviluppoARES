Imports System.Net
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data
Imports System.Threading


Partial Class sql1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = Date.Now.ToString
    End Sub




    Protected Sub btn_go_Click(sender As Object, e As EventArgs)
        'If flag_avvia = True Then Exit Sub

        Dim targa As String = Request.QueryString("targa")
        If targa = "" Then
            lbl_lastid.Text = "Nessuna targa"
            Exit Sub
        End If

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)

        Dim sql As String

        'sql = "INSERT INTO [funzionalita] ([id],[id_macro_funzionalita],[descrizione]) VALUES (160,1,'Esportazione Dati XML')"

        'sql= "DELETE FROM [permessi_operatori] where [id_funzionalita]=159"
        'sql = "DELETE FROM [funzionalita] where [id]=159"

        'sql = "update [funzionalita] set id_macro_funzionalita=11 where [id]=160"

        Dim Cmd As New Data.SqlClient.SqlCommand()

        Try

            Dbc.Open()

            'sql = "INSERT INTO [macro_funzionalita] ([descrizione]) VALUES ('Libero')"
            'sql = "INSERT INTO [funzionalita] ([id],[id_macro_funzionalita],[descrizione]) VALUES (159,12,'Tipo Allegati PREN/CNT')"
            'sql = "update [veicoli] set da_rifornire=0 where [targa]='fv 045 wv'"
            'sql = "update [veicoli] set disponibile_nolo=1 where [targa]='fv 045 wv'"

            sql = "update [veicoli] set disponibile_nolo=1, da_lavare=0, da_rifornire=0, noleggiata=0 where [targa]='" & targa & "'"

            Cmd.Connection = Dbc
            Cmd.CommandText = sql
            Cmd.ExecuteNonQuery()

            'sql = "update [funzionalita] set id_macro_funzionalita=15 where [id]=141 or [id]=109"
            'Cmd.CommandText = sql
            'Cmd.ExecuteNonQuery()


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
