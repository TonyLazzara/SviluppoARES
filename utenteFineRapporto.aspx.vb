Imports funzioni_comuni
Imports System.IO
Imports System.Net.Mail
Imports System.Net
Imports Microsoft.VisualBasic 'have to have this namespace to use msgbox
Partial Class utenteFineRapporto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim myMail As New MailMessage()

            Dim mySmtp As New SmtpClient("smtps.aruba.it")
            mySmtp.Port = 587
            mySmtp.Credentials = New System.Net.NetworkCredential("supporto@trinakriaservizi.it", "MailSupp@1")
            mySmtp.EnableSsl = True

            'Dim allegato As String = "C:\alazzara\comune" & AttualeFoglioPolarita & ".xlsx"


            Dim StringaEmailDestinatari As String = "it-support@sicilyrentcar.it"
            'im StringaEmailBcc As String = "risorseumane@sicilyrentcar.it,it-support@sicilyrentcar.it"
            'Dim StringaEmailBcc As String = "it-support@sicilyrentcar.it"

            myMail = New MailMessage()
            myMail.From = New MailAddress("noreply@sicilyrentcar.it")
            myMail.To.Add(StringaEmailDestinatari)
            'myMail.Bcc.Add(StringaEmailBcc)

            myMail.Subject = "Tenativo Accesso ARES Utente Disattivato"
            'myMail.Attachments.Add(New Attachment(allegato))
            myMail.IsBodyHtml = True
            myMail.Body = "Arrivato Accesso ARES Utente Disattivato: " & Session("UtenteDisattivato") & " <br>"

            'FIRMA            
            myMail.Body = myMail.Body & "<p><span style='font-size:8.0pt'>Ai sensi delle vigenti disposizioni in materia si precisa che la presente e-mail, con i suoi eventuali allegati, può contenere informazioni private e/o confidenziali ed è destinata esclusivamente ai destinatari in indirizzo. Se avete ricevuto questa e-mail per errore siete espressamente diffidati dal riprodurla in tutto od in parte o, comunque,dall'utilizzare le informazioni contenute nella stessa e nei suoi eventuali allegati. Siete, altresì, pregati di voler contattare il mittente e di distruggere ogni copia di questa e-mail.</span><br> <span style='font-size:8.0pt'>&nbsp;</span><br><span style='font-size:8.0pt'>We inform you that this e-mail, including any attachments, may contain private and/or confidential information. If you are not the addressee or if you have received this e-mail in error, you must not use it or take any action based on this e-mail or any information herein. Please contact the sender immediately and delete any copies of this e-mail.</span></p>"


            mySmtp.Send(myMail)
            Console.WriteLine("Email inviata")
        Catch ex As Exception
            Console.WriteLine(e.ToString)
        End Try
    End Sub
End Class
