
Partial Class preventivo_vedi_calcolo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim test As Integer

            Try
                test = CInt(Request.QueryString("idPrv"))
                idPreventivo.Text = test

                test = CInt(Request.QueryString("versione"))
                numCalcolo.Text = test

                test = CInt(Request.QueryString("idGrp"))
                idGruppo.Text = Request.QueryString("idGrp")

                test = CInt(Request.QueryString("numGG"))
                txtNumeroGiorni.Text = Request.QueryString("numGG")
            Catch ex As Exception

            End Try
        End If
        listPreventiviCosti.Visible = True
        listPreventiviCosti.DataBind()

    End Sub



    Protected Sub listPreventiviCosti_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles listPreventiviCosti.DataBinding
        ultimo_gruppo.Text = ""
    End Sub

    Protected Sub listPreventiviCosti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listPreventiviCosti.ItemDataBound
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
            qta.Text = txtNumeroGiorni.Text

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
        If (omaggiabile.Text = "True") And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_con_valore And id_metodo_stampa.Text <> Costanti.id_stampa_informativa_senza_valore And Not e.Item.FindControl("lblIncluso").Visible And LCase(nome_costo.Text) <> LCase(Costanti.testo_elemento_totale) Then
            omaggiato.Visible = True
            omaggiato.Enabled = False
        End If
        '----------------------------------------------------------------------------------------------------------------------------------

    End Sub

    Protected Function get_tempo_km() As String
        Dim id_elemento As Label
        For i = 0 To listPreventiviCosti.Items.Count - 1
            id_elemento = listPreventiviCosti.Items(i).FindControl("id_elemento")
            If id_elemento.Text = Costanti.ID_tempo_km Then
                Dim costo_scontato As Label = listPreventiviCosti.Items(i).FindControl("costo_scontato")
                get_tempo_km = costo_scontato.Text
                Exit For
            End If
        Next
    End Function

End Class