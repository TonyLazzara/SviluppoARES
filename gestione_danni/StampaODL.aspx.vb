

Partial Class gestione_danni_RiepilogoRDS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim num_odl As String = Request.QueryString("num_odl")

        lb_num_odl.Text = num_odl
        listViewElencoDanniPerEvento.DataBind()

        Dim mio_odl As odl = odl.getRecordDaNumODL(Integer.Parse(num_odl))
        InitODL(mio_odl)

        Dim mio_veicolo As tabella_veicoli = tabella_veicoli.get_record_da_id(mio_odl.id_veicolo)
        InitIntestazione(mio_veicolo)

        lb_id_veicolo.Text = mio_odl.id_veicolo
    End Sub

    Protected Sub InitODL(ByVal mio_odl As odl)
        With mio_odl
            ' Intestazione
            lb_numero_odl.Text = .num_odl & ""
            lb_data_odl.Text = Libreria.myFormatta(.data_odl, "dd/MM/yyyy")
            If .id_stazione_uscita Is Nothing Then
                lb_stazione.Text = "(N.V.)"
            Else
                lb_stazione.Text = Libreria.getNomeStazioneDaId(.id_stazione_uscita)
            End If

            ' Fornitore
            Dim mio_fornitore As fornitore = Nothing
            If .id_fornitore IsNot Nothing Then
                mio_fornitore = fornitore.getRecordDaId(.id_fornitore)
            End If
            If mio_fornitore IsNot Nothing Then
                With mio_fornitore
                    lb_fonitore.Text = .ragione_sociale
                    lb_fonitore_indirizzo.Text = .indirizzo
                    lb_fonitore_citta.Text = .comune
                    If Not (.cap Is Nothing OrElse .cap = "") Then
                        lb_fonitore_citta.Text += " (" & .cap & ")"
                    End If
                    lb_fonitore_telefono.Text = .telefono & ""
                End With
            End If

            ' Sezione documento apertura
            lb_num_rds.Text = .num_rds & ""
            If .id_tipo_doc_apertura IsNot Nothing Then
                Dim id_tipo_documento As tipo_documento = .id_tipo_doc_apertura
                Select Case id_tipo_documento
                    Case tipo_documento.Contratto
                        lb_tipo_documento.Text = "RA"
                    Case tipo_documento.MovimentoInterno
                        lb_tipo_documento.Text = "M.I."
                    Case tipo_documento.ODL
                        lb_tipo_documento.Text = "ODL"
                    Case tipo_documento.DuranteODL
                        lb_tipo_documento.Text = "DODL"
                    Case Else
                        lb_tipo_documento.Text = "##"
                End Select
            Else
                lb_tipo_documento.Text = "##"
            End If
            lb_num_documento.Text = .id_doc_apertura & ""

            ' Dati uscita
            lb_stazione_out.Text = Libreria.getNomeStazioneDaId(.id_stazione_uscita)
            lb_data_out.Text = Libreria.myFormatta(.data_uscita, "dd/MM/yyyy")
            lb_ora_out.Text = Libreria.myFormatta(.data_uscita, "HH:mm").Replace(".", ":")
            lb_km_out.Text = .km_uscita & ""
            lb_litri_out.Text = .litri_uscita & ""
            lb_consegnato_da.Text = Libreria.getNomeOperatoreDaId(.id_consegnato_da)

            ' Dati rientro
            lb_stazione_in.Text = Libreria.getNomeStazioneDaId(.id_stazione_rientro)
            lb_data_in.Text = Libreria.myFormatta(.data_rientro, "dd/MM/yyyy")
            lb_ora_in.Text = Libreria.myFormatta(.data_rientro, "HH:mm").Replace(".", ":")
            lb_km_in.Text = .km_rientro & ""
            lb_litri_in.Text = .litri_rientro & ""
            lb_ritirato_da.Text = Libreria.getNomeOperatoreDaId(.id_ritirato_da)

            ' Conducente

            Dim mio_driver As drivers = Nothing
            If .id_conducente IsNot Nothing Then
                mio_driver = drivers.getRecordDaId(.id_conducente)
            End If
            If mio_driver IsNot Nothing Then
                With mio_driver
                    lb_conducente.Text = .cognome & " " & .nome
                    lb_conducente_indirizzo.Text = .indirizzo & ""
                    lb_conducente_citta.Text = .citta & ""

                    lb_conducente_citta_nascita.Text = .citta_nascita & ""
                    lb_conducente_data_nascita.Text = Libreria.myFormatta(.data_nascita, "dd/MM/yyyy")
                    lb_conducente_codice_fiscale.Text = .codice_fiscale & ""

                    lb_conducente_patente.Text = .patente
                    lb_conducente_patente_emissione.Text = .luogo_emissione & ""
                    lb_conducente_patente_scadenza.Text = Libreria.myFormatta(.scadenza_patente, "dd/MM/yyyy")
                End With
            End If

            ' elenco danni collegato all'odl
            If .id_gruppo_danni_uscita Is Nothing Then
                lb_id_gruppo_apertura.Text = 0
            Else
                lb_id_gruppo_apertura.Text = .id_gruppo_danni_uscita
            End If

            listViewElencoDanniPerEvento.DataBind()

            NotePerStampa.InitForm(enum_note_tipo.note_odl, .num_odl)

            ' Fattura collegata
            If .codice_fattura IsNot Nothing Then
                lb_numero_fattura.Text = .codice_fattura & ""
            End If
            If .data_fattura IsNot Nothing Then
                lb_data_fattura.Text = Libreria.myFormatta(.data_fattura, "dd/MM/yyyy")
            End If
            If .importo_fattura IsNot Nothing Then
                lb_importo_fattura.Text = Libreria.myFormatta(.importo_fattura, "0.00")
            End If

            ' Autorizzazione preventivo
            lb_autorizzato_preventivo.Text = Libreria.getNomeOperatoreDaId(.id_autorizzato_pagamento)
            lb_data_autorizzazione_preventivo.Text = Libreria.myFormatta(.data_autorizzato_pagamento, "dd/MM/yyyy")
            lb_importo_preventivo.Text = Libreria.myFormatta(.importo, "0.00")
 

        End With
    End Sub


    Protected Sub InitIntestazione(mio_veicolo As tabella_veicoli)
        With mio_veicolo
            lb_targa.Text = .targa & ""
            lb_modello.Text = .modello & ""
            If lb_modello.Text = "" Then
                lb_modello.Text = "(N.V.)"
            End If
            lb_stazione.Text = .stazione & ""
            If lb_stazione.Text = "" Then
                lb_stazione.Text = "(N.V.)"
            End If
            lb_telaio.Text = .telaio & ""
            If lb_telaio.Text = "" Then
                lb_telaio.Text = "(N.V.)"
            End If
            lb_proprietario.Text = .proprietario & ""
            If lb_proprietario.Text = "" Then
                lb_proprietario.Text = "(N.V.)"
            End If
        End With
    End Sub


    Protected Sub listViewElencoDanniPerEvento_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listViewElencoDanniPerEvento.ItemDataBound
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
        End If
    End Sub

End Class
