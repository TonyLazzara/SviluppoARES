Imports System.Data
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports funzioni_comuni

' in fondo c'è anche la classe AllegatiMulte e la classe InvioMailMulte
Public Class Multe
    Inherits ITabellaDB
    Private _ID As Integer
    Private _Prot As Integer 'il protocollo si azzera ogni anno
    Private _Anno As Integer
    Private _DataInserimento As DateTime
    Private _UtenteID As Integer
    Private _ProvenienzaID As Integer
    Private _StatoAperto As Boolean 'pratica aperta o chiusa
    Private _EnteID As Integer
    Private _EnteIndirizzo As String
    Private _EnteComune As String
    Private _EnteCap As String
    Private _EnteProv As String

    '# inserite 19.10.2022 salvo
    Private _EnteEmail As String
    Private _EnteEmailPec As String
    Private _EnteTel As String
    Private _EnteNotes As String
    Private _EnteProtNotes As String
    '@ end 

    Private _NumVerbale As String
    Private _DataNotifica As DateTime
    Private _ArticoloCDS As Integer
    Private _MultaImporto As Double
    Private _Targa As String
    Private _DataInfrazione As DateTime
    Private _CasisticaID As Integer
    Private _IDConducente As Integer
    Private _CodCliente As Integer
    Private _ContrattoNolo As Integer
    Private _StazioneInizio As String
    Private _StazioneFine As String
    Private _DataInizioNolo As DateTime
    Private _DataFineNolo As DateTime
    Private _NumCartaCredito As String
    Private _ScadCartaCredito As String
    Private _AcquirenteMezzo As String
    Private _DataVenditaMezzo As DateTime
    Private _NumFattVendMezzo As Integer
    Private _CodClienteVendMezzo As Integer
    Private _AltroResponsMulta As String
    Private _NumDocAltroCaso As String
    Private _DataDocAltroCaso As DateTime
    Private _DescrizAltroCaso As String
    Private _Note As String
    Private _RicorsoYesNo As Boolean
    Private _RicorsoNumRacc As String
    Private _RicorsoDataRacc As DateTime
    Private _RicorsoDataFax As DateTime
    Private _ComunicazClienteYesNo As Boolean
    Private _DataMailCliente As DateTime
    Private _IncassatoYesNo As Boolean
    Private _NumTentativiIncassi As Integer
    Private _MotivoMancInc As Integer
    Private _RimborsatoYesNo As Boolean
    Private _RimborsatoData As DateTime
    Private _RimborsatoImporto As Double
    Private _PagatoYesNo As Boolean
    Private _PagatoData As DateTime
    Private _PagatoImporto As Double
    Private _FatturatoYesNo As Boolean
    Private _LocatoreId As Integer
    Private _LocatoreNumFatt As String
    Private _LocatoreDataFatt As DateTime

    Private _UltimoRecordInserito As Integer
    Dim funzioni As New funzioni_comuni
    Public Sub New()

    End Sub

    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property




    Public Property Prot() As Integer
        Get
            Return _Prot
        End Get
        Set(ByVal value As Integer)
            _Prot = value
        End Set
    End Property

    Public Property Anno() As Integer
        Get
            Return _Anno
        End Get
        Set(ByVal value As Integer)
            _Anno = value
        End Set
    End Property

    Public Property DataInserimento() As DateTime
        Get
            Return _DataInserimento
        End Get
        Set(ByVal value As DateTime)
            _DataInserimento = value
        End Set
    End Property

    Public Property UtenteID() As Integer
        Get
            Return _UtenteID
        End Get
        Set(ByVal value As Integer)
            _UtenteID = value
        End Set
    End Property

    Public Property ProvenienzaID() As Integer
        Get
            Return _ProvenienzaID
        End Get
        Set(ByVal value As Integer)
            _ProvenienzaID = value
        End Set
    End Property

    Public Property StatoAperto() As Boolean
        Get
            Return _StatoAperto
        End Get
        Set(ByVal value As Boolean)
            _StatoAperto = value
        End Set
    End Property

    Public Property EnteID() As Integer
        Get
            Return _EnteID
        End Get
        Set(ByVal value As Integer)
            _EnteID = value
        End Set
    End Property

    Public Property EnteIndirizzo() As String
        Get
            Return _EnteIndirizzo
        End Get
        Set(ByVal value As String)
            _EnteIndirizzo = value
        End Set
    End Property

    Public Property EnteComune() As String
        Get
            Return _EnteComune
        End Get
        Set(ByVal value As String)
            _EnteComune = value
        End Set
    End Property

    Public Property EnteCap() As String
        Get
            Return _EnteCap
        End Get
        Set(ByVal value As String)
            _EnteCap = value
        End Set
    End Property

    Public Property EnteProv() As String
        Get
            Return _EnteProv
        End Get
        Set(ByVal value As String)
            _EnteProv = value
        End Set
    End Property

    '# inserito 19.10.2022 salvo
    Public Property EnteEmail() As String
        Get
            Return _EnteEmail
        End Get
        Set(ByVal value As String)
            _EnteEmail = value
        End Set
    End Property
    Public Property EnteEmailPec() As String
        Get
            Return _EnteEmailPec
        End Get
        Set(ByVal value As String)
            _EnteEmailPec = value
        End Set
    End Property

    Public Property EnteTel() As String
        Get
            Return _EnteTel
        End Get
        Set(ByVal value As String)
            _EnteTel = value
        End Set
    End Property
    Public Property EnteProtNotes() As String
        Get
            Return _EnteProtNotes
        End Get
        Set(ByVal value As String)
            _EnteProtNotes = value
        End Set
    End Property

    Public Property EnteNotes() As String
        Get
            Return _EnteNotes
        End Get
        Set(ByVal value As String)
            _EnteNotes = value
        End Set
    End Property


    '@ end


    Public Property NumVerbale() As String
        Get
            Return _NumVerbale
        End Get
        Set(ByVal value As String)
            _NumVerbale = value
        End Set
    End Property

    Public Property DataNotifica() As DateTime
        Get
            Return _DataNotifica
        End Get
        Set(ByVal value As DateTime)
            _DataNotifica = value
        End Set
    End Property

    Public Property ArticoloCDS() As Integer
        Get
            Return _ArticoloCDS
        End Get
        Set(ByVal value As Integer)
            _ArticoloCDS = value
        End Set
    End Property

    Public Property MultaImporto() As Double
        Get
            Return _MultaImporto
        End Get
        Set(ByVal value As Double)
            _MultaImporto = value
        End Set
    End Property

    Public Property Targa() As String
        Get
            Return _Targa
        End Get
        Set(ByVal value As String)
            _Targa = value
        End Set
    End Property

    Public Property DataInfrazione() As DateTime
        Get
            Return _DataInfrazione
        End Get
        Set(ByVal value As DateTime)
            _DataInfrazione = value
        End Set
    End Property

    Public Property CasisticaID() As Integer
        Get
            Return _CasisticaID
        End Get
        Set(ByVal value As Integer)
            _CasisticaID = value
        End Set
    End Property

    Public Property IDConducente() As Integer
        Get
            Return _IDConducente
        End Get
        Set(ByVal value As Integer)
            _IDConducente = value
        End Set
    End Property

    Public Property CodCliente() As Integer
        Get
            Return _CodCliente
        End Get
        Set(ByVal value As Integer)
            _CodCliente = value
        End Set
    End Property
    Public Property ContrattoNolo() As Integer
        Get
            Return _ContrattoNolo
        End Get
        Set(ByVal value As Integer)
            _ContrattoNolo = value
        End Set
    End Property
    Public Property StazioneInizio() As String
        Get
            Return _StazioneInizio
        End Get
        Set(ByVal value As String)
            _StazioneInizio = value
        End Set
    End Property
    Public Property StazioneFine() As String
        Get
            Return _StazioneFine
        End Get
        Set(ByVal value As String)
            _StazioneFine = value
        End Set
    End Property
    Public Property DataInizioNolo() As DateTime
        Get
            Return _DataInizioNolo
        End Get
        Set(ByVal value As DateTime)
            _DataInizioNolo = value
        End Set
    End Property
    Public Property DataFineNolo() As DateTime
        Get
            Return _DataFineNolo
        End Get
        Set(ByVal value As DateTime)
            _DataFineNolo = value
        End Set
    End Property
    Public Property NumCartaCredito() As String
        Get
            Return _NumCartaCredito
        End Get
        Set(ByVal value As String)
            _NumCartaCredito = value
        End Set
    End Property
    Public Property ScadCartaCredito() As String
        Get
            Return _ScadCartaCredito
        End Get
        Set(ByVal value As String)
            _ScadCartaCredito = value
        End Set
    End Property
    Public Property AcquirenteMezzo() As String
        Get
            Return _AcquirenteMezzo
        End Get
        Set(ByVal value As String)
            _AcquirenteMezzo = value
        End Set
    End Property
    Public Property DataVenditaMezzo() As DateTime
        Get
            Return _DataVenditaMezzo
        End Get
        Set(ByVal value As DateTime)
            _DataVenditaMezzo = value
        End Set
    End Property
    Public Property NumFattVendMezzo() As Integer
        Get
            Return _NumFattVendMezzo
        End Get
        Set(ByVal value As Integer)
            _NumFattVendMezzo = value
        End Set
    End Property
    Public Property CodClienteVendMezzo() As Integer
        Get
            Return _CodClienteVendMezzo
        End Get
        Set(ByVal value As Integer)
            _CodClienteVendMezzo = value
        End Set
    End Property
    Public Property AltroResponsMulta() As String
        Get
            Return _AltroResponsMulta
        End Get
        Set(ByVal value As String)
            _AltroResponsMulta = value
        End Set
    End Property
    Public Property NumDocAltroCaso() As String
        Get
            Return _NumDocAltroCaso
        End Get
        Set(ByVal value As String)
            _NumDocAltroCaso = value
        End Set
    End Property
    Public Property DataDocAltroCaso() As DateTime
        Get
            Return _DataDocAltroCaso
        End Get
        Set(ByVal value As DateTime)
            _DataDocAltroCaso = value
        End Set
    End Property
    Public Property DescrizAltroCaso() As String
        Get
            Return _DescrizAltroCaso
        End Get
        Set(ByVal value As String)
            _DescrizAltroCaso = value
        End Set
    End Property

    Public Property Note() As String
        Get
            Return _Note
        End Get
        Set(ByVal value As String)
            _Note = value
        End Set
    End Property

    Public Property RicorsoYesNo() As Boolean
        Get
            Return _RicorsoYesNo
        End Get
        Set(ByVal value As Boolean)
            _RicorsoYesNo = value
        End Set
    End Property

    Public Property RicorsoNumRacc() As String
        Get
            Return _RicorsoNumRacc
        End Get
        Set(ByVal value As String)
            _RicorsoNumRacc = value
        End Set
    End Property

    Public Property RicorsoDataRacc() As DateTime
        Get
            Return _RicorsoDataRacc
        End Get
        Set(ByVal value As DateTime)
            _RicorsoDataRacc = value
        End Set
    End Property

    Public Property RicorsoDataFax() As DateTime
        Get
            Return _RicorsoDataFax
        End Get
        Set(ByVal value As DateTime)
            _RicorsoDataFax = value
        End Set
    End Property

    Public Property ComunicazClienteYesNo() As Boolean
        Get
            Return _ComunicazClienteYesNo
        End Get
        Set(ByVal value As Boolean)
            _ComunicazClienteYesNo = value
        End Set
    End Property

    Public Property DataMailCliente() As DateTime
        Get
            Return _DataMailCliente
        End Get
        Set(ByVal value As DateTime)
            _DataMailCliente = value
        End Set
    End Property

    Public Property IncassatoYesNo() As Boolean
        Get
            Return _IncassatoYesNo
        End Get
        Set(ByVal value As Boolean)
            _IncassatoYesNo = value
        End Set
    End Property

    Public Property NumTentativiIncassi() As Integer
        Get
            Return _NumTentativiIncassi
        End Get
        Set(ByVal value As Integer)
            _NumTentativiIncassi = value
        End Set
    End Property

    Public Property MotivoMancInc() As Integer
        Get
            Return _MotivoMancInc
        End Get
        Set(ByVal value As Integer)
            _MotivoMancInc = value
        End Set
    End Property

    Public Property RimborsatoYesNo() As Boolean
        Get
            Return _RimborsatoYesNo
        End Get
        Set(ByVal value As Boolean)
            _RimborsatoYesNo = value
        End Set
    End Property

    Public Property RimborsatoData() As DateTime
        Get
            Return _RimborsatoData
        End Get
        Set(ByVal value As DateTime)
            _RimborsatoData = value
        End Set
    End Property

    Public Property RimborsatoImporto() As Double
        Get
            Return _RimborsatoImporto
        End Get
        Set(ByVal value As Double)
            _RimborsatoImporto = value
        End Set
    End Property

    Public Property PagatoYesNo() As Boolean
        Get
            Return _PagatoYesNo
        End Get
        Set(ByVal value As Boolean)
            _PagatoYesNo = value
        End Set
    End Property

    Public Property PagatoData() As DateTime
        Get
            Return _PagatoData
        End Get
        Set(ByVal value As DateTime)
            _PagatoData = value
        End Set
    End Property

    Public Property PagatoImporto() As Double
        Get
            Return _PagatoImporto
        End Get
        Set(ByVal value As Double)
            _PagatoImporto = value
        End Set
    End Property

    Public Property FatturatoYesNo() As Boolean
        Get
            Return _FatturatoYesNo
        End Get
        Set(ByVal value As Boolean)
            _FatturatoYesNo = value
        End Set
    End Property

    Public Property LocatoreId() As Integer
        Get
            Return _LocatoreId
        End Get
        Set(ByVal value As Integer)
            _LocatoreId = value
        End Set
    End Property

    Public Property LocatoreNumFatt() As String
        Get
            Return _LocatoreNumFatt
        End Get
        Set(ByVal value As String)
            _LocatoreNumFatt = value
        End Set
    End Property

    Public Property LocatoreDataFatt() As DateTime
        Get
            Return _LocatoreDataFatt
        End Get
        Set(ByVal value As DateTime)
            _LocatoreDataFatt = value
        End Set
    End Property

    Public Property UltimoRecordInserito() As Integer
        Get
            Return _UltimoRecordInserito
        End Get
        Set(ByVal value As Integer)
            _UltimoRecordInserito = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO multe (ID,Prot,Anno,DataInserimento,UtenteID,ProvenienzaID,StatoAperto,EnteID,EnteIndirizzo," & _
            "EnteComune,EnteCap,EnteProv,NumVerbale,DataNotifica,ArticoloCDS,MultaImporto,Targa,DataInfrazione)" & _
            " VALUES (@ID,@Prot,@Anno,@DataInserimento,@UtenteID,@ProvenienzaID,@StatoAperto,@EnteID,@EnteIndirizzo," & _
            "@EnteComune,@EnteCap,@EnteProv,@NumVerbale,@DataNotifica,@ArticoloCDS,@MultaImporto,@Targa,@DataInfrazione)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                UltimoRecordInserito = GetMaxIdMulte()
                addParametro(Cmd, "@ID", System.Data.SqlDbType.Int, UltimoRecordInserito + 1)
                Anno = CInt(Year(CDate(DataNotifica)))
                Prot = GetMaxProt(Anno) + 1
                addParametro(Cmd, "@Prot", System.Data.SqlDbType.Int, Prot)
                addParametro(Cmd, "@Anno", System.Data.SqlDbType.Int, Anno)
                DataInserimento = Now
                'addParametro è una funzione che si trova in ITabellaDB che serve ad impostare i parametri in generale e nello
                'specifico prevede anche i parametri da impostare a null
                addParametro(Cmd, "@DataInserimento", System.Data.SqlDbType.DateTime, DataInserimento)
                addParametro(Cmd, "@UtenteID", System.Data.SqlDbType.Int, UtenteID)
                addParametro(Cmd, "@ProvenienzaID", System.Data.SqlDbType.Int, ProvenienzaID)
                addParametro(Cmd, "@StatoAperto", System.Data.SqlDbType.Bit, StatoAperto)
                addParametro(Cmd, "@EnteID", System.Data.SqlDbType.Int, EnteID)
                addParametro(Cmd, "@EnteIndirizzo", System.Data.SqlDbType.NVarChar, EnteIndirizzo)
                addParametro(Cmd, "@EnteComune", System.Data.SqlDbType.NVarChar, EnteComune)
                addParametro(Cmd, "@EnteCap", System.Data.SqlDbType.NVarChar, EnteCap)
                addParametro(Cmd, "@EnteProv", System.Data.SqlDbType.NVarChar, EnteProv)
                addParametro(Cmd, "@NumVerbale", System.Data.SqlDbType.NVarChar, NumVerbale)
                addParametro(Cmd, "@DataNotifica", System.Data.SqlDbType.DateTime, DataNotifica)
                addParametro(Cmd, "@ArticoloCDS", System.Data.SqlDbType.Int, ArticoloCDS)
                addParametro(Cmd, "@MultaImporto", System.Data.SqlDbType.Decimal, MultaImporto)
                addParametro(Cmd, "@Targa", System.Data.SqlDbType.NVarChar, Targa)
                addParametro(Cmd, "@DataInfrazione", System.Data.SqlDbType.DateTime, DataInfrazione)
                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            ''sqlStr = "SELECT @@IDENTITY FROM multe"

            'Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
            'UltimoRecordInserito = Cmd.ExecuteScalar
            'End Using
        End Using

        Return UltimoRecordInserito + 1

    End Function

    Public Function SalvaRecordVuoto(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO multe (ID,Prot,Anno,DataInserimento,UtenteID,StatoAperto) " & _
            " VALUES (@ID,@Prot,@Anno,@DataInserimento,@UtenteID,@StatoAperto)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                UltimoRecordInserito = GetMaxIdMulte()
                MyAddParametro(Cmd, "@ID", System.Data.SqlDbType.Int, UltimoRecordInserito + 1)
                DataInserimento = Now
                Anno = CInt(Year(CDate(DataInserimento)))
                Prot = GetMaxProt(Anno) + 1
                MyAddParametro(Cmd, "@Prot", System.Data.SqlDbType.Int, Prot)
                MyAddParametro(Cmd, "@Anno", System.Data.SqlDbType.Int, Anno)
                MyAddParametro(Cmd, "@DataInserimento", System.Data.SqlDbType.DateTime, DataInserimento)
                MyAddParametro(Cmd, "@UtenteID", System.Data.SqlDbType.Int, UtenteID)
                MyAddParametro(Cmd, "@StatoAperto", System.Data.SqlDbType.Bit, StatoAperto)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

        End Using

        Return UltimoRecordInserito + 1

    End Function

    Public Shared Function GenProtVuoti(ByVal UtenteID As Integer, ByVal quantita As Integer) As String
        Dim sqlStr As String = "INSERT INTO multe (ID,Prot,Anno,DataInserimento,UtenteID,StatoAperto) "
        sqlStr = sqlStr & "SELECT @ID + 1,@Prot + 1 ,@Anno,@DataInserimento,@UtenteID,@StatoAperto "
        For i = 1 To quantita
            If i > 1 Then
                sqlStr = sqlStr & "UNION ALL SELECT @ID + " & i & ",@Prot + " & i & ",@Anno,@DataInserimento,@UtenteID,@StatoAperto "
            End If
        Next

        Dim DataInserimento As DateTime = Now

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dim UltimoIdMulta As Integer = GetMaxIdMulte()
                MyAddParametro(Cmd, "@ID", System.Data.SqlDbType.Int, UltimoIdMulta)

                Dim anno As Integer = CInt(Year(CDate(DataInserimento)))
                Dim UltimoProt As Integer = GetMaxProt(anno)
                MyAddParametro(Cmd, "@Prot", System.Data.SqlDbType.Int, UltimoProt)
                MyAddParametro(Cmd, "@Anno", System.Data.SqlDbType.Int, anno)
                MyAddParametro(Cmd, "@DataInserimento", System.Data.SqlDbType.DateTime, DataInserimento)
                MyAddParametro(Cmd, "@UtenteID", System.Data.SqlDbType.Int, UtenteID)
                MyAddParametro(Cmd, "@StatoAperto", System.Data.SqlDbType.Bit, True)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

        End Using

        'adesso riprendo i primo e l'ultimo protocollo appena inseriti
        sqlStr = ""
        Dim PrimoProtInserito As Integer
        Dim UltimoProtInserito As Integer

        sqlStr = "SELECT MIN(Prot) as primoProt, MAX(Prot) as ultimoProt FROM multe WHERE DataInserimento=@DataInserimento"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                MyAddParametro(Cmd, "@DataInserimento", System.Data.SqlDbType.DateTime, DataInserimento)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader()
                    If Rs.Read() Then
                        PrimoProtInserito = Rs("primoProt")
                        UltimoProtInserito = Rs("ultimoProt")
                    End If
                End Using
            End Using
        End Using

        If PrimoProtInserito And UltimoProtInserito > 0 Then
            Return "dal n. " & CStr(PrimoProtInserito) & " al n. " & CStr(UltimoProtInserito)
        Else
            Return ""
        End If

    End Function

    Public Shared Function GetMaxIdMulte() As Integer
        Dim MyReader As SqlDataReader
        Using MyConnection As SqlConnection = New SqlConnection

            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString

            Using MyCommand As SqlCommand = New SqlCommand()
                MyCommand.CommandText = "SELECT MAX(ID) AS MaxId FROM multe"
                MyCommand.CommandType = CommandType.Text
                MyCommand.Connection = MyConnection

                MyCommand.Connection.Open()

                MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

                MyReader.Read()
                If MyReader.IsDBNull(0) Then
                    Return 0
                Else
                    Return MyReader("MaxId")
                End If

            End Using
        End Using
    End Function

    Public Shared Function GetMaxProt(ByVal ValoreAnno As Integer) As Integer
        Dim MyReader As SqlDataReader
        Using MyConnection As SqlConnection = New SqlConnection

            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString

            Using MyCommand As SqlCommand = New SqlCommand()
                MyCommand.CommandText = "SELECT MAX(Prot) AS MaxProt FROM multe where Anno=" & ValoreAnno
                MyCommand.CommandType = CommandType.Text
                MyCommand.Connection = MyConnection

                MyCommand.Connection.Open()

                MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

                MyReader.Read()
                If MyReader.IsDBNull(0) Then
                    Return 0
                Else
                    Return MyReader("MaxProt")
                End If

            End Using
        End Using
    End Function

    Public Shared Function UnisciDataOra(ByVal data As Date, ByVal ora As Integer, ByVal minuti As Integer) As DateTime
        Dim dataOra = DateAdd(DateInterval.Hour, ora, data)
        Return DateAdd(DateInterval.Minute, minuti, dataOra)
    End Function

    Public Function AggiornaRecord() As Boolean
        AggiornaRecord = False

        Dim sqlStr As String = "UPDATE multe SET" &
        " ProvenienzaID = @ProvenienzaID," &
        " StatoAperto = @StatoAperto," &
        " EnteID = @EnteID," &
        " EnteIndirizzo = @EnteIndirizzo," &
        " EnteComune = @EnteComune," &
        " EnteCap = @EnteCap," &
        " EnteProv = @EnteProv," &
        " NumVerbale = @NumVerbale," &
        " DataNotifica = @DataNotifica," &
        " ArticoloCDS = @ArticoloCDS," &
        " MultaImporto = @MultaImporto," &
        " Targa = @Targa," &
        " DataInfrazione = @DataInfrazione," &
        " CasisticaID = @CasisticaID," &
        " IDConducente = @IDConducente," &
        " CodCliente = @CodCliente," &
        " ContrattoNolo = @ContrattoNolo," &
        " StazioneInizio = @StazioneInizio," &
        " StazioneFine = @StazioneFine," &
        " DataInizioNolo = @DataInizioNolo," &
        " DataFineNolo = @DataFineNolo," &
        " NumCartaCredito = @NumCartaCredito," &
        " ScadCartaCredito = @ScadCartaCredito," &
        " AcquirenteMezzo = @AcquirenteMezzo," &
        " DataVenditaMezzo = @DataVenditaMezzo," &
        " NumFattVendMezzo = @NumFattVendMezzo," &
        " CodClienteVendMezzo = @CodClienteVendMezzo," &
        " AltroResponsMulta = @AltroResponsMulta," &
        " NumDocAltroCaso = @NumDocAltroCaso," &
        " DataDocAltroCaso = @DataDocAltroCaso," &
        " DescrizAltroCaso = @DescrizAltroCaso," &
        " Note = @Note," &
        " RicorsoYesNo = @RicorsoYesNo," &
        " RicorsoNumRacc = @RicorsoNumRacc," &
        " RicorsoDataRacc = @RicorsoDataRacc," &
        " RicorsoDataFax = @RicorsoDataFax," &
        " ComunicazClienteYesNo = @ComunicazClienteYesNo," &
        " DataMailCliente = @DataMailCliente," &
        " IncassatoYesNo = @IncassatoYesNo," &
        " NumTentativiIncassi = @NumTentativiIncassi," &
        " MotivoMancInc = @MotivoMancInc," &
        " RimborsatoYesNo = @RimborsatoYesNo," &
        " RimborsatoData = @RimborsatoData," &
        " RimborsatoImporto = @RimborsatoImporto," &
        " PagatoYesNo = @PagatoYesNo," &
        " PagatoData = @PagatoData," &
        " PagatoImporto = @PagatoImporto," &
        " FatturatoYesNo = @FatturatoYesNo," &
        " LocatoreId = @LocatoreId," &
        " LocatoreNumFatt = @LocatoreNumFatt," &
        " LocatoreDataFatt = @LocatoreDataFatt," &
        " ente_email = @enteEmail," &
        " ente_emailpec = @enteEmailpec," &
        " ente_tel = @entetel," &
        " ente_notes = @enteNotes" &
        " WHERE ID = @ID"

        'HttpContext.Current.Trace.Write("prima dell'update --------------------------------" & sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                'uso la funz MyAddParametro in locale e non quella utilizzata in ITabellaDB perchè personalizzata
                MyAddParametro(Cmd, "@ProvenienzaID", System.Data.SqlDbType.Int, ProvenienzaID)
                MyAddParametro(Cmd, "@StatoAperto", System.Data.SqlDbType.Bit, StatoAperto)
                MyAddParametro(Cmd, "@EnteID", System.Data.SqlDbType.Int, EnteID)
                MyAddParametro(Cmd, "@EnteIndirizzo", System.Data.SqlDbType.NVarChar, EnteIndirizzo)
                MyAddParametro(Cmd, "@EnteComune", System.Data.SqlDbType.NVarChar, EnteComune)
                MyAddParametro(Cmd, "@EnteCap", System.Data.SqlDbType.NVarChar, EnteCap)
                MyAddParametro(Cmd, "@EnteProv", System.Data.SqlDbType.NVarChar, EnteProv)
                MyAddParametro(Cmd, "@NumVerbale", System.Data.SqlDbType.NVarChar, NumVerbale)
                MyAddParametro(Cmd, "@DataNotifica", System.Data.SqlDbType.DateTime, getDataDb_con_orario2(DataNotifica))
                MyAddParametro(Cmd, "@ArticoloCDS", System.Data.SqlDbType.Int, ArticoloCDS)
                MyAddParametro(Cmd, "@MultaImporto", System.Data.SqlDbType.Decimal, MultaImporto)
                MyAddParametro(Cmd, "@Targa", System.Data.SqlDbType.NVarChar, Targa)
                MyAddParametro(Cmd, "@DataInfrazione", System.Data.SqlDbType.DateTime, getDataDb_con_orario2(DataInfrazione))
                MyAddParametro(Cmd, "@CasisticaID", System.Data.SqlDbType.Int, CasisticaID)
                MyAddParametro(Cmd, "@IDConducente", System.Data.SqlDbType.Int, IDConducente)
                MyAddParametro(Cmd, "@CodCliente", System.Data.SqlDbType.Int, CodCliente)
                MyAddParametro(Cmd, "@ContrattoNolo", System.Data.SqlDbType.Int, ContrattoNolo)
                MyAddParametro(Cmd, "@StazioneInizio", System.Data.SqlDbType.NVarChar, StazioneInizio)
                MyAddParametro(Cmd, "@StazioneFine", System.Data.SqlDbType.NVarChar, StazioneFine)
                MyAddParametro(Cmd, "@DataInizioNolo", System.Data.SqlDbType.DateTime, getDataDb_con_orario2(DataInizioNolo))
                MyAddParametro(Cmd, "@DataFineNolo", System.Data.SqlDbType.DateTime, getDataDb_con_orario2(DataFineNolo))
                MyAddParametro(Cmd, "@NumCartaCredito", System.Data.SqlDbType.NVarChar, NumCartaCredito)
                MyAddParametro(Cmd, "@ScadCartaCredito", System.Data.SqlDbType.NVarChar, ScadCartaCredito)
                MyAddParametro(Cmd, "@AcquirenteMezzo", System.Data.SqlDbType.NVarChar, AcquirenteMezzo)
                MyAddParametro(Cmd, "@DataVenditaMezzo", System.Data.SqlDbType.DateTime, getDataDb_con_orario2(DataVenditaMezzo))
                MyAddParametro(Cmd, "@NumFattVendMezzo", System.Data.SqlDbType.Int, NumFattVendMezzo)
                MyAddParametro(Cmd, "@CodClienteVendMezzo", System.Data.SqlDbType.Int, CodClienteVendMezzo)
                MyAddParametro(Cmd, "@AltroResponsMulta", System.Data.SqlDbType.NVarChar, AltroResponsMulta)
                MyAddParametro(Cmd, "@NumDocAltroCaso", System.Data.SqlDbType.NVarChar, NumDocAltroCaso)
                MyAddParametro(Cmd, "@DataDocAltroCaso", System.Data.SqlDbType.DateTime, getDataDb_con_orario2(DataDocAltroCaso))
                MyAddParametro(Cmd, "@DescrizAltroCaso", System.Data.SqlDbType.NVarChar, DescrizAltroCaso)
                MyAddParametro(Cmd, "@Note", System.Data.SqlDbType.NVarChar, Note)
                MyAddParametro(Cmd, "@RicorsoYesNo", System.Data.SqlDbType.Bit, RicorsoYesNo)
                MyAddParametro(Cmd, "@RicorsoNumRacc", System.Data.SqlDbType.NVarChar, RicorsoNumRacc)
                MyAddParametro(Cmd, "@RicorsoDataRacc", System.Data.SqlDbType.DateTime, getDataDb_con_orario2(RicorsoDataRacc))
                MyAddParametro(Cmd, "@RicorsoDataFax", System.Data.SqlDbType.DateTime, getDataDb_con_orario2(RicorsoDataFax))
                MyAddParametro(Cmd, "@ComunicazClienteYesNo", System.Data.SqlDbType.Bit, ComunicazClienteYesNo)
                MyAddParametro(Cmd, "@DataMailCliente", System.Data.SqlDbType.DateTime, getDataDb_con_orario2(DataMailCliente))
                MyAddParametro(Cmd, "@IncassatoYesNo", System.Data.SqlDbType.Bit, IncassatoYesNo)
                MyAddParametro(Cmd, "@NumTentativiIncassi", System.Data.SqlDbType.Int, NumTentativiIncassi)
                MyAddParametro(Cmd, "@MotivoMancInc", System.Data.SqlDbType.Int, MotivoMancInc)
                MyAddParametro(Cmd, "@RimborsatoYesNo", System.Data.SqlDbType.Bit, RimborsatoYesNo)
                MyAddParametro(Cmd, "@RimborsatoData", System.Data.SqlDbType.DateTime, getDataDb_con_orario2(RimborsatoData))
                MyAddParametro(Cmd, "@RimborsatoImporto", System.Data.SqlDbType.Decimal, RimborsatoImporto)
                MyAddParametro(Cmd, "@PagatoYesNo", System.Data.SqlDbType.Bit, PagatoYesNo)
                MyAddParametro(Cmd, "@PagatoData", System.Data.SqlDbType.DateTime, getDataDb_con_orario2(PagatoData))
                MyAddParametro(Cmd, "@PagatoImporto", System.Data.SqlDbType.Decimal, PagatoImporto)
                MyAddParametro(Cmd, "@FatturatoYesNo", System.Data.SqlDbType.Bit, FatturatoYesNo)
                MyAddParametro(Cmd, "@LocatoreId", System.Data.SqlDbType.Int, LocatoreId)
                MyAddParametro(Cmd, "@LocatoreNumFatt", System.Data.SqlDbType.NVarChar, LocatoreNumFatt)
                MyAddParametro(Cmd, "@LocatoreDataFatt", System.Data.SqlDbType.DateTime, getDataDb_con_orario2(LocatoreDataFatt))

                '# aggiunto 19.10.2022 salvo
                MyAddParametro(Cmd, "@EnteEmail", System.Data.SqlDbType.NVarChar, EnteEmail)
                MyAddParametro(Cmd, "@EnteEmailpec", System.Data.SqlDbType.NVarChar, EnteEmailPec)
                MyAddParametro(Cmd, "@EnteTel", System.Data.SqlDbType.NVarChar, EnteTel)
                MyAddParametro(Cmd, "@EnteNotes", System.Data.SqlDbType.NVarChar, EnteNotes)
                '@ end

                MyAddParametro(Cmd, "@ID", System.Data.SqlDbType.Int, ID)

                Trace.Write("Parametro " & Cmd.CommandText)

                Dbc.Open()
                Dim xi As Integer = Cmd.ExecuteNonQuery()
                AggiornaRecord = True


            End Using
        End Using


        'Dim sqlStr2 As String = "UPDATE multe SET ProvenienzaID = @ProvenienzaID, " & _
        '                                          "StatoAperto = @StatoAperto, " & _
        '                                          "EnteID = @EnteID, " & _
        '                                          "EnteIndirizzo = @EnteIndirizzo, " & _ 
        '                                          "EnteComune = @EnteComune, " & _ 
        '                                          "EnteCap = @EnteCap, " & _
        '                                          EnteProv = @EnteProv, NumVerbale = @NumVerbale, DataNotifica = @DataNotifica, ArticoloCDS = @ArticoloCDS, MultaImporto = @MultaImporto, Targa = @Targa, DataInfrazione = @DataInfrazione, CasisticaID = @CasisticaID, IDConducente = @IDConducente, CodCliente = @CodCliente, ContrattoNolo = @ContrattoNolo, StazioneInizio = @StazioneInizio, StazioneFine = @StazioneFine, DataInizioNolo = @DataInizioNolo, DataFineNolo = @DataFineNolo, NumCartaCredito = @NumCartaCredito, ScadCartaCredito = @ScadCartaCredito, AcquirenteMezzo = @AcquirenteMezzo, DataVenditaMezzo = @DataVenditaMezzo, NumFattVendMezzo = @NumFattVendMezzo, CodClienteVendMezzo = @CodClienteVendMezzo, AltroResponsMulta = @AltroResponsMulta, NumDocAltroCaso = @NumDocAltroCaso, DataDocAltroCaso = @DataDocAltroCaso, DescrizAltroCaso = @DescrizAltroCaso, Note = @Note, RicorsoYesNo = @RicorsoYesNo, RicorsoNumRacc = @RicorsoNumRacc, RicorsoDataRacc = @RicorsoDataRacc, RicorsoDataFax = @RicorsoDataFax, ComunicazClienteYesNo = @ComunicazClienteYesNo, DataMailCliente = @DataMailCliente, IncassatoYesNo = @IncassatoYesNo, NumTentativiIncassi = @NumTentativiIncassi, MotivoMancInc = @MotivoMancInc, RimborsatoYesNo = @RimborsatoYesNo, RimborsatoData = @RimborsatoData, RimborsatoImporto = @RimborsatoImporto, PagatoYesNo = @PagatoYesNo, PagatoData = @PagatoData, PagatoImporto = @PagatoImporto, FatturatoYesNo = @FatturatoYesNo, LocatoreId = @LocatoreId, LocatoreNumFatt = @LocatoreNumFatt, LocatoreDataFatt = @LocatoreDataFatt WHERE ID = @ID

        'Dim sqlStr2 As String = "UPDATE multe SET ProvenienzaID = @ProvenienzaID"
        'HttpContext.Current.Trace.Write("prima dell'update --------------------------------" & sqlStr2)        

        'Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        '    Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
        '        Dbc.Open()
        '        Cmd.ExecuteNonQuery()
        '        AggiornaRecord = True
        '    End Using
        'End Using
    End Function


    Protected Shared Sub MyAddParametro(ByVal Cmd As System.Data.SqlClient.SqlCommand, ByVal NomePar As String, ByVal Tipo As System.Data.SqlDbType, ByVal Valore As Object)

        If IsDate(Valore) AndAlso Valore = CDate("01/01/1800") Then 'utilizzato solo per le date
            Cmd.Parameters.Add(NomePar, Tipo).Value = DBNull.Value
        Else 'per tutti gli altri tipi di dati
            If Valore Is DBNull.Value Or Valore Is Nothing Then
                'HttpContext.Current.Trace.Write("addParametro DBNull: " & NomePar & " - " & Tipo & " - " & Valore)
                Cmd.Parameters.Add(NomePar, Tipo).Value = DBNull.Value
            Else
                'HttpContext.Current.Trace.Write("addParametro Valore: " & NomePar & " - " & Tipo & " - " & Valore)
                Cmd.Parameters.Add(NomePar, Tipo).Value = Valore
            End If
        End If
    End Sub

    Public Shared Function getFullRecordsMulte(ByVal idMulta As Integer) As Multe
        Dim mio_record As Multe = New Multe

        Dim sqlStr As String = ""

        sqlStr = "SELECT ID, Prot, Anno, DataInserimento, ProvenienzaID, StatoAperto, EnteID, EnteIndirizzo, "
        sqlStr = sqlStr & "EnteComune, EnteCap, EnteProv, NumVerbale, DataNotifica, ArticoloCDS, MultaImporto, Targa, "
        sqlStr = sqlStr & "DataInfrazione, CasisticaID, IDConducente, CodCliente, ContrattoNolo, StazioneInizio, StazioneFine, "
        sqlStr = sqlStr & "DataInizioNolo, DataFineNolo, NumCartaCredito, ScadCartaCredito, AcquirenteMezzo, DataVenditaMezzo, "
        sqlStr = sqlStr & "NumFattVendMezzo, CodClienteVendMezzo, AltroResponsMulta, NumDocAltroCaso, DataDocAltroCaso, "
        sqlStr = sqlStr & "DescrizAltroCaso, Note, RicorsoYesNo, RicorsoNumRacc, RicorsoDataRacc, RicorsoDataFax, "
        sqlStr = sqlStr & "ComunicazClienteYesNo, DataMailCliente, IncassatoYesNo, NumTentativiIncassi, MotivoMancInc, "
        sqlStr = sqlStr & "RimborsatoYesNo, RimborsatoData, RimborsatoImporto, PagatoYesNo, PagatoData, PagatoImporto, "
        sqlStr = sqlStr & "FatturatoYesNo, LocatoreId, LocatoreNumFatt, LocatoreDataFatt, "
        sqlStr = sqlStr & "ente_email, ente_emailpec, ente_tel, ente_notes "        'aggiunto 19.10.2022 salvo
        sqlStr = sqlStr & "FROM dbo.multe WITH(NOLOCK) WHERE (ID =" & idMulta & ")"

        'Modificata SQL per errata visualizzazione delle date OUT/IN 30.11.2021
        'recupera i valori direttamente dai dati del Contratto
        'sqlStr = "Select Multe.ID, multe.Prot, multe.Anno, multe.DataInserimento, multe.ProvenienzaID, multe.StatoAperto, multe.EnteID, multe.EnteIndirizzo, multe.EnteComune, multe.EnteCap, multe.EnteProv, multe.NumVerbale, multe.DataNotifica, "
        'sqlStr += "Multe.ArticoloCDS, Multe.MultaImporto, Multe.Targa, Multe.DataInfrazione, Multe.CasisticaID, Multe.IDConducente, Multe.CodCliente,"
        'sqlStr += "Multe.ContrattoNolo, Multe.NumCartaCredito, Multe.ScadCartaCredito, Multe.AcquirenteMezzo, Multe.DataVenditaMezzo, Multe.NumFattVendMezzo, Multe.CodClienteVendMezzo, Multe.AltroResponsMulta,"
        'sqlStr += "Multe.NumDocAltroCaso, Multe.DataDocAltroCaso, Multe.DescrizAltroCaso, Multe.Note, Multe.RicorsoYesNo, Multe.RicorsoNumRacc, Multe.RicorsoDataRacc, Multe.RicorsoDataFax, Multe.ComunicazClienteYesNo,"
        'sqlStr += "Multe.DataMailCliente, Multe.IncassatoYesNo, Multe.NumTentativiIncassi, Multe.MotivoMancInc, Multe.RimborsatoYesNo, Multe.RimborsatoData, Multe.RimborsatoImporto, Multe.PagatoYesNo, Multe.PagatoData,"
        'sqlStr += "Multe.PagatoImporto, Multe.FatturatoYesNo, Multe.LocatoreId, Multe.LocatoreNumFatt, Multe.LocatoreDataFatt,"
        'sqlStr += "contratti.data_uscita As DataInizioNolo, contratti.data_rientro As DataFineNolo, stazioni.nome_stazione As StazioneInizio, stazioni_1.nome_stazione AS StazioneFine "
        'sqlStr += "From multe WITH (NOLOCK) INNER Join contratti On multe.ContrattoNolo = contratti.num_contratto INNER Join stazioni On contratti.id_stazione_uscita = stazioni.id INNER Join "
        'sqlStr += "stazioni As stazioni_1 On contratti.id_stazione_rientro = stazioni_1.id "
        'sqlStr += "Where (multe.ID = " & idMulta & ") And (contratti.attivo = 1)"


        'HttpContext.Current.Trace.Write("---------------------------------" & sqlStr)

        'recupera i valori dalla tabella Multe


        Dim EnteID As String = ""

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    If Rs.Read Then
                        With mio_record
                            .ID = getValueOrNohing(Rs("ID"))
                            .Prot = getValueOrNohing(Rs("Prot"))
                            .Anno = getValueOrNohing(Rs("Anno"))
                            .DataInserimento = getValueOrNohing(Rs("DataInserimento"))
                            .ProvenienzaID = getValueOrNohing(Rs("ProvenienzaID"))
                            .StatoAperto = getValueOrNohing(Rs("StatoAperto"))
                            .EnteID = getValueOrNohing(Rs("EnteID"))
                            EnteID = getValueOrNohing(Rs("EnteID"))                     'aggiunto salvo 24.11.2022
                            .EnteIndirizzo = getValueOrNohing(Rs("EnteIndirizzo"))
                            .EnteComune = getValueOrNohing(Rs("EnteComune"))
                            .EnteCap = getValueOrNohing(Rs("EnteCap"))
                            .EnteProv = getValueOrNohing(Rs("EnteProv"))
                            .NumVerbale = getValueOrNohing(Rs("NumVerbale"))
                            .DataNotifica = getValueOrNohing(Rs("DataNotifica"))
                            .ArticoloCDS = getValueOrNohing(Rs("ArticoloCDS"))
                            .MultaImporto = getValueOrNohing(Rs("MultaImporto"))
                            .Targa = getValueOrNohing(Rs("Targa"))
                            .DataInfrazione = getValueOrNohing(Rs("DataInfrazione"))
                            .CasisticaID = getValueOrNohing(Rs("CasisticaID"))
                            .IDConducente = getValueOrNohing(Rs("IDConducente"))
                            .CodCliente = getValueOrNohing(Rs("CodCliente"))
                            .ContrattoNolo = getValueOrNohing(Rs("ContrattoNolo"))
                            .StazioneInizio = getValueOrNohing(Rs("StazioneInizio"))
                            '.StazioneInizio = getValueOrNohing(Rs("StazioneInizio"))
                            .StazioneFine = getValueOrNohing(Rs("StazioneFine"))
                            .DataInizioNolo = getValueOrNohing(Rs("DataInizioNolo"))
                            .DataFineNolo = getValueOrNohing(Rs("DataFineNolo"))
                            .NumCartaCredito = getValueOrNohing(Rs("NumCartaCredito"))
                            .ScadCartaCredito = getValueOrNohing(Rs("ScadCartaCredito"))
                            .AcquirenteMezzo = getValueOrNohing(Rs("AcquirenteMezzo"))
                            .DataVenditaMezzo = getValueOrNohing(Rs("DataVenditaMezzo"))
                            .NumFattVendMezzo = getValueOrNohing(Rs("NumFattVendMezzo"))
                            .CodClienteVendMezzo = getValueOrNohing(Rs("CodClienteVendMezzo"))
                            .AltroResponsMulta = getValueOrNohing(Rs("AltroResponsMulta"))
                            .NumDocAltroCaso = getValueOrNohing(Rs("NumDocAltroCaso"))
                            .DataDocAltroCaso = getValueOrNohing(Rs("DataDocAltroCaso"))
                            .DescrizAltroCaso = getValueOrNohing(Rs("DescrizAltroCaso"))
                            .Note = getValueOrNohing(Rs("Note"))
                            .RicorsoYesNo = getValueOrNohing(Rs("RicorsoYesNo"))
                            .RicorsoNumRacc = getValueOrNohing(Rs("RicorsoNumRacc"))
                            .RicorsoDataRacc = getValueOrNohing(Rs("RicorsoDataRacc"))
                            .RicorsoDataFax = getValueOrNohing(Rs("RicorsoDataFax"))
                            .ComunicazClienteYesNo = getValueOrNohing(Rs("ComunicazClienteYesNo"))
                            .DataMailCliente = getValueOrNohing(Rs("DataMailCliente"))
                            .IncassatoYesNo = getValueOrNohing(Rs("IncassatoYesNo"))
                            .NumTentativiIncassi = getValueOrNohing(Rs("NumTentativiIncassi"))
                            .MotivoMancInc = getValueOrNohing(Rs("MotivoMancInc"))
                            .RimborsatoYesNo = getValueOrNohing(Rs("RimborsatoYesNo"))
                            .RimborsatoData = getValueOrNohing(Rs("RimborsatoData"))
                            .RimborsatoImporto = getValueOrNohing(Rs("RimborsatoImporto"))
                            .PagatoYesNo = getValueOrNohing(Rs("PagatoYesNo"))
                            .PagatoData = getValueOrNohing(Rs("PagatoData"))
                            .PagatoImporto = getValueOrNohing(Rs("PagatoImporto"))
                            .FatturatoYesNo = getValueOrNohing(Rs("FatturatoYesNo"))
                            .LocatoreId = getValueOrNohing(Rs("LocatoreId"))
                            .LocatoreNumFatt = getValueOrNohing(Rs("LocatoreNumFatt"))
                            .LocatoreDataFatt = getValueOrNohing(Rs("LocatoreDataFatt"))

                            'aggiunto 19.10.2022 salvo
                            .EnteEmail = getValueOrNohing(Rs("ente_email"))
                            .EnteEmailPec = getValueOrNohing(Rs("ente_emailpec"))
                            .EnteTel = getValueOrNohing(Rs("ente_tel"))
                            .EnteProtNotes = getValueOrNohing(Rs("ente_notes"))


                            'se dati mancanti recupera da ente salvo 24.11.2022 modificato 20.12.2022
                            Dim dati_ente() As String = funzioni_comuni_new.GetDatiEnteMulte(EnteID)
                            'il valore note dell'ente viene visualizzato nel campo Ente Note - salvo 20.12.2022
                            'If .EnteTel = "" Then
                            '    .EnteTel = dati_ente(0)
                            'End If
                            'If .EnteEmail = "" Then
                            '    .EnteEmail = dati_ente(1)
                            'End If
                            'If .EnteEmailPec = "" Then
                            '    .EnteEmailPec = dati_ente(2)
                            'End If
                            If dati_ente(3) <> "" Then
                                .EnteNotes = dati_ente(3)       'carica note dell'ente
                            End If

                        End With
                    End If

                End Using
            End Using
        End Using

        Return mio_record
    End Function
End Class

Public Class AllegatiMulte
    Private _id As Integer
    Private _DataCreazione As DateTime
    Private _IdTipoDocumento As Integer
    Private _NomeFile As String
    Private _PercorsoFile As String
    Private _IdMulta As Integer

    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Property DataCreazione() As DateTime
        Get
            Return _DataCreazione
        End Get
        Set(ByVal value As DateTime)
            _DataCreazione = value
        End Set
    End Property

    Public Property IdTipoDocumento() As Integer
        Get
            Return _IdTipoDocumento
        End Get
        Set(ByVal value As Integer)
            _IdTipoDocumento = value
        End Set
    End Property

    Public Property NomeFile() As String
        Get
            Return _NomeFile
        End Get
        Set(ByVal value As String)
            _NomeFile = value
        End Set
    End Property

    Public Property PercorsoFile() As String
        Get
            Return _PercorsoFile
        End Get
        Set(ByVal value As String)
            _PercorsoFile = value
        End Set
    End Property

    Public Property IdMulta() As Integer
        Get
            Return _IdMulta
        End Get
        Set(ByVal value As Integer)
            _IdMulta = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub InsertAllegatoMulta()

        Dim id_operatore As String = HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente")


        Dim sqlStr As String = "INSERT INTO multe_Allegati (Id,DataCreazione,IdTipoDocumento,NomeFile,PercorsoFile,IdMulta,id_operatore) " &
            " VALUES (@Id,@DataCreazione,@IdTipoDocumento,@NomeFile,@PercorsoFile,@IdMulta,'" & id_operatore & "')"
        'HttpContext.Current.Trace.Write("--------------------------- sql insert: " & sqlStr)
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = GetMaxIdAllegati() + 1
                Cmd.Parameters.Add("@DataCreazione", System.Data.SqlDbType.DateTime).Value = DataCreazione
                Cmd.Parameters.Add("@IdTipoDocumento", System.Data.SqlDbType.Int).Value = IdTipoDocumento
                Cmd.Parameters.Add("@NomeFile", System.Data.SqlDbType.NVarChar).Value = NomeFile
                Cmd.Parameters.Add("@PercorsoFile", System.Data.SqlDbType.NVarChar).Value = PercorsoFile
                Cmd.Parameters.Add("@IdMulta", System.Data.SqlDbType.Int).Value = IdMulta
                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    Public Sub DeleteAllegatoMulta()
        Dim sqlStr As String = "DELETE FROM multe_Allegati WHERE Id = @id"
        'HttpContext.Current.Trace.Write("--------------------------- sql insert: " & sqlStr)
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = id
                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    Public Shared Function GetMaxIdAllegati() As Integer
        Dim MyReader As SqlDataReader
        Using MyConnection As SqlConnection = New SqlConnection

            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString

            Using MyCommand As SqlCommand = New SqlCommand()
                MyCommand.CommandText = "SELECT MAX(ID) AS MaxId FROM multe_Allegati"
                MyCommand.CommandType = CommandType.Text
                MyCommand.Connection = MyConnection

                MyCommand.Connection.Open()

                MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

                MyReader.Read()
                If MyReader.IsDBNull(0) Then
                    Return 0
                Else
                    Return MyReader("MaxId")
                End If

            End Using
        End Using
    End Function

    Public Function VerificaFileSeImportatoPrima(ByVal file As String) As Boolean
        If file = "" Then Return False

        Dim strRd As String = "SELECT id FROM multe_Allegati WHERE NomeFile='" & file & "'"

        Dim MyReader As SqlDataReader
        Dim MyConnection As SqlConnection = New SqlConnection
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString
        Using MyCommand As SqlCommand = New SqlCommand()
            MyCommand.CommandText = strRd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection

            MyCommand.Connection.Open()
            MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

            If MyReader.HasRows Then
                Return True
            Else
                Return False
            End If
        End Using

    End Function
End Class

Public Class InvioMailMulte
    Private _idMail As Integer
    Private _DataInvio As DateTime
    Private _Destinatario As String
    Private _PerConoscenza As String
    Private _Oggetto As String
    Private _Testo As String
    Private _UtenteId As Integer
    Private _IdMulta As Integer

    Public Property idMail() As Integer
        Get
            Return _idMail
        End Get
        Set(ByVal value As Integer)
            _idMail = value
        End Set
    End Property

    Public Property DataInvio() As DateTime
        Get
            Return _DataInvio
        End Get
        Set(ByVal value As DateTime)
            _DataInvio = value
        End Set
    End Property

    Public Property Destinatario() As String
        Get
            Return _Destinatario
        End Get
        Set(ByVal value As String)
            _Destinatario = value
        End Set
    End Property

    Public Property PerConoscenza() As String
        Get
            Return _PerConoscenza
        End Get
        Set(ByVal value As String)
            _PerConoscenza = value
        End Set
    End Property

    Public Property Oggetto() As String
        Get
            Return _Oggetto
        End Get
        Set(ByVal value As String)
            _Oggetto = value
        End Set
    End Property

    Public Property Testo() As String
        Get
            Return _Testo
        End Get
        Set(ByVal value As String)
            _Testo = value
        End Set
    End Property

    Public Property UtenteId() As Integer
        Get
            Return _UtenteId
        End Get
        Set(ByVal value As Integer)
            _UtenteId = value
        End Set
    End Property

    Public Property IdMulta() As Integer
        Get
            Return _IdMulta
        End Get
        Set(ByVal value As Integer)
            _IdMulta = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub InsertInvioMailMulte(ByVal List_allegati As Generic.List(Of AllegatiMail))
        Dim sqlStr As String = "INSERT INTO multe_InvioMail (idMail,DataInvio,Destinatario,PerConoscenza,Oggetto,Testo,UtenteId,IdMulta) " & _
            " VALUES (@idMail,@DataInvio,@Destinatario,@PerConoscenza,@Oggetto,@Testo,@UtenteId,@IdMulta)"
        'HttpContext.Current.Trace.Write("--------------------------- sql insert: " & sqlStr)

        Dim id_mail As Integer = GetMaxInvioMail() + 1

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.Parameters.Add("@idMail", System.Data.SqlDbType.Int).Value = id_mail
                Cmd.Parameters.Add("@DataInvio", System.Data.SqlDbType.DateTime).Value = DataInvio
                Cmd.Parameters.Add("@Destinatario", System.Data.SqlDbType.NVarChar).Value = Destinatario
                Cmd.Parameters.Add("@PerConoscenza", System.Data.SqlDbType.NVarChar).Value = PerConoscenza
                Cmd.Parameters.Add("@Oggetto", System.Data.SqlDbType.NVarChar).Value = Oggetto
                Cmd.Parameters.Add("@Testo", System.Data.SqlDbType.NVarChar).Value = Testo
                Cmd.Parameters.Add("@UtenteId", System.Data.SqlDbType.Int).Value = UtenteId
                Cmd.Parameters.Add("@IdMulta", System.Data.SqlDbType.Int).Value = IdMulta
                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using

        For Each ElencoAllegati In List_allegati
            InsertAllegatoMail(ElencoAllegati.FileAllegatoMail, ElencoAllegati.PercorsoAllegatoMail, id_mail)
            'HttpContext.Current.Trace.Write("------------------------ file: " & ElencoAllegati.FileAllegatoMail)
            'HttpContext.Current.Trace.Write("------------------------ perc: " & ElencoAllegati.PercorsoAllegatoMail)
        Next
    End Sub

    Public Shared Sub InsertAllegatoMail(ByVal file As String, ByVal percorso As String, ByVal idMail As Integer)
        Dim sqlStr As String = "INSERT INTO multe_AllegatiMail (Id, NomeFile, PercorsoFile, idMail) " & _
            " VALUES (@Id, @NomeFile, @PercorsoFile, @idMail)"
        'HttpContext.Current.Trace.Write("--------------------------- sql insert: " & sqlStr)
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = GetMaxAllegatoMail() + 1
                Cmd.Parameters.Add("@NomeFile", System.Data.SqlDbType.NVarChar).Value = file
                Cmd.Parameters.Add("@PercorsoFile", System.Data.SqlDbType.NVarChar).Value = percorso
                Cmd.Parameters.Add("@idMail", System.Data.SqlDbType.Int).Value = idMail
                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using

    End Sub

    Public Shared Function GetMaxInvioMail() As Integer
        Dim MyReader As SqlDataReader
        Using MyConnection As SqlConnection = New SqlConnection

            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString

            Using MyCommand As SqlCommand = New SqlCommand()
                MyCommand.CommandText = "SELECT MAX(idMail) AS MaxIdMail FROM multe_InvioMail"
                MyCommand.CommandType = CommandType.Text
                MyCommand.Connection = MyConnection

                MyCommand.Connection.Open()

                MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

                MyReader.Read()
                If MyReader.IsDBNull(0) Then
                    Return 0
                Else
                    Return MyReader("MaxIdMail")
                End If

            End Using
        End Using
    End Function

    Public Shared Function GetMaxAllegatoMail() As Integer
        Dim MyReader As SqlDataReader
        Using MyConnection As SqlConnection = New SqlConnection

            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString

            Using MyCommand As SqlCommand = New SqlCommand()
                MyCommand.CommandText = "SELECT MAX(Id) AS MaxIdAllegatoMail FROM multe_AllegatiMail"
                MyCommand.CommandType = CommandType.Text
                MyCommand.Connection = MyConnection

                MyCommand.Connection.Open()

                MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

                MyReader.Read()
                If MyReader.IsDBNull(0) Then
                    Return 0
                Else
                    Return MyReader("MaxIdAllegatoMail")
                End If

            End Using
        End Using
    End Function
    Public Class AllegatiMail
        Private _FileAllegatoMail As String
        Private _PercorsoAllegatoMail As String

        Public Property FileAllegatoMail() As String
            Get
                Return _FileAllegatoMail
            End Get
            Set(ByVal value As String)
                _FileAllegatoMail = value
            End Set
        End Property

        Public Property PercorsoAllegatoMail() As String
            Get
                Return _PercorsoAllegatoMail
            End Get
            Set(ByVal value As String)
                _PercorsoAllegatoMail = value
            End Set
        End Property

        Public Sub New(ByVal file As String, ByVal percorso As String)
            MyBase.New()
            _FileAllegatoMail = file
            _PercorsoAllegatoMail = percorso
        End Sub

    End Class
End Class

Public Class FiltroReports
    Private _id As Integer
    Private _Query As String
    Private _DaId As String
    Private _AId As String
    Private _DaProt As String
    Private _AProt As String
    Private _DaAnno As String
    Private _AAnno As String
    Private _DaDataInserimento As String
    Private _ADataInserimento As String
    Private _DaNumVerbale As String
    Private _ANumVerbale As String
    Private _DaDataNotifica As String
    Private _ADataNotifica As String
    Private _DaTarga As String
    Private _ATarga As String
    Private _DaDataInfrazione As String
    Private _ADataInfrazione As String
    Private _DaOraInfrazione As String
    Private _AOraInfrazione As String
    Private _StatoMulta As String
    Private _Casistica As String
    Private _Incassate As String
    Private _MotivoMancatoIncasso As String
    Private _Fatturate As String
    Private _Ricorso As String
    Private _ComunCliente As String
    Private _Fax As String
    Private _ConFattLocatori As String
    Private _Rimborsate As String
    Private _Pagate As String
    Private _Operatori As String

    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Property Query() As String
        Get
            Return _Query
        End Get
        Set(ByVal value As String)
            _Query = value
        End Set
    End Property

    Public Property DaId() As String
        Get
            Return _DaId
        End Get
        Set(ByVal value As String)
            _DaId = value
        End Set
    End Property

    Public Property AId() As String
        Get
            Return _AId
        End Get
        Set(ByVal value As String)
            _AId = value
        End Set
    End Property

    Public Property DaProt() As String
        Get
            Return _DaProt
        End Get
        Set(ByVal value As String)
            _DaProt = value
        End Set
    End Property

    Public Property AProt() As String
        Get
            Return _AProt
        End Get
        Set(ByVal value As String)
            _AProt = value
        End Set
    End Property

    Public Property DaAnno() As String
        Get
            Return _DaAnno
        End Get
        Set(ByVal value As String)
            _DaAnno = value
        End Set
    End Property

    Public Property AAnno() As String
        Get
            Return _AAnno
        End Get
        Set(ByVal value As String)
            _AAnno = value
        End Set
    End Property

    Public Property DaDataInserimento() As String
        Get
            Return _DaDataInserimento
        End Get
        Set(ByVal value As String)
            _DaDataInserimento = value
        End Set
    End Property

    Public Property ADataInserimento() As String
        Get
            Return _ADataInserimento
        End Get
        Set(ByVal value As String)
            _ADataInserimento = value
        End Set
    End Property

    Public Property DaNumVerbale() As String
        Get
            Return _DaNumVerbale
        End Get
        Set(ByVal value As String)
            _DaNumVerbale = value
        End Set
    End Property

    Public Property ANumVerbale() As String
        Get
            Return _ANumVerbale
        End Get
        Set(ByVal value As String)
            _ANumVerbale = value
        End Set
    End Property

    Public Property DaDataNotifica() As String
        Get
            Return _DaDataNotifica
        End Get
        Set(ByVal value As String)
            _DaDataNotifica = value
        End Set
    End Property

    Public Property ADataNotifica() As String
        Get
            Return _ADataNotifica
        End Get
        Set(ByVal value As String)
            _ADataNotifica = value
        End Set
    End Property

    Public Property DaTarga() As String
        Get
            Return _DaTarga
        End Get
        Set(ByVal value As String)
            _DaTarga = value
        End Set
    End Property

    Public Property ATarga() As String
        Get
            Return _ATarga
        End Get
        Set(ByVal value As String)
            _ATarga = value
        End Set
    End Property

    Public Property DaDataInfrazione() As String
        Get
            Return _DaDataInfrazione
        End Get
        Set(ByVal value As String)
            _DaDataInfrazione = value
        End Set
    End Property

    Public Property ADataInfrazione() As String
        Get
            Return _ADataInfrazione
        End Get
        Set(ByVal value As String)
            _ADataInfrazione = value
        End Set
    End Property

    Public Property DaOraInfrazione() As String
        Get
            Return _DaOraInfrazione
        End Get
        Set(ByVal value As String)
            _DaOraInfrazione = value
        End Set
    End Property

    Public Property AOraInfrazione() As String
        Get
            Return _AOraInfrazione
        End Get
        Set(ByVal value As String)
            _AOraInfrazione = value
        End Set
    End Property

    Public Property StatoMulta() As String
        Get
            Return _StatoMulta
        End Get
        Set(ByVal value As String)
            _StatoMulta = value
        End Set
    End Property

    Public Property Casistica() As String
        Get
            Return _Casistica
        End Get
        Set(ByVal value As String)
            _Casistica = value
        End Set
    End Property

    Public Property Incassate() As String
        Get
            Return _Incassate
        End Get
        Set(ByVal value As String)
            _Incassate = value
        End Set
    End Property

    Public Property MotivoMancatoIncasso() As String
        Get
            Return _MotivoMancatoIncasso
        End Get
        Set(ByVal value As String)
            _MotivoMancatoIncasso = value
        End Set
    End Property

    Public Property Fatturate() As String
        Get
            Return _Fatturate
        End Get
        Set(ByVal value As String)
            _Fatturate = value
        End Set
    End Property

    Public Property Ricorso() As String
        Get
            Return _Ricorso
        End Get
        Set(ByVal value As String)
            _Ricorso = value
        End Set
    End Property

    Public Property ComunCliente() As String
        Get
            Return _ComunCliente
        End Get
        Set(ByVal value As String)
            _ComunCliente = value
        End Set
    End Property

    Public Property Fax() As String
        Get
            Return _Fax
        End Get
        Set(ByVal value As String)
            _Fax = value
        End Set
    End Property

    Public Property ConFattLocatori() As String
        Get
            Return _ConFattLocatori
        End Get
        Set(ByVal value As String)
            _ConFattLocatori = value
        End Set
    End Property

    Public Property Rimborsate() As String
        Get
            Return _Rimborsate
        End Get
        Set(ByVal value As String)
            _Rimborsate = value
        End Set
    End Property

    Public Property Pagate() As String
        Get
            Return _Pagate
        End Get
        Set(ByVal value As String)
            _Pagate = value
        End Set
    End Property

    Public Property Operatori() As String
        Get
            Return _Operatori
        End Get
        Set(ByVal value As String)
            _Operatori = value
        End Set
    End Property

    Public Function InsertFiltroReport() As Integer
        Dim sqlStr As String = "INSERT INTO multe_FiltroReports (id,Query,DaId,AId,DaProt,AProt,DaAnno,AAnno," & _
                "DaDataInserimento,ADataInserimento,DaNumVerbale,ANumVerbale,DaDataNotifica,ADataNotifica,DaTarga,ATarga," & _
                "DaDataInfrazione,ADataInfrazione,DaOraInfrazione,AOraInfrazione,StatoMulta,Casistica,Incassate," & _
                "MotivoMancatoIncasso,Fatturate,Ricorso,ComunCliente,Fax,ConFattLocatori,Rimborsate,Pagate,Operatori) " & _
                " VALUES (@id,@Query,@DaId,@AId,@DaProt,@AProt,@DaAnno,@AAnno," & _
                "@DaDataInserimento,@ADataInserimento,@DaNumVerbale,@ANumVerbale,@DaDataNotifica,@ADataNotifica,@DaTarga,@ATarga," & _
                "@DaDataInfrazione,@ADataInfrazione,@DaOraInfrazione,@AOraInfrazione,@StatoMulta,@Casistica,@Incassate," & _
                "@MotivoMancatoIncasso,@Fatturate,@Ricorso,@ComunCliente,@Fax,@ConFattLocatori,@Rimborsate,@Pagate,@Operatori) "
        HttpContext.Current.Trace.Write("--------------------------- sql insert: " & sqlStr)
        HttpContext.Current.Trace.Write("-------- fax: " & Fax)
        Dim id_filtro As Integer = GetMaxIdFiltroReport() + 1

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id_filtro
                Cmd.Parameters.Add("@Query", System.Data.SqlDbType.NVarChar).Value = Query
                Cmd.Parameters.Add("@DaId", System.Data.SqlDbType.NVarChar).Value = DaId
                Cmd.Parameters.Add("@AId", System.Data.SqlDbType.NVarChar).Value = AId
                Cmd.Parameters.Add("@DaProt", System.Data.SqlDbType.NVarChar).Value = DaProt
                Cmd.Parameters.Add("@AProt", System.Data.SqlDbType.NVarChar).Value = AProt
                Cmd.Parameters.Add("@DaAnno", System.Data.SqlDbType.NVarChar).Value = DaAnno
                Cmd.Parameters.Add("@AAnno", System.Data.SqlDbType.NVarChar).Value = AAnno
                Cmd.Parameters.Add("@DaDataInserimento", System.Data.SqlDbType.NVarChar).Value = DaDataInserimento
                Cmd.Parameters.Add("@ADataInserimento", System.Data.SqlDbType.NVarChar).Value = ADataInserimento
                Cmd.Parameters.Add("@DaNumVerbale", System.Data.SqlDbType.NVarChar).Value = DaNumVerbale
                Cmd.Parameters.Add("@ANumVerbale", System.Data.SqlDbType.NVarChar).Value = ANumVerbale
                Cmd.Parameters.Add("@DaDataNotifica", System.Data.SqlDbType.NVarChar).Value = DaDataNotifica
                Cmd.Parameters.Add("@ADataNotifica", System.Data.SqlDbType.NVarChar).Value = ADataNotifica
                Cmd.Parameters.Add("@DaTarga", System.Data.SqlDbType.NVarChar).Value = DaTarga
                Cmd.Parameters.Add("@ATarga", System.Data.SqlDbType.NVarChar).Value = ATarga
                Cmd.Parameters.Add("@DaDataInfrazione", System.Data.SqlDbType.NVarChar).Value = DaDataInfrazione
                Cmd.Parameters.Add("@ADataInfrazione", System.Data.SqlDbType.NVarChar).Value = ADataInfrazione
                Cmd.Parameters.Add("@DaOraInfrazione", System.Data.SqlDbType.NVarChar).Value = DaOraInfrazione
                Cmd.Parameters.Add("@AOraInfrazione", System.Data.SqlDbType.NVarChar).Value = AOraInfrazione
                Cmd.Parameters.Add("@StatoMulta", System.Data.SqlDbType.NVarChar).Value = StatoMulta
                Cmd.Parameters.Add("@Casistica", System.Data.SqlDbType.NVarChar).Value = Casistica
                Cmd.Parameters.Add("@Incassate", System.Data.SqlDbType.NVarChar).Value = Incassate
                Cmd.Parameters.Add("@MotivoMancatoIncasso", System.Data.SqlDbType.NVarChar).Value = MotivoMancatoIncasso
                Cmd.Parameters.Add("@Fatturate", System.Data.SqlDbType.NVarChar).Value = Fatturate
                Cmd.Parameters.Add("@Ricorso", System.Data.SqlDbType.NVarChar).Value = Ricorso
                Cmd.Parameters.Add("@ComunCliente", System.Data.SqlDbType.NVarChar).Value = ComunCliente
                Cmd.Parameters.Add("@Fax", System.Data.SqlDbType.NVarChar).Value = Fax
                Cmd.Parameters.Add("@ConFattLocatori", System.Data.SqlDbType.NVarChar).Value = ConFattLocatori
                Cmd.Parameters.Add("@Rimborsate", System.Data.SqlDbType.NVarChar).Value = Rimborsate
                Cmd.Parameters.Add("@Pagate", System.Data.SqlDbType.NVarChar).Value = Pagate
                Cmd.Parameters.Add("@Operatori", System.Data.SqlDbType.NVarChar).Value = Operatori
                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using

        Return id_filtro
    End Function


    Public Shared Function GetMaxIdFiltroReport() As Integer
        Dim MyReader As SqlDataReader
        Using MyConnection As SqlConnection = New SqlConnection

            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString

            Using MyCommand As SqlCommand = New SqlCommand()
                MyCommand.CommandText = "SELECT MAX(id) AS MaxIdFiltro FROM multe_FiltroReports"
                MyCommand.CommandType = CommandType.Text
                MyCommand.Connection = MyConnection

                MyCommand.Connection.Open()

                MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

                MyReader.Read()
                If MyReader.IsDBNull(0) Then
                    Return 0
                Else
                    Return MyReader("MaxIdFiltro")
                End If

            End Using
        End Using
    End Function
End Class
