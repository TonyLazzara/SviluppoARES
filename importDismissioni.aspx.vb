Imports System.IO

Partial Class importDismissioni
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.Cookies("SicilyRentCar") Is Nothing Then
                Response.Redirect("LogIn.aspx")
            Else
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 84) = "3" Then
                    'AzzeraTab()
                    'PanelDatiGenerali.Visible = True
                    If Request.QueryString("ritorno") = "true" Then
                        LblImportFile.Text = "CaricaFile"
                        Dim nome_file As String = Session("nome_file")
                        Session("nome_file") = ""
                        CaricaFoglioExcel(nome_file)
                    End If
                Else
                    Response.Redirect("default.aspx")
                End If
            End If
        End If
    End Sub

    Public Sub CancellaTabAppoggioDismissioni()
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim cmd As New Data.SqlClient.SqlCommand("DELETE FROM dismissioni_appoggio WHERE id_utente='" & Request.Cookies("SicilyRentCar")("idUtente") & "'", Dbc)
        cmd.ExecuteNonQuery()

        cmd.Dispose()
        cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Public Function CaricaRigaInDB(ByVal RigaStringa As String) As Boolean
        'RESTITUISCE SE LA RIGA E' CORRETTA
        Dim RigaStrApp(1) As String

        Dim id_veicolo As String
        Dim veicolo_venduto As String

        Dim veicolo_is_leasing As Boolean

        RigaStrApp = Split(RigaStringa, ";")

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim riga_ok As Boolean = True

        'CONTROLLI SULLA TARGA--------------------------------------------------------------------------------------------------------
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM veicoli where targa='" & Replace(Trim(RigaStrApp(0)), "'", "''") & "'", Dbc)
        id_veicolo = Cmd.ExecuteScalar

        If id_veicolo = "" Then
            id_veicolo = "NULL"
            veicolo_venduto = "NULL"

            riga_ok = False
        Else
            id_veicolo = "'" & id_veicolo & "'"
        End If

        'CONTROLLO SE IL VEICOLO E' GIA' VENDUTO
        If id_veicolo <> "NULL" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT venduta FROM veicoli where id=" & id_veicolo & "", Dbc)
            veicolo_venduto = "'" & Cmd.ExecuteScalar & "'"

            If veicolo_venduto = "'True'" Then
                riga_ok = False
            End If
        End If
        '------------------------------------------------------------------------------------------------------------------------------
        'CONTROLLO SULL'ESISTENZA DEL NUMERO DI BOLLA (obbligatorio)-------------------------------------------------------------------
        Dim numero_bolla As String
        If Trim(RigaStrApp(1)) <> "" Then
            numero_bolla = "'" & Replace(RigaStrApp(1), "'", "''") & "'"
        Else
            numero_bolla = "NULL"

            riga_ok = False
        End If
        '------------------------------------------------------------------------------------------------------------------------------
        'DATA DDT - CAMPO OBBLIGATORIO - DEVE ESSERE UNA DATA VALIDA-------------------------------------------------------------------
        Dim data_ddt As String
        If Trim(RigaStrApp(2)) = "" Then
            data_ddt = "NULL"

            riga_ok = False
        Else
            data_ddt = "'" & Replace(Trim(RigaStrApp(2)), "'", "''") & "'"
            Dim test As String
            Try
                test = funzioni_comuni.getDataDb_senza_orario(Trim(RigaStrApp(2)))
            Catch ex As Exception
                riga_ok = False
            End Try
        End If
        '------------------------------------------------------------------------------------------------------------------------------
        'I CONTROLLI PROSEGUONO DIVERSAMENTE NEL CASO IN CUI IL VEICOLO SIA IN LEASING: IN QUESTO CASO GLI ALTRI CAMPI DEVONO ESSERE VUOTI
        'A PARTE DATA ATTO DI VENDITA CHE PERO' DEVE ESSERE UGUALE A DATA DDT----------------------------------------------------------
        Dim leasing_si_no As String
        Dim leasing As Boolean

        If Trim(RigaStrApp(7)) = "" Then
            leasing_si_no = "NULL"
            riga_ok = False

            leasing = False
        ElseIf UCase(Trim(RigaStrApp(7))) = "SI" Or UCase(Trim(RigaStrApp(7))) = "NO" Then
            leasing_si_no = "'" & Trim(RigaStrApp(7)) & "'"

            If Trim(RigaStrApp(7)) = "SI" Then
                leasing = True
            Else
                leasing = False
            End If
        Else
            leasing_si_no = "'" & Replace(Trim(RigaStrApp(7)), "'", "''") & "'"
            riga_ok = False

            leasing = False
        End If
        '----------------------------------------------------------------------------------------------------------------------------------
        'SE IL VEICOLO E' STATO TROVATO CONTROLLO SE L'AUTO EFFETTIVAMENTE E' O NON E' IN LEASING------------------------------------------
        If id_veicolo <> "NULL" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM veicoli where id=" & id_veicolo & " AND (ISNULL(auto_in_leasing,'False')='True' OR ISNULL(auto_in_leasing_SBC,'False')='True') ", Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test <> "" Then
                veicolo_is_leasing = True
            Else
                veicolo_is_leasing = False
            End If


            If leasing <> veicolo_is_leasing Then
                riga_ok = False
            End If
        Else
            veicolo_is_leasing = False
        End If
        '----------------------------------------------------------------------------------------------------------------------------------
        'DATA ATTO DI VENDITA - DEVE ESSERE UNA DATA VALIDA - CAMPO NON OBBLIGATORIO-------------------------------------------------------
        Dim data_atto_vendita As String
        If Trim(RigaStrApp(4)) = "" Then
            data_atto_vendita = "NULL"
        Else
            'PER PRIMA COSA CONTROLLO LA CORRETTEZZA DEL DATO - SE E' CORRETTO NEL CASO DI LEASING CONTROLLO SE E' UGUALE ALLA DATA DDT
            data_atto_vendita = "'" & Replace(Trim(RigaStrApp(4)), "'", "''") & "'"
            Dim test As String
            Try
                test = funzioni_comuni.getDataDb_senza_orario(Trim(RigaStrApp(4)))
                If leasing Then
                    If Trim(RigaStrApp(2)) <> Trim(RigaStrApp(4)) Then
                        riga_ok = False
                    End If
                End If
            Catch ex As Exception
                riga_ok = False
            End Try
        End If

        '----------------------------------------------------------------------------------------------------------------------------------
        'VENDITORE (DA NON VALORIZZARE PER LEASING - OBBLIGATORIO ALTRIMENTI)--------------------------------------------------------------
        Dim venditore As String
        Dim id_venditore As String
        If leasing And Trim(RigaStrApp(5)) <> "" Then
            riga_ok = False

            id_venditore = "NULL"
            venditore = "'" & Replace(Trim(RigaStrApp(5)), "'", "''") & "'"
        ElseIf leasing And Trim(RigaStrApp(5)) = "" Then
            id_venditore = "NULL"
            venditore = "NULL"
        ElseIf Not leasing And Trim(RigaStrApp(5)) = "" Then
            riga_ok = False

            id_venditore = "NULL"
            venditore = "NULL"
        Else
            'NON E' LEASING E C'E' UN DATO - CONTROLLO CHE CORRISPONDE AD UN VENDITORE SALVATO
            venditore = "'" & Replace(Trim(RigaStrApp(5)), "'", "''") & "'"
            Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM venditori WHERE nome='" & Replace(Trim(RigaStrApp(5)), "'", "''") & "'", Dbc)
            id_venditore = Cmd.ExecuteScalar & ""
            If id_venditore = "" Then
                id_venditore = "NULL"

                riga_ok = False
            Else
                id_venditore = "'" & id_venditore & "'"
            End If
        End If

        '----------------------------------------------------------------------------------------------------------------------------------
        'PRESSO_DI (SEMPRE OBBLIGATORIO)--------------------------------------------------------------
        Dim presso_di As String
        Dim id_presso_di As String

        If Trim(RigaStrApp(3)) = "" Then
            id_presso_di = "NULL"
            presso_di = "NULL"
            riga_ok = False
        Else
            'C'E' UN DATO - CONTROLLO CHE CORRISPONDE AD UN VENDITORE SALVATO
            presso_di = "'" & Replace(Trim(RigaStrApp(3)), "'", "''") & "'"
            Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM acquirenti_veicoli WHERE rag_soc='" & Replace(Trim(RigaStrApp(3)), "'", "''") & "'", Dbc)
            id_presso_di = Cmd.ExecuteScalar & ""
            If id_presso_di = "" Then
                id_presso_di = "NULL"

                riga_ok = False
            Else
                id_presso_di = "'" & id_presso_di & "'"
            End If
        End If
        'ACQUIRENTE (SEMPRE OBBLIGATORIO)--------------------------------------------------------------
        Dim acquirente As String
        Dim id_acquirente As String

        If Trim(RigaStrApp(6)) = "" Then
            id_acquirente = "NULL"
            acquirente = "NULL"
            riga_ok = False
        Else
            'C'E' UN DATO - CONTROLLO CHE CORRISPONDE AD UN VENDITORE SALVATO
            acquirente = "'" & Replace(Trim(RigaStrApp(6)), "'", "''") & "'"
            Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM acquirenti_veicoli WHERE rag_soc='" & Replace(Trim(RigaStrApp(6)), "'", "''") & "'", Dbc)
            id_acquirente = Cmd.ExecuteScalar & ""
            If id_acquirente = "" Then
                id_acquirente = "NULL"

                riga_ok = False
            Else
                id_acquirente = "'" & id_acquirente & "'"
            End If
        End If
        'FATTURE--------------------------------------------------------------------------------------------------------------------------
        'I FATTURA------------
        Dim numero_fattura_1 As String
        Dim data_fattura_1 As String
        Dim importo_fattura_1 As String

        If RigaStrApp(9).Trim = "" And RigaStrApp(10).Trim = "" And RigaStrApp(11).Trim = "" Then
            numero_fattura_1 = "NULL"
            data_fattura_1 = "NULL"
            importo_fattura_1 = "NULL"
        ElseIf RigaStrApp(9).Trim <> "" And RigaStrApp(10).Trim <> "" And RigaStrApp(11).Trim <> "" Then
            numero_fattura_1 = "'" & RigaStrApp(9).Trim.Replace("'", "''") & "'"

            Try
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario(RigaStrApp(10))
                data_fattura_1 = "'" & RigaStrApp(10) & "'"
            Catch ex As Exception
                data_fattura_1 = "'" & RigaStrApp(10) & "'"
                riga_ok = False
            End Try

            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(11))

                importo_fattura_1 = "'" & RigaStrApp(11) & "'"

                If test_dbl < 0 Then
                    riga_ok = False
                End If
            Catch ex As Exception
                importo_fattura_1 = "'" & Replace(RigaStrApp(11), "'", "''") & "'"
                riga_ok = False
            End Try

        Else
            numero_fattura_1 = "'" & RigaStrApp(9).Trim.Replace("'", "''") & "'"
            data_fattura_1 = "'" & RigaStrApp(10).Trim.Replace("'", "''") & "'"
            importo_fattura_1 = "'" & RigaStrApp(11).Trim.Replace("'", "''") & "'"
            riga_ok = False
        End If
        'II FATTURA------------
        Dim numero_fattura_2 As String
        Dim data_fattura_2 As String
        Dim importo_fattura_2 As String

        If RigaStrApp(12).Trim = "" And RigaStrApp(13).Trim = "" And RigaStrApp(14).Trim = "" Then
            numero_fattura_2 = "NULL"
            data_fattura_2 = "NULL"
            importo_fattura_2 = "NULL"
        ElseIf RigaStrApp(12).Trim <> "" And RigaStrApp(13).Trim <> "" And RigaStrApp(14).Trim <> "" Then
            numero_fattura_2 = "'" & RigaStrApp(12).Trim.Replace("'", "''") & "'"

            Try
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario(RigaStrApp(13))
                data_fattura_2 = "'" & RigaStrApp(13) & "'"
            Catch ex As Exception
                data_fattura_2 = "'" & RigaStrApp(13) & "'"
                riga_ok = False
            End Try

            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(14))

                importo_fattura_2 = "'" & RigaStrApp(14) & "'"

                If test_dbl < 0 Then
                    riga_ok = False
                End If
            Catch ex As Exception
                importo_fattura_2 = "'" & Replace(RigaStrApp(14), "'", "''") & "'"
                riga_ok = False
            End Try

        Else
            numero_fattura_2 = "'" & RigaStrApp(12).Trim.Replace("'", "''") & "'"
            data_fattura_2 = "'" & RigaStrApp(13).Trim.Replace("'", "''") & "'"
            importo_fattura_2 = "'" & RigaStrApp(14).Trim.Replace("'", "''") & "'"
            riga_ok = False
        End If
        'I NOTA DI CREDIO
        Dim numero_nc_1 As String
        Dim data_nc_1 As String
        Dim importo_nc_1 As String

        If RigaStrApp(15).Trim = "" And RigaStrApp(16).Trim = "" And RigaStrApp(17).Trim = "" Then
            numero_nc_1 = "NULL"
            data_nc_1 = "NULL"
            importo_nc_1 = "NULL"
        ElseIf RigaStrApp(15).Trim <> "" And RigaStrApp(16).Trim <> "" And RigaStrApp(17).Trim <> "" Then
            numero_nc_1 = "'" & RigaStrApp(15).Trim.Replace("'", "''") & "'"

            Try
                Dim test_data As String = funzioni_comuni.getDataDb_senza_orario(RigaStrApp(16))
                data_nc_1 = "'" & RigaStrApp(16) & "'"
            Catch ex As Exception
                data_nc_1 = "'" & RigaStrApp(16) & "'"
                riga_ok = False
            End Try

            Try
                Dim test_dbl As Double = CDbl(RigaStrApp(17))

                importo_nc_1 = "'" & RigaStrApp(17) & "'"

                If test_dbl < 0 Then
                    riga_ok = False
                End If
            Catch ex As Exception
                importo_nc_1 = "'" & Replace(RigaStrApp(17), "'", "''") & "'"
                riga_ok = False
            End Try
        Else
            numero_nc_1 = "'" & RigaStrApp(15).Trim.Replace("'", "''") & "'"
            data_nc_1 = "'" & RigaStrApp(16).Trim.Replace("'", "''") & "'"
            importo_nc_1 = "'" & RigaStrApp(17).Trim.Replace("'", "''") & "'"
            riga_ok = False
        End If
        '---------------------------------------------------------------------------------------------------------------------------------

        Dim sqlStr As String = "INSERT INTO dismissioni_appoggio (targa,id_veicolo,veicolo_venduto, DDT, data_ddt, leasing, veicolo_is_leasing, data_atto_di_vendita, venditore, id_venditore, presso_di, id_presso_di, acquirente, id_acquirente, numero_prima_fattura, data_prima_fattura, importo_prima_fattura, numero_seconda_fattura,data_seconda_fattura,importo_seconda_fattura,numero_prima_nc, data_prima_nc, importo_prima_nc, note, id_utente) VALUES " &
         "('" & Replace(RigaStrApp(0), "'", "''") & "'," & id_veicolo & "," & veicolo_venduto & "," & numero_bolla & "," & data_ddt & "," & leasing_si_no & ",'" & veicolo_is_leasing & "'," &
         data_atto_vendita & "," & venditore & "," & id_venditore & "," &
         presso_di & "," & id_presso_di & "," &
         acquirente & "," & id_acquirente & "," & numero_fattura_1 & "," & data_fattura_1 & "," & importo_fattura_1 & "," &
         numero_fattura_2 & "," & data_fattura_2 & "," & importo_fattura_2 & "," &
         numero_nc_1 & "," & data_nc_1 & "," & importo_nc_1 & "," &
         "'" & Replace(RigaStrApp(8), "'", "''") & "'," &
         "'" & Request.Cookies("SicilyRentCar")("idUtente") & "')"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteScalar()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()

        CaricaRigaInDB = riga_ok
        LblImportFile.Text = "CaricaFile"
    End Function

    Protected Sub CaricaFoglioExcel(Optional ByVal file_da_caricare As String = "")
        LblImportFile.Text = ""

        CancellaTabAppoggioDismissioni()

        Dim filePath As String
        filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\docs"

        Dim fileName As String

        If file_da_caricare = "" Then
            fileName = FileUpload2.FileName
        End If

        Dim FileTestoValorizzato As Boolean = False

        If FileUpload2.HasFile Or file_da_caricare <> "" Then
            If FileUpload2.FileName = "dismissioni.csv" Or file_da_caricare <> "" Then
                Dim objStreamReader As StreamReader
                If FileUpload2.HasFile Then
                    If File.Exists(filePath & "\" & fileName) Then
                        'Response.Write("IN")
                        'Response.End()
                        File.Delete(filePath & "\" & fileName)
                    End If
                    FileUpload2.SaveAs(filePath & "\" & fileName)
                    'Aprire un file per la lettura        
                    objStreamReader = File.OpenText(filePath & "\" & fileName)

                    nome_file.Text = filePath & "\" & fileName
                Else
                    'Aprire un file per la lettura        
                    objStreamReader = File.OpenText(file_da_caricare)
                    nome_file.Text = file_da_caricare
                End If
                Dim Riga As String
                Dim RigaStr(1) As String

                Riga = objStreamReader.ReadLine()

                Dim possibile_salvare As Boolean = True
                Dim test As Boolean

                While (objStreamReader.Peek() <> -1)
                    Riga = objStreamReader.ReadLine()
                    RigaStr = Split(Riga, ";")

                    test = CaricaRigaInDB(Riga)
                    If Not test Then
                        possibile_salvare = False
                    End If
                End While

                objStreamReader.Close()

                If possibile_salvare Then
                    Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()
                    Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc2.Open()
                    Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM dismissioni_appoggio", Dbc)
                    Dim Cmd2 As New Data.SqlClient.SqlCommand("", Dbc2)

                    Dim Rs As Data.SqlClient.SqlDataReader
                    Rs = Cmd.ExecuteReader()

                    Dim sqlStr As String = ""

                    Dim data_atto As String
                    Dim venduta_da_fattura As String
                    Dim totale_fatture As Double

                    Do While Rs.Read
                        'SE SONO STATE SPECIFICATE FATTURE E, CON LE NOTE, LA SOMMA E' MAGGIORE DI ZERO RECUPERO L'ATTUALE TOTALE DI FATTURE
                        'DELL'AUTO PER VEDERE SE DEVE ESSERE AGGIORNATO IL CAMPO venduta_da_fattura
                        venduta_da_fattura = ""
                        totale_fatture = 0

                        If (Rs("numero_prima_fattura") & "") <> "" Or (Rs("numero_seconda_fattura") & "") <> "" Or (Rs("numero_prima_nc") & "") <> "" Then
                            If (Rs("numero_prima_fattura") & "") <> "" Then
                                totale_fatture = totale_fatture + CDbl(Rs("importo_prima_fattura"))
                            End If
                            If (Rs("numero_seconda_fattura") & "") <> "" Then
                                totale_fatture = totale_fatture + CDbl(Rs("importo_seconda_fattura"))
                            End If
                            If (Rs("numero_prima_nc") & "") <> "" Then
                                totale_fatture = totale_fatture - CDbl(Rs("importo_prima_nc"))
                            End If

                            If totale_fatture <> 0 Then
                                totale_fatture = totale_fatture + getTotaleVendita(Rs("id_veicolo"))

                                If totale_fatture > 0 Then
                                    venduta_da_fattura = " venduta_da_fattura='1', "
                                ElseIf totale_fatture = 0 Then
                                    venduta_da_fattura = " venduta_da_fattura='0', "
                                End If
                            End If
                        End If


                        If UCase(Rs("leasing")) = "SI" Then
                            sqlStr = "UPDATE veicoli SET num_bolla='" & Replace(Rs("ddt"), "'", "''") & "'," &
                            "data_bolla='" & funzioni_comuni.getDataDb_senza_orario(Rs("data_ddt")) & "'," &
                            "data_atto_vendita='" & funzioni_comuni.getDataDb_senza_orario(Rs("data_ddt")) & "'," &
                            "note_vendita='" & Replace(Rs("note"), "'", "''") & "', id_acquirente='" & Rs("id_acquirente") & "', " &
                            "id_fornitore='" & Rs("id_presso_di") & "'," & venduta_da_fattura &
                            "buy_back='1', in_vendita='0', disponibile_nolo='0', " &
                            "id_operatore_vendita='" & Request.Cookies("SicilyRentCar")("idUtente") & "', data_operazione_vendita = getDate() " &
                            "WHERE id='" & Rs("id_veicolo") & "'"
                        ElseIf UCase(Rs("leasing")) = "NO" Then
                            If (Rs("data_atto_di_vendita") & "") <> "" Then
                                data_atto = "'" & funzioni_comuni.getDataDb_senza_orario(Rs("data_atto_di_vendita")) & "'"
                            Else
                                data_atto = "NULL"
                            End If

                            sqlStr = "UPDATE veicoli SET num_bolla='" & Replace(Rs("ddt"), "'", "''") & "'," &
                            "data_bolla='" & funzioni_comuni.getDataDb_senza_orario(Rs("data_ddt")) & "'," &
                            "data_atto_vendita=" & data_atto & "," &
                            "id_acquirente='" & Rs("id_acquirente") & "', id_venditore='" & Rs("id_venditore") & "'," &
                            "note_vendita='" & Replace(Rs("note"), "'", "''") & "', " &
                            "id_fornitore='" & Rs("id_presso_di") & "'," & venduta_da_fattura &
                            "venduta='1', in_vendita='0', disponibile_nolo='0', " &
                            "id_operatore_vendita='" & Request.Cookies("SicilyRentCar")("idUtente") & "', data_operazione_vendita=getDate() " &
                            "WHERE id='" & Rs("id_veicolo") & "'"
                        End If

                        Cmd2 = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                        Cmd2.ExecuteNonQuery()

                        'SALVATAGGIO DELLE FATTURE (SE SPECIFICATE) ------------------------------------------------------------------
                        'I FATTURA DI VENDITA -------
                        If (Rs("numero_prima_fattura") & "") <> "" Then
                            sqlStr = "INSERT INTO fatture_vendita_veicoli (id_veicolo,data_vendita,num_vendita,imponibile_vendita,tipo,operatore_inserimento,data_inserimento) VALUES (" _
                                & "'" & Rs("id_veicolo") & "','" & funzioni_comuni.getDataDb_senza_orario(Rs("data_prima_fattura")) & "'" _
                                & ",'" & Replace(Rs("numero_prima_fattura"), "'", "''") & "','" & Replace(Rs("importo_prima_fattura"), ",", ".") & "'" _
                                & ",'FATTURA','" & Request.Cookies("SicilyRentCar")("idUtente") & "',getDate())"
                            Cmd2 = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                            Cmd2.ExecuteNonQuery()
                        End If
                        'II FATTURA DI VENDITA -------
                        If (Rs("numero_seconda_fattura") & "") <> "" Then
                            sqlStr = "INSERT INTO fatture_vendita_veicoli (id_veicolo,data_vendita,num_vendita,imponibile_vendita,tipo,operatore_inserimento,data_inserimento) VALUES (" _
                                & "'" & Rs("id_veicolo") & "','" & funzioni_comuni.getDataDb_senza_orario(Rs("data_seconda_fattura")) & "'" _
                                & ",'" & Replace(Rs("numero_seconda_fattura"), "'", "''") & "','" & Replace(Rs("importo_seconda_fattura"), ",", ".") & "'" _
                                & ",'FATTURA','" & Request.Cookies("SicilyRentCar")("idUtente") & "',getDate())"
                            Cmd2 = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                            Cmd2.ExecuteNonQuery()
                        End If
                        'I NOTA DI CREDITO ACQUISTO -------
                        If (Rs("numero_prima_nc") & "") <> "" Then
                            sqlStr = "INSERT INTO fatture_vendita_veicoli (id_veicolo,data_vendita,num_vendita,imponibile_vendita,tipo,operatore_inserimento,data_inserimento) VALUES (" _
                                & "'" & Rs("id_veicolo") & "','" & funzioni_comuni.getDataDb_senza_orario(Rs("data_prima_nc")) & "'" _
                                & ",'" & Replace(Rs("numero_prima_nc"), "'", "''") & "','" & Replace(CDbl(Rs("importo_prima_nc")) * -1, ",", ".") & "'" _
                                & ",'NOTA','" & Request.Cookies("SicilyRentCar")("idUtente") & "',getDate())"
                            Cmd2 = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                            Cmd2.ExecuteNonQuery()
                        End If
                    Loop

                    Rs.Close()
                    Rs = Nothing
                    Cmd.Dispose()
                    Cmd = Nothing
                    Cmd2.Dispose()
                    Cmd2 = Nothing
                    Dbc2.Close()
                    Dbc2.Dispose()
                    Dbc2 = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing

                    Libreria.genUserMsgBox(Me, "Tutti i dati sono stati caricati correttamente")
                Else
                    Libreria.genUserMsgBox(Me, "Dati non caricati sul Data Base (Verificare gli errori)")
                End If
            Else
                Libreria.genUserMsgBox(Me, "Il file scelto non è corretto.")
            End If
        Else
            Libreria.genUserMsgBox(Me, "Scegliere un file.")
        End If
    End Sub

    Protected Function getTotaleVendita(ByVal id_veicolo As String) As Double
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(SUM(imponibile_vendita),0) FROM fatture_vendita_veicoli WHERE (id_veicolo='" & id_veicolo & "')", Dbc)

        getTotaleVendita = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub btnImportaFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportaFile.Click
        CaricaFoglioExcel()
    End Sub

    Protected Sub btnVenditore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVenditore.Click
        Session("nome_file") = nome_file.Text

        Response.Redirect("tabelle.aspx?val=venditore&prov=ImportDismissioni")
    End Sub

    Protected Sub btnAcqirente_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAcqirente.Click
        Session("nome_file") = nome_file.Text

        Response.Redirect("tabelle.aspx?val=acquirente&prov=ImportDismissioni")
    End Sub

    Protected Sub btnPressoDi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPressoDi.Click
        Session("nome_file") = nome_file.Text

        Response.Redirect("tabelle.aspx?val=acquirente&prov=ImportDismissioni")
    End Sub
End Class
