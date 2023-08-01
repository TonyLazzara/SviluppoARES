Imports System.Net.Mail

Partial Class invioemailtest
    Inherits System.Web.UI.Page

    Dim smail As New sendmailcls
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Request.Cookies("SicilyRentCar") Is Nothing Then

            Label2.Text = "-"

        Else

            Label2.Text = "Cookies attivi: emailOperatore:" & Request.Cookies("SicilyRentCar")("email_operatore") & " - email Stazione : " & Request.Cookies("SicilyRentCar")("email_stazione")

        End If



    End Sub
    Protected Sub btnInvioEmail_Click(sender As Object, e As EventArgs)

        'inviaMailPrenotazione(TextBox1.Text)

        Try
            Dim xinvio As Integer = smail.sendmail(ddl_stazioni.SelectedValue.ToString, ddl_stazioni.SelectedItem.Text, TextBox1.Text, "Test Oggetto", "Testo mail", True, "")

            If xinvio = 1 Then
                Label1.Text = "email inviata a " & ddl_stazioni.SelectedValue.ToString & " - " & Date.Now.ToString
                Label1.ForeColor = Drawing.Color.Black
            ElseIf xinvio = -1 Then
                Label1.Text = "formato email destinatario non valido - " & Date.Now.ToString
                Label1.ForeColor = Drawing.Color.Red
            Else
                Label1.Text = "email NON inviata " & Date.Now.ToString
                Label1.ForeColor = Drawing.Color.Red
            End If

        Catch ex As Exception
            Label1.Text = "Errore email NON inviata :" & ex.Message
            Label1.ForeColor = Drawing.Color.Red
        End Try





    End Sub


    Protected Sub inviaMailPrenotazione(ByVal mail_conducente As String)


        Dim mail As New MailMessage()

        mail.To.Add(mail_conducente)

        'Imposta l'oggetto della Mail
        mail.Subject = "Richiesta Prenotazione -  "

        'Imposta la priorità  della Mail
        mail.Priority = MailPriority.High

        mail.IsBodyHtml = True

        Dim corpoMessaggio As String
        corpoMessaggio = "Gentile Cliente / Dear Client,"


        'Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        'Dbc.Open()
        'Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(cod_gruppo,'') + ' ' + ISNULL(descrizione,'') FROM gruppi WITH(NOLOCK) WHERE id_gruppo='" & id_gruppo_auto_scelto.Text & "'", Dbc)

        'corpoMessaggio = corpoMessaggio & "<b>Veicolo / Vehicle: </b> " & "Gruppo " & Cmd.ExecuteScalar & " o similare<br><br>" &
        '"<b>RITIRO / PICK UP </b><br>" &

        'Cmd = New Data.SqlClient.SqlCommand("SELECT indirizzo, telefono, cellulare, email, testo_mail FROM stazioni WITH(NOLOCK) WHERE id='" & dropStazionePickUp.SelectedValue & "'", Dbc)

        'Dim Rs As Data.SqlClient.SqlDataReader
        'Rs = Cmd.ExecuteReader()
        'Rs.Read()

        Dim email_stazione As String = "system@sicilyrentcars.it" 'email 'Rs("email") & ""

        Dim testo_mail As String = "Test email " 'Rs("testo_mail") & ""


        Try
            mail.From = New MailAddress(email_stazione)
            mail.To.Add(email_stazione)
        Catch ex As Exception
            mail.From = New MailAddress("system@sicilyrentcars.it")
        End Try



        corpoMessaggio = testo_mail ' corpoMessaggio & Rs("indirizzo") & ""

        'If (Rs("telefono") & "") <> "" Then
        '    corpoMessaggio = corpoMessaggio & " Telefono " & Rs("telefono")
        'End If

        'If (Rs("cellulare") & "") <> "" Then
        '    corpoMessaggio = corpoMessaggio & " (Cellulare " & Rs("cellulare") & ")"
        'End If



        'corpoMessaggio = corpoMessaggio & "<br><br><b>RICONSEGNA / DROP OFF</b><br>" &
        'dropStazioneDropOff.SelectedItem.Text & "<br>" &
        'txtAData.Text & " ore " & txtOraRientro.Text & "<br>"

        'Dbc.Close()
        'Dbc.Open()

        'Cmd = New Data.SqlClient.SqlCommand("SELECT indirizzo, telefono, cellulare, email FROM stazioni WITH(NOLOCK) WHERE id='" & dropStazioneDropOff.SelectedValue & "'", Dbc)
        'Rs = Cmd.ExecuteReader()
        'Rs.Read()

        corpoMessaggio = corpoMessaggio '& Rs("indirizzo") & ""

        'If (Rs("telefono") & "") <> "" Then
        '    corpoMessaggio = corpoMessaggio & " Telefono " & Rs("telefono")
        'End If



        'If (Rs("cellulare") & "") <> "" Then
        '    corpoMessaggio = corpoMessaggio & " (Cellulare " & Rs("cellulare") & ")"
        'End If

        corpoMessaggio = corpoMessaggio & "<br><br> <b>Giorni / Days:</b> " '& txtNumeroGiorni.Text & "<br><br>" &



        corpoMessaggio = corpoMessaggio & "<br><br><b>Totale noleggio / Total:</b> Euro "


        mail.Body = Replace(corpoMessaggio, "!", "")

        'Dim attachment As New System.Net.Mail.Attachment(Server.MapPath("\prenotazioni\" & "prenotazione" & "_" & lblNumPreventivo.Text & ".pdf"))
        'mail.Attachments.Add(attachment)


        'Imposta il server smtp di posta da utilizzare        
        Dim client As New Net.Mail.SmtpClient("authsmtp.securemail.pro", 465)
        client.Credentials = New System.Net.NetworkCredential("system@sicilyrentcars.it", "SyRcar2020!")
        client.EnableSsl = True

        client.Host = "authsmtp.securemail.pro"

        'Invia l'e-mail
        Try
            client.Send(mail)
            'Libreria.genUserMsgBox(Me, "E-Mail inviata correttamente.")
            Label1.Text = "email inviata :" & Date.Now.ToString
        Catch ex As Exception

            ' Libreria.genUserMsgBox(Me, "Si è verificato un errore nell'invio della mail.")

            Label1.Text = "Errore email NON inviata :" & ex.Message

        End Try

        'attachment.Dispose()

        Try
            ' File.Delete(Server.MapPath("\prenotazioni\" & "prenotazione" & "_" & lblNumPreventivo.Text & ".pdf"))
        Catch ex As Exception

        End Try

        'Rs.Close()
        'Rs = Nothing
        'Cmd.Dispose()
        'Cmd = Nothing
        'Dbc.Close()
        'Dbc.Dispose()
        'Dbc = Nothing
    End Sub




End Class
