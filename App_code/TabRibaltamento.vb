Public Class TabRibaltamento
    Inherits ITabellaDB

    Private m_id As Integer
    Public ReadOnly Property id As Integer
        Get
            Return m_id
        End Get
    End Property

    Protected m_blocco_ribaltamento As Nullable(Of Integer)
    Protected m_provenienza_replica As Nullable(Of Integer)
    Protected m_TipoPrenotazione As Nullable(Of Integer)
    Protected m_id_prenotazione_ares As Nullable(Of Integer)
    Protected m_IDPREN_esterno As Nullable(Of Integer)
    Protected m_NUMPREN As Nullable(Of Integer)
    Protected m_CODNUMPREN As String
    Protected m_DATAPREN As Nullable(Of DateTime)
    Protected m_id_gruppo As Nullable(Of Integer)
    Protected m_cod_gruppo As String
    Protected m_id_gruppo_da_consegnare As Nullable(Of Integer)
    Protected m_cod_gruppo_da_consegnare As String
    Protected m_stato As Nullable(Of Integer)
    Protected m_STA_OUT As Nullable(Of Integer)
    Protected m_STA_IN As Nullable(Of Integer)
    Protected m_cod_sta_out As String
    Protected m_cod_sta_in As String
    Protected m_data_out As Nullable(Of DateTime)
    Protected m_data_in As Nullable(Of DateTime)
    Protected m_volo_out As String
    Protected m_volo_in As String
    Protected m_idtariffa As Nullable(Of Integer)
    Protected m_codtar As String
    Protected m_id_tour_operator As Nullable(Of Integer)
    Protected m_sconto As Nullable(Of Double)
    Protected m_totale As Nullable(Of Double)
    Protected m_supplementi As String
    Protected m_impbase As Nullable(Of Double)
    Protected m_id_cliente_web As Nullable(Of Integer)
    Protected m_data_nascita As Nullable(Of Date)
    Protected m_cognome As String
    Protected m_nome As String
    Protected m_indirizzo As String
    Protected m_citta As String
    Protected m_cap As String
    Protected m_provincia As String
    Protected m_nazione As String
    Protected m_email As String
    Protected m_telefono As String
    Protected m_fax As String
    Protected m_cell As String
    Protected m_luogo_nascita As String
    Protected m_CodNazioneNascita As String
    Protected m_codfisc As String
    Protected m_patente_num As String
    Protected m_patente_ril As String
    Protected m_data_pat_rilascio As Nullable(Of Date)
    Protected m_scad_patente As Nullable(Of Date)
    Protected m_flag_azienda As Nullable(Of Boolean)
    Protected m_id_azienda As Nullable(Of Integer)
    Protected m_nome_azienda As String
    Protected m_indirizzo_az As String
    Protected m_citta_az As String
    Protected m_cap_az As String
    Protected m_prov_az As String
    Protected m_tel_az As String
    Protected m_fax_az As String
    Protected m_cell_az As String
    Protected m_email_az As String
    Protected m_piva As String
    Protected m_gruppi_spec As Nullable(Of Boolean)
    Protected m_COD_CONV As String
    Protected m_CCNUMAUT As String
    Protected m_CCDATA As Nullable(Of DateTime)
    Protected m_CCIMPORTO As Nullable(Of Double)
    Protected m_CCNUMOPE As String
    Protected m_CCRISP As String
    Protected m_CCTRANS As String
    Protected m_CCOMPAGNIA As String
    Protected m_TRANSOK As Nullable(Of Boolean)
    Protected m_TERMINAL_ID As String
    Protected m_SCARICATADA As Nullable(Of Integer)
    Protected m_DATA_SCARICO As Nullable(Of DateTime)
    Protected m_NOTE As String
    Protected m_codici_errore As String
    Protected m_str_data_nascita As String
    Protected m_str_data_ril_patente As String
    Protected m_FileImport As String

    Public Property blocco_ribaltamento() As Nullable(Of Integer)
        Get
            Return m_blocco_ribaltamento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_blocco_ribaltamento = value
        End Set
    End Property
    Public Property provenienza_replica() As Nullable(Of Integer)
        Get
            Return m_provenienza_replica
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_provenienza_replica = value
        End Set
    End Property
    Public Property TipoPrenotazione() As Nullable(Of Integer)
        Get
            Return m_TipoPrenotazione
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_TipoPrenotazione = value
        End Set
    End Property
    Public Property id_prenotazione_ares() As Nullable(Of Integer)
        Get
            Return m_id_prenotazione_ares
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id_prenotazione_ares = value
        End Set
    End Property
    Public Property IDPREN_esterno() As Nullable(Of Integer)
        Get
            Return m_IDPREN_esterno
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_IDPREN_esterno = value
        End Set
    End Property
    Public Property CODNUMPREN() As String
        Get
            Return m_CODNUMPREN
        End Get
        Set(ByVal value As String)
            m_CODNUMPREN = value
        End Set
    End Property
    Public Property NUMPREN() As Nullable(Of Integer)
        Get
            Return m_NUMPREN
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_NUMPREN = value
        End Set
    End Property
    Public Property DATAPREN() As Nullable(Of DateTime)
        Get
            Return m_DATAPREN
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            m_DATAPREN = value
        End Set
    End Property
    Public Property id_gruppo() As Nullable(Of Integer)
        Get
            Return m_id_gruppo
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id_gruppo = value
        End Set
    End Property
    Public Property cod_gruppo() As String
        Get
            Return m_cod_gruppo
        End Get
        Set(ByVal value As String)
            m_cod_gruppo = value
        End Set
    End Property
    Public Property id_gruppo_da_consegnare() As Nullable(Of Integer)
        Get
            Return m_id_gruppo_da_consegnare
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id_gruppo_da_consegnare = value
        End Set
    End Property
    Public Property cod_gruppo_da_consegnare() As String
        Get
            Return m_cod_gruppo_da_consegnare
        End Get
        Set(ByVal value As String)
            m_cod_gruppo_da_consegnare = value
        End Set
    End Property
    Public Property stato() As Nullable(Of Integer)
        Get
            Return m_stato
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_stato = value
        End Set
    End Property
    Public Property STA_OUT() As Nullable(Of Integer)
        Get
            Return m_STA_OUT
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_STA_OUT = value
        End Set
    End Property
    Public Property STA_IN() As Nullable(Of Integer)
        Get
            Return m_STA_IN
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_STA_IN = value
        End Set
    End Property
    Public Property cod_sta_out() As String
        Get
            Return m_cod_sta_out
        End Get
        Set(ByVal value As String)
            m_cod_sta_out = value
        End Set
    End Property
    Public Property cod_sta_in() As String
        Get
            Return m_cod_sta_in
        End Get
        Set(ByVal value As String)
            m_cod_sta_in = value
        End Set
    End Property
    Public Property data_out() As Nullable(Of DateTime)
        Get
            Return m_data_out
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            m_data_out = value
        End Set
    End Property
    Public Property data_in() As Nullable(Of DateTime)
        Get
            Return m_data_in
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            m_data_in = value
        End Set
    End Property
    Public Property volo_out() As String
        Get
            Return m_volo_out
        End Get
        Set(ByVal value As String)
            m_volo_out = value
        End Set
    End Property
    Public Property volo_in() As String
        Get
            Return m_volo_in
        End Get
        Set(ByVal value As String)
            m_volo_in = value
        End Set
    End Property
    Public Property idtariffa() As Nullable(Of Integer)
        Get
            Return m_idtariffa
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_idtariffa = value
        End Set
    End Property
    Public Property codtar() As String
        Get
            Return m_codtar
        End Get
        Set(ByVal value As String)
            m_codtar = value
        End Set
    End Property
    Public Property id_tour_operator() As Nullable(Of Integer)
        Get
            Return m_id_tour_operator
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id_tour_operator = value
        End Set
    End Property
    Public Property sconto() As Nullable(Of Double)
        Get
            Return m_sconto
        End Get
        Set(ByVal value As Nullable(Of Double))
            m_sconto = value
        End Set
    End Property
    Public Property totale() As Nullable(Of Double)
        Get
            Return m_totale
        End Get
        Set(ByVal value As Nullable(Of Double))
            m_totale = value
        End Set
    End Property
    Public Property supplementi() As String
        Get
            Return m_supplementi
        End Get
        Set(ByVal value As String)
            m_supplementi = value
        End Set
    End Property
    Public Property impbase() As Nullable(Of Double)
        Get
            Return m_impbase
        End Get
        Set(ByVal value As Nullable(Of Double))
            m_impbase = value
        End Set
    End Property
    Public Property id_cliente_web() As Nullable(Of Integer)
        Get
            Return m_id_cliente_web
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id_cliente_web = value
        End Set
    End Property
    Public Property data_nascita() As Nullable(Of Date)
        Get
            Return m_data_nascita
        End Get
        Set(ByVal value As Nullable(Of Date))
            m_data_nascita = value
        End Set
    End Property
    Public Property cognome() As String
        Get
            Return m_cognome
        End Get
        Set(ByVal value As String)
            m_cognome = value
        End Set
    End Property
    Public Property nome() As String
        Get
            Return m_nome
        End Get
        Set(ByVal value As String)
            m_nome = value
        End Set
    End Property
    Public Property indirizzo() As String
        Get
            Return m_indirizzo
        End Get
        Set(ByVal value As String)
            m_indirizzo = value
        End Set
    End Property
    Public Property citta() As String
        Get
            Return m_citta
        End Get
        Set(ByVal value As String)
            m_citta = value
        End Set
    End Property
    Public Property cap() As String
        Get
            Return m_cap
        End Get
        Set(ByVal value As String)
            m_cap = value
        End Set
    End Property
    Public Property provincia() As String
        Get
            Return m_provincia
        End Get
        Set(ByVal value As String)
            m_provincia = value
        End Set
    End Property
    Public Property nazione() As String
        Get
            Return m_nazione
        End Get
        Set(ByVal value As String)
            m_nazione = value
        End Set
    End Property
    Public Property email() As String
        Get
            Return m_email
        End Get
        Set(ByVal value As String)
            m_email = value
        End Set
    End Property
    Public Property telefono() As String
        Get
            Return m_telefono
        End Get
        Set(ByVal value As String)
            m_telefono = value
        End Set
    End Property
    Public Property fax() As String
        Get
            Return m_fax
        End Get
        Set(ByVal value As String)
            m_fax = value
        End Set
    End Property
    Public Property cell() As String
        Get
            Return m_cell
        End Get
        Set(ByVal value As String)
            m_cell = value
        End Set
    End Property
    Public Property luogo_nascita() As String
        Get
            Return m_luogo_nascita
        End Get
        Set(ByVal value As String)
            m_luogo_nascita = value
        End Set
    End Property
    Public Property CodNazioneNascita() As String
        Get
            Return m_CodNazioneNascita
        End Get
        Set(ByVal value As String)
            m_CodNazioneNascita = value
        End Set
    End Property
    Public Property codfisc() As String
        Get
            Return m_codfisc
        End Get
        Set(ByVal value As String)
            m_codfisc = value
        End Set
    End Property
    Public Property patente_num() As String
        Get
            Return m_patente_num
        End Get
        Set(ByVal value As String)
            m_patente_num = value
        End Set
    End Property
    Public Property patente_ril() As String
        Get
            Return m_patente_ril
        End Get
        Set(ByVal value As String)
            m_patente_ril = value
        End Set
    End Property
    Public Property data_pat_rilascio() As Nullable(Of Date)
        Get
            Return m_data_pat_rilascio
        End Get
        Set(ByVal value As Nullable(Of Date))
            m_data_pat_rilascio = value
        End Set
    End Property
    Public Property scad_patente() As Nullable(Of Date)
        Get
            Return m_scad_patente
        End Get
        Set(ByVal value As Nullable(Of Date))
            m_scad_patente = value
        End Set
    End Property
    Public Property flag_azienda() As Nullable(Of Boolean)
        Get
            Return m_flag_azienda
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_flag_azienda = value
        End Set
    End Property
    Public Property id_azienda() As Nullable(Of Integer)
        Get
            Return m_id_azienda
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id_azienda = value
        End Set
    End Property
    Public Property nome_azienda() As String
        Get
            Return m_nome_azienda
        End Get
        Set(ByVal value As String)
            m_nome_azienda = value
        End Set
    End Property
    Public Property indirizzo_az() As String
        Get
            Return m_indirizzo_az
        End Get
        Set(ByVal value As String)
            m_indirizzo_az = value
        End Set
    End Property
    Public Property citta_az() As String
        Get
            Return m_citta_az
        End Get
        Set(ByVal value As String)
            m_citta_az = value
        End Set
    End Property
    Public Property cap_az() As String
        Get
            Return m_cap_az
        End Get
        Set(ByVal value As String)
            m_cap_az = value
        End Set
    End Property
    Public Property prov_az() As String
        Get
            Return m_prov_az
        End Get
        Set(ByVal value As String)
            m_prov_az = value
        End Set
    End Property
    Public Property tel_az() As String
        Get
            Return m_tel_az
        End Get
        Set(ByVal value As String)
            m_tel_az = value
        End Set
    End Property
    Public Property fax_az() As String
        Get
            Return m_fax_az
        End Get
        Set(ByVal value As String)
            m_fax_az = value
        End Set
    End Property
    Public Property cell_az() As String
        Get
            Return m_cell_az
        End Get
        Set(ByVal value As String)
            m_cell_az = value
        End Set
    End Property
    Public Property email_az() As String
        Get
            Return m_email_az
        End Get
        Set(ByVal value As String)
            m_email_az = value
        End Set
    End Property
    Public Property piva() As String
        Get
            Return m_piva
        End Get
        Set(ByVal value As String)
            m_piva = value
        End Set
    End Property
    Public Property gruppi_spec() As Nullable(Of Boolean)
        Get
            Return m_gruppi_spec
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_gruppi_spec = value
        End Set
    End Property
    Public Property COD_CONV() As String
        Get
            Return m_COD_CONV
        End Get
        Set(ByVal value As String)
            m_COD_CONV = value
        End Set
    End Property
    Public Property CCNUMAUT() As String
        Get
            Return m_CCNUMAUT
        End Get
        Set(ByVal value As String)
            m_CCNUMAUT = value
        End Set
    End Property
    Public Property CCDATA() As Nullable(Of DateTime)
        Get
            Return m_CCDATA
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            m_CCDATA = value
        End Set
    End Property
    Public Property CCIMPORTO() As Nullable(Of Double)
        Get
            Return m_CCIMPORTO
        End Get
        Set(ByVal value As Nullable(Of Double))
            m_CCIMPORTO = value
        End Set
    End Property
    Public Property CCNUMOPE() As String
        Get
            Return m_CCNUMOPE
        End Get
        Set(ByVal value As String)
            m_CCNUMOPE = value
        End Set
    End Property
    Public Property CCRISP() As String
        Get
            Return m_CCRISP
        End Get
        Set(ByVal value As String)
            m_CCRISP = value
        End Set
    End Property
    Public Property CCTRANS() As String
        Get
            Return m_CCTRANS
        End Get
        Set(ByVal value As String)
            m_CCTRANS = value
        End Set
    End Property
    Public Property CCOMPAGNIA() As String
        Get
            Return m_CCOMPAGNIA
        End Get
        Set(ByVal value As String)
            m_CCOMPAGNIA = value
        End Set
    End Property
    Public Property TRANSOK() As Nullable(Of Boolean)
        Get
            Return m_TRANSOK
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_TRANSOK = value
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
    Public Property SCARICATADA() As Nullable(Of Integer)
        Get
            Return m_SCARICATADA
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_SCARICATADA = value
        End Set
    End Property
    Public Property DATA_SCARICO() As Nullable(Of DateTime)
        Get
            Return m_DATA_SCARICO
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            m_DATA_SCARICO = value
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
    Public Property codici_errore() As String
        Get
            Return m_codici_errore
        End Get
        Set(ByVal value As String)
            m_codici_errore = value
        End Set
    End Property
    Public Property str_data_nascita() As String
        Get
            Return m_str_data_nascita
        End Get
        Set(ByVal value As String)
            m_str_data_nascita = value
        End Set
    End Property
    Public Property str_data_ril_patente() As String
        Get
            Return m_str_data_ril_patente
        End Get
        Set(ByVal value As String)
            m_str_data_ril_patente = value
        End Set
    End Property
    Public Property FileImport() As String
        Get
            Return m_FileImport
        End Get
        Set(ByVal value As String)
            m_FileImport = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional RecuperaId As Boolean = False) As Integer
        SalvaRecord = -1

        Dim sqlStr As String = "INSERT INTO ribaltamento (blocco_ribaltamento,provenienza_replica,TipoPrenotazione,id_prenotazione_ares,IDPREN_esterno,CODNUMPREN,NUMPREN,DATAPREN,id_gruppo,cod_gruppo,id_gruppo_da_consegnare,cod_gruppo_da_consegnare,stato,STA_OUT,STA_IN,cod_sta_out,cod_sta_in,data_out,data_in,volo_out,volo_in,idtariffa,codtar,id_tour_operator,sconto,totale,supplementi,impbase,id_cliente_web,data_nascita,cognome,nome,indirizzo,citta,cap,provincia,nazione,email,telefono,fax,cell,luogo_nascita,CodNazioneNascita,codfisc,patente_num,patente_ril,data_pat_rilascio,scad_patente,flag_azienda,id_azienda,nome_azienda,indirizzo_az,citta_az,cap_az,prov_az,tel_az,fax_az,cell_az,email_az,piva,gruppi_spec,COD_CONV,CCNUMAUT,CCDATA,CCIMPORTO,CCNUMOPE,CCRISP,CCTRANS,CCOMPAGNIA,TRANSOK,TERMINAL_ID,SCARICATADA,DATA_SCARICO,NOTE,codici_errore,str_data_nascita,str_data_ril_patente,FileImport)" & _
            " VALUES (@blocco_ribaltamento,@provenienza_replica,@TipoPrenotazione,@id_prenotazione_ares,@IDPREN_esterno,@CODNUMPREN,@NUMPREN,@DATAPREN,@id_gruppo,@cod_gruppo,@id_gruppo_da_consegnare,@cod_gruppo_da_consegnare,@stato,@STA_OUT,@STA_IN,@cod_sta_out,@cod_sta_in,@data_out,@data_in,@volo_out,@volo_in,@idtariffa,@codtar,@id_tour_operator,@sconto,@totale,@supplementi,@impbase,@id_cliente_web,@data_nascita,@cognome,@nome,@indirizzo,@citta,@cap,@provincia,@nazione,@email,@telefono,@fax,@cell,@luogo_nascita,@CodNazioneNascita,@codfisc,@patente_num,@patente_ril,@data_pat_rilascio,@scad_patente,@flag_azienda,@id_azienda,@nome_azienda,@indirizzo_az,@citta_az,@cap_az,@prov_az,@tel_az,@fax_az,@cell_az,@email_az,@piva,@gruppi_spec,@COD_CONV,@CCNUMAUT,@CCDATA,@CCIMPORTO,@CCNUMOPE,@CCRISP,@CCTRANS,@CCOMPAGNIA,@TRANSOK,@TERMINAL_ID,@SCARICATADA,@DATA_SCARICO,@NOTE,@codici_errore,@str_data_nascita,@str_data_ril_patente,@FileImport)"
        HttpContext.Current.Trace.Write("SalvaRibaltamento: " & sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@blocco_ribaltamento", System.Data.SqlDbType.Int, blocco_ribaltamento)
                addParametro(Cmd, "@provenienza_replica", System.Data.SqlDbType.Int, provenienza_replica)
                addParametro(Cmd, "@TipoPrenotazione", System.Data.SqlDbType.Int, TipoPrenotazione)
                addParametro(Cmd, "@id_prenotazione_ares", System.Data.SqlDbType.Int, id_prenotazione_ares)
                addParametro(Cmd, "@IDPREN_esterno", System.Data.SqlDbType.Int, IDPREN_esterno)
                addParametro(Cmd, "@CODNUMPREN", System.Data.SqlDbType.NChar, CODNUMPREN)
                addParametro(Cmd, "@NUMPREN", System.Data.SqlDbType.Int, SubstringSicuroNothing(NUMPREN, 10))
                addParametro(Cmd, "@DATAPREN", System.Data.SqlDbType.DateTime, DATAPREN)
                addParametro(Cmd, "@id_gruppo", System.Data.SqlDbType.Int, id_gruppo)
                addParametro(Cmd, "@cod_gruppo", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cod_gruppo, 3))
                addParametro(Cmd, "@id_gruppo_da_consegnare", System.Data.SqlDbType.Int, id_gruppo_da_consegnare)
                addParametro(Cmd, "@cod_gruppo_da_consegnare", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cod_gruppo_da_consegnare, 3))
                addParametro(Cmd, "@stato", System.Data.SqlDbType.Int, stato)
                addParametro(Cmd, "@STA_OUT", System.Data.SqlDbType.Int, STA_OUT)
                addParametro(Cmd, "@STA_IN", System.Data.SqlDbType.Int, STA_IN)
                addParametro(Cmd, "@cod_sta_out", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cod_sta_out, 20))
                addParametro(Cmd, "@cod_sta_in", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cod_sta_in, 20))
                addParametro(Cmd, "@data_out", System.Data.SqlDbType.DateTime, data_out)
                addParametro(Cmd, "@data_in", System.Data.SqlDbType.DateTime, data_in)
                addParametro(Cmd, "@volo_out", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(volo_out, 100))
                addParametro(Cmd, "@volo_in", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(volo_in, 100))
                addParametro(Cmd, "@idtariffa", System.Data.SqlDbType.Int, idtariffa)
                addParametro(Cmd, "@codtar", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(codtar, 50))
                addParametro(Cmd, "@id_tour_operator", System.Data.SqlDbType.Int, id_tour_operator)
                addParametro(Cmd, "@sconto", System.Data.SqlDbType.Float, sconto)
                addParametro(Cmd, "@totale", System.Data.SqlDbType.Float, totale)
                addParametro(Cmd, "@supplementi", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(supplementi, 50))
                addParametro(Cmd, "@impbase", System.Data.SqlDbType.Float, impbase)
                addParametro(Cmd, "@id_cliente_web", System.Data.SqlDbType.Int, id_cliente_web)
                addParametro(Cmd, "@data_nascita", System.Data.SqlDbType.Date, data_nascita)
                addParametro(Cmd, "@cognome", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cognome, 50))
                addParametro(Cmd, "@nome", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(nome, 50))
                addParametro(Cmd, "@indirizzo", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(indirizzo, 100))
                addParametro(Cmd, "@citta", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(citta, 50))
                addParametro(Cmd, "@cap", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cap, 20))
                addParametro(Cmd, "@provincia", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(provincia, 50))
                addParametro(Cmd, "@nazione", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(nazione, 50))
                addParametro(Cmd, "@email", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(email, 50))
                addParametro(Cmd, "@telefono", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(telefono, 20))
                addParametro(Cmd, "@fax", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(fax, 50))
                addParametro(Cmd, "@cell", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cell, 50))
                addParametro(Cmd, "@luogo_nascita", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(luogo_nascita, 50))
                addParametro(Cmd, "@CodNazioneNascita", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(CodNazioneNascita, 3))
                addParametro(Cmd, "@codfisc", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(codfisc, 20))
                addParametro(Cmd, "@patente_num", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(patente_num, 20))
                addParametro(Cmd, "@patente_ril", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(patente_ril, 50))
                addParametro(Cmd, "@data_pat_rilascio", System.Data.SqlDbType.Date, data_pat_rilascio)
                addParametro(Cmd, "@scad_patente", System.Data.SqlDbType.Date, scad_patente)
                addParametro(Cmd, "@flag_azienda", System.Data.SqlDbType.Bit, flag_azienda)
                addParametro(Cmd, "@id_azienda", System.Data.SqlDbType.Int, id_azienda)
                addParametro(Cmd, "@nome_azienda", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(nome_azienda, 100))
                addParametro(Cmd, "@indirizzo_az", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(indirizzo_az, 100))
                addParametro(Cmd, "@citta_az", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(citta_az, 100))
                addParametro(Cmd, "@cap_az", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cap_az, 50))
                addParametro(Cmd, "@prov_az", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(prov_az, 50))
                addParametro(Cmd, "@tel_az", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(tel_az, 50))
                addParametro(Cmd, "@fax_az", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(fax_az, 50))
                addParametro(Cmd, "@cell_az", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cell_az, 50))
                addParametro(Cmd, "@email_az", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(email_az, 100))
                addParametro(Cmd, "@piva", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(piva, 50))
                addParametro(Cmd, "@gruppi_spec", System.Data.SqlDbType.Bit, gruppi_spec)
                addParametro(Cmd, "@COD_CONV", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(COD_CONV, 30))
                addParametro(Cmd, "@CCNUMAUT", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(CCNUMAUT, 50))
                addParametro(Cmd, "@CCDATA", System.Data.SqlDbType.DateTime, CCDATA)
                addParametro(Cmd, "@CCIMPORTO", System.Data.SqlDbType.Decimal, CCIMPORTO)
                addParametro(Cmd, "@CCNUMOPE", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(CCNUMOPE, 50))
                addParametro(Cmd, "@CCRISP", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(CCRISP, 50))
                addParametro(Cmd, "@CCTRANS", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(CCTRANS, 50))
                addParametro(Cmd, "@CCOMPAGNIA", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(CCOMPAGNIA, 50))
                addParametro(Cmd, "@TRANSOK", System.Data.SqlDbType.Bit, TRANSOK)
                addParametro(Cmd, "@TERMINAL_ID", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(TERMINAL_ID, 50))
                addParametro(Cmd, "@SCARICATADA", System.Data.SqlDbType.Int, SCARICATADA)
                addParametro(Cmd, "@DATA_SCARICO", System.Data.SqlDbType.DateTime, DATA_SCARICO)
                addParametro(Cmd, "@NOTE", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(NOTE, 255))
                addParametro(Cmd, "@codici_errore", System.Data.SqlDbType.VarChar, SubstringSicuroNothing(codici_errore, 50))
                addParametro(Cmd, "@str_data_nascita", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(str_data_nascita, 50))
                addParametro(Cmd, "@str_data_ril_patente", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(str_data_ril_patente, 50))
                addParametro(Cmd, "@FileImport", System.Data.SqlDbType.VarChar, SubstringSicuroNothing(FileImport, 20))

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            If RecuperaId Then
                sqlStr = "SELECT @@IDENTITY FROM ribaltamento"
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    m_id = Cmd.ExecuteScalar
                End Using
            End If
        End Using

        Return id
    End Function


    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As TabRibaltamento
        Dim mio_record As TabRibaltamento = New TabRibaltamento
        With mio_record
            .provenienza_replica = getValueOrNohing(Rs("provenienza_replica"))
            .TipoPrenotazione = getValueOrNohing(Rs("TipoPrenotazione"))
            .id_prenotazione_ares = getValueOrNohing(Rs("id_prenotazione_ares"))
            .IDPREN_esterno = getValueOrNohing(Rs("IDPREN_esterno"))
            .CODNUMPREN = getValueOrNohing(Rs("CODNUMPREN"))
            .NUMPREN = getValueOrNohing(Rs("NUMPREN"))
            .DATAPREN = getValueOrNohing(Rs("DATAPREN"))
            .id_gruppo = getValueOrNohing(Rs("id_gruppo"))
            .cod_gruppo = getValueOrNohing(Rs("cod_gruppo"))
            .id_gruppo_da_consegnare = getValueOrNohing(Rs("id_gruppo_da_consegnare"))
            .cod_gruppo_da_consegnare = getValueOrNohing(Rs("cod_gruppo_da_consegnare"))
            .stato = getValueOrNohing(Rs("stato"))
            .STA_OUT = getValueOrNohing(Rs("STA_OUT"))
            .STA_IN = getValueOrNohing(Rs("STA_IN"))
            .cod_sta_out = getValueOrNohing(Rs("cod_sta_out"))
            .cod_sta_in = getValueOrNohing(Rs("cod_sta_in"))
            .data_out = getValueOrNohing(Rs("data_out"))
            .data_in = getValueOrNohing(Rs("data_in"))
            .volo_out = getValueOrNohing(Rs("volo_out"))
            .volo_in = getValueOrNohing(Rs("volo_in"))
            .idtariffa = getValueOrNohing(Rs("idtariffa"))
            .codtar = getValueOrNohing(Rs("codtar"))
            .id_tour_operator = getValueOrNohing(Rs("id_tour_operator"))
            .sconto = getDoubleOrNohing(Rs("sconto"))
            .totale = getDoubleOrNohing(Rs("totale"))
            .supplementi = getValueOrNohing(Rs("supplementi"))
            .impbase = getDoubleOrNohing(Rs("impbase"))
            .id_cliente_web = getValueOrNohing(Rs("id_cliente_web"))
            .data_nascita = getValueOrNohing(Rs("data_nascita"))
            .cognome = getValueOrNohing(Rs("cognome"))
            .nome = getValueOrNohing(Rs("nome"))
            .indirizzo = getValueOrNohing(Rs("indirizzo"))
            .citta = getValueOrNohing(Rs("citta"))
            .cap = getValueOrNohing(Rs("cap"))
            .provincia = getValueOrNohing(Rs("provincia"))
            .nazione = getValueOrNohing(Rs("nazione"))
            .email = getValueOrNohing(Rs("email"))
            .telefono = getValueOrNohing(Rs("telefono"))
            .fax = getValueOrNohing(Rs("fax"))
            .cell = getValueOrNohing(Rs("cell"))
            .luogo_nascita = getValueOrNohing(Rs("luogo_nascita"))
            .CodNazioneNascita = getValueOrNohing(Rs("CodNazioneNascita"))
            .codfisc = getValueOrNohing(Rs("codfisc"))
            .patente_num = getValueOrNohing(Rs("patente_num"))
            .patente_ril = getValueOrNohing(Rs("patente_ril"))
            .data_pat_rilascio = getValueOrNohing(Rs("data_pat_rilascio"))
            .scad_patente = getValueOrNohing(Rs("scad_patente"))
            .flag_azienda = getValueOrNohing(Rs("flag_azienda"))
            .id_azienda = getValueOrNohing(Rs("id_azienda"))
            .nome_azienda = getValueOrNohing(Rs("nome_azienda"))
            .indirizzo_az = getValueOrNohing(Rs("indirizzo_az"))
            .citta_az = getValueOrNohing(Rs("citta_az"))
            .cap_az = getValueOrNohing(Rs("cap_az"))
            .prov_az = getValueOrNohing(Rs("prov_az"))
            .tel_az = getValueOrNohing(Rs("tel_az"))
            .fax_az = getValueOrNohing(Rs("fax_az"))
            .cell_az = getValueOrNohing(Rs("cell_az"))
            .email_az = getValueOrNohing(Rs("email_az"))
            .piva = getValueOrNohing(Rs("piva"))
            .gruppi_spec = getValueOrNohing(Rs("gruppi_spec"))
            .COD_CONV = getValueOrNohing(Rs("COD_CONV"))
            .CCNUMAUT = getValueOrNohing(Rs("CCNUMAUT"))
            .CCDATA = getValueOrNohing(Rs("CCDATA"))
            .CCIMPORTO = getDoubleOrNohing(Rs("CCIMPORTO"))
            .CCNUMOPE = getValueOrNohing(Rs("CCNUMOPE"))
            .CCRISP = getValueOrNohing(Rs("CCRISP"))
            .CCTRANS = getValueOrNohing(Rs("CCTRANS"))
            .CCOMPAGNIA = getValueOrNohing(Rs("CCOMPAGNIA"))
            .TRANSOK = getValueOrNohing(Rs("TRANSOK"))
            .TERMINAL_ID = getValueOrNohing(Rs("TERMINAL_ID"))
            .SCARICATADA = getValueOrNohing(Rs("SCARICATADA"))
            .DATA_SCARICO = getValueOrNohing(Rs("DATA_SCARICO"))
            .NOTE = getValueOrNohing(Rs("NOTE"))
            .codici_errore = getValueOrNohing(Rs("codici_errore"))
            .str_data_nascita = getValueOrNohing(Rs("str_data_nascita"))
            .str_data_ril_patente = getValueOrNohing(Rs("str_data_ril_patente"))
            .FileImport = getValueOrNohing(Rs("FileImport"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As TabRibaltamento
        Dim mio_record As TabRibaltamento = Nothing

        Dim sqlStr As String = "SELECT * FROM [ribaltamento] WITH(NOLOCK) WHERE id = " & id_record

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

    Public Function AggiornaRecord() As Boolean
        AggiornaRecord = False

        Dim sqlStr As String = "UPDATE ribaltamento SET" & _
            " provenienza_replica = @provenienza_replica," & _
            " TipoPrenotazione = @TipoPrenotazione," & _
            " id_prenotazione_ares = @id_prenotazione_ares," & _
            " IDPREN_esterno = @IDPREN_esterno," & _
            " CODNUMPREN = @CODNUMPREN," & _
            " NUMPREN = @NUMPREN," & _
            " DATAPREN = @DATAPREN," & _
            " id_gruppo = @id_gruppo," & _
            " cod_gruppo = @cod_gruppo," & _
            " id_gruppo_da_consegnare = @id_gruppo_da_consegnare," & _
            " cod_gruppo_da_consegnare = @cod_gruppo_da_consegnare," & _
            " stato = @stato," & _
            " STA_OUT = @STA_OUT," & _
            " STA_IN = @STA_IN," & _
            " cod_sta_out = @cod_sta_out," & _
            " cod_sta_in = @cod_sta_in," & _
            " data_out = @data_out," & _
            " data_in = @data_in," & _
            " volo_out = @volo_out," & _
            " volo_in = @volo_in," & _
            " idtariffa = @idtariffa," & _
            " codtar = @codtar," & _
            " id_tour_operator = @id_tour_operator," & _
            " sconto = @sconto," & _
            " totale = @totale," & _
            " supplementi = @supplementi," & _
            " impbase = @impbase," & _
            " id_cliente_web = @id_cliente_web," & _
            " data_nascita = @data_nascita," & _
            " cognome = @cognome," & _
            " nome = @nome," & _
            " indirizzo = @indirizzo," & _
            " citta = @citta," & _
            " cap = @cap," & _
            " provincia = @provincia," & _
            " nazione = @nazione," & _
            " email = @email," & _
            " telefono = @telefono," & _
            " fax = @fax," & _
            " cell = @cell," & _
            " luogo_nascita = @luogo_nascita," & _
            " CodNazioneNascita = @CodNazioneNascita," & _
            " codfisc = @codfisc," & _
            " patente_num = @patente_num," & _
            " patente_ril = @patente_ril," & _
            " data_pat_rilascio = @data_pat_rilascio," & _
            " scad_patente = @scad_patente," & _
            " flag_azienda = @flag_azienda," & _
            " id_azienda = @id_azienda," & _
            " nome_azienda = @nome_azienda," & _
            " indirizzo_az = @indirizzo_az," & _
            " citta_az = @citta_az," & _
            " cap_az = @cap_az," & _
            " prov_az = @prov_az," & _
            " tel_az = @tel_az," & _
            " fax_az = @fax_az," & _
            " cell_az = @cell_az," & _
            " email_az = @email_az," & _
            " piva = @piva," & _
            " gruppi_spec = @gruppi_spec," & _
            " COD_CONV = @COD_CONV," & _
            " CCNUMAUT = @CCNUMAUT," & _
            " CCDATA = @CCDATA," & _
            " CCIMPORTO = @CCIMPORTO," & _
            " CCNUMOPE = @CCNUMOPE," & _
            " CCRISP = @CCRISP," & _
            " CCTRANS = @CCTRANS," & _
            " CCOMPAGNIA = @CCOMPAGNIA," & _
            " TRANSOK = @TRANSOK," & _
            " TERMINAL_ID = @TERMINAL_ID," & _
            " SCARICATADA = @SCARICATADA," & _
            " DATA_SCARICO = @DATA_SCARICO," & _
            " NOTE = @NOTE," & _
            " codici_errore = @codici_errore," & _
            " str_data_nascita = @str_data_nascita," & _
            " str_data_ril_patente = @str_data_ril_patente," & _
            " FileImport = @FileImport" & _
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@provenienza_replica", System.Data.SqlDbType.Int, provenienza_replica)
                addParametro(Cmd, "@TipoPrenotazione", System.Data.SqlDbType.Int, TipoPrenotazione)
                addParametro(Cmd, "@id_prenotazione_ares", System.Data.SqlDbType.Int, id_prenotazione_ares)
                addParametro(Cmd, "@IDPREN_esterno", System.Data.SqlDbType.Int, IDPREN_esterno)
                addParametro(Cmd, "@CODNUMPREN", System.Data.SqlDbType.NChar, CODNUMPREN)
                addParametro(Cmd, "@NUMPREN", System.Data.SqlDbType.Int, SubstringSicuroNothing(NUMPREN, 10))
                addParametro(Cmd, "@DATAPREN", System.Data.SqlDbType.DateTime, DATAPREN)
                addParametro(Cmd, "@id_gruppo", System.Data.SqlDbType.Int, id_gruppo)
                addParametro(Cmd, "@cod_gruppo", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cod_gruppo, 3))
                addParametro(Cmd, "@id_gruppo_da_consegnare", System.Data.SqlDbType.Int, id_gruppo_da_consegnare)
                addParametro(Cmd, "@cod_gruppo_da_consegnare", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cod_gruppo_da_consegnare, 3))
                addParametro(Cmd, "@stato", System.Data.SqlDbType.Int, stato)
                addParametro(Cmd, "@STA_OUT", System.Data.SqlDbType.Int, STA_OUT)
                addParametro(Cmd, "@STA_IN", System.Data.SqlDbType.Int, STA_IN)
                addParametro(Cmd, "@cod_sta_out", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cod_sta_out, 20))
                addParametro(Cmd, "@cod_sta_in", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cod_sta_in, 20))
                addParametro(Cmd, "@data_out", System.Data.SqlDbType.DateTime, data_out)
                addParametro(Cmd, "@data_in", System.Data.SqlDbType.DateTime, data_in)
                addParametro(Cmd, "@volo_out", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(volo_out, 100))
                addParametro(Cmd, "@volo_in", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(volo_in, 100))
                addParametro(Cmd, "@idtariffa", System.Data.SqlDbType.Int, idtariffa)
                addParametro(Cmd, "@codtar", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(codtar, 50))
                addParametro(Cmd, "@id_tour_operator", System.Data.SqlDbType.Int, id_tour_operator)
                addParametro(Cmd, "@sconto", System.Data.SqlDbType.Float, sconto)
                addParametro(Cmd, "@totale", System.Data.SqlDbType.Float, totale)
                addParametro(Cmd, "@supplementi", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(supplementi, 50))
                addParametro(Cmd, "@impbase", System.Data.SqlDbType.Float, impbase)
                addParametro(Cmd, "@id_cliente_web", System.Data.SqlDbType.Int, id_cliente_web)
                addParametro(Cmd, "@data_nascita", System.Data.SqlDbType.Date, data_nascita)
                addParametro(Cmd, "@cognome", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cognome, 50))
                addParametro(Cmd, "@nome", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(nome, 50))
                addParametro(Cmd, "@indirizzo", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(indirizzo, 100))
                addParametro(Cmd, "@citta", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(citta, 50))
                addParametro(Cmd, "@cap", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cap, 20))
                addParametro(Cmd, "@provincia", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(provincia, 50))
                addParametro(Cmd, "@nazione", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(nazione, 50))
                addParametro(Cmd, "@email", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(email, 50))
                addParametro(Cmd, "@telefono", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(telefono, 20))
                addParametro(Cmd, "@fax", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(fax, 50))
                addParametro(Cmd, "@cell", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cell, 50))
                addParametro(Cmd, "@luogo_nascita", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(luogo_nascita, 50))
                addParametro(Cmd, "@CodNazioneNascita", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(CodNazioneNascita, 3))
                addParametro(Cmd, "@codfisc", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(codfisc, 20))
                addParametro(Cmd, "@patente_num", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(patente_num, 20))
                addParametro(Cmd, "@patente_ril", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(patente_ril, 50))
                addParametro(Cmd, "@data_pat_rilascio", System.Data.SqlDbType.Date, data_pat_rilascio)
                addParametro(Cmd, "@scad_patente", System.Data.SqlDbType.Date, scad_patente)
                addParametro(Cmd, "@flag_azienda", System.Data.SqlDbType.Bit, flag_azienda)
                addParametro(Cmd, "@id_azienda", System.Data.SqlDbType.Int, id_azienda)
                addParametro(Cmd, "@nome_azienda", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(nome_azienda, 100))
                addParametro(Cmd, "@indirizzo_az", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(indirizzo_az, 100))
                addParametro(Cmd, "@citta_az", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(citta_az, 100))
                addParametro(Cmd, "@cap_az", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cap_az, 50))
                addParametro(Cmd, "@prov_az", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(prov_az, 50))
                addParametro(Cmd, "@tel_az", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(tel_az, 50))
                addParametro(Cmd, "@fax_az", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(fax_az, 50))
                addParametro(Cmd, "@cell_az", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(cell_az, 50))
                addParametro(Cmd, "@email_az", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(email_az, 100))
                addParametro(Cmd, "@piva", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(piva, 50))
                addParametro(Cmd, "@gruppi_spec", System.Data.SqlDbType.Bit, gruppi_spec)
                addParametro(Cmd, "@COD_CONV", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(COD_CONV, 30))
                addParametro(Cmd, "@CCNUMAUT", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(CCNUMAUT, 50))
                addParametro(Cmd, "@CCDATA", System.Data.SqlDbType.DateTime, CCDATA)
                addParametro(Cmd, "@CCIMPORTO", System.Data.SqlDbType.Decimal, CCIMPORTO)
                addParametro(Cmd, "@CCNUMOPE", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(CCNUMOPE, 50))
                addParametro(Cmd, "@CCRISP", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(CCRISP, 50))
                addParametro(Cmd, "@CCTRANS", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(CCTRANS, 50))
                addParametro(Cmd, "@CCOMPAGNIA", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(CCOMPAGNIA, 50))
                addParametro(Cmd, "@TRANSOK", System.Data.SqlDbType.Bit, TRANSOK)
                addParametro(Cmd, "@TERMINAL_ID", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(TERMINAL_ID, 50))
                addParametro(Cmd, "@SCARICATADA", System.Data.SqlDbType.Int, SCARICATADA)
                addParametro(Cmd, "@DATA_SCARICO", System.Data.SqlDbType.DateTime, DATA_SCARICO)
                addParametro(Cmd, "@NOTE", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(NOTE, 255))
                addParametro(Cmd, "@codici_errore", System.Data.SqlDbType.VarChar, SubstringSicuroNothing(codici_errore, 50))
                addParametro(Cmd, "@str_data_nascita", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(str_data_nascita, 50))
                addParametro(Cmd, "@str_data_ril_patente", System.Data.SqlDbType.NVarChar, SubstringSicuroNothing(str_data_ril_patente, 50))
                addParametro(Cmd, "@FileImport", System.Data.SqlDbType.VarChar, SubstringSicuroNothing(FileImport, 20))

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaRecord = True
            End Using
        End Using
    End Function

    Public Function EliminaRecord() As Boolean
        Return EliminaRecord(Me.id)
    End Function

    Public Shared Function EliminaRecord(ByVal id_record As Integer) As Boolean
        EliminaRecord = False

        Dim sqlStr As String = "DELETE FROM ribaltamento WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord ribaltamento: " & ex.Message)
        End Try
    End Function
End Class

