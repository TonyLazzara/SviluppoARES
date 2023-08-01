Public Class Costanti
    Public Enum costanti_web
        id_a_carico_di = 2
        id_a_carico_utente = 5
        id_elemento_escluso = 98
        id_elemento_escluso2 = 200
        id_metodo_stampa = 2
        id_metodo_stampa_franchigie = 3
        id_operatore_web = 7
        id_tariffa = 8
    End Enum

    Public Enum stato_contratto
        non_salvato = 0
        check_out = 1
        aperto = 2
        quick = 3
        da_incassare = 4
        crv_attesa_sostituzione = 5
        fatturato = 6
        void = 7
        da_fatturare = 8
    End Enum

    Public Enum stato_gps
        in_parco = 1
        in_nolo = 2
        furto = 3
        malfunzionamento = 4
        vendita = 5
        smarrimento = 6
        dismesso = 7
    End Enum

    Public Enum tipologia_movimenti
        immatricolazione = 1
        immissione_in_parco = 2
        noleggio = 3
        fermo_tecnico = 4
        rifornimento = 7
        furto = 8
        movimento_interno = 9
        riparazione = 10
        bisarca = 11
        lavaggio = 12
        dismissione = 13
    End Enum

    Public Shared ReadOnly Property POS_TipoEthernet() As Integer
        Get
            Return 1
        End Get
    End Property


    Public Shared ReadOnly Property EMail_UfficioMonetica() As String
        Get
            Return "massimo.cappello@sbc.it"
        End Get
    End Property

    Public Shared ReadOnly Property codice_cash() As Integer
        Get
            Return 9999
        End Get
    End Property

    'COSTANTI PER PREVENTIVI/PRENOTAZIONI/CONTRATTI

    'ID DI condizioni_elementi PER IL SECONDO GUIDATORE
    Public Shared ReadOnly Property Id_Secondo_Guidatore() As String
        Get
            Return "86"
        End Get
    End Property

    'ID DELL'ELEMENTO TEMPO+KM (O VALORE TARIFFA) DELLA TABELLA condizioni_elementi
    Public Shared ReadOnly Property ID_tempo_km() As String
        Get
            Return "98"
        End Get
    End Property

    'COSTANTE PER ID NAZIONE ITALIA DA TABELLA NAZIONI
    Public Shared ReadOnly Property ID_Italia() As String
        Get
            Return "16"
        End Get
    End Property

    'TESTO DELLA RIGA TOTALE
    Public Shared ReadOnly Property testo_elemento_totale() As String
        Get
            Return "TOTALE"
        End Get
    End Property

    'ID TIPOLOGIA DI PAGAMENTO DA TABELLA P1000 "TIP_PAG" PER RICHIESTRA PREAUTORIZZAZIONE POS
    Public Shared ReadOnly Property id_richiesta_preautorizzazione_pos_p1000() As String
        Get
            Return "-438610305"
        End Get
    End Property

    'ID TIPOLOGIA DI PAGAMENTO DA TABELLA P1000 "TIP_PAG" PER RIMBORSO POS
    Public Shared ReadOnly Property id_rimborso_pos_p1000() As String
        Get
            Return "-722881389"
        End Get
    End Property

    'ID TIPOLOGIA DI PAGAMENTO DA TABELLA P1000 "TIP_PAG" PER VENDITA POS
    Public Shared ReadOnly Property id_incasso_pos_p1000() As String
        Get
            Return "1011098650"
        End Get
    End Property

    'ID TIPOLOGIA DI PAGAMENTO DA TABELLA P1000 "TIP_PAG" PER INTEGRAZIONE PREAUTORIZZAZIONE
    Public Shared ReadOnly Property id_integrazione_preautorizzazione_pos_p1000() As String
        Get
            Return "-438610303"
        End Get
    End Property

    'ID TIPOLOGIA DI PAGAMENTO DA TABELLA P1000 "TIP_PAG" PER CHIUSURA PREAUTORIZZAZIONE
    Public Shared ReadOnly Property id_chiusura_preautorizzazione_pos_p1000() As String
        Get
            Return "-438610304"
        End Get
    End Property

    'ID TIPOLOGIA DI PAGAMENTO DA TABELLA P1000 "TIP_PAG" PER STORNO PREAUTORIZZAZIONE
    Public Shared ReadOnly Property id_STORNO_preautorizzazione_pos_p1000() As String
        Get
            Return "-438610301"
        End Get
    End Property

    'ID TIPOLOGIA DI PAGAMENTO DA TABELLA P1000 "TIP_PAG" PER STORNO INTEGRAZIONE
    Public Shared ReadOnly Property id_STORNO_integrazione_pos_p1000() As String
        Get
            Return "-438610300"
        End Get
    End Property

    'ID TIPOLOGIA DI PAGAMENTO DA TABELLA P1000 "TIP_PAG" PER STORNO CHIUSURA
    Public Shared ReadOnly Property id_STORNO_chiusura_pos_p1000() As String
        Get
            Return "-438610299"
        End Get
    End Property

    'ID TIPOLOGIA DI PAGAMENTO DA TABELLA P1000 "TIP_PAG" PER STORNO VENDITA
    Public Shared ReadOnly Property id_STORNO_vendita_pos_p1000() As String
        Get
            Return "-438610298"
        End Get
    End Property

    'ID TIPOLOGIA DI PAGAMENTO DA TABELLA P1000 "TIP_PAG" PER STORNO RIMBORSO
    Public Shared ReadOnly Property id_STORNO_rimborso_pos_p1000() As String
        Get
            Return ""
        End Get
    End Property

    'ID TIPOLOGIA DI PAGAMENTO DA TABELLA P1000 "TIP_PAG" PER PAGAMENTO WEB
    Public Shared ReadOnly Property id_pagamento_web_p1000() As String
        Get
            Return "-2005199230"
        End Get
    End Property

    'ID DELL'ELEMENTO INCLUSO IN condizioni_elementi_a_carico_di
    Public Shared ReadOnly Property id_accessorio_incluso() As String
        Get
            Return "5"
        End Get
    End Property

    'UNITA' DI MISURA X CONDIZIONI: GIORNI
    Public Shared ReadOnly Property id_unita_misura_giorni() As String
        Get
            Return "9"
        End Get
    End Property

    'UNITA' DI MISURA X CONDIZIONI: km tra stazioni
    Public Shared ReadOnly Property id_unita_misura_km_tra_stazioni() As String
        Get
            Return "16"
        End Get
    End Property

    'ID DELL'ELEMENTO stampa informativa con valore DALLA TABELLA condizioni_metodo_stampa
    Public Shared ReadOnly Property id_stampa_informativa_con_valore() As String
        Get
            Return "3"
        End Get
    End Property

    'ID DELL'ELEMENTO stampa informativa senza valore DALLA TABELLA condizioni_metodo_stampa
    Public Shared ReadOnly Property id_stampa_informativa_senza_valore() As String
        Get
            Return "4"
        End Get
    End Property

    'ID DELL'ELEMENTO valorizza nel contratto DALLA TABELLA condizioni_metodo_stampa
    Public Shared ReadOnly Property id_valorizza_nel_contratto() As String
        Get
            Return "2"
        End Get
    End Property

    'ID DELL'ELEMENTO non stampare (nel contratto) DALLA TABELLA condizioni_metodo_stampa
    Public Shared ReadOnly Property id_non_stampare() As String
        Get
            Return "1"
        End Get
    End Property

    'ID DELL'ELEMENTO "CLIENTE" DELLA TABELLA condizioni_a_carico_di
    Public Shared ReadOnly Property id_a_carico_del_cliente() As String
        Get
            Return "2"
        End Get
    End Property

    Public Shared ReadOnly Property CodiceIvaWeb() As String
        Get
            Return "021"
        End Get
    End Property

    'VALORI IVA DI DEFAULT - IL PRIMO UTILIZZATO NEI RIBALTAMENTI IL SECONDO IN ARES QUANDO L'IVA NON E' SCELTA DALL'UTENTE DAL MENU A TENDINA
    Public Shared ReadOnly Property ValoreIvaWeb() As Integer
        Get
            Return 21
        End Get
    End Property

    Public Shared ReadOnly Property id_iva_default() As Integer
        Get
            Return 1
        End Get
    End Property

    Public Shared ReadOnly Property iva_default() As Integer
        Get
            Return 22
        End Get
    End Property

    Public Shared ReadOnly Property IdModalitaPagamentoWeb() As Integer
        ' dalla tabella [SicilyByCar].[dbo].[MOD_PAG] la riga: 'POS da CLIENTI INTERNET'
        Get
            Return -88607897
        End Get
    End Property

    Public Shared ReadOnly Property IdTipoPagamentoWeb() As Integer
        ' dalla tabella [SicilyByCar].[dbo].[TIP_PAG] ... 
        ' sembrerebbe invece che il valore sia dalla tabella [TERMINE_DI_PAGAMENTO] 
        Get
            Return 8 ' = Rimessa Diretta che coincide con [CODICE_CONTABILE] = "RD0" nell'export verso la contabilità
        End Get
    End Property
    '--------------------------------------------------------------------------------------------------------------------------------------
    Public Shared ReadOnly Property IdImmatricolazione() As Integer
        'ID DELLA TABELLA tipologia_movimenti INERENTE ALL'IMMATRICOLAZIONE DI UN VEICOLO
        Get
            Return tipologia_movimenti.immatricolazione
        End Get
    End Property

    Public Shared ReadOnly Property IdImmissioneInParco() As Integer
        'ID DELLA TABELLA tipologia_movimenti INERENTE ALL'IMMISSIONE IN PARCO DI UN VEICOLO (MOMENTO IN CUI IL VEICOLO RISULTA
        'NOLEGGIABILE)
        Get
            Return tipologia_movimenti.immissione_in_parco
        End Get
    End Property

    Public Shared ReadOnly Property idMovimentoNoleggio() As Integer
        'ID DELLA TABELLA tipologia_movimenti INERENTE AL MOVIMENTO DI NOLEGGIO
        Get
            Return tipologia_movimenti.noleggio
        End Get
    End Property

    Public Shared ReadOnly Property idFermoTecnico() As Integer
        'ID DELLA TABELLA tipologia_movimenti INERENTE AL FERMO TECNICO
        Get
            Return tipologia_movimenti.fermo_tecnico
        End Get
    End Property

    Public Shared ReadOnly Property idMovimentoRifornimento() As Integer
        'ID DELLA TABELLA tipologia_movimenti INERENTE AL MOVIMENTO DI RIFORNIMENTO
        Get
            Return tipologia_movimenti.rifornimento
        End Get
    End Property

    Public Shared ReadOnly Property idMovimentoFurto() As Integer
        'ID DELLA TABELLA tipologia_movimenti INERENTE AL MOVIMENTO DI RIFORNIMENTO
        Get
            Return tipologia_movimenti.furto
        End Get
    End Property

    Public Shared ReadOnly Property idMovimentoInterno() As Integer
        'ID DELLA TABELLA tipologia_movimenti INERENTE AL MOVIMENTO DI RIFORNIMENTO
        Get
            Return tipologia_movimenti.movimento_interno
        End Get
    End Property

    Public Shared ReadOnly Property idLavaggio() As Integer
        'ID DELLA TABELLA tipologia_movimenti INERENTE AL MOVIMENTO DI RIFORNIMENTO
        Get
            Return tipologia_movimenti.lavaggio
        End Get
    End Property

    Public Shared ReadOnly Property idBisarca() As Integer
        'ID DELLA TABELLA tipologia_movimenti INERENTE AL MOVIMENTO DI BISARCA
        Get
            Return tipologia_movimenti.bisarca
        End Get
    End Property

    Public Shared ReadOnly Property idMovimentoODL() As Integer
        'ID DELLA TABELLA tipologia_movimenti INERENTE AL MOVIMENTO ODL (=Riparazione)
        Get
            Return tipologia_movimenti.riparazione
        End Get
    End Property
    Public Shared ReadOnly Property idMovimentoLavaggio() As Integer
        'ID DELLA TABELLA tipologia_movimenti INERENTE AL MOVIMENTO DI Lavaggio
        Get
            Return tipologia_movimenti.lavaggio
        End Get
    End Property
    '--------------------------------------------------------------------------------------------------------------------------------------

    'TABELLA trasferimenti_status PER LA FUNZIONALITA' trasferimenti veicoli --------------------------------------------------------------
    Public Shared ReadOnly Property id_trasferimento_richiesta() As Integer
        Get
            Return 1
        End Get
    End Property
    Public Shared ReadOnly Property id_trasferimento_negato() As Integer
        Get
            Return 2
        End Get
    End Property
    Public Shared ReadOnly Property id_trasferimento_accettato() As Integer
        Get
            Return 3
        End Get
    End Property
    Public Shared ReadOnly Property id_trasferimento_in_corso() As Integer
        Get
            Return 4
        End Get
    End Property
    Public Shared ReadOnly Property id_trasferimento_eseguito() As Integer
        Get
            Return 5
        End Get
    End Property
    Public Shared ReadOnly Property id_trasferimento_annullato() As Integer
        Get
            Return 6
        End Get
    End Property
    '--------------------------------------------------------------------------------------------------------------------------------------

    'TABELLA STATUS CONTRATTI ------------------------------------------------------------------------------------------------------------
    Public Shared ReadOnly Property id_contratto_non_salvato() As Integer
        Get
            Return 0
        End Get
    End Property
    Public Shared ReadOnly Property id_contratto_da_preautorizzare() As Integer
        Get
            Return 1
        End Get
    End Property
    Public Shared ReadOnly Property id_contratto_aperto() As Integer
        Get
            Return 2
        End Get
    End Property
    Public Shared ReadOnly Property id_contratto_quick_check_in() As Integer
        Get
            Return 3
        End Get
    End Property
    Public Shared ReadOnly Property id_contratto_chiuso() As Integer
        Get
            Return 4
        End Get
    End Property
    Public Shared ReadOnly Property id_contratto_crv() As Integer
        Get
            Return 5
        End Get
    End Property
    '-------------------------------------------------------------------------------------------------------------------------------------

    Public Shared ReadOnly Property terminal_id_web() As String
        Get
            Return "WEB_000000011389"
        End Get
    End Property

    Public Shared ReadOnly Property tariffa_Thrifty() As String
        Get
            Return "THRIFTY"
        End Get
    End Property

    Public Shared ReadOnly Property id_clienti_tipologia_dollar() As Integer
        Get
            Return 934
        End Get
    End Property

    Public Shared ReadOnly Property id_clienti_tipologia_thrifty() As Integer
        Get
            Return 934
        End Get
    End Property

    Public Shared ReadOnly Property tariffa_Dollar() As String
        Get
            Return "DOLLAR"
        End Get
    End Property

    Public Shared ReadOnly Property tariffa_55_SPECWON() As String
        Get
            Return "SPECWON"
        End Get
    End Property

    Public Shared ReadOnly Property tariffa_55_SPECWOD() As String
        Get
            Return "SPECWOD"
        End Get
    End Property

    Public Shared ReadOnly Property tariffa_55_SPECWEBA() As String
        Get
            Return "SPECWEBA"
        End Get
    End Property

    Public Shared ReadOnly Property tariffa_55_SPECWEBMENSILE() As String
        Get
            Return "SPECWEBMENSILE"
        End Get
    End Property

    'id della tabella stazioni riguardante la sede
    Public Shared ReadOnly Property id_stazione_sede() As String
        Get
            Return "1"
        End Get
    End Property

    'MINUTI DI TOLLERANZA PER L'ORARIO DI USCITO DEL VEICOLO DA CONTRATTO RISPETTO ALL'ORARIO ATTUALE - DETERMINA ANCHE DI QUANTI MINUTI
    'DEVE ESSERE PRECEDENTE L'ORARIO DI RIENTRO DA MOVIMENTO DI RIFORNIMENTO E DI PULIZIA VEICOLO RISPETTO L'ORARIO PRECEDENTE IN MODO DA
    'NON AVERE SOVRAPPOSIZIONI
    Public Shared ReadOnly Property cnt_minuti_tolleranza_uscita() As Integer
        Get
            Return 10
        End Get
    End Property


    Public Shared ReadOnly Property spese_postali_rds_default() As Double
        Get
            Return 90
        End Get
    End Property

    ' mappa di defualt se i modello non è censito  (veicoli_img_modelli)
    Public Shared ReadOnly Property id_mappa_default() As Integer
        Get
            Return 1
        End Get
    End Property

    ' costanti per il corretto posizionamento delle icone sulla mappa di una vettura 
    Public Const DeltaIconaX = 20
    Public Const DeltaIconaY = 20
End Class
