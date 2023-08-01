
Partial Class contratto_vedi_ditta
    Inherits System.Web.UI.Page

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

    Protected Function getPagamento(ByVal id_pagamento As String) As String
        If Trim(id_pagamento) <> "" Then
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT descrizione FROM TERMINE_DI_PAGAMENTO WITH(NOLOCK) WHERE ID=" & id_pagamento & "", Dbc)

            getPagamento = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Else
            getPagamento = ""
        End If
    End Function

    Protected Function getModalitaDiPagamento(ByVal id_modalita_di_pagamento As String) As String
        If Trim(id_modalita_di_pagamento) <> "" Then
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT Descrizione FROM MOD_PAG WITH(NOLOCK) WHERE ID_ModPag=" & id_modalita_di_pagamento & "", Dbc)

            getModalitaDiPagamento = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Else
            getModalitaDiPagamento = ""
        End If
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

    Protected Function getTipoCliente(ByVal id_tipo_cliente As String) As String
        If Trim(id_tipo_cliente) <> "" Then
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT descrizione FROM clienti_tipologia WITH(NOLOCK) WHERE id=" & id_tipo_cliente & "", Dbc)

            getTipoCliente = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Else
            getTipoCliente = ""
        End If
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("num_contratto") <> "" And Request.QueryString("idDitta") <> "" Then
                'NUMERO DI UN CONTRATTO SALVATO - NELLA TABELLA contratti_ditte SONO PRESENTI I DATI DELLA DITTA AL MOMENTO DEL SALVATAGGIO
                'DEL CONTRATTO E L'ID DELLA TABELLA DITTE CON L'ANAGRAFICA ATTUALE
                num_contratto.Text = Request.QueryString("num_contratto")
                idDitta.Text = Request.QueryString("idDitta")

                Dim test As Integer

                Try
                    test = CInt(num_contratto.Text)
                    Try
                        test = CInt(idDitta.Text)
                        fillDittaContratto(num_contratto.Text, idDitta.Text)
                    Catch ex As Exception

                    End Try
                Catch ex As Exception

                End Try
                tab_anagrafica_contratto.Visible = True
            ElseIf Request.QueryString("idDitta") <> "" Then
                'ID UTENTE PASSATO AL MOMENTO DELLA COMPILAZIONE DI UN NUOVO CONTRATTO
                idDitta.Text = Request.QueryString("idDitta")
                Dim test As Integer

                Try
                    test = CInt(idDitta.Text)
                    fillDittaAnagrafica(idDitta.Text)
                Catch ex As Exception

                End Try

                tab_anagrafica_contratto.Visible = False

            End If
        End If
    End Sub

    Protected Sub fillDittaContratto(ByVal num_contratto As String, ByVal id_ditta As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM contratti_ditte WITH(NOLOCK) WHERE num_contratto='" & num_contratto & "'", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()
        If Rs.Read() Then
            'ANAGRAFICA ATTUALE ----------------------------------------------------------------------------------------------------------
            'ATTENZIONE: IN CASO DI VARIAZIONE DITTA IN FASE DI FATTURAZIONE COME ANAGRAFICA ATTUALE VIENE VISUALIZZATA QUELLA FATTURATA MENTRE
            'COME ANAGRAFICA ORIGINALE QUELLA SCELTA AL MOMENTO DEL CONTRATTO O DI ULTIMA MODIFICA DI DITTA SU CONTRATTO
            fillDittaAnagrafica(id_ditta)
            '-----------------------------------------------------------------------------------------------------------------------------
            txtCodiceEdp_cnt.Text = Rs("codice_edp") & ""
            txtTipoCliente_cnt.Text = getTipoCliente(Rs("id_tipo_cliente") & "")
            txtRagioneSociale_cnt.Text = Rs("rag_soc") & ""
            txtPartitaIva_cnt.Text = Rs("PIva") & ""
            txtNazione_cnt.Text = getNazione(Rs("nazione") & "")
            txtProvincia_cnt.Text = Rs("provincia") & ""
            If (Rs("id_comune_ares") & "") <> "" Then
                txtComune_cnt.Text = getComuneAres(Rs("id_comune_ares"))
            Else
                txtComune_cnt.Text = Rs("citta") & ""
            End If
            txtIndirizzo_cnt.Text = Rs("indirizzo") & ""
            txtCap_cnt.Text = Rs("cap") & ""
            txtPartitaIvaEstera_cnt.Text = Rs("PIva_ESTERA") & ""
            txtCodiceFiscale_cnt.Text = Rs("c_fis") & ""
            txtFax_cnt.Text = Rs("fax") & ""
            txtTelefono_cnt.Text = Rs("tel") & ""

            If (Rs("tipo_spedizione_fattura") & "") <> "" Then
                radioSpedizioneFattura_cnt.SelectedValue = Rs("tipo_spedizione_fattura")
            End If

            If (Rs("invio_email") & "") <> "" Then
                checkInvioEmail_cnt.Checked = Rs("invio_email")
            End If
            txtEmail_cnt.Text = Rs("email") & ""
            txtEmailPec_cnt.Text = Rs("email_pec") & ""
            txtCodiceSdi_cnt.Text = Rs("codice_sdi") & ""
        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub fillDittaAnagrafica(ByVal id_ditta As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM ditte WITH(NOLOCK) WHERE id_Ditta=" & id_ditta & "", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()
        Rs.Read()

        If Rs.HasRows Then
            txtRagioneSociale.Text = Rs("Rag_soc") & ""
            txtCodiceEDP.Text = Rs("CODICE EDP") & ""
            txtTipoCliente.Text = getTipoCliente(Rs("id_tipo_cliente") & "")
            txtPartitaIva.Text = Rs("Piva") & ""
            txtNazione.Text = getNazione(Rs("nazione") & "")
            txtProvincia.Text = Rs("provincia") & ""
            If (Rs("id_comune_ares") & "") <> "" Then
                txtComune.Text = getComuneAres(Rs("id_comune_ares"))
            Else
                txtComune.Text = Rs("citta") & ""
            End If
            txtIndirizzo.Text = Rs("indirizzo") & ""
            txtCAP.Text = Rs("cap") & ""
            txtPartitaIvaEstera.Text = Rs("PIva_ESTERA") & ""
            txtCodiceFiscale.Text = Rs("c_fis") & ""
            txtFax.Text = Rs("fax") & ""
            txtTelefono.Text = Rs("tel") & ""
            If (Rs("tour_op") & "") <> "" Then
                checkTourOperator.Checked = Rs("tour_op")
            End If
            txtModalitaDiPagamento.Text = getModalitaDiPagamento(Rs("id_ModPag") & "")
            txtPagamento.Text = getPagamento(Rs("ID_Pagamento") & "")
            If (Rs("tipo_spedizione_fattura") & "") <> "" Then
                radioSpedizioneFattura.SelectedValue = Rs("tipo_spedizione_fattura")
            End If

            If (Rs("invio_email") & "") <> "" Then
                checkInvioEmail.Checked = Rs("invio_email")
            End If
            txtEmail.Text = Rs("email") & ""
            txtEmailPec.Text = Rs("email_pec") & ""
            txtCodiceSDI.Text = Rs("codice_sdi") & ""
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
