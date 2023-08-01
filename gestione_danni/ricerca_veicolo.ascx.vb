
Partial Class gestione_danni_ricerca_veicolo
    Inherits System.Web.UI.UserControl

    Public Delegate Sub EventoSelezionaVeicolo(ByVal sender As Object, ByVal e As EventoNuovoRecord)
    Event SelezionaVeicolo As EventHandler

    Private Enum DivVisibile
        Nessuno = 0
        Ricerca = 1
        ElencoVeicoli = 2
        GestioneRicerca = Ricerca Or ElencoVeicoli
    End Enum

    Private Sub Visibilita(ByVal Valore As DivVisibile)
        Trace.Write("Visibilita gestione_danni: " & Valore.ToString & " " & Valore)

        div_ricerca_targa.Visible = Valore And DivVisibile.Ricerca

        div_elenco_veicoli.Visible = Valore And DivVisibile.ElencoVeicoli
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Write(Page.IsPostBack)
        sqlElencoVeicoli.SelectCommand = lb_sqlElencoVeicoli.Text

        If Not Page.IsPostBack Then                        

            DropDownStazioni_x_apertura.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
            DropDownStazioni_x_apertura.Enabled = False

            Visibilita(DivVisibile.Ricerca)
        End If

        'Tony 16/02/2023
        'Funzione: Stazione di appartenenza        
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "167") < 3 Then
            DropDownStazioni_x_apertura.Enabled = False
        Else
            DropDownStazioni_x_apertura.Enabled = True
        End If
        'FINE Tony
    End Sub


    Protected Sub bt_cerca_veicoli_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_cerca_veicoli.Click
        Dim url As String = HttpContext.Current.Request.Url.AbsolutePath
        'Response.Write(url)
        'Response.End()

        'Tony 20-09-2022 / 30-09-2022

        'Funzione: Apertura RDS /ODL
        If url = "/gestione_danni.aspx" Then
            'Funzione: Apertura RDS
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "166") < 3 Then
                Libreria.genUserMsgBox(Page, "Non hai i permessi per Aprire RDS")
                Return
            Else
                'Funzione: Stazione di appartenenza
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "167") < 3 Then
                    DropDownStazioni_x_apertura.Enabled = False
                Else
                    DropDownStazioni_x_apertura.Enabled = True
                End If


                lb_sqlElencoVeicoli.Text = genera_sql()

                sqlElencoVeicoli.SelectCommand = lb_sqlElencoVeicoli.Text

                'Response.Write("SQL " & sqlElencoVeicoli.SelectCommand)
                Trace.Write(sqlElencoVeicoli.SelectCommand)

                listViewElencoVeicoli.DataBind()

                Visibilita(DivVisibile.GestioneRicerca)
            End If
            'FINE Tony
        ElseIf url = "/gestione_odl.aspx" Then
            lb_sqlElencoVeicoli.Text = genera_sql()

            sqlElencoVeicoli.SelectCommand = lb_sqlElencoVeicoli.Text

            'Response.Write("SQL " & sqlElencoVeicoli.SelectCommand)
            Trace.Write(sqlElencoVeicoli.SelectCommand)

            listViewElencoVeicoli.DataBind()

            Visibilita(DivVisibile.GestioneRicerca)
        End If
        

    End Sub

    Protected Function genera_sql() As String
        Dim sqlStr As String = getSelect()

        'Tony 20-09-2022
        If DropDownStazioni_x_apertura.SelectedValue = "0" Then
            sqlStr += getClausolaWhereSqlElencoVeicoliSenzaIDStazione(tx_targa_x_apertura.Text, _
                Integer.Parse(DropDownProprietario_x_apertura.SelectedValue), _
                Integer.Parse(DropDownStatodanni.SelectedValue), _
                Integer.Parse(DropDownTipoRecordDanno.SelectedValue), _
                Integer.Parse(DropDownTipologia.SelectedValue))
        Else
            sqlStr += getClausolaWhereSqlElencoVeicoli(tx_targa_x_apertura.Text, _
                Integer.Parse(DropDownProprietario_x_apertura.SelectedValue), _
                Integer.Parse(DropDownStazioni_x_apertura.SelectedValue), _
                Integer.Parse(DropDownStatodanni.SelectedValue), _
                Integer.Parse(DropDownTipoRecordDanno.SelectedValue), _
                Integer.Parse(DropDownTipologia.SelectedValue))
        End If
        'FINE Tony
        

        sqlStr += getOrderBy()

        Return sqlStr
    End Function

    Protected Function getOrderBy() As String
        Dim sqlStr As String = ""

        sqlStr += " ORDER BY v.targa" ' gestire in qualche modo l'ordinamento...

        Return sqlStr
    End Function

    Protected Function getSelect() As String
        Dim sqlStr As String = "SELECT DISTINCT v.id id_veicolo, v.targa, s.nome_stazione, p.descrizione proprietario" & _
            " FROM veicoli v WITH(NOLOCK)" & _
            " LEFT JOIN stazioni s WITH(NOLOCK) ON v.id_stazione = s.id" & _
            " LEFT JOIN proprietari_veicoli p WITH(NOLOCK) ON v.id_proprietario = p.id" & _
            " LEFT JOIN veicoli_danni d WITH(NOLOCK) ON v.id = d.id_veicolo AND d.attivo = 1" & _
            " LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON d.id_posizione_danno = pd.id" & _
            " WHERE 1 = 1"

        Return sqlStr
    End Function

    Protected Function getClausolaWhereSqlElencoVeicoli(ByVal targa As String, ByVal id_proprietario As Integer, ByVal id_stazione As Integer, ByVal stato_danni As Integer, ByVal id_tipo_record_danni As tipo_record_danni, ByVal tipologia As Integer) As String
        Dim sqlStr As String = ""

        If targa <> "" Then
            sqlStr += " AND v.targa = '" & Libreria.formattaSql(targa) & "'"
            sqlStr += " AND v.id_stazione = " & id_stazione
        Else
            sqlStr += " AND v.data_atto_vendita IS NULL"

            If id_proprietario > 0 Then
                sqlStr += " AND v.id_proprietario = '" & id_proprietario & "'"
            End If

            If id_stazione > 0 Then
                sqlStr += " AND v.id_stazione = " & id_stazione
            End If

            Select Case stato_danni
                Case 1 ' danno aperto
                    sqlStr += " AND d.stato = 1" ' danno aperto
                Case 2 ' fermo tecnico
                    sqlStr += " AND v.da_riparare = 1" ' danno chiuso
            End Select

            Select Case id_tipo_record_danni
                Case tipo_record_danni.Danno_Carrozzeria, tipo_record_danni.Danno_Meccanico, tipo_record_danni.Danno_Elettrico, tipo_record_danni.Dotazione
                    sqlStr += " AND d.tipo_record = " & id_tipo_record_danni
            End Select

            Select Case tipologia
                Case 1
                    sqlStr += " AND pd.bloccante = 1"
                Case 2
                    sqlStr += " AND ISNULL(pd.bloccante, 0) = 0"
            End Select
        End If

        Return sqlStr
    End Function

    'Tony 20-09-2022
    Protected Function getClausolaWhereSqlElencoVeicoliSenzaIDStazione(ByVal targa As String, ByVal id_proprietario As Integer, ByVal stato_danni As Integer, ByVal id_tipo_record_danni As tipo_record_danni, ByVal tipologia As Integer) As String
        Dim sqlStr As String = ""

        If targa <> "" Then
            sqlStr += " AND v.targa = '" & Libreria.formattaSql(targa) & "'"            
        Else
            sqlStr += " AND v.data_atto_vendita IS NULL"

            If id_proprietario > 0 Then
                sqlStr += " AND v.id_proprietario = '" & id_proprietario & "'"
            End If


            Select Case stato_danni
                Case 1 ' danno aperto
                    sqlStr += " AND d.stato = 1" ' danno aperto
                Case 2 ' fermo tecnico
                    sqlStr += " AND v.da_riparare = 1" ' danno chiuso
            End Select

            Select Case id_tipo_record_danni
                Case tipo_record_danni.Danno_Carrozzeria, tipo_record_danni.Danno_Meccanico, tipo_record_danni.Danno_Elettrico, tipo_record_danni.Dotazione
                    sqlStr += " AND d.tipo_record = " & id_tipo_record_danni
            End Select

            Select Case tipologia
                Case 1
                    sqlStr += " AND pd.bloccante = 1"
                Case 2
                    sqlStr += " AND ISNULL(pd.bloccante, 0) = 0"
            End Select
        End If

        Return sqlStr
    End Function
    'FINE

    Protected Sub listViewElencoVeicoli_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listViewElencoVeicoli.ItemCommand
        If e.CommandName = "lente" Then
            Dim lb_id_veicolo_list As Label = e.Item.FindControl("lb_id_veicolo")

            Dim ev As EventoNuovoRecord = New EventoNuovoRecord
            ev.Valore = Integer.Parse(lb_id_veicolo_list.Text)

            RaiseEvent SelezionaVeicolo(Me, ev)
        End If
    End Sub

End Class
