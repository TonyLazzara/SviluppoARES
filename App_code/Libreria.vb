' si devono rendere reali tutte queste costanti!!!
Public Enum PermessiUtente
    'ACCESSI ALLE FUNZIONALITA'

    tipo_allegati = 159 'nuovo 21.05.2021 inserito anche in tabella funzionalità
    documenti = 86      'nuovo 21.05.2021
    esportazione_dati_xml = 160      'nuovo 21.05.2021 inserito anche in tabella funzionalità
    nuova_stazione = 17 'nuovo 26.05.2021 

    ParcoVeicoli = 1
    TabelleVeicoli = 9
    ODL = 138
    FunzioniVeicoli = 33
    GestioneListini = 44
    TabelleListini = 39
    GestioneStazioni = 51
    GestioneVal = 58
    MovimentiVeicoli = 83
    ModificaKmVettura = 158
    '---------------------------------------
    CensimentoPOS = 59
    CensimentoEntiProprietari = 63
    CensimentoCircuiti = 64
    CensimentoAquires = 65
    CensimentoTipologieDiErrore = 66
    CensimentoActionCode = 67
    FunzionalitaPOS = 69
    CensimentoTransazioneTelefonica = 146
    '---------------------------------------
    GestioneMulte = 89
    PreventiviPrenotazioni = 71
    GestionePos = 61
    GestioneDanni = 103
    Anagrafica = 119
    '--------------------------------------------------------------------------

    TabelleDitte = 117
    TabelleConducenti = 116
    GestioneGps = 148
    GestioneGpsDismissione = 155
    RimozioneBlackList = 118

    RibaltamentoPrenotazioni = 82
    VisualizzaDettaglioOperazionePOS = 81
    GestioneFatture = 82

    GestioneTabelleDanni = 105
    ' devono diventare GestioneTabelleDanni = 105
    GestioneOrigineDanno = 105
    GestionePosizioneDanno = 105
    GestioneTipoDanno = 105
    GestioneTipoDocumentoDanno = 105
    GestioneTipoDocumentoEventoDanno = 105
    GestioneMappaturaModelli = 105
    GestioneSinistri = 107


    ' -------------------------------------------
    GestioneRDS = 104
    GestioneTabelleRDS = 106
    ModificaStatoChiusoRDS = 108
    GestioneODL = 112
    GestioneFornitori = 112
    GestioneDrivers = 112
    GestioneODLAdmin = 113
    '---------------------------------------------
    'PREVENTIVI/PRENOTAZIONI/CONTRATTI-----------
    VisualizzareValoreTariffa = 121
    VisualizzareCondizioniTariffa = 122
    VisualizzareCondizioniTariffeBroker = 123
    VisualizzareValoreTariffaBroker = 124

    ApplicareSconto = 72
    OmaggiareAccessori = 73
    VisualizzareCostiBroker = 80
    AnnullareEripristinarePrenotazioni = 85
    ModificaPrenotazioniBroker = 114
    TargaSempreModificabile = 88
    AnnullareQuickCheckIn = 102
    Fatturazione = 134
    EliminarePagamenti = 153
    Contatore_ra = 154
    '--------------------------------------------
    'TABELLE LISTINI ----------------------------
    tipoClienti = 79
    AddebitoAccessoriPersi = 120
    Carburante = 91
    CarburanteTutteLeStazioni = 92
    ElementiCondizioni = 40
    ConsegnaFuoriOrario = 78
    GiorniDiNoloDaPreautorizzare = 96
    GiorniDiNoloDaPreautorizzareAdmin = 99
    MinutiModificaTarga = 94
    MinutiModificaTargaAdmin = 95
    ElementiPercentuale = 68
    ScontoSuFranchigia = 100
    ScontoSuFranchigiaAdmin = 101
    SpeseSpedizioneFattura = 115
    TipoTariffa = 48
    FontiCommissionabili = 125
    ValSatellitare = 133
    MinutiRaVoid = 147
    SogliaArrotondamentoPrepagato = 149
    FranchigieParziali = 150
    ' Riepilo POS
    Riepilogo_Pagamenti_POS = 126
    Riepilogo_Pagamenti_POS_Admin = 127
    ' Petty Cash
    GestionePettyCash = 128
    GestioneBustaPettyCash = 129
    GestionePettyCashAdmin = 130
    ' Cassa
    GestioneCassa = 131
    GestioneCassaAdmin = 132
    GestioneCassaModificaImporti = 140
    CassaModificaGiorniRicerca = 152

    Commissioni = 156

    PagamentoContanti = 139
    PagamentoFull_Credit = 142
    PagamentoComplimentary = 143
    PagamentoAbbuoni = 144

    'OPERATORI
    GestioneOperatori = 21
    PermessiOperatori = 135
    AnagraficaOperatoreAdmin = 136
    AnagraficaOperatoreAccesso = 137

    'FUNZIONI VEICOLI
    TrasferimentoVeicoli = 74
    TrasferimentoVeicoliAdmin = 111
    Bisarca = 151
    ricercaVeicoli = 87
    PrevisioneStazione = 75
    StopSale = 60
    Ammortamenti = 23
    Assicurazioni = 6
    BolliAuto = 49
    PrevisionePerTarga = 76
    Rifornimenti = 110
    RifornimentoAdmin = 145
    GestioneLavaggi = 157

    'TARIFFE
    Condizioni = 47
    TempoKm = 46
    Tariffe = 45

    'POS
    CambioStazionePagamento = 141   '21.04.2022 aggiunto



