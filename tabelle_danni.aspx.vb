
Partial Class tabelle_danni
    Inherits System.Web.UI.Page

    Private Enum DivVisibile
        Nessuno = 0
        MenuPulsanti = 1
        GestioneOrigineDanno = 2
        GestionePosizioneDanno = 4
        GestioneTipoDanno = 8
        GestioneTipoDocumentoDanno = 16
        GestioneTipoDocumentoEventoDanno = 32
        GestioneMappaturaModelli = 64
        GestioneTipoAttesaUfficioManutenzione = 128
        SinistroGestitoDa = 256
        SinistroTipologia = 512
        GestioneMotivoNonAddebitoDanno = 1024
        GestioneFornitori = 2048
        GestioneDrivers = 4096
    End Enum

    Private Sub Visibilita(Valore As DivVisibile)
        PanelGestioneOrigineDanno.Visible = Valore And DivVisibile.GestioneOrigineDanno
        btnGestioneOrigineDanno.Visible = Valore And (DivVisibile.GestioneOrigineDanno Or DivVisibile.MenuPulsanti)
        puntoGestioneOrigineDanno.Visible = Valore And (DivVisibile.GestioneOrigineDanno Or DivVisibile.MenuPulsanti)

        PanelGestionePosizioneDanno.Visible = Valore And DivVisibile.GestionePosizioneDanno
        btnGestionePosizioneDanno.Visible = Valore And (DivVisibile.GestionePosizioneDanno Or DivVisibile.MenuPulsanti)
        puntoGestionePosizioneDanno.Visible = Valore And (DivVisibile.GestionePosizioneDanno Or DivVisibile.MenuPulsanti)

        PanelGestioneTipoDanno.Visible = Valore And DivVisibile.GestioneTipoDanno
        btnGestioneTipoDanno.Visible = Valore And (DivVisibile.GestioneTipoDanno Or DivVisibile.MenuPulsanti)
        puntoGestioneTipoDanno.Visible = Valore And (DivVisibile.GestioneTipoDanno Or DivVisibile.MenuPulsanti)

        PanelGestioneTipoDocumentoDanno.Visible = Valore And DivVisibile.GestioneTipoDocumentoDanno
        btnGestioneTipoDocumentoDanno.Visible = Valore And (DivVisibile.GestioneTipoDocumentoDanno Or DivVisibile.MenuPulsanti)
        puntoGestioneTipoDocumentoDanno.Visible = Valore And (DivVisibile.GestioneTipoDocumentoDanno Or DivVisibile.MenuPulsanti)

        PanelGestioneTipoAttesaUfficioManutenzione.Visible = Valore And DivVisibile.GestioneTipoAttesaUfficioManutenzione
        btnGestioneTipoAttesaUfficioManutenzione.Visible = Valore And (DivVisibile.GestioneTipoAttesaUfficioManutenzione Or DivVisibile.MenuPulsanti)
        puntoGestioneTipoAttesaUfficioManutenzione.Visible = Valore And (DivVisibile.GestioneTipoAttesaUfficioManutenzione Or DivVisibile.MenuPulsanti)

        PanelGestioneMotivoNonAddebitoDanno.Visible = Valore And DivVisibile.GestioneMotivoNonAddebitoDanno
        btnGestioneMotivoNonAddebitoDanno.Visible = Valore And (DivVisibile.GestioneMotivoNonAddebitoDanno Or DivVisibile.MenuPulsanti)
        puntoGestioneMotivoNonAddebitoDanno.Visible = Valore And (DivVisibile.GestioneMotivoNonAddebitoDanno Or DivVisibile.MenuPulsanti)

        PanelGestioneTipoDocumentoEventoDanno.Visible = Valore And DivVisibile.GestioneTipoDocumentoEventoDanno
        btnGestioneTipoDocumentoEventoDanno.Visible = Valore And (DivVisibile.GestioneTipoDocumentoEventoDanno Or DivVisibile.MenuPulsanti)
        puntoGestioneTipoDocumentoEventoDanno.Visible = Valore And (DivVisibile.GestioneTipoDocumentoEventoDanno Or DivVisibile.MenuPulsanti)

        PanelGestioneMappaturaModelli.Visible = Valore And DivVisibile.GestioneMappaturaModelli
        btnGestioneMappaturaModelli.Visible = Valore And (DivVisibile.GestioneMappaturaModelli Or DivVisibile.MenuPulsanti)
        puntoGestioneMappaturaModelli.Visible = Valore And (DivVisibile.GestioneMappaturaModelli Or DivVisibile.MenuPulsanti)

        PanelSinistriGestitoDa.Visible = Valore And DivVisibile.SinistroGestitoDa
        btnSinistroGestitoDa.Visible = Valore And (DivVisibile.SinistroGestitoDa Or DivVisibile.MenuPulsanti)
        puntoGestitoDa.Visible = Valore And (DivVisibile.SinistroGestitoDa Or DivVisibile.MenuPulsanti)

        PanelSinistriTipologia.Visible = Valore And DivVisibile.SinistroTipologia
        btnSinistroTipologia.Visible = Valore And (DivVisibile.SinistroTipologia Or DivVisibile.MenuPulsanti)
        puntoTIpologia.Visible = Valore And (DivVisibile.SinistroTipologia Or DivVisibile.MenuPulsanti)

        PanelGestioneFornitori.Visible = Valore And DivVisibile.GestioneFornitori
        btnGestioneFornitori.Visible = Valore And (DivVisibile.GestioneFornitori Or DivVisibile.MenuPulsanti)
        puntoGestioneFornitori.Visible = Valore And (DivVisibile.GestioneFornitori Or DivVisibile.MenuPulsanti)

        PanelGestioneDrivers.Visible = Valore And DivVisibile.GestioneDrivers
        btnGestioneDrivers.Visible = Valore And (DivVisibile.GestioneDrivers Or DivVisibile.MenuPulsanti)
        puntoGestioneDrivers.Visible = Valore And (DivVisibile.GestioneDrivers Or DivVisibile.MenuPulsanti)


        ' se aggiungi pannelli si deve aggiungere anche in coda alle 2 seguenti il div relativo...
        Dim gestioneMenu As Boolean = Valore And (DivVisibile.GestioneOrigineDanno Or DivVisibile.GestionePosizioneDanno Or DivVisibile.GestioneTipoDanno Or DivVisibile.GestioneTipoDocumentoDanno Or DivVisibile.GestioneTipoDocumentoEventoDanno Or DivVisibile.GestioneMappaturaModelli Or DivVisibile.GestioneTipoAttesaUfficioManutenzione Or DivVisibile.SinistroTipologia Or DivVisibile.SinistroGestitoDa Or DivVisibile.GestioneFornitori Or DivVisibile.GestioneDrivers)

        btnTorna.Visible = gestioneMenu
        puntoTorna.Visible = gestioneMenu
    End Sub

    Protected Sub btnTorna_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTorna.Click
        Visibilita(DivVisibile.MenuPulsanti)
    End Sub

    Protected Sub btnGestioneOrigineDanno_Click(sender As Object, e As System.EventArgs) Handles btnGestioneOrigineDanno.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneOrigineDanno) <> "1" Then
            Visibilita(DivVisibile.GestioneOrigineDanno)
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnGestionePosizioneDanno_Click(sender As Object, e As System.EventArgs) Handles btnGestionePosizioneDanno.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestionePosizioneDanno) <> "1" Then
            Visibilita(DivVisibile.GestionePosizioneDanno)
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnGestioneTipoDanno_Click(sender As Object, e As System.EventArgs) Handles btnGestioneTipoDanno.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneTipoDanno) <> "1" Then
            Visibilita(DivVisibile.GestioneTipoDanno)
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnGestioneTipoDocumentoDanno_Click(sender As Object, e As System.EventArgs) Handles btnGestioneTipoDocumentoDanno.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneTipoDocumentoDanno) <> "1" Then
            Visibilita(DivVisibile.GestioneTipoDocumentoDanno)
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnGestioneTipoAttesaUfficioManutenzione_Click(sender As Object, e As System.EventArgs) Handles btnGestioneTipoAttesaUfficioManutenzione.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneTabelleRDS) <> "1" Then
            Visibilita(DivVisibile.GestioneTipoAttesaUfficioManutenzione)
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnGestioneMotivoNonAddebitoDanno_Click(sender As Object, e As System.EventArgs) Handles btnGestioneMotivoNonAddebitoDanno.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneTabelleRDS) <> "1" Then
            Visibilita(DivVisibile.GestioneMotivoNonAddebitoDanno)
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnGestioneTipoDocumentoEventoDanno_Click(sender As Object, e As System.EventArgs) Handles btnGestioneTipoDocumentoEventoDanno.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneTipoDocumentoEventoDanno) <> "1" Then
            Visibilita(DivVisibile.GestioneTipoDocumentoEventoDanno)
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnGestioneMappaturaModelli_Click(sender As Object, e As System.EventArgs) Handles btnGestioneMappaturaModelli.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneMappaturaModelli) <> "1" Then
            Visibilita(DivVisibile.GestioneMappaturaModelli)
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnGestioneFornitori_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGestioneFornitori.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneFornitori) <> "1" Then
            Visibilita(DivVisibile.GestioneFornitori)
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.") 'GestioneDrivers
        End If
    End Sub

    Protected Sub btnGestioneDrivers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGestioneDrivers.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneDrivers) <> "1" Then
            Visibilita(DivVisibile.GestioneDrivers)
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub ChiusuraForm(sender As Object, e As System.EventArgs)
        Visibilita(DivVisibile.MenuPulsanti)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler origine_danno.ChiusuraForm, AddressOf ChiusuraForm
        AddHandler posizione_danno.ChiusuraForm, AddressOf ChiusuraForm
        AddHandler tipo_danno.ChiusuraForm, AddressOf ChiusuraForm
        AddHandler tipo_documento_danno.ChiusuraForm, AddressOf ChiusuraForm
        AddHandler tipo_attesa_manutenzione.ChiusuraForm, AddressOf ChiusuraForm
        AddHandler tipo_motivo_non_addebito.ChiusuraForm, AddressOf ChiusuraForm
        AddHandler tipo_documento_evento_danno.ChiusuraForm, AddressOf ChiusuraForm
        AddHandler anagrafica_fornitori.ChiusuraForm, AddressOf ChiusuraForm
        AddHandler anagrafica_drivers.chiusuraform, AddressOf ChiusuraForm

        If Not Page.IsPostBack Then
            origine_danno.InitForm(stato:=2)
            posizione_danno.InitForm(stato:=2)
            tipo_danno.InitForm(stato:=2)
            tipo_documento_danno.InitForm(stato:=2, tipo_documento:=tipo_danni_img_tipo_documenti.Danno)
            tipo_attesa_manutenzione.InitForm(stato:=2)
            tipo_motivo_non_addebito.InitForm(stato:=2)
            tipo_documento_evento_danno.InitForm(stato:=2, tipo_documento:=tipo_danni_img_tipo_documenti.Evento)
            anagrafica_fornitori.InitForm(stato:=2)
            anagrafica_drivers.InitForm(stato:=2)
        End If
    End Sub

    Protected Sub btnSinistroTipologia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSinistroTipologia.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneSinistri) <> "1" Then
            Visibilita(DivVisibile.SinistroTipologia)
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnSinistroGestitoDa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSinistroGestitoDa.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneSinistri) <> "1" Then
            Visibilita(DivVisibile.SinistroGestitoDa)
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub
End Class
