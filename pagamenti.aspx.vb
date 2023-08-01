'Imports Security
Imports System.IO
Imports System.Net.Mail
Imports System.Collections.Generic
Imports System.Security.Cryptography
Imports System.Text
Imports System.Drawing
Imports funzioni_comuni


Partial Class pagamenti
    Inherits System.Web.UI.Page

    Public Delegate Sub EventoChiusuraForm(ByVal sender As Object, ByVal e As EventArgs)
    Event ChiusuraForm As EventHandler

    Public Delegate Sub EventoSalvaRecord(ByVal sender As Object, ByVal e As EventoConOggetto)
    Event SalvaRecord As EventHandler

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Cookies("SicilyRentCar") Is Nothing Then
            Response.Redirect("LogIn.aspx")
        Else
            Dim objCookie As HttpCookie = Request.Cookies("SicilyRentCar")
            objCookie.Expires = DateAdd(DateInterval.Minute, 120, Now())
            Response.AppendCookie(objCookie)
        End If

        Session("DatiSqlIng") = ""

        If Not IsPostBack Then
            'Response.Write("NO postback")
            txtId.Text = Session("carica_dati")
            div_contenitore_pos.Visible = True
            carica_datiPos(txtId.Text)

            sqlEnti.SelectCommand = "select * from POS_enti_proprietari, POS_Stazioni_Enti WITH(NOLOCK) where POS_enti_proprietari.id = POS_Stazioni_Enti.id_ente and POS_Stazioni_Enti.id_stazione = '" & Request.Cookies("SicilyRentCar")("stazione") & "'"
            'Response.Write(sqlEnti.SelectCommand)

            listEnti.DataBind()
        Else
            'Response.Write("postback")
        End If
        'Response.Write("Importo " & Session("DaPagare"))
    End Sub

    Protected Sub carica_dati(ByVal id As String, ByVal pulsanteScelto As Integer)
        'Funzione: Cambio Stazione Pagamento
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "141") < 3 Then
            lbl_stazione_pagamento.Visible = False
            ddl_stazioni_pagamento.Visible = False
        Else
            lbl_stazione_pagamento.Visible = True
            ddl_stazioni_pagamento.Visible = True
        End If





        If lblModalita.Text = "PRENOTAZIONE" Then
            'Funzione: Pagamento modalità Bonifico
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "163") < 3 Then
                tx_importo.Enabled = False
            Else
                tx_importo.Enabled = True
            End If
        Else
            tx_importo.Enabled = True
        End If

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            'Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM contratti,stazioni WITH(NOLOCK) WHERE contratti.id_stazione_uscita = stazioni.id and num_contratto='" & txtId.Text & "'", Dbc)
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM stazioni WITH(NOLOCK) WHERE stazioni.id='" & Request.Cookies("SicilyRentCar")("stazione") & "'", Dbc)
            'Response.Write(Cmd.CommandText & "<br><br>")
            'Response.End()

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            If Rs.HasRows Then
                Do While Rs.Read
                    lb_stazione.Text = Rs("codice") & " - " & Rs("nome_stazione")
                    lblCodiceStazione.Text = Rs("codice")

                    Select Case Session("provenienza")
                        Case Is = "Contratto"
                            lblProvenienzaDocumento.Text = "RA "
                        Case Is = "Prenotazione"
                            lblProvenienzaDocumento.Text = "RES "
                    End Select

                    'lb_documento_riferimento.Text = Rs("num_contratto")
                    lb_documento_riferimento.Text = id
                    lb_operatore.Text = Libreria.getNomeOperatoreDaId(Request.Cookies("SicilyRentCar")("idUtente"))

                    btnAbbuoni.Visible = False
                Loop
            Else

            End If

            Rs.Close()
            Dbc.Close()
            Rs = Nothing
            Dbc = Nothing

        Catch ex As Exception
            Libreria.genUserMsgBox(Me, ex.Message & " Carica Dati --- Errore contattare amministratore del sistema.")
        End Try

        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "162") = 3 Then
            tr_StazioneDiPagamento.Visible = True
        Else
            tr_StazioneDiPagamento.Visible = False
        End If


        Select Case pulsanteScelto
            Case Is = 1 'Contanti/Bonifico
                lb_tipo_pagamento.Text = "Contanti/Bonifico"

                DropDownTipoPagamento.Items.Clear()
                DropDownTipoPagamento.Items.Add("Seleziona...")

                Dim sqlStr As String

                'Visibili voci Deposito solo su RA
                If lblProvenienzaDocumento.Text = "RA " Then
                    'Pulsanti Rimborsi (Rimborso Deposito e Rimborso)
                    If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "164") < 3 Then
                        sqlStr = "SELECT ID_TIPPag, descrizione + ' (' + SEGNO + ')' descrizione FROM [TIP_PAG] WITH(NOLOCK)" &
                            " WHERE ID_TIPPag IN (3,-1886319629,-714677539)" &
                            " ORDER BY STA_IX"
                    Else
                        sqlStr = "SELECT ID_TIPPag, descrizione + ' (' + SEGNO + ')' descrizione FROM [TIP_PAG] WITH(NOLOCK)" &
                            " WHERE ID_TIPPag IN (3,-1886319629,-714677539, 2)" &
                            " ORDER BY STA_IX"
                    End If                    

                    sqlTipoPagamento.SelectCommand = sqlStr
                    DropDownTipoPagamento.DataBind()

                    DropDownTipoPagamento.Enabled = True
                Else
                    'x Operatore Banco NON visualizzo rimborso
                    'Pulsanti Rimborsi (Rimborso Deposito e Rimborso)
                    If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "164") < 3 Then
                        sqlStr = "SELECT ID_TIPPag, descrizione + ' (' + SEGNO + ')' descrizione FROM [TIP_PAG] WITH(NOLOCK)" &
                            " WHERE ID_TIPPag IN (3)" &
                            " ORDER BY MOV_CASSA, Descrizione"

                        sqlTipoPagamento.SelectCommand = sqlStr
                        DropDownTipoPagamento.DataBind()

                        DropDownTipoPagamento.SelectedValue = "3"
                        DropDownTipoPagamento.Enabled = False
                    Else
                        sqlStr = "SELECT ID_TIPPag, descrizione + ' (' + SEGNO + ')' descrizione FROM [TIP_PAG] WITH(NOLOCK)" &
                            " WHERE ID_TIPPag IN (3, 2)" &
                            " ORDER BY MOV_CASSA, Descrizione"

                        sqlTipoPagamento.SelectCommand = sqlStr
                        DropDownTipoPagamento.DataBind()

                        DropDownTipoPagamento.Enabled = True
                    End If
                End If
                


                DropDownModalitaPagamento.Items.Clear()
                DropDownModalitaPagamento.Items.Add("Seleziona...")

                'x Operatore Banco NON visualizzo rimborso
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "164") < 3 Then
                    sqlStr = "SELECT ID_ModPag, Descrizione FROM [MOD_PAG] WITH(NOLOCK)" &
                    " WHERE ID_ModPag IN (4)" &
                    " ORDER BY [Descrizione] DESC"


                    SqlModalitaPagamento.SelectCommand = sqlStr
                    DropDownModalitaPagamento.DataBind()

                    DropDownModalitaPagamento.SelectedValue = "4"
                    DropDownModalitaPagamento.Enabled = False
                    'btnAbbuoni.Visible = True
                Else
                    sqlStr = "SELECT ID_ModPag, Descrizione FROM [MOD_PAG] WITH(NOLOCK)" &
                        " WHERE ID_ModPag IN (10,4)" &
                        " ORDER BY [Descrizione] DESC"

                    SqlModalitaPagamento.SelectCommand = sqlStr
                    DropDownModalitaPagamento.DataBind()

                    DropDownModalitaPagamento.Enabled = True                    
                End If

                
                tx_data.Text = Libreria.myFormatta(Now, "dd/MM/yyyy")
                'tx_importo.Text = Session("DaPagare")                

                modalita_cassa.Visible = True

                modalita_carta_credito_tel.Visible = False

                tr_autorizzazione_telefonica.Visible = False

                tr_01.Visible = False

                tr_02.Visible = False

                bt_stampa_riga_cassa.Visible = False

                'ABBUONI
                dropTipoPagamentoAbbuoni.Items.Clear()
                dropTipoPagamentoAbbuoni.Items.Add("Seleziona...")

                sqlStr = "SELECT ID_TIPPag, descrizione + ' (' + SEGNO + ')' descrizione FROM [TIP_PAG] WITH(NOLOCK)" &
                    " WHERE ID_TIPPag IN (653868889,-1577445210)" &
                    " ORDER BY MOV_CASSA, Descrizione"


                sqlTipoPagamentoAbbuoni.SelectCommand = sqlStr
                dropTipoPagamentoAbbuoni.DataBind()
                dropTipoPagamentoAbbuoni.Enabled = True

            Case Is = 2 'Autorizzazione Telefonica
                lb_tipo_pagamento.Text = "Autorizzazione Telefonica"

                DropDownTipoPagamento.Items.Clear()
                DropDownTipoPagamento.Items.Add("Seleziona...")

                Dim sqlStr As String

                sqlStr = "SELECT ID_TIPPag, descrizione + ' (' + SEGNO + ')' descrizione FROM [TIP_PAG] WITH(NOLOCK)" &
                    " WHERE ID_TIPPag IN (-1768195793, -1768195794)" &
                    " ORDER BY MOV_CASSA, Descrizione"

                sqlTipoPagamento.SelectCommand = sqlStr
                DropDownTipoPagamento.DataBind()
                DropDownTipoPagamento.Enabled = True

                tx_data.Text = Libreria.myFormatta(Now, "dd/MM/yyyy")
                tx_importo.Text = Session("DaPagare")

                modalita_cassa.Visible = False

                modalita_carta_credito_tel.Visible = False

                tr_autorizzazione_telefonica.Visible = False

                tr_01.Visible = False


                bt_stampa_riga_cassa.Visible = False
            Case Is = 3 'Full Credit
                lb_tipo_pagamento.Text = "Full Credit"

                DropDownTipoPagamento.Items.Clear()
                DropDownTipoPagamento.Items.Add("Seleziona...")

                Dim sqlStr As String = "SELECT ID_TIPPag, descrizione + ' (' + SEGNO + ')' descrizione FROM [TIP_PAG] WITH(NOLOCK)" &
                    " WHERE ID_TIPPag IN (-496006762)" &
                    " ORDER BY MOV_CASSA, Descrizione"

                sqlTipoPagamento.SelectCommand = sqlStr
                DropDownTipoPagamento.DataBind()
                DropDownTipoPagamento.Enabled = True
                DropDownTipoPagamento.SelectedValue = -496006762
                DropDownTipoPagamento.Enabled = False

                DropDownModalitaPagamento.Items.Clear()
                DropDownModalitaPagamento.Items.Add("Seleziona...")

                sqlStr = "SELECT ID_ModPag, Descrizione FROM [MOD_PAG] WITH(NOLOCK)" &
                    " WHERE ID_ModPag IN (-88607905)" &
                    " ORDER BY [Descrizione] DESC"

                SqlModalitaPagamento.SelectCommand = sqlStr
                DropDownModalitaPagamento.DataBind()
                DropDownModalitaPagamento.Enabled = True
                DropDownModalitaPagamento.SelectedValue = -88607905
                DropDownModalitaPagamento.Enabled = False

                tx_data.Text = Libreria.myFormatta(Now, "dd/MM/yyyy")
                tx_importo.Text = Session("DaPagare")

                Label7.Visible = False 'Seconda Modalità Pagamento
                DropDownEnti.Visible = False

                Label10.Visible = False 'Autorizzazione
                DropDownAutorizzazioni.Visible = False

                Label4.Visible = False 'Nr Carta di credito
                tx_titolo.Visible = False
                Label5.Visible = False 'Mese scadenze
                tx_mese_scadenza.Visible = False
                Label9.Visible = False 'Anno Scadenza
                tx_anno_scadenza.Visible = False

                Label6.Visible = False 'Autorizzazione
                tx_num_autorizzazione.Visible = False

                bt_stampa_riga_cassa.Visible = False

            Case Is = 4 'Complimentary
                lb_tipo_pagamento.Text = "Complimentary"                

                DropDownTipoPagamento.Items.Clear()
                DropDownTipoPagamento.Items.Add("Seleziona...")

                Dim sqlStr As String = "SELECT ID_TIPPag, descrizione + ' (' + SEGNO + ')' descrizione FROM [TIP_PAG] WITH(NOLOCK)" &
                    " WHERE ID_TIPPag IN (276309583)" &
                    " ORDER BY MOV_CASSA, Descrizione"

                sqlTipoPagamento.SelectCommand = sqlStr
                DropDownTipoPagamento.DataBind()
                DropDownTipoPagamento.Enabled = True
                DropDownTipoPagamento.SelectedValue = 276309583
                DropDownTipoPagamento.Enabled = False

                DropDownModalitaPagamento.Items.Clear()
                DropDownModalitaPagamento.Items.Add("Seleziona...")

                modalita_cassa.Visible = True

                sqlStr = "SELECT ID_ModPag, Descrizione FROM [MOD_PAG] WITH(NOLOCK)" &
                    " WHERE ID_ModPag IN (6)" &
                    " ORDER BY [Descrizione] DESC"

                SqlModalitaPagamento.SelectCommand = sqlStr
                DropDownModalitaPagamento.DataBind()
                DropDownModalitaPagamento.Enabled = True
                DropDownModalitaPagamento.SelectedValue = 6
                DropDownModalitaPagamento.Enabled = False

                tx_data.Text = Libreria.myFormatta(Now, "dd/MM/yyyy")
                tx_importo.Text = Session("DaPagare")

                modalita_carta_credito_tel.Visible = False

                tr_autorizzazione_telefonica.Visible = False

                tr_01.Visible = False

                tr_02.Visible = False

                bt_stampa_riga_cassa.Visible = False
        End Select

        tx_nota.Text = ""

        If lblModalita.Text = "PRENOTAZIONE" Then
            ValorizzaImportoDaPrenotazione2()
        Else
            tx_importo.Text = Session("DaPagare")
        End If

        If lb_tipo_pagamento.Text = "Complimentary" Then
            tx_importo.Text = 0
            tx_importo.Enabled = False
        End If
        

    End Sub

    Protected Sub carica_datiPos(ByVal id As String)       
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            'Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM contratti,stazioni WITH(NOLOCK) WHERE contratti.id_stazione_uscita = stazioni.id and num_contratto='" & id & "'", Dbc)
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM stazioni WITH(NOLOCK) WHERE stazioni.id='" & Request.Cookies("SicilyRentCar")("stazione") & "'", Dbc)
            'Response.Write(Cmd.CommandText & "<br><br>")
            'Response.End()
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            If Rs.HasRows Then
                Do While Rs.Read
                    lblStazione.Text = Rs("codice") & " - " & Rs("nome_stazione")
                    IdStazione.Text = Rs("id")
                    'Response.Write("<br>Contratto " & txtId.Text)

                    Select Case Session("provenienza")
                        Case Is = "Contratto"
                            lblModalita.Text = "CONTRATTO"
                        Case Is = "Prenotazione"
                            lblModalita.Text = "PRENOTAZIONE"
                        Case Is = "Multe"
                            lblModalita.Text = "MULTE"
                    End Select
                    lblStazioneCodice.Text = Rs("codice")

                    lb_tipo_pagamento.Text = "POS"
                    lblNumeroDocumento.Text = id
                    lb_operatore.Text = Libreria.getNomeOperatoreDaId(Request.Cookies("SicilyRentCar")("idUtente"))

                Loop
            Else

            End If

            Rs.Close()
            Dbc.Close()
            Rs = Nothing
            Dbc = Nothing

        Catch ex As Exception
            Libreria.genUserMsgBox(Me, ex.Message & " Carica Dati POS --- Errore contattare amministratore del sistema.")
        End Try
    End Sub

    Protected Sub bt_contanti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_contanti.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "139") < 3 Then
            Libreria.genUserMsgBox(Page, "Non hai i permessi per effettuare un Pagamenti in Contanti")
            Return
        End If

        div_pagamento_pulsantiera.Visible = True
        div_contenitore_pos.Visible = False

        carica_dati(txtId.Text, 1)
    End Sub

    Protected Sub bt_AutorizzazioneTelefonica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_AutorizzazioneTelefonica.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "161") < 3 Then
            Libreria.genUserMsgBox(Page, "Non hai i permessi per effettuare una Autorizzazione Telefonica")
            Return
        End If

        div_pagamento_pulsantiera.Visible = True
        div_contenitore_pos.Visible = False

        carica_dati(txtId.Text, 2)
    End Sub

    Protected Sub bt_full_credit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_full_credit.Click
        div_pagamento_pulsantiera.Visible = True

        carica_dati(txtId.Text, 3)
    End Sub

    Protected Sub bt_chiudi_riga_cassa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_chiudi_riga_cassa.Click        
        Select Case lblModalita.Text
            Case Is = "PRENOTAZIONE"
                Response.Redirect("prenotazioni.aspx?nr=" & lb_documento_riferimento.Text)
            Case Is = "CONTRATTO"
                Response.Redirect("contratti.aspx?nr=" & lb_documento_riferimento.Text)
            Case Is = "MULTE"
                Response.Redirect("gestione_multe.aspx?IdMulta=" & lblNumeroDocumento.Text)
        End Select
    End Sub

    Protected Sub bt_salva_riga_cassa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_salva_riga_cassa.Click
        If sql_inj(tx_importo.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lb_documento_riferimento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If sql_inj(tx_nota.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lb_documento_riferimento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If sql_inj(txtImportoAbbuoni.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lb_documento_riferimento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If sql_inj(tx_titolo.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lb_documento_riferimento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If sql_inj(tx_num_autorizzazione.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lb_documento_riferimento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneCassa) < 3 Then
            Libreria.genUserMsgBox(Page, "Non hai i permessi per salvare il movimento di cassa.")
            Return
        End If

        Dim Messaggio As String = ""

        If controllo_campi_ok() = 0 Then
            tx_importo.BackColor = Drawing.Color.White
            tx_nota.BackColor = Drawing.Color.White
            DropDownTipoPagamento.BackColor = Drawing.Color.White
            DropDownModalitaPagamento.BackColor = Drawing.Color.White


            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
            Dim Sql As String
            Dim Sql2 As String
            Dim SqlQuery As String

            Try
                Select Case bt_salva_riga_cassa.Text
                    Case Is = "Salva"
                        Dim ArrayDataTime(1) As String
                        Dim ArrayData(2) As String

                        Dim DataOggi As String                        

                        Dim idPosFunzioni_ares As String

                        ArrayDataTime = Split(Now, " ")
                        ArrayData = Split(ArrayDataTime(0), "/")

                        'modificato salvo 12.04.2023 x formato data
                        DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString
                        'DataOggi = ArrayData(2) & "-" & ArrayData(1) & "-" & ArrayData(0) & " " & ArrayDataTime(1)

                        'Rilevare se sito di Sviluppo o Produzione per settare corettamente DataOggi
                        'Response.Write(Request.ServerVariables("HTTP_REFERER") & "<br>")
                        Dim ArrayPercorso(3) As String
                        ArrayPercorso = Split(Request.ServerVariables("HTTP_REFERER"), "/")
                        'Response.Write(ArrayPercorso(2) & "<br>")

                        If ArrayPercorso(2) = "ares.sicilyrentcar.it" Then
                            DataOggi = CDate(DataOggi)
                            'modificato salvo 12.04.2023 x formato data
                            DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString
                        End If


                        'Importo negativo
                        If idPosFunzioni_ares = "8" Or idPosFunzioni_ares = "15" Then
                            txtImporto.Text = "-" & txtImporto.Text
                        End If

                        If txtImportoAbbuoni.Text <> "" Then
                            If CDbl(Replace(txtImportoAbbuoni.Text, ".", ",")) > 2 Then
                                Libreria.genUserMsgBox(Page, "Non è possibile fare un reso oltre a 2 Euro")
                                Return
                            End If
                            Select Case dropTipoPagamentoAbbuoni.SelectedValue
                                Case Is = "653868889" 'Abbuono -
                                    idPosFunzioni_ares = "11"
                                    txtImportoAbbuoni.Text = "-" & txtImportoAbbuoni.Text
                                Case Is = "-1577445210" 'Abbuono +
                                    idPosFunzioni_ares = "12"
                            End Select

                            If lblModalita.Text = "PRENOTAZIONE" Then
                                Sql = "insert into PAGAMENTI_EXTRA " & _
                                                        "(Nr_Contratto,Data,ID_TIPPAG,ID_ModPag,id_pos_funzioni_ares,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_PREN_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker,NOTE) " & _
                                                        "values('" & GetMaxId(lblCodiceStazione.Text) & "','" & DataOggi & "','" & dropTipoPagamentoAbbuoni.SelectedValue & "','6','" & idPosFunzioni_ares & "','" & funzioni_comuni.GetIdStazioneDaCodice(lblCodiceStazione.Text) & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lb_documento_riferimento.Text & "','" & Replace(txtImportoAbbuoni.Text, ",", ".") & "','" & DataOggi & "','0','" & tx_nota.Text & "')"
                            Else
                                Sql = "insert into PAGAMENTI_EXTRA " & _
                                                        "(Nr_Contratto,Data,ID_TIPPAG,ID_ModPag,id_pos_funzioni_ares,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_CONTRATTO_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker,NOTE) " & _
                                                        "values('" & GetMaxId(lblCodiceStazione.Text) & "','" & DataOggi & "','" & dropTipoPagamentoAbbuoni.SelectedValue & "','6','" & idPosFunzioni_ares & "','" & funzioni_comuni.GetIdStazioneDaCodice(lblCodiceStazione.Text) & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lb_documento_riferimento.Text & "','" & Replace(txtImportoAbbuoni.Text, ",", ".") & "','" & DataOggi & "','0','" & tx_nota.Text & "')"
                            End If

                            'Response.Write(Sql)
                            'Response.End()

                            Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
                            Cmd.ExecuteNonQuery()

                            'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                            'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                            'Session("residenza_virtuale") = Cmd.ExecuteScalar

                            SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                            Cmd.CommandText = SqlQuery
                            'Response.Write(Cmd.CommandText & "<br/>")
                            'Response.End()
                            Cmd.ExecuteNonQuery()

                            'tx_importo.Text = CDbl(Replace(tx_importo.Text, ".", ",")) + CDbl(Replace(txtImportoAbbuoni.Text, ".", ","))
                        End If

                        Select Case DropDownTipoPagamento.SelectedValue
                            Case Is = "3" 'CH Pagamento +
                                idPosFunzioni_ares = "7"
                            Case Is = "-1886319629" 'DE Deposito su RA +
                                idPosFunzioni_ares = "8"
                            Case Is = "-714677539" 'RB Rimborso Deposito Su RA -
                                idPosFunzioni_ares = "15"
                                tx_importo.Text = "-" & tx_importo.Text
                            Case Is = "2" 'CA Rimborso Su RA -
                                idPosFunzioni_ares = "9"
                                tx_importo.Text = "-" & tx_importo.Text
                            Case Is = "276309583" 'Complementary
                                idPosFunzioni_ares = "14"
                            Case Is = "-1768195793" 'Autorizzazione Telefonica
                                idPosFunzioni_ares = "10"
                        End Select

                        'Response.Write("Funzione " & GetIdStazioneDaCodice(lblCodiceStazione.Text) & " testo " & lblCodiceStazione.Text)
                        'Response.End()


                        If ddl_stazioni_pagamento.Visible = True And ddl_stazioni_pagamento.SelectedValue <> "0" Then
                            'Response.Write("IF<br>")
                            If lblModalita.Text = "PRENOTAZIONE" Then
                                Sql = "insert into PAGAMENTI_EXTRA " & _
                                                        "(Nr_Contratto,Data,ID_TIPPAG,ID_ModPag,id_pos_funzioni_ares,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_PREN_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker,NOTE,operazione_stornata) " & _
                                                        "values('" & GetMaxId(ddl_stazioni_pagamento.SelectedValue) & "','" & DataOggi & "','" & DropDownTipoPagamento.SelectedValue & "','" & DropDownModalitaPagamento.SelectedValue & "','" & idPosFunzioni_ares & "','" & ddl_stazioni_pagamento.SelectedValue & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lb_documento_riferimento.Text & "','" & Replace(tx_importo.Text, ",", ".") & "','" & DataOggi & "','0','" & tx_nota.Text & "','0')"

                            End If
                            If lblModalita.Text = "CONTRATTO" Then
                                Sql = "insert into PAGAMENTI_EXTRA " & _
                                                        "(Nr_Contratto,Data,ID_TIPPAG,ID_ModPag,id_pos_funzioni_ares,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_CONTRATTO_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker,NOTE,operazione_stornata) " & _
                                                        "values('" & GetMaxId(ddl_stazioni_pagamento.SelectedValue) & "','" & DataOggi & "','" & DropDownTipoPagamento.SelectedValue & "','" & DropDownModalitaPagamento.SelectedValue & "','" & idPosFunzioni_ares & "','" & ddl_stazioni_pagamento.SelectedValue & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lb_documento_riferimento.Text & "','" & Replace(tx_importo.Text, ",", ".") & "','" & DataOggi & "','0','" & tx_nota.Text & "','0')"

                            End If

                            If lblModalita.Text = "MULTE" Then
                                Sql = "insert into PAGAMENTI_EXTRA " & _
                                                        "(Nr_Contratto,Data,ID_TIPPAG,ID_ModPag,id_pos_funzioni_ares,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_MULTA_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker,NOTE,operazione_stornata) " & _
                                                        "values('" & GetMaxId(ddl_stazioni_pagamento.SelectedValue) & "','" & DataOggi & "','" & DropDownTipoPagamento.SelectedValue & "','" & DropDownModalitaPagamento.SelectedValue & "','" & idPosFunzioni_ares & "','" & ddl_stazioni_pagamento.SelectedValue & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lb_documento_riferimento.Text & "','" & Replace(tx_importo.Text, ",", ".") & "','" & DataOggi & "','0','" & tx_nota.Text & "','0')"

                            End If

                        Else
                            If lblModalita.Text = "PRENOTAZIONE" Then
                                If DropDownEnti.Visible = True Then
                                    Sql = "insert into PAGAMENTI_EXTRA " & _
                                                        "(Nr_Contratto,Data,ID_TIPPAG,ID_ModPag,id_pos_funzioni_ares,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_PREN_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker,NOTE,titolo,scadenza,nr_aut,preaut_aperta,operazione_stornata) " & _
                                                        "values('" & GetMaxId(IdStazione.Text) & "','" & DataOggi & "','" & DropDownTipoPagamento.SelectedValue & "','1','" & idPosFunzioni_ares & "','" & funzioni_comuni.GetIdStazioneDaCodice(lblCodiceStazione.Text) & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lb_documento_riferimento.Text & "','" & Replace(tx_importo.Text, ",", ".") & "','" & DataOggi & "','0','" & tx_nota.Text & "','" & cripta(tx_titolo.Text, 37) & "','" & tx_mese_scadenza.Text & "/" & tx_anno_scadenza.Text & "','" & tx_num_autorizzazione.Text & "','1','0')"
                                Else
                                    Sql = "insert into PAGAMENTI_EXTRA " &
                                                        "(Nr_Contratto,Data,ID_TIPPAG,ID_ModPag,id_pos_funzioni_ares,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_PREN_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker,NOTE,operazione_stornata) " &
                                                        "values('" & GetMaxId(IdStazione.Text) & "',convert(datetime,'" & DataOggi & "',102),'" & DropDownTipoPagamento.SelectedValue & "','" & DropDownModalitaPagamento.SelectedValue & "','" & idPosFunzioni_ares & "','" & funzioni_comuni.GetIdStazioneDaCodice(lblCodiceStazione.Text) & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lb_documento_riferimento.Text & "','" & Replace(tx_importo.Text, ",", ".") & "','" & DataOggi & "','0','" & tx_nota.Text & "','0')"
                                End If

                            End If
                            If lblModalita.Text = "CONTRATTO" Then
                                If DropDownEnti.Visible = True Then
                                    Sql = "insert into PAGAMENTI_EXTRA " &
                                                        "(Nr_Contratto,Data,ID_TIPPAG,ID_ModPag,id_pos_funzioni_ares,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_CONTRATTO_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker,NOTE,titolo,scadenza,nr_aut,preaut_aperta,operazione_stornata) " &
                                                        "values('" & GetMaxId(IdStazione.Text) & "',convert(datetime,'" & DataOggi & "',102),'" & DropDownTipoPagamento.SelectedValue & "','1','" & idPosFunzioni_ares & "','" & funzioni_comuni.GetIdStazioneDaCodice(lblCodiceStazione.Text) & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "',convert(datetime,'" & DataOggi & "',102),'" & lb_operatore.Text & "','" & lb_documento_riferimento.Text & "','" & Replace(tx_importo.Text, ",", ".") & "','" & DataOggi & "','0','" & tx_nota.Text & "','" & cripta(tx_titolo.Text, 37) & "','" & tx_mese_scadenza.Text & "/" & tx_anno_scadenza.Text & "','" & tx_num_autorizzazione.Text & "','1','0')"
                                Else
                                    Sql = "insert into PAGAMENTI_EXTRA " &
                                                        "(Nr_Contratto,Data,ID_TIPPAG,ID_ModPag,id_pos_funzioni_ares,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_CONTRATTO_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker,NOTE,operazione_stornata) " &
                                                        "values('" & GetMaxId(IdStazione.Text) & "',convert(datetime,'" & DataOggi & "',102),'" & DropDownTipoPagamento.SelectedValue & "','" & DropDownModalitaPagamento.SelectedValue & "','" & idPosFunzioni_ares & "','" & funzioni_comuni.GetIdStazioneDaCodice(lblCodiceStazione.Text) & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "',convert(datetime,'" & DataOggi & "',102),'" & lb_operatore.Text & "','" & lb_documento_riferimento.Text & "','" & Replace(tx_importo.Text, ",", ".") & "',convert(datetime,'" & DataOggi & "',102),'0','" & tx_nota.Text & "','0')"
                                End If
                            End If
                        End If


                        'Response.Write(Sql & "<br>")
                        'Response.End()

                        Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
                        Dim x As Integer = Cmd.ExecuteNonQuery()

                        'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                        'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                        'Session("residenza_virtuale") = Cmd.ExecuteScalar

                        SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                        Cmd.CommandText = SqlQuery
                        'Response.Write(Cmd.CommandText & "<br/>")
                        'Response.End()
                        Cmd.ExecuteNonQuery()

                        If lblModalita.Text = "PRENOTAZIONE" Then
                            'Imposto righe prepagate
                            ImpostoRighePrepagate(lb_documento_riferimento.Text)
                        End If

                        tx_importo.Enabled = False
                End Select
            Catch ex As Exception
                Response.Write(ex)
                Response.Write("<br><br>")
                Libreria.genUserMsgBox(Me, "Salvataggio Ins Pagamenti Contanti Errore contattare amministratore del sistema.")
                Response.Write(Cmd.CommandText)
            End Try

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            WhiteList(lblNumeroDocumento.Text, Replace(tx_importo.Text, ",", "."))

            Messaggio = "Pagamento registrato correttamente."
            If Messaggio <> "" Then
                genUserMsgBox(Page, Messaggio)
                Session("PagamentoRegistrato") = True
            Else
                Session("PagamentoRegistrato") = False
            End If
            If Session("PagamentoRegistrato") = True Then
                If lblModalita.Text = "CONTRATTO" Then
                    If Session("InModificaEstensione") Then
                        Session("InModificaEstensione") = False
                        Response.Write("<SCRIPT Language=JavaScript>history.back()</SCRIPT>")
                    Else
                        Response.Redirect("contratti.aspx?nr=" & lb_documento_riferimento.Text)
                    End If
                End If
                If lblModalita.Text = "PRENOTAZIONE" Then
                    Response.Redirect("prenotazioni.aspx?nr=" & lb_documento_riferimento.Text)
                End If

            End If


            'InviaMail(txtId.Text)
            'Response.Redirect("elenco_anagrafe_stazioni.aspx")
        Else
            Select Case controllo_campi_ok()
                Case 1
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    tx_nota.BackColor = Drawing.Color.White
                    DropDownTipoPagamento.BackColor = Drawing.Color.White
                    DropDownModalitaPagamento.BackColor = Drawing.Color.White
                    txtImportoAbbuoni.BackColor = Drawing.Color.White

                    tx_importo.BackColor = Drawing.Color.Yellow
                    tx_importo.Focus()
                Case 3
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    tx_importo.BackColor = Drawing.Color.White
                    tx_nota.BackColor = Drawing.Color.White
                    DropDownModalitaPagamento.BackColor = Drawing.Color.White
                    txtImportoAbbuoni.BackColor = Drawing.Color.White


                    DropDownTipoPagamento.BackColor = Drawing.Color.Yellow
                    DropDownTipoPagamento.Focus()
                Case 4
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    tx_importo.BackColor = Drawing.Color.White
                    tx_nota.BackColor = Drawing.Color.White
                    DropDownTipoPagamento.BackColor = Drawing.Color.White
                    txtImportoAbbuoni.BackColor = Drawing.Color.White

                    DropDownModalitaPagamento.BackColor = Drawing.Color.Yellow
                    DropDownModalitaPagamento.Focus()
                Case 5
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    tx_importo.BackColor = Drawing.Color.White
                    tx_nota.BackColor = Drawing.Color.White
                    DropDownTipoPagamento.BackColor = Drawing.Color.White
                    DropDownModalitaPagamento.BackColor = Drawing.Color.White

                    txtImportoAbbuoni.BackColor = Drawing.Color.Yellow
                    txtImportoAbbuoni.Focus()
            End Select
        End If
    End Sub

    'Tony 31/01/2023
    Protected Sub WhiteList(ByVal NumDocumento As String, ByVal Importo As String)
        Dim ConnectionStrings As String = ""
        Dim Sql, SqlQuery As String

        'rimosso salvo 21.02.2023
        'ConnectionStrings = "Data Source=.\SQLEXPRESS;Initial Catalog=Autonoleggio_SRC;Integrated Security=True;MultipleActiveResultSets=True;Max Pool Size=50000;Pooling=True;"

        Try
            'rimosso salvo 21.02.2023
            'Dim Dbc As New Data.SqlClient.SqlConnection(ConnectionStrings)

            'modificato Salvo 21.02.2023 
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            '@ end salvo

            Sql = "select * from conducenti_black_list WITH(NOLOCK) where black_list='" & lblNumeroDocumento.Text & "' and somma_da_pagare='-" & tx_importo.Text & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(Sql, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            If Rs.HasRows Then
                Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc2.Open()

                Dim Cmd2 As New Data.SqlClient.SqlCommand("delete from conducenti_black_list where id=" & Rs("id"), Dbc2)

                Cmd2 = New Data.SqlClient.SqlCommand(Sql, Dbc)
                Cmd2.ExecuteNonQuery()

                SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                Cmd2.CommandText = SqlQuery
                'Response.Write(Cmd.CommandText & "<br/>")
                'Response.End()
                Cmd2.ExecuteNonQuery()

                Cmd2.Dispose()
                Cmd2 = Nothing
                Dbc2.Close()
                Dbc2.Dispose()
                Dbc2 = Nothing
            End If

            Cmd.Dispose()
            Cmd = Nothing

            Rs.Close()
            Dbc.Close()
            Rs = Nothing
            Dbc = Nothing
        Catch ex As Exception
            Libreria.genUserMsgBox(Me, "Errore in WhiteList.")
        End Try
    End Sub
    'FINE Tony


    Protected Function controllo_campi_ok() As Integer
        If tx_importo.Text = "" Or tx_importo.Text <= 0 Then
            If lb_tipo_pagamento.Text = "Complimentary" Then
                controllo_campi_ok = 0
            Else
                controllo_campi_ok = 1
            End If
        ElseIf DropDownTipoPagamento.SelectedItem.Text = "Seleziona..." Then
            controllo_campi_ok = 3
        ElseIf DropDownModalitaPagamento.SelectedItem.Text = "Seleziona..." Then
            controllo_campi_ok = 4
        ElseIf txtImportoAbbuoni.Visible = True Then
            If txtImportoAbbuoni.Text = "" Then
                controllo_campi_ok = 5
            End If
        Else
            controllo_campi_ok = 0
        End If
    End Function

    Protected Function controllo_campiAcquistoCarte_ok() As Integer
       If txtImporto.Text = "" Or txtImporto.Text = "0" Then
            controllo_campiAcquistoCarte_ok = 1
        ElseIf txtTitolareCarta.Text = "" Then
            controllo_campiAcquistoCarte_ok = 2
        ElseIf txtNumerodiCarta.Text = "" Then
            controllo_campiAcquistoCarte_ok = 3
        ElseIf txtScadenzaMese.Text = "" Then
            controllo_campiAcquistoCarte_ok = 4
        ElseIf txtScadenzaAnno.Text = "" Then
            controllo_campiAcquistoCarte_ok = 5
        ElseIf Int(txtScadenzaMese.Text) > 12 Or Int(txtScadenzaMese.Text) < 1 Then
            controllo_campiAcquistoCarte_ok = 6
        ElseIf Not CheckOkMeseScadenzaCarta(txtScadenzaMese.Text, txtScadenzaAnno.Text) Then
            controllo_campiAcquistoCarte_ok = 7
        Else
            controllo_campiAcquistoCarte_ok = 0
        End If
    End Function

    Protected Function controllo_campiPreautorizzaCarte_ok() As Integer
        If txtImporto.Text = "" Or txtImporto.Text = "0" Then
            controllo_campiPreautorizzaCarte_ok = 1
        ElseIf txtTitolareCartaPreautorizzazione.Text = "" Then
            controllo_campiPreautorizzaCarte_ok = 2
        ElseIf txtNumerodiCartaPreautorizzazione.Text = "" Then
            controllo_campiPreautorizzaCarte_ok = 3
        ElseIf txtScadenzaMesePreautorizzazione.Text = "" Then
            controllo_campiPreautorizzaCarte_ok = 4
        ElseIf txtScadenzaAnnoPreautorizzazione.Text = "" Then
            controllo_campiPreautorizzaCarte_ok = 5
        ElseIf Int(txtScadenzaMesePreautorizzazione.Text) > 12 Or Int(txtScadenzaMesePreautorizzazione.Text) < 1 Then
            controllo_campiPreautorizzaCarte_ok = 6
        ElseIf Not CheckOkMeseScadenzaCarta(txtScadenzaMesePreautorizzazione.Text, txtScadenzaAnnoPreautorizzazione.Text) Then
            controllo_campiPreautorizzaCarte_ok = 7
        ElseIf txtPreautorizzazioneCarte.Text = "" Then
            controllo_campiPreautorizzaCarte_ok = 8
        ElseIf txtCodiceAuthPreautorizzazione.Text = "" Then
            controllo_campiPreautorizzaCarte_ok = 9
        Else
            controllo_campiPreautorizzaCarte_ok = 0
        End If
    End Function

    Protected Function controllo_campiChiusuraPreautorizzazione_ok() As Integer
        If txtImporto.Text = "" Or txtImporto.Text = "0" Then
            controllo_campiChiusuraPreautorizzazione_ok = 1
        ElseIf dropChiusuraPreautorizzazione.SelectedValue = "Seleziona..." Then
            controllo_campiChiusuraPreautorizzazione_ok = 2
            'ElseIf txtNoteChiusuraPreautorizzazioneCarte.Text = "" Then
            '    controllo_campiChiusuraPreautorizzazione_ok = 3
        Else
            controllo_campiChiusuraPreautorizzazione_ok = 0
        End If
    End Function

    Protected Function controllo_campiRimborsoCarte_ok() As Integer
        If txtImporto.Text = "" Or txtImporto.Text = "0" Then
            controllo_campiRimborsoCarte_ok = 1        
        Else
            controllo_campiRimborsoCarte_ok = 0
        End If
    End Function



    'Bancomat
    Protected Function controllo_campiAcquistoBM_ok() As Integer
        If txtImporto.Text = "" Or txtImporto.Text = "0" Then
            controllo_campiAcquistoBM_ok = 1
        ElseIf txtTitolareCartaAcquistoBancomat.Text = "" Then
            controllo_campiAcquistoBM_ok = 2
        ElseIf txtScadenzaMeseAcquistoBancomat.Text = "" Then
            controllo_campiAcquistoBM_ok = 3
        ElseIf txtScadenzaAnnoAcquistoBancomat.Text = "" Then
            controllo_campiAcquistoBM_ok = 4
        ElseIf Int(txtScadenzaMeseAcquistoBancomat.Text) > 12 Or Int(txtScadenzaMeseAcquistoBancomat.Text) < 1 Then
            controllo_campiAcquistoBM_ok = 5
        ElseIf Not CheckOkMeseScadenzaCarta(txtScadenzaMeseAcquistoBancomat.Text, txtScadenzaAnnoAcquistoBancomat.Text) Then
            controllo_campiAcquistoBM_ok = 6
        Else
            controllo_campiAcquistoBM_ok = 0
        End If
    End Function

    Protected Function controllo_campiDepositoBM_ok() As Integer
        If txtImporto.Text = "" Or txtImporto.Text = "0" Then
            controllo_campiDepositoBM_ok = 1
        ElseIf txtTitolareCartaDepositoBancomat.Text = "" Then
            controllo_campiDepositoBM_ok = 2
        ElseIf txtScadenzaMeseDepositoBancomat.Text = "" Then
            controllo_campiDepositoBM_ok = 3
        ElseIf txtScadenzaAnnoDepositoBancomat.Text = "" Then
            controllo_campiDepositoBM_ok = 4
        ElseIf Int(txtScadenzaMeseDepositoBancomat.Text) > 12 Or Int(txtScadenzaMeseDepositoBancomat.Text) < 1 Then
            controllo_campiDepositoBM_ok = 5
        ElseIf Not CheckOkMeseScadenzaCarta(txtScadenzaMeseDepositoBancomat.Text, txtScadenzaAnnoDepositoBancomat.Text) Then
            controllo_campiDepositoBM_ok = 6
        ElseIf txtCodiceAuthDepositoBancomat.Text = "" Then
            controllo_campiDepositoBM_ok = 7
        Else
            controllo_campiDepositoBM_ok = 0
        End If
    End Function

    Protected Function controllo_campiRimborsoDepositoBM_ok() As Integer
        If txtImporto.Text = "" Or txtImporto.Text = "0" Then
            controllo_campiRimborsoDepositoBM_ok = 1        
        Else
            controllo_campiRimborsoDepositoBM_ok = 0
        End If
    End Function

    Public Function GetMaxId(ByVal codice As String) As String

        Dim ris As String = ""

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Try


            Dim sqlStr As String = "select max(nr_contratto) from PAGAMENTI_EXTRA where Nr_Contratto like '" & codice & "%'"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris = Int(Rs1(0)) + 1
            End If

            Rs1.Close()
            Cmd1.Dispose()
            Dbc.Close()
            Rs1 = Nothing
            Cmd1 = Nothing
            Dbc = Nothing

        Catch ex As Exception

        End Try

        Return ris
    End Function

    Protected Sub bt_pos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_pos.Click
        div_pagamento_pulsantiera.Visible = False
        divIntestazione.Visible = False
        div_contenitore_pos.Visible = True
        divImmagine.Visible = True

        carica_datiPos(txtId.Text)

        InizializzaColore()
        InizializzaColoreCircuito()
        InizializzaColoreFunzionalita()
        InizializzoDivPos()
        listCircuiti.Visible = False
        lisFunzionalità.Visible = False
    End Sub

    Protected Sub btnChiudiPos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudiPos.Click        
        Select Case lblModalita.Text
            Case Is = "PRENOTAZIONE"
                Response.Redirect("prenotazioni.aspx?nr=" & lblNumeroDocumento.Text)
            Case Is = "CONTRATTO"
                Response.Redirect("contratti.aspx?nr=" & lblNumeroDocumento.Text)
            Case Is = "MULTE"
                Response.Redirect("gestione_multe.aspx?IdMulta=" & lblNumeroDocumento.Text)
        End Select
    End Sub

    Protected Sub bt_complimentary_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_complimentary.Click


        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "143") < 3 Then
            Libreria.genUserMsgBox(Page, "Non hai i permessi per effettuare un pagamento Complimentary")
            Return
        End If

        'bt_complimentary.Enabled = False

        div_pagamento_pulsantiera.Visible = True
        div_contenitore_pos.Visible = False

        carica_dati(txtId.Text, 4)
    End Sub

    Protected Sub DropDownModalitaPagamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownModalitaPagamento.SelectedIndexChanged
        If DropDownModalitaPagamento.SelectedValue = "4" And DropDownTipoPagamento.SelectedValue = "3" Then
            btnAbbuoni.Visible = True
        Else
            btnAbbuoni.Visible = False
            lblTipoPagamentiAbbuoni.Visible = False
            dropTipoPagamentoAbbuoni.Visible = False
            lblImportoAbbuoni.Visible = False
            txtImportoAbbuoni.Visible = False
        End If

    End Sub

    Protected Sub btnAbbuoni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAbbuoni.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "144") < 3 Then
            Libreria.genUserMsgBox(Page, "Non hai i permessi per effettuare un Abbuono")
            Return
        End If

        lblTipoPagamentiAbbuoni.Visible = True
        dropTipoPagamentoAbbuoni.Visible = True
        lblImportoAbbuoni.Visible = True
        txtImportoAbbuoni.Visible = True
    End Sub

    Protected Sub DropDownTipoPagamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownTipoPagamento.SelectedIndexChanged

        'Prelevo Importo da valorizzare in Prenotazione
        If lblModalita.Text = "PRENOTAZIONE" Then
            ValorizzaImportoDaPrenotazione2()
        Else
            tx_importo.Text = Session("DaPagare")
        End If

        Select Case DropDownTipoPagamento.SelectedValue           
            Case Is = "-1886319629" 'Deposito su RA
                tx_importo.Text = Session("DaPreautorizzare")               
                btnAbbuoni.Visible = False
            Case Is = "-714677539" 'Rimborso Deposito
                tx_importo.Text = "0,00"
                btnAbbuoni.Visible = False
            Case Is = "2" 'Rimborso
                tx_importo.Text = "0,00"
                btnAbbuoni.Visible = False
            Case Is = "3" 'Pagamento
                If DropDownModalitaPagamento.SelectedValue = "4" Then
                    btnAbbuoni.Visible = True                
                End If
            Case Is = "-1768195793"
                modalita_carta_credito_tel.Visible = True 'modalita carta credito tel

                DropDownEnti.Items.Clear()
                DropDownEnti.Items.Add("Seleziona...")

                Dim sqlStr As String

                sqlStr = "SELECT ID_ModPag, Descrizione FROM [MOD_PAG] WITH(NOLOCK)" &
                    " WHERE ID_ModPag IN (1)" &
                    " ORDER BY [Descrizione] DESC"

                sqlEnti.SelectCommand = sqlStr
                DropDownEnti.DataBind()
                DropDownEnti.Enabled = True

                lb_telefono.Text = "N.V."
                lb_codice_esercente.Text = "N.V."
                lb_durata_autorizzazione.Text = "N.V."

                tr_autorizzazione_telefonica.Visible = False

                tr_01.Visible = True
                tr_02.Visible = True
                tx_titolo.Text = ""
                tx_mese_scadenza.Text = ""
                tx_anno_scadenza.Text = ""
                tx_num_autorizzazione.Text = ""

            Case Is = "-1768195794"
                modalita_carta_credito_tel.Visible = False

                tr_autorizzazione_telefonica.Visible = True 'autorizzazione carta credito tel
                tr_01.Visible = False
                tr_02.Visible = False

                ValorizzazioneDropAutorizzazione()

        End Select
    End Sub

    Protected Sub ValorizzazioneDropAutorizzazione()
        Dim sqlStr As String

        DropDownAutorizzazioni.Items.Clear()
        DropDownAutorizzazioni.Items.Add("Seleziona...")

        If lblModalita.Text = "PRENOTAZIONE" Then
            sqlStr = "select ID_CTR,nr_aut,scadenza_preaut from PAGAMENTI_EXTRA WITH(NOLOCK) where ID_TIPPAG = '-1768195793' and  preaut_aperta = '1' and N_PREN_RIF = '" & lb_documento_riferimento.Text & "'"
        Else
            sqlStr = "select ID_CTR,nr_aut,scadenza_preaut from PAGAMENTI_EXTRA WITH(NOLOCK) where ID_TIPPAG = '-1768195793' and  preaut_aperta = '1' and N_CONTRATTO_RIF = '" & lb_documento_riferimento.Text & "'"
        End If
        'Response.Write(sqlStr)

        SqlAutorizzazioni.SelectCommand = sqlStr
        DropDownAutorizzazioni.DataBind()
        DropDownAutorizzazioni.Enabled = True
    End Sub

    Protected Sub DropDownEnti_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownEnti.SelectedIndexChanged
        ValorizzaDatiAutorizzazione("1")
    End Sub

    Protected Sub ValorizzaDatiAutorizzazione(ByVal IdTipModPag As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Try
            Dim sqlStr As String = "select * from POS_autorizzazioni WITH(NOLOCK) where id_tip_mod_pag = '" & IdTipModPag & "'"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                lb_telefono.Text = Rs1("telefono")
                lb_codice_esercente.Text = Rs1("codice_esercente")
                lb_durata_autorizzazione.Text = Rs1("gg_autorizzazione") & " gg."
            End If

            Rs1.Close()
            Cmd1.Dispose()
            Dbc.Close()
            Rs1 = Nothing
            Cmd1 = Nothing
            Dbc = Nothing

        Catch ex As Exception
            Libreria.genUserMsgBox(Me, "Valorizzazione Dati Autorizzazione Errore contattare amministratore del sistema.")
        End Try
    End Sub

    Protected Sub DropDownAutorizzazioni_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownAutorizzazioni.SelectedIndexChanged
        ValorizzaDatiSceltaAutorizzazione(DropDownAutorizzazioni.SelectedValue)
    End Sub

    Protected Sub ValorizzaDatiSceltaAutorizzazione(ByVal IdRiga As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        'Dim Security As PAGAMENTI_EXTRA = Nothing

        Try
            Dim sqlStr As String = "select * from PAGAMENTI_EXTRA WITH(NOLOCK) where ID_CTR = '" & IdRiga & "'"
            'Response.Write(sqlStr)
            'Response.End()

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                tr_01.Visible = True

                lb_scadenza_autorizzazione.Text = Rs1("Scadenza")
                lb_importo_autorizzato.Text = Rs1("PER_IMPORTO")
                tx_importo.Text = Rs1("PER_IMPORTO")
                'tx_titolo.Text = decryptString("Titolo")
                tx_titolo.Text = "XXXXXXXXXXXXXXX"

                Dim arrayMeseAnnoScadenza(1) As String

                arrayMeseAnnoScadenza = Split(Rs1("scadenza"), "/")
                tx_mese_scadenza.Text = arrayMeseAnnoScadenza(0)
                tx_anno_scadenza.Text = arrayMeseAnnoScadenza(1)

                tx_nota.Text = Rs1("note")
            End If

            Rs1.Close()
            Cmd1.Dispose()
            Dbc.Close()
            Rs1 = Nothing
            Cmd1 = Nothing
            Dbc = Nothing

        Catch ex As Exception
            Libreria.genUserMsgBox(Me, "Valorizzazione Dati Autorizzazione Errore contattare amministratore del sistema.")
        End Try
    End Sub

    Public Function encryptString(ByVal strtext As String) As String
        Dim key As String = "&/?@*>:>"
        Return Encrypt(strtext, key)
    End Function

    Public Function decryptString(ByVal strtext As String) As String
        Dim key As String = "&/?@*>:>"
        Return Decrypt(strtext, key)
    End Function

    'The function used to encrypt the text
    Private Function Encrypt(ByVal strText As String, ByVal strEncrKey _
    As String) As String
        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Left(strEncrKey, 8))
            Dim des As New DESCryptoServiceProvider()
            Dim inputByteArray() As Byte = Encoding.UTF8.GetBytes(strText)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Return Convert.ToBase64String(ms.ToArray())
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    'The function used to decrypt the text
    Private Function Decrypt(ByVal strText As String, ByVal sDecrKey _
    As String) As String
        Dim byKey() As Byte = {}
        Dim IV() As Byte = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}
        Dim inputByteArray(strText.Length) As Byte
        Try
            byKey = System.Text.Encoding.UTF8.GetBytes(Left(sDecrKey, 8))
            Dim des As New DESCryptoServiceProvider()
            inputByteArray = Convert.FromBase64String(strText)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
            Return encoding.GetString(ms.ToArray())
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Protected Sub listEnti_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listEnti.ItemCommand
        Dim idLabel As Label = e.Item.FindControl("ID")
        Dim btnScelto As Button = e.Item.FindControl("btnEnte")

        InizializzaColore()
        InizializzoDivPos()

        If e.CommandName = "Modifica" Then
            sqlCircuiti.SelectCommand = "select  distinct(POS_circuiti.Nome)  from POS_enti_proprietari,POS_circuiti,POS_Funzioni,POS_Enti_Circuiti_Funzioni WITH(NOLOCK) where POS_Enti_Circuiti_Funzioni.id_ente = POS_enti_proprietari.id and POS_Enti_Circuiti_Funzioni.id_circuito = POS_circuiti.id and POS_Enti_Circuiti_Funzioni.id_funzione = POS_Funzioni.id and POS_Enti_Circuiti_Funzioni.id_ente ='" & idLabel.Text & "' order by POS_circuiti.Nome"
            'Response.Write(sqlCircuiti.SelectCommand)

            listCircuiti.DataBind()
            listCircuiti.Visible = True
            'Response.Write("ID " & idLabel.Text)

            btnScelto.BackColor = Drawing.Color.Green

            lblEnteScelto.Text = idLabel.Text
        End If
    End Sub

    Protected Sub InizializzaColore()
        Dim btnScelto As Button

        'Pone tutti gli sfondi ad Arancione
        For i = 0 To listEnti.Items.Count - 1
            btnScelto = listEnti.Items(i).FindControl("btnEnte")
            btnScelto.BackColor = Color.FromName("#e88532")
        Next
    End Sub

    Protected Sub listCircuiti_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listCircuiti.ItemCommand        
        Dim btnScelto As Button = e.Item.FindControl("btnEnte")

        InizializzaColoreCircuito()
        InizializzoDivPos()

        If e.CommandName = "Modifica" Then
            sqlFunzioni.SelectCommand = "select * from POS_enti_proprietari,POS_circuiti,POS_Funzioni,POS_Enti_Circuiti_Funzioni WITH(NOLOCK) where POS_Enti_Circuiti_Funzioni.id_ente = POS_enti_proprietari.id and POS_Enti_Circuiti_Funzioni.id_circuito = POS_circuiti.id and POS_Enti_Circuiti_Funzioni.id_funzione = POS_Funzioni.id and POS_Enti_Circuiti_Funzioni.id_ente ='" & lblEnteScelto.Text & "' and POS_circuiti.Nome ='" & btnScelto.Text & "' order by POS_Funzioni.ordine"
            'Response.Write(sqlFunzioni.SelectCommand)
            'Response.End()

            lisFunzionalità.DataBind()
            lisFunzionalità.Visible = True
            'Response.Write("ID " & idLabel.Text)

            btnScelto.BackColor = Drawing.Color.Green

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            'Dim Security As PAGAMENTI_EXTRA = Nothing

            Try
                Dim sqlStr As String = "select * from Pos_circuiti WITH(NOLOCK) where nome = '" & btnScelto.Text & "'"
                'Response.Write(sqlStr)
                'Response.End()

                Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dim Rs1 As Data.SqlClient.SqlDataReader
                Rs1 = Cmd1.ExecuteReader()

                If Rs1.HasRows Then
                    Rs1.Read()
                    lblCircuitoScelto.Text = Rs1("id")
                    lblIntestazione.Text = btnScelto.Text
                    lblTestoCircuito.Text = btnScelto.Text
                End If

                Rs1.Close()
                Cmd1.Dispose()
                Dbc.Close()
                Rs1 = Nothing
                Cmd1 = Nothing
                Dbc = Nothing

            Catch ex As Exception
                Libreria.genUserMsgBox(Me, "Select Dati per Id Circuito Errore contattare amministratore del sistema.")
            End Try

            'divImmagine.Visible = False

            'divIntestazione.Visible = True
            'txtImporto.Enabled = False

        End If
    End Sub

    Protected Sub InizializzaColoreCircuito()
        Dim btnScelto As Button

        'Pone tutti gli sfondi ad Arancione
        For i = 0 To listCircuiti.Items.Count - 1
            btnScelto = listCircuiti.Items(i).FindControl("btnEnte")
            btnScelto.BackColor = Color.FromName("#e88532")
        Next

        divImmagine.Visible = True
        divIntestazione.Visible = False
    End Sub

    Protected Sub btnChiudiPos2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudiPos2.Click
        Select Case lblModalita.Text
            Case Is = "PRENOTAZIONE"
                Response.Redirect("prenotazioni.aspx?nr=" & lblNumeroDocumento.Text)
            Case Is = "CONTRATTO"
                Response.Redirect("contratti.aspx?nr=" & lblNumeroDocumento.Text)
            Case Is = "MULTE"
                Response.Redirect("gestione_multe.aspx?IdMulta=" & lblNumeroDocumento.Text)
        End Select
    End Sub

    Protected Sub lisFunzionalità_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles lisFunzionalità.ItemCommand
        Dim btnScelto As Button = e.Item.FindControl("btnFunzione")

        InizializzaColoreFunzionalita()

        divImmagine.Visible = False

        divIntestazione.Visible = True
        txtImporto.Enabled = False

        If e.CommandName = "Modifica" Then                        

            Select Case btnScelto.Text
                Case Is = "Acquisto"                    
                    InizializzoDivPos()

                    'Funzione Stazione di pagamento
                    If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "162") < 3 Then
                        ddl_stazioni_pagamento2.Visible = False
                    Else
                        ddl_stazioni_pagamento2.Visible = True
                    End If

                    'Prelevo Importo da valorizzare in Prenotazione
                    If lblModalita.Text = "PRENOTAZIONE" Then
                        ValorizzaImportoDaPrenotazione()
                    Else
                        txtImporto.Text = Session("DaPagare")
                    End If

                    'If CDbl(txtImporto.Text) <= 0 Then
                    '    Libreria.genUserMsgBox(Me, "Importo deve essere maggiore di Zero")
                    'End If

                    If lblCircuitoScelto.Text = 20 Then 'Carta di Credito
                        divCampiAcquistoCarte.Visible = True
                    Else 'Bancomat                        
                        divCampiAcquistoBancomat.Visible = True
                    End If

                    btnScelto.BackColor = Drawing.Color.Green                    
                Case Is = "Preautorizzazione"
                    If lblModalita.Text <> "PRENOTAZIONE" Then
                        InizializzoDivPos()

                        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "162") < 3 Then
                            ddl_stazioni_pagamento2.Visible = False
                        Else
                            ddl_stazioni_pagamento2.Visible = True
                        End If

                        txtImporto.Text = Session("DaPreautorizzare")
                        'If CDbl(txtImporto.Text) <= 0 Then
                        '    Libreria.genUserMsgBox(Me, "Preautorizzare un valore maggiore di Zero")

                        'End If

                        divCampiPreautorizzazioneCarte.Visible = True

                        btnScelto.BackColor = Drawing.Color.Green
                    Else
                        Libreria.genUserMsgBox(Page, "Funzionalità NON prevista in Prenotazione")
                        Return
                    End If
                Case Is = "Chiusura Preautorizzazione"
                    If lblModalita.Text <> "PRENOTAZIONE" Then
                        InizializzoDivPos()

                        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "162") < 3 Then
                            ddl_stazioni_pagamento2.Visible = False
                        Else
                            ddl_stazioni_pagamento2.Visible = True
                        End If

                        'Prelevo Importo da valorizzare in Prenotazione
                        If lblModalita.Text = "PRENOTAZIONE" Then
                            ValorizzaImportoDaPrenotazione()
                        Else
                            txtImporto.Text = Session("DaPagare")
                        End If
                        'If CDbl(txtImporto.Text) <= 0 Then
                        '    Libreria.genUserMsgBox(Me, "Importo deve essere maggiore di Zero")

                        'End If

                        divCampiChiusuraPreautorizzazioneCarte.Visible = True

                        btnScelto.BackColor = Drawing.Color.Green

                        'Valorizzazione Drop Preautorizzazione                    
                        Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        Dbc2.Open()

                        Try
                            Dim sqlStr As String = "SELECT ID_CTR, '| ' + NR_PREAUT + ' | ' + CONVERT(varchar, data, 120) + ' | ' + CONVERT(varchar, PER_IMPORTO)  as testo FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE preaut_aperta = 1 AND N_CONTRATTO_RIF = '" & lblNumeroDocumento.Text & "'"
                            'Response.Write(sqlStr)
                            'Response.End()

                            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                            Dim Rs1 As Data.SqlClient.SqlDataReader
                            Rs1 = Cmd1.ExecuteReader()

                            dropChiusuraPreautorizzazione.Items.Clear()
                            dropChiusuraPreautorizzazione.Items.Add("Seleziona...")

                            If Rs1.HasRows Then
                                SqlPreautorizzazione.SelectCommand = sqlStr
                                dropChiusuraPreautorizzazione.DataBind()
                            End If

                            Rs1.Close()
                            Cmd1.Dispose()
                            Dbc2.Close()
                            Rs1 = Nothing
                            Cmd1 = Nothing
                            Dbc2 = Nothing

                        Catch ex As Exception
                            Libreria.genUserMsgBox(Me, "Select Dati per drop Chiusura Preautirizzazione Errore contattare amministratore del sistema.")
                        End Try
                    Else
                        Libreria.genUserMsgBox(Page, "Funzionalità NON prevista in Prenotazione")
                        Return
                    End If
                Case Is = "Storno"
                    'Funzione: Pulsanti Rimborsi (Rimborso Deposito e Rimborso)
                    If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "164") < 3 Then
                        Libreria.genUserMsgBox(Page, "Non hai i permessi per effettuare il Rimborso")
                        Return
                    Else
                        InizializzoDivPos()

                        'Funzione: Stazione di pagamento
                        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "162") < 3 Then
                            ddl_stazioni_pagamento2.Visible = False
                        Else
                            ddl_stazioni_pagamento2.Visible = True
                        End If

                        'Prelevo Importo da valorizzare in Prenotazione
                        If lblModalita.Text = "PRENOTAZIONE" Then
                            ValorizzaImportoDaPrenotazione()
                        Else
                            txtImporto.Text = Session("DaPagare")
                        End If

                        If txtImporto.Text & "" <> "" Then
                            If CDbl(txtImporto.Text) <= 0 Then
                                txtImporto.Text = Replace(txtImporto.Text, "-", "")
                            End If
                        End If

                        If lblCircuitoScelto.Text = 20 Then 'Carta di Credito
                            divCampiRimborsoCarte.Visible = True
                        Else 'Bancomat
                            divCampiRimborsoBancomat.Visible = True
                        End If

                        btnScelto.BackColor = Drawing.Color.Green
                    End If

                Case Is = "Deposito"
                    If lblModalita.Text <> "PRENOTAZIONE" Then
                        InizializzoDivPos()

                        'Funzione: Stazione di pagamento
                        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "162") < 3 Then
                            ddl_stazioni_pagamento2.Visible = False
                        Else
                            ddl_stazioni_pagamento2.Visible = True
                        End If

                        txtImporto.Text = Session("DaPreautorizzare")
                        'If CDbl(txtImporto.Text) <= 0 Then
                        '    Libreria.genUserMsgBox(Me, "Importo di deposito deve essere maggiore di Zero")

                        'End If

                        divCampiDepositoBancomat.Visible = True

                        btnScelto.BackColor = Drawing.Color.Green
                    Else
                        Libreria.genUserMsgBox(Page, "Funzionalità NON prevista in Prenotazione")
                        Return
                    End If

                Case Is = "Rimborso Deposito"
                    'Funzione: Pulsanti Rimborsi (Rimborso Deposito e Rimborso)
                    If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "164") < 3 Then
                        Libreria.genUserMsgBox(Page, "Non hai i permessi per effettuare il Rimborso")
                        Return
                    Else
                        InizializzoDivPos()

                        'Funzione: Stazione di pagamento
                        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "162") < 3 Then
                            ddl_stazioni_pagamento2.Visible = False
                        Else
                            ddl_stazioni_pagamento2.Visible = True
                        End If

                        'Prelevo Importo da valorizzare in Prenotazione
                        If lblModalita.Text = "PRENOTAZIONE" Then
                            ValorizzaImportoDaPrenotazione()
                        Else
                            txtImporto.Text = Session("DaPagare")
                        End If
                        'If CDbl(txtImporto.Text) <= 0 Then
                        '    Libreria.genUserMsgBox(Me, "Importo deve essere maggiore di Zero")
                        'End If

                        divCampiRimborsoDepositoBancomat.Visible = True

                        'Selezione La somma dei Depositi da rimborsare
                        Dim Dbc3 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        Dbc3.Open()

                        Try
                            Dim sqlStr As String = "select sum(PER_IMPORTO) as somma from PAGAMENTI_EXTRA where ID_TIPPAG ='-1886319629' and N_CONTRATTO_RIF ='" & lblNumeroDocumento.Text & "'"
                            'Response.Write(sqlStr)
                            'Response.End()

                            Dim Cmd3 As New Data.SqlClient.SqlCommand(sqlStr, Dbc3)
                            Dim Rs3 As Data.SqlClient.SqlDataReader
                            Rs3 = Cmd3.ExecuteReader()

                            If Rs3.HasRows Then
                                Rs3.Read()
                                If Rs3("somma") & "" <> "" Then
                                    txtImporto.Text = Format(Rs3("somma"), "0.00")
                                Else
                                    txtImporto.Text = "0.00"
                                End If

                            End If

                            Rs3.Close()
                            Cmd3.Dispose()
                            Dbc3.Close()
                            Rs3 = Nothing
                            Cmd3 = Nothing
                            Dbc3 = Nothing

                        Catch ex As Exception
                            Libreria.genUserMsgBox(Me, "Select Somma Depositi Errore contattare amministratore del sistema.")
                        End Try

                        'Valorizzazione Drop Preautorizzazione                    
                        Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        Dbc2.Open()

                        Try
                            Dim sqlStr As String = "SELECT ID_CTR, '| ' + codiceAuth + ' | ' + CONVERT(varchar, data, 120) + ' | ' + CONVERT(varchar, PER_IMPORTO)  as testo FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE id_tippag = '-1886319629' AND N_CONTRATTO_RIF = '" & lblNumeroDocumento.Text & "'"
                            'Response.Write(sqlStr)
                            'Response.End()

                            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                            Dim Rs1 As Data.SqlClient.SqlDataReader
                            Rs1 = Cmd1.ExecuteReader()

                            dropCodiceAuthRimborso.Items.Clear()
                            dropCodiceAuthRimborso.Items.Add("Seleziona...")

                            If Rs1.HasRows Then
                                SqlDepositoBM.SelectCommand = sqlStr
                                dropCodiceAuthRimborso.DataBind()
                            End If

                            Rs1.Close()
                            Cmd1.Dispose()
                            Dbc2.Close()
                            Rs1 = Nothing
                            Cmd1 = Nothing
                            Dbc2 = Nothing

                        Catch ex As Exception
                            Libreria.genUserMsgBox(Me, "Select Dati per drop Chiusura Preautirizzazione Errore contattare amministratore del sistema.")
                        End Try


                        btnScelto.BackColor = Drawing.Color.Green
                    End If
            End Select

            lblIntestazione.Text = lblTestoCircuito.Text & " - " & btnScelto.Text

            If lblModalita.Text = "PRENOTAZIONE" Then

                'Funzione: Pulsanti Rimborsi (Rimborso Deposito e Rimborso)
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "164") < 3 Then
                    txtImporto.Enabled = False
                Else
                    txtImporto.Enabled = True
                End If

            Else
                txtImporto.Enabled = True
            End If

            bt_salva_riga_cassa2.Visible = True

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            'Dim Security As PAGAMENTI_EXTRA = Nothing

            Try
                Dim sqlStr As String = "select * from POS_enti_proprietari,POS_circuiti,POS_Funzioni,POS_Enti_Circuiti_Funzioni WITH(NOLOCK) where POS_Enti_Circuiti_Funzioni.id_ente = POS_enti_proprietari.id and POS_Enti_Circuiti_Funzioni.id_circuito = POS_circuiti.id and POS_Enti_Circuiti_Funzioni.id_funzione = POS_Funzioni.id and POS_Enti_Circuiti_Funzioni.id_ente ='" & lblEnteScelto.Text & "' and POS_Enti_Circuiti_Funzioni.id_circuito ='" & lblCircuitoScelto.Text & "'  and Funzione ='" & btnScelto.Text & "' order by POS_Funzioni.ordine"
                'Response.Write(sqlStr)
                'Response.End()

                Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dim Rs1 As Data.SqlClient.SqlDataReader
                Rs1 = Cmd1.ExecuteReader()

                If Rs1.HasRows Then
                    Rs1.Read()
                    lblFunzioneScelta.Text = Rs1("id_funzione")
                End If

                Rs1.Close()
                Cmd1.Dispose()
                Dbc.Close()
                Rs1 = Nothing
                Cmd1 = Nothing
                Dbc = Nothing

            Catch ex As Exception
                Libreria.genUserMsgBox(Me, "Select Dati per Id Funzione Errore contattare amministratore del sistema.")
            End Try
        End If
    End Sub

    Protected Sub InizializzaColoreFunzionalita()
        Dim btnScelto As Button

        'Pone tutti gli sfondi ad Arancione
        For i = 0 To lisFunzionalità.Items.Count - 1
            btnScelto = lisFunzionalità.Items(i).FindControl("btnFunzione")
            btnScelto.BackColor = Color.FromName("#e88532")
        Next
    End Sub

    Protected Sub InizializzoDivPos()
        divCampiAcquistoCarte.Visible = False
        divCampiPreautorizzazioneCarte.Visible = False
        divCampiChiusuraPreautorizzazioneCarte.Visible = False
        divCampiRimborsoCarte.Visible = False

        divCampiAcquistoBancomat.Visible = False
        divCampiDepositoBancomat.Visible = False
        divCampiRimborsoDepositoBancomat.Visible = False
        divCampiRimborsoBancomat.Visible = False

        txtImporto.Text = ""
        txtImporto.Enabled = False
        bt_salva_riga_cassa2.Visible = False

        InizializzoCampiAcquistoCarte()
        InizializzoCampiPreautorizzazioneCarte()
    End Sub

    Protected Sub InizializzoCampiAcquistoCarte()
        txtImporto.Text = ""
        txtTitolareCarta.Text = ""
        txtNumerodiCarta.Text = ""
        txtScadenzaMese.Text = ""
        txtScadenzaAnno.Text = ""

        txtImporto.BackColor = Drawing.Color.White
        txtTitolareCarta.BackColor = Drawing.Color.White
        txtNumerodiCarta.BackColor = Drawing.Color.White
        txtScadenzaMese.BackColor = Drawing.Color.White
        txtScadenzaAnno.BackColor = Drawing.Color.White
    End Sub

    Protected Sub InizializzoCampiPreautorizzazioneCarte()
        txtImporto.Text = ""
        txtTitolareCartaPreautorizzazione.Text = ""
        txtNumerodiCartaPreautorizzazione.Text = ""
        txtScadenzaMesePreautorizzazione.Text = ""
        txtScadenzaAnnoPreautorizzazione.Text = ""
        txtPreautorizzazioneCarte.Text = ""
        txtCodiceAuthPreautorizzazione.Text = ""

        txtImporto.BackColor = Drawing.Color.White
        txtTitolareCartaPreautorizzazione.BackColor = Drawing.Color.White
        txtNumerodiCartaPreautorizzazione.BackColor = Drawing.Color.White
        txtScadenzaMesePreautorizzazione.BackColor = Drawing.Color.White
        txtScadenzaAnnoPreautorizzazione.BackColor = Drawing.Color.White
        txtPreautorizzazioneCarte.BackColor = Drawing.Color.White
        txtCodiceAuthPreautorizzazione.BackColor = Drawing.Color.White
    End Sub

    Protected Sub bt_salva_riga_cassa2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_salva_riga_cassa2.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneCassa) < 3 Then
            Libreria.genUserMsgBox(Page, "Non hai i permessi per salvare il movimento di cassa.")
            Return
        End If

        If sql_inj(txtImporto.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        'Acquisto con Carta di Credito
        If lblCircuitoScelto.Text = "20" And lblFunzioneScelta.Text = "4" Then
            Acquisto_con_Carta_Credito()
        End If

        'Preautorizzazione con Carta di Credito
        If lblCircuitoScelto.Text = "20" And lblFunzioneScelta.Text = "1" Then
            Preautorizzazione_con_Carta_Credito()
        End If

        'Chiusura Preautorizzazione con Carta di Credito
        If lblCircuitoScelto.Text = "20" And lblFunzioneScelta.Text = "3" Then
            Chiusura_Preautorizzazione_con_Carta_Credito()
        End If

        'Rimborso con Carta di Credito
        If lblCircuitoScelto.Text = "20" And lblFunzioneScelta.Text = "9" Then
            Rimborso_con_Carta_Credito()
        End If

        'BANCOMAT -------------------------------------------------------------------------

        'Acquisto con Bancomat
        If lblCircuitoScelto.Text = "21" And lblFunzioneScelta.Text = "4" Then
            Acquisto_con_Bancomat()
        End If

        'Deposito con Bancomat
        If lblCircuitoScelto.Text = "21" And lblFunzioneScelta.Text = "17" Then
            Deposito_con_Bancomat()
        End If

        'Rimborso Deposito con Bancomat
        If lblCircuitoScelto.Text = "21" And lblFunzioneScelta.Text = "15" Then
            Rimborso_Deposito_con_Bancomat()
        End If

        'Rimborso con Bancomat
        If lblCircuitoScelto.Text = "21" And lblFunzioneScelta.Text = "9" Then
            Rimborso_con_Bancomat()
        End If
    End Sub

    Private Function CheckOkMeseScadenzaCarta(ByVal meseCifra As String, ByVal annoCifra As String) As Boolean
        Dim MeseScadenza As Integer
        Dim AnnoScadenza As Integer
        Dim msg As String = String.Empty


        AnnoScadenza = annoCifra + 2000
        MeseScadenza = meseCifra

        Dim DScadenza As DateTime = New DateTime(AnnoScadenza, MeseScadenza, 1)
        'Response.Write(DScadenza & " ")
        Dim DOggi As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)
        'Response.Write(DOggi)

        If DScadenza < DOggi Then
            CheckOkMeseScadenzaCarta = 0
        Else
            CheckOkMeseScadenzaCarta = 1
        End If

    End Function

    Protected Sub Acquisto_con_Carta_Credito()
        If sql_inj(txtTitolareCarta.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If sql_inj(txtNumerodiCarta.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If sql_inj(txtScadenzaMese.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If sql_inj(txtScadenzaAnno.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        Dim Messaggio As String = ""

        If controllo_campiAcquistoCarte_ok() = 0 Then           
            txtImporto.BackColor = Drawing.Color.White
            txtTitolareCarta.BackColor = Drawing.Color.White
            txtNumerodiCarta.BackColor = Drawing.Color.White
            txtScadenzaMese.BackColor = Drawing.Color.White
            txtScadenzaAnno.BackColor = Drawing.Color.White

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
            Dim Sql As String
            Dim Sql2 As String
            Dim SqlQuery As String

            Try
                Dim ArrayDataTime(1) As String
                Dim ArrayData(2) As String
                Dim DataOggi As String
                Dim idPosFunzioni_ares As String

                ArrayDataTime = Split(Now, " ")
                ArrayData = Split(ArrayDataTime(0), "/")
                DataOggi = ArrayData(2) & "-" & ArrayData(1) & "-" & ArrayData(0) & " " & ArrayDataTime(1)
                'modificato salvo 12.04.2023 x formato data
                DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString

                'Rilevare se sito di Sviluppo o Produzione per settare corettamente DataOggi
                'Response.Write(Request.ServerVariables("HTTP_REFERER") & "<br>")
                Dim ArrayPercorso(3) As String
                ArrayPercorso = Split(Request.ServerVariables("HTTP_REFERER"), "/")
                'Response.Write(ArrayPercorso(2) & "<br>")

                If ArrayPercorso(2) = "ares.sicilyrentcar.it" Then
                    DataOggi = CDate(DataOggi)
                    'modificato salvo 12.04.2023 x formato data
                    DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString
                End If


                idPosFunzioni_ares = "4"
                'Response.Write("Funzione " & GetIdStazioneDaCodice(lblStazioneCodice.Text) & " testo " & lblCodiceStazione.Text)
                'Response.End()


                Dim CassaSuCuiOperare As String

                'If Request.Cookies("SicilyRentCar")("nome") = "Prestigiacomo Angela" Then
                '    CassaSuCuiOperare = ddl_stazioni_pagamento2.SelectedValue
                'Else
                '    CassaSuCuiOperare = lblStazioneCodice.Text
                'End If

                'Permesso Stazione di pagamento
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "162") = 3 Then
                    CassaSuCuiOperare = funzioni_comuni.GetStazioneCodice(ddl_stazioni_pagamento2.SelectedValue)
                    lblStazioneID.Text = ddl_stazioni_pagamento2.SelectedValue
                Else
                    CassaSuCuiOperare = lblStazioneCodice.Text
                    lblStazioneID.Text = funzioni_comuni.GetIdStazioneDaCodice(lblStazioneCodice.Text)
                End If

                If lblModalita.Text = "CONTRATTO" Then
                    Sql = "insert into PAGAMENTI_EXTRA " & _
                          "(Nr_Contratto,Data,ID_TIPPAG,id_pos_funzioni_ares,Importo,cassa,Titolo,Intestatario,Scadenza,nr_aut,operazione_stornata,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_CONTRATTO_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker) " & _
                          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','1011098650','" & idPosFunzioni_ares & "','0.000'" & ",'" & CassaSuCuiOperare & "','" & cripta(txtNumerodiCarta.Text, 37) & "','" & Replace(txtTitolareCarta.Text, "'", "''") & "','" & txtScadenzaMese.Text & "/" & txtScadenzaAnno.Text & "','0000023','0','" & lblStazioneID.Text & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','" & Replace(txtImporto.Text, ",", ".") & "','" & DataOggi & "','0')"
                ElseIf lblModalita.Text = "PRENOTAZIONE" Then
                    Sql = "insert into PAGAMENTI_EXTRA " & _
                          "(Nr_Contratto,Data,ID_TIPPAG,id_pos_funzioni_ares,Importo,cassa,Titolo,Intestatario,Scadenza,nr_aut,operazione_stornata,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_PREN_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker) " & _
                          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','1011098650','" & idPosFunzioni_ares & "','0.000'" & ",'" & CassaSuCuiOperare & "','" & cripta(txtNumerodiCarta.Text, 37) & "','" & Replace(txtTitolareCarta.Text, "'", "''") & "','" & txtScadenzaMese.Text & "/" & txtScadenzaAnno.Text & "','0000023','0','" & lblStazioneID.Text & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','" & Replace(txtImporto.Text, ",", ".") & "','" & DataOggi & "','0')"
                ElseIf lblModalita.Text = "MULTE" Then
                    Sql = "insert into PAGAMENTI_EXTRA " & _
                          "(Nr_Contratto,Data,ID_TIPPAG,id_pos_funzioni_ares,Importo,cassa,Titolo,Intestatario,Scadenza,nr_aut,operazione_stornata,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_MULTA_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker) " & _
                          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','1011098650','" & idPosFunzioni_ares & "','0.000'" & ",'" & CassaSuCuiOperare & "','" & cripta(txtNumerodiCarta.Text, 37) & "','" & Replace(txtTitolareCarta.Text, "'", "''") & "','" & txtScadenzaMese.Text & "/" & txtScadenzaAnno.Text & "','0000023','0','" & lblStazioneID.Text & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','" & Replace(txtImporto.Text, ",", ".") & "','" & DataOggi & "','0')"
                End If

                'Response.Write(Sql)
                'Response.End()

                Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
                Cmd.ExecuteNonQuery()

                'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                'Session("residenza_virtuale") = Cmd.ExecuteScalar

                SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                Cmd.CommandText = SqlQuery
                'Response.Write(Cmd.CommandText & "<br/>")
                'Response.End()
                Cmd.ExecuteNonQuery()

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                If lblModalita.Text = "PRENOTAZIONE" Then
                    'Imposto righe prepagate
                    ImpostoRighePrepagate(lblNumeroDocumento.Text)
                End If                

                Messaggio = "Pagamento registrato correttamente."
                If Messaggio <> "" Then
                    genUserMsgBox(Page, Messaggio)
                    Session("PagamentoRegistrato") = True
                Else
                    Session("PagamentoRegistrato") = False
                End If
                If Session("PagamentoRegistrato") = True Then
                    Select Case lblModalita.Text
                        Case Is = "PRENOTAZIONE"
                            Response.Redirect("prenotazioni.aspx?nr=" & lblNumeroDocumento.Text)
                        Case Is = "CONTRATTO"
                            Response.Redirect("contratti.aspx?nr=" & lblNumeroDocumento.Text)
                    End Select                    
                End If


                'InviaMail(txtId.Text)
                'Response.Redirect("elenco_anagrafe_stazioni.aspx")
            Catch ex As Exception
                Response.Write(ex)
                Response.Write("<br><br>")
                Libreria.genUserMsgBox(Me, "Salvataggio Ins Acquisto Carte Errore contattare amministratore del sistema.")
                Response.Write(Cmd.CommandText)
            End Try


        Else
            Select Case controllo_campiAcquistoCarte_ok()
                Case 1
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtTitolareCarta.BackColor = Drawing.Color.White
                    txtNumerodiCarta.BackColor = Drawing.Color.White
                    txtScadenzaMese.BackColor = Drawing.Color.White
                    txtScadenzaAnno.BackColor = Drawing.Color.White

                    txtImporto.BackColor = Drawing.Color.Yellow
                    txtImporto.Focus()
                Case 2
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtNumerodiCarta.BackColor = Drawing.Color.White
                    txtScadenzaMese.BackColor = Drawing.Color.White
                    txtScadenzaAnno.BackColor = Drawing.Color.White

                    txtTitolareCarta.BackColor = Drawing.Color.Yellow
                    txtTitolareCarta.Focus()
                Case 3
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCarta.BackColor = Drawing.Color.White
                    txtScadenzaMese.BackColor = Drawing.Color.White
                    txtScadenzaAnno.BackColor = Drawing.Color.White

                    txtNumerodiCarta.BackColor = Drawing.Color.Yellow
                    txtNumerodiCarta.Focus()
                Case 4
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCarta.BackColor = Drawing.Color.White
                    txtNumerodiCarta.BackColor = Drawing.Color.White
                    txtScadenzaAnno.BackColor = Drawing.Color.White

                    txtScadenzaMese.BackColor = Drawing.Color.Yellow
                    txtScadenzaMese.Focus()
                Case 5
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCarta.BackColor = Drawing.Color.White
                    txtNumerodiCarta.BackColor = Drawing.Color.White
                    txtScadenzaMese.BackColor = Drawing.Color.White

                    txtScadenzaAnno.BackColor = Drawing.Color.Yellow
                    txtScadenzaAnno.Focus()
                Case 6 'Mese non in formato valido > 12 or < 1
                    Messaggio = "il mese NON ha un formato valido."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCarta.BackColor = Drawing.Color.White
                    txtNumerodiCarta.BackColor = Drawing.Color.White
                    txtScadenzaAnno.BackColor = Drawing.Color.White

                    txtScadenzaMese.BackColor = Drawing.Color.Yellow
                    txtScadenzaMese.Focus()
                Case 7 'Campo NON numerico
                    Messaggio = "il mese e l'anno di scadenza risultato passati."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCarta.BackColor = Drawing.Color.White
                    txtNumerodiCarta.BackColor = Drawing.Color.White
                    txtScadenzaAnno.BackColor = Drawing.Color.White

                    txtScadenzaMese.BackColor = Drawing.Color.Yellow
                    txtScadenzaMese.Focus()
                    'Case 90
                    '    Messaggio = "Campo Obbligatorio."
                    '    If Messaggio <> "" Then
                    '        Libreria.genUserMsgBox(Page, Messaggio)
                    '    End If
                    '    txtImporto.BackColor = Drawing.Color.White
                    '    txtTitolareCarta.BackColor = Drawing.Color.White
                    '    txtNumerodiCarta.BackColor = Drawing.Color.White
                    '    txtScadenzaMese.BackColor = Drawing.Color.White
                    '    txtScadenzaAnno.BackColor = Drawing.Color.Yellow

                    '    ddl_stazioni_pagamento2.BackColor = Drawing.Color.Yellow
                    '    ddl_stazioni_pagamento2.Focus()
            End Select
        End If
    End Sub

    Protected Sub Preautorizzazione_con_Carta_Credito()
        If sql_inj(txtTitolareCartaPreautorizzazione.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If sql_inj(txtNumerodiCartaPreautorizzazione.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If sql_inj(txtScadenzaMesePreautorizzazione.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If sql_inj(txtScadenzaAnnoPreautorizzazione.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If sql_inj(txtPreautorizzazioneCarte.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If sql_inj(txtCodiceAuthPreautorizzazione.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        Dim Messaggio As String = ""

        If controllo_campiPreautorizzaCarte_ok() = 0 Then
            txtImporto.BackColor = Drawing.Color.White
            txtTitolareCartaPreautorizzazione.BackColor = Drawing.Color.White
            txtNumerodiCartaPreautorizzazione.BackColor = Drawing.Color.White
            txtScadenzaMesePreautorizzazione.BackColor = Drawing.Color.White
            txtScadenzaAnnoPreautorizzazione.BackColor = Drawing.Color.White
            txtPreautorizzazioneCarte.BackColor = Drawing.Color.White
            txtCodiceAuthPreautorizzazione.BackColor = Drawing.Color.White

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
            Dim Sql As String
            Dim Sql2 As String
            Dim SqlQuery As String

            Try
                Dim ArrayDataTime(1) As String
                Dim ArrayData(2) As String
                Dim DataOggi As String
                Dim DataScadenzaPreautorizzazione As String
                Dim idPosFunzioni_ares As String

                ArrayDataTime = Split(Now, " ")
                ArrayData = Split(ArrayDataTime(0), "/")

                DataOggi = ArrayData(2) & "-" & ArrayData(1) & "-" & ArrayData(0) & " " & ArrayDataTime(1)
                'modificato salvo 12.04.2023 x formato data
                DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString

                'Rilevare se sito di Sviluppo o Produzione per settare corettamente DataOggi
                'Response.Write(Request.ServerVariables("HTTP_REFERER") & "<br>")
                Dim ArrayPercorso(3) As String
                ArrayPercorso = Split(Request.ServerVariables("HTTP_REFERER"), "/")
                'Response.Write(ArrayPercorso(2) & "<br>")

                If ArrayPercorso(2) = "ares.sicilyrentcar.it" Then
                    DataOggi = CDate(DataOggi)
                    'modificato salvo 12.04.2023 x formato data
                    DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString

                End If

                'Fare una select su POS_enti_acquires_circuiti per gg preautorizzazione
                DataScadenzaPreautorizzazione = DateAdd("d", 21, DataOggi)


                idPosFunzioni_ares = "1"
                'Response.Write("Funzione " & GetIdStazioneDaCodice(lblStazioneCodice.Text) & " testo " & lblCodiceStazione.Text)
                'Response.End()

                'lblStazioneID.Text = funzioni_comuni.GetIdStazioneDaCodice(lblStazioneCodice.Text)

                Dim CassaSuCuiOperare As String

                'If Request.Cookies("SicilyRentCar")("nome") = "Prestigiacomo Angela" Then
                '    CassaSuCuiOperare = ddl_stazioni_pagamento2.SelectedValue
                'Else
                '    CassaSuCuiOperare = lblStazioneCodice.Text
                'End If

                'Permesso Stazione di pagamento
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "162") = 3 Then
                    CassaSuCuiOperare = funzioni_comuni.GetStazioneCodice(ddl_stazioni_pagamento2.SelectedValue)
                    lblStazioneID.Text = ddl_stazioni_pagamento2.SelectedValue
                Else
                    CassaSuCuiOperare = lblStazioneCodice.Text
                    lblStazioneID.Text = funzioni_comuni.GetIdStazioneDaCodice(lblStazioneCodice.Text)
                End If

                Sql = "insert into PAGAMENTI_EXTRA " & _
                         "(Nr_Contratto,Data,ID_TIPPAG,id_pos_funzioni_ares,Importo,cassa,Titolo,Intestatario,Scadenza,nr_aut,TIPSEGNO,preaut_aperta,operazione_stornata,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_CONTRATTO_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker,NR_PREAUT,codiceAuth) " & _
                        "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','-438610305','" & idPosFunzioni_ares & "','0.000'" & ",'" & CassaSuCuiOperare & "','" & cripta(txtNumerodiCartaPreautorizzazione.Text, 37) & "','" & Replace(txtTitolareCartaPreautorizzazione.Text, "'", "''") & "','" & txtScadenzaMesePreautorizzazione.Text & "/" & txtScadenzaAnnoPreautorizzazione.Text & "','511047','1','1','0','" & lblStazioneID.Text & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','" & Replace(txtImporto.Text, ",", ".") & "','" & DataOggi & "','0','" & txtPreautorizzazioneCarte.Text & "','" & txtCodiceAuthPreautorizzazione.Text & "')"

                'Sql = "insert into PAGAMENTI_EXTRA " & _
                '          "(Nr_Contratto,Data,ID_TIPPAG,id_pos_funzioni_ares,Importo,cassa,Titolo,Intestatario,Scadenza,nr_aut,TIPSEGNO,preaut_aperta,operazione_stornata,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_CONTRATTO_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker,NR_PREAUT,codiceAuth,CONTABILIZZATO,TRANSATION_TYPE,CARD_TYPE,acquire_id,action_code,scadenza_preaut) " & _
                '          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','-438610305','" & idPosFunzioni_ares & "','0.000'" & ",'" & CassaSuCuiOperare & "','" & cripta(txtNumerodiCartaPreautorizzazione.Text, 37) & "','" & txtTitolareCartaPreautorizzazione.Text & "','" & txtScadenzaMesePreautorizzazione.Text & "/" & txtScadenzaAnnoPreautorizzazione.Text & "','511047','1','1','0','" & funzioni_comuni.GetIdStazioneDaCodice(lblStazioneCodice.Text) & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','" & Replace(txtImporto.Text, ",", ".") & "','" & DataOggi & "','0','" & txtPreautorizzazioneCarte.Text & "','" & txtCodiceAuthPreautorizzazione.Text & "'.'0','MAG','2','2','0','" & DataScadenzaPreautorizzazione & "')"

                'Response.Write(Sql)
                'Response.End()

                Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
                Cmd.ExecuteNonQuery()

                'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                'Session("residenza_virtuale") = Cmd.ExecuteScalar

                SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                Cmd.CommandText = SqlQuery
                'Response.Write(Cmd.CommandText & "<br/>")
                'Response.End()
                Cmd.ExecuteNonQuery()

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                Messaggio = "Pagamento registrato correttamente."
                If Messaggio <> "" Then
                    genUserMsgBox(Page, Messaggio)
                    Session("PagamentoRegistrato") = True
                Else
                    Session("PagamentoRegistrato") = False
                End If
                If Session("PagamentoRegistrato") = True Then
                    Select Case lblModalita.Text
                        Case Is = "PRENOTAZIONE"
                            Response.Redirect("prenotazioni.aspx?nr=" & lblNumeroDocumento.Text)
                        Case Is = "CONTRATTO"
                            Response.Redirect("contratti.aspx?nr=" & lblNumeroDocumento.Text)
                    End Select
                End If


                'InviaMail(txtId.Text)
                '    'Response.Redirect("elenco_anagrafe_stazioni.aspx")
            Catch ex As Exception
                Response.Write(ex)
                Response.Write("<br><br>")
                Libreria.genUserMsgBox(Me, "Salvataggio Ins Acquisto Carte Errore contattare amministratore del sistema.")
                Response.Write(Cmd.CommandText)
            End Try

        Else
            Select Case controllo_campiPreautorizzaCarte_ok()
                Case 1
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtTitolareCartaPreautorizzazione.BackColor = Drawing.Color.White
                    txtNumerodiCartaPreautorizzazione.BackColor = Drawing.Color.White
                    txtScadenzaMesePreautorizzazione.BackColor = Drawing.Color.White
                    txtScadenzaAnnoPreautorizzazione.BackColor = Drawing.Color.White
                    txtPreautorizzazioneCarte.BackColor = Drawing.Color.White
                    txtCodiceAuthPreautorizzazione.BackColor = Drawing.Color.White

                    txtImporto.BackColor = Drawing.Color.Yellow
                    txtImporto.Focus()
                Case 2
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtNumerodiCartaPreautorizzazione.BackColor = Drawing.Color.White
                    txtScadenzaMesePreautorizzazione.BackColor = Drawing.Color.White
                    txtScadenzaAnnoPreautorizzazione.BackColor = Drawing.Color.White
                    txtPreautorizzazioneCarte.BackColor = Drawing.Color.White
                    txtCodiceAuthPreautorizzazione.BackColor = Drawing.Color.White

                    txtTitolareCartaPreautorizzazione.BackColor = Drawing.Color.Yellow
                    txtTitolareCartaPreautorizzazione.Focus()
                Case 3
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCartaPreautorizzazione.BackColor = Drawing.Color.White
                    txtScadenzaMesePreautorizzazione.BackColor = Drawing.Color.White
                    txtScadenzaAnnoPreautorizzazione.BackColor = Drawing.Color.White
                    txtPreautorizzazioneCarte.BackColor = Drawing.Color.White
                    txtCodiceAuthPreautorizzazione.BackColor = Drawing.Color.White

                    txtNumerodiCartaPreautorizzazione.BackColor = Drawing.Color.Yellow
                    txtNumerodiCartaPreautorizzazione.Focus()
                Case 4
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCartaPreautorizzazione.BackColor = Drawing.Color.White
                    txtNumerodiCartaPreautorizzazione.BackColor = Drawing.Color.White
                    txtScadenzaAnnoPreautorizzazione.BackColor = Drawing.Color.White
                    txtPreautorizzazioneCarte.BackColor = Drawing.Color.White
                    txtCodiceAuthPreautorizzazione.BackColor = Drawing.Color.White

                    txtScadenzaMesePreautorizzazione.BackColor = Drawing.Color.Yellow
                    txtScadenzaMesePreautorizzazione.Focus()
                Case 5
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCartaPreautorizzazione.BackColor = Drawing.Color.White
                    txtNumerodiCartaPreautorizzazione.BackColor = Drawing.Color.White
                    txtScadenzaMesePreautorizzazione.BackColor = Drawing.Color.White
                    txtPreautorizzazioneCarte.BackColor = Drawing.Color.White
                    txtCodiceAuthPreautorizzazione.BackColor = Drawing.Color.White

                    txtScadenzaAnnoPreautorizzazione.BackColor = Drawing.Color.Yellow
                    txtScadenzaAnnoPreautorizzazione.Focus()
                Case 6 'Mese non in formato valido > 12 or < 1
                    Messaggio = "il mese NON ha un formato valido."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCartaPreautorizzazione.BackColor = Drawing.Color.White
                    txtNumerodiCartaPreautorizzazione.BackColor = Drawing.Color.White
                    txtScadenzaAnnoPreautorizzazione.BackColor = Drawing.Color.White
                    txtPreautorizzazioneCarte.BackColor = Drawing.Color.White
                    txtCodiceAuthPreautorizzazione.BackColor = Drawing.Color.White

                    txtScadenzaMesePreautorizzazione.BackColor = Drawing.Color.Yellow
                    txtScadenzaMesePreautorizzazione.Focus()
                Case 7 'Campo NON numerico
                    Messaggio = "il mese e l'anno di scadenza risultato passati."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCartaPreautorizzazione.BackColor = Drawing.Color.White
                    txtNumerodiCartaPreautorizzazione.BackColor = Drawing.Color.White
                    txtScadenzaAnnoPreautorizzazione.BackColor = Drawing.Color.White
                    txtPreautorizzazioneCarte.BackColor = Drawing.Color.White
                    txtCodiceAuthPreautorizzazione.BackColor = Drawing.Color.White

                    txtScadenzaMesePreautorizzazione.BackColor = Drawing.Color.Yellow
                    txtScadenzaMesePreautorizzazione.Focus()
                Case 8
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCartaPreautorizzazione.BackColor = Drawing.Color.White
                    txtNumerodiCartaPreautorizzazione.BackColor = Drawing.Color.White
                    txtScadenzaMesePreautorizzazione.BackColor = Drawing.Color.White
                    txtScadenzaAnnoPreautorizzazione.BackColor = Drawing.Color.White
                    txtCodiceAuthPreautorizzazione.BackColor = Drawing.Color.White

                    txtPreautorizzazioneCarte.BackColor = Drawing.Color.Yellow
                    txtPreautorizzazioneCarte.Focus()
                Case 9
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCartaPreautorizzazione.BackColor = Drawing.Color.White
                    txtNumerodiCartaPreautorizzazione.BackColor = Drawing.Color.White
                    txtScadenzaMesePreautorizzazione.BackColor = Drawing.Color.White
                    txtScadenzaAnnoPreautorizzazione.BackColor = Drawing.Color.White
                    txtPreautorizzazioneCarte.BackColor = Drawing.Color.White

                    txtCodiceAuthPreautorizzazione.BackColor = Drawing.Color.Yellow
                    txtCodiceAuthPreautorizzazione.Focus()
            End Select
        End If
    End Sub

    Protected Sub Chiusura_Preautorizzazione_con_Carta_Credito()
        If sql_inj(txtNoteChiusuraPreautorizzazioneCarte.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        Dim Messaggio As String = ""
        Dim Num_di_Preautorizzazione As String = ""

        If controllo_campiChiusuraPreautorizzazione_ok() = 0 Then
            txtImporto.BackColor = Drawing.Color.White
            dropChiusuraPreautorizzazione.BackColor = Drawing.Color.White
            txtNoteChiusuraPreautorizzazioneCarte.BackColor = Drawing.Color.White           

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
            Dim Sql As String
            Dim Sql2 As String
            Dim SqlQuery As String

            Try
                Dim ArrayDataTime(1) As String
                Dim ArrayData(2) As String
                Dim DataOggi As String
                Dim idPosFunzioni_ares As String

                ArrayDataTime = Split(Now, " ")
                ArrayData = Split(ArrayDataTime(0), "/")
                DataOggi = ArrayData(2) & "-" & ArrayData(1) & "-" & ArrayData(0) & " " & ArrayDataTime(1)
                'modificato salvo 12.04.2023 x formato data
                DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString

                'Rilevare se sito di Sviluppo o Produzione per settare corettamente DataOggi
                'Response.Write(Request.ServerVariables("HTTP_REFERER") & "<br>")
                Dim ArrayPercorso(3) As String
                ArrayPercorso = Split(Request.ServerVariables("HTTP_REFERER"), "/")
                'Response.Write(ArrayPercorso(2) & "<br>")

                If ArrayPercorso(2) = "ares.sicilyrentcar.it" Then
                    DataOggi = CDate(DataOggi)
                    'modificato salvo 12.04.2023 x formato data
                    DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString
                End If

                'Prelevo dati sulla preautorizzazione scelta
                Try
                    Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc2.Open()                    

                    'Dim Cmd2 As New Data.SqlClient.SqlCommand("SELECT * FROM contratti,stazioni WITH(NOLOCK) WHERE contratti.id_stazione_uscita = stazioni.id and num_contratto='" & txtId.Text & "'", Dbc)
                    Dim Cmd2 As New Data.SqlClient.SqlCommand("SELECT * FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE Id_Ctr='" & dropChiusuraPreautorizzazione.SelectedValue & "'", Dbc2)
                    'Response.Write(Cmd.CommandText & "<br><br>")
                    'Response.End()

                    Dim Rs2 As Data.SqlClient.SqlDataReader
                    Rs2 = Cmd2.ExecuteReader()
                    If Rs2.HasRows Then
                        Do While Rs2.Read
                            Num_di_Preautorizzazione = Rs2("NR_PREAUT")
                        Loop
                    Else
                        Num_di_Preautorizzazione = ""
                    End If

                    Rs2.Close()
                    Dbc2.Close()
                    Rs2 = Nothing
                    Dbc2 = Nothing

                Catch ex As Exception
                    Libreria.genUserMsgBox(Me, ex.Message & " Seleziona Dati Preautorizzazione --- Errore contattare amministratore del sistema.")
                End Try

                idPosFunzioni_ares = "3"
                'Response.Write("Funzione " & GetIdStazioneDaCodice(lblStazioneCodice.Text) & " testo " & lblCodiceStazione.Text)
                'Response.End()

                'lblStazioneID.Text = funzioni_comuni.GetIdStazioneDaCodice(lblStazioneCodice.Text)
                Dim CassaSuCuiOperare As String

                'Permesso Stazione di pagamento
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "162") = 3 Then
                    CassaSuCuiOperare = funzioni_comuni.GetStazioneCodice(ddl_stazioni_pagamento2.SelectedValue)
                    lblStazioneID.Text = ddl_stazioni_pagamento2.SelectedValue
                Else
                    CassaSuCuiOperare = lblStazioneCodice.Text
                    lblStazioneID.Text = funzioni_comuni.GetIdStazioneDaCodice(lblStazioneCodice.Text)
                End If

                If lblModalita.Text = "PRENOTAZIONE" Then
                    Sql = "insert into PAGAMENTI_EXTRA " & _
                          "(Nr_Contratto,Data,ID_TIPPAG,id_pos_funzioni_ares,ID_STAZIONE,cassa,TIPSEGNO,id_operatore_ares,DATACRE,UTECRE,N_PREN_RIF,NR_BATCH,PER_IMPORTO,TERMINAL_ID,DATA_OPERAZIONE,NR_PREAUT,operazione_stornata,CONTABILIZZATO,TRANSATION_TYPE,CARD_TYPE,action_code,TentativiPos,pagamento_broker) " & _
                          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','-438610304','" & idPosFunzioni_ares & "','" & lblStazioneID.Text & "','" & CassaSuCuiOperare & "','1','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','000018','" & Replace(txtImporto.Text, ",", ".") & "','11282696','" & DataOggi & "','" & Num_di_Preautorizzazione & "','0','0','MAG','2','000','1','0')"
                Else
                    Sql = "insert into PAGAMENTI_EXTRA " & _
                          "(Nr_Contratto,Data,ID_TIPPAG,id_pos_funzioni_ares,ID_STAZIONE,cassa,TIPSEGNO,id_operatore_ares,DATACRE,UTECRE,N_CONTRATTO_RIF,NR_BATCH,PER_IMPORTO,TERMINAL_ID,DATA_OPERAZIONE,NR_PREAUT,operazione_stornata,CONTABILIZZATO,TRANSATION_TYPE,CARD_TYPE,action_code,TentativiPos,pagamento_broker) " & _
                          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','-438610304','" & idPosFunzioni_ares & "','" & lblStazioneID.Text & "','" & CassaSuCuiOperare & "','1','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','000018','" & Replace(txtImporto.Text, ",", ".") & "','11282696','" & DataOggi & "','" & Num_di_Preautorizzazione & "','0','0','MAG','2','000','1','0')"
                End If
                
                'Response.Write(Sql)
                'Response.End()

                Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
                Cmd.ExecuteNonQuery()

                'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                'Session("residenza_virtuale") = Cmd.ExecuteScalar

                SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                Cmd.CommandText = SqlQuery
                'Response.Write(Cmd.CommandText & "<br/>")
                'Response.End()
                Cmd.ExecuteNonQuery()

                Sql = "update PAGAMENTI_EXTRA set preaut_aperta = 0 where NR_PREAUT ='" & Num_di_Preautorizzazione & "'"
                Cmd.CommandText = Sql
                'Response.Write(Cmd.CommandText & "<br/>")
                'Response.End()
                Cmd.ExecuteNonQuery()

                SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                Cmd.CommandText = SqlQuery
                'Response.Write(Cmd.CommandText & "<br/>")
                'Response.End()
                Cmd.ExecuteNonQuery()

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                Messaggio = "Pagamento registrato correttamente."
                If Messaggio <> "" Then
                    genUserMsgBox(Page, Messaggio)
                    Session("PagamentoRegistrato") = True
                Else
                    Session("PagamentoRegistrato") = False
                End If
                If Session("PagamentoRegistrato") = True Then                    
                    Select Case lblModalita.Text
                        Case Is = "PRENOTAZIONE"
                            Response.Redirect("prenotazioni.aspx?nr=" & lblNumeroDocumento.Text)
                        Case Is = "CONTRATTO"
                            Response.Redirect("contratti.aspx?nr=" & lblNumeroDocumento.Text)
                    End Select
                End If


                'InviaMail(txtId.Text)
                'Response.Redirect("elenco_anagrafe_stazioni.aspx")
            Catch ex As Exception
                Response.Write(ex)
                Response.Write("<br><br>")
                Libreria.genUserMsgBox(Me, "Salvataggio Ins Chiusura Preaut Errore contattare amministratore del sistema.")
                Response.Write(Cmd.CommandText)
            End Try
        Else
            Select Case controllo_campiChiusuraPreautorizzazione_ok()
                Case 1
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If                    
                    dropChiusuraPreautorizzazione.BackColor = Drawing.Color.White
                    txtNoteChiusuraPreautorizzazioneCarte.BackColor = Drawing.Color.White

                    txtImporto.BackColor = Drawing.Color.Yellow
                    txtImporto.Focus()
                Case 2
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White                    
                    txtNoteChiusuraPreautorizzazioneCarte.BackColor = Drawing.Color.White

                    dropChiusuraPreautorizzazione.BackColor = Drawing.Color.Yellow
                    dropChiusuraPreautorizzazione.Focus()
                Case 3
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White                    
                    dropChiusuraPreautorizzazione.BackColor = Drawing.Color.White

                    txtNoteChiusuraPreautorizzazioneCarte.BackColor = Drawing.Color.Yellow
                    txtNoteChiusuraPreautorizzazioneCarte.Focus()                
            End Select
        End If

    End Sub

    Protected Sub Rimborso_con_Carta_Credito()
        If sql_inj(txtNoteRimborsoCarte.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        Dim Messaggio As String = ""

        If controllo_campiRimborsoCarte_ok() = 0 Then
            txtImporto.BackColor = Drawing.Color.White           
            txtNoteRimborsoCarte.BackColor = Drawing.Color.White

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
            Dim Sql As String
            Dim Sql2 As String
            Dim SqlQuery As String

            Try
                Dim ArrayDataTime(1) As String
                Dim ArrayData(2) As String
                Dim DataOggi As String
                Dim idPosFunzioni_ares As String

                ArrayDataTime = Split(Now, " ")
                ArrayData = Split(ArrayDataTime(0), "/")
                DataOggi = ArrayData(2) & "-" & ArrayData(1) & "-" & ArrayData(0) & " " & ArrayDataTime(1)
                'modificato salvo 12.04.2023 x formato data
                DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString

                'Rilevare se sito di Sviluppo o Produzione per settare corettamente DataOggi
                'Response.Write(Request.ServerVariables("HTTP_REFERER") & "<br>")
                Dim ArrayPercorso(3) As String
                ArrayPercorso = Split(Request.ServerVariables("HTTP_REFERER"), "/")
                'Response.Write(ArrayPercorso(2) & "<br>")

                If ArrayPercorso(2) = "ares.sicilyrentcar.it" Then
                    DataOggi = CDate(DataOggi)
                    'modificato salvo 12.04.2023 x formato data
                    DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString
                End If

                idPosFunzioni_ares = "9"
                'Response.Write("Funzione " & GetIdStazioneDaCodice(lblStazioneCodice.Text) & " testo " & lblCodiceStazione.Text)
                'Response.End()

                'lblStazioneID.Text = funzioni_comuni.GetIdStazioneDaCodice(lblStazioneCodice.Text)
                txtImporto.Text = "-" & txtImporto.Text

                Dim CassaSuCuiOperare As String
                'Permesso Stazione di pagamento
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "162") = 3 Then
                    CassaSuCuiOperare = funzioni_comuni.GetStazioneCodice(ddl_stazioni_pagamento2.SelectedValue)
                    lblStazioneID.Text = ddl_stazioni_pagamento2.SelectedValue
                Else
                    CassaSuCuiOperare = lblStazioneCodice.Text
                    lblStazioneID.Text = funzioni_comuni.GetIdStazioneDaCodice(lblStazioneCodice.Text)
                End If

                If lblModalita.Text = "PRENOTAZIONE" Then
                    Sql = "insert into PAGAMENTI_EXTRA " & _
                          "(Nr_Contratto,Data,ID_ModPag,ID_TIPPAG,id_pos_funzioni_ares,cassa,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_PREN_RIF,PER_IMPORTO,DATA_OPERAZIONE,preaut_aperta,operazione_stornata,pagamento_broker,note) " & _
                          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','11','2','" & idPosFunzioni_ares & "','" & CassaSuCuiOperare & "','" & lblStazioneID.Text & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','" & Replace(txtImporto.Text, ",", ".") & "','" & DataOggi & "','0','0','0','" & txtNoteRimborsoCarte.Text & "')"
                ElseIf lblModalita.Text = "CONTRATTO" Then
                    Sql = "insert into PAGAMENTI_EXTRA " & _
                          "(Nr_Contratto,Data,ID_ModPag,ID_TIPPAG,id_pos_funzioni_ares,cassa,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_CONTRATTO_RIF,PER_IMPORTO,DATA_OPERAZIONE,preaut_aperta,operazione_stornata,pagamento_broker,note) " & _
                          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','11','2','" & idPosFunzioni_ares & "','" & CassaSuCuiOperare & "','" & lblStazioneID.Text & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','" & Replace(txtImporto.Text, ",", ".") & "','" & DataOggi & "','0','0','0','" & txtNoteRimborsoCarte.Text & "')"
                ElseIf lblModalita.Text = "MULTE" Then
                    Sql = "insert into PAGAMENTI_EXTRA " & _
                          "(Nr_Contratto,Data,ID_ModPag,ID_TIPPAG,id_pos_funzioni_ares,cassa,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_MULTA_RIF,PER_IMPORTO,DATA_OPERAZIONE,preaut_aperta,operazione_stornata,pagamento_broker,note) " & _
                          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','11','2','" & idPosFunzioni_ares & "','" & CassaSuCuiOperare & "','" & lblStazioneID.Text & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','" & Replace(txtImporto.Text, ",", ".") & "','" & DataOggi & "','0','0','0','" & txtNoteRimborsoCarte.Text & "')"
                End If

                'Response.Write(Sql)
                'Response.End()

                Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
                Cmd.ExecuteNonQuery()

                'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                'Session("residenza_virtuale") = Cmd.ExecuteScalar

                SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                Cmd.CommandText = SqlQuery
                'Response.Write(Cmd.CommandText & "<br/>")
                'Response.End()
                Cmd.ExecuteNonQuery()

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                Messaggio = "Pagamento registrato correttamente."
                If Messaggio <> "" Then
                    genUserMsgBox(Page, Messaggio)
                    Session("PagamentoRegistrato") = True
                Else
                    Session("PagamentoRegistrato") = False
                End If
                If Session("PagamentoRegistrato") = True Then
                    Select Case lblModalita.Text
                        Case Is = "PRENOTAZIONE"
                            Response.Redirect("prenotazioni.aspx?nr=" & lblNumeroDocumento.Text)
                        Case Is = "CONTRATTO"
                            Response.Redirect("contratti.aspx?nr=" & lblNumeroDocumento.Text)
                    End Select
                End If


                'InviaMail(txtId.Text)
                'Response.Redirect("elenco_anagrafe_stazioni.aspx")
            Catch ex As Exception
                Response.Write(ex)
                Response.Write("<br><br>")
                Libreria.genUserMsgBox(Me, "Salvataggio Ins Rimborso carte Errore contattare amministratore del sistema.")
                Response.Write(Cmd.CommandText)
            End Try
        Else
            Select Case controllo_campiRimborsoCarte_ok()
                Case 1
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If                    
                    txtNoteRimborsoCarte.BackColor = Drawing.Color.White

                    txtImporto.BackColor = Drawing.Color.Yellow
                    txtImporto.Focus()                                
            End Select
        End If

    End Sub

    'Bancomat
    Protected Sub Acquisto_con_Bancomat()
        If sql_inj(txtNumerodiCartaPreautorizzazione.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If sql_inj(txtScadenzaMesePreautorizzazione.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If sql_inj(txtScadenzaAnnoPreautorizzazione.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        Dim Messaggio As String = ""

        If controllo_campiAcquistoBM_ok() = 0 Then
            txtImporto.BackColor = Drawing.Color.White
            txtTitolareCartaAcquistoBancomat.BackColor = Drawing.Color.White
            txtScadenzaMeseAcquistoBancomat.BackColor = Drawing.Color.White
            txtScadenzaAnnoAcquistoBancomat.BackColor = Drawing.Color.White

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
            Dim Sql As String
            Dim Sql2 As String
            Dim SqlQuery As String

            Try
                Dim ArrayDataTime(1) As String
                Dim ArrayData(2) As String
                Dim DataOggi As String
                Dim idPosFunzioni_ares As String

                ArrayDataTime = Split(Now, " ")
                ArrayData = Split(ArrayDataTime(0), "/")
                DataOggi = ArrayData(2) & "-" & ArrayData(1) & "-" & ArrayData(0) & " " & ArrayDataTime(1)
                'modificato salvo 12.04.2023 x formato data
                DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString

                'Rilevare se sito di Sviluppo o Produzione per settare corettamente DataOggi
                'Response.Write(Request.ServerVariables("HTTP_REFERER") & "<br>")
                Dim ArrayPercorso(3) As String
                ArrayPercorso = Split(Request.ServerVariables("HTTP_REFERER"), "/")
                'Response.Write(ArrayPercorso(2) & "<br>")

                If ArrayPercorso(2) = "ares.sicilyrentcar.it" Then
                    DataOggi = CDate(DataOggi)
                    'modificato salvo 12.04.2023 x formato data
                    DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString
                End If

                idPosFunzioni_ares = "4"
                'Response.Write("Funzione " & GetIdStazioneDaCodice(lblStazioneCodice.Text) & " testo " & lblCodiceStazione.Text)
                'Response.End()

                'lblStazioneID.Text = funzioni_comuni.GetIdStazioneDaCodice(lblStazioneCodice.Text)

                Dim CassaSuCuiOperare As String

                'If Request.Cookies("SicilyRentCar")("nome") = "Prestigiacomo Angela" Then
                '    CassaSuCuiOperare = ddl_stazioni_pagamento2.SelectedValue
                'Else
                '    CassaSuCuiOperare = lblStazioneCodice.Text
                'End If

                'Permesso Stazione di pagamento
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "162") = 3 Then
                    CassaSuCuiOperare = funzioni_comuni.GetStazioneCodice(ddl_stazioni_pagamento2.SelectedValue)
                    lblStazioneID.Text = ddl_stazioni_pagamento2.SelectedValue
                Else
                    CassaSuCuiOperare = lblStazioneCodice.Text
                    lblStazioneID.Text = funzioni_comuni.GetIdStazioneDaCodice(lblStazioneCodice.Text)
                End If

                If lblModalita.Text = "CONTRATTO" Then
                    Sql = "insert into PAGAMENTI_EXTRA " & _
                          "(Nr_Contratto,Data,Id_MOdPAg,ID_TIPPAG,id_pos_funzioni_ares,Importo,cassa,Intestatario,Scadenza,nr_aut,operazione_stornata,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_CONTRATTO_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker) " & _
                          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','9','1011098660','" & idPosFunzioni_ares & "','0.000'" & ",'" & CassaSuCuiOperare & "','" & Replace(txtTitolareCartaAcquistoBancomat.Text, "'", "''") & "','" & txtScadenzaMeseAcquistoBancomat.Text & "/" & txtScadenzaAnnoAcquistoBancomat.Text & "','0000023','0','" & lblStazioneID.Text & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','" & Replace(txtImporto.Text, ",", ".") & "','" & DataOggi & "','0')"
                ElseIf lblModalita.Text = "PRENOTAZIONE" Then
                    Sql = "insert into PAGAMENTI_EXTRA " & _
                          "(Nr_Contratto,Data,Id_MOdPAg,ID_TIPPAG,id_pos_funzioni_ares,Importo,cassa,Intestatario,Scadenza,nr_aut,operazione_stornata,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_PREN_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker) " & _
                          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','9','1011098660','" & idPosFunzioni_ares & "','0.000'" & ",'" & CassaSuCuiOperare & "','" & Replace(txtTitolareCartaAcquistoBancomat.Text, "'", "''") & "','" & txtScadenzaMeseAcquistoBancomat.Text & "/" & txtScadenzaAnnoAcquistoBancomat.Text & "','0000023','0','" & lblStazioneID.Text & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','" & Replace(txtImporto.Text, ",", ".") & "','" & DataOggi & "','0')"
                End If
                
                'Response.Write(Sql)
                'Response.End()

                Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
                Cmd.ExecuteNonQuery()

                'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                'Session("residenza_virtuale") = Cmd.ExecuteScalar

                SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                Cmd.CommandText = SqlQuery
                'Response.Write(Cmd.CommandText & "<br/>")
                'Response.End()
                Cmd.ExecuteNonQuery()

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                If lblModalita.Text = "PRENOTAZIONE" Then
                    'Imposto righe prepagate
                    ImpostoRighePrepagate(lblNumeroDocumento.Text)
                End If

                Messaggio = "Pagamento registrato correttamente."
                If Messaggio <> "" Then
                    genUserMsgBox(Page, Messaggio)
                    Session("PagamentoRegistrato") = True
                Else
                    Session("PagamentoRegistrato") = False
                End If
                If Session("PagamentoRegistrato") = True Then
                    Select Case lblModalita.Text
                        Case Is = "PRENOTAZIONE"
                            Response.Redirect("prenotazioni.aspx?nr=" & lblNumeroDocumento.Text)
                        Case Is = "CONTRATTO"
                            Response.Redirect("contratti.aspx?nr=" & lblNumeroDocumento.Text)
                    End Select
                End If


                'InviaMail(txtId.Text)
                'Response.Redirect("elenco_anagrafe_stazioni.aspx")
            Catch ex As Exception
                Response.Write(ex)
                Response.Write("<br><br>")
                Libreria.genUserMsgBox(Me, "Salvataggio Ins Acquisto Bancomat Errore contattare amministratore del sistema.")
                Response.Write(Cmd.CommandText)
            End Try


        Else
            Select Case controllo_campiAcquistoBM_ok()
                Case 1
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If                    
                    txtTitolareCartaAcquistoBancomat.BackColor = Drawing.Color.White
                    txtScadenzaMeseAcquistoBancomat.BackColor = Drawing.Color.White
                    txtScadenzaAnnoAcquistoBancomat.BackColor = Drawing.Color.White

                    txtImporto.BackColor = Drawing.Color.Yellow
                    txtImporto.Focus()
                Case 2
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White                    
                    txtScadenzaMeseAcquistoBancomat.BackColor = Drawing.Color.White
                    txtScadenzaAnnoAcquistoBancomat.BackColor = Drawing.Color.White

                    txtTitolareCartaAcquistoBancomat.BackColor = Drawing.Color.Yellow
                    txtTitolareCartaAcquistoBancomat.Focus()                
                Case 3
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCartaAcquistoBancomat.BackColor = Drawing.Color.White
                    txtScadenzaAnnoAcquistoBancomat.BackColor = Drawing.Color.White

                    txtScadenzaMeseAcquistoBancomat.BackColor = Drawing.Color.Yellow
                    txtScadenzaMeseAcquistoBancomat.Focus()
                Case 4
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCartaAcquistoBancomat.BackColor = Drawing.Color.White
                    txtScadenzaMeseAcquistoBancomat.BackColor = Drawing.Color.White


                    txtScadenzaAnnoAcquistoBancomat.BackColor = Drawing.Color.Yellow
                    txtScadenzaAnnoAcquistoBancomat.Focus()
                Case 5 'Mese non in formato valido > 12 or < 1
                    Messaggio = "il mese NON ha un formato valido."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCartaAcquistoBancomat.BackColor = Drawing.Color.White
                    txtScadenzaAnnoAcquistoBancomat.BackColor = Drawing.Color.White

                    txtScadenzaMeseAcquistoBancomat.BackColor = Drawing.Color.Yellow
                    txtScadenzaMeseAcquistoBancomat.Focus()
                Case 6 'Campo NON numerico
                    Messaggio = "il mese e l'anno di scadenza risultato passati."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                     txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCartaAcquistoBancomat.BackColor = Drawing.Color.White
                    txtScadenzaAnnoAcquistoBancomat.BackColor = Drawing.Color.White

                    txtScadenzaMeseAcquistoBancomat.BackColor = Drawing.Color.Yellow
                    txtScadenzaMeseAcquistoBancomat.Focus()
            End Select
        End If
    End Sub

    Protected Sub Deposito_con_Bancomat()
        If sql_inj(txtTitolareCartaAcquistoBancomat.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If sql_inj(txtScadenzaMeseAcquistoBancomat.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        If sql_inj(txtScadenzaAnnoAcquistoBancomat.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        Dim Messaggio As String = ""

        If controllo_campiDepositoBM_ok() = 0 Then
            txtImporto.BackColor = Drawing.Color.White
            txtTitolareCartaDepositoBancomat.BackColor = Drawing.Color.White
            txtScadenzaMeseDepositoBancomat.BackColor = Drawing.Color.White
            txtScadenzaAnnoDepositoBancomat.BackColor = Drawing.Color.White
            txtCodiceAuthDepositoBancomat.BackColor = Drawing.Color.White

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
            Dim Sql As String
            Dim Sql2 As String
            Dim SqlQuery As String

            Try
                Dim ArrayDataTime(1) As String
                Dim ArrayData(2) As String
                Dim DataOggi As String
                Dim idPosFunzioni_ares As String

                ArrayDataTime = Split(Now, " ")
                ArrayData = Split(ArrayDataTime(0), "/")
                DataOggi = ArrayData(2) & "-" & ArrayData(1) & "-" & ArrayData(0) & " " & ArrayDataTime(1)
                'modificato salvo 12.04.2023 x formato data
                DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString

                'Rilevare se sito di Sviluppo o Produzione per settare corettamente DataOggi
                'Response.Write(Request.ServerVariables("HTTP_REFERER") & "<br>")
                Dim ArrayPercorso(3) As String
                ArrayPercorso = Split(Request.ServerVariables("HTTP_REFERER"), "/")
                'Response.Write(ArrayPercorso(2) & "<br>")

                If ArrayPercorso(2) = "ares.sicilyrentcar.it" Then
                    DataOggi = CDate(DataOggi)
                    'modificato salvo 12.04.2023 x formato data
                    DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString
                End If

                idPosFunzioni_ares = "8"
                'Response.Write("Funzione " & GetIdStazioneDaCodice(lblStazioneCodice.Text) & " testo " & lblCodiceStazione.Text)
                'Response.End()

                'lblStazioneID.Text = funzioni_comuni.GetIdStazioneDaCodice(lblStazioneCodice.Text)
                Dim CassaSuCuiOperare As String

                'If Request.Cookies("SicilyRentCar")("nome") = "Prestigiacomo Angela" Then
                '    CassaSuCuiOperare = ddl_stazioni_pagamento2.SelectedValue
                'Else
                '    CassaSuCuiOperare = lblStazioneCodice.Text
                'End If

                'Permesso Stazione di pagamento
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "162") = 3 Then
                    CassaSuCuiOperare = funzioni_comuni.GetStazioneCodice(ddl_stazioni_pagamento2.SelectedValue)
                    lblStazioneID.Text = ddl_stazioni_pagamento2.SelectedValue
                Else
                    CassaSuCuiOperare = lblStazioneCodice.Text
                    lblStazioneID.Text = funzioni_comuni.GetIdStazioneDaCodice(lblStazioneCodice.Text)
                End If

                'Tony 30/01/2023
                'Sql = "insert into PAGAMENTI_EXTRA " & _
                '          "(Nr_Contratto,Data,ID_ModPag,ID_TIPPAG,id_pos_funzioni_ares,Importo,cassa,Intestatario,Scadenza,nr_aut,operazione_stornata,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_CONTRATTO_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker,codiceAuth,PAN) " & _
                '          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','9','-1886319629','" & idPosFunzioni_ares & "','0.000'" & ",'" & CassaSuCuiOperare & "','" & Replace(txtTitolareCartaDepositoBancomat.Text, "'", "''") & "','" & txtScadenzaMeseDepositoBancomat.Text & "/" & txtScadenzaAnnoDepositoBancomat.Text & "','0000023','0','" & lblStazioneID.Text & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','" & Replace(txtImporto.Text, ",", ".") & "','" & DataOggi & "','0','" & txtCodiceAuthDepositoBancomat.Text & "','" & txtCodicePANDepositoBancomat.Text & "')"
                'FINE Tony

                Sql = "insert into PAGAMENTI_EXTRA " & _
                          "(Nr_Contratto,Data,ID_ModPag,ID_TIPPAG,id_pos_funzioni_ares,Importo,cassa,Intestatario,Scadenza,nr_aut,operazione_stornata,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_CONTRATTO_RIF,PER_IMPORTO,DATA_OPERAZIONE,pagamento_broker,codiceAuth) " & _
                          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','9','-1886319629','" & idPosFunzioni_ares & "','0.000'" & ",'" & CassaSuCuiOperare & "','" & Replace(txtTitolareCartaDepositoBancomat.Text, "'", "''") & "','" & txtScadenzaMeseDepositoBancomat.Text & "/" & txtScadenzaAnnoDepositoBancomat.Text & "','0000023','0','" & lblStazioneID.Text & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','" & Replace(txtImporto.Text, ",", ".") & "','" & DataOggi & "','0','" & txtCodiceAuthDepositoBancomat.Text & "')"

                'Response.Write(Sql)
                'Response.End()

                Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
                Cmd.ExecuteNonQuery()

                'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                'Session("residenza_virtuale") = Cmd.ExecuteScalar

                SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                Cmd.CommandText = SqlQuery
                'Response.Write(Cmd.CommandText & "<br/>")
                'Response.End()
                Cmd.ExecuteNonQuery()

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                'Tony 30/01/2023
                'ControlloEsistenzaPAN(lblNumeroDocumento.Text)
                'FINE Tony


                Messaggio = "Pagamento registrato correttamente."
                If Messaggio <> "" Then
                    genUserMsgBox(Page, Messaggio)
                    Session("PagamentoRegistrato") = True
                Else
                    Session("PagamentoRegistrato") = False
                End If
                If Session("PagamentoRegistrato") = True Then
                    Select Case lblModalita.Text
                        Case Is = "PRENOTAZIONE"
                            Response.Redirect("prenotazioni.aspx?nr=" & lblNumeroDocumento.Text)
                        Case Is = "CONTRATTO"
                            Response.Redirect("contratti.aspx?nr=" & lblNumeroDocumento.Text)
                    End Select
                End If


                'InviaMail(txtId.Text)
                'Response.Redirect("elenco_anagrafe_stazioni.aspx")
            Catch ex As Exception
                Response.Write(ex)
                Response.Write("<br><br>")
                Libreria.genUserMsgBox(Me, "Salvataggio Ins Deposito Bancomat Errore contattare amministratore del sistema.")
                Response.Write(Cmd.CommandText)
            End Try


        Else
            Select Case controllo_campiDepositoBM_ok()
                Case 1
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtTitolareCartaDepositoBancomat.BackColor = Drawing.Color.White
                    txtScadenzaMeseDepositoBancomat.BackColor = Drawing.Color.White
                    txtScadenzaAnnoDepositoBancomat.BackColor = Drawing.Color.White
                    txtCodiceAuthDepositoBancomat.BackColor = Drawing.Color.White

                    txtImporto.BackColor = Drawing.Color.Yellow
                    txtImporto.Focus()
                Case 2
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White                    
                    txtScadenzaMeseDepositoBancomat.BackColor = Drawing.Color.White
                    txtScadenzaAnnoDepositoBancomat.BackColor = Drawing.Color.White
                    txtCodiceAuthDepositoBancomat.BackColor = Drawing.Color.White

                    txtTitolareCartaDepositoBancomat.BackColor = Drawing.Color.Yellow
                    txtTitolareCartaDepositoBancomat.Focus()
                Case 3
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCartaDepositoBancomat.BackColor = Drawing.Color.White
                    txtScadenzaAnnoDepositoBancomat.BackColor = Drawing.Color.White
                    txtCodiceAuthDepositoBancomat.BackColor = Drawing.Color.White

                    txtScadenzaMeseDepositoBancomat.BackColor = Drawing.Color.Yellow
                    txtScadenzaMeseDepositoBancomat.Focus()
                Case 4
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCartaDepositoBancomat.BackColor = Drawing.Color.White
                    txtScadenzaMeseDepositoBancomat.BackColor = Drawing.Color.White
                    txtCodiceAuthDepositoBancomat.BackColor = Drawing.Color.White


                    txtScadenzaAnnoDepositoBancomat.BackColor = Drawing.Color.Yellow
                    txtScadenzaAnnoDepositoBancomat.Focus()
                Case 5 'Mese non in formato valido > 12 or < 1
                    Messaggio = "il mese NON ha un formato valido."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCartaDepositoBancomat.BackColor = Drawing.Color.White
                    txtScadenzaAnnoDepositoBancomat.BackColor = Drawing.Color.White
                    txtCodiceAuthDepositoBancomat.BackColor = Drawing.Color.White

                    txtScadenzaMeseDepositoBancomat.BackColor = Drawing.Color.Yellow
                    txtScadenzaMeseDepositoBancomat.Focus()
                Case 6 'Campo NON numerico
                    Messaggio = "il mese e l'anno di scadenza risultato passati."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCartaDepositoBancomat.BackColor = Drawing.Color.White
                    txtScadenzaAnnoDepositoBancomat.BackColor = Drawing.Color.White
                    txtCodiceAuthDepositoBancomat.BackColor = Drawing.Color.White

                    txtScadenzaMeseDepositoBancomat.BackColor = Drawing.Color.Yellow
                    txtScadenzaMeseDepositoBancomat.Focus()
                Case 7
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.White
                    txtTitolareCartaDepositoBancomat.BackColor = Drawing.Color.White
                    txtScadenzaMeseDepositoBancomat.BackColor = Drawing.Color.White
                    txtScadenzaAnnoDepositoBancomat.BackColor = Drawing.Color.White


                    txtCodiceAuthDepositoBancomat.BackColor = Drawing.Color.Yellow
                    txtCodiceAuthDepositoBancomat.Focus()
            End Select
        End If
    End Sub

    Protected Sub Rimborso_Deposito_con_Bancomat()
        If sql_inj(txtNoteRimborsoDepositoBancomat.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        Dim Messaggio As String = ""

        If controllo_campiRimborsoDepositoBM_ok() = 0 Then
            txtImporto.BackColor = Drawing.Color.White            

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
            Dim Sql As String
            Dim Sql2 As String
            Dim SqlQuery As String

            Try
                Dim ArrayDataTime(1) As String
                Dim ArrayData(2) As String
                Dim DataOggi As String
                Dim idPosFunzioni_ares As String

                ArrayDataTime = Split(Now, " ")
                ArrayData = Split(ArrayDataTime(0), "/")
                DataOggi = ArrayData(2) & "-" & ArrayData(1) & "-" & ArrayData(0) & " " & ArrayDataTime(1)
                'modificato salvo 12.04.2023 x formato data
                DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString

                'Rilevare se sito di Sviluppo o Produzione per settare corettamente DataOggi
                'Response.Write(Request.ServerVariables("HTTP_REFERER") & "<br>")
                Dim ArrayPercorso(3) As String
                ArrayPercorso = Split(Request.ServerVariables("HTTP_REFERER"), "/")
                'Response.Write(ArrayPercorso(2) & "<br>")

                If ArrayPercorso(2) = "ares.sicilyrentcar.it" Then
                    DataOggi = CDate(DataOggi)
                    'modificato salvo 12.04.2023 x formato data
                    DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString
                End If

                idPosFunzioni_ares = "15"
                'Response.Write("Funzione " & GetIdStazioneDaCodice(lblStazioneCodice.Text) & " testo " & lblCodiceStazione.Text)
                'Response.End()

                'lblStazioneID.Text = funzioni_comuni.GetIdStazioneDaCodice(lblStazioneCodice.Text)               
                txtImporto.Text = "-" & txtImporto.Text

                Dim CassaSuCuiOperare As String

                'If Request.Cookies("SicilyRentCar")("nome") = "Prestigiacomo Angela" Then
                '    CassaSuCuiOperare = ddl_stazioni_pagamento2.SelectedValue
                'Else
                '    CassaSuCuiOperare = lblStazioneCodice.Text
                'End If

                'Permesso Stazione di pagamento
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "162") = 3 Then
                    CassaSuCuiOperare = funzioni_comuni.GetStazioneCodice(ddl_stazioni_pagamento2.SelectedValue)
                    lblStazioneID.Text = ddl_stazioni_pagamento2.SelectedValue
                Else
                    CassaSuCuiOperare = lblStazioneCodice.Text
                    lblStazioneID.Text = funzioni_comuni.GetIdStazioneDaCodice(lblStazioneCodice.Text)
                End If

                If lblModalita.Text = "PRENOTAZIONE" Then
                    Sql = "insert into PAGAMENTI_EXTRA " & _
                          "(Nr_Contratto,Data,ID_ModPag,ID_TIPPAG,id_pos_funzioni_ares,cassa,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_PREN_RIF,PER_IMPORTO,DATA_OPERAZIONE,preaut_aperta,operazione_stornata,pagamento_broker) " & _
                          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','11','-714677539','" & idPosFunzioni_ares & "','" & CassaSuCuiOperare & "','" & lblStazioneID.Text & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','" & Replace(txtImporto.Text, ",", ".") & "','" & DataOggi & "','0','0','0')"
                Else
                    Sql = "insert into PAGAMENTI_EXTRA " & _
                          "(Nr_Contratto,Data,ID_ModPag,ID_TIPPAG,id_pos_funzioni_ares,cassa,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_CONTRATTO_RIF,PER_IMPORTO,DATA_OPERAZIONE,preaut_aperta,operazione_stornata,pagamento_broker) " & _
                          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','11','-714677539','" & idPosFunzioni_ares & "','" & CassaSuCuiOperare & "','" & lblStazioneID.Text & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','" & Replace(txtImporto.Text, ",", ".") & "','" & DataOggi & "','0','0','0')"
                End If

                
                'Response.Write(Sql)
                'Response.End()

                Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
                Cmd.ExecuteNonQuery()

                'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                'Session("residenza_virtuale") = Cmd.ExecuteScalar

                SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                Cmd.CommandText = SqlQuery
                'Response.Write(Cmd.CommandText & "<br/>")
                'Response.End()
                Cmd.ExecuteNonQuery()

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                Messaggio = "Pagamento registrato correttamente."
                If Messaggio <> "" Then
                    genUserMsgBox(Page, Messaggio)
                    Session("PagamentoRegistrato") = True
                Else
                    Session("PagamentoRegistrato") = False
                End If
                If Session("PagamentoRegistrato") = True Then
                    Select Case lblModalita.Text
                        Case Is = "PRENOTAZIONE"
                            Response.Redirect("prenotazioni.aspx?nr=" & lblNumeroDocumento.Text)
                        Case Is = "CONTRATTO"
                            Response.Redirect("contratti.aspx?nr=" & lblNumeroDocumento.Text)
                    End Select
                End If


                'InviaMail(txtId.Text)
                'Response.Redirect("elenco_anagrafe_stazioni.aspx")
            Catch ex As Exception
                Response.Write(ex)
                Response.Write("<br><br>")
                Libreria.genUserMsgBox(Me, "Salvataggio Ins Rimborso Deposito Bancomat Errore contattare amministratore del sistema.")
                Response.Write(Cmd.CommandText)
            End Try


        Else
            Select Case controllo_campiDepositoBM_ok()
                Case 1
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If                   
                    txtImporto.BackColor = Drawing.Color.Yellow
                    txtImporto.Focus()                
            End Select
        End If
    End Sub

    Protected Sub Rimborso_con_Bancomat()
        If sql_inj(txtNoteRimborsoDepositoBancomat.Text) Then
            Session("DatiSqlIng") = "Pagamenti - Documento: " & lblNumeroDocumento.Text
            Response.Redirect("teantaivosqlinj.aspx")
        End If

        Dim Messaggio As String = ""

        If controllo_campiRimborsoDepositoBM_ok() = 0 Then
            txtImporto.BackColor = Drawing.Color.White

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
            Dim Sql As String
            Dim Sql2 As String
            Dim SqlQuery As String

            Try
                Dim ArrayDataTime(1) As String
                Dim ArrayData(2) As String
                Dim DataOggi As String
                Dim idPosFunzioni_ares As String

                ArrayDataTime = Split(Now, " ")
                ArrayData = Split(ArrayDataTime(0), "/")
                DataOggi = ArrayData(2) & "-" & ArrayData(1) & "-" & ArrayData(0) & " " & ArrayDataTime(1)
                'modificato salvo 12.04.2023 x formato data
                DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString

                'Rilevare se sito di Sviluppo o Produzione per settare corettamente DataOggi
                'Response.Write(Request.ServerVariables("HTTP_REFERER") & "<br>")
                Dim ArrayPercorso(3) As String
                ArrayPercorso = Split(Request.ServerVariables("HTTP_REFERER"), "/")
                'Response.Write(ArrayPercorso(2) & "<br>")

                If ArrayPercorso(2) = "ares.sicilyrentcar.it" Then
                    DataOggi = CDate(DataOggi)
                    'modificato salvo 12.04.2023 x formato data
                    DataOggi = Now.Year.ToString & "-" & Now.Month.ToString & "-" & Now.Day.ToString & " " & Now.Hour & ":" & Now.Minute.ToString & ":" & Now.Second.ToString
                End If

                idPosFunzioni_ares = "9"
                'Response.Write("Funzione " & GetIdStazioneDaCodice(lblStazioneCodice.Text) & " testo " & lblCodiceStazione.Text)
                'Response.End()

                'lblStazioneID.Text = funzioni_comuni.GetIdStazioneDaCodice(lblStazioneCodice.Text)
                txtImporto.Text = "-" & txtImporto.Text

                Dim CassaSuCuiOperare As String

                'If Request.Cookies("SicilyRentCar")("nome") = "Prestigiacomo Angela" Then
                '    CassaSuCuiOperare = ddl_stazioni_pagamento2.SelectedValue
                'Else
                '    CassaSuCuiOperare = lblStazioneCodice.Text
                'End If

                'Permesso Stazione di pagamento
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "162") = 3 Then
                    CassaSuCuiOperare = funzioni_comuni.GetStazioneCodice(ddl_stazioni_pagamento2.SelectedValue)
                    lblStazioneID.Text = ddl_stazioni_pagamento2.SelectedValue
                Else
                    CassaSuCuiOperare = lblStazioneCodice.Text
                    lblStazioneID.Text = funzioni_comuni.GetIdStazioneDaCodice(lblStazioneCodice.Text)
                End If

                If lblModalita.Text = "PRENOTAZIONE" Then
                    Sql = "insert into PAGAMENTI_EXTRA " & _
                          "(Nr_Contratto,Data,ID_ModPag,ID_TIPPAG,id_pos_funzioni_ares,cassa,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_PREN_RIF,PER_IMPORTO,DATA_OPERAZIONE,preaut_aperta,operazione_stornata,pagamento_broker) " & _
                          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','11','2','" & idPosFunzioni_ares & "','" & CassaSuCuiOperare & "','" & lblStazioneID.Text & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','" & Replace(txtImporto.Text, ",", ".") & "','" & DataOggi & "','0','0','0')"
                Else
                    Sql = "insert into PAGAMENTI_EXTRA " & _
                          "(Nr_Contratto,Data,ID_ModPag,ID_TIPPAG,id_pos_funzioni_ares,cassa,ID_STAZIONE,id_operatore_ares,DATACRE,UTECRE,N_CONTRATTO_RIF,PER_IMPORTO,DATA_OPERAZIONE,preaut_aperta,operazione_stornata,pagamento_broker) " & _
                          "values('" & GetMaxId(lblStazioneID.Text) & "','" & DataOggi & "','11','2','" & idPosFunzioni_ares & "','" & CassaSuCuiOperare & "','" & lblStazioneID.Text & "','" & CInt(Request.Cookies("SicilyRentCar")("IdUtente")) & "','" & DataOggi & "','" & lb_operatore.Text & "','" & lblNumeroDocumento.Text & "','" & Replace(txtImporto.Text, ",", ".") & "','" & DataOggi & "','0','0','0')"
                End If
                
                'Response.Write(Sql)
                'Response.End()

                Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
                Cmd.ExecuteNonQuery()

                'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                'Session("residenza_virtuale") = Cmd.ExecuteScalar

                SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
                Cmd.CommandText = SqlQuery
                'Response.Write(Cmd.CommandText & "<br/>")
                'Response.End()
                Cmd.ExecuteNonQuery()

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                Messaggio = "Pagamento registrato correttamente."
                If Messaggio <> "" Then
                    genUserMsgBox(Page, Messaggio)
                    Session("PagamentoRegistrato") = True
                Else
                    Session("PagamentoRegistrato") = False
                End If
                If Session("PagamentoRegistrato") = True Then
                    Select Case lblModalita.Text
                        Case Is = "PRENOTAZIONE"
                            Response.Redirect("prenotazioni.aspx?nr=" & lblNumeroDocumento.Text)
                        Case Is = "CONTRATTO"
                            Response.Redirect("contratti.aspx?nr=" & lblNumeroDocumento.Text)
                    End Select
                End If


                'InviaMail(txtId.Text)
                'Response.Redirect("elenco_anagrafe_stazioni.aspx")
            Catch ex As Exception
                Response.Write(ex)
                Response.Write("<br><br>")
                Libreria.genUserMsgBox(Me, "Salvataggio Ins Rimborso Deposito Bancomat Errore contattare amministratore del sistema.")
                Response.Write(Cmd.CommandText)
            End Try


        Else
            Select Case controllo_campiDepositoBM_ok()
                Case 1
                    Messaggio = "Campo Obbligatorio."
                    If Messaggio <> "" Then
                        Libreria.genUserMsgBox(Page, Messaggio)
                    End If
                    txtImporto.BackColor = Drawing.Color.Yellow
                    txtImporto.Focus()
            End Select
        End If
    End Sub

    Protected Sub dropChiusuraPreautorizzazione_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dropChiusuraPreautorizzazione.SelectedIndexChanged
        'Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        'Dbc2.Open()
        ''Dim Security As PAGAMENTI_EXTRA = Nothing

        'Try
        '    Dim sqlStr As String = "SELECT  PER_IMPORTO FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE ID_CTR='" & dropChiusuraPreautorizzazione.SelectedValue & "'"
        '    'Response.Write(sqlStr)
        '    'Response.End()

        '    Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
        '    Dim Rs1 As Data.SqlClient.SqlDataReader
        '    Rs1 = Cmd1.ExecuteReader()

        '    If Rs1.HasRows Then
        '        Rs1.Read()
        '        txtImporto.Text = Rs1(0)
        '        txtImporto.Enabled = False
        '    End If

        '    Rs1.Close()
        '    Cmd1.Dispose()
        '    Dbc2.Close()
        '    Rs1 = Nothing
        '    Cmd1 = Nothing
        '    Dbc2 = Nothing

        'Catch ex As Exception
        '    Libreria.genUserMsgBox(Me, "Select Valore per drop Chiusura Preautirizzazione Errore contattare amministratore del sistema.")
        'End Try
    End Sub

    Protected Sub ImpostoRighePrepagate(ByVal NumDocumento As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Try
            Dim sqlStr As String = "SELECT Nr_Pren, NUMPREN, num_calcolo, status, giorni, sconto_applicato FROM prenotazioni WITH(NOLOCK)  WHERE (NUMPREN = '" & NumDocumento & "') AND (attiva = 1)"
            'Response.Write(sqlStr & "<br>")
            'Response.End()

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows Then
                Rs.Read()

                Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc2.Open()

                Try
                    Dim sqlStr2 As String = "SELECT * FROM  prenotazioni_costi with(NOLOCK) WHERE (id_documento = '" & Rs("Nr_Pren") & "') AND (selezionato = 1) AND (valore_costo <> 0)"
                    'Response.Write(sqlStr2 & "<br>")
                    'Response.End()

                    Dim Cmd2 As New Data.SqlClient.SqlCommand(sqlStr2, Dbc2)
                    Dim Rs2 As Data.SqlClient.SqlDataReader
                    Rs2 = Cmd2.ExecuteReader()

                    If Rs2.HasRows Then
                        Do While Rs2.Read
                            Dim Dbc3 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                            Dbc3.Open()

                            Try
                                Dim Cmd3 As New Data.SqlClient.SqlCommand("", Dbc3)

                                Dim SqlQuery As String
                                Dim Sql3 As String = "update prenotazioni_costi set prepagato=1, imponibile_scontato_prepagato = imponibile_scontato, iva_imponibile_scontato_prepagato = iva_imponibile_scontato where id=" & Rs2("id")
                                'Response.Write(Sql3 & "<br>")
                                'Response.End()

                                Cmd3 = New Data.SqlClient.SqlCommand(Sql3, Dbc3)
                                Cmd3.ExecuteNonQuery()

                                'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                                'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                                'Session("residenza_virtuale") = Cmd.ExecuteScalar

                                SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql3, "'", "''") & "')"
                                Cmd3 = New Data.SqlClient.SqlCommand(SqlQuery, Dbc3)
                                Cmd3.ExecuteNonQuery()

                                'Valorizzo Sconto in caso di applicazione                                
                                If Not IsDBNull(Rs("sconto_applicato")) Then                                    
                                    Dim DbcSconto As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                                    DbcSconto.Open()
                                    Try
                                        Dim CmdSconto As New Data.SqlClient.SqlCommand("", DbcSconto)

                                        Dim SqlQuerySconto As String
                                        Dim SqlSconto As String

                                        If Not IsDBNull(Rs2("sconto_su_imponibile_prepagato")) Then                                                                                        
                                            SqlSconto = "update prenotazioni_costi set imponibile='" & Replace(Rs2("imponibile_scontato"), ",", ".") & "', iva_imponibile='" & Replace(Rs2("iva_imponibile_scontato"), ",", ".") & "', imponibile_scontato_prepagato='" & Replace(Rs2("imponibile_scontato_prepagato"), ",", ".") & "', iva_imponibile_scontato_prepagato ='" & Replace(Rs2("iva_imponibile_scontato_prepagato"), ",", ".") & "', sconto_su_imponibile_prepagato='" & Replace(Rs2("sconto_su_imponibile_prepagato"), ",", ".") & "' where  (id_documento = '" & Rs("Nr_Pren") & "')  and nome_costo = 'Valore Tariffa'"
                                        End If

                                        'Response.Write(SqlSconto & "<br>")
                                        'Response.End()

                                        CmdSconto = New Data.SqlClient.SqlCommand(SqlSconto, DbcSconto)
                                        CmdSconto.ExecuteNonQuery()

                                        'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                                        'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                                        'Session("residenza_virtuale") = Cmd.ExecuteScalar

                                        SqlQuerySconto = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(SqlSconto, "'", "''") & "')"
                                        CmdSconto = New Data.SqlClient.SqlCommand(SqlQuerySconto, DbcSconto)
                                        CmdSconto.ExecuteNonQuery()


                                        CmdSconto.Dispose()
                                        DbcSconto.Close()
                                        CmdSconto = Nothing
                                        DbcSconto = Nothing
                                    Catch ex As Exception
                                        Libreria.genUserMsgBox(Me, "UPDATE Su Sconto Errore contattare amministratore del sistema.")
                                    End Try                                
                                End If


                                Cmd3.Dispose()
                                Dbc3.Close()
                                Cmd3 = Nothing
                                Dbc3 = Nothing

                            Catch ex As Exception
                                Libreria.genUserMsgBox(Me, "Insert Su Prenotazione Errore contattare amministratore del sistema.")
                            End Try
                            'Response.End()
                        Loop
                    End If


                    Rs2.Close()
                    Cmd2.Dispose()
                    Dbc2.Close()
                    Rs2 = Nothing
                    Cmd2 = Nothing
                    Dbc2 = Nothing

                    'Response.End()

                    'Settaggio campi prepagati su prenotazioni costi Riga TOTALE
                    Dim Dbc5 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc5.Open()

                    Try
                        Dim Cmd5 As New Data.SqlClient.SqlCommand("", Dbc5)

                        Dim SqlQuery As String
                        Dim Sql5 As String = "update prenotazioni_costi set prepagato=1, imponibile_scontato_prepagato = imponibile, iva_imponibile_scontato_prepagato = iva_imponibile WHERE (id_documento = '" & Rs("Nr_Pren") & "') AND (nome_costo = 'TOTALE')"
                        'Response.Write(Sql5 & "<br>")
                        'Response.End()

                        Cmd5 = New Data.SqlClient.SqlCommand(Sql5, Dbc5)
                        Cmd5.ExecuteNonQuery()

                        'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                        'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                        'Session("residenza_virtuale") = Cmd.ExecuteScalar

                        SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql5, "'", "''") & "')"
                        Cmd5 = New Data.SqlClient.SqlCommand(SqlQuery, Dbc5)
                        Cmd5.ExecuteNonQuery()


                        Cmd5.Dispose()
                        Dbc5.Close()
                        Cmd5 = Nothing
                        Dbc5 = Nothing

                    Catch ex As Exception
                        Libreria.genUserMsgBox(Me, "Update Su Prenotazione Voce TOTALE Errore contattare amministratore del sistema.")
                    End Try

                    'Selezione Voce TOTALE campi prepagati su prenotazioni
                    Dim Dbc6 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc6.Open()

                    Try
                        Dim sqlStr6 As String = "SELECT * FROM  prenotazioni_costi with(NOLOCK) WHERE (id_documento = '" & Rs("Nr_Pren") & "')   AND (nome_costo = 'TOTALE')"
                        'Response.Write(sqlStr6 & "<br>")
                        'Response.End()

                        Dim Cmd6 As New Data.SqlClient.SqlCommand(sqlStr6, Dbc6)
                        Dim Rs6 As Data.SqlClient.SqlDataReader
                        Rs6 = Cmd6.ExecuteReader()

                        If Rs6.HasRows Then                            
                            Do While Rs6.Read
                                'Settaggio campi prepagati su prenotazioni
                                Dim Dbc4 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                                Dbc4.Open()

                                Try
                                    Dim Cmd4 As New Data.SqlClient.SqlCommand("", Dbc4)

                                    Dim SqlQuery As String
                                    Dim Sql4 As String = "update prenotazioni set  prepagata=1, importo_prepagato='" & Replace(Rs6("valore_costo"), ",", ".") & "', giorni_prepagati ='" & Rs("giorni") & "'  WHERE (NUMPREN = '" & NumDocumento & "') AND (attiva = 1)"
                                    'Response.Write(Sql4 & "<br>")
                                    'Response.End()

                                    Cmd4 = New Data.SqlClient.SqlCommand(Sql4, Dbc4)
                                    Cmd4.ExecuteNonQuery()

                                    'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
                                    'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
                                    'Session("residenza_virtuale") = Cmd.ExecuteScalar

                                    SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql4, "'", "''") & "')"
                                    Cmd4 = New Data.SqlClient.SqlCommand(SqlQuery, Dbc4)
                                    Cmd4.ExecuteNonQuery()

                                    Cmd4.Dispose()
                                    Dbc4.Close()
                                    Cmd4 = Nothing
                                    Dbc4 = Nothing

                                Catch ex As Exception
                                    Libreria.genUserMsgBox(Me, "Update Su Prenotazione Errore contattare amministratore del sistema.")
                                End Try
                            Loop
                        End If

                        Rs6.Close()
                        Cmd6.Dispose()
                        Dbc6.Close()
                        Rs6 = Nothing
                        Cmd6 = Nothing
                        Dbc6 = Nothing
                    Catch ex As Exception
                        Libreria.genUserMsgBox(Me, "Select Su Prenotazione_costi Voce Totale Errore contattare amministratore del sistema.")
                    End Try

                Catch ex As Exception
                    Libreria.genUserMsgBox(Me, "Select Su Prenotazione2 Errore contattare amministratore del sistema.")
                End Try
            End If

            Rs.Close()
            Cmd.Dispose()
            Dbc.Close()
            Rs = Nothing
            Cmd = Nothing
            Dbc = Nothing

        Catch ex As Exception
            Libreria.genUserMsgBox(Me, "Select Su Prenotazione Errore contattare amministratore del sistema.")
        End Try

        'Response.End()
    End Sub

    Protected Sub ValorizzaImportoDaPrenotazione()        
        'Prelevo Importo da valorizzare
        Dim ImportoAux As String = 0

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM prenotazioni with(NOLOCK) WHERE (NUMPREN = '" & lblNumeroDocumento.Text & "')", Dbc)
            'Response.Write(Cmd.CommandText & "<br><br>")
            'Response.End()
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            If Rs.HasRows Then
                Do While Rs.Read
                    Try
                        Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        Dbc2.Open()

                        Dim Cmd2 As New Data.SqlClient.SqlCommand("select * from prenotazioni_costi where id_documento ='" & Rs("NR_PREN") & "' and  prenotazioni_costi.nome_costo = 'TOTALE'", Dbc2)
                        'Response.Write(Cmd.CommandText & "<br><br>")
                        'Response.End()
                        Dim Rs2 As Data.SqlClient.SqlDataReader
                        Rs2 = Cmd2.ExecuteReader()
                        If Rs2.HasRows Then
                            Do While Rs2.Read                                
                                ImportoAux = Format(Rs2("valore_costo"), "0.00")
                            Loop
                        Else

                        End If

                        Rs2.Close()
                        Dbc2.Close()
                        Rs2 = Nothing
                        Dbc2 = Nothing

                    Catch ex As Exception
                        HttpContext.Current.Response.Write("error Valorizzazione Importo2 : <br/>" & ex.Message & "<br/>")
                    End Try
                Loop
            Else

            End If

            Rs.Close()
            Dbc.Close()
            Rs = Nothing
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("error Valorizzazione Importo : <br/>" & ex.Message & "<br/>")
        End Try

        Dim ImportoIncassato As String = 0

        Try
            Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc2.Open()

            Dim Cmd2 As New Data.SqlClient.SqlCommand("SELECT sum(PER_IMPORTO) as TotaleIncassato FROM  PAGAMENTI_EXTRA WHERE (N_PREN_RIF = '" & lblNumeroDocumento.Text & "')", Dbc2)
            'Dim Cmd2 As New Data.SqlClient.SqlCommand("SELECT sum(PER_IMPORTO) as TotaleIncassato FROM  PAGAMENTI_EXTRA WHERE (N_PREN_RIF = '12000069')", Dbc2)

            'Response.Write(Cmd2.CommandText & "<br><br>")
            'Response.End()
            Dim Rs2 As Data.SqlClient.SqlDataReader
            Rs2 = Cmd2.ExecuteReader()
            If Rs2.HasRows Then
                Do While Rs2.Read
                    If IsDBNull(Rs2("TotaleIncassato")) Then
                        ImportoIncassato = "0.00"
                    Else
                        ImportoIncassato = Format(Rs2("TotaleIncassato"), "0.00")
                    End If
                Loop
            Else

            End If

            Rs2.Close()
            Dbc2.Close()
            Rs2 = Nothing
            Dbc2 = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("error Valorizzazione Importo2 : <br/>" & ex.Message & "<br/>")
        End Try

        'Response.Write("AUX" & CDbl(ImportoAux))
        'Response.Write("Incassato" & CDbl(ImportoIncassato))
        txtImporto.Text = Format(CDbl(ImportoAux) - CDbl(ImportoIncassato), "0.00")
        'Response.Write("Da Incassare" & tx_importo.Text)
        'Response.End()
    End Sub

    Protected Sub ValorizzaImportoDaPrenotazione2()
        'Prelevo Importo da valorizzare
        Dim ImportoAux As String = 0

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM prenotazioni with(NOLOCK) WHERE (NUMPREN = '" & lb_documento_riferimento.Text & "')", Dbc)
            'Response.Write(Cmd.CommandText & "<br><br>")
            'Response.End()
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            If Rs.HasRows Then
                Do While Rs.Read
                    Try
                        Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        Dbc2.Open()

                        Dim Cmd2 As New Data.SqlClient.SqlCommand("select * from prenotazioni_costi where id_documento ='" & Rs("NR_PREN") & "' and  prenotazioni_costi.nome_costo = 'TOTALE'", Dbc2)
                        'Response.Write(Cmd.CommandText & "<br><br>")
                        'Response.End()
                        Dim Rs2 As Data.SqlClient.SqlDataReader
                        Rs2 = Cmd2.ExecuteReader()
                        If Rs2.HasRows Then
                            Do While Rs2.Read
                                ImportoAux = Format(Rs2("valore_costo"), "0.00")
                            Loop
                        Else

                        End If

                        Rs2.Close()
                        Dbc2.Close()
                        Rs2 = Nothing
                        Dbc2 = Nothing

                    Catch ex As Exception
                        HttpContext.Current.Response.Write("error Valorizzazione Importo2 : <br/>" & ex.Message & "<br/>")
                    End Try
                Loop
            Else

            End If

            Rs.Close()
            Dbc.Close()
            Rs = Nothing
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("error Valorizzazione Importo : <br/>" & ex.Message & "<br/>")
        End Try

        Dim ImportoIncassato As String = 0

        Try
            Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc2.Open()

            Dim Cmd2 As New Data.SqlClient.SqlCommand("SELECT sum(PER_IMPORTO) as TotaleIncassato FROM  PAGAMENTI_EXTRA WHERE (N_PREN_RIF = '" & lb_documento_riferimento.Text & "')", Dbc2)
            'Dim Cmd2 As New Data.SqlClient.SqlCommand("SELECT sum(PER_IMPORTO) as TotaleIncassato FROM  PAGAMENTI_EXTRA WHERE (N_PREN_RIF = '12000069')", Dbc2)

            'Response.Write(Cmd2.CommandText & "<br><br>")
            'Response.End()
            Dim Rs2 As Data.SqlClient.SqlDataReader
            Rs2 = Cmd2.ExecuteReader()
            If Rs2.HasRows Then
                Do While Rs2.Read
                    If IsDBNull(Rs2("TotaleIncassato")) Then
                        ImportoIncassato = "0.00"
                    Else
                        ImportoIncassato = Format(Rs2("TotaleIncassato"), "0.00")
                    End If
                Loop
            Else

            End If

            Rs2.Close()
            Dbc2.Close()
            Rs2 = Nothing
            Dbc2 = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("error Valorizzazione Importo2 : <br/>" & ex.Message & "<br/>")
        End Try

        'Response.Write("AUX" & CDbl(ImportoAux))
        'Response.Write("Incassato" & CDbl(ImportoIncassato))
        tx_importo.Text = Format(CDbl(ImportoAux) - CDbl(ImportoIncassato), "0.00")
        'Response.Write("Da Incassare" & tx_importo.Text)
        'Response.End()
    End Sub

    Protected Sub Inizializza()
        div_pagamento_pulsantiera.Visible = False
        divIntestazione.Visible = False
        div_contenitore_pos.Visible = True
        divImmagine.Visible = True
    End Sub

    'Tony 30/01/2023
    'Protected Sub ControlloEsistenzaPAN(ByVal NumeroDelDocumento As String)
    '    Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '    Dbc.Open()

    '    Try
    '        If txtCodicePANDepositoBancomat.Text & "" = "" Then
    '            Dim sqlStr As String = "SELECT id, num_contratto, num_calcolo, status, id_gruppo_auto FROM contratti WITH(NOLOCK)  WHERE (num_contratto = '" & NumeroDelDocumento & "') AND (attivo = 1)"
    '            'Response.Write(sqlStr & "<br>")
    '            'Response.End()

    '            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '            Dim Rs As Data.SqlClient.SqlDataReader
    '            Rs = Cmd.ExecuteReader()

    '            If Rs.HasRows Then
    '                Rs.Read()

    '                Try
    '                    Dim Dbc3 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '                    Dbc3.Open()

    '                    Dim Cmd3 As New Data.SqlClient.SqlCommand("", Dbc3)

    '                    Dim SqlQuery As String
    '                    Dim Sql3 As String = "insert into contratti_costi (id_documento,num_calcolo,ordine_stampa,id_gruppo,id_elemento,nome_costo,selezionato,prepagato,valore_costo,imponibile,iva_imponibile,imponibile_scontato,iva_imponibile_scontato,aliquota_iva,codice_iva,iva_inclusa,scontabile,omaggiabile,acquistabile_nolo_in_corso,id_a_carico_di,id_metodo_stampa,obbligatorio,id_unita_misura,qta,packed)" & _
    '                                        " values('" & Rs("id") & "','" & Rs("num_calcolo") & "','4','" & Rs("id_gruppo_auto") & "','206','Spese bancarie','1','0','15','" & Replace("12,295081967213115", ",", ".") & "','" & Replace("2,7049180327868854", ",", ".") & "','" & Replace("12,295081967213115", ",", ".") & "','" & Replace("2,7049180327868854", ",", ".") & "','22','022','1','0','1','1'," & _
    '                                        "'2','2','0','0','1','1')"
    '                    'Response.Write(Sql3 & "<br>")
    '                    'Response.End()

    '                    Cmd3 = New Data.SqlClient.SqlCommand(Sql3, Dbc3)
    '                    Cmd3.ExecuteNonQuery()

    '                    'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
    '                    'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
    '                    'Session("residenza_virtuale") = Cmd.ExecuteScalar

    '                    SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql3, "'", "''") & "')"
    '                    Cmd3 = New Data.SqlClient.SqlCommand(SqlQuery, Dbc3)
    '                    Cmd3.ExecuteNonQuery()


    '                    Cmd3.Dispose()
    '                    Dbc3.Close()
    '                    Cmd3 = Nothing
    '                    Dbc3 = Nothing

    '                Catch ex As Exception
    '                    Libreria.genUserMsgBox(Me, "Insert Su Prenotazione Errore contattare amministratore del sistema.")
    '                End Try
    '                'Response.End()

    '                'Settaggio campi prepagati su prenotazioni costi Riga TOTALE
    '                Dim Dbc5 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '                Dbc5.Open()

    '                Try
    '                    Try
    '                        Dim ValoreCostoNew, ImponibileNew, IvaNew As String

    '                        Dim DbcUpdate As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '                        DbcUpdate.Open()

    '                        Dim sqlStrUpdate As String = "SELECT valore_costo,imponibile,iva_imponibile FROM contratti_costi WITH(NOLOCK)  WHERE (id_documento = '" & Rs("id") & "') AND (nome_costo = 'TOTALE')"
    '                        Response.Write(sqlStrUpdate & "<br>")
    '                        'Response.End()

    '                        Dim CmdUpdate As New Data.SqlClient.SqlCommand(sqlStrUpdate, DbcUpdate)
    '                        Dim RsUpdate As Data.SqlClient.SqlDataReader
    '                        RsUpdate = CmdUpdate.ExecuteReader()
    '                        If RsUpdate.HasRows Then
    '                            RsUpdate.Read()

    '                            ValoreCostoNew = CDbl(RsUpdate("valore_costo")) + CDbl("15")
    '                            ImponibileNew = CDbl(RsUpdate("imponibile")) + CDbl("12,295081967213115")
    '                            IvaNew = CDbl(RsUpdate("iva_imponibile")) + CDbl("2,7049180327868854")
    '                            Response.Write(ValoreCostoNew & "<br>")
    '                            Response.Write(ImponibileNew & "<br>")
    '                            Response.Write(IvaNew & "<br>")


    '                            'Aggiorno Riga TOTALE
    '                            Dim Cmd5 As New Data.SqlClient.SqlCommand("", Dbc5)

    '                            Dim SqlQuery As String
    '                            Dim Sql5 As String = "update contratti_costi set valore_costo = '" & Replace(ValoreCostoNew, ",", ".") & "', imponibile='" & Replace(ImponibileNew, ",", ".") & "', iva_imponibile ='" & Replace(IvaNew, ",", ".") & "', imponibile_scontato='" & Replace(ImponibileNew, ",", ".") & "', iva_imponibile_scontato ='" & Replace(IvaNew, ",", ".") & "' WHERE (id_documento = '" & Rs("id") & "') AND (nome_costo = 'TOTALE')"
    '                            Response.Write(Sql5 & "<br>")
    '                            'Response.End()

    '                            Cmd5 = New Data.SqlClient.SqlCommand(Sql5, Dbc5)
    '                            Cmd5.ExecuteNonQuery()

    '                            SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql5, "'", "''") & "')"
    '                            Cmd5 = New Data.SqlClient.SqlCommand(SqlQuery, Dbc5)
    '                            Cmd5.ExecuteNonQuery()

    '                            Cmd5.Dispose()
    '                            Dbc5.Close()
    '                            Cmd5 = Nothing
    '                            Dbc5 = Nothing
    '                        End If

    '                        RsUpdate.Close()
    '                        CmdUpdate.Dispose()
    '                        DbcUpdate.Close()
    '                        RsUpdate = Nothing
    '                        CmdUpdate = Nothing
    '                        DbcUpdate = Nothing

    '                    Catch ex As Exception
    '                        Libreria.genUserMsgBox(Me, "Aggiorna Dati Errore contattare amministratore del sistema.")
    '                    End Try

    '                Catch ex As Exception
    '                    Libreria.genUserMsgBox(Me, "Update Su Prenotazione Voce TOTALE Errore contattare amministratore del sistema.")
    '                End Try
    '            End If

    '            Rs.Close()
    '            Cmd.Dispose()
    '            Dbc.Close()
    '            Rs = Nothing
    '            Cmd = Nothing
    '            Dbc = Nothing
    '        End If
    '    Catch ex As Exception
    '        Libreria.genUserMsgBox(Me, "Select Su Contratto x Esistenza PAN Errore contattare amministratore del sistema.")
    '    End Try

    '    'Response.End()
    'End Sub
    'FINE Tony
End Class
