Public Enum enum_tipo_pagamento
    POS
    Contanti
    Carta_Credito
    Voucher
    Full_Credit
    Complimentary
    Abbuoni
End Enum

Public Enum FiltroTipoPagamentoContanti
    MCR = 1
    DEPOSITO = 2
    CASH = 4
    RIMBORSO = 8
    Prenotazione = MCR Or RIMBORSO
    AperturaContratto = DEPOSITO Or RIMBORSO
    ChiusuraContratto = DEPOSITO Or RIMBORSO Or CASH
End Enum

Public Enum enum_tipo_pagamento_p1000
    DE_DEPOSITO_SU_RA = -1886319629
    COMPLIMENTARY = 276309583
    IN_INCASSO_EXTRA = 422080218
    CH_PAGAMENTO_CASH = 3
    MCR_PAGAMENTO_CASH = -714677539  'RIMBORSO DEPOSITO SU RA
    SE_PRELIEVO_DA_SEDE = 139849350
    PST_PRELIEVO_DA_STAZIONE = -1896761599
    RCC_Rimborsi_Vari_su_CC = 943238740
    AA_ABBUONO_ATTIVO = 653868889
    AP_ABBUONO_PASSIVO = -1577445210
    CC_C_CREDITO_AUTORIZZAZIONE = -1768195793
    CI_C_CREDITO_INCASSO = -1768195794
    FC_FULL_CREDIT = -496006762
    RB_RIMBORSO_BENZINA = 1
    RE_RIMBORSO_EXTRA = 936346504
    RO_RIMBORSO_OLIO = 184249272
    RV_RIMBORSI_VARI = 900624220
    RCO_Rimborsi_Vari_su_CASH = 2038953789
    VI_VOUCHER_INTERNET_ADV = -438610306
    V1_VOUCHER_1 = -2122129675
    V0_VOUCHER_0 = -923498684
    DEPOSITO_POS = -2005199230
    Richiesta_PRE_Autoriz_POS = -438610305
    Integraz_PRE_Autoriz_POS = -438610303
    AUTORIZZAZIONE_POS = 1071240973
    Chiusura_PRE_Autoriz_POS = -438610304
    INCASSO_POS = 1011098650
    Storno_PRE_Autoriz_POS = -438610302
    Storno_RichPreautPOS = -438610301
    Storno_IntegPreautPOS = -438610300
    Storno_ChiusPreautPOS = -438610299
    Storno_INCASSO_POS = -438610298
    RIMBORSO_POS = -722881389
    RIMBORSO_POS_SEDE = 1031826137
    PCC_Rimborsi_Vari_su_POS = 1631542
    PC_PETTY_CASH = -1271703469
    COM_PAGAMENTO_COMMISSIONI = -1174775529
    SV_SPESE_VARIE = 901094198
    CA_RIMBORSO_SU_RA = 2
    VB_VERSAMENTO_A_BANCA = 834992313
    VST_VERSAMENTO_A_STAZIONE = 1013576448
    VS_VERSAMENTO_A_SEDE = 1895581336
End Enum

Public Enum enum_tipo_pagamento_ares
    Richiesta = 1
    Integrazione = 2
    Chiusura = 3
    Vendita = 4
    Rimborso = 5
    Storno_Ultima_Operazione = 6
    Pagamento_Contanti = 7
    Deposito_su_RA = 8
    Rimborso_su_RA = 9
    Carta_Credito_Telefonico_Autorizzazione = 10
    Abbuono_Attivo = 11
    Abbuono_Passivo = 12
    Full_Credit = 13
    Complimentary = 14
    MCR_Pagamento_Cash = 15
    Carta_Credito_Telefonico_Incasso = 16
End Enum

Public Class InizializzaDatiCassa
    Public id_stazione As Integer
    Public id_tipo_documento As TipoFattura
    Public num_documento As Integer
    Public id_tipo_pagamento As enum_tipo_pagamento
    Public TipoPagamentoContanti As FiltroTipoPagamentoContanti = FiltroTipoPagamentoContanti.RIMBORSO
    Public importo_suggerito As Double?
End Class

Public Class DueDate
    Public MinData As DateTime
    Public MaxData As DateTime
End Class

