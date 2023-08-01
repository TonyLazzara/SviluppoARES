Imports funzioni_comuni

Partial Class gestione_fatture
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Write("ID " & Session("IdFattura"))
        'Response.End()

        'Trace.Write("Page_Load Page -----------------------------------------")
        'fatture_Prenotazione.tipo_fattura = TipoFattura.Prenotazione
        fatture_Noleggio.tipo_fattura = TipoFattura.Noleggio
        'fatture_RDS.tipo_fattura = TipoFattura.RDS
        fatture_Multe.tipo_fattura = TipoFattura.Multe

        If Not Page.IsPostBack Then
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Fatturazione) = "1" Then
                Response.Redirect("default.aspx")
            End If
            'If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Fatturazione) = "1" Then
            '    fatture_Prenotazione.Visible = False
            'End If

            ''LE FATTURE NOLO VENGONO GESTITE IN UN'ALTRA PAGINA
            'fatture_Noleggio.Visible = False

            'If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Fatturazione) = "1" Then
            '    fatture_RDS.Visible = False
            'End If
            'If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Fatturazione) = "1" Then
            '    fatture_Multe.Visible = False
            'End If

            'Trace.Write("Page_Load Page Session: " & Session("fatture_provenienza"))

            'SE PROVENGO DA UN'ALTRA PAGINA CONTROLLO SE E' STATO RICHIESTO UN TAB IN PARTICOLARE
            If Session("fatture_provenienza") Is Nothing Then
                'Trace.Write("Page_Load Page Session: Nothing")
                If fatture_Noleggio.Visible Then
                    Session("fatture_provenienza") = TipoFattura.Noleggio
                    'ElseIf fatture_RDS.Visible Then
                    '   Session("fatture_provenienza") = TipoFattura.RDS
                    'ElseIf fatture_Prenotazione.Visible Then
                    '   Session("fatture_provenienza") = TipoFattura.Prenotazione
                ElseIf fatture_Multe.Visible Then
                    Session("fatture_provenienza") = TipoFattura.Multe
                End If
            End If

            Dim fatture_provenienza As TipoFattura = Session("fatture_provenienza")

            'Trace.Write("Page_Load Page fatture_provenienza: " & fatture_provenienza)

            Select Case fatture_provenienza
                Case TipoFattura.Prenotazione
                    'tabPanelFatture.ActiveTab = tabFatturePrenotazioni
                Case TipoFattura.Noleggio
                    tabPanelFatture.ActiveTab = TabFattureNoleggio
                    'Case TipoFattura.RDS
                    '    tabPanelFatture.ActiveTab = TabFattureRDS
                Case TipoFattura.Multe
                    tabPanelFatture.ActiveTab = TabFattureMulte
                Case Else
                    'tabPanelFatture.ActiveTab = tabFatturePrenotazioni
            End Select
        End If
    End Sub

End Class