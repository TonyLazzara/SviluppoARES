
Partial Class stampaReportCommissioniOperatore
    Inherits System.Web.UI.Page


    Public Function GetCostiAltri(operatore As String, ddt As String, adt As String) As Array

        Dim ris(1) As Double

        Dim sqlstr As String = "SELECT SUM(contratti_costi.imponibile) AS imponibile, SUM(contratti.giorni) AS n_giorni "
        sqlstr += "From contratti INNER JOIN contratti_costi ON contratti.id = contratti_costi.id_documento "
        sqlstr += "WHERE(contratti.status = 6 Or contratti.status = 8) And (contratti.attivo = 1) And (contratti.id_operatore_creazione = '" & operatore & "') "
        sqlstr += "And (contratti.data_uscita BETWEEN CONVERT(DATETIME, '" & ddt & "', 102) AND CONVERT(DATETIME, '" & adt & "', 102)) "
        sqlstr += "AND (contratti_costi.id_elemento = 98)"




        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows Then
                Rs.Read()
                ris(0) = Rs!imponibile
                ris(1) = Rs!n_giorni
            Else
                ris(0) = 0
                ris(1) = 0
            End If


            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Libreria.genUserMsgBox(Page, "Errore GetCostiAltri:" & ex.Message)

        End Try

        Return ris




    End Function





End Class
