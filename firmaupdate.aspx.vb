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



        If IsNothing(Request.Cookies("SicilyRentCar")) Then
            Response.Redirect("default.aspx")
        End If
        If funzioni_comuni.sql_inj(Request.ServerVariables("SERVER_NAME") & Request.ServerVariables("PATH_INFO") & Request.ServerVariables("QUERY_STRING")) Then
            Response.Redirect("default.aspx")
        End If


        If Request.Cookies("SicilyRentCar")("idUtente") <> "8" And Request.Cookies("SicilyRentCar")("idUtente") <> "5" Then
            Response.Redirect("default.aspx")
        End If



        Ip_Address = Request.ServerVariables("REMOTE_ADDR")

        If Not IsPostBack Then

            lbl_msg.ForeColor = Drawing.Color.Black
            Session("start") = ""


            HyperLink1.Text = "-"
            HyperLink1.NavigateUrl = ""

        Else

            If Session("end") <> "" Then

            End If



        End If


    End Sub
    Protected Sub LginAccedi_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim numco As String = txt_num_contratto.Text
        If numco = "" Then
            lbl_msg.Text = "Inserire numero contratto"
            lbl_msg.ForeColor = Drawing.Color.Red
            Exit Sub
        End If

        Try

            'aggiorna i campi firma rientro del db
            Dim x As Integer = SetFirma(numco)

            'se record aggiornati 
            'copia firma di uscita per il rientro
            If x > 0 Then

                Dim filePath As String = Server.MapPath("\")
                Dim pathfirma As String = filePath & "\firme_contratti\pick_up\"
                Dim firmaOut As String = numco & ".png"
                Dim firmaIn As String = numco & "_RB.png"
                Dim firmaOutTrasp As String = numco & "-trasp.png"
                Dim firmaInTrasp As String = numco & "_RB-trasp.png"



                If File.Exists(pathfirma & firmaOut) And File.Exists(pathfirma & firmaOutTrasp) Then
                    'se presenti i files firma uscita
                    'li copia per firma rientro


                    File.Copy(pathfirma & firmaOut, pathfirma & firmaIn, True)

                    File.Copy(pathfirma & firmaOutTrasp, pathfirma & firmaInTrasp, True)

                    lbl_msg.Text = "Contratto " & numco & " firmato"
                    lbl_msg.ForeColor = Drawing.Color.Black

                    HyperLink1.Text = "v. contratto " & numco
                    HyperLink1.NavigateUrl = "/contratti.aspx?nr=" & numco
                    HyperLink1.Target = "_blank"

                Else
                    lbl_msg.Text = "Firma uscita non presente : " & numco & " "
                    lbl_msg.ForeColor = Drawing.Color.Red

                End If


            End If





            'HttpContext.Current.Response.Write("START test SQL " & Date.Now.ToString & "<br/><hr/><br/>")
            'HttpContext.Current.Response.Write("<table>")
            'HttpContext.Current.Response.Write("<tr>")
            'HttpContext.Current.Response.Write("<td>ID</td><td>id_stazione</td><td>id_orario</td><td>da_mese</td>")
            'HttpContext.Current.Response.Write("<td>a_mese</td><td>da_giorno</td><td>a_giorno</td>")
            'HttpContext.Current.Response.Write("</tr>")
            'While Rs.Read
            '    HttpContext.Current.Response.Write("<tr>")
            '    HttpContext.Current.Response.Write("<td>" & Rs("id") & "</td><td>" & Rs("id_stazione") & "</td><td>" & Rs("id_orario") & "</td><td>" & Rs("da_mese") & "</td>")
            '    HttpContext.Current.Response.Write("<td>" & Rs("a_mese") & "</td><td>" & Rs("da_giorno") & "</td><td>" & Rs("a_giorno") & "</td>")
            '    HttpContext.Current.Response.Write("</tr>")
            'End While
            'HttpContext.Current.Response.Write("</table>")



            ' HttpContext.Current.Response.Write("Terminata: " & " " & "<br/>")


        Catch ex As Exception
            HttpContext.Current.Response.Write("Error: " & ex.Message & "<br/>")
            lbl_msg.ForeColor = Drawing.Color.Red


            HyperLink1.Text = "-"
            HyperLink1.NavigateUrl = ""

        End Try

        Session("start") = "end"
        'Response.End()

    End Sub


    Protected Function SetFirma(nco) As Integer
        Dim ris As Integer = 0
        Try

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)

            Dbc.Open()

            Dim sqlu As String = "UPDATE contratti SET [firma_tablet_rientro]=1, [id_tablet_firma_rientro]=[id_tablet_firma] WHERE num_contratto='" & nco & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlu, Dbc)
            ris = Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception

            lbl_msg.Text = "error: " & ex.Message


        End Try

        Return ris



    End Function



End Class
