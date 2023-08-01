
Partial Class gestione_danni_SelezionaLetteraRDS
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'If Session("StampaLetteraRDS_id_evento") Is Nothing Then
        '    Libreria.genUserMsgBox(Page, "Parametro non corretto." & vbCrLf & "Impossibile stampare.")
        'End If
        If Session("StampaLetteraRDS_id_evento") Is Nothing Then
            lb_id_evento.Text = Request.QueryString("id_evento") & ""
        Else
            lb_id_evento.Text = Session("StampaLetteraRDS_id_evento")
        End If
        Trace.Write("gestione_danni_SelezionaLetteraRDS.Page_Load " & lb_id_evento.Text)
        If lb_id_evento.Text = "" Then
            Libreria.genUserMsgBox(Page, "Parametro non corretto." & vbCrLf & "Impossibile stampare.")
        End If
        'Libreria.genUserMsgBox(Page, lb_id_evento.Text)
    End Sub

    Protected Function getComuneAres(id_comune As Integer) As String
        Dim sqlStr As String = "SELECT comune + ' (' + provincia + ')' FROM comuni_ares WITH(NOLOCK)" & _
            " WHERE id = " & id_comune

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getComuneAres = Cmd.ExecuteScalar() & ""
            End Using
        End Using
    End Function

    Protected Sub StampaLetterRDS(mio_evento As veicoli_evento_apertura_danno, tipo_lettera As tipo_lettera_rds, linguaggio As tipo_linguaggio_rds)

        Dim mio_record As DatiContratto = DatiContratto.getRecordDaNumContratto(mio_evento.id_documento_apertura, mio_evento.num_crv)

        Dim my_id As Integer

        Try
            Dim test As Integer = mio_record.id_primo_conducente
            my_id = mio_record.id_primo_conducente
        Catch ex As Exception
            my_id = -1
        End Try

        Dim sqlStr As String = "SELECT * FROM CONDUCENTI WITH(NOLOCK) WHERE ID_CONDUCENTE = " & my_id

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader
                    Dim miei_dati As DatiStampaLetteraRDS = New DatiStampaLetteraRDS
                    With miei_dati
                        If Rs.Read Then
                            .dati_conducente = Rs("Nominativo") & vbCrLf

                            If (Rs("Indirizzo") & "") <> "" Then
                                .dati_conducente += Rs("Indirizzo") & vbCrLf
                            End If

                            Dim comune As String = ""
                            If Not (Rs("id_comune_ares") Is DBNull.Value) Then
                                comune = getComuneAres(Rs("id_comune_ares")) & ""
                            End If
                            If comune = "" Then
                                comune = Rs("City") & " (" & Rs("PROVINCIA") & ")"
                            End If
                            .dati_conducente += comune

                            .num_contratto = mio_record.num_contratto
                            .data_contratto = mio_record.data_uscita
                            .data_documento = Format(Now(), "dd/MM/yyyy")
                            .targa = mio_record.targa
                            .num_rds = mio_evento.id_rds
                            If linguaggio = tipo_linguaggio_rds.italiano Then
                                If tipo_lettera And tipo_lettera_rds.NoKasko Then
                                    .importo = Format(mio_evento.importo, "0.00")
                                Else
                                    .importo = Format(mio_evento.importo, "0.00") '& " + I.V.A."    'tolto perchè già presente nel testo del PDF - salvo 24.11.2022
                                End If
                            ElseIf linguaggio = tipo_linguaggio_rds.inglese Then
                                If tipo_lettera And tipo_lettera_rds.NoKasko Then
                                    .importo = Format(mio_evento.importo, "0.00")
                                Else
                                    .importo = Format(mio_evento.importo, "0.00") '& " + V.A.T."    'tolto perchè già presente nel testo del PDF - salvo 24.11.2022
                                End If
                            End If
                            .iva = Libreria.getAliquotaIVADaId(Costanti.iva_default)
                            If mio_evento.spese_postali Is Nothing Then
                                .spese_postali = "0,00"
                            Else
                                .spese_postali = Format(mio_evento.spese_postali, "0.00")
                            End If
                        Else
                            .dati_conducente = ""

                            .num_contratto = ""
                            .data_contratto = ""
                            .data_documento = ""
                            .targa = ""
                            .num_rds = mio_evento.id_rds
                            .importo = Format(mio_evento.importo, "0.00")
                            .iva = Libreria.getAliquotaIVADaId(Costanti.iva_default)
                            If mio_evento.spese_postali Is Nothing Then
                                .spese_postali = "0,00"
                            Else
                                .spese_postali = Format(mio_evento.spese_postali, "0.00")
                            End If
                        End If

                        .linguaggio = linguaggio

                        .motivazione = ""

                        Dim testo_voce As String = ""


                        Select Case linguaggio
                            Case tipo_linguaggio_rds.italiano

                                'Addebito danni
                                If tipo_lettera And tipo_lettera_rds.FranchigiaParziale Then '---> Addebito danni
                                    'old
                                    '"Qualora dovesse risultare una differenza in Suo favore effettueremo un rimborso su carta di credito, in alternativa Le chiediamo di inviarci il Suo codice Iban e Bic per un rimborso con bonifico bancario." & vbCrLf

                                    'Tony 06-03-2023
                                    'testo_voce = "L'addebito applicato è dovuto alla franchigia non coperta dalle garanzie da Lei sottoscritte " &
                                    '"alla stipula del contratto di noleggio. Qualora dovesse risultare una differenza in Suo favore effettueremo uno " &
                                    '"storno su carta, in alternativa Le chiediamo di inviarci il Suo codice Iban e Bic per un rimborso con bonifico bancario. "

                                    testo_voce = "a seguito dei danni riscontrati alla vettura da Lei noleggiata, in riferimento all’Art. 4 delle Condizioni Generali, l’importo sopra indicato è dovuto alla franchigia danni non coperta dalle garanzie da Lei sottoscritte alla stipula del contratto di noleggio. "
                                    'FINE Tony

                                    .motivazione += testo_voce

                                End If

                                'Addebito Franchigia
                                If tipo_lettera And tipo_lettera_rds.NoKasko Then 'Franchigia ---> Addebito Franchigia

                                    'Tony 06-03-2023
                                    'New dal 24.11.2022 salvo
                                    'testo_voce = "L'addebito applicato è dovuto alla franchigia non coperta dalle garanzie da Lei sottoscritte alla stipula del contratto di noleggio."

                                    testo_voce = "a seguito dei danni riscontrati alla vettura da Lei presa a noleggio, in riferimento all’Art. 4 delle Condizioni Generali, l’importo sopra indicato è relativo alla franchigia danni a Suo carico come da contratto di noleggio sottoscritto."
                                    'FINE Tony

                                    .motivazione += testo_voce

                                End If

                                'Addebito danni esclusi
                                If tipo_lettera And tipo_lettera_rds.NoDichiarazione Then

                                    'Tony 07-03-2023
                                    '.motivazione += "L'importo di cui sopra le viene ascritto per danni subiti dal nostro veicolo" &
                                    '   " a causa del sinistro occorsole e con la più ampia riserva di richiederle un importo maggiore" &
                                    '   " per ogni eventuale richiesta di risarcimento da parte di possibili controparti ad oggi non note" &
                                    '   " e non menzionate. L'addebito di cui sopra è motivato dal fatto che lei non ci ha rilasciato" &
                                    '   " alcuna dichiarazione riguardante i predetti danni, disattendendo quanto disposto dall'art. 2.4" &
                                    '   " delle condizioni generali del contratto di noleggio di cui all'oggetto." & vbCrLf

                                    testo_voce = "in riferimento all’Art. 4 delle Condizioni Generali di noleggio, la garanzia accessoria sottoscritta non prevede la copertura per i danni riscontrati alla vettura da Lei noleggiata, in quanto esclusi."
                                    'FINE Tony

                                    .motivazione += testo_voce
                                End If

                                'Dolo colpa grave
                                If tipo_lettera And tipo_lettera_rds.Imperizia Then

                                    'Tony 06-03-2023
                                    '.motivazione += "Per imperizia e grave negligenza nella conduzione dell'autoveicolo da lei preso a nolo," &
                                    '" come stabilito dall'art. 2.4 del contratto di noleggio da lei sottoscritto." & vbCrLf

                                    testo_voce = "per dolo e/o colpa grave nella conduzione del veicolo, come stabilito dall’Art. 3 e dall’Art. 4 delle Condizioni Generali del contratto di noleggio sottoscritto."
                                    'FINE Tony

                                    .motivazione += testo_voce
                                End If

                                'Furto
                                If tipo_lettera And tipo_lettera_rds.Furto Then

                                    'Tony 06-03-2023
                                    '.motivazione += "Il suddetto addebito, come stabilito dall'art. 4.1 delle condizioni generali del contratto di cui all'oggetto," &
                                    '   " è motivato dal fatto che lei non ha sottoscritto la copertura assicurativa inerente alla garanzia furto/incendio." & vbCrLf

                                    testo_voce = "a seguito del furto della vettura da Lei presa a noleggio, in riferimento all’Art. 4 delle Condizioni Generali, l’importo sopra indicato è dovuto alla franchigia furto a Suo carico come da contratto di noleggio sottoscritto."
                                    'FINE Tony

                                    .motivazione += testo_voce
                                End If

                                'Campania
                                If tipo_lettera And tipo_lettera_rds.PaesiVietati Then

                                    'Tony 06-03-2023
                                    '.motivazione += "Il veicolo da lei noleggiato è stato condotto in uno Stato in cui, come stabilito" &
                                    '   " dall'art. 2.3 del contratto di cui all'oggetto, è fatto divieto assoluto di transito." & vbCrLf

                                    testo_voce = "a seguito dei danni riscontrati alla vettura da Lei presa a noleggio, come stabilito dall’ Art. 4 delle Condizioni Generali, seppure Lei abbia sottoscritto la garanzia accessoria che limita o elimina la responsabilità per danni, furto e incendio al veicolo, in conseguenza che l’evento dannoso si è verificato nel territorio della regione Campania, la responsabilità a suo carico rimane in solido fino ad un importo pari al 50% del valore della franchigia prevista nella tariffa base."
                                    'vbCrLf &
                                    'FINE Tony

                                    .motivazione += testo_voce
                                End If


                                '---------------------
                                'Non visibili

                                If tipo_lettera And tipo_lettera_rds.AttiVandalici Then  '---> Addebito atti vandalici

                                    'OLD
                                    'testo_voce = "Per atti vandalici, non coperti dalle garanzie da Lei accettate, come stabilito al punto 5" &
                                    '    " delle condizioni generali di noleggio da Lei sottoscritte alla stipula del contratto." & vbCrLf & vbCrLf &
                                    '    "Qualora dovesse risultare una differenza in Suo favore effettueremo un rimborso su carta di credito, in alternativa Le chiediamo di inviarci il Suo codice Iban e Bic per un rimborso con bonifico bancario." & vbCrLf

                                    'New 24.11.2022
                                    testo_voce = "L'addebito applicato è dovuto ad atti vandalici, non coperti dalle garanzie da Lei sottoscritte, " &
                                        "come stabilito al punto 6 delle condizioni generali, alla stipula del contratto di noleggio. " &
                                    "Qualora dovesse risultare una differenza in Suo favore effettueremo uno storno su carta, in alternativa " &
                                    "Le chiediamo di inviarci il Suo codice Iban e Bic per un rimborso con bonifico bancario. "

                                    .motivazione += vbCrLf & testo_voce

                                End If
                                
                                If tipo_lettera And tipo_lettera_rds.ErratoRifornimento Then
                                    .motivazione += "Per errato rifornimento, ai sensi dell'art. 2.2 del contratto di noleggio da lei sottoscritto." & vbCrLf
                                End If

                                If tipo_lettera And tipo_lettera_rds.Ruote Then
                                    .motivazione += "Per danni causati a parti del veicolo non coperti da nessun tipo di assicurazione: Ruote [  ] - Sottoscocca [  ] - Selleria ed Accessori [  ]" &
                                        " - Tetto [  ] - Parabrezza Lunotto [  ] - Chiavi [  ] - Frizione Bruciata. dovuta ad un uso improprio del veicolo [  ]." & vbCrLf
                                End If
                                
                                If tipo_lettera And tipo_lettera_rds.AttiVandalici Then  '---> Addebito atti vandalici

                                    'OLD
                                    'testo_voce = "Per atti vandalici, non coperti dalle garanzie da Lei accettate, come stabilito al punto 5" &
                                    '    " delle condizioni generali di noleggio da Lei sottoscritte alla stipula del contratto." & vbCrLf & vbCrLf &
                                    '    "Qualora dovesse risultare una differenza in Suo favore effettueremo un rimborso su carta di credito, in alternativa Le chiediamo di inviarci il Suo codice Iban e Bic per un rimborso con bonifico bancario." & vbCrLf

                                    'New 24.11.2022
                                    testo_voce = "L'addebito applicato è dovuto ad atti vandalici, non coperti dalle garanzie da Lei sottoscritte, " &
                                        "come stabilito al punto 6 delle condizioni generali, alla stipula del contratto di noleggio. " &
                                    "Qualora dovesse risultare una differenza in Suo favore effettueremo uno storno su carta, in alternativa " &
                                    "Le chiediamo di inviarci il Suo codice Iban e Bic per un rimborso con bonifico bancario. "
                                    .motivazione += testo_voce & vbCrLf

                                End If

                                If tipo_lettera And tipo_lettera_rds.GuidaNonAutorizzata Then
                                    .motivazione += "Per guida non autorizzata, come stabilito dall'art. 2.3 del contratto di noleggio da lei sottoscritto." & vbCrLf
                                End If

                                If tipo_lettera And tipo_lettera_rds.SinistroAttivo Then
                                    .motivazione += "Per danni causati al veicolo a seguito del sinistro da lei dichiarato attivo, come stabilito" &
                                        " dall'art. 2.4, lei non ha sottoscritto la copertura assicurativa totale." &
                                        " Dopo aver ottenuto il risarcimento del danno dalla compagnia assicuratrice della controparte" &
                                        " provvederemo a rimborsarle la suddetta somma." & vbCrLf
                                End If

                                If tipo_lettera And tipo_lettera_rds.DichiarazioneParziale Then
                                    .motivazione += "Per non averci fornito i dati esatti della controparte che non ci permettono il recupero" &
                                        " dei danni subiti dalla vettura da lei condotta, come stabilito dall'art. 2.4 e 2.5" &
                                        " del contratto di noleggio da lei sottoscritto." & vbCrLf
                                End If
                                


                                'INGLESE
                            Case tipo_linguaggio_rds.inglese

                                'Addebito danni
                                If tipo_lettera And tipo_lettera_rds.FranchigiaParziale Then 'Addebito danni 24.11.2022

                                    'old
                                    'vbCrLf & "Should be a difference in your favour, kindly send us your bank details, Iban and Bic code, as soon as possible." & vbCrLf

                                    'Tony 07-03-2023
                                    'testo_voce = "The charge is due to the excess not covered by the guarantees you have signed with " &
                                    '   "the rental agreement. In case of difference in your favour we’ll charge the amount back to your card, " &
                                    '   "or we’ll request your IBAN and BIC to refund you by bank transfer."

                                    testo_voce = "as a result of damage to the vehicle rented by you, with reference to Art. 4 of the General Terms and Conditions, the amount indicated above is due to the excess of damage not covered by the guarantees you subscribed to when the rental agreement was signed."
                                    .motivazione += testo_voce & vbCrLf
                                    'FINE Tony

                                End If

                                'Addebito franchigia: 
                                If tipo_lettera And tipo_lettera_rds.NoKasko Then 'Addebito franchigia

                                    'OLD text
                                    '"The charges is due to the residual part not covered by guarantee you accepted at the beginning of the rental." & vbCrLf

                                    'Tony 07-03-2023
                                    '24.11.2022 - salvo
                                    'testo_voce = "The charge is due to the excess not covered by the guarantees you have signed with the rental agreement."

                                    testo_voce = "as a result of damage to the vehicle you rented, with reference to Art. 4 of the General Conditions, the amount indicated above is due to the excess of damage charged to you as per the rental agreement signed."
                                    .motivazione += testo_voce & vbCrLf
                                    'FINE Tony
                                End If

                                'Addebito danni esclusi: 
                                If tipo_lettera And tipo_lettera_rds.NoDichiarazione Then

                                    'Tony 07-03-2023
                                    '.motivazione += "The charges was due to damages on the vehicle rented by you, following to an accident occurred. " &
                                    '    "We also reserve all the right to require you further amounts, in case of possible refund request " &
                                    '    "from third parties who are unknown at today’s date." & vbCrLf &
                                    '    "The above mentioned charge is also due, because you did not leave us any declaration/report of the " &
                                    '    "damages occurred to the vehicle and/or the dynamic of the accident, as it is stated by point 2.4 " &
                                    '    "reported in general rental terms and condition on the back of the rental agreement, you have signed for acceptance. " & vbCrLf

                                    testo_voce = "with reference to Art. 4 of the General Rental Terms and Conditions, the undersigned ancillary guarantee does not provide cover for damage to the vehicle rented by you, as this is excluded."
                                    .motivazione += testo_voce & vbCrLf
                                    'FINE Tony
                                End If

                                'Dolo colpa grave
                                If tipo_lettera And tipo_lettera_rds.Imperizia Then

                                    'Tony 07-03-2023
                                    '.motivazione += "The charges was due to negligence and careless driving of vehicle rented by you, as it is stated " &
                                    '    "by point 2.4 reported in general rental terms and condition on the back of the rental agreement, " &
                                    '    "you have signed for acceptance." & vbCrLf

                                    testo_voce = "for intent and/or gross negligence while driving the vehicle, as established in Art. 3 and Art. 4 of the General Conditions of the undersigned rental agreement."
                                    .motivazione += testo_voce & vbCrLf
                                    'FINE Tony
                                End If

                                'Furto
                                If tipo_lettera And tipo_lettera_rds.Furto Then

                                    'Tony 07-03-2023
                                    '.motivazione += "The charges is due to the fact that you did not sign for the insurance coverage for theft/fire, " &
                                    '    "as stated by the article 4.1 of the rental agreement general conditions." & vbCrLf

                                    testo_voce = "following the theft of the vehicle rented by you, with reference to Art. 4 of the General Conditions, the amount indicated above is due to the theft deductible payable by you as per the rental agreement signed."
                                    .motivazione += testo_voce & vbCrLf
                                    'FINE Tony
                                End If

                                'Campania:
                                If tipo_lettera And tipo_lettera_rds.PaesiVietati Then

                                    'Tony 07-03-2023
                                    '.motivazione += "The charges was applied because the rented vehicle was taken to a country where it was forbidden " &
                                    '    "take the vehicle to, as it is stated by point 2.3 reported in general rental terms and condition " &
                                    '    "on the back of the rental agreement in ref." & vbCrLf

                                    testo_voce = "following the damage to the vehicle you have rented, as established in Art. 4 of the General Conditions, even if you have subscribed to the additional guarantee that limits or eliminates liability for damage, theft and fire to the vehicle, as a result of the damaging event occurring in the territory of the Campania region, the liability due to you shall remain jointly and severally liable up to an amount equal to 50% of the value of the excess indicated in the base rate."
                                    .motivazione += testo_voce & vbCrLf
                                    'FINE Tony
                                End If


                                '------------
                                'NON visibili

                                If tipo_lettera And tipo_lettera_rds.ErratoRifornimento Then
                                    .motivazione += "The charges is due to a wrong refuelling applied to the here above vehicle, as per the article 2.2 " &
                                        "of the rental agreement general conditions." & vbCrLf
                                End If
                                If tipo_lettera And tipo_lettera_rds.Ruote Then
                                    .motivazione += "The charges was due to damages occurred to the not insured parts of the rented vehicle, such as: " &
                                        "[  ] tyres - [  ] underside parts of the vehicle - [  ] tapestry and internal parts of the vehicle - " &
                                        "[  ] roof - [  ] windscreen and glasses in general - [  ] car keys - [  ] vehicle plates - " &
                                        "[  ] burnt clutch, due to the incorrect customer driving technique" & vbCrLf
                                End If

                                
                                If tipo_lettera And tipo_lettera_rds.AttiVandalici Then '24.11.2022 salvo

                                    'OLD
                                    '"The charges is due to the acts of vandalism on rented vehicle. As per the article 5 of the " &
                                    '    "rental agreement general condition, these damages are not covered by insurance." & vbCrLf & vbCrLf &
                                    '    "Should be a difference in your favour, kindly send us your bank details, Iban and Bic code, as soon as possible." & vbCrLf

                                    'new 24.11.2022 salvo
                                    testo_voce = "The charge is due to vandalism not covered by the guarantees you have signed with the rental " &
                                        "agreement, as per point 6 from general conditions. " &
                                        "In case of difference in your favour we’ll charge " &
                                        "the amount back to your card, or we’ll request your IBAN and BIC to refund you by bank transfer."

                                    .motivazione += testo_voce & vbCrLf

                                End If

                                If tipo_lettera And tipo_lettera_rds.GuidaNonAutorizzata Then
                                    .motivazione += "The charges is due to the fact that name of person who was driving the vehicle was not " &
                                        "registered on the rental agreement (as stated by the article 2.3 of general condition)." & vbCrLf
                                End If
                                If tipo_lettera And tipo_lettera_rds.SinistroAttivo Then
                                    .motivazione += "The charges is to the damages sustained to the vehicle following to the accident occurred " &
                                        "(whose fault is seemingly not yours) and was applied because you did not sign for the full coverage insurance." &
                                        "As soon as we get compensation for the damages by the third party Insurance, we will refund you for the above amount." & vbCrLf
                                End If

                                If tipo_lettera And tipo_lettera_rds.DichiarazioneParziale Then
                                    .motivazione += "The charges was due for not giving us the third party details which do not allow us to be refunded " &
                                        "by the third party, for the damages occurred to the vehicle rented by you, as it is stated by points 2.4 and 2.5 " &
                                        "reported in general rental terms and condition on the back of rental agreement , you have signed for acceptance." & vbCrLf
                                End If
                                

                        End Select


                    End With

                    Session("DatiStampaLetteraRDS") = miei_dati

                    Dim num_random As String = Format((New System.Random()).Next(100000000), "000000000")

                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('/GeneraLetteraRDS.aspx?a=" & num_random & "','')", True)
                    End If

                End Using
            End Using
        End Using
    End Sub

    Protected Sub bt_stampa_Click(sender As Object, e As System.EventArgs) Handles bt_stampa.Click
        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(Integer.Parse(lb_id_evento.Text))

        Dim linguaggio As tipo_linguaggio_rds
        If rb_linguaggio.SelectedValue = 2 Then
            linguaggio = tipo_linguaggio_rds.inglese
        Else
            linguaggio = tipo_linguaggio_rds.italiano
        End If

        Dim tipo_lettera As tipo_lettera_rds = 0
        If ck_no_kasko.Checked Then
            tipo_lettera = tipo_lettera Or tipo_lettera_rds.NoKasko
        End If
        If ck_no_dichiarazione.Checked Then
            tipo_lettera = tipo_lettera Or tipo_lettera_rds.NoDichiarazione
        End If
        If ck_furto.Checked Then
            tipo_lettera = tipo_lettera Or tipo_lettera_rds.Furto
        End If
        If ck_errato_rifornimento.Checked Then
            tipo_lettera = tipo_lettera Or tipo_lettera_rds.ErratoRifornimento
        End If
        If ck_ruote.Checked Then
            tipo_lettera = tipo_lettera Or tipo_lettera_rds.Ruote
        End If
        If ck_imperizia.Checked Then
            tipo_lettera = tipo_lettera Or tipo_lettera_rds.Imperizia
        End If
        If ck_atti_vandalici.Checked Then
            tipo_lettera = tipo_lettera Or tipo_lettera_rds.AttiVandalici
        End If
        If ck_guida_non_autorizzata.Checked Then
            tipo_lettera = tipo_lettera Or tipo_lettera_rds.GuidaNonAutorizzata
        End If
        If ck_sinistro_attivo.Checked Then
            tipo_lettera = tipo_lettera Or tipo_lettera_rds.SinistroAttivo
        End If
        If ck_franchigia_parziale.Checked Then
            tipo_lettera = tipo_lettera Or tipo_lettera_rds.FranchigiaParziale
        End If
        If ck_dichiarazione_parziale.Checked Then
            tipo_lettera = tipo_lettera Or tipo_lettera_rds.DichiarazioneParziale
        End If
        If ck_paesi_vietati.Checked Then
            tipo_lettera = tipo_lettera Or tipo_lettera_rds.PaesiVietati
        End If

        StampaLetterRDS(mio_evento, tipo_lettera, linguaggio)

        Dim mia_variazione_stato As veicoli_stato_rds_variazione = New veicoli_stato_rds_variazione
        Dim stato_old As sessione_danni.stato_rds = mio_evento.stato_rds
        mio_evento.stato_rds = sessione_danni.stato_rds.Stampa_lettera_al_cliente
        ' In effetti non cambio lo stato dell'rds.. mi serve solo per salvare l'operazione di stampa nella variabile
        ' questa viene utilizzata nel report dell'rds
        With mia_variazione_stato
            mia_variazione_stato.InitDati(mio_evento, stato_old)
            mia_variazione_stato.SalvaRecord()
        End With
    End Sub

End Class
