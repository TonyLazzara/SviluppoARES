Imports System.IO
Imports iTextSharp.text.pdf

Public Class StampaModelloMulte
    Private _IdMulta As String
    Private _IdModello As Integer
    Private _TipoAllegato As String
    Private _NomeModello As String
    Private _PercorsoModello As String
    Private _Prot As String
    Private _Anno As String
    Private _Ente As String
    Private _IndirizzoEnte As String
    Private _CapEnte As String
    Private _ComuneEnte As String
    Private _ProvEnte As String
    Private _DataInserimento As String
    Private _NumVerbale As String
    Private _ImportoVerbale As String
    Private _ImportoAddServFee As String
    Private _DataNotifica As String
    Private _DataVerbale As String
    Private _OraVerbale As String
    Private _ArtCDS As String
    Private _Targa As String
    Private _contratto As String
    Private _DataInizioNolo As String
    Private _DataFineNolo As String
    Private _Conducente As String
    Private _LuogoNascitaCond As String
    Private _DataNascitaCond As String
    Private _IndirizzoCond As String
    Private _ComuneComplCond As String
    Private _Nazione As String
    Private _PatenteCateg As String
    Private _PatenteNum As String
    Private _LuogoRilascioPatente As String
    Private _DataRilascioPatente As String
    Private _DataScadPatente As String
    Private _DataVendita As String
    Private _ModelloAutoCorretto As String
    Private _ModelloAutoErrato As String
    Private _NumPrecVerb As String
    Private _DataPrecVerb As String
    Private _OraPrecVerb As String
    Private _DataNotificaPrecVerb As String
    Private _PuntiDecurtPrecVerb As String
    Private _NumRaccPrecVerb As String
    Private _DataRaccPrecVerb As String
    Private _IndirInfraz As String
    Private _DescrizAltroLuogo As String
    Private _LuogoDenuncia As String
    Private _DataDenuncia As String
    Private _AllegatiRicorso As String
    Private _SalvaComeAllegato As Boolean

    Public Sub New()

    End Sub

    Public Property IdMulta() As String
        Get
            Return _IdMulta
        End Get
        Set(ByVal value As String)
            _IdMulta = value
        End Set
    End Property

    Public Property IdModello() As Integer
        Get
            Return _IdModello
        End Get
        Set(ByVal value As Integer)
            _IdModello = value
        End Set
    End Property

    Public Property TipoAllegato() As String
        Get
            Return _TipoAllegato
        End Get
        Set(ByVal value As String)
            _TipoAllegato = value
        End Set
    End Property

    Public Property NomeModello() As String
        Get
            Return _NomeModello
        End Get
        Set(ByVal value As String)
            _NomeModello = value
        End Set
    End Property

    Public Property PercorsoModello() As String
        Get
            Return _PercorsoModello
        End Get
        Set(ByVal value As String)
            _PercorsoModello = value
        End Set
    End Property

    Public Property Prot() As String
        Get
            Return _Prot
        End Get
        Set(ByVal value As String)
            _Prot = value
        End Set
    End Property

    Public Property Anno() As String
        Get
            Return _Anno
        End Get
        Set(ByVal value As String)
            _Anno = value
        End Set
    End Property

    Public Property Ente() As String
        Get
            Return _Ente
        End Get
        Set(ByVal value As String)
            _Ente = value
        End Set
    End Property

    Public Property IndirizzoEnte() As String
        Get
            Return _IndirizzoEnte
        End Get
        Set(ByVal value As String)
            _IndirizzoEnte = value
        End Set
    End Property

    Public Property CapEnte() As String
        Get
            Return _CapEnte
        End Get
        Set(ByVal value As String)
            _CapEnte = value
        End Set
    End Property

    Public Property ComuneEnte() As String
        Get
            Return _ComuneEnte
        End Get
        Set(ByVal value As String)
            _ComuneEnte = value
        End Set
    End Property

    Public Property ProvEnte() As String
        Get
            Return _ProvEnte
        End Get
        Set(ByVal value As String)
            _ProvEnte = value
        End Set
    End Property

    Public Property DataInserimento() As String
        Get
            Return _DataInserimento
        End Get
        Set(ByVal value As String)
            _DataInserimento = value
        End Set
    End Property

    Public Property NumVerbale() As String
        Get
            Return _NumVerbale
        End Get
        Set(ByVal value As String)
            _NumVerbale = value
        End Set
    End Property

    Public Property ImportoVerbale() As String
        Get
            Return _ImportoVerbale
        End Get
        Set(ByVal value As String)
            _ImportoVerbale = value
        End Set
    End Property

    Public Property ImportoAddServFee() As String
        Get
            Return _ImportoAddServFee
        End Get
        Set(ByVal value As String)
            _ImportoAddServFee = value
        End Set
    End Property

    Public Property DataNotifica() As String
        Get
            Return _DataNotifica
        End Get
        Set(ByVal value As String)
            _DataNotifica = value
        End Set
    End Property

    Public Property DataVerbale() As String
        Get
            Return _DataVerbale
        End Get
        Set(ByVal value As String)
            _DataVerbale = value
        End Set
    End Property

    Public Property OraVerbale() As String
        Get
            Return _OraVerbale
        End Get
        Set(ByVal value As String)
            _OraVerbale = value
        End Set
    End Property

    Public Property ArtCDS() As String
        Get
            Return _ArtCDS
        End Get
        Set(ByVal value As String)
            _ArtCDS = value
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

    Public Property Contratto() As String
        Get
            Return _contratto
        End Get
        Set(ByVal value As String)
            _contratto = value
        End Set
    End Property

    Public Property DataInizioNolo() As String
        Get
            Return _DataInizioNolo
        End Get
        Set(ByVal value As String)
            _DataInizioNolo = value
        End Set
    End Property

    Public Property DataFineNolo() As String
        Get
            Return _DataFineNolo
        End Get
        Set(ByVal value As String)
            _DataFineNolo = value
        End Set
    End Property

    Public Property Conducente() As String
        Get
            Return _Conducente
        End Get
        Set(ByVal value As String)
            _Conducente = value
        End Set
    End Property

    Public Property LuogoNascitaCond() As String
        Get
            Return _LuogoNascitaCond
        End Get
        Set(ByVal value As String)
            _LuogoNascitaCond = value
        End Set
    End Property

    Public Property DataNascitaCond() As String
        Get
            Return _DataNascitaCond
        End Get
        Set(ByVal value As String)
            _DataNascitaCond = value
        End Set
    End Property

    Public Property IndirizzoCond() As String
        Get
            Return _IndirizzoCond
        End Get
        Set(ByVal value As String)
            _IndirizzoCond = value
        End Set
    End Property

    Public Property ComuneComplCond() As String
        Get
            Return _ComuneComplCond
        End Get
        Set(ByVal value As String)
            _ComuneComplCond = value
        End Set
    End Property

    Public Property Nazione() As String
        Get
            Return _Nazione
        End Get
        Set(ByVal value As String)
            _Nazione = value
        End Set
    End Property

    Public Property PatenteCateg() As String
        Get
            Return _PatenteCateg
        End Get
        Set(ByVal value As String)
            _PatenteCateg = value
        End Set
    End Property

    Public Property PatenteNum() As String
        Get
            Return _PatenteNum
        End Get
        Set(ByVal value As String)
            _PatenteNum = value
        End Set
    End Property

    Public Property LuogoRilascioPatente() As String
        Get
            Return _LuogoRilascioPatente
        End Get
        Set(ByVal value As String)
            _LuogoRilascioPatente = value
        End Set
    End Property

    Public Property DataRilascioPatente() As String
        Get
            Return _DataRilascioPatente
        End Get
        Set(ByVal value As String)
            _DataRilascioPatente = value
        End Set
    End Property

    Public Property DataScadPatente() As String
        Get
            Return _DataScadPatente
        End Get
        Set(ByVal value As String)
            _DataScadPatente = value
        End Set
    End Property

    Public Property DataVendita() As String
        Get
            Return _DataVendita
        End Get
        Set(ByVal value As String)
            _DataVendita = value
        End Set
    End Property

    Public Property ModelloAutoCorretto() As String
        Get
            Return _ModelloAutoCorretto
        End Get
        Set(ByVal value As String)
            _ModelloAutoCorretto = value
        End Set
    End Property

    Public Property ModelloAutoErrato() As String
        Get
            Return _ModelloAutoErrato
        End Get
        Set(ByVal value As String)
            _ModelloAutoErrato = value
        End Set
    End Property

    Public Property NumPrecVerb() As String
        Get
            Return _NumPrecVerb
        End Get
        Set(ByVal value As String)
            _NumPrecVerb = value
        End Set
    End Property

    Public Property DataPrecVerb() As String
        Get
            Return _DataPrecVerb
        End Get
        Set(ByVal value As String)
            _DataPrecVerb = value
        End Set
    End Property

    Public Property OraPrecVerb() As String
        Get
            Return _OraPrecVerb
        End Get
        Set(ByVal value As String)
            _OraPrecVerb = value
        End Set
    End Property

    Public Property DataNotificaPrecVerb() As String
        Get
            Return _DataNotificaPrecVerb
        End Get
        Set(ByVal value As String)
            _DataNotificaPrecVerb = value
        End Set
    End Property

    Public Property PuntiDecurtPrecVerb() As String
        Get
            Return _PuntiDecurtPrecVerb
        End Get
        Set(ByVal value As String)
            _PuntiDecurtPrecVerb = value
        End Set
    End Property

    Public Property NumRaccPrecVerb() As String
        Get
            Return _NumRaccPrecVerb
        End Get
        Set(ByVal value As String)
            _NumRaccPrecVerb = value
        End Set
    End Property

    Public Property DataRaccPrecVerb() As String
        Get
            Return _DataRaccPrecVerb
        End Get
        Set(ByVal value As String)
            _DataRaccPrecVerb = value
        End Set
    End Property

    Public Property IndirInfraz() As String
        Get
            Return _IndirInfraz
        End Get
        Set(ByVal value As String)
            _IndirInfraz = value
        End Set
    End Property

    Public Property DescrizAltroLuogo() As String
        Get
            Return _DescrizAltroLuogo
        End Get
        Set(ByVal value As String)
            _DescrizAltroLuogo = value
        End Set
    End Property

    Public Property LuogoDenuncia() As String
        Get
            Return _LuogoDenuncia
        End Get
        Set(ByVal value As String)
            _LuogoDenuncia = value
        End Set
    End Property

    Public Property DataDenuncia() As String
        Get
            Return _DataDenuncia
        End Get
        Set(ByVal value As String)
            _DataDenuncia = value
        End Set
    End Property

    Public Property AllegatiRicorso() As String
        Get
            Return _AllegatiRicorso
        End Get
        Set(ByVal value As String)
            _AllegatiRicorso = value
        End Set
    End Property

    Public Property SalvaComeAllegato() As Boolean
        Get
            Return _SalvaComeAllegato
        End Get
        Set(ByVal value As Boolean)
            _SalvaComeAllegato = value
        End Set
    End Property

    Public Function GeneraDocumento() As MemoryStream
        'Dim PercFilePdf As String = "Rinotifica.pdf"
        'fare una funzione che mi restituisce il nome del file in base 
        'al parametro modello passato
        Dim PercFilePdf As String = PercorsoModello

        Dim pdfTemplate As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & PercFilePdf
        Dim PdfReader As PdfReader = New PdfReader(pdfTemplate)
        Dim PdfStamper As PdfStamper = Nothing
        Dim ms As MemoryStream = Nothing

        Try
            ms = New MemoryStream()
            PdfStamper = New PdfStamper(PdfReader, ms)
            Dim pdfFormFields As AcroFields = PdfStamper.AcroFields

            pdfFormFields.SetField("IdModello", IdModello)
            pdfFormFields.SetField("Rifer", Prot & "/" & Anno)
            pdfFormFields.SetField("Ente", Ente)
            pdfFormFields.SetField("IndirizzoEnte", IndirizzoEnte)
            pdfFormFields.SetField("EnteEsteso", Ente)
            pdfFormFields.SetField("ComuneCompleto", CapEnte & " " & ComuneEnte & " (" & ProvEnte & ")")
            pdfFormFields.SetField("CapEnte", CapEnte)
            pdfFormFields.SetField("ComuneEnte", ComuneEnte)
            pdfFormFields.SetField("ProvEnte", ProvEnte)
            pdfFormFields.SetField("DataInserimento", DataInserimento)
            pdfFormFields.SetField("NumVerbale", NumVerbale)
            pdfFormFields.SetField("DataVerbaleSolo", DataVerbale)
            pdfFormFields.SetField("DataVerbale", DataVerbale & " " & OraVerbale)
            pdfFormFields.SetField("OraVerbale", OraVerbale)
            pdfFormFields.SetField("ArtCDS", ArtCDS)
            pdfFormFields.SetField("DataNotifica", DataNotifica)
            pdfFormFields.SetField("ImportoAddServFee", ImportoAddServFee)
            pdfFormFields.SetField("ImportoVerbale", ImportoVerbale)
            pdfFormFields.SetField("ImportoVerbale_e_Fee", CDbl(ImportoVerbale) + CDbl(ImportoAddServFee))
            pdfFormFields.SetField("Targa", Targa)
            pdfFormFields.SetField("Contratto", Contratto)
            pdfFormFields.SetField("DataInizioNolo", DataInizioNolo)
            pdfFormFields.SetField("DataFineNolo", DataFineNolo)
            pdfFormFields.SetField("Conducente", Conducente)
            pdfFormFields.SetField("LuogoDataNascitaCond", LuogoNascitaCond & "  il  " & DataNascitaCond)
            If LuogoNascitaCond <> "" Then
                pdfFormFields.SetField("LuogoDataNascitaCond2", "nato a " & LuogoNascitaCond & "  il  " & DataNascitaCond)
            Else
                pdfFormFields.SetField("LuogoDataNascitaCond2", " ")
            End If
            pdfFormFields.SetField("DataNascitaCond", DataNascitaCond)
            pdfFormFields.SetField("IndirizzoCond", IndirizzoCond)
            If Nazione <> "" Then
                pdfFormFields.SetField("CittaNazione", ComuneComplCond & "   nazione " & Nazione)
            Else
                pdfFormFields.SetField("CittaNazione", ComuneComplCond)
            End If
            pdfFormFields.SetField("Nazione", Nazione)
            pdfFormFields.SetField("PatenteCompleta", "numero " & PatenteNum & "  categ. " & PatenteCateg & "  rilasciata a " & LuogoRilascioPatente & "  il " & DataRilascioPatente)
            'pdfFormFields.SetField("PatenteCateg", PatenteCateg)
            'pdfFormFields.SetField("PatenteNum", PatenteNum)
            'pdfFormFields.SetField("LuogoRilascioPatente", LuogoRilascioPatente)
            'pdfFormFields.SetField("DataRilascioPatente", DataRilascioPatente)
            pdfFormFields.SetField("DataScadPatente", DataScadPatente)
            pdfFormFields.SetField("DataVendita", DataVendita)
            pdfFormFields.SetField("ModelloAutoCorretto", ModelloAutoCorretto)
            pdfFormFields.SetField("ModelloAutoErrato", ModelloAutoErrato)
            pdfFormFields.SetField("NumPrecVerb", NumPrecVerb)
            pdfFormFields.SetField("DataPrecVerb", DataPrecVerb)
            pdfFormFields.SetField("OraPrecVerb", OraPrecVerb)
            pdfFormFields.SetField("DataNotificaPrecVerb", DataNotificaPrecVerb)
            pdfFormFields.SetField("PuntiDecurtPrecVerb", PuntiDecurtPrecVerb)
            pdfFormFields.SetField("NumRaccPrecVerb", NumRaccPrecVerb)
            pdfFormFields.SetField("DataRaccPrecVerb", DataRaccPrecVerb)
            pdfFormFields.SetField("IndirInfraz", IndirInfraz & ".")
            pdfFormFields.SetField("DescrizAltroLuogo", DescrizAltroLuogo)
            pdfFormFields.SetField("LuogoDenuncia", LuogoDenuncia)
            pdfFormFields.SetField("DataDenuncia", DataDenuncia)
            pdfFormFields.SetField("AllegatiRicorso", AllegatiRicorso)
            PdfStamper.FormFlattening = True
            PdfStamper.Close()

        Catch ex As Exception
            HttpContext.Current.Trace.Write("Errore in GeneraDocumento:" & ex.Message)
        End Try
        Return ms

    End Function

End Class