Public Class cassa_petty_cash
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_num_petty_cash As Integer
    Protected m_id_stazione As Integer?
    Protected m_data_creazione As DateTime?
    Protected m_id_utente As Integer?
    Protected m_data_chiusura As DateTime?
    Protected m_id_utente_chiusura As Integer?
    Protected m_importo As Double?
    Protected m_id_pagamenti_extra As Integer?

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public ReadOnly Property num_petty_cash() As Integer
        Get
            Return m_num_petty_cash
        End Get
    End Property
    Public Property id_stazione() As Integer?
        Get
            Return m_id_stazione
        End Get
        Set(ByVal value As Integer?)
            m_id_stazione = value
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
    Public Property importo() As Double?
        Get
            Return m_importo
        End Get
        Set(ByVal value As Double?)
            m_importo = value
        End Set
    End Property
    Public Property id_pagamenti_extra() As Integer?
        Get
            Return m_id_pagamenti_extra
        End Get
        Set(ByVal value As Integer?)
            m_id_pagamenti_extra = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO cassa_petty_cash (num_petty_cash,id_stazione,data_creazione,id_utente,data_chiusura,id_utente_chiusura,importo,id_pagamenti_extra)" & _
            " VALUES (@num_petty_cash,@id_stazione,@data_creazione,@id_utente,@data_chiusura,@id_utente_chiusura,@importo,@id_pagamenti_extra)"

        m_num_petty_cash = Contatori.getContatore_Petty_Cash(id_stazione)
        m_data_creazione = Now
        m_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@num_petty_cash", System.Data.SqlDbType.Int, num_petty_cash)
                addParametro(Cmd, "@id_stazione", System.Data.SqlDbType.Int, id_stazione)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)
                addParametro(Cmd, "@data_chiusura", System.Data.SqlDbType.DateTime, data_chiusura)
                addParametro(Cmd, "@id_utente_chiusura", System.Data.SqlDbType.Int, id_utente_chiusura)
                addParametro(Cmd, "@importo", System.Data.SqlDbType.Real, importo)
                addParametro(Cmd, "@id_pagamenti_extra", System.Data.SqlDbType.Int, id_pagamenti_extra)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM cassa_petty_cash"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As cassa_petty_cash
        Dim mio_record As cassa_petty_cash = New cassa_petty_cash
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .m_num_petty_cash = getValueOrNohing(Rs("num_petty_cash"))
            .id_stazione = getValueOrNohing(Rs("id_stazione"))
            .m_data_creazione = getValueOrNohing(Rs("data_creazione"))
            .m_id_utente = getValueOrNohing(Rs("id_utente"))
            .m_data_chiusura = getValueOrNohing(Rs("data_chiusura"))
            .m_id_utente_chiusura = getValueOrNohing(Rs("id_utente_chiusura"))
            .importo = getDoubleOrNohing(Rs("importo"))
            .id_pagamenti_extra = getValueOrNohing(Rs("id_pagamenti_extra"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As cassa_petty_cash
        Dim mio_record As cassa_petty_cash = Nothing

        Dim sqlStr As String = "SELECT * FROM cassa_petty_cash WITH(NOLOCK) WHERE id = " & id_record

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

    Public Shared Function getRecordDaNumPettyCash(ByVal num_petty_cash As Integer) As cassa_petty_cash
        Dim mio_record As cassa_petty_cash = Nothing

        Dim sqlStr As String = "SELECT * FROM cassa_petty_cash WITH(NOLOCK) WHERE num_petty_cash = " & num_petty_cash

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

    Public Shared Function getRecordDaData(ByVal id_stazione As Integer, ByVal data_validita As Date) As cassa_petty_cash
        Dim mio_record As cassa_petty_cash = Nothing

        Dim sqlStr As String = "SELECT TOP 1 * FROM cassa_petty_cash WITH(NOLOCK)" &
            " WHERE id_stazione = @id_stazione" &
            " AND data_creazione <= @data_validita" &
            " AND (data_chiusura > @data_validita OR data_chiusura IS NULL)" &
            " ORDER BY id DESC"
        HttpContext.Current.Trace.Write("getRecordDaData(" & data_validita & ") " & sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_stazione", System.Data.SqlDbType.Int, id_stazione)
                addParametro(Cmd, "@data_validita", System.Data.SqlDbType.DateTime, data_validita)

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

    Public Shared Function getRecordPettyCashAperto(ByVal id_stazione As String) As cassa_petty_cash
        Dim mio_record As cassa_petty_cash = Nothing

        Dim sqlStr As String = "SELECT * FROM cassa_petty_cash WITH(NOLOCK)" &
            " WHERE id_stazione = " & id_stazione &
            " AND data_chiusura IS NULL"

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

    Public Function ChiudiCassa() As Boolean
        ChiudiCassa = False

        importo = getTotaleImporto()

        Dim mio_record As PAGAMENTI_EXTRA = New PAGAMENTI_EXTRA
        With mio_record
            .ID_STAZIONE = id_stazione
            .Data = New Date(Year(Now), Month(Now), Day(Now), 0, 0, 0)
            .DATA_OPERAZIONE = Now

            .N_PREN_RIF = Nothing
            .N_CONTRATTO_RIF = Nothing
            .N_RDS_RIF = Nothing
            .N_MULTA_RIF = Nothing

            .ID_TIPPAG = -1271703469 ' PC-PETTY CASH

            .ID_ModPag = 4 ' CONTANTI


            .PER_IMPORTO = -importo

            .NOTE = "Chiusura Petty Cash " & num_petty_cash

            .Titolo = Nothing
            .Scadenza = Nothing

            .nr_aut = Nothing
            .NR_BATCH = Nothing
            .CASSA = Nothing
            .NR_PREAUT = Nothing
            .TERMINAL_ID = Nothing

            .SalvaRecord()
        End With

        Dim sqlStr As String = "UPDATE cassa_petty_cash SET" &
            " data_chiusura = @data_chiusura," &
            " id_utente_chiusura = @id_utente_chiusura," &
            " importo = @importo," &
            " id_pagamenti_extra = @id_pagamenti_extra" &
            " WHERE id = @id"

        m_data_chiusura = Now
        m_id_utente_chiusura = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@data_chiusura", System.Data.SqlDbType.DateTime, data_chiusura)
                addParametro(Cmd, "@id_utente_chiusura", System.Data.SqlDbType.Int, id_utente_chiusura)
                addParametro(Cmd, "@importo", System.Data.SqlDbType.Real, importo)
                addParametro(Cmd, "@id_pagamenti_extra", System.Data.SqlDbType.Int, mio_record.ID_CTR)

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                ChiudiCassa = True
            End Using
        End Using
    End Function

    Public Function EliminaRecord() As Boolean
        Return EliminaRecord(Me.id)
    End Function

    Public Shared Function EliminaRecord(ByVal id_record As Integer) As Boolean
        EliminaRecord = False

        Dim sqlStr As String = "DELETE FROM cassa_petty_cash WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord cassa_petty_cash: " & ex.Message)
        End Try
    End Function

    Public Function getTotaleImporto() As Double
        Dim sqlStr As String = "SELECT SUM(importo) totale" &
            " FROM cassa_petty_cash_riga WITH(NOLOCK)" &
            " WHERE num_petty_cash = @num_petty_cash" &
            " AND attivo = 1"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@num_petty_cash", System.Data.SqlDbType.Int, num_petty_cash)

                Dbc.Open()
                If Cmd.ExecuteScalar() Is DBNull.Value Then
                    Return 0
                Else
                    Return Cmd.ExecuteScalar()
                End If

            End Using
        End Using
    End Function

    Public Function getDateMinEMax() As DueDate
        Dim mio_DueDate As DueDate = Nothing
        Dim sqlStr As String = "SELECT MIN(Data) MinData, MAX(Data) MaxData" &
            " FROM cassa_petty_cash_riga WITH(NOLOCK)" &
            " WHERE num_petty_cash = @num_petty_cash" &
            " AND attivo = 1"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@num_petty_cash", System.Data.SqlDbType.Int, num_petty_cash)

                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    If Rs.Read Then
                        mio_DueDate = New DueDate
                        With mio_DueDate
                            .MinData = Rs("MinData")
                            .MaxData = Rs("MaxData")
                        End With
                    End If
                End Using
            End Using
        End Using

        Return mio_DueDate
    End Function

End Class

Public Class cassa_petty_cash_riga
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_id_stazione As Integer?
    Protected m_num_petty_cash As Integer?
    Protected m_id_originale As Integer?
    Protected m_attivo As Boolean?
    Protected m_data As DateTime?
    Protected m_importo As Double?
    Protected m_pagato_a As String
    Protected m_nota As String
    Protected m_data_creazione As DateTime?
    Protected m_id_utente As Integer?
    Protected m_data_chiusura As DateTime?
    Protected m_id_utente_chiusura As Integer?

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property id_stazione() As Integer?
        Get
            Return m_id_stazione
        End Get
        Set(ByVal value As Integer?)
            m_id_stazione = value
        End Set
    End Property
    Public Property num_petty_cash() As Integer?
        Get
            Return m_num_petty_cash
        End Get
        Set(ByVal value As Integer?)
            m_num_petty_cash = value
        End Set
    End Property
    Public Property id_originale() As Integer?
        Get
            Return m_id_originale
        End Get
        Set(ByVal value As Integer?)
            m_id_originale = value
        End Set
    End Property
    Public Property attivo() As Boolean?
        Get
            Return m_attivo
        End Get
        Set(ByVal value As Boolean?)
            m_attivo = value
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
    Public Property importo() As Double?
        Get
            Return m_importo
        End Get
        Set(ByVal value As Double?)
            m_importo = value
        End Set
    End Property
    Public Property pagato_a() As String
        Get
            Return m_pagato_a
        End Get
        Set(ByVal value As String)
            m_pagato_a = value
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

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO cassa_petty_cash_riga (id_stazione,num_petty_cash,id_originale,attivo,data,importo,pagato_a,nota,data_creazione,id_utente,data_chiusura,id_utente_chiusura)" &
            " VALUES (@id_stazione,@num_petty_cash,@id_originale,@attivo,@data,@importo,@pagato_a,@nota,@data_creazione,@id_utente,@data_chiusura,@id_utente_chiusura)"

        m_data_creazione = Now
        m_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_stazione", System.Data.SqlDbType.Int, id_stazione)
                addParametro(Cmd, "@num_petty_cash", System.Data.SqlDbType.Int, num_petty_cash)
                addParametro(Cmd, "@id_originale", System.Data.SqlDbType.Int, id_originale)
                addParametro(Cmd, "@attivo", System.Data.SqlDbType.Bit, attivo)
                addParametro(Cmd, "@data", System.Data.SqlDbType.Date, data)
                addParametro(Cmd, "@importo", System.Data.SqlDbType.Real, importo)
                addParametro(Cmd, "@pagato_a", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(pagato_a, 50))
                addParametro(Cmd, "@nota", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nota))
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)
                addParametro(Cmd, "@data_chiusura", System.Data.SqlDbType.DateTime, data_chiusura)
                addParametro(Cmd, "@id_utente_chiusura", System.Data.SqlDbType.Int, id_utente_chiusura)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM cassa_petty_cash_riga"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As cassa_petty_cash_riga
        Dim mio_record As cassa_petty_cash_riga = New cassa_petty_cash_riga
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .id_stazione = getValueOrNohing(Rs("id_stazione"))
            .num_petty_cash = getValueOrNohing(Rs("num_petty_cash"))
            .m_id_originale = getValueOrNohing(Rs("id_originale"))
            .attivo = getValueOrNohing(Rs("attivo"))
            .data = getValueOrNohing(Rs("data"))
            .importo = getDoubleOrNohing(Rs("importo"))
            .pagato_a = getValueOrNohing(Rs("pagato_a"))
            .nota = getValueOrNohing(Rs("nota"))
            .m_data_creazione = getValueOrNohing(Rs("data_creazione"))
            .m_id_utente = getValueOrNohing(Rs("id_utente"))
            .m_data_chiusura = getValueOrNohing(Rs("data_chiusura"))
            .m_id_utente_chiusura = getValueOrNohing(Rs("id_utente_chiusura"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As cassa_petty_cash_riga
        Dim mio_record As cassa_petty_cash_riga = Nothing

        Dim sqlStr As String = "SELECT * FROM cassa_petty_cash_riga WITH(NOLOCK) WHERE id = " & id_record

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
        ' l'aggiornamento coincide con il salvataggio di una nuova riga
        ' il riferimento alla riga modificata è mantenuta mediante l'id_originale!
        If id <= 0 Then
            Err.Raise(1001, Me, "Errore in aggiornamento l'id è gia noto")
        End If

        Dim sqlStr As String

        m_data_chiusura = Now
        m_id_utente_chiusura = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        If id_originale Is Nothing OrElse id_originale <= 0 Then
            id_originale = id
            sqlStr = "UPDATE cassa_petty_cash_riga SET" &
                    " attivo = 0," &
                    " data_chiusura = @data_chiusura," &
                    " id_utente_chiusura = @id_utente_chiusura," &
                    " id_originale = @id_originale" &
                    " WHERE id = @id_originale"
        Else
            sqlStr = "UPDATE cassa_petty_cash_riga SET" &
                    " attivo = 0," &
                    " data_chiusura = @data_chiusura," &
                    " id_utente_chiusura = @id_utente_chiusura" &
                    " WHERE id_originale = @id_originale"
        End If

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_originale", System.Data.SqlDbType.Int, id_originale)
                addParametro(Cmd, "@data_chiusura", System.Data.SqlDbType.DateTime, data_chiusura)
                addParametro(Cmd, "@id_utente_chiusura", System.Data.SqlDbType.Int, id_utente_chiusura)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using

        If SalvaRecord() > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function EliminaRecord() As Boolean
        Return EliminaRecord(Me.id)
    End Function

    Public Shared Function EliminaRecord(ByVal id_record As Integer) As Boolean
        EliminaRecord = False

        Dim sqlStr As String = "DELETE FROM cassa_petty_cash_riga WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord cassa_petty_cash_riga: " & ex.Message)
        End Try
    End Function
End Class

Public Class cassa_sospeso_riga
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_id_stazione As Integer?
    Protected m_num_petty_cash As Integer?
    Protected m_id_originale As Integer?
    Protected m_attivo As Boolean?
    Protected m_versione As Integer?
    Protected m_aperto As Boolean? = True
    Protected m_data As DateTime?
    Protected m_importo As Double?
    Protected m_pagato_a As String
    Protected m_nota As String
    Protected m_data_creazione As DateTime?
    Protected m_id_utente As Integer?
    Protected m_data_chiusura As DateTime?
    Protected m_id_utente_chiusura As Integer?

    Protected m_filtro_data_da As Date?
    Protected m_filtro_data_a As Date?
    Protected m_filtro_stato As Boolean?

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property id_stazione() As Integer?
        Get
            Return m_id_stazione
        End Get
        Set(ByVal value As Integer?)
            m_id_stazione = value
        End Set
    End Property
    Public ReadOnly Property id_originale() As Integer?
        Get
            Return m_id_originale
        End Get
    End Property
    Public Property attivo() As Boolean?
        Get
            Return m_attivo
        End Get
        Set(ByVal value As Boolean?)
            m_attivo = value
        End Set
    End Property
    Public ReadOnly Property versione() As Integer?
        Get
            Return m_versione
        End Get
    End Property
    Public ReadOnly Property aperto() As Boolean?
        Get
            Return m_aperto
        End Get
    End Property
    Public Property data() As DateTime?
        Get
            Return m_data
        End Get
        Set(ByVal value As DateTime?)
            m_data = value
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
    Public Property pagato_a() As String
        Get
            Return m_pagato_a
        End Get
        Set(ByVal value As String)
            m_pagato_a = value
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

    ' elementi per filtro!!!
    Public Property filtro_data_da() As Date?
        Get
            Return m_filtro_data_da
        End Get
        Set(ByVal value As Date?)
            m_filtro_data_da = value
        End Set
    End Property
    Public Property filtro_data_a() As Date?
        Get
            Return m_filtro_data_a
        End Get
        Set(ByVal value As Date?)
            m_filtro_data_a = value
        End Set
    End Property
    Public Property filtro_stato() As Boolean?
        Get
            Return m_filtro_stato
        End Get
        Set(ByVal value As Boolean?)
            m_filtro_stato = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            If id_originale Is Nothing Then
                m_id_originale = Contatori.getContatore_Sospeso(id_stazione)
            Else
                sqlStr = "UPDATE cassa_sospeso_riga SET" &
                    " attivo = 0" &
                    " WHERE id_originale = @id_originale"
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@id_originale", System.Data.SqlDbType.Int, id_originale)
                    Cmd.ExecuteNonQuery()
                End Using
            End If

            If m_versione Is Nothing Then
                m_versione = 1
            Else
                m_versione += 1
            End If
            m_aperto = True
            m_data_creazione = Now
            m_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

            sqlStr = "INSERT INTO cassa_sospeso_riga (id_stazione,id_originale,attivo,versione,aperto,data,importo,pagato_a,nota,data_creazione,id_utente,data_chiusura,id_utente_chiusura)" &
                " VALUES (@id_stazione,@id_originale,@attivo,@versione,@aperto,@data,@importo,@pagato_a,@nota,@data_creazione,@id_utente,@data_chiusura,@id_utente_chiusura)"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_stazione", System.Data.SqlDbType.Int, id_stazione)
                addParametro(Cmd, "@id_originale", System.Data.SqlDbType.Int, id_originale)
                addParametro(Cmd, "@attivo", System.Data.SqlDbType.Bit, attivo)
                addParametro(Cmd, "@versione", System.Data.SqlDbType.Int, versione)
                addParametro(Cmd, "@aperto", System.Data.SqlDbType.Bit, aperto)
                addParametro(Cmd, "@data", System.Data.SqlDbType.Date, data)
                addParametro(Cmd, "@importo", System.Data.SqlDbType.Real, importo)
                addParametro(Cmd, "@pagato_a", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(pagato_a, 50))
                addParametro(Cmd, "@nota", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(nota))
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)
                addParametro(Cmd, "@data_chiusura", System.Data.SqlDbType.DateTime, data_chiusura)
                addParametro(Cmd, "@id_utente_chiusura", System.Data.SqlDbType.Int, id_utente_chiusura)

                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM cassa_sospeso_riga"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As cassa_sospeso_riga
        Dim mio_record As cassa_sospeso_riga = New cassa_sospeso_riga
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .id_stazione = getValueOrNohing(Rs("id_stazione"))
            .m_id_originale = getValueOrNohing(Rs("id_originale"))
            .attivo = getValueOrNohing(Rs("attivo"))
            .m_versione = getValueOrNohing(Rs("versione"))
            .m_aperto = getValueOrNohing(Rs("aperto"))
            .data = getValueOrNohing(Rs("data"))
            .importo = getDoubleOrNohing(Rs("importo"))
            .pagato_a = getValueOrNohing(Rs("pagato_a"))
            .nota = getValueOrNohing(Rs("nota"))
            .m_data_creazione = getValueOrNohing(Rs("data_creazione"))
            .m_id_utente = getValueOrNohing(Rs("id_utente"))
            .m_data_chiusura = getValueOrNohing(Rs("data_chiusura"))
            .m_id_utente_chiusura = getValueOrNohing(Rs("id_utente_chiusura"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As cassa_sospeso_riga
        Dim mio_record As cassa_sospeso_riga = Nothing

        Dim sqlStr As String = "SELECT * FROM cassa_sospeso_riga WITH(NOLOCK) WHERE id = " & id_record

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

    Public Shared Function getRecordDaIdOriginale(ByVal id_originale As Integer) As cassa_sospeso_riga
        Dim mio_record As cassa_sospeso_riga = Nothing

        Dim sqlStr As String = "SELECT * FROM cassa_sospeso_riga WITH(NOLOCK)" &
            " WHERE id_originale = " & id_originale &
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
        If SalvaRecord() > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ChiudiRecord() As Boolean
        ChiudiRecord = False

        If id <= 0 Then
            Err.Raise(1001, Me, "Errore non può essere chiuso un record ancora non salvato")
        End If

        Dim sqlStr As String

        m_aperto = False
        m_data_chiusura = Now
        m_id_utente_chiusura = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        sqlStr = "UPDATE cassa_sospeso_riga SET" &
            " aperto = 0," &
            " data_chiusura = @data_chiusura," &
            " id_utente_chiusura = @id_utente_chiusura" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@data_chiusura", System.Data.SqlDbType.DateTime, data_chiusura)
                addParametro(Cmd, "@id_utente_chiusura", System.Data.SqlDbType.Int, id_utente_chiusura)

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)
                Dbc.Open()
                Cmd.ExecuteNonQuery()
                ChiudiRecord = True
            End Using
        End Using

    End Function

    Public Function EliminaRecord() As Boolean
        Return EliminaRecord(Me.id)
    End Function

    Public Shared Function EliminaRecord(ByVal id_record As Integer) As Boolean
        EliminaRecord = False

        Dim sqlStr As String = "DELETE FROM cassa_sospeso_riga WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord cassa_sospeso_riga: " & ex.Message)
        End Try
    End Function

    Public Function getQueryPerCassa() As String
        Dim sqlStr As String
        sqlStr = getSelect()

        sqlStr += getFiltroQueryPerCassa()

        sqlStr += getOrderByQuery()

        Return sqlStr
    End Function

    Public Function getQuery() As String
        Dim sqlStr As String
        sqlStr = getSelect()

        sqlStr += getFiltroQuery()

        sqlStr += getOrderByQuery()

        Return sqlStr
    End Function

    Protected Shared Function getSelect() As String
        Dim sqlStr As String = "SELECT spc.*, " &
            " o.cognome + ' ' + o.nome operatore," &
            " o2.cognome + ' ' + o2.nome operatore_chiusura" &
            " FROM [cassa_sospeso_riga] spc WITH(NOLOCK)" &
            " LEFT JOIN operatori o WITH(NOLOCK) ON spc.id_utente = o.id" &
            " LEFT JOIN operatori o2 WITH(NOLOCK) ON spc.id_utente_chiusura = o2.id" &
            " WHERE spc.attivo = 1"

        Return sqlStr
    End Function

    Protected Function getFiltroQueryPerCassa() As String
        Dim sqlStr As String = ""

        If id_stazione IsNot Nothing Then
            sqlStr += " AND spc.id_stazione = " & id_stazione
        End If

        If filtro_data_a IsNot Nothing Then
            'Dim DataAElab As DateTime = DateAdd(DateInterval.Day, 1, CType(filtro_data_a, DateTime))
            Dim dtsql As String = funzioni_comuni.GetDataSql(filtro_data_a, 0)
            sqlStr += " AND spc.data < CONVERT(DATETIME,'" & dtsql & "',102)"
            sqlStr += " AND (spc.aperto = 1 OR spc.data_chiusura >= CONVERT(DATETIME,'" & dtsql & "',102)"
        Else
            sqlStr += " AND spc.aperto = 1"
        End If

        Return sqlStr
    End Function

    Protected Function getFiltroQuery() As String
        Dim sqlStr As String = ""
        Dim dtsql As String
        If id_stazione IsNot Nothing Then
            sqlStr += " AND spc.id_stazione = " & id_stazione
        End If

        If filtro_data_da IsNot Nothing Then
            dtsql = funzioni_comuni.GetDataSql(filtro_data_da, 0)
            sqlStr += " AND spc.data >= CONVERT(DATETIME,'" & dtsql & "',102)"
        End If

        If filtro_data_a IsNot Nothing Then
            'Dim dataElab As DateTime = DateAdd(DateInterval.Day, 1, New Date(Year(filtro_data_a), Month(filtro_data_a), Day(filtro_data_a), 0, 0, 0))
            dtsql = funzioni_comuni.GetDataSql(filtro_data_a, 59)
            sqlStr += " AND spc.data < CONVERT(DATETIME,'" & dtsql & "',102)"
        End If

        If filtro_stato IsNot Nothing Then
            If filtro_stato Then
                sqlStr += " AND spc.aperto = 1"
            Else
                sqlStr += " AND spc.aperto = 0"
            End If
        End If

        Return sqlStr
    End Function

    Protected Function getOrderByQuery() As String
        Return " ORDER BY spc.id DESC"
    End Function
