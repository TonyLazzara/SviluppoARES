
Partial Class stampaReportContratti
    Inherits System.Web.UI.Page



    Public Function GetCostiAltri(id_doc As String, macrovoce As Integer, nFattura As String) As Double

        Dim ris As Double = 0

        Dim sqlstr As String = "" '"SELECT ISNULL(sum(contratti_costi.imponibile), 0 ) As Assicurazioni "
        'sqlstr += "From Fatture_nolo INNER Join contratti On Fatture_nolo.num_contratto_rif = contratti.num_contratto INNER Join "
        'sqlstr += "contratti_costi On contratti.id = contratti_costi.id_documento INNER Join condizioni_elementi On contratti_costi.id_elemento = condizioni_elementi.id INNER Join "
        'sqlstr += "condizioni_macro_voci On condizioni_elementi.id_macrovoce = condizioni_macro_voci.id_macrovoce "
        ''sqlstr += "Where (Fatture_nolo.num_contratto_rif = " & nco & ") and  (Fatture_nolo.num_fattura = " & nFattura & ")  And (contratti.status = 6) And (contratti_costi.selezionato = 1) And (condizioni_elementi.id_macrovoce = " & macrovoce & ")"
        'sqlstr += "Where (Fatture_nolo.num_contratto_rif = " & nco & ") And (contratti.status = 6) And (contratti_costi.selezionato = 1) And (condizioni_elementi.id_macrovoce = " & macrovoce & ")"

        'sqlstr = "Select sum(contratti_costi.imponibile) As AltriCosti "
        'sqlstr += "From contratti INNER Join contratti_costi On contratti.id = contratti_costi.id_documento LEFT OUTER Join "
        'sqlstr += "condizioni_elementi On contratti_costi.id_elemento = condizioni_elementi.id "
        'sqlstr += "Where (contratti_costi.id_documento = " & id_doc & ") And (condizioni_elementi.id_macrovoce =  '" & macrovoce & "') And (contratti_costi.selezionato = 1) "
        'sqlstr += "And (contratti_costi.omaggiato = 0)"


        'sqlstr = "Select sum(fatture_nolo_righe.totale)  As Imponibile From fatture_nolo_righe INNER Join condizioni_elementi " &
        '    "On fatture_nolo_righe.descrizione = condizioni_elementi.descrizione INNER Join Fatture_nolo On fatture_nolo_righe.id_fattura = Fatture_nolo.id " &
        '    "Where condizioni_elementi.id_macrovoce = 2 And Fatture_nolo.num_contratto_rif ='" & id_doc & "'"

        Try

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim flagFattura As Boolean = False

            '--verifica se presente fattura
            sqlstr = "Select Fatture_nolo.num_contratto_rif, fatture_nolo.num_fattura from Fatture_nolo " &
            "where Fatture_nolo.num_contratto_rif ='" & id_doc & "' AND attiva=1"


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()
            flagFattura = Rs1.HasRows
            If Rs1.HasRows = True Then
                Rs1.Read()
                Session("numFatturaContratto") = Rs1!num_fattura
            Else
                Session("numFatturaContratto") = ""
            End If
            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing


            'se presente fattura recupera valore della macrovoce passata
            If flagFattura = True Then

                sqlstr = "select sum(fatture_nolo_righe.totale)  As Imponibile FROM fatture_nolo_righe INNER JOIN " &
                "condizioni_elementi ON fatture_nolo_righe.descrizione = condizioni_elementi.descrizione INNER JOIN Fatture_nolo ON fatture_nolo_righe.id_fattura = Fatture_nolo.id " &
                "WHERE (condizioni_elementi.id_macrovoce='" & macrovoce & "' and Fatture_nolo.num_contratto_rif ='" & id_doc & "' And Attiva=1)"

                'solo se rifornimento mancante in fattura per i Carburanti salvo 11.10.2022
                If macrovoce = "1" Then
                    sqlstr = "Select sum(fatture_nolo_righe.totale) As Imponibile From fatture_nolo_righe INNER Join " &
                         "Fatture_nolo On fatture_nolo_righe.id_fattura = Fatture_nolo.id LEFT OUTER Join condizioni_elementi " &
                         "On fatture_nolo_righe.descrizione = condizioni_elementi.descrizione " &
                    "Where (condizioni_elementi.id_macrovoce = '" & macrovoce & "') AND (Fatture_nolo.num_contratto_rif = '" & id_doc & "') OR " &
                         "(Fatture_nolo.num_contratto_rif = '" & id_doc & "') AND (fatture_nolo_righe.descrizione LIKE 'carburante mancante%')"
                End If

                Dim Cmd2 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                Dim Rs2 As Data.SqlClient.SqlDataReader
                Rs2 = Cmd2.ExecuteReader()

                If Rs2.HasRows Then
                    Rs2.Read()
                    If IsDBNull(Rs2!imponibile) Then
                        ris = 0
                    Else
                        ris = Rs2!imponibile
                    End If

                End If

                Rs2.Close()
                Rs2 = Nothing
                Cmd2.Dispose()
                Cmd2 = Nothing


            Else    'se non presente fattura recupera valori dai contratti_costi


                sqlstr = "SELECT SUM(contratti_costi.imponibile) AS AltriCosti, contratti.num_contratto " &
                    "FROM contratti INNER JOIN contratti_costi ON contratti.id = contratti_costi.id_documento LEFT OUTER JOIN " &
                    "condizioni_elementi ON contratti_costi.id_elemento = condizioni_elementi.id " &
                    "WHERE (contratti.num_contratto = '" & id_doc & "') AND (condizioni_elementi.id_macrovoce ='" & macrovoce & "') " &
                    "AND (contratti_costi.selezionato = 1) AND (contratti_costi.omaggiato = 0) " &
                    "GROUP BY contratti.num_contratto"

                Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                If IsDBNull(Cmd.ExecuteScalar) Then
                    ris = 0
                Else
                    ris = Cmd.ExecuteScalar
                End If

                Cmd.Dispose()
                Cmd = Nothing

            End If


            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Libreria.genUserMsgBox(Page, "Errore GetCostiAltri:" & ex.Message)

            End Try

            Return ris

    End Function





End Class
