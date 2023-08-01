Imports System.Data
Imports System.Runtime.InteropServices.ComTypes
Imports funzioni_comuni
Imports Libreria
Imports sql

Partial Class commissioni_operatore
    Inherits System.Web.UI.Page
    'Protected PageBody As HtmlGenericControl

    Dim TotCassaPeriodoSel As Double
    Dim TotCassaMeseAnnoCur As Double
    Dim TotCassaMeseAnnoPrev As Double

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        Dim Lang As String = "it-IT"
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(Lang)
        If Not Page.IsPostBack Then
            'If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Commissioni) = "1" Then
            '    Response.Redirect("default.aspx")
            'End If

            Dim sqlq As String = "SELECT [id], [descrizione] FROM [condizioni_elementi] where id='100' or id='170' or id='223' ORDER BY [descrizione]"   'solo x test visualizza gli accessori 223, 170 e 100

            sqlq = "SELECT [id], [descrizione] FROM [condizioni_elementi] where id<>'176' and id<>'177' and id<>'131'  and id<>'98' ORDER BY [descrizione]" 'Produzione
            sqlAccessori.SelectCommand = sqlq
            checkAccessori.DataBind()
            For x = 0 To checkAccessori.Items.Count - 1
                checkAccessori.Items(x).Selected = False         'su apertura selezionato nessuno
            Next


        End If

        'PageBody.Attributes.Add("bgcolor", "#c8c8c6")
        'tr_ck_accessori.Visible = False        'riga accessori


    End Sub

    Protected Function GetTotaliCassa(dataDa As String, dataA As String, id_stazione As String, PeriodoSel As Integer) As Double


        'periodSel = 0 : range data
        'periodSel = 1 : mese/anno corrente
        'periodSel = 2 : mese/anno precedente

        Dim ris As Double = 0

        dataDa = funzioni_comuni.GetDataSql(dataDa, 0)
        dataA = funzioni_comuni.GetDataSql(dataA, 59)


        Dim mesePrev As String, annoCur As String
        Dim meseCur As String, annoPrev As String

        annoCur = Year(dataDa)
        meseCur = Month(dataDa)


        Dim sqlstr As String = "SELECT pe.*, tp.TIPO_PAGA, tp.MOV_CASSA  FROM [PAGAMENTI_EXTRA] pe WITH(NOLOCK) LEFT JOIN TIP_PAG tp WITH(NOLOCK) ON pe.ID_TIPPAG = tp.ID_TIPPag WHERE 1 = 1 "
        sqlstr += "And pe.ID_STAZIONE = '" & id_stazione & "' "
        sqlstr += "And pe.data >= CONVERT(DATETIME,'" & dataDa & "',102) AND pe.data < CONVERT(DATETIME,'" & dataA & "',102)"



        If PeriodoSel = 1 Then  'mese intero precedente /anno cur

            If CInt(meseCur) = 1 Then   'se gennaio calcola dicembre dell'anno precedente
                meseCur = 12
                annoCur = CInt(annoCur) - 1
            Else
                meseCur = CInt(meseCur) - 1
            End If

            sqlstr = "SELECT pe.*, tp.TIPO_PAGA, tp.MOV_CASSA  FROM [PAGAMENTI_EXTRA] pe WITH(NOLOCK) LEFT JOIN TIP_PAG tp WITH(NOLOCK) ON pe.ID_TIPPAG = tp.ID_TIPPag WHERE 1 = 1 "
            sqlstr += "And pe.ID_STAZIONE = '" & id_stazione & "' "
            sqlstr += "And month(pe.data) = '" & meseCur & "' AND year(pe.data) = '" & annoCur & "' "


        ElseIf PeriodoSel = 2 Then 'stesso mese / anno prec

            annoCur = Year(dataDa)
            meseCur = Month(dataDa)
            annoCur = CInt(annoCur) - 1

            sqlstr = "SELECT pe.*, tp.TIPO_PAGA, tp.MOV_CASSA  FROM [PAGAMENTI_EXTRA] pe WITH(NOLOCK) LEFT JOIN TIP_PAG tp WITH(NOLOCK) ON pe.ID_TIPPAG = tp.ID_TIPPag WHERE 1 = 1 "
            sqlstr += "And pe.ID_STAZIONE = '" & id_stazione & "' "
            sqlstr += "And month(pe.data) = '" & meseCur & "' AND year(pe.data) = '" & annoCur & "' "

        End If

        Dim num_incassi As Integer = 0
        Dim incassi As Double = 0
        Dim num_incassi_bonifico As Integer = 0
        Dim incassi_bonifico As Double = 0
        Dim num_depositi_cash As Integer = 0
        Dim depositi_cash As Double = 0
        Dim num_depositi_bancomat As Integer = 0
        Dim depositi_bancomat As Double = 0
        Dim num_depositi_bonifico As Integer = 0
        Dim depositi_bonifico As Double = 0
        Dim num_rimborsi_cash As Integer = 0
        Dim rimborsi_cash As Double = 0
        Dim num_rimborsi_pos = 0
        Dim rimborsi_pos As Double = 0
        Dim num_rimborsi_bonifico = 0
        Dim rimborsi_bonifico As Double = 0
        Dim num_busta_petty_cash As Integer = 0
        Dim busta_petty_cash As Double = 0
        Dim num_depositi_pos As Integer = 0
        Dim depositi_pos As Double = 0
        Dim num_carte_credito As Integer = 0
        Dim carte_credito As Double = 0
        Dim num_carte_credito_incasso As Integer = 0
        Dim carte_credito_incasso As Double = 0
        Dim num_full_credit As Integer = 0
        Dim full_credit As Double = 0
        Dim num_complimentary As Integer = 0
        Dim complimentary As Double = 0
        Dim num_incassi_extra As Integer = 0
        Dim incassi_extra As Double = 0
        Dim num_versamenti As Integer = 0
        Dim versamenti As Double = 0
        Dim num_prelievi As Integer = 0
        Dim prelievi As Double = 0
        Dim num_incassi_web As Integer = 0
        Dim incassi_web As Double = 0
        Dim num_incassi_pos As Integer = 0
        Dim incassi_pos As Double = 0
        Dim num_rimborsi_pos_fatt As Integer = 0
        Dim rimborsi_pos_fatt As Double = 0
        Dim num_voucher As Integer = 0
        Dim voucher As Double = 0
        Dim num_abbuoni_fatt As Integer = 0
        Dim abbuoni_fatt As Double = 0


        Try

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlstr, Dbc)
                    'HttpContext.Current.Trace.Write("Parametro: " & Cmd.CommandText)
                    Dbc.Open()
                    Using Rs = Cmd.ExecuteReader
                        Do While Rs.Read
                            Select Case Rs("ID_TIPPag")
                                Case enum_tipo_pagamento_p1000.CH_PAGAMENTO_CASH
                                    If Rs("id_modpag") <> "10" Then
                                        num_incassi += 1
                                        incassi += Rs("PER_IMPORTO")
                                    ElseIf Rs("id_modpag") = "10" Then
                                        num_incassi_bonifico += 1
                                        incassi_bonifico += Rs("PER_IMPORTO")
                                    End If
                                Case enum_tipo_pagamento_p1000.DE_DEPOSITO_SU_RA
                                    If Rs("id_modpag") <> "9" And Rs("id_modpag") <> "10" Then
                                        num_depositi_cash += 1
                                        depositi_cash += Rs("PER_IMPORTO")
                                    ElseIf Rs("id_modpag") = "9" Then
                                        num_depositi_bancomat += 1
                                        depositi_bancomat += Rs("PER_IMPORTO")
                                    ElseIf Rs("id_modpag") = "10" Then
                                        num_depositi_bonifico += 1
                                        depositi_bonifico += Rs("PER_IMPORTO")
                                    End If
                                Case enum_tipo_pagamento_p1000.CA_RIMBORSO_SU_RA, enum_tipo_pagamento_p1000.MCR_PAGAMENTO_CASH
                                    If Rs("id_modpag") <> "9" And Rs("id_modpag") <> "10" And Rs("id_modpag") <> "11" Then
                                        num_rimborsi_cash += 1
                                        rimborsi_cash += Rs("PER_IMPORTO")
                                    ElseIf Rs("id_modpag") = "9" Then
                                        'BANCOMAT
                                        If Rs("ID_TIPPag") = enum_tipo_pagamento_p1000.CA_RIMBORSO_SU_RA Then
                                            num_rimborsi_pos += 1
                                            rimborsi_pos += Rs("PER_IMPORTO")
                                        ElseIf Rs("ID_TIPPag") = enum_tipo_pagamento_p1000.MCR_PAGAMENTO_CASH Then
                                            num_rimborsi_bonifico += 1
                                            rimborsi_bonifico += Rs("PER_IMPORTO")
                                        End If
                                    ElseIf Rs("id_modpag") = "10" Then
                                        'BONIFICO
                                        If Rs("ID_TIPPag") = enum_tipo_pagamento_p1000.CA_RIMBORSO_SU_RA Then
                                            num_rimborsi_pos += 1
                                            rimborsi_pos += Rs("PER_IMPORTO")
                                        ElseIf Rs("ID_TIPPag") = enum_tipo_pagamento_p1000.MCR_PAGAMENTO_CASH Then
                                            num_rimborsi_bonifico += 1
                                            rimborsi_bonifico += Rs("PER_IMPORTO")
                                        End If
                                    ElseIf Rs("id_modpag") = "11" Then
                                        'STORNO
                                        If Rs("ID_TIPPag") = enum_tipo_pagamento_p1000.CA_RIMBORSO_SU_RA Then
                                            num_rimborsi_pos += 1
                                            rimborsi_pos += Rs("PER_IMPORTO")
                                        ElseIf Rs("ID_TIPPag") = enum_tipo_pagamento_p1000.MCR_PAGAMENTO_CASH Then
                                            num_rimborsi_bonifico += 1
                                            rimborsi_bonifico += Rs("PER_IMPORTO")
                                        End If
                                    End If
                                Case enum_tipo_pagamento_p1000.PC_PETTY_CASH
                                    num_busta_petty_cash += 1
                                    busta_petty_cash += Rs("PER_IMPORTO")
                                Case enum_tipo_pagamento_p1000.CC_C_CREDITO_AUTORIZZAZIONE
                                    num_depositi_pos += 1    'NELLA CASSA A VIDEO LE AUTORIZZAZIONI TELEFONICHE VENGONO AGGIUNTE A TUTTE LE PREAUTORIZZAZIONI
                                    depositi_pos += Rs("PER_IMPORTO")
                                    num_carte_credito += 1   'SERVE PER LA STAMPA CARTACEA VISTO CHE IL DATO CC TELEFONICO E' RIPORTATO A PARTE
                                    carte_credito += Rs("PER_IMPORTO")
                                Case enum_tipo_pagamento_p1000.CI_C_CREDITO_INCASSO
                                    num_carte_credito_incasso += 1
                                    carte_credito_incasso += Rs("PER_IMPORTO")
                                Case enum_tipo_pagamento_p1000.FC_FULL_CREDIT
                                    num_full_credit += 1
                                    full_credit += Rs("PER_IMPORTO")
                                Case enum_tipo_pagamento_p1000.COMPLIMENTARY
                                    num_complimentary += 1
                                    complimentary += Rs("PER_IMPORTO")
                                Case Else
                                    Select Case Rs("MOV_CASSA")
                                        Case "EN"
                                            Select Case Rs("TIPO_PAGA")
                                                Case "PR"
                                                'num_prelievi += 1
                                                'prelievi += Rs("PER_IMPORTO")
                                                Case "IE"
                                                    num_incassi_extra += 1
                                                    incassi_extra += Rs("PER_IMPORTO")
                                                Case "MC"
                                                    'num_incassi_extra += 1
                                                    'incassi_extra += Rs("PER_IMPORTO")
                                                    'num_prelievi += 1
                                                    'prelievi += Rs("PER_IMPORTO")
                                                Case Else
                                                    Err.Raise(1001, Nothing, "TIPO_PAGA non previsto: " & Rs("MOV_CASSA") & " - " & Rs("TIPO_PAGA"))
                                            End Select
                                        Case "US"
                                            Select Case Rs("TIPO_PAGA")
                                                Case "VS"
                                                    num_versamenti += 1
                                                    versamenti += Rs("PER_IMPORTO")
                                                Case "PR"
                                                    num_prelievi += 1
                                                    prelievi += Rs("PER_IMPORTO")
                                                Case Else
                                                    Err.Raise(1001, Nothing, "TIPO_PAGA non previsto: " & Rs("MOV_CASSA") & " - " & Rs("TIPO_PAGA"))
                                            End Select
                                        Case "PO"
                                            Select Case Rs("TIPO_PAGA")
                                                Case "VS"
                                                    num_versamenti += 1
                                                    versamenti += Rs("PER_IMPORTO")
                                                Case "DE"
                                                    num_depositi_pos += 1
                                                    depositi_pos += Rs("PER_IMPORTO")
                                                Case "IN"
                                                    If (Rs("utecre") & "") = "web" Then
                                                        num_incassi_web += 1
                                                        incassi_web += Rs("PER_IMPORTO")
                                                    Else
                                                        num_incassi_pos += 1
                                                        incassi_pos += Rs("PER_IMPORTO")
                                                    End If

                                                Case "RB"
                                                'num_rimborsi_pos += 1
                                                'rimborsi_pos += Rs("PER_IMPORTO")
                                                Case "RP"
                                                    num_rimborsi_pos_fatt += 1
                                                    rimborsi_pos_fatt += Rs("PER_IMPORTO")
                                                Case Else
                                                    Err.Raise(1001, Nothing, "TIPO_PAGA non previsto: " & Rs("MOV_CASSA") & " - " & Rs("TIPO_PAGA"))
                                            End Select
                                        Case "FT"
                                            Select Case Rs("TIPO_PAGA")
                                                Case "VO"
                                                    num_voucher += 1
                                                    voucher += Rs("PER_IMPORTO")
                                                Case "AB"
                                                    num_abbuoni_fatt += 1
                                                    abbuoni_fatt += Rs("PER_IMPORTO")
                                                Case Else
                                                    Err.Raise(1001, Nothing, "TIPO_PAGA non previsto: " & Rs("MOV_CASSA") & " - " & Rs("TIPO_PAGA"))
                                            End Select
                                        Case Else
                                            Err.Raise(1001, Nothing, "MOV_CASSA non previsto: " & Rs("MOV_CASSA") & " - " & Rs("TIPO_PAGA"))
                                    End Select
                            End Select
                        Loop
                    End Using
                End Using
            End Using


            Dim totale_entrate_cassa As Double = (incassi) + (prelievi) + (incassi_extra) + (versamenti)

            Dim totale_entrate As Double = (incassi) + (incassi_pos) + (incassi_web) + (prelievi) + (incassi_extra)

            Dim incassi_report_operatore As Double = incassi + incassi_pos + incassi_bonifico + incassi_web

            incassi_report_operatore = incassi_report_operatore

            ris = incassi_report_operatore


        Catch ex As Exception

        End Try



        Return ris





    End Function





    Protected Function condizione_where() As String

        'Dim da_data As String = funzioni_comuni.getDataDb_senza_orario(txtCercaDropOffDa.Text)
        'Dim a_data As String = funzioni_comuni.getDataDb_con_orario(txtCercaDropOffA.Text & " 23:59:59")

        Dim da_data As String = funzioni_comuni.GetDataSql(txtCercaDropOffDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaDropOffDa.Text)
        Dim a_data As String = funzioni_comuni.GetDataSql(txtCercaDropOffA.Text, 59) ' funzioni_comuni.getDataDb_con_orario(txtCercaDropOffA.Text & " 23:59:59")

        Dim da_dataPKUP As String = funzioni_comuni.GetDataSql(txtCercaPickUpDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaDropOffDa.Text)
        Dim a_dataPKUP As String = funzioni_comuni.GetDataSql(txtCercaPickUpA.Text, 59) ' funzioni_comuni.getDataDb_con_orario(txtCercaDropOffA.Text & " 23:59:59")


        'condizione_where = " AND (contratti.data_rientro BETWEEN '" & da_data & "' AND '" & a_data & "') "

        'verifica quali campi considerare
        If txtCercaDropOffDa.Text <> "" And txtCercaDropOffA.Text <> "" Then
            condizione_where = " AND (contratti.data_rientro BETWEEN Convert(DateTime, '" & da_data & "', 102) AND CONVERT(DATETIME, '" & a_data & "', 102)) "
        End If

        If txtCercaPickUpDa.Text <> "" And txtCercaPickUpA.Text <> "" Then

            If condizione_where = "" Then
                condizione_where = " AND (contratti.data_uscita BETWEEN Convert(DateTime, '" & da_dataPKUP & "', 102) AND CONVERT(DATETIME, '" & a_dataPKUP & "', 102)) "
            Else
                condizione_where += " AND (contratti.data_uscita BETWEEN Convert(DateTime, '" & da_dataPKUP & "', 102) AND CONVERT(DATETIME, '" & a_dataPKUP & "', 102)) "
            End If

        End If


    End Function

    Protected Function controlla_filtri() As String
        Dim messaggio As String = ""

        If dropCercaOperatori.SelectedValue = "0" Then
            messaggio = messaggio & "- Selezionare un operatore" & vbCrLf
        End If

        If txtPercentualeImponibile.Text = "" Then
            messaggio = messaggio & "- Specificare la percentuale sull'imponibile" & vbCrLf
        End If

        If txtPercentualeCommissione.Text = "" Then
            messaggio = messaggio & "- Specificare la percentuale della commmissione (da applicare sulla percentuale dell'imponibile)" & vbCrLf
        End If

        If txtCercaDropOffA.Text = "" And txtCercaDropOffDa.Text = "" And txtCercaPickUpDa.Text = "" And txtCercaPickUpA.Text = "" Then
            messaggio = messaggio & "- Specificare un'intervallo di date di drop off o pick up per effettuare il calcolo delle commissioni " & vbCrLf
        End If

        If txtCercaDropOffA.Text <> "" Or txtCercaDropOffDa.Text <> "" Then
            If txtCercaDropOffA.Text = "" Then
                messaggio = messaggio & "- Specificare date di A drop off per effettuare il calcolo delle commissioni " & vbCrLf
            End If
            If txtCercaDropOffDa.Text = "" Then
                messaggio = messaggio & "- Specificare date di DA drop off per effettuare il calcolo delle commissioni " & vbCrLf
            End If
        End If
        If txtCercaPickUpA.Text <> "" Or txtCercaPickUpDa.Text <> "" Then
            If txtCercaPickUpA.Text = "" Then
                messaggio = messaggio & "- Specificare date di A Pick Up per effettuare il calcolo delle commissioni " & vbCrLf
            End If
            If txtCercaPickUpDa.Text = "" Then
                messaggio = messaggio & "- Specificare date di DA Pick Up per effettuare il calcolo delle commissioni " & vbCrLf
            End If
        End If


        'If txtCercaDropOffA.Text <> "" And txtCercaDropOffDa.Text <> "" And txtCercaPickUpDa.Text <> "" And txtCercaPickUpA.Text <> "" Then
        '    messaggio = messaggio & "- Specificare un'intervallo di date drop off o pick up per effettuare il calcolo delle commissioni " & vbCrLf
        'End If

        Return messaggio


    End Function

    Protected Function controlla_filtri_esportaXLS() As String
        Dim messaggio As String = ""

        'If dropCercaOperatori.SelectedValue = "0" Then
        '    messaggio = messaggio & "- Selezionare un operatore" & vbCrLf
        'End If

        If txtPercentualeImponibile.Text = "" Then
            messaggio = messaggio & "- Specificare la percentuale sull'imponibile" & vbCrLf
        End If

        If txtPercentualeCommissione.Text = "" Then
            messaggio = messaggio & "- Specificare la percentuale della commmissione (da applicare sulla percentuale dell'imponibile)" & vbCrLf
        End If

        If txtCercaDropOffA.Text = "" And txtCercaDropOffDa.Text = "" And txtCercaPickUpDa.Text = "" And txtCercaPickUpA.Text = "" Then
            messaggio = messaggio & "- Specificare un'intervallo di date di drop off o pick up per effettuare il calcolo delle commissioni " & vbCrLf
        End If

        If txtCercaDropOffA.Text <> "" Or txtCercaDropOffDa.Text <> "" Then
            If txtCercaDropOffA.Text = "" Then
                messaggio = messaggio & "- Specificare date di A drop off per effettuare il calcolo delle commissioni " & vbCrLf
            End If
            If txtCercaDropOffDa.Text = "" Then
                messaggio = messaggio & "- Specificare date di DA drop off per effettuare il calcolo delle commissioni " & vbCrLf
            End If
        End If

        'If txtCercaPickUpA.Text <> "" Or txtCercaPickUpDa.Text <> "" Then
        '    If txtCercaPickUpA.Text = "" Then
        '        messaggio = messaggio & "- Specificare date di A Pick Up per effettuare il calcolo delle commissioni " & vbCrLf
        '    End If
        '    If txtCercaPickUpDa.Text = "" Then
        '        messaggio = messaggio & "- Specificare date di DA Pick Up per effettuare il calcolo delle commissioni " & vbCrLf
        '    End If
        'End If


        'If txtCercaDropOffA.Text <> "" And txtCercaDropOffDa.Text <> "" And txtCercaPickUpDa.Text <> "" And txtCercaPickUpA.Text <> "" Then
        '    messaggio = messaggio & "- Specificare un'intervallo di date drop off o pick up per effettuare il calcolo delle commissioni " & vbCrLf
        'End If

        Return messaggio


    End Function

    Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click

        Try
            Dim controllo_filtri As String = controlla_filtri()

            If controllo_filtri = "" Then


                Dim condizione As String = condizione_where()

                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    Session("url_print") = "/stampe/contratti/commissioni_operatore.aspx?orientamento=verticale&query=" & Server.UrlEncode(condizione) & "&percentuale_percentuale=" & txtPercentualeCommissione.Text & "&percentuale_imponibile=" & txtPercentualeImponibile.Text & "&cerca_operatore=" & dropCercaOperatori.SelectedValue & "&header_html=/stampe/contratti/header_commissioni_operatore.aspx?valore=" & Server.UrlEncode(dropCercaOperatori.SelectedItem.Text.Replace(" ", "-") & "-" & txtCercaDropOffDa.Text & "-" & txtCercaDropOffA.Text & "--Percentuale-imponibile:-" & txtPercentualeImponibile.Text & "%--Percentuale-commissione:-" & txtPercentualeCommissione.Text & "%")
                    If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
                    End If
                End If
            Else
                Libreria.genUserMsgBox(Me, controllo_filtri)
            End If

        Catch ex As Exception
            Response.Write("Error_btnStampa_Click_:" & ex.Message & "<br/>")
        End Try

    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click

        Try

            Dim listAccessori As String = ""

            For i As Integer = 0 To checkAccessori.Items.Count - 1

                If checkAccessori.Items(i).Selected = True Then
                    If listAccessori = "" Then
                        listAccessori = checkAccessori.Items(i).Value
                    Else
                        listAccessori += "," & checkAccessori.Items(i).Value
                    End If
                End If


            Next


            If listAccessori = "" Then
                funzioni_comuni.genUserMsgBox(Page, "Nessun accessorio selezionato.\nSelezionare un profilo o un accessorio dalla lista.")
                Exit Sub
            End If


            Dim controllo_filtri As String = controlla_filtri()

            If controllo_filtri = "" Then
                'sqlCommissioniOperatore.SelectCommand = "SELECT id, num_contratto, data_rientro, contratti.giorni, " & _
                '"(SELECT SUM(imponibile_scontato) FROM contratti_costi WITH(NOLOCK) INNER JOIN condizioni_elementi ON contratti_costi.id_elemento=condizioni_elementi.id " & _
                '"INNER JOIN commissioni_operatore WITH(NOLOCK) ON contratti_costi.id_elemento=commissioni_operatore.id_condizioni_elementi " & _
                '"WHERE contratti_costi.id_documento=contratti.id AND contratti_costi.num_calcolo=contratti.num_calcolo AND condizioni_elementi.commissione_operatore=1 AND selezionato=1 AND ISNULL(omaggiato,0)=0 AND commissioni_operatore.id_operatore=" & dropCercaOperatori.SelectedValue & " AND contratti.num_contratto=commissioni_operatore.num_contratto) As imponibile_commissione, " & _
                '"(SELECT contratti_costi_1.imponibile_scontato FROM contratti_costi As contratti_costi_1 WITH(NOLOCK) WHERE contratti_costi_1.id_documento=contratti.id AND contratti_costi_1.num_calcolo=contratti.num_calcolo AND contratti_costi_1.nome_costo='TOTALE') As imponibile_totale " & _
                '"FROM contratti WHERE (attivo=1) AND (status=4 OR status=6 OR status=8) AND (SELECT SUM(imponibile_scontato) FROM contratti_costi WITH(NOLOCK) INNER JOIN condizioni_elementi ON contratti_costi.id_elemento=condizioni_elementi.id " & _
                '"INNER JOIN commissioni_operatore WITH(NOLOCK) ON contratti_costi.id_elemento=commissioni_operatore.id_condizioni_elementi " & _
                '"WHERE contratti_costi.id_documento=contratti.id AND contratti_costi.num_calcolo=contratti.num_calcolo AND condizioni_elementi.commissione_operatore=1 AND selezionato=1 AND ISNULL(omaggiato,0)=0 AND commissioni_operatore.id_operatore=" & dropCercaOperatori.SelectedValue & " AND contratti.num_contratto=commissioni_operatore.num_contratto)<>0" & condizione_where()



                'prima di effettuare la ricerca deve aggiornare la tabella delle fonti commissionabili 06.09.2022

                'se accessori selezionati aggiorna le commissioni
                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)


                Dim sqlq As String = ""

                'prima reset commissioni ...
                ResetCommissioniOperatore()

                'assegna le commissioni sui ck selezionati
                For i As Integer = 0 To checkAccessori.Items.Count - 1

                    If checkAccessori.Items(i).Selected = True Then

                        'imposta le commissioni come tabelle listini --> commissioni
                        Dim id_elemento As String = checkAccessori.Items(i).Value 'id_elemento 'listElementiCondizionixCommissione.Items(i).FindControl("idLabel")

                        ' deve recuperare da tabella condizioni_elementi
                        Dim commissione_stazione As String = GetCommissioneStazione(id_elemento) 'DropDownList = listElementiCondizionixCommissione.Items(i).FindControl("commissione_stazione")

                        Dim commissione_operatore As String = "1" 'sempre vero perchè selezionato da array 'listElementiCondizionixCommissione.Items(i).FindControl("commissione_operatore")

                        sqlq = "UPDATE condizioni_elementi SET commissione_stazione='" & commissione_stazione & "', "
                        sqlq += "commissione_operatore='" & commissione_operatore & "' WHERE id=" & id_elemento

                        Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlq, Dbc)
                        Cmd1.ExecuteNonQuery()

                        Cmd1.Dispose()
                        Cmd1 = Nothing

                    End If

                Next

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing


                'effettua la ricerca
                Dim sql As String = "SELECT id, num_contratto, data_uscita, data_rientro, contratti.giorni, num_calcolo, "
                sql += "0 As imponibile_commissione, 0 As imponibile_totale "
                sql += "FROM contratti WHERE (attivo=1) AND (status=4 OR status=6 OR status=8)" & condizione_where()
                sql += " ORDER BY data_rientro,num_contratto"

                sqlCommissioniOperatore.SelectCommand = sql '"SELECT id, num_contratto, data_rientro, contratti.giorni, num_calcolo, " & _


                listCommissioniOperatore.Visible = True
                trlv.Visible = True


                '"0 As imponibile_commissione, " & _
                '"0 As imponibile_totale " & _
                '"FROM contratti WHERE (attivo=1) AND (status=4 OR status=6 OR status=8)" & condizione_where()

                'Response.Write(sql)
                'Response.End()

                Try
                    lblCommissioni.Text = 0
                    lblGiorni.Text = 0
                    listCommissioniOperatore.DataBind()

                    'AggiornaPagina()


                Catch ex As Exception
                    Response.Write("Error_btnCerca_:" & ex.Message & "<br/>")
                End Try

            Else
                Libreria.genUserMsgBox(Me, controllo_filtri)
            End If
        Catch ex As Exception
            Libreria.genUserMsgBox(Me, ex.Message)
            'Response.Write("Error_btnCerca_Click_:" & ex.Message & "<br/>")
        End Try

    End Sub
    Public Sub AggiornaPagina()
        Dim F As System.Web.UI.Page
        Dim sb As New StringBuilder()
        sb.Append("<script type = 'text/javascript'>")
        sb.Append("document.getElementById('listCommissioniOperatore').scrollIntoView(true);")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(Me.GetType(), "script", sb.ToString())
    End Sub
    Protected Sub listCommissioniOperatore_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listCommissioniOperatore.ItemDataBound

        Dim id_contratto As Label = e.Item.FindControl("idLabel")
        Dim num_calcolo As Label = e.Item.FindControl("num_calcolo")
        Dim num_contratto As Label = e.Item.FindControl("num_contratto")
        Dim imponibile_totale As Label = e.Item.FindControl("imponibile_totale")
        Dim imponibile_commissione As Label = e.Item.FindControl("imponibile_commissione")
        Dim giorni As Label = e.Item.FindControl("giorni")

        Try

            'PER PRIMA COSA CONTROLLO SE PER IL DATO OPERATORE HO UNA COMMISSIONE PER QUESTO CONTRATTO ALTRIMENTI NON FACCIO ALCUN CALCOLO
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String = "SELECT TOP 1 commissioni_operatore.id FROM commissioni_operatore WITH(NOLOCK) "
            sqlStr += "INNER JOIN condizioni_elementi WITH(NOLOCK) ON commissioni_operatore.id_condizioni_elementi=condizioni_elementi.id "
            sqlStr += "WHERE id_operatore='" & dropCercaOperatori.SelectedValue & "' AND num_contratto='" & num_contratto.Text & "' AND condizioni_elementi.commissione_operatore=1"


            If num_contratto.Text = "112207263" Then
                System.Threading.Thread.Sleep(10)
            End If


            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim test As String = Cmd.ExecuteScalar & ""

            If test <> "" Then

                sqlStr = "SELECT contratti_costi.imponibile_scontato FROM contratti_costi WITH(NOLOCK) WHERE contratti_costi.id_documento=" & id_contratto.Text & " "
                sqlStr += "And contratti_costi.num_calcolo=" & num_calcolo.Text & " And contratti_costi.nome_costo='TOTALE'"

                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                imponibile_totale.Text = Cmd.ExecuteScalar() & ""

                sqlStr = "SELECT SUM(imponibile_scontato) FROM contratti_costi WITH(NOLOCK) INNER JOIN condizioni_elementi ON contratti_costi.id_elemento=condizioni_elementi.id "
                sqlStr += "INNER JOIN commissioni_operatore WITH(NOLOCK) ON contratti_costi.id_elemento=commissioni_operatore.id_condizioni_elementi "
                sqlStr += "WHERE contratti_costi.id_documento=" & id_contratto.Text & " AND contratti_costi.num_calcolo=" & num_calcolo.Text & " AND condizioni_elementi.commissione_operatore=1 AND selezionato=1 AND ISNULL(omaggiato,0)=0 AND commissioni_operatore.id_operatore=" & dropCercaOperatori.SelectedValue & "AND commissioni_operatore.num_contratto=" & num_contratto.Text

                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                imponibile_commissione.Text = Cmd.ExecuteScalar & ""

                If imponibile_commissione.Text <> "" Then

                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing

                    Dim data_rientro As Label = e.Item.FindControl("data_rientro")

                    Dim imponibile_parziale As Label = e.Item.FindControl("imponibile_parziale")
                    Dim imponibile_netto As Label = e.Item.FindControl("imponibile_netto")

                    data_rientro.Text = Left(data_rientro.Text, 10)
                    imponibile_totale.Text = FormatNumber(CDbl(imponibile_totale.Text), 2)

                    If imponibile_commissione.Text = "" Then
                        imponibile_commissione.Text = "0"
                    End If

                    lblGiorni.Text = lblGiorni.Text + CInt(giorni.Text)
                    lblCommissioni.Text = CDbl(lblCommissioni.Text) + ((CDbl(imponibile_commissione.Text) * txtPercentualeImponibile.Text) / 100) * txtPercentualeCommissione.Text / 100
                    imponibile_parziale.Text = FormatNumber(CDbl(imponibile_commissione.Text), 2)
                    imponibile_netto.Text = FormatNumber(CDbl(imponibile_commissione.Text * txtPercentualeImponibile.Text) / 100, 2)
                    imponibile_commissione.Text = FormatNumber((((CDbl(imponibile_commissione.Text) * txtPercentualeImponibile.Text) / 100) * txtPercentualeCommissione.Text / 100), 2)
                Else
                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing

                    e.Item.FindControl("riga_contratto").Visible = False
                End If


            Else
                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                e.Item.FindControl("riga_contratto").Visible = False
            End If
        Catch ex As Exception
            Response.Write("Error_Item_Data_Bound_:" & ex.Message & "<br/>")
        End Try


    End Sub

    Protected Sub listCommissioniOperatore_DataBound(sender As Object, e As System.EventArgs) Handles listCommissioniOperatore.DataBound
        Try
            lblCommissioni.Text = FormatNumber(CDbl(lblCommissioni.Text), 2)
        Catch ex As Exception
            Response.Write("Error_DataBound_:" & ex.Message & "<br/>")
        End Try

    End Sub


    Protected Sub btnpulisci_Click(sender As Object, e As EventArgs)



        txtCercaDropOffA.Text = ""
        txtCercaDropOffDa.Text = ""
        txtCercaPickUpA.Text = ""
        txtCercaPickUpDa.Text = ""
        txtPercentualeCommissione.Text = ""
        txtPercentualeImponibile.Text = ""
        dropCercaOperatori.SelectedValue = "0"

        'nuovi 18.08.2022 salvo
        'reset cktipocliente
        For i As Integer = 0 To checkTipoClienti.Items.Count - 1
            checkTipoClienti.Items(i).Selected = False
        Next

        'reset ckaccessori salvo
        For i As Integer = 0 To checkAccessori.Items.Count - 1
            checkAccessori.Items(i).Selected = False
        Next

        'reset label
        listCommissioniOperatore.Visible = False
        lblCommissioni.Text = "0,00"
        lblGiorni.Text = "0"



    End Sub

    Protected Sub btnExportXLS_Click(sender As Object, e As EventArgs)


        Dim listTipoCliente As String = ""
        Dim listAccessori As String = ""


        For i As Integer = 0 To checkTipoClienti.Items.Count - 1
            If checkTipoClienti.Items(i).Selected = True Then
                If listTipoCliente = "" Then
                    listTipoCliente = checkTipoClienti.Items(i).Value
                Else
                    listTipoCliente += "," & checkTipoClienti.Items(i).Value
                End If

            End If
        Next


        Session("tipoclientereport") = listTipoCliente

        For i As Integer = 0 To checkAccessori.Items.Count - 1

            'If checkAccessori.Items(i).Value <> "98" Then               'esclude tmp+km perchè è sempre calcolata
            If checkAccessori.Items(i).Selected = True Then
                If listAccessori = "" Then
                    listAccessori = checkAccessori.Items(i).Value
                Else
                    listAccessori += "," & checkAccessori.Items(i).Value
                End If
            End If


            'End If

        Next
        Session("accessorireport") = listAccessori

        'Response.Write(listTipoCliente & "<br/>" & listAccessori & "<br/>")
        'Exit Sub

        If listAccessori = "" Then
            funzioni_comuni.genUserMsgBox(Page, "Nessun accessorio selezionato.\nSelezionare un profilo o un accessorio dalla lista.")
            Exit Sub
        Else


        End If



        Dim controllo_filtri As String = controlla_filtri_esportaXLS()

        If controllo_filtri = "" Then


            'Imposta le commissioni_operatore prima di inviare richiesta per il calcolo
            Dim lAccessori As String = SetCommissioniOperatore()

            Dim sqlstr As String = ""

            Dim condizione As String = ""

            Dim idoperatore As String = dropCercaOperatori.SelectedValue
            Dim operatore As String = dropCercaOperatori.SelectedItem.Text


            Dim da_data As String = funzioni_comuni.GetDataSql(txtCercaDropOffDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaDropOffDa.Text)
            Dim a_data As String = funzioni_comuni.GetDataSql(txtCercaDropOffA.Text, 59) ' funzioni_comuni.getDataDb_con_orario(txtCercaDropOffA.Text & " 23:59:59")

            'Dim condizione_where As String = " AND (contratti.id_stazione_uscita=" & dropCercaStazionePickUp.SelectedValue & ") AND (contratti.data_rientro BETWEEN Convert(DateTime, '" & da_data & "', 102) AND CONVERT(DATETIME, '" & a_data & "', 102)) "

            'Dim m As String = ddl_mese.SelectedValue
            'Dim y As String = ddl_anno.SelectedValue
            'Dim mese As String = ddl_mese.SelectedItem.Text
            Dim ddt As String = txtCercaDropOffDa.Text
            Dim adt As String = txtCercaDropOffA.Text

            Dim m As String = CDate(txtCercaDropOffDa.Text).Month.ToString
            Dim y As String = CDate(txtCercaDropOffA.Text).Year.ToString
            Dim mese As String = funzioni_comuni_new.GetNomeMese(m)

            Dim perc_comm As String = txtPercentualeImponibile.Text
            Dim perc_imp As String = txtPercentualeCommissione.Text


            'Exit Sub 'test


            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                Session("condizione_royalties") = condizione
                If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('stampaReportCommissioniOperatore.aspx?cka=" & listAccessori & "&tc=" & listTipoCliente & "&pcom=" & perc_comm & "&pimp=" & perc_imp & "&ddt=" & ddt & "&adt=" & adt & "&ope=" & idoperatore & "&mese=" & mese & "&m=" & m & "&y=" & y & "','')", True)
                End If
            End If

        Else
            Libreria.genUserMsgBox(Me, controllo_filtri)
        End If


    End Sub

    Private Sub commissioni_operatore_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete

    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)

        If checkAccessori.Visible = True Then
            checkAccessori.Visible = False
        Else
            checkAccessori.Visible = True
        End If

    End Sub


    Protected Sub ddl_profile_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim accessori As String = ""
        Dim ddlaccessori As String = ddl_profile.SelectedValue
        Dim aListAccessori() As String

        'reset list check e db
        For i As Integer = 0 To checkAccessori.Items.Count - 1
            checkAccessori.Items(i).Selected = False
        Next
        ResetCommissioniOperatore()  'mette tutte le commissioni_operatore a false

        'verifica quale profilo selezionare

        If ddl_profile.SelectedValue <> "0" Then
            If ddl_profile.SelectedValue = "1" Then
                accessori = "98" 'solo tempo + KM
            ElseIf ddl_profile.SelectedValue = "2" Then
                accessori = "98,171,211,218,228,224,210,223,235,236,237,86,184,101,227,281,202,110,72,279,217,216,252,280,248,247,234,100,170,253,282"
            End If
            aListAccessori = Split(accessori, ",")

        End If

        If accessori <> "" Then

            For x = 0 To UBound(aListAccessori)
                For i As Integer = 0 To checkAccessori.Items.Count - 1
                    If checkAccessori.Items(i).Value = aListAccessori(x) Then
                        checkAccessori.Items(i).Selected = True
                    End If
                Next
            Next


            'se accessori selezionati aggiorna le commissioni
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)


            Dim sqlq As String = ""

            For x = 0 To UBound(aListAccessori)

                For i As Integer = 0 To checkAccessori.Items.Count - 1

                    If checkAccessori.Items(i).Value = aListAccessori(x) Then

                        'imposta le commissioni come tabelle listini --> commissioni
                        Dim id_elemento As String = checkAccessori.Items(i).Value 'id_elemento 'listElementiCondizionixCommissione.Items(i).FindControl("idLabel")

                        ' deve recuperare da tabella condizioni_elementi
                        Dim commissione_stazione As String = GetCommissioneStazione(id_elemento) 'DropDownList = listElementiCondizionixCommissione.Items(i).FindControl("commissione_stazione")

                        Dim commissione_operatore As String = "1" 'sempre vero perchè selezionato da array 'listElementiCondizionixCommissione.Items(i).FindControl("commissione_operatore")

                        sqlq = "UPDATE condizioni_elementi SET commissione_stazione='" & commissione_stazione & "', "
                        sqlq += "commissione_operatore='" & commissione_operatore & "' WHERE id=" & id_elemento

                        Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlq, Dbc)
                        Cmd1.ExecuteNonQuery()

                        Cmd1.Dispose()
                        Cmd1 = Nothing

                    End If

                Next
            Next


            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Else

        End If


    End Sub


    Protected Function GetCommissioneStazione(id_elemento As String) As String

        Dim ris As String = "0"

        Dim sqlq As String = "SELECT [commissione_stazione]  FROM [dbo].[condizioni_elementi] where id=" & id_elemento

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)


        Try
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlq, Dbc)

            Dim oRSA As Data.SqlClient.SqlDataReader
            oRSA = Cmd.ExecuteReader()

            If oRSA.HasRows Then
                oRSA.Read()
                ris = oRSA!commissione_stazione
            End If

            oRSA.Close()
            Cmd.Dispose()
            oRSA = Nothing
            Cmd = Nothing
            Dbc.Close()

        Catch ex As Exception
            ris = "0"
        End Try


        Return ris


    End Function

    Protected Function SetCommissioniOperatore() As String

        'imposta valori sul db x i successivi calcoli
        Dim ris As String = ""
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

        Dim lista_Accessori As String = ""

        For i As Integer = 0 To checkAccessori.Items.Count - 1

            Dim id_elemento As String = checkAccessori.Items(i).Value

            If checkAccessori.Items(i).Selected = True Then

                Dim commissione_operatore As String = "1" 'sempre vero perchè selezionato da array 'listElementiCondizionixCommissione.Items(i).FindControl("commissione_operatore")

                Dim sqlq As String = "UPDATE condizioni_elementi SET commissione_operatore='" & commissione_operatore & "' WHERE id=" & id_elemento

                Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlq, Dbc)
                Cmd1.ExecuteNonQuery()
                Cmd1.Dispose()
                Cmd1 = Nothing

                If lista_Accessori = "" Then
                    lista_Accessori = id_elemento
                Else
                    lista_Accessori += "," & id_elemento
                End If

            Else

                Dim sqlq As String = "UPDATE condizioni_elementi SET commissione_operatore='0' WHERE id=" & id_elemento

                Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlq, Dbc)
                Cmd1.ExecuteNonQuery()
                Cmd1.Dispose()
                Cmd1 = Nothing

            End If



        Next

        Dbc.Close()
        Dbc = Nothing

        Return lista_Accessori


    End Function

    Protected Function ResetCommissioniOperatore() As Boolean

        Dim ris As Boolean = False
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)

        Try
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

            For i As Integer = 0 To checkAccessori.Items.Count - 1

                Dim id_elemento As String = checkAccessori.Items(i).Value

                Dim sqlq As String = "UPDATE condizioni_elementi SET commissione_operatore='0' WHERE id=" & id_elemento

                Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlq, Dbc)
                Cmd1.ExecuteNonQuery()

                Cmd1.Dispose()
                Cmd1 = Nothing

            Next
            Dbc.Close()

        Catch ex As Exception
            ris = False
        End Try


        Return ris


    End Function

    Protected Sub btnRiepilogoStazione_Click(sender As Object, e As EventArgs)


        Dim ris As Double = 0
        Dim risMeseAnnoCur As Double = 0
        Dim risMeseAnnoPrev As Double = 0
        Dim meseCur As String, annoCur As String
        annoCur = Year(txtCercaDropOffDa.Text)
        meseCur = Month(txtCercaDropOffA.Text)

        Try

            'ricerca per periodo selezionato
            ris = GetTotaliCassa(txtCercaDropOffDa.Text, txtCercaDropOffA.Text, "2", 0) 'test su Palermo APT

            txtTotPeriodo.Text = txtCercaDropOffDa.Text & "-" & txtCercaDropOffA.Text & ": €." & FormatNumber(ris, 2)

            'Cassa intero mese precedente, anno corrente

            risMeseAnnoCur = GetTotaliCassa(txtCercaDropOffDa.Text, txtCercaDropOffA.Text, "2", 1) 'test su Palermo APT
            If CInt(meseCur) = 1 Then   'se gennaio calcola dicembre dell'anno precedente
                meseCur = 12
                annoCur = CInt(annoCur) - 1
            Else
                meseCur = CInt(meseCur) - 1
            End If

            txtTotMesePrev.Text = meseCur & "/" & annoCur & ": €." & FormatNumber(risMeseAnnoCur, 2)

            'Cassa stesso mese intero, anno precedente

            risMeseAnnoPrev = GetTotaliCassa(txtCercaDropOffDa.Text, txtCercaDropOffA.Text, "2", 2) 'test su Palermo APT
            meseCur = Month(txtCercaDropOffA.Text)
            annoCur = CInt(annoCur) - 1
            txtTotMeseAnnoPrev.Text = meseCur & "/" & annoCur & ": €." & FormatNumber(risMeseAnnoPrev, 2)



        Catch ex As Exception
            Response.Write("error btnRiepilogoStazione_Click: " & ex.Message & "<br/>")
        End Try




    End Sub



End Class
