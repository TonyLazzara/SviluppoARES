Imports System.IO
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class AllegatiT
    Private _id As Integer
    Private _DataCreazione As DateTime
    Private _IdTipoDocumento As Integer
    Private _NomeFile As String
    Private _PercorsoFile As String
    Private _Id_rif As Integer

    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Property DataCreazione() As DateTime
        Get
            Return _DataCreazione
        End Get
        Set(ByVal value As DateTime)
            _DataCreazione = value
        End Set
    End Property

    Public Property IdTipoDocumento() As Integer
        Get
            Return _IdTipoDocumento
        End Get
        Set(ByVal value As Integer)
            _IdTipoDocumento = value
        End Set
    End Property

    Public Property NomeFile() As String
        Get
            Return _NomeFile
        End Get
        Set(ByVal value As String)
            _NomeFile = value
        End Set
    End Property

    Public Property PercorsoFile() As String
        Get
            Return _PercorsoFile
        End Get
        Set(ByVal value As String)
            _PercorsoFile = value
        End Set
    End Property

    Public Property Id_rif() As Integer
        Get
            Return _Id_rif
        End Get
        Set(ByVal value As Integer)
            _Id_rif = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub InsertAllegatoT(tbl As String, maxid As Integer)

        Dim id_operatore As String = HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente")


        Dim sqlStr As String = "INSERT INTO " & tbl & " (Id,DataCreazione,IdTipoDocumento,NomeFile,PercorsoFile,Id_rif,id_operatore) " &
            " VALUES (@Id,@DataCreazione,@IdTipoDocumento,@NomeFile,@PercorsoFile,@Id_rif,'" & id_operatore & "')"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = maxid
                Cmd.Parameters.Add("@DataCreazione", System.Data.SqlDbType.DateTime).Value = DataCreazione
                Cmd.Parameters.Add("@IdTipoDocumento", System.Data.SqlDbType.Int).Value = IdTipoDocumento
                Cmd.Parameters.Add("@NomeFile", System.Data.SqlDbType.NVarChar).Value = NomeFile
                Cmd.Parameters.Add("@PercorsoFile", System.Data.SqlDbType.NVarChar).Value = PercorsoFile
                Cmd.Parameters.Add("@Id_rif", System.Data.SqlDbType.Int).Value = Id_rif
                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
    Public Sub DeleteAllegatoT(tbl As String)
        Dim sqlStr As String = "DELETE FROM " & tbl & " WHERE Id = @id"
        'HttpContext.Current.Trace.Write("--------------------------- sql insert: " & sqlStr)
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = id
                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub


    Public Function VerificaFileSeImportatoPrima(ByVal file As String, tbl As String) As Boolean
        If file = "" Then Return False

        Dim strRd As String = "SELECT id FROM " & tbl & " WHERE NomeFile='" & file & "'"

        Dim MyReader As SqlDataReader
        Dim MyConnection As SqlConnection = New SqlConnection
        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString
        Using MyCommand As SqlCommand = New SqlCommand()
            MyCommand.CommandText = strRd
            MyCommand.CommandType = CommandType.Text
            MyCommand.Connection = MyConnection

            MyCommand.Connection.Open()
            MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

            If MyReader.HasRows Then
                Return True
            Else
                Return False
            End If
        End Using

    End Function
