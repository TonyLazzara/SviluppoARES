
Imports System.IO


Partial Class ImportVeicoli
    Inherits System.Web.UI.Page

    Public Sub CancellaTabAppoggioVeicoli()
        Dim DbcCancellaInTabAppoggio As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
        DbcCancellaInTabAppoggio.Open()
        Dim CmdCancellaInTabAppoggio As New Data.SqlClient.SqlCommand("", DbcCancellaInTabAppoggio)

        CmdCancellaInTabAppoggio.CommandText = "delete from veicoli_appoggio "
        'Response.Write(CmdCancellaInTabAppoggio.CommandText & "<br>")
        'Response.End()

        CmdCancellaInTabAppoggio.ExecuteNonQuery()
        CmdCancellaInTabAppoggio.Dispose()
        DbcCancellaInTabAppoggio.Close()
        DbcCancellaInTabAppoggio = Nothing
    End Sub

    'Public Sub ChiudiProcessi()
    '    ' Kill all excel processes
    '    Dim pList() As Process
    '    Dim pExcelProcess As System.Diagnostics.Process
    '    pList = Process.GetProcesses
    '    For Each pExcelProcess In pList
    '        If pExcelProcess.ProcessName.ToUpper = "EXCEL" Then
    '            pExcelProcess.Kill()
    '        End If
    '    Next
    'End Sub

    Public Function CaricaFoglioExcelInDB(ByVal RigaStringa As String) As Boolean
        Dim RigaStrApp(1) As String
        Dim presente_targa As Char
        Dim presente_telaio As Char
        Dim presente_modello As Char
        Dim presente_colore As Char
        Dim presente_proprietario As Char

        Dim eseguire_insert As Char
        Dim eseguire_update As Char

        Dim id_modello As String
        Dim id_colore As String
        Dim id_proprietario As String

        Dim test As String

        Dim possibile_salvare As Boolean = True

        'ChiudiProcessi()

        RigaStrApp = Split(RigaStringa, ";")
        'For i = 0 To UBound(RigaStrApp)
        '    Response.Write(RigaStrApp(i) & "<br>")
        'Next
        'Response.End()

        'Controllo esistenza targa
        Dim Dbc As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

        If Trim(RigaStrApp(0)) <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT targa, inserito_da_foglio_excel FROM veicoli where targa='" & Replace(RigaStrApp(0), "'", "''") & "'", Dbc)

            'Controllo esistenza TARGA
            Dim Rs_targa As Data.SqlClient.SqlDataReader
            Rs_targa = Cmd.ExecuteReader()
            If Rs_targa.HasRows() Then
                'SE LA TARGA E' STATA TROVATA SI CONTROLLA IL CAMPO "INSERITO DA FOGLIO EXCEL": SE E' FALSE VUOL DIRE CHE E' STATA INSERITA
                'DALLA STAZIONE. FA FEDE QUANTO SI TROVA SU FOGLIO EXCEL. SI DEVE ESEGUIRE UN UPDATE SU VEICOLI E SOVRASCRIVERE IL MOVIMENTO DI
                'IMMATRICOLAZIONE.
                Rs_targa.Read()

                presente_targa = "1"

                If Rs_targa("inserito_da_foglio_excel") Then
                    eseguire_insert = "1"
                    eseguire_update = "0"

                    possibile_salvare = False
                Else
                    eseguire_insert = "0"
                    eseguire_update = "1"
                End If
            Else
                presente_targa = "0"

                eseguire_insert = "1"
                eseguire_update = "0"
            End If
            Rs_targa.Close()
            Rs_targa = Nothing

            Dbc.Close()
            Dbc.Open()
        Else
            'IL CAMPO E' OBBLIGATORIO
            presente_targa = "0"

            eseguire_insert = "1"
            eseguire_update = "0"

            possibile_salvare = False
        End If
        

        'Controllo esistenza TELAIO - SE SI DEVE ESEGUIRE UN UPDATE CONTROLLO TRANNE CHE PER LA TARGA ATTUALE

        If Trim(RigaStrApp(1)) <> "" Then
            Dim condizione_telaio As String = ""

            If eseguire_update = "1" Then
                condizione_telaio = " AND targa<>'" & RigaStrApp(0) & "'"
            End If

            Cmd = New Data.SqlClient.SqlCommand("SELECT telaio FROM veicoli WHERE telaio='" & Replace(RigaStrApp(1), "'", "''") & "'" & condizione_telaio, Dbc)

            test = Cmd.ExecuteScalar & ""

            If test <> "" Then
                'TELAIO GIA' ESISTENTE - IMPOSSIBILE SALVARE
                presente_telaio = "1"
                possibile_salvare = False
            Else
                presente_telaio = "0"
            End If
        Else
            'CAMPO OBBLIGATORIO
            possibile_salvare = False
            presente_telaio = "0"
        End If


        'Controllo esistenza MODELLO
        If Trim(RigaStrApp(2)) <> "" Then
            Dim ModelloStr As String = ""
            ModelloStr = Replace(RigaStrApp(2), "'", "''")
            ModelloStr = Replace(ModelloStr, "�", "ë")
            'Response.Write(ModelloStr)
            'Response.End()

            Cmd = New Data.SqlClient.SqlCommand("SELECT id_modello FROM modelli WHERE descrizione='" & ModelloStr & "'", Dbc)

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
            'CAMPO OBBLIGATORIO
            presente_modello = "0"
            id_modello = "NULL"
            possibile_salvare = False
        End If
       
        'Controllo esistenza COLORE
        If Trim(RigaStrApp(3)) <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM colori WHERE descrizione='" & Replace(RigaStrApp(3), "'", "''") & "'", Dbc)

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
            possibile_salvare = False
        End If
        
        'Controllo esistenza proprietario
        If Trim(RigaStrApp(5)) <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM proprietari_veicoli WHERE descrizione='" & Replace(RigaStrApp(5), "'", "''") & "'", Dbc)

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
            possibile_salvare = False
        End If
       

        'If IsDate(RigaStrApp(5)) = False Then 'Data Immatricolazione non valida  
        '    Libreria.genUserMsgBox(Me, "Il campo Data Immatricolazione ha un formato errato")
        '    Exit Sub
        'End If

        Dim dataVal As String

        If RigaStrApp(4) <> "" Then
            Try
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario2(RigaStrApp(4))
                dataVal = "'" & RigaStrApp(4) & "'"
            Catch ex As Exception
                dataVal = "'" & RigaStrApp(4) & "'"
                possibile_salvare = False
            End Try
        Else
            dataVal = "NULL"
            possibile_salvare = False
        End If

        Dim EscludiDaAmmortamento As String
        If UCase(RigaStrApp(6)) = "SI" Then 'Escludi da ammortamento 
            EscludiDaAmmortamento = "SI"
        Else
            EscludiDaAmmortamento = " "
        End If

        'SEZIONE LEASING: O NON E' PRESENTE ALCUN DATO (AUTO NON LEASING) OPPURE DEVONO ESSERE TUTTI VALORIZZATI
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
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario2(RigaStrApp(7))
                data1 = funzioni_comuni.getDataDb_senza_orario2(RigaStrApp(7))
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
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario2(RigaStrApp(8))
                data2 = funzioni_comuni.getDataDb_senza_orario2(RigaStrApp(8))
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

        If RigaStrApp(11) <> "" Then
            leasing_almeno_un_dato_valorizzatoLeasys = True
            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(11))

                canone_mensile = "'" & RigaStrApp(11) & "'"

                If test_dbl < 0 Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                canone_mensile = "'" & Replace(RigaStrApp(11), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            canone_mensile = "NULL"
        End If
        'DURATA IN MESI ---------------------
        Dim durata_mesi As String

        If RigaStrApp(13) <> "" Then
            leasing_almeno_un_dato_valorizzatoSBC = True
            Try
                Dim test_int As Integer = CInt(RigaStrApp(13))

                durata_mesi = "'" & RigaStrApp(13) & "'"

                If test_int <= 0 Or RigaStrApp(13).Contains(",") Or RigaStrApp(13).Contains(".") Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                durata_mesi = "'" & Replace(RigaStrApp(13), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            durata_mesi = "NULL"
        End If

        'ENTE FINANZIATORE-------------------------------------------
        Dim ente_finanziatore As String
        Dim id_ente_finanziatore As String

        If Trim(RigaStrApp(14)) <> "" Then
            leasing_almeno_un_dato_valorizzatoSBC = True
            ente_finanziatore = "'" & Replace(RigaStrApp(14), "'", "''") & "'"

            Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM enti_finanziatori WHERE nome='" & Replace(RigaStrApp(14), "'", "''") & "'", Dbc)
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
            'possibile_salvare = False
        End If

        'KM INCLUSI -----------------------
        Dim km_inclusi As String

        If RigaStrApp(15) <> "" Then
            leasing_almeno_un_dato_valorizzatoLeasys = True
            Try
                Dim test_int As Integer = CInt(RigaStrApp(15))

                km_inclusi = "'" & RigaStrApp(15) & "'"

                If test_int < 0 Or RigaStrApp(15).Contains(",") Or RigaStrApp(15).Contains(".") Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                km_inclusi = "'" & Replace(RigaStrApp(15), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            km_inclusi = "NULL"
        End If
        'Response.Write(possibile_salvare & "KM<br>")

        'Prezzo Acquisto -----------------------
        Dim prezzo_acquisto As String

        If RigaStrApp(9) <> "" Then
            Try
                Dim test_int As Integer = CInt(RigaStrApp(9))

                prezzo_acquisto = "'" & RigaStrApp(9) & "'"

                'If test_int < 0 Or RigaStrApp(9).Contains(",") Or RigaStrApp(9).Contains(".") Then
                '    possibile_salvare = False
                'End If
            Catch ex As Exception
                prezzo_acquisto = "'" & Replace(RigaStrApp(9), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            prezzo_acquisto = "NULL"
        End If
        'Response.Write(possibile_salvare & "PA<br>")

        'Anticipo -----------------------
        Dim anticipo As String

        If RigaStrApp(10) <> "" Then
            Try
                Dim test_int As Integer = CInt(RigaStrApp(10))

                anticipo = "'" & RigaStrApp(10) & "'"

                'If test_int < 0 Or RigaStrApp(10).Contains(",") Or RigaStrApp(10).Contains(".") Then
                '    possibile_salvare = False
                'End If
            Catch ex As Exception
                anticipo = "'" & Replace(RigaStrApp(10), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            anticipo = "NULL"
        End If
        'Response.Write(possibile_salvare & "A<br>")

        'Riscatto -----------------------
        Dim riscatto As String

        If RigaStrApp(12) <> "" Then
            Try
                Dim test_int As Integer = CInt(RigaStrApp(12))

                riscatto = "'" & RigaStrApp(12) & "'"

                'If test_int < 0 Or RigaStrApp(12).Contains(",") Or RigaStrApp(12).Contains(".") Then
                '    possibile_salvare = False
                'End If
            Catch ex As Exception
                riscatto = "'" & Replace(RigaStrApp(12), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            riscatto = "NULL"
        End If
        'Response.Write(possibile_salvare & "R<br>")


        'FRANCHIGIA KM COMPRESI -------
        Dim franchigia_km_compresi As String

        If RigaStrApp(16) <> "" Then
            leasing_almeno_un_dato_valorizzatoLeasys = True
            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(16))

                franchigia_km_compresi = "'" & RigaStrApp(16) & "'"

                If test_dbl < 0 Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                franchigia_km_compresi = "'" & Replace(RigaStrApp(16), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            franchigia_km_compresi = "NULL"
        End If
        'Response.Write(possibile_salvare & "FC<br>")

        'ADDIZZIONALE 100 KM -------
        Dim addizionale_100_km As String

        If RigaStrApp(17) <> "" Then
            leasing_almeno_un_dato_valorizzatoLeasys = True
            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(17))

                addizionale_100_km = "'" & RigaStrApp(17) & "'"

                If test_dbl < 0 Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                addizionale_100_km = "'" & Replace(RigaStrApp(17), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            addizionale_100_km = "NULL"
        End If

        'RIMBORSO 100 KM -------
        Dim rimborso_100_km As String

        If RigaStrApp(18) <> "" Then
            leasing_almeno_un_dato_valorizzatoLeasys = True
            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(18))

                rimborso_100_km = "'" & RigaStrApp(18) & "'"

                If test_dbl < 0 Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                rimborso_100_km = "'" & Replace(RigaStrApp(18), "'", "''") & "'"
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

        'SE E' STATO SPECIFICATO ALMENO UN CAMPO DI LEASING IL PROPRIETARIO DEVE ESSERE SPECIFICATO!!!

        If (leasing_almeno_un_dato_valorizzatoLeasys Or leasing_almeno_un_dato_valorizzatoSBC) And ente_finanziatore = "NULL" Then
            possibile_salvare = False
        End If

        'FATTURE--------------------------------------------------------------------------------------------------------------------------
        'I FATTURA------------
        Dim numero_fattura_1 As String
        Dim data_fattura_1 As String
        Dim importo_fattura_1 As String

        If RigaStrApp(19).Trim = "" And RigaStrApp(20).Trim = "" And RigaStrApp(21).Trim = "" Then
            numero_fattura_1 = "NULL"
            data_fattura_1 = "NULL"
            importo_fattura_1 = "NULL"
        ElseIf RigaStrApp(19).Trim <> "" And RigaStrApp(20).Trim <> "" And RigaStrApp(21).Trim <> "" Then
            numero_fattura_1 = "'" & RigaStrApp(16).Trim.Replace("'", "''") & "'"

            Try
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario2(RigaStrApp(20))
                data_fattura_1 = "'" & RigaStrApp(20) & "'"
            Catch ex As Exception
                data_fattura_1 = "'" & RigaStrApp(20) & "'"
                possibile_salvare = False
            End Try

            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(21))

                importo_fattura_1 = "'" & RigaStrApp(21) & "'"

                If test_dbl < 0 Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                importo_fattura_1 = "'" & Replace(RigaStrApp(21), "'", "''") & "'"
                possibile_salvare = False
            End Try

        Else
            numero_fattura_1 = "'" & RigaStrApp(19).Trim.Replace("'", "''") & "'"
            data_fattura_1 = "'" & RigaStrApp(20).Trim.Replace("'", "''") & "'"
            importo_fattura_1 = "'" & RigaStrApp(21).Trim.Replace("'", "''") & "'"
            possibile_salvare = False
        End If

        'II FATTURA------------
        Dim numero_fattura_2 As String
        Dim data_fattura_2 As String
        Dim importo_fattura_2 As String

        If RigaStrApp(22).Trim = "" And RigaStrApp(23).Trim = "" And RigaStrApp(24).Trim = "" Then
            numero_fattura_2 = "NULL"
            data_fattura_2 = "NULL"
            importo_fattura_2 = "NULL"
        ElseIf RigaStrApp(22).Trim <> "" And RigaStrApp(23).Trim <> "" And RigaStrApp(24).Trim <> "" Then
            numero_fattura_2 = "'" & RigaStrApp(22).Trim.Replace("'", "''") & "'"

            Try
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario2(RigaStrApp(23))
                data_fattura_2 = "'" & RigaStrApp(23) & "'"
            Catch ex As Exception
                data_fattura_2 = "'" & RigaStrApp(23) & "'"
                possibile_salvare = False
            End Try

            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(24))

                importo_fattura_2 = "'" & RigaStrApp(24) & "'"

                If test_dbl < 0 Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                importo_fattura_2 = "'" & Replace(RigaStrApp(24), "'", "''") & "'"
                possibile_salvare = False
            End Try

        Else
            numero_fattura_2 = "'" & RigaStrApp(22).Trim.Replace("'", "''") & "'"
            data_fattura_2 = "'" & RigaStrApp(23).Trim.Replace("'", "''") & "'"
            importo_fattura_2 = "'" & RigaStrApp(24).Trim.Replace("'", "''") & "'"
            possibile_salvare = False
        End If

        'I NOTA DI CREDIO
        Dim numero_nc_1 As String
        Dim data_nc_1 As String
        Dim importo_nc_1 As String

        If RigaStrApp(25).Trim = "" And RigaStrApp(26).Trim = "" And RigaStrApp(27).Trim = "" Then
            numero_nc_1 = "NULL"
            data_nc_1 = "NULL"
            importo_nc_1 = "NULL"
        ElseIf RigaStrApp(25).Trim <> "" And RigaStrApp(26).Trim <> "" And RigaStrApp(27).Trim <> "" Then
            numero_nc_1 = "'" & RigaStrApp(22).Trim.Replace("'", "''") & "'"

            Try
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario2(RigaStrApp(26))
                data_nc_1 = "'" & RigaStrApp(26) & "'"
            Catch ex As Exception
                data_nc_1 = "'" & RigaStrApp(26) & "'"
                possibile_salvare = False
            End Try

            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(27))

                importo_nc_1 = "'" & RigaStrApp(24) & "'"

                If test_dbl < 0 Then
                    possibile_salvare = False
                End If
            Catch ex As Exception
                importo_nc_1 = "'" & Replace(RigaStrApp(27), "'", "''") & "'"
                possibile_salvare = False
            End Try
        Else
            numero_nc_1 = "'" & RigaStrApp(25).Trim.Replace("'", "''") & "'"
            data_nc_1 = "'" & RigaStrApp(26).Trim.Replace("'", "''") & "'"
            importo_nc_1 = "'" & RigaStrApp(27).Trim.Replace("'", "''") & "'"
            possibile_salvare = False
        End If

        Dim riga_ok As String
        If possibile_salvare Then
            riga_ok = "'1'"
        Else
            riga_ok = "'0'"
        End If
        '---------------------------------------------------------------------------------------------------------------------------------

        Cmd.CommandText = "insert into veicoli_appoggio (targa,presente_targa,telaio,presente_telaio,modello,presente_modello,id_modello,colore,presente_colore,id_colore,data_immatricolazione,proprietario,presente_proprietario,id_proprietario,escludi_ammortamento, data_inizio_leasing, data_fine_leasing,data_finale_precedente, canone_mensile, mesi_leasing, ente_finanziatore, id_ente_finanziatore, km_compresi, franchigia_km_compresi, addizionale_100_extra, rimborso_100_extra, is_leasing, is_leasing_sbc,numero_prima_fattura,data_prima_fattura,importo_prima_fattura,numero_seconda_fattura,data_seconda_fattura,importo_seconda_fattura,numero_prima_nc,data_prima_nc,importo_prima_nc, eseguire_insert, eseguire_update, riga_ok, prezzo_acquisto, anticipo, riscatto) " & _
                                                  "values('" & Replace(RigaStrApp(0), "'", "''") & "','" & presente_targa & "','" & Replace(RigaStrApp(1), "'", "''") & "','" & presente_telaio & "','" & Replace(RigaStrApp(2), "'", "''") & "','" & presente_modello & "'," & id_modello & ",'" & Replace(RigaStrApp(3), "'", "''") & "','" & presente_colore & "'," & id_colore & "," & dataVal & ",'" & Replace(RigaStrApp(5), "'", "''") & "','" & presente_proprietario & "'," & id_proprietario & ",'" & EscludiDaAmmortamento & "'," & _
                                                  data_inizio_leasing & "," & data_fine_leasing & ",'" & data_finale_precedente & "'," & _
                                                  canone_mensile & "," & durata_mesi & "," & ente_finanziatore & "," & id_ente_finanziatore & "," & _
                                                  km_inclusi & "," & franchigia_km_compresi & "," & addizionale_100_km & "," & rimborso_100_km & "," & _
                                                  leasys & "," & SBC & "," & numero_fattura_1 & "," & data_fattura_1 & "," & importo_fattura_1 & "," & _
                                                  numero_fattura_2 & "," & data_fattura_2 & "," & importo_fattura_2 & "," & _
                                                  numero_nc_1 & "," & data_nc_1 & "," & importo_nc_1 & "," & _
                                                  "'" & eseguire_insert & "','" & eseguire_update & "'," & riga_ok & "," & prezzo_acquisto & "," & anticipo & "," & riscatto & ")"
        'Response.Write(Cmd.CommandText & "<br>")
        'Response.End()

        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        CaricaFoglioExcelInDB = possibile_salvare
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
                        CaricaFoglioExcel(nome_file)
                    End If
                Else
                    Response.Redirect("default.aspx")
                End If
            End If
        End If
    End Sub

    Protected Sub CaricaFoglioExcel(Optional ByVal file_da_caricare As String = "")
        CancellaTabAppoggioVeicoli()

        Dim filePath As String
        filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\docs"

        Dim fileName As String

        Dim sqlstr As String = ""


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
        Try
            If FileUpload2.HasFile Or file_da_caricare <> "" Then
                'Response.Write("IN")
                'Response.End()

                'If FileUpload2.FileName = "parco_da_inserire.csv" Or file_da_caricare <> "" Then
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

                    possibile_salvare = CaricaFoglioExcelInDB(Riga)

                    If Not possibile_salvare Then
                        FileTestoValorizzato = True
                    End If
                End While

                objStreamReader.Close()

                filetxt.close()
                fs = Nothing

                'Response.Write(FileTestoValorizzato)
                'Response.End()

                If FileTestoValorizzato Then
                    Libreria.genUserMsgBox(Me, "Dati non caricati sul Data Base (Verificare gli errori)")
                Else
                    Dim DbcSalvataggio As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
                    DbcSalvataggio.Open()
                    Dim CmdSalvataggio As New Data.SqlClient.SqlCommand("", DbcSalvataggio)

                    Dim Dbc As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
                    Dbc.Open()
                    Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM veicoli_appoggio", Dbc)
                    'Response.Write(Cmd.CommandText & "<br><br>")
                    Dim Rs As Data.SqlClient.SqlDataReader
                    Rs = Cmd.ExecuteReader()

                    Dim auto_in_leasing As String
                    Dim auto_in_leasingSBC As String
                    Dim canone_mensile As String
                    Dim mesi_leasing As String
                    Dim id_ente_finanziatore As String
                    Dim km_compresi As String
                    Dim franchigia_km_compresi As String
                    Dim addizionale_100_extra As String
                    Dim rimborso_100_extra As String
                    Dim data_inizio_leasing As String
                    Dim data_fine_leasing As String

                    'Tony
                    Dim prezzo_acquisto As String
                    Dim anticipo As String
                    Dim riscatto As String

                    Do While Rs.Read
                        'Dati Generale Veicolo

                        If UCase(Rs("escludi_ammortamento")) = "SI" Then
                            EscludiAmmortamento = "1"
                        ElseIf UCase(Rs("escludi_ammortamento")) = "NO" Then
                            EscludiAmmortamento = "0"
                        Else
                            'SE NON VIENE SPECIFICATO NULLA IN AUTOMATICO NON SI ESCLUDE DA AMMORTAMENTO
                            EscludiAmmortamento = "0"
                        End If

                        If Rs("is_leasing") Or Rs("is_leasing_sbc") Then
                            If Rs("is_leasing") Then
                                auto_in_leasing = "'1'"
                                auto_in_leasingSBC = "'0'"
                            Else
                                auto_in_leasing = "'0'"
                                auto_in_leasingSBC = "'1'"
                            End If

                            If (Rs("canone_mensile") & "") <> "" Then
                                canone_mensile = "'" & Replace(Rs("canone_mensile"), ",", ".") & "'"
                            Else
                                canone_mensile = "NULL"
                            End If

                            If (Rs("mesi_leasing") & "") <> "" Then
                                mesi_leasing = "'" & Rs("mesi_leasing") & "'"
                            Else
                                mesi_leasing = "NULL"
                            End If

                            If (Rs("id_ente_finanziatore") & "") <> "" Then
                                id_ente_finanziatore = "'" & Rs("id_ente_finanziatore") & "'"
                            Else
                                id_ente_finanziatore = "NULL"
                            End If

                            If (Rs("km_compresi") & "") <> "" Then
                                km_compresi = "'" & Rs("km_compresi") & "'"
                            Else
                                km_compresi = "NULL"
                            End If

                            If (Rs("prezzo_acquisto") & "") <> "" Then
                                prezzo_acquisto = "'" & Rs("prezzo_acquisto") & "'"
                            Else
                                prezzo_acquisto = "NULL"
                            End If

                            If (Rs("anticipo") & "") <> "" Then
                                anticipo = "'" & Rs("anticipo") & "'"
                            Else
                                anticipo = "NULL"
                            End If

                            If (Rs("riscatto") & "") <> "" Then
                                riscatto = "'" & Rs("riscatto") & "'"
                            Else
                                riscatto = "NULL"
                            End If

                            If (Rs("franchigia_km_compresi") & "") <> "" Then
                                franchigia_km_compresi = "'" & Replace(Rs("franchigia_km_compresi"), ",", ".") & "'"
                            Else
                                franchigia_km_compresi = "NULL"
                            End If

                            If (Rs("prezzo_acquisto") & "") <> "" Then
                                prezzo_acquisto = "'" & Replace(Rs("prezzo_acquisto"), ",", ".") & "'"
                            Else
                                prezzo_acquisto = "NULL"
                            End If

                            If (Rs("anticipo") & "") <> "" Then
                                anticipo = "'" & Replace(Rs("anticipo"), ",", ".") & "'"
                            Else
                                anticipo = "NULL"
                            End If

                            If (Rs("riscatto") & "") <> "" Then
                                riscatto = "'" & Replace(Rs("riscatto"), ",", ".") & "'"
                            Else
                                riscatto = "NULL"
                            End If

                            If (Rs("addizionale_100_extra") & "") <> "" Then
                                addizionale_100_extra = "'" & Replace(Rs("addizionale_100_extra"), ",", ".") & "'"
                            Else
                                addizionale_100_extra = "NULL"
                            End If

                            If (Rs("rimborso_100_extra") & "") <> "" Then
                                rimborso_100_extra = "'" & Replace(Rs("rimborso_100_extra"), ",", ".") & "'"
                            Else
                                rimborso_100_extra = "NULL"
                            End If

                            If (Rs("data_inizio_leasing") & "") <> "" Then
                                data_inizio_leasing = "'" & funzioni_comuni.getDataDb_senza_orario(Rs("data_inizio_leasing")) & "'"
                            Else
                                data_inizio_leasing = "NULL"
                            End If

                            If (Rs("data_fine_leasing") & "") <> "" Then
                                data_fine_leasing = "'" & funzioni_comuni.getDataDb_con_orario(Rs("data_fine_leasing") & " 23:59:59") & "'"
                            Else
                                data_fine_leasing = "NULL"
                            End If
                        Else
                            auto_in_leasing = "'0'"
                            auto_in_leasingSBC = "'0'"
                            canone_mensile = "NULL"
                            mesi_leasing = "NULL"
                            id_ente_finanziatore = "NULL"
                            km_compresi = "NULL"
                            prezzo_acquisto = "NULL"
                            anticipo = "NULL"
                            riscatto = "NULL"
                            franchigia_km_compresi = "NULL"
                            addizionale_100_extra = "NULL"
                            rimborso_100_extra = "NULL"
                            data_inizio_leasing = "NULL"
                            data_fine_leasing = "NULL"
                        End If

                        'DataCorrente = DatePart("d", Now) & "/" & DatePart("M", Now) & "/" & DatePart("yyyy", Now) & " " & DatePart("h", Now) & "." & DatePart("n", Now) & "." & DatePart("s", Now)

                        Dim data As String = funzioni_comuni.getDataDb_con_orario(Rs("data_immatricolazione") & " 23:59:59")

                        Dim id_auto As String

                        If Rs("eseguire_insert") Then

                            sqlstr = "insert into veicoli (targa,id_modello,telaio,id_colore,id_proprietario,data_immatricolazione,escludi_ammortamento," &
                        "auto_in_leasing,data_inizio_leasing,data_fine_leasing,durata_mesi_leasing," &
                        "canone_mensile_leasing,km_compresi_leasing,franchigia_km_compresi_leasing,addizionale_100_extra_leasing," &
                        "rimborso_100_extra_leasing,id_ente_finanziatore," &
                        "data_inserimento,id_operatore_inserimento,venduta,in_vendita,disponibile_nolo,prezzo_acquisto,anticipo,riscatto," &
                        " inserito_da_foglio_excel) " &
                        "values('" & Rs("targa") & "','" & Rs("id_modello") & "','" & Rs("telaio") & "','" & Rs("id_colore") & "'," &
                        "'" & Rs("id_proprietario") & "',convert(datetime,'" & data & "',102),'" & EscludiAmmortamento & "'," &
                        auto_in_leasing & ",convert(datetime," & data_inizio_leasing & ",102),convert(datetime," & data_fine_leasing & ",102)," & mesi_leasing & "," &
                        canone_mensile & "," & km_compresi & "," & franchigia_km_compresi & "," & addizionale_100_extra & "," &
                        rimborso_100_extra & "," & id_ente_finanziatore & "," &
                        "convert(datetime,getDate(),102),'" & Request.Cookies("SicilyRentCar")("idUtente") & "','0','0','0'," & prezzo_acquisto & "," & anticipo & "," & riscatto & "," &
                        "'1')"

                            CmdSalvataggio.CommandText = sqlstr


                            CmdSalvataggio.ExecuteNonQuery()
                            CmdSalvataggio.CommandText = "SELECT @@IDENTITY FROM veicoli"

                            id_auto = CmdSalvataggio.ExecuteScalar

                            sqlstr = "insert into movimenti_targa (num_riferimento,id_veicolo,data_registrazione," &
                        "id_tipo_movimento,id_stazione_rientro, data_rientro, id_operatore, movimento_attivo) " &
                                            "values('" & id_auto & "','" & id_auto & "',getDate(),'" & Costanti.IdImmatricolazione & "','1',getDate(),'" & Request.Cookies("SicilyRentCar")("idUtente") & "','0')"

                            CmdSalvataggio = New Data.SqlClient.SqlCommand(sqlstr, DbcSalvataggio)
                            CmdSalvataggio.ExecuteNonQuery()

                            'SALVATAGGIO DELLE FATTURE (SE SPECIFICATE) ------------------------------------------------------------------
                            'I FATTURA ACQUISTO -------
                            If (Rs("numero_prima_fattura") & "") <> "" Then
                                sqlstr = "INSERT INTO fatture_acquisto_veicoli (id_veicolo,data_acquisto,num_acquisto,imponibile_acquisto,tipo,operatore_inserimento,data_inserimento) VALUES (" _
                                & "'" & id_auto & "',convert(datetime,'" & funzioni_comuni.getDataDb_senza_orario(Rs("data_prima_fattura")) & "',102)" _
                                & ",'" & Replace(Rs("numero_prima_fattura"), "'", "''") & "','" & Replace(Rs("importo_prima_fattura"), ",", ".") & "'" _
                                & ",'FATTURA','" & Request.Cookies("SicilyRentCar")("idUtente") & "',convert(datetime,getDate(),102))"
                                CmdSalvataggio = New Data.SqlClient.SqlCommand(sqlstr, DbcSalvataggio)
                                CmdSalvataggio.ExecuteNonQuery()
                            End If
                            'II FATTURA ACQUISTO -------
                            If (Rs("numero_seconda_fattura") & "") <> "" Then
                                sqlstr = "INSERT INTO fatture_acquisto_veicoli (id_veicolo,data_acquisto,num_acquisto,imponibile_acquisto,tipo,operatore_inserimento,data_inserimento) VALUES (" _
                                & "'" & id_auto & "',convert(datetime,'" & funzioni_comuni.getDataDb_senza_orario(Rs("data_seconda_fattura")) & "',102)" _
                                & ",'" & Replace(Rs("numero_seconda_fattura"), "'", "''") & "','" & Replace(Rs("importo_seconda_fattura"), ",", ".") & "'" _
                                & ",'FATTURA','" & Request.Cookies("SicilyRentCar")("idUtente") & "',convert(datetime,getDate(),102))"
                                CmdSalvataggio = New Data.SqlClient.SqlCommand(sqlstr, DbcSalvataggio)
                                CmdSalvataggio.ExecuteNonQuery()
                            End If
                            'I NOTA DI CREDITO ACQUISTO -------
                            If (Rs("numero_prima_nc") & "") <> "" Then
                                sqlstr = "INSERT INTO fatture_acquisto_veicoli (id_veicolo,data_acquisto,num_acquisto,imponibile_acquisto,tipo,operatore_inserimento,data_inserimento) VALUES (" _
                                & "'" & id_auto & "',convert(datetime,'" & funzioni_comuni.getDataDb_senza_orario(Rs("data_prima_nc")) & "',102)" _
                                & ",'" & Replace(Rs("numero_prima_nc"), "'", "''") & "','" & Replace(CDbl(Rs("importo_prima_nc")) * -1, ",", ".") & "'" _
                                & ",'NOTA','" & Request.Cookies("SicilyRentCar")("idUtente") & "',convert(datetime,getDate(),102))"
                                CmdSalvataggio = New Data.SqlClient.SqlCommand(sqlstr, DbcSalvataggio)
                                CmdSalvataggio.ExecuteNonQuery()
                            End If
                        ElseIf Rs("eseguire_update") Then
                            'ESEGUO UN UPDATE PERCHE' MI SONO ACCORTO CHE UNA TARGA GIA' ESISTE MA E' STATA INSERITA DA MASCHERA -
                            'NON VALORIZZO disponibile_nolo in quanto DA MASCHERA POTREBBE ESSERE GIA' STATO SALVATA L'IMMISSIONE IN PARCO
                            'PER CUI SOVRASCRIVEREI IL VALORE TRUE CORRENTE.

                            CmdSalvataggio.CommandText = "SELECT id FROM veicoli WHERE targa='" & Rs("targa") & "'"

                            id_auto = CmdSalvataggio.ExecuteScalar
                            sqlstr = "UPDATE veicoli SET id_modello='" & Rs("id_modello") & "'," &
                        "telaio='" & Rs("telaio") & "', id_colore='" & Rs("id_colore") & "', id_proprietario='" & Rs("id_proprietario") & "'," &
                        "data_immatricolazione=convert(datetime,'" & data & "',102),escludi_ammortamento='" & EscludiAmmortamento & "'," &
                        "auto_in_leasing=" & auto_in_leasing & ",data_inizio_leasing=convert(datetime," & data_inizio_leasing & ",102)," &
                        "data_fine_leasing=convert(datetime," & data_fine_leasing & ",102), durata_mesi_leasing=" & mesi_leasing & "," &
                        "canone_mensile_leasing=" & canone_mensile & ",km_compresi_leasing=" & km_compresi & "," &
                        "franchigia_km_compresi_leasing=" & franchigia_km_compresi & ",addizionale_100_extra_leasing=" & addizionale_100_extra & "," &
                        "rimborso_100_extra_leasing=" & rimborso_100_extra & "," &
                        "data_inserimento=convert(datetime,getDate(),102), id_operatore_inserimento='" & Request.Cookies("SicilyRentCar")("idUtente") & "', " &
                        "inserito_da_foglio_excel='1' " &
                        "WHERE id='" & id_auto & "'"

                            CmdSalvataggio.CommandText = sqlstr

                            CmdSalvataggio.ExecuteNonQuery()
                        End If

                        'Inserimento Riga movimentazione Immatricolazione


                        CampoModelloNonPresente = CampoModelloNonPresente + 1
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

                    Libreria.genUserMsgBox(Me, "Tutti i dati sono stati caricati correttamente")
                End If
                'Response.Write("Campi Obbl KO " & CampiObbligatoriKO & "<br>")
                'Response.Write("Campi Targa già P " & CampoTargaPresente & "<br>")
                'Response.Write("Campi Telaio già P " & CampoTelaioPresente & "<br>")
                'Response.Write("Campi Modello non P " & CampoModelloNonPresente & "<br>")
                'Else
                '    lblErrore2.Text = "Il file scelto non è corretto."
                'End If
            Else
                'Response.Write("OUT")
                'Response.End()
                lblErrore2.Text = "Scegliere un file."
            End If

        Catch ex As Exception
            Response.Write("error CaricaFoglioExcel : " & ex.Message & "<br/>" & sqlstr)
        End Try


    End Sub

    Protected Sub btnImportaFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportaFile.Click
        CaricaFoglioExcel()
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

    Protected Sub btnEnte_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEnte.Click
        Session("nome_file") = nome_file.Text
        Response.Redirect("tabelle.aspx?val=ente&prov=ImportMassivo")
    End Sub
End Class
