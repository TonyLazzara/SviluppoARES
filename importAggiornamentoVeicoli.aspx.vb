
Imports System.IO
Imports funzioni_comuni

Partial Class importAggiornamentoVeicoli
    Inherits System.Web.UI.Page
    Dim funzioni As New funzioni_comuni

    Public Sub CancellaTabAppoggioVeicoli()
        Dim DbcCancellaInTabAppoggio As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
        DbcCancellaInTabAppoggio.Open()
        Dim CmdCancellaInTabAppoggio As New Data.SqlClient.SqlCommand("", DbcCancellaInTabAppoggio)

        CmdCancellaInTabAppoggio.CommandText = "delete from veicoli_appoggio "

        CmdCancellaInTabAppoggio.ExecuteNonQuery()
        CmdCancellaInTabAppoggio.Dispose()
        DbcCancellaInTabAppoggio.Close()
        DbcCancellaInTabAppoggio = Nothing
    End Sub

    Public Function CaricaFoglioExcelAcquistoInDB(ByVal RigaStringa As String) As Boolean
        Dim RigaStrApp(1) As String
        Dim presente_targa As Char
        Dim presente_telaio As Char
        Dim presente_modello As Char
        Dim presente_colore As Char
        Dim presente_proprietario As Char

        Dim id_modello As String
        Dim id_colore As String
        Dim id_proprietario As String

        Dim id_veicolo As String = ""
        Dim test As String

        Dim possibile_salvare As Boolean = True

        RigaStrApp = Split(RigaStringa, ";")
        'For i = 0 To 9
        'Response.Write(RigaStrApp(i) & "<br>")
        'Next

        'Controllo esistenza targa
        Dim Dbc As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

        If Trim(RigaStrApp(0)) <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT id, targa, inserito_da_foglio_excel FROM veicoli WITH(NOLOCK) where targa='" & Replace(RigaStrApp(0), "'", "''") & "'", Dbc)

            'Controllo esistenza TARGA (SIAMO IN UPDATE QUINDI DEVE ESISTERE)
            Dim Rs_targa As Data.SqlClient.SqlDataReader
            Rs_targa = Cmd.ExecuteReader()

            If Rs_targa.HasRows() Then
                Rs_targa.Read()

                id_veicolo = Rs_targa("id")
                presente_targa = "1"
            Else
                presente_targa = "0"
                possibile_salvare = False
            End If

            Rs_targa.Close()
            Rs_targa = Nothing

            Dbc.Close()
            Dbc.Open()
        Else
            'IL CAMPO E' OBBLIGATORIO
            presente_targa = "0"
            possibile_salvare = False
        End If

        'Controllo esistenza TELAIO - SE SI DEVE ESEGUIRE UN UPDATE CONTROLLO TRANNE CHE PER LA TARGA ATTUALE
        Dim telaio As String

        If Trim(RigaStrApp(1)) <> "" Then
            telaio = "'" & RigaStrApp(1).Trim & "'"

            Cmd = New Data.SqlClient.SqlCommand("SELECT telaio FROM veicoli WITH(NOLOCK) WHERE telaio='" & Replace(RigaStrApp(1), "'", "''") & "' AND targa<>'" & RigaStrApp(0) & "'", Dbc)

            test = Cmd.ExecuteScalar & ""

            If test <> "" Then
                'TELAIO GIA' ESISTENTE - IMPOSSIBILE SALVARE
                presente_telaio = "1"
                possibile_salvare = False
            Else
                presente_telaio = "0"
            End If
        Else
            telaio = "NULL"
            presente_telaio = "0"
        End If

        'Controllo esistenza MODELLO
        If Trim(RigaStrApp(2)) <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT id_modello FROM modelli WITH(NOLOCK) WHERE descrizione='" & Replace(RigaStrApp(2), "'", "''") & "'", Dbc)

            id_modello = Cmd.ExecuteScalar & ""

            If id_modello <> "" Then
                presente_modello = "1"
                id_modello = "'" & id_modello & "'"
            Else
                presente_modello = "0"
                id_modello = "NULL"
                possibile_salvare = False
            End If
        Else
            'CAMPO OBBLIGAROIO
            presente_modello = "0"
            id_modello = "NULL"
        End If

        'Controllo esistenza COLORE
        If Trim(RigaStrApp(3)) <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM colori WITH(NOLOCK) WHERE descrizione='" & Replace(RigaStrApp(3), "'", "''") & "'", Dbc)

            id_colore = Cmd.ExecuteScalar & ""

            If id_colore <> "" Then
                presente_colore = "1"
                id_colore = "'" & id_colore & "'"
            Else
                presente_colore = "0"
                id_colore = "NULL"
                possibile_salvare = False
            End If
        Else
            'CAMPO OBBLIGATORIO
            presente_colore = "0"
            id_colore = "NULL"
        End If

        'Controllo esistenza proprietario
        If Trim(RigaStrApp(5)) <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM proprietari_veicoli WITH(NOLOCK) WHERE descrizione='" & Replace(RigaStrApp(5), "'", "''") & "'", Dbc)

            id_proprietario = Cmd.ExecuteScalar

            If id_proprietario <> "" Then
                presente_proprietario = "1"
                id_proprietario = "'" & id_proprietario & "'"
            Else
                presente_proprietario = "0"
                id_proprietario = "NULL"
                possibile_salvare = False
            End If
        Else
            'CAMPO OBBLIGATORIO
            presente_proprietario = "0"
            id_proprietario = "NULL"
        End If


        Dim dataVal As String

        If RigaStrApp(4) <> "" Then
            Try
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario(RigaStrApp(4))
                dataVal = "'" & RigaStrApp(4) & "'"
            Catch ex As Exception
                dataVal = "'" & RigaStrApp(4) & "'"
                possibile_salvare = False
            End Try
        Else
            dataVal = "NULL"
        End If

        Dim EscludiDaAmmortamento As String

        If UCase(RigaStrApp(6)) = "SI" Then 'Escludi da ammortamento 
            EscludiDaAmmortamento = "SI"
        ElseIf UCase(RigaStrApp(6)) = "NO" Then
            EscludiDaAmmortamento = "NO"
        ElseIf Trim(UCase(RigaStrApp(6)) = "") Then
            EscludiDaAmmortamento = " "
        Else
            EscludiDaAmmortamento = Replace(RigaStrApp(6), "'", "''")
            possibile_salvare = False
        End If

        'SEZIONE LEASING
        'DATA INIZIO LEASING - DATA FINE LEASING
        Dim leasing_almeno_un_dato_valorizzatoSBC As Boolean = False
        Dim leasing_almeno_un_dato_valorizzatoLeasys As Boolean = False

        Dim data_inizio_leasing As String
        Dim data_fine_leasing As String
        Dim data1 As DateTime = Nothing
        Dim data2 As DateTime = Nothing
        Dim data_finale_precedente As Boolean = False

        If RigaStrApp(7) <> "" Then
            leasing_almeno_un_dato_valorizzatoSBC = True
            Try
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario(RigaStrApp(7))
                data1 = funzioni_comuni.getDataDb_senza_orario(RigaStrApp(7))
                data_inizio_leasing = "'" & RigaStrApp(7) & "'"
            Catch ex As Exception
                data_inizio_leasing = "'" & Replace(RigaStrApp(7), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            data_inizio_leasing = "NULL"
        End If

        If RigaStrApp(8) <> "" Then
            leasing_almeno_un_dato_valorizzatoSBC = True
            Try
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario(RigaStrApp(8))
                data2 = funzioni_comuni.getDataDb_senza_orario(RigaStrApp(8))
                data_fine_leasing = "'" & RigaStrApp(8) & "'"
            Catch ex As Exception
                data_fine_leasing = "'" & Replace(RigaStrApp(8), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            data_fine_leasing = "NULL"
        End If

        'SE A QUESTO PUNTO SONO STATI SPECIFICATI AMBEDUE I DATI ED E' POSSIBILE SALVARE (OVVERO SONO DUE DATE CORRETTE) CONTROLLO SE LA DATA
        'FINE E' PRECEDENTE O SUCCESSIVA ALLA DATA INIZIO
        If (Not (data1 = Nothing) And Not (data2 = Nothing)) Then
            If DateDiff(DateInterval.Day, data1, data2) <= 0 Then
                possibile_salvare = False
                data_finale_precedente = True
            End If
        End If

        'CANONE LEASING -------
        Dim canone_mensile As String

        If RigaStrApp(9) <> "" Then
            leasing_almeno_un_dato_valorizzatoLeasys = True
            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(9))

                canone_mensile = "'" & RigaStrApp(9) & "'"

                If test_dbl < 0 Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                canone_mensile = "'" & Replace(RigaStrApp(9), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            canone_mensile = "NULL"
        End If
        'DURATA IN MESI ---------------------
        Dim durata_mesi As String

        If RigaStrApp(10) <> "" Then
            leasing_almeno_un_dato_valorizzatoSBC = True
            Try
                Dim test_int As Integer = CInt(RigaStrApp(10))

                durata_mesi = "'" & RigaStrApp(10) & "'"

                If test_int <= 0 Or RigaStrApp(10).Contains(",") Or RigaStrApp(10).Contains(".") Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                durata_mesi = "'" & Replace(RigaStrApp(10), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            durata_mesi = "NULL"
        End If
        'ENTE FINANZIATORE-------------------------------------------
        Dim ente_finanziatore As String
        Dim id_ente_finanziatore As String

        If Trim(RigaStrApp(11)) <> "" Then
            leasing_almeno_un_dato_valorizzatoSBC = True
            ente_finanziatore = "'" & Replace(RigaStrApp(11), "'", "''") & "'"

            Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM enti_finanziatori WITH(NOLOCK) WHERE nome='" & Replace(RigaStrApp(11), "'", "''") & "'", Dbc)
            id_ente_finanziatore = Cmd.ExecuteScalar & ""

            If id_ente_finanziatore <> "" Then
                id_ente_finanziatore = "'" & id_ente_finanziatore & "'"
            Else
                id_ente_finanziatore = "NULL"
                possibile_salvare = False
            End If
        Else
            'CAMPO OBBLIGATORIO
            id_ente_finanziatore = "NULL"
            ente_finanziatore = "NULL"
        End If
        'KM INCLUSI -----------------------
        Dim km_inclusi As String

        If RigaStrApp(12) <> "" Then
            leasing_almeno_un_dato_valorizzatoLeasys = True
            Try
                Dim test_int As Integer = CInt(RigaStrApp(12))

                km_inclusi = "'" & RigaStrApp(12) & "'"

                If test_int < 0 Or RigaStrApp(12).Contains(",") Or RigaStrApp(12).Contains(".") Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                km_inclusi = "'" & Replace(RigaStrApp(12), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            km_inclusi = "NULL"
        End If

        'FRANCHIGIA KM COMPRESI -------
        Dim franchigia_km_compresi As String

        If RigaStrApp(13) <> "" Then
            leasing_almeno_un_dato_valorizzatoLeasys = True
            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(13))
                franchigia_km_compresi = "'" & RigaStrApp(13) & "'"

                If test_dbl < 0 Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                franchigia_km_compresi = "'" & Replace(RigaStrApp(13), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            franchigia_km_compresi = "NULL"
        End If

        'ADDIZZIONALE 100 KM -------
        Dim addizionale_100_km As String

        If RigaStrApp(14) <> "" Then
            leasing_almeno_un_dato_valorizzatoLeasys = True
            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(14))

                addizionale_100_km = "'" & RigaStrApp(14) & "'"

                If test_dbl < 0 Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                addizionale_100_km = "'" & Replace(RigaStrApp(14), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            addizionale_100_km = "NULL"
        End If

        'RIMBORSO 100 KM -------
        Dim rimborso_100_km As String

        If RigaStrApp(15) <> "" Then
            leasing_almeno_un_dato_valorizzatoLeasys = True
            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(15))

                rimborso_100_km = "'" & RigaStrApp(15) & "'"

                If test_dbl < 0 Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                rimborso_100_km = "'" & Replace(RigaStrApp(15), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            rimborso_100_km = "NULL"
        End If

        Dim leasys As String
        Dim SBC As String

        If leasing_almeno_un_dato_valorizzatoLeasys Then
            leasys = "'1'"
            SBC = "'0'"
        ElseIf leasing_almeno_un_dato_valorizzatoSBC Then
            leasys = "'0'"
            SBC = "'1'"
        Else
            leasys = "'0'"
            SBC = "'0'"
        End If

        'FATTURE--------------------------------------------------------------------------------------------------------------------------
        'I FATTURA------------
        Dim numero_fattura_1 As String
        Dim data_fattura_1 As String
        Dim importo_fattura_1 As String

        Dim update_fattura_1 As String

        If RigaStrApp(16).Trim = "" And RigaStrApp(17).Trim = "" And RigaStrApp(18).Trim = "" Then
            numero_fattura_1 = "NULL"
            data_fattura_1 = "NULL"
            importo_fattura_1 = "NULL"
            update_fattura_1 = "NULL"
        ElseIf RigaStrApp(16).Trim <> "" And RigaStrApp(17).Trim <> "" And RigaStrApp(18).Trim <> "" Then
            numero_fattura_1 = "'" & RigaStrApp(16).Trim.Replace("'", "''") & "'"

            Try
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario(RigaStrApp(17))
                data_fattura_1 = "'" & RigaStrApp(17) & "'"
            Catch ex As Exception
                data_fattura_1 = "'" & RigaStrApp(17) & "'"
                possibile_salvare = False
            End Try

            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(18))

                importo_fattura_1 = "'" & RigaStrApp(18) & "'"

                If test_dbl < 0 Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                importo_fattura_1 = "'" & Replace(RigaStrApp(18), "'", "''") & "'"
                possibile_salvare = False
            End Try
            'CONTROLLO SE E' UNA FATTURA GIA' ESISTENTE OPPURE SE E' UNA NUOVA FATTURA (SE LA TARGA ESISTE ED EVITO DI CONTROLLARE SE GIA'
            'SO CHE NON E' POSSIBILE SALVARE LA RIGA
            If id_veicolo <> "" And possibile_salvare Then
                Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM fatture_acquisto_veicoli WITH(NOLOCK) WHERE id_veicolo='" & id_veicolo & "' AND tipo='FATTURA' AND num_acquisto=" & numero_fattura_1, Dbc)

                test = Cmd.ExecuteScalar & ""

                If test = "" Then
                    update_fattura_1 = "'0'"
                Else
                    update_fattura_1 = "'1'"
                End If
            Else
                update_fattura_1 = "NULL"
            End If
        Else
            numero_fattura_1 = "'" & RigaStrApp(16).Trim.Replace("'", "''") & "'"
            data_fattura_1 = "'" & RigaStrApp(17).Trim.Replace("'", "''") & "'"
            importo_fattura_1 = "'" & RigaStrApp(18).Trim.Replace("'", "''") & "'"
            possibile_salvare = False

            update_fattura_1 = "NULL"
        End If
        'II FATTURA--------------------------------------------------------------------------------------------------------------------
        Dim numero_fattura_2 As String
        Dim data_fattura_2 As String
        Dim importo_fattura_2 As String
        Dim update_fattura_2 As String

        If RigaStrApp(19).Trim = "" And RigaStrApp(20).Trim = "" And RigaStrApp(21).Trim = "" Then
            numero_fattura_2 = "NULL"
            data_fattura_2 = "NULL"
            importo_fattura_2 = "NULL"
            update_fattura_2 = "NULL"
        ElseIf RigaStrApp(19).Trim <> "" And RigaStrApp(20).Trim <> "" And RigaStrApp(21).Trim <> "" Then
            numero_fattura_2 = "'" & RigaStrApp(19).Trim.Replace("'", "''") & "'"

            Try
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario(RigaStrApp(20))
                data_fattura_2 = "'" & RigaStrApp(20) & "'"
            Catch ex As Exception
                data_fattura_2 = "'" & RigaStrApp(20) & "'"
                possibile_salvare = False
            End Try

            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(21))

                importo_fattura_2 = "'" & RigaStrApp(21) & "'"

                If test_dbl < 0 Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                importo_fattura_2 = "'" & Replace(RigaStrApp(21), "'", "''") & "'"
                possibile_salvare = False
            End Try
            'CONTROLLO SE E' UNA FATTURA GIA' ESISTENTE OPPURE SE E' UNA NUOVA FATTURA (SE LA TARGA ESISTE ED EVITO DI CONTROLLARE SE GIA'
            'SO CHE NON E' POSSIBILE SALVARE LA RIGA
            If id_veicolo <> "" And possibile_salvare Then
                Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM fatture_acquisto_veicoli WITH(NOLOCK) WHERE id_veicolo='" & id_veicolo & "' AND tipo='FATTURA' AND num_acquisto=" & numero_fattura_2, Dbc)

                test = Cmd.ExecuteScalar & ""

                If test = "" Then
                    update_fattura_2 = "'0'"
                Else
                    update_fattura_2 = "'1'"
                End If
            Else
                update_fattura_2 = "NULL"
            End If
        Else
            numero_fattura_2 = "'" & RigaStrApp(19).Trim.Replace("'", "''") & "'"
            data_fattura_2 = "'" & RigaStrApp(20).Trim.Replace("'", "''") & "'"
            importo_fattura_2 = "'" & RigaStrApp(21).Trim.Replace("'", "''") & "'"
            possibile_salvare = False

            update_fattura_2 = "NULL"
        End If
        'I NOTA DI CREDIO
        Dim numero_nc_1 As String
        Dim data_nc_1 As String
        Dim importo_nc_1 As String
        Dim update_nc_1 As String

        If RigaStrApp(22).Trim = "" And RigaStrApp(23).Trim = "" And RigaStrApp(24).Trim = "" Then
            numero_nc_1 = "NULL"
            data_nc_1 = "NULL"
            importo_nc_1 = "NULL"
            update_nc_1 = "NULL"
        ElseIf RigaStrApp(22).Trim <> "" And RigaStrApp(23).Trim <> "" And RigaStrApp(24).Trim <> "" Then
            numero_nc_1 = "'" & RigaStrApp(22).Trim.Replace("'", "''") & "'"

            Try
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario(RigaStrApp(23))
                data_nc_1 = "'" & RigaStrApp(23) & "'"
            Catch ex As Exception
                data_nc_1 = "'" & RigaStrApp(23) & "'"
                possibile_salvare = False
            End Try

            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(24))

                importo_nc_1 = "'" & RigaStrApp(24) & "'"

                If test_dbl < 0 Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                importo_nc_1 = "'" & Replace(RigaStrApp(24), "'", "''") & "'"
                possibile_salvare = False
            End Try

            'CONTROLLO SE E' UNA FATTURA GIA' ESISTENTE OPPURE SE E' UNA NUOVA FATTURA (SE LA TARGA ESISTE ED EVITO DI CONTROLLARE SE GIA'
            'SO CHE NON E' POSSIBILE SALVARE LA RIGA
            If id_veicolo <> "" And possibile_salvare Then
                Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM fatture_acquisto_veicoli WITH(NOLOCK) WHERE id_veicolo='" & id_veicolo & "' AND tipo='NOTA' AND num_acquisto=" & numero_nc_1, Dbc)

                test = Cmd.ExecuteScalar & ""

                If test = "" Then
                    update_nc_1 = "'0'"
                Else
                    update_nc_1 = "'1'"
                End If
            Else
                update_nc_1 = "NULL"
            End If
        Else
            numero_nc_1 = "'" & RigaStrApp(22).Trim.Replace("'", "''") & "'"
            data_nc_1 = "'" & RigaStrApp(23).Trim.Replace("'", "''") & "'"
            importo_nc_1 = "'" & RigaStrApp(24).Trim.Replace("'", "''") & "'"
            possibile_salvare = False

            update_nc_1 = "NULL"
        End If

        Dim riga_ok As String
        If possibile_salvare Then
            riga_ok = "'1'"
        Else
            riga_ok = "'0'"
        End If

        Cmd.CommandText = "insert into veicoli_appoggio (targa,presente_targa,telaio,presente_telaio,modello,presente_modello,id_modello,colore,presente_colore,id_colore,data_immatricolazione,proprietario,presente_proprietario,id_proprietario,escludi_ammortamento, data_inizio_leasing, data_fine_leasing,data_finale_precedente, canone_mensile, mesi_leasing, ente_finanziatore, id_ente_finanziatore, km_compresi, franchigia_km_compresi, addizionale_100_extra, rimborso_100_extra, is_leasing, is_leasing_sbc,numero_prima_fattura,data_prima_fattura,importo_prima_fattura,update_fattura_1, numero_seconda_fattura, data_seconda_fattura, importo_seconda_fattura, update_fattura_2, numero_prima_nc,data_prima_nc,importo_prima_nc, update_nc_1, riga_ok) " & _
                                                  "values('" & Replace(RigaStrApp(0), "'", "''") & "','" & presente_targa & "','" & Replace(RigaStrApp(1), "'", "''") & "','" & presente_telaio & "','" & Replace(RigaStrApp(2), "'", "''") & "','" & presente_modello & "'," & id_modello & ",'" & Replace(RigaStrApp(3), "'", "''") & "','" & presente_colore & "'," & id_colore & "," & dataVal & ",'" & Replace(RigaStrApp(5), "'", "''") & "','" & presente_proprietario & "'," & id_proprietario & ",'" & EscludiDaAmmortamento & "'," & _
                                                  data_inizio_leasing & "," & data_fine_leasing & ",'" & data_finale_precedente & "'," & _
                                                  canone_mensile & "," & durata_mesi & "," & ente_finanziatore & "," & id_ente_finanziatore & "," & _
                                                  km_inclusi & "," & franchigia_km_compresi & "," & addizionale_100_km & "," & rimborso_100_km & "," & _
                                                  leasys & "," & SBC & "," & _
                                                  numero_fattura_1 & "," & data_fattura_1 & "," & importo_fattura_1 & "," & update_fattura_1 & "," & _
                                                  numero_fattura_2 & "," & data_fattura_2 & "," & importo_fattura_2 & "," & update_fattura_2 & "," & _
                                                  numero_nc_1 & "," & data_nc_1 & "," & importo_nc_1 & "," & update_nc_1 & "," & _
                                                  riga_ok & ")"

        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        CaricaFoglioExcelAcquistoInDB = possibile_salvare
        LblImportFile.Text = "CaricaFile"
    End Function

    Public Function CaricaRigaInDBVendita(ByVal RigaStringa As String) As Boolean
        'RESTITUISCE SE LA RIGA E' CORRETTA
        Dim RigaStrApp(1) As String

        Dim id_veicolo As String
        Dim veicolo_venduto As String

        Dim veicolo_is_leasing As Boolean

        RigaStrApp = Split(RigaStringa, ";")

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim riga_ok As Boolean = True
        Dim test As String
        'CONTROLLI SULLA TARGA--------------------------------------------------------------------------------------------------------
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM veicoli WITH(NOLOCK) where targa='" & Replace(Trim(RigaStrApp(0)), "'", "''") & "'", Dbc)
        id_veicolo = Cmd.ExecuteScalar

        If id_veicolo = "" Then
            id_veicolo = "NULL"
            veicolo_venduto = "NULL"

            riga_ok = False
        Else
            id_veicolo = "'" & id_veicolo & "'"
        End If

        'CONTROLLO SE IL VEICOLO E' GIA' VENDUTO
        If id_veicolo <> "NULL" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT venduta FROM veicoli WITH(NOLOCK) where id=" & id_veicolo & "", Dbc)
            veicolo_venduto = "'" & Cmd.ExecuteScalar & "'"

            If veicolo_venduto = "'False'" Then
                riga_ok = False
            End If
        End If
        '------------------------------------------------------------------------------------------------------------------------------
        'CONTROLLO SULL'ESISTENZA DEL NUMERO DI BOLLA ---------------------------------------------------------------------------------
        Dim numero_bolla As String
        If Trim(RigaStrApp(1)) <> "" Then
            numero_bolla = "'" & Replace(RigaStrApp(1), "'", "''") & "'"
        Else
            numero_bolla = "NULL"
        End If
        '------------------------------------------------------------------------------------------------------------------------------
        'DATA DDT - DEVE ESSERE UNA DATA VALIDA ---------------------------------------------------------------------------------------
        Dim data_ddt As String
        If Trim(RigaStrApp(2)) = "" Then
            data_ddt = "NULL"
        Else
            data_ddt = "'" & Replace(Trim(RigaStrApp(2)), "'", "''") & "'"

            Try
                test = funzioni_comuni.getDataDb_senza_orario(Trim(RigaStrApp(2)))
            Catch ex As Exception
                riga_ok = False
            End Try
        End If
        '------------------------------------------------------------------------------------------------------------------------------
        'SE IL VEICOLO E' STATO TROVATO CONTROLLO SE L'AUTO E' O NON E' IN LEASING ---------------------------------------------------------
        If id_veicolo <> "NULL" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM veicoli where id=" & id_veicolo & " AND (ISNULL(auto_in_leasing,'False')='True' OR ISNULL(auto_in_leasing_SBC,'False')='True') ", Dbc)

            test = Cmd.ExecuteScalar

            If test <> "" Then
                veicolo_is_leasing = True
            Else
                veicolo_is_leasing = False
            End If
        Else
            veicolo_is_leasing = False
        End If
        '----------------------------------------------------------------------------------------------------------------------------------
        'DATA ATTO DI VENDITA - DEVE ESSERE UNA DATA VALIDA -------------------------------------------------------------------------------
        Dim data_atto_vendita As String
        If Trim(RigaStrApp(4)) = "" Then
            data_atto_vendita = "NULL"
        Else
            'PER PRIMA COSA CONTROLLO LA CORRETTEZZA DEL DATO - SE E' CORRETTO NEL CASO DI LEASING CONTROLLO SE E' UGUALE ALLA DATA DDT
            data_atto_vendita = "'" & Replace(Trim(RigaStrApp(4)), "'", "''") & "'"
            Try
                test = funzioni_comuni.getDataDb_senza_orario(Trim(RigaStrApp(4)))
            Catch ex As Exception
                riga_ok = False
            End Try
        End If
        '----------------------------------------------------------------------------------------------------------------------------------
        'VENDITORE (DA NON VALORIZZARE PER LEASING)--------------------------------------------------------------
        Dim venditore As String
        Dim id_venditore As String
        If veicolo_is_leasing And Trim(RigaStrApp(5)) <> "" Then
            riga_ok = False

            id_venditore = "NULL"
            venditore = "'" & Replace(Trim(RigaStrApp(5)), "'", "''") & "'"
        ElseIf Trim(RigaStrApp(5)) = "" Then
            id_venditore = "NULL"
            venditore = "NULL"
        Else
            'NON E' LEASING E C'E' UN DATO - CONTROLLO CHE CORRISPONDE AD UN VENDITORE SALVATO
            venditore = "'" & Replace(Trim(RigaStrApp(5)), "'", "''") & "'"
            Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM venditori WITH(NOLOCK) WHERE nome='" & Replace(Trim(RigaStrApp(5)), "'", "''") & "'", Dbc)
            id_venditore = Cmd.ExecuteScalar & ""
            If id_venditore = "" Then
                id_venditore = "NULL"

                riga_ok = False
            Else
                id_venditore = "'" & id_venditore & "'"
            End If
        End If
        '----------------------------------------------------------------------------------------------------------------------------------
        'PRESSO_DI ------------------------------------------------------------------------------------------------------------------------
        Dim presso_di As String
        Dim id_presso_di As String

        If Trim(RigaStrApp(3)) = "" Then
            id_presso_di = "NULL"
            presso_di = "NULL"
        Else
            'C'E' UN DATO - CONTROLLO CHE CORRISPONDE AD UN VENDITORE SALVATO
            presso_di = "'" & Replace(Trim(RigaStrApp(3)), "'", "''") & "'"
            Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM acquirenti_veicoli WITH(NOLOCK) WHERE rag_soc='" & Replace(Trim(RigaStrApp(3)), "'", "''") & "'", Dbc)
            id_presso_di = Cmd.ExecuteScalar & ""
            If id_presso_di = "" Then
                id_presso_di = "NULL"

                riga_ok = False
            Else
                id_presso_di = "'" & id_presso_di & "'"
            End If
        End If
        'ACQUIRENTE ------------------------------------------------------------------------------------------------------------------------
        Dim acquirente As String
        Dim id_acquirente As String

        If Trim(RigaStrApp(6)) = "" Then
            id_acquirente = "NULL"
            acquirente = "NULL"
        Else
            'C'E' UN DATO - CONTROLLO CHE CORRISPONDE AD UN VENDITORE SALVATO
            acquirente = "'" & Replace(Trim(RigaStrApp(6)), "'", "''") & "'"
            Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM acquirenti_veicoli WITH(NOLOCK) WHERE rag_soc='" & Replace(Trim(RigaStrApp(6)), "'", "''") & "'", Dbc)
            id_acquirente = Cmd.ExecuteScalar & ""
            If id_acquirente = "" Then
                id_acquirente = "NULL"

                riga_ok = False
            Else
                id_acquirente = "'" & id_acquirente & "'"
            End If
        End If
        'FATTURE--------------------------------------------------------------------------------------------------------------------------
        'I FATTURA------------
        Dim numero_fattura_1 As String
        Dim data_fattura_1 As String
        Dim importo_fattura_1 As String

        Dim update_fattura_1 As String

        If RigaStrApp(9).Trim = "" And RigaStrApp(10).Trim = "" And RigaStrApp(11).Trim = "" Then
            numero_fattura_1 = "NULL"
            data_fattura_1 = "NULL"
            importo_fattura_1 = "NULL"
            update_fattura_1 = "NULL"
        ElseIf RigaStrApp(9).Trim <> "" And RigaStrApp(10).Trim <> "" And RigaStrApp(11).Trim <> "" Then
            numero_fattura_1 = "'" & RigaStrApp(9).Trim.Replace("'", "''") & "'"

            Try
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario(RigaStrApp(10))
                data_fattura_1 = "'" & RigaStrApp(10) & "'"
            Catch ex As Exception
                data_fattura_1 = "'" & RigaStrApp(10) & "'"
                riga_ok = False
            End Try

            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(11))

                importo_fattura_1 = "'" & RigaStrApp(11) & "'"

                If test_dbl < 0 Then
                    riga_ok = False
                End If
            Catch ex As Exception
                importo_fattura_1 = "'" & Replace(RigaStrApp(11), "'", "''") & "'"
                riga_ok = False
            End Try
            'CONTROLLO SE E' UNA FATTURA GIA' ESISTENTE OPPURE SE E' UNA NUOVA FATTURA (SE LA TARGA ESISTE ED EVITO DI CONTROLLARE SE GIA'
            'SO CHE NON E' POSSIBILE SALVARE LA RIGA
            If id_veicolo <> "NULL" And riga_ok Then
                Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM fatture_vendita_veicoli WITH(NOLOCK) WHERE id_veicolo=" & id_veicolo & " AND tipo='FATTURA' AND num_vendita=" & numero_fattura_1, Dbc)

                test = Cmd.ExecuteScalar & ""

                If test = "" Then
                    update_fattura_1 = "'0'"
                Else
                    update_fattura_1 = "'1'"
                End If
            Else
                update_fattura_1 = "NULL"
            End If
        Else
            numero_fattura_1 = "'" & RigaStrApp(9).Trim.Replace("'", "''") & "'"
            data_fattura_1 = "'" & RigaStrApp(10).Trim.Replace("'", "''") & "'"
            importo_fattura_1 = "'" & RigaStrApp(11).Trim.Replace("'", "''") & "'"
            update_fattura_1 = "NULL"
            riga_ok = False
        End If
        'II FATTURA------------
        Dim numero_fattura_2 As String
        Dim data_fattura_2 As String
        Dim importo_fattura_2 As String

        Dim update_fattura_2 As String

        If RigaStrApp(12).Trim = "" And RigaStrApp(13).Trim = "" And RigaStrApp(14).Trim = "" Then
            numero_fattura_2 = "NULL"
            data_fattura_2 = "NULL"
            importo_fattura_2 = "NULL"
            update_fattura_2 = "NULL"
        ElseIf RigaStrApp(12).Trim <> "" And RigaStrApp(13).Trim <> "" And RigaStrApp(14).Trim <> "" Then
            numero_fattura_2 = "'" & RigaStrApp(12).Trim.Replace("'", "''") & "'"

            Try
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario(RigaStrApp(13))
                data_fattura_2 = "'" & RigaStrApp(13) & "'"
            Catch ex As Exception
                data_fattura_2 = "'" & RigaStrApp(13) & "'"
                riga_ok = False
            End Try

            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(14))

                importo_fattura_2 = "'" & RigaStrApp(14) & "'"

                If test_dbl < 0 Then
                    riga_ok = False
                End If
            Catch ex As Exception
                importo_fattura_2 = "'" & Replace(RigaStrApp(14), "'", "''") & "'"
                riga_ok = False
            End Try
            'CONTROLLO SE E' UNA FATTURA GIA' ESISTENTE OPPURE SE E' UNA NUOVA FATTURA (SE LA TARGA ESISTE ED EVITO DI CONTROLLARE SE GIA'
            'SO CHE NON E' POSSIBILE SALVARE LA RIGA
            If id_veicolo <> "NULL" And riga_ok Then
                Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM fatture_vendita_veicoli WITH(NOLOCK) WHERE id_veicolo=" & id_veicolo & " AND tipo='FATTURA' AND num_vendita=" & numero_fattura_2, Dbc)

                test = Cmd.ExecuteScalar & ""

                If test = "" Then
                    update_fattura_2 = "'0'"
                Else
                    update_fattura_2 = "'1'"
                End If
            Else
                update_fattura_2 = "NULL"
            End If
        Else
            numero_fattura_2 = "'" & RigaStrApp(12).Trim.Replace("'", "''") & "'"
            data_fattura_2 = "'" & RigaStrApp(13).Trim.Replace("'", "''") & "'"
            importo_fattura_2 = "'" & RigaStrApp(14).Trim.Replace("'", "''") & "'"
            update_fattura_2 = "NULL"
            riga_ok = False
        End If
        'I NOTA DI CREDIO
        Dim numero_nc_1 As String
        Dim data_nc_1 As String
        Dim importo_nc_1 As String

        Dim update_nc_1 As String

        If RigaStrApp(15).Trim = "" And RigaStrApp(16).Trim = "" And RigaStrApp(17).Trim = "" Then
            numero_nc_1 = "NULL"
            data_nc_1 = "NULL"
            importo_nc_1 = "NULL"
            update_nc_1 = "NULL"
        ElseIf RigaStrApp(15).Trim <> "" And RigaStrApp(16).Trim <> "" And RigaStrApp(17).Trim <> "" Then
            numero_nc_1 = "'" & RigaStrApp(15).Trim.Replace("'", "''") & "'"

            Try
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario(RigaStrApp(16))
                data_nc_1 = "'" & RigaStrApp(16) & "'"
            Catch ex As Exception
                data_nc_1 = "'" & RigaStrApp(16) & "'"
                riga_ok = False
            End Try

            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(17))

                importo_nc_1 = "'" & RigaStrApp(17) & "'"

                If test_dbl < 0 Then
                    riga_ok = False
                End If
            Catch ex As Exception
                importo_nc_1 = "'" & Replace(RigaStrApp(17), "'", "''") & "'"
                riga_ok = False
            End Try
            'CONTROLLO SE E' UNA FATTURA GIA' ESISTENTE OPPURE SE E' UNA NUOVA FATTURA (SE LA TARGA ESISTE ED EVITO DI CONTROLLARE SE GIA'
            'SO CHE NON E' POSSIBILE SALVARE LA RIGA
            If id_veicolo <> "NULL" And riga_ok Then
                Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM fatture_vendita_veicoli WITH(NOLOCK) WHERE id_veicolo=" & id_veicolo & " AND tipo='NOTA' AND num_vendita=" & numero_nc_1, Dbc)

                test = Cmd.ExecuteScalar & ""

                If test = "" Then
                    update_nc_1 = "'0'"
                Else
                    update_nc_1 = "'1'"
                End If
            Else
                update_nc_1 = "NULL"
            End If
        Else
            numero_nc_1 = "'" & RigaStrApp(15).Trim.Replace("'", "''") & "'"
            data_nc_1 = "'" & RigaStrApp(16).Trim.Replace("'", "''") & "'"
            importo_nc_1 = "'" & RigaStrApp(17).Trim.Replace("'", "''") & "'"
            update_nc_1 = "NULL"
            riga_ok = False
        End If
        '---------------------------------------------------------------------------------------------------------------------------------

        Dim sqlStr As String = "INSERT INTO dismissioni_appoggio (targa,id_veicolo,veicolo_venduto, DDT, data_ddt, veicolo_is_leasing, data_atto_di_vendita, venditore, id_venditore, presso_di, id_presso_di, acquirente, id_acquirente, numero_prima_fattura, data_prima_fattura, importo_prima_fattura, update_fattura_1, numero_seconda_fattura,data_seconda_fattura,importo_seconda_fattura, update_fattura_2, numero_prima_nc, data_prima_nc, importo_prima_nc, update_nc_1, note, id_utente) VALUES " &
         "('" & Replace(RigaStrApp(0), "'", "''") & "'," & id_veicolo & "," & veicolo_venduto & "," & numero_bolla & "," & data_ddt & ",'" & veicolo_is_leasing & "'," &
         data_atto_vendita & "," & venditore & "," & id_venditore & "," &
         presso_di & "," & id_presso_di & "," &
         acquirente & "," & id_acquirente & "," & numero_fattura_1 & "," & data_fattura_1 & "," & importo_fattura_1 & "," & update_fattura_1 & "," &
         numero_fattura_2 & "," & data_fattura_2 & "," & importo_fattura_2 & "," & update_fattura_2 & "," &
         numero_nc_1 & "," & data_nc_1 & "," & importo_nc_1 & "," & update_nc_1 & "," &
         "'" & Replace(RigaStrApp(8), "'", "''") & "'," &
         "'" & Request.Cookies("SicilyRentCar")("idUtente") & "')"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteScalar()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()

        CaricaRigaInDBVendita = riga_ok
        LblImportFile.Text = "CaricaFile"
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.Cookies("SicilyRentCar") Is Nothing Then
                Response.Redirect("LogIn.aspx")
            Else
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 20) = "3" Then
                    'AzzeraTab()
                    'PanelDatiGenerali.Visible = True
                    If Request.QueryString("ritorno") = "true" Then
                        LblImportFile.Text = "CaricaFile"
                        Dim nome_file As String = Session("nome_file")
                        Session("nome_file") = ""
                        CaricaFoglioExcelAcquistoInDB(nome_file)
                    End If
                Else
                    Response.Redirect("default.aspx")
                End If
            End If
        End If
    End Sub

    Protected Sub CaricaFoglioExcelAcquisto(Optional ByVal file_da_caricare As String = "")
        CancellaTabAppoggioVeicoli()

        Dim filePath As String
        filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\docs"

        Dim fileName As String

        If file_da_caricare = "" Then
            fileName = FileUpload2.FileName
        End If

        Dim fileTesto As String = "ParcoVeicoli_" & DatePart("d", Now) & DatePart("M", Now) & DatePart("yyyy", Now) & "_" & DatePart("h", Now) & DatePart("n", Now) & DatePart("s", Now) & ".csv"
        Dim fs = CreateObject("Scripting.FileSystemObject")
        Dim filetxt = fs.CreateTextFile(filePath & "\" & fileTesto, True) 'CREA IL FILE DI TESTO
        Dim FileTestoValorizzato As Boolean = False

        Dim CampiObbligatoriKO As Integer = 0
        Dim CampoTargaPresente As Integer = 0
        Dim CampoTelaioPresente As Integer = 0
        Dim CampoModelloNonPresente As Integer = 0
        Dim CampoColoreNonPresente As Integer = 0
        Dim EscludiAmmortamento As Char = ""

        Dim test As String

        Dim CampoProprietarioNonPresente As Integer = 0

        'Inserito Prima Riga        
        filetxt.writeline("TARGA;TELAIO;MODELLO;COLORE;IMMATRICOLAZIONE;IMPORTO_FATTURA;NUMERO_FATTURA;DATA_FATTURA;PROPRIETARIO;ESCLUDI_DA_AMMORTAMENTO")

        'Response.Write("A " & FileTestoValorizzato & "<br><br>")
        If FileUpload2.HasFile Or file_da_caricare <> "" Then
            'Response.Write("IN")
            'Response.End()
            If FileUpload2.FileName = "parco_da_inserire.csv" Or file_da_caricare <> "" Then
                'Get a StreamReader class that can be used to read the file 'Prendi una classe StreamReader che può essere utilizzata per leggere il file
                Dim objStreamReader As StreamReader

                If FileUpload2.HasFile Then
                    If File.Exists(filePath & "\" & fileName) Then
                        'Response.Write("IN")
                        'Response.End()
                        File.Delete(filePath & "\" & fileName)
                    End If
                    FileUpload2.SaveAs(filePath & "\" & fileName)
                    'Aprire un file per la lettura        
                    objStreamReader = File.OpenText(filePath & "\" & fileName)

                    nome_file.Text = filePath & "\" & fileName
                Else
                    'Aprire un file per la lettura        
                    objStreamReader = File.OpenText(file_da_caricare)
                    nome_file.Text = file_da_caricare
                End If

                Dim Riga As String
                Dim RigaStr(1) As String

                Dim possibile_salvare As Boolean

                'Prima Riga
                Riga = objStreamReader.ReadLine()
                While (objStreamReader.Peek() <> -1)
                    Riga = objStreamReader.ReadLine()
                    RigaStr = Split(Riga, ";")

                    possibile_salvare = CaricaFoglioExcelAcquistoInDB(Riga)

                    If Not possibile_salvare Then
                        FileTestoValorizzato = True
                    End If
                End While

                objStreamReader.Close()

                filetxt.close()
                fs = Nothing

                If FileTestoValorizzato Then
                    Libreria.genUserMsgBox(Me, "Dati non caricati sul Data Base (Verificare gli errori)")
                Else
                    Dim DbcSalvataggio As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
                    DbcSalvataggio.Open()
                    Dim CmdSalvataggio As New Data.SqlClient.SqlCommand("", DbcSalvataggio)

                    Dim Dbc As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
                    Dbc.Open()
                    Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM veicoli_appoggio WITH(NOLOCK)", Dbc)
                    'Response.Write(Cmd.CommandText & "<br><br>")
                    Dim Rs As Data.SqlClient.SqlDataReader
                    Rs = Cmd.ExecuteReader()

                    Dim auto_in_leasing As String
                    Dim auto_in_leasingSBC As String
                    Dim venduta_da_fattura As String
                    Dim totale_fatture As Double

                    Dim sqlStr As String
                    Dim id_veicolo As String

                    Dim cond As String

                    Dim Adata_inizio_leasing(3) As String
                    Dim data_inizio_leasing As String

                    Dim Adata_fine_leasing(3) As String
                    Dim data_fine_leasing As String

                    Dim Adata_immatricolazione(3) As String
                    Dim data_immatricolazione As String

                    Dim Aodierna(3) As String
                    Dim odierna As String


                    Do While Rs.Read
                        'Dati Generale Veicolo
                        cond = ""



                        If UCase(Rs("escludi_ammortamento")) = "SI" Then
                            cond = cond & " , escludi_ammortamento='1'"
                        ElseIf UCase(Rs("escludi_ammortamento")) = "NO" Then
                            cond = cond & " , escludi_ammortamento='0'"
                        End If

                        If Rs("is_leasing") Then
                            'E' L'UNICO CASO IN CUI POSSO FARE UN UPDATE PER I LEASING E' IL CASO IN CUI SONO SICURO CHE E' UN LEASYS
                            cond = cond & ", auto_in_leasing='1', auto_in_leasing_SBC='0'"
                        End If

                        If (Rs("id_modello") & "") <> "" Then
                            cond = cond & ", id_modello='" & Rs("id_modello") & "'"
                        End If

                        If (Rs("telaio") & "") <> "" Then
                            cond = cond & ", telaio='" & Replace(Rs("telaio"), ",", ".") & "'"
                        End If

                        If (Rs("id_colore") & "") <> "" Then
                            cond = cond & ", id_colore='" & Rs("id_colore") & "'"
                        End If

                        If (Rs("id_proprietario") & "") <> "" Then
                            cond = cond & ", id_proprietario='" & Rs("id_proprietario") & "'"
                        End If
                        '-------

                        If (Rs("canone_mensile") & "") <> "" Then
                            cond = cond & ", canone_mensile_leasing='" & Replace(Rs("canone_mensile"), ",", ".") & "'"
                        End If

                        If (Rs("mesi_leasing") & "") <> "" Then                            
                            cond = cond & ", durata_mesi_leasing='" & Replace(Rs("mesi_leasing"), ",", ".") & "'"
                        End If

                        If (Rs("id_ente_finanziatore") & "") <> "" Then
                            cond = cond & ", id_ente_finanziatore='" & Rs("id_ente_finanziatore") & "'"
                        End If

                        If (Rs("km_compresi") & "") <> "" Then
                            cond = cond & ", km_compresi_leasing='" & Replace(Rs("km_compresi"), ",", ".") & "'"
                        End If

                        If (Rs("franchigia_km_compresi") & "") <> "" Then
                            cond = cond & ", franchigia_km_compresi_leasing='" & Replace(Rs("franchigia_km_compresi"), ",", ".") & "'"
                        End If

                        If (Rs("addizionale_100_extra") & "") <> "" Then
                            cond = cond & ", addizionale_100_extra_leasing='" & Replace(Rs("addizionale_100_extra"), ",", ".") & "'"
                        End If

                        If (Rs("rimborso_100_extra") & "") <> "" Then
                            cond = cond & ", rimborso_100_extra_leasing='" & Replace(Rs("rimborso_100_extra"), ",", ".") & "'"
                        End If

                        If (Rs("data_inizio_leasing") & "") <> "" Then
                            'cond = cond & ", data_inizio_leasing='" & funzioni_comuni.getDataDb_senza_orario(Rs("data_inizio_leasing")) & "'"
                            cond = cond & ", data_inizio_leasing='" & Rs("data_inizio_leasing") & "'"
                        End If

                        If (Rs("data_fine_leasing") & "") <> "" Then
                            'cond = cond & ", data_fine_leasing='" & funzioni_comuni.getDataDb_con_orario(Rs("data_fine_leasing") & " 23:59:59") & "'"
                            cond = cond & ", data_fine_leasing='" & Rs("data_fine_leasing") & " 23:59:59" & "'"
                        End If

                        If (Rs("data_immatricolazione") & "") <> "" Then
                            'cond = cond & ", data_immatricolazione='" & getDataDb_con_orario(Rs("data_immatricolazione") & " 23:59:59") & "'"
                            cond = cond & ", data_immatricolazione='" & Rs("data_immatricolazione") & " 23:59:59" & "'"
                        End If

                        odierna = Now

                        CmdSalvataggio = New Data.SqlClient.SqlCommand("UPDATE veicoli SET data_ultima_modifica=getDate(), id_operatore_ultima_modifica='" & Request.Cookies("SicilyRentCar")("idUtente") & "' " & cond & " WHERE targa='" & Rs("targa").ToString.Trim.Replace("'", "''") & "'", DbcSalvataggio)
                        'Response.Write("UPDATE veicoli SET data_ultima_modifica=getDate(), id_operatore_ultima_modifica='" & Request.Cookies("SicilyRentCar")("idUtente") & "' " & cond & " WHERE targa='" & Rs("targa").ToString.Trim.Replace("'", "''") & "'")
                        'Response.End()

                        CmdSalvataggio.ExecuteNonQuery()

                        If (Rs("numero_prima_fattura") & "") <> "" Or (Rs("numero_seconda_fattura") & "") <> "" Or (Rs("numero_prima_nc") & "") <> "" Then
                            CmdSalvataggio = New Data.SqlClient.SqlCommand("SELECT id FROM veicoli WHERE targa='" & Rs("targa").ToString.Trim.Replace("'", "''") & "'", DbcSalvataggio)
                            id_veicolo = CmdSalvataggio.ExecuteScalar
                        End If


                        'INSERIMENTO/AGGIORNAMENTO FATTURE DI ACQUISTO
                        'I FATTURA ACQUISTO -------
                        If (Rs("numero_prima_fattura") & "") <> "" Then
                            If Rs("update_fattura_1") Then
                                sqlStr = "UPDATE fatture_acquisto_veicoli SET data_acquisto='" & funzioni_comuni.getDataDb_senza_orario(Rs("data_prima_fattura")) & "'," &
                                "imponibile_acquisto='" & Replace(Rs("importo_prima_fattura"), ",", ".") & "'," &
                                "operatore_ultima_modifica='" & Request.Cookies("SicilyRentCar")("idUtente") & "'," &
                                "data_ultima_modifica=getDate() " &
                                "WHERE id_veicolo='" & id_veicolo & "' AND num_acquisto='" & Rs("numero_prima_fattura").ToString.Replace("'", "''") & "' AND tipo='FATTURA'"
                            Else
                                sqlStr = "INSERT INTO fatture_acquisto_veicoli (id_veicolo,data_acquisto,num_acquisto,imponibile_acquisto,tipo,operatore_inserimento,data_inserimento) VALUES (" _
                                    & "'" & id_veicolo & "','" & funzioni_comuni.getDataDb_senza_orario(Rs("data_prima_fattura")) & "'" _
                                    & ",'" & Replace(Rs("numero_prima_fattura"), "'", "''") & "','" & Replace(Rs("importo_prima_fattura"), ",", ".") & "'" _
                                    & ",'FATTURA','" & Request.Cookies("SicilyRentCar")("idUtente") & "',getDate())"
                            End If

                            CmdSalvataggio = New Data.SqlClient.SqlCommand(sqlStr, DbcSalvataggio)
                            CmdSalvataggio.ExecuteNonQuery()
                        End If
                        'II FATTURA ACQUISTO -------
                        If (Rs("numero_seconda_fattura") & "") <> "" Then
                            If Rs("update_fattura_2") Then
                                sqlStr = "UPDATE fatture_acquisto_veicoli SET data_acquisto='" & funzioni_comuni.getDataDb_senza_orario(Rs("data_seconda_fattura")) & "'," &
                                    "imponibile_acquisto='" & Replace(Rs("importo_seconda_fattura"), ",", ".") & "'," &
                                    "operatore_ultima_modifica='" & Request.Cookies("SicilyRentCar")("idUtente") & "'," &
                                    "data_ultima_modifica=getDate() " &
                                    "WHERE id_veicolo='" & id_veicolo & "' AND num_acquisto='" & Rs("numero_seconda_fattura").ToString.Replace("'", "''") & "' AND tipo='FATTURA'"
                            Else
                                sqlStr = "INSERT INTO fatture_acquisto_veicoli (id_veicolo,data_acquisto,num_acquisto,imponibile_acquisto,tipo,operatore_inserimento,data_inserimento) VALUES (" _
                                    & "'" & id_veicolo & "','" & funzioni_comuni.getDataDb_senza_orario(Rs("data_seconda_fattura")) & "'" _
                                    & ",'" & Replace(Rs("numero_seconda_fattura"), "'", "''") & "','" & Replace(Rs("importo_seconda_fattura"), ",", ".") & "'" _
                                    & ",'FATTURA','" & Request.Cookies("SicilyRentCar")("idUtente") & "',getDate())"
                            End If

                            CmdSalvataggio = New Data.SqlClient.SqlCommand(sqlStr, DbcSalvataggio)
                            CmdSalvataggio.ExecuteNonQuery()
                        End If
                        'I NOTA DI CREDITO ACQUISTO -------
                        If (Rs("numero_prima_nc") & "") <> "" Then
                            If Rs("update_nc_1") Then
                                sqlStr = "UPDATE fatture_acquisto_veicoli SET data_acquisto='" & funzioni_comuni.getDataDb_senza_orario(Rs("data_prima_nc")) & "'," &
                                    "imponibile_acquisto='" & Replace(Rs("importo_prima_nc"), ",", ".") & "'," &
                                    "operatore_ultima_modifica='" & Request.Cookies("SicilyRentCar")("idUtente") & "'," &
                                    "data_ultima_modifica=getDate() " &
                                    "WHERE id_veicolo='" & id_veicolo & "' AND num_acquisto='" & Rs("numero_prima_nc").ToString.Replace("'", "''") & "' AND tipo='NOTA'"
                            Else
                                sqlStr = "INSERT INTO fatture_acquisto_veicoli (id_veicolo,data_acquisto,num_acquisto,imponibile_acquisto,tipo,operatore_inserimento,data_inserimento) VALUES (" _
                                    & "'" & id_veicolo & "','" & funzioni_comuni.getDataDb_senza_orario(Rs("data_prima_nc")) & "'" _
                                    & ",'" & Replace(Rs("numero_prima_nc"), "'", "''") & "','" & Replace(CDbl(Rs("importo_prima_nc")) * -1, ",", ".") & "'" _
                                    & ",'NOTA','" & Request.Cookies("SicilyRentCar")("idUtente") & "',getDate())"
                            End If

                            CmdSalvataggio = New Data.SqlClient.SqlCommand(sqlStr, DbcSalvataggio)
                            CmdSalvataggio.ExecuteNonQuery()
                        End If
                    Loop

                    CmdSalvataggio.Dispose()
                    CmdSalvataggio = Nothing
                    DbcSalvataggio.Close()
                    DbcSalvataggio.Dispose()
                    DbcSalvataggio = Nothing

                    Rs.Close()
                    Dbc.Close()
                    Rs = Nothing
                    Dbc = Nothing

                    Libreria.genUserMsgBox(Me, "Tutte le vetture sono state aggiornate correttamente")
                End If
                'Response.Write("Campi Obbl KO " & CampiObbligatoriKO & "<br>")
                'Response.Write("Campi Targa già P " & CampoTargaPresente & "<br>")
                'Response.Write("Campi Telaio già P " & CampoTelaioPresente & "<br>")
                'Response.Write("Campi Modello non P " & CampoModelloNonPresente & "<br>")
            Else
                lblErrore2.Text = "Il file scelto non è corretto."
            End If
        Else
            'Response.Write("OUT")
            'Response.End()
            lblErrore2.Text = "Scegliere un file."
        End If
    End Sub

    Public Sub CancellaTabAppoggioDismissioni()
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim cmd As New Data.SqlClient.SqlCommand("DELETE FROM dismissioni_appoggio WHERE id_utente='" & Request.Cookies("SicilyRentCar")("idUtente") & "'", Dbc)
        cmd.ExecuteNonQuery()

        cmd.Dispose()
        cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Function getTotaleVendita(ByVal id_veicolo As String, ByVal non_calcolare_nel_totale As String) As Double
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(SUM(imponibile_vendita),0) FROM fatture_vendita_veicoli WITH(NOLOCK) WHERE (id_veicolo='" & id_veicolo & "') " & non_calcolare_nel_totale, Dbc)

        getTotaleVendita = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub CaricaFoglioExcelVendita(Optional ByVal file_da_caricare As String = "")
        LblImportFile.Text = ""

        CancellaTabAppoggioDismissioni()

        Dim filePath As String
        filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\docs"

        Dim fileName As String

        If file_da_caricare = "" Then
            fileName = FileUpload2.FileName
        End If

        Dim FileTestoValorizzato As Boolean = False

        If FileUpload2.HasFile Or file_da_caricare <> "" Then
            If FileUpload2.FileName = "dismissioni.csv" Or file_da_caricare <> "" Then
                Dim objStreamReader As StreamReader
                If FileUpload2.HasFile Then
                    If File.Exists(filePath & "\" & fileName) Then
                        'Response.Write("IN")
                        'Response.End()
                        File.Delete(filePath & "\" & fileName)
                    End If
                    FileUpload2.SaveAs(filePath & "\" & fileName)
                    'Aprire un file per la lettura        
                    objStreamReader = File.OpenText(filePath & "\" & fileName)

                    nome_file.Text = filePath & "\" & fileName
                Else
                    'Aprire un file per la lettura        
                    objStreamReader = File.OpenText(file_da_caricare)
                    nome_file.Text = file_da_caricare
                End If
                Dim Riga As String
                Dim RigaStr(1) As String

                Riga = objStreamReader.ReadLine()

                Dim possibile_salvare As Boolean = True
                Dim test As Boolean

                While (objStreamReader.Peek() <> -1)
                    Riga = objStreamReader.ReadLine()
                    RigaStr = Split(Riga, ";")

                    test = CaricaRigaInDBVendita(Riga)
                    If Not test Then
                        possibile_salvare = False
                    End If
                End While

                objStreamReader.Close()

                If possibile_salvare Then
                    Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()
                    Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc2.Open()
                    Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM dismissioni_appoggio WITH(NOLOCK)", Dbc)
                    Dim Cmd2 As New Data.SqlClient.SqlCommand("", Dbc2)

                    Dim Rs As Data.SqlClient.SqlDataReader
                    Rs = Cmd.ExecuteReader()

                    Dim sqlStr As String = ""
                    Dim venduta_da_fattura As String
                    Dim totale_fatture As Double

                    Dim non_calcolare_nel_totale As String

                    Do While Rs.Read
                        Dim cond As String = ""

                        If (Rs("ddt") & "") <> "" Then
                            cond = cond & ",num_bolla='" & Replace(Rs("ddt"), "'", "''") & "'"
                        End If

                        If (Rs("data_ddt") & "") <> "" Then
                            cond = cond & ",data_bolla='" & funzioni_comuni.getDataDb_senza_orario(Rs("data_ddt")) & "'"
                        End If

                        If (Rs("data_atto_di_vendita") & "") <> "" Then
                            cond = cond & ",data_atto_vendita='" & funzioni_comuni.getDataDb_senza_orario(Rs("data_atto_di_vendita")) & "'"
                        End If

                        If (Rs("id_venditore") & "") <> "" Then
                            cond = cond & ",id_venditore='" & Rs("id_venditore") & "'"
                        End If

                        If (Rs("id_presso_di") & "") <> "" Then
                            cond = cond & ",id_fornitore='" & Rs("id_presso_di") & "'"
                        End If

                        If (Rs("id_acquirente") & "") <> "" Then
                            cond = cond & ",id_acquirente='" & Rs("id_acquirente") & "'"
                        End If

                        If (Rs("note") & "") <> "" Then
                            Cmd2 = New Data.SqlClient.SqlCommand("SELECT ISNULL(note_vendita,'') FROM veicoli WITH(NOLOCK) WHERE id='" & Rs("id_veicolo") & "'", Dbc2)
                            cond = cond & ",note_vendita='" & Cmd2.ExecuteScalar() & "' + ' ' + '" & Replace(Rs("note"), "'", "''") & "'"
                        End If

                        venduta_da_fattura = ""
                        totale_fatture = 0
                        non_calcolare_nel_totale = ""

                        If (Rs("numero_prima_fattura") & "") <> "" Or (Rs("numero_seconda_fattura") & "") <> "" Or (Rs("numero_prima_nc") & "") <> "" Then
                            If (Rs("numero_prima_fattura") & "") <> "" Then
                                If Rs("update_fattura_1") Then
                                    non_calcolare_nel_totale = non_calcolare_nel_totale & " AND num_vendita<>'" & Replace(Rs("numero_prima_fattura"), "'", "''") & "'"
                                End If
                                totale_fatture = totale_fatture + CDbl(Rs("importo_prima_fattura"))
                            End If

                            If (Rs("numero_seconda_fattura") & "") <> "" Then
                                If Rs("update_fattura_2") Then
                                    non_calcolare_nel_totale = non_calcolare_nel_totale & " AND num_vendita<>'" & Replace(Rs("numero_seconda_fattura"), "'", "''") & "'"
                                End If
                                totale_fatture = totale_fatture + CDbl(Rs("importo_seconda_fattura"))
                            End If
                            If (Rs("numero_prima_nc") & "") <> "" Then
                                If Rs("update_nc_1") Then
                                    non_calcolare_nel_totale = non_calcolare_nel_totale & " AND num_vendita<>'" & Replace(Rs("numero_prima_nc"), "'", "''") & "'"
                                End If
                                totale_fatture = totale_fatture - CDbl(Rs("importo_prima_nc"))
                            End If


                            totale_fatture = totale_fatture + getTotaleVendita(Rs("id_veicolo"), non_calcolare_nel_totale)

                            If totale_fatture > 0 Then
                                venduta_da_fattura = " venduta_da_fattura='1', "
                            ElseIf totale_fatture = 0 Then
                                venduta_da_fattura = " venduta_da_fattura='0', "
                            End If

                        End If

                        sqlStr = "UPDATE veicoli SET " & venduta_da_fattura & " data_ultima_modifica=getDate(), id_operatore_ultima_modifica='" & Request.Cookies("SicilyRentCar")("idUtente") & "'" & cond & " WHERE id='" & Rs("id_veicolo") & "'"


                        Cmd2 = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                        Cmd2.ExecuteNonQuery()

                        'SALVATAGGIO DELLE FATTURE (SE SPECIFICATE) ------------------------------------------------------------------
                        'I FATTURA DI VENDITA -------
                        If (Rs("numero_prima_fattura") & "") <> "" Then
                            If Rs("update_fattura_1") Then
                                sqlStr = "UPDATE fatture_vendita_veicoli SET data_vendita='" & funzioni_comuni.getDataDb_senza_orario(Rs("data_prima_fattura")) & "'," &
                            "imponibile_vendita='" & Replace(Rs("importo_prima_fattura"), ",", ".") & "'," &
                            "operatore_ultima_modifica='" & Request.Cookies("SicilyRentCar")("idUtente") & "'," &
                            "data_ultima_modifica=getDate() " &
                            "WHERE id_veicolo='" & Rs("id_veicolo") & "' AND num_vendita='" & Rs("numero_prima_fattura").ToString.Replace("'", "''") & "' AND tipo='FATTURA'"
                            Else
                                sqlStr = "INSERT INTO fatture_vendita_veicoli (id_veicolo,data_vendita,num_vendita,imponibile_vendita,tipo,operatore_inserimento,data_inserimento) VALUES (" _
                            & "'" & Rs("id_veicolo") & "','" & funzioni_comuni.getDataDb_senza_orario(Rs("data_prima_fattura")) & "'" _
                            & ",'" & Replace(Rs("numero_prima_fattura"), "'", "''") & "','" & Replace(Rs("importo_prima_fattura"), ",", ".") & "'" _
                            & ",'FATTURA','" & Request.Cookies("SicilyRentCar")("idUtente") & "',getDate())"
                            End If

                            Cmd2 = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                            Cmd2.ExecuteNonQuery()
                        End If
                        'II FATTURA DI VENDITA -------
                        If (Rs("numero_seconda_fattura") & "") <> "" Then
                            If Rs("update_fattura_2") Then
                                sqlStr = "UPDATE fatture_vendita_veicoli SET data_vendita='" & funzioni_comuni.getDataDb_senza_orario(Rs("data_seconda_fattura")) & "'," &
                            "imponibile_vendita='" & Replace(Rs("importo_seconda_fattura"), ",", ".") & "'," &
                            "operatore_ultima_modifica='" & Request.Cookies("SicilyRentCar")("idUtente") & "'," &
                            "data_ultima_modifica=getDate() " &
                            "WHERE id_veicolo='" & Rs("id_veicolo") & "' AND num_vendita='" & Rs("numero_seconda_fattura").ToString.Replace("'", "''") & "' AND tipo='FATTURA'"
                            Else
                                sqlStr = "INSERT INTO fatture_vendita_veicoli (id_veicolo,data_vendita,num_vendita,imponibile_vendita,tipo,operatore_inserimento,data_inserimento) VALUES (" _
                            & "'" & Rs("id_veicolo") & "','" & funzioni_comuni.getDataDb_senza_orario(Rs("data_seconda_fattura")) & "'" _
                            & ",'" & Replace(Rs("numero_seconda_fattura"), "'", "''") & "','" & Replace(Rs("importo_seconda_fattura"), ",", ".") & "'" _
                            & ",'FATTURA','" & Request.Cookies("SicilyRentCar")("idUtente") & "',getDate())"
                            End If

                            Cmd2 = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                            Cmd2.ExecuteNonQuery()
                        End If
                        'I NOTA DI CREDITO ACQUISTO -------
                        If (Rs("numero_prima_nc") & "") <> "" Then
                            If Rs("update_nc_1") Then
                                sqlStr = "UPDATE fatture_vendita_veicoli SET data_vendita='" & funzioni_comuni.getDataDb_senza_orario(Rs("data_prima_nc")) & "'," &
                                    "imponibile_vendita='" & Replace(CDbl(Rs("importo_prima_nc")) * -1, ",", ".") & "'," &
                                    "operatore_ultima_modifica='" & Request.Cookies("SicilyRentCar")("idUtente") & "'," &
                                    "data_ultima_modifica=getDate() " &
                                    "WHERE id_veicolo='" & Rs("id_veicolo") & "' AND num_vendita='" & Replace(Rs("numero_prima_nc"), "'", "''") & "' AND tipo='NOTA'"
                            Else
                                sqlStr = "INSERT INTO fatture_vendita_veicoli (id_veicolo,data_vendita,num_vendita,imponibile_vendita,tipo,operatore_inserimento,data_inserimento) VALUES (" _
                            & "'" & Rs("id_veicolo") & "','" & funzioni_comuni.getDataDb_senza_orario(Rs("data_prima_nc")) & "'" _
                            & ",'" & Replace(Rs("numero_prima_nc"), "'", "''") & "','" & Replace(CDbl(Rs("importo_prima_nc")) * -1, ",", ".") & "'" _
                            & ",'NOTA','" & Request.Cookies("SicilyRentCar")("idUtente") & "',getDate())"
                            End If

                    Cmd2 = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                    Cmd2.ExecuteNonQuery()
                End If
                    Loop

                    Rs.Close()
                    Rs = Nothing
                    Cmd.Dispose()
                    Cmd = Nothing
                    Cmd2.Dispose()
                    Cmd2 = Nothing
                    Dbc2.Close()
                    Dbc2.Dispose()
                    Dbc2 = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing

                    Libreria.genUserMsgBox(Me, "Tutti i dati sono stati caricati correttamente")
                Else
                    Libreria.genUserMsgBox(Me, "Dati non caricati sul Data Base (Verificare gli errori)")
                End If
            Else
                Libreria.genUserMsgBox(Me, "Il file scelto non è corretto.")
            End If
        Else
            Libreria.genUserMsgBox(Me, "Scegliere un file.")
        End If
    End Sub

    Protected Sub btnImportaFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportaFile.Click
        If dropTipo.SelectedValue = "1" Then
            CaricaFoglioExcelAcquisto()
        ElseIf dropTipo.SelectedValue = "2" Then
            CaricaFoglioExcelVendita()
        End If
    End Sub

    Protected Sub btnModello_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnModello.Click
        Session("nome_file") = nome_file.Text

        Response.Redirect("tabelle.aspx?val=modello&prov=ImportMassivo")
    End Sub

    Protected Sub btnColore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnColore.Click
        Session("nome_file") = nome_file.Text
        Response.Redirect("tabelle.aspx?val=colore&prov=ImportMassivo")
    End Sub

    Protected Sub btnProprietario_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProprietario.Click
        Session("nome_file") = nome_file.Text
        Response.Redirect("tabelle.aspx?val=propr&prov=ImportMassivo")
    End Sub
End Class
