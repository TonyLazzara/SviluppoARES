Imports funzioni_comuni

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    Dim funzioni As New funzioni_comuni

    'DA AGGIUNGERE ALLA MASTER PAGE

    'Protected Sub logout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles logout.Click
    '    Dim objCookie As HttpCookie = Request.Cookies("SicilyRentCar")
    '    objCookie.Expires = New DateTime(1980, 10, 10)
    '    Response.AppendCookie(objCookie)
    '    Response.Redirect("LogIn.aspx")
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If funzioni_comuni.sql_inj(Request.ServerVariables("SERVER_NAME") & Request.ServerVariables("PATH_INFO") & Request.ServerVariables("QUERY_STRING")) Then
            Response.Redirect("default.aspx")
        End If

        If Request.Cookies("SicilyRentCar") Is Nothing Then
            Response.Redirect("login.aspx")
        Else
            Dim objCookie As HttpCookie = Request.Cookies("SicilyRentCar")
            objCookie.Expires = DateAdd(DateInterval.Minute, 120, Now())
            Response.AppendCookie(objCookie)
        End If

        'CONTROLLO CHE IL COOKIE NON SIA STATO MODIFICATO--------------------------------------------------------------
        Dim test As Integer

        Try
            test = CInt(Request.Cookies("SicilyRentCar")("IdUtente"))
        Catch ex As Exception
            Dim objCookie As HttpCookie = Request.Cookies("SicilyRentCar")
            objCookie.Expires = DateTime.Now.AddDays(-1)
            Response.AppendCookie(objCookie)
            Response.Redirect("login.aspx")
        End Try
        '--------------------------------------------------------------------------------------------------------------

        If Not Page.IsPostBack Then
            'CONTROLLO DI ACCESSIBILITA' DELLE FUNZIONALITA'-----------------------------------------------------------

            'OPERATORI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.PermessiOperatori) = "1" Then
                permessi_operatori.Visible = False
                'profili_operatori.Visible = False
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.AnagraficaOperatoreAccesso) = "1" Then
                anagrafica_operatori.Visible = False
            End If

            'permesso “Fatturazione Modifica Contatore RA”  non devono vedere la voce “Contatori" in Gestione DATI aggiunto 10.05.2021 da punto 9 step 2
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Contatore_ra) = "1" Then
                contatori.Visible = False
            End If
            'permesso “Censimento POS”  non devono vedere "Gestione POS" in Gestione DATI aggiunto 10.05.2021 da punto 9 step 2
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.CensimentoPOS) = "1" Then
                tabelle_pos.Visible = False
            End If



            'PARCO VEICOLI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.ricercaVeicoli) = "1" Then
                parcoVeicoli.Visible = False
            End If
            'TABELLE AUTO
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.TabelleVeicoli) = "1" Then
                tabelle_auto.Visible = False
            End If
            'GESTIONE STAZIONI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneStazioni) = "1" Then
                tabelle_stazioni.Visible = False
            End If
            'GESTIONE DITTE
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.TabelleDitte) = "1" Then
                tabelle_ditte.Visible = False
            End If
            'GESTIONE CONDUCENTI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.TabelleConducenti) = "1" Then
                tabelle_conducenti.Visible = False
            End If
            'GESTIONE GPS
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneGps) = "1" Then
                gestione_gps.Visible = False
            End If
            'CAR CONTROL
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.PrevisioneStazione) = "1" Then
                previsione_stazione.Visible = False
            End If
            'BISARCA
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Bisarca) = "1" Then
                Bisarca.Visible = False
            End If
            'ORDINATIVO LAVORI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.ODL) = "1" Then
                ordinativo_lavori.Visible = False
            End If
            'TABELLE LISTINI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.TabelleListini) = "1" Then
                tabelle_listini.Visible = False
            End If
            'GESTIONE LISTINI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneListini) = "1" Then
                gestione_listini.Visible = False
            End If
            'TARIFFE
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Tariffe) = "1" Then
                crea_tariffa.Visible = False
            End If
            'TEMPO KM
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.TempoKm) = "1" Then
                Gest_Tempo_Km.Visible = False
            End If
            'CONDIZIONI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Condizioni) = "1" Then
                Gest_Condizioni.Visible = False
            End If
            'GESTIONE VAL
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneVal) = "1" Then
                gestione_val.Visible = False
            End If
            'STOP SALE
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.StopSale) = "1" Then
                stop_sale.Visible = False
            End If
            'DANNI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneDanni) = "1" Then
                gestione_danni.Visible = False
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneTabelleDanni) = "1" Then
                tabelle_danni.Visible = False
            End If
            'AMMORTAMENTO 
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Ammortamenti) = "1" Then
                ammortamento.Visible = False
            End If
            'GESTIONE ASSICURAZIONI 
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Assicurazioni) = "1" Then
                assicurazioni.Visible = False
            End If
            'MOVIMENTI VEICOLI 
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.MovimentiVeicoli) = "1" Then
                movimenti_veicoli.Visible = False
            End If
            'MODIFICA KM VETTURA 
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.ModificaKmVettura) = "1" Then
                modifica_km.Visible = False
            End If
            'BOLLI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.BolliAuto) = "1" Then
                bolli.Visible = False
            End If
            'Bollo Singolo 
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.BolliAuto) = "1" Then
                bollo_singolo.Visible = False
            End If
            'DDT
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.TrasferimentoVeicoli) = "1" Then
                Trasferimenti.Visible = False
            End If
            'PREVISIONE MEZZI SPECIALI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.PrevisionePerTarga) = "1" Then
                mezzi_speciali.Visible = False
            End If
            'GESTIONE POS
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestionePos) = "1" Then
                tabelle_pos.Visible = False
            End If
            'RIBALTAMENTO PRENOTAZIONI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.RibaltamentoPrenotazioni) = "1" Then
                ribaltamento_prenotazioni.Visible = False
            End If

            'CONTABILITA FATTURE
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneFatture) = "1" Then
                gestione_fatture_nolo.Visible = False
                gestione_fatture.Visible = False
                esporta_fatture_nolo.Visible = False
            End If
            'RIEPILOGO DI CASSA
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneCassa) = "1" Then
                gestione_cassa.Visible = False
            End If
            ' PETTY CASH
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneBustaPettyCash) = "1" Then
                gestione_petty_cash.Visible = False
            End If
            ' SOSPESI DI CASSA
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneBustaPettyCash) = "1" Then
                gestione_sospesi_cassa.Visible = False
            End If

            'COMMISSIONI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Commissioni) = "1" Then
                commissioni_operatore.Visible = False
                commissioni_stazione.Visible = False
            End If

            'PREVENTIVI/PRENOTAZIONI/CONTRATTI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.PreventiviPrenotazioni) = "1" Then
                preventivi_prenotazioni.Visible = False
            End If

            'TIPO ALLEGATI PRENOTAZIONI/CONTRATTI LEGATI A ANAGRAFICA OPERATORE ADMIN SU RICHIESTA DI FRANCESCO
            'If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.AnagraficaOperatoreAdmin) <> "3" Then 'messo in rem 21.05.2021
            '    tipo_allegati.Visible = False
            'End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.tipo_allegati) = "1" Then 'Aggiuntonuovo 24.05.2021
                tipo_allegati.Visible = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.esportazione_dati_xml) = "1" Then 'Aggiuntonuovo 21.05.2021
                Li1.Visible = False
            End If


            'GESTIONE SINISTRI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneSinistri) = "1" Then
                gestione_sinistri.Visible = False
            End If

            'GESTIONE MULTE
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneMulte) = "1" Then
                gestione_multe.Visible = False
            End If

            'GESTIONE ODL
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneODL) = "1" Then
                gestione_odl.Visible = False
            End If

            ' Riepilogo Pagamenti POS
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Riepilogo_Pagamenti_POS) = "1" Then
                riepilogo_pagamenti_pos.Visible = False
            End If

            'RIFORNIMENTI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Rifornimenti) = "1" Then
                gestione_rifornimenti.Visible = False
            End If

            'LAVAGGI
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneLavaggi) = "1" Then
                gestione_lavaggi.Visible = False
            End If
            '----------------------------------------------------------------------------------------------------------

            'REGOLE DI VISIBILITA' IN BASE ALLA VISIBILITA' DI SOTTO MENU

            'SOTTOMENU TABELLE
            If tabelle_listini.Visible = "False" And tabelle_auto.Visible = "False" And tabelle_stazioni.Visible = "False" And tabelle_pos.Visible = "False" And gestione_val.Visible = "False" And tabelle_ditte.Visible = "False" And tabelle_conducenti.Visible = "False" And tipo_allegati.Visible = "False" Then
                tabelle.Visible = False
            End If
            'SOTTOMENU GESTIONE LISTINI
            If crea_tariffa.Visible = "False" And Gest_Tempo_Km.Visible = "False" And Gest_Condizioni.Visible = "False" Then
                gestione_listini.Visible = False
            End If
            'GESTIONE FLOTTA
            If parcoVeicoli.Visible = "False" And ammortamento.Visible = "False" And bolli.Visible = "False" And Trasferimenti.Visible = "False" And previsione_stazione.Visible = "False" And mezzi_speciali.Visible = "False" And stop_sale.Visible = "False" And movimenti_veicoli.Visible = False And assicurazioni.Visible = False And bolli.Visible = False And gestione_danni.Visible = False And gestione_sinistri.Visible = False And gestione_multe.Visible = "False" And gestione_odl.Visible = "False" And gestione_rifornimenti.Visible = "False" And gestione_lavaggi.Visible = False And riepilogo_pagamenti_pos.Visible = "False" And Bisarca.Visible = "False" And modifica_km.Visible = "False" Then
                flotta.Visible = False
            End If
            'PREVENTIVI PRENOTAZIONI CONTRATTI
            If preventivi_prenotazioni.Visible = "False" And ribaltamento_prenotazioni.Visible = "False" Then
                menu_preventivi_prenotazioni_contratti.Visible = False
            End If
            ' MENU CONTABILITA'
            If gestione_fatture.Visible = False And gestione_fatture_nolo.Visible = False And esporta_fatture_nolo.Visible = False And gestione_cassa.Visible = False And gestione_petty_cash.Visible = False And gestione_sospesi_cassa.Visible = False And commissioni_operatore.Visible = False And commissioni_stazione.Visible = False Then
                menu_contabilita.Visible = False
            End If

            'MENU OPERATORI
            If permessi_operatori.Visible = False And anagrafica_operatori.Visible = False Then
                operatori2.Visible = False
            End If

        End If

        'aggiunto salvo 17.01.2023
        If Request.Cookies("SicilyRentCar")("idUtente") = "5" Or Request.Cookies("SicilyRentCar")("idUtente") = "8" Then
            firma_update.Visible = True
        Else
            firma_update.Visible = False
        End If




        'salva_log()
    End Sub

    Protected Sub salva_log()
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO utenti_clog (id_utente, nominativo, data, pagina, ip) VALUES ('" & Request.Cookies("SicilyRentCar")("IdUtente") & "',(SELECT ISNULL(cognome,'') + ' ' + ISNULL(nome,'') FROM operatori WHERE id='" & Request.Cookies("SicilyRentCar")("IdUtente") & "'),GetDate(),'" & Replace(Request.CurrentExecutionFilePath, "'", "''") & "','" & Replace(Request.UserHostAddress(), "'", "''") & "')", Dbc)

        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub
End Class

