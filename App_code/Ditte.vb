Public Class Ditte
    Inherits ITabellaDB

    Protected m_Id_Ditta As Integer
    Protected m_ID_Cliente As Nullable(Of Integer)
    Protected m_CODICE_EDP As Nullable(Of Integer)
    Protected m_id_tipo_cliente As Nullable(Of Integer)
    Protected m_Rag_soc As String
    Protected m_Indirizzo As String
    Protected m_Citta As String
    Protected m_id_comune_ares As Nullable(Of Integer)
    Protected m_provincia As String
    Protected m_Cap As String
    Protected m_NAZIONE As Nullable(Of Integer)
    Protected m_TIPO_PARTITA_IVA As String
    Protected m_PIva As String
    Protected m_PIva_ESTERA As String
    Protected m_c_fis As String
    Protected m_Fax As String
    Protected m_Tel As String
    Protected m_ID_Sconto As Nullable(Of Integer)
    Protected m_CATEGORIA As String
    Protected m_FULL_CREDIT As Nullable(Of Boolean)
    Protected m_VIP_CARD As Nullable(Of Boolean)
    Protected m_STATO_CLI As String
    Protected m_PRODUTTORE As Nullable(Of Integer)
    Protected m_Tour_op As Nullable(Of Boolean)
    Protected m_Note As String
    Protected m_art_es As Nullable(Of Integer)
    Protected m_s_iva As Nullable(Of Boolean)
    Protected m_per_fis As Nullable(Of Boolean)
    Protected m_ID_ModPag As Nullable(Of Integer)
    Protected m_ID_Pagamento As Nullable(Of Integer)
    Protected m_TIPO_SPEDIZIONE_FATTURA As String
    Protected m_INVIO_EMAIL As Nullable(Of Boolean)
    Protected m_EMAIL As String
    Protected m_email_pec As String
    Protected m_codice_sdi As String
    Protected m_INVIO_EMAIL_CC As Nullable(Of Boolean)
    Protected m_EMAIL_CC As String
    Protected m_INVIO_EMAIL_STATEMENT As Nullable(Of Boolean)
    Protected m_EMAIL_STATEMENT As String
    Protected m_ULTIMO_INVIO_EMAIL As Nullable(Of DateTime)
    Protected m_NO_INVIO_EMAIL_MARKETING As Nullable(Of Boolean)
    Protected m_ID_CONVENZIONE As Nullable(Of Integer)
    Protected m_DATACRE As Nullable(Of DateTime)
    Protected m_UTECRE As String
    Protected m_TIPOMOD As String
    Protected m_UTEMOD As String
    Protected m_DATAMOD As Nullable(Of DateTime)
    Protected m_id_comune As Nullable(Of Integer)
    Protected m_id_cliente_tipologia As Nullable(Of Integer)
    Protected m_ultimo_invio_email_marketing As Nullable(Of Date)
    Protected m_black_list As Nullable(Of Boolean)
    Protected m_note_black_list As String
    Protected m_operatore_immissione_black_list As Nullable(Of Integer)
    Protected m_data_immissione_black_list As Nullable(Of DateTime)
    Protected m_operatore_rimozione_black_list As Nullable(Of Integer)
    Protected m_data_rimozione_black_list As Nullable(Of DateTime)

    Public ReadOnly Property Id_Ditta() As Integer
        Get
            Return m_Id_Ditta
        End Get
    End Property
    Public ReadOnly Property ID_Cliente() As Nullable(Of Integer)
        Get
            Return m_ID_Cliente
        End Get
    End Property
    Public ReadOnly Property CODICE_EDP() As Nullable(Of Integer)
        Get
            Return m_CODICE_EDP
        End Get
    End Property
    Public Property id_tipo_cliente() As Nullable(Of Integer)
        Get
            Return m_id_tipo_cliente
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id_tipo_cliente = value
        End Set
    End Property

    Public Property Rag_soc() As String
        Get
            Return m_Rag_soc
        End Get
        Set(ByVal value As String)
            m_Rag_soc = value
        End Set
    End Property
    Public Property Indirizzo() As String
        Get
            Return m_Indirizzo
        End Get
        Set(ByVal value As String)
            m_Indirizzo = value
        End Set
    End Property
    Public Property Citta() As String
        Get
            Return m_Citta
        End Get
        Set(ByVal value As String)
            m_Citta = value
        End Set
    End Property
    Public Property id_comune_ares() As Nullable(Of Integer)
        Get
            Return m_id_comune_ares
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id_comune_ares = value
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
    Public Property Cap() As String
        Get
            Return m_Cap
        End Get
        Set(ByVal value As String)
            m_Cap = value
        End Set
    End Property
    Public Property NAZIONE() As Nullable(Of Integer)
        Get
            Return m_NAZIONE
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_NAZIONE = value
        End Set
    End Property
    Public Property TIPO_PARTITA_IVA() As String
        Get
            Return m_TIPO_PARTITA_IVA
        End Get
        Set(ByVal value As String)
            m_TIPO_PARTITA_IVA = value
        End Set
    End Property
    Public Property PIva() As String
        Get
            Return m_PIva
        End Get
        Set(ByVal value As String)
            m_PIva = value
        End Set
    End Property
    Public Property PIva_ESTERA() As String
        Get
            Return m_PIva_ESTERA
        End Get
        Set(ByVal value As String)
            m_PIva_ESTERA = value
        End Set
    End Property
    Public Property c_fis() As String
        Get
            Return m_c_fis
        End Get
        Set(ByVal value As String)
            m_c_fis = value
        End Set
    End Property
    Public Property Fax() As String
        Get
            Return m_Fax
        End Get
        Set(ByVal value As String)
            m_Fax = value
        End Set
    End Property
    Public Property Tel() As String
        Get
            Return m_Tel
        End Get
        Set(ByVal value As String)
            m_Tel = value
        End Set
    End Property
    Public Property ID_Sconto() As Nullable(Of Integer)
        Get
            Return m_ID_Sconto
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_ID_Sconto = value
        End Set
    End Property
    Public Property CATEGORIA() As String
        Get
            Return m_CATEGORIA
        End Get
        Set(ByVal value As String)
            m_CATEGORIA = value
        End Set
    End Property
    Public Property FULL_CREDIT() As Nullable(Of Boolean)
        Get
            Return m_FULL_CREDIT
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_FULL_CREDIT = value
        End Set
    End Property
    Public Property VIP_CARD() As Nullable(Of Boolean)
        Get
            Return m_VIP_CARD
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_VIP_CARD = value
        End Set
    End Property
    Public Property STATO_CLI() As String
        Get
            Return m_STATO_CLI
        End Get
        Set(ByVal value As String)
            m_STATO_CLI = value
        End Set
    End Property
    Public Property PRODUTTORE() As Nullable(Of Integer)
        Get
            Return m_PRODUTTORE
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_PRODUTTORE = value
        End Set
    End Property
    Public Property Tour_op() As Nullable(Of Boolean)
        Get
            Return m_Tour_op
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_Tour_op = value
        End Set
    End Property
    Public Property Note() As String
        Get
            Return m_Note
        End Get
        Set(ByVal value As String)
            m_Note = value
        End Set
    End Property
    Public Property art_es() As Nullable(Of Integer)
        Get
            Return m_art_es
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_art_es = value
        End Set
    End Property
    Public Property s_iva() As Nullable(Of Boolean)
        Get
            Return m_s_iva
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_s_iva = value
        End Set
    End Property
    Public Property per_fis() As Nullable(Of Boolean)
        Get
            Return m_per_fis
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_per_fis = value
        End Set
    End Property
    Public Property ID_ModPag() As Nullable(Of Integer)
        Get
            Return m_ID_ModPag
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_ID_ModPag = value
        End Set
    End Property
    Public Property ID_Pagamento() As Nullable(Of Integer)
        Get
            Return m_ID_Pagamento
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_ID_Pagamento = value
        End Set
    End Property
    Public Property TIPO_SPEDIZIONE_FATTURA() As String
        Get
            Return m_TIPO_SPEDIZIONE_FATTURA
        End Get
        Set(ByVal value As String)
            m_TIPO_SPEDIZIONE_FATTURA = value
        End Set
    End Property
    Public Property INVIO_EMAIL() As Nullable(Of Boolean)
        Get
            Return m_INVIO_EMAIL
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_INVIO_EMAIL = value
        End Set
    End Property
    Public Property EMAIL() As String
        Get
            Return m_EMAIL
        End Get
        Set(ByVal value As String)
            m_EMAIL = value
        End Set
    End Property

    Public Property email_pec() As String
        Get
            Return m_email_pec
        End Get
        Set(ByVal value As String)
            m_email_pec = value
        End Set
    End Property

    Public Property codice_sdi() As String
        Get
            Return m_codice_sdi
        End Get
        Set(ByVal value As String)
            m_codice_sdi = value
        End Set
    End Property

    Public Property INVIO_EMAIL_CC() As Nullable(Of Boolean)
        Get
            Return m_INVIO_EMAIL_CC
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_INVIO_EMAIL_CC = value
        End Set
    End Property
    Public Property EMAIL_CC() As String
        Get
            Return m_EMAIL_CC
        End Get
        Set(ByVal value As String)
            m_EMAIL_CC = value
        End Set
    End Property
    Public Property INVIO_EMAIL_STATEMENT() As Nullable(Of Boolean)
        Get
            Return m_INVIO_EMAIL_STATEMENT
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_INVIO_EMAIL_STATEMENT = value
        End Set
    End Property
    Public Property EMAIL_STATEMENT() As String
        Get
            Return m_EMAIL_STATEMENT
        End Get
        Set(ByVal value As String)
            m_EMAIL_STATEMENT = value
        End Set
    End Property
    Public Property ULTIMO_INVIO_EMAIL() As Nullable(Of DateTime)
        Get
            Return m_ULTIMO_INVIO_EMAIL
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            m_ULTIMO_INVIO_EMAIL = value
        End Set
    End Property
    Public Property NO_INVIO_EMAIL_MARKETING() As Nullable(Of Boolean)
        Get
            Return m_NO_INVIO_EMAIL_MARKETING
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_NO_INVIO_EMAIL_MARKETING = value
        End Set
    End Property
    Public Property ID_CONVENZIONE() As Nullable(Of Integer)
        Get
            Return m_ID_CONVENZIONE
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_ID_CONVENZIONE = value
        End Set
    End Property
    Public Property DATACRE() As Nullable(Of DateTime)
        Get
            Return m_DATACRE
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            m_DATACRE = value
        End Set
    End Property
    Public Property UTECRE() As String
        Get
            Return m_UTECRE
        End Get
        Set(ByVal value As String)
            m_UTECRE = value
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
    Public Property UTEMOD() As String
        Get
            Return m_UTEMOD
        End Get
        Set(ByVal value As String)
            m_UTEMOD = value
        End Set
    End Property
    Public Property DATAMOD() As Nullable(Of DateTime)
        Get
            Return m_DATAMOD
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            m_DATAMOD = value
        End Set
    End Property
    Public Property id_comune() As Nullable(Of Integer)
        Get
            Return m_id_comune
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id_comune = value
        End Set
    End Property
    Public Property id_cliente_tipologia() As Nullable(Of Integer)
        Get
            Return m_id_cliente_tipologia
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_id_cliente_tipologia = value
        End Set
    End Property
    Public Property ultimo_invio_email_marketing() As Nullable(Of Date)
        Get
            Return m_ultimo_invio_email_marketing
        End Get
        Set(ByVal value As Nullable(Of Date))
            m_ultimo_invio_email_marketing = value
        End Set
    End Property
    Public Property black_list() As Nullable(Of Boolean)
        Get
            Return m_black_list
        End Get
        Set(ByVal value As Nullable(Of Boolean))
            m_black_list = value
        End Set
    End Property

    Public Property note_black_list() As String
        Get
            Return m_note_black_list
        End Get
        Set(ByVal value As String)
            m_note_black_list = value
        End Set
    End Property


    Public Property operatore_immissione_black_list() As Nullable(Of Integer)
        Get
            Return m_operatore_immissione_black_list
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_operatore_immissione_black_list = value
        End Set
    End Property

    Public Property data_immissione_black_list() As Nullable(Of DateTime)
        Get
            Return m_data_immissione_black_list
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            m_data_immissione_black_list = value
        End Set
    End Property

    Public Property operatore_rimozione_black_list() As Nullable(Of Integer)
        Get
            Return m_operatore_rimozione_black_list
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_operatore_rimozione_black_list = value
        End Set
    End Property

    Public Property data_rimozione_black_list() As Nullable(Of DateTime)
        Get
            Return m_data_rimozione_black_list
        End Get
        Set(ByVal value As Nullable(Of DateTime))
            m_data_rimozione_black_list = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional RecuperaId As Boolean = False) As Integer
        'V
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "INSERT INTO DITTE (ID_Cliente,[CODICE EDP],id_tipo_cliente,Rag_soc,Indirizzo,Citta,id_comune_ares,provincia,Cap,NAZIONE,TIPO_PARTITA_IVA,PIva,PIva_ESTERA,c_fis,Fax,Tel,ID_Sconto,CATEGORIA,FULL_CREDIT,VIP_CARD,STATO_CLI,PRODUTTORE,Tour_op,Note,art_es,s_iva,per_fis,ID_ModPag,ID_Pagamento,TIPO_SPEDIZIONE_FATTURA,INVIO_EMAIL,EMAIL,INVIO_EMAIL_CC,EMAIL_CC,INVIO_EMAIL_STATEMENT,EMAIL_STATEMENT,ULTIMO_INVIO_EMAIL,NO_INVIO_EMAIL_MARKETING,ID_CONVENZIONE,DATACRE,UTECRE,TIPOMOD,UTEMOD,DATAMOD,id_comune,id_cliente_tipologia,ultimo_invio_email_marketing, black_list,note_black_list, email_pec, codice_sdi)" &
            " VALUES (@ID_Cliente,@CODICE_EDP,@id_tipo_cliente,@Rag_soc,@Indirizzo,@Citta,@id_comune_ares,@provincia,@Cap,@NAZIONE,@TIPO_PARTITA_IVA,@PIva,@PIva_ESTERA,@c_fis,@Fax,@Tel,@ID_Sconto,@CATEGORIA,@FULL_CREDIT,@VIP_CARD,@STATO_CLI,@PRODUTTORE,@Tour_op,@Note,@art_es,@s_iva,@per_fis,@ID_ModPag,@ID_Pagamento,@TIPO_SPEDIZIONE_FATTURA,@INVIO_EMAIL,@EMAIL,@INVIO_EMAIL_CC,@EMAIL_CC,@INVIO_EMAIL_STATEMENT,@EMAIL_STATEMENT,@ULTIMO_INVIO_EMAIL,@NO_INVIO_EMAIL_MARKETING,@ID_CONVENZIONE,@DATACRE,@UTECRE,@TIPOMOD,@UTEMOD,@DATAMOD,@id_comune,@id_cliente_tipologia,@ultimo_invio_email_marketing,@black_list,@note_black_list,@email_pec, @codice_sdi)"
        'HttpContext.Current.Trace.Write("SalvaRecord: " & sqlStr)

        Dim sql1 As String, sql2 As String

        Try
            Dim CodiceEDP As String = Contatori.NewCodiceEDP()
            'HttpContext.Current.Response.Write("error salvarecord ditte CodiceEDP : " & CodiceEDP & "<br/>") 'test

            If CodiceEDP = "" Then
                m_CODICE_EDP = Nothing
                m_ID_Cliente = Nothing
            Else
                m_CODICE_EDP = Integer.Parse(CodiceEDP)
                m_ID_Cliente = m_CODICE_EDP
            End If

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            addParametro(Cmd, "@ID_Cliente", System.Data.SqlDbType.Int, ID_Cliente)
            addParametro(Cmd, "@CODICE_EDP", System.Data.SqlDbType.Int, CODICE_EDP)
            addParametro(Cmd, "@id_tipo_cliente", System.Data.SqlDbType.Int, id_tipo_cliente)
            addParametro(Cmd, "@Rag_soc", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(Rag_soc, 50))
            addParametro(Cmd, "@Indirizzo", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(Indirizzo, 50))
            addParametro(Cmd, "@Citta", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(Citta, 50))
            addParametro(Cmd, "@id_comune_ares", System.Data.SqlDbType.Int, id_comune_ares)
            addParametro(Cmd, "@provincia", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(provincia, 2))
            addParametro(Cmd, "@Cap", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(Cap, 10))
            addParametro(Cmd, "@NAZIONE", System.Data.SqlDbType.Int, NAZIONE)
            addParametro(Cmd, "@TIPO_PARTITA_IVA", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(TIPO_PARTITA_IVA, 3))
            addParametro(Cmd, "@PIva", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(PIva, 20))
            addParametro(Cmd, "@PIva_ESTERA", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(PIva_ESTERA, 20))
            addParametro(Cmd, "@c_fis", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(c_fis, 20))
            addParametro(Cmd, "@Fax", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(Fax, 50))
            addParametro(Cmd, "@Tel", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(Tel, 50))
            addParametro(Cmd, "@ID_Sconto", System.Data.SqlDbType.Int, ID_Sconto)
            addParametro(Cmd, "@CATEGORIA", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(CATEGORIA, 6))
            addParametro(Cmd, "@FULL_CREDIT", System.Data.SqlDbType.Bit, FULL_CREDIT)
            addParametro(Cmd, "@VIP_CARD", System.Data.SqlDbType.Bit, VIP_CARD)
            addParametro(Cmd, "@STATO_CLI", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(STATO_CLI, 2))
            addParametro(Cmd, "@PRODUTTORE", System.Data.SqlDbType.Int, PRODUTTORE)
            addParametro(Cmd, "@Tour_op", System.Data.SqlDbType.Bit, Tour_op)
            addParametro(Cmd, "@Note", System.Data.SqlDbType.NVarChar, Libreria.formattaSqlTrim(Note))
            addParametro(Cmd, "@art_es", System.Data.SqlDbType.Int, art_es)
            addParametro(Cmd, "@s_iva", System.Data.SqlDbType.Bit, s_iva)
            addParametro(Cmd, "@per_fis", System.Data.SqlDbType.Bit, per_fis)
            addParametro(Cmd, "@ID_ModPag", System.Data.SqlDbType.Int, ID_ModPag)
            addParametro(Cmd, "@ID_Pagamento", System.Data.SqlDbType.Int, ID_Pagamento)
            addParametro(Cmd, "@TIPO_SPEDIZIONE_FATTURA", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(TIPO_SPEDIZIONE_FATTURA, 1))
            addParametro(Cmd, "@INVIO_EMAIL", System.Data.SqlDbType.Bit, INVIO_EMAIL)
            addParametro(Cmd, "@EMAIL", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(EMAIL, 50))
            addParametro(Cmd, "@INVIO_EMAIL_CC", System.Data.SqlDbType.Bit, INVIO_EMAIL_CC)
            addParametro(Cmd, "@EMAIL_CC", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(EMAIL_CC, 50))
            addParametro(Cmd, "@INVIO_EMAIL_STATEMENT", System.Data.SqlDbType.Bit, INVIO_EMAIL_STATEMENT)
            addParametro(Cmd, "@EMAIL_STATEMENT", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(EMAIL_STATEMENT, 50))
            addParametro(Cmd, "@ULTIMO_INVIO_EMAIL", System.Data.SqlDbType.DateTime, ULTIMO_INVIO_EMAIL)
            addParametro(Cmd, "@NO_INVIO_EMAIL_MARKETING", System.Data.SqlDbType.Bit, NO_INVIO_EMAIL_MARKETING)
            addParametro(Cmd, "@ID_CONVENZIONE", System.Data.SqlDbType.Int, ID_CONVENZIONE)
            addParametro(Cmd, "@DATACRE", System.Data.SqlDbType.DateTime, DATACRE)
            addParametro(Cmd, "@UTECRE", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(UTECRE, 15))
            addParametro(Cmd, "@TIPOMOD", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(TIPOMOD, 1))
            addParametro(Cmd, "@UTEMOD", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(UTEMOD, 10))
            addParametro(Cmd, "@DATAMOD", System.Data.SqlDbType.DateTime, DATAMOD)
            addParametro(Cmd, "@id_comune", System.Data.SqlDbType.Int, id_comune)
            addParametro(Cmd, "@id_cliente_tipologia", System.Data.SqlDbType.Int, id_cliente_tipologia)
            addParametro(Cmd, "@ultimo_invio_email_marketing", System.Data.SqlDbType.Date, ultimo_invio_email_marketing)
            addParametro(Cmd, "@black_list", System.Data.SqlDbType.Bit, black_list)
            addParametro(Cmd, "@note_black_list", System.Data.SqlDbType.NVarChar, note_black_list)
            addParametro(Cmd, "@email_pec", System.Data.SqlDbType.NVarChar, email_pec.Replace("'", "''"))
            addParametro(Cmd, "@codice_sdi", System.Data.SqlDbType.NVarChar, codice_sdi.Replace("'", "''"))

            sql1 = Cmd.CommandText

            Cmd.ExecuteNonQuery()
            sql1 = ""

            sqlStr = "SELECT @@IDENTITY FROM DITTE WITH(NOLOCK)"
            sql2 = sqlStr
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            SalvaRecord = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error salvarecord : " & ex.Message & "<br/>sql1:  " & sql1 & "<br/>sql2:" & sql2 & "<br/>")
        End Try






    End Function

    Private Shared Function FillRecord(Rs As System.Data.SqlClient.SqlDataReader) As Ditte
        'riempie i campi per la fattura
        Dim mio_record As Ditte = New Ditte
        With mio_record
            .m_Id_Ditta = Rs("Id_Ditta")
            .m_ID_Cliente = getValueOrNohing(Rs("ID_Cliente"))
            .m_CODICE_EDP = getValueOrNohing(Rs("CODICE_EDP"))
            .m_id_tipo_cliente = getValueOrNohing(Rs("id_tipo_cliente"))
            .Rag_soc = getValueOrNohing(Rs("Rag_soc"))
            .Indirizzo = getValueOrNohing(Rs("Indirizzo"))
            .Citta = getValueOrNohing(Rs("Citta"))
            .id_comune_ares = getValueOrNohing(Rs("id_comune_ares"))
            .provincia = getValueOrNohing(Rs("provincia"))
            .Cap = getValueOrNohing(Rs("Cap"))
            .NAZIONE = getValueOrNohing(Rs("NAZIONE"))
            .TIPO_PARTITA_IVA = getValueOrNohing(Rs("TIPO_PARTITA_IVA"))
            .PIva = getValueOrNohing(Rs("PIva"))
            .PIva_ESTERA = getValueOrNohing(Rs("PIva_ESTERA"))
            .c_fis = getValueOrNohing(Rs("c_fis"))
            .Fax = getValueOrNohing(Rs("Fax"))
            .Tel = getValueOrNohing(Rs("Tel"))
            .ID_Sconto = getValueOrNohing(Rs("ID_Sconto"))
            .CATEGORIA = getValueOrNohing(Rs("CATEGORIA"))
            .FULL_CREDIT = getValueOrNohing(Rs("FULL_CREDIT"))
            .VIP_CARD = getValueOrNohing(Rs("VIP_CARD"))
            .STATO_CLI = getValueOrNohing(Rs("STATO_CLI"))
            .PRODUTTORE = getValueOrNohing(Rs("PRODUTTORE"))
            .Tour_op = getValueOrNohing(Rs("Tour_op"))
            .Note = getValueOrNohing(Rs("Note"))
            .art_es = getValueOrNohing(Rs("art_es"))
            .s_iva = getValueOrNohing(Rs("s_iva"))
            .per_fis = getValueOrNohing(Rs("per_fis"))
            .ID_ModPag = getValueOrNohing(Rs("ID_ModPag"))
            .ID_Pagamento = getValueOrNohing(Rs("ID_Pagamento"))
            .TIPO_SPEDIZIONE_FATTURA = getValueOrNohing(Rs("TIPO_SPEDIZIONE_FATTURA"))
            .INVIO_EMAIL = getValueOrNohing(Rs("INVIO_EMAIL"))
            .EMAIL = getValueOrNohing(Rs("EMAIL"))
            .INVIO_EMAIL_CC = getValueOrNohing(Rs("INVIO_EMAIL_CC"))
            .EMAIL_CC = getValueOrNohing(Rs("EMAIL_CC"))
            .INVIO_EMAIL_STATEMENT = getValueOrNohing(Rs("INVIO_EMAIL_STATEMENT"))
            .EMAIL_STATEMENT = getValueOrNohing(Rs("EMAIL_STATEMENT"))
            .ULTIMO_INVIO_EMAIL = getValueOrNohing(Rs("ULTIMO_INVIO_EMAIL"))
            .NO_INVIO_EMAIL_MARKETING = getValueOrNohing(Rs("NO_INVIO_EMAIL_MARKETING"))
            .ID_CONVENZIONE = getValueOrNohing(Rs("ID_CONVENZIONE"))
            .DATACRE = getValueOrNohing(Rs("DATACRE"))
            .UTECRE = getValueOrNohing(Rs("UTECRE"))
            .TIPOMOD = getValueOrNohing(Rs("TIPOMOD"))
            .UTEMOD = getValueOrNohing(Rs("UTEMOD"))
            .DATAMOD = getValueOrNohing(Rs("DATAMOD"))
            .id_comune = getValueOrNohing(Rs("id_comune"))
            .id_cliente_tipologia = getValueOrNohing(Rs("id_cliente_tipologia"))
            .ultimo_invio_email_marketing = getValueOrNohing(Rs("ultimo_invio_email_marketing"))
            .black_list = getValueOrNohing(Rs("black_list"))
            .operatore_immissione_black_list = getValueOrNohing(Rs("operatore_immissione_black_list"))
            .data_immissione_black_list = getValueOrNohing(Rs("data_immissione_black_list"))
            .operatore_rimozione_black_list = getValueOrNohing(Rs("operatore_rimozione_black_list"))
            .data_rimozione_black_list = getValueOrNohing(Rs("data_rimozione_black_list"))
            .note_black_list = getValueOrNohing(Rs("note_black_list"))
            .email_pec = Rs("email_pec") & ""
            .codice_sdi = Rs("codice_sdi") & ""
        End With
        Return mio_record
    End Function

    Public Shared Function RecuperaRecordDaId(id_record As Integer) As Ditte
        'V
        RecuperaRecordDaId = Nothing
        Dim sqlStr As String = "SELECT [CODICE EDP] CODICE_EDP, ISNULL(black_list,'0') As black_list, * FROM DITTE WHERE Id_Ditta = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Using Rs = Cmd.ExecuteReader()
                        If Rs.Read() Then
                            Return FillRecord(Rs)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Response.Write("error RecuperaRecordDaId : " & ex.Message & "<br/>sqlStr:  " & sqlStr & "<br/>")
        End Try

    End Function

    Public Shared Function RecuperaRecordDaId_Cliente(ByVal id_record As Integer) As Ditte
        'V
        'utilizzata dalla procedura di fattura multe e precisamente da gestione_multe/FattureMulte.ascx
        'perchè sono in possesso del campo id_cliente anzichè dell'id_ditta
        RecuperaRecordDaId_Cliente = Nothing
        Dim sqlStr As String = "SELECT [CODICE EDP] CODICE_EDP, ISNULL(black_list,'0') As black_list, * FROM DITTE WHERE Id_Cliente = " & id_record
        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Using Rs = Cmd.ExecuteReader()
                        If Rs.Read() Then
                            Return FillRecord(Rs)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Response.Write("error RecuperaRecordDaId_Cliente : " & ex.Message & "<br/>sqlStr:  " & sqlStr & "<br/>")
        End Try

    End Function

    Public Shared Function RecuperaRecordDaCodiceEDP(CODICE_EDP As Integer) As Ditte
        'V
        RecuperaRecordDaCodiceEDP = Nothing
        Dim sqlStr As String = "SELECT [CODICE EDP] CODICE_EDP, * FROM DITTE WHERE [CODICE EDP] = " & CODICE_EDP
        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Using Rs = Cmd.ExecuteReader()
                        If Rs.Read() Then
                            Return FillRecord(Rs)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Response.Write("error RecuperaRecordDaCodiceEDP : " & ex.Message & "<br/>sqlStr:  " & sqlStr & "<br/>")
        End Try


    End Function

    Public Function ModificaRecord(id_record As Integer) As Boolean
        'V
        ModificaRecord = False

        Dim sqlStr As String = "UPDATE DITTE SET" &
            " Rag_soc = @Rag_soc," &
            " id_tipo_cliente = @id_tipo_cliente," &
            " Indirizzo = @Indirizzo," &
            " Citta = @Citta," &
            " id_comune_ares = @id_comune_ares," &
            " provincia = @provincia," &
            " Cap = @Cap," &
            " NAZIONE = @NAZIONE," &
            " TIPO_PARTITA_IVA = @TIPO_PARTITA_IVA," &
            " PIva = @PIva," &
            " PIva_ESTERA = @PIva_ESTERA," &
            " c_fis = @c_fis," &
            " Fax = @Fax," &
            " Tel = @Tel," &
            " ID_Sconto = @ID_Sconto," &
            " CATEGORIA = @CATEGORIA," &
            " FULL_CREDIT = @FULL_CREDIT," &
            " VIP_CARD = @VIP_CARD," &
            " STATO_CLI = @STATO_CLI," &
            " PRODUTTORE = @PRODUTTORE," &
            " Tour_op = @Tour_op," &
            " Note = @Note," &
            " art_es = @art_es," &
            " s_iva = @s_iva," &
            " per_fis = @per_fis," &
            " ID_ModPag = @ID_ModPag," &
            " ID_Pagamento = @ID_Pagamento," &
            " TIPO_SPEDIZIONE_FATTURA = @TIPO_SPEDIZIONE_FATTURA," &
            " INVIO_EMAIL = @INVIO_EMAIL," &
            " EMAIL = @EMAIL," &
            " email_pec = @email_pec," &
            " codice_sdi = @codice_sdi," &
            " INVIO_EMAIL_CC = @INVIO_EMAIL_CC," &
            " EMAIL_CC = @EMAIL_CC," &
            " INVIO_EMAIL_STATEMENT = @INVIO_EMAIL_STATEMENT," &
            " EMAIL_STATEMENT = @EMAIL_STATEMENT," &
            " ULTIMO_INVIO_EMAIL = @ULTIMO_INVIO_EMAIL," &
            " NO_INVIO_EMAIL_MARKETING = @NO_INVIO_EMAIL_MARKETING," &
            " ID_CONVENZIONE = @ID_CONVENZIONE," &
            " DATACRE = @DATACRE," &
            " UTECRE = @UTECRE," &
            " TIPOMOD = @TIPOMOD," &
            " UTEMOD = @UTEMOD," &
            " DATAMOD = @DATAMOD," &
            " id_comune = @id_comune," &
            " id_cliente_tipologia = @id_cliente_tipologia," &
            " ultimo_invio_email_marketing = @ultimo_invio_email_marketing, " &
            " black_list = @black_list, " &
            " note_black_list=ISNULL(note_black_list,'') + @note_black_list," &
            " operatore_immissione_black_list = @operatore_immissione_black_list, " &
            " data_immissione_black_list = @data_immissione_black_list, " &
            " operatore_rimozione_black_list = @operatore_rimozione_black_list, " &
            " data_rimozione_black_list = @data_rimozione_black_list " &
            " WHERE Id_Ditta = @Id_Ditta"
        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@Rag_soc", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(Rag_soc, 50))
                    addParametro(Cmd, "@id_tipo_cliente", System.Data.SqlDbType.Int, id_tipo_cliente)
                    addParametro(Cmd, "@Indirizzo", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(Indirizzo, 50))
                    addParametro(Cmd, "@Citta", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(Citta, 50))
                    addParametro(Cmd, "@id_comune_ares", System.Data.SqlDbType.Int, id_comune_ares)
                    addParametro(Cmd, "@provincia", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(provincia, 2))
                    addParametro(Cmd, "@Cap", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(Cap, 10))
                    addParametro(Cmd, "@NAZIONE", System.Data.SqlDbType.Int, NAZIONE)
                    addParametro(Cmd, "@TIPO_PARTITA_IVA", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(TIPO_PARTITA_IVA, 3))
                    addParametro(Cmd, "@PIva", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(PIva, 20))
                    addParametro(Cmd, "@PIva_ESTERA", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(PIva_ESTERA, 20))
                    addParametro(Cmd, "@c_fis", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(c_fis, 20))
                    addParametro(Cmd, "@Fax", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(Fax, 50))
                    addParametro(Cmd, "@Tel", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(Tel, 50))
                    addParametro(Cmd, "@ID_Sconto", System.Data.SqlDbType.Int, ID_Sconto)
                    addParametro(Cmd, "@CATEGORIA", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(CATEGORIA, 6))
                    addParametro(Cmd, "@FULL_CREDIT", System.Data.SqlDbType.Bit, FULL_CREDIT)
                    addParametro(Cmd, "@VIP_CARD", System.Data.SqlDbType.Bit, VIP_CARD)
                    addParametro(Cmd, "@STATO_CLI", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(STATO_CLI, 2))
                    addParametro(Cmd, "@PRODUTTORE", System.Data.SqlDbType.Int, PRODUTTORE)
                    addParametro(Cmd, "@Tour_op", System.Data.SqlDbType.Bit, Tour_op)
                    addParametro(Cmd, "@Note", System.Data.SqlDbType.NVarChar, Libreria.formattaSqlTrim(Note))
                    addParametro(Cmd, "@art_es", System.Data.SqlDbType.Int, art_es)
                    addParametro(Cmd, "@s_iva", System.Data.SqlDbType.Bit, s_iva)
                    addParametro(Cmd, "@per_fis", System.Data.SqlDbType.Bit, per_fis)
                    addParametro(Cmd, "@ID_ModPag", System.Data.SqlDbType.Int, ID_ModPag)
                    addParametro(Cmd, "@ID_Pagamento", System.Data.SqlDbType.Int, ID_Pagamento)
                    addParametro(Cmd, "@TIPO_SPEDIZIONE_FATTURA", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(TIPO_SPEDIZIONE_FATTURA, 1))
                    addParametro(Cmd, "@INVIO_EMAIL", System.Data.SqlDbType.Bit, INVIO_EMAIL)
                    addParametro(Cmd, "@EMAIL", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(EMAIL, 50))
                    addParametro(Cmd, "@INVIO_EMAIL_CC", System.Data.SqlDbType.Bit, INVIO_EMAIL_CC)
                    addParametro(Cmd, "@EMAIL_CC", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(EMAIL_CC, 50))
                    addParametro(Cmd, "@INVIO_EMAIL_STATEMENT", System.Data.SqlDbType.Bit, INVIO_EMAIL_STATEMENT)
                    addParametro(Cmd, "@EMAIL_STATEMENT", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(EMAIL_STATEMENT, 50))
                    addParametro(Cmd, "@ULTIMO_INVIO_EMAIL", System.Data.SqlDbType.DateTime, ULTIMO_INVIO_EMAIL)
                    addParametro(Cmd, "@NO_INVIO_EMAIL_MARKETING", System.Data.SqlDbType.Bit, NO_INVIO_EMAIL_MARKETING)
                    addParametro(Cmd, "@ID_CONVENZIONE", System.Data.SqlDbType.Int, ID_CONVENZIONE)
                    addParametro(Cmd, "@DATACRE", System.Data.SqlDbType.DateTime, DATACRE)
                    addParametro(Cmd, "@UTECRE", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(UTECRE, 15))
                    addParametro(Cmd, "@TIPOMOD", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(TIPOMOD, 1))
                    addParametro(Cmd, "@UTEMOD", System.Data.SqlDbType.NVarChar, Libreria.SubstringSicuro(UTEMOD, 10))
                    addParametro(Cmd, "@DATAMOD", System.Data.SqlDbType.DateTime, DATAMOD)
                    addParametro(Cmd, "@id_comune", System.Data.SqlDbType.Int, id_comune)
                    addParametro(Cmd, "@id_cliente_tipologia", System.Data.SqlDbType.Int, id_cliente_tipologia)
                    addParametro(Cmd, "@ultimo_invio_email_marketing", System.Data.SqlDbType.Date, ultimo_invio_email_marketing)
                    addParametro(Cmd, "@Id_Ditta", System.Data.SqlDbType.Int, id_record)
                    addParametro(Cmd, "@black_list", System.Data.SqlDbType.Bit, black_list)
                    addParametro(Cmd, "@note_black_list", System.Data.SqlDbType.NVarChar, note_black_list)
                    addParametro(Cmd, "@operatore_immissione_black_list", System.Data.SqlDbType.Int, operatore_immissione_black_list)
                    addParametro(Cmd, "@data_immissione_black_list", System.Data.SqlDbType.DateTime, data_immissione_black_list)
                    addParametro(Cmd, "@operatore_rimozione_black_list", System.Data.SqlDbType.Int, operatore_rimozione_black_list)
                    addParametro(Cmd, "@data_rimozione_black_list", System.Data.SqlDbType.DateTime, data_rimozione_black_list)
                    addParametro(Cmd, "@email_pec", System.Data.SqlDbType.NVarChar, email_pec.Replace("'", "''"))
                    addParametro(Cmd, "@codice_sdi", System.Data.SqlDbType.NVarChar, codice_sdi.Replace("'", "''"))

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    sqlStr = Cmd.CommandText    'aggiunto
                    ModificaRecord = True
                End Using
            End Using

        Catch ex As Exception
            HttpContext.Current.Response.Write("error RecuperaRecordDaCodiceEDP : " & ex.Message & "<br/>sqlStr:  " & sqlStr & "<br/>")
        End Try



    End Function

    Public Shared Function CancellaDittaDaId(id_record As Integer) As Boolean
        'rende inattiva una ditta 'A
        CancellaDittaDaId = False
        Dim sqlStr As String = "UPDATE DITTE SET attiva = 0 WHERE Id_Ditta = " & id_record
        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    CancellaDittaDaId = False
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Response.Write("error CancellaDittaDaId : " & ex.Message & "<br/>sqlStr:  " & sqlStr & "<br/>")
        End Try

    End Function

End Class
