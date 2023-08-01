Imports System.IO
Imports funzioni_comuni
Partial Class ImportAssicurazioniVeicoli
    Inherits System.Web.UI.Page
    Dim funzioni As New funzioni_comuni

    Protected Function ordineETargaNonInseriti(ByVal num_ordine As String, ByVal id_veicolo As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM veicoli_assicurazioni WHERE ordine='" & num_ordine & "' AND id_parco='" & id_veicolo & "'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test <> "" Then
            ordineETargaNonInseriti = False
        Else
            ordineETargaNonInseriti = True
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Sub CancellaTabAppoggioAssicurazioni()
        Dim DbcCancellaInTabAppoggio As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
        DbcCancellaInTabAppoggio.Open()
        Dim CmdCancellaInTabAppoggio As New Data.SqlClient.SqlCommand("", DbcCancellaInTabAppoggio)

        CmdCancellaInTabAppoggio.CommandText = "delete from assicurazioni_appoggio "
        'Response.Write(CmdCancellaInTabAppoggio.CommandText & "<br>")
        'Response.End()

        CmdCancellaInTabAppoggio.ExecuteNonQuery()
        CmdCancellaInTabAppoggio.Dispose()
        DbcCancellaInTabAppoggio.Close()
        DbcCancellaInTabAppoggio = Nothing
    End Sub

    Public Function CaricaFoglioExcelInDB(ByVal RigaStringa As String) As Boolean
        Dim RigaStrApp(1) As String
        Dim presente_targa As Char

        Dim id_veicolo As String

        Dim riga_duplicata As Boolean = False

        Dim DbcSalvataggioInTabAppoggio As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
        DbcSalvataggioInTabAppoggio.Open()
        Dim CmdSalvataggioInTabAppoggio As New Data.SqlClient.SqlCommand("", DbcSalvataggioInTabAppoggio)

        RigaStrApp = Split(RigaStringa, ";")
        'For i = 0 To 9
        'Response.Write(RigaStrApp(i) & "<br>")
        'Next

        'Controllo esistenza targa
        Dim Dbc_targa As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
        Dbc_targa.Open()
        Dim Cmd_targa As New Data.SqlClient.SqlCommand("SELECT id FROM veicoli WHERE targa='" & RigaStrApp(1) & "'", Dbc_targa)
        'Response.Write(Cmd_targa.CommandText & "<br><br>")
        'Response.End()

        'Controllo esistenza TARGA - DEVE ESISTERE IN QUANTO LA FUNZIONALITA' PUO' SOLO ESEGUIRE UN UPDATE SU UN VEICOLO ESISTENTE
        Dim Rs_targa As Data.SqlClient.SqlDataReader
        Rs_targa = Cmd_targa.ExecuteReader()
        If Rs_targa.HasRows Then
            Rs_targa.Read()
            id_veicolo = "'" & Rs_targa("id") & "'"
            presente_targa = "1"

            ''CONTROLLO SE IL DATO E' DUPLICATO (NUM_ORDINE E TARGA UNIVOCI) MA SOLO SE IL NUMERO D'ORDINE E' UN INTERO (SE NON LO E' VERRA'
            ''COMUNQUE GENERATO UN ERRORE TIPO DI DATO ERRATO)
            'Try
            '    Dim test As Integer = CInt(RigaStrApp(0))
            '    If Not ordineETargaNonInseriti(RigaStrApp(0), Rs_targa("id")) Then
            '        riga_duplicata = True
            '    End If
            'Catch ex As Exception

            'End Try
        Else
            id_veicolo = "NULL"
            presente_targa = "0"
        End If
        Rs_targa.Close()
        Dbc_targa.Close()
        Rs_targa = Nothing
        Dbc_targa = Nothing


        'If IsDate(RigaStrApp(2)) = False Then 'Data Inclusione non valida  
        '    lblErrore2.Text = "Il campo Data Inclusione ha un formato errato"
        '    Exit Function
        'End If

        'Dim data_inclusione As String = getDataDb_senza_orario(RigaStrApp(2), Request.ServerVariables("HTTP_HOST"))

        CmdSalvataggioInTabAppoggio.CommandText = "insert into assicurazioni_appoggio (num_ordine,targa,id_veicolo,presente_targa, riga_duplicata, data_inclusione,valore_assicurato) " & _
                                                  "values('" & RigaStrApp(0) & "','" & RigaStrApp(1) & "'," & id_veicolo & ",'" & presente_targa & "','" & riga_duplicata & "','" & RigaStrApp(2) & "','" & RigaStrApp(3) & "')"
        'Response.Write(CmdSalvataggioInTabAppoggio.CommandText & "<br>")
        'Response.End()

        CmdSalvataggioInTabAppoggio.ExecuteNonQuery()
        CmdSalvataggioInTabAppoggio.Dispose()
        DbcSalvataggioInTabAppoggio.Close()
        DbcSalvataggioInTabAppoggio = Nothing

        LblImportFile.Text = "CaricaFile"
    End Function

    Public Function CampiObbligatoriOk(ByVal RigaStringa As String) As Boolean
        Dim RigaStringaSplittato(1) As String
        'Response.Write(RigaStringa & "<br>")
        RigaStringaSplittato = Split(RigaStringa, ";")
        If RigaStringaSplittato(0) = "" Then 'Num Ordine
            CampiObbligatoriOk = False
            'Response.Write("O1 <br>")
            Exit Function
        ElseIf RigaStringaSplittato(1) = "" Then 'Targa
            CampiObbligatoriOk = False
            'Response.Write("O1 <br>")
            Exit Function
        ElseIf RigaStringaSplittato(2) = "" Then 'Data Inclusione
            CampiObbligatoriOk = False
            'Response.Write("O2 <br>")
            Exit Function
        ElseIf RigaStringaSplittato(3) = "" Then 'Valore Assicurato
            CampiObbligatoriOk = False
            'Response.Write("O3 <br>")
            Exit Function
        End If

        CampiObbligatoriOk = True
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.Cookies("SicilyRentCar") Is Nothing Then
                Response.Redirect("LogIn.aspx")
            Else
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 38) <> "1" Then
                    'AzzeraTab()
                    'PanelDatiGenerali.Visible = True
                Else
                    Response.Redirect("default.aspx")
                End If
            End If
        End If
    End Sub

    Protected Sub importa_file()
        Dim filePath As String
        filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\docs"
        Dim fileName As String = FileUpload2.FileName

        Dim fileTesto As String = "AssicurazioniVeicoli_" & DatePart("d", Now) & DatePart("M", Now) & DatePart("yyyy", Now) & "_" & DatePart("h", Now) & DatePart("n", Now) & DatePart("s", Now) & ".csv"
        Dim fs = CreateObject("Scripting.FileSystemObject")
        Dim filetxt = fs.CreateTextFile(filePath & "\" & fileTesto, True) 'CREA IL FILE DI TESTO
        Dim FileTestoValorizzato As Boolean = False

        Dim CampiObbligatoriKO As Integer = 0
        Dim CampoTargaPresente As Integer = 0
        Dim CampoTelaioPresente As Integer = 0
        Dim CampoModelloNonPresente As Integer = 0
        Dim EscludiAmmortamento As Char = ""

        Dim CampoProprietarioNonPresente As Integer = 0

        Dim DataCorrente As String

        'Inserito Prima Riga        
        filetxt.writeline("NUMERO_ORDINE;TARGA;DATA_INCLUSIONE;VALORE_ASSICURATO")

        'Response.Write("A " & FileTestoValorizzato & "<br>")
        If FileUpload2.HasFile Then
            If FileUpload2.FileName = "PARCO_ASSICURAZIONI_DA_INSERIRE.csv" Then
                If File.Exists(filePath & "\" & fileName) Then
                    'Response.Write("IN")
                    'Response.End()
                    File.Delete(filePath & "\" & fileName)
                End If
                FileUpload2.SaveAs(filePath & "\" & fileName)

                'Aprire un file per la lettura        

                'Get a StreamReader class that can be used to read the file 'Prendi una classe StreamReader che può essere utilizzata per leggere il file
                Dim objStreamReader As StreamReader
                Dim Riga As String
                Dim RigaStr(1) As String

                objStreamReader = File.OpenText(filePath & "\" & fileName)

                CancellaTabAppoggioAssicurazioni()

                'Prima Riga
                Riga = objStreamReader.ReadLine()
                While (objStreamReader.Peek() <> -1)
                    Riga = objStreamReader.ReadLine()
                    RigaStr = Split(Riga, ";")
                    'Response.Write(Riga & "<br>")

                    CaricaFoglioExcelInDB(Riga)
                    'Response.End()

                    'Response.Write("B " & FileTestoValorizzato & "<br>")
                    If CampiObbligatoriOk(Riga) Then
                        'Response.Write("C1 " & FileTestoValorizzato & "<br>")
                        'Response.Write("Tutto Ok Campi <br>")
                        'Response.End()

                        'Identificazione Id x campo Targa                       
                        Dim DbcT As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
                        DbcT.Open()
                        Dim CmdT As New Data.SqlClient.SqlCommand("SELECT id FROM veicoli where targa='" & RigaStr(1) & "'", DbcT)
                        'Response.Write(CmdT.CommandText & "<br><br>")
                        'Response.End()
                        Dim RsT As Data.SqlClient.SqlDataReader
                        RsT = CmdT.ExecuteReader()

                        Dim id_veicolo As String = ""

                        If RsT.HasRows Then
                            RsT.Read()
                            CampoTargaPresente = CampoTargaPresente + 1
                            id_veicolo = RsT("id")
                            'Response.Write(RigaStr(1) & "  " & CampoModelloNonPresente & "<br><br>")
                        Else
                            'LA TARGA DEVE ESSERE TROVATO
                            FileTestoValorizzato = True
                        End If

                        RsT.Close()
                        DbcT.Close()
                        RsT = Nothing
                        DbcT = Nothing

                        'CORRETTEZZA DEL CAMPO DATA_INCLUSIONE - DEVE ESSERE UNA DATA
                        If IsDate(RigaStr(2)) = False Then 'Data Inclusione non valida  
                            FileTestoValorizzato = True
                        End If
                        'Response.Write("C2 " & FileTestoValorizzato & "<br>")

                        'CORRETTEZZA DEL CAMPO VALORE_ASSICURATO - DEVE ESSERE UN DOUBLE
                        Try
                            Dim test As Double = CDbl(RigaStr(3))
                        Catch ex As Exception
                            FileTestoValorizzato = True
                        End Try
                        'Response.Write("C3 " & FileTestoValorizzato & "<br>")

                        ''CORRETTEZZA DEL CAMPO NUM_ORDINE - DEVE ESSERE UN INTERO
                        ''SE E' CORRETTO E SE LA TARGA E' STATA TROVATO CONTROLLO CHE NON E' UN DUPLICATO
                        'Try
                        '    Dim test As Integer = CInt(RigaStr(0))
                        '    If id_veicolo <> "" Then
                        '        If Not ordineETargaNonInseriti(RigaStr(0), id_veicolo) Then
                        '            FileTestoValorizzato = True
                        '        End If
                        '    End If

                        'Catch ex As Exception
                        '    FileTestoValorizzato = True
                        'End Try
                        'Response.Write("C4 " & FileTestoValorizzato & "<br>")

                        'filetxt.writeline(Riga)

                    Else 'Controllo campi obbligatori (KO)
                        'Response.Write("Ko Campi <br>")
                        'Response.End()

                        'Response.Write("C2 " & FileTestoValorizzato & "<br>")

                        'filetxt.writeline(Riga)

                        FileTestoValorizzato = True
                        CampiObbligatoriKO = CampiObbligatoriKO + 1
                    End If
                End While

                objStreamReader.Close()

                filetxt.close()
                fs = Nothing

                'Response.End()


                If FileTestoValorizzato Then
                    lblErrore2.Text = "Dati non caricati sul Data Base (Verificare gli errori)"
                Else
                    Dim Dbc As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
                    Dbc.Open()
                    Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM assicurazioni_appoggio", Dbc)
                    'Response.Write(Cmd.CommandText & "<br><br>")
                    Dim Rs As Data.SqlClient.SqlDataReader
                    Rs = Cmd.ExecuteReader()
                    Do While Rs.Read
                        'Dati Generale Veicolo

                        Dim data_inclusione As String = getDataDb_senza_orario(Rs("data_inclusione"), Request.ServerVariables("HTTP_HOST"))

                        Dim DbcSalvataggio As New Data.SqlClient.SqlConnection(SqlDataSource1.ConnectionString)
                        DbcSalvataggio.Open()
                        Dim CmdSalvataggio As New Data.SqlClient.SqlCommand("", DbcSalvataggio)
                        CmdSalvataggio.CommandText =
                        "INSERT INTO veicoli_assicurazioni(id_compagnia,ordine,id_parco,valore_I_F,data_inclusione,ora_inclusione,inclusa_da,inclusa_il)" &
                        " VALUES " &
                        "('" & dropCompagnie.SelectedValue & "','" & Rs("num_ordine") & "','" & Rs("id_veicolo") & "'," &
                        "'" & Replace(Rs("valore_assicurato"), ",", ".") & "','" & data_inclusione & "','24','" & Request.Cookies("SicilyRentCar")("idUtente") & "'," &
                        "GetDate())"

                        'Response.Write(CmdSalvataggio.CommandText)
                        'Response.End()

                        CmdSalvataggio.ExecuteNonQuery()
                        CmdSalvataggio.Dispose()
                        DbcSalvataggio.Close()
                        DbcSalvataggio = Nothing

                        CampoModelloNonPresente = CampoModelloNonPresente + 1
                    Loop
                    Rs.Close()
                    Dbc.Close()
                    Rs = Nothing
                    Dbc = Nothing

                    lblErrore2.Text = "Tutti i dati sono stati caricati correttamente"
                End If
                'Response.Write("Campi Obbl KO " & CampiObbligatoriKO & "<br>")
                'Response.Write("Campi Targa già P " & CampoTargaPresente & "<br>")
                'Response.Write("Campi Telaio già P " & CampoTelaioPresente & "<br>")
                'Response.Write("Campi Modello non P " & CampoModelloNonPresente & "<br>")
            Else
                lblErrore2.Text = "Il file scelto non è corretto."
            End If
        Else
            lblErrore2.Text = "Scegliere un file."
        End If
    End Sub

    Protected Sub btnImportaFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportaFile.Click
        If dropCompagnie.SelectedValue <> "0" Then
            lblErrore2.Text = ""
            importa_file()
        Else
            Libreria.genUserMsgBox(Me, "Specificare la compagnia assicurativa da associare alla lista auto.")
        End If
    End Sub
End Class
