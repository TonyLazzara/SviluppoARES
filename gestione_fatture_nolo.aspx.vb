Partial Class gestione_fatture_nolo
    Inherits System.Web.UI.Page

    'Dim sqlTotaleIncassato As String = "(SELECT ISNULL(SUM(per_importo),0) FROM pagamenti_extra WITH(NOLOCK) WHERE " & _
    '               "(N_CONTRATTO_RIF=contratti.num_contratto OR N_PREN_RIF=contratti.num_prenotazione) AND " & _
    '               "(id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Chiusura & "' " & _
    '               "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Vendita & "' " & _
    '               "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso & "' " & _
    '               "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Pagamento_Contanti & "' " & _
    '               "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Attivo & "' " & _
    '               "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Passivo & "' " & _
    '               "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.MCR_Pagamento_Cash & "' " & _
    '               "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso & "' " & _
    '               "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Deposito_su_RA & "' " & _
    '               "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso_su_RA & "') AND operazione_stornata='0') "

    Dim sqlTotaleIncassato As String = "(SELECT ISNULL(SUM(per_importo),0) FROM pagamenti_extra WITH(NOLOCK) WHERE " & _
                   "(N_CONTRATTO_RIF=contratti.num_contratto OR N_PREN_RIF=contratti.num_prenotazione) AND " & _
                   "(id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Chiusura & "' " & _
                   "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Vendita & "' " & _
                   "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso & "' " & _
                   "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Pagamento_Contanti & "' " & _
                   "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.MCR_Pagamento_Cash & "' " & _
                   "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso & "' " & _
                   "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Deposito_su_RA & "' " & _
                   "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso_su_RA & "') AND operazione_stornata='0') "

    Dim sqlTotaleIncassatoNoBroker As String = "(SELECT ISNULL(SUM(per_importo),0) FROM pagamenti_extra WITH(NOLOCK) WHERE " & _
                   "(N_CONTRATTO_RIF=contratti.num_contratto OR N_PREN_RIF=contratti.num_prenotazione) AND " & _
                   "(id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Chiusura & "' " & _
                   "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Vendita & "' " & _
                   "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso & "' " & _
                   "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Pagamento_Contanti & "' " & _
                   "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.MCR_Pagamento_Cash & "' " & _
                   "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso & "' " & _
                   "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Deposito_su_RA & "' " & _
                   "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso_su_RA & "') AND operazione_stornata='0' AND pagamento_broker='0') "

    Dim sqlTotaleAbbuoni As String = "(SELECT ISNULL(SUM(per_importo),0) FROM pagamenti_extra WITH(NOLOCK) WHERE " & _
                   "(N_CONTRATTO_RIF=contratti.num_contratto OR N_PREN_RIF=contratti.num_prenotazione) AND " & _
                   "(id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Attivo & "' " & _
                   "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Passivo & "') AND operazione_stornata='0') "


    Dim sqlCostoTotale As String = "(contratti_costi1.imponibile_scontato+contratti_costi1.iva_imponibile_scontato+ " & _
                   "ISNULL(contratti_costi1.imponibile_onere,0)+ISNULL(contratti_costi1.iva_onere,0)) "

    Dim sqlTempoKm As String = "(contratti_costi2.imponibile_scontato+contratti_costi2.iva_imponibile_scontato+ " & _
               "ISNULL(contratti_costi2.imponibile_onere,0)+ISNULL(contratti_costi2.iva_onere,0)) "

    Dim sqlTotaleIncassatoSoloContratto As String = " (SELECT ISNULL(SUM(per_importo),0) FROM pagamenti_extra WITH(NOLOCK) WHERE " & _
       " (N_CONTRATTO_RIF=contratti.num_contratto)  " & _
       " AND (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Chiusura & "' " & _
               "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Vendita & "' " & _
               "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso & "' " & _
               "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Pagamento_Contanti & "' " & _
               "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.MCR_Pagamento_Cash & "' " & _
               "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Carta_Credito_Telefonico_Incasso & "' " & _
               "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Deposito_su_RA & "' " & _
               "OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Rimborso_su_RA & "')" & _
       " AND operazione_stornata='0') "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblOrderBY.Text = " ORDER BY Cast(contratti.data_uscita As DateTime) ASC"

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Fatturazione) = "1" Then
                Response.Redirect("default.aspx")
            End If

            'ricerca(lblOrderBY.Text)
        Else
            sqlDaFatturare.SelectCommand = query_cerca.Text
        End If
    End Sub

    Protected Function condizione_where() As String
        'CONDIZIONE WHERE --------------------
        Dim condizione As String = ""
        If cercaStazionePickUp.SelectedValue <> "0" Then
            condizione = condizione & " AND contratti.id_stazione_uscita='" & cercaStazionePickUp.SelectedValue & "'"
        End If

        If cercaStato.SelectedValue = "0" Then
            condizione = condizione & "AND (contratti.status='" & Costanti.stato_contratto.da_fatturare & "' OR contratti.status='" & Costanti.stato_contratto.da_incassare & "')"
        ElseIf cercaStato.SelectedValue = "1" Then
            condizione = condizione & "AND contratti.status='" & Costanti.stato_contratto.da_fatturare & "'"
        ElseIf cercaStato.SelectedValue = "2" Then
            condizione = condizione & "AND contratti.status='" & Costanti.stato_contratto.da_fatturare & "' AND non_fatturare='1'"
        ElseIf cercaStato.SelectedValue = "3" Then
            condizione = condizione & "AND contratti.status='" & Costanti.stato_contratto.da_incassare & "'"
        ElseIf cercaStato.SelectedValue = "4" Then
            condizione = condizione & "AND contratti.status='" & Costanti.stato_contratto.da_fatturare & "' AND importo_a_carico_del_broker>0 AND NOT contratti.num_contratto IN (SELECT TOP 1 n_contratto_rif FROM pagamenti_extra WITH(NOLOCK) WHERE pagamento_broker=1 AND n_contratto_rif=contratti.num_contratto)"
        End If


        Dim ndatastr As String
        Dim ndata As Date

        If txtRientroDa.Text <> "" And txtRientroA.Text = "" Then

            Dim da_data As String = funzioni_comuni.getDataDb_senza_orario(txtRientroDa.Text)
            condizione = condizione & " AND contratti.data_rientro >= convert(date,'" & da_data & "',102) "

        ElseIf txtRientroDa.Text = "" And txtRientroA.Text <> "" Then
            ndata = CDate(txtRientroA.Text)                             'aggiornato 06.04.2021
            ndata = DateAdd("d", 1, ndata)
            ndatastr = Year(ndata) & "-" & Month(ndata) & "-" & Day(ndata) & " 00:00:00"

            Dim a_data As String = ndatastr ' funzioni_comuni.getDataDb_con_orario(ndata.ToString & " 00:00:00")

            condizione = condizione & " AND contratti.data_rientro <=convert(date,'" & a_data & "',102) "
        ElseIf txtRientroDa.Text <> "" And txtRientroA.Text <> "" Then
            Dim da_data As String = funzioni_comuni.getDataDb_con_orario(txtRientroDa.Text)
            Dim a_data As String = funzioni_comuni.getDataDb_con_orario(txtRientroA.Text & " 23:59:59")
            'ndata = CDate(txtRientroA.Text)
            'ndata = DateAdd("d", 1, ndata)
            'ndatastr = Year(ndata) & "-" & Month(ndata) & "-" & Day(ndata) & " 00:00:00"
            condizione = condizione & " AND contratti.data_rientro BETWEEN convert(date,'" & da_data & "',102) AND convert(date,'" & a_data & "',102) "
        End If

        If txtUscitaDa.Text <> "" And txtUscitaA.Text = "" Then
            Dim da_data As String = funzioni_comuni.getDataDb_senza_orario(txtUscitaDa.Text)
            condizione = condizione & " AND contratti.data_uscita >=convert(date,'" & da_data & "',102) "
        ElseIf txtUscitaDa.Text = "" And txtUscitaA.Text <> "" Then
            Dim a_data As String = funzioni_comuni.getDataDb_con_orario(txtUscitaA.Text & " 23:59:59")
            condizione = condizione & " AND contratti.data_uscita <=convert(date,'" & a_data & "',102) "
        ElseIf txtUscitaDa.Text <> "" And txtUscitaA.Text <> "" Then
            Dim da_data As String = funzioni_comuni.getDataDb_senza_orario(txtUscitaDa.Text)
            Dim a_data As String = funzioni_comuni.getDataDb_con_orario(txtUscitaA.Text & " 23:59:59")
            condizione = condizione & " AND contratti.data_uscita BETWEEN convert(date,'" & da_data & "',102) AND convert(date,'" & a_data & "',102) "
        End If

        If dropDaControllare.SelectedValue = "1" Then
            condizione = condizione & " AND contratti.giorni>25"
        ElseIf dropDaControllare.SelectedValue = "2" Then
            'BROKER: GIORNI < GIORNI VOUCHER
            condizione = condizione & " AND ISNULL(contratti.giorni_to,0)>contratti.giorni"
        ElseIf dropDaControllare.SelectedValue = "3" Then
            'PREPAGATI: GIORNI < GIORNI VOUCHER
            condizione = condizione & " AND ISNULL(contratti.giorni_prepagati,0)>contratti.giorni"
        ElseIf dropDaControllare.SelectedValue = "4" Then
            'TOTALEMNETE NON INCASSATI
            condizione = condizione & " AND " & sqlTotaleIncassato & "=0 AND " & sqlCostoTotale & "<>0"
        ElseIf dropDaControllare.SelectedValue = "5" Then
            'SALDO MAGGIORE DI 0
            condizione = condizione & " AND " &
                   "(CASE WHEN giorni>=ISNULL(giorni_to,0) THEN " &
                   sqlCostoTotale & "+ISNULL(" & sqlTotaleAbbuoni & ",0)-ISNULL(importo_a_carico_del_broker,0)-ISNULL(" & sqlTotaleIncassatoNoBroker & ",0) ELSE " &
                   " ISNULL(" & sqlCostoTotale & ",0)+ISNULL(" & sqlTotaleAbbuoni & ",0)-ISNULL(" & sqlTempoKm & ",0)-ISNULL(" & sqlTotaleIncassato & ",0) END " &
                   " BETWEEN 0.01 AND (CASE WHEN giorni>=ISNULL(giorni_to,0) THEN " &
                   " ISNULL(" & sqlCostoTotale & ",0)-ISNULL(importo_a_carico_del_broker,0) ELSE " &
                   " ISNULL(" & sqlCostoTotale & ",0)-ISNULL(" & sqlTempoKm & ",0) END)  -0.01)"



        ElseIf dropDaControllare.SelectedValue = "6" Then
            'SALDO MINORE DI 0
            condizione = condizione & " AND " &
                   "(CASE WHEN giorni>=ISNULL(giorni_to,0) THEN " &
                   " ISNULL(" & sqlCostoTotale & ",0)+ISNULL(" & sqlTotaleAbbuoni & ",0)-ISNULL(importo_a_carico_del_broker,0)-ISNULL(" & sqlTotaleIncassatoNoBroker & ",0) ELSE " &
                   " ISNULL(" & sqlCostoTotale & ",0)+ISNULL(" & sqlTotaleAbbuoni & ",0)-ISNULL(" & sqlTempoKm & ",0)-ISNULL(" & sqlTotaleIncassato & ",0) END < -0.05)"
        ElseIf dropDaControllare.SelectedValue = "7" Then
            'CON CRV
            condizione = condizione & " AND ISNULL(num_crv,0)>0"
        ElseIf dropDaControllare.SelectedValue = "8" Then
            'GRUPPO CONSEGNATO DIVERSO DA GRUPPO PRENOTATO
            condizione = condizione & " AND " &
                   " NOT contratti.id_gruppo_da_prenotazione IS NULL AND ISNULL(id_gruppo_da_prenotazione,0)<>id_gruppo_auto AND " &
                   " ISNULL(id_gruppo_da_prenotazione,0)<>ISNULL(id_gruppo_app,0)"
        ElseIf dropDaControllare.SelectedValue = "9" Then
            'GRUPPO CONSEGNATO DIVERSO DA GRUPPO DA FATTURARE
            condizione = condizione & " AND " &
                   " ISNULL(id_gruppo_app,id_gruppo_auto)<>id_gruppo_auto "
        ElseIf dropDaControllare.SelectedValue = "10" Then
            'FATTURA DA INVIARE PER MAIL E CAMPO MAIL ASSENTE O NON CORRETTO
            condizione = condizione & " AND " &
                   "contratti_ditte.tipo_spedizione_fattura='M' AND (contratti_ditte.email IS NULL OR contratti_ditte.email='' OR " &
                   "NOT (contratti_ditte.email LIKE '%_@_%_.__%' AND contratti_ditte.email NOT LIKE '%[^a-z,0-9,@,.]%')) "
        ElseIf dropDaControllare.SelectedValue = "11" Then
            'ABBUONO DA CONTROLLARE (SE E' STATO FATTO ALMENO UN ABBUONO)
            condizione = condizione & " AND " &
                   "(SELECT TOP 1 ISNULL(NR_CONTRATTO,0) FROM pagamenti_extra WITH(NOLOCK) WHERE " &
                       " (N_CONTRATTO_RIF=contratti.num_contratto OR N_PREN_RIF=contratti.num_prenotazione)  " &
                       " AND (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Attivo & "' OR id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Abbuono_Passivo & "') ) <> 0 "
        ElseIf dropDaControllare.SelectedValue = "12" Then
            'DA CONTROLLARE SU RICHIESTA DELL'OPERATORE DI STAZIONE
            condizione = condizione & " AND ISNULL(contratti.fatturazione_da_controllare,'0')='1'"
        ElseIf dropDaControllare.SelectedValue = "13" Then
            'CHIUSURA PREAUTORIZZAZIONE 0,05 - DITTA DIVERSA DA CASH
            condizione = condizione & " AND (contratti.status='8' OR contratti.status='4') AND " &
                   sqlTotaleIncassatoSoloContratto & "=0.05 AND contratti.id_cliente<>'" & funzioni_comuni.id_cliente_cash & "'"
        ElseIf dropDaControllare.SelectedValue = "14" Then
            condizione = condizione & " AND clienti_tipologia.broker='1' AND clienti_tipologia.id_ditta IS NULL "
        End If

        condizione_where = condizione
    End Function

    Protected Function condizione_where_stampa() As String
        'CONDIZIONE WHERE PER IL REPORT --------------------
        Dim condizione As String = ""
        If cercaStazionePickUp.SelectedValue <> "0" Then
            condizione = condizione & " AND contratti.id_stazione_uscita='" & cercaStazionePickUp.SelectedValue & "'"
        End If

        If txtRientroDa.Text <> "" And txtRientroA.Text = "" Then
            Dim da_data As String = funzioni_comuni.getDataDb_senza_orario(txtRientroDa.Text)
            condizione = condizione & " AND contratti.data_rientro >=convert(date,'" & da_data & "',102) "
        ElseIf txtRientroDa.Text = "" And txtRientroA.Text <> "" Then
            Dim a_data As String = funzioni_comuni.getDataDb_con_orario(txtRientroA.Text & " 23:59:59")
            condizione = condizione & " AND contratti.data_rientro <=convert(date,'" & a_data & "',102) "
        ElseIf txtRientroDa.Text <> "" And txtRientroA.Text <> "" Then
            Dim da_data As String = funzioni_comuni.getDataDb_con_orario(txtRientroDa.Text)
            Dim a_data As String = funzioni_comuni.getDataDb_con_orario(txtRientroA.Text & " 23:59:59")
            condizione = condizione & " AND contratti.data_rientro BETWEEN convert(date,'" & da_data & "',102) AND convert(date,'" & a_data & "',102) "
        End If

        If txtUscitaDa.Text <> "" And txtUscitaA.Text = "" Then
            Dim da_data As String = funzioni_comuni.getDataDb_senza_orario(txtUscitaDa.Text)
            condizione = condizione & " AND contratti.data_uscita >=convert(date,'" & da_data & "',102) "
        ElseIf txtUscitaDa.Text = "" And txtUscitaA.Text <> "" Then
            Dim a_data As String = funzioni_comuni.getDataDb_con_orario(txtUscitaA.Text & " 23:59:59")
            condizione = condizione & " AND contratti.data_uscita <=convert(date,'" & a_data & "',102) "
        ElseIf txtUscitaDa.Text <> "" And txtRientroA.Text <> "" Then
            Dim da_data As String = funzioni_comuni.getDataDb_senza_orario(txtUscitaDa.Text)
            Dim a_data As String = funzioni_comuni.getDataDb_con_orario(txtUscitaA.Text & " 23:59:59")
            condizione = condizione & " AND contratti.data_uscita BETWEEN convert(date,'" & da_data & "',102) AND convert(date,'" & a_data & "',102) "
        End If

        condizione_where_stampa = condizione
    End Function

    Protected Sub ricerca(ByVal order_by As String)
        Dim sqla As String = "SELECT contratti.id,  contratti.num_contratto, contratti.num_calcolo, ISNULL(non_fatturare,'0') As non_fatturare, contratti.status," &
                   "prenotazione_prepagata As prepagata, contratti.giorni, DATEPART(Ms, contratti.data_creazione) As milli," &
                   "ISNULL(giorni_to,0) As giorni_to,gruppi1.cod_gruppo As gruppo_fatturare, contratti.codice_edp, contratti.id_cliente," &
                   "ISNULL(gruppi2.cod_gruppo, gruppi1.cod_gruppo) As gruppo_consegnare, contratti.codtar, contratti.num_crv," &
                   sqlTotaleIncassato & " As totale_incassato, " &
                   sqlTotaleIncassatoNoBroker & " As totale_incassato_no_broker, " &
                   sqlTotaleAbbuoni & " As totale_abbuoni, " &
                   sqlCostoTotale & " As costo_totale," &
                   sqlTempoKm & " As costo_tempo_km," &
                   "ISNULL(contratti.importo_a_carico_del_broker,0) As importo_a_carico_del_broker, " &
                   "ISNULL(giorni_prepagati,0) As giorni_prepagati," &
                   "ISNULL((SELECT TOP 1 id FROM veicoli_evento_apertura_danno WHERE id_tipo_documento_apertura='1' AND id_documento_apertura=contratti.num_contratto " &
                   "AND (stato_rds<>'" & sessione_danni.stato_rds.Chiuso & "' " &
                   "AND stato_rds<>'" & sessione_danni.stato_rds.Da_fatturare & "' " &
                   "AND stato_rds<>'" & sessione_danni.stato_rds.Fatturato & "')),0) As rds " &
                   "FROM contratti WITH(NOLOCK) " &
                   "LEFT JOIN contratti_status WITH(NOLOCK) ON contratti.status=contratti_status.id " &
                   "LEFT JOIN clienti_tipologia WITH(NOLOCK) ON contratti.id_fonte=clienti_tipologia.id " &
                   "LEFT JOIN contratti_ditte WITH(NOLOCK) ON contratti.id_cliente=contratti_ditte.id_ditta AND contratti_ditte.num_contratto=contratti.num_contratto " &
                   "LEFT JOIN gruppi As gruppi1 WITH(NOLOCK) ON contratti.id_gruppo_auto=gruppi1.id_gruppo " &
                   "LEFT JOIN gruppi As gruppi2 WITH(NOLOCK) ON contratti.id_gruppo_app=gruppi2.id_gruppo " &
                   "LEFT JOIN contratti_costi As contratti_costi1 WITH(NOLOCK) ON contratti_costi1.id_documento=contratti.id AND contratti_costi1.num_calcolo=contratti.num_calcolo AND contratti_costi1.nome_costo='totale' " &
                   "LEFT JOIN contratti_costi As contratti_costi2 WITH(NOLOCK) ON contratti_costi2.id_documento=contratti.id AND contratti_costi2.num_calcolo=contratti.num_calcolo AND contratti_costi2.id_elemento='98'  " &
                   "WHERE contratti.attivo='1' " &
                   condizione_where() & " "

        query_cerca.Text = sqla

        query_cerca.Text = query_cerca.Text & order_by

        'Response.Write(query_cerca.Text)
        Try
            sqlDaFatturare.SelectCommand = query_cerca.Text
            listDaFatturare.DataBind()
        Catch ex As Exception
            HttpContext.Current.Response.Write("error ricerca " & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Protected Sub listDaFatturare_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listDaFatturare.ItemDataBound
        'TARIFFE BROKER: SE C'E' STATO RIENTRO ANTICIPATO DEL CLIENTE E I GIORNI NON SONO RIMBORSABILI ALLORA I GIORNI A CARICO DEL CLIENTE
        '(CHE AL MERO CALCOLO VERREBBERO NEGATIVI) SONO 0
        Dim giorni_cliente As Label = e.Item.FindControl("giorni_cliente")
        If CInt(giorni_cliente.Text) < 0 Then
            giorni_cliente.Text = "0"
        End If

        'SE IL CLIENTE A CUI FATTURARE E' CLIENTE CASH NON MOSTRO L'ANAGRAFICA
        Dim codice_edp As Label = e.Item.FindControl("codice_edp")
        If codice_edp.Text = Costanti.codice_cash Then
            e.Item.FindControl("image_fatturare_a").Visible = False
            codice_edp.Visible = False
        End If

        'CRV
        Dim num_crv As Label = e.Item.FindControl("num_crv")
        If CInt(num_crv.Text) > 0 Then
            Dim chk_crv As CheckBox = e.Item.FindControl("chk_crv")
            chk_crv.Checked = True
        End If

        'RDS - SE E' STATO TROVATO UN ID DI UN DANNO APERTO PER IL CONTRATTO VUOL DIRE CHE C'E' UN RDS
        Dim rds As Label = e.Item.FindControl("rds")
        If CInt(rds.Text) <> 0 Then
            Dim chk_rds As CheckBox = e.Item.FindControl("chk_rds")
            chk_rds.Checked = True
        End If

        'TOTALE A CARICO DEL CLIENTE - SE LA TARIFFA NON E' BROKER GIORNI_TO E IMPORTO_A_CARICO_DEL_BROKER SONO 0
        Dim giorni As Label = e.Item.FindControl("giorni")
        Dim giorni_to As Label = e.Item.FindControl("giorni_to")
        Dim importo_cliente As Label = e.Item.FindControl("importo_cliente")
        Dim importo_a_carico_del_broker As Label = e.Item.FindControl("importo_a_carico_del_broker")
        Dim costo_tempo_km As Label = e.Item.FindControl("costo_tempo_km")
        Dim costo_totale As Label = e.Item.FindControl("costo_totale")
        Dim totale_abbuoni As Label = e.Item.FindControl("totale_abbuoni")

        If CInt(giorni.Text) >= CInt(giorni_to.Text) Then
            importo_cliente.Text = FormatNumber(costo_totale.Text - importo_a_carico_del_broker.Text, 2)
        Else
            importo_cliente.Text = FormatNumber(costo_totale.Text - costo_tempo_km.Text, 2)
        End If

        'SALDO
        Dim saldo As Label = e.Item.FindControl("saldo")
        Dim totale_incassato As Label = e.Item.FindControl("totale_incassato")
        Dim totale_incassato_no_broker As Label = e.Item.FindControl("totale_incassato_no_broker")
        If CInt(giorni.Text) >= CInt(giorni_to.Text) Then
            saldo.Text = FormatNumber(costo_totale.Text + CDbl(totale_abbuoni.Text) - importo_a_carico_del_broker.Text - totale_incassato_no_broker.Text, 2)
        Else
            saldo.Text = FormatNumber(costo_totale.Text + CDbl(totale_abbuoni.Text) - costo_tempo_km.Text - totale_incassato_no_broker.Text, 2)
        End If

        'TOTALE A CARICO DEL BROKER
        If CDbl(importo_a_carico_del_broker.Text) = 0 Then
            importo_a_carico_del_broker.Visible = False
        Else
            importo_a_carico_del_broker.Text = FormatNumber(importo_a_carico_del_broker.Text, 2)
        End If

        'STATO CONTRATTO
        Dim status As Label = e.Item.FindControl("status")
        If status.Text = Costanti.stato_contratto.da_incassare Then
            status.Text = "Inc"
        ElseIf status.Text = Costanti.stato_contratto.da_fatturare Then
            status.Text = "Fatt"
        End If

        'GIORNI TO/GIORNI PREPAGATI
        If giorni_to.Text = "0" Then
            Dim giorni_prepagati As Label = e.Item.FindControl("giorni_prepagati")
            If giorni_prepagati.Text <> "0" Then
                giorni_prepagati.Visible = True
                giorni_to.Visible = False
            End If
        End If
    End Sub

    Protected Sub listDaFatturare_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listDaFatturare.ItemCommand
        If e.CommandName = "vedi_ra" Then
            'setta_session_x_contratto()
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                Dim id As Label = e.Item.FindControl("id")

                Dim nco As LinkButton = e.Item.FindControl("num_contratto")


                Session("carica_contratto") = id.Text
                Dim num_co As String = nco.Text ' funzioni_comuni_new.GetNumContratto(id.Text)
                'contratti.aspx?nr=102211031
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('contratti.aspx?nr=" & num_co & "','')", True)

            End If
        End If
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerca.Click
        ricerca(lblOrderBY.Text)
    End Sub

    Protected Sub btnStampaFatturazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaFatturazione.Click
        Dim condizione As String = condizione_where_stampa()

        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            Session("url_print") = "/stampe/contratti/controllo_fatturazione.aspx?orientamento=orizzontale&query=" & Server.UrlEncode(condizione) & "&utente=" & Request.Cookies("SicilyRentCar")("idUtente") & "&header_html=/stampe/contratti/header_controllo_fatturazione.aspx?orientamento"
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
            End If
        End If
    End Sub

    Protected Sub btnFattDaControllare_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFattDaControllare.Click
        If txtUscitaDa.Text = "" Or txtUscitaA.Text = "" Then
            Libreria.genUserMsgBox(Me, "Attenzione: specificare un intervallo di date di uscita.")
        ElseIf txtRientroDa.Text <> "" Or txtRientroA.Text <> "" Then
            Libreria.genUserMsgBox(Me, "Attenzione: per generare le fatture è necessario specificare solamente un intervallo di date di uscita.")
        Else

            If txtDataFattura.Text.Trim <> "" Then
                Dim test As String
                Try
                    test = funzioni_comuni.getDataDb_senza_orario2(txtDataFattura.Text)
                Catch ex As Exception
                    test = ""
                End Try

                If test = "" Then
                    Libreria.genUserMsgBox(Me, "Specificare correttamente la data di fatturazione")
                Else
                    fatturazione_nolo.genera_fatture_ra(txtUscitaDa.Text, txtUscitaA.Text, txtDataFattura.Text)

                    Libreria.genUserMsgBox(Me, "Operazione completata.")
                End If
            Else
                fatturazione_nolo.genera_fatture_ra(txtUscitaDa.Text, txtUscitaA.Text, "")

                Libreria.genUserMsgBox(Me, "Operazione completata.")
            End If



        End If
    End Sub
End Class
