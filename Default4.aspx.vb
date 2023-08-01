
Partial Class Default4
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Dati As New tabelle_pos_Scambio_Importo.DatiInizializzazione()
            Dati.IDStazione = 11
            Dati.NumeroDocumento = "122334"
            Dati.TipoDocumento = tabelle_pos_Scambio_Importo.TipoDocumento.Contratto
            Dati.ListaPreautorizzazioni = New System.Collections.Generic.List(Of tabelle_pos_Scambio_Importo.Preautorizzazione)

            Dim Pre As New tabelle_pos_Scambio_Importo.Preautorizzazione()
            Pre.Numero = "011324075"

            Dati.ListaPreautorizzazioni.Add(Pre)

            Pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()
            Pre.Numero = "363320898"

            Dati.ListaPreautorizzazioni.Add(Pre)


            Dati.Importo = 5

            Dati.ImportoMassimoRimborsabile = 100

            'Dati.PreSelectIDEnte = 13
            'Dati.PreSelectIDAcquireCircuito = 64
            'Dati.PreSelectIDFunzione = Costanti.POS_FunzioneIntegrazione
            'Dati.PreSelectPOSID = 16
            'Dati.PreSelectNumeroPreautorizzazione = "363320898"
            Dati.TestMode = False

            Scambio_Importo1.InizializzazioneDati(Dati)
        End If

        AddHandler Scambio_Importo1.ScambioImportoClose, AddressOf ScambioImportoClose
        AddHandler Scambio_Importo1.ScambioImportoTransazioneEseguita, AddressOf ScambioImportoTransazioneEseguita

    End Sub

    Private Sub ScambioImportoClose(ByVal sender As Object, ByVal e As EventArgs)
        Scambio_Importo1.Visible = False
        lbLog.Items.Add("Ricevuto evento chiusura")
        lbLog.Visible = True
    End Sub

    Public Sub ScambioImportoTransazioneEseguita(ByVal sender As Object, ByVal e As tabelle_pos_Scambio_Importo.TransazioneEseguitaEventArgs)

        Scambio_Importo1.Visible = False
        lbLog.Items.Add("Ricevuto evento Transazione su terminal ID " & e.Transazione.TerminalID)
        lbLog.Visible = True
    End Sub

    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    'Dim EmailClient As New System.Net.Mail.SmtpClient()
    'EmailClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
    'EmailClient.Send(EmailMsg)



    'End Sub
End Class