End Class

Public Class filtro_petty_cash
    Public Intestazione As String = ""
    Public id_stazione As Integer
    Public num_petty_cash As Integer
    Public DataDa As String
    Public DataA As String
    Public sospeso As Boolean
    Public AbilitaLente As Boolean
    Public AbilitaOperatore As Boolean = True
    Public CampiModificabili As Boolean

    Public Function getSql() As String
        Dim sqlStr As String = getSelect()
        sqlStr += getClausolaWhereSql()
        sqlStr += getOrdeBy()

        Return sqlStr
    End Function

    Protected Function getSelect() As String
        Dim sqlStr As String = "SELECT pcr.*, pc.data_creazione, pc.data_chiusura," &
            " op.cognome + ' ' + op.nome operatore" &
            " FROM [cassa_petty_cash_riga] pcr WITH(NOLOCK)" &
            " LEFT JOIN [cassa_petty_cash] pc WITH(NOLOCK) ON pcr.num_petty_cash = pc.num_petty_cash" &
            " LEFT JOIN operatori op WITH(NOLOCK) ON pcr.id_utente = op.id" &
            " WHERE pcr.attivo = 1"

        Return sqlStr
    End Function

    Protected Function getClausolaWhereSql() As String
        Dim sqlStr As String = ""
        Dim dtsql As String

        If id_stazione > 0 Then
            sqlStr += " AND pcr.id_stazione = " & id_stazione
        End If

        If DataDa <> "" Then
            'Dim DataDaElab As DateTime = New DateTime(Year(DataDa), Month(DataDa), Day(DataDa), 0, 0, 0)
            dtsql = funzioni_comuni.GetDataSql(DataDa, 0)
            sqlStr += " AND pcr.data >= CONVERT(DATETIME,'" & dtsql & "',102)"
        End If

        If DataA <> "" Then
            'Dim DataAElab As DateTime = DateAdd(DateInterval.Day, 1, New DateTime(Year(DataA), Month(DataA), Day(DataA), 0, 0, 0))
            dtsql = funzioni_comuni.GetDataSql(DataA, 59)
            sqlStr += " AND pcr.data < CONVERT(DATETIME,'" & dtsql & "',102)"
        End If

        'If sospeso Then
        '    sqlStr += " AND pcr.sospeso = 1"
        'Else
        '    sqlStr += " AND pcr.sospeso = 0"
        'End If

        Return sqlStr

    End Function

    Protected Function getOrdeBy() As String
        Dim sqlStr As String = " ORDER BY pcr.data"

        Return sqlStr
    End Function

    Public Function getSqlRidotta() As String
        Dim sqlStr As String = getSelectRidotta()
        sqlStr += getClausolaWhereSqlRidotta()

        Return sqlStr
    End Function

    Protected Function getSelectRidotta() As String
        Dim sqlStr As String = "SELECT pcr.*" &
            " FROM [cassa_petty_cash_riga] pcr WITH(NOLOCK)" &
            " WHERE pcr.attivo = 1"

        Return sqlStr
    End Function

    Protected Function getClausolaWhereSqlRidotta() As String
        Dim sqlStr As String = ""
        Dim dtsql As String
        If id_stazione > 0 Then
            sqlStr += " AND pcr.id_stazione = " & id_stazione
        End If

        If DataDa <> "" Then
            'Dim DataDaElab As DateTime = New DateTime(Year(DataDa), Month(DataDa), Day(DataDa), 0, 0, 0)
            dtsql = funzioni_comuni.GetDataSql(DataDa, 0)
            sqlStr += " AND pcr.data >= CONVERT(DATETIME,'" & dtsql & "',102)"
        End If

        If DataA <> "" Then
            'Dim DataAElab As DateTime = DateAdd(DateInterval.Day, 1, New DateTime(Year(DataA), Month(DataA), Day(DataA), 0, 0, 0))
            dtsql = funzioni_comuni.GetDataSql(DataA, 59)
            sqlStr += " AND pcr.data < CONVERT(DATETIME,'" & dtsql & "',102)"
        End If

        Return sqlStr

    End Function

    Protected Shared Sub addParametro(ByVal Cmd As System.Data.SqlClient.SqlCommand, ByVal NomePar As String, ByVal Tipo As System.Data.SqlDbType, ByVal Valore As Object)
        If Valore Is DBNull.Value Or Valore Is Nothing Then
            'HttpContext.Current.Trace.Write("addParametro DBNull: " & NomePar & " - " & Tipo & " - " & Valore)
            Cmd.Parameters.Add(NomePar, Tipo).Value = DBNull.Value
        Else
            'HttpContext.Current.Trace.Write("addParametro Valore: " & NomePar & " - " & Tipo & " - " & Valore)
            Cmd.Parameters.Add(NomePar, Tipo).Value = Valore
        End If
    End Sub

    Public Shared Function getTotaliPettyCashAperta(ByVal mio_filtro As filtro_pagamenti_extra) As Double()

        Dim sqlStr As String

        Try

            'Dim DataAElab As DateTime = DateAdd(DateInterval.Day, 1, New DateTime(Year(mio_filtro.DataA), Month(mio_filtro.DataA), Day(mio_filtro.DataA), 0, 0, 0))
            Dim dtsql As String = funzioni_comuni.GetDataSql(mio_filtro.DataA, 59)
            Dim mio_record As cassa_petty_cash = cassa_petty_cash.getRecordDaData(mio_filtro.id_stazione, dtsql)

            If mio_record Is Nothing Then
                Return New Double() {0, 0}
            End If

            Dim risultato(1) As Double

            sqlStr = "SELECT COUNT(*) Num, SUM(importo) Tot" &
                " FROM [cassa_petty_cash_riga] WITH(NOLOCK)" &
                " WHERE num_petty_cash = @num_petty_cash" &
                " AND data < @data" &
                " AND attivo = 1"

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@num_petty_cash", System.Data.SqlDbType.Int, mio_record.num_petty_cash)
                    addParametro(Cmd, "@data", System.Data.SqlDbType.DateTime, dtsql)

                    Dbc.Open()
                    Using Rs = Cmd.ExecuteReader
                        If Rs.Read Then
                            If Rs(0) Is DBNull.Value Then
                                risultato(0) = 0
                            Else
                                risultato(0) = Rs(0)
                            End If

                            If Rs(1) Is DBNull.Value Then
                                risultato(1) = 0
                            Else
                                risultato(1) = Rs(1)
                            End If
                        End If
                    End Using
                End Using
            End Using

            Return risultato
        Catch ex As Exception
            HttpContext.Current.Response.Write("err getTotaliPettyCashAperta : " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Public Shared Function getTotaliPettyCashSospesi(ByVal mio_filtro As filtro_pagamenti_extra) As Double()

        Dim risultato(1) As Double
        Dim sqlStr As String

        Try
            'Dim DataAElab As DateTime = DateAdd(DateInterval.Day, 1, New DateTime(Year(mio_filtro.DataA), Month(mio_filtro.DataA), Day(mio_filtro.DataA), 0, 0, 0))
            Dim dtsql As String = funzioni_comuni.GetDataSql(mio_filtro.DataA, 59)

            'Dim sqlStr As String = "SELECT COUNT(*) Num, SUM(importo) Tot" & _
            '    " FROM [cassa_sospeso_riga] pcs WITH(NOLOCK)" & _
            '    " INNER JOIN" & _
            '        " (SELECT MAX(pcs.id) id, id_originale FROM [cassa_sospeso_riga] pcs WITH(NOLOCK)" & _
            '        " WHERE pcs.id_stazione = @id_stazione" & _
            '        " AND pcs.data < @data" & _
            '        " AND pcs.data_chiusura > @data" & _
            '        " GROUP BY id_originale" & _
            '        " UNION ALL" & _
            '        " SELECT pcs.id, pcs.id_originale FROM [cassa_sospeso_riga] pcs WITH(NOLOCK)" & _
            '        " WHERE pcs.id_stazione = @id_stazione" & _
            '        " AND pcs.data < @data" & _
            '        " AND pcs.data_chiusura IS NULL) filtrati ON filtrati.id = pcs.id"

            sqlStr = "SELECT COUNT(*) Num, SUM(importo) Tot" &
            " FROM [cassa_sospeso_riga] pcs WITH(NOLOCK)" &
            " WHERE pcs.attivo = 1" &
            " AND pcs.id_stazione = @id_stazione" &
            " AND pcs.data < @data" &
            " AND (pcs.aperto = 1 OR pcs.data_chiusura >= @data)"

            HttpContext.Current.Trace.Write("getTotaliPettyCashSospesi (" & mio_filtro.id_stazione & ", " & dtsql & ") " & sqlStr)

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@id_stazione", System.Data.SqlDbType.Int, mio_filtro.id_stazione)
                    addParametro(Cmd, "@data", System.Data.SqlDbType.DateTime, dtsql)

                    Dbc.Open()
                    Using Rs = Cmd.ExecuteReader
                        If Rs.Read Then
                            If Rs(0) Is DBNull.Value Then
                                risultato(0) = 0
                            Else
                                risultato(0) = Rs(0)
                            End If

                            If Rs(1) Is DBNull.Value Then
                                risultato(1) = 0
                            Else
                                risultato(1) = Rs(1)
                            End If
                        End If
                    End Using
                End Using
            End Using

            Return risultato


        Catch ex As Exception
            HttpContext.Current.Response.Write("err getTotaliPettyCashSospesi : " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try





    End Function

    Public Function getTSqlPettyCash() As String
        Dim sqlStr As String = "SELECT * FROM [cassa_petty_cash] pc WITH(NOLOCK)" &
            " WHERE 1 = 1"
        sqlStr += getClausolaWhereSqlPettyCash()
        sqlStr += getOrdeByPettyCash()

        Return sqlStr
    End Function

    Protected Function getClausolaWhereSqlPettyCash() As String
        Dim sqlStr As String = ""
        Dim dtsql As String
        If id_stazione > 0 Then
            sqlStr += " AND pc.id_stazione = " & id_stazione
        End If

        If DataDa <> "" Then
            ' Dim DataDaElab As DateTime = New DateTime(Year(DataDa), Month(DataDa), Day(DataDa), 0, 0, 0)
            dtsql = funzioni_comuni.GetDataSql(DataDa, 0)
            sqlStr += " AND pc.data_creazione >= CONVERT(DATETIME,'" & dtsql & "',102)"
        End If

        If DataA <> "" Then
            'Dim DataAElab As DateTime = DateAdd(DateInterval.Day, 1, New DateTime(Year(DataA), Month(DataA), Day(DataA), 0, 0, 0))
            dtsql = funzioni_comuni.GetDataSql(DataA, 59)
            sqlStr += " AND pc.data_creazione < CONVERT(DATETIME,'" & dtsql & "',102)"
        End If

        Return sqlStr
    End Function

    Protected Function getOrdeByPettyCash() As String
        Dim sqlStr As String = " ORDER BY pc.data_creazione DESC"

        Return sqlStr
    End Function

End Class

Public Class filtro_pagamenti_extra
    Public Intestazione As String = ""
    Public id_stazione As Integer = 0
    Public DataDa As String = ""
    Public DataA As String = ""
    Public ID_TIPPAG As Integer = 0
    Public ID_tippag2_or As Integer = 0
    Public TIPO_PAGA As String = ""
    Public MOV_CASSA As String = ""
    Public AbilitaLente As Boolean
    Public AbilitaOperatore As Boolean = True
    Public CampiModificabili As Boolean
    Public ID_MOD_PAG As Integer = 0
    Public tipo_pagamento_generico As String = ""

    Public Function getURL() As String
        Dim url As String = "Intestazione=" & System.Web.HttpUtility.UrlEncode(Intestazione) & "&" &
            "id_stazione=" & id_stazione & "&" &
            "DataDa=" & DataDa & "&" &
            "DataA=" & DataA & "&" &
            "ID_TIPPAG=" & ID_TIPPAG & "&" &
            "ID_TIPPAG2_OR=" & ID_tippag2_or & "&" &
            "ID_MOD_PAG=" & ID_MOD_PAG & "&" &
            "TIPO_PAGA=" & TIPO_PAGA & "&" &
            "MOV_CASSA=" & MOV_CASSA & "&" &
            "AbilitaLente=" & AbilitaLente & "&" &
            "AbilitaOperatore=" & AbilitaOperatore & "&" &
            "tipoPagamentoGenerico=" & tipo_pagamento_generico

        Return url
    End Function

    Public Shared Function getRecordDaRequest(ByVal R As System.Web.HttpRequest) As filtro_pagamenti_extra
        Dim mio_record As filtro_pagamenti_extra = New filtro_pagamenti_extra
        With mio_record
            .Intestazione = R.QueryString("Intestazione")
            .id_stazione = R.QueryString("id_stazione")
            .DataDa = R.QueryString("DataDa")
            .DataA = R.QueryString("DataA")
            .ID_TIPPAG = R.QueryString("ID_TIPPAG")
            .ID_tippag2_or = R.QueryString("ID_TIPPAG2_OR")
            .TIPO_PAGA = R.QueryString("TIPO_PAGA")
            .MOV_CASSA = R.QueryString("MOV_CASSA")
            .AbilitaLente = Boolean.Parse(R.QueryString("AbilitaLente"))
            .AbilitaOperatore = Boolean.Parse(R.QueryString("AbilitaOperatore"))
        End With

        Return mio_record
    End Function

    Public Function getSql() As String
        Dim sqlStr As String = getSelect()
        sqlStr += getClausolaWhereSql()
        sqlStr += getOrdeBy()

        Return sqlStr
    End Function

    Protected Shared Function getSelect() As String
        Dim sqlStr As String = "SELECT pe.*, tp.Descrizione Des_ID_TIPPAG, mp.Descrizione Des_ID_ModPag," &
            " op.cognome + ' ' + op.nome operatore" &
            " FROM [PAGAMENTI_EXTRA] pe WITH(NOLOCK)" &
            " LEFT JOIN TIP_PAG tp WITH(NOLOCK) ON pe.ID_TIPPAG = tp.ID_TIPPag" &
            " LEFT JOIN MOD_PAG mp WITH(NOLOCK) ON pe.ID_ModPag = mp.ID_ModPag" &
            " LEFT JOIN operatori op WITH(NOLOCK) ON pe.id_operatore_ares = op.id" &
            " WHERE 1 = 1"

        Return sqlStr
    End Function

    Protected Function getClausolaWhereSql() As String
        Dim sqlStr As String = ""
        Dim dtsql As String
        If id_stazione > 0 Then
            sqlStr += " AND pe.ID_STAZIONE = " & id_stazione
        End If

        If DataDa <> "" Then
            'Dim DataDaElab As DateTime = New DateTime(Year(DataDa), Month(DataDa), Day(DataDa), 0, 0, 0)
            dtsql = funzioni_comuni.GetDataSql(DataDa, 0)
            sqlStr += " AND pe.data >= CONVERT(DATETIME,'" & dtsql & "',102)"
        End If

        If DataA <> "" Then
            ' Dim DataAElab As DateTime = DateAdd(DateInterval.Day, 1, New DateTime(Year(DataA), Month(DataA), Day(DataA), 0, 0, 0))
            dtsql = funzioni_comuni.GetDataSql(DataA, 59)
            sqlStr += " AND pe.data < CONVERT(DATETIME,'" & dtsql & "',102)"
        End If

        If TIPO_PAGA <> "" Then
            sqlStr += " AND tp.TIPO_PAGA IN (" & TIPO_PAGA & ")"
        End If

        If MOV_CASSA <> "" Then
            sqlStr += " AND tp.MOV_CASSA IN (" & MOV_CASSA & ")"
        End If


        If ID_TIPPAG <> 0 Then
            If ID_tippag2_or = 0 Then
                If ID_TIPPAG <> -1 Then
                    sqlStr += " AND pe.ID_TIPPAG = " & ID_TIPPAG
                ElseIf ID_TIPPAG = -1 Then
                    sqlStr += " AND pe.ID_TIPPAG <> " & enum_tipo_pagamento_p1000.CI_C_CREDITO_INCASSO
                End If

            Else
                sqlStr += " AND (pe.ID_TIPPAG = " & ID_TIPPAG & " OR pe.ID_TIPPAG = " & ID_tippag2_or & ") "
            End If

        End If


        If tipo_pagamento_generico = "web" Then
            sqlStr += " AND pe.UTECRE='web'"
        ElseIf tipo_pagamento_generico = "no_web" Then
            sqlStr += " AND pe.UTECRE<>'web'"
        End If



        If ID_MOD_PAG <> 0 Then
            If ID_MOD_PAG <> -1 And ID_MOD_PAG <> -2 And ID_MOD_PAG <> -3 And ID_MOD_PAG <> -4 And ID_MOD_PAG <> 910 And ID_MOD_PAG <> 91011 Then
                sqlStr += " AND mp.id_modpag = " & ID_MOD_PAG
            ElseIf ID_MOD_PAG = -1 Then
                '-1 SIGNIFICA TUTTO TRANNE BANCOMAT
                sqlStr += " AND mp.id_modpag <> 9 "
            ElseIf ID_MOD_PAG = -2 Then
                '-2 SIGNIFICA TUTTO TRANNE BONIFICO
                sqlStr += " AND mp.id_modpag <> 10 "
            ElseIf ID_MOD_PAG = -3 Then
                '-3 SIGNIFICA TUTTO TRANNE BANCOMAT E BONIFICO
                sqlStr += " AND mp.id_modpag <> 10 AND mp.id_modpag <> 9 "
            ElseIf ID_MOD_PAG = -4 Then
                '-3 SIGNIFICA TUTTO TRANNE BANCOMAT E BONIFICO E STORNO
                sqlStr += " AND mp.id_modpag <> 10 AND mp.id_modpag <> 9 AND mp.id_modpag <> 11 "
            ElseIf ID_MOD_PAG = 910 Then
                sqlStr += " AND (mp.id_modpag = 10 OR mp.id_modpag = 9) "
            ElseIf ID_MOD_PAG = 91011 Then
                sqlStr += " AND (mp.id_modpag = 10 OR mp.id_modpag = 9 OR mp.id_modpag = 11) "
            End If

        End If


        Return sqlStr

    End Function

    Public Function getSqlRidotta() As String
        Dim sqlStr As String = getSelectRidotta()
        sqlStr += getClausolaWhereSqlRidotta()

        Return sqlStr
    End Function

    Protected Shared Function getSelectRidotta() As String
        Dim sqlStr As String = "SELECT pe.*, tp.TIPO_PAGA, tp.MOV_CASSA " &
            " FROM [PAGAMENTI_EXTRA] pe WITH(NOLOCK)" &
            " LEFT JOIN TIP_PAG tp WITH(NOLOCK) ON pe.ID_TIPPAG = tp.ID_TIPPag" &
            " WHERE 1 = 1"

        Return sqlStr
    End Function

    Protected Function getClausolaWhereSqlRidotta() As String
        Dim sqlStr As String = ""
        Dim dtsql As String
        If id_stazione > 0 Then
            sqlStr += " AND pe.ID_STAZIONE = " & id_stazione
        End If

        If DataDa <> "" Then
            'Dim DataDaElab As DateTime = New DateTime(Year(DataDa), Month(DataDa), Day(DataDa), 0, 0, 0)
            dtsql = funzioni_comuni.GetDataSql(DataDa, 0)
            sqlStr += " AND pe.data >= CONVERT(DATETIME,'" & dtsql & "',102)"
        End If

        If DataA <> "" Then
            'Dim DataAElab As DateTime = DateAdd(DateInterval.Day, 1, New DateTime(Year(DataA), Month(DataA), Day(DataA), 0, 0, 0))
            dtsql = funzioni_comuni.GetDataSql(DataA, 59)
            sqlStr += " AND pe.data < CONVERT(DATETIME,'" & dtsql & "',102)"
        End If

        Return sqlStr

    End Function

    Protected Shared Function getOrdeBy() As String
        Dim sqlStr As String = " ORDER BY pe.data"

        Return sqlStr
    End Function

    Public Function getSqlDepositiSospesi() As String
        Dim sqlStr As String = getSelectDepositiSospesi()
        sqlStr += getClausolaWhereSqlDepositiSospesi()
        sqlStr += getOrdeByDepositiSospesi()

        Return sqlStr
    End Function

    Protected Shared Function getSelectDepositiSospesi() As String
        Dim sqlStr As String = "SELECT pe.*, tp.Descrizione Des_ID_TIPPAG, mp.Descrizione Des_ID_ModPag," &
            " op.cognome + ' ' + op.nome operatore" &
            " FROM [PAGAMENTI_EXTRA] pe WITH(NOLOCK)" &
            " INNER JOIN TIP_PAG tp WITH(NOLOCK) ON pe.ID_TIPPAG = tp.ID_TIPPag" &
            " LEFT JOIN MOD_PAG mp WITH(NOLOCK) ON pe.ID_ModPag = mp.ID_ModPag" &
            " LEFT JOIN operatori op WITH(NOLOCK) ON pe.id_operatore_ares = op.id" &
            " INNER JOIN contratti c WITH(NOLOCK) ON pe.N_CONTRATTO_RIF = c.num_contratto AND c.attivo = 1" &
            " WHERE pe.ID_TIPPag = " & enum_tipo_pagamento_p1000.DE_DEPOSITO_SU_RA

        Return sqlStr
    End Function

    Protected Function getClausolaWhereSqlDepositiSospesi() As String
        Dim sqlStr As String = ""

        If id_stazione > 0 Then
            sqlStr += " AND c.id_stazione_presunto_rientro = " & id_stazione
        End If

        'If DataDa <> "" Then
        '    Dim DataDaElab As DateTime = New DateTime(Year(DataDa), Month(DataDa), Day(DataDa), 0, 0, 0)
        '    sqlStr += " AND c.data_presunto_rientro >= '" & Libreria.FormattaDataOreMinSec(DataDaElab) & "'"
        'End If

        If DataA <> "" Then
            'Dim DataAElab As DateTime = DateAdd(DateInterval.Day, 1, New DateTime(Year(DataA), Month(DataA), Day(DataA), 0, 0, 0))
            Dim dtsql As String = funzioni_comuni.GetDataSql(DataA, 59)
            sqlStr += " AND pe.Data < CONVERT(DATETIME,'" & dtsql & "',102)"
            sqlStr += " AND (pe.AUTORIZZ_EVASA_IL IS NULL OR pe.AUTORIZZ_EVASA_IL >= CONVERT(DATETIME,'" & dtsql & "',102))"
        Else
            sqlStr += " AND pe.AUTORIZZ_EVASA_IL IS NULL"
        End If

        Return sqlStr

    End Function

    Protected Shared Function getOrdeByDepositiSospesi() As String
        Dim sqlStr As String = " ORDER BY c.data_presunto_rientro"
        Return sqlStr
    End Function

    Public Function getSqlTotaleDepositiSospesi() As String
        Dim sqlStr As String = getSelectTotaleDepositiSospesi()
        sqlStr += getClausolaWhereSqlDepositiSospesi()

        HttpContext.Current.Trace.Write(sqlStr)

        Return sqlStr
    End Function

    Protected Function getSelectTotaleDepositiSospesi() As String
        Dim sqlStr As String = "SELECT COUNT(*) NumSospesi, SUM(pe.PER_IMPORTO) TotSospesi" &
            " FROM [PAGAMENTI_EXTRA] pe WITH(NOLOCK)" &
            " INNER JOIN contratti c WITH(NOLOCK) ON pe.N_CONTRATTO_RIF = c.num_contratto AND c.attivo = 1" &
            " WHERE pe.ID_TIPPag = " & enum_tipo_pagamento_p1000.DE_DEPOSITO_SU_RA

        sqlStr += getClausolaWhereSqlDepositiSospesi()

        Return sqlStr
    End Function


    Public Function getTotaliDepositiSospesi() As Double()
        Dim risultato(1) As Double

        Dim sqlStr As String = getSqlTotaleDepositiSospesi()
        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Using Rs = Cmd.ExecuteReader
                        If Rs.Read Then
                            If Rs(0) Is DBNull.Value Then
                                risultato(0) = 0
                            Else
                                risultato(0) = Rs(0)
                            End If

                            If Rs(1) Is DBNull.Value Then
                                risultato(1) = 0
                            Else
                                risultato(1) = Rs(1)
                            End If
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            HttpContext.Current.Response.Write("error getTotaliDepositiSospesi " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

        Return risultato



    End Function

    'Protected Function getClausolaWhereSqlRidottaDepositiSospesi() As String

    'End Function

    Public Function SaldoIniziale() As Double
        ' ATTENZIONE: da verificare come gestire il segno delle transazioni
        ' Nella query presuppongo che gli importi siano inseriti con segno!!!!!
        ' In disaccordo con le rimanenti query in cui suppongo tutto positivo...
        ' devo chiarire questo con Marco e Tony
        ' inoltre dvo prevedere come gestire il saldo iniziale della filiale
        ' (se devo avere un movimento di inizializzazione della filiale)
        ' ad esempio un movimento per l'anno in corso...
        ' che raggruppi i totali dell'anno precedente
        ' se ad un certo punto decidiamo di svuotare il db ad ogni inizio di anno per alleggerire il db....
        ' oppure considerare la somma per intero non svuotando i db....
        ' Nicola utilizza un'altra tabella dove salva i riporti dall'anno predente....
        ' da prendere questa decisione!!!

        'ESCLUDO DAL TOTALE DEL PERIODO I DEPOSITI CASH

        Dim sqlStr As String = "SELECT SUM(PER_IMPORTO) FROM [PAGAMENTI_EXTRA] pe WITH(NOLOCK)" &
            " LEFT JOIN TIP_PAG tp WITH(NOLOCK) ON pe.ID_TIPPAG = tp.ID_TIPPag" &
            " WHERE tp.MOV_CASSA IN ('EN','US') AND ISNULL(pe.id_modpag,0)<>9 AND ISNULL(pe.id_modpag,0)<>10 AND ISNULL(pe.id_modpag,0)<>11"

        If id_stazione > 0 Then
            sqlStr += " AND pe.ID_STAZIONE = " & id_stazione
        End If

        If DataDa <> "" Then
            Dim DataDaElab As DateTime = New DateTime(Year(DataDa), Month(DataDa), Day(DataDa), 0, 0, 0)
            sqlStr += " AND pe.data < convert(datetime,'" & Libreria.FormattaDataOreMinSecProduzione(DataDaElab) & "',102)"
        End If

        HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                If Cmd.ExecuteScalar Is DBNull.Value Then
                    Return 0
                Else
                    Return Cmd.ExecuteScalar
                End If
            End Using
        End Using

    End Function

    Public Shared Function getMovCassaDaTipoPagamento(ByVal id_tipo_pagamento As Integer) As String
        Dim sqlStr As String = "SELECT note FROM [TIP_PAG] tp WITH(NOLOCK)" &
            " WHERE tp.ID_TIPPag = " & id_tipo_pagamento

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                If Cmd.ExecuteScalar Is DBNull.Value Then
                    Return ""
                Else
                    Return Cmd.ExecuteScalar
                End If
            End Using
        End Using
    End Function
End Class


Public Class calcolo_totali_cassa
    Public num_incassi As Integer = 0
    Public incassi As Double = 0
    Public num_depositi As Integer = 0
    Public num_incassi_bonifico As Integer = 0
    Public incassi_bonifico As Double = 0
    Public num_depositi_cash As Integer = 0
    Public num_depositi_bancomat As Integer = 0
    Public num_depositi_bonifico As Integer = 0
    Public depositi_bonifico As Double = 0
    Public depositi As Double = 0
    Public depositi_cash As Double = 0
    Public depositi_bancomat As Double = 0
    Public num_rimborsi As Integer = 0
    Public num_rimborsi_cash As Integer = 0
    Public num_rimborsi_bonifico As Integer = 0
    Public rimborsi As Double = 0
    Public rimborsi_cash As Double = 0
    Public rimborsi_bonifico As Double = 0
    Public num_incassi_extra As Integer = 0
    Public incassi_extra As Double = 0
    Public num_busta_petty_cash As Integer = 0
    Public busta_petty_cash As Double = 0
    Public num_carte_credito As Integer = 0
    Public carte_credito As Double = 0
    Public num_carte_credito_incasso As Integer = 0
    Public carte_credito_incasso As Double = 0
    Public num_full_credit As Integer = 0
    Public full_credit As Double = 0
    Public num_complimentary As Integer = 0
    Public complimentary As Double = 0
    Public num_prelievi As Integer = 0
    Public prelievi As Double = 0
    Public num_versamenti As Integer = 0
    Public versamenti As Double = 0
    Public num_depositi_pos As Integer = 0
    Public depositi_pos As Double = 0
    Public num_incassi_pos As Integer = 0
    Public incassi_pos As Double = 0
    Public num_incassi_web As Integer = 0
    Public incassi_web As Double = 0
    Public num_rimborsi_pos As Integer = 0
    Public rimborsi_pos As Double = 0
    Public num_rimborsi_pos_fatt As Integer = 0
    Public rimborsi_pos_fatt As Double = 0
    Public num_voucher As Integer = 0
    Public voucher As Double = 0
    Public num_abbuoni_fatt As Integer = 0
    Public abbuoni_fatt As Double = 0

    Public num_petty_sospesa As Integer = 0
    Public petty_sospesa As Double = 0
    Public num_sospesi As Integer = 0
    Public sospesi As Double = 0

    Public num_depositi_sospesi As Integer = 0
    Public depositi_sospesi As Double = 0

    Public totale_entrate_cassa As Double = 0
    Public totale_entrate As Double = 0
    Public totale_uscite As Double = 0
    Public totale_sospesi As Double = 0
    Public totale_periodo As Double = 0

    Public saldo_iniziale As Double = 0
    Public saldo_finale As Double = 0
    Public saldo_reale As Double = 0


    Protected Sub AzzeraTotali()
        num_incassi = 0
        incassi = 0
        num_incassi_bonifico = 0
        incassi_bonifico = 0
        num_depositi_cash = 0
        depositi_cash = 0
        num_depositi_bonifico = 0
        depositi_bonifico = 0
        num_depositi_cash = 0
        num_depositi_bancomat = 0
        depositi_bancomat = 0
        num_rimborsi_cash = 0
        num_rimborsi_bonifico = 0
        rimborsi_cash = 0
        rimborsi_bonifico = 0
        num_incassi_extra = 0
        incassi_extra = 0
        num_busta_petty_cash = 0
        busta_petty_cash = 0
        num_carte_credito = 0
        carte_credito = 0
        num_carte_credito_incasso = 0
        carte_credito_incasso = 0
        num_full_credit = 0
        full_credit = 0
        num_complimentary = 0
        complimentary = 0
        num_prelievi = 0
        prelievi = 0
        num_versamenti = 0
        versamenti = 0
        num_depositi_pos = 0
        depositi_pos = 0
        num_incassi_pos = 0
        incassi_pos = 0
        num_rimborsi_pos = 0
        rimborsi_pos = 0
        num_rimborsi_pos_fatt = 0
        rimborsi_pos_fatt = 0
        num_voucher = 0
        voucher = 0
        num_abbuoni_fatt = 0
        abbuoni_fatt = 0
        incassi_web = 0
        num_incassi_web = 0
    End Sub

    Public Sub aggiorna_totali(ByVal mio_filtro As filtro_pagamenti_extra)
        Dim sqlStr As String

        AzzeraTotali()

        sqlStr = mio_filtro.getSqlRidotta
        'HttpContext.Current.Trace.Write("Parametro: " & sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                'HttpContext.Current.Trace.Write("Parametro: " & Cmd.CommandText)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    Do While Rs.Read
                        Select Case Rs("ID_TIPPag")
                            Case enum_tipo_pagamento_p1000.CH_PAGAMENTO_CASH
                                If Rs("id_modpag") <> "10" Then
                                    num_incassi += 1
                                    incassi += Rs("PER_IMPORTO")
                                ElseIf Rs("id_modpag") = "10" Then
                                    num_incassi_bonifico += 1
                                    incassi_bonifico += Rs("PER_IMPORTO")
                                End If
                            Case enum_tipo_pagamento_p1000.DE_DEPOSITO_SU_RA
                                If Rs("id_modpag") <> "9" And Rs("id_modpag") <> "10" Then
                                    num_depositi_cash += 1
                                    depositi_cash += Rs("PER_IMPORTO")
                                ElseIf Rs("id_modpag") = "9" Then
                                    num_depositi_bancomat += 1
                                    depositi_bancomat += Rs("PER_IMPORTO")
                                ElseIf Rs("id_modpag") = "10" Then
                                    num_depositi_bonifico += 1
                                    depositi_bonifico += Rs("PER_IMPORTO")
                                End If
                            Case enum_tipo_pagamento_p1000.CA_RIMBORSO_SU_RA, enum_tipo_pagamento_p1000.MCR_PAGAMENTO_CASH
                                If Rs("id_modpag") <> "9" And Rs("id_modpag") <> "10" And Rs("id_modpag") <> "11" Then
                                    num_rimborsi_cash += 1
                                    rimborsi_cash += Rs("PER_IMPORTO")
                                ElseIf Rs("id_modpag") = "9" Then
                                    'BANCOMAT
                                    If Rs("ID_TIPPag") = enum_tipo_pagamento_p1000.CA_RIMBORSO_SU_RA Then
                                        num_rimborsi_pos += 1
                                        rimborsi_pos += Rs("PER_IMPORTO")
                                    ElseIf Rs("ID_TIPPag") = enum_tipo_pagamento_p1000.MCR_PAGAMENTO_CASH Then
                                        num_rimborsi_bonifico += 1
                                        rimborsi_bonifico += Rs("PER_IMPORTO")
                                    End If
                                ElseIf Rs("id_modpag") = "10" Then
                                    'BONIFICO
                                    If Rs("ID_TIPPag") = enum_tipo_pagamento_p1000.CA_RIMBORSO_SU_RA Then
                                        num_rimborsi_pos += 1
                                        rimborsi_pos += Rs("PER_IMPORTO")
                                    ElseIf Rs("ID_TIPPag") = enum_tipo_pagamento_p1000.MCR_PAGAMENTO_CASH Then
                                        num_rimborsi_bonifico += 1
                                        rimborsi_bonifico += Rs("PER_IMPORTO")
                                    End If
                                ElseIf Rs("id_modpag") = "11" Then
                                    'STORNO
                                    If Rs("ID_TIPPag") = enum_tipo_pagamento_p1000.CA_RIMBORSO_SU_RA Then
                                        num_rimborsi_pos += 1
                                        rimborsi_pos += Rs("PER_IMPORTO")
                                    ElseIf Rs("ID_TIPPag") = enum_tipo_pagamento_p1000.MCR_PAGAMENTO_CASH Then
                                        num_rimborsi_bonifico += 1
                                        rimborsi_bonifico += Rs("PER_IMPORTO")
                                    End If
                                End If
                            Case enum_tipo_pagamento_p1000.PC_PETTY_CASH
                                num_busta_petty_cash += 1
                                busta_petty_cash += Rs("PER_IMPORTO")
                            Case enum_tipo_pagamento_p1000.CC_C_CREDITO_AUTORIZZAZIONE
                                num_depositi_pos += 1    'NELLA CASSA A VIDEO LE AUTORIZZAZIONI TELEFONICHE VENGONO AGGIUNTE A TUTTE LE PREAUTORIZZAZIONI
                                depositi_pos += Rs("PER_IMPORTO")
                                num_carte_credito += 1   'SERVE PER LA STAMPA CARTACEA VISTO CHE IL DATO CC TELEFONICO E' RIPORTATO A PARTE
                                carte_credito += Rs("PER_IMPORTO")
                            Case enum_tipo_pagamento_p1000.CI_C_CREDITO_INCASSO
                                num_carte_credito_incasso += 1
                                carte_credito_incasso += Rs("PER_IMPORTO")
                            Case enum_tipo_pagamento_p1000.FC_FULL_CREDIT
                                num_full_credit += 1
                                full_credit += Rs("PER_IMPORTO")
                            Case enum_tipo_pagamento_p1000.COMPLIMENTARY
                                num_complimentary += 1
                                complimentary += Rs("PER_IMPORTO")
                            Case Else
                                Select Case Rs("MOV_CASSA")
                                    Case "EN"
                                        Select Case Rs("TIPO_PAGA")
                                            Case "PR"
                                                'num_prelievi += 1
                                                'prelievi += Rs("PER_IMPORTO")
                                            Case "IE"
                                                num_incassi_extra += 1
                                                incassi_extra += Rs("PER_IMPORTO")
                                            Case "MC"
                                                'num_incassi_extra += 1
                                                'incassi_extra += Rs("PER_IMPORTO")
                                                'num_prelievi += 1
                                                'prelievi += Rs("PER_IMPORTO")
                                            Case Else
                                                Err.Raise(1001, Nothing, "TIPO_PAGA non previsto: " & Rs("MOV_CASSA") & " - " & Rs("TIPO_PAGA"))
                                        End Select
                                    Case "US"
                                        Select Case Rs("TIPO_PAGA")
                                            Case "VS"
                                                num_versamenti += 1
                                                versamenti += Rs("PER_IMPORTO")
                                            Case "PR"
                                                num_prelievi += 1
                                                prelievi += Rs("PER_IMPORTO")
                                            Case Else
                                                Err.Raise(1001, Nothing, "TIPO_PAGA non previsto: " & Rs("MOV_CASSA") & " - " & Rs("TIPO_PAGA"))
                                        End Select
                                    Case "PO"
                                        Select Case Rs("TIPO_PAGA")
                                            Case "VS"
                                                num_versamenti += 1
                                                versamenti += Rs("PER_IMPORTO")
                                            Case "DE"
                                                num_depositi_pos += 1
                                                depositi_pos += Rs("PER_IMPORTO")
                                            Case "IN"
                                                If (Rs("utecre") & "") = "web" Then
                                                    num_incassi_web += 1
                                                    incassi_web += Rs("PER_IMPORTO")
                                                Else
                                                    num_incassi_pos += 1
                                                    incassi_pos += Rs("PER_IMPORTO")
                                                End If

                                            Case "RB"
                                                'num_rimborsi_pos += 1
                                                'rimborsi_pos += Rs("PER_IMPORTO")
                                            Case "RP"
                                                num_rimborsi_pos_fatt += 1
                                                rimborsi_pos_fatt += Rs("PER_IMPORTO")
                                            Case Else
                                                Err.Raise(1001, Nothing, "TIPO_PAGA non previsto: " & Rs("MOV_CASSA") & " - " & Rs("TIPO_PAGA"))
                                        End Select
                                    Case "FT"
                                        Select Case Rs("TIPO_PAGA")
                                            Case "VO"
                                                num_voucher += 1
                                                voucher += Rs("PER_IMPORTO")
                                            Case "AB"
                                                num_abbuoni_fatt += 1
                                                abbuoni_fatt += Rs("PER_IMPORTO")
                                            Case Else
                                                Err.Raise(1001, Nothing, "TIPO_PAGA non previsto: " & Rs("MOV_CASSA") & " - " & Rs("TIPO_PAGA"))
                                        End Select
                                    Case Else
                                        Err.Raise(1001, Nothing, "MOV_CASSA non previsto: " & Rs("MOV_CASSA") & " - " & Rs("TIPO_PAGA"))
                                End Select
                        End Select
                    Loop
                End Using
            End Using
        End Using

        Dim risultato() As Double

        risultato = filtro_petty_cash.getTotaliPettyCashAperta(mio_filtro)
        num_petty_sospesa = risultato(0)
        petty_sospesa = risultato(1)

        risultato = filtro_petty_cash.getTotaliPettyCashSospesi(mio_filtro)
        num_sospesi = risultato(0)
        sospesi = risultato(1)

        risultato = mio_filtro.getTotaliDepositiSospesi
        num_depositi_sospesi = risultato(0)
        depositi_sospesi = risultato(1)


        totale_entrate_cassa = (incassi) _
                        + (prelievi) _
                        + (incassi_extra) _
                        + (versamenti)

        'totale_entrate = (incassi) _
        '                + (depositi) _
        '                + (prelievi) _
        '                + (incassi_extra) _
        '                + (depositi_sospesi)

        totale_entrate = (incassi) _
                        + (incassi_pos) _
                        + (incassi_web) _
                        + (prelievi) _
                        + (incassi_extra)


        Dim incassi_report_operatore As Double = incassi + incassi_pos + incassi_bonifico + incassi_web

        incassi_report_operatore = incassi_report_operatore



        totale_uscite = ((depositi_cash) _
                        + (rimborsi_cash)) * -1



        '+ (versamenti) _
        '+ (busta_petty_cash)

        'totale_sospesi = (petty_sospesa) _
        '                + (sospesi)

        totale_sospesi = totale_entrate + totale_uscite

        saldo_iniziale = mio_filtro.SaldoIniziale

        'totale_periodo = (incassi) _
        '                + (depositi_cash) _
        '                + (prelievi) _
        '                + (incassi_extra) _
        '                + (rimborsi_cash) _
        '                + (versamenti) _
        '                + (busta_petty_cash)

        totale_periodo = (incassi) _
                        + (depositi_cash) _
                        + (prelievi) _
                        + (incassi_extra) _
                        + (rimborsi_cash) _
                        + (versamenti)

        saldo_finale = (saldo_iniziale) _
                        + (totale_periodo)

        saldo_reale = (saldo_finale) _
                    - (petty_sospesa) _
                    - (sospesi)

    End Sub
End Class


Public Class POST_enti
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_attivo As Boolean?
    Protected m_descrizione As String
    Protected m_telefono As String
    Protected m_durata_autorizzazione As Integer?
    Protected m_stampa As Boolean?
    Protected m_id_mod_pag As Long?

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property attivo() As Boolean?
        Get
            Return m_attivo
        End Get
        Set(ByVal value As Boolean?)
            m_attivo = value
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
    Public Property telefono() As String
        Get
            Return m_telefono
        End Get
        Set(ByVal value As String)
            m_telefono = value
        End Set
    End Property
    Public Property durata_autorizzazione() As Integer?
        Get
            Return m_durata_autorizzazione
        End Get
        Set(ByVal value As Integer?)
            m_durata_autorizzazione = value
        End Set
    End Property
    Public Property stampa() As Boolean?
        Get
            Return m_stampa
        End Get
        Set(ByVal value As Boolean?)
            m_stampa = value
        End Set
    End Property
    Public Property id_mod_pag() As Long?
        Get
            Return m_id_mod_pag
        End Get
        Set(ByVal value As Long?)
            m_id_mod_pag = value
        End Set
    End Property

    Protected Function SalvaModalitaPagamento() As Integer

        Dim idmodpag As Integer = -1
        Dim sqlStr As String = "select max(id_modpag) as maxid from Autonoleggio_SRC.dbo.MOD_PAG "

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    idmodpag = Cmd.ExecuteScalar()
                    idmodpag += 1
                    Dbc.Close()
                    Cmd.Dispose()
                End Using
            End Using

        Catch ex As Exception
            HttpContext.Current.Response.Write("Err SalvaModalitaPagamento 1 : " & sqlStr & " - " & Me.toString)
        End Try


        sqlStr = "INSERT INTO MOD_PAG (Descrizione,Tipo,note,DATAMOD,UTEMOD, id_modpag)" &
            " VALUES (@Descrizione,'C/Credito','CC',@DATAMOD,@UTEMOD, '" & idmodpag & "')"

        Dim DATAMOD As DateTime = Now
        Dim UTEMOD As String = Libreria.getNomeOperatoreDaId(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        HttpContext.Current.Trace.Write("SalvaModalitaPagamento: " & sqlStr & " - " & Me.toString)

        Try

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)


                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 25))
                    addParametro(Cmd, "@DATAMOD", System.Data.SqlDbType.DateTime, DATAMOD)
                    addParametro(Cmd, "@UTEMOD", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(UTEMOD, 10))

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                End Using

                ' -------------------------------------------------------------------------
                'recupero l'id del nuopvo record comunque...
                'sqlStr = "SELECT @@IDENTITY FROM MOD_PAG"

                'Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                '    Dim valore = Cmd.ExecuteScalar
                '    If valore Is DBNull.Value Then
                '        id_mod_pag = Nothing
                '    Else
                '        id_mod_pag = Long.Parse(valore)
                '    End If
                'End Using

            End Using

            Return idmodpag


        Catch ex As Exception
            HttpContext.Current.Response.Write("Err SalvaModalitaPagamento 2: " & sqlStr & " - " & Me.toString)
        End Try


    End Function

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer

        id_mod_pag = SalvaModalitaPagamento()   'Valore dell ID modpag 

        'recupera nuovo id (dell'ente) in tabella POST_Enti
        Dim sqlStr As String = "select max([id]) as maxid from Autonoleggio_SRC.dbo.POST_enti "

        sqlStr = "INSERT INTO POST_enti (attivo,descrizione,telefono,durata_autorizzazione,stampa,id_mod_pag)" &
                " VALUES (@attivo,@descrizione,@telefono,@durata_autorizzazione,@stampa,@id_mod_pag)"
        Try


            'm_data_creazione = Now
            'm_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@attivo", System.Data.SqlDbType.Bit, attivo)
                    addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
                    addParametro(Cmd, "@telefono", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(telefono, 50))
                    addParametro(Cmd, "@durata_autorizzazione", System.Data.SqlDbType.Int, durata_autorizzazione)
                    addParametro(Cmd, "@stampa", System.Data.SqlDbType.Bit, stampa)
                    addParametro(Cmd, "@id_mod_pag", System.Data.SqlDbType.Int, id_mod_pag)

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()


                End Using

                ' -------------------------------------------------------------------------
                'recupero l'id del nuopvo record comunque...
                'sqlStr = "SELECT @@IDENTITY FROM POST_enti"

                'Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                '    m_id = Cmd.ExecuteScalar
                'End Using
            End Using

            Return id_mod_pag

        Catch ex As Exception
            HttpContext.Current.Response.Write("Err SalvaRecord 2 : " & sqlStr & " - " & Me.toString)
        End Try

    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As POST_enti
        Dim mio_record As POST_enti = New POST_enti
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .attivo = getValueOrNohing(Rs("attivo"))
            .descrizione = getValueOrNohing(Rs("descrizione"))
            .telefono = getValueOrNohing(Rs("telefono"))
            .durata_autorizzazione = getValueOrNohing(Rs("durata_autorizzazione"))
            .stampa = getValueOrNohing(Rs("stampa"))
            If Rs("id_mod_pag") Is DBNull.Value Then
                .id_mod_pag = Nothing
            Else
                .id_mod_pag = Long.Parse(Rs("id_mod_pag"))
            End If

        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As POST_enti
        Dim mio_record As POST_enti = Nothing

        Dim sqlStr As String = "SELECT * FROM POST_enti WITH(NOLOCK) WHERE id = " & id_record

        HttpContext.Current.Trace.Write("getRecordDaId: " & sqlStr)

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

        If mio_record IsNot Nothing Then
            HttpContext.Current.Trace.Write("getRecordDaId: " & mio_record.toString)
        Else
            HttpContext.Current.Trace.Write("getRecordDaId: NOTHING!!!!!")
        End If


        Return mio_record
    End Function

    Public Shared Function getRecordDaIdModPag(ByVal id_mod_pag As Integer) As POST_enti
        Dim mio_record As POST_enti = Nothing

        Dim sqlStr As String = "SELECT * FROM POST_enti WITH(NOLOCK) WHERE id_mod_pag = " & id_mod_pag

        HttpContext.Current.Trace.Write("getRecordDaId: " & sqlStr)

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

        If mio_record IsNot Nothing Then
            HttpContext.Current.Trace.Write("getRecordDaId: " & mio_record.toString)
        Else
            HttpContext.Current.Trace.Write("getRecordDaId: NOTHING!!!!!")
        End If


        Return mio_record
    End Function

    Private Function AggiornaModalitaPagamento() As Boolean
        AggiornaModalitaPagamento = False

        Dim sqlStr As String = "UPDATE MOD_PAG SET" &
            " Descrizione = @Descrizione," &
            " DATAMOD = @DATAMOD," &
            " UTEMOD = @UTEMOD" &
            " WHERE ID_ModPag = @id_mod_pag"

        HttpContext.Current.Trace.Write("AggiornaModalitaPagamento: " & sqlStr & " - " & Me.toString)

        Dim DATAMOD As DateTime = Now
        Dim UTEMOD As String = Libreria.getNomeOperatoreDaId(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 25))
                addParametro(Cmd, "@DATAMOD", System.Data.SqlDbType.DateTime, DATAMOD)
                addParametro(Cmd, "@UTEMOD", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(UTEMOD, 10))
                addParametro(Cmd, "@id_mod_pag", System.Data.SqlDbType.Int, id_mod_pag)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaModalitaPagamento = True
            End Using
        End Using

    End Function

    Public Function AggiornaRecord() As Boolean
        AggiornaRecord = False

        AggiornaModalitaPagamento()

        Dim sqlStr As String = "UPDATE POST_enti SET" &
            " attivo = @attivo," &
            " descrizione = @descrizione," &
            " telefono = @telefono," &
            " durata_autorizzazione = @durata_autorizzazione," &
            " stampa = @stampa," &
            " id_mod_pag = @id_mod_pag" &
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@attivo", System.Data.SqlDbType.Bit, attivo)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@telefono", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(telefono, 50))
                addParametro(Cmd, "@durata_autorizzazione", System.Data.SqlDbType.Int, durata_autorizzazione)
                addParametro(Cmd, "@stampa", System.Data.SqlDbType.Bit, stampa)
                addParametro(Cmd, "@id_mod_pag", System.Data.SqlDbType.Int, id_mod_pag)

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

        Dim sqlStr As String = "DELETE FROM POST_enti WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord POST_enti: " & ex.Message)
        End Try
    End Function

    Public Overrides Function toString() As String
        Dim separatore As String = "<br />"

        Dim Valore As String = "TABELLA POST_enti" & separatore &
            "m_id =  " & m_id & separatore &
            "m_attivo =  " & m_attivo & separatore &
            "m_descrizione =  " & m_descrizione & separatore &
            "m_telefono =  " & m_telefono & separatore &
            "m_durata_autorizzazione =  " & m_durata_autorizzazione & separatore &
            "m_stampa =  " & m_stampa & separatore &
            "id_mod_pag = " & m_id_mod_pag & separatore

        Return Valore
    End Function

    Public Function getSelect() As String
        Dim sqlStr As String = "SELECT * FROM POST_enti  WITH(NOLOCK)" &
            " WHERE 1 = 1"

        sqlStr += getWhere() + getOrderBy()

        Return sqlStr
    End Function

    Protected Function getWhere() As String
        Dim sqlStr As String = ""

        If descrizione & "" = "" Then
            sqlStr += " AND descrizione LIKE '" & Libreria.formattaSqlTrim(descrizione) & "%'"
        End If

        If attivo IsNot Nothing Then
            If attivo Then
                sqlStr += " AND attivo = 1"
            Else
                sqlStr += " AND attivo = 0"
            End If
        End If

        Return sqlStr
    End Function

    Protected Function getOrderBy() As String
        Dim sqlStr As String = " ORDER BY descrizione"

        Return sqlStr
    End Function

