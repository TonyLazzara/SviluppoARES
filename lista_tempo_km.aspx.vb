
Partial Class tariffe_lista_tempo_km
    Inherits System.Web.UI.Page

    Protected Function getGruppi() As String()
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT cod_gruppo FROM gruppi WITH(NOLOCK) ORDER BY cod_gruppo", Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Dim gruppi(200) As String

            Dim i As Integer = 3

            Do While Rs.Read()
                gruppi(i) = Rs("cod_gruppo")
                i = i + 1
            Loop
            gruppi(i) = "000"
            getGruppi = gruppi

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error getGruppi : " & ex.Message & " " & "<br/>")
        End Try

    End Function

    Protected Function getIdGruppo(ByVal gruppo As String) As Integer
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_gruppo FROM gruppi WHERE cod_gruppo='" & gruppo & "'", Dbc)

            getIdGruppo = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error getIdGruppo : " & ex.Message & " " & "<br/>")
        End Try

    End Function

    Protected Function getNumColonne(ByVal codice As String) As Integer
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(MAX(colonna),0) as numColonne FROM righe_tempo_km WHERE id_tempo_km='" & Replace(codice, "'", "''") & "'", Dbc)

            getNumColonne = Cmd.ExecuteScalar + 1

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error getNumColonne : " & ex.Message & " " & "<br/>")
        End Try

    End Function

    Protected Function getColonnaDa(ByVal codice As String, ByVal colonna As String) As Integer
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 da FROM righe_tempo_km WHERE id_tempo_km='" & lbl_id_codice.Text & "' AND colonna='" & colonna & "'", Dbc)

            getColonnaDa = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error getColonnaDa : " & ex.Message & " " & "<br/>")
        End Try


    End Function

    Protected Function getColonnaA(ByVal codice As String, ByVal colonna As String) As Integer
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 a FROM righe_tempo_km WHERE id_tempo_km='" & lbl_id_codice.Text & "' AND colonna='" & colonna & "'", Dbc)

            getColonnaA = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error getColonnaA : " & ex.Message & " " & "<br/>")
        End Try

    End Function

    Protected Function getValore(ByVal codice As String, ByVal gruppo As String, ByVal colonna As Integer) As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT valore FROM righe_tempo_km WHERE id_tempo_km='" & lbl_id_codice.Text & "' AND id_gruppo='" & getIdGruppo(gruppo) & "' AND colonna='" & colonna & "'", Dbc)

            getValore = FormatNumber(Cmd.ExecuteScalar, 2, , , TriState.False)

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error getValore : " & ex.Message & " " & "<br/>")
        End Try

    End Function

    Protected Sub elimina_righe(ByVal id_tempo_km As String)
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM righe_tempo_km WHERE id_tempo_km='" & id_tempo_km & "'", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error elimina_righe : " & ex.Message & " " & "<br/>")
        End Try


    End Sub

    Protected Sub memorizza_valore(ByVal idCodice As String, ByVal gruppo As String, ByVal da As String, ByVal a As String, ByVal valore As String, ByVal colonna As String, ByVal packed As Char, ByVal gg_extra As Char)
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO righe_tempo_km (id_tempo_km,id_gruppo,da,a,pac,gg_extra,valore, colonna, id_utente, data) VALUES ('" & idCodice & "','" & getIdGruppo(gruppo) & "','" & da & "','" & a & "','" & packed & "','" & gg_extra & "','" & Replace(valore, ",", ".") & "','" & colonna & "','" & Request.Cookies("SicilyRentCar")("idUtente") & "','" & Now() & "')", Dbc)

            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error memorizza_valore : " & ex.Message & " " & "<br/>")
        End Try


    End Sub

    Protected Function salvaTempoKm(ByVal codice As String, ByVal validoDa As String, ByVal validoA As String, ByVal id_codice As String) As Boolean
        'SE ID_CODICE E' VALORIZZATO SIAMO IN FASE DI MODIFICA. SE ID_CODICE E' VUOTO VUOL DIRE CHE E' UN NUOVO INSERIMENTO
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            validoDa = Year(validoDa) & "-" & Day(validoDa) & "-" & Month(validoDa)

            validoA = Year(validoA) & "-" & Day(validoA) & "-" & Month(validoA)


            Dim sqlStr As String

            If id_codice <> "" Then
                sqlStr = "UPDATE tempo_km SET codice='" & Replace(codice, "'", "''") & "', valido_da='" & validoDa & "', valido_a='" & validoA & "', iva_inclusa='" & dropIva.SelectedValue & "', id_aliquota_iva='" & dropAliquota.SelectedValue & "', attivo='1' WHERE id='" & id_codice & "'"
            Else
                sqlStr = "INSERT INTO tempo_km (codice, valido_da,valido_a,data_creazione,id_operatore_inserimento, iva_inclusa, id_aliquota_iva, attivo) VALUES ('" & Replace(codice, "'", "''") & "','" & validoDa & "','" & validoA & "',GetDate(),'" & Request.Cookies("SicilyRentCar")("idUtente") & "','" & dropIva.SelectedValue & "','" & dropAliquota.SelectedValue & "','1')"
            End If

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            Cmd.ExecuteNonQuery()

            'SE SIAMO IN FASE DI NUOVO INSERIMENTO SELEZIONO L'ID DELLA RIGA APPENA CREATA

            If id_codice = "" Then
                sqlStr = "SELECT MAX(id) FROM tempo_km"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                lbl_id_codice.Text = Cmd.ExecuteScalar()
            End If


            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error salvaTempoKm : " & ex.Message & " " & "<br/>")
        End Try

    End Function

    Protected Function check_tempo_km(ByVal codice As String, ByVal validoDa As String, ByVal validoA As String, ByVal id_codice As String) As Boolean
        'CONTROLLA SE IL TEMPO-KM E' UNIVOCO. SE ID_CODICE E' VALORIZZATO SIAMO IN FASE DI MODIFICA PER CUI CONTROLLO PER I TEMPO_KM
        'DIVERSI DA QUELLO ATTUALE. SE ID_CODICE E' VUOTO VUOL DIRE CHE E' UN NUOVO INSERIMENTO
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            validoDa = Year(validoDa) & "-" & Day(validoDa) & "-" & Month(validoDa)


            validoA = Year(validoA) & "-" & Day(validoA) & "-" & Month(validoA)

            Dim condizione As String = ""

            If id_codice <> "" Then
                condizione = " AND id <> '" & id_codice & "'"
            End If

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM tempo_km WHERE codice='" & Replace(codice, "'", "''") & "' AND valido_da='" & validoDa & "' AND valido_a='" & validoA & "'" & condizione, Dbc)

            'Response.Write(Cmd.CommandText)
            'Response.End()
            Dim test As String = Cmd.ExecuteScalar

            If test = "" Then
                check_tempo_km = True
            Else
                check_tempo_km = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error check_tempo_km : " & ex.Message & " " & "<br/>")
        End Try


    End Function

    Protected Function esiste_extra() As Boolean
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM righe_tempo_km WHERE id_tempo_km='" & lbl_id_codice.Text & "' AND ( a='999' OR a='9999')", Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test <> "" Then
                esiste_extra = True
            Else
                esiste_extra = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error esiste_extra : " & ex.Message & " " & "<br/>")
        End Try



    End Function

    Protected Function getPacked(ByVal colonna As String) As Boolean
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 pac FROM righe_tempo_km WHERE id_tempo_km='" & lbl_id_codice.Text & "' AND colonna='" & colonna & "'", Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test = "" Then
                getPacked = "False"
            Else
                If test = "True" Then
                    getPacked = "True"
                Else
                    getPacked = "False"
                End If
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error getPacked : " & ex.Message & " " & "<br/>")
        End Try

    End Function

    Protected Function getGGExtra(ByVal colonna As String) As Boolean

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 gg_extra FROM righe_tempo_km WHERE id_tempo_km='" & lbl_id_codice.Text & "' AND colonna='" & colonna & "'", Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test = "" Then
                getGGExtra = "False"
            Else
                If test = "True" Then
                    getGGExtra = "True"
                Else
                    getGGExtra = "False"
                End If
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error getGGExtra : " & ex.Message & " " & "<br/>")
        End Try


    End Function

    Protected Function tempo_km_non_collegato(ByVal id_tempo_km As String) As Boolean
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 tariffe_righe.id FROM tariffe_righe INNER JOIN tariffe ON tariffe_righe.id_tariffa=tariffe.id WHERE tariffe_righe.id_tempo_km='" & id_tempo_km & "' AND tariffe.attiva='1'", Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test = "" Then
                tempo_km_non_collegato = True
            Else
                tempo_km_non_collegato = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error tempo_km_non_collegato : " & ex.Message & " " & "<br/>")
        End Try


    End Function

    Protected Function duplica(ByVal id_tempo_km As String, ByVal variazione As Integer) As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO tempo_km (data_creazione, id_operatore_inserimento, attivo) VALUES (GetDate(),'" & Request.Cookies("SicilyRentCar")("idUtente") & "','0')", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd = New Data.SqlClient.SqlCommand("SELECT @@IDENTITY FROM tempo_km", Dbc)
            Dim nuovo_id As String = Cmd.ExecuteScalar

            Dim sqlStr As String = ""
            If variazione >= 0 Then
                sqlStr = "INSERT INTO righe_tempo_km (id_tempo_km, id_gruppo, da, a, pac, valore, colonna, gg_extra) " &
            "(SELECT '" & nuovo_id & "',id_gruppo, da, a, pac, ROUND(valore + (valore*" & variazione & "/100),2),colonna, gg_extra FROM righe_tempo_km WHERE id_tempo_km='" & id_tempo_km & "')"

            Else
                variazione = variazione * -1
                sqlStr = "INSERT INTO righe_tempo_km (id_tempo_km, id_gruppo, da, a, pac, valore, colonna, gg_extra) " &
            "(SELECT '" & nuovo_id & "',id_gruppo, da, a, pac, ROUND(valore - (valore*" & variazione & "/100),2),colonna, gg_extra FROM righe_tempo_km WHERE id_tempo_km='" & id_tempo_km & "')"
            End If

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            duplica = nuovo_id

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error duplica : " & ex.Message & " " & "<br/>")
        End Try


    End Function

    Protected Sub listTempoKm_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listTempoKm.ItemCommand
        lbl_errore_sup.Text = ""
        Try
            If e.CommandName = "vedi" Then
                Dim id_tempo_km As Label = e.Item.FindControl("idLabel")
                Dim valido_da As Label = e.Item.FindControl("valido_daLabel")
                Dim valido_a As Label = e.Item.FindControl("valido_aLabel")
                Dim nome_tempo_km As Label = e.Item.FindControl("codiceLabel")
                Dim iva_inclusa As Label = e.Item.FindControl("iva_inclusa")
                Dim id_aliquota_iva As Label = e.Item.FindControl("id_aliquota_iva")

                lbl_id_codice.Text = id_tempo_km.Text
                lblCodice.Text = nome_tempo_km.Text
                txtValidoDa.Text = valido_da.Text
                txtValidoA.Text = valido_a.Text

                If iva_inclusa.Text = "True" Then
                    dropIva.SelectedValue = "1"
                ElseIf iva_inclusa.Text = "False" Then
                    dropIva.SelectedValue = "0"
                End If

                dropAliquota.SelectedValue = id_aliquota_iva.Text
                lblCodice.Text = nome_tempo_km.Text
                txtValidoDa.Text = valido_da.Text
                txtValidoA.Text = valido_a.Text

                tab_vedi.Visible = True
                tab_cerca.Visible = False
            ElseIf e.CommandName = "duplica" Then
                'DUPLICO IL TEMPO KM EVENTUALMENTE MAGGIORATO O DIMINUITO DELLA PERCENTUALE INDICATA 
                Dim id_tempo_km As Label = e.Item.FindControl("idLabel")
                Dim variazione As TextBox = e.Item.FindControl("txtVariazione")

                Dim iva_inclusa As Label = e.Item.FindControl("iva_inclusa")
                Dim id_aliquota_iva As Label = e.Item.FindControl("id_aliquota_iva")

                If Trim(variazione.Text) = "" Then
                    variazione.Text = "0"
                End If

                Dim nuovo_id As String = duplica(id_tempo_km.Text, CInt(variazione.Text))

                lbl_id_codice.Text = nuovo_id

                lblCodice.Text = ""
                txtValidoDa.Text = ""
                txtValidoA.Text = ""

                If iva_inclusa.Text = "True" Then
                    dropIva.SelectedValue = "1"
                ElseIf iva_inclusa.Text = "False" Then
                    dropIva.SelectedValue = "0"
                End If

                dropAliquota.SelectedValue = id_aliquota_iva.Text

                tab_vedi.Visible = True
                tab_cerca.Visible = False

            ElseIf e.CommandName = "elimina" Then
                Dim id_tempo_km As Label = e.Item.FindControl("idLabel")

                If tempo_km_non_collegato(id_tempo_km.Text) Then
                    elimina_tempo_km(id_tempo_km.Text)
                    listTempoKm.DataBind()
                Else
                    lbl_errore_sup.Text = "Impossibile eliminare: il tempo-km selzionato è collegato ad una o più tariffe."
                End If
            ElseIf e.CommandName = "modifica" Then
                Dim id_tempo_km As Label = e.Item.FindControl("idLabel")
                Dim valido_da As TextBox = e.Item.FindControl("valido_daText")
                Dim valido_a As TextBox = e.Item.FindControl("valido_aText")
                Dim nome_tempo_km As TextBox = e.Item.FindControl("codiceText")

                If check_tempo_km(nome_tempo_km.Text, valido_da.Text, valido_a.Text, id_tempo_km.Text) Then
                    modifica_tempo_km(id_tempo_km.Text, valido_da.Text, valido_a.Text, nome_tempo_km.Text)
                Else
                    lbl_errore_sup.Text = "Tempo-Km già esistente: modifica non effettuata."
                End If

                listTempoKm.DataBind()
            End If
        Catch ex As Exception
            Response.Write("error listTempoKm_ItemCommand : " & ex.Message & " " & "<br/>")
        End Try


    End Sub

    Protected Sub modifica_tempo_km(ByVal id_tempo_km As String, ByVal valido_da As String, ByVal valido_a As String, ByVal codice As String)

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            valido_da = Year(valido_da) & "-" & Day(valido_da) & "-" & Month(valido_da)

            valido_a = Year(valido_a) & "-" & Day(valido_a) & "-" & Month(valido_a)


            Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE tempo_km SET codice='" & Replace(codice, "'", "''") & "', valido_da='" & valido_da & "', valido_a='" & valido_a & "' WHERE id='" & id_tempo_km & "'", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error modifica_tempo_km : " & ex.Message & " " & "<br/>")
        End Try

    End Sub

    Protected Sub elimina_tempo_km(ByVal id_tempo_km As String)
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM righe_tempo_km WHERE id_tempo_km='" & id_tempo_km & "'", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd = New Data.SqlClient.SqlCommand("DELETE FROM tempo_km WHERE id='" & id_tempo_km & "'", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error elimina_tempo_km : " & ex.Message & " " & "<br/>")
        End Try

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click
        'SE STO ANNULLANDO CON UNA TARIFFA NON ANCORA ATTIVA (TARIFFA DUPLICATA NON SALVATA) LA ELIMINO
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT attivo FROM tempo_km WHERE id='" & lbl_id_codice.Text & "'", Dbc)
            Dim attivo As Boolean = Cmd.ExecuteScalar

            If Not attivo Then
                Cmd = New Data.SqlClient.SqlCommand("DELETE FROM righe_tempo_km WHERE id_tempo_km='" & lbl_id_codice.Text & "'", Dbc)
                Cmd.ExecuteNonQuery()
                Cmd = New Data.SqlClient.SqlCommand("DELETE FROM tempo_km WHERE id='" & lbl_id_codice.Text & "'", Dbc)
                Cmd.ExecuteNonQuery()
            End If


            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            listTempoKm.DataBind()

            lbl_id_codice.Text = ""
            lblCodice.Text = ""

            tab_vedi.Visible = False
            tab_cerca.Visible = True
        Catch ex As Exception
            Response.Write("error btnAnnulla_Click : " & ex.Message & " " & "<br/>")
        End Try


    End Sub

    Protected Sub btnNuovo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovo.Click

        lbl_errore_sup.Text = ""

        lbl_id_codice.Text = ""
        lblCodice.Text = ""

        tab_vedi.Visible = True
        tab_cerca.Visible = False

    End Sub

    Protected Sub setQuery()

        sqlTempoKm.SelectCommand = "SELECT id, codice, valido_da, valido_a, data_creazione, iva_inclusa, id_aliquota_iva FROM tempo_km WHERE id >0 "

        Dim condizioneWhere As String = ""
        Try
            If Trim(txtCodice.Text) <> "" Then
                condizioneWhere = " AND codice LIKE '%" & Replace(txtCodice.Text, "'", "''") & "%' "
            End If

            If Trim(txtCercaValidoDa.Text) <> "" And Trim(txtCercaValidoA.Text) = "" Then
                Dim valido_da As String = txtCercaValidoDa.Text

                valido_da = Year(valido_da) & "-" & Month(valido_da) & "-" & Day(valido_da)

                condizioneWhere += " AND valido_da>=convert(datetime,'" & valido_da & "',102) "

            ElseIf Trim(txtCercaValidoDa.Text) = "" And Trim(txtCercaValidoA.Text) <> "" Then
                Dim valido_a As String = txtCercaValidoA.Text

                valido_a = Year(valido_a) & "-" & Month(valido_a) & "-" & Day(valido_a)

                condizioneWhere += " AND valido_a <= convert(datetime,'" & valido_a & "',102) "


            ElseIf Trim(txtCercaValidoDa.Text) <> "" And Trim(txtCercaValidoA.Text) <> "" Then

                Dim valido_da As String = txtCercaValidoDa.Text
                Dim valido_a As String = txtCercaValidoA.Text

                valido_da = Year(valido_da) & "-" & Month(valido_da) & "-" & Day(valido_da)
                valido_a = Year(valido_a) & "-" & Month(valido_a) & "-" & Day(valido_a)

                'condizioneWhere = condizioneWhere & " AND ((convert(datetime,'" & valido_da & "',102) >= valido_da AND convert(datetime,'" & valido_a & "',102) <= valido_a) OR (convert(datetime,'" & valido_da & "',102) <= valido_da AND convert(datetime,'" & valido_a & "',102) >= valido_a))"
                condizioneWhere += " AND (valido_da>= convert(datetime,'" & valido_da & "',102) AND valido_a <= convert(datetime,'" & valido_a & "',102))"


            End If

            txtQuery.Text = sqlTempoKm.SelectCommand & condizioneWhere & " ORDER BY id DESC, valido_da"
            sqlTempoKm.SelectCommand = txtQuery.Text

            listTempoKm.DataBind()
        Catch ex As Exception
            Response.Write("error setQuery : " & ex.Message & " " & "<br/>")
        End Try


    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerca.Click
        lbl_errore_sup.Text = ""
        setQuery()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            txtQuery.Text = "SELECT id, codice, valido_da, valido_a, data_creazione, iva_inclusa, id_aliquota_iva FROM tempo_km ORDER BY id DESC"
        End If

        Try
            sqlTempoKm.SelectCommand = txtQuery.Text
            sqlTempoKm.DataBind()
        Catch ex As Exception
            Response.Write("error page_load : " & ex.Message & " " & "<br/>")
        End Try

    End Sub

    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampa.Click
        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            Session("url_print") = "/stampe/stampa_tempo_km.aspx?pagina=verticale&id_tempo_km=" & lbl_id_codice.Text
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
            End If
        End If
    End Sub


End Class
