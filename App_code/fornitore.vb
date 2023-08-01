Public Class fornitore
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_attivo As Boolean
    Protected m_autorizzato As Boolean
    Protected m_ragione_sociale As String
    Protected m_referente As String
    Protected m_indirizzo As String
    Protected m_comune As String
    Protected m_cap As String
    Protected m_provincia As String
    Protected m_id_comune_ares As Integer?
    Protected m_piva As String
    Protected m_telefono As String
    Protected m_fax As String
    Protected m_note As String
    Protected m_data_creazione As DateTime?
    Protected m_id_utente As Integer?
    Protected m_data_modifica As Datetime?
    Protected m_id_utente_modifica As Integer?

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
    Public Property autorizzato() As Boolean
        Get
            Return m_autorizzato
        End Get
        Set(ByVal value As Boolean)
            m_autorizzato = value
        End Set
    End Property
    Public Property ragione_sociale() As String
        Get
            Return m_ragione_sociale
        End Get
        Set(ByVal value As String)
            m_ragione_sociale = value
        End Set
    End Property
    Public Property referente() As String
        Get
            Return m_referente
        End Get
        Set(ByVal value As String)
            m_referente = value
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
    Public Property comune() As String
        Get
            Return m_comune
        End Get
        Set(ByVal value As String)
            m_comune = value
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
    Public Property id_comune_ares() As Integer?
        Get
            Return m_id_comune_ares
        End Get
        Set(ByVal value As Integer?)
            m_id_comune_ares = value
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
    Public Property note() As String
        Get
            Return m_note
        End Get
        Set(ByVal value As String)
            m_note = value
        End Set
    End Property
    Public Property data_creazione() As DateTime?
        Get
            Return m_data_creazione
        End Get
        Set(ByVal value As DateTime?)
            m_data_creazione = value
        End Set
    End Property
    Public Property id_utente() As Integer?
        Get
            Return m_id_utente
        End Get
        Set(ByVal value As Integer?)
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


    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO fornitore (attivo,autorizzato,ragione_sociale,referente,indirizzo,comune,cap,provincia,id_comune_ares,piva,telefono,fax,note,data_creazione,id_utente)" & _
            " VALUES (@attivo,@autorizzato,@ragione_sociale,@referente,@indirizzo,@comune,@cap,@provincia,@id_comune_ares,@piva,@telefono,@fax,@note,@data_creazione,@id_utente)"

        m_data_creazione = Now
        m_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@attivo", System.Data.SqlDbType.Bit, attivo)
                addParametro(Cmd, "@autorizzato", System.Data.SqlDbType.Bit, autorizzato)
                addParametro(Cmd, "@ragione_sociale", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(ragione_sociale, 50))
                addParametro(Cmd, "@referente", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(referente, 50))
                addParametro(Cmd, "@indirizzo", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(indirizzo, 50))
                addParametro(Cmd, "@comune", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(comune, 50))
                addParametro(Cmd, "@cap", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(cap, 5))
                addParametro(Cmd, "@provincia", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(provincia, 2))
                addParametro(Cmd, "@id_comune_ares", System.Data.SqlDbType.Int, id_comune_ares)
                addParametro(Cmd, "@piva", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(piva, 20))
                addParametro(Cmd, "@telefono", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(telefono, 20))
                addParametro(Cmd, "@fax", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(fax, 20))
                addParametro(Cmd, "@note", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(note))
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM fornitore"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As fornitore
        Dim mio_record As fornitore = New fornitore
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .attivo = getValueOrNohing(Rs("attivo"))
            .autorizzato = getValueOrNohing(Rs("autorizzato"))
            .ragione_sociale = getValueOrNohing(Rs("ragione_sociale"))
            .referente = getValueOrNohing(Rs("referente"))
            .indirizzo = getValueOrNohing(Rs("indirizzo"))
            .comune = getValueOrNohing(Rs("comune"))
            .cap = getValueOrNohing(Rs("cap"))
            .provincia = getValueOrNohing(Rs("provincia"))
            .id_comune_ares = getValueOrNohing(Rs("id_comune_ares"))
            .piva = getValueOrNohing(Rs("piva"))
            .telefono = getValueOrNohing(Rs("telefono"))
            .fax = getValueOrNohing(Rs("fax"))
            .note = getValueOrNohing(Rs("note"))
            .data_creazione = getValueOrNohing(Rs("data_creazione"))
            .id_utente = getValueOrNohing(Rs("id_utente"))
            .data_modifica = getValueOrNohing(Rs("data_modifica"))
            .id_utente_modifica = getValueOrNohing(Rs("id_utente_modifica"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As fornitore
        Dim mio_record As fornitore = Nothing

        Dim sqlStr As String = "SELECT * FROM fornitore WITH(NOLOCK) WHERE id = " & id_record

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

        Dim sqlStr As String = "UPDATE fornitore SET" &
            " attivo = @attivo," &
            " autorizzato = @autorizzato," &
            " ragione_sociale = @ragione_sociale," &
            " referente = @referente," &
            " indirizzo = @indirizzo," &
            " comune = @comune," &
            " cap = @cap," &
            " provincia = @provincia," &
            " id_comune_ares = @id_comune_ares," &
            " piva = @piva," &
            " telefono = @telefono," &
            " fax = @fax," &
            " note = @note," &
            " data_modifica = @data_modifica," &
            " id_utente_modifica = @id_utente_modifica" &
            " WHERE id = @id"

        m_data_modifica = Now
        m_id_utente_modifica = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@attivo", System.Data.SqlDbType.Bit, attivo)
                addParametro(Cmd, "@autorizzato", System.Data.SqlDbType.Bit, autorizzato)
                addParametro(Cmd, "@ragione_sociale", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(ragione_sociale, 50))
                addParametro(Cmd, "@referente", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(referente, 50))
                addParametro(Cmd, "@indirizzo", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(indirizzo, 50))
                addParametro(Cmd, "@comune", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(comune, 50))
                addParametro(Cmd, "@cap", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(cap, 5))
                addParametro(Cmd, "@provincia", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(provincia, 2))
                addParametro(Cmd, "@id_comune_ares", System.Data.SqlDbType.Int, id_comune_ares)
                addParametro(Cmd, "@piva", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(piva, 20))
                addParametro(Cmd, "@telefono", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(telefono, 20))
                addParametro(Cmd, "@fax", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(fax, 20))
                addParametro(Cmd, "@note", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(note))
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

        Dim sqlStr As String = "DELETE FROM fornitore WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord fornitore: " & ex.Message)
        End Try
    End Function
End Class

Public Class fornitore_tipo
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
        Dim sqlStr As String = "INSERT INTO fornitore_tipo (descrizione)" &
            " VALUES (@descrizione)"

        'm_data_creazione = Now
        'm_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM fornitore_tipo"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As fornitore_tipo
        Dim mio_record As fornitore_tipo = New fornitore_tipo
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .descrizione = getValueOrNohing(Rs("descrizione"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As fornitore_tipo
        Dim mio_record As fornitore_tipo = Nothing

        Dim sqlStr As String = "SELECT * FROM fornitore_tipo WITH(NOLOCK) WHERE id = " & id_record

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

        Dim sqlStr As String = "UPDATE fornitore_tipo SET" &
            " descrizione = @descrizione" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
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

        Dim sqlStr As String = "DELETE FROM fornitore_tipo WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord fornitore_tipo: " & ex.Message)
        End Try
    End Function
End Class

Public Class drivers
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_cognome As String
    Protected m_nome As String
    Protected m_data_nascita As Date?
    Protected m_citta_nascita As String
    Protected m_codice_fiscale As String
    Protected m_indirizzo As String
    Protected m_citta As String
    Protected m_cap As String
    Protected m_provincia As String
    Protected m_id_comune_ares As Integer?
    Protected m_telefono As String
    Protected m_patente As String
    Protected m_tipo_patente As String
    Protected m_luogo_emissione As String
    Protected m_scadenza_patente As Date?
    Protected m_data_creazione As DateTime?
    Protected m_id_utente As Integer?
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
    Public Property data_nascita() As Date?
        Get
            Return m_data_nascita
        End Get
        Set(ByVal value As Date?)
            m_data_nascita = value
        End Set
    End Property
    Public Property citta_nascita() As String
        Get
            Return m_citta_nascita
        End Get
        Set(ByVal value As String)
            m_citta_nascita = value
        End Set
    End Property
    Public Property codice_fiscale() As String
        Get
            Return m_codice_fiscale
        End Get
        Set(ByVal value As String)
            m_codice_fiscale = value
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
    Public Property id_comune_ares() As Integer?
        Get
            Return m_id_comune_ares
        End Get
        Set(ByVal value As Integer?)
            m_id_comune_ares = value
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
    Public Property patente() As String
        Get
            Return m_patente
        End Get
        Set(ByVal value As String)
            m_patente = value
        End Set
    End Property
    Public Property tipo_patente() As String
        Get
            Return m_tipo_patente
        End Get
        Set(ByVal value As String)
            m_tipo_patente = value
        End Set
    End Property
    Public Property luogo_emissione() As String
        Get
            Return m_luogo_emissione
        End Get
        Set(ByVal value As String)
            m_luogo_emissione = value
        End Set
    End Property
    Public Property scadenza_patente() As Date?
        Get
            Return m_scadenza_patente
        End Get
        Set(ByVal value As Date?)
            m_scadenza_patente = value
        End Set
    End Property
    Public Property data_creazione() As DateTime?
        Get
            Return m_data_creazione
        End Get
        Set(ByVal value As DateTime?)
            m_data_creazione = value
        End Set
    End Property
    Public Property id_utente() As Integer?
        Get
            Return m_id_utente
        End Get
        Set(ByVal value As Integer?)
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

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO drivers (cognome,nome,data_nascita,citta_nascita,codice_fiscale,indirizzo,citta,cap,provincia,id_comune_ares,telefono,patente,tipo_patente,luogo_emissione,scadenza_patente,data_creazione,id_utente)" &
            " VALUES (@cognome,@nome,@data_nascita,@citta_nascita,@codice_fiscale,@indirizzo,@citta,@cap,@provincia,@id_comune_ares,@telefono,@patente,@tipo_patente,@luogo_emissione,@scadenza_patente,@data_creazione,@id_utente)"

        m_data_creazione = Now
        m_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@cognome", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(cognome, 50))
                addParametro(Cmd, "@nome", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nome, 50))
                addParametro(Cmd, "@data_nascita", System.Data.SqlDbType.Date, data_nascita)
                addParametro(Cmd, "@citta_nascita", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(citta_nascita, 50))
                addParametro(Cmd, "@codice_fiscale", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(codice_fiscale, 20))
                addParametro(Cmd, "@indirizzo", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(indirizzo, 50))
                addParametro(Cmd, "@citta", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(citta, 50))
                addParametro(Cmd, "@cap", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(cap, 5))
                addParametro(Cmd, "@provincia", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(provincia, 2))
                addParametro(Cmd, "@id_comune_ares", System.Data.SqlDbType.Int, id_comune_ares)
                addParametro(Cmd, "@telefono", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(telefono, 20))
                addParametro(Cmd, "@patente", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(patente, 30))
                addParametro(Cmd, "@tipo_patente", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(tipo_patente, 5))
                addParametro(Cmd, "@luogo_emissione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(luogo_emissione, 50))
                addParametro(Cmd, "@scadenza_patente", System.Data.SqlDbType.Date, scadenza_patente)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM drivers"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As drivers
        Dim mio_record As drivers = New drivers
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .cognome = getValueOrNohing(Rs("cognome"))
            .nome = getValueOrNohing(Rs("nome"))
            .data_nascita = getValueOrNohing(Rs("data_nascita"))
            .citta_nascita = getValueOrNohing(Rs("citta_nascita"))
            .codice_fiscale = getValueOrNohing(Rs("codice_fiscale"))
            .indirizzo = getValueOrNohing(Rs("indirizzo"))
            .citta = getValueOrNohing(Rs("citta"))
            .cap = getValueOrNohing(Rs("cap"))
            .provincia = getValueOrNohing(Rs("provincia"))
            .id_comune_ares = getValueOrNohing(Rs("id_comune_ares"))
            .telefono = getValueOrNohing(Rs("telefono"))
            .patente = getValueOrNohing(Rs("patente"))
            .tipo_patente = getValueOrNohing(Rs("tipo_patente"))
            .luogo_emissione = getValueOrNohing(Rs("luogo_emissione"))
            .scadenza_patente = getValueOrNohing(Rs("scadenza_patente"))
            .data_creazione = getValueOrNohing(Rs("data_creazione"))
            .id_utente = getValueOrNohing(Rs("id_utente"))
            .data_modifica = getValueOrNohing(Rs("data_modifica"))
            .id_utente_modifica = getValueOrNohing(Rs("id_utente_modifica"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As drivers
        Dim mio_record As drivers = Nothing

        Dim sqlStr As String = "SELECT * FROM drivers WITH(NOLOCK) WHERE id = " & id_record

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

        Dim sqlStr As String = "UPDATE drivers SET" &
            " cognome = @cognome," &
            " nome = @nome," &
            " data_nascita = @data_nascita," &
            " citta_nascita = @citta_nascita," &
            " codice_fiscale = @codice_fiscale," &
            " indirizzo = @indirizzo," &
            " citta = @citta," &
            " cap = @cap," &
            " provincia = @provincia," &
            " id_comune_ares = @id_comune_ares," &
            " telefono = @telefono," &
            " patente = @patente," &
            " tipo_patente = @tipo_patente," &
            " luogo_emissione = @luogo_emissione," &
            " scadenza_patente = @scadenza_patente," &
            " data_modifica = @data_modifica," &
            " id_utente_modifica = @id_utente_modifica" &
            " WHERE id = @id"

        m_data_modifica = Now
        m_id_utente_modifica = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@cognome", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(cognome, 50))
                addParametro(Cmd, "@nome", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nome, 50))
                addParametro(Cmd, "@data_nascita", System.Data.SqlDbType.Date, data_nascita)
                addParametro(Cmd, "@citta_nascita", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(citta_nascita, 50))
                addParametro(Cmd, "@codice_fiscale", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(codice_fiscale, 20))
                addParametro(Cmd, "@indirizzo", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(indirizzo, 50))
                addParametro(Cmd, "@citta", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(citta, 50))
                addParametro(Cmd, "@cap", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(cap, 5))
                addParametro(Cmd, "@provincia", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(provincia, 2))
                addParametro(Cmd, "@id_comune_ares", System.Data.SqlDbType.Int, id_comune_ares)
                addParametro(Cmd, "@telefono", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(telefono, 20))
                addParametro(Cmd, "@patente", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(patente, 30))
                addParametro(Cmd, "@tipo_patente", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(tipo_patente, 5))
                addParametro(Cmd, "@luogo_emissione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(luogo_emissione, 50))
                addParametro(Cmd, "@scadenza_patente", System.Data.SqlDbType.Date, scadenza_patente)
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

        Dim sqlStr As String = "DELETE FROM drivers WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord drivers: " & ex.Message)
        End Try
    End Function
End Class