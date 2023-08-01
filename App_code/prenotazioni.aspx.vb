Imports funzioni_comuni
Imports iTextSharp

Imports iTextSharp.text.pdf
Imports iTextSharp.text.xml

Imports System.IO

Imports System.Net.Mail

Partial Class prenotazioni
    Inherits System.Web.UI.Page

    Dim funzioni As New funzioni_comuni
    Dim cPagamenti As New Pagamenti
    Dim flagItemBound248 As Boolean    '05.01.2022
    Dim flagItemBound223 As Boolean    '05.01.2022
    Dim flagItemBound100 As Boolean    '05.01.2022
    Dim flagItemBound170 As Boolean    '05.01.2022

    '09.12.2021
    'per verifica ck su ItemBound
    Dim ib_EliRes As Boolean = False
    Dim ib_PPLus As Boolean = False
    Dim ib_RD As Boolean = False
    Dim ib_RF As Boolean = False

    Dim RD_prepagato As Boolean = False
    Dim RF_prepagato As Boolean = False

    Dim RD_ck As Boolean = False
    Dim RF_ck As Boolean = False




    Dim lPrepagato As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        AddHandler anagrafica_conducenti.scegliConducente, AddressOf scegli_conduente
        AddHandler anagrafica_ditte.scegliDitta, AddressOf scegli_ditta
        AddHandler Scambio_Importo1.ScambioImportoClose, AddressOf ScambioImportoClose
        AddHandler Scambio_Importo1.ScambioImportoTransazioneEseguita, AddressOf ScambioImportoTransazioneEseguita
        AddHandler Scambio_Importo1.CassaPagamentoEseguito, AddressOf CassaPagamentoEseguito


        If IsNothing(Request.Cookies("SicilyRentCar")) Then
            Response.Redirect("default.aspx")
        End If



        ultimo_gruppo.Text = ""
        '07.12.2021
        Dim tResponse As String = ""
        Dim tResponseOLD As String = ""
        Dim idgruppo As String = ""

        If Not Page.IsPostBack() Then
            dropStazionePickUp.DataBind()
            dropStazioneDropOff.DataBind()
            dropTipoCliente.DataBind()
            dropGruppoDaConsegnare.DataBind()
            'RECUPERO IL LIVELLO DI ACCESSO PER L'APPLICAZIONE DELLO SCONTO E PER L'OMAGGIABILITA' DEGLI ACCESSORI
            livello_accesso_sconto.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.ApplicareSconto)
            livello_accesso_omaggi.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.OmaggiareAccessori)
            livello_accesso_dettaglio_pos.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzaDettaglioOperazionePOS)
            livello_accesso_broker.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzareCostiBroker)
            livello_accesso_annulla_ripristina.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.AnnullareEripristinarePrenotazioni)
            livello_accesso_modifica_broker.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.ModificaPrenotazioniBroker)
            livello_accesso_eliminare_pagamenti.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.EliminarePagamenti)

            'recupera num_prenotazione da querystring 21.0.2022
            If Request.QueryString("nr") <> "" Then
                Session("num_prenotazione_from_preventivi") = Request.QueryString("nr")
            End If

            '#salvo 24.02.2023
            Dim ValoreTariffaOri As String = "0"
            Dim valtar As Label
            'end salvo

            If Session("num_prenotazione_from_preventivi") <> "" Then
                'SE PROVENGO DA preventivi.aspx CON LA RICHIESTA DI VISUALIZZARE/MODIFICARE UNA PRENOTAZIONE
                provenienza.Text = "preventivi.aspx"
                Dim num_pren As String = Session("num_prenotazione_from_preventivi")
                Session("num_prenotazione_from_preventivi") = ""
                tab_cerca_tariffe.Visible = False
                tab_ricerca.Visible = True

                txtNumPrenotazione.Text = num_pren
                richiama_prenotazione(num_pren)

                'creazione della stringa tResponse x la verifica dei ck 07.12.2021
                'Verifica condizioni per i ck 24.11.2021 
                idgruppo = id_gruppo_auto_scelto.Text         'imposta il gruppo passato
                'test x Verifica Ck e MSG
                Dim idgruppoT As String
                Dim id_gruppoSceltoT As Label = listPrenotazioniCosti.FindControl("id_gruppoLabel") 'Non necessario perchè ricontrollo quale gruppo richiamato
                Dim id_gruppoT As Label
                Dim check_attualeT As CheckBox
                Dim check_oldT As CheckBox
                Dim id_elementoT As Label
                Dim omaggiatoT As CheckBox
                Dim old_omaggiatoT As CheckBox

                Dim num_elementoT As Label
                Dim tipologia_franchigiaT As Label
                Dim sottotipologia_franchigiaT As Label

                Dim old_selT As CheckBox
                Dim id_gruppoListT As Label



                Dim protezione_plusT As Boolean = False  'aggiunto 16.11.2021
                Dim Eliminazione_resT As Boolean = False  'aggiunto 16.11.2021

                For i = 0 To listPrenotazioniCosti.Items.Count - 1




                    id_gruppoT = listPrenotazioniCosti.Items(i).FindControl("id_gruppoLabel")

                    If id_gruppoT.Text = idgruppo Then 'id_gruppoSceltoT.Text Then
                        check_attualeT = listPrenotazioniCosti.Items(i).FindControl("chkScegli")
                        check_oldT = listPrenotazioniCosti.Items(i).FindControl("chkOldScegli")
                        id_elementoT = listPrenotazioniCosti.Items(i).FindControl("id_elemento")

                        If check_attualeT.Checked = True Then
                            tResponse += id_elementoT.Text & ","        'creo stringa con i ck attivi
                        End If

                        If check_oldT.Checked = True Then
                            tResponseOLD += id_elementoT.Text & ","     'creo stringa con i ck OLD
                        End If

                        'aggiunto salvo 24.02.2023 da verificare
                        If id_elementoT.Text = "98" Then
                            'valtar = listPrenotazioniCosti.Items(i).FindControl("valore_costo")
                            'ValoreTariffaOri = valtar.Text
                        End If


                    End If

                Next

                'solo x test
                'Response.Write("ck:<br/>" & tResponse & "<br/>")
                'Response.Write("<br/>ckOLD:<br/>" & tResponseOLD & "<br/>")

                'end creazione della stringa tResponse per la verifica dei ck 07.12.2021

                'deve ricalcolare le tariffe
                tResponse = tResponse

                '# aggiorna la visualizzazione delle franchigie 22.12.2021
                If tResponse.IndexOf("248") = -1 And tResponse.IndexOf("100") > -1 Then
                    funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "180", "")
                    funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "203", "")
                End If
                If tResponse.IndexOf("248") = -1 And tResponse.IndexOf("170") > -1 Then
                    funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "181", "")
                    funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "204", "")
                End If

                If tResponse.IndexOf("248") = -1 And tResponse.IndexOf("100") = -1 Then
                    funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "203", "")
                    funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "180", "")
                End If
                If tResponse.IndexOf("248") = -1 And tResponse.IndexOf("170") = -1 Then
                    funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "204", "")
                    funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "181", "")
                End If
                '#@ aggiorna la visualizzazione delle franchigie 22.12.2021

                'verifica se ELiRes attiva disabilita le franchigie
                If tResponse.IndexOf("223") > -1 Then

                    funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "204", "")
                    funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "203", "")
                    funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "180", "")
                    funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "181", "")

                End If

                'deve aggiornare il deposito cauzionale 02.02.2022
                Dim impdc As String = SetDepositoCauzionale(idPrenotazione.Text, idgruppo, numCalcolo.Text, True)       'con databind
                listPrenotazioniCosti.DataBind()






            ElseIf Session("num_prenotazione_from_ribaltamento") <> "" Then
                'SE PROVENGO DA preventivi.aspx CON LA RICHIESTA DI VISUALIZZARE/MODIFICARE UNA PRENOTAZIONE
                provenienza.Text = "ribaltamento.aspx"
                Dim num_pren As String = Session("num_prenotazione_from_ribaltamento")
                Session("num_prenotazione_from_preventivi") = ""
                tab_cerca_tariffe.Visible = False
                tab_ricerca.Visible = True

                txtNumPrenotazione.Text = num_pren
                richiama_prenotazione(num_pren)



            ElseIf Not Session("prenotazione_from_fatture") Is Nothing Then
                provenienza.Text = "gestione_fatture.aspx"

                Dim ditte_from_fatture As TipoCodiceFattura = Session("prenotazione_from_fatture")
                Session("prenotazione_from_fatture") = Nothing

                setTipoCodiceFattura(ditte_from_fatture)

                tab_cerca_tariffe.Visible = False
                tab_ricerca.Visible = True

                txtNumPrenotazione.Text = ditte_from_fatture.id_riferimento
                richiama_prenotazione(ditte_from_fatture.id_riferimento)

            Else
                'ricerca veloce 07.04.2022
                GetList_anno(0)

            End If



            'richiamata da lista ricerca
            '# salvo aggiunto 24.02.2023
            'valorizza campi originali x successivi confronti
            lbl_StazionePickUp_OLD.Text = dropStazionePickUp.SelectedValue
            lbl_DaData_Old.Text = txtDaData.Text
            lbl_DaOre_Old.Text = txtoraPartenza.Text
            lbl_DaMinuti_Old.Text = ""
            lbl_stazioneDropOff_OLD.Text = dropStazioneDropOff.SelectedValue
            lbl_AData_old.Text = txtAData.Text
            lbl_AOre_old.Text = txtOraRientro.Text
            lbl_AMinuti_old.Text = ""
            lbl_numeroGiorni_old.Text = txtNumeroGiorni.Text
            lbl_valore_tariffa_ori.Text = ValoreTariffaOri




            '@end salvo







            Session("exit_prenotazione") = ""   'azzera sessione

        Else

            'PostBack

            'creazione della stringa tResponse x la verifica dei ck 07.12.2021
            'Verifica condizioni per i ck 24.11.2021 
            idgruppo = id_gruppo_auto_scelto.Text         'imposta il gruppo passato
            'test x Verifica Ck e MSG
            Dim idgruppoT As String
            Dim id_gruppoSceltoT As Label = listPrenotazioniCosti.FindControl("id_gruppoLabel") 'Non necessario perchè ricontrollo quale gruppo richiamato
            Dim id_gruppoT As Label
            Dim check_attualeT As CheckBox
            Dim check_oldT As CheckBox
            Dim id_elementoT As Label
            Dim omaggiatoT As CheckBox
            Dim old_omaggiatoT As CheckBox

            Dim num_elementoT As Label
            Dim tipologia_franchigiaT As Label
            Dim sottotipologia_franchigiaT As Label

            Dim old_selT As CheckBox
            Dim id_gruppoListT As Label

            Dim protezione_plusT As Boolean = False  'aggiunto 16.11.2021
            Dim Eliminazione_resT As Boolean = False  'aggiunto 16.11.2021

            For i = 0 To listPrenotazioniCosti.Items.Count - 1

                id_gruppoT = listPrenotazioniCosti.Items(i).FindControl("id_gruppoLabel")

                If id_gruppoT.Text = idgruppo Then 'id_gruppoSceltoT.Text Then
                    check_attualeT = listPrenotazioniCosti.Items(i).FindControl("chkScegli")
                    check_oldT = listPrenotazioniCosti.Items(i).FindControl("chkOldScegli")
                    id_elementoT = listPrenotazioniCosti.Items(i).FindControl("id_elemento")

                    If check_attualeT.Checked = True Then
                        tResponse += id_elementoT.Text & ","        'creo stringa con i ck attivi
                    End If

                    If check_oldT.Checked = True Then
                        tResponseOLD += id_elementoT.Text & ","     'creo stringa con i ck OLD
                    End If

                End If

            Next

        End If

        dropTipoSconto.SelectedValue = "1"      'punto 18 email 26.02.2021






        'reset DIM x ItemDatabound 09.12.2021
        ib_RF = False
        ib_RD = False
        ib_EliRes = False
        ib_PPLus = False

        'richiama e ricalcola

        'Page_Load 

        'test ck elementi 05.01.2022
        'Response.Write("-->:" & tt & " - RDck:" & RD_ck.ToString & ",RFck:" & RF_ck.ToString & ",RDpre:" & RD_prepagato.ToString & ",RFpre:" & RF_prepagato.ToString & "<br/>")


        ' cambio colore 02.02.2022
        'If btn.Text = "Chiudi" Then
        ' btnAnnullaDocumento.BackColor = Drawing.Color.Gray
        'Else
        ' btnAnnullaDocumento.BackColor = Drawing.ColorTranslator.FromHtml("#e88532")
        'End If

        'test
        'btnPagamento.Visible = True

        If Request.Cookies("SicilyRentCar")("idUtente") = "8" Or Request.Cookies("SicilyRentCar")("idUtente") = "5" Then
            btnPagamento.Visible = True
        End If

        'Sempre attivo 11.03.2022
        dropTipoCliente.Enabled = True
        'dropTipoCliente.BackColor = Drawing.Color.White

        'richiamata da lista ricerca
        '# salvo aggiunto 24.02.2023/ 25.02.2023 e non visibili in produzione
        'valorizza campi originali x successivi confronti
        Dim setValueBoolean As Boolean = False
        If Request.Cookies("SicilyRentCar")("idUtente") = "5" Then
            setValueBoolean = True
        End If
        lbl_StazionePickUp_OLD.Visible = setValueBoolean
        lbl_DaData_Old.Visible = setValueBoolean
        lbl_DaOre_Old.Visible = setValueBoolean
        lbl_DaMinuti_Old.Visible = setValueBoolean
        lbl_stazioneDropOff_OLD.Visible = setValueBoolean
        lbl_AData_old.Visible = setValueBoolean
        lbl_AOre_old.Visible = setValueBoolean
        lbl_AMinuti_old.Visible = setValueBoolean
        lbl_numeroGiorni_old.Visible = setValueBoolean
        lbl_valore_tariffa_ori.Visible = setValueBoolean
        '@end salvo




    End Sub

    Protected Function SetDepositoCauzionale(iddoc As String, idgruppo As String, numcalcolo As String, refreshdatabind As Boolean) As String

        Dim deposito_cauzionale As String = ""
        Dim cf As String = ""


        'recupera valore originario del deposito cauzionale 26.01.22
        Dim impDepDefault As String = GetValoreDepositoCauzionaleDefault(idgruppo)
        Dim impDepCalcolato As String = impDepDefault

        'se PPLUS o Elires 26.01.22 modifica deposito cauzionale
        If VerificaOpzione(listPrenotazioniCosti, "248", "ck") = "True" Or VerificaOpzione(listPrenotazioniCosti, "223", "ck") = "True" Then
            'Riduce importo Deposito cauzionale 26.01.22
            impDepCalcolato = "200"
        End If

        'se Ambedue RIDUZIONI ATTIVE e PPLUS disabilitate riduce valore deposito cauzionale 
        ' ma senza PPLUS Attiva
        If VerificaOpzione(listPrenotazioniCosti, "248", "ck") = "False" And VerificaOpzione(listPrenotazioniCosti, "100", "ck") = "True" _
                    And VerificaOpzione(listPrenotazioniCosti, "170", "ck") = "True" And VerificaOpzione(listPrenotazioniCosti, "223", "ck") = "False" Then
            impDepCalcolato = "300"
        End If

        'Nessuna opzione attiva 28.01.22
        If VerificaOpzione(listPrenotazioniCosti, "248", "ck") = "False" _
                    And VerificaOpzione(listPrenotazioniCosti, "100", "ck") = "False" And VerificaOpzione(listPrenotazioniCosti, "170", "ck") = "False" _
                    And VerificaOpzione(listPrenotazioniCosti, "223", "ck") = "False" Then
            impDepCalcolato = impDepDefault
        End If

        'se opzione 'Riduzione Deposito 50%' importo = deposito / 2
        If VerificaOpzione(listPrenotazioniCosti, "234", "ck") = "True" Then
            impDepCalcolato = FormatNumber((CDbl(impDepCalcolato) / 2), 2)
        End If

        impDepCalcolato = impDepCalcolato
        ''aggiornato 27.02.2022 pren-a-3 / contratto-a-4
        funzioni_comuni.aggiorna_deposito_cauzionale("", "", iddoc, "", numcalcolo, idgruppo, "283", impDepCalcolato)


        If refreshdatabind = True Then
            listPrenotazioniCosti.DataBind()        'refresh list 28.01.2022
        End If
        'end - verifica x deposito cauzionale

        'restituisce l'importo in formato 000,00
        Return FormatNumber(impDepCalcolato, 2)


    End Function




    Protected Sub listWarningPickPrenotazioni_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listWarningPickPrenotazioni.ItemDataBound
        Dim tipo As Label = e.Item.FindControl("tipo")
        If tipo.Text = "PICK INFO" Then
            Dim warning As Label = e.Item.FindControl("warning")
            warning.ForeColor = Drawing.Color.Red 'Yellow
            warning.Font.Bold = True
        ElseIf tipo.Text = "PICK" Then
            Dim warning As Label = e.Item.FindControl("warning")
            warning.ForeColor = Drawing.Color.Red
            warning.Font.Bold = True
        End If
    End Sub

    Protected Sub listWarningDropPrenotazioni_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listWarningDropPrenotazioni.ItemDataBound
        Dim tipo As Label = e.Item.FindControl("tipo")
        If tipo.Text = "DROP INFO" Then
            Dim warning As Label = e.Item.FindControl("warning")
            warning.ForeColor = Drawing.Color.Red   'Yellow
            warning.Font.Bold = True
        ElseIf tipo.Text = "DROP" Then
            Dim warning As Label = e.Item.FindControl("warning")
            warning.ForeColor = Drawing.Color.Red
            warning.Font.Bold = True
        End If
    End Sub

    Protected Function is_gruppo_speciale(ByVal id_gruppo As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT speciale FROM gruppi WHERE id_gruppo='" & id_gruppo & "'", Dbc)

        is_gruppo_speciale = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getNomeDittaFromEdp(ByVal codEDP As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT Rag_Soc FROM ditte WITH(NOLOCK) WHERE [CODICE EDP]='" & codEDP & "'", Dbc)

        getNomeDittaFromEdp = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getNomeDittaFromId(ByVal id_cliente As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT Rag_Soc FROM ditte WITH(NOLOCK) WHERE id_ditta='" & id_cliente & "'", Dbc)

        getNomeDittaFromId = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function



    Private Sub ScambioImportoClose(ByVal sender As Object, ByVal e As EventArgs)
        tab_pagamento.Visible = False
        tab_cerca_tariffe.Visible = True
    End Sub


    Protected Sub salvataggio_prepagata(ByVal tr As classi_pagamento.TransazioneVendita)
        salva_modifiche(True)

        If Not lblPrepagato.Visible Then
            lblPrepagato.Visible = True
            lblPrepagato.Text = "CLIENTE PREPAGATO - GG: " & txtNumeroGiorni.Text
        End If

        ricalcolaPrepagato.Text = ""
        btnRicalcola.Visible = True

        'FATTURAZIONE DEL PREPAGAMENTO
        Dim miaPrenotazioneWeb As funzioni_comuni.PrenotazioneWeb = New funzioni_comuni.PrenotazioneWeb
        With miaPrenotazioneWeb
            .CCDATA = Format(Now(), "dd/MM/yyyy")
            .NumeroGiorni = txtGiorniPrepagati.Text
            .data_uscita = txtOldDaData.Text
            .data_rientro = txtOldAData.Text
            .CCOMPAGNIA = Pagamenti.get_CCOMPAGNIA_web(tr.IDRecord)
            .CCIMPORTO = tr.Importo
            .Totale = tr.Importo
            .CodiceStazionePickUp = OldStazionePickUp.Text
            .CodiceStazioneDropOff = OldStazioneDropOff.Text
            .NumPrenotazione = txtNumPrenotazione.Text

            Dim AnnoFattura As Integer = Year(.CCDATA)
            Dim CodiceFattura As Integer = funzioni_comuni.GeneraIdFattura_Web(AnnoFattura) ' attenzione sino a quando non inserisco il record se uso l'algoritmo del max per anno non va bene... tranne a bloccare la tabella
            Dim id_pagamento As String = tr.IDRecord
            funzioni_comuni.SalvaFattura_Web(CodiceFattura, id_ditta.Text, txtNumPrenotazione.Text, miaPrenotazioneWeb, id_pagamento)
            funzioni_comuni.SalvaFatturaAres_Web(CodiceFattura, id_ditta.Text, txtNumPrenotazione.Text, miaPrenotazioneWeb, id_pagamento)
        End With
    End Sub

    Protected Sub salvataggio_prepagata_cash(ByVal importo As String, ByVal id_pagamento As String)
        salva_modifiche(True)

        If Not lblPrepagato.Visible Then
            lblPrepagato.Visible = True
            lblPrepagato.Text = "CLIENTE PREPAGATO - GG: " & txtNumeroGiorni.Text
        End If

        ricalcolaPrepagato.Text = ""
        btnRicalcola.Visible = True

        'FATTURAZIONE DEL PREPAGAMENTO
        Dim miaPrenotazioneWeb As funzioni_comuni.PrenotazioneWeb = New funzioni_comuni.PrenotazioneWeb
        With miaPrenotazioneWeb
            .CCDATA = Format(Now(), "dd/MM/yyyy")
            .NumeroGiorni = txtGiorniPrepagati.Text
            .data_uscita = txtOldDaData.Text
            .data_rientro = txtOldAData.Text
            .CCOMPAGNIA = ""
            .CCIMPORTO = importo
            .Totale = importo
            .CodiceStazionePickUp = OldStazionePickUp.Text
            .CodiceStazioneDropOff = OldStazioneDropOff.Text
            .NumPrenotazione = txtNumPrenotazione.Text

            Dim AnnoFattura As Integer = Year(.CCDATA)
            Dim CodiceFattura As Integer = funzioni_comuni.GeneraIdFattura_Web(AnnoFattura) ' attenzione sino a quando non inserisco il record se uso l'algoritmo del max per anno non va bene... tranne a bloccare la tabella
            funzioni_comuni.SalvaFattura_Web(CodiceFattura, id_ditta.Text, txtNumPrenotazione.Text, miaPrenotazioneWeb, id_pagamento)
            funzioni_comuni.SalvaFatturaAres_Web(CodiceFattura, id_ditta.Text, txtNumPrenotazione.Text, miaPrenotazioneWeb, id_pagamento)
        End With
    End Sub

    Public Sub ScambioImportoTransazioneEseguita(ByVal sender As Object, ByVal e As tabelle_pos_Scambio_Importo.TransazioneEseguitaEventArgs)
        tab_pagamento.Visible = False
        tab_cerca_tariffe.Visible = True



        Select Case e.Transazione.IDFunzione
            Case Is = enum_tipo_pagamento_ares.Richiesta
                'Dim tr As classi_pagamento.TransazionePreautorizzazione = e.Transazione
                'cPagamenti.registra_preautorizzazione(tr, txtNumPrenotazione.Text, "", "", "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
            Case Is = enum_tipo_pagamento_ares.Vendita
                Dim tr As classi_pagamento.TransazioneVendita = e.Transazione
                'cPagamenti.registra_vendita(tr, txtNumPrenotazione.Text, "", "", "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
                'SE E' STATA ESEGUITA UNA VENDITA LA PRENOTAZIONE VIENE SETTATA COME PREPAGATA - SE NON E' BROKER!

                If Not tariffa_broker.Text = "1" Then
                    salvataggio_prepagata(tr)
                End If

            Case Is = enum_tipo_pagamento_ares.Integrazione
                'Dim tr As classi_pagamento.TransazioneIntegrazione = e.Transazione
                'cPagamenti.registra_integrazione(tr, txtNumPrenotazione.Text, "", "", "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
            Case Is = enum_tipo_pagamento_ares.Chiusura
                'Dim tr As classi_pagamento.TransazioneChiusura = e.Transazione
                'cPagamenti.registra_chiusura(tr, txtNumPrenotazione.Text, "", "", "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
            Case Is = enum_tipo_pagamento_ares.Rimborso
                'Dim tr As classi_pagamento.TransazioneRimborso = e.Transazione
                'cPagamenti.registra_rimborso(tr, txtNumPrenotazione.Text, "", "", "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
            Case Is = enum_tipo_pagamento_ares.Storno_Ultima_Operazione
                'Dim tr As classi_pagamento.TransazioneStorno = e.Transazione
                'cPagamenti.registra_storno(tr, txtNumPrenotazione.Text, "", "", "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
        End Select
        'libreria.genUserMsgBox(Me, "Ricevuto evento Transazione su terminal ID " & e.Transazione.TerminalID)

        tab_dettagli_pagamento.Visible = True
        listPagamenti.DataBind()
        ImpostaPannelloPagamento(txtNumPrenotazione.Text)
    End Sub

    Protected Sub CassaPagamentoEseguito(ByVal sender As Object, ByVal e As EventoConOggetto)
        tab_pagamento.Visible = False
        tab_cerca_tariffe.Visible = True

        Dim mio_record As PAGAMENTI_EXTRA = CType(e.mioOggetto, PAGAMENTI_EXTRA)
        With mio_record
            Select Case .ID_TIPPAG
                Case enum_tipo_pagamento_p1000.CH_PAGAMENTO_CASH, enum_tipo_pagamento_p1000.MCR_PAGAMENTO_CASH
                    If Not tariffa_broker.Text = "1" Then
                        salvataggio_prepagata_cash(.PER_IMPORTO, mio_record.Nr_Contratto)
                    End If
                Case enum_tipo_pagamento_p1000.COMPLIMENTARY
                    complimentary.Text = "1"
                    lblComplimentary.Visible = True

                    omaggia_tutto_x_complimentary()

                    listPrenotazioniCosti.DataBind()
                Case enum_tipo_pagamento_p1000.FC_FULL_CREDIT
                    full_credit.Text = "1"
                    lblFullCredit.Visible = True
            End Select
        End With

        tab_dettagli_pagamento.Visible = True
        listPagamenti.DataBind()
        ImpostaPannelloPagamento(txtNumPrenotazione.Text)
    End Sub



    Protected Function check_spese_postali(ByVal metodo_spedizione As String) As Boolean
        'RESTITUISCE TRUE SE IL COSTO VIENE AGGIUNTO O SE VIENE RIMOSSO
        check_spese_postali = False

        Dim id_tariffe_righe As String = ""

        If dropTariffeGeneriche.SelectedValue <> "0" Then
            id_tariffe_righe = dropTariffeGeneriche.SelectedValue
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            id_tariffe_righe = dropTariffeParticolari.SelectedValue
        End If

        Dim sconto As Double
        If dropTipoSconto.SelectedValue = "0" Then
            sconto = CDbl(txtSconto.Text)
        ElseIf dropTipoSconto.SelectedValue = "1" Then
            sconto = 0
        End If

        Dim id_elemento_spese As String = funzioni_comuni.get_id_spese_spedizione_postali()

        If id_elemento_spese <> "0" Then
            Dim accessorio_esistente As Boolean = Not funzioni_comuni.accessorioExtraNonAggiunto(id_elemento_spese, id_gruppo_auto_scelto.Text, "", idPrenotazione.Text, "", numCalcolo.Text)
            'SE LA DITTA COLLEGATA PREVEDE LA SPEDIZIONE POSTALE DELLA FATTURA AGGIUNGO IL COSTO 'SPEDE DI SPEDIZIONE POSTALI
            If metodo_spedizione = "P" And Not accessorio_esistente Then
                Dim imposta_prepagato As Boolean = False
                If btnRicalcolaPrepagato.Text = "1" Then
                    imposta_prepagato = True
                End If
                funzioni.aggiungi_accessorio_obbligatorio(id_elemento_spese, dropStazionePickUp.SelectedValue, dropStazioneDropOff.SelectedValue, id_gruppo_auto_scelto.Text, txtNumeroGiorni.Text, 0, imposta_prepagato, Nothing, "", "", sconto, id_tariffe_righe, "", "", idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                listPrenotazioniCosti.DataBind()
                check_spese_postali = True
            ElseIf metodo_spedizione <> "P" And accessorio_esistente Then
                'IN QUESTO CASO L'ACCESSORIO ERA STATO AGGIUNTO MA ADESSO L'AZIENDA SELEZIONATA NON RICHIEDE LA SPEDIZIONE PER POSTA
                Dim omaggiato As Boolean
                Dim id_ele As Label

                'CONTROLLO SE ERA STATO OMAGGIATO
                For i = 0 To listPrenotazioniCosti.Items.Count - 1
                    id_ele = listPrenotazioniCosti.Items(i).FindControl("id_elemento")
                    If id_ele.Text = id_elemento_spese Then
                        Dim chkOldOmaggio As CheckBox = listPrenotazioniCosti.Items(i).FindControl("chkOldOmaggio")
                        If chkOldOmaggio.Checked Then
                            omaggiato = True
                        Else
                            omaggiato = False
                        End If
                        Exit For
                    End If
                Next

                If Not omaggiato Then
                    funzioni.rimuovi_costo_accessorio("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, id_elemento_spese, "", "EXTRA", dropTipoCommissione.SelectedValue)
                    check_spese_postali = True
                Else
                    funzioni.omaggio_accessorio(False, False, True, "", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, id_elemento_spese, "", dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, "", "")
                    check_spese_postali = True
                End If

                listPrenotazioniCosti.DataBind()
            End If
        End If
    End Function

    Private Sub scegli_ditta(ByVal sender As Object, ByVal e As anagrafica_anagrafica_ditte.ScegliDittaEventArgs)
        txtNomeDitta.Text = e.ragione_sociale
        id_ditta.Text = e.id_ditta

        check_spese_postali(e.metodo_spedizione)

        anagrafica_ditte.Visible = False
    End Sub

    Private Sub scegli_conduente(ByVal sender As Object, ByVal e As anagrafica_anagrafica_conducenti.ScegliConducenteEventArgs)
        id_conducente.Text = e.id_conducente

        image_primo_guidatore.Visible = True
        vediPrimoGuidatore.HRef = "contratto_vedi_guidatore.aspx?idUtente=" & id_conducente.Text

        txtNomeConducente.Text = e.nome_conducente
        txtNomeConducente.ReadOnly = True
        txtCognomeConducente.Text = e.cognome_conducente
        txtCognomeConducente.ReadOnly = True
        txtMailConducente.Text = e.email_conducente
        txtIndirizzoConducente.Text = e.indirizzo_conducente

        txtDataDiNascita.Text = e.data_nascita
        txtDataDiNascita.ReadOnly = True

        btnModificaConducente.Text = "Scegli"

        'SE E' STATO SELEZIONATO UN UTENTE E' NECESSARIO CONTROLLARNE L'ETA' E SE DEVE ESSERE AGGIUNTO/RIMOSSO LO YOUNG DRIVER -----------
        If txtDataDiNascita.Text <> "" Then
            Dim test_eta As Integer
            Dim month_nascita As Integer = Month(txtDataDiNascita.Text)
            Dim day_nascita As Integer = Day(txtDataDiNascita.Text)
            Dim data_nascita As DateTime = getDataDb_senza_orario2(txtDataDiNascita.Text)

            test_eta = DateDiff(DateInterval.Year, data_nascita, Now())

            If Month(Now()) < month_nascita Then
                test_eta = CInt(test_eta) - 1
            ElseIf Month(Now()) = month_nascita And Day(Now()) < day_nascita Then
                test_eta = CInt(test_eta) - 1
            End If

            'SE L'ETA' E' LA STESSA SEGNALATA AD INIZIO PROCEDURA NULLA DEVE ESSERE FATTO - ALTRIMENTI IN QUESTA FASE SI AVVERTE SOLAMENTE
            'CIRCA L'EVENTUALE MODIFICHE DA EFFETTUARE CIRCA LO YOUNG DRIVER
            If CStr(test_eta) <> txtEtaPrimo.Text Then
                Dim check_eta As String = funzioni_comuni.gruppo_vendibile_eta_guidatori(id_gruppo_auto_scelto.Text, test_eta, "", "", "", "", "", "", "", False)
                If check_eta = "0" Then
                    'L'AUTO NON E' VENDIBILE - NON E' POSSIBILE COLLEGARE IL GUIDATORE ALLA PRENOTAZIONE DA SALVARE
                    id_conducente.Text = ""

                    txtNomeConducente.ReadOnly = False
                    txtNomeConducente.Text = ""

                    txtCognomeConducente.ReadOnly = False
                    txtCognomeConducente.Text = ""

                    txtMailConducente.Text = ""
                    txtIndirizzoConducente.Text = ""
                    txtDataDiNascita.ReadOnly = False
                    txtDataDiNascita.Text = ""

                    image_primo_guidatore.Visible = False

                    Libreria.genUserMsgBox(Me, "Impossibile selezionare il guidatore scelto: gruppo auto non vendibile a causa dell'età.")
                ElseIf check_eta = "1" Then
                    'IN QUESTO CASO IL GRUPPO AUTO E' VENDIBILE MA CON SUPPLEMENTO YOUNG DRIVER CHE DEVE ESSERE AGGIUNTO AUTOMATICAMENTE
                    'txtEtaPrimo.Text = test_eta

                    'nuovo_accessorio(get_id_young_driver(), id_gruppo_auto_scelto.Text, "YOUNG PRIMO", test_eta, "")
                    'listPrenotazioniCosti.DataBind()

                    Libreria.genUserMsgBox(Me, "Gruppo auto vendibile con supplemento Young Driver - Cliccare su 'Modifica dati cliente per confermare.")
                ElseIf check_eta = "4" Then
                    'VENDIBILE SENZA LO YOUNG DRIVER - QUALORA FOSSE STATO PRECEDENTEMENTE AGGIUNTO PROVVEDO ALLA SUA RIMOZIONE
                    'txtEtaPrimo.Text = test_eta

                    If funzioni.esiste_young_driver_primo_guidatore(get_id_young_driver(), id_gruppo_auto_scelto.Text, numCalcolo.Text, "", idPrenotazione.Text, "", "") Then
                        '    funzioni.rimuovi_costo_accessorio("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, get_id_young_driver(), "1", "EXTRA")
                        '    listPrenotazioniCosti.DataBind()
                        Libreria.genUserMsgBox(Me, "Verrà rimosso lo Young driver per il primo guidatore. - Cliccare su 'Modifica dati cliente per confermare.")
                    End If
                End If
            End If
        End If

        '---------------------------------------------------------------------------------------------------------------------------------

        anagrafica_conducenti.Visible = False
    End Sub


    Protected Function cerca_fase1() As Integer
        'PRIMA FASE DELLA RICERCA TARIFFA - DATA/STAZIONE/FONTE
        'RESTITUISCE 1 SE SONO PRESENTI DEI WARNING - RESTITUISCE 0 SE NON E' STATO GENERATO ALCUN WARNING
        'txtSconto.Text = ""

        fonte_stop_sell.Visible = False

        Dim daData As String = txtDaData.Text
        Dim aData As String = txtAData.Text

        Dim data_pick_up_db As String = getDataDb_senza_orario(daData, Request.ServerVariables("HTTP_HOST"))

        Dim stazione_permette_VAL_verso_altre_stazioni As Boolean = True
        Dim stazione_accetta_VAL_da_altre_stazioni As Boolean = True

        Dim num_gruppi_selezionati As Integer = 0

        'CONTROLLO SU APERTURA PICK UP
        Dim stazione_aperta_pick_up As String = funzioni_comuni.stazione_aperta_pick_up(dropStazionePickUp.SelectedValue, daData, ore1.Text, minuti1.Text, "", "", idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
        Dim stazione_aperta_drop_off As String = funzioni_comuni.stazione_aperta_drop_off(dropStazioneDropOff.SelectedValue, aData, ore2.Text, minuti2.Text, "", "", idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))

        Dim pick_up_on_request As String = funzioni_comuni.stazione_pick_up_on_request(dropStazionePickUp.SelectedValue, "", "", idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
        Dim drop_off_on_request As String = funzioni_comuni.stazione_drop_off_on_request(dropStazioneDropOff.SelectedValue, "", "", idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))

        'CONTROLLO EVENTUALE VAL 
        If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
            stazione_permette_VAL_verso_altre_stazioni = funzioni_comuni.stazione_permette_VAL_verso_altre_stazioni(dropStazionePickUp.SelectedValue, "", "", idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
            stazione_accetta_VAL_da_altre_stazioni = funzioni_comuni.stazione_accetta_VAL_da_altre_stazioni(dropStazioneDropOff.SelectedValue, "", "", idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))
        End If

        'CONTROLLO SE LA STAZIONE DI PICK UP O IL SINGOLO GRUPPO SONO IN STOP SELL
        Dim stazione_in_stop_sell As Boolean = funzioni_comuni.stazioneInStopSell(dropStazionePickUp.SelectedValue, data_pick_up_db, "", "", idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))

        'SE E' STATA SELEZIONATA UNA FONTE CONTROLLO SE E' IN STOP SELL
        Dim fonte_in_stop_sell As Boolean
        If dropTipoCliente.SelectedValue <> "0" Then
            fonte_in_stop_sell = funzioni_comuni.fonteInStopSell(dropStazionePickUp.SelectedValue, dropTipoCliente.SelectedValue, data_pick_up_db, "", "", idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"))

            If fonte_in_stop_sell Then
                fonte_stop_sell.Visible = True
            End If
        End If

        If CInt(stazione_aperta_pick_up) < 2 Or CInt(stazione_aperta_drop_off) < 2 Or Not stazione_permette_VAL_verso_altre_stazioni Or Not stazione_accetta_VAL_da_altre_stazioni Or stazione_in_stop_sell Or fonte_in_stop_sell Then
            cerca_fase1 = 1
        Else
            cerca_fase1 = 0
        End If

        listWarningPickPrenotazioni.DataBind()
        listWarningDropPrenotazioni.DataBind()
    End Function

    Protected Sub cercaPrenotazioneFase1()
        Dim ricerca As Integer = cerca_fase1()

        lblMinGiorniNolo.Text = ""

        If ricerca = 1 Then
            'SE CI SONO WARNING
            table_warning.Visible = True
            table_gruppi.Visible = False
            table_tariffe.Visible = False
        ElseIf ricerca = 0 Then
            'SE NON CI SONO WARNING
            table_warning.Visible = False
            setQueryTariffePossibili(0)
            table_gruppi.Visible = True
            table_tariffe.Visible = True

            listGruppi.DataBind()
            'RISELEZIONO IL VECCHIO GRUPPO E LA VECCHIA TARIFFA, SE VENDIBILI
            For i = 0 To listGruppi.Items.Count - 1
                Dim id_gr As Label = listGruppi.Items(i).FindControl("id_gruppo")
                If id_gr.Text = id_gruppo_auto_scelto.Text Then
                    Dim sel_gruppo As CheckBox = listGruppi.Items(i).FindControl("sel_gruppo")
                    If sel_gruppo.Enabled Then
                        sel_gruppo.Checked = True
                    End If
                End If
            Next

            Try
                dropTariffeParticolari.SelectedValue = id_tariffa_broker.Text
            Catch ex As Exception

            End Try
        End If

        'DISABILITO I PULSANTI PER LA RICERCA DELLA TARIFFA ---------------------------------------------------------------------------------
        btnCerca.Visible = False
        riga_broker.Visible = False
        dropStazionePickUp.Enabled = False
        dropStazioneDropOff.Enabled = False
        txtoraPartenza.Enabled = False
        txtOraRientro.Enabled = False
        minuti1.Enabled = False
        ore1.Enabled = False
        ore2.Enabled = False
        minuti2.Enabled = False
        dropTipoCliente.Enabled = False
        txtDaData.Enabled = False
        txtAData.Enabled = False
        txtEtaPrimo.Enabled = False
        txtEtaSecondo.Enabled = False
        txtNumeroGiorni.Enabled = False
        btnAnnulla0.Visible = False
        '-----------------------------------------------------------------------------------------------------------------------------------

        btnAnnulla2.Visible = True
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerca.Click
        'SI PUO' CLICCARE SU QUESTO PULSANTE QUANDO UNA PRENOTAZIONE E' TOTALMENTE MODIFICABILE (ATTUALMENTE: BROKER) - CONTROLLO LA
        'CORRETTEZZA DELLE DATE
        Dim uscita As DateTime = funzioni_comuni.getDataDb_con_orario2(txtDaData.Text & " 00:00:00")
        Dim rientro As DateTime = funzioni_comuni.getDataDb_con_orario2(txtAData.Text & " 23:59:59")
        Dim oggi As DateTime = funzioni_comuni.getDataDb_con_orario2(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()) & " 23:59:59")

        If DateDiff(DateInterval.Day, oggi, uscita) >= 0 Then
            If (DateDiff(DateInterval.Day, uscita, rientro) > 0) Or (DateDiff(DateInterval.Day, uscita, rientro) = 0 And Hour(txtOraRientro.Text) > Hour(txtoraPartenza.Text)) Or (DateDiff(DateInterval.Day, uscita, rientro) = 0 And Hour(txtOraRientro.Text) = Hour(txtoraPartenza.Text) And Minute(txtOraRientro.Text) > Minute(txtoraPartenza.Text)) Then

                ore1.Text = Hour(txtoraPartenza.Text)
                minuti1.Text = Minute(txtoraPartenza.Text)

                ore2.Text = Hour(txtOraRientro.Text)
                minuti2.Text = Minute(txtOraRientro.Text)

                If Len(ore1.Text) = 1 Then
                    ore1.Text = "0" & ore1.Text
                End If
                If Len(ore2.Text) = 1 Then
                    ore2.Text = "0" & ore2.Text
                End If
                If Len(minuti1.Text) = 1 Then
                    minuti1.Text = "0" & minuti1.Text
                End If
                If Len(minuti2.Text) = 1 Then
                    minuti2.Text = "0" & minuti2.Text
                End If

                cercaPrenotazioneFase1()
                'VISTO CHE LA PRENOTAZIONE E' BROKER NON E' POSSIBILE SCEGLIERE UNA TARIFFA GENERICA
                dropTariffeGeneriche.Enabled = False
            Else
                Libreria.genUserMsgBox(Me, "Attenzione: la data di rientro è precedente a quella di uscita.")
            End If
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: la data di uscita è precedente alla data odierna.")
        End If
    End Sub

    Protected Sub setQueryTariffePossibili(ByVal id_pren As Integer)

        'SE VIENE PASSATO UN id_tariffa ESEGUO LA RICERCA SOLAMENTE PER LA TARIFFA RICHIESTA (SERVE PER QUANDO SI RICHIAMA UN PREVENTIVO)
        Dim daData As String = getDataDb_senza_orario(txtDaData.Text, Request.ServerVariables("HTTP_HOST"))
        Dim aData As String = getDataDb_senza_orario(txtAData.Text, Request.ServerVariables("HTTP_HOST"))

        Dim condizione_id_prev As String = ""
        If id_pren <> 0 Then
            condizione_id_prev = " AND tariffe.id=" & id_pren
        End If

        'QUERY
        'PARTE 1: SELEZIONO LE TARIFFE GENERICHE (PER CUI TUTTE LE FONTI SONO POSSIBILI - TUTTE LE STAZIONI SONO POSSIBILI)
        'COME ID SELEZIONO DIRETTAMENTE LA RIGA DI tariffe_righe DA CUI POI POSSO SUBITO OTTENERE LA CONDIZIONE E IL TEMPO-KM
        'DA UTILIZZARE
        sqlTariffeGeneriche.SelectCommand = "(SELECT tariffe_righe.id,tariffe.codice FROM tariffe WITH(NOLOCK) " &
        "INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
        "WHERE tariffe.attiva='1' " & condizione_id_prev & " AND convert(datetime,'" & daData & "',102) BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " &
        "AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a " &
        "AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa  FROM tariffe_X_stazioni WITH(NOLOCK) WHERE id_tariffa=tariffe.id) " &
        "AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa  FROM tariffe_X_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id) " &
        "AND convert(datetime,'" & aData & "',102) <= ISNULL(max_data_rientro, '3000-12-12 23:59:59'))" 'MODIFICARE LA QUERY ANCHE IN RIBALTAMENTO.ASPX

        'PARTE 2: SELEZIONO LE TARIFFE VENDIBILI PER LA STAZIONE SPECIFICATA E PER L'EVENTUALE FONTE 
        'COME ID SELEZIONO DIRETTAMENTE LA RIGA DI tariffe_righe DA CUI POI POSSO SUBITO OTTENERE LA CONDIZIONE E IL TEMPO-KM
        'DA UTILIZZARE

        Dim condizione_nome_tariffa_fonte As String = "NULL"
        'If dropTipoCliente.SelectedValue > 0 Then
        '    condizione_nome_tariffa_fonte = "(SELECT nome_tariffa FROM tariffe_X_fonti WHERE tariffe_x_fonti.id_tariffa=tariffe.id AND id_tipologia_cliente='" & dropTipoCliente.SelectedValue & "')"
        'End If

        sqlTariffeParticolari.SelectCommand = "SELECT tariffe_righe.id, ISNULL(" & condizione_nome_tariffa_fonte & ",tariffe.codice) As codice FROM tariffe WITH(NOLOCK) " &
        "INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
        "WHERE tariffe.attiva='1' " & condizione_id_prev & " AND convert(datetime,'" & daData & "',102) BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " &
        "AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a " &
        "AND convert(datetime,'" & aData & "',102) <= ISNULL(max_data_rientro, '3000-12-12 23:59:59') " &
        "AND ( (" &
        "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & dropStazionePickUp.SelectedValue & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND " &
        "(tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & dropStazioneDropOff.SelectedValue & "' AND tipo='DROP' AND id_tariffa=tariffe.id))) " &
        " OR " &
        "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & dropStazionePickUp.SelectedValue & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND " &
        "(tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='DROP' AND id_tariffa=tariffe.id))) " &
        " OR " &
        "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & dropStazioneDropOff.SelectedValue & "' AND tipo='DROP' AND id_tariffa=tariffe.id)) AND " &
        "(tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='PICK' AND id_tariffa=tariffe.id))) ) " 'MODIFICARE LA QUERY ANCHE IN RIBALTAMENTO.ASPX


        If dropTipoCliente.SelectedValue > 0 Then
            'SE E' STATA SELEZIONATA UNA FONTE CONTROLLO ANCHE TRA I REQUISITI PER FONTE DELLE TARIFFE
            'UNENDO IN OR (L'ULTIMO) LA POSSIBILITA CHE LA TARIFFA NON SPECIFICHI ALCUNA STAZIONE MA SPECIFICHI UNA O PIU' FONTI
            sqlTariffeParticolari.SelectCommand = sqlTariffeParticolari.SelectCommand & "" &
            "AND (tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tipologia_cliente='" & dropTipoCliente.SelectedValue & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da))" &
            "OR tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id)) " &
            "OR (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='PICK' AND id_tariffa=tariffe.id) AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='DROP' AND id_tariffa=tariffe.id) AND tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tipologia_cliente='" & dropTipoCliente.SelectedValue & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da)) )) "
        Else
            'SE NON E' STATA SELEZIONATA UNA FONTE PRELEVO SOLAMENTE LE TARIFFE CHE NON HANNO ALCUN PREREQUISITO DI FONTE
            sqlTariffeParticolari.SelectCommand = sqlTariffeParticolari.SelectCommand & "" &
            "AND (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id)))"
        End If

        'DO LA POSSIBILITA' DI MANTENERE LA TARIFFA PRENOTATA ANCHE SE OGGI NON PIU' VENDIBILE
        Dim tar_attuale_part As String = ""
        Dim id_tar_attuale_part As String = ""

        Dim tar_attuale_gen As String = ""
        Dim id_tar_attuale_gen As String = ""

        If dropTariffeParticolari.SelectedValue <> "0" Then
            'SE C'E' PIU' DI UNA TARIFFA VUOL DIRE CHE HO GIA' EFFETTUATO UNA MODIFICA - SELEZIONO QUELLA DI PRENOTAZIONE (POTREBBE NON ESSERE
            'SELEZIONATA

            If dropTariffeParticolari.Items.Count = 2 Then
                tar_attuale_part = dropTariffeParticolari.SelectedItem.Text.Replace(" (PREN)", "") & " (PREN)"
                id_tar_attuale_part = dropTariffeParticolari.SelectedValue
            Else
                For i = 0 To dropTariffeParticolari.Items.Count - 1
                    If dropTariffeParticolari.Items(i).Text.Contains(" (PREN)") Then
                        tar_attuale_part = dropTariffeParticolari.Items(i).Text
                        id_tar_attuale_part = dropTariffeParticolari.Items(i).Value
                    End If
                Next
            End If
        End If

        If dropTariffeGeneriche.SelectedValue <> "0" Then
            If dropTariffeGeneriche.Items.Count = 2 Then
                tar_attuale_gen = dropTariffeGeneriche.SelectedItem.Text.Replace(" (PREN)", "") & " (PREN)"
                id_tar_attuale_gen = dropTariffeGeneriche.SelectedValue
            Else
                For i = 0 To dropTariffeGeneriche.Items.Count - 1
                    If dropTariffeGeneriche.Items(i).Text.Contains(" (PREN)") Then
                        tar_attuale_gen = dropTariffeGeneriche.Items(i).Text
                        id_tar_attuale_gen = dropTariffeGeneriche.Items(i).Value
                    End If
                Next
            End If
        End If

        dropTariffeGeneriche.Items.Clear()
        dropTariffeGeneriche.Items.Add("Seleziona...")
        dropTariffeGeneriche.Items(0).Value = 0
        dropTariffeParticolari.Items.Clear()
        dropTariffeParticolari.Items.Add("Seleziona...")
        dropTariffeParticolari.Items(0).Value = 0
        If tar_attuale_part <> "" Then
            dropTariffeParticolari.Items.Add(tar_attuale_part)
            dropTariffeParticolari.Items(1).Value = id_tar_attuale_part
        End If

        If tar_attuale_gen <> "" Then
            dropTariffeGeneriche.Items.Add(tar_attuale_gen)
            dropTariffeGeneriche.Items(1).Value = id_tar_attuale_gen
        End If

        dropTariffeGeneriche.DataBind()
        dropTariffeParticolari.DataBind()

        'RIMUOVO UN EVENTUALE DUPLICATO SE LA TARIFFA PRENOTATA E' ANCORA VENDIBILE E QUINDI E' PRESENTE DUE VOLTE
        If tar_attuale_part <> "" Then
            For i = 2 To dropTariffeParticolari.Items.Count - 1
                If dropTariffeParticolari.Items(i).Value = id_tar_attuale_part Then
                    dropTariffeParticolari.Items.RemoveAt(i)
                    Exit For
                End If
            Next
        End If

        If tar_attuale_gen <> "" Then
            For i = 2 To dropTariffeGeneriche.Items.Count - 1
                If dropTariffeGeneriche.Items(i).Value = id_tar_attuale_gen Then
                    dropTariffeGeneriche.Items.RemoveAt(i)
                    Exit For
                End If
            Next
        End If
    End Sub


    Protected Sub setQueryTariffePossibili_NEW(ByVal id_pren As Integer)

        '#Salvo aggiunta per visualizzare le tariffe in caso di cambio PickUP 20.02.2023


        'SE VIENE PASSATO UN id_tariffa ESEGUO LA RICERCA SOLAMENTE PER LA TARIFFA RICHIESTA (SERVE PER QUANDO SI RICHIAMA UN PREVENTIVO)
        Dim daData As String = getDataDb_senza_orario(txtDaData.Text, Request.ServerVariables("HTTP_HOST"))
        Dim aData As String = getDataDb_senza_orario(txtAData.Text, Request.ServerVariables("HTTP_HOST"))

        Dim condizione_id_prev As String = ""
        If id_pren <> 0 Then
            condizione_id_prev = " AND tariffe.id=" & id_pren
        End If

        'QUERY
        'PARTE 1: SELEZIONO LE TARIFFE GENERICHE (PER CUI TUTTE LE FONTI SONO POSSIBILI - TUTTE LE STAZIONI SONO POSSIBILI)
        'COME ID SELEZIONO DIRETTAMENTE LA RIGA DI tariffe_righe DA CUI POI POSSO SUBITO OTTENERE LA CONDIZIONE E IL TEMPO-KM
        'DA UTILIZZARE
        sqlTariffeGeneriche.SelectCommand = "(SELECT tariffe_righe.id,tariffe.codice FROM tariffe WITH(NOLOCK) " &
        "INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
        "WHERE tariffe.attiva='1' " & condizione_id_prev & " AND convert(datetime,'" & daData & "',102) BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " &
        "AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a " &
        "AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa  FROM tariffe_X_stazioni WITH(NOLOCK) WHERE id_tariffa=tariffe.id) " &
        "AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa  FROM tariffe_X_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id) " &
        "AND convert(datetime,'" & aData & "',102) <= ISNULL(max_data_rientro, '3000-12-12 23:59:59'))" 'MODIFICARE LA QUERY ANCHE IN RIBALTAMENTO.ASPX

        'PARTE 2: SELEZIONO LE TARIFFE VENDIBILI PER LA STAZIONE SPECIFICATA E PER L'EVENTUALE FONTE 
        'COME ID SELEZIONO DIRETTAMENTE LA RIGA DI tariffe_righe DA CUI POI POSSO SUBITO OTTENERE LA CONDIZIONE E IL TEMPO-KM
        'DA UTILIZZARE

        Dim condizione_nome_tariffa_fonte As String = "NULL"
        'If dropTipoCliente.SelectedValue > 0 Then
        '    condizione_nome_tariffa_fonte = "(SELECT nome_tariffa FROM tariffe_X_fonti WHERE tariffe_x_fonti.id_tariffa=tariffe.id AND id_tipologia_cliente='" & dropTipoCliente.SelectedValue & "')"
        'End If

        sqlTariffeParticolari.SelectCommand = "SELECT tariffe_righe.id, ISNULL(" & condizione_nome_tariffa_fonte & ",tariffe.codice) As codice FROM tariffe WITH(NOLOCK) " &
        "INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
        "WHERE tariffe.attiva='1' " & condizione_id_prev & " AND convert(datetime,'" & daData & "',102) BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " &
        "AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a " &
        "AND convert(datetime,'" & aData & "',102) <= ISNULL(max_data_rientro, '3000-12-12 23:59:59') " &
        "AND ( (" &
        "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & dropStazionePickUp.SelectedValue & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND " &
        "(tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & dropStazioneDropOff.SelectedValue & "' AND tipo='DROP' AND id_tariffa=tariffe.id))) " &
        " OR " &
        "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & dropStazionePickUp.SelectedValue & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND " &
        "(tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='DROP' AND id_tariffa=tariffe.id))) " &
        " OR " &
        "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & dropStazioneDropOff.SelectedValue & "' AND tipo='DROP' AND id_tariffa=tariffe.id)) AND " &
        "(tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='PICK' AND id_tariffa=tariffe.id))) ) " 'MODIFICARE LA QUERY ANCHE IN RIBALTAMENTO.ASPX


        If dropTipoCliente.SelectedValue > 0 Then
            'SE E' STATA SELEZIONATA UNA FONTE CONTROLLO ANCHE TRA I REQUISITI PER FONTE DELLE TARIFFE
            'UNENDO IN OR (L'ULTIMO) LA POSSIBILITA CHE LA TARIFFA NON SPECIFICHI ALCUNA STAZIONE MA SPECIFICHI UNA O PIU' FONTI
            sqlTariffeParticolari.SelectCommand = sqlTariffeParticolari.SelectCommand & "" &
            "AND (tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tipologia_cliente='" & dropTipoCliente.SelectedValue & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da))" &
            "OR tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id)) " &
            "OR (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='PICK' AND id_tariffa=tariffe.id) AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='DROP' AND id_tariffa=tariffe.id) AND tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tipologia_cliente='" & dropTipoCliente.SelectedValue & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da)) )) "
        Else
            'SE NON E' STATA SELEZIONATA UNA FONTE PRELEVO SOLAMENTE LE TARIFFE CHE NON HANNO ALCUN PREREQUISITO DI FONTE
            sqlTariffeParticolari.SelectCommand = sqlTariffeParticolari.SelectCommand & "" &
            "AND (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id)))"
        End If

        'DO LA POSSIBILITA' DI MANTENERE LA TARIFFA PRENOTATA ANCHE SE OGGI NON PIU' VENDIBILE
        Dim tar_attuale_part As String = ""
        Dim id_tar_attuale_part As String = ""

        Dim tar_attuale_gen As String = ""
        Dim id_tar_attuale_gen As String = ""

        If dropTariffeParticolari.SelectedValue <> "0" Then
            'SE C'E' PIU' DI UNA TARIFFA VUOL DIRE CHE HO GIA' EFFETTUATO UNA MODIFICA - SELEZIONO QUELLA DI PRENOTAZIONE (POTREBBE NON ESSERE
            'SELEZIONATA

            If dropTariffeParticolari.Items.Count = 2 Then
                tar_attuale_part = dropTariffeParticolari.SelectedItem.Text.Replace(" (PREN)", "") & " (PREN)"
                id_tar_attuale_part = dropTariffeParticolari.SelectedValue
            Else
                For i = 0 To dropTariffeParticolari.Items.Count - 1
                    If dropTariffeParticolari.Items(i).Text.Contains(" (PREN)") Then
                        tar_attuale_part = dropTariffeParticolari.Items(i).Text
                        id_tar_attuale_part = dropTariffeParticolari.Items(i).Value
                    End If
                Next
            End If
        End If

        If dropTariffeGeneriche.SelectedValue <> "0" Then
            If dropTariffeGeneriche.Items.Count = 2 Then
                tar_attuale_gen = dropTariffeGeneriche.SelectedItem.Text.Replace(" (PREN)", "") & " (PREN)"
                id_tar_attuale_gen = dropTariffeGeneriche.SelectedValue
            Else
                For i = 0 To dropTariffeGeneriche.Items.Count - 1
                    If dropTariffeGeneriche.Items(i).Text.Contains(" (PREN)") Then
                        tar_attuale_gen = dropTariffeGeneriche.Items(i).Text
                        id_tar_attuale_gen = dropTariffeGeneriche.Items(i).Value
                    End If
                Next
            End If
        End If

        dropTariffeGeneriche.Items.Clear()
        dropTariffeGeneriche.Items.Add("Seleziona...")
        dropTariffeGeneriche.Items(0).Value = 0
        dropTariffeParticolari.Items.Clear()
        dropTariffeParticolari.Items.Add("Seleziona...")
        dropTariffeParticolari.Items(0).Value = 0
        If tar_attuale_part <> "" Then
            dropTariffeParticolari.Items.Add(tar_attuale_part)
            dropTariffeParticolari.Items(1).Value = id_tar_attuale_part
        End If

        If tar_attuale_gen <> "" Then
            dropTariffeGeneriche.Items.Add(tar_attuale_gen)
            dropTariffeGeneriche.Items(1).Value = id_tar_attuale_gen
        End If

        dropTariffeGeneriche.DataBind()
        dropTariffeParticolari.DataBind()

        'RIMUOVO UN EVENTUALE DUPLICATO SE LA TARIFFA PRENOTATA E' ANCORA VENDIBILE E QUINDI E' PRESENTE DUE VOLTE
        If tar_attuale_part <> "" Then
            For i = 2 To dropTariffeParticolari.Items.Count - 1
                If dropTariffeParticolari.Items(i).Value = id_tar_attuale_part Then
                    dropTariffeParticolari.Items.RemoveAt(i)
                    Exit For
                End If
            Next
        End If

        If tar_attuale_gen <> "" Then
            For i = 2 To dropTariffeGeneriche.Items.Count - 1
                If dropTariffeGeneriche.Items(i).Value = id_tar_attuale_gen Then
                    dropTariffeGeneriche.Items.RemoveAt(i)
                    Exit For
                End If
            Next
        End If
    End Sub

    Protected Sub btnContinua_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinua.Click
        btnAnnulla1.Visible = False
        table_warning.Visible = False
        setQueryTariffePossibili(0)
        table_gruppi.Visible = True
        table_tariffe.Visible = True

        listGruppi.DataBind()
    End Sub

    Protected Sub listGruppi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listGruppi.ItemDataBound
        Dim id_gruppo As Label = e.Item.FindControl("id_gruppo")

        If funzioni_comuni.gruppo_vendibile_eta_guidatori(id_gruppo.Text, Trim(txtEtaPrimo.Text), Trim(txtEtaSecondo.Text), "", "", "", "", "", "", False) <> 0 Then
            If Not funzioni_comuni.gruppo_vendibile_pick_up(dropStazionePickUp.SelectedValue, id_gruppo.Text, "", "", "", "", "", "", False) Then
                Dim punto1 As Image = e.Item.FindControl("punto1")
                Dim pick As Label = e.Item.FindControl("pick")

                punto1.Visible = True
                pick.Visible = True
            End If

            If Not funzioni_comuni.gruppo_vendibile_drop_off(dropStazioneDropOff.SelectedValue, id_gruppo.Text, "", "", "", "", "", "", False) Then
                Dim punto2 As Image = e.Item.FindControl("punto2")
                Dim drop As Label = e.Item.FindControl("drop")

                punto2.Visible = True
                drop.Visible = True
            End If

            If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
                If Not funzioni_comuni.gruppo_vendibile_val(dropStazioneDropOff.SelectedValue, id_gruppo.Text, "", "", "", "", "", "", False) Then
                    Dim punto3 As Image = e.Item.FindControl("punto3")
                    Dim val As Label = e.Item.FindControl("val")

                    punto3.Visible = True
                    val.Visible = True
                End If
            End If

            Dim data_pick As String = txtDaData.Text
            data_pick = Year(data_pick) & "-" & Month(data_pick) & "-" & Day(data_pick)


            If funzioni_comuni.gruppoInStopSell(dropStazionePickUp.SelectedValue, id_gruppo.Text, data_pick, "", "", "", "", "", "", False) Then
                Dim punto4 As Image = e.Item.FindControl("punto4")
                Dim stop_sale As Label = e.Item.FindControl("stop_sale")

                punto4.Visible = True
                stop_sale.Visible = True
            End If

            'SE E' STATA SELEZIONATA UNA FONTE CONTROLLO SE LA FONTE E' IN STOP SELL PER IL SINGOLO GRUPPO
            If dropTipoCliente.SelectedValue <> "0" Then
                If funzioni_comuni.gruppoInStopSellPerFonte(dropStazionePickUp.SelectedValue, dropTipoCliente.SelectedValue, id_gruppo.Text, data_pick, "", "", "", "", "", "", False) Then
                    Dim punto5 As Image = e.Item.FindControl("punto5")
                    Dim stop_sale_fonte As Label = e.Item.FindControl("stop_sale_fonte")

                    punto5.Visible = True
                    stop_sale_fonte.Visible = True
                End If
            End If
        Else
            'IL CONTROLLO SULL'ETA' DEI GUIDATORI E' BLOCCANTE: DISABILITO I GRUPPI NON VENDIBILI
            Dim sel_gruppo As CheckBox = e.Item.FindControl("sel_gruppo")
            sel_gruppo.Enabled = False

            Dim punto6 As Image = e.Item.FindControl("punto6")
            Dim eta_guidatore As Label = e.Item.FindControl("eta_guidatore")

            punto6.Visible = True
            eta_guidatore.Visible = True
        End If
    End Sub

    Protected Sub vedi_tariffe(ByVal imposta_prepagato As Boolean)
        'ELIMINO LE PRECEDENTI RIGHE DI CALCOLO (PER IL NUM. CALCOLO ATTUALE)---------------------------------------------------------------
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM prenotazioni_costi WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM prenotazioni_warning WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
        '-----------------------------------------------------------------------------------------------------------------------------------
        'CALCOLO DEL PREZZO PER OGNI GRUPPO SELEZIONATO

        'CONTROLLO CHE E' STATO SELEZIONATO ALMENO UN GRUPPO
        Dim almeno_un_gruppo_selezionato As Boolean = False
        Dim gruppi_selezionati As Integer = 0
        Dim id_gruppo_selezioanto As String

        For i = 0 To listGruppi.Items.Count - 1
            Dim sel_gruppo As CheckBox = listGruppi.Items(i).FindControl("sel_gruppo")
            If sel_gruppo.Checked Then
                Dim id_gruppo As Label = listGruppi.Items(i).FindControl("id_gruppo")
                almeno_un_gruppo_selezionato = True
                gruppi_selezionati = gruppi_selezionati + 1
                id_gruppo_selezioanto = id_gruppo.Text
            End If
        Next

        If almeno_un_gruppo_selezionato Then
            If (dropTariffeGeneriche.SelectedValue <> "0" And dropTariffeParticolari.SelectedValue = "0") Or (dropTariffeGeneriche.SelectedValue = "0" And dropTariffeParticolari.SelectedValue <> "0") Then
                Dim id_tariffe_righe As String = ""

                If dropTariffeGeneriche.SelectedValue <> "0" Then
                    id_tariffe_righe = dropTariffeGeneriche.SelectedValue
                ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                    id_tariffe_righe = dropTariffeParticolari.SelectedValue
                End If

                'CONTROLLO CHE LA TARIFFA RISPETTI EVENTUALI VINCOLI DI MINIMO/MASSIMO GIORNI DI NOLEGGIO
                Dim numero_giorni As Integer = 0

                Dim min_giorni_nolo As String = funzioni_comuni.getMinGiorniNolo(id_tariffe_righe)
                Dim max_giorni_nolo As String = funzioni_comuni.getMaxGiorniNolo(id_tariffe_righe)

                If min_giorni_nolo <> "-1" Or max_giorni_nolo <> "-1" Then
                    numero_giorni = funzioni_comuni.getGiorniDiNoleggio(txtDaData.Text, txtAData.Text, ore1.Text, minuti1.Text, ore2.Text, minuti2.Text, id_tariffe_righe)
                    If min_giorni_nolo <> "-1" Then
                        lblMinGiorniNolo.Text = "La tariffa prevede un minimo di " & min_giorni_nolo & " giorni/o di noleggio"
                    Else
                        lblMinGiorniNolo.Text = ""
                    End If
                End If

                If (min_giorni_nolo = "-1" And max_giorni_nolo = "-1") Or (min_giorni_nolo = "-1" And CInt(numero_giorni) <= CInt(max_giorni_nolo) And max_giorni_nolo <> "-1") Or (CInt(min_giorni_nolo) <= CInt(numero_giorni) And max_giorni_nolo = "-1" And min_giorni_nolo <> "-1") Or (CInt(min_giorni_nolo) <= CInt(numero_giorni) And CInt(max_giorni_nolo) >= CInt(numero_giorni) And min_giorni_nolo <> "-1" And max_giorni_nolo <> "-1") Then
                    'BLOCCO LA POSSIBILITA' DI MODIFICARE TARIFFA E SCONTO - PER FARLO SARA' NECESSARIO AGIRE SULL'APPOSITO PULSANTE--------------------
                    dropTariffeGeneriche.Enabled = False
                    dropTariffeParticolari.Enabled = False
                    If tariffa_broker.Text = "1" Then
                        txtSconto.ReadOnly = True
                        dropTipoSconto.Enabled = False
                        txtCodiceSconto.Enabled = False
                        'btnApplicaCodiceSconto.Visible = False
                    End If
                    btnCambiaTariffa.Visible = True
                    '-----------------------------------------------------------------------------------------------------------------------------------

                    table_accessori_extra.Visible = True

                    'SELEZIONO GLI ELEMENTI EXTRA (NON OBBLIGATORI E CONTRASSEGNATI COME "NON VALORIZZARE" IN condizioni_elementi)
                    sqlElementiExtra.SelectCommand = funzioni.getQueryElementiExtra(id_tariffe_righe, False)

                    dropElementiExtra.Items.Clear()
                    dropElementiExtra.Items.Add("Seleziona...")
                    dropElementiExtra.Items(0).Value = "0"
                    dropElementiExtra.DataBind()

                    scegli_attivo.Text = "1"

                    tariffa_broker.Text = funzioni_comuni.is_tariffa_broker(id_tariffe_righe)

                    If tariffa_broker.Text = "0" Then
                        a_carico_del_broker.Text = ""
                        a_carico_del_broker_ultimo_calcolo.Text = ""
                    End If

                    calcolaTariffe(id_tariffe_righe, imposta_prepagato, lbl_data_creazione.Text)   'aggiornato salvo 19.02.2023

                    listPrenotazioniCosti.DataBind()

                    btnVediUltimoCalcolo.Visible = True

                    'E' NECESSARIO AGGIUNGERE GLI ACCESSORI PRECEDENTI ------------------------------------------------------------------------------
                    aggiungi_accessori_precedente_calcolo(imposta_prepagato)
                    '--------------------------------------------------------------------------------------------------------------------------------

                    'SE LA PRENOTAZIONE E' PREPAGATA E' NECESSARIO RIPORTARE I COSTI PREPAGATI DAL CALCOLO PRECEDENTE
                    'If lblPrepagato.Visible Then
                    '    funzioni_comuni.riporta_costi_prepagati(idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_selezioanto, "", "", False)
                    'End If

                    listPrenotazioniCosti.DataBind()
                    listPrenotazioniCosti.Focus()

                    'SE E' STATO SELEZIONATO UN SOLO GRUPPO ALLORA SALTO LA FASE DI SCELTA - SOLO PER TARIFFE BROKER
                    If gruppi_selezionati = 1 And tariffa_broker.Text = "1" Then
                        dropGruppoDaConsegnare.SelectedValue = id_gruppo_selezioanto

                        scegli_gruppo(id_gruppo_selezioanto)
                    End If
                Else
                    Dim msg As String = "Attenzione: i giorni di noleggio sono " & numero_giorni & "; la tariffa scelta prevede"
                    If min_giorni_nolo <> "-1" Then
                        msg = msg & " minimo " & min_giorni_nolo & " giorni/o di nolo - "
                    End If
                    If max_giorni_nolo <> "-1" Then
                        msg = msg & " massimo " & max_giorni_nolo & " giorni/o di nolo "
                    End If

                    Libreria.genUserMsgBox(Page, msg)
                End If
            Else
                Libreria.genUserMsgBox(Me, "Selezionare una tariffa generica oppure una tariffa particolare.")
            End If
        Else
            Libreria.genUserMsgBox(Me, "Selezionare almeno un gruppo.")
        End If
    End Sub

    Protected Sub btnProsegui_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProsegui.Click
        'NEL CASO DI BROKER! (ATTUALEMNTE SI ARRIVA A QUESTO PUNTO SOLO PER TARIFFE BROKER, TENERNE CONTO SE SI USERA' LO STESSO MECCANISMO
        'ANCHE PER LE TARIFFE PARTICOLARI)
        'SE E' STATA CAMBIATA LA TARIFFA RISPETTO A QUELLA SALVATA, E' NECESSARIO CHE L'UTENTE SCELGA A CARICO DEL BROKER!!!

        If dropTariffeParticolari.SelectedValue <> lbl_tariffa_broker_salvata.Text And dropVariazioneACaricoDi.SelectedValue = 0 Then
            Libreria.genUserMsgBox(Me, "ATTENZIONE: cambiando la tariffa rispetto a quella prenotata è necessario salvare inizialmente i giorni del voucher a carico del broker e successivamente effettuare l'estensione a carico del cliente. Cliccare su annulla per tornare al passo precedente, dove deve essere selezionato 'BROKER' nel campo 'VARIAZIONE A CARICO DI'")
        Else
            vedi_tariffe(False)
        End If
    End Sub

    Protected Sub nuovo_accessorio(ByVal id_accessorio As String, ByVal id_gruppo As String, ByVal tipo As String, ByVal eta_primo As String, ByVal eta_secondo As String, ByVal imposta_prepagato As Boolean, Optional ByVal accessorio_non_prepagato As Boolean = False)
        Dim id_tariffe_righe As String = ""

        If dropTariffeGeneriche.SelectedValue <> "0" Then
            id_tariffe_righe = dropTariffeGeneriche.SelectedValue
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            id_tariffe_righe = dropTariffeParticolari.SelectedValue
        End If


        Dim sconto As Double
        If dropTipoSconto.SelectedValue = "0" Then
            sconto = CDbl(txtSconto.Text)
        ElseIf dropTipoSconto.SelectedValue = "1" Then
            sconto = 0
        End If

        If tipo = "YOUNG" Then
            Dim prepagato As Boolean
            Dim giorni_prepagati As Integer
            If txtGiorniPrepagati.Visible And Not accessorio_non_prepagato Then


                prepagato = True
                If Not imposta_prepagato Then
                    giorni_prepagati = txtGiorniPrepagati.Text
                Else
                    giorni_prepagati = 0
                End If
            Else
                prepagato = False
                giorni_prepagati = 0
            End If

            'MARCO YOUNG 2
            If imposta_prepagato Then
                funzioni.calcola_costo_joung_driver_secondo_guidatore_primo_calcolo_prepagato(id_accessorio, dropStazionePickUp.SelectedValue, dropStazioneDropOff.SelectedValue, txtEtaPrimo.Text, txtEtaSecondo.Text, id_gruppo, txtNumeroGiorni.Text, giorni_prepagati, prepagato, sconto, id_tariffe_righe, "", "", idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                funzioni.prepagato_memorizza_costi_prepagati_x_fattura(idPrenotazione.Text, "", "", id_gruppo, numCalcolo.Text, id_accessorio)
            Else
                funzioni.calcola_costo_joung_driver_secondo_guidatore(id_accessorio, dropStazionePickUp.SelectedValue, dropStazioneDropOff.SelectedValue, txtEtaPrimo.Text, txtEtaSecondo.Text, id_gruppo, txtNumeroGiorni.Text, giorni_prepagati, prepagato, sconto, id_tariffe_righe, "", "", idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
            End If
        ElseIf tipo = "YOUNG PRIMO" Then
            Dim prepagato As Boolean
            Dim giorni_prepagati As Integer
            If txtGiorniPrepagati.Visible And Not accessorio_non_prepagato Then
                prepagato = True
                If Not imposta_prepagato Then
                    giorni_prepagati = txtGiorniPrepagati.Text
                Else
                    giorni_prepagati = 0
                End If
            Else
                prepagato = False
                giorni_prepagati = 0
            End If
            funzioni.calcola_costo_joung_driver_primo_guidatore(id_accessorio, dropStazionePickUp.SelectedValue, dropStazioneDropOff.SelectedValue, eta_primo, eta_secondo, id_gruppo, txtNumeroGiorni.Text, giorni_prepagati, prepagato, sconto, id_tariffe_righe, "", "", idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), False, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
        ElseIf tipo = "VAL_GPS" Then
            Dim prepagato As Boolean
            Dim giorni_prepagati As Integer
            If txtGiorniPrepagati.Visible And Not accessorio_non_prepagato Then
                prepagato = True
                If Not imposta_prepagato Then
                    giorni_prepagati = txtGiorniPrepagati.Text
                Else
                    giorni_prepagati = 0
                End If
            Else
                prepagato = False
                giorni_prepagati = 0
            End If

            funzioni.aggiungi_val_gps(dropStazionePickUp.SelectedValue, dropStazioneDropOff.SelectedValue, id_gruppo, txtNumeroGiorni.Text, giorni_prepagati, prepagato, Nothing, "", "", sconto, id_tariffe_righe, "", "", idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
        ElseIf tipo = "ELEMENTO" Then
            Dim prepagato As Boolean
            Dim giorni_prepagati As Integer


            If txtGiorniPrepagati.Visible And Not accessorio_non_prepagato Then
                prepagato = True
                If Not imposta_prepagato Then
                    giorni_prepagati = txtGiorniPrepagati.Text
                Else
                    giorni_prepagati = 0
                End If
            Else
                prepagato = False
                giorni_prepagati = 0
            End If



            funzioni.calcola_costo_elemento_extra(id_accessorio, dropStazionePickUp.SelectedValue, dropStazioneDropOff.SelectedValue, txtEtaPrimo.Text, txtEtaSecondo.Text, id_gruppo, txtNumeroGiorni.Text, giorni_prepagati, prepagato, "", "", sconto, id_tariffe_righe, "", "", idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
        End If
    End Sub

    Protected Sub calcolaTariffe(ByVal id_tariffe_righe As String, ByVal imposta_prepagato As Boolean, Optional data_creazione As String = "")



        'righe seguenti x verifica sconto spostate al termine della funzione salvo 19.02.2023
        ''SE E' STATO SELEZIONATO UN SCONTO CONTROLLO SE SUPERA LO SCONTO MASSIMO APPLICABILE. SE SI SOSTITUISCO IL VALORE COL VALORE MASSIMO
        'If txtSconto.Text <> "" Then
        '    Dim max_sconto As Double = funzioni_comuni.checkMaxSconto(id_tariffe_righe, txtSconto.Text, "", "", "", "", "", "", False)

        '    If max_sconto <> -1 Then
        '        txtSconto.Text = max_sconto
        '        lblScontoAttuale.Text = max_sconto
        '        lblMxSconto.Visible = True
        '    Else
        '        lblMxSconto.Visible = False
        '    End If

        'Else
        '    txtSconto.Text = "0"
        '    lblScontoAttuale.Text = "0"
        '    lblMxSconto.Visible = False
        'End If


        '#Salvo aggiunto x verifica sconto 19.02.2023
        '#Aggiunto salvo 19.02.2023
        'visualizza il massimo sconto applicabile sulla lbl msg 20.01.2023 salvo
        'se lo sconto è pari o superiore a quello inserito
        Dim max_sconto_applicabile() = Split(HttpContext.Current.Session("perc_sconto_tariffa_tutte"), ",")
        Dim iMaxSconto As Double = 0
        For xm = 0 To UBound(max_sconto_applicabile)
            Dim i As Double = 0
            If max_sconto_applicabile(xm) <> "" Then
                i = CDbl(max_sconto_applicabile(xm))
                If i >= iMaxSconto Then
                    iMaxSconto = i
                End If
            End If

        Next
        'se lo sconto inserito è maggiore di quello MAx stabilito per quella tariffa
        'visualizza l'etichetta


        If CDbl(txtSconto.Text) > iMaxSconto Then
            If CDbl(iMaxSconto) = "0" Then
                lblMxSconto.Text = "Nessuno sconto applicabile "
                lblMxSconto.Visible = True
                lblMxSconto.Font.Bold = True
                lblMxSconto.ForeColor = Drawing.Color.Red
                txtSconto.Text = "0"
            Else
                lblMxSconto.Text = "Massimo sconto applicabile " & iMaxSconto.ToString & "%"
                lblMxSconto.Visible = True
                lblMxSconto.Font.Bold = True
                lblMxSconto.ForeColor = Drawing.Color.Red
                txtSconto.Text = iMaxSconto.ToString
            End If

        Else
            If txtSconto.Text = "0" Then
                If CDbl(iMaxSconto) = "0" Then
                    lblMxSconto.Visible = False
                End If
            Else
                lblMxSconto.Text = "Sconto applicato " & txtSconto.Text & "%"
                lblMxSconto.Visible = True
                lblMxSconto.Font.Bold = True
                lblMxSconto.ForeColor = Drawing.Color.Green
            End If
        End If
        '@end aggiunto salvo



        '@end salvo aggiunto





        Dim tariffaVendibile As String

        'CONOSCENDO L'ID DELLA TABELLA tariffe righe (E' IL SELECTED VALUE DELLE DUE DROP DOWN LIST CHE VISUALIZZANO LE TARIFFE) CONOSCO
        '+ id_tempo_km
        '+ id_condizione
        '+ id_tariffa_madre
        '+ minuti_di_ritardo (minuti di ritardo consentiti oltre i quali scatta la giornata extra)

        'CALCOLO IL NUMERO DI GIORNI DI NOLEGGIO:----------------------------------------------------------------------------------------
        Dim numero_giorni As Integer = funzioni_comuni.getGiorniDiNoleggio(txtDaData.Text, txtAData.Text, ore1.Text, minuti1.Text, ore2.Text, minuti2.Text, id_tariffe_righe)
        txtNumeroGiorni.Text = numero_giorni
        'SE LA TARIFFA E' BROKER AGGIORNO IL NUMERO DI GIORNI A CARICO DEL BROKER (SE LA VARIAZIONE E' A SUO CARICO)
        If tariffa_broker.Text = "1" Then
            If dropVariazioneACaricoDi.SelectedValue = "1" Then
                'BROKER
                txtNumeroGiorniTO.Text = txtNumeroGiorni.Text
            ElseIf dropVariazioneACaricoDi.SelectedValue = "0" Then
                'CLIENTE
                txtNumeroGiorniTO.Text = lblGiorniToOld.Text
            End If

            'SE LA TARIFFA E' BROER DEVO UTILIZZARE LA RACK SE LA VARIAIZONE E' A CARICO DEL CLIENTE
            If dropVariazioneACaricoDi.SelectedValue = "1" Then
                tariffaVendibile = True
            Else
                tariffaVendibile = False
            End If
        ElseIf txtGiorniPrepagati.Visible Then
            'PER LE PREPAGATE SI APPLICA SEMPRE LA RACK PER LE ESTENSIONI
            tariffaVendibile = False
        Else
            tariffaVendibile = True
        End If

        If imposta_prepagato Then
            txtGiorniPrepagati.Visible = True
            txtGiorniPrepagati.Text = numero_giorni
        End If

        Dim giorni_prepagati As Integer = 0
        Dim prepagata As Boolean = False

        If txtGiorniPrepagati.Visible Then
            giorni_prepagati = txtGiorniPrepagati.Text
            prepagata = True
        End If
        '--------------------------------------------------------------------------------------------------------------------------------

        '#Blocco x Nuovo calcolo tariffe x periodi salvo 10.12.2022
        Dim tipoCli As String = dropTipoCliente.SelectedValue
        Dim descTariffa As String

        'verifica se tariffe generiche o tariffe particolari 
        'e ricava valore dal dropdown
        Dim TipoTariffa As String
        If dropTariffeGeneriche.SelectedValue <> "0" Then
            id_tariffe_righe = dropTariffeGeneriche.SelectedValue
            descTariffa = dropTariffeGeneriche.SelectedItem.ToString
            TipoTariffa = "G"
        ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            id_tariffe_righe = dropTariffeParticolari.SelectedValue
            descTariffa = dropTariffeParticolari.SelectedItem.ToString
            TipoTariffa = "P"
        End If

        'recupera data di creazione della prenotazione salvo 10.01.2023
        If data_creazione = "" Then
            data_creazione = lbl_data_creazione.Text
            If data_creazione = "" Then
                data_creazione = lblDataPrenotazione.Text
            End If
            data_creazione = getDataDb_con_orario(data_creazione)
        End If

        'da verificare 11.01.2023 salvo
        'se la dataout è cambiata imposta la data di creazione con la nuova data
        'in modo da attivare il calcolo tariffa con il nuovo metodo anche se 
        'la data di creazione effettiva è antecedente alla data di spostamento
        Dim data_out_originale As String = lbl_data_out_originale.Text
        Dim data_out_nuova As String = txtDaData.Text & " " & txtoraPartenza.Text & ":00"
        Dim diffData As Integer = DateDiff("d", CDate(data_out_originale), CDate(data_out_nuova))
        If diffData > 0 Then
            data_creazione = data_out_nuova
        End If

        '# Salvo aggiunto 25.02.2023
        'dati per calcolo tempoKm sono uguali recupera valore tariffa per 
        'registrarlo successivamente
        Dim SetTariffaOri As Boolean = False
        Dim ValoreTariffaOri As String = "0"
        If (dropStazionePickUp.SelectedValue = lbl_StazionePickUp_OLD.Text) And (CDate(txtDaData.Text) = CDate(lbl_DaData_Old.Text)) And (txtoraPartenza.Text = lbl_DaOre_Old.Text) And (dropStazioneDropOff.SelectedValue = lbl_stazioneDropOff_OLD.Text) And (CDate(txtAData.Text) = CDate(lbl_AData_old.Text)) And (txtOraRientro.Text = lbl_AOre_old.Text) And (txtNumeroGiorni.Text = lbl_numeroGiorni_old.Text) Then
            'dati non sono cambiati recupera valore tariffa e totale del calcolo corrente
            ValoreTariffaOri = funzioni_comuni_new.GetUltimoValoreTariffaValidoPren(idPrenotazione.Text, (CInt(numCalcolo.Text) - 1))
            lbl_valore_tariffa_ori.Text = ValoreTariffaOri
            SetTariffaOri = True
        End If
        '@end salvo





        '@ end Blocco x Nuovo calcolo tariffe x periodi salvo 10.12.2022


        If tariffa_broker.Text = "1" Then
            'PER OGNI GRUPPO CALCOLO I COSTI------------------------------------------------------------------------
            For i = 0 To listGruppi.Items.Count - 1
                Dim sel_gruppo As CheckBox = listGruppi.Items(i).FindControl("sel_gruppo")
                Dim id_gruppo As Label = listGruppi.Items(i).FindControl("id_gruppo")
                If sel_gruppo.Checked Then
                    'CALCOLO CON TARIFFA ORIGINARIA
                    If imposta_prepagato OrElse ((Not txtGiorniPrepagati.Visible) AndAlso ((tariffaVendibile) OrElse (tariffa_broker.Text = "1" And CInt(txtNumeroGiorniTO.Text) > CInt(txtNumeroGiorni.Text)))) Then
                        Dim sconto_web_prepagato As Double = 0
                        If imposta_prepagato Then
                            ricalcolaPrepagato.Text = "1"
                            giorni_prepagati = 0
                            If CDbl(txtSconto.Text) > 0 Then
                                'PER LE PREPAGATE LO SCONTO SI APPLICA SEMPRE AL SOLO TEMPO KM
                                sconto_web_prepagato = CDbl(txtSconto.Text)
                                txtSconto.Text = 0
                            End If
                        End If
                        'Response.Write(giorni_prepagati)
                        funzioni.calcolaTariffa_x_gruppo(dropStazionePickUp.SelectedValue, txtDaData.Text, CInt(ore1.Text), CInt(minuti1.Text), dropStazioneDropOff.SelectedValue, id_tariffe_righe, id_gruppo.Text, "",
                                                         numero_giorni, giorni_prepagati, prepagata, 0, CDbl(txtSconto.Text), dropTipoSconto.SelectedValue, sconto_web_prepagato, 0, txtEtaPrimo.Text, txtEtaSecondo.Text, "", "",
                                                         idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), id_ditta.Text, dropFonteCommissionabile.SelectedValue, txtPercentualeCommissionabile.Text,
                                                         dropTipoCommissione.SelectedValue, False, lblGGcommissioniOriginali.Text, "", TipoTariffa, descTariffa, txtAData.Text, tipoCli, data_creazione,
                                                         txtSconto.Text,,, ValoreTariffaOri, SetTariffaOri)
                    Else
                        Dim giorni_Extra_rack As Integer
                        If txtGiorniPrepagati.Visible Then
                            giorni_Extra_rack = (numero_giorni - CInt(txtGiorniPrepagati.Text))
                        Else
                            giorni_Extra_rack = numero_giorni - CInt(txtNumeroGiorniTO.Text)
                        End If
                        If giorni_Extra_rack < 0 Then
                            giorni_Extra_rack = 0
                        End If
                        'Response.Write(giorni_prepagati)
                        funzioni.calcolaTariffa_x_gruppo(dropStazionePickUp.SelectedValue, txtDaData.Text, CInt(ore1.Text), CInt(minuti1.Text), dropStazioneDropOff.SelectedValue, id_tariffe_righe, id_gruppo.Text, "",
                                                         numero_giorni, giorni_prepagati, prepagata, giorni_Extra_rack, CDbl(txtSconto.Text), dropTipoSconto.SelectedValue, 0, 0, txtEtaPrimo.Text, txtEtaSecondo.Text, "", "",
                                                         idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), id_ditta.Text, dropFonteCommissionabile.SelectedValue, txtPercentualeCommissionabile.Text,
                                                         dropTipoCommissione.SelectedValue, False, lblGGcommissioniOriginali.Text, "", TipoTariffa, descTariffa, txtAData.Text, tipoCli, data_creazione,
                                                         txtSconto.Text,,, ValoreTariffaOri, SetTariffaOri)
                    End If
                End If
            Next
        Else
            'NEL CASO DI NON PREPAGATO IL GRUPPO VIENE SELEZIONATO DAL MENU A TENDINA APPOSITO

            If imposta_prepagato OrElse ((Not txtGiorniPrepagati.Visible) AndAlso ((tariffaVendibile) OrElse (tariffa_broker.Text = "1" And CInt(txtNumeroGiorniTO.Text) > CInt(txtNumeroGiorni.Text)))) Then
                Dim sconto_web_prepagato As Double = 0
                If imposta_prepagato Then
                    ricalcolaPrepagato.Text = "1"
                    giorni_prepagati = 0
                    If CDbl(txtSconto.Text) > 0 Then
                        'PER LE PREPAGATE LO SCONTO SI APPLICA SEMPRE AL SOLO TEMPO KM
                        sconto_web_prepagato = CDbl(txtSconto.Text)
                        txtSconto.Text = 0
                    End If
                End If

                funzioni.calcolaTariffa_x_gruppo(dropStazionePickUp.SelectedValue, txtDaData.Text, CInt(ore1.Text), CInt(minuti1.Text), dropStazioneDropOff.SelectedValue, id_tariffe_righe, gruppoDaCalcolare.SelectedValue, "",
                                                 numero_giorni, giorni_prepagati, prepagata, 0, CDbl(0), dropTipoSconto.SelectedValue, sconto_web_prepagato, 0, txtEtaPrimo.Text, txtEtaSecondo.Text, "", "",
                                                 idPrenotazione.Text, "", numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), id_ditta.Text, dropFonteCommissionabile.SelectedValue, txtPercentualeCommissionabile.Text,
                                                 dropTipoCommissione.SelectedValue, False, lblGGcommissioniOriginali.Text, "", TipoTariffa, descTariffa, txtAData.Text, tipoCli, data_creazione,
                                                 txtSconto.Text,,, ValoreTariffaOri, SetTariffaOri)
            Else
                Dim giorni_Extra_rack As Integer
                If txtGiorniPrepagati.Visible Then
                    giorni_Extra_rack = (numero_giorni - CInt(txtGiorniPrepagati.Text))
                Else
                    giorni_Extra_rack = numero_giorni - CInt(txtNumeroGiorniTO.Text)
                End If
                If giorni_Extra_rack < 0 Then
                    giorni_Extra_rack = 0
                End If
                funzioni.calcolaTariffa_x_gruppo(dropStazionePickUp.SelectedValue, txtDaData.Text, CInt(ore1.Text), CInt(minuti1.Text), dropStazioneDropOff.SelectedValue, id_tariffe_righe, gruppoDaCalcolare.SelectedValue, "",
                                                 numero_giorni, giorni_prepagati, prepagata, giorni_Extra_rack, CDbl(0), dropTipoSconto.SelectedValue, 0, 0, txtEtaPrimo.Text, txtEtaSecondo.Text, "", "", idPrenotazione.Text, "",
                                                 numCalcolo.Text, Request.Cookies("SicilyRentCar")("idUtente"), id_ditta.Text, dropFonteCommissionabile.SelectedValue, txtPercentualeCommissionabile.Text, dropTipoCommissione.SelectedValue, False,
                                                 lblGGcommissioniOriginali.Text, "", TipoTariffa, descTariffa, txtAData.Text, tipoCli, data_creazione,
                                                 txtSconto.Text,,, ValoreTariffaOri, SetTariffaOri)
            End If
        End If

        ultimo_gruppo.Text = ""




    End Sub

    Protected Sub listPrenotazioniCosti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listPrenotazioniCosti.ItemDataBound



        Dim gruppo As Label = e.Item.FindControl("gruppo")
        Dim id_a_carico_di As Label = e.Item.FindControl("id_a_carico_di")
        Dim nome_costo As Label = e.Item.FindControl("nome_costo")
        Dim obbligatorio As Label = e.Item.FindControl("obbligatorio")
        Dim id_metodo_stampa As Label = e.Item.FindControl("id_metodo_stampa")
        Dim aggiorna As Button = e.Item.FindControl("aggiorna")
        Dim sconto As Label = e.Item.FindControl("lblSconto")
        Dim scontoAttuale As Label = e.Item.FindControl("lblScontoAttuale")         'salvo aggiunto 17.02.2023
        Dim valore_costo As Label = e.Item.FindControl("valore_costoLabel")
        Dim costo_scontato As Label = e.Item.FindControl("costo_scontato")
        Dim omaggiabile As Label = e.Item.FindControl("omaggiabile")
        Dim prepagato As Label = e.Item.FindControl("prepagato")
        Dim Oldomaggiato As CheckBox = e.Item.FindControl("chkOldOmaggio")
        Dim omaggiato As CheckBox = e.Item.FindControl("chkOmaggio")
        Dim scegli As Button = e.Item.FindControl("scegli")
        Dim chkScegli As CheckBox = e.Item.FindControl("chkScegli")
        Dim id_elemento As Label = e.Item.FindControl("id_elemento")


        '09.12.2021

        If chkScegli.Checked = True Then
            If id_elemento.Text = "100" Then
                ib_RD = True
            End If
            If id_elemento.Text = "170" Then
                ib_RF = True
            End If
            If id_elemento.Text = "223" Then
                ib_EliRes = True
                chkScegli.Enabled = False
            End If
            If id_elemento.Text = "248" Then
                ib_PPLus = True
            End If
        End If
        'end 09.12.2021






        'COLONNA COMMISSIONI
        If riga_commissioni.Visible Then
            e.Item.FindControl("labelCommissioni").Visible = True

            Dim lblCommissioni As Label = e.Item.FindControl("lblCommissioni")
            If lblCommissioni.Text <> "0,00" Then
                lblCommissioni.Visible = True
            End If
            If nome_costo.Text = Costanti.testo_elemento_totale Then
                lblCommissioni.Font.Bold = True
                lblCommissioni.Font.Size = 12
            End If
        End If

        'ATTIVO PER GLI ACCESSORI LA POSSIBILITA' DI SELEZIONARLO - NON POSSIBILE PER I PREPAGATI (NON SI POSSONO DESELEZIONARE) PER CUI LA CHECKBOX VIENE DISABILITATA
        If obbligatorio.Text = "False" And id_a_carico_di.Text <> Costanti.id_accessorio_incluso Then
            chkScegli.Visible = True
            'NEL CASO IN CUI L'ELEMENTO NON E' SELEZIONATO NON FACCIO VEDERE LA COLONNA SCONTO
        End If

        If prepagato.Text = "True" Or (txtGiorniPrepagati.Visible And nome_costo.Text = Costanti.testo_elemento_totale) Then
            chkScegli.Enabled = False
            e.Item.FindControl("costo_prepagato").Visible = True
            e.Item.FindControl("labelPrepagato").Visible = True

            If nome_costo.Text = Costanti.testo_elemento_totale Then
                Dim costo_prepagato As Label = e.Item.FindControl("costo_prepagato")
                costo_prepagato.Font.Bold = True
                costo_prepagato.Font.Size = 12
            End If
        End If

        'PER ALCUNI PROBLEMI CHE SI VERIFICANO CON LE FRANCHIGIE CHE DEVONO ESSERE RISOLTE SE AGGIUNTE, TUTTI GLI ACCESSORI DA SCEGLIERE DEVONO ESSERE SELEZIONATI PRIMA
        'CHE SIA CLICCATO SU "RICALCOLA PREPAGATO"
        If ricalcolaPrepagato.Text = "1" Then
            chkScegli.Enabled = False
        End If

        If scegli_attivo.Text = "1" Then
            scegli.Visible = True
        End If

        If sconto.Text = "0,00" Then
            sconto.Text = ""
            valore_costo.Visible = False
        Else
            sconto.Text = sconto.Text & " €"
            sconto.Visible = True
        End If

        If Not chkScegli.Checked And chkScegli.Visible Then
            'GLI ACCESSORI NON SELEZIONATI NON MOSTRANO IL PREZZO
            valore_costo.Visible = False
            sconto.Visible = False
            costo_scontato.Visible = False
        End If

        If Oldomaggiato.Checked Then
            valore_costo.Text = "0,00 €"
            costo_scontato.Text = "0,00 €"
            sconto.Text = ""
        Else
            valore_costo.Text = valore_costo.Text & " €"
            costo_scontato.Text = costo_scontato.Text & " €"
        End If

        If LCase(nome_costo.Text) = "valore tariffa" Or LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Or LCase(nome_costo.Text) = "sconto" Then
            nome_costo.Font.Bold = True
        End If




        If id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore Then
            If id_a_carico_di.Text = Costanti.id_accessorio_incluso And LCase(nome_costo.Text) <> "valore tariffa" Then
                'PER GLI ELEMENTI INCLUSI TRANNE IL TEMPO KM NON MOSTRO IL PREZZO (CHE E' SEMPRE ZERO)
                e.Item.FindControl("lblIncluso").Visible = True
                valore_costo.Visible = False
                sconto.Visible = False
                costo_scontato.Visible = False
            ElseIf id_a_carico_di.Text <> Costanti.id_accessorio_incluso And obbligatorio.Text = "True" And LCase(nome_costo.Text) <> "valore tariffa" And LCase(nome_costo.Text) <> "sconto" Then
                e.Item.FindControl("lblObbligatorio").Visible = True
            End If
        Else
            e.Item.FindControl("lblInformativa").Visible = True
        End If

        If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
            If tariffa_broker.Text = "1" Then
                If tipo_prenotazione.Text = "modifica" And dropVariazioneACaricoDi.SelectedValue = "1" Then
                    'NEL CASO IN CUI SIAMO NELLO STATO DI MODIFICA PRENOTAZIONE (PER CUI E' POSSIBILE SCEGLIERE DI CALCOLARE PER DIVERSI GRUPPI)
                    'SE LA MODIFICA E' A CARICO DEL BROKER RIMUOVO DIRETTAMENTE IL COSTO DEL VALORE TARIFFA (A CARICO DEL BROKER)
                    Dim id_gruppoLabel As Label = e.Item.FindControl("id_gruppoLabel")
                    costo_scontato.Text = CDbl(costo_scontato.Text) - getCostoACaricoDelBroker(numCalcolo.Text, id_gruppoLabel.Text)
                Else
                    'SE LA VARIAZIONE E' A CARICO DEL CLIENTE MA IL COSTO E' DIMINUITO TRATTO IL CASO COME FOSSE A CARICO DEL BROKER
                    Dim id_gruppoLabel As Label = e.Item.FindControl("id_gruppoLabel")
                    Dim costo_broker As Double = getCostoACaricoDelBroker(numCalcolo.Text, id_gruppoLabel.Text)
                    If CDbl(a_carico_del_broker.Text) < costo_broker Then
                        costo_scontato.Text = CDbl(costo_scontato.Text) - CDbl(a_carico_del_broker.Text)
                    Else
                        costo_scontato.Text = CDbl(costo_scontato.Text) - costo_broker
                    End If
                End If
            End If

            costo_scontato.Text = FormatNumber(costo_scontato.Text, 2, , , TriState.False)
            valore_costo.Visible = False
            costo_scontato.Font.Bold = True
            costo_scontato.ForeColor = Drawing.Color.Blue
            costo_scontato.Font.Size = 12
            e.Item.FindControl("lblObbligatorio").Visible = False

            If statoPrenotazione.Text = "0" And prenotazioneScaduta.Text <> "1" Then
                'AGGIORNAMENTO DEGLI ACCESSORI VISIBILE SOLO SE LA PRENOTAZIONE E' ATTIVA
                aggiorna.Visible = True
            End If
        End If

        'Response.Write(nome_costo.Text & "<br>")

        If gruppo.Text = ultimo_gruppo.Text Then
            e.Item.FindControl("riga_gruppo").Visible = False
            e.Item.FindControl("riga_intestazione").Visible = False
        Else
            e.Item.FindControl("riga_gruppo").Visible = True
            e.Item.FindControl("riga_intestazione").Visible = True
            ultimo_gruppo.Text = gruppo.Text
        End If

        If (omaggiabile.Text = "True" Or complimentary.Text = "1") And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore And Not e.Item.FindControl("lblIncluso").Visible And LCase(nome_costo.Text) <> LCase(Costanti.testo_elemento_totale) Then
            'ABILITO L'OMAGGIABILITA' DI UN ELEMENTO A SCELTA SOLAMENTE SE L'UTENTE HA IL RELATIVO PERMESSO - SOLO PER OBBLIGATORI ED ACCESSORI
            'NEL CASO DI COMPLIMENTARY OBBLIGATORI ED A SCELTA SONO CUMUNQUE TUTTI OMAGGIABILI
            If (livello_accesso_omaggi.Text = "3" Or complimentary.Text = "1") And Not ricalcolaPrepagato.Text = "1" Then
                omaggiato.Visible = True
            Else
                omaggiato.Visible = True
                omaggiato.Enabled = False
            End If
        End If

        'TARIFFE(BROKER) : SE L'UTENTE NON HA IL PERMESSO VENGONO NASCOSTI TUTTI I PREZZI E VENGONO VISUALIZZATI SOLO GLI ACCESSORI (SENZA PREZZO)
        If tariffa_broker.Text = "1" Then
            'If livello_accesso_broker.Text <> "3" Then
            If id_elemento.Text = Costanti.ID_tempo_km Then
                valore_costo.Visible = False
                sconto.Visible = False
                'IL COSTO SCONTATO NON E' VISIBILE SE COINCIDE COL COSTO A CARICO DEL BROKER, ALTRIMENTI VISUALIZZO SOLO LA PARTE A 
                'CARICO DEL CLIENTE - SE, IN FASE DI NUOVO CALCOLO, IL COSTO E' A CARICO DEL BROKER COMUNQUE NASCONDO IL COSTO
                If CDbl(costo_scontato.Text) = CDbl(a_carico_del_broker.Text) Or dropVariazioneACaricoDi.SelectedValue = "1" Then
                    costo_scontato.Visible = False
                Else
                    'SE IL COSTO E' DIMINUITO TRATTO IL CASO COME SE FOSSE A CARICO DEL BROKER
                    If CDbl(costo_scontato.Text) >= CDbl(a_carico_del_broker.Text) Then
                        costo_scontato.Text = FormatNumber(CDbl(costo_scontato.Text) - CDbl(CDbl(a_carico_del_broker.Text)), 2, , , TriState.False)
                    Else
                        costo_scontato.Visible = False
                    End If
                End If
            End If

            If tariffa_broker.Text = "1" Then
                'COSTI BROKER VISUALIZZATI SOLO CON PERMESSO
                If (livello_accesso_broker.Text = "3" Or livello_accesso_broker.Text = "2") Then
                    'L'UNICO ACCESSORIO A CARICO DEL BROKER E' IL VALORE TARIFFA
                    If id_elemento.Text = Costanti.ID_tempo_km Then
                        Dim a_carico_to As Label = e.Item.FindControl("a_carico_to")
                        e.Item.FindControl("labelTO").Visible = True
                        a_carico_to.Visible = True
                        If dropVariazioneACaricoDi.SelectedValue = "1" Then
                            a_carico_to.Text = FormatNumber(Replace(costo_scontato.Text, "€", ""), 2, , , TriState.False)
                        Else
                            a_carico_to.Text = FormatNumber(Replace(a_carico_del_broker.Text, "€", ""), 2, , , TriState.False)
                        End If
                    End If
                    If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
                        Dim a_carico_to As Label = e.Item.FindControl("a_carico_to")
                        a_carico_to.Visible = True
                        If dropVariazioneACaricoDi.SelectedValue = "1" Then
                            'NEL CASO DI IMPORTO TOTALMENTE A CARICO DEL BROKER L'INTERNO VALORE TARIFFA E' IL TOTALE DA PAGARE
                            a_carico_to.Text = FormatNumber(Replace(get_tempo_km(), "€", ""), 2, , , TriState.False)
                        Else
                            a_carico_to.Text = FormatNumber(Replace(a_carico_del_broker.Text, "€", ""), 2, , , TriState.False)
                        End If
                        a_carico_to.Font.Bold = True
                        a_carico_to.Font.Size = 12
                    End If
                End If
            End If

            'End If
            'If (obbligatorio.Text = "False" And id_a_carico_di.Text <> Costanti.id_accessorio_incluso) Or (LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale)) Then
            '    'PER GLI ELEMENTI A SCELTA (O PER LA RIGA TOTALE DOVE E' PRESENTE IL PULSANTE AGGIUNGI ACCESSORIO) NASCONDO I COSTI
            '    valore_costo.Visible = False
            '    sconto.Visible = False
            '    costo_scontato.Visible = False

            '    If (LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale)) Then
            '        nome_costo.Visible = False
            '    End If
            'Else
            '    e.Item.FindControl("riga_elementi").Visible = False
            'End If
        End If


        '09.12.2021
        'verifica ck

        'disabilita eliRes se RD + TF true ma solo se Prepagata 28.12.2021
        If (ib_RD = True Or ib_RF = True) And id_elemento.Text = "223" Then
            If funzioni_comuni.GetOpzionePrepagata("prenotazioni_costi", idPrenotazione.Text, "213") = False Or funzioni_comuni.GetOpzionePrepagata("prenotazioni_costi", idPrenotazione.Text, "214") = False Then
                'se rd o rf prepagate
                chkScegli.Enabled = True
            Else
                'se prepagata enabled false
                chkScegli.Enabled = False

            End If
            'end 09.12.2021
        End If

        'se elires attivata 22.12.2021
        If id_elemento.Text = "223" And ib_EliRes = True Then
            chkScegli.Enabled = True

        End If

        'se elires attiva PPLus disabilitata 28.12.2021

        If (funzioni_comuni.VerificaOpzione(listPrenotazioniCosti, "223", "ck") = True) _
          And id_elemento.Text = "248" Then
            chkScegli.Enabled = False
        End If

        If (funzioni_comuni.VerificaOpzione(listPrenotazioniCosti, "248", "ck") = True) _
          And id_elemento.Text = "223" Then
            chkScegli.Enabled = False
        End If


        If (funzioni_comuni.VerificaOpzione(listPrenotazioniCosti, "223", "ck") = False) And (funzioni_comuni.VerificaOpzione(listPrenotazioniCosti, "248", "ck") = False) _
            And funzioni_comuni.VerificaOpzione(listPrenotazioniCosti, "100", "ck") = True _
            And id_elemento.Text = "100" Then
            'Da Abilitare ??? da verificare 30.12.2021
            funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, "203", "")
            funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, "180", "")
        End If


        If (funzioni_comuni.VerificaOpzione(listPrenotazioniCosti, "223", "ck") = False) And (funzioni_comuni.VerificaOpzione(listPrenotazioniCosti, "248", "ck") = False) _
            And funzioni_comuni.VerificaOpzione(listPrenotazioniCosti, "170", "ck") = True _
            And id_elemento.Text = "170" Then
            'Da Abilitare ??? da verificare 30.12.2021
            funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, "204", "")
            funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, "181", "")
        End If



        'se prepagati RD o RF EliRes 223 NON Abilitato 
        If (funzioni_comuni.VerificaOpzione(listPrenotazioniCosti, "100", "ck") = True Or funzioni_comuni.VerificaOpzione(listPrenotazioniCosti, "170", "ck") = True) _
            And funzioni_comuni.VerificaOpzione(listPrenotazioniCosti, "100", "pre") = True _
            And id_elemento.Text = "223" Then
            chkScegli.Enabled = True
        End If
        'se NON prepagati RD o RF EliRes 223 Abilitato 
        If (funzioni_comuni.VerificaOpzione(listPrenotazioniCosti, "100", "ck") = True Or funzioni_comuni.VerificaOpzione(listPrenotazioniCosti, "170", "ck") = True) _
            And funzioni_comuni.VerificaOpzione(listPrenotazioniCosti, "100", "pre") = False _
            And id_elemento.Text = "223" Then
            chkScegli.Enabled = False
        End If

        If id_elemento.Text = "100" Then
            RD_ck = chkScegli.Checked
        End If
        If id_elemento.Text = "170" Then
            RF_ck = chkScegli.Checked
        End If
        If id_elemento.Text = "100" Then
            RD_prepagato = prepagato.Text
        End If
        If id_elemento.Text = "170" Then
            RF_prepagato = prepagato.Text
        End If

        '# Verifiche su ck se attivi e/o abilitati 

        '05.01.2022 se PPLUS Attiva
        'If flagItemBound248 <> True Then
        If VerificaOpzione(listPrenotazioniCosti, "248", "ck") = "True" Then
                SetOpzione(listPrenotazioniCosti, "223", False, False, False) 'EliRes disabilitata
                flagItemBound248 = True
            End If
        'End If


        '05.01.2022 se PPLUS disattiva e ELiRes Disattiva ambedue ck abilitati
        'If flagItemBound223 <> True Then
        If VerificaOpzione(listPrenotazioniCosti, "248", "ck") = False And VerificaOpzione(listPrenotazioniCosti, "223", "ck") = False Then
                'se RD e/o Prepagate ELIRES disabilitata
                If VerificaOpzione(listPrenotazioniCosti, "100", "pre") = True Then
                    SetOpzione(listPrenotazioniCosti, "223", False, False, False)
                Else
                    SetOpzione(listPrenotazioniCosti, "223", False, True, False)
                End If
                flagItemBound223 = True
            End If
        'End If


        '09.01.2021
        If VerificaOpzione(listPrenotazioniCosti, "223", "ck") = "True" Then
            'se prepagata deve essere disabilitata 09.01.2021
            If funzioni_comuni.VerificaOpzione(listPrenotazioniCosti, "223", "pre") = True Then
                funzioni_comuni.SetOpzione(listPrenotazioniCosti, "223", True, False, False)
            Else
                funzioni_comuni.SetOpzione(listPrenotazioniCosti, "223", True, True, False)
            End If
            SetOpzione(listPrenotazioniCosti, "248", False, False, False) 'PPLUS disabilitata
            flagItemBound223 = True
        End If


        '@# Verifiche su ck se attivi e/o abilitati


        'Tony 15/10/2022
        'TARIFFE BROKER: VENGONO NASCOSTI I PREZZI A CARICO DEL BROKER (VALORE_TARIFFA) E VENGONO VISUALIZZATI SOLO I COSTI DEGLI ACCESSORI (PERO' C'E' IL PERMESSO)
        If tariffa_broker.Text = "1" Then
            'Response.Write("OK2_2" & nome_costo.Text & "<br>")
            If (LCase(nome_costo.Text) = LCase("TOTALE")) Then
                costo_scontato.Text = ImportiCaricoCliente(idPrenotazione.Text)
                costo_scontato.Visible = True
            End If
        Else
            'Response.Write("KO2<br>")
        End If

        'Salvo 17.02.2023
        If LCase(nome_costo.Text) = "valore tariffa" Then
            If lblScontoAttuale.Text <> "0" Then
                sconto.Visible = True
                'recupera importo sconto  e lo scrive per recuperarlo nella list
                Dim val_imp_sconto As String = funzioni_comuni_new.GetImportoScontoPrenotazione(idPrenotazione.Text, numCalcolo.Text)

                If val_imp_sconto <> "0" Then
                    lbl_Importo_Sconto.Text = val_imp_sconto
                    sconto.Text = val_imp_sconto & " €"   'lbl_Importo_Sconto.Text
                Else

                End If



            Else
                sconto.Visible = False
            End If
        End If
        '@salvo




    End Sub



    Protected Function get_tempo_km() As String
        Dim id_elemento As Label
        For i = 0 To listPrenotazioniCosti.Items.Count - 1
            id_elemento = listPrenotazioniCosti.Items(i).FindControl("id_elemento")
            If id_elemento.Text = Costanti.ID_tempo_km Then
                Dim costo_scontato As Label = listPrenotazioniCosti.Items(i).FindControl("costo_scontato")
                get_tempo_km = costo_scontato.Text
                Exit For
            End If
        Next
    End Function

    Protected Function getCostoACaricoDelBroker(ByVal num_calcolo As String, ByVal id_gruppo As String) As Double
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT imponibile_scontato + iva_imponibile_scontato FROM prenotazioni_costi WITH(NOLOCK) WHERE id_elemento='" & Costanti.ID_tempo_km & "' AND id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "'", Dbc)

        getCostoACaricoDelBroker = Cmd.ExecuteScalar()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function


    Protected Sub AggiornaPrenotazione(idgruppo)

        'Procedura Aggiunta il 10.11.2021 
        'codice spostato da listPrenotazioniCosti_ItemCommand
        'assegnata variabile idgruppo = id_gruppoScelto.Text che era assegnata come label a id_gruppoScelto

        'AGGIORNA IL COSTO DELLA PRENOTAZIONE (PER SINGOLO GRUPPO) AGGIUNGENDO O RIMUOVENDO IN COSTO DI UNO O PIU' ACCESSORI SELEZIOANTI
        Dim id_gruppoScelto As String = idgruppo  '= listPrenotazioniCosti.Items(i).FindControl("id_gruppoLabel") modificato 10.11.2021
        Dim id_gruppo As Label
        Dim check_attuale As CheckBox
        Dim check_old As CheckBox
        Dim id_elemento As Label
        Dim omaggiato As CheckBox
        Dim old_omaggiato As CheckBox
        Dim is_gps As Label
        Dim num_elemento As Label
        Dim tipologia_franchigia As Label
        Dim sottotipologia_franchigia As Label

        Dim protezione_plus As Boolean = False                  'aggiunto il 09.11.2021
        Dim eliminazione_res As Boolean = False                 'aggiunto il 10.11.2021

        Dim franc_furto_rid As Boolean = False
        Dim franc_danni_rid As Boolean = False
        Dim id_elemento_danni As String = "-1"
        Dim id_elemento_furto As String = "-1"
        Dim xpausa As String = ""

        Dim tResponse As String = ""
        Dim tResponseOLD As String = ""

        Dim disabledPPLus As Boolean = False
        Dim line_error As Integer = 0

        Dim a() As String = Split(GetResponse(listPrenotazioniCosti), "#")
        tResponse = a(0)
        tResponseOLD = a(1)
        'Solo x Test
        'Response.Write("tresponse: " & tResponse & "<br/>")
        'Response.Write("tresponseOLD: " & tResponseOLD & "<br/>")

        'Exit Sub 'solo x ()

        '# verifica se PPLUS viene disattivato successivamente e una delle due Riduzioni viene disabilitate 
        'compare msg e disabilita PPLUS 20.12.2021

        'se PPLUS Attiva ma RF è stata disabilitata deve mettere PPLUS a ck=false
        If tResponse.IndexOf("248") > -1 And tResponse.IndexOf("170") = -1 And tResponseOLD.IndexOf("170") > -1 Then
            'se RD Attiva RF viene disabilitata disattiva PPLUS
            SetOpzione(listPrenotazioniCosti, "248", False, True, False)
            funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "204", "")
            line_error = -1
            funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "181", "")

            disabledPPLus = True
        End If

        If idPrenotazione.Text = "" Then
            Libreria.genUserMsgBox(Page, line_error.ToString)
        End If

        If tResponse.IndexOf("248") > -1 And tResponse.IndexOf("100") = -1 And tResponseOLD.IndexOf("100") > -1 Then
            'se RF Attiva RF viene disabilitata disattiva PPLUS
            SetOpzione(listPrenotazioniCosti, "248", False, True, False)
            funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "203", "")
            funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "180", "")
            disabledPPLus = True
            line_error = -2
        End If

        If idPrenotazione.Text = "" Then
            Libreria.genUserMsgBox(Page, line_error.ToString)
        End If

        'Exit Sub   'SOLO PER TEST 20.12.2021

        'una volta effettuato la verifica prosegue con i calcoli tariffe

        '## end verifica

        '################################################################################################
        '@ NOTA : le verifiche sui ck attivi devono essere effettuate prima della sezione seguente
        '################################################################################################

        'SCORRO LA LISTA CERCANDO, ALL'INTERNO DEL GRUPPO SELEZIONATO, GLI ELEMENTI SELEZIONABILI (OVVERO OVE LA CHECKBOX E' SELEZIONATA)
        'PER QUESTI ELEMENTI CONTROLLO SE E' STATO SELEZIONATO (E NON LO ERA PRECEDENTEMENTE) O SE E' STATO DESELEZIONATO (RISPETTO
        'ALLA VOLTA PRECEDENTE) AGGIORNANDO IL TOTALE

        For i = 0 To listPrenotazioniCosti.Items.Count - 1

            id_gruppo = listPrenotazioniCosti.Items(i).FindControl("id_gruppoLabel")
            If id_gruppo.Text = idgruppo Then 'ex id_gruppoScelto.Text da selezione
                check_attuale = listPrenotazioniCosti.Items(i).FindControl("chkScegli")
                check_old = listPrenotazioniCosti.Items(i).FindControl("chkOldScegli")
                id_elemento = listPrenotazioniCosti.Items(i).FindControl("id_elemento")
                omaggiato = listPrenotazioniCosti.Items(i).FindControl("chkOmaggio")
                is_gps = listPrenotazioniCosti.Items(i).FindControl("is_gps")
                num_elemento = listPrenotazioniCosti.Items(i).FindControl("num_elemento")
                tipologia_franchigia = listPrenotazioniCosti.Items(i).FindControl("tipologia_franchigia")
                sottotipologia_franchigia = listPrenotazioniCosti.Items(i).FindControl("sottotipologia_franchigia")

                If check_attuale.Checked = True Then
                    If id_elemento.Text <> "" Then
                        tResponse += id_elemento.Text & ","
                    End If
                End If


                If check_old.Checked = True Then
                        tResponseOLD += id_elemento.Text & ","     'creo stringa con i ck OLD
                    End If



                    If tipologia_franchigia.Text = "ASSICURAZIONE" Then
                        xpausa = id_elemento.Text
                    End If

                    If tipologia_franchigia.Text = "ASSICURAZIONE" And sottotipologia_franchigia.Text = "DANNI" And check_attuale.Checked = True Then
                        franc_danni_rid = True
                        id_elemento_danni = id_elemento.Text
                    End If
                    If tipologia_franchigia.Text = "ASSICURAZIONE" And sottotipologia_franchigia.Text = "FURTO" And check_attuale.Checked = True Then
                        franc_furto_rid = True
                        id_elemento_furto = id_elemento.Text
                    End If


                    If id_elemento.Text = "248" Then
                        id_elemento.Text = "248"
                        If check_attuale.Checked = True Then
                            protezione_plus = True
                        End If
                    End If

                    If id_elemento.Text = "223" Then
                        id_elemento.Text = "223"
                        If check_attuale.Checked = True Then
                            eliminazione_res = True
                        End If
                    End If


                If check_attuale.Checked = True Then
                    If id_elemento.Text = "100" Then 'RD
                        franc_danni_rid = True
                    End If
                    If id_elemento.Text = "170" Then 'RF
                        franc_furto_rid = True
                    End If
                End If

                'riempie stringhe tResponse
                If check_attuale.Checked = True Then
                    If id_elemento.Text <> "" Then
                        tResponse += id_elemento.Text & ","
                    End If
                End If
                If check_old.Checked = True Then
                    tResponseOLD += id_elemento.Text & ","     'creo stringa con i ck OLD
                End If

                If check_attuale.Visible Or (Not check_attuale.Visible And omaggiato.Visible) Then

                    old_omaggiato = listPrenotazioniCosti.Items(i).FindControl("chkOldOmaggio")
                    'SE E' E' UNA NUOVA SELEZIONE O SE E' STATO RICHIESTO L'OMAGGIO SENZA SELEZIONARE IL CHECK 'seleziona' (A MENO CHE L'ELEMENTO NON SIA STATO PRECEDENTEMENTE SELEZIONATO)
                    If (check_attuale.Checked And Not check_old.Checked) Or (omaggiato.Checked And Not old_omaggiato.Checked And Not (check_attuale.Checked And check_old.Checked)) Then
                        'AGGIUNGO IL COSTO DELL'ACCESSORIO AL TOTALE O LO OMAGGIO

                        check_attuale.Checked = True 'NEL CASO IN CUI SIA STATO SELEZIONATO SOLO 'OMAGGIO'

                        If omaggiato.Checked Then
                            funzioni.omaggio_accessorio(True, False, False, "", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text, sottotipologia_franchigia.Text)
                        Else
                            Dim imposta_prepagato As Boolean = False
                            If ricalcolaPrepagato.Text = "1" Then
                                imposta_prepagato = True
                            End If
                            aggiungi_costo_accessorio("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, id_elemento.Text, "", "", "", imposta_prepagato, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                        End If
                        'SE E' STATO AGGIUNTO IL SECONDO GUIDATORE DEVO AGGIUNGERE, SE NECESSARIO, IL COSTO DELLO YOUNG DRIVER
                        If id_elemento.Text = Costanti.Id_Secondo_Guidatore Then
                            Dim accessorio_non_prepagato As Boolean = False

                            Dim imposta_prepagato As Boolean = False
                            If ricalcolaPrepagato.Text = "1" Then
                                imposta_prepagato = True
                            Else
                                accessorio_non_prepagato = True
                            End If
                            nuovo_accessorio(get_id_young_driver(), idgruppo, "YOUNG", txtEtaPrimo.Text, txtEtaSecondo.Text, imposta_prepagato, accessorio_non_prepagato)
                        End If

                        'SE E' STATO AGGIUNTO IL GPS AGGIUNGO EVENTUALMENTE IL VAL
                        If is_gps.Text = "True" Then
                            If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
                                Dim accessorio_non_prepagato As Boolean = False

                                Dim imposta_prepagato As Boolean = False
                                If ricalcolaPrepagato.Text = "1" Then
                                    imposta_prepagato = True
                                Else
                                    accessorio_non_prepagato = True
                                End If
                                nuovo_accessorio("", idgruppo, "VAL_GPS", txtEtaPrimo.Text, txtEtaSecondo.Text, imposta_prepagato, accessorio_non_prepagato)
                            End If
                        End If
                    ElseIf (check_attuale.Checked And check_old.Checked) And (omaggiato.Checked And Not old_omaggiato.Checked) Then
                        'DOPO AVER AGGIUNTO IL COSTO DELL'ACCESSORIO AL TOTALE SI DECIDE DI OMAGGIARLO
                        funzioni.omaggio_accessorio(True, False, False, "", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text, sottotipologia_franchigia.Text)
                    ElseIf (check_attuale.Checked And check_old.Checked) And (Not omaggiato.Checked And old_omaggiato.Checked) Then
                        'DOPO AVER AGGIUNTO IL COSTO DELL'ACCESSORIO E AVERLO OMAGGIATO TOLGO L'OMAGGIO
                        funzioni.omaggio_accessorio(False, True, False, "", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text, sottotipologia_franchigia.Text)
                    ElseIf Not check_attuale.Checked And check_old.Checked And Not omaggiato.Checked Then
                        'TOLGO IL COSTO DELL'ACCESSORIO DAL TOTALE A MENO CHE NON ERA OMAGGIATO
                        If old_omaggiato.Checked Then
                            funzioni.omaggio_accessorio(False, False, True, "", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia_franchigia.Text, sottotipologia_franchigia.Text)
                        Else
                            funzioni.rimuovi_costo_accessorio("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, id_elemento.Text, "", "SCELTA", dropTipoCommissione.SelectedValue)
                        End If

                        If id_elemento.Text = Costanti.Id_Secondo_Guidatore Then
                            Dim id_young_driver As String = get_id_young_driver()
                            'RIMUOVO IL COSTO DELLO JOUNG DRIVER NEL CASO IN CUI SI RIMUOVE "SECONDO GUIDATORE"
                            If funzioni.esiste_young_driver_secondo_guidatore(id_young_driver, idgruppo, numCalcolo.Text, "", idPrenotazione.Text, "", "") Then
                                funzioni.rimuovi_costo_accessorio("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, id_young_driver, "2", "EXTRA", dropTipoCommissione.SelectedValue)
                            End If
                        End If

                        If is_gps.Text = "True" And dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
                            Dim id_val_gps As String = funzioni_comuni.getIdValGps()
                            If id_val_gps <> "" Then
                                funzioni.rimuovi_costo_accessorio("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, id_val_gps, "", "EXTRA", dropTipoCommissione.SelectedValue)
                            End If
                        End If
                    End If
                End If
                '# CODICE DISABILITATO NEL PRIMO CICLO e ABILITATO NEL SECONDO 17.12.2021


            End If
        Next
        '# primo ciclo di verifica cosa è selezionato





        '# INIZIO VERIFICHE PPLUS CK
        'se una delle due riduzioni è disabilitata 
        'disabilita PPLUS 17.12.2021
        If franc_danni_rid = False Or franc_furto_rid = False And protezione_plus = True Then
            If id_elemento.Text = "248" Then
                If check_attuale.Checked = True Then
                    check_attuale.Checked = False  'disabilita PPLUS
                End If
            End If
        End If

        'se selezionato Protezione Plus nasconde le franchigie 09.11.2021
        If protezione_plus = True Then
            funzioni_comuni.aggiorna_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "TOTALE")
        Else
            'se Eliminazione responsabilità danni, furto e inc. è disabilitata
            If eliminazione_res = False Then

                'id_elemento=180 Franchigia Danni 
                'id_elemento=181 Franchigia Furto e inc 

                'id_elemento=203 Franchigia Danni ridotta
                'id_elemento=204 Franchigia Furto e inc ridotta
                '
                line_error = 1
                If franc_danni_rid = True Then
                    funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, id_elemento_danni, "")
                Else
                    funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, id_elemento_danni, "")
                End If
                line_error = 2
                If franc_furto_rid = True Then
                    funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, id_elemento_furto, "")
                Else
                    funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, id_elemento_furto, "")
                End If
                line_error = 3

                'If (franc_danni_rid = True And franc_furto_rid = True) Then
                '    'imposta le due franchigie ridotte 10.11.2021
                funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "203", "204")
                'Else
                'funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "203", "204")
                'End If


            End If
        End If

        If idPrenotazione.Text = "" Then
            Libreria.genUserMsgBox(Page, line_error.ToString)
        End If




        'inserita per test x fare azzerare le tariffe da pagare al banco
        'se viene tolta una riduzione e la PPLUS viene disabilitata 
        'allora ricalcola le tariffe ed esce dalla procedura
        If disabledPPLus = True Then
            Session("ricalcola_prenotazione_salva") = "1"
            ricalcola_non_broker(False)
            Session("ricalcola_prenotazione_salva") = ""
            Exit Sub
        End If

        '09.12.2021
        'nasconde/visualizza le franchigie
        If tResponse.IndexOf("100") > 1 Then
            line_error = 4
            funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, "203", "")
            funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, "180", "")
        Else
            line_error = 5
            funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, "180", "")
            funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, "203", "")
        End If
        If tResponse.IndexOf("170") > 1 Then
            line_error = 6
            funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, "204", "")
            funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, "181", "")
        Else
            line_error = 7
            funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, "181", "")
            funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, "204", "")
        End If


        If idPrenotazione.Text = "" Then
            Libreria.genUserMsgBox(Page, line_error.ToString)
        End If


        '# codice da preventivi aggiornamento tariffe nel caso di aggiunta PPLUS 09.12.2021
        '# se RD attivata e RF no la aggiunge e ricalcola le tariffe

        '23.11.2021 e 09.12.2021
        'se ck PPlus Disattivo o non visibile/ Rid Furto Attiva / rid Danni Attiva
        If tResponse.IndexOf("100") > -1 And (tResponse.IndexOf("248") = -1 Or protezione_plus = False) And tResponse.IndexOf("170") > -1 Then
            'deve anche assegnare la franchigia_attiva x 181 e toglie la 204 (ridotta)
            line_error = 8
            funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "204", "")      'visualizza intera
            funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "181", "")        'nasconde la ridotta

            'attiva la Fra rid danni x 100 
            line_error = 9
            funzioni_comuni.visualizza_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "203", "")      'visualizza la ridotta
            funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "180", "")        'nasconde franc Intera

        End If

        If idPrenotazione.Text = "" Then
            Libreria.genUserMsgBox(Page, line_error.ToString)
        End If



        '23.11.2021  e 09.12.2021
        'se CK Protezione Plus Attivata 

        If tResponse.IndexOf("248") > -1 Then
            'imposta il valore selezionato sui ck Riduzioni
            funzioni_comuni.Aggiorna_Ck("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "170", "1")
            funzioni_comuni.Aggiorna_Ck("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "100", "1")

            'aggiorna Totale includendo il costo delle riduzioni quando presente PPLUS
            'se non selezionate
            If tResponse.IndexOf("100") = -1 Then   'costo del riduzione danni se nn presente
                line_error = 10
                visualizza_franchigie_Aggiorna_Costo("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "100")
            End If
            If tResponse.IndexOf("170") = -1 Then   'costo del riduzione furto se nn presente
                line_error = 11
                visualizza_franchigie_Aggiorna_Costo("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "170")
            End If

            'aggiorna e seleziona CK PPLUS
            funzioni_comuni.Aggiorna_Ck("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "248", "1")
        End If

        If idPrenotazione.Text = "" Then
            Libreria.genUserMsgBox(Page, line_error.ToString)
        End If




        'era abilitata RF perchè attivata PPLUS 17.12.2021
        If tResponse.IndexOf("248") > -1 And tResponseOLD.IndexOf("170") > -1 And tResponse.IndexOf("170") = -1 Then
            'deve disabilitare PPLUS
            funzioni_comuni.Aggiorna_Ck("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "170", "0")
            'aggiorna e disabilita PPLUS
            funzioni_comuni.Aggiorna_Ck("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "248", "0")

            'costo del Riduzione Furto se nn presente
            If tResponse.IndexOf("170") = -1 Then
                line_error = 12
                visualizza_franchigie_Aggiorna_Costo("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "170")
            End If

        End If


        If idPrenotazione.Text = "" Then
            Libreria.genUserMsgBox(Page, line_error.ToString)
        End If


        '23.11.2021  e 09.12.2021
        'se CK Protezione Plus o Eliminazione Respons. Attivata 
        If tResponse.IndexOf("248") > -1 Or tResponse.IndexOf("223") > -1 Then
            'nasconde le franchigie
            funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "204", "")
            funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "181", "")
            funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "203", "")
            funzioni_comuni.nascondi_franchigie("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "180", "")
        End If
        '#@ fine verifiche PPLUS 


        'verifiche RD RF ELI REs 30.12.2021

        'aggiorna la ListPreventivi Costi
        listPrenotazioniCosti.DataBind()

        '#verifiche finali 21.12.2021
        'verifica CK attivati e condizioni relative 24.11.2021 e 09.12.2021
        VerificaCk(tResponse, tResponseOLD, id_gruppo, listPrenotazioniCosti)


        'recupera valore originario del deposito cauzionale 26.01.22
        Dim impDepDefault As String = GetValoreDepositoCauzionaleDefault(idgruppo)
        Dim impDepCalcolato As String = impDepDefault

        'se PPLUS o Elires 26.01.22 modifica deposito cauzionale
        If tResponse.IndexOf("248") > -1 Or tResponse.IndexOf("223") > -1 Then
            'Riduce importo Deposito cauzionale 26.01.22
            impDepCalcolato = "200"
            funzioni_comuni.aggiorna_deposito_cauzionale("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
        End If



        'se Ambedue RIDUZIONI ATTIVE e PPLUS abilitate riduce valore deposito cauzionale 
        ' ma senza PPLUS Attiva
        If tResponse.IndexOf("248") = -1 And tResponse.IndexOf("100") > -1 And tResponse.IndexOf("170") > -1 _
            And tResponse.IndexOf("223") = -1 Then
            impDepCalcolato = "300"
            funzioni_comuni.aggiorna_deposito_cauzionale("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
        End If


        'se opzione 'Riduzione Deposito 50%' importo = deposito / 2
        If tResponse.IndexOf("234") > -1 Then
            impDepCalcolato = FormatNumber((CDbl(impDepCalcolato) / 2), 2)
            funzioni_comuni.aggiorna_deposito_cauzionale("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
        End If


        'Nessuna opzione attiva 26.01.22
        If tResponse.IndexOf("248") = -1 And tResponse.IndexOf("100") = -1 And tResponse.IndexOf("170") = -1 _
            And tResponse.IndexOf("223") = -1 And tResponse.IndexOf("234") = -1 Then
            impDepCalcolato = impDepDefault
            funzioni_comuni.aggiorna_deposito_cauzionale("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
        End If


        '## PRIMA del databind la modifca dei DATI sul DB 22.01.2022

        'aggiorna la lista con tutte le variazioni 17.12.2021
        listPrenotazioniCosti.DataBind()
        '#@ end verifiche finali

        '## DOPO il databind la modifca di visualizzazione dei CK 22.01.2022

        'se PPLUS Attivata disabilita ELIRES 05.01.2022 OK verificato
        If VerificaOpzione(listPrenotazioniCosti, "248", "ck") = True Then
            SetOpzione(listPrenotazioniCosti, "223", False, False, False)
        End If





    End Sub
    Public Sub VerificaCk(ByVal tResponse As String, ByVal tResponseOld As String, ByRef id_gruppoSceltoT As Label, lst As DataList)

        '09.12.2021 aggiunto alla pagina prenotazioni.vb.aspx
        '24.11.2021 aggiunta procedura x controllo condizioni dei ck attivati
        '23.11.2021

        'verifica tutte le condizioni per l'abilitazione o disabilitazione dei ck
        'solo x visualizzazione - se bisogna intervenire sui calcoli modificare nel blocco prima del listPreventiviCosti.DATABIND()

        'dim lst As DataList = listPrenotazioniCosti   'nel caso di valore non passato


        Dim id_gruppoScelto As Label = id_gruppoSceltoT  'e.Item.FindControl("id_gruppoLabel")   'modificato il 18.11.2021 passato il valore di sopra
        Dim id_gruppo As Label
        Dim check_attuale As CheckBox
        Dim check_old As CheckBox
        Dim id_elemento As Label
        Dim omaggiato As CheckBox
        Dim old_omaggiato As CheckBox
        Dim is_gps As Label
        Dim num_elemento As Label
        Dim tipologia_franchigia As Label
        Dim sottotipologia_franchigia As Label

        Dim old_sel As CheckBox
        Dim id_gruppoList As Label

        Dim protezione_plus As Boolean = False  'aggiunto 16.11.2021
        Dim Eliminazione_res As Boolean = False  'aggiunto 16.11.2021


        For i = 0 To lst.Items.Count - 1

            check_attuale = lst.Items(i).FindControl("chkScegli")
            check_old = lst.Items(i).FindControl("chkOldScegli")
            id_elemento = lst.Items(i).FindControl("id_elemento")
            omaggiato = lst.Items(i).FindControl("chkOmaggio")
            is_gps = lst.Items(i).FindControl("is_gps")
            num_elemento = lst.Items(i).FindControl("num_elemento")
            tipologia_franchigia = lst.Items(i).FindControl("tipologia_franchigia")
            sottotipologia_franchigia = lst.Items(i).FindControl("sottotipologia_franchigia")

            'se PPLUS Attiva disabilita ELiRes
            If tResponse.IndexOf("248") > -1 And id_elemento.Text = "223" Then
                check_attuale.Enabled = False
            End If
            'se EliRes Attiva disabilita PPLUS
            If tResponse.IndexOf("223") > -1 And id_elemento.Text = "248" Then
                check_attuale.Enabled = False
            End If
            'se PPLUS Attiva e RD nnAttiva abilita ck PPLUS
            If tResponse.IndexOf("248") > -1 And tResponse.IndexOf("100") = -1 And id_elemento.Text = "248" Then
                check_attuale.Enabled = True
                check_attuale.Checked = False
            End If
            'se PPLUS Attiva e RD Attiva abilita ck RD disable
            If tResponse.IndexOf("248") > -1 And tResponse.IndexOf("100") > -1 And tResponse.IndexOf("170") > -1 And id_elemento.Text = "100" Then
                check_attuale.Enabled = False
            End If
            'se PPLUS Attiva e RD Attiva abilita ck RF disable
            If tResponse.IndexOf("248") > -1 And tResponse.IndexOf("100") > -1 And tResponse.IndexOf("170") > -1 And id_elemento.Text = "170" Then
                check_attuale.Enabled = False
            End If
            'se PPLUS nnAttiva e RD Attiva e RF attiva abilita ck RD enable
            If tResponse.IndexOf("248") = -1 And tResponse.IndexOf("100") > -1 And tResponse.IndexOf("170") > -1 And id_elemento.Text = "100" Then
                check_attuale.Enabled = True
            End If
            'se PPLUS nnAttiva e RD Attiva e RF attiva abilita ck RF enable
            If tResponse.IndexOf("248") = -1 And tResponse.IndexOf("100") > -1 And tResponse.IndexOf("170") > -1 And id_elemento.Text = "170" Then
                check_attuale.Enabled = True
            End If
            'se PPLUS Attiva e RD nnAttiva e abilita ck RD CKTrue / Disable
            If tResponse.IndexOf("248") > -1 And tResponse.IndexOf("100") = -1 And id_elemento.Text = "100" Then
                check_attuale.Checked = True
                check_attuale.Enabled = False
            End If
            'se PPLUS Attiva e RF nnAttiva e abilita ck RF CKTrue / Disable
            If tResponse.IndexOf("248") > -1 And tResponse.IndexOf("170") = -1 And id_elemento.Text = "170" Then
                check_attuale.Checked = True
                check_attuale.Enabled = False
            End If

            'se EliRes Attiva e RD noCK -> CkFalse / Disabled
            If tResponse.IndexOf("223") > -1 And tResponse.IndexOf("100") = -1 And id_elemento.Text = "100" Then
                check_attuale.Checked = False
                check_attuale.Enabled = False
            End If

            'se EliRes Attiva e RF noCK -> CkFalse / Disabled
            If tResponse.IndexOf("223") > -1 And tResponse.IndexOf("170") = -1 And id_elemento.Text = "170" Then
                check_attuale.Checked = False
                check_attuale.Enabled = False
            End If


        Next  'ciclo x verifica delle condizioni di visualizzazione dei CK su ListPreventiviCosti 23.11.2021



    End Sub


    Protected Sub listPrenotazioniCosti_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles listPrenotazioniCosti.ItemCommand
        If e.CommandName = "Aggiorna" Then
            If prenotazione_modificabile("0") Then

                Dim idgrupposcelto As Label = e.Item.FindControl("id_gruppoLabel")
                AggiornaPrenotazione(idgrupposcelto.Text)  'spostato codice nella procedura AggiornaPrenotazione() 10.11.2021


            Else
                Libreria.genUserMsgBox(Me, "Attenzione: la prenotazione non è più modificabile in quanto è variato lo stato. E' necessario tornare alla maschera di ricerca e ricaricare la prenotazione.")
            End If

            listPrenotazioniCosti.Focus()

            'AGGIORNO LA TABELLA COMMISSIONI OPERATORE SE NON SIAMO NELLA FASE DI "RICALCOLO" O "RICALCOLO PREPAGATO" (LE MODIFICHE VERRANNO CONFERMATE SE SI SALVA LA PRENOTAZIONE)
            If Not btnSalvaModifiche.Visible And Not (btnPagamento.Visible And Not btnRicalcola.Visible) Then
                aggiorna_commissioni_operatore()
            End If

            dropElementiExtra.SelectedIndex = -1    'riporta a zero la lista extra 10.11.2021




        ElseIf e.CommandName = "scegli" Then
            Dim id_gruppoLabel As Label = e.Item.FindControl("id_gruppoLabel")

            dropGruppoDaConsegnare.SelectedValue = id_gruppoLabel.Text

            scegli_gruppo(id_gruppoLabel.Text)

        End If

        Dim aggiorna As Button = e.Item.FindControl("aggiorna")
        aggiorna.Focus()



    End Sub

    Protected Sub aggiorna_commissioni_operatore()

        Dim sqla As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            sqla = "INSERT INTO commissioni_operatore (num_prenotazione, num_contratto, id_operatore, id_condizioni_elementi, nome_costo) " &
                    "(SELECT '" & lblNumPrenotazione.Text & "','0','" & Request.Cookies("SicilyRentCar")("idUtente") & "', id_elemento, nome_costo FROM prenotazioni_costi WITH(NOLOCK) " &
                    "WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "' AND selezionato='1' AND (id_a_carico_di=2 OR id_elemento='" & Costanti.ID_tempo_km & "') " &
                    "AND NOT EXISTS (SELECT 1 FROM commissioni_operatore WITH(NOLOCK) WHERE commissioni_operatore.num_prenotazione='" & lblNumPrenotazione.Text & "' AND commissioni_operatore.id_condizioni_elementi=id_elemento))"


            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Cmd.ExecuteNonQuery()


            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception

            HttpContext.Current.Response.Write("error prenotazioni aggiorna_commissioni_operatore : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Protected Sub aggiorna_commissioni_web()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "INSERT INTO commissioni_operatore (num_prenotazione, num_contratto, id_operatore, id_condizioni_elementi, nome_costo) " &
                "(SELECT '" & lblNumPrenotazione.Text & "','0','0', id_elemento, nome_costo FROM prenotazioni_costi WITH(NOLOCK) " &
                "WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "' AND selezionato='1' AND (id_a_carico_di=2 OR id_elemento='" & Costanti.ID_tempo_km & "') " &
                "AND NOT EXISTS (SELECT 1 FROM commissioni_operatore WITH(NOLOCK) WHERE commissioni_operatore.num_prenotazione='" & lblNumPrenotazione.Text & "' AND commissioni_operatore.id_condizioni_elementi=id_elemento))"


        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Cmd.ExecuteNonQuery()

        sqlStr = "UPDATE prenotazioni SET commissioni_da_assegnare_web=0 WHERE Nr_Pren='" & idPrenotazione.Text & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub scegli_gruppo(ByVal id_gruppo As String)
        scegli_attivo.Text = "0"
        id_gruppo_auto_scelto.Text = id_gruppo

        'ELIMINO LE RIGHE DI CALCOLO RELATIVE AGLI ALTRI GRUPPI ------------------------------------------------------------------------
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM prenotazioni_costi WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "' AND id_gruppo<>'" & id_gruppo & "'", Dbc)
        Cmd.ExecuteNonQuery()

        listPrenotazioniCosti.DataBind()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
        '------------------------------------------------------------------------------------------------------------------------------

        'SE E' STATO SELEZIONATO UN GRUPPO PER CUI SONO STATI TROVATI DEI WARNING LI AGGIUNGO ALLA LISTA DI WARNING NELL'APPOSITA TABELLA
        aggiungi_warning_gruppo_auto(id_gruppo)

        btnSalvaModifiche.Visible = True
        btnModificaPrenotazione.Visible = True

        tab_conducenti.Visible = True
        gestione_note.Visible = True
        tab_annullamento.Visible = True
        table_gruppi.Visible = False
    End Sub

    Protected Sub btnAnnulla1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla1.Click
        'RIABILITO I PULSANTI PER LA RICERCA DELLA TARIFFA ---------------------------------------------------------------------------------
        btnCerca.Visible = True
        dropStazionePickUp.Enabled = True
        dropStazioneDropOff.Enabled = True
        txtoraPartenza.Enabled = True
        txtOraRientro.Enabled = True
        minuti1.Enabled = True
        ore1.Enabled = True
        ore2.Enabled = True
        minuti2.Enabled = True
        dropTipoCliente.Enabled = True
        txtDaData.Enabled = True
        txtAData.Enabled = True
        txtEtaPrimo.Enabled = True
        txtEtaSecondo.Enabled = True
        txtNumeroGiorni.Enabled = True
        btnAnnulla0.Visible = True
        '-----------------------------------------------------------------------------------------------------------------------------------

        'ELIMINO I DATI DI WARNING ---------------------------------------------------------------------------------------------------------
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM prenotazioni_warning WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        table_warning.Visible = False
        listWarningDropPrenotazioni.DataBind()
        listWarningPickPrenotazioni.DataBind()
        fonte_stop_sell.Visible = False

        Session("exit_prenotazione") = "1"


        '-----------------------------------------------------------------------------------------------------------------------------------
    End Sub

    Protected Sub btnAnnulla0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla0.Click
        tab_cerca_tariffe.Visible = False
        tab_ricerca.Visible = True
    End Sub

    Protected Sub btnAnnulla2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla2.Click
        'RIABILITO I PULSANTI PER LA RICERCA DELLA TARIFFA ---------------------------------------------------------------------------------
        btnCerca.Visible = True
        dropStazionePickUp.Enabled = True
        dropStazioneDropOff.Enabled = True
        txtoraPartenza.Enabled = True
        txtOraRientro.Enabled = True
        minuti1.Enabled = True
        ore1.Enabled = True
        ore2.Enabled = True
        minuti2.Enabled = True
        dropTipoCliente.Enabled = True
        txtDaData.Enabled = True
        txtAData.Enabled = True
        txtEtaPrimo.Enabled = True
        txtEtaSecondo.Enabled = True
        txtNumeroGiorni.Enabled = True
        riga_broker.Visible = True
        'btnAnnulla0.Visible = True

        dropTariffeGeneriche.Enabled = True
        dropTariffeParticolari.Enabled = True
        txtSconto.ReadOnly = False
        dropTipoSconto.Enabled = True
        txtCodiceSconto.Enabled = True
        'btnApplicaCodiceSconto.Visible = True

        btnCambiaTariffa.Visible = False
        table_accessori_extra.Visible = False

        btnAnnulla1.Visible = True
        table_warning.Visible = True
        table_gruppi.Visible = False
        table_tariffe.Visible = False
        '-----------------------------------------------------------------------------------------------------------------------------------

        'ELIMINO I DATI DI WARNING E DEI COSTI ---------------------------------------------------------------------------------------------

        'Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        'Dbc.Open()
        'Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM prenotazioni_warning WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        'Cmd.ExecuteNonQuery()

        'Cmd = New Data.SqlClient.SqlCommand("DELETE FROM prenotazioni_warning WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        'Cmd.ExecuteNonQuery()

        'Cmd.Dispose()
        'Cmd = Nothing
        'Dbc.Close()
        'Dbc.Dispose()
        'Dbc = Nothing

        table_warning.Visible = False
        listWarningDropPrenotazioni.DataBind()
        listWarningPickPrenotazioni.DataBind()
        listPrenotazioniCosti.DataBind()
        fonte_stop_sell.Visible = False
        '-----------------------------------------------------------------------------------------------------------------------------------
    End Sub

    Protected Sub ImpostaPannelloPagamento(ByVal num_pren As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 Nr_Contratto FROM pagamenti_extra WITH(NOLOCK) WHERE N_PREN_RIF='" & num_pren & "'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test = "" Then
            'NESSUNA TRANSAZIONE
            tab_dettagli_pagamento.Visible = False
        Else
            tab_dettagli_pagamento.Visible = True
            'SE ESISTE ALMENO UNA TRANSAZIONE MOSTRO IL TOTALE PREAUTORIZZATO E IL TOTALE INCASSATO
            Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(SUM(ISNULL(PER_IMPORTO,0)),0) FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE (N_PREN_RIF='" & num_pren & "') AND ((id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Richiesta & "' AND preaut_aperta='1' AND operazione_stornata='0') OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Integrazione & "' AND preaut_aperta='1' AND operazione_stornata='0') )", Dbc)
            txtPOS_TotPreaut.Text = FormatNumber(Cmd.ExecuteScalar, 2, , , TriState.False)

            Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(SUM(ISNULL(PER_IMPORTO,0)),0) FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE (N_PREN_RIF='" & num_pren & "') AND ((id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Vendita & "' AND operazione_stornata='0') OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Chiusura & "' AND operazione_stornata='0') OR (per_importo<0 AND operazione_stornata='0') OR (id_pos_funzioni_ares='" & enum_tipo_pagamento_ares.Pagamento_Contanti & "' AND operazione_stornata='0') )", Dbc)

            txtPOS_TotIncassato.Text = FormatNumber(Cmd.ExecuteScalar, 2, , , TriState.False)
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub richiama_prenotazione(ByVal num_pren As String)
        'RICHIAMO LA PRENOTAZIONE VISUALIZZANDO LE INFORMAZIONI SALVATE SENZA ESEGUIRE IL CALCOLO

        Dim iderror As Integer = 0

        Dim sqlstr As String = ""


        Try

            tipo_prenotazione.Text = "richiama"
            scegli_attivo.Text = "0"
            btnSalvaModifiche.Visible = False
            btnVediUltimoCalcolo.Visible = False
            old_ultimo_gruppo.Text = ""
            listVecchioCalcolo.Visible = False
            btnVediUltimoCalcolo.Visible = False

            listPagamenti.DataBind()

            Dim richiamo_a_buon_fine As Boolean = True
            Dim id_gruppo_auto As String

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            sqlstr = "SELECT ISNULL(prepagata,'False') As prepagata, ISNULL(pren_broker_no_tariffa,'False') As pren_broker_no_tariffa, * FROM prenotazioni WITH(NOLOCK) WHERE NUMPREN='" & num_pren & "' AND attiva='1'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            Rs.Read()

            If Rs.HasRows() Then

                'aggiunto 10.01.2023/11.01.2023 salvo
                If Not IsDBNull(Rs!data_creazione) Then
                    lbl_data_creazione.Text = Rs!data_creazione
                Else
                    lbl_data_creazione.Text = Rs!datapren
                End If
                If lbl_data_creazione.Text = "" Then
                    lbl_data_creazione.Text = Date.Now.ToString
                End If
                'registra data di uscita originale su prenotazione per 
                'eventuale verifica se in caso di cambio dataout effettua il calcolo con nuovo metodo
                If Not IsDBNull(Rs!PRDATA_OUT) Then
                    lbl_data_out_originale.Text = Day(Rs("PRDATA_OUT")) & "/" & Month(Rs("PRDATA_OUT")) & "/" & Year(Rs("PRDATA_OUT")) & " " & Rs("ore_uscita") & ":" & Rs("minuti_uscita") & ":00"

                End If

                '@ end aggiunto salvo 11.03.2023


                idPrenotazione.Text = Rs("NR_PREN")

                    lblNumPrenotazione.Text = Rs("NUMPREN")
                    statoPrenotazione.Text = Rs("status")

                sqlAllegati.SelectCommand = "SELECT id_allegato, nome_file, cartella, (cartella + nome_file) as my_path, descrizione, data_operazione,id_operatore FROM contratti_prenotazioni_allegati WITH(NOLOCK) " &
                                        "INNER JOIN contratti_prenotazioni_allegati_tipo WITH(NOLOCK) ON contratti_prenotazioni_allegati.id_cnt_pren_allegati_tipo=contratti_prenotazioni_allegati_tipo.id_cnt_pren_allegati_tipo WHERE num_pren='" & Rs("numpren") & "'"
                    dataListAllegati.DataBind()

                    'SE E' STATO EFFETTUATA ALMENO UN'OPERAZIONE POS MOSTRO IL TOTALE E LA SEZIONE DI PAGAMENTO -------------------
                    ImpostaPannelloPagamento(lblNumPrenotazione.Text)
                    '--------------------------------------------------------------------------------------------------------------


                    If Pagamenti.is_complimentary(lblNumPrenotazione.Text, "") Then
                        complimentary.Text = "1"
                        lblComplimentary.Visible = True
                    Else
                        complimentary.Text = ""
                    End If

                    If Pagamenti.is_full_credit(lblNumPrenotazione.Text, "") Then
                        full_credit.Text = "1"
                        lblFullCredit.Visible = True
                    Else
                        full_credit.Text = ""
                    End If

                    'modificato il 28.07.2021
                    lblDataPrenotazione.Text = Rs("datapren") & "" '& FormatDateTime(Rs!datapren, vbShortTime)


                    If Rs("num_calcolo") > 1 Then
                        lblNumeroVariazione.Text = "MODIFICA n. " & (Rs("num_calcolo") - 1)
                    Else
                        lblNumeroVariazione.Text = ""
                    End If

                    lblOperatoreCreazione.Text = getNomeOperatore(Rs("id_operatore_creazione") & "")

                    If (Rs("id_operatore_ultima_modifica") & "") <> "" Then
                        lblOperatoreModifica.Text = "Modifica: " & getNomeOperatore(Rs("id_operatore_ultima_modifica")) & " " & Rs("data_ultima_modifica") & ""
                    End If

                    If Rs("prepagata") Then
                        lblPrepagato.Visible = True
                        lblPrepagato.Text = "CLIENTE PREPAGATO - GG: " & Rs("giorni_prepagati")
                        lblGiorniPrepagati.Visible = True
                        txtGiorniPrepagati.Visible = True
                        txtGiorniPrepagati.Text = Rs("giorni_prepagati")
                    End If

                    elimina_righe_calcolo(num_pren)



                    If (Rs("id_motivo_annullamento") & "") <> "" Then
                        iderror = 1
                        dropMotivoAnnullamento.SelectedValue = Rs("id_motivo_annullamento") & ""
                        dropMotivoAnnullamento.Enabled = False
                        iderror = 2
                        If Rs("status") = "0" Then
                            'LA PRENOTAZINE E' ATTIVA MA C'E' UNA MOTIVAZIONE DI ANNUALMENTO: SIGNIFICA CHE C'E' UNA RICHIESTA DI ANNULLAMENTO
                            lblRichiestaAnnullamento.Visible = True
                            dropMotivoAnnullamento.Enabled = True
                            iderror = 3
                        End If
                    Else
                        dropMotivoAnnullamento.Enabled = True
                        iderror = 4
                    End If

                    If (Rs("status") = "2") Or (Rs("status") = "1") Then
                        'PRENOTAZIONE ANNULLATA
                        lblDataAnnullamento.Text = " - PRENOTAZIONE ANNULLATA - DATA: " & Rs("data_annullamento") & ""
                        lblOperatoreAnnullamento.Text = "- Operatore Annullamento : " & getNomeOperatore(Rs("id_operatore_annullamento") & "")

                        btnAnnullaPrenotazione.Visible = False
                        btnRichiestaAnnullamento.Visible = False

                        If livello_accesso_annulla_ripristina.Text = "3" Then
                            btnRipristinaPrenotazione.Visible = True
                        End If

                        lblPrenotazioneAnnullata.Visible = True
                    ElseIf Rs("status") = "0" Then
                        'PRENOTAZIONE ATTIVA
                        If livello_accesso_annulla_ripristina.Text = "3" Then
                            btnAnnullaPrenotazione.Visible = True
                            btnRichiestaAnnullamento.Visible = False
                        Else
                            btnAnnullaPrenotazione.Visible = False
                            btnRichiestaAnnullamento.Visible = True
                        End If

                        If (Rs("data_ripristino") & "") <> "" Then
                            lblDataAnnullamento.Text = " - PRENOTAZIONE RIPRISTINATA - DATA: " & Rs("data_ripristino") & ""
                            lblOperatoreRipristino.Text = "- Operatore Ripristino : " & getNomeOperatore(Rs("id_operatore_ripristino") & "")
                        End If
                    ElseIf Rs("status") = "3" Then
                        'PRENOTAZIONE CHE E' DIVENTATA UN CONTRATTO
                        btnAnnullaPrenotazione.Visible = False
                        btnRichiestaAnnullamento.Visible = False
                        numContratto.Text = Rs("num_contratto")
                        numContratto.Visible = True
                        lblContrattoNum.Visible = True
                    ElseIf Rs("status") = "4" Then
                        lbl_bloccata_ribaltamento.Visible = True
                        btnAnnullaPrenotazione.Visible = False
                        btnRichiestaAnnullamento.Visible = False
                    End If

                    numCalcolo.Text = CInt(Rs("num_calcolo"))
                    id_gruppo_auto = Rs("ID_GRUPPO") & ""
                    id_gruppo_auto_scelto.Text = Rs("ID_GRUPPO") & ""

                    Try
                        gruppoDaCalcolare.SelectedValue = Rs("ID_GRUPPO")
                    Catch ex As Exception

                    End Try


                    If is_gruppo_speciale(Rs("ID_GRUPPO")) Then
                        tab_assegnazione_targa.Visible = True
                        If (Rs("targa_gruppo_speciale") & "") <> "" Then
                            txtTarga.Text = Rs("targa_gruppo_speciale")
                            txtTarga.ReadOnly = True
                            btnAssegnaTarga.Text = "Rimuovi"
                            lblTargaSelezionata.Text = Rs("targa_gruppo_speciale")
                        End If
                    Else
                        tab_assegnazione_targa.Visible = False
                    End If

                    txtNumeroGiorni.Text = Rs("giorni") & ""


                    'SE LA PRENOTAZIONE PROVIENE DAL WEB E NON è STATA MAI APERTA ASSEGNO LE COMMISSIONI DEGLI ELEMENTI ATTUALI ALL'OPERATORE 0
                    If Rs("commissioni_da_assegnare_web") Then
                        aggiorna_commissioni_web()
                    End If

                    'FASE 1 - DATE/STAZIONI/FONTI ---------------------------------------------------------------------------------------------
                    Try
                        If (Rs("id_fonte_commissionabile") & "") <> "" Then
                            dropFonteCommissionabile.SelectedValue = Rs("id_fonte_commissionabile")
                            dropTipoCommissione.SelectedValue = Rs("tipo_commissione")
                            txtPercentualeCommissionabile.Text = Rs("commissione_percentuale")
                            lblGGcommissioniOriginali.Text = Rs("giorni_commissioni")
                            riga_commissioni.Visible = True
                            dropTipoCommissione.Enabled = False
                            dropFonteCommissionabile.Enabled = False
                        Else
                            txtPercentualeCommissionabile.Text = ""
                            dropTipoCommissione.SelectedValue = "0"
                            lblGGcommissioniOriginali.Text = ""
                        End If

                        Dim codice_provenienza As String = Rs("codice_provenienza") & ""
                        Select Case codice_provenienza
                            Case "55", "56", "58", "59"
                                Libreria.ListControlSelectvalueSicuro(dropStazionePickUp, Rs("PRID_stazione_out") & "", "N.V.(" & Rs("cod_rib_stazione_out") & ")")
                                Libreria.ListControlSelectvalueSicuro(dropStazioneDropOff, Rs("PRID_stazione_pr") & "", "N.V.(" & Rs("cod_rib_stazione_pr") & ")")
                            Case Else
                                dropStazionePickUp.SelectedValue = Rs("PRID_stazione_out")
                                dropStazioneDropOff.SelectedValue = Rs("PRID_stazione_pr")
                        End Select


                        dropTipoCliente.SelectedValue = Rs("id_fonte")
                    Catch ex As Exception
                        'SE LA/LE STAZIONI O/E LA FONTE NON E' PIU' DISPONIBILE QUANDO SI RICHIAMA LA PRENOTAZIONE NON SARA' POSSIBILE RICHIAMARE
                        'LA PRENOTAZIONE RICHIESTA
                        richiamo_a_buon_fine = False
                    End Try


                    If richiamo_a_buon_fine Then
                        txtDaData.Text = Day(Rs("PRDATA_OUT")) & "/" & Month(Rs("PRDATA_OUT")) & "/" & Year(Rs("PRDATA_OUT"))
                        txtOldDaData.Text = Day(Rs("PRDATA_OUT")) & "/" & Month(Rs("PRDATA_OUT")) & "/" & Year(Rs("PRDATA_OUT"))
                        txtAData.Text = Day(Rs("PRDATA_PR")) & "/" & Month(Rs("PRDATA_PR")) & "/" & Year(Rs("PRDATA_PR"))



                        txtoraPartenza.Text = Rs("ore_uscita") & ":" & Rs("minuti_uscita")
                        txtOraRientro.Text = Rs("ore_rientro") & ":" & Rs("minuti_rientro")

                        'CONTROLLO SE LA PRENOTAZIONE E' SCADUTA E SALVO L'INFORMAZIONE SU LABEL NASCOSTA
                        Dim uscita As DateTime = funzioni_comuni.getDataDb_con_orario2(txtDaData.Text & " 00:00:00")
                        Dim oggi As DateTime = funzioni_comuni.getDataDb_con_orario2(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()) & " 23:59:59")
                        If DateDiff(DateInterval.Day, oggi, uscita) < 0 Then
                            prenotazioneScaduta.Text = "1"
                            lblPrenotazioneScaduta.Visible = True
                        Else
                            prenotazioneScaduta.Text = ""
                        End If

                        ore1.Text = Rs("ore_uscita")
                        minuti1.Text = Rs("minuti_uscita")
                        ore2.Text = Rs("ore_rientro")
                        minuti2.Text = Rs("minuti_rientro")
                        txtEtaPrimo.Text = Trim(Rs("eta_primo_guidatore"))
                        txtEtaSecondo.Text = Trim(Rs("eta_secondo_guidatore"))

                        'SE ERA STATO SPECIFICATO UN CODICE EDP SELEZIONO DA ESSO LA DITTA COLLEGATA - ALTRIMENTI CONTROLLO SE E' STATA VALORIZZATA
                        'LA DITTA PER FATTURAZIONE (SENZA AVER SPECIFICATO IL CODICE EDP ALL'INIZIO DEL PROCESSO DI PRENOTAZIONE)

                        If (Rs("codice_edp") & "") <> "" Then
                            txtCodiceCliente.Text = Rs("codice_edp")
                            lblNomeDitta.Text = getNomeDittaFromEdp(Rs("codice_edp"))
                            'NEL CASO IN CUI E' STATO SPECIFICATO IL CODICE EDP LA DITTA E' PER FORZA QUELLA COLLEGATA AD ESSO

                            id_ditta.Text = Rs("id_cliente")
                            txtNomeDitta.Text = getNomeDittaFromEdp(Rs("codice_edp"))

                            btnModificaDitta.Visible = False
                        ElseIf (Rs("id_cliente") & "") <> "" Then
                            id_ditta.Text = Rs("id_cliente")
                            txtNomeDitta.Text = getNomeDittaFromId(Rs("id_cliente"))
                        Else
                            txtCodiceCliente.Text = ""
                            lblNomeDitta.Text = ""
                        End If

                        dropStazionePickUp.Enabled = False
                        dropStazioneDropOff.Enabled = False
                        txtDaData.Enabled = False
                        txtAData.Enabled = False
                        txtEtaPrimo.Enabled = False
                        txtEtaSecondo.Enabled = False

                        txtCodiceCliente.Enabled = False

                        If (Rs("data_nascita") & "") <> "" Then
                            txtDataDiNascita.Text = Day(Rs("data_nascita")) & "/" & Month(Rs("data_nascita")) & "/" & Year(Rs("data_nascita"))
                        End If

                        txtNumeroGiorni.Enabled = False
                        txtoraPartenza.Enabled = False
                        txtOraRientro.Enabled = False
                        ore1.Enabled = False
                        minuti1.Enabled = False
                        ore2.Enabled = False
                        minuti2.Enabled = False
                        dropTipoCliente.Enabled = False
                        btnCerca.Visible = False
                        btnAnnulla0.Visible = False

                        tab_ricerca.Visible = False
                        tab_cerca_tariffe.Visible = True
                        tab_prenotazioni.Visible = False
                        tab_prenotazioni.Visible = False
                        table_accessori_extra.Visible = False
                        table_gruppi.Visible = False

                        'FASE 2 - RICERCA DELLA RIGA DI TARIFFA CORRISPONDENTE ALLA TARIFFA SCELTA ----------------------------------------------------
                        table_tariffe.Visible = True

                        If ((Rs("id_tariffa") & "") <> "") And ((Rs("id_tariffe_righe") & "") <> "") Then
                            'setQueryTariffePossibili(Rs("id_tariffa"))
                            'RICHIAMANDO UNA VECCHIA PRENOTAZIONI IMPOSTO COME ATTIVA LA TARIFFA ALL'ATTO DELLA PRENOTAZIONE

                            prenotazione_no_tariffa.Text = "0"

                            If (Rs("tipo_tariffa") & "") = "generica" Then

                                dropTariffeGeneriche.Items.Add(Rs("codtar"))
                                dropTariffeGeneriche.Items(1).Value = Rs("id_tariffe_righe")

                                dropTariffeGeneriche.Items(0).Selected = False
                                dropTariffeGeneriche.Items(1).Selected = True
                                dropTariffeGeneriche.Enabled = False
                                dropTariffeParticolari.Enabled = False

                            ElseIf (Rs("tipo_tariffa") & "") = "fonte" Then

                                dropTariffeParticolari.Items.Add(Rs("codtar"))
                                dropTariffeParticolari.Items(1).Value = Rs("id_tariffe_righe")

                                dropTariffeParticolari.Items(0).Selected = False
                                dropTariffeParticolari.Items(1).Selected = True
                                dropTariffeParticolari.Enabled = False
                                dropTariffeGeneriche.Enabled = False

                            End If

                        Else
                            If Rs("pren_broker_no_tariffa") Then
                                prenotazione_no_tariffa.Text = "1"
                            Else
                                richiamo_a_buon_fine = False
                                setQueryTariffePossibili(0)
                            End If
                        End If

                        id_conducente.Text = Rs("id_conducente") & ""

                        If (Rs("id_conducente") & "") <> "" Then
                            image_primo_guidatore.Visible = True
                            vediPrimoGuidatore.HRef = "contratto_vedi_guidatore.aspx?idUtente=" & Rs("id_conducente")
                        Else
                            image_primo_guidatore.Visible = False
                        End If

                        'If Trim(id_conducente.Text) = "" Then
                        '    btnPagamento.Visible = False
                        'Else
                        '    btnPagamento.Visible = True
                        'End If

                        txtNomeConducente.Text = Rs("nome_conducente") & ""
                        txtCognomeConducente.Text = Rs("cognome_conducente") & ""
                        txtMailConducente.Text = Rs("mail_conducente") & ""
                        txtIndirizzoConducente.Text = Rs("indirizzo_conducente") & ""
                        txtRiferimentoTO.Text = Rs("rif_to") & ""
                        lblRifToOld.Text = Rs("rif_to") & ""
                        txtRifTel.Text = Rs("riferimento_telefono") & ""

                        'Trace.Write("dropGruppoDaConsegnare.SelectedValue: ------ -----" & Rs("id_gruppo_app"))
                        ' se l'elemento non è presente nella combo, non va in errore in questa fase
                        ' ma durante il metodo PreRender in cui viene effettivamente diseganto il componenete 
                        ' nella pagina restituita. Memorizzo i valori in variabili globali
                        ' e gestisco il problema sul metodo dropGruppoDaConsegnare_PreRender

                        Dim codice_provenienza As String = Rs("codice_provenienza") & ""
                        Select Case codice_provenienza
                            Case "55", "56", "58", "59"
                                Libreria.ListControlSelectvalueSicuro(dropGruppoDaConsegnare, Rs("id_gruppo_app") & "", "N.V.(" & Rs("COD_GRUPPO_APP") & ")")
                            Case Else
                                If (Rs("id_gruppo_app") & "") <> "" Then
                                    dropGruppoDaConsegnare.SelectedValue = Rs("id_gruppo_app") & ""
                                Else
                                    dropGruppoDaConsegnare.SelectedValue = "0"
                                End If
                        End Select

                        txtVoloOut.Text = Rs("N_VOLOOUT") & ""
                        txtVoloPr.Text = Rs("N_VOLOPR") & ""

                        txtSconto.Text = Rs("sconto_applicato")
                        If (Rs("tipo_sconto") & "") <> "" Then 'SOLO PER COMPATIBILITA' CON VECCHI DATI - IL TIPO SCONTO E' NECESSARIO SE LO SCONTO E' SPECIFICATO
                            dropTipoSconto.SelectedValue = Rs("tipo_sconto")
                        End If



                        lblScontoAttuale.Text = Rs("sconto_applicato")
                        lblTipoScontoAttuale.Text = Rs("tipo_sconto") & ""

                        If livello_accesso_sconto.Text = "3" Then
                            txtSconto.Enabled = True
                            dropTipoSconto.Enabled = True
                            txtCodiceSconto.Enabled = True
                            'btnApplicaCodiceSconto.Visible = True
                        Else
                            txtSconto.Enabled = False
                            dropTipoSconto.Enabled = False
                            txtCodiceSconto.Enabled = True
                            'btnApplicaCodiceSconto.Visible = True
                        End If

                        If (Rs("codice_convenzione") & "") <> "" Then
                            txtCodiceSconto.Text = Rs("codice_convenzione")
                            txtCodiceSconto.Enabled = False
                            'btnApplicaCodiceSconto.Visible = False
                            txtSconto.Enabled = False
                            dropTipoSconto.Enabled = False
                        ElseIf (Rs("sconto_web_prepagato") & "") <> "" Then
                            txtCodiceSconto.Text = "Sconto prepagato: " & Rs("sconto_web_prepagato") & "%"
                            txtCodiceSconto.Enabled = False
                            'btnApplicaCodiceSconto.Visible = False
                        End If
                        '----------------------------------------------------------------------------------------------------------------------

                        If richiamo_a_buon_fine Then
                            'FASE 3 - SELEZIONE DEL GRUPPO -----------------------------------------------------------------------------------------
                            listGruppi.DataBind()
                            table_gruppi.Visible = True

                            richiamo_a_buon_fine = False

                            For i = 0 To listGruppi.Items.Count - 1
                                Dim id_gruppo_lista As Label = listGruppi.Items(i).FindControl("id_gruppo")
                                If id_gruppo_lista.Text = Rs("id_gruppo") Then
                                    'CONTROLLO SE IL GRUPPO AUTO E' ANCORA VENDIBILE PER QUANTO RIGUARDA L'ETA' DEL GUIDATORE (NELL'EVENTO 
                                    'ITEM DATA BOUND DELLA TABELLA listGruppi IL CHECKBOX CORRISPONDENTE AL GRUPPO VIENE DISABILITATO SE LO STESSO
                                    'NON E' VENDIBILE)
                                    Dim check_gruppo As CheckBox = listGruppi.Items(i).FindControl("sel_gruppo")

                                    check_gruppo.Checked = True
                                    richiamo_a_buon_fine = True
                                End If
                            Next
                            table_gruppi.Visible = False
                            '-----------------------------------------------------------------------------------------------------------------------

                            If richiamo_a_buon_fine Then
                                'FASE 4 -VISUALIZZAZIONE DEL CALCOLO PRECEDENTE ----------------------------------------------------------------
                                Dim id_tariffe_righe As String = ""

                                If dropTariffeGeneriche.SelectedValue <> "0" Then
                                    id_tariffe_righe = dropTariffeGeneriche.SelectedValue
                                ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                                    id_tariffe_righe = dropTariffeParticolari.SelectedValue
                                End If

                                Dim min_giorni_nolo As String = funzioni_comuni.getMinGiorniNolo(id_tariffe_righe)
                                If min_giorni_nolo <> "-1" Then
                                    lblMinGiorniNolo.Text = "La tariffa prevede un minimo di " & min_giorni_nolo & " giorni/o di noleggio"
                                Else
                                    lblMinGiorniNolo.Text = ""
                                End If

                                If (Rs("importo_a_carico_del_broker") & "") <> "" Then
                                    'SE QUESTO VALORE E' VALORIZZATO LA TARIFFA E' BROKER, ALTRIMENTO NON LO E', QUESTO IN QUANTO
                                    'NON E' POSSIIBLE TRASFORMARE UNA TARIFFA DA BROKER A NON BROKER SE E' GIA' STATA UTILIZZATA
                                    tariffa_broker.Text = "1"

                                    Label132.Visible = False
                                    gruppoDaCalcolare.Visible = False

                                    txtNumeroGiorniTO.Text = Rs("giorni_to") & ""
                                    lblGiorniToOld.Text = Rs("giorni_to") & ""
                                    a_carico_del_broker.Text = Rs("importo_a_carico_del_broker")
                                    a_carico_del_broker_ultimo_calcolo.Text = a_carico_del_broker.Text

                                    If livello_accesso_broker.Text <> "1" Then
                                        colonna_list_prenotazioni.Width = "70%"
                                    Else
                                        colonna_list_prenotazioni.Width = "60%"
                                    End If
                                Else
                                    'NON C'E' IMPORTO A CARICO DEL BROKER ANCHE SE LA PRENOTAZIONE BROKER E' STATA SALVATA SENZA TARIFFA
                                    If prenotazione_no_tariffa.Text = "0" Then
                                        tariffa_broker.Text = "0"
                                        lblGiorniTO.Visible = False
                                        txtNumeroGiorniTO.Visible = False
                                        a_carico_del_broker.Text = ""
                                        a_carico_del_broker_ultimo_calcolo.Text = ""
                                        colonna_list_prenotazioni.Width = "60%"
                                    ElseIf prenotazione_no_tariffa.Text = "1" Then
                                        tariffa_broker.Text = "1"
                                        Label132.Visible = False
                                        gruppoDaCalcolare.Visible = False
                                        txtNumeroGiorniTO.Text = Rs("giorni_to") & ""
                                        lblGiorniToOld.Text = Rs("giorni_to") & ""
                                        a_carico_del_broker.Text = "0"
                                        If livello_accesso_broker.Text <> "1" Then
                                            colonna_list_prenotazioni.Width = "70%"
                                        Else
                                            colonna_list_prenotazioni.Width = "60%"
                                        End If
                                    End If
                                End If

                                'tariffa_broker.Text = funzioni_comuni.is_tariffa_broker(id_tariffe_righe)

                                If tariffa_broker.Text = "1" Then
                                    btnInviaMail.Visible = False
                                Else
                                    btnInviaMail.Visible = True
                                End If
                                'btnInviaMail.Visible = True '# SOLO X TEST

                                table_accessori_extra.Visible = True
                                sqlElementiExtra.SelectCommand = funzioni.getQueryElementiExtra(id_tariffe_righe, False)
                                dropElementiExtra.Items.Clear()
                                dropElementiExtra.Items.Add("Seleziona...")
                                dropElementiExtra.Items(0).Value = "0"
                                dropElementiExtra.DataBind()

                                tab_prenotazioni.Visible = True

                                If tariffa_broker.Text = "1" And statoPrenotazione.Text = "0" Then
                                    btnModificaPrenotazione.Visible = True
                                    btnRicalcola.Visible = False
                                    btnRicalcolaPrepagato.Visible = False
                                ElseIf tariffa_broker.Text = "0" And statoPrenotazione.Text = "0" Then
                                    'TARIFFA NON BROKER - PRENOTAZIONE ATTIVA - MODIFICABILE COME DATI DI PICK UP E DROP OFF
                                    btnModificaPrenotazione.Visible = False
                                    txtoraPartenza.Enabled = True
                                    txtOraRientro.Enabled = True
                                    txtDaData.Enabled = True
                                    txtAData.Enabled = True
                                    dropStazioneDropOff.Enabled = True
                                    dropStazionePickUp.Enabled = True
                                    btnRicalcola.Visible = True
                                    If Not riga_commissioni.Visible Then
                                        btnRicalcolaPrepagato.Visible = True
                                    Else
                                        btnRicalcolaPrepagato.Visible = False
                                    End If
                                Else
                                    btnModificaPrenotazione.Visible = False
                                    btnRicalcola.Visible = False
                                    btnRicalcolaPrepagato.Visible = False
                                End If
                            End If
                        Else
                            tab_prenotazioni.Visible = True
                            'btnModificaPrenotazione.Visible = True
                        End If
                    End If
                    '--------------------------------------------------------------------------------------------------------------------------
                    'SE LA PRENOTAZIONE NON E' NELLO STATO 0 (APERTA) NON DO LA POSSIBILITA' DI EFFETTUARE MODIFICA - STESSA COSA SE E' SCADUTA
                    'E NON E' TOTALMENTE MODIFICABILE (BROKER) --------------------------------------------------------------------------------
                    If (statoPrenotazione.Text <> "0") Then
                        btnInviaMail.Visible = False
                        btnModificaDatiCliente.Visible = False
                        btnModificaConducente.Visible = False
                        btnModificaDitta.Visible = False
                        'btnAnnullaPrenotazione.Visible = False
                        btnModificaPrenotazione.Visible = False
                        btnAggiungiExtra.Visible = False
                        btnPagamento.Visible = True
                        btnRicalcola.Visible = False
                        btnRicalcolaPrepagato.Visible = False
                        btnContratto.Visible = False
                        btnAssegnaTarga.Visible = False
                        If statoPrenotazione.Text = "3" Then  '18.03.2022
                            'PRENOTAZIONE CHE E' DIVENTATA UN CONTRATTO - NON MODIFICABILE, DEVO POTER ACCEDERE AL CONTRATTO

                            btnContratto.Visible = True
                            btnContratto.Text = "Vedi contratto"
                        End If
                    ElseIf prenotazioneScaduta.Text = "1" And tariffa_broker.Text = "0" Then
                        'PRENOTAZIONE NON BROKER SCADUTA
                        btnInviaMail.Visible = False
                        btnModificaDatiCliente.Visible = False
                        btnModificaConducente.Visible = False
                        btnModificaDitta.Visible = False
                        'btnAnnullaPrenotazione.Visible = False
                        btnModificaPrenotazione.Visible = False
                        btnAggiungiExtra.Visible = False
                        btnPagamento.Visible = True
                        btnRicalcola.Visible = True
                        btnRicalcolaPrepagato.Visible = False 'IN QUESTO CASO E' NECESSARIO RIATTIVARE UNA PRENOTAZIONE PER POTER PROCEDERE AL PREPAGAMENTO
                        btnContratto.Visible = False
                        btnAssegnaTarga.Visible = False
                        If statoPrenotazione.Text = "3" Then
                            'PRENOTAZIONE CHE E' DIVENTATA UN CONTRATTO - NON MODIFICABILE, DEVO POTER ACCEDERE AL CONTRATTO
                            btnContratto.Visible = True
                            btnContratto.Text = "Vedi contratto"
                        End If
                    Else
                        btnModificaDatiCliente.Visible = True
                        'btnPagamento.Visible = True
                        Dim uscita As DateTime = funzioni_comuni.getDataDb_senza_orario2(txtDaData.Text)
                        Dim oggi As DateTime = funzioni_comuni.getDataDb_senza_orario2(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()))

                        If DateDiff(DateInterval.Day, oggi, uscita) = 0 And dropStazionePickUp.SelectedValue = Request.Cookies("SicilyRentCar")("stazione") Then
                            'UNA PRENOTAZIONE ATTIVA PUO' DIVENTARE CONTRATTO SOLO SE E' DEL GIORNO ODIERNO
                            btnContratto.Visible = True
                        End If
                    End If

                    If tariffa_broker.Text = "1" Then
                        'PERMESSI PER MODIFICA PRENOTAZIONI BROKER
                        If livello_accesso_modifica_broker.Text <> "3" Then
                            btnModificaDatiCliente.Visible = False
                            btnModificaPrenotazione.Visible = False
                            btnModificaConducente.Visible = False
                            btnModificaDitta.Visible = False

                            txtDataDiNascita.Enabled = False
                            dropGruppoDaConsegnare.Enabled = False
                            txtVoloOut.Enabled = False
                            txtVoloPr.Enabled = False
                            txtRiferimentoTO.Enabled = False
                            txtNomeConducente.Enabled = False
                            txtCognomeConducente.Enabled = False
                            txtMailConducente.Enabled = False
                        End If
                        btnPagamento.Visible = True
                    End If

                    gestione_note.InitForm(enum_note_tipo.note_prenotazione, txtNumPrenotazione.Text, False, False)
                '--------------------------------------------------------------------------------------------------------------------------




            Else
                Libreria.genUserMsgBox(Me, "Prenotazione non trovata.")
                If provenienza.Text = "preventivi.aspx" Then
                    Session("errore_pren") = "Prenotazione non trovata."
                    Session("provenienza") = "prenotazioni.aspx"
                    Response.Redirect("preventivi.aspx")
                End If
            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing




        Catch ex As Exception
            Response.Write("errore richiamaPrenotazione: " & iderror.ToString & "-" & ex.Message & "<br/>")
        End Try





    End Sub






    Protected Sub btnRichiamaPrenotazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRichiamaPrenotazione.Click

        Try
            'richiama_prenotazione(txtNumPrenotazione.Text) 


            If txtNumPrenotazione.Text = "" Then
                Libreria.genUserMsgBox(Page, "Numero Prenotazione mancante")
                Exit Sub
            End If

            'sostituisce quello sopra 05.04.2022
            'Response.Redirect("prenotazioni.aspx?nr=" & txtNumPrenotazione.Text)
            Dim url As String = "prenotazioni.aspx?nr=" & txtNumPrenotazione.Text
            Dim sb As New StringBuilder()
            sb.Append("<script type = 'text/javascript'>")
            sb.Append("window.open('")
            sb.Append(url)
            sb.Append("');")
            sb.Append("</script>")
            ClientScript.RegisterStartupScript(Me.GetType(), "script", sb.ToString())


        Catch ex As Exception
            Response.Write("errore btn_richiamaPrenotazione: " & ex.Message & "<br/>")
        End Try


    End Sub


    Protected Sub btnRichiamaContratto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRichiamaContratto.Click

        Try



            If txt_num_contratto.Text = "" Then
                Libreria.genUserMsgBox(Page, "Numero Contratto mancante")
                Exit Sub
            End If

            'sostituisce quello sopra 05.04.2022
            Dim url As String = "contratti.aspx?nr=" & txt_num_contratto.Text
            Dim sb As New StringBuilder()
            sb.Append("<script type = 'text/javascript'>")
            sb.Append("window.open('")
            sb.Append(url)
            sb.Append("');")
            sb.Append("</script>")
            ClientScript.RegisterStartupScript(Me.GetType(), "script", sb.ToString())


        Catch ex As Exception
            Response.Write("errore btn_richiamaContratti: " & ex.Message & "<br/>")
        End Try


    End Sub

    Protected Sub btnRichiamaFatturaAnno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRichiamaFatturaAnno.Click

        Try

            Dim nfattAnno As String = txt_num_fattura_anno.Text


            If nfattAnno = "" Or ddl_anno_fattura.SelectedValue = "" Then
                Libreria.genUserMsgBox(Page, "Numero fattura/Anno mancante ")
                Exit Sub
            End If


            'recupera il numerocontratto 

            Dim nfatt As String = txt_num_fattura_anno.Text
            Dim anno As String = ddl_anno_fattura.SelectedValue
            Dim tipo_fattura As String = "2"


            If ck_fattura_multe.Checked = True Then
                tipo_fattura = "4"
            End If


            Dim b() As String = funzioni_comuni.GetRiferimentoFattura(nfatt, anno, tipo_fattura)
            Dim id_riferimento As String = b(0)
            Dim id_multa As String = b(1)


            If id_riferimento = "" Then
                Libreria.genUserMsgBox(Page, "Errore nel recupero del riferimento fattura o fattura Multe")
                Exit Sub
            End If

            'sostituisce quello sopra 05.04.2022
            Dim url As String = "contratti.aspx?nr=" & id_riferimento & "&h=1&idm=" & id_multa

            Dim sb As New StringBuilder()
            sb.Append("<script type = 'text/javascript'>")
            sb.Append("window.open('")
            sb.Append(url)
            sb.Append("');")
            sb.Append("</script>")
            ClientScript.RegisterStartupScript(Me.GetType(), "script", sb.ToString())


        Catch ex As Exception
            Response.Write("errore btn_richiamaContratti: " & ex.Message & "<br/>")
        End Try


    End Sub

    Protected Sub GetList_anno(id)

        'a seconda della data visualizzo elenco
        Dim ycur As Integer = Year(Date.Now)

        Dim xc As Integer
        Try

            For xc = ycur To 2013 Step -1

                Dim l As New ListItem(xc, xc, True)
                ddl_anno_fattura.Items.Add(l)
            Next

            ddl_anno_fattura.SelectedValue = ycur


        Catch ex As Exception
            Response.Write("error getList_anno: " & ex.Message & "<br/>")
        End Try


    End Sub

    Protected Sub listPrenotazioniCosti_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles listPrenotazioniCosti.DataBinding
        ultimo_gruppo.Text = ""
    End Sub

    Protected Function getNumPrenotazione(ByVal idPren As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT numpren FROM prenotazioni WITH(NOLOCK) WHERE id='" & idPren & "'", Dbc)

        getNumPrenotazione = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getNumContratto(ByVal idPrev As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT num_contratto FROM prenotazioni WITH(NOLOCK) WHERE id='" & idPrev & "'", Dbc)

        getNumContratto = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub btnAggiungiExtra_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAggiungiExtra.Click



        If txtSconto.Text = "" Then
            txtSconto.Text = "0"
        End If
        If CDbl(txtSconto.Text) <> CDbl(lblScontoAttuale.Text) Then
            txtSconto.Text = lblScontoAttuale.Text
        End If
        If dropTipoSconto.SelectedValue <> lblTipoScontoAttuale.Text Then
            dropTipoSconto.SelectedValue = lblTipoScontoAttuale.Text
        End If


        Dim idgruppoxaggiorna As String = "" 'aggiunto 10.11.2021 

        If dropElementiExtra.SelectedValue <> "0" Then
            For i = 0 To listGruppi.Items.Count - 1
                Dim sel_gruppo As CheckBox = listGruppi.Items(i).FindControl("sel_gruppo")
                Dim id_gruppo As Label = listGruppi.Items(i).FindControl("id_gruppo")
                If sel_gruppo.Checked Then
                    idgruppoxaggiorna = id_gruppo.Text 'aggiunto 10.11.2021 
                    If funzioni_comuni.accessorioExtraNonAggiunto(dropElementiExtra.SelectedValue, id_gruppo.Text, "", idPrenotazione.Text, "", numCalcolo.Text) Then
                        Dim imposta_prepagato As Boolean = False
                        Dim accessorio_non_prepagato As Boolean = False

                        If ricalcolaPrepagato.Text = "1" Then
                            imposta_prepagato = True
                        Else
                            accessorio_non_prepagato = True
                        End If

                        nuovo_accessorio(dropElementiExtra.SelectedValue, id_gruppo.Text, "ELEMENTO", txtEtaPrimo.Text, txtEtaSecondo.Text, imposta_prepagato, accessorio_non_prepagato)
                        'NEL CASO IN CUI L'ACCESSORIO EXTRA SIA IL GPS AGGIUNGO EVENTUALMENTE IL VAL
                        'SE E' STATO AGGINTO IL GPS AGGIUNGO EVENTUALMENTE IL VAL
                        If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
                            If funzioni_comuni.is_gps(dropElementiExtra.SelectedValue) Then
                                nuovo_accessorio("", id_gruppo.Text, "VAL_GPS", txtEtaPrimo.Text, txtEtaSecondo.Text, imposta_prepagato, accessorio_non_prepagato)
                            End If
                        End If
                    Else
                        Libreria.genUserMsgBox(Me, "L'accessorio è già stato aggiunto.")
                    End If
                End If
            Next

            ultimo_gruppo.Text = ""
            listPrenotazioniCosti.DataBind()

            aggiorna_commissioni_operatore()

        End If


        'richiama il pulsante aggiorna se richiamato Protezione Plus 'aggiunto 10.11.2021 
        If dropElementiExtra.SelectedValue = "248" Then 'protezione plus

            AggiornaPrenotazione(idgruppoxaggiorna)
        Else

        End If


        dropElementiExtra.SelectedIndex = -1        'riporta l'indice a zero 10.11.2021

    End Sub

    Protected Sub elimina_righe_calcolo(ByVal numpren As String)
        'VENGONO ELIMINATE LE RIGHE DI CALCOLO SUCCESSIVE A QUELLA ATTUALMENTE SALVATA 
        Dim sqla As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT MAX(num_calcolo) FROM prenotazioni WITH(NOLOCK) WHERE numpren='" & numpren & "'", Dbc)
            Dim num_calcolo_attuale As Integer = Cmd.ExecuteScalar

            Cmd = New Data.SqlClient.SqlCommand("SELECT NR_pren FROM prenotazioni WITH(NOLOCK) WHERE num_calcolo='" & num_calcolo_attuale & "' AND NUMPREN='" & numpren & "'", Dbc)
            Dim id_prenotazione As String = Cmd.ExecuteScalar

            Cmd = New Data.SqlClient.SqlCommand("DELETE FROM prenotazioni_costi WHERE id_documento='" & id_prenotazione & "' AND num_calcolo>" & num_calcolo_attuale, Dbc)
            Cmd.ExecuteNonQuery()
            Cmd = New Data.SqlClient.SqlCommand("DELETE FROM prenotazioni_warning WHERE id_documento='" & id_prenotazione & "' AND num_calcolo>" & num_calcolo_attuale, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error elimina_righe_calcolo  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Protected Sub btnAnnulla6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla6.Click
        If prenotazione_modificabile("0") Then
            elimina_righe_calcolo(txtNumPrenotazione.Text)
        End If

        If provenienza.Text = "preventivi.aspx" Then
            Session("provenienza") = "prenotazioni.aspx"
            Response.Redirect("preventivi.aspx")
        ElseIf provenienza.Text = "ribaltamento.aspx" Then
            'Session("provenienza") = "prenotazioni.aspx"
            Response.Redirect("ribaltamento.aspx")
        ElseIf provenienza.Text = "gestione_fatture.aspx" Then
            Dim from_fatture As TipoCodiceFattura = getTipoCodiceFattura()
            'Trace.Write("btnAnnulla_Click: " & from_fatture.anno_fattura & " " & from_fatture.codice_fattura & " " & from_fatture.id_riferimento)

            If Not from_fatture Is Nothing Then
                Session("ditte_from_fatture") = from_fatture
                Session("fatture_provenienza") = from_fatture.tipo_fattura
                Response.Redirect("gestione_fatture.aspx")
            End If
        Else
            tab_cerca_tariffe.Visible = False
            tab_ricerca.Visible = True
        End If


    End Sub

    Protected Function getImportoPreventivo(ByVal id_preventivo As String, ByVal num_calcolo As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT imponibile_scontato+iva_imponibile FROM preventivi_costi WITH(NOLOCK) WHERE id_documento='" & id_preventivo & "' AND num_calcolo='" & num_calcolo & "' AND ordine_stampa='6'", Dbc)

        getImportoPreventivo = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub btnModificaConducente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaConducente.Click
        'If id_conducente.Text <> "" Then
        '    'SE L'UTENTE ERA STATO PRECEDENTEMENTE SELEZIONATO
        '    '1) L'ETA' VIENE AZZERATA
        '    txtEtaPrimo.Text = ""
        '    'SE PER L'UTENTE ERA STATO AGGIUNTO LO YOUNG DRIVER LO RIMUOVO
        '    If funzioni.esiste_young_driver_primo_guidatore(get_id_young_driver(), id_gruppo_auto_scelto.Text, numCalcolo.Text, "", idPrenotazione.Text, "") Then
        '        funzioni.rimuovi_costo_accessorio("", "", idPrenotazione.Text, numCalcolo.Text, id_gruppo_auto_scelto.Text, get_id_young_driver(), "1", "EXTRA")
        '        listPrenotazioniCosti.DataBind()
        '        libreria.genUserMsgBox(Me, "Rimosso il costo dello Young Driver per il primo guidatore.")
        '    End If
        'End If

        If btnModificaConducente.Text = "Scegli" Then
            id_conducente.Text = ""

            image_primo_guidatore.Visible = False

            'txtNomeConducente.Text = ""
            txtNomeConducente.ReadOnly = False
            txtIndirizzoConducente.Text = ""
            'txtCognomeConducente.Text = ""
            txtCognomeConducente.ReadOnly = False
            'txtMailConducente.Text = ""
            txtIndirizzoConducente.Text = ""
            txtMailConducente.ReadOnly = False

            'txtDataDiNascita.Text = ""
            txtDataDiNascita.ReadOnly = False

            anagrafica_conducenti.Visible = True
            anagrafica_ditte.Visible = False

            btnContratto.Visible = False

            btnModificaConducente.Text = "Chiudi"
        ElseIf btnModificaConducente.Text = "Chiudi" Then
            anagrafica_conducenti.Visible = False

            btnModificaConducente.Text = "Scegli"
        End If

    End Sub

    Protected Sub btnCambiaTariffa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCambiaTariffa.Click
        'ABILITO I CONTROLLI PER LA MODIFICA DELLA TARIFFA E DELLO SCONTO ED ELIMINO LE ATTUALI RIGHE DI CALCOLO
        dropTariffeGeneriche.Enabled = True
        dropTariffeParticolari.Enabled = True
        txtSconto.ReadOnly = False
        dropTipoSconto.Enabled = True
        txtCodiceSconto.Enabled = True
        'btnApplicaCodiceSconto.Visible = True
        btnCambiaTariffa.Visible = False

        table_accessori_extra.Visible = False

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM prenotazioni_costi WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM prenotazioni_warning WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        listPrenotazioniCosti.DataBind()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub btnModificaPrenotazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaPrenotazione.Click
        If prenotazione_modificabile("0") Then
            'MODIFICA PRENOTAZIONE - NEL CASO DI BROKER
            If tipo_prenotazione.Text = "richiama" Then
                numCalcolo.Text = CInt(numCalcolo.Text) + 1
                tipo_prenotazione.Text = "modifica"

                lbl_tariffa_broker_salvata.Text = dropTariffeParticolari.SelectedValue
            End If

            id_tariffa_broker.Text = dropTariffeParticolari.SelectedValue

            tab_conducenti.Visible = False
            gestione_note.Visible = False
            tab_annullamento.Visible = False

            btnSalvaModifiche.Visible = False
            btnVediUltimoCalcolo.Visible = False
            btnModificaDatiCliente.Visible = False
            btnStampa.Visible = False

            old_ultimo_gruppo.Text = ""
            listVecchioCalcolo.Visible = False
            btnVediUltimoCalcolo.Visible = False

            btnContratto.Visible = False
            'btnPagamento.Visible = False
            btnInviaMail.Visible = False


            'PER EVENTUALI CALCOLI PRECEDENTI ELIMINO LE RIGHE DI CALCOLO PER IL NUMERO DI CALCOLO ATTUALE ----------------------------------------------------------
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM prenotazioni_costi WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd = New Data.SqlClient.SqlCommand("DELETE FROM prenotazioni_warning WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
            Cmd.ExecuteNonQuery()
            '----------------------------------------------------------------------------------------------------------------------------------

            'SETTO CORRETTAMENTE LA SEZIONE DI RICERCA COSTO PER UN NUOVO PREVENTIVO-----------------------------------------------------------
            'lblTipoDocumento.Text = ""
            listPrenotazioniCosti.DataBind()

            dropTariffeGeneriche.Enabled = True
            dropTariffeParticolari.Enabled = True
            txtSconto.ReadOnly = False
            dropTipoSconto.Enabled = True
            txtCodiceSconto.Enabled = True
            'btnApplicaCodiceSconto.Visible = True
            btnCambiaTariffa.Visible = False

            lblMxSconto.Visible = False

            tab_ricerca.Visible = False
            tab_cerca_tariffe.Visible = True

            'tab_prenotazioni.Visible = False
            table_tariffe.Visible = False
            table_accessori_extra.Visible = False

            table_gruppi.Visible = False

            btnModificaPrenotazione.Visible = False
            btnCerca.Visible = True
            'btnAnnulla0.Visible = True
            dropStazionePickUp.Enabled = True
            dropStazioneDropOff.Enabled = True
            txtoraPartenza.Enabled = True
            txtOraRientro.Enabled = True
            ore1.Enabled = True
            minuti1.Enabled = True
            ore2.Enabled = True
            minuti2.Enabled = True
            'dropTipoCliente.Enabled = True
            txtDaData.Enabled = True
            txtAData.Enabled = True
            txtEtaPrimo.Enabled = True
            txtEtaSecondo.Enabled = True
            txtNumeroGiorni.Enabled = True

            dropTariffeGeneriche.Enabled = True
            dropTariffeParticolari.Enabled = True

            'ABILITO LO SCONTO SOLAMENTE SE IL LIVELLO ACCESSO DEL RELATIVO PERMESSO E' 3
            If livello_accesso_sconto.Text = "3" Then
                txtSconto.Enabled = True
                dropTipoSconto.Enabled = True
                txtCodiceSconto.Enabled = True
                'btnApplicaCodiceSconto.Visible = True
            Else
                txtSconto.Enabled = False
                dropTipoSconto.Enabled = False
                txtCodiceSconto.Enabled = True
                'btnApplicaCodiceSconto.Visible = True
            End If

            riga_broker.Visible = True

            'VISTO CHE SIAMO NEL CASO DI BROKER, CONTROLLO SE LA VARIAZIONE E' SEMPRE A CARICO DEL CLIENTE O SE E' A SCELTA DELL'UTENTE
            '-----------------------------------------------------------------------------------------------------------------------------
            Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(estensioni_sempre_a_carico_del_broker,'0') FROM clienti_tipologia WITH(NOLOCK) WHERE id='" & dropTipoCliente.SelectedValue & "'", Dbc)

            Dim sempre_a_carico_del_broker As Boolean = Cmd.ExecuteScalar

            If sempre_a_carico_del_broker Then
                dropVariazioneACaricoDi.SelectedValue = "1"
                dropVariazioneACaricoDi.Enabled = False
            Else
                'NEL CASO DI PRENOTAZIONE SENZA TARIFFA LA SCELTA E' PER FORZA A CARICO DEL BROKER
                If prenotazione_no_tariffa.Text = "1" Then
                    dropVariazioneACaricoDi.SelectedValue = "1"
                    dropVariazioneACaricoDi.Enabled = False
                Else
                    dropVariazioneACaricoDi.SelectedValue = "-1"
                    dropVariazioneACaricoDi.Enabled = True
                End If
            End If
            '-----------------------------------------------------------------------------------------------------------------------------

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: la prenotazione non è più modificabile in quanto è variato lo stato. E' necessario tornare alla maschera di ricerca e ricaricare la prenotazione.")
        End If
    End Sub

    Protected Function getImportoPrepagato() As Double
        Dim nome_costo As Label
        Dim importo_prepagato As Double
        For i = 0 To listPrenotazioniCosti.Items.Count - 1
            nome_costo = listPrenotazioniCosti.Items(i).FindControl("nome_costo")

            If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
                Dim lblImpPrepagato As Label = listPrenotazioniCosti.Items(i).FindControl("costo_prepagato")
                importo_prepagato = CDbl(lblImpPrepagato.Text)
            End If
        Next

        'SE ERA GIA' STATO PREPAGATO SOTTRAGGO AL TOTALE L'IMPORTO GIA' PREPAGATO
        If txtPOS_TotIncassato.Text <> "" Then
            importo_prepagato = importo_prepagato - CDbl(txtPOS_TotIncassato.Text)
        End If

        getImportoPrepagato = importo_prepagato


    End Function

    Protected Sub salvaPrenotazione(ByVal imposta_prepagato As Boolean)
        Dim sqla As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Dbc1 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc1.Open()
            Dim Cmd1 As New Data.SqlClient.SqlCommand("", Dbc1)

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM prenotazioni WITH(NOLOCK) WHERE Nr_Pren='" & idPrenotazione.Text & "' AND attiva='1'", Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.Read() Then
                Dim num_prenotazione As Integer = Rs("NUMPREN")  'IL NUMERO DI PRENOTAZIONE NON CAMBIA

                Dim data_prenotazione As String = getDataDb_con_orario(Rs("datapren"), Request.ServerVariables("HTTP_HOST"))

                'TROVO L'ID-TARIFFA SCELTA --- NEL MENU A TENDINA IL SELECTED VALUE E' L'ID DI tariffe_righe
                Dim id_tariffe_righe As Integer
                Dim id_tariffa As Integer
                Dim tipo_tariffa As String = ""
                Dim codice_tariffa As String
                Dim cod As String


                'FACCIO ANCHE IN MODO CHE LA TARIFFA SCELTA SIA L'UNICA PRESENTE NEL MENU' A TENDINA (SERVE PER SUCCESSIVE MODIFICHE)
                If dropTariffeGeneriche.SelectedValue <> "0" Then
                    id_tariffe_righe = dropTariffeGeneriche.SelectedValue
                    codice_tariffa = dropTariffeGeneriche.SelectedItem.Text
                    tipo_tariffa = "generica"

                    dropTariffeGeneriche.Items.Clear()
                    dropTariffeGeneriche.Items.Add("Seleziona...")
                    dropTariffeGeneriche.Items(0).Value = "0"

                    dropTariffeGeneriche.Items.Add(codice_tariffa.Replace(" (PREN)", ""))
                    dropTariffeGeneriche.Items(1).Value = id_tariffe_righe

                    dropTariffeGeneriche.SelectedValue = id_tariffe_righe
                ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                    id_tariffe_righe = dropTariffeParticolari.SelectedValue
                    codice_tariffa = dropTariffeParticolari.SelectedItem.Text
                    tipo_tariffa = "fonte"

                    dropTariffeParticolari.Items.Clear()
                    dropTariffeParticolari.Items.Add("Seleziona...")
                    dropTariffeParticolari.Items(0).Value = "0"

                    dropTariffeParticolari.Items.Add(codice_tariffa.Replace(" (PREN)", ""))
                    dropTariffeParticolari.Items(1).Value = id_tariffe_righe

                    dropTariffeParticolari.SelectedValue = id_tariffe_righe
                End If

                Cmd1 = New Data.SqlClient.SqlCommand("SELECT id_tariffa FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc1)
                id_tariffa = Cmd1.ExecuteScalar

                'Cmd1 = New Data.SqlClient.SqlCommand("SELECT codice FROM tariffe WITH(NOLOCK) WHERE id='" & id_tariffa & "'", Dbc1)
                'codice_tariffa = Cmd1.ExecuteScalar

                Cmd1 = New Data.SqlClient.SqlCommand("SELECT CODTAR FROM tariffe WITH(NOLOCK) WHERE id='" & id_tariffa & "'", Dbc1)
                cod = Cmd1.ExecuteScalar

                Dim data_uscita As String = txtDaData.Text
                Dim data_rientro As String = txtAData.Text

                data_uscita = getDataDb_senza_orario(data_uscita, Request.ServerVariables("HTTP_HOST"))
                data_rientro = getDataDb_senza_orario(data_rientro, Request.ServerVariables("HTTP_HOST"))

                Dim data_di_nascita As String = txtDataDiNascita.Text

                If data_di_nascita = "" Then
                    data_di_nascita = "NULL"
                Else
                    data_di_nascita = "'" & getDataDb_senza_orario(txtDataDiNascita.Text, Request.ServerVariables("HTTP_HOST")) & "'"
                End If

                Dim conducente As String
                If id_conducente.Text = "" Then
                    conducente = "NULL"
                Else
                    conducente = "'" & id_conducente.Text & "'"
                End If

                Dim id_preven As String = Rs("id_preventivo") & ""
                If id_preven <> "" Then
                    id_preven = "'" & id_preven & "'"
                Else
                    id_preven = "NULL"
                End If

                Dim idDitta As String

                If id_ditta.Text <> "" Then
                    idDitta = "'" & id_ditta.Text & "'"
                Else
                    idDitta = "NULL"
                End If

                Dim codEdp As String

                If txtCodiceCliente.Text <> "" Then
                    codEdp = "'" & txtCodiceCliente.Text & "'"
                Else
                    codEdp = "NULL"
                End If

                'CAMPI DA GENERARE PER IL FUNZIONAMENTO DI P1000---------------------------------------------------------------------------------
                'PRORA_OUT - PRORA_PR: 01/01/1900 HH:mm:00
                Dim prora_out As String  'ORARIO USCITA
                Dim prora_pr As String   'ORARIO PRESUNTO RIENTRO
                prora_out = "1900-01-01 " & ore1.Text & ":" & minuti1.Text
                prora_pr = "1900-01-01 " & ore2.Text & ":" & minuti2.Text
                'TAR_VAL_DAL - TAR_VAL_AL
                Dim TAR_VAL_DAL As String
                Dim TAR_VAL_AL As String

                Cmd1 = New Data.SqlClient.SqlCommand("SELECT vendibilita_da FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc1)
                TAR_VAL_DAL = Cmd1.ExecuteScalar

                Cmd1 = New Data.SqlClient.SqlCommand("SELECT vendibilita_A FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc1)
                TAR_VAL_AL = Cmd1.ExecuteScalar

                TAR_VAL_DAL = funzioni_comuni.getDataDb_senza_orario(TAR_VAL_DAL)

                TAR_VAL_AL = funzioni_comuni.getDataDb_senza_orario(TAR_VAL_AL)

                'IMPORTO PREVENTIVO
                Dim importo_preventivo As String = Rs("importo_preventivo") & ""

                If importo_preventivo <> "" Then
                    importo_preventivo = "'" & Replace(importo_preventivo, ",", ".") & "'"
                Else
                    importo_preventivo = "NULL"
                End If

                'PREPAGATA
                Dim prepagata As String = Rs("prepagata") & ""
                If prepagata = "True" Or imposta_prepagato Then
                    prepagata = "'1'"
                ElseIf prepagata = "" Or prepagata = "False" Then
                    prepagata = "'0'"
                End If

                Dim importo_prepagato As String
                If imposta_prepagato Then
                    importo_prepagato = getImportoPrepagato()
                Else
                    importo_prepagato = Rs("importo_prepagato") & ""
                End If

                If importo_prepagato = "" Then
                    importo_prepagato = "NULL"
                Else
                    importo_prepagato = "'" & Replace(importo_prepagato, ",", ".") & "'"
                End If

                Dim giorni_prepagati As String
                If imposta_prepagato Then
                    giorni_prepagati = CInt(txtNumeroGiorni.Text)
                Else
                    giorni_prepagati = Rs("giorni_prepagati") & ""
                End If

                If giorni_prepagati = "" Then
                    giorni_prepagati = "NULL"
                Else
                    giorni_prepagati = "'" & giorni_prepagati & "'"
                End If

                Dim sconto_web_prepagato As String
                If imposta_prepagato Then
                    If (Rs("sconto_web_prepagato") & "") <> "" Then
                        sconto_web_prepagato = "'" & Replace(Rs("sconto_web_prepagato"), ",", ".") & "'"
                    ElseIf imposta_prepagato Then
                        sconto_web_prepagato = "'" & Replace(txtSconto.Text, ",", ".") & "'"
                    ElseIf txtCodiceSconto.Enabled = False Then
                        'RIPORTARE IL CODICE SCONTO IMPOSTATO
                    Else
                        sconto_web_prepagato = "NULL"
                    End If
                Else
                    If (Rs("sconto_web_prepagato") & "") <> "" Then
                        sconto_web_prepagato = "'" & Replace(Rs("sconto_web_prepagato"), ",", ".") & "'"
                    Else
                        sconto_web_prepagato = "NULL"
                    End If
                End If


                Dim codice_convenzione As String
                If Not txtCodiceSconto.Enabled Then
                    codice_convenzione = "'" & Replace(txtCodiceSconto.Text, "'", "''") & "'"
                Else
                    codice_convenzione = "NULL"
                End If

                'REGISTRAZIONE OPERAZIONI
                Dim op_ripristino As String
                Dim data_ripristino As String
                Dim op_annullamento As String
                Dim data_annullamento As String

                If (Rs("id_operatore_ripristino") & "") = "" Then
                    op_ripristino = "NULL"
                Else
                    op_ripristino = "'" & Rs("id_operatore_ripristino") & "'"
                End If
                If (Rs("id_operatore_annullamento") & "") = "" Then
                    op_annullamento = "NULL"
                Else
                    op_annullamento = "'" & Rs("id_operatore_annullamento") & "'"
                End If
                If (Rs("data_ripristino") & "") = "" Then
                    data_ripristino = "NULL"
                Else
                    data_ripristino = "'" & funzioni_comuni.getDataDb_con_orario(Rs("data_ripristino")) & "'"
                End If
                If (Rs("data_annullamento") & "") = "" Then
                    data_annullamento = "NULL"
                Else
                    data_annullamento = "'" & funzioni_comuni.getDataDb_con_orario(Rs("data_annullamento")) & "'"
                End If
                '--------------------------------------------------------------------------------------------------------------------------------

                'STATUS: 0 - PRENOTATO
                '        1 - EFFETTUATO (DIVENTATO CONTRATTO)
                '        2 - NO SHOW
                '        3 - REFUSAL (ANNULLATO DA OPERATORE)
                '        4 - CANCEL (ANNULLATO DA CLIENTE)

                Dim id_gruppo_da_consegnare As String
                If dropGruppoDaConsegnare.SelectedValue = "0" Then
                    id_gruppo_da_consegnare = "NULL"
                Else
                    id_gruppo_da_consegnare = "'" & dropGruppoDaConsegnare.SelectedValue & "'"
                End If

                Dim imp_a_carico_del_broker As String
                Dim imp_a_carico_del_broker_ribaltato As String
                Dim gg_a_carico_del_broker_ribaltato As String
                Dim giorni_to As String

                If (Rs("importo_a_carico_del_broker") & "") <> "" Or prenotazione_no_tariffa.Text = "1" Then
                    'NEL CASO IN CUI SI ENTRA PERCHE' LA PRENOTAZIONE E' STATA EFFETTUATA SENZA TARIFFA (SOLO NEL CASO DI BROKER!!!)
                    'LA VARIAZIONE E' ERTAMENTE A CARICO DEL BROKER
                    If dropVariazioneACaricoDi.Text = "1" Then
                        'SE E' A CARICO DEL BROKER
                        a_carico_del_broker.Text = getCostoACaricoDelBroker(numCalcolo.Text, id_gruppo_auto_scelto.Text)
                        a_carico_del_broker_ultimo_calcolo.Text = a_carico_del_broker.Text
                        imp_a_carico_del_broker = "'" & Replace(a_carico_del_broker.Text, ",", ".") & "'"
                    Else
                        'SE E' A CARICO DEL CLIENTE IL COSTO A CARICO DEL BROKER E' QUELLO PRECEDENTE
                        imp_a_carico_del_broker = "'" & Replace(Rs("importo_a_carico_del_broker"), ",", ".") & "'"
                    End If
                    giorni_to = "'" & txtNumeroGiorniTO.Text & "'"
                    lblGiorniToOld.Text = txtNumeroGiorniTO.Text

                    If (Rs("importo_a_carico_del_broker_ribaltato") & "") <> "" Then
                        imp_a_carico_del_broker_ribaltato = "'" & Replace(Rs("importo_a_carico_del_broker_ribaltato"), ",", ".") & "'"
                        gg_a_carico_del_broker_ribaltato = "'" & Rs("gg_a_carico_del_broker_ribaltato") & "'"
                    Else
                        imp_a_carico_del_broker_ribaltato = "NULL"
                        gg_a_carico_del_broker_ribaltato = "NULL"
                    End If
                Else
                    imp_a_carico_del_broker = "NULL"
                    giorni_to = "NULL"
                    imp_a_carico_del_broker_ribaltato = "NULL"
                    gg_a_carico_del_broker_ribaltato = "NULL"
                End If

                Dim targa_gruppo_speciale As String
                If lblTargaSelezionata.Text <> "" Then
                    targa_gruppo_speciale = "'" & Replace(lblTargaSelezionata.Text, "'", "''") & "'"
                Else
                    targa_gruppo_speciale = "NULL"
                End If

                Dim id_tempo_km_rack As String
                If (Rs("id_tempo_km_rack") & "") <> "" Then
                    id_tempo_km_rack = Rs("id_tempo_km_rack")
                Else
                    id_tempo_km_rack = "NULL"
                End If

                Dim id_fonte_commissionabile As String
                If (Rs("id_fonte_commissionabile") & "") <> "" Then
                    id_fonte_commissionabile = Rs("id_fonte_commissionabile")
                Else
                    id_fonte_commissionabile = "NULL"
                End If

                Dim tipo_commissione As String
                If (Rs("tipo_commissione") & "") <> "" Then
                    tipo_commissione = Rs("tipo_commissione")
                Else
                    tipo_commissione = "NULL"
                End If

                Dim commissione_percentuale As String
                If (Rs("commissione_percentuale") & "") <> "" Then
                    commissione_percentuale = Replace(Rs("commissione_percentuale"), ",", ".")
                Else
                    commissione_percentuale = "NULL"
                End If

                Dim giorni_commissioni As String
                If (Rs("giorni_commissioni") & "") <> "" Then
                    giorni_commissioni = Rs("giorni_commissioni")
                Else
                    giorni_commissioni = "NULL"
                End If

                Dim sconto As String = Replace(txtSconto.Text, ",", ".")
                If imposta_prepagato Then
                    sconto = "0"
                End If

                Dim id_gruppo_originale_prepagato As String
                If imposta_prepagato Then
                    id_gruppo_originale_prepagato = "'" & id_gruppo_auto_scelto.Text & "'"
                Else
                    id_gruppo_originale_prepagato = "'" & Rs("id_gruppo_originale_prepagato") & "" & "'"
                End If

                sqla = "INSERT INTO prenotazioni (NUMPREN,num_calcolo,status,attiva,provenienza,codice_provenienza,PRID_stazione_out,PRID_stazione_pr," &
                "PRDATA_OUT,ore_uscita,minuti_uscita,PRDATA_PR,ore_rientro,minuti_rientro,ID_GRUPPO,ID_GRUPPO_APP,id_gruppo_originale_prepagato,id_conducente,nome_conducente," &
                "cognome_conducente,eta_primo_guidatore,eta_secondo_guidatore,data_nascita,mail_conducente,indirizzo_conducente,riferimento_telefono,rif_to,id_fonte, id_fonte_commissionabile, tipo_commissione, commissione_percentuale, giorni_commissioni,codice_edp, id_cliente,id_tariffa, id_tariffe_righe,tipo_tariffa,sconto_applicato, tipo_sconto, sconto_web_prepagato," &
                "id_preventivo,id_operatore_ultima_modifica,data_ultima_modifica,id_operatore_creazione,DATAPREN,PRORA_OUT,PRORA_PR,giorni,giorni_to,CODTAR,codice,TAR_VAL_DAL,TAR_VAL_AL, codice_convenzione, N_VOLOOUT,N_VOLOPR," &
                "IMPORTO_PREVENTIVO, id_operatore_annullamento,data_annullamento,id_operatore_ripristino,data_ripristino, prepagata, importo_prepagato, giorni_prepagati, importo_a_carico_del_broker, importo_a_carico_del_broker_ribaltato, gg_a_carico_del_broker_ribaltato, targa_gruppo_speciale, id_tempo_km_rack, commissioni_da_assegnare_web)" &
                " VALUES " &
                "('" & num_prenotazione & "','" & numCalcolo.Text & "','" & Rs("status") & "" & "','1','" & Replace(Rs("provenienza") & "", "'", "''") & "','" & Rs("codice_provenienza") & "" & "','" & dropStazionePickUp.SelectedValue & "','" & dropStazioneDropOff.SelectedValue & "'," &
                "convert(datetime,'" & data_uscita & "',102),'" & ore1.Text & "','" & minuti1.Text & "',convert(datetime,'" & data_rientro & "',102)," &
                "'" & ore2.Text & "','" & minuti2.Text & "','" & id_gruppo_auto_scelto.Text & "'," &
                id_gruppo_da_consegnare & "," & id_gruppo_originale_prepagato & "," & conducente & "," &
                "'" & Replace(txtNomeConducente.Text, "'", "''") & "','" & Replace(txtCognomeConducente.Text, "'", "''") & "'," &
                "'" & txtEtaPrimo.Text & "','" & txtEtaSecondo.Text & "',convert(datetime," & data_di_nascita & ",102),'" & Replace(txtMailConducente.Text, "'", "''") & "','" & Replace(txtIndirizzoConducente.Text, "'", "''") & "','" & Replace(txtRifTel.Text, "'", "''") & "','" & Replace(txtRiferimentoTO.Text, "'", "''") & "'," &
                "'" & dropTipoCliente.SelectedValue & "'," & id_fonte_commissionabile & "," & tipo_commissione & "," & commissione_percentuale & "," & giorni_commissioni & "," & codEdp & "," & idDitta & ",'" & id_tariffa & "','" & id_tariffe_righe & "','" & tipo_tariffa & "','" & sconto & "','" & dropTipoSconto.SelectedValue & "'," & sconto_web_prepagato & "," & id_preven & "," &
                "'" & Request.Cookies("SicilyRentCar")("idUtente") & "',convert(datetime,GetDate(),102),'" & Rs("id_operatore_creazione") & "" & "',convert(datetime,'" & data_prenotazione & "',102)," &
                "'" & prora_out & "','" & prora_pr & "','" & txtNumeroGiorni.Text & "'," & giorni_to & ",'" & Replace(codice_tariffa, "'", "''") & "','" & Replace(cod, "'", "''") & "'," &
                "convert(datetime,'" & TAR_VAL_DAL & "',102),convert(datetime,'" & TAR_VAL_AL & "',102)," & codice_convenzione & ",'" & Replace(txtVoloOut.Text, "'", "''") & "'," &
                "'" & Replace(txtVoloPr.Text, "'", "''") & "'," & importo_preventivo & "," & op_annullamento & ",convert(datetime," & data_annullamento & ",102)," & op_ripristino & ",convert(datetime," &
                data_ripristino & ",102)," & prepagata & "," & importo_prepagato & "," & giorni_prepagati & "," & imp_a_carico_del_broker & "," & imp_a_carico_del_broker_ribaltato & "," &
                gg_a_carico_del_broker_ribaltato & "," & targa_gruppo_speciale & "," & id_tempo_km_rack & ",'0')"

                Dbc.Close()
                Dbc.Open()

                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                Cmd.ExecuteNonQuery()

                Dim id_prenotazione As String
                sqla = "SELECT @@IDENTITY FROM prenotazioni WITH(NOLOCK)"
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                id_prenotazione = Cmd.ExecuteScalar

                'SEGNO COME NON ATTIVA LA RIGA PRECEDENTE
                sqla = "UPDATE prenotazioni SET attiva='0' WHERE Nr_Pren='" & idPrenotazione.Text & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                Cmd.ExecuteScalar()

                'msg solo se non viene da aggiornamento ?? 17.12.2021
                If Session("ricalcola_prenotazione_salva") = "" Then
                    Libreria.genUserMsgBox(Me, "Prenotazione " & num_prenotazione & " modificata correttamente")
                End If



                'AGGIORNO WARNING E CALCOLO COL NUOVI ID
                sqla = "UPDATE prenotazioni_costi SET id_documento='" & id_prenotazione & "' WHERE num_calcolo='" & numCalcolo.Text & "' AND id_documento='" & idPrenotazione.Text & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                Cmd.ExecuteNonQuery()
                sqla = "UPDATE prenotazioni_warning SET id_documento='" & id_prenotazione & "' WHERE num_calcolo='" & numCalcolo.Text & "' AND id_documento='" & idPrenotazione.Text & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                Cmd.ExecuteNonQuery()

                idPrenotazione.Text = id_prenotazione

                prenotazione_no_tariffa.Text = "0"

                aggiorna_commissioni_operatore()

            Else
                Libreria.genUserMsgBox(Me, "Prenotazione non più attiva: impossibile procedere con la modifica.")
            End If
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc1.Close()
            Dbc1.Dispose()
            Dbc1 = Nothing

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception

            HttpContext.Current.Response.Write("error prenotazioni salvaPrenotazione : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Protected Function prenotazione_modificabile(ByVal status_in_cui_dovremmo_essere As String) As Boolean
        'PER ESSERE MODIFICABILE IL RECORD ATTUALE DEVE ESSERE ATTIVO E LO STATO QUELLO PASSATO COME ARGOMENTO 
        'NEL CASO DI PRENOTAZIONE ANNULLATA AGGIUNGO ANCHE LA CONDIZIONE PER annullata_da_rimborsare

        Dim status As String
        If status_in_cui_dovremmo_essere = "2" Then
            status = "(status='2' OR status='1')"
        Else
            status = "status='" & status_in_cui_dovremmo_essere & "'"
        End If

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT Nr_Pren FROM prenotazioni WHERE " & status & " AND attiva='1' AND Nr_Pren='" & idPrenotazione.Text & "'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test <> "" Then
            prenotazione_modificabile = True
        Else
            prenotazione_modificabile = False
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub salva_modifiche(ByVal imposta_prepagato As Boolean)
        'PER PRIMA COSA, SE SPECIFICATO, VIENE CONTROLLATA L'UNICITA' DEL NUMERO DI RIFERIMENTO DEL TOUR OPERATOR

        Dim sqla As String = ""
        Dim sconto As String = txtSconto.Text 'salvo aggiunto 19.02.2023

        Try
            If check_rif_to() Or lblRifToOld.Text = txtRiferimentoTO.Text Then
                lblRifToOld.Text = txtRiferimentoTO.Text
                lblRiferimentoEsistente.Visible = False
                'SALVO UNA NUOVA RIGA DI PRENOTAZIONI - SE L'ETA' PERMETTE IL SALVATAGGIO
                If prenotazione_modificabile("0") Then
                    If Trim(txtNomeConducente.Text) <> "" And Trim(txtCognomeConducente.Text) <> "" Then
                        'CONTROLLO LA BLACK LIST 
                        If check_black_list() Then
                            If (Hour(txtoraPartenza.Text) = CInt(ore1.Text) And Minute(txtoraPartenza.Text) = CInt(minuti1.Text) And Hour(txtOraRientro.Text) = CInt(ore2.Text) And Minute(txtOraRientro.Text) = CInt(minuti2.Text) And OldStazioneDropOff.Text = dropStazioneDropOff.SelectedValue And OldStazionePickUp.Text = dropStazionePickUp.SelectedValue And txtOldAData.Text = txtAData.Text And txtOldDaData.Text = txtDaData.Text And id_gruppo_auto_scelto.Text = gruppoDaCalcolare.SelectedValue) Or (tariffa_broker.Text = "1") Then
                                Dim check_eta As String = ""
                                If txtDataDiNascita.Text <> "" Then
                                    'SE NON E' STATO SELEZIONATO UN CONDUCENTE MA E' STATA SPECIFICATA UNA DATA DI NASCITA E' NECESSARIO CONTROLLARE SE C'E' 
                                    'VARIAZIONE SULL'ETA' 
                                    Dim test_eta As Integer
                                    Dim month_nascita As Integer = Month(txtDataDiNascita.Text)
                                    Dim day_nascita As Integer = Day(txtDataDiNascita.Text)
                                    Dim data_nascita As DateTime = getDataDb_senza_orario2(txtDataDiNascita.Text, Request.ServerVariables("HTTP_HOST"))

                                    test_eta = DateDiff(DateInterval.Year, data_nascita, Now())

                                    If Month(Now()) < month_nascita Then
                                        test_eta = CInt(test_eta) - 1
                                    ElseIf Month(Now()) = month_nascita And Day(Now()) < day_nascita Then
                                        test_eta = CInt(test_eta) - 1
                                    End If

                                    If CStr(test_eta) <> txtEtaPrimo.Text Then

                                        check_eta = funzioni_comuni.gruppo_vendibile_eta_guidatori(id_gruppo_auto_scelto.Text, test_eta, "", "", "", "", "", "", "", False)

                                        If check_eta = "0" Then
                                            'L'AUTO NON E' VENDIBILE - NON E' POSSIBILE COLLEGARE IL GUIDATORE ALLA PRENOTAZIONE DA SALVARE

                                        ElseIf check_eta = "1" Then
                                            'IN QUESTO CASO IL GRUPPO AUTO E' VENDIBILE MA CON SUPPLEMENTO YOUNG DRIVER CHE DEVE ESSERE AGGIUNTO AUTOMATICAMENTE
                                            If Not funzioni.esiste_young_driver_primo_guidatore(get_id_young_driver(), id_gruppo_auto_scelto.Text, numCalcolo.Text, "", idPrenotazione.Text, "", "") Then
                                                nuovo_accessorio(get_id_young_driver(), id_gruppo_auto_scelto.Text, "YOUNG PRIMO", test_eta, "", imposta_prepagato)

                                                listPrenotazioniCosti.DataBind()

                                                txtEtaPrimo.Text = test_eta
                                                Libreria.genUserMsgBox(Me, "Aggiunto supplemento Young Driver.")
                                            End If
                                        ElseIf check_eta = "4" Then
                                            'VENDIBILE SENZA LO YOUNG DRIVER - QUALORA FOSSE STATO PRECEDENTEMENTE AGGIUNTO PROVVEDO ALLA SUA RIMOZIONE
                                            txtEtaPrimo.Text = test_eta
                                            If funzioni.esiste_young_driver_primo_guidatore(get_id_young_driver(), id_gruppo_auto_scelto.Text, numCalcolo.Text, "", idPrenotazione.Text, "", "") Then
                                                funzioni.rimuovi_costo_accessorio("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, get_id_young_driver(), "1", "EXTRA", dropTipoCommissione.SelectedValue)
                                                listPrenotazioniCosti.DataBind()
                                                txtEtaPrimo.Text = test_eta
                                                Libreria.genUserMsgBox(Me, "Rimosso il costo dello Young Driver per il primo guidatore.")
                                            End If
                                        End If
                                    End If
                                End If

                                If check_eta <> "0" Then
                                    If Not is_gruppo_speciale(id_gruppo_auto_scelto.Text) Then
                                        'E' STATO VARIATO IL GRUPPO - NON E' PIU' SPECIALE - ERA STATA ASSEGNATA LA TARGA AL CONTRATTO
                                        If lblTargaSelezionata.Text <> "" Then
                                            rimuovi_targa()
                                            txtTarga.Text = ""
                                            txtTarga.ReadOnly = False
                                            btnAssegnaTarga.Text = "Assegna"
                                        End If
                                        tab_assegnazione_targa.Visible = False
                                    Else
                                        tab_assegnazione_targa.Visible = True
                                    End If

                                    If CDbl(txtSconto.Text) <> CDbl(lblScontoAttuale.Text) Then
                                        txtSconto.Text = lblScontoAttuale.Text
                                    End If

                                    If dropTipoSconto.SelectedValue <> lblTipoScontoAttuale.Text Then
                                        dropTipoSconto.SelectedValue = lblTipoScontoAttuale.Text
                                    End If

                                    salvaPrenotazione(imposta_prepagato)

                                    'SE SALVO DA QUA DENTRO CERTAMENTE LA PRENOTAZIONE HA ASSEGNATA UNA TARIFFA
                                    prenotazione_no_tariffa.Text = "0"

                                    lblNumeroVariazione.Text = "MODIFICA n. " & (CInt(numCalcolo.Text) - 1)
                                    lblNumeroVariazione.Visible = True

                                    lblPrenotazioneScaduta.Visible = False
                                    prenotazioneScaduta.Text = ""
                                    btnModificaConducente.Visible = True
                                    btnModificaDitta.Visible = True

                                    tipo_prenotazione.Text = "richiama"

                                    'If id_conducente.Text <> "" Then
                                    '    If tariffa_broker.Text <> "1" Then
                                    '        btnPagamento.Visible = True
                                    '    End If
                                    'Else
                                    '    btnPagamento.Visible = False
                                    'End If

                                    btnSalvaModifiche.Visible = False
                                    btnModificaDatiCliente.Visible = True
                                    btnStampa.Visible = True
                                    btnVediUltimoCalcolo.Visible = False

                                    Dim uscita As DateTime = funzioni_comuni.getDataDb_senza_orario2(txtDaData.Text)
                                    Dim oggi As DateTime = funzioni_comuni.getDataDb_senza_orario2(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()))

                                    If DateDiff(DateInterval.Day, oggi, uscita) = 0 And dropStazionePickUp.SelectedValue = Request.Cookies("SicilyRentCar")("stazione") Then
                                        btnContratto.Visible = True
                                    End If


                                    btnAssegnaTarga.Visible = True

                                    If tariffa_broker.Text = "1" Then
                                        btnInviaMail.Visible = False
                                        'btnPagamento.Visible = False
                                    Else
                                        btnInviaMail.Visible = True
                                        If Not riga_commissioni.Visible Then
                                            btnRicalcolaPrepagato.Visible = True
                                        End If

                                        'btnPagamento.Visible = True
                                    End If

                                Else
                                    Libreria.genUserMsgBox(Me, "Impossibile effettuare le modifiche: gruppo auto non vendibile a causa dell'età del guidatore.")
                                End If
                            Else
                                Libreria.genUserMsgBox(Me, "E' necessario cliccare su 'RICALCOLA' per poter salvare le modifiche.")
                            End If
                        Else
                            Libreria.genUserMsgBox(Me, "Attenzione: esiste un cliente con lo stesso nome e cognome che si trova in BLACK LIST. E' possibile comunque salvare la prenotazione, tuttavia non sarà possibile successivamente collegare questo cliente alla prenotazione e/o al contratto.")
                        End If
                    Else
                        Libreria.genUserMsgBox(Me, "Specificare almeno nome e cognome del conducente.")
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Attenzione: la prenotazione non è più modificabile in quanto è variato lo stato. E' necessario tornare alla maschera di ricerca e ricaricare la prenotazione.")
                End If

                '#aggiunto salvo 19.02.2023
                txtSconto.Text = sconto 'aggiunto salvo 19.02.2023
                If lblMxSconto.Text = "Nessuno sconto applicabile" Then
                    lblMxSconto.Visible = False
                End If
                '@end salvo 19.02.2023

            Else
                    lblRiferimentoEsistente.Visible = True
                lblRifToOld.Text = txtRiferimentoTO.Text
                Libreria.genUserMsgBox(Me, "Attenzione: esiste un'altra prenotazione con lo stesso numero di riferimento del TO. Cliccando nuovamente su SALVA la prenotazione verrà memorizzata ugualmente.")
            End If
        Catch ex As Exception

            HttpContext.Current.Response.Write("error prenotazioni salva_modifiche : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Protected Sub btnSalvaModifiche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvaModifiche.Click
        salva_modifiche(False)
    End Sub

    Protected Sub ricalcola_rimuovi_warning_per_stazione()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM prenotazioni_warning WHERE (tipo='DROP' OR tipo='PICK' OR tipo='PICK INFO' OR tipo='DROP INFO') AND id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        listWarningPickPrenotazioni.DataBind()
        listWarningDropPrenotazioni.DataBind()
    End Sub

    Protected Sub modifica_orario_prenotazione()
        'IN QUESTO CASO SI DEVE SOLAMENTE VARIARE L'ORARIO DI PICK-UP E DI DROP-OFF - I GIORNI DI NOLEGGIO NON SONO SICURAMENTE VARIATI
        'VENGONO ANCHE AGGIORNATI I DATI DEL CLIENTE CHE POTREBBERO ESSERE STATI VARIATI

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim conducente As String
        If id_conducente.Text = "" Then
            conducente = "NULL"
        Else
            conducente = "'" & id_conducente.Text & "'"
        End If

        Dim data_di_nascita As String = txtDataDiNascita.Text

        If data_di_nascita = "" Then
            data_di_nascita = "NULL"
        Else
            data_di_nascita = "'" & getDataDb_senza_orario(txtDataDiNascita.Text, Request.ServerVariables("HTTP_HOST")) & "'"
        End If

        Dim sqlStr As String = "UPDATE prenotazioni SET ore_uscita='" & ore1.Text & "', minuti_uscita='" & minuti1.Text & "', ore_rientro='" & ore2.Text & "', minuti_rientro='" & minuti2.Text & "', " &
        "id_conducente=" & conducente & ", nome_conducente='" & Replace(txtNomeConducente.Text, "'", "''") & "', " &
        " cognome_conducente='" & Replace(txtCognomeConducente.Text, "'", "''") & "', eta_primo_guidatore='" & txtEtaPrimo.Text & "', " &
        " eta_secondo_guidatore='" & txtEtaSecondo.Text & "', data_nascita=" & data_di_nascita & ", " &
        " mail_conducente='" & Replace(txtMailConducente.Text, "'", "''") & "', indirizzo_conducente='" & Replace(txtIndirizzoConducente.Text, "'", "''") & "', riferimento_telefono='" & Replace(txtRifTel.Text, "'", "''") & "'," &
        " N_VOLOOUT='" & Replace(txtVoloOut.Text, "'", "''") & "', N_VOLOPR='" & Replace(txtVoloPr.Text, "'", "''") & "' " &
        " WHERE Nr_Pren='" & idPrenotazione.Text & "'"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteScalar()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub aggiungi_warning_gruppo_auto(ByVal gruppo_scelto As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

        For i = 0 To listGruppi.Items.Count - 1
            Dim id_gruppo_lista As Label = listGruppi.Items(i).FindControl("id_gruppo")
            If id_gruppo_lista.Text = gruppo_scelto Then
                Dim pick As Label = listGruppi.Items(i).FindControl("pick")
                If pick.Visible Then
                    Cmd = New Data.SqlClient.SqlCommand("INSERT INTO prenotazioni_warning (id_documento, num_calcolo, warning, id_operatore, tipo) VALUES ('" & idPrenotazione.Text & "','" & numCalcolo.Text & "','" & Replace("GRUPPO - " & pick.Text, "'", "''") & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "','" & "GRUPPO" & "')", Dbc)
                    Cmd.ExecuteNonQuery()
                End If

                Dim drop As Label = listGruppi.Items(i).FindControl("drop")
                If drop.Visible Then
                    Cmd = New Data.SqlClient.SqlCommand("INSERT INTO prenotazioni_warning (id_documento, num_calcolo, warning, id_operatore, tipo) VALUES ('" & idPrenotazione.Text & "','" & numCalcolo.Text & "','" & Replace("GRUPPO - " & drop.Text, "'", "''") & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "','" & "GRUPPO" & "')", Dbc)
                    Cmd.ExecuteNonQuery()
                End If

                Dim val As Label = listGruppi.Items(i).FindControl("val")
                If val.Visible Then
                    Cmd = New Data.SqlClient.SqlCommand("INSERT INTO prenotazioni_warning (id_documento, num_calcolo, warning, id_operatore, tipo) VALUES ('" & idPrenotazione.Text & "','" & numCalcolo.Text & "','" & Replace("GRUPPO - " & val.Text, "'", "''") & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "','" & "GRUPPO" & "')", Dbc)
                    Cmd.ExecuteNonQuery()
                End If

                Dim stop_sale As Label = listGruppi.Items(i).FindControl("stop_sale")
                If stop_sale.Visible Then
                    Cmd = New Data.SqlClient.SqlCommand("INSERT INTO prenotazioni_warning (id_documento, num_calcolo, warning, id_operatore, tipo) VALUES ('" & idPrenotazione.Text & "','" & numCalcolo.Text & "','" & Replace("GRUPPO - " & stop_sale.Text, "'", "''") & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "','" & "GRUPPO" & "')", Dbc)
                    Cmd.ExecuteNonQuery()
                End If

                Dim stop_sale_fonte As Label = listGruppi.Items(i).FindControl("stop_sale_fonte")
                If stop_sale_fonte.Visible Then
                    Cmd = New Data.SqlClient.SqlCommand("INSERT INTO prenotazioni_warning (id_documento, num_calcolo, warning, id_operatore, tipo) VALUES ('" & idPrenotazione.Text & "','" & numCalcolo.Text & "','" & Replace("GRUPPO - " & stop_sale_fonte.Text, "'", "''") & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "','" & "GRUPPO" & "')", Dbc)
                    Cmd.ExecuteNonQuery()
                End If
            End If
        Next

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub listVecchioCalcolo_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listVecchioCalcolo.ItemDataBound
        Dim gruppo As Label = e.Item.FindControl("gruppo")
        Dim id_a_carico_di As Label = e.Item.FindControl("id_a_carico_di")
        Dim nome_costo As Label = e.Item.FindControl("nome_costo")
        Dim obbligatorio As Label = e.Item.FindControl("obbligatorio")
        Dim id_metodo_stampa As Label = e.Item.FindControl("id_metodo_stampa")
        Dim sconto As Label = e.Item.FindControl("lblSconto")
        Dim valore_costo As Label = e.Item.FindControl("valore_costoLabel")
        Dim costo_scontato As Label = e.Item.FindControl("costo_scontato")
        Dim omaggiabile As Label = e.Item.FindControl("omaggiabile")
        Dim prepagato As Label = e.Item.FindControl("prepagato")
        Dim omaggiato As CheckBox = e.Item.FindControl("chkOldOmaggio")
        Dim chkOmaggio As CheckBox = e.Item.FindControl("chkOmaggio")
        Dim chkScegli As CheckBox = e.Item.FindControl("chkScegli")
        Dim id_elemento As Label = e.Item.FindControl("id_elemento")

        If obbligatorio.Text = "False" And id_a_carico_di.Text <> Costanti.id_accessorio_incluso Then
            e.Item.FindControl("chkScegli").Visible = True

            'NEL CASO IN CUI L'ELEMENTO NON E' SELEZIONATO NON FACCIO VEDERE LA COLONNA SCONTO
        End If

        If prepagato.Text = "True" Then
            If LCase(nome_costo.Text) <> "valore tariffa" Then
                e.Item.FindControl("lblPrepagato").Visible = True
            End If
        End If

        If sconto.Text = "0,00" Then
            sconto.Text = ""
            valore_costo.Visible = False
        Else
            sconto.Text = sconto.Text & " €"
            sconto.Visible = True
        End If

        If Not chkScegli.Checked And chkScegli.Visible Then
            valore_costo.Visible = False
            sconto.Visible = False
            costo_scontato.Visible = False
        End If

        If omaggiato.Checked Then
            valore_costo.Text = "0,00 €"
            costo_scontato.Text = "0,00 €"
            sconto.Text = ""
        Else
            valore_costo.Text = valore_costo.Text
            costo_scontato.Text = costo_scontato.Text
        End If

        If LCase(nome_costo.Text) = "valore tariffa" Or LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Or LCase(nome_costo.Text) = "sconto" Then
            nome_costo.Font.Bold = True
        End If

        If id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore Then
            If id_a_carico_di.Text = Costanti.id_accessorio_incluso And LCase(nome_costo.Text) <> "valore tariffa" Then
                'PER GLI ELEMENTI INCLUSI TRANNE IL TEMPO KM NON MOSTRO IL PREZZO (CHE E' SEMPRE ZERO)
                e.Item.FindControl("lblIncluso").Visible = True
                valore_costo.Visible = False
                sconto.Visible = False
                costo_scontato.Visible = False
            ElseIf id_a_carico_di.Text <> Costanti.id_accessorio_incluso And obbligatorio.Text = "True" And LCase(nome_costo.Text) <> "valore tariffa" And LCase(nome_costo.Text) <> "sconto" Then
                e.Item.FindControl("lblObbligatorio").Visible = True
            End If
        Else
            e.Item.FindControl("lblInformativa").Visible = True
        End If

        If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
            Dim id_gruppoLabel As Label = e.Item.FindControl("id_gruppoLabel")
            Dim costo_broker As Double = getCostoACaricoDelBroker(CInt(numCalcolo.Text) - 1, id_gruppoLabel.Text)
            If CDbl(a_carico_del_broker_ultimo_calcolo.Text) < costo_broker Then
                costo_scontato.Text = CDbl(costo_scontato.Text) - CDbl(a_carico_del_broker_ultimo_calcolo.Text)
            Else
                costo_scontato.Text = CDbl(costo_scontato.Text) - costo_broker
            End If

            costo_scontato.Text = FormatNumber(costo_scontato.Text, 2, , , TriState.False)
            valore_costo.Visible = False
            costo_scontato.Font.Bold = True
            costo_scontato.ForeColor = Drawing.Color.Blue
            costo_scontato.Font.Size = 12
            e.Item.FindControl("lblObbligatorio").Visible = False
        End If

        If gruppo.Text = old_ultimo_gruppo.Text Then
            e.Item.FindControl("riga_gruppo").Visible = False
            e.Item.FindControl("riga_intestazione").Visible = False
        Else
            e.Item.FindControl("riga_gruppo").Visible = True
            e.Item.FindControl("riga_intestazione").Visible = True
            old_ultimo_gruppo.Text = gruppo.Text
        End If

        If omaggiabile.Text = "True" And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore Then
            chkOmaggio.Visible = True
            chkOmaggio.Enabled = False
        End If

        'TARIFFE BROKER: SE L'UTENTE NON HA IL PERMESSO VENGONO NASCOSTI TUTTI I PREZZI E VENGONO VISUALIZZATI SOLO GLI ACCESSORI (SENZA PREZZO)
        If tariffa_broker.Text = "1" Then
            'If livello_accesso_broker.Text <> "3" Then
            If id_elemento.Text = Costanti.ID_tempo_km Then
                sconto.Visible = False
                If CDbl(costo_scontato.Text) = CDbl(a_carico_del_broker_ultimo_calcolo.Text) Then
                    costo_scontato.Visible = False
                Else
                    If CDbl(costo_scontato.Text) >= CDbl(a_carico_del_broker_ultimo_calcolo.Text) Then
                        costo_scontato.Text = FormatNumber(CDbl(costo_scontato.Text) - CDbl(CDbl(a_carico_del_broker_ultimo_calcolo.Text)), 2, , , TriState.False)
                    Else
                        costo_scontato.Visible = False
                    End If
                End If
            End If


            'End If
            'If (obbligatorio.Text = "False" And id_a_carico_di.Text <> Costanti.id_accessorio_incluso) Or (LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale)) Then
            '    'PER GLI ELEMENTI A SCELTA (O PER LA RIGA TOTALE DOVE E' PRESENTE IL PULSANTE AGGIUNGI ACCESSORIO) NASCONDO I COSTI
            '    valore_costo.Visible = False
            '    sconto.Visible = False
            '    costo_scontato.Visible = False

            '    'If (LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale)) Then
            '    '    nome_costo.Text = ""
            '    'End If
            'Else
            '    e.Item.FindControl("riga_elementi").Visible = False
            'End If
        End If
    End Sub

    Protected Sub btnVediUltimoCalcolo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVediUltimoCalcolo.Click

        old_ultimo_gruppo.Text = ""
        listVecchioCalcolo.Visible = True
        listVecchioCalcolo.DataBind()

        btnVediUltimoCalcolo.Visible = False

        listPrenotazioniCosti.Focus()
    End Sub


    Protected Sub btnPagamento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPagamento.Click
        'tab_pagamento.Visible = True
        'tab_cerca_tariffe.Visible = False

        'Dim Dati As New tabelle_pos_Scambio_Importo.DatiInizializzazione()
        'Dati.IDStazione = Request.Cookies("SicilyRentCar")("stazione")
        'Dati.NumeroDocumento = txtNumPrenotazione.Text
        'Dati.TipoDocumento = tabelle_pos_Scambio_Importo.TipoDocumento.Prenotazione
        'Dati.ListaPreautorizzazioni = New System.Collections.Generic.List(Of tabelle_pos_Scambio_Importo.Preautorizzazione)

        ''RECUPERO EVENTUALI PREAUTORIZZAZIONI 

        'Dim preautorizzazioni(50) As String
        'preautorizzazioni = cPagamenti.getListPreautorizzazioni(txtNumPrenotazione.Text, "", "", "")

        'Dim i As Integer = 0

        'Dim pre As tabelle_pos_Scambio_Importo.Preautorizzazione
        'pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()

        'Do While preautorizzazioni(i) <> "0"
        '    pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()
        '    pre.Numero = preautorizzazioni(i)
        '    Dati.ListaPreautorizzazioni.Add(pre)
        '    i = i + 1
        'Loop

        'Dim costo_scontato As Label
        'Dim nome_costo As Label
        'Dim totale_da_prenotazione As Double = getImportoPrepagato()



        'Dati.Importo = totale_da_prenotazione


        'Dati.ImportoMassimoRimborsabile = 0   '<<<----- IMPOSTARE
        'Dati.importo_non_modificabile_vendita = True


        ''Dati.PreSelectIDEnte = 13
        ''Dati.PreSelectIDAcquireCircuito = 64
        ''Dati.PreSelectIDFunzione = Costanti.POS_FunzioneIntegrazione
        ''Dati.PreSelectPOSID = 16
        ''Dati.PreSelectNumeroPreautorizzazione = "363320898"

        'Dati.TestMode = True

        'Dati.TipoPagamentoContanti = FiltroTipoPagamentoContanti.Prenotazione

        'If tariffa_broker.Text = "1" Or complimentary.Text = "1" Then
        '    Dati.complimentary_abilitato = False
        'End If

        'If tariffa_broker.Text = "1" Then
        '    Dati.importo_non_modificabile_vendita = False
        'End If

        'If full_credit.Text = "1" Then
        '    Dati.full_credit_abilitato = False
        'End If

        'Scambio_Importo1.InizializzazioneDati(Dati)

        'Tony 02/08/2022
        pagamento_nuovo_contratto()
    End Sub

    'Tony 02/08/2022
    Protected Sub pagamento_nuovo_contratto()
        Session("carica_dati") = txtNumPrenotazione.Text
        Session("provenienza") = "Prenotazione"

        'If lblDifferenzaDaPrenotazione.Text & "" <> "" Then
        '    Session("DaPagare") = lblDifferenzaDaPrenotazione.Text
        'Else
        '    Session("DaPagare") = "0"
        'End If

        'If lblDaPreautorizzare.Text & "" <> "" Then
        '    Session("DaPreautorizzare") = lblDaPreautorizzare.Text
        'Else
        '    Session("DaPreautorizzare") = "0"
        'End If
        Session("DaPagare") = "0"
        Session("DaPreautorizzare") = "0"

        Response.Redirect("pagamenti.aspx")
    End Sub

    Protected Function check_rif_to() As Boolean
        If Trim(txtRiferimentoTO.Text) <> "" Then
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 Nr_Pren FROM prenotazioni WITH(NOLOCK) WHERE rif_to='" & Replace(txtRiferimentoTO.Text, "'", "''") & "' AND id_fonte='" & dropTipoCliente.SelectedValue & "' AND status<>'1' AND status<>'2' AND attiva='1' AND Nr_Pren<>'" & idPrenotazione.Text & "'", Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test = "" Then
                check_rif_to = True
            Else
                check_rif_to = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Else
            check_rif_to = True
        End If
    End Function

    Protected Function check_black_list() As Boolean
        'RESTITUISCE TRUE SE E' POSSIBILE SALVARE LA PRENOTAZIONE
        If id_conducente.Text <> "" Then
            'SE E' STATO SELEZIONATO UN UTENTE DA ANAGRAFICA QUESTO E' DI CERTO SELEZIONABILE (IL CONTROLLO E' BLOCCANTE)
            check_black_list = True
        ElseIf lblAvvisoBlackList.Text = txtCognomeConducente.Text & " " & txtNomeConducente.Text Then
            'L'AVVISO ERA GIA' STATO MOSTRATO - NOME E COGNOME NON SONO STATI VARIATI
            check_black_list = True
        Else
            'IN TUTTI GLI ALTRI CASI CONTROLLO CHE NON ESISTA IN ANAGRAFICA UN UTENTE (CONTROLLANDO NOME E COGNOME) CHE SIA IN BLACK LIST
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id_conducente FROM CONDUCENTI WITH(NOLOCK) WHERE cognome='" & Replace(txtCognomeConducente.Text, "'", "''") & "' AND nome='" & Replace(txtNomeConducente.Text, "'", "''") & "' AND black_list='1'", Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test = "" Then
                check_black_list = True
            Else
                check_black_list = False
            End If

            lblAvvisoBlackList.Text = txtCognomeConducente.Text & " " & txtNomeConducente.Text

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End If
    End Function

    Protected Sub modifica_dati_cliente()
        'PER PRIMA COSA, SE SPECIFICATO, VIENE CONTROLLATA L'UNICITA' DEL NUMERO DI RIFERIMENTO DEL TOUR OPERATOR
        Dim msg As String = ""

        Try
            If check_rif_to() Or lblRifToOld.Text = txtRiferimentoTO.Text Then

                lblRifToOld.Text = txtRiferimentoTO.Text
                lblRiferimentoEsistente.Visible = False
                If prenotazione_modificabile("0") Then

                    'MODIFICA UNICAMENTE I DATA ANAGRAFICI E DI CONTATTO DEL CLIENTE - QUALORA FOSSE CAMBIATO L'UTENTE E QUINDI L'ETA' SI AGGIUNGE O 
                    'RIMUOVE LO YOUNG DRIVER
                    If (Trim(txtCognomeConducente.Text) <> "") And (Trim(txtNomeConducente.Text) <> "") Then
                        'CONTROLLO DELLA BLACK LIST
                        If check_black_list() Then
                            Dim check_eta As String = ""

                            If txtDataDiNascita.Text <> "" Then
                                'SE E' STATA SPECIFICATA UNA DATA DI NASCITA (SIA MANUALMENTE CHE SELEZIONANDO UN UTENTE) E' NECESSARIO CONTROLLARE SE C'E' 
                                'VARIAZIONE SULL'ETA' 
                                Dim test_eta As Integer
                                Dim month_nascita As Integer = Month(txtDataDiNascita.Text)
                                Dim day_nascita As Integer = Day(txtDataDiNascita.Text)
                                Dim data_nascita As DateTime = getDataDb_senza_orario2(txtDataDiNascita.Text)

                                Dim data2 As DateTime
                                data2 = getDataDb_senza_orario2(txtDaData.Text)
                                test_eta = DateDiff(DateInterval.Year, data_nascita, data2)

                                If Month(Now()) < month_nascita Then
                                    test_eta = CInt(test_eta) - 1
                                ElseIf Month(Now()) = month_nascita And Day(Now()) < day_nascita Then
                                    test_eta = CInt(test_eta) - 1
                                End If

                                If CStr(test_eta) <> txtEtaPrimo.Text Then

                                    check_eta = funzioni_comuni.gruppo_vendibile_eta_guidatori(id_gruppo_auto_scelto.Text, test_eta, "", "", "", "", "", "", "", False)

                                    If check_eta = "0" Then
                                        'L'AUTO NON E' VENDIBILE - NON E' POSSIBILE COLLEGARE IL GUIDATORE ALLA PRENOTAZIONE DA SALVARE
                                        txtEtaPrimo.Text = test_eta
                                    ElseIf check_eta = "1" Then
                                        'IN QUESTO CASO IL GRUPPO AUTO E' VENDIBILE MA CON SUPPLEMENTO YOUNG DRIVER CHE DEVE ESSERE AGGIUNTO AUTOMATICAMENTE
                                        txtEtaPrimo.Text = test_eta
                                        If Not funzioni.esiste_young_driver_primo_guidatore(get_id_young_driver(), id_gruppo_auto_scelto.Text, numCalcolo.Text, "", idPrenotazione.Text, "", "") Then
                                            Dim imposta_prepagato As Boolean = False
                                            If ricalcolaPrepagato.Text = "1" Then
                                                imposta_prepagato = True
                                            End If
                                            nuovo_accessorio(get_id_young_driver(), id_gruppo_auto_scelto.Text, "YOUNG PRIMO", test_eta, "", imposta_prepagato)

                                            listPrenotazioniCosti.DataBind()
                                            msg = msg & "Aggiunto supplemento Young Driver." & vbCrLf

                                            aggiorna_commissioni_operatore()
                                        End If
                                    ElseIf check_eta = "4" Then
                                        'VENDIBILE SENZA LO YOUNG DRIVER - QUALORA FOSSE STATO PRECEDENTEMENTE AGGIUNTO PROVVEDO ALLA SUA RIMOZIONE
                                        txtEtaPrimo.Text = test_eta
                                        If funzioni.esiste_young_driver_primo_guidatore(get_id_young_driver(), id_gruppo_auto_scelto.Text, numCalcolo.Text, "", idPrenotazione.Text, "", "") Then
                                            funzioni.rimuovi_costo_accessorio("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppo_auto_scelto.Text, get_id_young_driver(), "1", "EXTRA", dropTipoCommissione.SelectedValue)
                                            listPrenotazioniCosti.DataBind()

                                            msg = msg & "Rimosso il costo dello Young Driver per il primo guidatore." & vbCrLf
                                        End If
                                    End If
                                End If
                            End If

                            If check_eta <> "0" Then
                                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                                Dbc.Open()

                                Dim conducente As String
                                Dim idDitta As String

                                If id_conducente.Text = "" Then
                                    conducente = "NULL"
                                Else
                                    conducente = "'" & id_conducente.Text & "'"
                                End If

                                Dim data_di_nascita As String = txtDataDiNascita.Text

                                If data_di_nascita = "" Then
                                    data_di_nascita = "NULL"
                                Else
                                    data_di_nascita = "'" & getDataDb_senza_orario(txtDataDiNascita.Text, Request.ServerVariables("HTTP_HOST")) & "'"
                                End If

                                If id_ditta.Text <> "" Then
                                    idDitta = "'" & id_ditta.Text & "'"
                                Else
                                    idDitta = "NULL"
                                End If

                                Dim id_gruppo_da_consegnare As String
                                If dropGruppoDaConsegnare.SelectedValue = "0" Then
                                    id_gruppo_da_consegnare = "NULL"
                                Else
                                    id_gruppo_da_consegnare = "'" & dropGruppoDaConsegnare.SelectedValue & "'"
                                End If

                                Dim sqlStr As String = "UPDATE prenotazioni SET id_conducente=" & conducente & ", nome_conducente='" & Replace(txtNomeConducente.Text, "'", "''") & "'," &
                                "cognome_conducente='" & Replace(txtCognomeConducente.Text, "'", "''") & "', eta_primo_guidatore='" & txtEtaPrimo.Text & "'," &
                                "data_nascita=convert(datetime," & data_di_nascita & ",102),id_gruppo_app=" & id_gruppo_da_consegnare & "," &
                                "mail_conducente='" & Replace(txtMailConducente.Text, "'", "''") & "',indirizzo_conducente='" & Replace(txtIndirizzoConducente.Text, "'", "''") & "', riferimento_telefono='" & Replace(txtRifTel.Text, "'", "''") & "'," &
                                "N_VOLOOUT='" & Replace(txtVoloOut.Text, "'", "''") & "', N_VOLOPR='" & Replace(txtVoloPr.Text, "'", "''") & "'," &
                                "rif_to='" & Replace(txtRiferimentoTO.Text, "'", "''") & "', id_cliente=" & idDitta & " WHERE Nr_Pren='" & idPrenotazione.Text & "'"

                                Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                                Cmd.ExecuteScalar()

                                Cmd.Dispose()
                                Cmd = Nothing
                                Dbc.Close()
                                Dbc.Dispose()
                                Dbc = Nothing
                                msg = msg & "Modifica effettuata correttamente." & vbCrLf
                                Libreria.genUserMsgBox(Me, msg)

                                Dim uscita As DateTime = funzioni_comuni.getDataDb_senza_orario2(txtDaData.Text)
                                Dim oggi As DateTime = funzioni_comuni.getDataDb_senza_orario2(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()))

                                If DateDiff(DateInterval.Day, oggi, uscita) = 0 And dropStazionePickUp.SelectedValue = Request.Cookies("SicilyRentCar")("stazione") Then
                                    'UNA PRENOTAZIONE ATTIVA PUO' DIVENTARE CONTRATTO SOLO SE E' DEL GIORNO ODIERNO
                                    btnContratto.Visible = True
                                End If

                                anagrafica_conducenti.Visible = False

                                'If id_conducente.Text <> "" Then
                                '    btnPagamento.Visible = True
                                'Else
                                '    btnPagamento.Visible = False
                                'End If

                            Else
                                Libreria.genUserMsgBox(Me, "Impossibile effettuare le modifiche: gruppo auto non vendibile a causa dell'età del guidatore.")
                            End If
                        Else
                            Libreria.genUserMsgBox(Me, "Attenzione: esiste un cliente con lo stesso nome e cognome che si trova in BLACK LIST. E' possibile comunque salvare la prenotazione, tuttavia non sarà possibile successivamente collegare questo cliente alla prenotazione e/o al contratto.")
                        End If
                    Else
                        Libreria.genUserMsgBox(Me, "Specificare almeno nome e cognome del cliente.")
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Attenzione: la prenotazione non è più modificabile in quanto è variato lo stato. E' necessario tornare alla maschera di ricerca e ricaricare la prenotazione.")
                End If
            Else
                lblRiferimentoEsistente.Visible = True
                lblRifToOld.Text = txtRiferimentoTO.Text
                Libreria.genUserMsgBox(Me, "Attenzione: esiste un'altra prenotazione con lo stesso numero di riferimento del TO. Cliccando nuovamente su SALVA la prenotazione verrà memorizzata ugualmente.")
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error  ModificaDatiCliente : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try





    End Sub

    Protected Sub btnModificaDatiCliente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaDatiCliente.Click
        Try
            modifica_dati_cliente()
        Catch ex As Exception
            HttpContext.Current.Response.Write("error  btnModificaDatiCliente_Click : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Sub listPagamenti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listPagamenti.ItemDataBound
        Dim preaut_aperta As Label = e.Item.FindControl("preaut_aperta")
        Dim operazione_stornata As Label = e.Item.FindControl("operazione_stornata")
        Dim lblStato As Label = e.Item.FindControl("lblStato")
        Dim id_pos_funzioni_ares As Label = e.Item.FindControl("id_pos_funzioni_ares")
        Dim btnVedi As ImageButton = e.Item.FindControl("vedi")
        Dim id_tippag As Label = e.Item.FindControl("ID_TIPPAG")
        Dim lb_Des_ID_ModPag As Label = e.Item.FindControl("lb_Des_ID_ModPag")
        Dim pagamento_broker As Label = e.Item.FindControl("pagamento_broker")

        If id_tippag.Text = "1011098650" And lb_Des_ID_ModPag.Text = "" Then
            lb_Des_ID_ModPag.Text = "C.CREDITO"
        End If

        If (operazione_stornata.Text & "") = "True" Then
            lblStato.Text = "STORNATA"
        Else
            If id_pos_funzioni_ares.Text = enum_tipo_pagamento_ares.Richiesta Then
                If (preaut_aperta.Text & "") = "True" Then
                    lblStato.Text = "APERTA"
                Else
                    lblStato.Text = "CHIUSA"
                End If
            End If
        End If

        If pagamento_broker.Text = "False" Then
            pagamento_broker.Text = "Cliente"
        Else
            pagamento_broker.Text = "Broker"
        End If

        If livello_accesso_dettaglio_pos.Text = "1" Then
            btnVedi.Visible = False
        End If
    End Sub


    Protected Sub listPagamenti_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listPagamenti.ItemCommand
        If e.CommandName = "vedi" Then
            riga_pagamento_pos.Visible = True

            Dim id_pagamento_extra As Label = e.Item.FindControl("ID_CTRLabel")
            Dim funzione As Label = e.Item.FindControl("funzione")
            Dim DATA_OPERAZIONELabel As Label = e.Item.FindControl("DATA_OPERAZIONELabel")
            Dim lblStato As Label = e.Item.FindControl("lblStato")
            Dim pagamento_broker As Label = e.Item.FindControl("pagamento_broker")

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT per_importo as importo, fatture.anno_fattura,codice_fattura, stazioni.codice,  * FROM PAGAMENTI_EXTRA WITH(NOLOCK) LEFT JOIN stazioni WITH(NOLOCK) ON pagamenti_extra.id_stazione=stazioni.id LEFT JOIN fatture WITH(NOLOCK) ON PAGAMENTI_EXTRA.Nr_Contratto=fatture.id_pagamento WHERE ID_CTR='" & id_pagamento_extra.Text & "' AND ISNULL(tipo_fattura,'" & TipoFattura.Prenotazione & "')='" & TipoFattura.Prenotazione & "'", Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            Rs.Read()

            If Rs.HasRows() Then
                idPagamentoExtra.Text = id_pagamento_extra.Text
                txtPOS_Funzione.Text = funzione.Text
                txtPOS_Stazione.Text = Rs("codice") & ""
                txtPOS_Cassa.Text = Rs("CASSA") & ""


                If (Rs("titolo") & "") <> "" Then
                    txtPOS_Carta.Text = "XXXX XXXX XXXX XXXX"
                    btnVisualizzaCC.Visible = True
                    lblPasswordCC.Visible = True
                    txtPasswordCC.Visible = True
                Else
                    btnVisualizzaCC.Visible = False
                    lblPasswordCC.Visible = False
                    txtPasswordCC.Visible = False
                    txtPOS_Carta.Text = ""
                End If

                txtPOS_Intestatario.Text = Rs("intestatario") & ""
                txtPOS_Scadenza.Text = Rs("scadenza") & ""

                txtPOS_Operatore.Text = funzioni_comuni.getNomeOperatore(Rs("id_operatore_ares") & "")
                txtPOS_DataOperazione.Text = DATA_OPERAZIONELabel.Text
                txtPOS_TerminalID.Text = Rs("TERMINAL_ID") & ""

                txtPOS_NrPreaut.Text = Rs("NR_PREAUT") & ""
                txtPOS_ScadenzaPreaut.Text = Rs("scadenza_preaut") & ""
                txtPOS_Stato.Text = lblStato.Text

                'inserito per modifica importo pagamento 27.04.2022
                If Not IsDBNull(Rs!importo) Then
                    txt_importo.Text = FormatNumber(Rs!importo, 2)
                Else
                    txt_importo.Text = ""
                End If


                txtPOS_Note.Text = Rs("note") & ""
                txtPOS_Note.Focus()

                If livello_accesso_eliminare_pagamenti.Text = 3 Then
                    btnIncassoWeb.Visible = True
                    btnModificaDataPagamento.Visible = True
                    txtPOS_DataOperazione.ReadOnly = False
                    btnEliminaPagamento.Visible = True
                    btnAzzeraPagamento.Visible = True

                    txt_importo.ReadOnly = False    '27.04.2022 inserito campo importo



                    If txtNumeroGiorniTO.Visible And pagamento_broker.Text = "Cliente" Then
                        btnIncassoBroker.Visible = True
                        btnIncassoWeb.Visible = False
                    ElseIf txtNumeroGiorniTO.Visible Then
                        btnIncassoBroker.Visible = False
                        btnIncassoWeb.Visible = False
                    Else
                        btnIncassoBroker.Visible = False
                    End If
                Else
                    btnIncassoWeb.Visible = False
                    btnModificaDataPagamento.Visible = False
                    btnEliminaPagamento.Visible = False
                    btnAzzeraPagamento.Visible = False
                    txtPOS_DataOperazione.Enabled = True
                    btnIncassoBroker.Visible = False
                End If

            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End If
    End Sub

    Protected Sub btnAnnulla7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla7.Click
        riga_pagamento_pos.Visible = False
    End Sub

    Protected Sub annulla_prenotazione()
        'SE LA PRENOTAZIONE E' PREPAGATA SI SETTA LO STATO AD 1 (ANNULLATA PREPAGATA) ALTRIMENTI SI SETTA LO STATO A 2
        'A 2 (ANNULLATA)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim stato As String = ""

        If lblPrepagato.Visible Then
            stato = "1"
        Else
            stato = "2"
        End If

        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE prenotazioni SET status='" & stato & "', id_motivo_annullamento='" & dropMotivoAnnullamento.SelectedValue & "', data_annullamento=GetDate(),data_ripristino=NULL,id_operatore_ripristino=NULL, id_operatore_annullamento='" & Request.Cookies("SicilyRentCar")("idUtente") & "' WHERE Nr_Pren='" & idPrenotazione.Text & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Libreria.genUserMsgBox(Me, "Prenotazione annullata correttamente")

        statoPrenotazione.Text = stato

        btnModificaConducente.Visible = False
        'btnPagamento.Visible = False
        btnSalvaModifiche.Visible = False
        btnModificaDatiCliente.Visible = False
        btnModificaPrenotazione.Visible = False
        btnAnnullaPrenotazione.Visible = False
        btnAggiungiExtra.Visible = False
        btnContratto.Visible = False
        btnRicalcola.Visible = False
        btnRicalcolaPrepagato.Visible = False
        btnAssegnaTarga.Visible = False

        txtoraPartenza.Enabled = False
        txtOraRientro.Enabled = False

        dropMotivoAnnullamento.Enabled = False

        lblPrenotazioneAnnullata.Visible = True
        lblRichiestaAnnullamento.Visible = False

        If livello_accesso_annulla_ripristina.Text = "3" Then
            btnRipristinaPrenotazione.Visible = True
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    'Protected Function prenotazione_modificabile() As Boolean
    '    'PER ESSERE MODIFICABILE IL RECORD ATTUALE DEVE ESSERE ATTIVO E LO STATO 0 (ATTIVA)
    '    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '    Dbc.Open()
    '    Dim Cmd As New Data.SqlClient.SqlCommand("SELECT Nr_Pren FROM prenotazioni WHERE status='0' AND attiva='1' AND Nr_Pren='" & idPrenotazione.Text & "'", Dbc)

    '    Dim test As String = Cmd.ExecuteScalar & ""

    '    If test <> "" Then
    '        prenotazione_modificabile = True
    '    Else
    '        prenotazione_modificabile = False
    '    End If

    '    Cmd.Dispose()
    '    Cmd = Nothing
    '    Dbc.Close()
    '    Dbc.Dispose()
    '    Dbc = Nothing
    'End Function

    Protected Sub btnAnnullaPrenotazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnullaPrenotazione.Click
        If prenotazione_modificabile("0") Then
            If dropMotivoAnnullamento.SelectedValue <> "0" Then
                annulla_prenotazione()
            Else
                Libreria.genUserMsgBox(Me, "Selezionare il motivo dell'annullamento.")
            End If
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: la prenotazione non è più modificabile in quanto è variato lo stato. E' necessario tornare alla maschera di ricerca e ricaricare la prenotazione.")
        End If
    End Sub

    Protected Function getGruppoAutoSalvato() As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ID_GRUPPO_ORIGINALE_PREPAGATO FROM prenotazioni WITH(NOLOCK) WHERE Nr_Pren='" & idPrenotazione.Text & "'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test = "" Then
            test = id_gruppo_auto_scelto.Text
        End If

        getGruppoAutoSalvato = test

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function



    Protected Sub ricalcola()

        Dim imposta_prepagato As Boolean = False


        'Try

        '    'ricalcola diretto 25.02.2021
        '    Dim pick_up As String = funzioni_comuni.stazione_aperta_pick_up(dropStazionePickUp.SelectedValue, txtDaData.Text, Hour(txtoraPartenza.Text), Minute(txtoraPartenza.Text), "", "", "", "", "", Request.Cookies("SicilyRentCar")("idUtente"))
        '    Dim drop_off As String = funzioni_comuni.stazione_aperta_drop_off(dropStazioneDropOff.SelectedValue, txtAData.Text, Hour(txtOraRientro.Text), Minute(txtOraRientro.Text), "", "", "", "", "", Request.Cookies("SicilyRentCar")("idUtente"))

        '    'If pick_up = "2" And drop_off = "2" Then
        '    '2) SE ENTRAMBE LE STAZIONI SONO APERTE CONTROLLO SE C'E' UNA VARIAZIONE NEI GIORNI DI NOLEGGIO. SE NON C'E' E' POSSIBILE SALVARE
        '    'LA PRENOTAZIONE VARIANDO UNICAMENTE L'ORARIO IN QUANTO I COSTI NON CAMBIANO (NON E' NEMMENO NECESSARIO CONTROLLARE GLI STOP SELL
        '    'IN QUANTO LA PRENOTAZIONE E' GIA' ATTIVA E IL GRUPPO ERA GIA' STATO VENDUTO)

        '    Dim id_tariffe_righe As String

        '    If dropTariffeGeneriche.SelectedValue <> "0" Then
        '        id_tariffe_righe = dropTariffeGeneriche.SelectedValue
        '    ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
        '        id_tariffe_righe = dropTariffeParticolari.SelectedValue
        '    End If

        '    Dim giorni_noleggio As Integer = funzioni_comuni.getGiorniDiNoleggio(txtDaData.Text, txtAData.Text, Hour(txtoraPartenza.Text), Minute(txtoraPartenza.Text), Hour(txtOraRientro.Text), Minute(txtOraRientro.Text), id_tariffe_righe, False)

        '    ore1.Text = Hour(txtoraPartenza.Text)
        '    minuti1.Text = Minute(txtoraPartenza.Text)

        '    ore2.Text = Hour(txtOraRientro.Text)
        '    minuti2.Text = Minute(txtOraRientro.Text)

        '    If Len(ore1.Text) = 1 Then
        '        ore1.Text = "0" & ore1.Text
        '    End If
        '    If Len(ore2.Text) = 1 Then
        '        ore2.Text = "0" & ore2.Text
        '    End If
        '    If Len(minuti1.Text) = 1 Then
        '        minuti1.Text = "0" & minuti1.Text
        '    End If
        '    If Len(minuti2.Text) = 1 Then
        '        minuti2.Text = "0" & minuti2.Text
        '    End If

        '    txtOldDaData.Text = txtDaData.Text
        '    txtOldAData.Text = txtAData.Text
        '    OldStazioneDropOff.Text = dropStazioneDropOff.SelectedValue
        '    OldStazionePickUp.Text = dropStazionePickUp.SelectedValue

        '    'SE LA PRENOTAZIONE E' PREPAGATA CONTROLLO CHE NON SIAMO IN PRESENZA DI UN DOWNSELL
        '    Dim gruppoModificabileUpsell_oppure_gruppo_non_modificato As Boolean = True

        '    If txtGiorniPrepagati.Visible And Not imposta_prepagato Then
        '        If gruppoDaCalcolare.SelectedValue <> id_gruppo_auto_scelto.Text Then
        '            'QUESTA PROCEDURA VALE SOLO PER LE PREPAGATE ESTESE CON PAGAMENTO AL BANCO. PER LE PREPAGATE SI DEVE SEMPRE USARE LA RACK (QUINDI TARIFFA VENDIBILE E' SEMPRE FALSE)
        '            gruppoModificabileUpsell_oppure_gruppo_non_modificato = funzioni_comuni.check_upsell(id_tariffe_righe, CInt(gruppoDaCalcolare.SelectedValue), getGruppoAutoSalvato(), giorni_noleggio, False, "", idPrenotazione.Text)
        '        End If
        '    End If

        '    If gruppoModificabileUpsell_oppure_gruppo_non_modificato Then
        '        'CONTROLLO CHE E' NON VI SIANO VINCOLI DI MINIMO E MASSIMO GIORNI DI NOLEGGIO DA RISPETTARE
        '        Dim min_giorni_nolo As String = funzioni_comuni.getMinGiorniNolo(id_tariffe_righe)
        '        Dim max_giorni_nolo As String = funzioni_comuni.getMaxGiorniNolo(id_tariffe_righe)

        '        If (min_giorni_nolo = "-1" And max_giorni_nolo = "-1") Or (min_giorni_nolo = "-1" And CInt(giorni_noleggio) <= CInt(max_giorni_nolo) And max_giorni_nolo <> "-1") Or (CInt(min_giorni_nolo) <= CInt(giorni_noleggio) And max_giorni_nolo = "-1" And min_giorni_nolo <> "-1") Or (CInt(min_giorni_nolo) <= CInt(giorni_noleggio) And CInt(max_giorni_nolo) >= CInt(giorni_noleggio) And min_giorni_nolo <> "-1" And max_giorni_nolo <> "-1") Then
        '            lblScontoAttuale.Text = txtSconto.Text
        '            lblTipoScontoAttuale.Text = dropTipoSconto.SelectedValue

        '            If gruppoDaCalcolare.Visible Then
        '                If id_gruppo_auto_scelto.Text <> gruppoDaCalcolare.SelectedValue Then
        '                    dropGruppoDaConsegnare.SelectedValue = gruppoDaCalcolare.SelectedValue
        '                End If

        '                id_gruppo_auto_scelto.Text = gruppoDaCalcolare.SelectedValue
        '            End If

        '            esegui_ricalcolo_giorni_modificati(imposta_prepagato)
        '            If imposta_prepagato Then
        '                Libreria.genUserMsgBox(Page, "E' possibile procedendo con il prepagamento cliccando su 'Pagamento'.")
        '            Else
        '                Libreria.genUserMsgBox(Page, "E' possibile completare la modifica cliccando su salva.")
        '            End If


        '            btnModificaDatiCliente.Visible = False
        '            btnStampa.Visible = False
        '            btnContratto.Visible = False
        '            'btnPagamento.Visible = False
        '            btnInviaMail.Visible = False
        '            btnAssegnaTarga.Visible = False

        '            If imposta_prepagato Then
        '                'IN QUESTO CASO E' POSSIBILE SOLAMENTE RICALCOLARE NUOVAMENTE OPPURE PAGARE PER SALVARE
        '                btnRicalcola.Visible = False
        '                btnAnnullaPrenotazione.Visible = False
        '                btnPagamento.Visible = True
        '            Else
        '                btnSalvaModifiche.Enabled = True
        '                btnSalvaModifiche.Visible = True
        '            End If
        '        Else
        '            Dim msg As String = "Attenzione: i giorni di noleggio sono " & giorni_noleggio & "; la tariffa scelta prevede"
        '            If min_giorni_nolo <> "-1" Then
        '                msg = msg & " minimo " & min_giorni_nolo & " giorni/o di nolo - "
        '            End If
        '            If max_giorni_nolo <> "-1" Then
        '                msg = msg & " massimo " & max_giorni_nolo & " giorni/o di nolo "
        '            End If

        '            Libreria.genUserMsgBox(Page, msg)

        '            btnSalvaModifiche.Enabled = False
        '        End If

        'Catch ex As Exception
        '    MsgBox = "errore ricalcola"
        '    Libreria.genUserMsgBox(Page, msg)
        'End Try


    End Sub


    Protected Sub ricalcola_non_broker(ByVal imposta_prepagato As Boolean)
        Dim data_prenotazione As DateTime = funzioni_comuni.getDataDb_senza_orario2(txtDaData.Text)

        If DateDiff(DateInterval.Day, Now(), data_prenotazione) < 0 Then
            Libreria.genUserMsgBox(Me, "Attenzione: la data di pick up deve essere almeno la data odierna.")
        Else


            If prenotazione_modificabile("0") Then


                'QUANDO E' VISIBILE QUESTO PULSANTE E' POSSIBILE MODIFICARE UNICAMENTE ORARIO DI PICK-UP E/O DI DROP OFF E GRUPPO -------------
                '1) PER PRIMA COSA SI CONTROLLA SE LA STAZIONE E' APERTA PER I NUOVI ORARI. SE PER CASO NON E' APERTA NON E' POSSIBILE PROCEDERE CON
                'LE MODIFICHE RICHIESTE
                Dim pick_up As String = funzioni_comuni.stazione_aperta_pick_up(dropStazionePickUp.SelectedValue, txtDaData.Text, Hour(txtoraPartenza.Text), Minute(txtoraPartenza.Text), "", "", "", "", "", Request.Cookies("SicilyRentCar")("idUtente"))
                Dim drop_off As String = funzioni_comuni.stazione_aperta_drop_off(dropStazioneDropOff.SelectedValue, txtAData.Text, Hour(txtOraRientro.Text), Minute(txtOraRientro.Text), "", "", "", "", "", Request.Cookies("SicilyRentCar")("idUtente"))

                'If pick_up = "2" And drop_off = "2" Then
                '2) SE ENTRAMBE LE STAZIONI SONO APERTE CONTROLLO SE C'E' UNA VARIAZIONE NEI GIORNI DI NOLEGGIO. SE NON C'E' E' POSSIBILE SALVARE
                'LA PRENOTAZIONE VARIANDO UNICAMENTE L'ORARIO IN QUANTO I COSTI NON CAMBIANO (NON E' NEMMENO NECESSARIO CONTROLLARE GLI STOP SELL
                'IN QUANTO LA PRENOTAZIONE E' GIA' ATTIVA E IL GRUPPO ERA GIA' STATO VENDUTO)

                Dim id_tariffe_righe As String

                If dropTariffeGeneriche.SelectedValue <> "0" Then
                    id_tariffe_righe = dropTariffeGeneriche.SelectedValue
                ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                    id_tariffe_righe = dropTariffeParticolari.SelectedValue
                End If

                Dim giorni_noleggio As Integer = funzioni_comuni.getGiorniDiNoleggio(txtDaData.Text, txtAData.Text, Hour(txtoraPartenza.Text), Minute(txtoraPartenza.Text), Hour(txtOraRientro.Text), Minute(txtOraRientro.Text), id_tariffe_righe, False)

                ore1.Text = Hour(txtoraPartenza.Text)
                minuti1.Text = Minute(txtoraPartenza.Text)

                ore2.Text = Hour(txtOraRientro.Text)
                minuti2.Text = Minute(txtOraRientro.Text)

                If Len(ore1.Text) = 1 Then
                    ore1.Text = "0" & ore1.Text
                End If
                If Len(ore2.Text) = 1 Then
                    ore2.Text = "0" & ore2.Text
                End If
                If Len(minuti1.Text) = 1 Then
                    minuti1.Text = "0" & minuti1.Text
                End If
                If Len(minuti2.Text) = 1 Then
                    minuti2.Text = "0" & minuti2.Text
                End If

                txtOldDaData.Text = txtDaData.Text
                txtOldAData.Text = txtAData.Text
                OldStazioneDropOff.Text = dropStazioneDropOff.SelectedValue
                OldStazionePickUp.Text = dropStazionePickUp.SelectedValue

                'SE LA PRENOTAZIONE E' PREPAGATA CONTROLLO CHE NON SIAMO IN PRESENZA DI UN DOWNSELL
                Dim gruppoModificabileUpsell_oppure_gruppo_non_modificato As Boolean = True

                If txtGiorniPrepagati.Visible And Not imposta_prepagato Then
                    If gruppoDaCalcolare.SelectedValue <> id_gruppo_auto_scelto.Text Then
                        'QUESTA PROCEDURA VALE SOLO PER LE PREPAGATE ESTESE CON PAGAMENTO AL BANCO. PER LE PREPAGATE SI DEVE SEMPRE USARE LA RACK (QUINDI TARIFFA VENDIBILE E' SEMPRE FALSE)
                        gruppoModificabileUpsell_oppure_gruppo_non_modificato = funzioni_comuni.check_upsell(id_tariffe_righe, CInt(gruppoDaCalcolare.SelectedValue), getGruppoAutoSalvato(), giorni_noleggio, False, "", idPrenotazione.Text)
                    End If
                End If

                If gruppoModificabileUpsell_oppure_gruppo_non_modificato Then
                    'CONTROLLO CHE E' NON VI SIANO VINCOLI DI MINIMO E MASSIMO GIORNI DI NOLEGGIO DA RISPETTARE
                    Dim min_giorni_nolo As String = funzioni_comuni.getMinGiorniNolo(id_tariffe_righe)
                    Dim max_giorni_nolo As String = funzioni_comuni.getMaxGiorniNolo(id_tariffe_righe)

                    If (min_giorni_nolo = "-1" And max_giorni_nolo = "-1") Or (min_giorni_nolo = "-1" And CInt(giorni_noleggio) <= CInt(max_giorni_nolo) And max_giorni_nolo <> "-1") Or (CInt(min_giorni_nolo) <= CInt(giorni_noleggio) And max_giorni_nolo = "-1" And min_giorni_nolo <> "-1") Or (CInt(min_giorni_nolo) <= CInt(giorni_noleggio) And CInt(max_giorni_nolo) >= CInt(giorni_noleggio) And min_giorni_nolo <> "-1" And max_giorni_nolo <> "-1") Then
                        lblScontoAttuale.Text = txtSconto.Text
                        lblTipoScontoAttuale.Text = dropTipoSconto.SelectedValue

                        If gruppoDaCalcolare.Visible Then
                            If id_gruppo_auto_scelto.Text <> gruppoDaCalcolare.SelectedValue Then
                                dropGruppoDaConsegnare.SelectedValue = gruppoDaCalcolare.SelectedValue
                            End If

                            id_gruppo_auto_scelto.Text = gruppoDaCalcolare.SelectedValue
                        End If

                        esegui_ricalcolo_giorni_modificati(imposta_prepagato)


                        'assegna valore deposito cauzionale 28.01.2022
                        'NON Serve qui perchè è inserito nel btnRicalcolaPrepagato
                        'Dim deposito_cauzionale As String = ""      '26.01.2022
                        'Dim cf As String = ""                       '26.01.2022
                        'Dim idgruppo As String = id_gruppo_auto_scelto.Text      '28.01.2022 

                        ''recupera valore originario del deposito cauzionale 26.01.22
                        'Dim impDepDefault As String = GetValoreDepositoCauzionaleDefault(idgruppo)
                        'Dim impDepCalcolato As String = impDepDefault
                        ''se Ambedue RIDUZIONI ATTIVE e PPLUS disabilitate riduce valore deposito cauzionale 
                        '' ma senza PPLUS Attiva
                        'If VerificaOpzione(listPrenotazioniCosti, "248", "ck") = "False" And VerificaOpzione(listPrenotazioniCosti, "100", "ck") = "True" _
                        '        And VerificaOpzione(listPrenotazioniCosti, "170", "ck") = "True" And VerificaOpzione(listPrenotazioniCosti, "223", "ck") = "False" Then
                        '    impDepCalcolato = "300"
                        '    funzioni_comuni.aggiorna_deposito_cauzionale("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
                        'End If
                        'end - NON Serve qui perchè è inserito nel btnRicalcolaPrepagato




                        If imposta_prepagato Then
                            Libreria.genUserMsgBox(Page, "E' possibile procedendo con il prepagamento cliccando su 'Pagamento'.")
                        Else

                            'nn fa visualizzare il msg 17.12.2021 nel caso di click su aggiorna
                            'e salvare direttamente 
                            If Session("ricalcola_prenotazione_salva") = "1" Then
                                'salva_modifiche(False)
                                'Session("ricalcola_prenotazione_salva") = ""    'reset session su Aggiorna Prenotazione 17.12.2021
                            Else
                                Libreria.genUserMsgBox(Page, "E' possibile completare la modifica cliccando su salva.")
                            End If

                        End If


                        btnModificaDatiCliente.Visible = False
                        btnStampa.Visible = False
                        btnContratto.Visible = False
                        'btnPagamento.Visible = False
                        btnInviaMail.Visible = False
                        btnAssegnaTarga.Visible = False

                        If imposta_prepagato Then
                            'IN QUESTO CASO E' POSSIBILE SOLAMENTE RICALCOLARE NUOVAMENTE OPPURE PAGARE PER SALVARE
                            btnRicalcola.Visible = False
                            btnAnnullaPrenotazione.Visible = False
                            btnPagamento.Visible = True
                        Else
                            btnSalvaModifiche.Enabled = True
                            btnSalvaModifiche.Visible = True
                        End If
                    Else
                        Dim msg As String = "Attenzione: i giorni di noleggio sono " & giorni_noleggio & "; la tariffa scelta prevede"
                        If min_giorni_nolo <> "-1" Then
                            msg = msg & " minimo " & min_giorni_nolo & " giorni/o di nolo - "
                        End If
                        If max_giorni_nolo <> "-1" Then
                            msg = msg & " massimo " & max_giorni_nolo & " giorni/o di nolo "
                        End If

                        Libreria.genUserMsgBox(Page, msg)

                        btnSalvaModifiche.Enabled = False
                    End If
                    'ElseIf pick_up <> "2" And drop_off = "2" Then
                    '    Libreria.genUserMsgBox(Me, "Stazione di pick up chiusa - impossibile modificare la prenotazione.")
                    '    txtoraPartenza.Text = ore1.Text & ":" & minuti1.Text
                    'ElseIf pick_up = "2" And drop_off <> "2" Then
                    '    Libreria.genUserMsgBox(Me, "Stazione di drop off chiusa - impossibile modificare la prenotazione.")
                    '    txtOraRientro.Text = ore2.Text & ":" & minuti2.Text
                    'ElseIf pick_up <> "2" And drop_off <> "2" Then
                    '    Libreria.genUserMsgBox(Me, "Stazione di di pick up e di drop off chiuse - impossibile modificare la prenotazione.")
                    '    txtoraPartenza.Text = ore1.Text & ":" & minuti1.Text
                    '    txtOraRientro.Text = ore2.Text & ":" & minuti2.Text
                    'End If
                Else
                    Libreria.genUserMsgBox(Me, "Impossibile modificare la prenotazione: downsell non permesso.")
                End If

            Else
                Libreria.genUserMsgBox(Me, "Attenzione: la prenotazione non è più modificabile in quanto è variato lo stato. E' necessario tornare alla maschera di ricerca e ricaricare la prenotazione.")
            End If
        End If

    End Sub

    Protected Sub btnRicalcola_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRicalcola.Click

        'aggiunto salvo 20.02.2023
        If txtDaData.Text <> "" And txtAData.Text <> "" Then
            If DateDiff("d", CDate(txtDaData.Text), CDate(txtAData.Text)) < 0 Then
                Libreria.genUserMsgBox(Page, "La data di Drop Off non può essere minore della data di Pick Up")
                Exit Sub
            End If
        End If

        ''# Salvo aggiunto 25.02.2023
        ''dati per calcolo tempoKm sono uguali recupera valore tariffa per 
        ''registrarlo successivamente
        'If (dropStazionePickUp.SelectedValue = lbl_StazionePickUp_OLD.Text) And (CDate(txtDaData.Text) = CDate(lbl_DaData_Old.Text)) And (txtoraPartenza.Text = lbl_DaOre_Old.Text) And (dropStazioneDropOff.SelectedValue = lbl_stazioneDropOff_OLD.Text) And (CDate(txtAData.Text) = CDate(lbl_AData_old.Text)) And (txtOraRientro.Text = lbl_AOre_old.Text) And (txtNumeroGiorni.Text = lbl_numeroGiorni_old.Text) Then
        '    'dati non sono cambiati recupera valore tariffa e totale del calcolo corrente
        '    lbl_valore_tariffa_ori.Text = funzioni_comuni_new.GetUltimoValoreTariffaValidoPren(idPrenotazione.Text, numCalcolo.Text)

        'End If
        ''@end salvo


        'Ricalcola
        ricalcola_non_broker(False)

        '# aggiunto salvo 20.02.2023 se sconto registra importo su record SCONTO
        If txtSconto.Text <> "0" Then
            Dim Valore_Originale_Tariffa As String = HttpContext.Current.Session("valore_preventivo_tariffa").ToString
            Dim valore_scontato_tariffa As String = HttpContext.Current.Session("valore_scontato_tariffa").ToString
            Dim ImportoSconto As Double = 0
            If valore_scontato_tariffa <> "" And valore_scontato_tariffa <> "" Then
                ImportoSconto = CDbl(Valore_Originale_Tariffa) - CDbl(valore_scontato_tariffa)
            End If
            lbl_Importo_Sconto.Text = FormatNumber(ImportoSconto.ToString, 2)
            Dim aggiornaSconto As String = funzioni_comuni_new.AggiornaCostiSconto(idPrenotazione.Text, numCalcolo.Text, "R", ImportoSconto, 0)
        Else
            lbl_Importo_Sconto.Text = "0"
        End If
        '@end salvo

        'verifica deposito cauzionale 28.01.2022
        '05.01.2022 mantiene disabilitate le opzioni
        If VerificaOpzione(listPrenotazioniCosti, "223", "ck") = "False" Then
            SetOpzione(listPrenotazioniCosti, "223", False, False, False)
        Else
            SetOpzione(listPrenotazioniCosti, "223", True, False, False)
        End If

        'verifica per deposito cauzionale 28.01.2022
        Dim deposito_cauzionale As String = ""      '26.01.2022
        Dim cf As String = ""                       '26.01.2022
        Dim idgruppo As String = id_gruppo_auto_scelto.Text      '28.01.2022 

        'recupera valore originario del deposito cauzionale 26.01.22
        Dim impDepDefault As String = GetValoreDepositoCauzionaleDefault(idgruppo)
        Dim impDepCalcolato As String = impDepDefault

        'se PPLUS o Elires 26.01.22 modifica deposito cauzionale
        If VerificaOpzione(listPrenotazioniCosti, "248", "ck") = "False" Or VerificaOpzione(listPrenotazioniCosti, "223", "ck") = "False" _
            Then
            'Riduce importo Deposito cauzionale 26.01.22
            impDepCalcolato = "200"
            funzioni_comuni.aggiorna_deposito_cauzionale("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
        End If

        'se Ambedue RIDUZIONI ATTIVE e PPLUS disabilitate riduce valore deposito cauzionale 
        ' ma senza PPLUS Attiva
        If VerificaOpzione(listPrenotazioniCosti, "248", "ck") = "False" And VerificaOpzione(listPrenotazioniCosti, "100", "ck") = "True" _
            And VerificaOpzione(listPrenotazioniCosti, "170", "ck") = "True" And VerificaOpzione(listPrenotazioniCosti, "223", "ck") = "False" Then
            impDepCalcolato = "300"
            funzioni_comuni.aggiorna_deposito_cauzionale("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
        End If




        'Nessuna opzione attiva 28.01.22
        If VerificaOpzione(listPrenotazioniCosti, "248", "ck") = "False" _
            And VerificaOpzione(listPrenotazioniCosti, "100", "ck") = "False" And VerificaOpzione(listPrenotazioniCosti, "170", "ck") = "False" _
            And VerificaOpzione(listPrenotazioniCosti, "223", "ck") = "False" Then 'aggiornato 03.02.2022
            impDepCalcolato = impDepDefault
            funzioni_comuni.aggiorna_deposito_cauzionale("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
        End If

        'la verifica su riduzione franchigia al 50% deve essere l'ultima verifica 03.02.2022
        'se opzione 'Riduzione Deposito 50%' importo = deposito / 2
        If VerificaOpzione(listPrenotazioniCosti, "234", "ck") = "True" Then
            impDepCalcolato = FormatNumber((CDbl(impDepCalcolato) / 2), 2)
            funzioni_comuni.aggiorna_deposito_cauzionale("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
        End If



        impDepCalcolato = impDepCalcolato

        listPrenotazioniCosti.DataBind()        'refresh list 28.01.2022


        'end - verifica deposito cauzionale




    End Sub

    Protected Function almeno_un_accessorio_omaggiato() As Boolean
        almeno_un_accessorio_omaggiato = False

        Dim omaggiato As CheckBox
        For i = 0 To listPrenotazioniCosti.Items.Count - 1
            omaggiato = listPrenotazioniCosti.Items(i).FindControl("chkOldOmaggio")
            If omaggiato.Checked Then
                almeno_un_accessorio_omaggiato = True
                Exit For
            End If
        Next
    End Function

    Protected Sub btnRicalcolaPrepagato_Click(sender As Object, e As System.EventArgs) Handles btnRicalcolaPrepagato.Click




        'If id_conducente.Text = "" Then
        '    Libreria.genUserMsgBox(Me, "E' necessario selezionare un guidatore a cui emettere fattura l'importo prepagato.")
        If id_ditta.Text = "" Then
            Libreria.genUserMsgBox(Me, "E' necessario selezionare una ditta. Se non è necessaria la fattura selezionare 'CLIENTE CASH' come ditta.")
            'ElseIf txtSconto.Text <> "" AndAlso CInt(txtSconto.Text) > 0 And txtCodiceSconto.Enabled Then
            '    Libreria.genUserMsgBox(Me, "E' necessario rimuovere lo sconto. Specificare un codice convenzione per poter scontare l'importo prepagato.")
        ElseIf dropTipoSconto.SelectedValue = "0" And txtSconto.Text <> "" AndAlso CInt(txtSconto.Text) > 0 Then
            Libreria.genUserMsgBox(Me, "E' possibile prepagare applicando uno sconto unicamente al Valore Tariffa.")
            'ElseIf txtGiorniPrepagati.Visible AndAlso ricalcolaPrepagato.Text <> "1" AndAlso getImportoPrepagato() <> 0 Then
            '    Libreria.genUserMsgBox(Me, "Impossibile procedere con l'integrazione di prepagamento a causa della differenza tra totale calcolato da ARES e totale precedentemente incassato.")
        ElseIf almeno_un_accessorio_omaggiato() Then
            Libreria.genUserMsgBox(Me, "Attenzione: rimuovere gli accessori omaggiati per poter procedere al prepagamento.")
        Else
            btnSalvaModifiche.Visible = False
            ricalcola_non_broker(True)

            '05.01.2022 mantiene disabilitate le opzioni
            If VerificaOpzione(listPrenotazioniCosti, "223", "ck") = "False" Then
                SetOpzione(listPrenotazioniCosti, "223", False, False, False)
            Else
                SetOpzione(listPrenotazioniCosti, "223", True, False, False)
            End If

            'verifica per deposito cauzionale 28.01.2022
            Dim deposito_cauzionale As String = ""      '26.01.2022
            Dim cf As String = ""                       '26.01.2022
            Dim idgruppo As String = id_gruppo_auto_scelto.Text      '28.01.2022 

            'recupera valore originario del deposito cauzionale 26.01.22
            Dim impDepDefault As String = GetValoreDepositoCauzionaleDefault(idgruppo)
            Dim impDepCalcolato As String = impDepDefault

            'se PPLUS o Elires 26.01.22 modifica deposito cauzionale
            If VerificaOpzione(listPrenotazioniCosti, "248", "ck") = "False" Or VerificaOpzione(listPrenotazioniCosti, "223", "ck") = "False" _
                Then
                'Riduce importo Deposito cauzionale 26.01.22
                impDepCalcolato = "200"
                funzioni_comuni.aggiorna_deposito_cauzionale("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
            End If

            'se Ambedue RIDUZIONI ATTIVE e PPLUS disabilitate riduce valore deposito cauzionale 
            ' ma senza PPLUS Attiva
            If VerificaOpzione(listPrenotazioniCosti, "248", "ck") = "False" And VerificaOpzione(listPrenotazioniCosti, "100", "ck") = "True" _
                And VerificaOpzione(listPrenotazioniCosti, "170", "ck") = "True" And VerificaOpzione(listPrenotazioniCosti, "223", "ck") = "False" Then
                impDepCalcolato = "300"
                funzioni_comuni.aggiorna_deposito_cauzionale("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
            End If


            'Nessuna opzione attiva 28.01.22
            If VerificaOpzione(listPrenotazioniCosti, "248", "ck") = "False" _
                And VerificaOpzione(listPrenotazioniCosti, "100", "ck") = "False" And VerificaOpzione(listPrenotazioniCosti, "170", "ck") = "False" _
                And VerificaOpzione(listPrenotazioniCosti, "223", "ck") = "False" Then
                impDepCalcolato = impDepDefault
                funzioni_comuni.aggiorna_deposito_cauzionale("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
            End If


            'se opzione 'Riduzione Deposito 50%' importo = deposito / 2
            If VerificaOpzione(listPrenotazioniCosti, "234", "ck") = "True" Then
                impDepCalcolato = FormatNumber((CDbl(impDepCalcolato) / 2), 2)
                funzioni_comuni.aggiorna_deposito_cauzionale("", "", idPrenotazione.Text, "", numCalcolo.Text, idgruppo, "283", impDepCalcolato)
            End If


            impDepCalcolato = impDepCalcolato

            listPrenotazioniCosti.DataBind()        'refresh list 28.01.2022



        End If
    End Sub

    Protected Sub esegui_ricalcolo_giorni_modificati(ByVal imposta_prepagato As Boolean)
        If tipo_prenotazione.Text = "richiama" Then
            numCalcolo.Text = CInt(numCalcolo.Text) + 1
            tipo_prenotazione.Text = "modifica"
        End If

        vedi_tariffe(imposta_prepagato)

        scegli_attivo.Text = "0"
        btnVediUltimoCalcolo.Visible = False

        'E' NECESSARIO AGGIUNGERE GLI ACCESSORI PRECEDENTI ------------------------------------------------------------------------------
        'VIENE GIA' FATTO DENTRO vedi_tariffe
        'aggiungi_accessori_precedente_calcolo()
        '--------------------------------------------------------------------------------------------------------------------------------

        listPrenotazioniCosti.DataBind()
    End Sub

    Protected Sub omaggia_tutto_x_complimentary()
        'NEL CASO DI COMPLIMENTARY OMAGGIO TUTTI I COSTI 

        Dim id_gruppoLabel As Label
        Dim id_a_carico_di As Label
        Dim id_elemento As Label
        Dim num_elemento As Label
        Dim nome_costo As Label
        Dim obbligatorio As Label
        Dim tipologia As Label
        Dim sottotipologia As Label

        For i = 0 To listPrenotazioniCosti.Items.Count - 1
            id_gruppoLabel = listPrenotazioniCosti.Items(i).FindControl("id_gruppoLabel")
            id_a_carico_di = listPrenotazioniCosti.Items(i).FindControl("id_a_carico_di")
            id_elemento = listPrenotazioniCosti.Items(i).FindControl("id_elemento")
            nome_costo = listPrenotazioniCosti.Items(i).FindControl("nome_costo")
            obbligatorio = listPrenotazioniCosti.Items(i).FindControl("obbligatorio")
            tipologia = listPrenotazioniCosti.Items(i).FindControl("tipologia_franchigia")
            sottotipologia = listPrenotazioniCosti.Items(i).FindControl("sottotipologia_franchigia")

            'VALORE TARIFFA
            If id_a_carico_di.Text = Costanti.id_accessorio_incluso And id_elemento.Text = Costanti.ID_tempo_km Then
                num_elemento = listPrenotazioniCosti.Items(i).FindControl("num_elemento")

                funzioni.omaggio_accessorio(True, False, False, "", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppoLabel.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia.Text, sottotipologia.Text)
            End If

            'OBBLIGATORI
            If id_a_carico_di.Text <> Costanti.id_accessorio_incluso And obbligatorio.Text = "True" And nome_costo.Text <> Costanti.testo_elemento_totale Then
                num_elemento = listPrenotazioniCosti.Items(i).FindControl("num_elemento")

                funzioni.omaggio_accessorio(True, False, False, "", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppoLabel.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia.Text, sottotipologia.Text)
            End If


            'ACCESSORI
            Dim chkScegli As CheckBox = listPrenotazioniCosti.Items(i).FindControl("chkScegli")
            If listPrenotazioniCosti.Items(i).FindControl("chkScegli").Visible Or (Not listPrenotazioniCosti.Items(i).FindControl("chkScegli").Visible And listPrenotazioniCosti.Items(i).FindControl("chkOmaggio").Visible) Then
                Dim chkOldScegli As CheckBox = listPrenotazioniCosti.Items(i).FindControl("chkOldScegli")
                If chkOldScegli.Checked Then
                    num_elemento = listPrenotazioniCosti.Items(i).FindControl("num_elemento")

                    funzioni.omaggio_accessorio(True, False, False, "", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppoLabel.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, tipologia.Text, sottotipologia.Text)
                End If
            End If
        Next
    End Sub

    Protected Sub aggiungi_accessori_precedente_calcolo(ByVal imposta_prepagato As Boolean)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

        Dim id_gruppoLabel As Label
        'NON UTILIZZO id_gruppo_auto_scelto IN QUANTO, SE SONO IN FASE DI MODIFICA BROKER, POSSO AVER SCELTO UNO O PIU' GRUPPI DIVERSI
        'DA QUELLO ORIGINALE

        For i = 0 To listPrenotazioniCosti.Items.Count - 1
            'SE L'ELEMENTO E' UNO DEGLI ELEMENTI A SCELTA CONTROLLO SE PER IL NUMERO DI CALCOLO PRECENDENTE ERA STATO SELEZIONATO
            'SE SI LO SELEZIONO ED AGGIUNGO IL COSTO AL TOTALE
            id_gruppoLabel = listPrenotazioniCosti.Items(i).FindControl("id_gruppoLabel")

            Dim chkScegli As CheckBox = listPrenotazioniCosti.Items(i).FindControl("chkScegli")
            If listPrenotazioniCosti.Items(i).FindControl("chkScegli").Visible Or (Not listPrenotazioniCosti.Items(i).FindControl("chkScegli").Visible And listPrenotazioniCosti.Items(i).FindControl("chkOmaggio").Visible) Then
                Dim id_elemento As Label = listPrenotazioniCosti.Items(i).FindControl("id_elemento")
                Cmd = New Data.SqlClient.SqlCommand("SELECT selezionato FROM prenotazioni_costi WITH(NOLOCK) WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "' AND id_elemento='" & id_elemento.Text & "'", Dbc)

                Dim precedentemente_selezionato As String = Cmd.ExecuteScalar

                If precedentemente_selezionato = "True" Then
                    'CONTROLLO SE ERA STATO OMAGGIATO E SE L'ACCESSORIO E' ANCORA OMAGGIABILE - SE LA PRENOTAZIONE E' COMPLIMENTARI SALTO QUESTO CONTROLLO IN QUANTO E' SEMPRE OMAGGIABILE
                    Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(omaggiato,'False') As omaggiato FROM prenotazioni_costi WITH(NOLOCK) WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "' AND id_elemento='" & id_elemento.Text & "'", Dbc)
                    Dim precedentemente_omaggiato As Boolean = Cmd.ExecuteScalar
                    Dim omaggiabile As Boolean

                    If complimentary.Text = "1" Then
                        omaggiabile = True
                    Else
                        Cmd = New Data.SqlClient.SqlCommand("SELECT omaggiabile FROM prenotazioni_costi WITH(NOLOCK) WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) & "' AND id_elemento='" & id_elemento.Text & "' AND id_gruppo='" & id_gruppoLabel.Text & "'", Dbc)
                        omaggiabile = Cmd.ExecuteScalar
                    End If

                    If precedentemente_omaggiato And omaggiabile Then
                        'SE L'ACCESSORIO ERA STATO OMAGGIATO ED E' ANCORA OMAGGIABILE VIENE NUOVAMENTE OMAGGIATO (SIA 
                        'PER ACCESSORI A SCELTA CHE PER ACCESSORI OBBLIGATORI)
                        Dim num_elemento As Label = listPrenotazioniCosti.Items(i).FindControl("num_elemento")
                        funzioni.omaggio_accessorio(True, False, False, "", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppoLabel.Text, id_elemento.Text, num_elemento.Text, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, "", "")
                    Else
                        If (listPrenotazioniCosti.Items(i).FindControl("chkScegli").Visible) Then
                            'AGGIUNGO IL COSTO SOLAMENTE SE SI TRATTA DI UN ACCESSORIO A SCELTA (SE E' UN ELEMENTO
                            'OBBLIGATORIO IL COSTO E' GIA' STATO CALCOLATO QUANDO E' STATA ANALIZZATA LA CONDIZIONE
                            aggiungi_costo_accessorio("", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppoLabel.Text, id_elemento.Text, "", "", "", imposta_prepagato, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue)
                        End If
                    End If

                    'SE SI TRATTA DI SECONDO GUIDATORE AGGIUNGO, SE NECESSARIO, IL COSTO DELLO YOUNG DRIVER
                    If id_elemento.Text = Costanti.Id_Secondo_Guidatore Then
                        Dim id_young_driver As String = get_id_young_driver()

                        nuovo_accessorio(id_young_driver, id_gruppoLabel.Text, "YOUNG", txtEtaPrimo.Text, txtEtaSecondo.Text, imposta_prepagato)  'MARCO

                        'SE ERA STATO OMAGGIATO PRECEDENTEMENTE (YOUNG DRIVER PER IL SECONDO GUIDATORE, num_elemento=2) LO OMAGGIO
                        'SE L'ELEMENTO E' STATO CALCOLATO
                        Cmd = New Data.SqlClient.SqlCommand("SELECT id  FROM prenotazioni_costi WITH(NOLOCK) WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) & "' AND id_elemento='" & id_young_driver & "' AND num_elemento='2' AND id_gruppo='" & id_gruppoLabel.Text & "'", Dbc)
                        Dim test As String = Cmd.ExecuteScalar & ""
                        If test <> "" Then
                            'LO YOUNG DRIVER PER IL SECONDO GUIDATORE E' STATO AGGIUNTO CONTROLLO SE PER IL CALCOLO PRECEDENTE ERA STATO OMAGGIATO
                            Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(omaggiato,'False') As omaggiato FROM prenotazioni_costi WITH(NOLOCK) WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "' AND id_elemento='" & id_young_driver & "' AND num_elemento='2'", Dbc)
                            precedentemente_omaggiato = Cmd.ExecuteScalar
                            If precedentemente_omaggiato Then
                                funzioni.omaggio_accessorio(True, False, False, "", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppoLabel.Text, id_young_driver, "2", dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, "", "")
                            End If
                        End If
                    End If

                    'SE E' STATO AGGINTO IL GPS AGGIUNGO EVENTUALMENTE IL VAL
                    Dim is_gps As Label = listPrenotazioniCosti.Items(i).FindControl("is_gps")
                    If is_gps.Text = "True" Then
                        If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then
                            'DEVO SAPERE SE NEL CALCOLO PRECEDENTE ERA PREPAGATO
                            Dim accessorio_non_prepagato = False

                            If txtGiorniPrepagati.Visible And Not imposta_prepagato Then
                                Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM condizioni_elementi WHERE tipologia='VAL_GPS'", Dbc)
                                Dim id_elemento_val_gps As String = Cmd.ExecuteScalar & ""

                                Cmd = New Data.SqlClient.SqlCommand("SELECT prepagato FROM prenotazioni_costi WITH(NOLOCK) WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "' AND id_elemento='" & id_elemento_val_gps & "'", Dbc)

                                Dim test As String = Cmd.ExecuteScalar & ""

                                If test = "" Then
                                    'NON C'ERA NEL CALCOLO PRECEDENTE
                                    accessorio_non_prepagato = True
                                ElseIf test = "False" Then
                                    accessorio_non_prepagato = True
                                End If
                            End If


                            nuovo_accessorio("", id_gruppoLabel.Text, "VAL_GPS", txtEtaPrimo.Text, txtEtaSecondo.Text, imposta_prepagato, accessorio_non_prepagato)
                        End If
                    End If

                End If
            End If
        Next

        'E' NECESSARIO ADESSO AGGIUNGERE GLI ACCESSORI EXTRA PRECEDENTEMENTE SELEZIONATI - OMAGGIO SE ERA OMAGGIATO PRECEDENTEMENTE
        'SE L'ACCESSORIO E' ANCORA OMAGGIABILE
        Cmd = New Data.SqlClient.SqlCommand("SELECT id_elemento, ISNULL(omaggiato,'False') As omaggiato, condizioni_elementi.omaggiabile, ISNULL(condizioni_elementi.is_gps,'False') As is_gps, prenotazioni_costi.num_elemento, prepagato  FROM prenotazioni_costi WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON prenotazioni_costi.id_elemento=condizioni_elementi.id WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & CInt(numCalcolo.Text) - 1 & "' AND obbligatorio='0' AND selezionato='1' AND valorizza='0'", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Do While Rs.Read()
            Dim accessorio_non_prepagato As Boolean = False

            If Not imposta_prepagato And Not Rs("prepagato") Then
                accessorio_non_prepagato = True
            End If

            nuovo_accessorio(Rs("id_elemento"), id_gruppoLabel.Text, "ELEMENTO", txtEtaPrimo.Text, txtEtaSecondo.Text, imposta_prepagato, accessorio_non_prepagato)
            Dim precedentemente_omaggiato As Boolean = Rs("omaggiato")
            Dim omaggiabile As Boolean = Rs("omaggiabile")
            If precedentemente_omaggiato Then
                If omaggiabile Then
                    Dim num_elemento As String = Rs("num_elemento") & ""
                    funzioni.omaggio_accessorio(True, False, False, "", "", idPrenotazione.Text, "", numCalcolo.Text, id_gruppoLabel.Text, Rs("id_elemento"), num_elemento, dropTipoCommissione.SelectedValue, txtPercentualeCommissionabile.Text, dropFonteCommissionabile.SelectedValue, "", "")
                End If
            End If

            'SE L'ELEMENTO EXTRA E' UN GPS
            If Rs("is_gps") Then
                If dropStazionePickUp.SelectedValue <> dropStazioneDropOff.SelectedValue Then

                    nuovo_accessorio("", id_gruppoLabel.Text, "VAL_GPS", txtEtaPrimo.Text, txtEtaSecondo.Text, imposta_prepagato, accessorio_non_prepagato)
                End If
            End If
        Loop

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub salva_contratto()
        'SALVATAGGIO DEL CONTRATTO E REINDIRIZZAMENTO VERSO LA PAGINA DI CONTRATTO
        Dim sqla As String = ""

        Try
            Dim id_contratto As String

            'modifica l'ora di uscita secondo l'ora corrente Aggiunto 19.12.2020 Modificato
            'se l'ora corrente è minore della data impostata
            ' assegna l'ora corrente
            If Hour(Date.Now) < CInt(ore1.Text) Then    '04.03.21
                ore1.Text = Format(Date.Now, "HH")
                minuti1.Text = Format(Date.Now, "mm")
            Else ' altrimenti lascia l'ora impostata

            End If

            Dim data_uscita As String = getDataDb_con_orario(txtDaData.Text & " " & ore1.Text & ":" & minuti1.Text & ":00")
            Dim data_rientro As String = getDataDb_con_orario(txtAData.Text & " " & ore2.Text & ":" & minuti2.Text & ":00")

            Dim id_tariffe_righe As Integer
            Dim id_tariffa As Integer
            Dim tipo_tariffa As String = ""
            Dim codice_tariffa As String

            If dropTariffeGeneriche.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeGeneriche.SelectedValue
                codice_tariffa = dropTariffeGeneriche.SelectedItem.Text
                tipo_tariffa = "generica"
            ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeParticolari.SelectedValue
                codice_tariffa = dropTariffeParticolari.SelectedItem.Text
                tipo_tariffa = "fonte"
            End If

            Dim num_prenotazione As String = lblNumPrenotazione.Text

            Dim codEdp As String
            Dim idDitta As String

            If Trim(txtCodiceCliente.Text) <> "" Then
                codEdp = "'" & txtCodiceCliente.Text & "'"
            Else
                codEdp = "NULL"
            End If

            If id_ditta.Text <> "" Then
                idDitta = "'" & id_ditta.Text & "'"
            Else
                idDitta = "NULL"
            End If

            'Dim num_contratto As String = "1"

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_tariffa FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)
            id_tariffa = Cmd.ExecuteScalar

            'Cmd = New Data.SqlClient.SqlCommand("SELECT codice FROM tariffe WITH(NOLOCK) WHERE id='" & id_tariffa & "'", Dbc)
            'codice_tariffa = Cmd.ExecuteScalar

            Dim id_gruppo_da_consegnare As String
            If dropGruppoDaConsegnare.SelectedValue = "0" Then
                id_gruppo_da_consegnare = "NULL"
            Else
                id_gruppo_da_consegnare = "'" & dropGruppoDaConsegnare.SelectedValue & "'"
            End If

            Dim nome_costo As Label
            Dim totale_da_prenotazione As Double
            Dim costo_scontato As Label
            For i = 0 To listPrenotazioniCosti.Items.Count - 1
                nome_costo = listPrenotazioniCosti.Items(i).FindControl("nome_costo")

                If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
                    costo_scontato = listPrenotazioniCosti.Items(i).FindControl("costo_scontato")
                    totale_da_prenotazione = CDbl(costo_scontato.Text)
                End If
            Next

            'PREPAGATA
            Cmd = New Data.SqlClient.SqlCommand("SELECT id_fonte_commissionabile, tipo_commissione, commissione_percentuale, giorni_commissioni, prepagata, importo_prepagato, giorni_prepagati, id_tempo_km_rack, rif_to, importo_a_carico_del_broker_ribaltato, gg_a_carico_del_broker_ribaltato, sconto_web_prepagato  FROM prenotazioni WITH(NOLOCK) WHERE Nr_Pren='" & idPrenotazione.Text & "' AND attiva='1'", Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            Rs.Read()

            Dim prepagata As String = Rs("prepagata") & ""
            If prepagata = "" Or prepagata = "False" Then
                prepagata = "'0'"
            ElseIf prepagata = "True" Then
                prepagata = "'1'"
            End If

            Dim importo_prepagato As String = Rs("importo_prepagato") & ""
            If importo_prepagato = "" Then
                importo_prepagato = "'0'"
            Else
                importo_prepagato = "'" & Replace(importo_prepagato, ",", ".") & "'"
            End If

            Dim giorni_prepagati As String = Rs("giorni_prepagati") & ""
            If giorni_prepagati = "" Then
                giorni_prepagati = "NULL"
            Else
                giorni_prepagati = "'" & giorni_prepagati & "'"
            End If

            Dim sconto_web_prepagato As String
            If (Rs("sconto_web_prepagato") & "") <> "" Then
                sconto_web_prepagato = "'" & Replace(Rs("sconto_web_prepagato"), ",", ".") & "'"
            Else
                sconto_web_prepagato = "NULL"
            End If

            Dim id_tempo_km_rack As String = Rs("id_tempo_km_rack") & ""
            If id_tempo_km_rack = "" Then
                id_tempo_km_rack = "NULL"
            Else
                id_tempo_km_rack = "'" & id_tempo_km_rack & "'"
            End If


            Dim id_primo_conducente As String
            If id_conducente.Text <> "" Then
                id_primo_conducente = "'" & id_conducente.Text & "'"
            Else
                id_primo_conducente = "NULL"
            End If

            Dim num_voucher As String = Rs("rif_to") & ""

            Dim imp_a_carico_del_broker_ribaltato As String
            Dim gg_a_carico_del_broker_ribaltato As String

            If (Rs("importo_a_carico_del_broker_ribaltato") & "") <> "" Then
                imp_a_carico_del_broker_ribaltato = "'" & Replace(Rs("importo_a_carico_del_broker_ribaltato"), ",", ".") & "'"
                gg_a_carico_del_broker_ribaltato = "'" & Rs("gg_a_carico_del_broker_ribaltato") & "'"
            Else
                imp_a_carico_del_broker_ribaltato = "NULL"
                gg_a_carico_del_broker_ribaltato = "NULL"
            End If

            Dim id_fonte_commissionabile As String
            If (Rs("id_fonte_commissionabile") & "") <> "" Then
                id_fonte_commissionabile = Rs("id_fonte_commissionabile")
            Else
                id_fonte_commissionabile = "NULL"
            End If

            Dim tipo_commissione As String
            If (Rs("tipo_commissione") & "") <> "" Then
                tipo_commissione = Rs("tipo_commissione")
            Else
                tipo_commissione = "NULL"
            End If

            Dim commissione_percentuale As String
            If (Rs("commissione_percentuale") & "") <> "" Then
                commissione_percentuale = Replace(Rs("commissione_percentuale"), ",", ".")
            Else
                commissione_percentuale = "NULL"
            End If

            Dim giorni_commissioni As String
            If (Rs("giorni_commissioni") & "") <> "" Then
                giorni_commissioni = Rs("giorni_commissioni")
            Else
                giorni_commissioni = "NULL"
            End If

            Rs.Close()
            Rs = Nothing
            Dbc.Close()
            Dbc.Open()

            Dim imp_a_carico_del_broker As String

            Dim giorni_to As String

            If a_carico_del_broker.Text <> "" Then
                'SE E' A CARICO DEL BROKER
                imp_a_carico_del_broker = "'" & Replace(a_carico_del_broker.Text, ",", ".") & "'"
                giorni_to = "'" & txtNumeroGiorniTO.Text & "'"
            Else
                imp_a_carico_del_broker = "NULL"
                giorni_to = "NULL"
            End If

            Dim targa_speciale As String
            If lblTargaSelezionata.Text <> "" Then
                targa_speciale = "'" & Replace(lblTargaSelezionata.Text, "'", "''") & "'"
            Else
                targa_speciale = "NULL"
            End If

            sqla = "INSERT INTO contratti (num_contratto, num_calcolo, status, attivo, id_stazione_uscita, id_stazione_presunto_rientro, " &
                    "data_uscita, data_presunto_rientro, id_gruppo_auto, giorni, giorni_to," &
                    "ID_GRUPPO_APP, id_primo_conducente, eta_primo_guidatore, eta_secondo_guidatore, id_fonte, id_fonte_commissionabile, tipo_commissione, commissione_percentuale," &
                    "giorni_commissioni," &
                    "codice_edp,id_cliente, id_tariffa, id_tariffe_righe,tariffa_rack_utilizzata, id_tempo_km_rack, tipo_tariffa, sconto_applicato, tipo_sconto, sconto_web_prepagato, " &
                    "CODTAR, id_operatore_creazione, data_creazione, num_prenotazione, totale_costo_prenotazione, prenotazione_prepagata, importo_prepagato, giorni_prepagati, " &
                    "giorni_noleggio_da_prenotazione, data_uscita_da_prenotazione, data_presunto_rientro_da_prenotazione, id_gruppo_da_prenotazione, " &
                    "id_stazione_drop_off_da_prenotazione, importo_a_carico_del_broker, importo_a_carico_del_broker_ribaltato, gg_a_carico_del_broker_ribaltato, rif_to, targa_gruppo_speciale_pren)"

            sqla = sqla & " VALUES "

            sqla = sqla & " ('','0','0','0','" & dropStazionePickUp.SelectedValue & "','" & dropStazioneDropOff.SelectedValue & "'," &
            "convert(datetime,'" & data_uscita & "',102),convert(datetime,'" & data_rientro & "',102),'" & id_gruppo_auto_scelto.Text & "','" & txtNumeroGiorni.Text & "'," & giorni_to & "," &
            id_gruppo_da_consegnare & "," & id_primo_conducente & ",'" & txtEtaPrimo.Text & "','" & txtEtaSecondo.Text & "','" & dropTipoCliente.SelectedValue & "'," &
            id_fonte_commissionabile & "," & tipo_commissione & "," & commissione_percentuale & "," & giorni_commissioni & "," & codEdp & "," & idDitta & "," &
            "'" & id_tariffa & "','" & id_tariffe_righe & "','0'," & id_tempo_km_rack & ",'" & tipo_tariffa & "','" & txtSconto.Text & "','" & dropTipoSconto.SelectedValue & "'," & sconto_web_prepagato & ",'" & Replace(codice_tariffa, "'", "''") & "', " &
            "'" & Request.Cookies("SicilyRentCar")("IdUtente") & "',GetDate()," & num_prenotazione & ",'" & Replace(totale_da_prenotazione, ",", ".") & "'," & prepagata & "," & importo_prepagato & "," & giorni_prepagati & "," &
            "'" & txtNumeroGiorni.Text & "',convert(datetime,'" & data_uscita & "',102),convert(datetime,'" & data_rientro & "',102),'" & id_gruppo_auto_scelto.Text & "'," &
            "'" & dropStazioneDropOff.SelectedValue & "'," & imp_a_carico_del_broker & "," & imp_a_carico_del_broker_ribaltato & "," & gg_a_carico_del_broker_ribaltato & ",'" & num_voucher & "'," &
            targa_speciale & ")"

            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd = New Data.SqlClient.SqlCommand("SELECT @@IDENTITY FROM contratti WITH(NOLOCK)", Dbc)
            id_contratto = Cmd.ExecuteScalar

            'SALVO LA RIGA DI CALCOLO E DI WARNING NELLE TABELLE CONTRATTI_COSTI E CONTRATTI_WARNING
            sqla = "INSERT INTO contratti_costi (id_documento, num_calcolo, ordine_stampa, id_gruppo, id_elemento, num_elemento, nome_costo," &
            "selezionato,omaggiato, prepagato, valore_costo,valore_percentuale,imponibile,iva_imponibile,imponibile_scontato,iva_imponibile_scontato," &
            "imponibile_onere, iva_onere, aliquota_iva,iva_inclusa,scontabile,omaggiabile,acquistabile_nolo_in_corso,id_a_carico_di,id_metodo_stampa,obbligatorio, franchigia_attiva, id_unita_misura, qta, packed, " &
            "imponibile_scontato_prepagato, iva_imponibile_scontato_prepagato, imponibile_onere_prepagato, iva_onere_prepagato, sconto_su_imponibile_prepagato, commissioni_imponibile, commissioni_iva, commissioni_imponibile_originale, commissioni_iva_originale)" &
            "(SELECT '" & id_contratto & "','0', ordine_stampa, id_gruppo,id_elemento,num_elemento,nome_costo," &
            "selezionato,omaggiato, prepagato, valore_costo,valore_percentuale,imponibile,iva_imponibile,imponibile_scontato,iva_imponibile_scontato," &
            "imponibile_onere, iva_onere, aliquota_iva,iva_inclusa,scontabile,omaggiabile,acquistabile_nolo_in_corso,id_a_carico_di,id_metodo_stampa,obbligatorio, franchigia_attiva, id_unita_misura, qta, packed, " &
            "imponibile_scontato_prepagato, iva_imponibile_scontato_prepagato, imponibile_onere_prepagato, iva_onere_prepagato, sconto_su_imponibile_prepagato, commissioni_imponibile, commissioni_iva, commissioni_imponibile_originale, commissioni_iva_originale FROM prenotazioni_costi WITH(NOLOCK) WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "')"

            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteScalar()

            'NON SALVO I WARNING - IN QUESTO MOMENTO ENTRANDO SU CONTRATTO DA PREVENTIVO O DA PRENOTAZIONE IL SISTEMA RICALCOLA TUTTI
            'I WARNING
            'sqlStr = "INSERT INTO contratti_warning (id_documento, num_calcolo, warning, id_operatore, tipo) " & _
            '        "(SELECT '" & id_contratto & "','1',warning,id_operatore,tipo FROM prenotazioni_warning WITH(NOLOCK) WHERE id_documento='" & idPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "')"

            'Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            'Cmd.ExecuteScalar()

            'SE E' STATO SCELTO UN UTENTE DA ANAGRAFICA LO SALVO IN contratti_conducenti
            'If id_conducente.Text <> "" Then
            '    sqlStr = "INSERT INTO contratti_conducenti (id_contratto, id_conducente) VALUES ('" & id_contratto & "','" & id_conducente.Text & "')"
            '    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            '    Cmd.ExecuteScalar()
            'End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            Session("id_contratto_from_prenotazioni") = id_contratto
            Session("idPrenotazione") = idPrenotazione.Text

            '# Aggiunto Salvo 13.02.2023
            Dim dtres As String = lblDataPrenotazione.Text
            If lblDataPrenotazione.Text <> "" Then
                dtres = CDate(lblDataPrenotazione.Text).Year & "-" & CDate(lblDataPrenotazione.Text).Month & "-" & CDate(lblDataPrenotazione.Text).Day & " " & CDate(lblDataPrenotazione.Text).Hour & ":" & CDate(lblDataPrenotazione.Text).Minute & ":" & CDate(lblDataPrenotazione.Text).Second
            End If
            Session("data_prenotazione") = dtres
            Session("data_pickup_pre") = txtDaData.Text & " " & txtoraPartenza.Text
            '@end salvo

            Response.Redirect("contratti.aspx?dtres=" & dtres & "&nres=" & idPrenotazione.Text & "&dtpk=" & Session("data_pickup_pre")) 'modificato Salvo 13.02.2023

        Catch ex As Exception
            HttpContext.Current.Response.Write("error prenotazioni salva_contratto  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")

        End Try


    End Sub

    Protected Function getIdContratto(ByVal num_contratto As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM contratti WITH(NOLOCK) WHERE num_contratto='" & num_contratto & "' AND attivo='1'", Dbc)

        getIdContratto = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function esistono_righe_tariffa() As Boolean

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM prenotazioni_costi WITH(NOLOCK) WHERE id_documento='" & idPrenotazione.Text & "'", Dbc)

            Dim test As String = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            If test <> "" Then
                esistono_righe_tariffa = True
            Else
                esistono_righe_tariffa = False
            End If
        Catch ex As Exception

        End Try

    End Function

    Protected Sub btnContratto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContratto.Click
        If statoPrenotazione.Text <> "3" Then
            If esistono_righe_tariffa() Then
                salva_contratto()
            Else
                Libreria.genUserMsgBox(Me, "Attenzione: la prenotazione non presenta righe di tariffa, impossibile creare il contratto.")
            End If
        Else
            Session("carica_contratto") = getIdContratto(numContratto.Text)
            Response.Redirect("contratti.aspx")
        End If
    End Sub

    Private Function getCodGruppoById(IdGruppo As String, Optional Attivo As Boolean = False) As String
        Dim sqlStr As String = "SELECT cod_gruppo  FROM gruppi WITH(NOLOCK) WHERE id_gruppo='" & IdGruppo & "'"
        If Attivo Then
            sqlStr += " AND attivo='1'"
        End If
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        getCodGruppoById = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Private Function getTipoCodiceFattura() As TipoCodiceFattura
        Dim from_fatture As TipoCodiceFattura = New TipoCodiceFattura

        from_fatture.anno_fattura = Integer.Parse(lb_anno_fattura.Text)
        from_fatture.codice_fattura = Integer.Parse(lb_codice_fattura.Text)
        from_fatture.tipo_fattura = Integer.Parse(lb_tipo_fattura.Text)
        from_fatture.id_ditta = Integer.Parse(lb_id_ditta_fattura.Text)

        from_fatture.id_riferimento = Integer.Parse(txtNumPrenotazione.Text)
        'Trace.Write(from_fatture.anno_fattura & " - " & from_fatture.anno_fattura & " - " & from_fatture.codice_fattura & " - " & from_fatture.id_ditta)
        Return from_fatture
    End Function

    Private Sub setTipoCodiceFattura(from_fatture As TipoCodiceFattura)

        lb_anno_fattura.Text = from_fatture.anno_fattura
        lb_codice_fattura.Text = from_fatture.codice_fattura
        lb_tipo_fattura.Text = from_fatture.tipo_fattura
        lb_id_ditta_fattura.Text = from_fatture.id_ditta
        txtNumPrenotazione.Text = from_fatture.id_riferimento
    End Sub

    Protected Sub btnRichiestaAnnullamento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRichiestaAnnullamento.Click
        If prenotazione_modificabile("0") Then
            'LE PRENOTAZIONI IN STATO 'RICHIESTA DI ANNULLAMENTO' SONO PRENOTAZIONI ATTIVE PER CUI E' STATO SALVATO UN MOTIVO DI ANNULLAMENTO
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            'SE NON VIENE SELEZIONATO ALCUN MOTIVO DI ANNULLAMENTO VUOL DIRE CHE SI VUOLE ANNULLARE LA RICHIESTA
            Dim motivo_annullamento As String

            If dropMotivoAnnullamento.SelectedValue <> "0" Then
                motivo_annullamento = "'" & dropMotivoAnnullamento.SelectedValue & "'"
            Else
                motivo_annullamento = "NULL"
            End If

            Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE prenotazioni SET  id_motivo_annullamento=" & motivo_annullamento & " WHERE Nr_Pren='" & idPrenotazione.Text & "'", Dbc)
            Cmd.ExecuteNonQuery()

            If dropMotivoAnnullamento.SelectedValue <> "0" Then
                Libreria.genUserMsgBox(Me, "Richiesta di annullamento memorizzata correttamente.")
                lblRichiestaAnnullamento.Visible = True
            Else
                Libreria.genUserMsgBox(Me, "La prenotazione è nuovamente attiva.")
                lblRichiestaAnnullamento.Visible = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: la prenotazione non è più modificabile in quanto è variato lo stato. E' necessario tornare alla maschera di ricerca e ricaricare la prenotazione.")
        End If
    End Sub

    Protected Sub btnRipristinaPrenotazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRipristinaPrenotazione.Click
        If prenotazione_modificabile("2") Then
            'Dim data_prenotazione As DateTime = funzioni_comuni.getDataDb_senza_orario(txtDaData.Text)
            'LA PRENOTAZIONE E' RIPRISTINABILE ANCHE SE SCADUTA
            'If DateDiff(DateInterval.Day, Now(), data_prenotazione) >= 0 Then
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE prenotazioni SET status='0', id_motivo_annullamento=NULL, data_annullamento=NULL, id_operatore_annullamento=NULL, data_ripristino=getDate(), id_operatore_ripristino='" & Request.Cookies("SicilyRentCar")("idUtente") & "' WHERE Nr_Pren='" & idPrenotazione.Text & "'", Dbc)
            Cmd.ExecuteNonQuery()


            Libreria.genUserMsgBox(Me, "Prenotazione ripristinata correttamente")

            Session("num_prenotazione_from_preventivi") = txtNumPrenotazione.Text
            Response.Redirect("prenotazioni.aspx")

            statoPrenotazione.Text = "0"

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
            'Else
            '    Libreria.genUserMsgBox(Me, "Prenotazione scaduta: impossibile procedere con il ripristino.")
            'End If
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: la prenotazione non è più modificabile in quanto è variato lo stato. E' necessario tornare alla maschera di ricerca e ricaricare la prenotazione.")
        End If
    End Sub

    Protected Sub btnAssegnaTarga_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssegnaTarga.Click
        If btnAssegnaTarga.Text = "Assegna" Then
            If txtTarga.Text <> "" Then

                txtTarga.ReadOnly = True

                'SI CONTROLLA UNICAMENTE SE L'AUTO ESISTE - ANCHE SE NON EISISTE, DOPO UNA CONFERMA, SI PERMETTE DI ASSEGNARLA ALLA PRENOTAZIONE
                '(POTREBBE ESSERE UN VEICOLO ANCORA NON IN PARCO)
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("SELECT modelli.id_gruppo FROM veicoli WITH(NOLOCK) INNER JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello WHERE veicoli.targa='" & Replace(txtTarga.Text, "'", "''") & "'", Dbc)

                Dim test As String = Cmd.ExecuteScalar & ""

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                Dim msg As String = ""

                If test = "" Then
                    'AUTO NON TROVATA
                    msg = "Veicolo non trovato."
                ElseIf test <> id_gruppo_auto_scelto.Text Then
                    'GRUPPO DIVERSO DA QUELLO CALCOLATO
                    msg = "Gruppo diverso dal gruppo calcolato."
                Else
                    assegna_targa()
                    btnAnnullaAssegnaTarga.Visible = False
                    btnAssegnaTarga.Text = "Rimuovi"
                End If

                If msg <> "" Then
                    'SONO PRESENTI DEGLI AVVISI PER CUI E' NECESSARIO CONFERMARE LA SCELTA DELLA VETTURA
                    msg = msg & vbCrLf & "E' necessario confermare la targa."
                    Libreria.genUserMsgBox(Me, msg)
                    btnAssegnaTarga.Text = "Conferma"
                    btnAnnullaAssegnaTarga.Visible = True
                End If
            Else
                Libreria.genUserMsgBox(Me, "Specificare la targa da assegnarea alla prenotazione.")
            End If
        ElseIf btnAssegnaTarga.Text = "Conferma" Then
            assegna_targa()
            btnAnnullaAssegnaTarga.Visible = False
            btnAssegnaTarga.Text = "Rimuovi"
        ElseIf btnAssegnaTarga.Text = "Rimuovi" Then
            rimuovi_targa()
            txtTarga.Text = ""
            txtTarga.ReadOnly = False
            btnAssegnaTarga.Text = "Assegna"
        End If
    End Sub

    Protected Sub rimuovi_targa()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE prenotazioni SET targa_gruppo_speciale=NULL WHERE Nr_Pren='" & idPrenotazione.Text & "'", Dbc)

        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        lblTargaSelezionata.Text = ""

        Libreria.genUserMsgBox(Me, "Targa rimossa correttamente.")
    End Sub

    Protected Sub assegna_targa()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE prenotazioni SET targa_gruppo_speciale='" & Replace(txtTarga.Text, "'", "''") & "' WHERE Nr_Pren='" & idPrenotazione.Text & "'", Dbc)

        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        lblTargaSelezionata.Text = txtTarga.Text

        Libreria.genUserMsgBox(Me, "Targa assegnata correttamente.")
    End Sub

    Protected Sub btnAnnullaAssegnaTarga_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnullaAssegnaTarga.Click
        btnAnnullaAssegnaTarga.Visible = False
        btnAssegnaTarga.Text = "Assegna"
        txtTarga.Text = ""
        txtTarga.ReadOnly = False
    End Sub

    Protected Sub btnModificaDitta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaDitta.Click
        If Not anagrafica_ditte.Visible Then
            id_ditta.Text = ""
            txtNomeDitta.Text = ""

            anagrafica_conducenti.Visible = False
            anagrafica_ditte.Visible = True
        Else
            anagrafica_ditte.Visible = False
        End If

    End Sub

    Protected Function get_nome_franchigia_inglese(ByVal id_elemento As Label) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "SELECT descrizione_en FROM condizioni_elementi WITH(NOLOCK) WHERE id=" & id_elemento.Text
        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        get_nome_franchigia_inglese = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

    End Function

    Protected Sub inviaMailPrenotazione(ByVal mail_conducente As String)

        Try
            Dim mail As New MailMessage()

            'Dichiaro il mittente
            'mail.From = New MailAddress("noreply@sicilyrentcar.it")

            'mail.To.Add("msicilia@entermed.it")
            'mail.CC.Add("fran.lamp@libero.it")
            'mail.CC.Add("prenotazioni@sbc.it")

            mail.CC.Add("send@sicilyrentcar.it")

            mail.To.Add(mail_conducente)
            'mail.CC.Add("")

            'Imposta l'oggetto della Mail
            Dim oggmail As String = "Conferma Prenotazione / Booking Confirmation N. " & lblNumPrenotazione.Text
            mail.Subject = oggmail

            'Imposta la priorità  della Mail
            mail.Priority = MailPriority.High

            mail.IsBodyHtml = True

            Dim corpoMessaggio As String
            corpoMessaggio = "Gentile Cliente / Dear Client," & "<br /><br />" & "la Sua prenotazione è avvenuta con successo / your reservation was successful:<br><br>" &
        "<b>N. Prenotazione / Reservation: </b>" & lblNumPrenotazione.Text & "<br>" &
        "<b>Conducente / Driver: </b>" & txtCognomeConducente.Text & " " & txtNomeConducente.Text & "<br>"


            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(cod_gruppo,'') + ' ' + ISNULL(descrizione,'') FROM gruppi WITH(NOLOCK) WHERE id_gruppo='" & id_gruppo_auto_scelto.Text & "'", Dbc)


            Dim nomeStazioneNoCode As String = funzioni_comuni.GetNomeStazioneNoCode(dropStazionePickUp.SelectedItem.Text)

            corpoMessaggio = corpoMessaggio & "<b>Veicolo / Vehicle: </b> " & "Gruppo " & Cmd.ExecuteScalar & " o similare<br><br><b>RITIRO / PICK-UP </b><br>" &
            nomeStazioneNoCode & "<br>" &
            txtDaData.Text & " ore " & txtoraPartenza.Text & "<br>"

            Cmd = New Data.SqlClient.SqlCommand("SELECT indirizzo, telefono, cellulare, email, testo_mail FROM stazioni WITH(NOLOCK) WHERE id='" & dropStazionePickUp.SelectedValue & "'", Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            Rs.Read()

            Dim email_stazione As String = Rs("email") & ""

            Dim testo_mail As String = Rs("testo_mail") & ""


            Try
                mail.From = New MailAddress(email_stazione)
                mail.To.Add(email_stazione)
            Catch ex As Exception
                mail.From = New MailAddress("info@sicilyrentcar.it")
            End Try

            corpoMessaggio = corpoMessaggio & Rs("indirizzo") & ""

            If (Rs("telefono") & "") <> "" Then
                corpoMessaggio = corpoMessaggio & " Telefono " & Rs("telefono")
            End If

            'If (Rs("cellulare") & "") <> "" Then
            '    corpoMessaggio = corpoMessaggio & " (Cellulare " & Rs("cellulare") & ")"
            'End If
            nomeStazioneNoCode = funzioni_comuni.GetNomeStazioneNoCode(dropStazioneDropOff.SelectedItem.Text)
            corpoMessaggio = corpoMessaggio & "<br><br><b>RICONSEGNA / DROP OFF</b><br>" &
            nomeStazioneNoCode & "<br>" &
            txtAData.Text & " ore " & txtOraRientro.Text & "<br>"

            Dbc.Close()
            Dbc.Open()


            'aggiunto id 29.06.2022 salvo
            Cmd = New System.Data.SqlClient.SqlCommand("SELECT id,indirizzo, telefono, cellulare, email FROM stazioni WITH(NOLOCK) WHERE id='" & dropStazioneDropOff.SelectedValue & "'", Dbc)
            Rs = Cmd.ExecuteReader()
            Rs.Read()

            Dim idstazione As String = Rs("id") 'aggiunto 29.06.2022 salvo

            corpoMessaggio = corpoMessaggio & Rs("indirizzo") & ""

            If (Rs("telefono") & "") <> "" Then
                corpoMessaggio = corpoMessaggio & " Telefono " & Rs("telefono")
            End If

            'If (Rs("cellulare") & "") <> "" Then
            '    corpoMessaggio = corpoMessaggio & " (Cellulare " & Rs("cellulare") & ")"
            'End If


            corpoMessaggio += "<br><br> <b>Giorni / Days:</b> " & txtNumeroGiorni.Text


            'Insermento delle Condizioni Incluse 02.08.2021
            Dim Condizioni_incluse As String = ""
            Dim lblIncluso As Label
            Dim lblObbligatorio As Label
            Dim nome_costo_incluso As Label
            Dim check_attuale_incluso As CheckBox

            corpoMessaggio = corpoMessaggio & "<br><br> <b>Condizioni incluse / Conditions included</b>"

            For i = 0 To listPrenotazioniCosti.Items.Count - 1
                nome_costo_incluso = listPrenotazioniCosti.Items(i).FindControl("nome_costo")
                lblIncluso = listPrenotazioniCosti.Items(i).FindControl("lblIncluso")
                lblObbligatorio = listPrenotazioniCosti.Items(i).FindControl("lblObbligatorio")
                check_attuale_incluso = listPrenotazioniCosti.Items(i).FindControl("chkScegli")
                If lblIncluso.Visible = True Then
                    Condizioni_incluse += "<br>" & nome_costo_incluso.Text
                End If
                If lblObbligatorio.Visible = True Then
                    Condizioni_incluse += "<br>" & nome_costo_incluso.Text
                End If
            Next

            If Condizioni_incluse = "" Then
                corpoMessaggio += "<br>-" 'al  posto di Nessuno
            Else
                corpoMessaggio += Condizioni_incluse
            End If

            'Exit Sub 'test
            ' fine inserimento Condizioni Incluse

            'Lista Supplementi costi
            Dim supplementi As String = ""
            Dim check_attuale As CheckBox
            Dim nome_costo As Label
            Dim check_old_scegli As CheckBox
            Dim importo_totale As Label

            Dim franchigie As String = ""
            Dim lblInformativa As Label
            Dim costo_franchigie As Label
            Dim tipologia_franchigia As Label
            Dim sottotipologia_franchigia As Label

            Dim id_elemento As Label   '30.12.2021

            'Inserite per la visualizzazione del deposito cauzionale 26.01.2022
            Dim costo_scontato As Label                 '26.01.2022
            Dim deposito_cauzionale As String = ""      '26.01.2022
            Dim cf As String = ""                       '26.01.2022

            corpoMessaggio += "<br><br><b>Supplementi / Extra:</b>"

            For i = 0 To listPrenotazioniCosti.Items.Count - 1
                check_attuale = listPrenotazioniCosti.Items(i).FindControl("chkScegli")
                nome_costo = listPrenotazioniCosti.Items(i).FindControl("nome_costo")
                check_old_scegli = listPrenotazioniCosti.Items(i).FindControl("chkOldScegli")
                lblInformativa = listPrenotazioniCosti.Items(i).FindControl("lblInformativa")
                tipologia_franchigia = listPrenotazioniCosti.Items(i).FindControl("tipologia_franchigia")
                sottotipologia_franchigia = listPrenotazioniCosti.Items(i).FindControl("sottotipologia_franchigia")

                id_elemento = listPrenotazioniCosti.Items(i).FindControl("id_elemento")     '30.12.2021

                costo_scontato = listPrenotazioniCosti.Items(i).FindControl("costo_scontato")   'Inserite per la visualizzazione del deposito cauzionale 26.01.2022

                'SE check_attuale E' VISIBILE SIGNIFICA CHE L'ACCESSORIO E' A SCELTA, MENTRE CONTROLLO check_old_scegli PER ESSERE SICURO
                'CHE L'ACCESSORIO E' STATO AGGIUNTO AL PREZZO E NON SIA STATO SEMPLICEMENTE SELEZIONATO SENZA SALVARE

                If check_attuale.Visible And check_old_scegli.Checked Then
                    supplementi = supplementi & "<br>" & nome_costo.Text
                End If

                'NE APPROFITTO PER RECUPERARE IL COSTO DEL TOTALE CHE MI SERVIRA' DOPO
                If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
                    importo_totale = listPrenotazioniCosti.Items(i).FindControl("costo_scontato")
                End If

                'CONSERVO LE INFORMATIVE
                If lblInformativa.Visible Then
                    Try
                        'se elemento diverso da ProtezionePlus 248 visualizza le altre Franchigie
                        If id_elemento.Text <> "248" And id_elemento.Text <> "283" Then   '30.12.2021 e 26.01.2022
                            costo_franchigie = listPrenotazioniCosti.Items(i).FindControl("costo_scontato")
                            cf = costo_franchigie.Text
                            cf = cf.Replace("€", "")
                            cf = "€ " & cf
                            franchigie = franchigie & "<b>" & nome_costo.Text & " / " & get_nome_franchigia_inglese(listPrenotazioniCosti.Items(i).FindControl("id_elemento")) & ":</b> " & cf & "<br />"
                        End If

                    Catch ex As Exception

                    End Try

                End If

                If tipologia_franchigia.Text = "ASSICURAZIONE" Then
                    If check_attuale.Visible And check_old_scegli.Checked Then
                        If sottotipologia_franchigia.Text = "TOTALE" Then
                            franchigie = franchigie & "<b>Franchigia danni / Damage excess:</b> € 0,00<br />" &
                       "<b>Franchigia furto e incendio / Theft and fire excess:</b> € 0,00<br />"
                        End If
                    End If
                End If

                'se PPLUS 30.12.2021 (da verificare e casomai togliere)
                If id_elemento.Text = "248" And check_attuale.Checked = True Then
                    franchigie = franchigie & "<b>Franchigia danni / Damage excess:</b> € 0,00<br />" &
                       "<b>Franchigia furto e incendio / Theft and fire excess:</b> € 0,00<br />"
                End If


                'se deposito cauzionale visualizza riga 26.01.2022
                If id_elemento.Text = "283" And lblInformativa.Visible = True Then
                    cf = costo_scontato.Text
                    cf = cf.Replace("€", "")
                    cf = "€ " & cf
                    deposito_cauzionale = "<br/><b>Deposito cauzionale / Security deposit: </b>" & cf
                End If




            Next

            If supplementi = "" Then
                corpoMessaggio += "<br>-" 'modifica 21.04.2021 al  posto di Nessuno
            Else
                corpoMessaggio += supplementi
            End If


            If dropTariffeGeneriche.SelectedValue <> "0" Then
                corpoMessaggio += "<br><br> <b>Tariffa applicata / Rate applied:</b> " & dropTariffeGeneriche.SelectedItem.Text
            ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                corpoMessaggio += "<br><br> <b>Tariffa applicata / Rate applied:</b> " & dropTariffeParticolari.SelectedItem.Text
            End If



            corpoMessaggio = corpoMessaggio & "<br><br><b>Totale noleggio / Total:</b> € " & importo_totale.Text




            If franchigie <> "" Then
                corpoMessaggio += "<br><br>" & franchigie
            End If

            'Deposito Cauzionale 18.01.22
            If deposito_cauzionale <> "" Then
                corpoMessaggio += deposito_cauzionale
            End If


            corpoMessaggio = corpoMessaggio & "<br><br><b><a href='https://www.sicilyrentcar.it/condizioni-noleggio-auto/'>Termini e condizioni</a>&nbsp;/&nbsp;<a href='https://www.sicilyrentcar.it/en/rental-conditions/'>Terms and conditions</a></b>"

            'inserire qui FAQ email Francesco - salvo 25.11.2022
            corpoMessaggio += "<br/><br/><b>Hai qualche dubbio? Leggi le nostre <a href='https://www.sicilyrentcar.it/faq-noleggio-auto/'>FAQ</a></b>&nbsp;&nbsp;/&nbsp;&nbsp;<b>Do you have any doubts? Read our <a href='https://www.sicilyrentcar.it/en/car-rental-faq/'>FAQ</a></b>"
            '@ end nuovo inserimento 


            corpoMessaggio = corpoMessaggio & "<br><br><b>Per effettuare il pagamento clicca qui > <a href='https://www.sicilyrentcar.it/servizi-noleggio-auto/'>Modulo autorizzazione utilizzo carta</a>&nbsp;/&nbsp; To make the payment click here > <a href='https://www.sicilyrentcar.it/en/car-rental-services/'>Card use authorization form</a></b>"

            'nuovo inserimento da email di Francesco del 26.05.2021
            corpoMessaggio = corpoMessaggio & "<br><br>Se la tariffa applicata prevede il pagamento anticipato, entro 72 ore dalla conferma della prenotazione, sarà necessario procedere con il saldo totale del servizio. In caso di mancato pagamento entro i suddetti termini la tariffa potrebbe subire delle variazioni."
            corpoMessaggio = corpoMessaggio & "<br><br>If the rate applied requires prepayment of the rental, within 72 hours of confirmation of the booking, you must pay the full balance of the service. Failure to pay may result in a change in the rate."  ''

            'Tony 27-07-2022
            'Tolto da qui testo con firma

            corpoMessaggio = corpoMessaggio & "<br><br><b>Ricorda</b>: se sei un nuovo Cliente SRC Rent Car puoi effettuare la registrazione dei tuoi dati direttamente online collegandoti alla pagina <a href='http://www.sicilyrentcar.it/web-check-in-noleggio-auto/'>Web check-in</a>, un’esclusiva che ti consentirà di risparmiare tempo nella procedura di consegna dell'auto."
            corpoMessaggio = corpoMessaggio & "<br><br><b>Remember</b>: if you are a new Customer of SRC Rent Car you can now register your data directly online connecting to the page <a href='http://www.sicilyrentcar.it/en/web-check-in-car-rental/'>Web check-in</a>, an exclusive that will allow you to save time on delivery of the car."


            'TESTO NUOVO 29.06.2022 da campo testo_mail salvo
            corpoMessaggio += funzioni_comuni_new.getTestoMailImportante(idstazione)    'recupera il testo che va prima della firma

            'Tony 27-07-2022
            'Posto qui testo con firma
            'corpoMessaggio = corpoMessaggio & testo_mail & ""  'Salvo 10.10.2022 modificato senza testo_email perchè già inserito nella riga 6831 mod. del 29.06.2022
            corpoMessaggio = corpoMessaggio & ""

            'corpoMessaggio = corpoMessaggio & "<br><br>Cordiali saluti / Best regards" &
            '"<br><br><br>SRC Rent Car - Centro Prenotazioni<br> " &
            '"<br>e-mail: " & email_stazione

            corpoMessaggio = corpoMessaggio & "<br/><br/><a href='https://www.sicilyrentcar.it/'>"
            corpoMessaggio += "<img src='http://ares.sicilyrentcar.it/img/SRC_logo.jpg'></a>"


            mail.Body = Replace(corpoMessaggio, "!", "")

            Dim attachment As New System.Net.Mail.Attachment(Server.MapPath("\prenotazioni\" & "prenotazione" & "_" & lblNumPrenotazione.Text & ".pdf"))
            Dim fileallegato As String = Server.MapPath("\prenotazioni\" & "prenotazione" & "_" & lblNumPrenotazione.Text & ".pdf")
            mail.Attachments.Add(attachment)

            'Imposta il server smtp di posta da utilizzare        
            Dim client As New Net.Mail.SmtpClient("authsmtp.securemail.pro", 465)
            client.Credentials = New System.Net.NetworkCredential("send@sicilyrentcar.it", "sicilyrentcar")
            client.EnableSsl = True

            'client.Host = "authsmtp.securemail.pro"

            '## VERIFICATA E TESTATA 31.12.2020
            Dim sm As New sendmailcls

            'Invia l'e-mail a stazione NON INVIA SE SI PREME IL PULSANTE 31.12.2020

            Dim mittente As String = ""
            'se operatore sede il mittente è booking 
            If Request.Cookies("SicilyRentCar")("stazione") = "1" Then
                mittente = "booking@sicilyrentcar.it"
            Else
                mittente = email_stazione
            End If

            'aggiunta intestazione html 18.01.22
            'Dim prebody As String = "<!DOCTYPE html><html xmlns=""http://www.w3.org/1999/xhtml"""
            'prebody += "<head>meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8'/></head><body>"
            'Dim endbody As String = "</body></html>"
            'corpoMessaggio = prebody & corpoMessaggio & endbody
            '#


            Try

                sm.sendmail(mittente, "SRC Rent Car", email_stazione, oggmail, corpoMessaggio, True, fileallegato)
                'Libreria.genUserMsgBox(Me, "E-Mail inviata correttamente.")

                'Se stazione Cinisi invia anche a PAAPT 10.02.2022
                If dropStazionePickUp.SelectedItem.Text.IndexOf("cinisi") > -1 Or dropStazioneDropOff.SelectedItem.Text.IndexOf("cinisi") > -1 Then
                    sm.sendmail(email_stazione, "SRC Rent Car", "palermoapt@sicilyrentcar.it", oggmail, corpoMessaggio, True, "")
                End If


            Catch ex As Exception
                Libreria.genUserMsgBox(Me, "Si è verificato un errore nell'invio della mail alla stazione")
            End Try

            'invia email al cliente
            Try

                sm.sendmail(mittente, "SRC Rent Car", mail_conducente, oggmail, corpoMessaggio, True, fileallegato)
                Libreria.genUserMsgBox(Me, "E-Mail inviata correttamente.")
            Catch ex As Exception
                Libreria.genUserMsgBox(Me, "Si è verificato un errore nell'invio della mail al cliente")
            End Try

            'invia email a booking  da msg wapp 11.01.2021
            Try

                sm.sendmail(mittente, "SRC Rent Car", "booking@sicilyrentcar.it", oggmail, corpoMessaggio, True, fileallegato)
                Libreria.genUserMsgBox(Me, "E-Mail inviata correttamente.")
            Catch ex As Exception
                Libreria.genUserMsgBox(Me, "Si è verificato un errore nell'invio della mail al booking")
            End Try

            attachment.Dispose()

            Try
                File.Delete(Server.MapPath("\prenotazioni\" & "prenotazione" & "_" & lblNumPrenotazione.Text & ".pdf"))
            Catch ex As Exception

            End Try

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("error prenotazioni inviaMailPrenotazione : <br/>" & ex.Message & "<br/>" & "<br/>")

        End Try




    End Sub

    Protected Sub btnInviaMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInviaMail.Click
        If prenotazione_modificabile("0") Then
            If txtMailConducente.Text <> "" Then
                genera_prenotazione()
                inviaMailPrenotazione(txtMailConducente.Text)
            Else
                Libreria.genUserMsgBox(Me, "Specificare l'indirizzo e-mail del cliente.")
            End If
        Else
            Libreria.genUserMsgBox(Me, "Attenzione: la prenotazione è stata variata da un altro operatore. E' necessario tornare alla maschera di ricerca e ricaricarla per poter inviare la mail al cliente.")
        End If
    End Sub

    Protected Sub btnStampaTKm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaTKm.Click

        If dropTariffeGeneriche.SelectedValue = "0" And dropTariffeParticolari.SelectedValue = "0" Then
            Libreria.genUserMsgBox(Me, "Selezionare una tariffa.")
        ElseIf dropTariffeGeneriche.SelectedValue <> "0" And dropTariffeParticolari.SelectedValue <> "0" Then
            Libreria.genUserMsgBox(Me, "Selezionare una tariffa generica oppure una tariffa particolare.")
        Else
            Dim id_tariffe_righe As String
            If dropTariffeGeneriche.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeGeneriche.SelectedValue
            ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeParticolari.SelectedValue
            End If
            Dim tariffa_broker As Boolean = is_tariffa_broker(id_tariffe_righe)





            '# NUOVO recupera la lista delle tariffe salvo 07.12.2022

            Dim id_tempo_km As String = "" 'funzioni_comuni.getIdTempoKm(id_tariffe_righe)
            Dim idStazionePickUp As String = dropStazionePickUp.SelectedValue
            Dim idStazioneDropOff As String = dropStazioneDropOff.SelectedValue

            Dim dataPickup As String = txtDaData.Text   '"21/12/2022"
            Dim dataDropOff As String = txtAData.Text   '"24/12/2022"
            Dim gnolo As Integer = txtNumeroGiorni.Text 'DateDiff("d", CDate(dataPickup), CDate(dataDropOff))

            Dim tipoCli As String = dropTipoCliente.SelectedValue
            Dim idGruppo As String = "24" 'nn serve a nulla ma solo per riempire il parametro salvo 07.12.2022

            'verifica se tariffe generiche o tariffe particolari 
            'e ricava valore dal dropdown
            Dim TipoTariffa As String
            Dim descTariffa As String
            If dropTariffeGeneriche.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeGeneriche.SelectedValue
                descTariffa = dropTariffeGeneriche.SelectedItem.ToString
                TipoTariffa = "G"
            ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeParticolari.SelectedValue
                descTariffa = dropTariffeParticolari.SelectedItem.ToString
                TipoTariffa = "P"
            End If


            'recupera il codice descrizione completo della tariffa salvo 29.12.2022
            Dim IdTariffa As String = id_tariffe_righe
            descTariffa = funzioni_comuni_new.GetCodiceTariffa(IdTariffa)

            'lista delle tariffe - salvo 08.12.2022 12.12.2022
            id_tempo_km = funzioni_comuni_new.GetNewTariffaTempoKmPeriodiPDFTariffe(idStazionePickUp, idStazioneDropOff, dataPickup,
                                                                                          dataDropOff, tipoCli, idGruppo, TipoTariffa, descTariffa, idTariffa, gnolo)
            '@ end modifica per lista tariffe


            If tariffa_broker Then
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzareValoreTariffaBroker) <> "1" Then
                    'Dim id_tempo_km As String = funzioni_comuni.getIdTempoKm(id_tariffe_righe)

                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        Session("url_print") = "/stampe/stampa_tempo_km.aspx?pagina=verticale&id_tempo_km=" & id_tempo_km
                        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                        End If
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Non hai le autorizzazioni per visualizzare i Valori Tariffa per le Tariffe Broker.")
                End If
            Else
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzareValoreTariffa) <> "1" Then
                    ' Dim id_tempo_km As String = funzioni_comuni.getIdTempoKm(id_tariffe_righe)

                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        Session("url_print") = "/stampe/stampa_tempo_km.aspx?pagina=verticale&id_tempo_km=" & id_tempo_km
                        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                        End If
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Non hai le autorizzazioni per visualizzare i Valori Tariffa.")
                End If
            End If
        End If
    End Sub

    Protected Sub btnStampaCondizioni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaCondizioni.Click
        If dropTariffeGeneriche.SelectedValue = "0" And dropTariffeParticolari.SelectedValue = "0" Then
            Libreria.genUserMsgBox(Me, "Selezionare una tariffa.")
        ElseIf dropTariffeGeneriche.SelectedValue <> "0" And dropTariffeParticolari.SelectedValue <> "0" Then
            Libreria.genUserMsgBox(Me, "Selezionare una tariffa generica oppure una tariffa particolare.")
        Else
            Dim id_tariffe_righe As String
            If dropTariffeGeneriche.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeGeneriche.SelectedValue
            ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeParticolari.SelectedValue
            End If
            Dim tariffa_broker As Boolean = is_tariffa_broker(id_tariffe_righe)

            If tariffa_broker Then
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzareCondizioniTariffeBroker) <> "1" Then
                    Dim id_condizione As String = funzioni_comuni.getIdCondizione(id_tariffe_righe)
                    Dim id_condizione_madre As String = funzioni_comuni.getIdCondizioneMadre(id_tariffe_righe)


                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        Session("url_print") = "/stampe/stampa_tariffa.aspx?pagina=verticale&id_tariffe_righe=" & id_tariffe_righe & "&id_cond=" & id_condizione & "&id_cond_madre=" & id_condizione_madre
                        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                        End If
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Non hai le autorizzazioni per visualizzare le condizioni per le Tariffe Broker.")
                End If
            Else
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.VisualizzareCondizioniTariffa) <> "1" Then
                    Dim id_condizione As String = funzioni_comuni.getIdCondizione(id_tariffe_righe)
                    Dim id_condizione_madre As String = funzioni_comuni.getIdCondizioneMadre(id_tariffe_righe)

                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        Session("url_print") = "/stampe/stampa_tariffa.aspx?pagina=verticale&id_tariffe_righe=" & id_tariffe_righe & "&id_cond=" & id_condizione & "&id_cond_madre=" & id_condizione_madre
                        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                        End If
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Non hai le autorizzazioni per visualizzare le condizioni di tariffa.")
                End If
            End If
        End If
    End Sub

    Protected Sub stampa_prenotazione()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "SELECT (stazioni1.codice + ' - ' + stazioni1.nome_stazione) As stazione_pick, (stazioni2.codice + ' - ' + stazioni2.nome_stazione) As stazione_drop," &
         "CONVERT(char(10), prdata_out, 103) As prdata_out, CONVERT(char(10), prdata_pr, 103) As prdata_pr, ore_uscita, minuti_uscita, ore_rientro, minuti_rientro, " &
         "clienti_tipologia.descrizione As fonte, eta_primo_guidatore, eta_secondo_guidatore, giorni, giorni_to, cognome_conducente, nome_conducente, " &
         "mail_conducente, indirizzo_conducente, CONVERT(char(10), data_nascita, 103) As data_nascita, cod_gruppo_app, gruppi.cod_gruppo As codice_gruppo, " &
         "N_VOLOOUT, N_VOLOPR, rif_to, riferimento_telefono, id_cliente, codice_edp " &
         "FROM prenotazioni WITH(NOLOCK) INNER JOIN stazioni As stazioni1 WITH(NOLOCK) ON prenotazioni.prid_stazione_out=stazioni1.id " &
         "INNER JOIN stazioni As stazioni2 WITH(NOLOCK) ON prenotazioni.prid_stazione_pr=stazioni2.id " &
         "LEFT JOIN clienti_tipologia WITH(NOLOCK) ON prenotazioni.id_fonte=clienti_tipologia.id " &
         "INNER JOIN gruppi WITH(NOLOCK) ON prenotazioni.id_gruppo=gruppi.id_gruppo " &
         "WHERE numpren='" & lblNumPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "' AND prenotazioni.attiva='1'"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()


        If Rs.Read() Then
            Dim miei_dati As DatiStampaPrenotazione = New DatiStampaPrenotazione
            With miei_dati
                .n_num_prenotazione = "Numero prenotazione:   " & lblNumPrenotazione.Text
                .n_dettagli_prenotazione = "Data Prenotazione: " & lblDataPrenotazione.Text & "       -    " & lblNumeroVariazione.Text
                .n_pickup_location = Rs("stazione_pick") & " " & Rs("prdata_out") & " " & Rs("ore_uscita") & ":" & Rs("minuti_uscita")
                .n_dropoff_location = Rs("stazione_drop") & " " & Rs("prdata_pr") & " " & Rs("ore_rientro") & ":" & Rs("minuti_rientro")
                .n_fonte = Rs("fonte") & ""
                .n_cod_convenzione = ""
                .n_eta1 = Rs("eta_primo_guidatore") & ""
                .n_eta2 = Rs("eta_secondo_guidatore") & ""
                .n_gg = Rs("giorni") & ""
                .n_gg_to = Rs("giorni_to") & ""
                .n_cognome = Rs("cognome_conducente") & ""
                .n_nome = Rs("nome_conducente") & ""
                .n_email = Rs("mail_conducente") & ""
                .n_indirizzo = Rs("indirizzo_conducente") & ""
                .n_nascita = Rs("data_nascita") & ""
                If (Rs("cod_gruppo_app") & "") <> "" Then
                    .n_gruppo = Rs("cod_gruppo_app")
                Else
                    .n_gruppo = Rs("codice_gruppo") & ""
                End If
                .n_nvolo1 = Rs("N_VOLOOUT") & ""
                .n_nvolo2 = Rs("N_VOLOPR") & ""
                .n_riferimento = Rs("rif_to") & ""
                .n_riftel = Rs("riferimento_telefono") & ""
                If (Rs("codice_edp") & "") <> "" Then
                    .n_fatturare_a = getNomeDittaFromEdp(Rs("codice_edp"))
                ElseIf (Rs("id_cliente") & "") <> "" Then
                    .n_fatturare_a = getNomeDittaFromId(Rs("id_cliente"))
                End If

                'DETTAGLI COSTI ---------------------------------------------------------------------------------------------------------------
                Dim lblIncluso As Label
                Dim lblObbligatorio As Label
                Dim lblInformativa As Label
                Dim nome_costo As Label
                Dim costo_scontato As Label
                Dim chkOldOmaggio As CheckBox
                Dim chkScegli As CheckBox
                Dim chkOldScegli As CheckBox
                Dim a_carico_to As Label

                Dim prima_informativa As Boolean = True

                For i = 0 To listPrenotazioniCosti.Items.Count - 1
                    lblIncluso = listPrenotazioniCosti.Items(i).FindControl("lblIncluso")
                    lblObbligatorio = listPrenotazioniCosti.Items(i).FindControl("lblObbligatorio")
                    lblInformativa = listPrenotazioniCosti.Items(i).FindControl("lblInformativa")
                    nome_costo = listPrenotazioniCosti.Items(i).FindControl("nome_costo")
                    costo_scontato = listPrenotazioniCosti.Items(i).FindControl("costo_scontato")
                    chkOldOmaggio = listPrenotazioniCosti.Items(i).FindControl("chkOldOmaggio")
                    chkScegli = listPrenotazioniCosti.Items(i).FindControl("chkScegli")
                    chkOldScegli = listPrenotazioniCosti.Items(i).FindControl("chkOldScegli")
                    a_carico_to = listPrenotazioniCosti.Items(i).FindControl("a_carico_to")

                    If lblIncluso.Visible Or lblObbligatorio.Visible Or lblInformativa.Visible Then
                        If lblInformativa.Visible And prima_informativa Then
                            prima_informativa = False
                            .n_dettaglio_nome = .n_dettaglio_nome & vbCrLf
                            .n_dettaglio_costo = .n_dettaglio_costo & vbCrLf
                            .n_dettaglio_omaggio = .n_dettaglio_omaggio & vbCrLf
                            .n_dettaglio_costo_to = .n_dettaglio_costo_to & vbCrLf
                        End If

                        .n_dettaglio_nome = .n_dettaglio_nome & nome_costo.Text & vbCrLf
                        If costo_scontato.Visible Then
                            .n_dettaglio_costo = .n_dettaglio_costo & costo_scontato.Text & vbCrLf
                        Else
                            .n_dettaglio_costo = .n_dettaglio_costo & vbCrLf
                        End If

                        If chkOldOmaggio.Checked Then
                            .n_dettaglio_omaggio = .n_dettaglio_omaggio & "X" & vbCrLf
                        Else
                            .n_dettaglio_omaggio = .n_dettaglio_omaggio & vbCrLf
                        End If

                        .n_dettaglio_costo_to = .n_dettaglio_costo_to & vbCrLf
                    ElseIf chkScegli.Visible And chkOldScegli.Checked Then
                        .n_dettaglio_nome = .n_dettaglio_nome & nome_costo.Text & vbCrLf
                        .n_dettaglio_costo = .n_dettaglio_costo & costo_scontato.Text & vbCrLf
                        If chkOldOmaggio.Checked Then
                            .n_dettaglio_omaggio = .n_dettaglio_omaggio & "X" & vbCrLf
                        Else
                            .n_dettaglio_omaggio = .n_dettaglio_omaggio & vbCrLf
                        End If

                        .n_dettaglio_costo_to = .n_dettaglio_costo_to & vbCrLf
                    ElseIf Not chkScegli.Visible Then
                        .n_dettaglio_nome = .n_dettaglio_nome & nome_costo.Text & vbCrLf
                        If costo_scontato.Visible Then
                            .n_dettaglio_costo = .n_dettaglio_costo & costo_scontato.Text & vbCrLf
                        Else
                            .n_dettaglio_costo = .n_dettaglio_costo & vbCrLf
                        End If

                        If tariffa_broker.Text = "1" And a_carico_to.Visible Then
                            .n_dettaglio_costo_to = .n_dettaglio_costo_to & a_carico_to.Text & vbCrLf
                        Else
                            .n_dettaglio_costo_to = .n_dettaglio_costo_to & vbCrLf
                        End If

                        .n_dettaglio_omaggio = .n_dettaglio_omaggio & vbCrLf
                    End If
                Next
                '------------------------------------------------------------------------------------------------------------------------------

            End With

            Session("DatiStampaPrenotazione") = miei_dati

            Dim Generator As System.Random = New System.Random()
            Dim num_random As String = Format(Generator.Next(100000000), "000000000")

            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraStampaPrenotazione.aspx?a=" & num_random & "','')", True)
                End If
            End If
        Else
            Libreria.genUserMsgBox(Me, "Prenotazione non trovata o non più attiva - Impossibile procedere con la stampa.")
        End If


        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub genera_prenotazione()

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String = "SELECT numpren, datapren, num_calcolo, (stazioni1.codice + ' - ' + stazioni1.nome_stazione) As stazione_pick, (stazioni2.codice + ' - ' + stazioni2.nome_stazione) As stazione_drop," &
             "CONVERT(char(10), prdata_out, 103) As prdata_out, CONVERT(char(10), prdata_pr, 103) As prdata_pr, ore_uscita, minuti_uscita, ore_rientro, minuti_rientro, " &
             "clienti_tipologia.descrizione As fonte, eta_primo_guidatore, eta_secondo_guidatore, giorni, giorni_to, cognome_conducente, nome_conducente, " &
             "mail_conducente, indirizzo_conducente, CONVERT(char(10), data_nascita, 103) As data_nascita, cod_gruppo_app, gruppi.cod_gruppo As codice_gruppo, " &
             "N_VOLOOUT, N_VOLOPR, rif_to, riferimento_telefono, id_cliente, codice_edp " &
             "FROM prenotazioni WITH(NOLOCK) INNER JOIN stazioni As stazioni1 WITH(NOLOCK) ON prenotazioni.prid_stazione_out=stazioni1.id " &
             "INNER JOIN stazioni As stazioni2 WITH(NOLOCK) ON prenotazioni.prid_stazione_pr=stazioni2.id " &
             "LEFT JOIN clienti_tipologia WITH(NOLOCK) ON prenotazioni.id_fonte=clienti_tipologia.id " &
             "INNER JOIN gruppi WITH(NOLOCK) ON prenotazioni.id_gruppo=gruppi.id_gruppo " &
             "WHERE numpren='" & lblNumPrenotazione.Text & "' AND num_calcolo='" & numCalcolo.Text & "' AND prenotazioni.attiva='1'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()


            If Rs.Read() Then
                Dim myPdfReader As PdfReader
                Dim pdfFormFields As AcroFields

                Dim newFile As String = Server.MapPath("\prenotazioni\" & "prenotazione_" & Rs("numpren") & ".pdf")
                Using file_stream As FileStream = New FileStream(newFile, FileMode.Create)
                    myPdfReader = New PdfReader(Server.MapPath("\modelli_documenti\prenotazione.pdf"))
                    Using myPdfStamper As PdfStamper = New PdfStamper(myPdfReader, file_stream)


                        pdfFormFields = myPdfStamper.AcroFields

                        pdfFormFields.SetField("Text1", "Numero prenotazione:   " & Rs("numpren"))
                        pdfFormFields.SetField("Text2", "Data Prenotazione:" & Rs("datapren"))
                        pdfFormFields.SetField("pickup_location", Rs("stazione_pick") & " " & Rs("prdata_out") & " " & Rs("ore_uscita") & ":" & Rs("minuti_uscita"))
                        pdfFormFields.SetField("dropoff_location", Rs("stazione_drop") & " " & Rs("prdata_pr") & " " & Rs("ore_rientro") & ":" & Rs("minuti_rientro"))
                        pdfFormFields.SetField("fonte", Rs("fonte") & "")
                        pdfFormFields.SetField("cod_convenzione", "")
                        pdfFormFields.SetField("eta1", Rs("eta_primo_guidatore") & "")
                        pdfFormFields.SetField("eta2", Rs("eta_secondo_guidatore") & "")
                        pdfFormFields.SetField("gg", Rs("giorni") & "")
                        pdfFormFields.SetField("gg_to", Rs("giorni_to") & "")
                        pdfFormFields.SetField("cognome", Rs("cognome_conducente") & "")
                        pdfFormFields.SetField("nome", Rs("nome_conducente") & "")
                        pdfFormFields.SetField("email", Rs("mail_conducente") & "")
                        pdfFormFields.SetField("indirizzo", Rs("indirizzo_conducente") & "")
                        pdfFormFields.SetField("nascita", Rs("data_nascita") & "")
                        If (Rs("cod_gruppo_app") & "") <> "" Then
                            pdfFormFields.SetField("gruppo", Rs("cod_gruppo_app"))
                        Else
                            pdfFormFields.SetField("gruppo", Rs("codice_gruppo"))
                        End If
                        pdfFormFields.SetField("nvolo1", Rs("N_VOLOOUT") & "")
                        pdfFormFields.SetField("nvolo2", Rs("N_VOLOPR") & "")
                        pdfFormFields.SetField("riferimento", Rs("rif_to") & "")
                        pdfFormFields.SetField("riftel", Rs("riferimento_telefono") & "")
                        If (Rs("codice_edp") & "") <> "" Then
                            pdfFormFields.SetField("fatturare_a", getNomeDittaFromEdp(Rs("codice_edp")))
                        ElseIf (Rs("id_cliente") & "") <> "" Then
                            pdfFormFields.SetField("fatturare_a", getNomeDittaFromId(Rs("id_cliente")))
                        End If


                        Dim miei_dati As DatiStampaPrenotazione = New DatiStampaPrenotazione
                        With miei_dati

                            'DETTAGLI COSTI ---------------------------------------------------------------------------------------------------------------
                            Dim lblIncluso As Label
                            Dim lblObbligatorio As Label
                            Dim lblInformativa As Label
                            Dim nome_costo As Label
                            Dim costo_scontato As Label
                            Dim chkOldOmaggio As CheckBox
                            Dim chkScegli As CheckBox
                            Dim chkOldScegli As CheckBox
                            Dim a_carico_to As Label

                            Dim prima_informativa As Boolean = True

                            For i = 0 To listPrenotazioniCosti.Items.Count - 1
                                lblIncluso = listPrenotazioniCosti.Items(i).FindControl("lblIncluso")
                                lblObbligatorio = listPrenotazioniCosti.Items(i).FindControl("lblObbligatorio")
                                lblInformativa = listPrenotazioniCosti.Items(i).FindControl("lblInformativa")
                                nome_costo = listPrenotazioniCosti.Items(i).FindControl("nome_costo")
                                costo_scontato = listPrenotazioniCosti.Items(i).FindControl("costo_scontato")
                                chkOldOmaggio = listPrenotazioniCosti.Items(i).FindControl("chkOldOmaggio")
                                chkScegli = listPrenotazioniCosti.Items(i).FindControl("chkScegli")
                                chkOldScegli = listPrenotazioniCosti.Items(i).FindControl("chkOldScegli")
                                a_carico_to = listPrenotazioniCosti.Items(i).FindControl("a_carico_to")

                                If lblIncluso.Visible Or lblObbligatorio.Visible Or lblInformativa.Visible Then
                                    If lblInformativa.Visible And prima_informativa Then
                                        prima_informativa = False
                                        .n_dettaglio_nome = .n_dettaglio_nome & vbCrLf
                                        .n_dettaglio_costo = .n_dettaglio_costo & vbCrLf
                                        .n_dettaglio_omaggio = .n_dettaglio_omaggio & vbCrLf
                                        .n_dettaglio_costo_to = .n_dettaglio_costo_to & vbCrLf
                                    End If

                                    .n_dettaglio_nome = .n_dettaglio_nome & nome_costo.Text & vbCrLf
                                    If costo_scontato.Visible Then
                                        .n_dettaglio_costo = .n_dettaglio_costo & costo_scontato.Text & vbCrLf
                                    Else
                                        .n_dettaglio_costo = .n_dettaglio_costo & vbCrLf
                                    End If

                                    If chkOldOmaggio.Checked Then
                                        .n_dettaglio_omaggio = .n_dettaglio_omaggio & "X" & vbCrLf
                                    Else
                                        .n_dettaglio_omaggio = .n_dettaglio_omaggio & vbCrLf
                                    End If

                                    .n_dettaglio_costo_to = .n_dettaglio_costo_to & vbCrLf
                                ElseIf chkScegli.Visible And chkOldScegli.Checked Then
                                    .n_dettaglio_nome = .n_dettaglio_nome & nome_costo.Text & vbCrLf
                                    .n_dettaglio_costo = .n_dettaglio_costo & costo_scontato.Text & vbCrLf
                                    If chkOldOmaggio.Checked Then
                                        .n_dettaglio_omaggio = .n_dettaglio_omaggio & "X" & vbCrLf
                                    Else
                                        .n_dettaglio_omaggio = .n_dettaglio_omaggio & vbCrLf
                                    End If

                                    .n_dettaglio_costo_to = .n_dettaglio_costo_to & vbCrLf
                                ElseIf Not chkScegli.Visible Then
                                    .n_dettaglio_nome = .n_dettaglio_nome & nome_costo.Text & vbCrLf
                                    If costo_scontato.Visible Then
                                        .n_dettaglio_costo = .n_dettaglio_costo & costo_scontato.Text & vbCrLf
                                    Else
                                        .n_dettaglio_costo = .n_dettaglio_costo & vbCrLf
                                    End If

                                    If tariffa_broker.Text = "1" And a_carico_to.Visible Then
                                        .n_dettaglio_costo_to = .n_dettaglio_costo_to & a_carico_to.Text & vbCrLf
                                    Else
                                        .n_dettaglio_costo_to = .n_dettaglio_costo_to & vbCrLf
                                    End If

                                    .n_dettaglio_omaggio = .n_dettaglio_omaggio & vbCrLf
                                End If
                            Next
                            '------------------------------------------------------------------------------------------------------------------------------

                            pdfFormFields.SetField("dettaglio_nome", .n_dettaglio_nome)
                            pdfFormFields.SetField("dettaglio_costo", .n_dettaglio_costo)
                            pdfFormFields.SetField("dettaglio_omaggio", .n_dettaglio_omaggio)
                            pdfFormFields.SetField("dettaglio_costo_to", .n_dettaglio_costo_to)
                        End With


                    End Using
                End Using
                'myPdfStamper.FormFlattening = True
                'myPdfStamper.Close()
                'myPdfStamper.Dispose()

                'file_stream.Flush()
                'file_stream.Dispose()
                'file_stream.Close()

                myPdfReader.Close()
                'myPdfReader = Nothing


            End If


            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error genera_prenotazioni  : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
        stampa_prenotazione()
    End Sub


    Protected Sub btnApplicaCodiceSconto_Click(sender As Object, e As System.EventArgs) Handles btnApplicaCodiceSconto.Click
        If txtCodiceSconto.Enabled = False Then
            'Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            'Dbc.Open()
            'Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

            'Cmd.ExecuteNonQuery()

            'Cmd.Dispose()
            'Cmd = Nothing
            'Dbc.Close()
            'Dbc.Dispose()
            'Dbc = Nothing
        Else
            If Trim(txtCodiceSconto.Text) = "" Then
                Libreria.genUserMsgBox(Me, "Specificare il codice sconto.")
            End If
        End If
    End Sub

    Protected Function CheckPasswordCC() As Boolean
        Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        conn.Open()

        Dim strQuery As String
        strQuery = "SELECT id FROM operatori WHERE id=" & Request.Cookies("SicilyRentCar")("IdUtente") & " AND password='" & Trim(txtPasswordCC.Text) & "'"

        Dim cmd As New Data.SqlClient.SqlCommand(strQuery, conn)

        Dim test As String = cmd.ExecuteScalar & ""

        If test <> "" Then
            strQuery = "INSERT INTO log_cc (id_utente, id_pagamenti_extra, data, status) VALUES ('" & Request.Cookies("SicilyRentCar")("IdUtente") & "'," & idPagamentoExtra.Text & ", GetDate(),'OK')"
            cmd = New Data.SqlClient.SqlCommand(strQuery, conn)
            cmd.ExecuteNonQuery()

            CheckPasswordCC = True

            txtPasswordCC.Text = ""
        Else
            strQuery = "INSERT INTO log_cc (id_utente, id_pagamenti_extra, data, status) VALUES ('" & Request.Cookies("SicilyRentCar")("IdUtente") & "'," & idPagamentoExtra.Text & ", GetDate(),'PWD ERRATA')"
            cmd = New Data.SqlClient.SqlCommand(strQuery, conn)
            cmd.ExecuteNonQuery()

            CheckPasswordCC = False

            txtPasswordCC.Text = ""
        End If


        cmd.Dispose()
        cmd = Nothing
        conn.Close()
        conn.Dispose()
        conn = Nothing
    End Function


    Protected Sub btnVisualizzaCC_Click(sender As Object, e As System.EventArgs) Handles btnVisualizzaCC.Click
        txtPOS_Carta.Focus()

        If Trim(txtPasswordCC.Text) = "" Then
            Libreria.genUserMsgBox(Me, "Specificare la password del proprio account per poter visualizzare il numero della carta di credito.")
        ElseIf Not CheckPasswordCC() Then
            Libreria.genUserMsgBox(Me, "Attenzione: password errata. Il tentativo di visualizzare il numero della carta di credito è stato registrato.")
        Else
            salva_logCarta(lblNumPrenotazione.Text)

            Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            conn.Open()

            Dim strQuery As String
            strQuery = "SELECT titolo FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE ID_CTR='" & idPagamentoExtra.Text & "'"

            Dim cmd As New Data.SqlClient.SqlCommand(strQuery, conn)

            Dim test As String = cmd.ExecuteScalar & ""

            With New security
                'txtPOS_Carta.Text = .decryptString(test)
                txtPOS_Carta.Text = decripta(test, 37)
            End With


            cmd.Dispose()
            cmd = Nothing
            conn.Close()
            conn.Dispose()
            conn = Nothing

            btnVisualizzaCC.Visible = False
            lblPasswordCC.Visible = False
            txtPasswordCC.Visible = False
        End If
    End Sub


    'Tony
    Protected Sub salva_logCarta(ByVal NumContratto As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO utenti_clog (id_utente, nominativo, data, pagina, ip) VALUES ('" & Request.Cookies("SicilyRentCar")("IdUtente") & "',(SELECT ISNULL(cognome,'') + ' ' + ISNULL(nome,'') FROM operatori WHERE id='" & Request.Cookies("SicilyRentCar")("IdUtente") & "'),GetDate(),'Visualizzata Carta Credito - " & NumContratto & "','" & Replace(Request.UserHostAddress(), "'", "''") & "')", Dbc)
        'Response.Write(Cmd.CommandText)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub btnModificaDataPagamento_Click(sender As Object, e As System.EventArgs) Handles btnModificaDataPagamento.Click
        If Trim(txtPOS_DataOperazione.Text) = "" Then
            Libreria.genUserMsgBox(Me, "Specificare la data dell'operazione")
        Else
            Try
                Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                conn.Open()

                Dim data_pagamento As String = getDataDb_con_orario(txtPOS_DataOperazione.Text.Replace(".", ":"))
                Dim data_pagamento_no_ora As String = getDataDb_senza_orario(txtPOS_DataOperazione.Text.Replace(".", ":"))

                Dim strQuery As String
                strQuery = "UPDATE PAGAMENTI_EXTRA SET DATA=convert(datetime,'" & data_pagamento_no_ora & "',102), DATA_OPERAZIONE=convert(datetime,'" & data_pagamento & "',102) WHERE  ID_CTR=" & idPagamentoExtra.Text


                'inserito campo importo per la modifica 27.04.2022
                Dim importo As String = txt_importo.Text
                If importo = "" Then
                    strQuery = "UPDATE PAGAMENTI_EXTRA SET per_importo=NULL, DATA=convert(datetime,'" & data_pagamento_no_ora & "',102), DATA_OPERAZIONE=convert(datetime,'" & data_pagamento & "',102) WHERE ID_CTR=" & idPagamentoExtra.Text
                Else
                    importo = Trim(importo)
                    importo = importo.Replace(".", "")
                    importo = importo.Replace(",", ".")
                    strQuery = "UPDATE PAGAMENTI_EXTRA SET per_importo=" & importo & ", DATA=convert(datetime,'" & data_pagamento_no_ora & "',102), DATA_OPERAZIONE=convert(datetime,'" & data_pagamento & "',102) WHERE ID_CTR=" & idPagamentoExtra.Text
                End If
                'end modifica stringa query con inserimento del campo importo 27.04.2022

                Dim cmd As New Data.SqlClient.SqlCommand(strQuery, conn)

                cmd.ExecuteNonQuery()

                Libreria.genUserMsgBox(Me, "Pagamento modificato correttamente.")

                cmd.Dispose()
                cmd = Nothing
                conn.Close()
                conn.Dispose()
                conn = Nothing

                listPagamenti.DataBind()
                txtPOS_DataOperazione.Focus()

                'imposta pannello pagamento 27.04.2022
                ImpostaPannelloPagamento(txtNumPrenotazione.Text)





            Catch ex As Exception
                Libreria.genUserMsgBox(Me, "Si è verificato un errore: controllare la data e l'ora specificati.")
            End Try



        End If
    End Sub




    Protected Sub btnEliminaPagamento_Click(sender As Object, e As System.EventArgs) Handles btnEliminaPagamento.Click
        Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        conn.Open()

        Dim strQuery As String
        strQuery = "DELETE FROM PAGAMENTI_EXTRA WHERE ID_CTR=" & idPagamentoExtra.Text

        Dim cmd As New Data.SqlClient.SqlCommand(strQuery, conn)

        Try
            cmd.ExecuteNonQuery()

            listPagamenti.DataBind()

            txtPOS_Carta.Text = ""
            idPagamentoExtra.Text = ""
            riga_pagamento_pos.Visible = False

            ImpostaPannelloPagamento(txtNumPrenotazione.Text)


            Libreria.genUserMsgBox(Me, "Pagamento eliminato correttamente.")
        Catch ex As Exception
            Libreria.genUserMsgBox(Me, "Impossibile eliminare questo pagamento")
        End Try


        cmd.Dispose()
        cmd = Nothing
        conn.Close()
        conn.Dispose()
        conn = Nothing
    End Sub

    Protected Sub btnAzzeraPagamento_Click(sender As Object, e As System.EventArgs) Handles btnAzzeraPagamento.Click
        Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        conn.Open()

        Dim strQuery As String
        strQuery = "UPDATE PAGAMENTI_EXTRA SET PER_IMPORTO=0 WHERE ID_CTR=" & idPagamentoExtra.Text

        Dim cmd As New Data.SqlClient.SqlCommand(strQuery, conn)

        Try
            cmd.ExecuteNonQuery()

            listPagamenti.DataBind()

            txtPOS_Carta.Text = ""
            idPagamentoExtra.Text = ""
            riga_pagamento_pos.Visible = False

            ImpostaPannelloPagamento(txtNumPrenotazione.Text)


            Libreria.genUserMsgBox(Me, "Pagamento azzerato correttamente.")
        Catch ex As Exception
            Libreria.genUserMsgBox(Me, "Impossibile azzerare questo pagamento")
        End Try


        cmd.Dispose()
        cmd = Nothing
        conn.Close()
        conn.Dispose()
        conn = Nothing
    End Sub

    Protected Sub btnIncassoWeb_Click(sender As Object, e As System.EventArgs) Handles btnIncassoWeb.Click
        Dim dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        dbc.Open()

        Dim strSql As String = "UPDATE PAGAMENTI_EXTRA SET id_modpag=NULL, id_tippag='1011098650', id_pos_funzioni_ares='4', cassa='0', intestatario='', scadenza='', " &
            "nr_aut='0000023', tipsegno='1', note=NULL, id_operatore_ares=NULL, utecre='web', nr_batch='000018', contabilizzato='0',  TRANSATION_TYPE='MAG', " &
            "CARD_TYPE='2', acquire_id='00000000002', action_code='000', tentativiPOS='1' WHERE ID_CTR=" & idPagamentoExtra.Text

        Dim cmd As New Data.SqlClient.SqlCommand(strSql, dbc)
        cmd.ExecuteNonQuery()

        listPagamenti.DataBind()

        txtPOS_Carta.Text = ""
        idPagamentoExtra.Text = ""
        riga_pagamento_pos.Visible = False

        ImpostaPannelloPagamento(txtNumPrenotazione.Text)

        Libreria.genUserMsgBox(Me, "Pagamento modificato correttamente.")

        cmd.Dispose()
        cmd = Nothing
        dbc.Close()
        dbc.Dispose()
        dbc = Nothing
    End Sub

    Protected Sub btnIncassoBroker_Click(sender As Object, e As System.EventArgs) Handles btnIncassoBroker.Click
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE pagamenti_extra SET pagamento_broker=1 WHERE ID_CTR=" & idPagamentoExtra.Text, Dbc)

        Cmd.ExecuteNonQuery()

        listPagamenti.DataBind()

        txtPOS_Carta.Text = ""
        idPagamentoExtra.Text = ""
        riga_pagamento_pos.Visible = False

        ImpostaPannelloPagamento(txtNumPrenotazione.Text)

        Libreria.genUserMsgBox(Me, "Pagamento modificato correttamente.")

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub upload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles upload.Click

        'Protected Sub upload_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles upload.Click
        If dropNuovoAllegato.SelectedValue = "0" Then
            Libreria.genUserMsgBox(Me, "Selezionare una tipologia di allegato.")
        ElseIf Not UploadAllegati.HasFile Then
            Libreria.genUserMsgBox(Me, "Selezionare un file da allegare.")
        Else

            'SVILUPPO
            'Dim filePath As String = "C:/inetpub/sviluppoares.sicilyrentcar.it/allegati_pren_cnt/" & txtNumPrenotazione.Text & "/"
            'PRODUZIONE
            'Dim filePath As String = "E:/siti_internet/ares.sicilyrentcar.it/htdocs/allegati_pren_cnt/" & txtNumPrenotazione.Text & "/"
            Dim filePath As String = Server.MapPath("\allegati_pren_cnt/" & txtNumPrenotazione.Text & "/") ' x Tutti path assouluto 31.12.2020
            'FORMAZIONE
            'Dim filePath As String = "C:/siti_internet/src-formazione.entermed.it/htdocs/allegati_pren_cnt/" & txtNumPrenotazione.Text & "/"

            Dim my_path As String = "/allegati_pren_cnt/" & txtNumPrenotazione.Text & "/"

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)

            Dim siglaAllegato As String = funzioni_comuni.getSiglaAllegato(dropNuovoAllegato.SelectedValue.ToString)


            If UploadAllegati.HasFile Then
                Dim nome_file As String = UploadAllegati.FileName

                Dim estensione As String = funzioni_comuni.GetEstensioneFile(nome_file)

                Dim nome_file_operatore As String = nome_file

                nome_file = siglaAllegato & "_" & txtNumPrenotazione.Text & "." & estensione


                Dim sqlall As String = "INSERT INTO contratti_prenotazioni_allegati (nome_file, cartella,num_pren, id_cnt_pren_allegati_tipo, nome_file_operatore,id_operatore) " &
                                                         " VALUES ('" & nome_file & "','" & my_path & "','" & txtNumPrenotazione.Text & "'," & dropNuovoAllegato.SelectedValue & ",'" & nome_file_operatore & "'," & Request.Cookies("SicilyRentCar")("idUtente") & ")"



                If Directory.Exists(filePath) = False Then
                    ' ...Creo la cartella al percorso specificato
                    Directory.CreateDirectory(filePath)
                End If

                'filePath += nome_file
                'se file esistente rinomina quello che deve essere caricato
                Dim x As Integer = 0
                While File.Exists(filePath & nome_file)

                    x += 1

                    'lorinomina
                    nome_file = siglaAllegato & "_" & txtNumPrenotazione.Text & "_" & x.ToString & "." & estensione
                    sqlall = "INSERT INTO contratti_prenotazioni_allegati (nome_file, cartella, num_pren, id_cnt_pren_allegati_tipo, nome_file_operatore,id_operatore) " &
                                                         " VALUES ('" & nome_file & "','" & my_path & "','" & txtNumPrenotazione.Text & "'," & dropNuovoAllegato.SelectedValue & ",'" & nome_file_operatore & "'," & Request.Cookies("SicilyRentCar")("idUtente") & ")"

                    If x = 20 Then
                        Exit While
                    End If

                End While

                Dim Cmd As New Data.SqlClient.SqlCommand(sqlall, Dbc)


                If File.Exists(filePath & nome_file) Then
                    Libreria.genUserMsgBox(Page, "File Esistente.")
                Else
                    Try

                        filePath += nome_file

                        UploadAllegati.SaveAs(filePath)
                        Dbc.Open()

                        Cmd.ExecuteNonQuery()
                        Libreria.genUserMsgBox(Page, "File caricato correttamente.")

                        sqlAllegati.SelectCommand = "SELECT id_allegato, nome_file, cartella, (cartella + nome_file) as my_path, descrizione, data_operazione, id_operatore FROM contratti_prenotazioni_allegati WITH(NOLOCK) " &
                                        "INNER JOIN contratti_prenotazioni_allegati_tipo WITH(NOLOCK) ON contratti_prenotazioni_allegati.id_cnt_pren_allegati_tipo=contratti_prenotazioni_allegati_tipo.id_cnt_pren_allegati_tipo WHERE num_pren='" & lblNumPrenotazione.Text & "'"
                        'dataListAllegati.DataBind()
                        ListViewAllegati.DataBind()

                        dropNuovoAllegato.SelectedValue = "0"
                    Catch ex As Exception
                        Libreria.genUserMsgBox(Me, "Si è verificato un errore. Si prega di riprovare.")
                    End Try


                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing

                End If
            End If
        End If









    End Sub

    Protected Sub dataListAllegati_ItemCommand(sender As Object, e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles dataListAllegati.ItemCommand
        If e.CommandName = "elimina" Then
            Dim id_allegato As Label = e.Item.FindControl("id_allegato")
            Dim cartella As Label = e.Item.FindControl("cartella")
            Dim nome_file As Label = e.Item.FindControl("nome_file")

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim sqlStr As String = "DELETE FROM contratti_prenotazioni_allegati WHERE id_allegato='" & id_allegato.Text & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc = Nothing

            'SVILUPPO
            'Dim filePath As String = "C:/inetpub/sviluppoares.sicilyrentcar.it" & cartella.Text & nome_file.Text
            'PRODUZIONE
            'Dim filePath As String = "E:/siti_internet/ares.sicilyrentcar.it/htdocs" & cartella.Text & nome_file.Text 'ultimo su Entermed
            'FORMAZIONE
            'Dim filePath As String = "C:/siti_internet/src-formazione.entermed.it/htdocs" & cartella.Text & nome_file.Text

            'aggiunto 08.01.2021
            Dim filePath As String = Server.MapPath("\" & cartella.Text & nome_file.Text)        'Path aggiunto 08.01.2021

            'Elimina Allegati
            Try
                Dim msgfile As String = ""
                If File.Exists(filePath) Then   'inserito il 08.01.2021
                    File.Delete(filePath)
                Else

                End If

                sqlAllegati.SelectCommand = "SELECT id_allegato, nome_file, cartella, (cartella + nome_file) as my_path, descrizione, data_operazione, id_operatore FROM contratti_prenotazioni_allegati WITH(NOLOCK) " &
                                        "INNER JOIN contratti_prenotazioni_allegati_tipo WITH(NOLOCK) ON contratti_prenotazioni_allegati.id_cnt_pren_allegati_tipo=contratti_prenotazioni_allegati_tipo.id_cnt_pren_allegati_tipo WHERE num_pren='" & lblNumPrenotazione.Text & "'"
                dataListAllegati.DataBind()
                Libreria.genUserMsgBox(Me, "Allegato eliminato correttamente.")
            Catch ex As Exception
                Libreria.genUserMsgBox(Me, "Si è verificato un errore. Si prega di riprovare.")
            End Try
        End If
    End Sub


    Protected Sub ListViewAllegati_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListViewAllegati.ItemCommand
        'inserito per nuova list allegati 19.04.2022
        Dim id_allegato As Label = e.Item.FindControl("lblIdAllegato")
        Dim cartella As Label = e.Item.FindControl("lblPercorsoFile")
        Dim nome_file As Label = e.Item.FindControl("lblNomeFile")

        Dim newPercorso As String = ""

        If e.CommandName = "SelezionaAllegato" Then
            'newPercorso = newPercorso & NomeFile.Text 'aggiungo il nome del file al precorso

            newPercorso = cartella.Text

            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('" & newPercorso & "','')", True)
                End If
            End If
        End If



        If e.CommandName = "EliminaAllegato" Then

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim sqlStr As String = "DELETE FROM contratti_prenotazioni_allegati WHERE id_allegato='" & id_allegato.Text & "'"

            Try
                Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteNonQuery()
                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc = Nothing

            Catch ex As Exception

            End Try

            'SVILUPPO
            'Dim filePath As String = "C:/inetpub/sviluppoares.sicilyrentcar.it" & cartella.Text & nome_file.Text
            'PRODUZIONE
            'Dim filePath As String = "E:/siti_internet/ares.sicilyrentcar.it/htdocs" & cartella.Text & nome_file.Text 'ultimo su Entermed
            'FORMAZIONE
            'Dim filePath As String = "C:/siti_internet/src-formazione.entermed.it/htdocs" & cartella.Text & nome_file.Text

            'aggiunto 08.01.2021
            Dim filePath As String = Server.MapPath("\" & cartella.Text)        'Path aggiunto 08.01.2021

            'Elimina Allegati
            Try
                Dim msgfile As String = ""
                If File.Exists(filePath) Then   'inserito il 08.01.2021
                    File.Delete(filePath)
                Else

                End If

                sqlAllegati.SelectCommand = "SELECT id_allegato, nome_file, cartella, (cartella + nome_file) as my_path, descrizione, data_operazione, id_operatore FROM contratti_prenotazioni_allegati WITH(NOLOCK) " &
                                    "INNER JOIN contratti_prenotazioni_allegati_tipo WITH(NOLOCK) ON contratti_prenotazioni_allegati.id_cnt_pren_allegati_tipo=contratti_prenotazioni_allegati_tipo.id_cnt_pren_allegati_tipo WHERE num_pren='" & lblNumPrenotazione.Text & "'"
                'dataListAllegati.DataBind()
                ListViewAllegati.DataBind()

                Libreria.genUserMsgBox(Me, "Allegato eliminato correttamente.")
            Catch ex As Exception
                Libreria.genUserMsgBox(Me, "Si è verificato un errore. Si prega di riprovare.")
            End Try

        End If




    End Sub




    Private Sub btnAggiungiExtra_Command(sender As Object, e As CommandEventArgs) Handles btnAggiungiExtra.Command

    End Sub

    'Tony 27/10/2022
    Protected Function ImportiCaricoCliente(ByVal IdPren As String) As String
        Dim ris As String = ""

        Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc2.Open()

        Dim sqlStr As String = "SELECT SUM(valore_costo) AS somma from prenotazioni_costi WHERE (id_documento = '" & IdPren & "') AND (selezionato = 1) and id_a_carico_di = 2"
        'Response.Write(sqlStr & "<br>")

        Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
        Dim Rs1 As Data.SqlClient.SqlDataReader
        Rs1 = Cmd1.ExecuteReader()

        If Rs1.HasRows Then
            Rs1.Read()
            If Rs1("somma") & "" <> "" Then
                ImportiCaricoCliente = FormatNumber(CDbl(Rs1("somma")), 2)
            Else
                ImportiCaricoCliente = "0,00"
            End If

        End If

        Rs1.Close()
        Cmd1.Dispose()
        Dbc2.Close()
        Rs1 = Nothing
        Cmd1 = Nothing
        Dbc2 = Nothing
    End Function
    'FINE Tony



    Protected Sub ListViewAllegati_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles ListViewAllegati.ItemDataBound
        Dim IdAllegato As Label = e.Item.FindControl("lblIdAllegato")
        Dim Operatore As Label = e.Item.FindControl("lbl_operatore")

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT nome,cognome FROM operatori WITH(NOLOCK) WHERE id=" & Operatore.Text, Dbc)
            'Response.Write(Cmd.CommandText & "<br><br>")
            'Response.End()
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            If Rs.HasRows Then
                Do While Rs.Read
                    Operatore.Text = Rs("nome") & " " & Rs("Cognome")
                Loop
            Else
                Operatore.Text = " "
            End If

            Rs.Close()
            Dbc.Close()
            Rs = Nothing
            Dbc = Nothing
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub dropStazionePickUp_SelectedIndexChanged(sender As Object, e As EventArgs)

        '#Aggiunto Salvo 20.02.2023
        'nel caso di cambio stazione di Pick Up

        Dim oldStazione As String = dropStazionePickUp.SelectedValue

        dropTariffeGeneriche.Enabled = True
        dropTariffeParticolari.Enabled = True


        setQueryTariffePossibili_NEW(0)


        '@end salvo


    End Sub
End Class
