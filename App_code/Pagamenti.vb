Public Class Pagamenti
    Dim funzioni As New funzioni_comuni

    'FUNZIONI VARIE-------------------------------------------------------------------------------------------------------------------------
    Public Function getListPreautorizzazioni(ByVal num_prenotazione As String, ByVal num_rds As String, ByVal num_multa As String, ByVal num_contratto As String) As String()
        'LA FUNZIONE RESTITUISCE TUTTE LE PREAUTORIZZAZIONI APERTE PER I DOCUMENTI PASSATI COME ARGOMENTO - E' POSSIBILE PASSARE PIU'
        'ARGOMENTI, AD ESEMPIO PER OTTENERE TUTTE LE PREAUTORIZZAZIONI APERTE DA CONTRATTO E DALLA PRENOTAZIONE DA CUI DERIVA IL CONTRATTO
        'Dim num_documento As String
        'Dim campo_documento As String

        Dim condizione As String = "("

        If num_prenotazione <> "" Then
            condizione = condizione & " N_PREN_RIF='" & num_prenotazione & "'"
            'num_documento = num_prenotazione
            'campo_documento = "N_PREN_RIF"
        End If

        If num_contratto <> "" Then
            If condizione <> "(" Then
                condizione = condizione & " OR N_CONTRATTO_RIF='" & num_contratto & "'"
            Else
                condizione = condizione & " N_CONTRATTO_RIF='" & num_contratto & "'"
            End If
            'num_documento = num_contratto
            'campo_documento = "N_CONTRATTO_RIF"
        End If
        If num_rds <> "" Then
            If condizione <> "(" Then
                condizione = condizione & " OR N_RDS_RIF='" & num_rds & "'"
            Else
                condizione = condizione & " N_RDS_RIF='" & num_rds & "'"
            End If
            'num_documento = num_rds
            'campo_documento = "N_RDS_RIF"
        End If

        If num_multa <> "" Then
            If condizione <> "(" Then
                condizione = condizione & " OR N_MULTA_RIF='" & num_rds & "'"
            Else
                condizione = condizione & " N_MULTA_RIF='" & num_rds & "'"
            End If
            'num_contratto = num_multa
            'campo_documento = "N_MULTA_RIF"
        End If
        condizione = condizione & ")"

        HttpContext.Current.Trace.Write("Condizione -------------------->>>>>>>>>>>>>>> " & condizione)

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim id_ente As String = ""
        Dim id_acquire As String = ""
        Dim id_circuito As String = ""

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT NR_PREAUT, PER_IMPORTO, DATA_OPERAZIONE FROM pagamenti_extra WITH(NOLOCK) WHERE " & condizione & " AND id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Richiesta & "' AND preaut_aperta='1' AND operazione_stornata='0'", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Dim preautorizzazioni(50) As String
        Dim i As Integer = 0

        Do While Rs.Read()
            Dim preaut As String = Rs("NR_PREAUT") & ""
            Dim importo As String = Libreria.myFormatta(Rs("PER_IMPORTO"), "0.00") & ""
            Dim data_preaut As String = Libreria.myFormatta(Rs("DATA_OPERAZIONE"), "dd/MM/yyyy").PadRight(10)
            preautorizzazioni(i) = preaut.PadRight(15) & "| " & data_preaut & " |" & importo.PadLeft(10)
            i = i + 1
        Loop
        preautorizzazioni(i) = "0"

        getListPreautorizzazioni = preautorizzazioni

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Private Function getGiorniPreautorizzazione(ByVal id_enti_acquires_circuiti As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT giorni_preautorizzazione FROM POS_enti_acquires_circuiti WITH(NOLOCK) WHERE id='" & id_enti_acquires_circuiti & "'", Dbc)

        getGiorniPreautorizzazione = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Private Function getIdModPag(ByVal id_enti_acquires_circuiti As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim id_ente As String = ""
        Dim id_acquire As String = ""
        Dim id_circuito As String = ""

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_ente_proprietario, id_acquire, id_circuito, giorni_preautorizzazione FROM POS_enti_acquires_circuiti WITH(NOLOCK) WHERE id='" & id_enti_acquires_circuiti & "'", Dbc)

        'HttpContext.Current.Trace.Write("SELECT id_ente_proprietario, id_acquire, id_circuito, giorni_preautorizzazione FROM POS_enti_acquires_circuiti WITH(NOLOCK) WHERE id='" & id_enti_acquires_circuiti & "'")

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()
        Rs.Read()

        id_ente = Rs("id_ente_proprietario") & ""
        id_acquire = Rs("id_acquire") & ""
        id_circuito = Rs("id_circuito") & ""

        Dbc.Close()
        Dbc.Open()

        If id_ente <> "" And id_circuito <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT ID_ModPag FROM MOD_PAG WITH(NOLOCK) WHERE id_ente_ares='" & id_ente & "' AND id_circuito_ares='" & id_circuito & "'", Dbc)
        ElseIf id_ente <> "" And id_acquire <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT ID_ModPag FROM MOD_PAG WITH(NOLOCK) WHERE id_ente_ares='" & id_ente & "' AND id_acquire_ares='" & id_acquire & "'", Dbc)
        End If

        getIdModPag = Cmd.ExecuteScalar & ""

        If getIdModPag <> "" Then
            getIdModPag = "'" & getIdModPag & "'"
        Else
            getIdModPag = "NULL"
        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function
    '---------------------------------------------------------------------------------------------------------------------------------------
    'REGISTRAZIONE OPERAZIONI---------------------------------------------------------------------------------------------------------------

    Public Sub registra_storno(ByVal tr As classi_pagamento.TransazioneStorno, ByVal num_prenotazione As String, ByVal num_rds As String, ByVal num_multa As String, ByVal num_contratto As String, ByVal provenienza_x_data As String, ByVal id_stazione As String, ByVal id_utente As String, ByVal nome_utente As String)
        'Dim Str As String = tr.DataTransazione & "<br />" & _
        '        tr.TerminalID & "<br />" & _
        '        tr.AcquireID & "<br />" & _
        '        tr.ActionCode & "<br />" & _
        '        tr.IDFunzione & "<br />" & _
        '        tr.IDFunzioneStornata & "<br />" & _
        '        tr.IDRecord & "<br />" & _
        '        tr.OperationNumber & "<br />" & _
        '        tr.STAN & "<br />" & _
        '        tr.TerminalID & "<br />" & _
        '        tr.TipoCarta & "<br />" & _s
        '        tr.STANStornato & "<br />"

        'HttpContext.Current.Trace.Write(Str)
        Dim sqlStr As String = ""
        Try
            'A SECONDA DEI PARAMETRI REGISTRO PER UNA PRENOTAZIONE, CONTRATTO, RDS, MULTA-------------------------------------------------------
            Dim num_documento As String
            Dim campo_documento As String
            If num_prenotazione <> "" Then
                num_documento = num_prenotazione
                campo_documento = "N_PREN_RIF"
            ElseIf num_contratto <> "" Then
                num_documento = num_contratto
                campo_documento = "N_CONTRATTO_RIF"
            ElseIf num_rds <> "" Then
                num_documento = num_rds
                campo_documento = "N_RDS_RIF"
            ElseIf num_multa <> "" Then
                num_contratto = num_multa
                campo_documento = "N_MULTA_RIF"
            End If
            '-----------------------------------------------------------------------------------------------------------------------------------

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            'SELEZIONE ID_TIP_PAG --------------------------------------------------------------------------------------------------------------
            'IN BASE ALL'OPERAZIONE STORNATA SELEZIONO L'ID DI TIP PAG CORRETTO
            Dim id_tip_pag As String = ""

            If tr.IDFunzioneStornata = enum_tipo_pagamento_ares.Chiusura Then
                id_tip_pag = Costanti.id_STORNO_chiusura_pos_p1000
            ElseIf tr.IDFunzioneStornata = enum_tipo_pagamento_ares.Integrazione Then
                id_tip_pag = Costanti.id_STORNO_integrazione_pos_p1000
            ElseIf tr.IDFunzioneStornata = enum_tipo_pagamento_ares.Richiesta Then
                id_tip_pag = Costanti.id_STORNO_preautorizzazione_pos_p1000
            ElseIf tr.IDFunzioneStornata = enum_tipo_pagamento_ares.Rimborso Then
                id_tip_pag = Costanti.id_STORNO_rimborso_pos_p1000
            ElseIf tr.IDFunzioneStornata = enum_tipo_pagamento_ares.Vendita Then
                id_tip_pag = Costanti.id_STORNO_vendita_pos_p1000
            End If
            '-----------------------------------------------------------------------------------------------------------------------------------
            'SELEZIONO IL NUMERO DI CASSA-------------------------------------------------------------------------------------------------------
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT cassa FROM pos WITH(NOLOCK) WHERE terminal_id='" & tr.TerminalID & "'", Dbc)
            Dim cassa As String = Cmd.ExecuteScalar & ""
            '-----------------------------------------------------------------------------------------------------------------------------------

            'CALCOLO DELLA DATA DI SCADENZA PREAUTORIZZAZIONE-----------------------------------------------------------------------------------
            Dim data_odierna As String = funzioni.getDataDb_senza_orario(Now(), provenienza_x_data)

            Dim data_operazione As String = funzioni.getDataDb_con_orario(tr.DataTransazione, provenienza_x_data)

            'Dim scadenza_preautorizzazione As String = funzioni.getDataDb_con_orario(tr.DataTransazione.AddDays(CDbl(giorni_preautorizzazione)), provenienza_x_data)
            '-----------------------------------------------------------------------------------------------------------------------------------
            'CONTATORE--------------------------------------------------------------------------------------------------------------------------
            Dim Nr_Contratto As String = Contatori.getContatore_pagamenti_extra(id_stazione)
            '-----------------------------------------------------------------------------------------------------------------------------------

            sqlStr = "INSERT INTO pagamenti_extra (Nr_Contratto,data,tipo_docu,id_tippag, id_pos_funzioni_ares, id_funzione_stornata_ares,STAN_stornato, importo, ID_STAZIONE, " &
            "cassa, nr_aut," &
            "tipsegno,busta_pc,note,id_operatore_ares, DATACRE, UTECRE," &
            campo_documento & ", NR_BATCH,TERMINAL_ID, DATA_OPERAZIONE,preaut_aperta, CONTABILIZZATO,TRANSATION_TYPE, CARD_TYPE, operazione_stornata, acquire_id, action_code)" &
            " VALUES " &
            "('" & Nr_Contratto & "',convert(datetime,'" & data_odierna & "',102),NULL,'" & id_tip_pag & "','" & tr.IDFunzione & "','" & tr.IDFunzioneStornata & "','" & tr.STANStornato & "','0','" & id_stazione & "'," &
            "'" & cassa & "',NULL," &
            "'1',NULL,NULL,'" & id_utente & "',convert(datetime,GetDate(),102),'" & Right(nome_utente.Replace("'", "''"), 15) & "'," &
            "'" & num_documento & "','" & tr.STAN & "','" & tr.TerminalID & "',convert(datetime,'" & data_operazione & "',102),'1'," &
            "'0','" & tr.TipoCarta & "','2','0','" & tr.AcquireID & "','" & tr.ActionCode & "')"

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            'SETTO COME STORNATA LA RIGA CORRISPONDENTE ALLO STAN E TERMINAL ID 
            sqlStr = "UPDATE PAGAMENTI_EXTRA SET operazione_stornata='1' WHERE terminal_id='" & tr.TerminalID & "' AND NR_BATCH='" & tr.STANStornato & "'"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error pagamenti registra_storno : <br/>" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Sub

    Public Sub registra_rimborso(ByVal tr As classi_pagamento.TransazioneRimborso, ByVal num_prenotazione As String, ByVal num_rds As String, ByVal num_multa As String, ByVal num_contratto As String, ByVal provenienza_x_data As String, ByVal id_stazione As String, ByVal id_utente As String, ByVal nome_utente As String)
        'Dim Str As String = tr.DataTransazione & "<br />" & _
        '        tr.IDAcquireCircuito & "<br />" & _
        '        tr.IDEnte & "<br />" & _
        '        tr.IDFunzione & "<br />" & _
        '        tr.IDRecord & "<br />" & _
        '        tr.Importo & "<br />" & _
        '        tr.NumeroAutorizzazione & "<br />" & _
        '        tr.NumeroCarta & "<br />" & _
        '        tr.OperationNumber & "<br />" & _
        '        tr.STAN & "<br />" & _
        '        tr.TerminalID & "<br />" & _
        '        tr.TipoCarta

        Dim sqla As String = ""

        Try
            'A SECONDA DEI PARAMETRI REGISTRO PER UNA PRENOTAZIONE, CONTRATTO, RDS, MULTA-------------------------------------------------------
            Dim num_documento As String
            Dim campo_documento As String
            If num_prenotazione <> "" Then
                num_documento = num_prenotazione
                campo_documento = "N_PREN_RIF"
            ElseIf num_contratto <> "" Then
                num_documento = num_contratto
                campo_documento = "N_CONTRATTO_RIF"
            ElseIf num_rds <> "" Then
                num_documento = num_rds
                campo_documento = "N_RDS_RIF"
            ElseIf num_multa <> "" Then
                num_contratto = num_multa
                campo_documento = "N_MULTA_RIF"
            End If
            '-----------------------------------------------------------------------------------------------------------------------------------

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            'SELEZIONO ID_MODPAG ---------------------------------------------------------------------------------------------------------------
            Dim idCircuito As Integer = tr.IDAcquireCircuito    '26.04.2022
            Dim id_mod_pag As String = getIdModPag(tr.IDAcquireCircuito)
            '-----------------------------------------------------------------------------------------------------------------------------------
            'SELEZIONO IL NUMERO DI CASSA-------------------------------------------------------------------------------------------------------
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT cassa FROM pos WITH(NOLOCK) WHERE terminal_id='" & tr.TerminalID & "'", Dbc)
            Dim cassa As String = Cmd.ExecuteScalar & ""
            '-----------------------------------------------------------------------------------------------------------------------------------

            'CALCOLO DELLA DATA DI SCADENZA PREAUTORIZZAZIONE-----------------------------------------------------------------------------------
            Dim data_odierna As String = funzioni.getDataDb_senza_orario(Now(), provenienza_x_data)

            Dim data_operazione As String = funzioni.getDataDb_con_orario(tr.DataTransazione, provenienza_x_data)
            '-----------------------------------------------------------------------------------------------0------------------------------------
            '-----------------------------------------------------------------------------------------------------------------------------------
            'CONTATORE--------------------------------------------------------------------------------------------------------------------------
            Dim Nr_Contratto As String = Contatori.getContatore_pagamenti_extra(id_stazione)
            '-----------------------------------------------------------------------------------------------------------------------------------

            Dim titolo As String
            If tr.NumeroCarta = "" Then
                titolo = "NULL"
            Else
                With New security
                    titolo = "'" & .encryptString(tr.NumeroCarta) & "'"
                End With
            End If

            sqla = "INSERT INTO pagamenti_extra (Nr_Contratto,data,tipo_docu,id_ModPag,id_tippag, id_pos_funzioni_ares, importo, ID_STAZIONE, " &
        "cassa, titolo, intestatario, nr_aut," &
        "tipsegno,busta_pc,note,id_operatore_ares, DATACRE, UTECRE," &
        campo_documento & ", NR_BATCH,PER_IMPORTO,TERMINAL_ID, DATA_OPERAZIONE, CONTABILIZZATO,TRANSATION_TYPE, CARD_TYPE, operazione_stornata, acquire_id, action_code)" &
        " VALUES " &
        "('" & Nr_Contratto & "',convert(datetime,'" & data_odierna & "',102),NULL," & id_mod_pag & ",'" & Costanti.id_rimborso_pos_p1000 & "','" & tr.IDFunzione & "','0','" & id_stazione & "'," &
        "'" & cassa & "'," & titolo & ",'" & tr.Intestatario & "','" & tr.NumeroAutorizzazione & "'," &
        "'0',NULL,NULL,'" & id_utente & "',convert(datetime,GetDate(),102),'" & Right(nome_utente.Replace("'", "''"), 15) & "'," &
        "'" & num_documento & "','" & tr.STAN & "','" & Replace(tr.Importo, ",", ".") & "','" & tr.TerminalID & "',convert(datetime,'" & data_operazione & "',102)," &
        "'0','" & tr.TipoCarta & "','2','0','" & tr.AcquireID & "','" & tr.ActionCode & "')"

            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing


            'se id_modpag=0 invia email di segnalazione 26.04.2022
            Try
                If id_mod_pag = "0" Then
                    Try
                        Dim sendm As New sendmailcls
                        Dim oo As String = "Registra Rimborso - MODPAG0=0 - contratto nr." & Nr_Contratto & " " & data_odierna
                        Dim tt As String = Date.Now.ToString & "<br/>Registra Rimborso - MODPAG0=0 - contratto nr." & Nr_Contratto & " " & data_odierna & "<br/>id stazione: " & id_stazione & "<br/>id Utente: " & id_utente
                        tt += "<br/>IDAcquireCircuito: " & idCircuito.ToString

                        Dim x As Integer = sendm.sendmail("dimatteo@xinformatica.it", "ARES", "dimatteo@xinformatica.it", oo, tt, True, "")

                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception

            End Try





        Catch ex As Exception
            HttpContext.Current.Response.Write("error pagamenti registra_rimborso: <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try





    End Sub

    Public Sub registra_chiusura(ByVal tr As classi_pagamento.TransazioneChiusura, ByVal num_prenotazione As String, ByVal num_rds As String, ByVal num_multa As String, ByVal num_contratto As String, ByVal provenienza_x_data As String, ByVal id_stazione As String, ByVal id_utente As String, ByVal nome_utente As String)

        Dim sqla As String = ""
        Try
            'A SECONDA DEI PARAMETRI REGISTRO PER UNA PRENOTAZIONE, CONTRATTO, RDS, MULTA-------------------------------------------------------
            Dim num_documento As String
            Dim campo_documento As String
            If num_prenotazione <> "" Then
                num_documento = num_prenotazione
                campo_documento = "N_PREN_RIF"
            ElseIf num_contratto <> "" Then
                num_documento = num_contratto
                campo_documento = "N_CONTRATTO_RIF"
            ElseIf num_rds <> "" Then
                num_documento = num_rds
                campo_documento = "N_RDS_RIF"
            ElseIf num_multa <> "" Then
                num_contratto = num_multa
                campo_documento = "N_MULTA_RIF"
            End If
            '-----------------------------------------------------------------------------------------------------------------------------------
            'HttpContext.Current.Trace.Write("PREC" & tr.NumeroPreautorizzazione)
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            'MOD PAG: E' LO STESSO DELLA RICHIESTA DI PREAUTORIZZAZIONE ------------------------------------------------------------------------
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_modPag FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE " & campo_documento & "='" & num_documento & "' AND id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Richiesta & "' AND NR_PREAUT='" & tr.NumeroPreautorizzazione & "' AND operazione_stornata='0'", Dbc)
            Dim id_modPag As String = Cmd.ExecuteScalar & ""
            If id_modPag = "" Then
                'SOLO IN CASO DI ERRORE PER PERMETTERE LA REGISTRAZIONE DEL PAGAMENTO - ID MOD PAG DEVE ESSERE TROVATO PER FORZA DALLA PREAUTORIZZAZIONE
                id_modPag = "NULL"
            Else
                id_modPag = "'" & id_modPag & "'"
            End If
            '-----------------------------------------------------------------------------------------------------------------------------------
            'SELEZIONO IL NUMERO DI CASSA-------------------------------------------------------------------------------------------------------
            Cmd = New Data.SqlClient.SqlCommand("SELECT cassa FROM pos WITH(NOLOCK) WHERE terminal_id='" & tr.TerminalID & "'", Dbc)

            Dim cassa As String = Cmd.ExecuteScalar & ""
            '-----------------------------------------------------------------------------------------------------------------------------------
            'CONTATORE--------------------------------------------------------------------------------------------------------------------------
            Dim Nr_Contratto As String = Contatori.getContatore_pagamenti_extra(id_stazione)
            '-----------------------------------------------------------------------------------------------------------------------------------

            Dim data_odierna As String = funzioni_comuni.getDataDb_senza_orario(Now(), provenienza_x_data)
            Dim data_operazione As String = funzioni_comuni.getDataDb_con_orario(tr.DataTransazione, provenienza_x_data)

            sqla = "INSERT INTO pagamenti_extra (Nr_Contratto,data,tipo_docu,ID_ModPag,id_tippag, id_pos_funzioni_ares, importo, ID_STAZIONE, " &
            "cassa," &
            "tipsegno,busta_pc,note,id_operatore_ares, DATACRE, UTECRE," &
            campo_documento & ", NR_BATCH,PER_IMPORTO,NR_PREAUT,TERMINAL_ID, CONTABILIZZATO,TRANSATION_TYPE, CARD_TYPE, operazione_stornata, acquire_id, action_code, DATA_OPERAZIONE)" &
            " VALUES " &
            "('" & Nr_Contratto & "',convert(datetime,'" & data_odierna & "',102),NULL," & id_modPag & ",'" & Costanti.id_chiusura_preautorizzazione_pos_p1000 & "','" & tr.IDFunzione & "','0','" & id_stazione & "'," &
            "'" & cassa & "'," &
            "'1',NULL,NULL,'" & id_utente & "',convert(datetime,GetDate(),102),'" & Right(nome_utente.Replace("'", "''"), 15) & "'," &
            "'" & num_documento & "','" & tr.STAN & "','" & Replace(tr.Importo, ",", ".") & "','" & tr.NumeroPreautorizzazione & "','" & tr.TerminalID & "'," &
            "'0','" & tr.TipoCarta & "','2','0','" & tr.AcquireID & "','" & tr.ActionCode & "',convert(datetime,'" & data_operazione & "',102))"
            'HttpContext.Current.Trace.Write(sqlStr)

            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()

            'A QUESTO PUNTO FACCIO IN MODO CHE LA PREAUTORIZZAZIONE E LE SUE EVENTUALI INTEGRAZIONI IN PAGAMENTI_EXTRA RISULTI CHIUSA -----------------------------------------
            sqla = "UPDATE PAGAMENTI_EXTRA SET preaut_aperta='0', AUTORIZZ_EVASA_IL = GetDate() WHERE " & campo_documento & "='" & num_documento & "' AND (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Richiesta & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Integrazione & "') AND NR_PREAUT='" & tr.NumeroPreautorizzazione & "' AND operazione_stornata='0'"
            HttpContext.Current.Trace.Write(sqla)

            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteScalar()
            '---------------------------------------------------------------------------------------------------------------------------------
            'SE IL CONTRATTO E' NELLO STATO 4 (CHIUSO DA INCASSARE) SUCCESSIVAMANTE AD UNA CHIUSURA PREAUTORIZZAZIONE PASSA NELLO STATO 8 (DA FATTURARE)
            If num_contratto <> "" Then
                sqla = "SELECT status FROM contratti WITH(NOLOCK) WHERE num_contratto='" & num_contratto & "' AND attivo='1'"
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                Dim cnt_status As String = Cmd.ExecuteScalar & ""
                If cnt_status = "4" Then
                    sqla = "UPDATE contratti SET status='8' WHERE num_contratto='" & num_contratto & "' AND attivo='1'"
                    Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                    Cmd.ExecuteNonQuery()
                End If
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            'se id_modpag=0 invia email di segnalazione 26.04.2022
            Try
                If id_modPag = "0" Then
                    Try
                        Dim sendm As New sendmailcls
                        Dim oo As String = "Registra Chiusura - MODPAG0=0 - contratto nr." & Nr_Contratto & " " & data_odierna
                        Dim tt As String = Date.Now.ToString & "<br/>Registra Chiusura - MODPAG0=0 - contratto nr." & num_contratto & " " & data_odierna & "<br/>id stazione: " & id_stazione & "<br/>id Utente: " & id_utente
                        tt += "<br/>MOD PAG: E' LO STESSO DELLA RICHIESTA DI PREAUTORIZZAZIONE: (" & id_modPag & ")"

                        Dim x As Integer = sendm.sendmail("dimatteo@xinformatica.it", "ARES", "dimatteo@xinformatica.it", oo, tt, True, "")

                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception

            End Try








        Catch ex As Exception
            HttpContext.Current.Response.Write("error pagamenti registra_chiusura: <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Public Shared Function get_id_mod_pag_web(compagnia As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim sqlStr As String = "SELECT TOP 1 id_mod_pag FROM POS_mod_pag_web WITH(NOLOCK)" & _
            " WHERE CCOMPAGNIA = '" & compagnia & "'" & _
            " OR CCOMPAGNIA = 'DEFAULT'" & _
            " ORDER BY id DESC"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        get_id_mod_pag_web = Cmd.ExecuteScalar & ""
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function get_CCOMPAGNIA_web(ByVal id_pagamenti_extra As String) As String
        'UTILIZZATO DENTRO PRENOTAZIONI DURANTE IL SALVATAGGIO DELLA FATTURA A SEGUITO DI UN PREPAGAMENTO SU ARES.
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "SELECT ID_ModPag FROM PAGAMENTI_EXTRA WHERE Nr_contratto='" & id_pagamenti_extra & "'"
        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Dim id_mod_pag As String = Cmd.ExecuteScalar & ""

        sqlStr = "SELECT TOP 1 ISNULL(CCOMPAGNIA, 'DEFAULT') FROM POS_mod_pag_web WHERE id_mod_pag='" & id_mod_pag & "'"

        get_CCOMPAGNIA_web = Cmd.ExecuteScalar & ""

        If get_CCOMPAGNIA_web = "" Then
            get_CCOMPAGNIA_web = "DEFAULT"
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Function registra_vendita(ByVal tr As classi_pagamento.TransazioneVendita, ByVal num_prenotazione As String, ByVal num_rds As String, ByVal num_multa As String, ByVal num_contratto As String, ByVal provenienza_x_data As String, ByVal id_stazione As String, ByVal id_utente As String, ByVal nome_utente As String) As Integer
        'Dim Str As String = tr.DataTransazione & "<br />" & _
        '        tr.IDAcquireCircuito & "<br />" & _
        '        tr.IDEnte & "<br />" & _
        '        tr.IDFunzione & "<br />" & _
        '        tr.IDRecord & "<br />" & _
        '        tr.Importo & "<br />" & _
        '        tr.NumeroAutorizzazione & "<br />" & _
        '        tr.NumeroCarta & "<br />" & _
        '        tr.OperationNumber & "<br />" & _
        '        tr.ScadenzaCartaAnno & "<br />" & _
        '        tr.ScadenzaCartaMese & "<br />" & _
        '        tr.STAN & "<br />" & _
        '        tr.TerminalID & "<br />" & _
        '        tr.TipoCarta & "<br />"
        'HttpContext.Current.Trace.Write(Str)
        'A SECONDA DEI PARAMETRI REGISTRO PER UNA PRENOTAZIONE, CONTRATTO, RDS, MULTA-------------------------------------------------------
        Dim sqlstr As String = ""
        Try
            Dim num_documento As String
            Dim campo_documento As String
            If num_prenotazione <> "" Then
                num_documento = num_prenotazione
                campo_documento = "N_PREN_RIF"
            ElseIf num_contratto <> "" Then
                num_documento = num_contratto
                campo_documento = "N_CONTRATTO_RIF"
            ElseIf num_rds <> "" Then
                num_documento = num_rds
                campo_documento = "N_RDS_RIF"
            ElseIf num_multa <> "" Then
                num_documento = num_multa
                campo_documento = "N_MULTA_RIF"
            End If
            '-----------------------------------------------------------------------------------------------------------------------------------


            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As Data.SqlClient.SqlCommand

            'SELEZIONO ID_MODPAG ---------------------------------------------------------------------------------------------------------------
            Dim id_mod_pag As String
            Dim tip_pag As String
            Dim cassa As String = ""
            Dim NumeroTentativiPOS As Integer = 1
            Dim IdCircuito As Integer = 0 '26.04.2022

            If tr.TipoCarta = "----WEB----" Then
                ' ATTENZIONE: il valore tr.TipoCarta è un valore di comodo 
                ' riciclato per passare il dato alla funzione "registra_vendita" 
                ' utilizzata anche nella procedura ribaltamento

                tr.TipoCarta = ""
                id_mod_pag = get_id_mod_pag_web(tr.OperationNumber)
                tip_pag = Costanti.id_pagamento_web_p1000
                NumeroTentativiPOS = tr.IDEnte
            Else
                IdCircuito = tr.IDAcquireCircuito
                id_mod_pag = getIdModPag(tr.IDAcquireCircuito)

                tip_pag = Costanti.id_incasso_pos_p1000

                sqlStr = "SELECT cassa FROM pos WITH(NOLOCK) WHERE terminal_id='" & tr.TerminalID & "'"

                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                cassa = Cmd.ExecuteScalar & ""
                Cmd.Dispose()
                Cmd = Nothing
            End If

            '-----------------------------------------------------------------------------------------------------------------------------------
            'SELEZIONO IL NUMERO DI CASSA-------------------------------------------------------------------------------------------------------

            'CONTATORE--------------------------------------------------------------------------------------------------------------------------
            Dim Nr_Contratto As String = Contatori.getContatore_pagamenti_extra(id_stazione)
            '-----------------------------------------------------------------------------------------------------------------------------------

            Dim data_odierna As String = funzioni.getDataDb_senza_orario(Now(), provenienza_x_data)
            Dim data_operazione As String = funzioni.getDataDb_con_orario(tr.DataTransazione, provenienza_x_data)

            Dim titolo As String
            If tr.NumeroCarta = "" Then
                titolo = "NULL"
            Else
                With New security
                    titolo = "'" & .encryptString(tr.NumeroCarta) & "'"
                End With
            End If

            sqlstr = "INSERT INTO pagamenti_extra (Nr_Contratto,data,tipo_docu,id_ModPag,id_tippag, id_pos_funzioni_ares, importo, ID_STAZIONE, " &
            "cassa, titolo, intestatario, scadenza, nr_aut," &
            "tipsegno,busta_pc,note,id_operatore_ares, DATACRE, UTECRE," &
            campo_documento & ", NR_BATCH,PER_IMPORTO,TERMINAL_ID, CONTABILIZZATO,TRANSATION_TYPE, CARD_TYPE, operazione_stornata, acquire_id, action_code, DATA_OPERAZIONE,TentativiPOS)" &
            " VALUES " &
            "('" & Nr_Contratto & "',convert(datetime,'" & data_odierna & "',102),NULL," & id_mod_pag & ",'" & tip_pag & "','" & tr.IDFunzione & "','0','" & id_stazione & "'," &
            "'" & cassa & "'," & titolo & ",'" & tr.Intestatario.Replace("'", "''") & "','" & tr.ScadenzaCartaMese & "/" & tr.ScadenzaCartaAnno & "','" & tr.NumeroAutorizzazione & "'," &
            "'1',NULL,NULL,'" & id_utente & "',convert(datetime,GetDate(),102),'" & Right(nome_utente.Replace("'", "''"), 15) & "'," &
            "'" & num_documento & "','" & tr.STAN & "','" & Replace(tr.Importo, ",", ".") & "','" & tr.TerminalID & "'," &
            "'0','" & tr.TipoCarta & "','2','0','" & tr.AcquireID & "','" & tr.ActionCode & "',convert(datetime,'" & data_operazione & "',102)," & NumeroTentativiPOS & ")"

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            'SE IL CONTRATTO E' NELLO STATO 4 (CHIUSO DA INCASSARE) SUCCESSIVAMANTE AD UNA VENDITA (RARO MA PUO' AVVENIRE)
            ' PASSA NELLO STATO 8 (DA FATTURARE)
            If num_contratto <> "" Then
                sqlStr = "SELECT status FROM contratti WITH(NOLOCK) WHERE num_contratto='" & num_contratto & "' AND attivo='1'"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dim cnt_status As String = Cmd.ExecuteScalar & ""
                If cnt_status = "4" Then
                    sqlStr = "UPDATE contratti SET status='8' WHERE num_contratto='" & num_contratto & "' AND attivo='1'"
                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.ExecuteNonQuery()
                End If
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing


            'se id_modpag=0 invia email di segnalazione 26.04.2022
            Try
                If id_mod_pag = "0" Then
                    Try
                        Dim sendm As New sendmailcls
                        Dim oo As String = "Registra Vendita - MODPAG0=0 - contratto nr." & num_contratto & " " & data_odierna
                        Dim tt As String = Date.Now.ToString & "<br/>Registra Vendita - MODPAG0=0 - contratto nr." & num_contratto & " " & data_odierna & "<br/>id stazione: " & id_stazione & "<br/>id Utente: " & id_utente
                        tt += "<br/>tr.IDAcquireCircuito: " & IdCircuito.ToString

                        Dim x As Integer = sendm.sendmail("dimatteo@xinformatica.it", "ARES", "dimatteo@xinformatica.it", oo, tt, True, "")

                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception

            End Try




            Return Nr_Contratto


        Catch ex As Exception
            HttpContext.Current.Response.Write("error pagamenti registra_vendita: <br/>" & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try





    End Function



    Public Sub registra_preautorizzazione(ByVal tr As classi_pagamento.TransazionePreautorizzazione, ByVal num_prenotazione As String, ByVal num_rds As String, ByVal num_multa As String, ByVal num_contratto As String, ByVal provenienza_x_data As String, ByVal id_stazione As String, ByVal id_utente As String, ByVal nome_utente As String)
        'Dim Str As String = tr.DataTransazione & "<br />" & _
        '        tr.IDAcquireCircuito & "<br />" & _
        '        tr.IDEnte & "<br />" & _
        '        tr.IDFunzione & "<br />" & _
        '        tr.IDRecord & "<br />" & _
        '        tr.Importo & "<br />" & _
        '        tr.NumeroAutorizzazione & "<br />" & _
        '        tr.NumeroCarta & "<br />" & _
        '        tr.NumeroPreautorizzazione & "<br />" & _
        '        tr.OperationNumber & "<br />" & _
        '        tr.ScadenzaCartaAnno & "<br />" & _
        '        tr.ScadenzaCartaMese & "<br />" & _
        '        tr.STAN & "<br />" & _
        '        tr.TerminalID & "<br />" & _
        '        tr.ActionCode & "<br />" & _
        '        tr.AcquireID & "<br />"

        'HttpContext.Current.Trace.Write(Str)

        Dim sqlstr As String = ""

        Try
            'A SECONDA DEI PARAMETRI REGISTRO PER UNA PRENOTAZIONE, CONTRATTO, RDS, MULTA-------------------------------------------------------
            Dim num_documento As String
            Dim campo_documento As String
            If num_prenotazione <> "" Then
                num_documento = num_prenotazione
                campo_documento = "N_PREN_RIF"
            ElseIf num_contratto <> "" Then
                num_documento = num_contratto
                campo_documento = "N_CONTRATTO_RIF"
            ElseIf num_rds <> "" Then
                num_documento = num_rds
                campo_documento = "N_RDS_RIF"
            ElseIf num_multa <> "" Then
                num_contratto = num_multa
                campo_documento = "N_MULTA_RIF"
            End If
            '-----------------------------------------------------------------------------------------------------------------------------------

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            'SELEZIONO ID_MODPAG ---------------------------------------------------------------------------------------------------------------
            Dim giorni_preautorizzazione As String = getGiorniPreautorizzazione(tr.IDAcquireCircuito)
            Dim id_mod_pag As String = getIdModPag(tr.IDAcquireCircuito)
            '-----------------------------------------------------------------------------------------------------------------------------------
            'SELEZIONO IL NUMERO DI CASSA-------------------------------------------------------------------------------------------------------
            Dim sqlStr1 As String = "SELECT cassa FROM pos WITH(NOLOCK) WHERE terminal_id='" & tr.TerminalID & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr1, Dbc)
            Dim cassa As String = Cmd.ExecuteScalar & ""
            HttpContext.Current.Trace.Write("cassa: " & cassa & " - " & sqlStr1)
            '-----------------------------------------------------------------------------------------------------------------------------------

            'CALCOLO DELLA DATA DI SCADENZA PREAUTORIZZAZIONE-----------------------------------------------------------------------------------
            Dim data_odierna As String = funzioni_comuni.getDataDb_senza_orario(Now(), provenienza_x_data)

            Dim data_operazione As String = funzioni_comuni.getDataDb_con_orario(tr.DataTransazione, provenienza_x_data)

            Dim scadenza_preautorizzazione As String = funzioni_comuni.getDataDb_con_orario(tr.DataTransazione.AddDays(CDbl(giorni_preautorizzazione)), provenienza_x_data)
            '-----------------------------------------------------------------------------------------------------------------------------------
            'CONTATORE--------------------------------------------------------------------------------------------------------------------------
            Dim Nr_Contratto As String = Contatori.getContatore_pagamenti_extra(id_stazione)
            '-----------------------------------------------------------------------------------------------------------------------------------

            Dim titolo As String
            If tr.NumeroCarta = "" Then
                titolo = "NULL"
            Else
                With New security
                    titolo = "'" & .encryptString(tr.NumeroCarta) & "'"
                End With
            End If

            sqlstr = "INSERT INTO pagamenti_extra (Nr_Contratto,data,tipo_docu,id_ModPag,id_tippag, id_pos_funzioni_ares, importo, ID_STAZIONE, " &
            "cassa, titolo, intestatario, scadenza, nr_aut," &
            "tipsegno,busta_pc,note,id_operatore_ares, DATACRE, UTECRE," &
            campo_documento & ", NR_BATCH,PER_IMPORTO,TERMINAL_ID, DATA_OPERAZIONE, NR_PREAUT,preaut_aperta, scadenza_preaut, CONTABILIZZATO,TRANSATION_TYPE, CARD_TYPE, operazione_stornata, acquire_id, action_code)" &
            " VALUES " &
            "('" & Nr_Contratto & "',convert(datetime,'" & data_odierna & "',102),NULL," & id_mod_pag & ",'" & Costanti.id_richiesta_preautorizzazione_pos_p1000 & "','" & tr.IDFunzione & "','0','" & id_stazione & "'," &
            "'" & cassa & "'," & titolo & ",'" & tr.Intestatario & "','" & tr.ScadenzaCartaMese & "/" & tr.ScadenzaCartaAnno & "','" & tr.NumeroAutorizzazione & "'," &
            "'1',NULL,NULL,'" & id_utente & "',convert(datetime,GetDate(),102),'" & Right(nome_utente.Replace("'", "''"), 15) & "'," &
            "'" & num_documento & "','" & tr.STAN & "','" & Replace(tr.Importo, ",", ".") & "','" & tr.TerminalID & "',convert(datetime,'" & data_operazione & "',102),'" & tr.NumeroPreautorizzazione & "','1'," &
            "convert(datetime,'" & scadenza_preautorizzazione & "',102),'0','" & tr.TipoCarta & "','2','0','" & tr.AcquireID & "','" & tr.ActionCode & "')"

            Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error registra_preautorizzazione  : <br/>" & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try

    End Sub

    Public Sub registra_integrazione(ByVal tr As classi_pagamento.TransazioneIntegrazione, ByVal num_prenotazione As String, ByVal num_rds As String, ByVal num_multa As String, ByVal num_contratto As String, ByVal provenienza_x_data As String, ByVal id_stazione As String, ByVal id_utente As String, ByVal nome_utente As String)
        'Dim Str As String = tr.DataTransazione & "<br />" & _
        '        tr.IDFunzione & "<br />" & _
        '        tr.IDRecord & "<br />" & _
        '        tr.Importo & "<br />" & _
        '        tr.NumeroAutorizzazione & "<br />" & _
        '        tr.NumeroPreautorizzazione & "<br />" & _
        '        tr.OperationNumber & "<br />" & _
        '        tr.STAN & "<br />" & _
        '        tr.TerminalID & "<br />" & _
        '        tr.TipoCarta & "<br />"
        'HttpContext.Current.Trace.Write(Str)

        'A SECONDA DEI PARAMETRI REGISTRO PER UNA PRENOTAZIONE, CONTRATTO, RDS, MULTA-------------------------------------------------------
        Dim num_documento As String
        Dim campo_documento As String
        If num_prenotazione <> "" Then
            num_documento = num_prenotazione
            campo_documento = "N_PREN_RIF"
        ElseIf num_contratto <> "" Then
            num_documento = num_contratto
            campo_documento = "N_CONTRATTO_RIF"
        ElseIf num_rds <> "" Then
            num_documento = num_rds
            campo_documento = "N_RDS_RIF"
        ElseIf num_multa <> "" Then
            num_contratto = num_multa
            campo_documento = "N_MULTA_RIF"
        End If
        '-----------------------------------------------------------------------------------------------------------------------------------
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        'SELEZIONO ID_MODPAG ---------------------------------------------------------------------------------------------------------------
        'IN QUESTO CASO NON SI UTILIZZA (???)
        '-----------------------------------------------------------------------------------------------------------------------------------
        'SELEZIONO IL NUMERO DI CASSA-------------------------------------------------------------------------------------------------------
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT cassa FROM pos WITH(NOLOCK) WHERE terminal_id='" & tr.TerminalID & "'", Dbc)
        Dim cassa As String = Cmd.ExecuteScalar & ""
        '-----------------------------------------------------------------------------------------------------------------------------------
        'CONTATORE--------------------------------------------------------------------------------------------------------------------------
        Dim Nr_Contratto As String = Contatori.getContatore_pagamenti_extra(id_stazione)
        '-----------------------------------------------------------------------------------------------------------------------------------
        Dim data_odierna As String = funzioni_comuni.getDataDb_senza_orario(Now(), provenienza_x_data)
        Dim data_operazione As String = funzioni_comuni.getDataDb_con_orario(tr.DataTransazione, provenienza_x_data)

        Dim sqlStr As String = "INSERT INTO pagamenti_extra (Nr_Contratto,data,tipo_docu,id_tippag, id_pos_funzioni_ares, importo, ID_STAZIONE, " &
        "cassa, intestatario, nr_aut," &
        "tipsegno,busta_pc,note,id_operatore_ares, DATACRE, UTECRE," &
        campo_documento & ", NR_BATCH,PER_IMPORTO,TERMINAL_ID, NR_PREAUT, CONTABILIZZATO,TRANSATION_TYPE, CARD_TYPE, preaut_aperta, operazione_stornata, acquire_id, action_code, DATA_OPERAZIONE)" &
        " VALUES " &
        "('" & Nr_Contratto & "','" & data_odierna & "',NULL,'" & Costanti.id_integrazione_preautorizzazione_pos_p1000 & "','" & tr.IDFunzione & "','0','" & id_stazione & "'," &
        "'" & cassa & "',NULL,'" & tr.NumeroAutorizzazione & "'," &
        "'1',NULL,NULL,'" & id_utente & "',convert(datetime,GetDate(),102),'" & Right(nome_utente.Replace("'", "''"), 15) & "'," &
        "'" & num_documento & "','" & tr.STAN & "','" & Replace(tr.Importo, ",", ".") & "','" & tr.TerminalID & "','" & tr.NumeroPreautorizzazione & "'," &
        "'0','" & tr.TipoCarta & "','2','1','0','" & tr.AcquireID & "','" & tr.ActionCode & "',convert(datetime,'" & data_operazione & "',102))"

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Public Shared Function is_complimentary(ByVal num_pren As String, ByVal num_cnt As String) As Boolean
        Dim sqlStr As String

        If num_pren <> "" And num_cnt <> "" Then
            sqlStr = "SELECT TOP 1 ID_CTR FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE (N_PREN_RIF='" & num_pren & "' OR N_CONTRATTO_RIF='" & num_cnt & "') AND ID_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Complimentary & "'"
        ElseIf num_pren <> "" Then
            sqlStr = "SELECT TOP 1 ID_CTR FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE N_PREN_RIF='" & num_pren & "' AND ID_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Complimentary & "'"
        ElseIf num_cnt <> "" Then
            sqlStr = "SELECT TOP 1 ID_CTR FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE N_CONTRATTO_RIF='" & num_cnt & "' AND ID_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Complimentary & "'"
        End If


        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()

                Dim test As String = Cmd.ExecuteScalar & ""

                If test <> "" Then
                    Return True
                Else
                    Return False
                End If

            End Using
        End Using
    End Function

    Public Shared Function is_full_credit(ByVal num_pren As String, ByVal num_cnt As String) As Boolean
        Dim sqlStr As String

        If num_pren <> "" And num_cnt <> "" Then
            sqlStr = "SELECT TOP 1 ID_CTR FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE (N_PREN_RIF='" & num_pren & "' OR N_CONTRATTO_RIF='" & num_cnt & "') AND ID_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Full_Credit & "'"
        ElseIf num_pren <> "" Then
            sqlStr = "SELECT TOP 1 ID_CTR FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE N_PREN_RIF='" & num_pren & "' AND ID_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Full_Credit & "'"
        ElseIf num_cnt <> "" Then
            sqlStr = "SELECT TOP 1 ID_CTR FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE N_CONTRATTO_RIF='" & num_cnt & "' AND ID_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Full_Credit & "'"
        End If


        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()

                Dim test As String = Cmd.ExecuteScalar & ""

                If test <> "" Then
                    Return True
                Else
                    Return False
                End If

            End Using
        End Using
    End Function

End Class


Public Class PAGAMENTI_EXTRA
    Inherits ITabellaDB

    Protected m_Nr_Contratto As Integer?
    Protected m_Data As DateTime?
    Protected m_ID_CTR As Integer
    Protected m_TIPO_DOCU As Integer?
    Protected m_ID_ModPag As Long?
    Protected m_ID_TIPPAG As Integer?
    Protected m_id_pos_funzioni_ares As Integer?
    Protected m_id_funzione_stornata_ares As Integer?
    Protected m_STAN_stornato As String
    Protected m_Importo As Double?
    Protected m_ID_STAZIONE As Integer?
    Protected m_CASSA As Integer?
    Protected m_Titolo As String
    Protected m_Intestatario As String
    Protected m_Scadenza As String
    Protected m_nr_aut As String
    Protected m_TIPSEGNO As Double?
    Protected m_BUSTA_PC As Integer?
    Protected m_NOTE As String
    Protected m_ID_OPERATORE As Integer?
    Protected m_id_operatore_ares As Integer?
    Protected m_DATAMOD As DateTime?
    Protected m_UTEMOD As String
    Protected m_TIPOMOD As String
    Protected m_DATACRE As DateTime?
    Protected m_UTECRE As String
    Protected m_N_CONTRATTO_RIF As Integer?
    Protected m_N_RDS_RIF As Integer?
    Protected m_N_MULTA_RIF As String
    Protected m_STA_IX As Integer?
    Protected m_N_PREN_RIF As Integer?
    Protected m_NR_BATCH As String
    Protected m_PER_IMPORTO As Double?
    Protected m_STAMPATOIL As DateTime?
    Protected m_DATA_FATT_RIF As DateTime?
    Protected m_NUM_FATT_RIF As Integer?
    Protected m_TERMINAL_ID As String
    Protected m_ESITO_TRANSAZIONE As String
    Protected m_DATA_OPERAZIONE As DateTime?
    Protected m_AUTORIZZ_EVASA_IL As DateTime?
    Protected m_NR_PRATICA As String
    Protected m_PAG_RIF As Integer?
    Protected m_NR_PREAUT As String
    Protected m_preaut_aperta As Boolean?
    Protected m_operazione_stornata As Boolean? = False
    Protected m_scadenza_preaut As DateTime?
    Protected m_CONTABILIZZATO As Boolean?
    Protected m_TRANSATION_TYPE As String
    Protected m_CARD_TYPE As String
    Protected m_acquire_id As String
    Protected m_action_code As String
    Protected m_TentativiPOS As Integer?

    Public Property Nr_Contratto() As Integer?
        Get
            Return m_Nr_Contratto
        End Get
        Set(ByVal value As Integer?)
            m_Nr_Contratto = value
        End Set
    End Property
    Public Property Data() As DateTime?
        Get
            Return m_Data
        End Get
        Set(ByVal value As DateTime?)
            m_Data = value
        End Set
    End Property
    Public Property ID_CTR() As Integer
        Get
            Return m_ID_CTR
        End Get
        Set(ByVal value As Integer)
            m_ID_CTR = value
        End Set
    End Property
    Public Property TIPO_DOCU() As Integer?
        Get
            Return m_TIPO_DOCU
        End Get
        Set(ByVal value As Integer?)
            m_TIPO_DOCU = value
        End Set
    End Property
    Public Property ID_ModPag() As Long?
        Get
            Return m_ID_ModPag
        End Get
        Set(ByVal value As Long?)
            m_ID_ModPag = value
        End Set
    End Property
    Public Property ID_TIPPAG() As Integer?
        Get
            Return m_ID_TIPPAG
        End Get
        Set(ByVal value As Integer?)
            m_ID_TIPPAG = value
        End Set
    End Property
    Public Property id_pos_funzioni_ares() As Integer?
        Get
            Return m_id_pos_funzioni_ares
        End Get
        Set(ByVal value As Integer?)
            m_id_pos_funzioni_ares = value
        End Set
    End Property
    Public Property id_funzione_stornata_ares() As Integer?
        Get
            Return m_id_funzione_stornata_ares
        End Get
        Set(ByVal value As Integer?)
            m_id_funzione_stornata_ares = value
        End Set
    End Property
    Public Property STAN_stornato() As String
        Get
            Return m_STAN_stornato
        End Get
        Set(ByVal value As String)
            m_STAN_stornato = value
        End Set
    End Property
    Public Property Importo() As Double?
        Get
            Return m_Importo
        End Get
        Set(ByVal value As Double?)
            m_Importo = value
        End Set
    End Property
    Public Property ID_STAZIONE() As Integer?
        Get
            Return m_ID_STAZIONE
        End Get
        Set(ByVal value As Integer?)
            m_ID_STAZIONE = value
        End Set
    End Property
    Public Property CASSA() As Integer?
        Get
            Return m_CASSA
        End Get
        Set(ByVal value As Integer?)
            m_CASSA = value
        End Set
    End Property
    Public Property Titolo() As String
        Get
            Return m_Titolo
        End Get
        Set(ByVal value As String)
            m_Titolo = value
        End Set
    End Property
    Public Property Intestatario() As String
        Get
            Return m_Intestatario
        End Get
        Set(ByVal value As String)
            m_Intestatario = value
        End Set
    End Property
    Public Property Scadenza() As String
        Get
            Return m_Scadenza
        End Get
        Set(ByVal value As String)
            m_Scadenza = value
        End Set
    End Property
    Public Property anno_scadenza() As Integer
        Get
            If m_Scadenza Is Nothing OrElse m_Scadenza = "" Then
                Return 0
            End If
            If m_Scadenza.Length = 5 Then
                Return Integer.Parse(m_Scadenza.Substring(3, 2))
            Else
                Return Integer.Parse(m_Scadenza.Substring(2, 2))
            End If
        End Get
        Set(ByVal value As Integer)
            If m_Scadenza Is Nothing OrElse m_Scadenza = "" Then
                m_Scadenza = Libreria.myFormatta(value, "00") & "/" & "00"
            Else
                m_Scadenza = Libreria.myFormatta(value, "00") & "/" & Libreria.myFormatta(mese_scadenza, "00")
            End If
        End Set
    End Property
    Public Property mese_scadenza() As Integer
        Get
            If m_Scadenza Is Nothing OrElse m_Scadenza = "" Then
                Return 0
            End If
            If m_Scadenza.Length = 5 Then
                Return Integer.Parse(m_Scadenza.Substring(0, 2))
            Else
                Return Integer.Parse("0" & m_Scadenza.Substring(0, 1))
            End If

        End Get
        Set(ByVal value As Integer)
            If m_Scadenza Is Nothing OrElse m_Scadenza = "" Then
                m_Scadenza = "00" & "/" & Libreria.myFormatta(value, "00")
            Else
                m_Scadenza = Libreria.myFormatta(anno_scadenza, "00") & "/" & Libreria.myFormatta(value, "00")
            End If
        End Set
    End Property
    Public Property nr_aut() As String
        Get
            Return m_nr_aut
        End Get
        Set(ByVal value As String)
            m_nr_aut = value
        End Set
    End Property
    Public Property TIPSEGNO() As Double?
        Get
            Return m_TIPSEGNO
        End Get
        Set(ByVal value As Double?)
            m_TIPSEGNO = value
        End Set
    End Property
    Public Property BUSTA_PC() As Integer?
        Get
            Return m_BUSTA_PC
        End Get
        Set(ByVal value As Integer?)
            m_BUSTA_PC = value
        End Set
    End Property
    Public Property NOTE() As String
        Get
            Return m_NOTE
        End Get
        Set(ByVal value As String)
            m_NOTE = value
        End Set
    End Property
    Public Property ID_OPERATORE() As Integer?
        Get
            Return m_ID_OPERATORE
        End Get
        Set(ByVal value As Integer?)
            m_ID_OPERATORE = value
        End Set
    End Property
    Public ReadOnly Property id_operatore_ares() As Integer?
        Get
            Return m_id_operatore_ares
        End Get
    End Property
    Public Property DATAMOD() As DateTime?
        Get
            Return m_DATAMOD
        End Get
        Set(ByVal value As DateTime?)
            m_DATAMOD = value
        End Set
    End Property
    Public Property UTEMOD() As String
        Get
            Return m_UTEMOD
        End Get
        Set(ByVal value As String)
            m_UTEMOD = value
        End Set
    End Property
    Public Property TIPOMOD() As String
        Get
            Return m_TIPOMOD
        End Get
        Set(ByVal value As String)
            m_TIPOMOD = value
        End Set
    End Property
    Public ReadOnly Property DATACRE() As DateTime?
        Get
            Return m_DATACRE
        End Get
    End Property
    Public ReadOnly Property UTECRE() As String
        Get
            Return m_UTECRE
        End Get
    End Property
    Public Property N_CONTRATTO_RIF() As Integer?
        Get
            Return m_N_CONTRATTO_RIF
        End Get
        Set(ByVal value As Integer?)
            m_N_CONTRATTO_RIF = value
        End Set
    End Property
    Public Property N_RDS_RIF() As Integer?
        Get
            Return m_N_RDS_RIF
        End Get
        Set(ByVal value As Integer?)
            m_N_RDS_RIF = value
        End Set
    End Property
    Public Property N_MULTA_RIF() As String
        Get
            Return m_N_MULTA_RIF
        End Get
        Set(ByVal value As String)
            m_N_MULTA_RIF = value
        End Set
    End Property
    Public Property STA_IX() As Integer?
        Get
            Return m_STA_IX
        End Get
        Set(ByVal value As Integer?)
            m_STA_IX = value
        End Set
    End Property
    Public Property N_PREN_RIF() As Integer?
        Get
            Return m_N_PREN_RIF
        End Get
        Set(ByVal value As Integer?)
            m_N_PREN_RIF = value
        End Set
    End Property
    Public Property NR_BATCH() As String
        Get
            Return m_NR_BATCH
        End Get
        Set(ByVal value As String)
            m_NR_BATCH = value
        End Set
    End Property
    Public Property PER_IMPORTO() As Double?
        Get
            Return m_PER_IMPORTO
        End Get
        Set(ByVal value As Double?)
            m_PER_IMPORTO = value
        End Set
    End Property
    Public Property STAMPATOIL() As DateTime?
        Get
            Return m_STAMPATOIL
        End Get
        Set(ByVal value As DateTime?)
            m_STAMPATOIL = value
        End Set
    End Property
    Public Property DATA_FATT_RIF() As DateTime?
        Get
            Return m_DATA_FATT_RIF
        End Get
        Set(ByVal value As DateTime?)
            m_DATA_FATT_RIF = value
        End Set
    End Property
    Public Property NUM_FATT_RIF() As Integer?
        Get
            Return m_NUM_FATT_RIF
        End Get
        Set(ByVal value As Integer?)
            m_NUM_FATT_RIF = value
        End Set
    End Property
    Public Property TERMINAL_ID() As String
        Get
            Return m_TERMINAL_ID
        End Get
        Set(ByVal value As String)
            m_TERMINAL_ID = value
        End Set
    End Property
    Public Property ESITO_TRANSAZIONE() As String
        Get
            Return m_ESITO_TRANSAZIONE
        End Get
        Set(ByVal value As String)
            m_ESITO_TRANSAZIONE = value
        End Set
    End Property
    Public Property DATA_OPERAZIONE() As DateTime?
        Get
            Return m_DATA_OPERAZIONE
        End Get
        Set(ByVal value As DateTime?)
            m_DATA_OPERAZIONE = value
        End Set
    End Property
    Public Property AUTORIZZ_EVASA_IL() As DateTime?
        Get
            Return m_AUTORIZZ_EVASA_IL
        End Get
        Set(ByVal value As DateTime?)
            m_AUTORIZZ_EVASA_IL = value
        End Set
    End Property
    Public Property NR_PRATICA() As String
        Get
            Return m_NR_PRATICA
        End Get
        Set(ByVal value As String)
            m_NR_PRATICA = value
        End Set
    End Property
    Public Property PAG_RIF() As Integer?
        Get
            Return m_PAG_RIF
        End Get
        Set(ByVal value As Integer?)
            m_PAG_RIF = value
        End Set
    End Property
    Public Property NR_PREAUT() As String
        Get
            Return m_NR_PREAUT
        End Get
        Set(ByVal value As String)
            m_NR_PREAUT = value
        End Set
    End Property
    Public Property preaut_aperta() As Boolean?
        Get
            Return m_preaut_aperta
        End Get
        Set(ByVal value As Boolean?)
            m_preaut_aperta = value
        End Set
    End Property
    Public Property operazione_stornata() As Boolean?
        Get
            Return m_operazione_stornata
        End Get
        Set(ByVal value As Boolean?)
            m_operazione_stornata = value
        End Set
    End Property
    Public Property scadenza_preaut() As DateTime?
        Get
            Return m_scadenza_preaut
        End Get
        Set(ByVal value As DateTime?)
            m_scadenza_preaut = value
        End Set
    End Property
    Public Property CONTABILIZZATO() As Boolean?
        Get
            Return m_CONTABILIZZATO
        End Get
        Set(ByVal value As Boolean?)
            m_CONTABILIZZATO = value
        End Set
    End Property
    Public Property TRANSATION_TYPE() As String
        Get
            Return m_TRANSATION_TYPE
        End Get
        Set(ByVal value As String)
            m_TRANSATION_TYPE = value
        End Set
    End Property
    Public Property CARD_TYPE() As String
        Get
            Return m_CARD_TYPE
        End Get
        Set(ByVal value As String)
            m_CARD_TYPE = value
        End Set
    End Property
    Public Property acquire_id() As String
        Get
            Return m_acquire_id
        End Get
        Set(ByVal value As String)
            m_acquire_id = value
        End Set
    End Property
    Public Property action_code() As String
        Get
            Return m_action_code
        End Get
        Set(ByVal value As String)
            m_action_code = value
        End Set
    End Property
    Public Property TentativiPOS() As Integer?
        Get
            Return m_TentativiPOS
        End Get
        Set(ByVal value As Integer?)
            m_TentativiPOS = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        If ID_STAZIONE Is Nothing Then
            Err.Raise(1001, Me, "Non è possibile salvare il pagamento se la stazione non è valorizzata")
        End If

        Dim sqlStr As String = "INSERT INTO PAGAMENTI_EXTRA (Nr_Contratto,Data,TIPO_DOCU,ID_ModPag,ID_TIPPAG,id_pos_funzioni_ares,id_funzione_stornata_ares,STAN_stornato,Importo,ID_STAZIONE,CASSA,Titolo,Intestatario,Scadenza,nr_aut,TIPSEGNO,BUSTA_PC,NOTE,ID_OPERATORE,id_operatore_ares,DATAMOD,UTEMOD,TIPOMOD,DATACRE,UTECRE,N_CONTRATTO_RIF,N_RDS_RIF,N_MULTA_RIF,STA_IX,N_PREN_RIF,NR_BATCH,PER_IMPORTO,STAMPATOIL,DATA_FATT_RIF,NUM_FATT_RIF,TERMINAL_ID,ESITO_TRANSAZIONE,DATA_OPERAZIONE,AUTORIZZ_EVASA_IL,NR_PRATICA,PAG_RIF,NR_PREAUT,preaut_aperta,operazione_stornata,scadenza_preaut,CONTABILIZZATO,TRANSATION_TYPE,CARD_TYPE,acquire_id,action_code,TentativiPOS)" & _
            " VALUES (@Nr_Contratto,@Data,@TIPO_DOCU,@ID_ModPag,@ID_TIPPAG,@id_pos_funzioni_ares,@id_funzione_stornata_ares,@STAN_stornato,@Importo,@ID_STAZIONE,@CASSA,@Titolo,@Intestatario,@Scadenza,@nr_aut,@TIPSEGNO,@BUSTA_PC,@NOTE,@ID_OPERATORE,@id_operatore_ares,@DATAMOD,@UTEMOD,@TIPOMOD,@DATACRE,@UTECRE,@N_CONTRATTO_RIF,@N_RDS_RIF,@N_MULTA_RIF,@STA_IX,@N_PREN_RIF,@NR_BATCH,@PER_IMPORTO,@STAMPATOIL,@DATA_FATT_RIF,@NUM_FATT_RIF,@TERMINAL_ID,@ESITO_TRANSAZIONE,@DATA_OPERAZIONE,@AUTORIZZ_EVASA_IL,@NR_PRATICA,@PAG_RIF,@NR_PREAUT,@preaut_aperta,@operazione_stornata,@scadenza_preaut,@CONTABILIZZATO,@TRANSATION_TYPE,@CARD_TYPE,@acquire_id,@action_code,@TentativiPOS)"

        Nr_Contratto = Contatori.getContatore_pagamenti_extra(ID_STAZIONE)
        m_DATACRE = Now
        m_id_operatore_ares = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))
        m_UTECRE = Libreria.getNomeOperatoreDaId(m_id_operatore_ares)
        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@Nr_Contratto", System.Data.SqlDbType.Int, Nr_Contratto)
                    addParametro(Cmd, "@Data", System.Data.SqlDbType.DateTime, Data)
                    addParametro(Cmd, "@TIPO_DOCU", System.Data.SqlDbType.Int, TIPO_DOCU)
                    addParametro(Cmd, "@ID_ModPag", System.Data.SqlDbType.Int, ID_ModPag)
                    addParametro(Cmd, "@ID_TIPPAG", System.Data.SqlDbType.Int, ID_TIPPAG)
                    addParametro(Cmd, "@id_pos_funzioni_ares", System.Data.SqlDbType.Int, id_pos_funzioni_ares)
                    addParametro(Cmd, "@id_funzione_stornata_ares", System.Data.SqlDbType.Int, id_funzione_stornata_ares)
                    addParametro(Cmd, "@STAN_stornato", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(STAN_stornato, 20))
                    addParametro(Cmd, "@Importo", System.Data.SqlDbType.Money, Importo)
                    addParametro(Cmd, "@ID_STAZIONE", System.Data.SqlDbType.Int, ID_STAZIONE)
                    addParametro(Cmd, "@CASSA", System.Data.SqlDbType.Int, CASSA)

                    If Libreria.TrimSicuro(Titolo, 60) <> Nothing Then
                        Dim my_titolo As String

                        With New security
                            my_titolo = .encryptString(Libreria.TrimSicuro(Titolo, 50))
                        End With

                        addParametro(Cmd, "@Titolo", System.Data.SqlDbType.VarChar, my_titolo)
                    Else
                        addParametro(Cmd, "@Titolo", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(Titolo, 50))
                    End If

                    addParametro(Cmd, "@Intestatario", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(Intestatario, 50))
                    addParametro(Cmd, "@Scadenza", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(Scadenza, 5))
                    addParametro(Cmd, "@nr_aut", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(nr_aut, 8))
                    addParametro(Cmd, "@TIPSEGNO", System.Data.SqlDbType.SmallInt, TIPSEGNO)
                    addParametro(Cmd, "@BUSTA_PC", System.Data.SqlDbType.Int, BUSTA_PC)
                    addParametro(Cmd, "@NOTE", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(NOTE, 255))
                    addParametro(Cmd, "@ID_OPERATORE", System.Data.SqlDbType.Int, ID_OPERATORE)
                    addParametro(Cmd, "@id_operatore_ares", System.Data.SqlDbType.Int, id_operatore_ares)
                    addParametro(Cmd, "@DATAMOD", System.Data.SqlDbType.DateTime, DATAMOD)
                    addParametro(Cmd, "@UTEMOD", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(UTEMOD, 10))
                    addParametro(Cmd, "@TIPOMOD", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(TIPOMOD, 1))
                    addParametro(Cmd, "@DATACRE", System.Data.SqlDbType.DateTime, DATACRE)
                    addParametro(Cmd, "@UTECRE", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(UTECRE, 15))
                    addParametro(Cmd, "@N_CONTRATTO_RIF", System.Data.SqlDbType.Int, N_CONTRATTO_RIF)
                    addParametro(Cmd, "@N_RDS_RIF", System.Data.SqlDbType.Int, N_RDS_RIF)
                    addParametro(Cmd, "@N_MULTA_RIF", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(N_MULTA_RIF, 15))
                    addParametro(Cmd, "@STA_IX", System.Data.SqlDbType.Int, STA_IX)
                    addParametro(Cmd, "@N_PREN_RIF", System.Data.SqlDbType.Int, N_PREN_RIF)
                    addParametro(Cmd, "@NR_BATCH", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(NR_BATCH, 20))
                    addParametro(Cmd, "@PER_IMPORTO", System.Data.SqlDbType.Float, PER_IMPORTO)
                    addParametro(Cmd, "@STAMPATOIL", System.Data.SqlDbType.DateTime, STAMPATOIL)
                    addParametro(Cmd, "@DATA_FATT_RIF", System.Data.SqlDbType.DateTime, DATA_FATT_RIF)
                    addParametro(Cmd, "@NUM_FATT_RIF", System.Data.SqlDbType.Int, NUM_FATT_RIF)
                    addParametro(Cmd, "@TERMINAL_ID", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(TERMINAL_ID, 20))
                    addParametro(Cmd, "@ESITO_TRANSAZIONE", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(ESITO_TRANSAZIONE, 2))
                    addParametro(Cmd, "@DATA_OPERAZIONE", System.Data.SqlDbType.DateTime, DATA_OPERAZIONE)
                    addParametro(Cmd, "@AUTORIZZ_EVASA_IL", System.Data.SqlDbType.DateTime, AUTORIZZ_EVASA_IL)
                    addParametro(Cmd, "@NR_PRATICA", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(NR_PRATICA, 50))
                    addParametro(Cmd, "@PAG_RIF", System.Data.SqlDbType.Int, PAG_RIF)
                    addParametro(Cmd, "@NR_PREAUT", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(NR_PREAUT, 20))
                    addParametro(Cmd, "@preaut_aperta", System.Data.SqlDbType.Bit, preaut_aperta)
                    addParametro(Cmd, "@operazione_stornata", System.Data.SqlDbType.Bit, operazione_stornata)
                    addParametro(Cmd, "@scadenza_preaut", System.Data.SqlDbType.DateTime, scadenza_preaut)
                    addParametro(Cmd, "@CONTABILIZZATO", System.Data.SqlDbType.Bit, CONTABILIZZATO)
                    addParametro(Cmd, "@TRANSATION_TYPE", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(TRANSATION_TYPE, 3))
                    addParametro(Cmd, "@CARD_TYPE", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(CARD_TYPE, 1))
                    addParametro(Cmd, "@acquire_id", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(acquire_id, 20))
                    addParametro(Cmd, "@action_code", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(action_code, 10))
                    addParametro(Cmd, "@TentativiPOS", System.Data.SqlDbType.Int, TentativiPOS)

                    ' addParametro(Cmd, "@ID_CTR", System.Data.SqlDbType.Int, ID_CTR) campo auto increment

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()



                End Using
                'SE SI STA REGISTRANDO UN PAGAMENTO CONTATI O UN INCASSO TELEFONICO PER UN CONTRATTO IN STATO "DA INCASSARE" PASSO LO STATO
                'DEL CONTRATTO A "DA FATTURARE"
                If CStr(N_CONTRATTO_RIF & "") <> "" And (id_pos_funzioni_ares = enum_tipo_pagamento_ares.Pagamento_Contanti Or id_pos_funzioni_ares = enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso) Then
                    'SE IL CONTRATTO E' NELLO STATO 4 (CHIUSO DA INCASSARE) SUCCESSIVAMANTE AD UNA VENDITA (RARO MA PUO' AVVENIRE)
                    ' PASSA NELLO STATO 8 (DA FATTURARE)
                    Dim cnt_status As String
                    sqlStr = "SELECT status FROM contratti WITH(NOLOCK) WHERE num_contratto='" & N_CONTRATTO_RIF & "' AND attivo='1'"
                    HttpContext.Current.Trace.Write(sqlStr)
                    Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                        cnt_status = Cmd.ExecuteScalar & ""
                    End Using
                    If cnt_status = "4" Then
                        sqlStr = "UPDATE contratti SET status='8' WHERE num_contratto='" & N_CONTRATTO_RIF & "' AND attivo='1'"
                        Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                            Cmd.ExecuteNonQuery()
                        End Using
                    End If
                End If
                ' -------------------------------------------------------------------------
                'recupero l'id del nuopvo record comunque...
                sqlStr = "SELECT @@IDENTITY FROM PAGAMENTI_EXTRA"

                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    m_ID_CTR = Cmd.ExecuteScalar
                End Using
            End Using

        Catch ex As Exception
            HttpContext.Current.Response.Write("error pagamenti SalvaRecord : <br/>" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try




        Return m_ID_CTR
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As PAGAMENTI_EXTRA
        Dim mio_record As PAGAMENTI_EXTRA = New PAGAMENTI_EXTRA
        With mio_record
            .Nr_Contratto = getValueOrNohing(Rs("Nr_Contratto"))
            .Data = getValueOrNohing(Rs("Data"))
            .ID_CTR = getValueOrNohing(Rs("ID_CTR"))
            .TIPO_DOCU = getValueOrNohing(Rs("TIPO_DOCU"))
            If Rs("ID_ModPag") Is DBNull.Value Then
                .ID_ModPag = Nothing
            Else
                .ID_ModPag = Long.Parse(Rs("ID_ModPag"))
            End If
            .ID_TIPPAG = getValueOrNohing(Rs("ID_TIPPAG"))
            .id_pos_funzioni_ares = getValueOrNohing(Rs("id_pos_funzioni_ares"))
            .id_funzione_stornata_ares = getValueOrNohing(Rs("id_funzione_stornata_ares"))
            .STAN_stornato = getValueOrNohing(Rs("STAN_stornato"))
            .Importo = getDoubleOrNohing(Rs("Importo"))
            .ID_STAZIONE = getValueOrNohing(Rs("ID_STAZIONE"))
            .CASSA = getValueOrNohing(Rs("CASSA"))
            .Titolo = getValueOrNohing(Rs("Titolo"))
            .Intestatario = getValueOrNohing(Rs("Intestatario"))
            .Scadenza = getValueOrNohing(Rs("Scadenza"))
            .nr_aut = getValueOrNohing(Rs("nr_aut"))
            .TIPSEGNO = getDoubleOrNohing(Rs("TIPSEGNO"))
            .BUSTA_PC = getValueOrNohing(Rs("BUSTA_PC"))
            .NOTE = getValueOrNohing(Rs("NOTE"))
            .ID_OPERATORE = getValueOrNohing(Rs("ID_OPERATORE"))
            .m_id_operatore_ares = getValueOrNohing(Rs("id_operatore_ares"))
            .DATAMOD = getValueOrNohing(Rs("DATAMOD"))
            .UTEMOD = getValueOrNohing(Rs("UTEMOD"))
            .TIPOMOD = getValueOrNohing(Rs("TIPOMOD"))
            .m_DATACRE = getValueOrNohing(Rs("DATACRE"))
            .m_UTECRE = getValueOrNohing(Rs("UTECRE"))
            .N_CONTRATTO_RIF = getValueOrNohing(Rs("N_CONTRATTO_RIF"))
            .N_RDS_RIF = getValueOrNohing(Rs("N_RDS_RIF"))
            .N_MULTA_RIF = getValueOrNohing(Rs("N_MULTA_RIF"))
            .STA_IX = getValueOrNohing(Rs("STA_IX"))
            .N_PREN_RIF = getValueOrNohing(Rs("N_PREN_RIF"))
            .NR_BATCH = getValueOrNohing(Rs("NR_BATCH"))
            .PER_IMPORTO = getDoubleOrNohing(Rs("PER_IMPORTO"))
            .STAMPATOIL = getValueOrNohing(Rs("STAMPATOIL"))
            .DATA_FATT_RIF = getValueOrNohing(Rs("DATA_FATT_RIF"))
            .NUM_FATT_RIF = getValueOrNohing(Rs("NUM_FATT_RIF"))
            .TERMINAL_ID = getValueOrNohing(Rs("TERMINAL_ID"))
            .ESITO_TRANSAZIONE = getValueOrNohing(Rs("ESITO_TRANSAZIONE"))
            .DATA_OPERAZIONE = getValueOrNohing(Rs("DATA_OPERAZIONE"))
            .AUTORIZZ_EVASA_IL = getValueOrNohing(Rs("AUTORIZZ_EVASA_IL"))
            .NR_PRATICA = getValueOrNohing(Rs("NR_PRATICA"))
            .PAG_RIF = getValueOrNohing(Rs("PAG_RIF"))
            .NR_PREAUT = getValueOrNohing(Rs("NR_PREAUT"))
            .preaut_aperta = getValueOrNohing(Rs("preaut_aperta"))
            .operazione_stornata = getValueOrNohing(Rs("operazione_stornata"))
            .scadenza_preaut = getValueOrNohing(Rs("scadenza_preaut"))
            .CONTABILIZZATO = getValueOrNohing(Rs("CONTABILIZZATO"))
            .TRANSATION_TYPE = getValueOrNohing(Rs("TRANSATION_TYPE"))
            .CARD_TYPE = getValueOrNohing(Rs("CARD_TYPE"))
            .acquire_id = getValueOrNohing(Rs("acquire_id"))
            .action_code = getValueOrNohing(Rs("action_code"))
            .TentativiPOS = getValueOrNohing(Rs("TentativiPOS"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As PAGAMENTI_EXTRA
        Dim mio_record As PAGAMENTI_EXTRA = Nothing

        Dim sqlStr As String = "SELECT * FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE ID_CTR = " & id_record

        'HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    If Rs.Read Then
                        mio_record = FillRecord(Rs)
                    End If
                End Using
            End Using
        End Using

        Return mio_record
    End Function

    Public Shared Function getDepositiSuRa(ByVal num_contratto As Integer) As IList(Of PAGAMENTI_EXTRA)
        Dim miei_depositi As IList(Of PAGAMENTI_EXTRA) = New List(Of PAGAMENTI_EXTRA)
        Dim mio_record As PAGAMENTI_EXTRA = Nothing

        Dim sqlStr As String = "SELECT * FROM PAGAMENTI_EXTRA WITH(NOLOCK)" &
            " WHERE N_CONTRATTO_RIF = " & num_contratto &
            " AND ID_TIPPAG = " & enum_tipo_pagamento_p1000.DE_DEPOSITO_SU_RA &
            " AND preaut_aperta = 1" &
            " AND operazione_stornata = 0"

        HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    If Rs.Read Then
                        mio_record = FillRecord(Rs)
                        If mio_record IsNot Nothing Then
                            miei_depositi.Add(mio_record)
                        End If
                    End If
                End Using
            End Using
        End Using

        Return miei_depositi
    End Function

    Public Shared Function ChiudiDepositiApertiSuRA(ByVal num_contratto As Integer) As Boolean
        ChiudiDepositiApertiSuRA = False

        Dim sqlStr As String = "UPDATE PAGAMENTI_EXTRA SET" &
            " preaut_aperta = 0," &
            " AUTORIZZ_EVASA_IL = GetDate()" &
            " WHERE N_CONTRATTO_RIF = " & num_contratto &
            " AND ID_TIPPAG = " & enum_tipo_pagamento_p1000.DE_DEPOSITO_SU_RA &
            " AND preaut_aperta = 1" &
            " AND operazione_stornata = 0"

        HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Cmd.ExecuteNonQuery()
                ChiudiDepositiApertiSuRA = True
            End Using
        End Using
    End Function

    Public Function AggiornaRecord() As Boolean ' non so se deve essere consentito!!!!!
        AggiornaRecord = False

        Dim sqlStr As String = "UPDATE PAGAMENTI_EXTRA SET" &
            " Nr_Contratto = @Nr_Contratto," &
            " Data = @Data," &
            " TIPO_DOCU = @TIPO_DOCU," &
            " ID_ModPag = @ID_ModPag," &
            " ID_TIPPAG = @ID_TIPPAG," &
            " id_pos_funzioni_ares = @id_pos_funzioni_ares," &
            " id_funzione_stornata_ares = @id_funzione_stornata_ares," &
            " STAN_stornato = @STAN_stornato," &
            " Importo = @Importo," &
            " ID_STAZIONE = @ID_STAZIONE," &
            " CASSA = @CASSA," &
            " Titolo = @Titolo," &
            " Intestatario = @Intestatario," &
            " Scadenza = @Scadenza," &
            " nr_aut = @nr_aut," &
            " TIPSEGNO = @TIPSEGNO," &
            " BUSTA_PC = @BUSTA_PC," &
            " NOTE = @NOTE," &
            " ID_OPERATORE = @ID_OPERATORE," &
            " id_operatore_ares = @id_operatore_ares," &
            " DATAMOD = @DATAMOD," &
            " UTEMOD = @UTEMOD," &
            " TIPOMOD = @TIPOMOD," &
            " DATACRE = @DATACRE," &
            " UTECRE = @UTECRE," &
            " N_CONTRATTO_RIF = @N_CONTRATTO_RIF," &
            " N_RDS_RIF = @N_RDS_RIF," &
            " N_MULTA_RIF = @N_MULTA_RIF," &
            " STA_IX = @STA_IX," &
            " N_PREN_RIF = @N_PREN_RIF," &
            " NR_BATCH = @NR_BATCH," &
            " PER_IMPORTO = @PER_IMPORTO," &
            " STAMPATOIL = @STAMPATOIL," &
            " DATA_FATT_RIF = @DATA_FATT_RIF," &
            " NUM_FATT_RIF = @NUM_FATT_RIF," &
            " TERMINAL_ID = @TERMINAL_ID," &
            " ESITO_TRANSAZIONE = @ESITO_TRANSAZIONE," &
            " DATA_OPERAZIONE = @DATA_OPERAZIONE," &
            " AUTORIZZ_EVASA_IL = @AUTORIZZ_EVASA_IL," &
            " NR_PRATICA = @NR_PRATICA," &
            " PAG_RIF = @PAG_RIF," &
            " NR_PREAUT = @NR_PREAUT," &
            " preaut_aperta = @preaut_aperta," &
            " operazione_stornata = @operazione_stornata," &
            " scadenza_preaut = @scadenza_preaut," &
            " CONTABILIZZATO = @CONTABILIZZATO," &
            " TRANSATION_TYPE = @TRANSATION_TYPE," &
            " CARD_TYPE = @CARD_TYPE," &
            " acquire_id = @acquire_id," &
            " action_code = @action_code," &
            " TentativiPOS = @TentativiPOS" &
            " WHERE ID_CTR = @ID_CTR"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@Nr_Contratto", System.Data.SqlDbType.Int, Nr_Contratto)
                addParametro(Cmd, "@Data", System.Data.SqlDbType.DateTime, Data)
                addParametro(Cmd, "@TIPO_DOCU", System.Data.SqlDbType.Int, TIPO_DOCU)
                addParametro(Cmd, "@ID_ModPag", System.Data.SqlDbType.Int, ID_ModPag)
                addParametro(Cmd, "@ID_TIPPAG", System.Data.SqlDbType.Int, ID_TIPPAG)
                addParametro(Cmd, "@id_pos_funzioni_ares", System.Data.SqlDbType.Int, id_pos_funzioni_ares)
                addParametro(Cmd, "@id_funzione_stornata_ares", System.Data.SqlDbType.Int, id_funzione_stornata_ares)
                addParametro(Cmd, "@STAN_stornato", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(STAN_stornato, 20))
                addParametro(Cmd, "@Importo", System.Data.SqlDbType.Money, Importo)
                addParametro(Cmd, "@ID_STAZIONE", System.Data.SqlDbType.Int, ID_STAZIONE)
                addParametro(Cmd, "@CASSA", System.Data.SqlDbType.Int, CASSA)
                If Libreria.TrimSicuro(Titolo, 60) <> Nothing Then
                    Dim my_titolo As String

                    With New security
                        my_titolo = .encryptString(Libreria.TrimSicuro(Titolo, 50))
                    End With

                    addParametro(Cmd, "@Titolo", System.Data.SqlDbType.VarChar, my_titolo)
                Else
                    addParametro(Cmd, "@Titolo", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(Titolo, 50))
                End If
                addParametro(Cmd, "@Intestatario", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(Intestatario, 50))
                addParametro(Cmd, "@Scadenza", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(Scadenza, 5))
                addParametro(Cmd, "@nr_aut", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(nr_aut, 8))
                addParametro(Cmd, "@TIPSEGNO", System.Data.SqlDbType.SmallInt, TIPSEGNO)
                addParametro(Cmd, "@BUSTA_PC", System.Data.SqlDbType.Int, BUSTA_PC)
                addParametro(Cmd, "@NOTE", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(NOTE, 255))
                addParametro(Cmd, "@ID_OPERATORE", System.Data.SqlDbType.Int, ID_OPERATORE)
                addParametro(Cmd, "@id_operatore_ares", System.Data.SqlDbType.Int, id_operatore_ares)
                addParametro(Cmd, "@DATAMOD", System.Data.SqlDbType.DateTime, DATAMOD)
                addParametro(Cmd, "@UTEMOD", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(UTEMOD, 10))
                addParametro(Cmd, "@TIPOMOD", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(TIPOMOD, 1))
                addParametro(Cmd, "@DATACRE", System.Data.SqlDbType.DateTime, DATACRE)
                addParametro(Cmd, "@UTECRE", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(UTECRE, 15))
                addParametro(Cmd, "@N_CONTRATTO_RIF", System.Data.SqlDbType.Int, N_CONTRATTO_RIF)
                addParametro(Cmd, "@N_RDS_RIF", System.Data.SqlDbType.Int, N_RDS_RIF)
                addParametro(Cmd, "@N_MULTA_RIF", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(N_MULTA_RIF, 15))
                addParametro(Cmd, "@STA_IX", System.Data.SqlDbType.Int, STA_IX)
                addParametro(Cmd, "@N_PREN_RIF", System.Data.SqlDbType.Int, N_PREN_RIF)
                addParametro(Cmd, "@NR_BATCH", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(NR_BATCH, 20))
                addParametro(Cmd, "@PER_IMPORTO", System.Data.SqlDbType.Float, PER_IMPORTO)
                addParametro(Cmd, "@STAMPATOIL", System.Data.SqlDbType.DateTime, STAMPATOIL)
                addParametro(Cmd, "@DATA_FATT_RIF", System.Data.SqlDbType.DateTime, DATA_FATT_RIF)
                addParametro(Cmd, "@NUM_FATT_RIF", System.Data.SqlDbType.Int, NUM_FATT_RIF)
                addParametro(Cmd, "@TERMINAL_ID", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(TERMINAL_ID, 20))
                addParametro(Cmd, "@ESITO_TRANSAZIONE", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(ESITO_TRANSAZIONE, 2))
                addParametro(Cmd, "@DATA_OPERAZIONE", System.Data.SqlDbType.DateTime, DATA_OPERAZIONE)
                addParametro(Cmd, "@AUTORIZZ_EVASA_IL", System.Data.SqlDbType.DateTime, AUTORIZZ_EVASA_IL)
                addParametro(Cmd, "@NR_PRATICA", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(NR_PRATICA, 50))
                addParametro(Cmd, "@PAG_RIF", System.Data.SqlDbType.Int, PAG_RIF)
                addParametro(Cmd, "@NR_PREAUT", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(NR_PREAUT, 20))
                addParametro(Cmd, "@preaut_aperta", System.Data.SqlDbType.Bit, preaut_aperta)
                addParametro(Cmd, "@operazione_stornata", System.Data.SqlDbType.Bit, operazione_stornata)
                addParametro(Cmd, "@scadenza_preaut", System.Data.SqlDbType.DateTime, scadenza_preaut)
                addParametro(Cmd, "@CONTABILIZZATO", System.Data.SqlDbType.Bit, CONTABILIZZATO)
                addParametro(Cmd, "@TRANSATION_TYPE", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(TRANSATION_TYPE, 3))
                addParametro(Cmd, "@CARD_TYPE", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(CARD_TYPE, 1))
                addParametro(Cmd, "@acquire_id", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(acquire_id, 20))
                addParametro(Cmd, "@action_code", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(action_code, 10))
                addParametro(Cmd, "@TentativiPOS", System.Data.SqlDbType.Int, TentativiPOS)

                addParametro(Cmd, "@ID_CTR", System.Data.SqlDbType.Int, ID_CTR)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaRecord = True
            End Using
        End Using
    End Function

    Public Function EliminaRecord() As Boolean
        Return EliminaRecord(Me.ID_CTR)
    End Function

    Public Shared Function EliminaRecord(ByVal id_record As Integer) As Boolean
        EliminaRecord = False

        Dim sqlStr As String = "DELETE FROM PAGAMENTI_EXTRA WHERE ID_CTR = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord PAGAMENTI_EXTRA: " & ex.Message)
        End Try
    End Function

    Public Overrides Function toString() As String
        Dim separatore As String = "<br />"

        Dim Valore As String = "TABELLA PAGAMENTI_EXTRA" & separatore &
                "m_Nr_Contratto =  " & m_Nr_Contratto & separatore &
                "m_Data =  " & m_Data & separatore &
                "m_ID_CTR =  " & m_ID_CTR & separatore &
                "m_TIPO_DOCU =  " & m_TIPO_DOCU & separatore &
                "m_ID_ModPag =  " & m_ID_ModPag & separatore &
                "m_ID_TIPPAG =  " & m_ID_TIPPAG & separatore &
                "m_id_pos_funzioni_ares =  " & m_id_pos_funzioni_ares & separatore &
                "m_id_funzione_stornata_ares =  " & m_id_funzione_stornata_ares & separatore &
                "m_STAN_stornato =  " & m_STAN_stornato & separatore &
                "m_Importo =  " & m_Importo & separatore &
                "m_ID_STAZIONE =  " & m_ID_STAZIONE & separatore &
                "m_CASSA =  " & m_CASSA & separatore &
                "m_Titolo =  " & m_Titolo & separatore &
                "m_Intestatario =  " & m_Intestatario & separatore &
                "m_Scadenza =  " & m_Scadenza & separatore &
                "m_nr_aut =  " & m_nr_aut & separatore &
                "m_TIPSEGNO =  " & m_TIPSEGNO & separatore &
                "m_BUSTA_PC =  " & m_BUSTA_PC & separatore &
                "m_NOTE =  " & m_NOTE & separatore &
                "m_ID_OPERATORE =  " & m_ID_OPERATORE & separatore &
                "m_id_operatore_ares =  " & m_id_operatore_ares & separatore &
                "m_DATAMOD =  " & m_DATAMOD & separatore &
                "m_UTEMOD =  " & m_UTEMOD & separatore &
                "m_TIPOMOD =  " & m_TIPOMOD & separatore &
                "m_DATACRE =  " & m_DATACRE & separatore &
                "m_UTECRE =  " & m_UTECRE & separatore &
                "m_N_CONTRATTO_RIF =  " & m_N_CONTRATTO_RIF & separatore &
                "m_N_RDS_RIF =  " & m_N_RDS_RIF & separatore &
                "m_N_MULTA_RIF =  " & m_N_MULTA_RIF & separatore &
                "m_STA_IX =  " & m_STA_IX & separatore &
                "m_N_PREN_RIF =  " & m_N_PREN_RIF & separatore &
                "m_NR_BATCH =  " & m_NR_BATCH & separatore &
                "m_PER_IMPORTO =  " & m_PER_IMPORTO & separatore &
                "m_STAMPATOIL =  " & m_STAMPATOIL & separatore &
                "m_DATA_FATT_RIF =  " & m_DATA_FATT_RIF & separatore &
                "m_NUM_FATT_RIF =  " & m_NUM_FATT_RIF & separatore &
                "m_TERMINAL_ID =  " & m_TERMINAL_ID & separatore &
                "m_ESITO_TRANSAZIONE =  " & m_ESITO_TRANSAZIONE & separatore &
                "m_DATA_OPERAZIONE =  " & m_DATA_OPERAZIONE & separatore &
                "m_AUTORIZZ_EVASA_IL =  " & m_AUTORIZZ_EVASA_IL & separatore &
                "m_NR_PRATICA =  " & m_NR_PRATICA & separatore &
                "m_PAG_RIF =  " & m_PAG_RIF & separatore &
                "m_NR_PREAUT =  " & m_NR_PREAUT & separatore &
                "m_preaut_aperta =  " & m_preaut_aperta & separatore &
                "m_operazione_stornata =  " & m_operazione_stornata & separatore &
                "m_scadenza_preaut =  " & m_scadenza_preaut & separatore &
                "m_CONTABILIZZATO =  " & m_CONTABILIZZATO & separatore &
                "m_TRANSATION_TYPE =  " & m_TRANSATION_TYPE & separatore &
                "m_CARD_TYPE =  " & m_CARD_TYPE & separatore &
                "m_acquire_id =  " & m_acquire_id & separatore &
                "m_action_code =  " & m_action_code & separatore &
                "m_TentativiPOS =  " & m_TentativiPOS & separatore

        Return Valore
    End Function

    Public Shared Function getTipoPagamentoDaId(ByVal id_tip_pag As Integer) As String
        Dim sqlStr As String
        sqlStr = "SELECT Descrizione FROM TIP_PAG WITH(NOLOCK)" &
            " WHERE ID_TIPPag = " & id_tip_pag

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()

                Return Cmd.ExecuteScalar & ""
            End Using
        End Using
    End Function

    Public Shared Function getSegnoTipoPagamentoDaId(ByVal id_tip_pag As Integer?) As String
        If id_tip_pag Is Nothing Then
            Return "+"
        End If
        Dim sqlStr As String
        sqlStr = "SELECT SEGNO FROM TIP_PAG WITH(NOLOCK)" &
            " WHERE ID_TIPPag = " & id_tip_pag

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()

                Return Cmd.ExecuteScalar & ""
            End Using
        End Using
    End Function

    Public Shared Function getModPagamentoDaId(ByVal id_mod_pag As Integer) As String
        Dim sqlStr As String
        sqlStr = "SELECT Descrizione FROM MOD_PAG WITH(NOLOCK)" &
            " WHERE ID_ModPag = " & id_mod_pag

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()

                Return Cmd.ExecuteScalar & ""
            End Using
        End Using
    End Function

    Public Function ValorizzazioneStampaModuloPagamento() As DatiStampaModuloPagamenti

        Return ValorizzazioneStampaModuloPagamento(Me)
    End Function


    Protected Function getComuneDiNascitaAres(ByVal id_comune As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT comune FROM comuni_ares WHERE id='" & id_comune & "'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test <> "" Then
            getComuneDiNascitaAres = test
        Else
            getComuneDiNascitaAres = ""
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function ValorizzazioneStampaModuloPagamento(ByVal mio_record As PAGAMENTI_EXTRA) As DatiStampaModuloPagamenti
        Dim miei_dati As DatiStampaModuloPagamenti = New DatiStampaModuloPagamenti

        Dim num_documento As Integer
        Dim id_tipo_documento As TipoFattura
        If mio_record.N_CONTRATTO_RIF IsNot Nothing Then
            num_documento = mio_record.N_CONTRATTO_RIF
            id_tipo_documento = TipoFattura.Noleggio
        ElseIf mio_record.N_PREN_RIF IsNot Nothing Then
            num_documento = mio_record.N_PREN_RIF
            id_tipo_documento = TipoFattura.Prenotazione
        ElseIf mio_record.N_RDS_RIF IsNot Nothing Then
            num_documento = mio_record.N_RDS_RIF
            id_tipo_documento = TipoFattura.RDS
        ElseIf mio_record.N_MULTA_RIF IsNot Nothing Then
            num_documento = mio_record.N_MULTA_RIF
            id_tipo_documento = TipoFattura.Multe
        Else
            Return miei_dati
        End If

        With miei_dati
            If id_tipo_documento = TipoFattura.Noleggio Then
                Dim id_primo_conducente As String = ""
                Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()

                    Dim sqlStr As String = "SELECT top 1 id_primo_conducente, id_secondo_conducente" &
                        " FROM contratti WITH(NOLOCK)" &
                        " WHERE num_contratto = " & num_documento &
                        " AND attivo = 1" &
                        " ORDER BY num_calcolo DESC"
                    HttpContext.Current.Trace.Write(sqlStr)

                    Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                        Using Rs = Cmd.ExecuteReader
                            If Rs.Read Then
                                id_primo_conducente = Rs("id_primo_conducente")
                            End If
                        End Using
                    End Using

                    If id_primo_conducente <> "" Then
                        sqlStr = "SELECT * FROM CONDUCENTI WITH(NOLOCK) WHERE ID_CONDUCENTE = " & id_primo_conducente
                        HttpContext.Current.Trace.Write(sqlStr)

                        Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                            Using Rs = Cmd.ExecuteReader
                                If Rs.Read Then
                                    HttpContext.Current.Trace.Write("Nominativo")
                                    .sottoscritto = Rs("Nominativo") & "" ' & " " & Rs("COGNOME")
                                    HttpContext.Current.Trace.Write("Data_Nascita")
                                    Dim data_nascita As String = Rs("Data_Nascita") & ""
                                    If data_nascita <> "" Then
                                        .nato_giorno = Libreria.myFormatta(Day(data_nascita), "00")
                                        .nato_mese = Libreria.myFormatta(Month(data_nascita), "00")
                                        .nato_anno = Year(data_nascita)
                                    End If

                                    If Rs("nazione_nascita") = Costanti.ID_Italia Then
                                        If (Rs("id_comune_ares_nascita") & "") <> "" Then
                                            .nato_a = getComuneDiNascitaAres(Rs("id_comune_ares_nascita"))
                                        End If
                                    Else
                                        .nato_a = Rs("comune_nascita_ee") & ""
                                    End If

                                    If .nato_a = "" Then
                                        .nato_a = Rs("Luogo_Nascita") & ""
                                    End If

                                    Dim comune As String = ""
                                    If Not (Rs("id_comune_ares") Is DBNull.Value) Then
                                        comune = Libreria.getComuneAres(Rs("id_comune_ares")) & ""
                                    End If
                                    If comune = "" Then
                                        .residenza = Rs("City")
                                        If Not (Rs("PROVINCIA") Is DBNull.Value) Then
                                            .residenza += " (" & Rs("PROVINCIA") & ")"
                                        End If
                                    Else
                                        .residenza = comune
                                    End If

                                    .indirizzo = Rs("Indirizzo") & ""

                                    Dim patente As String = Rs("Patente") & ""
                                    If patente <> "" Then
                                        .documento_tipo = "Patente " & Rs("Tipo_Patente")
                                        .documento_numero = patente
                                        .documento_rilasciato = Rs("LUOGO_EMISSIONE") & ""
                                        Dim data_rilascio As String = Rs("RILASCIATA_IL") & ""
                                        If data_rilascio <> "" Then
                                            .documento_giorno = Libreria.myFormatta(Day(data_rilascio), "00")
                                            .documento_mese = Libreria.myFormatta(Month(data_rilascio), "00")
                                            .documento_anno = Year(data_rilascio)
                                        End If

                                    End If

                                End If
                            End Using
                        End Using
                    End If
                End Using
            End If

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                Dim sqlStr As String = "SELECT citta FROM stazioni WITH(NOLOCK)" &
                    " WHERE id = " & HttpContext.Current.Request.Cookies("SicilyRentCar")("stazione")
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    .localita = Cmd.ExecuteScalar & ""
                End Using
            End Using

            .contratto_numero = num_documento
            .autorizzazione = mio_record.nr_aut & ""
            '.conto_numero = Libreria.NascondiNumeroCarta(mio_record.Titolo & "")
            .conto_numero = mio_record.Titolo & ""

            If mio_record.mese_scadenza > 0 Then
                .conto_mese = Libreria.myFormatta(mio_record.mese_scadenza, "00")
            End If
            If mio_record.anno_scadenza > 0 Then
                .conto_anno = Libreria.myFormatta(mio_record.anno_scadenza, "00")
            End If

            If mio_record.PER_IMPORTO IsNot Nothing Then
                Dim importo() As String = Libreria.myFormatta(mio_record.PER_IMPORTO, "0.00").Split(",")
                .importo_euro = importo(0)
                If importo.Length > 1 Then
                    .importo_centesimi = importo(1)
                End If
            End If
            If mio_record.Data IsNot Nothing Then
                .data_giorno = Libreria.myFormatta(Day(mio_record.Data), "00")
                .data_mese = Libreria.myFormatta(Month(mio_record.Data), "00")
                .data_anno = Year(mio_record.Data)
            End If

        End With


        Return miei_dati
    End Function


End Class
