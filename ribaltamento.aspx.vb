Imports funzioni_comuni

Partial Class ribaltamento
    Inherits System.Web.UI.Page

    Dim funzioni As New funzioni_comuni

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Trace.Write("Page_Load Page -----------------------------------------")
        ImportXml.ribaltamento_provenienza = OrigineImport.Xml
        ImportWeb.ribaltamento_provenienza = OrigineImport.Web
        ImportR55.ribaltamento_provenienza = OrigineImport.R55
        ImportDollar.ribaltamento_provenienza = OrigineImport.Dollar
        ImportThrifty.ribaltamento_provenienza = OrigineImport.Thrifty

        If Not Page.IsPostBack Then
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 82) = "1" Then
                ImportXml.Visible = False
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 82) = "1" Then
                ImportWeb.Visible = False
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 82) = "1" Then
                ImportR55.Visible = False
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 82) = "1" Then
                ImportDollar.Visible = False
            End If
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 82) = "1" Then
                ImportThrifty.Visible = False
            End If

            Trace.Write("Page_Load Page Session: " & Session("ribaltamento_provenienza"))

            'SE PROVENGO DA UN'ALTRA PAGINA CONTROLLO SE E' STATO RICHIESTO UN TAB IN PARTICOLARE
            If Session("ribaltamento_provenienza") Is Nothing Then
                Trace.Write("Page_Load Page Session: Nothing")
                If ImportXml.Visible Then
                    Session("ribaltamento_provenienza") = OrigineImport.Xml
                ElseIf ImportWeb.Visible Then
                    Session("ribaltamento_provenienza") = OrigineImport.Web
                ElseIf ImportR55.Visible Then
                    Session("ribaltamento_provenienza") = OrigineImport.R55
                ElseIf ImportDollar.Visible Then
                    Session("ribaltamento_provenienza") = OrigineImport.Dollar
                ElseIf ImportThrifty.Visible Then
                    Session("ribaltamento_provenienza") = OrigineImport.Thrifty
                End If
            End If

            Dim ribaltamento_provenienza As OrigineImport = Session("ribaltamento_provenienza")

            Trace.Write("Page_Load Page ribaltamento_provenienza: " & ribaltamento_provenienza)

            Select Case ribaltamento_provenienza
                Case OrigineImport.Xml
                    tabPanelImport.ActiveTab = tabImportXml
                Case OrigineImport.Web
                    tabPanelImport.ActiveTab = TabImportWeb
                Case OrigineImport.R55
                    tabPanelImport.ActiveTab = TabImportR55
                Case OrigineImport.Dollar
                    tabPanelImport.ActiveTab = TabImportDollar
                Case OrigineImport.Thrifty
                    tabPanelImport.ActiveTab = TabImportThrifty
                Case Else
                    tabPanelImport.ActiveTab = tabImportXml
            End Select
        End If
    End Sub

End Class
