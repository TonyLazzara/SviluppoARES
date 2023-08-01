
Partial Class MasterPageNoMenuX
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If funzioni_comuni.sql_inj(Request.ServerVariables("SERVER_NAME") & Request.ServerVariables("PATH_INFO") & Request.ServerVariables("QUERY_STRING")) Then
            Response.Redirect("default.aspx")
        End If


        If Request.Cookies("SicilyRentCar") Is Nothing Then
            Response.Redirect("login.aspx")
        Else
            Dim objCookie As HttpCookie = Request.Cookies("SicilyRentCar")
            objCookie.Expires = DateAdd(DateInterval.Minute, 120, Now())
            Response.AppendCookie(objCookie)
        End If

        'CONTROLLO CHE IL COOKIE NON SIA STATO MODIFICATO--------------------------------------------------------------
        Dim test As Integer

        Try
            test = CInt(Request.Cookies("SicilyRentCar")("IdUtente"))
        Catch ex As Exception
            Dim objCookie As HttpCookie = Request.Cookies("SicilyRentCar")
            objCookie.Expires = DateTime.Now.AddDays(-1)
            Response.AppendCookie(objCookie)
            Response.Redirect("login.aspx")
        End Try
        '--------------------------------------------------------------------------------------------------------------
        'salva_log()

        'Dim sbScript As StringBuilder = New StringBuilder
        'sbScript.Append("<script>" & vbCrLf)
        ''sbScript.Append("<!--" & vbCrLf)
        'sbScript.Append("function documentOnKeyPress()" & vbCrLf)
        'sbScript.Append("{" & vbCrLf)
        'sbScript.Append(" var charCode = window.event.keyCode;" & vbCrLf)
        'sbScript.Append(" var elementType = window.event.srcElement.type;" & vbCrLf)
        'sbScript.Append(" if ( (charCode == 13) && (elementType == ""text"") )" & vbCrLf)
        'sbScript.Append("   {" & vbCrLf)
        'sbScript.Append("        // Cancel the keystroke completely" & vbCrLf)
        'sbScript.Append("        window.event.returnValue = false;" & vbCrLf)
        'sbScript.Append("        window.event.cancel = true;" & vbCrLf)
        'sbScript.Append("        // Or change it to a tab" & vbCrLf)
        'sbScript.Append("  //window.event.keyCode = 9;" & vbCrLf)
        'sbScript.Append("   }" & vbCrLf)
        'sbScript.Append("}" & vbCrLf)
        'sbScript.Append("document.onkeypress = documentOnKeyPress;" & vbCrLf)
        ''sbScript.Append("// -->" & vbCrLf)
        'sbScript.Append("</script>" & vbCrLf)



        'ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "OnKeyPressScript", sbScript.ToString(), True)
    End Sub

    Protected Sub salva_log()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO utenti_clog (id_utente, nominativo, data, pagina, ip) VALUES ('" & Request.Cookies("SicilyRentCar")("IdUtente") & "',(SELECT ISNULL(cognome,'') + ' ' + ISNULL(nome,'') FROM operatori WHERE id='" & Request.Cookies("SicilyRentCar")("IdUtente") & "'),GetDate(),'" & Replace(Request.CurrentExecutionFilePath, "'", "''") & "','" & Replace(Request.UserHostAddress(), "'", "''") & "')", Dbc)

        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub
End Class

