
Partial Class contratto_vedi_calcolo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            livello_accesso_broker.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "80")

            Dim test As Integer

            Try
                test = CInt(Request.QueryString("idCnt"))
                idContratto.Text = test

                test = CInt(Request.QueryString("versione"))
                numCalcolo.Text = test

                test = CInt(Request.QueryString("test"))
                milli.Text = test

                fillDatiContratto()
            Catch ex As Exception

            End Try

        End If
    End Sub

    Protected Sub fillDatiContratto()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT (stazioni1.codice + ' - ' + stazioni1.nome_stazione) As stazione_uscita, (stazioni2.codice + ' - ' + stazioni2.nome_stazione) As stazione_presunto_rientro, num_prenotazione, giorni, giorni_to, data_uscita, data_presunto_rientro, data_creazione, num_contratto, totale_costo_prenotazione, importo_a_carico_del_broker, codtar, giorni_prepagati FROM contratti WITH(NOLOCK) INNER JOIN stazioni As stazioni1 WITH(NOLOCK) ON contratti.id_stazione_presunto_rientro=stazioni1.id INNER JOIN stazioni As stazioni2 WITH(NOLOCK) ON contratti.id_stazione_presunto_rientro=stazioni2.id WHERE contratti.id='" & idContratto.Text & "' AND contratti.num_calcolo='" & numCalcolo.Text & "' AND DATEPART(Ms, contratti.data_creazione)=" & CInt(milli.Text), Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        If Rs.Read() Then
            listContrattiCosti.Visible = True

            lblNumContratto.Text = Rs("num_contratto")
            numVariazione.Text = CInt(numCalcolo.Text - 1)

            If numVariazione.Text = "0" Then
                lblVariazione.Visible = False
                numVariazione.Visible = False
            End If

            lblDataContratto.Text = Rs("data_creazione")

            If (Rs("num_prenotazione") & "") <> "" Then
                lblNumPren.Text = Rs("num_prenotazione")

                lblNumPren.Visible = True
                lblDaPrenotazione.Visible = True

                If Pagamenti.is_complimentary(Rs("num_prenotazione"), Rs("num_contratto")) Then
                    complimentary.Text = "1"
                End If
            Else
                If Pagamenti.is_complimentary("", Rs("num_contratto")) Then
                    complimentary.Text = "1"
                End If
            End If

            dropStazioneDropOff.Text = Rs("stazione_presunto_rientro")
            dropStazionePickUp.Text = Rs("stazione_uscita")

            txtDaData.Text = Day(Rs("data_uscita")) & "/" & Month(Rs("data_uscita")) & "/" & Year(Rs("data_uscita"))
            txtAData.Text = Day(Rs("data_presunto_rientro")) & "/" & Month(Rs("data_presunto_rientro")) & "/" & Year(Rs("data_presunto_rientro"))

            ore1.Text = Hour(Rs("data_uscita"))
            minuti1.Text = Minute(Rs("data_uscita"))
            ore2.Text = Hour(Rs("data_presunto_rientro"))
            minuti2.Text = Minute(Rs("data_presunto_rientro"))

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

            txtoraPartenza.Text = ore1.Text & ":" & minuti1.Text
            txtOraRientro.Text = ore2.Text & ":" & minuti2.Text

            lblTariffa.Text = Rs("CODTAR")

            txtGiorni.Text = Rs("giorni")

            'CONTROLLO SE LA TARIFFA E' DI TIPO BROKER
            If (Rs("importo_a_carico_del_broker") & "") <> "" Then
                'SE QUESTO VALORE E' VALORIZZATO LA TARIFFA E' BROKER, ALTRIMENTO NON LO E', QUESTO IN QUANTO
                'NON E' POSSIIBLE TRASFORMARE UNA TARIFFA DA BROKER A NON BROKER SE E' GIA' STATA UTILIZZATA
                tariffa_broker.Text = "1"
                a_carico_del_broker.Text = Rs("importo_a_carico_del_broker")
                txtNumeroGiorniTO.Text = Rs("giorni_to") & ""
            Else
                tariffa_broker.Text = "0"
                a_carico_del_broker.Text = ""
                lblGiorniTO.Visible = False
                txtNumeroGiorniTO.Visible = False
            End If

            If (Rs("giorni_prepagati") & "") <> "" Then
                txtGiorniPrepagati.Text = Rs("giorni_prepagati")
            Else
                lblGiorniPrepagati.Visible = False
                txtGiorniPrepagati.Visible = False
            End If

            If (Rs("num_prenotazione") & "") <> "" Then
                Dim nome_costo As Label
                Dim totale_contratto As Double
                Dim costo_scontato As Label
                For i = 0 To listContrattiCosti.Items.Count - 1
                    nome_costo = listContrattiCosti.Items(i).FindControl("nome_costo")

                    If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
                        costo_scontato = listContrattiCosti.Items(i).FindControl("costo_scontato")
                        totale_contratto = CDbl(costo_scontato.Text)
                    End If
                Next

                If totale_contratto <> CDbl(Rs("totale_costo_prenotazione")) Then
                    lblTestoDaPrenotazione.Visible = True
                    lblDifferenzaDaPrenotazione.Visible = True
                    lblEuroDaPrenotazione.Visible = True

                    lblDifferenzaDaPrenotazione.Text = FormatNumber(totale_contratto - CDbl(Rs("totale_costo_prenotazione")), 2, , , TriState.False)
                Else
                    lblTestoDaPrenotazione.Visible = False
                    lblDifferenzaDaPrenotazione.Visible = False
                    lblEuroDaPrenotazione.Visible = False

                    lblDifferenzaDaPrenotazione.Text = "0"
                End If
            Else
                lblTestoDaPrenotazione.Visible = False
                lblDifferenzaDaPrenotazione.Visible = False
                lblEuroDaPrenotazione.Visible = False

                lblDifferenzaDaPrenotazione.Text = "0"
            End If
        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub listContrattiCosti_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles listContrattiCosti.DataBinding
        ultimo_gruppo.Text = ""
    End Sub

    Protected Sub listContrattiCosti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listContrattiCosti.ItemDataBound
        Dim gruppo As Label = e.Item.FindControl("gruppo")
        Dim id_a_carico_di As Label = e.Item.FindControl("id_a_carico_di")
        Dim nome_costo As Label = e.Item.FindControl("nome_costo")
        Dim obbligatorio As Label = e.Item.FindControl("obbligatorio")
        Dim id_metodo_stampa As Label = e.Item.FindControl("id_metodo_stampa")
        Dim sconto As Label = e.Item.FindControl("lblSconto")
        'Dim valore_costo As Label = e.Item.FindControl("valore_costoLabel")
        Dim costo_scontato As Label = e.Item.FindControl("costo_scontato")
        Dim omaggiabile As Label = e.Item.FindControl("omaggiabile")
        Dim Oldomaggiato As CheckBox = e.Item.FindControl("chkOldOmaggio")
        Dim omaggiato As CheckBox = e.Item.FindControl("chkOmaggio")
        Dim id_unita_misura As Label = e.Item.FindControl("id_unita_misura")
        Dim packed As Label = e.Item.FindControl("packed")
        Dim costo_unitario As Label = e.Item.FindControl("lbl_costo_unitario")
        Dim qta As Label = e.Item.FindControl("qta")
        Dim id_elemento As Label = e.Item.FindControl("id_elemento")
        Dim tipologia As Label = e.Item.FindControl("tipologia")
        Dim imponibile As Label = e.Item.FindControl("imponibile")
        Dim onere As Label = e.Item.FindControl("onere")
        Dim iva As Label = e.Item.FindControl("iva")
        Dim prepagato As Label = e.Item.FindControl("prepagato")

        If prepagato.Text = "True" Or (txtGiorniPrepagati.Visible And nome_costo.Text = Costanti.testo_elemento_totale) Then
            e.Item.FindControl("costo_prepagato").Visible = True
            e.Item.FindControl("labelPrepagato").Visible = True

            If nome_costo.Text = Costanti.testo_elemento_totale Then
                Dim costo_prepagato As Label = e.Item.FindControl("costo_prepagato")
                costo_prepagato.Font.Bold = True
                costo_prepagato.Font.Size = 12
            End If

            If costo_scontato.Text = "0,00" Then
                costo_unitario.Visible = False
                qta.Visible = False
                costo_scontato.Visible = False
                onere.Visible = False
                e.Item.FindControl("aliquota_iva").Visible = False
                iva.Visible = False
                imponibile.Visible = False
            End If
        End If

        'REGOLE SU ACCESSORI A SCELTA ----------------------------------------------------------------------------------------------------
        If obbligatorio.Text = "False" And id_a_carico_di.Text <> Costanti.id_accessorio_incluso Then
            Dim chkScegli As CheckBox = e.Item.FindControl("chkScegli")
            chkScegli.Visible = True
            chkScegli.Enabled = False

            'GLI ACCESSORI NON SELEZIONATI NON MOSTRANO IL PREZZO------------------------------------------------------------------------------
            If Not chkScegli.Checked Then
                'valore_costo.Visible = False
                sconto.Visible = False
                costo_scontato.Visible = False
                qta.Visible = False
                costo_unitario.Visible = False
                imponibile.Visible = False
                onere.Visible = False
                iva.Visible = False
                e.Item.FindControl("aliquota_iva").Visible = False
            End If
            '----------------------------------------------------------------------------------------------------------------------------------
        End If
        '---------------------------------------------------------------------------------------------------------------------------------

        If sconto.Text = "0,00" Then
            sconto.Text = ""
        Else
            sconto.Text = sconto.Text & " €"
        End If

        If packed.Text = "True" Then
            'SE L'ACCESSORIO E' PACKED ALLORA LA QUANTITA E' SEMPRE 1
            'qta.Text = "1"
            costo_unitario.Text = costo_scontato.Text
        ElseIf id_unita_misura.Text = "0" Then
            'SE L'UNITA' DI MISURA NON E' SPECIFICATA ALLORA L'ACCESSORIO HA COSTO UNITARIO A PRESCINDERE DAL CAMPO PACKED
            'qta.Text = "1"
            costo_unitario.Text = costo_scontato.Text
        ElseIf (id_unita_misura.Text = Costanti.id_unita_misura_giorni) Or (tipologia.Text = "KM_EXTRA" And obbligatorio.Text = "True") Then
            'SE L'L'UNITA' DI MISURA E' AL GIORNO (ACCESSORIO CERTAMENTE NON PACKED) - OPPURE SE SI TRATTA DI KM_EXTRA ADDEBITATI (OBBLIGATORIO)
            'qta.Text = txtNumeroGiorni.Text
            If txtGiorniPrepagati.Visible And id_unita_misura.Text = Costanti.id_unita_misura_giorni And prepagato.Text = "True" Then
                qta.Text = CInt(qta.Text) - CInt(txtGiorniPrepagati.Text)
            End If

            If CInt(qta.Text) = 0 Then
                qta.Text = "1"
            End If
            costo_unitario.Text = FormatNumber(CDbl(imponibile.Text) / CInt(qta.Text), 2, , , TriState.False)
        ElseIf id_unita_misura.Text <> Costanti.id_unita_misura_giorni Then
            'SE L'UNITA' DI MISURA NON E' GIORNALIERA ATTUALMENTE LO TRATTO COME FOSSE PACKED
            'qta.Text = "1"
            costo_unitario.Text = costo_scontato.Text & " €"
        ElseIf packed.Text = "" And id_unita_misura.Text = "" Then
            qta.Text = ""
            costo_unitario.Text = ""
        End If


        'CALCOLO DEL COSTO UNITARIO-------------------------------------------------------------------------------------------------------- 
        If Oldomaggiato.Checked Then
            'valore_costo.Text = "0,00 €"
            If id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore Then
                'PER LE INFORMATIVE NON NASCONDO IL COSTO - PER QUESTI ACCESSORI L'OMAGGIABILITA' EQUIVALE AD UN PAGAMENTO IN UN SECONDO MOMENTO
                costo_scontato.Text = "0,00 €"
            End If

            sconto.Text = ""
            costo_unitario.Visible = False
            qta.Visible = False
        Else
            'valore_costo.Text = valore_costo.Text & " €"
            costo_scontato.Text = costo_scontato.Text & " €"
        End If
        '-----------------------------------------------------------------------------------------------------------------------------------

        If LCase(nome_costo.Text) = "valore tariffa" Or LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Or LCase(nome_costo.Text) = "sconto" Then
            nome_costo.Font.Bold = True
        End If

        If id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore Then
            If id_a_carico_di.Text = Costanti.id_accessorio_incluso And LCase(nome_costo.Text) <> "valore tariffa" Then
                'PER GLI ELEMENTI INCLUSI TRANNE IL TEMPO KM NON MOSTRO IL PREZZO (CHE E' SEMPRE ZERO)
                e.Item.FindControl("lblIncluso").Visible = True
                'valore_costo.Visible = False
                sconto.Visible = False
                costo_scontato.Visible = False
                costo_unitario.Visible = False
                qta.Visible = False
            ElseIf id_a_carico_di.Text <> Costanti.id_accessorio_incluso And obbligatorio.Text = "True" And LCase(nome_costo.Text) <> "valore tariffa" And LCase(nome_costo.Text) <> "sconto" Then
                e.Item.FindControl("lblObbligatorio").Visible = True
            End If
        Else
            'PER LE INFORMATIVE NON MOSTRO IL COSTO UNITARIO E LA QUANTITA'
            e.Item.FindControl("lblInformativa").Visible = True

            costo_unitario.Visible = False
            qta.Visible = False
        End If

        If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
            If tariffa_broker.Text = "1" Then
                Dim costo_broker As Double = getCostoACaricoDelBroker()

                If CDbl(a_carico_del_broker.Text) < costo_broker Then
                    costo_scontato.Text = CDbl(costo_scontato.Text) - CDbl(a_carico_del_broker.Text)
                Else
                    costo_scontato.Text = CDbl(costo_scontato.Text) - costo_broker
                End If
            End If

            'valore_costo.Visible = False
            costo_scontato.Text = FormatNumber(costo_scontato.Text, 2, , , TriState.False)
            costo_scontato.Font.Bold = True
            costo_scontato.ForeColor = Drawing.Color.Blue
            costo_scontato.Font.Size = 12
            e.Item.FindControl("lblObbligatorio").Visible = False
            qta.Visible = False
            costo_unitario.Visible = False
        End If

        If gruppo.Text = ultimo_gruppo.Text Then
            e.Item.FindControl("riga_gruppo").Visible = False
            e.Item.FindControl("riga_intestazione").Visible = False
        Else
            e.Item.FindControl("riga_gruppo").Visible = True
            e.Item.FindControl("riga_intestazione").Visible = True
            ultimo_gruppo.Text = gruppo.Text
        End If

        'OMAGGIABILITA' DI ACCESSORI E OBBLIGATORI --------------------------------------------------------------------------------------
        If (omaggiabile.Text = "True" Or complimentary.Text = "1") And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore And Not e.Item.FindControl("lblIncluso").Visible And LCase(nome_costo.Text) <> LCase(Costanti.testo_elemento_totale) Then
            omaggiato.Visible = True
            omaggiato.Enabled = False
        End If
        '----------------------------------------------------------------------------------------------------------------------------------

        If tariffa_broker.Text = "1" Then
            If id_elemento.Text = Costanti.ID_tempo_km Then
                costo_unitario.Visible = False
                qta.Visible = False
                sconto.Visible = False
                'IL COSTO SCONTATO NON E' VISIBILE SE COINCIDE COL COSTO A CARICO DEL BROKER, ALTRIMENTI VISUALIZZO SOLO LA PARTE A 
                'CARICO DEL CLIENTE - SE, IN FASE DI NUOVO CALCOLO, IL COSTO E' A CARICO DEL BROKER COMUNQUE NASCONDO IL COSTO
                If CDbl(costo_scontato.Text) = CDbl(a_carico_del_broker.Text) Then
                    costo_scontato.Visible = False
                Else
                    'SE IL COSTO E' DIMINUITO TRATTO IL CASO COME SE FOSSE A CARICO DEL BROKER
                    If CDbl(costo_scontato.Text) >= CDbl(a_carico_del_broker.Text) Then
                        costo_scontato.Text = FormatNumber(CDbl(costo_scontato.Text) - CDbl(CDbl(a_carico_del_broker.Text)), 2, , , TriState.False)
                        costo_unitario.Visible = True
                        qta.Visible = True
                        qta.Text = "1"
                        costo_unitario.Text = costo_scontato.Text
                    Else
                        costo_scontato.Visible = False
                    End If
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
                    If CDbl(costo_scontato.Text) = CDbl(a_carico_del_broker.Text) Then
                        a_carico_to.Text = Replace(costo_scontato.Text, "€", "")
                    Else
                        a_carico_to.Text = Replace(a_carico_del_broker.Text, "€", "")
                    End If
                End If
                If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
                    Dim a_carico_to As Label = e.Item.FindControl("a_carico_to")
                    a_carico_to.Visible = True
                    If CDbl(costo_scontato.Text) = CDbl(a_carico_del_broker.Text) Then
                        'NEL CASO DI IMPORTO TOTALMENTE A CARICO DEL BROKER L'INTERNO VALORE TARIFFA E' IL TOTALE DA PAGARE
                        a_carico_to.Text = Replace(get_tempo_km(), "€", "")
                    Else
                        a_carico_to.Text = Replace(a_carico_del_broker.Text, "€", "")
                    End If
                    a_carico_to.Font.Bold = True
                    a_carico_to.Font.Size = 12
                End If
            End If
        End If
    End Sub

    Protected Function get_tempo_km() As String
        Dim id_elemento As Label
        For i = 0 To listContrattiCosti.Items.Count - 1
            id_elemento = listContrattiCosti.Items(i).FindControl("id_elemento")
            If id_elemento.Text = Costanti.ID_tempo_km Then
                Dim costo_scontato As Label = listContrattiCosti.Items(i).FindControl("costo_scontato")
                get_tempo_km = costo_scontato.Text
                Exit For
            End If
        Next
    End Function

    Protected Function getCostoACaricoDelBroker() As Double
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT imponibile_scontato + iva_imponibile_scontato FROM contratti_costi WITH(NOLOCK) WHERE id_elemento='" & Costanti.ID_tempo_km & "' AND id_documento='" & idContratto.Text & "' AND num_calcolo='" & numCalcolo.Text & "'", Dbc)

        getCostoACaricoDelBroker = Cmd.ExecuteScalar()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function
End Class
