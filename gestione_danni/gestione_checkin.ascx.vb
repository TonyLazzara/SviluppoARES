Imports System.IO
Imports System.Collections.Generic


'Tony 16/08/2022
Imports System.Net.Mail
Imports System.Net
Imports Microsoft.VisualBasic 'have to have this namespace to use msgbox
Imports funzioni_comuni
'Fine Tony

' Imports System.Runtime.Serialization



Public Enum stato_form_danni
    NonDefinito = 0
    DaCheckOut
    DaCheckIn
    DaRDS
    DaCheckOutRDS
End Enum



Partial Class gestione_danni_gestione_checkin
    Inherits System.Web.UI.UserControl

    Public Delegate Sub EventoChiusuraForm(ByVal sender As Object, ByVal e As EventArgs)
    Event ChiusuraForm As EventHandler

    Public Delegate Sub EventoSalvaRDS(ByVal sender As Object, ByVal e As EventArgs)
    Event SalvaRDS As EventHandler

    Public Delegate Sub EventoSalvaCheckIn(ByVal sender As Object, ByVal e As EventoNuovoRecord)
    Event SalvaCheckIn As EventHandler

    Public Delegate Sub EventoSalvaCheckInConFurto(ByVal sender As Object, ByVal e As EventArgs)
    Event SalvaCheckInConFurto As EventHandler

    Public Delegate Sub EventoPagamentoDanno(ByVal sender As Object, ByVal e As EventoNuovoRecord)
    Event PagamentoDanno As EventHandler

    Dim id_evento_old_per_ItemDataBound As Integer = -1
    Dim sqla As String = ""

    Private Enum DivVisibile
        Nessuno = 0

        Ricerca = 1

        DivIntestazione = 2

        DivStatoVeicolo = 4

        DivEvento = 8

        DivDanno = 16

        DivCheckIn = 32

        DivRDS = 64

        DivPagamenti = 128

        DivDettaglioEvento = 256

        DivPulsantiNuovoCheckIn = 512

        DivNota = 1024

        ' ------------------------ voci composte...
        Div_CheckOut = DivIntestazione Or DivStatoVeicolo Or DivPulsantiNuovoCheckIn
        Div_NuovoEvento = DivIntestazione Or DivStatoVeicolo Or DivPulsantiNuovoCheckIn Or DivNota
        Div_ReadEvento = DivIntestazione Or DivStatoVeicolo Or DivEvento Or DivDettaglioEvento Or DivCheckIn Or DivPagamenti Or DivNota
        Div_ReadEventoSenzaDanno = DivIntestazione Or DivStatoVeicolo Or DivCheckIn Or DivPagamenti
        Div_ReadEventoFurto = DivIntestazione Or DivStatoVeicolo Or DivCheckIn Or DivPagamenti Or DivNota

        Div_EditEvento = DivIntestazione Or DivStatoVeicolo Or DivEvento Or DivDettaglioEvento Or DivDanno Or DivCheckIn Or DivPagamenti Or DivNota
        Div_NessunDanno = DivIntestazione Or DivCheckIn
        Div_ReadEventoConDanno = DivIntestazione Or DivStatoVeicolo Or DivEvento Or DivDettaglioEvento Or DivDanno Or DivCheckIn Or DivPulsantiNuovoCheckIn Or DivPagamenti Or DivNota
        Div_Furto = DivIntestazione Or DivEvento Or DivDettaglioEvento

        Div_CheckOutRDS = DivStatoVeicolo
        Div_CheckOutRDSDettaglio = DivStatoVeicolo Or DivDanno
        Div_GestioneRDS = DivEvento Or DivDettaglioEvento Or DivRDS Or DivPagamenti Or DivNota
        Div_GestioneRDSConDanno = DivEvento Or DivDettaglioEvento Or DivRDS Or DivDanno Or DivPagamenti Or DivNota

    End Enum

    'Tony 16/08/2022
    Private Sub InvioMail(ByVal NumDoc, ByVal NumeroDiTarga, ByVal TipoDocumento)
        Dim Messaggio As String = ""

        Try
            Dim myMail As New MailMessage()

            Dim mySmtp As New SmtpClient("smtps.aruba.it")
            mySmtp.Port = 587
            mySmtp.Credentials = New System.Net.NetworkCredential("supporto@trinakriaservizi.it", "MailSupp@1")
            mySmtp.EnableSsl = True

            'Dim allegato As String = "C:\alazzara\comune" & AttualeFoglioPolarita & ".xlsx"

            Dim StringaEmailDestinatari As String = "amministrazione@sicilyrentcar.it,it-support@sicilyrentcar.it"
            'Dim StringaEmailDestinatari As String = "alazzara@inwind.it,it-support@sicilyrentcar.it"

            myMail = New MailMessage()
            myMail.From = New MailAddress("noreply@sicilyrentcar.it")
            myMail.To.Add(StringaEmailDestinatari)
            'myMail.Bcc.Add("tonyboyscoutlzz@gmail.com")
            Select Case TipoDocumento
                Case Is = "CAI"
                    myMail.Subject = "Inserimento Doc CAI -- RDS " & NumDoc.text
                Case Is = "Dichiarazione Cliente"
                    myMail.Subject = "Inserimento Doc Dichiarazione Cliente -- RDS " & NumDoc.text

            End Select

            'myMail.Attachments.Add(New Attachment(allegato))
            myMail.IsBodyHtml = True
            Select Case TipoDocumento
                Case Is = "CAI"
                    myMail.Body = "E' stato inserito Il documento CAI: <br>"
                Case Is = "Dichiarazione Cliente"
                    myMail.Body = "E' stato inserito Il documento Dichiarazione Cliente: <br>"
            End Select

            mySmtp.Send(myMail)

            'Messaggio = "Email inviata"

            'Console.WriteLine("Email inviata")
        Catch e As Exception
            Console.WriteLine(e.ToString)
            Messaggio = "Email NON inviata -" & NumDoc.text & "-- " & NumeroDiTarga.text & "-- " + TipoDocumento
            'Messaggio = NumDoc & "-- " & NumeroDiTarga.text + "-- " + TipoDocumento + "-- " + "Email NON inviata"
        End Try

        'If Messaggio <> "" Then
        '    Libreria.genUserMsgBox(Page, Messaggio)
        'End If
    End Sub
    'Fine Tony


    'Tony 26/08/2022
    Private Sub SalvaEventoApertura()
        Dim DbcSelect As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        DbcSelect.Open()

        Try
            Dim sqlStr As String = "SELECT * FROM veicoli_danni,veicoli_evento_apertura_danno  WITH(NOLOCK) WHERE veicoli_danni.id_evento_apertura = veicoli_evento_apertura_danno.id   AND  veicoli_danni.id_veicolo = '" & lb_id_veicolo.Text & "' and id_documento_apertura = '" & lb_num_documento.Text & "' and  veicoli_danni.attivo = 1"
            'Response.Write(sqlStr & "<br>")
            'Response.End()

                Dim CmdSelect As New Data.SqlClient.SqlCommand(sqlStr, DbcSelect)
                Dim RsSelect As Data.SqlClient.SqlDataReader
                RsSelect = CmdSelect.ExecuteReader()

            If Not RsSelect.HasRows Then 'Nessun danno presente                

                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
                Dim Sql As String
                Dim Sql2 As String
                Dim SqlQuery As String

                Try
                    Dim ArrayDataTime(1) As String
                    Dim ArrayData(2) As String

                    Dim DataOggi, DataOggiConOrario As String

                    ArrayDataTime = Split(Now, " ")
                    ArrayData = Split(ArrayDataTime(0), "/")
                    DataOggi = ArrayData(2) & "-" & ArrayData(1) & "-" & ArrayData(0)
                    DataOggiConOrario = ArrayData(2) & "-" & ArrayData(1) & "-" & ArrayData(0) & " " & ArrayDataTime(1)

                    Dim ArrayPercorso(3) As String
                    ArrayPercorso = Split(Request.ServerVariables("HTTP_REFERER"), "/")

                    If ArrayPercorso(2) <> "sviluppo.sicilyrentcar.it" Then
                        DataOggi = CDate(DataOggi)
                        DataOggiConOrario = CDate(DataOggiConOrario)
                    End If

                    Sql = "insert into veicoli_evento_apertura_danno (attivo,sospeso_rds,id_veicolo,id_tipo_documento_apertura,id_documento_apertura,num_crv,data,data_creazione,id_utente) " & _
                                                              "values('0','0','" & lb_id_veicolo.Text & "','" & lb_id_tipo_documento_apertura.Text & "','" & lb_id_documento_apertura.Text & "','" & lb_num_crv.Text & "','" & DataOggi & "','" & DataOggiConOrario & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "')"

                    'Response.Write(Sql)
                    'Response.End()

                    Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
                    Cmd.ExecuteNonQuery()

                    Sql2 = "Select @@IDENTITY FROM veicoli_evento_apertura_danno With(NOLOCK)"
                    Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                    Session("IdEventoApertura") = Int(Cmd.ExecuteScalar) - 1
                    txtIdEventoApertura.Text = Session("IdEventoApertura")

                    SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                    Cmd.CommandText = SqlQuery
                    'Response.Write(Cmd.CommandText & "<br/>")
                    'Response.End()
                    Cmd.ExecuteNonQuery()

                Catch ex As Exception
                    Response.Write(ex)
                    Response.Write("<br><br>")
                    Response.Write("Salvataggio Ins Danni Errore contattare amministratore del sistema.")
                    Response.Write(Cmd.CommandText)
                End Try

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            Else
                RsSelect.Read()
                txtIdEventoApertura.Text = RsSelect("id_evento_apertura")
            End If


                RsSelect.Close()
                CmdSelect.Dispose()
                DbcSelect.Close()
                RsSelect = Nothing
                CmdSelect = Nothing
                DbcSelect = Nothing
        Catch ex As Exception
            Response.Write(ex)
            Response.Write("<br><br>")
            Response.Write("Select Danni Errore contattare amministratore del sistema.")            
        End Try
    End Sub

    Private Sub AggiornaRecordApertuaDanno()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
        Dim Sql As String
        Dim Sql2 As String
        Dim SqlQuery As String

        Try
            Dim ArrayDataTime(1) As String
            Dim ArrayData(2) As String

            Dim DataOggi, DataOggiConOrario As String

            ArrayDataTime = Split(Now, " ")
            ArrayData = Split(ArrayDataTime(0), "/")
            DataOggi = ArrayData(2) & "-" & ArrayData(1) & "-" & ArrayData(0)
            DataOggiConOrario = ArrayData(2) & "-" & ArrayData(1) & "-" & ArrayData(0) & " " & ArrayDataTime(1)

            Dim ArrayPercorso(3) As String
            ArrayPercorso = Split(Request.ServerVariables("HTTP_REFERER"), "/")

            If ArrayPercorso(2) <> "sviluppo.sicilyrentcar.it" Then
                DataOggi = CDate(DataOggi)
                DataOggiConOrario = CDate(DataOggiConOrario)
            End If

            Sql = "update veicoli_evento_apertura_danno set id_tipo_documento_apertura = '" & lb_id_tipo_documento_apertura.Text & "', id_documento_apertura ='" & lb_id_documento_apertura.Text & "' where id=" & txtIdEventoApertura.Text
            'Response.Write(Sql & "<br>")
            'Response.End()

            Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
            Cmd.ExecuteNonQuery()

            'Sql2 = "Select @@IDENTITY FROM veicoli_evento_apertura_danno With(NOLOCK)"
            'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
            'Session("IdEventoApertura") = Int(Cmd.ExecuteScalar) - 1
            'txtIdEventoApertura.Text = Session("IdEventoApertura")

            SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
            Cmd.CommandText = SqlQuery
            'Response.Write(Cmd.CommandText & "<br/>")
            'Response.End()
            Cmd.ExecuteNonQuery()

        Catch ex As Exception
            Response.Write(ex)
            Response.Write("<br><br>")
            Response.Write("Salvataggio Ins Danni Errore contattare amministratore del sistema.")
            Response.Write(Cmd.CommandText)
        End Try

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub
    'Tony FINE

    Private Sub Visibilita_2(Valore As DivVisibile)
        div_ricerca.Visible = Valore And DivVisibile.Ricerca

        div_elenco_auto.Visible = Valore And DivVisibile.Ricerca

        div_targa.Visible = Valore And DivVisibile.DivIntestazione

        div_elenco_danni.Visible = Valore And DivVisibile.DivStatoVeicolo

        div_edit_evento.Visible = Valore And DivVisibile.DivEvento

        div_elenco_edit_evento.Visible = Valore And DivVisibile.DivDettaglioEvento

        div_edit_danno.Visible = Valore And DivVisibile.DivDanno

        div_salva_checkin.Visible = Valore And DivVisibile.DivCheckIn

        div_nuovo_checkin.Visible = Valore And DivVisibile.DivPulsantiNuovoCheckIn

        div_gestione_rds.Visible = Valore And DivVisibile.DivRDS

        tab_dettagli_pagamento.Visible = Valore And DivVisibile.DivPagamenti

        div_nota.Visible = Valore And DivVisibile.DivNota

    End Sub

    Protected Sub AbilitaEvento(Valore As Boolean)
        tx_data_evento.Enabled = Valore
        tx_nota_evento.Enabled = Valore

        tx_data_denuncia_furto.Enabled = Valore
        tx_ora_denuncia_furto.Enabled = Valore

        If lb_flag_richiede_id.Text = "1" Then
            DropDownTipoEventoAperturaDanno.Enabled = False
            div_num_documento.Visible = True
        Else
            DropDownTipoEventoAperturaDanno.Enabled = Valore
            div_num_documento.Visible = False ' sono nel caso non ci sia un documento associato...
        End If
    End Sub

    Protected Sub AbilitaCheckIn(Valore As Boolean)
        rb_ready_to_go.Enabled = Valore
        ck_lavare.Enabled = Valore
        ck_rifornire.Enabled = Valore
        ck_fermo_tecnico.Enabled = Valore
        ck_vendita_buy_back.Enabled = Valore
        tx_altro.Enabled = Valore
        tx_km_rientro.Enabled = Valore
        DropDownSerbatoioRientro.Enabled = Valore
        ck_furto.Enabled = Valore       '20.08.2021

        'Tony 20/05/2022        
        If Request.Path.ToString = "/gestione_danni.aspx" Then
            'Response.Write("IN")
            Label13.Visible = False
            tx_km_rientro.Visible = False

            Label15.Visible = False
            DropDownSerbatoioRientro.Visible = False
        Else
            'Response.Write("ELSE")
        End If

    End Sub

    Protected Sub DropDownTipoEventoAperturaDanno_DataBind()
        Trace.Write("DropDownPosizione_DataBinding")
        DropDownTipoEventoAperturaDanno.Items.Clear()
        DropDownTipoEventoAperturaDanno.Items.Add(New ListItem("Seleziona...", "0"))
        DropDownTipoEventoAperturaDanno.DataBind()
    End Sub

    Protected Sub aggiorna_elenco()
        Trace.Write("EditDanno_AggiornaElenco")
        listViewElencoDanniPerEvento.DataBind()
        lv_elenco_danni_F.DataBind()
        lv_elenco_danni_R.DataBind()
    End Sub

    Protected Sub EditDanno_AggiornaElenco(sender As Object, e As EventArgs)
        aggiorna_elenco()
    End Sub

    Protected Sub DisegnaPuntiMappa(id_gruppo_evento As tipo_documento)

        For i As Tipo_Img_Mappa = Tipo_Img_Mappa.Fronte To Tipo_Img_Mappa.Retro
            Dim mia_lista As List(Of I_veicoli_img_mappatura) = veicoli_img_mappatura_indicizzata.get_lista_punti_veicolo_con_indice_per_gruppo(id_gruppo_evento, i)

            Select Case i
                Case Tipo_Img_Mappa.Fronte
                    veicoli_img_mappatura_indicizzata.DisegnaSuContenitore(div_img_fronte, mia_lista, i)
                Case Tipo_Img_Mappa.Retro
                    veicoli_img_mappatura_indicizzata.DisegnaSuContenitore(div_img_retro, mia_lista, i)
            End Select
        Next
    End Sub

    Protected Sub InitImmagine(id_veicolo As Integer, id_gruppo_evento As tipo_documento)

        Trace.Write("InitImmagine --------------------- " & id_veicolo)
        Dim mio_macro_modello As veicoli_img_modelli = veicoli_img_modelli.getMacroModello(id_veicolo)
        With mio_macro_modello
            img_fronte.ImageUrl = "~\images\Mappe\" & .img_fronte
            img_retro.ImageUrl = "~\images\Mappe\" & .img_retro
        End With
        ViewState("veicoli_img_modelli") = mio_macro_modello
        DisegnaPuntiMappa(id_gruppo_evento)
        'Try

        'Catch ex As Exception
        '    Trace.Write("Errore Disegno Punti: " & ex.Message)
        'End Try
    End Sub

    Public Sub AggiornaAcessori(id_tipo_documento As tipo_documento, id_documento As Integer, numero_crv As Integer)
        Dim mio_record As DatiContratto = Nothing ' per  adesso così... in seguito dichiaro una interfaccia!
        Dim mio_gruppo_evento As veicoli_gruppo_evento = Nothing

        Select Case id_tipo_documento
            Case tipo_documento.Contratto
                mio_record = DatiContratto.getRecordDaNumContratto(id_documento, numero_crv)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: contratto non trovato.")
                End If
            Case tipo_documento.MovimentoInterno, tipo_documento.ODL, tipo_documento.DuranteODL
                ' non faccio niente perché in movimenti interni gli accessori non servono...
                Return

            Case Is >= tipo_documento.RDSGenerico
                Return

            Case Else
                Err.Raise(1001, Nothing, "Tipo documento non previsto")
        End Select

        mio_gruppo_evento = veicoli_gruppo_evento.getRecordDaId(mio_record.id_gruppo_danni_uscita)

        mio_gruppo_evento.AggiornaAcessori(mio_record.id, 0)
    End Sub

    Protected Sub elimina_rifornimenti(ByVal id_veicolo As String)
        Try
   Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
            sqla = "DELETE FROM rifornimenti WHERE id_veicolo='" & id_veicolo & "' AND data_uscita_parco IS NULL"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
        Catch ex As Exception
            Response.Write("error elimina_rifornimenti :" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Public Function InitFormCheckOut(ByVal id_tipo_documento As tipo_documento, ByVal id_documento As Integer, ByVal numero_crv As Integer) As Integer
        InitFormCheckOut = 0

        lb_stato_form.Text = stato_form_danni.DaCheckOut

        tab_mappe.ActiveTabIndex = 0

        lb_th_lente_storico.Text = False

        Dim mio_record As DatiContratto = Nothing ' per  adesso così... in seguito dichiaro una interfaccia!
        Dim mio_gruppo_evento As veicoli_gruppo_evento = Nothing

        Select Case id_tipo_documento
            Case tipo_documento.Contratto
                mio_record = DatiContratto.getRecordDaNumContratto(id_documento, numero_crv)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: contratto non trovato.")
                End If

            Case tipo_documento.MovimentoInterno
                mio_record = DatiContratto.getRecordDaNumTrasferimento(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: movimento interno non trovato.")
                End If

            Case tipo_documento.ODL
                mio_record = DatiContratto.getRecordDaNumODL(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                End If

            Case tipo_documento.DuranteODL
                mio_record = DatiContratto.getRecordDaNumDuranteODL(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                End If
            Case tipo_documento.Lavaggio
                mio_record = DatiContratto.getRecordDaLavaggio(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                End If
            Case Else
                Err.Raise(1001, Nothing, "Tipo documento non previsto")
        End Select

        

        If mio_record.id_gruppo_danni_uscita IsNot Nothing Then

            mio_gruppo_evento = veicoli_gruppo_evento.getRecordDaId(mio_record.id_gruppo_danni_uscita)
            'VISTO CHE POTREBBERO ESSERE CAMBIATI GLI ACCESSORI ACQUISTATI A NOLO IN CORSO, AGGIORNO LA RELATIVA LISTA
            ListView_accessori.DataBind()

            
        Else
            mio_gruppo_evento = veicoli_gruppo_evento.SalvaDanniApertiAuto(mio_record.id_veicolo, id_tipo_documento, id_documento, numero_crv, 0)

            Select Case id_tipo_documento
                Case tipo_documento.Contratto
                    mio_record.SalvaGruppoDanniCheckOut(mio_gruppo_evento.id)

                Case tipo_documento.MovimentoInterno
                    mio_record.SalvaGruppoDanniMovimentoInternoCheckOut(mio_gruppo_evento.id)

                Case tipo_documento.Lavaggio
                    mio_record.SalvaGruppoDanniLavaggio(mio_gruppo_evento.id)

                Case tipo_documento.ODL
                    mio_record.SalvaGruppoDanniODLCheckOut(mio_gruppo_evento.id)

                Case tipo_documento.DuranteODL
                    ' boooo

                Case Else
                    Err.Raise(1001, Nothing, "Tipo documento non previsto")
            End Select

            mio_record.id_gruppo_danni_uscita = mio_gruppo_evento.id
        End If

        'SU SALVATAGGIO DEL CHECK OUT E' NECESSARIO ELIMINARE LE RIGHE IN RIFORNIMENTI CHE NON SONO STATE UTILIZZATE
        elimina_rifornimenti(mio_record.id_veicolo)

        InitFormCheckOut = mio_record.id_gruppo_danni_uscita

        'Trace.Write("-----------------------------------------------------------------------------")
        'Trace.Write("InitFormCheckOut " & id_tipo_documento & " - " & id_documento & " - " & numero_crv & " - " & mio_record.num_prenotazione)
        'Trace.Write("-----------------------------------------------------------------------------")

        ViewState("DocumentoAssociato") = mio_record

        lb_id_gruppo_evento.Text = mio_gruppo_evento.id

        lb_id_tipo_documento_apertura.Text = id_tipo_documento
        lb_id_documento_apertura.Text = id_documento
        lb_num_crv.Text = numero_crv
        lb_num_prenotazione.Text = mio_record.num_prenotazione & ""
        lb_id_veicolo.Text = mio_record.id_veicolo
        lb_stato_danno.Text = 1 ' = aperto 
        lb_id_flag.Text = 1 ' = aperto 

        lb_id_ditta.Text = mio_record.id_cliente & ""

        Dim mio_tipo As veicoli_tipo_documento_apertura_danno = veicoli_tipo_documento_apertura_danno.get_record_da_id(id_tipo_documento)
        If mio_tipo.richiede_id Is Nothing OrElse mio_tipo.richiede_id = False Then
            lb_flag_richiede_id.Text = 0
        Else
            lb_flag_richiede_id.Text = 1
        End If
        DropDownTipoEventoAperturaDanno_DataBind()

        InitImmagine(mio_record.id_veicolo, mio_gruppo_evento.id)

        InitIntestazione(mio_record)

        bt_stampa_atto_notorio.Visible = False
        bt_stampa_check_in.Visible = False
        bt_stampa_check_out.Visible = True
        bt_furto.Visible = False
        bt_nuovo_evento_danno.Visible = False
        bt_nessun_danno.Visible = False
        bt_chiudi_chekin.Visible = True

        Visibilita_2(DivVisibile.Div_CheckOut)


        lv_elenco_danni_F.DataBind()
        lv_elenco_danni_R.DataBind()
        ListView_dotazioni.DataBind()
        ListView_meccanici_elettrici.DataBind()
        listViewDocumenti.DataBind()

    End Function

    Public Sub FillManutenzioneOrdinaria(ByVal id_veicolo As String)
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            'PRELEVO LE INFORMAZIONI UTILI AL CALCOLO DELLA MANUTENZIONE ORDINARIA DALLA TABELLA veicoli
            sqla = "SELECT data_immatricolazione, primo_tagliando_mesi, successivo_tagliando_mesi, "
            sqla += "primo_tagliando_km, successivo_tagliando_km, sostituzione_gomme_km, vendita_veicolo_km, data_fine_leasing,"
            sqla += "data_ultimo_tagliando, km_ultimo_tagliando "
            sqla += "FROM veicoli WITH(NOLOCK) "
            sqla += "WHERE id='" & id_veicolo & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.Read Then
                'CALCOLO DELLA PROSSIMA DATA DEL TAGLIANDO ------------------------------------------------------------------------------------
                'COMPATIBILITA' CON DATI VECCHI: SE LA DATA DI IMMATRICOLAZIONE E' ASSENTE NON E' POSSIBILE CALCOLARE LE SCADENZE IN MESI
                If (Rs("data_immatricolazione") & "") = "" Then
                    lb_tagliando_data.Text = "N.D."
                ElseIf (Rs("primo_tagliando_mesi") & "") <> "" And (Rs("successivo_tagliando_mesi") & "") <> "" Then
                    Dim data_tagliando As DateTime = Rs("data_immatricolazione")
                    Dim data_odierna As DateTime = Now()
                    Dim data_ultimo_tagliando As DateTime = Nothing
                    If (Rs("data_ultimo_tagliando") & "") <> "" Then
                        data_ultimo_tagliando = Rs("data_ultimo_tagliando")
                    End If

                    data_tagliando = data_tagliando.AddMonths(Rs("primo_tagliando_mesi"))

                    'PER TRENTA GIORNI DOPO LA SCADENZA DEL TAGLIANDO MOSTRO LA VECCHIA SCADENZA - VADO AVANTI ANCHE SE L'ULTIMO TAGLIANDO 
                    'E' SUCCESSIVO ALLA DATA DI SCADENZA DELLO STESSO O PRECEDENTE DI 2 MESI 

                    If Rs("successivo_tagliando_mesi") <> 0 Then
                        Do While DateDiff(DateInterval.Day, data_tagliando, data_odierna) > 30
                            data_tagliando = data_tagliando.AddMonths(Rs("successivo_tagliando_mesi"))
                        Loop
                    End If

                    If Not data_ultimo_tagliando = Nothing Then
                        If DateDiff(DateInterval.Day, data_tagliando, data_ultimo_tagliando) > -60 Then
                            data_tagliando = data_tagliando.AddMonths(Rs("successivo_tagliando_mesi"))
                        End If
                    End If

                    lb_tagliando_data.Text = Format(data_tagliando, "dd/MM/yyyy")
                Else
                    lb_tagliando_data.Text = "N.D."
                End If

                If (Rs("data_ultimo_tagliando") & "") <> "" And (Rs("km_ultimo_tagliando") & "") <> "" Then
                    lb_ultimo_tagliando.Text = Format(Rs("data_ultimo_tagliando"), "dd/MM/yyyy") & " --- " & Rs("km_ultimo_tagliando")
                ElseIf (Rs("data_ultimo_tagliando") & "") <> "" And (Rs("km_ultimo_tagliando") & "") = "" Then
                    lb_ultimo_tagliando.Text = Format(Rs("data_ultimo_tagliando"), "dd/MM/yyyy")
                ElseIf (Rs("data_ultimo_tagliando") & "") = "" And (Rs("km_ultimo_tagliando") & "") <> "" Then
                    lb_ultimo_tagliando.Text = Rs("km_ultimo_tagliando")
                Else
                    lb_ultimo_tagliando.Text = "N.D."
                End If
                '-----------------------------------------------------------------------------------------------------------------------------
                'CALCOLO DEL PROSSIMO TAGLIANDO IN KM ----------------------------------------------------------------------------------------

                '-----------------------------------------------------------------------------------------------------------------------------

                'INFORMAZIONI DI VENDITA/BUY BACK --------------------------------------------------------------------------------------------
                If (Rs("vendita_veicolo_km") & "") <> "" Then
                    lb_vendita.Text = Rs("vendita_veicolo_km")
                Else
                    lb_vendita.Text = ""
                End If
                If (Rs("data_fine_leasing") & "") <> "" Then
                    lb_buy_back.Text = Format(Rs("data_fine_leasing"), "dd/MM/yyyy")
                Else
                    lb_buy_back.Text = ""
                End If
                '-----------------------------------------------------------------------------------------------------------------------------
            Else
                lb_tagliando_km.Text = ""
                lb_tagliando_data.Text = ""
                lb_sostituzione_gomme_data.Text = ""
                lb_sostituzione_gomme_km.Text = ""
                lb_vendita.Text = ""
                lb_buy_back.Text = ""
            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error FillManutenzioneOrdinaria :" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Public Sub InitFormCheckIn(ByVal id_tipo_documento As tipo_documento, ByVal id_documento As Integer, ByVal numero_crv As Integer) ' , Optional ByVal id_evento_apertura_odl As Integer = 0
        'Response.Write("IN InitFormCheckIn")
        lb_stato_form.Text = stato_form_danni.DaCheckIn

        lb_th_lente.Text = True
        lb_th_da_addebitare.Text = False
        lb_th_lente_storico.Text = False

        tab_mappe.ActiveTabIndex = 0

        Dim mio_record As DatiContratto = Nothing ' per  adesso così... in seguito dichiaro una interfaccia!

        Try

            'Response.Write(id_tipo_documento & "<br>")
            'Response.Write(id_documento)
            Select Case id_tipo_documento
                Case tipo_documento.Contratto
                    mio_record = DatiContratto.getRecordDaNumContratto(id_documento, numero_crv)
                    If mio_record Is Nothing Then
                        Err.Raise(1001, Nothing, "Errore: contratto non trovato.")
                    End If

                Case tipo_documento.MovimentoInterno
                    mio_record = DatiContratto.getRecordDaNumTrasferimento(id_documento)
                    If mio_record Is Nothing Then
                        Err.Raise(1001, Nothing, "Errore: movimento interno non trovato.")
                    End If
                Case tipo_documento.Lavaggio
                    mio_record = DatiContratto.getRecordDaLavaggio(id_documento)
                    If mio_record Is Nothing Then
                        Err.Raise(1001, Nothing, "Errore: movimento interno non trovato.")
                    End If
                Case tipo_documento.ODL
                    mio_record = DatiContratto.getRecordDaNumODL(id_documento)
                    If mio_record Is Nothing Then
                        Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                    End If

                Case Else
                    Err.Raise(1001, Nothing, "Tipo documento non previsto")
            End Select

            'Trace.Write("-----------------------------------------------------------------------------")
            'Trace.Write("InitFormCheckIn " & id_tipo_documento.ToString & " - " & id_documento & " - " & numero_crv & " - " & mio_record.num_prenotazione)
            'Trace.Write("-----------------------------------------------------------------------------")

            ViewState("DocumentoAssociato") = mio_record

            lb_id_tipo_documento_apertura.Text = id_tipo_documento
            lb_id_documento_apertura.Text = id_documento
            lb_num_crv.Text = numero_crv
            lb_num_prenotazione.Text = mio_record.num_prenotazione & ""
            lb_id_veicolo.Text = mio_record.id_veicolo
            lb_stato_danno.Text = 1 ' = aperto 
            lb_id_flag.Text = 1 ' = aperto 

            lb_id_ditta.Text = mio_record.id_cliente & ""

            Dim mio_tipo As veicoli_tipo_documento_apertura_danno = veicoli_tipo_documento_apertura_danno.get_record_da_id(id_tipo_documento)
            If mio_tipo.richiede_id Is Nothing OrElse mio_tipo.richiede_id = False Then
                lb_flag_richiede_id.Text = 0
            Else
                lb_flag_richiede_id.Text = 1
            End If
            DropDownTipoEventoAperturaDanno_DataBind()

            lb_km_uscita_memo.Text = "(Km uscita " & mio_record.km_uscita & ")"

            Dim capacita_serbatoio As Integer = getSerbatoioDaIdVeicolo(mio_record.id_veicolo)
            lb_serbatoio_memo.Text = "(Serbatoio " & capacita_serbatoio & ")"

            For i = 0 To 8
                If DropDownSerbatoioRientro.Items(i).Value <> "-1" And DropDownSerbatoioRientro.Items(i).Value <> "0" Then
                    DropDownSerbatoioRientro.Items(i).Text = DropDownSerbatoioRientro.Items(i).Value & "/8 (" & CInt(capacita_serbatoio / 8 * DropDownSerbatoioRientro.Items(i).Value) & ")"
                End If
            Next

            If mio_record.id_gruppo_danni_rientro Is Nothing Then
                Dim rds_esistente As Boolean = False

                bt_pagamento.Visible = False

                CompareValidatorKmRientro.ValueToCompare = mio_record.km_uscita
                CompareValidatorKmRientro.ErrorMessage = "Specificare un valore corretto per i km di rientro presenti al check in, maggiore o uguale dei km di uscita (" & mio_record.km_uscita & " km)"

                DropDownTipoEventoAperturaDanno_DataBind()
                DropDownTipoEventoAperturaDanno.SelectedValue = id_tipo_documento

                Dim mio_evento As veicoli_evento_apertura_danno = Nothing

                ' -------------------------------------------------------------------------------------------------------------------
                ' ATTENZIONE:
                ' anche se apro il check in da contratto 
                ' devo verificare se sul contratto non esista un evento danno aperto da un ODL
                ' nel caso positivo devo verificare che l'odl corrispondente sia stato nello stato chiuso
                ' in caso contrario non posso effettuare il check del noleggio ??????????????????????????????????????????????????????
                ' oppure devo comunque consentire di procedere all'utente ???????????????????????????????????????????????????????????
                ' -------------------------------------------------------------------------------------------------------------------
                If id_tipo_documento = tipo_documento.Contratto Then
                    mio_evento = veicoli_evento_apertura_danno.getRecordDaDocumento(id_tipo_documento, id_documento, numero_crv)

                    If mio_evento IsNot Nothing Then
                        rds_esistente = True
                        lb_id_evento.Text = mio_evento.id


                        ' Se mio_evento non è nothing vuol dire che pur non essendo stato effettuato il check in
                        ' sono presenti dei danni inseriti nella procedura dell'ODL
                        ' devo gestire l'eventuale presenza di danni legati all'evento ma NON ATTIVI !!!
                        ' Cancello eventuali danni non attivi presenti sull'evento
                        mio_evento.EliminaDanniNonAttivi()

                        Dim mio_odl As odl = odl.getRecordDaDocumento(id_tipo_documento, id_documento, numero_crv)
                        If mio_odl IsNot Nothing Then
                            If mio_odl.id_stato_odl < enum_odl_stato.Chiuso Then ' Chiuso = 9 
                                Libreria.genUserMsgBox(Page, "ATTENZIONE:" & vbCrLf & "L'ODL (" & mio_odl.num_odl & ") su questo contratto non risulta ancora chiuso!")
                            End If
                        End If
                    End If
                End If


                If mio_evento Is Nothing Then
                    mio_evento = New veicoli_evento_apertura_danno ' veicoli_evento_apertura_danno.getRecordDaDocumento(id_tipo_documento, id_documento)

                    With mio_evento
                        .id_veicolo = mio_record.id_veicolo

                        .attivo = False
                        .id_tipo_documento_apertura = id_tipo_documento
                        .id_documento_apertura = id_documento
                        .num_crv = numero_crv
                        .data = Now
                        .nota = ""
                        .id_ditta = mio_record.id_cliente

                        .SalvaRecord()
                    End With
                End If

                FillEvento(mio_evento)

                'MANUTENZIONE ORDINARIA ---------------------------------------------
                manutenzione_ordinaria.Visible = True
                FillManutenzioneOrdinaria(mio_evento.id_veicolo)
                '--------------------------------------------------------------------

                lb_id_evento.Text = mio_evento.id

                listViewElencoDanniPerEvento.DataBind()

                lb_intestazione_evento.Text = "Nuovo Evento"
                lb_evento_modificato.Text = ""


                'aggiunta riga se passa il valore Nothing perchè NULL nel campo id_gruppo_danni_uscita su contratti 16.01.2021
                If IsNothing(mio_record.id_gruppo_danni_uscita) Or IsDBNull(mio_record.id_gruppo_danni_uscita) Then
                    mio_record.id_gruppo_danni_uscita = 0
                End If

                edit_danno.InitForm(mio_evento.id, , mio_record.id_gruppo_danni_uscita)



                AzzeraCheckIn()

                lb_id_gruppo_evento.Text = mio_record.id_gruppo_danni_uscita


                InitImmagine(mio_record.id_veicolo, mio_record.id_gruppo_danni_uscita)


                InitIntestazione(mio_record)

                AbilitaCheckIn(True)
                AbilitaEvento(True)

                tab_checkin.Visible = True

                ' sulla scheda storico...

                If id_tipo_documento <> tipo_documento.Lavaggio Then
                    bt_stampa_atto_notorio.Visible = True
                    bt_stampa_check_in.Visible = False
                    bt_stampa_check_out.Visible = True
                Else
                    bt_stampa_atto_notorio.Visible = False
                    bt_stampa_check_in.Visible = False
                    bt_stampa_check_out.Visible = False
                End If

                ' bt_furto.Visible = True  ' per adesso disabilito il furto....
                bt_nuovo_evento_danno.Visible = True
                bt_nessun_danno.Visible = True
                bt_chiudi_chekin.Visible = True

                ' sulla scheda Dettaglio Evento
                bt_nuovo_danno.Visible = True
                ' per gestire furto...
                bt_addebita_furto.Visible = False
                bt_non_addebita_furto.Visible = False
                bt_rientro_veicolo_rubato.Visible = False
                bt_pagamento_da_furto.Visible = False
                bt_salva_furto.Visible = False
                bt_chiudi_senza_furto.Visible = False

                ' sulla scheda Check In (ancora non visibile...)
                bt_pagamento.Visible = False
                bt_salva_checkin.Visible = True

                If rds_esistente Then
                    bt_nessun_danno.Visible = False

                    tab_mappe.ActiveTabIndex = 0

                    Visibilita_2(DivVisibile.Div_EditEvento)
                Else
                    Visibilita_2(DivVisibile.Div_NuovoEvento)
                End If

            Else
                ' sono nel caso di check in già effettuato (visualizzo solamente le informazioni salvate!)
                Dim record_check_in As veicoli_check_in = veicoli_check_in.getRecordDaDocumento(id_tipo_documento, id_documento, numero_crv)
                If record_check_in IsNot Nothing Then
                    FillCheckIn(record_check_in)
                Else
                    AzzeraCheckIn()
                End If

                lb_intestazione_evento.Text = "Evento"

                DropDownTipoEventoAperturaDanno_DataBind()
                DropDownTipoEventoAperturaDanno.SelectedValue = id_tipo_documento

                lb_id_gruppo_evento.Text = mio_record.id_gruppo_danni_rientro

                Dim mio_gruppo_evento As veicoli_gruppo_evento = veicoli_gruppo_evento.getRecordDaId(mio_record.id_gruppo_danni_rientro)

                lb_id_evento.Text = mio_gruppo_evento.id_evento

                Dim mio_evento As veicoli_evento_apertura_danno = Nothing
                If mio_gruppo_evento.id_evento Is Nothing Then
                    AzzeraEvento()
                Else
                    mio_evento = veicoli_evento_apertura_danno.getRecordDaId(mio_gruppo_evento.id_evento)
                    If mio_evento Is Nothing Then
                        AzzeraEvento()
                    Else
                        FillEvento(mio_evento)
                    End If
                End If

                InitImmagine(mio_record.id_veicolo, mio_record.id_gruppo_danni_rientro)

                InitIntestazione(mio_record)

                bt_nuovo_danno.Visible = False
                bt_salva_checkin.Visible = False

                AbilitaCheckIn(False)
                AbilitaEvento(False)

                ' sulla scheda storico...
                If id_tipo_documento <> tipo_documento.Lavaggio Then
                    bt_stampa_atto_notorio.Visible = True
                    bt_stampa_check_in.Visible = True
                    bt_stampa_check_out.Visible = True
                Else
                    bt_stampa_atto_notorio.Visible = False
                    bt_stampa_check_in.Visible = False
                    bt_stampa_check_out.Visible = False
                End If


                bt_furto.Visible = False
                bt_nuovo_evento_danno.Visible = False
                bt_nessun_danno.Visible = False
                bt_chiudi_chekin.Visible = False

                ' sulla scheda Dettaglio Evento
                bt_nuovo_danno.Visible = False
                ' per gestire furto...
                bt_addebita_furto.Visible = False
                bt_non_addebita_furto.Visible = False
                bt_rientro_veicolo_rubato.Visible = False
                bt_pagamento_da_furto.Visible = False
                bt_salva_furto.Visible = False
                bt_chiudi_senza_furto.Visible = False

                ' sulla scheda Check In 
                bt_salva_checkin.Visible = False

                If mio_evento Is Nothing Then
                    bt_pagamento.Visible = False ' non ci sono danni quindi non visualizzo il pulsante per i pagamenti

                    Visibilita_2(DivVisibile.Div_ReadEventoSenzaDanno)
                Else
                    If id_tipo_documento = tipo_documento.Contratto Then
                        'Tony 24/08/2022
                        'bt_pagamento.Visible = True ' ci sono danni quindi visualizzo il pulsante per i pagamenti
                        bt_pagamento.Visible = False
                        'Tony FINE
                    Else
                        bt_pagamento.Visible = False
                    End If


                    ''Da verificare con l'aggiunta del ck_furto 19.08.2021
                    If mio_evento.data_dichiarazione_furto IsNot Nothing Then
                        If mio_evento.data_ritrovamento_da_furto IsNot Nothing Then
                            ' ancora non gestito.... forse ReadEvento ...
                        Else
                            ' sulla scheda Dettaglio Evento
                            bt_rientro_veicolo_rubato.Visible = True
                            bt_pagamento_da_furto.Visible = True
                            bt_salva_furto.Visible = True
                            bt_chiudi_senza_furto.Visible = True
                            Visibilita_2(DivVisibile.Div_ReadEventoFurto)
                        End If
                    Else
                        Visibilita_2(DivVisibile.Div_ReadEvento)
                    End If
                End If

            End If

            lv_elenco_danni_F.DataBind()
            lv_elenco_danni_R.DataBind()
            ListView_dotazioni.DataBind()
            ListView_meccanici_elettrici.DataBind()

            listViewDocumenti.DataBind()

            gestione_note.InitForm(enum_note_tipo.note_rds, Integer.Parse(lb_id_evento.Text), False)


            'Aggiunto il 19.08.2021
            If getVeicoloRubato(mio_record.id_veicolo) Then
                ck_furto.Enabled = False
                rb_ready_to_go.SelectedValue = 2
                ck_furto.Checked = True
            End If



            'Tony 06/05/2022            
            If GetInNolo(mio_record.id_veicolo) Then
                Try
                    Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()

                    Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 * FROM odl WITH(NOLOCK) WHERE attivo = 1 AND  id_stato_odl <> 9 and id_veicolo = " & mio_record.id_veicolo, Dbc)
                    'Response.Write(Cmd.CommandText & "<br><br>")
                    'Response.End()
                    Dim Rs As Data.SqlClient.SqlDataReader
                    Rs = Cmd.ExecuteReader()
                    If Rs.HasRows Then
                        Do While Rs.Read
                            rb_ready_to_go.SelectedValue = 2
                            ck_fermo_tecnico.Checked = True
                            tx_altro.Text = "Auto in ODL"
                            'Response.Write(mio_record.id_veicolo)

                            rb_ready_to_go.Enabled = False
                            ck_fermo_tecnico.Enabled = False
                            ck_vendita_buy_back.Enabled = False
                            ck_furto.Enabled = False
                            tx_altro.Enabled = False

                            bt_nessun_danno.Visible = False
                            bt_nuovo_evento_danno.Visible = False
                            AutoInOdl.Visible = True

                        Loop
                    Else
                        rb_ready_to_go.Enabled = True
                        ck_fermo_tecnico.Enabled = True
                        ck_vendita_buy_back.Enabled = True
                        ck_furto.Enabled = True
                        tx_altro.Enabled = True

                        bt_nessun_danno.Visible = True
                        bt_nuovo_evento_danno.Visible = True
                        AutoInOdl.Visible = False
                    End If

                    Rs.Close()
                    Dbc.Close()
                    Rs = Nothing
                    Dbc = Nothing

                Catch ex As Exception
                    HttpContext.Current.Response.Write(ex.Message & " Carica Ready to go --- Errore contattare amministratore del sistema.")
                End Try
            Else
                'Response.Write("ELSE")
                rb_ready_to_go.Enabled = True
                ck_fermo_tecnico.Enabled = True
                ck_vendita_buy_back.Enabled = True
                ck_furto.Enabled = True
                tx_altro.Enabled = True

                bt_nessun_danno.Visible = True
                bt_nuovo_evento_danno.Visible = True
                AutoInOdl.Visible = False

                'AzzeraCheckIn()
                AbilitaCheckIn(True)
                AbilitaEvento(True)
            End If

        Catch ex As Exception
            HttpContext.Current.Response.Write("error  InitFormCheckIn: <br/>" & ex.Message & "<br/>" & id_documento & "-" & numero_crv & "<br/>")
        End Try

        'Tony 25/08/2022
        'Permesso Su Pulsante Nuovo Danno
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "165") = 3 Then
            Dim stato_contratto As String = getStatoContratto()
            If stato_contratto = "8" Then
                'gestione_checkin.InitFormCheckIn(tipo_documento.Contratto, Integer.Parse(lblNumContratto.Text), Integer.Parse(numCrv.Text))

                div_nuovo_checkin.Visible = True
                bt_nessun_danno.Visible = False
                bt_chiudi_chekin.Visible = True

                'Sezione Evento
                DropDownTipoEventoAperturaDanno.SelectedValue = 1

                'Sezione Check In
                div_salva_checkin.Visible = False

                'Salva in veicolo_evento_apertura
                SalvaEventoApertura()

            End If

            If Request.Path.ToString = "/trasferimenti.aspx" Then
                tx_km_rientro.Enabled = True
                DropDownSerbatoioRientro.Enabled = True
            End If

        Else
            'Sezione Check In
            If getStatoContratto() = "8" Then
                rb_ready_to_go.Enabled = False
                ck_fermo_tecnico.Enabled = False
                ck_vendita_buy_back.Enabled = False
                ck_furto.Enabled = False
                tx_altro.Enabled = False
                tx_km_rientro.Enabled = False
                DropDownSerbatoioRientro.Enabled = False            
            End If

            If Request.Path.ToString = "/trasferimenti.aspx" Then
                tx_km_rientro.Enabled = True
                DropDownSerbatoioRientro.Enabled = True
            End If
        End If
        'Tony FINE


    End Sub

    Public Sub InitFormNuovoRDSInContratto(ByVal id_tipo_documento As tipo_documento, ByVal id_documento As Integer, ByVal numero_crv As Integer, ByVal id_veicolo As Integer)
        'Response.Write("IN InitFormNuovoRDSInContratto")

        lb_stato_form.Text = stato_form_danni.DaCheckIn

        lb_th_lente.Text = True
        lb_th_da_addebitare.Text = False
        lb_th_lente_storico.Text = False
        bt_pagamento.Visible = False
        tab_mappe.ActiveTabIndex = 0

        lb_id_tipo_documento_apertura.Text = 0
        lb_id_documento_apertura.Text = 0
        lb_num_crv.Text = 0
        lb_num_prenotazione.Text = 0
        lb_id_veicolo.Text = id_veicolo
        lb_stato_danno.Text = 1 ' = aperto 
        lb_id_flag.Text = 1 ' = aperto 

        lb_id_tipo_documento_apertura.Text = id_tipo_documento
        lb_id_documento_apertura.Text = id_documento
        lb_num_crv.Text = numero_crv
        'lb_num_prenotazione.Text = mio_record.num_prenotazione & ""
        lb_id_veicolo.Text = lb_id_veicolo.Text
        lb_stato_danno.Text = 1 ' = aperto 
        lb_id_flag.Text = 1 ' = aperto 

        lb_id_ditta.Text = 0


        ' devo permettere all'utente di selezionare la tipologia del danno
        lb_flag_richiede_id.Text = 0
        DropDownTipoEventoAperturaDanno_DataBind()

        ''Dim mio_veicolo As tabella_veicoli = tabella_veicoli.get_record_da_id(id_veicolo)
        'Dim mio_veicolo As Daticontratto = Nothing
        'InitIntestazione(mio_veicolo)

        'If mio_veicolo.km_uscita IsNot Nothing Then
        '    lb_km_uscita_memo.Text = "(Km uscita " & mio_veicolo.km_uscita & ")"
        '    tx_km_rientro.Text = mio_veicolo.km_rientro

        '    CompareValidatorKmRientro.ValueToCompare = mio_veicolo.km_rientro
        '    CompareValidatorKmRientro.ErrorMessage = "Specificare un valore corretto per i km di rientro presenti al check in, maggiore o uguale dei km di uscita (" & mio_veicolo.km_rientro & " km)"
        'Else
        '    lb_km_uscita_memo.Text = "(Km uscita N.V.)"
        '    CompareValidatorKmRientro.ValueToCompare = 0
        '    CompareValidatorKmRientro.ErrorMessage = "Specificare un valore corretto per i km di rientro presenti al check in, maggiore o uguale dei km di uscita (N.V. km)"
        'End If



        Dim capacita_serbatoio As Integer = getSerbatoioDaIdVeicolo(id_veicolo)
        lb_serbatoio_memo.Text = "(Serbatoio " & capacita_serbatoio & ")"

        For i = 0 To 8
            If DropDownSerbatoioRientro.Items(i).Value <> "-1" And DropDownSerbatoioRientro.Items(i).Value <> "0" Then
                DropDownSerbatoioRientro.Items(i).Text = DropDownSerbatoioRientro.Items(i).Value & "/8 (" & CInt(capacita_serbatoio / 8 * DropDownSerbatoioRientro.Items(i).Value) & ")"
            End If
        Next

        Dim mio_gruppo As veicoli_gruppo_evento = veicoli_gruppo_evento.SalvaDanniApertiAuto(id_veicolo, tipo_documento.Altro, 0, 0, 0)

        Dim mio_evento As veicoli_evento_apertura_danno = New veicoli_evento_apertura_danno

        With mio_evento
            .id_veicolo = id_veicolo
            .attivo = False
            .id_tipo_documento_apertura = 0 ' è il valore che ancora l'utente non ha selezionato sulla combo...
            .id_documento_apertura = 0 ' il numero RDS se viene effettuato il check in
            .num_crv = 0
            .data = Now
            .nota = ""
            .id_ditta = 0

            .id_stazione_apertura = Request.Cookies("SicilyRentCar")("stazione") ' la stazione dell'utente ATTENZIONE: sara anche il "seme" dell'RDS
            .id_gruppo_danni_apertura = mio_gruppo.id

            .SalvaRecord()
        End With
        FillEvento(mio_evento)

        lb_id_evento.Text = mio_evento.id

        listViewElencoDanniPerEvento.DataBind()

        lb_intestazione_evento.Text = "Nuovo Evento"
        lb_evento_modificato.Text = ""

        edit_danno.InitForm(mio_evento.id, , mio_evento.id_gruppo_danni_apertura)

        'AzzeraCheckIn()

        lb_id_gruppo_evento.Text = mio_evento.id_gruppo_danni_apertura

        InitImmagine(id_veicolo, mio_evento.id_gruppo_danni_apertura)

        AbilitaCheckIn(False)
        AbilitaEvento(True)

        ' sulla scheda storico...
        bt_stampa_atto_notorio.Visible = False
        bt_stampa_check_in.Visible = False
        bt_stampa_check_out.Visible = True
        ' bt_furto.Visible = True  ' per adesso disabilito il furto....
        bt_nuovo_evento_danno.Visible = True
        bt_nessun_danno.Visible = False
        bt_chiudi_chekin.Visible = True

        ' sulla scheda Dettaglio Evento
        bt_nuovo_danno.Visible = False
        ' per gestire furto...
        bt_addebita_furto.Visible = False
        bt_non_addebita_furto.Visible = False
        bt_rientro_veicolo_rubato.Visible = False
        bt_pagamento_da_furto.Visible = False
        bt_salva_furto.Visible = False
        bt_chiudi_senza_furto.Visible = False

        ' sulla scheda Check In (ancora non visibile...)
        bt_pagamento.Visible = False
        bt_salva_checkin.Visible = True

        tab_mappe.ActiveTabIndex = 0

        Visibilita_2(DivVisibile.Div_EditEvento)

        lv_elenco_danni_F.DataBind()
        lv_elenco_danni_R.DataBind()
        ListView_dotazioni.DataBind()
        ListView_meccanici_elettrici.DataBind()

        listViewDocumenti.DataBind()

        gestione_note.InitForm(enum_note_tipo.note_rds, Integer.Parse(lb_id_evento.Text), False)



        ''## MODIFICA 05.02.2021 ##
        ''verifica se veicolo noleggioX, trasferimentoX, lavaggioX, ODLX e bisarca
        ''imposta a NO Ready to Go - valore da passare id_veicolo
        'se noleggio
        If GetInNolo(id_veicolo) = True Then
            rb_ready_to_go.SelectedValue = 2        'MODIFICATO 05.02.2021
            rb_ready_to_go.Enabled = False
            lbl_status_veicolo.Text = "(in noleggio)"
        End If

        'se ODL
        If GetInODL(id_veicolo) = True Then
            rb_ready_to_go.SelectedValue = 2        'MODIFICATO 05.02.2021
            rb_ready_to_go.Enabled = False
            lbl_status_veicolo.Text = "(in ODL)"
        End If

        'se tarsferimento
        If GetInTrasferimento(id_veicolo) = True Then
            rb_ready_to_go.SelectedValue = 2        'MODIFICATO 05.02.2021
            rb_ready_to_go.Enabled = False
            lbl_status_veicolo.Text = "(in trasferimento)"
        End If

        'se Lavaggio
        If GetInLavaggio(id_veicolo) = True Then
            rb_ready_to_go.SelectedValue = 2        'MODIFICATO 05.02.2021
            rb_ready_to_go.Enabled = False
            lbl_status_veicolo.Text = "(in lavaggio)"
        End If

        'se Bisarca
        If lbl_status_veicolo.Text = "(in bisarca)" Then lbl_status_veicolo.Text = ""
        If GetInBisarca(id_veicolo) = True Then
            rb_ready_to_go.SelectedValue = 2        'MODIFICATO 05.02.2021
            rb_ready_to_go.Enabled = False
            lbl_status_veicolo.Text = "(in bisarca)"
        Else
            '  lbl_status_veicolo.Text = ""
        End If
        ''##END MODIFICA 05.02.2021

        'Tony 25/08/2022
        'Permesso Su Pulsante Nuovo Danno
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "165") = 3 Then

            'TovaContratto
            Try
                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM contratti,stazioni,veicoli_check_in WITH(NOLOCK) WHERE contratti.id_stazione_rientro = stazioni.id and contratti.num_contratto =  veicoli_check_in.id_documento and (num_contratto = '" & id_documento & "') AND (attivo = 1)", Dbc)

                'Response.Write(Cmd.CommandText & "<br><br>")
                'Response.End()
                Dim Rs As Data.SqlClient.SqlDataReader
                Rs = Cmd.ExecuteReader()
                If Rs.HasRows Then
                    Do While Rs.Read
                        div_num_documento.Visible = True
                        lb_num_documento.Text = Rs("num_contratto")
                        lb_targa.Text = Rs("targa")
                        lb_modello.Text = Rs("modello")
                        lb_stazione.Text = Rs("codice") & " " & Rs("nome_stazione")
                        lb_km.Text = Rs("km_rientro")



                        If Rs("km_uscita") IsNot Nothing Then
                            lb_km_uscita_memo.Text = "(Km uscita " & Rs("km_uscita") & ")"
                            tx_km_rientro.Text = Rs("km_rientro")

                            CompareValidatorKmRientro.ValueToCompare = Rs("km_rientro")
                            CompareValidatorKmRientro.ErrorMessage = "Specificare un valore corretto per i km di rientro presenti al check in, maggiore o uguale dei km di uscita (" & Rs("km_rientro") & " km)"

                            DropDownSerbatoioRientro.SelectedValue = Rs("litri_rientro_frazione")
                        Else
                            lb_km_uscita_memo.Text = "(Km uscita N.V.)"
                            CompareValidatorKmRientro.ValueToCompare = 0
                            CompareValidatorKmRientro.ErrorMessage = "Specificare un valore corretto per i km di rientro presenti al check in, maggiore o uguale dei km di uscita (N.V. km)"
                        End If
                    Loop
                End If
                Rs.Close()
                Dbc.Close()
                Rs = Nothing
                Dbc = Nothing
            Catch ex As Exception
                HttpContext.Current.Response.Write("Trova Valori Contratto/Stazione Rientro  : <br/>" & ex.Message & "<br/>")
            End Try


            Dim stato_contratto As String = getStatoContratto()
            'If stato_contratto = "8" Then
            'gestione_checkin.InitFormCheckIn(tipo_documento.Contratto, Integer.Parse(lblNumContratto.Text), Integer.Parse(numCrv.Text))

            div_nuovo_checkin.Visible = True
            bt_nessun_danno.Visible = False
            bt_nuovo_evento_danno.Visible = True
            bt_chiudi_chekin.Visible = True

            'Sezione Evento
            DropDownTipoEventoAperturaDanno.Items.Clear()
            DropDownTipoEventoAperturaDanno.Items.Add(New ListItem("Seleziona...", "0"))
            DropDownTipoEventoAperturaDanno.Items.Add(New ListItem("Chiusura Contratto", "1"))
            DropDownTipoEventoAperturaDanno.DataBind()
            DropDownTipoEventoAperturaDanno.SelectedValue = 1
            DropDownTipoEventoAperturaDanno.Enabled = False

            'Sezione Dettaglio Danno
            div_edit_danno.Visible = False

            'Sezione Check In            
            rb_ready_to_go.Enabled = True
            ck_fermo_tecnico.Enabled = True
            ck_vendita_buy_back.Enabled = True
            ck_furto.Enabled = True
            tx_altro.Enabled = True            

            'INSERIRE
            'tx_km_rientro.Text = mio_veicolo.km_rientro
            tx_km_rientro.Enabled = False

            DropDownSerbatoioRientro.Enabled = False
            'Response.Write(CInt(mio_veicolo.capacita_serbatoio))
            'Dim valore_sezionare_dropserbatoio As Integer
            'valore_sezionare_dropserbatoio = (8 * CInt(capacita_serbatoio)) / CInt(capacita_serbatoio)
            'DropDownSerbatoioRientro.SelectedValue = valore_sezionare_dropserbatoio

            bt_salva_checkin.Visible = True
            div_salva_checkin.Visible = False

            'Salva in veicolo_evento_apertura  
            lb_id_veicolo.Text = id_veicolo
            SalvaEventoApertura()
            AggiornaRecordApertuaDanno()


            'End If
        Else
            'Sezione Check In
            rb_ready_to_go.Enabled = False
            ck_fermo_tecnico.Enabled = False
            ck_vendita_buy_back.Enabled = False
            ck_furto.Enabled = False
            tx_altro.Enabled = False
            tx_km_rientro.Enabled = False
            DropDownSerbatoioRientro.Enabled = False
        End If
        'Tony FINE

    End Sub

    Protected Function getVeicoloRubato(ByVal id_veicolo As Integer) As Boolean
        Dim sqlStr As String = "select furto from veicoli where id =" & id_veicolo

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getVeicoloRubato = Cmd.ExecuteScalar() & ""
            End Using
        End Using
    End Function

    Public Sub InitFormStatoUsoVeicolo(ByVal id_veicolo As Integer)
        lb_stato_form.Text = stato_form_danni.DaCheckIn

        lb_th_lente.Text = True
        lb_th_da_addebitare.Text = False
        lb_th_lente_storico.Text = False
        bt_pagamento.Visible = False
        tab_mappe.ActiveTabIndex = 0

        lb_id_tipo_documento_apertura.Text = 0
        lb_id_documento_apertura.Text = 0
        lb_num_crv.Text = 0
        lb_num_prenotazione.Text = 0
        lb_id_veicolo.Text = id_veicolo
        lb_stato_danno.Text = 1 ' = aperto 
        lb_id_flag.Text = 1 ' = aperto 

        lb_id_ditta.Text = 0


        ' devo permettere all'utente di selezionare la tipologia del danno
        lb_flag_richiede_id.Text = 0
        DropDownTipoEventoAperturaDanno_DataBind()

        Dim mio_veicolo As tabella_veicoli = tabella_veicoli.get_record_da_id(id_veicolo)
        InitIntestazione(mio_veicolo)

        If mio_veicolo.km_attuali IsNot Nothing Then
            lb_km_uscita_memo.Text = "(Km uscita " & mio_veicolo.km_attuali & ")"
            CompareValidatorKmRientro.ValueToCompare = mio_veicolo.km_attuali
            CompareValidatorKmRientro.ErrorMessage = "Specificare un valore corretto per i km di rientro presenti al check in, maggiore o uguale dei km di uscita (" & mio_veicolo.km_attuali & " km)"
        Else
            lb_km_uscita_memo.Text = "(Km uscita N.V.)"
            CompareValidatorKmRientro.ValueToCompare = 0
            CompareValidatorKmRientro.ErrorMessage = "Specificare un valore corretto per i km di rientro presenti al check in, maggiore o uguale dei km di uscita (N.V. km)"
        End If
        If mio_veicolo.capacita_serbatoio IsNot Nothing Then
            lb_serbatoio_memo.Text = "(Serbatoio " & mio_veicolo.capacita_serbatoio & ")"

            For i = 0 To 8
                If DropDownSerbatoioRientro.Items(i).Value <> "-1" And DropDownSerbatoioRientro.Items(i).Value <> "0" Then
                    DropDownSerbatoioRientro.Items(i).Text = DropDownSerbatoioRientro.Items(i).Value & "/8 (" & CInt(mio_veicolo.capacita_serbatoio / 8 * DropDownSerbatoioRientro.Items(i).Value) & ")"
                End If
            Next
        Else
            lb_serbatoio_memo.Text = "(Serbatoio N.V.)"
        End If

        Dim mio_gruppo As veicoli_gruppo_evento = veicoli_gruppo_evento.SalvaDanniApertiAuto(id_veicolo, tipo_documento.Altro, 0, 0, 0)

        Dim mio_evento As veicoli_evento_apertura_danno = New veicoli_evento_apertura_danno

        With mio_evento
            .id_veicolo = id_veicolo
            .attivo = False
            .id_tipo_documento_apertura = 0 ' è il valore che ancora l'utente non ha selezionato sulla combo...
            .id_documento_apertura = 0 ' il numero RDS se viene effettuato il check in
            .num_crv = 0
            .data = Now
            .nota = ""
            .id_ditta = 0

            .id_stazione_apertura = Request.Cookies("SicilyRentCar")("stazione") ' la stazione dell'utente ATTENZIONE: sara anche il "seme" dell'RDS
            .id_gruppo_danni_apertura = mio_gruppo.id

            .SalvaRecord()
        End With
        FillEvento(mio_evento)

        lb_id_evento.Text = mio_evento.id

        listViewElencoDanniPerEvento.DataBind()

        lb_intestazione_evento.Text = "Nuovo Evento"
        lb_evento_modificato.Text = ""

        edit_danno.InitForm(mio_evento.id, , mio_evento.id_gruppo_danni_apertura)

        AzzeraCheckIn()

        lb_id_gruppo_evento.Text = mio_evento.id_gruppo_danni_apertura

        InitImmagine(id_veicolo, mio_evento.id_gruppo_danni_apertura)

        AbilitaCheckIn(True)
        AbilitaEvento(True)

        ' sulla scheda storico...
        bt_stampa_atto_notorio.Visible = False
        bt_stampa_check_in.Visible = False
        bt_stampa_check_out.Visible = True
        ' bt_furto.Visible = True  ' per adesso disabilito il furto....
        bt_nuovo_evento_danno.Visible = False  'MARCO
        bt_nessun_danno.Visible = False
        bt_chiudi_chekin.Visible = False    'MARCO

        ' sulla scheda Dettaglio Evento
        bt_nuovo_danno.Visible = False 'MARCO
        ' per gestire furto...
        bt_addebita_furto.Visible = False
        bt_non_addebita_furto.Visible = False
        bt_rientro_veicolo_rubato.Visible = False
        bt_pagamento_da_furto.Visible = False
        bt_salva_furto.Visible = False
        bt_chiudi_senza_furto.Visible = False

        ' sulla scheda Check In (ancora non visibile...)
        bt_pagamento.Visible = False
        bt_salva_checkin.Visible = False 'MARCO

        tab_mappe.ActiveTabIndex = 0

        Visibilita_2(DivVisibile.Div_EditEvento)
        div_edit_evento.Visible = False
        div_edit_danno.Visible = False
        div_nota.Visible = False
        tab_dettagli_pagamento.Visible = False
        tab_checkin.Visible = False


        lv_elenco_danni_F.DataBind()
        lv_elenco_danni_R.DataBind()
        ListView_dotazioni.DataBind()
        ListView_meccanici_elettrici.DataBind()

        listViewDocumenti.DataBind()
    End Sub

    Public Sub InitFormNuovoRDSGenerico(ByVal id_veicolo As Integer)
        lb_stato_form.Text = stato_form_danni.DaCheckIn

        lb_th_lente.Text = True
        lb_th_da_addebitare.Text = False
        lb_th_lente_storico.Text = False
        bt_pagamento.Visible = False
        tab_mappe.ActiveTabIndex = 0

        lb_id_tipo_documento_apertura.Text = 0
        lb_id_documento_apertura.Text = 0
        lb_num_crv.Text = 0
        lb_num_prenotazione.Text = 0
        lb_id_veicolo.Text = id_veicolo
        lb_stato_danno.Text = 1 ' = aperto 
        lb_id_flag.Text = 1 ' = aperto 

        lb_id_ditta.Text = 0


        ' devo permettere all'utente di selezionare la tipologia del danno
        lb_flag_richiede_id.Text = 0
        DropDownTipoEventoAperturaDanno_DataBind()

        Dim mio_veicolo As tabella_veicoli = tabella_veicoli.get_record_da_id(id_veicolo)
        InitIntestazione(mio_veicolo)

        If mio_veicolo.km_attuali IsNot Nothing Then
            lb_km_uscita_memo.Text = "(Km uscita " & mio_veicolo.km_attuali & ")"
            CompareValidatorKmRientro.ValueToCompare = mio_veicolo.km_attuali
            CompareValidatorKmRientro.ErrorMessage = "Specificare un valore corretto per i km di rientro presenti al check in, maggiore o uguale dei km di uscita (" & mio_veicolo.km_attuali & " km)"
        Else
            lb_km_uscita_memo.Text = "(Km uscita N.V.)"
            CompareValidatorKmRientro.ValueToCompare = 0
            CompareValidatorKmRientro.ErrorMessage = "Specificare un valore corretto per i km di rientro presenti al check in, maggiore o uguale dei km di uscita (N.V. km)"
        End If
        If mio_veicolo.capacita_serbatoio IsNot Nothing Then
            lb_serbatoio_memo.Text = "(Serbatoio " & mio_veicolo.capacita_serbatoio & ")"

            For i = 0 To 8
                If DropDownSerbatoioRientro.Items(i).Value <> "-1" And DropDownSerbatoioRientro.Items(i).Value <> "0" Then
                    DropDownSerbatoioRientro.Items(i).Text = DropDownSerbatoioRientro.Items(i).Value & "/8 (" & CInt(mio_veicolo.capacita_serbatoio / 8 * DropDownSerbatoioRientro.Items(i).Value) & ")"
                    'Tony 31/10/2022
                Else
                    lb_serbatoio_memo.Text = "(Serbatoio " & mio_veicolo.serbatoio_attuale & ")"
                    'FINE Tony
                End If
            Next
        Else
            lb_serbatoio_memo.Text = "(Serbatoio N.V.)"
        End If

        Dim mio_gruppo As veicoli_gruppo_evento = veicoli_gruppo_evento.SalvaDanniApertiAuto(id_veicolo, tipo_documento.Altro, 0, 0, 0)

        Dim mio_evento As veicoli_evento_apertura_danno = New veicoli_evento_apertura_danno

        With mio_evento
            .id_veicolo = id_veicolo
            .attivo = False
            .id_tipo_documento_apertura = 0 ' è il valore che ancora l'utente non ha selezionato sulla combo...
            .id_documento_apertura = 0 ' il numero RDS se viene effettuato il check in
            .num_crv = 0
            .data = Now
            .nota = ""
            .id_ditta = 0

            .id_stazione_apertura = Request.Cookies("SicilyRentCar")("stazione") ' la stazione dell'utente ATTENZIONE: sara anche il "seme" dell'RDS
            .id_gruppo_danni_apertura = mio_gruppo.id

            .SalvaRecord()
        End With
        FillEvento(mio_evento)

        lb_id_evento.Text = mio_evento.id

        listViewElencoDanniPerEvento.DataBind()

        lb_intestazione_evento.Text = "Nuovo Evento"
        lb_evento_modificato.Text = ""

        edit_danno.InitForm(mio_evento.id, , mio_evento.id_gruppo_danni_apertura)

        AzzeraCheckIn()

        lb_id_gruppo_evento.Text = mio_evento.id_gruppo_danni_apertura

        InitImmagine(id_veicolo, mio_evento.id_gruppo_danni_apertura)

        AbilitaCheckIn(True)
        AbilitaEvento(True)

        ' sulla scheda storico...
        bt_stampa_atto_notorio.Visible = False
        bt_stampa_check_in.Visible = False
        bt_stampa_check_out.Visible = True
        ' bt_furto.Visible = True  ' per adesso disabilito il furto....
        bt_nuovo_evento_danno.Visible = True
        bt_nessun_danno.Visible = False
        bt_chiudi_chekin.Visible = True

        ' sulla scheda Dettaglio Evento
        bt_nuovo_danno.Visible = True
        ' per gestire furto...
        bt_addebita_furto.Visible = False
        bt_non_addebita_furto.Visible = False
        bt_rientro_veicolo_rubato.Visible = False
        bt_pagamento_da_furto.Visible = False
        bt_salva_furto.Visible = False
        bt_chiudi_senza_furto.Visible = False

        ' sulla scheda Check In (ancora non visibile...)
        bt_pagamento.Visible = False
        bt_salva_checkin.Visible = True

        tab_mappe.ActiveTabIndex = 0

        Visibilita_2(DivVisibile.Div_EditEvento)

        lv_elenco_danni_F.DataBind()
        lv_elenco_danni_R.DataBind()
        ListView_dotazioni.DataBind()
        ListView_meccanici_elettrici.DataBind()

        listViewDocumenti.DataBind()

        gestione_note.InitForm(enum_note_tipo.note_rds, Integer.Parse(lb_id_evento.Text), False)



        ''## MODIFICA 05.02.2021 ##
        ''verifica se veicolo noleggioX, trasferimentoX, lavaggioX, ODLX e bisarca
        ''imposta a NO Ready to Go - valore da passare id_veicolo
        'se noleggio
        If GetInNolo(id_veicolo) = True Then
            rb_ready_to_go.SelectedValue = 2        'MODIFICATO 05.02.2021
            rb_ready_to_go.Enabled = False
            lbl_status_veicolo.Text = "(in noleggio)"
        End If

        'se ODL
        If GetInODL(id_veicolo) = True Then
            rb_ready_to_go.SelectedValue = 2        'MODIFICATO 05.02.2021
            rb_ready_to_go.Enabled = False
            lbl_status_veicolo.Text = "(in ODL)"
        End If

        'se tarsferimento
        If GetInTrasferimento(id_veicolo) = True Then
            rb_ready_to_go.SelectedValue = 2        'MODIFICATO 05.02.2021
            rb_ready_to_go.Enabled = False
            lbl_status_veicolo.Text = "(in trasferimento)"
        End If

        'se Lavaggio
        If GetInLavaggio(id_veicolo) = True Then
            rb_ready_to_go.SelectedValue = 2        'MODIFICATO 05.02.2021
            rb_ready_to_go.Enabled = False
            lbl_status_veicolo.Text = "(in lavaggio)"
        End If

        'se Bisarca
        If lbl_status_veicolo.Text = "(in bisarca)" Then lbl_status_veicolo.Text = ""
        If GetInBisarca(id_veicolo) = True Then
            rb_ready_to_go.SelectedValue = 2        'MODIFICATO 05.02.2021
            rb_ready_to_go.Enabled = False
            lbl_status_veicolo.Text = "(in bisarca)"
        Else
            '  lbl_status_veicolo.Text = ""
        End If
        ''##END MODIFICA 05.02.2021

        'Tony 01/11/2022
        'DropDownSerbatoioRientro.Enabled = False
        'DropDownSerbatoioRientro.Visible = False
        ''Response.Write(CInt(mio_veicolo.serbatoio_attuale) / CInt(mio_veicolo.capacita_serbatoio / 8))       
        'DropDownSerbatoioRientro.SelectedValue = CInt(mio_veicolo.serbatoio_attuale) / CInt(mio_veicolo.capacita_serbatoio / 8)

        'Dim mio_veicolo As tabella_veicoli = tabella_veicoli.get_record_da_id(lb_id_veicolo.Text)
        DropDownSerbatoioRientro.Enabled = False
        DropDownSerbatoioRientro.Visible = False
        'Response.Write("Att " & CInt(mio_veicolo.serbatoio_attuale) & "<br>")
        'Response.Write("Cap " & CInt(mio_veicolo.capacita_serbatoio / 8) & "<br>")
        'Response.Write("Diff " & CInt(CInt(mio_veicolo.serbatoio_attuale) / CInt(mio_veicolo.capacita_serbatoio / 8)))
        DropDownSerbatoioRientro.SelectedValue = CInt(CInt(mio_veicolo.serbatoio_attuale) / CInt(mio_veicolo.capacita_serbatoio / 8))
        'FINE Tony
    End Sub

    Private Function GetInBisarca(id_veicolo) As Boolean
        'la verifica dovrebbe essere sull'ultimo movimento
        'non può esssere fatto su tutti perchè potrebbe essere stata in biscarc in altro movimento
        'da verificare 27.08.2021
        'modificato 28.01.2022 per la verifica su ultimo record registrato
        'se contiene bisarca è=true

        Dim sqls As String = "SELECT [id], [riferimento] FROM [Autonoleggio_SRC].[dbo].[trasferimenti] where id_veicolo =" & id_veicolo & " order by id desc;" ' AND riferimento like '%bisarca%';"

        Dim res As Boolean = False

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sqls, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            res = False

            If Rs.HasRows Then
                Rs.Read()
                Dim r As String = ""
                If Not IsDBNull(Rs("riferimento")) Then
                    r = Rs("riferimento")
                    If r.IndexOf("bisarca") > -1 Then
                        res = True
                    End If
                End If

                End If

            Rs.Close()
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing



        Catch ex As Exception

        End Try

        Return res

    End Function


    Private Function GetInLavaggio(id_veicolo) As Boolean

        Dim sqls As String = "SELECT [id] FROM [Autonoleggio_SRC].[dbo].[lavaggi] where id_veicolo=" & id_veicolo & " and data_rientro is null;"
        Dim res As Boolean = False

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sqls, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            res = Rs.HasRows

            Rs.Close()
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing



        Catch ex As Exception

        End Try

        Return res

    End Function
    Private Function GetInTrasferimento(id_veicolo) As Boolean

        Dim sqls As String = "SELECT [id] FROM [Autonoleggio_SRC].[dbo].[trasferimenti] where id_veicolo = " & id_veicolo & " and data_rientro is null;"
        Dim res As Boolean = False

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sqls, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            res = Rs.HasRows

            Rs.Close()
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing



        Catch ex As Exception

        End Try

        Return res

    End Function
    Private Function GetInODL(id_veicolo) As Boolean

        Dim sqls As String = "Select [id] FROM [Autonoleggio_SRC].[dbo].[odl] Where id_veicolo=" & id_veicolo & " And attivo= 1 And data_rientro Is null;"
        Dim res As Boolean = False

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sqls, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            res = Rs.HasRows

            Rs.Close()
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing



        Catch ex As Exception

        End Try

        Return res

    End Function




    Private Function GetInNolo(id_veicolo) As Boolean        
        Dim sqls As String = "Select [ID] From [Autonoleggio_SRC].[dbo].[veicoli] Where noleggiata = 1 And ID = " & id_veicolo & ";"
        'Tony 30-05-2022
        'Dim sqls As String = "Select [ID] From [Autonoleggio_SRC].[dbo].[veicoli] Where disponibile_nolo = 1 And ID = " & id_veicolo & ";"
        Dim res As Boolean = False

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sqls, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            res = Rs.HasRows

            Rs.Close()
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing



        Catch ex As Exception

        End Try

        Return res


    End Function



    Protected Sub InitFormRDSGenerico(ByVal id_evento As Integer)
        lb_stato_form.Text = stato_form_danni.DaCheckIn

        lb_th_lente.Text = True
        lb_th_da_addebitare.Text = False
        lb_th_lente_storico.Text = False
        bt_pagamento.Visible = False
        tab_mappe.ActiveTabIndex = 0

        ' sulla scheda storico...
        bt_stampa_atto_notorio.Visible = False
        bt_stampa_check_in.Visible = True
        bt_stampa_check_out.Visible = True
        bt_furto.Visible = False
        bt_nuovo_evento_danno.Visible = False
        bt_nessun_danno.Visible = False
        bt_chiudi_chekin.Visible = False

        ' sulla scheda Dettaglio Evento
        bt_nuovo_danno.Visible = False
        ' per gestire furto...
        bt_addebita_furto.Visible = False
        bt_non_addebita_furto.Visible = False
        bt_rientro_veicolo_rubato.Visible = False
        bt_pagamento_da_furto.Visible = False
        bt_salva_furto.Visible = False
        bt_chiudi_senza_furto.Visible = False

        lb_intestazione_evento.Text = "Evento"

        ' sulla scheda Check In 
        bt_salva_checkin.Visible = False
        bt_pagamento.Visible = False

        bt_nuovo_danno.Visible = False
        bt_salva_checkin.Visible = False

        AbilitaCheckIn(False)
        AbilitaEvento(False)

        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(id_evento)

        If mio_evento Is Nothing Then
            Libreria.genUserMsgBox(Page, "Errore RDS non trovato")
            Return
        End If

        ' sono nel caso di check in già effettuato (visualizzo solamente le informazioni salvate!)
        With mio_evento
            Dim record_check_in As veicoli_check_in = veicoli_check_in.getRecordDaDocumento(.id_tipo_documento_apertura, .id_rds, 0)
            If record_check_in IsNot Nothing Then
                FillCheckIn(record_check_in)
            Else
                AzzeraCheckIn()
            End If

            lb_flag_richiede_id.Text = 0
            DropDownTipoEventoAperturaDanno_DataBind()
            DropDownTipoEventoAperturaDanno.SelectedValue = .id_tipo_documento_apertura

            lb_id_evento.Text = id_evento
            lb_id_gruppo_evento.Text = .id_gruppo_danni_chiusura

            InitImmagine(.id_veicolo, .id_gruppo_danni_chiusura)

            Dim mio_veicolo As tabella_veicoli = tabella_veicoli.get_record_da_id(.id_veicolo)
            InitIntestazione(mio_veicolo)

            If mio_evento.data_dichiarazione_furto IsNot Nothing Then
                If mio_evento.data_ritrovamento_da_furto IsNot Nothing Then
                    ' ancora non gestito.... forse ReadEvento ...
                Else
                    ' sulla scheda Dettaglio Evento
                    bt_rientro_veicolo_rubato.Visible = True
                    bt_pagamento_da_furto.Visible = True
                    bt_salva_furto.Visible = True
                    bt_chiudi_senza_furto.Visible = True
                    Visibilita_2(DivVisibile.Div_ReadEventoFurto)
                End If
            Else
                Visibilita_2(DivVisibile.Div_ReadEvento)
            End If

        End With

        lv_elenco_danni_F.DataBind()
        lv_elenco_danni_R.DataBind()
        ListView_dotazioni.DataBind()
        ListView_meccanici_elettrici.DataBind()

        listViewDocumenti.DataBind()

        gestione_note.InitForm(enum_note_tipo.note_rds, Integer.Parse(lb_id_evento.Text), False)
    End Sub

    Public Sub InitFormRDS(ByVal id_tipo_documento As tipo_documento, ByVal id_documento As Integer, ByVal numero_crv As Integer)
        'Response.Write("InitFormRDS: " & Integer.Parse(id_tipo_documento) & " - " & id_documento & " - " & numero_crv)

        

        lb_stato_form.Text = stato_form_danni.DaRDS

        lb_th_lente.Text = True
        lb_th_da_addebitare.Text = True
        lb_th_lente_storico.Text = False

        tab_mappe.ActiveTabIndex = 0

        Tab_RDS.ActiveTabIndex = 0

        Dim mio_record As DatiContratto = Nothing ' per  adesso così... in seguito dichiaro una interfaccia!

        
        Select Case id_tipo_documento
            Case tipo_documento.Contratto
                mio_record = DatiContratto.getRecordDaNumContratto(id_documento, numero_crv)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: contratto non trovato.")
                End If

            Case tipo_documento.MovimentoInterno
                mio_record = DatiContratto.getRecordDaNumTrasferimento(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: movimento interno non trovato.")
                End If
            Case tipo_documento.Lavaggio
                mio_record = DatiContratto.getRecordDaLavaggio(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: lavaggio non trovato.")
                End If
            Case tipo_documento.ODL
                mio_record = DatiContratto.getRecordDaNumODL(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                End If

            Case tipo_documento.DuranteODL
                mio_record = DatiContratto.getRecordDaNumDuranteODL(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                End If
            Case Is >= tipo_documento.RDSGenerico
                mio_record = DatiContratto.getRecordDaRDSGenerico(id_tipo_documento, id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: RDS Generico non trovato.")
                End If

            Case Else
                Err.Raise(1001, Nothing, "Tipo documento non previsto")
        End Select


        ViewState("DocumentoAssociato") = mio_record

        lb_id_tipo_documento_apertura.Text = id_tipo_documento
        lb_id_documento_apertura.Text = id_documento
        lb_num_crv.Text = numero_crv
        lb_num_prenotazione.Text = mio_record.num_prenotazione & ""
        lb_id_veicolo.Text = mio_record.id_veicolo
        lb_stato_danno.Text = 1 ' = aperto 
        lb_id_flag.Text = 1 ' = aperto 

        lb_id_ditta.Text = mio_record.id_cliente & ""

        Dim mio_tipo As veicoli_tipo_documento_apertura_danno = veicoli_tipo_documento_apertura_danno.get_record_da_id(id_tipo_documento)
        If mio_tipo.richiede_id Is Nothing OrElse mio_tipo.richiede_id = False Then
            lb_flag_richiede_id.Text = 0
        Else
            lb_flag_richiede_id.Text = 1
        End If

        DropDownTipoEventoAperturaDanno_DataBind()
        DropDownTipoEventoAperturaDanno.SelectedValue = Integer.Parse(id_tipo_documento)

        'Response.Write("OKK5")
        'Response.End()

        Dim record_check_in As veicoli_check_in = veicoli_check_in.getRecordDaDocumento(id_tipo_documento, id_documento, numero_crv)
        If record_check_in IsNot Nothing Then
            FillCheckIn(record_check_in)
        Else
            AzzeraCheckIn()
        End If

        lb_km_uscita_memo.Text = "(Km uscita " & mio_record.km_uscita & ")"

        Dim cap_serbatoio As Integer = getSerbatoioDaIdVeicolo(mio_record.id_veicolo)

        lb_serbatoio_memo.Text = "(Serbatoio " & cap_serbatoio & ")"

        For i = 0 To 8
            If DropDownSerbatoioRientro.Items(i).Value <> "-1" And DropDownSerbatoioRientro.Items(i).Value <> "0" Then
                DropDownSerbatoioRientro.Items(i).Text = DropDownSerbatoioRientro.Items(i).Value & "/8 (" & CInt(cap_serbatoio / 8 * DropDownSerbatoioRientro.Items(i).Value) & ")"
            End If
        Next


        lb_intestazione_evento.Text = "Evento"
        lb_id_gruppo_evento.Text = mio_record.id_gruppo_danni_rientro

        Dim mio_gruppo_evento As veicoli_gruppo_evento = veicoli_gruppo_evento.getRecordDaId(mio_record.id_gruppo_danni_rientro)

        lb_id_evento.Text = mio_gruppo_evento.id_evento

        ' mio_evento non può essere null... dato che siamo negli RDS quindi almeno un danno c'è!!!
        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(mio_gruppo_evento.id_evento)

        FillRDS(mio_evento) ' lb_num_rds lo valorizzo qui...

        tab_dettagli_pagamento.Visible = True
        DettagliPagamento.InitForm(lb_num_prenotazione.Text, lb_id_documento_apertura.Text, lb_num_rds.Text, )

        InitImmagine(mio_record.id_veicolo, mio_record.id_gruppo_danni_rientro)

        InitIntestazione(mio_record)


        AbilitaCheckIn(False)
        AbilitaEvento(False)

        ' sulla scheda storico...
        bt_stampa_atto_notorio.Visible = False
        bt_stampa_check_in.Visible = False
        bt_stampa_check_out.Visible = False
        bt_furto.Visible = False
        bt_nuovo_evento_danno.Visible = False
        bt_nessun_danno.Visible = False
        bt_chiudi_chekin.Visible = False

        ' sulla scheda Dettaglio Evento
        bt_nuovo_danno.Visible = False
        ' per gestire furto...
        bt_addebita_furto.Visible = False
        bt_non_addebita_furto.Visible = False
        bt_rientro_veicolo_rubato.Visible = False
        bt_pagamento_da_furto.Visible = False
        bt_salva_furto.Visible = False
        bt_chiudi_senza_furto.Visible = False

        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneRDS) <> "3" Then
            bt_salva_rds.Visible = False
        Else
            bt_salva_rds.Visible = True
        End If

        '' sulla scheda Check In 
        'bt_pagamento.Visible = False
        'bt_salva_checkin.Visible = False
        'bt_chiudi.Visible = False


        If mio_evento Is Nothing Then
            Visibilita_2(DivVisibile.Div_GestioneRDS) ' ok non dovrebbe esserci mai...
        Else
            If mio_evento.data_dichiarazione_furto IsNot Nothing Then
                If mio_evento.data_ritrovamento_da_furto IsNot Nothing Then
                    ' ancora non gestito.... forse ReadEvento ...
                Else
                    lb_th_lente.Visible = False

                    bt_addebita_furto.Visible = True
                    bt_non_addebita_furto.Visible = True

                    Visibilita_2(DivVisibile.Div_GestioneRDS)
                End If
            Else
                Visibilita_2(DivVisibile.Div_GestioneRDS)
            End If
        End If

        lv_elenco_danni_F.DataBind()
        lv_elenco_danni_R.DataBind()
        ListView_dotazioni.DataBind()
        ListView_meccanici_elettrici.DataBind()

        listViewElencoDanniPerEvento.DataBind()
        listViewDocumenti.DataBind()

        ' visibilità o meno della sezione sinistri.
        If mio_evento.numero_sinistro Is Nothing Then
            div_gestione_sinistro.Visible = False
            div_nuovo_sinistro.Visible = True
        Else
            FillSinistro(mio_evento)
            div_gestione_sinistro.Visible = True
            div_nuovo_sinistro.Visible = False
        End If

        gestione_note.InitForm(enum_note_tipo.note_rds, Integer.Parse(lb_id_evento.Text), False)

        'Response.Write("OKKK")
        'Response.End()
        'Tony 30-09-2022
        'RDS (Nuovo-Modifiche-Eliminazione)
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "168") = 3 Then
            div_elenco_danni.Visible = True
            div_nuovo_checkin.Visible = True
            bt_nuovo_evento_danno.Visible = True
            'Response.Write("OK7")

            div_nuovo_checkin.Visible = True
            bt_nessun_danno.Visible = False
            bt_chiudi_chekin.Visible = True

            'Sezione Evento
            'DropDownTipoEventoAperturaDanno.SelectedValue = 1

            'Sezione Check In
            div_salva_checkin.Visible = False

            'Salva in veicolo_evento_apertura
            SalvaEventoApertura()
        End If

    End Sub

    Public Sub InitFormCheckOutRDS(ByVal id_tipo_documento As tipo_documento, ByVal id_documento As Integer, ByVal numero_crv As Integer)        
        
        Trace.Write("InitFormCheckOutRDS: " & id_tipo_documento.ToString & " - " & id_documento & " - " & numero_crv)

        lb_stato_form.Text = stato_form_danni.DaCheckOutRDS

        tab_mappe.ActiveTabIndex = 0

        lb_th_lente.Text = False
        lb_th_da_addebitare.Text = False
        lb_th_lente_storico.Text = True

        Dim mio_record As DatiContratto = Nothing ' per  adesso così... in seguito dichiaro una interfaccia!
        Dim mio_gruppo_evento As veicoli_gruppo_evento = Nothing

        Select Case id_tipo_documento
            Case tipo_documento.Contratto
                mio_record = DatiContratto.getRecordDaNumContratto(id_documento, numero_crv)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: contratto non trovato.")
                End If

            Case tipo_documento.MovimentoInterno
                mio_record = DatiContratto.getRecordDaNumTrasferimento(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore:  movimento interno non trovato.")
                End If
            Case tipo_documento.Lavaggio
                mio_record = DatiContratto.getRecordDaLavaggio(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore:  lavaggio non trovato.")
                End If
            Case tipo_documento.ODL
                mio_record = DatiContratto.getRecordDaNumODL(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                End If

            Case tipo_documento.DuranteODL
                mio_record = DatiContratto.getRecordDaNumDuranteODL(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                End If
            Case Is >= tipo_documento.RDSGenerico
                mio_record = DatiContratto.getRecordDaRDSGenerico(id_tipo_documento, id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: RDS Generico non trovato.")
                End If

            Case Else
                Err.Raise(1001, Nothing, "Tipo documento non previsto")
        End Select

        Trace.Write("-----------------------------------------------------------------------------")
        Trace.Write("InitFormCheckOutRDS " & id_tipo_documento & " - " & id_documento & " - " & numero_crv & " - " & mio_record.num_prenotazione)
        Trace.Write("-----------------------------------------------------------------------------")

        ViewState("DocumentoAssociato") = mio_record

        If mio_record.id_gruppo_danni_uscita IsNot Nothing Then
            mio_gruppo_evento = veicoli_gruppo_evento.getRecordDaId(mio_record.id_gruppo_danni_uscita)
            'VISTO CHE POTREBBERO ESSERE CAMBIATI GLI ACCESSORI ACQUISTATI A NOLO IN CORSO, AGGIORNO LA RELATIVA LISTA
            ListView_accessori.DataBind()
        End If

        lb_id_gruppo_evento.Text = mio_gruppo_evento.id

        lb_id_tipo_documento_apertura.Text = id_tipo_documento
        lb_id_documento_apertura.Text = id_documento
        lb_num_crv.Text = numero_crv
        lb_num_prenotazione.Text = mio_record.num_prenotazione & ""
        lb_id_veicolo.Text = mio_record.id_veicolo
        lb_stato_danno.Text = 1 ' = aperto 
        lb_id_flag.Text = 1 ' = aperto 

        lb_id_ditta.Text = mio_record.id_cliente & ""

        Dim mio_tipo As veicoli_tipo_documento_apertura_danno = veicoli_tipo_documento_apertura_danno.get_record_da_id(id_tipo_documento)
        If mio_tipo.richiede_id Is Nothing OrElse mio_tipo.richiede_id = False Then
            lb_flag_richiede_id.Text = 0
        Else
            lb_flag_richiede_id.Text = 1
        End If
        DropDownTipoEventoAperturaDanno_DataBind()

        InitImmagine(mio_record.id_veicolo, mio_gruppo_evento.id)

        InitIntestazione(mio_record)

        lv_elenco_danni_F.DataBind()
        lv_elenco_danni_R.DataBind()
        ListView_dotazioni.DataBind()
        ListView_meccanici_elettrici.DataBind()

        AbilitaCheckIn(True)
        AbilitaEvento(False)

        ' sulla scheda storico...
        bt_stampa_atto_notorio.Visible = False
        bt_stampa_check_in.Visible = False
        bt_stampa_check_out.Visible = False
        bt_furto.Visible = False
        bt_nuovo_evento_danno.Visible = False
        bt_nessun_danno.Visible = False
        bt_chiudi_chekin.Visible = False

        '' sulla scheda Dettaglio Evento
        'bt_nuovo_danno.Visible = False
        '' per gestire furto...
        'bt_addebita_furto.Visible = False
        'bt_non_addebita_furto.Visible = False
        'bt_rientro_veicolo_rubato.Visible = False
        'bt_pagamento_da_furto.Visible = False
        'bt_salva_furto.Visible = False
        'bt_chiudi_senza_furto.Visible = False

        ' sulla scheda Check In (ancora non visibile...)
        bt_pagamento.Visible = False
        bt_salva_checkin.Visible = False
        bt_chiudi_chekin.Visible = False

        Visibilita_2(DivVisibile.Div_CheckOutRDS)

        gestione_note.InitForm(enum_note_tipo.note_rds, Integer.Parse(lb_id_evento.Text), False)

        
        'Response.End()
    End Sub

    Public Sub InitForm(ByVal mio_record As veicoli_evento_apertura_danno)
        lb_stato_form.Text = 1

        lb_flag_richiede_id.Text = 0

        DropDownTipoEventoAperturaDanno_DataBind()

        lb_id_veicolo.Text = mio_record.id_veicolo

        lb_id_ditta.Text = mio_record.id_ditta & ""

        FillEvento(mio_record)

        Visibilita_2(DivVisibile.Div_EditEvento)
    End Sub

    Protected Sub FillCheckIn(ByVal record_check_in As veicoli_check_in)
        With record_check_in
            rb_ready_to_go.ClearSelection()
            ck_lavare.Checked = False
            ck_rifornire.Checked = False
            ck_vendita_buy_back.Checked = False
            tx_altro.Text = ""
            tx_km_rientro.Text = .km_rientro & ""
            If .litri_rientro_frazione IsNot Nothing Then
                DropDownSerbatoioRientro.SelectedValue = .litri_rientro_frazione
            Else
                DropDownSerbatoioRientro.SelectedValue = 0
            End If


            If .data_ready_to_go IsNot Nothing Then
                rb_ready_to_go.SelectedValue = 1
            Else
                If .data_lavaggio IsNot Nothing Then
                    ck_lavare.Checked = True
                    rb_ready_to_go.SelectedValue = 2
                End If

                If .data_rifornimento IsNot Nothing Then
                    ck_rifornire.Checked = True
                    rb_ready_to_go.SelectedValue = 2
                End If

                If .fermo_tecnico Is Nothing OrElse Not .fermo_tecnico Then
                    ck_fermo_tecnico.Checked = False
                Else
                    ck_fermo_tecnico.Checked = True
                    rb_ready_to_go.SelectedValue = 2
                End If

                If .in_vendita_buy_back Is Nothing OrElse Not .in_vendita_buy_back Then
                    ck_vendita_buy_back.Checked = False
                Else
                    ck_vendita_buy_back.Checked = True
                    rb_ready_to_go.SelectedValue = 2
                End If

                tx_altro.Text = .altro & ""
                If tx_altro.Text <> "" Then
                    rb_ready_to_go.SelectedValue = 2
                End If

                If ck_furto.Checked = True Then         'modificato il 19.08.2021
                    rb_ready_to_go.SelectedValue = 2
                    ck_furto.Enabled = False
                End If


            End If

        End With
    End Sub

    Protected Sub AzzeraCheckIn()
        Dim record_check_in As veicoli_check_in = New veicoli_check_in
        With record_check_in
            .data_ready_to_go = Nothing
            .data_lavaggio = Nothing
            .data_rifornimento = Nothing
            .fermo_tecnico = Nothing
            .in_vendita_buy_back = Nothing
            .altro = Nothing
            .km_rientro = Nothing
            .litri_rientro_frazione = -1
        End With
        FillCheckIn(record_check_in)
        'rb_ready_to_go.ClearSelection()
        'ck_lavare.Checked = False
        'ck_rifornire.Checked = False
        'tx_km_rientro.Text = ""
        'DropDownSerbatoioRientro.SelectedValue = -1
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DropOperazione.Attributes.Add("onChange", "return OnSelectedIndexChange(this);")
        Drop_aliquote_iva.Attributes.Add("onChange", "return OnChangeIVA(this);")

        AddHandler edit_danno.AggiornaElenco, AddressOf EditDanno_AggiornaElenco

        If Not Page.IsPostBack Then
            lb_id_stato_rds.Text = 0

            lb_immagine_default.Text = Costanti.id_mappa_default

            DropDownTipoEventoAperturaDanno_DataBind()
            DropDownNonAddebito.DataBind()
            If Drop_aliquote_iva.Items.Count <= 1 Then
                Drop_aliquote_iva.DataBind()
            End If

            Drop_manutenzione.DataBind()
            Drop_documenti.DataBind()

            'Visibilita_2(DivVisibile.Ricerca)

            tab_mappe.ActiveTabIndex = 0

        Else
            sqlElencoAuto.SelectCommand = lb_sqlElencoAuto.Text

            If lb_id_veicolo.Text <> "" And lb_id_gruppo_evento.Text <> "" Then
                InitImmagine(Integer.Parse(lb_id_veicolo.Text), Integer.Parse(lb_id_gruppo_evento.Text))
            End If
        End If

        If lb_stato_form.Text = stato_form_danni.DaRDS Then
            Dim body As HtmlGenericControl = Page.Master.FindControl("myBody")
            body.Attributes.Add("onLoad", "InitDivVisibile(" & DropOperazione.SelectedValue & ");")
            Trace.Write("Page_Load --------------------------------- myBody.InitDivVisibile(" & DropOperazione.SelectedValue & ");")
        End If

        Trace.Write("Danni TONY" & sqlDanniMappati_F.SelectCommand)
    End Sub

    Protected Sub bt_cerca_targa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_cerca_targa.Click

        CercaVeicolo(tx_targa.Text, Integer.Parse(DropDownList_stato_danno.SelectedValue))

    End Sub

    Protected Sub bt_cerca_veicoli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_cerca_veicoli.Click

        lb_sqlElencoAuto.Text = getSqlAuto(getFiltroAutoDanneggiate())

        sqlElencoAuto.SelectCommand = lb_sqlElencoAuto.Text

        Trace.Write("bt_cerca_veicoli_Click: " & sqlElencoAuto.SelectCommand)

        listViewElencoAuto.DataBind()

    End Sub


    Protected Function getFiltroAutoDanneggiate() As String
        getFiltroAutoDanneggiate = ""

        If DropDownStazioni.SelectedValue <> "0" Then
            getFiltroAutoDanneggiate += " AND s.id = " & DropDownStazioni.SelectedValue
        End If

        If tx_DataDa.Text <> "" Then
            getFiltroAutoDanneggiate += " AND d.data_creazione >= '" & Libreria.FormattaData(tx_DataDa.Text) & "'"
        End If

        If tx_DataA.Text <> "" Then
            Dim mia_data As Date = Libreria.getDateDaStr(tx_DataA.Text)
            mia_data = DateAdd(DateInterval.Day, 1.0, mia_data)
            getFiltroAutoDanneggiate += " AND d.data_creazione < '" & Libreria.FormattaData(mia_data) & "'"
        End If

        If DropDown_stato_danno.SelectedValue <> "0" Then
            getFiltroAutoDanneggiate += " AND d.stato = " & DropDown_stato_danno.SelectedValue
        End If

    End Function

    Protected Function getSqlAuto(ByVal filtro As String) As String
        Dim sqlStr As String

        sqlStr = "SELECT COUNT(*) num, d.id_veicolo, v.id_stazione, s.nome_stazione," &
            " v.targa, m.descrizione modello" &
            " FROM veicoli_danni d WITH(NOLOCK)" &
            " INNER JOIN veicoli v WITH(NOLOCK) ON d.id_veicolo = v.id" &
            " LEFT JOIN stazioni s WITH(NOLOCK) ON s.id = v.id_stazione" &
            " LEFT JOIN MODELLI m WITH(NOLOCK) ON m.ID_MODELLO = v.id_modello" &
            " WHERE attivo = 1" &
            filtro &
            " GROUP BY v.id_stazione, s.nome_stazione, d.id_veicolo, v.targa, m.descrizione " &
            " ORDER BY s.nome_stazione, v.targa"

        Return sqlStr
    End Function

    Protected Sub listViewElencoAuto_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listViewElencoAuto.ItemCommand
        If e.CommandName = "lente" Then
            Dim lb_targa As Label = e.Item.FindControl("lb_targa")

            CercaVeicolo(lb_targa.Text, Integer.Parse(DropDown_stato_danno.SelectedValue))
        End If
    End Sub

    ' CERCA DA TARGA -----------------------------------------------------------------------
    Protected Sub CercaVeicolo(ByVal Targa As String, ByVal stato_danno As Integer)
        Dim id_veicolo As String = getIdAutoDaTarga(Targa)
        If id_veicolo = "" Then
            Libreria.genUserMsgBox(Page, "Veicolo non trovato")
            Return
        End If

        ' imposto i filtri per la query di selezione
        lb_id_veicolo.Text = id_veicolo        
        lb_stato_danno.Text = stato_danno
        lb_id_flag.Text = stato_danno

        ' InitImmagine(id_veicolo)

        InitIntestazione(id_veicolo, Targa)

        Dim mio_evento As veicoli_evento_apertura_danno = New veicoli_evento_apertura_danno
        With mio_evento
            .id_veicolo = id_veicolo
            .attivo = False
            .id_tipo_documento_apertura = 0
            .id_documento_apertura = 0
            .data = Now
            .nota = ""
            .id_ditta = Integer.Parse(lb_id_ditta.Text)

            lb_id_evento.Text = .SalvaRecord
        End With

        FillEvento(mio_evento)

        edit_danno.InitForm(Integer.Parse(lb_id_evento.Text))

        Visibilita_2(DivVisibile.Div_EditEvento)
    End Sub

    Protected Sub InitIntestazione(ByVal mio_veicolo As tabella_veicoli)
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
            lb_km.Text = .km_attuali & ""
            'Tony  26/05/2022          
            IDdelVeicolo.Text = getIdAutoDaTarga(.targa)
        End With

        lb_num_documento.Text = ""

    End Sub

    Protected Sub InitIntestazione(ByVal mio_record As DatiContratto)
        With mio_record
            lb_targa.Text = .targa
            lb_modello.Text = getModelloAutoDaId(.id_veicolo)
            If lb_modello.Text = "" Then
                lb_modello.Text = "(N.V.)"
            End If
            lb_stazione.Text = getStazioneAutoDaId(.id_veicolo)
            If lb_stazione.Text = "" Then
                lb_stazione.Text = "(N.V.)"
            End If
            If lb_stato_form.Text = stato_form_danni.DaCheckOut Then
                lb_km.Text = .km_uscita
            Else
                If .km_rientro Is Nothing Then
                    lb_km.Text = .km_uscita
                Else
                    lb_km.Text = .km_rientro
                End If

            End If

            lb_num_documento.Text = .num_contratto
        End With
    End Sub

    Protected Sub InitIntestazione(ByVal id_veicolo As Integer, Optional ByVal Targa As String = "", Optional ByVal id_tipo_documento As tipo_documento = tipo_documento.Nessuno, Optional ByVal num_documento As Integer = 0)
        If Targa = "" Then
            Targa = getTargaDaId(id_veicolo)
        End If
        lb_targa.Text = Targa
        lb_modello.Text = getModelloAutoDaId(id_veicolo)
        If lb_modello.Text = "" Then
            lb_modello.Text = "(N.V.)"
        End If

        lb_km.Text = getKmDaIdVeicolo(id_veicolo)

        Select Case id_tipo_documento
            Case tipo_documento.Nessuno
                ' Non faccio niente...
            Case tipo_documento.Contratto ' aggiungere qui gli altri...
                lb_num_documento.Text = num_documento
            Case Else
                Err.Raise(1001, Nothing, "Tipo Documento Apertura Danno non previsto")
        End Select
    End Sub

    Protected Function getTargaDaId(ByVal id_veicolo As Integer) As String
        Dim sqlStr As String
        Try

            sqlStr = "SELECT targa FROM veicoli WITH(NOLOCK) where id = " & id_veicolo
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    getTargaDaId = Cmd.ExecuteScalar & ""
                End Using
            End Using
        Catch ex As Exception
            Response.Write("error getTargaDaId :" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Private Function getIdAutoDaTarga(ByVal targa As String) As String
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
            Response.Write("error getIdAutoDaTarga :" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Private Function getKmDaIdVeicolo(ByVal id_veicolo As Integer) As String
        Dim sqlStr As String
        Try
            sqlStr = "SELECT km_attuali FROM veicoli WITH(NOLOCK) where id = " & id_veicolo
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    getKmDaIdVeicolo = Cmd.ExecuteScalar & ""
                End Using
            End Using
        Catch ex As Exception
            Response.Write("error getKmDaIdVeicolo :" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Private Function getModelloAutoDaId(ByVal id_vaicolo As Integer) As String
        Dim sqlStr As String
        Try
            sqlStr = "SELECT m.descrizione AS modello" &
                       " FROM veicoli v WITH(NOLOCK)" &
                       " INNER JOIN modelli m WITH(NOLOCK) ON v.id_modello = m.id_modello" &
                       " WHERE v.id = '" & id_vaicolo & "'"
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    getModelloAutoDaId = Cmd.ExecuteScalar & ""
                End Using
            End Using
        Catch ex As Exception
            Response.Write("error getModelloAutoDaId :" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Private Function getStazioneAutoDaId(ByVal id_vaicolo As Integer) As String
        Dim sqlStr As String
        Try
            sqlStr = "SELECT  (s.codice + ' - '+ s.nome_stazione) AS Stazione" &
                      " FROM veicoli v WITH(NOLOCK)" &
                      " INNER JOIN stazioni s WITH(NOLOCK) ON v.id_stazione = s.id" &
                      " WHERE v.id = '" & id_vaicolo & "'"

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    getStazioneAutoDaId = Cmd.ExecuteScalar & ""
                End Using
            End Using
        Catch ex As Exception
            Response.Write("error getStazioneAutoDaId :" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Private Function getSerbatoioDaIdVeicolo(ByVal id_veicolo As Integer) As Integer
        Dim sqlStr As String
        Try
            sqlStr = "SELECT m.capacita_serbatoio FROM veicoli v WITH(NOLOCK)" &
                        " INNER JOIN MODELLI m WITH(NOLOCK) ON v.id_modello = m.ID_MODELLO" &
                        " WHERE v.id = " & id_veicolo

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    If Cmd.ExecuteScalar Is DBNull.Value Then
                        getSerbatoioDaIdVeicolo = 0
                    Else
                        getSerbatoioDaIdVeicolo = Cmd.ExecuteScalar
                    End If
                End Using
            End Using
        Catch ex As Exception
            Response.Write("error getSerbatoioDaIdVeicolo :" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Private Function nolo_in_corso(ByVal id_veicolo As Integer) As Boolean
        Dim sqlStr As String
        Try
            sqlStr = "SELECT noleggiata FROM veicoli WHERE veicoli.id = " & id_veicolo

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    If Cmd.ExecuteScalar Is DBNull.Value Then
                        nolo_in_corso = False
                    Else
                        nolo_in_corso = Cmd.ExecuteScalar
                    End If
                End Using
            End Using
        Catch ex As Exception
            Response.Write("error nolo_in_corso :" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function
    ' FINE CERCA DA TARGA -----------------------------------------------------------------------

    Protected Sub AbilitaRDS(ByVal Valore As Boolean)
        tx_stima_rds.Enabled = Valore
        tx_data_incidente.Enabled = Valore
        tx_luogo_incidente.Enabled = Valore
        Drop_aliquote_iva.Enabled = Valore
        tx_spese_postali.Enabled = Valore
        tx_data_perizia.Enabled = Valore
        ck_perizia.Enabled = Valore
        tx_giorni_fermo_tecnico.Enabled = Valore
        ck_CID.Enabled = Valore
        ck_denuncia.Enabled = Valore
        ck_fotocopia_doc.Enabled = Valore
        ck_preventivo.Enabled = Valore
        tx_num_fotografie.Enabled = Valore
    End Sub

    Protected Function getValoreCheck(ByVal Valore As Boolean?) As Boolean
        If Valore Is Nothing Then
            Return False
        End If
        Return Valore
    End Function

    Protected Function getMatriceIVA() As String
        Dim sqlStr As String = "SELECT id, aliquota FROM aliquote_iva"
        Try
            Dim matrice As String = ""
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Using rs = Cmd.ExecuteReader
                        Do While rs.Read
                            If matrice = "" Then
                                matrice = "[" & rs("id") & "," & rs("aliquota") & "]"
                            Else
                                matrice += ",[" & rs("id") & "," & rs("aliquota") & "]"
                            End If
                        Loop
                    End Using
                End Using
            End Using

            ' Trace.Write(sqlStr & " - " & matrice)

            Return "[" & matrice & "]"
        Catch ex As Exception
            Response.Write("error getMatriceIVA :" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try


    End Function

    Protected Function StrStatoRDS(ByVal mio_stato_rds As sessione_danni.stato_rds) As String
        Select Case mio_stato_rds
            Case sessione_danni.stato_rds.Da_lavorare
                Return "Da lavorare"
            Case sessione_danni.stato_rds.Chiuso
                Return "Chiuso"
            Case sessione_danni.stato_rds.In_attesa
                Return "In attesa"
            Case sessione_danni.stato_rds.All_attenzione
                Return "All'attenzione"
            Case sessione_danni.stato_rds.Da_addebitare
                Return "Da addebitare"
            Case sessione_danni.stato_rds.Da_fatturare
                Return "Da fatturare"
            Case sessione_danni.stato_rds.Fatturato
                Return "Fatturato"
            Case Else
                Return "Stato non definito"
        End Select
    End Function

    Private Sub FillRDS(ByVal mio_record As veicoli_evento_apertura_danno)

        Try
            With mio_record
                'lb_id_evento.Text = .id
                'DropDownTipoEventoAperturaDanno.SelectedValue = .id_tipo_documento_apertura
                'lb_id_documento_apertura.Text = .id_documento_apertura
                'lb_num_crv.Text = .num_crv

                tx_data_evento.Text = .data & ""
                tx_nota_evento.Text = .nota & ""
                If .id_non_addebito Is Nothing Then
                    DropDownNonAddebito.SelectedValue = 0
                Else
                    DropDownNonAddebito.SelectedValue = .id_non_addebito
                End If


                ' sezione RDS----------------------------------------
                AbilitaRDS(True)
                bt_stampa_RDS.Visible = True
                bt_stampa_lettera.Visible = True
                bt_salva_rds.Visible = True
                DropOperazione.Enabled = True
                DropOperazione.Visible = True
                lb_operazione.Visible = False
                lb_per_operazione.Visible = True
                div_pulsanti_sinistro.Visible = True

                bt_salva_rds.Text = "Salva"

                Dim mio_stato_rds As sessione_danni.stato_rds = .stato_rds
                lb_stato_rds.Text = StrStatoRDS(mio_stato_rds)
                lb_id_stato_rds.Text = mio_stato_rds
                lb_num_rds.Text = .id_rds & ""

                Select Case mio_stato_rds
                    Case sessione_danni.stato_rds.Da_lavorare
                        Dim body As HtmlGenericControl = Page.Master.FindControl("myBody")
                        body.Attributes.Add("onLoad", "InitDivVisibile(1);")


                        bt_stampa_lettera.Visible = False
                        div_pulsanti_sinistro.Visible = False
                    Case sessione_danni.stato_rds.Chiuso ' adesso è diventato "Da non addebitare"
                        Dim body As HtmlGenericControl = Page.Master.FindControl("myBody")
                        body.Attributes.Add("onLoad", "InitDivVisibile(2);")

                        AbilitaRDS(False)
                        DropOperazione.SelectedValue = 2
                        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.ModificaStatoChiusoRDS) <> "3" Then
                            DropOperazione.Enabled = False
                        End If
                        bt_stampa_lettera.Visible = False
                        bt_salva_rds.Visible = False
                        div_pulsanti_sinistro.Visible = True
                    Case sessione_danni.stato_rds.In_attesa
                        Dim body As HtmlGenericControl = Page.Master.FindControl("myBody")
                        body.Attributes.Add("onLoad", "InitDivVisibile(3);")

                        bt_stampa_lettera.Visible = False
                        DropOperazione.SelectedValue = 3
                    Case sessione_danni.stato_rds.All_attenzione
                        Dim body As HtmlGenericControl = Page.Master.FindControl("myBody")
                        body.Attributes.Add("onLoad", "InitDivVisibile(1);")

                        DropOperazione.SelectedValue = 4

                        bt_stampa_lettera.Visible = False
                    Case sessione_danni.stato_rds.Da_addebitare
                        Dim body As HtmlGenericControl = Page.Master.FindControl("myBody")
                        body.Attributes.Add("onLoad", "InitDivVisibile(1);")

                        DropOperazione.SelectedValue = 5
                        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.ModificaStatoChiusoRDS) <> "3" Then
                            DropOperazione.Enabled = False
                        End If

                        bt_salva_rds.Text = "Modifica Importo"
                    Case sessione_danni.stato_rds.Da_fatturare
                        Dim body As HtmlGenericControl = Page.Master.FindControl("myBody")
                        body.Attributes.Add("onLoad", "InitDivVisibile(1);")

                        AbilitaRDS(False)
                        DropOperazione.Visible = False
                        bt_salva_rds.Visible = False
                        lb_operazione.Visible = True
                        lb_operazione.Text = "Da fatturare"
                    Case sessione_danni.stato_rds.Fatturato
                        Dim body As HtmlGenericControl = Page.Master.FindControl("myBody")
                        body.Attributes.Add("onLoad", "InitDivVisibile(1);")

                        AbilitaRDS(False)
                        DropOperazione.Visible = False
                        lb_per_operazione.Visible = False
                        lb_operazione.Visible = True
                        lb_operazione.Text = "Fatturato"
                        bt_salva_rds.Visible = False
                    Case Else
                        Dim body As HtmlGenericControl = Page.Master.FindControl("myBody")
                        body.Attributes.Add("onLoad", "InitDivVisibile(1);")

                        AbilitaRDS(False)
                        DropOperazione.Visible = False
                        lb_per_operazione.Visible = False
                        bt_stampa_lettera.Visible = False
                        bt_stampa_RDS.Visible = False
                        bt_salva_rds.Visible = False
                End Select

                tx_data_incidente.Text = .data_incidente & ""
                tx_luogo_incidente.Text = .luogo_incidente & ""

                tx_stima_rds.Text = Libreria.myFormatta(.importo, "0.00")

                If Drop_aliquote_iva.Items.Count <= 1 Then
                    Drop_aliquote_iva.DataBind()
                End If

                If .iva Is Nothing Then
                    Drop_aliquote_iva.SelectedValue = Costanti.id_iva_default
                Else
                    Drop_aliquote_iva.SelectedValue = .iva
                End If

                If .spese_postali Is Nothing Then
                    tx_spese_postali.Text = Libreria.myFormatta(Costanti.spese_postali_rds_default, "0.00")
                Else
                    tx_spese_postali.Text = Libreria.myFormatta(.spese_postali, "0.00")
                End If

                If .totale Is Nothing Then
                    tx_importo_totale.Text = CalcolaTotaleRDS(.importo, Drop_aliquote_iva.SelectedValue, Double.Parse(tx_spese_postali.Text))
                Else
                    tx_importo_totale.Text = Libreria.myFormatta(.totale, "0.00")
                End If

                tx_data_perizia.Text = .data_perizia & ""
                ck_perizia.Checked = getValoreCheck(.perizia)

                tx_giorni_fermo_tecnico.Text = .giorni_fermo_tecnico & ""

                ck_CID.Checked = getValoreCheck(.doc_CID)
                ck_denuncia.Checked = getValoreCheck(.doc_denuncia)
                ck_fotocopia_doc.Checked = getValoreCheck(.doc_fotocopia_doc)
                ck_preventivo.Checked = getValoreCheck(.doc_preventivo)
                tx_num_fotografie.Text = .num_fotografie & ""
                If .attesa_manutenzione Is Nothing Then
                    Drop_manutenzione.SelectedValue = 0
                Else
                    Drop_manutenzione.SelectedValue = .attesa_manutenzione
                End If
                If .attesa_documentazione Is Nothing Then
                    Drop_documenti.SelectedValue = 0
                Else
                    Drop_documenti.SelectedValue = .attesa_documentazione
                End If

            End With
        Catch ex As Exception
            Response.Write("error fillRds :" & ex.Message & "<br/>")
        End Try


    End Sub

    Private Sub FillEvento(ByVal mio_record As veicoli_evento_apertura_danno)
        With mio_record
            lb_id_evento.Text = .id
            DropDownTipoEventoAperturaDanno.SelectedValue = .id_tipo_documento_apertura
            'lb_id_documento_apertura.Text = .id_documento_apertura
            'lb_num_crv.Text = .num_crv
            tx_data_evento.Text = .data
            tx_nota_evento.Text = .nota
            lb_num_rds.Text = .id_rds & ""

            tab_dettagli_pagamento.Visible = True
            DettagliPagamento.InitForm(lb_num_prenotazione.Text, lb_id_documento_apertura.Text, lb_num_rds.Text, )
        End With
    End Sub

    Private Sub AzzeraEvento()
        Dim mio_record As veicoli_evento_apertura_danno = New veicoli_evento_apertura_danno
        With mio_record
            .id = 0
            .id_tipo_documento_apertura = 0
            .id_documento_apertura = 0
            .data = Now
            .nota = ""
        End With

        FillEvento(mio_record)
    End Sub

    Protected Sub ListView_dotazioni_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles ListView_dotazioni.ItemDataBound
        Dim lvi As ListViewDataItem = e.Item
        If (lvi.ItemType = ListViewItemType.DataItem) Then
            Dim lb_assente As Label = CType(lvi.FindControl("lb_assente"), Label)
            Dim lb_des_assente As Label = CType(lvi.FindControl("lb_des_assente"), Label)
            If Boolean.Parse(lb_assente.Text) Then
                lb_des_assente.Text = "Si"
                lb_des_assente.ForeColor = Drawing.Color.Red
            Else
                lb_des_assente.Text = "No"
            End If
        End If
    End Sub

    Protected Sub ListView_accessori_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles ListView_accessori.ItemDataBound
        Dim lvi As ListViewDataItem = e.Item
        If (lvi.ItemType = ListViewItemType.DataItem) Then
            Dim lb_assente As Label = CType(lvi.FindControl("lb_assente"), Label)
            Dim lb_des_assente As Label = CType(lvi.FindControl("lb_des_assente"), Label)
            If Boolean.Parse(lb_assente.Text) Then
                lb_des_assente.Text = "Si"
                lb_des_assente.ForeColor = Drawing.Color.Red
            Else
                lb_des_assente.Text = "No"
            End If
        End If
    End Sub

    Protected Sub lv_elenco_danni_F_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles lv_elenco_danni_F.DataBound
        Dim th_lente As Control = lv_elenco_danni_F.FindControl("th_lente")
        If th_lente IsNot Nothing Then
            th_lente.Visible = Boolean.Parse(lb_th_lente_storico.Text)
        End If
    End Sub


    Protected Sub lv_elenco_danni_R_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles lv_elenco_danni_R.DataBound
        Dim th_lente As Control = lv_elenco_danni_R.FindControl("th_lente")
        If th_lente IsNot Nothing Then
            th_lente.Visible = Boolean.Parse(lb_th_lente_storico.Text)
        End If
    End Sub


    Protected Sub ListView_meccanici_elettrici_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView_meccanici_elettrici.DataBound
        Dim th_lente As Control = ListView_meccanici_elettrici.FindControl("th_lente")
        If th_lente IsNot Nothing Then
            th_lente.Visible = Boolean.Parse(lb_th_lente_storico.Text)
        End If
    End Sub

    Protected Sub ListView_meccanici_elettrici_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListView_meccanici_elettrici.ItemCommand
        If e.CommandName = "lente" Then
            Dim lb_id_danno As Label = e.Item.FindControl("lb_id_danno")
            Dim lb_id_evento_apertura As Label = e.Item.FindControl("lb_id_evento_apertura")

            Dim mio_danno As veicoli_danni = veicoli_danni.getRecordDaId(Integer.Parse(lb_id_danno.Text))
            Trace.Write("ListView_meccanici_elettrici_ItemCommand: " & mio_danno.id_evento_apertura & " - " & mio_danno.id)

            edit_danno.InitForm(mio_danno.id_evento_apertura, mio_danno.id, 0, 0)
            Visibilita_2(DivVisibile.Div_CheckOutRDSDettaglio)
        End If
    End Sub

    Protected Sub ListView_meccanici_elettrici_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles ListView_meccanici_elettrici.ItemDataBound
        Dim lvi As ListViewDataItem = e.Item
        If (lvi.ItemType = ListViewItemType.DataItem) Then
            Dim lb_tipo_record As Label = CType(lvi.FindControl("lb_tipo_record"), Label)
            If lb_tipo_record.Text <> "" Then

                Dim lb_des_tipo_record As Label = CType(lvi.FindControl("lb_des_tipo_record"), Label)

                Dim id_tipo_record As tipo_record_danni = Integer.Parse(lb_tipo_record.Text)
                lb_des_tipo_record.Text = (id_tipo_record.ToString).Replace("_", " ")
            End If

        End If
    End Sub

    Protected Sub listViewElencoDanniPerEvento_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles listViewElencoDanniPerEvento.DataBound
        Dim th_lente As Control = listViewElencoDanniPerEvento.FindControl("th_lente")
        If th_lente IsNot Nothing Then
            th_lente.Visible = Boolean.Parse(lb_th_lente.Text)
        End If
        Dim th_da_addebitare As Control = listViewElencoDanniPerEvento.FindControl("th_da_addebitare")
        If th_da_addebitare IsNot Nothing Then
            th_da_addebitare.Visible = Boolean.Parse(lb_th_da_addebitare.Text)
        End If
    End Sub

    Protected Sub listViewElencoDanniPerEvento_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listViewElencoDanniPerEvento.ItemDataBound
        Dim lvi As ListViewDataItem = e.Item
        If (lvi.ItemType = ListViewItemType.DataItem) Then

            Dim lb_id_stato As Label = CType(lvi.FindControl("lb_id_stato"), Label)
            If lb_id_stato.Text <> "" Then
                Dim lb_des_id_stato As Label = CType(lvi.FindControl("lb_des_id_stato"), Label)
                Dim id_stato As stato_danno = Integer.Parse(lb_id_stato.Text)
                If id_stato = stato_danno.aperto Then
                    lb_des_id_stato.Text = "No"
                    lb_des_id_stato.ForeColor = Drawing.Color.Red
                Else
                    lb_des_id_stato.Text = "Si"
                End If
            End If

            Dim lb_id_entita_danno As Label = CType(lvi.FindControl("lb_id_entita_danno"), Label)
            If lb_id_entita_danno.Text <> "" Then
                Dim lb_des_id_entita_danno As Label = CType(lvi.FindControl("lb_des_id_entita_danno"), Label)
                Dim id_entita_danno As Entita_Danno = Integer.Parse(lb_id_entita_danno.Text)
                lb_des_id_entita_danno.Text = id_entita_danno.ToString
            End If

            Dim lb_tipo_record As Label = CType(lvi.FindControl("lb_tipo_record"), Label)
            If lb_tipo_record.Text <> "" Then
                Dim lb_des_tipo_record As Label = CType(lvi.FindControl("lb_des_tipo_record"), Label)
                Dim lb_des_id_tipo_danno_tipo_record As Label = CType(lvi.FindControl("lb_des_id_tipo_danno_tipo_record"), Label)
                Dim lb_descrizione_danno As Label = CType(lvi.FindControl("lb_descrizione_danno"), Label)
                Dim id_tipo_record As tipo_record_danni = Integer.Parse(lb_tipo_record.Text)
                lb_des_tipo_record.Text = (id_tipo_record.ToString).Replace("_", " ")

                lb_descrizione_danno.Visible = False
                Select Case id_tipo_record
                    Case tipo_record_danni.Danno_Carrozzeria
                        lb_des_id_tipo_danno_tipo_record.Text = ""
                    Case tipo_record_danni.Danno_Elettrico, tipo_record_danni.Danno_Meccanico
                        lb_des_id_tipo_danno_tipo_record.Text = "Guasto"
                        lb_descrizione_danno.Visible = True
                    Case tipo_record_danni.Furto
                        lb_des_id_tipo_danno_tipo_record.Text = "Totale"
                    Case Else
                        lb_des_id_tipo_danno_tipo_record.Text = "Assente"
                End Select
            End If

            Dim lb_da_addebitare As Label = CType(lvi.FindControl("lb_da_addebitare"), Label)
            If lb_da_addebitare.Text <> "" Then
                Dim lb_des_da_addebitare As Label = CType(lvi.FindControl("lb_des_da_addebitare"), Label)
                If lb_da_addebitare.Text Then
                    lb_des_da_addebitare.Text = "Si"
                    lb_des_da_addebitare.ForeColor = Drawing.Color.Red
                Else
                    lb_des_da_addebitare.Text = "No"
                End If
            End If
        End If
    End Sub

    Protected Sub listViewElencoDanniPerEvento_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listViewElencoDanniPerEvento.ItemCommand
        Dim lb_id_danno As Label = e.Item.FindControl("lb_id_danno")

        If e.CommandName = "lente" Then            
            Dim lb_tipo_record As Label = e.Item.FindControl("lb_tipo_record")
            If lb_tipo_record.Text = tipo_record_danni.Furto Then
                Libreria.genUserMsgBox(Page, "Per il furto totale del mezzo non esiste un dettaglio.")
                Return
            End If

            Dim stato_rds As Integer = 0
            If lb_stato_form.Text = stato_form_danni.DaRDS Then
                stato_rds = 1
            End If

            'Tony 05/10/2022
            'RDS (Nuovo-Modifiche-Eliminazione)
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "168") < 3 Then
                edit_danno.InitForm(Integer.Parse(lb_id_evento.Text), Integer.Parse(lb_id_danno.Text), Integer.Parse(lb_id_gruppo_evento.Text), stato_rds)
            Else
                edit_danno.InitFormModifiche(Integer.Parse(lb_id_evento.Text), lb_id_danno.Text, Integer.Parse(lb_id_danno.Text), Integer.Parse(lb_id_gruppo_evento.Text), stato_rds)
            End If
            'FINE Tony

            If lb_stato_form.Text = stato_form_danni.DaRDS Then
                Visibilita_2(DivVisibile.Div_GestioneRDSConDanno)
            Else
                Visibilita_2(DivVisibile.Div_ReadEventoConDanno)
            End If

        End If
        If e.CommandName = "elimina" Then
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
            Dim Sql As String
            Dim Sql2 As String
            Dim SqlQuery As String

            Try
                'Response.Write(lb_id_danno.Text & "<br>")

                Sql = "delete  from veicoli_danni WHERE id = '" & lb_id_danno.Text & "'"

                'Response.Write(Sql & " -- " & lb_id_evento.Text)
                'Response.End()

                Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
                Cmd.ExecuteNonQuery()

                'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                'Session("residenza_virtuale") = Cmd.ExecuteScalar

                SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                Cmd.CommandText = SqlQuery
                'Response.Write(Cmd.CommandText & "<br/>")
                'Response.End()
                Cmd.ExecuteNonQuery()

                If VeicoliDanniVuoto() Then
                    EliminaVeicoliEventoAperturaDanno(lb_id_evento.Text)
                End If
            Catch ex As Exception
                HttpContext.Current.Response.Write("Eliminazione Danno Errore contattare amministratore del sistema. <br/>" & ex.Message & "<br/>")
            End Try

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            listViewElencoDanniPerEvento.DataBind()
            lv_elenco_danni_F.DataBind()
            div_edit_danno.Visible = False
        End If
    End Sub

    Protected Sub bt_salva_edit_evento_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles bt_salva_edit_evento.Click
        Dim mio_record As veicoli_evento_apertura_danno = New veicoli_evento_apertura_danno
        With mio_record
            .id = lb_id_evento.Text
            .id_veicolo = lb_id_veicolo.Text
            .id_tipo_documento_apertura = DropDownTipoEventoAperturaDanno.SelectedValue
            .id_documento_apertura = lb_id_documento_apertura.Text
            .num_crv = lb_num_crv.Text
            .data = Date.Parse(tx_data_evento.Text)
            .nota = tx_nota_evento.Text
            .id_ditta = Integer.Parse(lb_id_ditta.Text)

            If .id = 0 Then
                lb_id_evento.Text = .SalvaRecord()
            Else
                .AggiornaRecord()
            End If

        End With

        Libreria.genUserMsgBox(Page, "Informazione salvata correttamente.")
    End Sub

    Protected Sub bt_nuovo_danno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_nuovo_danno.Click
        edit_danno.InitForm(Integer.Parse(lb_id_evento.Text))
    End Sub

    Protected Sub DropDownTipoDocumentoImg_DataBind(Optional ByVal id_select_value As Integer = 0)
        DropDownTipoDocumentoImg.Items.Clear()
        DropDownTipoDocumentoImg.Items.Add(New ListItem("Seleziona...", "0"))
        DropDownTipoDocumentoImg.DataBind()
        DropDownTipoDocumentoImg.SelectedValue = id_select_value
    End Sub

    Protected Function generaNomeFile() As String
        generaNomeFile = System.Guid.NewGuid().ToString
    End Function

    Protected Sub InviaFile(ByVal MioFileUpload As FileUpload, ByVal filePath As String)
        Dim Messaggio As String = ""
        If MioFileUpload.HasFile Then
            Dim estensione As String = LCase(Right(MioFileUpload.FileName, 4))
            If estensione = ".jpg" Or estensione = ".png" Or estensione = ".pdf" Then
                ' Trace.Write("MioFileUpload.PostedFile.ContentLength:" & MioFileUpload.PostedFile.ContentLength)
                If MioFileUpload.PostedFile.ContentLength <= 5000000 Then

                    Dim NomeFile As String
                    NomeFile = generaNomeFile()

                    Dim fileNameBig As String = lb_id_veicolo.Text & "_" & lb_id_evento.Text & "_" &
                        DropDownTipoDocumentoImg.SelectedValue & "_" & NomeFile & estensione

                    MioFileUpload.SaveAs(filePath & fileNameBig)

                    If File.Exists(filePath & fileNameBig) Then
                        Dim mia_foto_danno As veicoli_danni_evento_foto = New veicoli_danni_evento_foto
                        With mia_foto_danno
                            .id_evento = Integer.Parse(lb_id_evento.Text)
                            .descrizione = MioFileUpload.FileName
                            .riferimento_foto = fileNameBig
                            .tipo_documento = DropDownTipoDocumentoImg.SelectedValue

                            .SalvaRecord()
                        End With

                        Select Case DropDownTipoDocumentoImg.SelectedValue
                            Case Is = "8" 'Dichiarazione Cliente
                                InvioMail(lb_num_rds, lb_targa, "Dichiarazione Cliente")
                            Case Is = "9" 'CAI
                                InvioMail(lb_num_rds, lb_targa, "CAI")
                        End Select                        

                        listViewDocumenti.DataBind()

                        DropDownTipoDocumentoImg.SelectedValue = 0

                        Messaggio = "Documento/Immagine correttamente salvata."
                    End If
                Else
                    Messaggio = "Il file non può essere caricato perché supera 5MB!"
                End If


            Else
                Messaggio = "L'estensione dell'immagine deve essere con estensione (jpg, png, pdf)"
            End If



        Else
            Messaggio = "Selezionare un file da salvare."
        End If

        If Messaggio <> "" Then
            Libreria.genUserMsgBox(Page, Messaggio)
        End If
    End Sub

    Protected Sub btnInviaFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInviaFile.Click
        InviaFile(FileUpload1, HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\images\DocDanni\")
    End Sub

    Protected Sub listViewDocumenti_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listViewDocumenti.ItemCommand
        Trace.Write("listViewFoto_ItemCommand: " & e.CommandName)

        If e.CommandName = "elimina" Then
            Dim id_riga As Label = e.Item.FindControl("lb_id")

            Dim mia_foto As veicoli_danni_evento_foto = veicoli_danni_evento_foto.get_foto_da_id(Integer.Parse(id_riga.Text))

            Dim filePath As String
            filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\images\DocDanni\"

            File.Delete(filePath & mia_foto.riferimento_foto)
            If File.Exists(filePath & mia_foto.riferimento_foto) Then
                Libreria.genUserMsgBox(Page, "Errore nella cancellazione del documento.")
                Return
            End If

            If mia_foto.CancellaFoto() Then
                Libreria.genUserMsgBox(Page, "Documento cancellato correttamente.")
            End If

            listViewDocumenti.DataBind()
        End If
    End Sub

    Protected Sub lv_elenco_danni_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles lv_elenco_danni_F.ItemCommand, lv_elenco_danni_R.ItemCommand
        Dim lb_id_danno As Label = e.Item.FindControl("lb_id_danno")
        Dim lb_id_evento_apertura As Label = e.Item.FindControl("lb_id_evento_apertura")
        Dim QuantiImg As Integer = 0



        If e.CommandName = "lente" Then

            Dim mio_danno As veicoli_danni = veicoli_danni.getRecordDaId(Integer.Parse(lb_id_danno.Text))
            Trace.Write("lv_elenco_danni_ItemCommand: " & mio_danno.id_evento_apertura & " - " & mio_danno.id)

            edit_danno.InitForm(mio_danno.id_evento_apertura, mio_danno.id, 0, 0)

            Visibilita_2(DivVisibile.Div_CheckOutRDSDettaglio)
        End If

        'Tony 16/06/2022
        If e.CommandName = "lentefoto" Then
            'Response.Write("OK")
            Session("DatiSlideImg") = ""
            Session("Dati") = ""

            Try
                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM  veicoli_danni WITH(NOLOCK) WHERE id=" & lb_id_danno.Text, Dbc)
                'Response.Write(Cmd.CommandText & "<br><br>")
                'Response.End()
                Dim Rs As Data.SqlClient.SqlDataReader
                Rs = Cmd.ExecuteReader()
                If Rs.HasRows Then
                    Do While Rs.Read
                        Try
                            Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                            Dbc2.Open()

                            Dim Cmd2 As New Data.SqlClient.SqlCommand("SELECT * FROM  veicoli_danni_foto WITH(NOLOCK) WHERE id_danno=" & Rs("id"), Dbc2)
                            'Response.Write(Cmd2.CommandText & "<br><br>")
                            'Response.End()
                            Dim Rs2 As Data.SqlClient.SqlDataReader
                            Rs2 = Cmd2.ExecuteReader()
                            If Rs2.HasRows Then
                                Do While Rs2.Read
                                    QuantiImg = QuantiImg + 1
                                    If QuantiImg = 1 Then
                                        Session("DatiSlideImg") = "<div class=""item active""><img src=""\images\FotoDanni\" & Rs2("riferimento_foto") & """ alt=""Los Angeles"" style=""width:100%;""></div>"
                                    Else
                                        Session("DatiSlideImg") = Session("DatiSlideImg") & "@" & "<div class=""item""><img src=""\images\DocDanni\" & Rs2("riferimento_foto") & """ alt=""Los Angeles"" style=""width:100%;""></div>"
                                    End If
                                Loop
                                Rs2.Close()
                                Dbc2.Close()
                                Rs2 = Nothing
                                Dbc2 = Nothing

                                Session("Dati") = lb_num_documento.Text & "@" & lb_targa.Text & "@" & lb_modello.Text & "@" & lb_stazione.Text & "@" & lb_km.Text

                                Dim UrlPagina As String = "vedi_foto_danni.aspx"

                                Response.Write("<script language=javascript>window.open('" & UrlPagina & "', '_blank');</script>")
                                'Response.Redirect("\vedi_foto_danni.aspx")



                            Else
                                Dim RispostaMsgBx As String

                                RispostaMsgBx = "Nessuna immagine caricata per questo evento"
                                Libreria.genUserMsgBox(Page, RispostaMsgBx)

                                Rs2.Close()
                                Dbc2.Close()
                                Rs2 = Nothing
                                Dbc2 = Nothing
                            End If

                            
                        Catch ex As Exception
                            Response.Write("KO - veicoli_danni_evento_foto")
                        End Try
                    Loop                    
                End If

                Rs.Close()
                Dbc.Close()
                Rs = Nothing
                Dbc = Nothing
            Catch ex As Exception
                Response.Write("KO - veicoli_gruppo_danni")
            End Try
        End If
    End Sub

    Protected Sub lv_elenco_danni_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles lv_elenco_danni_F.ItemDataBound, lv_elenco_danni_R.ItemDataBound
        Dim lvi As ListViewDataItem = e.Item
        If (lvi.ItemType = ListViewItemType.DataItem) Then

            Dim lb_id_entita_danno As Label = CType(lvi.FindControl("lb_id_entita_danno"), Label)
            If lb_id_entita_danno.Text <> "" Then
                Dim lb_des_id_entita_danno As Label = CType(lvi.FindControl("lb_des_id_entita_danno"), Label)
                Dim id_entita_danno As Entita_Danno = Integer.Parse(lb_id_entita_danno.Text)
                lb_des_id_entita_danno.Text = id_entita_danno.ToString
            End If
        End If
    End Sub

    Protected Sub bt_nessun_danno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_nessun_danno.Click
        Visibilita_2(DivVisibile.Div_NessunDanno)
    End Sub

    Protected Sub bt_chiudi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_chiudi.Click
        tab_checkin.Visible = True
        RaiseEvent ChiusuraForm(Me, New EventArgs)



    End Sub

    Protected Sub bt_nuovo_evento_danno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_nuovo_evento_danno.Click
        'Tony 06/10/2022
        If Request.Path.ToString = "/gestione_danni.aspx" Then
            'Response.Write("Nuovo da RDS <br>")

            Session("IdEventoApertura_mio") = txtIdEventoApertura.Text
            'Response.Write("1 " & Session("IdEventoApertura_mio") & "<br>")
            Session("IdVeicolo_mio") = lb_id_veicolo.Text
            'Response.Write("1 " & Session("IdEventoApertura_mio") & "<br>")

            tab_mappe.ActiveTabIndex = 0            

            Visibilita_2(DivVisibile.Div_EditEvento)

            rb_ready_to_go.Enabled = True
            ck_fermo_tecnico.Enabled = True
            ck_vendita_buy_back.Enabled = True
            ck_furto.Enabled = True
            tx_altro.Enabled = True

            'Dati Km e Litri di rientro
            Try
                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM  veicoli WITH(NOLOCK) WHERE targa='" & lb_targa.Text & "'", Dbc)
                'Response.Write(Cmd.CommandText & "<br><br>")
                'Response.End()
                Dim Rs As Data.SqlClient.SqlDataReader
                Rs = Cmd.ExecuteReader()
                If Rs.HasRows Then
                    Do While Rs.Read
                        lb_serbatoio_memo.Text = "(Serbatoio " & Rs("serbatoio_attuale") & ")"
                    Loop
                End If

                Rs.Close()
                Dbc.Close()
                Rs = Nothing
                Dbc = Nothing
            Catch ex As Exception
                HttpContext.Current.Response.Write("Visibilità Dati KM e Litri di Rientro Errore contattare amministratore del sistema. <br/>" & ex.Message & "<br/>")
            End Try

            Label13.Visible = False
            tx_km_rientro.Enabled = False
            tx_km_rientro.Visible = False

            Label15.Visible = False
            DropDownSerbatoioRientro.Visible = False
            DropDownSerbatoioRientro.Enabled = False

            bt_salva_checkin.Visible = True

            'Tony 01/11/2022
            Dim mio_veicolo As tabella_veicoli = tabella_veicoli.get_record_da_id(lb_id_veicolo.Text)
            DropDownSerbatoioRientro.Enabled = False
            DropDownSerbatoioRientro.Visible = False
            'Response.Write("Att " & CInt(mio_veicolo.serbatoio_attuale) & "<br>")
            'Response.Write("Cap " & CInt(mio_veicolo.capacita_serbatoio / 8) & "<br>")
            'Response.Write("Diff " & CInt(CInt(mio_veicolo.serbatoio_attuale) / CInt(mio_veicolo.capacita_serbatoio / 8)))
            DropDownSerbatoioRientro.SelectedValue = CInt(CInt(mio_veicolo.serbatoio_attuale) / CInt(mio_veicolo.capacita_serbatoio / 8))
            'FINE Tony
        Else
            tab_mappe.ActiveTabIndex = 0

            'Tony 29/08/2022
            Session("IdEventoApertura_mio") = txtIdEventoApertura.Text
            'Response.Write("1 " & Session("IdEventoApertura_mio") & "<br>")
            Session("IdVeicolo_mio") = lb_id_veicolo.Text
            'Response.Write("1 " & Session("IdEventoApertura_mio") & "<br>")

            Visibilita_2(DivVisibile.Div_EditEvento)

            'Sezione Check In
            div_salva_checkin.Visible = True



            'Permesso Su Pulsante Nuovo Danno
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "165") = 3 Then
                'rb_ready_to_go.Enabled = False
                ''Inizializza Radio Button
                'For i As Integer = 0 To rb_ready_to_go.Items.Count - 1
                '    rb_ready_to_go.Items(i).Selected = False
                'Next

                If getRdsNum(lb_num_documento.Text) <> "" Then
                    rb_ready_to_go.Enabled = False
                    ck_fermo_tecnico.Enabled = False
                    ck_vendita_buy_back.Enabled = False
                    ck_furto.Enabled = False
                    tx_altro.Enabled = False
                    tx_km_rientro.Enabled = False
                    DropDownSerbatoioRientro.Enabled = False
                    bt_salva_checkin.Visible = True
                Else
                    rb_ready_to_go.Enabled = True
                    ck_fermo_tecnico.Enabled = True
                    ck_vendita_buy_back.Enabled = True
                    ck_furto.Enabled = True
                    tx_altro.Enabled = True
                    tx_km_rientro.Enabled = False
                    DropDownSerbatoioRientro.Enabled = False
                    bt_salva_checkin.Visible = True
                End If

                If Request.Path.ToString = "/trasferimenti.aspx" Then
                    ck_fermo_tecnico.Enabled = True
                    ck_vendita_buy_back.Enabled = True
                    ck_furto.Enabled = True
                    tx_altro.Enabled = True
                    tx_km_rientro.Enabled = True
                    DropDownSerbatoioRientro.Enabled = True
                    bt_salva_checkin.Visible = True
                End If
            Else
                ck_fermo_tecnico.Enabled = True
                ck_vendita_buy_back.Enabled = True
                ck_furto.Enabled = True
                tx_altro.Enabled = True
                tx_km_rientro.Enabled = True
                DropDownSerbatoioRientro.Enabled = True
                bt_salva_checkin.Visible = True
            End If
        End If

    End Sub

    Protected Sub bt_furto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_furto.Click
        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(Integer.Parse(lb_id_evento.Text))

        Dim mio_danno As veicoli_danni = New veicoli_danni
        With mio_danno
            .tipo_record = tipo_record_danni.Furto
            .id_evento_apertura = Integer.Parse(lb_id_evento.Text)
            .id_veicolo = Integer.Parse(lb_id_veicolo.Text)
            If lb_id_ditta.Text <> "" Then
                .id_ditta = Integer.Parse(lb_id_ditta.Text)
            End If
            .attivo = False

            .SalvaRecord()
        End With

        listViewElencoDanniPerEvento.DataBind()

        lb_th_lente.Text = False
        riga_per_furto.Visible = True

        bt_nuovo_danno.Visible = False
        bt_salva_furto.Visible = True
        bt_chiudi_senza_furto.Visible = True
        bt_pagamento_da_furto.Visible = False

        Visibilita_2(DivVisibile.Div_Furto)
    End Sub

    Protected Sub bt_chiudi_chekin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_chiudi_chekin.Click
        RaiseEvent ChiusuraForm(Me, New EventArgs)
    End Sub

    Protected Function verifica_check_in() As Boolean
        Dim messaggio As String = ""

        If DropDownTipoEventoAperturaDanno.Enabled AndAlso DropDownTipoEventoAperturaDanno.SelectedValue <= 0 Then
            messaggio += "Specificare l'evento che ha generato il danno."
        End If

        If tx_data_evento.Text = "" Then
            messaggio += "Specificare la data dell'evento che ha generato il danno."
        End If

        If rb_ready_to_go.SelectedValue Is Nothing OrElse rb_ready_to_go.SelectedValue = "" Then
            messaggio += "Specificare se il veicolo è Ready To Go o meno." & vbCrLf
        Else
            Dim DanniBloccanti As Boolean = veicoli_evento_apertura_danno.verificaDanniBloccanti(Integer.Parse(lb_id_evento.Text))
            If rb_ready_to_go.SelectedValue = "1" Then
                If DanniBloccanti Then
                    messaggio += "Il veicolo non può essere Ready To Go perché sono stati inseriti dei danni che pongono il veicolo in fermo tecnico." & vbCrLf
                End If
                'If ck_lavare.Checked Then
                '    messaggio += "Se il veicolo è Ready To Go non può essere contemporaneamente da lavare." & vbCrLf
                'End If
                If ck_rifornire.Checked Then
                    messaggio += "Se il veicolo è Ready To Go non può essere contemporaneamente da rifornire." & vbCrLf
                End If
                If ck_fermo_tecnico.Checked Then
                    messaggio += "Se il veicolo è Ready To Go non può essere contemporaneamente in fermo tecnico." & vbCrLf
                End If
                If tx_altro.Text <> "" Then
                    messaggio += "Se il veicolo è Ready To Go non può essere valorizzato il campo Altro." & vbCrLf
                End If
                If ck_vendita_buy_back.Checked Then
                    messaggio += "Se il veicolo è Ready To Go non può essere in vendita/buy back." & vbCrLf
                End If
                If ck_furto.Checked Then '16.08.2021
                    messaggio += "Se il veicolo è stato rubato non può essere Ready To Go" & vbCrLf
                End If
            Else
                If lbl_status_veicolo.Text <> "(in noleggio)" And lbl_status_veicolo.Text <> "(in lavaggio)" _
                    And lbl_status_veicolo.Text <> "(in ODL)" And lbl_status_veicolo.Text <> "(in trasferimento)" Then '21.03.2022 se è a noleggio salta verifica
                    If (Not ck_lavare.Checked) And (Not ck_rifornire.Checked) And (Not ck_fermo_tecnico.Checked) And Not (ck_vendita_buy_back.Checked) And Not (ck_furto.Checked) And tx_altro.Text = "" Then
                        messaggio += "Se il veicolo non è Ready To Go è necessario specificare perché." & vbCrLf
                    End If
                End If
                If DanniBloccanti And (Not ck_fermo_tecnico.Checked) Then
                    messaggio += "Sono presenti de danni per cui è necessario porre il veicolo in fermo tecnico (è necessario specificare una nota)." & vbCrLf
                End If
                If ck_fermo_tecnico.Checked Then
                    If tx_altro.Text = "" Then
                        messaggio += "Se il veicolo è in fermo tecnico è necessario specificare una nota nel campo (Non nolegiabile per altro:)." & vbCrLf
                    End If
                End If
                If ck_vendita_buy_back.Checked Then
                    If (ck_lavare.Checked) Or (ck_rifornire.Checked) Or (ck_fermo_tecnico.Checked) Then
                        messaggio += "Se il veicolo è in vendita/in buy back non può essere da lavare/da rifornire/in fermo tecnico." & vbCrLf
                    End If
                End If
                If ck_furto.Checked Then '16.08.2021
                    If (ck_lavare.Checked) Or (ck_rifornire.Checked) Or (ck_fermo_tecnico.Checked) Then
                        messaggio += "Se il veicolo è stato Rubato non può essere da lavare/da rifornire/in fermo tecnico." & vbCrLf
                    End If
                End If


            End If
        End If

        'Tony 20/05/2022
        'Response.Write("Percorso: " & Request.Path.ToString)
        'Response.Write("<br>URL: " & Request.Url.ToString)

        If Request.Path.ToString <> "/gestione_danni.aspx" Then
            If DropDownSerbatoioRientro.SelectedValue < 0 Then
                messaggio += "Specificare la quantità di carburante presente al check in." & vbCrLf
            End If

            If tx_km_rientro.Text = "" Then
                messaggio += "Specificare i km di rientro presenti al check in." & vbCrLf
            Else
                If Not IsNumeric(tx_km_rientro.Text) Then
                    messaggio += "Specificare un valore corretto per i km di rientro presenti al check in." & vbCrLf
                Else
                    If Integer.Parse(tx_km_rientro.Text) < CompareValidatorKmRientro.ValueToCompare Then
                        messaggio += "Specificare un valore corretto per i km di rientro presenti al check in, maggiore o uguale dei km di uscita (" & CompareValidatorKmRientro.ValueToCompare & " km)" & vbCrLf
                    End If
                End If
            End If

        End If

        'Trace.Write("div_edit_danno.Visible: " & div_edit_danno.Visible)

        If div_edit_danno.Visible Then ' sono nel caso di inserimento nuovo danno!
            If listViewElencoDanniPerEvento.Items.Count <= 0 Then
                messaggio += "Non è stato specificato alcun danno, in questo caso è necessario selezionare: (Nessun Nuovo Danno)." & vbCrLf
            End If
        End If

        If messaggio <> "" Then
            Libreria.genUserMsgBox(Page, messaggio)
            Return False
        Else
            Return True
        End If

    End Function

    Protected Function SalvaDaRifornire(ByVal id_veicolo As Integer, ByVal litri_serbatoio As Integer) As Boolean
        'PER NON CREARE RIGHE DOPPIE ELIMINO LE RIGHE DI RIFORNIMENTO NON USATE
        elimina_rifornimenti(id_veicolo)
        Dim sqlStr As String
        Try
            SalvaDaRifornire = False

            sqlStr = "INSERT INTO rifornimenti (id_veicolo, serbatoio) VALUES (" & id_veicolo & "," & litri_serbatoio & ")"

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM rifornimenti WHERE id_veicolo='" & id_veicolo & "' AND NOT data_uscita_parco IS NULL AND data_rientro_parco IS NULL", Dbc)
            Dim test As String = Cmd.ExecuteScalar & ""

            If test <> "" Then
                'SE C'E' GIA' UN RIFORNIMENTO IN CORSO NON EFFETTUO UN NUOVO INSERIMENTO
                SalvaDaRifornire = True
            Else
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteScalar()

                SalvaDaRifornire = True
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error SalvaDaRifornire :" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function


    Protected Function SalvaDaLavare(ByVal id_veicolo As Integer) As Boolean
        SalvaDaLavare = False
        Dim sqlStr As String
        Try
            sqlStr = "INSERT INTO lavaggi (id_veicolo) VALUES (" & id_veicolo & ")"

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    SalvaDaLavare = True
                End Using
            End Using
        Catch ex As Exception
            Response.Write("error SalvaDaLavare :" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Protected Function getCodiceStazione(ByVal id_stazione As Integer) As String
        getCodiceStazione = ""
        Dim sqlStr As String
        sqlStr = "SELECT codice FROM stazioni WITH(NOLOCK) WHERE id = " & id_stazione

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getCodiceStazione = Cmd.ExecuteScalar()
            End Using
        End Using
    End Function

    Protected Function getAutoODL(ByVal targa As String) As Boolean

        getAutoODL = False

        Dim sqlStr As String
        sqlStr = "SELECT veicoli.targa FROM odl INNER JOIN veicoli ON odl.id_veicolo = veicoli.id "
        sqlStr += "WHERE (odl.attivo = 1) and targa = '" & targa & "'  and id_stato_odl='1' "
        sqlStr += "ORDER BY odl.id DESC"
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    getAutoODL = Rs.HasRows
                End Using
            End Using
        End Using

    End Function


    Protected Sub bt_salva_checkin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_salva_checkin.Click
        
            If Not verifica_check_in() Then
                Return
            End If


            'se auto in odl ready to go solo NO '29.09.2021
            If getAutoODL(lb_targa.Text) Then
                If rb_ready_to_go.SelectedValue = "1" Then  'se SI msg
                    Libreria.genUserMsgBox(Page, "ODL Aperto. Ready to Go deve essere NO")
                    Exit Sub
                Else
                    rb_ready_to_go.SelectedValue = "2"  'attiva NO
                    rb_ready_to_go.Enabled = False
                End If
            End If


            'se a nolo non deve effettuare verifica 21.03.2022
            If lbl_status_veicolo.Text <> "(in noleggio)" And lbl_status_veicolo.Text <> "(in lavaggio)" _
                And lbl_status_veicolo.Text <> "(in trasferimento)" And lbl_status_veicolo.Text <> "(in ODL)" Then
                'se è in noleggio non è necessario alcuna specifica
                If ck_fermo_tecnico.Checked = False And tx_altro.Text <> "" Then
                    Libreria.genUserMsgBox(Page, "Note presenti, è necessario selezionare l'opzione fermo tecnico")
                    Exit Sub
                End If

                If ck_fermo_tecnico.Checked = True And tx_altro.Text = "" Then
                    Libreria.genUserMsgBox(Page, "E' necessario specificare il motivo per il fermo tecnico")
                    Exit Sub
                End If

            Else


            End If



            Dim nuovo_check_in As veicoli_check_in = New veicoli_check_in

            With nuovo_check_in
                .id_tipo_documento = Integer.Parse(lb_id_tipo_documento_apertura.Text)
                .id_documento = Integer.Parse(lb_id_documento_apertura.Text)
                .num_crv = Integer.Parse(lb_num_crv.Text)
                If rb_ready_to_go.SelectedValue = "1" Then
                    .data_ready_to_go = Now
                Else
                    .data_ready_to_go = Nothing

                    If ck_lavare.Checked Then
                        .data_lavaggio = Now
                    Else
                        .data_lavaggio = Nothing
                    End If

                    If ck_rifornire.Checked Then
                        .data_rifornimento = Now
                    Else
                        .data_rifornimento = Nothing
                    End If

                    .fermo_tecnico = ck_fermo_tecnico.Checked

                    .in_vendita_buy_back = ck_vendita_buy_back.Checked

                    If tx_altro.Text <> "" Then
                        .altro = tx_altro.Text
                    Else
                        .altro = Nothing
                    End If
                End If

                'Tony 25/05/2022
                Dim ArrayKm(1) As String
                Dim KmAus As String

                Dim ArrayLitri(1) As String
                Dim LitriAus As String

                If tx_km_rientro.Text = "" Then
                    ArrayKm = Split(lb_km_uscita_memo.Text, "uscita")
                    KmAus = Replace(ArrayKm(1), ")", "")
                    tx_km_rientro.Text = KmAus
                    'Response.Write(tx_km_rientro.Text & "<br>")                           
                End If

                .km_rientro = tx_km_rientro.Text
                'Response.Write(.km_rientro & "<br>")

                If DropDownSerbatoioRientro.SelectedValue = "-1" Then
                    ArrayLitri = Split(lb_serbatoio_memo.Text, " ")
                    LitriAus = Replace(ArrayLitri(1), ")", "")
                    'Response.Write("Valore Serbatoio " & LitriAus & "<br>")
                    DropDownSerbatoioRientro.SelectedValue = (getSerbatoioDaIdVeicolo(IDdelVeicolo.Text) / Integer.Parse(LitriAus)) * 8
                    'Response.Write("Valore in 8 " & DropDownSerbatoioRientro.SelectedValue & "<br>")
                End If

                .litri_rientro_frazione = DropDownSerbatoioRientro.SelectedValue
                'Response.Write(.litri_rientro_frazione & "<br>")
                'Fine


                .SalvaRecord()
            End With


            Dim id_tipo_documento As tipo_documento = Integer.Parse(lb_id_tipo_documento_apertura.Text)
            If Integer.Parse(DropDownTipoEventoAperturaDanno.SelectedValue) >= tipo_documento.RDSGenerico Then
                id_tipo_documento = Integer.Parse(DropDownTipoEventoAperturaDanno.SelectedValue)
                lb_id_tipo_documento_apertura.Text = id_tipo_documento
            End If

            Dim mio_record As DatiContratto = Nothing
            Select Case id_tipo_documento
                Case tipo_documento.Contratto
                    mio_record = DatiContratto.getRecordDaNumContratto(lb_id_documento_apertura.Text, lb_num_crv.Text)
                    If mio_record Is Nothing Then
                        Err.Raise(1001, Nothing, "Errore: contratto non trovato.")
                    End If

                Case tipo_documento.MovimentoInterno
                    mio_record = DatiContratto.getRecordDaNumTrasferimento(lb_id_documento_apertura.Text)
                    If mio_record Is Nothing Then
                        Err.Raise(1001, Nothing, "Errore: movimento interno non trovato.")
                    End If
                Case tipo_documento.Lavaggio
                    mio_record = DatiContratto.getRecordDaLavaggio(lb_id_documento_apertura.Text)
                    If mio_record Is Nothing Then
                        Err.Raise(1001, Nothing, "Errore: movimento interno non trovato.")
                    End If
                Case tipo_documento.ODL
                    mio_record = DatiContratto.getRecordDaNumODL(lb_id_documento_apertura.Text)
                    If mio_record Is Nothing Then
                        Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                    End If

                Case Is >= tipo_documento.RDSGenerico
                    mio_record = DatiContratto.getRecordDaRDSGenerico(lb_id_evento.Text)
                    If mio_record Is Nothing Then
                        Err.Raise(1001, Nothing, "Errore: RDS Generico non trovato.")
                    End If

                Case Else
                    Err.Raise(1001, Nothing, "Tipo documento non previsto")
            End Select

            'se auto rubata aggiorna campo furto in veicoli 14.08.2021
            If ck_furto.Checked Then
                Dim idveicolo = mio_record.id_veicolo
                SetFurtoVicolo(idveicolo)


            End If




            Dim noleggio_in_corso As Boolean = False

            With mio_record
                'NEL CASO DI RDS GENERICO CONTROLLO SE E' UN RDS A NOLO IN CORSO - IN QUESTO CASO NON CAMBIO LO STATO DI "NOLEGGIATA" E "DISPONIBILE_NOLO"
                If id_tipo_documento >= tipo_documento.RDSGenerico Then
                    noleggio_in_corso = nolo_in_corso(.id_veicolo)
                    If noleggio_in_corso Then
                        .noleggiata = True
                        .disponibile_nolo = False
                    Else
                        .noleggiata = False
                        .disponibile_nolo = (rb_ready_to_go.SelectedValue = "1")
                    End If
                Else
                    .noleggiata = False
                    .disponibile_nolo = (rb_ready_to_go.SelectedValue = "1")
                End If


                .da_lavare = ck_lavare.Checked
                .da_rifornire = ck_rifornire.Checked
                .da_riparare = ck_fermo_tecnico.Checked
                .in_vendita_buy_back = ck_vendita_buy_back.Checked

                Dim dimensione_serbatoio As Integer = getSerbatoioDaIdVeicolo(.id_veicolo)

                'Tony 25-05-2022
                .litri_rientro = DropDownSerbatoioRientro.SelectedValue * dimensione_serbatoio / 8
                .km_rientro = Integer.Parse(tx_km_rientro.Text)
                'Fine

                Dim mio_gruppo_evento As veicoli_gruppo_evento = Nothing

                If div_edit_danno.Visible Then ' sono nel caso di inserimento nuovo danno!
                    If listViewElencoDanniPerEvento.Items.Count > 0 Then
                        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(Integer.Parse(lb_id_evento.Text))
                        If id_tipo_documento >= tipo_documento.RDSGenerico Then
                            mio_evento.id_tipo_documento_apertura = id_tipo_documento
                        End If
                        mio_evento.data = New Date(Year(tx_data_evento.Text), Month(tx_data_evento.Text), Day(tx_data_evento.Text))
                        mio_evento.nota = tx_nota_evento.Text
                        mio_evento.sospeso_rds = False ' rendo visibile al modulo RDS il danno inserito (anche quello proveniente da ODL)
                        mio_evento.AggiornaRecord()

                        If mio_evento.id_rds Is Nothing Then
                            'Tony 22-09-2022 

                            'Permesso Su Pulsante Nuovo Danno
                            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "165") = 3 Then
                                Dim CodiceStazione As String = TrovareCodiceStazione(lb_num_documento.Text)
                                'Response.Write("Stazione 1 Codice " & CodiceStazione)
                                'Response.End()

                                mio_evento.AttivaRecord(CodiceStazione)
                                Session("StazioneDiRientro") = CodiceStazione
                            Else
                                Dim CodiceStazione As String = getCodiceStazione(.id_stazione_rientro)
                                'Response.Write("Stazione 2" & getCodiceStazione(.id_stazione_rientro))

                                mio_evento.AttivaRecord(CodiceStazione)
                            End If
                            'FINE Tony
                        Else
                            ' salvo lo stato inizale delle variazioni RDS
                            mio_evento.SalvaStatoInizialeVariazioneRDS()

                            ' devo attivare tutti i danni sia quelli attuali che quelli provenienti da ODL
                            For i = 0 To listViewElencoDanniPerEvento.Items.Count - 1
                                Dim lb_id_danno As Label = listViewElencoDanniPerEvento.Items(i).FindControl("lb_id_danno")
                                veicoli_danni.AttivaDanno(Integer.Parse(lb_id_danno.Text))
                            Next

                            'Tony 22_09_2022
                            'Permesso Su Pulsante Nuovo Danno
                            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "165") = 3 Then
                                Dim CodiceStazione As String = TrovareCodiceStazione(lb_num_documento.Text)
                                'Response.Write("Stazione 1 Codice " & CodiceStazione)

                                mio_evento.AttivaRecord(CodiceStazione)
                                Session("StazioneDiRientro") = CodiceStazione
                            Else
                                Dim CodiceStazione As String = getCodiceStazione(.id_stazione_rientro)
                                'Response.Write("Stazione 2" & getCodiceStazione(.id_stazione_rientro))
                            End If
                            'FINE Tony
                        End If

                        lb_num_rds.Text = mio_evento.id_rds

                        mio_gruppo_evento = veicoli_gruppo_evento.SalvaDanniApertiAuto(mio_record.id_veicolo, id_tipo_documento, mio_record.num_contratto, mio_record.num_crv, mio_evento.id)

                        ' se non sono su contratto non ha senso attivare il pagamento!
                        If id_tipo_documento = tipo_documento.Contratto Then
                            ' ho inserito un danno rendo quindi visibile il pulsante dei pagamenti

                            'Tony 24/08/2022
                            'bt_pagamento.Visible = True
                            bt_pagamento.Visible = False
                            'Tony FINE
                        End If
                    End If
                End If

                If mio_gruppo_evento Is Nothing Then
                    .id_gruppo_danni_rientro = .id_gruppo_danni_uscita
                Else
                    .id_gruppo_danni_rientro = mio_gruppo_evento.id
                End If


                'Tony 30-09-2022
                'Prima dell'intervento era attivo solo da IF ad End If adesso commentato

                'If Integer.Parse(DropDownSerbatoioRientro.SelectedValue) < 8 And id_tipo_documento < tipo_documento.RDSGenerico Then
                '    SalvaDaRifornire(.id_veicolo, .litri_rientro)
                'End If

                'Response.Write("tipo doc " & id_tipo_documento & "<br>")
                'Response.Write("tipo doc Gen RDS" & id_tipo_documento.RDSGenerico & "<br>")
                'Permesso Su Pulsante Nuovo Danno
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "165") = 3 Then
                    '.AggiornaVeicoliPerCheckIn(id_tipo_documento)                   

                    Dim stato_contratto As String = getStatoContratto()
                    If stato_contratto = "3" Then
                        If Integer.Parse(DropDownSerbatoioRientro.SelectedValue) < 8 And id_tipo_documento < tipo_documento.RDSGenerico Then
                            SalvaDaRifornire(.id_veicolo, .litri_rientro)
                        End If
                    End If
                Else
                    If Integer.Parse(DropDownSerbatoioRientro.SelectedValue) < 8 And id_tipo_documento < tipo_documento.RDSGenerico Then
                        SalvaDaRifornire(.id_veicolo, .litri_rientro)
                    End If
                End If
                'Fine Tony

                If ck_lavare.Checked Then
                    SalvaDaLavare(.id_veicolo)
                End If

                Select Case id_tipo_documento
                    Case tipo_documento.Contratto
                        .AggiornaContrattiPerCheckIn()
                        .AggiornaCRVPerCheckIn()
                        .AggiornaMovimentiAutoPerCheckIn(Costanti.idMovimentoNoleggio) ' attenzione nel metodo gestisco in modo differente se CRV o meno

                    Case tipo_documento.MovimentoInterno
                        .data_rientro = Now
                        .AggiornaMovimentiInterniPerCheckIn()
                        .AggiornaMovimentiAutoPerCheckIn(Costanti.idMovimentoInterno)
                    Case tipo_documento.Lavaggio
                        .data_rientro = Now
                        .AggiornaLavaggioPerCheckIn()
                        .AggiornaMovimentiAutoPerCheckIn(Costanti.idLavaggio)
                    Case tipo_documento.ODL
                        ' .data_rientro = Now ' LA DATA VIENE INSERITA NEL MODULO ODL!
                        .AggiornaODLPerCheckIn()
                        .AggiornaMovimentiAutoPerCheckIn(Costanti.idMovimentoODL)

                    Case Is >= tipo_documento.RDSGenerico
                        ' salvo il gruppo di chiusura danno sul record ed il numero documento diventa il numero RDS!
                        .AggiornaRDSGenericoPerCheckIn()

                    Case Else

                End Select

                'Response.Write("tipo doc " & id_tipo_documento & "<br>")
                'Response.Write("RDSGenerico doc " & tipo_documento.RDSGenerico & "<br>")

                'Response.End()

                If noleggio_in_corso Then
                    .AggiornaVeicoliNoloInCorso(id_tipo_documento)
                    'Response.Write("1")
                Else
                    'Response.Write("2")
                    'Tony 20-09-2022
                    'Permesso Su Pulsante Nuovo Danno
                    If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "165") = 3 Then
                        '.AggiornaVeicoliPerCheckIn(id_tipo_documento)                   

                        If id_tipo_documento = tipo_documento.Contratto Then
                            Dim stato_contratto As String = getStatoContratto()
                            If stato_contratto = "3" Then
                                If Session("StazioneDiRientro") & "" <> "" Then
                                    AggiornaMioCheckIn(Session("StazioneDiRientro"))
                                End If
                            End If
                        Else
                            .AggiornaVeicoliPerCheckIn(id_tipo_documento)
                        End If

                        If id_tipo_documento >= tipo_documento.RDSGenerico Then
                            Dim StazioneDiRientroRDS As String
                            Dim arrayStazioneDiRientroRDS(1) As String
                            arrayStazioneDiRientroRDS = Split(lb_stazione.Text, " ")
                            StazioneDiRientroRDS = arrayStazioneDiRientroRDS(0)

                            AggiornaMioCheckIn(TrovareIdStazione(StazioneDiRientroRDS))
                        End If

                    Else
                        .AggiornaVeicoliPerCheckIn(id_tipo_documento)
                    End If
                    'FINE Tony
                End If

            End With

            Libreria.genUserMsgBox(Page, "Salvataggio avvenuto correttamente")

            Dim ev As EventoNuovoRecord = New EventoNuovoRecord
            ev.Valore = mio_record.num_crv

            RaiseEvent SalvaCheckIn(Me, ev)

        'Tony 14/10/2022
        Select Case Request.Path.ToString
            Case Is = "/gestione_danni.aspx"
                If rb_ready_to_go.SelectedValue = "1" Then
                    AggiornaStatoVeicolo(1)
                Else
                    AggiornaStatoVeicolo(0)
                End If

                tab_checkin.Visible = True
                RaiseEvent ChiusuraForm(Me, New EventArgs)
            Case Is = "/trasferimenti.aspx"
                If rb_ready_to_go.SelectedValue = "1" Then
                    AggiornaStatoVeicolo(1)
                Else
                    AggiornaStatoVeicolo(0)
                End If

                tab_checkin.Visible = True
                RaiseEvent ChiusuraForm(Me, New EventArgs)
            Case Else
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "165") = 3 Then
                    If id_tipo_documento = "1" Then 'Contratto
                        AggiornaContratto()
                        Response.Redirect("contratti.aspx?nr=" & lb_num_documento.Text)
                        'Response.End()
                    Else
                        'Response.End()
                    End If
                Else
                    If id_tipo_documento = "1" Then 'Contratto                
                        Response.Redirect("contratti.aspx?nr=" & lb_num_documento.Text)
                        'Response.End()
                    Else
                        'Response.End()
                    End If
                End If
        End Select


        'If Request.Path.ToString = "/gestione_danni.aspx" Then               
        '    If rb_ready_to_go.SelectedValue = "1" Then
        '        AggiornaStatoVeicolo(1)
        '    Else
        '        AggiornaStatoVeicolo(0)
        '    End If

        '    tab_checkin.Visible = True
        '    RaiseEvent ChiusuraForm(Me, New EventArgs)
        'Else
        '    If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "165") = 3 Then
        '        If id_tipo_documento = "1" Then 'Contratto
        '            AggiornaContratto()
        '            Response.Redirect("contratti.aspx?nr=" & lb_num_documento.Text)
        '            'Response.End()
        '        Else
        '            'Response.End()
        '        End If
        '    Else
        '        If id_tipo_documento = "1" Then 'Contratto                
        '            Response.Redirect("contratti.aspx?nr=" & lb_num_documento.Text)
        '            'Response.End()
        '        Else
        '            'Response.End()
        '        End If
        '    End If
        'End If                   

    End Sub


    Protected Sub SetFurtoVicolo(ByVal idveicolo As String)

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            'LE COMMISSIONI LE RICOPIO SEMPRE DAI CAMPI ORIGINALI - QUESTO IN QUANTO, SE E' STATO EFFETTUATO UN CALCOLO CON GIORNI DIMINUITI E POI RITORNO AI GIORNI ORIGINALI 
            'O SUPERIORE, I CAMPI NON ORIGINALI CONTENGONO LA PERCENTUALE DEI GIORNI EFFETTIVI, NON CORRISPONDENTI ALLA COMMISSIONE PREINCASSATA DALL'AGENZIA
            Dim sqlStr As String = "UPDATE veicoli SET furto=1 WHERE [id]='" & idveicolo & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception

        End Try



    End Sub




    Protected Function getComuneAres(ByVal id_comune As Integer) As String
        Dim sqlStr As String = "SELECT comune + ' (' + provincia + ')' FROM comuni_ares WITH(NOLOCK)" &
            " WHERE id = " & id_comune

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getComuneAres = Cmd.ExecuteScalar() & ""
            End Using
        End Using
    End Function

    Protected Sub bt_stampa_atto_notorio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_stampa_atto_notorio.Click

        Dim mio_record As DatiContratto = CType(ViewState("DocumentoAssociato"), DatiContratto)
        Dim miei_dati As DatiStampaAttoNotorio = Nothing

        Dim id_tipo_documento As tipo_documento = Integer.Parse(lb_id_tipo_documento_apertura.Text)
        Select Case id_tipo_documento
            Case tipo_documento.Contratto
                Dim sqlStr As String = "SELECT conducenti.nominativo, conducenti.data_nascita, conducenti.luogo_nascita As luogo_nascita_old, conducenti.id_comune_ares, conducenti.provincia, conducenti.indirizzo, conducenti.cap, " &
                    " conducenti.comune_nascita_ee, comuni_ares_nascita.comune As luogo_nascita, conducenti.city " &
                    " FROM conducenti WITH(NOLOCK) " &
                    " LEFT JOIN comuni_ares As comuni_ares_nascita WITH(NOLOCK) ON conducenti.id_comune_ares_nascita=comuni_ares_nascita.id " &
                    " WHERE ID_CONDUCENTE = " & mio_record.id_primo_conducente
                Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                        Dbc.Open()
                        Using Rs = Cmd.ExecuteReader
                            If Rs.Read Then
                                miei_dati = New DatiStampaAttoNotorio
                                With miei_dati
                                    .sottoscritto = Rs("Nominativo") & "" ' & " " & Rs("COGNOME")
                                    .natoil = Rs("Data_Nascita") & ""
                                    If (Rs("Luogo_Nascita") & "") <> "" Then
                                        .natoa = Rs("luogo_nascita") & ""
                                    ElseIf (Rs("comune_nascita_ee") & "") <> "" Then
                                        .natoa = Rs("comune_nascita_ee") & ""
                                    Else
                                        .natoa = Rs("luogo_nascita_old") & ""
                                    End If
                                    'If (Rs("Nazione_Nascita") & "") <> "" Then
                                    '    .natoa += " (" & Rs("Nazione_Nascita") & ")"
                                    'End If

                                    Dim comune As String = ""
                                    If Not (Rs("id_comune_ares") Is DBNull.Value) Then
                                        comune = getComuneAres(Rs("id_comune_ares")) & ""
                                    End If
                                    If comune = "" Then
                                        .residentea = Rs("City")
                                        If Not (Rs("PROVINCIA") Is DBNull.Value) Then
                                            .residentea += " (" & Rs("PROVINCIA") & ")"
                                        End If
                                    Else
                                        .residentea = comune
                                    End If

                                    .indirizzo = Rs("Indirizzo") & ""
                                    .cap = Rs("Cap") & ""
                                End With
                            Else
                                Libreria.genUserMsgBox(Page, "Primo conducente non trovato.")
                            End If
                        End Using
                    End Using
                End Using

            Case tipo_documento.MovimentoInterno, tipo_documento.ODL, tipo_documento.DuranteODL
                Dim sqlStr As String = "SELECT * FROM drivers WITH(NOLOCK) WHERE id = " & mio_record.id_primo_conducente
                Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                        Dbc.Open()
                        Using Rs = Cmd.ExecuteReader
                            If Rs.Read Then
                                miei_dati = New DatiStampaAttoNotorio
                                With miei_dati
                                    .sottoscritto = Rs("cognome") & " " & Rs("nome")
                                    .natoil = Rs("data_nascita") & ""
                                    .natoa = Rs("citta_nascita") & ""
                                    'If (Rs("Nazione_Nascita") & "") <> "" Then
                                    '    .natoa += " (" & Rs("Nazione_Nascita") & ")"
                                    'End If

                                    Dim comune As String = ""
                                    If Not (Rs("id_comune_ares") Is DBNull.Value) Then
                                        comune = getComuneAres(Rs("id_comune_ares")) & ""
                                    End If
                                    If comune = "" Then
                                        .residentea = Rs("citta")
                                        If Not (Rs("provincia") Is DBNull.Value) Then
                                            .residentea += " (" & Rs("provincia") & ")"
                                        End If
                                    Else
                                        .residentea = comune
                                    End If

                                    .indirizzo = Rs("indirizzo") & ""
                                    .cap = Rs("cap") & ""
                                End With
                            Else
                                Libreria.genUserMsgBox(Page, "conducente non trovato.")
                            End If
                        End Using
                    End Using
                End Using

            Case Else
                Err.Raise(1001, Nothing, "Tipo documento non previsto")

        End Select

        If miei_dati Is Nothing Then
            miei_dati = New DatiStampaAttoNotorio
            With miei_dati
                .sottoscritto = ""
                .natoil = ""
                .natoa = ""
                .residentea = ""
                .indirizzo = ""
                .cap = ""
            End With
        End If

        Session("DatiStampaAttoNotorio") = miei_dati

        Dim Generator As System.Random = New System.Random()

        Dim num_random As String = Format(Generator.Next(100000000), "000000000")
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraAttoNotorio.aspx?a=" & num_random & "','')", True)
        End If
    End Sub

    Protected Function getMarcaModelloAuto(ByVal id_veicolo As Integer) As String
        Dim sqlStr As String = "select m.descrizione from veicoli v WITH(NOLOCK)" &
            " INNER JOIN MODELLI m WITH(NOLOCK) ON v.id_modello = m.ID_MODELLO" &
            " LEFT JOIN marche ma WITH(NOLOCK) ON m.ID_CasaAutomobilistica = ma.id" &
            " WHERE v.id = " & id_veicolo

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getMarcaModelloAuto = Cmd.ExecuteScalar() & ""
            End Using
        End Using
    End Function
    Protected Function get_posizione_danno(ByVal id_gruppo_danni_uscita As String, ByVal id_danno As String) As String
        Dim sql As String = " SELECT CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END indice," &
            "gd.id_danno, gd.id_evento_apertura, gd.id_posizione_danno, pd.descrizione des_id_posizione_danno, " &
           " gd.id_tipo_danno, td.descrizione des_id_tipo_danno, gd.entita_danno " &
           " FROM veicoli_gruppo_evento ge WITH(NOLOCK) " &
            "INNER JOIN veicoli_gruppo_danni gd WITH(NOLOCK) ON ge.id = gd.id_evento_apertura AND gd.tipo_record = 1 AND ge.id = '" & id_gruppo_danni_uscita & "'" &
            "LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = gd.id_tipo_danno" &
           " LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = gd.id_posizione_danno" &
           " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON ge.id_modello = am.id_modello" &
            " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello,1) AND im.tipo_img = 1 AND gd.id_posizione_danno = im.id_posizione_danno " &
            " ORDER BY CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END"


        Dim Dbc3 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc3.Open()
        Dim Cmd3 As New Data.SqlClient.SqlCommand(sql, Dbc3)
        Dim Rs3 As Data.SqlClient.SqlDataReader
        Rs3 = Cmd3.ExecuteReader()
        Dim i As Integer = 1
        get_posizione_danno = ","
        Do While Rs3.Read

            get_posizione_danno = get_posizione_danno & Rs3("indice") & ","

        Loop
        Rs3.Close()
        Rs3 = Nothing
        Cmd3.Dispose()
        Cmd3 = Nothing
        Dbc3.Close()
        Dbc3.Dispose()
        Dbc3 = Nothing

    End Function

    Protected Sub Stampa_Check_Out_Privata(ByVal mio_record As DatiContratto)
        Dim sqlStr As String

        Dim miei_dati As DatiStampaCheck = New DatiStampaCheck
        With miei_dati

            Select Case mio_record.tipo_documento_origine
                Case tipo_documento.Contratto
                    .TRASFERIMENTO_AUTO = ""
                    .CONTRATTO = mio_record.num_contratto
                Case tipo_documento.MovimentoInterno
                    .TRASFERIMENTO_AUTO = mio_record.num_contratto
                    .CONTRATTO = ""
                Case tipo_documento.ODL
                    .TRASFERIMENTO_AUTO = "ODL " & mio_record.num_contratto
                    .CONTRATTO = ""
                Case tipo_documento.DuranteODL
                    .TRASFERIMENTO_AUTO = "DODL " & mio_record.num_contratto
                    .CONTRATTO = ""
                Case Is >= tipo_documento.RDSGenerico
                    .TRASFERIMENTO_AUTO = "RDS " & mio_record.num_contratto
                    .CONTRATTO = ""
                Case Else
                    .TRASFERIMENTO_AUTO = ""
                    .CONTRATTO = mio_record.num_contratto
            End Select

            .PARCHEGGIO = ""
            .MARCAMODELLO = getMarcaModelloAuto(mio_record.id_veicolo)
            .TARGA = mio_record.targa
            .ID_VEICOLO = mio_record.id_veicolo
            .ID_GRUPPO_EVENTO = mio_record.id_gruppo_danni_uscita
            .TIPO_STAMPA = "check_out"


            'aggiunto 22.07.2022 salvo
            If IsNothing(mio_record.data_rientro_previsto) Then
                .DATAEORA_OUT_PREVISTA = ""
            Else
                .DATAEORA_OUT_PREVISTA = mio_record.data_rientro_previsto
            End If


            'HttpContext.Current.Trace.Write("GRUPPO DANNI RIENTRO -------------------- " & mio_record.id_gruppo_danni_rientro)

            If mio_record.id_gruppo_danni_rientro Is Nothing Then
                .gruppo_danni_rientro = ""
            Else
                .gruppo_danni_rientro = mio_record.id_gruppo_danni_rientro
            End If

            .gruppo_danni_uscita = mio_record.id_gruppo_danni_uscita




            Dim dimensione_serbatoio As Integer = getSerbatoioDaIdVeicolo(mio_record.id_veicolo)

            .KM_OUT = mio_record.km_uscita & ""
            If mio_record.data_uscita IsNot Nothing Then
                .DATAEORA_OUT = Format(mio_record.data_uscita, "dd/MM/yyyy HH:mm")
            End If
            .CARBURANTE_OUT = mio_record.litri_uscita & "/" & dimensione_serbatoio
            .NOTE_OUT = ""

            .DANNI_OUT_1 = ""
            .DANNI_OUT_2 = ""
            .DANNI_OUT_3 = ""

            .DANNI_IN_1 = ""
            .DANNI_IN_2 = ""
            .DANNI_IN_3 = ""

            .ACCESSORI_OUT_1 = ""
            .ACCESSORI_OUT_2 = ""
            .ACCESSORI_OUT_3 = ""

            .ACCESSORI_IN_1 = ""
            .ACCESSORI_IN_2 = ""
            .ACCESSORI_IN_3 = ""

            Dim NumRecord As Integer = 0


            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                If mio_record.id_gruppo_danni_rientro Is Nothing Then
                    ' danni

                    'sqlStr = "SELECT gd.id_posizione_danno, pd.descrizione des_id_posizione_danno, td.descrizione des_id_tipo_danno, gd.entita_danno, gd.descrizione descrizione_danno, gd.tipo_record, gd.id_danno " & _
                    '   " FROM veicoli_gruppo_evento ge WITH(NOLOCK) " & _
                    '   " INNER JOIN veicoli_gruppo_danni gd WITH(NOLOCK) ON ge.id = gd.id_evento_apertura AND gd.tipo_record IN (1,2,3) AND ge.id = @id_gruppo_evento" & _
                    '   " LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = gd.id_tipo_danno" & _
                    '   " LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = gd.id_posizione_danno" & _
                    '   " ORDER BY gd.tipo_record"

                    sqlStr = "SELECT gd.id_posizione_danno, pd.descrizione des_id_posizione_danno, td.descrizione des_id_tipo_danno, gd.entita_danno, gd.descrizione descrizione_danno, gd.tipo_record, gd.id_danno," &
                        " CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END indice " &
                        " FROM veicoli_gruppo_evento ge WITH(NOLOCK) " &
                        " INNER JOIN veicoli_gruppo_danni gd WITH(NOLOCK) ON ge.id = gd.id_evento_apertura AND gd.tipo_record IN (1,2,3) AND ge.id = @id_gruppo_evento" &
                        " LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = gd.id_tipo_danno " &
                        " LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = gd.id_posizione_danno " &
                        " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON ge.id_modello = am.id_modello " &
                        " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello," & lb_immagine_default.Text & ") AND im.tipo_img = 1 AND gd.id_posizione_danno = im.id_posizione_danno " &
                        " ORDER BY gd.tipo_record, CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END "

                    'HttpContext.Current.Trace.Write(" DANNI ---------------------- " & mio_record.id_gruppo_danni_uscita & " " & sqlStr)

                    Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                        Cmd.Parameters.Add("@id_gruppo_evento", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_uscita

                        Dim i As Integer = 0
                        Dim k As Integer = 1
                        Dim j As Integer
                        Dim pos_danno As String = ""
                        Using Rs = Cmd.ExecuteReader
                            Do While Rs.Read

                                Dim id_tipo_record As tipo_record_danni = Rs("tipo_record")

                                Dim danno As String = ""
                                Select Case id_tipo_record
                                    Case tipo_record_danni.Danno_Carrozzeria
                                        Dim id_entita_danno As Entita_Danno = Rs("entita_danno")
                                        danno = Rs("indice") & " - " & Rs("des_id_posizione_danno") & " " & Rs("des_id_tipo_danno") & " " & id_entita_danno.ToString.ToLower & vbCrLf
                                    Case tipo_record_danni.Danno_Elettrico, tipo_record_danni.Danno_Meccanico
                                        danno = Rs("descrizione_danno") & ""
                                        If danno.Length > 30 Then
                                            danno = danno.Substring(0, 30) & "..."
                                        End If
                                        danno = (id_tipo_record.ToString).Replace("_", " ") & " " & danno & vbCrLf
                                End Select


                                i += 1
                                'If pos_danno <> "" Then
                                '    If Rs("id_posizione_danno") & "" <> pos_danno Then
                                '        k = k + 1
                                '    End If
                                'End If
                                Dim id_danno As String = Rs("id_danno") & ""
                                Dim num_danno() As String = Split(get_posizione_danno(mio_record.id_gruppo_danni_uscita, id_danno), ",")
                                k = k + 1
                                j = i Mod 3
                                Select Case j
                                    Case 1
                                        .DANNI_OUT_1 += danno
                                        .DANNI_IN_1 += danno
                                    Case 2
                                        .DANNI_OUT_2 += danno
                                        .DANNI_IN_2 += danno
                                    Case Else
                                        .DANNI_OUT_3 += danno
                                        .DANNI_IN_3 += danno
                                End Select

                                '  pos_danno = Rs("id_posizione_danno") & ""
                            Loop
                        End Using
                    End Using

                Else
                    sqlStr = "SELECT pd.descrizione des_id_posizione_danno, td.descrizione des_id_tipo_danno, gd.entita_danno, gd.descrizione descrizione_danno, gd.tipo_record, ISNULL(gd2.id,0) GiaPresente, CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END indice " &
                    " FROM veicoli_gruppo_danni gd WITH(NOLOCK) " &
                    " LEFT JOIN veicoli_gruppo_evento ge WITH(NOLOCK) ON gd.id_evento_apertura=ge.id " &
                    " LEFT JOIN veicoli_gruppo_danni gd2 WITH(NOLOCK) ON gd.id_danno = gd2.id_danno AND gd2.tipo_record IN (1,2,3) AND gd2.id_evento_apertura= @id_gruppo_danni_uscita" &
                    " LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = gd.id_tipo_danno" &
                    " LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = gd.id_posizione_danno" &
                    " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON ge.id_modello = am.id_modello " &
                    " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello," & lb_immagine_default.Text & ") AND im.tipo_img = 1 AND gd.id_posizione_danno = im.id_posizione_danno " &
                    " WHERE gd.tipo_record IN (1,2,3) AND gd.id_evento_apertura= @id_gruppo_danni_rientro" &
                    " ORDER BY    gd.tipo_record, CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END, gd2.id DESC "

                    'Trace.Write(sqlStr)
                    'Trace.Write("SET @id_gruppo_danni_rientro = " & mio_record.id_gruppo_danni_rientro & ";")
                    'Trace.Write("SET @id_gruppo_danni_uscita = " & mio_record.id_gruppo_danni_uscita & ";")

                    Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                        Cmd.Parameters.Add("@id_gruppo_danni_rientro", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_rientro
                        Cmd.Parameters.Add("@id_gruppo_danni_uscita", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_uscita

                        Dim i As Integer = 1
                        Dim j As Integer
                        Using Rs = Cmd.ExecuteReader
                            Do While Rs.Read

                                Dim GiaPresente As Integer = Rs("GiaPresente")
                                Dim id_tipo_record As tipo_record_danni = Rs("tipo_record")
                                Dim danno As String = ""
                                Select Case id_tipo_record
                                    Case tipo_record_danni.Danno_Carrozzeria
                                        Dim id_entita_danno As Entita_Danno = Rs("entita_danno")
                                        danno = Rs("indice") & " - " & Rs("des_id_posizione_danno") & " " & Rs("des_id_tipo_danno") & " " & id_entita_danno.ToString.ToLower & vbCrLf
                                    Case tipo_record_danni.Danno_Elettrico, tipo_record_danni.Danno_Meccanico
                                        danno = Rs("descrizione_danno") & ""
                                        If danno.Length > 30 Then
                                            danno = danno.Substring(0, 30) & "..."
                                        End If
                                        danno = (id_tipo_record.ToString).Replace("_", " ") & " " & danno & vbCrLf
                                End Select



                                j = i Mod 3
                                Select Case j
                                    Case 1
                                        If GiaPresente > 0 Then
                                            .DANNI_OUT_1 += danno
                                            .DANNI_IN_1 += danno

                                            i += 1
                                        End If

                                    Case 2
                                        If GiaPresente > 0 Then
                                            .DANNI_OUT_2 += danno
                                            .DANNI_IN_2 += danno

                                            i += 1
                                        End If

                                    Case Else
                                        If GiaPresente > 0 Then
                                            .DANNI_OUT_3 += danno
                                            .DANNI_IN_3 += danno

                                            i += 1
                                        End If

                                End Select
                            Loop
                        End Using
                    End Using
                End If

                ' Dotazioni (simile query sql_dotazioni)
                sqlStr = "SELECT a.descrizione, a.descrizione_ing" &
                    " FROM veicoli_gruppo_accessori ga WITH(NOLOCK) " &
                    " INNER JOIN accessori a WITH(NOLOCK) ON a.id = ga.id_accessorio" &
                    " WHERE ga.id_evento_apertura = @id_evento_apertura" &
                    " AND ga.assente = 0" &
                    " ORDER BY a.descrizione"

                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.Parameters.Add("@id_evento_apertura", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_uscita

                    Dim i As Integer = 0
                    Dim j As Integer = 0
                    Using Rs = Cmd.ExecuteReader
                        Do While Rs.Read
                            Dim danno As String = Rs("descrizione") & " (" & Rs("descrizione_ing") & ")" & vbCrLf

                            i += 1
                            j = i Mod 3
                            Trace.Write(i & " D " & j)
                            Select Case j
                                Case 1
                                    .ACCESSORI_OUT_1 += danno
                                    .ACCESSORI_IN_1 += "[ ]" & danno
                                Case 2
                                    .ACCESSORI_OUT_2 += danno
                                    .ACCESSORI_IN_2 += "[ ]" & danno
                                Case Else
                                    .ACCESSORI_OUT_3 += danno
                                    .ACCESSORI_IN_3 += "[ ]" & danno
                            End Select
                        Loop
                    End Using
                End Using

                ' Accessori
                sqlStr = "SELECT ce.descrizione" &
                    " FROM veicoli_gruppo_accessori_contratto ga WITH(NOLOCK) " &
                    " INNER JOIN  condizioni_elementi ce WITH(NOLOCK) ON ga.id_accessorio = ce.id " &
                    " WHERE ga.id_evento_apertura = @id_gruppo_evento" &
                    " ORDER BY ce.descrizione"

                'sqlStr = "SELECT condizioni_elementi.id, condizioni_elementi.descrizione " & _
                '    " FROM contratti_costi " & _
                '    " INNER JOIN contratti ON contratti.id=contratti_costi.id_documento " & _
                '    " INNER JOIN  condizioni_elementi ON contratti_costi.id_elemento = condizioni_elementi.id " & _
                '    " WHERE (contratti.num_contratto = @id_documento AND attivo='1') " & _
                '    " AND (contratti_costi.id_a_carico_di = @id_a_carico_del_cliente) " & _
                '    " AND (contratti_costi.obbligatorio = '0') " & _
                '    " AND (contratti_costi.selezionato = '1')" & _
                '    " AND (condizioni_elementi.accessorio_check = '1')"

                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    'Cmd.Parameters.Add("@id_a_carico_del_cliente", System.Data.SqlDbType.Int).Value = Costanti.id_a_carico_del_cliente
                    'Cmd.Parameters.Add("@id_documento", System.Data.SqlDbType.Int).Value = mio_record.num_contratto
                    Cmd.Parameters.Add("@id_gruppo_evento", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_uscita

                    Dim i As Integer = 0
                    Dim j As Integer = 0
                    Using Rs = Cmd.ExecuteReader
                        Do While Rs.Read

                            Dim danno As String = Rs("descrizione") & vbCrLf

                            i += 1
                            j = i Mod 3
                            Trace.Write(i & " A " & j)

                            Select Case j
                                Case 1
                                    .ACCESSORI_OUT_1 += danno
                                    .ACCESSORI_IN_1 += "[ ]" & danno
                                Case 2
                                    .ACCESSORI_OUT_2 += danno
                                    .ACCESSORI_IN_2 += "[ ]" & danno
                                Case Else
                                    .ACCESSORI_OUT_3 += danno
                                    .ACCESSORI_IN_3 += "[ ]" & danno
                            End Select
                        Loop
                    End Using
                End Using

            End Using
        End With

        Session("DatiStampaCheck") = miei_dati

        Dim Generator As System.Random = New System.Random()

        Dim num_random As String = Format(Generator.Next(100000000), "000000000") 'System.Guid.NewGuid().ToString

        If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraStampaCheck.aspx?a=" & num_random & "','')", True)
        End If

    End Sub

    ' metodo per consentire la stampa anche da moduli esterni!!!
    Public Sub Stampa_Check_Out(ByVal id_tipo_documento As tipo_documento, ByVal id_documento As Integer, ByVal numero_crv As Integer)
        Dim mio_record As DatiContratto = Nothing ' per  adesso così... in seguito dichiaro una interfaccia!

        Select Case id_tipo_documento
            Case tipo_documento.Contratto
                mio_record = DatiContratto.getRecordDaNumContratto(id_documento, numero_crv)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: contratto non trovato.")
                End If

            Case tipo_documento.MovimentoInterno
                mio_record = DatiContratto.getRecordDaNumTrasferimento(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore:  movimento interno non trovato.")
                End If

            Case tipo_documento.ODL
                mio_record = DatiContratto.getRecordDaNumODL(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                End If

            Case tipo_documento.DuranteODL ' non so se serve!!!!!!!!!
                mio_record = DatiContratto.getRecordDaNumDuranteODL(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                End If

            Case Is >= tipo_documento.RDSGenerico
                mio_record = DatiContratto.getRecordDaRDSGenerico(id_tipo_documento, id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: RDS Generico non trovato.")
                End If

            Case Else
                Err.Raise(1001, Nothing, "Tipo documento non previsto")
        End Select

        Stampa_Check_Out_Privata(mio_record)
    End Sub


    Protected Sub Stampa_Check_In_Privata(ByVal mio_record As DatiContratto, Optional tipostampa As String = "s") 'modificato da protected a public 01.06.2022 salvo
        Dim sqlStr As String

        mio_record.MioTrace()

        Dim miei_dati As DatiStampaCheck = New DatiStampaCheck

        With miei_dati

            Select Case mio_record.tipo_documento_origine
                Case tipo_documento.Contratto
                    .TRASFERIMENTO_AUTO = ""
                    .CONTRATTO = mio_record.num_contratto
                Case tipo_documento.MovimentoInterno
                    .TRASFERIMENTO_AUTO = mio_record.num_contratto
                    .CONTRATTO = ""
                Case tipo_documento.ODL
                    .TRASFERIMENTO_AUTO = "ODL " & mio_record.num_contratto
                    .CONTRATTO = ""
                Case tipo_documento.DuranteODL
                    .TRASFERIMENTO_AUTO = "DODL " & mio_record.num_contratto
                    .CONTRATTO = ""
                Case Is >= tipo_documento.RDSGenerico
                    .TRASFERIMENTO_AUTO = "RDS " & mio_record.num_contratto
                    .CONTRATTO = ""
                Case Else
                    .TRASFERIMENTO_AUTO = ""
                    .CONTRATTO = mio_record.num_contratto
            End Select

            .DATAEORA_OUT_PREVISTA = mio_record.data_rientro_previsto       'aggiunto 21.06.2022 salvo


            .PARCHEGGIO = ""
            .MARCAMODELLO = getMarcaModelloAuto(mio_record.id_veicolo) & ""
            .TARGA = mio_record.targa
            .ID_VEICOLO = mio_record.id_veicolo
            .ID_GRUPPO_EVENTO = mio_record.id_gruppo_danni_uscita
            .TIPO_STAMPA = "check_in"
            .gruppo_danni_rientro = mio_record.id_gruppo_danni_rientro
            .gruppo_danni_uscita = mio_record.id_gruppo_danni_uscita

            Dim dimensione_serbatoio As Integer = getSerbatoioDaIdVeicolo(mio_record.id_veicolo)

            .KM_OUT = mio_record.km_uscita & ""
            If mio_record.data_uscita IsNot Nothing Then
                .DATAEORA_OUT = Format(mio_record.data_uscita, "dd/MM/yyyy HH:mm")
            End If
            .CARBURANTE_OUT = mio_record.litri_uscita & "/" & dimensione_serbatoio
            .NOTE_OUT = ""

            .KM_IN = mio_record.km_rientro & ""
            If mio_record.data_rientro IsNot Nothing Then
                .DATAEORA_IN = Format(mio_record.data_rientro, "dd/MM/yyyy HH:mm")
            End If
            .CARBURANTE_IN = mio_record.litri_rientro & "/" & dimensione_serbatoio
            .NOTE_IN = ""

            .DANNI_OUT_1 = ""
            .DANNI_OUT_2 = ""
            .DANNI_OUT_3 = ""

            .DANNI_IN_1 = ""
            .DANNI_IN_2 = ""
            .DANNI_IN_3 = ""

            .ACCESSORI_OUT_1 = ""
            .ACCESSORI_OUT_2 = ""
            .ACCESSORI_OUT_3 = ""

            .ACCESSORI_IN_1 = ""
            .ACCESSORI_IN_2 = ""
            .ACCESSORI_IN_3 = ""

            Dim NumRecord As Integer = 0
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                ' danni
                'sqlStr = "SELECT pd.descrizione des_id_posizione_danno, td.descrizione des_id_tipo_danno, gd.entita_danno, gd.descrizione descrizione_danno, gd.tipo_record, ISNULL(gd2.id,0) GiaPresente" & _
                '    " FROM veicoli_gruppo_danni gd WITH(NOLOCK) " & _
                '    " LEFT JOIN veicoli_gruppo_danni gd2 WITH(NOLOCK) ON gd.id_danno = gd2.id_danno AND gd2.tipo_record IN (1,2,3) AND gd2.id_evento_apertura= @id_gruppo_danni_uscita" & _
                '    " LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = gd.id_tipo_danno" & _
                '    " LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = gd.id_posizione_danno" & _
                '    " WHERE gd.tipo_record IN (1,2,3) AND gd.id_evento_apertura= @id_gruppo_danni_rientro" & _
                '    " ORDER BY gd2.id DESC, gd.tipo_record"

                'sqlStr = "SELECT gd.id_posizione_danno, pd.descrizione des_id_posizione_danno, td.descrizione des_id_tipo_danno, gd.entita_danno, gd.descrizione descrizione_danno, gd.tipo_record, gd.id_danno," & _
                '   " CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END indice " & _
                '   " FROM veicoli_gruppo_evento ge WITH(NOLOCK) " & _
                '   " INNER JOIN veicoli_gruppo_danni gd WITH(NOLOCK) ON ge.id = gd.id_evento_apertura AND gd.tipo_record IN (1,2,3) AND ge.id = @id_gruppo_evento " & _
                '   " LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = gd.id_tipo_danno " & _
                '   " LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = gd.id_posizione_danno " & _
                '   " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON ge.id_modello = am.id_modello " & _
                '   " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello," & lb_immagine_default.Text & ") AND im.tipo_img = 1 AND gd.id_posizione_danno = im.id_posizione_danno " & _
                '   " ORDER BY gd.tipo_record "

                'Trace.Write(sqlStr)


                Dim lblimg As String = "1"
                If Not IsNothing(lb_immagine_default) Then     'se richiamata dal pannello check-IN
                    lblimg = lb_immagine_default.Text
                End If


                sqlStr = "SELECT pd.descrizione des_id_posizione_danno, td.descrizione des_id_tipo_danno, gd.entita_danno, gd.descrizione descrizione_danno, gd.tipo_record, ISNULL(gd2.id,0) GiaPresente, CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END indice " &
                    " FROM veicoli_gruppo_danni gd WITH(NOLOCK) " &
                    " LEFT JOIN veicoli_gruppo_evento ge WITH(NOLOCK) ON gd.id_evento_apertura=ge.id " &
                    " LEFT JOIN veicoli_gruppo_danni gd2 WITH(NOLOCK) ON gd.id_danno = gd2.id_danno AND gd2.tipo_record IN (1,2,3) AND gd2.id_evento_apertura= @id_gruppo_danni_uscita" &
                    " LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = gd.id_tipo_danno" &
                    " LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = gd.id_posizione_danno" &
                    " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON ge.id_modello = am.id_modello " &
                    " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello," & lblimg & ") AND im.tipo_img = 1 AND gd.id_posizione_danno = im.id_posizione_danno " &
                    " WHERE gd.tipo_record IN (1,2,3) AND gd.id_evento_apertura= @id_gruppo_danni_rientro" &
                    " ORDER BY gd.tipo_record, CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END, gd2.id DESC"

                'Trace.Write(sqlStr)
                'Trace.Write("SET @id_gruppo_danni_rientro = " & mio_record.id_gruppo_danni_rientro & ";")
                'Trace.Write("SET @id_gruppo_danni_uscita = " & mio_record.id_gruppo_danni_uscita & ";")

                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.Parameters.Add("@id_gruppo_danni_rientro", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_rientro
                    Cmd.Parameters.Add("@id_gruppo_danni_uscita", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_uscita

                    Dim i As Integer = 0
                    Dim j As Integer
                    Using Rs = Cmd.ExecuteReader
                        Do While Rs.Read

                            Dim GiaPresente As Integer = Rs("GiaPresente")
                            Dim id_tipo_record As tipo_record_danni = Rs("tipo_record")
                            Dim danno As String = ""
                            Select Case id_tipo_record
                                Case tipo_record_danni.Danno_Carrozzeria
                                    Dim id_entita_danno As Entita_Danno = Rs("entita_danno")
                                    danno = Rs("indice") & " - " & Rs("des_id_posizione_danno") & " " & Rs("des_id_tipo_danno") & " " & id_entita_danno.ToString.ToLower & vbCrLf
                                Case tipo_record_danni.Danno_Elettrico, tipo_record_danni.Danno_Meccanico
                                    danno = Rs("descrizione_danno") & ""
                                    If danno.Length > 30 Then
                                        danno = danno.Substring(0, 30) & "..."
                                    End If
                                    danno = (id_tipo_record.ToString).Replace("_", " ") & " " & danno & vbCrLf
                            End Select


                            i += 1
                            j = i Mod 3
                            Select Case j
                                Case 1
                                    If GiaPresente > 0 Then
                                        .DANNI_OUT_1 += danno
                                    End If
                                    .DANNI_IN_1 += danno
                                Case 2
                                    If GiaPresente > 0 Then
                                        .DANNI_OUT_2 += danno
                                    End If
                                    .DANNI_IN_2 += danno
                                Case Else
                                    If GiaPresente > 0 Then
                                        .DANNI_OUT_3 += danno
                                    End If
                                    .DANNI_IN_3 += danno
                            End Select
                        Loop
                    End Using
                End Using

                ' Dotazioni (simile query sql_dotazioni)
                sqlStr = "SELECT a.descrizione, a.descrizione_ing, ga2.assente" &
                    " FROM veicoli_gruppo_accessori ga WITH(NOLOCK)" &
                    " INNER JOIN veicoli_gruppo_accessori ga2 WITH(NOLOCK) ON ga2.id_evento_apertura = @id_gruppo_danni_rientro AND ga2.id_accessorio = ga.id_accessorio" &
                    " INNER JOIN accessori a WITH(NOLOCK) ON a.id = ga.id_accessorio" &
                    " WHERE ga.id_evento_apertura = @id_gruppo_danni_uscita" &
                    " AND ga.assente = 0" &
                    " ORDER BY a.descrizione"

                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.Parameters.Add("@id_gruppo_danni_rientro", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_rientro
                    Cmd.Parameters.Add("@id_gruppo_danni_uscita", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_uscita

                    Dim i As Integer = 0
                    Dim j As Integer = 0
                    Using Rs = Cmd.ExecuteReader
                        Do While Rs.Read
                            Dim assente As Boolean = Rs("assente")
                            Dim danno As String = Rs("descrizione") & " (" & Rs("descrizione_ing") & ")" & vbCrLf

                            i += 1
                            j = i Mod 3
                            Select Case j
                                Case 1
                                    .ACCESSORI_OUT_1 += danno
                                    If assente Then
                                        .ACCESSORI_IN_1 += "[X]" & danno
                                    Else
                                        .ACCESSORI_IN_1 += "[ ]" & danno
                                    End If
                                Case 2
                                    .ACCESSORI_OUT_2 += danno
                                    If assente Then
                                        .ACCESSORI_IN_2 += "[X]" & danno
                                    Else
                                        .ACCESSORI_IN_2 += "[ ]" & danno
                                    End If
                                Case Else
                                    .ACCESSORI_OUT_3 += danno
                                    If assente Then
                                        .ACCESSORI_IN_3 += "[X]" & danno
                                    Else
                                        .ACCESSORI_IN_3 += "[ ]" & danno
                                    End If
                            End Select
                        Loop
                    End Using
                End Using

                ' Accessori
                sqlStr = "SELECT ce.descrizione, ga2.assente" &
                    " FROM veicoli_gruppo_accessori_contratto ga WITH(NOLOCK) " &
                    " INNER JOIN veicoli_gruppo_accessori_contratto ga2 WITH(NOLOCK) ON ga2.id_evento_apertura = @id_gruppo_danni_rientro AND ga2.id_accessorio = ga.id_accessorio" &
                    " INNER JOIN  condizioni_elementi ce WITH(NOLOCK) ON ga.id_accessorio = ce.id " &
                    " WHERE ga.id_evento_apertura = @id_gruppo_danni_uscita" &
                    " ORDER BY ce.descrizione"

                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.Parameters.Add("@id_gruppo_danni_rientro", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_rientro
                    Cmd.Parameters.Add("@id_gruppo_danni_uscita", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_uscita

                    Dim i As Integer = 0
                    Dim j As Integer = 0
                    Using Rs = Cmd.ExecuteReader
                        Do While Rs.Read
                            Dim assente As Boolean = Rs("assente")
                            Dim danno As String = Rs("descrizione") & vbCrLf

                            i += 1
                            j = i Mod 3
                            Select Case j
                                Case 1
                                    .ACCESSORI_OUT_1 += danno
                                    If assente Then
                                        .ACCESSORI_IN_1 += "[X]" & danno
                                    Else
                                        .ACCESSORI_IN_1 += "[ ]" & danno
                                    End If
                                Case 2
                                    .ACCESSORI_OUT_2 += danno
                                    If assente Then
                                        .ACCESSORI_IN_2 += "[X]" & danno
                                    Else
                                        .ACCESSORI_IN_2 += "[ ]" & danno
                                    End If
                                Case Else
                                    .ACCESSORI_OUT_3 += danno
                                    If assente Then
                                        .ACCESSORI_IN_3 += "[X]" & danno
                                    Else
                                        .ACCESSORI_IN_3 += "[ ]" & danno
                                    End If
                            End Select
                        Loop
                    End Using
                End Using

            End Using
        End With




        If tipostampa = "f" Then ' se f crea il PDF (riempimento session per creazione pdf del checkIN 01.06.2022

            Exit Sub


        Else

            Session("DatiStampaCheck") = miei_dati

            Dim Generator As System.Random = New System.Random()

            Dim num_random As String = Format(Generator.Next(100000000), "000000000") 'System.Guid.NewGuid().ToString

            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraStampaCheck.aspx?a=" & num_random & "','')", True)
            End If
        End If



    End Sub


    Protected Function Stampa_Check_In_Privata_PDF(ByVal mio_record As DatiContratto, Optional tipostampa As String = "s") As DatiStampaCheck

        Dim sqlStr As String

        mio_record.MioTrace()

        Dim miei_dati As DatiStampaCheck = New DatiStampaCheck

        With miei_dati

            Select Case mio_record.tipo_documento_origine
                Case tipo_documento.Contratto
                    .TRASFERIMENTO_AUTO = ""
                    .CONTRATTO = mio_record.num_contratto
                Case tipo_documento.MovimentoInterno
                    .TRASFERIMENTO_AUTO = mio_record.num_contratto
                    .CONTRATTO = ""
                Case tipo_documento.ODL
                    .TRASFERIMENTO_AUTO = "ODL " & mio_record.num_contratto
                    .CONTRATTO = ""
                Case tipo_documento.DuranteODL
                    .TRASFERIMENTO_AUTO = "DODL " & mio_record.num_contratto
                    .CONTRATTO = ""
                Case Is >= tipo_documento.RDSGenerico
                    .TRASFERIMENTO_AUTO = "RDS " & mio_record.num_contratto
                    .CONTRATTO = ""
                Case Else
                    .TRASFERIMENTO_AUTO = ""
                    .CONTRATTO = mio_record.num_contratto
            End Select

            .PARCHEGGIO = ""
            .MARCAMODELLO = getMarcaModelloAuto(mio_record.id_veicolo) & ""
            .TARGA = mio_record.targa
            .ID_VEICOLO = mio_record.id_veicolo
            .ID_GRUPPO_EVENTO = mio_record.id_gruppo_danni_uscita
            .TIPO_STAMPA = "check_in"
            .gruppo_danni_rientro = mio_record.id_gruppo_danni_rientro
            .gruppo_danni_uscita = mio_record.id_gruppo_danni_uscita

            Dim dimensione_serbatoio As Integer = getSerbatoioDaIdVeicolo(mio_record.id_veicolo)

            .KM_OUT = mio_record.km_uscita & ""
            If mio_record.data_uscita IsNot Nothing Then
                .DATAEORA_OUT = Format(mio_record.data_uscita, "dd/MM/yyyy HH:mm")
            End If
            .CARBURANTE_OUT = mio_record.litri_uscita & "/" & dimensione_serbatoio
            .NOTE_OUT = ""

            .KM_IN = mio_record.km_rientro & ""
            If mio_record.data_rientro IsNot Nothing Then
                .DATAEORA_IN = Format(mio_record.data_rientro, "dd/MM/yyyy HH:mm")
            End If
            .CARBURANTE_IN = mio_record.litri_rientro & "/" & dimensione_serbatoio
            .NOTE_IN = ""

            .DANNI_OUT_1 = ""
            .DANNI_OUT_2 = ""
            .DANNI_OUT_3 = ""

            .DANNI_IN_1 = ""
            .DANNI_IN_2 = ""
            .DANNI_IN_3 = ""

            .ACCESSORI_OUT_1 = ""
            .ACCESSORI_OUT_2 = ""
            .ACCESSORI_OUT_3 = ""

            .ACCESSORI_IN_1 = ""
            .ACCESSORI_IN_2 = ""
            .ACCESSORI_IN_3 = ""

            Dim NumRecord As Integer = 0
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                ' danni
                'sqlStr = "SELECT pd.descrizione des_id_posizione_danno, td.descrizione des_id_tipo_danno, gd.entita_danno, gd.descrizione descrizione_danno, gd.tipo_record, ISNULL(gd2.id,0) GiaPresente" & _
                '    " FROM veicoli_gruppo_danni gd WITH(NOLOCK) " & _
                '    " LEFT JOIN veicoli_gruppo_danni gd2 WITH(NOLOCK) ON gd.id_danno = gd2.id_danno AND gd2.tipo_record IN (1,2,3) AND gd2.id_evento_apertura= @id_gruppo_danni_uscita" & _
                '    " LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = gd.id_tipo_danno" & _
                '    " LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = gd.id_posizione_danno" & _
                '    " WHERE gd.tipo_record IN (1,2,3) AND gd.id_evento_apertura= @id_gruppo_danni_rientro" & _
                '    " ORDER BY gd2.id DESC, gd.tipo_record"

                'sqlStr = "SELECT gd.id_posizione_danno, pd.descrizione des_id_posizione_danno, td.descrizione des_id_tipo_danno, gd.entita_danno, gd.descrizione descrizione_danno, gd.tipo_record, gd.id_danno," & _
                '   " CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END indice " & _
                '   " FROM veicoli_gruppo_evento ge WITH(NOLOCK) " & _
                '   " INNER JOIN veicoli_gruppo_danni gd WITH(NOLOCK) ON ge.id = gd.id_evento_apertura AND gd.tipo_record IN (1,2,3) AND ge.id = @id_gruppo_evento " & _
                '   " LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = gd.id_tipo_danno " & _
                '   " LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = gd.id_posizione_danno " & _
                '   " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON ge.id_modello = am.id_modello " & _
                '   " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello," & lb_immagine_default.Text & ") AND im.tipo_img = 1 AND gd.id_posizione_danno = im.id_posizione_danno " & _
                '   " ORDER BY gd.tipo_record "

                'Trace.Write(sqlStr)


                Dim lblimg As String = "1"
                If Not IsNothing(lb_immagine_default) Then     'se richiamata dal pannello check-IN
                    lblimg = lb_immagine_default.Text
                End If


                sqlStr = "SELECT pd.descrizione des_id_posizione_danno, td.descrizione des_id_tipo_danno, gd.entita_danno, gd.descrizione descrizione_danno, gd.tipo_record, ISNULL(gd2.id,0) GiaPresente, CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END indice " &
                    " FROM veicoli_gruppo_danni gd WITH(NOLOCK) " &
                    " LEFT JOIN veicoli_gruppo_evento ge WITH(NOLOCK) ON gd.id_evento_apertura=ge.id " &
                    " LEFT JOIN veicoli_gruppo_danni gd2 WITH(NOLOCK) ON gd.id_danno = gd2.id_danno AND gd2.tipo_record IN (1,2,3) AND gd2.id_evento_apertura= @id_gruppo_danni_uscita" &
                    " LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = gd.id_tipo_danno" &
                    " LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = gd.id_posizione_danno" &
                    " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON ge.id_modello = am.id_modello " &
                    " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello," & lblimg & ") AND im.tipo_img = 1 AND gd.id_posizione_danno = im.id_posizione_danno " &
                    " WHERE gd.tipo_record IN (1,2,3) AND gd.id_evento_apertura= @id_gruppo_danni_rientro" &
                    " ORDER BY gd.tipo_record, CASE WHEN im.id_posizione_danno IS NULL THEN 0 ELSE DENSE_RANK() OVER (ORDER BY im.id_posizione_danno DESC) END, gd2.id DESC"

                'Trace.Write(sqlStr)
                'Trace.Write("SET @id_gruppo_danni_rientro = " & mio_record.id_gruppo_danni_rientro & ";")
                'Trace.Write("SET @id_gruppo_danni_uscita = " & mio_record.id_gruppo_danni_uscita & ";")

                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.Parameters.Add("@id_gruppo_danni_rientro", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_rientro
                    Cmd.Parameters.Add("@id_gruppo_danni_uscita", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_uscita

                    Dim i As Integer = 0
                    Dim j As Integer
                    Using Rs = Cmd.ExecuteReader
                        Do While Rs.Read

                            Dim GiaPresente As Integer = Rs("GiaPresente")
                            Dim id_tipo_record As tipo_record_danni = Rs("tipo_record")
                            Dim danno As String = ""
                            Select Case id_tipo_record
                                Case tipo_record_danni.Danno_Carrozzeria
                                    Dim id_entita_danno As Entita_Danno = Rs("entita_danno")
                                    danno = Rs("indice") & " - " & Rs("des_id_posizione_danno") & " " & Rs("des_id_tipo_danno") & " " & id_entita_danno.ToString.ToLower & vbCrLf
                                Case tipo_record_danni.Danno_Elettrico, tipo_record_danni.Danno_Meccanico
                                    danno = Rs("descrizione_danno") & ""
                                    If danno.Length > 30 Then
                                        danno = danno.Substring(0, 30) & "..."
                                    End If
                                    danno = (id_tipo_record.ToString).Replace("_", " ") & " " & danno & vbCrLf
                            End Select


                            i += 1
                            j = i Mod 3
                            Select Case j
                                Case 1
                                    If GiaPresente > 0 Then
                                        .DANNI_OUT_1 += danno
                                    End If
                                    .DANNI_IN_1 += danno
                                Case 2
                                    If GiaPresente > 0 Then
                                        .DANNI_OUT_2 += danno
                                    End If
                                    .DANNI_IN_2 += danno
                                Case Else
                                    If GiaPresente > 0 Then
                                        .DANNI_OUT_3 += danno
                                    End If
                                    .DANNI_IN_3 += danno
                            End Select
                        Loop
                    End Using
                End Using

                ' Dotazioni (simile query sql_dotazioni)
                sqlStr = "SELECT a.descrizione, a.descrizione_ing, ga2.assente" &
                    " FROM veicoli_gruppo_accessori ga WITH(NOLOCK)" &
                    " INNER JOIN veicoli_gruppo_accessori ga2 WITH(NOLOCK) ON ga2.id_evento_apertura = @id_gruppo_danni_rientro AND ga2.id_accessorio = ga.id_accessorio" &
                    " INNER JOIN accessori a WITH(NOLOCK) ON a.id = ga.id_accessorio" &
                    " WHERE ga.id_evento_apertura = @id_gruppo_danni_uscita" &
                    " AND ga.assente = 0" &
                    " ORDER BY a.descrizione"

                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.Parameters.Add("@id_gruppo_danni_rientro", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_rientro
                    Cmd.Parameters.Add("@id_gruppo_danni_uscita", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_uscita

                    Dim i As Integer = 0
                    Dim j As Integer = 0
                    Using Rs = Cmd.ExecuteReader
                        Do While Rs.Read
                            Dim assente As Boolean = Rs("assente")
                            Dim danno As String = Rs("descrizione") & " (" & Rs("descrizione_ing") & ")" & vbCrLf

                            i += 1
                            j = i Mod 3
                            Select Case j
                                Case 1
                                    .ACCESSORI_OUT_1 += danno
                                    If assente Then
                                        .ACCESSORI_IN_1 += "[X]" & danno
                                    Else
                                        .ACCESSORI_IN_1 += "[ ]" & danno
                                    End If
                                Case 2
                                    .ACCESSORI_OUT_2 += danno
                                    If assente Then
                                        .ACCESSORI_IN_2 += "[X]" & danno
                                    Else
                                        .ACCESSORI_IN_2 += "[ ]" & danno
                                    End If
                                Case Else
                                    .ACCESSORI_OUT_3 += danno
                                    If assente Then
                                        .ACCESSORI_IN_3 += "[X]" & danno
                                    Else
                                        .ACCESSORI_IN_3 += "[ ]" & danno
                                    End If
                            End Select
                        Loop
                    End Using
                End Using

                ' Accessori
                sqlStr = "SELECT ce.descrizione, ga2.assente" &
                    " FROM veicoli_gruppo_accessori_contratto ga WITH(NOLOCK) " &
                    " INNER JOIN veicoli_gruppo_accessori_contratto ga2 WITH(NOLOCK) ON ga2.id_evento_apertura = @id_gruppo_danni_rientro AND ga2.id_accessorio = ga.id_accessorio" &
                    " INNER JOIN  condizioni_elementi ce WITH(NOLOCK) ON ga.id_accessorio = ce.id " &
                    " WHERE ga.id_evento_apertura = @id_gruppo_danni_uscita" &
                    " ORDER BY ce.descrizione"

                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Cmd.Parameters.Add("@id_gruppo_danni_rientro", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_rientro
                    Cmd.Parameters.Add("@id_gruppo_danni_uscita", System.Data.SqlDbType.Int).Value = mio_record.id_gruppo_danni_uscita

                    Dim i As Integer = 0
                    Dim j As Integer = 0
                    Using Rs = Cmd.ExecuteReader
                        Do While Rs.Read
                            Dim assente As Boolean = Rs("assente")
                            Dim danno As String = Rs("descrizione") & vbCrLf

                            i += 1
                            j = i Mod 3
                            Select Case j
                                Case 1
                                    .ACCESSORI_OUT_1 += danno
                                    If assente Then
                                        .ACCESSORI_IN_1 += "[X]" & danno
                                    Else
                                        .ACCESSORI_IN_1 += "[ ]" & danno
                                    End If
                                Case 2
                                    .ACCESSORI_OUT_2 += danno
                                    If assente Then
                                        .ACCESSORI_IN_2 += "[X]" & danno
                                    Else
                                        .ACCESSORI_IN_2 += "[ ]" & danno
                                    End If
                                Case Else
                                    .ACCESSORI_OUT_3 += danno
                                    If assente Then
                                        .ACCESSORI_IN_3 += "[X]" & danno
                                    Else
                                        .ACCESSORI_IN_3 += "[ ]" & danno
                                    End If
                            End Select
                        Loop
                    End Using
                End Using

            End Using
        End With


        Stampa_Check_In_Privata_PDF = miei_dati



        Return Stampa_Check_In_Privata_PDF

        'restituisce i dati check




    End Function



    ' metodo pubblico per consentire la stampa anche ha moduli esterni!

    Public Sub Stampa_Check_In(ByVal id_tipo_documento As tipo_documento, ByVal id_documento As Integer, ByVal numero_crv As Integer, Optional tipostampa As String = "s")

        Dim mio_record As DatiContratto = Nothing ' per  adesso così... in seguito dichiaro una interfaccia!

        Select Case id_tipo_documento
            Case tipo_documento.Contratto
                mio_record = DatiContratto.getRecordDaNumContratto(id_documento, numero_crv)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: contratto non trovato.")
                End If

            Case tipo_documento.MovimentoInterno
                mio_record = DatiContratto.getRecordDaNumTrasferimento(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore:  movimento interno non trovato.")
                End If

            Case tipo_documento.ODL
                mio_record = DatiContratto.getRecordDaNumODL(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                End If

            Case tipo_documento.DuranteODL
                mio_record = DatiContratto.getRecordDaNumDuranteODL(id_documento)
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                End If

            Case Is >= tipo_documento.RDSGenerico
                mio_record = DatiContratto.getRecordDaRDSGenerico(Integer.Parse(lb_id_evento.Text))
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: RDS Generico non trovato.")
                End If

            Case Else
                Err.Raise(1001, Nothing, "Tipo documento non previsto")
        End Select

        If tipostampa = "f" Then

            'crea file in pdf del ckin 01.06.2022


            Dim dc As DatiStampaCheck = Stampa_Check_In_Privata_PDF(mio_record, tipostampa) 'modificato per generare pdf del ck 01.06.2022 salvo

            Dim pathpdf As String = StampaCheck.GeneraDocumentoPDF(dc, id_documento)
            If pathpdf <> "" Then

                'allega se non presente in db

                Dim allega As Boolean = funzioni_comuni_new.AllegaCkIn(id_documento)

                'sqlAllegati.SelectCommand = "SELECT id_allegato, nome_file, cartella, (cartella + nome_file) as my_path, descrizione FROM contratti_prenotazioni_allegati WITH(NOLOCK) " &
                '"INNER JOIN contratti_prenotazioni_allegati_tipo WITH(NOLOCK) ON contratti_prenotazioni_allegati.id_cnt_pren_allegati_tipo=contratti_prenotazioni_allegati_tipo.id_cnt_pren_allegati_tipo WHERE num_pren='" & lblNumPren.Text & "' OR num_cnt=" & lblNumContratto.Text &
                '"Order by id_allegato"
                ''dataListAllegati.DataBind()
                'ListViewAllegati.DataBind() ' aggiornato 


            End If

        Else
            Stampa_Check_In_Privata(mio_record, tipostampa) 'modificato per generare pdf del ck 01.06.2022 salvo
        End If







    End Sub






    Protected Sub bt_stampa_check_out_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_stampa_check_out.Click
        Dim mio_record As DatiContratto = Nothing
        Dim id_tipo_documento As tipo_documento = Integer.Parse(lb_id_tipo_documento_apertura.Text)
        If id_tipo_documento = 0 And DropDownTipoEventoAperturaDanno.Enabled Then
            id_tipo_documento = tipo_documento.RDSGenerico
        End If

        Select Case id_tipo_documento
            Case tipo_documento.Contratto
                mio_record = DatiContratto.getRecordDaNumContratto(Integer.Parse(lb_id_documento_apertura.Text), Integer.Parse(lb_num_crv.Text))
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: contratto non trovato.")
                End If

            Case tipo_documento.MovimentoInterno
                mio_record = DatiContratto.getRecordDaNumTrasferimento(Integer.Parse(lb_id_documento_apertura.Text))
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore:  movimento interno non trovato.")
                End If
            Case tipo_documento.Lavaggio
                mio_record = DatiContratto.getRecordDaLavaggio(Integer.Parse(lb_id_documento_apertura.Text))
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore:  movimento interno non trovato.")
                End If
            Case tipo_documento.ODL
                mio_record = DatiContratto.getRecordDaNumODL(Integer.Parse(lb_id_documento_apertura.Text))
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                End If

            Case tipo_documento.DuranteODL
                mio_record = DatiContratto.getRecordDaNumDuranteODL(Integer.Parse(lb_id_documento_apertura.Text))
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                End If

            Case Is >= tipo_documento.RDSGenerico
                mio_record = DatiContratto.getRecordDaRDSGenerico(Integer.Parse(lb_id_evento.Text))
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: RDS Generico non trovato.")
                End If

            Case Else
                Err.Raise(1001, Nothing, "Tipo documento non previsto")
        End Select

        Stampa_Check_Out_Privata(mio_record)
    End Sub

    Protected Sub bt_stampa_check_in_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_stampa_check_in.Click
        Dim mio_record As DatiContratto = Nothing
        Dim id_tipo_documento As tipo_documento = Integer.Parse(lb_id_tipo_documento_apertura.Text)
        Select Case id_tipo_documento
            Case tipo_documento.Contratto
                mio_record = DatiContratto.getRecordDaNumContratto(Integer.Parse(lb_id_documento_apertura.Text), Integer.Parse(lb_num_crv.Text))
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: contratto non trovato.")
                End If

            Case tipo_documento.MovimentoInterno
                mio_record = DatiContratto.getRecordDaNumTrasferimento(Integer.Parse(lb_id_documento_apertura.Text))
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore:  movimento interno non trovato.")
                End If
            Case tipo_documento.Lavaggio
                mio_record = DatiContratto.getRecordDaLavaggio(Integer.Parse(lb_id_documento_apertura.Text))
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore:  movimento interno non trovato.")
                End If
            Case tipo_documento.ODL
                mio_record = DatiContratto.getRecordDaNumODL(Integer.Parse(lb_id_documento_apertura.Text))
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                End If

            Case tipo_documento.DuranteODL
                mio_record = DatiContratto.getRecordDaNumDuranteODL(Integer.Parse(lb_id_documento_apertura.Text))
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: ODL non trovato.")
                End If

            Case Is >= tipo_documento.RDSGenerico
                mio_record = DatiContratto.getRecordDaRDSGenerico(Integer.Parse(lb_id_evento.Text))
                If mio_record Is Nothing Then
                    Err.Raise(1001, Nothing, "Errore: RDS Generico non trovato.")
                End If

            Case Else
                Err.Raise(1001, Nothing, "Tipo documento non previsto")
        End Select

        Stampa_Check_In_Privata(mio_record)
    End Sub

    Protected Function verifica_da_addebitare() As Boolean
        verifica_da_addebitare = True
        Dim lb_des_da_addebitare As Label
        For i = 0 To listViewElencoDanniPerEvento.Items.Count - 1
            lb_des_da_addebitare = listViewElencoDanniPerEvento.Items(i).FindControl("lb_des_da_addebitare")
            If lb_des_da_addebitare.Text = "" Then
                verifica_da_addebitare = False
            End If
        Next
    End Function

    Protected Function verifica_non_addebitare() As Boolean
        verifica_non_addebitare = False

        For i = 0 To listViewElencoDanniPerEvento.Items.Count - 1
            Dim lb_id_stato As Label = listViewElencoDanniPerEvento.Items(i).FindControl("lb_id_stato")
            Dim id_stato As stato_danno = Integer.Parse(lb_id_stato.Text)
            If id_stato = stato_danno.aperto Then ' se il danno risulta chiuso (ad esempio rientro da furto...) non verifico se da addebitare o meno
                Dim lb_des_da_addebitare As Label = listViewElencoDanniPerEvento.Items(i).FindControl("lb_des_da_addebitare")
                If lb_des_da_addebitare.Text = "" Then
                    Return False
                End If
                If lb_des_da_addebitare.Text = "Si" Then
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    Protected Function CalcolaTotaleRDS(ByVal importo As Double?, ByVal id_iva As Integer?, ByVal spese_postali As Double?) As Double
        If importo Is Nothing Then
            importo = 0
        End If
        If spese_postali Is Nothing Then
            spese_postali = 0
        End If
        Dim iva As Double = 0
        If id_iva IsNot Nothing Then
            iva = Libreria.getAliquotaIVADaId(id_iva)
        End If

        Dim totale As Double = importo * (1 + iva / 100) + spese_postali

        Return System.Math.Truncate(totale * 100 + 0.5) / 100
    End Function

    Protected Sub bt_salva_rds_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_salva_rds.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneRDS) <> "3" Then
            Libreria.genUserMsgBox(Page, "Non hai i diritti per salvare o modificare l'evento danno.")
            Return
        End If

        Dim mio_stato_rds As sessione_danni.stato_rds = Integer.Parse(lb_id_stato_rds.Text)
        Dim nuovo_stato_rds As sessione_danni.stato_rds = Integer.Parse(DropOperazione.SelectedValue)

        If Not (nuovo_stato_rds = sessione_danni.stato_rds.In_attesa) AndAlso Not verifica_da_addebitare() Then
            Libreria.genUserMsgBox(Page, "Specificare se ogni danno è da addebitare o meno.")
            Return
        End If

        If Not (mio_stato_rds = sessione_danni.stato_rds.Da_lavorare Or mio_stato_rds = sessione_danni.stato_rds.In_attesa Or mio_stato_rds = sessione_danni.stato_rds.All_attenzione Or mio_stato_rds = sessione_danni.stato_rds.Da_addebitare) Then
            Libreria.genUserMsgBox(Page, "Errore" & vbCrLf & "Nello stato attuale, non è possibile modificare lo stato dell'RDS.")
            Return
        End If

        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(Integer.Parse(lb_id_evento.Text))
        Dim mia_variazione_stato As veicoli_stato_rds_variazione = New veicoli_stato_rds_variazione

        Select Case nuovo_stato_rds
            Case sessione_danni.stato_rds.Chiuso
                Dim messaggio As String = ""
                If Not verifica_non_addebitare() Then
                    messaggio += "Non è possibile chiudere l'evento danno se qualche danno risulta da addebitare." & vbCrLf
                End If
                If DropDownNonAddebito.SelectedValue <= 0 Then
                    messaggio += "Specificare il motivo per il mancato addebito." & vbCrLf
                End If
                If messaggio <> "" Then
                    Libreria.genUserMsgBox(Page, messaggio)
                    Return
                End If

                mio_evento.Chiudi_RDS(Integer.Parse(DropDownNonAddebito.SelectedValue))
                mia_variazione_stato.InitDati(mio_evento, mio_stato_rds)
                mia_variazione_stato.SalvaRecord()

                lb_id_stato_rds.Text = nuovo_stato_rds
            Case sessione_danni.stato_rds.In_attesa
                With mio_evento
                    If Drop_documenti.SelectedValue <= 0 And Drop_manutenzione.SelectedValue <= 0 Then
                        Libreria.genUserMsgBox(Page, "Specificare il motivo dello stato di attesa se dovuto all'ufficio manutenzione o alla documentazione mancante.")
                        Return
                    End If

                    .stato_rds = nuovo_stato_rds
                    If Drop_documenti.SelectedValue = 0 Then
                        .attesa_documentazione = Nothing
                    Else
                        .attesa_documentazione = Drop_documenti.SelectedValue
                    End If
                    If Drop_manutenzione.SelectedValue = 0 Then
                        .attesa_manutenzione = Nothing
                    Else
                        .attesa_manutenzione = Drop_manutenzione.SelectedValue
                    End If

                    .AggiornaRecord()
                End With
                mia_variazione_stato.InitDati(mio_evento, mio_stato_rds)
                mia_variazione_stato.SalvaRecord()

                lb_id_stato_rds.Text = nuovo_stato_rds
            Case sessione_danni.stato_rds.All_attenzione, sessione_danni.stato_rds.Da_addebitare, sessione_danni.stato_rds.Da_addebitare
                With mio_evento
                    Dim messaggio As String = ""
                    If tx_stima_rds.Text = "" Then
                        messaggio += "Specificare l'importo della stima dell'RDS." & vbCrLf
                    Else
                        If Not IsNumeric(tx_stima_rds.Text) Then
                            messaggio += "Specificare un valore corretto per l'importo della stima dell'RDS." & vbCrLf
                        End If
                    End If
                    If Drop_aliquote_iva.SelectedValue <= 0 Then
                        messaggio += "Selezionare una valore per l'IVA." & vbCrLf
                    End If
                    If messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, messaggio)
                        Return
                    End If

                    .stato_rds = nuovo_stato_rds
                    .attesa_documentazione = Nothing
                    .attesa_manutenzione = Nothing

                    If tx_stima_rds.Text = "" Then
                        .importo = Nothing
                    Else
                        .importo = Double.Parse(tx_stima_rds.Text)
                    End If

                    .iva = Drop_aliquote_iva.SelectedValue ' attenzione non è  l'iva ma l'indice nella tabella dell'iva!

                    If tx_spese_postali.Text = "" Then
                        .spese_postali = Nothing
                    Else
                        .spese_postali = Double.Parse(tx_spese_postali.Text)
                    End If

                    .totale = CalcolaTotaleRDS(.importo, .iva, .spese_postali)

                    If tx_data_perizia.Text = "" Then ' non è un campo obbligatorio
                        .data_perizia = Nothing
                    Else
                        .data_perizia = Date.Parse(tx_data_perizia.Text)
                    End If
                    .perizia = ck_perizia.Checked

                    If tx_giorni_fermo_tecnico.Text = "" Then
                        .giorni_fermo_tecnico = Nothing
                    Else
                        .giorni_fermo_tecnico = Integer.Parse(tx_giorni_fermo_tecnico.Text)
                    End If
                    '------------------------------------------------------------------------------------
                    If sessione_danni.stato_rds.Da_addebitare Then
                        ' .stimato  la stima viene salvata solo quando si è scelto di addebitare!
                        .stimato = .importo
                    End If
                    '------------------------------------------------------------------------------------
                    If tx_data_incidente.Text = "" Then ' non è un campo obbligatorio
                        .data_incidente = Nothing
                    Else
                        .data_incidente = Date.Parse(tx_data_incidente.Text)
                    End If

                    If tx_luogo_incidente.Text = "" Then
                        .luogo_incidente = Nothing
                    Else
                        .luogo_incidente = tx_luogo_incidente.Text
                    End If

                    .doc_CID = ck_CID.Checked
                    .doc_denuncia = ck_denuncia.Checked
                    .doc_fotocopia_doc = ck_fotocopia_doc.Checked
                    .doc_preventivo = ck_preventivo.Checked
                    If tx_num_fotografie.Text = "" Then
                        .num_fotografie = Nothing
                    Else
                        .num_fotografie = Integer.Parse(tx_num_fotografie.Text)
                    End If

                    .AggiornaRecord()
                    mia_variazione_stato.InitDati(mio_evento, mio_stato_rds)
                    mia_variazione_stato.SalvaRecord()

                    lb_id_stato_rds.Text = nuovo_stato_rds
                End With

            Case Else
                Libreria.genUserMsgBox(Page, "Selezionare una operazione valida.")
                Return
        End Select

        FillRDS(mio_evento)

        tab_dettagli_pagamento.Visible = True
        DettagliPagamento.InitForm(lb_num_prenotazione.Text, lb_id_documento_apertura.Text, lb_num_rds.Text, )

        listViewStoricoVariazioni.DataBind()

        RaiseEvent SalvaRDS(Me, New EventArgs)

    End Sub

    Protected Function GetBoolean(ByVal Valore As Object) As Boolean
        If Valore Is DBNull.Value Then
            Return False
        End If
        Return Valore
    End Function

    Protected Sub bt_chiudi_rds_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_chiudi_rds.Click
        RaiseEvent ChiusuraForm(Me, New EventArgs)
    End Sub

    Protected Sub bt_stampa_lettera_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_stampa_lettera.Click
        Session("StampaLetteraRDS_id_evento") = lb_id_evento.Text

        If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('/gestione_danni/SelezionaLetteraRDS.aspx','','width=400,height=400,left=100,top=100,resizable=no,menubar=no,toolbar=no,scrollbars=no,location=no,status=no');", True)
        End If

        ' StampaLetterRDS(tipo_lettera_rds.NoKasko, tipo_linguaggio_rds.italiano)
    End Sub

    Protected Sub bt_pagamento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_pagamento.Click, bt_pagamento_da_furto.Click
        Dim mio_evento As EventoNuovoRecord = New EventoNuovoRecord
        mio_evento.Valore = Integer.Parse(lb_id_evento.Text)
        RaiseEvent PagamentoDanno(Me, mio_evento)
    End Sub

    Public Sub AggiornaPagamenti()
        tab_dettagli_pagamento.Visible = True
        DettagliPagamento.AggiornaTabella()
    End Sub

    Protected Sub bt_sinistro_attivo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_sinistro_attivo.Click
        Dim mio_record As DatiContratto = ViewState("DocumentoAssociato")
        ' Response.Write(mio_record.id & " " & lb_id_evento.Text & " " & "ATTIVO")

        If (mio_record.tipo_documento_origine = tipo_documento.Contratto) Then
            Session("snx_id_cnt") = mio_record.id & ""
        Else
            Session("snx_id_cnt") = "0"
        End If

        Session("snx_id_rds") = lb_id_evento.Text
        Session("snx_sin") = "ATTIVO"

        ' Response.Redirect("sinistri.aspx")
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('sinistri.aspx','','');", True)
            End If
        End If
    End Sub

    Protected Sub bt_sinistro_passivo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_sinistro_passivo.Click
        Dim mio_record As DatiContratto = ViewState("DocumentoAssociato")
        'Trace.Write(mio_record.id & " " & lb_id_evento.Text & " " & "PASSIVO")
        If (mio_record.tipo_documento_origine = tipo_documento.Contratto) Then
            Session("snx_id_cnt") = mio_record.id & ""
        Else
            Session("snx_id_cnt") = "0"
        End If
        Session("snx_id_rds") = lb_id_evento.Text
        Session("snx_sin") = "PASSIVO"

        ' Response.Redirect("sinistri.aspx")
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('/sinistri.aspx','','');", True)
            End If
        End If
    End Sub

    Protected Sub bt_gestione_sinistri_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_gestione_sinistri.Click
        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(Integer.Parse(lb_id_evento.Text))
        With mio_evento
            Session("sin_anno") = .anno_sinistro & ""
            Session("sin_proto") = .numero_sinistro & ""
        End With

        If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('sinistri.aspx','','');", True)
            End If
        End If
    End Sub

    Protected Function verifica_furto() As Boolean
        Dim messaggio As String = ""
        If tx_data_evento.Text = "" Then
            messaggio += "Specificare la data dell'evento che ha generato il danno."
        End If

        If tx_data_denuncia_furto.Text = "" Then
            messaggio += "Specificare la data della denuncia del furto."
        End If

        If tx_ora_denuncia_furto.Text = "" Then
            messaggio += "Specificare l'ora della denuncia del furto."
        End If

        If messaggio <> "" Then
            Libreria.genUserMsgBox(Page, messaggio)
            Return False
        Else
            Return True
        End If
    End Function

    Protected Sub bt_salva_furto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_salva_furto.Click
        If Not verifica_furto() Then
            Return
        End If

        Dim mio_record As DatiContratto = CType(ViewState("DocumentoAssociato"), DatiContratto)

        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(Integer.Parse(lb_id_evento.Text))
        With mio_evento
            .data = New Date(Year(tx_data_evento.Text), Month(tx_data_evento.Text), Day(tx_data_evento.Text))
            .data_dichiarazione_furto = New Date(Year(tx_data_denuncia_furto.Text), Month(tx_data_denuncia_furto.Text), Day(tx_data_denuncia_furto.Text), Hour(tx_ora_denuncia_furto.Text), Minute(tx_ora_denuncia_furto.Text), 0)
            .nota = tx_nota_evento.Text

            .AggiornaRecord()

            Dim CodiceStazione As String = getCodiceStazione(mio_record.id_stazione_rientro)
            .AttivaRecord(CodiceStazione)

            .SalvaFurtoInMovimentiTarga(mio_record.id_stazione_rientro)
            .AggiornaVeicoliPerFurto()


            lb_num_rds.Text = .id_rds

            bt_pagamento_da_furto.Visible = True
        End With

        Dim mio_gruppo_evento As veicoli_gruppo_evento = Nothing
        mio_gruppo_evento = veicoli_gruppo_evento.SalvaDanniApertiAuto(mio_record.id_veicolo, Integer.Parse(lb_id_tipo_documento_apertura.Text), mio_record.num_contratto, mio_record.num_crv, mio_evento.id)

        mio_record.id_gruppo_danni_rientro = mio_gruppo_evento.id
        mio_record.AggiornaContrattiPerFurto() ' salvo solo l'id del gruppo di rientro

        bt_pagamento_da_furto.Visible = True

        Libreria.genUserMsgBox(Page, "Salvataggio avvenuto correttamente")

        RaiseEvent SalvaCheckInConFurto(Me, New EventArgs)
    End Sub

    Protected Sub bt_chiudi_senza_furto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_chiudi_senza_furto.Click
        RaiseEvent ChiusuraForm(Me, New EventArgs)
    End Sub

    Protected Sub bt_addebita_furto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_addebita_furto.Click
        Dim mio_danno As veicoli_danni = veicoli_danni.getDannoFurto(Integer.Parse(lb_id_evento.Text))

        If mio_danno IsNot Nothing Then
            With mio_danno
                .DaAddebitare(True)
            End With
        Else
            Libreria.genUserMsgBox(Page, "Danno per furto non trovato.")
        End If

        listViewElencoDanniPerEvento.DataBind()
    End Sub

    Protected Sub bt_non_addebita_furto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_non_addebita_furto.Click
        Dim mio_danno As veicoli_danni = veicoli_danni.getDannoFurto(Integer.Parse(lb_id_evento.Text))

        If mio_danno IsNot Nothing Then
            With mio_danno
                .DaAddebitare(False)
            End With
        Else
            Libreria.genUserMsgBox(Page, "Danno per furto non trovato.")
        End If

        listViewElencoDanniPerEvento.DataBind()
    End Sub

    Protected Sub FillSinistro(ByVal mio_evento As veicoli_evento_apertura_danno)

        lb_anno.Text = mio_evento.anno_sinistro & ""
        lb_numero_protocollo_interno.Text = mio_evento.numero_sinistro & ""

        lb_stato_sinistro.Text = "(N.V.)"
        lb_tipologia_sinistro.Text = "(N.V.)"
        lb_numero_pratica.Text = "(N.V.)"
        lb_proprietario_veicolo.Text = "(N.V.)"

        Dim sqlStr As String
        sqlStr = "SELECT st.descrizione stato_sinistro, t.descrizione tipologia_sinistro," &
            " numero numero_pratica, proprietario_veicolo" &
            " FROM sinistri s WITH(NOLOCK)" &
            " INNER JOIN sinistri_stato st WITH(NOLOCK) ON s.id_stato = st.id" &
            " INNER JOIN sinistri_tipologia t WITH(NOLOCK) ON s.id_tipologia = t.id" &
            " WHERE s.attivo = 1" &
            " AND s.anno = @anno" &
            " AND s.numero_protocollo_interno = @numero_protocollo_interno"


        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.Parameters.Add("@anno", System.Data.SqlDbType.Int).Value = mio_evento.anno_sinistro
                Cmd.Parameters.Add("@numero_protocollo_interno", System.Data.SqlDbType.Int).Value = mio_evento.numero_sinistro

                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    If Rs.Read Then
                        lb_stato_sinistro.Text = Rs("stato_sinistro") & ""
                        lb_tipologia_sinistro.Text = Rs("tipologia_sinistro") & ""
                        lb_numero_pratica.Text = Rs("numero_pratica") & ""
                        lb_proprietario_veicolo.Text = Rs("proprietario_veicolo") & ""
                    End If
                End Using
            End Using
        End Using
    End Sub

    Protected Sub lb_num_documento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_num_documento.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.PreventiviPrenotazioni) = "1" Then
            Libreria.genUserMsgBox(Page, "Non hai diritti per visionare il contratto.")
            Return
        End If

        Dim id_tipo_documento As tipo_documento = Integer.Parse(lb_id_tipo_documento_apertura.Text)

        Select Case id_tipo_documento
            Case tipo_documento.Contratto
                Dim mio_record As DatiContratto = ViewState("DocumentoAssociato")

                Session("carica_contratto_da_gestione_rds") = mio_record.id & ""

                If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('contratti.aspx','','');", True)
                    End If
                End If

            Case Else
                Libreria.genUserMsgBox(Page, "Tipo documento ancora non gestito")
        End Select

    End Sub

    Protected Sub bt_stampa_RDS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_stampa_RDS.Click
        If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            Dim url_print As String = "/gestione_danni/RiepilogoRDS.aspx?orientamento=verticale&id_evento=" & lb_id_evento.Text
            Dim mio_random As String = Format((New Random).Next(), "0000000000")
            Trace.Write(url_print)
            Session("url_print") = url_print
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx?a=" & mio_random & "','')", True)
            End If
        End If
    End Sub


    Protected Sub bt_da_non_addebitare_F_Click(sender As Object, e As System.EventArgs) Handles bt_da_non_addebitare_F.Click
        If DropDownNonAddebito_F.SelectedValue = "0" Then
            Libreria.genUserMsgBox(Page, "Specificare il motivo per il non addebito.")
        Else
            Dim i As Integer
            Dim lb_id_danno As Label
            For i = 0 To listViewElencoDanniPerEvento.Items.Count - 1
                lb_id_danno = listViewElencoDanniPerEvento.Items(i).FindControl("lb_id_danno")

                veicoli_danni.DaAddebitare(Integer.Parse(lb_id_danno.Text), False, Integer.Parse(DropDownNonAddebito_F.SelectedValue))
            Next
        End If

        aggiorna_elenco()
        DropDownNonAddebito_F.SelectedValue = "0"
    End Sub

    Protected Sub bt_da_addebitare_F_Click(sender As Object, e As System.EventArgs) Handles bt_da_addebitare_F.Click
        Dim i As Integer
        Dim lb_id_danno As Label
        For i = 0 To listViewElencoDanniPerEvento.Items.Count - 1
            lb_id_danno = listViewElencoDanniPerEvento.Items(i).FindControl("lb_id_danno")

            veicoli_danni.DaAddebitare(Integer.Parse(lb_id_danno.Text), True, 0)
        Next

        aggiorna_elenco()
    End Sub

    'Tony 24/08/2022
    Protected Function getStatoContratto() As String
        Dim sqla As String = "SELECT status FROM contratti WITH(NOLOCK) WHERE num_contratto='" & lb_num_documento.Text & "' AND attivo='1'"
        Try
            'RESTITUISCE LO STATO DEL CONTRATTO ATTUALE (QUINDI PARTENDO DAL NUMERO DI CONTRATTRO)
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim stato As String = Cmd.ExecuteScalar & ""

            If stato = "" Then
                'IN QUESTO CASO SICURAMENTE IL CONTRATTO E' IN STATO 0
                stato = "0"
            End If

            getStatoContratto = stato

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("errorgetStatoContratto  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function
    'Tony FINE

    'Tony 20-09-2022
    Protected Function TrovareIdStazione(ByVal valore As String) As String
        'Dim arrayStazione(2) As String

        'arrayStazione = Split(valore, " ")
        'TrovareCodiceStazione = arrayStazione(0)
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM stazioni WITH(NOLOCK) WHERE (codice = '" & valore & "')", Dbc)
            'Response.Write(Cmd.CommandText & "<br><br>")
            'Response.End()
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            If Rs.HasRows Then
                Do While Rs.Read
                    TrovareIdStazione = Rs("id")
                Loop
            End If
            Rs.Close()
            Dbc.Close()
            Rs = Nothing
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("Trova Codice Stazione Rientro  : <br/>" & ex.Message & "<br/>")
        End Try
    End Function

    Protected Function TrovareCodiceStazione(ByVal valore As String) As String
        'Dim arrayStazione(2) As String

        'arrayStazione = Split(valore, " ")
        'TrovareCodiceStazione = arrayStazione(0)
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM contratti,stazioni WITH(NOLOCK) WHERE contratti.id_stazione_rientro = stazioni.id and (num_contratto = '" & valore & "') AND (attivo = 1)", Dbc)

            'Response.Write(Cmd.CommandText & "<br><br>")
            'Response.End()
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            If Rs.HasRows Then
                Do While Rs.Read
                    TrovareCodiceStazione = Rs("codice")
                Loop
            End If
            Rs.Close()
            Dbc.Close()
            Rs = Nothing
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("Trova Codice Stazione Rientro  : <br/>" & ex.Message & "<br/>")
        End Try
    End Function
    'Tony FINE

    Protected Sub AggiornaMioCheckIn(ByVal Valore As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
        Dim Sql As String
        Dim Sql2 As String
        Dim SqlQuery As String

        Try
            Sql = "UPDATE veicoli SET id_stazione = " & Valore & " WHERE id =" & lb_id_veicolo.Text
            'Response.Write(Sql & "<br/>")
            'Response.End()

            Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
            Cmd.ExecuteNonQuery()

            'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
            'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
            'Session("residenza_virtuale") = Cmd.ExecuteScalar

            SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
            Cmd.CommandText = SqlQuery
            'Response.Write(Cmd.CommandText & "<br/>")
            'Response.End()
            Cmd.ExecuteNonQuery()
        Catch ex As Exception            
            HttpContext.Current.Response.Write("AggiornaMioCheck  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")            
        End Try

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    'Tony 22-09-2022
    Protected Sub TrovaContratto()

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM contratti WITH(NOLOCK) WHERE (targa = '" & lb_targa.Text & "') and km_rientro ='" & lb_km.Text & "' AND (attivo = 1)", Dbc)
            'Response.Write(Cmd.CommandText & "<br><br>")
            'Response.End()
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            If Rs.HasRows Then
                Do While Rs.Read
                    lb_num_documento.Text = Rs("num_contratto")
                Loop
            End If
            Rs.Close()
            Dbc.Close()
            Rs = Nothing
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("Trova Codice Stazione Rientro  : <br/>" & ex.Message & "<br/>")
        End Try
    End Sub

    'Tony 29-09-2022
    Protected Sub AggiornaContratto()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
        Dim Sql As String
        Dim Sql2 As String
        Dim SqlQuery As String

        Dim LitriRientroMio As String
        'Response.Write("ID " & lb_id_veicolo.Text & "<br>")
        Dim capacita_serbatoio As Integer = getSerbatoioDaIdVeicolo(lb_id_veicolo.Text)
        'Response.Write("capacita " & capacita_serbatoio & "<br>")

        LitriRientroMio = (capacita_serbatoio / 8) * CInt(DropDownSerbatoioRientro.SelectedValue)
        'Response.Write("LitriRie " & LitriRientroMio & "<br>")
        'Response.End()

        Try
            Sql = "update contratti set litri_rientro = '" & Int(LitriRientroMio) & "', km_rientro ='" & tx_km_rientro.Text & "' WHERE (num_contratto = '" & lb_num_documento.Text & "')  AND (attivo = 1)"
            'Response.Write(Sql & "<br/>")
            'Response.End()

            Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
            Cmd.ExecuteNonQuery()

            'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
            'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
            'Session("residenza_virtuale") = Cmd.ExecuteScalar

            SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
            Cmd.CommandText = SqlQuery
            'Response.Write(Cmd.CommandText & "<br/>")
            'Response.End()
            Cmd.ExecuteNonQuery()
        Catch ex As Exception
            HttpContext.Current.Response.Write("AggiornaContratto  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        'Response.End()
    End Sub
    'FINE Tony

    Protected Function getRdsNum(ByVal num_contratto As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id_rds FROM veicoli_evento_apertura_danno WITH(NOLOCK) WHERE NOT id_rds IS NULL  AND id_documento_apertura=" & num_contratto, Dbc)
        Dim test As String = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        If test <> "" Then
            getRdsNum = "RDS N. " & test
        Else
            getRdsNum = ""
        End If
    End Function

    Protected Function VeicoliDanniVuoto() As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT count(*) FROM  veicoli_danni WITH(NOLOCK) WHERE id_evento_apertura=" & lb_id_evento.Text, Dbc)
        Dim test As String = Cmd.ExecuteScalar & ""

        'Response.Write(Cmd.CommandText & "<br>")

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        If test = "0" Then
            'Response.Write("Vuoto True")
            VeicoliDanniVuoto = True
        Else
            'Response.Write("Vuoto False")
            VeicoliDanniVuoto = False
        End If
    End Function

    Protected Sub EliminaVeicoliEventoAperturaDanno(ByVal Id As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
        Dim Sql As String
        Dim Sql2 As String
        Dim SqlQuery As String

        Try
            'Response.Write(Id & "<br>")

            Sql = "delete  from veicoli_evento_apertura_danno WHERE id = '" & Id & "'"

            'Response.Write(Sql & "<br>")
            'Response.End()

            Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
            Cmd.ExecuteNonQuery()

            'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
            'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
            'Session("residenza_virtuale") = Cmd.ExecuteScalar

            SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
            Cmd.CommandText = SqlQuery
            'Response.Write(Cmd.CommandText & "<br/>")
            'Response.End()
            Cmd.ExecuteNonQuery()
        Catch ex As Exception
            HttpContext.Current.Response.Write("Eliminazione Danno Errore contattare amministratore del sistema. <br/>" & ex.Message & "<br/>")
        End Try

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    'Tony 10/10/2022
    Protected Sub AggiornaStatoVeicolo(ByVal Valore As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
        Dim Sql As String
        Dim Sql2 As String
        Dim SqlQuery As String

        Try
            Sql = "UPDATE veicoli SET disponibile_nolo = " & Valore & " WHERE id =" & lb_id_veicolo.Text
            'Response.Write(Sql & "<br/>")
            'Response.End()

            Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
            Cmd.ExecuteNonQuery()

            'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
            'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
            'Session("residenza_virtuale") = Cmd.ExecuteScalar

            SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
            Cmd.CommandText = SqlQuery
            'Response.Write(Cmd.CommandText & "<br/>")
            'Response.End()
            Cmd.ExecuteNonQuery()
        Catch ex As Exception
            HttpContext.Current.Response.Write("AggiornaStatoVeicolo  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub
    'FINE Tony

 
End Class
