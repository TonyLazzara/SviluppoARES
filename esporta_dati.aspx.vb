Imports System
Imports System.IO
Imports System.IO.File
Imports System.IO.Compression
Imports System.Net
Imports System.Net.Mail

Partial Class esporta_dati
    Inherits System.Web.UI.Page

    'Sviluppo
    'Private directoryPath As String = "C:\inetpub\sviluppoares.sicilyrentcar.it\allegati\xml"

    'Produzione
    Private directoryPath As String = Server.MapPath("allegati\xml\") '"C:\siti_internet\ares.sicilyrentcar.it\htdocs\allegati\xml"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.Fatturazione) = "1" Then
                    Response.Redirect("default.aspx")
                End If
            Catch ex As Exception

            End Try

        End If
    End Sub

    Protected Sub InvioMail(ByVal Cartella As String, ByVal NomeFile As String)

        Libreria.genUserMsgBox(Me, "InvioMail esporta dati in fase di sviluppo")
        Exit Sub


        'Invio(Mail)        
        'Libreria.genUserMsgBox(Me, NomeFile)



        Dim corpoMessaggio As String
        'Dichiaro e creo un nuovo messaggio
        Dim mail As New MailMessage()

        'Dichiato il mittente
        'mail.From = New MailAddress("noreply@airgest.it")
        mail.From = New MailAddress("noreply@sicilyrentcar.it", "SicilyRentCar")


        'Dichiaro il destinatario
        'Elenco destinatari
        'Dim DestMail As String = Session("EmailDest")

        mail.To.Add("dimatteo@xinformatica.it")
        'mail.To.Add(DestMail)

        'Dichiaro il destinatario CC
        'Elenco destinatari CC
        'Dim DestCCMail As String = ConfigurationManager.AppSettings.Get("DestCCMail")
        'mail.CC.Add(DestCCMail)

        'Dichiaro il destinatario Bcc
        mail.Bcc.Add("dimatteo@xinformatica.it")

        'Allegato                     
        mail.Attachments.Add(New Attachment(Server.MapPath("allegati\xml\" & Cartella & "\" & NomeFile)))

        'mail.Attachments.Add(New Attachment(Server.MapPath("public\voucher_10AP01-2017.pdf")))


        'Imposta l'oggetto della Mail
        mail.Subject = "File XML " & NomeFile
        corpoMessaggio = "Gentile Cliente "
        'corpoMessaggio = "Gentile Cliente xxxxxxxxxxxxxx, xxxxxxxx, la sua transazione è andata a buon fine, in allegato il voucher con i dettagli della sua prenotazione e le istruzioni d'uso."
        corpoMessaggio = corpoMessaggio & "<br>"
        corpoMessaggio = corpoMessaggio & "Cordiali Saluti"
        corpoMessaggio = corpoMessaggio & "<br>"
        corpoMessaggio = corpoMessaggio & "Airgest Spa"


        'Imposta la priorità  della Mail
        mail.Priority = MailPriority.High

        mail.IsBodyHtml = True

        'Imposta il testo del messaggio                
        'mail.Body = Replace(corpoMessaggio, "!", "")
        mail.Body = corpoMessaggio

        'Imposta il server smtp di posta da utilizzare        
        Dim client As New Net.Mail.SmtpClient(ConfigurationManager.AppSettings.Get("Mail_SMTP"))
        client.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings.Get("Mail_SMTP_credenziali_param1"), ConfigurationManager.AppSettings.Get("Mail_SMTP_credenziali_param2"))
        client.Host = ConfigurationManager.AppSettings.Get("Mail_SMTP")
        client.Port = 25

        Dim sm As New sendmailcls
        Try


            client.Send(mail)
        Catch ex As Exception
            MsgBox(Page, ex.ToString)
        End Try


        'Response.End()
        'Fine Invio Mail
    End Sub

    Protected Sub btnCmdAvvio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCmdAvvio.Click
        Dim NumFattGenerate As Integer

        'Sviluppo
        'Dim filePath As String = "C:/inetpub/sviluppoares.sicilyrentcar.it/allegati/xml/"

        'Produzione
        Dim filePath As String = Server.MapPath("allegati\xml\") '"C:/siti_internet/ares.sicilyrentcar.it/htdocs/allegati/xml/"

        'Sviluppo
        'Dim Url As String = "http://sviluppoares.sicilyrentcar.it/allegati/xml/"

        'Produzione
        Dim Url As String = "http://ares.sicilyrentcar.it/allegati/xml/"
        'sviluppo sicilyrentcars
        'Dim Url As String = "http://sviluppo.sicilyrentcar.it/allegati/xml/"

        Dim NomeCartella As String = ""
        Dim NomeFile, NFile As String

        Dim dadatasql As String
        Dim adatasql As String

        Dim yeardatatxt As String '05.01.2022

        Dim codice_sdi As String = ""   'aggiunto 20.10.2022 salvo



        Select Case btnCmdAvvio.Text
            Case Is = "Genera XML"
                Try
                    If rdbtnNoleggi.Checked = False And rdbtnMulte.Checked = False Then
                        'Libreria.genUserMsgBox(Me, "IF")
                        Libreria.genUserMsgBox(Me, "Selezionare se fatturare i Noleggi o le Multe")
                    Else
                        'Libreria.genUserMsgBox(Me, "ELSE")
                        Dim Data, DataA, DataOra, DataOraA As String
                        Dim DataOraArray(), DataOraAArray() As String

                        btnCmdAvvio.Enabled = False

                        DataOra = txtDataFattura.Text
                        DataOraA = txtDataFatturaA.Text


                        'se campi data vuoti considera la data corrente
                        'inserito il 28.07.2021

                        If txtDataFattura.Text = "" Then

                            DataOra = FormatDateTime(Date.Now, vbShortDate) '"01/01/" & Year(Date.Now)      
                            DataOraA = DataOra ' FormatDateTime(Date.Now, vbShortDate)


                            txtDataFattura.Text = DataOra
                            txtDataFatturaA.Text = DataOraA

                            'Libreria.genUserMsgBox(Me, DataOra)
                            DataOraArray = Split(DataOra, " ")
                            DataOraAArray = Split(DataOraA, " ")
                            Data = DataOraArray(0)
                            DataA = DataOraAArray(0)

                            dadatasql = Year(Data) & "-01-01 00:00:00"
                            adatasql = Year(DataA) & "-" & Month(DataA) & "-" & Day(DataA) & " 23:59:59"

                            yeardatatxt = Year(Date.Now)    '05.01.2022


                        Else



                            'Libreria.genUserMsgBox(Me, DataOra)
                            DataOraArray = Split(DataOra, " ")
                            If DataOraA = "" Then 'aggiunto salvo 21.03.2023
                                DataOraA = DataOra
                            End If


                            DataOraAArray = Split(DataOraA, " ")
                            Data = DataOraArray(0)
                            DataA = DataOraAArray(0)

                            dadatasql = Year(Data) & "-" & Month(Data) & "-" & Day(Data) & " 00:00:00"
                            adatasql = Year(DataA) & "-" & Month(DataA) & "-" & Day(DataA) & " 23:59:59"

                            yeardatatxt = Year(CDate(txtDataFattura.Text))    '05.01.2022

                        End If


                        If txtDataFattura.Text <> "" Then
                            'Libreria.genUserMsgBox(Me, "IF")
                            Dim Giorno, Mese, Anno As String
                            Dim gmaArray() As String

                            gmaArray = Split(Data, "/")
                            Giorno = gmaArray(0)
                            Mese = gmaArray(1)
                            Anno = gmaArray(2)

                            If rdbtnNoleggi.Checked = True Then 'Noleggi 
                                filePath = filePath & "Noleggi " & Giorno & "-" & Mese & "-" & Anno
                                Url = Url & "Noleggi " & Giorno & "-" & Mese & "-" & Anno & "/"
                                NomeCartella = "Noleggi " & Giorno & "-" & Mese & "-" & Anno
                            Else
                                filePath = filePath & "Multe " & Giorno & "-" & Mese & "-" & Anno
                                Url = Url & "Multe " & Giorno & "-" & Mese & "-" & Anno & "/"
                                NomeCartella = "Multe " & Giorno & "-" & Mese & "-" & Anno
                            End If
                        Else
                            'Libreria.genUserMsgBox(Me, "ELSE")
                        End If
                        'Libreria.genUserMsgBox(Me, filePath)

                        If Directory.Exists(filePath) = False Then
                            ' ...Creo la cartella al percorso specificato
                            Directory.CreateDirectory(filePath)
                        End If
                        'Response.End()

                        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        Dbc.Open()

                        Dim StrQuery As String
                        Dim OrderBy As String
                        If rdbtnNoleggi.Checked = True Then 'Noleggi 
                            OrderBy = "ORDER BY CAST(num_fattura AS int)"
                        Else
                            OrderBy = "ORDER BY CAST(fatture.codice_fattura AS int)"
                        End If

                        If rdbtnNoleggi.Checked = True Then 'Noleggi
                            'StrQuery = "select fatture_nolo.*, nazioni.cod_3166_alpha1 as cod3166_alpha2 FROM  Fatture_nolo, nazioni WHERE  fatture_nolo.nazione = nazioni.nazione and fatture_nolo.id_tipo_fattura = 2 AND (fatture_nolo.data_fattura BETWEEN '" & Data & "' AND '" & DataA & "') and fatture_nolo.nazione = 'ITALIA' "
                            StrQuery = "select fatture_nolo.*, nazioni.cod_3166_alpha1 as cod3166_alpha2 FROM  Fatture_nolo, nazioni WHERE  fatture_nolo.nazione = nazioni.nazione and fatture_nolo.id_tipo_fattura = 2 AND (fatture_nolo.data_fattura BETWEEN convert(datetime,'" & dadatasql & "',102) AND convert(datetime,'" & adatasql & "',102)) "
                        Else 'Multe                            
                            'StrQuery = "select fatture.*, nazioni.cod_3166_alpha1 as cod3166_alpha2 from fatture, nazioni WHERE  fatture.nazione = nazioni.id_nazione and tipo_fattura = 4 and (data_fattura BETWEEN '" & Data & "' AND '" & DataA & "') and fatture.nazione = 16 "
                            StrQuery = "select fatture.*, nazioni.cod_3166_alpha1 as cod3166_alpha2 from fatture, nazioni WHERE  fatture.nazione = nazioni.id_nazione and tipo_fattura = 4 and (data_fattura BETWEEN convert(datetime,'" & dadatasql & "',102) AND convert(datetime,'" & adatasql & "',102)) "
                        End If

                        'Libreria.genUserMsgBox(Me, "1")
                        If dropNominativo.SelectedItem.Text <> "Seleziona..." Then
                            'Libreria.genUserMsgBox(Me, "IF")
                            Dim NominativoCodFiscale() As String
                            NominativoCodFiscale = Split(dropNominativo.SelectedItem.Text, "   --")
                            'Libreria.genUserMsgBox(Me, NominativoCodFiscale(0))

                            If rdbtnNoleggi.Checked = True Then 'Noleggi
                                StrQuery = "select fatture_nolo.*, nazioni.cod_3166_alpha1 as cod3166_alpha2 FROM  Fatture_nolo, nazioni WHERE  fatture_nolo.nazione = nazioni.nazione and fatture_nolo.id_tipo_fattura = 2 "
                            Else 'Multe
                                'StrQuery = "select * from fatture where tipo_fattura = 4 and  (data_fattura BETWEEN '" & Data & "' AND '" & Data & "')"
                                StrQuery = "select fatture.*, nazioni.cod_3166_alpha1 as cod3166_alpha2 from fatture, nazioni WHERE  fatture.nazione = nazioni.id_nazione and tipo_fattura = 4 "
                            End If
                            StrQuery = StrQuery & " and intestazione = '" & Trim(NominativoCodFiscale(0)) & "' and  codice_fiscale = '" & Trim(NominativoCodFiscale(1)) & "' "
                        End If

                        'Libreria.genUserMsgBox(Me, "2")
                        If txtNumFattura.Text <> "" Then

                            'aggiunto salvo 21.03.2023 x numfattura multipla salvo 21.03.2023
                            If txtNumFattura.Text.IndexOf("-") = -1 Then
                                'Numero Fattura Unico
                                If rdbtnNoleggi.Checked = True Then 'Noleggi
                                    StrQuery = "select fatture_nolo.*, nazioni.cod_3166_alpha1 as cod3166_alpha2 FROM  Fatture_nolo, nazioni WHERE  fatture_nolo.nazione = nazioni.nazione and fatture_nolo.id_tipo_fattura = 2 "
                                    StrQuery = StrQuery & " and num_fattura ='" & txtNumFattura.Text & "' and data_fattura like '%" & yeardatatxt & "%'" '05.01.2022

                                Else 'Multe
                                    'StrQuery = "select * from fatture where tipo_fattura = 4 and  (data_fattura BETWEEN '" & Data & "' AND '" & Data & "')"
                                    StrQuery = "select fatture.*, nazioni.cod_3166_alpha1 as cod3166_alpha2 from fatture, nazioni WHERE  fatture.nazione = nazioni.id_nazione and tipo_fattura = 4 "
                                    StrQuery = StrQuery & " and codice_fattura ='" & txtNumFattura.Text & "' and data_fattura like '%" & yeardatatxt & "%'"  '05.01.2022
                                End If

                                'StrQuery = StrQuery & " and num_fattura ='" & txtNumFattura.Text & "' and data_fattura like '%" & Year(Now) & "%'"
                                Session("NumFatturaDaScaricare") = txtNumFattura.Text

                            Else 'Range NUMERI      salvo 21.03.2023

                                Dim rnf() As String = txtNumFattura.Text.Split("-")
                                Dim danumft As String = rnf(0)
                                Dim anumft As String = rnf(1)

                                If rdbtnNoleggi.Checked = True Then 'Noleggi
                                    StrQuery = "select fatture_nolo.*, nazioni.cod_3166_alpha1 as cod3166_alpha2 FROM  Fatture_nolo, nazioni WHERE  fatture_nolo.nazione = nazioni.nazione and fatture_nolo.id_tipo_fattura = 2 "
                                    StrQuery = StrQuery & " AND num_fattura between '" & danumft & "' AND '" & anumft & "' AND data_fattura like '%" & yeardatatxt & "%'" '05.01.2022

                                Else 'Multe
                                    'StrQuery = "select * from fatture where tipo_fattura = 4 and  (data_fattura BETWEEN '" & Data & "' AND '" & Data & "')"
                                    StrQuery = "select fatture.*, nazioni.cod_3166_alpha1 as cod3166_alpha2 from fatture, nazioni WHERE  fatture.nazione = nazioni.id_nazione and tipo_fattura = 4 "
                                    StrQuery = StrQuery & " and codice_fattura between '" & danumft & "' AND '" & anumft & "' AND data_fattura like '%" & yeardatatxt & "%'"  '05.01.2022
                                End If

                                'StrQuery = StrQuery & " and num_fattura ='" & txtNumFattura.Text & "' and data_fattura like '%" & Year(Now) & "%'"
                                Session("NumFatturaDaScaricare") = txtNumFattura.Text

                            End If 'se numero fattura unico o range numeri fattura

                        Else
                            Session("NumFatturaDaScaricare") = ""
                        End If

                            StrQuery = StrQuery & OrderBy
                        'Libreria.genUserMsgBox(Me, StrQuery)

                        'verifica 16.09.2021



                        Dim Cmd As New Data.SqlClient.SqlCommand(StrQuery, Dbc)

                            Dim Rs As Data.SqlClient.SqlDataReader
                            Rs = Cmd.ExecuteReader()

                            If Rs.HasRows Then
                                'Salvo dati in tabella Elenco file scaricati Data
                                Dim DbcExecute As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                                DbcExecute.Open()

                            Dim SqlEFSD As String
                            If NomeCartella <> "" Then
                                SqlEFSD = "insert into XML_elenco_file_scaricati_cartella (cartella) values('" & NomeCartella & "')"
                            Else
                                SqlEFSD = "insert into XML_elenco_file_scaricati_cartella (cartella) values('" & txtNumFattura.Text & "')"
                            End If

                            'Libreria.genUserMsgBox(Me, SqlEFSD)

                            Dim CmdExecute As New Data.SqlClient.SqlCommand(SqlEFSD, DbcExecute)
                            CmdExecute.ExecuteNonQuery()

                            'Prelevo ultimo id inserito
                            Dim SqlUltimoId, UltimoId As String

                            Dim DbcUltimoId As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                            DbcUltimoId.Open()

                            SqlUltimoId = "select max(id) from XML_elenco_file_scaricati_cartella"
                            'Libreria.genUserMsgBox(Me, SqlUltimoId)

                            Dim CmdUltimoId As New Data.SqlClient.SqlCommand(SqlUltimoId, DbcUltimoId)

                            Dim RsUltimoId As Data.SqlClient.SqlDataReader
                            RsUltimoId = CmdUltimoId.ExecuteReader()
                            If RsUltimoId.HasRows Then
                                Do While RsUltimoId.Read
                                    UltimoId = RsUltimoId(0)
                                Loop
                            End If

                            CmdUltimoId.Dispose()
                            CmdUltimoId = Nothing

                            RsUltimoId.Close()
                            DbcUltimoId.Close()
                            RsUltimoId = Nothing
                            DbcUltimoId = Nothing

                            'Libreria.genUserMsgBox(Me, UltimoId)

                            ''Recupera lista delle fatture da dove prelevare i dati per XML 19.04.2021
                            Do While Rs.Read
                                Dim IVA0, IVA21, IVA22 As String
                                IVA0 = -1
                                IVA21 = 0
                                IVA22 = 0
                                Dim PrzTot0, PrzTot21, PrzTot22 As String
                                PrzTot0 = 0
                                PrzTot21 = 0
                                PrzTot22 = 0

                                Dim DbcNumRighe As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                                DbcNumRighe.Open()

                                Dim SqlNumRighe As String

                                If rdbtnNoleggi.Checked = True Then 'Noleggi
                                    SqlNumRighe = "select count(*) FROM  Fatture_nolo where fatture_nolo.id_tipo_fattura = 2 AND (fatture_nolo.data_fattura BETWEEN convert(datetime,'" & dadatasql & "',102) AND convert(datetime,'" & adatasql & "',102))"
                                Else
                                    SqlNumRighe = "select count(*) FROM  Fatture where Fatture.tipo_fattura = 4 AND (Fatture.data_fattura BETWEEN convert(datetime,'" & dadatasql & "',102) AND convert(datetime,'" & adatasql & "',102))"
                                End If
                                'Libreria.genUserMsgBox(Me, SqlNumRighe)

                                Dim CmdNumRighe As New Data.SqlClient.SqlCommand(SqlNumRighe, DbcNumRighe)

                                Dim RsNumRighe As Data.SqlClient.SqlDataReader
                                RsNumRighe = CmdNumRighe.ExecuteReader()
                                If RsNumRighe.HasRows Then
                                    Do While RsNumRighe.Read
                                        Dim NumRighe As Integer = RsNumRighe(0)
                                        'Libreria.genUserMsgBox(Me, NumRighe)
                                        'ProgressBar1.Maximum = NumRighe
                                    Loop
                                End If

                                CmdNumRighe.Dispose()
                                CmdNumRighe = Nothing

                                RsNumRighe.Close()
                                DbcNumRighe.Close()
                                RsNumRighe = Nothing
                                DbcNumRighe = Nothing

                                NumFattGenerate = NumFattGenerate + 1
                                'ProgressBar1.Visible = True
                                'If ProgressBar1.Value < ProgressBar1.Maximum Then
                                '    ProgressBar1.Value += 1
                                'End If

                                Dim Sql, progressivo As String

                                Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                                Dbc2.Open()

                                If rdbtnNoleggi.Checked = True Then 'Noleggio
                                    Sql = "select * from contatoriXML where descrizione='Fatture inviate'"
                                Else
                                    Sql = "select * from contatoriXML where descrizione='Fatture inviate'"
                                End If
                                'Libreria.genUserMsgBox(Me, Sql)

                                Dim Cmd2 As New Data.SqlClient.SqlCommand(Sql, Dbc2)

                                Dim Rs2 As Data.SqlClient.SqlDataReader
                                Rs2 = Cmd2.ExecuteReader()
                                If Rs2.HasRows Then
                                    Do While Rs2.Read
                                        'Creazione N. Progressivo
                                        progressivo = ""
                                        txtNFattInviate.Text = Rs2("valore") + 1
                                        For i = Len(txtNFattInviate.Text) + 1 To 5
                                            progressivo = progressivo & "0"
                                        Next i
                                        progressivo = progressivo & txtNFattInviate.Text

                                        NomeFile = "\IT02486830819_" & progressivo & ".xml"
                                        NFile = "IT02486830819_" & progressivo & ".xml"
                                    Loop
                                End If
                                'Libreria.genUserMsgBox(Me, NomeFile)

                                Cmd2.Dispose()
                                Cmd2 = Nothing

                                Rs2.Close()
                                Dbc2.Close()
                                Rs2 = Nothing
                                Dbc2 = Nothing

                                Dim Sql3 As String

                                'Salvo valore progressivo
                                'Funzione Inserimento o Aggiornamento
                                If rdbtnNoleggi.Checked = True Then 'Noleggio
                                    Sql3 = "update contatoriXML set valore = valore+1 where descrizione = 'Fatture inviate'"
                                Else
                                    Sql3 = "update contatoriXML set valore = valore+1 where descrizione = 'Fatture inviate'"
                                End If
                                'Libreria.genUserMsgBox(Me, Sql3)

                                CmdExecute.CommandText = Sql3
                                CmdExecute.ExecuteNonQuery()

                                'NomeFile = "/IT02486830819_3.doc"
                                NomeFile = filePath & NomeFile

                                If System.IO.File.Exists(NomeFile) = False Then
                                    System.IO.File.Create(NomeFile).Dispose()
                                End If


                                'Salvo dati in tabella Elenco file scaricati Lista
                                Dim SqlEFSL As String
                                SqlEFSL = "insert into XML_elenco_file_scaricati_lista (id_e_f_s_d, nome_file) values('" & UltimoId & "','" & "allegati/xml/" & NomeCartella & "/" & NFile & "')"
                                'Libreria.genUserMsgBox(Me, SqlEFSL)

                                CmdExecute.CommandText = SqlEFSL
                                CmdExecute.ExecuteNonQuery()


                                Dim objWriter As New System.IO.StreamWriter(NomeFile, True)

                                '# aggiunto nel caso di NULL nel campo codice_sdi 20.10.2022 salvo
                                If IsDBNull(Rs("codice_sdi")) Then
                                    codice_sdi = ""
                                Else
                                    codice_sdi = Rs("codice_sdi")
                                End If

                                If Len(codice_sdi) = 6 Then 'Pubblica Amministarzione
                                    objWriter.WriteLine("<?xml version=""1.0"" encoding=""UTF-8""?><?xml-stylesheet type=""text/xsl"" href=""fatturaordinaria_v1.2.1.xsl""?><ns0:FatturaElettronica xmlns:ns0=""http://ivaservizi.agenziaentrate.gov.it/docs/xsd/fatture/v1.2"" versione=""FPA12"">")
                                Else
                                    objWriter.WriteLine("<?xml version=""1.0"" encoding=""UTF-8""?><?xml-stylesheet type=""text/xsl"" href=""fatturaordinaria_v1.2.1.xsl""?><ns0:FatturaElettronica xmlns:ns0=""http://ivaservizi.agenziaentrate.gov.it/docs/xsd/fatture/v1.2"" versione=""FPR12"">")
                                End If


                                objWriter.WriteLine("<FatturaElettronicaHeader xmlns:ns0=""http://ivaservizi.agenziaentrate.gov.it/docs/xsd/fatture/v1.2"">")
                                objWriter.WriteLine("    <DatiTrasmissione>")
                                objWriter.WriteLine("        <IdTrasmittente>")

                                'IdIdPaese
                                objWriter.WriteLine("            <IdPaese>IT</IdPaese>")

                                'IdCodice
                                objWriter.WriteLine("            <IdCodice>02486830819</IdCodice>")

                                objWriter.WriteLine("         </IdTrasmittente>")

                                Dim Dbc4 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                                Dbc4.Open()

                                If rdbtnNoleggi.Checked = True Then 'Noleggio
                                    Sql = "select * from contatoriXML where descrizione='Fatture Noleggio' and anno='" & Year(Now) & "'"
                                Else
                                    Sql = "select * from contatoriXML where descrizione='Fatture Multe' and anno='" & Year(Now) & "'"
                                End If

                                Dim progressivo2, SQL_INS As String
                                Dim Cmd4 As New Data.SqlClient.SqlCommand(Sql, Dbc4)

                                Dim Rs4 As Data.SqlClient.SqlDataReader
                                Rs4 = Cmd4.ExecuteReader()
                                If Rs4.HasRows Then
                                    Do While Rs4.Read
                                        progressivo2 = ""
                                        txtProgressivo.Text = Rs4("valore") + 1
                                        For i = Len(txtProgressivo.Text) + 1 To 5
                                            progressivo2 = progressivo2 & "0"
                                        Next i
                                        progressivo2 = progressivo2 & txtProgressivo.Text
                                        objWriter.WriteLine("         <ProgressivoInvio>" & Year(Now) & progressivo2 & "</ProgressivoInvio>")


                                        'ProgressivoInvio            
                                        'Salvo valore progressivo2
                                        'Funzione Inserimento o Aggiornamento
                                        If rdbtnNoleggi.Checked = True Then 'Noleggio
                                            SQL_INS = "update contatoriXML set valore = valore+1 where descrizione='Fatture Noleggio' and anno='" & Year(Now) & "'"
                                        Else
                                            SQL_INS = "update contatoriXML set valore = valore+1 where descrizione='Fatture Multe' and anno='" & Year(Now) & "'"
                                        End If

                                        CmdExecute.CommandText = SQL_INS
                                        CmdExecute.ExecuteNonQuery()
                                    Loop
                                End If

                                Cmd4.Dispose()
                                Cmd4 = Nothing

                                Rs4.Close()
                                Dbc4.Close()
                                Rs4 = Nothing
                                Dbc4 = Nothing

                                If Len(codice_sdi) = 6 Then 'Pubblica Amministarzione          'aggiornato 20.10.2022 salvo                  
                                    'FormatoTrasmissione
                                    objWriter.WriteLine("         <FormatoTrasmissione>FPA12</FormatoTrasmissione>")

                                    'CodiceDestinatario                            
                                    objWriter.WriteLine("         <CodiceDestinatario>" & codice_sdi & "</CodiceDestinatario>")
                                Else 'Privato NO Pubblica Amministrazione
                                    'FormatoTrasmissione
                                    objWriter.WriteLine("         <FormatoTrasmissione>FPR12</FormatoTrasmissione>")

                                    If rdbtnNoleggi.Checked = True Then 'Noleggio
                                        If UCase(Rs("nazione")) = "ITALIA" Then 'Cliente Italiano
                                            If codice_sdi & "" = "" Then
                                                'CodiceDestinatario                                    
                                                objWriter.WriteLine("         <CodiceDestinatario>0000000</CodiceDestinatario>")
                                            Else
                                                'CodiceDestinatario                                    
                                                objWriter.WriteLine("         <CodiceDestinatario>" & Trim(codice_sdi) & "</CodiceDestinatario>") ''aggiornato 20.10.2022 salvo
                                            End If
                                        Else 'Cliente straniero
                                            'CodiceDestinatario                                
                                            objWriter.WriteLine("         <CodiceDestinatario>XXXXXXX</CodiceDestinatario>")
                                        End If
                                    Else
                                        'Libreria.genUserMsgBox(Me, "ELSE")
                                        If Rs("nazione") = "16" Then 'Cliente Italiano c
                                            If codice_sdi & "" = "" Then 'aggiornato 20.10.2022 salvo
                                                'CodiceDestinatario                                    
                                                objWriter.WriteLine("         <CodiceDestinatario>0000000</CodiceDestinatario>")
                                            Else
                                                'CodiceDestinatario                                    
                                                objWriter.WriteLine("         <CodiceDestinatario>" & Trim(codice_sdi) & "</CodiceDestinatario>")
                                            End If
                                        Else 'Cliente straniero
                                            'CodiceDestinatario                                
                                            objWriter.WriteLine("         <CodiceDestinatario>XXXXXXX</CodiceDestinatario>")
                                        End If
                                    End If

                                End If

                                If codice_sdi = "" And (Rs("email_pec") & "") <> "" Then 'aggiornato 20.10.2022 salvo
                                    'PECDestinatario                            
                                    objWriter.WriteLine("         <PECDestinatario>" & Rs("email_pec") & "</PECDestinatario>")
                                End If

                                objWriter.WriteLine("    </DatiTrasmissione>")

                                'CedentePrestatore - DatiAnagrafici
                                objWriter.WriteLine("        <CedentePrestatore>")

                                objWriter.WriteLine("            <DatiAnagrafici>")

                                'IdFiscaleIVA
                                objWriter.WriteLine("                <IdFiscaleIVA>")
                                objWriter.WriteLine("                    <IdPaese>IT</IdPaese>")
                                objWriter.WriteLine("                    <IdCodice>02486830819</IdCodice>")
                                objWriter.WriteLine("                </IdFiscaleIVA>")

                                'CodiceFiscale
                                objWriter.WriteLine("                <CodiceFiscale>02486830819</CodiceFiscale>")

                                'Anagrafica
                                objWriter.WriteLine("                <Anagrafica>")

                                'Denominazione
                                objWriter.WriteLine("                    <Denominazione>SICILY RENT CAR S.r.l.</Denominazione>")

                                '/Anagrafica
                                objWriter.WriteLine("                </Anagrafica>")

                                'RegimeFiscale
                                objWriter.WriteLine("                <RegimeFiscale>RF01</RegimeFiscale>")

                                '/DatiAnagrafici
                                objWriter.WriteLine("            </DatiAnagrafici>")

                                'Sede
                                objWriter.WriteLine("            <Sede>")

                                'Indirizzo
                                objWriter.WriteLine("                <Indirizzo>Largo Lituania, 11</Indirizzo>")

                                'CAP
                                objWriter.WriteLine("                <CAP>90146</CAP>")

                                'Comune
                                objWriter.WriteLine("                <Comune>Palermo</Comune>")

                                'Provincia
                                objWriter.WriteLine("                <Provincia>PA</Provincia>")

                                'Nazione
                                objWriter.WriteLine("                <Nazione>IT</Nazione>")

                                '/Sede
                                objWriter.WriteLine("            </Sede>")


                                'IscrizioneREA
                                objWriter.WriteLine("            <IscrizioneREA>")

                                'Ufficio
                                objWriter.WriteLine("                <Ufficio>PA</Ufficio>")

                                'NumeroREA
                                objWriter.WriteLine("                <NumeroREA>303366</NumeroREA>")

                                'CapitaleSociale
                                objWriter.WriteLine("                <CapitaleSociale>70000.00</CapitaleSociale>")

                                'SocioUnico
                                objWriter.WriteLine("                <SocioUnico>SU</SocioUnico>")

                                'StatoLiquidazione
                                objWriter.WriteLine("                <StatoLiquidazione>LN</StatoLiquidazione>")

                                '/IscrizioneREA
                                objWriter.WriteLine("            </IscrizioneREA>")

                                '/CedentePrestatore
                                objWriter.WriteLine("        </CedentePrestatore>")

                                '[1.4] CessionarioCommittente
                                objWriter.WriteLine("        <CessionarioCommittente>")

                                '[1.4.1] DatiAnagrafici
                                objWriter.WriteLine("            <DatiAnagrafici>")

                                If rdbtnNoleggi.Checked = True Then 'Noleggi
                                    If UCase(Rs("nazione")) = "ITALIA" Then 'Cliente Italiano

                                        '[1.4.1.1] IdFiscaleIVA
                                        If Rs("piva") & "" <> "" Then





                                            objWriter.WriteLine("                <IdFiscaleIVA>")

                                            '[1.4.1.1.1] IdPaese
                                            objWriter.WriteLine("                    <IdPaese>IT</IdPaese>")

                                            '[1.4.1.1.2] IdCodice
                                            objWriter.WriteLine("                    <IdCodice>" & Rs("piva") & "</IdCodice>")

                                            'CHIUSURA [1.4.1.1] IdFiscaleIVA
                                            objWriter.WriteLine("                </IdFiscaleIVA>")






                                        End If






                                        '[1.4.1.2] CodiceFiscale
                                        If rdbtnNoleggi.Checked = True Then 'Noleggio
                                            If Rs("codice_fiscale") & "" <> "" Then
                                                objWriter.WriteLine("                    <CodiceFiscale>" & Rs("codice_fiscale") & "</CodiceFiscale>")
                                            End If
                                        Else
                                            If Rs("codicefiscale") & "" <> "" Then
                                                objWriter.WriteLine("                    <CodiceFiscale>" & Rs("codicefiscale") & "</CodiceFiscale>")
                                            End If
                                        End If
                                    Else 'Cliente NON Italiano
                                        objWriter.WriteLine("                <IdFiscaleIVA>")

                                        '[1.4.1.1.1] IdPaese
                                        objWriter.WriteLine("                    <IdPaese>" & Rs("cod3166_alpha2") & "</IdPaese>")

                                        '[1.4.1.1.2] IdCodice


                                        'recupera piva ee 10.08.2022 salvo 
                                        'Trim(Rs("num_contratto_rif"))
                                        Dim piva_ee As String = funzioni_comuni_new.getPivaEE(Trim(Rs("num_contratto_rif")))
                                        If piva_ee <> "" Then
                                            objWriter.WriteLine("                    <IdCodice>" & Trim(piva_ee) & "</IdCodice>")
                                        Else
                                            objWriter.WriteLine("                    <IdCodice>OO99999999999</IdCodice>")
                                        End If

                                        'CHIUSURA [1.4.1.1] IdFiscaleIVA
                                        objWriter.WriteLine("                </IdFiscaleIVA>")
                                    End If
                                Else
                                    If Rs("nazione") = "16" Then 'Cliente Italiano

                                        '[1.4.1.1] IdFiscaleIVA
                                        If Rs("piva") & "" <> "" Then
                                            objWriter.WriteLine("                <IdFiscaleIVA>")

                                            '[1.4.1.1.1] IdPaese
                                            objWriter.WriteLine("                    <IdPaese>IT</IdPaese>")

                                            '[1.4.1.1.2] IdCodice
                                            objWriter.WriteLine("                    <IdCodice>" & Rs("piva") & "</IdCodice>")

                                            'CHIUSURA [1.4.1.1] IdFiscaleIVA
                                            objWriter.WriteLine("                </IdFiscaleIVA>")
                                        End If

                                        '[1.4.1.2] CodiceFiscale
                                        If rdbtnNoleggi.Checked = True Then 'Noleggio
                                            If Rs("codice_fiscale") & "" <> "" Then
                                                objWriter.WriteLine("                    <CodiceFiscale>" & Rs("codice_fiscale") & "</CodiceFiscale>")
                                            End If
                                        Else
                                            If Rs("codicefiscale") & "" <> "" Then
                                                objWriter.WriteLine("                    <CodiceFiscale>" & Rs("codicefiscale") & "</CodiceFiscale>")
                                            End If
                                        End If
                                    Else 'Cliente NON Italiano
                                        objWriter.WriteLine("                <IdFiscaleIVA>")

                                        '[1.4.1.1.1] IdPaese 'inserito il codice IE nella tabella nazioni campo alpha2
                                        objWriter.WriteLine("                    <IdPaese>" & Rs("cod3166_alpha2") & "</IdPaese>")

                                        '[1.4.1.1.2] IdCodice
                                        objWriter.WriteLine("                    <IdCodice>OO99999999999</IdCodice>")

                                        'CHIUSURA [1.4.1.1] IdFiscaleIVA
                                        objWriter.WriteLine("                </IdFiscaleIVA>")
                                    End If
                                End If


                                '[1.4.1.3] Anagrafica
                                objWriter.WriteLine("                <Anagrafica>")

                                If Rs("piva") & "" <> "" Then
                                    '[1.4.1.3.1] Denominazione
                                    Dim deno As String = Trim(Rs("intestazione")) 'modificato 09.04.2021
                                    deno = deno.Replace("&", "&amp;")
                                    objWriter.WriteLine("                    <Denominazione>" & deno & "</Denominazione>")

                                Else
                                    Dim Nome, Cognome As String
                                    Dim Nominativo As String
                                    Dim ArrayNominativo() As String
                                    Nome = ""
                                    Cognome = ""

                                    Nominativo = Rs("intestazione")
                                    'Libreria.genUserMsgBox(Me, Nominativo)
                                    ArrayNominativo = Split(Nominativo, " ")
                                    If Len(ArrayNominativo(0)) <= 3 Then
                                        'Libreria.genUserMsgBox(Me, "IF")
                                        If UBound(ArrayNominativo) <> 1 Then
                                            'Libreria.genUserMsgBox(Me, "IF2")
                                            Cognome = ArrayNominativo(0) & " " & ArrayNominativo(1)
                                            Nome = ArrayNominativo(2)
                                            For j = 3 To UBound(ArrayNominativo)
                                                Nome = Nome & " " & ArrayNominativo(j)
                                            Next j
                                        Else
                                            'Libreria.genUserMsgBox(Me, "else")
                                            Cognome = ArrayNominativo(0)
                                            Nome = ArrayNominativo(1)
                                        End If
                                    Else
                                        Cognome = ArrayNominativo(0)
                                        Nome = ArrayNominativo(1)
                                        For j = 2 To UBound(ArrayNominativo)
                                            Nome = Nome & " " & ArrayNominativo(j)
                                        Next j
                                    End If

                                    '[1.4.1.3.2] nome
                                    objWriter.WriteLine("                    <Nome>" & Trim(Nome) & "</Nome>")

                                    '[1.4.1.3.3] Cognome
                                    objWriter.WriteLine("                    <Cognome>" & Trim(Cognome) & "</Cognome>")
                                End If

                                'CHIUSURA [1.4.1.3] Anagrafica
                                objWriter.WriteLine("                </Anagrafica>")

                                'CHIUSURA [1.4.1] DatiAnagrafici
                                objWriter.WriteLine("            </DatiAnagrafici>")

                                'Libreria.genUserMsgBox(Me, "3")
                                '[1.4.2] Sede
                                objWriter.WriteLine("            <Sede>")

                                '[1.4.2.1] Indirizzo
                                objWriter.WriteLine("                <Indirizzo>" & Trim(Rs("indirizzo")) & "</Indirizzo>")

                                '[1.4.2.3] CAP
                                If rdbtnNoleggi.Checked = True Then 'Noleggi
                                    If UCase(Rs("nazione")) = "ITALIA" Then 'Cliente Italiano
                                        objWriter.WriteLine("                <CAP>" & Trim(Rs("cap")) & "</CAP>")
                                    Else
                                        objWriter.WriteLine("                <CAP>00000</CAP>")
                                    End If
                                Else
                                    If Rs("nazione") = "16" Then 'Cliente Italiano
                                        objWriter.WriteLine("                <CAP>" & Trim(Rs("cap")) & "</CAP>")
                                    Else
                                        objWriter.WriteLine("                <CAP>00000</CAP>")
                                    End If
                                End If


                                '[1.4.2.4] Comune
                                objWriter.WriteLine("                <Comune>" & Trim(Rs("citta")) & "</Comune>")

                                '[1.4.2.5] Provincia
                                If rdbtnNoleggi.Checked = True Then 'Noleggi
                                    If UCase(Rs("nazione")) = "ITALIA" Then 'Cliente Italiano
                                        objWriter.WriteLine("                <Provincia>" & Trim(Rs("provincia")) & "</Provincia>")
                                    Else
                                        objWriter.WriteLine("                <Provincia>EE</Provincia>")
                                    End If
                                Else
                                    If Rs("nazione") = "16" Then 'Cliente Italiano
                                        objWriter.WriteLine("                <Provincia>" & Trim(Rs("provincia")) & "</Provincia>")
                                    Else
                                        objWriter.WriteLine("                <Provincia>EE</Provincia>")
                                    End If
                                End If
                                

                                '[1.4.2.6] Nazione
                                objWriter.WriteLine("                <Nazione>" & Rs("cod3166_alpha2") & "</Nazione>")

                                'CHIUSURA [1.4.2] Sede
                                objWriter.WriteLine("            </Sede>")

                                'CHIUSURA [1.4] CessionarioCommittente
                                objWriter.WriteLine("        </CessionarioCommittente>")

                                'CHIUSURA [1] FatturaElettronicaHeader
                                objWriter.WriteLine("    </FatturaElettronicaHeader>")

                                'Libreria.genUserMsgBox(Me, "4")
                                '[2] FatturaElettronicaBody
                                objWriter.WriteLine("    <FatturaElettronicaBody>")

                                '[2.1] DatiGenerali
                                objWriter.WriteLine("        <DatiGenerali>")

                                '[2.1.1] DatiGeneraliDocumento
                                objWriter.WriteLine("            <DatiGeneraliDocumento>")

                                '[2.1.1.1] TipoDocumento
                                objWriter.WriteLine("                <TipoDocumento>TD01</TipoDocumento>")

                                '[2.1.1.2] Divisa
                                objWriter.WriteLine("                <Divisa>EUR</Divisa>")

                                Dim Mese, Giorno As String
                                'Libreria.genUserMsgBox(Me, Month(Rs("data_fattura")))
                                Mese = Month(Rs("data_fattura"))
                                If Len(Mese) = 1 Then
                                    Mese = "0" & Mese
                                    'Libreria.genUserMsgBox(Me, "IF")
                                Else
                                    Mese = Mese
                                    'Libreria.genUserMsgBox(Me, "ELSE")
                                End If

                                Giorno = Day(Rs("data_fattura"))
                                If Len(Giorno) = 1 Then
                                    Giorno = "0" & Giorno
                                Else
                                    Giorno = Giorno
                                End If
                                Dim dataIN As String = Year(Rs("data_fattura")) & "-" & Mese & "-" & Giorno

                                '[2.1.1.3] Data
                                objWriter.WriteLine("                <Data>" & dataIN & "</Data>")

                                If rdbtnNoleggi.Checked = True Then 'Noleggio
                                    If Rs("num_fattura") = "901" Then
                                        Dim aa As String
                                        aa = Rs("num_fattura")
                                    End If

                                Else
                                    If Rs("codice_fattura") = "901" Then
                                        Dim aa As String
                                        aa = Rs("codice_fattura")
                                    End If

                                End If


                                If rdbtnNoleggi.Checked = True Then 'Noleggio
                                    '[2.1.1.4] Numero
                                    objWriter.WriteLine("                <Numero>" & Rs("num_fattura") & "</Numero>")
                                Else
                                    '[2.1.1.4] Numero
                                    objWriter.WriteLine("                <Numero>" & Rs("codice_fattura") & "</Numero>")
                                End If


                                'se riga non imponibile ivato e importo >77,47 - 04.10.2022 salvo
                                Dim id_tipo_fatttura As String = "multe"
                                If rdbtnNoleggi.Checked = True Then
                                    id_tipo_fatttura = "nolo"
                                End If
                                If funzioni_comuni_new.getImpostaBollo(Rs!id, id_tipo_fatttura) Then
                                    objWriter.WriteLine("                <DatiBollo>")
                                    objWriter.WriteLine("                  <BolloVirtuale>SI</BolloVirtuale>")
                                    objWriter.WriteLine("                  <ImportoBollo>           2.00</ImportoBollo>")
                                    objWriter.WriteLine("                </DatiBollo>")
                                End If

                                If rdbtnNoleggi.Checked = True Then 'Noleggio
                                    Dim ScontoMaggiorazione As String = 0
                                    '[2.1.1.8] ScontoMaggiorazione                        
                                    If Rs("totale_fattura") <> Rs("totale_pagamenti") Then
                                        Dim Abbuono As String
                                        Dim Tipo As String

                                        Abbuono = Rs("saldo")
                                        If Abbuono < 0 Then
                                            Abbuono = -1 * (Abbuono / 2)
                                            Tipo = "MG"
                                        Else
                                            Abbuono = -1 * (Abbuono / 2)
                                            Tipo = "SC"
                                        End If

                                        ScontoMaggiorazione = FormatNumber(Abbuono, 2, , , vbFalse)
                                        ScontoMaggiorazione = ScontoMaggiorazione.Replace(",", ".")
                                        objWriter.WriteLine("                <ScontoMaggiorazione>")

                                        '[2.1.1.8.1] Tipo
                                        objWriter.WriteLine("                    <Tipo>" & Tipo & "</Tipo>")

                                        '[2.1.1.8.3] Importo                           
                                        objWriter.WriteLine("                    <Importo>" & ScontoMaggiorazione & "</Importo>")

                                        objWriter.WriteLine("                </ScontoMaggiorazione>")
                                    End If

                                    '[2.1.1.9] ImportoTotaleDocumento     
                                    ScontoMaggiorazione = ScontoMaggiorazione.Replace(".", ",")
                                    Dim ImportoTotaleDocumento As String = CDbl(Rs("totale_fattura")) + CDbl(ScontoMaggiorazione)
                                    ImportoTotaleDocumento = FormatNumber(ImportoTotaleDocumento, 2, , , vbFalse)
                                    ImportoTotaleDocumento = ImportoTotaleDocumento.Replace(",", ".")
                                    'Libreria.genUserMsgBox(Me, ImportoTotaleDocumento)
                                    objWriter.WriteLine("                <ImportoTotaleDocumento>" & ImportoTotaleDocumento & "</ImportoTotaleDocumento>")
                                Else
                                    'Libreria.genUserMsgBox(Me, "5")
                                    '[2.1.1.9] ImportoTotaleDocumento                                 
                                    Dim ImportoTotaleDocumento As String = CDbl(Rs("TotaleImponibile")) + CDbl(Rs("TotaleIVA"))
                                    ImportoTotaleDocumento = FormatNumber(ImportoTotaleDocumento, 2, , , vbFalse)
                                    ImportoTotaleDocumento = ImportoTotaleDocumento.Replace(",", ".")
                                    'Libreria.genUserMsgBox(Me, ImportoTotaleDocumento)
                                    objWriter.WriteLine("                <ImportoTotaleDocumento>" & ImportoTotaleDocumento & "</ImportoTotaleDocumento>")
                                End If


                                'CHIUSURA [2.1.1] DatiGeneraliDocumento
                                objWriter.WriteLine("            </DatiGeneraliDocumento>")

                                If rdbtnNoleggi.Checked = True Then 'Noleggio
                                    '[2.1.3] DatiContratto
                                    objWriter.WriteLine("            <DatiContratto>")

                                    '[2.1.3.1] RiferimentoNumeroLinea
                                    objWriter.WriteLine("                <RiferimentoNumeroLinea>1</RiferimentoNumeroLinea>")

                                    '[2.1.3.2] IdDocumento
                                    objWriter.WriteLine("                <IdDocumento>" & Trim(Rs("num_contratto_rif")) & "</IdDocumento>")

                                    '[2.1.3.3] Data
                                    Dim Mese2, Giorno2 As String
                                    'Libreria.genUserMsgBox(Me, Month(Rs("data_fattura")))
                                    Mese2 = Month(Rs("data_uscita"))
                                    If Len(Mese2) = 1 Then
                                        Mese2 = "0" & Mese2
                                        'Libreria.genUserMsgBox(Me, "IF")
                                    Else
                                        Mese2 = Mese2
                                        'Libreria.genUserMsgBox(Me, "ELSE")
                                    End If

                                    Giorno2 = Day(Rs("data_uscita"))
                                    If Len(Giorno2) = 1 Then
                                        Giorno2 = "0" & Giorno2
                                    Else
                                        Giorno2 = Giorno2
                                    End If
                                    Dim dataIN2 As String = Year(Rs("data_uscita")) & "-" & Mese2 & "-" & Giorno2
                                    objWriter.WriteLine("                <Data>" & dataIN2 & "</Data>")

                                    'CHIUSURA [2.1.3] DatiContratto
                                    objWriter.WriteLine("            </DatiContratto>")
                                End If

                                'CHIUSURA [2.1] DatiGenerali
                                objWriter.WriteLine("        </DatiGenerali>")

                                'Libreria.genUserMsgBox(Me, "6")
                                '[2.2] DatiBeniServizi
                                objWriter.WriteLine("        <DatiBeniServizi>")

                                Dim numLinea As Integer = 0
                                Dim NumRighe2 As Integer

                                Dim DbcRighe As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                                DbcRighe.Open()

                                Dim SqlRighe As String
                                If rdbtnNoleggi.Checked = True Then 'Noleggio
                                    SqlRighe = "select * from fatture_nolo_righe where id_fattura=" & Rs("id")
                                Else
                                    SqlRighe = "select * from fatture_riga where id_fattura=" & Rs("id")
                                End If
                                'Libreria.genUserMsgBox(Me, SqlRighe)

                                Dim CmdRighe As New Data.SqlClient.SqlCommand(SqlRighe, DbcRighe)

                                Dim RsRighe As Data.SqlClient.SqlDataReader
                                RsRighe = CmdRighe.ExecuteReader()
                                If RsRighe.HasRows Then
                                    Do While RsRighe.Read
                                        Dim DbcNumRighe2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                                        DbcNumRighe2.Open()

                                        Dim SqlNumRighe2 As String

                                        If rdbtnNoleggi.Checked = True Then 'Noleggio
                                            SqlNumRighe2 = "select count(*) from fatture_nolo_righe where id_fattura=" & Rs("id")
                                        Else
                                            SqlNumRighe2 = "select count(*) from fatture_riga where id_fattura=" & Rs("id")
                                        End If
                                        'Libreria.genUserMsgBox(Me, SqlNumRighe2)

                                        Dim CmdNumRighe2 As New Data.SqlClient.SqlCommand(SqlNumRighe2, DbcNumRighe2)

                                        Dim RsNumRighe2 As Data.SqlClient.SqlDataReader
                                        RsNumRighe2 = CmdNumRighe2.ExecuteReader()
                                        If RsNumRighe2.HasRows Then
                                            Do While RsNumRighe2.Read
                                                NumRighe2 = RsNumRighe2(0)
                                                'Libreria.genUserMsgBox(Me, NumRighe2)
                                                'ProgressBar1.Maximum = NumRighe
                                            Loop
                                        End If

                                        CmdNumRighe2.Dispose()
                                        CmdNumRighe2 = Nothing

                                        RsNumRighe2.Close()
                                        DbcNumRighe2.Close()
                                        RsNumRighe2 = Nothing
                                        DbcNumRighe2 = Nothing


                                        '[2.2.1] DettaglioLinee
                                        objWriter.WriteLine("            <DettaglioLinee>")

                                        numLinea = numLinea + 1
                                        '[2.2.1.1] NumeroLinea
                                        objWriter.WriteLine("                <NumeroLinea>" & numLinea & "</NumeroLinea>")

                                        '[2.2.1.4] Descrizione
                                        objWriter.WriteLine("                <Descrizione>" & RsRighe("descrizione") & "</Descrizione>")

                                        '[2.2.1.5] Quantita
                                        Dim Quantita As String = 0
                                        Quantita = FormatNumber(RsRighe("quantita"), 2, , , vbFalse)
                                        Quantita = Quantita.Replace(",", ".")

                                        'Libreria.genUserMsgBox(Me, "7")
                                        'Modifica
                                        Quantita = "1.00"
                                        objWriter.WriteLine("                <Quantita>" & Quantita & "</Quantita>")
                                        'Libreria.genUserMsgBox(Me, Quantita)

                                        'If Quantita = "1.00" Then
                                        '    '[2.2.1.6] UnitaMisura
                                        '    objWriter.WriteLine("                <UnitaMisura>Pezzo</UnitaMisura>")
                                        'Else
                                        '    '[2.2.1.6] UnitaMisura
                                        '    objWriter.WriteLine("                <UnitaMisura>Giorno</UnitaMisura>")
                                        'End If

                                        '[2.2.1.9] PrezzoUnitario
                                        Dim PrezzoUnitario As String = 0
                                        'If rdbtnNoleggi.Checked = True Then 'Noleggio
                                        '    PrezzoUnitario = FormatNumber(RsRighe("costo_unitario"), 2, , , vbFalse)
                                        'Else
                                        '    PrezzoUnitario = FormatNumber(RsRighe("Prezzo"), 2, , , vbFalse)
                                        'End If

                                        If rdbtnNoleggi.Checked = True Then 'Noleggio
                                            PrezzoUnitario = FormatNumber(RsRighe("totale"), 2, , , vbFalse)
                                        Else
                                            PrezzoUnitario = CDbl(RsRighe("Prezzo")) * CDbl(RsRighe("quantita"))
                                            PrezzoUnitario = FormatNumber(PrezzoUnitario, 2, , , vbFalse)
                                        End If

                                        PrezzoUnitario = PrezzoUnitario.Replace(",", ".")
                                        objWriter.WriteLine("                <PrezzoUnitario>" & PrezzoUnitario & "</PrezzoUnitario>")

                                        '[2.2.1.11] PrezzoTotale
                                        Dim PrezzoTotale As String = 0
                                        If rdbtnNoleggi.Checked = True Then 'Noleggio
                                            PrezzoTotale = FormatNumber(RsRighe("totale"), 2, , , vbFalse)
                                        Else
                                            PrezzoTotale = CDbl(RsRighe("Prezzo")) * CDbl(RsRighe("quantita"))
                                            PrezzoTotale = FormatNumber(PrezzoTotale, 2, , , vbFalse)
                                        End If

                                        PrezzoTotale = PrezzoTotale.Replace(",", ".")
                                        objWriter.WriteLine("                <PrezzoTotale>" & PrezzoTotale & "</PrezzoTotale>")

                                        'Salvo informazione Prezzo Totale per il campo Rieplilogo IVA [2.2.2.5] ImponibileImporto                               

                                        If rdbtnNoleggi.Checked = True Then 'Noleggio
                                            Select Case RsRighe("aliquota_iva")
                                                Case Is = 0
                                                    PrzTot0 = PrzTot0 + RsRighe("totale")
                                                Case Is = 21
                                                    PrzTot21 = PrzTot21 + RsRighe("totale")
                                                Case Is = 22
                                                    PrzTot22 = PrzTot22 + RsRighe("totale")
                                            End Select
                                        Else
                                            'Libreria.genUserMsgBox(Me, "8")
                                            Select Case RsRighe("aliquotaiva")
                                                Case Is = 0
                                                    PrzTot0 = PrzTot0 + (CDbl(RsRighe("Prezzo")) * CDbl(RsRighe("quantita")))
                                                Case Is = 21
                                                    PrzTot21 = PrzTot21 + (CDbl(RsRighe("Prezzo")) * CDbl(RsRighe("quantita")))
                                                Case Is = 22
                                                    PrzTot22 = PrzTot22 + (CDbl(RsRighe("Prezzo")) * CDbl(RsRighe("quantita")))
                                            End Select
                                        End If


                                        '[2.2.1.12] AliquotaIva
                                        Dim AliquotaIva As String = 0
                                        If rdbtnNoleggi.Checked = True Then 'Noleggio
                                            AliquotaIva = FormatNumber(RsRighe("aliquota_iva"), 2, , , vbFalse)
                                        Else
                                            AliquotaIva = FormatNumber(RsRighe("aliquotaiva"), 2, , , vbFalse)
                                        End If
                                        AliquotaIva = AliquotaIva.Replace(",", ".")
                                        objWriter.WriteLine("                <AliquotaIVA>" & AliquotaIva & "</AliquotaIVA>")

                                        'Salvo informazione iva per il campo Rieplilogo IVA [2.2.2.6] Imposta                               

                                        If rdbtnNoleggi.Checked = True Then 'Noleggio
                                            Select Case RsRighe("aliquota_iva")
                                                Case Is = 0
                                                    IVA0 = 0
                                                Case Is = 21
                                                    IVA21 = IVA21 + RsRighe("iva")
                                                Case Is = 22
                                                    IVA22 = IVA22 + RsRighe("iva")
                                            End Select
                                        Else
                                            'Libreria.genUserMsgBox(Me, "9")
                                            Select Case RsRighe("aliquotaiva")
                                                Case Is = 0
                                                    IVA0 = 0
                                                Case Is = 21
                                                    IVA21 = IVA21 + ((RsRighe("imponibile") * RsRighe("aliquotaiva")) / 100)
                                                Case Is = 22
                                                    IVA22 = IVA22 + ((RsRighe("imponibile") * RsRighe("aliquotaiva")) / 100)
                                            End Select
                                        End If


                                        'Libreria.genUserMsgBox(Me, "10")
                                        '[2.2.1.14] Natura     
                                        If rdbtnNoleggi.Checked = True Then 'Noleggio
                                            If RsRighe("aliquota_iva") = 0 Then
                                                objWriter.WriteLine("                <Natura>N1</Natura>")
                                            End If
                                        Else
                                            If RsRighe("aliquotaiva") = 0 Then
                                                objWriter.WriteLine("                <Natura>N1</Natura>")
                                            End If
                                        End If

                                        'Libreria.genUserMsgBox(Me, "11")
                                        If rdbtnNoleggi.Checked = True Then 'Noleggio
                                            If numLinea = NumRighe2 Then
                                                '[2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("                <AltriDatiGestionali>")

                                                '[2.2.1.16.1] TipoDato
                                                objWriter.WriteLine("                  <TipoDato>AltriDati</TipoDato>")

                                                '[2.2.1.16.2] RiferimentoTesto
                                                objWriter.WriteLine("                  <RiferimentoTesto>Modello auto / Car model: " & Trim(Rs("modello")) & "</RiferimentoTesto>")

                                                'CHIUSURA [2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("               </AltriDatiGestionali>")

                                                '[2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("                <AltriDatiGestionali>")

                                                '[2.2.1.16.1] TipoDato
                                                objWriter.WriteLine("                  <TipoDato>AltriDati</TipoDato>")

                                                '[2.2.1.16.2] RiferimentoTesto
                                                objWriter.WriteLine("                  <RiferimentoTesto>Targa / Car plate: " & Trim(Rs("targa")) & "</RiferimentoTesto>")

                                                'CHIUSURA [2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("               </AltriDatiGestionali>")

                                                '[2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("                <AltriDatiGestionali>")

                                                '[2.2.1.16.1] TipoDato
                                                objWriter.WriteLine("                  <TipoDato>AltriDati</TipoDato>")

                                                '[2.2.1.16.2] RiferimentoTesto
                                                objWriter.WriteLine("                  <RiferimentoTesto>Conducente / Driver: " & Trim(Rs("conducente")) & "</RiferimentoTesto>")

                                                'CHIUSURA [2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("               </AltriDatiGestionali>")

                                                '[2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("                <AltriDatiGestionali>")

                                                '[2.2.1.16.1] TipoDato
                                                objWriter.WriteLine("                  <TipoDato>AltriDati</TipoDato>")

                                                '[2.2.1.16.2] RiferimentoTesto
                                                objWriter.WriteLine("                  <RiferimentoTesto>Stazione di ritiro / Pick-up location: " & Rs("stazione_uscita") & "</RiferimentoTesto>")

                                                'CHIUSURA [2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("               </AltriDatiGestionali>")

                                                '[2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("                <AltriDatiGestionali>")

                                                '[2.2.1.16.1] TipoDato
                                                objWriter.WriteLine("                  <TipoDato>AltriDati</TipoDato>")

                                                '[2.2.1.16.2] RiferimentoTesto
                                                objWriter.WriteLine("                  <RiferimentoTesto>Data di ritiro / Pick-up date: " & Rs("data_uscita") & "</RiferimentoTesto>")

                                                'CHIUSURA [2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("               </AltriDatiGestionali>")

                                                '[2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("                <AltriDatiGestionali>")

                                                '[2.2.1.16.1] TipoDato
                                                objWriter.WriteLine("                  <TipoDato>AltriDati</TipoDato>")

                                                '[2.2.1.16.2] RiferimentoTesto
                                                objWriter.WriteLine("                  <RiferimentoTesto>Km iniziali / Kms out: " & Rs("km_uscita") & "</RiferimentoTesto>")

                                                'CHIUSURA [2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("               </AltriDatiGestionali>")

                                                '[2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("                <AltriDatiGestionali>")

                                                '[2.2.1.16.1] TipoDato
                                                objWriter.WriteLine("                  <TipoDato>AltriDati</TipoDato>")

                                                '[2.2.1.16.2] RiferimentoTesto
                                                objWriter.WriteLine("                  <RiferimentoTesto>Stazione di consegna / Dropoff location " & Rs("stazione_rientro") & "</RiferimentoTesto>")

                                                'CHIUSURA [2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("               </AltriDatiGestionali>")

                                                '[2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("                <AltriDatiGestionali>")

                                                '[2.2.1.16.1] TipoDato
                                                objWriter.WriteLine("                  <TipoDato>AltriDati</TipoDato>")

                                                '[2.2.1.16.2] RiferimentoTesto
                                                objWriter.WriteLine("                  <RiferimentoTesto>Data di consegna / Drop off date: " & Rs("data_rientro") & "</RiferimentoTesto>")

                                                'CHIUSURA [2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("               </AltriDatiGestionali>")

                                                '[2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("                <AltriDatiGestionali>")

                                                '[2.2.1.16.1] TipoDato
                                                objWriter.WriteLine("                  <TipoDato>AltriDati</TipoDato>")

                                                '[2.2.1.16.2] RiferimentoTesto
                                                objWriter.WriteLine("                  <RiferimentoTesto>Km finali / Kms in: " & Rs("km_rientro") & "</RiferimentoTesto>")

                                                'CHIUSURA [2.2.1.16] AltriDatiGestionali
                                                objWriter.WriteLine("               </AltriDatiGestionali>")
                                            End If
                                        End If

                                        'CHIUSURA [2.2.1] DettaglioLinee
                                        objWriter.WriteLine("            </DettaglioLinee>")
                                    Loop





                                End If

                                CmdRighe.Dispose()
                                CmdRighe = Nothing

                                RsRighe.Close()
                                DbcRighe.Close()
                                RsRighe = Nothing
                                DbcRighe = Nothing


                                Dim ImponibileImportoN1 As Double = 0      'inserito il 26.10.2021




                                If IVA0 <> -1 Or IVA0 = 0 Then 'Modificata il 26.10.2021 (aggiunto Or IVA0 = 0 da verificare ???)

                                    '[2.2.2] DatiRiepilogo
                                    objWriter.WriteLine("            <DatiRiepilogo>")

                                    'Libreria.genUserMsgBox(Me, IVA22)

                                    '[2.2.2.1] AliquotaIVA                                                        
                                    objWriter.WriteLine("                <AliquotaIVA>0.00</AliquotaIVA>")

                                    '[2.2.2.2 ] Natura
                                    objWriter.WriteLine("                <Natura>N1</Natura>")

                                    '[2.2.2.5 ] ImponibileImporto
                                    Dim ImponibileImporto As String = 0


                                    If rdbtnNoleggi.Checked = False Then 'Multe
                                        ImponibileImporto = FormatNumber(PrzTot0, 2, , , vbFalse)
                                    End If

                                    If rdbtnNoleggi.Checked = True Then 'Noleggi
                                        ' ImponibileImporto = FormatNumber(Rs("imponibile"), 2, , , vbFalse)
                                        ImponibileImporto = FormatNumber(PrzTot0, 2, , , vbFalse)   'modificato il 26.10.2021 deve essere il valore del pz tot
                                        ImponibileImportoN1 = PrzTot0
                                        'Else 'Multe                                        
                                        'ImponibileImporto = FormatNumber(Rs("TotaleImponibile"), 2, , , vbFalse)
                                    End If
                                    ImponibileImporto = ImponibileImporto.Replace(",", ".")
                                    objWriter.WriteLine("                <ImponibileImporto>" & ImponibileImporto & "</ImponibileImporto>")

                                    '[2.2.2.6] Imposta
                                    Dim Imposta As String = 0

                                    If rdbtnNoleggi.Checked = False Then 'Multe
                                        Imposta = FormatNumber(IVA0, 2, , , vbFalse)
                                    End If


                                    If rdbtnNoleggi.Checked = True Then 'Noleggi
                                        If IVA0 = "0" Then
                                            Imposta = FormatNumber(0, 2, , , vbFalse)           'aggiunto 26.10.2021 se IVA=0
                                        Else
                                            Imposta = FormatNumber(Rs("iva"), 2, , , vbFalse)
                                        End If

                                        'Else 'Multe                                        
                                        'Imposta = FormatNumber(Rs("TotaleIVA"), 2, , , vbFalse)
                                    End If
                                    Imposta = Imposta.Replace(",", ".")
                                    objWriter.WriteLine("                <Imposta>" & Imposta & "</Imposta>")

                                    '[2.2.2.8] RiferimentoNormativo
                                    objWriter.WriteLine("                <RiferimentoNormativo>escluse ex art. 15</RiferimentoNormativo>")

                                    'CHIUSURA [2.2.2] DatiRiepilogo
                                    objWriter.WriteLine("            </DatiRiepilogo>")
                                End If


                                If IVA21 <> 0 Then
                                    '[2.2.2] DatiRiepilogo
                                    objWriter.WriteLine("            <DatiRiepilogo>")

                                    'Libreria.genUserMsgBox(Me, IVA22)

                                    '[2.2.2.1] AliquotaIVA                                                        
                                    objWriter.WriteLine("                  <AliquotaIVA>21.00</AliquotaIVA>")

                                    '[2.2.2.5 ] ImponibileImporto
                                    Dim ImponibileImporto As String = 0

                                    If rdbtnNoleggi.Checked = False Then 'Multe
                                        ImponibileImporto = FormatNumber(PrzTot21, 2, , , vbFalse)
                                    End If

                                    If rdbtnNoleggi.Checked = True Then 'Noleggi
                                        ImponibileImporto = FormatNumber(Rs("imponibile"), 2, , , vbFalse)
                                        'Else 'Multe                                        
                                        'ImponibileImporto = FormatNumber(Rs("TotaleImponibile"), 2, , , vbFalse)
                                    End If
                                    ImponibileImporto = ImponibileImporto.Replace(",", ".")
                                    objWriter.WriteLine("                <ImponibileImporto>" & ImponibileImporto & "</ImponibileImporto>")

                                    '[2.2.2.6] Imposta
                                    Dim Imposta As String = 0

                                    If rdbtnNoleggi.Checked = False Then 'Multe
                                        Imposta = FormatNumber(IVA21, 2, , , vbFalse)
                                    End If
                                    If rdbtnNoleggi.Checked = True Then 'Noleggi
                                        Imposta = FormatNumber(Rs("iva"), 2, , , vbFalse)
                                        'Else 'Multe                                        
                                        'Imposta = FormatNumber(Rs("TotaleIVA"), 2, , , vbFalse)
                                    End If
                                    Imposta = Imposta.Replace(",", ".")
                                    objWriter.WriteLine("                <Imposta>" & Imposta & "</Imposta>")

                                    '[2.2.2.7] EsigibilitaIVA
                                    objWriter.WriteLine("               <EsigibilitaIVA>I</EsigibilitaIVA>")

                                    'CHIUSURA [2.2.2] DatiRiepilogo
                                    objWriter.WriteLine("            </DatiRiepilogo>")
                                End If



                                If IVA22 <> 0 Then
                                    'Libreria.genUserMsgBox(Me, "11")
                                    ''[2.2.2] DatiRiepilogo
                                    'objWriter.WriteLine("            <DatiRiepilogo>")

                                    ''[2.2.2.1] AliquotaIVA                                                        
                                    'objWriter.WriteLine("                  <AliquotaIVA>22.00</AliquotaIVA>")


                                    ''[2.2.2.5 ] ImponibileImporto
                                    'Dim ImponibileImporto As String = 0
                                    ''Libreria.genUserMsgBox(Me, "Imponibile " & RsRighe("totale"))
                                    ''ImponibileImporto = FormatNumber(PrzTot22, 2, , , vbFalse)
                                    ''ImponibileImporto = FormatNumber(RsRighe("totale") - PrzTot22, 2, , , vbFalse)
                                    'ImponibileImporto = FormatNumber(Rs("imponibile"), 2, , , vbFalse)

                                    'ImponibileImporto = ImponibileImporto.Replace(",", ".")
                                    ''Libreria.genUserMsgBox(Me, "Imponibile " & ImponibileImporto)

                                    'objWriter.WriteLine("                <ImponibileImporto>" & ImponibileImporto & "</ImponibileImporto>")

                                    ''[2.2.2.6] Imposta
                                    'Dim Imposta As String = 0
                                    ''Imposta = FormatNumber(IVA22, 2, , , vbFalse)
                                    'Imposta = FormatNumber(Rs("iva"), 2, , , vbFalse)
                                    'Imposta = Imposta.Replace(",", ".")

                                    'objWriter.WriteLine("                <Imposta>" & Imposta & "</Imposta>")


                                    ''[2.2.2.7] EsigibilitaIVA
                                    'objWriter.WriteLine("               <EsigibilitaIVA>I</EsigibilitaIVA>")

                                    ''CHIUSURA [2.2.2] DatiRiepilogo
                                    'objWriter.WriteLine("            </DatiRiepilogo>")


                                    '----------------
                                    '[2.2.2] DatiRiepilogo
                                    objWriter.WriteLine("            <DatiRiepilogo>")

                                    'Libreria.genUserMsgBox(Me, IVA22)

                                    '[2.2.2.1] AliquotaIVA                                                        
                                    objWriter.WriteLine("                  <AliquotaIVA>22.00</AliquotaIVA>")

                                    '[2.2.2.5 ] ImponibileImporto
                                    Dim ImponibileImporto As String = 0

                                    If rdbtnNoleggi.Checked = False Then 'Multe
                                        ImponibileImporto = FormatNumber(PrzTot22, 2, , , vbFalse)
                                    End If

                                    If rdbtnNoleggi.Checked = True Then 'Noleggi
                                        ImponibileImporto = Rs("imponibile") - ImponibileImportoN1          'Aggiunto il 26.10.2021
                                        ImponibileImporto = FormatNumber(ImponibileImporto, 2, , , vbFalse) 'modificato il 26.10.2021 ( rs("imponibile") )
                                        'Else 'Multe                                        
                                        'ImponibileImporto = FormatNumber(Rs("TotaleImponibile"), 2, , , vbFalse)
                                    End If
                                    ImponibileImporto = ImponibileImporto.Replace(",", ".")
                                    objWriter.WriteLine("                <ImponibileImporto>" & ImponibileImporto & "</ImponibileImporto>")


                                    '[2.2.2.6] Imposta
                                    Dim Imposta As String = 0

                                    If rdbtnNoleggi.Checked = False Then 'Multe
                                        Imposta = FormatNumber(IVA22, 2, , , vbFalse)
                                    End If

                                    If rdbtnNoleggi.Checked = True Then 'Noleggi
                                        Imposta = FormatNumber(Rs("iva"), 2, , , vbFalse)
                                        'Else 'Multe                                        
                                        'Imposta = FormatNumber(Rs("TotaleIVA"), 2, , , vbFalse)
                                    End If
                                    Imposta = Imposta.Replace(",", ".")
                                    objWriter.WriteLine("                <Imposta>" & Imposta & "</Imposta>")

                                    '[2.2.2.7] EsigibilitaIVA
                                    objWriter.WriteLine("               <EsigibilitaIVA>I</EsigibilitaIVA>")

                                    'CHIUSURA [2.2.2] DatiRiepilogo
                                    objWriter.WriteLine("            </DatiRiepilogo>")
                                End If

                                'CHIUSURA [2.2] DatiBeniServizi
                                objWriter.WriteLine("        </DatiBeniServizi>")


                                If rdbtnNoleggi.Checked = True Then 'Noleggio
                                    Dim DbcPagamenti As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                                    DbcPagamenti.Open()

                                    Dim SqlPagamenti As String

                                    'SQL originale che comprendeva i movimenti delle fatture compresi Preautorizzazione e Autorizzazione Telefonica
                                    'SqlPagamenti = "Select * from fatture_nolo_pagamenti where  id_fattura_nolo = '" & Rs("id") & "' "

                                    'senza "Preautorizzazione" e senza "Autorizzazione Telefonica" '19.04.2021 da email del 14.04.2021
                                    'verificare testo in tabella POS_Funzioni con ID=10 che contiene uno spazio all'inizio da rimuovere 19.04.2021
                                    SqlPagamenti = "select * from fatture_nolo_pagamenti where  id_fattura_nolo = '" & Rs("id") &
                                    "' AND (tipo_pagamento <> ' Autorizzazione Telefonica' AND tipo_pagamento <> 'Preautorizzazione' AND tipo_pagamento <> 'Autorizzazione Telefonica' and tipo_pagamento <> 'Deposito' and tipo_pagamento <> 'Rimborso Deposito')"

                                            'SqlPagamenti = "select * from fatture_nolo_pagamenti where (tipo_pagamento = 'Acquisto' or tipo_pagamento = 'Pagamento' or tipo_pagamento = 'Rimborso su RA') and id_fattura_nolo = '" & Rs("id") & "'"
                                            'Libreria.genUserMsgBox(Me, SqlPagamenti)

                                            Dim CmdPagamenti As New Data.SqlClient.SqlCommand(SqlPagamenti, DbcPagamenti)

                                            Dim RsPagamenti As Data.SqlClient.SqlDataReader
                                            RsPagamenti = CmdPagamenti.ExecuteReader()
                                            If RsPagamenti.HasRows Then
                                                Do While RsPagamenti.Read
                                                    '[2.4] DatiPagamento
                                                    objWriter.WriteLine("        <DatiPagamento>")

                                                    '[2.4.1] CondizioniPagamento
                                                    objWriter.WriteLine("            <CondizioniPagamento>TP03</CondizioniPagamento>")

                                                    '[2.4.2] DettaglioPagamento
                                                    objWriter.WriteLine("            <DettaglioPagamento>")

                                                    Select Case RsPagamenti("modalita_pagamento")
                                                        Case Is = "CONTANTI"
                                                            '[2.4.2.2] ModalitaPagamento
                                                            objWriter.WriteLine("                <ModalitaPagamento>MP01</ModalitaPagamento>")
                                                Case Is = "SU FATTURA"
                                                    '[2.4.2.2] ModalitaPagamento
                                                    'tipo_pagamento Abbuono Attivo (-)
                                                    If RsPagamenti("tipo_pagamento") = "Abbuono Attivo (-)" Then
                                                        objWriter.WriteLine("                <ModalitaPagamento>MP01</ModalitaPagamento>")
                                                    End If


                                                Case Is = "BANCOMAT"
                                                    '[2.4.2.2] ModalitaPagamento
                                                    objWriter.WriteLine("                <ModalitaPagamento>MP08</ModalitaPagamento>")
                                                        Case Is = "C.CREDITO"
                                                            '[2.4.2.2] ModalitaPagamento
                                                            objWriter.WriteLine("                <ModalitaPagamento>MP08</ModalitaPagamento>")
                                                        Case Is = "American Express"
                                                            '[2.4.2.2] ModalitaPagamento
                                                            objWriter.WriteLine("                <ModalitaPagamento>MP08</ModalitaPagamento>")
                                                        Case Is = "BONIFICO"
                                                            '[2.4.2.2] ModalitaPagamento
                                                            objWriter.WriteLine("                <ModalitaPagamento>MP05</ModalitaPagamento>")
                                                        Case Is = "STORNO"
                                                            '[2.4.2.2] ModalitaPagamento
                                                            objWriter.WriteLine("                <ModalitaPagamento>MP08</ModalitaPagamento>")
                                                    End Select

                                                    '[2.4.2.3] DataRiferimentoTerminiPagamento
                                                    Dim Mese3, Giorno3 As String
                                                    'Libreria.genUserMsgBox(Me, Month(RsPagamenti("data_pagamento")))
                                                    Mese3 = Month(RsPagamenti("data_pagamento"))
                                                    If Len(Mese3) = 1 Then
                                                        Mese3 = "0" & Mese3
                                                        'Libreria.genUserMsgBox(Me, "IF")
                                                    Else
                                                        Mese3 = Mese3
                                                        'Libreria.genUserMsgBox(Me, "ELSE")
                                                    End If

                                                    Giorno3 = Day(RsPagamenti("data_pagamento"))
                                                    If Len(Giorno3) = 1 Then
                                                        Giorno3 = "0" & Giorno3
                                                    Else
                                                        Giorno3 = Giorno3
                                                    End If
                                                    Dim dataIN3 As String = Year(RsPagamenti("data_pagamento")) & "-" & Mese3 & "-" & Giorno3
                                                    objWriter.WriteLine("                <DataRiferimentoTerminiPagamento>" & dataIN3 & "</DataRiferimentoTerminiPagamento>")

                                                    '[2.4.2.6] ImportoPagamento
                                                    Dim ImportoPagamento As String = 0
                                                    ImportoPagamento = FormatNumber(RsPagamenti("importo"), 2, , , vbFalse)
                                                    ImportoPagamento = ImportoPagamento.Replace(",", ".")
                                                    objWriter.WriteLine("                <ImportoPagamento>" & ImportoPagamento & "</ImportoPagamento>")

                                                    'CHIUSURA [2.4.2] DettaglioPagamento
                                                    objWriter.WriteLine("            </DettaglioPagamento>")

                                                    'CHIUSURA [2.4] DatiPagamento
                                                    objWriter.WriteLine("        </DatiPagamento>")
                                                Loop
                                            End If

                                            CmdPagamenti.Dispose()
                                            CmdPagamenti = Nothing

                                            RsPagamenti.Close()
                                            DbcPagamenti.Close()
                                            RsPagamenti = Nothing
                                            DbcPagamenti = Nothing
                                        Else
                                            '[2.4] DatiPagamento
                                            objWriter.WriteLine("        <DatiPagamento>")

                                    '[2.4.1] CondizioniPagamento
                                    objWriter.WriteLine("            <CondizioniPagamento>TP02</CondizioniPagamento>")

                                    '[2.4.2] DettaglioPagamento
                                    objWriter.WriteLine("            <DettaglioPagamento>")

                                    '[2.4.2.2] ModalitaPagamento
                                    objWriter.WriteLine("                <ModalitaPagamento>MP08</ModalitaPagamento>")

                                    '[2.4.2.3] DataRiferimentoTerminiPagamento
                                    objWriter.WriteLine("                <DataRiferimentoTerminiPagamento>" & dataIN & "</DataRiferimentoTerminiPagamento>")

                                    '[2.4.2.6] ImportoPagamento
                                    Dim ImportoPagamento2 As String = CDbl(Rs("TotaleImponibile")) + CDbl(Rs("TotaleIVA"))
                                    ImportoPagamento2 = FormatNumber(ImportoPagamento2, 2, , , vbFalse)
                                    ImportoPagamento2 = ImportoPagamento2.Replace(",", ".")
                                    'Libreria.genUserMsgBox(Me, ImportoTotaleDocumento)
                                    objWriter.WriteLine("                <ImportoPagamento>" & ImportoPagamento2 & "</ImportoPagamento>")

                                    'CHIUSURA [2.4.2] DettaglioPagamento
                                    objWriter.WriteLine("            </DettaglioPagamento>")

                                    'CHIUSURA [2.4] DatiPagamento
                                    objWriter.WriteLine("        </DatiPagamento>")
                                End If

                                'CHIUSURA [2] FatturaElettronicaBody
                                objWriter.WriteLine("    </FatturaElettronicaBody>")

                                'CHIUSURA FatturaElettronica
                                objWriter.WriteLine("</ns0:FatturaElettronica>")

                                objWriter.Close()

                                'InvioMail(NomeCartella, NFile)

                            Loop

                            CmdExecute.Dispose()
                            CmdExecute = Nothing
                            DbcExecute.Close()
                            DbcExecute.Dispose()
                            DbcExecute = Nothing

                        End If

                        Cmd.Dispose()
                        Cmd = Nothing

                        Rs.Close()
                        Dbc.Close()
                        Rs = Nothing
                        Dbc = Nothing


                        'ProgressBar1.Visible = False
                        If rdbtnNoleggi.Checked = True Then 'Noleggi
                            'MsgBox("Generate " & NumFattGenerate & " Fatture Noleggio", vbDefaultButton1, "ATTENZIONE")
                            Libreria.genUserMsgBox(Me, "Generate " & NumFattGenerate & " Fatture Noleggio")
                        Else
                            'MsgBox("Generate " & NumFattGenerate & " Fatture Multe", vbDefaultButton1, "ATTENZIONE")
                            Libreria.genUserMsgBox(Me, "Generate " & NumFattGenerate & " Fatture Multe")
                        End If

                        btnCmdAvvio.Text = "Riazzera"
                        btnCmdAvvio.Enabled = True

                    End If
                Catch ex As Exception
                    Libreria.genUserMsgBox(Me, "ERRORE " & ex.Message)
                End Try
            Case Is = "Riazzera"
                btnCmdAvvio.Text = "Genera XML"
        End Select
    End Sub

    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("/default.aspx")
    End Sub

    Protected Sub btnScaricaFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnScaricaFile.Click
        If txtDataFattura.Text <> "" Then
            Session("GiornoDaScaricare") = txtDataFattura.Text
        Else
            Session("GiornoDaScaricare") = ""
        End If
        If txtNumFattura.Text <> "" Then
            Session("NumFatturaDaScaricare") = txtNumFattura.Text
        Else
            Session("NumFatturaDaScaricare") = ""
        End If
        If rdbtnNoleggi.Checked = True Then 'Noleggi
            Session("TipFattura") = "Noleggi"
        Else
            Session("TipFattura") = "Multe"
        End If

        Response.Redirect("/scarica_XML.aspx")
    End Sub
End Class
