Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Collections.Generic
Imports System.Data
Imports System.IO
Imports System.Web.Script.Serialization

Partial Class sqltest2
    Inherits System.Web.UI.Page

    Dim flag_avvia As Boolean


    Public Ip_Address   'mod. 01.08.2014

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



    End Sub


    Protected Sub Elenco_Veicoli_Click(sender As Object, e As EventArgs)

        Dim id As String = "9999"
        Dim strElencoVeicoli As String = ""
        Dim sta_ini_nolo As String = "2"
        Dim dt_ini_nolo As String = "2020-10-26"
        Dim ora_ini_nolo As String = "11"
        Dim minuti_end As String = "30"
        Dim sta_end_nolo As String = "2"
        Dim dt_end_nolo As String = "2020-10-27"
        Dim ora_end_nolo As String = "11"
        Dim minuti_ini As String = "30"

        Dim prepagata As Boolean = False
        Dim id_tariffe As String = "10"
        Dim id_gruppo As String = "44"
        Dim nome As String = "NomeGuidatore"
        Dim cognome As String = "CognomeGuidatore"
        Dim dtna As String = "08/09/1961"
        Dim email As String = "dimatteo@xinformatica.it"
        Dim ind As String = "via xxxx 1"
        Dim telefono As String = "333123456"
        Dim num_volo_arrivo As String = "AZ1234"
        Dim num_volo_partenza As String = "AZ5678"
        Dim eta As String = "26"
        Dim numero_giorni As String = "1"

        Dim strReturn As String = putDatiPrenotazione(id, dt_ini_nolo, dt_end_nolo, prepagata, ora_ini_nolo, minuti_ini, ora_end_nolo, minuti_end, id_tariffe, id_gruppo, sta_ini_nolo, sta_end_nolo, nome, cognome, dtna, email, ind, telefono, num_volo_arrivo, num_volo_partenza, eta, numero_giorni)

        Response.Write("putDatiPrenotazione: " & strReturn & " -- " & Date.Now.ToString & "")



    End Sub



    '' FUNZIONI GLOBALI da copiare in webservice
    Public Function putDatiPrenotazione(ByVal Id As String, ByVal data_inizio_completa As String, ByVal data_fine_completa As String, ByVal prepagata As Boolean, ByVal ora_inizio As String, ByVal minuti_inizio As String, ByVal ora_fine As String, ByVal minuti_fine As String, ByVal id_tariffe_righe As String, ByVal id_gruppo As String, ByVal stazione As String, ByVal stazione_off As String, ByVal nome As String, ByVal cognome As String, ByVal data_nascita As String, ByVal email As String, ByVal indirizzo As String, ByVal telefono As String, ByVal num_volo_arrivo As String, ByVal num_volo_partenza As String, ByVal eta As String, ByVal numero_giorni As String) As String

        Dim sqla As String = ""
        Try

            Dim data_uscita As String = data_inizio_completa
            Dim data_rientro As String = data_fine_completa

            'data_uscita = getDataDb_senza_orario(data_uscita, Request.ServerVariables("HTTP_HOST"))
            'data_rientro = getDataDb_senza_orario(data_rientro, Request.ServerVariables("HTTP_HOST"))

            Dim tipo_cliente As String = "3"



            Dim DbcTariffa As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            DbcTariffa.Open()
            Dim CmdTariffa As New Data.SqlClient.SqlCommand("SELECT id_tariffa  FROM tariffe_righe WITH(NOLOCK)  WHERE (id = '" & id_tariffe_righe & "')", DbcTariffa)


            Dim id_tariffa As String = CmdTariffa.ExecuteScalar

            CmdTariffa.Dispose()
            CmdTariffa = Nothing
            DbcTariffa.Close()
            DbcTariffa.Dispose()
            DbcTariffa = Nothing


            'Dim id_tariffa As String
            'If prepagata Then
            '    id_tariffa = id_tariffe_righe_prepagate
            'Else
            '    id_tariffa = id_tariffe_righe
            'End If

            'Dim id_tariffe_righe As String = lbl_id_tariffe_righe.Text

            Dim tipo_tariffa As String = "fonte"
            Dim pren_no_tariffa As String = "0"
            Dim prora_out As String  'ORARIO USCITA
            Dim prora_pr As String   'ORARIO PRESUNTO RIENTRO
            prora_out = "1900-01-01 " & ora_inizio & ":" & minuti_inizio
            prora_pr = "1900-01-01 " & ora_fine & ":" & minuti_fine
            Dim codice_tariffa As String = "8"

            Dim DbcTariffa2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            DbcTariffa2.Open()
            Dim CmdTariffa2 As New Data.SqlClient.SqlCommand("SELECT  tariffe.CODTAR  FROM tariffe WITH(NOLOCK)  WHERE  (tariffe.id = '" & id_tariffa & "')", DbcTariffa2)

            Dim cod As String = CmdTariffa2.ExecuteScalar

            CmdTariffa2.Dispose()
            CmdTariffa2 = Nothing
            DbcTariffa2.Close()
            DbcTariffa2.Dispose()
            DbcTariffa2 = Nothing


            If data_nascita = "" Then
                data_nascita = "NULL"
            Else
                data_nascita = funzioni_comuni.GetDataSql(data_nascita, 0)
            End If

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(MAX(NUMPREN),0) FROM prenotazioni WITH(NOLOCK) WHERE codice_provenienza='" & stazione & "'", Dbc)

            Dim num_prenotazione As Integer = Cmd.ExecuteScalar + 1

            If num_prenotazione = 1 Then
                'SE E' LA PRIMA PRENOTAZIONE DELLA STAZIONE RESTITUISCO IL PRIMO NUMERO
                num_prenotazione = CInt(Left(stazione, 2) & "000001")
            End If
            Cmd.Dispose()


            Dim TAR_VAL_DAL As String
            Dim TAR_VAL_AL As String
            Cmd = New Data.SqlClient.SqlCommand("SELECT vendibilita_da FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)
            TAR_VAL_DAL = Cmd.ExecuteScalar

            Cmd = New Data.SqlClient.SqlCommand("SELECT vendibilita_A FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)
            TAR_VAL_AL = Cmd.ExecuteScalar

            TAR_VAL_DAL = Year(TAR_VAL_DAL) & "-" & Month(TAR_VAL_DAL) & "-" & Day(TAR_VAL_DAL) & " 00:00:00"

            TAR_VAL_AL = Year(TAR_VAL_AL) & "-" & Month(TAR_VAL_AL) & "-" & Day(TAR_VAL_AL) & " 00:00:00"

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            Dim volo_out As String = Replace(num_volo_arrivo, "'", "''")
            Dim volo_pr As String = Replace(num_volo_partenza, "'", "''")
            Dim id_operatore As String = Costanti.costanti_web.id_operatore_web

            Dim sqlStr2 As String = "INSERT INTO prenotazioni (NUMPREN,num_calcolo,status,attiva,provenienza,codice_provenienza,PRID_stazione_out,PRID_stazione_pr,"

            sqlStr2 += "PRDATA_OUT,ore_uscita,minuti_uscita,PRDATA_PR,ore_rientro,minuti_rientro,ID_GRUPPO,ID_GRUPPO_APP,id_conducente,nome_conducente,"
            sqlStr2 += "cognome_conducente,eta_primo_guidatore,eta_secondo_guidatore,data_nascita,mail_conducente,indirizzo_conducente,riferimento_telefono,"
            sqlStr2 += "id_fonte,codice_edp,id_cliente,id_tariffa,id_tariffe_righe,tipo_tariffa,pren_broker_no_tariffa,sconto_applicato, tipo_sconto,"
            sqlStr2 += "id_preventivo,id_operatore_creazione,DATAPREN,PRORA_OUT,PRORA_PR,giorni,CODTAR,codice,"
            sqlStr2 += "TAR_VAL_DAL,TAR_VAL_AL,N_VOLOOUT,N_VOLOPR,"
            sqlStr2 += "IMPORTO_PREVENTIVO, importo_a_carico_del_broker, rif_to, giorni_to, id_fonte_commissionabile, tipo_commissione, commissione_percentuale, giorni_commissioni"
            sqlStr2 += ") VALUES ("
            sqlStr2 += "'" & num_prenotazione & "','1',0,'1','web','" & stazione & "','" & stazione & "','" & stazione_off & "',"
            sqlStr2 += "convert(date,'" & data_uscita & "',102),'" & ora_inizio & "','" & minuti_inizio & "',convert(date,'" & data_rientro & "',102),"
            sqlStr2 += "'" & ora_fine & "','" & minuti_fine & "','" & id_gruppo & "','" & id_gruppo & "',NULL,'" & Replace(nome, "'", "''") & "',"
            sqlStr2 += "'" & Replace(cognome, "'", "''") & "','" & eta & "','',convert(date,'" & data_nascita & "',102),'" & Replace(email, "'", "''") & "','" & Replace(indirizzo, "'", "''") & "','" & Replace(telefono, "'", "''") & "',"
            sqlStr2 += "'" & tipo_cliente & "',NULL,'1'," & id_tariffa & "," & id_tariffe_righe & ",'" & tipo_tariffa & "'," & pren_no_tariffa & ",'0','0',"

            sqlStr2 += "'" & Id & "','" & id_operatore & "',convert(date,GetDate(),102),'" & prora_out & "','" & prora_pr & "','" & numero_giorni & "','" & cod & "','" & cod & "',"
            sqlStr2 += "convert(date,'" & TAR_VAL_DAL & "',102),convert(date,'" & TAR_VAL_AL & "',102),'" & volo_out & "','" & volo_pr & "',"
            sqlStr2 += "NULL,NULL,'',NULL,NULL,NULL,NULL,NULL)"

            sqla = sqlStr2

            'HttpContext.Current.Trace.Write("sqlStr2 " & sqlStr2)


            ''Response.Write(sqlStr)
            'Response.End()
            Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc2.Open()
            Dim Cmd2 = New Data.SqlClient.SqlCommand(sqlStr2, Dbc2)
            Dim x2 As Integer = Cmd2.ExecuteNonQuery()
            If x2 <> 1 Then
                ' num_prenotazione = "02"
            End If

            Cmd2.Dispose()
            Cmd2 = Nothing

            Dbc2.Close()
            Dbc2.Dispose()
            Dbc2 = Nothing

            'Dim Sql3 = "SELECT @@IDENTITY FROM prenotazioni WITH(NOLOCK)"
            Dim Sql3 = "SELECT MAX(Nr_Pren) FROM prenotazioni WITH(NOLOCK)"
            'HttpContext.Current.Trace.Write("Sql3 " & Sql3)
            sqla = Sql3



            Dim Dbc3 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc3.Open()
            Dim Cmd3 = New Data.SqlClient.SqlCommand(Sql3, Dbc3)

            Dim id_prenotazione As String = Cmd3.ExecuteScalar

            Cmd3.Dispose()
            Cmd3 = Nothing

            Dbc3.Close()
            Dbc3.Dispose()
            Dbc3 = Nothing


            'SALVO LA RIGA DI CALCOLO E DI WARNING NELLE TABELLE PRENOTAZIONI_COSTI E PRENOTAZIONI_WARNING
            Dim sqlStr = "INSERT INTO prenotazioni_costi (id_documento, num_calcolo, ordine_stampa, id_gruppo, id_elemento, num_elemento, nome_costo," &
            "selezionato,omaggiato, prepagato, valore_costo,valore_percentuale,imponibile,iva_imponibile,imponibile_scontato,iva_imponibile_scontato," &
            "imponibile_onere, iva_onere, aliquota_iva,iva_inclusa,scontabile,omaggiabile,acquistabile_nolo_in_corso,id_a_carico_di,id_metodo_stampa,obbligatorio, franchigia_attiva, id_unita_misura, qta, packed,imponibile_scontato_prepagato,iva_imponibile_scontato_prepagato,imponibile_onere_prepagato,iva_onere_prepagato, commissioni_imponibile,sconto_su_imponibile_prepagato, commissioni_iva, commissioni_imponibile_originale, commissioni_iva_originale) " &
            "(SELECT '" & id_prenotazione & "','1', ordine_stampa, id_gruppo,id_elemento,num_elemento,nome_costo," &
            "selezionato,omaggiato,prepagato, valore_costo,valore_percentuale,imponibile,iva_imponibile,imponibile_scontato,iva_imponibile_scontato," &
            "imponibile_onere, iva_onere, aliquota_iva,iva_inclusa,scontabile,omaggiabile,acquistabile_nolo_in_corso,id_a_carico_di,id_metodo_stampa,obbligatorio, franchigia_attiva, id_unita_misura,qta, packed,imponibile_scontato_prepagato,iva_imponibile_scontato_prepagato,imponibile_onere_prepagato, commissioni_imponibile,iva_onere_prepagato,sconto_su_imponibile_prepagato, commissioni_iva, commissioni_imponibile_originale, commissioni_iva_originale FROM preventivi_web_costi WITH(NOLOCK) WHERE id_documento='" & Id & "' AND num_calcolo='1')"
            sqla = sqlStr

            'HttpContext.Current.Trace.Write("sqlStr " & sqlStr)

            Dim Dbc4 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc4.Open()
            Dim Cmd4 = New Data.SqlClient.SqlCommand(sqlStr, Dbc4)
            Dim x4 As Integer = Cmd4.ExecuteNonQuery()
            If x4 <> 1 Then
                'num_prenotazione = "04"
            End If

            Cmd4.Dispose()
            Cmd4 = Nothing

            Dbc4.Close()
            Dbc4.Dispose()
            Dbc4 = Nothing

            Return num_prenotazione

            'Response.Redirect("riepilogo.aspx?prenotazione=" & id_preventivo & "&num=" & id_prenotazione)           
        Catch ex As Exception
            Return ex.Message & "<br/>" & sqla & "<br/>"
        End Try
    End Function

End Class
