Imports System.IO
Imports System.Collections.Generic

Public Class filtro_rds    
    Public id_rds As String = ""
    Public contratto As String = ""
    Public id_proprietario As Integer = 0
    Public targa As String = ""
    Public stato_rds As Integer = 0
    Public id_stazione As Integer = 0
    Public RdsDataDa As String = ""
    Public RdsDataA As String = ""
    Public PagDataDa As String = ""
    Public PagDataA As String = ""
    Public PeriziaDataDa As String = ""
    Public PeriziaDataA As String = ""
    Public id_origine As Integer = 0
    Public CampiModificabili As Boolean = True
    Public AbilitaPagamento As Boolean = True
    Public AbilitaLente As Boolean = True
    Public EseguiRicerca As Boolean = False
End Class

Partial Class gestione_danni_gestione_danni
    Inherits System.Web.UI.UserControl

    Public Delegate Sub EventoChiusuraForm(ByVal sender As Object, ByVal e As EventArgs)
    Event ChiusuraForm As EventHandler

    Dim cPagamenti As New Pagamenti

    Private Enum DivVisibile
        Nessuno = 0
        Ricerca = 1
        ElencoEventi = 2
        IntestazioneRDS = 4
        GestioneRDS = 8
        FormRDS = IntestazioneRDS Or GestioneRDS
        FormDettaglioPagamento = 16
        FormPagamenti = 32
        FormRDSGenerico = 64
        GestionePagamenti = IntestazioneRDS Or FormDettaglioPagamento
    End Enum

    Private Enum OrigineScambioImporto
        Pag_RDS = 1
        Pag_RA
        Pag_RDS_RA
        Pag_NuovaPreautorizzazione
    End Enum

    Private Sub Visibilita(Valore As DivVisibile)
        Trace.Write("Visibilita gestione_danni: " & Valore.ToString & " " & Valore)

        div_ricerca.Visible = Valore And (DivVisibile.Ricerca Or DivVisibile.ElencoEventi)

        div_elenco_eventi.Visible = Valore And DivVisibile.ElencoEventi

        div_targa.Visible = Valore And DivVisibile.IntestazioneRDS

        div_dettaglio_rds.Visible = Valore And DivVisibile.GestioneRDS

        div_dettaglio_pagamento.Visible = Valore And DivVisibile.FormDettaglioPagamento

        div_pagamento.Visible = Valore And DivVisibile.FormPagamenti

        div_rds_generico.Visible = Valore And DivVisibile.FormRDSGenerico
    End Sub

    Public Sub InitForm(mio_record As veicoli_evento_apertura_danno)
 
    End Sub

    Private Function getModelloAutoDaId(id_veicolo As Integer) As String
        Dim sqlStr As String
        Try
            sqlStr = "SELECT m.descrizione AS modello "
            sqlStr += "FROM veicoli v WITH(NOLOCK) "
            sqlStr += "INNER JOIN modelli m WITH(NOLOCK) ON v.id_modello = m.id_modello "
            sqlStr += "WHERE v.id = '" & id_veicolo & "'"
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    getModelloAutoDaId = Cmd.ExecuteScalar & ""
                End Using
            End Using
        Catch ex As Exception
            Response.Write("error_getModelloAutoDaId: " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Private Function getStazioneAutoDaId(ByVal id_veicolo As Integer) As String
        Dim sqlStr As String
        Try
            sqlStr = "SELECT  (s.codice + ' - '+ s.nome_stazione) AS Stazione "
            sqlStr += "FROM veicoli v WITH(NOLOCK) "
            sqlStr += "INNER JOIN stazioni s WITH(NOLOCK) ON v.id_stazione = s.id "
            sqlStr += "WHERE v.id = '" & id_veicolo & "'"

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    getStazioneAutoDaId = Cmd.ExecuteScalar & ""
                End Using
            End Using
        Catch ex As Exception
            Response.Write("error_getStazioneAutoDaId: " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try


    End Function

    Protected Sub InitIntestazione(mio_record As DatiContratto)
        Try
            lb_num_documento.Text = mio_record.num_contratto
            Dim mio_veicolo As tabella_veicoli = tabella_veicoli.get_record_da_id(mio_record.id_veicolo)
            InitIntestazione(mio_veicolo)
        Catch ex As Exception
            Response.Write("error InitIntestazione : " & ex.Message & "<br/>")
        End Try


    End Sub

    Protected Sub InitIntestazione(mio_veicolo As tabella_veicoli)
        With mio_veicolo
            lb_targa.Text = .targa
            lb_modello.Text = .modello
            If lb_modello.Text = "" Then
                lb_modello.Text = "(N.V.)"
            End If
            lb_stazione.Text = .stazione
            If lb_stazione.Text = "" Then
                lb_stazione.Text = "(N.V.)"
            End If
            lb_km.Text = .km_attuali
        End With
    End Sub

    Protected Sub gestione_danni_ChiusuraForm(sender As Object, e As System.EventArgs)
        Visibilita(DivVisibile.ElencoEventi)
    End Sub

    Protected Sub gestione_danni_SalvaRDS(sender As Object, e As System.EventArgs)
        listViewEventiDanni.DataBind()
        ' Visibilita(DivVisibile.ElencoEventi)
    End Sub

    Protected Sub gestione_danni_SalvaRDSGenerico(ByVal sender As Object, ByVal e As System.EventArgs)
        listViewEventiDanni.DataBind()

        Visibilita(DivVisibile.ElencoEventi)
    End Sub

    Protected Sub ScambioImportoClose(sender As Object, e As System.EventArgs)
        Visibilita(DivVisibile.GestionePagamenti)
    End Sub


    Public Sub ScambioImportoTransazioneEseguita(ByVal sender As Object, ByVal e As tabelle_pos_Scambio_Importo.TransazioneEseguitaEventArgs)

        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(lb_id_evento.Text)

        Dim rds As String = ""
        Dim contratto As String = ""

        Dim Origine As OrigineScambioImporto = Integer.Parse(lb_si_origine.Text)
        Dim Importo As Decimal = Decimal.Parse(lb_si_importo.Text)

        Try
            Select Case Origine
                Case OrigineScambioImporto.Pag_NuovaPreautorizzazione
                    rds = mio_evento.id_rds
                Case OrigineScambioImporto.Pag_RDS
                    rds = mio_evento.id_rds
                Case OrigineScambioImporto.Pag_RDS_RA, OrigineScambioImporto.Pag_RA
                    contratto = mio_evento.id_documento_apertura
                Case Else
                    Err.Raise(1001, , "Origine per operazione scambio importo non gestita")
            End Select

            Select Case e.Transazione.IDFunzione
                Case Is = enum_tipo_pagamento_ares.Richiesta
                'Dim tr As classi_pagamento.TransazionePreautorizzazione = e.Transazione
                'cPagamenti.registra_preautorizzazione(tr, "", rds, "", contratto, Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
                Case Is = enum_tipo_pagamento_ares.Vendita
                'Dim tr As classi_pagamento.TransazioneVendita = e.Transazione
                'cPagamenti.registra_vendita(tr, "", rds, "", contratto, Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
                Case Is = enum_tipo_pagamento_ares.Integrazione
                'Dim tr As classi_pagamento.TransazioneIntegrazione = e.Transazione
                'cPagamenti.registra_integrazione(tr, "", rds, "", contratto, Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
                Case Is = enum_tipo_pagamento_ares.Chiusura
                    Dim tr As classi_pagamento.TransazioneChiusura = e.Transazione
                    'cPagamenti.registra_chiusura(tr, "", rds, "", contratto, Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
                    If Origine = OrigineScambioImporto.Pag_RDS Or Origine = OrigineScambioImporto.Pag_RDS_RA Then
                        'If Importo = tr.Importo Then ' se gli importi sono differenti???
                        Dim stato_precedente As sessione_danni.stato_rds = mio_evento.stato_rds
                        mio_evento.stato_rds = sessione_danni.stato_rds.Da_fatturare
                        mio_evento.incasso = tr.Importo
                        mio_evento.AggiornaRecord()
                        Dim mia_variazione_stato As veicoli_stato_rds_variazione = New veicoli_stato_rds_variazione
                        mia_variazione_stato.InitDati(mio_evento, stato_precedente)
                        mia_variazione_stato.SalvaRecord()
                        'End If
                    ElseIf Origine = OrigineScambioImporto.Pag_RA Then

                        ' non posso effettuare una chiusura della preautorizzazione....???
                    End If
                Case Is = enum_tipo_pagamento_ares.Rimborso
                'Dim tr As classi_pagamento.TransazioneRimborso = e.Transazione
                'cPagamenti.registra_rimborso(tr, "", rds, "", contratto, Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
                Case Is = enum_tipo_pagamento_ares.Storno_Ultima_Operazione
                    'Dim tr As classi_pagamento.TransazioneStorno = e.Transazione
                    'cPagamenti.registra_storno(tr, "", rds, "", contratto, Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
            End Select
        Catch ex As Exception
            Response.Write("err ScambioImportoTransazioneEseguita-1: " & ex.Message & "<br/>" & "<br/>")
        End Try

        Try
            DettagliPagamento.AggiornaTabella()
            listViewEventiDanni.DataBind()
            Visibilita(DivVisibile.GestionePagamenti)

        Catch ex As Exception
            Response.Write("err ScambioImportoTransazioneEseguita-2: " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Function getPreautorizzazioneDaIdPagExtra(ByVal id_pagamento_extra As Integer) As String
        Dim sqlStr As String = "SELECT NR_PREAUT FROM PAGAMENTI_EXTRA WITH(NOLOCK) "
        sqlStr += "WHERE ID_CTR = " & id_pagamento_extra
        Try
            Using Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    getPreautorizzazioneDaIdPagExtra = Cmd.ExecuteScalar() & ""
                End Using
            End Using
        Catch ex As Exception
            Response.Write("err getPreautorizzazioneDaIdPagExtra : " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Protected Sub Pagamento_RDS(ByVal sender As Object, ByVal e As EventoNuovoRecord)

        Try
            Dim id_pagamento_extra As Integer = e.Valore
            Dim preautorizzazione As String = getPreautorizzazioneDaIdPagExtra(id_pagamento_extra)


            Trace.Write("Pagamento_RDS - preautorizzazione: " & preautorizzazione)

            Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(Integer.Parse(lb_id_evento.Text))

            InizializzaScambioImportoPagamento_RDS(mio_evento, preautorizzazione, DropDownSimulazione.SelectedValue)
        Catch ex As Exception
            Response.Write("err Pagamento_RDS : " & ex.Message & "<br/>" & "<br/>")
        End Try



    End Sub

    Protected Sub Pagamento_RA(ByVal sender As Object, ByVal e As EventoNuovoRecord)
        Try
            Dim id_pagamento_extra As Integer = e.Valore
            Dim preautorizzazione As String = getPreautorizzazioneDaIdPagExtra(id_pagamento_extra)

            Trace.Write("Pagamento_RA - preautorizzazione: " & preautorizzazione)

            Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(Integer.Parse(lb_id_evento.Text))

            InizializzaScambioImportoPagamento_RA(mio_evento, preautorizzazione, DropDownSimulazione.SelectedValue)
        Catch ex As Exception
            Response.Write("err Pagamento_RA : " & ex.Message & "<br/>" & "<br/>")
        End Try


    End Sub

    Protected Sub Pagamento_RDS_RA(ByVal sender As Object, ByVal e As EventoNuovoRecord)
        Try
            Dim id_pagamento_extra As Integer = e.Valore
            Dim preautorizzazione As String = getPreautorizzazioneDaIdPagExtra(id_pagamento_extra)

            Trace.Write("Pagamento_RDS_RA - preautorizzazione: " & preautorizzazione)

            Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(Integer.Parse(lb_id_evento.Text))

            InizializzaScambioImportoPagamento_RDS_RA(mio_evento, preautorizzazione, DropDownSimulazione.SelectedValue)
        Catch ex As Exception
            Response.Write("err Pagamento_RDS_RA : " & ex.Message & "<br/>" & "<br/>")
        End Try


    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        Try
            If Not Page.IsPostBack Then
                Dim tipo_documento As String = Session("tipo_documento") & ""
                Session("tipo_documento") = Nothing

                Dim numero_rds As String = Session("numero_rds") & ""
                Session("numero_rds") = Nothing

                Dim numero_documento As String = Session("numero_documento") & ""
                Session("numero_documento") = Nothing

                Dim numero_crv As String = Session("numero_crv") & ""
                Session("numero_crv") = Nothing

                Trace.Write("************" & tipo_documento & " " & numero_rds & " " & numero_documento & " " & numero_crv)
                If tipo_documento <> "" Then
                    Dim mio_evento As veicoli_evento_apertura_danno = Nothing
                    If numero_rds <> "" Then
                        mio_evento = veicoli_evento_apertura_danno.getRecordDaRDS(Integer.Parse(tipo_documento), Integer.Parse(numero_rds))
                    ElseIf numero_documento <> "" Then
                        If numero_crv = "" Then
                            numero_crv = 0
                        End If
                        mio_evento = veicoli_evento_apertura_danno.getRecordDaDocumento(Integer.Parse(tipo_documento), Integer.Parse(numero_documento), Integer.Parse(numero_crv))
                    End If

                    If mio_evento IsNot Nothing Then
                        With mio_evento
                            VisualizzaRDS(.id_tipo_documento_apertura, .id_documento_apertura, .num_crv)
                        End With
                    End If
                Else
                    Visibilita(DivVisibile.Ricerca)
                End If
            End If
        Catch ex As Exception
            Response.Write("err Page_PreRender : " & ex.Message & "<br/>" & "<br/>")
        End Try




    End Sub

    Protected Sub selezionaVeicolo(ByVal id_veicolo As Integer)
        Try
            gestione_checkin_rds_generico.InitFormNuovoRDSGenerico(id_veicolo)

            Visibilita(DivVisibile.FormRDSGenerico)
        Catch ex As Exception
            Response.Write("err selezionaVeicolo : " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Sub statoUsoVeicolo(ByVal id_veicolo As Integer)
        Try
            gestione_checkin_rds_generico.InitFormStatoUsoVeicolo(id_veicolo)

            Visibilita(DivVisibile.FormRDSGenerico)
        Catch ex As Exception
            Response.Write("err statoUsoVeicolo : " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub


    Protected Sub ricerca_veicolo_SelezionaVeicolo(ByVal sender As Object, ByVal e As EventoNuovoRecord)
        Try
            selezionaVeicolo(e.Valore)
        Catch ex As Exception
            Response.Write("err ricerca_veicolo_SelezionaVeicolo : " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler gestione_checkin_rds_generico.ChiusuraForm, AddressOf gestione_danni_ChiusuraForm
        AddHandler gestione_checkin_rds_generico.SalvaCheckIn, AddressOf gestione_danni_SalvaRDSGenerico

        AddHandler gestione_checkin.ChiusuraForm, AddressOf gestione_danni_ChiusuraForm
        AddHandler gestione_checkin.SalvaRDS, AddressOf gestione_danni_SalvaRDS

        AddHandler Scambio_Importo1.ScambioImportoClose, AddressOf ScambioImportoClose
        AddHandler Scambio_Importo1.ScambioImportoTransazioneEseguita, AddressOf ScambioImportoTransazioneEseguita
        AddHandler Scambio_Importo1.CassaPagamentoEseguito, AddressOf ScambioImportoTransazioneEseguita


        AddHandler DettagliPagamento.Pagamento_RDS, AddressOf Pagamento_RDS
        AddHandler DettagliPagamento.Pagamento_RA, AddressOf Pagamento_RA
        AddHandler DettagliPagamento.Pagamento_RDS_RA, AddressOf Pagamento_RDS_RA

        AddHandler ricerca_veicolo.SelezionaVeicolo, AddressOf ricerca_veicolo_SelezionaVeicolo


        sqlEventiDannoTarga.SelectCommand = lb_sqlEventiDannoTarga.Text

        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneRDS) = 1 Then
            lb_AbilitaLente.Text = False
            lb_AbilitaPagamento.Text = False
        Else
            lb_AbilitaLente.Text = True
            lb_AbilitaPagamento.Text = True
        End If


        If Not Page.IsPostBack Then
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneRDS) <> 3 Then
                'DropDownStazioni.Enabled = False
                DropDownStazioni.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
            Else
                DropDownStazioni.Enabled = True
                DropDownStazioni.SelectedValue = 0
            End If
        End If

    End Sub

    Private Function getIdAutoDaTarga(targa As String) As String
        Dim sqlStr As String
        Try
            sqlStr = "SELECT id FROM veicoli WITH(NOLOCK) where targa = '" & Libreria.formattaSqlTrim(targa) & "'"
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    getIdAutoDaTarga = Cmd.ExecuteScalar & ""
                End Using
            End Using

        Catch ex As Exception
            Response.Write("err getIdAutoDaTarga : " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try


    End Function

    Protected Sub bt_cerca_rds_Click(sender As Object, e As System.EventArgs) Handles bt_cerca_rds.Click
        Dim sqlstr As String
        Try

            sqlstr = getSqlEventiDannoTarga(tx_RDS.Text, tx_contratto.Text, Integer.Parse(DropDownProprietario.SelectedValue), tx_targa.Text, Integer.Parse(DropDownList_stato_rds.SelectedValue), Integer.Parse(DropDownStazioni.SelectedValue), tx_RdsDataDa.Text, tx_RdsDataA.Text, tx_PagDataDa.Text, tx_PagDataA.Text, tx_PeriziaDataDa.Text, tx_PeriziaDataA.Text, DropDownOrigine.SelectedValue, tx_ScadenzaPreautDaData.Text, tx_ScadenzaPreautAData.Text)


            'Response.Write("err CalcolaTotaliSQL :<br/>" & sqlstr & "<br/>")
            ' Response.End()



            lb_sqlEventiDannoTarga.Text = sqlstr

            sqlEventiDannoTarga.SelectCommand = lb_sqlEventiDannoTarga.Text

            'Response.Write(lb_sqlEventiDannoTarga.Text)

            CalcolaTotali()

            listViewEventiDanni.DataBind()

            Visibilita(DivVisibile.ElencoEventi)

        Catch ex As Exception
            Response.Write("err bt_cerca_rds_Click : " & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try


    End Sub

    Private Sub CalcolaTotali()

        Dim sqlStr As String = getSqlNumeroRecord(tx_RDS.Text,
                              tx_contratto.Text,
                              Integer.Parse(DropDownProprietario.SelectedValue),
                              tx_targa.Text,
                              Integer.Parse(DropDownList_stato_rds.SelectedValue),
                              Integer.Parse(DropDownStazioni.SelectedValue),
                              tx_RdsDataDa.Text,
                              tx_RdsDataA.Text,
                              tx_PagDataDa.Text,
                              tx_PagDataA.Text,
                              tx_PeriziaDataDa.Text,
                              tx_PeriziaDataA.Text,
                              DropDownOrigine.SelectedValue)

        'Response.Write("err CalcolaTotaliSQL :<br/>" & sqlStr & "<br/>")
        'Response.End()

        Try


            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Using Rs = Cmd.ExecuteReader
                        If Rs.Read Then
                            ''Tony 18-05-2023
                            'Dim TotaleNumRecAux As String = ""
                            'Dim TotaleAux As String = ""
                            'Dim SpesePostaliAux As String = ""
                            'Dim TotaleIncassatoAux As String = ""

                            ''Response.Write("Tot Aux " & TotaleAux)
                            ''FINE Tony

                            'TotaleNumRecAux = IIf(Rs("NumRecord") Is DBNull.Value, 0, Rs("NumRecord"))
                            'TotaleNumRecAux = TotaleAux
                            'TotaleNumRecAux = FormatNumber(TotaleAux, 2, , , TriState.True)
                            ''IIf(Rs("NumRecord") Is DBNull.Value, 0, Rs("NumRecord"))                            
                            'lb_NumRecord.Text = TotaleNumRecAux

                            'TotaleAux = IIf(Rs("TotStimato") Is DBNull.Value, "0,00", Libreria.myFormatta(Rs("TotStimato"), "0.00"))
                            'TotaleAux = TotaleAux * 1.22
                            'TotaleAux = FormatNumber(TotaleAux, 2, , , TriState.True)
                            ''lb_TotStimato.Text = IIf(Rs("TotStimato") Is DBNull.Value, "0.00", Libreria.myFormatta(Rs("TotStimato"), "0.00"))
                            'lb_TotStimato.Text = TotaleAux

                            'TotaleIncassatoAux = IIf(Rs("TotIncassato") Is DBNull.Value, "0,00", Libreria.myFormatta(Rs("TotIncassato"), "0.00"))
                            ''Response.Write("Inc " & TotaleIncassatoAux)
                            'TotaleIncassatoAux = FormatNumber(TotaleIncassatoAux, 2, , , TriState.True)
                            'lb_TotIncassato.Text = TotaleIncassatoAux

                            lb_NumRecord.Text = IIf(Rs("NumRecord") Is DBNull.Value, 0, Rs("NumRecord"))
                            lb_NumRecord.Text = FormatNumber(lb_NumRecord.Text, 0, , , TriState.True)

                            lb_TotStimato.Text = IIf(Rs("TotStimato") Is DBNull.Value, "0.00", Libreria.myFormatta(Rs("TotStimato"), "0.00"))
                            lb_TotStimato.Text = lb_TotStimato.Text
                            lb_TotStimato.Text = FormatNumber(lb_TotStimato.Text, 2, , , TriState.True)

                            lb_TotIncassato.Text = IIf(Rs("TotIncassato") Is DBNull.Value, "0.00", Libreria.myFormatta(Rs("TotIncassato"), "0.00"))
                            lb_TotIncassato.Text = FormatNumber(lb_TotIncassato.Text, 2, , , TriState.True)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Response.Write("err CalcolaTotali : " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Sub

    Protected Function getClausolaWhereSqlEventiDanno(Optional ByVal id_rds As String = "", Optional ByVal contratto As String = "", Optional ByVal id_proprietario As Integer = 0, Optional ByVal targa As String = "", Optional ByVal stato_rds As Integer = 0, Optional ByVal id_stazione As Integer = 0, Optional ByVal RdsDataDa As String = "", Optional ByVal RdsDataA As String = "", Optional ByVal PagDataDa As String = "", Optional ByVal PagDataA As String = "", Optional ByVal PeriziaDataDa As String = "", Optional ByVal PeriziaDataA As String = "", Optional ByVal id_origine As Integer = 0, Optional ByVal PreaudDataDa As String = "", Optional ByVal PreautDataA As String = "") As String
        Dim sqlStr As String = ""

        'MAX(px.scadenza_preaut) scadenza_preaut

        If id_rds <> "" Then
            sqlStr += " AND ed.id_rds = '" & Libreria.formattaSql(id_rds) & "'"
        Else
            If contratto <> "" Then
                sqlStr += " AND ed.id_tipo_documento_apertura = " & tipo_documento.Contratto & " AND ed.id_documento_apertura = '" & Libreria.formattaSql(contratto) & "'"
            End If

            If id_proprietario > 0 Then
                sqlStr += " AND v.id_proprietario = '" & id_proprietario & "'"
            End If

            If targa <> "" Then
                sqlStr += " AND v.targa = '" & Libreria.formattaSql(targa) & "'"
            End If

            'Tony 17/04/2023
            If stato_rds > 0 And stato_rds <> 4 Then
                sqlStr += " AND ed.stato_rds = " & stato_rds
            Else
                If stato_rds <> 0 Then 'Statords sarà 4
                    sqlStr += " AND ed.da_riparare = 1"                
                End If

            End If
            'FINE Tony

            If id_stazione > 0 Then
                sqlStr += " AND v.id_stazione = " & id_stazione
            End If

            If id_origine > 0 Then
                sqlStr += " AND ed.id_tipo_documento_apertura = " & id_origine
            End If

            Dim dtsql As String = ""
            If RdsDataDa <> "" Then
                Dim DataDaElab As DateTime = New DateTime(Year(RdsDataDa), Month(RdsDataDa), Day(RdsDataDa), 0, 0, 0)
                dtsql = funzioni_comuni.GetDataSql(RdsDataDa, 0)
                'sqlStr += " AND ed.data_rds >= CONVERT(DATETIME,'" & Libreria.FormattaDataOreMinSec(DataDaElab) & "',102)"
                sqlStr += " AND ed.data_rds >= CONVERT(DATETIME,'" & dtsql & "',102)"
            End If

            If RdsDataA <> "" Then
                'Dim RdsDataAElab As DateTime = DateAdd(DateInterval.Day, 1, New DateTime(Year(RdsDataA), Month(RdsDataA), Day(RdsDataA), 0, 0, 0))
                'RdsDataAElab = DateAdd(DateInterval.Day, 1, CDate(RdsDataA))
                dtsql = funzioni_comuni.GetDataSql(RdsDataA, 59)
                sqlStr += " AND ed.data_rds < CONVERT(DATETIME,'" & dtsql & "',102)"
                'sqlStr += " AND ed.data_rds < '" & Libreria.FormattaDataOreMinSec(RdsDataAElab) & "'"
            End If

            If PagDataDa <> "" Then
                'Dim PagDataDaElab As DateTime = New DateTime(Year(PagDataDa), Month(PagDataDa), Day(PagDataDa), 0, 0, 0)
                dtsql = funzioni_comuni.GetDataSql(PagDataDa, 0)
                sqlStr += " AND ed.data_pagamento >= CONVERT(DATETIME,'" & dtsql & "',102)"
                'sqlStr += " AND ed.data_pagamento >= '" & Libreria.FormattaDataOreMinSec(PagDataDaElab) & "'"
            End If

            If PagDataA <> "" Then
                'Dim PagDataAElab As DateTime = DateAdd(DateInterval.Day, 1, New DateTime(Year(PagDataA), Month(PagDataA), Day(PagDataA), 0, 0, 0))
                'PagDataAElab = DateAdd(DateInterval.Day, 1, CDate(PagDataA))
                dtsql = funzioni_comuni.GetDataSql(PagDataA, 59)
                'sqlStr += " AND ed.data_pagamento < '" & Libreria.FormattaDataOreMinSec(PagDataAElab) & "'"
                sqlStr += " AND ed.data_pagamento < CONVERT(DATETIME,'" & dtsql & "',102)"
            End If

            If PreaudDataDa <> "" Then
                'Dim PagDataDaElab As DateTime = New DateTime(Year(PreaudDataDa), Month(PreaudDataDa), Day(PreaudDataDa), 0, 0, 0)
                dtsql = funzioni_comuni.GetDataSql(PreaudDataDa, 0)
                'sqlStr += " AND px.scadenza_preaut >= '" & Libreria.FormattaDataOreMinSec(PagDataDaElab) & "'"
                sqlStr += " AND px.scadenza_preaut >= CONVERT(DATETIME,'" & dtsql & "',102)"
            End If

            If PreautDataA <> "" Then
                'Dim PagDataAElab As DateTime = DateAdd(DateInterval.Day, 1, New DateTime(Year(PreautDataA), Month(PreautDataA), Day(PreautDataA), 0, 0, 0))
                'PagDataAElab = DateAdd(DateInterval.Day, 1, CDate(PreautDataA))
                dtsql = funzioni_comuni.GetDataSql(PreautDataA, 59)
                'sqlStr += " AND px.scadenza_preaut < '" & Libreria.FormattaDataOreMinSec(PagDataAElab) & "'"
                sqlStr += " AND px.scadenza_preaut < CONVERT(DATETIME,'" & dtsql & "',102)"
            End If

            If PeriziaDataDa <> "" Then
                'Dim PeriziaDataDaElab As DateTime = New DateTime(Year(PeriziaDataDa), Month(PeriziaDataDa), Day(PeriziaDataDa), 0, 0, 0)
                dtsql = funzioni_comuni.GetDataSql(PeriziaDataDa, 0)
                ' sqlStr += " AND ed.data_perizia >= '" & Libreria.FormattaDataOreMinSec(PeriziaDataDaElab) & "'"
                sqlStr += " AND ed.data_perizia >= CONVERT(DATETIME,'" & dtsql & "',102)"
            End If

            If PeriziaDataA <> "" Then
                'Dim PeriziaDataAElab As DateTime = DateAdd(DateInterval.Day, 1, New DateTime(Year(PeriziaDataA), Month(PeriziaDataA), Day(PeriziaDataA), 0, 0, 0))
                dtsql = funzioni_comuni.GetDataSql(PeriziaDataA, 59)
                'sqlStr += " AND ed.data_perizia < '" & Libreria.FormattaDataOreMinSec(PeriziaDataAElab) & "'"
                sqlStr += " AND ed.data_perizia < CONVERT(DATETIME,'" & dtsql & "',102)"
            End If

            If PeriziaDataDa <> "" And PeriziaDataA <> "" Then
                sqlStr += " AND ed.perizia = 1"
            End If
        End If
        'Response.Write("sql getClausolaWhereSqlEventiDanno " & sqlStr & "<br/>")
        Return sqlStr
    End Function

    Protected Function getSqlEventiDannoTarga(ByVal id_rds As String, ByVal contratto As String, ByVal id_proprietario As Integer, ByVal targa As String, ByVal stato_rds As Integer, ByVal id_stazione As Integer, ByVal RdsDataDa As String, ByVal RdsDataA As String, ByVal PagDataDa As String, ByVal PagDataA As String, ByVal PeriziaDataDa As String, ByVal PeriziaDataA As String, ByVal id_origine As Integer, ByVal PreaudDataDa As String, ByVal PreautDataA As String) As String
        Dim sqlStr As String = "SELECT tdd.codice_sintetico des_id_tipo_documento_apertura, v.targa, s.nome_stazione, sr.descrizione des_stato_rds, p.descrizione proprietario,"
        sqlStr += " ed.id id_evento, ed.id_veicolo, ed.id_tipo_documento_apertura, ed.id_documento_apertura, ed.num_crv, ed.data_rds, ed.id_rds, ed.stato_rds, ed.importo, ed.incasso, ed.totale, MAX(px.scadenza_preaut) scadenza_preaut"
        sqlStr += " FROM veicoli_evento_apertura_danno ed WITH(NOLOCK)"
        sqlStr += " LEFT JOIN veicoli v WITH(NOLOCK) ON ed.id_veicolo = v.id"
        sqlStr += " LEFT JOIN stazioni s WITH(NOLOCK) ON v.id_stazione = s.id"
        sqlStr += " LEFT JOIN proprietari_veicoli p WITH(NOLOCK) ON v.id_proprietario = p.id"
        sqlStr += " LEFT JOIN veicoli_stato_rds sr WITH(NOLOCK) ON ed.stato_rds = sr.id"
        sqlStr += " LEFT JOIN veicoli_tipo_documento_apertura_danno tdd WITH(NOLOCK) ON ed.id_tipo_documento_apertura = tdd.id"
        sqlStr += " LEFT JOIN PAGAMENTI_EXTRA px WITH(NOLOCK)"
        sqlStr += " ON (ed.id_tipo_documento_apertura = 1 AND ed.id_documento_apertura = px.N_CONTRATTO_RIF AND px.id_pos_funzioni_ares = '" & enum_tipo_pagamento_ares.Richiesta & "' AND px.preaut_aperta = '1' AND px.operazione_stornata = '0')"
        sqlStr += " OR (ed.id_rds = px.N_RDS_RIF AND px.id_pos_funzioni_ares = '" & enum_tipo_pagamento_ares.Richiesta & "' AND px.preaut_aperta = '1' AND px.operazione_stornata = '0')"
        sqlStr += " WHERE ed.attivo = 1"
        sqlStr += " AND ed.sospeso_rds = 0"

        ' AND ed.id_rds = '11'
        ' AND v.targa = 'AA123BB'
        ' AND ed.stato_rds = 1
        ' AND v.id_stazione = 11
        ' AND ed.data_creazione >= '2012-09-01 0:0:0'
        ' AND ed.data_creazione < '2012-09-02 0:0:0'
        ' AND ed.data_pagamento >= '2012-09-01 0:0:0'
        ' AND ed.data_pagamento < '2012-09-02 0:0:0'

        sqlStr += getClausolaWhereSqlEventiDanno(id_rds, contratto, id_proprietario, targa, stato_rds, id_stazione, RdsDataDa, RdsDataA, PagDataDa, PagDataA, PeriziaDataDa, PeriziaDataA, id_origine, PreaudDataDa, PreautDataA)

        sqlStr += " GROUP BY tdd.codice_sintetico, v.targa, s.nome_stazione, sr.descrizione, p.descrizione," &
            " ed.id, ed.id_veicolo, ed.id_tipo_documento_apertura, ed.id_documento_apertura, ed.num_crv, ed.data_rds, ed.id_rds, ed.stato_rds, ed.importo, ed.incasso, ed.totale"

        sqlStr += " ORDER BY ed.data_rds DESC, ed.id_documento_apertura DESC, ed.id_rds DESC"

        'Trace.Write("******************************")
        'Trace.Write(sqlStr)


        'Tony 17/04/2023
        If DropDownList_stato_rds.SelectedValue = 4 Then 'SElezionato Da riparare
            'Response.Write("Da Riparare :<br/>" & sqlStr & "<br/>")
        Else
            'Response.Write("ALTRO :<br/>" & sqlStr & "<br/>")
        End If
        'FINE Tony
        'Response.End()


        'Response.Write(sqlStr)
        Return sqlStr
    End Function

    Protected Function getSqlNumeroRecord(ByVal id_rds As String, ByVal contratto As String, ByVal id_proprietario As Integer, ByVal targa As String, ByVal stato_rds As Integer, ByVal id_stazione As Integer, ByVal RdsDataDa As String, ByVal RdsDataA As String, ByVal PagDataDa As String, ByVal PagDataA As String, ByVal PeriziaDataDa As String, ByVal PeriziaDataA As String, ByVal id_origine As Integer) As String
        'Tony 20-05-2023
        'Dim sqlStr As String = "SELECT COUNT(*) NumRecord, SUM(ed.importo) TotStimato, SUM(ed.incasso) TotIncassato"
        'sqlStr += " FROM veicoli_evento_apertura_danno ed WITH(NOLOCK)"
        'sqlStr += " INNER JOIN veicoli v WITH(NOLOCK) ON ed.id_veicolo = v.id"
        'sqlStr += " INNER JOIN stazioni s WITH(NOLOCK) ON v.id_stazione = s.id"
        'sqlStr += " INNER JOIN proprietari_veicoli p WITH(NOLOCK) ON v.id_proprietario = p.id"
        'sqlStr += " WHERE ed.attivo = 1"
        'sqlStr += " AND ed.sospeso_rds = 0"

        Dim sqlStr As String = "SELECT COUNT(*) NumRecord, SUM(ed.totale) TotStimato, SUM(ed.incasso) TotIncassato"
        sqlStr += " FROM veicoli_evento_apertura_danno ed WITH(NOLOCK)"
        sqlStr += " INNER JOIN veicoli v WITH(NOLOCK) ON ed.id_veicolo = v.id"
        sqlStr += " INNER JOIN stazioni s WITH(NOLOCK) ON v.id_stazione = s.id"
        sqlStr += " INNER JOIN proprietari_veicoli p WITH(NOLOCK) ON v.id_proprietario = p.id"
        sqlStr += " WHERE ed.attivo = 1"
        sqlStr += " AND ed.sospeso_rds = 0"
        'FINE Tony

        sqlStr += getClausolaWhereSqlEventiDanno(id_rds, contratto, id_proprietario, targa, stato_rds, id_stazione, RdsDataDa, RdsDataA, PagDataDa, PagDataA, PeriziaDataDa, PeriziaDataA, id_origine)

        Return sqlStr
    End Function

    Protected Sub listViewEventiDanni_DataBound(sender As Object, e As System.EventArgs) Handles listViewEventiDanni.DataBound
        Dim th_lente As Control = listViewEventiDanni.FindControl("th_lente")
        If th_lente IsNot Nothing Then
            th_lente.Visible = Boolean.Parse(lb_AbilitaLente.Text)
        End If
        Dim th_pagamento As Control = listViewEventiDanni.FindControl("th_pagamento")
        If th_pagamento IsNot Nothing Then
            th_pagamento.Visible = Boolean.Parse(lb_AbilitaPagamento.Text)
        End If
    End Sub


    Protected Sub listViewEventiDanni_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listViewEventiDanni.ItemDataBound
        Dim lb_stato_rds As Label = e.Item.FindControl("lb_stato_rds")
        Dim pagamento As ImageButton = e.Item.FindControl("pagamento")
        Dim lb_id_tipo_documento_apertura As Label = e.Item.FindControl("lb_id_tipo_documento_apertura")
        Dim TestoColonnaStimatoIncassato As ImageButton = e.Item.FindControl("lblColonnaStimatoIncassato")

        'Tony 11-05-2023
        'Dim lblImporto As Label = e.Item.FindControl("lb_importo")
        'Dim lblImportoIncasso As Label = e.Item.FindControl("lb_importoIncasso")
        'FINE Tony


        If Integer.Parse(lb_id_tipo_documento_apertura.Text) = tipo_documento.Contratto Then
            Dim stato_rds_riga As sessione_danni.stato_rds = Integer.Parse(lb_stato_rds.Text)
            If stato_rds_riga = sessione_danni.stato_rds.Da_addebitare Or stato_rds_riga = sessione_danni.stato_rds.Da_fatturare Or stato_rds_riga = sessione_danni.stato_rds.Fatturato Then
                'Tony 20-04-2023
                'pagamento.Visible = True
                pagamento.Visible = False
                'FINE Tony
            Else
                pagamento.Visible = False
            End If

            'If DropDownList_stato_rds.SelectedValue = 6 Then
            '    lblImporto.Visible = False
            'Else
            '    lblImportoIncasso.Visible = False
            'End If
            'If stato_rds_riga = sessione_danni.stato_rds.Da_fatturare Then
            '    lblImporto.Visible = False
            'Else
            '    lblImportoIncasso.Visible = False
            'End If
        Else
            pagamento.Visible = False
        End If

        

    End Sub

    Public Sub VisualizzaRDS(ByVal tipo_documento As tipo_documento, ByVal numero_documento As Integer, Optional ByVal numero_crv As Integer = 0)        

        Dim mio_record As DatiContratto = Nothing

        lb_id_tipo_documento.Text = tipo_documento
        lb_id_documento.Text = numero_documento
        lb_num_crv.Text = numero_crv

        Select Case tipo_documento
            Case tipo_documento.Contratto
                mio_record = DatiContratto.getRecordDaNumContratto(numero_documento, numero_crv)
                If mio_record Is Nothing Then
                    Libreria.genUserMsgBox(Page, "Contratto non trovato.")
                    Return
                End If
            Case tipo_documento.MovimentoInterno
                mio_record = DatiContratto.getRecordDaNumTrasferimento(numero_documento)
                If mio_record Is Nothing Then
                    Libreria.genUserMsgBox(Page, "Movimento Interno non trovato.")
                    Return
                End If
            Case tipo_documento.Lavaggio
                mio_record = DatiContratto.getRecordDaLavaggio(numero_documento)
                If mio_record Is Nothing Then
                    Libreria.genUserMsgBox(Page, "Lavaggio non trovato.")
                    Return
                End If
            Case tipo_documento.ODL
                mio_record = DatiContratto.getRecordDaNumODL(numero_documento)
                If mio_record Is Nothing Then
                    Libreria.genUserMsgBox(Page, "ODL non trovato.")
                    Return
                End If
            Case tipo_documento.DuranteODL
                mio_record = DatiContratto.getRecordDaNumDuranteODL(numero_documento)
                If mio_record Is Nothing Then
                    Libreria.genUserMsgBox(Page, "ODL non trovato.")
                    Return
                End If
            Case Is >= Global.tipo_documento.RDSGenerico
                mio_record = DatiContratto.getRecordDaRDSGenerico(tipo_documento, numero_documento)
                If mio_record Is Nothing Then
                    Libreria.genUserMsgBox(Page, "RDS Generico non trovato.")
                    Return
                End If
            Case Else
                Libreria.genUserMsgBox(Page, "Tipo documento non gestito.")
                Return
        End Select

        InitIntestazione(mio_record)

        'Tony 04-05-2023
        'Response.Write(tipo_documento & " " & numero_documento & " " & numero_crv)

        gestione_checkin.InitFormRDS(tipo_documento, numero_documento, numero_crv)
        
        tabConducente.Visible = True

        If tipo_documento = tipo_documento.MovimentoInterno Or tipo_documento = tipo_documento.Lavaggio Or tipo_documento = tipo_documento.ODL Or tipo_documento = tipo_documento.DuranteODL Then
            div_drivers.Visible = True
            anagrafica_conducenti_1.Visible = False
            anagrafica_conducenti_2.Visible = False
            Dim id_drivers As Integer = 0
            If mio_record.id_primo_conducente IsNot Nothing Then
                id_drivers = mio_record.id_primo_conducente
            End If

            anagrafica_drivers.InitFormDrivers(id_drivers, True)
        ElseIf tipo_documento >= Global.tipo_documento.RDSGenerico Then
            tabConducente.Visible = False

        Else
            anagrafica_conducenti_1.Visible = True
            anagrafica_conducenti_1.VisualizzaConducente(mio_record.id_primo_conducente)
            If mio_record.id_secondo_conducente IsNot Nothing Then
                anagrafica_conducenti_2.VisualizzaConducente(mio_record.id_secondo_conducente)
                div_anagrafica_conducenti_2.Visible = True
            Else
                div_anagrafica_conducenti_2.Visible = False
            End If
            div_drivers.Visible = False
        End If

        If tipo_documento = tipo_documento.Contratto Then
            tabTariffa.Visible = True
            AccessoriSelezionati.InitForm(numero_documento, numero_crv)
        Else
            tabTariffa.Visible = False
        End If

        gestione_checkin_storico.InitFormCheckOutRDS(tipo_documento, numero_documento, numero_crv)

        Visibilita(DivVisibile.FormRDS)


        
    End Sub

    Protected Sub listViewEventiDanni_ItemCommand(sender As Object, e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listViewEventiDanni.ItemCommand
        If e.CommandName = "lente" Then
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneRDS) = 1 Then
                Libreria.genUserMsgBox(Page, "Non hai i diritti per visualizzare l'evento danno.")
                Return
            End If

            Dim lb_id_tipo_documento_apertura As Label = e.Item.FindControl("lb_id_tipo_documento_apertura")
            Dim lb_id_documento_apertura As Label = e.Item.FindControl("lb_id_documento_apertura")
            Dim lb_num_crv_apertura As Label = e.Item.FindControl("lb_num_crv_apertura")

            VisualizzaRDS(Integer.Parse(lb_id_tipo_documento_apertura.Text), Integer.Parse(lb_id_documento_apertura.Text), Integer.Parse(lb_num_crv_apertura.Text))

        ElseIf e.CommandName = "pagamento" Then

            ' ATTENZIONE MODIFICARE IL PERMESSO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneDanni) = 1 Then
                Libreria.genUserMsgBox(Page, "Non hai i diritti per effettuare il pagamento dell'evento danno.")
                Return
            End If

            Dim lb_id_evento_apertura As Label = e.Item.FindControl("lb_id_evento_apertura")

            Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(Integer.Parse(lb_id_evento_apertura.Text))

            If mio_evento Is Nothing Then
                Libreria.genUserMsgBox(Page, "Errore nel recuperare l'evento danno.")
                Return
            End If

            Dim lb_id_tipo_documento_apertura As Label = e.Item.FindControl("lb_id_tipo_documento_apertura")
            Dim lb_id_documento_apertura As Label = e.Item.FindControl("lb_id_documento_apertura")
            Dim lb_num_crv_apertura As Label = e.Item.FindControl("lb_num_crv_apertura")

            lb_id_evento.Text = lb_id_evento_apertura.Text
            lb_id_tipo_documento.Text = lb_id_tipo_documento_apertura.Text
            lb_id_documento.Text = lb_id_documento_apertura.Text
            lb_num_crv.Text = lb_num_crv_apertura.Text

            Dim mio_record As DatiContratto = Nothing

            Dim id_tipo_documento As tipo_documento = Integer.Parse(lb_id_tipo_documento_apertura.Text)
            Dim id_documento As Integer = Integer.Parse(lb_id_documento_apertura.Text)
            Dim num_crv As Integer = Integer.Parse(lb_num_crv_apertura.Text)
            Select Case id_tipo_documento
                Case tipo_documento.Contratto
                    mio_record = DatiContratto.getRecordDaNumContratto(id_documento, num_crv)
                    If mio_record Is Nothing Then
                        Libreria.genUserMsgBox(Page, "Contratto non trovato.")
                        Return
                    End If
                Case Else
                    Libreria.genUserMsgBox(Page, "Tipo documento non gestito.")
                    Return
            End Select

            lb_num_rds.Text = mio_evento.id_rds
            InitIntestazione(mio_record)
            InitFormPagamento(mio_evento, mio_record)

            DettagliPagamento.InitForm(mio_record.num_prenotazione & "", mio_record.num_contratto & "", mio_evento.id_rds & "", , True)

            Visibilita(DivVisibile.GestionePagamenti)
        End If
    End Sub

    Protected Sub InitFormPagamento(mio_evento As veicoli_evento_apertura_danno, mio_record As DatiContratto)
        With mio_evento
            tx_stimato.Text = Libreria.myFormatta(.importo, "0.00")
            tx_iva.Text = Libreria.getAliquotaIVADaId(.iva)
            tx_spese_postali.Text = Libreria.myFormatta(.spese_postali, "0.00")
            tx_totale_rds.Text = Libreria.myFormatta(.totale, "0.00")
            Dim totale_ra As Double = 0
            If mio_record.totale_da_incassare IsNot Nothing Then
                totale_ra = mio_record.totale_da_incassare
            End If
            lb_num_contratto_pagamento.Text = mio_record.num_contratto
            tx_totale_contratto.Text = Libreria.myFormatta(mio_record.totale_da_incassare, "0.00")
            tx_totale_ra_rds.text = Libreria.myFormatta(.totale + totale_ra, "0.00")
        End With
    End Sub

    Protected Sub InizializzaScambioImportoNuovaPreautorizzazione(mio_evento As veicoli_evento_apertura_danno, simulazione As Boolean)

        Dim Dati As New tabelle_pos_Scambio_Importo.DatiInizializzazione()
        Dati.IDStazione = Request.Cookies("SicilyRentCar")("stazione")
        Dati.NumeroDocumento = mio_evento.id_rds
        Dati.TipoDocumento = tabelle_pos_Scambio_Importo.TipoDocumento.RDS
        Dati.ListaPreautorizzazioni = New System.Collections.Generic.List(Of tabelle_pos_Scambio_Importo.Preautorizzazione)

        'RECUPERO EVENTUALI PREAUTORIZZAZIONI APERTE SE C'E' UNA PRENOTAZIONE (ALTRIMENTI DI SICURO NON E' MAI STATO FATTO ALCUN PAGAMENTO)
        Dim mio_record As DatiContratto = Nothing

        Dim id_tipo_documento As tipo_documento = mio_evento.id_tipo_documento_apertura
        Dim id_documento As Integer = mio_evento.id_documento_apertura
        Dim num_crv As Integer = mio_evento.num_crv
        Select Case id_tipo_documento
            Case tipo_documento.Contratto
                mio_record = DatiContratto.getRecordDaNumContratto(id_documento, num_crv)
                If mio_record Is Nothing Then
                    Libreria.genUserMsgBox(Page, "Contratto non trovato.")
                    Return
                End If
            Case Else
                Libreria.genUserMsgBox(Page, "Tipo documento non gestito.")
                Return
        End Select

        Dim preautorizzazioni(50) As String
        preautorizzazioni(0) = "0"
        ' non carico alcuna preautorizzazione
        'preautorizzazioni = cPagamenti.getListPreautorizzazioni(mio_record.num_prenotazione & "", mio_evento.id_rds, "", mio_record.num_contratto)


        Dim i As Integer = 0

        Dim pre As tabelle_pos_Scambio_Importo.Preautorizzazione
        pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()

        Do While preautorizzazioni(i) <> "0"
            pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()
            pre.Numero = preautorizzazioni(i)
            Dati.ListaPreautorizzazioni.Add(pre)
            i = i + 1
        Loop

        Dati.Importo = mio_evento.totale ' l'importo coincide con il totale RDS!

        If simulazione Then
            Dati.importo_non_modificabile_preautorizzazione = True
            Dati.TestMode = True
        Else
            Dati.importo_non_modificabile_preautorizzazione = False
            Dati.TestMode = False
        End If

        Dati.ImportoMassimoRimborsabile = 0   '<<<----- IMPOSTARE

        'Dati.PreSelectIDEnte = 13
        'Dati.PreSelectIDAcquireCircuito = 64
        'Dati.PreSelectIDFunzione = Costanti.POS_FunzioneIntegrazione
        'Dati.PreSelectPOSID = 16
        'Dati.PreSelectNumeroPreautorizzazione = "363320898"

        lb_si_origine.Text = OrigineScambioImporto.Pag_NuovaPreautorizzazione
        lb_si_importo.Text = Dati.Importo

        Scambio_Importo1.InizializzazioneDati(Dati)

        Visibilita(DivVisibile.FormPagamenti)
    End Sub

    Protected Sub InizializzaScambioImportoPagamento_RDS(ByVal mio_evento As veicoli_evento_apertura_danno, ByVal preautorizzazione As String, ByVal simulazione As Boolean)

        Dim Dati As New tabelle_pos_Scambio_Importo.DatiInizializzazione()
        Dati.IDStazione = Request.Cookies("SicilyRentCar")("stazione")
        Dati.NumeroDocumento = mio_evento.id_rds
        Dati.TipoDocumento = tabelle_pos_Scambio_Importo.TipoDocumento.RDS
        Dati.ListaPreautorizzazioni = New System.Collections.Generic.List(Of tabelle_pos_Scambio_Importo.Preautorizzazione)


        Dim preautorizzazioni(50) As String
        preautorizzazioni(0) = preautorizzazione ' valorizzo solo la preautorizzazione selezionata dal form DettaglioPagamenti
        preautorizzazioni(1) = "0"

        Dim i As Integer = 0

        Dim pre As tabelle_pos_Scambio_Importo.Preautorizzazione
        pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()

        Do While preautorizzazioni(i) <> "0"
            pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()
            pre.Numero = preautorizzazioni(i)
            Dati.ListaPreautorizzazioni.Add(pre)
            i = i + 1
        Loop

        Dati.Importo = mio_evento.totale ' l'importo coincide con il totale RDS!


        If simulazione Then
            Dati.importo_non_modificabile_preautorizzazione = True
            Dati.TestMode = True
        Else
            Dati.importo_non_modificabile_preautorizzazione = False
            Dati.TestMode = False
        End If

        Dati.ImportoMassimoRimborsabile = 0   '<<<----- IMPOSTARE

        'Dati.PreSelectIDEnte = 13
        'Dati.PreSelectIDAcquireCircuito = 64
        'Dati.PreSelectIDFunzione = Costanti.POS_FunzioneIntegrazione
        'Dati.PreSelectPOSID = 16
        'Dati.PreSelectNumeroPreautorizzazione = "363320898"

        lb_si_origine.Text = OrigineScambioImporto.Pag_RDS
        lb_si_importo.Text = Dati.Importo

        Scambio_Importo1.InizializzazioneDati(Dati)

        Visibilita(DivVisibile.FormPagamenti)
    End Sub

    Protected Sub InizializzaScambioImportoPagamento_RDS_RA(ByVal mio_evento As veicoli_evento_apertura_danno, ByVal preautorizzazione As String, ByVal simulazione As Boolean)

        Dim mio_record As DatiContratto = Nothing
        Dim Dati As New tabelle_pos_Scambio_Importo.DatiInizializzazione()

        Dim id_tipo_documento As tipo_documento = mio_evento.id_tipo_documento_apertura
        Dim id_documento As Integer = mio_evento.id_documento_apertura
        Dim num_crv As Integer = mio_evento.num_crv
        Select Case id_tipo_documento
            Case tipo_documento.Contratto
                mio_record = DatiContratto.getRecordDaNumContratto(id_documento, num_crv)
                If mio_record Is Nothing Then
                    Libreria.genUserMsgBox(Page, "Contratto non trovato.")
                    Return
                End If

                Dati.NumeroDocumento = mio_evento.id_documento_apertura ' il documento è quello del contratto per tipo_documento = 1
                Dati.TipoDocumento = tabelle_pos_Scambio_Importo.TipoDocumento.Contratto
            Case Else
                Libreria.genUserMsgBox(Page, "Tipo documento non gestito.")
                Return
        End Select

        Dati.IDStazione = Request.Cookies("SicilyRentCar")("stazione")
        Dati.ListaPreautorizzazioni = New System.Collections.Generic.List(Of tabelle_pos_Scambio_Importo.Preautorizzazione)


        Dim preautorizzazioni(50) As String
        preautorizzazioni(0) = preautorizzazione ' valorizzo solo la preautorizzazione selezionata dal form DettaglioPagamenti
        preautorizzazioni(1) = "0"

        Dim i As Integer = 0

        Dim pre As tabelle_pos_Scambio_Importo.Preautorizzazione
        pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()

        Do While preautorizzazioni(i) <> "0"
            pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()
            pre.Numero = preautorizzazioni(i)
            Dati.ListaPreautorizzazioni.Add(pre)
            i = i + 1
        Loop

        Dim totale_da_incassare As Double
        If mio_record.totale_da_incassare Is Nothing Then
            totale_da_incassare = 0
        Else
            totale_da_incassare = mio_record.totale_da_incassare
        End If

        Dati.Importo = mio_evento.totale + totale_da_incassare ' l'importo coincide con il totale RDS + l'importo del contratto!

        If simulazione Then
            Dati.importo_non_modificabile_preautorizzazione = True
            Dati.TestMode = True
        Else
            Dati.importo_non_modificabile_preautorizzazione = False
            Dati.TestMode = False
        End If

        Dati.ImportoMassimoRimborsabile = 0   '<<<----- IMPOSTARE

        'Dati.PreSelectIDEnte = 13
        'Dati.PreSelectIDAcquireCircuito = 64
        'Dati.PreSelectIDFunzione = Costanti.POS_FunzioneIntegrazione
        'Dati.PreSelectPOSID = 16
        'Dati.PreSelectNumeroPreautorizzazione = "363320898"

        lb_si_origine.Text = OrigineScambioImporto.Pag_RDS_RA
        lb_si_importo.Text = Dati.Importo

        Scambio_Importo1.InizializzazioneDati(Dati)

        Visibilita(DivVisibile.FormPagamenti)
    End Sub

    Protected Sub InizializzaScambioImportoPagamento_RA(ByVal mio_evento As veicoli_evento_apertura_danno, ByVal preautorizzazione As String, ByVal simulazione As Boolean)

        Dim mio_record As DatiContratto = Nothing
        Dim Dati As New tabelle_pos_Scambio_Importo.DatiInizializzazione()

        Dim id_tipo_documento As tipo_documento = mio_evento.id_tipo_documento_apertura
        Dim id_documento As Integer = mio_evento.id_documento_apertura
        Dim num_crv As Integer = mio_evento.num_crv
        Select Case id_tipo_documento
            Case tipo_documento.Contratto
                mio_record = DatiContratto.getRecordDaNumContratto(id_documento, num_crv)
                If mio_record Is Nothing Then
                    Libreria.genUserMsgBox(Page, "Contratto non trovato.")
                    Return
                End If

                Dati.NumeroDocumento = mio_evento.id_documento_apertura ' il documento è quello del contratto per tipo_documento = 1
                Dati.TipoDocumento = tabelle_pos_Scambio_Importo.TipoDocumento.Contratto
            Case Else
                Libreria.genUserMsgBox(Page, "Tipo documento non gestito.")
                Return
        End Select

        Dati.IDStazione = Request.Cookies("SicilyRentCar")("stazione")
        Dati.ListaPreautorizzazioni = New System.Collections.Generic.List(Of tabelle_pos_Scambio_Importo.Preautorizzazione)


        Dim preautorizzazioni(50) As String
        preautorizzazioni(0) = preautorizzazione ' valorizzo solo la preautorizzazione selezionata dal form DettaglioPagamenti
        preautorizzazioni(1) = "0"

        Dim i As Integer = 0

        Dim pre As tabelle_pos_Scambio_Importo.Preautorizzazione
        pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()

        Do While preautorizzazioni(i) <> "0"
            pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()
            pre.Numero = preautorizzazioni(i)
            Dati.ListaPreautorizzazioni.Add(pre)
            i = i + 1
        Loop

        Dim totale_da_incassare As Double
        If mio_record.totale_da_incassare Is Nothing Then
            totale_da_incassare = 0
        Else
            totale_da_incassare = mio_record.totale_da_incassare
        End If

        Dati.Importo = totale_da_incassare ' l'importo coincide con il totale del contratto!

        If simulazione Then
            Dati.importo_non_modificabile_preautorizzazione = True
            Dati.TestMode = True
        Else
            Dati.importo_non_modificabile_preautorizzazione = False
            Dati.TestMode = False
        End If

        Dati.ImportoMassimoRimborsabile = 0   '<<<----- IMPOSTARE

        'Dati.PreSelectIDEnte = 13
        'Dati.PreSelectIDAcquireCircuito = 64
        'Dati.PreSelectIDFunzione = Costanti.POS_FunzioneIntegrazione
        'Dati.PreSelectPOSID = 16
        'Dati.PreSelectNumeroPreautorizzazione = "363320898"

        lb_si_origine.Text = OrigineScambioImporto.Pag_RA
        lb_si_importo.Text = Dati.Importo

        Scambio_Importo1.InizializzazioneDati(Dati)

        Visibilita(DivVisibile.FormPagamenti)
    End Sub

    Protected Sub lb_num_documento_Click(sender As Object, e As System.EventArgs) Handles lb_num_documento.Click
        Dim id_tipo_documento As tipo_documento = Integer.Parse(lb_id_tipo_documento.Text)
        Dim id_documento As Integer = Integer.Parse(lb_id_documento.Text)
        Dim num_crv As Integer = Integer.Parse(lb_num_crv.Text)

        Dim mio_record As DatiContratto = Nothing

        Select Case id_tipo_documento
            Case tipo_documento.Contratto
                mio_record = DatiContratto.getRecordDaNumContratto(id_documento, num_crv)

                Session("carica_contratto_da_gestione_rds") = mio_record.id & ""

                If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('contratti.aspx','','');", True)
                End If
                'Case tipo_documento.MovimentoInterno
                '    mio_record = DatiContratto.getRecordDaNumTrasferimento(id_documento)
                '    If mio_record Is Nothing Then
                '        Libreria.genUserMsgBox(Page, "Movimento Interno non trovato.")
                '        Return
                '    End If

                '    Session("carica_contratto_da_gestione_rds") = mio_record.id & ""

                '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                '        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('contratti.aspx','','');", True)
                '    End If
            Case Else
                Libreria.genUserMsgBox(Page, "Tipo documento non previsto")
                Return
        End Select
    End Sub


    Public Sub InitForm(ByVal filtro As filtro_rds)
        With filtro
            tx_RDS.Text = .id_rds
            tx_contratto.Text = .contratto
            tx_targa.Text = .targa
            DropDownList_stato_rds.SelectedValue = .stato_rds
            DropDownStazioni.SelectedValue = .id_stazione
            tx_RdsDataDa.Text = .RdsDataDa
            tx_RdsDataA.Text = .RdsDataA
            tx_PagDataDa.Text = .PagDataDa
            tx_PagDataA.Text = .PagDataA
            tx_PeriziaDataDa.Text = .PeriziaDataDa
            tx_PeriziaDataA.Text = .PeriziaDataA
            DropDownOrigine.SelectedValue = .id_origine

            'SU RICHIESTA DI SCALIA LA STAZIONE E' MODIFICABILE ANCHE IN CASO DI SOLA LETTURA
            'DropDownStazioni.Enabled = .CampiModificabili

            DropDownStazioni.Enabled = True

            lb_AbilitaPagamento.Text = .AbilitaPagamento

            lb_AbilitaLente.Text = .AbilitaLente

            If .EseguiRicerca Then
                bt_cerca_rds_Click(Nothing, Nothing)
            Else
                Visibilita(DivVisibile.Ricerca)
            End If
        End With
    End Sub

    Protected Function getFiltroRicerca() As String
        getFiltroRicerca = "id_rds=" & tx_RDS.Text & "&" & _
            "contratto=" & tx_contratto.Text & "&" & _
            "id_proprietario=" & DropDownProprietario.SelectedValue & "&" & _
            "targa=" & tx_targa.Text & "&" & _
            "stato_rds=" & DropDownList_stato_rds.SelectedValue & "&" & _
            "id_stazione=" & DropDownStazioni.SelectedValue & "&" & _
            "RdsDataDa=" & tx_RdsDataDa.Text & "&" & _
            "RdsDataA=" & tx_RdsDataA.Text & "&" & _
            "PagDataDa=" & tx_PagDataDa.Text & "&" & _
            "PagDataA=" & tx_PagDataA.Text & "&" & _
            "PeriziaDataDa=" & tx_PeriziaDataDa.Text & "&" & _
            "PeriziaDataA=" & tx_PeriziaDataA.Text & "&" & _
            "id_origine=" & DropDownOrigine.SelectedValue
    End Function

    Protected Sub bt_stampa_Click(sender As Object, e As System.EventArgs) Handles bt_stampa.Click
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            Dim url_print As String = "/Stampe/stampa_elenco_rds.aspx?orientamento=orizzontale&" & getFiltroRicerca()
            Trace.Write(url_print)
            Dim mio_random As String = Format((New Random).Next(), "0000000000")
            Trace.Write(url_print)
            Session("url_print") = url_print
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx?a=" & mio_random & "','')", True)
        End If
    End Sub

    Protected Sub bt_chiudi_pagamento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_chiudi_pagamento.Click
        Visibilita(DivVisibile.ElencoEventi)
    End Sub

    Protected Sub bt_nuova_preautorizzazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_nuova_preautorizzazione.Click
        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(Integer.Parse(lb_id_evento.Text))

        InizializzaScambioImportoNuovaPreautorizzazione(mio_evento, Boolean.Parse(DropDownSimulazione.SelectedValue))
    End Sub

    Protected Sub bt_vedi_stato_Click(sender As Object, e As System.EventArgs) Handles bt_vedi_stato.Click
        Dim sqlStr As String = "SELECT id FROM veicoli WHERE targa='" & Replace(txtCercaTargaStato.Text, "'", "''") & "'"

        Dim id_veicolo As String

        Using Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                id_veicolo = Cmd.ExecuteScalar() & ""
            End Using
        End Using

        If id_veicolo <> "" Then
            statoUsoVeicolo(id_veicolo)
        Else
            Libreria.genUserMsgBox(Page, "Targa non trovata.")
        End If

    End Sub
End Class
