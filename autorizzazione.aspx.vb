'Imports funzioni_comuni
Imports System.Net.Mail
Imports System.Net
Partial Class autorizzazione
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load        
        If Not IsPostBack Then
            Response.Write("NO Post Back")
            Session("Pusante") = "OFF"
            Response.Write("IN")
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                'Session("url_print") = "/stampe/RAP/stampa_report.aspx?orientamento=orizzontale&query=" & Server.UrlEncode("") & "&utente=5&header_html=/stampe/header_stampa_elenco.aspx"
                Session("url_print") = "/stampe/RAP/stampa_report.aspx?orientamento=orizzontale"
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                End If
            End If



            'If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            '    Dim url_print As String = "/cassa/StampaSituazioneCassa.aspx?orientamento=verticale&"
            '    Trace.Write(url_print)
            '    Dim mio_random As String = Format((New Random).Next(), "0000000000")
            '    Trace.Write(url_print)
            '    Session("url_print") = url_print
            '    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx?a=" & mio_random & "','')", True)
            'End If
        Else
            Response.Write("Post Back")           
        End If
    End Sub

    Protected Sub btnPulsante_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPulsante.Click
        Dim Messaggio As String = ""

        Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("RAPConnectionString").ConnectionString)
        Dbc2.Open()
        Dim Cmd2 As New Data.SqlClient.SqlCommand("select * from ipMoxa WITH(NOLOCK) ", Dbc2)
        Response.Write(Cmd2.CommandText & "<br>")
        'Response.End()

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd2.ExecuteReader()
        Rs.Read()


        Dim IP As String = ""

        If Rs.HasRows() Then
            IP = Rs(1)            
            Select Case Session("Pusante")
                Case Is = "OFF"                    
                    Try
                        Dim req As WebRequest = WebRequest.Create("http://" & IP & "/setParam.cgi?/setParam.cgi?DOStatus_00=1&DOStatus_01=0&DOStatus_02=0&DOStatus_03=0&DOStatus_04=0&DOStatus_05=0")                                            
                        Dim resp As WebResponse = req.GetResponse()
                        Response.Write(req.RequestUri)
                        Response.End()
                        resp.Close()

                        btnPulsante.BackColor = Drawing.Color.Green
                        btnPulsante.Text = "ON"
                        Session("Pusante") = "ON"
                    Catch ex As Exception
                        Messaggio = "Indirizzo IP (" & IP & ") non corretto. Contattare l'amministratore del sistema."
                        If Messaggio <> "" Then
                            'genUserMsgBox(Page, Messaggio)
                        End If
                    End Try
                Case Is = "ON"                    
                    Try
                        Dim req As WebRequest = WebRequest.Create("http://" & IP & "/setParam.cgi?DOStatus_00=0")
                        Dim resp As WebResponse = req.GetResponse()
                        resp.Close()

                        btnPulsante.BackColor = Drawing.Color.Red
                        btnPulsante.Text = "OFF"
                        Session("Pusante") = "OFF"
                    Catch ex As Exception
                        Messaggio = "Indirizzo IP (" & IP & ") non corretto. Contattare l'amministratore del sistema."
                        If Messaggio <> "" Then
                            'genUserMsgBox(Page, Messaggio)
                        End If
                    End Try
            End Select
        Else
            Messaggio = "Indirizzo IP non settato. Contattare l'amministratore del sistema."
            If Messaggio <> "" Then
                'genUserMsgBox(Page, Messaggio)
            End If
        End If
        Response.Write("IP: " & IP & "<br>")

        Rs.Close()
        Rs = Nothing

        Cmd2.Dispose()
        Cmd2 = Nothing
        Dbc2.Close()
        Dbc2.Dispose()
        Dbc2 = Nothing
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Write("IN")
        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            'Session("url_print") = "/stampe/RAP/stampa_report.aspx?orientamento=orizzontale&query=" & Server.UrlEncode("") & "&utente=5&header_html=/stampe/header_stampa_elenco.aspx"
            Session("url_print") = "/stampe/RAP/stampa_report.aspx?orientamento=orizzontale"
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
            End If
        End If
    End Sub

End Class
