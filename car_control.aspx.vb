Imports funzioni_comuni

Partial Class car_control
    Inherits System.Web.UI.Page
    Dim funzioni As New funzioni_comuni

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            livelloAccesso.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 76)

            If livelloAccesso.Text = "1" Then
                Response.Redirect("default.aspx")
            End If

            If Request.Cookies("SicilyRentCar") Is Nothing Then
                Response.Redirect("LogIn.aspx")
            End If
        End If
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisualizza.Click
        If txtDaData.Text <> "" Then
            'Response.Write(CDate(txtDaData.Text) & " " & DateDiff(DateInterval.Day, Now(), CDate(txtDaData.Text)))
            If DateDiff(DateInterval.Day, Now(), CDate(txtDaData.Text)) < 0 Then
                Libreria.genUserMsgBox(Me, "Il planing è visualizzabile a partire dal giorno corrente.")
                txtDaData.Text = ""
            End If

        End If

        If txtDaData.Text <> "" And txtAData.Text <> "" Then
            If DateDiff(DateInterval.Day, CDate(txtDaData.Text), CDate(txtAData.Text)) > 30 Then
                Libreria.genUserMsgBox(Me, "Il planing è visualizzabile per un intervallo massimo di 30 giorni.")
                txtDaData.Text = ""
                txtAData.Text = ""
            End If
        End If

        If txtDaData.Text <> "" And txtAData.Text <> "" Then
            If DateDiff(DateInterval.Day, CDate(txtDaData.Text), CDate(txtAData.Text)) < 0 Then
                Libreria.genUserMsgBox(Me, "Attenzione: data finale minore della data iniziale.")
                txtDaData.Text = ""
                txtAData.Text = ""
            End If
        End If
    End Sub

    Protected Function getTrasferimenti(ByVal id_veicolo As String, ByVal dataIniziale As DateTime, ByVal dataFinale As DateTime, ByVal stazioneAttuale As String) As Char()
        'RISULTATO: 0 - DISPONIBILE
        'RISULTATO: 1 - IN CORSO
        'RISULTATO: 2 - RIENTRO IN ALTRA STAZIONE 
        'RISULTATO: 3 - IN CORSO CON RIENTRO PREVISTO PRECEDENTE LA DATA ODIERNA


        Dim sql As String = ""



        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim data_iniziale As String = funzioni_comuni.getDataDb_senza_orario2(dataIniziale)
            Dim data_finale As String = funzioni_comuni.getDataDb_senza_orario2(dataFinale)

            Dim data1 As DateTime = dataIniziale
            Dim data2 As DateTime = dataFinale

            Dim mese(30) As Char

            Dim i As Integer = 0

            Do While i < 30
                mese(i) = "0"

                i = i + 1
            Loop

            sql = "SELECT data_uscita, id_stazione_uscita, data_presunto_rientro As data_previsto_rientro, id_stazione_presunto_rientro As id_stazione_previsto_rientro "
            sql += "FROM movimenti_targa WITH(NOLOCK) "
            sql += "WHERE (id_tipo_movimento='" & Costanti.idMovimentoInterno & "' OR id_tipo_movimento='" & Costanti.idBisarca & "') AND movimento_attivo='1' AND id_veicolo='" & id_veicolo & "'"


            'SE HO UN TRASFERIMENTO ATTIVO PER L'AUTO CONSIDERATA DEVO CONSIDERARLO IN OGNI CASO - NON FACCIO CONTROLLI SULLE DATE
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Do While Rs.Read()
                Dim data_uscita As DateTime = CDate(funzioni_comuni.getDataDb_senza_orario2(Rs("data_uscita")))
                Dim data_presunto_rientro As DateTime = CDate(funzioni_comuni.getDataDb_senza_orario2(Rs("data_previsto_rientro")))

                'NEL PRIMO CASO LA STAZIONE DI USCITA COINCIDE CON QUELLA DI RIENTRO ED E' LA STESSA DELLA STAZIONE SELEZIONATA NELLA FUNZIONALITA'.
                If (Rs("id_stazione_previsto_rientro") = Rs("id_stazione_uscita")) And (Rs("id_stazione_previsto_rientro") = stazioneAttuale) Then
                    'CASO 1: PER TUTTO IL PERIODO LA MACCHINA NON E' DISPONIBILE

                    If DateDiff(DateInterval.Day, data1, data_uscita) <= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) >= 0 Then
                        i = 0

                        'Response.Write("F1")

                        Do While i < 30
                            mese(i) = "1"

                            i = i + 1
                        Loop
                    End If

                    'CASO 2: DATA DI USCITA PRECEDENTE A DATA INIZIALE; DATA RIENTRO COMPRESA TRA LE DUE DATE 
                    If DateDiff(DateInterval.Day, data1, data_uscita) <= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) <= 0 Then
                        i = 0

                        'Response.Write("F2")

                        Do While i <= DateDiff(DateInterval.Day, data1, data_presunto_rientro)
                            mese(i) = "1"

                            i = i + 1
                        Loop
                    End If


                    'CASO 3: DATA DI USCITA TRA LE DUE DATE; DATA DI RIENTRO SUCCESSIVA ALLA DATA FINALE
                    If DateDiff(DateInterval.Day, data1, data_uscita) >= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) >= 0 Then
                        i = 0

                        'Response.Write("F3")

                        Do While i < 30
                            If i >= DateDiff(DateInterval.Day, data1, data_uscita) Then
                                mese(i) = "1"
                            End If

                            i = i + 1
                        Loop
                    End If

                    'CASO 3: DATA DI USCITA TRA LE DUE DATE; DATA DI RIENTRO TRA LE DUE DATE
                    If DateDiff(DateInterval.Day, data1, data_uscita) >= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) < 0 Then
                        i = 0

                        'Response.Write("F4")

                        Do While i < 30
                            If i >= DateDiff(DateInterval.Day, data1, data_uscita) And i <= DateDiff(DateInterval.Day, data1, data2) - DateDiff(DateInterval.Day, data_presunto_rientro, data2) Then
                                mese(i) = "1"
                            End If

                            i = i + 1
                        Loop
                    End If
                End If
                'CASO4: PRESUNTO RIENTRO PRECEDENTE ALLA DATA ODIERNA - IN QUESTO CASO NON HO IDEA QUANDO IL VEICOLO DOVREBBE RIENTRARE PER 
                'CUI LO CONSIDERO SEMPRE NOLEGGIATA FINO A NUOVA COMUNICAZIONE DEL CLIENTE (SOLO NEL CASO IN CUI NON HO TROVATO NIENT'ALTO SOPRA)

                If DateDiff(DateInterval.Day, data1, data_uscita) < 0 And DateDiff(DateInterval.Day, data1, data_presunto_rientro) < 0 Then
                    i = 0
                    Do While i < 30

                        mese(i) = "3"

                        i = i + 1
                    Loop
                End If
                '----------------------------------------------------------------------------------------------------------------------------
                'NEL SECONDO CASO IL VEICOLO ESCE DALLA STAZIONE CONSIDERATA MA RIENTRA IN UN'ALTRA STAZIONE
                If (Rs("id_stazione_previsto_rientro") <> Rs("id_stazione_uscita")) And (Rs("id_stazione_uscita") = stazioneAttuale) Then

                    'CASO 1: DATA DI USCITA PRECEDENTE A DATA INIZIALE - DATA DI PRESUNTO RIENTRO TRA LE DUE DATE 
                    If DateDiff(DateInterval.Day, data1, data_uscita) <= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) < 0 Then
                        i = 0
                        Do While i < 30
                            If i >= DateDiff(DateInterval.Day, data1, data_uscita) And i <= (DateDiff(DateInterval.Day, data1, CDate(data_presunto_rientro))) Then
                                mese(i) = "1"
                            ElseIf i > (DateDiff(DateInterval.Day, data1, CDate(data_presunto_rientro))) Then
                                mese(i) = "2"
                            End If

                            i = i + 1
                        Loop
                    End If
                    'CASO 2: DATA DI USCITA PRECEDENTE A DATA INIZIALE - DATA DI PRESUNTO RIENTRO SUCCESSIVA ALLA DATA FINALE
                    If DateDiff(DateInterval.Day, data1, data_uscita) <= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) >= 0 Then
                        i = 0
                        Do While i < 30
                            mese(i) = "1"

                            i = i + 1
                        Loop
                    End If
                End If
            Loop

            getTrasferimenti = mese

            Rs.Close()
            Rs = Nothing

            Cmd.Dispose()
            Cmd = Nothing

            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing


        Catch ex As Exception
            Response.Write("error_GetTrasferimenti_:" & ex.Message & "<br/>")
        End Try

    End Function

    Protected Function getLavaggi(ByVal id_veicolo As String, ByVal dataIniziale As DateTime, ByVal dataFinale As DateTime, ByVal stazioneAttuale As String) As Char()
        'RISULTATO: 0 - DISPONIBILE
        'RISULTATO: 1 - IN CORSO
        'RISULTATO: 3 - IN CORSO CON RIENTRO PREVISTO PRECEDENTE LA DATA ODIERNA
        Dim sql As String = ""




        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim data_iniziale As String = funzioni_comuni.getDataDb_senza_orario2(dataIniziale)
            Dim data_finale As String = funzioni_comuni.getDataDb_senza_orario2(dataFinale)

            Dim data1 As DateTime = dataIniziale
            Dim data2 As DateTime = dataFinale

            Dim mese(30) As Char

            Dim i As Integer = 0

            Do While i < 30
                mese(i) = "0"

                i = i + 1
            Loop

            sql = "SELECT data_uscita, id_stazione_uscita, data_presunto_rientro As data_previsto_rientro, id_stazione_presunto_rientro As id_stazione_previsto_rientro "
            sql += "FROM movimenti_targa WITH(NOLOCK) WHERE (id_tipo_movimento='" & Costanti.idLavaggio & "') AND movimento_attivo='1' AND id_veicolo='" & id_veicolo & "'"

            'SE HO UN LAVAGGIO ATTIVO PER L'AUTO CONSIDERATA DEVO CONSIDERARLO IN OGNI CASO - NON FACCIO CONTROLLI SULLE DATE
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Do While Rs.Read()
                Dim data_uscita As DateTime = CDate(funzioni_comuni.getDataDb_senza_orario2(Rs("data_uscita")))
                Dim data_presunto_rientro As DateTime = CDate(funzioni_comuni.getDataDb_senza_orario2(Rs("data_previsto_rientro")))

                'NEL PRIMO CASO LA STAZIONE DI USCITA COINCIDE CON QUELLA DI RIENTRO ED E' LA STESSA DELLA STAZIONE SELEZIONATA NELLA FUNZIONALITA'.
                If (Rs("id_stazione_previsto_rientro") = Rs("id_stazione_uscita")) And (Rs("id_stazione_previsto_rientro") = stazioneAttuale) Then
                    'CASO 1: PER TUTTO IL PERIODO LA MACCHINA NON E' DISPONIBILE

                    If DateDiff(DateInterval.Day, data1, data_uscita) <= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) >= 0 Then
                        i = 0
                        'Response.Write("F1")

                        Do While i < 30
                            mese(i) = "1"

                            i = i + 1
                        Loop
                    End If

                    'CASO 2: DATA DI USCITA PRECEDENTE A DATA INIZIALE; DATA RIENTRO COMPRESA TRA LE DUE DATE 
                    If DateDiff(DateInterval.Day, data1, data_uscita) <= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) <= 0 Then
                        i = 0

                        'Response.Write("F2")

                        Do While i <= DateDiff(DateInterval.Day, data1, data_presunto_rientro)
                            mese(i) = "1"

                            i = i + 1
                        Loop
                    End If


                    'CASO 3: DATA DI USCITA TRA LE DUE DATE; DATA DI RIENTRO SUCCESSIVA ALLA DATA FINALE
                    If DateDiff(DateInterval.Day, data1, data_uscita) >= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) >= 0 Then
                        i = 0

                        'Response.Write("F3")

                        Do While i < 30
                            If i >= DateDiff(DateInterval.Day, data1, data_uscita) Then
                                mese(i) = "1"
                            End If

                            i = i + 1
                        Loop
                    End If

                    'CASO 3: DATA DI USCITA TRA LE DUE DATE; DATA DI RIENTRO TRA LE DUE DATE
                    If DateDiff(DateInterval.Day, data1, data_uscita) >= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) < 0 Then
                        i = 0

                        'Response.Write("F4")

                        Do While i < 30
                            If i >= DateDiff(DateInterval.Day, data1, data_uscita) And i <= DateDiff(DateInterval.Day, data1, data2) - DateDiff(DateInterval.Day, data_presunto_rientro, data2) Then
                                mese(i) = "1"
                            End If

                            i = i + 1
                        Loop
                    End If
                End If
                'CASO4: PRESUNTO RIENTRO PRECEDENTE ALLA DATA ODIERNA - IN QUESTO CASO NON HO IDEA QUANDO IL VEICOLO DOVREBBE RIENTRARE PER 
                'CUI LO CONSIDERO SEMPRE NOLEGGIATA FINO A NUOVA COMUNICAZIONE DEL CLIENTE (SOLO NEL CASO IN CUI NON HO TROVATO NIENT'ALTO SOPRA)

                If DateDiff(DateInterval.Day, data1, data_uscita) < 0 And DateDiff(DateInterval.Day, data1, data_presunto_rientro) < 0 Then
                    i = 0
                    Do While i < 30

                        mese(i) = "3"

                        i = i + 1
                    Loop
                End If
                '----------------------------------------------------------------------------------------------------------------------------
            Loop


            getLavaggi = mese

            Rs.Close()
            Rs = Nothing

            Cmd.Dispose()
            Cmd = Nothing

            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error_GetLavaggi_:" & ex.Message & "<br/>" & sql & "<br/>")
        End Try



    End Function

    Protected Function getContratti(ByVal id_veicolo As String, ByVal dataIniziale As DateTime, ByVal dataFinale As DateTime, ByVal stazioneAttuale As String) As Char()
        'RISULTATO: 0 - DISPONIBILE
        'RISULTATO: 1 - CONTRATTO IN CORSO
        'RISULTATO: 2 - RIENTRO IN ALTRA STAZIONE 
        'RISULTATO: 3 - CONTRATTO IN CORSO CON RIENTRO PREVISTO PRECEDENTE LA DATA ODIERNA
        'RISULTATO: 4 - CRV - ATTESA SOSTITUZIONE

        Dim sql As String = ""

        Try



            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim data_iniziale As String = funzioni_comuni.getDataDb_senza_orario(dataIniziale)
            Dim data_finale As String = funzioni_comuni.getDataDb_senza_orario(dataFinale)

            Dim data1 As DateTime = dataIniziale
            Dim data2 As DateTime = dataFinale

            Dim mese(30) As Char

            Dim i As Integer = 0

            Do While i < 30
                mese(i) = "0"

                i = i + 1
            Loop

            Dim contratto_trovato As Boolean = False

            'SE HO UN CONTRATTO ATTIVO PER L'AUTO CONSIDERATA DEVO CONSIDERARLO IN OGNI CASO - NON FACCIO CONTROLLI SULLE DATE
            sql = "SELECT id, data_uscita, id_stazione_uscita, data_presunto_rientro As data_previsto_rientro, id_stazione_presunto_rientro As id_stazione_previsto_rientro "
            sql += "FROM contratti WITH(NOLOCK) "
            sql += "WHERE (status='" & Costanti.id_contratto_aperto & "' OR status='" & Costanti.id_contratto_quick_check_in & "') AND (attivo='1') AND id_veicolo='" & id_veicolo & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Do While Rs.Read()
                If (Rs("id") & "") <> "" Then
                    contratto_trovato = True
                End If

                Dim data_uscita As DateTime = CDate(funzioni_comuni.getDataDb_senza_orario2(Rs("data_uscita")))                         'modificato 20.11.2020 
                Dim data_presunto_rientro As DateTime = CDate(funzioni_comuni.getDataDb_senza_orario2(Rs("data_previsto_rientro")))

                'NEL PRIMO CASO LA STAZIONE DI USCITA COINCIDE CON QUELLA DI RIENTRO ED E' LA STESSA DELLA STAZIONE SELEZIONATA NELLA FUNZIONALITA'.
                If (Rs("id_stazione_previsto_rientro") = Rs("id_stazione_uscita")) And (Rs("id_stazione_previsto_rientro") = stazioneAttuale) Then
                    'CASO 1: PER TUTTO IL PERIODO LA MACCHINA NON E' DISPONIBILE

                    If DateDiff(DateInterval.Day, data1, data_uscita) <= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) >= 0 Then
                        i = 0

                        'Response.Write("F1")

                        Do While i < 30
                            mese(i) = "1"

                            i = i + 1
                        Loop
                    End If

                    'CASO 2: DATA DI USCITA PRECEDENTE A DATA INIZIALE; DATA RIENTRO COMPRESA TRA LE DUE DATE 
                    If DateDiff(DateInterval.Day, data1, data_uscita) <= 0 And DateDiff(DateInterval.Day, data1, data_presunto_rientro) >= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) <= 0 Then
                        i = 0

                        'Response.Write("F2")

                        Do While i <= DateDiff(DateInterval.Day, data1, data_presunto_rientro)
                            mese(i) = "1"

                            i = i + 1
                        Loop
                    End If


                    'CASO 3: DATA DI USCITA TRA LE DUE DATE; DATA DI RIENTRO SUCCESSIVA ALLA DATA FINALE
                    If DateDiff(DateInterval.Day, data1, data_uscita) >= 0 And DateDiff(DateInterval.Day, data2, data_uscita) <= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) >= 0 Then
                        i = 0

                        'Response.Write("F3")

                        Do While i < 30
                            If i >= DateDiff(DateInterval.Day, data1, data_uscita) Then
                                mese(i) = "1"
                            End If

                            i = i + 1
                        Loop
                    End If

                    'CASO 3: DATA DI USCITA TRA LE DUE DATE; DATA DI RIENTRO TRA LE DUE DATE
                    If DateDiff(DateInterval.Day, data1, data_uscita) >= 0 And DateDiff(DateInterval.Day, data2, data_uscita) <= 0 And DateDiff(DateInterval.Day, data1, data_presunto_rientro) >= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) <= 0 Then
                        i = 0

                        'Response.Write("F4")

                        Do While i < 30
                            If i >= DateDiff(DateInterval.Day, data1, data_uscita) And i <= DateDiff(DateInterval.Day, data1, data2) - DateDiff(DateInterval.Day, data_presunto_rientro, data2) Then
                                mese(i) = "1"
                            End If

                            i = i + 1
                        Loop
                    End If

                    'CASO4: PRESUNTO RIENTRO PRECEDENTE ALLA DATA ODIERNA - IN QUESTO CASO NON HO IDEA QUANDO IL VEICOLO DOVREBBE RIENTRARE PER 
                    'CUI LO CONSIDERO SEMPRE NOLEGGIATA FINO A NUOVA COMUNICAZIONE DEL CLIENTE (SOLO NEL CASO IN CUI NON HO TROVATO NIENT'ALTO SOPRA)

                    If DateDiff(DateInterval.Day, data1, data_uscita) < 0 And DateDiff(DateInterval.Day, data1, data_presunto_rientro) < 0 Then
                        i = 0
                        Do While i < 30

                            mese(i) = "3"

                            i = i + 1
                        Loop
                    End If
                End If

                '----------------------------------------------------------------------------------------------------------------------------
                'NEL SECONDO CASO IL VEICOLO ESCE DALLA STAZIONE CONSIDERATA MA RIENTRA IN UN'ALTRA STAZIONE
                If (Rs("id_stazione_previsto_rientro") <> Rs("id_stazione_uscita")) And (Rs("id_stazione_uscita") = stazioneAttuale) Then

                    'CASO 1: DATA DI USCITA PRECEDENTE A DATA INIZIALE - DATA DI PRESUNTO RIENTRO TRA LE DUE DATE 
                    If DateDiff(DateInterval.Day, data1, data_uscita) <= 0 And DateDiff(DateInterval.Day, data1, data_presunto_rientro) >= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) < 0 Then
                        i = 0
                        Do While i < 30
                            If i >= DateDiff(DateInterval.Day, data1, data_uscita) And i <= (DateDiff(DateInterval.Day, data1, CDate(data_presunto_rientro))) Then
                                mese(i) = "1"
                            ElseIf i > (DateDiff(DateInterval.Day, data1, CDate(data_presunto_rientro))) Then
                                mese(i) = "2"
                            End If

                            i = i + 1
                        Loop
                    End If
                    'CASO 2: DATA DI USCITA PRECEDENTE A DATA INIZIALE - DATA DI PRESUNTO RIENTRO SUCCESSIVA ALLA DATA FINALE
                    If DateDiff(DateInterval.Day, data1, data_uscita) <= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) >= 0 Then
                        i = 0
                        Do While i < 30
                            mese(i) = "1"

                            i = i + 1
                        Loop
                    End If

                    'CASO3: PRESUNTO RIENTRO PRECEDENTE ALLA DATA ODIERNA - IN QUESTO CASO NON HO IDEA QUANDO IL VEICOLO DOVREBBE RIENTRARE PER 
                    'CUI LO CONSIDERO SEMPRE NOLEGGIATA FINO A NUOVA COMUNICAZIONE DEL CLIENTE (SOLO NEL CASO IN CUI NON HO TROVATO NIENT'ALTO SOPRA)

                    If DateDiff(DateInterval.Day, data1, data_uscita) < 0 And DateDiff(DateInterval.Day, data1, data_presunto_rientro) < 0 Then
                        i = 0
                        Do While i < 30

                            mese(i) = "3"

                            i = i + 1
                        Loop
                    End If
                End If
                'NEL TERZO CASO IL VEICOLO ESCE DA UN'ALTRA STAZIONE E RIENTRA IN QUELLA CONSIDERATA
                If (Rs("id_stazione_previsto_rientro") <> Rs("id_stazione_uscita")) And (Rs("id_stazione_previsto_rientro") = stazioneAttuale) Then
                    'CASO 1: DATA DI USCITA PRECEDENTE A DATA INIZIALE - DATA DI PRESUNTO RIENTRO TRA LE DUE DATE 
                    If DateDiff(DateInterval.Day, data1, data_uscita) <= 0 And DateDiff(DateInterval.Day, data1, data_presunto_rientro) >= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) < 0 Then
                        i = 0
                        Do While i < 30
                            If i >= DateDiff(DateInterval.Day, data1, data_uscita) And i <= (DateDiff(DateInterval.Day, data1, CDate(data_presunto_rientro))) Then
                                mese(i) = "2"
                            ElseIf i > (DateDiff(DateInterval.Day, data1, CDate(data_presunto_rientro))) Then
                                mese(i) = "0"
                            End If

                            i = i + 1
                        Loop
                    End If
                    'CASO 2: DATA DI USCITA PRECEDENTE A DATA INIZIALE - DATA DI PRESUNTO RIENTRO SUCCESSIVA ALLA DATA FINALE
                    If DateDiff(DateInterval.Day, data1, data_uscita) <= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) >= 0 Then
                        i = 0
                        Do While i < 30
                            mese(i) = "2"

                            i = i + 1
                        Loop
                    End If

                    If DateDiff(DateInterval.Day, data1, data_uscita) < 0 And DateDiff(DateInterval.Day, data1, data_presunto_rientro) < 0 Then
                        i = 0
                        Do While i < 30

                            mese(i) = "3"

                            i = i + 1
                        Loop
                    End If
                End If
            Loop

            If Not contratto_trovato Then
                'SE NON SONO ENTRATO NEL CICLO PRECEDENTE (NESSUN CONTRATTO ATTIVO PER L'AUTO CONSIDERATA) CERCO TRA I CRV PER CUI NON E' ANCORA
                'STATO EFFETTUATO IL CHECK OUT - UN'AUTO IN QUESTO STATO E' CONSIDERATA NON DISPONIBILE A TEMPO INDETERMINATO QUINDI NON VIENE FATTO
                'ALCUN CONTROLLO SULLE DATE
                Dbc.Close()
                Dbc.Open()

                sql = "SELECT id FROM contratti_crv_veicoli WHERE id_veicolo='" & id_veicolo & "' AND check_in_effettuato='0'"
                Cmd = New Data.SqlClient.SqlCommand(sql, Dbc)
                Rs = Cmd.ExecuteReader()

                If Rs.Read() Then
                    If (Rs("id") & "") <> "" Then
                        i = 0
                        Do While i < 30
                            mese(i) = "4"

                            i = i + 1
                        Loop
                    End If
                End If
            End If

            getContratti = mese

            Rs.Close()
            Rs = Nothing

            Cmd.Dispose()
            Cmd = Nothing

            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error_GetContratti_:" & ex.Message & "<br/>" & sql & "<br/>")
        End Try



    End Function

    Protected Function getPrenotazioni(ByVal targa As String, ByVal dataIniziale As DateTime, ByVal dataFinale As DateTime, ByVal stazioneAttuale As String) As Char()

        'ok verificato 20.11.2020 10.32

        'RISULTATO: 0 - DISPONIBILE
        'RISULTATO: 1 - PRENOTATA
        'RISULTATO: 2 - IN ALTRA STAZIONE (PRIMA O DOPO LA PRENOTAZIONE)

        Dim sql As String = ""


        Try
            'NELLE PRENOTAZIONI LA TARGA VIENE ASSEGNATA PROVVISORIAMENTE SENZA PARTICOLARI CONTROLLI
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim data_iniziale As String = funzioni_comuni.getDataDb_senza_orario(dataIniziale)
            Dim data_finale As String = funzioni_comuni.getDataDb_senza_orario(dataFinale)

            Dim data1 As DateTime = dataIniziale
            Dim data2 As DateTime = dataFinale

            Dim mese(30) As Char

            Dim i As Integer = 0

            Do While i < 30
                mese(i) = "0"

                i = i + 1
            Loop

            sql = "SELECT PRDATA_OUT As data_uscita, PRID_stazione_out As id_stazione_uscita, PRDATA_PR As data_previsto_rientro, PRID_stazione_pr As id_stazione_previsto_rientro "
            sql += "FROM prenotazioni WITH(NOLOCK) "
            sql += "WHERE ((convert(datetime,'" & data_iniziale & "',102) BETWEEN PRDATA_OUT AND PRDATA_PR) OR (convert(datetime,'" & data_finale & "',102) "
            sql += "BETWEEN PRDATA_OUT And PRDATA_PR) Or (PRDATA_OUT <= convert(datetime,'" & data_iniziale & "',102) AND PRDATA_PR >= convert(datetime,'" & data_finale & "',102)) "
            sql += "Or (convert(datetime,'" & data_iniziale & "',102) <= PRDATA_OUT AND convert(datetime,'" & data_finale & "',102) >= PRDATA_PR)) AND (targa_gruppo_speciale='" & targa & "') AND (status='0') AND attiva=1"



            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Do While Rs.Read()
                Dim data_uscita As DateTime = CDate(funzioni_comuni.getDataDb_senza_orario2(Rs("data_uscita")))
                Dim data_presunto_rientro As DateTime = CDate(funzioni_comuni.getDataDb_senza_orario2(Rs("data_previsto_rientro")))

                'NEL PRIMO CASO LA STAZIONE DI USCITA COINCIDE CON QUELLA DI RIENTRO ED E' LA STESSA DELLA STAZIONE SELEZIONATA NELLA FUNZIONALITA'.
                If (Rs("id_stazione_previsto_rientro") = Rs("id_stazione_uscita")) And (Rs("id_stazione_previsto_rientro") = stazioneAttuale) Then
                    'CASO 1: PER TUTTO IL PERIODO LA MACCHINA NON E' DISPONIBILE
                    If DateDiff(DateInterval.Day, data1, data_uscita) <= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) >= 0 Then
                        i = 0

                        'Response.Write("F1")

                        Do While i < 30
                            mese(i) = "1"

                            i = i + 1
                        Loop
                    End If

                    'CASO 2: DATA DI USCITA PRECEDENTE A DATA INIZIALE; DATA RIENTRO COMPRESA TRA LE DUE DATE - IN QUESTO CASO LA PRENOTAZIONE
                    'E' SCADUTA E QUINDI NON DEVE ESSERE CONSIDERATA
                    'If DateDiff(DateInterval.Day, data1, data_uscita) <= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) < 0 Then
                    'i = 0

                    ''Response.Write("F2")

                    'Do While i <= DateDiff(DateInterval.Day, data1, data_presunto_rientro)
                    '    mese(i) = "1"

                    '    i = i + 1
                    'Loop
                    'End If


                    'CASO 3: DATA DI USCITA TRA LE DUE DATE; DATA DI RIENTRO SUCCESSIVA ALLA DATA FINALE
                    If DateDiff(DateInterval.Day, data1, data_uscita) >= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) >= 0 Then
                        i = 0

                        'Response.Write("F3")

                        Do While i < 30
                            If i >= DateDiff(DateInterval.Day, data1, data_uscita) Then
                                mese(i) = "1"
                            End If

                            i = i + 1
                        Loop
                    End If

                    'CASO 3: DATA DI USCITA TRA LE DUE DATE; DATA DI RIENTRO TRA LE DUE DATE
                    If DateDiff(DateInterval.Day, data1, data_uscita) >= 0 And DateDiff(DateInterval.Day, data2, data_presunto_rientro) < 0 Then
                        i = 0

                        'Response.Write("F4")

                        Do While i < 30
                            If i >= DateDiff(DateInterval.Day, data1, data_uscita) And i <= DateDiff(DateInterval.Day, data1, data2) - DateDiff(DateInterval.Day, data_presunto_rientro, data2) Then
                                mese(i) = "1"
                            End If

                            i = i + 1
                        Loop
                    End If
                End If
                '----------------------------------------------------------------------------------------------------------------------------
                'NEL SECONDO CASO IL VEICOLO ESCE DALLA STAZIONE CONSIDERATA MA RIENTRA IN UN'ALTRA STAZIONE
                If (Rs("id_stazione_previsto_rientro") <> Rs("id_stazione_uscita")) And (Rs("id_stazione_uscita") = stazioneAttuale) Then

                    'CASO 1: DATA DI USCITA PRECEDENTE A DATA INIZIALE - NEL CASO DI PRENOTAZIONE LA STESSA (NON TRASFORMATA IN CONTRATTO)
                    'RISULTA SCADUTA - QUINDI NON SI CONSIDERA
                    If DateDiff(DateInterval.Day, data1, data_uscita) < 0 Then

                    End If
                    'CASO 2: DATA DI USCITA SUCCESSIVA A DATA INIZIALE; 
                    If DateDiff(DateInterval.Day, data1, data_uscita) >= 0 Then
                        i = 0

                        Do While i < 30
                            If i >= DateDiff(DateInterval.Day, data1, data_uscita) And i <= (DateDiff(DateInterval.Day, data1, CDate(data_presunto_rientro))) Then
                                mese(i) = "1"
                            ElseIf i > (DateDiff(DateInterval.Day, data1, CDate(data_presunto_rientro))) Then
                                mese(i) = "2"
                            End If

                            i = i + 1
                        Loop
                    End If


                End If
            Loop

            getPrenotazioni = mese

            Rs.Close()
            Rs = Nothing

            Cmd.Dispose()
            Cmd = Nothing

            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_GetPrenotazioni_:" & ex.Message & "<br/>" & sql & "<br/>")
        End Try


    End Function

    Protected Function getLavoriIntervento(ByVal idVeicolo As String, ByVal dataIniziale As DateTime, ByVal dataFinale As DateTime, ByVal stazioneAttuale As String) As Char()
        'ok verificato 20.11.2020 10.32
        'RISULTATO: 0 - DISPONIBILE
        'RISULTATO: 1 - PRENOTATA
        'RISULTATO: 2 - IN ALTRA STAZIONE (PRIMA O DOPO LA PRENOTAZIONE)

        Dim sql As String = ""
        Try

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim data1 As String = funzioni_comuni.getDataDb_con_orario(dataIniziale & " 00:00:00")
            Dim data2 As String = funzioni_comuni.getDataDb_senza_orario(dataFinale & " 23:59:59")


            Dim mese(30) As Char

            Dim i As Integer = 0

            Do While i < 30
                mese(i) = "0"

                i = i + 1
            Loop


            sql = "SELECT data_uscita, ISNULL(id_stazione_uscita,(SELECT id_stazione FROM veicoli with(nolock) "
            sql += "WHERE id = " & idVeicolo & ")) As id_stazione_uscita, data_previsto_rientro, id_stazione_previsto_rientro "

            sql += "FROM odl WITH(NOLOCK) WHERE ((convert(datetime,'" & data1 & "',102) BETWEEN CAST(CAST(data_uscita As date) As datetime) AND data_previsto_rientro) "
            'sql += "FROM odl WITH(NOLOCK) WHERE (('" & data1 & "' BETWEEN CONVERT(DATETIME,data_uscita,102) AND CONVERT(DATETIME,data_previsto_rientro,102)) "

            sql += "Or (convert(datetime,'" & data2 & "',102) BETWEEN CAST(CAST(data_uscita As date) As datetime) AND data_previsto_rientro) "
            'sql += "Or ('" & data2 & "' BETWEEN CONVERT(DATETIME, data_uscita, As datetime),102) AND CONVERT(DATETIME,data_previsto_rientro,102)) "

            sql += "Or (CAST(CAST(data_uscita As date) As datetime) <= convert(datetime,'" & data1 & "',102) AND data_previsto_rientro >= convert(datetime,'" & data2 & "',102)) "
            'sql += "Or (CAST(CAST(data_uscita As date) As datetime) <= CONVERT(DATETIME,'" & data1 & "',102) AND data_previsto_rientro >= CONVERT(DATETIME,'" & data2 & "',102)) "

            sql += "Or (convert(datetime,'" & data1 & "',102) <= CAST(CAST(data_uscita As date) As datetime) AND convert(datetime,'" & data2 & "',102) >= data_previsto_rientro) "
            'sql += "Or ('" & data1 & "' <= CONVERT(DATETIME,data_uscita, As datetime),102) AND '" & data2 & "' >= CONVERT(DATETIME,data_previsto_rientro,102)) "

            sql += "Or (CAST(CAST(data_uscita As date) As datetime) <= convert(datetime,'" & data1 & "',102))) "
            'sql += "Or (CAST(CAST(data_uscita As date) As datetime) <= CONVERT(DATETIME,'" & data1 & "',102))) "


            sql += "And (id_veicolo ='" & idVeicolo & "') AND odl.attivo=1 AND odl.id_stato_odl<>9"



            'Response.Write(sql & "<br>")
            'Response.End()


            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)




            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Do While Rs.Read()


                Dim data_uscita As DateTime = CDate(funzioni_comuni.getDataDb_senza_orario2(Rs("data_uscita")))

                If (Rs("data_previsto_rientro") & "") <> "" Then

                    Dim data_presunto_rientro As DateTime = CDate(funzioni_comuni.getDataDb_senza_orario2(Rs("data_previsto_rientro"))) 'modificato 

                    'NEL PRIMO CASO LA STAZIONE DI USCITA COINCIDE CON QUELLA DI RIENTRO ED E' LA STESSA DELLA STAZIONE SELEZIONATA NELLA FUNZIONALITA'.
                    If ((Rs("id_stazione_previsto_rientro") & "") = (Rs("id_stazione_uscita") & "")) And ((Rs("id_stazione_previsto_rientro") & "") = stazioneAttuale) Then



                        'CASO 1: PER TUTTO IL PERIODO LA MACCHINA NON E' DISPONIBILE
                        If DateDiff(DateInterval.Day, CDate(dataIniziale), data_uscita) <= 0 And DateDiff(DateInterval.Day, CDate(dataFinale), data_presunto_rientro) >= 0 Then
                            i = 0

                            Do While i < 30
                                mese(i) = "1"

                                i = i + 1
                            Loop
                        End If

                        'CASO 2: DATA DI USCITA PRECEDENTE A DATA INIZIALE; DATA RIENTRO COMPRESA TRA LE DUE DATE
                        If DateDiff(DateInterval.Day, CDate(dataIniziale), data_uscita) <= 0 And DateDiff(DateInterval.Day, CDate(dataFinale), data_presunto_rientro) < 0 Then
                            i = 0

                            'Response.Write("F2")

                            Do While i <= DateDiff(DateInterval.Day, CDate(dataIniziale), data_presunto_rientro)
                                mese(i) = "1"

                                i = i + 1
                            Loop
                        End If


                        'CASO 3: DATA DI USCITA TRA LE DUE DATE; DATA DI RIENTRO SUCCESSIVA ALLA DATA FINALE
                        If DateDiff(DateInterval.Day, CDate(dataIniziale), data_uscita) > 0 And DateDiff(DateInterval.Day, CDate(dataFinale), data_presunto_rientro) >= 0 Then
                            i = 0

                            'Response.Write("F3")

                            Do While i < 30
                                If i >= DateDiff(DateInterval.Day, CDate(dataIniziale), data_uscita) Then
                                    mese(i) = "1"
                                End If

                                i = i + 1
                            Loop
                        End If

                        'CASO 4: DATA DI USCITA TRA LE DUE DATE; DATA DI RIENTRO TRA LE DUE DATE
                        If DateDiff(DateInterval.Day, CDate(dataIniziale), data_uscita) > 0 And DateDiff(DateInterval.Day, CDate(dataFinale), data_presunto_rientro) < 0 Then
                            i = 0

                            'Response.Write("F4")

                            Do While i < 30
                                If i >= DateDiff(DateInterval.Day, CDate(dataIniziale), data_uscita) And i <= DateDiff(DateInterval.Day, dataIniziale, dataFinale) - DateDiff(DateInterval.Day, data_presunto_rientro, CDate(dataFinale)) Then
                                    mese(i) = "1"
                                End If

                                i = i + 1
                            Loop
                        End If

                        'CASO 5: PRESUNTO RIENTRO PRECEDENTE ALLA DATA ODIERNA - IN QUESTO CASO NON HO IDEA QUANDO IL VEICOLO DOVREBBE RIENTRARE PER 
                        If DateDiff(DateInterval.Day, CDate(dataIniziale), data_uscita) < 0 And DateDiff(DateInterval.Day, CDate(dataIniziale), data_presunto_rientro) < 0 Then
                            i = 0
                            Do While i < 30

                                mese(i) = "3"

                                i = i + 1
                            Loop
                        End If
                    End If
                    '----------------------------------------------------------------------------------------------------------------------------
                    'NEL SECONDO CASO IL VEICOLO ESCE DALLA STAZIONE CONSIDERATA MA RIENTRA IN UN'ALTRA STAZIONE
                    If ((Rs("id_stazione_previsto_rientro") & "") <> (Rs("id_stazione_uscita") & "")) And ((Rs("id_stazione_uscita") & "") = stazioneAttuale) Then
                        i = 0
                        Response.Write(dataIniziale & " " & data_uscita)



                        Do While i < 30
                            If i >= DateDiff(DateInterval.Day, CDate(dataIniziale), data_uscita) And i < DateDiff(DateInterval.Day, CDate(dataIniziale), CDate(data_presunto_rientro)) + 1 Then
                                mese(i) = "1"
                            ElseIf i >= DateDiff(DateInterval.Day, CDate(dataIniziale), CDate(data_presunto_rientro)) Then
                                mese(i) = "2"
                            End If

                            i = i + 1
                        Loop



                    End If


                Else
                    'IN QUESTO CASO LA STAZIONE E LA DATA DI PREVISTO RIENTRO SONO SCONOSCIUTI
                    If ((Rs("id_stazione_previsto_rientro") & "") = "" And (Rs("data_previsto_rientro") & "") = "") And ((Rs("id_stazione_uscita") & "") = stazioneAttuale) Then
                        i = 0

                        'Response.Write("F1")

                        Do While i < 30
                            mese(i) = "1"

                            i = i + 1
                        Loop
                    End If
                End If



            Loop

            getLavoriIntervento = mese

            Rs.Close()
            Rs = Nothing

            Cmd.Dispose()
            Cmd = Nothing

            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_getLavoriIntervento_:" & ex.Message & "<br/>" & sql & "<br/>")
        End Try




    End Function



    Protected Sub btnVisualizza_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVisualizza.Load

    End Sub
End Class