End Class
Partial Class trasferimenti
    Inherits System.Web.UI.Page
    Dim sqla As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        AddHandler gestione_checkin.ChiusuraForm, AddressOf gestione_danni_ChiusuraForm
        AddHandler gestione_checkin.SalvaCheckIn, AddressOf gestione_danni_SalvataggioCheckIn

        sqlTrasferimentiStatusDaStazione.SelectCommand = "SELECT id, descrizione FROM trasferimenti_status WITH(NOLOCK) WHERE (id='" & Costanti.id_trasferimento_accettato & "' OR id='" & Costanti.id_trasferimento_in_corso & "' OR id='" & Costanti.id_trasferimento_eseguito & "') ORDER BY id"
        sqlTrasferimentiStatusAStazione.SelectCommand = "SELECT id, descrizione FROM trasferimenti_status WITH(NOLOCK) WHERE (id='" & Costanti.id_trasferimento_in_corso & "' OR id='" & Costanti.id_trasferimento_eseguito & "') ORDER BY id"
        Try


            If Not Page.IsPostBack() Then

                permesso_accesso.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.TrasferimentoVeicoli)
                permesso_admin.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.TrasferimentoVeicoliAdmin)

                id_operatore.Text = Request.Cookies("SicilyRentCar")("idUtente")
                id_stazione_operatore.Text = Request.Cookies("SicilyRentCar")("stazione")

                dropStazioneRichiesta.DataBind()
                dropCercaStazioneRichiesta.DataBind()
                dropGruppoRichiesto.DataBind()
                dropCercaStazioneRichiestaCheckIn.DataBind()
                dropDrivers.DataBind()
                dropDriversInterno.DataBind()
                dropConducenteCheckIn.DataBind()
                dropStazionePickUpInterno.DataBind()
                dropStazionePresuntoDropOffInterno.DataBind()

                dropCercaStatoCheck.DataBind()
                dropCercaStatoCheck.SelectedValue = Costanti.id_trasferimento_accettato

                dropCercaStatusCheckIn.DataBind()
                dropCercaStatusCheckIn.SelectedValue = Costanti.id_trasferimento_in_corso

                dropCercaStatus.DataBind()
                dropCercaStatus.SelectedValue = Costanti.id_trasferimento_richiesta

                If permesso_accesso.Text <> "1" Then
                    If permesso_admin.Text = "1" Then
                        dropCercaStazioneRichiesta.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
                        dropCercaStazionePickUpCheck.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
                        dropCercaStazioneRichiestaCheckIn.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
                        dropCercaStazionePickUpCheck.Enabled = False
                        dropCercaStazioneRichiesta.Enabled = False
                        dropCercaStazioneRichiestaCheckIn.Enabled = False

                        btnNuovaRichiesta.Visible = True
                    Else
                        btnNuovaRichiesta.Visible = True
                    End If
                Else
                    Response.Redirect("default.aspx")
                End If

                lblOrderBY.Text = "  ORDER BY trasferimenti.id ASC"
                lblOrderBy2.Text = " ORDER BY trasferimenti.id ASC"
                lblOrderBy3.Text = " ORDER BY trasferimenti.id ASC"
                lblOrderBy4.Text = " ORDER BY trasferimenti.id ASC"

                ricerca(lblOrderBY.Text)
                ricerca_trasf_staz(lblOrderBy2.Text)
                ricerca_trasf_in_ingresso(lblOrderBy3.Text)
                ricerca_trasf_interni(lblOrderBy4.Text)
            Else
                sqlRichiesteStazione.SelectCommand = query_cerca.Text & " " & lblOrderBY.Text
                sqlTrasferimentiDaEffettuare.SelectCommand = query_cerca2.Text & " " & lblOrderBy2.Text
                sqlTrasferimentiVersoStazione.SelectCommand = query_cerca3.Text & " " & lblOrderBy3.Text
                sqlTrasferimentiInterni.SelectCommand = query_cerca4.Text & " " & lblOrderBy4.Text
            End If

        Catch ex As Exception

        End Try




    End Sub


    Public Shared Function GetMaxIdAllegati(tbl As String) As Integer
        Dim MyReader As SqlDataReader
        Using MyConnection As SqlConnection = New SqlConnection

            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString

            Using MyCommand As SqlCommand = New SqlCommand()
                MyCommand.CommandText = "SELECT MAX(ID) AS MaxId FROM " & tbl & ";"
                MyCommand.CommandType = CommandType.Text
                MyCommand.Connection = MyConnection

                MyCommand.Connection.Open()

                MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

                MyReader.Read()
                If MyReader.IsDBNull(0) Then
                    Return 0
                Else
                    Return MyReader("MaxId")
                End If

            End Using
        End Using
    End Function

    Protected Sub salva_rientro()
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT km_rientro FROM trasferimenti WITH(NOLOCK) WHERE id='" & id_modifica.Text & "'", Dbc)
            txtKmRientroCheckIn.Text = Cmd.ExecuteScalar
            Cmd = New Data.SqlClient.SqlCommand("SELECT litri_rientro FROM trasferimenti WITH(NOLOCK) WHERE id='" & id_modifica.Text & "'", Dbc)
            txtSerbatoioRientroCheckIn.Text = Cmd.ExecuteScalar
            Cmd = New Data.SqlClient.SqlCommand("SELECT data_rientro FROM trasferimenti WITH(NOLOCK) WHERE id='" & id_modifica.Text & "'", Dbc)

            Dim test As String = Cmd.ExecuteScalar
            txtDataRientroCheckIn.Text = Day(test) & "/" & Month(test) & "/" & Year(test)
            txtOraRientroCheckIn.Text = IIf(Len(CStr(Hour(test))) = 1, "0" & Hour(test), Hour(test)) & "." & IIf(Len(CStr(Month(test))) = 1, "0" & Month(test), Month(test))

            sqla = "UPDATE trasferimenti SET id_operatore_rientro='" & Request.Cookies("SicilyRentCar")("IdUtente") & "', " &
            "status='" & Costanti.id_trasferimento_eseguito & "'" &
            "WHERE id='" & id_modifica.Text & "'"

            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()

            Libreria.genUserMsgBox(Me, "Richiesta memorizzata correttamente.")

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error salva_rientro :" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Protected Sub gestione_danni_SalvataggioCheckIn(ByVal sender As Object, ByVal e As EventoNuovoRecord)
        salva_rientro()

        btnStampaFoglioTrasferimentoCheckIn.Visible = True
        btnCheckIn.Text = "Vedi Check"

        div_edit_danno.Visible = False
        tabPanel1.Visible = True
    End Sub

    Protected Sub gestione_danni_ChiusuraForm(ByVal sender As Object, ByVal e As System.EventArgs)
        div_edit_danno.Visible = False
        tabPanel1.Visible = True
    End Sub

    Protected Sub listTrasferimentiAppoggio_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles listTrasferimentiAppoggio.ItemCommand
        If e.CommandName = "scegli_gruppo" Then
            Dim dropGruppoDaScegliere As DropDownList = e.Item.FindControl("dropGruppoDaScegliere")
            Dim id_stazione As Label = e.Item.FindControl("id_stazione")

            dropGruppoDaTrasferire.SelectedValue = dropGruppoDaScegliere.SelectedValue
            dropStazionePickUp.SelectedValue = id_stazione.Text

            elimina_righe_appoggio()
            listTrasferimentiAppoggio.DataBind()
        End If
    End Sub


    Protected Sub listTrasferimentiAppoggio_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles listTrasferimentiAppoggio.ItemDataBound
        Dim id_stazione As Label = e.Item.FindControl("id_stazione")
        Dim sqlTrasferimentiAppoggioGruppi As SqlDataSource = e.Item.FindControl("sqlTrasferimentiAppoggioGruppi")
        Dim distanza As Label = e.Item.FindControl("distanza")

        If distanza.Text <> "0" Then
            distanza.Text = " - Distanza : " & distanza.Text & " Km"
        Else
            distanza.Text = ""
        End If

        Dim sqlStr As String = "SELECT gruppi.id_gruppo, gruppi.cod_gruppo, valore_iniziale, pru_12, pru_16, pru_20, pru_24, pru_tot, rp_12, rp_16, rp_20, rp_24, rp_tot, rc_12, rc_16, rc_20, rc_24, rc_tot, tot_12, tot_16, tot_20, tot_24, tot_tot FROM trasferimenti_appoggio WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON trasferimenti_appoggio.id_gruppo=gruppi.id_gruppo WHERE id_trasferimenti=" & id_modifica.Text & " AND id_operatore='" & id_operatore.Text & "' AND id_stazione='" & id_stazione.Text & "' ORDER BY cod_gruppo"

        sqlTrasferimentiAppoggioGruppi.SelectCommand = sqlStr
        sqlTrasferimentiAppoggioGruppi.DataBind()

        Dim dropGruppoDaScegliere As DropDownList = e.Item.FindControl("dropGruppoDaScegliere")

        For i = 0 To listGruppiPickSelezionati.Items.Count - 1
            dropGruppoDaScegliere.Items().Add(listGruppiPickSelezionati.Items(i).Text)
            dropGruppoDaScegliere.Items(i).Value = listGruppiPickSelezionati.Items(i).Value
        Next

    End Sub


    'IN COSTANTI VENGONO DEFINITI GLI ID DELLA TABELLA trasferimenti_status INDISPENSABILI PER LA FUNZIONALITA' CORRENTI
    Protected Sub PassaUnoStaz_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassaUnoStaz.Click
        Dim k As Integer = 0
        Dim j As Integer = listStazioniPickSelezionate.Items.Count

        For i = 0 To listStazioniPick.Items.Count() - 1
            If listStazioniPick.Items(i).Selected Then
                listStazioniPickSelezionate.Items.Add(listStazioniPick.Items(i).Text)
                listStazioniPickSelezionate.Items(j).Value = listStazioniPick.Items(i).Value
                j = j + 1

                k = k + 1
            End If
        Next

        For i = 1 To k
            listStazioniPick.Items.Remove(listStazioniPick.SelectedItem)
        Next

        funzioni_comuni.SortListBox(listStazioniPickSelezionate)
    End Sub

    Protected Sub TornaUnoStaz_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TornaUnoStaz.Click
        Dim k As Integer = 0
        Dim j As Integer = listStazioniPick.Items.Count

        For i = 0 To listStazioniPickSelezionate.Items.Count() - 1
            If listStazioniPickSelezionate.Items(i).Selected Then
                listStazioniPick.Items.Add(listStazioniPickSelezionate.Items(i).Text)
                listStazioniPick.Items(j).Value = listStazioniPickSelezionate.Items(i).Value
                j = j + 1

                k = k + 1
            End If
        Next

        For i = 1 To k
            listStazioniPickSelezionate.Items.Remove(listStazioniPickSelezionate.SelectedItem)
        Next

        funzioni_comuni.SortListBox(listStazioniPick)
    End Sub

    Protected Sub PassaTuttiStaz_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassaTuttiStaz.Click
        Dim j As Integer = listStazioniPickSelezionate.Items.Count

        For i = 0 To listStazioniPick.Items.Count() - 1
            listStazioniPickSelezionate.Items.Add(listStazioniPick.Items(i).Text)
            listStazioniPickSelezionate.Items(j).Value = listStazioniPick.Items(i).Value
            j = j + 1
        Next

        listStazioniPick.Items.Clear()
        funzioni_comuni.SortListBox(listStazioniPickSelezionate)
    End Sub

    Protected Sub TornaTuttiStaz_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TornaTuttiStaz.Click
        listStazioniPick.Items.Clear()
        listStazioniPickSelezionate.Items.Clear()

        listStazioniPick.DataBind()
    End Sub

    Protected Sub PassaUnoGruppi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassaUnoGruppi.Click
        Dim k As Integer = 0
        Dim j As Integer = listGruppiPickSelezionati.Items.Count

        For i = 0 To listGruppiPick.Items.Count() - 1
            If listGruppiPick.Items(i).Selected Then
                listGruppiPickSelezionati.Items.Add(listGruppiPick.Items(i).Text)
                listGruppiPickSelezionati.Items(j).Value = listGruppiPick.Items(i).Value
                j = j + 1

                k = k + 1
            End If
        Next

        For i = 1 To k
            listGruppiPick.Items.Remove(listGruppiPick.SelectedItem)
        Next

        funzioni_comuni.SortListBox(listGruppiPickSelezionati)
    End Sub

    Protected Sub TornaUnoGruppi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TornaUnoGruppi.Click
        Dim k As Integer = 0
        Dim j As Integer = listGruppiPick.Items.Count

        For i = 0 To listGruppiPickSelezionati.Items.Count() - 1
            If listGruppiPickSelezionati.Items(i).Selected Then
                listGruppiPick.Items.Add(listGruppiPickSelezionati.Items(i).Text)
                listGruppiPick.Items(j).Value = listGruppiPickSelezionati.Items(i).Value
                j = j + 1

                k = k + 1
            End If
        Next

        For i = 1 To k
            listGruppiPickSelezionati.Items.Remove(listGruppiPickSelezionati.SelectedItem)
        Next

        funzioni_comuni.SortListBox(listGruppiPick)
    End Sub

    Protected Sub PassaTuttiGruppi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassaTuttiGruppi.Click
        Dim j As Integer = listGruppiPickSelezionati.Items.Count

        For i = 0 To listGruppiPick.Items.Count() - 1
            listGruppiPickSelezionati.Items.Add(listGruppiPick.Items(i).Text)
            listGruppiPickSelezionati.Items(j).Value = listGruppiPick.Items(i).Value
            j = j + 1
        Next

        listGruppiPick.Items.Clear()
        funzioni_comuni.SortListBox(listGruppiPickSelezionati)
    End Sub

    Protected Sub TornaTuttiGruppi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TornaTuttiGruppi.Click
        listGruppiPick.Items.Clear()
        listGruppiPickSelezionati.Items.Clear()

        listGruppiPick.DataBind()
    End Sub

    Protected Sub btnNuovaRichiesta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovaRichiesta.Click
        div_ricerca.Visible = False
        div_dati.Visible = True
        div_trasferimenti_stazione.Visible = False

        dropStazioneRichiesta.Enabled = True
        dropStazioneRichiesta.SelectedValue = "0"
        dropGruppoRichiesto.Enabled = True
        dropGruppoRichiesto.SelectedValue = "0"
        txtRichiestaPerData.Enabled = True
        txtRichiestaPerData.Text = ""
        txtRichiestaPerOra.Enabled = True
        txtRichiestaPerOra.Text = ""

        lblNoteAdminXStazione.Text = ""
        lblInfoGruppoPick.Text = ""
        lblInfoStazionePick.Text = ""
        lblStazionePickUp.Text = ""
        lblGruppoPickUp.Text = ""

        abilitazione_campi_richiesta(True, False, False, "1")

        dropStazioneRichiesta.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
        If permesso_admin.Text = "3" Then
            dropStazioneRichiesta.Enabled = True
        Else
            dropStazioneRichiesta.Enabled = False
        End If

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click
        listTrasferimentiDaEffettuare.DataBind()
        listTrasferimentiVersoStazione.DataBind()
        listRichieste.DataBind()
        listTrasferimentiInterni.DataBind()

        annulla()
    End Sub

    Protected Sub annulla()
        div_ricerca.Visible = True
        div_dati.Visible = False
        div_trasferimenti_stazione.Visible = True

        'listRichieste.DataBind()
        'listTrasferimentiDaEffettuare.DataBind()

        dropGruppoRichiesto.SelectedValue = "0"
        txtRichiestaPerData.Text = ""
        txtRichiestaPerOra.Text = ""
        txtRiferimento.Text = ""
        id_modifica.Text = ""
        txtMotivoAnnullamento.Text = ""
    End Sub

    Protected Sub salva_nuova_richiesta()
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim richiesta_per_data As String = funzioni_comuni.GetDataSql(txtRichiestaPerData.Text, 99) & " " & txtRichiestaPerOra.Text & ":00:00"

            Dim sqlStr As String = "INSERT INTO trasferimenti (status, tipologia, id_gruppo_richiesto, richiesta_per_id_stazione, riferimento, id_stazione_richiesta, "
            sqlStr += "id_operatore_richiesta, richiesta_per_data, data_richiesta) VALUES ("
            sqlStr += "'" & Costanti.id_trasferimento_richiesta & "','1','" & dropGruppoRichiesto.SelectedValue & "','" & dropStazioneRichiesta.SelectedValue & "','" & Replace(txtRiferimento.Text, "'", "''") & "', "
            sqlStr += "'" & Request.Cookies("SicilyRentCar")("stazione") & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "', "
            sqlStr += "convert(datetime,'" & richiesta_per_data & "',102),convert(datetime,GetDate(),102))"


            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            If permesso_admin.Text = "3" Then
                'NEL CASO DI ADMIN SI PASSA DIRETTAMENTE ALLA FASE DI RICERCA
                abilitazione_campi_richiesta(False, False, True, Costanti.id_trasferimento_richiesta)

                dropStazionePickUp.SelectedValue = "0"
                dropGruppoDaTrasferire.SelectedValue = "0"
                txtNoteAdmin.Text = ""
            Else
                Libreria.genUserMsgBox(Me, "Richiesta memorizzata correttamente.")
                annulla()
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error salva_nuova_richiesta :" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Protected Sub btnSalvaRichiesta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvaRichiesta.Click
        If txtRichiestaPerData.Text = "" Then
            Libreria.genUserMsgBox(Me, "Specificare la data.")
        ElseIf txtRichiestaPerOra.Text = "" Then
            Libreria.genUserMsgBox(Me, "Specificare l'orario.")
        ElseIf CInt(txtRichiestaPerOra.Text) < 0 Or CInt(txtRichiestaPerOra.Text) > 23 Then
            Libreria.genUserMsgBox(Me, "Specificare correttamente l'orario.")
        ElseIf dropGruppoRichiesto.SelectedValue = "0" Then
            Libreria.genUserMsgBox(Me, "Specificar il gruppo richiesto.")
        Else
            salva_nuova_richiesta()
        End If

    End Sub

    Protected Function condizione_where_stazione() As String
        Dim condizione As String = ""

        If dropCercaStazioneRichiesta.SelectedValue <> "0" Then
            condizione = condizione & " AND trasferimenti.richiesta_per_id_stazione='" & dropCercaStazioneRichiesta.SelectedValue & "'"
        End If

        If dropCercaStatus.SelectedValue <> "0" Then
            condizione = condizione & " AND trasferimenti.status='" & dropCercaStatus.SelectedValue & "'"
        End If

        If dropCercaGruppoRichiesto.SelectedValue <> "0" Then
            condizione = condizione & " AND trasferimenti.id_gruppo_richiesto='" & dropCercaGruppoRichiesto.SelectedValue & "'"
        End If

        'RICHIESTA PER DATA-----------------------------------------------------------------------------------------------------------
        If txtCercaRichiestaPerDataDa.Text <> "" And txtCercaRichiestaPerDataA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaRichiestaPerDataDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaRichiestaPerDataDa.Text)
            'condizione = condizione & " AND richiesta_per_data >= '" & data1 & "'"
            condizione += " AND richiesta_per_data >= CONVERT(DATETIME, '" & data1 & "',102)"
        End If

        If txtCercaRichiestaPerDataDa.Text = "" And txtCercaRichiestaPerDataA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaRichiestaPerDataA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaRichiestaPerDataA.Text & " 23:59:59")
            condizione += " AND richiesta_per_data <= CONVERT(DATETIME, '" & data2 & "',102)"
            'condizione = condizione & " AND richiesta_per_data <= '" & data2 & "'"
        End If

        If txtCercaRichiestaPerDataDa.Text <> "" And txtCercaRichiestaPerDataA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaRichiestaPerDataDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaRichiestaPerDataDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaRichiestaPerDataA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaRichiestaPerDataA.Text & " 23:59:59")
            'condizione = condizione & " AND richiesta_per_data BETWEEN '" & data1 & "' AND '" & data2 & "'"
            condizione += " AND richiesta_per_data BETWEEN CONVERT(DATETIME, '" & data1 & "',102) AND CONVERT(DATETIME, '" & data2 & "',102)"
        End If
        '----------------------------------------------------------------------------------------------------------------------------

        If dropCercaStazionePickUp.SelectedValue <> "0" Then
            condizione = condizione & " AND trasferimenti.id_stazione_uscita='" & dropCercaStazionePickUp.SelectedValue & "'"
        End If

        'DATA RICHIESTA -------------------------------------------------------------------------------------------------------------
        If txtCercaDataRichiestaDa.Text <> "" And txtCercaDataRichiestaA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaDataRichiestaDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaDataRichiestaDa.Text)
            condizione += " AND data_richiesta >= CONVERT(DATETIME, '" & data1 & "',102)" '" & data1 & "'"
        End If

        If txtCercaDataRichiestaDa.Text = "" And txtCercaDataRichiestaA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaDataRichiestaA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaDataRichiestaA.Text & " 23:59:59")
            condizione += " AND data_richiesta <= CONVERT(DATETIME, '" & data2 & "',102)" ''" & data2 & "'"
        End If

        If txtCercaDataRichiestaDa.Text <> "" And txtCercaDataRichiestaA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaDataRichiestaDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtCercaDataRichiestaDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaDataRichiestaA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaDataRichiestaA.Text & " 23:59:59")
            'condizione = condizione & " AND data_richiesta BETWEEN '" & data1 & "' AND '" & data2 & "'"
            condizione += " AND data_richiesta BETWEEN CONVERT(DATETIME, '" & data1 & "',102) AND CONVERT(DATETIME, '" & data2 & "',102)"
        End If
        '----------------------------------------------------------------------------------------------------------------------------
        condizione_where_stazione = condizione
    End Function

    Protected Function condizione_where_trasf_in_ingresso() As String
        'IN QUESTA LISTA SI VEDONO I TRASFERIMENTI DELLE STAZIONI, QUINDI SOLO LO STATUS 'ACCETTATO' , 'IN CORSO', 'ESEGUITO'
        'CHI NON HA I PERMESSI ADMIN VEDE SOLAMENTE QUELLI DELLA PROPRIA STAZIONE

        Dim condizione As String = " AND (trasferimenti.status='" & Costanti.id_trasferimento_in_corso & "' OR trasferimenti.status='" & Costanti.id_trasferimento_eseguito & "')"

        If dropCercaStatusCheckIn.SelectedValue <> "0" Then
            condizione = condizione & " AND trasferimenti.status='" & dropCercaStatusCheckIn.SelectedValue & "'"
        End If

        If permesso_admin.Text = "1" Then
            condizione = condizione & " AND trasferimenti.richiesta_per_id_stazione='" & id_stazione_operatore.Text & "'"
        Else
            If dropCercaStazioneRichiestaCheckIn.SelectedValue <> "0" Then
                condizione = condizione & " AND trasferimenti.richiesta_per_id_stazione='" & dropCercaStazioneRichiestaCheckIn.SelectedValue & "'"
            End If
        End If

        If dropCercaStazionePickCheckIn.SelectedValue <> "0" Then
            condizione = condizione & " AND trasferimenti.id_stazione_uscita='" & dropCercaStazionePickCheckIn.SelectedValue & "'"
        End If

        'RICHIESTA PER DATA-----------------------------------------------------------------------------------------------------------
        If txtRichiestaPerDataCheckInDa.Text <> "" And txtRichiestaPerDataCheckInA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtRichiestaPerDataCheckInDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtRichiestaPerDataCheckInDa.Text)
            'condizione = condizione & " AND richiesta_per_data >= '" & data1 & "'"
            condizione = condizione & " AND richiesta_per_data >= CONVERT(DATETIME, '" & data1 & "',102)" ''" & data1 & "'"
        End If

        If txtRichiestaPerDataCheckInDa.Text = "" And txtRichiestaPerDataCheckInA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtRichiestaPerDataCheckInA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtRichiestaPerDataCheckInA.Text & " 23:59:59")
            ' condizione = condizione & " AND richiesta_per_data <= '" & data2 & "'"
            condizione += " AND richiesta_per_data <= CONVERT(DATETIME, '" & data2 & "',102)" ''" & data2 & "'"
        End If

        If txtRichiestaPerDataCheckInDa.Text <> "" And txtRichiestaPerDataCheckInA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtRichiestaPerDataCheckInDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtRichiestaPerDataCheckInDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtRichiestaPerDataCheckInA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtRichiestaPerDataCheckInA.Text & " 23:59:59")
            condizione += " AND richiesta_per_data BETWEEN CONVERT(DATETIME, '" & data1 & "',102) AND CONVERT(DATETIME, '" & data2 & "',102)"
            'condizione = condizione & " AND richiesta_per_data BETWEEN '" & data1 & "' AND '" & data2 & "'"
        End If
        '----------------------------------------------------------------------------------------------------------------------------

        If dropCercaGruppoCheckIn.SelectedValue <> "0" Then
            condizione = condizione & " AND trasferimenti.id_gruppo_richiesto='" & dropCercaGruppoCheckIn.SelectedValue & "'"
        End If

        If txtCercaTargaCheckIn.Text <> "" Then
            condizione = condizione & " AND trasferimenti.targa='" & Replace(txtCercaTargaCheckIn.Text, "'", "''") & "'"
        End If

        condizione_where_trasf_in_ingresso = condizione
    End Function

    Protected Function condizione_where_trasf_staz() As String
        'IN QUESTA LISTA SI VEDONO I TRASFERIMENTI DELLE STAZIONI, QUINDI SOLO LO STATUS 'ACCETTATO' , 'IN CORSO', 'ESEGUITO'
        'CHI NON HA I PERMESSI ADMIN VEDE SOLAMENTE QUELLI DELLA PROPRIA STAZIONE

        Dim condizione As String = " AND (trasferimenti.status='" & Costanti.id_trasferimento_accettato & "' OR trasferimenti.status='" & Costanti.id_trasferimento_in_corso & "' OR trasferimenti.status='" & Costanti.id_trasferimento_eseguito & "')"

        If dropCercaStatoCheck.SelectedValue <> "0" Then
            condizione = condizione & " AND trasferimenti.status='" & dropCercaStatoCheck.SelectedValue & "'"
        End If

        If permesso_admin.Text = "1" Then
            condizione = condizione & " AND trasferimenti.id_stazione_uscita='" & id_stazione_operatore.Text & "'"
        Else
            If dropCercaStazionePickUp.SelectedValue <> "0" Then
                condizione = condizione & " AND trasferimenti.id_stazione_uscita='" & dropCercaStazionePickUpCheck.SelectedValue & "'"
            End If
        End If

        'RICHIESTA PER DATA-----------------------------------------------------------------------------------------------------------
        If txtRichiestaPerDataCheckDa.Text <> "" And txtRichiestaPerDataCheckA.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtRichiestaPerDataCheckDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtRichiestaPerDataCheckDa.Text)
            condizione += " AND richiesta_per_data >= CONVERT(DATETIME, '" & data1 & "',102)" '" & data1 & "'"
        End If

        If txtRichiestaPerDataCheckDa.Text = "" And txtRichiestaPerDataCheckA.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaRichiestaPerDataA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtCercaRichiestaPerDataA.Text & " 23:59:59")
            condizione = condizione & " AND richiesta_per_data <= CONVERT(DATETIME, '" & data2 & "',102)" '" & data2 & "'"
        End If

        If txtRichiestaPerDataCheckDa.Text <> "" And txtRichiestaPerDataCheckA.Text <> "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtRichiestaPerDataCheckDa.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(txtRichiestaPerDataCheckDa.Text)
            Dim data2 As String = funzioni_comuni.GetDataSql(txtRichiestaPerDataCheckA.Text, 59) 'funzioni_comuni.getDataDb_con_orario(txtRichiestaPerDataCheckA.Text & " 23:59:59")

            condizione += " AND richiesta_per_data BETWEEN CONVERT(DATETIME, '" & data1 & "',102) AND CONVERT(DATETIME, '" & data2 & "',102)" '" & data1 & "' AND '" & data2 & "'"
        End If
        '----------------------------------------------------------------------------------------------------------------------------

        If dropCercaGruppoCheck.SelectedValue <> "0" Then
            condizione = condizione & " AND trasferimenti.id_gruppo_richiesto='" & dropCercaGruppoCheck.SelectedValue & "'"
        End If

        If dropCercaStazioneRichiestaCheck.SelectedValue <> "0" Then
            condizione = condizione & " AND trasferimenti.id_stazione_richiesta='" & dropCercaStazioneRichiestaCheck.SelectedValue & "'"
        End If

        If txtCercaTarga.Text <> "" Then
            condizione = condizione & " AND trasferimenti.targa='" & Replace(txtCercaTarga.Text, "'", "''") & "'"
        End If

        condizione_where_trasf_staz = condizione
    End Function

    Protected Sub btnCercaCheckIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCercaCheckIn.Click
        ricerca_trasf_in_ingresso(lblOrderBy3.Text)
    End Sub

    Protected Sub btnCercaCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCercaCheck.Click
        ricerca_trasf_staz(lblOrderBy2.Text)
    End Sub

    Protected Sub ricerca_trasf_in_ingresso(ByVal order_by As String)
        Try
            query_cerca3.Text = "SELECT trasferimenti.id, trasferimenti.num_trasferimento, trasferimenti.data_uscita, trasferimenti.data_presunto_rientro, trasferimenti.data_rientro, trasferimenti.status As id_status, trasferimenti_status.descrizione As status, gruppi2.cod_gruppo As gruppo_da_trasferire, trasferimenti.id_gruppo_da_trasferire, (stazioni1.codice + ' ' + stazioni1.nome_stazione) As stazione_pick_up, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As richiesta_per_stazione, trasferimenti.richiesta_per_id_stazione, trasferimenti.riferimento, trasferimenti.id_veicolo, trasferimenti.targa, modelli.descrizione As modello, trasferimenti.id_conducente, trasferimenti.km_uscita, trasferimenti.km_rientro, trasferimenti.litri_uscita, trasferimenti.litri_rientro, trasferimenti.litri_max  FROM trasferimenti WITH(NOLOCK) INNER JOIN trasferimenti_status WITH(NOLOCK) ON trasferimenti.status=trasferimenti_status.id LEFT JOIN gruppi As gruppi2 WITH(NOLOCK) ON trasferimenti.id_gruppo_da_trasferire=gruppi2.id_gruppo LEFT JOIN stazioni As stazioni1 WITH(NOLOCK) ON trasferimenti.id_stazione_uscita=stazioni1.id LEFT JOIN stazioni As stazioni2 WITH(NOLOCK) ON trasferimenti.richiesta_per_id_stazione=stazioni2.id LEFT JOIN veicoli WITH(NOLOCK) ON trasferimenti.id_veicolo=veicoli.id LEFT JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello WHERE trasferimenti.id<>'0' " & condizione_where_trasf_in_ingresso()
            sqlTrasferimentiVersoStazione.SelectCommand = query_cerca3.Text & " " & order_by

            lblOrderBy3.Text = order_by

            listTrasferimentiVersoStazione.DataBind()

        Catch ex As Exception
            Response.Write("error_ricerca_trasf_in_ingresso_:" & ex.Message & "<br/>")
        End Try

    End Sub

    Protected Sub ricerca_trasf_staz(ByVal order_by As String)
        Try

            query_cerca2.Text = "SELECT trasferimenti.id, trasferimenti.num_trasferimento, trasferimenti.id_conducente, trasferimenti.km_uscita, trasferimenti.litri_uscita, trasferimenti.km_rientro, trasferimenti.litri_rientro, trasferimenti.litri_max, trasferimenti.data_presunto_rientro, trasferimenti.data_rientro, trasferimenti.richiesta_per_data, trasferimenti.status As id_status, trasferimenti_status.descrizione As status, gruppi2.cod_gruppo As gruppo_da_trasferire, trasferimenti.id_gruppo_da_trasferire, (stazioni1.codice + ' ' + stazioni1.nome_stazione) As stazione_pick_up, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As richiesta_per_stazione, trasferimenti.richiesta_per_id_stazione, trasferimenti.note_admin, trasferimenti.riferimento, trasferimenti.id_veicolo, trasferimenti.targa, modelli.descrizione As modello FROM trasferimenti WITH(NOLOCK) INNER JOIN trasferimenti_status WITH(NOLOCK) ON trasferimenti.status=trasferimenti_status.id LEFT JOIN gruppi As gruppi2 WITH(NOLOCK) ON trasferimenti.id_gruppo_da_trasferire=gruppi2.id_gruppo LEFT JOIN stazioni As stazioni1 WITH(NOLOCK) ON trasferimenti.id_stazione_uscita=stazioni1.id INNER JOIN stazioni As stazioni2 WITH(NOLOCK) ON trasferimenti.richiesta_per_id_stazione=stazioni2.id LEFT JOIN veicoli WITH(NOLOCK) ON trasferimenti.id_veicolo=veicoli.id LEFT JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello  WHERE tipologia='1' " & condizione_where_trasf_staz()
            sqla = query_cerca2.Text
            sqlTrasferimentiDaEffettuare.SelectCommand = query_cerca2.Text & " " & order_by

            lblOrderBy2.Text = order_by

            listTrasferimentiDaEffettuare.DataBind()
        Catch ex As Exception
            Response.Write("error ricerca_trasf_staz :" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Protected Sub ricerca(ByVal order_by As String)
        Try
            query_cerca.Text = "SELECT trasferimenti.id, trasferimenti.richiesta_per_data, trasferimenti.id_gruppo_richiesto, trasferimenti.status As id_status, trasferimenti_status.descrizione As status, gruppi1.cod_gruppo As gruppo_richiesto, gruppi2.cod_gruppo As gruppo_da_trasferire, trasferimenti.id_gruppo_da_trasferire, riferimento, (ISNULL(operatori1.cognome,'') + ' ' + ISNULL(operatori1.nome,'')) As operatore_richiesta, data_richiesta, (ISNULL(operatori2.cognome,'') + ' ' + ISNULL(operatori2.nome,'')) As operatore_admin, data_gestione_admin, (stazioni1.codice + ' ' + stazioni1.nome_stazione) As stazione_pick_up, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As richiesta_per_stazione, trasferimenti.richiesta_per_id_stazione, trasferimenti.note_admin FROM trasferimenti WITH(NOLOCK) INNER JOIN trasferimenti_status WITH(NOLOCK) ON trasferimenti.status=trasferimenti_status.id INNER JOIN operatori As operatori1 WITH(NOLOCK) ON trasferimenti.id_operatore_richiesta=operatori1.id LEFT JOIN operatori As operatori2 WITH(NOLOCK) ON trasferimenti.id_operatore_admin=operatori2.id INNER JOIN gruppi As gruppi1 WITH(NOLOCK) ON trasferimenti.id_gruppo_richiesto=gruppi1.id_gruppo LEFT JOIN gruppi As gruppi2 WITH(NOLOCK) ON trasferimenti.id_gruppo_da_trasferire=gruppi2.id_gruppo LEFT JOIN stazioni As stazioni1 WITH(NOLOCK) ON trasferimenti.id_stazione_uscita=stazioni1.id INNER JOIN stazioni As stazioni2 WITH(NOLOCK) ON trasferimenti.richiesta_per_id_stazione=stazioni2.id WHERE tipologia='1' " & condizione_where_stazione()
            sqla = query_cerca.Text
            sqlRichiesteStazione.SelectCommand = query_cerca.Text & " " & order_by

            lblOrderBY.Text = order_by

            listRichieste.DataBind()
        Catch ex As Exception
            Response.Write("error ricerca _:" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Protected Sub abilitazione_campi_richiesta(ByVal abilita_campi_salvataggio As Boolean, ByVal abilita_annullamento As Boolean, ByVal abilita_admin As Boolean, ByVal id_status As String)
        If abilita_campi_salvataggio Then
            dropStazioneRichiesta.Enabled = True
            dropGruppoRichiesto.Enabled = True
            txtRichiestaPerData.Enabled = True
            txtRichiestaPerOra.Enabled = True
            txtRiferimento.Enabled = True
            btnSalvaRichiesta.Visible = True
        Else
            dropStazioneRichiesta.Enabled = False
            dropGruppoRichiesto.Enabled = False
            txtRichiestaPerData.Enabled = False
            txtRichiestaPerOra.Enabled = False
            txtRiferimento.Enabled = False
            btnSalvaRichiesta.Visible = False
        End If

        If abilita_annullamento Then
            riga_annullamento_1.Visible = True
            riga_annullamento_2.Visible = True
            If id_status = Costanti.id_trasferimento_richiesta Then
                btnAnnullaRichiesta.Visible = True
            Else
                btnAnnullaRichiesta.Visible = False
            End If
        Else
            riga_annullamento_1.Visible = False
            riga_annullamento_2.Visible = False
            btnAnnullaRichiesta.Visible = False
        End If

        If abilita_admin Then
            tab_admin.Visible = True
            btnCercaGruppi.Visible = True
            If id_status = Costanti.id_trasferimento_richiesta Then
                btnNegaTrasferimento.Visible = True
                btnAccettaTrasferimento.Visible = True
                dropStazionePickUp.Enabled = True
                dropGruppoDaTrasferire.Enabled = True
            Else
                btnNegaTrasferimento.Visible = False
                btnAccettaTrasferimento.Visible = False
                dropStazionePickUp.Enabled = False
                dropGruppoDaTrasferire.Enabled = False
            End If
        Else
            tab_admin.Visible = False
            btnCercaGruppi.Visible = False
            btnNegaTrasferimento.Visible = False
            btnAccettaTrasferimento.Visible = False
        End If
    End Sub

    Protected Sub listTrasferimentiDaEffettuare_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listTrasferimentiDaEffettuare.ItemDataBound
        Dim id_status As Label = e.Item.FindControl("id_status")
        Dim btnCheckOut As Button = e.Item.FindControl("btnCheckOut")
        Dim btnVediCheck As Button = e.Item.FindControl("btnVediCheck")

        If id_status.Text = Costanti.id_trasferimento_accettato Then
            If permesso_admin.Text <> "1" Then
                'L'OPERATORE ADMIN CONFERMA I TRASFERIMENTI E PUO' VEDERE I TRASFERIMENTI DI TUTTE LE STAZIONE MA NON LI PUO' EFFETTUARE 
                '(E' UN OPERATORE DI SEDE)
                btnCheckOut.Visible = False
            Else
                btnCheckOut.Visible = True
            End If

            btnVediCheck.Visible = False
        Else
            btnCheckOut.Visible = False
            btnVediCheck.Visible = True
        End If
    End Sub

    Protected Sub listTrasferimentiVersoStazione_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listTrasferimentiVersoStazione.ItemDataBound
        Dim id_status As Label = e.Item.FindControl("id_status")
        Dim btnCheckIn As Button = e.Item.FindControl("btnCheckIn")
        Dim btnVediCheck As Button = e.Item.FindControl("btnVediCheck")

        If id_status.Text = Costanti.id_trasferimento_in_corso Then
            btnCheckIn.Visible = True
            btnVediCheck.Visible = False
        ElseIf id_status.Text = Costanti.id_trasferimento_eseguito Then
            btnCheckIn.Visible = False
            btnVediCheck.Visible = True
        End If
    End Sub

    Protected Sub listTrasferimentiVersoStazione_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listTrasferimentiVersoStazione.ItemCommand
        If e.CommandName = "check_in" Then
            Dim idLabel As Label = e.Item.FindControl("idLabel")
            Dim gruppo_da_trasferire As Label = e.Item.FindControl("gruppo_da_trasferire")
            Dim id_veicolo As Label = e.Item.FindControl("id_veicolo")
            Dim targa As Label = e.Item.FindControl("targa")
            Dim lblModello As Label = e.Item.FindControl("lblModello")
            Dim id_conducente As Label = e.Item.FindControl("id_conducente")
            Dim km_uscita As Label = e.Item.FindControl("km_uscita")
            Dim litri_uscita As Label = e.Item.FindControl("litri_uscita")
            Dim data_presunto_rientro As Label = e.Item.FindControl("data_presunto_rientro")
            Dim litri_max As Label = e.Item.FindControl("litri_max")

            id_modifica.Text = idLabel.Text
            idVeicoloSelezionatoCheckIn.Text = id_veicolo.Text
            txtTargaCheckIn.Text = targa.Text
            txtGruppoCheckIn.Text = gruppo_da_trasferire.Text
            txtModelloCheckIn.Text = lblModello.Text
            txtKmUscitaCheckIn.Text = km_uscita.Text
            txtSerbatoioUscitaCheckIn.Text = litri_uscita.Text
            txtSerbatoioRientroCheckIn.Text = ""
            txtKmRientroCheckIn.Text = ""
            dropConducenteCheckIn.SelectedValue = id_conducente.Text
            txtPresuntoRientroCheckIn.Text = Day(data_presunto_rientro.Text) & "/" & Month(data_presunto_rientro.Text) & "/" & Year(data_presunto_rientro.Text)
            txtOraPresuntoRientroCheckIn.Text = Hour(data_presunto_rientro.Text)
            lblSerbatoioMaxRientroCheckIn.Text = litri_max.Text
            lblSerbatoioMaxUscitaCheckIn.Text = litri_max.Text

            txtDataRientroCheckIn.Text = ""
            txtOraRientroCheckIn.Text = ""

            btnCheckIn.Text = "Check In"
            btnStampaFoglioTrasferimentoCheckIn.Visible = False

            div_ricerca_check_in.Visible = False
            div_check_in.Visible = True
        ElseIf e.CommandName = "vedi_check_out" Then
            Dim idLabel As Label = e.Item.FindControl("idLabel")
            Dim gruppo_da_trasferire As Label = e.Item.FindControl("gruppo_da_trasferire")
            Dim id_veicolo As Label = e.Item.FindControl("id_veicolo")
            Dim targa As Label = e.Item.FindControl("targa")
            Dim lblModello As Label = e.Item.FindControl("lblModello")
            Dim id_conducente As Label = e.Item.FindControl("id_conducente")
            Dim km_uscita As Label = e.Item.FindControl("km_uscita")
            Dim km_rientro As Label = e.Item.FindControl("km_rientro")
            Dim litri_uscita As Label = e.Item.FindControl("litri_uscita")
            Dim litri_rientro As Label = e.Item.FindControl("litri_rientro")
            Dim data_presunto_rientro As Label = e.Item.FindControl("data_presunto_rientro")
            Dim data_rientro As Label = e.Item.FindControl("data_rientro")
            Dim litri_max As Label = e.Item.FindControl("litri_max")

            id_modifica.Text = idLabel.Text
            idVeicoloSelezionatoCheckIn.Text = id_veicolo.Text
            txtTargaCheckIn.Text = targa.Text
            txtGruppoCheckIn.Text = gruppo_da_trasferire.Text
            txtModelloCheckIn.Text = lblModello.Text
            txtKmUscitaCheckIn.Text = km_uscita.Text
            txtKmRientroCheckIn.Text = km_rientro.Text
            txtSerbatoioUscitaCheckIn.Text = litri_uscita.Text
            txtSerbatoioRientroCheckIn.Text = litri_rientro.Text
            dropConducenteCheckIn.SelectedValue = id_conducente.Text
            txtPresuntoRientroCheckIn.Text = Day(data_presunto_rientro.Text) & "/" & Month(data_presunto_rientro.Text) & "/" & Year(data_presunto_rientro.Text)
            txtOraPresuntoRientroCheckIn.Text = Hour(data_presunto_rientro.Text)
            txtDataRientroCheckIn.Text = Day(data_rientro.Text) & "/" & Month(data_rientro.Text) & "/" & Year(data_rientro.Text)
            txtOraRientroCheckIn.Text = Hour(data_rientro.Text) & "." & Minute(data_rientro.Text)
            lblSerbatoioMaxRientroCheckIn.Text = litri_max.Text
            lblSerbatoioMaxUscitaCheckIn.Text = litri_max.Text

            btnCheckIn.Text = "Vedi Check"

            btnStampaFoglioTrasferimentoCheckIn.Visible = True

            div_ricerca_check_in.Visible = False
            div_check_in.Visible = True
        ElseIf e.CommandName = "order_by_stazione_pick_up" Then
            If lblOrderBy3.Text = " ORDER BY stazione_pick_up DESC" Then
                ricerca_trasf_in_ingresso(" ORDER BY stazione_pick_up ASC")
            ElseIf lblOrderBy3.Text = " ORDER BY stazione_pick_up ASC" Then
                ricerca_trasf_in_ingresso(" ORDER BY stazione_pick_up DESC")
            Else
                ricerca_trasf_in_ingresso(" ORDER BY stazione_pick_up ASC")
            End If
        ElseIf e.CommandName = "order_by_gruppo" Then
            If lblOrderBy3.Text = " ORDER BY gruppo_da_trasferire DESC" Then
                ricerca_trasf_in_ingresso(" ORDER BY gruppo_da_trasferire ASC")
            ElseIf lblOrderBy3.Text = " ORDER BY gruppo_da_trasferire ASC" Then
                ricerca_trasf_in_ingresso(" ORDER BY gruppo_da_trasferire DESC")
            Else
                ricerca_trasf_in_ingresso(" ORDER BY gruppo_da_trasferire ASC")
            End If
        ElseIf e.CommandName = "order_by_data_uscita" Then
            If lblOrderBy3.Text = " ORDER BY data_uscita DESC" Then
                ricerca_trasf_in_ingresso(" ORDER BY data_uscita ASC")
            ElseIf lblOrderBy3.Text = " ORDER BY data_uscita ASC" Then
                ricerca_trasf_in_ingresso(" ORDER BY data_uscita DESC")
            Else
                ricerca_trasf_in_ingresso(" ORDER BY data_uscita ASC")
            End If
        ElseIf e.CommandName = "order_by_status" Then
            If lblOrderBy3.Text = " ORDER BY status DESC" Then
                ricerca_trasf_in_ingresso(" ORDER BY status ASC")
            ElseIf lblOrderBy3.Text = " ORDER BY status ASC" Then
                ricerca_trasf_in_ingresso(" ORDER BY status DESC")
            Else
                ricerca_trasf_in_ingresso(" ORDER BY status ASC")
            End If
        ElseIf e.CommandName = "order_by_richiesta_per_stazione" Then
            If lblOrderBy3.Text = " ORDER BY richiesta_per_stazione DESC" Then
                ricerca_trasf_in_ingresso(" ORDER BY richiesta_per_stazione ASC")
            ElseIf lblOrderBy3.Text = " ORDER BY richiesta_per_stazione ASC" Then
                ricerca_trasf_in_ingresso(" ORDER BY richiesta_per_stazione DESC")
            Else
                ricerca_trasf_in_ingresso(" ORDER BY richiesta_per_stazione ASC")
            End If
        ElseIf e.CommandName = "order_by_targa" Then
            If lblOrderBy3.Text = " ORDER BY trasferimenti.targa DESC" Then
                ricerca_trasf_in_ingresso(" ORDER BY trasferimenti.targa ASC")
            ElseIf lblOrderBy3.Text = " ORDER BY trasferimenti.targa ASC" Then
                ricerca_trasf_in_ingresso(" ORDER BY trasferimenti.targa DESC")
            Else
                ricerca_trasf_in_ingresso(" ORDER BY trasferimenti.targa ASC")
            End If
        ElseIf e.CommandName = "order_by_modello" Then
            If lblOrderBy3.Text = " ORDER BY modello DESC" Then
                ricerca_trasf_in_ingresso(" ORDER BY modello ASC")
            ElseIf lblOrderBy3.Text = " ORDER BY modello ASC" Then
                ricerca_trasf_in_ingresso(" ORDER BY modello DESC")
            Else
                ricerca_trasf_in_ingresso(" ORDER BY modello ASC")
            End If
        ElseIf e.CommandName = "order_by_presunto_arrivo" Then
            If lblOrderBy3.Text = " ORDER BY data_presunto_rientro DESC" Then
                ricerca_trasf_in_ingresso(" ORDER BY data_presunto_rientro ASC")
            ElseIf lblOrderBy3.Text = " ORDER BY data_presunto_rientro ASC" Then
                ricerca_trasf_in_ingresso(" ORDER BY data_presunto_rientro DESC")
            Else
                ricerca_trasf_in_ingresso(" ORDER BY data_presunto_rientro ASC")
            End If
        End If
    End Sub

    Protected Sub listTrasferimentiDaEffettuare_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listTrasferimentiDaEffettuare.ItemCommand
        If e.CommandName = "check_out" Then
            Dim gruppo_da_trasferire As Label = e.Item.FindControl("gruppo_da_trasferire")
            lblGruppoDaTrasferire.Text = gruppo_da_trasferire.Text
            Dim id_gruppo_da_trasferire As Label = e.Item.FindControl("id_gruppo_da_trasferire")
            idGruppoDaTrasferire.Text = id_gruppo_da_trasferire.Text
            Dim idLabel As Label = e.Item.FindControl("idLabel")
            Dim id_status As Label = e.Item.FindControl("id_status")
            Dim richiesta_per_id_stazione As Label = e.Item.FindControl("richiesta_per_id_stazione")

            idStazioneRichiesta.Text = richiesta_per_id_stazione.Text

            id_modifica.Text = idLabel.Text

            idVeicoloSelezionato.Text = ""
            btnScegliTarga.Text = "Scegli"
            btnScegliTarga.Visible = True

            txtTarga.Text = ""
            txtTarga.ReadOnly = False
            txtGruppo.Text = ""
            txtModello.Text = ""
            txtKm.Text = ""
            txtKm.ReadOnly = False
            txtSerbatoio.Text = ""
            txtSerbatoio.ReadOnly = False

            riga_rientro_veicolo.Visible = False
            txtKmRientro.Text = ""
            txtSerbatoioRientro.Text = ""

            btnCheckOut.Visible = True
            btnCheckIn.Visible = False

            riga_rientro_veicolo.Visible = False

            btnCheckOut.Text = "Salva - Check Out"

            btnStampaFoglioTrasferimento.Visible = False

            div_trasferimenti_stazione.Visible = False
            div_check_veicolo.Visible = True

            id_stato_attuale.Text = id_status.Text
        ElseIf e.CommandName = "vedi_check_out" Then
            Dim id_status As Label = e.Item.FindControl("id_status")
            Dim idLabel As Label = e.Item.FindControl("idLabel")
            Dim gruppo_da_trasferire As Label = e.Item.FindControl("gruppo_da_trasferire")
            Dim id_veicolo As Label = e.Item.FindControl("id_veicolo")
            Dim targa As Label = e.Item.FindControl("targa")
            Dim lblModello As Label = e.Item.FindControl("lblModello")
            Dim id_conducente As Label = e.Item.FindControl("id_conducente")
            Dim km_uscita As Label = e.Item.FindControl("km_uscita")
            Dim km_rientro As Label = e.Item.FindControl("km_rientro")
            Dim litri_uscita As Label = e.Item.FindControl("litri_uscita")
            Dim litri_rientro As Label = e.Item.FindControl("litri_rientro")
            Dim data_presunto_rientro As Label = e.Item.FindControl("data_presunto_rientro")
            Dim data_rientro As Label = e.Item.FindControl("data_rientro")
            Dim litri_max As Label = e.Item.FindControl("litri_max")

            id_modifica.Text = idLabel.Text
            idVeicoloSelezionato.Text = id_veicolo.Text
            txtTarga.Text = targa.Text
            txtGruppo.Text = gruppo_da_trasferire.Text
            txtModello.Text = lblModello.Text
            txtKm.Text = km_uscita.Text
            txtKmRientro.Text = km_rientro.Text
            txtSerbatoio.Text = litri_uscita.Text
            txtSerbatoioRientro.Text = litri_rientro.Text
            dropDrivers.SelectedValue = id_conducente.Text
            txtPresuntoRientro.Text = Day(data_presunto_rientro.Text) & "/" & Month(data_presunto_rientro.Text) & "/" & Year(data_presunto_rientro.Text)
            txtPresuntoRientroOra.Text = Hour(data_presunto_rientro.Text)
            lblSerbatoioMax.Text = litri_max.Text

            If id_status.Text = Costanti.id_trasferimento_eseguito Then
                riga_rientro_veicolo.Visible = True
                txtDataRientro.Text = Day(data_rientro.Text) & "/" & Month(data_rientro.Text) & "/" & Year(data_rientro.Text)
                txtOraRientro.Text = Hour(data_rientro.Text) & "." & Minute(data_rientro.Text)
                lblSerbatoioMaxRientro.Text = litri_max.Text
            Else
                riga_rientro_veicolo.Visible = False
                txtDataRientro.Text = ""
                txtOraRientro.Text = ""
                lblSerbatoioMaxRientro.Text = ""
            End If

            id_stato_attuale.Text = id_status.Text

            btnCheckOut.Text = "Vedi Check"
            btnStampaFoglioTrasferimento.Visible = True

            txtTarga.ReadOnly = True
            btnScegliTarga.Visible = False
            dropDrivers.Enabled = False
            txtPresuntoRientro.Enabled = False
            txtPresuntoRientroOra.ReadOnly = True

            div_trasferimenti_stazione.Visible = False
            div_check_veicolo.Visible = True
        ElseIf e.CommandName = "order_by_stazione_pick_up" Then
            If lblOrderBy2.Text = " ORDER BY stazione_pick_up DESC" Then
                ricerca_trasf_staz(" ORDER BY stazione_pick_up ASC")
            ElseIf lblOrderBy2.Text = " ORDER BY stazione_pick_up ASC" Then
                ricerca_trasf_staz(" ORDER BY stazione_pick_up DESC")
            Else
                ricerca_trasf_staz(" ORDER BY stazione_pick_up ASC")
            End If
        ElseIf e.CommandName = "order_by_gruppo" Then
            If lblOrderBy2.Text = " ORDER BY gruppo_da_trasferire DESC" Then
                ricerca_trasf_staz(" ORDER BY gruppo_da_trasferire ASC")
            ElseIf lblOrderBy2.Text = " ORDER BY gruppo_da_trasferire ASC" Then
                ricerca_trasf_staz(" ORDER BY gruppo_da_trasferire DESC")
            Else
                ricerca_trasf_staz(" ORDER BY gruppo_da_trasferire ASC")
            End If
        ElseIf e.CommandName = "order_by_richiesta_per_data" Then
            If lblOrderBy2.Text = " ORDER BY richiesta_per_data DESC" Then
                ricerca_trasf_staz(" ORDER BY richiesta_per_data ASC")
            ElseIf lblOrderBy2.Text = " ORDER BY richiesta_per_data ASC" Then
                ricerca_trasf_staz(" ORDER BY richiesta_per_data DESC")
            Else
                ricerca_trasf_staz(" ORDER BY richiesta_per_data ASC")
            End If
        ElseIf e.CommandName = "order_by_status" Then
            If lblOrderBy2.Text = " ORDER BY status DESC" Then
                ricerca_trasf_staz(" ORDER BY status ASC")
            ElseIf lblOrderBy2.Text = " ORDER BY status ASC" Then
                ricerca_trasf_staz(" ORDER BY status DESC")
            Else
                ricerca_trasf_staz(" ORDER BY status ASC")
            End If
        ElseIf e.CommandName = "order_by_richiesta_per_stazione" Then
            If lblOrderBy2.Text = " ORDER BY richiesta_per_stazione DESC" Then
                ricerca_trasf_staz(" ORDER BY richiesta_per_stazione ASC")
            ElseIf lblOrderBy2.Text = " ORDER BY richiesta_per_stazione ASC" Then
                ricerca_trasf_staz(" ORDER BY richiesta_per_stazione DESC")
            Else
                ricerca_trasf_staz(" ORDER BY richiesta_per_stazione ASC")
            End If
        ElseIf e.CommandName = "order_by_targa" Then
            If lblOrderBy2.Text = " ORDER BY trasferimenti.targa DESC" Then
                ricerca_trasf_staz(" ORDER BY trasferimenti.targa ASC")
            ElseIf lblOrderBy2.Text = " ORDER BY trasferimenti.targa ASC" Then
                ricerca_trasf_staz(" ORDER BY trasferimenti.targa DESC")
            Else
                ricerca_trasf_staz(" ORDER BY trasferimenti.targa ASC")
            End If
        ElseIf e.CommandName = "order_by_modello" Then
            If lblOrderBy2.Text = " ORDER BY modello DESC" Then
                ricerca_trasf_staz(" ORDER BY modello ASC")
            ElseIf lblOrderBy2.Text = " ORDER BY modello ASC" Then
                ricerca_trasf_staz(" ORDER BY modello DESC")
            Else
                ricerca_trasf_staz(" ORDER BY modello ASC")
            End If
        End If
    End Sub



    Protected Sub listRichieste_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listRichieste.ItemCommand
        If e.CommandName = "vedi" Then
            'NEL CASO DI UTENTE SENZA PERMESSO ADMIN - L'UNICA AZIONE POSSIBILE E' L'ANNULLAMENTO DELLA RICHIESTA (DISPONIBILE SOLO NELLO
            'STATO DI RICHIESTA)
            Dim id_gruppo_richiesto As Label = e.Item.FindControl("id_gruppo_richiesto")
            Dim richiesta_per_id_stazione As Label = e.Item.FindControl("richiesta_per_id_stazione")
            Dim richiesta_per_data As Label = e.Item.FindControl("richiesta_per_data")
            Dim riferimento As Label = e.Item.FindControl("riferimento")
            Dim idLabel As Label = e.Item.FindControl("idLabel")
            Dim note_admin As Label = e.Item.FindControl("note_admin")
            Dim id_status As Label = e.Item.FindControl("id_status")
            Dim status As Label = e.Item.FindControl("status")
            Dim stazione_pick_up As Label = e.Item.FindControl("stazione_pick_up")
            Dim gruppo_da_trasferire As Label = e.Item.FindControl("gruppo_da_trasferire")

            dropGruppoRichiesto.SelectedValue = id_gruppo_richiesto.Text
            dropStazioneRichiesta.SelectedValue = richiesta_per_id_stazione.Text
            txtRiferimento.Text = riferimento.Text
            txtRichiestaPerData.Text = Day(richiesta_per_data.Text) & "/" & Month(richiesta_per_data.Text) & "/" & Year(richiesta_per_data.Text)
            If note_admin.Text <> "" Then
                Dim operatore_admin As Label = e.Item.FindControl("operatore_admin")
                Dim data_gestione_admin As Label = e.Item.FindControl("data_gestione_admin")
                lblNoteAdminXStazione.Text = data_gestione_admin.Text & " - " & operatore_admin.Text & " - " & note_admin.Text
            Else
                lblNoteAdminXStazione.Text = ""
            End If
            txtRichiestaPerOra.Text = Hour(richiesta_per_data.Text)

            lblGruppoPickUp.Text = gruppo_da_trasferire.Text
            lblStazionePickUp.Text = stazione_pick_up.Text

            If lblStazionePickUp.Text <> "" Then
                lblInfoStazionePick.Text = "Stazione Pick Up"
                lblInfoGruppoPick.Text = "Gruppo da trasferire"
            Else
                lblInfoStazionePick.Text = ""
                lblInfoGruppoPick.Text = ""
            End If

            lblStato.Text = status.Text
            lblStato2.Text = status.Text

            id_modifica.Text = idLabel.Text

            If permesso_admin.Text = "1" Or id_status.Text <> Costanti.id_trasferimento_richiesta Then
                abilitazione_campi_richiesta(False, True, False, id_status.Text)
                div_ricerca.Visible = False
                div_dati.Visible = True
                div_trasferimenti_stazione.Visible = False
            Else
                abilitazione_campi_richiesta(False, False, True, id_status.Text)
                div_ricerca.Visible = False
                div_dati.Visible = True
                div_trasferimenti_stazione.Visible = False

                elimina_righe_appoggio()
            End If
        ElseIf e.CommandName = "order_by_gruppo" Then
            If lblOrderBY.Text = " ORDER BY gruppo_richiesto DESC" Then
                ricerca(" ORDER BY gruppo_richiesto ASC")
            ElseIf lblOrderBY.Text = " ORDER BY gruppo_richiesto ASC" Then
                ricerca(" ORDER BY gruppo_richiesto DESC")
            Else
                ricerca(" ORDER BY gruppo_richiesto ASC")
            End If
        ElseIf e.CommandName = "order_by_status" Then
            If lblOrderBY.Text = " ORDER BY status DESC" Then
                ricerca(" ORDER BY status ASC")
            ElseIf lblOrderBY.Text = " ORDER BY status ASC" Then
                ricerca(" ORDER BY status DESC")
            Else
                ricerca(" ORDER BY status ASC")
            End If
        ElseIf e.CommandName = "order_by_richiesta_per_stazione" Then
            If lblOrderBY.Text = " ORDER BY richiesta_per_stazione DESC" Then
                ricerca(" ORDER BY richiesta_per_stazione ASC")
            ElseIf lblOrderBY.Text = " ORDER BY richiesta_per_stazione ASC" Then
                ricerca(" ORDER BY richiesta_per_stazione DESC")
            Else
                ricerca(" ORDER BY richiesta_per_stazione ASC")
            End If
        ElseIf e.CommandName = "order_by_richiesta_per_data" Then
            If lblOrderBY.Text = " ORDER BY richiesta_per_data DESC" Then
                ricerca(" ORDER BY richiesta_per_data ASC")
            ElseIf lblOrderBY.Text = " ORDER BY richiesta_per_data ASC" Then
                ricerca(" ORDER BY richiesta_per_data DESC")
            Else
                ricerca(" ORDER BY richiesta_per_data ASC")
            End If
        ElseIf e.CommandName = "order_by_operatore_richiesta" Then
            If lblOrderBY.Text = " ORDER BY operatore_richiesta DESC" Then
                ricerca(" ORDER BY operatore_richiesta ASC")
            ElseIf lblOrderBY.Text = " ORDER BY operatore_richiesta ASC" Then
                ricerca(" ORDER BY operatore_richiesta DESC")
            Else
                ricerca(" ORDER BY operatore_richiesta ASC")
            End If
        ElseIf e.CommandName = "order_by_riferimento" Then
            If lblOrderBY.Text = " ORDER BY riferimento DESC" Then
                ricerca(" ORDER BY riferimento ASC")
            ElseIf lblOrderBY.Text = " ORDER BY riferimento ASC" Then
                ricerca(" ORDER BY riferimento DESC")
            Else
                ricerca(" ORDER BY riferimento ASC")
            End If
        ElseIf e.CommandName = "order_by_data_richiesta" Then
            If lblOrderBY.Text = " ORDER BY data_richiesta DESC" Then
                ricerca(" ORDER BY data_richiesta ASC")
            ElseIf lblOrderBY.Text = " ORDER BY data_richiesta ASC" Then
                ricerca(" ORDER BY data_richiesta DESC")
            Else
                ricerca(" ORDER BY data_richiesta ASC")
            End If
        ElseIf e.CommandName = "order_by_stazione_pick_up" Then
            If lblOrderBY.Text = " ORDER BY stazione_pick_up DESC" Then
                ricerca(" ORDER BY stazione_pick_up ASC")
            ElseIf lblOrderBY.Text = " ORDER BY stazione_pick_up ASC" Then
                ricerca(" ORDER BY stazione_pick_up DESC")
            Else
                ricerca(" ORDER BY stazione_pick_up ASC")
            End If
        ElseIf e.CommandName = "order_by_data_gestione_admin" Then
            If lblOrderBY.Text = " ORDER BY data_gestione_admin DESC" Then
                ricerca(" ORDER BY data_gestione_admin ASC")
            ElseIf lblOrderBY.Text = " ORDER BY data_gestione_admin ASC" Then
                ricerca(" ORDER BY data_gestione_admin DESC")
            Else
                ricerca(" ORDER BY data_gestione_admin ASC")
            End If
        ElseIf e.CommandName = "order_by_operatore_admin" Then
            If lblOrderBY.Text = " ORDER BY operatore_admin DESC" Then
                ricerca(" ORDER BY operatore_admin ASC")
            ElseIf lblOrderBY.Text = " ORDER BY operatore_admin ASC" Then
                ricerca(" ORDER BY operatore_admin DESC")
            Else
                ricerca(" ORDER BY operatore_admin ASC")
            End If
        ElseIf e.CommandName = "order_by_gruppo_da_trasferire" Then
            If lblOrderBY.Text = " ORDER BY gruppo_da_trasferire DESC" Then
                ricerca(" ORDER BY gruppo_da_trasferire ASC")
            ElseIf lblOrderBY.Text = " ORDER BY gruppo_da_trasferire ASC" Then
                ricerca(" ORDER BY gruppo_da_trasferire DESC")
            Else
                ricerca(" ORDER BY gruppo_da_trasferire ASC")
            End If
        End If
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerca.Click
        ricerca(lblOrderBY.Text)
    End Sub

    Protected Sub listRichieste_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listRichieste.ItemDataBound
        Dim id_status As Label = e.Item.FindControl("id_status")

        'If id_status.Text <> Costanti.id_trasferimento_richiesta And id_status.Text <> Costanti.id_trasferimento_negato Then
        Dim vedi As ImageButton = e.Item.FindControl("vedi")

        '    vedi.Visible = False
        'End If
    End Sub

    Protected Sub btnAnnullaRichiesta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnullaRichiesta.Click

        Try
            If Trim(txtMotivoAnnullamento.Text) <> "" Then
                If getStatus(id_modifica.Text) = Costanti.id_trasferimento_richiesta Then
                    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()

                    sqla = "UPDATE trasferimenti SET status='" & Costanti.id_trasferimento_annullato & "'," &
                    "id_operatore_annullamento_richiesta='" & Request.Cookies("SicilyRentCar")("idUtente") & "', " &
                    "data_annullamento_richiesta=convert(datetime,GetDate(),102) WHERE id='" & id_modifica.Text & "'"

                    Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
                    Cmd.ExecuteNonQuery()

                    Libreria.genUserMsgBox(Me, "Richiesta di trasferimento annullata correttamente.")

                    annulla()

                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing
                Else
                    Libreria.genUserMsgBox(Me, "Attenzione: non è più possibile annullare la richiesta di trasferimento selezionata.")
                    annulla()
                End If
            Else
                Libreria.genUserMsgBox(Me, "Specificare il motivo dell'annullamento della richiesta di trasferimento.")
            End If
        Catch ex As Exception
            Response.Write("error_btnAnnullaRichiesta_:" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try



    End Sub

    Protected Sub btnNegaTrasferimento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNegaTrasferimento.Click
        Try

            If getStatus(id_modifica.Text) = Costanti.id_trasferimento_richiesta Then

                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                sqla = "UPDATE trasferimenti SET status='" & Costanti.id_trasferimento_negato & "'," &
                "id_operatore_admin='" & Request.Cookies("SicilyRentCar")("idUtente") & "', " &
                "data_gestione_admin=convert(datetime,GetDate(),102), note_admin='" & Replace(txtNoteAdmin.Text, "'", "''") & "' WHERE id='" & id_modifica.Text & "'"

                Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
                Cmd.ExecuteNonQuery()

                Libreria.genUserMsgBox(Me, "Richiesta di trasferimento annullata correttamente.")

                annulla()

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing


            Else
                Libreria.genUserMsgBox(Me, "Attenzione: non è più possibile modificare lo stato della richiesta di trasferimento selezionata.")
            End If


        Catch ex As Exception
            Response.Write("error btn_NegaTrasferimento :" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try




    End Sub

    Protected Function getStatus(ByVal id_trasferimento As String) As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            sqla = "SELECT status FROM trasferimenti WITH(NOLOCK) WHERE id='" & id_trasferimento & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            getStatus = Cmd.ExecuteScalar()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_GetStatus_:" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function

    Protected Function get_distanza_stazioni(ByVal stazione As String, ByVal stazione_pick As String) As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            sqla = "SELECT ISNULL(distanza,0) FROM stazioni_distanza WHERE (id_stazione1='" & stazione & "' AND id_stazione2='" & stazione_pick & "') OR (id_stazione1='" & stazione_pick & "' AND id_stazione2='" & stazione & "') "

            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            get_distanza_stazioni = Cmd.ExecuteScalar()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_get_distanza_stazioni_:" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function

    Protected Sub cercaGruppi()
        Try
            Dim num_gruppi As Integer = listGruppiPickSelezionati.Items.Count()
            Dim num_stazioni As Integer = listStazioniPickSelezionate.Items.Count()

            If num_gruppi <> 0 Then
                If num_stazioni <> 0 Then
                    'ORDINO I GRUPPI (IN MODO DA FARLI COMBACIARE CON QUELLI SELEZIONATI NELLE PROCEDURE DI RICERCA)
                    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()
                    Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
                    Dim sqlStr As String

                    Dim gruppi(num_gruppi) As String
                    Dim cod_gruppi(num_gruppi) As String
                    Dim valoreInizialeGruppi(num_gruppi) As Integer

                    Dim i As Integer
                    Dim k As Integer

                    Dim tot_12 As Integer
                    Dim tot_16 As Integer
                    Dim tot_20 As Integer
                    Dim tot_24 As Integer


                    For i = 0 To num_gruppi - 1
                        gruppi(i) = listGruppiPickSelezionati.Items(i).Value
                        cod_gruppi(i) = listGruppiPickSelezionati.Items(i).Text
                        valoreInizialeGruppi(i) = 0
                    Next

                    Dim situazione(i * 15) As Integer

                    gruppi(num_gruppi) = "000"
                    cod_gruppi(num_gruppi) = "000"

                    Dim data_ricerca As DateTime = txtRichiestaPerData.Text & " 00:00:00"
                    Dim dataOdierna As DateTime = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()) & " 00:00:00"
                    Dim stazione As Integer
                    Dim distanza As Integer

                    'SELEZIONO LA SITUAZIONE PER IL GIORNO DEI GRUPPI RICHIESTI PER OGNI STAZIONE
                    For j = 0 To num_stazioni - 1
                        stazione = listStazioniPickSelezionate.Items(j).Value
                        distanza = get_distanza_stazioni(dropStazioneRichiesta.SelectedValue, stazione)

                        'SELEZIONO LA DISPONIBILITA' INIZIALE PER OGNI GRUPPO SELEZIONATO DAL GIORNO ODIERNO FINO AL GIORNO CONSIDERATO

                        Dim giorni As Integer = DateDiff(DateInterval.Day, dataOdierna, data_ricerca)
                        valoreInizialeGruppi = funzioni_comuni.getDisponibilitaOdierna(dataOdierna, stazione, cod_gruppi)

                        Do While giorni > 0
                            i = 0
                            k = 1

                            situazione = funzioni_comuni.getSituazione_x_gruppi(dataOdierna, stazione, cod_gruppi)

                            Do While gruppi(i) <> "000"
                                valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 14) + situazione(k + 9) - situazione(k + 4)

                                'Response.Write(valoreInizialeGruppi(i))

                                k = k + 15
                                i = i + 1
                            Loop

                            dataOdierna = dataOdierna.AddDays(1)
                            giorni = DateDiff(DateInterval.Day, dataOdierna, data_ricerca)
                        Loop
                        'QUINDI SELEZIONO LA SITUAZIONE DELLA FLOTTA PER LA GIORNATA CORRENTE.    
                        situazione = funzioni_comuni.getSituazione_x_gruppi(data_ricerca, stazione, cod_gruppi)

                        'REGISTRAZIONE SU DB DEL RISULTATO PER LA STAZIONE SPECIFICATA - CONSIDERARE CHE, ESSENDO LO STESSO MOTORE DELLA
                        'FUNZIONALITA' situazione_flotta_gruppi.aspx I GRUPPI SONO INCOLONNATI A GRUPPI DI 4
                        Dim nuovo_gruppo As Boolean = True
                        i = 0
                        k = 1

                        Dim primo As Boolean = True

                        sqla = "INSERT INTO trasferimenti_appoggio (id_operatore, id_trasferimenti, id_gruppo, id_stazione, distanza, valore_iniziale," &
                    "pru_12, pru_16, pru_20, pru_24, pru_tot, rp_12, rp_16, rp_20, rp_24, rp_tot, rc_12, rc_16, rc_20, rc_24, rc_tot, " &
                    "tot_12, tot_16, tot_20, tot_24, tot_tot) VALUES "

                        Do While gruppi(i) <> "000"
                            If Not primo Then
                                sqla = sqla & ","

                            End If

                            tot_12 = valoreInizialeGruppi(i) + situazione(k + 10) + situazione(k + 5) - situazione(k)
                            tot_16 = tot_12 + situazione(k + 11) + situazione(k + 6) - situazione(k + 1)
                            tot_20 = tot_16 + situazione(k + 12) + situazione(k + 7) - situazione(k + 2)
                            tot_24 = tot_20 + situazione(k + 13) + situazione(k + 8) - situazione(k + 3)

                            sqla += "('" & Request.Cookies("SicilyRentCar")("idUtente") & "','" & id_modifica.Text & "'," &
                        "'" & gruppi(i) & "','" & stazione & "','" & distanza & "','" & valoreInizialeGruppi(i) & "'," &
                        "'" & situazione(k) & "','" & situazione(k + 1) & "','" & situazione(k + 2) & "','" & situazione(k + 3) & "'," &
                        "'" & situazione(k + 4) & "','" & situazione(k + 5) & "','" & situazione(k + 6) & "','" & situazione(k + 7) & "'," &
                        "'" & situazione(k + 8) & "','" & situazione(k + 9) & "','" & situazione(k + 10) & "','" & situazione(k + 11) & "'," &
                        "'" & situazione(k + 12) & "','" & situazione(k + 13) & "','" & situazione(k + 14) & "'," &
                        "'" & tot_12 & "','" & tot_16 & "','" & tot_20 & "','" & tot_24 & "','" & tot_24 & "')"

                            primo = False
                            i = i + 1
                            k = k + 15
                        Loop
                        Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                        Cmd.ExecuteNonQuery()

                    Next

                    Cmd.Dispose()
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing
                Else
                    Libreria.genUserMsgBox(Me, "Specificare almeno una stazione.")
                End If
            Else
                Libreria.genUserMsgBox(Me, "Specificare almeno un gruppo da ricercare.")
            End If

        Catch ex As Exception
            Response.Write("error cercaGruppi :" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try




    End Sub

    Protected Sub elimina_righe_appoggio()
        Try
            sqla = "DELETE FROM trasferimenti_appoggio WHERE id_operatore='" & id_operatore.Text & "' AND id_trasferimenti='" & id_modifica.Text & "'"
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteScalar()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error _eliminarigheappoggio_ :" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Protected Sub btnCercaGruppi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCercaGruppi.Click
        Try
            elimina_righe_appoggio()
            cercaGruppi()
            listTrasferimentiAppoggio.DataBind()
            sqlTrasferimentiAppoggio.DataBind()
        Catch ex As Exception
            Response.Write("error_btnCercaGruppiclick_:" & ex.Message & "<br/>")
        End Try

    End Sub


    Protected Sub btnAccettaTrasferimento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccettaTrasferimento.Click
        Try
            If dropStazionePickUp.SelectedValue <> "0" Then
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                sqla = "UPDATE trasferimenti SET status='" & Costanti.id_trasferimento_accettato & "'," &
                "id_operatore_admin='" & Request.Cookies("SicilyRentCar")("idUtente") & "', " &
                "id_stazione_uscita='" & dropStazionePickUp.SelectedValue & "', id_gruppo_da_trasferire='" & dropGruppoDaTrasferire.SelectedValue & "'," &
                "data_gestione_admin=convert(datetime,GetDate(),102), note_admin='" & Replace(txtNoteAdmin.Text, "'", "''") & "' WHERE id='" & id_modifica.Text & "'"

                Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
                Cmd.ExecuteNonQuery()

                Libreria.genUserMsgBox(Me, "Richiesta di trasferimento accettata correttamente.")

                elimina_righe_appoggio()
                listTrasferimentiAppoggio.DataBind()

                annulla()

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            Else
                Libreria.genUserMsgBox(Me, "Specificare quale gruppo trasferire e da quale stazione.")
            End If
        Catch ex As Exception
            Response.Write("error_ btnAccettaTrasferimentoClick _:" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Protected Function get_num_trasferimento(ByVal stazione As String) As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            sqla = "SELECT ISNULL(MAX(num_trasferimento)," & stazione & Right(Year(Now()), 2) & "00000) FROM trasferimenti WITH(NOLOCK) WHERE id_stazione_uscita='" & stazione & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)


            get_num_trasferimento = Cmd.ExecuteScalar + 1

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_ getNumTrasferimento _:" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function


    Protected Sub check_out()

        'QUI ESEGUIRE L'USCITA DEL VEICOLO

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim num_trasferimento As String = get_num_trasferimento(Request.Cookies("SicilyRentCar")("stazione"))
            Dim presunto_rientro As String = funzioni_comuni.GetDataSql(txtPresuntoRientro.Text, 99) & " " & txtPresuntoRientroOra.Text & ":00:00"

            'REGISTRAZIONE DEL MOVIMENTO DI NOLO IN CORSO PER IL VEICOLO -----------------------------------------------------------------------

            sqla = "insert into movimenti_targa (num_riferimento, id_veicolo, id_tipo_movimento, data_uscita, id_stazione_uscita, data_presunto_rientro, id_stazione_presunto_rientro, "
            sqla += " km_uscita, serbatoio_uscita, id_operatore, data_registrazione, movimento_attivo) "
            sqla += "VALUES"
            sqla += " ('" & num_trasferimento & "','" & idVeicoloSelezionato.Text & "','" & Costanti.idMovimentoInterno & "', convert(datetime,getDate(),102) ,'" & id_stazione_operatore.Text & "',"
            sqla += "convert(datetime,'" & presunto_rientro & "',102),'" & idStazioneRichiesta.Text & "',"
            sqla += "'" & txtKm.Text & "','" & txtSerbatoio.Text & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "',convert(datetime,getDate(),102),'1')"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()

            'SETTAGGIO DEL VEICOLO COME NOLEGGIATO
            sqla = "UPDATE veicoli SET disponibile_nolo='0', noleggiata='1' WHERE id='" & idVeicoloSelezionato.Text & "'"

            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()

            ''-------------------------------------------------------------------------------------------------------------------------------
            sqla = "UPDATE trasferimenti SET num_trasferimento='" & num_trasferimento & "', id_veicolo='" & idVeicoloSelezionato.Text & "', targa='" & txtTarga.Text & "', "
            sqla += "km_uscita='" & txtKm.Text & "', litri_uscita='" & txtSerbatoio.Text & "', litri_max='" & lblSerbatoioMax.Text & "', status='" & Costanti.id_trasferimento_in_corso & "', "
            sqla += "data_uscita=convert(datetime,getDate(),102), id_conducente='" & dropDrivers.SelectedValue & "', data_presunto_rientro=convert(datetime,'" & presunto_rientro & "',102), "
            sqla += "id_operatore_uscita='" & Request.Cookies("SicilyRentCar")("IdUtente") & "' "
            sqla += "WHERE id='" & id_modifica.Text & "'"

            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()

            id_stato_attuale.Text = Costanti.id_trasferimento_in_corso

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            gestione_checkin.InitFormCheckOut(tipo_documento.MovimentoInterno, Integer.Parse(id_modifica.Text), 0)
            div_edit_danno.Visible = True
            tabPanel1.Visible = False


        Catch ex As Exception
            Response.Write("error_checkout_:" & ex.Message & "<br/> " & sqla & "<br/>")
        End Try



    End Sub

    Protected Sub btnCheckOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckOut.Click
        Try
            If id_stato_attuale.Text = Costanti.id_trasferimento_accettato Then
                'IN QUESTO CASO STO SALVANDO L'USCITA DEL VEICOLO - E' NECESSARIO CONTROLLARE I CAMPI PER POTER PROCEDERE
                If idVeicoloSelezionato.Text <> "" Then
                    If dropDrivers.SelectedValue <> "0" Then
                        If txtPresuntoRientro.Text <> "" And txtPresuntoRientroOra.Text <> "" Then
                            Dim data1 As DateTime = funzioni_comuni.getDataDb_con_orario2(txtPresuntoRientro.Text & " " & txtPresuntoRientroOra.Text & ":00:00")
                            If DateDiff(DateInterval.Minute, Now(), data1) > 0 Then
                                'RICONTROLLO CHE IL VEICOLO SIA DISPONIBILE - NEL FRATTEMPO POTREBBE AVER CAMBIATO STATO
                                If check_veicolo_disponibile("1") Then
                                    check_out()

                                    dropDrivers.Enabled = False
                                    txtPresuntoRientro.Enabled = False
                                    txtPresuntoRientroOra.ReadOnly = True
                                    btnScegliTarga.Visible = False
                                    btnStampaFoglioTrasferimento.Visible = True
                                    btnCheckOut.Text = "Vedi Check"
                                Else
                                    Libreria.genUserMsgBox(Me, "Attenzione: il veicolo non è più disponibile. Selezionare un'altra vettura.")
                                End If
                            Else
                                Libreria.genUserMsgBox(Me, "Attenzione: il presunto rientro deve essere successivo all'orario attuale.")
                            End If
                        Else
                            Libreria.genUserMsgBox(Me, "Specificare data ed ora di presunto arrivo del veicolo.")
                        End If
                    Else
                        Libreria.genUserMsgBox(Me, "Specificare il conducente del veicolo.")
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Specificare un veicolo.")
                End If
            Else
                'IN QUESTO CASO L'USCITA E' GIA' STATA EFFETTUATA - SI PROCEDE CON LA STAMPA DEL CHECK OUT GIA' EFFETTUATO (NEL CASO DI TRASFERIMENTO IN CORSO)
                'O DEL CHECK OUT/CHECK IN (NEL CASO DI TRASFERIMENTO ESEGUITO)
                If id_stato_attuale.Text = Costanti.id_trasferimento_in_corso Then
                    gestione_checkin.InitFormCheckOut(tipo_documento.MovimentoInterno, Integer.Parse(id_modifica.Text), 0)
                    div_edit_danno.Visible = True
                    tabPanel1.Visible = False
                ElseIf id_stato_attuale.Text = Costanti.id_trasferimento_eseguito Then
                    gestione_checkin.InitFormCheckIn(tipo_documento.MovimentoInterno, Integer.Parse(id_modifica.Text), 0)

                    tabPanel1.Visible = False
                    div_edit_danno.Visible = True
                End If
            End If
        Catch ex As Exception
            Response.Write("error_btnCheckOut_click_:" & ex.Message & "<br/>")
        End Try

    End Sub

    Protected Sub btnCheckIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckIn.Click
        gestione_checkin.InitFormCheckIn(tipo_documento.MovimentoInterno, Integer.Parse(id_modifica.Text), 0)

        tabPanel1.Visible = False
        div_edit_danno.Visible = True
    End Sub

    Protected Sub btnChiudiCheck_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudiCheck.Click
        id_modifica.Text = ""
        pulisci_campi_scelta_veicolo()

        listTrasferimentiDaEffettuare.DataBind()
        listTrasferimentiVersoStazione.DataBind()
        listRichieste.DataBind()
        listTrasferimentiInterni.DataBind()

        div_ricerca.Visible = True
        div_dati.Visible = False
        div_trasferimenti_stazione.Visible = True
        div_check_veicolo.Visible = False
    End Sub

    Protected Function check_veicolo_disponibile(ByVal tipo As String) As Boolean
        Dim risultato(11) As String
        If tipo = "1" Then 'TRASFERIMENTO SU RICHIESTA
            risultato = funzioni_comuni.scegli_targa_x_contratto(txtTarga.Text)
        ElseIf tipo = "2" Then 'TRASFERIMENTO INTERNO/DIRETTO
            risultato = funzioni_comuni.scegli_targa_x_contratto(txtTargaInterno.Text)
        End If


        If risultato(1) <> "" Then
            check_veicolo_disponibile = False
        Else
            'ANCHE SE NON VIENE RESTITUITO UN ERRORE BLOCCANTE DEVONO ESSERE ESEGUITI ALCUNI CONTROLLI:
            '1)AUTO NON IN PARCO: DEVE ESSERE POSSIBILE EFFETTUARE L'IMMISSIONE IN PARCO
            If (idGruppoDaTrasferire.Text <> risultato(6) And tipo = "1") Then
                check_veicolo_disponibile = False
            ElseIf risultato(5) = "" Then
                check_veicolo_disponibile = False
            ElseIf ((risultato(5) <> dropStazionePickUpInterno.SelectedValue) Or tipo = 1) And (tipo = 2 Or (risultato(5) <> Request.Cookies("SicilyRentCar")("stazione"))) Then
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

    Protected Sub seleziona_targa(ByVal tipo As String)
        'SE LA TIPOLOGIA E' 1 SI AGISCE SUL TAB Trasferimenti da effettuare
        'SE LA TIPOLOGIA E' 2 SI AGISCE SUL TAB Trasferimenti Interni/Trasferimenti

        Dim risultato(11) As String

        If tipo = 1 Then
            risultato = funzioni_comuni.scegli_targa_x_contratto(txtTarga.Text)
        Else
            risultato = funzioni_comuni.scegli_targa_x_contratto(txtTargaInterno.Text)
        End If


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

        If tipo = "1" Then
            idVeicoloSelezionato.Text = ""
        ElseIf tipo = "2" Then
            idVeicoloSelezionatoInterno.Text = ""
        End If

        'SE C'E' UN MESSAGGIO DI ERRORE IN POSIZIONE 1 VUOL DIRE CHE IL VEICOLO NON E' SELEZIONABILE (MOTIVI BLOCCANTI)
        If risultato(1) <> "" Then
            Libreria.genUserMsgBox(Me, risultato(1))
        Else
            'ANCHE SE NON VIENE RESTITUITO UN ERRORE BLOCCANTE DEVONO ESSERE ESEGUITI ALCUNI CONTROLLI:
            '1)AUTO NON IN PARCO: DEVE ESSERE POSSIBILE EFFETTUARE L'IMMISSIONE IN PARCO
            If (idGruppoDaTrasferire.Text <> risultato(6) And tipo = "1") Then
                Libreria.genUserMsgBox(Me, "Gruppo auto diverso dal gruppo auto da trasferire.")
            ElseIf risultato(5) = "" Then
                Libreria.genUserMsgBox(Me, "Auto non in parco.")
            ElseIf ((risultato(5) <> dropStazionePickUpInterno.SelectedValue) Or tipo = 1) And (tipo = 2 Or (risultato(5) <> Request.Cookies("SicilyRentCar")("stazione"))) Then
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
                If tipo = "1" Then
                    idVeicoloSelezionato.Text = risultato(0)
                    txtKm.Text = risultato(4)
                    txtSerbatoio.Text = risultato(2)
                    lblSerbatoioMax.Text = risultato(3)
                    lblSerbatoioMaxRientro.Text = risultato(3)
                    txtModello.Text = risultato(7)
                    txtGruppo.Text = risultato(8)
                    'id_alimentazione.Text = risultato(9)
                    txtTarga.ReadOnly = True
                    btnScegliTarga.Text = "Modifica"
                ElseIf tipo = "2" Then
                    idVeicoloSelezionatoInterno.Text = risultato(0)
                    txtKmInterno.Text = risultato(4)
                    txtSerbatoioInterno.Text = risultato(2)
                    lblSerbatoioMaxInterno.Text = risultato(3)
                    lblSerbatoioMaxRientroInterno.Text = risultato(3)
                    txtModelloInterno.Text = risultato(7)
                    idGruppoInterno.Text = risultato(6)
                    txtGruppoInterno.Text = risultato(8)
                    'id_alimentazione.Text = risultato(9)
                    txtTargaInterno.ReadOnly = True
                    btnScegliTargaInterno.Text = "Modifica"
                End If


                Libreria.genUserMsgBox(Me, "Veicolo selezionato correttamente.")
            End If
        End If
    End Sub

    Protected Sub pulisci_campi_scelta_veicolo_check_in()
        idVeicoloSelezionatoCheckIn.Text = ""
        txtTargaCheckIn.Text = ""
        txtGruppoCheckIn.Text = ""
        txtModelloCheckIn.Text = ""
        txtKmUscitaCheckIn.Text = ""
        txtSerbatoioUscitaCheckIn.Text = ""
        txtKmRientroCheckIn.Text = ""
        txtSerbatoioRientroCheckIn.Text = ""
    End Sub

    Protected Sub pulisci_campi_scelta_veicolo()
        idVeicoloSelezionato.Text = ""
        idStazioneRichiesta.Text = ""
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
        btnStampaFoglioTrasferimento.Visible = False

        btnCheckOut.Text = "Salva - Check Out"

        lblSerbatoioMax.Text = ""
        lblSerbatoioMaxRientro.Text = ""

        dropDrivers.SelectedValue = "0"
        dropDrivers.Enabled = True
        txtPresuntoRientro.Text = ""
        txtPresuntoRientro.Enabled = True
        txtPresuntoRientroOra.Text = ""
        txtPresuntoRientroOra.ReadOnly = False
        txtDataRientro.Text = ""
        txtOraRientro.Text = ""
    End Sub

    Protected Sub btnScegliTarga_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScegliTarga.Click
        If idVeicoloSelezionato.Text = "" Then
            seleziona_targa("1")
        Else
            pulisci_campi_scelta_veicolo()
            btnScegliTarga.Text = "Scegli"
        End If
    End Sub




    Protected Sub btnChiudiCheckIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudiCheckIn.Click
        Try
            id_modifica.Text = ""
            pulisci_campi_scelta_veicolo_check_in()

            listTrasferimentiDaEffettuare.DataBind()
            listTrasferimentiVersoStazione.DataBind()
            listRichieste.DataBind()
            listTrasferimentiInterni.DataBind()

            div_ricerca_check_in.Visible = True
            div_check_in.Visible = False
        Catch ex As Exception
            Response.Write("error_btnChiudiCheckIn_Click_:" & ex.Message & "<br/>")
        End Try

    End Sub

    Protected Sub btnStampaFoglioTrasferimento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaFoglioTrasferimento.Click
        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            Session("url_print") = "/Stampe/movimenti_veicoli/foglio_trasferimento.aspx?orientamento=verticale&id_trasf=" & id_modifica.Text
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
            End If
        End If
    End Sub

    Protected Sub btnStampaFoglioTrasferimentoCheckIn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaFoglioTrasferimentoCheckIn.Click
        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            Session("url_print") = "/Stampe/movimenti_veicoli/foglio_trasferimento.aspx?orientamento=verticale&id_trasf=" & id_modifica.Text
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
            End If
        End If
    End Sub

    Protected Sub btnScegliTargaInterno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScegliTargaInterno.Click
        If idVeicoloSelezionatoInterno.Text = "" Then
            seleziona_targa("2")
        Else
            pulisci_campi_scelta_veicolo_interno()
            btnScegliTarga.Text = "Scegli"
        End If
    End Sub

    Protected Sub pulisci_campi_scelta_veicolo_interno()
        idVeicoloSelezionatoInterno.Text = ""
        idGruppoInterno.Text = ""
        idTrasfInterno.Text = ""
        stato_trasferimento_interno.Text = ""
        txtTargaInterno.Text = ""
        txtTargaInterno.ReadOnly = False
        txtGruppoInterno.Text = ""
        txtModelloInterno.Text = ""
        txtKmInterno.Text = ""
        txtSerbatoioInterno.Text = ""
        txtKmRientroInterno.Text = ""
        txtKmRientroInterno.ReadOnly = False
        txtSerbatoioRientroInterno.Text = ""
        txtSerbatoioRientroInterno.ReadOnly = False

        txtRiferimentoInterno.Text = ""
        txtRiferimentoInterno.Enabled = True

        btnScegliTargaInterno.Visible = True
        btnStampaFoglioTrasferimentoInterno.Visible = False

        btnCheckOutInterno.Text = "Salva - Check Out"

        lblSerbatoioMaxInterno.Text = ""
        lblSerbatoioMaxRientroInterno.Text = ""

        dropDriversInterno.SelectedValue = "0"
        dropDriversInterno.Enabled = True
        txtPresuntoRientroInterno.Text = ""
        txtPresuntoRientroInterno.Enabled = True
        txtPresuntoRientroOraInterno.Text = ""
        txtPresuntoRientroOraInterno.Enabled = True

        txtDataUscitaInterno.Text = ""
        txtOraUscitaInterno.Text = ""

        txtDataRientroInterno.Text = ""
        txtOraRientroInterno.Text = ""

        dropStazionePickUpInterno.SelectedValue = "0"
        dropStazionePresuntoDropOffInterno.SelectedValue = "0"
        dropStazionePickUpInterno.Enabled = True
        dropStazionePresuntoDropOffInterno.Enabled = True

        btnAutorizzaTrasferimentoInterno.Visible = False
        btnNegaTrasferimentoInterno.Visible = False

        riga_rientro_interno.Visible = False
    End Sub

    Protected Sub btnNuovoInterno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovoInterno.Click
        stato_trasferimento_interno.Text = "0"

        div_cerca_interni.Visible = False
        div_traferimento_interno.Visible = True

        If permesso_admin.Text = "3" Then
            dropStazionePickUpInterno.SelectedValue = "0"
            dropStazionePickUpInterno.Enabled = True
            txtDataUscitaInterno.Enabled = False
            txtOraUscitaInterno.Enabled = False
        Else
            dropStazionePickUpInterno.SelectedValue = id_stazione_operatore.Text
            dropStazionePickUpInterno.Enabled = False
            txtDataUscitaInterno.Enabled = False
            txtOraUscitaInterno.Enabled = False
        End If

        btnVediCheckInterno.Visible = False
        btnCheckOutInterno.Visible = True
        btnStampaFoglioTrasferimentoInterno.Visible = False

        lblStatoAut.Text = ""
    End Sub

    Protected Sub btnChiudiCheckInterno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudiCheckInterno.Click
        Try
            idTrasfInterno.Text = ""
            pulisci_campi_scelta_veicolo_interno()

            listTrasferimentiDaEffettuare.DataBind()
            listTrasferimentiVersoStazione.DataBind()
            listRichieste.DataBind()
            listTrasferimentiInterni.DataBind()

            div_cerca_interni.Visible = True
            div_traferimento_interno.Visible = False
        Catch ex As Exception
            Response.Write("error_btnChiudiCheckInterno_Click_:" & ex.Message & "<br/>")
        End Try

    End Sub

    Protected Sub check_out_trasferimento_interno()
        'SALVATAGGIO DELLA RIGA DI TRASFERIMENTO
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim num_trasferimento As String = get_num_trasferimento(Request.Cookies("SicilyRentCar")("stazione"))
            Dim presunto_rientro As String = funzioni_comuni.GetDataSql(txtPresuntoRientroInterno.Text, 99) & " " & txtPresuntoRientroOraInterno.Text & ":00"

            Dim uscita_interno As String


            uscita_interno = "GetDate()"
            txtDataUscitaInterno.Text = Format(Now(), "dd/MM/yyyy")
            txtOraUscitaInterno.Text = Hour(Now()) & ":" & Minute(Now())

            Dim trasf_diretto_autorizzato As String
            Dim id_operatore_admin As String
            Dim data_gestione_admin As String

            If permesso_admin.Text = "3" Then
                trasf_diretto_autorizzato = "'1'"
                id_operatore_admin = "'" & Request.Cookies("SicilyRentCar")("IdUtente") & "'"
                data_gestione_admin = "GetDate()"
                lblStatoAut.Text = "Trasferimento Autorizzato"
            Else
                trasf_diretto_autorizzato = "NULL"
                id_operatore_admin = "NULL"
                data_gestione_admin = "NULL"
                lblStatoAut.Text = "Da Autorizzare"
            End If

            sqla = "INSERT INTO trasferimenti (status, tipologia, id_gruppo_da_trasferire, num_trasferimento,"
            sqla += "id_veicolo, targa, km_uscita, litri_uscita, litri_max, data_uscita,id_conducente,data_presunto_rientro,id_operatore_uscita, richiesta_per_id_stazione, id_stazione_uscita, riferimento, trasf_diretto_autorizzato, id_operatore_admin, data_gestione_admin) VALUES ("
            sqla += "'" & Costanti.id_trasferimento_in_corso & "','2','" & idGruppoInterno.Text & "','" & num_trasferimento & "',"
            sqla += "'" & idVeicoloSelezionatoInterno.Text & "','" & txtTargaInterno.Text & "','" & txtKmInterno.Text & "',"
            sqla += "'" & txtSerbatoioInterno.Text & "','" & lblSerbatoioMaxInterno.Text & "',convert(datetime," & uscita_interno & ",102),"
            sqla += "'" & dropDriversInterno.SelectedValue & "',convert(datetime,'" & presunto_rientro & "',102),'" & Request.Cookies("SicilyRentCar")("IdUtente") & "',"
            sqla += "'" & dropStazionePresuntoDropOffInterno.SelectedValue & "','" & dropStazionePickUpInterno.SelectedValue & "',"
            sqla += "'" & Replace(txtRiferimentoInterno.Text, "'", "''") & "'," & trasf_diretto_autorizzato & "," & id_operatore_admin & ",convert(datetime," & data_gestione_admin & ",102))"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd = New Data.SqlClient.SqlCommand("SELECT @@IDENTITY FROM trasferimenti WITH(NOLOCK)", Dbc)
            idTrasfInterno.Text = Cmd.ExecuteScalar

            'REGISTRAZIONE DEL MOVIMENTO DI NOLO IN CORSO PER IL VEICOLO -----------------------------------------------------------------------
            sqla = "insert into movimenti_targa (num_riferimento, id_veicolo, id_tipo_movimento, data_uscita, id_stazione_uscita, data_presunto_rientro, id_stazione_presunto_rientro, "
            sqla += " km_uscita, serbatoio_uscita, id_operatore, data_registrazione, movimento_attivo) "
            sqla += "VALUES"
            sqla += " ('" & num_trasferimento & "','" & idVeicoloSelezionatoInterno.Text & "','" & Costanti.idMovimentoInterno & "',convert(datetime," & uscita_interno & ",102),'" & dropStazionePickUpInterno.SelectedValue & "',"
            sqla += "convert(datetime,'" & presunto_rientro & "',102),'" & dropStazionePresuntoDropOffInterno.SelectedValue & "',"
            sqla += "'" & txtKmInterno.Text & "','" & txtSerbatoioInterno.Text & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "',convert(datetime,GetDate(),102),'1')"

            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()

            'SETTAGGIO DEL VEICOLO COME NOLEGGIATO
            sqla = "UPDATE veicoli SET disponibile_nolo='0', noleggiata='1' WHERE id='" & idVeicoloSelezionatoInterno.Text & "'"

            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()
            ''-------------------------------------------------------------------------------------------------------------------------------

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            gestione_checkin.InitFormCheckOut(tipo_documento.MovimentoInterno, Integer.Parse(idTrasfInterno.Text), 0)
            div_edit_danno.Visible = True
            tabPanel1.Visible = False

        Catch ex As Exception
            Response.Write("error_check_out_trasferimento_interno_" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Protected Sub btnCheckOutInterno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckOutInterno.Click
        Try
            If stato_trasferimento_interno.Text = "0" Then
                'IN QUESTO CASO STO SALVANDO L'USCITA DEL VEICOLO - E' NECESSARIO CONTROLLARE I CAMPI PER POTER PROCEDERE
                If idVeicoloSelezionatoInterno.Text <> "" Then
                    If dropStazionePickUpInterno.SelectedValue <> "0" Then
                        If dropStazionePresuntoDropOffInterno.SelectedValue <> "0" Then
                            If dropDriversInterno.SelectedValue <> "0" Then
                                If txtPresuntoRientroInterno.Text <> "" And txtPresuntoRientroOraInterno.Text <> "" Then
                                    Dim data1 As DateTime = funzioni_comuni.getDataDb_con_orario2(txtPresuntoRientroInterno.Text & " " & txtPresuntoRientroOraInterno.Text & ":00")
                                    Dim data2 As DateTime


                                    data2 = funzioni_comuni.getDataDb_con_orario2(Now())


                                    If DateDiff(DateInterval.Minute, data2, data1) > 0 Then
                                        'RICONTROLLO CHE IL VEICOLO SIA DISPONIBILE - NEL FRATTEMPO POTREBBE AVER CAMBIATO STATO
                                        If check_veicolo_disponibile("2") Then
                                            check_out_trasferimento_interno()

                                            stato_trasferimento_interno.Text = "1"

                                            dropDriversInterno.Enabled = False
                                            txtPresuntoRientroInterno.Enabled = False
                                            txtPresuntoRientroOraInterno.Enabled = False
                                            dropStazionePickUpInterno.Enabled = False
                                            dropStazionePresuntoDropOffInterno.Enabled = False
                                            txtDataUscitaInterno.Enabled = False
                                            txtOraUscitaInterno.Enabled = False
                                            btnScegliTargaInterno.Visible = False
                                            btnStampaFoglioTrasferimentoInterno.Visible = True
                                            btnCheckOutInterno.Visible = False
                                            btnVediCheckInterno.Visible = True
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
                                Libreria.genUserMsgBox(Me, "Specificare il conducente del veicolo.")
                            End If
                        Else
                            Libreria.genUserMsgBox(Me, "Specificare la stazione di rientro.")
                        End If
                    Else
                        Libreria.genUserMsgBox(Me, "Specificare la stazione di uscita.")
                    End If
                Else
                    Libreria.genUserMsgBox(Me, "Specificare un veicolo.")
                End If
            End If
        Catch ex As Exception
            Response.Write("error:_btnCheckOutInterno_Click_:" & ex.Message & "<br>")
        End Try

    End Sub

    Protected Sub btnStampaFoglioTrasferimentoInterno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStampaFoglioTrasferimentoInterno.Click
        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            Session("url_print") = "/Stampe/movimenti_veicoli/foglio_trasferimento.aspx?orientamento=verticale&id_trasf=" & idTrasfInterno.Text
            If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx','')", True)
            End If
        End If
    End Sub

    Protected Sub btnVediCheckInterno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVediCheckInterno.Click
        'IN QUESTO CASO L'USCITA E' GIA' STATA EFFETTUATA - SI PROCEDE CON LA STAMPA DEL CHECK OUT GIA' EFFETTUATO (NEL CASO DI TRASFERIMENTO IN CORSO)
        'O DEL CHECK OUT/CHECK IN (NEL CASO DI TRASFERIMENTO ESEGUITO)
        If stato_trasferimento_interno.Text = "1" Then
            gestione_checkin.InitFormCheckOut(tipo_documento.MovimentoInterno, Integer.Parse(idTrasfInterno.Text), 0)
            div_edit_danno.Visible = True
            tabPanel1.Visible = False
        Else
            gestione_checkin.InitFormCheckIn(tipo_documento.MovimentoInterno, Integer.Parse(idTrasfInterno.Text), 0)

            tabPanel1.Visible = False
            div_edit_danno.Visible = True
        End If
    End Sub

    Protected Function condizione_where_trasf_interni() As String
        'IN QUESTA LISTA SI VEDONO I TRASFERIMENTI INTERNI E I TRASFERIMENTI DIRETTI (SENZA PASSARE DALLA RICHIESTA)

        Dim condizione As String = " AND (trasferimenti.status='" & Costanti.id_trasferimento_in_corso & "' OR trasferimenti.status='" & Costanti.id_trasferimento_eseguito & "') AND trasferimenti.tipologia='2'"

        If dropCercaStatoInterno.SelectedValue <> "0" Then
            condizione = condizione & " AND trasferimenti.status='" & dropCercaStatoInterno.SelectedValue & "'"
        End If

        If dropCercaStazioneRientroInterno.SelectedValue <> "0" Then
            condizione = condizione & " AND trasferimenti.richiesta_per_id_stazione='" & dropCercaStazioneRientroInterno.SelectedValue & "'"
        End If

        If dropCercaStazionePickUpInterno.SelectedValue <> "0" Then
            condizione = condizione & " AND trasferimenti.id_stazione_uscita='" & dropCercaStazionePickUpInterno.SelectedValue & "'"
        End If

        'DATA USCITA-----------------------------------------------------------------------------------------------------------
        If txtCercaDataUscitaDaInterno.Text <> "" And txtCercaDataUscitaAInterno.Text = "" Then
            Dim data1 As String = funzioni_comuni.GetDataSql(txtCercaDataUscitaDaInterno.Text, 0) ' funzioni_comuni.getDataDb_senza_orario(txtCercaDataUscitaDaInterno.Text)
            'condizione = condizione & " AND data_uscita >= '" & data1 & "'"
            condizione += " AND data_uscita >=CONVERT(DATETIME, '" & data1 & "', 102)"
        End If

        If txtCercaDataUscitaDaInterno.Text = "" And txtCercaDataUscitaAInterno.Text <> "" Then
            Dim data2 As String = funzioni_comuni.GetDataSql(txtCercaDataUscitaAInterno.Text, 0) 'funzioni_comuni.getDataDb_con_orario(txtCercaDataUscitaAInterno.Text & " 23:59:59")
            condizione += " AND data_uscita <=CONVERT(DATETIME, '" & data2 & "', 102)"
            'condizione = condizione & " AND data_uscita <= '" & data2 & "'"
        End If

        If txtCercaDataUscitaDaInterno.Text <> "" And txtCercaDataUscitaAInterno.Text <> "" Then
            Dim data1 As String = funzioni_comuni.getDataDb_senza_orario(txtCercaDataUscitaDaInterno.Text)
            Dim data2 As String = funzioni_comuni.getDataDb_con_orario(txtCercaDataUscitaAInterno.Text & " 23:59:59")
            condizione += " AND data_uscita BETWEEN CONVERT(DATETIME, '" & data1 & "',102) AND CONVERT(DATETIME, '" & data2 & "',102)"
            'condizione = condizione & " AND data_uscita BETWEEN '" & data1 & "' AND '" & data2 & "'"
        End If
        '----------------------------------------------------------------------------------------------------------------------------

        If dropCercaGruppoAutoInterno.SelectedValue <> "0" Then
            condizione = condizione & " AND trasferimenti.id_gruppo_da_trasferire='" & dropCercaGruppoAutoInterno.SelectedValue & "'"
        End If

        If txtCercaTargaInterno.Text <> "" Then
            condizione = condizione & " AND trasferimenti.targa='" & Replace(txtCercaTargaInterno.Text, "'", "''") & "'"
        End If

        If dropAutorizzati.SelectedValue = "2" Then
            condizione = condizione & " AND trasferimenti.trasf_diretto_autorizzato IS NULL"
        ElseIf dropAutorizzati.SelectedValue <> "-1" Then
            condizione = condizione & " AND trasferimenti.trasf_diretto_autorizzato='" & dropAutorizzati.SelectedValue & "'"
        End If

        condizione_where_trasf_interni = condizione
    End Function

    Protected Sub ricerca_trasf_interni(ByVal order_by As String)
        Try
            query_cerca4.Text = "SELECT trasferimenti.id, trasferimenti.num_trasferimento, trasferimenti.data_uscita, trasferimenti.data_presunto_rientro, trasferimenti.data_rientro, trasferimenti.status As id_status, trasferimenti_status.descrizione As status, gruppi2.cod_gruppo As gruppo_da_trasferire, trasferimenti.id_gruppo_da_trasferire, (stazioni1.codice + ' ' + stazioni1.nome_stazione) As stazione_pick_up, trasferimenti.id_stazione_uscita, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As richiesta_per_stazione, trasferimenti.richiesta_per_id_stazione, trasferimenti.riferimento, trasferimenti.id_veicolo, trasferimenti.targa, modelli.descrizione As modello, trasferimenti.id_conducente, trasferimenti.km_uscita, trasferimenti.km_rientro, trasferimenti.litri_uscita, trasferimenti.litri_rientro, trasferimenti.litri_max, trasferimenti.trasf_diretto_autorizzato  FROM trasferimenti WITH(NOLOCK) INNER JOIN trasferimenti_status WITH(NOLOCK) ON trasferimenti.status=trasferimenti_status.id LEFT JOIN gruppi As gruppi2 WITH(NOLOCK) ON trasferimenti.id_gruppo_da_trasferire=gruppi2.id_gruppo LEFT JOIN stazioni As stazioni1 WITH(NOLOCK) ON trasferimenti.id_stazione_uscita=stazioni1.id LEFT JOIN stazioni As stazioni2 WITH(NOLOCK) ON trasferimenti.richiesta_per_id_stazione=stazioni2.id LEFT JOIN veicoli WITH(NOLOCK) ON trasferimenti.id_veicolo=veicoli.id LEFT JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello WHERE trasferimenti.id<>'0' " & condizione_where_trasf_interni()
            sqlTrasferimentiInterni.SelectCommand = query_cerca4.Text & " " & order_by
            sqla = sqlTrasferimentiInterni.SelectCommand
            lblOrderBy4.Text = order_by
            listTrasferimentiInterni.DataBind()
        Catch ex As Exception
            Response.Write("error:" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Protected Sub btnCercaInterni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCercaInterni.Click
        ricerca_trasf_interni(lblOrderBy4.Text)
    End Sub

    Protected Sub listTrasferimentiInterni_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listTrasferimentiInterni.ItemDataBound

        Try
            Dim trasf_diretto_autorizzato As Label = e.Item.FindControl("trasf_diretto_autorizzato")
            Dim autorizzato As Label = e.Item.FindControl("autorizzato")

            If trasf_diretto_autorizzato.Text = "" Then
                autorizzato.Text = "Da Aut."
            ElseIf trasf_diretto_autorizzato.Text = "True" Then
                autorizzato.Text = "Autorizz."
            ElseIf trasf_diretto_autorizzato.Text = "False" Then
                autorizzato.Text = "Negato"
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error ListTrasferimentiInterni ItemDataBound  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Protected Sub listTrasferimentiInterni_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listTrasferimentiInterni.ItemCommand
        If e.CommandName = "vedi" Then

            Dim numTrasferimento As Label = e.Item.FindControl("num_trasferimento")
            lblNumTrasferimento.Text = numTrasferimento.Text

            Dim idLabel As Label = e.Item.FindControl("idLabel")
            Dim gruppo_da_trasferire As Label = e.Item.FindControl("gruppo_da_trasferire")
            Dim id_veicolo As Label = e.Item.FindControl("id_veicolo")
            Dim targa As Label = e.Item.FindControl("targa")
            Dim lblModello As Label = e.Item.FindControl("lblModello")
            Dim id_conducente As Label = e.Item.FindControl("id_conducente")
            Dim km_uscita As Label = e.Item.FindControl("km_uscita")
            Dim km_rientro As Label = e.Item.FindControl("km_rientro")
            Dim litri_uscita As Label = e.Item.FindControl("litri_uscita")
            Dim litri_rientro As Label = e.Item.FindControl("litri_rientro")
            Dim data_uscita As Label = e.Item.FindControl("data_uscita")
            Dim data_presunto_rientro As Label = e.Item.FindControl("data_presunto_rientro")
            Dim data_rientro As Label = e.Item.FindControl("data_rientro")
            Dim litri_max As Label = e.Item.FindControl("litri_max")
            Dim riferimento As Label = e.Item.FindControl("riferimento")
            Dim richiesta_per_id_stazione As Label = e.Item.FindControl("richiesta_per_id_stazione")
            Dim id_stazione_pick_up As Label = e.Item.FindControl("id_stazione_pick_up")
            Dim trasf_diretto_autorizzato As Label = e.Item.FindControl("trasf_diretto_autorizzato")

            If trasf_diretto_autorizzato.Text = "" Then
                lblStatoAut.Text = "Da Autorizzare"

                If permesso_admin.Text = "3" Then
                    btnAutorizzaTrasferimentoInterno.Visible = True
                    btnNegaTrasferimentoInterno.Visible = True
                End If
            ElseIf trasf_diretto_autorizzato.Text = "True" Then
                lblStatoAut.Text = "Trasferimento Autorizzato"
            ElseIf trasf_diretto_autorizzato.Text = "False" Then
                lblStatoAut.Text = "Trasferimento Negato"
            End If

            id_modifica.Text = idLabel.Text
            idTrasfInterno.Text = idLabel.Text
            idVeicoloSelezionatoInterno.Text = id_veicolo.Text
            txtTargaInterno.Text = targa.Text
            txtGruppoInterno.Text = gruppo_da_trasferire.Text
            txtModelloInterno.Text = lblModello.Text
            txtKmInterno.Text = km_uscita.Text
            txtKmRientroInterno.Text = km_rientro.Text
            txtSerbatoioInterno.Text = litri_uscita.Text
            lblSerbatoioMaxInterno.Text = litri_max.Text
            txtSerbatoioRientroInterno.Text = litri_rientro.Text
            dropDriversInterno.SelectedValue = id_conducente.Text
            dropDriversInterno.Enabled = False

            txtDataUscitaInterno.Text = Day(data_uscita.Text) & "/" & Month(data_uscita.Text) & "/" & Year(data_uscita.Text)
            txtOraUscitaInterno.Text = Hour(data_uscita.Text) & ":" & Minute(data_uscita.Text)
            txtDataUscitaInterno.Enabled = False
            txtOraUscitaInterno.Enabled = False

            txtPresuntoRientroInterno.Text = Day(data_presunto_rientro.Text) & "/" & Month(data_presunto_rientro.Text) & "/" & Year(data_presunto_rientro.Text)
            txtPresuntoRientroOraInterno.Text = Hour(data_presunto_rientro.Text) & ":" & Minute(data_presunto_rientro.Text)
            txtPresuntoRientroInterno.Enabled = False
            txtPresuntoRientroOraInterno.Enabled = False

            If data_rientro.Text <> "" Then
                riga_rientro_interno.Visible = True
                txtDataRientroInterno.Text = Day(data_rientro.Text) & "/" & Month(data_rientro.Text) & "/" & Year(data_rientro.Text)
                txtOraRientroInterno.Text = Hour(data_rientro.Text) & ":" & Minute(data_rientro.Text)

                lblSerbatoioMaxRientroInterno.Text = litri_max.Text
                txtSerbatoioRientroInterno.Text = litri_rientro.Text
                txtKmRientroInterno.Text = km_rientro.Text
            End If


            txtRiferimentoInterno.Text = riferimento.Text
            txtRiferimentoInterno.Enabled = False


            dropStazionePickUpInterno.SelectedValue = id_stazione_pick_up.Text
            dropStazionePresuntoDropOffInterno.SelectedValue = richiesta_per_id_stazione.Text
            dropStazionePickUpInterno.Enabled = False
            dropStazionePresuntoDropOffInterno.Enabled = False

            btnCheckOutInterno.Visible = False
            btnVediCheckInterno.Visible = True

            btnScegliTargaInterno.Visible = False

            btnStampaFoglioTrasferimentoInterno.Visible = True

            div_cerca_interni.Visible = False
            div_traferimento_interno.Visible = True

            AggiornaListAllegatiT(lblNumTrasferimento.Text)




        End If
    End Sub

    Protected Function trasferimento_da_autorizzate(ByVal id_trasferimento As String) As Boolean

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            sqla = "SELECT id FROM trasferimenti WHERE trasf_diretto_autorizzato IS NULL AND id='" & id_trasferimento & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim test As String = Cmd.ExecuteScalar & ""

            If test <> "" Then
                trasferimento_da_autorizzate = True
            Else
                trasferimento_da_autorizzate = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error:_ trasferimento_da_autorizzate _" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function

    Protected Sub btnAutorizzaTrasferimentoInterno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAutorizzaTrasferimentoInterno.Click

        Try
            If trasferimento_da_autorizzate(id_modifica.Text) Then
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                sqla = "UPDATE trasferimenti SET trasf_diretto_autorizzato='1', id_operatore_admin='" & Request.Cookies("SicilyRentCar")("IdUtente") & "'," &
                "data_gestione_admin=convert(datetime,GetDate(),102) WHERE id='" & id_modifica.Text & "'"

                Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

                Cmd.ExecuteNonQuery()

                btnAutorizzaTrasferimentoInterno.Visible = False
                btnNegaTrasferimentoInterno.Visible = False

                lblStatoAut.Text = "Trasferimento Autorizzato"
                Libreria.genUserMsgBox(Me, "Trasferimento autorizzato correttamente.")

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            Else
                Libreria.genUserMsgBox(Me, "Attenzione: il trasferimento è stato già autorizzato o negato. Impossibile proseguire.")
            End If
        Catch ex As Exception
            Response.Write("error:_btnAutorizzaTrasferimentoInterno_Click_" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try




    End Sub

    Protected Sub btnNegaTrasferimentoInterno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNegaTrasferimentoInterno.Click
        Try
            If trasferimento_da_autorizzate(id_modifica.Text) Then
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                sqla = "UPDATE trasferimenti SET trasf_diretto_autorizzato='0', id_operatore_admin='" & Request.Cookies("SicilyRentCar")("IdUtente") & "'," &
                "data_gestione_admin=convert(datetime,GetDate(),102) WHERE id='" & id_modifica.Text & "'"

                Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

                Cmd.ExecuteNonQuery()

                btnAutorizzaTrasferimentoInterno.Visible = False
                btnNegaTrasferimentoInterno.Visible = False

                lblStatoAut.Text = "Trasferimento Negato"
                Libreria.genUserMsgBox(Me, "Trasferimento negato correttamente.")

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            Else
                Libreria.genUserMsgBox(Me, "Attenzione: il trasferimento è stato già autorizzato o negato. Impossibile proseguire.")
            End If
        Catch ex As Exception
            Response.Write("error:_ btnNegaTrasferimentoInterno_Click _" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub


    'blocco pannello allegati 21.09.2022 salvo
    Protected Sub ListViewAllegati_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListViewAllegati.ItemCommand

        Try
            If e.CommandName = "SelezionaAllegato" Then

                Dim NomeFile As Label = e.Item.FindControl("lblNomeFile")
                Dim PercFile As Label = e.Item.FindControl("lblPercorsoFile")

                Dim posizione As Integer = PercFile.Text.IndexOf("trasferimenti")
                Dim newPercorso As String = Mid(Replace(PercFile.Text, "\", "/"), posizione + 1) 'restituisce una stringa a partire dalla posizione specificata dopo averla convertita
                newPercorso = "/allegati_veicoli/" & newPercorso & NomeFile.Text 'aggiungo il nome del file al precorso

                'Dim filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "allegati_veicoli\trasferimenti\" & nomedir & "\"

                If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('" & newPercorso & "','')", True)
                    End If
                End If
            End If

            If e.CommandName = "EliminaAllegato" Then

                Dim NomeFile As Label = e.Item.FindControl("lblNomeFile")
                Dim PercFile As Label = e.Item.FindControl("lblPercorsoFile")

                Dim IdAllegatoDaEliminare As Label = e.Item.FindControl("lblIdAllegato")
                Dim my_allegatiT As AllegatiT = New AllegatiT
                my_allegatiT.id = IdAllegatoDaEliminare.Text
                my_allegatiT.DeleteAllegatoT("trasferimenti_allegati")
                AggiornaListAllegatiT(CInt(lblNumTrasferimento.Text))


                'file da eliminare se presente
                Dim pathfilefull As String = PercFile.Text & "\" & NomeFile.Text
                If File.Exists(pathfilefull) Then
                    File.Delete(pathfilefull)
                End If



            End If
        Catch ex As Exception
            Response.Write("error itemlistview1: " & ex.Message & "<br/>")
        End Try

    End Sub


    Protected Sub AggiornaListAllegatiT(ByVal id_rif As Integer)

        'Dim sqlstr As String = ""Select dbo.trasferimenti_allegati.Id, dbo.trasferimenti_TipoAllegato.TipoAllegato, dbo.trasferimenti_allegati.NomeFile, dbo.trasferimenti_allegati.PercorsoFile "
        'sqlstr += "From dbo.trasferimenti_allegati With (NOLOCK) INNER Join dbo.trasferimenti_TipoAllegato With (NOLOCK) On dbo.trasferimenti_allegati.Idtipodocumento = dbo.trasferimenti_TipoAllegato.Id "
        'sqlstr += "Where dbo.trasferimenti_allegati.Id_rif = " & id_rif & ";"
        Dim sqlstr As String
        sqlstr = "Select  trasferimenti_Allegati.Id, trasferimenti_Allegati.DataCreazione, trasferimenti_TipoAllegato.TipoAllegato, trasferimenti_Allegati.NomeFile, "
        sqlstr += "trasferimenti_Allegati.PercorsoFile, (operatori.cognome + ' ' + operatori.nome) as operatore "
        sqlstr += "From trasferimenti_Allegati WITH (NOLOCK) INNER Join "
        sqlstr += "trasferimenti_TipoAllegato WITH (NOLOCK) ON trasferimenti_Allegati.IdTipoDocumento = trasferimenti_TipoAllegato.Id LEFT OUTER Join "
        sqlstr += "operatori On trasferimenti_Allegati.id_operatore = operatori.id "
        sqlstr += "Where trasferimenti_Allegati.Id_rif = " & id_rif & ";"

        sqlAllegati.SelectCommand = sqlstr

        ListViewAllegati.DataBind()
        ListViewAllegati.Visible = True



    End Sub





    Protected Sub btnMemorizzaAlleg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMemorizzaAlleg.Click '20.09.2022 salvo

        'If FileUploadAllegati.HasFile Then
        'Try
        'FileUploadAllegati.SaveAs(Server.MapPath("gestione_multe\Allegati\2012") & "\" & FileUploadAllegati.FileName)
        'Catch ex As Exception
        'lblMessUploadFile.Text = "Errore: " & ex.Message.ToString()
        'End Try
        'End If

        Dim Messaggio As String = ""

        Try
            If FileUploadAllegati.HasFile And DropTipoAllegato.SelectedValue <> "0" Then
                Dim estensione As String = LCase(Right(FileUploadAllegati.FileName, 4))
                If estensione = ".jpg" Or estensione = ".png" Or estensione = ".pdf" Then
                    'Trace.Write("FileUpload1.PostedFile.ContentLength:" & FileUploadAllegati.PostedFile.ContentLength)
                    If FileUploadAllegati.PostedFile.ContentLength <= 6000000 Then 'aggiornato salvo 15.05.2023

                        Dim filePath As String
                        Dim data_uscita As String = FormatDateTime(txtDataUscitaInterno.Text, vbShortDate)
                        Dim anomedir() As String = Split(data_uscita, "/")
                        Dim nomedir As String = lblNumTrasferimento.Text 'anomedir(2) & anomedir(1) & anomedir(0)

                        Dim numTrasferimento As String = lblNumTrasferimento.Text

                        filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "allegati_veicoli\trasferimenti\" & nomedir & "\"

                        If Directory.Exists(filePath) = False Then
                            'creo la directory
                            Directory.CreateDirectory(filePath)
                        End If

                        Dim NomeFile As String
                        'NomeFile = System.Guid.NewGuid().ToString
                        Dim dataTemp As Date = Now
                        NomeFile = nomedir & "_" & Hour(dataTemp) & Minute(dataTemp) & Second(dataTemp)

                        'ricava sigla documento
                        Dim siglaAllegato As String = funzioni_comuni_new.getSiglaAllegato(DropTipoAllegato.SelectedValue.ToString, "trasferimenti_TipoAllegato")

                        Dim maxid As Integer = GetMaxIdAllegati("trasferimenti_allegati") + 1


                        Dim fileNameBig As String = NomeFile & "_" & siglaAllegato & "_"

                        fileNameBig = fileNameBig & estensione

                        FileUploadAllegati.SaveAs(filePath & fileNameBig)
                        Dim my_allegatit As AllegatiT = New AllegatiT

                        If File.Exists(filePath & fileNameBig) Then
                            'qui il codice per registrare il percorso del file nell'apposita tabella

                            With my_allegatit
                                .DataCreazione = Now
                                .IdTipoDocumento = DropTipoAllegato.SelectedValue
                                .NomeFile = fileNameBig
                                .PercorsoFile = filePath
                                .Id_rif = CInt(nomedir)
                                my_allegatit.InsertAllegatoT("trasferimenti_allegati", maxid)
                            End With

                            AggiornaListAllegatiT(my_allegatit.Id_rif)

                            DropTipoAllegato.SelectedValue = 0
                            Messaggio = "Documento correttamente salvato."
                        End If
                    Else
                        Messaggio = "Il file non può essere caricato perché supera 2MB!"
                    End If
                Else
                    Messaggio = "L'estensione dell'immagine deve essere con estensione (jpg, png, pdf)"
                    End If
            Else
                If DropTipoAllegato.SelectedValue = "0" Then
                    Messaggio = "Selezionare un tipo di allegato."
                Else
                    Messaggio = "Selezionare un file da salvare."
                End If

            End If

            If Messaggio <> "" Then
                Libreria.genUserMsgBox(Page, Messaggio)
            End If

        Catch ex As Exception
            Messaggio = "Error: Memorizza Allegato: " & ex.Message
            Libreria.genUserMsgBox(Page, Messaggio)
        End Try





    End Sub

    'end blocco pannello allegati















End Class
