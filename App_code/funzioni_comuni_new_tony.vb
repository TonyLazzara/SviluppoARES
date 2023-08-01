Imports System.Data
Imports System.IO

'Funzioni Tony

Public Class funzioni_comuni_new_tony

    Public Shared Function ContaNumeroDanni(ByVal idEvento As String) As String        
        Dim ris As String = "0"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String = "select id_veicolo from veicoli_evento_apertura_danno where id ='" & idEvento & "'"
            'HttpContext.Current.Response.write(sqlStr & "<br>")

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows Then
                Rs.Read()
                Try
                    Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc2.Open()

                    Dim sqlStr2 As String = "SELECT count(*) FROM veicoli_danni WHERE (id_veicolo = '" & rs("id_veicolo") & "') AND (stato = 1)"
                    'HttpContext.Current.response.write(sqlStr2 & "<br>")

                    Dim Cmd2 As New Data.SqlClient.SqlCommand(sqlStr2, Dbc2)

                    Dim Rs2 As Data.SqlClient.SqlDataReader
                    Rs2 = Cmd2.ExecuteReader()

                    If Rs2.HasRows Then
                        Rs2.Read()

                        If Not IsDBNull(Rs2(0)) Then
                            ris = Rs2(0)
                        End If

                    End If

                    Rs2.Close()
                    Rs2 = Nothing
                    Cmd2.Dispose()
                    Cmd2 = Nothing
                    Dbc2.Close()
                    Dbc2.Dispose()
                    Dbc2 = Nothing

                Catch ex As Exception

                End Try
            End If
            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception

        End Try

        Return ris

    End Function
End Class
