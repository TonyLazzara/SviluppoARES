Imports funzioni_comuni

Partial Class tabelle_stazioni
    Inherits System.Web.UI.Page
    Dim funzioni As New funzioni_comuni

    Protected Sub AzzeraTab()
        PanelStazioni.Visible = False
        PanelProprietariStazioni.Visible = False
        PanelOrari.Visible = False
        PanelZone.Visible = False
        PanelFestivita.Visible = False
        PanelOrariFestivita.Visible = False
        PanelDistanzaVal.Visible = False
        PanelOneri.Visible = False
    End Sub

    Protected Sub mostraPulsanti()
        btnDistanzaVAL.Visible = True
        btnFestivia.Visible = True
        btnOrariFestività.Visible = True
        btnOrariStazione.Visible = True
        btnProprietariStazioni.Visible = True
        btnStazioni.Visible = True
        btnZone.Visible = True
        btnOneri.Visible = True

        puntoDistanza.Visible = True
        puntoFestivita.Visible = True
        puntoOrariFestivita.Visible = True
        puntoOrariStazione.Visible = True
        puntoProprietari.Visible = True
        puntoStazioni.Visible = True
        puntoZone.Visible = True
        puntoOneri.Visible = True
    End Sub

    Protected Sub NascondiPulsanti()
        btnDistanzaVAL.Visible = False
        btnFestivia.Visible = False
        btnOrariFestività.Visible = False
        btnOrariStazione.Visible = False
        btnProprietariStazioni.Visible = False
        btnStazioni.Visible = False
        btnZone.Visible = False
        btnOneri.Visible = False

        puntoDistanza.Visible = False
        puntoFestivita.Visible = False
        puntoOrariFestivita.Visible = False
        puntoOrariStazione.Visible = False
        puntoProprietari.Visible = False
        puntoStazioni.Visible = False
        puntoZone.Visible = False
        puntoOneri.Visible = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session("cambia_tab") = ""
            Session("orario") = ""
            Session("prop") = ""
            Session("staz") = ""
            Session("zona") = ""
            Session("festivo") = ""
            Session("oneri") = ""

            If Request.Cookies("SicilyRentCar") Is Nothing Then
                Response.Redirect("LogIn.aspx")
            Else
                AzzeraTab()
            End If
        End If

        AddHandler orari_festivita.cambia_festivita, AddressOf cambia_festivita
        AddHandler festivita.trasferisci_festivita, AddressOf trasferisci_festivita
    End Sub


    Protected Sub cambia_festivita(ByVal sender As Object, ByVal e As tabelle_stazioni_orari_festivita.ScegliFestivitaEventArgs)
        If e.tab = "from_orari_festivita" Then
            AzzeraTab()
            NascondiPulsanti()
            PanelFestivita.Visible = True
            btnFestivia.Visible = True
            puntoFestivita.Visible = True
            btnTorna.Visible = True
            puntoTorna.Visible = True

            Session("cambia_tab") = "from_orari_festivita"
        End If
    End Sub

    Protected Sub trasferisci_festivita(ByVal sender As Object, ByVal e As tabelle_stazioni_festivita.trasferisci_festivitaEventArgs)
        If e.tab = "festivita" Then
            If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 54) <> "1" Then
                AzzeraTab()
                NascondiPulsanti()
                PanelOrariFestivita.Visible = True
                btnOrariFestività.Visible = True
                puntoOrariFestivita.Visible = True
                btnTorna.Visible = True
                puntoTorna.Visible = True
            Else
                Session("cambia_tab") = ""
                Libreria.genUserMsgBox(Me, "Accesso negato.")
            End If
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        'CONTROLLO SE DENTRO STAZIONI E' STATO RICHIESTO DI PASSARE IN UN ALTRO TAB
        If Session("cambia_tab") = "prop" And Not PanelProprietariStazioni.Visible Then
            If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 18) <> "1" Then
                AzzeraTab()
                NascondiPulsanti()
                PanelProprietariStazioni.Visible = True
                btnProprietariStazioni.Visible = True
                puntoProprietari.Visible = True
                btnTorna.Visible = True
                puntoTorna.Visible = True
            Else
                Session("cambia_tab") = ""
                Libreria.genUserMsgBox(Me, "Accesso negato.")
            End If
        ElseIf Session("cambia_tab") = "staz" And Not PanelStazioni.Visible Then
            If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 17) <> "1" Then
                AzzeraTab()
                NascondiPulsanti()
                PanelStazioni.Visible = True
                btnStazioni.Visible = True
                puntoStazioni.Visible = True
                btnTorna.Visible = True
                puntoTorna.Visible = True
            Else
                Session("cambia_tab") = ""
                Libreria.genUserMsgBox(Me, "Accesso negato.")
            End If
        ElseIf Session("cambia_tab") = "zona" And Not PanelZone.Visible Then
            If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 53) <> "1" Then
                AzzeraTab()
                NascondiPulsanti()
                PanelZone.Visible = True
                btnZone.Visible = True
                puntoZone.Visible = True
                btnTorna.Visible = True
                puntoTorna.Visible = True
            Else
                Session("cambia_tab") = ""
                Libreria.genUserMsgBox(Me, "Accesso negato.")
            End If
        ElseIf Session("cambia_tab") = "orario" And Not PanelOrari.Visible Then
            If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 50) <> "1" Then
                AzzeraTab()
                NascondiPulsanti()
                PanelOrari.Visible = True
                btnOrariStazione.Visible = True
                puntoOrariStazione.Visible = True
                btnTorna.Visible = True
                puntoTorna.Visible = True
            Else
                Session("cambia_tab") = ""
                Libreria.genUserMsgBox(Me, "Accesso negato.")
            End If
        ElseIf Session("cambia_tab") = "festivo" And Not PanelOrariFestivita.Visible Then
            If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 52) <> "1" Then
                AzzeraTab()
                NascondiPulsanti()
                PanelOrariFestivita.Visible = True
                btnOrariFestività.Visible = True
                puntoOrariFestivita.Visible = True
                btnTorna.Visible = True
                puntoTorna.Visible = True
            Else
                Session("cambia_tab") = ""
                Libreria.genUserMsgBox(Me, "Accesso negato.")
            End If
        ElseIf Session("cambia_tab") = "locale" And Not PanelOrariFestivita.Visible Then
            If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 52) <> "1" Then
                AzzeraTab()
                NascondiPulsanti()
                PanelOrariFestivita.Visible = True
                btnOrariFestività.Visible = True
                puntoOrariFestivita.Visible = True
                btnTorna.Visible = True
                puntoTorna.Visible = True
            Else
                Session("cambia_tab") = ""
                Libreria.genUserMsgBox(Me, "Accesso negato.")
            End If
        ElseIf Session("cambia_tab") = "oneri" And Not PanelOneri.Visible Then
            If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 70) <> "1" Then
                AzzeraTab()
                NascondiPulsanti()
                PanelOneri.Visible = True
                btnOneri.Visible = True
                puntoOneri.Visible = True
                btnTorna.Visible = True
                puntoTorna.Visible = True
            Else
                Session("cambia_tab") = ""
                Libreria.genUserMsgBox(Me, "Accesso negato.")
            End If
        End If
    End Sub

    Protected Sub btnStazioni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStazioni.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 17) <> "1" Then
            NascondiPulsanti()
            'Session("prev_page") = "stazioni"
            PanelStazioni.Visible = True
            btnStazioni.Visible = True
            puntoStazioni.Visible = True
            btnTorna.Visible = True
            puntoTorna.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnProprietariStazioni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProprietariStazioni.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 18) <> "1" Then
            NascondiPulsanti()
            'Session("prev_page") = "proprietari"
            PanelProprietariStazioni.Visible = True
            btnProprietariStazioni.Visible = True
            puntoProprietari.Visible = True
            btnTorna.Visible = True
            puntoTorna.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnOrariStazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrariStazione.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 50) <> "1" Then
            NascondiPulsanti()
            'Session("prev_page") = "orari"
            PanelOrari.Visible = True
            btnOrariStazione.Visible = True
            puntoOrariStazione.Visible = True
            btnTorna.Visible = True
            puntoTorna.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub


    Protected Sub btnZone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnZone.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 53) <> "1" Then
            NascondiPulsanti()
            'Session("prev_page") = "zone"
            PanelZone.Visible = True
            btnZone.Visible = True
            puntoZone.Visible = True
            btnTorna.Visible = True
            puntoTorna.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnFestivia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFestivia.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 52) <> "1" Then
            NascondiPulsanti()
            'Session("prev_page") = "feste"
            PanelFestivita.Visible = True
            btnFestivia.Visible = True
            puntoFestivita.Visible = True
            btnTorna.Visible = True
            puntoTorna.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnOrariFestività_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrariFestività.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 54) <> "1" Then
            NascondiPulsanti()
            'Session("prev_page") = "orari_feste"
            PanelOrariFestivita.Visible = True
            btnOrariFestività.Visible = True
            puntoOrariFestivita.Visible = True
            btnTorna.Visible = True
            puntoTorna.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnDistanzaVAL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDistanzaVAL.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 55) <> "1" Then
            NascondiPulsanti()
            'Session("prev_page") = "distanza/VAL"
            btnDistanzaVAL.Visible = True
            PanelDistanzaVal.Visible = True
            puntoDistanza.Visible = True
            btnTorna.Visible = True
            puntoTorna.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnTorna_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTorna.Click
        AzzeraTab()
        mostraPulsanti()
        btnTorna.Visible = False
        puntoTorna.Visible = False

        Session("cambia_tab") = ""
    End Sub

    Protected Sub btnOneri_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOneri.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 70) <> "1" Then
            NascondiPulsanti()
            'Session("prev_page") = "distanza/VAL"
            btnOneri.Visible = True
            PanelOneri.Visible = True
            puntoOneri.Visible = True
            btnTorna.Visible = True
            puntoTorna.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub
End Class
