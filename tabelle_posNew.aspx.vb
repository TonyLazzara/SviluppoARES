
Partial Class tabelle_posNew
    Inherits System.Web.UI.Page

    Dim funzioni As New funzioni_comuni

    Protected Sub MostraNascondiPulsanti(ByVal visible As Boolean)
        btnCensimentoPOS.Visible = visible
        puntoCensimentoPOS.Visible = visible

        btnCensimentoEntiProprietari.Visible = visible
        puntoCensimentoEntiProprietari.Visible = visible

        btnCensimentoCircuiti.Visible = visible
        puntoCensimentoCircuiti.Visible = visible

        btnFunzionalita.Visible = visible
        puntoFunzionalità.Visible = visible

        btnEntiTransazioneTelefono.Visible = visible
        puntoEntiTransazioneTelefono.Visible = visible

        btn_anagrafica_esercenti_telefonici.Visible = visible
        punto_anagrafica_esercenti_telefonici.Visible = visible
    End Sub

    Protected Sub EventoChiusuraFormPannello(ByVal sender As Object, ByVal e As System.EventArgs)
        btnTorna_Click(sender, e)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        AddHandler anagrafica_enti_telefonici.ChiusuraForm, AddressOf EventoChiusuraFormPannello
        AddHandler anagrafica_esercenti_telefonici.ChiusuraForm, AddressOf EventoChiusuraFormPannello

    End Sub


    Protected Sub btnTorna_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTorna.Click
        AzzeraTab()
        MostraNascondiPulsanti(True)
        btnTorna.Visible = False
        puntoTorna.Visible = False
    End Sub

    Private Sub AzzeraTab()
        PanelCensimentoEntiProprietari.Visible = False
        PanelCensimentoPOS.Visible = False
        PanelCensimentoCircuiti.Visible = False
        PanelCensimentoAcquires.Visible = False
        PanelCensimentoTipologieErrori.Visible = False
        PanelCensimentoActionCode.Visible = False
        PanelFunzionalita.Visible = False
        PanelEntiTransazioneTelefono.Visible = False
        Panel_anagrafica_esercenti_telefonici.Visible = False
    End Sub

    Protected Sub btnCensimentoPOS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCensimentoPOS.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.CensimentoPOS) <> "1" Then
            MostraNascondiPulsanti(False)
            btnCensimentoPOS.Visible = True
            PanelCensimentoPOS.Visible = True
            puntoCensimentoPOS.Visible = True
            btnTorna.Visible = True
            puntoTorna.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If

    End Sub

    Protected Sub btnCensimentoEntiProprietari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCensimentoEntiProprietari.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 63) <> "1" Then
            Response.Redirect("\tabelle_pos\pos_enti.aspx")
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnCensimentoCircuiti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCensimentoCircuiti.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 64) <> "1" Then
            Response.Redirect("\tabelle_pos\pos_circuiti.aspx")
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub
    Protected Sub btnFunzionalita_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFunzionalita.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 69) <> "1" Then
            Response.Redirect("\tabelle_pos\pos_funzionalita.aspx")
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnEntiTransazioneTelefono_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEntiTransazioneTelefono.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.CensimentoTransazioneTelefonica) <> "1" Then
            MostraNascondiPulsanti(False)
            btnEntiTransazioneTelefono.Visible = True
            PanelEntiTransazioneTelefono.Visible = True
            puntoEntiTransazioneTelefono.Visible = True
            btnTorna.Visible = True
            puntoTorna.Visible = True

            anagrafica_enti_telefonici.InitForm(stato:=2)
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If

    End Sub

    Protected Sub btn_anagrafica_esercenti_telefonici_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_anagrafica_esercenti_telefonici.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.CensimentoTransazioneTelefonica) <> "1" Then
            MostraNascondiPulsanti(False)
            btn_anagrafica_esercenti_telefonici.Visible = True
            Panel_anagrafica_esercenti_telefonici.Visible = True
            punto_anagrafica_esercenti_telefonici.Visible = True
            btnTorna.Visible = True
            puntoTorna.Visible = True

            anagrafica_esercenti_telefonici.InitForm(stato:=2)
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If

    End Sub

    Protected Sub btnCensimentoPOS2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCensimentoPOS2.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 59) <> "1" Then
            Response.Redirect("\tabelle_pos\pos_gestione_pos.aspx")
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub
End Class
