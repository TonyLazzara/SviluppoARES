
Partial Class stampaReportContratti
    Inherits System.Web.UI.Page


    Protected Function GetTotaleIncassato(numco As String) As String
        Dim ris As String = "0"

        Try


            Dim sqlstr As String = "SELECT  sum(PAGAMENTI_EXTRA.PER_IMPORTO) as totale FROM PAGAMENTI_EXTRA INNER JOIN TIP_PAG ON PAGAMENTI_EXTRA.ID_TIPPAG = TIP_PAG.ID_TIPPag "
            sqlstr += "WHERE (PAGAMENTI_EXTRA.N_CONTRATTO_RIF = '" & numco & "') and TIPO_PAGA not like 'DE' and segno not like '-' "


            Try
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                Dim Rs1 As Data.SqlClient.SqlDataReader
                Rs1 = Cmd1.ExecuteReader()

                If Rs1.HasRows Then
                    Rs1.Read()
                    ris = Rs1!totale

                End If

                Rs1.Close()
                Rs1 = Nothing
                Cmd1.Dispose()
                Cmd1 = Nothing
                Dbc.Close()
                Dbc = Nothing


            Catch ex As Exception

            End Try

            Return ris



        Catch ex As Exception

        End Try

        Return ris


    End Function



End Class
