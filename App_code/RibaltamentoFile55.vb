Imports System.IO
Imports System.Net


Public Class RibaltamentoFile55
    ' http://support.microsoft.com/kb/842789/it

    Private m_TipoImport As OrigineImport

    Private FileCorrente As String
    Private PathInput As String = ConfigurationManager.AppSettings.Get("PathInputR55") ' "\\192.168.100.2\autoeuropa\intranet\" 
    Private PathOutput As String = ConfigurationManager.AppSettings.Get("PathOutputR55") ' "C:\Ribaltamento55\R55\Elaborati\"

    Private URL_Ribaltamento55 As String = ConfigurationManager.AppSettings.Get("URL_R55") ' "http://192.168.100.2/intranet/esporta_ares.asp"

    Public Function Scarica(ByVal strURL As String) As String
        HttpContext.Current.Trace.Write("strURL: " & strURL)
        Dim wreq As WebRequest = WebRequest.Create(strURL)
        Dim wres As WebResponse = wreq.GetResponse()
        Dim iBuffer As Integer = 0
        Dim buffer(128) As [Byte]
        Dim stream As Stream = wres.GetResponseStream()
        iBuffer = stream.Read(buffer, 0, buffer.Length)
        Dim strRes As New StringBuilder("")
        While iBuffer <> 0
            strRes.Append(Encoding.ASCII.GetString(buffer, 0, iBuffer))
            iBuffer = stream.Read(buffer, 0, buffer.Length)
        End While
        Return strRes.ToString()
    End Function

    Public Sub New(Optional TipoImport As OrigineImport = OrigineImport.R55)
        If Not (TipoImport = OrigineImport.R55) Then
            Throw New Exception("Il parametro TipoImport può essere solo R55 nel costruttor di RibaltamentoFile55")
        End If

        m_TipoImport = TipoImport

        ' PathInput = ConfigurationManager.AppSettings.Get("PathInput" & m_TipoImport.ToString)
        ' PathOutput = ConfigurationManager.AppSettings.Get("PathOutput" & m_TipoImport.ToString)
    End Sub

    Private Enum CampiRecord
        NUMPREN = 0
        DATAORA
        GRUPPO
        UFFNOL
        DATANOL
        ORANOL
        VOLONOL
        UFFRES
        DATARES
        ORARES
        VOLORES
        NOTE ' da ritrasformare... " - "
        Accessori
        TOTALE
        IMPBASE
        IMPSUPP
        IMPAPT
        IMPVAL

        TAG_DEP ' -1 = true 0 = false
        IMP_DEP ' ????
        CognomeNome
        indirizzo
        citta
        telefono
        email
        nome_tar
        data_nascita
        luogo_nascita
        codfisc

        patente_num
        patente_ril
        data_pat_rilascio

        NUM_CARD_LIDL

        CCNUMAUT        '- NUMERO AUTORIZZAZIONE 6 CIFRE
        CCDATA          '- DATA TRANSAZIONE
        CCORA           '- ORA TRANSAZIONE
        CCIMPORTO       '- IMPORTO TRANSAZIONE
        CCOMPAGNIA      '- NOME CARTA (01-VISA / 02-MASTERCARD / 06-AMEX 

        IDCLIENTE

        TRANS_PAG ' -1 = true 0 = false 'PAGATO ON LINE o NON PAGATO (STRANIERE)
        AZIENDA ' -1 = true 0 = false
        VUOTO1
        VUOTO2
    End Enum

    Private Function ParsingRiga(RigaDoc As String) As String()
        HttpContext.Current.Trace.Write("ParsingRiga: " & RigaDoc)

        'For i As Integer = 0 To RigaDoc.Length - 1
        '    HttpContext.Current.Trace.Write(RigaDoc(i) & " - " & AscW(RigaDoc(i)) & " - " & ChrW(65533))
        'Next

        ParsingRiga = RigaDoc.Split(New Char() {ChrW(65533)})
    End Function

    Private Function getFloat(Valore As String) As Double
        getFloat = Double.Parse(Valore)
    End Function

    Private Function getTrueFalse(Valore As String) As Boolean
        If Valore = "-1" Then
            getTrueFalse = True
        Else ' Valore = "0"
            getTrueFalse = False
        End If
    End Function

    Private Function getData(str_data As String, Optional str_ora As String = "") As DateTime?
        HttpContext.Current.Trace.Write("str_data: (" & str_data & ") str_ora: (" & str_ora & ")")
        If str_ora <> "" Then
            str_data = Trim(str_data & " " & str_ora)
        End If
        If str_data = "" Then
            Return Nothing
        End If
        Try
            Dim anno As Integer = Year(str_data)
            Dim mese As Integer = Month(str_data)
            Dim giorno As Integer = Day(str_data)

            Dim ora As Integer = Hour(str_data)
            Dim minuti As Integer = Minute(str_data)
            Dim secondi As Integer = Second(str_data)

            getData = New Date(anno, mese, giorno, ora, minuti, secondi)
        Catch ex As Exception
            getData = Nothing
        End Try

        HttpContext.Current.Trace.Write("getData: " & getData)
    End Function

    Private Function getIdStazione(CodiceStazione As String) As Integer?
        Dim sqlStr As String

        sqlStr = "SELECT id FROM stazioni WHERE codice = '" & CodiceStazione & "'"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getIdStazione = Cmd.ExecuteScalar()
            End Using
        End Using
    End Function

    Protected Function getIdTariffa(ByVal codtar As String) As Integer?
        Dim sqlStr As String = "SELECT id FROM tariffe WHERE codtar='" & codtar & "'"
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getIdTariffa = Cmd.ExecuteScalar()
            End Using
        End Using
    End Function

    Private Function getIdGruppo(ByVal cod_gruppo As String, Optional Attivo As Boolean = False) As Integer?
        Dim sqlStr As String

        sqlStr = "SELECT id_gruppo FROM gruppi WHERE cod_gruppo = '" & cod_gruppo & "'"
        If Attivo Then
            sqlStr += " AND attivo='1'"
        End If

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getIdGruppo = Cmd.ExecuteScalar()
            End Using
        End Using
    End Function

    Private Sub SalvaPrenotazione(RigaDoc As String)
        Dim MioRibaltamento As TabRibaltamento = New TabRibaltamento

        Dim RecordPrenotazione() As String = ParsingRiga(RigaDoc)

        For i As CampiRecord = 0 To RecordPrenotazione.Length - 1
            If i <> CampiRecord.NOTE Then
                RecordPrenotazione(i) = RecordPrenotazione(i).Trim
            End If
            HttpContext.Current.Trace.Write("RecordPrenotazione(" & i.ToString & ") = [" & RecordPrenotazione(i) & "]")
        Next

        Dim Operazione As TipoOperazione
        Operazione = TipoOperazione.Nuova ' sono gestiti solo nuovi record!

        Dim stato As Boolean = True
        Dim codici_errore As String = ""

        With MioRibaltamento
            .FileImport = FileCorrente
            .TipoPrenotazione = Operazione
            .provenienza_replica = m_TipoImport
            .CODNUMPREN = RecordPrenotazione(CampiRecord.NUMPREN)

            .NUMPREN = Integer.Parse(.CODNUMPREN)
            .IDPREN_esterno = .NUMPREN
            .DATAPREN = getData(RecordPrenotazione(CampiRecord.DATAORA))

            .cod_gruppo = RecordPrenotazione(CampiRecord.GRUPPO)
            .id_gruppo = getIdGruppo(.cod_gruppo, True)
            .cod_gruppo_da_consegnare = .cod_gruppo
            .id_gruppo_da_consegnare = .id_gruppo

            If .id_gruppo Is Nothing Then
                stato = False
                ' ricerco il gruppo anche tra gli elementi non più attivi...
                .id_gruppo = getIdGruppo(.cod_gruppo)
                If .id_gruppo Is Nothing Then
                    codici_errore += "21a|" ' vedi funzione: App_code.funzioni_comuni.salvaWarning
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

            .cod_sta_out = RecordPrenotazione(CampiRecord.UFFNOL)
            .cod_sta_in = RecordPrenotazione(CampiRecord.UFFRES)
            .STA_OUT = getIdStazione(.cod_sta_out)
            If .STA_OUT Is Nothing Then
                stato = False
                codici_errore += "22|"
            End If
            .STA_IN = getIdStazione(.cod_sta_in)
            If .STA_IN Is Nothing Then
                stato = False
                codici_errore += "23|"
            End If

            .data_out = getData(RecordPrenotazione(CampiRecord.DATANOL), RecordPrenotazione(CampiRecord.ORANOL))
            .data_in = getData(RecordPrenotazione(CampiRecord.DATARES), RecordPrenotazione(CampiRecord.ORARES))

            .volo_out = RecordPrenotazione(CampiRecord.VOLONOL)
            .volo_in = RecordPrenotazione(CampiRecord.VOLORES)

            .NOTE = Trim(RecordPrenotazione(CampiRecord.NOTE).Replace(" - ", vbCrLf))
            If .NOTE = "" Then
                .NOTE = Nothing
            End If

            .supplementi = RecordPrenotazione(CampiRecord.Accessori)

            .sconto = 0
            .totale = getFloat(RecordPrenotazione(CampiRecord.TOTALE))

            ' TAG_DEP cosa sono?
            ' IMP_DEP

            Dim CognomeNome() As String = RecordPrenotazione(CampiRecord.CognomeNome).Split(" ")
            .cognome = CognomeNome(0)
            For i As Integer = 1 To CognomeNome.Length - 1
                .nome += CognomeNome(i) & " "
            Next
            .nome = .nome.Trim
            .indirizzo = RecordPrenotazione(CampiRecord.indirizzo)
            .citta = RecordPrenotazione(CampiRecord.citta)
            .telefono = RecordPrenotazione(CampiRecord.telefono)
            .email = RecordPrenotazione(CampiRecord.email)

            .blocco_ribaltamento = 0
            .codtar = RecordPrenotazione(CampiRecord.nome_tar)
            .idtariffa = getIdTariffa(.codtar)
            If .idtariffa Is Nothing Then
                stato = False
                codici_errore += "20|"
                .blocco_ribaltamento = 1
            End If

            '.provincia = RecordPrenotazione(CampiRecord.Provincia) 
            '.cap = RecordPrenotazione(CampiRecord.cap)
            '.nazione = RecordPrenotazione(CampiRecord.Nazione)

            Dim str_data_nascita As String = RecordPrenotazione(CampiRecord.data_nascita)
            .str_data_nascita = str_data_nascita
            If str_data_nascita.Trim <> "" Then
                .data_nascita = getData(str_data_nascita)
                If .data_nascita Is Nothing Then
                    stato = False
                    codici_errore += "24|"
                    .data_nascita = Nothing
                End If
            End If
            .luogo_nascita = RecordPrenotazione(CampiRecord.luogo_nascita)

            .patente_num = RecordPrenotazione(CampiRecord.patente_num)
            .patente_ril = RecordPrenotazione(CampiRecord.patente_ril)
            .str_data_ril_patente = RecordPrenotazione(CampiRecord.data_pat_rilascio)
            .data_pat_rilascio = getData(RecordPrenotazione(CampiRecord.data_pat_rilascio))

            .CCNUMAUT = RecordPrenotazione(CampiRecord.CCNUMAUT)
            .CCDATA = getData(RecordPrenotazione(CampiRecord.CCDATA), RecordPrenotazione(CampiRecord.CCORA))
            .CCIMPORTO = RecordPrenotazione(CampiRecord.CCIMPORTO)
            .CCOMPAGNIA = RecordPrenotazione(CampiRecord.CCOMPAGNIA)
            .TRANSOK = getTrueFalse(RecordPrenotazione(CampiRecord.TRANS_PAG))
            If .TRANSOK Then
                .TERMINAL_ID = Costanti.terminal_id_web
            End If

            .id_azienda = Integer.Parse(RecordPrenotazione(CampiRecord.IDCLIENTE))
            .flag_azienda = getTrueFalse(RecordPrenotazione(CampiRecord.AZIENDA))

            If stato Then
                .stato = 0
            Else
                .stato = 3
            End If

            If codici_errore.Length > 0 Then
                .codici_errore = codici_errore.Substring(0, codici_errore.Length - 1)  ' elimino l'ultima pipe...
            End If

            .SalvaRecord()
        End With
    End Sub

    Private Sub ParsingDocumento(NomeFile As String)
        Dim objReader As New StreamReader(NomeFile)
        Dim sLine As String = ""

        Do
            sLine = objReader.ReadLine()
            If Not sLine Is Nothing Then
                SalvaPrenotazione(sLine)
            End If
        Loop Until sLine Is Nothing
        objReader.Close()
    End Sub

    Public Sub ParsingCartella()
        ' Processa la lista dei files trovati nella directory passata
        Dim fileEntries As String() = Directory.GetFiles(PathInput)
        Array.Sort(fileEntries)
        For Each fileName As String In fileEntries
            FileCorrente = Path.GetFileName(fileName)
            ParsingDocumento(fileName)
            HttpContext.Current.Trace.Write("File.Move(" & fileName & "," & PathOutput & FileCorrente & ")")
            File.Move(fileName, PathOutput & FileCorrente)
        Next
    End Sub

    Public Sub ParsingRibaltamentoDaURL()
        Dim HtmlOutput As String = Scarica(URL_Ribaltamento55)

        HttpContext.Current.Trace.Write("HtmOutput(" & HtmlOutput & ")")

        If HtmlOutput.Contains("NON CI SONO PRENOTAZIONI DA ESPORTARE") Then
            HttpContext.Current.Trace.Write("NON CI SONO PRENOTAZIONI DA ESPORTARE")

            'per testare il sistema anche in assenza di prenotazioni... -----------------------------------
            Dim fileNameIn As String = PathInput & "esp-20120402083602.txt"
            Dim fileNameOut As String = PathOutput & "esp-20120402083602.txt"

            ParsingDocumento(fileNameIn)
            File.Copy(fileNameIn, fileNameOut, True)
            '-----------------------------------------------------------------------------------------------
            Return
        End If

        ' esempio: esp-20120402115014.txt
        Dim R As New Regex("(?<NomeFile>esp-[0-9]{14}.txt)", RegexOptions.Multiline) 'esp-[0-9]{14}.txt

        Dim Matches As MatchCollection

        Matches = R.Matches(HtmlOutput)

        For Each M As Match In Matches
            Dim fileNameIn As String = PathInput & M.Groups("NomeFile").Value
            Dim fileNameOut As String = PathOutput & M.Groups("NomeFile").Value
            HttpContext.Current.Trace.Write("NomeFile: " & fileNameIn & " - " & fileNameOut)
            ParsingDocumento(fileNameIn)
            ' dovrei spostare... ma non ho i diritti per farlo...
            ' quindi copio semplicemente il file!
            ' File.Move(fileName, PathOutput & M.Groups("NomeFile").Value) 
            File.Copy(fileNameIn, fileNameOut, True)
            Exit For ' mi basta elaborare solo la prima occorenza trovata... il file generato è uno solo
        Next

    End Sub

End Class
