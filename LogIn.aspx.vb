Imports funzioni_comuni

Partial Class LogIn2
    Inherits System.Web.UI.Page

    Public Ip_Address As String
    Public id_user As String

    Protected Sub LginAccedi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LginAccedi.Click        
        Try

            'aggiunto 13.01.2021
            If InStr(1, TxtCodiceCliente.Text, "<", 1) > 0 Then Response.Redirect("login.aspx")
            If InStr(1, TxtCodiceCliente.Text, ">", 1) > 0 Then Response.Redirect("login.aspx")

            'Dim esisteCookie = Request.ServerVariables("HTTP_COOKIE")
            'If esisteCookie = "" Then
            'Response.Write("<script>javascript:alert('Il tuo browser non accetta i cookie.\nAbilitarli e riprovare il login. Grazie');</script>")
            'Else
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            'Dim a As String
            'a = RequestCheck(TxtCodiceCliente.Text)
            'Response.Write(a & "<br><br>")
            'Response.End()


            Dim sqla As String = "Select operatori.id, operatori.nome, operatori.cognome, operatori.username, operatori.password, operatori.id_stazione, operatori.mail, operatori.data_accesso, operatori.ip_access, stazioni.codice "
            sqla += "From operatori INNER Join stazioni On operatori.id_stazione = stazioni.id "
            sqla += "WHERE username='" & RequestCheck(TxtCodiceCliente.Text) & "' and password = '" & RequestCheck(TxtPassword.Text) & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)


            ' Response.Write(Cmd.CommandText & "<br><br>")
            'Response.End()

            Dim idstazione As String = ""

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            If Rs.HasRows Then
                Rs.Read()
                Dim cookie As New HttpCookie("SicilyRentCar")
                cookie("IdUtente") = Rs("id")
                cookie("nome") = Rs("cognome") & " " & Rs("nome")
                cookie("stazione") = Rs("id_stazione")
                cookie("codice_stazione") = Rs("codice")
                cookie("emailsupporto") = "system@sicilyrentcars.it"
                cookie("email_operatore") = Rs("mail")                '
                cookie("email_stazione") = GetMailStazione(Rs("id_stazione")) 'email stazione 02.12.2020
                cookie("usr") = Rs("username") 'ck validate password

                cookie.Expires = DateAdd(DateInterval.Minute, 540, Now())
                'response.write("Scadenza :" & cookie.Expires)
                'response.end()
                Response.Cookies.Add(cookie)

                id_user = Rs("id")
                idstazione = Rs("id_stazione")  '09.12.2021 1918

                If ValidateFineRapporto(Rs("cognome"), Rs("nome")) Then
                    Session("UtenteDisattivato") = Rs("cognome") & " " & Rs("nome")
                    Response.Redirect("utenteFineRapporto.aspx")
                    Exit Sub
                End If

                Rs.Close()
                Dbc.Close()
                Rs = Nothing
                Dbc = Nothing



                ''aggiunto 13.01.2021 x verifica password
                If ValidatePassword(RequestCheck(TxtPassword.Text)) = False Then
                    Response.Redirect("cambiapwd.aspx?us=" & TxtCodiceCliente.Text)
                    Exit Sub
                Else

                    'registra accesso
                    registra_ip(Ip_Address, id_user, "login")

                    '# verifica se disponibilità di codici cliente 09.12.2021 1912
                    'eventualmente ne aggiunge (100) se accesso da stazione PAAPT o CTAPT
                    'solo prima delle 9 AM
                    If (idstazione = "2" Or idstazione = "4") And Hour(Date.Now) < 9 Then
                        ck_codici_cliente()
                    End If
                    '## verifica se disponibilità di codici cliente 09.12.2021

                    Response.Redirect("default.aspx")
                    Exit Sub

                End If

            Else

                FailureText.Text = "Codice Cliente e/o Passord Errato"

                Dim emsg As String = "<br/>User digitata: " & TxtCodiceCliente.Text & " <br/>Password digitata: " & TxtPassword.Text

                Try

                    If Date.Now.Hour > 1 And Date.Now.Hour < 6 Then 'aggiunto 06.06.2022
                        Dim smail As New sendmailcls
                        smail.sendmail("system@sicilyrentcars.it", "ARES Software", "dimatteo@xinformatica.it", "Errore Login da parte di: " & TxtCodiceCliente.Text, "errore login txt : " & emsg, True)
                        ' smail.sendmail("system@sicilyrentcars.it", "ARES Software", "f.scalia@sicilyrentcar.it", "Errore Login da parte di: " & TxtCodiceCliente.Text, "errore login txt: " & emsg, True)
                    End If


                Catch ex As Exception

                End Try

                Rs.Close()
                Dbc.Close()
                Rs = Nothing
                Dbc = Nothing

            End If


        Catch ex As Exception
            Response.Write("error LginAccedi " & Date.Now.ToString & "---" & ex.Message & "<br/><br/>")

        End Try


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
    Public Shared Function RequestCheck(ByVal var As String) As String
        Dim strAus As String

        strAus = Replace(var, "'", "''")
        strAus = Replace(strAus, "drop", "")
        strAus = Replace(strAus, "insert", "")
        strAus = Replace(strAus, "update", "")
        strAus = Replace(strAus, "xp_", "")
        RequestCheck = strAus
        'RequestCheck = var
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("nascondi_menu") = ""
        Ip_Address = Request.ServerVariables("REMOTE_ADDR")

        'TxtCodiceCliente.Text = ""
        'TxtPassword.Text = ""


    End Sub


    Protected Sub ck_codici_cliente()
        '#verifica se codici cliente presenti se meno di 50 li ricrea
        '09.12.2021 1910

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT count(codice_cliente) as MaxId  FROM codice_cliente", Dbc)
            Dim nrec As Integer = 0
            nrec = Cmd.ExecuteScalar

            If nrec < 50 Then
                'ne ricrea 50
                Dim maxid As Integer = getMAXcode()
                For x = 1 To 100
                    maxid += x
                    Dim sql2 = "INSERT INTO [codice_cliente] ([codice_cliente]) VALUES ('" & maxid & "')"
                    Cmd.CommandText = sql2
                    Cmd.ExecuteNonQuery()
                Next

            End If


            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing



        Catch ex As Exception

        End Try









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

    Protected Sub registra_ip(indirizzoip, idusr, tipoA)  'm

        Dim dd = Date.Now.Day
        Dim mm = Date.Now.Month
        Dim yy = Date.Now.Year
        Dim hh = Date.Now.Hour
        Dim mmin = Date.Now.Minute
        Dim ss = Date.Now.Second
        Dim tt = yy & "-" & mm & "-" & dd & " " & hh & ":" & mmin & ":" & ss & ".000"

        Dim sql As String = "INSERT INTO ip_access (ipaddress,dataora,iduser,datanowora, tipo_accesso) VALUES ('" & indirizzoip & "','" & Date.Now.ToString & "','" & idusr & "',CONVERT(DATETIME, '" & tt & "', 102),'" & tipoA & "')"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing


            sql = "update operatori set data_accesso=convert(datetime, getdate(),102), ip_access='" & indirizzoip & "' WHERE [ID]='" & idusr & "';"
            Dim Cmd1 As New Data.SqlClient.SqlCommand(sql, Dbc)
            Cmd1.ExecuteNonQuery()

            Cmd1.Dispose()
            Cmd1 = Nothing



            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception

            Response.Write("error_registra_ip:" & ex.Message & "<br/>" & sql & "<br/>")

        End Try



    End Sub


    'Tony 30/10/2022
    Protected Function ValidateFineRapporto(ByVal Cognome As String, ByVal Nome As String) As Boolean
        Try
            'Operatori su ARES
            Dim DbcFineRapportoSuARES As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyTimbratureConnectionString").ConnectionString)
            DbcFineRapportoSuARES.Open()

            Dim CmdFineRapportoSuARES As New Data.SqlClient.SqlCommand("SELECT data_fine_rapporto FROM anagrafica_dipendenti WITH(NOLOCK) WHERE anagrafica_dipendenti.cognome='" & Cognome & "' and anagrafica_dipendenti.nome='" & Nome & "'", DbcFineRapportoSuARES)
            Response.Write(CmdFineRapportoSuARES.CommandText & "<br><br>")            

            Dim RsFineRapportoSuARES As Data.SqlClient.SqlDataReader
            RsFineRapportoSuARES = CmdFineRapportoSuARES.ExecuteReader()
            If RsFineRapportoSuARES.HasRows Then
                Do While RsFineRapportoSuARES.Read
                    If RsFineRapportoSuARES("data_fine_rapporto") & "" <> "" Then
                        If CDate(RsFineRapportoSuARES("data_fine_rapporto")) <= Today Then
                            ValidateFineRapporto = 1

                            Dim Sql As String
                            Dim Sql2 As String
                            Dim SqlQuery As String

                            Dim DbcARES As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                            DbcARES.Open()

                            Dim CmdARES As New Data.SqlClient.SqlCommand("", DbcARES)
                            Dim SqlARES As String
                            Dim SqlQueryARES As String

                            Try
                                Sql = "update operatori set attivo ='0', password='PassSRC1!' WHERE operatori.cognome ='" & Cognome & "' and operatori.nome ='" & Nome & "'"

                                'Response.Write(Sql & "<br>")
                                'Response.End()

                                CmdARES = New Data.SqlClient.SqlCommand(Sql, DbcARES)
                                CmdARES.ExecuteNonQuery()

                                'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                                'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                                'Session("residenza_virtuale") = Cmd.ExecuteScalar

                                SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SRC_timbrature")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                                CmdARES.CommandText = SqlQuery
                                'Response.Write(CmdARES.CommandText & "<br/>")
                                'Response.End()
                                CmdARES.ExecuteNonQuery()
                            Catch ex As Exception
                                Response.Write(ex)
                                Response.Write("<br><br>")
                                Libreria.genUserMsgBox(Me, "Salvataggio Ins Dipendenti ARES Errore contattare amministratore del sistema.")
                                Response.Write(CmdARES.CommandText)
                            End Try

                            CmdARES.Dispose()
                            CmdARES = Nothing
                            DbcARES.Close()
                            DbcARES.Dispose()
                            DbcARES = Nothing
                        Else
                            ValidateFineRapporto = 0
                        End If
                    Else
                        ValidateFineRapporto = 0
                    End If
                Loop
            Else
                ValidateFineRapporto = 0
            End If

            RsFineRapportoSuARES.Close()
            DbcFineRapportoSuARES.Close()
            RsFineRapportoSuARES = Nothing
            DbcFineRapportoSuARES = Nothing

        Catch ex As Exception
            Response.Write("error Validazione " & Date.Now.ToString & "---" & ex.Message & "<br/><br/>")
        End Try
    End Function
    'FINE Tony



End Class
