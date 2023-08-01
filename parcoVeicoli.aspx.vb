Imports funzioni_comuni
Partial Class parcoVeicoli2
    Inherits System.Web.UI.Page

    Dim funzioni As New funzioni_comuni

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim almeno_un_tab_visibile As Boolean = False

            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 2) = "1" Then
                dati_generali.Visible = False
            Else
                almeno_un_tab_visibile = True
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 3) = "1" Then
                tabAccessori.Visible = False
            Else
                almeno_un_tab_visibile = True
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 4) = "1" Then
                tabAcquisto.Visible = False
            Else
                almeno_un_tab_visibile = True
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 5) = "1" Then
                tabVendita.Visible = False
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 6) = "1" Then
                tabAssicurazioni.Visible = False
            Else
                almeno_un_tab_visibile = True
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 7) = "1" Then
                tabLeasing.Visible = False
            Else
                almeno_un_tab_visibile = True
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 8) = "1" Then
                tabManutenzione.Visible = False
            Else
                almeno_un_tab_visibile = True
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 86) = "1" Then
                tabDocumenti.Visible = False
            Else
                almeno_un_tab_visibile = True
            End If

            'SE PROVENGO DA UN'ALTRA PAGINA CONTROLLO SE E' STATO RICHIESTO UN TAB IN PARTICOLARE

            If Request.QueryString("val") = "gen" Then
                tabPanel1.ActiveTab = tabDatiGenerali
            End If
            'If Request.QueryString("val") = "acc" Then
            '    tabPanel1.ActiveTab = tabAccessori
            'End If
            If Request.QueryString("val") = "ass" Then
                tabPanel1.ActiveTab = tabAssicurazioni
            End If
            If Request.QueryString("val") = "ven" Then
                tabPanel1.ActiveTab = tabVendita
            End If
            If Request.QueryString("val") = "lea" Then
                tabPanel1.ActiveTab = tabLeasing
            End If

            If Not almeno_un_tab_visibile Then
                Session("provenienza") = "parco_veicoli"
                Session("parco_accesso_negato") = "SI"
                Response.Redirect("cercaVeicoli.aspx")
            End If

        End If
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        If Session("provenienza") = "bolli.aspx" Then
            ' se provengo da bolli ripulisco alla fine del caricamento di ogni ascx le variabili...
            Session("provenienza") = ""
            Session("posizione_num_pagina_bollo") = Nothing
        End If
    End Sub
End Class
