Imports variabili
Imports System.Net
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data
Imports System.Threading


Partial Class LogIn
    Inherits System.Web.UI.Page


    Dim flag_avvia As Boolean


    Public Ip_Address   'mod. 01.08.2014

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ip_Address = Request.ServerVariables("REMOTE_ADDR")
        Buffer = False
        Server.ScriptTimeout = 360
        'registra_ip(Ip_Address)
        'check_ip()




    End Sub

    Protected Sub LginAccedi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LginAccedi.Click

        'If flag_avvia = True Then Exit Sub

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)

        'Dim sc As String = "server=DESKTOP-ALB9FKR\SQLEXPRESS;User id=Salvo;Password=Return2019$;database=numeridelcalcio"
        'Dim sql As String = "SELECT * FROM tb_arbitri"
        'Dim cn As New SqlConnection(sc)
        'Dim cm As SqlCommand = New SqlCommand(sql, cn)
        'cn.Open()
        'Response.Write(cm.ExecuteScalar().ToString)

        Dim sql As String = "SELECT * FROM operatori order by cognome;"
        Dim cont As Integer = 0
        Dim Rs As Data.SqlClient.SqlDataReader

        Dim strdiv As String = "<div style="
        Dim posdiv As Integer = 0
        Dim dbupdate As Integer = 0
        Dim nmod As Integer = 2000



        Try

            Dbc.Open()

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sql, Dbc)

            Rs = Cmd1.ExecuteReader()
            cont = 0
            If Rs.HasRows Then

                Response.Write("START test SQL " & Date.Now.ToString & "<br/><hr/><br/>")

                While Rs.Read

                    Response.Write(Rs("cognome") & " - " & Rs("password") & "<br/>")

                End While



            End If
            Rs.Close()

        Catch ex As Exception
            Response.Write(ex.Message & "<br/>")

        End Try


        Dbc.Close()
        Rs = Nothing
        Dbc = Nothing


        Exit Sub


    End Sub

  

   


   

   





End Class
