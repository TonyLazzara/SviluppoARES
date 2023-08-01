Imports System.IO



Public Enum OrigineImport
    Xml = 1
    Web = 2
    R55 = 3
    Dollar = 4
    Thrifty = 5
End Enum

Public Enum TipoOperazione
    NonGestita = -1
    Nuova = 0
    Modifica = 1
    Cancellazione = 2
End Enum

Public Class RibaltamentoFile

    Private m_TipoImport As OrigineImport

    Private Class recordSuFile
        Public NomeRiga As String
        Public Inizio As Integer
        Public Fine As Integer
        Public NomeCampo As String
    End Class

    Private FileCorrente As String

    Private NumBatch As String
    Private DataPrenotazione As String
    Private OraPrenotazione As String

    Private CodificaRighe As Dictionary(Of String, List(Of recordSuFile))
    Private RecordPrenotazione As Dictionary(Of String, String)
    Private CodiciAcessori As Dictionary(Of String, String)

    Public Sub New(TipoImport As OrigineImport)
        If Not (TipoImport = OrigineImport.Dollar Or TipoImport = OrigineImport.Thrifty) Then
            Throw New Exception("Il parametro TipoImport può essere solo Dollar o Thrifty nel costruttor di RibaltamentoFile")
        End If

        m_TipoImport = TipoImport

        CaricaRegoleLettura()
    End Sub

    Private Sub CaricaRegoleLettura()


        CodificaRighe = New Dictionary(Of String, List(Of recordSuFile))

        Using Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)

            Dim sqlStr As String = "SELECT NomeRiga, Inizio, Fine, NomeCampo" & _
                " FROM RibaltamentoFile" & _
                " WHERE id_import = " & m_TipoImport ' & _
            ' " ORDER BY NomeRiga, Inizio"

            HttpContext.Current.Trace.Write(sqlStr)
            Using Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader()
                    Do While Rs.Read()
                        Dim recordRiga As recordSuFile = New recordSuFile
                        recordRiga.NomeRiga = Rs("NomeRiga")
                        recordRiga.Inizio = Rs("Inizio")
                        recordRiga.Fine = Rs("Fine")
                        recordRiga.NomeCampo = Rs("NomeCampo")
                        If recordRiga.NomeRiga <> "" Then
                            Dim myRiga As List(Of recordSuFile)
                            If CodificaRighe.ContainsKey(recordRiga.NomeRiga) Then
                                myRiga = CodificaRighe.Item(recordRiga.NomeRiga)
                            Else
                                myRiga = New List(Of recordSuFile)
                                CodificaRighe.Add(recordRiga.NomeRiga, myRiga)
                            End If

                            myRiga.Add(recordRiga)
                        End If
                    Loop
                End Using
            End Using
        End Using
    End Sub

    Private Sub ValorizzaAccessoriCensiti()
        CodiciAcessori = New Dictionary(Of String, String)
        Dim sqlStr As String = "SELECT codifica, CodDollar from condizioni_elementi where CodDollar IS NOT NULL"
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader()
                    Do While Rs.Read
                        If (Rs("codifica") & "") <> "" Then
                            CodiciAcessori.Add(Rs("CodDollar"), Rs("codifica") & "")
                        End If
                    Loop
                End Using
            End Using
        End Using
    End Sub

    Private Function DecodificaAcessori(CodicAcessoriDollar As String) As String
        DecodificaAcessori = ""
        If CodicAcessoriDollar = "" Then
            Return ""
        End If
        If Not CodiciAcessori Is Nothing Then
            Dim ElencoAcessori() As String = CodicAcessoriDollar.Split(" ")
            For i As Integer = 0 To ElencoAcessori.Length - 1
                If CodiciAcessori.ContainsKey(ElencoAcessori(i)) Then
                    DecodificaAcessori = DecodificaAcessori & "-" & CodiciAcessori.Item(ElencoAcessori(i))
                Else
                    DecodificaAcessori = DecodificaAcessori & "-" & ElencoAcessori(i)
                End If
            Next
        End If
    End Function

    Private Sub ParsingRiga(RigaDoc As String, ByVal num_batch As String)
        ' HttpContext.Current.Trace.Write("ParsingRiga: " & RigaDoc)
        Dim NomeRiga As String = Trim(Libreria.SubstringSicuro(RigaDoc, 3))
        If NomeRiga <> "" Then ' se i primi 3 caratteri sono vuoti salto la riga...
            If RecordPrenotazione Is Nothing Then
                ' inizializzo il processo per il nuovo record
                RecordPrenotazione = New Dictionary(Of String, String)

            End If
            If CodificaRighe.ContainsKey(NomeRiga) Then
                Dim myRiga As List(Of recordSuFile) = CodificaRighe.Item(NomeRiga)

                For Each regola As recordSuFile In myRiga
                    Dim valore As String = Trim(Libreria.SubstringSicuro(RigaDoc, regola.Inizio - 1, regola.Fine - regola.Inizio + 1))
                    RecordPrenotazione.Add(regola.NomeCampo, valore)
                Next
                If NomeRiga = "00:" Then
                    NumBatch = RecordPrenotazione("NumBatch")
                    DataPrenotazione = RecordPrenotazione("DataPrenotazione")
                    OraPrenotazione = RecordPrenotazione("OraPrenotazione")
                End If
            End If
            If (m_TipoImport = OrigineImport.Dollar And NomeRiga = "32:") _
                Or (m_TipoImport = OrigineImport.Thrifty And NomeRiga = "27:") Then
                ' verifico la corretta lettura del record e lo salvo in ribaltamento
                If num_batch = "" Or num_batch <> "" And NumBatch = num_batch Then
                    'NEL CASO IN CUI STO RICHIDEDENDO UNA PARTICOLARE PRENOTAZIONE SALVO LA RIGA SOLAMENTE SE HO APPENA PARSERIZZATO QUELLA STESSA PRENOTAZIONE
                    SalvaPrenotazione()
                End If

                RecordPrenotazione = Nothing
            End If
        End If
    End Sub

    Private Function getData(str_data As String, Optional str_ora As String = "00:00:00") As DateTime
        HttpContext.Current.Trace.Write("str_data: (" & str_data & " - " & str_ora & ")")
        Dim anno As Integer = 2000 + Integer.Parse(str_data.Substring(6, 2))
        Dim mese As Integer = Integer.Parse(str_data.Substring(0, 2))
        Dim giorno As Integer = Integer.Parse(str_data.Substring(3, 2))

        Dim ora As Integer = Integer.Parse(str_ora.Substring(0, 2))
        Dim minuti As Integer = Integer.Parse(str_ora.Substring(3, 2))
        Dim secondi As Integer = Integer.Parse(str_ora.Substring(6, 2))

        getData = New Date(anno, mese, giorno, ora, minuti, secondi)
    End Function

    Private Function getIdStazione(CodiceStazione As String) As Integer?
        Dim sqlStr As String
        Select Case m_TipoImport
            Case OrigineImport.Dollar
                sqlStr = "SELECT id FROM stazioni WHERE CodDollar = '" & CodiceStazione & "'"
            Case OrigineImport.Thrifty
                sqlStr = "SELECT id FROM stazioni WHERE CodThrifty = '" & CodiceStazione & "'"
            Case Else
                sqlStr = "SELECT id FROM stazioni WHERE codice = '" & CodiceStazione & "'"
        End Select

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getIdStazione = Cmd.ExecuteScalar()
            End Using
        End Using
    End Function

    Protected Function getIdTariffa(ByVal codtar As String) As Integer?
        Dim sqlStr As String = "SELECT id FROM tariffe WHERE codtar='" & Replace(codtar, "'", "''") & "'"
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getIdTariffa = Cmd.ExecuteScalar()
            End Using
        End Using
    End Function

    Private Function getIdGruppo(ByVal cod_gruppo As String, Optional Attivo As Boolean = False) As Integer?
        Dim sqlStr As String
        Select Case m_TipoImport
            Case OrigineImport.Dollar
                sqlStr = "SELECT id_gruppo FROM gruppi WHERE CodDollar = '" & cod_gruppo & "'"
            Case OrigineImport.Thrifty
                sqlStr = "SELECT id_gruppo FROM gruppi WHERE CodThrifty = '" & cod_gruppo & "'"
            Case Else
                sqlStr = "SELECT id_gruppo FROM gruppi WHERE cod_gruppo = '" & cod_gruppo & "'"
        End Select
        If Attivo Then
            sqlStr += " AND attivo='1'"
        End If

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getIdGruppo = Cmd.ExecuteScalar()
                'HttpContext.Current.Trace.Write("getIdGruppo: (" & getIdGruppo & ")")
                'If getIdGruppo = "" Then
                '    HttpContext.Current.Trace.Write("getIdGruppo: Nothing")
                '    getIdGruppo = Nothing
                'End If
            End Using
        End Using
    End Function

    Private Function CalcolaScadenzaPatente(str_data As String) As Date?
        If str_data.Trim = "" Then
            Return Nothing
        End If
        HttpContext.Current.Trace.Write("1 CalcolaScadenzaPatente: (" & str_data & ")")
        Dim anno As Integer = Integer.Parse(str_data.Substring(3, 2)) + 2000
        Dim mese As Integer = Integer.Parse(str_data.Substring(0, 2))
        Dim MiaData As Date = New Date(anno, mese + 1, 1) ' ottengo così l'ultimo giorno del mese precedente...
        MiaData = MiaData.AddDays(-1)
        HttpContext.Current.Trace.Write("2 CalcolaScadenzaPatente: (" & MiaData & ")")
        Return MiaData
    End Function

    Private Function getNumPrenotazioneDaCodiceInterno(CodInterno As String) As String
        Dim sqlStr As String = "SELECT MAX([NUMPREN]) FROM prenotazioni WHERE [rif_to] = '" & CodInterno & "'"
        HttpContext.Current.Trace.Write(sqlStr)
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getNumPrenotazioneDaCodiceInterno = Cmd.ExecuteScalar() & ""
            End Using
        End Using
    End Function

    Private Function getNumPrenotazioneDaRibaltamento(CodInterno As String) As String
        Dim sqlStr As String = "SELECT MAX([NUMPREN]) FROM ribaltamento WHERE CODNUMPREN = '" & CodInterno & "'"
        HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getNumPrenotazioneDaRibaltamento = Cmd.ExecuteScalar() & ""
            End Using
        End Using
    End Function

    Private Function getIdFonte(TipoImport As OrigineImport) As String
        Dim sqlStr As String
        sqlStr = "SELECT Id FROM clienti_tipologia WHERE descrizione = '" & Libreria.formattaSqlTrim(TipoImport.ToString.ToUpper()) & "'"
        HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getIdFonte = Cmd.ExecuteScalar() & ""
            End Using
        End Using
        If getIdFonte = "" Then
            getIdFonte = -1
        End If
    End Function

    Private Sub SalvaPrenotazione()
        'DOLLAR - THRIFTY
        Dim MioRibaltamento As TabRibaltamento = New TabRibaltamento
        ' Dim NumBatch As Integer = Integer.Parse(RecordPrenotazione("NumBatch")) ' utilizzato per verifica....
        Dim Operazione As TipoOperazione
        Select Case RecordPrenotazione("TipoOperazione")
            Case "R"
                Operazione = TipoOperazione.Nuova
            Case "A"
                Operazione = TipoOperazione.Modifica
            Case "C"
                Operazione = TipoOperazione.Cancellazione
            Case Else
                Operazione = TipoOperazione.NonGestita
        End Select

        HttpContext.Current.Trace.Write("SalvaPrenotazione: (" & Operazione.ToString & ")")

        Dim stato As Boolean = True
        Dim codici_errore As String = ""

        With MioRibaltamento
            .blocco_ribaltamento = 0
            .FileImport = FileCorrente
            .TipoPrenotazione = Operazione
            .provenienza_replica = m_TipoImport
            .DATAPREN = getData(DataPrenotazione, OraPrenotazione)
            .IDPREN_esterno = Integer.Parse(NumBatch) ' non so se va bene....
            .CODNUMPREN = RecordPrenotazione("NumPrenotazione")

            Select Case m_TipoImport
                Case OrigineImport.Dollar
                    .codtar = Costanti.tariffa_Dollar
                    .id_tour_operator = Costanti.id_clienti_tipologia_dollar()
                Case OrigineImport.Thrifty
                    .codtar = Costanti.tariffa_Thrifty
                    .id_tour_operator = Costanti.id_clienti_tipologia_thrifty()
            End Select
            'MARCO
            .idtariffa = getIdTariffa(.codtar)

            If .idtariffa Is Nothing Then
                stato = False
                codici_errore += "20|"
            End If

            If Operazione = TipoOperazione.Nuova Then
                ' da Inizializzare correttamente il contatore!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                .NUMPREN = Contatori.getContatore_DollarThrifty() ' utilizzo questo codice per individuare il record!!!!
                '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Else
                Dim NumPre As String = getNumPrenotazioneDaCodiceInterno(.CODNUMPREN)
                If NumPre = "" Then
                    ' può darsi che la prenotazine non sia stata ancora salvata perché elaborata nella corrente elaborazione!
                    NumPre = getNumPrenotazioneDaRibaltamento(.CODNUMPREN)
                End If
                If Operazione = TipoOperazione.Modifica Then
                    If NumPre = "" Then
                        .NUMPREN = Contatori.getContatore_DollarThrifty()
                    Else
                        .NUMPREN = Integer.Parse(NumPre)
                    End If
                Else ' TipoOperazione.Cancellazione o Non gestita
                    If NumPre = "" Then
                        NumPre = "0"
                    End If
                    .NUMPREN = Integer.Parse(NumPre)
                End If
            End If

            .cod_sta_out = RecordPrenotazione("StazionePickUp")
            .cod_sta_in = RecordPrenotazione("StazioneDropOff")
            If .cod_sta_in = "" Then
                .cod_sta_in = .cod_sta_out
            End If
            .STA_OUT = getIdStazione(.cod_sta_out)
            If .STA_OUT Is Nothing Then
                stato = False
                codici_errore += "22|"
                .blocco_ribaltamento = 1
            End If
            .STA_IN = getIdStazione(.cod_sta_in)
            If .STA_IN Is Nothing Then
                stato = False
                codici_errore += "23|"
                .blocco_ribaltamento = 1
            End If

            Dim Ora As String = RecordPrenotazione("OraPickUp")
            .data_out = getData(RecordPrenotazione("DataPickUp"), Ora.Substring(0, 2) & ":" & Ora.Substring(2, 2) & ":00")
            Ora = RecordPrenotazione("OraDropOff")
            .data_in = getData(RecordPrenotazione("DataDropOff"), Ora.Substring(0, 2) & ":" & Ora.Substring(2, 2) & ":00")

            Dim NomeCognome() As String = RecordPrenotazione("Nome").Split("/"c)

            If NomeCognome.Length > 1 Then
                .cognome = NomeCognome(0)
                .nome = NomeCognome(1)
            Else
                .nome = RecordPrenotazione("Nome")
            End If

            .nome_azienda = RecordPrenotazione("NomeAz")
            .indirizzo = Trim(RecordPrenotazione("Indirizzo1") & " " & RecordPrenotazione("Indirizzo2"))
            .telefono = Trim(RecordPrenotazione("PreInternazionale") & " " & RecordPrenotazione("Telefono"))
            .volo_out = RecordPrenotazione("NumVoloPickUp")

            ' non sono sicuro che sia un valore numerico... 
            ' può essere stringa vuota...
            ' cmq non so se siano censiti...
            '.id_tour_operator = Integer.Parse(RecordPrenotazione("TourOperator")) 



            .citta = RecordPrenotazione("Citta")
            .provincia = RecordPrenotazione("Provincia")
            .cap = RecordPrenotazione("CAP")
            .nazione = RecordPrenotazione("Nazione")

            .supplementi = DecodificaAcessori(RecordPrenotazione("Accessori"))

            .cod_gruppo = RecordPrenotazione("CodiceGruppo")
            .id_gruppo = getIdGruppo(.cod_gruppo, True)
            .cod_gruppo_da_consegnare = .cod_gruppo
            .id_gruppo_da_consegnare = .id_gruppo

            If .id_gruppo Is Nothing Then
                stato = False
                .id_gruppo = getIdGruppo(.cod_gruppo)
                If .id_gruppo Is Nothing Then
                    codici_errore += "21a|" ' vedi funzione: App_code.funzioni_comuni.salvaWarning
                    .blocco_ribaltamento = 1
                Else
                    codici_errore += "21c|"

                    .id_gruppo_da_consegnare = .id_gruppo
                    codici_errore += "21d|"
                End If

            End If
            If .id_gruppo_da_consegnare Is Nothing Then
                stato = False
                codici_errore += "21b|"
            End If

            .email = RecordPrenotazione("Mail")

            .patente_num = RecordPrenotazione("Patente")

            .scad_patente = CalcolaScadenzaPatente(RecordPrenotazione("ScadenzaPatente"))

            Dim str_data_nascita As String = RecordPrenotazione("DataNascita")
            '.str_data_nascita = str_data_nascita
            If str_data_nascita.Trim = "" Then
                'stato = False
                ' codici_errore += "24|"
                .data_nascita = Nothing
            Else
                If m_TipoImport = OrigineImport.Thrifty Then
                    ' secondo il tracciato era MMDDYY invece trovo ho trovato MM/DD/YY
                    .data_nascita = getData(str_data_nascita)
                Else
                    ' secondo il tracciato era MMDDYY ... spero :(
                    .data_nascita = getData(str_data_nascita.Substring(0, 2) & "/" & str_data_nascita.Substring(2, 2) & "/" & str_data_nascita.Substring(4, 2))
                End If
                'HttpContext.Current.Trace.Write("data_nascita: (" & str_data_nascita & ")")
                'HttpContext.Current.Trace.Write("data_nascita: (" & str_data_nascita.Substring(0, 2) & "/" & str_data_nascita.Substring(2, 2) & "/" & str_data_nascita.Substring(4, 2) & ")")

                If Year(Now()) - Year(.data_nascita) < 18 Then ' probabilmente è nato nel secolo scorso... il 1900
                    .data_nascita = New Date(Year(.data_nascita) - 100, Month(.data_nascita), Day(.data_nascita))
                End If
                'HttpContext.Current.Trace.Write(".data_nascita: (" & .data_nascita & ")")
            End If

            'Dim VerificaBatch = RecordPrenotazione("V_NumBatch")
            'If VerificaBatch Is Nothing Then
            '    stato = False
            'Else
            '    If VerificaBatch = "" Then
            '        stato = False
            '    Else
            '        If .IDPREN_esterno <> Integer.Parse(VerificaBatch) Then
            '            stato = False
            '        End If
            '    End If
            'End If

            '.codici_errore = codici_errore

            '.volo_in = Rs("VOLORES") non valorizzato
            '.cognome = Rs("COGNOME") insieme a nome...
            '.luogo_nascita = Rs("luogo_nascita")
            '.COD_CONV = Rs("CONV")

            'Numero Auto?????
            'Tipo Garanzia G o Q????



            '.codtar = CodiceTariffa
            .sconto = 0
            .totale = 0 ' Rs("TOTALE")

            .flag_azienda = False
            '.id_azienda = id_azienda

            '.patente_ril = Rs("patente_ril")
            '.data_pat_rilascio = Rs("data_pat_rilascio")
            '.str_data_ril_patente = Rs("data_pat_rilascio")

            '.CCIMPORTO = Rs("CCNUMAUT")
            '.CCDATA = CCDATA
            '.CCIMPORTO = Rs("CCIMPORTO")
            '.CCNUMOPE = Rs("CCNUMOPE")
            '.CCRISP = Rs("CCRISP")
            '.CCTRANS = Rs("CCTRANS")
            '.CCOMPAGNIA = Rs("CCOMPAGNIA")
            '.TRANSOK = Rs("TRANSOK")
            '.TERMINAL_ID = TERMINAL_ID

            If stato Then
                .stato = 0
            Else
                .stato = 3
            End If
            If codici_errore.Length > 0 Then
                .codici_errore = codici_errore.Substring(0, codici_errore.Length - 1)  ' elimino l'ultima pipe...
            End If

            .NOTE = Trim(RecordPrenotazione("Nota1") & " " & RecordPrenotazione("Nota2") & " " & RecordPrenotazione("Nota3"))

            .SalvaRecord()
            If RecordPrenotazione("NumeroAuto") <> "" And RecordPrenotazione("NumeroAuto") <> "1" And TipoOperazione.Cancellazione <> Operazione Then
                .NOTE = Trim(.NOTE & vbCrLf & "Aggiunta alla prenotazione N° " & .NUMPREN)
                Dim NumeroAuto As Integer = Integer.Parse(RecordPrenotazione("NumeroAuto"))
                For i As Integer = 1 To NumeroAuto - 1
                    .NUMPREN = Contatori.getContatore_DollarThrifty()
                    .SalvaRecord()
                Next
            End If
        End With
    End Sub

    Private Sub ParsingDocumento(NomeFile As String, ByVal num_batch As String)
        Dim objReader As New StreamReader(NomeFile)
        Dim sLine As String = ""

        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                ParsingRiga(sLine, num_batch)
            End If
        Loop Until sLine Is Nothing
        objReader.Close()
    End Sub

    Public Function ParsingCartella(ByVal file_import As String, ByVal num_batch As String) As Integer

        '<add key="DriveDollar" value="p:"/>
        '<add key="PathOutputDollar" value="\Elaborati\"/>
        '<add key="PathInputDollar" value="\\10.0.88.60\DOLLAR_x_ARES"/>
        '<add key="LoginDollar" value="SBC\ARES"/>
        '<add key="PasswordDollar" value="ares"/>

        Dim PathOutput As String = ConfigurationManager.AppSettings.Get("PathOutput" & m_TipoImport.ToString)

        Dim MappaUnitaRete As CMappaUnitaRete = New CMappaUnitaRete

        With MappaUnitaRete
            .Drive = ConfigurationManager.AppSettings.Get("Drive" & m_TipoImport.ToString)
            .Path = ConfigurationManager.AppSettings.Get("PathInput" & m_TipoImport.ToString)

            'If file_import <> "" Then
            '    'SE CHIEDO DI IMPORTARE UN FILE GIA' IMPORTATO LO TROVO ALL'INTERNO DELLA CARTELLA DEI FILE ELABORATI
            '    .Path = .Path & ConfigurationManager.AppSettings.Get("PathOutput" & m_TipoImport.ToString)
            'End If

            .UserName = ConfigurationManager.AppSettings.Get("Login" & m_TipoImport.ToString)
            .Password = ConfigurationManager.AppSettings.Get("Password" & m_TipoImport.ToString)
        End With

        MappaUnitaRete.CollegaUnita()

        Dim fileEntries As String()

        If file_import <> "" Then
            fileEntries = Directory.GetFiles(MappaUnitaRete.Drive & ConfigurationManager.AppSettings.Get("PathOutput" & m_TipoImport.ToString), file_import)
        Else
            fileEntries = Directory.GetFiles(MappaUnitaRete.Drive, "*.REZ")
        End If

        Array.Sort(fileEntries)
        ParsingCartella = fileEntries.Length
        Dim i As Integer = 0
        For Each fileName As String In fileEntries
            i += 1
            FileCorrente = Path.GetFileName(fileName)
            ParsingDocumento(fileName, num_batch)
            'HttpContext.Current.Trace.Write(i & " - File.Move(" & fileName & "," & MappaUnitaRete.Drive & PathOutput & FileCorrente & ")")
            ' sino a quando il sistema non va in produzione
            ' e necessario commentare questa riga!
            File.Move(fileName, MappaUnitaRete.Drive & PathOutput & FileCorrente)

            If i > 4 Then
                Exit For
            End If
        Next
        Return ParsingCartella - i
        ' Processa la lista dei files trovati nella directory passata

    End Function

End Class
