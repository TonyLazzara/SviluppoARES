
Partial Class prova_ws
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim objWS As bookingOnLine.WebServiceSviluppoBooking = New bookingOnLine.WebServiceSviluppoBooking

        Label1.Text = objWS.HelloWorld
    End Sub

    'Protected Sub btnSomma_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSomma.Click
    '    Dim objWS As bookingOnLine.WebServiceSviluppoBooking = New bookingOnLine.WebServiceSviluppoBooking

    '    lblSomma.Text = objWS.Somma(txt1.Text, txt2.Text)
    'End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerca.Click
        Dim objWS As bookingOnLine.WebServiceSviluppoBooking = New bookingOnLine.WebServiceSviluppoBooking

        If txtTipCliente.Text = "" Then
            txtTipCliente.Text = 0
        End If
        lblElencoStazioni.Text = objWS.getListaStazioni()
        lblElencoTipClienti.Text = objWS.getListaTipClienti()
        'lblElencoCodice.Text = objWS.getCodicePromo(txtCodPromo.Text)
        'response.write("Stazione" & DropStazioneUscita.SelectedValue)
        'response.end()
        lblElenco.Text = objWS.getElencoVeicoli(DropStazioneUscita.SelectedValue, data_uscita.Text, ora_uscita.SelectedValue, DropStazioneRientro.SelectedValue, data_rientro.Text, ora_rientro.SelectedValue, eta.Text, txtTipCliente.Text, txtLingua.Text, txtCodPromo.Text)
    End Sub

    Protected Sub btnDettaglio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDettaglio.Click
        Dim objWS As bookingOnLine.WebServiceSviluppoBooking = New bookingOnLine.WebServiceSviluppoBooking

        lblExtra.Text = objWS.getListaExtra(txtIdPagamento.Text, txtLingua.Text)
        lblCondizioni.Text = objWS.getListaCondizioniInclusi(txtIdPagamento.Text, txtLingua.Text)
        lblFranchiggie.Text = objWS.getListaFranchigie(txtIdPagamento.Text, txtLingua.Text)
        lblNomiFranchiggie.Text = objWS.getNomiFranchigie(txtIdPagamento.Text, txtLingua.Text)
    End Sub

    Protected Sub btnSelezionato_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelezionato.Click
        Dim objWS As bookingOnLine.WebServiceSviluppoBooking = New bookingOnLine.WebServiceSviluppoBooking

        lblElementiExtra.Text = objWS.putAccessorio(txtIdPreventivoAccessorio.Text, txtIdGruppoScelto.Text, txtIdElemento.Text, txtPrepagato.Text, txtGps.Text, DropStazioneUscita.SelectedValue, DropStazioneRientro.SelectedValue, txtNumGiorni.Text)        
        lblExtra.Text = objWS.getListaExtra(txtIdPagamento.Text, txtLingua.Text)
        lblCondizioni.Text = objWS.getListaCondizioniInclusi(txtIdPagamento.Text, txtLingua.Text)
        lblFranchiggie.Text = objWS.getListaFranchigie(txtIdPagamento.Text, txtLingua.Text)
    End Sub

    Protected Sub btnDeselezionato_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeselezionato.Click
        Dim objWS As bookingOnLine.WebServiceSviluppoBooking = New bookingOnLine.WebServiceSviluppoBooking

        lblElementiExtra.Text = objWS.RimuoviAccessorio(txtIdPreventivoAccessorio.Text, txtIdGruppoScelto.Text, txtIdElemento.Text, txtPrepagato.Text, txtGps.Text, DropStazioneUscita.SelectedValue, DropStazioneRientro.SelectedValue, txtNumGiorni.Text)
        lblExtra.Text = objWS.getListaExtra(txtIdPagamento.Text, txtLingua.Text)
        lblCondizioni.Text = objWS.getListaCondizioniInclusi(txtIdPagamento.Text, txtLingua.Text)
        lblFranchiggie.Text = objWS.getListaFranchigie(txtIdPagamento.Text, txtLingua.Text)
    End Sub

    Protected Sub btnPrenota_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrenota.Click
        Dim objWS As bookingOnLine.WebServiceSviluppoBooking = New bookingOnLine.WebServiceSviluppoBooking
        Dim ora, minuti, oraF, minutiF As String
        Dim ora_daSplittare As Array

        ora_daSplittare = Split(ora_uscita.SelectedValue, ":")
        ora = ora_daSplittare(0)
        minuti = ora_daSplittare(1)

        ora_daSplittare = Split(ora_rientro.SelectedValue, ":")
        oraF = ora_daSplittare(0)
        minutiF = ora_daSplittare(1)

        lblOkPrenota.Text = objWS.putDatiPrenotazione(txtIdPagamento.Text, data_uscita.Text, data_rientro.Text, txtPrepagataPrenota.Text, ora, minuti, oraF, minutiF, txtIdTariffaPrenota.Text, txtIdGruppoPrenota.Text, DropStazioneUscita.SelectedValue, DropStazioneRientro.SelectedValue, TxtNome.Text, txtCognome.Text, txtDataNascita.Text, txtemail.Text, txtIndirizzo.Text, txtTelefono.Text, txtVoloArrivo.Text, txtVoloPartenza.Text, eta.Text, txtNumGiorniPrenota.Text)
    End Sub

    Protected Sub btnPrenotaPrepagato_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrenotaPrepagato.Click
        Dim objWS As bookingOnLine.WebServiceSviluppoBooking = New bookingOnLine.WebServiceSviluppoBooking
        Dim ora, minuti, oraF, minutiF As String
        Dim ora_daSplittare As Array

        ora_daSplittare = Split(ora_uscita.SelectedValue, ":")
        ora = ora_daSplittare(0)
        minuti = ora_daSplittare(1)

        ora_daSplittare = Split(ora_rientro.SelectedValue, ":")
        oraF = ora_daSplittare(0)
        minutiF = ora_daSplittare(1)

        lblOkPrenota.Text = objWS.putDatiPrenotazionePrepagata(txtIdPagamento.Text, data_uscita.Text, data_rientro.Text, txtPrepagataPrenota.Text, ora, minuti, oraF, minutiF, txtIdTariffaPrenota.Text, txtIdGruppoPrenota.Text, DropStazioneUscita.SelectedValue, DropStazioneRientro.SelectedValue, TxtNome.Text, txtCognome.Text, txtDataNascita.Text, txtemail.Text, txtIndirizzo.Text, txtTelefono.Text, txtVoloArrivo.Text, txtVoloPartenza.Text, eta.Text, txtNumGiorniPrenota.Text, txtTotaleDaPagare.Text)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.End()
    End Sub
End Class