End Enum

Public Class EventoConOggetto
    Inherits EventArgs

    Dim m_mioOggetto As Object

    Public Property mioOggetto() As Object
        Get
            Return m_mioOggetto
        End Get
        Set(ByVal value As Object)
            m_mioOggetto = value
        End Set
    End Property
End Class

Public Class Libreria

    Public Shared Function ListControlSelectvalueSicuro(list As ListControl, Valore As String, Optional Descrizione As String = "") As Boolean
        ListControlSelectvalueSicuro = True
        Dim NuovoEl As ListItem = list.Items.FindByValue(Valore)
        If NuovoEl Is Nothing Then
            If Descrizione = "" Then
                NuovoEl = New ListItem("N.V.(" & Valore & ")", Valore)
            Else
                NuovoEl = New ListItem(Descrizione, Valore)
            End If
            ListControlSelectvalueSicuro = False
            list.Items.Add(NuovoEl)
        End If

        list.SelectedValue = Valore
    End Function

    Public Shared Function ArrotondaDouble(valore As Double) As Double
        Return (Math.Truncate(valore * 100 + 0.5) / 100)
    End Function

    Public Shared Function verificaValoreIntero(Valore As String, Optional Segno As Integer = 0) As Boolean
        Dim ValoreIntero As Integer
        verificaValoreIntero = False
        Try
            ValoreIntero = Integer.Parse(Valore)
            If Segno > 0 Then
                If ValoreIntero < 0 Then
                    Return verificaValoreIntero
                End If
            ElseIf Segno < 0 Then
                If ValoreIntero > 0 Then
                    Return verificaValoreIntero
                End If
            End If
            verificaValoreIntero = True
        Catch ex As Exception

        End Try
    End Function

    Public Shared Function getDateDaStr(DataInput As String) As DateTime?
        HttpContext.Current.Trace.Write("getDateDaStr: " & DataInput)
        Dim MiaData As DateTime? = Nothing
        Try
            MiaData = New DateTime(Year(DataInput), Month(DataInput), Day(DataInput), Hour(DataInput), Minute(DataInput), Second(DataInput))
        Catch ex As Exception
            HttpContext.Current.Trace.Write("Data input errata: " & (MiaData Is Nothing))
        End Try
        Return MiaData
    End Function

    ''' <summary>
    ''' Permette di formattare la data fornita in ingresso 
    ''' YYYY-MM-dd oppure yyyy-dd-MM
    ''' in funzione del server sql in esecuzione.
    ''' Allo scopo viene utilizzata la variabile di sessione HTTP_HOST
    ''' non so dove sia stata definita...
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FormattaData(ByVal DataInput As String) As String
        FormattaData = Year(DataInput) & "-" & Month(DataInput) & "-" & Day(DataInput)

        'If HttpContext.Current.Request.ServerVariables("HTTP_HOST") = "sviluppo.sicilybycar.it" Or HttpContext.Current.Request.ServerVariables("HTTP_HOST") = "src.entermed.it" Then
        '    FormattaData = Year(DataInput) & "-" & Day(DataInput) & "-" & Month(DataInput)
        'Else
        '    FormattaData = Year(DataInput) & "-" & Month(DataInput) & "-" & Day(DataInput)
        'End If
        'HttpContext.Current.Trace.Write("HTTP_HOST (" & HttpContext.Current.Request.ServerVariables("HTTP_HOST") & ") FormattaData (" & FormattaData & ")")
    End Function
    Public Shared Function FormattaDataItaliana(ByVal DataInput As String) As String
        FormattaDataItaliana = Day(DataInput) & "/" & Month(DataInput) & "/" & Year(DataInput)
        'HttpContext.Current.Trace.Write("HTTP_HOST (" & HttpContext.Current.Request.ServerVariables("HTTP_HOST") & ") FormattaData (" & FormattaData & ")")
    End Function

    Public Shared Function FormattaDataOreMinSec(DataInput As String) As String
        'If HttpContext.Current.Request.ServerVariables("HTTP_HOST") = "sviluppoares.sicilyrentcar.it" Or HttpContext.Current.Request.ServerVariables("HTTP_HOST") = "ares.sicilyrentcar.it" Then
        'FormattaDataOreMinSec = Year(DataInput) & "-" & Day(DataInput) & "-" & Month(DataInput) & " " & Hour(DataInput) & ":" & Minute(DataInput) & ":" & Second(DataInput)
        'Else
        FormattaDataOreMinSec = Year(DataInput) & "-" & Month(DataInput) & "-" & Day(DataInput) & " " & Hour(DataInput) & ":" & Minute(DataInput) & ":" & Second(DataInput)
        'End If
        'HttpContext.Current.Trace.Write("HTTP_HOST (" & HttpContext.Current.Request.ServerVariables("HTTP_HOST") & ") FormattaData (" & FormattaData & ")")
    End Function

    Public Shared Function FormattaDataOreMinSecProduzione(DataInput As String) As String
        'FormattaDataOreMinSecProduzione = Year(DataInput) & "-" & Day(DataInput) & "-" & Month(DataInput) & " " & Hour(DataInput) & ":" & Minute(DataInput) & ":" & Second(DataInput)
        FormattaDataOreMinSecProduzione = Year(DataInput) & "-" & Month(DataInput) & "-" & Day(DataInput) & " " & Hour(DataInput) & ":" & Minute(DataInput) & ":" & Second(DataInput)
    End Function

    Public Shared Function FormattaDataNull(DataInput As String) As String
        If DataInput = "" Then
            FormattaDataNull = "NULL"
        Else
            Try
                Dim miaData As Date = New Date(Year(DataInput), Month(DataInput), Day(DataInput))
                FormattaDataNull = "'" & FormattaData(DataInput) & "'"
            Catch ex As Exception
                FormattaDataNull = "NULL"
            End Try
        End If
    End Function

    Public Shared Function FormattaDataOreMinSecNull(DataInput As String) As String
        If DataInput = "" Then
            FormattaDataOreMinSecNull = "NULL"
        Else
            Try
                Dim miaData As DateTime = New DateTime(Year(DataInput), Month(DataInput), Day(DataInput), Hour(DataInput), Minute(DataInput), Second(DataInput))
                FormattaDataOreMinSecNull = "'" & FormattaDataOreMinSec(DataInput) & "'"
            Catch ex As Exception
                FormattaDataOreMinSecNull = "NULL"
            End Try
        End If
    End Function

    'Public Shared Function gestioneNull(myObject As Object) As String
    '    If myObject = DBNull.Value Then
    '        gestioneNull = " NULL "
    '    Else
    '        gestioneNull = "'" & myObject & "'"
    '    End If
    'End Function

    Public Shared Function TrimSicuro(input As String) As String
        If input Is Nothing Then
            Return Nothing
        End If
        Return input.Trim()
    End Function

    Public Shared Function TrimSicuro(input As String, MaxLen As Integer) As String
        If input Is Nothing Then
            Return Nothing
        End If
        Return SubstringSicuro(input.Trim(), MaxLen)
    End Function

    Public Shared Function formattaSqlTrim(input As String) As String
        Return formattaSql(input).Trim()
    End Function

    Public Shared Function formattaSql(input As String) As String
        If input Is Nothing Then
            Return ""
        End If
        Return input.Replace("'", "''")
    End Function

    Public Shared Function formattaSqlTrim(input As String, MaxLen As Integer) As String
        Return formattaSql(input, MaxLen).Trim()
    End Function

    Public Shared Function SubstringSicuro(input As String, MaxLen As Integer) As String
        If input Is Nothing Then
            Return ""
        End If
        If MaxLen < 0 Then
            MaxLen = 0
        End If
        Dim R As String = input
        If R.Length > MaxLen Then
            R = R.Substring(0, MaxLen)
        End If
        Return R
    End Function

    Public Shared Function SubstringSicuro(input As String, Start As Integer, Length As Integer) As String
        If input Is Nothing Then
            Return ""
        End If
        Dim R As String = input
        If R.Length < Start Then
            Return ""
        End If
        If R.Length > Start + Length Then
            R = R.Substring(Start, Length)
        Else
            R = R.Substring(Start, R.Length - Start)
        End If
        Return R
    End Function

    Public Shared Function formattaSql(input As String, MaxLen As Integer) As String
        Return SubstringSicuro(input, MaxLen).Replace("'", "''")
    End Function

    Public Shared Function FormattaRealeNull(ByVal O As Object) As String
        If O Is Nothing OrElse O Is DBNull.Value Then
            Return "NULL"
        End If
        Return Replace(O & "", ",", ".")
    End Function

    Public Shared Function ValoreOppureNull(ByVal O As Object) As String
        If O Is Nothing Then
            Return "NULL"
        End If
        Return (O & "")
    End Function

    Public Shared Function ValoreOrNull(ByVal O As Object) As Object
        If O Is Nothing Then
            Return DBNull.Value
        End If
        Return O
    End Function

    Public Shared Function ValoreOrNothing(ByVal O As Object) As Object
        If O Is DBNull.Value Then
            HttpContext.Current.Trace.Write("ValoreOrNothing: Nothing")
            Return Nothing
        End If
        HttpContext.Current.Trace.Write("ValoreOrNothing: " & O)
        Return O
    End Function

    Public Shared Sub genUserMsgBox(ByVal F As System.Web.UI.Page, ByVal sMsg As String)
        'Dim sb As New StringBuilder()
        'Dim oFormObject As System.Web.UI.Control = Nothing
        'Try
        '    sMsg = sMsg.Replace("'", "\'")
        '    sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
        '    sMsg = sMsg.Replace(vbCrLf, "\n")
        '    sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
        '    sb = New StringBuilder()
        '    sb.Append(sMsg)
        '    For Each oFormObject In F.Controls
        '        If TypeOf oFormObject Is HtmlForm Then
        '            Exit For
        '        End If
        '    Next
        '    oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        'Catch ex As Exception

        'End Try

        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            'sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sMsg = "alert('" & sMsg & "')"

            'sb = New StringBuilder()
            'sb.Append(sMsg)

            ScriptManager.RegisterClientScriptBlock(F, F.GetType(), "clientScript", sMsg, True)

            'Page.ClientScript.RegisterStartupScript([GetType], "MyScript", "<script>alert('hiiiii Shoyebaziz123 ')</script>")

            'For Each oFormObject In F.Controls
            '    If TypeOf oFormObject Is HtmlForm Then
            '        Exit For
            '    End If
            'Next
            'oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Function IIF(Test As Boolean, Valore1 As Object, Valore2 As Object) As Object
        If Test Then
            Return Valore1
        Else
            Return Valore2
        End If
    End Function

    Public Shared Function getAliquotaIVADaId(id_iva As Integer) As Double

        Dim sqlStr As String = "SELECT aliquota FROM aliquote_iva WITH(NOLOCK) WHERE id = " & id_iva

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

    Public Shared Function getDescrizioneAliquotaIVADaId(ByVal id_iva As Integer) As String

        Dim sqlStr As String = "SELECT descrizione FROM aliquote_iva WITH(NOLOCK) WHERE id = " & id_iva

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

    Public Shared Function getNomeOperatoreDaId(ByVal id_operatore As Integer?) As String
        If id_operatore Is Nothing Then
            Return ""
        End If

        Dim sqlStr As String = "SELECT cognome + ' ' + nome FROM operatori WITH(NOLOCK) WHERE id = " & id_operatore

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()

                Return Cmd.ExecuteScalar & ""
            End Using
        End Using
    End Function

    Public Shared Function getNomeStazioneDaId(ByVal id_stazione As Integer?) As String
        If id_stazione Is Nothing Then
            Return ""
        End If

        Dim sqlStr As String = "SELECT codice + ' - ' + nome_stazione FROM stazioni WITH(NOLOCK) WHERE id = " & id_stazione

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()

                Return Cmd.ExecuteScalar & ""
            End Using
        End Using
    End Function

    Public Shared Function getNomeProprietarioDaId(ByVal id_proprietario As Integer?) As String
        If id_proprietario Is Nothing Then
            Return ""
        End If
        Dim sqlStr As String = "SELECT descrizione FROM proprietari_veicoli WITH(NOLOCK) WHERE id = " & id_proprietario

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()

                Return Cmd.ExecuteScalar & ""
            End Using
        End Using
    End Function

    Public Shared Function getNomeOrigineDaId(ByVal id_origine As Integer?, Optional ByVal codice_sintetico As Boolean = False) As String
        If id_origine Is Nothing Then
            Return ""
        End If
        ' SELECT id, descrizione + ' (' + codice_sintetico + ')' descrizione FROM veicoli_tipo_documento_apertura_danno WITH(NOLOCK) ORDER BY id
        Dim sqlStr As String
        If codice_sintetico Then
            sqlStr = "SELECT descrizione + ' (' + codice_sintetico + ')' descrizione FROM veicoli_tipo_documento_apertura_danno WITH(NOLOCK) WHERE id = " & id_origine
        Else
            sqlStr = "SELECT descrizione FROM veicoli_tipo_documento_apertura_danno WITH(NOLOCK) WHERE id = " & id_origine
        End If

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()

                Return Cmd.ExecuteScalar & ""
            End Using
        End Using
    End Function

    Public Shared Function myFormatta(Valore As Object, str As String) As String
        If Valore Is DBNull.Value Then
            Return Format(Nothing, str)
        Else
            Return Format(Valore, str)
        End If
    End Function

    Public Shared Function getControCassa(ByVal cassa As String) As String
        If cassa <> "" Then
            Dim sqlStr As String
            sqlStr = "SELECT contro_cassa FROM pos WHERE cassa = " & cassa

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Return Cmd.ExecuteScalar & ""
                End Using
            End Using
        Else
            Return "(N.V.)"
        End If
    End Function

    Public Shared Function getComuneAres(ByVal id_comune As Integer) As String
        Dim sqlStr As String = "SELECT comune + ' (' + provincia + ')' FROM comuni_ares WITH(NOLOCK)" & _
            " WHERE id = " & id_comune
        HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getComuneAres = Cmd.ExecuteScalar() & ""
            End Using
        End Using
    End Function

    Public Shared Function NascondiNumeroCarta(ByVal num_carta As String) As String
        If num_carta Is Nothing Then
            Return ""
        End If
        Dim asterischi As String = ""
        If num_carta.Length < 10 Then
            Return asterischi.PadRight(num_carta.Length, "*")
        End If
        Return num_carta.Substring(0, 6) & asterischi.PadRight(num_carta.Length - 10, "*") & num_carta.Substring(num_carta.Length - 4)
    End Function
End Class
