Imports System.IO

Public Enum TipoOrientamento
    NoDef = 1
    Portrait
    Landscape
End Enum

Public Class ClassGeneraHtmlToPdf
    Public Class Parametri_wkhtmltopdf
        'http://madalgo.au.dk/~jakobt/wkhtmltoxdoc/wkhtmltopdf_0.10.0_rc2-doc.html
        ' elementi utilizzabili nelle sezioni di testo heade e footer...
        '* [page]       Replaced by the number of the pages currently being printed
        '* [frompage]   Replaced by the number of the first page to be printed
        '* [topage]     Replaced by the number of the last page to be printed
        '* [webpage]    Replaced by the URL of the page being printed
        '* [section]    Replaced by the name of the current section
        '* [subsection] Replaced by the name of the current subsection
        '* [date]       Replaced by the current date in system local format
        '* [time]       Replaced by the current time in system local format
        '* [title]      Replaced by the title of the of the current page object
        '* [doctitle]   Replaced by the title of the output document

        Private m_margin_bottom As Integer = -1
        Private m_margin_left As Integer = -1
        Private m_margin_right As Integer = -1
        Private m_margin_top As Integer = 15
        Private m_orientation As TipoOrientamento = TipoOrientamento.NoDef
        Private m_title As String

        Private m_header_font_size As Integer = -1 ' --header-font-size
        Private m_header_left As String = ""    ' --header-left
        Private m_header_center As String = ""  ' --header-center
        Private m_header_right As String = ""   ' --header-right
        Private m_header_html As String = ""    ' --header-html

        Private m_footer_font_size As Integer = 8
        Private m_footer_left As String = ""    ' --footer-left
        Private m_footer_center As String = "Pagina [page] di [toPage]"
        Private m_footer_right As String = "[date] [time]"   ' --footer-right
        Private m_footer_html As String = ""    ' --footer-html

        Public WriteOnly Property margin_bottom As Integer
            Set(value As Integer)
                m_margin_bottom = value
            End Set
        End Property
        Public WriteOnly Property margin_left As Integer
            Set(value As Integer)
                m_margin_left = value
            End Set
        End Property
        Public WriteOnly Property margin_right As Integer
            Set(value As Integer)
                m_margin_right = value
            End Set
        End Property
        Public WriteOnly Property margin_top As Integer
            Set(value As Integer)
                m_margin_top = value
            End Set
        End Property
        Public WriteOnly Property orientation As TipoOrientamento
            Set(value As TipoOrientamento)
                m_orientation = value
            End Set
        End Property
        Public WriteOnly Property title As String
            Set(value As String)
                m_title = value
            End Set
        End Property

        Public WriteOnly Property header_font_size As Integer
            Set(value As Integer)
                m_header_font_size = value
            End Set
        End Property
        Public WriteOnly Property header_html As String
            Set(value As String)
                m_header_html = value
            End Set
        End Property
        Public WriteOnly Property header_left As String
            Set(value As String)
                m_header_left = value
            End Set
        End Property
        Public WriteOnly Property header_center As String
            Set(value As String)
                m_header_center = value
            End Set
        End Property
        Public WriteOnly Property header_right As String
            Set(value As String)
                m_header_right = value
            End Set
        End Property

        Public WriteOnly Property footer_font_size As Integer
            Set(value As Integer)
                m_footer_font_size = value
            End Set
        End Property
        Public WriteOnly Property footer_left As String
            Set(value As String)
                m_footer_left = value
            End Set
        End Property
        Public WriteOnly Property footer_center As String
            Set(value As String)
                m_footer_center = value
            End Set
        End Property
        Public WriteOnly Property footer_right As String
            Set(value As String)
                m_footer_right = value
            End Set
        End Property
        Public WriteOnly Property footer_html As String
            Set(value As String)
                m_footer_html = value
            End Set
        End Property

        Public Function getParametri() As String
            Dim str As String = ""
            If m_margin_bottom >= 0 Then
                str += " --margin-bottom " & m_margin_bottom
            End If
            If m_margin_top >= 0 Then
                str += " --margin-top " & m_margin_top
            End If
            If m_margin_left >= 0 Then
                str += " --margin-left " & m_margin_left
            End If
            If m_margin_right >= 0 Then
                str += " --margin-right " & m_margin_right
            End If

            Select Case m_orientation
                Case TipoOrientamento.Landscape, TipoOrientamento.Portrait
                    str += " --orientation " & m_orientation.ToString
            End Select
            If m_title <> "" Then
                str += " --title """ & m_title & """"
            End If

            ' header
            If m_header_font_size >= 0 Then
                str += " --header-font-size " & m_header_font_size
            End If
            If m_header_left <> "" Then
                str += " --header-left """ & m_header_left & """"
            End If
            If m_header_center <> "" Then
                str += " --header-center """ & m_header_center & """"
            End If
            If m_header_right <> "" Then
                str += " --header-right """ & m_header_right & """"
            End If
            If m_header_html <> "" Then
                str += " --header-html " & m_header_html ' & " --header-spacing 5"
            End If

            ' footer
            If m_footer_font_size >= 0 Then
                str += " --footer-font-size " & m_footer_font_size
            End If
            If m_footer_left <> "" Then
                str += " --footer-left """ & m_footer_left & """"
            End If
            If m_footer_center <> "" Then
                str += " --footer-center """ & m_footer_center & """"
            End If
            If m_footer_right <> "" Then
                str += " --footer-right """ & m_footer_right & """"
            End If
            If m_footer_html <> "" Then
                str += " --footer-html " & m_footer_html ' & " --footer-spacing 5"
            End If

            ' str += " --top 40"

            HttpContext.Current.Trace.Write("getParametri->str: (" & str & ")")

            Return str & " "
        End Function

    End Class

    Private ReadOnly Property MiaPath() As String


        Get
            'Return Context.Request.ApplicationPath
            'Return (HttpContext.Current.Server.MapPath("~")).Replace("\", "/")
            Return ConfigurationManager.AppSettings.Get("PathAssolutaSitoPerPDF") ' "http://localhost/"

        End Get
    End Property

    Private Function ParseParametriStampa(querystring As String) As Parametri_wkhtmltopdf
        Dim ParamPdf As Parametri_wkhtmltopdf = New Parametri_wkhtmltopdf
        ' Parse the query string variables into a NameValueCollection.
        Dim qscoll As NameValueCollection = HttpUtility.ParseQueryString(querystring)
        Dim valore As String = Nothing
        valore = qscoll.Item("orientamento")
        If Not valore Is Nothing Then
            Select Case valore
                Case "verticale"
                    ParamPdf.orientation = TipoOrientamento.Portrait
                Case "orizzontale"
                    ParamPdf.orientation = TipoOrientamento.Landscape
            End Select
        End If
        valore = qscoll.Item("titolo")
        If Not valore Is Nothing Then
            ParamPdf.title = valore
        End If

        valore = qscoll.Item("margin_bottom")
        If Not valore Is Nothing Then
            ParamPdf.margin_bottom = Integer.Parse(valore)
        End If
        valore = qscoll.Item("margin_top")
        If Not valore Is Nothing Then
            ParamPdf.margin_top = Integer.Parse(valore)
        End If
        valore = qscoll.Item("margin_left")
        If Not valore Is Nothing Then
            ParamPdf.margin_left = Integer.Parse(valore)
        End If
        valore = qscoll.Item("margin_right")
        If Not valore Is Nothing Then
            ParamPdf.margin_right = Integer.Parse(valore)
        End If

        valore = qscoll.Item("header_font_size")
        If Not valore Is Nothing Then
            ParamPdf.header_font_size = Integer.Parse(valore)
        End If
        valore = qscoll.Item("header_left")
        If Not valore Is Nothing Then
            ParamPdf.header_left = valore
        End If
        valore = qscoll.Item("header_center")
        If Not valore Is Nothing Then
            ParamPdf.header_center = valore
        End If
        valore = qscoll.Item("header_right")
        If Not valore Is Nothing Then
            ParamPdf.header_right = valore
        End If
        valore = qscoll.Item("header_html")
        If Not valore Is Nothing Then
            ParamPdf.header_html = MiaPath & valore
        End If

        valore = qscoll.Item("footer_font_size")
        If Not valore Is Nothing Then
            ParamPdf.footer_font_size = Integer.Parse(valore)
        End If
        valore = qscoll.Item("footer_left")
        If Not valore Is Nothing Then
            ParamPdf.footer_left = valore
        End If
        valore = qscoll.Item("footer_center")

        If Not valore Is Nothing Then
            ParamPdf.footer_center = valore
        End If
        valore = qscoll.Item("footer_right")
        If Not valore Is Nothing Then
            ParamPdf.footer_right = valore
        End If
        valore = qscoll.Item("footer_html")
        If Not valore Is Nothing Then
            ParamPdf.footer_html = valore
        End If

        Return ParamPdf
    End Function

    Public Function GeneraHtmlToPdf(url_print As String) As MemoryStream
        'Dim NomeCompleto As String = MiaPath & url_print
        Dim NomeCompleto As String = url_print

        HttpContext.Current.Trace.Write("NomeCompleto: (" & NomeCompleto & ")")

        Dim querystring As String = ""
        Dim iqs As Int32 = NomeCompleto.IndexOf("?".ToCharArray())
        If (iqs >= 0) And iqs < NomeCompleto.Length - 1 Then
            querystring = NomeCompleto.Substring(iqs + 1)
        End If
        Dim parampdf As Parametri_wkhtmltopdf = ParseParametriStampa(querystring)

        Dim MioProcesso As ProcessLetturaSincrona = New ProcessLetturaSincrona
        Dim Eseguibile As String = ConfigurationManager.AppSettings.Get("PathPerStampaPdf") & "wkhtmltopdf.exe" ' 
        Dim Parametri As String = parampdf.getParametri() & NomeCompleto & " - "
        HttpContext.Current.Trace.Write("Eseguibile (" & Eseguibile & ") Parametri (" & Parametri & ")")
        Dim ms As MemoryStream = MioProcesso.EseguiExe(Eseguibile, Parametri)

        Return ms
    End Function


    Public Function GeneraHtmlToPdf(url_print As String, parampdf As Parametri_wkhtmltopdf) As MemoryStream
        Dim NomeCompleto As String = MiaPath & url_print

        HttpContext.Current.Trace.Write("NomeCompleto: (" & NomeCompleto & ")")

        Dim MioProcesso As ProcessLetturaSincrona = New ProcessLetturaSincrona
        Dim Eseguibile As String = ConfigurationManager.AppSettings.Get("PathPerStampaPdf") & "wkhtmltopdf.exe" ' 
        Dim Parametri As String = parampdf.getParametri() & NomeCompleto & " - "
        Dim ms As MemoryStream = MioProcesso.EseguiExe(Eseguibile, Parametri)

        Return ms
    End Function
End Class
