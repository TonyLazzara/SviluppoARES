
Partial Class gestione_danni_tipo_origine_danno
    Inherits System.Web.UI.UserControl

    Public Delegate Sub EventoChiusuraForm(ByVal sender As Object, ByVal e As EventArgs)
    Event ChiusuraForm As EventHandler

    Public Delegate Sub EventoChiusuraEdit(ByVal sender As Object, ByVal e As EventArgs)
    Event ChiusuraEdit As EventHandler

    Private Enum DivVisibile
        Nessuno = 0
        Elenco = 1
        Modifica = 2
        Intestazione = 4
    End Enum

    Private Sub Visibilita(Valore As DivVisibile)
        Trace.Write("Visibilita gestione_danni_tipo_origine_danno: " & Valore.ToString)

        div_elenco_origine_danno.Visible = Valore And DivVisibile.Elenco
        div_modifica_tipo_danno.Visible = Valore And DivVisibile.Modifica
        div_intestazione.Visible = Valore And DivVisibile.Intestazione
    End Sub

    Public Sub InitForm(Optional mio_record As veicoli_tipo_documento_apertura_danno = Nothing, Optional stato As Integer = 0, Optional provenienza As String = "")
        lb_stato.Text = stato
        lb_provenienza.Text = provenienza

        If mio_record IsNot Nothing Then
            FillEdit(mio_record)
            btnModifica.Text = "Modifica"
            Visibilita(DivVisibile.Modifica)
            Return
        End If

        If stato = 3 Then
            AzzeraEdit()
            btnModifica.Text = "Salva"
            Visibilita(DivVisibile.Modifica Or DivVisibile.Intestazione)
            Return
        End If

        Visibilita(DivVisibile.Elenco)
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Request.Cookies("SicilyRentCar") Is Nothing Then
            Response.Redirect("LogIn.aspx")
            Return
        End If
        'If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneOrigineDanno) = "1" Then
        '    Response.Redirect("default.aspx")
        '    Return
        'End If

        If Not Page.IsPostBack Then
            Trace.Write("Not Page.IsPostBack")

            Visibilita(DivVisibile.Elenco)
        Else
            Trace.Write("Page.IsPostBack")
        End If
    End Sub

    Protected Sub ChiudiModifica()
        If lb_stato.Text = "0" Or lb_stato.Text = "2" Then
            Visibilita(DivVisibile.Elenco)
        ElseIf lb_stato.Text = "1" Then
            If lb_provenienza.Text = "" Then
                Response.Redirect("default.aspx")
            Else
                Response.Redirect(lb_provenienza.Text)
            End If
        ElseIf lb_stato.Text = "3" Then
            ' rispondo con un evento sulla modifica del record...
            Dim e As EventoNuovoRecord = New EventoNuovoRecord
            e.Valore = 0
            RaiseEvent ChiusuraEdit(Me, e)
        End If
    End Sub

    Protected Sub btnChiudiModifica_Click(sender As Object, e As System.EventArgs) Handles btnChiudiModifica.Click
        ChiudiModifica()
    End Sub

    Protected Sub ChiudiElenco()
        If lb_stato.Text = "1" Then
            If lb_provenienza.Text = "" Then
                Response.Redirect("default.aspx")
            Else
                Response.Redirect(lb_provenienza.Text)
            End If
        ElseIf lb_stato.Text = "2" Then
            RaiseEvent ChiusuraForm(Me, New EventArgs)
        End If
    End Sub

    Protected Sub btnChiudi_Click(sender As Object, e As System.EventArgs) Handles btnChiudi.Click
        ChiudiElenco()
    End Sub

    Protected Sub FillEdit(mio_record As veicoli_tipo_documento_apertura_danno)
        With mio_record
            lb_id.Text = .id
            tx_descrizione.Text = .descrizione
            lb_richiede_id.Text = .richiede_id
            tx_codice_sintetico.Text = .codice_sintetico
        End With
    End Sub

    Protected Sub AzzeraEdit()
        Dim mio_record As veicoli_tipo_documento_apertura_danno = New veicoli_tipo_documento_apertura_danno
        With mio_record
            .id = 0
            .descrizione = ""
            .codice_sintetico = ""
            .richiede_id = False
        End With
        FillEdit(mio_record)
    End Sub

    Protected Sub btnNuovo_Click(sender As Object, e As System.EventArgs) Handles btnNuovo.Click
        AzzeraEdit()
        btnModifica.Text = "Salva"
        Visibilita(DivVisibile.Modifica)
    End Sub

    Protected Sub btnModifica_Click(sender As Object, e As System.EventArgs) Handles btnModifica.Click
        Dim mio_record As veicoli_tipo_documento_apertura_danno = New veicoli_tipo_documento_apertura_danno

        With mio_record
            .id = Integer.Parse(lb_id.Text)
            .descrizione = tx_descrizione.Text
            .richiede_id = Boolean.Parse(lb_richiede_id.Text)
            .codice_sintetico = tx_codice_sintetico.Text

            If .id = 0 Then
                .SalvaRecord()
            Else
                .AggiornaRecord()
            End If
        End With

        If lb_stato.Text = "3" Then
            ' rispondo con un evento sulla modifica del record...
            Dim my_e As EventoNuovoRecord = New EventoNuovoRecord
            my_e.Valore = mio_record.id
            RaiseEvent ChiusuraEdit(Me, my_e)
        Else
            listViewOrigineDanni.DataBind()

            Visibilita(DivVisibile.Elenco)
        End If

    End Sub

    Protected Sub listViewTipoDanni_ItemCommand(sender As Object, e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listViewOrigineDanni.ItemCommand
        If e.CommandName = "Modifica" Then
            Dim lb_id As Label = e.Item.FindControl("lb_id")

            Dim mio_record As veicoli_tipo_documento_apertura_danno = veicoli_tipo_documento_apertura_danno.get_record_da_id(lb_id.Text)

            FillEdit(mio_record)

            btnModifica.Text = "Modifica"

            Visibilita(DivVisibile.Modifica)

        ElseIf e.CommandName = "Elimina" Then
            Dim lb_id As Label = e.Item.FindControl("lb_id")
            Dim mio_record As veicoli_tipo_documento_apertura_danno = veicoli_tipo_documento_apertura_danno.get_record_da_id(Integer.Parse(lb_id.Text))

            If mio_record.CancellaRecord() Then

                listViewOrigineDanni.DataBind()

                Libreria.genUserMsgBox(Page, "Tipo origine danno veicolo eliminato correttamente")
            Else
                Libreria.genUserMsgBox(Page, "Errore nell'eliminazione del tipo origine danno.")
            End If
        End If
    End Sub

End Class
