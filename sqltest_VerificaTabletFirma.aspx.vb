Imports variabili
Imports System.Net
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data
Imports System.Threading


Partial Class sqltest2
    Inherits System.Web.UI.Page

    Dim flag_avvia As Boolean


    Public Ip_Address   'mod. 01.08.2014

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Request.Cookies("SicilyRentCar") Is Nothing Then
            Response.Redirect("LogIn.aspx")
        End If



        Try

            If Not Page.IsPostBack Then

                CaricaDati()



                'gridview1.Visible = False
                'LginAccedi.Text = "Visualizza dati"



            Else

            End If





        Catch ex As Exception
            Response.Write("error:" & ex.Message)
        End Try




    End Sub


    Protected Sub CaricaDati()
        Dim sqlstr As String
        Try
            sqlstr = "SELECT top 20 id, num_contratto, firma_tablet, id_tablet_firma, attivo, id_stazione_uscita, data_uscita, data_uscita_originale, data_ultima_modifica, data_creazione, status "
            sqlstr += "From contratti WHERE (status = 2 or status=1) "
            sqlstr += "ORDER BY contratti.data_uscita DESC, contratti.id DESC"

            SqlDataSource1.SelectCommand = sqlstr
            SqlDataSource1.DataBind()
            gridview1.DataBind()
            gridview1.Visible = True

            Dim tt As String = Date.Now.ToString

            LginAccedi.Text = "Refresh dati " & tt

            Dim ll As String = Date.Now.ToString

            If lbl_last.Text = "" Then
                lbl_last.Text = tt
            Else
                'se passano più di 25 minuti aggiorna ultima lbl
                If DateDiff(DateInterval.Minute, CDate(lbl_last.Text), Date.Now) > 6 Then
                    lbl_last.Text = tt
                End If

            End If

            Dim ra As String = gridview1.Rows(0).Cells(1).Text
            If lbl_last_ra.Text = "" Then
                lbl_last_ra.Text = ra
            Else
                If lbl_last_ra.Text <> ra Then
                    lbl_last_ra.Text = ra
                End If
            End If




        Catch ex As Exception
            Response.Write("error lgn " & ex.Message & "<br/>")
        End Try

        Exit Sub
    End Sub




    Protected Sub LginAccedi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LginAccedi.Click


        CaricaDati()


        Exit Sub


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
