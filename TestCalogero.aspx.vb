
Partial Class TestCalogero
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Trace.Write("Page_Load ------------------------ TestCalogero")
        If Not Page.IsPostBack Then
            Dim NumContratto As String = "11000097"

            Dim Dati As New tabelle_pos_Scambio_Importo.DatiInizializzazione()
            Dati.IDStazione = Request.Cookies("SicilyRentCar")("stazione")
            Dati.NumeroDocumento = NumContratto
            Dati.TipoDocumento = tabelle_pos_Scambio_Importo.TipoDocumento.Contratto
            Dati.ListaPreautorizzazioni = New System.Collections.Generic.List(Of tabelle_pos_Scambio_Importo.Preautorizzazione)

            'RECUPERO EVENTUALI PREAUTORIZZAZIONI APERTE SE C'E' UNA PRENOTAZIONE (ALTRIMENTI DI SICURO NON E' MAI STATO FATTO ALCUN PAGAMENTO)

            Dim cPagamenti As Pagamenti = New Pagamenti

            Dim preautorizzazioni(50) As String

            preautorizzazioni = cPagamenti.getListPreautorizzazioni("", "", "", NumContratto)


            Dim i As Integer = 0

            Dim pre As tabelle_pos_Scambio_Importo.Preautorizzazione
            pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()

            Do While preautorizzazioni(i) <> "0"
                pre = New tabelle_pos_Scambio_Importo.Preautorizzazione()
                pre.Numero = preautorizzazioni(i)
                Dati.ListaPreautorizzazioni.Add(pre)
                i = i + 1
            Loop

            Dati.Importo = 150

            Dati.importo_non_modificabile_preautorizzazione = False
            Dati.TestMode = True

            Dati.ImportoMassimoRimborsabile = 0   '<<<----- IMPOSTARE

            'Dati.PreSelectIDEnte = 13
            'Dati.PreSelectIDAcquireCircuito = 64
            'Dati.PreSelectIDFunzione = Costanti.POS_FunzioneIntegrazione
            'Dati.PreSelectPOSID = 16
            'Dati.PreSelectNumeroPreautorizzazione = "363320898"

            Dati.TipoPagamentoContanti = FiltroTipoPagamentoContanti.ChiusuraContratto

            Scambio_Importo.InizializzazioneDati(Dati)

        End If
    End Sub
End Class
