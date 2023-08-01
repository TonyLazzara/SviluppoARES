
Partial Class lavaggi
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then            
            tab_fornitore.Visible = False

            id_rifornimento.Text = Request("rifornimento")

            serb.Text = Request("serb")

            Dim test As Integer

            Try
                test = CInt(id_rifornimento.Text)
            Catch ex As Exception
                test = 0
            End Try

            Try
                test = CInt(serb.Text)
            Catch ex As Exception
                test = 0
            End Try

            If test <> 0 Then
                Dim IdAus As String
                Dim StatoVeicolo As String
                Dim IdStazione As String
                Dim SerbatoioOut As String
                Dim Id As String
                Dim Id_Veicolo As String
                Dim Data_Rientro As String
                Dim Data_Rientro2 As String

                'Response.Write("IF3")
                'Response.End()


                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                Dim Cmd As New Data.SqlClient.SqlCommand("select * from lavaggi WITH(NOLOCK) where id =" & id_rifornimento.Text, Dbc)
                'Response.Write(Cmd.CommandText & "<br><br>")
                'Response.End()

                Dim Rs As Data.SqlClient.SqlDataReader
                Rs = Cmd.ExecuteReader()
                If Rs.Read() Then
                    'Response.Write("IF4")
                    'Response.End()

                    id_stazione.Text = get_id_stazione_veicolo(Rs("id_veicolo"))

                    If (Rs("data_uscita_parco") & "") = "" And (Rs("data_rientro_parco") & "") = "" Then
                        'Da Lavare
                        LblDataMovimento.Text = "Data/Ora Uscita dal Parco"

                        LblConducenti.Visible = True
                        DDLConducenti.Visible = True
                        StatoVeicolo = "Da Lavare"

                        'btnVediFornitore.Visible = False



                    ElseIf (Rs("data_uscita_parco") & "") <> "" And (Rs("data_rientro_parco") & "") = "" Then
                        'A Lavaggio
                        'lblNumeroRifornimento.Text = Right(Rs("anno_rifornimento") & "", 2) & "/" & Rs("num_rifornimento") & ""

                        LblDataMovimento.Text = "Data/Ora Rientro nel Parco"
                        TxtKm.Enabled = True
                        LblImporto.Visible = False
                        LblImporto.Text = "Importo"

                        StatoVeicolo = "A Lavaggio"

                        lblFornitore.Visible = False
                        'DDLFornitore.Visible = False

                        'btnVediFornitore.Visible = True

                        LblConducenti.Visible = True
                        DDLConducenti.Visible = True

                        If Rs("id_conducente") & "" <> "" Then
                            DDLConducenti.SelectedValue = Rs("id_conducente")
                            DDLConducenti.Enabled = False
                        End If

                    Else
                        'Lavato        
                        'lblNumeroRifornimento.Text = Right(Rs("anno_rifornimento") & "", 2) & "/" & Rs("num_rifornimento") & ""

                        Dim data1 As String = funzioni_comuni.getDataDb_senza_orario2(Rs("data_rientro_parco"))

                        Dim Ora1 As String = funzioni_comuni.getDataDb_orario_senza_data(Rs("data_rientro_parco"))

                        Id = Rs("id")
                        Id_Veicolo = Rs("Id_Veicolo")
                        Data_Rientro = Libreria.FormattaDataOreMinSec(Rs("data_rientro_parco"))

                        BtnSalva.Visible = False

                        LblDataMovimento.Text = "Data/Ora Rientro nel Parco"
                        txtDataMovimento.Visible = False
                        LblSeperatore.Visible = False
                        txtOra.Visible = False

                        txtDataMovimento1.Visible = True
                        txtDataMovimento1.Enabled = False

                        Separatore1.Visible = True


                        txtOra1.Visible = True
                        txtOra1.Enabled = False

                        lblFornitore.Visible = True
                        'DDLFornitore.Visible = True

                        'btnVediFornitore.Visible = False

                        LblConducenti.Visible = True
                        DDLConducenti.Visible = True

                        If Rs("id_conducente") & "" <> "" Then
                            DDLConducenti.SelectedValue = Rs("id_conducente")
                            DDLConducenti.Enabled = False
                        End If

                        'If Rs("litri_riforniti") = "0" Then
                        '    txtLitriRiforniti.Enabled = True
                        'Else
                        '    txtLitriRiforniti.Enabled = False
                        'End If

                        LblImporto.Visible = True

                        'If (Rs("litri_riforniti") & "") <> "0" Then
                        '    txtLitriRiforniti.Text = Rs("litri_riforniti")
                        'End If

                        'If (Rs("importo_rifornimento") & "") <> "" Then
                        '    TxtImporto.Text = Rs("importo_rifornimento") & ""
                        'End If

                        'If txtLitriRiforniti.Text <> "" And TxtImporto.Text <> "" Then
                        '    'CALCOLO DEL COSTO AL LITRO
                        '    Try
                        '        lblCostoLitro.Text = FormatNumber(CDbl(TxtImporto.Text) / CDbl(txtLitriRiforniti.Text), 2, , , TriState.False)
                        '    Catch ex As Exception
                        '        lblCostoAlLitro.Text = ""
                        '    End Try
                        'Else
                        '    lblCostoLitro.Text = ""
                        'End If


                        'DDLFornitore.DataBind()

                        'Try
                        '    DDLFornitore.SelectedValue = Rs("id_fornitore")
                        'Catch ex As Exception
                        '    DDLFornitore.SelectedValue = "0"
                        'End Try

                        'DDLFornitore.Enabled = False

                        txtDataMovimento1.Text = Libreria.FormattaDataItaliana(data1)
                        txtOra1.Text = Ora1

                        'LblDataMovimento.Visible = False
                        'LblKm.Visible = False

                        'txtDataMovimento.Visible = False
                        'LblSeperatore.Visible = False
                        'txtOra.Visible = False
                        'TxtKm.Visible = False
                        'LblKmAttuali.Visible = False

                        'DDLConducenti.Visible = False

                        'BtnSalva.Visible = False

                        StatoVeicolo = "Lavato"

                        'PER UN RIFORNIMENTO EFFETTUATO POTREBBE ESSERE NECESSARIO SPECIFICARE ANCORA L'IMPORTO  OPPURE IL FORNITORE
                        'If (Rs("importo_rifornimento") & "") = "" Then
                        '    LblImporto.Visible = True
                        '    LblImporto.Text = "Importo"
                        '    TxtImporto.Visible = True
                        '    StatoVeicolo = "Inserire Importo"

                        '    lblCostoAlLitro.Visible = True

                        '    lblLitriRiforniti.Visible = True
                        '    txtLitriRiforniti.Visible = True

                        '    LblConducenti.Visible = True
                        '    DDLConducenti.Visible = True

                        '    DDLFornitore.Visible = True
                        '    lblFornitore.Visible = True

                        '    BtnSalva.Visible = True

                        '    RFImporto.Enabled = True
                        'End If

                        'Dim DbcRifornimentoPrec As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        'DbcRifornimentoPrec.Open()

                        'Dim SqlRifornimentoPrec As String
                        'SqlRifornimentoPrec = "SELECT id, id_veicolo, data_uscita_parco, km_out, data_rientro_parco, km_in, data_rifornimento, importo_rifornimento, litri_riforniti, serbatoio, costo_benzina_litro, " & _
                        '                            "id_conducente, id_stazione_out, id_stazione_in, id_tipologia, riferimento, id_operatore_apertura, data_operazione_apertura, id_operatore_chiusura, " & _
                        '                            "data_operazione_chiusura, registrato_in_contratto_id " & _
                        '                      "FROM rifornimenti WITH (NOLOCK) " & _
                        '                            "WHERE (id < " & Id & ") AND (id_veicolo = " & Id_Veicolo & ")"


                        'Dim CmdRifornimentoPrec As New Data.SqlClient.SqlCommand(SqlRifornimentoPrec, DbcRifornimentoPrec)
                        ''Response.Write(CmdRifornimentoPrec.CommandText & "<br><br>")
                        ''Response.End()

                        'Dim RsRifornimentoPrec As Data.SqlClient.SqlDataReader
                        'RsRifornimentoPrec = CmdRifornimentoPrec.ExecuteReader()
                        'Do While RsRifornimentoPrec.Read
                        '    Data_Rientro2 = Libreria.FormattaDataOreMinSec(RsRifornimentoPrec("data_rientro_parco"))
                        'Loop

                        'CmdRifornimentoPrec.Dispose()
                        'CmdRifornimentoPrec = Nothing

                        'RsRifornimentoPrec.Close()
                        'RsRifornimentoPrec.Dispose()
                        'RsRifornimentoPrec = Nothing

                        'DbcRifornimentoPrec.Close()
                        'DbcRifornimentoPrec.Dispose()
                        'DbcRifornimentoPrec = Nothing


                    End If
                    'Response.Write("IF5- Stato Veicolo" & StatoVeicolo)
                    'Response.End()

                    IdAus = Rs("Id_veicolo")
                    'SerbatoioOut = Rs("serbatoio")


                    



                    'Response.Write(SqlMovimenti.SelectCommand & "<br><br>")
                    'Response.End()

                    'ListViewMovimenti.DataBind()
                Else
                    'LA RIGA POTREBBE ESSERE STATA CANCELLATA (AVENDO ESEGUITO UN CHECK OUT DEL VEICOLO SENZA IL PIENO EFFETTUATO)
                    Cmd.Dispose()
                    Cmd = Nothing

                    Rs.Close()
                    Rs.Dispose()
                    Rs = Nothing

                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing
                    Response.Write("Else")
                    Response.End()

                    Response.Redirect("elenco_lavaggi.aspx")
                End If

                'Response.Write("IF6- Stato Veicolo" & StatoVeicolo)
                'Response.End()

                Cmd.Dispose()
                Cmd = Nothing

                Rs.Close()
                Rs.Dispose()
                Rs = Nothing

                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc2.Open()

                Dim SqlVeicolo As String
                SqlVeicolo = "SELECT  veicoli.id,veicoli.targa, veicoli.id_stazione, ISNULL(veicoli.serbatoio_attuale,0) As serbatoio_attuale, ISNULL(modelli.capacita_serbatoio,0) As capacita_serbatoio, MODELLI.descrizione AS modello, marche.descrizione AS marca,  veicoli.km_attuali, modelli.TipoCarburante, veicoli.da_lavare " & _
                                 "FROM veicoli WITH(NOLOCK) INNER JOIN " & _
                                 "MODELLI WITH(NOLOCK) ON veicoli.id_modello = MODELLI.ID_MODELLO INNER JOIN " & _
                                 "marche WITH(NOLOCK) ON MODELLI.ID_CasaAutomobilistica = marche.id "

                Dim Cmd2 As New Data.SqlClient.SqlCommand(SqlVeicolo & " where veicoli.id =" & IdAus, Dbc2)
                'Response.Write(Cmd2.CommandText & "<br><br>")
                'Response.End()

                Dim Rs2 As Data.SqlClient.SqlDataReader
                Rs2 = Cmd2.ExecuteReader()
                Do While Rs2.Read
                    'Valorizzo inf veicolo
                    LblTargaTesto.Text = Rs2("targa")
                    LblMarcaTesto.Text = Rs2("marca")
                    LblModelloTesto.Text = Rs2("modello")

                    lblSerbatoioAtt.Text = Rs2("serbatoio_attuale")
                    lblSerbatoioMax.Text = Rs2("capacita_serbatoio")

                    LblStatoTesto.Text = StatoVeicolo
                    If (Rs2("km_attuali") & "") <> "" Then
                        LblKmAttuali.Text = "(Km attuali: " & Rs2("km_attuali") & ")"
                    Else
                        LblKmAttuali.Text = "(Km attuali ND)"
                    End If
                    TxtKm.Text = Rs2("km_attuali")
                    If Not Page.IsPostBack Then
                        txtDataMovimento.Text = Today()
                        txtOra.Text = TimeOfDay()
                    End If
                    IdStazione = Rs2("id_stazione")
                    id_alimentazione.Text = Rs2("TipoCarburante") & ""

                    If Rs2("da_lavare") Then
                        da_lavare.Text = "1"
                    Else
                        da_lavare.Text = "0"
                    End If
                Loop

                Cmd2.Dispose()
                Cmd2 = Nothing

                Rs2.Close()
                Rs2.Dispose()
                Rs2 = Nothing

                Dbc2.Close()
                Dbc2.Dispose()
                Dbc2 = Nothing

                Session("StatoDelVeicolo") = StatoVeicolo
                Session("IdVeicolo") = IdAus
                'Session("SerbatoioOut") = SerbatoioOut
            Else
                Response.Redirect("default.aspx")
            End If
        End If
    End Sub

    Protected Function get_id_stazione_veicolo(ByVal id_veicolo As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_stazione FROM veicoli WHERE id='" & id_veicolo & "'", Dbc)

        get_id_stazione_veicolo = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub BtnChiudi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnChiudi.Click
        'LblSession.Text = Session("rifornumento_stazione") & "-" & Session("rifornumento_targa") & "-" & Session("rifornumento_telaio") & "-" & Session("rifornumento_marca") & "-" & Session("rifornumento_modello") & "-" & Session("rifornumento_stato")
        'LblSession.Visible = True
        Response.Redirect("elenco_lavaggi.aspx")
    End Sub

    Protected Function is_stato_da_rifornire(ByVal id_rif As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM rifornimenti WHERE data_uscita_parco IS NULL AND id='" & id_rif & "'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test = "" Then
            is_stato_da_rifornire = False
        Else
            is_stato_da_rifornire = True
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub BtnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSalva.Click
       

        salvaDati()


        'Response.Write(Session("StatoDelVeicolo") & "<br><br>")
        'Response.End()

        If Session("StatoDelVeicolo") = "Da Lavare" Then
            salvaDatiMovimento()
        End If

        If Session("StatoDelVeicolo") = "A Lavaggio" Then
            AggiornaDatiMovimento()

            'salvaDatiMovimento2()
            Session("StatoDelVeicolo") = "Lavato"
        End If

        Response.Redirect("elenco_lavaggi.aspx")
        'Response.Write(Session("StatoDelVeicolo") & "<br><br>")
        'Response.End()
    End Sub

    Protected Sub salvaDati()
        Dim DataOra As String

        Dim DbcSalvataggio As New Data.SqlClient.SqlConnection(SqlSalvaDati.ConnectionString)
        DbcSalvataggio.Open()
        Dim CmdSalvataggio As New Data.SqlClient.SqlCommand("", DbcSalvataggio)

        'DataOra = txtDataMovimento.Text & " " & txtOra.Text        

        ' Response.Write(txtDataMovimento.Text & "<br>")
        ' Response.Write(txtOra.Text)
        'Response.End()
        'DataOra = Year(txtDataMovimento.Text) & "-" & Month(txtDataMovimento.Text) & "-" & Day(txtDataMovimento.Text) & " " & Replace(txtOra.Text, ".", ":")
        DataOra = funzioni_comuni.getDataDb_con_orario(txtDataMovimento.Text & " " & Replace(txtOra.Text, ".", ":"))

        Select Case Session("StatoDelVeicolo")
            Case Is = "Da Lavare"
                'GENERAZIONE DEL NUMERO DI Lavaggio
                Dim anno As String = Year(txtDataMovimento.Text)
                Dim numero_rifornimento As String = funzioni_comuni.getNumRifornimento(id_stazione.Text, anno)

                CmdSalvataggio.CommandText =
                    "update lavaggi set data_uscita_parco ='" & DataOra & "', km_out ='" & TxtKm.Text & "', id_conducente ='" & DDLConducenti.SelectedValue & "', id_stazione_out ='" & id_stazione.Text & "'," &
                    "id_operatore_apertura='" & Request.Cookies("SicilyRentCar")("idUtente") & "', data_operazione_apertura=GetDate() " &
                    " where id = '" & id_rifornimento.Text & "'"

                '    "update rifornimenti set anno_rifornimento='" & anno & "', num_rifornimento='" & numero_rifornimento & "', data_uscita_parco ='" & DataOra & "', km_out ='" & TxtKm.Text & "', id_conducente ='" & DDLConducenti.SelectedValue & "', id_stazione_out ='" & id_stazione.Text & "'," & _
                '    "id_operatore_apertura='" & Request.Cookies("SicilyRentCar")("idUtente") & "', data_operazione_apertura=GetDate() " & _
                '    " where id = '" & id_rifornimento.Text & "'"


                'lblNumeroRifornimento.Text = Right(anno, 2) & "/" & numero_rifornimento
            Case Is = "A Lavaggio"
                'L'IMPORTO NON E' OBBLIGATORIO IN QUANTO PUO' NON ESSERE NOTO AL MOMENTO DELLA REGISTRAZIONE - PUO' ESSERE SPECIFICATO SUCCESSIVAMENTE


                CmdSalvataggio.CommandText =
                   "update lavaggi set data_rientro_parco ='" & DataOra & "', data_lavaggio ='" & DataOra & "', km_in ='" & TxtKm.Text & "',  id_stazione_in ='" & id_stazione.Text & "', id_conducente ='" & DDLConducenti.SelectedValue & "'," &
                   "id_operatore_chiusura='" & Request.Cookies("SicilyRentCar")("idUtente") & "'" &
                   " where id = '" & id_rifornimento.Text & "'"

        End Select

        'Response.Write(CmdSalvataggio.CommandText & "<br><br>")
        'Response.Write(Session("StatoDelVeicolo") & "<br><br>")
        'Response.End()

        CmdSalvataggio.ExecuteNonQuery()
        CmdSalvataggio.Dispose()
        DbcSalvataggio.Close()
        DbcSalvataggio = Nothing
    End Sub

    Protected Sub salvaDatiMovimento()
        Dim DataOra As String

        Dim DbcSalvataggio As New Data.SqlClient.SqlConnection(SqlSalvaDati.ConnectionString)
        DbcSalvataggio.Open()
        Dim CmdSalvataggio As New Data.SqlClient.SqlCommand("", DbcSalvataggio)

        'DataOra = txtDataMovimento.Text & " " & txtOra.Text        

        DataOra = funzioni_comuni.getDataDb_con_orario(txtDataMovimento.Text & " " & Replace(txtOra.Text, ".", ":"))
        'DataOra = Year(txtDataMovimento.Text) & "-" & Month(txtDataMovimento.Text) & "-" & Day(txtDataMovimento.Text) & " " & Replace(txtOra.Text, ".", ":")
        'Response.Write(DataOra & "<br>")

        CmdSalvataggio.CommandText =
                    "insert into movimenti_targa (num_riferimento,id_veicolo,id_tipo_movimento,data_uscita,id_stazione_uscita,km_uscita,serbatoio_uscita, movimento_attivo, data_registrazione, id_operatore) " &
                    "values('" & id_rifornimento.Text & "','" & Session("IdVeicolo") & "','" & Costanti.idMovimentoLavaggio & "','" & DataOra & "','" & id_stazione.Text & "','" & TxtKm.Text & "','" & lblSerbatoioAtt.Text & "','1',GetDate(),'" & Request.Cookies("SicilyRentCar")("idUtente") & "')"

        'Response.Write(CmdSalvataggio.CommandText & "<br>")
        'Response.End()

        CmdSalvataggio.ExecuteNonQuery()

        CmdSalvataggio.CommandText =
                    "update veicoli set disponibile_nolo = 0 WHERE (targa = '" & LblTargaTesto.Text & "')"
        'Response.Write(CmdSalvataggio.CommandText & "<br>")
        'Response.End()

        CmdSalvataggio.ExecuteNonQuery()
        CmdSalvataggio.Dispose()
        DbcSalvataggio.Close()
        DbcSalvataggio = Nothing
    End Sub

    Protected Sub salvaDatiMovimento2()
        Dim DataOra As String

        Dim DbcSalvataggio As New Data.SqlClient.SqlConnection(SqlSalvaDati.ConnectionString)
        DbcSalvataggio.Open()
        Dim CmdSalvataggio As New Data.SqlClient.SqlCommand("", DbcSalvataggio)

        'DataOra = txtDataMovimento.Text & " " & txtOra.Text        

        DataOra = funzioni_comuni.getDataDb_con_orario(txtDataMovimento.Text & " " & Replace(txtOra.Text, ".", ":"))
        'DataOra = Year(txtDataMovimento.Text) & "-" & Month(txtDataMovimento.Text) & "-" & Day(txtDataMovimento.Text) & " " & Replace(txtOra.Text, ".", ":")
        'Response.Write(DataOra & "<br>")

        'CmdSalvataggio.CommandText = _
        '"insert into movimenti_targa (num_riferimento,id_veicolo,id_tipo_movimento,data_uscita,id_stazione_uscita,km_uscita,serbatoio_uscita, movimento_attivo, data_registrazione, id_operatore) " & _
        '"values('" & id_rifornimento.Text & "','" & Session("IdVeicolo") & "','" & Costanti.idMovimentoLavaggio & "','" & DataOra & "','" & id_stazione.Text & "','" & TxtKm.Text & "','" & Session("SerbatoioOut") & "','1',GetDate(),'" & Request.Cookies("SicilyRentCar")("idUtente") & "')"

        'Response.Write(CmdSalvataggio.CommandText & "<br>")
        'Response.End()

        'CmdSalvataggio.ExecuteNonQuery()

        CmdSalvataggio.CommandText =
                    "update veicoli set disponibile_nolo = 1 WHERE (targa = '" & LblTargaTesto.Text & "')"

        'Response.Write(CmdSalvataggio.CommandText & "<br>")
        'Response.End()

        CmdSalvataggio.ExecuteNonQuery()
        CmdSalvataggio.Dispose()
        DbcSalvataggio.Close()
        DbcSalvataggio = Nothing

        Session("StatoDelVeicolo") = "Lavato"
    End Sub

    Protected Sub AggiornaDatiMovimento()
        Dim DataOra As String

        'Prelevo Informazioni dalle tabelle Rifornimenti, Veicoli e Modelli
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim SqlMovimento As String
        SqlMovimento = "SELECT lavaggi.id_veicolo, lavaggi.data_uscita_parco, MODELLI.capacita_serbatoio,veicoli.da_rifornire,veicoli.disponibile_nolo " &
                            "FROM lavaggi INNER JOIN " &
                            "veicoli ON lavaggi.id_veicolo = veicoli.id INNER JOIN " &
                            "MODELLI ON veicoli.id_modello = MODELLI.ID_MODELLO " &
                            "where lavaggi.id='" & id_rifornimento.Text & "'"

        Dim Cmd As New Data.SqlClient.SqlCommand(SqlMovimento, Dbc)
        'Response.Write(Cmd.CommandText & "<br><br>")
        'Response.End()

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()
        Do While Rs.Read

            'Prelevo Record da Movimento Targa per aggiornamento
            Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc2.Open()

            Dim SqlMovimento2 As String
            'DataOra = Year(Rs("data_uscita_parco")) & "-" & Month(Rs("data_uscita_parco")) & "-" & Day(Rs("data_uscita_parco")) & " " & Hour(Rs("data_uscita_parco")) & ":" & Minute(Rs("data_uscita_parco"))
            'DataOra = Year(Rs("data_uscita_parco")) & "-" & Day(Rs("data_uscita_parco")) & "-" & Month(Rs("data_uscita_parco")) & " " & Hour(Rs("data_uscita_parco")) & ":" & Minute(Rs("data_uscita_parco"))
            'DataOra = funzioni_comuni.getDataDb_con_orario(txtDataMovimento.Text & " " & Replace(txtOra.Text, ".", ":"))

            'SqlMovimento2 = "select id from movimenti_targa WITH(NOLOCK) where id_tipo_movimento = " & Costanti.idMovimentoRifornimento & " and id_veicolo ='" & Rs("id_veicolo") & "' and data_uscita ='" & DataOra & "'"
            SqlMovimento2 = "select id from movimenti_targa WITH(NOLOCK) where num_riferimento = '" & id_rifornimento.Text & "'"

            Dim Cmd2 As New Data.SqlClient.SqlCommand(SqlMovimento2, Dbc2)
            'Response.Write(Cmd2.CommandText & "<br><br>")
            'Response.End()
            Dim Rs2 As Data.SqlClient.SqlDataReader
            Rs2 = Cmd2.ExecuteReader()
            Do While Rs2.Read

                'Salvataggio Movimento
                Dim DbcSalvataggio As New Data.SqlClient.SqlConnection(SqlSalvaDati.ConnectionString)
                DbcSalvataggio.Open()
                Dim CmdSalvataggio As New Data.SqlClient.SqlCommand("", DbcSalvataggio)

                DataOra = Year(txtDataMovimento.Text) & "-" & Day(txtDataMovimento.Text) & "-" & Month(txtDataMovimento.Text) & " " & Replace(txtOra.Text, ".", ":")



                CmdSalvataggio.CommandText =
                            "update movimenti_targa set data_rientro ='" & DataOra & "', id_stazione_rientro = '" & id_stazione.Text & "', km_rientro ='" & TxtKm.Text & "', serbatoio_rientro = serbatoio_uscita, movimento_attivo='0', " &
                            "data_registrazione_rientro=GetDate(),id_operatore_rientro='" & Request.Cookies("SicilyRentCar")("idUtente") & "'" &
                            "where id ='" & Rs2("id") & "'"

                'Response.Write(CmdSalvataggio.CommandText & "<br><br>")
                'Response.End()

                CmdSalvataggio.ExecuteNonQuery()
                CmdSalvataggio.Dispose()
                DbcSalvataggio.Close()
                DbcSalvataggio = Nothing

                'Rimetto disponibilità Veicolo per noleggio
                Dim DbcSalvataggio2 As New Data.SqlClient.SqlConnection(SqlSalvaDati.ConnectionString)
                DbcSalvataggio2.Open()
                Dim CmdSalvataggio2 As New Data.SqlClient.SqlCommand("", DbcSalvataggio2)

                'MARCO: ERRORE - DEVE ESSERE SETTATO DISPONIBILE_NOLO SOLAMENTE SE NON E' ANCHE DA LAVARE

                'Response.Write(Rs("da_rifornire"))
                'Response.End()

                Dim disponibile_nolo As String
                If Rs("da_rifornire") = "1" Then
                    disponibile_nolo = "'0'"
                Else
                    disponibile_nolo = "'1'"
                End If

                CmdSalvataggio2.CommandText = _
                            "update veicoli set disponibile_nolo=" & disponibile_nolo & ", da_lavare ='0', km_attuali='" & TxtKm.Text & "' " & _
                            "where id ='" & Rs("id_veicolo") & "'"


                'Response.Write(CmdSalvataggio2.CommandText & "<br><br>")
                'Response.End()

                CmdSalvataggio2.ExecuteNonQuery()
                CmdSalvataggio2.Dispose()
                DbcSalvataggio2.Close()
                DbcSalvataggio2 = Nothing
            Loop

            Cmd2.Dispose()
            Cmd2 = Nothing

            Rs2.Close()
            Rs2.Dispose()
            Rs2 = Nothing

            Dbc2.Close()
            Dbc2.Dispose()
            Dbc2 = Nothing
        Loop

        Cmd.Dispose()
        Cmd = Nothing

        Rs.Close()
        Rs.Dispose()
        Rs = Nothing

        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub


    

    'Protected Sub btnVediFornitore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVediFornitore.Click
    '    tab_pagina.Visible = False
    '    tab_fornitore.Visible = True
    'End Sub

    Protected Sub btnAnnullaFornitore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnullaFornitore.Click
        txtNuovoFornitore.Text = ""

        tab_pagina.Visible = True
        tab_fornitore.Visible = False
    End Sub

    Protected Function fornitore_non_esistente(ByVal fornitore As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        'SELEZIONO L'ID DEL CONTRATTO
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM alimentazione_fornitori_x_stazione WITH(NOLOCK) WHERE descrizione='" & Replace(fornitore, "'", "''") & "' AND id_stazione='" & id_stazione.Text & "'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test = "" Then
            fornitore_non_esistente = True
        Else
            fornitore_non_esistente = False
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub salvaFornitore(ByVal fornitore As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO alimentazione_fornitori_x_stazione (descrizione, id_stazione) VALUES ('" & Replace(fornitore, "'", "''") & "','" & id_stazione.Text & "')", Dbc)

        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    'Protected Sub btnSalvaFornitore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvaFornitore.Click
    '    If Trim(txtNuovoFornitore.Text) <> "" Then
    '        If fornitore_non_esistente(Trim(txtNuovoFornitore.Text)) Then
    '            salvaFornitore(Trim(txtNuovoFornitore.Text))

    '            Libreria.genUserMsgBox(Me, "Fornitore memorizzato correttamente.")

    '            txtNuovoFornitore.Text = ""
    '            tab_pagina.Visible = True
    '            tab_fornitore.Visible = False

    '            DDLFornitore.Items.Clear()
    '            DDLFornitore.Items.Add("Seleziona...")
    '            DDLFornitore.Items(0).Value = "0"
    '            DDLFornitore.DataBind()
    '        Else
    '            Libreria.genUserMsgBox(Me, "Fornitore già specificato.")
    '        End If
    '    Else
    '        Libreria.genUserMsgBox(Me, "Specificare il nome del fornitore.")
    '    End If
    'End Sub
End Class
