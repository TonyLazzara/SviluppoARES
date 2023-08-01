
Partial Class contratto_vedi_guidatore
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("num_contratto") <> "" And Request.QueryString("idUtente") <> "" Then
                'NELLA TABELLA contratti_conducenti SONO PRESENTI I DATI DEL CLIENTE AL MOMENTO DEL SALVATAGGIO
                'DEL CONTRATTO E L'ID DELLA TABELLA CONDUCENTI CON L'ANAGRAFICA ATTUALE
                numContratto.Text = Request.QueryString("num_contratto")
                idUtenteAnagrafica.Text = Request.QueryString("idUtente")
                Dim test As Integer

                Try
                    test = CInt(numContratto.Text)
                    Try
                        test = CInt(idUtenteAnagrafica.Text)
                        fillUtenteContratto(numContratto.Text, idUtenteAnagrafica.Text)
                    Catch ex As Exception

                    End Try

                Catch ex As Exception

                End Try

                tab_anagrafica.Visible = True
                tab_anagrafica_contratto.Visible = True
            ElseIf Request.QueryString("idUtente") <> "" Then
                'ID UTENTE PASSATO AL MOMENTO DELLA COMPILAZIONE DI UN NUOVO PREVENTIVO
                idUtenteAnagrafica.Text = Request.QueryString("idUtente")
                Dim test As Integer

                Try
                    test = CInt(idUtenteAnagrafica.Text)
                    fillUtenteAnagrafica(idUtenteAnagrafica.Text)
                Catch ex As Exception

                End Try

                tab_anagrafica.Visible = True
                tab_anagrafica_contratto.Visible = False
            End If
        End If
    End Sub

    Protected Function getComuneAres(ByVal id_comune_ares As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT comune FROM comuni_ares WITH(NOLOCK) WHERE id=" & id_comune_ares & "", Dbc)

        getComuneAres = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getNazione(ByVal id_nazione As String) As String
        If Trim(id_nazione) <> "" Then
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT nazione FROM nazioni WITH(NOLOCK) WHERE id_nazione=" & id_nazione & "", Dbc)

            getNazione = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Else
            getNazione = ""
        End If
    End Function

    Private Sub fillUtenteAnagrafica(ByVal id_utente As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM conducenti WITH(NOLOCK) WHERE id_conducente=" & id_utente & "", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()
        Rs.Read()

        If Rs.HasRows Then
            If (Rs("nazione_nascita") & "") <> "" Then
                txtNazioneNascita.Text = getNazione(Rs("nazione_nascita") & "")
            End If

            If Rs("nazione_nascita") = Costanti.ID_Italia Then
                radioNazionalita.SelectedValue = "it"
                radioNazionalita_cnt.SelectedValue = "it"
                If (Rs("id_comune_ares_nascita") & "") <> "" Then
                    txtLuogoNascita.Text = Rs("provincia_nascita") & " - " & getComuneAres(Rs("id_comune_ares_nascita"))
                Else
                    txtLuogoNascita.Text = Rs("provincia_nascita") & " - " & Rs("comune_nascita_ee") & ""
                End If
            Else
                radioNazionalita.SelectedValue = "ee"
                radioNazionalita_cnt.SelectedValue = "ee"
                txtLuogoNascita.Text = Rs("comune_nascita_ee") & ""
            End If

            If txtLuogoNascita.Text = "" Then
                txtLuogoNascita.Text = Rs("provincia_nascita") & " - " & Rs("luogo_nascita") & ""
            End If

            If (Rs("nazione") & "") <> "" Then
                If Rs("nazione") = Costanti.ID_Italia Then

                    txtNazione.Text = "Italia"
                    txtProvincia.Text = Rs("provincia") & ""

                    If (Rs("id_comune_ares") & "") <> "" Then
                        txtComune.Text = getComuneAres(Rs("id_comune_ares"))
                    Else
                        txtComune.Text = Rs("city") & ""
                    End If
                Else


                    txtNazione.Text = getNazione(Rs("nazione") & "")
                    txtComune.Text = Rs("city") & ""
                    txtProvincia.Text = "EE"
                End If
            End If

            radioNazionalita.Enabled = False

            txtNominativo.Text = Rs("nominativo") & ""
            txtNome.Text = Rs("nome") & ""
            txtCognome.Text = Rs("cognome") & ""
            txtIndirizzo.Text = Rs("indirizzo") & ""
            txtCap.Text = Rs("cap") & ""
            txtDataNascita.Text = Rs("data_nascita") & ""
            'txtLuogoNascita.Text = Rs("luogo_nascita") & ""

            If Rs("sesso") & "" = "M" Then
                radioSessoM.Checked = True
                radioSessoF.Checked = False
            ElseIf Rs("sesso") & "" = "F" Then
                radioSessoF.Checked = True
                radioSessoM.Checked = False
            Else
                radioSessoF.Checked = False
                radioSessoM.Checked = False
            End If

            radioSessoF.Enabled = False
            radioSessoM.Enabled = False

            txtCodiceFiscale.Text = Rs("codfis") & ""
            txtDomicilio.Text = Rs("domicilio_locale") & ""
            txtTelefono.Text = Rs("telefono") & ""
            txtFax.Text = Rs("fax") & ""
            txtCellulare.Text = Rs("cell") & ""
            txtEmail.Text = Rs("email") & ""
            txtPatente.Text = Rs("patente") & ""
            txtTipoPatente.Text = Rs("tipo_patente") & ""
            txtScadenzaPatente.Text = Rs("scadenza_patente") & ""
            txtDataRilascioPatente.Text = Rs("rilasciata_il") & ""
            txtLuogoEmissionePatente.Text = Rs("luogo_emissione") & ""
            txtAltriDocumenti.Text = Rs("altri_documenti") & ""
            'If Rs("id_cliente_tipologia") & "" <> "" Then
            '    listConvenzioni.SelectedValue = Rs("id_cliente_tipologia")
            'End If
        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub fillUtenteContratto(ByVal num_contratto As String, ByVal id_conducente As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM contratti_conducenti WITH(NOLOCK) WHERE num_contratto='" & num_contratto & "' AND id_conducente='" & id_conducente & "'", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()
        If Rs.Read() Then
            'ANAGRAFICA ATTUALE ----------------------------------------------------------------------------------------------------------
            fillUtenteAnagrafica(Rs("id_conducente"))
            '-----------------------------------------------------------------------------------------------------------------------------

            'ANAGRAFICA SALVATA AL MOMENTO DEL COTNRATTO ---------------------------------------------------------------------------------
            If (Rs("nazione") & "") <> "" Then
                If Rs("nazione") = Costanti.ID_Italia Then
                    txtNazione_cnt.Text = "Italia"
                    txtProvincia_cnt.Text = Rs("provincia") & ""

                    If (Rs("id_comune_ares") & "") <> "" Then
                        txtComune_cnt.Text = getComuneAres(Rs("id_comune_ares"))
                    Else
                        txtComune_cnt.Text = Rs("city") & ""
                    End If
                Else

                    txtNazione_cnt.Text = getNazione(Rs("nazione") & "")
                    txtComune_cnt.Text = Rs("city") & ""
                    txtProvincia_cnt.Text = "EE"
                End If
            End If

            radioNazionalita_cnt.Enabled = False

            txtNome_cnt.Text = Rs("nome") & ""
            txtCognome_cnt.Text = Rs("cognome") & ""
            txtIndirizzo_cnt.Text = Rs("indirizzo") & ""
            txtCap_cnt.Text = Rs("cap") & ""
            txtDataNascita_cnt.Text = Rs("data_nascita") & ""
            txtLuogoNascita_cnt.Text = Rs("provincia_nascita") & " - " & Rs("luogo_nascita") & ""

            txtCodiceFiscale_cnt.Text = Rs("codfis") & ""
            txtDomicilio_cnt.Text = Rs("domicilio_locale") & ""
            txtTelefono_cnt.Text = Rs("telefono") & ""
            txtCellulare_cnt.Text = Rs("cell") & ""
            txtEmail_cnt.Text = Rs("email") & ""
            txtPatente_cnt.Text = Rs("patente") & ""
            txtTipoPatente_cnt.Text = Rs("tipo_patente") & ""
            txtScadenzaPatente_cnt.Text = Rs("scadenza_patente") & ""
            txtDataRilascioPatente_cnt.Text = Rs("rilasciata_il") & ""
            txtLuogoEmissionePatente_cnt.Text = Rs("luogo_emissione") & ""
            txtAltriDocumenti_cnt.Text = Rs("altri_documenti") & ""
            '-----------------------------------------------------------------------------------------------------------------------------
        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub
End Class
