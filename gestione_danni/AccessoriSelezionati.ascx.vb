
Partial Class gestione_danni_AccessoriSelezionati
    Inherits System.Web.UI.UserControl

    Public Sub InitForm(id_numero_documento As Integer, numero_crv As Integer)
        Dim mio_record As DatiContratto = Nothing
        mio_record = DatiContratto.getRecordDaNumContratto(id_numero_documento, numero_crv)

        idContratto.Text = mio_record.id
        numCalcolo.Text = mio_record.num_calcolo
        statoContratto.Text = mio_record.status

        If (mio_record.importo_a_carico_del_broker & "") <> "" Then
            'SE QUESTO VALORE E' VALORIZZATO LA TARIFFA E' BROKER, ALTRIMENTO NON LO E', QUESTO IN QUANTO
            'NON E' POSSIIBLE TRASFORMARE UNA TARIFFA DA BROKER A NON BROKER SE E' GIA' STATA UTILIZZATA
            tariffa_broker.Text = "1"
            a_carico_del_broker.Text = mio_record.importo_a_carico_del_broker
        Else
            tariffa_broker.Text = "0"
            a_carico_del_broker.Text = ""
        End If
        statoModificaContratto.Text = "0" ' non so se va bene ma non sono sicuramente in modifica del contratto...
        livello_accesso_omaggi.Text = "1" ' tutto non è omaggiabile perché il contratto è cmq chiuso...

        If (mio_record.tipo_tariffa & "") = "generica" Then
            dropTariffeGeneriche.Items.Add(mio_record.CODTAR)
            dropTariffeGeneriche.Items(1).Value = mio_record.id_tariffe_righe

            dropTariffeGeneriche.Items(0).Selected = False
            dropTariffeGeneriche.Items(1).Selected = True
            dropTariffeGeneriche.Enabled = False
            dropTariffeParticolari.Enabled = False

        ElseIf (mio_record.tipo_tariffa & "") = "fonte" Then
            dropTariffeParticolari.Items.Add(mio_record.CODTAR)
            dropTariffeParticolari.Items(1).Value = mio_record.id_tariffe_righe

            dropTariffeParticolari.Items(0).Selected = False
            dropTariffeParticolari.Items(1).Selected = True
            dropTariffeParticolari.Enabled = False
            dropTariffeGeneriche.Enabled = False

        End If

    End Sub

    Dim funzioni As New funzioni_comuni

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

    Public Shared Function get_id_pieno_carburante() As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM condizioni_elementi WITH(NOLOCK) WHERE tipologia='RIMUOVI_RIFORNIMENTO'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test = "" Then
            test = "0"
        End If

        get_id_pieno_carburante = test

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function pieno_carburante_selezionato(ByVal id_contratto As String, ByVal num_calcolo As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(selezionato,'0') FROM contratti_costi WITH(NOLOCK) WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND id_elemento='" & get_id_pieno_carburante() & "'", Dbc)

        pieno_carburante_selezionato = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub listContrattiCosti_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles listContrattiCosti.DataBinding
        'SE IL CONTRATTO E' NELLO STATO "APERTO" OGNI VOLTA CHE AGGIORNO LA TABELLA AGGIORNO, PER GLI ACCESSORI, L'INFORMAZIONE CIRCA
        'LA VENDIBILITA' NOLO IN CORSO (PER FAR SI CHE, SE SU RICHIESTA SE NE ABILITA UNO O PIU', L'UTENTE, AGGIORNANDO LA LISTA CON L'APPOSITO
        'PULSANTE POSSA DOPO SELEZIONARE GLI ACCESSORI AGGIUNTI)


        If statoContratto.Text = "2" Then
            funzioni_comuni.aggiorna_accessori_acquistabili_nolo_in_corso(idContratto.Text, numCalcolo.Text)

            'AGGIORNO GLI ELEMENTI EXTRA
            Dim id_tariffe_righe As String = ""
            If dropTariffeGeneriche.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeGeneriche.SelectedValue
            ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
                id_tariffe_righe = dropTariffeParticolari.SelectedValue
            End If
        End If

        ultimo_gruppo.Text = ""
        intestazione_informativa_da_visualizzare.Text = "1"
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

        'REGOLE SU ACCESSORI A SCELTA ----------------------------------------------------------------------------------------------------
        If obbligatorio.Text = "False" And id_a_carico_di.Text <> Costanti.id_accessorio_incluso Then
            Dim chkScegli As CheckBox = e.Item.FindControl("chkScegli")

            chkScegli.Visible = True
            'NEL CASO IN CUI IL CONTRATTO SIA NELLO STATO "2" (APERTO)
            'SI POSSONO AGGIUNGERE SOLAMENTE ACCESSORI TRA QUELLI CHE SONO STATI SETTATI COME "ACQUISTABILI A NOLO IN CORSO"
            'SE LO STATO E' "3" o "4" (chiuso senzo/con quick check in)
            If statoContratto.Text = "2" Then
                Dim acquistabile_nolo_in_corso As Label = e.Item.FindControl("acquistabile_nolo_in_corso")
                If acquistabile_nolo_in_corso.Text = "False" Then
                    chkScegli.Enabled = False
                End If
            ElseIf statoContratto.Text = "3" Or statoContratto.Text = "4" Then
                chkScegli.Enabled = False
            End If

            'GLI ACCESSORI NON SELEZIONATI NON MOSTRANO IL PREZZO------------------------------------------------------------------------------
            If Not chkScegli.Checked Then
                'valore_costo.Visible = False
                sconto.Visible = False
                costo_scontato.Visible = False
                qta.Visible = False
                costo_unitario.Visible = False
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
        ElseIf id_unita_misura.Text = Costanti.id_unita_misura_giorni Then
            'SE L'L'UNITA' DI MISURA E' AL GIORNO (ACCESSORIO CERTAMENTE NON PACKED)
            'qta.Text = txtNumeroGiorni.Text
            costo_unitario.Text = FormatNumber(CDbl(costo_scontato.Text) / CInt(qta.Text), 2, , , TriState.False)
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

        If id_elemento.Text = Costanti.ID_tempo_km Or LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Or LCase(nome_costo.Text) = "sconto" Then
            nome_costo.Font.Bold = True
        End If

        If id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore Then
            If id_a_carico_di.Text = Costanti.id_accessorio_incluso And id_elemento.Text <> Costanti.ID_tempo_km Then
                'PER GLI ELEMENTI INCLUSI TRANNE IL TEMPO KM NON MOSTRO IL PREZZO (CHE E' SEMPRE ZERO)
                e.Item.FindControl("lblIncluso").Visible = True
                'valore_costo.Visible = False
                sconto.Visible = False
                costo_scontato.Visible = False
                costo_unitario.Visible = False
                qta.Visible = False
            ElseIf id_a_carico_di.Text <> Costanti.id_accessorio_incluso And obbligatorio.Text = "True" And id_elemento.Text <> Costanti.ID_tempo_km And LCase(nome_costo.Text) <> "sconto" Then
                e.Item.FindControl("lblObbligatorio").Visible = True
            End If
        Else
            Dim servizio_rifornimento_tolleranza As Label = e.Item.FindControl("servizio_rifornimento_tolleranza")

            'A CONTRATTO CHIUSO (QUICK-CHECK IN E CHIUSO) LA LABEL INFORMATIVA VIENE SOSTITUITO COL CHECK BOX 
            If statoContratto.Text <> "3" And statoContratto.Text <> "4" Then
                e.Item.FindControl("lblInformativa").Visible = True
            End If

            'PER LE INFORMATIVE NON MOSTRO IL COSTO UNITARIO E LA QUANTITA'
            costo_unitario.Visible = False
            qta.Visible = False

            'IN FASE DI CONTRATTO CHIUSO PER CUI E' POSSIBILE INSERIRE LE INFORMATIVE MOSTRO L'INTESTAZIONE - DO LA POSSIBILITA' DI 
            'SCEGLIERE L'INFORMATIVA PER AGGIUNGERE IL COSTO AL TOTALE
            If (statoContratto.Text = "3" Or statoContratto.Text = "4") Then
                Dim chkScegli As CheckBox = e.Item.FindControl("chkScegli")

                If intestazione_informativa_da_visualizzare.Text = "1" Then
                    e.Item.FindControl("riga_intestazione_informative").Visible = True
                    intestazione_informativa_da_visualizzare.Text = "0"
                End If
                chkScegli.Visible = True
                omaggiato.Visible = True
                chkScegli.Enabled = False
                omaggiato.Enabled = False

                'L'INFORMATIVA "SERVIZIO RIFORNIMENTO", SE SETTATO COME DA GESTIRE IN MANIERA AUTOMATICA (CARBURANTE DENTRO TABELLE LISTINI)
                'NON E' GESTIBILE DALL'UTENTE. SE SETTATO DA GESTIRE IN MANIERA AUTOMATICA, L'EMENENTO IN condizioni_elementi AVRA' IL CAMPO
                'servizio_rifornimento_tolleranza DIVERSO DA NULL
                If servizio_rifornimento_tolleranza.Text <> "" Then
                    chkScegli.Enabled = False
                End If
            End If

            'SE E' STATO ACQUISTATO L'ACCESSORIO PIENO CARBURANTE NON MOSTRO LA PENALITA' SERVIZIO RIFORNIMENTO
            If servizio_rifornimento_tolleranza.Text <> "" Then
                If pieno_carburante_selezionato(idContratto.Text, numCalcolo.Text) Then
                    e.Item.FindControl("riga_elementi").Visible = False
                End If
            End If
        End If

        If LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale) Then
            'NEL CASO DI BROKER IL TOTALE CHE DEVE PAGARE IL CLIENTE E' IL TOTALE CALCOLATO DA SISTEMA MENO IL VALORE TARIFFA A CARICO
            'DEL BROKER (LE ESTENSIONI DEL VALORE TARIFFA POSSONO O MENO ESSERE A CARICO DEL CLIENTE). AGGIORNO QUINDI IL COSTO COL VALORE
            'DA MOSTRARE

            'If tariffa_broker.Text = "1" Then
            '    costo_scontato.Text = CDbl(costo_scontato.Text) - getCostoACaricoDelBroker()
            'End If

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

        'Response.Write(nome_costo.Text & "<br>")

        If gruppo.Text = ultimo_gruppo.Text Then
            e.Item.FindControl("riga_gruppo").Visible = False
            e.Item.FindControl("riga_intestazione").Visible = False
        Else
            e.Item.FindControl("riga_gruppo").Visible = True
            e.Item.FindControl("riga_intestazione").Visible = True
            ultimo_gruppo.Text = gruppo.Text
        End If

        'OMAGGIABILITA' DI ACCESSORI E OBBLIGATORI --------------------------------------------------------------------------------------
        If omaggiabile.Text = "True" And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore Then
            'ABILITO L'OMAGGIABILITA' DI UN ELEMENTO A SCELTA SOLAMENTE SE L'UTENTE HA IL RELATIVO PERMESSO - SOLO PER OBBLIGATORI ED ACCESSORI
            If livello_accesso_omaggi.Text = "3" Then
                omaggiato.Visible = True
                If statoContratto.Text = "2" Or statoContratto.Text = "3" Or statoContratto.Text = "4" Then
                    'PER I CONTRATTI APERTI O CHIUSI NON PERMETTO DI VARIARE L'OMAGGIABILITA' (PER GLI ACCESSORI A SCELTA RESTA ABILITATA SE
                    'E' ACQUISTABILE A NOLO IN CORSO) 
                    If obbligatorio.Text = "False" Then
                        Dim acquistabile_nolo_in_corso As Label = e.Item.FindControl("acquistabile_nolo_in_corso")
                        If acquistabile_nolo_in_corso.Text = "False" Or statoContratto.Text = "3" Or statoContratto.Text = "4" Then
                            omaggiato.Enabled = False
                        End If
                    Else
                        'GLI OBBLIGATORI INVECE NON SONO MAI OMAGGIABILI
                        omaggiato.Enabled = False
                    End If
                End If
            Else
                omaggiato.Visible = True
                omaggiato.Enabled = False
            End If
        End If

        '----------------------------------------------------------------------------------------------------------------------------------
        'TARIFFE(BROKER) : SE L'UTENTE NON HA IL PERMESSO VENGONO NASCOSTI TUTTI I PREZZI E VENGONO VISUALIZZATI SOLO GLI ACCESSORI (SENZA PREZZO)

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

            'If (obbligatorio.Text = "False" And id_a_carico_di.Text <> Costanti.id_accessorio_incluso) Or (LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale)) Then
            '    'PER GLI ELEMENTI A SCELTA (O PER LA RIGA TOTALE DOVE E' PRESENTE IL PULSANTE AGGIUNGI ACCESSORIO) NASCONDO I COSTI
            '    'valore_costo.Visible = False
            '    sconto.Visible = False
            '    costo_scontato.Visible = False
            '    costo_unitario.Visible = False
            '    qta.Visible = False

            '    If (LCase(nome_costo.Text) = LCase(Costanti.testo_elemento_totale)) Then
            '        nome_costo.Visible = False
            '    End If
            'Else
            '    e.Item.FindControl("riga_elementi").Visible = False
            'End If
        End If
    End Sub

End Class
