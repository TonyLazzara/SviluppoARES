Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports funzioni_comuni

Partial Class MulteLocatori
    Inherits System.Web.UI.Page

    Protected Sub btnImportaFattura_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportaFattura.Click
        If Not IsDate(txtDataFattLoc.Text) Then
            Libreria.genUserMsgBox(Page, "Data fattura non corretta")
            Exit Sub
        End If
        Dim Messaggio As String = ""
        If FileUploadFattLocatori.HasFile Then
            Dim estensione As String = LCase(Right(FileUploadFattLocatori.FileName, 4))
            If estensione = ".csv" Then
                If FileUploadFattLocatori.PostedFile.ContentLength <= 1000000 Then
                    Dim filePath As String
                    filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\gestione_multe\FattureLocatori\"
                    'Trace.Write("--------------path: " & filePath)
                    If Directory.Exists(filePath) = False Then
                        'creo la directory
                        Directory.CreateDirectory(filePath)
                    End If

                    Dim NomeFile As String
                    'NomeFile = System.Guid.NewGuid().ToString
                    Dim dataFatt As Date = txtDataFattLoc.Text
                    NomeFile = DropLocatori.SelectedItem.ToString & "_" & Year(dataFatt) & CStr(Month(dataFatt)).PadLeft(2, "0"c) & CStr(Day(dataFatt)).PadLeft(2, "0"c) & "_" & txtNumFattLoc.Text & ".csv"

                    If File.Exists(filePath & NomeFile) Then
                        Libreria.genUserMsgBox(Page, "File già importato precedentemente.")
                        Exit Sub
                    End If

                    FileUploadFattLocatori.SaveAs(filePath & NomeFile)

                    If File.Exists(filePath & NomeFile) Then
                        'qui il codice per memorizzare i dati nel database
                        Dim objStreamReader As StreamReader
                        objStreamReader = File.OpenText(filePath & NomeFile)
                        Dim Riga As String

                        Riga = objStreamReader.ReadLine()

                        Dim IdFatturaLocMemorizzata As Integer = MemorizzaFatturaLoc()
                        While (objStreamReader.Peek() <> -1)
                            Riga = objStreamReader.ReadLine()

                            MemorizzaRigaFattLoc(Riga, IdFatturaLocMemorizzata)

                        End While


                        objStreamReader.Close()

                        Messaggio = "Documento correttamente salvato."
                        ListViewFattLocatori.DataBind()
                    End If
                Else
                    Messaggio = "Il file non può essere caricato perché supera 1MB!"
                End If
            Else
                Messaggio = "L'estensione del file deve essere con estensione csv"
            End If
        Else
            Messaggio = "Selezionare un file da salvare."
        End If

        If Messaggio <> "" Then
            Libreria.genUserMsgBox(Page, Messaggio)
        End If
    End Sub

    Public Function MemorizzaFatturaLoc() As Integer
        Dim sqlStr As String = "INSERT INTO multe_FattureLocatore (Id,idLocatore,NFattura,DataFattura,TotFattura) " & _
                                "VALUES (@Id,@idLocatore,@NFattura,@DataFattura,@TotFattura)"
        Dim UltimoRecordInserito = GetMaxIdFattureLoc()
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)

                MyAddParametro(Cmd, "@Id", System.Data.SqlDbType.Int, UltimoRecordInserito + 1)
                MyAddParametro(Cmd, "@idLocatore", System.Data.SqlDbType.Int, DropLocatori.SelectedValue)
                MyAddParametro(Cmd, "@NFattura", System.Data.SqlDbType.NVarChar, txtNumFattLoc.Text)
                MyAddParametro(Cmd, "@DataFattura", System.Data.SqlDbType.DateTime, txtDataFattLoc.Text)
                MyAddParametro(Cmd, "@TotFattura", System.Data.SqlDbType.Real, txtTotFattLoc.Text)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

        End Using

        Return UltimoRecordInserito + 1
    End Function

    Public Shared Function GetMaxIdFattureLoc() As Integer
        Dim MyReader As SqlDataReader
        Using MyConnection As SqlConnection = New SqlConnection

            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString

            Using MyCommand As SqlCommand = New SqlCommand()
                MyCommand.CommandText = "SELECT MAX(Id) AS MaxId FROM multe_FattureLocatore"
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

    Protected Shared Sub MyAddParametro(ByVal Cmd As System.Data.SqlClient.SqlCommand, ByVal NomePar As String, ByVal Tipo As System.Data.SqlDbType, ByVal Valore As Object)

        If IsDate(Valore) AndAlso Valore = CDate("01/01/1800") Then 'utilizzato solo per le date
            Cmd.Parameters.Add(NomePar, Tipo).Value = DBNull.Value
        Else 'per tutti gli altri tipi di dati
            If Valore Is DBNull.Value Or Valore Is Nothing Then
                'HttpContext.Current.Trace.Write("addParametro DBNull: " & NomePar & " - " & Tipo & " - " & Valore)
                Cmd.Parameters.Add(NomePar, Tipo).Value = DBNull.Value
            Else
                'HttpContext.Current.Trace.Write("addParametro Valore: " & NomePar & " - " & Tipo & " - " & Valore)
                Cmd.Parameters.Add(NomePar, Tipo).Value = Valore
            End If
        End If
    End Sub

    Public Sub MemorizzaRigaFattLoc(ByVal RigaFatt As String, ByVal idft As Integer)
        Dim RigaStr() As String = Split(RigaFatt, ";")

        Dim sqlStr As String = "INSERT INTO multe_RigaFattLocatore (idFatturaLoc,Targa,DataInfrazione,NumVerbale,Ente,ComuneEnte,DataNotifica,Importo,Spese,Totale,Descrizione) " & _
                        "VALUES (@idFatturaLoc,@Targa,@DataInfrazione,@NumVerbale,@Ente,@ComuneEnte,@DataNotifica,@Importo,@Spese,@Totale,@Descrizione)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)

                MyAddParametro(Cmd, "@idFatturaLoc", System.Data.SqlDbType.Int, idft)
                MyAddParametro(Cmd, "@Targa", System.Data.SqlDbType.NVarChar, RigaStr(0))
                MyAddParametro(Cmd, "@DataInfrazione", System.Data.SqlDbType.DateTime, Replace(RigaStr(1), ".", ":"))
                MyAddParametro(Cmd, "@NumVerbale", System.Data.SqlDbType.NVarChar, RigaStr(2))
                MyAddParametro(Cmd, "@Ente", System.Data.SqlDbType.NVarChar, RigaStr(3))
                MyAddParametro(Cmd, "@ComuneEnte", System.Data.SqlDbType.NVarChar, RigaStr(4))
                MyAddParametro(Cmd, "@DataNotifica", System.Data.SqlDbType.DateTime, RigaStr(5))
                MyAddParametro(Cmd, "@Importo", System.Data.SqlDbType.Real, RigaStr(6))
                MyAddParametro(Cmd, "@Spese", System.Data.SqlDbType.Real, RigaStr(7))
                MyAddParametro(Cmd, "@Totale", System.Data.SqlDbType.Real, RigaStr(8))
                MyAddParametro(Cmd, "@Descrizione", System.Data.SqlDbType.NVarChar, RigaStr(9))

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

        End Using
    End Sub

    Protected Sub btnChiudi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudi.Click

        txtRifFattLoc.Text = ""
        txtRifDataFattLoc.Text = ""
        txtRifLocatore.Text = ""
        lblidFatturaLocatore.Text = ""
        lblidFatturaLocatore.Text = ""
        lblId_Locatore.Text = ""
        divMulteLocatori.Visible = True
        divElaboraFattLoc.Visible = False
    End Sub

    Protected Sub ListViewFattLocatori_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListViewFattLocatori.ItemCommand
        If e.CommandName = "ElaboraFattLoc" Then
            Dim LabelNumFatt As Label = e.Item.FindControl("lbl_NFattura")
            Dim LabelDataFatt As Label = e.Item.FindControl("lbl_DataFattura")
            Dim LabelLocatore As Label = e.Item.FindControl("lbl_Locatore")
            Dim LabelIdLocatore As Label = e.Item.FindControl("lbl_IdLocatore")
            Dim LabelIDFattLoc As Label = e.Item.FindControl("lbl_id")

            txtRifFattLoc.Text = LabelNumFatt.Text
            txtRifDataFattLoc.Text = LabelDataFatt.Text
            txtRifLocatore.Text = LabelLocatore.Text
            lblId_Locatore.Text = LabelIdLocatore.Text
            lblidFatturaLocatore.Text = LabelIDFattLoc.Text
            SqlDettaglioFattLoc.SelectCommand = "SELECT Id, idFatturaLoc, Targa, DataInfrazione, NumVerbale, Ente, ComuneEnte, Importo, Spese, Totale, IdMulta, ProtMulta FROM multe_RigaFattLocatore WITH(NOLOCK) WHERE idFatturaLoc=" & LabelIDFattLoc.Text & " ORDER BY ProtMulta , id"
            SqlDettaglioFattLoc.DataBind()
            divMulteLocatori.Visible = False
            divElaboraFattLoc.Visible = True
        End If

        If e.CommandName = "StampaFattLoc" Then
            Dim LabelIDFattLoc As Label = e.Item.FindControl("lbl_id")
            Dim LabelNumFatt As Label = e.Item.FindControl("lbl_NFattura")
            Dim LabelDataFatt As Label = e.Item.FindControl("lbl_DataFattura")
            Dim LabelLocatore As Label = e.Item.FindControl("lbl_Locatore")
            'Dim Intestazione As String = "Elenco--multe"
            Dim Intestazione As String = "Elenco--multe--" & LabelLocatore.Text & "--relative--alla--fattura--n.--" & LabelNumFatt.Text & "--del--" & LabelDataFatt.Text & "--non--riscontrate."
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                Dim url_print As String = "/stampe/multe/FattLocatoriDaContestare.aspx?orientamento=verticale&margin_top=25&IdFattLocatori=" & LabelIDFattLoc.Text & "&TipoStampa=0&header_html=/stampe/multe/header_FattLocatoriDaContestare.aspx?Titolo=" & Intestazione
                Dim mio_random As String = Format((New Random).Next(), "0000000000")
                Session("url_print") = url_print
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx?a=" & mio_random & "','')", True)
                End If
            End If

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            divMulteLocatori.Visible = True
            divElaboraFattLoc.Visible = False
        End If
    End Sub

    Protected Sub ListViewMulte_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListViewMulte.ItemCommand
        Dim id_riga_fatt_loc As Integer
        If lblIdRigaFatturaLocSelezionata.Text = "" Then
            Libreria.genUserMsgBox(Page, "Nessuna riga fattura locatore selezionata")
            Exit Sub
        Else
            id_riga_fatt_loc = Int(lblIdRigaFatturaLocSelezionata.Text)
        End If

        'metto i riferimenti della multa nella rigafatturaLocatore
        Dim lbl_IdMulta As Label = e.Item.FindControl("lblIdMulta")
        Dim lbl_Prot As Label = e.Item.FindControl("lblProt")
        Dim lbl_Anno As Label = e.Item.FindControl("lblAnno")

        MarcaRigaFattLocatore(id_riga_fatt_loc, lbl_IdMulta.Text, lbl_Prot.Text & "/" & lbl_Anno.Text)
        AzzeraRigaTemp()
        MarcaMulte(lbl_IdMulta.Text)
        SqlDettaglioFattLoc.SelectCommand = "SELECT Id, idFatturaLoc, Targa, DataInfrazione, NumVerbale, Ente, ComuneEnte, Importo, Spese, Totale, IdMulta, ProtMulta FROM multe_RigaFattLocatore WITH(NOLOCK) WHERE idFatturaLoc=" & lblidFatturaLocatore.Text & " ORDER BY ProtMulta , id"
        SqlDettaglioFattLoc.DataBind()
        sqlElencoMulte.SelectCommand = "SELECT multe.ID, multe.Prot, multe.Anno, multe.NumVerbale, multe.DataInfrazione, multe.DataNotifica, multe.MultaImporto, multe.Targa, " & _
                        "multe.LocatoreNumFatt, multe.LocatoreDataFatt, multe_Locatori.Locatore FROM multe WITH(NOLOCK) LEFT OUTER JOIN " & _
                        "multe_Locatori WITH(NOLOCK) ON multe.LocatoreId = multe_Locatori.Id WHERE multe.ID = 0"
        sqlElencoMulte.DataBind()
        Libreria.genUserMsgBox(Page, "Multa agganciata correttamente alla fattura.")
    End Sub

    Protected Sub ListViewDettFattLoc_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListViewDettFattLoc.ItemCommand
        'azzero i dati per precauzione
        sqlElencoMulte.SelectCommand = "SELECT multe.ID, multe.Prot, multe.Anno, multe.NumVerbale, multe.DataInfrazione, multe.DataNotifica, multe.MultaImporto, multe.Targa, " & _
                        "multe.LocatoreNumFatt, multe.LocatoreDataFatt, multe_Locatori.Locatore FROM multe WITH(NOLOCK) LEFT OUTER JOIN " & _
                        "multe_Locatori WITH(NOLOCK) ON multe.LocatoreId = multe_Locatori.Id WHERE multe.ID = 0"
        sqlElencoMulte.DataBind()
        lblIdRigaFatturaLocSelezionata.Text = ""
        AzzeraRigaTemp()

        If e.CommandName = "RicercaMulta" Then
            Dim lbl_idRigaFatturaLoc As Label = e.Item.FindControl("lbl_id")
            Dim lbl_Targa As Label = e.Item.FindControl("lbl_Targa")
            Dim lbl_DataInfrazione As Label = e.Item.FindControl("lbl_DataInfrazione")
            Dim lbl_NumVerbale As Label = e.Item.FindControl("lbl_NumVerbale")
            Dim lbl_Ente As Label = e.Item.FindControl("lbl_Ente")
            Dim lbl_ComuneEnte As Label = e.Item.FindControl("lbl_ComuneEnte")
            Dim lbl_Importo As Label = e.Item.FindControl("lbl_Importo")
            Dim lbl_Spese As Label = e.Item.FindControl("lbl_Spese")
            Dim lbl_Totale As Label = e.Item.FindControl("lbl_Totale")
            Dim lbl_idMulta As Label = e.Item.FindControl("lbl_idMulta")
            Dim lbl_ProtMulta As Label = e.Item.FindControl("lbl_ProtMulta")

            lblIdRigaFatturaLocSelezionata.Text = lbl_idRigaFatturaLoc.Text
            sqlElencoMulte.SelectCommand = "SELECT multe.ID, multe.Prot, multe.Anno, multe.NumVerbale, multe.DataInfrazione, multe.DataNotifica, multe.MultaImporto, multe.Targa, " & _
                        "multe.LocatoreNumFatt, multe.LocatoreDataFatt, multe_Locatori.Locatore FROM multe WITH(NOLOCK) LEFT OUTER JOIN " & _
                        "multe_Locatori WITH(NOLOCK) ON multe.LocatoreId = multe_Locatori.Id WHERE Targa='" & lbl_Targa.Text & "' ORDER BY DataInfrazione DESC"
            'Trace.Write("------------------ sql query: " & "SELECT [id], [Prot], [Anno], [NumVerbale], [DataInfrazione], [DataNotifica], [MultaImporto], [Targa], [LocatoreNumFatt] FROM [multe] WHERE Targa='" & lbl_Targa.Text & "'")
            sqlElencoMulte.DataBind()

            txtTargaTemp.Text = lbl_Targa.Text
            txtDataInfrazTemp.Text = lbl_DataInfrazione.Text
            txtNumVerbTemp.Text = lbl_NumVerbale.Text
            txtEnteTemp.Text = lbl_Ente.Text
            txtComuneEnteTemp.Text = lbl_ComuneEnte.Text
            txtImportoTemp.Text = lbl_Importo.Text
            txtSpeseTemp.Text = lbl_Spese.Text
            txtTotaleTemp.Text = lbl_Totale.Text
            txtIdMultaTemp.Text = lbl_idMulta.Text
            txtProMultaTemp.Text = lbl_ProtMulta.Text
        End If

        If e.CommandName = "CancellaProt" Then
            Dim lbl_idRigaFatturaLoc As Label = e.Item.FindControl("lbl_id")
            Dim lbl_idMulta As Label = e.Item.FindControl("lbl_idMulta")


            If lbl_idMulta.Text = "" Then
                Exit Sub
            End If
            CancMarcaturaRigaFattLocatore(lbl_idRigaFatturaLoc.Text)
            CancMarcaturaMulte(lbl_idMulta.Text)
            SqlDettaglioFattLoc.SelectCommand = "SELECT Id, idFatturaLoc, Targa, DataInfrazione, NumVerbale, Ente, ComuneEnte, Importo, Spese, Totale, IdMulta, ProtMulta FROM multe_RigaFattLocatore WITH(NOLOCK) WHERE idFatturaLoc=" & lblidFatturaLocatore.Text & " ORDER BY ProtMulta , id"
            SqlDettaglioFattLoc.DataBind()
            Libreria.genUserMsgBox(Page, "Cancellato aggancio con multa.")
        End If

    End Sub

    Public Sub MarcaRigaFattLocatore(ByVal IdRigaFattLoc As Integer, ByVal idMulta As Integer, ByVal ProtMulta As String)
        Dim sqlStr As String = "UPDATE multe_RigaFattLocatore SET" & _
        " IdMulta=@IdMulta," & _
        " ProtMulta = @ProtMulta " & _
        " WHERE ID = @ID"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                'uso la funz MyAddParametro in locale e non quella utilizzata in ITabellaDB perchè personalizzata
                MyAddParametro(Cmd, "@IdMulta", System.Data.SqlDbType.Int, idMulta)
                MyAddParametro(Cmd, "@ProtMulta", System.Data.SqlDbType.NVarChar, ProtMulta)
                MyAddParametro(Cmd, "@ID", System.Data.SqlDbType.Int, IdRigaFattLoc)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub MarcaMulte(ByVal idMulta As Integer)
        Dim sqlStr As String = "UPDATE multe SET " & _
        "LocatoreId=@LocatoreId," & _
        "LocatoreNumFatt = @LocatoreNumFatt," & _
        "LocatoreDataFatt = @LocatoreDataFatt " & _
        "WHERE ID = @ID"

        'HttpContext.Current.Trace.Write("prima dell'update --------------------------------" & sqlStr)
        'Trace.Write("--------- IdRigaFattLoc: " & IdRigaFattLoc)
        'Trace.Write("--------- IdMulta: " & idMulta)
        'Trace.Write("--------- ProtMulta: " & ProtMulta)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                'uso la funz MyAddParametro in locale e non quella utilizzata in ITabellaDB perchè personalizzata
                MyAddParametro(Cmd, "@LocatoreId", System.Data.SqlDbType.Int, lblId_Locatore.Text)
                MyAddParametro(Cmd, "@LocatoreNumFatt", System.Data.SqlDbType.NVarChar, txtRifFattLoc.Text)
                MyAddParametro(Cmd, "@LocatoreDataFatt", System.Data.SqlDbType.DateTime, txtRifDataFattLoc.Text)
                MyAddParametro(Cmd, "@ID", System.Data.SqlDbType.Int, idMulta)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub AzzeraRigaTemp()
        txtTargaTemp.Text = ""
        txtDataInfrazTemp.Text = ""
        txtNumVerbTemp.Text = ""
        txtEnteTemp.Text = ""
        txtComuneEnteTemp.Text = ""
        txtImportoTemp.Text = ""
        txtSpeseTemp.Text = ""
        txtTotaleTemp.Text = ""
        txtIdMultaTemp.Text = ""
        txtProMultaTemp.Text = ""
    End Sub

    Public Sub CancMarcaturaRigaFattLocatore(ByVal IdRigaFattLoc As Integer)
        Dim sqlStr As String = "UPDATE multe_RigaFattLocatore SET" & _
        " IdMulta=@IdMulta," & _
        " ProtMulta = @ProtMulta " & _
        " WHERE ID = @ID"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                'uso la funz MyAddParametro in locale e non quella utilizzata in ITabellaDB perchè personalizzata
                MyAddParametro(Cmd, "@IdMulta", System.Data.SqlDbType.Int, Nothing)
                MyAddParametro(Cmd, "@ProtMulta", System.Data.SqlDbType.NVarChar, Nothing)
                MyAddParametro(Cmd, "@ID", System.Data.SqlDbType.Int, IdRigaFattLoc)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub CancMarcaturaMulte(ByVal idMulta As Integer)
        Dim sqlStr As String = "UPDATE multe SET " & _
        "LocatoreId=@LocatoreId," & _
        "LocatoreNumFatt = @LocatoreNumFatt," & _
        "LocatoreDataFatt = @LocatoreDataFatt " & _
        "WHERE ID = @ID"

        'HttpContext.Current.Trace.Write("prima dell'update --------------------------------" & sqlStr)
        'Trace.Write("--------- IdRigaFattLoc: " & IdRigaFattLoc)
        'Trace.Write("--------- IdMulta: " & idMulta)
        'Trace.Write("--------- ProtMulta: " & ProtMulta)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                'uso la funz MyAddParametro in locale e non quella utilizzata in ITabellaDB perchè personalizzata
                MyAddParametro(Cmd, "@LocatoreId", System.Data.SqlDbType.Int, Nothing)
                MyAddParametro(Cmd, "@LocatoreNumFatt", System.Data.SqlDbType.NVarChar, Nothing)
                MyAddParametro(Cmd, "@LocatoreDataFatt", System.Data.SqlDbType.DateTime, Nothing)
                MyAddParametro(Cmd, "@ID", System.Data.SqlDbType.Int, idMulta)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub


    Protected Sub btnRicercaAutMulte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRicercaAutMulte.Click
        Dim myMulta As Multe = New Multe
        Dim numTransaz As Integer = 0
        For Each riga In ListViewDettFattLoc.Items
            Dim lbl_idRigaFatturaLoc As Label = riga.FindControl("lbl_id")
            Dim lbl_targa As Label = riga.FindControl("lbl_Targa")
            Dim lbl_dataInfrazione As Label = riga.FindControl("lbl_DataInfrazione")

            myMulta = VerificaEsistenzaMulta(lbl_targa.Text, CDate(lbl_dataInfrazione.Text))

            If myMulta.ID > 0 Then
                MarcaRigaFattLocatore(lbl_idRigaFatturaLoc.Text, myMulta.ID, myMulta.Prot & "/" & myMulta.Anno)
                MarcaMulte(myMulta.ID)
                numTransaz = numTransaz + 1
            End If

            myMulta = Nothing
        Next

        SqlDettaglioFattLoc.SelectCommand = "SELECT Id, idFatturaLoc, Targa, DataInfrazione, NumVerbale, Ente, ComuneEnte, Importo, Spese, Totale, IdMulta, ProtMulta FROM multe_RigaFattLocatore WITH(NOLOCK) WHERE idFatturaLoc=" & lblidFatturaLocatore.Text & " ORDER BY ProtMulta , id"
        ListViewDettFattLoc.DataBind()
        Libreria.genUserMsgBox(Page, "Sono state trovate ed agganciate n." & numTransaz & " righe")
    End Sub

    Public Function VerificaEsistenzaMulta(ByVal targa As String, ByVal dataInfraz As Date) As Multe
        Dim mio_record As Multe = New Multe
        Dim MyReader As SqlDataReader
        Dim MyConnection As SqlConnection = New SqlConnection

        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString
        Dim MyCommand As SqlCommand = New SqlCommand()
        MyCommand.CommandText = "Select ID, Prot, Anno, LocatoreNumFatt from multe where Targa='" & targa & "' and DataInfrazione=CONVERT(DATETIME, '" & Year(dataInfraz) & "-" & Month(dataInfraz) & "-" & Day(dataInfraz) & " " & Hour(dataInfraz) & ":" & Minute(dataInfraz) & ":00', 102)"
        MyCommand.CommandType = CommandType.Text
        MyCommand.Connection = MyConnection

        MyCommand.Connection.Open()
        MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

        mio_record.ID = 0
        Dim contatore As Integer = 0
        Dim VerificaPrecAnnotazFattLoc As String = ""
        While MyReader.Read()
            mio_record.ID = MyReader("ID")
            mio_record.Prot = MyReader("Prot")
            mio_record.Anno = MyReader("Anno")
            If IsDBNull(MyReader("LocatoreNumFatt")) = True Then
                VerificaPrecAnnotazFattLoc = ""
            Else
                If MyReader("LocatoreNumFatt") = 0 Then
                    VerificaPrecAnnotazFattLoc = ""
                Else
                    VerificaPrecAnnotazFattLoc = MyReader("LocatoreNumFatt")
                End If
            End If

            contatore = contatore + 1
        End While

        If contatore > 1 Then 'nel caso ci fosse più di una occorrenza non eseguo la transazione
            mio_record.ID = 0
        End If

        If VerificaPrecAnnotazFattLoc <> "" Then 'nel caso era già stato attributo un rif.fatt.locatore non eseguo la transazione
            mio_record.ID = 0
        End If
        MyReader.Close()
        Return mio_record
    End Function

    Protected Sub btnChiudiFattLoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudiFattLoc.Click
        Response.Redirect("RicercaMulte.aspx")
    End Sub

End Class
