<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)

        'Codice eseguito in caso di errore non gestito
        'If Request.ServerVariables("REMOTE_ADDR") <> "" Then

        Dim LastException As String = Server.GetLastError().ToString()
        Dim Errore = ""
        Errore = Server.GetLastError().Message & "<br>" & Server.GetLastError().ToString()

        Dim ex as Exception = Server.GetLastError()
        If (ex.GetType Is GetType(HttpException)) Then
            Dim code As HttpException = CType(ex, HTTPEXCEPTION)
            If (code.GetHttpCode() <> 404) Then
                'InviaMail(Errore)
            End If
        Else
            'InviaMail(Errore)
        End If
        Response.Write("Error_Global_=" & Errore & "<br/>" &  "<br/>Message:<br/>" & ex.Message & "<br/>")

        'Session("gl_error") = ex.Message
        'HttpContext.Current.Response.Write("error   : <br/>" & ex.Message & "<br/>" & "<br/>")
        Context.ClearError()
        System.Data.SqlClient.SqlConnection.ClearAllPools()

        'Response.Redirect("http://ares.sicilyrentcar.it/errore.aspx")
        'End If
    End Sub



    Sub InviaMail(ByVal Errore)

        Dim Mail As New Net.Mail.MailMessage()
        Dim From As New Net.Mail.MailAddress("ares_sbc@xinformatica.it")
        Dim Destinatario As New Net.Mail.MailAddress("supporto@xinformatica.it")

        Mail = New Net.Mail.MailMessage(From, Destinatario)
        Mail.Subject = "Errore da ARES Sicilyrentcar"

        Mail.IsBodyHtml = True
        Mail.Body = "<br><br>IP utente: " & Request.ServerVariables("REMOTE_ADDR") & "<br>" & Request.ServerVariables("HTTP_REFERER") & "<br>" & Errore

        Dim client As New Net.Mail.SmtpClient("")
        client.Credentials = New System.Net.NetworkCredential("ares_sbc@xinformatica.it", "Sbc!2020")
        client.Host = "smtp.xinformatica.it"

        Try
            client.Send(Mail)
        Catch ex As Exception

        End Try
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub

</script>