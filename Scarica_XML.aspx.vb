
Partial Class Scarica_XML
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If IsNothing(Request.Cookies("SicilyRentCar")) Then
            Response.Redirect("default.aspx")
        End If

        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Fatturazione) = "1" Then
            Response.Redirect("default.aspx")
        End If

        If Not IsPostBack Then
            'Response.Write("NO Post Back")
            ricerca("")
        Else
            'Response.Write("Post Back")
        End If
    End Sub

    Protected Sub ricerca(ByVal order_by As String)

        Dim StrQuery As String
        Dim condizione As String = ""
        Dim ordine As String

        If order_by <> "" Then
            ordine = order_by
            lblOrderBy.Text = ID
        Else
            'ordine = " order by impegno_vendite.id asc"
            ordine = " order by id asc"
        End If

        ''Dautilizzare su tutti i campi editabili
        'If sql_inj(txtCercaCodCliente.Text) Then
        '    Response.Redirect("teantaivosqlinj.aspx")
        'End If

        If Session("GiornoDaScaricare") <> "" Then

            Dim Giorno, Mese, Anno, GiornoDaScaricare As String
            Dim gmaArray() As String

            gmaArray = Split(Session("GiornoDaScaricare"), "/")
            Giorno = gmaArray(0)
            Mese = gmaArray(1)
            Anno = gmaArray(2)

            GiornoDaScaricare = Giorno & "-" & Mese & "-" & Anno
            condizione = " and cartella='" & Session("TipFattura") & " " & GiornoDaScaricare & "'"


        Else    'modificato 16.09.2021 'se c'è la cartella la inserisce altrimenti mette il numero di fattura

            'If Session("NumFatturaDaScaricare") <> "" Then         'rimosso 16.09.2021

            condizione = " and cartella='" & Session("NumFatturaDaScaricare") & "'"


        End If

        'StrQuery = "select * from elenco_file_scaricati_lista"
        StrQuery = "select XML_elenco_file_scaricati_lista.*, XML_elenco_file_scaricati_cartella.* from XML_elenco_file_scaricati_lista,XML_elenco_file_scaricati_cartella where XML_elenco_file_scaricati_lista.id_e_f_s_d = XML_elenco_file_scaricati_cartella.id " & condizione

        'Response.Write("Num FAtt " & Session("NumFatturaDaScaricare") & " " & StrQuery & "<br><br>")
        lblTxtQuery.Text = StrQuery
        'Response.End()

        sqlAllegati.SelectCommand = StrQuery

        dataListAllegati.DataBind()
        dataListAllegati.Visible = True
    End Sub

    Protected Sub dataListAllegati_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles dataListAllegati.ItemDataBound
        Dim StrNomeFile As HyperLink = e.Item.FindControl("lblNome")
        Dim StrNomeFileArray() As String

        StrNomeFileArray = Split(StrNomeFile.Text, "/")
        StrNomeFile.Text = StrNomeFileArray(3)
    End Sub

    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHome.Click
        Response.Redirect("/")
    End Sub
End Class
