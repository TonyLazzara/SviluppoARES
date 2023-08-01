

Partial Class gestione_danni_RiepilogoRDS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim id_evento As String = Request.QueryString("id_evento")

        lb_id_evento.Text = id_evento
        listViewElencoDanniPerEvento.DataBind()

        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(Integer.Parse(id_evento))
        InitEvento(mio_evento)

        Dim mio_veicolo As tabella_veicoli = tabella_veicoli.get_record_da_id(mio_evento.id_veicolo)
        InitIntestazione(mio_veicolo)
    End Sub

    Protected Sub InitEvento(mio_evento As veicoli_evento_apertura_danno)
        With mio_evento
            Dim id_tipo_doc As tipo_documento = .id_tipo_documento_apertura
            Select Case id_tipo_doc
                Case tipo_documento.Contratto
                    lb_tipo_documento.Text = "RA"
                Case Else
                    Libreria.genUserMsgBox(Page, "Tipo documento non previsto")
            End Select
            lb_num_documento.Text = .id_documento_apertura
            lb_numero_rds.Text = .id_rds
            lb_data_rds.Text = Format(.data_rds, "dd/MM/yyyy")

            If .data_incidente IsNot Nothing Then
                lb_data_incidente.Text = Format(.data_incidente, "dd/MM/yyyy")
            End If
            lb_luogo_incidente.Text = .luogo_incidente & ""
            If .doc_CID IsNot Nothing AndAlso .doc_CID Then
                lb_CID.Text = "[ X ]"
            Else
                lb_CID.Text = "[ &nbsp;&nbsp; ]"
            End If
            If .doc_denuncia IsNot Nothing AndAlso .doc_denuncia Then
                lb_denuncia.Text = "[ X ]"
            Else
                lb_denuncia.Text = "[ &nbsp;&nbsp; ]"
            End If
            If .doc_fotocopia_doc IsNot Nothing AndAlso .doc_fotocopia_doc Then
                lb_fotocopia_documenti.Text = "[ X ]"
            Else
                lb_fotocopia_documenti.Text = "[ &nbsp;&nbsp; ]"
            End If
            If .doc_preventivo IsNot Nothing AndAlso .doc_preventivo Then
                lb_preventivo.Text = "[ X ]"
            Else
                lb_preventivo.Text = "[ &nbsp;&nbsp; ]"
            End If
            If .num_fotografie Is Nothing Then
                lb_num_fotografie.Text = "[ 0 ]"
            Else
                lb_num_fotografie.Text = "[ " & .num_fotografie & " ]"
            End If

            ' non so cosa di preciso sia...
            'If .doc_chiavi_auto_rubate IsNot Nothing AndAlso .doc_chiavi_auto_rubate Then 
            '    lb_chiavi_auto_rubata.Text = "X"
            'End If

            lb_note.Text = .nota
            If .importo Is Nothing Then
                .importo = 0
                .iva = 0
            End If

            ' l'operatore che ha periziato e l'importo (escluso iva) periziato (considero l'ultimo importo e/o iva differente immesso... stato: da addebitare)
            lb_importo.Text = Libreria.myFormatta(.importo, "0.00")
            lb_aliquota_iva.Text = Libreria.getDescrizioneAliquotaIVADaId(.iva)
            lb_spese_postali.Text = Libreria.myFormatta(.spese_postali, "0.00")
            lb_totale.Text = Libreria.myFormatta(.totale, "0.00")
            lb_giorni_fermo_tecnico.Text = .giorni_fermo_tecnico & ""
            If .perizia IsNot Nothing OrElse .perizia Then
                lb_perizia_effettuata.Text = "Si"
            Else
                lb_perizia_effettuata.Text = "No"
            End If
            lb_perizia_data.Text = Libreria.myFormatta(.data_perizia, "dd/MM/yyyy")

            '' attenzione pur avendo effettuato una stima del danno potrei non aver ancora reso da addebitare!!!
            '' quindi non valorizzerei i record sotto...
            'Dim ultima_stima As veicoli_stato_rds_variazione = veicoli_stato_rds_variazione.getUltimaStimaImporto(mio_evento.id, .importo, .iva)
            'If ultima_stima IsNot Nothing Then
            '    lb_compilato_da.Text = Libreria.getNomeOperatoreDaId(ultima_stima.id_utente)

            'End If

            ' e quello che ha stampato la lettera al cliente (considero l'ultima richiesta di stampa lettera al cliente... potrebbero essercene più di una...)
            Dim ultima_stampa As veicoli_stato_rds_variazione = veicoli_stato_rds_variazione.getUltimaStampa(mio_evento.id)
            If ultima_stampa IsNot Nothing Then
                lb_data_spedizione.Text = Format(ultima_stampa.data_creazione, "dd/MM/yyyy")
                lb_spedito_da.Text = Libreria.getNomeOperatoreDaId(ultima_stampa.id_utente)
            End If

            ' prelievo le info sulla fattura
            lb_numero_fattura.Text = ""
            lb_data_fattura.Text = ""
            lb_importo_fattura.Text = ""
            lb_note_fattura.Text = ""

            lb_incasso_fattura.Text = "" ' non so ancora cosa sia...

        End With
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
            lb_proprietario.Text = .proprietario
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

End Class
