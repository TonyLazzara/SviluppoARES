Imports funzioni_comuni

Partial Class tabelle_listini
    Inherits System.Web.UI.Page
    Dim funzioni As New funzioni_comuni

    Protected Sub AzzeraTab()
        PanelCondizioni.Visible = False

        PanelMacrovoci.Visible = False        'aggiunto 06.08.2022 salvo


        'PanelACaricoDi.Visible = False

        PanelFuoriOrario.Visible = False

        'PanelUnitaDiMisura.Visible = False
        'PanelTipoTariffa.Visible = False

        PanelPercentuale.Visible = False
        PanelTipoClienti.Visible = False
        PanelCarburante.Visible = False
        PanelModificaTarga.Visible = False
        PanelGiorniDaPreautorizzare.Visible = False
        PanelScontoFranchigia.Visible = False
        PanelSpeseSpedizioneFattura.Visible = False
        PanelAddebitoAccessoriPersi.Visible = False
        PanelValGps.Visible = False
        PanelRaVoid.Visible = False
        PanelStampaFattura.Visible = False
        PanelArrotondamentoPrepagato.Visible = False
        PanelFranchigieParziali.Visible = False
        PanelCommissioni.Visible = False


    End Sub

    

    Protected Sub Condizioni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Condizioni.Click
        AzzeraTab()
        PanelCondizioni.Visible = True
    End Sub

    Protected Sub btn_macrovoci_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_macrovoci.Click
        'aggiunto 06.08.2022 salvo
        AzzeraTab()
        PanelMacrovoci.Visible = True
    End Sub


    Protected Sub percentuale_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles percentuale.Click
        AzzeraTab()
        PanelPercentuale.Visible = True
    End Sub


    Protected Sub FuoriOrario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles FuoriOrario.Click
        AzzeraTab()
        PanelFuoriOrario.Visible = True
    End Sub

    

    Protected Sub tipo_cliente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tipo_cliente.Click
        AzzeraTab()
        PanelTipoClienti.Visible = True
    End Sub

    

    Protected Sub btnCarburante_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCarburante.Click
        AzzeraTab()
        PanelCarburante.Visible = True
    End Sub

    

    Protected Sub modificaTarga_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles modificaTarga.Click
        AzzeraTab()
        PanelModificaTarga.Visible = True
    End Sub

    

    Protected Sub btnGiorniDaPreautorizzare_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGiorniDaPreautorizzare.Click
        AzzeraTab()
        PanelGiorniDaPreautorizzare.Visible = True
    End Sub

    

    Protected Sub btnScontoFranchigia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScontoFranchigia.Click
        AzzeraTab()
        PanelScontoFranchigia.Visible = True
    End Sub

    

    Protected Sub spese_spedizione_fattura_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles spese_spedizione_fattura.Click
        AzzeraTab()
        PanelSpeseSpedizioneFattura.Visible = True
    End Sub

    

    Protected Sub btnAddebitoAccessoriPersi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddebitoAccessoriPersi.Click
        AzzeraTab()
        PanelAddebitoAccessoriPersi.Visible = True
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session("nascondi_menu") = ""
            Session("prev_page") = ""

            If Request.Cookies("SicilyRentCar") Is Nothing Then
                Response.Redirect("LogIn.aspx")
            Else
                AzzeraTab()
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.ElementiCondizioni) = "1" Then
                Condizioni.Visible = False
                puntoCondizioni.Visible = False
                btn_commissioni.Visible = False
                puntoCommissioni.Visible = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.ElementiPercentuale) = "1" Then
                FuoriOrario.Visible = False
                puntoFuoriOrario.Visible = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.ConsegnaFuoriOrario) = "1" Then
                percentuale.Visible = False
                puntoPercentuale.Visible = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.tipoClienti) = "1" Then
                tipo_cliente.Visible = False
                puntoTipoCliente.Visible = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Carburante) = "1" Then
                btnCarburante.Visible = False
                puntoCarburante.Visible = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.MinutiModificaTarga) = "1" Then
                modificaTarga.Visible = False
                puntoModificaTarga.Visible = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GiorniDiNoloDaPreautorizzare) = "1" Then
                btnGiorniDaPreautorizzare.Visible = False
                puntoGiorniDaPreautorizzare.Visible = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.ScontoSuFranchigia) = "1" Then
                btnScontoFranchigia.Visible = False
                puntoScontoSuFranchigia.Visible = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.SpeseSpedizioneFattura) = "1" Then
                spese_spedizione_fattura.Visible = False
                puntoSpeseSpedizioneFattura.Visible = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.AddebitoAccessoriPersi) = "1" Then
                btnAddebitoAccessoriPersi.Visible = False
                puntoAddebitoAccessoriPersi.Visible = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.ValSatellitare) = "1" Then
                btn_val_gps.Visible = False
                puntoValGps.Visible = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.MinutiRaVoid) = "1" Then
                btnRaVoid.Visible = False
                puntoRaVoid.Visible = False
            End If

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Fatturazione) = "1" Then
                btnStampaFattura.Visible = False
                puntoStampaFattura.Visible = False
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.SogliaArrotondamentoPrepagato) = "1" Then
                spese_spedizione_fattura.Visible = False
                puntoSpeseSpedizioneFattura.Visible = False
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.FranchigieParziali) = "1" Then
                franchigie_parziali.Visible = False
                punto_franchigie_parziali.Visible = False
            End If
        End If
    End Sub


    Protected Sub btn_val_gps_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_val_gps.Click
        AzzeraTab()

        PanelValGps.Visible = True
    End Sub


    Protected Sub btnRaVoid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRaVoid.Click
        AzzeraTab()

        PanelRaVoid.Visible = True
    End Sub

    Protected Sub btnStampaFattura_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaFattura.Click
        AzzeraTab()

        PanelStampaFattura.Visible = True
    End Sub

    Protected Sub arrotondamento_prepagato_Click(sender As Object, e As System.EventArgs) Handles arrotondamento_prepagato.Click
        AzzeraTab()

        PanelArrotondamentoPrepagato.Visible = True
    End Sub


    Protected Sub franchigie_parziali_Click(sender As Object, e As System.EventArgs) Handles franchigie_parziali.Click
        AzzeraTab()

        PanelFranchigieParziali.Visible = True
    End Sub

    Protected Sub btn_commissioni_Click(sender As Object, e As System.EventArgs) Handles btn_commissioni.Click
        AzzeraTab()
        PanelCommissioni.Visible = True
    End Sub
End Class
