
Imports Costanti
Imports System.IO

Partial Class rifornimenti
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        id_rifornimento.Text = Request("rifornimento")




        If Not Page.IsPostBack Then
            tab_fornitore.Visible = False

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


                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                Dim Cmd As New Data.SqlClient.SqlCommand("select * from rifornimenti WITH(NOLOCK) where id =" & id_rifornimento.Text, Dbc)
                'Response.Write(Cmd.CommandText & "<br><br>")
                'Response.End()

                Dim Rs As Data.SqlClient.SqlDataReader
                Rs = Cmd.ExecuteReader()
                If Rs.Read() Then
                    id_stazione.Text = get_id_stazione_veicolo(Rs("id_veicolo"))
                    stazione_out.Text = get_codice_stazione_uscita(Rs("id_veicolo"))

                    If (Rs("data_uscita_parco") & "") = "" And (Rs("data_rientro_parco") & "") = "" Then
                        'Da Rifornire
                        id_stazione_x_fornitori.Text = get_id_stazione_veicolo(Rs("id_veicolo"))
                        LblDataMovimento.Text = "Data/Ora Uscita dal Parco"

                        LblConducenti.Visible = True
                        DDLConducenti.Visible = True
                        StatoVeicolo = "Da Rifornire"

                        btnVediFornitore.Visible = False

                        ListViewMovimenti.Visible = False

                    ElseIf (Rs("data_uscita_parco") & "") <> "" And (Rs("data_rientro_parco") & "") = "" Then
                        'In Rifornimento
                        id_stazione_x_fornitori.Text = get_id_stazione_veicolo(Rs("id_veicolo"))
                        lblNumeroRifornimento.Text = Right(Rs("anno_rifornimento") & "", 2) & "/" & stazione_out.Text & "-" & Rs("num_rifornimento") & ""

                        LblDataMovimento.Text = "Data/Ora Rientro nel Parco"
                        TxtKm.Enabled = True
                        LblImporto.Visible = True
                        LblImporto.Text = "Importo"
                        TxtImporto.Visible = True
                        StatoVeicolo = "In Rifornimento"
                        lblLitriRiforniti.Visible = True
                        txtLitriRiforniti.Enabled = True
                        txtLitriRiforniti.Visible = True
                        lblFornitore.Visible = True
                        DDLFornitore.Visible = True

                        btnVediFornitore.Visible = True

                        LblConducenti.Visible = True
                        DDLConducenti.Visible = True

                        If Rs("id_conducente") & "" <> "" Then
                            DDLConducenti.SelectedValue = Rs("id_conducente")
                            DDLConducenti.Enabled = False
                        End If

                        RFLitriRiforniti.Enabled = True

                        ListViewMovimenti.Visible = False
                    Else
                        'Rifornito        
                        id_stazione_x_fornitori.Text = Rs("id_stazione_out")
                        lblNumeroRifornimento.Text = Right(Rs("anno_rifornimento") & "", 2) & "/" & stazione_out.Text & "-" & Rs("num_rifornimento") & ""

                        Dim data1 As String = funzioni_comuni.getDataDb_senza_orario2(Rs("data_rientro_parco"))

                        Dim Ora1 As String = funzioni_comuni.getDataDb_orario_senza_data(Rs("data_rientro_parco"))

                        Id = Rs("id")
                        Id_Veicolo = Rs("Id_Veicolo")
                        Data_Rientro = Libreria.FormattaDataOreMinSecProduzione(Rs("data_rientro_parco"))

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

                        lblCostoAlLitro.Visible = True

                        lblLitriRiforniti.Visible = True
                        txtLitriRiforniti.Visible = True

                        lblFornitore.Visible = True
                        DDLFornitore.Visible = True

                        btnVediFornitore.Visible = False

                        LblConducenti.Visible = True
                        DDLConducenti.Visible = True

                        If Rs("id_conducente") & "" <> "" Then
                            DDLConducenti.SelectedValue = Rs("id_conducente")
                            DDLConducenti.Enabled = False
                        End If

                        If Rs("litri_riforniti") = "0" Then
                            txtLitriRiforniti.Enabled = True
                        Else
                            txtLitriRiforniti.Enabled = False
                        End If

                        LblImporto.Visible = True

                        If (Rs("litri_riforniti") & "") <> "0" Then
                            txtLitriRiforniti.Text = Rs("litri_riforniti")
                        End If

                        If (Rs("importo_rifornimento") & "") <> "" Then
                            TxtImporto.Text = Rs("importo_rifornimento") & ""
                        End If

                        If txtLitriRiforniti.Text <> "" And TxtImporto.Text <> "" Then
                            'CALCOLO DEL COSTO AL LITRO
                            Try
                                lblCostoLitro.Text = FormatNumber(CDbl(TxtImporto.Text) / CDbl(txtLitriRiforniti.Text), 2, , , TriState.False)
                            Catch ex As Exception
                                lblCostoAlLitro.Text = ""
                            End Try
                        Else
                            lblCostoLitro.Text = ""
                        End If


                        TxtImporto.Visible = True
                        If TxtImporto.Text <> "" Then
                            TxtImporto.Enabled = False
                        Else
                            TxtImporto.Enabled = True
                        End If

                        DDLFornitore.DataBind()

                        Try
                            DDLFornitore.SelectedValue = Rs("id_fornitore")
                        Catch ex As Exception
                            DDLFornitore.SelectedValue = "0"
                        End Try

                        DDLFornitore.Enabled = False

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

                        StatoVeicolo = "Rifornito"



                        'PER UN RIFORNIMENTO EFFETTUATO POTREBBE ESSERE NECESSARIO SPECIFICARE ANCORA L'IMPORTO  OPPURE IL FORNITORE
                        If (Rs("importo_rifornimento") & "") = "" Then
                            LblImporto.Visible = True
                            LblImporto.Text = "Importo"
                            TxtImporto.Visible = True
                            StatoVeicolo = "Inserire Importo"

                            lblCostoAlLitro.Visible = True

                            lblLitriRiforniti.Visible = True
                            txtLitriRiforniti.Visible = True

                            LblConducenti.Visible = True
                            DDLConducenti.Visible = True

                            DDLFornitore.Visible = True
                            lblFornitore.Visible = True

                            BtnSalva.Visible = True

                            RFImporto.Enabled = True
                        End If

                        Dim DbcRifornimentoPrec As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        DbcRifornimentoPrec.Open()

                        Dim SqlRifornimentoPrec As String
                        SqlRifornimentoPrec = "SELECT id, id_veicolo, data_uscita_parco, km_out, data_rientro_parco, km_in, data_rifornimento, importo_rifornimento, litri_riforniti, serbatoio, costo_benzina_litro, " &
                                                    "id_conducente, id_stazione_out, id_stazione_in, id_tipologia, riferimento, id_operatore_apertura, data_operazione_apertura, id_operatore_chiusura, " &
                                                    "data_operazione_chiusura, registrato_in_contratto_id " &
                                              "FROM rifornimenti WITH (NOLOCK) " &
                                                    "WHERE (id < " & Id & ") AND (id_veicolo = " & Id_Veicolo & ")"


                        Dim CmdRifornimentoPrec As New Data.SqlClient.SqlCommand(SqlRifornimentoPrec, DbcRifornimentoPrec)
                        'Response.Write(CmdRifornimentoPrec.CommandText & "<br><br>")
                        'Response.End()

                        Dim RsRifornimentoPrec As Data.SqlClient.SqlDataReader
                        RsRifornimentoPrec = CmdRifornimentoPrec.ExecuteReader()

                        Do While RsRifornimentoPrec.Read
                            If (RsRifornimentoPrec("data_rientro_parco") & "") <> "" Then
                                Data_Rientro2 = Libreria.FormattaDataOreMinSecProduzione(RsRifornimentoPrec("data_rientro_parco"))
                            End If
                        Loop

                        CmdRifornimentoPrec.Dispose()
                        CmdRifornimentoPrec = Nothing

                        RsRifornimentoPrec.Close()
                        RsRifornimentoPrec.Dispose()
                        RsRifornimentoPrec = Nothing

                        DbcRifornimentoPrec.Close()
                        DbcRifornimentoPrec.Dispose()
                        DbcRifornimentoPrec = Nothing


                    End If
                    IdAus = Rs("Id_veicolo")
                    SerbatoioOut = Rs("serbatoio")


                    'SqlMovimenti.SelectCommand = "SELECT movimenti_targa.id, movimenti_targa.num_riferimento, tipologia_movimenti.descrzione, veicoli.targa " & _
                    '"FROM movimenti_targa INNER JOIN " & _
                    '"tipologia_movimenti ON movimenti_targa.id_tipo_movimento = tipologia_movimenti.id INNER JOIN " & _
                    '"veicoli ON movimenti_targa.id_veicolo = veicoli.id " & _
                    '"WHERE (movimenti_targa.id_veicolo = '" & Rs("id_veicolo") & "') "

                    'SqlMovimenti.SelectCommand = "SELECT movimenti_targa.id, movimenti_targa.num_riferimento, veicoli.targa, tipologia_movimenti.descrzione AS tipo_movimento, " & _
                    '                                "stazioni2.codice + ' ' + stazioni2.nome_stazione AS stazione_rientro " & _
                    '                              "FROM movimenti_targa INNER JOIN " & _
                    '                                "veicoli ON movimenti_targa.id_veicolo = veicoli.id LEFT OUTER JOIN " & _
                    '                                "stazioni AS stazioni1 ON movimenti_targa.id_stazione_uscita = stazioni1.id LEFT OUTER JOIN " & _
                    '                                "stazioni AS stazioni2 ON movimenti_targa.id_stazione_rientro = stazioni2.id INNER JOIN " & _
                    '                                "tipologia_movimenti ON movimenti_targa.id_tipo_movimento = tipologia_movimenti.id INNER JOIN " & _
                    '                                "MODELLI ON veicoli.id_modello = MODELLI.ID_MODELLO " & _
                    '                            "WHERE (movimenti_targa.id > 0) AND (movimenti_targa.data_rientro BETWEEN '2012-9-18 10:10:00' AND '2012-9-18 10:55:00') AND " & _
                    '                                "(movimenti_targa.id_tipo_movimento = 0 OR " & _
                    '                                "movimenti_targa.id_tipo_movimento = '3' OR " & _
                    '                                "movimenti_targa.id_tipo_movimento = '4' OR " & _
                    '                                "movimenti_targa.id_tipo_movimento = '8') AND (movimenti_targa.id_veicolo = '37595') "

                    SqlMovimenti.SelectCommand = " SELECT movimenti_targa.num_riferimento, veicoli.targa, tipologia_movimenti.descrzione, " &
                                                    "SUM(serbatoio_rientro-serbatoio_uscita) As differenza_litri, id_tipo_movimento, MAX(movimenti_targa.id) As id " &
                                                    "FROM movimenti_targa WITH(NOLOCK) " &
                                                    "INNER JOIN veicoli WITH(NOLOCK) ON movimenti_targa.id_veicolo = veicoli.id  " &
                                                    "INNER JOIN tipologia_movimenti WITH(NOLOCK) ON movimenti_targa.id_tipo_movimento = tipologia_movimenti.id  " &
                                                    " WHERE (movimenti_targa.id_tipo_movimento = 0 OR " &
                                                    "movimenti_targa.id_tipo_movimento = '" & Costanti.idMovimentoNoleggio & "' OR " &
                                                    "movimenti_targa.id_tipo_movimento = '" & Costanti.idFermoTecnico & "' OR " &
                                                    "movimenti_targa.id_tipo_movimento = '" & Costanti.idMovimentoInterno & "' OR " &
                                                    "movimenti_targa.id_tipo_movimento = '" & Costanti.idMovimentoODL & "' OR " &
                                                    "movimenti_targa.id_tipo_movimento = '" & Costanti.idMovimentoFurto & "') AND (movimenti_targa.id_veicolo = '" & Id_Veicolo & "') " &
                                                    "AND (movimenti_targa.data_rientro BETWEEN convert(datetime,'" & Data_Rientro2 & "',102) AND convert(datetime,'" & Data_Rientro & "',102)) " &
                                                    "AND movimenti_targa.serbatoio_uscita<>movimenti_targa.serbatoio_rientro " &
                                                    "AND movimenti_targa.serbatoio_rientro <> " & serb.Text &
                                                    " GROUP BY movimenti_targa.num_riferimento,  veicoli.targa, tipologia_movimenti.descrzione,id_tipo_movimento " &
                                                    " ORDER BY id DESC "




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

                    Response.Redirect("elenco_rifornimenti.aspx")
                End If

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
                SqlVeicolo = "SELECT  veicoli.id,veicoli.targa, veicoli.id_stazione, ISNULL(veicoli.serbatoio_attuale,0) As serbatoio_attuale, ISNULL(modelli.capacita_serbatoio,0) As capacita_serbatoio, MODELLI.descrizione AS modello, marche.descrizione AS marca,  veicoli.km_attuali, modelli.TipoCarburante, veicoli.da_lavare " &
                                 "FROM veicoli WITH(NOLOCK) INNER JOIN " &
                                 "MODELLI WITH(NOLOCK) ON veicoli.id_modello = MODELLI.ID_MODELLO INNER JOIN " &
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

                    If (Rs2("da_lavare") & "") <> "" Then
                        If Rs2("da_lavare") Then
                            da_lavare.Text = "1"
                        Else
                            da_lavare.Text = "0"
                        End If
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
                Session("SerbatoioOut") = SerbatoioOut

            Else
                Response.Redirect("default.aspx")
            End If


            Aggiorna_Allegati(id_rifornimento.Text)     'inserito - salvo 16.12.2022

            If DDLFornitore.Visible = True Then
                tb_allegati.Visible = True
            Else
                tb_allegati.Visible = False
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
    Protected Function get_codice_stazione_uscita(ByVal id_veicolo As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        'Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_stazione FROM veicoli WHERE id='" & id_veicolo & "'", Dbc)
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT stazioni.codice FROM stazioni INNER JOIN veicoli ON stazioni.id = veicoli.id_stazione WHERE veicoli.id='" & id_veicolo & "'", Dbc)

        get_codice_stazione_uscita = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub BtnChiudi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnChiudi.Click
        'LblSession.Text = Session("rifornumento_stazione") & "-" & Session("rifornumento_targa") & "-" & Session("rifornumento_telaio") & "-" & Session("rifornumento_marca") & "-" & Session("rifornumento_modello") & "-" & Session("rifornumento_stato")
        'LblSession.Visible = True
        Response.Redirect("elenco_rifornimenti.aspx")
    End Sub

    Protected Function is_stato_da_rifornire(ByVal id_rif As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM rifornimenti WITH(NOLOCK) WHERE data_uscita_parco IS NULL AND id='" & id_rif & "'", Dbc)

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

    Protected Function getKmAttuali(ByVal targa As String) As Integer
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(km_attuali,0) As km_attuali FROM veicoli WITH(NOLOCK) WHERE targa='" & targa & "'", Dbc)

        getKmAttuali = Cmd.ExecuteScalar & ""


        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub BtnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSalva.Click
        'CONTROLLO CHE MI TROVO NELLO STATO CORRETTO (L'UTENTE PUO' TORNARE INDIETRO COL BROWSER)
        Try
            If LblStatoTesto.Text <> "Da Rifornire" Or (LblStatoTesto.Text = "Da Rifornire" AndAlso is_stato_da_rifornire(id_rifornimento.Text)) Then
                Dim test_km As Boolean

                If LblStatoTesto.Text = "In Rifornimento" Then
                    If TxtKm.Text <> "" Then
                        Dim km_attuali As Integer = getKmAttuali(LblTargaTesto.Text)
                        If CInt(TxtKm.Text) < km_attuali Then
                            test_km = False
                        Else
                            test_km = True
                        End If
                    Else
                        test_km = False
                    End If
                Else
                    test_km = True
                End If



                'NEL CASO IN CUI LO STATO SIA "IN RIFORNIMENTO" CONTROLLO LA CORRETTEZZA DEL CAMPO Litri_riforniti 
                Dim test_litri As Boolean

                If LblStatoTesto.Text = "Da Rifornire" Then
                    test_litri = True
                Else
                    If CDbl(txtLitriRiforniti.Text) <= CDbl(lblSerbatoioMax.Text) Then
                        test_litri = True
                    Else
                        test_litri = False
                    End If
                End If

                Dim uscita As DateTime
                Dim oggi As DateTime

                Dim dati_ok As Boolean

                Try
                    uscita = funzioni_comuni.getDataDb_con_orario2(txtDataMovimento.Text & " " & txtOra.Text)
                    oggi = funzioni_comuni.getDataDb_con_orario2(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()) & " 00:00:00")

                    dati_ok = True
                Catch
                    dati_ok = False
                End Try

                If dati_ok Then
                    If test_km Then
                        'Libreria.genUserMsgBox(Me, DateDiff(DateInterval.Minute, Now(), uscita))
                        If LblStatoTesto.Text <> "Da Rifornire" Or (LblStatoTesto.Text = "Da Rifornire" And DateDiff(DateInterval.Minute, Now(), uscita) <= 0) Then
                            If LblStatoTesto.Text <> "Da Rifornire" Or (LblStatoTesto.Text = "Da Rifornire" And DateDiff(DateInterval.Day, oggi, uscita) > -2) Then
                                If test_litri Then
                                    salvaDati()

                                    If Session("StatoDelVeicolo") = "Da Rifornire" Then
                                        salvaDatiMovimento()
                                    End If

                                    'Response.Write(Session("StatoDelVeicolo") & "<br><br>")        
                                    If Session("StatoDelVeicolo") = "In Rifornimento" Then
                                        AggiornaDatiMovimento()
                                    End If

                                    Session("StatoDelVeicolo") = ""
                                    Session("IdVeicolo") = ""


                                    If LblStatoTesto.Text = "Da Rifornire" Or LblStatoTesto.Text = "In Rifornimento" Or LblStatoTesto.Text = "Inserire Importo" Then
                                        Response.Redirect("rifornimenti.aspx?rifornimento=" & id_rifornimento.Text & "&serb=" & lblSerbatoioMax.Text)
                                    Else
                                        Response.Redirect("elenco_rifornimenti.aspx")
                                    End If

                                Else
                                    Libreria.genUserMsgBox(Me, "Attenzione: i litri riforniti non possono superare il serbatoio massimo.")
                                End If
                            Else
                                Libreria.genUserMsgBox(Me, "Attenzione: è possibile registrare rifornimenti entro 3 giorni dalla data odierna.")
                            End If
                        Else
                            Libreria.genUserMsgBox(Me, "Attenzione: la data del rifornimento non può essere successiva al momento del salvataggio.")
                        End If
                    Else
                        Libreria.genUserMsgBox(Me, "Attenzione: i KM specificati sono inferiori a quelli attuali del veicolo.")
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Specificare correttamente i dati.")
                End If
            Else
                Libreria.genUserMsgBox(Me, "Attenzione: è stata già registrata l'uscita per rifornimento del veicolo oppure è stato appena effettuato un check out di questa vettura. Tornare nella schermata dei rifornimenti e caricare nuovamente questo rifornimento.")
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error btnSalvaClick  : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try

        'aggiunto salvo 11.02.2023
        If DDLFornitore.Visible = True Then
            tb_allegati.Visible = True
        Else
            tb_allegati.Visible = False

        End If



    End Sub

    Protected Sub salvaDati()
        Dim sqla As String = ""
        Try
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
                Case Is = "Da Rifornire"






                    'GENERAZIONE DEL NUMERO DI RIFORNIMENTO
                    Dim anno As String = Year(txtDataMovimento.Text)
                    Dim numero_rifornimento As String = funzioni_comuni.getNumRifornimento(id_stazione.Text, anno)
                    sqla = "update rifornimenti set anno_rifornimento='" & anno & "', num_rifornimento='" & numero_rifornimento & "', data_uscita_parco =convert(datetime,'" & DataOra & "',102), km_out ='" & TxtKm.Text & "', id_conducente ='" & DDLConducenti.SelectedValue & "', id_stazione_out ='" & id_stazione.Text & "'," &
                        "id_operatore_apertura='" & Request.Cookies("SicilyRentCar")("idUtente") & "', data_operazione_apertura=convert(datetime,GetDate(),102) " &
                        " where id = '" & id_rifornimento.Text & "'"
                    CmdSalvataggio.CommandText = sqla

                    lblNumeroRifornimento.Text = Right(anno, 2) & "/" & stazione_out.Text & "-" & numero_rifornimento

                    'salvataggio effettuato
                    'visibile tb_allegati
                    tb_allegati.Visible = True



                Case Is = "In Rifornimento"
                    'L'IMPORTO NON E' OBBLIGATORIO IN QUANTO PUO' NON ESSERE NOTO AL MOMENTO DELLA REGISTRAZIONE - PUO' ESSERE SPECIFICATO SUCCESSIVAMENTE
                    Dim importo As String
                    If TxtImporto.Text = "" Then
                        importo = "NULL"
                    Else
                        importo = "'" & Replace(TxtImporto.Text, ",", ".") & "'"
                    End If
                    sqla = "update rifornimenti set data_rientro_parco =convert(datetime,'" & DataOra & "',102), data_rifornimento =convert(datetime,'" & DataOra & "',102), km_in ='" & TxtKm.Text & "', importo_rifornimento =" & importo & ", id_stazione_in ='" & id_stazione.Text & "', id_conducente ='" & DDLConducenti.SelectedValue & "',id_fornitore='" & DDLFornitore.SelectedValue & "'," &
                       "id_operatore_chiusura='" & Request.Cookies("SicilyRentCar")("idUtente") & "', data_operazione_chiusura=convert(datetime,GetDate(),102),litri_riforniti='" & Replace(txtLitriRiforniti.Text, ",", ".") & "', costo_benzina_litro='" & Replace(funzioni_comuni.getCostoCarburante_x_litro(id_stazione.Text, id_alimentazione.Text), ",", ".") & "' " &
                       " where id = '" & id_rifornimento.Text & "'"
                    CmdSalvataggio.CommandText = sqla

                Case Is = "Inserire Importo"
                    'SALVATAGGIO DEL SOLO IMPORTO A MOVIMENTO GIA' REGISTRATO E CHIUSO
                    Dim importo As String
                    importo = Replace(TxtImporto.Text, ",", ".")
                    sqla = "UPDATE rifornimenti SET id_conducente ='" & DDLConducenti.SelectedValue & "', litri_riforniti='" & Replace(txtLitriRiforniti.Text, ",", ".") & "', importo_rifornimento='" & importo & "' where id = '" & id_rifornimento.Text & "'"
                    CmdSalvataggio.CommandText = sqla

                Case Is = "Rifornito"

            End Select

            'Response.Write(CmdSalvataggio.CommandText & "<br><br>")
            'Response.Write(Session("StatoDelVeicolo") & "<br><br>")
            'Response.End()
            CmdSalvataggio.ExecuteNonQuery()
            CmdSalvataggio.Dispose()
            DbcSalvataggio.Close()
            DbcSalvataggio = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error salvaDati : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")

        End Try

    End Sub

    Protected Sub salvaDatiMovimento()
        Dim sqla As String = ""
        Try
            Dim DataOra As String

            Dim DbcSalvataggio As New Data.SqlClient.SqlConnection(SqlSalvaDati.ConnectionString)
            DbcSalvataggio.Open()
            Dim CmdSalvataggio As New Data.SqlClient.SqlCommand("", DbcSalvataggio)

            'DataOra = txtDataMovimento.Text & " " & txtOra.Text        

            DataOra = funzioni_comuni.getDataDb_con_orario(txtDataMovimento.Text & " " & Replace(txtOra.Text, ".", ":"))
            'DataOra = Year(txtDataMovimento.Text) & "-" & Month(txtDataMovimento.Text) & "-" & Day(txtDataMovimento.Text) & " " & Replace(txtOra.Text, ".", ":")
            'Response.Write(DataOra & "<br>")
            sqla = "insert into movimenti_targa (num_riferimento,id_veicolo,id_tipo_movimento,data_uscita,id_stazione_uscita,km_uscita,serbatoio_uscita, movimento_attivo, data_registrazione, id_operatore) " &
                        "values('" & id_rifornimento.Text & "','" & Session("IdVeicolo") & "','" & Costanti.idMovimentoRifornimento & "',convert(datetime,'" & DataOra & "',102),'" & id_stazione.Text & "','" & TxtKm.Text & "','" & Session("SerbatoioOut") & "','1',convert(datetime,GetDate(),102),'" & Request.Cookies("SicilyRentCar")("idUtente") & "')"

            CmdSalvataggio.CommandText = sqla

            'Response.Write(CmdSalvataggio.CommandText & "<br>")
            'Response.End()

            CmdSalvataggio.ExecuteNonQuery()
            sqla = "update veicoli set disponibile_nolo = 0, da_rifornire = 1 WHERE (targa = '" & LblTargaTesto.Text & "')"
            CmdSalvataggio.CommandText = sqla

            'Response.Write(CmdSalvataggio.CommandText & "<br>")
            'Response.End()

            CmdSalvataggio.ExecuteNonQuery()
            CmdSalvataggio.Dispose()
            DbcSalvataggio.Close()
            DbcSalvataggio = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error salvaDatiMovimento : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Protected Sub AggiornaDatiMovimento()

        Dim DataOra As String

        Try
            'Prelevo Informazioni dalle tabelle Rifornimenti, Veicoli e Modelli
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim SqlMovimento As String
            SqlMovimento = "SELECT rifornimenti.id_veicolo, rifornimenti.data_uscita_parco, MODELLI.capacita_serbatoio,veicoli.disponibile_nolo " &
                                "FROM rifornimenti INNER JOIN " &
                                "veicoli ON rifornimenti.id_veicolo = veicoli.id INNER JOIN " &
                                "MODELLI ON veicoli.id_modello = MODELLI.ID_MODELLO " &
                                "where rifornimenti.id='" & id_rifornimento.Text & "'"

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

                    'aggiornato 21.01.2021 invertito day/month
                    DataOra = Year(txtDataMovimento.Text) & "-" & Month(txtDataMovimento.Text) & "-" & Day(txtDataMovimento.Text) & " " & Replace(txtOra.Text, ".", ":")



                    CmdSalvataggio.CommandText =
                                "update movimenti_targa set data_rientro =convert(datetime,'" & DataOra & "',102), id_stazione_rientro = '" & id_stazione.Text & "', km_rientro ='" & TxtKm.Text & "', serbatoio_rientro ='" & Rs("capacita_serbatoio") & "', movimento_attivo='0', " &
                                "data_registrazione_rientro=convert(datetime,GetDate(),102),id_operatore_rientro='" & Request.Cookies("SicilyRentCar")("idUtente") & "'" &
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
                    Dim disponibile_nolo As String
                    If da_lavare.Text = "1" Then
                        disponibile_nolo = "'0'"
                    Else
                        disponibile_nolo = "'1'"
                    End If

                    CmdSalvataggio2.CommandText =
                                "update veicoli set da_rifornire='0', km_attuali='" & TxtKm.Text & "', serbatoio_attuale='" & Rs("capacita_serbatoio") & "' " &
                                "where id ='" & Rs("id_veicolo") & "'"
                    CmdSalvataggio2.ExecuteNonQuery()

                    CmdSalvataggio2.CommandText =
                               "update veicoli set disponibile_nolo=" & disponibile_nolo & " " &
                               "where id ='" & Rs("id_veicolo") & "' AND venduta='0' AND in_vendita='0' AND venduta_da_fattura='0'"
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
        Catch ex As Exception
            HttpContext.Current.Response.Write("error AggiornaDatiMovimento  : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try


    End Sub

    Protected Sub ListViewMovimenti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles ListViewMovimenti.ItemDataBound
        'SE IL MOVIMENTO SI RIFERISCE AD UN CONTRATTO OTTENGO GLI IMPORTI ADDEBITATI AL CLIENTE RIGUARDANTI LA BENZINA
        Dim id_tipo_movimento As Label = e.Item.FindControl("id_tipo_movimento")
        If id_tipo_movimento.Text = Costanti.idMovimentoNoleggio Then
            Dim num_riferimento As HyperLink = e.Item.FindControl("num_riferimento")

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            'SELEZIONO L'ID DEL CONTRATTO
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT contratti.id, contratti.num_calcolo, veicoli.targa  FROM contratti WITH(NOLOCK) LEFT JOIN veicoli WITH(NOLOCK) ON contratti.id_veicolo=veicoli.id WHERE num_contratto='" & num_riferimento.Text & "' AND attivo='1'", Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            Dim id_contratto As String = ""
            Dim num_calcolo As String = ""
            Dim targa_contratto As String = ""

            If Rs.Read() Then
                id_contratto = Rs("id")
                num_calcolo = Rs("num_calcolo")
                targa_contratto = Rs("targa") & "" 'NEL CASO DI CONTRATTO NELLO STATO CRV LA TARGA NON E' SPECIFICATA
            End If

            If id_contratto <> "" Then
                Dim targaLabel As Label = e.Item.FindControl("targaLabel")
                '1) SELEZIONO L'ID DELL'ELEMENTO REFUEL
                Dbc.Close()
                Dbc.Open()

                Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM condizioni_elementi WITH(NOLOCK) WHERE tipologia='REFUEL'", Dbc)
                Dim id_refuel As String = Cmd.ExecuteScalar & ""
                If id_refuel <> "" Then

                    'RECUPERO L'IMPORTO ADDEBITATO AL CLIENTE - ESEGUO LA SOMMA DI PIU' RIGHE IN QUANTO, IN CASO DI CRV, L'ADDEBITO POTREBBE
                    'ESSERE EFFETTUATO PIU' VOLTE - NEL NOME DEL COSTO FIGURA, ALLA FINE, LA TARGA DEL VEICOLO
                    Dim SqlStr As String = "SELECT ISNULL(SUM(ISNULL(imponibile_scontato+iva_imponibile_scontato+ISNULL(imponibile_onere,0) " &
                    " + ISNULL(iva_onere,0),0)),0) FROM contratti_costi WITH(NOLOCK) WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' " &
                    "AND id_elemento='" & id_refuel & "' AND nome_costo LIKE '%" & targaLabel.Text & "' AND ISNULL(omaggiato,'0')='0'"

                    Cmd = New Data.SqlClient.SqlCommand(SqlStr, Dbc)

                    Dim importo As String = Cmd.ExecuteScalar

                    If importo = "" Then
                        importo = "0"
                    End If

                    Dim importo_addebitato As Label = e.Item.FindControl("importo_addebitato")
                    importo_addebitato.Text = FormatNumber(importo, 2, , , TriState.False)
                End If

                '2) 'SELEZIONO L'IMPORTO DEL SERVIZIO RIFORNIMENTO TENENDO CONTO CHE IN CASO DI CRV IL SERVIZIO RIFORNIMENTO SI RIFERISCE
                'UNICAMENTE ALL'ULTIMA TARGA DEL CONTRATTO
                If targa_contratto = targaLabel.Text Then

                    Dim id_servizio As String = funzioni_comuni.get_id_servizio_rifornimento()
                    If id_servizio <> "0" Then

                        Dim SqlStr As String = "SELECT ISNULL(imponibile_scontato+iva_imponibile_scontato+ISNULL(imponibile_onere,0) " &
                        " + ISNULL(iva_onere,0),0) FROM contratti_costi WITH(NOLOCK) WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' " &
                        "AND id_elemento='" & id_servizio & "' AND ISNULL(omaggiato,'0')='0' AND selezionato='1'"

                        Cmd = New Data.SqlClient.SqlCommand(SqlStr, Dbc)

                        Dim importo As String = Cmd.ExecuteScalar

                        If importo = "" Then
                            importo = "0"
                        End If

                        Dim servizio_addebitato As Label = e.Item.FindControl("servizio_addebitato")
                        servizio_addebitato.Text = FormatNumber(importo, 2, , , TriState.False)
                    End If
                Else
                    Dim servizio_addebitato As Label = e.Item.FindControl("servizio_addebitato")
                    servizio_addebitato.Text = "0"
                End If
            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End If

    End Sub

    Protected Sub ListViewMovimenti_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListViewMovimenti.DataBound
        Dim tot_carburante As Double = 0
        Dim tot_servizio As Double = 0

        Dim importo_addebitato As Label
        Dim servizio_addebitato As Label

        Dim entrato As Boolean = False

        For i = 0 To ListViewMovimenti.Items.Count - 1
            entrato = True

            importo_addebitato = ListViewMovimenti.Items(i).FindControl("importo_addebitato")
            If importo_addebitato.Text = "" Then
                importo_addebitato.Text = "0"
            End If
            tot_carburante = tot_carburante + CDbl(importo_addebitato.Text)

            servizio_addebitato = ListViewMovimenti.Items(i).FindControl("servizio_addebitato")
            If servizio_addebitato.Text = "" Then
                servizio_addebitato.Text = "0"
            End If
            tot_servizio = tot_servizio + CDbl(servizio_addebitato.Text)
        Next

        If entrato And TxtImporto.Text <> "" Then
            lblTotSoloRefuel.Text = tot_carburante - CDbl(TxtImporto.Text)
            lblTotRefuelServizio.Text = tot_carburante + tot_servizio - CDbl(TxtImporto.Text)

            riga_totale.Visible = True
        End If
    End Sub

    Protected Sub btnVediFornitore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVediFornitore.Click
        tab_pagina.Visible = False
        tab_fornitore.Visible = True
    End Sub

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

    Protected Sub btnSalvaFornitore_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvaFornitore.Click
        If Trim(txtNuovoFornitore.Text) <> "" Then
            If fornitore_non_esistente(Trim(txtNuovoFornitore.Text)) Then
                salvaFornitore(Trim(txtNuovoFornitore.Text))

                Libreria.genUserMsgBox(Me, "Fornitore memorizzato correttamente.")

                txtNuovoFornitore.Text = ""
                tab_pagina.Visible = True
                tab_fornitore.Visible = False

                DDLFornitore.Items.Clear()
                DDLFornitore.Items.Add("Seleziona...")
                DDLFornitore.Items(0).Value = "0"
                DDLFornitore.DataBind()
            Else
                Libreria.genUserMsgBox(Me, "Fornitore già specificato.")
            End If
        Else
            Libreria.genUserMsgBox(Me, "Specificare il nome del fornitore.")
        End If
    End Sub



    'Salvo 16.12.2022
    Protected Sub Aggiorna_Allegati(id_rifornimento As String)
        'nuovo salvo 22.11.2022


        Dim id_stazione As String = ""
        Dim daData As String = "" 'funzioni_comuni.GetDataSql(tx_DataDa.Text, 0)
        Dim aData As String = "" 'funzioni_comuni.GetDataSql(tx_DataA.Text, 59)

        Dim sqlstr As String

        sqlstr = "SELECT rifornimenti_allegati.id_allegato, rifornimenti_allegati.DataCreazione, rifornimenti_allegati.DataInserimento, rifornimenti_allegati.NomeFile, rifornimenti_allegati.PercorsoFile, rifornimenti_allegati.Id_Stazione, " &
                  "rifornimenti_allegati.Id_Operatore, rifornimenti_allegati.notes, rifornimenti_allegati.idTipoDocumento, rifornimenti_allegati.nome_file_operatore, rifornimenti_allegati.id_rifornimento, rifornimenti_TipoAllegato.TipoAllegato as descrizione, " &
                  "rifornimenti_TipoAllegato.sigla, operatori.cognome + ' ' + operatori.nome as operatore " &
                    "From rifornimenti_allegati INNER JOIN rifornimenti_TipoAllegato ON rifornimenti_allegati.idTipoDocumento = rifornimenti_TipoAllegato.Id INNER JOIN " &
                    "operatori ON rifornimenti_allegati.Id_Operatore = operatori.id " &
                    "WHERE  (rifornimenti_allegati.id_rifornimento = '" & id_rifornimento & "')"



        Try
            sqlAllegati.SelectCommand = sqlstr
            sqlAllegati.DataBind()
            ListViewAllegati.DataBind()


        Catch ex As Exception
            funzioni_comuni.genUserMsgBox(Page, ex.Message)
        End Try



    End Sub

    Protected Sub upload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles upload.Click

        Try

            'Dim maxfile As Integer = 16 * 1024 * 1024

            'If UploadAllegati.PostedFile.ContentLength < maxfile Then

            '    Libreria.genUserMsgBox(Page, "Impossibile caricare il file perchè di dimensioni maggiori a 15 MB")
            '    Exit Sub

            'End If

            If dropNuovoAllegato.SelectedValue = "0" Then
                Libreria.genUserMsgBox(Page, "Selezionare una tipologia di allegato.")
            ElseIf Not UploadAllegati.HasFile Then
                Libreria.genUserMsgBox(Page, "Selezionare un file da allegare.")
            Else

                Dim filePath As String = Server.MapPath("\allegati\rifornimenti/")

                Dim my_path As String = "/allegati/rifornimenti/"

                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)

                Dim siglaAllegato As String = getSiglaAllegato(dropNuovoAllegato.SelectedValue.ToString)


                If UploadAllegati.HasFile Then

                    Dim nome_file As String = UploadAllegati.FileName

                    Dim estensione As String = funzioni_comuni.GetEstensioneFile(nome_file)

                    Dim nome_file_operatore As String = nome_file

                    'Dim data_allegato As String = FormatDateTime(Date.Now, vbShortDate) ' txt_data_allegato.Text ignorato x tutti gli utenti nn abilitati

                    Dim id_rifornimento As String = Request.QueryString("rifornimento")

                    'se visibile vuol dire che è abilitato per quell'utente - salvo 06.12.2022
                    Dim Data_allegato As String = "" 'Date.Now.ToShortDateString & " " & Date.Now.ToLongTimeString
                    Dim dataAllegatoSql As String = funzioni_comuni.getDataDb_senza_orario(Data_allegato)

                    Data_allegato = Year(Date.Now) & "-" & Month(Date.Now) & "-" & Day(Date.Now) & " " & Date.Now.ToLongTimeString

                    Dim id_operatore As String = Request.Cookies("SicilyRentCar")("IdUtente")

                    If Directory.Exists(filePath) = False Then
                        Directory.CreateDirectory(filePath)
                    End If

                    nome_file = siglaAllegato & "_" & id_rifornimento & "." & estensione '& "_" & Data_allegato
                    nome_file = nome_file.Replace(":", "_")
                    nome_file = nome_file.Replace(" ", "_")

                    For x = 1 To 30
                        'nome_file = siglaAllegato & "_" & id_stazione & "_" & data_allegato & "." & estensione
                        If File.Exists(filePath & nome_file) Then
                            Dim xNum As String
                            If x < 10 Then
                                xNum = "0" & x.ToString
                            Else
                                xNum = x.ToString
                            End If
                            nome_file = siglaAllegato & "_" & id_rifornimento & "_" & Data_allegato & "_" & xNum & "." & estensione
                        Else
                            Exit For
                        End If
                    Next
                    'End If

                    'registra sul db
                    Dim sqlall As String = "INSERT INTO rifornimenti_allegati (dataInserimento, nomeFile, percorsoFile, id_rifornimento, id_operatore, idtipoDocumento, nome_file_operatore) " &
                    " VALUES (convert(datetime,getdate(),102),'" & nome_file & "','" & my_path & "','" & id_rifornimento & "','" & id_operatore & "','" & dropNuovoAllegato.SelectedValue & "','" & nome_file_operatore & "')"

                    Dim Cmd As New Data.SqlClient.SqlCommand(sqlall, Dbc)

                    If File.Exists(filePath) Then
                        Libreria.genUserMsgBox(Page, "File Esistente.")
                        Exit Sub
                    Else


                        filePath += nome_file

                        UploadAllegati.SaveAs(filePath)     'salva il file
                        Dbc.Open()

                        Cmd.ExecuteNonQuery()   'aggiorna DB

                        Libreria.genUserMsgBox(Page, "File caricato correttamente.")

                        'aggiorna elenco
                        Aggiorna_Allegati(id_rifornimento)


                        'reset campi
                        dropNuovoAllegato.SelectedValue = "0"

                    End If


                End If 'se file da caricare presente



            End If
        Catch ex As Exception
            funzioni_comuni.genUserMsgBox(Page, "error upload_click : <br/>" & ex.Message)
        End Try




    End Sub

    Protected Function getSiglaAllegato(ByVal id_tipoDocumento As String) As String

        'RESTITUISCE la sigla dell'allegato

        Dim ris As String = ""
        Dim sqlstr As String = ""

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Try

            sqlstr = "select sigla from rifornimenti_TipoAllegato where id='" & id_tipoDocumento & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            ris = Cmd.ExecuteScalar


            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            ris = "nn"
        End Try

        Return ris



    End Function

    Protected Sub ListViewAllegati_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListViewAllegati.ItemCommand


        Dim IdAllegatoDaEliminare As Label = e.Item.FindControl("lblIdAllegato")
        Dim NomeFile As Label = e.Item.FindControl("lblNomeFile")
        Dim PercFile As Label = e.Item.FindControl("lblPercorsoFile")

        Dim posizione As Integer = 0 'PercFile.Text.IndexOf("gestione_multe")
        Dim newPercorso As String = "" 'Mid(Replace(PercFile.Text, "\", "/"), posizione + 1) 'restituisce una stringa a partire dalla posizione specificata dopo averla convertita
        Dim id_operatore As String = Request.Cookies("SicilyRentCar")("IdUtente")

        Dim id_rifornimento As String = Request.QueryString("rifornimento")

        If e.CommandName = "SelezionaAllegato" Then

            'newPercorso = newPercorso & NomeFile.Text 'aggiungo il nome del file al precorso
            'Response.Write("NEW " & NomeFile.Text)
            'Response.Write("NEW2 " & PercFile.Text)
            newPercorso = PercFile.Text & NomeFile.Text

            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('" & newPercorso & "','')", True)
                End If
            End If

        End If

        If e.CommandName = "EliminaAllegato" Then

            Dim ris As Integer = DeleteAllegato(IdAllegatoDaEliminare.Text)

            If ris > 0 Then

                'elimina il file relativo 19.04.2022
                Dim filePath As String = Server.MapPath("\" & PercFile.Text)        'Path 19.04.2022

                'Dim msgfile As String = ""
                Try
                    If File.Exists(filePath & NomeFile.Text) Then

                        Dim nomefiledel As String = "DEL-" & id_operatore & "_" & NomeFile.Text
                        File.Copy(filePath & NomeFile.Text, filePath & nomefiledel, True) 'se presente lo sovrascrive-
                        System.Threading.Thread.Sleep(200)
                        File.Delete(filePath & NomeFile.Text)

                    End If

                Catch ex As Exception

                End Try

                Libreria.genUserMsgBox(Page, "Allegato eliminato correttamente")

                Aggiorna_Allegati(id_rifornimento)

                'tx_DataDa.Enabled = True
                'tx_DataA.Enabled = True

            End If

        End If




    End Sub

    Protected Function DeleteAllegato(idallegato As String) As Integer

        Dim ris As Integer = 0
        Dim sqlstr As String = "delete from [rifornimenti_allegati] where id_allegato='" & idallegato & "';"


        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()


        Try
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            ris = Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Libreria.genUserMsgBox(Page, "Errore nell'eliminazione dell'allegato: " & idallegato)

        End Try

        Return ris


    End Function



    'end Salvo 16.12.2022










End Class
