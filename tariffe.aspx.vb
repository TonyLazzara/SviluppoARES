
Imports System.Data
Imports Libreria

Partial Class tariffe
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim sqlstr As String = "SELECT id, data_creazione, codice, CODTAR, max_sconto, max_sconto_rack, is_broker_prepaid,is_web, is_web_prepagato, codicepromozionale "
        sqlstr += "FROM tariffe WITH(NOLOCK) WHERE attiva='1'  "

        Try
            If Not Page.IsPostBack() Then
                'lblQuery.Text = "SELECT tariffe.id, tariffe.data_creazione, tariffe.codice,tariffe.CODTAR, tariffe.max_sconto, tariffe.max_sconto_rack," & _
                '" tariffe.is_broker_prepaid, CONVERT(Char(10),tariffe_righe.vendibilita_da,103) As vendibilita_da, CONVERT(Char(10),tariffe_righe.vendibilita_a,103) As vendibilita_a, CONVERT(Char(10),tariffe_righe.pickup_Da,103) As pickup_Da, CONVERT(Char(10),tariffe_righe.pickup_a,103) As pickup_a " & _
                '" FROM tariffe WITH(NOLOCK) INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa WHERE attiva='1' "

                lblQuery.Text = sqlstr

                dropTempoKm.DataBind()
                dropTempoKmFiltrato.DataBind()
                dropCondizioni.DataBind()
                dropCondizioniFiltrato.DataBind()
                dropCondizioneMadre.DataBind()
                dropCondizioniMadreFiltrato.DataBind()
                dropTariffaRack.DataBind()
                'dropTempoKmRack.DataBind()
                'dropTempoKmRackFiltrato.DataBind()

                'salvo 10.02.2023 
                ck_modifica_admin.Checked = False
                ck_modifica_admin.Visible = False
                lbl_modifica_admin.Visible = False

            Else

                sqlTariffe.SelectCommand = lblQuery.Text & " ORDER BY codice ASC"
                sqlTariffe.DataBind()

                'Response.Write(sqlDettagliTariffa.SelectCommand & "<br/>")



            End If


        Catch ex As Exception
            Response.Write("error Page_Load : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try

        '# aggiunto per disabilitare campi sconto su tariffe salvo 17.01.2023
        txtMaxSconto.Enabled = False
        txtMaxScontoRack.Enabled = False
        txtMaxSconto.BackColor = Drawing.Color.LightGray
        txtMaxScontoRack.BackColor = Drawing.Color.LightGray
        '@ end aggiunto per disabilitare campi sconto su tariffe salvo 17.01.2023



    End Sub
    Protected Sub nuova_tariffa()

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO tariffe (attiva) VALUES (0)", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd = New Data.SqlClient.SqlCommand("SELECT MAX(id) FROM tariffe WITH(NOLOCK)", Dbc)

            idTariffa.Text = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error nuova tariffa : " & ex.Message & "<br/>")
        End Try
        'CREO UNA RIGA NON ATTIVA IN tariffe

    End Sub

    Protected Sub btnNuovo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovo.Click
        nuova_tariffa()

        btnSalvaIntestazione.Visible = False

        txtNomeTariffa.Text = ""
        txtCodicePromozionale.Text = ""
        btnAnnulla.Text = "Annulla"
        btnSalve.Visible = True

        imposta_filtri()

        'dropCondizioneMadre.Items.Clear()
        'dropCondizioneMadre.Items.Add("Seleziona...")
        'dropCondizioneMadre.Items(0).Value = "0"
        'dropCondizioneMadre.DataBind()

        'dropCondizioneMadre.SelectedValue = "0"
        'dropCondizioneMadre.Enabled = True

        dropBroker.Enabled = True

        tab_cerca.Visible = False
        tab_vedi.Visible = True
    End Sub

    Protected Sub inserisci()




        Dim vendibilita_da As String = vendibilitaDa.Text
        Dim vendibilita_a As String = vendibilitaA.Text
        Dim pickup_da As String = pickUpDa.Text
        Dim pickup_a As String = pickUpA.Text
        Dim max_data As String = ""

        '# Aggiunto salvo 17.01.2023
        Dim max_sconto As String = txt_max_sconto.Text
        If max_sconto = "" Then
            max_sconto = "0"
        End If
        '@ end max sconto




        'CONTROLLI LATO SERVER SULLA CORRETTEZZA DEI DATI.
        If txtMaxDataRilascio.Text <> "" Then
            max_data = "'" & funzioni_comuni.getDataDb_con_orario(txtMaxDataRilascio.Text & " 23:59:59") & "'"
        Else
            max_data = "NULL"
        End If

        vendibilita_da = funzioni_comuni.GetDataSql(vendibilita_da, 0) 'funzioni_comuni.getDataDb_senza_orario(vendibilita_da)
        vendibilita_a = funzioni_comuni.GetDataSql(vendibilita_a, 59) 'funzioni_comuni.getDataDb_con_orario(vendibilita_a & " 23:59:59")
        pickup_da = funzioni_comuni.GetDataSql(pickup_da, 0) 'funzioni_comuni.getDataDb_senza_orario(pickup_da)
        pickup_a = funzioni_comuni.GetDataSql(pickup_a, 59) 'funzioni_comuni.getDataDb_con_orario(pickup_a & " 23:59:59")

        Dim test As Integer
        test = CInt(txtMinutiDiRitardo.Text)
        test = CInt(txtUlterioreTolleranza.Text)

        If Trim(txtMinGiorniNolo.Text) <> "" Then
            test = CInt(txtMinGiorniNolo.Text)
        End If

        If Trim(txtMaxGiorniNolo.Text) <> "" Then
            test = CInt(txtMinGiorniNolo.Text)
        End If

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim condizione_madre As String
        If (filtra.Text = "0" And dropCondizioneMadre.SelectedValue <> "0") Or (filtra.Text = "1" And dropCondizioniMadreFiltrato.SelectedValue <> "0") Then
            If filtra.Text = "1" Then
                condizione_madre = "'" & dropCondizioniMadreFiltrato.SelectedValue & "'"
            ElseIf filtra.Text = "0" Then
                condizione_madre = "'" & dropCondizioneMadre.SelectedValue & "'"
            End If
        Else
            condizione_madre = "NULL"
        End If

        Dim tariffa_rack As String
        If dropTariffaRack.SelectedValue <> "0" Then
            tariffa_rack = "'" & dropTariffaRack.SelectedValue & "'"
        Else
            tariffa_rack = "NULL"
        End If

        'Dim tmp_km_rack As String
        'If (filtra.Text = "0" And dropTempoKmRack.SelectedValue <> "0") Or (filtra.Text = "1" And dropTempoKmRack.SelectedValue <> "0") Then
        '    If filtra.Text = "1" Then
        '        tmp_km_rack = "'" & dropTempoKmRackFiltrato.SelectedValue & "'"
        '    ElseIf filtra.Text = "0" Then
        '        tmp_km_rack = "'" & dropTempoKmRack.SelectedValue & "'"
        '    End If
        'Else
        '    tmp_km_rack = "NULL"
        'End If

        Dim condizioni As String
        If filtra.Text = "1" Then
            condizioni = "'" & dropCondizioniFiltrato.SelectedValue & "'"
        ElseIf filtra.Text = "0" Then
            condizioni = "'" & dropCondizioni.SelectedValue & "'"
        End If

        Dim tempo_km As String
        If filtra.Text = "1" Then
            tempo_km = "'" & dropTempoKmFiltrato.SelectedValue & "'"
        ElseIf filtra.Text = "0" Then
            tempo_km = "'" & dropTempoKm.SelectedValue & "'"
        End If

        Dim min_giorni_nolo As String
        If Trim(txtMinGiorniNolo.Text) <> "" Then
            min_giorni_nolo = "'" & txtMinGiorniNolo.Text & "'"
        Else
            min_giorni_nolo = "NULL"
        End If

        Dim max_giorni_nolo As String
        If Trim(txtMaxGiorniNolo.Text) <> "" Then
            max_giorni_nolo = "'" & txtMaxGiorniNolo.Text & "'"
        Else
            max_giorni_nolo = "NULL"
        End If

        Dim sqlStr As String
        Dim ris As Integer = 0

        sqlStr = "INSERT INTO tariffe_righe (id_tariffa,id_tempo_km,id_condizione,vendibilita_da,vendibilita_a,pickup_da,pickup_a,"
        sqlStr += "minuti_di_ritardo,tolleranza_rientro_nolo,id_condizione_madre, id_tariffa_rack, max_data_rientro, min_giorni_nolo, max_giorni_nolo, max_sconto) VALUES "
        sqlStr += "('" & idTariffa.Text & "'," & tempo_km & "," & condizioni & ",CONVERT(DATETIME,'" & vendibilita_da & "',102),CONVERT(DATETIME,'" & vendibilita_a & "',102),"
        sqlStr += "CONVERT(DATETIME,'" & pickup_da & "',102),CONVERT(DATETIME,'" & pickup_a & "',102),'" & Replace(txtMinutiDiRitardo.Text, ",", ".") & "',"
        sqlStr += "'" & Replace(txtUlterioreTolleranza.Text, ",", ".") & "'," & condizione_madre & ", "
        sqlStr += "" & tariffa_rack & "," & max_data & "," & min_giorni_nolo & "," & max_giorni_nolo & "," & max_sconto & ")"

        Try
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            ris = Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            listRigheTariffa.DataBind()
        Catch ex As Exception
            Response.Write("error inserisci : " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Sub

    Protected Sub modifica(idTariffaVisualizzata As String)

        Dim vendibilita_da As String = vendibilitaDa.Text
        Dim vendibilita_a As String = vendibilitaA.Text
        Dim pickup_da As String = pickUpDa.Text
        Dim pickup_a As String = pickUpA.Text

        '# Aggiunto salvo 17.01.2023
        Dim imax_sconto As String = txt_max_sconto.Text
        If imax_sconto = "" Then
            imax_sconto = "0"
        End If
        '@ end max sconto

        Dim IdTempoKM As String = txt_id_tempokm.Text  'aggiunto salvo 18.01.2023



        Dim max_data As String = ""
        If txtMaxDataRilascio.Text <> "" Then
            'max_data = funzioni_comuni.GetDataSql(txtMaxDataRilascio.Text, 0)
            max_data = funzioni_comuni.GetDataSql(txtMaxDataRilascio.Text, 59) 'FormattaData(max_data) & " 23:59:59"
        End If

        'vendibilita_da = FormattaData(vendibilita_da)
        'vendibilita_a = FormattaData(vendibilita_a) & " 23:59:59"
        'pickup_da = FormattaData(pickup_da)
        'pickup_a = FormattaData(pickup_a) & " 23:59:59"

        vendibilita_da = funzioni_comuni.GetDataSql(vendibilita_da, 0) 'funzioni_comuni.getDataDb_senza_orario(vendibilita_da)
        vendibilita_a = funzioni_comuni.GetDataSql(vendibilita_a, 59) 'funzioni_comuni.getDataDb_con_orario(vendibilita_a & " 23:59:59")
        pickup_da = funzioni_comuni.GetDataSql(pickup_da, 0) 'funzioni_comuni.getDataDb_senza_orario(pickup_da)
        pickup_a = funzioni_comuni.GetDataSql(pickup_a, 59) 'funzioni_comuni.getDataDb_con_orario(pickup_a & " 23:59:59")




        'Dim tariffa_madre As String
        'If dropTariffaMadre.SelectedValue <> "0" Then
        '    tariffa_madre = "'" & dropTariffaMadre.SelectedValue & "',"
        'Else
        '    tariffa_madre = "NULL,"
        'End If

        Dim sqlStr As String
        '" id_tariffa = '" & idTariffa.Text & "'," & _
        '" id_tempo_km = '" & dropTempoKm.SelectedValue & "'," & _
        '" id_condizione = '" & dropCondizioni.SelectedValue & "'," & _

        sqlStr = "UPDATE tariffe_righe SET"
        sqlStr += " vendibilita_da = CONVERT(DATETIME,'" & vendibilita_da & "',102),"
        sqlStr += " vendibilita_a = CONVERT(DATETIME,'" & vendibilita_a & "',102),"
        sqlStr += " pickup_da = CONVERT(DATETIME,'" & pickup_da & "',102),"
        sqlStr += " pickup_a = CONVERT(DATETIME,'" & pickup_a & "',102),"
        sqlStr += " minuti_di_ritardo = '" & Replace(txtMinutiDiRitardo.Text, ",", ".") & "',"
        sqlStr += " tolleranza_rientro_nolo='" & Replace(txtUlterioreTolleranza.Text, ",", ".") & "',"

        If max_data <> "" Then
            sqlStr = sqlStr & " max_data_rientro = CONVERT(DATETIME,'" & max_data & "',102),"
        Else
            sqlStr = sqlStr & " max_data_rientro=NULL,"
        End If

        If Trim(txtMinGiorniNolo.Text) <> "" Then
            sqlStr = sqlStr & " min_giorni_nolo='" & txtMinGiorniNolo.Text & "',"
        Else
            sqlStr = sqlStr & " min_giorni_nolo=NULL,"
        End If

        If Trim(txtMaxGiorniNolo.Text) <> "" Then
            sqlStr = sqlStr & " max_giorni_nolo='" & txtMaxGiorniNolo.Text & "' "
        Else
            sqlStr = sqlStr & " max_giorni_nolo=NULL "
        End If

        '# aggiunto salvo 17.01.2023
        If Trim(imax_sconto) <> "" Then
            sqlStr = sqlStr & ", max_sconto='" & imax_sconto & "' "
        Else
            sqlStr = sqlStr & ", max_sconto=0 "
        End If
        '@ end aggiunto salvo 17.01.2023



        sqlStr = sqlStr & " WHERE Id = '" & idTariffaVisualizzata & "'"

        Dim ris As Integer = 0

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            ris = Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing


            '# aggiorna le righe di tempokm salvo 18.01.2023 con lo sconto
            'per il successivo recupero x singola riga
            sqlStr = "UPDATE righe_tempo_km set max_sconto='" & Trim(txt_max_sconto.Text) & "' where righe_tempo_km.id_tempo_km = '" & Trim(txt_id_tempokm.Text) & "'"
            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            ris = Cmd1.ExecuteNonQuery()
            Cmd1.Dispose()
            Cmd1 = Nothing
            '@end aggiorna le righe di tempokm salvo 18.01.2023


            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing



            listRigheTariffa.DataBind()


        Catch ex As Exception
            Response.Write("error modifica : " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try


    End Sub

    Protected Function checkVendibilita(Optional Id As String = "") As Boolean
        'TEMPO KM E CONDIZIONI VANNO IN PARALLELO COME RANGE DI VENDIBILITA'
        Dim vendibilita_da As String = vendibilitaDa.Text
        Dim vendibilita_a As String = vendibilitaA.Text
        Dim pickup_da As String = pickUpDa.Text
        Dim pickup_a As String = pickUpA.Text

        'vendibilita_da = FormattaData(vendibilita_da)
        'vendibilita_a = FormattaData(vendibilita_a)
        'pickup_da = FormattaData(pickup_da)
        'pickup_a = FormattaData(pickup_a)

        vendibilita_da = funzioni_comuni.GetDataSql(vendibilita_da, 0) 'funzioni_comuni.getDataDb_senza_orario(vendibilita_da)
        vendibilita_a = funzioni_comuni.GetDataSql(vendibilita_a, 59) 'funzioni_comuni.getDataDb_con_orario(vendibilita_a & " 23:59:59")
        pickup_da = funzioni_comuni.GetDataSql(pickup_da, 0) 'funzioni_comuni.getDataDb_senza_orario(pickup_da)
        pickup_a = funzioni_comuni.GetDataSql(pickup_a, 59) 'funzioni_comuni.getDataDb_con_orario(pickup_a & " 23:59:59")


        ' verifico che per la stessa tariffa non vi sia contemporanea sovrapposizione
        ' della data della vendibilità e della data di pickup
        Dim sqlStr As String = "SELECT TOP 1 id FROM tariffe_righe WITH(NOLOCK) WHERE"

        'If Id <> "" Then
        '    sqlStr = sqlStr & " (id <> '" & Id & "') AND"
        'End If
        'sqlStr += " (id_tariffa = '" & idTariffa.Text & "') AND"

        ''da entermed
        If Id <> "" Then
            sqlStr = sqlStr & " (id <> '" & Id & "') AND"
        End If

        'Da ultimo ENTERMED
        sqlStr += " (id_tariffa = '" & idTariffa.Text & "') AND" &
            " ((convert(datetime,'" & vendibilita_da & "',102) BETWEEN vendibilita_da AND vendibilita_a) " &
            "OR (convert(datetime,'" & vendibilita_a & "',102) BETWEEN vendibilita_Da AND vendibilita_a) " &
            "OR (convert(datetime,'" & vendibilita_da & "',102) < vendibilita_da AND convert(datetime,'" & vendibilita_a & "',102) > vendibilita_a)) " &
            "And ((convert(datetime,'" & pickup_da & "',102) BETWEEN pickup_da AND pickup_a) OR (convert(datetime,'" & pickup_a & "',102) " &
            "BETWEEN pickup_Da AND pickup_a) OR (convert(datetime,'" & pickup_da & "',102) < pickup_da AND convert(datetime,'" & pickup_a & "',102) > pickup_a))"

        '''OLD
        'sqlStr += " ((convert(datetime,'" & vendibilita_da & "',102) BETWEEN CONVERT(DATETIME,'" & vendibilita_da & "',102) AND CONVERT(DATETIME,'" & vendibilita_a & "',102)) "
        'sqlStr += "Or ('" & vendibilita_a & "' BETWEEN CONVERT(DATETIME,'" & vendibilita_da & "',102) AND BETWEEN CONVERT(DATETIME,'" & vendibilita_a & "',102)) "
        'sqlStr += "Or (convert(datetime,'" & vendibilita_da & "',102) < BETWEEN CONVERT(DATETIME,'" & vendibilita_da & "',102) And convert(datetime,'" & vendibilita_a & "',102) "
        'sqlStr += "> BETWEEN CONVERT(DATETIME,'" & vendibilita_a & "',102))) " '4

        'sqlStr += "AND ((convert(datetime,'" & pickup_da & "',102) BETWEEN CONVERT(DATETIME,'" & pickup_da & "',102) " '5
        'sqlStr += "And CONVERT(DATETIME,'" & pickup_da & "',102)) OR (convert(datetime,'" & pickup_a & "',102) BETWEEN CONVERT(DATETIME,'" & pickup_da & "',102) " '6

        'sqlStr += "And CONVERT(DATETIME,'" & pickup_a & "',102)) OR (convert(datetime,'" & pickup_da & "',102) < CONVERT(DATETIME,'" & pickup_da & "',102) " '7
        'sqlStr += "And convert(datetime,'" & pickup_a & "',102) > CONVERT(DATETIME,'" & pickup_a & "',102)))" '8


        '# TEST
        'Response.Write(sqlStr & "<br/>")
        'checkVendibilita = False
        'Exit Function
        '# TEST
        Try
            Using Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString),
                            Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                Dbc.Open()
                Dim test As String = Cmd.ExecuteScalar

                If test <> "" Then
                    checkVendibilita = False
                Else
                    checkVendibilita = True
                End If
            End Using
        Catch ex As Exception
            Response.Write("error checkVendibilita : " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try


    End Function

    Protected Function controlli_server_side_ok() As Boolean
        controlli_server_side_ok = True
        Try
            'CONTROLLI LATO SERVER SULLA CORRETTEZZA DEI DATI.
            Dim max_data As String
            If txtMaxDataRilascio.Text <> "" Then
                'max_data = "'" & funzioni_comuni.getDataDb_con_orario(txtMaxDataRilascio.Text & " 23:59:59") & "'"
                max_data = "'" & funzioni_comuni.GetDataSql(txtMaxDataRilascio.Text, 59) & "'" 'FormattaData(max_data) & " 23:59:59"
            Else
                max_data = "NULL"
            End If

            'vendibilita_da = funzioni_comuni.getDataDb_senza_orario(vendibilita_da)
            'vendibilita_a = funzioni_comuni.getDataDb_senza_orario(vendibilita_a)
            'pickup_da = funzioni_comuni.getDataDb_senza_orario(pickup_da)
            'pickup_a = funzioni_comuni.getDataDb_con_orario(pickup_a & " 23:59:59")

            Dim test As Integer
            test = CInt(txtMinutiDiRitardo.Text)
            test = CInt(txtUlterioreTolleranza.Text)

            If Trim(txtMinGiorniNolo.Text) <> "" Then
                test = CInt(txtMinGiorniNolo.Text)
            End If

            If Trim(txtMaxGiorniNolo.Text) <> "" Then
                test = CInt(txtMinGiorniNolo.Text)
            End If
        Catch ex As Exception
            controlli_server_side_ok = False
        End Try
    End Function

    Protected Sub btnInserisci_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInserisci.Click

        Try
            If controlli_server_side_ok() Then
                If btnInserisci.Text = "Inserisci" Then
                    If Not checkVendibilita() Then
                        Libreria.genUserMsgBox(Me, "Attenzione: il range di pick-up specificato si sovrappone ad un range precedentemente inserito.")
                    ElseIf (filtra.Text = "0" And dropCondizioneMadre.SelectedValue = dropCondizioni.Text) Or (filtra.Text = "1" And dropCondizioniMadreFiltrato.SelectedValue = dropCondizioniFiltrato.Text) Then
                        Libreria.genUserMsgBox(Me, "Impossibile specificare la stessa condizione come madre. Non selezionarne nessuna per indicare che la condizione non ha alcuna madre.")
                    Else
                        inserisci()

                        pulisciCampi()
                    End If
                ElseIf btnInserisci.Text = "Modifica" Then
                    If Not checkVendibilita(idTariffaVisualizzata.Text) Then
                        Libreria.genUserMsgBox(Me, "Attenzione: il range di pick-up specificato si sovrappone ad un range precedentemente inserito.")
                    Else

                        'verifica
                        '# inserito per verificare se la data di pickup_da è presente in qualche tariffa  salvo 08.02.2023
                        'utilizzata da qualche contratto ancora attivo e aperto (status 2) 
                        If lbl_pickup_da.Text <> "" Then
                            If lbl_pickup_da.Text <> pickUpDa.Text Then 'stessa data procede all'aggiornamento
                                Dim data_originale_pickup_da As String = funzioni_comuni.GetDataSql(pickUpDa.Text, 0)
                                If ck_modifica_admin.Checked = False Then 'se il ck attivo aggiorna id dati
                                    Dim ListContratti As String = VerificaPickUpTariffe(data_originale_pickup_da, txt_id_tempokm.Text, txt_id_tariffa.Text)
                                    If ListContratti <> "" Then
                                        genUserMsgBox(Page, "ATTENZIONE!,\nLa data di pickup da che si vuole modificare (" & lbl_pickup_da.Text & ")\nè presente in alcuni contratti aperti come data di PickUp \ne pertanto è necessario attivare il check 'Modifica Admin' \nper salvare la tariffa con la nuova data di pick up (" & pickUpDa.Text & ").")
                                        'pickUpDa.Text = lbl_pickup_da.Text
                                        lbl_list_contratti.Text = ListContratti
                                        lbl_modifica_admin.Visible = True
                                        ck_modifica_admin.Visible = True
                                        Exit Sub
                                    Else
                                        lbl_list_contratti.Text = ""
                                        lbl_modifica_admin.Visible = False
                                        ck_modifica_admin.Visible = False
                                        ck_modifica_admin.Checked = False
                                    End If
                                End If
                            Else
                                lbl_list_contratti.Text = ""
                                lbl_modifica_admin.Visible = False
                                ck_modifica_admin.Visible = False
                                ck_modifica_admin.Checked = False
                            End If
                            '@end salvo 08.02.2023



                        End If

                        modifica(idTariffaVisualizzata.Text)

                        pulisciCampi()



                    End If
                End If

            Else
                Libreria.genUserMsgBox(Me, "Si è verificato un errore imprevisto. Si prega di riprovare.")
            End If
        Catch ex As Exception
            Response.Write("error btnInserisci_Click : " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Function qualcosa_salvato() As Boolean
        Dim sqlstr As String = "SELECT id FROM tariffe_righe WITH(NOLOCK) WHERE id_tariffa='" & idTariffa.Text & "' "
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim test As String = Cmd.ExecuteScalar

            If test = "" Then
                qualcosa_salvato = False
            Else
                qualcosa_salvato = True
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error qualcosa_salvato : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try



    End Function

    Protected Sub salva_tariffa()

        Dim t As String = Replace(txtNomeTariffa.Text, "'", "''")
        t = Trim(t)

        Dim sqlstr As String = "UPDATE tariffe SET data_creazione=getDate(), codice='" & t & "', id_utente_creazione='" & Request.Cookies("SicilyRentCar")("IdUtente") & "', attiva='1', max_sconto='" & Replace(txtMaxSconto.Text, ",", ".") & "', max_sconto_rack='" & Replace(txtMaxScontoRack.Text, ",", ".") & "',  codtar='" & Replace(txtCodtar.Text, "'", "''") & "', is_broker_prepaid='" & dropBroker.SelectedValue & "' WHERE id='" & idTariffa.Text & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error salva_tariffa : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try



    End Sub

    Protected Sub btnSalve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalve.Click
        If qualcosa_salvato() Then
            salva_tariffa()
            Libreria.genUserMsgBox(Me, "Tariffa salvata correttamente.")
            btnAnnulla.Text = "Torna alla lista"
            btnSalve.Visible = False

            btnSalvaIntestazione.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Inserire almeno un Valore di Tariffa o una Condizione")
        End If

    End Sub

    Protected Sub imposta_filtri()
        dropTempoKm.Visible = False
        dropTempoKmFiltrato.Visible = True
        dropCondizioni.Visible = False
        dropCondizioniFiltrato.Visible = True
        dropCondizioneMadre.Visible = False
        dropCondizioniMadreFiltrato.Visible = True
        'dropTariffaRack.Visible = False
        'dropTempoKmRack.Visible = False
        'dropTempoKmRackFiltrato.Visible = True

        compareCondizioni.Enabled = False
        compareCondizioniFiltrato.Enabled = True
        compareTempoKm.Enabled = False
        compareTempoKmFiltrato.Enabled = True

        filtra.Text = "1"
        btnFiltra.Text = "Non Filtrare"
        btnFiltra.Enabled = True
    End Sub

    Protected Sub rimuovi_filtri()
        dropTempoKm.Visible = True
        dropTempoKmFiltrato.Visible = False
        dropCondizioni.Visible = True
        dropCondizioniFiltrato.Visible = False
        dropCondizioneMadre.Visible = True
        dropCondizioniMadreFiltrato.Visible = False
        'dropTariffaRack.Visible = True
        'dropTempoKmRack.Visible = True
        'dropTempoKmRackFiltrato.Visible = False

        compareCondizioni.Enabled = True
        compareCondizioniFiltrato.Enabled = False
        compareTempoKm.Enabled = True
        compareTempoKmFiltrato.Enabled = False

        filtra.Text = "0"
        btnFiltra.Text = "Filtra"
        btnFiltra.Enabled = True
    End Sub

    Protected Sub listTariffe_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listTariffe.ItemCommand
        If e.CommandName = "vedi" Then
            Dim id_tariffa As Label = e.Item.FindControl("idLabel")
            Dim nome_tariffa As Label = e.Item.FindControl("codiceLabel")
            Dim tariffa_madre As Label = e.Item.FindControl("tariffa_madre")
            Dim max_sconto As Label = e.Item.FindControl("max_sconto")
            Dim max_sconto_rack As Label = e.Item.FindControl("max_sconto_rack")
            Dim tipo_pagamento As Label = e.Item.FindControl("pagamento_obbligatorio")
            Dim tariffa_broker As Label = e.Item.FindControl("tariffa_broker")
            Dim CODTAR As Label = e.Item.FindControl("CODTAR")
            Dim IsWeb As Label = e.Item.FindControl("IsWeb")
            Dim IsWebPrepagata As Label = e.Item.FindControl("IsWebPrepagata")
            Dim codicepromozionale As Label = e.Item.FindControl("codicepromozionale")

            idTariffaVisualizzata.Text = "0"

            dropTempoKm.Enabled = True
            dropTempoKmFiltrato.Enabled = True
            dropCondizioni.Enabled = True
            dropCondizioniFiltrato.Enabled = True
            dropCondizioneMadre.Enabled = True
            dropCondizioniMadreFiltrato.Enabled = True
            dropTariffaRack.Enabled = True
            txtCodicePromozionale.Text = codicepromozionale.Text
            'dropTempoKmRack.Enabled = True
            'dropTempoKmRackFiltrato.Enabled = True
            imposta_filtri()

            If tariffa_broker.Text Then
                dropBroker.SelectedValue = "1"
            Else
                dropBroker.SelectedValue = "0"
            End If
            'E' POSSIBILE MODIFICARE SE LA TARIFFA E' O MENO DI TIPO BROKER SOLAMENTE SE NON HA DELLE TIPOLOGIE DI CLIENTI GIA' COLLEGATE:
            'IN QUESTO CASO LA TIPOLOGIA DI CLIENTI E' CONGRUENTE COL VALORE ATTUALMENTE SALVATO, E QUINDI NON E' POSSIBILE VARIARLO
            If brokerModificabile(id_tariffa.Text) Then
                dropBroker.Enabled = True
            Else
                dropBroker.Enabled = False
            End If

            txtMaxSconto.Text = max_sconto.Text
            txtMaxScontoRack.Text = max_sconto_rack.Text

            txtNomeTariffa.Text = nome_tariffa.Text
            txtCodtar.Text = CODTAR.Text
            idTariffa.Text = id_tariffa.Text

            If IsWeb.Text <> "" Then
                If IsWeb.Text Then
                    DropDownListWeb.SelectedValue = "1"
                Else
                    DropDownListWeb.SelectedValue = "0"
                End If
            Else
                DropDownListWeb.SelectedValue = "0"
            End If

            If IsWebPrepagata.Text <> "" Then
                If IsWebPrepagata.Text Then
                    DropDownListWebPrepagata.SelectedValue = "1"
                Else
                    DropDownListWebPrepagata.SelectedValue = "0"
                End If
            Else
                DropDownListWebPrepagata.SelectedValue = "0"
            End If

            Try
                If id_tariffa.Text = 24 Then 'DriveMe
                    sqlDettagliTariffa.SelectCommand = "SELECT tariffe_righe.max_sconto , tariffe_righe.id, minuti_di_ritardo, tolleranza_rientro_nolo, id_condizione_madre, tariffe_righe.id_tariffa_rack, (condizione_madre.codice + ' - ' + CONVERT(char(10),condizione_madre.valido_da,103) + ' - ' + CONVERT(char(10),condizione_madre.valido_a,103)) As cond_madre, (tempo_km.codice + ' - ' + CONVERT(char(10),tempo_km.valido_da,103) + ' - ' + CONVERT(char(10),tempo_km.valido_a,103)) As tempo_km, (tariffa_rack.codice) As tariffa_rack, id_tempo_km, (condizione_figlia.codice + ' - ' + CONVERT(char(10),condizione_figlia.valido_da,103) + ' - ' + CONVERT(char(10),condizione_figlia.valido_a,103)) As condizione, id_condizione, convert(char(10),vendibilita_da,103) As vendibilitaDa, convert(char(10),vendibilita_a,103) As vendibilita_a, convert(char(10),pickup_da,103) As pickup_da, convert(char(10),pickup_a,103) As pickup_a, CONVERT(char(10),max_data_rientro,103) As max_data_rientro, min_giorni_nolo, max_giorni_nolo FROM tariffe_righe WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON tariffe_righe.id_tempo_km=tempo_km.id LEFT JOIN tariffe As tariffa_rack WITH(NOLOCK) ON tariffe_righe.id_tariffa_rack=tariffa_rack.id INNER JOIN condizioni As condizione_figlia WITH(NOLOCK) ON tariffe_righe.id_condizione=condizione_figlia.id LEFT JOIN condizioni As condizione_madre WITH(NOLOCK) ON tariffe_righe.id_condizione_madre=condizione_madre.id WHERE ([id_tariffa] = 24) and vendibilita_a > '01/01/" & Year(Now) & "' ORDER BY tariffe_righe.pickup_da DESC"
                End If
                listRigheTariffa.DataBind()
            Catch ex As Exception
                Response.Write("error listRigheTariffa.DataBind() itemcommand : " & ex.Message & "<br/>" & "<br/>")
            End Try



            btnSalve.Visible = False
            btnAnnulla.Text = "Torna alla lista"
            btnAnnullaModifica.Visible = False

            tab_cerca.Visible = False
            tab_vedi.Visible = True
            tab_fonti_stazioni.Visible = False

            btnSalvaIntestazione.Visible = True


            txt_id_tariffa.Text = id_tariffa.Text   'aggiunto salvo 09.02.2023


        ElseIf e.CommandName = "fonti_stazioni" Then

            Dim id_tariffa As Label = e.Item.FindControl("idLabel")
            idTariffa.Text = id_tariffa.Text

            tab_cerca.Visible = False
            tab_vedi.Visible = False
            tab_fonti_stazioni.Visible = True

            listTipoClienti.Items.Clear()

            tariffaBroker.Text = is_tariffa_broker(id_tariffa.Text)
            listTipoClienti.DataBind()

            listTipoClientiSelezionati.Items.Clear()

            listTipoClientiSelezionati.Width = listTipoClienti.Width

            listTipoClienti.Items.Clear()
            listTipoClientiSelezionati.Items.Clear()
            listTipoClienti.DataBind()

            listStazioniPick.Items.Clear()
            listStazioniPickSelezionate.Items.Clear()
            listStazioniPick.DataBind()

            listStazioniDrop.Items.Clear()
            listStazioniDropSelezionate.Items.Clear()

            Try
                listStazioniDrop.DataBind()
            Catch ex As Exception
                Response.Write("error listStazioniDrop.DataBind() itemcommand : " & ex.Message & "<br/>" & "<br/>")
            End Try


            FillFonti()

            FillPickUp()

            FillDropOff()

            Try
                listRegoleXFonti.DataBind()
            Catch ex As Exception
                Response.Write("error listRegoleXFonti.DataBind() itemcommand : " & ex.Message & "<br/>" & "<br/>")
            End Try



        ElseIf e.CommandName = "elimina" Then

            Dim id_tariffa As Label = e.Item.FindControl("idLabel")
            If tariffaEliminabile(id_tariffa.Text) Then
                elimina_tariffa_salvata(id_tariffa.Text)
                Try
                    listTariffe.DataBind()
                Catch ex As Exception
                    Response.Write("error   listTariffe.DataBind() itemcommand : " & ex.Message & "<br/>" & "<br/>")
                End Try

                Libreria.genUserMsgBox(Me, "Tariffa eliminata correttamente.")
            Else
                Libreria.genUserMsgBox(Me, "Impossibile eliminare la tariffa specificata in quanto è stata già utilizzata.")
            End If
        End If
    End Sub

    Protected Function brokerModificabile(ByVal id_tariffa As String) As Boolean
        'NON E' POSSIBILE MODIFICARE LA TIPOLOGIA DI TARIFFA SIA SE E' VI SONO FONTI AD ESSA COLLEGATE SIA SE E' GIA' STATA UTILIZZATA

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM tariffe_X_fonti WITH(NOLOCK) WHERE id_tariffa='" & id_tariffa & "'", Dbc)

            Dim test As String = Cmd.ExecuteScalar & ""

            If test <> "" Then
                brokerModificabile = False
            Else
                Cmd = New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM contratti WITH(NOLOCK) WHERE id_tariffa='" & id_tariffa & "' AND attivo='1'", Dbc)
                test = Cmd.ExecuteScalar & ""
                If test <> "" Then
                    brokerModificabile = False
                Else
                    Cmd = New Data.SqlClient.SqlCommand("SELECT TOP 1 Nr_Pren FROM prenotazioni WITH(NOLOCK) WHERE id_tariffa='" & id_tariffa & "' AND attiva='1'", Dbc)
                    test = Cmd.ExecuteScalar & ""
                    If test <> "" Then
                        brokerModificabile = False
                    Else
                        Cmd = New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM preventivi WITH(NOLOCK) WHERE id_tariffa='" & id_tariffa & "'", Dbc)
                        test = Cmd.ExecuteScalar & ""
                        If test <> "" Then
                            brokerModificabile = False
                        Else
                            brokerModificabile = True
                        End If
                    End If
                End If
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error brokerModificabile : " & ex.Message & "<br/>" & "<br/>")
        End Try


    End Function

    Protected Function tariffaEliminabile(ByVal id_tariffa As String) As Boolean
        'NON E' POSSIBILE ELIMINARE UNA TARIFFA SE E' GIA' STATA UTILIZZATA
        Dim sqlstr As String

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim test As String
            sqlstr = "SELECT TOP 1 id FROM contratti WITH(NOLOCK) WHERE id_tariffa='" & id_tariffa & "' AND attivo='1'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            test = Cmd.ExecuteScalar & ""
            If test <> "" Then
                tariffaEliminabile = False
            Else
                sqlstr = "SELECT TOP 1 Nr_Pren FROM prenotazioni WITH(NOLOCK) WHERE id_tariffa='" & id_tariffa & "' AND attiva='1'"
                Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                test = Cmd.ExecuteScalar & ""
                If test <> "" Then
                    tariffaEliminabile = False
                Else
                    sqlstr = "SELECT TOP 1 id FROM preventivi WITH(NOLOCK) WHERE id_tariffa='" & id_tariffa & "'"
                    Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                    test = Cmd.ExecuteScalar & ""
                    If test <> "" Then
                        tariffaEliminabile = False
                    Else
                        tariffaEliminabile = True
                    End If
                End If
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error tariffaEliminabile : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try



    End Function

    Protected Function is_tariffa_broker(ByVal id_tariffa As String) As String
        'RESTITUISCE 1 SE LA TARIFFA SCELTA E' UNA TARIFFA BROKER - 0 ALTRIMENTI
        Dim sqlstr As String = "SELECT is_broker_prepaid FROM tariffe WITH(NOLOCK) WHERE id='" & id_tariffa & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            Dim test As Boolean = Cmd.ExecuteScalar

            If test Then
                is_tariffa_broker = "1"
            Else
                is_tariffa_broker = "0"
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error is_tariffa_broker : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try



    End Function

    Protected Sub FillDropOff()

        Dim sqlstr As String = "SELECT id_stazione FROM tariffe_X_stazioni WITH(NOLOCK) WHERE id_tariffa='" & idTariffa.Text & "' AND tipo='DROP'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Cmd.ExecuteNonQuery()

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.Read() Then

                radioSel3.SelectedValue = 0
                Dim k As Integer = 0

                Do
                    For i = 0 To listStazioniDrop.Items.Count - 1
                        If listStazioniDrop.Items(i).Value = Rs("id_stazione") Then
                            listStazioniDropSelezionate.Items.Add(listStazioniDrop.Items(i).Text)
                            listStazioniDropSelezionate.Items(k).Value = Rs("id_stazione")
                            listStazioniDrop.Items.RemoveAt(i)
                            k = k + 1
                            Exit For
                        End If
                    Next
                Loop Until Not Rs.Read()

                listStazioniDrop.Enabled = True
                listStazioniDropSelezionate.Enabled = True
                PassaTuttiDrop.Enabled = True
                PassaUnoDrop.Enabled = True
                TornaTuttiDrop.Enabled = True
                TornaUnoDrop.Enabled = True

            Else

                'SE NON TROVO NIENTE VUOL DIRE CHE E' LA TARIFFA E' APPLICABILE SENZA RESTRIZIONI
                radioSel3.SelectedValue = 1

                listStazioniDrop.Enabled = False
                listStazioniDropSelezionate.Enabled = False
                PassaTuttiDrop.Enabled = False
                PassaUnoDrop.Enabled = False
                TornaTuttiDrop.Enabled = False
                TornaUnoDrop.Enabled = False

            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error FillDropOff : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try

    End Sub

    Protected Sub FillPickUp()

        Dim sqlstr As String = "SELECT id_stazione FROM tariffe_X_stazioni WITH(NOLOCK) WHERE id_tariffa='" & idTariffa.Text & "' AND tipo='PICK'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Cmd.ExecuteNonQuery()

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.Read() Then
                radioSel2.SelectedValue = 0
                Dim k As Integer = 0

                Do
                    For i = 0 To listStazioniPick.Items.Count - 1
                        If listStazioniPick.Items(i).Value = Rs("id_stazione") Then
                            listStazioniPickSelezionate.Items.Add(listStazioniPick.Items(i).Text)
                            listStazioniPickSelezionate.Items(k).Value = Rs("id_stazione")
                            listStazioniPick.Items.RemoveAt(i)
                            k = k + 1
                            Exit For
                        End If
                    Next
                Loop Until Not Rs.Read()

                listStazioniPick.Enabled = True
                listStazioniPickSelezionate.Enabled = True
                PassaTuttiPick.Enabled = True
                PassaUnoPick.Enabled = True
                TornaTuttiPick.Enabled = True
                TornaUnoPick.Enabled = True

            Else
                'SE NON TROVO NIENTE VUOL DIRE CHE E' LA TARIFFA E' APPLICABILE SENZA RESTRIZIONI
                radioSel2.SelectedValue = 1

                listStazioniPick.Enabled = False
                listStazioniPickSelezionate.Enabled = False
                PassaTuttiPick.Enabled = False
                PassaUnoPick.Enabled = False
                TornaTuttiPick.Enabled = False
                TornaUnoPick.Enabled = False

            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error FillPickUp : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try



    End Sub

    Protected Sub FillFonti()

        Dim sqlstr As String = "SELECT id_tipologia_cliente FROM tariffe_X_fonti WITH(NOLOCK) WHERE id_tariffa='" & idTariffa.Text & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Cmd.ExecuteNonQuery()

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.Read() Then
                radioSel1.SelectedValue = 0
                Dim k As Integer = 0

                Do
                    For i = 0 To listTipoClienti.Items.Count - 1
                        If listTipoClienti.Items(i).Value = Rs("id_tipologia_cliente") Then
                            listTipoClientiSelezionati.Items.Add(listTipoClienti.Items(i).Text)
                            listTipoClientiSelezionati.Items(k).Value = Rs("id_tipologia_cliente")
                            listTipoClienti.Items.RemoveAt(i)
                            k = k + 1
                            Exit For
                        End If
                    Next
                Loop Until Not Rs.Read()

                listTipoClienti.Enabled = True
                listTipoClientiSelezionati.Enabled = True
                PassaTuttoClienti.Enabled = True
                PassaUnoClienti.Enabled = True
                TornaTuttoClienti.Enabled = True
                TornaUnoClienti.Enabled = True

            Else
                'SE NON TROVO NIENTE VUOL DIRE CHE E' LA TARIFFA E' APPLICABILE SENZA RESTRIZIONI
                radioSel1.SelectedValue = 1

                listTipoClienti.Enabled = False
                listTipoClientiSelezionati.Enabled = False
                PassaTuttoClienti.Enabled = False
                PassaUnoClienti.Enabled = False
                TornaTuttoClienti.Enabled = False
                TornaUnoClienti.Enabled = False
            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error FillFonti : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try

    End Sub

    Protected Sub elimina_tariffa_salvata(ByVal id_tariffa As String)

        Dim sqlstr As String = "UPDATE tariffe SET attiva='0' WHERE id='" & id_tariffa & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error elimina_tariffa_salvata : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try

    End Sub

    Protected Sub elimina_intera_tariffa(ByVal id_tariffa As String)

        Dim sqlstr As String = "DELETE FROM tariffe_righe WHERE id_tariffa='" & id_tariffa & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Cmd.ExecuteNonQuery()

            sqlstr = "DELETE FROM tariffe_x_fonti WHERE id_tariffa='" & id_tariffa & "'"
            Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Cmd.ExecuteNonQuery()

            sqlstr = "DELETE FROM tariffe_x_stazioni WHERE id_tariffa='" & id_tariffa & "'"
            Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Cmd.ExecuteNonQuery()

            sqlstr = "DELETE FROM tariffe WHERE id='" & id_tariffa & "'"
            Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error elimina_tariffa_salvata : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click

        Try
            If btnAnnulla.Text = "Annulla" Then
                'NUOVO INSERIMENTO - LA TARIFFA NON E' ATTIVA E VA QUINDI CANCELLATA
                elimina_intera_tariffa(idTariffa.Text)

                idTariffa.Text = ""

                listTariffe.DataBind()
                tab_cerca.Visible = True
                tab_vedi.Visible = False
            Else
                'MODIFICA
                idTariffa.Text = ""

                listTariffe.DataBind()
                tab_cerca.Visible = True
                tab_vedi.Visible = False
            End If

            txtNomeTariffa.Text = ""
            txtCodtar.Text = ""
            vendibilitaA.Text = ""
            vendibilitaDa.Text = ""
            pickUpA.Text = ""
            pickUpDa.Text = ""
            lbl_pickup_da.Text = "" 'aggiunto salvo 08.02.2023
            lbl_list_contratti.Text = "" 'aggiunto salvo 08.02.2023
            lbl_modifica_admin.Visible = False
            ck_modifica_admin.Visible = False
            ck_modifica_admin.Checked = False


            txtMinutiDiRitardo.Text = ""
            txtMaxDataRilascio.Text = ""
            dropTempoKm.SelectedValue = "0"
            dropTempoKmFiltrato.SelectedValue = "0"
            dropCondizioni.SelectedValue = "0"
            dropCondizioniFiltrato.SelectedValue = "0"
            dropCondizioniMadreFiltrato.SelectedValue = "0"
            dropCondizioneMadre.SelectedValue = "0"
            dropTariffaRack.SelectedValue = "0"
            'dropTempoKmRack.SelectedValue = "0"
            'dropTempoKmRackFiltrato.SelectedValue = "0"
            dropBroker.SelectedValue = "2"

            txtMaxSconto.Text = ""
            txtMaxScontoRack.Text = ""
        Catch ex As Exception
            Response.Write("error btnAnnulla_Click: " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Sub btnTorna_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTorna.Click
        tab_cerca.Visible = True
        tab_vedi.Visible = False
        tab_fonti_stazioni.Visible = False

        idTariffa.Text = ""
    End Sub


    Protected Sub radioSel1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radioSel1.SelectedIndexChanged
        If radioSel1.SelectedValue = "1" Then
            listTipoClienti.Enabled = False
            listTipoClientiSelezionati.Enabled = False
            PassaTuttoClienti.Enabled = False
            PassaUnoClienti.Enabled = False
            TornaTuttoClienti.Enabled = False
            TornaUnoClienti.Enabled = False
        Else
            listTipoClienti.Enabled = True
            listTipoClientiSelezionati.Enabled = True
            listTipoClientiSelezionati.Enabled = True
            PassaTuttoClienti.Enabled = True
            PassaUnoClienti.Enabled = True
            TornaTuttoClienti.Enabled = True
            TornaUnoClienti.Enabled = True
        End If
    End Sub

    Protected Sub radioSel2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radioSel2.SelectedIndexChanged
        If radioSel2.SelectedValue = "1" Then
            listStazioniPick.Enabled = False
            listStazioniPickSelezionate.Enabled = False
            PassaTuttiPick.Enabled = False
            PassaUnoPick.Enabled = False
            TornaTuttiPick.Enabled = False
            TornaUnoPick.Enabled = False
        Else
            listStazioniPick.Enabled = True
            listStazioniPickSelezionate.Enabled = True
            PassaTuttiPick.Enabled = True
            PassaUnoPick.Enabled = True
            TornaTuttiPick.Enabled = True
            TornaUnoPick.Enabled = True
        End If
    End Sub

    Protected Sub radioSel3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radioSel3.SelectedIndexChanged
        If radioSel3.SelectedValue = "1" Then
            listStazioniDrop.Enabled = False
            listStazioniDropSelezionate.Enabled = False
            PassaTuttiDrop.Enabled = False
            PassaUnoDrop.Enabled = False
            TornaTuttiDrop.Enabled = False
            TornaUnoDrop.Enabled = False
        Else
            listStazioniDrop.Enabled = True
            listStazioniDropSelezionate.Enabled = True
            PassaTuttiDrop.Enabled = True
            PassaUnoDrop.Enabled = True
            TornaTuttiDrop.Enabled = True
            TornaUnoDrop.Enabled = True
        End If
    End Sub

    Protected Function fonte_non_collegata(ByVal id_tariffa As String, ByVal id_fonte As String) As Boolean

        Dim sqlstr As String = "SELECT TOP 1 id FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tariffa='" & id_tariffa & "' AND id_tipologia_cliente='" & id_fonte & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            Dim test As String = Cmd.ExecuteScalar & ""

            If test <> "" Then
                fonte_non_collegata = False
            Else
                fonte_non_collegata = True
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error fonte_non_collegata: " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Function


    Protected Sub btnMemorizzaFontiStazioni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMemorizzaFontiStazioni.Click

        Dim possibile_salvare As Boolean = False
        Dim messaggio As String = ""

        Dim strSql As String = ""

        Try


            'SE VIENE INDICATO DI VOLER SPECIFICARE UNA LISTA DI FONTI/STAZIONI E' NECESSARIO SPECIFICARNE ALMENO UNO PER POTER SALVARE
            If radioSel1.SelectedValue = "0" Then
                For i = 0 To listTipoClientiSelezionati.Items.Count - 1
                    possibile_salvare = True
                Next

                If Not possibile_salvare Then
                    messaggio = "Specificare almeno una tipologia di cliente."
                End If
            End If

            possibile_salvare = False

            If radioSel2.SelectedValue = "0" Then
                For i = 0 To listStazioniPickSelezionate.Items.Count - 1
                    possibile_salvare = True
                Next


                If Not possibile_salvare Then
                    messaggio = messaggio & " " & "Specificare almeno una stazione di pick up."
                End If
            End If

            possibile_salvare = False

            If radioSel3.SelectedValue = "0" Then
                For i = 0 To listStazioniDropSelezionate.Items.Count - 1
                    possibile_salvare = True
                Next

                If Not possibile_salvare Then
                    messaggio = messaggio & " " & "Specificare almeno una stazione di drop off."
                End If
            End If

            If messaggio = "" Then

                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()


                'FONTI : CANCELLO TUTTO E RICREO SE E' STATO SELEZIONATO UN GRUPPI DI STAZIONI - NEL CASO DI TUTTE LE FONTI SELEZIONATE NON
                'SALVO NIENTE


                strSql = "DELETE FROM tariffe_x_fonti WHERE id_tariffa = '" & idTariffa.Text & "'"
                Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

                Dim cond_delete As String = "DELETE FROM tariffe_x_fonti WHERE id_tariffa = '" & idTariffa.Text & "' "

                If radioSel1.SelectedValue = "0" Then
                    For i = 0 To listTipoClientiSelezionati.Items.Count - 1
                        cond_delete = cond_delete & " AND id_tipologia_cliente<>'" & listTipoClientiSelezionati.Items(i).Value & "'"
                        'INSERISCO SOLO SE GIA' NON ESISTENTE (DEVO CONTROLLARE PER NON SOVRASCRIVERE NOME TARIFFA E VALIDO DA)
                        If fonte_non_collegata(idTariffa.Text, listTipoClientiSelezionati.Items(i).Value) Then
                            strSql = "INSERT INTO tariffe_x_fonti (id_tariffa, id_tipologia_cliente) VALUES ('" & idTariffa.Text & "','" & listTipoClientiSelezionati.Items(i).Value & "')"
                            Cmd = New Data.SqlClient.SqlCommand(strSql, Dbc)
                            Cmd.ExecuteNonQuery()
                        End If
                    Next
                    'CANCELLO LE EVENTUALI FONTI NON PIU' COLLEGATE
                    Cmd = New Data.SqlClient.SqlCommand(cond_delete, Dbc)
                    Cmd.ExecuteNonQuery()
                Else
                    strSql = "DELETE FROM tariffe_x_fonti WHERE id_tariffa = '" & idTariffa.Text & "'"
                    Cmd = New Data.SqlClient.SqlCommand(strSql, Dbc)
                    Cmd.ExecuteNonQuery()
                End If

                'strSql = "DELETE FROM tariffe_x_fonti WHERE id_tariffa = '" & idTariffa.Text & "'"
                'Dim Cmd As New Data.SqlClient.SqlCommand(strSql, Dbc)
                'Cmd.ExecuteNonQuery()

                'If radioSel1.SelectedValue = "0" Then
                '    For i = 0 To listTipoClientiSelezionati.Items.Count - 1
                '        strSql = "INSERT INTO tariffe_x_fonti (id_tariffa, id_tipologia_cliente) VALUES ('" & idTariffa.Text & "','" & listTipoClientiSelezionati.Items(i).Value & "')"
                '        Cmd = New Data.SqlClient.SqlCommand(strSql, Dbc)
                '        Cmd.ExecuteNonQuery()
                '    Next
                'End If

                'STAZIONI PICK UP : CANCELLO TUTTO E RICREO SE E' STATO SELEZIONATO UN GRUPPI DI STAZIONI - NEL CASO DI TUTTE LE STAZIONI SELEZIONATE NON
                'SALVO NIENTE
                strSql = "DELETE FROM tariffe_X_stazioni WHERE id_tariffa = '" & idTariffa.Text & "' AND tipo='PICK'"
                Cmd = New Data.SqlClient.SqlCommand(strSql, Dbc)
                Cmd.ExecuteNonQuery()

                If radioSel2.SelectedValue = "0" Then
                    For i = 0 To listStazioniPickSelezionate.Items.Count - 1
                        strSql = "INSERT INTO tariffe_X_stazioni (id_tariffa, id_stazione,tipo) VALUES ('" & idTariffa.Text & "','" & listStazioniPickSelezionate.Items(i).Value & "','PICK')"
                        Cmd = New Data.SqlClient.SqlCommand(strSql, Dbc)
                        Cmd.ExecuteNonQuery()
                    Next
                End If

                'STAZIONI DROP OFF: CANCELLO TUTTO E RICREO SE E' STATO SELEZIONATO UN GRUPPI DI STAZIONI - NEL CASO DI TUTTE LE STAZIONI SELEZIONATE NON
                'SALVO NIENTE
                strSql = "DELETE FROM tariffe_X_stazioni WHERE id_tariffa = '" & idTariffa.Text & "' AND tipo='DROP'"
                Cmd = New Data.SqlClient.SqlCommand(strSql, Dbc)
                Cmd.ExecuteNonQuery()

                If radioSel3.SelectedValue = "0" Then
                    For i = 0 To listStazioniDropSelezionate.Items.Count - 1
                        strSql = "INSERT INTO tariffe_X_stazioni (id_tariffa, id_stazione,tipo) VALUES ('" & idTariffa.Text & "','" & listStazioniDropSelezionate.Items(i).Value & "','DROP')"
                        Cmd = New Data.SqlClient.SqlCommand(strSql, Dbc)
                        Cmd.ExecuteNonQuery()
                    Next
                End If


                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                Libreria.genUserMsgBox(Me, "Selezioni memorizzate correttamente.")


                listRegoleXFonti.DataBind()
            Else
                Libreria.genUserMsgBox(Me, messaggio)
            End If

        Catch ex As Exception
            Response.Write("error btnMemorizzaFontiStazioni_Click: " & ex.Message & "<br/>" & strSql & "<br/>")
        End Try



    End Sub

    Protected Sub PassaUnoClienti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassaUnoClienti.Click
        Dim k As Integer = 0
        Dim j As Integer = listTipoClientiSelezionati.Items.Count

        For i = 0 To listTipoClienti.Items.Count() - 1
            If listTipoClienti.Items(i).Selected Then
                listTipoClientiSelezionati.Items.Add(listTipoClienti.Items(i).Text)
                listTipoClientiSelezionati.Items(j).Value = listTipoClienti.Items(i).Value
                j = j + 1

                k = k + 1
            End If
        Next

        For i = 1 To k
            listTipoClienti.Items.Remove(listTipoClienti.SelectedItem)
        Next

        funzioni_comuni.SortListBox(listTipoClientiSelezionati)
    End Sub

    Protected Sub TornaUnoClienti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TornaUnoClienti.Click
        Dim k As Integer = 0
        Dim j As Integer = listTipoClienti.Items.Count

        For i = 0 To listTipoClientiSelezionati.Items.Count() - 1
            If listTipoClientiSelezionati.Items(i).Selected Then
                listTipoClienti.Items.Add(listTipoClientiSelezionati.Items(i).Text)
                listTipoClienti.Items(j).Value = listTipoClientiSelezionati.Items(i).Value
                j = j + 1

                k = k + 1
            End If
        Next

        For i = 1 To k
            listTipoClientiSelezionati.Items.Remove(listTipoClientiSelezionati.SelectedItem)
        Next

        funzioni_comuni.SortListBox(listTipoClienti)
    End Sub

    Protected Sub TornaTuttoClienti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TornaTuttoClienti.Click
        listTipoClienti.Items.Clear()
        listTipoClientiSelezionati.Items.Clear()

        listTipoClienti.DataBind()
        funzioni_comuni.SortListBox(listTipoClienti)
    End Sub

    Protected Sub PassaTuttoClienti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassaTuttoClienti.Click
        Dim j As Integer = listTipoClientiSelezionati.Items.Count

        For i = 0 To listTipoClienti.Items.Count() - 1
            listTipoClientiSelezionati.Items.Add(listTipoClienti.Items(i).Text)
            listTipoClientiSelezionati.Items(j).Value = listTipoClienti.Items(i).Value
            j = j + 1
        Next

        listTipoClienti.Items.Clear()
        funzioni_comuni.SortListBox(listTipoClientiSelezionati)
    End Sub

    Protected Sub PassaUnoPick_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassaUnoPick.Click
        Dim k As Integer = 0
        Dim j As Integer = listStazioniPickSelezionate.Items.Count

        For i = 0 To listStazioniPick.Items.Count() - 1
            If listStazioniPick.Items(i).Selected Then
                listStazioniPickSelezionate.Items.Add(listStazioniPick.Items(i).Text)
                listStazioniPickSelezionate.Items(j).Value = listStazioniPick.Items(i).Value
                j = j + 1

                k = k + 1
            End If
        Next

        For i = 1 To k
            listStazioniPick.Items.Remove(listStazioniPick.SelectedItem)
        Next

        funzioni_comuni.SortListBox(listStazioniPickSelezionate)
    End Sub

    Protected Sub TornaUnoPick_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TornaUnoPick.Click
        Dim k As Integer = 0
        Dim j As Integer = listStazioniPick.Items.Count

        For i = 0 To listStazioniPickSelezionate.Items.Count() - 1
            If listStazioniPickSelezionate.Items(i).Selected Then
                listStazioniPick.Items.Add(listStazioniPickSelezionate.Items(i).Text)
                listStazioniPick.Items(j).Value = listStazioniPickSelezionate.Items(i).Value
                j = j + 1

                k = k + 1
            End If
        Next

        For i = 1 To k
            listStazioniPickSelezionate.Items.Remove(listStazioniPickSelezionate.SelectedItem)
        Next

        funzioni_comuni.SortListBox(listStazioniPick)
    End Sub

    Protected Sub PassaTuttiPick_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassaTuttiPick.Click
        Dim j As Integer = listStazioniPickSelezionate.Items.Count

        For i = 0 To listStazioniPick.Items.Count() - 1
            listStazioniPickSelezionate.Items.Add(listStazioniPick.Items(i).Text)
            listStazioniPickSelezionate.Items(j).Value = listStazioniPick.Items(i).Value
            j = j + 1
        Next

        listStazioniPick.Items.Clear()
        funzioni_comuni.SortListBox(listStazioniPickSelezionate)
    End Sub

    Protected Sub TornaTuttiPick_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TornaTuttiPick.Click
        listStazioniPick.Items.Clear()
        listStazioniPickSelezionate.Items.Clear()

        listStazioniPick.DataBind()
        funzioni_comuni.SortListBox(listStazioniPick)
    End Sub

    Protected Sub PassaUnoDrop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassaUnoDrop.Click
        Dim k As Integer = 0
        Dim j As Integer = listStazioniDropSelezionate.Items.Count

        For i = 0 To listStazioniDrop.Items.Count() - 1
            If listStazioniDrop.Items(i).Selected Then
                listStazioniDropSelezionate.Items.Add(listStazioniDrop.Items(i).Text)
                listStazioniDropSelezionate.Items(j).Value = listStazioniDrop.Items(i).Value
                j = j + 1

                k = k + 1
            End If
        Next

        For i = 1 To k
            listStazioniDrop.Items.Remove(listStazioniDrop.SelectedItem)
        Next

        funzioni_comuni.SortListBox(listStazioniDropSelezionate)
    End Sub

    Protected Sub TornaUnoDrop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TornaUnoDrop.Click
        Dim k As Integer = 0
        Dim j As Integer = listStazioniDrop.Items.Count

        For i = 0 To listStazioniDropSelezionate.Items.Count() - 1
            If listStazioniDropSelezionate.Items(i).Selected Then
                listStazioniDrop.Items.Add(listStazioniDropSelezionate.Items(i).Text)
                listStazioniDrop.Items(j).Value = listStazioniDropSelezionate.Items(i).Value
                j = j + 1

                k = k + 1
            End If
        Next

        For i = 1 To k
            listStazioniDropSelezionate.Items.Remove(listStazioniDropSelezionate.SelectedItem)
        Next

        funzioni_comuni.SortListBox(listStazioniDrop)
    End Sub

    Protected Sub PassaTuttiDrop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassaTuttiDrop.Click
        Dim j As Integer = listStazioniDropSelezionate.Items.Count

        For i = 0 To listStazioniDrop.Items.Count() - 1
            listStazioniDropSelezionate.Items.Add(listStazioniDrop.Items(i).Text)
            listStazioniDropSelezionate.Items(j).Value = listStazioniDrop.Items(i).Value
            j = j + 1
        Next

        listStazioniDrop.Items.Clear()
        funzioni_comuni.SortListBox(listStazioniDropSelezionate)
    End Sub

    Protected Sub TornaTuttiDrop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TornaTuttiDrop.Click

        Try
            listStazioniDrop.Items.Clear()
            listStazioniDropSelezionate.Items.Clear()

            listStazioniDrop.DataBind()
            funzioni_comuni.SortListBox(listStazioniDrop)
        Catch ex As Exception
            Response.Write("error TornaTuttiDrop_Click : " & ex.Message & "<br/>")

        End Try



    End Sub

    Protected Sub btnSalvaIntestazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvaIntestazione.Click


        Dim t As String = Replace(txtNomeTariffa.Text, "'", "''")
        t = Trim(t)

        Dim ct As String = Replace(txtCodtar.Text, "'", "''")
        ct = Trim(ct)

        Dim sqlstr As String = "UPDATE tariffe SET codice='" & t & "', codtar='" & ct & "', max_sconto='" & Replace(txtMaxSconto.Text, ",", ".") & "', max_sconto_rack='" & Replace(txtMaxScontoRack.Text, ",", ".") & "',  "
        sqlstr += "is_broker_prepaid='" & dropBroker.SelectedValue & "',  is_web='" & DropDownListWeb.SelectedValue & "',  is_web_prepagato='" & DropDownListWebPrepagata.SelectedValue & "', codicepromozionale ='" & txtCodicePromozionale.Text & "' WHERE id='" & idTariffa.Text & "'"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Cmd.ExecuteNonQuery()

            Libreria.genUserMsgBox(Me, "Tariffa modificata correttamente")

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error btnSalvaIntestazione_Click : " & ex.Message & "<br/>")

        End Try

    End Sub

    Protected Sub listRigheTariffa_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles listRigheTariffa.ItemCommand
        Dim idLabel As Label = e.Item.FindControl("idLabel")
        Try
            If e.CommandName = "vediDettaglioTariffa" Then

                Dim id_tempo_kmLabel As Label = e.Item.FindControl("id_tempo_kmLabel")
                Dim lbl_id_condizione As Label = e.Item.FindControl("lbl_id_condizione")
                Dim vendibilita_daLabel As Label = e.Item.FindControl("vendibilita_daLabel")
                Dim vendibilita_aLabel As Label = e.Item.FindControl("vendibilita_aLabel")
                Dim pickup_daLabel As Label = e.Item.FindControl("pickup_daLabel")
                Dim pickup_aLabel As Label = e.Item.FindControl("pickup_aLabel")
                Dim idTariffaMadre As Label = e.Item.FindControl("idTariffaMadre")
                Dim Lb_minuti_di_ritardo As Label = e.Item.FindControl("Lb_minuti_di_ritardo")
                Dim Lb_tolleranza_rientro_nolo As Label = e.Item.FindControl("Lb_tolleranza_rientro_nolo")
                Dim lblMaxData As Label = e.Item.FindControl("lblMaxData")
                Dim Lb_condizione_madre As Label = e.Item.FindControl("Lb_condizione_madre")

                Dim id_tariffa_rack As Label = e.Item.FindControl("id_tariffa_rack")

                Dim min_giorni_nolo As Label = e.Item.FindControl("min_giorni_nolo")
                Dim max_giorni_nolo As Label = e.Item.FindControl("max_giorni_nolo")

                'aggiunto salvo 17.01.2023 si tratta del valore dello sconto sulla riga di tariffe_righe
                Dim max_sconto As Label = e.Item.FindControl("max_sconto_t")

                Dim id_TempoKM As Label = e.Item.FindControl("id_tempo_kmLabel")



                btnInserisci.Text = "Modifica"
                btnAnnullaModifica.Visible = True
                idTariffaVisualizzata.Text = idLabel.Text

                If filtra.Text = "1" Then
                    dropTempoKmFiltrato.Visible = False
                    dropTempoKm.Visible = True
                    dropCondizioniFiltrato.Visible = False
                    dropCondizioni.Visible = True
                    'dropTempoKmRackFiltrato.Visible = False
                    'dropTempoKmRack.Visible = True
                    dropCondizioniMadreFiltrato.Visible = False
                    dropCondizioneMadre.Visible = True
                End If

                btnFiltra.Enabled = False

                dropTempoKm.SelectedValue = id_tempo_kmLabel.Text
                dropTempoKm.Enabled = False
                dropCondizioni.SelectedValue = lbl_id_condizione.Text
                dropCondizioni.Enabled = False

                If Lb_condizione_madre.Text <> "" Then
                    dropCondizioneMadre.SelectedValue = idTariffaMadre.Text
                Else
                    dropCondizioneMadre.SelectedValue = "0"
                End If

                dropCondizioneMadre.Enabled = False

                If id_tariffa_rack.Text <> "" Then
                    dropTariffaRack.SelectedValue = id_tariffa_rack.Text
                Else
                    dropTariffaRack.SelectedValue = "0"
                End If

                dropTariffaRack.Enabled = False

                vendibilitaDa.Text = vendibilita_daLabel.Text
                vendibilitaA.Text = vendibilita_aLabel.Text
                pickUpDa.Text = pickup_daLabel.Text
                lbl_pickup_da.Text = pickup_daLabel.Text 'aggiunto da salvo per verifica modifica data pickup 08.02.2023


                pickUpA.Text = pickup_aLabel.Text
                txtMaxDataRilascio.Text = lblMaxData.Text
                txtMinutiDiRitardo.Text = Lb_minuti_di_ritardo.Text
                txtUlterioreTolleranza.Text = Lb_tolleranza_rientro_nolo.Text

                '# aggiunto salvo 17.01.2023 
                'tolto perchè deve stare a NULL
                'If Trim(min_giorni_nolo.Text) = "" Then
                '    min_giorni_nolo.Text = "0"
                'End If
                'If Trim(max_giorni_nolo.Text) = "" Then
                '    max_giorni_nolo.Text = "0"
                'End If
                '@ aggiunto salvo 17.01.2023 

                txtMinGiorniNolo.Text = min_giorni_nolo.Text
                txtMaxGiorniNolo.Text = max_giorni_nolo.Text

                txt_max_sconto.Text = max_sconto.Text       'aggiunto salvo 2
                If Trim(max_sconto.Text) = "" Then
                    max_sconto.Text = "0"
                End If


                'aggiunto 18.01.2023 salvo
                txt_id_tempokm.Text = id_TempoKM.Text




                'txtMinGiorniNolo.Enabled = False
                'txtMaxGiorniNolo.Enabled = False
                'txtMinutiDiRitardo.Enabled = False

            ElseIf e.CommandName = "eliminaDettaglioTariffa" Then

                eliminaTariffa(idLabel.Text)

            ElseIf e.CommandName = "stampa" Then

                Dim id_tariffe_righe As Label = e.Item.FindControl("idLabel")
                Dim id_tempo_km As Label = e.Item.FindControl("id_tempo_kmLabel")
                Dim id_condizione As Label = e.Item.FindControl("lbl_id_condizione")
                Dim id_condizione_madre As Label = e.Item.FindControl("idTariffaMadre")


                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    Session("url_print") = "stampe/stampa_tariffa.aspx?pagina=verticale&id_tariffe_righe=" & id_tariffe_righe.Text & "&id_cond=" & id_condizione.Text & "&id_tempo_km=" & id_tempo_km.Text & "&id_cond_madre=" & id_condizione_madre.Text
                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write("error listRigheTariffa_ItemCommand : " & ex.Message & "<br/>")
        End Try


    End Sub

    Private Sub eliminaTariffa(ByVal idTariffa As String)

        Dim sqlStr As String
        sqlStr = "DELETE FROM tariffe_righe WHERE id = '" & idTariffa & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dbc.Open()

            Try
                Cmd.ExecuteNonQuery()
                listRigheTariffa.DataBind()
                Libreria.genUserMsgBox(Me, "Tariffa cancella correttamente")
            Catch ex As Exception
                Libreria.genUserMsgBox(Me, "Errore nella cancellazione della tariffa: " & idTariffa)
            End Try

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error eliminaTariffa : " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Sub

    Protected Sub btnAnnullaModifica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnullaModifica.Click
        pulisciCampi()
    End Sub

    Private Sub pulisciCampi()
        btnAnnullaModifica.Visible = False
        btnInserisci.Text = "Inserisci"
        idTariffaVisualizzata.Text = "0"

        dropTempoKm.SelectedValue = "0"
        dropTempoKmFiltrato.SelectedValue = "0"
        dropTempoKm.Enabled = True
        dropTempoKmFiltrato.Enabled = True
        dropCondizioni.SelectedValue = "0"
        dropCondizioniFiltrato.SelectedValue = "0"
        dropCondizioni.Enabled = True
        dropCondizioniFiltrato.Enabled = True
        dropCondizioneMadre.SelectedValue = "0"
        dropCondizioniMadreFiltrato.SelectedValue = "0"
        dropCondizioneMadre.Enabled = True
        dropCondizioniMadreFiltrato.Enabled = True
        'dropTempoKmRack.Enabled = True
        dropTariffaRack.Enabled = True
        dropTempoKmFiltrato.Enabled = True
        dropTariffaRack.Enabled = True
        'dropTempoKmRack.SelectedValue = "0"
        'dropTempoKmRackFiltrato.SelectedValue = "0"



        txtMinGiorniNolo.Enabled = True
        txtMaxGiorniNolo.Enabled = True
        txtMinutiDiRitardo.Enabled = True
        txtUlterioreTolleranza.Enabled = True

        If filtra.Text = "1" Then
            dropTempoKmFiltrato.Visible = True
            dropTempoKm.Visible = False
            dropCondizioniFiltrato.Visible = True
            dropCondizioni.Visible = False
            'dropTempoKmRackFiltrato.Visible = True
            'dropTempoKmRack.Visible = False
            dropCondizioniMadreFiltrato.Visible = True
            dropCondizioneMadre.Visible = False
        ElseIf filtra.Text = "0" Then
            dropTempoKmFiltrato.Visible = False
            dropTempoKm.Visible = True
            dropCondizioniFiltrato.Visible = False
            dropCondizioni.Visible = True
            'dropTempoKmRackFiltrato.Visible = False
            'dropTempoKmRack.Visible = True
            dropCondizioniMadreFiltrato.Visible = False
            dropCondizioneMadre.Visible = True
        End If

        btnFiltra.Enabled = True

        vendibilitaDa.Text = ""
        vendibilitaA.Text = ""
        pickUpDa.Text = ""
        lbl_pickup_da.Text = ""       'aggiunto salvo 08.02.2023
        lbl_list_contratti.Text = ""  'aggiunto salvo 08.02.2023
        lbl_modifica_admin.Visible = False
        ck_modifica_admin.Checked = False
        ck_modifica_admin.Visible = False

        pickUpA.Text = ""
        txtMaxDataRilascio.Text = ""
        txtMinutiDiRitardo.Text = ""
        txtUlterioreTolleranza.Text = ""
        txtMinGiorniNolo.Text = ""
        txtMaxGiorniNolo.Text = ""
        txtMaxSconto.Text = ""

        txt_max_sconto.Text = "" 'aggiunto salvo 18.01.2023
        txt_id_tempokm.Text = "" 'aggiunto salvo 18.01.2023



    End Sub

    Protected Sub btnFiltra_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltra.Click
        If filtra.Text = "1" Then
            rimuovi_filtri()

            dropTempoKm.Items.Clear()
            dropCondizioni.Items.Clear()
            dropCondizioneMadre.Items.Clear()

            dropTempoKm.Items.Add("Seleziona...")
            dropTempoKm.Items(0).Value = "0"
            dropTempoKm.DataBind()

            dropCondizioni.Items.Add("Seleziona...")
            dropCondizioni.Items(0).Value = "0"
            dropCondizioni.DataBind()

            dropCondizioneMadre.Items.Add("Seleziona...")
            dropCondizioneMadre.Items(0).Value = "0"
            dropCondizioneMadre.DataBind()

            'dropTempoKmRack.DataBind()

            Try
                dropTempoKm.SelectedValue = dropTempoKmFiltrato.SelectedValue
            Catch ex As Exception
                dropTempoKm.SelectedValue = "0"
            End Try

            Try
                dropCondizioni.SelectedValue = dropCondizioniFiltrato.SelectedValue
            Catch ex As Exception
                dropCondizioni.SelectedValue = "0"
            End Try

            Try
                dropCondizioneMadre.SelectedValue = dropCondizioniMadreFiltrato.SelectedValue
            Catch ex As Exception
                dropCondizioneMadre.SelectedValue = "0"
            End Try

            'Try
            '    dropTempoKmRack.SelectedValue = dropTempoKmRackFiltrato.SelectedValue
            'Catch ex As Exception
            '    dropTempoKmRack.SelectedValue = "0"
            'End Try

            dropTempoKmFiltrato.SelectedValue = "0"
            dropCondizioniFiltrato.SelectedValue = "0"
            dropCondizioniMadreFiltrato.SelectedValue = "0"
            'dropTempoKmRackFiltrato.SelectedValue = "0"
        ElseIf filtra.Text = "0" Then
            imposta_filtri()

            dropTempoKm.Items.Clear()
            dropCondizioni.Items.Clear()
            dropCondizioneMadre.Items.Clear()

            dropTempoKm.Items.Add("Seleziona...")
            dropTempoKm.Items(0).Value = "0"
            dropTempoKm.DataBind()

            dropCondizioni.Items.Add("Seleziona...")
            dropCondizioni.Items(0).Value = "0"
            dropCondizioni.DataBind()

            dropCondizioneMadre.Items.Add("Seleziona...")
            dropCondizioneMadre.Items(0).Value = "0"
            dropCondizioneMadre.DataBind()

            Try
                dropTempoKmFiltrato.SelectedValue = dropTempoKm.SelectedValue
            Catch ex As Exception
                dropTempoKmFiltrato.SelectedValue = "0"
            End Try

            Try
                dropCondizioniFiltrato.SelectedValue = dropCondizioni.SelectedValue
            Catch ex As Exception
                dropCondizioniFiltrato.SelectedValue = "0"
            End Try

            Try
                dropCondizioniMadreFiltrato.SelectedValue = dropCondizioneMadre.SelectedValue
            Catch ex As Exception
                dropCondizioniMadreFiltrato.SelectedValue = "0"
            End Try

            'Try
            '    dropTempoKmRackFiltrato.SelectedValue = dropTempoKmRack.SelectedValue
            'Catch ex As Exception
            '    dropTempoKmRackFiltrato.SelectedValue = "0"
            'End Try

            dropTempoKm.SelectedValue = "0"
            dropCondizioni.SelectedValue = "0"
            dropCondizioneMadre.SelectedValue = "0"
            'dropTempoKmRack.SelectedValue = "0"
        End If
    End Sub



    Protected Sub btnMemorizzaImpostazioniFonti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMemorizzaImpostazioniFonti.Click

        Dim sqlStr As String

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
            Dbc.Open()


            Dim nome_tariffa As TextBox
            Dim valido_da As TextBox
            Dim idLabel As Label

            For i = 0 To listRegoleXFonti.Items.Count - 1
                sqlStr = "UPDATE tariffe_x_fonti SET "

                nome_tariffa = listRegoleXFonti.Items(i).FindControl("nome_tariffa")
                valido_da = listRegoleXFonti.Items(i).FindControl("valido_da")
                idLabel = listRegoleXFonti.Items(i).FindControl("idLabel")

                If Trim(nome_tariffa.Text) <> "" Then
                    sqlStr = sqlStr & " nome_tariffa='" & Replace(nome_tariffa.Text, "'", "''") & "'"
                Else
                    sqlStr = sqlStr & " nome_tariffa=NULL"
                End If

                If valido_da.Text <> "" Then
                    Try
                        sqlStr = sqlStr & ",valido_da='" & funzioni_comuni.getDataDb_senza_orario(valido_da.Text) & "'"
                    Catch ex As Exception
                        sqlStr = sqlStr & ",valido_da=NULL"
                    End Try
                Else
                    sqlStr = sqlStr & ",valido_da=NULL"
                End If

                sqlStr = sqlStr & " WHERE id='" & idLabel.Text & "'"

                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteNonQuery()
            Next

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            Libreria.genUserMsgBox(Me, "Salvataggio effettuato correttamente.")
        Catch ex As Exception
            Response.Write("error btnMemorizzaImpostazioniFonti_Click : " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try



    End Sub

    Protected Function condizioneWhere() As String
        Dim condizione As String = ""

        If txtCodice.Text <> "" Then
            condizione = condizione & " AND codice LIKE '" & Replace(txtCodice.Text, "'", "''") & "%'"
        End If

        If txtCodiceBreve.Text <> "" Then
            condizione = condizione & " AND CODTAR LIKE '" & Replace(txtCodiceBreve.Text, "'", "''") & "%'"
        End If

        If cercaVendibilita.Text <> "" Then

            Dim data1 As String = funzioni_comuni.GetDataSql(cercaVendibilita.Text, 0) ' funzioni_comuni.getDataDb_senza_orario(cercaVendibilita.Text)

            condizione = condizione & " AND '" & data1 & "' BETWEEN vendibilita_da AND vendibilita_a "
        End If

        If cercaPickup.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(cercaPickup.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(cercaPickup.Text)

            condizione = condizione & " AND '" & data1 & "' BETWEEN pickup_Da AND pickup_a "
        End If

        condizioneWhere = condizione
    End Function

    Protected Sub cerca()

        sqlTariffe.SelectCommand = "SELECT id, data_creazione, codice, CODTAR, max_sconto, max_sconto_rack, is_broker_prepaid,is_web,is_web_prepagato, codicepromozionale " &
            "FROM tariffe WITH(NOLOCK) WHERE attiva='1'  " & condizioneWhere()

        'sqlTariffe.SelectCommand = "SELECT tariffe.id, tariffe.data_creazione, tariffe.codice,tariffe.CODTAR, tariffe.max_sconto, tariffe.max_sconto_rack," & _
        '    " tariffe.is_broker_prepaid, CONVERT(Char(10),tariffe_righe.vendibilita_da,103) As vendibilita_da, CONVERT(Char(10),tariffe_righe.vendibilita_a,103) As vendibilita_a, CONVERT(Char(10),tariffe_righe.pickup_Da,103) As pickup_Da, CONVERT(Char(10),tariffe_righe.pickup_a,103) As pickup_a " & _
        '    " FROM tariffe WITH(NOLOCK) INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa WHERE attiva='1' " & condizioneWhere()

        lblQuery.Text = sqlTariffe.SelectCommand
        sqlTariffe.SelectCommand = lblQuery.Text & " ORDER BY codice ASC"
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        cerca()
    End Sub


    Protected Function VerificaPickUpTariffe(data_pickup_da As String, id_tempokm As String, id_tariffa As String) As String
        Dim ris As String = ""

        Dim sqlstr As String = "" '"SELECT contratti.num_contratto, contratti.CODTAR, contratti.data_uscita, tariffe_righe.id_tempo_km, tariffe_righe.vendibilita_da, tariffe_righe.vendibilita_a, tariffe_righe.pickup_da, tariffe_righe.pickup_a "
        'sqlstr += "FROM contratti INNER JOIN tariffe ON contratti.CODTAR = tariffe.codice INNER JOIN "
        'sqlstr += "tariffe_righe ON tariffe.id = tariffe_righe.id_tariffa "
        'sqlstr += "WHERE  (contratti.status = '2') AND (contratti.attivo = 1) and convert(datetime,'" & data_pickup_da & "',102) between pickup_da and pickup_a "
        'sqlstr += "and id_tempo_km='" & id_tempokm & "' order by pickup_da "


        sqlstr = "Select contratti.num_contratto, contratti.CODTAR, contratti.data_uscita, tariffe_righe.id_tempo_km, contratti.id_tariffa "
        sqlstr += "From contratti LEFT OUTER Join tariffe_righe On contratti.id_tariffe_righe = tariffe_righe.id LEFT OUTER Join "
        sqlstr += "tariffe On contratti.CODTAR = tariffe.codice "
        sqlstr += "Where (contratti.status = '2') AND (contratti.attivo = 1) AND (tariffe.codice LIKE 'Web banco PSA') "
        sqlstr += "And contratti.data_uscita < Convert(DateTime, '" & data_pickup_da & "', 102) "
        sqlstr += "And tariffe_righe.id_tempo_km ='" & id_tempokm & "' and contratti.id_tariffa='" & id_tariffa & "' ORDER BY contratti.data_uscita DESC"


        Try

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows Then
                While Rs.Read
                    If ris = "" Then
                        ris = "<a href='contratti.aspx?nr=" & Rs!num_contratto & "' target=_blank>" & Rs!num_contratto & " (" & Rs!data_uscita & ")</a>"
                    Else
                        ris += ", <a href='contratti.aspx?nr=" & Rs!num_contratto & "' target=_blank>" & Rs!num_contratto & " (" & Rs!data_uscita & ")</a>"
                    End If
                End While
            End If


            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception

            genUserMsgBox(Page, "Errore VerificaPickUpTariffe: " & ex.Message & " - sql: " & sqlstr)

        End Try

        Return ris

    End Function


End Class
