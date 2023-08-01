Imports System.Net.Mail


Public Class sendmailcls

    Public Function sendmail(ByVal mittente As String, ByVal nomemittente As String, ByVal destinatario As String, ByVal oggmail As String, ByVal txtmail As String, ByVal flaghtml As Boolean, Optional ByVal AttachmentFile As String = "") As Integer
        '1= email inviata
        '0= errore nell'invio

        '### SOLO TEST NN INVIA ###
        'sendmail = 1
        'Exit Function
        '### SOLO TEST NN INVIA ###


        Dim ip As String = HttpContext.Current.Request.ServerVariables("REMOTE_HOST")

        'Dim client As New SmtpClient("authsmtp.securemail.pro") 'Register
        Dim client As New SmtpClient("smtp.xinformatica.it") 'Aruba     'da utilizzare per limite di invio di Register 31.12.2020

        If mittente = "" Then
            sendmail = 0
            Exit Function
        End If
        If nomemittente = "" Then nomemittente = "Sicilyrentcar"
        If destinatario = "" Then
            sendmail = 0
            Exit Function
            'destinatario = "dimatteo@xinformatica.it"
        End If
        If oggmail = "" Then oggmail = "Oggetto"
        If txtmail = "" Then txtmail = "testo email"

        If IsValidEmail(destinatario) = False Then
            Return -1
            Exit Function
        End If


        Dim [from] As New MailAddress(mittente, nomemittente)
        Dim [to] As New MailAddress(destinatario)
        Dim message As New MailMessage([from], [to])

        Dim usr As String, pwd As String
        'system@
        'Register
        'usr = "system@sicilyrentcars.it"
        'pwd = "SyRcar2020!"

        'Aruba
        usr = "ares_sbc@xinformatica.it"
        pwd = "Sbc!2020"

        'se altro mittente verifica credenziali

        Dim credentials As New System.Net.NetworkCredential(usr, pwd)

        client.Credentials = credentials

        Try
            message.Body = txtmail
            message.IsBodyHtml = flaghtml
            message.Subject = oggmail
            If AttachmentFile <> "" Then

                Dim afile As New Attachment(AttachmentFile)
                message.Attachments.Add(afile)
                'message.Attachments.Add(AttachmentFile)
            End If
            client.Send(message)
            sendmail = 1

            System.Threading.Thread.Sleep(500)

        Catch ex As Exception
            HttpContext.Current.Response.Write("errore (sendmail):" & ex.Message & "<br/>")
            'Libreria.genUserMsgBox(Page, ex.Message)
            sendmail = 0
        End Try


    End Function

    Function IsValidEmail(strIn As String) As Boolean
        ' Return true if strIn is in valid e-mail format.
        Dim pattern As String
        pattern = "^(?("")("".+?""@)|(([0-9a-zA-Z_\.\-]((\.(?!\.))|[-_!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z_\.\-])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z_\.\-][-\w]*[0-9a-zA-Z_\.\-]\.)+[a-zA-Z]{2,6}))$"
        Return Regex.IsMatch(strIn, pattern, RegexOptions.IgnoreCase)

    End Function

    Public Function sendmailMulipleFile(ByVal mittente As String, ByVal nomemittente As String, ByVal destinatario As String, ByVal oggmail As String, ByVal txtmail As String, ByVal flaghtml As Boolean, Optional ByVal AttachmentFile As String = "") As Integer
        '1= email inviata
        '0= errore nell'invio

        '### SOLO TEST NN INVIA ###
        'sendmail = 1
        'Exit Function
        '### SOLO TEST NN INVIA ###


        Dim ip As String = HttpContext.Current.Request.ServerVariables("REMOTE_HOST")

        'Dim client As New SmtpClient("authsmtp.securemail.pro") 'Register
        Dim client As New SmtpClient("smtp.xinformatica.it") 'Aruba     'da utilizzare per limite di invio di Register 31.12.2020

        If mittente = "" Then
            sendmailMulipleFile = 0
            Exit Function
        End If
        If nomemittente = "" Then nomemittente = "Sicilyrentcar"
        If destinatario = "" Then
            sendmailMulipleFile = 0
            Exit Function
            'destinatario = "dimatteo@xinformatica.it"
        End If
        If oggmail = "" Then oggmail = "Oggetto"
        If txtmail = "" Then txtmail = "testo email"

        If IsValidEmail(destinatario) = False Then
            Return -1
            Exit Function
        End If


        Dim [from] As New MailAddress(mittente, nomemittente)
        Dim [to] As New MailAddress(destinatario)
        Dim message As New MailMessage([from], [to])

        Dim usr As String, pwd As String
        'system@
        'Register
        'usr = "system@sicilyrentcars.it"
        'pwd = "SyRcar2020!"

        'Aruba
        usr = "ares_sbc@xinformatica.it"
        pwd = "Sbc!2020"

        'se altro mittente verifica credenziali

        Dim credentials As New System.Net.NetworkCredential(usr, pwd)

        client.Credentials = credentials

        Try
            message.Body = txtmail
            message.IsBodyHtml = flaghtml
            message.Subject = oggmail
            If AttachmentFile <> "" Then


                If AttachmentFile.IndexOf(";") > -1 Then
                    Dim aa() As String = Split(AttachmentFile, ";")

                    For x = 0 To UBound(aa)
                        Dim afile As New Attachment(aa(x))
                        message.Attachments.Add(afile)
                    Next

                Else
                    Dim afile As New Attachment(AttachmentFile)
                    message.Attachments.Add(afile)
                End If


                'message.Attachments.Add(AttachmentFile)
            End If
            client.Send(message)
            sendmailMulipleFile = 1

            System.Threading.Thread.Sleep(500)

        Catch ex As Exception
            HttpContext.Current.Response.Write("errore (sendmailMulipleFile):" & ex.Message & "<br/>")
            'Libreria.genUserMsgBox(Page, ex.Message)
            sendmailMulipleFile = 0
        End Try


    End Function

End Class
