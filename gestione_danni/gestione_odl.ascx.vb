Imports System.IO
Imports System.Collections.Generic

Public Class filtro_odl
    Public num_odl As String = ""
    Public id_proprietario As Integer = 0
    Public targa As String = ""
    Public stato_rds As Integer = 0
    Public id_stazione As Integer = 0
    Public ODLDataDa As String = ""
    Public ODLDataA As String = ""
    Public CampiModificabili As Boolean = True
    Public AbilitaPagamento As Boolean = True
    Public AbilitaLente As Boolean = True
    Public EseguiRicerca As Boolean = False
End Class

Partial Class gestione_danni_gestione_odl
    Inherits System.Web.UI.UserControl

    Public Delegate Sub EventoChiusuraForm(ByVal sender As Object, ByVal e As EventArgs)
    Event ChiusuraForm As EventHandler

    Dim cPagamenti As New Pagamenti

    Private Enum DivVisibile
        Nessuno = 0
        Ricerca = 1
        ElenchiRicercati = 2
        EditODL = 4
        GestioneRicerca = Ricerca Or ElenchiRicercati
    End Enum

    Private Sub Visibilita(ByVal Valore As DivVisibile)
        Trace.Write("Visibilita gestione_danni: " & Valore.ToString & " " & Valore)

        div_principale.Visible = Valore And DivVisibile.Ricerca

        div_ricerca.Visible = Valore And DivVisibile.Ricerca

        div_elenco_odl.Visible = Valore And DivVisibile.ElenchiRicercati

        'div_ricerca_targa.Visible = Valore And DivVisibile.Ricerca

        'div_elenco_veicoli.Visible = Valore And DivVisibile.ElenchiRicercati

        div_edit_odl.Visible = Valore And DivVisibile.EditODL

        'div_targa.Visible = Valore And DivVisibile.IntestazioneRDS

        'div_dettaglio_ODL.Visible = Valore And DivVisibile.GestioneODL

        'div_dettaglio_pagamento.Visible = Valore And DivVisibile.FormDettaglioPagamento
    End Sub

    Public Sub InitForm(ByVal mio_record As veicoli_evento_apertura_danno)

    End Sub

    Public Sub InitForm(ByVal filtro As filtro_odl)
        Try
            With filtro
                tx_ODL.Text = .num_odl
                tx_targa.Text = .targa
                DropDownList_stato_ODL.SelectedValue = .stato_rds
                DropDownStazioni.SelectedValue = .id_stazione
                tx_ODLDataDa.Text = .ODLDataDa
                tx_ODLDataA.Text = .ODLDataA

                DropDownStazioni.Enabled = .CampiModificabili

                lb_AbilitaLente.Text = .AbilitaLente

                If .EseguiRicerca Then
                    bt_cerca_ODL_Click(Nothing, Nothing)
                Else
                    Visibilita(DivVisibile.Ricerca)
                End If
            End With
        Catch ex As Exception
            Response.Write("err InitForm : " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Sub gestione_danni_ChiusuraForm(ByVal sender As Object, ByVal e As System.EventArgs)
        Visibilita(DivVisibile.GestioneRicerca)
    End Sub

    Protected Sub gestione_danni_SalvaODL(ByVal sender As Object, ByVal e As System.EventArgs)
        listViewODL.DataBind()
        ' Visibilita(DivVisibile.GestioneRicerca)
    End Sub

    Protected Sub ricerca_veicolo_SelezionaVeicolo(ByVal sender As Object, ByVal e As EventoNuovoRecord)
        Dim id_veicolo As Integer = e.Valore

        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneODL) < 3 Then
            Libreria.genUserMsgBox(Page, "Non hai i diritti per aprire un ODL.")
            Return
        End If

        Dim num_odl As String = odl.VerificaODLApertoSuVeicolo(id_veicolo)
        If num_odl <> "" Then
            Libreria.genUserMsgBox(Page, "E' gia presente un ODL:(" & num_odl & ") aperto su questo veicolo.")
            Return
        End If

        Dim id_lavaggio As String = odl.VerificaAutoALavaggio(id_veicolo)
        If id_lavaggio <> "" Then
            Libreria.genUserMsgBox(Page, "Attenzione: l'auto è attualmente a lavaggio. Impossibile aprire un ODL.")
            Return
        End If

        Dim id_rifornimento As String = odl.VerificaAutoARifornimento(id_veicolo)
        If id_rifornimento <> "" Then
            Libreria.genUserMsgBox(Page, "Attenzione: l'auto è attualmente in rifornimento. Impossibile aprire un ODL.")
            Return
        End If

        Dim id_trasferimento As String = odl.VerificaAutoInTrasferimento(id_veicolo)
        If id_trasferimento <> "" Then
            Libreria.genUserMsgBox(Page, "Attenzione: l'auto è attualmente in trasferimento. Impossibile aprire un ODL.")
            Return
        End If

        lb_id_veicolo.Text = id_veicolo

        edit_odl.InitFormNuovoODL(id_veicolo)

        Visibilita(DivVisibile.EditODL)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'AddHandler gestione_checkin.ChiusuraForm, AddressOf gestione_danni_ChiusuraForm

        AddHandler edit_odl.ChiusuraForm, AddressOf gestione_danni_ChiusuraForm
        AddHandler edit_odl.AggiornaElencoODL, AddressOf gestione_danni_SalvaODL

        AddHandler ricerca_veicolo.SelezionaVeicolo, AddressOf ricerca_veicolo_SelezionaVeicolo

        Dim sqlstr As String = lb_sqlODL.Text
        Try
            sqlODL.SelectCommand = sqlstr

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneRDS) = 1 Then
                lb_AbilitaLente.Text = False
            Else
                lb_AbilitaLente.Text = True
            End If

            If Not Page.IsPostBack Then
                Visibilita(DivVisibile.Ricerca)
            Else

            End If
        Catch ex As Exception
            Response.Write("err Page_Load : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try

    End Sub

    Protected Sub listViewODL_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles listViewODL.DataBound
        Dim th_lente As Control = listViewODL.FindControl("th_lente")
        If th_lente IsNot Nothing Then
            th_lente.Visible = Boolean.Parse(lb_AbilitaLente.Text)
        End If
    End Sub

    Protected Sub listViewODL_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listViewODL.ItemCommand


        If e.CommandName = "lente" Then

            Try
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneRDS) = 1 Then
                    Libreria.genUserMsgBox(Page, "Non hai i diritti per visualizzare l'ODL.")
                    Return
                End If

                Dim lb_num_odl As Label = e.Item.FindControl("lb_num_odl")

                edit_odl.InitFormODL(Integer.Parse(lb_num_odl.Text))

                Visibilita(DivVisibile.EditODL)

            Catch ex As Exception
                Response.Write("err listViewODL_ItemCommand : " & ex.Message & "<br/>" & "<br/>")
            End Try



        End If
    End Sub

    Protected Function getFiltroRicerca() As String
        getFiltroRicerca = "id_rds=" & tx_ODL.Text & "&" &
            "id_proprietario=" & DropDownProprietario.SelectedValue & "&" &
            "targa=" & tx_targa.Text & "&" &
            "stato_ODL=" & DropDownList_stato_ODL.SelectedValue & "&" &
            "id_stazione=" & DropDownStazioni.SelectedValue & "&" &
            "RdsDataDa= CONVERT(DATETIME,'" & funzioni_comuni.GetDataSql(tx_ODLDataDa.Text, 0) & "',102) &" &
            "RdsDataA=CONVERT(DATETIME,'" & funzioni_comuni.GetDataSql(tx_ODLDataA.Text, 59) & "',102)"
    End Function

    Protected Sub bt_stampa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_stampa.Click
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            Dim url_print As String = "/Stampe/stampa_elenco_rds.aspx?orientamento=orizzontale&" & getFiltroRicerca()
            'Trace.Write(url_print)
            Dim mio_random As String = Format((New Random).Next(), "0000000000")
            'Trace.Write(url_print)
            Session("url_print") = url_print
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx?a=" & mio_random & "','')", True)
        End If
    End Sub

    Protected Sub bt_cerca_ODL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_cerca_ODL.Click

        Dim sqlstr As String = getSqlODL(tx_ODL.Text, Integer.Parse(DropDownProprietario.SelectedValue), tx_targa.Text,
                              Integer.Parse(DropDownList_stato_ODL.SelectedValue), Integer.Parse(DropDownStazioni.SelectedValue),
                              tx_ODLDataDa.Text, tx_ODLDataA.Text)

        Try
            lb_sqlODL.Text = sqlstr

            sqlODL.SelectCommand = lb_sqlODL.Text

            'Trace.Write(sqlODL.SelectCommand)

            listViewODL.DataBind()

            Visibilita(DivVisibile.GestioneRicerca)
        Catch ex As Exception
            Response.Write("err bt_cerca_ODL_Click : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try

    End Sub

    Protected Function getClausolaWhereSqlODL(Optional ByVal num_odl As String = "", Optional ByVal id_proprietario As Integer = 0, Optional ByVal targa As String = "", Optional ByVal stato_ODL As Integer = 0, Optional ByVal id_stazione As Integer = 0, Optional ByVal ODLDataDa As String = "", Optional ByVal ODLDataA As String = "") As String
        Dim sqlStr As String = ""

        If num_odl <> "" Then
            sqlStr += " AND odl.num_odl = '" & Libreria.formattaSql(num_odl) & "'"
        Else
            If id_proprietario > 0 Then
                sqlStr += " AND v.id_proprietario = '" & id_proprietario & "'"
            End If

            If targa <> "" Then
                sqlStr += " AND v.targa = '" & Libreria.formattaSql(targa) & "'"
            End If

            If stato_ODL > 0 Then
                sqlStr += " AND odl.id_stato_odl = " & stato_ODL
            End If

            If id_stazione > 0 Then
                sqlStr += " AND v.id_stazione = " & id_stazione
            End If
            Dim dtsql As String
            If ODLDataDa <> "" Then
                Dim DataDaElab As DateTime = New DateTime(Year(ODLDataDa), Month(ODLDataDa), Day(ODLDataDa), 0, 0, 0)
                dtsql = funzioni_comuni.GetDataSql(ODLDataDa, 0)
                sqlStr += " AND odl.data_odl >= CONVERT(DATETIME, '" & dtsql & "', 102)"
            End If

            If ODLDataA <> "" Then
                Dim ODLDataAElab As DateTime = DateAdd(DateInterval.Day, 1, New DateTime(Year(ODLDataA), Month(ODLDataA), Day(ODLDataA), 0, 0, 0))
                dtsql = DateAdd(DateInterval.Day, 1, CDate(ODLDataA))
                sqlStr += " AND odl.data_odl < CONVERT(DATETIME, '" & funzioni_comuni.GetDataSql(dtsql, 59) & "', 102)"
            End If
        End If

        Return sqlStr
    End Function

    Protected Function getSqlODL(Optional ByVal id_rds As String = "", Optional ByVal id_proprietario As Integer = 0, Optional ByVal targa As String = "", Optional ByVal stato_rds As Integer = 0, Optional ByVal id_stazione As Integer = 0, Optional ByVal OdlDataDa As String = "", Optional ByVal OdlDataA As String = "") As String
        Dim sqlStr As String = "SELECT v.targa, s.nome_stazione, os.descrizione des_stato_odl, p.descrizione proprietario," &
            " odl.id id_odl, odl.num_odl, odl.id_veicolo, odl.data_odl, odl.id_stato_odl, odl.preventivo, odl.importo" &
            " FROM odl WITH(NOLOCK)" &
            " INNER JOIN veicoli v WITH(NOLOCK) ON odl.id_veicolo = v.id" &
            " LEFT JOIN stazioni s WITH(NOLOCK) ON v.id_stazione = s.id" &
            " LEFT JOIN proprietari_veicoli p WITH(NOLOCK) ON v.id_proprietario = p.id" &
            " LEFT JOIN odl_stato os WITH(NOLOCK) ON odl.id_stato_odl = os.id" &
            " WHERE odl.attivo = 1"

        sqlStr += getClausolaWhereSqlODL(id_rds, id_proprietario, targa, stato_rds, id_stazione, OdlDataDa, OdlDataA)

        sqlStr += " ORDER BY odl.data_odl DESC"

        Return sqlStr
    End Function

    'Protected Sub bt_cerca_veicoli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_cerca_veicoli.Click
    '    lb_sqlElencoVeicoli.Text = getSqlElencoVeicoli(tx_targa_x_apertura.Text, Integer.Parse(DropDownProprietario_x_apertura.SelectedValue), Integer.Parse(DropDownStazioni_x_apertura.SelectedValue), _
    '                Integer.Parse(DropDownStatodanni.SelectedValue), Integer.Parse(DropDownTipoRecordDanno.SelectedValue), Integer.Parse(DropDownTipologia.SelectedValue))

    '    sqlElencoVeicoli.SelectCommand = lb_sqlElencoVeicoli.Text

    '    Trace.Write(sqlElencoVeicoli.SelectCommand)

    '    listViewElencoVeicoli.DataBind()

    '    Visibilita(DivVisibile.GestioneRicerca)
    'End Sub


    'Protected Function getSqlElencoVeicoli(ByVal targa As String, ByVal id_proprietario As Integer, ByVal id_stazione As Integer, ByVal stato_danni As Integer, ByVal tipo_record_danni As Integer, ByVal tipologia As Integer) As String
    '    Dim sqlStr As String = "SELECT DISTINCT v.id id_veicolo, v.targa, s.nome_stazione, p.descrizione proprietario" & _
    '        " FROM veicoli v WITH(NOLOCK)" & _
    '        " LEFT JOIN stazioni s WITH(NOLOCK) ON v.id_stazione = s.id" & _
    '        " LEFT JOIN proprietari_veicoli p WITH(NOLOCK) ON v.id_proprietario = p.id" & _
    '        " LEFT JOIN veicoli_danni d WITH(NOLOCK) ON v.id = d.id_veicolo AND d.attivo = 1" & _
    '        " LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON d.id_posizione_danno = pd.id" & _
    '        " WHERE 1 = 1"

    '    ' devo inserire la clausola che il veicolo non sia stato venduto!

    '    sqlStr += getClausolaWhereSqlElencoVeicoli(targa, id_proprietario, id_stazione, stato_danni, tipo_record_danni, tipologia)

    '    sqlStr += " ORDER BY v.targa" ' gestire in qualche modo l'ordinamento...

    '    Return sqlStr
    'End Function


    'Protected Function getClausolaWhereSqlElencoVeicoli(ByVal targa As String, ByVal id_proprietario As Integer, ByVal id_stazione As Integer, ByVal stato_danni As Integer, ByVal id_tipo_record_danni As tipo_record_danni, ByVal tipologia As Integer) As String
    '    Dim sqlStr As String = ""

    '    If targa <> "" Then
    '        sqlStr += " AND v.targa = '" & Libreria.formattaSql(targa) & "'"
    '    Else
    '        sqlStr += " AND v.data_atto_vendita IS NULL"

    '        If id_proprietario > 0 Then
    '            sqlStr += " AND v.id_proprietario = '" & id_proprietario & "'"
    '        End If

    '        If id_stazione > 0 Then
    '            sqlStr += " AND v.id_stazione = " & id_stazione
    '        End If

    '        Select Case stato_danni
    '            Case 1 ' danno aperto
    '                sqlStr += " AND d.stato = 1" ' danno aperto
    '            Case 2 ' fermo tecnico
    '                sqlStr += " AND v.da_riparare = 1" ' danno chiuso
    '        End Select

    '        Select Case id_tipo_record_danni
    '            Case tipo_record_danni.Danno_Carrozzeria, tipo_record_danni.Danno_Meccanico, tipo_record_danni.Danno_Elettrico, tipo_record_danni.Dotazione
    '                sqlStr += " AND d.tipo_record = " & id_tipo_record_danni
    '        End Select

    '        Select Case tipologia
    '            Case 1
    '                sqlStr += " AND pd.bloccante = 1"
    '            Case 2
    '                sqlStr += " AND ISNULL(pd.bloccante, 0) = 0"
    '        End Select
    '    End If

    '    Return sqlStr
    'End Function

    'Protected Sub listViewElencoVeicoli_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listViewElencoVeicoli.ItemCommand
    '    If e.CommandName = "lente" Then
    '        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneODL) < 3 Then
    '            Libreria.genUserMsgBox(Page, "Non hai i diritti per aprire un ODL.")
    '            Return
    '        End If

    '        Dim lb_id_veicolo_list As Label = e.Item.FindControl("lb_id_veicolo")

    '        Dim num_odl As String = odl.VerificaODLApertoSuVeicolo(Integer.Parse(lb_id_veicolo_list.Text))
    '        If num_odl <> "" Then
    '            Libreria.genUserMsgBox(Page, "E' gia presente un ODL:(" & num_odl & ") aperto su questo veicolo.")
    '            Return
    '        End If

    '        lb_id_veicolo.Text = lb_id_veicolo_list.Text

    '        edit_odl.InitFormNuovoODL(Integer.Parse(lb_id_veicolo_list.Text))

    '        Visibilita(DivVisibile.EditODL)
    '    End If
    'End Sub
End Class
