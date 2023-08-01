
Partial Class lista_condizioni
    Inherits System.Web.UI.Page
    Dim sqla As String = ""

    Protected Function deleterigaperiodo(ByVal idrec As String) As Boolean

        'aggiunta salvo 11.07.2023
        Dim ris As Boolean = False
        Dim sqlstr As String = "DELETE from condizioni_righe_costi_periodi where id='" & idrec & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim dbaction As Integer = Cmd.ExecuteNonQuery()
            If dbaction > 0 Then
                ris = True
            End If



            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception

        End Try

        Return ris

    End Function

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("it-IT")
        Try



            If Not Page.IsPostBack Then

                '# se richiamata da eliminazione dati aggiunto salvo 11.07.2023
                If Request.QueryString("mode") = "del" And Request.QueryString("idrec") <> "" Then
                    Dim delcon As Boolean = deleterigaperiodo(Request.QueryString("idrec"))
                    Session("richiama_condizioni_id_riga") = Request.QueryString("idriga")
                    Session("elimina_costoperiodo_riga") = "si"

                    'ElseIf Request.QueryString("mode") = "upd" And Request.QueryString("idrec") <> "" Then
                    'vedi_riga(Request.QueryString("idriga"), "0")

                Else
                    Session("elimina_costoperiodo_riga") = ""
                End If
                '@ end salvo


                lblQuery.Text = "SELECT id, codice, valido_da, valido_a, data_creazione, id_template_val, iva_inclusa " &
                   "FROM condizioni WITH(NOLOCK) WHERE attivo='1' ORDER BY data_creazione DESC"


                    If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Condizioni) = "1" Then
                        Response.Redirect("default.aspx")
                    End If

                    If Not IsNothing(Session("richiama_condizioni_id_riga")) And Session("richiama_condizioni_id_riga") <> "" Then

                        '#start recupero valori da session  salvo 03.05.2023
                        lbl_id_codice.Text = Session("richiama_condizioni_idcodice")
                        lblCodice.Text = Session("richiama_condizioni_codice")
                        txtValidoDa.Text = Session("richiama_condizioni_val_da")
                        txtValidoA.Text = Session("richiama_condizioni_val_a")
                        'id_template_val.Text = Session("richiama_condizioni_id_template_val")

                        If Not IsNothing(Session("richiama_condizioni_periodo_da")) And Not IsDBNull(Session("richiama_condizioni_periodo_da")) Then
                            txt_periodo_da.Text = Session("richiama_condizioni_periodo_da")
                        End If
                        If Not IsNothing(Session("richiama_condizioni_periodo_a")) And Not IsDBNull(Session("richiama_condizioni_periodo_a")) Then
                            txt_periodo_a.Text = Session("richiama_condizioni_periodo_a")
                        End If

                        dropIva.SelectedValue = Session("richiama_condizioni_iva_inclusa")
                        dropTemplateVal.SelectedValue = Session("richiama_condizioni_template_val")

                        'sezione elemento
                        If Not IsNothing(Session("richiama_condizioni_drop_id_elemento")) And Not IsDBNull(Session("richiama_condizioni_drop_id_elemento")) Then
                            dropElementi.SelectedValue = Session("richiama_condizioni_drop_id_elemento")
                        End If

                        'dropElementi.Enabled = False 'da attivare
                        If Not IsDBNull(Session("richiama_condizioni_drop_id_elemento_val")) Then
                            dropElementoVal.SelectedValue = Session("richiama_condizioni_drop_id_elemento_val")
                        Else
                            dropElementoVal.SelectedIndex = -1
                        End If

                        If Not IsDBNull(Session("richiama_condizioni_drop_id_metodo_stampa")) Then
                            dropTipoStampa.SelectedValue = Session("richiama_condizioni_drop_id_metodo_stampa")
                        Else
                            dropTipoStampa.SelectedIndex = -1
                        End If

                        If Not IsDBNull(Session("richiama_condizioni_drop_A_carico_di")) Then
                            dropACaricoDi.SelectedValue = Session("richiama_condizioni_drop_A_carico_di")
                        Else
                            dropACaricoDi.SelectedIndex = -1
                        End If

                        If Session("richiama_condizioni_ckPacked") = "1" Then
                            chkPacked.Checked = True
                        Else
                            chkPacked.Checked = False
                        End If

                        If Session("richiama_condizioni_ckObbligatorio") = "1" Then
                            chkObbligatorio.Checked = True
                        Else
                            chkObbligatorio.Checked = False
                        End If

                        If Not IsDBNull(Session("richiama_condizioni_drop_unita_misura")) Then
                            dropUnitaDiMisura.SelectedValue = Session("richiama_condizioni_drop_unita_misura")
                        Else
                            dropUnitaDiMisura.SelectedIndex = -1
                        End If

                        If Not IsDBNull(Session("richiama_condizioni_costo")) Then
                            txtCosto.Text = Session("richiama_condizioni_costo")
                        Else
                            txtCosto.Text = ""
                        End If

                        If Not IsDBNull(Session("richiama_condizioni_tipo_costo")) Then
                            dropPerc.SelectedValue = Session("richiama_condizioni_tipo_costo")
                        Else
                            dropPerc.SelectedIndex = -1
                        End If


                        If Session("richiama_condizioni_applicabilita_da") = "0" And Session("richiama_condizioni_applicabilita_a") = "0" Then
                            txtApplicabilitaDa.Text = ""
                            txtApplicabilitaA.Text = ""
                            chkSenzaLimite.Checked = True
                        Else
                            txtApplicabilitaDa.Text = Session("richiama_condizioni_applicabilita_da")
                            txtApplicabilitaA.Text = Session("richiama_condizioni_applicabilita_a")
                            chkSenzaLimite.Checked = False
                        End If

                        Dim gruppi As String = Session("richiama_condizioni_gruppi")
                        If gruppi <> "" Then
                            gruppi = gruppi.Replace("(", "")
                            gruppi = gruppi.Replace(" ", "")


                            ListBoxGruppiAuto.DataBind() 'carica valori su drop gruppi

                            Dim aGruppi() As String = Split(gruppi, "-")

                            For Each item As ListItem In ListBoxGruppiAuto.Items
                                For xg = 0 To UBound(aGruppi)
                                    If item.Value = aGruppi(xg) Then
                                        'item.Attributes.Add("selected", "selected")
                                        item.Selected = True

                                    End If
                                Next
                            Next

                        End If

                        lbl_id_riga.Text = Session("richiama_condizioni_id_riga")



                        '@end recupero valori da session  salvo 03.05.2023

                        If dropTemplateVal.SelectedValue = "0" Then
                            dropTemplateVal.Enabled = True
                            btnModificaVAL.Visible = False
                        Else
                            dropTemplateVal.Enabled = False
                            btnModificaVAL.Visible = True
                        End If

                        btnModificaVAL.Visible = True


                        sqlCondizioni.SelectCommand = lblQuery.Text
                        sqlCondizioni.DataBind()

                        lbl_tipo_op.Text = "modifica"
                        btnAnnulla.Text = "Torna alla lista"


                        '@ aggiunto salvo 04.05.2023
                        If Session("richiama_condizioni_status") = "Modifica e salva" Then
                            btnAggiungi.Text = "Modifica e salva"
                        Else
                            btnAggiungi.Text = "Aggiungi e salva"       'già presente
                        End If


                        btnSalva.Visible = False
                        btnModificaIntestazione.Visible = True

                        dropTemplateVal.Items.Clear()
                        dropTemplateVal.Items.Add("Seleziona...")
                        dropTemplateVal.Items(0).Value = "0"
                        dropTemplateVal.DataBind()


                        dropElementoVal.Items.Clear()
                        dropElementoVal.Items.Add("Seleziona...")
                        dropElementoVal.Items(0).Value = "0"
                        dropElementoVal.DataBind()

                        tab_vedi.Visible = True
                        tab_cerca.Visible = False

                        '# stringa verifica test salvo 05.05.2023
                        Dim tInsert As String = ""
                        tInsert = "id_riga='" & Session("richiama_condizioni_id_riga") & "' "
                        tInsert += ",id_condizione=" & lbl_id_codice.Text & "' "
                    lbl_righe_visualizzazione.Text = "Insert into condizioni_righe_costi_periodi set " & tInsert

                    '@end salvo







                Else

                        Session("elimina_condizione") = ""

                    End If

                Else

                    sqlCondizioni.SelectCommand = lblQuery.Text
                sqlCondizioni.DataBind()

                'svuota i campi ?
                If Session("elimina_condizione") = "elimina_riga" Then
                    'reset campi  aggiunto 04.05.2023
                    Session("richiama_condizioni_id_riga") = ""
                    btnAggiungi.Text = "Aggiungi e salva"
                    lbl_id_riga.Text = ""
                    dropElementi.SelectedIndex = -1
                    dropElementoVal.SelectedIndex = -1
                    dropTipoStampa.SelectedIndex = -1
                    txtApplicabilitaDa.Text = ""
                    txtApplicabilitaA.Text = ""
                    txtCosto.Text = ""
                    chkObbligatorio.Checked = False
                    chkPacked.Checked = False
                    chkSenzaLimite.Checked = False
                    txt_periodo_da.Text = ""
                    txt_periodo_a.Text = ""
                    dropACaricoDi.SelectedIndex = -1
                    dropUnitaDiMisura.SelectedIndex = -1
                    dropPerc.SelectedIndex = -1

                End If


            End If

            'aggiunto salvo 04.05.2023
            If Request.Cookies("SicilyRentCar")("idutente") = "5" Then
                lbl_id_riga.Visible = True
                table_salvo.Visible = True
                lbl_righe_visualizzazione.Visible = True
            Else
                lbl_id_riga.Visible = False
                table_salvo.Visible = False
                lbl_righe_visualizzazione.Visible = False
            End If

            ''se richiamata dopo eliminazione reset campi
            'If Session("elimina_costoperiodo_riga") = "si" Then

            '    btnAggiungi.Text = "Aggiungi e salva"
            '    lbl_id_riga.Text = ""
            '    dropElementi.SelectedValue = "0"
            '    dropElementoVal.SelectedIndex = "0"
            '    dropTipoStampa.SelectedIndex = "0"
            '    txtApplicabilitaDa.Text = ""
            '    txtApplicabilitaA.Text = ""
            '    txtCosto.Text = ""
            '    chkObbligatorio.Checked = False
            '    chkPacked.Checked = False
            '    chkSenzaLimite.Checked = False
            '    txt_periodo_da.Text = ""
            '    txt_periodo_a.Text = ""
            '    dropACaricoDi.SelectedValue = "0"
            '    dropUnitaDiMisura.SelectedValue = "0"
            '    'dropPerc.SelectedValue = "0"

            '    Session("elimina_costoperiodo_riga") = ""

            'End If





        Catch ex As Exception
            Response.Write("error vedi_riga : " & ex.Message & "<br/>")
        End Try

    End Sub

    Protected Sub listCondizioni_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listCondizioni.ItemCommand


        If e.CommandName = "vedi" Then

            Dim id_condzione As Label = e.Item.FindControl("idLabel")
            Dim valido_da As Label = e.Item.FindControl("valido_daLabel")
            Dim valido_a As Label = e.Item.FindControl("valido_aLabel")
            Dim nome_tempo_km As Label = e.Item.FindControl("codiceLabel")
            Dim id_template_val As Label = e.Item.FindControl("id_template_val")
            Dim iva_inclusa As Label = e.Item.FindControl("iva_inclusa")

            If iva_inclusa.Text = "True" Then
                dropIva.SelectedValue = "1"
            ElseIf iva_inclusa.Text = "False" Then
                dropIva.SelectedValue = "0"
            End If

            lbl_tipo_op.Text = "modifica"
            btnAnnulla.Text = "Torna alla lista"
            btnAggiungi.Text = "Aggiungi e salva"

            btnSalva.Visible = False
            btnModificaIntestazione.Visible = True

            lbl_id_codice.Text = id_condzione.Text
            lblCodice.Text = nome_tempo_km.Text
            txtValidoDa.Text = valido_da.Text
            txtValidoA.Text = valido_a.Text

            dropTemplateVal.Items.Clear()
            dropTemplateVal.Items.Add("Seleziona...")
            dropTemplateVal.Items(0).Value = "0"
            dropTemplateVal.DataBind()

            If id_template_val.Text <> "" Then
                dropTemplateVal.SelectedValue = id_template_val.Text
                dropTemplateVal.Enabled = False
                btnModificaVAL.Visible = True
            Else
                dropTemplateVal.SelectedValue = "0"
                dropTemplateVal.Enabled = True
                btnModificaVAL.Visible = False
            End If

            dropElementoVal.Items.Clear()
            dropElementoVal.Items.Add("Seleziona...")
            dropElementoVal.Items(0).Value = "0"
            dropElementoVal.DataBind()

            tab_vedi.Visible = True
            tab_cerca.Visible = False


        ElseIf e.CommandName = "modifica" Then

            Dim id_condizione As Label = e.Item.FindControl("idLabel")
            Dim valido_da As TextBox = e.Item.FindControl("valido_daText")
            Dim valido_a As TextBox = e.Item.FindControl("valido_aText")
            Dim nome_condizione As TextBox = e.Item.FindControl("codiceText")

            If check_condizione(nome_condizione.Text, valido_da.Text, valido_a.Text, id_condizione.Text) Then
                modifica_condizione(id_condizione.Text, valido_da.Text, valido_a.Text, nome_condizione.Text)
                Libreria.genUserMsgBox(Me, "Modifica effettuata correttamente.")
            Else
                Libreria.genUserMsgBox(Me, "Condizione già esistente: modifica non effettuata.")
            End If

            listCondizioni.DataBind()

        ElseIf e.CommandName = "elimina" Then
            Dim id_condizione As Label = e.Item.FindControl("idLabel")

            If possibile_eliminare_condizione(id_condizione.Text) Then
                elimina_intera_condizione(id_condizione.Text)
                listCondizioni.DataBind()
            Else
                Libreria.genUserMsgBox(Me, "Impossibile eliminare la condizione in quanto utilizzata in una o più tariffe.")
            End If

        ElseIf e.CommandName = "duplica" Then
            Dim id_condizione As Label = e.Item.FindControl("idLabel")
            Dim id_template_val As Label = e.Item.FindControl("id_template_val")
            Dim iva_inclusa As Label = e.Item.FindControl("iva_inclusa")

            Dim id_condizione_new As String = duplica_intera_condizione(id_condizione.Text, id_template_val.Text, iva_inclusa.Text)

            Trace.Write("iva_inclusa.Text " & iva_inclusa.Text)
            Trace.Write("id_template_val.Text " & id_template_val.Text)
            If id_condizione_new <> "" Then

                If iva_inclusa.Text = "True" Then
                    dropIva.SelectedValue = "1"
                ElseIf iva_inclusa.Text = "False" Then
                    dropIva.SelectedValue = "0"
                End If

                lbl_tipo_op.Text = "nuova"
                btnAnnulla.Text = "Annulla"
                btnAggiungi.Text = "Aggiungi"

                btnSalva.Visible = True
                btnModificaIntestazione.Visible = False

                lbl_id_codice.Text = id_condizione_new
                lblCodice.Text = ""
                txtValidoDa.Text = ""
                txtValidoA.Text = ""

                dropTemplateVal.Items.Clear()
                dropTemplateVal.Items.Add("Seleziona...")
                dropTemplateVal.Items(0).Value = "0"
                dropTemplateVal.DataBind()

                If id_template_val.Text <> "" Then
                    dropTemplateVal.SelectedValue = id_template_val.Text
                    dropTemplateVal.Enabled = False
                    btnModificaVAL.Visible = True
                Else
                    dropTemplateVal.SelectedValue = "0"
                    dropTemplateVal.Enabled = True
                    btnModificaVAL.Visible = False
                End If

                dropElementoVal.Items.Clear()
                dropElementoVal.Items.Add("Seleziona...")
                dropElementoVal.Items(0).Value = "0"
                dropElementoVal.DataBind()

                tab_vedi.Visible = True
                tab_cerca.Visible = False

            Else
                Libreria.genUserMsgBox(Me, "E' avvenuto qualche errore nella duplicazione delle condizioni.")
            End If
        End If
    End Sub


    Protected Function possibile_eliminare_condizione(ByVal id_condizione As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM tariffe_righe WITH(NOLOCK) WHERE id_condizione='" & id_condizione & "' OR id_condizione_madre='" & id_condizione & "'", Dbc)

        Dim test As String = Cmd.ExecuteScalar

        If test = "" Then
            possibile_eliminare_condizione = True
        Else
            possibile_eliminare_condizione = False
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub elimina_intera_condizione(ByVal id_condizione As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Dbc1 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc1.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM condizioni_righe WITH(NOLOCK) WHERE id_condizione='" & id_condizione & "'", Dbc)
        Dim Cmd1 As New Data.SqlClient.SqlCommand("", Dbc1)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Do While Rs.Read()
            Cmd1 = New Data.SqlClient.SqlCommand("DELETE FROM condizioni_x_gruppi WHERE id_condizione='" & Rs("id") & "'", Dbc1)
            Cmd1.ExecuteNonQuery()
        Loop

        Cmd1 = New Data.SqlClient.SqlCommand("DELETE FROM condizioni_righe WHERE id_condizione='" & id_condizione & "'", Dbc1)
        Cmd1.ExecuteNonQuery()
        Cmd1 = New Data.SqlClient.SqlCommand("DELETE FROM condizioni WHERE id='" & id_condizione & "'", Dbc1)
        Cmd1.ExecuteNonQuery()

        Cmd1.Dispose()
        Cmd1 = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
        Dbc1.Close()
        Dbc1.Dispose()
        Dbc1 = Nothing
    End Sub

    Protected Function duplica_intera_condizione(ByVal id_condizione As String, ByVal id_template_val As String, ByVal iva_inclusa As String) As String

        duplica_intera_condizione = ""

        Dim sqlStr As String = "INSERT [condizioni] ([data_creazione],[attivo],[id_template_val],[iva_inclusa])" & _
            " VALUES (GETDATE(),0,"
        If id_template_val = "" Then
            sqlStr = sqlStr & " NULL,"
        Else
            sqlStr = sqlStr & "'" & id_template_val & "',"
        End If
        If iva_inclusa = "" Then
            sqlStr = sqlStr & " NULL)"
        Else
            sqlStr = sqlStr & "'" & iva_inclusa & "')"
        End If

        Trace.Write(sqlStr)

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Using Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()
        End Using

        sqlStr = "SELECT @@Identity FROM condizioni WITH(NOLOCK)"
        Trace.Write(sqlStr)

        Dim id_condizione_new As String

        Using Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            id_condizione_new = Cmd.ExecuteScalar
        End Using

        duplica_intera_condizione = id_condizione_new

        sqlStr = "SELECT [id] FROM condizioni_righe WITH(NOLOCK) WHERE [id_condizione] = '" & id_condizione & "'"
        Trace.Write(sqlStr)

        Using Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Using Rs As Data.SqlClient.SqlDataReader = Cmd.ExecuteReader()
                Using Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc2.Open()

                    Do While Rs.Read()
                        sqlStr = "INSERT [condizioni_righe] ([data_creazione],[id_condizione],[num_riga],[applicabilita_da],[applicabilita_a],[id_elemento],[id_a_carico_di],[pac],[id_unita_misura],[costo],[tipo_costo],[id_elemento_val],[obbligatorio],[id_metodo_stampa])" & _
                            " SELECT GETDATE(),'" & id_condizione_new & "', [num_riga],[applicabilita_da],[applicabilita_a],[id_elemento],[id_a_carico_di],[pac],[id_unita_misura],[costo],[tipo_costo],[id_elemento_val],[obbligatorio],[id_metodo_stampa]" & _
                            " FROM condizioni_righe WITH(NOLOCK) WHERE [id] = '" & Rs("id") & "'"
                        Trace.Write(sqlStr)

                        Using Cmd2 As New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                            Cmd2.ExecuteNonQuery()
                        End Using

                        sqlStr = "SELECT @@Identity FROM condizioni_righe WITH(NOLOCK)"
                        Trace.Write(sqlStr)

                        Dim id_condizione_riga As String
                        Using Cmd2 As New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                            id_condizione_riga = Cmd2.ExecuteScalar
                        End Using

                        sqlStr = "INSERT [condizioni_x_gruppi] ([id_condizione],[id_gruppo])" & _
                            " SELECT '" & id_condizione_riga & "',[id_gruppo] FROM condizioni_x_gruppi WITH(NOLOCK) " & _
                            " WHERE [id_condizione] = '" & Rs("id") & "'"
                        Trace.Write(sqlStr)

                        Using Cmd2 As New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                            Cmd2.ExecuteNonQuery()
                        End Using
                    Loop
                End Using
            End Using
        End Using

        Trace.Write("Fine copia dati")

        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub modifica_condizione(ByVal id_condizione As String, ByVal valido_da As String, ByVal valido_a As String, ByVal codice As String)

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            valido_da = funzioni_comuni.getDataDb_senza_orario(valido_da)
            valido_a = funzioni_comuni.getDataDb_senza_orario(valido_a)

            Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE condizioni SET codice='" & Replace(codice, "'", "''") & "', valido_da=convert(datetime,'" & valido_da & "',102), valido_a=convert(datetime,'" & valido_a & "',102), iva_inclusa='" & dropIva.SelectedValue & "' WHERE id='" & id_condizione & "'", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("error  modifica_condizione : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Function check_condizione(ByVal nome As String, ByVal validoDa As String, ByVal validoA As String, ByVal id_condizione As String) As Boolean
        'CONTROLLA SE IL TEMPO-KM E' UNIVOCO. SE ID_CODICE E' VALORIZZATO SIAMO IN FASE DI MODIFICA PER CUI CONTROLLO PER I TEMPO_KM
        'DIVERSI DA QUELLO ATTUALE. SE ID_CODICE E' VUOTO VUOL DIRE CHE E' UN NUOVO INSERIMENTO
        Dim sqla As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            validoDa = funzioni_comuni.getDataDb_senza_orario(validoDa)
            validoA = funzioni_comuni.getDataDb_senza_orario(validoA)
            sqla = "SELECT TOP 1 id FROM condizioni WITH(NOLOCK) WHERE codice='" & Replace(nome, "'", "''") & "' AND valido_da=convert(datetime,'" & validoDa & "',102) AND valido_a=convert(datetime,'" & validoA & "',102) AND attivo='1' AND id<>'" & id_condizione & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test = "" Then
                check_condizione = True
            Else
                check_condizione = False
            End If

            listCondizioni.DataBind()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error check_condizione  : <br/>" & ex.Message & "<br/>" & "" & "<br/>")
        End Try

    End Function

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click
        If lbl_tipo_op.Text = "nuova" Then
            elimina_intera_condizione(lbl_id_codice.Text)
        End If


        listCondizioni.DataBind()

        If btnAnnulla.Text = "Annulla" Then
            'CASO DI NUOVO INSERIMENTO: ELIMINO DAL DB LE RIGHE CREATE
        End If

        chkSenzaLimite.Checked = False

        txtApplicabilitaA.ReadOnly = False
        txtApplicabilitaDa.ReadOnly = False

        btnModificaIntestazione.Visible = False

        txtApplicabilitaA.Text = ""
        txtApplicabilitaDa.Text = ""

        lbl_tipo_op.Text = ""

        lbl_id_codice.Text = ""
        lblCodice.Text = ""

        txtValidoA.Text = ""
        txtValidoDa.Text = ""

        tab_vedi.Visible = False
        tab_cerca.Visible = True
    End Sub

    Protected Function getGruppi(ByVal id_condizione As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT gruppi.cod_gruppo As gruppo FROM condizioni_x_gruppi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON condizioni_x_gruppi.id_gruppo=gruppi.id_gruppo WHERE condizioni_x_gruppi.id_condizione='" & id_condizione & "'", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        getGruppi = ""

        Do While Rs.Read()
            getGruppi = getGruppi & Rs("gruppo") & " - "
        Loop

        If Len(getGruppi) > 0 Then
            getGruppi = "(" & Left(getGruppi, Len(getGruppi) - 2) & ")"
        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function







    Protected Function getGruppiID(ByVal id_condizione As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT gruppi.id_gruppo As gruppo FROM condizioni_x_gruppi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON condizioni_x_gruppi.id_gruppo=gruppi.id_gruppo WHERE condizioni_x_gruppi.id_condizione='" & id_condizione & "'", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Dim ris As String = ""

        Do While Rs.Read()
            If ris = "" Then
                ris = Rs("gruppo")
            Else
                ris += "-" & Rs("gruppo")
            End If
        Loop


        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        Return ris



    End Function
    Protected Function get_id_condizione(ByVal idCondizione As String) As String()
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM condizioni_righe WITH(NOLOCK) WHERE id_condizione='" & idCondizione & "'", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Dim array_id(800) As String

        Dim i As Integer = 1

        Do While Rs.Read()
            array_id(i) = Rs("id")
            i = i + 1
        Loop
        array_id(i) = "000"
        get_id_condizione = array_id

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub elimina_riga(ByVal id_riga As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT num_riga FROM condizioni_righe WITH(NOLOCK) WHERE id='" & id_riga & "'", Dbc)
        Dim num_riga As String = Cmd.ExecuteScalar

        Cmd = New Data.SqlClient.SqlCommand("SELECT count(num_riga) FROM condizioni_righe WITH(NOLOCK) WHERE num_riga='" & num_riga & "' AND id_condizione='" & lbl_id_codice.Text & "'", Dbc)
        Dim num_righe As Integer = Cmd.ExecuteScalar

        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM condizioni_x_gruppi WHERE id_condizione='" & id_riga & "'", Dbc)
        Cmd.ExecuteNonQuery()
        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM condizioni_righe WHERE id='" & id_riga & "'", Dbc)
        Cmd.ExecuteNonQuery()

        'SE NON VI SONO ALTRE RIGHE PER L'ID CONDIZIONE CORRENTE DIMINUISCO IL CAMPO num_riga PER LE RIGHE SUCCESSIVE

        If num_righe = 1 Then
            Cmd = New Data.SqlClient.SqlCommand("UPDATE condizioni_righe SET num_riga=num_riga-1 WHERE id_condizione='" & lbl_id_codice.Text & "' AND num_riga >" & num_riga, Dbc)
            Cmd.ExecuteNonQuery()
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        Session("elimina_condizione") = "elimina_riga"






    End Sub

    Protected Sub vedi_riga_rec(ByVal id_riga As String, idrec As String)
        'aggiunto salvo 12.07.2023

        Dim sqlstr As String = ""

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            sqlstr = "SELECT * FROM condizioni_righe WITH(NOLOCK) WHERE id='" & id_riga & "'" ' and id='" & idrec & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim num_riga As String = "" '"Cmd.ExecuteScalar
            'Response.Write("Vedi Riga: " & id_riga & "<br/>")

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Dim id_elemento As String = ""
            Dim t As String = ""

            If Rs.HasRows() Then
                Rs.Read()
                t = "nriga=" & Rs!id
                t = "&idele=" & Rs!id_elemento
                t += "&idco=" & Rs!id_condizione

                Session("richiama_condizioni_id_riga") = (Rs!id).ToString
                Session("richiama_condizioni_drop_id_elemento") = Rs!id_elemento
                Session("richiama_condizioni_id_condizione") = Rs!id_condizione
                Session("richiama_condizioni_drop_id_elemento_val") = Rs!id_elemento_val
                Session("richiama_condizioni_drop_id_metodo_stampa") = Rs!id_metodo_stampa
                Session("richiama_condizioni_drop_A_carico_di") = Rs!id_a_carico_di
                Session("richiama_condizioni_ckPacked") = Rs!pac
                Session("richiama_condizioni_ckObbligatorio") = Rs!obbligatorio
                Session("richiama_condizioni_drop_unita_misura") = Rs!id_unita_misura
                Session("richiama_condizioni_costo") = Rs!costo
                Session("richiama_condizioni_tipo_costo") = Rs!tipo_costo
                Session("richiama_condizioni_applicabilita_da") = Rs!applicabilita_da
                Session("richiama_condizioni_applicabilita_a") = Rs!applicabilita_a
                Session("richiama_condizioni_periodo_da") = Rs!periodo_da
                Session("richiama_condizioni_periodo_a") = Rs!periodo_a

            Else

            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            t += "&idcod2=" & lbl_id_codice.Text 'come idco sopra
            t += "&idcod3=" & lblCodice.Text    'etichetta x esteso 
            t += "&valda=" & txtValidoDa.Text 'campo validitàda
            t += "&vala=" & txtValidoA.Text 'campo validità a

            'valorizza sessione madre per valori salvo 03.05.2023
            Session("richiama_condizioni_idcodice") = lbl_id_codice.Text
            Session("richiama_condizioni_codice") = lblCodice.Text
            Session("richiama_condizioni_val_da") = txtValidoDa.Text
            Session("richiama_condizioni_val_a") = txtValidoA.Text
            Session("richiama_condizioni_id_template_val") = dropTemplateVal.SelectedValue
            Session("richiama_condizioni_template_val") = dropTemplateVal.SelectedValue
            Session("richiama_condizioni_iva_inclusa") = dropIva.SelectedValue

            Session("richiama_condizioni_gruppi") = getGruppiID(id_riga)

            Session("richiama_condizioni_status") = "Modifica e salva"

            Response.Redirect("lista_condizioni.aspx") '?idriga=" & id_riga & t)

            Cmd = New Data.SqlClient.SqlCommand("SELECT count(num_riga) FROM condizioni_righe WITH(NOLOCK) WHERE num_riga='" & num_riga & "' AND id_condizione='" & lbl_id_codice.Text & "'", Dbc)
            Dim num_righe As Integer = Cmd.ExecuteScalar

            'Cmd = New Data.SqlClient.SqlCommand("DELETE FROM condizioni_x_gruppi WHERE id_condizione='" & id_riga & "'", Dbc)
            'Cmd.ExecuteNonQuery()
            'Cmd = New Data.SqlClient.SqlCommand("DELETE FROM condizioni_righe WHERE id='" & id_riga & "'", Dbc)
            'Cmd.ExecuteNonQuery()

            'SE NON VI SONO ALTRE RIGHE PER L'ID CONDIZIONE CORRENTE DIMINUISCO IL CAMPO num_riga PER LE RIGHE SUCCESSIVE

            'If num_righe = 1 Then
            'Cmd = New Data.SqlClient.SqlCommand("UPDATE condizioni_righe SET num_riga=num_riga-1 WHERE id_condizione='" & lbl_id_codice.Text & "' AND num_riga >" & num_riga, Dbc)
            'Cmd.ExecuteNonQuery()
            'End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("Errore: Vedi Riga: " & id_riga & " " & ex.Message)
            'funzioni_comuni.genUserMsgBox(Page, "Errore: Vedi Riga: " & id_riga & " " & ex.Message)
        End Try


    End Sub

    Protected Sub vedi_riga(ByVal id_riga As String, idrec As String)
        'aggiunto salvo 28.04.2023

        Dim sqlstr As String = ""

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            sqlstr = "SELECT * FROM condizioni_righe WITH(NOLOCK) WHERE id='" & id_riga & "'" ' and id='" & idrec & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim num_riga As String = "" '"Cmd.ExecuteScalar
            'Response.Write("Vedi Riga: " & id_riga & "<br/>")

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Dim id_elemento As String = ""
            Dim t As String = ""

            If Rs.HasRows() Then
                Rs.Read()
                t = "nriga=" & Rs!id
                t = "&idele=" & Rs!id_elemento
                t += "&idco=" & Rs!id_condizione

                Session("richiama_condizioni_id_riga") = (Rs!id).ToString
                Session("richiama_condizioni_drop_id_elemento") = Rs!id_elemento
                Session("richiama_condizioni_id_condizione") = Rs!id_condizione
                Session("richiama_condizioni_drop_id_elemento_val") = Rs!id_elemento_val
                Session("richiama_condizioni_drop_id_metodo_stampa") = Rs!id_metodo_stampa
                Session("richiama_condizioni_drop_A_carico_di") = Rs!id_a_carico_di
                Session("richiama_condizioni_ckPacked") = Rs!pac
                Session("richiama_condizioni_ckObbligatorio") = Rs!obbligatorio
                Session("richiama_condizioni_drop_unita_misura") = Rs!id_unita_misura
                Session("richiama_condizioni_costo") = Rs!costo
                Session("richiama_condizioni_tipo_costo") = Rs!tipo_costo
                Session("richiama_condizioni_applicabilita_da") = Rs!applicabilita_da
                Session("richiama_condizioni_applicabilita_a") = Rs!applicabilita_a
                Session("richiama_condizioni_periodo_da") = Rs!periodo_da
                Session("richiama_condizioni_periodo_a") = Rs!periodo_a

            Else

            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            t += "&idcod2=" & lbl_id_codice.Text 'come idco sopra
            t += "&idcod3=" & lblCodice.Text    'etichetta x esteso 
            t += "&valda=" & txtValidoDa.Text 'campo validitàda
            t += "&vala=" & txtValidoA.Text 'campo validità a

            'valorizza sessione madre per valori salvo 03.05.2023
            Session("richiama_condizioni_idcodice") = lbl_id_codice.Text
            Session("richiama_condizioni_codice") = lblCodice.Text
            Session("richiama_condizioni_val_da") = txtValidoDa.Text
            Session("richiama_condizioni_val_a") = txtValidoA.Text
            Session("richiama_condizioni_id_template_val") = dropTemplateVal.SelectedValue
            Session("richiama_condizioni_template_val") = dropTemplateVal.SelectedValue
            Session("richiama_condizioni_iva_inclusa") = dropIva.SelectedValue

            Session("richiama_condizioni_gruppi") = getGruppiID(id_riga)

            Session("richiama_condizioni_status") = "Modifica e salva"

            Response.Redirect("lista_condizioni.aspx") '?idriga=" & id_riga & t)

            Cmd = New Data.SqlClient.SqlCommand("SELECT count(num_riga) FROM condizioni_righe WITH(NOLOCK) WHERE num_riga='" & num_riga & "' AND id_condizione='" & lbl_id_codice.Text & "'", Dbc)
            Dim num_righe As Integer = Cmd.ExecuteScalar

            'Cmd = New Data.SqlClient.SqlCommand("DELETE FROM condizioni_x_gruppi WHERE id_condizione='" & id_riga & "'", Dbc)
            'Cmd.ExecuteNonQuery()
            'Cmd = New Data.SqlClient.SqlCommand("DELETE FROM condizioni_righe WHERE id='" & id_riga & "'", Dbc)
            'Cmd.ExecuteNonQuery()

            'SE NON VI SONO ALTRE RIGHE PER L'ID CONDIZIONE CORRENTE DIMINUISCO IL CAMPO num_riga PER LE RIGHE SUCCESSIVE

            'If num_righe = 1 Then
            'Cmd = New Data.SqlClient.SqlCommand("UPDATE condizioni_righe SET num_riga=num_riga-1 WHERE id_condizione='" & lbl_id_codice.Text & "' AND num_riga >" & num_riga, Dbc)
            'Cmd.ExecuteNonQuery()
            'End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("Errore: Vedi Riga: " & id_riga & " " & ex.Message)
            'funzioni_comuni.genUserMsgBox(Page, "Errore: Vedi Riga: " & id_riga & " " & ex.Message)
        End Try


    End Sub



    Protected Sub elimina_condizione(ByVal num_riga As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Dbc1 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc1.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM condizioni_righe WITH(NOLOCK) WHERE id_condizione='" & lbl_id_codice.Text & "' AND num_riga='" & num_riga & "'", Dbc)
        Dim Cmd1 As New Data.SqlClient.SqlCommand("", Dbc1)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Do While Rs.Read()
            Cmd1 = New Data.SqlClient.SqlCommand("DELETE FROM condizioni_x_gruppi WHERE id_condizione='" & Rs("id") & "'", Dbc1)
            Cmd1.ExecuteNonQuery()
        Loop

        Cmd1 = New Data.SqlClient.SqlCommand("DELETE FROM condizioni_righe WHERE id_condizione='" & lbl_id_codice.Text & "' AND num_riga='" & num_riga & "'", Dbc1)
        Cmd1.ExecuteNonQuery()
        Cmd1 = New Data.SqlClient.SqlCommand("UPDATE condizioni_righe SET num_riga=num_riga-1 WHERE id_condizione='" & lbl_id_codice.Text & "' AND num_riga >" & num_riga, Dbc1)
        Cmd1.ExecuteNonQuery()

        Cmd1.Dispose()
        Cmd1 = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
        Dbc1.Close()
        Dbc1.Dispose()
        Dbc1 = Nothing

        Session("elimina_condizione") = "elimina_riga" 'aggiunto salvo 04.05.2023



    End Sub

    Protected Function getRigaSuccessiva(ByVal idCondizione As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(MAX(num_riga),0) FROM condizioni_righe WITH(NOLOCK) WHERE id_condizione='" & idCondizione & "'", Dbc)

        getRigaSuccessiva = Cmd.ExecuteScalar + 1

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function stessa_unita_di_misura() As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim id_unita_misura As String
        Dim sqlStr As String

        If dropElementi.SelectedValue <> "0" Then
            sqlStr = "SELECT TOP 1 id_unita_misura FROM condizioni_righe WITH(NOLOCK) WHERE id_condizione='" & lbl_id_codice.Text & "' AND id_elemento='" & dropElementi.SelectedValue & "'"
        ElseIf dropElementoVal.SelectedValue <> "0" Then
            sqlStr = "SELECT TOP 1 id_unita_misura FROM condizioni_righe WITH(NOLOCK) WHERE id_condizione='" & lbl_id_codice.Text & "' AND id_elemento_val='" & dropElementoVal.SelectedValue & "'"
        End If

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        id_unita_misura = Cmd.ExecuteScalar

        If id_unita_misura = "" Then
            'PRIMO INSERIMENTO PER QUESTO ELEMENTO
            stessa_unita_di_misura = True
        ElseIf id_unita_misura = dropUnitaDiMisura.SelectedValue Then
            stessa_unita_di_misura = True
        Else
            stessa_unita_di_misura = False
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub salvaRiga(ByVal num_gruppi As Integer)
        Dim sqlStr As String
        Dim id_elemento As String
        Dim id_elemento_val As String

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim num_riga As String
            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

            If dropElementi.SelectedValue <> "0" Then
                sqlStr = "SELECT TOP 1 num_riga FROM condizioni_righe WITH(NOLOCK) WHERE id_condizione='" & lbl_id_codice.Text & "' AND id_elemento='" & dropElementi.SelectedValue & "'"
                id_elemento = "'" & dropElementi.SelectedValue & "'"
                id_elemento_val = "NULL"
            ElseIf dropElementoVal.SelectedValue <> "0" Then
                sqlStr = "SELECT TOP 1 num_riga FROM condizioni_righe WITH(NOLOCK) WHERE id_condizione='" & lbl_id_codice.Text & "' AND id_elemento_val='" & dropElementoVal.SelectedValue & "'"
                id_elemento = "NULL"
                id_elemento_val = "'" & dropElementoVal.SelectedValue & "'"
            End If

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            num_riga = Cmd.ExecuteScalar


            If num_riga = "" Then
                num_riga = getRigaSuccessiva(lbl_id_codice.Text)
            End If

            Dim perc As String
            If txtCosto.Text <> "" Then
                perc = dropPerc.SelectedValue
            Else
                perc = ""
            End If

            Dim obbligatorio As String = chkObbligatorio.Checked
            If chkObbligatorio.Checked = True Then
                obbligatorio = "1"
            Else
                obbligatorio = "0"
            End If

            'GLI ELEMENTI INCLUSI E LE INFORMATIVE DEVONO NECESSARIAMENTE ESSERE OBBLIGATORI
            If dropACaricoDi.SelectedValue = "5" Or dropTipoStampa.SelectedValue = "3" Or dropTipoStampa.SelectedValue = "4" Then
                obbligatorio = "1"
            End If


            '#verifica se modifica o nuovo inserimento salvo 04.05.2023
            Dim id_riga As String = ""
            Dim sqlUpd As String = ""
            Dim tblIns As String = "condizioni_righe"

            If btnAggiungi.Text = "Modifica e salva" Then

                id_riga = lbl_id_riga.Text
                Session("richiama_condizioni_id_riga") = id_riga

                sqlUpd = "UPDATE condizioni_righe SET "
                sqlUpd += "id_a_carico_di='" & dropACaricoDi.SelectedValue & "'"


                If chkSenzaLimite.Checked = True Then
                    sqlUpd += ", applicabilita_da='0'"
                    sqlUpd += ", applicabilita_a='0'"
                Else
                    sqlUpd += ", applicabilita_da='" & Trim(txtApplicabilitaDa.Text) & "'"
                    sqlUpd += ", applicabilita_a='" & Trim(txtApplicabilitaA.Text) & "'"
                End If

                If chkPacked.Checked = True Then
                    sqlUpd += ", pac='1'"
                Else
                    sqlUpd += ", pac='0'"
                End If


                sqlUpd += ", id_unita_misura='" & dropUnitaDiMisura.SelectedValue & "'"


                If dropACaricoDi.SelectedValue = "5" Then 'se incluso valore null non può avere costo 05.05.2023
                    sqlUpd += ", costo=NULL"
                Else
                    sqlUpd += ", costo='" & Replace(txtCosto.Text, ",", ".") & "'"
                End If


                sqlUpd += ", tipo_costo='" & perc & "'"

                If id_elemento_val = "" Or IsDBNull(id_elemento_val) Or id_elemento_val = "NULL" Then
                    sqlUpd += ", id_elemento_val=NULL"
                Else
                    sqlUpd += ", id_elemento_val=" & id_elemento_val & " "
                End If

                sqlUpd += ", obbligatorio='" & obbligatorio & "'"

                sqlUpd += ", id_metodo_stampa='" & dropTipoStampa.SelectedValue & "' "

                If txt_periodo_da.Text <> "" Then 'aggiunto salvo 05.05.2023
                    Dim periododa As String = CDate(txt_periodo_da.Text).Year.ToString & "-" & CDate(txt_periodo_da.Text).Month.ToString & "-" & CDate(txt_periodo_da.Text).Day.ToString & " 00:00:00"
                    'Dim periodoa As String = CDate(txt_periodo_a.Text).Year.ToString & "-" & CDate(txt_periodo_a.Text).Month.ToString & "-" & CDate(txt_periodo_a.Text).Day.ToString & " 23:59:59"
                    sqlUpd += ", periodo_da=convert(datetime,'" & periododa & "',102) "
                    'sqlUpd += ", periodo_a=convert(datetime,'" & periodoa & "',102) "
                Else
                    sqlUpd += ", periodo_da=NULL"
                    'sqlUpd += ", periodo_a=NULL "
                End If

                sqlUpd += "WHERE id= '" & id_riga & "'"

                tblIns = "condizioni_righe_costi_periodi"       'serve per inserimento su altra tabella

            End If 'se Modifica e salva

            'Else
            'altrimenti inserisce
            'aggiunto il 19.01.2021

            'Valorizza la stringa di inserimento che se 'Modifica e salva'
            'aggiunge nella tabella condizioni_righe_costi_periodi

            If chkSenzaLimite.Checked = False Then

                sqlStr = "INSERT INTO " & tblIns & " (data_creazione, id_condizione,num_riga, id_elemento, id_a_carico_di, applicabilita_da, applicabilita_a, pac, id_unita_misura, costo, tipo_costo"
                sqlStr += ",id_elemento_val,obbligatorio,id_metodo_stampa,periodo_da" 'aggiunto salvo 05.05.2023

                If btnAggiungi.Text = "Modifica e salva" Then
                    sqlStr += ",id_riga"
                End If

                sqlStr += ")VALUES("
                sqlStr += "convert(datetime, GetDate(), 102), '" & lbl_id_codice.Text & "','" & num_riga & "'," & id_elemento
                sqlStr += ", '" & dropACaricoDi.SelectedValue & "','" & Trim(txtApplicabilitaDa.Text) & "','" & Trim(txtApplicabilitaA.Text) & "'"
                sqlStr += ", '" & chkPacked.Checked & "','" & dropUnitaDiMisura.SelectedValue & "','" & Replace(txtCosto.Text, ",", ".") & "'"
                sqlStr += ",'" & perc & "'," & id_elemento_val & ",'" & obbligatorio & "','" & dropTipoStampa.SelectedValue & "'"
                If txt_periodo_da.Text <> "" Then 'aggiunto salvo 05.05.2023
                    Dim periododa As String = CDate(txt_periodo_da.Text).Year.ToString & "-" & CDate(txt_periodo_da.Text).Month.ToString & "-" & CDate(txt_periodo_da.Text).Day.ToString & " 00:00:00"
                    'Dim periodoa As String = CDate(txt_periodo_a.Text).Year.ToString & "-" & CDate(txt_periodo_a.Text).Month.ToString & "-" & CDate(txt_periodo_a.Text).Day.ToString & " 23:59:59"
                    sqlStr += ", convert(datetime,'" & periododa & "',102) "
                    'sqlStr += ", convert(datetime,'" & periodoa & "',102) "
                Else
                    sqlStr += ", NULL"
                    'sqlStr += ", NULL "
                End If

                If btnAggiungi.Text = "Modifica e salva" Then
                    sqlStr += ",'" & id_riga & "'"
                End If

                sqlStr += ")"

            Else

                sqlStr = "INSERT INTO " & tblIns & " (data_creazione, id_condizione,num_riga, id_elemento, id_a_carico_di, applicabilita_da, applicabilita_a, pac, id_unita_misura, costo, tipo_costo"
                sqlStr += ",id_elemento_val,obbligatorio,id_metodo_stampa,periodo_da" 'aggiunto salvo 05.05.2023
                If btnAggiungi.Text = "Modifica e salva" Then
                    sqlStr += ",id_riga"
                End If
                sqlStr += ") VALUES ("
                sqlStr += "convert(datetime,GetDate(),102), '" & lbl_id_codice.Text & "','" & num_riga & "'," & id_elemento
                sqlStr += ", '" & dropACaricoDi.SelectedValue & "','0','0'"
                sqlStr += ", '" & chkPacked.Checked & "','" & dropUnitaDiMisura.SelectedValue & "','" & Replace(txtCosto.Text, ",", ".") & "'"
                sqlStr += ",'" & perc & "'," & id_elemento_val & ",'" & obbligatorio & "','" & dropTipoStampa.SelectedValue & "'"
                If txt_periodo_da.Text <> "" Then 'aggiunto salvo 05.05.2023
                    Dim periododa As String = CDate(txt_periodo_da.Text).Year.ToString & "-" & CDate(txt_periodo_da.Text).Month.ToString & "-" & CDate(txt_periodo_da.Text).Day.ToString & " 00:00:00"
                    'Dim periodoa As String = CDate(txt_periodo_a.Text).Year.ToString & "-" & CDate(txt_periodo_a.Text).Month.ToString & "-" & CDate(txt_periodo_a.Text).Day.ToString & " 23:59:59"
                    sqlStr += ", convert(datetime,'" & periododa & "',102) "
                    'sqlStr += ", convert(datetime,'" & periodoa & "',102) "
                Else
                    sqlStr += ", NULL"
                    'sqlStr += ", NULL "
                End If

                If btnAggiungi.Text = "Modifica e salva" Then
                    sqlStr += ",'" & id_riga & "'"
                End If

                sqlStr += ")"

            End If
            'End If
            '@end salvo

            Dim insris As Integer = 0
            If btnAggiungi.Text = "Modifica e salva" Then
                'modifica e aggiornamento
                Cmd = New Data.SqlClient.SqlCommand(sqlUpd, Dbc)
                insris = Cmd.ExecuteNonQuery()
            End If

            'nuovo inserimento
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            insris = Cmd.ExecuteNonQuery()

            '# aggiunto salvo 04.05.2023
            If btnAggiungi.Text = "Modifica e salva" Then
                'se modifica

                If num_gruppi > 0 Then
                    For i = 0 To ListBoxGruppiAuto.Items.Count - 1
                        If ListBoxGruppiAuto.Items(i).Selected Then
                            Cmd = New Data.SqlClient.SqlCommand("INSERT INTO condizioni_x_gruppi (id_condizione,id_gruppo) VALUES ('" & id_riga & "','" & ListBoxGruppiAuto.Items(i).Value & "')", Dbc)
                            Cmd.ExecuteNonQuery()
                        End If
                    Next
                Else
                    Cmd = New Data.SqlClient.SqlCommand("DELETE condizioni_x_gruppi where id_condizione='" & id_riga & "'", Dbc)
                    Dim delr As Integer = Cmd.ExecuteNonQuery()
                End If


                '#salvo 12.07.2023 deve cambiare il valore del costo sulla riga e la data periodo
                'i valori iniziali della prima colonna sono riferiti alla data del 01.01.2023
                'se la modifica viene effettuata nel periodo compreso dal campo txt_periodo_da
                'Dim ddiff As Integer = DateDiff("d", Now, CDate(txt_periodo_da.Text))
                'If ddiff <= 0 Then 'aggiorna costo
                '    Dim sqlu As String = "update condizioni_righe set costo='" & Replace(txtCosto.Text, ",", ".") & "' "
                '    sqlu += ",periodo_da=convert(datetime,'" & Year(txt_periodo_da.Text) & "-" & Month(txt_periodo_da.Text) & "-" & Day(txt_periodo_da.Text) & " 00:00:00',102) "
                '    sqlu += "where id='" & id_riga & "'"
                '    Cmd = New Data.SqlClient.SqlCommand(sqlu, Dbc)
                '    Dim updr As Integer = Cmd.ExecuteNonQuery()
                'End If
                '@end salvo

                funzioni_comuni.genUserMsgBox(Page, "Modifica alla riga effettuata")

            Else

                'se inserimento
                If num_gruppi > 0 Then
                    'RECUPERO L'ID DELLA RIGA CREATA E SALVO IN condizioni_x_gruppi UNA RIGA PER OGNI GRUPPO AUTO
                    Cmd = New Data.SqlClient.SqlCommand("SELECT MAX(id) FROM condizioni_righe WITH(NOLOCK)", Dbc)
                    id_riga = Cmd.ExecuteScalar
                    If num_gruppi > 0 Then
                        For i = 0 To ListBoxGruppiAuto.Items.Count - 1
                            If ListBoxGruppiAuto.Items(i).Selected Then
                                Cmd = New Data.SqlClient.SqlCommand("INSERT INTO condizioni_x_gruppi (id_condizione,id_gruppo) VALUES ('" & id_riga & "','" & ListBoxGruppiAuto.Items(i).Value & "')", Dbc)
                                Cmd.ExecuteNonQuery()
                            End If
                        Next
                    End If
                End If

                funzioni_comuni.genUserMsgBox(Page, "Nuovo inserimento effettuato")

            End If
            '@ end salvo 

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            '# azzera session valorizzata per modifica/inserimento salvo 04.05.2023
            Session("richiama_condizioni_id_riga") = ""

            'reset campi 
            btnAggiungi.Text = "Aggiungi e salva"
            lbl_id_riga.Text = ""
            dropElementi.SelectedIndex = -1
            dropElementoVal.SelectedIndex = -1
            dropTipoStampa.SelectedIndex = -1
            txtApplicabilitaDa.Text = ""
            txtApplicabilitaA.Text = ""
            txtCosto.Text = ""
            chkObbligatorio.Checked = False
            chkPacked.Checked = False
            chkSenzaLimite.Checked = False
            txt_periodo_da.Text = ""
            txt_periodo_a.Text = ""
            dropACaricoDi.SelectedIndex = -1
            dropUnitaDiMisura.SelectedIndex = -1
            dropPerc.SelectedIndex = -1
            '@ salvo

        Catch ex As Exception
            HttpContext.Current.Response.Write("error salvaRiga  : <br/>" & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try

    End Sub

    Protected Function condizioneNonInserita() As Boolean
        'CONTROLLO SE LA CONDIZIONE E' STATA GIA' SALVATA, ALMENO IN UNA SUA PARTE.
        'QUINDI CONTROLLO SE E' GIA' ESISTENTE NEL CASO IN CUI NON SIANO STATI SPECIFICATI GRUPPI O NEL CASO IN CUI NON NE SIANO STATI
        'SPECIFICATI
        Dim sqlStr As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Dbc1 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc1.Open()


            Dim app_da As String = txtApplicabilitaDa.Text
            Dim app_a As String = txtApplicabilitaA.Text

            If app_da = "" Then
                app_da = "0"
            End If

            If app_a = "" Then
                app_a = "0"
            End If

            'AND id_a_carico_di='" & dropACaricoDi.SelectedValue & "' "
            If dropElementi.SelectedValue <> "0" Then
                sqlStr = "SELECT id FROM condizioni_righe WITH(NOLOCK) WHERE id_condizione='" & lbl_id_codice.Text & "' " &
                "AND applicabilita_da='" & Trim(txtApplicabilitaDa.Text) & "' AND applicabilita_a='" & Trim(txtApplicabilitaA.Text) & "' " &
                "AND id_elemento='" & dropElementi.SelectedValue & "' " &
                "AND id_unita_misura='" & dropUnitaDiMisura.SelectedValue & "'"
            ElseIf dropElementoVal.SelectedValue <> "0" Then
                sqlStr = "SELECT id FROM condizioni_righe WITH(NOLOCK) WHERE id_condizione='" & lbl_id_codice.Text & "' " &
                "AND applicabilita_da='" & Trim(txtApplicabilitaDa.Text) & "' AND applicabilita_a='" & Trim(txtApplicabilitaA.Text) & "' " &
                "AND id_elemento_val='" & dropElementoVal.SelectedValue & "' " &
                "AND id_unita_misura='" & dropUnitaDiMisura.SelectedValue & "'"
            End If


            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Cmd1 As New Data.SqlClient.SqlCommand("", Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            Rs.Read()

            If Not Rs.HasRows Then
                'IN QUESTO CASO QUESTA CONDIZIONE NON E' STATA INSERITA
                condizioneNonInserita = True
            Else
                Dim almeno_un_gruppo_selezionato As Boolean = False

                For i = 0 To ListBoxGruppiAuto.Items.Count - 1
                    If ListBoxGruppiAuto.Items(i).Selected Then
                        almeno_un_gruppo_selezionato = True
                    End If
                Next

                'CONTROLLO SE C'E' UNA SOVRAPPOSIZIONE DI GRUPPI
                '1) PRIMO CASO: LE DUE CONDIZIONI NON HANNO ALCUN GRUPPO SPECIFICATO

                If Not almeno_un_gruppo_selezionato Then
                    'IN OGNI CASO E' UNA CONDIZIONE CHE VA IN CONTRASTO CON QUELLA TROVATA
                    condizioneNonInserita = False
                Else
                    'CONTROLLO SE VI SONO SOVRAPPOSIZIONI DI GRUPPI CON UNA DELLE CONDIZIONI SELEZIONATE PRECEDENTEMENTE
                    Dim trovata_sovrapposizione As Boolean = False
                    Dim Rs1 As Data.SqlClient.SqlDataReader

                    Do
                        sqlStr = "SELECT gruppi.cod_gruppo As gruppo FROM condizioni_x_gruppi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON condizioni_x_gruppi.id_gruppo=gruppi.id_gruppo WHERE id_condizione='" & Rs("id") & "'"
                        Cmd1 = New Data.SqlClient.SqlCommand(sqlStr, Dbc1)
                        Rs1 = Cmd1.ExecuteReader()

                        Rs1.Read()

                        If Rs1.HasRows Then

                            Do
                                For i = 0 To ListBoxGruppiAuto.Items.Count - 1
                                    If ListBoxGruppiAuto.Items(i).Selected Then
                                        If ListBoxGruppiAuto.Items(i).Text = Rs1("gruppo") Then
                                            trovata_sovrapposizione = True
                                        End If
                                    End If
                                Next
                            Loop Until Not Rs1.Read()

                            If trovata_sovrapposizione Then
                                condizioneNonInserita = False
                            Else
                                condizioneNonInserita = True
                            End If

                            Rs1.Close()
                            Rs1 = Nothing
                            Dbc1.Close()
                            Dbc1.Open()
                        Else
                            'IN QUESTO CASO HO GIA' TROVATO UNA SOVRAPPOSIZIONE PERCHE' LA CONDIZIONE PRELEVATA DA DB NON AVEVA GRUPPI COLLEGATI
                            condizioneNonInserita = False
                        End If


                    Loop Until Not Rs.Read()

                End If
            End If


            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc1.Close()
            Dbc1.Dispose()
            Dbc1 = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error condizioneNonInserita  : <br/>" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try


    End Function

    Protected Function check_metodo_stampa() As Boolean
        Dim sqlStr As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            If dropElementi.SelectedValue <> "0" Then
                sqlStr = "SELECT TOP 1 id_metodo_Stampa FROM condizioni_righe WITH(NOLOCK) WHERE id_elemento='" & dropElementi.SelectedValue & "' AND id_condizione='" & lbl_id_codice.Text & "'"
            ElseIf dropElementoVal.SelectedValue <> "0" Then
                sqlStr = "SELECT TOP 1 id_metodo_Stampa FROM condizioni_righe WITH(NOLOCK) WHERE id_elemento_val='" & dropElementoVal.SelectedValue & "' AND id_condizione='" & lbl_id_codice.Text & "'"
            End If

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim test As String = Cmd.ExecuteScalar & ""

            If test = "" Then
                'PRIMA VOLTA CHE SALVO L'ELEMENTO
                check_metodo_stampa = True
            ElseIf test = dropTipoStampa.SelectedValue Then
                check_metodo_stampa = True
            ElseIf test <> dropTipoStampa.SelectedValue Then
                check_metodo_stampa = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error check_metodo_stampa  : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try



    End Function


    Protected Function check_obbligatorio() As Boolean

        Dim sqlStr As String

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()



            If dropElementi.SelectedValue <> "0" Then
                sqlStr = "SELECT TOP 1 obbligatorio FROM condizioni_righe WITH(NOLOCK) WHERE id_elemento='" & dropElementi.SelectedValue & "' AND id_condizione='" & lbl_id_codice.Text & "' AND id_a_carico_di<>" & Costanti.id_accessorio_incluso
            ElseIf dropElementoVal.SelectedValue <> "0" Then
                sqlStr = "SELECT TOP 1 obbligatorio FROM condizioni_righe WITH(NOLOCK) WHERE id_elemento_val='" & dropElementoVal.SelectedValue & "' AND id_condizione='" & lbl_id_codice.Text & "' AND id_a_carico_di<>" & Costanti.id_accessorio_incluso
            End If

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim test As String = Cmd.ExecuteScalar & ""

            If test = "" Then
                'PRIMA VOLTA CHE SALVO L'ELEMENTO
                check_obbligatorio = True
            ElseIf (chkObbligatorio.Checked And test = "True") Or (Not chkObbligatorio.Checked And test = "False") Then
                check_obbligatorio = True
            Else
                check_obbligatorio = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error check_obbligatorio  : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try

    End Function

    Protected Function elemento_specificabile_in_percentuale() As Boolean
        Try
            If dropElementoVal.SelectedValue <> "0" Then
                elemento_specificabile_in_percentuale = False
            Else
                'IN QUESTO CASO E' STATO SELEZIONATO UN ELEMENTO DA dropElementi. Controllo se è di tipologia 'ONERE' (gli unici
                'specificabili in percentuale)
                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM condizioni_elementi WITH(NOLOCK) WHERE id='" & dropElementi.SelectedValue & "' AND tipologia='ONERE'", Dbc)

                Dim test As String = Cmd.ExecuteScalar

                If test = "" Then
                    elemento_specificabile_in_percentuale = False
                Else
                    elemento_specificabile_in_percentuale = True
                End If

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error elemento_specificabile_in_percentuale  : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try


    End Function

    Protected Function get_tipologia_elemento(ByVal id_elemento As String) As String

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT tipologia FROM condizioni_elementi WITH(NOLOCK) WHERE id='" & id_elemento & "'", Dbc)

            get_tipologia_elemento = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error get_tipologia_elemento  : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try



    End Function

    Protected Sub btnAggiungi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAggiungi.Click
        Dim costo As Double
        Try
            If txtCosto.Text = "" Then
                costo = 0
            Else
                costo = CDbl(txtCosto.Text)
            End If

            Dim tipologia_elemento As String = ""

            If dropElementi.SelectedValue <> "0" Then
                tipologia_elemento = get_tipologia_elemento(dropElementi.SelectedValue)
            End If

            If dropElementi.SelectedValue = "0" And dropElementoVal.SelectedValue = "0" Then
                Libreria.genUserMsgBox(Me, "Selezionare un Elemento o un Elemento VAL.")
            ElseIf dropElementi.SelectedValue <> "0" And dropElementoVal.SelectedValue <> "0" Then
                Libreria.genUserMsgBox(Me, "Selezionare un Elemento o un Elemento VAL .")
            ElseIf dropPerc.SelectedValue = "%" And Not elemento_specificabile_in_percentuale() Then
                Libreria.genUserMsgBox(Me, "Solamente gli oneri sono specificabili in percentuale.")
            ElseIf dropTipoStampa.SelectedValue = "0" Then
                Libreria.genUserMsgBox(Me, "Selezionare un metodo di stampa.")
            ElseIf Not check_metodo_stampa() Then
                Libreria.genUserMsgBox(Me, "Non e' possibile specificare diversi metodi di stampa per uno stesso elemento.")
            ElseIf Not check_obbligatorio() Then
                Libreria.genUserMsgBox(Me, "Non e' possibile specificare valori diversi per il campo 'obbligatorio' per uno stesso elemento.")
            ElseIf dropACaricoDi.SelectedValue = "0" Then
                Libreria.genUserMsgBox(Me, "Selezioanre un campo dalla lista 'A carico di'.")
            ElseIf dropACaricoDi.SelectedValue = "5" And dropUnitaDiMisura.SelectedValue <> "0" Then
                Libreria.genUserMsgBox(Me, "Non è possibile specificare un'unità di misura per una condizone inclusa.")
            ElseIf (dropACaricoDi.SelectedValue = "5" And Trim(txtCosto.Text) <> "") And (dropACaricoDi.SelectedValue = "5" And Trim(txtCosto.Text) <> "0") Then 'aggiunto Or Salvo 05.05.2023
                Libreria.genUserMsgBox(Me, "Non è possibile specificare un costo per una condizone inclusa.")
            ElseIf (dropACaricoDi.SelectedItem.Text <> "Incluso" Or (dropACaricoDi.SelectedValue = "5" And Trim(txtCosto.Text <> ""))) And Not chkSenzaLimite.Checked And Trim(txtApplicabilitaDa.Text) = "" And Trim(txtApplicabilitaA.Text) = "" Then
                Libreria.genUserMsgBox(Me, "Specificare se la condizione viene applicata senza alcun limite oppure specificare i limiti.")
            ElseIf dropACaricoDi.SelectedValue <> "5" And Not chkSenzaLimite.Checked And (Not (Trim(txtApplicabilitaDa.Text) <> "" And Trim(txtApplicabilitaA.Text) <> "")) Then
                Libreria.genUserMsgBox(Me, "Specificare entrambi i limiti di applicabilità della condizione.")
            ElseIf dropPerc.SelectedValue = "%" And costo > 100 Then
                Libreria.genUserMsgBox(Me, "Specificare un valore corretto per il costo percentuale.")
            ElseIf dropElementoVal.SelectedValue <> "0" And dropUnitaDiMisura.SelectedValue <> "9" And dropUnitaDiMisura.SelectedValue <> "16" And dropUnitaDiMisura.SelectedValue <> "0" Then
                Libreria.genUserMsgBox(Me, "E' possibile specificare solo 'GIORNI' o 'KM tra stazioni' come unità di misura.")
            ElseIf (dropUnitaDiMisura.SelectedValue = "11" Or dropUnitaDiMisura.SelectedValue = "13" Or dropUnitaDiMisura.SelectedValue = "14" Or dropUnitaDiMisura.SelectedValue = "15") And (dropTipoStampa.SelectedValue = "2") Then
                Libreria.genUserMsgBox(Me, "Per le unità di misura dipendenti dai km percorsi non è possibile valorizzare su contratto.")
            ElseIf dropElementoVal.SelectedValue <> "0" And Not chkObbligatorio.Checked Then
                Libreria.genUserMsgBox(Me, "L'elemento VAL deve essere obbligatorio.")
            ElseIf dropElementoVal.SelectedValue <> "0" And Trim(txtCosto.Text) = "" And dropACaricoDi.SelectedValue <> "5" Then
                Libreria.genUserMsgBox(Me, "L'elemento VAL deve avere un valore sempre specificato.")
            ElseIf tipologia_elemento = "VAL_GPS" And Not chkObbligatorio.Checked Then
                Libreria.genUserMsgBox(Me, "L'elemento VAL SATELLITARE deve essere obbligatorio.")
            ElseIf tipologia_elemento = "VAL_GPS" And Trim(txtCosto.Text) = "" And dropACaricoDi.SelectedValue <> "5" Then
                Libreria.genUserMsgBox(Me, "L'elemento VAL SATELLITARE deve avere un valore sempre specificato.")
            ElseIf dropPerc.SelectedValue = "%" And txtCosto.Text <> "0" And dropUnitaDiMisura.SelectedValue <> "0" And Not chkPacked.Checked Then
                Libreria.genUserMsgBox(Me, "In caso di valore percentuale e di condizione con unità di misura è necessario specificare il valore come PACKED.")
            ElseIf chkPacked.Checked And chkSenzaLimite.Checked And dropUnitaDiMisura.SelectedValue <> "0" Then
                Libreria.genUserMsgBox(Me, "Una condizione senza limiti di applicabilità e PACKED deve essere specificata senza unità di misura.")
            ElseIf dropElementi.SelectedValue = funzioni_comuni.get_id_servizio_rifornimento And dropTipoStampa.SelectedValue <> Costanti.id_stampa_informativa_con_valore And dropTipoStampa.SelectedValue <> Costanti.id_stampa_informativa_senza_valore Then
                Libreria.genUserMsgBox(Me, "Attenzione: l'elemento selzionato (salvato come 'SERVIZIO RIFORNIMENTO' NELLA FUNZIONALITA' 'CARBURANTE' DENTRO 'TABELLE LISTINI') deve essere un'informativa per essere gestito in maniera automatica come richiesto.")
            ElseIf tipologia_elemento = "SPESE_SPED_FATT" And Not chkObbligatorio.Checked Then
                Libreria.genUserMsgBox(Me, "Attenzione: l'elemento selezionato (salvato come 'SPESE SPEDIZIONI POSTALI FATTURA' dentro 'TABELLE LISTINI') deve essere obbligatorio (il sistema lo aggiungerà automaticamente quando la ditta collegata ad una prenotazione o contratto richiede l'invio per posta della fattura.")
            Else
                Dim test_da As Integer
                Dim test_a As Integer
                'Try
                If txtApplicabilitaDa.Text = "" Then
                    txtApplicabilitaDa.Text = "0"
                End If
                If txtApplicabilitaA.Text = "" Then
                    txtApplicabilitaA.Text = "0"
                End If

                test_da = CInt(Trim(txtApplicabilitaDa.Text))
                test_a = CInt(Trim(txtApplicabilitaA.Text))

                If txtApplicabilitaDa.Text = "0" Then
                    txtApplicabilitaDa.Text = ""
                End If
                If txtApplicabilitaA.Text = "0" Then
                    txtApplicabilitaA.Text = ""
                End If

                If (test_da > test_a) Or (test_da < 0) Or (test_a < 0) Then
                    Libreria.genUserMsgBox(Me, "Specificare correttamente i limiti di applicabilità della condizione.")
                ElseIf (txtApplicabilitaDa.Text <> "" And txtApplicabilitaA.Text <> "") And (dropUnitaDiMisura.SelectedValue = "0") Then
                    Libreria.genUserMsgBox(Me, "Specificare l'unità di misura dell'applicabilità della condizone.")
                ElseIf (Trim(txtApplicabilitaDa.Text) <> "" And Trim(txtApplicabilitaA.Text) <> "") And Trim(txtCosto.Text) = "" Then
                    Libreria.genUserMsgBox(Me, "Specificare il costo della condizione.")

                ElseIf Not condizioneNonInserita() And btnAggiungi.Text <> "Modifica e salva" Then 'modificato salvo 04.05.203

                    Libreria.genUserMsgBox(Me, "La condizione o una parte di essa è stata già specificata.")

                    'ElseIf Not stessa_unita_di_misura() Then
                    '    'TENEDNDO FISSO L'ELEMENTO L'UNITA' DI MISURA DELLE DIVERSE CONDIZIONI DEVE RESTARE LA STESSA
                    '    Libreria.genUserMsgBox(Me, "Non è possibile specificare due condizioni con una diversa unità di misura per uno stesso elemento.")
                    '#### X TEST le righe sono rem
                    'ElseIf Not checkGruppi() Then

                    '    Libreria.genUserMsgBox(Me, "Attenzione: è stata già specificata una condizione senza alcun limite per uno dei gruppi selezionati.")
                    '@@@@ X TEST le righe sopra sono rem

                Else
                    Dim num_gruppi As Integer = 0

                    For i = 0 To ListBoxGruppiAuto.Items.Count - 1
                        If ListBoxGruppiAuto.Items(i).Selected Then
                            num_gruppi = num_gruppi + 1
                        End If
                    Next
                    salvaRiga(num_gruppi)



                End If
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error  btnAggiungi_click: <br/>" & ex.Message & "<br/>" & sqla & "<br/>")

        End Try




    End Sub

    Protected Function checkGruppi() As Boolean
        'SE E' STATO SELEZIONATO UNO O PIU' GRUPPI CONTROLLO CHE NON ESISTE UNA RIGA PER LO STESSO ELEMENTO CHE E' SENZA LIMITE
        '(applicabilita_da='0' AND applicabilita_a='0') E CON LO STESSO GRUPPO SPECIFICATO
        Dim almeno_un_gruppo_selezionato As Boolean = False

        Try
            For i = 0 To ListBoxGruppiAuto.Items.Count - 1
                If ListBoxGruppiAuto.Items(i).Selected Then
                    almeno_un_gruppo_selezionato = True
                End If
            Next

            If Not almeno_un_gruppo_selezionato Then
                'IN QUESTO CASO STO SPECIFICANDO UNA CONDIZIONE SENZA GRUPPI PER CUI NON DEVO CONTROLLARE NULLA
                checkGruppi = True
            Else
                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

                Dim trovato As Boolean = False

                For i = 0 To ListBoxGruppiAuto.Items.Count - 1
                    If ListBoxGruppiAuto.Items(i).Selected Then

                        Dim sqlStr As String
                        sqlStr = "SELECT TOP 1 condizioni_righe.id FROM condizioni_righe WITH(NOLOCK) INNER JOIN condizioni_x_gruppi WITH(NOLOCK) ON condizioni_x_gruppi.id_condizione=condizioni_righe.id " &
                        "WHERE applicabilita_da='0' AND applicabilita_a='0' AND condizioni_x_gruppi.id_gruppo='" & ListBoxGruppiAuto.Items(i).Value & "' AND condizioni_righe.id_condizione='" & lbl_id_codice.Text & "' AND "

                        If dropElementi.SelectedValue <> "0" Then
                            sqlStr = sqlStr & "id_elemento='" & dropElementi.SelectedValue & "'"
                        ElseIf dropElementoVal.SelectedValue <> "0" Then
                            sqlStr = sqlStr & "id_elemento_val='" & dropElementoVal.SelectedValue & "'"
                        End If

                        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                        Dim test As String = Cmd.ExecuteScalar

                        If test <> "" Then
                            trovato = True
                        End If

                    End If
                Next

                If trovato Then
                    checkGruppi = False
                Else
                    checkGruppi = True
                End If

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error  checkGruppi: <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try



    End Function

    Protected Sub btnNuovo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovo.Click
        'CREO UNA RIGA NON ATTIVA IN condizioni

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO condizioni (attivo) VALUES (0)", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd = New Data.SqlClient.SqlCommand("SELECT MAX(id) FROM condizioni WITH(NOLOCK)", Dbc)

            lbl_id_codice.Text = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            lbl_tipo_op.Text = "nuova"
            btnAnnulla.Text = "Annulla"

            btnAggiungi.Text = "Aggiungi"

            btnModificaVAL.Visible = False
            dropTemplateVal.Enabled = True
            dropTemplateVal.SelectedValue = "0"

            dropElementoVal.Items.Clear()
            dropElementoVal.Items.Add("Seleziona...")
            dropElementoVal.Items(0).Value = "0"

            btnSalva.Visible = True
            tab_vedi.Visible = True
            tab_cerca.Visible = False
        Catch ex As Exception
            HttpContext.Current.Response.Write("error  btnNuovo_Click: <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Function almeno_una_condizione_salvata(ByVal idCondizione As String) As Boolean
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM condizioni_righe WITH(NOLOCK) WHERE id_condizione='" & lbl_id_codice.Text & "'", Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test <> "" Then
                almeno_una_condizione_salvata = True
            Else
                almeno_una_condizione_salvata = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error  almeno_una_condizione_salvata: <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try

    End Function

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click

        Try

            If almeno_una_condizione_salvata(lbl_id_codice.Text) Then
                Dim valido_da As String = txtValidoDa.Text

                valido_da = funzioni_comuni.getDataDb_senza_orario(valido_da)

                Dim valido_a As String = txtValidoA.Text

                valido_a = funzioni_comuni.getDataDb_senza_orario(valido_a)


                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE condizioni SET codice='" & Replace(lblCodice.Text, "'", "''") & "', valido_da=convert(datetime,'" & valido_da & "',102), valido_a=convert(datetime,'" & valido_a & "',102), data_creazione=convert(datetime,getDate(),102), attivo='1', iva_inclusa='" & dropIva.SelectedValue & "' WHERE id='" & lbl_id_codice.Text & "'", Dbc)

                Cmd.ExecuteScalar()

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                Libreria.genUserMsgBox(Me, "Salvataggio effettuato correttamente")

                listCondizioni.DataBind()

                lbl_tipo_op.Text = ""

                lbl_id_codice.Text = ""
                lblCodice.Text = ""

                txtValidoA.Text = ""
                txtValidoDa.Text = ""

                tab_vedi.Visible = False
                tab_cerca.Visible = True
            Else
                Libreria.genUserMsgBox(Me, "Specificare almeno una condizione.")
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error btnSalva_Click : <br/>" & ex.Message & "<br/>" & "<br/>")

        End Try

    End Sub

    Protected Sub chkSenzaLimite_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSenzaLimite.CheckedChanged
        If Not chkSenzaLimite.Checked Then
            txtApplicabilitaDa.Text = ""
            txtApplicabilitaDa.ReadOnly = False

            txtApplicabilitaA.Text = ""
            txtApplicabilitaA.ReadOnly = False
        Else
            txtApplicabilitaDa.Text = ""
            txtApplicabilitaDa.ReadOnly = True

            txtApplicabilitaA.Text = ""
            txtApplicabilitaA.ReadOnly = True
        End If
    End Sub

    Protected Sub btnModificaIntestazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaIntestazione.Click
        If check_condizione(lblCodice.Text, txtValidoDa.Text, txtValidoA.Text, lbl_id_codice.Text) Then
            modifica_condizione(lbl_id_codice.Text, txtValidoDa.Text, txtValidoA.Text, lblCodice.Text)
            Libreria.genUserMsgBox(Me, "Modifica effettuata correttamente.")
        Else
            Libreria.genUserMsgBox(Me, "Condizione già esistente: modifica non effettuata.")
        End If

    End Sub

    Protected Sub dropTemplateVal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropTemplateVal.SelectedIndexChanged
        Try
            If dropTemplateVal.SelectedValue <> "0" Then
                'NEL MOMENTO IN CUI CAMBIO IL TEMPLATE VAL SELEZIONATO LO SALVO SUBITO NELL'INTESTAZIONE ED ELIMINO I PRECEDENTI VAL COLLEGATI
                'ALLA CONDIZIONE
                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE condizioni SET id_template_val='" & dropTemplateVal.SelectedValue & "' WHERE id='" & lbl_id_codice.Text & "'", Dbc)
                Cmd.ExecuteNonQuery()

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing


                btnModificaVAL.Visible = True
                dropTemplateVal.Enabled = False

                dropElementoVal.Items.Clear()
                dropElementoVal.Items.Add("Seleziona...")
                dropElementoVal.Items(0).Value = "0"

                dropElementoVal.DataBind()
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error dropTemplateVal_SelectedIndexChanged : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Sub elimina_val()
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Dbc1 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc1.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM condizioni_righe WITH(NOLOCK) WHERE id_condizione='" & lbl_id_codice.Text & "' AND NOT id_elemento_val IS NULL", Dbc)
            Dim Cmd1 As New Data.SqlClient.SqlCommand("", Dbc1)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Do While Rs.Read()
                Cmd1 = New Data.SqlClient.SqlCommand("DELETE FROM condizioni_x_gruppi WHERE id_condizione='" & Rs("id") & "'", Dbc1)
                Cmd1.ExecuteNonQuery()
            Loop

            Cmd1 = New Data.SqlClient.SqlCommand("DELETE FROM condizioni_righe WHERE id_condizione='" & lbl_id_codice.Text & "' AND NOT id_elemento_val IS NULL", Dbc1)
            Cmd1.ExecuteNonQuery()

            Cmd1 = New Data.SqlClient.SqlCommand("UPDATE condizioni SET id_template_val=NULL WHERE id='" & lbl_id_codice.Text & "'", Dbc1)
            Cmd1.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
            Dbc1.Close()
            Dbc1.Dispose()
            Dbc1 = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error elimina_val : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try


    End Sub

    Protected Sub btnModificaVAL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificaVAL.Click
        btnModificaVAL.Visible = False
        dropTemplateVal.Enabled = True
        dropTemplateVal.SelectedValue = "0"

        dropElementoVal.Items.Clear()
        dropElementoVal.Items.Add("Seleziona...")
        dropElementoVal.Items(0).Value = "0"

        elimina_val()

    End Sub

    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampa.Click
        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            Session("url_print") = "/stampe/stampa_condizione.aspx?pagina=verticale&id_cond=" & lbl_id_codice.Text
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
            End If
        End If
    End Sub




    Protected Function condizioneWhere() As String
        Dim condizione As String = ""

        If txtCodice.Text <> "" Then
            condizione = condizione & " AND codice LIKE '" & Replace(txtCodice.Text, "'", "''") & "%'"
        End If

        If txtCercaValidoDa.Text <> "" And txtCercaValidoA.Text = "" Then
            Dim data1 As String = funzioni_comuni.getDataDb_senza_orario(txtCercaValidoDa.Text)

            condizione = condizione & " AND valido_da='" & data1 & "'"
        End If

        If txtCercaValidoDa.Text = "" And txtCercaValidoA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.getDataDb_senza_orario(txtCercaValidoA.Text)

            condizione = condizione & " AND valido_a='" & data2 & "'"
        End If

        If txtCercaValidoDa.Text <> "" And txtCercaValidoA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.getDataDb_senza_orario(txtCercaValidoDa.Text)
            Dim data2 As String = funzioni_comuni.getDataDb_senza_orario(txtCercaValidoA.Text)

            condizione = condizione & " AND valido_da=convert(datetime,'" & data1 & "',102) AND valido_a=convert(datetime,'" & data2 & "',102)"
        End If

        condizioneWhere = condizione
    End Function

    Protected Sub cerca()
        sqlCondizioni.SelectCommand = "SELECT id, codice, valido_da, valido_a, data_creazione, id_template_val, iva_inclusa " & _
            "FROM condizioni WITH(NOLOCK) WHERE attivo='1' " & condizioneWhere()

        lblQuery.Text = sqlCondizioni.SelectCommand
        sqlCondizioni.SelectCommand = lblQuery.Text & " ORDER BY data_creazione DESC"
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        cerca()
    End Sub


    Protected Function getCondizioniPeriodiCosti(ByVal id_riga As String) As String

        Dim ris As String = ""


        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            'ordinato per dataperiodo e data creazione dec salvo 12.07.2023
            Dim sqlstr As String = "Select * from condizioni_righe_costi_periodi where id_riga='" & id_riga & "' order by periodo_da, data_creazione desc"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Do While Rs.Read()

                If id_riga = 4030 Then
                    id_riga = 4030
                End If

                Dim perDa As Date

                If IsDBNull(Rs!periodo_da) Then
                    perDa = "01/01/2023"           'verificare se formato data corretto su Produzione
                Else
                    perDa = Rs!periodo_da
                End If

                If Not IsDBNull(perDa) Then
                    'If DateDiff("d", Rs("periodo_da"), Date.Now) < 0 Then

                    Dim str As String = FormatDateTime(perDa, vbShortDate) & " € " & Rs!costo
                    'str = "<a href='lista_condizioni.aspx?mode=upd&idrec=" & Rs!id & "&idriga=" & id_riga & "' target='_self'>" & str & "</a>"
                    'str = "<a href='lista_condizioni.aspx?mode=upd&idrec=" & Rs!id & "&idriga=" & id_riga & "' target='_self'>" & str & "</a>"

                    If ris.IndexOf(str) = -1 Then
                        If ris = "" Then
                            ris = "(dal " & str & " - <a target='_self' href='lista_condizioni.aspx?mode=del&idrec=" & Rs!id & "&idriga=" & id_riga & "'> elimina)</a> "
                        Else
                            ris += " - (dal " & str & " - <a target='_self' href='lista_condizioni.aspx?mode=del&idrec=" & Rs!id & "&idriga=" & id_riga & "'> elimina)</a> "
                        End If

                    End If
                    'End If

                End If

            Loop

            'ris = "(" & ris & ")"


            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("GetCondizioni Error: " & ex.Message & "</br>")
        End Try

        Return ris

    End Function

    Private Sub lista_condizioni_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        'su completo caricamento della lista e se proviene da Elimina 
        'fa il reset del form
        If Session("elimina_costoperiodo_riga") = "si" Then
            dropElementi.SelectedValue = "0"
            dropTipoStampa.SelectedValue = "0"
            btnAggiungi.Text = "Aggiungi e salva"
            lbl_id_riga.Text = ""
            dropElementi.SelectedValue = "0"
            dropElementoVal.SelectedIndex = "0"
            dropTipoStampa.SelectedIndex = "0"
            txtApplicabilitaDa.Text = ""
            txtApplicabilitaA.Text = ""
            txtCosto.Text = ""
            chkObbligatorio.Checked = False
            chkPacked.Checked = False
            chkSenzaLimite.Checked = False
            txt_periodo_da.Text = ""
            txt_periodo_a.Text = ""
            dropACaricoDi.SelectedValue = "0"
            dropUnitaDiMisura.SelectedValue = "0"
            'dropPerc.SelectedValue = "0"

        End If


    End Sub
End Class
