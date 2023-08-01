
Partial Class tabelle_multe
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 93) = "1" Then
                Response.Redirect("default.aspx")
            End If
            If Request.Cookies("SicilyRentCar") Is Nothing Then
                Response.Redirect("LogIn.aspx")
            Else
                AzzeraTab()
            End If
        End If
    End Sub
    Protected Sub AzzeraTab()
        panelArticoliCDS.Visible = False
        panelEnti.Visible = False
        panelProvenienza.Visible = False
        panelTipoAllegato.Visible = False
        panelModelloRicorsi.Visible = False
        panelCasistiche.Visible = False
        panelCausaliFattura.Visible = False
    End Sub

    Protected Sub btnArticoliCDS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnArticoliCDS.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 93) <> "1" Then
            AzzeraTab()
            panelArticoliCDS.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnEnti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnti.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 93) <> "1" Then
            AzzeraTab()
            panelEnti.Visible = True

            Session("glCercaEnti") = "Y"


            'azzera valori sul panelEnti salvo 21.03.2023
            Dim txt As TextBox = TryCast(Page.FindControl("ctl00$ContentPlaceHolder1$Enti1$txtEnte"), TextBox)
            txt.Text = ""
            txt = TryCast(Page.FindControl("ctl00$ContentPlaceHolder1$Enti1$txtIndirizzo"), TextBox)
            txt.Text = ""
            txt = TryCast(Page.FindControl("ctl00$ContentPlaceHolder1$Enti1$txtComune"), TextBox)
            txt.Text = ""
            txt = TryCast(Page.FindControl("ctl00$ContentPlaceHolder1$Enti1$txtCap"), TextBox)
            txt.Text = ""
            txt = TryCast(Page.FindControl("ctl00$ContentPlaceHolder1$Enti1$txtProv"), TextBox)
            txt.Text = ""
            txt = TryCast(Page.FindControl("ctl00$ContentPlaceHolder1$Enti1$txt_enti_tel"), TextBox)
            txt.Text = ""
            txt = TryCast(Page.FindControl("ctl00$ContentPlaceHolder1$Enti1$txt_enti_tel"), TextBox)
            txt.Text = ""
            txt = TryCast(Page.FindControl("ctl00$ContentPlaceHolder1$Enti1$txt_enti_email"), TextBox)
            txt.Text = ""
            txt = TryCast(Page.FindControl("ctl00$ContentPlaceHolder1$Enti1$txt_enti_emailpec"), TextBox)
            txt.Text = ""
            txt = TryCast(Page.FindControl("ctl00$ContentPlaceHolder1$Enti1$txt_enti_notes"), TextBox)
            txt.Text = ""






        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnChiudi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudi.Click
        Response.Redirect("RicercaMulte.aspx")
    End Sub

    Protected Sub btnProvenienza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProvenienza.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 93) <> "1" Then
            AzzeraTab()
            panelProvenienza.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnTipoAllegato_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTipoAllegato.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 93) <> "1" Then
            AzzeraTab()
            panelTipoAllegato.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnModelloRicorso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModelloRicorso.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 93) <> "1" Then
            AzzeraTab()
            panelModelloRicorsi.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnCasistiche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCasistiche.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 93) <> "1" Then
            AzzeraTab()
            panelCasistiche.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnCausaliFattura_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCausaliFattura.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 93) <> "1" Then
            AzzeraTab()
            panelCausaliFattura.Visible = True
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub


End Class
