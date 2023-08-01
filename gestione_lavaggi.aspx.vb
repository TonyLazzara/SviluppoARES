Imports System.Collections.Generic

Partial Class gestione_lavaggi
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        AddHandler gestione_checkin.ChiusuraForm, AddressOf gestione_danni_ChiusuraForm
        AddHandler gestione_checkin.SalvaCheckIn, AddressOf gestione_danni_SalvataggioCheckIn
        If IsNothing(Request.Cookies("SicilyRentCar")("stazione")) Then
            Response.Redirect("")
        End If
        Dim sql As String = "SELECT id, descrizione FROM lavaggi_autolavaggio WITH(NOLOCK) WHERE id_stazione=" & Request.Cookies("SicilyRentCar")("stazione") & " ORDER BY descrizione"

        Try
            If Not Page.IsPostBack() Then

                sql = "SELECT gruppi.cod_gruppo, lavaggi.id, lavaggi.num_lavaggio, lavaggi.data_uscita, lavaggi.data_rientro, lavaggi.data_presunto_rientro, lavaggi.stato As id_status, lavaggi.gruppo, "
                sql += "(stazioni1.codice + ' ' + stazioni1.nome_stazione) As stazione_uscita, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As stazione_rientro, lavaggi.id_veicolo, "
                sql += "lavaggi.targa, modelli.descrizione As modello, lavaggi.id_conducente, lavaggi.km_uscita, lavaggi.km_rientro, lavaggi.litri_uscita, lavaggi.litri_rientro, "
                sql += "lavaggi.litri_max, lavaggi.id_lavaggi_tipologia, lavaggi_tipologia.descrizione As tipologia, lavaggi_autolavaggio.descrizione As autolavaggio FROM lavaggi With(NOLOCK) "
                sql += "LEFT JOIN lavaggi_tipologia With(NOLOCK) On lavaggi.id_lavaggi_tipologia=lavaggi_tipologia.id "
                sql += "LEFT JOIN lavaggi_autolavaggio With(NOLOCK) On lavaggi.id_lavaggi_autolavaggio=lavaggi_autolavaggio.id "
                sql += "LEFT JOIN stazioni As stazioni1 With(NOLOCK) On lavaggi.id_stazione_uscita=stazioni1.id "
                sql += "LEFT JOIN stazioni As stazioni2 With(NOLOCK) On lavaggi.id_stazione_rientro=stazioni2.id "
                sql += "LEFT JOIN veicoli With(NOLOCK) On lavaggi.id_veicolo=veicoli.id "
                sql += "LEFT JOIN gruppi On modelli.id_gruppo=gruppi.id_gruppo "
                sql += "LEFT JOIN modelli With(NOLOCK) On veicoli.id_modello=modelli.id_modello WHERE lavaggi.id>0"


                sqlLavaggi.SelectCommand = sql

                permesso_accesso.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneLavaggi)


                id_operatore.Text = Request.Cookies("SicilyRentCar")("idUtente")
                id_stazione_operatore.Text = Request.Cookies("SicilyRentCar")("stazione")


                dropDrivers.DataBind()
                dropAutolavaggio.DataBind()
                dropTipologia.DataBind()
                dropCercaStazionePickUp.DataBind()

                dropStazionePickUp.DataBind()
                dropStazionePickUp.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
                dropStazionePickUp.Enabled = False

                dropCercaStazionePickUp.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
                dropCercaStato.SelectedValue = "0"

                If permesso_accesso.Text <> "1" Then
                    If permesso_accesso.Text <> "3" Then
                        'dropCercaStazionePickUp.Enabled = False

                        btnNuovoLavaggio.Visible = False
                    Else
                        btnNuovoLavaggio.Visible = True
                    End If
                Else
                    Response.Redirect("Default.aspx")
                End If

                lblOrderBY.Text = "  ORDER BY lavaggi.id DESC"

                ricerca(lblOrderBY.Text)
            Else
                sqlLavaggi.SelectCommand = query_cerca.Text & " " & lblOrderBY.Text
            End If
        Catch ex As Exception
            Response.Write("error_pageload_:" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

        sqlAutolavaggio.SelectCommand = sql



    End Sub

    Protected Function condizione_where() As String
        Dim condizione As String = ""

        If dropCercaStato.SelectedValue <> "-1" Then
            condizione = condizione & " AND lavaggi.stato='" & dropCercaStato.SelectedValue & "'"
        End If


        If dropCercaStazionePickUp.SelectedValue <> "0" Then
            condizione = condizione & " AND lavaggi.id_stazione_uscita='" & dropCercaStazionePickUp.SelectedValue & "'"
        End If

        If dropCercaTipologia.SelectedValue <> "0" Then
            condizione = condizione & " AND lavaggi.id_lavaggi_tipologia=" & dropCercaTipologia.SelectedValue
        End If

        If dropCercaAutolavaggio.SelectedValue <> "0" Then
            condizione = condizione & " AND lavaggi.id_lavaggi_autolavaggio=" & dropCercaAutolavaggio.SelectedValue
        End If

        ''DATA USCITA-----------------------------------------------------------------------------------------------------------
        If txtCercaDataUscitaDaInterno.Text <> "" And txtCercaDataUscitaAInterno.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaDataUscitaDaInterno.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaDataUscitaDaInterno.Text)

            condizione = condizione & " AND data_uscita >= CONVERT(DATETIME,'" & data1 & "',102)"
        End If

        If txtCercaDataUscitaDaInterno.Text = "" And txtCercaDataUscitaAInterno.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaDataUscitaAInterno.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaDataUscitaAInterno.Text & " 23:59:59")

            condizione = condizione & " AND data_uscita <= CONVERT(DATETIME,'" & data2 & "',102)"
        End If

        If txtCercaDataUscitaDaInterno.Text <> "" And txtCercaDataUscitaAInterno.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaDataUscitaDaInterno.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaDataUscitaDaInterno.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaDataUscitaAInterno.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaDataUscitaAInterno.Text & " 23:59:59")

            condizione = condizione & " AND data_uscita BETWEEN CONVERT(DATETIME,'" & data1 & "',102) AND CONVERT(DATETIME,'" & data2 & "',102)"
        End If
        ''----------------------------------------------------------------------------------------------------------------------------

        If txtCercaTargaInterno.Text <> "" Then
            condizione = condizione & " AND lavaggi.targa='" & Replace(txtCercaTargaInterno.Text, "'", "''") & "'"
        End If


        condizione_where = condizione
    End Function


    Protected Sub ricerca(ByVal order_by As String)
        Dim sql As String = ""
        Try
            sql = "SELECT gruppi.cod_gruppo, lavaggi.id, lavaggi.num_lavaggio, lavaggi.data_uscita, lavaggi.data_rientro, lavaggi.data_presunto_rientro, lavaggi.stato, lavaggi.id_stazione_uscita, lavaggi.gruppo, "
            sql += "(stazioni1.codice + ' ' + stazioni1.nome_stazione) As stazione_uscita, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As stazione_rientro, lavaggi.id_veicolo, "
            sql += "lavaggi.targa, modelli.descrizione As modello, lavaggi.id_conducente, lavaggi.km_uscita, lavaggi.km_rientro, lavaggi.litri_uscita, lavaggi.litri_rientro, "
            sql += "lavaggi.litri_max, lavaggi.id_lavaggi_tipologia, lavaggi_tipologia.descrizione As tipologia, lavaggi_autolavaggio.descrizione As autolavaggio, lavaggi.id_lavaggi_autolavaggio FROM lavaggi WITH(NOLOCK) "
            sql += "LEFT JOIN lavaggi_tipologia WITH(NOLOCK) ON lavaggi.id_lavaggi_tipologia=lavaggi_tipologia.id "
            sql += "LEFT JOIN lavaggi_autolavaggio WITH(NOLOCK) ON lavaggi.id_lavaggi_autolavaggio=lavaggi_autolavaggio.id "
            sql += "LEFT JOIN stazioni As stazioni1 WITH(NOLOCK) ON lavaggi.id_stazione_uscita=stazioni1.id "
            sql += "LEFT JOIN stazioni As stazioni2 WITH(NOLOCK) ON lavaggi.id_stazione_rientro=stazioni2.id "
            sql += "LEFT JOIN veicoli WITH(NOLOCK) ON lavaggi.id_veicolo=veicoli.id "
            sql += "LEFT JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello "
            sql += "LEFT JOIN gruppi ON modelli.id_gruppo=gruppi.id_gruppo "
            sql += "WHERE lavaggi.id>0 " & condizione_where()

            query_cerca.Text = sql

            sqlLavaggi.SelectCommand = query_cerca.Text & " " & order_by

            lblOrderBY.Text = order_by

            ' Response.Write(sql & "<br/>")
            listLavaggi.DataBind()


        Catch ex As Exception
            Response.Write("error_ricerca_:" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Sub

    Protected Sub seleziona_targa()
        Dim risultato(11) As String

        risultato = funzioni_comuni.scegli_targa_x_contratto(txtTarga.Text)

        '0 = id del veicolo
        '1 = messaggio di errore se targa non selezionabile
        '2 = serbatoio attuale
        '3 = serbatoio massimo
        '4 = km attuali
        '5 = id stazione attuale
        '6 = id_gruppo
        '7 = Modello
        '8 = gruppo
        '9 = id_alimentazione
        '10 = 1 se da rifornire
        '11 = 1 se da lavare


        idVeicoloSelezionato.Text = ""


        'SE C'E' UN MESSAGGIO DI ERRORE IN POSIZIONE 1 VUOL DIRE CHE IL VEICOLO NON E' SELEZIONABILE (MOTIVI BLOCCANTI)
        If risultato(1) <> "" Then
            Libreria.genUserMsgBox(Me, risultato(1))
        Else
            'ANCHE SE NON VIENE RESTITUITO UN ERRORE BLOCCANTE DEVONO ESSERE ESEGUITI ALCUNI CONTROLLI:

            If risultato(5) = "" Then
                Libreria.genUserMsgBox(Me, "Auto non in parco.")
            ElseIf (risultato(5) <> dropStazionePickUp.SelectedValue) Then
                Libreria.genUserMsgBox(Me, "Attenzione: l'auto risulta in una stazione diversa da quella di uscita.")
            ElseIf risultato(10) = "1" And risultato(11) = "" Then
                Libreria.genUserMsgBox(Me, "Auto da rifornire.")
            ElseIf risultato(10) = "" And risultato(11) = "1" Then
                'AUTO NELLO STATO "DA LAVARE"
                Libreria.genUserMsgBox(Me, "Auto da lavare.")
            ElseIf risultato(10) = "1" And risultato(11) = "2" Then
                'AUTO SIA "DA RIFORNIRE" CHE "DA LAVARE"
                Libreria.genUserMsgBox(Me, "Auto da rifornire a da lavare.")
            Else
                'TUTTO OK - COLLEGO L'AUTO
                idVeicoloSelezionato.Text = risultato(0)
                txtKm.Text = risultato(4)
                txtSerbatoio.Text = risultato(2)
                lblSerbatoioMax.Text = risultato(3)
                lblSerbatoioMaxRientro.Text = risultato(3)
                txtModello.Text = risultato(7)
                idGruppo.Text = risultato(6)
                txtGruppo.Text = risultato(8)
                txtTarga.ReadOnly = True
                btnScegliTarga.Text = "Modifica"

                txtDataUscita.Enabled = False   '17.12.2020 aggiunto
                txtOraUscita.Enabled = False    '17.12.2020 aggiunto
                txtPresuntoRientro.Enabled = True    '17.12.2020 aggiunto
                txtPresuntoRientroOra.Enabled = True     '17.12.2020 aggiunto





                Libreria.genUserMsgBox(Me, "Veicolo selezionato correttamente.")
            End If
        End If
    End Sub

    Protected Function check_veicolo_disponibile() As Boolean
        Dim risultato(11) As String
        risultato = funzioni_comuni.scegli_targa_x_contratto(txtTarga.Text)


        If risultato(1) <> "" Then
            check_veicolo_disponibile = False
        Else
            'ANCHE SE NON VIENE RESTITUITO UN ERRORE BLOCCANTE DEVONO ESSERE ESEGUITI ALCUNI CONTROLLI:
            '1)AUTO NON IN PARCO: DEVE ESSERE POSSIBILE EFFETTUARE L'IMMISSIONE IN PARCO
            If risultato(5) = "" Then
                check_veicolo_disponibile = False
            ElseIf (risultato(5) <> dropStazionePickUp.SelectedValue) Then
                check_veicolo_disponibile = False
            ElseIf risultato(10) = "1" And risultato(11) = "" Then
                check_veicolo_disponibile = False
            ElseIf risultato(10) = "" And risultato(11) = "1" Then
                'AUTO NELLO STATO "DA LAVARE"
                check_veicolo_disponibile = False
            ElseIf risultato(10) = "1" And risultato(11) = "2" Then
                'AUTO SIA "DA RIFORNIRE" CHE "DA LAVARE"
                check_veicolo_disponibile = False
            Else
                'TUTTO OK - COLLEGO L'AUTO
                check_veicolo_disponibile = True
            End If
        End If

    End Function

    Protected Function get_num_lavaggio() As String

        Dim sql As String = "SELECT ISNULL(MAX(num_lavaggio)," & Right(Year(Now()), 2) & "00000) FROM lavaggi WITH(NOLOCK) WHERE Right(Year(data_uscita), 2)=" & Right(Year(Now()), 2)
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            get_num_lavaggio = Cmd.ExecuteScalar + 1

            If get_num_lavaggio = 0 Then
                get_num_lavaggio = Right(Year(Now()), 2) & "00001"
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error_getnumlavaggio_:" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Function

    Protected Sub check_out()
        Dim sqlStr As String = ""
        Try
            'SALVATAGGIO DELLA RIGA DI TRASFERIMENTO
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim num_lavaggio As String = get_num_lavaggio()
            Dim presunto_rientro As String = funzioni_comuni.getDataDb_con_orario(txtPresuntoRientro.Text & " " & txtPresuntoRientroOra.Text & ":00")

            Dim uscita As String

            uscita = "GetDate()"
            txtDataUscita.Text = Format(Now(), "dd/MM/yyyy")
            txtOraUscita.Text = Hour(Now()) & ":" & Minute(Now())

            Dim nn As String = funzioni_comuni.getDataDb_con_orario(Date.Now.ToString)


            sqlStr = "INSERT INTO lavaggi (stato, num_lavaggio, id_veicolo, targa, km_uscita, litri_uscita, litri_max, data_uscita,id_conducente,data_presunto_rientro,"
            sqlStr += "id_operatore_uscita,id_stazione_uscita, data_operazione_apertura, gruppo, id_lavaggi_autolavaggio, id_lavaggi_tipologia) VALUES ("
            sqlStr += "'0','" & num_lavaggio & "','" & idVeicoloSelezionato.Text & "','" & txtTarga.Text & "','" & txtKm.Text & "',"
            sqlStr += "'" & txtSerbatoio.Text & "','" & lblSerbatoioMax.Text & "'," & uscita & ","
            sqlStr += "'" & dropDrivers.SelectedValue & "',convert(datetime,'" & presunto_rientro & "',102),'" & Request.Cookies("SicilyRentCar")("IdUtente") & "',"
            sqlStr += "'" & dropStazionePickUp.SelectedValue & "',convert(datetime,'" & nn & "',102),'" & txtGruppo.Text & "'," & dropAutolavaggio.SelectedValue & "," & dropTipologia.SelectedValue & ")"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd = New Data.SqlClient.SqlCommand("SELECT @@IDENTITY FROM lavaggi WITH(NOLOCK)", Dbc)
            idLavaggio.Text = Cmd.ExecuteScalar

            'REGISTRAZIONE DEL MOVIMENTO DI NOLO IN CORSO PER IL VEICOLO -----------------------------------------------------------------------
            sqlStr = "insert into movimenti_targa (num_riferimento, id_veicolo, id_tipo_movimento, data_uscita, id_stazione_uscita, data_presunto_rientro, id_stazione_presunto_rientro, "
            sqlStr += " km_uscita, serbatoio_uscita, id_operatore, data_registrazione, movimento_attivo) "
            sqlStr += "VALUES"
            sqlStr += " ('" & num_lavaggio & "','" & idVeicoloSelezionato.Text & "','" & Costanti.idMovimentoLavaggio & "',convert(datetime," & uscita & ",102),'" & dropStazionePickUp.SelectedValue & "',"
            sqlStr += "convert(datetime,'" & presunto_rientro & "',102),'" & dropStazionePickUp.SelectedValue & "',"
            sqlStr += "'" & txtKm.Text & "','" & txtSerbatoio.Text & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "',convert(datetime,GetDate(),102),'1')"

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            'SETTAGGIO DEL VEICOLO COME NOLEGGIATO
            sqlStr = "UPDATE veicoli SET disponibile_nolo='0', noleggiata='1', da_lavare='1' WHERE id='" & idVeicoloSelezionato.Text & "'"

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()
            ''-------------------------------------------------------------------------------------------------------------------------------

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            gestione_checkin.InitFormCheckOut(tipo_documento.Lavaggio, Integer.Parse(idLavaggio.Text), 0)

            div_edit_danno.Visible = True
            div_lavaggi.Visible = False
        Catch ex As Exception
            Response.Write("error_checkout_:" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try


    End Sub

    Protected Sub btnCheckOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckOut.Click
        If stato_lavaggio.Text = "-1" Then
            'IN QUESTO CASO STO SALVANDO L'USCITA DEL VEICOLO - E' NECESSARIO CONTROLLARE I CAMPI PER POTER PROCEDERE
            If idVeicoloSelezionato.Text <> "" Then
                If dropStazionePickUp.SelectedValue <> "0" Then
                    If dropDrivers.SelectedValue <> "0" Then
                        If dropAutolavaggio.SelectedValue <> "0" Then
                            If dropTipologia.SelectedValue <> "0" Then
                                If txtPresuntoRientro.Text <> "" And txtPresuntoRientroOra.Text <> "" Then
                                    Dim data1 As DateTime = funzioni_comuni.getDataDb_con_orario2(txtPresuntoRientro.Text & " " & txtPresuntoRientroOra.Text & ":00")
                                    Dim data2 As DateTime


                                    data2 = funzioni_comuni.getDataDb_con_orario2(Now())


                                    If DateDiff(DateInterval.Minute, data2, data1) > 0 Then
                                        'RICONTROLLO CHE IL VEICOLO SIA DISPONIBILE - NEL FRATTEMPO POTREBBE AVER CAMBIATO STATO
                                        If check_veicolo_disponibile() Then
                                            check_out()

                                            stato_lavaggio.Text = "0"

                                            dropDrivers.Enabled = False
                                            txtPresuntoRientro.Enabled = False
                                            txtPresuntoRientroOra.Enabled = False
                                            dropStazionePickUp.Enabled = False
                                            txtDataUscita.Enabled = False
                                            txtOraUscita.Enabled = False
                                            btnScegliTarga.Visible = False
                                            btnCheckOut.Visible = False
                                            btnVediCheckInterno.Visible = False
                                            btnCheckIn.Visible = True

                                            dropTipologia.Enabled = False '18.12.2020
                                            dropAutolavaggio.Enabled = False '18.12.2020
                                            dropDrivers.Enabled = False '18.12.2020

                                        Else
                                            Libreria.genUserMsgBox(Me, "Attenzione: il veicolo non è più disponibile. Selezionare un'altra vettura.")
                                        End If
                                    Else
                                        Libreria.genUserMsgBox(Me, "Attenzione: il presunto rientro deve essere successivo all'orario di uscita.")
                                    End If
                                Else
                                    Libreria.genUserMsgBox(Me, "Specificare data ed ora di presunto arrivo del veicolo.")
                                End If
                            Else
                                Libreria.genUserMsgBox(Me, "Specificare la tipologia di lavaggio.")
                            End If
                        Else
                            Libreria.genUserMsgBox(Me, "Specificare l'autolavaggio.")
                        End If
                    Else
                        Libreria.genUserMsgBox(Me, "Specificare il conducente del veicolo.")
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Specificare la stazione di uscita.")
                End If
            Else
                Libreria.genUserMsgBox(Me, "Specificare un veicolo.")
            End If
        End If
    End Sub

    Protected Sub gestione_danni_ChiusuraForm(ByVal sender As Object, ByVal e As System.EventArgs)
        div_check.Visible = True
        div_lavaggi.Visible = True
        div_edit_danno.Visible = False
    End Sub

    Protected Sub gestione_danni_SalvataggioCheckIn(ByVal sender As Object, ByVal e As EventoNuovoRecord)
        salva_rientro()

        btnVediCheckInterno.Visible = True
        btnCheckIn.Visible = False
        riga_rientro_interno.Visible = True

        div_edit_danno.Visible = False
        div_lavaggi.Visible = True
        div_check.Visible = True
    End Sub

    Protected Sub salva_rientro()
        Dim sqlStr As String = ""

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT km_rientro FROM lavaggi WITH(NOLOCK) WHERE id='" & id_modifica.Text & "'", Dbc)
            txtKmRientro.Text = Cmd.ExecuteScalar
            Cmd = New Data.SqlClient.SqlCommand("SELECT litri_rientro FROM lavaggi WITH(NOLOCK) WHERE id='" & id_modifica.Text & "'", Dbc)
            txtSerbatoioRientro.Text = Cmd.ExecuteScalar
            Cmd = New Data.SqlClient.SqlCommand("SELECT data_rientro FROM lavaggi WITH(NOLOCK) WHERE id='" & id_modifica.Text & "'", Dbc)

            Dim test As String = Cmd.ExecuteScalar
            txtDataRientro.Text = Day(test) & "/" & Month(test) & "/" & Year(test)
            txtOraRientro.Text = IIf(Len(CStr(Hour(test))) = 1, "0" & Hour(test), Hour(test)) & "." & IIf(Len(CStr(Month(test))) = 1, "0" & Month(test), Month(test))

            sqlStr = "UPDATE lavaggi SET id_operatore_chiusura='" & Request.Cookies("SicilyRentCar")("IdUtente") & "', data_operazione_chiusura='" & Now() & "', " &
            "stato='1' " &
            "WHERE id='" & id_modifica.Text & "'"

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            Libreria.genUserMsgBox(Me, "Richiesta memorizzata correttamente.")

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_salvarientro_:" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Sub

    Protected Sub btnNuovoLavaggio_Click(sender As Object, e As System.EventArgs) Handles btnNuovoLavaggio.Click
        stato_lavaggio.Text = "-1"

        div_cerca_interni.Visible = False
        div_check.Visible = True
        riga_rientro_interno.Visible = False

        txtDataUscita.Text = Format(Now(), "dd/MM/yyy")
        'su nuovo lavaggio il campo 
        'txtOraUscita.Text = Format(Now(), "HH:mm") 

        txtPresuntoRientro.Text = Format(Now(), "dd/MM/yyy")

        txtDataUscita.Enabled = False    '16.12.2020 rimuovere anche java
        txtOraUscita.Enabled = False '16.12.2020

        'ClientScript.RegisterStartupScript([GetType](), "myalert", "nocalendar();", True)
        txtDataUscita.Attributes.Remove("<a>")

        txtDataRientro.Enabled = True '16.12.2020
        txtOraRientro.Enabled = True '16.12.2020

        btnVediCheckInterno.Visible = False
        btnCheckOut.Visible = True
        btnCheckIn.Visible = False

        'carica lista lavaggi aggiunto 16.12.2020 non caricava lista lavaggi
        'se lista vuota
        If dropAutolavaggio.Items.Count = 1 Then
            dropAutolavaggio.Items.Clear()          'aggiunto il 17.02.2022
            dropAutolavaggio.Items.Add(New ListItem("Seleziona...", "0")) 'aggiunto il 22.02.2022
            sqlAutolavaggioTutti.SelectCommand = "SELECT id, descrizione FROM lavaggi_autolavaggio WITH(NOLOCK) ORDER BY descrizione DESC"
            dropAutolavaggio.DataBind()
        End If




    End Sub

    Protected Sub btnScegliTarga_Click(sender As Object, e As System.EventArgs) Handles btnScegliTarga.Click
        seleziona_targa()
    End Sub

    Protected Sub listLavaggi_ItemCommand(sender As Object, e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listLavaggi.ItemCommand

        Try
            If e.CommandName = "vedi" Then

                'carica lista lavaggi aggiunto 16.12.2020 non caricava lista loavaggi


                'deve effettuare il reset del dropAutoLavaggio altrimenti raddoppia le voci 17.02.2022
                dropAutolavaggio.Items.Clear()
                dropAutolavaggio.Items.Add(New ListItem("Seleziona...", "0"))
                sqlAutolavaggioTutti.SelectCommand = "SELECT id, descrizione FROM lavaggi_autolavaggio WITH(NOLOCK) ORDER BY descrizione DESC"
                dropAutolavaggio.DataBind()


                Dim idLabel As Label = e.Item.FindControl("idLabel")
                Dim id_veicolo As Label = e.Item.FindControl("id_veicolo")
                Dim targa As Label = e.Item.FindControl("targa")
                Dim lblModello As Label = e.Item.FindControl("lblModello")
                Dim id_conducente As Label = e.Item.FindControl("id_conducente")
                Dim km_uscita As Label = e.Item.FindControl("km_uscita")
                Dim km_rientro As Label = e.Item.FindControl("km_rientro")
                Dim litri_uscita As Label = e.Item.FindControl("litri_uscita")
                Dim litri_rientro As Label = e.Item.FindControl("litri_rientro")
                Dim data_uscita As Label = e.Item.FindControl("data_uscita")
                Dim data_rientro As Label = e.Item.FindControl("data_rientro")
                Dim data_presunto_rientro As Label = e.Item.FindControl("data_presunto_rientro")
                Dim litri_max As Label = e.Item.FindControl("litri_max")
                Dim id_stazione_pick_up As Label = e.Item.FindControl("id_stazione_uscita")
                Dim gruppo As Label = e.Item.FindControl("gruppo")
                Dim stato As Label = e.Item.FindControl("id_stato")
                Dim id_lavaggi_autolavaggio As Label = e.Item.FindControl("id_lavaggi_autolavaggio")
                Dim id_lavaggi_tipologia As Label = e.Item.FindControl("id_lavaggi_tipologia")

                stato_lavaggio.Text = stato.Text

                id_modifica.Text = idLabel.Text

                idLavaggio.Text = idLabel.Text

                idVeicoloSelezionato.Text = id_veicolo.Text
                txtTarga.Text = targa.Text 'x test & "(" & id_lavaggi_autolavaggio.Text & ")"
                txtGruppo.Text = gruppo.Text
                txtModello.Text = lblModello.Text
                txtKm.Text = km_uscita.Text
                txtKmRientro.Text = km_rientro.Text
                txtSerbatoio.Text = litri_uscita.Text
                lblSerbatoioMax.Text = litri_max.Text
                txtSerbatoioRientro.Text = litri_rientro.Text
                dropDrivers.SelectedValue = id_conducente.Text
                dropDrivers.Enabled = False

                dropAutolavaggio.SelectedValue = id_lavaggi_autolavaggio.Text   '''reinserito il 16.12.2020 in seguito 21.03.2020 adesso carica la lista dei lavaggi
                dropAutolavaggio.Enabled = False 'deve essere false

                dropTipologia.SelectedValue = id_lavaggi_tipologia.Text
                dropTipologia.Enabled = False

                txtDataUscita.Text = Day(data_uscita.Text) & "/" & Month(data_uscita.Text) & "/" & Year(data_uscita.Text)
                txtOraUscita.Text = Hour(data_uscita.Text) & ":" & Minute(data_uscita.Text)
                txtDataUscita.Enabled = False
                txtOraUscita.Enabled = False

                txtPresuntoRientro.Text = Day(data_presunto_rientro.Text) & "/" & Month(data_presunto_rientro.Text) & "/" & Year(data_presunto_rientro.Text)
                txtPresuntoRientroOra.Text = Hour(data_presunto_rientro.Text) & ":" & Minute(data_presunto_rientro.Text)
                txtPresuntoRientro.Enabled = False
                txtPresuntoRientroOra.Enabled = False

                If data_rientro.Text <> "" Then
                    riga_rientro_interno.Visible = True
                    txtDataRientro.Text = Day(data_rientro.Text) & "/" & Month(data_rientro.Text) & "/" & Year(data_rientro.Text)
                    txtOraRientro.Text = Hour(data_rientro.Text) & ":" & Minute(data_rientro.Text)
                    txtDataRientro.Enabled = False  '16.12.2020
                    txtOraRientro.Enabled = False '16.12.2020


                    lblSerbatoioMaxRientro.Text = litri_max.Text
                    txtSerbatoioRientro.Text = litri_rientro.Text
                    txtKmRientro.Text = km_rientro.Text
                Else
                    riga_rientro_interno.Visible = False
                End If

                dropStazionePickUp.SelectedValue = id_stazione_pick_up.Text
                dropStazionePickUp.Enabled = False

                btnCheckOut.Visible = False
                btnVediCheckInterno.Visible = True

                btnScegliTarga.Visible = False

                div_cerca_interni.Visible = False
                div_check.Visible = True

                If stato.Text = "0" Then
                    btnCheckIn.Visible = True
                    btnVediCheckInterno.Visible = False
                Else
                    btnCheckIn.Visible = False
                    btnVediCheckInterno.Visible = True
                End If
            End If


            'dropAutolavaggio.Enabled = True 'x test 17.02.2022




        Catch ex As Exception
            Response.Write("error_listLavaggi_ItemCommand: " & ex.Message & "<br/>")
        End Try



    End Sub

    Protected Sub btnCheckIn_Click(sender As Object, e As System.EventArgs) Handles btnCheckIn.Click

        Try
            gestione_checkin.InitFormCheckIn(tipo_documento.Lavaggio, Integer.Parse(id_modifica.Text), 0)

            div_check.Visible = False
            div_edit_danno.Visible = True
        Catch ex As Exception
            Response.Write("error_btncheckin_cick_:" & ex.Message & "<br/>")
        End Try

    End Sub

    Protected Sub pulisci_campi()
        idVeicoloSelezionato.Text = ""
        idGruppo.Text = ""
        idLavaggio.Text = ""
        stato_lavaggio.Text = ""
        txtTarga.Text = ""
        txtTarga.ReadOnly = False
        txtGruppo.Text = ""
        txtModello.Text = ""
        txtKm.Text = ""
        txtSerbatoio.Text = ""
        txtKmRientro.Text = ""
        txtKmRientro.ReadOnly = False
        txtSerbatoioRientro.Text = ""
        txtSerbatoioRientro.ReadOnly = False


        btnScegliTarga.Visible = True

        btnCheckOut.Text = "Salva - Check Out"

        lblSerbatoioMax.Text = ""
        lblSerbatoioMaxRientro.Text = ""

        'dropAutolavaggio.SelectedValue = "0"       'in rem il 17.02.2022 perchè dava errore 
        dropAutolavaggio.Enabled = True

        dropTipologia.SelectedValue = "0"
        dropTipologia.Enabled = True

        dropDrivers.SelectedValue = "0"
        dropDrivers.Enabled = True
        txtPresuntoRientro.Text = ""
        txtPresuntoRientro.Enabled = True
        txtPresuntoRientroOra.Text = ""
        txtPresuntoRientroOra.Enabled = True

        txtDataUscita.Text = ""
        txtOraUscita.Text = ""

        txtDataRientro.Text = ""
        txtOraRientro.Text = ""

        riga_rientro_interno.Visible = False
    End Sub

    Protected Sub btnChiudiCheck_Click(sender As Object, e As System.EventArgs) Handles btnChiudiCheck.Click
        idLavaggio.Text = ""
        pulisci_campi()

        listLavaggi.DataBind()

        div_cerca_interni.Visible = True
        div_check.Visible = False
    End Sub

    Protected Sub btnVediCheckInterno_Click(sender As Object, e As System.EventArgs) Handles btnVediCheckInterno.Click
        gestione_checkin.InitFormCheckIn(tipo_documento.Lavaggio, Integer.Parse(id_modifica.Text), 0)

        div_check.Visible = False
        div_edit_danno.Visible = True
    End Sub

    Protected Sub listLavaggi_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listLavaggi.ItemDataBound
        Dim id_stazione_uscita As Label = e.Item.FindControl("id_stazione_uscita")
        Dim btnVedi As Button = e.Item.FindControl("btnVedi")
        Dim stato As Label = e.Item.FindControl("stato")

        If id_stazione_uscita.Text <> Request.Cookies("SicilyRentCar")("stazione") Then
            btnVedi.Visible = False
        End If

        If stato.Text = "0" Then
            stato.Text = "Aperto"
        ElseIf stato.Text = "1" Then
            stato.Text = "Chiuso"
        End If

        If permesso_accesso.Text <> "3" Then
            btnVedi.Visible = False
        End If
    End Sub

    Protected Sub btnCercaInterni_Click(sender As Object, e As System.EventArgs) Handles btnCercaInterni.Click
        ricerca(lblOrderBY.Text)
    End Sub
End Class
