Imports System.IO
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb

Partial Class cambiapwd
    Inherits System.Web.UI.Page
    Public txt As String = "La password non rispetta le condizioni di sicurezza e deve essere modificata.<br/>Deve essere lunga almeno 8 caratteri e contenere almeno:<br/>- 1 carattere maiuscolo<br/>- 1 carattere minuscolo<br/>- 1 numero<br/>- 1 carattere speciale(!$)"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Cookies("SicilyRentCar") Is Nothing Then
            Response.Redirect("LogIn.aspx")
        End If

        lbl_nota.Text = txt
        'txt_us.Text = Request.QueryString("us")

        If Page.IsPostBack Then


        Else


            Dim qsus As String = Request.QueryString("us")
            qsus = Request.Cookies("SicilyRentCar")("usr")

            HyperLink1.Visible = False
            txt_us.Text = qsus
            oldpwd.Text = ""
            newpwd.Text = ""
            newpwd1.Text = ""
        End If





    End Sub



    Function ValidatePassword(ByVal pwd As String, Optional ByVal minLength As Integer = 8, Optional ByVal numUpper As Integer = 1, Optional ByVal numLower As Integer = 1, Optional ByVal numNumbers As Integer = 1, Optional ByVal numSpecial As Integer = 1) As Boolean

        ' Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters.
        Dim upper As New System.Text.RegularExpressions.Regex("[A-Z]")
        Dim lower As New System.Text.RegularExpressions.Regex("[a-z]")
        Dim number As New System.Text.RegularExpressions.Regex("[0-9]")
        ' Special is "none of the above".
        Dim special As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]")

        ' Check the length.
        If Len(pwd) < minLength Then Return False

        ' Check for minimum number of occurrences.
        If upper.Matches(pwd).Count < numUpper Then Return False
        If lower.Matches(pwd).Count < numLower Then Return False
        If number.Matches(pwd).Count < numNumbers Then Return False
        If special.Matches(pwd).Count < numSpecial Then Return False

        ' Passed all checks.
        Return True

    End Function


    Protected Sub Button1_Click(sender As Object, e As EventArgs)

        Dim objCookie As HttpCookie = Request.Cookies("SicilyRentCar")

        If ValidatePassword(newpwd.Text) Then

            'modifica valore sul db
            Dim regpwd As String = registra_newpwd(txt_us.Text, newpwd.Text, oldpwd.Text)
            'Response.Write("regpwd:" & regpwd & "<br/>")
            If regpwd = "OK" Then
                PlaceHolder1.Visible = False
                PlaceHolder2.Visible = False
                Label1.Text = "OK Password modificata<br/><br/>"
                Label1.ForeColor = Drawing.Color.Green
                HyperLink1.NavigateUrl = "login.aspx"
                HyperLink1.Visible = True

                'reset cookies
                objCookie.Expires = DateTime.Now.AddDays(-1)
                Response.AppendCookie(objCookie)
                Response.Redirect("login.aspx")

                Exit Sub

            Else
                Label1.Text = "Errore : Verificare password vecchia - " & "<br/>" & regpwd
                Label1.ForeColor = Drawing.Color.Red
                HyperLink1.Visible = False
                HyperLink1.NavigateUrl = "#"
            End If

        Else
            Label1.Text = "KO Password NON complessa : " & "<br/>" & txt
            Label1.ForeColor = Drawing.Color.Red
            HyperLink1.Visible = False
            HyperLink1.NavigateUrl = "#"
        End If


    End Sub

    Protected Function registra_newpwd(us As String, newp As String, oldp As String) As String

        Dim dbaction As Integer = 0

        Dim dd = Date.Now.Day
        Dim mm = Date.Now.Month
        Dim yy = Date.Now.Year
        Dim hh = Date.Now.Hour
        Dim mmin = Date.Now.Minute
        Dim ss = Date.Now.Second
        Dim tt = yy & "-" & mm & "-" & dd & " " & hh & ":" & mmin & ":" & ss & ".000"
        Dim sql As String '= "INSERT INTO ip_access (ipaddress,dataora,iduser,datanowora) VALUES ('" & indirizzoip & "','" & Date.Now.ToString & "','" & idu & "',CONVERT(DATETIME, '" & tt & "', 102))"

        'sql = "UPDATE operatori set password='" & newp & "',data_accesso=CONVERT(DATETIME, '" & tt & "', 102) WHERE username ='" & us & "' AND password ='" & oldp & "';"
        sql = "UPDATE operatori set password='" & newp & "' WHERE username ='" & us & "' AND password ='" & oldp & "';"
        'Response.Write(sql & "<br/>")

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

        Try

            Dbc.Open()
            dbaction = Cmd.ExecuteNonQuery()
            If dbaction > 0 Then
                registra_newpwd = "OK"
            Else
                registra_newpwd = "KO"
            End If

        Catch ex As Exception
            Dim errtxt As String = ex.Message
            Response.Write("error_registra_newpwd:" & ex.Message & "<br/>" & sql & "<br/>")
            registra_newpwd = "Error:registranewpwd: " & errtxt

        End Try

        Cmd.Dispose()
        Cmd = Nothing

        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing




    End Function
End Class
