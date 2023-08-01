Imports funzioni_comuni

Partial Class tabelleAuto
    Inherits System.Web.UI.Page
    Dim funzioni As New funzioni_comuni

    Protected Sub AzzeraTab()
        'PanelAccessori.Visible = False
        PanelAlimentazione.Visible = False
        PanelEnti.Visible = False
        PanelGruppi.Visible = False
        PanelMarche.Visible = False
        PanelProprietari.Visible = False
        PanelModelli.Visible = False
        PanelCompagnieAssicurative.Visible = False
        PanelBolle.Visible = False
        'PanelAmmortaemtni.Visible = False
        PanelVenditori.Visible = False
        PanelRiparazioni.Visible = False
        PanelAcquirenti.Visible = False
        'PanelTipoAcquirente.Visible = False
        PanelColori.Visible = False
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

            'SELETTORE DI PROVENIENZA: DA PARCO VEICOLI--------------------------------------------
            If Request.QueryString("veicolo") <> "" Then
                If Request.QueryString("val") = "marca" Then
                    PanelMarche.Visible = True
                End If
                If Request.QueryString("val") = "modello" Then
                    PanelModelli.Visible = True
                End If
                'If Request.QueryString("val") = "alim" Then
                '    PanelAlimentazione.Visible = True
                'End If
                If Request.QueryString("val") = "propr" Then
                    PanelProprietari.Visible = True
                End If
                If Request.QueryString("val") = "acc" Then
                    PanelAccessori.Visible = True
                End If
                If Request.QueryString("val") = "ass" Then
                    PanelCompagnieAssicurative.Visible = True
                End If
                If Request.QueryString("val") = "bolla" Then
                    PanelBolle.Visible = True
                End If
                'If Request.QueryString("val") = "amm" Then
                '    PanelAmmortaemtni.Visible = True
                'End If
                If Request.QueryString("val") = "venditore" Then
                    PanelVenditori.Visible = True
                End If
                If Request.QueryString("val") = "ente" Then
                    PanelEnti.Visible = True
                End If
                If Request.QueryString("val") = "acquirente" Then
                    PanelAcquirenti.Visible = True
                End If
                'If Request.QueryString("val") = "tipoacq" Then
                '    PanelTipoAcquirente.Visible = True
                'End If
                If Request.QueryString("val") = "colore" Then
                    PanelColori.Visible = True
                End If
            ElseIf Request.QueryString("prov") = "ImportMassivo" Then
                If Request.QueryString("val") = "modello" Then
                    PanelModelli.Visible = True
                End If
                If Request.QueryString("val") = "colore" Then
                    PanelColori.Visible = True
                End If
                If Request.QueryString("val") = "propr" Then
                    PanelProprietari.Visible = True
                End If
                If Request.QueryString("val") = "ente" Then
                    PanelEnti.Visible = True
                End If
            ElseIf Request.QueryString("prov") = "ImportDismissioni" Then
                If Request.QueryString("val") = "venditore" Then
                    PanelVenditori.Visible = True
                End If
                If Request.QueryString("val") = "acquirente" Then
                    PanelAcquirenti.Visible = True
                End If
            End If
            '--------------------------------------------------------------------------------------

        End If
    End Sub

    Protected Sub Accessori_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Accessori.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 10) <> "1" Then
            AzzeraTab()
            'FillDatiCommerciali()
            PanelAccessori.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    'Protected Sub Alimentazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Alimentazione.Click
    '    If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 11) <> "1" Then
    '        AzzeraTab()
    '        PanelAlimentazione.Visible = True
    '    Else
    '        Libreria.genUserMsgBox(Me, "Accesso negato.")
    '    End If
    'End Sub

    Protected Sub EntiFinanziatori_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EntiFinanziatori.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 12) <> "1" Then
            AzzeraTab()
            PanelEnti.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub GruppiAuto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles GruppiAuto.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 13) <> "1" Then
            AzzeraTab()
            PanelGruppi.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnMarche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMarche.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 14) <> "1" Then
            AzzeraTab()
            PanelMarche.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnProprietari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProprietari.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 15) <> "1" Then
            AzzeraTab()
            PanelProprietari.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnModelli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModelli.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 16) <> "1" Then
            AzzeraTab()
            PanelModelli.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub



    Protected Sub btnCompagnieAssicurative_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCompagnieAssicurative.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 19) <> "1" Then
            AzzeraTab()
            PanelCompagnieAssicurative.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnBolle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBolle.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 22) <> "1" Then
            AzzeraTab()
            PanelBolle.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    'Protected Sub btnAmmortamenti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAmmortamenti.Click
    '    If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 23) <> "1" Then
    '        AzzeraTab()
    '        PanelAmmortaemtni.Visible = True
    '    Else
    '        Libreria.genUserMsgBox(Me, "Accesso negato.")
    '    End If
    'End Sub
    Protected Sub btnAlimentazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAlimentazione.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 11) <> "1" Then
            AzzeraTab()
            PanelAlimentazione.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnVenditori_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVenditori.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 24) <> "1" Then
            AzzeraTab()
            PanelVenditori.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btniparazioni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btniparazioni.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 29) <> "1" Then
            AzzeraTab()
            PanelRiparazioni.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnAcquirenti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAcquirenti.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 35) <> "1" Then
            AzzeraTab()
            PanelAcquirenti.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    'Protected Sub btnTipoAcquirente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTipoAcquirente.Click
    '    If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 36) <> "1" Then
    '        AzzeraTab()
    '        PanelTipoAcquirente.Visible = True
    '    Else
    '        Libreria.genUserMsgBox(Me, "Accesso negato.")
    '    End If
    'End Sub

    Protected Sub btnColori_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnColori.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 37) <> "1" Then
            AzzeraTab()
            PanelColori.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

End Class
