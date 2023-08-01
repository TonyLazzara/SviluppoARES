﻿Imports System.IO

Public Class fatturazione_nolo

    Public Shared Function getNumeroFatturaCliente(ByVal num_contratto As String, ByVal con_data As Boolean) As String
        Dim restituire_data As String = ""

        If con_data Then
            restituire_data = "CAST(YEAR(data_fattura) As NVARCHAR(20)) + '/' +"
        End If

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT " & restituire_data & " CAST(num_fattura As NVARCHAR(20)) FROM fatture_nolo WITH(NOLOCK) WHERE num_contratto_rif='" & num_contratto & "' AND fattura_cliente='1' AND attiva='1'", Dbc)

        getNumeroFatturaCliente = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function getNumeroFatturaBroker(ByVal num_contratto As String, ByVal con_data As Boolean) As String
        Dim restituire_data As String = ""

        If con_data Then
            restituire_data = "CAST(YEAR(data_fattura) As NVARCHAR(20)) + '/' +"
        End If

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT " & restituire_data & " CAST(num_fattura As NVARCHAR(20)) FROM fatture_nolo WITH(NOLOCK) WHERE num_contratto_rif='" & num_contratto & "' AND fattura_broker='1' AND attiva='1'", Dbc)

        getNumeroFatturaBroker = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function getNumeroFatturaCostiPrepagati(ByVal num_contratto As String, ByVal con_data As Boolean) As String
        Dim restituire_data As String = ""

        If con_data Then
            restituire_data = "CAST(YEAR(data_fattura) As NVARCHAR(20)) + '/' +"
        End If

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT " & restituire_data & " CAST(num_fattura As NVARCHAR(20)) FROM fatture_nolo WITH(NOLOCK) WHERE num_contratto_rif='" & num_contratto & "' AND fattura_costi_prepagati='1' AND attiva='1'", Dbc)

        getNumeroFatturaCostiPrepagati = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function get_id_fattura_cliente(ByVal id_contratto As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_fattura_cliente FROM contratti WITH(NOLOCK) WHERE id='" & id_contratto & "'", Dbc)

        get_id_fattura_cliente = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function get_id_fattura_broker(ByVal id_contratto As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_fattura_broker FROM contratti WITH(NOLOCK) WHERE id='" & id_contratto & "'", Dbc)

        get_id_fattura_broker = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function get_id_fattura_prepagato(ByVal id_contratto As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_fattura_prepagata FROM contratti WITH(NOLOCK) WHERE id='" & id_contratto & "'", Dbc)

        get_id_fattura_prepagato = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function elimina_fatture_ra(ByVal num_contratto As String) As Boolean
        'ELIMINA LE FATTURE ASSOCIATE AL CONTRATTO E RESTITUISCE TRUE OPPURE RESTITUISCE FALSE SE NON E' POSSIBILE ELIMINARLE
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM fatture_nolo WHERE num_contratto_rif='" & num_contratto & "' AND da_esportare_contabilita='0' AND attiva='1'", Dbc)
        Dim test As String = Cmd.ExecuteScalar & ""
        If test = "" Then
            'LE FATTURE NON SONO STATE INVIATE IN CONTABILITA' - E' POSSIBILE ELIMINARLE
            Dim sqlStr As String = ""
            sqlStr = "SELECT id_fattura_cliente, id_fattura_broker, id_fattura_prepagata FROM contratti WITH(NOLOCK) WHERE num_contratto='" & num_contratto & "' AND attivo='1'"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Rs.Read()

            Dim id_fattura_cliente As String = Rs("id_fattura_cliente") & ""
            Dim id_fattura_broker As String = Rs("id_fattura_broker") & ""
            Dim id_fattura_prepagata As String = Rs("id_fattura_prepagata") & ""

            If id_fattura_cliente = "" Then
                id_fattura_cliente = "0"
            End If
            If id_fattura_broker = "" Then
                id_fattura_broker = "0"
            End If
            If id_fattura_prepagata = "" Then
                id_fattura_prepagata = "0"
            End If

            Dbc.Close()
            Dbc.Open()

            sqlStr = "DELETE FROM fatture_nolo_pagamenti WHERE id_fattura_nolo='" & id_fattura_cliente & "' OR id_fattura_nolo='" & id_fattura_broker & "' OR id_fattura_nolo='" & id_fattura_prepagata & "'"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            sqlStr = "DELETE FROM fatture_nolo_righe WHERE id_fattura='" & id_fattura_cliente & "' OR id_fattura='" & id_fattura_broker & "' OR id_fattura='" & id_fattura_prepagata & "'"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            sqlStr = "UPDATE contratti SET id_fattura_cliente=NULL, id_fattura_broker=NULL, id_fattura_prepagata=NULL, status='" & Costanti.stato_contratto.da_fatturare & "' WHERE num_contratto='" & num_contratto & "' AND attivo='1'"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            sqlStr = "DELETE FROM fatture_nolo WHERE id='" & id_fattura_cliente & "' OR id='" & id_fattura_broker & "' OR id='" & id_fattura_prepagata & "'"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()



            Rs.Close()
            Rs = Nothing
            elimina_fatture_ra = True
        Else
            elimina_fatture_ra = False
        End If


        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Sub genera_fatture_ra(ByVal daData As String, ByVal aData As String, Optional ByVal data_fattura As String = "")
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim da_data As String = funzioni_comuni.getDataDb_senza_orario(daData)
        Dim a_data As String = funzioni_comuni.getDataDb_con_orario(aData & " 23:59:59")

        Dim sqlStr As String = "SELECT id, num_calcolo,  giorni_prepagati FROM contratti WITH(NOLOCK) WHERE attivo='1' AND status='" & Costanti.stato_contratto.da_fatturare & "' AND "
        '"contratti.data_uscita BETWEEN '" & da_data & "' AND '" & a_data & "' AND ISNULL(non_fatturare,'0')='0' ORDER BY contratti.data_uscita ASC"
        sqlStr += "contratti.data_uscita BETWEEN Convert(DateTime, '" & da_data & "', 102) AND CONVERT(DATETIME, '" & a_data & "', 102) AND ISNULL(non_fatturare,'0')='0' ORDER BY contratti.data_uscita ASC"

        Try
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Do While Rs.Read()
                If (Rs("giorni_prepagati") & "") <> "" Then
                    genera_fattura_ra_prepagato(Rs("id"), Rs("num_calcolo"), data_fattura)
                Else
                    genera_fattura_ra(Rs("id"), Rs("num_calcolo"), data_fattura)
                End If
            Loop

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("ERROR_fatturazione_nolo-genera_fatture_RA_:" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try



    End Sub


    Public Shared Function EsisteImponibileRDS(ByVal num_contratto As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()


        Dim sqlPag As String = "SELECT * FROM PAGAMENTI_EXTRA WITH(NOLOCK) LEFT JOIN MOD_PAG WITH(NOLOCK)"
        sqlPag += "ON pagamenti_extra.ID_ModPag=MOD_PAG.ID_ModPag LEFT JOIN POS_Funzioni WITH(NOLOCK) ON PAGAMENTI_EXTRA.id_pos_funzioni_ares=POS_Funzioni.id "
        sqlPag += "LEFT JOIN codici_contabili WITH(NOLOCK) ON pagamenti_extra.ID_ModPag=codici_contabili.modpag AND pagamenti_extra.ID_TipPag=codici_contabili.tippag "
        sqlPag += "WHERE pagamenti_extra.N_RDS_RIF IN (SELECT id_rds FROM veicoli_evento_apertura_danno WITH(NOLOCK) WHERE id_documento_apertura = '" & num_contratto & "') "
        sqlPag += "AND (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Chiusura & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Vendita & "' "
        sqlPag += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Pagamento_Contanti & "' "
        sqlPag += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso & "' "
        sqlPag += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Attivo & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Passivo & "' "
        sqlPag += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Deposito_su_RA & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso_su_RA & "') "
        sqlPag += "AND NOT operazione_stornata='1' AND N_RDS_RIF IS NOT NULL "

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlPag, Dbc)
        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Dim importo_rds As Double = 0
        Do While Rs.Read()
            importo_rds = importo_rds + CDbl(Rs("PER_IMPORTO"))
        Loop

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        If importo_rds > 0 Then
            EsisteImponibileRDS = True
        Else
            EsisteImponibileRDS = False
        End If
    End Function

    Public Shared Sub genera_fattura_ra_prepagato(ByVal id_contratto As String, ByVal num_calcolo As String, ByVal data_fattura As String, Optional ByVal num_fattura As String = "")
        'NEL CASO DI PREPAGATO QUESTA FUNZIONE GENERA UNA SINGOLA FATTURA CHE INCLUDE PRENOTAZIONE + CONTRATTO

        Dim sqlStr As String = ""
        Dim sqlStr2 As String
        Dim sqla As String = ""

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            sqlStr = "SELECT ISNULL(contratti.importo_a_carico_del_broker,0) As importo_a_carico_del_broker, num_calcolo,"
            sqlStr += "giorni, ISNULL(giorni_to,0) As giorni_to, rif_to, broker.[codice edp] As codice_edp_broker, broker.id_ditta As id_broker,"
            sqlStr += " data_rientro,targa, modello, contratti.codice_edp, contratti.id_cliente, contratti.num_contratto, num_prenotazione, num_crv, "
            sqlStr += "(stazione_uscita.codice + ' ' + stazione_uscita.nome_stazione) As staz_uscita, "
            sqlStr += "(stazione_rientro.codice + ' ' + stazione_rientro.nome_stazione) As staz_rientro, data_uscita, data_rientro, "
            sqlStr += "id_primo_conducente, (contratti_conducenti.cognome + ' ' + contratti_conducenti.nome) As conducente, "
            sqlStr += "contratti_conducenti.indirizzo, contratti_conducenti.city, contratti_conducenti.cap, contratti_conducenti.provincia, contratti.giorni_prepagati, "
            sqlStr += "nazioni.nazione, contratti_conducenti.codfis, contratti_conducenti.email, km_uscita, km_rientro, targa, modello, ISNULL(prenotazione_prepagata,'0') As prenotazione_prepagata "
            sqlStr += "FROM contratti WITH(NOLOCK) INNER JOIN stazioni As stazione_uscita WITH(NOLOCK) ON contratti.id_stazione_uscita=stazione_uscita.id "
            sqlStr += "INNER JOIN stazioni As stazione_rientro WITH(NOLOCK) ON contratti.id_stazione_rientro=stazione_rientro.id "
            sqlStr += "INNER JOIN contratti_conducenti WITH(NOLOCK) ON contratti.id_primo_conducente=contratti_conducenti.id_conducente AND contratti.num_contratto=contratti_conducenti.num_contratto "
            sqlStr += "LEFT JOIN nazioni WITH(NOLOCK) ON contratti_conducenti.nazione=nazioni.id_nazione "
            sqlStr += "LEFT JOIN clienti_tipologia WITH(NOLOCK) ON contratti.id_fonte=clienti_tipologia.id "
            sqlStr += "LEFT JOIN ditte As broker WITH(NOLOCK) ON clienti_tipologia.id_ditta=broker.id_ditta "
            sqlStr += "WHERE contratti.id='" & id_contratto & "'"
            sqla = sqlStr
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()


            Rs.Read()
            'INTESTAZIONE FATTURA -----------------------------------------------------------------

            'GENERARE NUMERO FATTURA
            'DATA FATTURA NEL CASO DI FATTURA GENEREATA ALL'INTERNO DELLA PAGINA DEL CONTRATTO LA DATA DEVE ESSERE PASSATA COME ARGOMENTO
            'SALVARE NUMERO VOUCHER (ATTUALMENTE SU PRENOTAZIONE - RIPORTARLO SU CONTRATTO VISTO CHE SERVE ANCHE NELLA STAMPA DELL'RA)

            Dim dataFattura As String
            If data_fattura <> "" Then
                dataFattura = funzioni_comuni.getDataDb_senza_orario2(data_fattura)
            Else
                If Day(Now()) <= 9 Then
                    data_fattura = "0" & Day(Now())
                Else
                    data_fattura = Day(Now())
                End If

                If Month(Now()) <= 9 Then
                    data_fattura = data_fattura & "/0" & Month(Now())
                Else
                    data_fattura = data_fattura & "/" & Month(Now())
                End If
                data_fattura = data_fattura & "/" & Year(Now())
            End If

            Dim id_fattura As String
            Dim anno_fattura As String = Year(dataFattura)

            Dim targa As String
            Dim modello As String
            Dim km_uscita As String
            Dim km_rientro As String

            Dim giorni As String = Rs("giorni")
            Dim giorni_to As String = Rs("giorni_to")
            Dim giorni_prepagati As String = Rs("giorni_prepagati") & ""
            If giorni_prepagati = "" Then
                giorni_prepagati = "0"
            End If
            Dim aliquota_iva As Double

            Dim intestazione As String = Rs("conducente")
            Dim conducente As String = Rs("conducente")
            Dim indirizzo As String = Rs("indirizzo")
            Dim citta As String = Rs("city")
            Dim cap As String = Rs("cap")
            Dim provincia As String = Rs("provincia") & ""
            Dim nazione As String = Rs("nazione") & ""
            Dim piva As String = ""
            Dim codice_fiscale As String = Rs("codfis")
            Dim email As String = Rs("email") & ""
            Dim email_pec As String = ""
            Dim codice_sdi As String = ""

            Dim codice_edp As String = Rs("codice_edp")
            Dim num_contratto As String = Rs("num_contratto")
            Dim num_prenotazione As String = Rs("num_prenotazione") & ""
            Dim num_voucher As String = Rs("rif_to") & ""
            If num_voucher = "" Then
                num_voucher = num_prenotazione
            End If
            Dim id_primo_conducente As String = Rs("id_primo_conducente")
            Dim id_cliente As String = Rs("id_cliente")

            'NEL CASO DI CRV SU FATTURA DEVE ESSERE SPECIFICATO IL PRIMO VEICOLO
            If Rs("num_crv") = "0" Then
                targa = Rs("targa")
                modello = Rs("modello")
                km_uscita = Rs("km_uscita")
                km_rientro = Rs("km_rientro")
            Else
                targa = ""
                modello = ""
                km_uscita = ""
                km_rientro = ""
            End If



            'NUM_RF/DATA FATTURA: IN QUESTO MOMENTO NON SONO CORRETTI - UTILIZZARE I VALORI UNA VOLTA CAPITO DA OVE VENGONO GENERATI

            sqlStr = "INSERT INTO fatture_nolo (attiva, id_tipo_fattura, fattura_cliente, fattura_broker, fattura_costi_prepagati, num_fattura, data_fattura, "
            sqlStr += "num_rf, data_rf, id_ditta, cod_edp,"
            sqlStr += "num_contratto_rif,num_prenotazione_rif, num_voucher, stazione_uscita, data_uscita, stazione_rientro, data_rientro, conducente,  "

            sqlStr2 = " VALUES ( "
            sqlStr2 += "'0','2','1','0','0','0',convert(datetime,'" & funzioni_comuni.getDataDb_con_orario(dataFattura) & "',102),"
            sqlStr2 += "'0',convert(datetime,'" & funzioni_comuni.getDataDb_con_orario(dataFattura) & "',102),"
            sqlStr2 += "'" & Rs("id_cliente") & "','" & Rs("codice_edp") & "','" & num_contratto & "','" & num_prenotazione & "" & "',"
            sqlStr2 += "'" & Replace(num_voucher, "'", "''") & "','" & Replace(Rs("staz_uscita"), "'", "''") & "',convert(datetime,'" & funzioni_comuni.getDataDb_con_orario(Rs("data_uscita")) & "',102),"
            sqlStr2 += "'" & Replace(Rs("staz_rientro"), "'", "''") & "',convert(datetime,'" & funzioni_comuni.getDataDb_con_orario(Rs("data_rientro")) & "',102),'" & Replace(conducente, "'", "''") & "',"

            Dbc.Close()
            Dbc.Open()

            'DATI VETTURA: NEL CASO DI CRV LI TROVO IN contratti_crv_veicoli
            sqlStr = sqlStr & "targa,modello,km_uscita,km_rientro, "
            If targa <> "" Then
                sqlStr2 = sqlStr2 & "'" & Replace(targa, "'", "''") & "','" & Replace(modello, "'", "''") & "','" & km_uscita & "','" & km_rientro & "',"
            Else
                Dim sql As String = "SELECT km_uscita, km_rientro, veicoli.targa, modelli.descrizione As modello "
                sql += "FROM contratti_crv_veicoli WITH(NOLOCK) "
                sql += "INNER JOIN veicoli WITH(NOLOCK) ON contratti_crv_veicoli.id_veicolo=veicoli.id "
                sql += "INNER JOIN modelli ON veicoli.id_modello=modelli.id_modello WHERE num_contratto='" & num_contratto & "' AND num_crv='0'"
                sqla = sql
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                Rs = Cmd.ExecuteReader()

                Rs.Read()

                targa = Rs("targa")
                modello = Rs("modello")
                km_uscita = Rs("km_uscita")
                km_rientro = Rs("km_rientro")

                sqlStr2 = sqlStr2 & "'" & Replace(targa, "'", "''") & "','" & Replace(modello, "'", "''") & "','" & km_uscita & "','" & km_rientro & "',"

                Dbc.Close()
                Dbc.Open()
            End If

            'INTESTAZIONE FATTURA: NEL CASO DI CLIENTE CASH PRENDO I DATI DA QUELLI DEL PRIMO GUIDATORE ALTRIMENTI DA QUELLI DELLA DITTA
            sqlStr = sqlStr & "intestazione, indirizzo, citta, cap, provincia, nazione, piva, codice_fiscale, mail, email_pec, codice_sdi,"
            If codice_edp = Costanti.codice_cash Then
                sqlStr2 += "'" & Replace(intestazione, "'", "''") & "','" & Replace(indirizzo, "'", "''") & "',"
                sqlStr2 += "'" & Replace(citta, "'", "''") & "','" & Replace(cap, "'", "''") & "','" & Replace(provincia, "'", "''") & "',"
                sqlStr2 += "'" & Replace(nazione, "'", "''") & "','','" & Replace(codice_fiscale, "'", "''") & "','" & Replace(email, "'", "''") & "','','',"
            Else
                Dim sql As String = "SELECT rag_soc, indirizzo, citta, cap, provincia, nazioni.nazione, piva, email, c_fis, email_pec, codice_sdi "
                sql += "FROM ditte WITH(NOLOCK) LEFT JOIN nazioni WITH(NOLOCK) ON ditte.nazione=nazioni.id_nazione "
                sql += "WHERE ditte.id_ditta='" & id_cliente & "'"
                sqla = sql
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                Rs = Cmd.ExecuteReader()

                Rs.Read()

                intestazione = Rs("rag_soc") & ""
                indirizzo = Rs("indirizzo") & ""
                citta = Rs("citta") & ""
                cap = Rs("cap") & ""
                provincia = Rs("provincia") & ""
                nazione = Rs("nazione") & ""
                piva = Rs("piva") & ""
                email = Rs("email") & ""
                codice_fiscale = Rs("c_fis") & ""
                email_pec = Rs("email_pec") & ""
                codice_sdi = Rs("codice_sdi") & ""

                sqlStr2 += "'" & Replace(intestazione, "'", "''") & "','" & Replace(indirizzo, "'", "''") & "',"
                sqlStr2 += "'" & Replace(citta, "'", "''") & "','" & Replace(cap, "'", "''") & "','" & Replace(provincia, "'", "''") & "',"
                sqlStr2 += "'" & Replace(nazione, "'", "''") & "','" & Replace(piva, "'", "''") & "','" & Replace(codice_fiscale, "'", "''") & "','" & Replace(email, "'", "''") & "',"
                sqlStr2 += "'" & Replace(email_pec, "'", "''") & "','" & Replace(codice_sdi, "'", "''") & "',"

                Dbc.Close()
                Dbc.Open()
            End If

            'TOTALE NELL'INTESTAZIONE DELLA FATTURA
            If num_prenotazione = "" Then
                num_prenotazione = "-1"
            End If


            Dim sql2 As String = " (SELECT ISNULL(SUM(per_importo),0) FROM pagamenti_extra WITH(NOLOCK) WHERE "
            sql2 += " (N_CONTRATTO_RIF='" & num_contratto & "' OR N_PREN_RIF='" & num_prenotazione & "')  "
            sql2 += " AND (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Chiusura & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Vendita & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Pagamento_Contanti & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Attivo & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Passivo & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.MCR_Pagamento_Cash & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Deposito_su_RA & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso_su_RA & "') "
            sql2 += "AND NOT operazione_stornata='1')  "
            sqla = sql2
            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Dim totale_incassato As Double = Cmd.ExecuteScalar

            'TOTALE IMPONIBILE ED IVA 
            sql2 = " SELECT (imponibile_scontato+ISNULL(imponibile_onere,0)) As imponibile,"
            sql2 += "(iva_imponibile_scontato+ISNULL(iva_onere,0)) As iva FROM contratti_costi WITH(NOLOCK) "
            sql2 += "WHERE id_documento=" & id_contratto & " AND num_calcolo='" & num_calcolo & "' "
            sql2 += "AND nome_costo='" & LCase(Costanti.testo_elemento_totale) & "'"
            sqla = sql2
            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Rs = Cmd.ExecuteReader()

            Rs.Read()

            Dim imponibile As Double = Rs("imponibile")
            Dim iva As Double = Rs("iva")

            Dbc.Close()
            Dbc.Open()

            sqlStr = sqlStr & "imponibile, iva, totale_fattura, totale_pagamenti, saldo, "
            sqlStr2 += "'" & Replace(FormatNumber(imponibile, 2, , , TriState.False), ",", ".") & "',"
            sqlStr2 += "'" & Replace(FormatNumber(iva, 2, , , TriState.False), ",", ".") & "',"
            sqlStr2 += "'" & Replace(FormatNumber(imponibile + iva, 2, , , TriState.False), ",", ".") & "',"
            sqlStr2 += "'" & Replace(FormatNumber(totale_incassato, 2, , , TriState.False), ",", ".") & "',"
            sqlStr2 += "'" & Replace(FormatNumber(imponibile + iva - totale_incassato, 2, , , TriState.False), ",", ".") & "',"
            sqlStr += "data_fattura_prepagato, numero_fattura_prepagato)"
            sqlStr2 += "NULL,NULL)"
            sqla = sqlStr & " " & sqlStr2
            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()

            'RECUPERO L'ID DELLA FATTURA APPENA CREATA 
            sqlStr = "SELECT @@IDENTITY FROM fatture_nolo"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            id_fattura = Cmd.ExecuteScalar

            Dim aliquota_iva_tempo_km As String
            Dim codice_iva_tempo_km As String
            Dim iva_elemento As String

            Dim i As Integer = 0

            'RIGHE DELLA FATTURA --------------------------------------------------------------------------------------------------------------
            Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc2.Open()

            sqlStr = "SELECT id_elemento, nome_costo, ISNULL(imponibile_scontato,0) As imponibile_scontato, ISNULL(iva_imponibile_scontato,0) As iva_imponibile_scontato, ISNULL(imponibile_scontato_prepagato,0) As imponibile_scontato_prepagato,"
            sqlStr += "ISNULL(iva_imponibile_scontato_prepagato,0) As iva_imponibile_scontato_prepagato, qta, aliquota_iva, ISNULL(codice_iva,'') As codice_iva, ISNULL(prepagato,'0') As prepagato FROM contratti_costi WITH(NOLOCK) "
            sqlStr += "WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND selezionato='1' "
            sqlStr += "AND (id_a_carico_di<>'" & Costanti.id_accessorio_incluso & "' OR id_elemento='" & Costanti.ID_tempo_km & "') AND ISNULL(omaggiato,'0')='0' "
            sqlStr += "ORDER BY ordine_stampa ASC"
            sqla = sqlStr
            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Rs = Cmd.ExecuteReader()

            Do While Rs.Read()
                imponibile = Rs("imponibile_scontato")
                iva_elemento = Rs("iva_imponibile_scontato")

                If Rs("id_elemento") = Costanti.ID_tempo_km Then
                    'NE APPROFITTO PER SALVARE L'ALIQUOTA IVA DEL TEMPO KM DA UTILIZZARE EVENTUALMENTE PER SCORPORARE SUCCESSIVAMENTE
                    'L'IMPORTO PREPAGATO
                    aliquota_iva_tempo_km = Rs("aliquota_iva")
                    codice_iva_tempo_km = Rs("codice_iva") & ""
                End If
                Dim qta As Integer = Rs("qta")

                'HttpContext.Current.Trace.Write(Rs("nome_costo") & " " & iva_elemento & " " & imponibile)

                sqlStr2 = "INSERT INTO fatture_nolo_righe (id_fattura, descrizione, quantita, costo_unitario, iva, totale, aliquota_iva, codice_iva) VALUES ("
                sqlStr2 += "'" & id_fattura & "','" & Replace(Rs("nome_costo"), "'", "''") & "','" & qta & "',"
                sqlStr2 += "'" & Replace(FormatNumber(imponibile / qta, 2, , , TriState.False), ",", ".") & "','" & Replace(FormatNumber(iva_elemento, 2, , , TriState.False), ",", ".") & "',"
                sqlStr2 += "'" & Replace(FormatNumber(imponibile, 2, , , TriState.False), ",", ".") & "','" & Replace(Rs("aliquota_iva"), ",", ".") & "','" & Rs("codice_iva") & "" & "')"
                sqla = sqlStr2
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc2)
                Cmd.ExecuteNonQuery()
            Loop

            'RIGHE DI PAGAMENTO ---------------------------------------------------------------------------------------------------------------
            Dbc.Close()
            Dbc.Open()


            'ATTENZIONE: RIMBORSO SU RA SERVE PERCHE' CON QUESTA VOCE SI BILANCIA L'EVENTUALE CHIUSURA A 5 CENTESIMI DELLE PREAUTORIZZAZIONI
            'TENERNE CONTO SE SI DOVESSE DECIDERE DI NON RIPORTARE I DEPOSITI CAUZIONALI IN CONTANTI E I CORRISPETTIVI RIMBORSI SU FATTURA.

            sqlStr = "SELECT pagamenti_extra.data, mod_pag.descrizione As modalita_pagamento, POS_Funzioni.Funzione As tipo_pagamento, ID_TIPPAG,  "
            sqlStr += "pagamenti_extra.PER_IMPORTO, N_CONTRATTO_RIF,  N_RDS_RIF, N_PREN_RIF, codici_contabili.codice_contabile, pagamenti_extra.id_ctr FROM PAGAMENTI_EXTRA WITH(NOLOCK) "
            sqlStr += "LEFT JOIN MOD_PAG WITH(NOLOCK) ON pagamenti_extra.ID_ModPag=MOD_PAG.ID_ModPag "
            sqlStr += "LEFT JOIN POS_Funzioni WITH(NOLOCK) ON PAGAMENTI_EXTRA.id_pos_funzioni_ares=POS_Funzioni.id "
            sqlStr += "LEFT JOIN codici_contabili WITH(NOLOCK) ON pagamenti_extra.ID_ModPag=codici_contabili.modpag AND pagamenti_extra.ID_TipPag=codici_contabili.tippag "
            sqlStr += "WHERE (N_CONTRATTO_RIF='" & num_contratto & "' OR N_PREN_RIF='" & num_prenotazione & "' OR pagamenti_extra.N_RDS_RIF IN (SELECT id_rds FROM veicoli_evento_apertura_danno WITH(NOLOCK) WHERE id_documento_apertura = '" & num_contratto & "')) "
            sqlStr += "AND NOT operazione_stornata='1' ORDER BY pagamenti_extra.data ASC"

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Rs = Cmd.ExecuteReader()

            Do While Rs.Read()
                Dim modalita_pagamento As String = Rs("modalita_pagamento") & ""
                If (modalita_pagamento = "" And (Rs("ID_TIPPAG") & "") = "1011098650") Or (Rs("tipo_pagamento") & "").ToString.ToLower = "preautorizzazione" Or (Rs("tipo_pagamento") & "").ToString.ToLower = "chiusura preautorizzazione" Then
                    modalita_pagamento = "C.CREDITO"
                End If

                Dim tipo_pagamento As String = Rs("tipo_pagamento") & ""
                If tipo_pagamento.ToLower = "chiusura" Then
                    tipo_pagamento = "Chiusura Preautorizzazione"
                End If

                If (Rs("n_contratto_rif") & "") <> "" Or (Rs("N_RDS_RIF") & "") <> "" Or (Rs("N_PREN_RIF") & "") <> "" Then

                    'REGISTRO L'OPERAZIONE IN fatture_nolo_pagamenti
                    sqlStr2 = "INSERT INTO fatture_nolo_pagamenti (id_fattura_nolo, data_pagamento, modalita_pagamento, tipo_pagamento, codice_contabile, importo, id_ctr_pagamenti_extra) "
                    sqlStr2 += "VALUES ('" & id_fattura & "',convert(datetime,'" & funzioni_comuni.getDataDb_senza_orario(Rs("data")) & "',102),"
                    sqlStr2 += "'" & Replace(modalita_pagamento, "'", "''") & "','" & Replace(tipo_pagamento & "", "'", "''") & "',"
                    sqlStr2 += "'" & Rs("codice_contabile") & "" & "',"
                    sqlStr2 += "'" & Replace(Rs("per_importo"), ",", ".") & "','" & Rs("id_ctr") & "')"

                    Cmd = New Data.SqlClient.SqlCommand(sqlStr2, Dbc2)
                    Cmd.ExecuteNonQuery()
                End If
            Loop

            Dbc.Close()
            Dbc.Open()

            'SE TUTTO E' ANDATO A BUON FINE RENDO ATTIVE LE DUE FATTURE ASSEGNANDONE IL NUMERO E CAMBIO LO STATO DEL CONTRATTO A "FATTURATO"
            Dim num_fattura_cliente As String

            'SE DALL'ESTERNO E' STATO SPECIFICATO UN NUMERO DI FATTURA (SOLO PER ASSEGNAZIONE SINGOLO CONTRATTO) ALLORA VIENE UTILIZZATO QUEL VALORE. ATTENZIONE: PRIMA DI CHIAMARE
            'QUESTO METODO CI SI DEVE ACCERTARE CHE I NUMERI FATTURA (CLIENTE ED EVENTUALMENTE BROKER) SONO DISPONIBILI.
            If num_fattura = "" Then
                If id_fattura <> "0" Then
                    'SE LA FATTURA CLIENTE E' STATA CREATA
                    num_fattura_cliente = Contatori.getContatore_fatture_nolo(anno_fattura)
                End If
            Else
                num_fattura_cliente = num_fattura
            End If

            If id_fattura <> "0" Then
                sqla = "UPDATE fatture_nolo SET attiva='1', da_esportare_contabilita='1', num_fattura='" & num_fattura_cliente & "' WHERE id='" & id_fattura & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                Cmd.ExecuteNonQuery()

                sqla = "UPDATE contratti SET status='6', fatturazione_da_controllare='0', non_fatturare='0', id_fattura_cliente='" & id_fattura & "' WHERE id='" & id_contratto & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                Cmd.ExecuteNonQuery()
            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
            Dbc2.Close()
            Dbc2.Dispose()
            Dbc2 = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error genera_fattura_ra_prepagato : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try




    End Sub

    'Public Shared Sub genera_fattura_ra(ByVal id_contratto As String, ByVal num_calcolo As String, ByVal data_fattura As String, Optional ByVal num_fattura As String = "")
    '    'NEL CASO DI PREPAGATO QUESTA FUNZIONE GENERA UNA FATTURA PREPAGATA ED UNA FATTURA PER IL CONTRATTO
    '    'FATTURA A CARICO DEL CLIENTE
    '    Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '    Dbc.Open()

    '    Dim sqlStr As String = "SELECT ISNULL(contratti.importo_a_carico_del_broker,0) As importo_a_carico_del_broker, num_calcolo," & _
    '    "giorni, ISNULL(giorni_to,0) As giorni_to, rif_to, broker.[codice edp] As codice_edp_broker, broker.id_ditta As id_broker," & _
    '    " data_rientro,targa, modello, contratti.codice_edp, contratti.id_cliente, contratti.num_contratto, num_prenotazione, num_crv, " & _
    '    "(stazione_uscita.codice + ' ' + stazione_uscita.nome_stazione) As staz_uscita, " & _
    '    "(stazione_rientro.codice + ' ' + stazione_rientro.nome_stazione) As staz_rientro, data_uscita, data_rientro, " & _
    '    "id_primo_conducente, (contratti_conducenti.cognome + ' ' + contratti_conducenti.nome) As conducente, " & _
    '    "contratti_conducenti.indirizzo, contratti_conducenti.city, contratti_conducenti.cap, contratti_conducenti.provincia, contratti.giorni_prepagati, " & _
    '    "nazioni.nazione, contratti_conducenti.codfis, contratti_conducenti.email, km_uscita, km_rientro, targa, modello, ISNULL(prenotazione_prepagata,'0') As prenotazione_prepagata " & _
    '    "FROM contratti WITH(NOLOCK) INNER JOIN stazioni As stazione_uscita WITH(NOLOCK) ON contratti.id_stazione_uscita=stazione_uscita.id " & _
    '    "INNER JOIN stazioni As stazione_rientro WITH(NOLOCK) ON contratti.id_stazione_rientro=stazione_rientro.id " & _
    '    "INNER JOIN contratti_conducenti WITH(NOLOCK) ON contratti.id_primo_conducente=contratti_conducenti.id_conducente AND contratti.num_contratto=contratti_conducenti.num_contratto " & _
    '    "LEFT JOIN nazioni WITH(NOLOCK) ON contratti_conducenti.nazione=nazioni.id_nazione " & _
    '    "LEFT JOIN clienti_tipologia WITH(NOLOCK) ON contratti.id_fonte=clienti_tipologia.id " & _
    '    "LEFT JOIN ditte As broker WITH(NOLOCK) ON clienti_tipologia.id_ditta=broker.id_ditta " & _
    '    "WHERE contratti.id='" & id_contratto & "'"

    '    Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '    Dim Rs As Data.SqlClient.SqlDataReader
    '    Rs = Cmd.ExecuteReader()

    '    Dim sqlStr2 As String
    '    Dim sqlStr_broker As String
    '    Dim sqlStr_prepagata As String

    '    Rs.Read()
    '    'INTESTAZIONE FATTURA -----------------------------------------------------------------

    '    'GENERARE NUMERO FATTURA
    '    'DATA FATTURA NEL CASO DI FATTURA GENEREATA ALL'INTERNO DELLA PAGINA DEL CONTRATTO LA DATA DEVE ESSERE PASSATA COME ARGOMENTO
    '    'SALVARE NUMERO VOUCHER (ATTUALMENTE SU PRENOTAZIONE - RIPORTARLO SU CONTRATTO VISTO CHE SERVE ANCHE NELLA STAMPA DELL'RA)

    '    Dim dataFattura As String
    '    If data_fattura <> "" Then
    '        dataFattura = funzioni_comuni.getDataDb_senza_orario2(data_fattura)
    '    Else
    '        If Day(Now()) <= 9 Then
    '            data_fattura = "0" & Day(Now())
    '        Else
    '            data_fattura = Day(Now())
    '        End If

    '        If Month(Now()) <= 9 Then
    '            data_fattura = data_fattura & "/0" & Month(Now())
    '        Else
    '            data_fattura = data_fattura & "/" & Month(Now())
    '        End If
    '        data_fattura = data_fattura & "/" & Year(Now())
    '    End If

    '    Dim anno_fattura As String = Year(dataFattura)

    '    Dim targa As String
    '    Dim modello As String
    '    Dim km_uscita As String
    '    Dim km_rientro As String

    '    Dim giorni As String = Rs("giorni")
    '    Dim giorni_to As String = Rs("giorni_to")
    '    Dim giorni_prepagati As String = Rs("giorni_prepagati") & ""
    '    If giorni_prepagati = "" Then
    '        giorni_prepagati = "0"
    '    End If
    '    Dim importo_a_carico_del_broker As Double = Rs("importo_a_carico_del_broker")
    '    Dim imponibile_a_carico_del_broker As Double
    '    Dim iva_a_carico_del_broker As Double
    '    Dim aliquota_iva As Double

    '    Dim intestazione As String = Rs("conducente")
    '    Dim conducente As String = Rs("conducente")
    '    Dim indirizzo As String = Rs("indirizzo")
    '    Dim citta As String = Rs("city")
    '    Dim cap As String = Rs("cap")
    '    Dim provincia As String = Rs("provincia") & ""
    '    Dim nazione As String = Rs("nazione") & ""
    '    Dim piva As String = ""
    '    Dim codice_fiscale As String = Rs("codfis")
    '    Dim email As String = Rs("email") & ""

    '    Dim id_broker As String = Rs("id_broker") & ""
    '    Dim codice_edp As String = Rs("codice_edp")
    '    Dim num_contratto As String = Rs("num_contratto")
    '    Dim num_prenotazione As String = Rs("num_prenotazione") & ""
    '    Dim num_voucher As String = Rs("rif_to") & ""
    '    If num_voucher = "" Then
    '        num_voucher = num_prenotazione
    '    End If
    '    Dim id_primo_conducente As String = Rs("id_primo_conducente")
    '    Dim id_cliente As String = Rs("id_cliente")

    '    Dim prepagata As Boolean
    '    If Rs("prenotazione_prepagata") Then
    '        prepagata = True
    '    Else
    '        prepagata = False
    '    End If


    '    'NEL CASO DI CRV SU FATTURA DEVE ESSERE SPECIFICATO IL PRIMO VEICOLO
    '    If Rs("num_crv") = "0" Then
    '        targa = Rs("targa")
    '        modello = Rs("modello")
    '        km_uscita = Rs("km_uscita")
    '        km_rientro = Rs("km_rientro")
    '    Else
    '        targa = ""
    '        modello = ""
    '        km_uscita = ""
    '        km_rientro = ""
    '    End If

    '    'NUM_RF/DATA FATTURA: IN QUESTO MOMENTO NON SONO CORRETTI - UTILIZZARE I VALORI UNA VOLTA CAPITO DA OVE VENGONO GENERATI

    '    sqlStr = "INSERT INTO fatture_nolo (attiva, id_tipo_fattura, fattura_cliente, fattura_broker, fattura_costi_prepagati, num_fattura, data_fattura, " & _
    '    "num_rf, data_rf, id_ditta, cod_edp," & _
    '    "num_contratto_rif,num_prenotazione_rif, num_voucher, stazione_uscita, data_uscita, stazione_rientro, data_rientro, conducente,  "

    '    sqlStr2 = " VALUES ( " & _
    '    "'0','2','1','0','0','0','" & funzioni_comuni.getDataDb_con_orario(dataFattura) & "'," & _
    '    "'0','" & funzioni_comuni.getDataDb_con_orario(dataFattura) & "'," & _
    '    "'" & Rs("id_cliente") & "','" & Rs("codice_edp") & "','" & num_contratto & "','" & num_prenotazione & "" & "'," & _
    '    "'" & Replace(num_voucher, "'", "''") & "','" & Replace(Rs("staz_uscita"), "'", "''") & "','" & funzioni_comuni.getDataDb_con_orario(Rs("data_uscita")) & "'," & _
    '    "'" & Replace(Rs("staz_rientro"), "'", "''") & "','" & funzioni_comuni.getDataDb_con_orario(Rs("data_rientro")) & "','" & Replace(conducente, "'", "''") & "',"

    '    If importo_a_carico_del_broker <> 0 Then
    '        'NEL CASO DI PRENOTAZIONE BROKER ALLA FINE DOVRO' SALVARE LA FATTURA DEL BROKER - INIZIO A COSTRUIRE I DATI DA SALVARE VISTO
    '        'CHE MOLTI SONO IN COMUNE TRA LE DUE FATTURE
    '        sqlStr_broker = " VALUES ( " & _
    '        "'0','2','0','1','0','0','" & funzioni_comuni.getDataDb_con_orario(dataFattura) & "'," & _
    '        "'0','" & funzioni_comuni.getDataDb_con_orario(dataFattura) & "'," & _
    '        "'" & Rs("id_broker") & "','" & Rs("codice_edp_broker") & "','" & num_contratto & "','" & num_prenotazione & "" & "'," & _
    '        "'" & Replace(num_voucher, "'", "''") & "','" & Replace(Rs("staz_uscita"), "'", "''") & "','" & funzioni_comuni.getDataDb_con_orario(Rs("data_uscita")) & "'," & _
    '        "'" & Replace(Rs("staz_rientro"), "'", "''") & "','" & funzioni_comuni.getDataDb_con_orario(Rs("data_rientro")) & "','" & Replace(conducente, "'", "''") & "',"
    '    ElseIf prepagata Then
    '        'ANCHE SE LA PRENOTAZIONE E' PREPAGATA E' NECESSARIO CREARE LA FATTURA CON I SOLI COSTI PREPGATAI - INIZIO A PREPARARE LA QUERY
    '        sqlStr_prepagata = " VALUES ( " & _
    '        "'0','2','0','0','1','0','" & funzioni_comuni.getDataDb_con_orario(dataFattura) & "'," & _
    '        "'0','" & funzioni_comuni.getDataDb_con_orario(dataFattura) & "'," & _
    '        "'" & Rs("id_cliente") & "','" & Rs("codice_edp") & "','" & num_contratto & "','" & num_prenotazione & "" & "'," & _
    '        "'" & Replace(num_voucher, "'", "''") & "','" & Replace(Rs("staz_uscita"), "'", "''") & "','" & funzioni_comuni.getDataDb_con_orario(Rs("data_uscita")) & "'," & _
    '        "'" & Replace(Rs("staz_rientro"), "'", "''") & "','" & funzioni_comuni.getDataDb_con_orario(Rs("data_rientro")) & "','" & Replace(conducente, "'", "''") & "',"
    '    End If

    '    Dbc.Close()
    '    Dbc.Open()

    '    'DATI VETTURA: NEL CASO DI CRV LI TROVO IN contratti_crv_veicoli
    '    sqlStr = sqlStr & "targa,modello,km_uscita,km_rientro, "
    '    If targa <> "" Then
    '        sqlStr2 = sqlStr2 & "'" & Replace(targa, "'", "''") & "','" & Replace(modello, "'", "''") & "','" & km_uscita & "','" & km_rientro & "',"
    '        If importo_a_carico_del_broker <> 0 Then
    '            sqlStr_broker = sqlStr_broker & "'" & Replace(targa, "'", "''") & "','" & Replace(modello, "'", "''") & "','" & km_uscita & "','" & km_rientro & "',"
    '        ElseIf prepagata Then
    '            sqlStr_prepagata = sqlStr_prepagata & "'" & Replace(targa, "'", "''") & "','" & Replace(modello, "'", "''") & "','" & km_uscita & "','" & km_rientro & "',"
    '        End If
    '    Else
    '        Dim sql As String = "SELECT km_uscita, km_rientro, veicoli.targa, modelli.descrizione As modello " & _
    '        "FROM contratti_crv_veicoli WITH(NOLOCK) " & _
    '        "INNER JOIN veicoli WITH(NOLOCK) ON contratti_crv_veicoli.id_veicolo=veicoli.id " & _
    '        "INNER JOIN modelli ON veicoli.id_modello=modelli.id_modello WHERE num_contratto='" & num_contratto & "' AND num_crv='0'"
    '        Cmd = New Data.SqlClient.SqlCommand(sql, Dbc)
    '        Rs = Cmd.ExecuteReader()

    '        Rs.Read()

    '        targa = Rs("targa")
    '        modello = Rs("modello")
    '        km_uscita = Rs("km_uscita")
    '        km_rientro = Rs("km_rientro")

    '        sqlStr2 = sqlStr2 & "'" & Replace(targa, "'", "''") & "','" & Replace(modello, "'", "''") & "','" & km_uscita & "'," & _
    '        "'" & km_rientro & "',"

    '        If importo_a_carico_del_broker <> 0 Then
    '            sqlStr_broker = sqlStr_broker & "'" & Replace(targa, "'", "''") & "','" & Replace(modello, "'", "''") & "','" & km_uscita & "'," & _
    '            "'" & km_rientro & "',"
    '        ElseIf prepagata Then
    '            sqlStr_prepagata = sqlStr_prepagata & "'" & Replace(targa, "'", "''") & "','" & Replace(modello, "'", "''") & "','" & km_uscita & "'," & _
    '            "'" & km_rientro & "',"
    '        End If

    '        Dbc.Close()
    '        Dbc.Open()
    '    End If

    '    'INTESTAZIONE FATTURA: NEL CASO DI CLIENTE CASH PRENDO I DATI DA QUELLI DEL PRIMO GUIDATORE ALTRIMENTI DA QUELLI DELLA DITTA
    '    sqlStr = sqlStr & "intestazione, indirizzo, citta, cap, provincia, nazione, piva, codice_fiscale, mail, "
    '    If codice_edp = Costanti.codice_cash Then
    '        sqlStr2 = sqlStr2 & "'" & Replace(intestazione, "'", "''") & "','" & Replace(indirizzo, "'", "''") & "'," & _
    '        "'" & Replace(citta, "'", "''") & "','" & Replace(cap, "'", "''") & "','" & Replace(provincia, "'", "''") & "'," & _
    '        "'" & Replace(nazione, "'", "''") & "','','" & Replace(codice_fiscale, "'", "''") & "','" & Replace(email, "'", "''") & "',"

    '        If prepagata Then
    '            sqlStr_prepagata = sqlStr_prepagata & "'" & Replace(intestazione, "'", "''") & "','" & Replace(indirizzo, "'", "''") & "'," & _
    '        "'" & Replace(citta, "'", "''") & "','" & Replace(cap, "'", "''") & "','" & Replace(provincia, "'", "''") & "'," & _
    '        "'" & Replace(nazione, "'", "''") & "','','" & Replace(codice_fiscale, "'", "''") & "','" & Replace(email, "'", "''") & "',"
    '        End If
    '    Else
    '        Dim sql As String = "SELECT rag_soc, indirizzo, citta, cap, provincia, nazioni.nazione, piva, email, c_fis " & _
    '        "FROM ditte WITH(NOLOCK) LEFT JOIN nazioni WITH(NOLOCK) ON ditte.nazione=nazioni.id_nazione " & _
    '        "WHERE ditte.id_ditta='" & id_cliente & "'"

    '        Cmd = New Data.SqlClient.SqlCommand(sql, Dbc)
    '        Rs = Cmd.ExecuteReader()

    '        Rs.Read()

    '        intestazione = Rs("rag_soc") & ""
    '        indirizzo = Rs("indirizzo") & ""
    '        citta = Rs("citta") & ""
    '        cap = Rs("cap") & ""
    '        provincia = Rs("provincia") & ""
    '        nazione = Rs("nazione") & ""
    '        piva = Rs("piva") & ""
    '        email = Rs("email") & ""
    '        codice_fiscale = Rs("c_fis") & ""

    '        sqlStr2 = sqlStr2 & "'" & Replace(intestazione, "'", "''") & "','" & Replace(indirizzo, "'", "''") & "'," & _
    '                "'" & Replace(citta, "'", "''") & "','" & Replace(cap, "'", "''") & "','" & Replace(provincia, "'", "''") & "'," & _
    '                "'" & Replace(nazione, "'", "''") & "','" & Replace(piva, "'", "''") & "','" & Replace(codice_fiscale, "'", "''") & "','" & Replace(email, "'", "''") & "',"

    '        If prepagata Then
    '            sqlStr_prepagata = sqlStr_prepagata & "'" & Replace(intestazione, "'", "''") & "','" & Replace(indirizzo, "'", "''") & "'," & _
    '                "'" & Replace(citta, "'", "''") & "','" & Replace(cap, "'", "''") & "','" & Replace(provincia, "'", "''") & "'," & _
    '                "'" & Replace(nazione, "'", "''") & "','" & Replace(piva, "'", "''") & "','" & Replace(codice_fiscale, "'", "''") & "','" & Replace(email, "'", "''") & "',"
    '        End If

    '        Dbc.Close()
    '        Dbc.Open()
    '    End If

    '    If importo_a_carico_del_broker <> 0 Then
    '        'IN QUESTO CASO SELEZIONO IL BROKER A CUI FATTURARE - QUESTO DEVE ESSERE SPECIFICATO PER FORZA (BLOCCANTE PER POTER GENERARE FATTURA)

    '        Dim sql As String = "SELECT rag_soc, indirizzo, citta, cap, provincia, nazioni.nazione, piva, email " & _
    '              "FROM ditte WITH(NOLOCK) LEFT JOIN nazioni WITH(NOLOCK) ON ditte.nazione=nazioni.id_nazione " & _
    '              "WHERE ditte.id_ditta='" & id_broker & "'"

    '        Cmd = New Data.SqlClient.SqlCommand(sql, Dbc)
    '        Rs = Cmd.ExecuteReader()

    '        Rs.Read()

    '        intestazione = Rs("rag_soc") & ""
    '        indirizzo = Rs("indirizzo") & ""
    '        citta = Rs("citta") & ""
    '        cap = Rs("cap") & ""
    '        provincia = Rs("provincia") & ""
    '        nazione = Rs("nazione") & ""
    '        piva = Rs("piva") & ""
    '        email = Rs("email") & ""

    '        Dbc.Close()
    '        Dbc.Open()

    '        sqlStr_broker = sqlStr_broker & "'" & Replace(intestazione, "'", "''") & "','" & Replace(indirizzo, "'", "''") & "'," & _
    '        "'" & Replace(citta, "'", "''") & "','" & Replace(cap, "'", "''") & "','" & Replace(provincia, "'", "''") & "'," & _
    '        "'" & Replace(nazione, "'", "''") & "','" & Replace(piva, "'", "''") & "','','" & Replace(email, "'", "''") & "',"
    '    End If

    '    'TOTALE NELL'INTESTAZIONE DELLA FATTURA
    '    If num_prenotazione = "" Then
    '        num_prenotazione = "-1"
    '    End If

    '    'OR N_PREN_RIF='" & num_prenotazione & "'  - IL PREPAGAMENTO NON DEVE MAI ESSERE CONSIDERATO 

    '    Dim sql2 As String = " (SELECT ISNULL(SUM(per_importo),0) FROM pagamenti_extra WITH(NOLOCK) WHERE " & _
    '           " (N_CONTRATTO_RIF='" & num_contratto & "')  " & _
    '           " AND (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Chiusura & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Vendita & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Pagamento_Contanti & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Attivo & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Passivo & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.MCR_Pagamento_Cash & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Deposito_su_RA & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso_su_RA & "') " & _
    '           "AND NOT operazione_stornata='1' AND pagamento_broker='0')  "

    '    Cmd = New Data.SqlClient.SqlCommand(sql2, Dbc)
    '    Dim totale_incassato As Double = Cmd.ExecuteScalar


    '    sql2 = " (SELECT ISNULL(SUM(per_importo),0) FROM pagamenti_extra WITH(NOLOCK) WHERE " & _
    '           " (N_CONTRATTO_RIF='" & num_contratto & "' OR N_PREN_RIF='" & num_prenotazione & "')  " & _
    '           " AND (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Chiusura & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Vendita & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Pagamento_Contanti & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Attivo & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Passivo & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.MCR_Pagamento_Cash & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Deposito_su_RA & "' " & _
    '           "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso_su_RA & "') " & _
    '           "AND NOT operazione_stornata='1' AND pagamento_broker='1')  "

    '    Cmd = New Data.SqlClient.SqlCommand(sql2, Dbc)
    '    Dim totale_incassato_broker As Double = Cmd.ExecuteScalar

    '    'PER IL CONTRATTO PRINCIPALE IL SINGOLO COSTO DEVE ESSERE SOTTRATTO ALL'IMPORTO PREPAGATO - INFATTI NELLA FATTURA PER IL CLIENTE, NEL CASO DI CONTRATTO DA PRENOTAZIONE
    '    'PREPAGATA, VENGONO RIPORTATI SOLAMENTE I COSTI EXTRA RISPETTO A QUANTO PAGATO IN FASE DI PREPAGAMENTO. INOLTRE SE NON CI SONO EXTRA VERRA' EMESSA UNICAMENTE LA FATTURA
    '    'PER GLI IMPORTI PREPAGATI
    '    sql2 = " SELECT (imponibile_scontato+ISNULL(imponibile_onere,0)) As imponibile," & _
    '    "(iva_imponibile_scontato+ISNULL(iva_onere,0)) As iva FROM contratti_costi WITH(NOLOCK) " & _
    '    "WHERE id_documento=" & id_contratto & " AND num_calcolo='" & num_calcolo & "' " & _
    '    "AND nome_costo='" & LCase(Costanti.testo_elemento_totale) & "'"
    '    Cmd = New Data.SqlClient.SqlCommand(sql2, Dbc)
    '    Rs = Cmd.ExecuteReader()

    '    Rs.Read()

    '    Dim imponibile As Double = Rs("imponibile")
    '    Dim iva As Double = Rs("iva")

    '    Dbc.Close()
    '    Dbc.Open()

    '    Dim imponibile_prepagato As Double = 0
    '    Dim iva_prepagata As Double = 0

    '    If prepagata Then
    '        'SELEZIONO IMPONIBILE E ONERE PREPAGATO E SOTTRAGGO IL VALORE ALL'IMPONIBILE E ALL'IVA TOTALI
    '        sql2 = " SELECT SUM(ISNULL(imponibile_scontato_prepagato,0)) As imponibile_prepagato," & _
    '                "SUM(ISNULL(iva_imponibile_scontato_prepagato,0)) As iva_prepagato FROM contratti_costi WITH(NOLOCK) " & _
    '                "WHERE id_documento=" & id_contratto & " AND num_calcolo='" & num_calcolo & "' " & _
    '                "AND prepagato=1"

    '        Cmd = New Data.SqlClient.SqlCommand(sql2, Dbc)
    '        Rs = Cmd.ExecuteReader()
    '        Rs.Read()

    '        imponibile_prepagato = Rs("imponibile_prepagato")
    '        iva_prepagata = Rs("iva_prepagato")

    '        imponibile = imponibile - imponibile_prepagato
    '        iva = iva - iva_prepagata

    '        Dbc.Close()
    '        Dbc.Open()
    '    End If

    '    If importo_a_carico_del_broker <> 0 Then
    '        'NEL CASO DI TARIFFA BROKER DEVO DECURTARE I TOTALI DEL COSTO A CARICO DEL TO
    '        If giorni >= giorni_to Then
    '            'SE I GIORNI DI NOLEGGIO SONO MAGGIORI DEI GIORNI DI VOUCHER DEVO TOGLIERE DAL TOTALE IL TOTALE A CARICO DEL TO
    '            sql2 = "SELECT aliquota_iva FROM contratti_costi WHERE id_documento=" & id_contratto & " AND num_calcolo='" & num_calcolo & "' " & _
    '                    "AND id_elemento='" & LCase(Costanti.ID_tempo_km) & "'"
    '            Cmd = New Data.SqlClient.SqlCommand(sql2, Dbc)

    '            aliquota_iva = Cmd.ExecuteScalar

    '            imponibile_a_carico_del_broker = importo_a_carico_del_broker / (1 + aliquota_iva / 100)
    '            iva_a_carico_del_broker = imponibile_a_carico_del_broker * aliquota_iva / 100
    '        Else
    '            'SE I GIORNI DI NOLEGGIO SONO MINORI DEI GIORNI DI VOUCHER  (CASO DI RIENTRO ANTICIPATO CON GIORNI NON USUFRUITI NON 
    '            'RIMBORSABILI) TOLGO DAL TOTALE IL COSTO DEL VALORE TARIFFA (CHE IN QUESTO CASO NON COINCIDE COL TOTALE A CARICO DEL BROKER
    '            'SALVATO IN contratti MA RISULTA INFERIORE)
    '            sql2 = " SELECT imponibile_scontato+ISNULL(imponibile_onere,0) As imponibile," & _
    '                    "iva_imponibile_scontato+ISNULL(iva_onere,0) As iva, aliquota_iva FROM contratti_costi WITH(NOLOCK) " & _
    '                    "WHERE id_documento=" & id_contratto & " AND num_calcolo='" & num_calcolo & "' " & _
    '                    "AND id_elemento='" & LCase(Costanti.ID_tempo_km) & "'"

    '            Cmd = New Data.SqlClient.SqlCommand(sql2, Dbc)
    '            Rs = Cmd.ExecuteReader()
    '            Rs.Read()

    '            imponibile_a_carico_del_broker = Rs("imponibile")
    '            iva_a_carico_del_broker = Rs("iva")
    '            aliquota_iva = Rs("aliquota_iva")

    '            Dbc.Close()
    '            Dbc.Open()
    '        End If

    '        imponibile = imponibile - imponibile_a_carico_del_broker
    '        iva = iva - iva_a_carico_del_broker

    '        'DA QUESTO MOMENTO IN POI L'IMPONIBILE A CARICO DEL BROKER (PER SUCCESSIVI SALVATAGGI SU DB) TORNA AD ESSERE SEMPRE CALCOLATO
    '        'RISPETTO A QUANTO DEVE EFFETTIVAMENTE PAGARE IL BROKER (SALVATO IN contratti)
    '        imponibile_a_carico_del_broker = importo_a_carico_del_broker / (1 + aliquota_iva / 100)
    '        iva_a_carico_del_broker = imponibile_a_carico_del_broker * aliquota_iva / 100

    '        sqlStr_broker = sqlStr_broker & "'" & Replace(FormatNumber(imponibile_a_carico_del_broker, 2, , , TriState.False), ",", ".") & "'," & _
    '        "'" & Replace(FormatNumber(iva_a_carico_del_broker, 2, , , TriState.False), ",", ".") & "'," & _
    '        "'" & Replace(FormatNumber(imponibile_a_carico_del_broker + iva_a_carico_del_broker, 2, , , TriState.False), ",", ".") & "'," & _
    '        "'" & Replace(FormatNumber(totale_incassato_broker, 2, , , TriState.False), ",", ".") & "'," & _
    '        "'" & Replace(FormatNumber(imponibile_a_carico_del_broker + iva_a_carico_del_broker - totale_incassato_broker, 2, , , TriState.False), ",", ".") & "')"
    '    ElseIf prepagata Then
    '        'sqlStr_prepagata = sqlStr_prepagata & "0,0,0,0,0,"
    '        sqlStr_prepagata = sqlStr_prepagata & "'" & imponibile_prepagato.ToString.Replace(",", ".") & "','" & iva_prepagata.ToString.Replace(",", ".") & "'," & _
    '            "'" & (iva_prepagata + imponibile_prepagato).ToString.Replace(",", ".") & "','" & (iva_prepagata + imponibile_prepagato).ToString.Replace(",", ".") & "',0,"
    '    End If

    '    sqlStr = sqlStr & "imponibile, iva, totale_fattura, totale_pagamenti, saldo, "
    '    sqlStr2 = sqlStr2 & "'" & Replace(FormatNumber(imponibile, 2, , , TriState.False), ",", ".") & "'," & _
    '    "'" & Replace(FormatNumber(iva, 2, , , TriState.False), ",", ".") & "'," & _
    '    "'" & Replace(FormatNumber(imponibile + iva, 2, , , TriState.False), ",", ".") & "'," & _
    '    "'" & Replace(FormatNumber(totale_incassato, 2, , , TriState.False), ",", ".") & "'," & _
    '    "'" & Replace(FormatNumber(imponibile + iva - totale_incassato, 2, , , TriState.False), ",", ".") & "',"


    '    'SE IL CONTRATTO DERIVA DA UNA PRENOTAZIONE CONTROLLO SE ESISTE UNA FATTURA DI PREPAGAMENTO SALVANDONE IL RIFERIMENTO 
    '    Dim data_fattura_prepagato As String = "NULL"
    '    Dim num_fattura_prepagato As String = "NULL"

    '    If num_prenotazione <> "" Then
    '        Cmd = New Data.SqlClient.SqlCommand("SELECT codice_fattura, data_fattura FROM fatture WITH(NOLOCK) WHERE tipo_fattura='1' AND id_riferimento='" & num_prenotazione & "'", Dbc)
    '        Rs = Cmd.ExecuteReader()

    '        If Rs.Read() Then
    '            data_fattura_prepagato = "'" & funzioni_comuni.getDataDb_senza_orario(Rs("data_fattura")) & "'"
    '            num_fattura_prepagato = "'" & Rs("codice_fattura") & "'"
    '        End If

    '        Dbc.Close()
    '        Dbc.Open()
    '    End If

    '    sqlStr = sqlStr & "data_fattura_prepagato, numero_fattura_prepagato)"
    '    sqlStr2 = sqlStr2 & "NULL,NULL)"

    '    If prepagata Then
    '        sqlStr_prepagata = sqlStr_prepagata & data_fattura_prepagato & "," & num_fattura_prepagato & ")"
    '    End If

    '    Dim id_fattura As String
    '    'LA FATTURA CLIENTE NON VA CREATA SE L'IMPONIBILE E' ZERO E ANCHE IL TOTALE INCASSATO E' A ZERO (DOVREBBE ESSERE ZERO SOLO NEL CASO DI PREPAGATE/BROKER CON NESSUN ADDEBITO AL BANCO)
    '    'VA PERO' CREATA SE C'E' ALMENO UN PAGAMENTO RD (LA RIGA RDS VIENE AGGIUNTA IN FASE DI STAMPA) O SE E' COMPLIMENTARY
    '    'HttpContext.Current.Trace.Write("IMPONIBILE --- > ", FormatNumber(imponibile, 2, , , TriState.False))

    '    If FormatNumber(imponibile, 2, , , TriState.False) <> "0,00" Or FormatNumber(totale_incassato, 2, , , TriState.False) <> "0,00" Or EsisteImponibileRDS(num_contratto) OrElse Pagamenti.is_complimentary("", num_contratto) Then
    '        'HttpContext.Current.Trace.Write("IMPONIBILE --- > ", imponibile)
    '        Cmd = New Data.SqlClient.SqlCommand(sqlStr & " " & sqlStr2, Dbc)
    '        Cmd.ExecuteNonQuery()

    '        'RECUPERO L'ID DELLA FATTURA APPENA CREATA 
    '        sqlStr = "SELECT @@IDENTITY FROM fatture_nolo"
    '        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '        id_fattura = Cmd.ExecuteScalar
    '    Else
    '        id_fattura = "0"
    '    End If

    '    Dim aliquota_iva_tempo_km As String
    '    Dim codice_iva_tempo_km As String
    '    Dim iva_elemento As String

    '    Dim insert_prepagati(50) As String
    '    Dim insert_pagamenti_broker(50) As String

    '    insert_pagamenti_broker(0) = "0"

    '    Dim k As Integer = 0
    '    Dim i As Integer = 0

    '    'RIGHE DELLA FATTURA --------------------------------------------------------------------------------------------------------------
    '    Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '    Dbc2.Open()

    '    sqlStr = "SELECT id_elemento, nome_costo, ISNULL(imponibile_scontato,0) As imponibile_scontato, ISNULL(iva_imponibile_scontato,0) As iva_imponibile_scontato, ISNULL(imponibile_scontato_prepagato,0) As imponibile_scontato_prepagato," & _
    '        "ISNULL(iva_imponibile_scontato_prepagato,0) As iva_imponibile_scontato_prepagato, qta, aliquota_iva, ISNULL(codice_iva,'') As codice_iva, ISNULL(prepagato,'0') As prepagato FROM contratti_costi WITH(NOLOCK) " & _
    '        "WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND selezionato='1' " & _
    '        "AND (id_a_carico_di<>'" & Costanti.id_accessorio_incluso & "' OR id_elemento='" & Costanti.ID_tempo_km & "') AND ISNULL(omaggiato,'0')='0' " & _
    '        "ORDER BY ordine_stampa ASC"
    '    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '    Rs = Cmd.ExecuteReader()

    '    Do While Rs.Read()
    '        imponibile = Rs("imponibile_scontato") - Rs("imponibile_scontato_prepagato")
    '        iva_elemento = Rs("iva_imponibile_scontato") - Rs("iva_imponibile_scontato_prepagato")

    '        If Rs("id_elemento") = Costanti.ID_tempo_km And importo_a_carico_del_broker <> "0" Then
    '            'TEMPO KM DELLA TARIFFA BROKER
    '            If giorni >= giorni_to Then
    '                imponibile = imponibile - imponibile_a_carico_del_broker
    '                iva_elemento = iva_elemento - iva_a_carico_del_broker
    '            Else
    '                imponibile = 0
    '                iva_elemento = 0
    '            End If
    '            'NE APPROFITTO PER SALVARE L'ALIQUOTA IVA DEL TEMPO KM DA UTILIZZARE EVENTUALMENTE PER SCORPORARE SUCCESSIVAMENTE
    '            'L'IMPORTO PREPAGATO
    '            aliquota_iva_tempo_km = Rs("aliquota_iva")
    '            codice_iva_tempo_km = Rs("codice_iva") & ""
    '        ElseIf Rs("id_elemento") = Costanti.ID_tempo_km Then
    '            'NE APPROFITTO PER SALVARE L'ALIQUOTA IVA DEL TEMPO KM DA UTILIZZARE EVENTUALMENTE PER SCORPORARE SUCCESSIVAMENTE
    '            'L'IMPORTO PREPAGATO
    '            aliquota_iva_tempo_km = Rs("aliquota_iva")
    '            codice_iva_tempo_km = Rs("codice_iva") & ""
    '        End If
    '        Dim qta As Integer
    '        If imponibile <> 0 Then
    '            'HttpContext.Current.Trace.Write(Rs("nome_costo") & " " & iva_elemento & " " & imponibile)

    '            'FATTURA CLIENTE
    '            qta = Rs("qta")
    '            If Rs("prepagato") And qta > CInt(giorni_prepagati) Then
    '                qta = qta - CInt(giorni_prepagati)
    '            End If

    '            'NEL CASO DI FATTURA CLIENTE A ZERO LA FATTURA NON E' STATA CREATA E QUINDI NON EFFETTUO L'INSERT
    '            If id_fattura <> "0" Then
    '                sqlStr2 = "INSERT INTO fatture_nolo_righe (id_fattura, descrizione, quantita, costo_unitario, iva, totale, aliquota_iva, codice_iva) VALUES (" & _
    '            "'" & id_fattura & "','" & Replace(Rs("nome_costo"), "'", "''") & "','" & qta & "'," & _
    '            "'" & Replace(FormatNumber(imponibile / qta, 2, , , TriState.False), ",", ".") & "','" & Replace(FormatNumber(iva_elemento, 2, , , TriState.False), ",", ".") & "'," & _
    '            "'" & Replace(FormatNumber(imponibile, 2, , , TriState.False), ",", ".") & "','" & Replace(Rs("aliquota_iva"), ",", ".") & "','" & Rs("codice_iva") & "" & "')"
    '                Cmd = New Data.SqlClient.SqlCommand(sqlStr2, Dbc2)
    '                Cmd.ExecuteNonQuery()
    '            End If
    '        End If
    '        If Rs("prepagato") Then
    '            'CONSERVO L'INSERT PER GLI ELEMENTI PREPAGATI PER LA FATTURA APPPOSITA
    '            qta = Rs("qta")
    '            If qta > CInt(giorni_prepagati) Then
    '                qta = CInt(giorni_prepagati)
    '            End If

    '            insert_prepagati(i) = "INSERT INTO fatture_nolo_righe (id_fattura, descrizione, quantita, costo_unitario, iva, totale, aliquota_iva, codice_iva) VALUES (" & _
    '                "'ID_FATT_PREP1234','" & Replace(Rs("nome_costo"), "'", "''") & "','" & qta & "'," & _
    '                "'" & Replace(FormatNumber(Rs("imponibile_scontato_prepagato") / qta, 2, , , TriState.False), ",", ".") & "','" & Replace(FormatNumber(Rs("iva_imponibile_scontato_prepagato"), 2, , , TriState.False), ",", ".") & "'," & _
    '                "'" & Replace(FormatNumber(Rs("imponibile_scontato_prepagato"), 2, , , TriState.False), ",", ".") & "','" & Replace(Rs("aliquota_iva"), ",", ".") & "','" & Rs("codice_iva") & "" & "')"

    '            i = i + 1
    '        End If
    '    Loop
    '    insert_prepagati(i) = "0"
    '    '----------------------------------------------------------------------------------------------------------------------------------
    '    'RIGHE DI PAGAMENTO ---------------------------------------------------------------------------------------------------------------
    '    Dbc.Close()
    '    Dbc.Open()

    '    'SE IL CONTRATTO PROVIENE DA UNA PRENOTAZIONE SELEZIONO ANCHE LE EVENTUALI VENDITE SU PRENOTAZIONE PER DETERMNARE L'IMPORTO PREPAGATO
    '    'CHE NELLA FATTURA PER IL CLIENTE DEVE ESSERE AGGIUNTO TRA LE RIGHE DEI SERVIZI
    '    Dim importo_prepagato As Double = 0
    '    Dim importo_pagato_broker As Double = 0
    '    Dim sql_prepagato As String = ""

    '    If num_prenotazione <> "" Then
    '        sql_prepagato = "(N_CONTRATTO_RIF='" & num_contratto & "' OR N_PREN_RIF='" & num_prenotazione & "' OR pagamenti_extra.N_RDS_RIF IN (SELECT id_rds FROM veicoli_evento_apertura_danno WITH(NOLOCK) WHERE id_documento_apertura = '" & num_contratto & "')) "
    '    Else
    '        sql_prepagato = "N_CONTRATTO_RIF='" & num_contratto & "' OR pagamenti_extra.N_RDS_RIF IN (SELECT id_rds FROM veicoli_evento_apertura_danno WITH(NOLOCK) WHERE id_documento_apertura = '" & num_contratto & "'))"
    '    End If

    '    'ATTENZIONE: RIMBORSO SU RA SERVE PERCHE' CON QUESTA VOCE SI BILANCIA L'EVENTUALE CHIUSURA A 5 CENTESIMI DELLE PREAUTORIZZAZIONI
    '    'TENERNE CONTO SE SI DOVESSE DECIDERE DI NON RIPORTARE I DEPOSITI CAUZIONALI IN CONTANTI E I CORRISPETTIVI RIMBORSI SU FATTURA.

    '    sqlStr = "SELECT pagamenti_extra.data, mod_pag.descrizione As modalita_pagamento, POS_Funzioni.Funzione As tipo_pagamento, ID_TIPPAG, pagamento_broker,  " & _
    '    "pagamenti_extra.PER_IMPORTO, N_CONTRATTO_RIF,  N_RDS_RIF, N_PREN_RIF, codici_contabili.codice_contabile, pagamenti_extra.id_ctr FROM PAGAMENTI_EXTRA WITH(NOLOCK) " & _
    '    "LEFT JOIN MOD_PAG WITH(NOLOCK) ON pagamenti_extra.ID_ModPag=MOD_PAG.ID_ModPag " & _
    '    "LEFT JOIN POS_Funzioni WITH(NOLOCK) ON PAGAMENTI_EXTRA.id_pos_funzioni_ares=POS_Funzioni.id " & _
    '    "LEFT JOIN codici_contabili WITH(NOLOCK) ON pagamenti_extra.ID_ModPag=codici_contabili.modpag AND pagamenti_extra.ID_TipPag=codici_contabili.tippag " & _
    '    "WHERE " & sql_prepagato & _
    '    "AND NOT operazione_stornata='1' ORDER BY pagamenti_extra.data ASC"


    '    '(PERCHE' INCLUDERE SOLO ALCUNI PAGAMENTI????)
    '    'If num_prenotazione <> "" Then
    '    '    sql_prepagato = "(N_CONTRATTO_RIF='" & num_contratto & "' OR (N_PREN_RIF='" & num_prenotazione & "' AND (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Vendita & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Pagamento_Contanti & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.MCR_Pagamento_Cash & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Chiusura & "')) OR pagamenti_extra.N_RDS_RIF IN (SELECT id_rds FROM veicoli_evento_apertura_danno WITH(NOLOCK) WHERE id_documento_apertura = '" & num_contratto & "')) "
    '    'Else
    '    '    sql_prepagato = "N_CONTRATTO_RIF='" & num_contratto & "' "
    '    'End If
    '    'sqlStr = "SELECT pagamenti_extra.data, mod_pag.descrizione As modalita_pagamento, POS_Funzioni.Funzione As tipo_pagamento,  " & _
    '    '"pagamenti_extra.PER_IMPORTO, N_CONTRATTO_RIF, codici_contabili.codice_contabile, pagamenti_extra.id_ctr FROM PAGAMENTI_EXTRA WITH(NOLOCK) " & _
    '    '"LEFT JOIN MOD_PAG WITH(NOLOCK) ON pagamenti_extra.ID_ModPag=MOD_PAG.ID_ModPag " & _
    '    '"LEFT JOIN POS_Funzioni WITH(NOLOCK) ON PAGAMENTI_EXTRA.id_pos_funzioni_ares=POS_Funzioni.id " & _
    '    '"LEFT JOIN codici_contabili WITH(NOLOCK) ON pagamenti_extra.ID_ModPag=codici_contabili.modpag AND pagamenti_extra.ID_TipPag=codici_contabili.tippag " & _
    '    '"WHERE " & sql_prepagato & _
    '    '"AND (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Chiusura & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Vendita & "' " & _
    '    '"OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Pagamento_Contanti & "' " & _
    '    '"OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso & "' " & _
    '    '"OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Attivo & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Passivo & "' " & _
    '    '"OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Deposito_su_RA & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso_su_RA & "') " & _
    '    '"AND NOT operazione_stornata='1' ORDER BY pagamenti_extra.data ASC"
    '    'HttpContext.Current.Trace.Write(sqlStr)
    '    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '    Rs = Cmd.ExecuteReader()


    '    Do While Rs.Read()
    '        Dim modalita_pagamento As String = Rs("modalita_pagamento") & ""
    '        If modalita_pagamento = "" And (Rs("ID_TIPPAG") & "") = "1011098650" Then
    '            modalita_pagamento = "C.CREDITO"
    '        End If
    '        If ((Rs("n_contratto_rif") & "") <> "" Or (Rs("N_RDS_RIF") & "") <> "") And Not Rs("pagamento_broker") Then
    '            'SOLO NEL CASO IN CUI LA FATTURA E' STATA CREATA, QUINDI CON IMPONIBILE DIVERSO DA ZERO
    '            If id_fattura <> "0" Then
    '                'IN QUESTO CASO SI TRATTA DI UN PAGAMENTO SU CONTRATTO - REGISTRO L'OPERAZIONE IN fatture_nolo_pagamenti
    '                sqlStr2 = "INSERT INTO fatture_nolo_pagamenti (id_fattura_nolo, data_pagamento, modalita_pagamento, tipo_pagamento, codice_contabile, importo, id_ctr_pagamenti_extra) " & _
    '                "VALUES ('" & id_fattura & "','" & funzioni_comuni.getDataDb_senza_orario(Rs("data")) & "'," & _
    '                "'" & Replace(modalita_pagamento, "'", "''") & "','" & Replace(Rs("tipo_pagamento") & "", "'", "''") & "'," & _
    '                "'" & Rs("codice_contabile") & "" & "'," & _
    '                "'" & Replace(Rs("per_importo"), ",", ".") & "','" & Rs("id_ctr") & "')"

    '                Cmd = New Data.SqlClient.SqlCommand(sqlStr2, Dbc2)
    '                Cmd.ExecuteNonQuery()
    '            End If
    '        ElseIf (Rs("N_PREN_RIF") & "") <> "" And Not Rs("pagamento_broker") Then
    '            'IN QUESTO CASO SI TRATTA DI PREPAGAMENTO (SU PRENOTAZIONE) PER CUI REGISTRO L'IMPORTO PREPAGATO PER POTER INSERIRE LA RIGA
    '            'DI PREPAGAMENTO TRA I SERVIZI
    '            importo_prepagato = importo_prepagato + Rs("per_importo")

    '            insert_prepagati(i) = "INSERT INTO fatture_nolo_pagamenti (id_fattura_nolo, data_pagamento, modalita_pagamento, tipo_pagamento, codice_contabile, importo, id_ctr_pagamenti_extra) " & _
    '                "VALUES ('ID_FATT_PREP1234','" & funzioni_comuni.getDataDb_senza_orario(Rs("data")) & "'," & _
    '                "'" & Replace(modalita_pagamento, "'", "''") & "','" & Replace(Rs("tipo_pagamento") & "", "'", "''") & "'," & _
    '                "'" & Rs("codice_contabile") & "" & "'," & _
    '                "'" & Replace(Rs("per_importo"), ",", ".") & "','" & Rs("id_ctr") & "')"

    '            i = i + 1

    '            insert_prepagati(i) = "0"
    '        ElseIf Rs("pagamento_broker") Then
    '            'IN QUESTO CASO SI TRATTA DI PAGAMENTO BROKER  PER CUI REGISTRO L'IMPORTO PER POTER INSERIRE LA RIGA
    '            importo_pagato_broker = importo_pagato_broker + Rs("per_importo")

    '            insert_pagamenti_broker(k) = "INSERT INTO fatture_nolo_pagamenti (id_fattura_nolo, data_pagamento, modalita_pagamento, tipo_pagamento, codice_contabile, importo, id_ctr_pagamenti_extra) " & _
    '                "VALUES ('ID_FATT_BROKER1234','" & funzioni_comuni.getDataDb_senza_orario(Rs("data")) & "'," & _
    '                "'" & Replace(modalita_pagamento, "'", "''") & "','" & Replace(Rs("tipo_pagamento") & "", "'", "''") & "'," & _
    '                "'" & Rs("codice_contabile") & "" & "'," & _
    '                "'" & Replace(Rs("per_importo"), ",", ".") & "','" & Rs("id_ctr") & "')"

    '            k = k + 1

    '            insert_pagamenti_broker(k) = "0"
    '        End If
    '    Loop

    '    Dbc.Close()
    '    Dbc.Open()
    '    '----------------------------------------------------------------------------------------------------------------------------------
    '    'Dim iva_prepagato As Double

    '    'If importo_prepagato <> 0 Then
    '    '    'SALVATAGGIO TRA I SERVIZI DELLA RIGA DI PREPAGAMENTO - SCORPORO L'IVA DALL'IMPORTO PREPAGATO UTILIZZANDO L'IVA DEL TEMPO KM
    '    '    importo_prepagato = (importo_prepagato / (1 + aliquota_iva_tempo_km / 100)) * -1
    '    '    iva_prepagato = importo_prepagato * aliquota_iva_tempo_km / 100
    '    '    insert_prepagati(i) = "INSERT INTO fatture_nolo_righe (id_fattura, descrizione, quantita, costo_unitario,iva, totale, aliquota_iva, codice_iva) VALUES (" & _
    '    '        "'ID_FATT_PREP1234','PREPAGATO',1," & _
    '    '        "'" & Replace(FormatNumber(importo_prepagato, 2, , , TriState.False), ",", ".") & "'," & _
    '    '        "'" & Replace(FormatNumber(iva_prepagato, 2, , , TriState.False), ",", ".") & "'," & _
    '    '        "'" & Replace(FormatNumber(importo_prepagato, 2, , , TriState.False), ",", ".") & "','" & Replace(aliquota_iva_tempo_km, ",", ".") & "','" & codice_iva_tempo_km & "')"
    '    '    insert_prepagati(i + 1) = "0"
    '    'End If
    '    '-------------------------------------------------------------------------------------------------------------------------------
    '    Dim id_fattura_broker As String = ""
    '    Dim id_fattura_prepagato As String = ""

    '    If importo_a_carico_del_broker <> 0 Then
    '        'FATTURA BROKER ----------------------------------------------------------------------------------------------------------------
    '        'L'INTESTAZIONE DELLA FATTURA E' GIA' STATA CREATA - PROCEDO AL SALVATAGGIO
    '        sqlStr = "INSERT INTO fatture_nolo (attiva, id_tipo_fattura,fattura_cliente, fattura_broker, fattura_costi_prepagati, num_fattura, data_fattura, num_rf, data_rf, id_ditta, cod_edp, num_contratto_rif, " & _
    '        "num_prenotazione_rif, num_voucher, stazione_uscita, data_uscita, stazione_rientro, data_rientro, conducente,  " & _
    '        "targa,modello,km_uscita,km_rientro,intestazione, indirizzo, citta, cap, provincia, nazione, piva, codice_fiscale, mail," & _
    '        "imponibile, iva, totale_fattura, totale_pagamenti, saldo) " & sqlStr_broker

    '        'HttpContext.Current.Trace.Write(sqlStr)

    '        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
    '        Cmd.ExecuteNonQuery()


    '        sqlStr = "SELECT @@IDENTITY FROM fatture_nolo WITH(NOLOCK)"
    '        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
    '        id_fattura_broker = Cmd.ExecuteScalar

    '        'RIGA FATTURA - A CARICO DEL BROKER C'E' UNICAMENTE IL TEMPO KM 
    '        sqlStr = "INSERT INTO fatture_nolo_righe (id_fattura, descrizione, quantita, costo_unitario, iva, totale, aliquota_iva, codice_iva) VALUES (" & _
    '              "'" & id_fattura_broker & "','Valore Tariffa','" & giorni_to & "'," & _
    '              "'" & Replace(FormatNumber(imponibile_a_carico_del_broker / giorni_to, 2, , , TriState.False), ",", ".") & "'," & _
    '              "'" & Replace(FormatNumber(iva_a_carico_del_broker, 2, , , TriState.False), ",", ".") & "'," & _
    '              "'" & Replace(FormatNumber(imponibile_a_carico_del_broker, 2, , , TriState.False), ",", ".") & "','" & aliquota_iva_tempo_km & "','" & codice_iva_tempo_km & "')"
    '        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '        Cmd.ExecuteNonQuery()

    '        'REGISTRO I PAGAMENTI BROKER
    '        k = 0

    '        Do While insert_pagamenti_broker(k) <> "0"
    '            'SALVO I PAGAMENTI BROKER CHE HO PRECEDENTEMENTE MEMORIZZATO
    '            Cmd = New Data.SqlClient.SqlCommand(Replace(insert_pagamenti_broker(k), "ID_FATT_BROKER1234", id_fattura_broker), Dbc2)
    '            Cmd.ExecuteNonQuery()

    '            k = k + 1
    '        Loop
    '    ElseIf prepagata Then
    '        'SALVATAGGIO DELLA FATTURA PREPAGATA A 0 - A DIFFERENZA DELLA FATTURA DEI COSTI A CARICO DEL CLIENTE QUESTA FATTURA VIENE SEMPRE CREATA
    '        sqlStr = "INSERT INTO fatture_nolo (attiva, id_tipo_fattura,fattura_cliente, fattura_broker, fattura_costi_prepagati, num_fattura, data_fattura, num_rf, data_rf, id_ditta, cod_edp, num_contratto_rif, " & _
    '        "num_prenotazione_rif, num_voucher, stazione_uscita, data_uscita, stazione_rientro, data_rientro, conducente,  " & _
    '        "targa,modello,km_uscita,km_rientro,intestazione, indirizzo, citta, cap, provincia, nazione, piva, codice_fiscale, mail," & _
    '        "imponibile, iva, totale_fattura, totale_pagamenti, saldo,  data_fattura_prepagato, numero_fattura_prepagato) " & sqlStr_prepagata
    '        'HttpContext.Current.Trace.Write(sqlStr)

    '        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
    '        Cmd.ExecuteNonQuery()

    '        sqlStr = "SELECT @@IDENTITY FROM fatture_nolo WITH(NOLOCK)"
    '        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
    '        id_fattura_prepagato = Cmd.ExecuteScalar

    '        i = 0

    '        Do While insert_prepagati(i) <> "0"
    '            'SALVO LE RIGHE DEGLI ELEMENTI PREPAGATI CHE HO PRECEDENTEMENTE MEMORIZZATO
    '            Cmd = New Data.SqlClient.SqlCommand(Replace(insert_prepagati(i), "ID_FATT_PREP1234", id_fattura_prepagato), Dbc2)
    '            Cmd.ExecuteNonQuery()

    '            i = i + 1
    '        Loop
    '    End If

    '    'SE TUTTO E' ANDATO A BUON FINE RENDO ATTIVE LE DUE FATTURE ASSEGNANDONE IL NUMERO E CAMBIO LO STATO DEL CONTRATTO A "FATTURATO"
    '    Dim num_fattura_cliente As String
    '    Dim num_fattura_broker As String
    '    Dim num_fattura_prepagata As String

    '    'SE DALL'ESTERNO E' STATO SPECIFICATO UN NUMERO DI FATTURA (SOLO PER ASSEGNAZIONE SINGOLO CONTRATTO) ALLORA VIENE UTILIZZATO QUEL VALORE. ATTENZIONE: PRIMA DI CHIAMARE
    '    'QUESTO METODO CI SI DEVE ACCERTARE CHE I NUMERI FATTURA (CLIENTE ED EVENTUALMENTE BROKER) SONO DISPONIBILI.
    '    If num_fattura = "" Then
    '        If id_fattura <> "0" Then
    '            'SE LA FATTURA CLIENTE E' STATA CREATA
    '            num_fattura_cliente = Contatori.getContatore_fatture_nolo(anno_fattura)
    '        End If
    '    Else
    '        num_fattura_cliente = num_fattura
    '    End If

    '    If id_fattura <> "0" Then
    '        Cmd = New Data.SqlClient.SqlCommand("UPDATE fatture_nolo SET attiva='1', da_esportare_contabilita='1', num_fattura='" & num_fattura_cliente & "' WHERE id='" & id_fattura & "'", Dbc)
    '        Cmd.ExecuteNonQuery()
    '    End If

    '    If importo_a_carico_del_broker <> 0 Then
    '        If num_fattura = "" Then
    '            num_fattura_broker = Contatori.getContatore_fatture_nolo(anno_fattura)
    '        Else
    '            If id_fattura <> "0" Then
    '                num_fattura_broker = CInt(num_fattura) + 1
    '            Else
    '                num_fattura_broker = CInt(num_fattura)
    '            End If
    '        End If

    '        Cmd = New Data.SqlClient.SqlCommand("UPDATE fatture_nolo SET attiva='1', num_fattura='" & num_fattura_broker & "', da_esportare_contabilita='1' WHERE id='" & id_fattura_broker & "'", Dbc)
    '        Cmd.ExecuteNonQuery()

    '        Dim update_fattura_cliente As String = ""
    '        If id_fattura <> "0" Then
    '            update_fattura_cliente = "id_fattura_cliente='" & id_fattura & "',"
    '        End If

    '        Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti SET status='6', fatturazione_da_controllare='0', non_fatturare='0', " & update_fattura_cliente & " id_fattura_broker='" & id_fattura_broker & "' WHERE id='" & id_contratto & "'", Dbc)
    '        Cmd.ExecuteNonQuery()
    '    ElseIf prepagata Then
    '        If num_fattura = "" Then
    '            num_fattura_prepagata = Contatori.getContatore_fatture_nolo(anno_fattura)
    '        Else
    '            If id_fattura <> "0" Then
    '                num_fattura_prepagata = CInt(num_fattura) + 1
    '            Else
    '                num_fattura_prepagata = CInt(num_fattura)
    '            End If

    '        End If

    '        Dim update_fattura_cliente As String = ""
    '        If id_fattura <> "0" Then
    '            update_fattura_cliente = "id_fattura_cliente='" & id_fattura & "',"
    '        End If

    '        Cmd = New Data.SqlClient.SqlCommand("UPDATE fatture_nolo SET attiva='1', num_fattura='" & num_fattura_prepagata & "', da_esportare_contabilita='1' WHERE id='" & id_fattura_prepagato & "'", Dbc)
    '        Cmd.ExecuteNonQuery()
    '        Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti SET status='6', fatturazione_da_controllare='0', non_fatturare='0', " & update_fattura_cliente & " id_fattura_prepagata='" & id_fattura_prepagato & "' WHERE id='" & id_contratto & "'", Dbc)
    '        Cmd.ExecuteNonQuery()
    '    Else

    '        'TECNICAMENTE NON DOVREBBE MAI ACCADERE CHE A QUESTO PUNTO L'IMPONIBILE SIA A ZERO E LA FATTURA NON SIA STATA CREATA
    '        If id_fattura <> "0" Then
    '            Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti SET status='6', fatturazione_da_controllare='0', non_fatturare='0', id_fattura_cliente='" & id_fattura & "' WHERE id='" & id_contratto & "'", Dbc)
    '            Cmd.ExecuteNonQuery()
    '        End If
    '    End If

    '    Rs.Close()
    '    Rs = Nothing
    '    Cmd.Dispose()
    '    Cmd = Nothing
    '    Dbc.Close()
    '    Dbc.Dispose()
    '    Dbc = Nothing
    '    Dbc2.Close()
    '    Dbc2.Dispose()
    '    Dbc2 = Nothing
    'End Sub

    Public Shared Sub genera_fattura_ra(ByVal id_contratto As String, ByVal num_calcolo As String, ByVal data_fattura As String, Optional ByVal num_fattura As String = "")

        'NEL CASO DI PREPAGATO QUESTA FUNZIONE GENERA UNA FATTURA PREPAGATA ED UNA FATTURA PER IL CONTRATTO
        'FATTURA A CARICO DEL CLIENTE
        Dim sqla As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            '# aggiunto salvo 21.02.2023
            'verifica se CF di conducenti = a quello dell'anagrafica e in caso negativo lo aggiorna
            Dim VerificaDati As String = funzioni_comuni_new.VerificaDatiConducenteFattura(id_contratto, num_calcolo)









            Dim sqlStr As String = "SELECT ISNULL(contratti.importo_a_carico_del_broker,0) As importo_a_carico_del_broker, num_calcolo,"
            sqlStr += "giorni, ISNULL(giorni_to,0) As giorni_to, rif_to, broker.[codice edp] As codice_edp_broker, broker.id_ditta As id_broker,"
            sqlStr += " data_rientro,targa, modello, contratti.codice_edp, contratti.id_cliente, contratti.num_contratto, num_prenotazione, num_crv, "
            sqlStr += "(stazione_uscita.codice + ' ' + stazione_uscita.nome_stazione) As staz_uscita, "
            sqlStr += "(stazione_rientro.codice + ' ' + stazione_rientro.nome_stazione) As staz_rientro, data_uscita, data_rientro, "
            sqlStr += "id_primo_conducente, (contratti_conducenti.cognome + ' ' + contratti_conducenti.nome) As conducente, "
            sqlStr += "contratti_conducenti.indirizzo, contratti_conducenti.city, contratti_conducenti.cap, contratti_conducenti.provincia, contratti.giorni_prepagati, "
            sqlStr += "nazioni.nazione, contratti_conducenti.codfis, contratti_conducenti.email, km_uscita, km_rientro, targa, modello, ISNULL(prenotazione_prepagata,'0') As prenotazione_prepagata "
            sqlStr += "FROM contratti WITH(NOLOCK) INNER JOIN stazioni As stazione_uscita WITH(NOLOCK) ON contratti.id_stazione_uscita=stazione_uscita.id "
            sqlStr += "INNER JOIN stazioni As stazione_rientro WITH(NOLOCK) ON contratti.id_stazione_rientro=stazione_rientro.id "
            sqlStr += "INNER JOIN contratti_conducenti WITH(NOLOCK) ON contratti.id_primo_conducente=contratti_conducenti.id_conducente AND contratti.num_contratto=contratti_conducenti.num_contratto "
            sqlStr += "LEFT JOIN nazioni WITH(NOLOCK) ON contratti_conducenti.nazione=nazioni.id_nazione "
            sqlStr += "LEFT JOIN clienti_tipologia WITH(NOLOCK) ON contratti.id_fonte=clienti_tipologia.id "
            sqlStr += "LEFT JOIN ditte As broker WITH(NOLOCK) ON clienti_tipologia.id_ditta=broker.id_ditta "
            sqlStr += "WHERE contratti.id='" & id_contratto & "'"

            sqla = sqlStr

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Dim sqlStr2 As String
            Dim sqlStr_broker As String

            Rs.Read()
            'INTESTAZIONE FATTURA -----------------------------------------------------------------

            'GENERARE NUMERO FATTURA
            'DATA FATTURA NEL CASO DI FATTURA GENEREATA ALL'INTERNO DELLA PAGINA DEL CONTRATTO LA DATA DEVE ESSERE PASSATA COME ARGOMENTO
            'SALVARE NUMERO VOUCHER (ATTUALMENTE SU PRENOTAZIONE - RIPORTARLO SU CONTRATTO VISTO CHE SERVE ANCHE NELLA STAMPA DELL'RA)

            Dim dataFattura As String
            If data_fattura <> "" Then
                dataFattura = funzioni_comuni.getDataDb_senza_orario2(data_fattura)
            Else
                If Day(Now()) <= 9 Then
                    data_fattura = "0" & Day(Now())
                Else
                    data_fattura = Day(Now())
                End If

                If Month(Now()) <= 9 Then
                    data_fattura = data_fattura & "/0" & Month(Now())
                Else
                    data_fattura = data_fattura & "/" & Month(Now())
                End If
                data_fattura = data_fattura & "/" & Year(Now())
            End If

            Dim anno_fattura As String = Year(dataFattura)

            Dim targa As String
            Dim modello As String
            Dim km_uscita As String
            Dim km_rientro As String

            Dim giorni As String = Rs("giorni")
            Dim giorni_to As String = Rs("giorni_to")
            Dim giorni_prepagati As String = Rs("giorni_prepagati") & ""
            If giorni_prepagati = "" Then
                giorni_prepagati = "0"
            End If
            Dim importo_a_carico_del_broker As Double = Rs("importo_a_carico_del_broker")
            Dim imponibile_a_carico_del_broker As Double
            Dim iva_a_carico_del_broker As Double
            Dim aliquota_iva As Double

            Dim intestazione As String = Rs("conducente")
            Dim conducente As String = Rs("conducente")
            Dim indirizzo As String = Rs("indirizzo")
            Dim citta As String = Rs("city")
            Dim cap As String = Rs("cap")
            Dim provincia As String = Rs("provincia") & ""
            Dim nazione As String = Rs("nazione") & ""
            Dim piva As String = ""
            Dim codice_fiscale As String = Rs("codfis")
            Dim email As String = Rs("email") & ""
            Dim email_pec As String = ""
            Dim codice_sdi As String = ""

            Dim id_broker As String = Rs("id_broker") & ""
            Dim codice_edp As String = Rs("codice_edp")
            Dim num_contratto As String = Rs("num_contratto")
            Dim num_prenotazione As String = Rs("num_prenotazione") & ""
            Dim num_voucher As String = Rs("rif_to") & ""
            If num_voucher = "" Then
                num_voucher = num_prenotazione
            End If
            Dim id_primo_conducente As String = Rs("id_primo_conducente")
            Dim id_cliente As String = Rs("id_cliente")

            'NEL CASO DI CRV SU FATTURA DEVE ESSERE SPECIFICATO IL PRIMO VEICOLO
            If Rs("num_crv") = "0" Then
                targa = Rs("targa")
                modello = Rs("modello")
                km_uscita = Rs("km_uscita")
                km_rientro = Rs("km_rientro")
            Else
                targa = ""
                modello = ""
                km_uscita = ""
                km_rientro = ""
            End If

            'NUM_RF/DATA FATTURA: IN QUESTO MOMENTO NON SONO CORRETTI - UTILIZZARE I VALORI UNA VOLTA CAPITO DA OVE VENGONO GENERATI

            sqlStr = "INSERT INTO fatture_nolo (attiva, id_tipo_fattura, fattura_cliente, fattura_broker, fattura_costi_prepagati, num_fattura, data_fattura, "
            sqlStr += "num_rf, data_rf, id_ditta, cod_edp,"
            sqlStr += "num_contratto_rif,num_prenotazione_rif, num_voucher, stazione_uscita, data_uscita, stazione_rientro, data_rientro, conducente,  "

            sqlStr2 = " VALUES ( "
            sqlStr2 += "'0','2','1','0','0','0',convert(datetime,'" & funzioni_comuni.getDataDb_con_orario(dataFattura) & "',102),"
            sqlStr2 += "'0',convert(datetime,'" & funzioni_comuni.getDataDb_con_orario(dataFattura) & "',102),"
            sqlStr2 += "'" & Rs("id_cliente") & "','" & Rs("codice_edp") & "','" & num_contratto & "','" & num_prenotazione & "" & "',"
            sqlStr2 += "'" & Replace(num_voucher, "'", "''") & "','" & Replace(Rs("staz_uscita"), "'", "''") & "',convert(datetime,'" & funzioni_comuni.getDataDb_con_orario(Rs("data_uscita")) & "',102),"
            sqlStr2 += "'" & Replace(Rs("staz_rientro"), "'", "''") & "',convert(datetime,'" & funzioni_comuni.getDataDb_con_orario(Rs("data_rientro")) & "',102),'" & Replace(conducente, "'", "''") & "',"

            If importo_a_carico_del_broker <> 0 Then
                'NEL CASO DI PRENOTAZIONE BROKER ALLA FINE DOVRO' SALVARE LA FATTURA DEL BROKER - INIZIO A COSTRUIRE I DATI DA SALVARE VISTO
                'CHE MOLTI SONO IN COMUNE TRA LE DUE FATTURE
                sqlStr_broker = " VALUES ( "
                sqlStr_broker += "'0','2','0','1','0','0',convert(datetime,'" & funzioni_comuni.getDataDb_con_orario(dataFattura) & "',102),"
                sqlStr_broker += "'0',convert(datetime,'" & funzioni_comuni.getDataDb_con_orario(dataFattura) & "',102),"
                sqlStr_broker += "'" & Rs("id_broker") & "','" & Rs("codice_edp_broker") & "','" & num_contratto & "','" & num_prenotazione & "" & "',"
                sqlStr_broker += "'" & Replace(num_voucher, "'", "''") & "','" & Replace(Rs("staz_uscita"), "'", "''") & "',convert(datetime,'" & funzioni_comuni.getDataDb_con_orario(Rs("data_uscita")) & "',102),"
                sqlStr_broker += "'" & Replace(Rs("staz_rientro"), "'", "''") & "',convert(datetime,'" & funzioni_comuni.getDataDb_con_orario(Rs("data_rientro")) & "',102),'" & Replace(conducente, "'", "''") & "',"
            End If

            Dbc.Close()
            Dbc.Open()

            'DATI VETTURA: NEL CASO DI CRV LI TROVO IN contratti_crv_veicoli
            sqlStr = sqlStr & "targa,modello,km_uscita,km_rientro, "
            If targa <> "" Then
                sqlStr2 += "'" & Replace(targa, "'", "''") & "','" & Replace(modello, "'", "''") & "','" & km_uscita & "','" & km_rientro & "',"
                If importo_a_carico_del_broker <> 0 Then
                    sqlStr_broker += "'" & Replace(targa, "'", "''") & "','" & Replace(modello, "'", "''") & "','" & km_uscita & "','" & km_rientro & "',"
                End If
            Else
                Dim sql As String = "SELECT km_uscita, km_rientro, veicoli.targa, modelli.descrizione As modello "
                sql += "FROM contratti_crv_veicoli WITH(NOLOCK) "
                sql += "INNER JOIN veicoli WITH(NOLOCK) ON contratti_crv_veicoli.id_veicolo=veicoli.id "
                sql += "INNER JOIN modelli ON veicoli.id_modello=modelli.id_modello WHERE num_contratto='" & num_contratto & "' AND num_crv='0'"

                sqla = sql
                Cmd = New Data.SqlClient.SqlCommand(sql, Dbc)
                Rs = Cmd.ExecuteReader()

                Rs.Read()

                targa = Rs("targa")
                modello = Rs("modello")
                km_uscita = Rs("km_uscita")
                km_rientro = Rs("km_rientro")

                sqlStr2 = sqlStr2 & "'" & Replace(targa, "'", "''") & "','" & Replace(modello, "'", "''") & "','" & km_uscita & "','" & km_rientro & "',"

                If importo_a_carico_del_broker <> 0 Then
                    sqlStr_broker += "'" & Replace(targa, "'", "''") & "','" & Replace(modello, "'", "''") & "','" & km_uscita & "','" & km_rientro & "',"
                End If

                Dbc.Close()
                Dbc.Open()
            End If

            'INTESTAZIONE FATTURA: NEL CASO DI CLIENTE CASH PRENDO I DATI DA QUELLI DEL PRIMO GUIDATORE ALTRIMENTI DA QUELLI DELLA DITTA
            sqlStr += "intestazione, indirizzo, citta, cap, provincia, nazione, piva, codice_fiscale, mail, email_pec, codice_sdi,"
            If codice_edp = Costanti.codice_cash Then
                sqlStr2 += "'" & Replace(intestazione, "'", "''") & "','" & Replace(indirizzo, "'", "''") & "',"
                sqlStr2 += "'" & Replace(citta, "'", "''") & "','" & Replace(cap, "'", "''") & "','" & Replace(provincia, "'", "''") & "',"
                sqlStr2 += "'" & Replace(nazione, "'", "''") & "','','" & Replace(codice_fiscale, "'", "''") & "','" & Replace(email, "'", "''") & "','','',"
            Else

                Dim sql As String = "SELECT rag_soc, indirizzo, citta, cap, provincia, nazioni.nazione, piva, email, c_fis, email_pec, codice_sdi "
                sql += "FROM ditte WITH(NOLOCK) LEFT JOIN nazioni WITH(NOLOCK) ON ditte.nazione=nazioni.id_nazione "
                sql += "WHERE ditte.id_ditta='" & id_cliente & "'"
                sqla = sql
                Cmd = New Data.SqlClient.SqlCommand(sql, Dbc)
                Rs = Cmd.ExecuteReader()

                Rs.Read()

                intestazione = Rs("rag_soc") & ""
                indirizzo = Rs("indirizzo") & ""
                citta = Rs("citta") & ""
                cap = Rs("cap") & ""
                provincia = Rs("provincia") & ""
                nazione = Rs("nazione") & ""
                piva = Rs("piva") & ""
                email = Rs("email") & ""
                codice_fiscale = Rs("c_fis") & ""
                email_pec = Rs("email_pec") & ""
                codice_sdi = Rs("codice_sdi") & ""

                sqlStr2 += "'" & Replace(intestazione, "'", "''") & "','" & Replace(indirizzo, "'", "''") & "',"
                sqlStr2 += "'" & Replace(citta, "'", "''") & "','" & Replace(cap, "'", "''") & "','" & Replace(provincia, "'", "''") & "',"
                sqlStr2 += "'" & Replace(nazione, "'", "''") & "','" & Replace(piva, "'", "''") & "','" & Replace(codice_fiscale, "'", "''") & "','" & Replace(email, "'", "''") & "',"
                sqlStr2 += "'" & Replace(email_pec, "'", "''") & "','" & Replace(codice_sdi, "'", "''") & "',"

                Dbc.Close()
                Dbc.Open()
            End If

            If importo_a_carico_del_broker <> 0 Then
                'IN QUESTO CASO SELEZIONO IL BROKER A CUI FATTURARE - QUESTO DEVE ESSERE SPECIFICATO PER FORZA (BLOCCANTE PER POTER GENERARE FATTURA)

                Dim sql As String = "SELECT rag_soc, indirizzo, citta, cap, provincia, nazioni.nazione, piva, email, email_pec, codice_sdi "
                sql += "FROM ditte WITH(NOLOCK) LEFT JOIN nazioni WITH(NOLOCK) ON ditte.nazione=nazioni.id_nazione "
                sql += "WHERE ditte.id_ditta='" & id_broker & "'"
                sqla = sql
                Cmd = New Data.SqlClient.SqlCommand(sql, Dbc)
                Rs = Cmd.ExecuteReader()

                Rs.Read()

                intestazione = Rs("rag_soc") & ""
                indirizzo = Rs("indirizzo") & ""
                citta = Rs("citta") & ""
                cap = Rs("cap") & ""
                provincia = Rs("provincia") & ""
                nazione = Rs("nazione") & ""
                piva = Rs("piva") & ""
                email = Rs("email") & ""
                email_pec = Rs("email_pec") & ""
                codice_sdi = Rs("codice_sdi") & ""

                Dbc.Close()
                Dbc.Open()

                sqlStr_broker += "'" & Replace(intestazione, "'", "''") & "','" & Replace(indirizzo, "'", "''") & "',"
                sqlStr_broker += "'" & Replace(citta, "'", "''") & "','" & Replace(cap, "'", "''") & "','" & Replace(provincia, "'", "''") & "',"
                sqlStr_broker += "'" & Replace(nazione, "'", "''") & "','" & Replace(piva, "'", "''") & "','','" & Replace(email, "'", "''") & "',"
                sqlStr_broker += "'" & Replace(email_pec, "'", "''") & "','" & Replace(codice_sdi, "'", "''") & "',"

            End If

            'TOTALE NELL'INTESTAZIONE DELLA FATTURA
            If num_prenotazione = "" Then
                num_prenotazione = "-1"
            End If

            Dim sql2 As String = " (SELECT ISNULL(SUM(per_importo),0) FROM pagamenti_extra WITH(NOLOCK) WHERE "
            sql2 += " (N_CONTRATTO_RIF='" & num_contratto & "' OR N_PREN_RIF='" & num_prenotazione & "')  "
            sql2 += " AND (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Chiusura & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Vendita & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Pagamento_Contanti & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Attivo & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Passivo & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.MCR_Pagamento_Cash & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Deposito_su_RA & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso_su_RA & "') "
            sql2 += "AND NOT operazione_stornata='1' AND pagamento_broker='0')  "

            sqla = sql2

            Cmd = New Data.SqlClient.SqlCommand(sql2, Dbc)
            Dim totale_incassato As Double = Cmd.ExecuteScalar


            sql2 = " (SELECT ISNULL(SUM(per_importo),0) FROM pagamenti_extra WITH(NOLOCK) WHERE "
            sql2 += " (N_CONTRATTO_RIF='" & num_contratto & "' OR N_PREN_RIF='" & num_prenotazione & "')  "
            sql2 += " AND (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Chiusura & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Vendita & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Pagamento_Contanti & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Attivo & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Passivo & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.MCR_Pagamento_Cash & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Deposito_su_RA & "' "
            sql2 += "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso_su_RA & "') "
            sql2 += "AND NOT operazione_stornata='1' AND pagamento_broker='1')  "
            sqla = sql2
            Cmd = New Data.SqlClient.SqlCommand(sql2, Dbc)
            Dim totale_incassato_broker As Double = Cmd.ExecuteScalar

            'PER IL CONTRATTO PRINCIPALE IL SINGOLO COSTO DEVE ESSERE SOTTRATTO ALL'IMPORTO PREPAGATO - INFATTI NELLA FATTURA PER IL CLIENTE, NEL CASO DI CONTRATTO DA PRENOTAZIONE
            'PREPAGATA, VENGONO RIPORTATI SOLAMENTE I COSTI EXTRA RISPETTO A QUANTO PAGATO IN FASE DI PREPAGAMENTO. INOLTRE SE NON CI SONO EXTRA VERRA' EMESSA UNICAMENTE LA FATTURA
            'PER GLI IMPORTI PREPAGATI
            sql2 = " SELECT (imponibile_scontato+ISNULL(imponibile_onere,0)) As imponibile,"
            sql2 += "(iva_imponibile_scontato+ISNULL(iva_onere,0)) As iva FROM contratti_costi WITH(NOLOCK) "
            sql2 += "WHERE id_documento=" & id_contratto & " AND num_calcolo='" & num_calcolo & "' "
            sql2 += "AND nome_costo='" & LCase(Costanti.testo_elemento_totale) & "'"
            sqla = sql2
            Cmd = New Data.SqlClient.SqlCommand(sql2, Dbc)
            Rs = Cmd.ExecuteReader()

            Rs.Read()

            Dim imponibile As Double = Rs("imponibile")
            Dim iva As Double = Rs("iva")

            Dbc.Close()
            Dbc.Open()

            If importo_a_carico_del_broker <> 0 Then
                'NEL CASO DI TARIFFA BROKER DEVO DECURTARE I TOTALI DEL COSTO A CARICO DEL TO
                If giorni >= giorni_to Then
                    'SE I GIORNI DI NOLEGGIO SONO MAGGIORI DEI GIORNI DI VOUCHER DEVO TOGLIERE DAL TOTALE IL TOTALE A CARICO DEL TO
                    sql2 = "SELECT aliquota_iva FROM contratti_costi WHERE id_documento=" & id_contratto & " AND num_calcolo='" & num_calcolo & "' "
                    sql2 += "AND id_elemento='" & LCase(Costanti.ID_tempo_km) & "'"
                    sqla = sql2
                    Cmd = New Data.SqlClient.SqlCommand(sql2, Dbc)

                    aliquota_iva = Cmd.ExecuteScalar

                    imponibile_a_carico_del_broker = importo_a_carico_del_broker / (1 + aliquota_iva / 100)
                    iva_a_carico_del_broker = imponibile_a_carico_del_broker * aliquota_iva / 100
                Else
                    'SE I GIORNI DI NOLEGGIO SONO MINORI DEI GIORNI DI VOUCHER  (CASO DI RIENTRO ANTICIPATO CON GIORNI NON USUFRUITI NON 
                    'RIMBORSABILI) TOLGO DAL TOTALE IL COSTO DEL VALORE TARIFFA (CHE IN QUESTO CASO NON COINCIDE COL TOTALE A CARICO DEL BROKER
                    'SALVATO IN contratti MA RISULTA INFERIORE)
                    sql2 = " SELECT imponibile_scontato+ISNULL(imponibile_onere,0) As imponibile,"
                    sql2 += "iva_imponibile_scontato+ISNULL(iva_onere,0) As iva, aliquota_iva FROM contratti_costi WITH(NOLOCK) "
                    sql2 += "WHERE id_documento=" & id_contratto & " AND num_calcolo='" & num_calcolo & "' "
                    sql2 += "AND id_elemento='" & LCase(Costanti.ID_tempo_km) & "'"
                    sqla = sql2
                    Cmd = New Data.SqlClient.SqlCommand(sql2, Dbc)
                    Rs = Cmd.ExecuteReader()
                    Rs.Read()

                    imponibile_a_carico_del_broker = Rs("imponibile")
                    iva_a_carico_del_broker = Rs("iva")
                    aliquota_iva = Rs("aliquota_iva")

                    Dbc.Close()
                    Dbc.Open()
                End If

                imponibile = imponibile - imponibile_a_carico_del_broker
                iva = iva - iva_a_carico_del_broker

                'DA QUESTO MOMENTO IN POI L'IMPONIBILE A CARICO DEL BROKER (PER SUCCESSIVI SALVATAGGI SU DB) TORNA AD ESSERE SEMPRE CALCOLATO
                'RISPETTO A QUANTO DEVE EFFETTIVAMENTE PAGARE IL BROKER (SALVATO IN contratti)
                imponibile_a_carico_del_broker = importo_a_carico_del_broker / (1 + aliquota_iva / 100)
                iva_a_carico_del_broker = imponibile_a_carico_del_broker * aliquota_iva / 100

                'codice da ultimo entermed 08.01.2021
                '    sqlStr_broker += "'" & Replace(imponibile_a_carico_del_broker, ",", ".") & "'," &
                '"'" & Replace(iva_a_carico_del_broker, ",", ".") & "'," &
                '"'" & Replace(imponibile_a_carico_del_broker + iva_a_carico_del_broker, ",", ".") & "'," &
                '"'" & Replace(totale_incassato_broker, ",", ".") & "'," &
                '"'" & Replace(imponibile_a_carico_del_broker + iva_a_carico_del_broker - totale_incassato_broker, ",", ".") & "')"

                'calcoli x fattura broker
                Dim ii0 As Double = FormatNumber(imponibile_a_carico_del_broker, 2)
                Dim ii1 As Double = FormatNumber(iva_a_carico_del_broker, 2)
                Dim ii2 As Double = imponibile_a_carico_del_broker + iva_a_carico_del_broker
                ii2 = FormatNumber(ii2, 2) 'totale fattura
                Dim ii3 As Double = FormatNumber(totale_incassato_broker, 2)
                Dim ii4 As Double = imponibile_a_carico_del_broker + iva_a_carico_del_broker - totale_incassato_broker

                sqlStr_broker += "'" & Replace(ii0, ",", ".") & "',"
                sqlStr_broker += "'" & Replace(ii1, ",", ".") & "',"
                sqlStr_broker += "'" & Replace(ii2, ",", ".") & "',"
                sqlStr_broker += "'" & Replace(ii3, ",", ".") & "',"
                sqlStr_broker += "'" & Replace(ii4, ",", ".") & "')"



                'HttpContext.Current.Response.End() 'solo x test


            End If

            'rem x ultimo entermed 08.01.2021
            'sqlStr += "imponibile, iva, totale_fattura, totale_pagamenti, saldo, "
            'sqlStr2 += "'" & Replace(FormatNumber(imponibile, 2, , , TriState.False), ",", ".") & "',"
            'sqlStr2 += "'" & Replace(FormatNumber(iva, 2, , , TriState.False), ",", ".") & "',"
            'sqlStr2 += "'" & Replace(FormatNumber(imponibile + iva, 2, , , TriState.False), ",", ".") & "',"
            'sqlStr2 += "'" & Replace(FormatNumber(totale_incassato, 2, , , TriState.False), ",", ".") & "',"
            'sqlStr2 += "'" & Replace(FormatNumber(imponibile + iva - totale_incassato, 2, , , TriState.False), ",", ".") & "',"

            'codice da ultimo entermed 08.01.2021
            sqlStr = sqlStr & "imponibile, iva, totale_fattura, totale_pagamenti, saldo, "
            sqlStr2 = sqlStr2 & "'" & Replace(imponibile, ",", ".") & "'," &
        "'" & Replace(iva, ",", ".") & "'," &
        "'" & Replace(imponibile + iva, ",", ".") & "'," &
        "'" & Replace(totale_incassato, ",", ".") & "'," &
        "'" & Replace(imponibile + iva - totale_incassato, ",", ".") & "',"

            sqlStr += "data_fattura_prepagato, numero_fattura_prepagato)"
            sqlStr2 += "NULL,NULL)"


            Dim id_fattura As String
            'LA FATTURA CLIENTE NON VA CREATA SE L'IMPONIBILE E' ZERO E ANCHE IL TOTALE INCASSATO E' A ZERO (DOVREBBE ESSERE ZERO SOLO NEL CASO DI PREPAGATE/BROKER CON NESSUN ADDEBITO AL BANCO)
            'VA PERO' CREATA SE C'E' ALMENO UN PAGAMENTO RD (LA RIGA RDS VIENE AGGIUNTA IN FASE DI STAMPA) O SE E' COMPLIMENTARY
            'HttpContext.Current.Trace.Write("IMPONIBILE --- > ", FormatNumber(imponibile, 2, , , TriState.False))

            If FormatNumber(imponibile, 2, , , TriState.False) <> "0,00" Or FormatNumber(totale_incassato, 2, , , TriState.False) <> "0,00" Or EsisteImponibileRDS(num_contratto) OrElse Pagamenti.is_complimentary("", num_contratto) Then
                'HttpContext.Current.Trace.Write("IMPONIBILE --- > ", imponibile)
                sqla = sqlStr & " " & sqlStr2
                Cmd = New Data.SqlClient.SqlCommand(sqlStr & " " & sqlStr2, Dbc)
                Cmd.ExecuteNonQuery()

                'RECUPERO L'ID DELLA FATTURA APPENA CREATA 
                sqlStr = "SELECT @@IDENTITY FROM fatture_nolo"
                sqla = sqlStr
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                id_fattura = Cmd.ExecuteScalar
            Else
                id_fattura = "0"
            End If

            Dim aliquota_iva_tempo_km As String
            Dim codice_iva_tempo_km As String
            Dim iva_elemento As String

            Dim insert_pagamenti_broker(50) As String

            insert_pagamenti_broker(0) = "0"

            Dim k As Integer = 0
            Dim i As Integer = 0

            'RIGHE DELLA FATTURA --------------------------------------------------------------------------------------------------------------
            Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc2.Open()

            sqlStr = "SELECT id_elemento, nome_costo, ISNULL(imponibile_scontato,0) As imponibile_scontato, ISNULL(iva_imponibile_scontato,0) As iva_imponibile_scontato, ISNULL(imponibile_scontato_prepagato,0) As imponibile_scontato_prepagato,"
            sqlStr += "ISNULL(iva_imponibile_scontato_prepagato,0) As iva_imponibile_scontato_prepagato, qta, aliquota_iva, ISNULL(codice_iva,'') As codice_iva, ISNULL(prepagato,'0') As prepagato FROM contratti_costi WITH(NOLOCK) "
            sqlStr += "WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND selezionato='1' "
            sqlStr += "AND (id_a_carico_di<>'" & Costanti.id_accessorio_incluso & "' OR id_elemento='" & Costanti.ID_tempo_km & "') AND ISNULL(omaggiato,'0')='0' "
            sqlStr += "ORDER BY ordine_stampa ASC"
            sqla = sqlStr
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Rs = Cmd.ExecuteReader()

            Do While Rs.Read()
                imponibile = Rs("imponibile_scontato") - Rs("imponibile_scontato_prepagato")
                iva_elemento = Rs("iva_imponibile_scontato") - Rs("iva_imponibile_scontato_prepagato")

                If Rs("id_elemento") = Costanti.ID_tempo_km And importo_a_carico_del_broker <> "0" Then
                    'TEMPO KM DELLA TARIFFA BROKER
                    If giorni >= giorni_to Then
                        imponibile = imponibile - imponibile_a_carico_del_broker
                        iva_elemento = iva_elemento - iva_a_carico_del_broker
                    Else
                        imponibile = 0
                        iva_elemento = 0
                    End If
                    'NE APPROFITTO PER SALVARE L'ALIQUOTA IVA DEL TEMPO KM DA UTILIZZARE EVENTUALMENTE PER SCORPORARE SUCCESSIVAMENTE
                    'L'IMPORTO PREPAGATO
                    aliquota_iva_tempo_km = Rs("aliquota_iva")
                    codice_iva_tempo_km = Rs("codice_iva") & ""
                ElseIf Rs("id_elemento") = Costanti.ID_tempo_km Then
                    'NE APPROFITTO PER SALVARE L'ALIQUOTA IVA DEL TEMPO KM DA UTILIZZARE EVENTUALMENTE PER SCORPORARE SUCCESSIVAMENTE
                    'L'IMPORTO PREPAGATO
                    aliquota_iva_tempo_km = Rs("aliquota_iva")
                    codice_iva_tempo_km = Rs("codice_iva") & ""
                End If
                Dim qta As Integer
                If imponibile <> 0 Then
                    'HttpContext.Current.Trace.Write(Rs("nome_costo") & " " & iva_elemento & " " & imponibile)

                    'FATTURA CLIENTE
                    qta = Rs("qta")

                    'NEL CASO DI FATTURA CLIENTE A ZERO LA FATTURA NON E' STATA CREATA E QUINDI NON EFFETTUO L'INSERT
                    If id_fattura <> "0" Then
                        sqlStr2 = "INSERT INTO fatture_nolo_righe (id_fattura, descrizione, quantita, costo_unitario, iva, totale, aliquota_iva, codice_iva) VALUES ("
                        sqlStr2 += "'" & id_fattura & "','" & Replace(Rs("nome_costo"), "'", "''") & "','" & qta & "',"
                        sqlStr2 += "'" & Replace(FormatNumber(imponibile / qta, 2, , , TriState.False), ",", ".") & "','" & Replace(FormatNumber(iva_elemento, 2, , , TriState.False), ",", ".") & "',"
                        sqlStr2 += "'" & Replace(FormatNumber(imponibile, 2, , , TriState.False), ",", ".") & "','" & Replace(Rs("aliquota_iva"), ",", ".") & "','" & Rs("codice_iva") & "" & "')"
                        sqla = sqlStr2
                        Cmd = New Data.SqlClient.SqlCommand(sqlStr2, Dbc2)
                        Cmd.ExecuteNonQuery()
                    End If
                End If
            Loop
            '----------------------------------------------------------------------------------------------------------------------------------
            'RIGHE DI PAGAMENTO ---------------------------------------------------------------------------------------------------------------
            Dbc.Close()
            Dbc.Open()

            'SE IL CONTRATTO PROVIENE DA UNA PRENOTAZIONE SELEZIONO ANCHE LE EVENTUALI VENDITE SU PRENOTAZIONE 
            'CHE NELLA FATTURA PER IL CLIENTE DEVE ESSERE AGGIUNTO TRA LE RIGHE DEI SERVIZI
            Dim importo_prepagato As Double = 0
            Dim importo_pagato_broker As Double = 0
            Dim sql_prepagato As String = ""

            If num_prenotazione <> "" Then
                sql_prepagato = "(N_CONTRATTO_RIF='" & num_contratto & "' OR N_PREN_RIF='" & num_prenotazione & "' OR pagamenti_extra.N_RDS_RIF IN (SELECT id_rds FROM veicoli_evento_apertura_danno WITH(NOLOCK) WHERE id_documento_apertura = '" & num_contratto & "')) "
            Else
                sql_prepagato = "N_CONTRATTO_RIF='" & num_contratto & "' OR pagamenti_extra.N_RDS_RIF IN (SELECT id_rds FROM veicoli_evento_apertura_danno WITH(NOLOCK) WHERE id_documento_apertura = '" & num_contratto & "'))"
            End If

            'ATTENZIONE: RIMBORSO SU RA SERVE PERCHE' CON QUESTA VOCE SI BILANCIA L'EVENTUALE CHIUSURA A 5 CENTESIMI DELLE PREAUTORIZZAZIONI
            'TENERNE CONTO SE SI DOVESSE DECIDERE DI NON RIPORTARE I DEPOSITI CAUZIONALI IN CONTANTI E I CORRISPETTIVI RIMBORSI SU FATTURA.

            sqlStr = "SELECT pagamenti_extra.data, mod_pag.descrizione As modalita_pagamento, POS_Funzioni.Funzione As tipo_pagamento, ID_TIPPAG, pagamento_broker,  "
            sqlStr += "pagamenti_extra.PER_IMPORTO, N_CONTRATTO_RIF,  N_RDS_RIF, N_PREN_RIF, codici_contabili.codice_contabile, pagamenti_extra.id_ctr FROM PAGAMENTI_EXTRA WITH(NOLOCK) "
            sqlStr += "LEFT JOIN MOD_PAG With(NOLOCK) On pagamenti_extra.ID_ModPag=MOD_PAG.ID_ModPag "
            sqlStr += "LEFT JOIN POS_Funzioni With(NOLOCK) On PAGAMENTI_EXTRA.id_pos_funzioni_ares=POS_Funzioni.id "
            sqlStr += "LEFT JOIN codici_contabili With(NOLOCK) On pagamenti_extra.ID_ModPag=codici_contabili.modpag And pagamenti_extra.ID_TipPag=codici_contabili.tippag "
            sqlStr += "WHERE " & sql_prepagato
            sqlStr += "And Not operazione_stornata ='1' ORDER BY pagamenti_extra.data ASC"
            sqla = sqlStr
            'response.write(sqla)
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Rs = Cmd.ExecuteReader()


            Do While Rs.Read()
                Dim modalita_pagamento As String = Rs("modalita_pagamento") & ""
                If (modalita_pagamento = "" And (Rs("ID_TIPPAG") & "") = "1011098650") Or (Rs("tipo_pagamento") & "").ToString.ToLower = "preautorizzazione" Or (Rs("tipo_pagamento") & "").ToString.ToLower = "chiusura preautorizzazione" Then
                    modalita_pagamento = "C.CREDITO"
                End If


                Dim tipo_pagamento As String = Rs("tipo_pagamento") & ""
                If tipo_pagamento.ToLower = "chiusura preautorizzazione" Then
                    tipo_pagamento = "Chiusura Preautorizzazione"
                End If

                If ((Rs("n_contratto_rif") & "") <> "" Or (Rs("N_RDS_RIF") & "") <> "" Or (Rs("N_PREN_RIF") & "") <> "") And Not Rs("pagamento_broker") Then
                    'SOLO NEL CASO IN CUI LA FATTURA E' STATA CREATA, QUINDI CON IMPONIBILE DIVERSO DA ZERO
                    If id_fattura <> "0" Then
                        'IN QUESTO CASO SI TRATTA DI UN PAGAMENTO SU CONTRATTO O DI UN PAGAMENTO SU PRENOTAZIONE IN CASO DI BROKER - REGISTRO L'OPERAZIONE IN fatture_nolo_pagamenti
                        sqlStr2 = "INSERT INTO fatture_nolo_pagamenti (id_fattura_nolo, data_pagamento, modalita_pagamento, tipo_pagamento, codice_contabile, importo, id_ctr_pagamenti_extra) "
                        sqlStr2 += "VALUES ('" & id_fattura & "',convert(datetime,'" & funzioni_comuni.getDataDb_senza_orario(Rs("data")) & "',102),"
                        sqlStr2 += "'" & Replace(modalita_pagamento, "'", "''") & "','" & Replace(tipo_pagamento & "", "'", "''") & "',"
                        sqlStr2 += "'" & Rs("codice_contabile") & "" & "',"
                        sqlStr2 += "'" & Replace(Rs("per_importo"), ",", ".") & "','" & Rs("id_ctr") & "')"
                        sqla = sqlStr2
                        Cmd = New Data.SqlClient.SqlCommand(sqlStr2, Dbc2)
                        Cmd.ExecuteNonQuery()
                    End If
                ElseIf Rs("pagamento_broker") Then
                    'IN QUESTO CASO SI TRATTA DI PAGAMENTO BROKER  PER CUI REGISTRO L'IMPORTO PER POTER INSERIRE LA RIGA
                    importo_pagato_broker = importo_pagato_broker + Rs("per_importo")

                    insert_pagamenti_broker(k) = "INSERT INTO fatture_nolo_pagamenti (id_fattura_nolo, data_pagamento, modalita_pagamento, tipo_pagamento, codice_contabile, importo, id_ctr_pagamenti_extra) "
                    insert_pagamenti_broker(k) += "VALUES ('ID_FATT_BROKER1234',convert(datetime,'" & funzioni_comuni.getDataDb_senza_orario(Rs("data")) & "',102),"
                    insert_pagamenti_broker(k) += "'" & Replace(modalita_pagamento, "'", "''") & "','" & Replace(Rs("tipo_pagamento") & "", "'", "''") & "',"
                    insert_pagamenti_broker(k) += "'" & Rs("codice_contabile") & "" & "',"
                    insert_pagamenti_broker(k) += "'" & Replace(Rs("per_importo"), ",", ".") & "','" & Rs("id_ctr") & "')"

                    k = k + 1

                    insert_pagamenti_broker(k) = "0"
                End If
            Loop

            Dbc.Close()
            Dbc.Open()
            '----------------------------------------------------------------------------------------------------------------------------------
            Dim id_fattura_broker As String = ""

            If importo_a_carico_del_broker <> 0 Then
                'FATTURA BROKER ----------------------------------------------------------------------------------------------------------------
                'L'INTESTAZIONE DELLA FATTURA E' GIA' STATA CREATA - PROCEDO AL SALVATAGGIO
                sqlStr = "INSERT INTO fatture_nolo (attiva, id_tipo_fattura,fattura_cliente, fattura_broker, fattura_costi_prepagati, num_fattura, data_fattura, num_rf, data_rf, id_ditta, cod_edp, num_contratto_rif, "
                sqlStr += "num_prenotazione_rif, num_voucher, stazione_uscita, data_uscita, stazione_rientro, data_rientro, conducente,  "
                sqlStr += "targa,modello,km_uscita,km_rientro,intestazione, indirizzo, citta, cap, provincia, nazione, piva, codice_fiscale, mail,email_pec, codice_sdi,"
                sqlStr += "imponibile, iva, totale_fattura, totale_pagamenti, saldo) " & sqlStr_broker

                'HttpContext.Current.Trace.Write(sqlStr)
                sqla = sqlStr
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                Cmd.ExecuteNonQuery()


                sqlStr = "SELECT @@IDENTITY FROM fatture_nolo WITH(NOLOCK)"
                sqla = sqlStr
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                id_fattura_broker = Cmd.ExecuteScalar

                'RIGA FATTURA - A CARICO DEL BROKER C'E' UNICAMENTE IL TEMPO KM 
                sqlStr = "INSERT INTO fatture_nolo_righe (id_fattura, descrizione, quantita, costo_unitario, iva, totale, aliquota_iva, codice_iva) VALUES ("
                sqlStr += "'" & id_fattura_broker & "','Valore Tariffa','" & giorni_to & "',"
                sqlStr += "'" & Replace(FormatNumber(imponibile_a_carico_del_broker / giorni_to, 2, , , TriState.False), ",", ".") & "',"
                sqlStr += "'" & Replace(FormatNumber(iva_a_carico_del_broker, 2, , , TriState.False), ",", ".") & "',"
                sqlStr += "'" & Replace(FormatNumber(imponibile_a_carico_del_broker, 2, , , TriState.False), ",", ".") & "','" & aliquota_iva_tempo_km & "','" & codice_iva_tempo_km & "')"
                sqla = sqlStr
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteNonQuery()

                'REGISTRO I PAGAMENTI BROKER
                k = 0

                Do While insert_pagamenti_broker(k) <> "0"
                    'SALVO I PAGAMENTI BROKER CHE HO PRECEDENTEMENTE MEMORIZZATO
                    Cmd = New Data.SqlClient.SqlCommand(Replace(insert_pagamenti_broker(k), "ID_FATT_BROKER1234", id_fattura_broker), Dbc2)
                    Cmd.ExecuteNonQuery()

                    k = k + 1
                Loop
            End If

            'SE TUTTO E' ANDATO A BUON FINE RENDO ATTIVE LE DUE FATTURE ASSEGNANDONE IL NUMERO E CAMBIO LO STATO DEL CONTRATTO A "FATTURATO"
            Dim num_fattura_cliente As String
            Dim num_fattura_broker As String

            'SE DALL'ESTERNO E' STATO SPECIFICATO UN NUMERO DI FATTURA (SOLO PER ASSEGNAZIONE SINGOLO CONTRATTO) ALLORA VIENE UTILIZZATO QUEL VALORE. ATTENZIONE: PRIMA DI CHIAMARE
            'QUESTO METODO CI SI DEVE ACCERTARE CHE I NUMERI FATTURA (CLIENTE ED EVENTUALMENTE BROKER) SONO DISPONIBILI.
            If num_fattura = "" Then
                If id_fattura <> "0" Then
                    'SE LA FATTURA CLIENTE E' STATA CREATA
                    num_fattura_cliente = Contatori.getContatore_fatture_nolo(anno_fattura)
                End If
            Else
                num_fattura_cliente = num_fattura
            End If

            If id_fattura <> "0" Then
                sqla = "UPDATE fatture_nolo SET attiva='1', da_esportare_contabilita='1', num_fattura='" & num_fattura_cliente & "' WHERE id='" & id_fattura & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                Cmd.ExecuteNonQuery()
            End If

            If importo_a_carico_del_broker <> 0 Then
                If num_fattura = "" Then
                    num_fattura_broker = Contatori.getContatore_fatture_nolo(anno_fattura)
                Else
                    If id_fattura <> "0" Then
                        num_fattura_broker = CInt(num_fattura) + 1
                    Else
                        num_fattura_broker = CInt(num_fattura)
                    End If
                End If
                sqla = "UPDATE fatture_nolo SET attiva='1', num_fattura='" & num_fattura_broker & "', da_esportare_contabilita='1' WHERE id='" & id_fattura_broker & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                Cmd.ExecuteNonQuery()

                Dim update_fattura_cliente As String = ""
                If id_fattura <> "0" Then
                    update_fattura_cliente = "id_fattura_cliente='" & id_fattura & "',"
                End If
                sqla = "UPDATE contratti SET status='6', fatturazione_da_controllare='0', non_fatturare='0', " & update_fattura_cliente & " id_fattura_broker='" & id_fattura_broker & "' WHERE id='" & id_contratto & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                Cmd.ExecuteNonQuery()
            Else

                'TECNICAMENTE NON DOVREBBE MAI ACCADERE CHE A QUESTO PUNTO L'IMPONIBILE SIA A ZERO E LA FATTURA NON SIA STATA CREATA
                If id_fattura <> "0" Then
                    sqla = "UPDATE contratti SET status='6', fatturazione_da_controllare='0', non_fatturare='0', id_fattura_cliente='" & id_fattura & "' WHERE id='" & id_contratto & "'"
                    Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                    Cmd.ExecuteNonQuery()
                End If
            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
            Dbc2.Close()
            Dbc2.Dispose()
            Dbc2 = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error genera_fattura_ra  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try


    End Sub

    Public Shared Function genera_flusso_fattura_ottico(ByVal anno As String, ByVal num_fattura_da As String, ByVal num_fattura_a As String) As String
        'RESTITUISCE IL PERCORSO E IL NOME DEL FILE GENERATO
        Dim file As String = ConfigurationManager.AppSettings.Get("PathFattureOttico") & "FATT_" & anno & "_" & num_fattura_da & "_" & num_fattura_a & ".TXT"
        genera_flusso_fattura_ottico = file
        Using fs1 As FileStream = New FileStream(file, FileMode.Create, FileAccess.Write)
            Using s1 As StreamWriter = New StreamWriter(fs1)
                'http://msdn.microsoft.com/it-it/library/txafckwd(v=vs.80).aspx?cs-save-lang=1&cs-lang=vb#code-snippet-5
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc2.Open()

                Dim sqlStr As String = "SELECT id, num_fattura, data_fattura, cod_edp, num_contratto_rif, intestazione, indirizzo, cap, citta, piva, codice_fiscale, "
                sqlStr += "num_rf, data_rf, modello, stazione_uscita, stazione_rientro, targa, data_uscita, data_rientro, conducente, km_uscita, km_rientro, "
                sqlStr += "totale_pagamenti, imponibile, cod_iva, iva, totale_fattura, saldo, num_prenotazione_rif, num_voucher, data_fattura_prepagato, numero_fattura_prepagato "
                sqlStr += "FROM fatture_nolo WITH(NOLOCK) WHERE YEAR(data_fattura)='" & anno & "' AND num_fattura BETWEEN " & CInt(num_fattura_da) & " AND " & CInt(num_fattura_a) & " AND fatture_nolo.attiva='1'"

                Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dim Rs As Data.SqlClient.SqlDataReader
                Dim Rs2 As Data.SqlClient.SqlDataReader
                Rs = Cmd.ExecuteReader()

                Do While Rs.Read()
                    Dim totale_pagamenti As String = Rs("totale_pagamenti")
                    Dim imponibile As String = Rs("imponibile")
                    Dim aliquota_iva As String = Rs("cod_iva") & ""
                    Dim iva As String = Rs("iva")
                    Dim totale_fattura As String = Rs("totale_fattura")
                    Dim saldo As String = Rs("saldo")
                    Dim codice_edp As String = Rs("cod_edp")
                    Dim num_ra As String = Rs("num_contratto_rif")
                    Dim num_fattura As String = Rs("num_fattura")
                    Dim num_prenotazione As String = Rs("num_prenotazione_rif")
                    Dim num_voucher As String = Rs("num_voucher")
                    Dim data_fattura_prepagato As String = Rs("data_fattura_prepagato") & ""
                    Dim numero_fattura_prepagato As String = Rs("numero_fattura_prepagato") & ""
                    Dim id_fattura As String = Rs("id")


                    s1.WriteLine("")
                    '93 SPAZI + NUM FATTURA SEGUITO DA TANTI SPAZI PER RAGGIUNGERE LUNGHEZZA 27 + DATA FATTURA (DD/MM/YYYY)
                    s1.WriteLine(String.Format("{0,93}", "") & String.Format("{0,-27}", Rs("num_fattura")) & Format(Rs("data_fattura"), "dd/MM/yyyy"))
                    s1.WriteLine("")
                    s1.WriteLine("")
                    s1.WriteLine("")
                    '94 SPAZI + CODICE EDP E SPAZI FINO A 28 + NUM_RA
                    s1.WriteLine(String.Format("{0,94}", "") & String.Format("{0,-28}", Rs("cod_edp")) & Rs("num_contratto_rif"))
                    s1.WriteLine("")
                    s1.WriteLine("")
                    s1.WriteLine("")
                    '72 spazi + INTESTAZIONE 
                    s1.WriteLine(String.Format("{0,72}", "") & Rs("intestazione"))
                    '72 spazi + INDIRIZZO
                    s1.WriteLine(String.Format("{0,72}", "") & Rs("indirizzo"))
                    s1.WriteLine("")
                    '72 spazi + CAP e spazi fino a 14 + Città
                    s1.WriteLine(String.Format("{0,72}", "") & String.Format("{0,-14}", Rs("cap")) & Rs("citta"))
                    s1.WriteLine("")
                    s1.WriteLine("")
                    '114 spazi + PIVA / 111 SPAZI + COICE FISCALE (C'E' VERAMENTE QUESTA DIFFERENZA?)
                    If Rs("piva") <> "" Then
                        s1.WriteLine(String.Format("{0,114}", "") & Rs("piva"))
                    Else
                        s1.WriteLine(String.Format("{0,111}", "") & Rs("codice_fiscale"))
                    End If
                    '27 SPAZI + NUMERO RF FINO A 27 + DATA RF (DD/MM/YY)
                    s1.WriteLine(String.Format("{0,11}", "") & String.Format("{0,-27}", Rs("num_rf")) & Format(Rs("data_rf"), "dd/MM/yy"))
                    s1.WriteLine("")
                    s1.WriteLine("")
                    s1.WriteLine("")
                    '2 SPAZI + MODELLO FINO A 52 + STAZIONE USCITA FINO A 50 + STAZIONE RIENTRO
                    s1.WriteLine(String.Format("{0,2}", "") & String.Format("{0,-52}", Rs("modello")) & String.Format("{0,-50}", Rs("stazione_uscita")) & Rs("stazione_rientro"))
                    s1.WriteLine("")
                    s1.WriteLine("")
                    '2 SPAZI + TARGA FINO A 52 + DATA/ORA USCITA FINO A 50 + DATA ORA RIENTRO
                    s1.WriteLine(String.Format("{0,2}", "") & String.Format("{0,-52}", Rs("targa")) & String.Format("{0,-50}", Rs("data_uscita")) & Rs("data_rientro"))
                    s1.WriteLine("")
                    '2 SPAZI + 1° GUIDATGORE FINO A 52 + KM USCITA FINO A 50 + KM RIENTRO 
                    s1.WriteLine(String.Format("{0,2}", "") & String.Format("{0,-52}", Rs("conducente")) & String.Format("{0,-50}", Rs("km_uscita")) & Rs("km_rientro"))
                    s1.WriteLine("")
                    s1.WriteLine("")
                    'INTESTAZIONE RIGHE FAATTURA
                    s1.WriteLine(String.Format("{0,1}", "") & String.Format("{0,-74}", "Servizi") & String.Format("{0,-19}", "Quantità") & String.Format("{0,-21}", "Costo Un.") & String.Format("{0,-14}", "Totale") & "AL.IVA")
                    s1.WriteLine("")

                    'RIGHE (FINO A 16)
                    Dbc2.Close()
                    Dbc2.Open()

                    Dim i As Integer = 0

                    sqlStr = "SELECT * FROM fatture_nolo_righe WITH(NOLOCK) WHERE id_fattura='" & id_fattura & "'"
                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)

                    Rs2 = Cmd.ExecuteReader()
                    Do While Rs2.Read()
                        s1.WriteLine(String.Format("{0,1}", "") & String.Format("{0,-74}", Rs2("descrizione")) & String.Format("{0,5}", Rs2("quantita")) & String.Format("{0,27}", Rs2("costo_unitario")) & String.Format("{0,23}", Rs2("totale")) & String.Format("{0,5}", Rs2("codice_iva")))

                        i = i + 1
                    Loop
                    For j = i To 15
                        s1.WriteLine("")
                    Next

                    'PAGAMENTI (8 RIGHE) - INTESTAZIONE SOLO SE C'E' UNA RIGA
                    i = 0

                    Dbc2.Close()
                    Dbc2.Open()

                    sqlStr = "SELECT * FROM fatture_nolo_pagamenti WITH(NOLOCK) WHERE id_fattura_nolo='" & id_fattura & "'"
                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)

                    Rs2 = Cmd.ExecuteReader()
                    Do While Rs2.Read()
                        If i = 0 Then
                            'SE C'E' UNA RIGA DI PAGAMENTO INSERISCO L'INTESTAZIONE DEI PAGAMENTI
                            s1.WriteLine(String.Format("{0,1}", "") & String.Format("{0,-25}", "PAGAMENTI") & String.Format("{0,-14}", "DATA") & String.Format("{0,-38}", "MODALITA'") & String.Format("{0,-51}", "TIPO") + "IMPORTO")
                            s1.WriteLine("")
                            s1.WriteLine("")
                        End If
                        s1.WriteLine(String.Format("{0,35}", Format(Rs2("data_pagamento"), "dd/MM/yyyy")) & String.Format("{0,5}", "") & String.Format("{0,-38}", Rs2("modalita_pagamento")) & String.Format("{0,-51}", Rs2("tipo_pagamento")) & String.Format("{0,8}", Rs2("importo")))

                        i = i + 1
                    Loop
                    If i = 0 Then
                        'LA RIGA CON L'INTESTAZIONE E' VUOTA
                        s1.WriteLine("")
                        s1.WriteLine("")
                        s1.WriteLine("")
                    End If
                    For j = i To 8
                        s1.WriteLine("")
                    Next

                    Rs2.Close()

                    '95 spazi + Totale Pagamenti : + MASCHERA DI 23 CAMPI CON TOTALE PAGAMENTI IN CODA
                    s1.WriteLine(String.Format("{0,95}", "") & "Totale Pagamenti :" & String.Format("{0,23}", totale_pagamenti))
                    s1.WriteLine("")
                    '5 SPAZI + IMPONIBILE + 12 SPAZI + AL.IVA + 11 SPAZI + I.V.A. + 27 + IMPONIBILE + MASCHERA DI 22 CAMPI CON IMPONIBILE IN CODA
                    s1.WriteLine(String.Format("{0,5}", "") & "IMPONIBILE" & String.Format("{0,12}", "") & "AL.IVA" & String.Format("{0,11}", "") & "I.V.A." & String.Format("{0,27}", "") & "IMPONIBILE :" & String.Format("{0,22}", imponibile))
                    'MASCHERA 13 CON IMPONIBILE + MASCHERA 19 CON ALIQUOTA IVA + MASCHERA 18 CON IVA
                    s1.WriteLine(String.Format("{0,13}", imponibile) & String.Format("{0,19}", aliquota_iva) & String.Format("{0,18}", iva))
                    '80 SPAZI + I.V.A. : + MASCHERA 23 CON IVA
                    s1.WriteLine(String.Format("{0,80}", "") & "I.V.A. :" & String.Format("{0,23}", iva))
                    s1.WriteLine("")
                    '72 SPAZI + TOTALE FATTURA : + MASCHERA 23 CON totale_fattura
                    s1.WriteLine(String.Format("{0,72}", "") & "TOTALE FATTURA :" & String.Format("{0,23}", totale_fattura))
                    s1.WriteLine("")
                    '71 SPAZI + A salo Fattura : + MASCHERA 23 CON saldo
                    s1.WriteLine(String.Format("{0,71}", "") & "A saldo Fattura :" & String.Format("{0,23}", saldo))

                    s1.WriteLine("")
                    s1.WriteLine("")
                    s1.WriteLine("")
                    s1.WriteLine("")
                    If numero_fattura_prepagato <> "" Then
                        s1.WriteLine("Riferimento Prepagato : Prenotazione N." & num_prenotazione & " con Fattura del " & data_fattura_prepagato & " e Numero " & numero_fattura_prepagato)
                    Else
                        s1.WriteLine("")
                    End If

                    s1.WriteLine("")
                    s1.WriteLine("")
                    s1.WriteLine("")

                    '23 SPAZI + CODICE EDP FINO A 42 + NUM RA FINO A 44 + SALDO
                    s1.WriteLine(String.Format("{0,23}", "") & String.Format("{0,-42}", codice_edp) & String.Format("{0,-44}", num_ra) & saldo)
                    '23 SPAZI + NUM FATTURA FINO A 42 + VOUCHER FINO A 44 + PRENOTAZIONE
                    s1.WriteLine(String.Format("{0,23}", "") & String.Format("{0,-42}", num_fattura) & String.Format("{0,-44}", num_voucher) & num_prenotazione)
                    s1.WriteLine("")
                    s1.WriteLine("")
                    '20 spazi + Euro 4,14 (Fisso?)
                    s1.WriteLine(String.Format("{0,20}", "") & "Euro 4,14")
                    s1.WriteLine("")
                    s1.WriteLine("")
                    s1.WriteLine("")
                    '6 righe di piè pagina - chiedere se la dicitura e' vissa
                    s1.WriteLine("MONTE DEI PASCHI....")
                    s1.WriteLine("UNICREDIT")
                    s1.WriteLine("UNICREDIT S.P.A.")
                    s1.WriteLine("INTESA SAN PAOLO")
                    s1.WriteLine("")
                    s1.WriteLine("")
                Loop

                Rs.Close()
                Rs = Nothing
                Rs2 = Nothing
                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
                Dbc2.Close()
                Dbc2.Dispose()
                Dbc2 = Nothing
            End Using
        End Using
    End Function

    Public Shared Function genera_flusso_clienti(ByVal anno As String, ByVal num_fattura_da As String, ByVal num_fattura_a As String) As String
        Dim file As String = ConfigurationManager.AppSettings.Get("PathFatture") & "CLIENTI.TXT"
        genera_flusso_clienti = file
        Using fs1 As FileStream = New FileStream(file, FileMode.Create, FileAccess.Write)
            Using s1 As StreamWriter = New StreamWriter(fs1)
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                'DA SPECIFICARE CORRETTAMENTE LA CATEGORIA  <<<<<<------------------------------------
                Dim sqlStr As String = "SELECT DISTINCT ditte.id_ditta, [codice edp] As cod_edp, ditte.rag_soc, ditte.indirizzo, ditte.citta, ditte.provincia, ditte.cap, "
                sqlStr += "ditte.c_fis, ditte.piva, aliquote_iva.codice_iva, ditte.tel, ditte.fax, termine_di_pagamento.codice_contabile, nazioni.nazione, "
                sqlStr += "ditte.piva_estera, '04' As categoria "
                sqlStr += "FROM fatture_nolo WITH(NOLOCK)  "
                sqlStr += "INNER JOIN ditte WITH(NOLOCK) ON fatture_nolo.id_ditta=ditte.id_ditta "
                sqlStr += "LEFT JOIN aliquote_iva WITH(NOLOCK) ON ditte.id_aliquota_iva=aliquote_iva.id "
                sqlStr += "LEFT JOIN termine_di_pagamento WITH(NOLOCK) ON ditte.id_pagamento=termine_di_pagamento.id "
                sqlStr += "LEFT JOIN nazioni WITH(NOLOCK) ON ditte.nazione=nazioni.id_nazione "
                sqlStr += "WHERE YEAR(fatture_nolo.data_fattura)='" & anno & "' AND fatture_nolo.num_fattura BETWEEN " & CInt(num_fattura_da) & " AND " & CInt(num_fattura_a)
                sqlStr += " AND fatture_nolo.attiva='1'"

                Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dim Rs As Data.SqlClient.SqlDataReader
                Rs = Cmd.ExecuteReader()

                s1.WriteLine("000000")

                Do While Rs.Read()
                    Dim stringa As String = String.Format("{0,-6}", Rs("id_ditta")) & String.Format("{0,-6}", Rs("cod_edp")) & String.Format("{0,-50}", Left(Rs("rag_soc"), 50))
                    stringa += String.Format("{0,-50}", Left(Rs("indirizzo") & "", 50)) & String.Format("{0,-50}", Left(Rs("citta") & "", 50))
                    stringa += String.Format("{0,-2}", Left(Rs("provincia") & "", 2)) & String.Format("{0,-10}", Left(Rs("cap") & "", 10))
                    stringa += String.Format("{0,-20}", Left(Rs("c_fis") & "", 20)) & String.Format("{0,-20}", Left(Rs("piva") & "", 20))
                    stringa += String.Format("{0,-3}", Left(Rs("codice_iva") & "", 3)) & String.Format("{0,-33}", Left(Rs("tel") & "", 33))
                    stringa += String.Format("{0,-30}", Left(Rs("fax") & "", 30)) & String.Format("{0,-6}", Left(Rs("codice_contabile") & "", 6))
                    stringa += String.Format("{0,-29}", Left(Rs("nazione") & "", 29)) & String.Format("{0,-19}", Left(Rs("piva_estera") & "", 19))
                    stringa += String.Format("{0,-6}", Left(Rs("categoria") & "", 6)) & "*"

                    s1.WriteLine(stringa)
                Loop

                Rs.Close()
                Rs = Nothing
                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            End Using
        End Using
    End Function

    Public Shared Function genera_flusso_pagamenti(ByVal anno As String, ByVal num_fattura_da As String, ByVal num_fattura_a As String) As String
        Dim file As String = ConfigurationManager.AppSettings.Get("PathFatture") & "TRAPAG.TXT"
        genera_flusso_pagamenti = file
        Using fs1 As FileStream = New FileStream(file, FileMode.Create, FileAccess.Write)
            Using s1 As StreamWriter = New StreamWriter(fs1)
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                'DA SPECIFICARE CORRETTAMENTE LA CATEGORIA  <<<<<<------------------------------------
                Dim sqlStr As String = "SELECT fatture_nolo_pagamenti.data_pagamento, fatture_nolo_pagamenti.codice_contabile,"
                sqlStr += "fatture_nolo_pagamenti.importo, id_ctr_pagamenti_extra, fatture_nolo.cod_edp, fatture_nolo.num_contratto_rif,"
                sqlStr += "stazioni.codice As stazione "
                sqlStr += "FROM fatture_nolo_pagamenti WITH(NOLOCK) LEFT JOIN fatture_nolo ON fatture_nolo_pagamenti.id_fattura_nolo=fatture_nolo.id "
                sqlStr += "LEFT JOIN pagamenti_extra WITH(NOLOCK) ON fatture_nolo_pagamenti.id_ctr_pagamenti_extra=pagamenti_extra.id_ctr "
                sqlStr += "LEFT JOIN stazioni WITH(NOLOCK) ON pagamenti_extra.id_stazione=stazioni.id "
                sqlStr += "WHERE YEAR(fatture_nolo.data_fattura)='" & anno & "' AND fatture_nolo.num_fattura BETWEEN " & CInt(num_fattura_da) & " AND " & CInt(num_fattura_a)
                sqlStr += " AND fatture_nolo.attiva='1' ORDER BY fatture_nolo_pagamenti.id_fattura_nolo ASC"

                Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dim Rs As Data.SqlClient.SqlDataReader
                Rs = Cmd.ExecuteReader()

                s1.WriteLine("000000")

                Do While Rs.Read()
                    Dim stringa As String = String.Format("{0,-8}", Year(Rs("data_pagamento")) & IIf(Len(Month(Rs("data_pagamento")).ToString) = 2, Month(Rs("data_pagamento")), "0" & Month(Rs("data_pagamento"))) & IIf(Len(Day(Rs("data_pagamento")).ToString) = 2, Day(Rs("data_pagamento")), "0" & Day(Rs("data_pagamento"))))
                    stringa += String.Format("{0,-6}", Rs("cod_edp")) & String.Format("{0,-9}", Rs("num_contratto_rif")) & String.Format("{0,-2}", Rs("codice_contabile"))
                    stringa += String.Format("{0,-2}", Rs("stazione")) & String.Format("{0,-8}", Rs("importo"))
                    stringa += "0" & String.Format("{0,-13}", Rs("id_ctr_pagamenti_extra")) & "*"

                    s1.WriteLine(stringa)
                Loop

                Rs.Close()
                Rs = Nothing
                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            End Using
        End Using
    End Function

    Public Shared Function genera_flusso_fatture_nolo(ByVal anno As String, ByVal num_fattura_da As String, ByVal num_fattura_a As String) As String
        Dim file As String = ConfigurationManager.AppSettings.Get("PathFatture") & "TRAFAT.TXT"
        genera_flusso_fatture_nolo = file
        Using fs1 As FileStream = New FileStream(file, FileMode.Create, FileAccess.Write)
            Using s1 As StreamWriter = New StreamWriter(fs1)
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc2.Open()
                'DA SPECIFICARE CORRETTAMENTE LA CATEGORIA  <<<<<<------------------------------------
                Dim sqlStr As String = "SELECT id, data_fattura, num_fattura, cod_edp, num_contratto_rif, totale_fattura, num_voucher, data_fattura_prepagato, numero_fattura_prepagato "
                sqlStr += "FROM fatture_nolo WITH(NOLOCK) "
                sqlStr += "WHERE YEAR(fatture_nolo.data_fattura)='" & anno & "' AND fatture_nolo.num_fattura BETWEEN " & CInt(num_fattura_da) & " AND " & CInt(num_fattura_a) & " AND attiva='1'"

                Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dim Cmd2 As New Data.SqlClient.SqlCommand("", Dbc2)
                Dim Rs2 As Data.SqlClient.SqlDataReader

                Dim Rs As Data.SqlClient.SqlDataReader
                Rs = Cmd.ExecuteReader()

                s1.WriteLine("000000")

                Dim i As Integer
                Dim ass1 As String
                Dim ass2 As String
                Dim ass3 As String

                Dim imp1 As String
                Dim imp2 As String
                Dim imp3 As String

                Dim iva1 As String
                Dim iva2 As String
                Dim iva3 As String

                Do While Rs.Read()

                    Dim stringa As String = String.Format("{0,-4}", Year(Rs("data_fattura")))
                    stringa += String.Format("{0,-6}", Right(Year(Rs("data_fattura")), 2) & IIf(Len(Month(Rs("data_fattura")).ToString) = 2, Month(Rs("data_fattura")), "0" & Month(Rs("data_fattura"))) & IIf(Len(Day(Rs("data_fattura")).ToString) = 2, Day(Rs("data_fattura")), "0" & Day(Rs("data_fattura"))))
                    stringa += String.Format("{0,-6}", Rs("num_fattura")) & String.Format("{0,-6}", Rs("cod_edp")) & String.Format("{0,-9}", Rs("num_contratto_rif"))

                    sqlStr = "SELECT SUM(TOTALE) as totale, SUM(IVA) as iva, MAX(codice_iva) As codice_iva "
                    sqlStr += "FROM fatture_nolo_righe WITH(NOLOCK) WHERE id_fattura=" & Rs("id") & " GROUP BY codice_iva ORDER BY codice_iva DESC"

                    'ATTENZIONE: ATTUALMENTE POSSONO ESSERE ESPORTATI FINO A TRE SCAGLIONI DI IVA, MA QUESTO LIMITE NON E' IMPOSTATO SU ARES; TENERNE CONTO IN CASO
                    'DI ERRORI DI ESPORTAZIONE
                    Cmd2 = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                    Rs2 = Cmd2.ExecuteReader()

                    i = 0
                    ass1 = ""
                    ass2 = ""
                    ass3 = ""

                    imp1 = "0"
                    imp2 = "0"
                    imp3 = "0"

                    iva1 = "0"
                    iva2 = "0"
                    iva3 = "0"

                    Do While Rs2.Read()
                        If i = 0 Then
                            ass1 = Rs2("codice_iva") & ""
                            imp1 = Rs2("totale")
                            iva1 = Rs2("iva")

                            i = i + 1
                        ElseIf i = 1 Then
                            ass2 = Rs2("codice_iva") & ""
                            imp2 = Rs2("totale")
                            iva2 = Rs2("iva")

                            i = i + 1
                        ElseIf i = 2 Then
                            ass3 = Rs2("codice_iva") & ""
                            imp3 = Rs2("totale")
                            iva3 = Rs2("iva")

                            i = i + 1
                        Else

                        End If
                    Loop

                    stringa += String.Format("{0,-3}", ass1) & String.Format("{0,-3}", ass2) & String.Format("{0,-3}", ass3)
                    stringa += String.Format("{0,-8}", FormatNumber(imp1, 2, , , TriState.False)) & String.Format("{0,-8}", FormatNumber(iva1, 2, , , TriState.False))
                    stringa += String.Format("{0,-8}", FormatNumber(imp2, 2, , , TriState.False)) & String.Format("{0,-8}", FormatNumber(iva2, 2, , , TriState.False))
                    stringa += String.Format("{0,-8}", FormatNumber(imp3, 2, , , TriState.False)) & String.Format("{0,-8}", FormatNumber(iva3, 2, , , TriState.False))
                    stringa += String.Format("{0,-8}", FormatNumber(Rs("totale_fattura"), 2, , , TriState.False)) & "N" & String.Format("{0,-11}", Rs("num_voucher"))

                    If Rs("data_fattura_prepagato") & "" <> "" Then
                        stringa += String.Format("{0,-6}", Right(Year(Rs("data_fattura_prepagato")), 2) & IIf(Len(Month(Rs("data_fattura_prepagato")).ToString) = 2, Month(Rs("data_fattura_prepagato")), "0" & Month(Rs("data_fattura_prepagato"))) & IIf(Len(Day(Rs("data_fattura_prepagato")).ToString) = 2, Day(Rs("data_fattura_prepagato")), "0" & Day(Rs("data_fattura_prepagato"))))
                    Else
                        stringa += String.Format("{0,-6}", "")
                    End If

                    If Rs("numero_fattura_prepagato") & "" <> "" Then
                        stringa += String.Format("{0,-6}", Rs("numero_fattura_prepagato"))
                    Else
                        stringa += String.Format("{0,-6}", "0")
                    End If

                    stringa += "*"

                    Dbc2.Close()
                    Dbc2.Open()
                    Rs2.Close()

                    s1.WriteLine(stringa)
                Loop

                Rs2 = Nothing
                Rs.Close()
                Rs = Nothing
                Cmd.Dispose()
                Cmd = Nothing
                Cmd2.Dispose()
                Cmd2 = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
                Dbc2.Close()
                Dbc2.Dispose()
                Dbc2 = Nothing
            End Using
        End Using
    End Function

    Public Shared Function leggi_cli_con(ByVal id_operatore As String) As String
        'RESTITUISCE -1 SE IL FILE cli_con.txt NON E' STATO TROVATO
        'RESTITUISCE 1 SE L'IMPORTAZIONE E' AVVENUTA CORRETTAMENTE
        Dim file_name As String = ConfigurationManager.AppSettings.Get("PathFatture") & "CLI_CON.TXT"

        If Not File.Exists(file_name) Then
            leggi_cli_con = "-1"
        Else

            'LETTURA DEL FILE POPOLANDO LA TABELLA DI APPOGGIO
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM fatture_nolo_clienti_appoggio WHERE id_operatore='" & id_operatore & "'", Dbc)
            Cmd.ExecuteNonQuery()

            Dim objStreamReader As StreamReader
            objStreamReader = File.OpenText(file_name)

            Dim Riga As String
            While (objStreamReader.Peek() <> -1)
                Riga = objStreamReader.ReadLine()

                Cmd = New Data.SqlClient.SqlCommand("INSERT INTO fatture_nolo_clienti_appoggio (id_cliente_fattura, id_cliente_precedente, id_operatore) VALUES ('" & Left(Riga, 6) & "','" & Mid(Riga, 7, 6) & "','" & id_operatore & "')", Dbc)
                Cmd.ExecuteNonQuery()
            End While

            objStreamReader.Close()

            leggi_cli_con = "1"

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End If
    End Function
End Class
