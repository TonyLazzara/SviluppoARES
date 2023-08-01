Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Public Enum TipoFattura
    Prenotazione = 1
    Noleggio
    RDS
    Multe
End Enum

Public Enum TipoStatoFattura
    NonAttiva = 0
    Attiva = 1
End Enum

Public Enum TipoScontoFattura
    Somma = 0
    Percentuale = 1
End Enum

Public Enum StatoFiltro
    Chiuso = -1
    Nuovo = 0
    Export
    Import
    Decisione
End Enum

Public Class TipoFiltroFatture
    Inherits ITabellaDB
    Public id As Integer = -1
    Public tipo_fattura As TipoFattura
    Public Annofatturazione As Integer
    Public DaNumFattura As Integer? = Nothing
    Public ANumfattura As Integer? = Nothing
    Public DaDataFattura As DateTime? = Nothing
    Public ADataFattura As DateTime? = Nothing
    Public stato As StatoFiltro = StatoFiltro.Nuovo

    Public Overrides Function SalvaRecord(Optional RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)

            sqlStr = "INSERT INTO Fatture_FiltroExport (datacre,id_utente,tipo_fattura,Annofatturazione,DaNumFattura,ANumfattura,DaDataFattura,ADataFattura,stato)" & _
                " VALUES (getdate(),@id_utente,@tipo_fattura,@Annofatturazione,@DaNumFattura,@ANumfattura,@DaDataFattura,@ADataFattura,0)"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente")))
                addParametro(Cmd, "@tipo_fattura", System.Data.SqlDbType.Int, tipo_fattura)
                addParametro(Cmd, "@Annofatturazione", System.Data.SqlDbType.Int, Annofatturazione)
                addParametro(Cmd, "@DaNumFattura", System.Data.SqlDbType.Int, DaNumFattura)
                addParametro(Cmd, "@ANumfattura", System.Data.SqlDbType.Int, ANumfattura)
                addParametro(Cmd, "@DaDataFattura", System.Data.SqlDbType.DateTime, DaDataFattura)
                addParametro(Cmd, "@ADataFattura", System.Data.SqlDbType.DateTime, ADataFattura)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            If RecuperaId Then
                sqlStr = "SELECT @@IDENTITY FROM Fatture_FiltroExport"
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    id = Cmd.ExecuteScalar
                End Using
            End If
        End Using

        Return id
    End Function

    Public Function UpdateStato() As Boolean
        UpdateStato = False

        Dim sqlStr As String
        sqlStr = "UPDATE Fatture_FiltroExport" &
            " SET stato = " & stato &
            " WHERE id = " & id

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Cmd.ExecuteNonQuery()
                UpdateStato = True
            End Using
        End Using
    End Function

    Private Shared Function FillFiltro(Rs As System.Data.SqlClient.SqlDataReader) As TipoFiltroFatture
        Dim mio_filtro As TipoFiltroFatture = New TipoFiltroFatture
        With mio_filtro
            .id = Rs("id")
            .tipo_fattura = Rs("tipo_fattura")
            .Annofatturazione = Rs("Annofatturazione")
            .DaNumFattura = getValueOrNohing(Rs("DaNumFattura"))
            .ANumfattura = getValueOrNohing(Rs("ANumFattura"))
            .DaDataFattura = getValueOrNohing(Rs("DaDataFattura"))
            .ADataFattura = getValueOrNohing(Rs("ADataFattura"))
            .stato = Rs("stato")
        End With
        Return mio_filtro
    End Function

    Public Shared Function getUltimoFiltroAperto(tipo_fattura As TipoFattura) As TipoFiltroFatture
        Dim mio_filtro As TipoFiltroFatture = Nothing

        Dim sqlStr As String
        sqlStr = "SELECT * FROM Fatture_FiltroExport WHERE tipo_fattura = " & tipo_fattura & " AND stato > " & StatoFiltro.Chiuso

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader()
                    If Rs.Read() Then
                        mio_filtro = FillFiltro(Rs)
                    End If
                End Using
            End Using
        End Using

        Return mio_filtro
    End Function
End Class

Public Class TipoCodiceFattura
    Public anno_fattura As Integer
    Public codice_fattura As Integer
    Public tipo_fattura As TipoFattura
    Public stato_fattura As TipoStatoFattura

    ' per comunicare con gli altri form:  ditte e prenotazioni...
    Public id_riferimento As Integer
    Public id_ditta As Integer
End Class

