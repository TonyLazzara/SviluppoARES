Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports funzioni_comuni
Imports System.Collections.Generic

Partial Class gestione_multe_RiepilogoIncassi
    Inherits System.Web.UI.UserControl

    Public Delegate Sub RiepilogoIncassiCloseEventHandler(ByVal sender As Object, ByVal e As EventArgs)
    Event RiepilogoIncassiClose As EventHandler

    Private Sub OnCloseEvent(ByVal e As EventArgs)
        RaiseEvent RiepilogoIncassiClose(Me, e)
    End Sub

    Protected Function GetTotaleIncassi(ByVal DaDataInc As Date, ByVal ADataInc As Date) As Double
        Dim MyReader As SqlDataReader
        Dim MyConnection As SqlConnection = New SqlConnection

        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString
        Dim MyCommand As SqlCommand = New SqlCommand()
        Dim myStringa As String = "SELECT SUM(PER_IMPORTO) as Totale FROM PAGAMENTI_EXTRA WITH(NOLOCK) "
        myStringa = myStringa & "WHERE N_MULTA_RIF >0 AND operazione_stornata = 0 AND id_pos_funzioni_ares = 4 "
        If DropControCassa.SelectedItem.ToString <> "Tutte" Then
            myStringa = myStringa & "AND CASSA=" & CInt(DropControCassa.SelectedValue)
        End If
        myStringa = myStringa & " AND DATA_OPERAZIONE BETWEEN CONVERT(DATETIME, '" & Year(DaDataInc) & "-"
        myStringa = myStringa & Month(DaDataInc) & "-" & Day(DaDataInc) & " 00:00:00', 102) AND "
        myStringa = myStringa & "CONVERT(DATETIME, '" & Year(ADataInc) & "-"
        myStringa = myStringa & Month(ADataInc) & "-" & Day(ADataInc) & " 23:59:59', 102)"

        Trace.Write("------------- query: " & myStringa)
        MyCommand.CommandText = myStringa
        MyCommand.CommandType = CommandType.Text
        MyCommand.Connection = MyConnection
        MyCommand.Connection.Open()
        MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

        MyReader.Read()
        If MyReader.IsDBNull(0) Then
            Return 0
        Else
            Return MyReader("Totale")
        End If

        MyCommand.Dispose()
        MyConnection.Close()
    End Function

    Protected Sub btnCalcolaTotale_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalcolaTotale.Click
        Dim messaggioErr As String = MessControllaDate()
        If messaggioErr <> "" Then
            Libreria.genUserMsgBox(Page, messaggioErr)
            Exit Sub
        End If
        lblTotaleInc.Text = "Euro " & Format(GetTotaleIncassi(CDate(txtDaDataRiepilogo.Text), CDate(txtADataRiepilogo.Text)), "0.00")
    End Sub

    Public Function MessControllaDate() As String
        Dim OkDataInizio As Boolean = False
        Dim OkDataFine As Boolean = False

        If txtDaDataRiepilogo.Text <> "" Then
            If IsDate(txtDaDataRiepilogo.Text) = True Then
                OkDataInizio = True
            End If
        End If

        If txtADataRiepilogo.Text <> "" Then
            If IsDate(txtADataRiepilogo.Text) = True Then
                OkDataFine = True
            End If
        End If

        If OkDataInizio And OkDataFine Then
            If CDate(txtDaDataRiepilogo.Text) > CDate(txtADataRiepilogo.Text) Then
                Return "Incongruenza nelle date scelte"
            Else
                Return ""
            End If
        Else
            Return "Date non imputate correttamente."
        End If
    End Function

    Protected Sub btnStampaReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaReport.Click
        Dim messaggioErr As String = MessControllaDate()
        If messaggioErr <> "" Then
            Libreria.genUserMsgBox(Page, messaggioErr)
            Exit Sub
        End If
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then

            'Dim param As String = Server.UrlEncode("orientamento=verticale&DataIncassi=" & txtDataRiepilogo.Text & "&TipoStampa=0&header_html=/stampe/multe/header_RiepilogoIncMulte.aspx?Titolo=Stampa incassi")
            'Dim url_print As String = "/stampe/multe/RiepilogoIncMulteDelGiorno.aspx?" & param
            Dim Intestazione As String = "Elenco--incassi--POS--dal--" & txtDaDataRiepilogo.Text & "--al--" & txtADataRiepilogo.Text & "--relativi--alla--cassa:--" & DropControCassa.SelectedItem.ToString
            'se si omette il parametro margin_top di default è 15 (così è impostato nella classe ClassGeneraHtmlToPdf.vb)
            Dim url_print As String = "/stampe/multe/RiepilogoIncMulteDelGiorno.aspx?orientamento=verticale&margin_top=30&DaDataIncassi=" & txtDaDataRiepilogo.Text & "&ADataIncassi=" & txtADataRiepilogo.Text & "&cassa=" & DropControCassa.SelectedValue & "&TipoStampa=0&header_html=/stampe/multe/header_RiepilogoIncMulte.aspx?Titolo=" & Intestazione
            'orientamento=verticale&DataIncassi=" & txtDataRiepilogo.Text & "&TipoStampa=0&header_html=/stampe/multe/header_RiepilogoIncMulte.aspx?Titolo=Stampa incassi"
            Dim mio_random As String = Format((New Random).Next(), "0000000000")
            'Trace.Write(url_print)
            Session("url_print") = url_print
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx?a=" & mio_random & "','')", True)
            End If
        End If

    End Sub

    Protected Sub btnChiudi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudi.Click
        OnCloseEvent(New EventArgs())
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblIdStazione.Text = Request.Cookies("SicilyRentCar")("stazione")
        End If
    End Sub
End Class
