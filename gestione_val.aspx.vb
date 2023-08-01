Imports funzioni_comuni

Partial Class tabelle_val
    Inherits System.Web.UI.Page
    Dim funzioni As New funzioni_comuni

    Protected Sub AzzeraTab()
        PanelAccessorioVAL.Visible = False
        PanelTemplateVAL.Visible = False
        PanelGruppiStazioni.Visible = False
    End Sub

    Protected Sub mostraPulsanti()
        btnAccessorioVAL.Visible = True
        btnTemplateVAL.Visible = True
        btnTemplateGruppiVAL.Visible = True

        puntoAccessorioVAL.Visible = True
        puntoTemplateVAL.Visible = True
        puntoTemplateGruppiVAL.Visible = True
    End Sub

    Protected Sub NascondiPulsanti()
        btnAccessorioVAL.Visible = False
        btnTemplateVAL.Visible = False
        btnTemplateGruppiVAL.Visible = False

        puntoAccessorioVAL.Visible = False
        puntoTemplateVAL.Visible = False
        puntoTemplateGruppiVAL.Visible = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session("cambia_tab") = ""
            Session("orario") = ""
            Session("prop") = ""
            Session("staz") = ""
            Session("zona") = ""
            Session("festivo") = ""

            If Request.Cookies("SicilyRentCar") Is Nothing Then
                Response.Redirect("LogIn.aspx")
            Else
                AzzeraTab()
            End If
        End If


    End Sub

    Protected Sub btnAccessorioVAL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccessorioVAL.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 56) <> "1" Then
            NascondiPulsanti()
            'Session("prev_page") = "orari"
            PanelAccessorioVAL.Visible = True
            btnAccessorioVAL.Visible = True
            puntoAccessorioVAL.Visible = True
            btnTorna.Visible = True
            puntoTorna.Visible = True
            divmenu.Visible = False
            Label1.Text = "Gestione VAL - Accessorio VAL"
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub


    Protected Sub btnTemplateVAL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTemplateVAL.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 57) <> "1" Then
            NascondiPulsanti()
            'Session("prev_page") = "feste"
            PanelTemplateVAL.Visible = True
            btnTemplateVAL.Visible = True
            puntoTemplateVAL.Visible = True
            btnTorna.Visible = True
            puntoTorna.Visible = True
            divmenu.Visible = False
            Label1.Text = "Gestione VAL - Template VAL"
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnTemplateGruppiVAL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTemplateGruppiVAL.Click
        If funzioni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 77) <> "1" Then
            NascondiPulsanti()
            'Session("prev_page") = "feste"
            PanelGruppiStazioni.Visible = True
            btnTemplateGruppiVAL.Visible = True
            puntoTemplateGruppiVAL.Visible = True
            btnTorna.Visible = True
            puntoTorna.Visible = True
            divmenu.Visible = False
            Label1.Text = "Gestione VAL - Template Gruppi VAL"
        Else
            Libreria.genUserMsgBox(Me, "Accesso negato.")
        End If
    End Sub

    Protected Sub btnTorna_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTorna.Click
        AzzeraTab()
        mostraPulsanti()
        btnTorna.Visible = False
        puntoTorna.Visible = False
        divmenu.Visible = True
        Label1.Text = "Gestione VAL"

        Session("cambia_tab") = ""
    End Sub

End Class