Public Class Fattura

    'CREATE TABLE [dbo].[Fatture](
    '	[id] [int] IDENTITY(1,1) NOT NULL,
    '   [attiva] [bit] default 1,
    '	[codice_fattura] [int] NULL,
    '	[anno_fattura] [int] NULL,
    '	[data_fattura] [datetime] NULL,
    '	[tipo_fattura] [int] NULL,
    '	[id_riferimento] [int] NULL,
    '	[id_ditta] [int] NULL,
    '	[Intestazione] [nvarchar](100) NULL,
    '	[Indirizzo] [nvarchar](50) NULL,
    '	[Citta] [nvarchar](50) NULL,
    '	[CAP] [varchar](10) NULL,
    '	[Provincia] [char](2) NULL,
    '	[Nazione] [char](3) NULL,
    '	[piva] [varchar](20) NULL,
    '	[codicefiscale] [varchar](20) NULL,
    '	[mail] [varchar](50) NULL,
    '	[id_modalita_pagamento] [int] NULL,
    '	[id_coordinata_bancaria] [int] NULL,
    '	[TotaleImponibile] [real] NULL,
    '	[TotaleIVA] [real] NULL,
    '	[id_utente] [int] NULL,
    '	[data_creazione] [datetime] NULL,
    ' CONSTRAINT [PK_Fatture] PRIMARY KEY CLUSTERED 
    '(
    '	[id] ASC
    ')WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
    ') ON [PRIMARY]

    Private m_id As Integer
    Private m_attiva As Boolean = True ' valore di default!
    Private m_codice_fattura As Integer
    Private m_anno_fattura As Integer
    Private m_data_fattura As DateTime
    Private m_tipo_fattura As Integer
    Private m_id_riferimento As Integer
    Private m_id_pagamento As Integer
    Private m_DescrizioneFattura As String
    Private m_id_ditta As Integer
    Private m_Intestazione As String
    Private m_Indirizzo As String
    Private m_Citta As String
    Private m_CAP As String
    Private m_Provincia As String
    Private m_Nazione As Integer
    Private m_piva As String
    Private m_codicefiscale As String
    Private m_mail As String
    Private m_pec As String
    Private m_codice_sdi As String
    Private m_id_modalita_pagamento As Integer
    Private m_id_coordinata_bancaria As Integer
    Private m_TotaleFattura As Double = 0
    Private m_TotaleImponibile As Double = 0
    Private m_TotaleIVA As Double = 0
    Private m_id_utente As Integer
    Private m_data_creazione As DateTime
    Private RigheFattura As New List(Of Fatture_riga)

    Public Class Fatture_riga
        Private m_id As Integer
        Private m_id_fattura As Integer
        Private m_Descrizione As String
        Private m_id_unita_misura As Integer
        Private m_quantita As Double
        Private m_Prezzo As Double
        Private m_TipoSconto As TipoScontoFattura
        Private m_Sconto As Double
        Private m_Imponibile As Double
        Private m_AliquotaIVA As Double
        Private m_id_iva As Integer?
        Private m_codice_coge As String

        Public Property id() As Integer
            Get
                Return m_id
            End Get
            Set(value As Integer)
                m_id = value
            End Set
        End Property

        Public Property id_fattura() As Integer
            Get
                Return m_id_fattura
            End Get
            Set(value As Integer)
                m_id_fattura = value
            End Set
        End Property

        Public Property Descrizione() As String
            Get
                Return m_Descrizione
            End Get
            Set(value As String)
                m_Descrizione = value
            End Set
        End Property

        Public Property id_unita_misura() As Integer
            Get
                Return m_id_unita_misura
            End Get
            Set(value As Integer)
                m_id_unita_misura = value
            End Set
        End Property

        Public Property quantita() As Double
            Get
                Return m_quantita
            End Get
            Set(value As Double)
                m_quantita = value
            End Set
        End Property

        Public Property Prezzo() As Double
            Get
                Return m_Prezzo
            End Get
            Set(value As Double)
                m_Prezzo = value
            End Set
        End Property

        Public Property TipoSconto() As TipoScontoFattura
            Get
                Return m_TipoSconto
            End Get
            Set(value As TipoScontoFattura)
                m_TipoSconto = value
            End Set
        End Property

        Public Property Sconto() As Double
            Get
                Return m_Sconto
            End Get
            Set(value As Double)
                m_Sconto = value
            End Set
        End Property

        Public ReadOnly Property Imponibile() As Double
            Get
                Return m_Imponibile
            End Get
        End Property

        Public Property AliquotaIVA() As Double
            Get
                Return m_AliquotaIVA
            End Get
            Set(value As Double)
                If id_iva IsNot Nothing Then
                    m_AliquotaIVA = Libreria.getAliquotaIVADaId(id_iva)
                Else
                    m_AliquotaIVA = value
                End If
            End Set
        End Property

        Public Property id_iva() As Integer?
            Get
                Return m_id_iva
            End Get
            Set(value As Integer?)
                m_id_iva = value
                If value IsNot Nothing Then
                    m_AliquotaIVA = Libreria.getAliquotaIVADaId(id_iva)
                End If
            End Set
        End Property

        Public Property codice_coge() As String
            Get
                Return m_codice_coge
            End Get
            Set(value As String)
                m_codice_coge = value
            End Set
        End Property

        Protected Shared Sub addParametro(Cmd As System.Data.SqlClient.SqlCommand, NomePar As String, Tipo As System.Data.SqlDbType, Valore As Object)
            If Valore Is DBNull.Value Or Valore Is Nothing Then
                'HttpContext.Current.Trace.Write("addParametro DBNull: " & NomePar & " - " & Tipo & " - " & Valore)
                Cmd.Parameters.Add(NomePar, Tipo).Value = DBNull.Value
            Else
                'HttpContext.Current.Trace.Write("addParametro Valore: " & NomePar & " - " & Tipo & " - " & Valore)
                Cmd.Parameters.Add(NomePar, Tipo).Value = Valore
            End If
        End Sub

        Protected Shared Function getValueOrNohing(Valore As Object) As Object
            If Valore Is DBNull.Value Then
                Return Nothing
            End If
            Return Valore
        End Function

        Protected Friend Function CalcolaImponibile() As Double
            With Me
                .m_Imponibile = .Prezzo * .quantita
                If .TipoSconto = TipoScontoFattura.Somma Then
                    .m_Imponibile -= .Sconto
                    .m_Imponibile = Libreria.ArrotondaDouble(.m_Imponibile)  ' troncamento ed arrotondamento!
                ElseIf .TipoSconto = TipoScontoFattura.Percentuale Then
                    .m_Imponibile = Libreria.ArrotondaDouble(.m_Imponibile * (1 - .Sconto / 100)) ' troncamento ed arrotondamento!
                End If
                Return .Imponibile
            End With
        End Function

        Public Function Save() As Integer
            '[id] [int] IDENTITY(1,1) NOT NULL,
            '[id_fattura] [int] NULL,
            '[Descrizione] [nvarchar](100) NULL,
            '[id_unita_misura] [int] NULL,
            '[quantita] [real] NULL,
            '[Prezzo] [real] NULL,
            '[TipoSconto] [int] NULL,
            '[Sconto] [real] NULL,
            '[Imponibile] [real] NULL,
            '[AliquotaIVA] [real] NULL,

            CalcolaImponibile()

            Dim sqlStr As String

            sqlStr = "INSERT INTO Fatture_riga (id_fattura,Descrizione,id_unita_misura,quantita,Prezzo,TipoSconto,Sconto,Imponibile,AliquotaIVA,id_iva,codice_coge)" &
                " VALUES (@id_fattura,@Descrizione,@id_unita_misura,@quantita,@Prezzo,@TipoSconto,@Sconto,@Imponibile,@AliquotaIVA,@id_iva,@codice_coge)"

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.Parameters.Add("@id_fattura", System.Data.SqlDbType.Int).Value = id_fattura
                    Cmd.Parameters.Add("@Descrizione", System.Data.SqlDbType.NVarChar).Value = Libreria.SubstringSicuro(Descrizione, 100)
                    Cmd.Parameters.Add("@id_unita_misura", System.Data.SqlDbType.Int).Value = id_unita_misura
                    Cmd.Parameters.Add("@quantita", System.Data.SqlDbType.Real).Value = quantita
                    Cmd.Parameters.Add("@Prezzo", System.Data.SqlDbType.Real).Value = Prezzo
                    Cmd.Parameters.Add("@TipoSconto", System.Data.SqlDbType.Int).Value = TipoSconto
                    Cmd.Parameters.Add("@Sconto", System.Data.SqlDbType.Real).Value = Sconto
                    Cmd.Parameters.Add("@Imponibile", System.Data.SqlDbType.Real).Value = Imponibile
                    If id_iva IsNot Nothing Then
                        AliquotaIVA = Libreria.getAliquotaIVADaId(id_iva)
                    End If
                    Cmd.Parameters.Add("@AliquotaIVA", System.Data.SqlDbType.Real).Value = AliquotaIVA
                    addParametro(Cmd, "@id_iva", System.Data.SqlDbType.Int, id_iva)
                    Cmd.Parameters.Add("@codice_coge", System.Data.SqlDbType.NVarChar).Value = Libreria.SubstringSicuro(codice_coge, 5)

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                End Using

                sqlStr = "SELECT @@IDENTITY FROM Fatture_riga"

                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Save = Cmd.ExecuteScalar
                End Using
            End Using
        End Function

        Private Shared Function FillRigaFattura(Rs As System.Data.SqlClient.SqlDataReader) As Fatture_riga
            Dim myRiga As New Fatture_riga

            With myRiga
                .id = Rs("id")
                .id_fattura = Rs("id_fattura")
                .Descrizione = Rs("Descrizione")
                .id_unita_misura = Rs("id_unita_misura")
                .quantita = Rs("quantita")
                .Prezzo = Rs("Prezzo")
                .TipoSconto = Rs("TipoSconto")
                .Sconto = Rs("Sconto")
                .m_Imponibile = Rs("Imponibile")
                '.AliquotaIVA = Rs("AliquotaIVA")
                .id_iva = getValueOrNohing(Rs("id_iva"))

                If IsDBNull(Rs("AliquotaIVA")) Then         'aggiunto salvo 24.05.2023
                    If Rs("id_iva") = "1" Then
                        .AliquotaIVA = "22"
                    End If
                Else
                    .AliquotaIVA = Rs("AliquotaIVA")
                End If

                .codice_coge = Rs("codice_coge") & ""
            End With

            Return myRiga
        End Function

        Protected Friend Shared Function getRigheFattura(id_fattura As Integer) As List(Of Fatture_riga)
            Dim myList As New List(Of Fatture_riga)

            Dim sqlStr As String = "Select * FROM Fatture_riga Where id_fattura = @id_fattura"
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.Parameters.Add("@id_fattura", System.Data.SqlDbType.Int).Value = id_fattura
                    Dbc.Open()
                    Using Rs = Cmd.ExecuteReader()
                        Do While Rs.Read()
                            Dim myRiga As Fatture_riga = FillRigaFattura(Rs)

                            myList.Add(myRiga)
                        Loop
                    End Using
                End Using
            End Using

            Return myList
        End Function

    End Class

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(value As Integer)
            m_id = value
        End Set
    End Property

    Public Property attiva() As Boolean
        Get
            Return m_attiva
        End Get
        Set(value As Boolean)
            m_attiva = value
        End Set
    End Property

    Public Property codice_fattura() As Integer
        Get
            Return m_codice_fattura
        End Get
        Set(value As Integer)
            m_codice_fattura = value
        End Set
    End Property

    Public Property anno_fattura() As Integer
        Get
            Return m_anno_fattura
        End Get
        Set(value As Integer)
            m_anno_fattura = value
        End Set
    End Property

    Public Property data_fattura() As DateTime
        Get
            Return m_data_fattura
        End Get
        Set(value As DateTime)
            m_data_fattura = value
        End Set
    End Property

    Public Property tipo_fattura() As Integer
        Get
            Return m_tipo_fattura
        End Get
        Set(value As Integer)
            m_tipo_fattura = value
        End Set
    End Property

    Public Property id_riferimento() As Integer
        Get
            Return m_id_riferimento
        End Get
        Set(value As Integer)
            m_id_riferimento = value
        End Set
    End Property

    Public Property id_pagamento() As Integer
        Get
            Return m_id_pagamento
        End Get
        Set(value As Integer)
            m_id_pagamento = value
        End Set
    End Property

    Public Property DescrizioneFattura() As String
        Get
            Return m_DescrizioneFattura
        End Get
        Set(value As String)
            m_DescrizioneFattura = value
        End Set
    End Property

    Public Property id_ditta() As Integer
        Get
            Return m_id_ditta
        End Get
        Set(value As Integer)
            m_id_ditta = value
        End Set
    End Property

    Public Property Intestazione() As String
        Get
            Return m_Intestazione
        End Get
        Set(value As String)
            m_Intestazione = value
        End Set
    End Property

    Public Property Indirizzo() As String
        Get
            Return m_Indirizzo
        End Get
        Set(value As String)
            m_Indirizzo = value
        End Set
    End Property

    Public Property Citta() As String
        Get
            Return m_Citta
        End Get
        Set(value As String)
            m_Citta = value
        End Set
    End Property

    Public Property CAP() As String
        Get
            Return m_CAP
        End Get
        Set(value As String)
            m_CAP = value
        End Set
    End Property

    Public Property Provincia() As String
        Get
            Return m_Provincia
        End Get
        Set(value As String)
            m_Provincia = value
        End Set
    End Property

    Public Property Nazione() As Integer
        Get
            Return m_Nazione
        End Get
        Set(value As Integer)
            m_Nazione = value
        End Set
    End Property

    Public Property piva() As String
        Get
            Return m_piva
        End Get
        Set(value As String)
            m_piva = value
        End Set
    End Property

    Public Property codicefiscale() As String
        Get
            Return m_codicefiscale
        End Get
        Set(value As String)
            m_codicefiscale = value
        End Set
    End Property

    Public Property mail() As String
        Get
            Return m_mail
        End Get
        Set(value As String)
            m_mail = value
        End Set
    End Property

    Public Property pec() As String
        Get
            Return m_pec
        End Get
        Set(value As String)
            m_pec = value
        End Set
    End Property

    Public Property codice_sdi() As String
        Get
            Return m_codice_sdi
        End Get
        Set(value As String)
            m_codice_sdi = value
        End Set
    End Property

    Public Property id_modalita_pagamento() As Integer
        Get
            Return m_id_modalita_pagamento
        End Get
        Set(value As Integer)
            m_id_modalita_pagamento = value
        End Set
    End Property

    Public Property id_coordinata_bancaria() As Integer
        Get
            Return m_id_coordinata_bancaria
        End Get
        Set(value As Integer)
            m_id_coordinata_bancaria = value
        End Set
    End Property

    Public ReadOnly Property TotaleFattura() As Double
        Get
            Return m_TotaleFattura
        End Get
    End Property

    Public ReadOnly Property TotaleImponibile() As Double
        Get
            Return m_TotaleImponibile
        End Get
    End Property

    Public ReadOnly Property TotaleIVA() As Double
        Get
            Return m_TotaleIVA
        End Get
    End Property

    Public ReadOnly Property id_utente() As Integer
        Get
            Return m_id_utente
        End Get
    End Property

    Public ReadOnly Property data_creazione() As DateTime
        Get
            Return m_data_creazione
        End Get
    End Property

    Public Function getRigheFattura() As List(Of Fatture_riga)
        Return Me.RigheFattura
    End Function

    Public Function addRigafattura(riga As Fatture_riga) As Fatture_riga
        Dim ImponibileRiga As Double = riga.CalcolaImponibile()
        m_TotaleImponibile += ImponibileRiga
        m_TotaleIVA += Libreria.ArrotondaDouble(ImponibileRiga * riga.AliquotaIVA / 100)

        Me.RigheFattura.Add(riga)
        Return riga
    End Function

    Public Sub AggiornaTotali()
        m_TotaleFattura = 0
        m_TotaleImponibile = 0
        m_TotaleIVA = 0

        For Each riga As Fatture_riga In RigheFattura
            With riga
                Dim ImponibileRiga As Double = .CalcolaImponibile()

                m_TotaleImponibile += ImponibileRiga
                m_TotaleIVA += ImponibileRiga * .AliquotaIVA / 100
            End With
        Next
        m_TotaleIVA = Libreria.ArrotondaDouble(m_TotaleIVA)
        m_TotaleFattura = m_TotaleImponibile + m_TotaleIVA
    End Sub

    Private Function DisabilitaFatturaPrecedente() As Boolean
        DisabilitaFatturaPrecedente = False
        Dim sqlStr As String

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            ' annullo eventualmente le fatture preesistenti con lo stesso codice fattura ancora attive (dovrebbe essere al più una sola)!
            sqlStr = "UPDATE Fatture SET attiva = 0" &
                " WHERE anno_fattura = @anno_fattura" &
                " AND tipo_fattura = @tipo_fattura" &
                " AND codice_fattura = @codice_fattura"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.Parameters.Add("@codice_fattura", System.Data.SqlDbType.Int).Value = codice_fattura
                Cmd.Parameters.Add("@tipo_fattura", System.Data.SqlDbType.Int).Value = tipo_fattura
                Cmd.Parameters.Add("@anno_fattura", System.Data.SqlDbType.Int).Value = anno_fattura

                Cmd.ExecuteScalar()
                DisabilitaFatturaPrecedente = True
            End Using
        End Using
    End Function

    Public Function Save() As Integer


        ' ATTENZIONE: una fattura è individuata univocamente da:
        ' 1) anno_fattura
        ' 2) tipo_fattura
        ' 3) codice_fattura
        ' 4) dallo stato della fattura (attiva = 1, NonAttiva = 0, Temporanea = -1)!

        Try
            AggiornaTotali()

            Dim sqlStr As String

            DisabilitaFatturaPrecedente()

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                ' -------------------------------------------------------------------------
                ' inserisco la nuova futtura
                sqlStr = "INSERT INTO Fatture (attiva,codice_fattura,anno_fattura,data_fattura,tipo_fattura,id_riferimento,id_pagamento,DescrizioneFattura,id_ditta,Intestazione,Indirizzo,Citta,CAP,Provincia,Nazione,piva,codicefiscale,mail,id_modalita_pagamento,id_coordinata_bancaria,TotaleImponibile,TotaleIVA,id_utente,data_creazione, email_pec, codice_sdi)" &
                    " VALUES (@attiva,@codice_fattura,@anno_fattura,@data_fattura,@tipo_fattura,@id_riferimento,@id_pagamento,@DescrizioneFattura,@id_ditta,@Intestazione,@Indirizzo,@Citta,@CAP,@Provincia,@Nazione,@piva,@codicefiscale,@mail,@id_modalita_pagamento,@id_coordinata_bancaria,@TotaleImponibile,@TotaleIVA,@id_utente,convert(datetime,getdate(),102), @email_pec, @codice_sdi)"

                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.Parameters.Add("@attiva", System.Data.SqlDbType.Int).Value = attiva
                    Cmd.Parameters.Add("@codice_fattura", System.Data.SqlDbType.Int).Value = codice_fattura
                    Cmd.Parameters.Add("@anno_fattura", System.Data.SqlDbType.Int).Value = anno_fattura
                    Cmd.Parameters.Add("@data_fattura", System.Data.SqlDbType.DateTime).Value = data_fattura
                    Cmd.Parameters.Add("@tipo_fattura", System.Data.SqlDbType.Int).Value = tipo_fattura
                    Cmd.Parameters.Add("@id_riferimento", System.Data.SqlDbType.Int).Value = id_riferimento
                    Cmd.Parameters.Add("@id_pagamento", System.Data.SqlDbType.Int).Value = id_pagamento
                    Cmd.Parameters.Add("@DescrizioneFattura", System.Data.SqlDbType.NVarChar).Value = Libreria.SubstringSicuro(DescrizioneFattura, 200)
                    Cmd.Parameters.Add("@id_ditta", System.Data.SqlDbType.Int).Value = id_ditta
                    Cmd.Parameters.Add("@Intestazione", System.Data.SqlDbType.NVarChar).Value = Libreria.SubstringSicuro(Intestazione, 100)
                    Cmd.Parameters.Add("@Indirizzo", System.Data.SqlDbType.NVarChar).Value = Libreria.SubstringSicuro(Indirizzo, 50)
                    Cmd.Parameters.Add("@Citta", System.Data.SqlDbType.NVarChar).Value = Libreria.SubstringSicuro(Citta, 50)
                    Cmd.Parameters.Add("@CAP", System.Data.SqlDbType.VarChar).Value = Libreria.SubstringSicuro(CAP, 10)
                    Cmd.Parameters.Add("@Provincia", System.Data.SqlDbType.Char).Value = Libreria.SubstringSicuro(Provincia, 2)
                    Cmd.Parameters.Add("@Nazione", System.Data.SqlDbType.Int).Value = Nazione
                    Cmd.Parameters.Add("@piva", System.Data.SqlDbType.VarChar).Value = Libreria.SubstringSicuro(piva, 20)
                    Cmd.Parameters.Add("@codicefiscale", System.Data.SqlDbType.VarChar).Value = Libreria.SubstringSicuro(codicefiscale, 20)
                    Cmd.Parameters.Add("@mail", System.Data.SqlDbType.VarChar).Value = Libreria.SubstringSicuro(mail, 50)
                    Cmd.Parameters.Add("@id_modalita_pagamento", System.Data.SqlDbType.Int).Value = id_modalita_pagamento
                    Cmd.Parameters.Add("@id_coordinata_bancaria", System.Data.SqlDbType.Int).Value = id_coordinata_bancaria
                    Cmd.Parameters.Add("@TotaleImponibile", System.Data.SqlDbType.Real).Value = TotaleImponibile
                    Cmd.Parameters.Add("@TotaleIVA", System.Data.SqlDbType.Real).Value = TotaleIVA
                    Cmd.Parameters.Add("@id_utente", System.Data.SqlDbType.Int).Value = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))
                    Cmd.Parameters.Add("@email_pec", System.Data.SqlDbType.NVarChar).Value = Libreria.SubstringSicuro(pec, 250)
                    Cmd.Parameters.Add("@codice_sdi", System.Data.SqlDbType.NVarChar).Value = Libreria.SubstringSicuro(codice_sdi, 50)

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                End Using

                ' -------------------------------------------------------------------------
                'recupero l'id del nuopvo record
                sqlStr = "SELECT @@IDENTITY FROM Fatture"

                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    m_id = Cmd.ExecuteScalar
                End Using
            End Using

            For Each riga As Fatture_riga In RigheFattura
                riga.id_fattura = m_id
                riga.Save()
            Next

            Return m_id
        Catch ex As Exception
            HttpContext.Current.Response.Write("error  Save : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try


    End Function

    Public Shared Function getFatturaDaId(id_fattura As Integer) As Fattura
        Dim m_Fattura As Fattura = Nothing
        Dim sqlStr As String = ""

        Try
            sqlStr = "Select * FROM Fatture Where id = @id"
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id_fattura
                    Dbc.Open()
                    Using Rs = Cmd.ExecuteReader()
                        If Rs.Read() Then
                            m_Fattura = fillFattura(Rs)
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            'Libreria.genUserMsgBox(Page, "Error: " & ex.Message & "<br/>" & sqlStr)
            HttpContext.Current.Response.Write("error  : <br/>" & ex.Message & "<br/>" & sqlStr & "<br/>")



        End Try

        Return m_Fattura
    End Function

    Public Shared Function getFatturaDaCodice(codice_fattura As Integer, anno_fattura As Integer, tipo_fattura As TipoFattura) As Fattura
        Dim m_Fattura As Fattura = Nothing
        Dim sqlStr As String

        sqlStr = "Select * FROM Fatture Where attiva = 1 AND codice_fattura = @codice_fattura AND anno_fattura = @anno_fattura AND tipo_fattura = @tipo_fattura"
        HttpContext.Current.Trace.Write(sqlStr)
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.Parameters.Add("@codice_fattura", System.Data.SqlDbType.Int).Value = codice_fattura
                Cmd.Parameters.Add("@anno_fattura", System.Data.SqlDbType.Int).Value = anno_fattura
                Cmd.Parameters.Add("@tipo_fattura", System.Data.SqlDbType.Int).Value = tipo_fattura
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader()
                    If Rs.Read() Then
                        m_Fattura = fillFattura(Rs)
                    End If
                End Using
            End Using
        End Using

        Return m_Fattura
    End Function

    Private Shared Function fillFattura(rs As System.Data.SqlClient.SqlDataReader) As Fattura
        Dim m_Fattura As Fattura = New Fattura
        Dim errorid As Integer = 0

        Try
            With m_Fattura

                .id = rs("id")
                .attiva = rs("attiva")
                .codice_fattura = rs("codice_fattura")
                errorid = 1
                .anno_fattura = rs("anno_fattura")
                .data_fattura = rs("data_fattura")
                .tipo_fattura = rs("tipo_fattura")
                .id_riferimento = rs("id_riferimento")
                errorid = 2
                .id_pagamento = rs("id_pagamento")
                .DescrizioneFattura = rs("DescrizioneFattura") & ""
                .id_ditta = rs("id_ditta")
                .Intestazione = rs("Intestazione")
                .Indirizzo = rs("Indirizzo")
                errorid = 3
                .Citta = rs("Citta")
                .CAP = rs("CAP")
                .Provincia = rs("Provincia")
                .Nazione = rs("Nazione")
                .piva = rs("piva")
                errorid = 4
                .codicefiscale = rs("codicefiscale")
                .mail = rs("mail")
                .pec = rs("email_pec") & ""
                .codice_sdi = rs("codice_sdi") & ""
                .id_modalita_pagamento = rs("id_modalita_pagamento")
                errorid = 5
                .id_coordinata_bancaria = rs("id_coordinata_bancaria")
                .m_TotaleImponibile = rs("TotaleImponibile")
                .m_TotaleIVA = rs("TotaleIVA")
                ' modifico i parametri privati per gli elementi solo ReadOnly
                .m_id_utente = rs("id_utente")
                .m_data_creazione = rs("data_creazione")
                errorid = 6

                .RigheFattura = Fatture_riga.getRigheFattura(.id)
            End With

        Catch ex As Exception
            HttpContext.Current.Response.Write("error  : <br/>" & ex.Message & "<br/>" & errorid & "<br/>")
        End Try


        Return m_Fattura
    End Function


End Class

Public Class Fatture_causali_coge
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_descrizione As String
    Protected m_codice_iva As Integer
    Protected m_prezzo As Double
    Protected m_codice_coge As String
    Protected m_codice_filiale As Boolean
    Protected m_gruppo_fattura As Integer
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
    Public Property descrizione() As String
        Get
            Return m_descrizione
        End Get
        Set(ByVal value As String)
            m_descrizione = value
        End Set
    End Property
    Public Property codice_iva() As Integer
        Get
            Return m_codice_iva
        End Get
        Set(ByVal value As Integer)
            m_codice_iva = value
        End Set
    End Property
    Public Property prezzo() As Double
        Get
            Return m_prezzo
        End Get
        Set(ByVal value As Double)
            m_prezzo = value
        End Set
    End Property
    Public Property codice_coge() As String
        Get
            Return m_codice_coge
        End Get
        Set(ByVal value As String)
            m_codice_coge = value
        End Set
    End Property
    Public Property codice_filiale() As Boolean
        Get
            Return m_codice_filiale
        End Get
        Set(ByVal value As Boolean)
            m_codice_filiale = value
        End Set
    End Property
    Public Property gruppo_fattura() As Integer
        Get
            Return m_gruppo_fattura
        End Get
        Set(ByVal value As Integer)
            m_gruppo_fattura = value
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


    Public Overrides Function SalvaRecord(Optional RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO Fatture_causali_coge (descrizione,codice_iva,prezzo,codice_coge,codice_filiale,gruppo_fattura,data_creazione,id_utente)" &
            " VALUES (@descrizione,@codice_iva,@prezzo,@codice_coge,@codice_filiale,@gruppo_fattura,@data_creazione,@id_utente)"

        m_data_creazione = Now
        m_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@codice_iva", System.Data.SqlDbType.Int, codice_iva)
                addParametro(Cmd, "@prezzo", System.Data.SqlDbType.Real, prezzo)
                addParametro(Cmd, "@codice_coge", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(codice_coge, 5))
                addParametro(Cmd, "@codice_filiale", System.Data.SqlDbType.Bit, codice_filiale)
                addParametro(Cmd, "@gruppo_fattura", System.Data.SqlDbType.Int, gruppo_fattura)
                addParametro(Cmd, "@data_creazione", System.Data.SqlDbType.DateTime, data_creazione)
                addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, id_utente)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM Fatture_causali_coge"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(Rs As System.Data.SqlClient.SqlDataReader) As Fatture_causali_coge
        Dim mio_record As Fatture_causali_coge = New Fatture_causali_coge
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .descrizione = getValueOrNohing(Rs("descrizione"))
            .codice_iva = getValueOrNohing(Rs("codice_iva"))
            .prezzo = getValueOrNohing(Rs("prezzo"))
            .codice_coge = getValueOrNohing(Rs("codice_coge"))
            .codice_filiale = getValueOrNohing(Rs("codice_filiale"))
            .gruppo_fattura = getValueOrNohing(Rs("gruppo_fattura"))
            .m_data_creazione = getValueOrNohing(Rs("data_creazione"))
            .m_id_utente = getValueOrNohing(Rs("id_utente"))
            .m_data_modifica = getValueOrNohing(Rs("data_modifica"))
            .m_id_utente_modifica = getValueOrNohing(Rs("id_utente_modifica"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(id_record As Integer) As Fatture_causali_coge
        Dim mio_record As Fatture_causali_coge = Nothing

        Dim sqlStr As String = "SELECT * FROM Fatture_causali_coge WITH(NOLOCK) WHERE id = " & id_record

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

        Dim sqlStr As String = "UPDATE Fatture_causali_coge SET" &
            " descrizione = @descrizione," &
            " codice_iva = @codice_iva," &
            " prezzo = @prezzo," &
            " codice_coge = @codice_coge," &
            " codice_filiale = @codice_filiale," &
            " gruppo_fattura = @gruppo_fattura," &
            " data_modifica = @data_modifica," &
            " id_utente_modifica = @id_utente_modifica" &
            " WHERE id = @id"

        m_data_modifica = Now
        m_id_utente_modifica = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))
                addParametro(Cmd, "@codice_iva", System.Data.SqlDbType.Int, codice_iva)
                addParametro(Cmd, "@prezzo", System.Data.SqlDbType.Real, prezzo)
                addParametro(Cmd, "@codice_coge", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(codice_coge, 5))
                addParametro(Cmd, "@codice_filiale", System.Data.SqlDbType.Bit, codice_filiale)
                addParametro(Cmd, "@gruppo_fattura", System.Data.SqlDbType.Int, gruppo_fattura)
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

    Public Shared Function EliminaRecord(id_record As Integer) As Boolean
        EliminaRecord = False

        Dim sqlStr As String = "DELETE FROM Fatture_causali_coge WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord Fatture_causali_coge: " & ex.Message)
        End Try
    End Function
End Class