End Class


Public Class POST_codici_esercenti
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_id_stazione As Integer?
    Protected m_id_POST_enti As Integer?
    Protected m_codice_esercente As String

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property id_stazione() As Integer?
        Get
            Return m_id_stazione
        End Get
        Set(ByVal value As Integer?)
            m_id_stazione = value
        End Set
    End Property
    Public Property id_POST_enti() As Integer?
        Get
            Return m_id_POST_enti
        End Get
        Set(ByVal value As Integer?)
            m_id_POST_enti = value
        End Set
    End Property
    Public Property codice_esercente() As String
        Get
            Return m_codice_esercente
        End Get
        Set(ByVal value As String)
            m_codice_esercente = value
        End Set
    End Property


    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO POST_codici_esercenti (id_stazione,id_POST_enti,codice_esercente)" &
            " VALUES (id_stazione,@id_POST_enti,@codice_esercente)"

        'm_data_creazione = Now
        'm_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_stazione", System.Data.SqlDbType.Int, id_stazione)
                addParametro(Cmd, "@id_POST_enti", System.Data.SqlDbType.Int, id_POST_enti)
                addParametro(Cmd, "@codice_esercente", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(codice_esercente, 20))

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM POST_codici_esercenti"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As POST_codici_esercenti
        Dim mio_record As POST_codici_esercenti = New POST_codici_esercenti
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .id_stazione = getValueOrNohing(Rs("id_stazione"))
            .id_POST_enti = getValueOrNohing(Rs("id_POST_enti"))
            .codice_esercente = getValueOrNohing(Rs("codice_esercente"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As POST_codici_esercenti
        Dim mio_record As POST_codici_esercenti = Nothing

        Dim sqlStr As String = "SELECT * FROM POST_codici_esercenti WITH(NOLOCK) WHERE id = " & id_record

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

    Public Shared Function getRecord(ByVal id_stazione As Integer, ByVal id_POST_enti As Integer) As POST_codici_esercenti
        Dim mio_record As POST_codici_esercenti = Nothing

        Dim sqlStr As String = "SELECT * FROM POST_codici_esercenti WITH(NOLOCK)" & _
            " WHERE id_stazione = @id_stazione" & _
            " AND id_POST_enti = @id_POST_enti"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_stazione", System.Data.SqlDbType.Int, id_stazione)
                addParametro(Cmd, "@id_POST_enti", System.Data.SqlDbType.Int, id_POST_enti)

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

        Dim sqlStr As String = "UPDATE POST_codici_esercenti SET" & _
            " id_stazione = @id_stazione," & _
            " id_POST_enti = @id_POST_enti," & _
            " codice_esercente = @codice_esercente" & _
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_stazione", System.Data.SqlDbType.Int, id_stazione)
                addParametro(Cmd, "@id_POST_enti", System.Data.SqlDbType.Int, id_POST_enti)
                addParametro(Cmd, "@codice_esercente", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(codice_esercente, 20))

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

        Dim sqlStr As String = "DELETE FROM POST_codici_esercenti WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord POST_codici_esercenti: " & ex.Message)
        End Try
    End Function

    Public Overrides Function toString() As String
        Dim separatore As String = "<br />"

        Dim Valore As String = "TABELLA POST_codici_esercenti" & separatore & _
            "m_id =  " & m_id & separatore & _
            "m_id_stazione =  " & m_id_stazione & separatore & _
            "m_id_POST_enti =  " & m_id_POST_enti & separatore & _
            "m_codice_esercente =  " & m_codice_esercente & separatore

        Return Valore
    End Function

    Public Function getSelect() As String
        Dim sqlStr As String = "SELECT * FROM POST_codici_esercenti WITH(NOLOCK)" & _
            " WHERE 1 = 1"

        sqlStr += getWhere() + getOrderBy()

        Return sqlStr
    End Function

    Protected Function getWhere() As String
        Dim sqlStr As String = ""

        If id_stazione IsNot Nothing AndAlso id_stazione > 0 Then
            sqlStr += " AND id_stazione = " & id_stazione
        End If

        If id_POST_enti IsNot Nothing AndAlso id_POST_enti > 0 Then
            sqlStr += " AND id_POST_enti = " & id_POST_enti
        End If

        If (codice_esercente & "") <> "" Then
            sqlStr += " AND codice_esercente LIKE '" & Libreria.formattaSqlTrim(codice_esercente) & "%'"
        End If

        Return sqlStr
    End Function

    Protected Function getOrderBy() As String
        Dim sqlStr As String = " ORDER BY id_stazione, id_POST_enti"

        Return sqlStr
    End Function

    'Public Shared Function getCrossSelect(Optional ByVal id_stazione As Integer = 0, Optional ByVal id_ente As Integer = 0) As String
    '    Dim sqlStr As String

    '    sqlStr = " select s.id id_stazione, s.codice + ' ' + s.nome_stazione stazione, e.id id_ente, e.descrizione ente," & _
    '        " c_e.id id_esercente, c_e.codice_esercente" & _
    '        " from stazioni s with(nolock)" & _
    '        " cross join dbo.POST_enti e with(nolock)" & _
    '        " left join dbo.POST_codici_esercenti c_e with(nolock) ON s.id = c_e.id_stazione AND e.id = c_e.id_POST_enti" & _
    '        " where(s.attiva = 1 And e.attivo = 1)"

    '    If id_stazione > 0 Then
    '        sqlStr += " AND s.id = " & id_stazione
    '    End If
    '    If id_ente > 0 Then
    '        sqlStr += " AND e.id = " & id_ente
    '    End If

    '    sqlStr += " ORDER BY s.codice, e.descrizione"

    '    Return sqlStr
    'End Function

End Class