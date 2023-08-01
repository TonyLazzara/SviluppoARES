Imports System.IO
Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports System.Runtime.Serialization


Public Enum stato_form_chiusura_danno
    Edit = 0
    ChiusuraDanno
End Enum


Public Enum enum_odl_stato
    Nuovo = 0
    Attesa_Preventivo = 1
    Attesa_Autorizzazione = 2
    Non_Autorizzato = 3
    Preventivo_Non_Accettato = 4
    Attesa_Riparazione = 5
    Riparato = 6
    Parzialmente_Riparato = 7
    Non_Riparato = 8
    Chiuso = 9
    Chiuso_Con_Continuazione_Noleggio = 10
End Enum

<SerializableAttribute()> _
Public Class sessione_form_chiusura_danno
    Public stato As stato_form_chiusura_danno
    Public id_danno As Integer
    Public id_chiusura_danno As Integer
    Public NotaChiusuraDanno As String
    Public provenienza As String
End Class

Public Class sessione_danni

    Public Enum stato_rds
        Stampa_lettera_al_cliente = -1
        Non_definito = 0
        Da_lavorare = 1
        Chiuso = 2
        In_attesa = 3
        All_attenzione = 4
        Da_addebitare = 5
        Da_fatturare = 6
        Fatturato = 7
    End Enum


    ''' <summary>
    ''' rappresenta l'id del gruppo danni considerato
    ''' il default = 0
    ''' se 0 viene generato in automatico un nuovo gruppo richiamando il componente
    ''' in caso contrario viene richiamato il gruppo passato come parametro, per il veicolo selezionato
    ''' </summary>
    ''' <remarks></remarks>
    Public id_gruppo_danni As Integer = 0
    ''' <summary>
    ''' parametro opzionale per gestire l'eventuale creazione di un nuovo gruppo in caso inserimento nuovo danno
    ''' il default = 0
    ''' se 1 viene creato un nuovo gruppo al primo inserimento di un nuovo danno
    ''' </summary>
    ''' <remarks></remarks>
    Public se_modifica_nuovo_gruppo As Integer = 0
    ''' <summary>
    ''' campo obbligatorio che individua il veicolo selezionato
    ''' il default = 0 
    ''' </summary>
    ''' <remarks></remarks>
    Public id_veicolo As Integer = 0
    ''' <summary>
    ''' campo obbligatorio che individua il tipo del documento che sta utilizzando il componente
    ''' il default = tipo_documento.Nessuno 
    ''' </summary>
    ''' <remarks></remarks>
    Public id_tipo_doc_apertura As tipo_documento = tipo_documento.Nessuno
    ''' <summary>
    ''' campo obbligatorio che individua il l'id del documento che sta utilizzando il componente
    ''' il default = 0 
    ''' </summary>
    ''' <remarks></remarks>
    Public id_doc_apertura As Integer = 0
    ''' <summary>
    ''' campo facoltativo che individua il tipo del documento che sta chiudendo il danno
    ''' nel caso in cui sia differente dal tipo documento apertura
    ''' in caso contrario viene valorizzato con lo stesso tipo di id_tipo_doc_apertura
    ''' </summary>
    ''' <remarks></remarks>
    Public id_tipo_doc_chiusura As tipo_documento = tipo_documento.Nessuno
    ''' <summary>
    ''' campo facoltativo che individua il tipo del documento che sta chiudendo il danno
    ''' nel caso in cui sia differente dall'id documento apertura
    ''' in caso contrario viene valorizzato con lo stesso valore di id_doc_apertura
    ''' </summary>
    ''' <remarks></remarks>
    Public id_doc_chiusura As Integer = 0
    ''' <summary>
    ''' variabile utilizzate per rendere visibile o meno le colonna dei check
    ''' default = true
    ''' </summary>
    ''' <remarks></remarks>
    Public td_check_visible As Boolean = True
    ''' <summary>
    ''' variabile utilizzate per rendere visibile o meno la colonna delle lenti
    ''' default = false
    ''' </summary>
    ''' <remarks></remarks>
    Public td_lente_visible As Boolean = False
    ''' <summary>
    ''' variabile utilizzate per rendere visibile o meno le colonna dell'elimina
    ''' default = false
    ''' </summary>
    ''' <remarks></remarks>
    Public td_elimina_visible As Boolean = False
End Class

Public Enum tipo_documento
    Nessuno = 0
    Contratto = 1
    Rifornimento = 2
    Bisarca = 3
    Lavaggio = 4
    MovimentoInterno = 5
    ODL = 6
    DuranteODL = 7
    RDSGenerico = 100
    Altro = 100 ' Tipi documento che generano un danno non legati ad un id!
    Piazzale = 101
    TrasportoDaFornitore
End Enum

Public Enum Entita_Danno
    nessuno = 0
    lieve = 1
    medio = 2
    grave = 3
End Enum

Public Enum stato_danno
    nodef = 0
    aperto = 1
    chiuso = 2
End Enum

Public Class veicoli_evento_apertura_danno
    Inherits ITabellaDB

    ' Const NomeTabella = "[veicoli_evento_apertura_danno]"

    Protected m_id As Integer
    Protected m_attivo As Boolean
    Protected m_sospeso_rds As Boolean? = False
    Protected m_id_veicolo As Integer
    Protected m_id_tipo_documento_apertura As Integer
    Protected m_id_documento_apertura As Integer?
    Protected m_num_crv As Integer? = 0 ' valore di default... cioè nessun crv
    Protected m_data As Date?
    Protected m_nota As String
    Protected m_id_ditta As Integer?
    Protected m_data_dichiarazione_furto As DateTime?
    Protected m_data_ritrovamento_da_furto As DateTime?
    Protected m_data_creazione As DateTime
    Protected m_id_utente As Integer
    Protected m_data_modifica As DateTime?
    Protected m_id_utente_modifica As Integer?
    ' per RDS
    Protected m_id_rds As String
    Protected m_data_rds As DateTime?
    Protected m_stato_rds As Integer?
    Protected m_nota_rds As String
    Protected m_id_non_addebito As Integer?
    Protected m_stimato As Double?
    Protected m_data_pagamento As DateTime?
    Protected m_importo As Double?
    Protected m_iva As Integer?
    Protected m_totale As Double?
    Protected m_spese_postali As Double?
    Protected m_incasso As Double?
    Protected m_giorni_fermo_tecnico As Integer?
    Protected m_perizia As Boolean?
    Protected m_data_perizia As Date?
    Protected m_attesa_manutenzione As Integer?
    Protected m_attesa_documentazione As Integer?
    Protected m_data_incidente As Date?
    Protected m_luogo_incidente As String
    Protected m_data_chiusura As Date?
    Protected m_id_utente_chiusura As Integer?
    Protected m_doc_CID As Boolean?
    Protected m_doc_denuncia As Boolean?
    Protected m_doc_fotocopia_doc As Boolean?
    Protected m_doc_preventivo As Boolean?
    Protected m_num_fotografie As Integer?
    Protected m_numero_sinistro As Integer?
    Protected m_anno_sinistro As Integer?

    ' gestione degli rds non legati ad un documento!
    Protected m_id_stazione_apertura As Integer?
    Protected m_id_gruppo_danni_apertura As Integer?
    Protected m_id_gruppo_danni_chiusura As Integer?
    Protected m_id_posizione_danno As Integer?


    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property attivo() As Boolean
        Get
            Return m_attivo
        End Get
        Set(ByVal value As Boolean)
            m_attivo = value
        End Set
    End Property
    Public Property sospeso_rds() As Boolean?
        Get
            Return m_sospeso_rds
        End Get
        Set(ByVal value As Boolean?)
            m_sospeso_rds = value
        End Set
    End Property
    Public Property id_veicolo() As Integer
        Get
            Return m_id_veicolo
        End Get
        Set(ByVal value As Integer)
            m_id_veicolo = value
        End Set
    End Property
    Public Property id_tipo_documento_apertura() As Integer
        Get
            Return m_id_tipo_documento_apertura
        End Get
        Set(ByVal value As Integer)
            m_id_tipo_documento_apertura = value
        End Set
    End Property
    Public Property id_documento_apertura() As Integer?
        Get
            Return m_id_documento_apertura
        End Get
        Set(ByVal value As Integer?)
            m_id_documento_apertura = value
        End Set
    End Property
    Public Property num_crv() As Integer?
        Get
            Return m_num_crv
        End Get
        Set(ByVal value As Integer?)
            m_num_crv = value
        End Set
    End Property
    Public Property data() As DateTime?
        Get
            Return m_data
        End Get
        Set(ByVal value As DateTime?)
            m_data = value
        End Set
    End Property
    Public Property nota() As String
        Get
            Return m_nota
        End Get
        Set(ByVal value As String)
            m_nota = value
        End Set
    End Property
    Public Property id_ditta() As Integer?
        Get
            Return m_id_ditta
        End Get
        Set(ByVal value As Integer?)
            m_id_ditta = value
        End Set
    End Property
    Public Property data_dichiarazione_furto() As DateTime?
        Get
            Return m_data_dichiarazione_furto
        End Get
        Set(ByVal value As DateTime?)
            m_data_dichiarazione_furto = value
        End Set
    End Property
    Public Property data_ritrovamento_da_furto() As DateTime?
        Get
            Return m_data_ritrovamento_da_furto
        End Get
        Set(ByVal value As DateTime?)
            m_data_ritrovamento_da_furto = value
        End Set
    End Property
    Public ReadOnly Property data_creazione() As DateTime
        Get
            Return m_data_creazione
        End Get
    End Property
    Public ReadOnly Property id_utente() As Integer
        Get
            Return m_id_utente
        End Get
    End Property
    Public ReadOnly Property data_modifica() As DateTime
        Get
            Return m_data_modifica
        End Get
    End Property
    Public ReadOnly Property id_utente_modifica() As Integer
        Get
            Return m_id_utente_modifica
        End Get
    End Property
    ' per RDS
    Public ReadOnly Property id_rds() As String
        Get
            Return m_id_rds
        End Get
    End Property
    Public ReadOnly Property data_rds() As DateTime?
        Get
            Return m_data_rds
        End Get
    End Property
    Public Property stato_rds() As Integer?
        Get
            Return m_stato_rds
        End Get
        Set(ByVal value As Integer?)
            m_stato_rds = value
        End Set
    End Property
    Public Property nota_rds() As String
        Get
            Return m_nota_rds
        End Get
        Set(ByVal value As String)
            m_nota_rds = value
        End Set
    End Property
    Public Property id_non_addebito() As Integer?
        Get
            Return m_id_non_addebito
        End Get
        Set(ByVal value As Integer?)
            m_id_non_addebito = value
        End Set
    End Property
    Public Property stimato() As Double?
        Get
            Return m_stimato
        End Get
        Set(ByVal value As Double?)
            m_stimato = value
        End Set
    End Property
    Public Property data_pagamento() As DateTime?
        Get
            Return m_data_pagamento
        End Get
        Set(ByVal value As DateTime?)
            m_data_pagamento = value
        End Set
    End Property
    Public Property importo() As Double?
        Get
            Return m_importo
        End Get
        Set(ByVal value As Double?)
            m_importo = value
        End Set
    End Property
    Public Property iva() As Integer?
        Get
            Return m_iva
        End Get
        Set(ByVal value As Integer?)
            m_iva = value
        End Set
    End Property
    Public Property totale() As Double?
        Get
            Return m_totale
        End Get
        Set(ByVal value As Double?)
            m_totale = value
        End Set
    End Property
    Public Property spese_postali() As Double?
        Get
            Return m_spese_postali
        End Get
        Set(ByVal value As Double?)
            m_spese_postali = value
        End Set
    End Property
    Public Property incasso() As Double?
        Get
            Return m_incasso
        End Get
        Set(ByVal value As Double?)
            m_incasso = value
        End Set
    End Property
    Public Property giorni_fermo_tecnico() As Integer?
        Get
            Return m_giorni_fermo_tecnico
        End Get
        Set(ByVal value As Integer?)
            m_giorni_fermo_tecnico = value
        End Set
    End Property
    Public Property perizia() As Boolean?
        Get
            Return m_perizia
        End Get
        Set(ByVal value As Boolean?)
            m_perizia = value
        End Set
    End Property
    Public Property data_perizia() As Date?
        Get
            Return m_data_perizia
        End Get
        Set(ByVal value As Date?)
            m_data_perizia = value
        End Set
    End Property
    Public Property attesa_manutenzione() As Integer?
        Get
            Return m_attesa_manutenzione
        End Get
        Set(ByVal value As Integer?)
            m_attesa_manutenzione = value
        End Set
    End Property
    Public Property attesa_documentazione() As Integer?
        Get
            Return m_attesa_documentazione
        End Get
        Set(ByVal value As Integer?)
            m_attesa_documentazione = value
        End Set
    End Property
    Public Property data_incidente() As Date?
        Get
            Return m_data_incidente
        End Get
        Set(ByVal value As Date?)
            m_data_incidente = value
        End Set
    End Property
    Public Property luogo_incidente() As String
        Get
            Return m_luogo_incidente
        End Get
        Set(ByVal value As String)
            m_luogo_incidente = value
        End Set
    End Property
    Public ReadOnly Property data_chiusura() As Date?
        Get
            Return m_data_chiusura
        End Get
    End Property
    Public ReadOnly Property id_utente_chiusura() As Integer
        Get
            Return m_id_utente_chiusura
        End Get
    End Property
    '------------------------------------------------
    Public Property doc_CID() As Boolean?
        Get
            Return m_doc_CID
        End Get
        Set(ByVal value As Boolean?)
            m_doc_CID = value
        End Set
    End Property
    Public Property doc_denuncia() As Boolean?
        Get
            Return m_doc_denuncia
        End Get
        Set(ByVal value As Boolean?)
            m_doc_denuncia = value
        End Set
    End Property
    Public Property doc_fotocopia_doc() As Boolean?
        Get
            Return m_doc_fotocopia_doc
        End Get
        Set(ByVal value As Boolean?)
            m_doc_fotocopia_doc = value
        End Set
    End Property
    Public Property doc_preventivo() As Boolean?
        Get
            Return m_doc_preventivo
        End Get
        Set(ByVal value As Boolean?)
            m_doc_preventivo = value
        End Set
    End Property
    Public Property num_fotografie() As Integer?
        Get
            Return m_num_fotografie
        End Get
        Set(ByVal value As Integer?)
            m_num_fotografie = value
        End Set
    End Property
    Public Property numero_sinistro() As Integer?
        Get
            Return m_numero_sinistro
        End Get
        Set(ByVal value As Integer?)
            m_numero_sinistro = value
        End Set
    End Property
    Public Property anno_sinistro() As Integer?
        Get
            Return m_anno_sinistro
        End Get
        Set(ByVal value As Integer?)
            m_anno_sinistro = value
        End Set
    End Property

    Public Property id_stazione_apertura() As Integer?
        Get
            Return m_id_stazione_apertura
        End Get
        Set(ByVal value As Integer?)
            m_id_stazione_apertura = value
        End Set
    End Property
    Public Property id_gruppo_danni_apertura() As Integer?
        Get
            Return m_id_gruppo_danni_apertura
        End Get
        Set(ByVal value As Integer?)
            m_id_gruppo_danni_apertura = value
        End Set
    End Property
    Public Property id_gruppo_danni_chiusura() As Integer?
        Get
            Return m_id_gruppo_danni_chiusura
        End Get
        Set(ByVal value As Integer?)
            m_id_gruppo_danni_chiusura = value
        End Set
    End Property

    Public Property id_posizione_danno() As Integer?
        Get
            Return m_id_gruppo_danni_chiusura
        End Get
        Set(ByVal value As Integer?)
            m_id_gruppo_danni_chiusura = value
        End Set
    End Property







    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer

        Dim sqlStr As String = "INSERT INTO [veicoli_evento_apertura_danno] (id_veicolo,attivo,sospeso_rds,id_tipo_documento_apertura,id_documento_apertura,num_crv,data,nota,id_ditta,data_dichiarazione_furto,data_ritrovamento_da_furto,data_creazione,id_utente,stato_rds,nota_rds,id_non_addebito,stimato,data_pagamento,importo,iva,totale,spese_postali,incasso,giorni_fermo_tecnico,perizia,data_perizia,attesa_manutenzione,attesa_documentazione,data_incidente,luogo_incidente,doc_CID,doc_denuncia,doc_fotocopia_doc,doc_preventivo,num_fotografie,numero_sinistro,anno_sinistro,id_stazione_apertura,id_gruppo_danni_apertura,id_gruppo_danni_chiusura)" & _
            " VALUES (@id_veicolo,@attivo,@sospeso_rds,@id_tipo_documento_apertura,@id_documento_apertura,@num_crv,@data,@nota,@id_ditta,@data_dichiarazione_furto,@data_ritrovamento_da_furto,@data_creazione,@id_utente,@stato_rds,@nota_rds,@id_non_addebito,@stimato,@data_pagamento,@importo,@iva,@totale,@spese_postali,@incasso,@giorni_fermo_tecnico,@perizia,@data_perizia,@attesa_manutenzione,@attesa_documentazione,@data_incidente,@luogo_incidente,@doc_CID,@doc_denuncia,@doc_fotocopia_doc,@doc_preventivo,@num_fotografie,@numero_sinistro,@anno_sinistro,@id_stazione_apertura,@id_gruppo_danni_apertura,@id_gruppo_danni_chiusura)"

        m_data_creazione = Now
        m_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)

                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                addParametro(Cmd, "@attivo", System.Data.SqlDbType.Bit, attivo)
                addParametro(Cmd, "@sospeso_rds", System.Data.SqlDbType.Bit, sospeso_rds)
                addParametro(Cmd, "@id_tipo_documento_apertura", System.Data.SqlDbType.Int, id_tipo_documento_apertura)
                addParametro(Cmd, "@id_documento_apertura", System.Data.SqlDbType.Int, id_documento_apertura)
                addParametro(Cmd, "@num_crv", System.Data.SqlDbType.Int, num_crv)
                addParametro(Cmd, "@data", System.Data.SqlDbType.Date, data)
                addParametro(Cmd, "@nota", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nota))
                addParametro(Cmd, "@id_ditta", System.Data.SqlDbType.Int, id_ditta)
                addParametro(Cmd, "@data_dichiarazione_furto", System.Data.SqlDbType.DateTime, data_dichiarazione_furto)
                addParametro(Cmd, "@data_ritrovamento_da_furto", System.Data.SqlDbType.DateTime, data_ritrovamento_da_furto)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)

                addParametro(Cmd, "@stato_rds", System.Data.SqlDbType.Int, stato_rds)
                addParametro(Cmd, "@nota_rds", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nota_rds))
                addParametro(Cmd, "@id_non_addebito", System.Data.SqlDbType.Int, id_non_addebito)
                addParametro(Cmd, "@stimato", System.Data.SqlDbType.Real, stimato)
                addParametro(Cmd, "@data_pagamento", System.Data.SqlDbType.DateTime, data_pagamento)
                addParametro(Cmd, "@importo", System.Data.SqlDbType.Real, importo)
                addParametro(Cmd, "@iva", System.Data.SqlDbType.Int, iva)
                addParametro(Cmd, "@totale", System.Data.SqlDbType.Real, totale)
                addParametro(Cmd, "@spese_postali", System.Data.SqlDbType.Real, spese_postali)
                addParametro(Cmd, "@incasso", System.Data.SqlDbType.Real, incasso)
                addParametro(Cmd, "@giorni_fermo_tecnico", System.Data.SqlDbType.Int, giorni_fermo_tecnico)
                addParametro(Cmd, "@perizia", System.Data.SqlDbType.Bit, perizia)
                addParametro(Cmd, "@data_perizia", System.Data.SqlDbType.Date, data_perizia)
                addParametro(Cmd, "@attesa_manutenzione", System.Data.SqlDbType.Int, attesa_manutenzione)
                addParametro(Cmd, "@attesa_documentazione", System.Data.SqlDbType.Int, attesa_documentazione)
                addParametro(Cmd, "@data_incidente", System.Data.SqlDbType.Date, data_incidente)
                addParametro(Cmd, "@luogo_incidente", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(luogo_incidente, 100))

                addParametro(Cmd, "@doc_CID", System.Data.SqlDbType.Bit, doc_CID)
                addParametro(Cmd, "@doc_denuncia", System.Data.SqlDbType.Bit, doc_denuncia)
                addParametro(Cmd, "@doc_fotocopia_doc", System.Data.SqlDbType.Bit, doc_fotocopia_doc)
                addParametro(Cmd, "@doc_preventivo", System.Data.SqlDbType.Bit, doc_preventivo)
                addParametro(Cmd, "@num_fotografie", System.Data.SqlDbType.Int, num_fotografie)
                addParametro(Cmd, "@numero_sinistro", System.Data.SqlDbType.Int, numero_sinistro)
                addParametro(Cmd, "@anno_sinistro", System.Data.SqlDbType.Int, anno_sinistro)

                addParametro(Cmd, "@id_stazione_apertura", System.Data.SqlDbType.Int, id_stazione_apertura)
                addParametro(Cmd, "@id_gruppo_danni_apertura", System.Data.SqlDbType.Int, id_gruppo_danni_apertura)
                addParametro(Cmd, "@id_gruppo_danni_chiusura", System.Data.SqlDbType.Int, id_gruppo_danni_chiusura)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM [veicoli_evento_apertura_danno]"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_evento_apertura_danno
        Dim mio_record As veicoli_evento_apertura_danno = New veicoli_evento_apertura_danno
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .attivo = getValueOrNohing(Rs("attivo"))
            .sospeso_rds = getValueOrNohing(Rs("sospeso_rds"))
            .id_veicolo = getValueOrNohing(Rs("id_veicolo"))
            .id_tipo_documento_apertura = getValueOrNohing(Rs("id_tipo_documento_apertura"))
            .id_documento_apertura = getValueOrNohing(Rs("id_documento_apertura"))
            .num_crv = getValueOrNohing(Rs("num_crv"))
            .data = getValueOrNohing(Rs("data"))
            .nota = getValueOrNohing(Rs("nota"))
            .m_data_creazione = getValueOrNohing(Rs("data_creazione"))
            .m_id_utente = getValueOrNohing(Rs("id_utente"))
            .id_ditta = getValueOrNohing(Rs("id_ditta"))
            .data_dichiarazione_furto = getValueOrNohing(Rs("data_dichiarazione_furto"))
            .data_ritrovamento_da_furto = getValueOrNohing(Rs("data_ritrovamento_da_furto"))
            .m_data_modifica = getValueOrNohing(Rs("data_modifica"))
            .m_id_utente_modifica = getValueOrNohing(Rs("id_utente_modifica"))
            .m_id_rds = getValueOrNohing(Rs("id_rds"))
            .m_data_rds = getValueOrNohing(Rs("data_rds"))
            .stato_rds = getValueOrNohing(Rs("stato_rds"))
            .nota_rds = getValueOrNohing(Rs("nota_rds"))
            .id_non_addebito = getValueOrNohing(Rs("id_non_addebito"))
            .stimato = getDoubleOrNohing(Rs("stimato"))
            .data_pagamento = getValueOrNohing(Rs("data_pagamento"))
            .importo = getDoubleOrNohing(Rs("importo"))
            .iva = getValueOrNohing(Rs("iva"))
            .totale = getDoubleOrNohing(Rs("totale"))
            .spese_postali = getDoubleOrNohing(Rs("spese_postali"))
            .incasso = getDoubleOrNohing(Rs("incasso"))
            .giorni_fermo_tecnico = getValueOrNohing(Rs("giorni_fermo_tecnico"))
            .perizia = getValueOrNohing(Rs("perizia"))
            .data_perizia = getValueOrNohing(Rs("data_perizia"))
            .attesa_manutenzione = getValueOrNohing(Rs("attesa_manutenzione"))
            .attesa_documentazione = getValueOrNohing(Rs("attesa_documentazione"))
            .data_incidente = getValueOrNohing(Rs("data_incidente"))
            .luogo_incidente = getValueOrNohing(Rs("luogo_incidente"))
            .m_data_chiusura = getValueOrNohing(Rs("data_chiusura"))
            .m_id_utente_chiusura = getValueOrNohing(Rs("id_utente_chiusura"))

            .doc_CID = getValueOrNohing(Rs("doc_CID"))
            .doc_denuncia = getValueOrNohing(Rs("doc_denuncia"))
            .doc_fotocopia_doc = getValueOrNohing(Rs("doc_fotocopia_doc"))
            .doc_preventivo = getValueOrNohing(Rs("doc_preventivo"))
            .num_fotografie = getValueOrNohing(Rs("num_fotografie"))

            .numero_sinistro = getValueOrNohing(Rs("numero_sinistro"))
            .anno_sinistro = getValueOrNohing(Rs("anno_sinistro"))

            .id_stazione_apertura = getValueOrNohing(Rs("id_stazione_apertura"))
            .id_gruppo_danni_apertura = getValueOrNohing(Rs("id_gruppo_danni_apertura"))
            .id_gruppo_danni_chiusura = getValueOrNohing(Rs("id_gruppo_danni_chiusura"))

            '.id_posizione_danno = getValueOrNohing(Rs("id_posizione_danno"))


        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As veicoli_evento_apertura_danno
        Dim mio_record As veicoli_evento_apertura_danno = Nothing

        Dim sqlStr As String = "SELECT * FROM [veicoli_evento_apertura_danno] WITH(NOLOCK) WHERE id = " & id_record

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

    Public Shared Function getRecordDaDocumento(ByVal id_tipo_documento As tipo_documento, ByVal id_documento As Integer, Optional ByVal numero_crv As Integer = 0) As veicoli_evento_apertura_danno
        Dim mio_record As veicoli_evento_apertura_danno = Nothing

        'Select Case id_tipo_documento
        '    Case tipo_documento.Contratto_Chiusura
        '    Case Else
        '        Err.Raise(1001, Nothing, "Tipo documento non gestito dal metodo getRecordDaDocumento")
        'End Select

        Dim sqlStr As String = "SELECT * FROM veicoli_evento_apertura_danno WITH(NOLOCK)" &
            " WHERE id_tipo_documento_apertura = " & id_tipo_documento &
            " AND id_documento_apertura = " & id_documento &
            " AND num_crv = " & numero_crv &
            " AND attivo = 1"

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

    Public Shared Function getRecordDaRDS(ByVal id_tipo_documento As tipo_documento, ByVal numero_rds As Integer) As veicoli_evento_apertura_danno
        Dim mio_record As veicoli_evento_apertura_danno = Nothing

        Dim sqlStr As String = "SELECT * FROM veicoli_evento_apertura_danno WITH(NOLOCK)" &
            " WHERE id_tipo_documento_apertura = " & id_tipo_documento &
            " AND id_rds = " & numero_rds &
            " AND attivo = 1"

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

        Dim sqlStr As String = "UPDATE [veicoli_evento_apertura_danno] SET" &
            " id_veicolo = @id_veicolo," &
            " attivo = @attivo," &
            " sospeso_rds = @sospeso_rds," &
            " id_tipo_documento_apertura = @id_tipo_documento_apertura," &
            " id_documento_apertura = @id_documento_apertura," &
            " num_crv = @num_crv," &
            " data = @data," &
            " nota = @nota," &
            " id_ditta = @id_ditta," &
            " data_dichiarazione_furto = @data_dichiarazione_furto," &
            " data_ritrovamento_da_furto = @data_ritrovamento_da_furto," &
            " stato_rds = @stato_rds," &
            " nota_rds = @nota_rds," &
            " id_non_addebito = @id_non_addebito," &
            " stimato = @stimato," &
            " data_pagamento = @data_pagamento," &
            " importo = @importo," &
            " iva = @iva," &
            " totale = @totale," &
            " spese_postali = @spese_postali," &
            " incasso = @incasso," &
            " giorni_fermo_tecnico = @giorni_fermo_tecnico," &
            " perizia = @perizia," &
            " data_perizia = @data_perizia," &
            " attesa_manutenzione = @attesa_manutenzione," &
            " attesa_documentazione = @attesa_documentazione," &
            " data_incidente = @data_incidente," &
            " luogo_incidente = @luogo_incidente," &
            " doc_CID = @doc_CID," &
            " doc_denuncia = @doc_denuncia," &
            " doc_fotocopia_doc = @doc_fotocopia_doc," &
            " doc_preventivo = @doc_preventivo," &
            " num_fotografie = @num_fotografie," &
            " numero_sinistro = @numero_sinistro," &
            " anno_sinistro = @anno_sinistro," &
            " id_stazione_apertura = @id_stazione_apertura," &
            " id_gruppo_danni_apertura = @id_gruppo_danni_apertura," &
            " id_gruppo_danni_chiusura = @id_gruppo_danni_chiusura," &
            " data_modifica = @data_modifica," &
            " id_utente_modifica = @id_utente_modifica" &
            " WHERE id = @id"

        m_data_modifica = Now
        m_id_utente_modifica = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                addParametro(Cmd, "@attivo", System.Data.SqlDbType.Bit, attivo)
                addParametro(Cmd, "@sospeso_rds", System.Data.SqlDbType.Bit, sospeso_rds)
                addParametro(Cmd, "@id_tipo_documento_apertura", System.Data.SqlDbType.Int, id_tipo_documento_apertura)
                addParametro(Cmd, "@id_documento_apertura", System.Data.SqlDbType.Int, id_documento_apertura)
                addParametro(Cmd, "@num_crv", System.Data.SqlDbType.Int, num_crv)
                addParametro(Cmd, "@data", System.Data.SqlDbType.Date, data)
                addParametro(Cmd, "@nota", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nota))
                addParametro(Cmd, "@id_ditta", System.Data.SqlDbType.Int, id_ditta)
                addParametro(Cmd, "@data_dichiarazione_furto", System.Data.SqlDbType.DateTime, data_dichiarazione_furto)
                addParametro(Cmd, "@data_ritrovamento_da_furto", System.Data.SqlDbType.DateTime, data_ritrovamento_da_furto)

                addParametro(Cmd, "@stato_rds", System.Data.SqlDbType.Int, stato_rds)
                addParametro(Cmd, "@nota_rds", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nota_rds))
                addParametro(Cmd, "@id_non_addebito", System.Data.SqlDbType.Int, id_non_addebito)
                addParametro(Cmd, "@stimato", System.Data.SqlDbType.Real, stimato)
                addParametro(Cmd, "@data_pagamento", System.Data.SqlDbType.DateTime, data_pagamento)
                addParametro(Cmd, "@importo", System.Data.SqlDbType.Real, importo)
                addParametro(Cmd, "@iva", System.Data.SqlDbType.Int, iva)
                addParametro(Cmd, "@totale", System.Data.SqlDbType.Real, totale)
                addParametro(Cmd, "@spese_postali", System.Data.SqlDbType.Real, spese_postali)
                addParametro(Cmd, "@incasso", System.Data.SqlDbType.Real, incasso)
                addParametro(Cmd, "@giorni_fermo_tecnico", System.Data.SqlDbType.Int, giorni_fermo_tecnico)
                addParametro(Cmd, "@perizia", System.Data.SqlDbType.Bit, perizia)
                addParametro(Cmd, "@data_perizia", System.Data.SqlDbType.Date, data_perizia)
                addParametro(Cmd, "@attesa_manutenzione", System.Data.SqlDbType.Int, attesa_manutenzione)
                addParametro(Cmd, "@attesa_documentazione", System.Data.SqlDbType.Int, attesa_documentazione)
                addParametro(Cmd, "@data_incidente", System.Data.SqlDbType.Date, data_incidente)
                addParametro(Cmd, "@luogo_incidente", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(luogo_incidente, 100))

                addParametro(Cmd, "@doc_CID", System.Data.SqlDbType.Bit, doc_CID)
                addParametro(Cmd, "@doc_denuncia", System.Data.SqlDbType.Bit, doc_denuncia)
                addParametro(Cmd, "@doc_fotocopia_doc", System.Data.SqlDbType.Bit, doc_fotocopia_doc)
                addParametro(Cmd, "@doc_preventivo", System.Data.SqlDbType.Bit, doc_preventivo)
                addParametro(Cmd, "@num_fotografie", System.Data.SqlDbType.Int, num_fotografie)

                addParametro(Cmd, "@numero_sinistro", System.Data.SqlDbType.Int, numero_sinistro)
                addParametro(Cmd, "@anno_sinistro", System.Data.SqlDbType.Int, anno_sinistro)

                addParametro(Cmd, "@id_stazione_apertura", System.Data.SqlDbType.Int, id_stazione_apertura)
                addParametro(Cmd, "@id_gruppo_danni_apertura", System.Data.SqlDbType.Int, id_gruppo_danni_apertura)
                addParametro(Cmd, "@id_gruppo_danni_chiusura", System.Data.SqlDbType.Int, id_gruppo_danni_chiusura)

                addParametro(Cmd, "@data_modifica", System.Data.SqlDbType.DateTime, data_modifica)
                addParametro(Cmd, "@id_utente_modifica", System.Data.SqlDbType.Int, id_utente_modifica)

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

        Dim sqlStr As String = "DELETE FROM [veicoli_evento_apertura_danno] WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord [veicoli_evento_apertura_danno]: " & ex.Message)
        End Try
    End Function

    Public Function SalvaStatoInizialeVariazioneRDS() As Boolean
        SalvaStatoInizialeVariazioneRDS = False

        Dim mia_variazione_stato_rds As veicoli_stato_rds_variazione = New veicoli_stato_rds_variazione
        With mia_variazione_stato_rds
            .id_evento = Me.id
            .id_stato_precedente = 0 ' stato_rds.Non_definito
            .id_stato = 1 ' stato_rds.da_lavorare
            .importo = Nothing
            .iva = Nothing
            .spese_postali = Nothing
            .att_manutenzione = Nothing
            .att_documentazione = Nothing

            .SalvaRecord()
            SalvaStatoInizialeVariazioneRDS = True
        End With
    End Function

    Public Function AttivaRecord(ByVal id_stazione As String, Optional ByVal stato_sospeso_rds As Boolean = False) As Boolean
        AttivaRecord = False

        Dim num_rds As String = Contatori.getContatore_RDS(id_stazione)
        m_id_rds = Integer.Parse(num_rds)
        m_data_rds = Now

        SalvaStatoInizialeVariazioneRDS()

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String = "UPDATE veicoli_danni SET attivo = 1 WHERE id_evento_apertura = " & id
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteNonQuery()
            End Using

            sqlStr = "UPDATE [veicoli_evento_apertura_danno] SET" &
                " attivo = 1," &
                " sospeso_rds = @sospeso_rds," &
                " id_rds = @id_rds," &
                " data_rds = @data_rds," &
                " stato_rds = 1" &
                " WHERE id = @id"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@sospeso_rds", System.Data.SqlDbType.Bit, stato_sospeso_rds)
                addParametro(Cmd, "@id_rds", System.Data.SqlDbType.Int, id_rds)
                addParametro(Cmd, "@data_rds", System.Data.SqlDbType.DateTime, data_rds)

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Cmd.ExecuteNonQuery()
                AttivaRecord = True
            End Using
        End Using

    End Function

    Public Function Chiudi_RDS(ByVal id_non_addebito As Integer) As Boolean
        Chiudi_RDS = False

        Dim sqlStr As String = "UPDATE [veicoli_evento_apertura_danno] SET" &
            " stato_rds = @stato_rds," &
            " id_non_addebito = @id_non_addebito," &
            " data_chiusura = @data_chiusura," &
            " id_utente_chiusura = @id_utente_chiusura" &
            " WHERE id = @id"

        Me.stato_rds = 2 ' stato_rds.Chiuso ... ho usato lo stesso nome dell'enum e non posso utilizzare la costante!!!

        m_data_chiusura = Now
        m_id_utente_chiusura = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@stato_rds", System.Data.SqlDbType.Int, stato_rds)
                addParametro(Cmd, "@id_non_addebito", System.Data.SqlDbType.Int, id_non_addebito)
                addParametro(Cmd, "@data_chiusura", System.Data.SqlDbType.DateTime, data_chiusura)
                addParametro(Cmd, "@id_utente_chiusura", System.Data.SqlDbType.Int, id_utente_chiusura)

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                Chiudi_RDS = True
            End Using
        End Using

    End Function

    Public Function Salva_Sinistro(ByVal sinistro_anno As Integer, ByVal sinistro_numero As Integer) As Boolean
        Return Salva_Sinistro(id, sinistro_anno, sinistro_numero)
    End Function

    Public Shared Function Salva_Sinistro(ByVal id_evento As Integer, ByVal sinistro_anno As Integer, ByVal sinistro_numero As Integer) As Boolean
        Salva_Sinistro = False

        Dim sqlStr As String = "UPDATE [veicoli_evento_apertura_danno] SET" &
            " anno_sinistro = @anno_sinistro," &
            " numero_sinistro = @numero_sinistro" &
            " WHERE id = @id"
        ' non so se devo modificare la data e l'utente che modifica il record...

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@anno_sinistro", System.Data.SqlDbType.Int, sinistro_anno)
                addParametro(Cmd, "@numero_sinistro", System.Data.SqlDbType.Int, sinistro_numero)

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id_evento)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                Salva_Sinistro = True
            End Using
        End Using
    End Function

    'Public Function getRiferimentoDocumento() As String
    '    Dim sqlStr As String = Nothing

    '    Dim TipoDocumento As tipo_documento = id_tipo_documento_apertura

    '    Select Case TipoDocumento
    '        Case tipo_documento.Contratto
    '            sqlStr = "SELECT TOP 1 id FROM contratti WITH(NOLOCK)" & _
    '                " WHERE num_contratto = " & id_documento_apertura & _
    '                " AND num_crv = " & num_crv & _
    '                " ORDER BY num_calcolo DESC"
    '        Case Else
    '            Err.Raise(1001, Me, "Tipo documento apertura danno con id documento e numero crv non censito.")
    '    End Select

    '    Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '            Dbc.Open()
    '            getRiferimentoDocumento = Cmd.ExecuteScalar()
    '        End Using
    '    End Using
    'End Function

    Public Function verificaDanniBloccanti() As Boolean
        Return verificaDanniBloccanti(id)
    End Function

    Public Shared Function verificaDanniBloccanti(ByVal id_evento As Integer) As Boolean
        Dim sqlStr As String
        sqlStr = "SELECT TOP 1 d.id FROM veicoli_danni d WITH(NOLOCK)" &
            " INNER JOIN veicoli_posizione_danno p WITH(NOLOCK) ON d.id_posizione_danno = p.id " &
            " WHERE d.id_evento_apertura = @id" &
            " AND p.bloccante = 1"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id_evento)
                Dbc.Open()
                Dim Valore As String = Cmd.ExecuteScalar() & ""
                If Valore = "" Then
                    Return False
                Else
                    Return True
                End If
            End Using
        End Using
    End Function

    Public Function SalvaFurtoInMovimentiTarga(ByVal id_stazione As Integer) As Boolean
        SalvaFurtoInMovimentiTarga = False
        Dim sqlStr As String
        sqlStr = "INSERT INTO [movimenti_targa] (num_riferimento,id_veicolo,id_tipo_movimento,data_uscita,id_stazione_uscita,id_operatore,data_registrazione,movimento_attivo)" &
            " VALUES (@num_riferimento,@id_veicolo,@id_tipo_movimento,@data_uscita,@id_stazione_uscita,@id_operatore,@data_registrazione,@movimento_attivo)"

        Dim id_operatore As Integer = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))
        Dim data_registrazione As DateTime = Now

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@num_riferimento", System.Data.SqlDbType.Int, id)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                addParametro(Cmd, "@id_tipo_movimento", System.Data.SqlDbType.Int, 8) ' = Furto
                addParametro(Cmd, "@data_uscita", System.Data.SqlDbType.DateTime, data_dichiarazione_furto)
                addParametro(Cmd, "@id_stazione_uscita", System.Data.SqlDbType.Int, id_stazione)
                addParametro(Cmd, "@movimento_attivo", System.Data.SqlDbType.Bit, True)

                addParametro(Cmd, "@id_operatore", System.Data.SqlDbType.Int, id_operatore)
                addParametro(Cmd, "@data_registrazione", System.Data.SqlDbType.DateTime, data_registrazione)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                SalvaFurtoInMovimentiTarga = True
            End Using
        End Using

    End Function


    Public Function AggiornaVeicoliPerFurto() As Boolean
        AggiornaVeicoliPerFurto = False
        Dim sqlStr As String
        sqlStr = "UPDATE veicoli SET" &
            " disponibile_nolo = 0," &
            " noleggiata = 0," &
            " in_vendita = 0," &
            " furto = 1," &
            " id_evento_furto = @id_evento_furto" &
            " WHERE id = @id_veicolo"

        Dim id_operatore As Integer = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))
        Dim data_registrazione As DateTime = Now

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_evento_furto", System.Data.SqlDbType.Int, id)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using

    End Function

    Public Function EliminaDanniNonAttivi() As Boolean
        EliminaDanniNonAttivi = False

        Dim sqlStr As String = "DELETE veicoli_danni " &
            " WHERE id_evento_apertura = @id_evento_apertura" &
            " AND attivo = 0"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_evento_apertura", System.Data.SqlDbType.Int, id)
                Dbc.Open()
                Cmd.ExecuteNonQuery()
                EliminaDanniNonAttivi = True
            End Using
        End Using
    End Function

End Class

Public Class veicoli_stato_rds_variazione
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_id_evento As Integer
    Protected m_id_stato_precedente As Integer
    Protected m_id_stato As Integer
    Protected m_data_creazione As DateTime
    Protected m_id_utente As Integer
    Protected m_importo As Double?
    Protected m_iva As Integer?
    Protected m_spese_postali As Double?
    Protected m_incasso As Double?
    Protected m_att_manutenzione As Integer?
    Protected m_att_documentazione As Integer?

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property id_evento() As Integer
        Get
            Return m_id_evento
        End Get
        Set(ByVal value As Integer)
            m_id_evento = value
        End Set
    End Property
    Public Property id_stato_precedente() As Integer
        Get
            Return m_id_stato_precedente
        End Get
        Set(ByVal value As Integer)
            m_id_stato_precedente = value
        End Set
    End Property
    Public Property id_stato() As Integer
        Get
            Return m_id_stato
        End Get
        Set(ByVal value As Integer)
            m_id_stato = value
        End Set
    End Property
    Public ReadOnly Property data_creazione() As DateTime
        Get
            Return m_data_creazione
        End Get
    End Property
    Public ReadOnly Property id_utente() As Integer
        Get
            Return m_id_utente
        End Get
    End Property
    Public Property importo() As Double?
        Get
            Return m_importo
        End Get
        Set(ByVal value As Double?)
            m_importo = value
        End Set
    End Property
    Public Property iva() As Integer?
        Get
            Return m_iva
        End Get
        Set(ByVal value As Integer?)
            m_iva = value
        End Set
    End Property
    Public Property spese_postali() As Double?
        Get
            Return m_spese_postali
        End Get
        Set(ByVal value As Double?)
            m_spese_postali = value
        End Set
    End Property
    Public Property incasso() As Double?
        Get
            Return m_incasso
        End Get
        Set(ByVal value As Double?)
            m_incasso = value
        End Set
    End Property
    Public Property att_manutenzione() As Integer?
        Get
            Return m_att_manutenzione
        End Get
        Set(ByVal value As Integer?)
            m_att_manutenzione = value
        End Set
    End Property
    Public Property att_documentazione() As Integer?
        Get
            Return m_att_documentazione
        End Get
        Set(ByVal value As Integer?)
            m_att_documentazione = value
        End Set
    End Property


    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO veicoli_stato_rds_variazione (id_evento,id_stato_precedente,id_stato,data_creazione,id_utente,importo,iva,spese_postali,incasso,att_manutenzione,att_documentazione)" &
            " VALUES (@id_evento,@id_stato_precedente,@id_stato,@data_creazione,@id_utente,@importo,@iva,@spese_postali,@incasso,@att_manutenzione,@att_documentazione)"

        m_data_creazione = Now
        m_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_evento", System.Data.SqlDbType.Int, id_evento)
                addParametro(Cmd, "@id_stato_precedente", System.Data.SqlDbType.Int, id_stato_precedente)
                addParametro(Cmd, "@id_stato", System.Data.SqlDbType.Int, id_stato)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)
                addParametro(Cmd, "@importo", System.Data.SqlDbType.Real, importo)
                addParametro(Cmd, "@iva", System.Data.SqlDbType.Int, iva)
                addParametro(Cmd, "@spese_postali", System.Data.SqlDbType.Real, spese_postali)
                addParametro(Cmd, "@incasso", System.Data.SqlDbType.Real, incasso)
                addParametro(Cmd, "@att_manutenzione", System.Data.SqlDbType.Int, att_manutenzione)
                addParametro(Cmd, "@att_documentazione", System.Data.SqlDbType.Int, att_documentazione)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM veicoli_stato_rds_variazione"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Public Sub InitDati(ByVal mio_evento As veicoli_evento_apertura_danno, ByVal stato_precedente As sessione_danni.stato_rds)
        With mio_evento
            id_evento = .id
            id_stato_precedente = stato_precedente
            id_stato = .stato_rds
            importo = .importo
            iva = .iva
            spese_postali = .spese_postali
            incasso = .incasso
            att_manutenzione = .attesa_manutenzione
            att_documentazione = .attesa_documentazione
        End With

    End Sub

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_stato_rds_variazione
        Dim mio_record As veicoli_stato_rds_variazione = New veicoli_stato_rds_variazione
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .id_evento = getValueOrNohing(Rs("id_evento"))
            .id_stato_precedente = getValueOrNohing(Rs("id_stato_precedente"))
            .id_stato = getValueOrNohing(Rs("id_stato"))
            .m_data_creazione = getValueOrNohing(Rs("data_creazione"))
            .m_id_utente = getValueOrNohing(Rs("id_utente"))
            .importo = getDoubleOrNohing(Rs("importo"))
            .iva = getValueOrNohing(Rs("iva"))
            .spese_postali = getDoubleOrNohing(Rs("spese_postali"))
            .incasso = getDoubleOrNohing(Rs("incasso"))
            .att_manutenzione = getValueOrNohing(Rs("att_manutenzione"))
            .att_documentazione = getValueOrNohing(Rs("att_documentazione"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As veicoli_stato_rds_variazione
        Dim mio_record As veicoli_stato_rds_variazione = Nothing

        Dim sqlStr As String = "SELECT * FROM veicoli_stato_rds_variazione WITH(NOLOCK) WHERE id = " & id_record

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

    Public Shared Function getUltimaStampa(ByVal id_evento As Integer) As veicoli_stato_rds_variazione
        Dim mio_record As veicoli_stato_rds_variazione = Nothing

        Dim sqlStr As String = "SELECT vrds.*" &
            " FROM veicoli_stato_rds_variazione vrds WITH(NOLOCK)" &
            " INNER JOIN (" &
            " SELECT MAX(id) max_id " &
            " FROM veicoli_stato_rds_variazione WITH(NOLOCK)" &
            " WHERE id_evento = @id_evento" &
            " AND id_stato = @id_stato" &
            " ) m ON vrds.id = m.max_id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_evento", System.Data.SqlDbType.Int, id_evento)
                addParametro(Cmd, "@id_stato", System.Data.SqlDbType.Int, sessione_danni.stato_rds.Stampa_lettera_al_cliente)
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

    'Public Shared Function getUltimaStimaImporto(id_evento As Integer, importo As Double, codice_iva As Integer) As veicoli_stato_rds_variazione
    '    Dim mio_record As veicoli_stato_rds_variazione = Nothing

    '    Dim sqlStr As String = "SELECT vrds.*" & _
    '        " FROM veicoli_stato_rds_variazione vrds WITH(NOLOCK)" & _
    '        " INNER JOIN (" & _
    '        " SELECT MAX(id) max_id " & _
    '        " FROM veicoli_stato_rds_variazione WITH(NOLOCK)" & _
    '        " WHERE id_evento = @id_evento" & _
    '        " AND id_stato = @id_stato" & _
    '        " AND importo = @importo" & _
    '        " AND iva = @iva" & _
    '        " ) m ON vrds.id = m.max_id"

    '    Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '            addParametro(Cmd, "@id_evento", System.Data.SqlDbType.Int, id_evento)
    '            addParametro(Cmd, "@id_stato", System.Data.SqlDbType.Int, stato_rds.Da_addebitare)
    '            addParametro(Cmd, "@importo", System.Data.SqlDbType.Real, importo)
    '            addParametro(Cmd, "@iva", System.Data.SqlDbType.Int, codice_iva)
    '            Dbc.Open()
    '            Using Rs = Cmd.ExecuteReader
    '                If Rs.Read Then
    '                    mio_record = FillRecord(Rs)
    '                End If
    '            End Using
    '        End Using
    '    End Using

    '    Return mio_record
    'End Function

    'Public Function AggiornaRecord() As Boolean
    '    AggiornaRecord = False

    '    Dim sqlStr As String = "UPDATE veicoli_stato_rds_variazione SET" & _
    '        " id_evento = @id_evento," & _
    '        " id_stato_precedente = @id_stato_precedente," & _
    '        " id_stato = @id_stato," & _
    '        " importo = @importo," & _
    '        " iva = @iva," & _
    '        " spese_postali = @spese_postali," & _
    '        " incasso = @incasso," & _
    '        " att_manutenzione = @att_manutenzione," & _
    '        " att_documentazione = @att_documentazione" & _
    '        " WHERE id = @id"

    '    Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '            addParametro(Cmd, "@id_evento", System.Data.SqlDbType.Int, id_evento)
    '            addParametro(Cmd, "@id_stato_precedente", System.Data.SqlDbType.Int, id_stato_precedente)
    '            addParametro(Cmd, "@id_stato", System.Data.SqlDbType.Int, id_stato)
    '            addParametro(Cmd, "@importo", System.Data.SqlDbType.Real, importo)
    '            addParametro(Cmd, "@iva", System.Data.SqlDbType.Int, iva)
    '            addParametro(Cmd, "@spese_postali", System.Data.SqlDbType.Real, spese_postali)
    '            addParametro(Cmd, "@incasso", System.Data.SqlDbType.Real, incasso)
    '            addParametro(Cmd, "@att_manutenzione", System.Data.SqlDbType.Bit, att_manutenzione)
    '            addParametro(Cmd, "@att_documentazione", System.Data.SqlDbType.Bit, att_documentazione)

    '            addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

    '            Dbc.Open()
    '            Cmd.ExecuteNonQuery()
    '            AggiornaRecord = True
    '        End Using
    '    End Using
    'End Function

    Public Function EliminaRecord() As Boolean
        Return EliminaRecord(Me.id)
    End Function

    Public Shared Function EliminaRecord(ByVal id_record As Integer) As Boolean
        EliminaRecord = False

        Dim sqlStr As String = "DELETE FROM veicoli_stato_rds_variazione WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord veicoli_stato_rds_variazione: " & ex.Message)
        End Try
    End Function
End Class

Public Class veicoli_evento_chiusura_danno
    Inherits ITabellaDB

    Const NomeTabella = "[veicoli_evento_chiusura_danno]"

    Protected m_id As Integer
    Protected m_id_veicolo As Integer
    Protected m_id_tipo_documento_chiusura As Integer
    Protected m_id_documento_chiusura As Integer?
    Protected m_data As Date?
    Protected m_nota As String
    Protected m_data_creazione As DateTime
    Protected m_id_utente As Integer
    Protected m_data_modifica As DateTime?
    Protected m_id_utente_modifica As Integer?

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property id_veicolo() As Integer
        Get
            Return m_id_veicolo
        End Get
        Set(ByVal value As Integer)
            m_id_veicolo = value
        End Set
    End Property
    Public Property id_tipo_documento_chiusura() As Integer
        Get
            Return m_id_tipo_documento_chiusura
        End Get
        Set(ByVal value As Integer)
            m_id_tipo_documento_chiusura = value
        End Set
    End Property
    Public Property id_documento_chiusura() As Integer?
        Get
            Return m_id_documento_chiusura
        End Get
        Set(ByVal value As Integer?)
            m_id_documento_chiusura = value
        End Set
    End Property
    Public Property data() As DateTime?
        Get
            Return m_data
        End Get
        Set(ByVal value As DateTime?)
            m_data = value
        End Set
    End Property
    Public Property nota() As String
        Get
            Return m_nota
        End Get
        Set(ByVal value As String)
            m_nota = value
        End Set
    End Property
    Public ReadOnly Property data_creazione() As DateTime
        Get
            Return m_data_creazione
        End Get
    End Property
    Public ReadOnly Property id_utente() As Integer
        Get
            Return m_id_utente
        End Get
    End Property
    Public ReadOnly Property data_modifica() As DateTime
        Get
            Return m_data_modifica
        End Get
    End Property
    Public ReadOnly Property id_utente_modifica() As Integer
        Get
            Return m_id_utente_modifica
        End Get
    End Property


    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO " & NomeTabella & " (id_veicolo,id_tipo_documento_chiusura,id_documento_chiusura,data,nota,data_creazione,id_utente)" &
            " VALUES (@id_veicolo,@id_tipo_documento_chiusura,@id_documento_chiusura,@data,@nota,@data_creazione,@id_utente)"

        m_data_creazione = Now
        m_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                addParametro(Cmd, "@id_tipo_documento_chiusura", System.Data.SqlDbType.Int, id_tipo_documento_chiusura)
                addParametro(Cmd, "@id_documento_chiusura", System.Data.SqlDbType.Int, id_documento_chiusura)
                addParametro(Cmd, "@data", System.Data.SqlDbType.Date, data)
                addParametro(Cmd, "@nota", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nota))
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM " & NomeTabella

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_evento_chiusura_danno
        Dim mio_record As veicoli_evento_chiusura_danno = New veicoli_evento_chiusura_danno
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .id_veicolo = getValueOrNohing(Rs("id_veicolo"))
            .id_tipo_documento_chiusura = getValueOrNohing(Rs("id_tipo_documento_chiusura"))
            .id_documento_chiusura = getValueOrNohing(Rs("id_documento_chiusura"))
            .data = getValueOrNohing(Rs("data"))
            .nota = getValueOrNohing(Rs("nota"))
            .m_data_creazione = getValueOrNohing(Rs("data_creazione"))
            .m_id_utente = getValueOrNohing(Rs("id_utente"))
            .m_data_modifica = getValueOrNohing(Rs("data_modifica"))
            .m_id_utente_modifica = getValueOrNohing(Rs("id_utente_modifica"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As veicoli_evento_chiusura_danno
        Dim mio_record As veicoli_evento_chiusura_danno = Nothing

        Dim sqlStr As String = "SELECT * FROM " & NomeTabella & " WITH(NOLOCK) WHERE id = " & id_record

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

    ' Devo gestire l'aggiornamento e la cancellazione.... sino a quando non ci siano record collegati...
    Public Function AggiornaRecord() As Boolean
        AggiornaRecord = False

        Dim sqlStr As String = "UPDATE " & NomeTabella & " SET" &
            " id_veicolo = @id_veicolo," &
            " id_tipo_documento_chiusura = @id_tipo_documento_chiusura," &
            " id_documento_chiusura = @id_documento_chiusura," &
            " data = @data," &
            " nota = @nota," &
            " data_modifica = @data_modifica," &
            " id_utente_modifica = @id_utente_modifica" &
            " WHERE id = @id"

        m_data_modifica = Now
        m_id_utente_modifica = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                addParametro(Cmd, "@id_tipo_documento_chiusura", System.Data.SqlDbType.Int, id_tipo_documento_chiusura)
                addParametro(Cmd, "@id_documento_chiusura", System.Data.SqlDbType.Int, id_documento_chiusura)
                addParametro(Cmd, "@data", System.Data.SqlDbType.Date, data)
                addParametro(Cmd, "@nota", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nota))
                addParametro(Cmd, "@data_modifica", System.Data.SqlDbType.DateTime, data_modifica)
                addParametro(Cmd, "@id_utente_modifica", System.Data.SqlDbType.Int, id_utente_modifica)

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

        Dim sqlStr As String = "DELETE FROM " & NomeTabella & " WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord " & NomeTabella & ": " & ex.Message)
        End Try
    End Function
End Class

Public Enum tipo_record_danni
    Non_Definito = 0
    Danno_Carrozzeria = 1
    Danno_Meccanico = 2
    Danno_Elettrico = 3
    Dotazione = 4
    Accessori = 5
    Furto = 6
    Altro = 7
End Enum

Public Class veicoli_danni
    Inherits ITabellaDB

    ' Const NomeTabella = "[veicoli_danni]"

    Protected m_id As Integer
    Protected m_attivo As Boolean
    Protected m_id_veicolo As Integer
    Protected m_id_evento_apertura As Integer
    Protected m_stato As Integer?
    Protected m_num_odl As Integer?
    Protected m_tipo_record As Integer?
    Protected m_data_creazione As DateTime
    Protected m_id_utente As Integer
    Protected m_data_modifica As DateTime?
    Protected m_id_utente_modifica As Integer?
    Protected m_id_posizione_danno As Integer?
    Protected m_id_tipo_danno As Integer?
    Protected m_entita_danno As Integer?
    Protected m_descrizione As String
    Protected m_id_dotazione As Integer?
    Protected m_id_acessori As Integer?
    Protected m_id_evento_chiusura As Integer?
    Protected m_data_chiusura As DateTime?
    Protected m_id_utente_chiusura As Integer?
    Protected m_stato_rds As Integer? = 1
    Protected m_id_ditta As Integer?
    Protected m_importo As Double? = Nothing
    Protected m_da_addebitare As Boolean?
    Protected m_motivo_non_addebito As Integer?
    Protected m_id_utente_addebito As Integer?
    Protected m_id_data_addebito As DateTime?


    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property attivo() As Boolean
        Get
            Return m_attivo
        End Get
        Set(ByVal value As Boolean)
            m_attivo = value
        End Set
    End Property
    Public Property id_veicolo() As Integer
        Get
            Return m_id_veicolo
        End Get
        Set(ByVal value As Integer)
            m_id_veicolo = value
        End Set
    End Property
    Public Property id_evento_apertura() As Integer
        Get
            Return m_id_evento_apertura
        End Get
        Set(ByVal value As Integer)
            m_id_evento_apertura = value
        End Set
    End Property
    Public ReadOnly Property stato() As Integer?
        Get
            Return m_stato
        End Get
    End Property
    Public Property num_odl() As Integer?
        Get
            Return m_num_odl
        End Get
        Set(ByVal value As Integer?)
            m_num_odl = value
        End Set
    End Property
    Public Property tipo_record() As tipo_record_danni?
        Get
            Return m_tipo_record
        End Get
        Set(ByVal value As tipo_record_danni?)
            m_tipo_record = value
        End Set
    End Property
    Public ReadOnly Property data_creazione() As DateTime
        Get
            Return m_data_creazione
        End Get
    End Property
    Public ReadOnly Property id_utente() As Integer
        Get
            Return m_id_utente
        End Get
    End Property
    Public ReadOnly Property data_modifica() As DateTime?
        Get
            Return m_data_modifica
        End Get
    End Property
    Public ReadOnly Property id_utente_modifica() As Integer?
        Get
            Return m_id_utente_modifica
        End Get
    End Property
    Public Property id_posizione_danno() As Integer?
        Get
            Return m_id_posizione_danno
        End Get
        Set(ByVal value As Integer?)
            m_id_posizione_danno = value
        End Set
    End Property
    Public Property id_tipo_danno() As Integer?
        Get
            Return m_id_tipo_danno
        End Get
        Set(ByVal value As Integer?)
            m_id_tipo_danno = value
        End Set
    End Property
    Public Property entita_danno() As Integer?
        Get
            Return m_entita_danno
        End Get
        Set(ByVal value As Integer?)
            m_entita_danno = value
        End Set
    End Property
    Public Property descrizione() As String
        Get
            Return m_descrizione
        End Get
        Set(ByVal value As String)
            m_descrizione = value
        End Set
    End Property
    Public Property id_dotazione() As Integer?
        Get
            Return m_id_dotazione
        End Get
        Set(ByVal value As Integer?)
            m_id_dotazione = value
        End Set
    End Property
    Public Property id_acessori() As Integer?
        Get
            Return m_id_acessori
        End Get
        Set(ByVal value As Integer?)
            m_id_acessori = value
        End Set
    End Property
    Public Property id_evento_chiusura() As Integer?
        Get
            Return m_id_evento_chiusura
        End Get
        Set(ByVal value As Integer?)
            m_id_evento_chiusura = value
        End Set
    End Property
    Public ReadOnly Property data_chiusura() As DateTime?
        Get
            Return m_data_chiusura
        End Get
    End Property
    Public ReadOnly Property id_utente_chiusura() As Integer?
        Get
            Return m_id_utente_chiusura
        End Get
    End Property
    Public Property stato_rds() As Integer?
        Get
            Return m_stato_rds
        End Get
        Set(ByVal value As Integer?)
            m_stato_rds = value
        End Set
    End Property
    Public Property id_ditta() As Integer?
        Get
            Return m_id_ditta
        End Get
        Set(ByVal value As Integer?)
            m_id_ditta = value
        End Set
    End Property
    Public Property importo() As Double?
        Get
            Return m_importo
        End Get
        Set(ByVal value As Double?)
            m_importo = value
        End Set
    End Property
    Public Property da_addebitare() As Boolean?
        Get
            Return m_da_addebitare
        End Get
        Set(ByVal value As Boolean?)
            m_da_addebitare = value
        End Set
    End Property
    Public Property motivo_non_addebito() As Integer?
        Get
            Return m_motivo_non_addebito
        End Get
        Set(ByVal value As Integer?)
            m_motivo_non_addebito = value
        End Set
    End Property
    Public ReadOnly Property id_utente_addebito() As Integer?
        Get
            Return m_id_utente_addebito
        End Get
    End Property
    Public ReadOnly Property id_data_addebito() As DateTime?
        Get
            Return m_id_data_addebito
        End Get
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO [veicoli_danni] (id_veicolo,attivo,id_evento_apertura,stato,num_odl,tipo_record,data_creazione,id_utente,id_posizione_danno,id_tipo_danno,entita_danno,descrizione,id_dotazione,id_acessori,stato_rds,id_ditta,importo,da_addebitare,motivo_non_addebito,id_utente_addebito,id_data_addebito)" &
            " VALUES (@id_veicolo,@attivo,@id_evento_apertura,@stato,@num_odl,@tipo_record,@data_creazione,@id_utente,@id_posizione_danno,@id_tipo_danno,@entita_danno,@descrizione,@id_dotazione,@id_acessori,@stato_rds,@id_ditta,@importo,@da_addebitare,@motivo_non_addebito,@id_utente_addebito,@id_data_addebito)"

        m_stato = stato_danno.aperto
        m_data_creazione = Now
        m_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Try

            HttpContext.Current.Session("glidposdanno") = id_posizione_danno
            'id_posizione_danno = HttpContext.Current.Session("glidposdanno")
            If id_posizione_danno = 0 Then
                HttpContext.Current.Response.Write("error  SalvaRecord: <br/>Selezionare Posizione Danno<br/>" & sqlStr & "<br/>")
            End If



            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                    addParametro(Cmd, "@attivo", System.Data.SqlDbType.Bit, attivo)
                    addParametro(Cmd, "@id_evento_apertura", System.Data.SqlDbType.Int, id_evento_apertura)
                    addParametro(Cmd, "@stato", System.Data.SqlDbType.Int, stato)
                    addParametro(Cmd, "@num_odl", System.Data.SqlDbType.Int, num_odl)
                    addParametro(Cmd, "@tipo_record", System.Data.SqlDbType.Int, tipo_record)
                    addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                    addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)
                    addParametro(Cmd, "@id_posizione_danno", System.Data.SqlDbType.Int, id_posizione_danno)
                    addParametro(Cmd, "@id_tipo_danno", System.Data.SqlDbType.Int, id_tipo_danno)
                    addParametro(Cmd, "@entita_danno", System.Data.SqlDbType.Int, entita_danno)
                    addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione))
                    addParametro(Cmd, "@id_dotazione", System.Data.SqlDbType.Int, id_dotazione)
                    addParametro(Cmd, "@id_acessori", System.Data.SqlDbType.Int, id_acessori)
                    addParametro(Cmd, "@stato_rds", System.Data.SqlDbType.Int, stato_rds)
                    addParametro(Cmd, "@id_ditta", System.Data.SqlDbType.Int, id_ditta)
                    addParametro(Cmd, "@importo", System.Data.SqlDbType.Real, importo)
                    addParametro(Cmd, "@da_addebitare", System.Data.SqlDbType.Bit, da_addebitare)
                    addParametro(Cmd, "@motivo_non_addebito", System.Data.SqlDbType.Int, motivo_non_addebito)
                    addParametro(Cmd, "@id_utente_addebito", System.Data.SqlDbType.Int, id_utente_addebito)
                    addParametro(Cmd, "@id_data_addebito", System.Data.SqlDbType.DateTime, id_data_addebito)

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                End Using

                ' -------------------------------------------------------------------------
                'recupero l'id del nuopvo record comunque...
                sqlStr = "SELECT @@IDENTITY FROM [veicoli_danni]"

                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    m_id = Cmd.ExecuteScalar
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Response.Write("error  SalvaRecord: <br/>" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try





        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_danni
        Dim mio_record As veicoli_danni = New veicoli_danni
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .attivo = getValueOrNohing(Rs("attivo"))
            .id_veicolo = getValueOrNohing(Rs("id_veicolo"))
            .id_evento_apertura = getValueOrNohing(Rs("id_evento_apertura"))
            .m_stato = getValueOrNohing(Rs("stato"))
            .num_odl = getValueOrNohing(Rs("num_odl"))
            .tipo_record = CType(getValueOrNohing(Rs("tipo_record")), Integer?)
            .m_data_creazione = getValueOrNohing(Rs("data_creazione"))
            .m_id_utente = getValueOrNohing(Rs("id_utente"))
            .m_data_modifica = getValueOrNohing(Rs("data_modifica"))
            .m_id_utente_modifica = getValueOrNohing(Rs("id_utente_modifica"))
            .id_posizione_danno = getValueOrNohing(Rs("id_posizione_danno"))
            .id_tipo_danno = getValueOrNohing(Rs("id_tipo_danno"))
            .entita_danno = getValueOrNohing(Rs("entita_danno"))
            .descrizione = getValueOrNohing(Rs("descrizione"))
            .id_dotazione = getValueOrNohing(Rs("id_dotazione"))
            .id_acessori = getValueOrNohing(Rs("id_acessori"))
            .id_evento_chiusura = getValueOrNohing(Rs("id_evento_chiusura"))
            .m_data_chiusura = getValueOrNohing(Rs("data_chiusura"))
            .m_id_utente_chiusura = getValueOrNohing(Rs("id_utente_chiusura"))
            .stato_rds = getValueOrNohing(Rs("stato_rds"))
            .id_ditta = getValueOrNohing(Rs("id_ditta"))
            .importo = getValueOrNohing(Rs("importo"))
            .da_addebitare = getValueOrNohing(Rs("da_addebitare"))
            .motivo_non_addebito = getValueOrNohing(Rs("motivo_non_addebito"))
            .m_id_utente_addebito = getValueOrNohing(Rs("id_utente_addebito"))
            .m_id_data_addebito = getValueOrNohing(Rs("id_data_addebito"))

        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As veicoli_danni
        Dim mio_record As veicoli_danni = Nothing

        Dim sqlStr As String = "SELECT * FROM [veicoli_danni] WITH(NOLOCK) WHERE id = " & id_record

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

    Public Shared Function getDannoFurto(ByVal id_evento As Integer) As veicoli_danni
        Dim mio_record As veicoli_danni = Nothing
        Dim sqlStr As String
        sqlStr = "SELECT * FROM veicoli_danni WITH(NOLOCK)" &
            " WHERE id_evento_apertura = @id_evento_apertura" &
            " AND attivo = 1" &
            " AND tipo_record = @tipo_record" &
            " AND stato = 1"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_evento_apertura", System.Data.SqlDbType.Int, id_evento)
                addParametro(Cmd, "@tipo_record", System.Data.SqlDbType.Int, tipo_record_danni.Furto)

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

        Dim sqlStr As String = "UPDATE [veicoli_danni] SET" &
            " attivo = @attivo," &
            " data_modifica = @data_modifica," &
            " id_utente_modifica = @id_utente_modifica," &
            " id_posizione_danno = @id_posizione_danno," &
            " id_tipo_danno = @id_tipo_danno," &
            " entita_danno = @entita_danno," &
            " id_dotazione = @id_dotazione," &
            " id_acessori = @id_acessori," &
            " descrizione = @descrizione," &
            " stato_rds = @stato_rds," &
            " id_ditta = @id_ditta," &
            " importo = @importo" &
            " WHERE id = @id"

        m_data_modifica = Now
        m_id_utente_modifica = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@attivo", System.Data.SqlDbType.Bit, attivo)
                addParametro(Cmd, "@data_modifica", System.Data.SqlDbType.DateTime, data_modifica)
                addParametro(Cmd, "@id_utente_modifica", System.Data.SqlDbType.Int, id_utente_modifica)
                addParametro(Cmd, "@id_posizione_danno", System.Data.SqlDbType.Int, id_posizione_danno)
                addParametro(Cmd, "@id_tipo_danno", System.Data.SqlDbType.Int, id_tipo_danno)
                addParametro(Cmd, "@entita_danno", System.Data.SqlDbType.Int, entita_danno)
                addParametro(Cmd, "@id_dotazione", System.Data.SqlDbType.Int, id_dotazione)
                addParametro(Cmd, "@id_acessori", System.Data.SqlDbType.Int, id_acessori)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione))
                addParametro(Cmd, "@stato_rds", System.Data.SqlDbType.Int, stato_rds)
                addParametro(Cmd, "@id_ditta", System.Data.SqlDbType.Int, id_ditta)
                addParametro(Cmd, "@importo", System.Data.SqlDbType.Real, importo)

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaRecord = True
            End Using
        End Using
    End Function

    Public Function AssociaODL(ByVal num_odl As Integer) As Boolean
        Return AssociaODL(id, num_odl)
    End Function

    Public Shared Function AssociaODL(ByVal id_danno As Integer, ByVal num_odl As Integer?) As Boolean
        AssociaODL = False

        Dim sqlStr As String = "UPDATE [veicoli_danni] SET" &
            " data_modifica = @data_modifica," &
            " id_utente_modifica = @id_utente_modifica," &
            " num_odl = @num_odl" &
            " WHERE id = @id"

        Dim data_modifica As Date = Now
        Dim id_utente_modifica As Integer = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@data_modifica", System.Data.SqlDbType.DateTime, data_modifica)
                addParametro(Cmd, "@id_utente_modifica", System.Data.SqlDbType.Int, id_utente_modifica)
                addParametro(Cmd, "@num_odl", System.Data.SqlDbType.Int, num_odl)
                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id_danno)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AssociaODL = True
            End Using
        End Using
    End Function

    Public Function ChiudiDanno() As Boolean
        ChiudiDanno = False

        Dim sqlStr As String = "UPDATE [veicoli_danni] SET" &
            " stato = @stato," &
            " num_odl = @num_odl," &
            " id_evento_chiusura = @id_evento_chiusura," &
            " data_chiusura = @data_chiusura," &
            " id_utente_chiusura = @id_utente_chiusura" &
            " WHERE id = @id"

        m_stato = stato_danno.chiuso
        m_data_chiusura = Now
        m_id_utente_chiusura = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@stato", System.Data.SqlDbType.Int, stato)
                addParametro(Cmd, "@num_odl", System.Data.SqlDbType.Int, num_odl)
                addParametro(Cmd, "@id_evento_chiusura", System.Data.SqlDbType.Int, id_evento_chiusura)
                addParametro(Cmd, "@data_chiusura", System.Data.SqlDbType.DateTime, data_chiusura)
                addParametro(Cmd, "@id_utente_chiusura", System.Data.SqlDbType.Int, id_utente_chiusura)

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                ChiudiDanno = True
            End Using
        End Using
    End Function

    Public Shared Function DeselezionaDanno(ByVal id_danno As Integer) As Boolean
        DeselezionaDanno = False

        Dim sqlStr As String = "UPDATE [veicoli_danni] SET" &
            " num_odl = NULL" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id_danno)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                DeselezionaDanno = True
            End Using
        End Using
    End Function

    Public Function RipristinaDanno() As Boolean
        RipristinaDanno = False

        Dim sqlStr As String = "UPDATE [veicoli_danni] SET" &
            " stato = @stato," &
            " id_evento_chiusura = @id_evento_chiusura," &
            " data_chiusura = @data_chiusura," &
            " id_utente_chiusura = @id_utente_chiusura" &
            " WHERE id = @id"

        m_stato = stato_danno.aperto
        m_data_chiusura = Nothing
        m_id_utente_chiusura = Nothing
        id_evento_chiusura = Nothing

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@stato", System.Data.SqlDbType.Int, stato)
                addParametro(Cmd, "@id_evento_chiusura", System.Data.SqlDbType.Int, id_evento_chiusura)
                addParametro(Cmd, "@data_chiusura", System.Data.SqlDbType.DateTime, data_chiusura)
                addParametro(Cmd, "@id_utente_chiusura", System.Data.SqlDbType.Int, id_utente_chiusura)

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                RipristinaDanno = True
            End Using
        End Using
    End Function

    Public Function DaAddebitare(ByVal da_addebitare As Boolean) As Boolean
        Return DaAddebitare(id, da_addebitare, motivo_non_addebito)
    End Function

    Public Shared Function DaAddebitare(ByVal id_danno As Integer, ByVal da_addebitare As Boolean, ByVal motivo_non_addebito As Integer) As Boolean
        DaAddebitare = False
        Dim sqlStr As String = "UPDATE veicoli_danni SET" &
            " da_addebitare = @da_addebitare," &
            " motivo_non_addebito = @motivo_non_addebito," &
            " id_data_addebito = @id_data_addebito," &
            " id_utente_addebito = @id_utente_addebito" &
            " WHERE id = @id_danno"

        HttpContext.Current.Trace.Write(sqlStr & " " & id_danno & " " & da_addebitare)
        Dim id_data_addebito As DateTime = Now
        Dim id_utente_addebito As Integer = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@da_addebitare", System.Data.SqlDbType.Bit, da_addebitare)
                addParametro(Cmd, "@motivo_non_addebito", System.Data.SqlDbType.Int, motivo_non_addebito)
                addParametro(Cmd, "@id_data_addebito", System.Data.SqlDbType.DateTime, id_data_addebito)
                addParametro(Cmd, "@id_utente_addebito", System.Data.SqlDbType.Int, id_utente_addebito)
                addParametro(Cmd, "@id_danno", System.Data.SqlDbType.Int, id_danno)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                DaAddebitare = True
            End Using
        End Using
    End Function

    Public Shared Function DaAddebitareDotazione(ByVal id_evento_apertura As Integer, ByVal id_dotazione As Integer, ByVal addebitare As Boolean, ByVal motivo_non_addebito As Integer) As Boolean
        DaAddebitareDotazione = False
        Dim sqlStr As String = "UPDATE veicoli_danni SET" &
            " da_addebitare = @da_addebitare," &
            " motivo_non_addebito = @motivo_non_addebito," &
            " id_data_addebito = @id_data_addebito," &
            " id_utente_addebito = @id_utente_addebito" &
            " WHERE attivo = 1" &
            " AND id_evento_apertura = @id_evento_apertura" &
            " AND id_dotazione = @id_dotazione"

        Dim id_data_addebito As DateTime = Now
        Dim id_utente_addebito As Integer = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_evento_apertura", System.Data.SqlDbType.Int, id_evento_apertura)
                addParametro(Cmd, "@id_dotazione", System.Data.SqlDbType.Int, id_dotazione)
                addParametro(Cmd, "@da_addebitare", System.Data.SqlDbType.Bit, addebitare)
                addParametro(Cmd, "@motivo_non_addebito", System.Data.SqlDbType.Int, motivo_non_addebito)
                addParametro(Cmd, "@id_data_addebito", System.Data.SqlDbType.DateTime, id_data_addebito)
                addParametro(Cmd, "@id_utente_addebito", System.Data.SqlDbType.Int, id_utente_addebito)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                DaAddebitareDotazione = True
            End Using
        End Using
    End Function

    Public Shared Function DaAddebitareAccessorio(ByVal id_evento_apertura As Integer, ByVal id_acessori As Integer, ByVal addebitare As Boolean, ByVal motivo_non_addebito As Integer) As Boolean
        DaAddebitareAccessorio = False
        Dim sqlStr As String = "UPDATE veicoli_danni SET" &
            " da_addebitare = @da_addebitare," &
            " motivo_non_addebito = @motivo_non_addebito," &
            " id_data_addebito = @id_data_addebito," &
            " id_utente_addebito = @id_utente_addebito" &
            " WHERE attivo = 1" &
            " AND id_evento_apertura = @id_evento_apertura" &
            " AND id_acessori = @id_acessori"

        HttpContext.Current.Trace.Write("declare @id_evento_apertura int; set @id_evento_apertura=" & id_evento_apertura & ";" & "declare @id_acessori int; set @id_acessori=" & id_evento_apertura & ";" & sqlStr)
        Dim id_data_addebito As DateTime = Now
        Dim id_utente_addebito As Integer = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_evento_apertura", System.Data.SqlDbType.Int, id_evento_apertura)
                addParametro(Cmd, "@id_acessori", System.Data.SqlDbType.Int, id_acessori)
                addParametro(Cmd, "@da_addebitare", System.Data.SqlDbType.Bit, addebitare)
                addParametro(Cmd, "@motivo_non_addebito", System.Data.SqlDbType.Int, motivo_non_addebito)
                addParametro(Cmd, "@id_data_addebito", System.Data.SqlDbType.DateTime, id_data_addebito)
                addParametro(Cmd, "@id_utente_addebito", System.Data.SqlDbType.Int, id_utente_addebito)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                DaAddebitareAccessorio = True
            End Using
        End Using
    End Function

    Public Shared Function AttivaDanno(ByVal id_danno As Integer) As Boolean
        AttivaDanno = False

        Dim sqlStr As String = "UPDATE veicoli_danni SET" &
            " attivo = 1" &
            " WHERE id = @id_danno"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_danno", System.Data.SqlDbType.Int, id_danno)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AttivaDanno = True
            End Using
        End Using
    End Function
End Class

Public Class veicoli_danni_foto
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_id_danno As Integer
    Protected m_data_creazione As Nullable(Of DateTime)
    Protected m_tipo_documento As Integer?
    Protected m_descrizione As String
    Protected m_riferimento_foto As String

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property id_danno() As Integer
        Get
            Return m_id_danno
        End Get
        Set(ByVal value As Integer)
            m_id_danno = value
        End Set
    End Property
    Public ReadOnly Property data_creazione() As Nullable(Of DateTime)
        Get
            Return m_data_creazione
        End Get
    End Property
    Public Property tipo_documento() As Integer?
        Get
            Return m_tipo_documento
        End Get
        Set(ByVal value As Integer?)
            m_tipo_documento = value
        End Set
    End Property
    Public Property descrizione() As String
        Get
            Return m_descrizione
        End Get
        Set(ByVal value As String)
            m_descrizione = value
        End Set
    End Property
    Public Property riferimento_foto() As String
        Get
            Return m_riferimento_foto
        End Get
        Set(ByVal value As String)
            m_riferimento_foto = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO veicoli_danni_foto (id_danno,data_creazione,tipo_documento,descrizione,riferimento_foto)" &
            " VALUES (@id_danno,@data_creazione,@tipo_documento,@descrizione,@riferimento_foto)"

        m_data_creazione = Now

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_danno", System.Data.SqlDbType.Int, id_danno)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@tipo_documento", System.Data.SqlDbType.Int, tipo_documento)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@riferimento_foto", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(riferimento_foto, 256))

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM veicoli_danni_foto"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillFoto(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_danni_foto
        Dim mia_foto As veicoli_danni_foto = New veicoli_danni_foto
        With mia_foto
            .id = Rs("id")
            .id_danno = Rs("id_danno")
            .m_data_creazione = Rs("data_creazione")
            .tipo_documento = Rs("tipo_documento")
            .descrizione = Rs("descrizione")
            .riferimento_foto = Rs("riferimento_foto")
        End With
        Return mia_foto
    End Function

    Public Shared Function get_foto_da_id(ByVal id_foto As Integer) As veicoli_danni_foto
        Dim mia_foto As veicoli_danni_foto = Nothing

        Dim sqlStr As String = "SELECT * FROM veicoli_danni_foto WITH(NOLOCK) WHERE id = " & id_foto

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    If Rs.Read Then
                        mia_foto = FillFoto(Rs)
                    End If
                End Using
            End Using
        End Using

        Return mia_foto
    End Function

    Public Function CancellaFoto() As Boolean
        Return CancellaFoto(id)
    End Function

    Public Shared Function CancellaFoto(ByVal id_foto As Integer) As Boolean
        CancellaFoto = False
        Dim sqlStr As String = "DELETE FROM veicoli_danni_foto WHERE id = " & id_foto

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    CancellaFoto = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("CancellaFoto: " & ex.Message)
        End Try
    End Function

End Class

Public Class veicoli_danni_evento_foto
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_id_evento As Integer
    Protected m_data_creazione As Nullable(Of DateTime)
    Protected m_tipo_documento As Integer?
    Protected m_descrizione As String
    Protected m_riferimento_foto As String

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property id_evento() As Integer
        Get
            Return m_id_evento
        End Get
        Set(ByVal value As Integer)
            m_id_evento = value
        End Set
    End Property
    Public ReadOnly Property data_creazione() As Nullable(Of DateTime)
        Get
            Return m_data_creazione
        End Get
    End Property
    Public Property tipo_documento() As Integer?
        Get
            Return m_tipo_documento
        End Get
        Set(ByVal value As Integer?)
            m_tipo_documento = value
        End Set
    End Property
    Public Property descrizione() As String
        Get
            Return m_descrizione
        End Get
        Set(ByVal value As String)
            m_descrizione = value
        End Set
    End Property
    Public Property riferimento_foto() As String
        Get
            Return m_riferimento_foto
        End Get
        Set(ByVal value As String)
            m_riferimento_foto = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO veicoli_danni_evento_foto (id_evento,data_creazione,tipo_documento,descrizione,riferimento_foto)" &
            " VALUES (@id_evento,@data_creazione,@tipo_documento,@descrizione,@riferimento_foto)"

        m_data_creazione = Now

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_evento", System.Data.SqlDbType.Int, id_evento)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@tipo_documento", System.Data.SqlDbType.Int, tipo_documento)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@riferimento_foto", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(riferimento_foto, 256))

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM veicoli_danni_evento_foto"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillFoto(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_danni_evento_foto
        Dim mia_foto As veicoli_danni_evento_foto = New veicoli_danni_evento_foto
        With mia_foto
            .id = Rs("id")
            .id_evento = Rs("id_evento")
            .m_data_creazione = Rs("data_creazione")
            .tipo_documento = Rs("tipo_documento")
            .descrizione = Rs("descrizione")
            .riferimento_foto = Rs("riferimento_foto")
        End With
        Return mia_foto
    End Function

    Public Shared Function get_foto_da_id(ByVal id_foto As Integer) As veicoli_danni_evento_foto
        Dim mia_foto As veicoli_danni_evento_foto = Nothing

        Dim sqlStr As String = "SELECT * FROM veicoli_danni_evento_foto WITH(NOLOCK) WHERE id = " & id_foto

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    If Rs.Read Then
                        mia_foto = FillFoto(Rs)
                    End If
                End Using
            End Using
        End Using

        Return mia_foto
    End Function

    Public Function CancellaFoto() As Boolean
        Return CancellaFoto(id)
    End Function

    Public Shared Function CancellaFoto(ByVal id_foto As Integer) As Boolean
        CancellaFoto = False
        Dim sqlStr As String = "DELETE FROM veicoli_danni_evento_foto WHERE id = " & id_foto

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    CancellaFoto = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("CancellaFoto: " & ex.Message)
        End Try
    End Function

End Class

'Public Class gestione_mail_per_danno
'    Protected Shared Function getPosizioneDannoDaId(id_posizione As Integer) As String
'        Dim sqlStr As String
'        sqlStr = "SELECT descrizione FROM veicoli_posizione_danno WHERE id = " & id_posizione
'        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
'            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
'                Dbc.Open()
'                getPosizioneDannoDaId = Cmd.ExecuteScalar & ""
'            End Using
'        End Using
'    End Function

'    Protected Shared Function getTargaDaId(id_veicolo As Integer) As String
'        Dim sqlStr As String
'        sqlStr = "SELECT targa FROM veicoli WHERE id = " & id_veicolo
'        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
'            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
'                Dbc.Open()
'                getTargaDaId = Cmd.ExecuteScalar & ""
'            End Using
'        End Using
'    End Function

'    Protected Shared Function getTipoDocumentoDannoDaId(id_tipo_documento As Integer) As String
'        Dim sqlStr As String
'        sqlStr = "SELECT descrizione FROM veicoli_tipo_documento_apertura_danno WHERE id = " & id_tipo_documento
'        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
'            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
'                Dbc.Open()
'                getTipoDocumentoDannoDaId = Cmd.ExecuteScalar & ""
'            End Using
'        End Using
'    End Function

'    Protected Shared Function getNumeroContrattoDaId(id_contratto As Integer) As String
'        Dim sqlStr As String
'        sqlStr = "SELECT num_contratto FROM contratti WHERE id = " & id_contratto
'        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
'            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
'                Dbc.Open()
'                getNumeroContrattoDaId = Cmd.ExecuteScalar & ""
'            End Using
'        End Using
'    End Function

'    Public Shared Sub inviaMailPerDanno(mio_danno As veicoli_danni)
'        Dim mail As New MailMessage()

'        Dim corpoMessaggio As String
'        With mio_danno
'            Dim targa As String = getTargaDaId(.id_veicolo)
'            Dim posizione_danno As String = getPosizioneDannoDaId(.id_posizione_danno)
'            If posizione_danno = "" Then
'                posizione_danno = "(N.V. " & .id_posizione_danno & ")"
'            End If
'            Dim tipo_documento_apertura As String = getTipoDocumentoDannoDaId(.id)
'            If tipo_documento_apertura = "" Then
'                tipo_documento_apertura = "(N.V. " & .id & ")"
'            End If

'            'Dichiato il mittente
'            mail.From = New MailAddress("noreply@sbc.it")


'            'Dim mailcoll As MailAddressCollection = New MailAddressCollection
'            'mailcoll.Add(New MailAddress("ccancellieri@entermed.it", "Calogero"))
'            'mailcoll.Add(New MailAddress("cancelliericalogero@gmail.com", "Calogero"))

'            'mail.To.Clear()
'            'For Each destinatari As MailAddress In mailcoll
'            '    mail.To.Add(destinatari)
'            'Next

'            mail = mail_destinatari.SetDestinatariMail(mail, 1)

'            'mail.CC.Add("")

'            'Imposta l'oggetto della Mail
'            mail.Subject = "Apertura danno per il veicolo di targa: " & targa

'            'Imposta la priorità  della Mail
'            mail.Priority = MailPriority.High

'            mail.IsBodyHtml = True

'            corpoMessaggio = "Per il veicolo con targa: " & targa & " è stato aperto in data " & Format(.data_creazione, "dd/MM/yyyy") & ".<br><br>" & _
'                "<b>Posizione del danno: </b>" & posizione_danno & "<br>" & _
'                "<b>Entità del danno: </b>" & .entita_danno.ToString & "<br>" & _
'                "<b>Descrizione: </b>" & .descrizione & "<br>" & _
'                "<b>Documento che ha generato il danno: </b>" & tipo_documento_apertura & "<br>"

'            Select Case .id
'                Case tipo_documento.Contratto_Chiusura
'                    Dim NumeroContratto As String = getNumeroContrattoDaId(.id_doc_apertura)
'                    If NumeroContratto = "" Then
'                        NumeroContratto = "(N.V. " & .id_doc_apertura & ")"
'                    End If
'                    corpoMessaggio += "<b>Numero contratto: </b>" & NumeroContratto & "<br>"
'                Case Else ' se non gestiti cmq. invio qualche informazione all'utente..
'                    corpoMessaggio += "<b>Identificativo del documento che ha generato il danno (non censito): </b>" & .id_doc_apertura & "<br>"
'            End Select


'        End With

'        mail.Body = corpoMessaggio.Replace("!", "")

'        'Imposta il server smtp di posta da utilizzare        
'        Dim Smtp As New SmtpClient(ConfigurationManager.AppSettings.Get("Mail_SMTP"))

'        'Invia l'e-mail
'        Try
'            Smtp.Send(mail)
'            HttpContext.Current.Trace.Write("inviaMailPerDanno: E-Mail inviata correttamente.")
'        Catch ex As Exception
'            HttpContext.Current.Trace.Write("inviaMailPerDanno: Si è verificato un errore nell'invio della E-Mail:" & ex.Message)
'        End Try
'    End Sub
'End Class

Public Class EventoAbilita
    Inherits EventArgs

    Dim m_Valore As Boolean

    Public Property Valore() As Boolean
        Get
            Return m_Valore
        End Get
        Set(ByVal value As Boolean)
            m_Valore = value
        End Set
    End Property
End Class

'Public Enum TipoInvio
'    NODEF = 0
'    TO_
'    CC
'    BCC
'End Enum

'Public Class mail_tipo
'    Inherits ITabellaDB

'    Protected m_id As Integer
'    Protected m_descrizione As String
'    Protected m_eliminabile As Boolean

'    Public Property id() As Integer
'        Get
'            Return m_id
'        End Get
'        Set(ByVal value As Integer)
'            m_id = value
'        End Set
'    End Property
'    Public Property descrizione() As String
'        Get
'            Return m_descrizione
'        End Get
'        Set(ByVal value As String)
'            m_descrizione = value
'        End Set
'    End Property
'    Public ReadOnly Property eliminabile() As Boolean
'        Get
'            Return m_eliminabile
'        End Get
'    End Property

'    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
'        Dim sqlStr As String = "INSERT INTO mail_tipo (descrizione,eliminabile)" & _
'                    " VALUES (@descrizione,1)"

'        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
'            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
'                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.varchar, Libreria.TrimSicuro(descrizione, 50))

'                Dbc.Open()
'                Cmd.ExecuteNonQuery()
'            End Using

'            ' -------------------------------------------------------------------------
'            'recupero l'id del nuopvo record comunque...
'            sqlStr = "SELECT @@IDENTITY FROM mail_tipo"

'            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
'                m_id = Cmd.ExecuteScalar
'            End Using
'        End Using

'        Return m_id
'    End Function

'    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As mail_tipo
'        Dim mio_record As mail_tipo = New mail_tipo
'        With mio_record
'            .id = Rs("id")
'            .descrizione = getValueOrNohing(Rs("descrizione"))
'            .m_eliminabile = Rs("eliminabile")
'        End With
'        Return mio_record
'    End Function

'    Public Shared Function getRecordDaId(ByVal id_record As Integer) As mail_tipo
'        Dim mio_record As mail_tipo = Nothing

'        Dim sqlStr As String = "SELECT * FROM mail_tipo WITH(NOLOCK) WHERE id = " & id_record

'        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
'            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
'                Dbc.Open()
'                Using Rs = Cmd.ExecuteReader
'                    If Rs.Read Then
'                        mio_record = FillRecord(Rs)
'                    End If
'                End Using
'            End Using
'        End Using

'        Return mio_record
'    End Function

'    Public Function UpdateRecord() As Boolean
'        UpdateRecord = False

'        Dim sqlStr As String = "UPDATE mail_tipo SET" & _
'            " descrizione = @descrizione" & _
'            " WHERE id = @id"

'        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
'            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
'                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, descrizione)
'                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

'                Dbc.Open()
'                Cmd.ExecuteNonQuery()
'                UpdateRecord = True
'            End Using
'        End Using
'    End Function

'    Public Shared Function EliminaRecord(ByVal id As Integer) As Boolean
'        EliminaRecord = False

'        Dim sqlStr As String = "DELETE FROM mail_tipo WHERE eliminabile = 1 AND id = " & id

'        Try
'            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
'                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
'                    Dbc.Open()
'                    Cmd.ExecuteNonQuery()
'                    EliminaRecord = True
'                End Using
'            End Using
'        Catch ex As Exception
'            HttpContext.Current.Trace.Write("EliminaRecord: " & ex.Message)
'        End Try
'    End Function
'End Class

'Public Class mail_destinatari
'    Inherits ITabellaDB

'    Protected m_id As Integer
'    Protected m_id_mail As Integer
'    Protected m_id_tipo_invio As TipoInvio
'    Protected m_mail As String
'    Protected m_nome As String


'    Public Property id() As Integer
'        Get
'            Return m_id
'        End Get
'        Set(ByVal value As Integer)
'            m_id = value
'        End Set
'    End Property
'    Public Property id_mail() As Integer
'        Get
'            Return m_id_mail
'        End Get
'        Set(ByVal value As Integer)
'            m_id_mail = value
'        End Set
'    End Property
'    Public Property id_tipo_invio() As TipoInvio
'        Get
'            Return m_id_tipo_invio
'        End Get
'        Set(ByVal value As TipoInvio)
'            m_id_tipo_invio = value
'        End Set
'    End Property
'    Public Property mail() As String
'        Get
'            Return m_mail
'        End Get
'        Set(ByVal value As String)
'            m_mail = value
'        End Set
'    End Property
'    Public Property nome() As String
'        Get
'            Return m_nome
'        End Get
'        Set(ByVal value As String)
'            m_nome = value
'        End Set
'    End Property


'    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
'        Dim sqlStr As String = "INSERT INTO mail_destinatari (id_mail,id_tipo_invio,mail,nome)" & _
'                    " VALUES (@id_mail,@id_tipo_invio,@mail,@nome)"

'        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
'            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
'                addParametro(Cmd, "@id_mail", System.Data.SqlDbType.Int, id_mail)
'                addParametro(Cmd, "@id_tipo_invio", System.Data.SqlDbType.Int, id_tipo_invio)
'                addParametro(Cmd, "@mail", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(mail, 50))
'                addParametro(Cmd, "@nome", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nome, 50))

'                Dbc.Open()
'                Cmd.ExecuteNonQuery()
'            End Using

'            ' -------------------------------------------------------------------------
'            'recupero l'id del nuopvo record comunque...
'            sqlStr = "SELECT @@IDENTITY FROM mail_destinatari"

'            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
'                m_id = Cmd.ExecuteScalar
'            End Using
'        End Using

'        Return m_id
'    End Function

'    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As mail_destinatari
'        Dim mio_record As mail_destinatari = New mail_destinatari
'        With mio_record
'            .id = Rs("id")
'            .id_mail = getValueOrNohing(Rs("id_mail"))
'            .id_tipo_invio = getValueOrNohing(Rs("id_tipo_invio"))
'            .mail = getValueOrNohing(Rs("mail"))
'            .nome = getValueOrNohing(Rs("nome"))
'        End With
'        Return mio_record
'    End Function

'    Public Shared Function getRecordDaId(ByVal id_record As Integer) As mail_destinatari
'        Dim mio_record As mail_destinatari = Nothing

'        Dim sqlStr As String = "SELECT * FROM mail_destinatari WITH(NOLOCK) WHERE id = " & id_record

'        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
'            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
'                Dbc.Open()
'                Using Rs = Cmd.ExecuteReader
'                    If Rs.Read Then
'                        mio_record = FillRecord(Rs)
'                    End If
'                End Using
'            End Using
'        End Using

'        Return mio_record
'    End Function

'    Public Function UpdateRecord() As Boolean
'        UpdateRecord = False

'        Dim sqlStr As String = "UPDATE mail_destinatari SET" & _
'            " id_mail = @id_mail," & _
'            " id_tipo_invio = @id_tipo_invio," & _
'            " mail = @mail," & _
'            " nome = @nome" & _
'            " WHERE id = @id"

'        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
'            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
'                addParametro(Cmd, "@id_mail", System.Data.SqlDbType.Int, id_mail)
'                addParametro(Cmd, "@id_tipo_invio", System.Data.SqlDbType.Int, id_tipo_invio)
'                addParametro(Cmd, "@mail", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(mail, 50))
'                addParametro(Cmd, "@nome", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nome, 50))
'                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

'                Dbc.Open()
'                Cmd.ExecuteNonQuery()
'                UpdateRecord = True
'            End Using
'        End Using
'    End Function

'    Public Shared Function EliminaRecord(ByVal id As Integer) As Boolean
'        EliminaRecord = False

'        Dim sqlStr As String = "DELETE FROM mail_destinatari WHERE id = " & id

'        Try
'            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
'                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
'                    Dbc.Open()
'                    Cmd.ExecuteNonQuery()
'                    EliminaRecord = True
'                End Using
'            End Using
'        Catch ex As Exception
'            HttpContext.Current.Trace.Write("EliminaRecord: " & ex.Message)
'        End Try
'    End Function

'    Private Function AddMail(ByVal mia_mail As MailMessage) As MailMessage
'        Dim mia_mail_adress As MailAddress
'        With Me
'            If (.nome IsNot Nothing) AndAlso .nome <> "" Then
'                mia_mail_adress = New MailAddress(.mail, .nome)
'            Else
'                mia_mail_adress = New MailAddress(.mail)
'            End If
'        End With

'        Select Case Me.id_tipo_invio
'            Case TipoInvio.TO_
'                mia_mail.To.Add(mia_mail_adress)
'            Case TipoInvio.CC
'                mia_mail.CC.Add(mia_mail_adress)
'            Case TipoInvio.BCC
'                mia_mail.Bcc.Add(mia_mail_adress)
'        End Select

'        Return mia_mail
'    End Function

'    Public Shared Function SetDestinatariMail(ByVal mia_mail As MailMessage, ByVal id_mail_tipo As Integer) As MailMessage
'        If mia_mail Is Nothing Then
'            Return Nothing
'        End If

'        Dim sqlStr As String
'        sqlStr = "SELECT * FROM mail_destinatari WITH(NOLOCK) WHERE id_mail = " & id_mail_tipo
'        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
'            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
'                Dbc.Open()
'                Using rs = Cmd.ExecuteReader()
'                    Do While rs.Read
'                        Dim mio_record As mail_destinatari = FillRecord(rs)
'                        mia_mail = mio_record.AddMail(mia_mail)
'                    Loop
'                End Using
'            End Using
'        End Using

'        Return mia_mail
'    End Function
'End Class


Public Class EventoNuovoRecord
    Inherits EventArgs

    Dim m_Valore As Integer

    Public Property Valore() As Integer
        Get
            Return m_Valore
        End Get
        Set(ByVal value As Integer)
            m_Valore = value
        End Set
    End Property
End Class

Public Class veicoli_posizione_danno
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_descrizione As String
    Protected m_bloccante As Boolean?

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property descrizione() As String
        Get
            Return m_descrizione
        End Get
        Set(ByVal value As String)
            m_descrizione = value
        End Set
    End Property
    Public Property bloccante() As Boolean?
        Get
            Return m_bloccante
        End Get
        Set(ByVal value As Boolean?)
            m_bloccante = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO veicoli_posizione_danno (descrizione,bloccante)" &
            " VALUES (@descrizione,@bloccante)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@bloccante", System.Data.SqlDbType.Bit, bloccante)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM veicoli_posizione_danno"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Public Function UpdateRecord() As veicoli_posizione_danno
        Dim sqlStr As String = "UPDATE veicoli_posizione_danno SET" &
            " descrizione = @descrizione," &
            " bloccante = @bloccante" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@bloccante", System.Data.SqlDbType.Bit, bloccante)
                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using

        Return Me
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_posizione_danno
        Dim mio_record As veicoli_posizione_danno = New veicoli_posizione_danno
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .descrizione = getValueOrNohing(Rs("descrizione"))
            .bloccante = getValueOrNohing(Rs("bloccante"))
        End With
        Return mio_record
    End Function

    Public Shared Function get_record_da_id(ByVal id_record As Integer) As veicoli_posizione_danno
        Dim mio_record As veicoli_posizione_danno = Nothing

        Dim sqlStr As String = "SELECT * FROM veicoli_posizione_danno WITH(NOLOCK) WHERE id = " & id_record

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

    Public Function CancellaRecord() As Boolean
        Return CancellaRecord(id)
    End Function

    Public Shared Function CancellaRecord(ByVal id_record As Integer) As Boolean
        CancellaRecord = False
        Dim sqlStr As String = "DELETE FROM veicoli_posizione_danno WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    CancellaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("CancellaRecord: " & ex.Message)
        End Try
    End Function

    Public Shared Function getDescrPosizioneDanno(ByVal id_record As Integer) As String
        Dim Valore As String = ""
        Dim sqlStr As String
        sqlStr = "SELECT descrizione FROM veicoli_posizione_danno WITH(NOLOCK) WHERE id = " & id_record

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Valore = Cmd.ExecuteScalar & ""
            End Using
        End Using

        Return Valore
    End Function
End Class

Public Class veicoli_tipo_danno
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_descrizione As String

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property descrizione() As String
        Get
            Return m_descrizione
        End Get
        Set(ByVal value As String)
            m_descrizione = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO veicoli_tipo_danno (descrizione)" &
            " VALUES (@descrizione)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM veicoli_tipo_danno"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Public Function UpdateRecord() As veicoli_tipo_danno
        Dim sqlStr As String = "UPDATE veicoli_tipo_danno SET" &
            " descrizione = @descrizione" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using

        Return Me
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_tipo_danno
        Dim mia_record As veicoli_tipo_danno = New veicoli_tipo_danno
        With mia_record
            .id = getValueOrNohing(Rs("id"))
            .descrizione = getValueOrNohing(Rs("descrizione"))
        End With
        Return mia_record
    End Function

    Public Shared Function get_record_da_id(ByVal id_record As Integer) As veicoli_tipo_danno
        Dim mio_record As veicoli_tipo_danno = Nothing

        Dim sqlStr As String = "SELECT * FROM veicoli_tipo_danno WITH(NOLOCK) WHERE id = " & id_record

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

    Public Function CancellaRecord() As Boolean
        Return CancellaRecord(id)
    End Function

    Public Shared Function CancellaRecord(ByVal id_record As Integer) As Boolean
        CancellaRecord = False
        Dim sqlStr As String = "DELETE FROM veicoli_tipo_danno WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    CancellaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("CancellaRecord: " & ex.Message)
        End Try
    End Function
End Class

Public Enum tipo_danni_img_tipo_documenti
    NonDefinito = 0
    Danno = 1
    Evento = 2
End Enum

Public Class veicoli_danni_img_tipo_documenti
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_tipo As Nullable(Of Integer)
    Protected m_descrizione As String
    Protected m_ordine As Nullable(Of Integer)
    Protected m_obbligatorio As Nullable(Of Boolean) = False

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property tipo() As Nullable(Of tipo_danni_img_tipo_documenti)
        Get
            Return m_tipo
        End Get
        Set(ByVal value As Nullable(Of tipo_danni_img_tipo_documenti))
            m_tipo = value
        End Set
    End Property
    Public Property descrizione() As String
        Get
            Return m_descrizione
        End Get
        Set(ByVal value As String)
            m_descrizione = value
        End Set
    End Property
    Public Property ordine() As Nullable(Of Integer)
        Get
            Return m_ordine
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_ordine = value
        End Set
    End Property
    Public Property obbligatorio() As Nullable(Of Boolean)
        Get
            Return m_obbligatorio
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_obbligatorio = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO veicoli_danni_img_tipo_documenti (tipo,descrizione,ordine,obbligatorio)" &
            " VALUES (@tipo,@descrizione,@ordine,@obbligatorio)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@tipo", System.Data.SqlDbType.Int, tipo)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@ordine", System.Data.SqlDbType.Int, ordine)
                addParametro(Cmd, "@obbligatorio", System.Data.SqlDbType.Bit, obbligatorio)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM veicoli_danni_img_tipo_documenti"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Public Function UpdateRecord() As veicoli_danni_img_tipo_documenti
        Dim sqlStr As String = "UPDATE veicoli_danni_img_tipo_documenti SET" &
            " tipo = @tipo," &
            " descrizione = @descrizione," &
            " ordine = @ordine," &
            " obbligatorio = @obbligatorio" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@tipo", System.Data.SqlDbType.Int, tipo)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@ordine", System.Data.SqlDbType.Int, ordine)
                addParametro(Cmd, "@obbligatorio", System.Data.SqlDbType.Bit, obbligatorio)
                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using

        Return Me
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_danni_img_tipo_documenti
        Dim mia_record As veicoli_danni_img_tipo_documenti = New veicoli_danni_img_tipo_documenti
        With mia_record
            .id = getValueOrNohing(Rs("id"))
            .tipo = getValueOrNohing(Rs("tipo"))
            .descrizione = getValueOrNohing(Rs("descrizione"))
            .ordine = getValueOrNohing(Rs("ordine"))
            .obbligatorio = getValueOrNohing(Rs("obbligatorio"))
        End With
        Return mia_record
    End Function

    Public Shared Function get_record_da_id(ByVal id_record As Integer) As veicoli_danni_img_tipo_documenti
        Dim mio_record As veicoli_danni_img_tipo_documenti = Nothing

        Dim sqlStr As String = "SELECT * FROM veicoli_danni_img_tipo_documenti WITH(NOLOCK) WHERE id = " & id_record

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

    Public Function CancellaRecord() As Boolean
        Return CancellaRecord(id)
    End Function

    Public Shared Function CancellaRecord(ByVal id_record As Integer) As Boolean
        CancellaRecord = False
        Dim sqlStr As String = "DELETE FROM veicoli_danni_img_tipo_documenti WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    CancellaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("CancellaRecord: " & ex.Message)
        End Try
    End Function
End Class

Public Class veicoli_tipo_documento_apertura_danno
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_richiede_id As Nullable(Of Boolean) = False
    Protected m_descrizione As String
    Protected m_codice_sintetico As String

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property richiede_id() As Nullable(Of Boolean)
        Get
            Return m_richiede_id
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_richiede_id = value
        End Set
    End Property
    Public Property descrizione() As String
        Get
            Return m_descrizione
        End Get
        Set(ByVal value As String)
            m_descrizione = value
        End Set
    End Property
    Public Property codice_sintetico() As String
        Get
            Return m_codice_sintetico
        End Get
        Set(ByVal value As String)
            m_codice_sintetico = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO veicoli_tipo_documento_apertura_danno (richiede_id,descrizione,codice_sintetico)" &
            " VALUES (@richiede_id,@descrizione,@codice_sintetico)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@richiede_id", System.Data.SqlDbType.Bit, richiede_id)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@codice_sintetico", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(codice_sintetico, 5))

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            '-------------------------------------------------------------------------
            ' recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM veicoli_tipo_documento_apertura_danno"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Public Function AggiornaRecord() As veicoli_tipo_documento_apertura_danno
        Dim sqlStr As String = "UPDATE veicoli_tipo_documento_apertura_danno SET" &
            " richiede_id = @richiede_id," &
            " descrizione = @descrizione," &
            " codice_sintetico = @codice_sintetico" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@richiede_id", System.Data.SqlDbType.Bit, richiede_id)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@codice_sintetico", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(codice_sintetico, 5))

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using

        Return Me
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_tipo_documento_apertura_danno
        Dim mia_record As veicoli_tipo_documento_apertura_danno = New veicoli_tipo_documento_apertura_danno
        With mia_record
            .id = getValueOrNohing(Rs("id"))
            .richiede_id = getValueOrNohing(Rs("richiede_id"))
            .descrizione = getValueOrNohing(Rs("descrizione"))
            .codice_sintetico = getValueOrNohing(Rs("codice_sintetico"))
        End With
        Return mia_record
    End Function

    Public Shared Function get_record_da_id(ByVal id_record As Integer) As veicoli_tipo_documento_apertura_danno
        Dim mio_record As veicoli_tipo_documento_apertura_danno = Nothing

        Dim sqlStr As String = "SELECT * FROM veicoli_tipo_documento_apertura_danno WITH(NOLOCK) WHERE id = " & id_record

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

    Public Function CancellaRecord() As Boolean
        If Me.richiede_id Then
            Return False
        End If
        Return CancellaRecord(id)
    End Function

    Protected Shared Function CancellaRecord(ByVal id_record As Integer) As Boolean
        CancellaRecord = False
        Dim sqlStr As String = "DELETE FROM veicoli_tipo_documento_apertura_danno WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    CancellaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("CancellaRecord: " & ex.Message)
        End Try
    End Function
End Class

Public Class veicoli_tipo_documento_chiusura_danno
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_richiede_id As Nullable(Of Boolean)
    Protected m_descrizione As String

    Public Property id() As Nullable(Of Integer)
        Get
            Return m_id
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id = value
        End Set
    End Property
    Public Property richiede_id() As Nullable(Of Boolean)
        Get
            Return m_richiede_id
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_richiede_id = value
        End Set
    End Property
    Public Property descrizione() As String
        Get
            Return m_descrizione
        End Get
        Set(ByVal value As String)
            m_descrizione = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO veicoli_tipo_documento_chiusura_danno (richiede_id,descrizione)" &
            " VALUES (@richiede_id,@descrizione)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@richiede_id", System.Data.SqlDbType.Bit, richiede_id)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM veicoli_tipo_documento_chiusura_danno"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Public Function AggiornaRecord() As Boolean
        AggiornaRecord = False

        Dim sqlStr As String
        sqlStr = "UPDATE veicoli_tipo_documento_chiusura_danno SET" &
            " richiede_id = @richiede_id," &
            " descrizione = @descrizione" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@richiede_id", System.Data.SqlDbType.Bit, richiede_id)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaRecord = True
            End Using
        End Using
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_tipo_documento_chiusura_danno
        Dim mia_record As veicoli_tipo_documento_chiusura_danno = New veicoli_tipo_documento_chiusura_danno
        With mia_record
            .id = getValueOrNohing(Rs("id"))
            .richiede_id = getValueOrNohing(Rs("richiede_id"))
            .descrizione = getValueOrNohing(Rs("descrizione"))
        End With
        Return mia_record
    End Function

    Public Shared Function get_record_da_id(ByVal id_record As Integer) As veicoli_tipo_documento_chiusura_danno
        Dim mio_record As veicoli_tipo_documento_chiusura_danno = Nothing

        Dim sqlStr As String = "SELECT * FROM veicoli_tipo_documento_chiusura_danno WITH(NOLOCK) WHERE id = " & id_record

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

    Public Function EliminaRecord() As Boolean
        If Me.richiede_id Then
            Return False
        End If
        Return EliminaRecord(Me.id)
    End Function

    Public Shared Function EliminaRecord(ByVal id_record As Integer) As Boolean
        EliminaRecord = False

        Dim sqlStr As String
        sqlStr = "DELETE FROM veicoli_tipo_documento_chiusura_danno WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord: " & ex.Message)
        End Try
    End Function
End Class

Public Class veicoli_movimenti_danni
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_id_tipo_documento As Integer
    Protected m_id_documento As Nullable(Of Integer)
    Protected m_id_veicolo As Integer
    Protected m_data_creazione As Nullable(Of DateTime)
    Protected m_id_utente As Nullable(Of Integer)
    Protected m_nota As String


    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property id_tipo_documento() As Nullable(Of Integer)
        Get
            Return m_id_tipo_documento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id_tipo_documento = value
        End Set
    End Property
    Public Property id_documento() As Nullable(Of Integer)
        Get
            Return m_id_documento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id_documento = value
        End Set
    End Property
    Public Property id_veicolo() As Integer
        Get
            Return m_id_veicolo
        End Get
        Set(ByVal value As Integer)
            m_id_veicolo = value
        End Set
    End Property
    Public Property data_creazione() As Nullable(Of DateTime)
        Get
            Return m_data_creazione
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            m_data_creazione = value
        End Set
    End Property
    Public Property id_utente() As Nullable(Of Integer)
        Get
            Return m_id_utente
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id_utente = value
        End Set
    End Property
    Public Property nota() As String
        Get
            Return m_nota
        End Get
        Set(ByVal value As String)
            m_nota = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO veicoli_movimenti_danni (id_tipo_documento,id_documento,id_veicolo,data_creazione,id_utente,nota)" &
            " VALUES (@id_tipo_documento,@id_documento,@id_veicolo,@data_creazione,@id_utente,@nota)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_tipo_documento", System.Data.SqlDbType.Int, id_tipo_documento)
                addParametro(Cmd, "@id_documento", System.Data.SqlDbType.Int, id_documento)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)
                addParametro(Cmd, "@nota", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nota))

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM veicoli_movimenti_danni"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Public Function AggiornaRecord() As Boolean
        AggiornaRecord = False

        Dim sqlStr As String
        sqlStr = "UPDATE veicoli_movimenti_danni SET" &
            " id_tipo_documento = @id_tipo_documento," &
            " id_documento = @id_documento," &
            " data_creazione = @data_creazione," &
            " id_utente = @id_utente," &
            " nota = @nota" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_tipo_documento", System.Data.SqlDbType.Int, id_tipo_documento)
                addParametro(Cmd, "@id_documento", System.Data.SqlDbType.Int, id_documento)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)
                addParametro(Cmd, "@nota", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nota))
                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaRecord = True
            End Using
        End Using
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_movimenti_danni
        Dim mia_record As veicoli_movimenti_danni = New veicoli_movimenti_danni
        With mia_record
            .id = getValueOrNohing(Rs("id"))
            .id_tipo_documento = getValueOrNohing(Rs("id_tipo_documento"))
            .id_documento = getValueOrNohing(Rs("id_documento"))
            .id_veicolo = getValueOrNohing(Rs("id_veicolo"))
            .data_creazione = getValueOrNohing(Rs("data_creazione"))
            .id_utente = getValueOrNohing(Rs("id_utente"))
            .nota = getValueOrNohing(Rs("nota"))

        End With
        Return mia_record
    End Function

    Public Shared Function get_record_da_id(ByVal id_record As Integer) As veicoli_movimenti_danni
        Dim mio_record As veicoli_movimenti_danni = Nothing

        Dim sqlStr As String = "SELECT * FROM veicoli_movimenti_danni WITH(NOLOCK) WHERE id = " & id_record

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

    ' Non so se mantenere le funzioni di eliminazione...
    ' un danno lo posso creare... ma non posso più eliminarlo, solo chiuderlo...
    'Public Function EliminaRecord() As Boolean
    '    Return EliminaRecord(Me.id)
    'End Function

    'Public Shared Function EliminaRecord(id_record As Integer) As Boolean
    '    EliminaRecord = False

    '    Dim sqlStr As String
    '    sqlStr = "DELETE FROM veicoli_movimenti_danni WHERE id = " & id_record

    '    Try
    '        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '                Dbc.Open()
    '                Cmd.ExecuteNonQuery()
    '                EliminaRecord = True
    '            End Using
    '        End Using
    '    Catch ex As Exception
    '        HttpContext.Current.Trace.Write("EliminaRecord: " & ex.Message)
    '    End Try

    'End Function
End Class

Public Class veicoli_chiusura_danni
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_id_tipo_documento As Integer
    Protected m_id_documento As Nullable(Of Integer)
    Protected m_id_veicolo As Integer
    Protected m_data_creazione As Nullable(Of DateTime)
    Protected m_id_utente As Nullable(Of Integer)
    Protected m_nota As String


    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property id_tipo_documento() As Nullable(Of Integer)
        Get
            Return m_id_tipo_documento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id_tipo_documento = value
        End Set
    End Property
    Public Property id_documento() As Nullable(Of Integer)
        Get
            Return m_id_documento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id_documento = value
        End Set
    End Property
    Public Property id_veicolo() As Integer
        Get
            Return m_id_veicolo
        End Get
        Set(ByVal value As Integer)
            m_id_veicolo = value
        End Set
    End Property
    Public Property data_creazione() As Nullable(Of DateTime)
        Get
            Return m_data_creazione
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            m_data_creazione = value
        End Set
    End Property
    Public Property id_utente() As Nullable(Of Integer)
        Get
            Return m_id_utente
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id_utente = value
        End Set
    End Property
    Public Property nota() As String
        Get
            Return m_nota
        End Get
        Set(ByVal value As String)
            m_nota = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO veicoli_chiusura_danni (id_tipo_documento,id_documento,id_veicolo,data_creazione,id_utente,nota)" &
            " VALUES (@id_tipo_documento,@id_documento,@id_veicolo,@data_creazione,@id_utente,@nota)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_tipo_documento", System.Data.SqlDbType.Int, id_tipo_documento)
                addParametro(Cmd, "@id_documento", System.Data.SqlDbType.Int, id_documento)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)
                addParametro(Cmd, "@nota", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nota))

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM veicoli_chiusura_danni"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Public Function AggiornaRecord() As Boolean
        AggiornaRecord = False

        Dim sqlStr As String
        sqlStr = "UPDATE veicoli_chiusura_danni SET" &
            " id_tipo_documento = @id_tipo_documento," &
            " id_documento = @id_documento," &
            " data_creazione = @data_creazione," &
            " id_utente = @id_utente," &
            " nota = @nota" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_tipo_documento", System.Data.SqlDbType.Int, id_tipo_documento)
                addParametro(Cmd, "@id_documento", System.Data.SqlDbType.Int, id_documento)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)
                addParametro(Cmd, "@nota", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nota))
                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaRecord = True
            End Using
        End Using
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_chiusura_danni
        Dim mia_record As veicoli_chiusura_danni = New veicoli_chiusura_danni
        With mia_record
            .id = getValueOrNohing(Rs("id"))
            .id_tipo_documento = getValueOrNohing(Rs("id_tipo_documento"))
            .id_documento = getValueOrNohing(Rs("id_documento"))
            .id_veicolo = getValueOrNohing(Rs("id_veicolo"))
            .data_creazione = getValueOrNohing(Rs("data_creazione"))
            .id_utente = getValueOrNohing(Rs("id_utente"))
            .nota = getValueOrNohing(Rs("nota"))

        End With
        Return mia_record
    End Function

    Public Shared Function get_record_da_id(ByVal id_record As Integer) As veicoli_chiusura_danni
        Dim mio_record As veicoli_chiusura_danni = Nothing

        Dim sqlStr As String = "SELECT * FROM veicoli_chiusura_danni WITH(NOLOCK) WHERE id = " & id_record

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

    Public Function EliminaRecord() As Boolean
        Return EliminaRecord(Me.id)
    End Function

    Public Shared Function EliminaRecord(ByVal id_record As Integer) As Boolean
        EliminaRecord = False

        Dim sqlStr As String

        sqlStr = "UPDATE veicoli_danni SET" &
            " id_doc_chiusura = Null," &
            " data_chiusura = Null," &
            " id_utente_chiusura = Null" &
            " WHERE id_doc_chiusura = " & id_record
        HttpContext.Current.Trace.Write("EliminaRecord sqlStr: " & sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using

        sqlStr = "DELETE FROM veicoli_chiusura_danni WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord: " & ex.Message)
        End Try

    End Function
End Class

<SerializableAttribute()>
Public Class veicoli_img_modelli
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_descrizione As String
    Protected m_img_fronte As String
    Protected m_img_retro As String

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property descrizione() As String
        Get
            Return m_descrizione
        End Get
        Set(ByVal value As String)
            m_descrizione = value
        End Set
    End Property
    Public Property img_fronte() As String
        Get
            Return m_img_fronte
        End Get
        Set(ByVal value As String)
            m_img_fronte = value
        End Set
    End Property
    Public Property img_retro() As String
        Get
            Return m_img_retro
        End Get
        Set(ByVal value As String)
            m_img_retro = value
        End Set
    End Property


    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO veicoli_img_modelli (descrizione,img_fronte,img_retro)" &
            " VALUES (@descrizione,@img_fronte,@img_retro)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@img_fronte", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(img_fronte, 50))
                addParametro(Cmd, "@img_retro", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(img_retro, 50))

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM veicoli_img_modelli"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Public Function verificaDescrizione() As Boolean
        Return verificaDescrizione(Me.descrizione, Me.id)
    End Function

    Public Shared Function verificaDescrizione(ByVal descr As String, Optional ByVal id_record As Integer = 0) As Boolean
        Dim sqlStr As String = "SELECT COUNT(*) FROM veicoli_img_modelli WITH(NOLOCK)" &
            " WHERE descrizione = '" & Libreria.formattaSqlTrim(descr) & "'"
        If id_record > 0 Then
            sqlStr += " AND id <> " & id_record
        End If

        Dim NumRecord As Integer
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                NumRecord = Cmd.ExecuteScalar()
            End Using
        End Using

        Return (0 = NumRecord)
    End Function

    Public Function AggiornaRecord() As Boolean
        AggiornaRecord = False

        Dim sqlStr As String
        sqlStr = "UPDATE veicoli_img_modelli SET" &
            " descrizione = @descrizione," &
            " img_fronte = @img_fronte," &
            " img_retro = @img_retro" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@img_fronte", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(img_fronte, 50))
                addParametro(Cmd, "@img_retro", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(img_retro, 50))
                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaRecord = True
            End Using
        End Using
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_img_modelli
        Dim mia_record As veicoli_img_modelli = New veicoli_img_modelli
        With mia_record
            .id = getValueOrNohing(Rs("id"))
            .descrizione = getValueOrNohing(Rs("descrizione"))
            .img_fronte = getValueOrNohing(Rs("img_fronte"))
            .img_retro = getValueOrNohing(Rs("img_retro"))
        End With
        Return mia_record
    End Function

    Public Shared Function get_record_da_id(ByVal id_record As Integer) As veicoli_img_modelli
        Dim mio_record As veicoli_img_modelli = Nothing

        Dim sqlStr As String = "SELECT * FROM veicoli_img_modelli WITH(NOLOCK) WHERE id = " & id_record

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

    Public Function EliminaRecord() As Boolean
        Return EliminaRecord(Me.id)
    End Function

    Public Shared Function EliminaRecord(ByVal id_record As Integer) As Boolean
        EliminaRecord = False

        Dim sqlStr As String = "DELETE FROM veicoli_img_modelli WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord: " & ex.Message)
        End Try

    End Function

    Public Shared Function getMacroModello(ByVal id_veicolo As Integer) As veicoli_img_modelli
        Dim sqlStr As String = "SELECT am.id_img_modello FROM veicoli v WITH(NOLOCK)" &
            " INNER JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON v.id_modello = am.id_modello" &
            " WHERE id = " & id_veicolo

        Dim id_img_modello As String = ""
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                id_img_modello = Cmd.ExecuteScalar & ""
            End Using
        End Using

        If id_img_modello = "" Then
            ' macro modello non trovato, utilizzo quello di default!
            id_img_modello = Costanti.id_mappa_default
        End If

        Return veicoli_img_modelli.get_record_da_id(Integer.Parse(id_img_modello))
    End Function

End Class

Public Enum Tipo_Img_Mappa
    Fronte = 1
    Retro
End Enum

Public Class veicoli_img_mappatura
    Inherits ITabellaDB
    Implements I_veicoli_img_mappatura


    Protected m_id As Integer
    Protected m_id_img_modelli As Integer
    Protected m_tipo_img As Integer
    Protected m_x As Integer
    Protected m_y As Integer
    Protected m_id_posizione_danno As Integer = 0

    Protected m_id_posizione_danno_selezionato As Integer

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property id_img_modelli() As Integer
        Get
            Return m_id_img_modelli
        End Get
        Set(ByVal value As Integer)
            m_id_img_modelli = value
        End Set
    End Property
    Public Property tipo_img() As Integer
        Get
            Return m_tipo_img
        End Get
        Set(ByVal value As Integer)
            m_tipo_img = value
        End Set
    End Property
    Public Property x() As Integer
        Get
            Return m_x
        End Get
        Set(ByVal value As Integer)
            m_x = value
        End Set
    End Property
    Public Property y() As Integer
        Get
            Return m_y
        End Get
        Set(ByVal value As Integer)
            m_y = value
        End Set
    End Property
    Public Property id_posizione_danno() As Integer
        Get
            Return m_id_posizione_danno
        End Get
        Set(ByVal value As Integer)
            m_id_posizione_danno = value
        End Set
    End Property
    Public Property id_posizione_danno_selezionato() As Integer
        Get
            Return m_id_posizione_danno_selezionato
        End Get
        Set(ByVal value As Integer)
            m_id_posizione_danno_selezionato = value
        End Set
    End Property


    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO veicoli_img_mappatura (id_img_modelli,tipo_img,x,y,id_posizione_danno)" &
            " VALUES (@id_img_modelli,@tipo_img,@x,@y,@id_posizione_danno)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_img_modelli", System.Data.SqlDbType.Int, id_img_modelli)
                addParametro(Cmd, "@tipo_img", System.Data.SqlDbType.Int, tipo_img)
                addParametro(Cmd, "@x", System.Data.SqlDbType.Int, x)
                addParametro(Cmd, "@y", System.Data.SqlDbType.Int, y)
                addParametro(Cmd, "@id_posizione_danno", System.Data.SqlDbType.Int, id_posizione_danno)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM veicoli_img_mappatura"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Public Function AggiornaRecord() As Boolean
        AggiornaRecord = False

        Dim sqlStr As String
        sqlStr = "UPDATE veicoli_img_mappatura SET" &
            " id_img_modelli = @id_img_modelli," &
            " tipo_img = @tipo_img," &
            " x = @x," &
            " y = @y," &
            " id_posizione_danno = @id_posizione_danno" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_img_modelli", System.Data.SqlDbType.Int, id_img_modelli)
                addParametro(Cmd, "@tipo_img", System.Data.SqlDbType.Int, tipo_img)
                addParametro(Cmd, "@x", System.Data.SqlDbType.Int, x)
                addParametro(Cmd, "@y", System.Data.SqlDbType.Int, y)
                addParametro(Cmd, "@id_posizione_danno", System.Data.SqlDbType.Int, id_posizione_danno)
                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaRecord = True
            End Using
        End Using
    End Function

    Private Overloads Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_img_mappatura
        Dim mia_record As veicoli_img_mappatura = New veicoli_img_mappatura
        With mia_record
            .id = getValueOrNohing(Rs("id"))
            .id_img_modelli = getValueOrNohing(Rs("id_img_modelli"))
            .tipo_img = getValueOrNohing(Rs("tipo_img"))
            .x = getValueOrNohing(Rs("x"))
            .y = getValueOrNohing(Rs("y"))
            .id_posizione_danno = getValueOrNohing(Rs("id_posizione_danno"))
        End With
        Return mia_record
    End Function

    Public Shared Function get_record_da_id(ByVal id_record As Integer) As veicoli_img_mappatura
        Dim mio_record As veicoli_img_mappatura = Nothing

        Dim sqlStr As String = "SELECT * FROM veicoli_img_mappatura WITH(NOLOCK) WHERE id = " & id_record

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

    Public Function EliminaRecord() As Boolean
        Return EliminaRecord(Me.id)
    End Function

    Public Shared Function EliminaRecord(ByVal id_record As Integer) As Boolean
        EliminaRecord = False

        Dim sqlStr As String = "DELETE FROM veicoli_img_mappatura WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord: " & ex.Message)
        End Try

    End Function

    Public Shared Function get_lista_punti_mappa(ByVal id_img_modello As Integer, ByVal mappa As Tipo_Img_Mappa) As List(Of I_veicoli_img_mappatura)
        Dim mia_lista As List(Of I_veicoli_img_mappatura) = New List(Of I_veicoli_img_mappatura)

        Dim sqlStr As String = "SELECT * FROM veicoli_img_mappatura WITH(NOLOCK)" &
            " WHERE id_img_modelli = " & id_img_modello &
            " AND tipo_img = " & mappa
        ' HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    Do While Rs.Read
                        Dim mio_record As veicoli_img_mappatura = FillRecord(Rs)
                        If mio_record IsNot Nothing Then
                            mia_lista.Add(mio_record)
                        End If
                    Loop
                End Using
            End Using
        End Using

        Return mia_lista
    End Function

    Public Shared Function get_lista_punti_veicolo(ByVal id_veicolo As Integer, ByVal mappa As Tipo_Img_Mappa) As List(Of I_veicoli_img_mappatura)
        Dim mia_lista As List(Of I_veicoli_img_mappatura) = New List(Of I_veicoli_img_mappatura)

        Dim sqlStr As String = "SELECT DISTINCT im.* FROM veicoli v WITH(NOLOCK)" &
            " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON v.id_modello = am.id_modello" &
            " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello," & Costanti.id_mappa_default & ") AND im.tipo_img = " & mappa &
            " INNER JOIN veicoli_danni d WITH(NOLOCK) ON d.id_posizione_danno = im.id_posizione_danno AND v.id = d.id_veicolo d.stato = 1" &
            " WHERE v.id = " & id_veicolo &
            " ORDER BY im.id_posizione_danno"
        HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    Do While Rs.Read
                        Dim mio_record As veicoli_img_mappatura = FillRecord(Rs)
                        If mio_record IsNot Nothing Then
                            mia_lista.Add(mio_record)
                        End If
                    Loop
                End Using
            End Using
        End Using

        Return mia_lista
    End Function

    Public Overridable Function NuovaImmagine() As ImageButton Implements I_veicoli_img_mappatura.NuovaImmagine
        Dim myImage As ImageButton = New ImageButton()

        myImage.ID = "myImage_" & id & "_end"

        myImage.ToolTip = veicoli_posizione_danno.getDescrPosizioneDanno(id_posizione_danno)
        If myImage.ToolTip = "" Then
            myImage.ToolTip = "N.V."
        End If

        If id_posizione_danno_selezionato = id_posizione_danno Then
            myImage.ImageUrl = "/images/Icone/centro_green.png"
        Else
            myImage.ImageUrl = "/images/Icone/centro.png"
        End If

        myImage.Style.Add(HtmlTextWriterStyle.Position, "absolute")
        myImage.Style.Add(HtmlTextWriterStyle.Width, Costanti.DeltaIconaX & "px")
        myImage.Style.Add(HtmlTextWriterStyle.Height, Costanti.DeltaIconaY & "px")
        myImage.Style.Add(HtmlTextWriterStyle.Left, (x - Costanti.DeltaIconaX \ 2) & "px")
        myImage.Style.Add(HtmlTextWriterStyle.Top, (y - Costanti.DeltaIconaY \ 2) & "px")

        If tipo_img = 1 Then
            myImage.OnClientClick = "javascript:return (point_it_F(" & id_posizione_danno & "))"
        Else
            myImage.OnClientClick = "javascript:return (point_it_R(" & id_posizione_danno & "))"
        End If

        HttpContext.Current.Trace.Write(">>>>>>>>>>>>>>>> NuovaImmagine: " & myImage.ID & " - " & myImage.OnClientClick)

        Return myImage
    End Function

    Public Shared Sub CancellaIcone(ByVal Contenitore As Control)
        Dim el_da_eliminare As List(Of Control) = New List(Of Control)
        For Each elemento As Control In Contenitore.Controls
            If TypeOf elemento Is Image Then

                If elemento.ID.Contains("myImage_") Then
                    el_da_eliminare.Add(elemento)
                End If
            End If
        Next

        For i As Integer = 0 To el_da_eliminare.Count - 1
            Dim elemento As Control = el_da_eliminare(i)
            HttpContext.Current.Trace.Write("CancellaIcone elemento: " & elemento.ID)
            Contenitore.Controls.Remove(elemento)
        Next
    End Sub

    Public Shared Sub DisegnaSuContenitore(ByVal Contenitore As Control, ByVal mia_lista As List(Of I_veicoli_img_mappatura), ByVal mappa As Tipo_Img_Mappa)
        CancellaIcone(Contenitore)

        For Each mio_record As I_veicoli_img_mappatura In mia_lista
            Dim myImage As ImageButton = mio_record.NuovaImmagine()

            Dim img_temp = Contenitore.FindControl(myImage.ID)
            If img_temp IsNot Nothing Then
                Contenitore.Controls.Remove(img_temp)
            End If
            Contenitore.Controls.Add(myImage)
        Next
    End Sub

End Class

Public Interface I_veicoli_img_mappatura
    Function NuovaImmagine() As ImageButton
End Interface

Public Class veicoli_img_mappatura_indicizzata
    Inherits veicoli_img_mappatura
    Implements I_veicoli_img_mappatura

    Protected m_indice As Integer
    Protected m_numero As Integer
    Public Property indice() As Integer
        Get
            Return m_indice
        End Get
        Set(ByVal value As Integer)
            m_indice = value
        End Set
    End Property
    Public Property numero() As Integer
        Get
            Return m_numero
        End Get
        Set(ByVal value As Integer)
            m_numero = value
        End Set
    End Property

    Public Shared Function get_lista_punti_veicolo_con_indice(ByVal id_veicolo As Integer, ByVal mappa As Tipo_Img_Mappa) As List(Of I_veicoli_img_mappatura)
        Dim mia_lista As List(Of I_veicoli_img_mappatura) = New List(Of I_veicoli_img_mappatura)

        Dim sqlStr As String = "SELECT COUNT(*) numero, " &
            " DENSE_RANK() OVER (ORDER BY im.id_posizione_danno) indice, im.* FROM veicoli v WITH(NOLOCK)" &
            " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON v.id_modello = am.id_modello" &
            " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello," & Costanti.id_mappa_default & ") AND im.tipo_img = " & mappa &
            " INNER JOIN veicoli_danni d WITH(NOLOCK) ON d.id_posizione_danno = im.id_posizione_danno AND v.id = d.id_veicolo AND d.stato = 1" &
            " WHERE v.id = " & id_veicolo &
            " GROUP BY im.id_posizione_danno, im.id, im.id_img_modelli, im.tipo_img, im.x, im.y" &
            " ORDER BY im.id_posizione_danno"
        HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    Do While Rs.Read
                        Dim mio_record As veicoli_img_mappatura_indicizzata = FillRecord(Rs)
                        If mio_record IsNot Nothing Then
                            mia_lista.Add(mio_record)
                        End If
                    Loop
                End Using
            End Using
        End Using

        Return mia_lista
    End Function

    Public Shared Function get_lista_punti_veicolo_con_indice_per_gruppo(ByVal id_gruppo_evento As Integer, ByVal mappa As Tipo_Img_Mappa) As List(Of I_veicoli_img_mappatura)
        Dim mia_lista As List(Of I_veicoli_img_mappatura) = New List(Of I_veicoli_img_mappatura)

        Dim sqlStr As String = "SELECT CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END indice," &
            " im.*" &
            " FROM veicoli_gruppo_evento ge WITH(NOLOCK)" &
            " INNER JOIN veicoli_gruppo_danni gd WITH(NOLOCK) ON ge.id = gd.id_evento_apertura AND gd.tipo_record = 1 AND ge.id = @id_gruppo_evento" &
            " LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = gd.id_tipo_danno" &
            " LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = gd.id_posizione_danno" &
            " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON ge.id_modello = am.id_modello" &
            " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello,@immagine_default) AND im.tipo_img = @mappa AND gd.id_posizione_danno = im.id_posizione_danno" &
            " WHERE im.id_posizione_danno IS NOT NULL"
        '   HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_gruppo_evento", System.Data.SqlDbType.Int, id_gruppo_evento)
                addParametro(Cmd, "@mappa", System.Data.SqlDbType.Int, mappa)
                addParametro(Cmd, "@immagine_default", System.Data.SqlDbType.Int, Costanti.id_mappa_default)
                HttpContext.Current.Trace.Write(sqlStr)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    Do While Rs.Read
                        Dim mio_record As veicoli_img_mappatura_indicizzata = FillRecord(Rs)
                        If mio_record IsNot Nothing Then
                            mia_lista.Add(mio_record)
                        End If
                    Loop
                End Using
            End Using
        End Using

        Return mia_lista
    End Function


    'Public Shared Function get_lista_punti_veicolo_con_indice(id_veicolo As Integer, id_tipo_documento As tipo_documento, id_documento As Integer, mappa As Tipo_Img_Mappa) As List(Of I_veicoli_img_mappatura)
    '    Dim mia_lista As List(Of I_veicoli_img_mappatura) = New List(Of I_veicoli_img_mappatura)

    '    Dim sqlStr As String = "SELECT CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END indice," & _
    '        " im.*" & _
    '        " FROM veicoli_gruppo_evento ge WITH(NOLOCK)" & _
    '        " INNER JOIN veicoli_gruppo_danni gd WITH(NOLOCK) ON ge.id = gd.id_evento_apertura AND gd.tipo_record = 1 AND ge.id_veicolo = @id_veicolo AND ge.id_tipo_documento_apertura = @id_tipo_documento AND ge.id_documento_apertura = @id_documento" & _
    '        " LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = gd.id_tipo_danno" & _
    '        " LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = gd.id_posizione_danno" & _
    '        " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON ge.id_modello = am.id_modello" & _
    '        " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello,@immagine_default) AND im.tipo_img = @mappa AND gd.id_posizione_danno = im.id_posizione_danno" & _
    '        " WHERE im.id_posizione_danno IS NOT NULL" 
    '    HttpContext.Current.Trace.Write(sqlStr)

    '    Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '            addParametro(Cmd, "@id_tipo_documento", System.Data.SqlDbType.Int, id_tipo_documento)
    '            addParametro(Cmd, "@id_documento", System.Data.SqlDbType.Int, id_documento)
    '            addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
    '            addParametro(Cmd, "@mappa", System.Data.SqlDbType.Int, mappa)
    '            addParametro(Cmd, "@immagine_default", System.Data.SqlDbType.Int, Costanti.id_mappa_default)

    '            Dbc.Open()
    '            Using Rs = Cmd.ExecuteReader
    '                Do While Rs.Read
    '                    Dim mio_record As veicoli_img_mappatura_indicizzata = FillRecord(Rs)
    '                    If mio_record IsNot Nothing Then
    '                        mia_lista.Add(mio_record)
    '                    End If
    '                Loop
    '            End Using
    '        End Using
    '    End Using

    '    Return mia_lista
    'End Function

    Public Shared Function get_lista_punti_veicolo_evento_con_indice(ByVal id_evento As Integer, ByVal id_veicolo As Integer, ByVal mappa As Tipo_Img_Mappa) As List(Of I_veicoli_img_mappatura)
        Dim mia_lista As List(Of I_veicoli_img_mappatura) = New List(Of I_veicoli_img_mappatura)

        Dim sqlStr As String = "SELECT COUNT(*) numero, " &
            " DENSE_RANK() OVER (ORDER BY im.id_posizione_danno) indice, im.* FROM veicoli v WITH(NOLOCK)" &
            " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON v.id_modello = am.id_modello" &
            " LEFT JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello," & Costanti.id_mappa_default & ") AND im.tipo_img = " & mappa &
            " LEFT JOIN veicoli_danni d WITH(NOLOCK) ON d.id_posizione_danno = im.id_posizione_danno AND v.id = d.id_veicolo AND d.stato = 1 AND d.attivo = 1 AND d.id_evento_apertura = " & id_evento &
            " WHERE v.id = " & id_veicolo &
            " GROUP BY im.id_posizione_danno, im.id, im.id_img_modelli, im.tipo_img, im.x, im.y" &
            " ORDER BY im.id_posizione_danno"
        HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    Do While Rs.Read
                        Dim mio_record As veicoli_img_mappatura_indicizzata = FillRecord(Rs)
                        If mio_record IsNot Nothing Then
                            mia_lista.Add(mio_record)
                        End If
                    Loop
                End Using
            End Using
        End Using

        Return mia_lista
    End Function

    Private Overloads Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_img_mappatura_indicizzata
        Dim mia_record As veicoli_img_mappatura_indicizzata = New veicoli_img_mappatura_indicizzata
        With mia_record
            .id = getValueOrNohing(Rs("id"))
            .id_img_modelli = getValueOrNohing(Rs("id_img_modelli"))
            .tipo_img = getValueOrNohing(Rs("tipo_img"))
            .x = getValueOrNohing(Rs("x"))
            .y = getValueOrNohing(Rs("y"))
            .id_posizione_danno = getValueOrNohing(Rs("id_posizione_danno"))
            .indice = getValueOrNohing(Rs("indice"))
            ' .numero = getValueOrNohing(Rs("numero"))
        End With
        Return mia_record
    End Function

    Public Overrides Function NuovaImmagine() As ImageButton
        Dim myImage As ImageButton = New ImageButton()

        myImage.ID = "myImage_" & id & "_end"
        HttpContext.Current.Trace.Write(">>>>>>>>>>>>>>>> NuovaImmagine: " & myImage.ID)
        myImage.ToolTip = veicoli_posizione_danno.getDescrPosizioneDanno(id_posizione_danno)
        If myImage.ToolTip = "" Then
            myImage.ToolTip = "N.V."
        End If
        myImage.ImageUrl = "/images/Icone/cerchio_" & Format(indice, "00") & ".png"

        myImage.Style.Add(HtmlTextWriterStyle.Position, "absolute")
        myImage.Style.Add(HtmlTextWriterStyle.Width, Costanti.DeltaIconaX & "px")
        myImage.Style.Add(HtmlTextWriterStyle.Height, Costanti.DeltaIconaY & "px")
        myImage.Style.Add(HtmlTextWriterStyle.Left, (x - Costanti.DeltaIconaX \ 2) & "px")
        myImage.Style.Add(HtmlTextWriterStyle.Top, (y - Costanti.DeltaIconaY \ 2) & "px")

        myImage.OnClientClick = "javascript:return false"

        Return myImage
    End Function
End Class


Public Class veicoli_gruppo_evento
    Inherits ITabellaDB

    ' Const NomeTabella = "[veicoli_gruppo_evento]"

    Protected m_id As Integer
    Protected m_id_veicolo As Integer
    Protected m_id_modello As Integer
    Protected m_id_tipo_documento_apertura As Integer
    Protected m_id_documento_apertura As Nullable(Of Integer)
    Protected m_data_creazione As DateTime
    Protected m_id_utente As Integer
    Protected m_id_evento As Integer?

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property id_veicolo() As Integer
        Get
            Return m_id_veicolo
        End Get
        Set(ByVal value As Integer)
            m_id_veicolo = value
        End Set
    End Property
    Public Property id_modello() As Integer
        Get
            Return m_id_modello
        End Get
        Set(ByVal value As Integer)
            m_id_modello = value
        End Set
    End Property
    Public Property id_tipo_documento_apertura() As Integer
        Get
            Return m_id_tipo_documento_apertura
        End Get
        Set(ByVal value As Integer)
            m_id_tipo_documento_apertura = value
        End Set
    End Property
    Public Property id_documento_apertura() As Integer?
        Get
            Return m_id_documento_apertura
        End Get
        Set(ByVal value As Integer?)
            m_id_documento_apertura = value
        End Set
    End Property
    Public Property data_creazione() As DateTime
        Get
            Return m_data_creazione
        End Get
        Set(ByVal value As DateTime)
            m_data_creazione = value
        End Set
    End Property
    Public Property id_utente() As Integer
        Get
            Return m_id_utente
        End Get
        Set(ByVal value As Integer)
            m_id_utente = value
        End Set
    End Property
    Public Property id_evento() As Integer?
        Get
            Return m_id_evento
        End Get
        Set(ByVal value As Integer?)
            m_id_evento = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO [veicoli_gruppo_evento] (id_veicolo,id_modello,id_tipo_documento_apertura,id_documento_apertura,data_creazione,id_utente,id_evento)" &
            " VALUES (@id_veicolo,@id_modello,@id_tipo_documento_apertura,@id_documento_apertura,@data_creazione,@id_utente,@id_evento)"

        m_data_creazione = Now
        m_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                addParametro(Cmd, "@id_modello", System.Data.SqlDbType.Int, id_modello)
                addParametro(Cmd, "@id_tipo_documento_apertura", System.Data.SqlDbType.Int, id_tipo_documento_apertura)
                addParametro(Cmd, "@id_documento_apertura", System.Data.SqlDbType.Int, id_documento_apertura)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)
                addParametro(Cmd, "@id_evento", System.Data.SqlDbType.Int, id_evento)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuovo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM [veicoli_gruppo_evento]"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_gruppo_evento
        Dim mio_record As veicoli_gruppo_evento = New veicoli_gruppo_evento
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .id_veicolo = getValueOrNohing(Rs("id_veicolo"))
            .id_modello = getValueOrNohing(Rs("id_modello"))
            .id_tipo_documento_apertura = getValueOrNohing(Rs("id_tipo_documento_apertura"))
            .id_documento_apertura = getValueOrNohing(Rs("id_documento_apertura"))
            .m_data_creazione = getValueOrNohing(Rs("data_creazione"))
            .m_id_utente = getValueOrNohing(Rs("id_utente"))
            .id_evento = getValueOrNohing(Rs("id_evento"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As veicoli_gruppo_evento
        Dim mio_record As veicoli_gruppo_evento = Nothing

        Dim sqlStr As String = "SELECT * FROM [veicoli_gruppo_evento] WITH(NOLOCK) WHERE id = " & id_record

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

    Public Shared Function getRecordDaDocumento(ByVal id_veicolo As Integer, ByVal id_tipo_documento As tipo_documento, ByVal id_documento As Integer) As veicoli_gruppo_evento
        Dim mio_record As veicoli_gruppo_evento = Nothing

        Dim sqlStr As String = "SELECT * FROM [veicoli_gruppo_evento] WITH(NOLOCK)" &
            " WHERE id_veicolo = " & id_veicolo &
            " AND id_tipo_documento_apertura = " & id_tipo_documento &
            " AND id_documento_apertura = " & id_documento

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

        Dim sqlStr As String = "UPDATE [veicoli_gruppo_evento] SET" &
            " id_veicolo = @id_veicolo," &
            " id_modello = @id_modello," &
            " id_tipo_documento_apertura = @id_tipo_documento_apertura," &
            " id_documento_apertura = @id_documento_apertura," &
            " data_creazione = @data_creazione," &
            " id_utente = @id_utente," &
            " id_evento = @id_evento" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                addParametro(Cmd, "@id_modello", System.Data.SqlDbType.Int, id_modello)
                addParametro(Cmd, "@id_tipo_documento_apertura", System.Data.SqlDbType.Int, id_tipo_documento_apertura)
                addParametro(Cmd, "@id_documento_apertura", System.Data.SqlDbType.Int, id_documento_apertura)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)
                addParametro(Cmd, "@id_evento", System.Data.SqlDbType.Int, id_evento)

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaRecord = True
            End Using
        End Using
    End Function

    Public Shared Function getNumeroDanni(ByVal id_veicolo As Integer) As Integer
        getNumeroDanni = 0
        Dim sqlStr As String = "SELECT COUNT(*) Num FROM veicoli_danni WITH(NOLOCK)" &
            " WHERE id_veicolo = " & id_veicolo &
            " AND stato = 1" &
            " AND attivo = 1"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getNumeroDanni = Cmd.ExecuteScalar()
            End Using
        End Using
    End Function

    Public Shared Function getModelloDaIdVeicolo(ByVal id_veicolo As Integer) As Integer
        getModelloDaIdVeicolo = 0
        Dim sqlStr As String = "SELECT id_modello FROM veicoli WITH(NOLOCK)" &
            " WHERE id = " & id_veicolo
        HttpContext.Current.Trace.Write("getModelloDaIdVeicolo: " & sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getModelloDaIdVeicolo = Cmd.ExecuteScalar()
                HttpContext.Current.Trace.Write("getModelloDaIdVeicolo: " & getModelloDaIdVeicolo)
            End Using
        End Using
    End Function

    Private Function SalvaDanni() As Integer
        Dim sqlStr As String = "INSERT INTO veicoli_gruppo_danni (id_danno,id_veicolo,id_evento_apertura,data_creazione,id_utente,data_modifica,id_utente_modifica,tipo_record,id_posizione_danno,id_tipo_danno,entita_danno,descrizione,id_dotazione,id_acessori,id_ditta)" &
            " SELECT id,id_veicolo," & id & ",data_creazione,id_utente,data_modifica,id_utente_modifica,tipo_record,id_posizione_danno,id_tipo_danno,entita_danno,descrizione,id_dotazione,id_acessori,id_ditta" &
            " FROM veicoli_danni WITH(NOLOCK)" &
            " WHERE id_veicolo = " & id_veicolo &
            " AND stato = 1" &
            " AND attivo = 1" &
            " AND tipo_record NOT IN (4,5)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                SalvaDanni = Cmd.ExecuteNonQuery
            End Using
        End Using
    End Function

    Public Shared Function PulisciAssociazioneODLSuDanni(ByVal id_gruppo As Integer) As Integer
        Dim sqlStr As String = "UPDATE veicoli_danni SET" &
            " num_odl = NULL" &
            " FROM veicoli_gruppo_danni gd WITH(NOLOCK)" &
            " INNER JOIN veicoli_danni d WITH(NOLOCK) ON d.id = gd.id_danno" &
            " WHERE gd.id_evento_apertura = " & id_gruppo &
            " AND d.stato = 1"
        HttpContext.Current.Trace.Write("PulisciAssociazioneODLSuDanni: " & sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                PulisciAssociazioneODLSuDanni = Cmd.ExecuteNonQuery
            End Using
        End Using
    End Function

    Public Shared Function AggiungiDanno(ByVal id_gruppo As Integer, ByVal id_danno As Integer) As Integer
        Dim sqlStr As String = "INSERT INTO veicoli_gruppo_danni (id_danno,id_veicolo,id_evento_apertura,data_creazione,id_utente,data_modifica,id_utente_modifica,tipo_record,id_posizione_danno,id_tipo_danno,entita_danno,descrizione,id_dotazione,id_acessori,id_ditta)" &
            " SELECT id,id_veicolo," & id_gruppo & ",data_creazione,id_utente,data_modifica,id_utente_modifica,tipo_record,id_posizione_danno,id_tipo_danno,entita_danno,descrizione,id_dotazione,id_acessori,id_ditta" &
            " FROM veicoli_danni WITH(NOLOCK)" &
            " WHERE id = " & id_danno

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                AggiungiDanno = Cmd.ExecuteNonQuery
            End Using
        End Using
    End Function

    Public Shared Function EliminaDanniNonAttivo(ByVal id_gruppo As Integer) As Integer
        Dim sqlStr As String = "DELETE gd" &
            " FROM veicoli_gruppo_danni gd WITH(NOLOCK)" &
            " INNER JOIN veicoli_danni d WITH(NOLOCK) ON d.id = gd.id_danno" &
            " WHERE gd.id_evento_apertura = " & id_gruppo &
            " AND d.attivo = 0"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                EliminaDanniNonAttivo = Cmd.ExecuteNonQuery
            End Using
        End Using
    End Function

    Private Function SalvaDotazioni() As Integer
        ' tipo_record = 4 = Dotazione!
        Dim sqlStr As String = "INSERT INTO veicoli_gruppo_accessori (id_evento_apertura,id_veicolo,id_accessorio,assente)" &
            " SELECT " & id & ",va.id_veicolo,va.id_accessorio,ISNULL(d.attivo,0)" &
            " FROM accessori_veicoli va WITH(NOLOCK)" &
            " INNER JOIN accessori a WITH(NOLOCK) ON a.id = va.id_accessorio AND a.dotazione = 1" &
            " LEFT JOIN veicoli_danni d WITH(NOLOCK) ON d.id_veicolo = va.id_veicolo AND d.attivo = 1 AND d.stato = 1 AND d.tipo_record = 4 AND d.id_dotazione = va.id_accessorio" &
            " WHERE va.id_veicolo = " & id_veicolo

        HttpContext.Current.Trace.Write("SalvaDotazioni: " & sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                SalvaDotazioni = Cmd.ExecuteNonQuery
            End Using
        End Using

        'sqlStr = "INSERT INTO veicoli_gruppo_danni (id_danno,id_veicolo,id_evento_apertura,data_creazione,id_utente,data_modifica,id_utente_modifica,tipo_record,id_posizione_danno,id_tipo_danno,entita_danno,descrizione,id_dotazione,id_acessori,id_ditta)" & _
        '    " SELECT id,id_veicolo," & id & ",data_creazione,id_utente,data_modifica,id_utente_modifica,tipo_record,id_posizione_danno,id_tipo_danno,entita_danno,descrizione,id_dotazione,id_acessori,id_ditta" & _
        '    " FROM veicoli_danni WITH(NOLOCK)" & _
        '    " WHERE id_veicolo = " & id_veicolo & _
        '    " AND stato = 1" & _
        '    " AND attivo = 1" & _
        '    " AND tipo_record  IN (4)"

        'Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        '    Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
        '        Dbc.Open()
        '        SalvaDotazioni = Cmd.ExecuteNonQuery
        '    End Using
        'End Using
    End Function

    Private Function SalvaAcessori(ByVal id_contratto As Integer, ByVal id_evento_apertura_danno As Integer) As Integer
        ' tipo_record = 5 = Accessori!
        Dim sqlStr As String = "INSERT INTO veicoli_gruppo_accessori_contratto (id_evento_apertura,id_veicolo,id_accessorio,assente)" &
            " SELECT " & id & ", c.id_veicolo, ce.id, CASE WHEN (d.id IS NULL) THEN 0 ELSE 1 END  Assente" &
            " FROM contratti_costi cc WITH(NOLOCK)" &
            " INNER JOIN contratti c WITH(NOLOCK) ON c.id = cc.id_documento " &
            " INNER JOIN condizioni_elementi ce WITH(NOLOCK) ON cc.id_elemento = ce.id " &
            " LEFT JOIN veicoli_danni d WITH(NOLOCK) ON d.id_veicolo = c.id_veicolo AND d.attivo = 1 AND d.stato = 1" &
            " AND d.tipo_record = 5 AND d.id_acessori = ce.id AND d.id_evento_apertura = @id_evento_apertura" &
            " WHERE (c.id = @id_contratto) " &
            " AND (cc.id_a_carico_di = @id_a_carico_del_cliente) " &
            " AND (cc.obbligatorio = '0') " &
            " AND (cc.selezionato = '1')" &
            " AND (ce.accessorio_check = '1')"
        HttpContext.Current.Trace.Write("SalvaAcessori: " & sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_a_carico_del_cliente", System.Data.SqlDbType.Int, Costanti.id_a_carico_del_cliente)
                addParametro(Cmd, "@id_contratto", System.Data.SqlDbType.Int, id_contratto)
                addParametro(Cmd, "@id_evento_apertura", System.Data.SqlDbType.Int, id_evento_apertura_danno)
                Dbc.Open()
                SalvaAcessori = Cmd.ExecuteNonQuery
            End Using
        End Using
    End Function

    Public Function AggiornaAcessori(ByVal id_contratto As Integer, ByVal id_evento_apertura_danno As Integer) As Integer
        Dim sqlStr As String

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            sqlStr = "DELETE FROM veicoli_gruppo_accessori_contratto" &
                " WHERE id_evento_apertura = " & id
            HttpContext.Current.Trace.Write("AggiornaAcessori: " & sqlStr)
            Dbc.Open()
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteNonQuery()
            End Using

            ' tipo_record = 5 = Accessori!
            sqlStr = "INSERT INTO veicoli_gruppo_accessori_contratto (id_evento_apertura,id_veicolo,id_accessorio,assente)" &
                " SELECT " & id & ", c.id_veicolo, ce.id, CASE WHEN (d.id IS NULL) THEN 0 ELSE 1 END  Assente" &
                " FROM contratti_costi cc WITH(NOLOCK)" &
                " INNER JOIN contratti c WITH(NOLOCK) ON c.id = cc.id_documento " &
                " INNER JOIN condizioni_elementi ce WITH(NOLOCK) ON cc.id_elemento = ce.id " &
                " LEFT JOIN veicoli_danni d WITH(NOLOCK) ON d.id_veicolo = c.id_veicolo AND d.attivo = 1 AND d.stato = 1" &
                " AND d.tipo_record = 5 AND d.id_acessori = ce.id AND d.id_evento_apertura = @id_evento_apertura" &
                " WHERE (c.id = @id_contratto) " &
                " AND (cc.id_a_carico_di = @id_a_carico_del_cliente) " &
                " AND (cc.obbligatorio = '0') " &
                " AND (cc.selezionato = '1')" &
                " AND (ce.accessorio_check = '1')"
            HttpContext.Current.Trace.Write("AggiornaAcessori: " & sqlStr)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_a_carico_del_cliente", System.Data.SqlDbType.Int, Costanti.id_a_carico_del_cliente)
                addParametro(Cmd, "@id_contratto", System.Data.SqlDbType.Int, id_contratto)
                addParametro(Cmd, "@id_evento_apertura", System.Data.SqlDbType.Int, id_evento_apertura_danno)

                AggiornaAcessori = Cmd.ExecuteNonQuery
            End Using
        End Using
    End Function

    Public Shared Function SalvaDanniApertiAuto(ByVal id_veicolo As Integer, ByVal id_tipo_documento As tipo_documento, ByVal id_documento As Integer, ByVal numero_crv As Integer, ByVal id_evento_danno As Integer) As veicoli_gruppo_evento

        ' Dim NumeroDanni As Integer = getNumeroDanni(id_veicolo)
        Dim id_modello As Integer = getModelloDaIdVeicolo(id_veicolo)

        Dim mio_contratto As DatiContratto = Nothing

        Select Case id_tipo_documento
            Case tipo_documento.Contratto
                mio_contratto = DatiContratto.getRecordDaNumContratto(id_documento, numero_crv)
                If mio_contratto Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: contratto non trovato.")
                End If

            Case tipo_documento.MovimentoInterno, tipo_documento.ODL, tipo_documento.DuranteODL, tipo_documento.Lavaggio
                ' non utilizzo il record...
                'mio_contratto = DatiContratto.getRecordDaNumTrasferimento(id_documento)
                'If mio_contratto Is Nothing Then
                '    Err.Raise(1001, Nothing, "Errore: movimento interno non trovato.")
                'End If

            Case Is >= tipo_documento.RDSGenerico
                ' sono nel caso di RDS aperti ma non legati ad un documento!

            Case Else
                Err.Raise(1001, Nothing, "Tipo documento non previsto")
        End Select

        Dim mio_record As veicoli_gruppo_evento = New veicoli_gruppo_evento
        With mio_record
            .id_veicolo = id_veicolo
            .id_modello = id_modello
            .id_tipo_documento_apertura = id_tipo_documento
            .id_documento_apertura = id_documento
            .id_evento = id_evento_danno

            .SalvaRecord()

            .SalvaDanni()

            .SalvaDotazioni()

            If id_tipo_documento = tipo_documento.Contratto Then
                .SalvaAcessori(mio_contratto.id, id_evento_danno)
            End If

        End With
        Return mio_record

    End Function

    Public Function EliminaRecord() As Boolean
        Return EliminaRecord(Me.id)
    End Function

    Public Shared Function EliminaRecord(ByVal id_record As Integer) As Boolean
        EliminaRecord = False

        Dim sqlStr As String

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                sqlStr = "DELETE FROM [veicoli_gruppo_accessori_contratto] WHERE id_evento_apertura = " & id_record
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.ExecuteNonQuery()
                End Using

                sqlStr = "DELETE FROM [veicoli_gruppo_accessori] WHERE id_evento_apertura = " & id_record
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.ExecuteNonQuery()
                End Using

                sqlStr = "DELETE FROM [veicoli_gruppo_danni] WHERE id_evento_apertura = " & id_record
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.ExecuteNonQuery()
                End Using

                sqlStr = "DELETE FROM [veicoli_gruppo_evento] WHERE id = " & id_record
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.ExecuteNonQuery()
                End Using

                EliminaRecord = True
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord: " & ex.Message)
        End Try

    End Function
End Class

Public Class veicoli_gruppo_danni
    Inherits ITabellaDB

    Const NomeTabella = "[veicoli_gruppo_danni]"

    Protected m_id As Integer
    Protected m_id_danno As Integer
    Protected m_id_veicolo As Integer
    Protected m_id_evento_apertura As Integer
    Protected m_data_creazione As DateTime
    Protected m_id_utente As Integer
    Protected m_data_modifica As DateTime?
    Protected m_id_utente_modifica As Integer?
    Protected m_tipo_record As Integer
    Protected m_id_posizione_danno As Integer?
    Protected m_id_tipo_danno As Integer?
    Protected m_entita_danno As Integer?
    Protected m_descrizione As String
    Protected m_id_dotazione As Integer?
    Protected m_id_acessori As Integer?

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property id_danno() As Integer
        Get
            Return m_id_danno
        End Get
        Set(ByVal value As Integer)
            m_id_danno = value
        End Set
    End Property
    Public Property id_veicolo() As Integer
        Get
            Return m_id_veicolo
        End Get
        Set(ByVal value As Integer)
            m_id_veicolo = value
        End Set
    End Property
    Public Property id_evento_apertura() As Integer
        Get
            Return m_id_evento_apertura
        End Get
        Set(ByVal value As Integer)
            m_id_evento_apertura = value
        End Set
    End Property
    Public Property data_creazione() As DateTime
        Get
            Return m_data_creazione
        End Get
        Set(ByVal value As DateTime)
            m_data_creazione = value
        End Set
    End Property
    Public Property id_utente() As Integer
        Get
            Return m_id_utente
        End Get
        Set(ByVal value As Integer)
            m_id_utente = value
        End Set
    End Property
    Public Property data_modifica() As DateTime?
        Get
            Return m_data_modifica
        End Get
        Set(ByVal value As DateTime?)
            m_data_modifica = value
        End Set
    End Property
    Public Property id_utente_modifica() As Integer?
        Get
            Return m_id_utente_modifica
        End Get
        Set(ByVal value As Integer?)
            m_id_utente_modifica = value
        End Set
    End Property
    Public Property tipo_record() As tipo_record_danni
        Get
            Return m_tipo_record
        End Get
        Set(ByVal value As tipo_record_danni)
            m_tipo_record = value
        End Set
    End Property
    Public Property id_posizione_danno() As Integer?
        Get
            Return m_id_posizione_danno
        End Get
        Set(ByVal value As Integer?)
            m_id_posizione_danno = value
        End Set
    End Property
    Public Property id_tipo_danno() As Integer?
        Get
            Return m_id_tipo_danno
        End Get
        Set(ByVal value As Integer?)
            m_id_tipo_danno = value
        End Set
    End Property
    Public Property entita_danno() As Integer?
        Get
            Return m_entita_danno
        End Get
        Set(ByVal value As Integer?)
            m_entita_danno = value
        End Set
    End Property
    Public Property descrizione() As String
        Get
            Return m_descrizione
        End Get
        Set(ByVal value As String)
            m_descrizione = value
        End Set
    End Property
    Public Property id_dotazione() As Integer?
        Get
            Return m_id_dotazione
        End Get
        Set(ByVal value As Integer?)
            m_id_dotazione = value
        End Set
    End Property
    Public Property id_acessori() As Integer?
        Get
            Return m_id_acessori
        End Get
        Set(ByVal value As Integer?)
            m_id_acessori = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO " & NomeTabella & " (id_danno,id_veicolo,id_evento_apertura,data_creazione,id_utente,data_modifica,id_utente_modifica,id_posizione_danno,id_tipo_danno,entita_danno,descrizione,id_dotazione,id_acessori)" &
            " VALUES (@id_danno,@id_veicolo,@id_evento_apertura,@data_creazione,@id_utente,@data_modifica,@id_utente_modifica,@id_posizione_danno,@id_tipo_danno,@entita_danno,@descrizione,@id_dotazione,@id_acessori)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_danno", System.Data.SqlDbType.Int, id_danno)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                addParametro(Cmd, "@id_evento_apertura", System.Data.SqlDbType.Int, id_evento_apertura)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)
                addParametro(Cmd, "@data_modifica", System.Data.SqlDbType.DateTime, data_modifica)
                addParametro(Cmd, "@id_utente_modifica", System.Data.SqlDbType.Int, id_utente_modifica)
                addParametro(Cmd, "@id_posizione_danno", System.Data.SqlDbType.Int, id_posizione_danno)
                addParametro(Cmd, "@id_tipo_danno", System.Data.SqlDbType.Int, id_tipo_danno)
                addParametro(Cmd, "@entita_danno", System.Data.SqlDbType.Int, entita_danno)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione))
                addParametro(Cmd, "@id_dotazione", System.Data.SqlDbType.Int, id_dotazione)
                addParametro(Cmd, "@id_acessori", System.Data.SqlDbType.Int, id_acessori)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM " & NomeTabella

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_gruppo_danni
        Dim mio_record As veicoli_gruppo_danni = New veicoli_gruppo_danni
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .id_danno = getValueOrNohing(Rs("id_danno"))
            .id_veicolo = getValueOrNohing(Rs("id_veicolo"))
            .id_evento_apertura = getValueOrNohing(Rs("id_evento_apertura"))
            .data_creazione = getValueOrNohing(Rs("data_creazione"))
            .id_utente = getValueOrNohing(Rs("id_utente"))
            .data_modifica = getValueOrNohing(Rs("data_modifica"))
            .id_utente_modifica = getValueOrNohing(Rs("id_utente_modifica"))
            .id_posizione_danno = getValueOrNohing(Rs("id_posizione_danno"))
            .id_tipo_danno = getValueOrNohing(Rs("id_tipo_danno"))
            .entita_danno = getValueOrNohing(Rs("entita_danno"))
            .descrizione = getValueOrNohing(Rs("descrizione"))
            .id_dotazione = getValueOrNohing(Rs("id_dotazione"))
            .id_acessori = getValueOrNohing(Rs("id_acessori"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As veicoli_gruppo_danni
        Dim mio_record As veicoli_gruppo_danni = Nothing

        Dim sqlStr As String = "SELECT * FROM " & NomeTabella & " WITH(NOLOCK) WHERE id = " & id_record

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

        Dim sqlStr As String = "UPDATE " & NomeTabella & " SET" &
            " id_danno = @id_danno," &
            " id_veicolo = @id_veicolo," &
            " id_evento_apertura = @id_evento_apertura," &
            " data_creazione = @data_creazione," &
            " id_utente = @id_utente," &
            " data_modifica = @data_modifica," &
            " id_utente_modifica = @id_utente_modifica," &
            " id_posizione_danno = @id_posizione_danno," &
            " id_tipo_danno = @id_tipo_danno," &
            " entita_danno = @entita_danno," &
            " id_dotazione = @id_dotazione," &
            " id_acessori = @id_acessori," &
            " descrizione = @descrizione" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_danno", System.Data.SqlDbType.Int, id_danno)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                addParametro(Cmd, "@id_evento_apertura", System.Data.SqlDbType.Int, id_evento_apertura)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)
                addParametro(Cmd, "@data_modifica", System.Data.SqlDbType.DateTime, data_modifica)
                addParametro(Cmd, "@id_utente_modifica", System.Data.SqlDbType.Int, id_utente_modifica)
                addParametro(Cmd, "@id_posizione_danno", System.Data.SqlDbType.Int, id_posizione_danno)
                addParametro(Cmd, "@id_tipo_danno", System.Data.SqlDbType.Int, id_tipo_danno)
                addParametro(Cmd, "@entita_danno", System.Data.SqlDbType.Int, entita_danno)
                addParametro(Cmd, "@id_dotazione", System.Data.SqlDbType.Int, id_dotazione)
                addParametro(Cmd, "@id_acessori", System.Data.SqlDbType.Int, id_acessori)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione))

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

        Dim sqlStr As String = "DELETE FROM " & NomeTabella & " WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord " & NomeTabella & ": " & ex.Message)
        End Try
    End Function
End Class


Public Class veicoli_check_in
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_id_tipo_documento As Integer
    Protected m_id_documento As Integer
    Protected m_num_crv As Integer? = 0
    Protected m_data_ready_to_go As Datetime?
    Protected m_data_lavaggio As Datetime?
    Protected m_data_rifornimento As DateTime?
    Protected m_fermo_tecnico As Boolean?
    Protected m_vendita_buy_back As Boolean?
    Protected m_altro As String
    Protected m_km_rientro As Integer?
    Protected m_litri_rientro_frazione As Integer?
    Protected m_data_creazione As DateTime
    Protected m_id_utente As Integer

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property id_tipo_documento() As Integer
        Get
            Return m_id_tipo_documento
        End Get
        Set(ByVal value As Integer)
            m_id_tipo_documento = value
        End Set
    End Property
    Public Property id_documento() As Integer
        Get
            Return m_id_documento
        End Get
        Set(ByVal value As Integer)
            m_id_documento = value
        End Set
    End Property
    Public Property num_crv() As Integer
        Get
            Return m_num_crv
        End Get
        Set(ByVal value As Integer)
            m_num_crv = value
        End Set
    End Property
    Public Property data_ready_to_go() As Datetime?
        Get
            Return m_data_ready_to_go
        End Get
        Set(ByVal value As Datetime?)
            m_data_ready_to_go = value
        End Set
    End Property
    Public Property data_lavaggio() As Datetime?
        Get
            Return m_data_lavaggio
        End Get
        Set(ByVal value As Datetime?)
            m_data_lavaggio = value
        End Set
    End Property
    Public Property data_rifornimento() As Datetime?
        Get
            Return m_data_rifornimento
        End Get
        Set(ByVal value As Datetime?)
            m_data_rifornimento = value
        End Set
    End Property
    Public Property fermo_tecnico() As Boolean?
        Get
            Return m_fermo_tecnico
        End Get
        Set(ByVal value As Boolean?)
            m_fermo_tecnico = value
        End Set
    End Property
    Public Property in_vendita_buy_back() As Boolean?
        Get
            Return m_vendita_buy_back
        End Get
        Set(ByVal value As Boolean?)
            m_vendita_buy_back = value
        End Set
    End Property
    Public Property altro() As String
        Get
            Return m_altro
        End Get
        Set(ByVal value As String)
            m_altro = value
        End Set
    End Property
    Public Property km_rientro() As Integer?
        Get
            Return m_km_rientro
        End Get
        Set(ByVal value As Integer?)
            m_km_rientro = value
        End Set
    End Property
    Public Property litri_rientro_frazione() As Integer?
        Get
            Return m_litri_rientro_frazione
        End Get
        Set(ByVal value As Integer?)
            m_litri_rientro_frazione = value
        End Set
    End Property
    Public ReadOnly Property data_creazione() As DateTime
        Get
            Return m_data_creazione
        End Get
    End Property
    Public ReadOnly Property id_utente() As Integer
        Get
            Return m_id_utente
        End Get
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO [veicoli_check_in] (id_tipo_documento,id_documento,num_crv,data_ready_to_go,data_lavaggio,data_rifornimento,fermo_tecnico,in_vendita_buy_back,altro,km_rientro,litri_rientro_frazione,data_creazione,id_utente)" &
            " VALUES (@id_tipo_documento,@id_documento,@num_crv,@data_ready_to_go,@data_lavaggio,@data_rifornimento,@fermo_tecnico,@in_vendita_buy_back,@altro,@km_rientro,@litri_rientro_frazione,@data_creazione,@id_utente)"

        m_data_creazione = Now
        m_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_tipo_documento", System.Data.SqlDbType.Int, id_tipo_documento)
                addParametro(Cmd, "@id_documento", System.Data.SqlDbType.Int, id_documento)
                addParametro(Cmd, "@num_crv", System.Data.SqlDbType.Int, num_crv)
                addParametro(Cmd, "@data_ready_to_go", System.Data.SqlDbType.DateTime, data_ready_to_go)
                addParametro(Cmd, "@data_lavaggio", System.Data.SqlDbType.DateTime, data_lavaggio)
                addParametro(Cmd, "@data_rifornimento", System.Data.SqlDbType.DateTime, data_rifornimento)
                addParametro(Cmd, "@fermo_tecnico", System.Data.SqlDbType.Bit, fermo_tecnico)
                addParametro(Cmd, "@in_vendita_buy_back", System.Data.SqlDbType.Bit, in_vendita_buy_back)
                addParametro(Cmd, "@altro", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(altro))
                addParametro(Cmd, "@km_rientro", System.Data.SqlDbType.Int, km_rientro)
                addParametro(Cmd, "@litri_rientro_frazione", System.Data.SqlDbType.Int, litri_rientro_frazione)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM [veicoli_check_in]"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_check_in
        Dim mio_record As veicoli_check_in = New veicoli_check_in
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .id_tipo_documento = getValueOrNohing(Rs("id_tipo_documento"))
            .id_documento = getValueOrNohing(Rs("id_documento"))
            .num_crv = getValueOrNohing(Rs("num_crv"))
            .data_ready_to_go = getValueOrNohing(Rs("data_ready_to_go"))
            .data_lavaggio = getValueOrNohing(Rs("data_lavaggio"))
            .data_rifornimento = getValueOrNohing(Rs("data_rifornimento"))
            .fermo_tecnico = getValueOrNohing(Rs("fermo_tecnico"))
            .in_vendita_buy_back = getValueOrNohing(Rs("in_vendita_buy_back"))
            .altro = getValueOrNohing(Rs("altro"))
            .km_rientro = getValueOrNohing(Rs("km_rientro"))
            .litri_rientro_frazione = getValueOrNohing(Rs("litri_rientro_frazione"))
            .m_data_creazione = getValueOrNohing(Rs("data_creazione"))
            .m_id_utente = getValueOrNohing(Rs("id_utente"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As veicoli_check_in
        Dim mio_record As veicoli_check_in = Nothing

        Dim sqlStr As String = "SELECT * FROM [veicoli_check_in] WITH(NOLOCK) WHERE id = " & id_record

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


    Public Shared Function getRecordDaDocumento(ByVal id_tipo_documento As tipo_documento, ByVal id_documento As Integer, ByVal num_crv As Integer) As veicoli_check_in
        Dim mio_record As veicoli_check_in = Nothing

        Dim sqlStr As String = "SELECT * FROM [veicoli_check_in] WITH(NOLOCK)" &
            " WHERE id_tipo_documento = " & id_tipo_documento &
            " AND id_documento = " & id_documento &
            " AND num_crv = " & num_crv

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

        Dim sqlStr As String = "UPDATE [veicoli_check_in] SET" &
            " data_ready_to_go = @data_ready_to_go," &
            " data_lavaggio = @data_lavaggio," &
            " data_rifornimento = @data_rifornimento," &
            " fermo_tecnico = @fermo_tecnico," &
            " altro = @altro," &
            " km_rientro = @km_rientro," &
            " litri_rientro_frazione = @litri_rientro_frazione" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@data_ready_to_go", System.Data.SqlDbType.DateTime, data_ready_to_go)
                addParametro(Cmd, "@data_lavaggio", System.Data.SqlDbType.DateTime, data_lavaggio)
                addParametro(Cmd, "@data_rifornimento", System.Data.SqlDbType.DateTime, data_rifornimento)
                addParametro(Cmd, "@fermo_tecnico", System.Data.SqlDbType.Bit, fermo_tecnico)
                addParametro(Cmd, "@altro", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(altro))
                addParametro(Cmd, "@km_rientro", System.Data.SqlDbType.Int, km_rientro)
                addParametro(Cmd, "@litri_rientro_frazione", System.Data.SqlDbType.Int, litri_rientro_frazione)

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

        Dim sqlStr As String = "DELETE FROM [veicoli_check_in] WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord [veicoli_check_in]: " & ex.Message)
        End Try
    End Function
End Class

<SerializableAttribute()>
Public Class DatiContratto
    Inherits ITabellaDB

    Protected m_tipo_documento_origine As tipo_documento

    Protected m_id As Integer
    Protected m_num_calcolo As Integer
    Protected m_status As Integer
    Protected m_num_contratto As Integer?
    Protected m_num_crv As Integer?
    Protected m_attivo As Boolean
    Protected m_id_stazione_uscita As Integer?
    Protected m_id_stazione_rientro As Integer?
    Protected m_data_uscita As DateTime?
    Protected m_data_rientro As DateTime?

    Protected m_data_rientro_previsto As DateTime?      'aggiunto x ckout data_rientro_previsto 21.06.2022 salvo

    Protected m_id_primo_conducente As Integer?
    Protected m_id_secondo_conducente As Integer?
    Protected m_id_veicolo As Integer?
    Protected m_targa As String
    Protected m_km_uscita As Integer?
    Protected m_km_rientro As Integer?
    Protected m_litri_uscita As Integer?
    Protected m_litri_rientro As Integer?
    Protected m_id_gruppo_danni_uscita As Integer?
    Protected m_id_gruppo_danni_rientro As Integer?

    Protected m_num_prenotazione As Integer?

    Protected m_id_cliente As Integer?

    Protected m_disponibile_nolo As Boolean
    Protected m_noleggiata As Boolean
    Protected m_da_lavare As Boolean
    Protected m_da_rifornire As Boolean
    Protected m_da_riparare As Boolean?
    Protected m_in_vendita_buy_back As Boolean?

    Protected m_importo_a_carico_del_broker As Double?
    Protected m_tipo_tariffa As String
    Protected m_CODTAR As String
    Protected m_id_tariffe_righe As Integer?

    Protected m_totale_da_incassare As Double?

    Public ReadOnly Property tipo_documento_origine As tipo_documento
        Get
            Return m_tipo_documento_origine
        End Get
    End Property
    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public ReadOnly Property status() As Integer
        Get
            Return m_status
        End Get
    End Property
    Public ReadOnly Property num_calcolo() As Integer
        Get
            Return m_num_calcolo
        End Get
    End Property
    Public Property num_contratto() As Integer?
        Get
            Return m_num_contratto
        End Get
        Set(ByVal value As Integer?)
            m_num_contratto = value
        End Set
    End Property
    Public Property num_crv() As Integer?
        Get
            Return m_num_crv
        End Get
        Set(ByVal value As Integer?)
            m_num_crv = value
        End Set
    End Property
    Public Property attivo() As Boolean
        Get
            Return m_attivo
        End Get
        Set(ByVal value As Boolean)
            m_attivo = value
        End Set
    End Property
    Public Property id_stazione_uscita() As Integer?
        Get
            Return m_id_stazione_uscita
        End Get
        Set(ByVal value As Integer?)
            m_id_stazione_uscita = value
        End Set
    End Property
    Public Property id_stazione_rientro() As Integer?
        Get
            Return m_id_stazione_rientro
        End Get
        Set(ByVal value As Integer?)
            m_id_stazione_rientro = value
        End Set
    End Property
    Public Property data_uscita() As DateTime?
        Get
            Return m_data_uscita
        End Get
        Set(ByVal value As DateTime?)
            m_data_uscita = value
        End Set
    End Property
    Public Property data_rientro() As DateTime?
        Get
            Return m_data_rientro
        End Get
        Set(ByVal value As DateTime?)
            m_data_rientro = value
        End Set
    End Property
    Public Property data_rientro_previsto() As DateTime?    'aggiunto 21.06.2022 salvo

        Get
            Return m_data_rientro_previsto
        End Get
        Set(ByVal value As DateTime?)
            m_data_rientro_previsto = value
        End Set

    End Property

    Public Property id_primo_conducente() As Integer?
        Get
            Return m_id_primo_conducente
        End Get
        Set(ByVal value As Integer?)
            m_id_primo_conducente = value
        End Set
    End Property
    Public Property id_secondo_conducente() As Integer?
        Get
            Return m_id_secondo_conducente
        End Get
        Set(ByVal value As Integer?)
            m_id_secondo_conducente = value
        End Set
    End Property
    Public Property id_veicolo() As Integer?
        Get
            Return m_id_veicolo
        End Get
        Set(ByVal value As Integer?)
            m_id_veicolo = value
        End Set
    End Property
    Public Property targa() As String
        Get
            Return m_targa
        End Get
        Set(ByVal value As String)
            m_targa = value
        End Set
    End Property
    Public Property km_uscita() As Integer?
        Get
            Return m_km_uscita
        End Get
        Set(ByVal value As Integer?)
            m_km_uscita = value
        End Set
    End Property
    Public Property km_rientro() As Integer?
        Get
            Return m_km_rientro
        End Get
        Set(ByVal value As Integer?)
            m_km_rientro = value
        End Set
    End Property
    Public Property litri_uscita() As Integer?
        Get
            Return m_litri_uscita
        End Get
        Set(ByVal value As Integer?)
            m_litri_uscita = value
        End Set
    End Property
    Public Property litri_rientro() As Integer?
        Get
            Return m_litri_rientro
        End Get
        Set(ByVal value As Integer?)
            m_litri_rientro = value
        End Set
    End Property
    Public Property id_gruppo_danni_uscita() As Integer?
        Get
            Return m_id_gruppo_danni_uscita
        End Get
        Set(ByVal value As Integer?)
            m_id_gruppo_danni_uscita = value
        End Set
    End Property
    Public Property id_gruppo_danni_rientro() As Integer?
        Get
            Return m_id_gruppo_danni_rientro
        End Get
        Set(ByVal value As Integer?)
            m_id_gruppo_danni_rientro = value
        End Set
    End Property

    Public Property id_cliente() As Integer?
        Get
            Return m_id_cliente
        End Get
        Set(ByVal value As Integer?)
            m_id_cliente = value
        End Set
    End Property

    Public Property disponibile_nolo() As Boolean
        Get
            Return m_disponibile_nolo
        End Get
        Set(ByVal value As Boolean)
            m_disponibile_nolo = value
        End Set
    End Property
    Public Property noleggiata() As Boolean
        Get
            Return m_noleggiata
        End Get
        Set(ByVal value As Boolean)
            m_noleggiata = value
        End Set
    End Property
    Public Property da_lavare() As Boolean
        Get
            Return m_da_lavare
        End Get
        Set(ByVal value As Boolean)
            m_da_lavare = value
        End Set
    End Property
    Public Property da_rifornire() As Boolean
        Get
            Return m_da_rifornire
        End Get
        Set(ByVal value As Boolean)
            m_da_rifornire = value
        End Set
    End Property

    Public Property da_riparare() As Boolean?
        Get
            Return m_da_riparare
        End Get
        Set(ByVal value As Boolean?)
            m_da_riparare = value
        End Set
    End Property
    Public Property in_vendita_buy_back() As Boolean?
        Get
            Return m_in_vendita_buy_back
        End Get
        Set(ByVal value As Boolean?)
            m_in_vendita_buy_back = value
        End Set
    End Property
    Public Property importo_a_carico_del_broker() As Double?
        Get
            Return m_importo_a_carico_del_broker
        End Get
        Set(ByVal value As Double?)
            m_importo_a_carico_del_broker = value
        End Set
    End Property
    Public Property tipo_tariffa() As String
        Get
            Return m_tipo_tariffa
        End Get
        Set(ByVal value As String)
            m_tipo_tariffa = value
        End Set
    End Property
    Public Property CODTAR() As String
        Get
            Return m_CODTAR
        End Get
        Set(ByVal value As String)
            m_CODTAR = value
        End Set
    End Property
    Public Property id_tariffe_righe() As Integer?
        Get
            Return m_id_tariffe_righe
        End Get
        Set(ByVal value As Integer?)
            m_id_tariffe_righe = value
        End Set
    End Property
    Public Property num_prenotazione() As Integer?
        Get
            Return m_num_prenotazione
        End Get
        Set(ByVal value As Integer?)
            m_num_prenotazione = value
        End Set
    End Property
    Public Property totale_da_incassare() As Double?
        Get
            Return m_totale_da_incassare
        End Get
        Set(ByVal value As Double?)
            m_totale_da_incassare = value
        End Set
    End Property

    Public Sub MioTrace()
        HttpContext.Current.Trace.Write("m_tipo_documento_origine =  " & m_tipo_documento_origine.ToString & vbCrLf &
            "m_id =  " & m_id & vbCrLf &
            "m_num_calcolo =  " & m_num_calcolo & vbCrLf &
            "m_status =  " & m_status & vbCrLf &
            "m_num_contratto =  " & m_num_contratto & vbCrLf &
            "m_num_crv =  " & m_num_crv & vbCrLf &
            "num_prenotazione = " & m_num_prenotazione & vbCrLf &
            "m_attivo =  " & m_attivo & vbCrLf &
            "m_id_stazione_uscita =  " & m_id_stazione_uscita & vbCrLf &
            "m_id_stazione_rientro =  " & m_id_stazione_rientro & vbCrLf &
            "m_data_uscita =  " & m_data_uscita & vbCrLf &
            "m_data_rientro =  " & m_data_rientro & vbCrLf &
            "m_data_rientro_previsto =  " & m_data_rientro_previsto & vbCrLf &
            "m_id_primo_conducente =  " & m_id_primo_conducente & vbCrLf &
            "m_id_secondo_conducente =  " & m_id_secondo_conducente & vbCrLf &
            "m_id_veicolo =  " & m_id_veicolo & vbCrLf &
            "m_targa =  " & m_targa & vbCrLf &
            "m_km_uscita =  " & m_km_uscita & vbCrLf &
            "m_km_rientro =  " & m_km_rientro & vbCrLf &
            "m_litri_uscita =  " & m_litri_uscita & vbCrLf &
            "m_litri_rientro =  " & m_litri_rientro & vbCrLf &
            "m_id_gruppo_danni_uscita =  " & m_id_gruppo_danni_uscita & vbCrLf &
            "m_id_gruppo_danni_rientro =  " & m_id_gruppo_danni_rientro & vbCrLf &
            "m_id_cliente =  " & m_id_cliente & vbCrLf &
            "m_disponibile_nolo =  " & m_disponibile_nolo & vbCrLf &
            "m_noleggiata =  " & m_noleggiata & vbCrLf &
            "m_da_lavare =  " & m_da_lavare & vbCrLf &
            "m_da_rifornire =  " & m_da_rifornire & vbCrLf &
            "m_da_riparare =  " & m_da_riparare & vbCrLf &
            "m_importo_a_carico_del_broker =  " & m_importo_a_carico_del_broker & vbCrLf &
            "m_tipo_tariffa =  " & m_tipo_tariffa & vbCrLf &
            "m_CODTAR =  " & m_CODTAR & vbCrLf &
            "m_id_tariffe_righe =  " & m_id_tariffe_righe & vbCrLf &
            "m_totale_da_incassare = " & m_totale_da_incassare)
    End Sub

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As DatiContratto
        Dim mio_record As DatiContratto = New DatiContratto
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .m_num_calcolo = getValueOrNohing(Rs("num_calcolo"))
            .m_status = getValueOrNohing(Rs("status"))
            .num_contratto = getValueOrNohing(Rs("num_contratto"))
            .num_crv = getValueOrNohing(Rs("num_crv"))
            .attivo = getValueOrNohing(Rs("attivo"))
            .id_stazione_uscita = getValueOrNohing(Rs("id_stazione_uscita"))
            .id_stazione_rientro = getValueOrNohing(Rs("id_stazione_rientro"))
            If .id_stazione_rientro Is Nothing Then
                .id_stazione_rientro = .id_stazione_uscita
            End If
            .data_uscita = getValueOrNohing(Rs("data_uscita"))
            .data_rientro = getValueOrNohing(Rs("data_rientro"))

            .data_rientro_previsto = getValueOrNohing(Rs("data_presunto_rientro")) 'aggiunto 21.06.2022 salvo


            .id_primo_conducente = getValueOrNohing(Rs("id_primo_conducente"))
            .id_secondo_conducente = getValueOrNohing(Rs("id_secondo_conducente"))
            .id_veicolo = getValueOrNohing(Rs("id_veicolo"))
            .targa = getValueOrNohing(Rs("targa"))
            .km_uscita = getValueOrNohing(Rs("km_uscita"))
            .km_rientro = getValueOrNohing(Rs("km_rientro"))
            .litri_uscita = getValueOrNohing(Rs("litri_uscita"))
            .litri_rientro = getValueOrNohing(Rs("litri_rientro"))
            .id_gruppo_danni_uscita = getValueOrNohing(Rs("id_gruppo_danni_uscita"))   'verificare se impostare a valore=0 se nullo/nothing 17.01.2021
            .id_gruppo_danni_rientro = getValueOrNohing(Rs("id_gruppo_danni_rientro"))
            .id_cliente = getValueOrNohing(Rs("id_cliente"))

            .num_prenotazione = getValueOrNohing(Rs("num_prenotazione"))

            .importo_a_carico_del_broker = getValueOrNohing(Rs("importo_a_carico_del_broker"))
            .tipo_tariffa = getValueOrNohing(Rs("tipo_tariffa"))
            .CODTAR = getValueOrNohing(Rs("CODTAR"))
            .id_tariffe_righe = getValueOrNohing(Rs("id_tariffe_righe"))

            .totale_da_incassare = getValueOrNohing(Rs("totale_da_incassare"))

        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaNumContratto(ByVal id_num_contratto As Integer, ByVal numero_crv As Integer) As DatiContratto
        Dim mio_record As DatiContratto = Nothing

        'modificata stringa sql aggiunto data_presunto_rientro 21.06.2022 salvo

        Dim sqlStr As String = "SELECT top 1 id,num_calcolo,status,num_prenotazione,num_contratto,num_crv,attivo,id_stazione_uscita,id_stazione_rientro,data_uscita,data_rientro,data_presunto_rientro, id_primo_conducente,id_secondo_conducente,id_veicolo,targa,km_uscita,km_rientro,litri_uscita,litri_rientro,id_gruppo_danni_uscita,id_gruppo_danni_rientro,id_cliente,importo_a_carico_del_broker,tipo_tariffa,CODTAR,id_tariffe_righe,totale_da_incassare" &
            " FROM contratti WITH(NOLOCK)" &
            " WHERE num_contratto = " & id_num_contratto &
            " AND num_crv = " & numero_crv &
            " ORDER BY num_calcolo DESC"
        'HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Using Rs = Cmd.ExecuteReader
                    If Rs.Read Then
                        mio_record = FillRecord(Rs)
                        mio_record.m_tipo_documento_origine = tipo_documento.Contratto
                    End If
                End Using
            End Using



            With mio_record
                Dim crv_ok As Boolean = True
                Try
                    Dim test As Integer = .num_crv
                Catch ex As Exception
                    crv_ok = False
                End Try

                If (crv_ok) AndAlso ((.num_crv > 0) OrElse (.num_crv = 0 And .attivo = False)) Then
                    ' sono nel caso di CRV, recupero dalla tabella contratti_crv_veicoli i reali orari, km, litri di uscita e di rientro del veicolo...
                    sqlStr = "SELECT * FROM contratti_crv_veicoli WITH(NOLOCK)" &
                        " WHERE num_contratto = " & id_num_contratto &
                        " AND num_crv = " & numero_crv &
                        " AND id_veicolo = " & .id_veicolo
                    Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                        Using Rs = Cmd.ExecuteReader
                            If Rs.Read Then
                                .km_uscita = getValueOrNohing(Rs("km_uscita"))
                                .km_rientro = getValueOrNohing(Rs("km_rientro"))
                                .litri_uscita = getValueOrNohing(Rs("serbatoio_uscita"))
                                .litri_rientro = getValueOrNohing(Rs("serbatoio_rientro"))

                                .data_uscita = getValueOrNohing(Rs("data_uscita"))
                                ' se sono nel CRV di chiusura del contratto, la data di rientro si trova sulla riga del contratto
                                If Not (.num_crv > 0 And .attivo) Then
                                    .data_rientro = getValueOrNohing(Rs("data_sostituzione"))
                                End If
                            End If
                        End Using
                    End Using
                End If
            End With

        End Using

        Return mio_record
    End Function

    Private Shared Function FillTrasferimenti(ByVal Rs As System.Data.SqlClient.SqlDataReader) As DatiContratto
        Dim mio_record As DatiContratto = New DatiContratto
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .m_num_calcolo = Nothing
            .m_status = getValueOrNohing(Rs("status"))
            .num_contratto = getValueOrNohing(Rs("num_trasferimento"))
            .num_crv = 0
            .attivo = 1
            .id_stazione_uscita = getValueOrNohing(Rs("id_stazione_uscita"))
            .id_stazione_rientro = getValueOrNohing(Rs("richiesta_per_id_stazione"))
            'If .id_stazione_rientro Is Nothing Then non può essere la stessa.... senno che trasferimento è?
            '    .id_stazione_rientro = .id_stazione_uscita
            'End If
            .data_uscita = getValueOrNohing(Rs("data_uscita"))
            .data_rientro = getValueOrNohing(Rs("data_rientro"))
            .id_primo_conducente = getValueOrNohing(Rs("id_conducente"))
            .id_secondo_conducente = Nothing
            .id_veicolo = getValueOrNohing(Rs("id_veicolo"))
            .targa = getValueOrNohing(Rs("targa"))
            .km_uscita = getValueOrNohing(Rs("km_uscita"))
            .km_rientro = getValueOrNohing(Rs("km_rientro"))
            .litri_uscita = getValueOrNohing(Rs("litri_uscita"))
            .litri_rientro = getValueOrNohing(Rs("litri_rientro"))
            .id_gruppo_danni_uscita = getValueOrNohing(Rs("id_gruppo_danni_uscita"))
            .id_gruppo_danni_rientro = getValueOrNohing(Rs("id_gruppo_danni_rientro"))
            .id_cliente = Nothing

            .num_prenotazione = Nothing

            .importo_a_carico_del_broker = Nothing
            .tipo_tariffa = Nothing
            .CODTAR = Nothing
            .id_tariffe_righe = Nothing

            .totale_da_incassare = Nothing

        End With
        Return mio_record
    End Function

    Private Shared Function FillLavaggi(ByVal Rs As System.Data.SqlClient.SqlDataReader) As DatiContratto
        Dim mio_record As DatiContratto = New DatiContratto
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .m_num_calcolo = Nothing
            .m_status = getValueOrNohing(Rs("stato"))
            .num_contratto = getValueOrNohing(Rs("num_lavaggio"))
            .num_crv = 0
            .attivo = 1
            .id_stazione_uscita = getValueOrNohing(Rs("id_stazione_uscita"))
            .id_stazione_rientro = getValueOrNohing(Rs("id_stazione_rientro"))
            If .id_stazione_rientro Is Nothing Then
                .id_stazione_rientro = .id_stazione_uscita
            End If
            .data_uscita = getValueOrNohing(Rs("data_uscita"))
            .data_rientro = getValueOrNohing(Rs("data_rientro"))
            .id_primo_conducente = getValueOrNohing(Rs("id_conducente"))
            .id_secondo_conducente = Nothing
            .id_veicolo = getValueOrNohing(Rs("id_veicolo"))
            .targa = getValueOrNohing(Rs("targa"))
            .km_uscita = getValueOrNohing(Rs("km_uscita"))
            .km_rientro = getValueOrNohing(Rs("km_rientro"))
            .litri_uscita = getValueOrNohing(Rs("litri_uscita"))
            .litri_rientro = getValueOrNohing(Rs("litri_rientro"))
            .id_gruppo_danni_uscita = getValueOrNohing(Rs("id_gruppo_danni_uscita"))
            .id_gruppo_danni_rientro = getValueOrNohing(Rs("id_gruppo_danni_rientro"))
            .id_cliente = Nothing

            .num_prenotazione = Nothing

            .importo_a_carico_del_broker = Nothing
            .tipo_tariffa = Nothing
            .CODTAR = Nothing
            .id_tariffe_righe = Nothing

            .totale_da_incassare = Nothing

        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaNumTrasferimento(ByVal id_trasferimento As Integer) As DatiContratto
        Dim mio_record As DatiContratto = Nothing

        Dim sqlStr As String = "SELECT * FROM trasferimenti WITH(NOLOCK)" &
            " WHERE id = @id_trasferimento"
        HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_trasferimento", System.Data.SqlDbType.Int, id_trasferimento)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    If Rs.Read Then
                        mio_record = FillTrasferimenti(Rs)
                        mio_record.m_tipo_documento_origine = tipo_documento.MovimentoInterno
                    End If
                End Using
            End Using
        End Using

        Return mio_record
    End Function

    Public Shared Function getRecordDaLavaggio(ByVal id_lavaggio As Integer) As DatiContratto
        Dim mio_record As DatiContratto = Nothing

        Dim sqlStr As String = "SELECT * FROM lavaggi WITH(NOLOCK)" &
            " WHERE id = @id_lavaggio"
        HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_lavaggio", System.Data.SqlDbType.Int, id_lavaggio)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    If Rs.Read Then
                        mio_record = FillLavaggi(Rs)
                        mio_record.m_tipo_documento_origine = tipo_documento.Lavaggio
                    End If
                End Using
            End Using
        End Using

        Return mio_record
    End Function

    Private Shared Function FillODL(ByVal mio_odl As odl, Optional ByVal durante_odl As Boolean = False) As DatiContratto
        Dim mio_record As DatiContratto = New DatiContratto
        With mio_record
            .id = mio_odl.id
            .m_num_calcolo = Nothing
            .m_status = Nothing
            .num_contratto = mio_odl.num_odl
            .num_crv = 0
            .attivo = 1
            .id_stazione_uscita = mio_odl.id_stazione_uscita
            .id_stazione_rientro = mio_odl.id_stazione_rientro
            .data_uscita = mio_odl.data_uscita
            .data_rientro = mio_odl.data_rientro
            .id_primo_conducente = mio_odl.id_conducente
            .id_secondo_conducente = Nothing
            .id_veicolo = mio_odl.id_veicolo
            .targa = tabella_veicoli.getTarga(mio_odl.id_veicolo)
            .km_uscita = mio_odl.km_uscita
            .km_rientro = mio_odl.km_rientro
            .litri_uscita = mio_odl.litri_uscita
            .litri_rientro = mio_odl.litri_rientro
            If durante_odl Then
                .id_gruppo_danni_uscita = mio_odl.id_gruppo_danni_uscita
                .id_gruppo_danni_rientro = mio_odl.id_gruppo_danni_durante_odl
            Else
                If mio_odl.noleggio_in_corso Then
                    .id_gruppo_danni_uscita = mio_odl.id_gruppo_danni_uscita
                    .id_gruppo_danni_rientro = mio_odl.id_gruppo_danni_rientro
                Else
                    If mio_odl.id_gruppo_danni_durante_odl Is Nothing Then ' nessun danno aperto...
                        .id_gruppo_danni_uscita = mio_odl.id_gruppo_danni_uscita
                    Else
                        .id_gruppo_danni_uscita = mio_odl.id_gruppo_danni_durante_odl
                    End If
                    .id_gruppo_danni_rientro = mio_odl.id_gruppo_danni_rientro
                End If
            End If

            .id_cliente = Nothing

            .num_prenotazione = Nothing

            .importo_a_carico_del_broker = Nothing
            .tipo_tariffa = Nothing
            .CODTAR = Nothing
            .id_tariffe_righe = Nothing

            .totale_da_incassare = Nothing

        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaNumODL(ByVal num_odl As Integer) As DatiContratto
        Dim mio_record As DatiContratto = Nothing

        Dim mio_odl As odl = odl.getRecordDaNumODL(num_odl)

        If mio_odl IsNot Nothing Then
            mio_record = FillODL(mio_odl)
            mio_record.m_tipo_documento_origine = tipo_documento.ODL
        End If

        Return mio_record
    End Function

    Public Shared Function getRecordDaNumDuranteODL(ByVal num_odl As Integer) As DatiContratto
        Dim mio_record As DatiContratto = Nothing

        Dim mio_odl As odl = odl.getRecordDaNumODL(num_odl)

        If mio_odl IsNot Nothing Then
            mio_record = FillODL(mio_odl, True)
            mio_record.m_tipo_documento_origine = tipo_documento.DuranteODL
        End If

        Return mio_record
    End Function

    Private Shared Function FillRDSGenerico(ByVal mio_evento As veicoli_evento_apertura_danno) As DatiContratto
        Dim mio_record As DatiContratto = New DatiContratto
        With mio_record
            .id = mio_evento.id
            .m_num_calcolo = Nothing
            .m_status = Nothing
            .num_contratto = mio_evento.id_rds
            .num_crv = 0
            .attivo = mio_evento.attivo
            .id_stazione_uscita = mio_evento.id_stazione_apertura
            .id_stazione_rientro = mio_evento.id_stazione_apertura
            .data_uscita = mio_evento.data
            .data_rientro = mio_evento.data
            .id_primo_conducente = Nothing
            .id_secondo_conducente = Nothing
            .id_veicolo = mio_evento.id_veicolo
            Dim mio_veicolo As tabella_veicoli = tabella_veicoli.get_record_da_id(mio_evento.id_veicolo)
            .targa = mio_veicolo.targa
            .km_uscita = mio_veicolo.km_attuali
            .km_rientro = mio_veicolo.km_attuali
            .litri_uscita = mio_veicolo.serbatoio_attuale
            .litri_rientro = mio_veicolo.serbatoio_attuale
            .id_gruppo_danni_uscita = mio_evento.id_gruppo_danni_apertura
            .id_gruppo_danni_rientro = mio_evento.id_gruppo_danni_chiusura

            .id_cliente = Nothing

            .num_prenotazione = Nothing

            .importo_a_carico_del_broker = Nothing
            .tipo_tariffa = Nothing
            .CODTAR = Nothing
            .id_tariffe_righe = Nothing

            .totale_da_incassare = Nothing

        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaRDSGenerico(ByVal id_evento As Integer) As DatiContratto
        Dim mio_record As DatiContratto = Nothing

        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(id_evento)

        If mio_evento IsNot Nothing Then
            mio_record = FillRDSGenerico(mio_evento)
            mio_record.m_tipo_documento_origine = tipo_documento.RDSGenerico
        End If

        Return mio_record
    End Function

    Public Shared Function getRecordDaRDSGenerico(ByVal id_tipo_documento As Integer, ByVal num_rds As Integer) As DatiContratto
        Dim mio_record As DatiContratto = Nothing

        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaRDS(id_tipo_documento, num_rds)

        If mio_evento IsNot Nothing Then
            mio_record = FillRDSGenerico(mio_evento)
            mio_record.m_tipo_documento_origine = tipo_documento.RDSGenerico
        End If

        Return mio_record
    End Function


    Public Function AggiornaContrattiPerCheckIn() As Boolean
        If tipo_documento_origine <> tipo_documento.Contratto Then
            Err.Raise(1001, Nothing, "Metodo AggiornaContrattiPerCheckIn non corretto per l'origine selezionata: " & tipo_documento_origine.ToString)
        End If
        AggiornaContrattiPerCheckIn = False

        Dim sqlStr As String = "UPDATE contratti SET" &
            " km_rientro = @km_rientro," &
            " litri_rientro = @litri_rientro," &
            " id_gruppo_danni_rientro = @id_gruppo_danni_rientro" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@km_rientro", System.Data.SqlDbType.Int, km_rientro)
                addParametro(Cmd, "@litri_rientro", System.Data.SqlDbType.Int, litri_rientro)
                addParametro(Cmd, "@id_gruppo_danni_rientro", System.Data.SqlDbType.Int, id_gruppo_danni_rientro)

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaContrattiPerCheckIn = True
            End Using
        End Using

    End Function

    Public Function AggiornaCRVPerCheckIn() As Boolean
        If tipo_documento_origine <> tipo_documento.Contratto Then
            Err.Raise(1001, Nothing, "Metodo AggiornaContrattiPerCheckIn non corretto per l'origine selezionata: " & tipo_documento_origine.ToString)
        End If
        AggiornaCRVPerCheckIn = False

        Dim sqlStr As String = "UPDATE contratti_crv_veicoli SET" &
            " check_in_effettuato = 1," &
            " km_rientro = @km_rientro," &
            " serbatoio_rientro = @serbatoio_rientro" &
            " WHERE num_contratto = @num_contratto" &
            " AND num_crv = @num_crv" &
            " AND id_veicolo = @id_veicolo"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@num_contratto", System.Data.SqlDbType.Int, num_contratto)
                addParametro(Cmd, "@num_crv", System.Data.SqlDbType.Int, num_crv)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                addParametro(Cmd, "@km_rientro", System.Data.SqlDbType.Int, km_rientro)
                addParametro(Cmd, "@serbatoio_rientro", System.Data.SqlDbType.Int, litri_rientro)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaCRVPerCheckIn = True
            End Using
        End Using

    End Function

    Public Function AggiornaMovimentiInterniPerCheckIn() As Boolean
        If tipo_documento_origine <> tipo_documento.MovimentoInterno Then
            Err.Raise(1001, Nothing, "Metodo AggiornaMovimentiInterniPerCheckIn non corretto per l'origine selezionata: " & tipo_documento_origine.ToString)
        End If
        AggiornaMovimentiInterniPerCheckIn = False

        Dim sqlStr As String = "UPDATE trasferimenti SET" &
            " data_rientro = @data_rientro," &
            " km_rientro = @km_rientro," &
            " litri_rientro = @litri_rientro," &
            " id_gruppo_danni_rientro = @id_gruppo_danni_rientro" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@data_rientro", System.Data.SqlDbType.DateTime, data_rientro)
                addParametro(Cmd, "@km_rientro", System.Data.SqlDbType.Int, km_rientro)
                addParametro(Cmd, "@litri_rientro", System.Data.SqlDbType.Int, litri_rientro)
                addParametro(Cmd, "@id_gruppo_danni_rientro", System.Data.SqlDbType.Int, id_gruppo_danni_rientro)

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaMovimentiInterniPerCheckIn = True
            End Using
        End Using

    End Function

    Public Function AggiornaLavaggioPerCheckIn() As Boolean
        If tipo_documento_origine <> tipo_documento.Lavaggio Then
            Err.Raise(1001, Nothing, "Metodo AggiornaLavaggioPerCheckIn non corretto per l'origine selezionata: " & tipo_documento_origine.ToString)
        End If
        AggiornaLavaggioPerCheckIn = False

        Dim sqlStr As String = "UPDATE lavaggi SET" &
            " data_rientro = @data_rientro," &
            " km_rientro = @km_rientro," &
            " litri_rientro = @litri_rientro," &
            " id_gruppo_danni_rientro = @id_gruppo_danni_rientro, " &
            " id_stazione_rientro = @id_stazione_rientro " &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@data_rientro", System.Data.SqlDbType.DateTime, data_rientro)
                addParametro(Cmd, "@km_rientro", System.Data.SqlDbType.Int, km_rientro)
                addParametro(Cmd, "@litri_rientro", System.Data.SqlDbType.Int, litri_rientro)
                addParametro(Cmd, "@id_gruppo_danni_rientro", System.Data.SqlDbType.Int, id_gruppo_danni_rientro)
                addParametro(Cmd, "@id_stazione_rientro", System.Data.SqlDbType.Int, id_stazione_rientro)

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaLavaggioPerCheckIn = True
            End Using
        End Using

    End Function



    Public Function AggiornaODLPerCheckIn() As Boolean
        If tipo_documento_origine <> tipo_documento.ODL Then
            Err.Raise(1001, Nothing, "Metodo AggiornaODLPerCheckIn non corretto per l'origine selezionata: " & tipo_documento_origine.ToString)
        End If
        AggiornaODLPerCheckIn = False

        Dim mio_odl As odl = odl.getRecordDaNumODL(num_contratto)

        If mio_odl IsNot Nothing Then
            With mio_odl
                .km_rientro = km_rientro
                .litri_rientro = litri_rientro
                .id_gruppo_danni_rientro = id_gruppo_danni_rientro
                .id_stato_odl = enum_odl_stato.Chiuso

                .SalvaRecord()
                AggiornaODLPerCheckIn = True
            End With
        End If
    End Function

    Public Function AggiornaRDSGenericoPerCheckIn() As Boolean
        If tipo_documento_origine <> tipo_documento.RDSGenerico Then
            Err.Raise(1001, Nothing, "Metodo AggiornaRDSGenericoPerCheckIn non corretto per l'origine selezionata: " & tipo_documento_origine.ToString)
        End If
        AggiornaRDSGenericoPerCheckIn = False

        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(id)

        If mio_evento IsNot Nothing Then
            With mio_evento
                .id_documento_apertura = .id_rds
                .id_gruppo_danni_chiusura = id_gruppo_danni_rientro

                .AggiornaRecord()
                AggiornaRDSGenericoPerCheckIn = True
            End With
        End If
    End Function

    Public Function AggiornaMovimentiAutoPerCheckIn(Optional ByVal tipo_movimento As Integer = -1) As Boolean
        AggiornaMovimentiAutoPerCheckIn = False

        If tipo_movimento = -1 Then
            tipo_movimento = Costanti.idMovimentoNoleggio
        End If

        If attivo Or tipo_documento_origine <> tipo_documento.Contratto Then ' siamo nel caso senza CRV oppure sul veicolo principale!!! oppure siamo su altri tipo_origine non Contratto!!!
            Dim sqlStr As String = "UPDATE movimenti_targa SET" &
                " data_rientro = @data_rientro," &
                " id_stazione_rientro = @id_stazione_rientro," &
                " km_rientro = @km_rientro," &
                " serbatoio_rientro = @litri_rientro," &
                " id_operatore_rientro = @id_utente," &
                " data_registrazione_rientro = GetDate()," &
                " movimento_attivo = 0" &
                " WHERE num_riferimento = @num_contratto" &
                " AND id_veicolo = @id_veicolo" &
                " AND id_tipo_movimento = @tipo_movimento" &
                " AND movimento_attivo = 1"

            Dim id_utente As Integer = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@data_rientro", System.Data.SqlDbType.DateTime, data_rientro)
                    addParametro(Cmd, "@id_stazione_rientro", System.Data.SqlDbType.Int, id_stazione_rientro)
                    addParametro(Cmd, "@km_rientro", System.Data.SqlDbType.Int, km_rientro)
                    addParametro(Cmd, "@litri_rientro", System.Data.SqlDbType.Int, litri_rientro)
                    addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)

                    addParametro(Cmd, "@num_contratto", System.Data.SqlDbType.Int, num_contratto)
                    addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                    addParametro(Cmd, "@tipo_movimento", System.Data.SqlDbType.Int, tipo_movimento)

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    AggiornaMovimentiAutoPerCheckIn = True
                End Using
            End Using
        Else ' siamo nel caso di CRV sui veicoli secondari!!!
            Dim sqlStr As String = "UPDATE movimenti_targa SET" &
                " km_rientro = @km_rientro," &
                " serbatoio_rientro = @litri_rientro" &
                " WHERE num_riferimento = @num_contratto" &
                " AND id_veicolo = @id_veicolo" &
                " AND id_tipo_movimento = @tipo_movimento" &
                " AND num_crv_contratto = @num_crv_contratto"

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@km_rientro", System.Data.SqlDbType.Int, km_rientro)
                    addParametro(Cmd, "@litri_rientro", System.Data.SqlDbType.Int, litri_rientro)

                    addParametro(Cmd, "@num_contratto", System.Data.SqlDbType.Int, num_contratto)
                    addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                    addParametro(Cmd, "@tipo_movimento", System.Data.SqlDbType.Int, Costanti.idMovimentoNoleggio)
                    addParametro(Cmd, "@num_crv_contratto", System.Data.SqlDbType.Int, num_crv)

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    AggiornaMovimentiAutoPerCheckIn = True
                End Using
            End Using
        End If



    End Function

    Public Function AggiornaVeicoliPerCheckIn(Optional ByVal id_tipo_documento As Integer = 0) As Boolean
        AggiornaVeicoliPerCheckIn = False

        Dim sqlStr As String = "UPDATE veicoli SET" &
            " id_stazione = @id_stazione_rientro," &
            " km_attuali = @km_rientro," &
            " serbatoio_attuale = @litri_rientro," &
            " disponibile_nolo = @disponibile_nolo," &
            " noleggiata = @noleggiata," &
            " da_lavare = @da_lavare," &
            " da_rifornire = @da_rifornire," &
            " da_riparare = @da_riparare," &
            " in_vendita = @in_vendita," &
            " odl_aperto = NULL" &
            " WHERE id = @id_veicolo"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)

                'MODIFICA RICHIESTA DA FRANCESCO - ANCHE PER I CRV L'AUTO VIENE ASSOCIATA SEMPRE ALLA STAZIONE DELL'UTENTE LOGGATO E NON A QUELLA DI RIENTRO DEL CONTRATTO
                'PER CUI IN OGNI CASO L'AUTO VIENE ASSOCIATA ALLA STAZIONE DELL'UTENTE CHE STA EFFETTUANDO IL RIENTRO (DA FARE SOLO IN CASO DI CONTRATTO ALTRIMENTI)


                If id_tipo_documento = tipo_documento.Contratto Then
                    addParametro(Cmd, "@id_stazione_rientro", System.Data.SqlDbType.Int, HttpContext.Current.Request.Cookies("SicilyRentCar")("stazione"))
                Else
                    addParametro(Cmd, "@id_stazione_rientro", System.Data.SqlDbType.Int, id_stazione_rientro)
                End If



                addParametro(Cmd, "@km_rientro", System.Data.SqlDbType.Int, km_rientro)
                addParametro(Cmd, "@litri_rientro", System.Data.SqlDbType.Int, litri_rientro)

                addParametro(Cmd, "@disponibile_nolo", System.Data.SqlDbType.Bit, disponibile_nolo)
                addParametro(Cmd, "@noleggiata", System.Data.SqlDbType.Bit, noleggiata)
                addParametro(Cmd, "@da_lavare", System.Data.SqlDbType.Bit, da_lavare)
                addParametro(Cmd, "@da_rifornire", System.Data.SqlDbType.Bit, da_rifornire)
                addParametro(Cmd, "@da_riparare", System.Data.SqlDbType.Bit, da_riparare)
                addParametro(Cmd, "@in_vendita", System.Data.SqlDbType.Bit, in_vendita_buy_back)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaVeicoliPerCheckIn = True
            End Using
        End Using

    End Function

    Public Function AggiornaVeicoliNoloInCorso(Optional ByVal id_tipo_documento As Integer = 0) As Boolean
        AggiornaVeicoliNoloInCorso = False

        Dim sqlStr As String = "UPDATE veicoli SET" &
            " disponibile_nolo = @disponibile_nolo," &
            " noleggiata = @noleggiata," &
            " da_riparare = @da_riparare " &
            " WHERE id = @id_veicolo"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)

                addParametro(Cmd, "@disponibile_nolo", System.Data.SqlDbType.Bit, disponibile_nolo)
                addParametro(Cmd, "@noleggiata", System.Data.SqlDbType.Bit, noleggiata)
                addParametro(Cmd, "@da_riparare", System.Data.SqlDbType.Bit, da_riparare)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaVeicoliNoloInCorso = True
            End Using
        End Using

    End Function

    Public Function SalvaGruppoDanniCheckOut(ByVal id_gruppo_danni As Integer) As Boolean
        If tipo_documento_origine <> tipo_documento.Contratto Then
            Err.Raise(1001, Nothing, "Metodo SalvaGruppoDanniCheckOut non corretto per l'origine selezionata: " & tipo_documento_origine.ToString)
        End If

        SalvaGruppoDanniCheckOut = False
        Dim sqlStr As String = Nothing

        sqlStr = "UPDATE contratti SET" &
            " id_gruppo_danni_uscita = @id_gruppo_danni" &
            " WHERE id = @id"

        If sqlStr IsNot Nothing Then
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@id_gruppo_danni", System.Data.SqlDbType.Int, id_gruppo_danni)
                    addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    SalvaGruppoDanniCheckOut = True
                End Using
            End Using
        End If
    End Function

    Public Function SalvaGruppoDanniMovimentoInternoCheckOut(ByVal id_gruppo_danni As Integer) As Boolean
        If tipo_documento_origine <> tipo_documento.MovimentoInterno Then
            Err.Raise(1001, Nothing, "Metodo SalvaGruppoDanniMovimentoInternoCheckOut non corretto per l'origine selezionata: " & tipo_documento_origine.ToString)
        End If

        SalvaGruppoDanniMovimentoInternoCheckOut = False
        Dim sqlStr As String = Nothing

        sqlStr = "UPDATE trasferimenti SET" &
            " id_gruppo_danni_uscita = @id_gruppo_danni" &
            " WHERE id = @id"

        If sqlStr IsNot Nothing Then
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@id_gruppo_danni", System.Data.SqlDbType.Int, id_gruppo_danni)
                    addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    SalvaGruppoDanniMovimentoInternoCheckOut = True
                End Using
            End Using
        End If
    End Function

    Public Function SalvaGruppoDanniLavaggio(ByVal id_gruppo_danni As Integer) As Boolean
        If tipo_documento_origine <> tipo_documento.Lavaggio Then
            Err.Raise(1001, Nothing, "Metodo SalvaGruppoDanniMovimentoInternoCheckOut non corretto per l'origine selezionata: " & tipo_documento_origine.ToString)
        End If

        SalvaGruppoDanniLavaggio = False
        Dim sqlStr As String = Nothing

        sqlStr = "UPDATE lavaggi SET" &
            " id_gruppo_danni_uscita = @id_gruppo_danni" &
            " WHERE id = @id"

        If sqlStr IsNot Nothing Then
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@id_gruppo_danni", System.Data.SqlDbType.Int, id_gruppo_danni)
                    addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    SalvaGruppoDanniLavaggio = True
                End Using
            End Using
        End If
    End Function

    Public Function SalvaGruppoDanniODLCheckOut(ByVal id_gruppo_danni As Integer) As Boolean
        If tipo_documento_origine <> tipo_documento.ODL Then
            Err.Raise(1001, Nothing, "Metodo SalvaGruppoDanniODLCheckOut non corretto per l'origine selezionata: " & tipo_documento_origine.ToString)
        End If

        SalvaGruppoDanniODLCheckOut = False
        Dim sqlStr As String = Nothing

        sqlStr = "UPDATE odl SET" &
            " id_gruppo_danni_uscita = @id_gruppo_danni" &
            " WHERE id = @id"

        If sqlStr IsNot Nothing Then
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@id_gruppo_danni", System.Data.SqlDbType.Int, id_gruppo_danni)
                    addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    SalvaGruppoDanniODLCheckOut = True
                End Using
            End Using
        End If
    End Function

    Public Function AggiornaContrattiPerFurto() As Boolean
        If tipo_documento_origine <> tipo_documento.Contratto Then
            Err.Raise(1001, Nothing, "Metodo AggiornaContrattiPerFurto non corretto per l'origine selezionata: " & tipo_documento_origine.ToString)
        End If

        AggiornaContrattiPerFurto = False

        Dim sqlStr As String = "UPDATE contratti SET" &
            " id_gruppo_danni_rientro = @id_gruppo_danni_rientro" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_gruppo_danni_rientro", System.Data.SqlDbType.Int, id_gruppo_danni_rientro)
                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaContrattiPerFurto = True
            End Using
        End Using
    End Function

    Public Function SalvaGruppoDanniCheckIn(ByVal id_gruppo_danni As Integer) As Boolean
        If tipo_documento_origine <> tipo_documento.Contratto Then
            Err.Raise(1001, Nothing, "Metodo SalvaGruppoDanniCheckIn non corretto per l'origine selezionata: " & tipo_documento_origine.ToString)
        End If

        SalvaGruppoDanniCheckIn = False
        Dim sqlStr As String = Nothing

        sqlStr = "UPDATE contratti SET" &
            " id_gruppo_danni_rientro = @id_gruppo_danni" &
            " WHERE id = @id"

        If sqlStr IsNot Nothing Then
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@id_gruppo_danni", System.Data.SqlDbType.Int, id_gruppo_danni)
                    addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    SalvaGruppoDanniCheckIn = True
                End Using
            End Using
        End If
    End Function

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Err.Raise(1001, Me, "Metodo non previsto per la classe")
        Return 0
    End Function
End Class


Public Class veicoli_tipo_attesa
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_descrizione As String

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property descrizione() As String
        Get
            Return m_descrizione
        End Get
        Set(ByVal value As String)
            m_descrizione = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO veicoli_tipo_attesa (descrizione)" &
            " VALUES (@descrizione)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM veicoli_tipo_attesa"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Public Function UpdateRecord() As veicoli_tipo_attesa
        Dim sqlStr As String = "UPDATE veicoli_tipo_attesa SET" &
            " descrizione = @descrizione" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using

        Return Me
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_tipo_attesa
        Dim mia_record As veicoli_tipo_attesa = New veicoli_tipo_attesa
        With mia_record
            .id = getValueOrNohing(Rs("id"))
            .descrizione = getValueOrNohing(Rs("descrizione"))
        End With
        Return mia_record
    End Function

    Public Shared Function get_record_da_id(ByVal id_record As Integer) As veicoli_tipo_attesa
        Dim mio_record As veicoli_tipo_attesa = Nothing

        Dim sqlStr As String = "SELECT * FROM veicoli_tipo_attesa WITH(NOLOCK) WHERE id = " & id_record

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

    Public Function CancellaRecord() As Boolean
        Return CancellaRecord(id)
    End Function

    Public Shared Function CancellaRecord(ByVal id_record As Integer) As Boolean
        CancellaRecord = False
        Dim sqlStr As String = "DELETE FROM veicoli_tipo_attesa WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    CancellaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("CancellaRecord: " & ex.Message)
        End Try
    End Function
End Class


Public Class veicoli_motivo_non_addebito_rds
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_descrizione As String

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property descrizione() As String
        Get
            Return m_descrizione
        End Get
        Set(ByVal value As String)
            m_descrizione = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO veicoli_motivo_non_addebito_rds (descrizione)" &
            " VALUES (@descrizione)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM veicoli_motivo_non_addebito_rds"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Public Function UpdateRecord() As veicoli_motivo_non_addebito_rds
        Dim sqlStr As String = "UPDATE veicoli_motivo_non_addebito_rds SET" &
            " descrizione = @descrizione" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using

        Return Me
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As veicoli_motivo_non_addebito_rds
        Dim mia_record As veicoli_motivo_non_addebito_rds = New veicoli_motivo_non_addebito_rds
        With mia_record
            .id = getValueOrNohing(Rs("id"))
            .descrizione = getValueOrNohing(Rs("descrizione"))
        End With
        Return mia_record
    End Function

    Public Shared Function get_record_da_id(ByVal id_record As Integer) As veicoli_motivo_non_addebito_rds
        Dim mio_record As veicoli_motivo_non_addebito_rds = Nothing

        Dim sqlStr As String = "SELECT * FROM veicoli_motivo_non_addebito_rds WITH(NOLOCK) WHERE id = " & id_record

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

    Public Function CancellaRecord() As Boolean
        Return CancellaRecord(id)
    End Function

    Public Shared Function CancellaRecord(ByVal id_record As Integer) As Boolean
        CancellaRecord = False
        Dim sqlStr As String = "DELETE FROM veicoli_motivo_non_addebito_rds WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    CancellaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("CancellaRecord: " & ex.Message)
        End Try
    End Function
End Class


Public Class tabella_veicoli
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_targa As String
    Protected m_telaio As String
    Protected m_km_attuali As Integer?
    Protected m_id_modello As Integer?
    Protected m_id_stazione As Integer?
    Protected m_id_proprietario As Integer?
    Protected m_modello As String
    Protected m_stazione As String
    Protected m_proprietario As String
    Protected m_capacita_serbatoio As Integer?
    Protected m_serbatoio_attuale As Integer?
    Protected m_data_buy_back As Date?

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public ReadOnly Property targa() As String
        Get
            Return m_targa
        End Get
    End Property
    Public ReadOnly Property telaio() As String
        Get
            Return m_telaio
        End Get
    End Property
    Public ReadOnly Property km_attuali() As Integer?
        Get
            Return m_km_attuali
        End Get
    End Property
    Public ReadOnly Property id_modello() As Integer?
        Get
            Return m_id_modello
        End Get
    End Property
    Public ReadOnly Property id_stazione() As Integer?
        Get
            Return m_id_stazione
        End Get
    End Property
    Public ReadOnly Property id_proprietario() As Integer?
        Get
            Return m_id_proprietario
        End Get
    End Property
    Public ReadOnly Property modello() As String
        Get
            Return m_modello
        End Get
    End Property
    Public ReadOnly Property stazione() As String
        Get
            Return m_stazione
        End Get
    End Property
    Public ReadOnly Property proprietario() As String
        Get
            Return m_proprietario
        End Get
    End Property
    Public ReadOnly Property capacita_serbatoio() As Integer?
        Get
            Return m_capacita_serbatoio
        End Get
    End Property
    Public ReadOnly Property serbatoio_attuale() As Integer?
        Get
            Return m_serbatoio_attuale
        End Get
    End Property
    Public ReadOnly Property data_buy_back As Date?
        Get
            Return m_data_buy_back
        End Get
    End Property

    Private Shared Function getModelloAutoDaId(ByVal id_modello As Integer) As String
        Dim sqlStr As String
        sqlStr = "SELECT m.descrizione AS modello" &
            " FROM modelli m WITH(NOLOCK)" &
            " WHERE m.id_modello = '" & id_modello & "'"
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getModelloAutoDaId = Cmd.ExecuteScalar & ""
            End Using
        End Using
    End Function

    Private Shared Function getSerbatoioAutoDaId(ByVal id_modello As Integer) As String
        Dim sqlStr As String
        sqlStr = "SELECT m.capacita_serbatoio" &
            " FROM modelli m WITH(NOLOCK)" &
            " WHERE m.id_modello = '" & id_modello & "'"
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getSerbatoioAutoDaId = Cmd.ExecuteScalar & ""
            End Using
        End Using
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As tabella_veicoli
        Dim mio_record As tabella_veicoli = New tabella_veicoli
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .m_targa = getValueOrNohing(Rs("targa"))
            .m_telaio = getValueOrNohing(Rs("telaio"))
            .m_km_attuali = getValueOrNohing(Rs("km_attuali"))
            .m_id_modello = getValueOrNohing(Rs("id_modello"))
            .m_id_stazione = getValueOrNohing(Rs("id_stazione"))
            .m_id_proprietario = getValueOrNohing(Rs("id_proprietario"))
            .m_modello = getModelloAutoDaId(.id_modello)
            .m_stazione = Libreria.getNomeStazioneDaId(.id_stazione)
            .m_proprietario = Libreria.getNomeProprietarioDaId(.id_proprietario)
            Dim serbatoio As String = getSerbatoioAutoDaId(.id_modello)
            If serbatoio = "" Then
                .m_capacita_serbatoio = 0
            Else
                .m_capacita_serbatoio = Integer.Parse(serbatoio)
            End If
            .m_serbatoio_attuale = getValueOrNohing(Rs("serbatoio_attuale"))
            .m_data_buy_back = getValueOrNohing(Rs("data_fine_leasing"))
        End With
        Return mio_record
    End Function

    Public Shared Function get_record_da_id(ByVal id_record As Integer) As tabella_veicoli
        Dim mio_record As tabella_veicoli = Nothing

        Dim sqlStr As String = "SELECT id,targa,telaio,km_attuali,serbatoio_attuale,id_modello,id_stazione,id_proprietario,data_fine_leasing FROM veicoli WITH(NOLOCK) WHERE id = " & id_record

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

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Err.Raise(1001, Me, "Metodo non previsto per la classe")
        Return 0
    End Function

    Public Shared Function getTarga(ByVal id_record As Integer) As String
        getTarga = ""
        Dim sqlStr As String = "SELECT targa FROM veicoli WITH(NOLOCK) WHERE id = " & id_record

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Return Cmd.ExecuteScalar & ""
            End Using
        End Using
    End Function
End Class



Public Class odl
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_num_odl As Integer
    Protected m_attivo As Boolean
    Protected m_data_odl As DateTime?
    Protected m_id_stato_odl As Integer?
    Protected m_id_tipo_doc_apertura As Integer?
    Protected m_id_doc_apertura As Integer?
    Protected m_num_crv_noleggio As Integer?
    Protected m_id_veicolo As Integer
    Protected m_noleggio_in_corso As Boolean?
    Protected m_id_evento_apertura_danno As Integer?
    Protected m_num_rds As Integer?
    Protected m_rds_attivo As Boolean?
    Protected m_lavoro_eseguito As Boolean?
    Protected m_data_lavoro_eseguito As DateTime?
    Protected m_id_tipo_riparazione As Integer?
    Protected m_id_fornitore As Integer?
    Protected m_id_lavoro_da_eseguire As Integer?
    Protected m_descrizione_lavoro As String
    Protected m_data_autorizzato As DateTime?
    Protected m_id_autorizzato_da As Integer?
    Protected m_id_conducente As Integer?
    Protected m_data_uscita As DateTime?
    Protected m_data_previsto_rientro As DateTime?
    Protected m_data_rientro As DateTime?
    Protected m_id_stazione_uscita As Integer?
    Protected m_id_stazione_previsto_rientro As Integer?
    Protected m_id_stazione_rientro As Integer?
    Protected m_km_uscita As Integer?
    Protected m_km_rientro As Integer?
    Protected m_litri_uscita As Integer?
    Protected m_litri_rientro As Integer?
    Protected m_id_consegnato_da As Integer?
    Protected m_id_ritirato_da As Integer?
    Protected m_data_richiesta_preventivo As DateTime?
    Protected m_preventivo As Double?
    Protected m_id_autorizzato_pagamento As Integer?
    Protected m_data_autorizzato_pagamento As DateTime?
    Protected m_importo As Double?
    Protected m_id_gruppo_danni_uscita As Integer?
    Protected m_id_gruppo_danni_durante_odl As Integer?
    Protected m_id_gruppo_danni_rientro As Integer?
    Protected m_id_movimento_targa As Integer?

    Protected m_tipo_fattura As Integer?
    Protected m_anno_fattura As Integer?
    Protected m_codice_fattura As Integer?
    Protected m_data_fattura As DateTime?
    Protected m_importo_fattura As Double?

    Protected m_data_creazione As DateTime?
    Protected m_id_utente As Integer?

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property num_odl() As Integer
        Get
            Return m_num_odl
        End Get
        Set(ByVal value As Integer)
            m_num_odl = value
        End Set
    End Property
    Public Property attivo() As Boolean
        Get
            Return m_attivo
        End Get
        Set(ByVal value As Boolean)
            m_attivo = value
        End Set
    End Property
    Public Property data_odl() As DateTime?
        Get
            Return m_data_odl
        End Get
        Set(ByVal value As DateTime?)
            m_data_odl = value
        End Set
    End Property
    Public Property id_stato_odl() As Integer?
        Get
            Return m_id_stato_odl
        End Get
        Set(ByVal value As Integer?)
            m_id_stato_odl = value
        End Set
    End Property
    Public Property id_tipo_doc_apertura() As Integer?
        Get
            Return m_id_tipo_doc_apertura
        End Get
        Set(ByVal value As Integer?)
            m_id_tipo_doc_apertura = value
        End Set
    End Property
    Public Property id_doc_apertura() As Integer?
        Get
            Return m_id_doc_apertura
        End Get
        Set(ByVal value As Integer?)
            m_id_doc_apertura = value
        End Set
    End Property
    Public Property num_crv_noleggio() As Integer?
        Get
            Return m_num_crv_noleggio
        End Get
        Set(ByVal value As Integer?)
            m_num_crv_noleggio = value
        End Set
    End Property
    Public Property id_veicolo() As Integer
        Get
            Return m_id_veicolo
        End Get
        Set(ByVal value As Integer)
            m_id_veicolo = value
        End Set
    End Property
    Public Property noleggio_in_corso() As Boolean?
        Get
            Return m_noleggio_in_corso
        End Get
        Set(ByVal value As Boolean?)
            m_noleggio_in_corso = value
        End Set
    End Property
    Public Property id_evento_apertura_danno() As Integer?
        Get
            Return m_id_evento_apertura_danno
        End Get
        Set(ByVal value As Integer?)
            m_id_evento_apertura_danno = value
        End Set
    End Property
    Public Property num_rds() As Integer?
        Get
            Return m_num_rds
        End Get
        Set(ByVal value As Integer?)
            m_num_rds = value
        End Set
    End Property
    Public Property rds_attivo() As Boolean?
        Get
            Return m_rds_attivo
        End Get
        Set(ByVal value As Boolean?)
            m_rds_attivo = value
        End Set
    End Property
    Public Property lavoro_eseguito() As Boolean?
        Get
            Return m_lavoro_eseguito
        End Get
        Set(ByVal value As Boolean?)
            m_lavoro_eseguito = value
        End Set
    End Property
    Public Property data_lavoro_eseguito() As DateTime?
        Get
            Return m_data_lavoro_eseguito
        End Get
        Set(ByVal value As DateTime?)
            m_data_lavoro_eseguito = value
        End Set
    End Property
    Public Property id_tipo_riparazione() As Integer?
        Get
            Return m_id_tipo_riparazione
        End Get
        Set(ByVal value As Integer?)
            m_id_tipo_riparazione = value
        End Set
    End Property
    Public Property id_fornitore() As Integer?
        Get
            Return m_id_fornitore
        End Get
        Set(ByVal value As Integer?)
            m_id_fornitore = value
        End Set
    End Property
    Public Property id_lavoro_da_eseguire() As Integer?
        Get
            Return m_id_lavoro_da_eseguire
        End Get
        Set(ByVal value As Integer?)
            m_id_lavoro_da_eseguire = value
        End Set
    End Property
    Public Property descrizione_lavoro() As String
        Get
            Return m_descrizione_lavoro
        End Get
        Set(ByVal value As String)
            m_descrizione_lavoro = value
        End Set
    End Property
    Public Property data_autorizzato() As DateTime?
        Get
            Return m_data_autorizzato
        End Get
        Set(ByVal value As DateTime?)
            m_data_autorizzato = value
        End Set
    End Property
    Public Property id_autorizzato_da() As Integer?
        Get
            Return m_id_autorizzato_da
        End Get
        Set(ByVal value As Integer?)
            m_id_autorizzato_da = value
        End Set
    End Property
    Public Property id_conducente() As Integer?
        Get
            Return m_id_conducente
        End Get
        Set(ByVal value As Integer?)
            m_id_conducente = value
        End Set
    End Property
    Public Property data_uscita() As DateTime?
        Get
            Return m_data_uscita
        End Get
        Set(ByVal value As DateTime?)
            m_data_uscita = value
        End Set
    End Property
    Public Property data_previsto_rientro() As DateTime?
        Get
            Return m_data_previsto_rientro
        End Get
        Set(ByVal value As DateTime?)
            m_data_previsto_rientro = value
        End Set
    End Property
    Public Property data_rientro() As DateTime?
        Get
            Return m_data_rientro
        End Get
        Set(ByVal value As DateTime?)
            m_data_rientro = value
        End Set
    End Property
    Public Property id_stazione_uscita() As Integer?
        Get
            Return m_id_stazione_uscita
        End Get
        Set(ByVal value As Integer?)
            m_id_stazione_uscita = value
        End Set
    End Property
    Public Property id_stazione_previsto_rientro() As Integer?
        Get
            Return m_id_stazione_previsto_rientro
        End Get
        Set(ByVal value As Integer?)
            m_id_stazione_previsto_rientro = value
        End Set
    End Property
    Public Property id_stazione_rientro() As Integer?
        Get
            Return m_id_stazione_rientro
        End Get
        Set(ByVal value As Integer?)
            m_id_stazione_rientro = value
        End Set
    End Property
    Public Property km_uscita() As Integer?
        Get
            Return m_km_uscita
        End Get
        Set(ByVal value As Integer?)
            m_km_uscita = value
        End Set
    End Property
    Public Property km_rientro() As Integer?
        Get
            Return m_km_rientro
        End Get
        Set(ByVal value As Integer?)
            m_km_rientro = value
        End Set
    End Property
    Public Property litri_uscita() As Integer?
        Get
            Return m_litri_uscita
        End Get
        Set(ByVal value As Integer?)
            m_litri_uscita = value
        End Set
    End Property
    Public Property litri_rientro() As Integer?
        Get
            Return m_litri_rientro
        End Get
        Set(ByVal value As Integer?)
            m_litri_rientro = value
        End Set
    End Property
    Public Property id_consegnato_da() As Integer?
        Get
            Return m_id_consegnato_da
        End Get
        Set(ByVal value As Integer?)
            m_id_consegnato_da = value
        End Set
    End Property
    Public Property id_ritirato_da() As Integer?
        Get
            Return m_id_ritirato_da
        End Get
        Set(ByVal value As Integer?)
            m_id_ritirato_da = value
        End Set
    End Property
    Public Property data_richiesta_preventivo() As DateTime?
        Get
            Return m_data_richiesta_preventivo
        End Get
        Set(ByVal value As DateTime?)
            m_data_richiesta_preventivo = value
        End Set
    End Property
    Public Property preventivo() As Double?
        Get
            Return m_preventivo
        End Get
        Set(ByVal value As Double?)
            m_preventivo = value
        End Set
    End Property
    Public Property id_autorizzato_pagamento() As Integer?
        Get
            Return m_id_autorizzato_pagamento
        End Get
        Set(ByVal value As Integer?)
            m_id_autorizzato_pagamento = value
        End Set
    End Property
    Public Property data_autorizzato_pagamento() As DateTime?
        Get
            Return m_data_autorizzato_pagamento
        End Get
        Set(ByVal value As DateTime?)
            m_data_autorizzato_pagamento = value
        End Set
    End Property
    Public Property importo() As Double?
        Get
            Return m_importo
        End Get
        Set(ByVal value As Double?)
            m_importo = value
        End Set
    End Property
    Public Property id_gruppo_danni_uscita() As Integer?
        Get
            Return m_id_gruppo_danni_uscita
        End Get
        Set(ByVal value As Integer?)
            m_id_gruppo_danni_uscita = value
        End Set
    End Property
    Public Property id_gruppo_danni_durante_odl() As Integer?
        Get
            Return m_id_gruppo_danni_durante_odl
        End Get
        Set(ByVal value As Integer?)
            m_id_gruppo_danni_durante_odl = value
        End Set
    End Property
    Public Property id_gruppo_danni_rientro() As Integer?
        Get
            Return m_id_gruppo_danni_rientro
        End Get
        Set(ByVal value As Integer?)
            m_id_gruppo_danni_rientro = value
        End Set
    End Property
    Public Property id_movimento_targa() As Integer?
        Get
            Return m_id_movimento_targa
        End Get
        Set(ByVal value As Integer?)
            m_id_movimento_targa = value
        End Set
    End Property

    Public Property tipo_fattura() As Integer?
        Get
            Return m_tipo_fattura
        End Get
        Set(ByVal value As Integer?)
            m_tipo_fattura = value
        End Set
    End Property
    Public Property anno_fattura() As Integer?
        Get
            Return m_anno_fattura
        End Get
        Set(ByVal value As Integer?)
            m_anno_fattura = value
        End Set
    End Property
    Public Property codice_fattura() As Integer?
        Get
            Return m_codice_fattura
        End Get
        Set(ByVal value As Integer?)
            m_codice_fattura = value
        End Set
    End Property
    Public Property data_fattura() As DateTime?
        Get
            Return m_data_fattura
        End Get
        Set(ByVal value As DateTime?)
            m_data_fattura = value
        End Set
    End Property
    Public Property importo_fattura() As Double?
        Get
            Return m_importo_fattura
        End Get
        Set(ByVal value As Double?)
            m_importo_fattura = value
        End Set
    End Property


    Public ReadOnly Property data_creazione() As DateTime?
        Get
            Return m_data_creazione
        End Get
    End Property
    Public ReadOnly Property id_utente() As Integer?
        Get
            Return m_id_utente
        End Get
    End Property


    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer

        Dim sqlStr As String

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            sqlStr = "UPDATE odl SET" &
                " attivo = 0" &
                " WHERE num_odl = @num_odl"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@num_odl", System.Data.SqlDbType.Int, num_odl)

                Cmd.ExecuteNonQuery()
            End Using

            sqlStr = "INSERT INTO odl (num_odl,attivo,data_odl,id_stato_odl,id_tipo_doc_apertura,id_doc_apertura,num_crv_noleggio,id_veicolo,noleggio_in_corso,id_evento_apertura_danno,num_rds,rds_attivo,lavoro_eseguito,data_lavoro_eseguito,id_tipo_riparazione,id_fornitore,id_lavoro_da_eseguire,descrizione_lavoro,data_autorizzato,id_autorizzato_da,id_conducente,data_uscita,data_previsto_rientro,data_rientro,id_stazione_uscita,id_stazione_previsto_rientro,id_stazione_rientro,km_uscita,km_rientro,litri_uscita,litri_rientro,id_consegnato_da,id_ritirato_da,data_richiesta_preventivo,preventivo,id_autorizzato_pagamento,data_autorizzato_pagamento,importo,id_gruppo_danni_uscita,id_gruppo_danni_durante_odl,id_gruppo_danni_rientro,id_movimento_targa,tipo_fattura,anno_fattura,codice_fattura,data_fattura,importo_fattura,data_creazione,id_utente)" &
                " VALUES (@num_odl,@attivo,@data_odl,@id_stato_odl,@id_tipo_doc_apertura,@id_doc_apertura,@num_crv_noleggio,@id_veicolo,@noleggio_in_corso,@id_evento_apertura_danno,@num_rds,@rds_attivo,@lavoro_eseguito,@data_lavoro_eseguito,@id_tipo_riparazione,@id_fornitore,@id_lavoro_da_eseguire,@descrizione_lavoro,@data_autorizzato,@id_autorizzato_da,@id_conducente,@data_uscita,@data_previsto_rientro,@data_rientro,@id_stazione_uscita,@id_stazione_previsto_rientro,@id_stazione_rientro,@km_uscita,@km_rientro,@litri_uscita,@litri_rientro,@id_consegnato_da,@id_ritirato_da,@data_richiesta_preventivo,@preventivo,@id_autorizzato_pagamento,@data_autorizzato_pagamento,@importo,@id_gruppo_danni_uscita,@id_gruppo_danni_durante_odl,@id_gruppo_danni_rientro,@id_movimento_targa,@tipo_fattura,@anno_fattura,@codice_fattura,@data_fattura,@importo_fattura,@data_creazione,@id_utente)"

            m_data_creazione = Now
            m_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))
            m_attivo = 1

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@num_odl", System.Data.SqlDbType.Int, num_odl)
                addParametro(Cmd, "@attivo", System.Data.SqlDbType.Bit, attivo)
                addParametro(Cmd, "@data_odl", System.Data.SqlDbType.DateTime, data_odl)
                addParametro(Cmd, "@id_stato_odl", System.Data.SqlDbType.Int, id_stato_odl)
                addParametro(Cmd, "@id_tipo_doc_apertura", System.Data.SqlDbType.Int, id_tipo_doc_apertura)
                addParametro(Cmd, "@id_doc_apertura", System.Data.SqlDbType.Int, id_doc_apertura)
                addParametro(Cmd, "@num_crv_noleggio", System.Data.SqlDbType.Int, num_crv_noleggio)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                addParametro(Cmd, "@noleggio_in_corso", System.Data.SqlDbType.Bit, noleggio_in_corso)
                addParametro(Cmd, "@id_evento_apertura_danno", System.Data.SqlDbType.Int, id_evento_apertura_danno)
                addParametro(Cmd, "@num_rds", System.Data.SqlDbType.Int, num_rds)
                addParametro(Cmd, "@rds_attivo", System.Data.SqlDbType.Bit, rds_attivo)
                addParametro(Cmd, "@lavoro_eseguito", System.Data.SqlDbType.Bit, lavoro_eseguito)
                addParametro(Cmd, "@data_lavoro_eseguito", System.Data.SqlDbType.DateTime, data_lavoro_eseguito)
                addParametro(Cmd, "@id_tipo_riparazione", System.Data.SqlDbType.Int, id_tipo_riparazione)
                addParametro(Cmd, "@id_fornitore", System.Data.SqlDbType.Int, id_fornitore)
                addParametro(Cmd, "@id_lavoro_da_eseguire", System.Data.SqlDbType.Int, id_lavoro_da_eseguire)
                addParametro(Cmd, "@descrizione_lavoro", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione_lavoro))
                addParametro(Cmd, "@data_autorizzato", System.Data.SqlDbType.DateTime, data_autorizzato)
                addParametro(Cmd, "@id_autorizzato_da", System.Data.SqlDbType.Int, id_autorizzato_da)
                addParametro(Cmd, "@id_conducente", System.Data.SqlDbType.Int, id_conducente)
                addParametro(Cmd, "@data_uscita", System.Data.SqlDbType.DateTime, data_uscita)
                addParametro(Cmd, "@data_previsto_rientro", System.Data.SqlDbType.DateTime, data_previsto_rientro)
                'addParametro(Cmd, "@data_presunto_rientro", System.Data.SqlDbType.DateTime, data_previsto_rientro)

                addParametro(Cmd, "@data_rientro", System.Data.SqlDbType.DateTime, data_rientro)
                addParametro(Cmd, "@id_stazione_uscita", System.Data.SqlDbType.Int, id_stazione_uscita)
                addParametro(Cmd, "@id_stazione_previsto_rientro", System.Data.SqlDbType.Int, id_stazione_previsto_rientro)
                'addParametro(Cmd, "@id_stazione_presunto_rientro", System.Data.SqlDbType.Int, id_stazione_previsto_rientro)

                addParametro(Cmd, "@id_stazione_rientro", System.Data.SqlDbType.Int, id_stazione_rientro)
                addParametro(Cmd, "@km_uscita", System.Data.SqlDbType.Int, km_uscita)
                addParametro(Cmd, "@km_rientro", System.Data.SqlDbType.Int, km_rientro)
                addParametro(Cmd, "@litri_uscita", System.Data.SqlDbType.Int, litri_uscita)
                addParametro(Cmd, "@litri_rientro", System.Data.SqlDbType.Int, litri_rientro)
                addParametro(Cmd, "@id_consegnato_da", System.Data.SqlDbType.Int, id_consegnato_da)
                addParametro(Cmd, "@id_ritirato_da", System.Data.SqlDbType.Int, id_ritirato_da)
                addParametro(Cmd, "@data_richiesta_preventivo", System.Data.SqlDbType.DateTime, data_richiesta_preventivo)
                addParametro(Cmd, "@preventivo", System.Data.SqlDbType.Real, preventivo)
                addParametro(Cmd, "@id_autorizzato_pagamento", System.Data.SqlDbType.Int, id_autorizzato_pagamento)
                addParametro(Cmd, "@data_autorizzato_pagamento", System.Data.SqlDbType.DateTime, data_autorizzato_pagamento)
                addParametro(Cmd, "@importo", System.Data.SqlDbType.Real, importo)
                addParametro(Cmd, "@id_gruppo_danni_uscita", System.Data.SqlDbType.Int, id_gruppo_danni_uscita)
                addParametro(Cmd, "@id_gruppo_danni_durante_odl", System.Data.SqlDbType.Int, id_gruppo_danni_durante_odl)
                addParametro(Cmd, "@id_gruppo_danni_rientro", System.Data.SqlDbType.Int, id_gruppo_danni_rientro)
                addParametro(Cmd, "@id_movimento_targa", System.Data.SqlDbType.Int, id_movimento_targa)

                addParametro(Cmd, "@tipo_fattura", System.Data.SqlDbType.Int, tipo_fattura)
                addParametro(Cmd, "@anno_fattura", System.Data.SqlDbType.Int, anno_fattura)
                addParametro(Cmd, "@codice_fattura", System.Data.SqlDbType.Int, codice_fattura)
                addParametro(Cmd, "@data_fattura", System.Data.SqlDbType.DateTime, data_fattura)
                addParametro(Cmd, "@importo_fattura", System.Data.SqlDbType.Real, importo_fattura)

                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)

                Cmd.ExecuteNonQuery()

                'inserito il 05.11.2021 per aggiornare id_stazione_presunto_rientro e data_presunto_rientro se presenti
                'nella tabella movimenti_targa per la visulazizzazione corretta in previsione x targa
                'il num_odl è il campo num_riferimento

                If id_stazione_previsto_rientro.ToString <> "" And data_previsto_rientro.ToString <> "" Then


                    Dim dt As String = Year(data_previsto_rientro) & "-" & Month(data_previsto_rientro) & "-" & Day(data_previsto_rientro) & " " & Hour(data_previsto_rientro) & ":" & Minute(data_previsto_rientro) & ":" & Second(data_previsto_rientro)

                    sqlStr = "UPDATE movimenti_targa SET id_stazione_presunto_rientro='" & id_stazione_previsto_rientro & "' "
                    sqlStr += ",data_presunto_rientro=convert(datetime,'" & dt & "',102) "
                    sqlStr += "WHERE num_riferimento = '" & num_odl & "' "

                    Using Cmd1 As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)

                        Cmd1.ExecuteNonQuery()
                    End Using

                End If




            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "Select @@IDENTITY FROM odl"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As odl
        Dim mio_record As odl = New odl
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .num_odl = getValueOrNohing(Rs("num_odl"))
            .attivo = getValueOrNohing(Rs("attivo"))
            .data_odl = getValueOrNohing(Rs("data_odl"))
            .id_stato_odl = getValueOrNohing(Rs("id_stato_odl"))
            .id_tipo_doc_apertura = getValueOrNohing(Rs("id_tipo_doc_apertura"))
            .id_doc_apertura = getValueOrNohing(Rs("id_doc_apertura"))
            .num_crv_noleggio = getValueOrNohing(Rs("num_crv_noleggio"))
            .id_veicolo = getValueOrNohing(Rs("id_veicolo"))
            .noleggio_in_corso = getValueOrNohing(Rs("noleggio_in_corso"))
            .id_evento_apertura_danno = getValueOrNohing(Rs("id_evento_apertura_danno"))
            .num_rds = getValueOrNohing(Rs("num_rds"))
            .rds_attivo = getValueOrNohing(Rs("rds_attivo"))
            .lavoro_eseguito = getValueOrNohing(Rs("lavoro_eseguito"))
            .data_lavoro_eseguito = getValueOrNohing(Rs("data_lavoro_eseguito"))
            .id_tipo_riparazione = getValueOrNohing(Rs("id_tipo_riparazione"))
            .id_fornitore = getValueOrNohing(Rs("id_fornitore"))
            .id_lavoro_da_eseguire = getValueOrNohing(Rs("id_lavoro_da_eseguire"))
            .descrizione_lavoro = getValueOrNohing(Rs("descrizione_lavoro"))
            .data_autorizzato = getValueOrNohing(Rs("data_autorizzato"))
            .id_autorizzato_da = getValueOrNohing(Rs("id_autorizzato_da"))
            .id_conducente = getValueOrNohing(Rs("id_conducente"))
            .data_uscita = getValueOrNohing(Rs("data_uscita"))
            .data_previsto_rientro = getValueOrNohing(Rs("data_previsto_rientro"))
            .data_rientro = getValueOrNohing(Rs("data_rientro"))
            .id_stazione_uscita = getValueOrNohing(Rs("id_stazione_uscita"))
            .id_stazione_previsto_rientro = getValueOrNohing(Rs("id_stazione_previsto_rientro"))
            .id_stazione_rientro = getValueOrNohing(Rs("id_stazione_rientro"))
            .km_uscita = getValueOrNohing(Rs("km_uscita"))
            .km_rientro = getValueOrNohing(Rs("km_rientro"))
            .litri_uscita = getValueOrNohing(Rs("litri_uscita"))
            .litri_rientro = getValueOrNohing(Rs("litri_rientro"))
            .id_consegnato_da = getValueOrNohing(Rs("id_consegnato_da"))
            .id_ritirato_da = getValueOrNohing(Rs("id_ritirato_da"))
            .data_richiesta_preventivo = getValueOrNohing(Rs("data_richiesta_preventivo"))
            .preventivo = getDoubleOrNohing(Rs("preventivo"))
            .id_autorizzato_pagamento = getValueOrNohing(Rs("id_autorizzato_pagamento"))
            .data_autorizzato_pagamento = getValueOrNohing(Rs("data_autorizzato_pagamento"))
            .importo = getDoubleOrNohing(Rs("importo"))
            .id_gruppo_danni_uscita = getValueOrNohing(Rs("id_gruppo_danni_uscita"))
            .id_gruppo_danni_durante_odl = getValueOrNohing(Rs("id_gruppo_danni_durante_odl"))
            .id_gruppo_danni_rientro = getValueOrNohing(Rs("id_gruppo_danni_rientro"))
            .id_movimento_targa = getValueOrNohing(Rs("id_movimento_targa"))

            .tipo_fattura = getValueOrNohing(Rs("tipo_fattura"))
            .anno_fattura = getValueOrNohing(Rs("anno_fattura"))
            .codice_fattura = getValueOrNohing(Rs("codice_fattura"))
            .data_fattura = getValueOrNohing(Rs("data_fattura"))
            .importo_fattura = getDoubleOrNohing(Rs("importo_fattura"))

            .m_data_creazione = getValueOrNohing(Rs("data_creazione"))
            .m_id_utente = getValueOrNohing(Rs("id_utente"))

        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As odl
        Dim mio_record As odl = Nothing

        Dim sqlStr As String = "Select * FROM odl With(NOLOCK) WHERE id = " & id_record

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

    Public Shared Function getRecordDaNumODL(ByVal num_odl As Integer) As odl
        Dim mio_record As odl = Nothing

        Dim sqlStr As String = "Select * FROM odl With(NOLOCK) WHERE attivo = 1 And num_odl = " & num_odl

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


    Public Shared Function getRecordDaDocumento(ByVal id_tipo_documento As tipo_documento, ByVal num_documento As Integer, ByVal num_crv As Integer) As odl
        Dim mio_record As odl = Nothing

        Dim sqlStr As String = "Select * FROM odl With(NOLOCK)" &
            " WHERE attivo = 1" &
            " And id_tipo_doc_apertura = " & id_tipo_documento &
            " And id_doc_apertura = " & num_documento &
            " And num_crv_noleggio = " & num_crv

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

    'Public Function AggiornaRecord() As Boolean
    '    AggiornaRecord = False

    '    Dim sqlStr As String = "UPDATE odl Set" & _
    '        " WHERE id = @id"

    '    Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)


    '            Dbc.Open()
    '            Cmd.ExecuteNonQuery()
    '            AggiornaRecord = True
    '        End Using
    '    End Using
    'End Function

    Public Function EliminaRecord() As Boolean
        Return EliminaRecord(Me.id)
    End Function

    Public Shared Function EliminaRecord(ByVal id_record As Integer) As Boolean
        EliminaRecord = False

        Dim sqlStr As String = "DELETE FROM odl WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord odl " & ex.Message)
        End Try
    End Function

    Public Shared Function VerificaODLApertoSuVeicolo(ByVal id_veicolo As Integer) As String
        VerificaODLApertoSuVeicolo = ""

        Dim sqlStr As String = "Select TOP 1 odl.num_odl FROM odl With(NOLOCK)" &
            " INNER JOIN odl_stato st With(NOLOCK) On odl.id_stato_odl = st.id" &
            " WHERE odl.attivo = 1" &
            " And odl.id_veicolo = @id_veicolo" &
            " And st.stato_chiuso = 0"

        HttpContext.Current.Trace.Write(sqlStr & " " & id_veicolo)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                Dbc.Open()
                Dim num_odl As String = Cmd.ExecuteScalar & ""

                Return num_odl
            End Using
        End Using
    End Function

    Public Shared Function VerificaAutoALavaggio(ByVal id_veicolo As Integer) As String
        VerificaAutoALavaggio = ""

        Dim sqlStr As String = "Select TOP 1 id FROM lavaggi With(NOLOCK)" &
            " WHERE stato = 0 " &
            " And id_veicolo = @id_veicolo"



        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                Dbc.Open()
                Dim id_lavaggio As String = Cmd.ExecuteScalar & ""

                Return id_lavaggio
            End Using
        End Using
    End Function

    Public Shared Function VerificaAutoARifornimento(ByVal id_veicolo As Integer) As String
        VerificaAutoARifornimento = ""

        Dim sqlStr As String = "Select TOP 1 id FROM rifornimenti With(NOLOCK)" &
            " WHERE Not num_rifornimento Is NULL And data_rientro_parco Is NULL " &
            " And id_veicolo = @id_veicolo"



        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                Dbc.Open()
                Dim id_rifornimento As String = Cmd.ExecuteScalar & ""

                Return id_rifornimento
            End Using
        End Using
    End Function

    Public Shared Function VerificaAutoInTrasferimento(ByVal id_veicolo As Integer) As String
        VerificaAutoInTrasferimento = ""

        Dim sqlStr As String = "Select TOP 1 id FROM trasferimenti With(NOLOCK)" &
            " WHERE status='" & Costanti.id_trasferimento_in_corso & "' " &
            " AND id_veicolo = @id_veicolo"



        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                Dbc.Open()
                Dim id_trasferimento As String = Cmd.ExecuteScalar & ""

                Return id_trasferimento
            End Using
        End Using
    End Function

    Public Shared Function getStatoODLAperto(ByVal id_stato_odl As Integer) As Boolean
        getStatoODLAperto = False

        Dim sqlStr As String = "SELECT stato_chiuso FROM odl_stato st WITH(NOLOCK)" &
            " WHERE id = @id_stato_odl"

        HttpContext.Current.Trace.Write(sqlStr & " " & id_stato_odl)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_stato_odl", System.Data.SqlDbType.Int, id_stato_odl)
                Dbc.Open()
                Dim stato_odl As String = Cmd.ExecuteScalar & ""

                If stato_odl = "" Then
                    Return False
                Else
                    Return Not Boolean.Parse(stato_odl)
                End If
            End Using
        End Using
    End Function

    Public Function AggiornaDocumentoApertura() As Boolean
        AggiornaDocumentoApertura = False

        Dim sqlStr As String = "UPDATE odl SET" &
            " id_tipo_doc_apertura = @id_tipo_doc_apertura," &
            " id_doc_apertura = @id_doc_apertura," &
            " num_crv_noleggio = @num_crv_noleggio" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_tipo_doc_apertura", System.Data.SqlDbType.Int, id_tipo_doc_apertura)
                addParametro(Cmd, "@id_doc_apertura", System.Data.SqlDbType.Int, id_doc_apertura)
                addParametro(Cmd, "@num_crv_noleggio", System.Data.SqlDbType.Int, num_crv_noleggio)

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)
                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaDocumentoApertura = True
            End Using
        End Using
    End Function
End Class

Public Class odl_nota
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_num_odl As Integer?
    Protected m_nota As String
    Protected m_id_utente As Integer?
    Protected m_data_creazione As DateTime?

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property num_odl() As Integer?
        Get
            Return m_num_odl
        End Get
        Set(ByVal value As Integer?)
            m_num_odl = value
        End Set
    End Property
    Public Property nota() As String
        Get
            Return m_nota
        End Get
        Set(ByVal value As String)
            m_nota = value
        End Set
    End Property
    Public ReadOnly Property id_utente() As Integer?
        Get
            Return m_id_utente
        End Get
    End Property
    Public ReadOnly Property data_creazione() As DateTime?
        Get
            Return m_data_creazione
        End Get
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO odl_nota (num_odl,nota,id_utente,data_creazione)" &
            " VALUES (@num_odl,@nota,@id_utente,@data_creazione)"

        m_data_creazione = Now
        m_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@num_odl", System.Data.SqlDbType.Int, num_odl)
                addParametro(Cmd, "@nota", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nota))
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM odl_nota"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As odl_nota
        Dim mio_record As odl_nota = New odl_nota
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .num_odl = getValueOrNohing(Rs("num_odl"))
            .nota = getValueOrNohing(Rs("nota"))
            .m_id_utente = getValueOrNohing(Rs("id_utente"))
            .m_data_creazione = getValueOrNohing(Rs("data_creazione"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As odl_nota
        Dim mio_record As odl_nota = Nothing

        Dim sqlStr As String = "SELECT * FROM odl_nota WITH(NOLOCK) WHERE id = " & id_record

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

        Dim sqlStr As String = "UPDATE odl_nota SET" &
            " num_odl = @num_odl," &
            " nota = @nota," &
            " id_utente = @id_utente," &
            " data_creazione = @data_creazione" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@num_odl", System.Data.SqlDbType.Int, num_odl)
                addParametro(Cmd, "@nota", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nota))
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)

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

        Dim sqlStr As String = "DELETE FROM odl_nota WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord odl_nota: " & ex.Message)
        End Try
    End Function
End Class


Public Enum enum_note_tipo
    NonValido = 0
    note_odl
    note_danni
    note_rds
    note_contratto
    note_prenotazione
    note_preventivo
End Enum

Public Class note
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_id_tipo As Integer?
    Protected m_id_documento As Integer?
    Protected m_data_creazione As DateTime?
    Protected m_id_utente As Integer?
    Protected m_nota As String
    Protected m_data_modifica As DateTime?
    Protected m_id_utente_modifica As Integer?

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property id_tipo() As Integer?
        Get
            Return m_id_tipo
        End Get
        Set(ByVal value As Integer?)
            m_id_tipo = value
        End Set
    End Property
    Public Property id_documento() As Integer?
        Get
            Return m_id_documento
        End Get
        Set(ByVal value As Integer?)
            m_id_documento = value
        End Set
    End Property
    Public ReadOnly Property data_creazione() As DateTime?
        Get
            Return m_data_creazione
        End Get
    End Property
    Public ReadOnly Property id_utente() As Integer?
        Get
            Return m_id_utente
        End Get
    End Property
    Public Property nota() As String
        Get
            Return m_nota
        End Get
        Set(ByVal value As String)
            m_nota = value
        End Set
    End Property
    Public Property data_modifica() As DateTime?
        Get
            Return m_data_modifica
        End Get
        Set(ByVal value As DateTime?)
            m_data_modifica = value
        End Set
    End Property
    Public Property id_utente_modifica() As Integer?
        Get
            Return m_id_utente_modifica
        End Get
        Set(ByVal value As Integer?)
            m_id_utente_modifica = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO note (id_tipo,id_documento,data_creazione,id_utente,nota)" &
            " VALUES (@id_tipo,@id_documento,@data_creazione,@id_utente,@nota)"

        m_data_creazione = Now
        m_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_tipo", System.Data.SqlDbType.Int, id_tipo)
                addParametro(Cmd, "@id_documento", System.Data.SqlDbType.Int, id_documento)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)
                addParametro(Cmd, "@nota", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nota))

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM note"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As note
        Dim mio_record As note = New note
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .id_tipo = getValueOrNohing(Rs("id_tipo"))
            .id_documento = getValueOrNohing(Rs("id_documento"))
            .m_data_creazione = getValueOrNohing(Rs("data_creazione"))
            .m_id_utente = getValueOrNohing(Rs("id_utente"))
            .nota = getValueOrNohing(Rs("nota"))
            .data_modifica = getValueOrNohing(Rs("data_modifica"))
            .id_utente_modifica = getValueOrNohing(Rs("id_utente_modifica"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As note
        Dim mio_record As note = Nothing

        Dim sqlStr As String = "SELECT * FROM note WITH(NOLOCK) WHERE id = " & id_record

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

        m_data_modifica = Now
        m_id_utente_modifica = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Dim sqlStr As String = "UPDATE note SET" & _
            " data_modifica = @data_modifica," & _
            " id_utente_modifica = @id_utente_modifica," & _
            " nota = @nota" & _
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@data_modifica", System.Data.SqlDbType.DateTime, data_modifica)
                addParametro(Cmd, "@id_utente_modifica", System.Data.SqlDbType.Int, id_utente_modifica)
                addParametro(Cmd, "@nota", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nota))

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

        Dim sqlStr As String = "DELETE FROM note WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord note: " & ex.Message)
        End Try
    End Function
End Class