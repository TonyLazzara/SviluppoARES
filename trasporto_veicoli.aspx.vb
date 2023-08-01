Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
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

    Public Sub InsertAllegatoT(tbl As String)
        Dim sqlStr As String = "INSERT INTO " & tbl & " (Id,DataCreazione,IdTipoDocumento,NomeFile,PercorsoFile,Id_rif) " &
            " VALUES (@Id,@DataCreazione,@IdTipoDocumento,@NomeFile,@PercorsoFile,@Id_rif)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = GetMaxIdAllegati() + 1
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
    Public Shared Function GetMaxIdAllegati() As Integer
        Dim MyReader As SqlDataReader
        Using MyConnection As SqlConnection = New SqlConnection

            MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString

            Using MyCommand As SqlCommand = New SqlCommand()
                MyCommand.CommandText = "SELECT MAX(ID) AS MaxId FROM trasporto_veicoli_Allegati"
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

    Public Function VerificaFileSeImportatoPrima(ByVal file As String) As Boolean
        If file = "" Then Return False

        Dim strRd As String = "SELECT id FROM trasporto_veicoli_Allegati WHERE NomeFile='" & file & "'"

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
Partial Class trasporto_veicoli
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim sql As String = "SELECT documenti_di_trasporto.id, documenti_di_trasporto.stato_trasferimento, STR(stazioni_uscita.codice) + ' - ' + stazioni_uscita.nome_stazione As stazione_uscita, "
        sql += "STR(stazioni_rientro.codice) + ' - ' + stazioni_rientro.nome_stazione As stazione_rientro,documenti_di_trasporto.data_uscita, documenti_di_trasporto.data_presunto_rientro, "
        sql += "documenti_di_trasporto.data_rientro, documenti_di_trasporto.note  "
        sql += "FROM documenti_di_trasporto WITH(NOLOCK) INNER JOIN stazioni As stazioni_uscita WITH(NOLOCK) ON documenti_di_trasporto.id_stazione_uscita=stazioni_uscita.id "
        sql += "INNER JOIN stazioni As stazioni_rientro WITH(NOLOCK) ON documenti_di_trasporto.id_stazione_rientro=stazioni_rientro.id"

        Try
            If Not Page.IsPostBack Then
                txtQuery.Text = sql
                id_stazione_utente.Text = Request.Cookies("SicilyRentCar")("stazione")

                divAllegInvioDoc.Visible = False

            Else

            End If

            sqlDDT.SelectCommand = txtQuery.Text & " ORDER BY data_creazione DESC"
            sqlDDT.DataBind()

            If id_ddt.Text <> "" Then
                stazione_rientro_ins.Enabled = False
                stazione_uscita_ins.Enabled = False

            End If
        Catch ex As Exception
            Response.Write("error_Page_Load_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Sub


    Protected Sub ListViewAllegati_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListViewAllegati.ItemCommand

        Try
            If e.CommandName = "SelezionaAllegato" Then

                Dim NomeFile As Label = e.Item.FindControl("lblNomeFile")
                Dim PercFile As Label = e.Item.FindControl("lblPercorsoFile")




                Dim posizione As Integer = PercFile.Text.IndexOf("trasporto_veicoli")
                Dim newPercorso As String = Mid(Replace(PercFile.Text, "\", "/"), posizione + 1) 'restituisce una stringa a partire dalla posizione specificata dopo averla convertita
                newPercorso = "/allegati_veicoli/" & newPercorso & NomeFile.Text 'aggiungo il nome del file al precorso

                'Dim filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "allegati_veicoli\trasporto_veicoli\" & nomedir & "\"

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
                my_allegatiT.DeleteAllegatoT("trasporto_veicoli_allegati")
                AggiornaListAllegatiT(CInt(id_ddt.Text))


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

        Dim sqlstr As String = "SELECT dbo.trasporto_veicoli_allegati.Id, dbo.trasporto_veicoli_TipoAllegato.TipoAllegato, dbo.trasporto_veicoli_allegati.NomeFile, dbo.trasporto_veicoli_allegati.PercorsoFile "
        sqlstr += "From dbo.trasporto_veicoli_allegati WITH (NOLOCK) INNER Join dbo.trasporto_veicoli_TipoAllegato WITH (NOLOCK) ON dbo.trasporto_veicoli_allegati.Idtipodocumento = dbo.trasporto_veicoli_TipoAllegato.Id "
        sqlstr += "Where dbo.trasporto_veicoli_allegati.Id_rif = " & id_rif & ";"

        sqlAllegati.SelectCommand = sqlstr

        ListViewAllegati.DataBind()
        ListViewAllegati.Visible = True



    End Sub


    Protected Sub Test()

        Dim Messaggio As String = ""
        If FileUploadAllegati.HasFile Then
            Dim estensione As String = LCase(Right(FileUploadAllegati.FileName, 4))
            If estensione = ".jpg" Or estensione = ".png" Or estensione = ".pdf" Then
                'Trace.Write("FileUpload1.PostedFile.ContentLength:" & FileUploadAllegati.PostedFile.ContentLength)
                If FileUploadAllegati.PostedFile.ContentLength <= 2000000 Then
                    Dim filePath As String
                    Dim data_uscita As String = FormatDateTime(lblDataDiUscita.Text, vbShortDate)
                    Dim anomedir() As String = Split(data_uscita, "/")
                    Dim nomedir As String = anomedir(2) & anomedir(1) & anomedir(0)

                    filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "allegati_veicoli\trasporto_veicoli\" & nomedir & "\"

                    If Directory.Exists(filePath) = False Then
                        'creo la directory
                        Directory.CreateDirectory(filePath)
                    End If

                    Dim NomeFile As String
                    'NomeFile = System.Guid.NewGuid().ToString
                    Dim dataTemp As Date = Now
                    NomeFile = Year(dataTemp) & Month(dataTemp) & Day(dataTemp) & Hour(dataTemp) & Minute(dataTemp) & Second(dataTemp)
                    Dim fileNameBig As String = id_ddt.Text & "_" & anomedir(2) & "_" & DropTipoAllegato.SelectedValue.ToString & "_"
                    fileNameBig = fileNameBig & "_" & NomeFile & estensione

                    FileUploadAllegati.SaveAs(filePath & fileNameBig)
                    Dim my_allegatit As AllegatiT = New AllegatiT

                    If File.Exists(filePath & fileNameBig) Then
                        'qui il codice per registrare il percorso del file nell'apposita tabella

                        With my_allegatit
                            .DataCreazione = Now
                            .IdTipoDocumento = DropTipoAllegato.SelectedValue
                            .NomeFile = fileNameBig
                            .PercorsoFile = filePath
                            .Id_rif = CInt(id_ddt.Text)
                            my_allegatit.InsertAllegatoT("trasporto_veicoli_allegati")
                        End With

                        AggiornaListAllegatiT(my_allegatit.Id_rif)

                        DropTipoAllegato.SelectedValue = 0
                        Messaggio = "Documento correttamente salvata."
                    End If
                Else
                    Messaggio = "Il file non può essere caricato perché supera 2MB!"
                End If
            Else
                Messaggio = "L'estensione dell'immagine deve essere con estensione (jpg, png, pdf)"
            End If
        Else
            Messaggio = "Selezionare un file da salvare."
        End If

        If Messaggio <> "" Then
            Libreria.genUserMsgBox(Page, Messaggio)
        End If
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

        If FileUploadAllegati.HasFile And DropTipoAllegato.SelectedValue <> "0" Then
            Dim estensione As String = LCase(Right(FileUploadAllegati.FileName, 4))
            If estensione = ".jpg" Or estensione = ".png" Or estensione = ".pdf" Then
                'Trace.Write("FileUpload1.PostedFile.ContentLength:" & FileUploadAllegati.PostedFile.ContentLength)
                If FileUploadAllegati.PostedFile.ContentLength <= 2000000 Then
                    Dim filePath As String
                    Dim data_uscita As String = FormatDateTime(lblDataDiUscita.Text, vbShortDate)
                    Dim anomedir() As String = Split(data_uscita, "/")
                    Dim nomedir As String = anomedir(2) & anomedir(1) & anomedir(0)

                    filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "allegati_veicoli\trasporto_veicoli\" & nomedir & "\"

                    If Directory.Exists(filePath) = False Then
                        'creo la directory
                        Directory.CreateDirectory(filePath)
                    End If

                    Dim NomeFile As String
                    'NomeFile = System.Guid.NewGuid().ToString
                    Dim dataTemp As Date = Now
                    NomeFile = nomedir & "_" & Hour(dataTemp) & Minute(dataTemp) & Second(dataTemp)
                    Dim fileNameBig As String = NomeFile & "_" & id_ddt.Text & "_" & DropTipoAllegato.SelectedValue.ToString & "_"
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
                            .Id_rif = CInt(id_ddt.Text)
                            my_allegatit.InsertAllegatoT("trasporto_veicoli_allegati")
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

    End Sub



    Protected Sub chiudiDdt(ByVal idDdt As String)
        Dim sqlStr As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc2.Open()
            Dim Cmd2 As New Data.SqlClient.SqlCommand("", Dbc2)

            'PER PRIMA COSA SETTO CORRETTAMENTE IN STATO DI CHIUSO IL DDT
            sqlStr = "UPDATE documenti_di_trasporto SET data_rientro=convert(datetime,getDate(),102), id_operatore_chiusura='" & Request.Cookies("SicilyRentCar")("IdUtente") & "',"
            sqlStr += "stato_trasferimento='E' WHERE id='" & idDdt & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            'QUINDI SETTO LE AUTO COLLEGATE AL DDT CORRENTE COME DISPONIBILI PER IL NOLEGGIO. INOLTRE SETTO LA LORO TANGA COME QUELLA SEGNALATA
            'NEL DDT

            sqlStr = "SELECT id_veicolo, serbatoio_uscita FROM documenti_di_trasporto_auto WITH(NOLOCK) WHERE id_documento_trasporto='" & idDdt & "'"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Do While Rs.Read()
                Cmd2 = New Data.SqlClient.SqlCommand("UPDATE veicoli SET disponibile_nolo='1', noleggiata='0', id_stazione='" & stazione_rientro_ins.SelectedValue & "' WHERE id='" & Rs("id_veicolo") & "'", Dbc2)
                Cmd2.ExecuteNonQuery()

                sqlStr = "UPDATE movimenti_targa SET data_rientro=convert(datetime,getDate(),102), id_stazione_rientro='" & stazione_rientro_ins.SelectedValue & "', km_rientro=km_uscita,"
                sqlStr += "serbatoio_rientro=serbatoio_uscita, id_operatore_rientro='" & Request.Cookies("SicilyRentCar")("IdUtente") & "', data_registrazione_rientro=convert(datetime,getDate(),102), "
                sqlStr += "movimento_attivo='0' WHERE num_riferimento='" & idDdt & "' AND id_tipo_movimento='" & Costanti.idBisarca & "' AND id_veicolo='" & Rs("id_veicolo") & "'"
                Cmd2 = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                Cmd2.ExecuteNonQuery()
            Loop

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Cmd2.Dispose()
            Cmd2 = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
            Dbc2.Close()
            Dbc2.Dispose()
            Dbc = Nothing





        Catch ex As Exception
            Response.Write("error_ ChiudiDDT _" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Sub

    Protected Function auto_in_stazione(ByVal targa As String, ByVal stazione As String) As Boolean
        Dim sqlstr As String = "SELECT id FROM veicoli WITH(NOLOCK) WHERE targa='" & Replace(targa, "'", "''") & "' AND id_stazione='" & stazione & "'"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test = "" Then
                auto_in_stazione = False
            Else
                auto_in_stazione = True
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_auto_in_stazione_" & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try

    End Function

    Protected Function auto_noleggiata(ByVal targa As String) As Boolean

        Dim sqlstr As String = "SELECT ISNULL(noleggiata,'0') FROM veicoli WITH(NOLOCK) WHERE targa='" & Replace(targa, "'", "''") & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            Dim test As Boolean = Cmd.ExecuteScalar

            If test Then
                auto_noleggiata = True
            Else
                auto_noleggiata = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_auto_noleggiata_" & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try




    End Function

    Protected Function auto_venduta(ByVal targa As String) As Boolean

        Dim sqlstr As String = "SELECT ISNULL(venduta,'0') FROM veicoli WITH(NOLOCK) WHERE targa='" & Replace(targa, "'", "''") & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            Dim test As Boolean = Cmd.ExecuteScalar

            If test Then
                auto_venduta = True
            Else
                auto_venduta = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_auto_venduta_" & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try




    End Function

    Protected Function auto_in_altro_ddt(ByVal targa As String, ByVal idDdt As String) As Boolean
        Dim sqlstr As String = "SELECT documenti_di_trasporto.id FROM documenti_di_trasporto WITH(NOLOCK) INNER JOIN documenti_di_trasporto_auto WITH(NOLOCK) ON documenti_di_trasporto.id=documenti_di_trasporto_auto.id_documento_trasporto WHERE documenti_di_trasporto_auto.id_veicolo='" & get_id_veicolo(targa) & "' AND documenti_di_trasporto.id<>'" & idDdt & "' AND stato_trasferimento='I'"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test <> "" Then
                auto_in_altro_ddt = True
            Else
                auto_in_altro_ddt = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_auto_in_altro_ddt_" & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try



    End Function

    Protected Function get_veicoli_in_ddt(ByVal idDddt As String) As String()


        Dim sql As String = "SELECT id_veicolo FROM documenti_di_trasporto_auto WITH(NOLOCK) WHERE id_documento_trasporto='" & idDddt & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Dim veicoli(50) As String

            Dim i As Integer = 1

            Do While Rs.Read()
                veicoli(i) = Rs("id_veicolo")
                i = i + 1
            Loop

            get_veicoli_in_ddt = veicoli

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_get_veicoli_in_ddt_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try



    End Function


    Protected Sub elimina_righe(ByVal idDDT As String)

        Dim sql As String = ""

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            'PRIMA DI ELIMINARE LE AUTO DAL DDT CORRENTE SETTO IL LORO STATO COME DISPONIBILE PER IL NOLEGGIO, QUESTO QUALORA, IN MODIFICA, 
            'SIA STATO RIMOSSO UNA O PIU' VEICOLI DAL DDT
            sql = "UPDATE veicoli SET disponibile_nolo='1' WHERE veicoli.id IN (SELECT id FROM documenti_di_trasporto_auto WHERE id_documento_trasporto='" & idDDT & "')"
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)
            Cmd.ExecuteNonQuery()

            sql = "DELETE FROM documenti_di_trasporto_auto WHERE id_documento_trasporto='" & idDDT & "'"

            Cmd = New Data.SqlClient.SqlCommand(sql, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_elimina_righe_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try


    End Sub

    Protected Sub inserisci_auto(ByVal targa As String, ByVal km As String, ByVal tanga As String, ByVal id_ddt As String)

        Dim sql As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim my_tanga() As String = Split(tanga, "/")
            Dim id_veicolo As Integer = get_id_veicolo(targa)
            Dim data_pres_rientro As String = funzioni_comuni.GetDataSql(data_rientro_ins.Text, 99) & " " & ore_rientro.SelectedValue & ":" & minuti_rientro.SelectedValue & ":00"

            sql = "INSERT INTO documenti_di_trasporto_auto(id_documento_trasporto, id_veicolo, serbatoio_uscita, km_uscita) VALUES ("
            sql += "'" & id_ddt & "','" & id_veicolo & "','" & my_tanga(0) & "','" & km & "')"

            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)
            Cmd.ExecuteNonQuery()

            'SALVATAGGIO DELLA RIGA DI MOVIMENTO AUTO
            sql = "insert into movimenti_targa (num_riferimento, id_veicolo, id_tipo_movimento, data_uscita, id_stazione_uscita, data_presunto_rientro, id_stazione_presunto_rientro, "
            sql += " km_uscita, serbatoio_uscita, id_operatore, data_registrazione, movimento_attivo) "
            sql += "VALUES"
            sql += " ('" & id_ddt & "','" & id_veicolo & "','" & Costanti.idBisarca & "', convert(datetime,getDate(),102) ,'" & stazione_uscita_ins.SelectedValue & "',"
            sql += "convert(datetime,'" & data_pres_rientro & "',102),'" & stazione_rientro_ins.SelectedValue & "',"
            sql += "'" & km & "','" & my_tanga(0) & "','" & Request.Cookies("SicilyRentCar")("IdUtente") & "',convert(datetime,GetDate(),102),'1')"

            Cmd = New Data.SqlClient.SqlCommand(sql, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing


        Catch ex As Exception
            Response.Write("error_inserisci_auto_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try
    End Sub

    Protected Function creaDDT() As String
        Dim sql As String = ""

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim data_rientro As String = funzioni_comuni.GetDataSql(data_rientro_ins.Text, 99) & " " & ore_rientro.SelectedValue & ":" & minuti_rientro.SelectedValue & ":00"


            sql = "INSERT INTO documenti_di_trasporto (id_stazione_uscita,id_stazione_rientro,data_uscita, data_presunto_rientro, data_creazione, note, id_operatore_inserimento, stato_trasferimento) VALUES ("
            sql += "'" & stazione_uscita_ins.SelectedValue & "','" & stazione_rientro_ins.SelectedValue & "',"
            sql += "convert(datetime,getDate(),102),CONVERT(DATETIME,'" & data_rientro & "',102),convert(datetime,getDate(),102),Null,'" & Request.Cookies("SicilyRentCar")("IdUtente") & "','I')"

            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)
            Cmd.ExecuteNonQuery()

            'RECUPERO E RESTITUISCO L'ID DEL DOCUMENTO APPENA INSERITO
            sql = "SELECT Max(id) FROM  documenti_di_trasporto WITH(NOLOCK)"
            Cmd = New Data.SqlClient.SqlCommand(sql, Dbc)

            creaDDT = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            Response.Write("error_CreaDDT_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try


    End Function

    Protected Sub modificaDDT(ByVal idDdt As String)
        Dim sql As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim data_rientro As String = funzioni_comuni.GetDataSql(data_rientro_ins.Text, 99) & " " & ore_rientro.SelectedValue & ":" & minuti_rientro.SelectedValue & ":00"

            sql = "UPDATE documenti_di_trasporto SET id_stazione_uscita='" & stazione_uscita_ins.SelectedValue & "',"
            sql += "id_stazione_rientro='" & stazione_rientro_ins.SelectedValue & "', data_presunto_rientro=convert(datetime,'" & data_rientro & "',102), id_operatore_ultima_modifica='" & Request.Cookies("SicilyRentCar")("IdUtente") & "' WHERE id='" & idDdt & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_ModificaDDT_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Sub

    Protected Function getTarga(ByVal id_veicolo As String) As String

        Dim sql As String = "SELECT targa FROM veicoli WITH(NOLOCK) WHERE id='" & id_veicolo & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)


            getTarga = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_getTarga_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Function

    Protected Function getNumeroAuto(ByVal idDdt As String) As String
        Dim sql As String = "SELECT COUNT(id) FROM documenti_di_trasporto_auto WITH(NOLOCK) WHERE id_documento_trasporto='" & idDdt & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            getNumeroAuto = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_GetNumeroAuto_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Function

    Protected Function getKM(ByVal targa As String) As String
        Dim sql As String = "SELECT km_attuali FROM veicoli WITH(NOLOCK) WHERE targa='" & txtTarga.Text & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            getKM = Cmd.ExecuteScalar & ""


            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_getKM_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Function

    Protected Function getKM_uscita(ByVal id_auto As String, ByVal idDdt As String) As String
        Dim sql As String = "SELECT km_uscita FROM documenti_di_trasporto_auto WITH(NOLOCK) WHERE id_veicolo='" & id_auto & "' AND id_documento_trasporto='" & idDdt & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            getKM_uscita = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_getKM_uscita_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Function

    Protected Function getSerbatoio(ByVal targa As String) As String

        Dim sql As String = "SELECT veicoli.serbatoio_attuale FROM veicoli WITH(NOLOCK)  WHERE targa='" & txtTarga.Text & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            getSerbatoio = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_getSerbatoio_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Function

    Protected Function getSerbatoioMax(ByVal targa As String) As String

        Dim sql As String = "SELECT id_modello FROM veicoli WITH(NOLOCK) WHERE targa='" & txtTarga.Text & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            Dim modello As String = Cmd.ExecuteScalar

            sql = "SELECT capacita_serbatoio FROM modelli WITH(NOLOCK) WHERE id_modello='" & modello & "'"
            Cmd = New Data.SqlClient.SqlCommand(sql, Dbc)

            getSerbatoioMax = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_getSerbatoioMAX_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Function

    Protected Function getSerbatoioMax_byId(ByVal idAuto As String) As String

        Dim sql As String = "SELECT id_modello FROM veicoli WITH(NOLOCK) WHERE id='" & idAuto & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            Dim modello As String = Cmd.ExecuteScalar
            sql = "SELECT modelli.capacita_serbatoio FROM modelli WITH(NOLOCK) WHERE id_modello='" & modello & "'"
            Cmd = New Data.SqlClient.SqlCommand(sql, Dbc)

            getSerbatoioMax_byId = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_getSerbatoioMAX_byId_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try


    End Function

    Protected Function getSerbatoio_uscita(ByVal id_auto As String, ByVal idDdt As String) As String
        Dim sql As String = "SELECT serbatoio_uscita FROM documenti_di_trasporto_auto WITH(NOLOCK) WHERE id_veicolo='" & id_auto & "' AND id_documento_trasporto='" & idDdt & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            getSerbatoio_uscita = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_getSerbatoioMAX_uscita_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Function

    Protected Function get_id_veicolo(ByVal targa As String) As String
        Dim sql As String = "SELECT id FROM veicoli WITH(NOLOCK) WHERE targa='" & Replace(targa, "'", "''") & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            get_id_veicolo = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_getIdveicolo_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Function

    Protected Function auto_esistente(ByVal targa As String) As Boolean
        Dim sql As String = "SELECT id FROM veicoli WITH(NOLOCK) WHERE targa='" & Replace(txtTarga.Text, "'", "''") & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test = "" Then
                auto_esistente = False
            Else
                auto_esistente = True
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error_auto_esistente_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Function

    Protected Sub setQuery()

        Dim sql As String = "SELECT documenti_di_trasporto.id, documenti_di_trasporto.stato_trasferimento,"
        sql += "STR(stazioni_uscita.codice) + ' - ' + stazioni_uscita.nome_stazione As stazione_uscita,"
        sql += "STR(stazioni_rientro.codice) + ' - ' + stazioni_rientro.nome_stazione As stazione_rientro,documenti_di_trasporto.data_uscita,"
        sql += "documenti_di_trasporto.data_presunto_rientro, documenti_di_trasporto.data_rientro, documenti_di_trasporto.note "
        sql += "FROM documenti_di_trasporto WITH(NOLOCK) INNER JOIN stazioni As stazioni_uscita WITH(NOLOCK) ON documenti_di_trasporto.id_stazione_uscita=stazioni_uscita.id "
        sql += "INNER JOIN stazioni As stazioni_rientro WITH(NOLOCK) ON documenti_di_trasporto.id_stazione_rientro=stazioni_rientro.id WHERE documenti_di_trasporto.id > 0"

        Try
            sqlDDT.SelectCommand = sql
            Dim condizioneWhere As String = ""

            If dropStatoTrasferimento.SelectedValue <> "0" Then
                condizioneWhere = condizioneWhere & " AND stato_trasferimento='" & dropStatoTrasferimento.SelectedValue & "'"
            End If

            If Trim(txtNumeroDDT.Text) <> "" Then
                condizioneWhere = condizioneWhere & " AND documenti_di_trasporto.id='" & txtNumeroDDT.Text & "'"
            End If

            If dropStazioneUscita.SelectedValue <> "0" Then
                condizioneWhere = condizioneWhere & " AND id_stazione_uscita='" & dropStazioneUscita.SelectedValue & "'"
            End If

            If dropStazioneRientro.SelectedValue <> "0" Then
                condizioneWhere = condizioneWhere & " AND id_stazione_rientro='" & dropStazioneRientro.SelectedValue & "'"
            End If

            If Trim(data_presunto_rientro_da.Text) <> "" And Trim(data_presunto_rientro_a.Text) = "" Then
                Dim da As String = funzioni_comuni.GetDataSql(data_presunto_rientro_da.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(data_presunto_rientro_da.Text)
                condizioneWhere += " AND data_presunto_rientro >=CONVERT(DATETIME,'" & da & "',102)"
            End If

            If Trim(data_presunto_rientro_da.Text) = "" And Trim(data_presunto_rientro_a.Text) <> "" Then
                Dim a As String = funzioni_comuni.GetDataSql(data_presunto_rientro_a.Text, 59) 'funzioni_comuni.getDataDb_con_orario(data_presunto_rientro_a.Text & " 23:59:59")
                condizioneWhere += " AND data_presunto_rientro<=CONVERT(DATETIME,'" & a & "',102)"
            End If

            If Trim(data_presunto_rientro_da.Text) <> "" And Trim(data_presunto_rientro_a.Text) <> "" Then
                Dim da As String = funzioni_comuni.GetDataSql(data_presunto_rientro_da.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(data_presunto_rientro_da.Text)
                Dim a As String = funzioni_comuni.GetDataSql(data_presunto_rientro_a.Text, 59) 'funzioni_comuni.getDataDb_con_orario(data_presunto_rientro_a.Text & " 23:59:59")
                condizioneWhere += " AND data_presunto_rientro BETWEEN CONVERT(DATETIME,'" & da & "',102) AND CONVERT(DATETIME,'" & a & "',102)"
            End If

            If Trim(data_uscita_da.Text) <> "" And Trim(data_uscita_a.Text) = "" Then
                Dim da As String = funzioni_comuni.GetDataSql(data_uscita_da.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(data_uscita_da.Text)
                condizioneWhere += " AND data_uscita >=CONVERT(DATETIME,'" & da & "',102)"
            End If

            If Trim(data_uscita_da.Text) = "" And Trim(data_uscita_a.Text) <> "" Then
                Dim a As String = funzioni_comuni.GetDataSql(data_uscita_a.Text, 59) 'funzioni_comuni.getDataDb_con_orario(data_uscita_a.Text & " 23:59:59")
                condizioneWhere += " AND data_uscita<=CONVERT(DATETIME,'" & a & "',102)"
            End If

            If Trim(data_uscita_da.Text) <> "" And Trim(data_uscita_a.Text) <> "" Then
                Dim da As String = funzioni_comuni.GetDataSql(data_uscita_da.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(data_uscita_da.Text)
                Dim a As String = funzioni_comuni.GetDataSql(data_uscita_a.Text, 59) 'funzioni_comuni.getDataDb_con_orario(data_uscita_a.Text & " 23:59:59")
                condizioneWhere += " AND data_uscita BETWEEN CONVERT(DATETIME,'" & da & "',102) AND CONVERT(DATETIME,'" & a & "',102)"
            End If

            '----------------

            If Trim(data_rientro_da.Text) <> "" And Trim(data_rientro_a.Text) = "" Then
                Dim da As String = funzioni_comuni.GetDataSql(data_rientro_da.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(data_rientro_da.Text)
                condizioneWhere += " AND data_rientro >=CONVERT(DATETIME,'" & da & "',102)"
            End If

            If Trim(data_rientro_da.Text) = "" And Trim(data_rientro_a.Text) <> "" Then
                Dim a As String = funzioni_comuni.GetDataSql(data_rientro_a.Text, 59) 'funzioni_comuni.getDataDb_con_orario(data_rientro_a.Text & " 23:59:59")
                condizioneWhere += " AND data_rientro<=CONVERT(DATETIME,'" & a & "',102)"
            End If

            If Trim(data_rientro_da.Text) <> "" And Trim(data_rientro_a.Text) <> "" Then
                Dim da As String = funzioni_comuni.GetDataSql(data_rientro_da.Text, 0) 'funzioni_comuni.getDataDb_senza_orario(data_rientro_da.Text)
                Dim a As String = funzioni_comuni.GetDataSql(data_rientro_a.Text, 59) 'funzioni_comuni.getDataDb_con_orario(data_rientro_a.Text & " 23:59:59")
                condizioneWhere += " AND data_rientro BETWEEN CONVERT(DATETIME,'" & da & "',102) AND CONVERT(DATETIME,'" & a & "',102)"
            End If

            sql = sqlDDT.SelectCommand & condizioneWhere & " ORDER BY data_creazione DESC"

            txtQuery.Text = sqlDDT.SelectCommand & condizioneWhere & " ORDER BY data_creazione DESC"
            sqlDDT.SelectCommand = txtQuery.Text

            listDDT.DataBind()

            'Response.Write(sql & "<br/>")


        Catch ex As Exception
            Response.Write("error_setQuery_" & ex.Message & "<br/>" & sql & "<br/>")
        End Try

    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerca.Click
        Try
            setQuery()
        Catch ex As Exception
            Response.Write("error_setQuery_click_" & ex.Message & "<br/>")
        End Try

    End Sub

    Protected Sub btnNuovo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovo.Click
        id_ddt.Text = ""
        btnSalva.Enabled = True

        stazione_rientro_ins.Enabled = True

        stazione_rientro_ins.Items.Clear()
        stazione_rientro_ins.Items.Add("Seleziona...")
        stazione_rientro_ins.Items(0).Value = "0"

        stazione_uscita_ins.Items.Clear()
        stazione_uscita_ins.Items.Add("Seleziona...")
        stazione_uscita_ins.Items(0).Value = "0"

        stazione_rientro_ins.DataBind()
        stazione_uscita_ins.DataBind()

        stazione_uscita_ins.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
        stazione_uscita_ins.Enabled = False

        txtTarga.Text = ""
        lblDescDataUscita.Visible = False
        lblDataDiUscita.Text = ""
        lblDescDataRientro.Visible = False
        lblDataDiRientro.Text = ""
        lblStato.Text = ""
        stazione_rientro_ins.SelectedValue = "0"

        data_rientro_ins.Text = ""
        minuti_rientro.SelectedValue = "-1"
        ore_rientro.SelectedValue = "-1"

        pannello_ricerca.Visible = False
        pannello_ddt.Visible = True

        'abilita pulsanti 21.09.2022 salvo
        data_rientro_ins.Enabled = True
        ore_rientro.Enabled = True
        minuti_rientro.Enabled = True




    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click
        Try
            id_ddt.Text = ""

            setQuery()
            listDDT.DataBind()

            pannello_ricerca.Visible = True
            pannello_ddt.Visible = False

            divAllegInvioDoc.Visible = False    '20.09.2022 salvo


        Catch ex As Exception
            Response.Write("error_btnAnnulla_Click_" & ex.Message & "<br/>")
        End Try

    End Sub

    Protected Sub listDDT_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listDDT.ItemCommand
        Dim sql As String = ""

        If e.CommandName = "vedi" Then
            Try
                Dim id_riga As Label = e.Item.FindControl("idLabel")
                sql = "SELECT * FROM documenti_di_trasporto WITH(NOLOCK) WHERE id='" & id_riga.Text & "'"
                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)

                Dim Rs As Data.SqlClient.SqlDataReader
                Rs = Cmd.ExecuteReader()


                If Rs.Read() Then
                    lblCodiceStato.Text = Rs("stato_trasferimento")

                    If Rs("stato_trasferimento") = "I" Then
                        lblStato.Text = "In Corso"
                        btnSalva.Enabled = True
                    ElseIf Rs("stato_trasferimento") = "A" Then
                        lblStato.Text = "Annullato"
                        btnSalva.Enabled = False
                    ElseIf Rs("stato_trasferimento") = "E" Then
                        lblStato.Text = "Effettuato"
                        btnSalva.Enabled = False
                    End If

                    lblDescDataUscita.Visible = True
                    lblDataDiUscita.Text = Rs("data_uscita")

                    If (Rs("data_rientro") & "") <> "" Then
                        lblDescDataRientro.Visible = True
                        lblDataDiRientro.Text = Rs("data_rientro")
                    Else
                        lblDescDataRientro.Visible = False
                        lblDataDiRientro.Text = ""
                    End If

                    stazione_rientro_ins.Items.Clear()
                    stazione_rientro_ins.Items.Add("Seleziona...")
                    stazione_rientro_ins.Items(0).Value = "0"

                    stazione_uscita_ins.Items.Clear()
                    stazione_uscita_ins.Items.Add("Seleziona...")
                    stazione_uscita_ins.Items(0).Value = "0"

                    stazione_rientro_ins.Enabled = False
                    stazione_uscita_ins.Enabled = False

                    stazione_rientro_ins.DataBind()
                    stazione_uscita_ins.DataBind()

                    stazione_uscita_ins.SelectedValue = Rs("id_stazione_uscita")
                    stazione_rientro_ins.SelectedValue = Rs("id_stazione_rientro")

                    data_rientro_ins.Text = Format(Rs("data_presunto_rientro"), "dd/MM/yyyy")

                    Dim ore As String = Hour(Rs("data_presunto_rientro"))
                    If Len(ore) = 1 Then
                        ore = "0" & ore
                    End If

                    Dim minuti As String = Minute(Rs("data_presunto_rientro"))
                    If Len(minuti) = 1 Then
                        minuti = "0" & minuti
                    End If

                    ore_rientro.SelectedValue = ore
                    minuti_rientro.SelectedValue = minuti

                    txtTarga.Text = ""

                    id_ddt.Text = id_riga.Text
                    pannello_ricerca.Visible = False
                    pannello_ddt.Visible = True
                End If


                Rs.Close()
                Rs = Nothing
                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing


                divAllegInvioDoc.Visible = True 'vedi pannello allegati

                'disabilita campi 21.09.2022 salvo
                data_rientro_ins.Enabled = False
                ore_rientro.Enabled = False
                minuti_rientro.Enabled = False



                AggiornaListAllegatiT(CInt(id_riga.Text))


            Catch ex As Exception
                Response.Write("error_ItemCommand_if:" & ex.Message & "<br/>" & sql & "<br/>")
            End Try

        ElseIf e.CommandName = "annulla" Then

            Try
                Dim id_riga As Label = e.Item.FindControl("idLabel")

                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                sql = "UPDATE documenti_di_trasporto SET stato_trasferimento='A', id_operatore_annullamento='" & Request.Cookies("SicilyRentCar")("IdUtente") & "', data_annullamento=convert(datetime,getDate(),102) WHERE id='" & id_riga.Text & "'"
                Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)
                Cmd.ExecuteNonQuery()

                'SETTO I VEICOLI COME DISPONIBILI (modificato 02.12.2020)
                sql = "UPDATE veicoli SET disponibile_nolo='1', noleggiata='0' WHERE veicoli.id IN (SELECT id_veicolo FROM documenti_di_trasporto_auto WITH(NOLOCK) WHERE id_documento_trasporto='" & id_riga.Text & "')"
                Cmd = New Data.SqlClient.SqlCommand(sql, Dbc)
                Cmd.ExecuteNonQuery()

                sql = "SELECT documenti_di_trasporto.id, documenti_di_trasporto.stato_trasferimento,STR(stazioni_uscita.codice) + ' - ' + stazioni_uscita.nome_stazione As stazione_uscita,STR(stazioni_rientro.codice) + ' - ' + stazioni_rientro.nome_stazione As stazione_rientro,documenti_di_trasporto.data_uscita,documenti_di_trasporto.data_presunto_rientro, documenti_di_trasporto.data_rientro, documenti_di_trasporto.note FROM documenti_di_trasporto WITH(NOLOCK) INNER JOIN stazioni As stazioni_uscita WITH(NOLOCK) ON documenti_di_trasporto.id_stazione_uscita=stazioni_uscita.id INNER JOIN stazioni As stazioni_rientro WITH(NOLOCK) ON documenti_di_trasporto.id_stazione_rientro=stazioni_rientro.id WHERE documenti_di_trasporto.id > 0 AND stato_trasferimento='I' ORDER BY data_creazione DESC"
                sqlDDT.SelectCommand = sql
                listDDT.DataBind()

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

            Catch ex As Exception
                Response.Write("error_ItemCommand_elseif annulla 2:" & ex.Message & "<br/>" & sql & "<br/>")
            End Try


        End If
    End Sub

    Protected Sub setta_auto_non_disponibile(ByVal idDdt As String)
        Dim sql As String = "UPDATE veicoli SET disponibile_nolo='0', noleggiata='1' WHERE veicoli.id IN (SELECT id_veicolo FROM documenti_di_trasporto_auto WITH(NOLOCK) WHERE id_documento_trasporto='" & idDdt & "')"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sql, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Response.Write("error setta_auto_non_disponibile:" & ex.Message & "<br/>" & sql & "<br/>")
        End Try


    End Sub

    Protected Sub listDDT_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listDDT.ItemDataBound
        Dim stato As Label = e.Item.FindControl("stato_trasferimentoLabel")
        Dim annulla As ImageButton = e.Item.FindControl("btnAnnulla")

        If stato.Text = "I" Then
            stato.Text = "In Corso"
            annulla.ToolTip = "Annulla Trasporto."
        ElseIf stato.Text = "A" Then
            stato.Text = "Annullato"
            annulla.Enabled = False
            annulla.ToolTip = "Trasporto già annullato."
        ElseIf stato.Text = "E" Then
            stato.Text = "Effettuato"
            annulla.Enabled = False
            annulla.ToolTip = "Impossibile annullare: trasporto eseguito."
        End If

    End Sub

    Protected Sub btnMemorizzaAlleg_Click1(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnMemorizzaAlleg_Click2(sender As Object, e As EventArgs)
        Test()

    End Sub

    Private Sub btnSalva_Click(sender As Object, e As EventArgs) Handles btnSalva.Click

        'disabilita campi 21.09.2022 salvo
        data_rientro_ins.Enabled = False
        ore_rientro.Enabled = False
        minuti_rientro.Enabled = False


    End Sub
End Class
