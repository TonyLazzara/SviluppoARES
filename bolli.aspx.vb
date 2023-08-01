Imports funzioni_comuni
Imports System.Collections.Generic
Imports System.Web

Public Enum Stato_Filtro_Bolli
    nondef = 0
    aperto = 1
    chiuso = 2
End Enum

Public Class Filtro_Bolli

    Inherits ITabellaDB
    Protected m_id As Integer
    Protected m_id_utente As Nullable(Of Integer)
    Protected m_data_creazione As Nullable(Of DateTime)
    Protected m_Mese As Nullable(Of Integer)
    Protected m_Anno As Nullable(Of Integer)
    Protected m_Modello As Nullable(Of Integer)
    Protected m_Proprietario As Nullable(Of Integer)
    Protected m_Leasing As Nullable(Of Integer)
    Protected m_DataAtto As Nullable(Of Date)
    Protected m_Targa As String
    Protected m_stato As Nullable(Of Integer)

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public ReadOnly Property id_utente() As Nullable(Of Integer)
        Get
            Return m_id_utente
        End Get
    End Property
    Public ReadOnly Property data_creazione() As Nullable(Of DateTime)
        Get
            Return m_data_creazione
        End Get
    End Property
    Public Property Mese() As Nullable(Of Integer)
        Get
            Return m_Mese
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_Mese = value
        End Set
    End Property
    Public Property Anno() As Nullable(Of Integer)
        Get
            Return m_Anno
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_Anno = value
        End Set
    End Property
    Public Property Modello() As Nullable(Of Integer)
        Get
            Return m_Modello
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_Modello = value
        End Set
    End Property
    Public Property Proprietario() As Nullable(Of Integer)
        Get
            Return m_Proprietario
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_Proprietario = value
        End Set
    End Property
    Public Property Leasing() As Nullable(Of Integer)
        Get
            Return m_Leasing
        End Get
        Set(ByVal value As Nullable(Of Integer))
            m_Leasing = value
        End Set
    End Property
    Public Property DataAtto() As Nullable(Of Date)
        Get
            Return m_DataAtto
        End Get
        Set(ByVal value As Nullable(Of Date))
            m_DataAtto = value
        End Set
    End Property
    Public Property Targa() As String
        Get
            Return m_Targa
        End Get
        Set(ByVal value As String)
            m_Targa = value
        End Set
    End Property
    Public ReadOnly Property stato() As Stato_Filtro_Bolli
        Get
            Return m_stato
        End Get
    End Property

    Public Overrides Function SalvaRecord(Optional RecuperaId As Boolean = False) As Integer

        Dim sqlStr As String = "INSERT INTO veicoli_filtro_bolli (id_utente,data_creazione,Mese,Anno,Modello,Proprietario,Leasing,DataAtto,Targa,stato)"
        sqlStr += " VALUES (@id_utente,convert(datetime,getdate(),102),@Mese,@Anno,@Modello,@Proprietario,@Leasing,@DataAtto,@Targa,1)"

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)
                    addParametro(Cmd, "@id_utente", System.Data.SqlDbType.Int, Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente")))
                    addParametro(Cmd, "@Mese", System.Data.SqlDbType.Int, Mese)
                    addParametro(Cmd, "@Anno", System.Data.SqlDbType.Int, Anno)
                    addParametro(Cmd, "@Modello", System.Data.SqlDbType.Int, Modello)
                    addParametro(Cmd, "@Proprietario", System.Data.SqlDbType.Int, Proprietario)
                    addParametro(Cmd, "@Leasing", System.Data.SqlDbType.Int, Leasing)
                    addParametro(Cmd, "@DataAtto", System.Data.SqlDbType.Date, DataAtto)
                    addParametro(Cmd, "@Targa", System.Data.SqlDbType.VarChar, Libreria.SubstringSicuro(Targa, 50))

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                End Using

                ' -------------------------------------------------------------------------
                'recupero l'id del nuopvo record
                sqlStr = "SELECT @@IDENTITY FROM veicoli_filtro_bolli WITH(NOLOCK)"

                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    m_id = Cmd.ExecuteScalar
                End Using
            End Using

        Catch ex As Exception
            HttpContext.Current.Response.Write("error_salvarecord_" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try


        Return m_id
    End Function

    Private Shared Function FillFiltro(Rs As System.Data.SqlClient.SqlDataReader) As Filtro_Bolli
        Dim mio_filtro As Filtro_Bolli = New Filtro_Bolli
        Try

            With mio_filtro
                .m_id = Rs("id")
                .m_id_utente = Rs("id_utente")
                .m_data_creazione = Rs("data_creazione")
                .Mese = Rs("Mese")
                .Anno = Rs("Anno")
                .Modello = Rs("Modello")
                .Proprietario = Rs("Proprietario")
                .Leasing = Rs("Leasing")
                If Rs("DataAtto") Is DBNull.Value Then
                    .DataAtto = Nothing
                Else
                    .DataAtto = Rs("DataAtto")
                End If
                .Targa = Rs("Targa") & ""
                If Rs("stato") Is DBNull.Value Then
                    .m_stato = 0
                Else
                    .m_stato = Rs("stato")
                End If

            End With
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_fillfiltro_" & ex.Message & "<br/>")
        End Try

        Return mio_filtro
    End Function

    Public Shared Function get_ultimo_filtro_aperto() As Filtro_Bolli
        Dim mio_filtro As Filtro_Bolli = Nothing
        Dim sqlStr As String = "SELECT TOP 1 * FROM veicoli_filtro_bolli WITH(NOLOCK) WHERE stato = 1 ORDER BY ID Desc"
        Try

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Using Rs = Cmd.ExecuteReader
                        If Rs.Read Then
                            mio_filtro = FillFiltro(Rs)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_getultimofileaperto_" & "<br/>" & sqlStr & "<br/>")
        End Try


        Return mio_filtro
    End Function

    Public Sub ChiudiFiltro()
        Dim sqlStr As String
        sqlStr = "DELETE veicoli_bolli"
        sqlStr += " FROM veicoli_bolli vb"
        sqlStr += " INNER JOIN veicoli_filtro_bolli vf ON vf.id = vb.id_filtro_bolli"
        sqlStr += " WHERE vf.stato = 2 AND vf.data_creazione < getdate()"

        Try

            HttpContext.Current.Trace.Write(sqlStr)

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                End Using
            End Using

            sqlStr = "UPDATE veicoli_filtro_bolli SET stato = " & Stato_Filtro_Bolli.chiuso & " WHERE Id = " & id

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_chiudifiltro_" & "<br/>" & sqlStr & "<br/>")
        End Try

    End Sub

End Class

Partial Class bolli
    Inherits System.Web.UI.Page

    ' Formula per il calcolo bollo: 
    ' http://www.quattroruote.it/bollo/
    ' http://www.aci.it/sezione-istituzionale/al-servizio-del-cittadino/guida-al-bollo-auto/regioni-e-province-autonome.html

    ' ------------------------------------------------------------------
    ' Esempio per la gestione dei check box con la paginazione
    ' utilizzando la viewstate (se funziona bellissimo)!
    ' http://evonet.com.au/maintaining-checkbox-state-in-a-listview/
    ' ------------------------------------------------------------------

    Dim funzioni As New funzioni_comuni

    Private Enum DivVisibile
        Nessuno = 0
        Ricerca = 1
        Elenco = 2
        Pulsanti = 4
    End Enum

    Private Enum Mesi
        Gennaio = 1
        Febbraio
        Marzo
        Aprile
        Maggio
        Giugno
        Luglio
        Agosto
        Settembre
        Ottobre
        Novembre
        Dicembre
    End Enum

    Private Sub Visibilita(Valore As DivVisibile)
        DivRicerca.Visible = Valore And DivVisibile.Ricerca
        DivElenco.Visible = Valore And DivVisibile.Elenco
        DivPulsanti.Visible = Valore And DivVisibile.Pulsanti
        ' btnAzzeraScadenzaBollo.Visible = Valore And DivVisibile.Elenco
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("SicilyRentCar") Is Nothing Then
            Response.Redirect("LogIn.aspx")
            Return
        End If
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 49) = "1" Then
            Visibilita(DivVisibile.Nessuno)
            Response.Redirect("default.aspx")
            Return
        End If

        If Not Page.IsPostBack Then

            Dim mio_filtro As Filtro_Bolli = Filtro_Bolli.get_ultimo_filtro_aperto
            If mio_filtro Is Nothing Then

                mio_filtro = New Filtro_Bolli
                With mio_filtro
                    .id = 0
                    .Mese = Month(Now)
                    .Anno = Year(Now)
                    .DataAtto = New Date(.Anno, .Mese, 1)
                    .Modello = 0
                    .Proprietario = 0
                    .Leasing = 0
                    .Targa = ""
                End With
                set_Filtro_Bolli(mio_filtro)
                set_interfaccia_da_filtro(mio_filtro)
                Visibilita(DivVisibile.Ricerca)
            Else
                set_Filtro_Bolli(mio_filtro)

                set_interfaccia_da_filtro(mio_filtro)

                BindElenco(mio_filtro)

                TotaleBollo(mio_filtro)

                Dim provenienza As String = Session("provenienza")
                If provenienza = "parco_veicoli" Then
                    Session("provenienza") = ""
                    Dim posizione_num_pagina_bollo As String = Session("posizione_num_pagina_bollo")
                    Session("posizione_num_pagina_bollo") = Nothing
                    If posizione_num_pagina_bollo IsNot Nothing Then
                        Dim DataPager1 As DataPager = ListBolli.FindControl("DataPager1")

                        DataPager1.SetPageProperties(DataPager1.PageSize * (Integer.Parse(posizione_num_pagina_bollo) - 1), DataPager1.MaximumRows, True)
                    End If
                End If


                Visibilita(DivVisibile.Ricerca Or DivVisibile.Elenco Or DivVisibile.Pulsanti)
            End If
        Else
            SqlDataSourceElencoBolli.SelectCommand = lb_SqlDataSourceElencoBolli.Text
        End If

        ' Literal1.Text = "<script type=""text/javascript"" language=""javascript"">function confermaStampa2() { var Valore = " & TotaleRecord() & "; if (Valore >= 1000) { var Messaggio = ""Il numero dei record filtrato è molto elevato ("" + Valore + "").\nIl tempo di elaborazione potrebbe essere lungo.\n\nSei sicuro di voler procedere?""; return(window.confirm (Messaggio)); } return 1; } </script>"
    End Sub

    Protected Sub ListBolli_ItemCommand(sender As Object, e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListBolli.ItemCommand
        Trace.Write("ListBolli_ItemCommand: " & e.CommandName)

        If e.CommandName = "Lente" Then
            Dim id_veicoloLabel As Label = e.Item.FindControl("id_veicoloLabel")
            Dim DataPager1 As DataPager = ListBolli.FindControl("DataPager1")

            Session("provenienza") = "bolli.aspx"
            Session("posizione_num_pagina_bollo") = (DataPager1.StartRowIndex \ DataPager1.PageSize) + 1

            Response.Redirect("parcoVeicoli.aspx?val=gen&veicolo=" & id_veicoloLabel.Text)
        End If
    End Sub

    ' ------------------------------------------------------------------
    ' Gestione CheckBox
    ' ------------------------------------------------------------------

    'Private ReadOnly Property IDs() As List(Of Integer)
    '    Get
    '        If Me.ViewState("IDs") Is Nothing Then
    '            Me.ViewState("IDs") = New List(Of Integer)()
    '        End If
    '        Return CType(Me.ViewState("IDs"), List(Of Integer))
    '    End Get
    'End Property

    'Private ReadOnly Property IdMesi() As Dictionary(Of Integer, Integer)
    '    '<asp:TextBox ID="MesiDaPagare" runat="server" MaxLength="2" Width="40px" Text=''></asp:TextBox>
    '    Get
    '        If Me.ViewState("IdMesi") Is Nothing Then
    '            Me.ViewState("IdMesi") = New Dictionary(Of Integer, Integer)()
    '        End If
    '        Return CType(Me.ViewState("IdMesi"), Dictionary(Of Integer, Integer))
    '    End Get
    'End Property

    Protected Sub ListBolli_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles ListBolli.ItemDataBound

        Dim lvi As ListViewDataItem = e.Item
        If (lvi.ItemType = ListViewItemType.DataItem) Then

            Dim chkSelect As CheckBox = CType(lvi.FindControl("chkSelect"), CheckBox)
            If (Not (chkSelect) Is Nothing) Then
                Dim selezionatoLabel As Label = CType(lvi.FindControl("selezionatoLabel"), Label)
                If selezionatoLabel.Text = True Then
                    chkSelect.Checked = True
                Else
                    chkSelect.Checked = False
                End If
                'Dim ID As Integer = Convert.ToInt32(ListBolli.DataKeys(lvi.DisplayIndex).Value)
                'chkSelect.Checked = Me.IDs.Contains(ID)
            End If

            Dim DropDownListMeseDaPagare As DropDownList = CType(lvi.FindControl("DropDownListMeseDaPagare"), DropDownList)
            If (Not (DropDownListMeseDaPagare) Is Nothing) Then
                Dim ID As Integer = Convert.ToInt32(ListBolli.DataKeys(lvi.DisplayIndex).Value)
                Dim mesi_bolloLabel As Label = CType(lvi.FindControl("mesi_bolloLabel"), Label)
                Dim mesi_bollo As Integer = Integer.Parse(mesi_bolloLabel.Text)

                Dim NumMinimoMesiBollo As Integer = mesi_bollo - 12
                Dim NumMassimoMesiBollo As Integer = mesi_bollo + 12
                DropDownListMeseDaPagare.Items.Clear()
                For i As Integer = NumMinimoMesiBollo To NumMassimoMesiBollo Step 4
                    If i > 0 And i < 13 Then
                        DropDownListMeseDaPagare.Items.Add(New ListItem(i, i))
                    End If
                Next

                '' recupero e imposto il valore selezionato della combo a partire da quanto conservato nel viewstate
                'If Me.IdMesi.ContainsKey(ID) Then
                '    DropDownListMeseDaPagare.SelectedValue = Me.IdMesi.Item(ID)
                'Else
                DropDownListMeseDaPagare.SelectedValue = mesi_bollo
                'End If

                Dim mese_scadenza_bollo As Label = CType(lvi.FindControl("mese_scadenza_bolloLabel"), Label)

                Dim data_scadenza_bolloLabel As Label = CType(lvi.FindControl("data_scadenza_bolloLabel"), Label)
                Dim data_scadenza_bollo As Date = Date.Parse(data_scadenza_bolloLabel.Text)
                Dim nuova_data_scadenza_bollo As Date = DateAdd(DateInterval.Month, Double.Parse(mesi_bollo), data_scadenza_bollo)

                Dim mese As Mesi = Month(nuova_data_scadenza_bollo)
                mese_scadenza_bollo.Text = mese.ToString

            End If

            Dim data_atto_venditaLabel As Label = CType(lvi.FindControl("data_atto_venditaLabel"), Label)
            If data_atto_venditaLabel.Text = "01/01/1900" Then
                data_atto_venditaLabel.Text = ""
            End If

            Dim data_bollo_autoLabel As Label = CType(lvi.FindControl("data_bollo_autoLabel"), Label)
            If data_atto_venditaLabel.Text = "01/01/1900" Then
                data_atto_venditaLabel.Text = ""
            End If

            Dim importoLabel As Label = CType(lvi.FindControl("importoLabel"), Label)
            If Not importoLabel Is Nothing Then
                Dim importo As Double = 0
                If importoLabel.Text.Trim <> "" Then
                    importo = Double.Parse(importoLabel.Text)
                End If
                importoLabel.Text = Format(importo, "0.00")
            End If

        End If
    End Sub

    'Protected Sub AddRowstoIDList()
    '    'For Each lvi As ListViewDataItem In ListBolli.Items

    '    '    Dim chkSelect As CheckBox = CType(lvi.FindControl("chkSelect"), CheckBox)

    '    '    If (Not (chkSelect) Is Nothing) Then
    '    '        Dim ID As Integer = Convert.ToInt32(ListBolli.DataKeys(lvi.DisplayIndex).Value)
    '    '        Trace.Write(chkSelect.Checked & " - " & ID)
    '    '        If (chkSelect.Checked AndAlso Not Me.IDs.Contains(ID)) Then
    '    '            Me.IDs.Add(ID)
    '    '        ElseIf (Not chkSelect.Checked AndAlso Me.IDs.Contains(ID)) Then
    '    '            Me.IDs.Remove(ID)
    '    '        End If
    '    '    End If

    '    'Next
    'End Sub

    'Protected Sub AddRowstoIdMesi()
    '    'For Each lvi As ListViewDataItem In ListBolli.Items

    '    '    Dim DropDownListMeseDaPagare As DropDownList = CType(lvi.FindControl("DropDownListMeseDaPagare"), DropDownList)

    '    '    If (Not (DropDownListMeseDaPagare) Is Nothing) Then
    '    '        Dim ID As Integer = Convert.ToInt32(ListBolli.DataKeys(lvi.DisplayIndex).Value)
    '    '        Dim Selectedvalue As Integer = Integer.Parse(DropDownListMeseDaPagare.SelectedValue)
    '    '        Trace.Write(Selectedvalue & " - " & ID)
    '    '        If Me.IdMesi.ContainsKey(ID) Then
    '    '            If Me.IdMesi.Item(ID) <> Selectedvalue Then
    '    '                Me.IdMesi.Remove(ID)
    '    '                Me.IdMesi.Add(ID, Selectedvalue)
    '    '            End If
    '    '        Else
    '    '            Me.IdMesi.Add(ID, Selectedvalue)
    '    '        End If
    '    '    End If

    '    'Next
    'End Sub

    Protected Sub UpdateRows_Mesi_Bollo(mio_filtro As Filtro_Bolli)

        Dim sqlStr As String = ""
        Try
            For Each lvi As ListViewDataItem In ListBolli.Items

                Dim chkSelect As CheckBox = CType(lvi.FindControl("chkSelect"), CheckBox)
                Dim DropDownListMeseDaPagare As DropDownList = CType(lvi.FindControl("DropDownListMeseDaPagare"), DropDownList)

                If (Not (DropDownListMeseDaPagare) Is Nothing) And (Not (chkSelect) Is Nothing) Then
                    Dim ID As Integer = Convert.ToInt32(ListBolli.DataKeys(lvi.DisplayIndex).Value)
                    Dim Selectedvalue As Integer = Integer.Parse(DropDownListMeseDaPagare.SelectedValue)
                    Dim selezionato As Integer = 0
                    If chkSelect.Checked Then
                        selezionato = 1
                    End If
                    Trace.Write(Selectedvalue & " - " & ID)

                    sqlStr = "UPDATE veicoli_bolli SET"
                    sqlStr += "mesi_bollo = " & Selectedvalue & ","
                    sqlStr += " importo = importoMese * " & Selectedvalue & ","
                    sqlStr += " selezionato = " & selezionato
                    sqlStr += " FROM veicoli_bolli vb "
                    sqlStr += " INNER JOIN veicoli v On v.id = vb.id_veicolo And vb.id_filtro_bolli = " & mio_filtro.id
                    sqlStr += " WHERE v.id = " & ID

                    Trace.Write(sqlStr)

                    Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                            Dbc.Open()
                            Cmd.ExecuteNonQuery()
                        End Using
                    End Using
                End If

            Next
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_UpdateRows_Mesi_bolli_" & "<br/>" & sqlStr & "<br/>")
        End Try

    End Sub

    Private Sub AggiornaScadenzaBollo(mio_filtro As Filtro_Bolli)
        Dim sqlStr As String = ""
        Try
            With mio_filtro
                Dim TrentunoGennaio As String = .Anno & "-01-31"

                Dim InizioMesePrecedente As Date = DateAdd(DateInterval.Month, -1, New Date(.Anno, .Mese, 1))
                Dim VentunoMesePrecedente As Date = DateAdd(DateInterval.Month, -1, New Date(.Anno, .Mese, 21))
                Dim InizioMeseCorrente As Date = New Date(.Anno, .Mese, 1)
                Dim VentunoMeseCorrente As Date = New Date(.Anno, .Mese, 21)

                'sqlStr = "UPDATE veicoli Set" & _
                '            " data_scadenza_bollo =  " & _
                '            " Case" & _
                '                " When v.data_immatricolazione < '" & Libreria.FormattaDataOreMinSec(InizioMesePrecedente) & "' THEN DATEADD(Month, " & (.Mese - 2) & " + vb.mesi_bollo , '" & Libreria.FormattaData(TrentunoGennaio) & "')" & _
                '                " WHEN v.data_immatricolazione >= '" & Libreria.FormattaDataOreMinSec(VentunoMesePrecedente) & "' AND v.data_immatricolazione < '" & Libreria.FormattaDataOreMinSec(InizioMeseCorrente) & "' THEN DATEADD(Month, " & (.Mese - 3) & " + vb.mesi_bollo , '" & Libreria.FormattaData(TrentunoGennaio) & "')" & _
                '                " WHEN v.data_immatricolazione >= '" & Libreria.FormattaDataOreMinSec(InizioMeseCorrente) & "' AND v.data_immatricolazione < '" & Libreria.FormattaDataOreMinSec(VentunoMeseCorrente) & "' THEN DATEADD(Month, " & (.Mese - 2) & " + vb.mesi_bollo , '" & Libreria.FormattaData(TrentunoGennaio) & "')" & _
                '                " ELSE NULL" & _
                '            " END" & _
                '            " FROM veicoli_bolli vb " & _
                '            " INNER JOIN veicoli v ON v.id = vb.id_veicolo AND vb.id_filtro_bolli = " & mio_filtro.id & _
                '            " WHERE vb.selezionato = 1"

                sqlStr = "UPDATE veicoli SET"
                sqlStr += " data_scadenza_bollo = DATEADD(month, vb.mesi_bollo + month(vb.scadenza_bollo) - 1, '" & Libreria.FormattaData(TrentunoGennaio) & "') "
                sqlStr += " FROM veicoli_bolli vb "
                sqlStr += " INNER JOIN veicoli v ON v.id = vb.id_veicolo AND vb.id_filtro_bolli = " & mio_filtro.id
                sqlStr += " WHERE vb.selezionato = 1"

                Trace.Write(sqlStr)

                Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                        Dbc.Open()
                        Cmd.ExecuteNonQuery()
                    End Using
                End Using
            End With
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_AggiornaScadenzaBollo_" & "<br/>" & sqlStr & "<br/>")
        End Try

    End Sub

    Private Function TotaleBollo(mio_filtro As Filtro_Bolli) As Double
        Dim sqlStr As String = ""
        Dim strImportoTotale As String
        Try
            sqlStr = "SELECT SUM(vb.importo) FROM veicoli_bolli vb WITH(NOLOCK)"
            sqlStr += " WHERE vb.id_filtro_bolli = " & mio_filtro.id
            sqlStr += " AND vb.selezionato = 1"
            Trace.Write(sqlStr)

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    strImportoTotale = Cmd.ExecuteScalar() & ""
                End Using
            End Using

            If strImportoTotale = "" Then
                strImportoTotale = "0"
            End If
            TotaleBollo = Double.Parse(strImportoTotale)
            ImportoTotaleLabel.Text = "<b>Importo Totale: </b>" & FormatNumber(TotaleBollo, 2)
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_TotaleBollo_" & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Protected Sub ListBolli_PagePropertiesChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.PagePropertiesChangingEventArgs) Handles ListBolli.PagePropertiesChanging
        Trace.Write("ListBolli_PagePropertiesChanging")
        'AddRowstoIDList()
        'AddRowstoIdMesi()

        Dim mio_filtro As Filtro_Bolli = get_Filtro_Bolli()
        UpdateRows_Mesi_Bollo(mio_filtro)
        TotaleBollo(mio_filtro)
    End Sub

    Protected Sub ListBolli_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewSortEventArgs) Handles ListBolli.Sorting
        Trace.Write("ListBolli_Sorting")
        'AddRowstoIDList()
        'AddRowstoIdMesi()

        Dim mio_filtro As Filtro_Bolli = get_Filtro_Bolli()
        UpdateRows_Mesi_Bollo(mio_filtro)

        TotaleBollo(mio_filtro)
    End Sub

    ' ------------------------------------------------------------------
    ' Fine Gestione CheckBox
    ' ------------------------------------------------------------------

    Protected Function condizioneWhere(mio_filtro As Filtro_Bolli) As String
        Dim condizione As String = ""

        With mio_filtro

            If .Targa <> "" Then
                condizione += " AND v.targa ='" & Libreria.formattaSqlTrim(.Targa) & "'"
            Else

                If .DataAtto Is Nothing Then
                    condizione += " AND v.data_atto_vendita IS NULL"
                Else
                    condizione += " AND (v.data_atto_vendita IS NULL OR v.data_atto_vendita >= CONVERT(DATETIME,'" & Libreria.FormattaData(.DataAtto) & "',102)"
                End If

                If .Proprietario > 0 Then
                    condizione += " AND v.id_proprietario = '" & .Proprietario & "'"
                End If

                If .Modello > 0 Then
                    condizione += " AND v.id_modello = '" & .Modello & "'"
                End If

                Select Case .Leasing
                    Case 1
                        condizione += " AND (v.id_ente_finanziatore IS NULL OR v.id_ente_finanziatore = 0)"
                    Case 2
                        condizione += " AND v.id_ente_finanziatore IS NOT NULL AND v.id_ente_finanziatore > 0"
                End Select

            End If
        End With

        condizioneWhere = condizione
    End Function

    Protected Function VerificaCercaVeicoli() As Boolean
        Dim Messaggio As String = ""
        If TxtAnno.Text.Trim = "" Then
            Messaggio += "Specificare l'anno di elaborazione!" & vbCrLf
        Else
            If Not (Libreria.verificaValoreIntero(TxtAnno.Text) AndAlso (Integer.Parse(TxtAnno.Text) >= 1900)) Then
                Messaggio += "Specificare un anno valido di elaborazione!" & vbCrLf
            End If
        End If

        If Messaggio <> "" Then
            Libreria.genUserMsgBox(Me, Messaggio)
            Return False
        Else
            Return True
        End If
    End Function

    Private Function SelectImportoBollo() As String
        ' per resto regioni 
        ' http://www.aci.it/fileadmin/documenti/per_circolare/guida_al_bollo/tariffari_2012/Basilicata_A4_.pdf
        ' tenendo conto del link: http://www.quattroruote.it/bollo/ in cui la Sicilia appartiene allo stesso gruppo della Basilicata
        ' per Bolzano
        ' http://www.aci.it/fileadmin/documenti/per_circolare/guida_al_bollo/tariffari_2012/Bolzano_A4.pdf
        ' ATTENZIONE METTERE SEMPRE UN NUMERO CON VIRGOLA (quindi 3.0 e non 3 !)
        ' perché sql server pensa che siano interi 
        ' e tronca sotto in caso di disvisione!
        SelectImportoBollo = "CASE v.id_tariffa_bollo" & _
            " WHEN 1 THEN" & _
                " CASE" & _
                " WHEN m.KW > 100 THEN" & _
                    " CASE m.Euro" & _
                        " WHEN 0 THEN 278 + (m.KW - 100) * 4.13" & _
                        " WHEN 1 THEN 269 + (m.KW - 100) * 4.03" & _
                        " WHEN 2 THEN 259 + (m.KW - 100) * 3.90" & _
                        " WHEN 3 THEN 250 + (m.KW - 100) * 3.75" & _
                        " WHEN 4 THEN 239 + (m.KW - 100) * 3.59" & _
                        " WHEN 5 THEN 239 + (m.KW - 100) * 3.59" & _
                        " WHEN 6 THEN 239 + (m.KW - 100) * 3.59" & _
                        " ELSE 278 + (m.KW - 100) * 4.13" & _
                    " END" & _
                " ELSE" & _
                    " CASE m.Euro" & _
                        " WHEN 0 THEN m.KW * 2.78" & _
                        " WHEN 1 THEN m.KW * 2.69" & _
                        " WHEN 2 THEN m.KW * 2.59" & _
                        " WHEN 3 THEN m.KW * 2.50" & _
                        " WHEN 4 THEN m.KW * 2.39" & _
                        " WHEN 5 THEN m.KW * 2.39" & _
                        " WHEN 6 THEN m.KW * 2.39" & _
                        " ELSE m.KW * 2.78" & _
                    " END" & _
                " END" & _
            " ELSE" & _
                " CASE" & _
                " WHEN m.KW > 100 THEN" & _
                    " CASE m.Euro" & _
                        " WHEN 0 THEN 309 + (m.KW - 100) * 4.59" & _
                        " WHEN 1 THEN 299 + (m.KW - 100) * 4.48" & _
                        " WHEN 2 THEN 288 + (m.KW - 100) * 4.33" & _
                        " WHEN 3 THEN 278 + (m.KW - 100) * 4.17" & _
                        " WHEN 4 THEN 266 + (m.KW - 100) * 3.99" & _
                        " WHEN 5 THEN 266 + (m.KW - 100) * 3.99" & _
                        " WHEN 6 THEN 266 + (m.KW - 100) * 3.99" & _
                        " ELSE 309 + (m.KW - 100) * 4.59" & _
                    " END" & _
                " ELSE" & _
                    " CASE m.Euro" & _
                        " WHEN 0 THEN m.KW * 3.09" & _
                        " WHEN 1 THEN m.KW * 2.99" & _
                        " WHEN 2 THEN m.KW * 2.88" & _
                        " WHEN 3 THEN m.KW * 2.78" & _
                        " WHEN 4 THEN m.KW * 2.66" & _
                        " WHEN 5 THEN m.KW * 2.66" & _
                        " WHEN 6 THEN m.KW * 2.66" & _
                        " ELSE m.KW * 3.09" & _
                    " END" & _
                " END" & _
            " END"

    End Function

    Private Sub set_interfaccia_da_filtro(mio_filtro As Filtro_Bolli)
        With mio_filtro
            Trace.Write("set_interfaccia_da_filtro: " & .DataAtto & " - " & .Mese & " - " & .Anno)
            DropDownListMese.SelectedValue = .Mese
            TxtAnno.Text = .Anno
            DropDownProprietario.SelectedValue = .Proprietario
            DropDownListModello.SelectedValue = .Modello
            DropDownListLeasing.SelectedValue = .Leasing
            If .DataAtto Is Nothing Then
                TxtDataAtto.Text = ""
            Else
                TxtDataAtto.Text = .DataAtto
            End If
            TxtTarga.Text = .Targa & ""
        End With
    End Sub

    Private Sub set_Filtro_Bolli(mio_filtro As Filtro_Bolli)
        With mio_filtro
            lb_id_filtro_bollo.Text = .id
            lb_Mese_filtro_bollo.Text = .Mese
            lb_Anno_filtro_bollo.Text = .Anno
            lb_Modello_filtro_bollo.Text = .Proprietario
            lb_Proprietario_filtro_bollo.Text = .Modello
            lb_Leasing_filtro_bollo.Text = .Leasing
            lb_DataAtto_filtro_bollo.Text = .DataAtto & ""
            lb_Targa_filtro_bollo.Text = .Targa & ""
        End With
    End Sub

    Private Function get_Filtro_Bolli() As Filtro_Bolli
        Dim mio_filtro As Filtro_Bolli = New Filtro_Bolli
        With mio_filtro
            .id = Integer.Parse(lb_id_filtro_bollo.Text)
            .Mese = Integer.Parse(lb_Mese_filtro_bollo.Text)
            .Anno = lb_Anno_filtro_bollo.Text
            .Proprietario = lb_Modello_filtro_bollo.Text
            .Modello = lb_Proprietario_filtro_bollo.Text
            .Leasing = lb_Leasing_filtro_bollo.Text
            If lb_DataAtto_filtro_bollo.Text = "" Then
                .DataAtto = Nothing
            Else
                .DataAtto = Libreria.getDateDaStr(lb_DataAtto_filtro_bollo.Text)
            End If
            .Targa = lb_Targa_filtro_bollo.Text
        End With
        Return mio_filtro
    End Function

    Protected Sub btnCercaVeicolo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCercaVeicolo.Click
        Dim sqlStr As String
        Dim sqlWhere As String
        Try

            If Not VerificaCercaVeicoli() Then
                Visibilita(DivVisibile.Ricerca)
                Return
            End If



            Dim mio_filtro As Filtro_Bolli = get_Filtro_Bolli()
            If mio_filtro.id > 0 Then
                mio_filtro.ChiudiFiltro()
                mio_filtro = Nothing
            End If

            mio_filtro = New Filtro_Bolli

            With mio_filtro
                .Anno = Integer.Parse(TxtAnno.Text)
                .Mese = Integer.Parse(DropDownListMese.SelectedValue)
                .Modello = Integer.Parse(DropDownListModello.SelectedValue)
                .Proprietario = Integer.Parse(DropDownProprietario.SelectedValue)
                .Leasing = Integer.Parse(DropDownListLeasing.SelectedValue)
                .DataAtto = Libreria.getDateDaStr(TxtDataAtto.Text)
                .Targa = TxtTarga.Text

                .SalvaRecord(True)

                set_Filtro_Bolli(mio_filtro)

                sqlWhere = condizioneWhere(mio_filtro)

                sqlStr = "INSERT INTO veicoli_bolli (id_filtro_bolli,id_veicolo,selezionato,ritardo,mesi_bollo,scadenza_bollo,importoMese)"

                Dim TrentunoGennaio As Date = New Date(.Anno, 1, 31)
                Dim InizioTreMesiPrima As Date = DateAdd(DateInterval.Month, -3, New Date(.Anno, .Mese, 1))
                Dim InizioMesePrecedente As Date = DateAdd(DateInterval.Month, -1, New Date(.Anno, .Mese, 1))
                Dim VentunoMesePrecedente As Date = DateAdd(DateInterval.Month, -1, New Date(.Anno, .Mese, 21))
                Dim InizioMeseCorrente As Date = New Date(.Anno, .Mese, 1)
                Dim VentunoMeseCorrente As Date = New Date(.Anno, .Mese, 21)

                Dim NumMesi As Integer = 4 - ((.Mese - 1) Mod 4)
                Dim NumMesiPagare As Integer
                Dim DataScadenzaBolloPrecedente As Date
                Trace.Write("NumMesi: " & NumMesi)

                Dim sqlSelect As String = ""
                Select Case mio_filtro.Mese
                    Case Mesi.Gennaio, Mesi.Maggio, Mesi.Settembre
                        NumMesiPagare = NumMesi
                        If NumMesiPagare < 4 Then
                            NumMesiPagare += 4
                        End If
                        DataScadenzaBolloPrecedente = DateAdd(DateInterval.Month, Double.Parse(.Mese - 2), TrentunoGennaio)
                        Trace.Write("NumMesiPagare: " & NumMesiPagare & " - " & DataScadenzaBolloPrecedente)

                        sqlSelect += " SELECT " & mio_filtro.id & ", v.id, 0, 0, " & NumMesiPagare & ", '" & Libreria.FormattaData(DataScadenzaBolloPrecedente) & "', bollo = ((" & SelectImportoBollo() & ") / 12.0)"
                        sqlSelect += " FROM veicoli v WITH(NOLOCK)"
                        sqlSelect += " INNER JOIN modelli m WITH(NOLOCK) ON v.id_modello = m.id_modello"
                        sqlSelect += " WHERE v.data_immatricolazione < CONVERT(DATETIME,'" & Libreria.FormattaDataOreMinSec(InizioTreMesiPrima) & "',102) " & sqlWhere

                    Case Else
                        'Dim DataInizioPeriodoBollo As Date = New Date(.Anno, ((.Mese - 1) \ 4) * 4 + 1, 1)
                        '' NOTA: in DataFinePeriodoBollo utilizzo giorno 1 dato che le operazioni le faccio cmq. sul valore del mese!
                        'Dim DataFinePeriodoBollo As Date = New Date(.Anno, ((.Mese - 1) \ 4 + 1) * 4, 1)

                        '' Aggiungo tutti i veicoli con data scadenza bollo non valorizzato ma immatricolati prima dell'attuale periodo...
                        'NumMesiPagare = 4 ' ritengo che cmq il bollo sia da pagare dall'ultimo periodo bollo...
                        'sqlSelect += " SELECT " & mio_filtro.id & ", v.id, 0, 1, " & NumMesiPagare & ", bollo = ((" & SelectImportoBollo() & ") / 12.0)" & _
                        '    " FROM veicoli v" & _
                        '    " INNER JOIN modelli m ON v.id_modello = m.id_modello" & _
                        '    " WHERE v.data_immatricolazione < '" & Libreria.FormattaDataOreMinSec(DataInizioPeriodoBollo) & "'" & _
                        '    " AND v.data_scadenza_bollo IS NULL" & _
                        '    sqlWhere

                        'If sqlSelect <> "" Then
                        '    sqlSelect += " UNION ALL"
                        'End If

                        '' Aggiungo tutti i veicoli immatricolati nell'attuale periodo ma con data scadenza bollo non valorizzato ...
                        '' il numero di mesi viene calcolata a partire dalla data di immatricolazione!
                        'sqlSelect += " SELECT " & mio_filtro.id & ", v.id, 0, 1," & _
                        '    " CASE" & _
                        '        " WHEN DATEDIFF(month, data_immatricolazione, '" & Libreria.FormattaData(DataFinePeriodoBollo) & "') < 2 THEN DATEDIFF(month, data_immatricolazione, '" & Libreria.FormattaData(DataFinePeriodoBollo) & "') + 5" & _
                        '        " ELSE DATEDIFF(month, data_immatricolazione, '" & Libreria.FormattaData(DataFinePeriodoBollo) & "') + 1" & _
                        '    " END," & _
                        '    " bollo = ((" & SelectImportoBollo() & ") / 12.0)" & _
                        '    " FROM veicoli v" & _
                        '    " INNER JOIN modelli m ON v.id_modello = m.id_modello" & _
                        '    " WHERE v.data_immatricolazione >= '" & Libreria.FormattaDataOreMinSec(DataInizioPeriodoBollo) & "'" & _
                        '    " AND v.data_immatricolazione < '" & Libreria.FormattaDataOreMinSec(VentunoMesePrecedente) & "'" & _
                        '    " AND v.data_scadenza_bollo IS NULL" & _
                        '    sqlWhere

                        'If sqlSelect <> "" Then
                        '    sqlSelect += " UNION ALL"
                        'End If

                        '' Aggiungo tutti i veicoli con data scadenza bollo valorizzato ma gia scaduto nei mesi precedenti
                        'sqlSelect += " SELECT " & mio_filtro.id & ", v.id, 0, 1," & _
                        '    " CASE" & _
                        '        " WHEN DATEDIFF(month, data_scadenza_bollo, '" & Libreria.FormattaData(DataFinePeriodoBollo) & "') < 3 THEN DATEDIFF(month, data_scadenza_bollo, '" & Libreria.FormattaData(DataFinePeriodoBollo) & "') + 4" & _
                        '        " ELSE DATEDIFF(month, data_scadenza_bollo, '" & Libreria.FormattaData(DataFinePeriodoBollo) & "') " & _
                        '    " END," & _
                        '    " bollo = ((" & SelectImportoBollo() & ") / 12.0)" & _
                        '    " FROM veicoli v" & _
                        '    " INNER JOIN modelli m ON v.id_modello = m.id_modello" & _
                        '    " WHERE v.data_immatricolazione < '" & Libreria.FormattaDataOreMinSec(VentunoMesePrecedente) & "'" & _
                        '    " AND v.data_scadenza_bollo < '" & Libreria.FormattaDataOreMinSec(InizioMeseCorrente) & "'" & _
                        '    sqlWhere
                End Select

                If sqlSelect <> "" Then
                    sqlSelect += " UNION ALL"
                End If

                NumMesiPagare = NumMesi + 1
                If NumMesiPagare < 4 Then
                    NumMesiPagare += 4
                End If
                DataScadenzaBolloPrecedente = DateAdd(DateInterval.Month, Double.Parse(.Mese - 3), TrentunoGennaio)
                Trace.Write("NumMesiPagare: " & NumMesiPagare & " - " & DataScadenzaBolloPrecedente)

                sqlSelect += " SELECT " & mio_filtro.id & ", v.id, 0, 0, " & NumMesiPagare & ", '" & Libreria.FormattaData(DataScadenzaBolloPrecedente) & "', bollo = ((" & SelectImportoBollo() & ") / 12.0)"
                sqlSelect += " FROM veicoli v WITH(NOLOCK)"
                sqlSelect += " INNER JOIN modelli m WITH(NOLOCK) ON v.id_modello = m.id_modello"
                sqlSelect += " WHERE v.data_immatricolazione >= CONVERT(DATETIME,'" & Libreria.FormattaDataOreMinSec(VentunoMesePrecedente) & "',102)"
                sqlSelect += " AND v.data_immatricolazione < CONVERT(DATETIME,'" & Libreria.FormattaDataOreMinSec(InizioMeseCorrente) & "',102)"
                sqlSelect += sqlWhere

                sqlSelect += " UNION ALL"

                NumMesiPagare = NumMesi
                If NumMesiPagare < 4 Then
                    NumMesiPagare += 4
                End If
                DataScadenzaBolloPrecedente = DateAdd(DateInterval.Month, Double.Parse(.Mese - 2), TrentunoGennaio)
                Trace.Write("NumMesiPagare: " & NumMesiPagare & " - " & DataScadenzaBolloPrecedente)

                sqlSelect += " SELECT " & mio_filtro.id & ", v.id, 0, 0, " & NumMesiPagare & ", '" & Libreria.FormattaData(DataScadenzaBolloPrecedente) & "', bollo = ((" & SelectImportoBollo() & ") / 12.0)"
                sqlSelect += " FROM veicoli v WITH(NOLOCK)"
                sqlSelect += " INNER JOIN modelli m WITH(NOLOCK) ON v.id_modello = m.id_modello"
                sqlSelect += " WHERE v.data_immatricolazione >= CONVERT(DATETIME,'" & Libreria.FormattaDataOreMinSec(InizioMeseCorrente) & "',102)"
                sqlSelect += " AND v.data_immatricolazione < CONVERT(DATETIME,'" & Libreria.FormattaDataOreMinSec(VentunoMeseCorrente) & "',102)"
                sqlSelect += sqlWhere

                sqlStr += sqlSelect
                Trace.Write(sqlStr)

                Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                        Dbc.Open()
                        Cmd.ExecuteNonQuery()
                    End Using
                End Using

            End With

            InitSelezionaAuto_CalcolaImporto(mio_filtro)

            BindElenco(mio_filtro)

            TotaleBollo(mio_filtro) ' dopo BindElenco!!!

            Visibilita(DivVisibile.Ricerca Or DivVisibile.Elenco Or DivVisibile.Pulsanti)
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_btnCercaVeicolo_click" & "<br/>" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try





    End Sub

    Private Sub InitSelezionaAuto_CalcolaImporto(mio_filtro As Filtro_Bolli)
        Dim sqlStr As String
        Dim InizioMeseCorrente As Date = New Date(mio_filtro.Anno, mio_filtro.Mese, 1)
        Try
            ' se dovessi gestire i bolli retroattivi... dovrei considerare anche 
            ' quelli per il mese passato ancora non venduti....
            ' ma il numero di mesi da pagare in questo caso non sarebbe corretto....
            ' preferisco non implementare questo... perché all'operatore non penso serva questo!
            ' Dim InizioMeseSuccessivo As Date = DateAdd(DateInterval.Month, 1, InizioMeseCorrente)

            sqlStr = "UPDATE veicoli_bolli SET"
            sqlStr += " importo = vb.mesi_bollo * vb.importoMese,"
            sqlStr += " selezionato ="
            sqlStr += " CASE"
            sqlStr += " WHEN (v.data_atto_vendita IS NULL) THEN 1"
            sqlStr += " ELSE 0"
            sqlStr += " END"
            sqlStr += " FROM veicoli v"
            sqlStr += " INNER JOIN veicoli_bolli vb ON v.id = vb.id_veicolo AND vb.id_filtro_bolli = " & mio_filtro.id
            Trace.Write(sqlStr)

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_InitSelezionaAutoCalcolaImporto_" & "<br/>" & sqlStr & "<br/>")
        End Try



    End Sub

    Protected Sub BindElenco(mio_filtro As Filtro_Bolli)
        Dim sqlStr As String = ""

        Try
            sqlStr = "SELECT v.id, v.targa, m.descrizione AS modello,"
            sqlStr += " CONVERT(char(10),v.data_immatricolazione,103) As data_immatricolazione,"
            sqlStr += " vb.scadenza_bollo As data_scadenza_bollo,"
            sqlStr += " CONVERT(Char(10),v.data_atto_vendita,103) As data_atto_vendita,"
            sqlStr += " m.Euro, m.KW,"
            sqlStr += " vb.selezionato,"
            sqlStr += " vb.ritardo,"
            sqlStr += " vb.mesi_bollo,"
            sqlStr += " vb.importo"
            sqlStr += " FROM veicoli v WITH(NOLOCK)"
            sqlStr += " INNER JOIN veicoli_bolli vb WITH(NOLOCK) ON v.id = vb.id_veicolo AND vb.id_filtro_bolli = " & mio_filtro.id
            sqlStr += " INNER JOIN modelli m WITH(NOLOCK) ON v.id_modello = m.id_modello"
            sqlStr += " ORDER BY m.descrizione, v.targa"
            Trace.Write("--------------------------------------------------- " & sqlStr)

            lb_SqlDataSourceElencoBolli.Text = sqlStr

            '' attenzione i clear deve essere fatto prima del bind!
            'Me.IDs.Clear()
            'Me.IdMesi.Clear()

            SqlDataSourceElencoBolli.SelectCommand = sqlStr
            ListBolli.DataBind()
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_bolli_bindelenco_" & "<br/>" & sqlStr & "<br/>")
        End Try




    End Sub

    Protected Function TotaleRecord() As Integer
        Dim DataPager1 As DataPager = ListBolli.FindControl("DataPager1")
        If DataPager1 IsNot Nothing Then
            Return DataPager1.TotalRowCount
        Else
            Return -1
        End If
    End Function

    Protected Sub btnConferma_Click(sender As Object, e As System.EventArgs) Handles btnConferma.Click
        ' carico i dati nella pagina corrente nella lista
        'AddRowstoIDList()
        'AddRowstoIdMesi()

        Try
            Dim mio_filtro As Filtro_Bolli = get_Filtro_Bolli()
            Dim mese As Mesi = mio_filtro.Mese

            UpdateRows_Mesi_Bollo(mio_filtro)

            AggiornaScadenzaBollo(mio_filtro)

            TotaleBollo(mio_filtro)

            ListBolli.DataBind()

            'LISTA BOLLI DA PAGARE
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                Dim url_print As String = "/stampe/bollo_auto.aspx?orientamento=verticale&id_filtro_bollo=" & mio_filtro.id & "&TipoStampa=" & DropDownTipoStampa.SelectedValue & "&header_html=/stampe/header_bollo_auto.aspx?Titolo=" & mio_filtro.Anno & "-" & mese.ToString
                Dim mio_random As String = Format((New Random).Next(), "0000000000")
                Trace.Write(url_print)
                Session("url_print") = url_print
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx?a=" & mio_random & "','')", True)
                End If
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_btnConferma_click_" & "<br/>")
        End Try



    End Sub

    Protected Sub btnChiudi_Click(sender As Object, e As System.EventArgs) Handles btnChiudi.Click
        Dim mio_filtro As Filtro_Bolli = get_Filtro_Bolli()
        mio_filtro.ChiudiFiltro()

        Visibilita(DivVisibile.Ricerca)
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            Dim mio_filtro As Filtro_Bolli = get_Filtro_Bolli()
            UpdateRows_Mesi_Bollo(mio_filtro)

            TotaleBollo(mio_filtro)

            ListBolli.DataBind()
        Catch ex As Exception
            HttpContext.Current.Response.Write("error_btnSalva.Click_" & "<br/>")
        End Try


    End Sub

    Protected Sub btnAzzeraScadenzaBollo_Click(sender As Object, e As System.EventArgs) Handles btnAzzeraScadenzaBollo.Click
        Dim sqlstr As String = ""
        Try
            Dim mio_filtro As Filtro_Bolli = get_Filtro_Bolli()
            UpdateRows_Mesi_Bollo(mio_filtro)

            sqlstr = "UPDATE veicoli SET data_scadenza_bollo = null"
            sqlstr += " FROM veicoli v"
            sqlstr += " INNER JOIN veicoli_bolli vb ON v.id = vb.id_veicolo AND vb.id_filtro_bolli = " & mio_filtro.id

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                End Using
            End Using

            InitSelezionaAuto_CalcolaImporto(mio_filtro)

            ListBolli.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    'Protected Sub DropDownListMese_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListMese.SelectedIndexChanged
    '    If Not Libreria.verificaValoreIntero(TxtAnno.Text) Then
    '        TxtDataAtto.Text = ""
    '        Return
    '    End If
    '    Dim anno As Integer = Integer.Parse(TxtAnno.Text)
    '    If anno < 1900 Then
    '        TxtDataAtto.Text = ""
    '        Return
    '    End If
    '    Dim data_atto As Date = New Date(anno, DropDownListMese.SelectedValue, 1)
    '    TxtDataAtto.Text = data_atto
    'End Sub




End Class
