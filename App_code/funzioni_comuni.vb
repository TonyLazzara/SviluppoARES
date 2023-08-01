Imports System.Globalization
Imports System.IO
Imports System.Security.Cryptography
Imports Microsoft.VisualBasic

Public Class funzioni_comuni

    'Tony
    Public Shared Function GetIdStazioneDaCodice(ByVal codice As String) As String


        Dim ris As String = ""

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Try


            Dim sqlStr As String = "select id from stazioni where codice = '" & codice & "'"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris = Rs1("id")
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

    Public Shared Function GetStazioneCodice(ByVal IDdellaStazione As String) As String


        Dim ris As String = ""

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Try


            Dim sqlStr As String = "select codice from stazioni where id = '" & IDdellaStazione & "'"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris = Rs1("codice")
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

    Public Shared Sub genUserMsgBox(ByVal F As System.Web.UI.Page, ByVal sMsg As String)
        'Dim sb As New StringBuilder()
        'Dim oFormObject As System.Web.UI.Control = Nothing
        'Try
        '    sMsg = sMsg.Replace("'", "\'")
        '    sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
        '    sMsg = sMsg.Replace(vbCrLf, "\n")
        '    sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
        '    sb = New StringBuilder()
        '    sb.Append(sMsg)
        '    For Each oFormObject In F.Controls
        '        If TypeOf oFormObject Is HtmlForm Then
        '            Exit For
        '        End If
        '    Next
        '    oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        'Catch ex As Exception

        'End Try

        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control = Nothing
        Try
            sMsg = sMsg.Replace("'", "\'")
            sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
            sMsg = sMsg.Replace(vbCrLf, "\n")
            'sMsg = "<script language='javascript'>alert('" & sMsg & "');</script>"
            sMsg = "alert('" & sMsg & "')"

            'sb = New StringBuilder()
            'sb.Append(sMsg)

            ScriptManager.RegisterClientScriptBlock(F, F.GetType(), "clientScript", sMsg, True)

            'Page.ClientScript.RegisterStartupScript([GetType], "MyScript", "<script>alert('hiiiii Shoyebaziz123 ')</script>")

            'For Each oFormObject In F.Controls
            '    If TypeOf oFormObject Is HtmlForm Then
            '        Exit For
            '    End If
            'Next
            'oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Function FixSQL(ByVal stringa As String) As String
        stringa = Replace(stringa, "'", "''")
        stringa = Replace(stringa, ";", "")
        stringa = Replace(stringa, "--", "")
        stringa = Replace(stringa, "+", "")
        stringa = Replace(stringa, "(", "")
        stringa = Replace(stringa, ")", "")
        stringa = Replace(stringa, "=", "")
        stringa = Replace(stringa, ">", "")
        stringa = Replace(stringa, "<", "")
        stringa = Replace(stringa, "@", "")
        stringa = Replace(stringa, "%", "")
        stringa = Replace(stringa, "[", "")
        stringa = Replace(stringa, "]", "")
        stringa = Replace(stringa, "_", "")
        stringa = Replace(stringa, "#", "[#]")
        FixSQL = stringa
    End Function

    Public Shared Function cripta(ByVal strTesto As String, ByVal intKey As Integer) As String
        Dim ctInd As Integer
        Dim chrAnalisi As String
        Dim strTesto2 As String = ""
        For ctInd = 1 To Len(strTesto)
            chrAnalisi = Mid(strTesto, ctInd, 1)
            chrAnalisi = Asc(chrAnalisi) + intKey
            chrAnalisi = chrAnalisi Mod 256
            strTesto2 = strTesto2 & Chr(chrAnalisi)
        Next
        cripta = strTesto2
    End Function

    Public Shared Function decripta(ByVal strTesto As String, ByVal intKey As Integer) As String
        Dim ctInd As Integer
        Dim chrAnalisi As String
        Dim IntValore As Integer
        Dim intResto As Integer
        Dim strTesto2 As String = ""
        For ctInd = 1 To Len(strTesto)
            chrAnalisi = Mid(strTesto, ctInd, 1)
            IntValore = Asc(chrAnalisi)
            intResto = (intKey + IntValore) Mod 256
            If (IntValore + intKey < 256) Then
                strTesto2 = strTesto2 & Chr(IntValore - intKey)
            Else
                strTesto2 = strTesto2 & Chr(256 - intKey + intResto)
            End If
        Next
        decripta = strTesto2
    End Function

    'FINE






    'Public Shared Function IsValidEmail(ByVal emailAddress As String) As Boolean

    '    Dim email As Regex = New Regex("([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})")

    '    If email.IsMatch(emailAddress) Then

    '        Return True

    '    Else

    '        Return False

    '    End If

    'End Function

    Public Shared Function GetRiferimentoFattura(numFattura As String, anno As String, tipo_fattura As String) As Array
        'restituisce riferimento 05.04.2022


        Dim ris(1) As String
        ris(0) = ""
        ris(1) = ""

        'verifica se presente firma 03.02.2022 e 09.02.2022
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Try


            Dim sqlStr As String = "select num_contratto_rif from fatture_nolo where num_fattura= '" & numFattura & "' AND year(data_fattura)='" & anno & "';"


            If tipo_fattura = "4" Then ' se multe
                sqlStr = "select id_riferimento From [Fatture] Where codice_fattura = '" & numFattura & "' AND year(data_fattura)='" & anno & "';"
            End If


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()

                If tipo_fattura = "4" Then
                    ris(0) = Rs1!id_riferimento
                Else
                    ris(0) = Rs1!num_contratto_rif
                End If

            End If

            Rs1.Close()
            Cmd1.Dispose()
            Rs1 = Nothing
            Cmd1 = Nothing


            'se multe ricava il riferimento contratto 05.04.2022
            If tipo_fattura = "4" Then

                sqlStr = "select rtrim([ContrattoNolo]) as contrattonolo From [multe] Where [id] = '" & ris(0) & "';"

                Dim Cmd2 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dim Rs2 As Data.SqlClient.SqlDataReader
                Rs2 = Cmd2.ExecuteReader()

                If Rs2.HasRows Then
                    Rs2.Read()
                    ris(0) = Rs2!contrattonolo
                End If
                Rs2.Close()
                Rs2 = Nothing
                Cmd2.Dispose()
                Cmd2 = Nothing


                'recupera ID della fattura se Multe

                sqlStr = "select [id] From [Fatture] Where codice_fattura = '" & numFattura & "' AND year(data_fattura)='" & anno & "';"

                Dim Cmd3 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dim Rs3 As Data.SqlClient.SqlDataReader
                Rs3 = Cmd3.ExecuteReader()

                If Rs3.HasRows Then
                    Rs3.Read()
                    ris(1) = Rs3!id
                End If
                Rs3.Close()
                Rs3 = Nothing
                Cmd3.Dispose()
                Cmd3 = Nothing

            End If


            Dbc.Close()
            Dbc = Nothing

        Catch ex As Exception

        End Try

        Return ris




    End Function


    Public Shared Function IsValidEmail(email As String) As Boolean

        If String.IsNullOrWhiteSpace(email) Then Return False

        ' Use IdnMapping class to convert Unicode domain names.
        Try
            'Examines the domain part of the email and normalizes it.
            Dim DomainMapper =
                Function(match As Match) As String

                    'Use IdnMapping class to convert Unicode domain names.
                    Dim idn = New IdnMapping

                    'Pull out and process domain name (throws ArgumentException on invalid)
                    Dim domainName As String = idn.GetAscii(match.Groups(2).Value)

                    Return match.Groups(1).Value & domainName

                End Function

            'Normalize the domain
            email = Regex.Replace(email, "(@)(.+)$", DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds(200))

        Catch e As RegexMatchTimeoutException
            Return False

        Catch e As ArgumentException
            Return False

        End Try

        Try
            Return Regex.IsMatch(email,
                                 "^[^@\s]+@[^@\s]+\.[^@\s]+$",
                                 RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250))

        Catch e As RegexMatchTimeoutException
            Return False

        End Try

    End Function
    Public Shared Function UpdateEmailContrattiAnagrafica(num_contratto As String, id_conducente As String, email As String, num_conducente As String) As Boolean
        'inserita 29.03.2022
        Dim ris As Boolean = False

        Dim sqlstr As String = "update contratti_conducenti set email='" & email & "' where num_contratto='" & num_contratto & "' and id_conducente='" & id_conducente
        sqlstr += "' AND num_conducente='" & num_conducente & "'"



        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim x As Integer = Cmd.ExecuteNonQuery()

            If x > 0 Then
                ris = True
            End If

            Cmd.Dispose()
            Cmd = Nothing


            'Anagrafica
            sqlstr = "update conducenti set email='" & email & "' where id_conducente='" & id_conducente & "'"

            Dim CmdA As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim xA As Integer = CmdA.ExecuteNonQuery()

            If xA > 0 Then
                ris = True
            End If

            CmdA.Dispose()
            CmdA = Nothing


            Dbc.Close()
            Dbc = Nothing

        Catch ex As Exception
            'Libreria.genUserMsgBox(Page, "Errore nell'inserimento dell'allegato. " & ex.Message)
        End Try

        Return ris



    End Function




    Public Shared Function GetEmailConducenteAnagrafica(ByVal id_conducente As String) As String


        Dim ris As String = ""

        'verifica se presente firma 03.02.2022 e 09.02.2022
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Try


            Dim sqlStr As String = "select id_conducente, rtrim(email) as email from conducenti where ID_CONDUCENTE = '" & id_conducente & "'"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris = Rs1!email
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



    Public Shared Sub RefreshListAllegatiContratti(sqlcom As SqlDataSource, dtlist As ListView, status_co As String, id_contratto As String, numero_contratto As String)
        '08.02.2022

        If status_co = "0" Then

            sqlcom.SelectCommand = "SELECT id_allegato, nome_file, cartella, (cartella + nome_file) as my_path, descrizione, data_operazione FROM contratti_prenotazioni_allegati WITH(NOLOCK) " &
                            "INNER JOIN contratti_prenotazioni_allegati_tipo WITH(NOLOCK) ON contratti_prenotazioni_allegati.id_cnt_pren_allegati_tipo=contratti_prenotazioni_allegati_tipo.id_cnt_pren_allegati_tipo WHERE id_cnt_provv=" & id_contratto &
                            " ORDER BY descrizione, nome_file"
            dtlist.DataBind()

        Else

            sqlcom.SelectCommand = "SELECT id_allegato, nome_file, cartella, (cartella + nome_file) as my_path, descrizione, data_operazione FROM contratti_prenotazioni_allegati WITH(NOLOCK) " &
                            "INNER JOIN contratti_prenotazioni_allegati_tipo WITH(NOLOCK) ON contratti_prenotazioni_allegati.id_cnt_pren_allegati_tipo=contratti_prenotazioni_allegati_tipo.id_cnt_pren_allegati_tipo WHERE num_cnt=" & numero_contratto &
                            " ORDER BY descrizione, nome_file"
            dtlist.DataBind()

        End If

    End Sub

    Public Shared Function GetIdMulte(y As String, nprot As String) As String

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim ris As String = "0"

        Try

            Dim sqlStr As String = "select id from multe where anno='" & y & "' and prot='" & nprot & "'"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris = Rs1!id
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



    Public Shared Function GetIdContratto(numcontratto As String) As String
        'verifica se presente firma 03.02.2022 e 09.02.2022
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim ris As String = "0"

        Try


            Dim sqlStr As String = "select id from contratti where num_contratto = '" & numcontratto & "' and attivo=1"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris = Rs1!id
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


    Public Shared Function AllegaContrattoDopoFirma(numcontratto As String, Optional tipo As String = "uscita") As Boolean
        Dim ris As Boolean = False

        Try
            'riempie la session che è stat valorizzata da pulsante firma contratto
            Dim mie_dati As DatiStampaContratto = HttpContext.Current.Session("DatiStampaContrattoPostFirma")
            Dim nazione As String = HttpContext.Current.Session("DatiStampaContrattoPostFirmaLang")

            'genera il contratto

            Dim newDir As String = HttpContext.Current.Server.MapPath("\allegati_pren_cnt\" & numcontratto)
            Dim newFile As String = HttpContext.Current.Server.MapPath("\allegati_pren_cnt\" & numcontratto & "\RA_" & numcontratto & ".pdf")
            If tipo = "rientro" Then 'aggiunto 16.05.2022
                newFile = HttpContext.Current.Server.MapPath("\allegati_pren_cnt\" & numcontratto & "\RB_" & numcontratto & ".pdf")
            End If

            'Verifica se file già presente se si salta la creazione del PDF 
            'e l'inserimento 

            'aggiornamento 10.03.2022 Lo crea ma non lo allega 
            'viene allegato dopo che la firma è andata a buon fine
            If File.Exists(newFile) = True Then
                'Dim StampaDocumento As ClassGeneraHtmlToPdf = New ClassGeneraHtmlToPdf
                'Dim pathContrattoPDF As String = StampaContratto.GeneraDocumentoPDF(mie_dati, numcontratto, nazione)         'restituisce stringa con path file da allegare

                'pathContrattoPDF = ""   'inserito per non inserirlo negli allegati automaticamente ma solo dopo chè è stato firmato 10.03.2022 
                'If pathContrattoPDF <> "" Then
                'Lo inserisce negli allegati 08.02.2022

                Dim id_operatore As String = HttpContext.Current.Request.Cookies("SicilyRentCar")("IdUtente")  'aggiornato salvo 15.02.2023

                Dim insertAllegati As Boolean = insertContrattoPDFtoAllegati("/allegati_pren_cnt/" & numcontratto & "/", numcontratto, id_operatore, tipo)    'aggiornato salvo 15.02.2023
                ris = insertAllegati

                If insertAllegati = True Then
                    'RefreshListAllegatiContratti(sqlAllegati, dataListAllegati, statoContratto.Text)
                End If

                'End If

            Else
                'Libreria.genUserMsgBox(Page, "Il Contratto in PDF è già presente. \nPer ricrearlo eliminare quello presente negli allegati.")
                'PostFirmaInserita = False
                'HttpContext.Current.Response.Write("Errore in AllegaContrattodopofirme:err_n_GenDOC:" & "<br/>" & ex.Message)
                Return ris
                Exit Function
            End If

        Catch ex As Exception
            HttpContext.Current.Response.Write("Errore in AllegaContrattodopofirme:err_n_GenDOC:" & "<br/>" & ex.Message)
        End Try


        Return ris




    End Function

    Public Shared Function AllegaContrattoDopoFirmaTest(numcontratto As String) As String
        Dim ris As String = "-"

        Dim errline As Integer = 0

        Try
            'riempie la session che è stat valorizzata da pulsante firma contratto
            errline = 1
            'Dim mie_dati As DatiStampaContratto = HttpContext.Current.Session("DatiStampaContrattoPostFirma")

            Dim nazione As String = HttpContext.Current.Session("DatiStampaContrattoPostFirmaLang")

            'genera il contratto
            errline = 1
            Dim newDir As String = HttpContext.Current.Server.MapPath("\allegati_pren_cnt\" & numcontratto)
            Dim newFile As String = HttpContext.Current.Server.MapPath("\allegati_pren_cnt\" & numcontratto & "\RA_" & numcontratto & ".pdf")

            'Verifica se file già presente se si salta la creazione del PDF 
            'e l'inserimento 

            'aggiornamento 10.03.2022 Lo crea ma non lo allega 
            'viene allegato dopo che la firma è andata a buon fine
            errline = 2
            If File.Exists(newFile) = False Then
                errline = 3
                Dim StampaDocumento As ClassGeneraHtmlToPdf = New ClassGeneraHtmlToPdf
                Dim pathContrattoPDF As String = "" 'StampaContratto.GeneraDocumentoPDF(numcontratto, nazione)         'restituisce stringa con path file da allegare
                errline = 4
                'pathContrattoPDF = ""   'inserito per non inserirlo negli allegati automaticamente ma solo dopo chè è stato firmato 10.03.2022 
                If pathContrattoPDF <> "" Then
                    'Lo inserisce negli allegati 08.02.2022
                    errline = 5
                    Dim id_operatore As String = HttpContext.Current.Request.Cookies("SicilyRentCar")("IdUtente")  'aggiornato salvo 15.02.2023
                    Dim tipo As String = "uscita"  'solo x test salvo
                    Dim insertAllegati As Boolean = insertContrattoPDFtoAllegati("/allegati_pren_cnt/" & numcontratto & "/", numcontratto, id_operatore, tipo)    'aggiornato salvo 15.02.2023

                    'Dim insertAllegati As Boolean = insertContrattoPDFtoAllegati("/allegati_pren_cnt/" & numcontratto & "/", numcontratto)
                    If insertAllegati = True Then
                        'RefreshListAllegatiContratti(sqlAllegati, dataListAllegati, statoContratto.Text)
                    End If
                    errline = 6
                End If

                ris = "OK-Contratto in PDF Generato"

            Else
                errline = 99
                'Libreria.genUserMsgBox(Page, "Il Contratto in PDF è già presente. \nPer ricrearlo eliminare quello presente negli allegati.")
                'PostFirmaInserita = False
                ris = "contratto già presente"

            End If

        Catch ex As Exception
            ris = "error: " & errline & "-" & ex.Message


        End Try

        Return ris




    End Function



    Public Shared Function insertContrattoPDFtoAllegati(ByVal pathAllegato As String, ByVal contratto_num As String, Optional ByVal operatore As String = "39", Optional ByVal tipo As String = "uscita") As Boolean
        Dim ris As Boolean = False
        Dim nome_file As String = "RA_" & contratto_num & ".pdf"
        Dim tipoAllegato As String = "12" 'contratto di uscita
        If tipo = "rientro" Then
            nome_file = "RB_" & contratto_num & ".pdf"
            tipoAllegato = "18" 'contratto di rientro aggiunto 16.05.2022 
        End If

        If operatore = "" Then
            operatore = 39
        End If

        Dim sqlstr As String
        Try

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            'verifica se già presente nel DB altrimenti lo allega
            Dim sqlver As String = "select nome_file from contratti_prenotazioni_allegati where nome_file='" & nome_file & "'"
            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlver, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()
            ris = Rs1.HasRows
            Rs1.Close()
            Cmd1.Dispose()
            Rs1 = Nothing
            Cmd1 = Nothing

            If ris = True Then
                Dbc.Close()
                Return ris
                Exit Function
            End If

            'Allegato non presente lo registra

            sqlstr = "INSERT INTO contratti_prenotazioni_allegati (nome_file, cartella, num_cnt, id_cnt_pren_allegati_tipo,nome_file_operatore, id_operatore) " &
                                                                     " VALUES ('" & nome_file & "','" & pathAllegato & "','" & contratto_num & "','" & tipoAllegato & "','contratto'," & operatore & ")"


            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim x As Integer = Cmd.ExecuteNonQuery()

            If x > 0 Then
                ris = True
            End If

            Cmd.Dispose() '
            Cmd = Nothing
            Dbc.Close()
            Dbc = Nothing

            'RefreshListAllegati()


        Catch ex As Exception
            ' Libreria.genUserMsgBox(Page, "Errore nell'inserimento dell'allegato. " & ex.Message)
            HttpContext.Current.Response.Write(sqlstr & " --- Errore nell'inserimento dell'allegato.:" & "<br/>" & ex.Message)
        End Try

        Return ris


    End Function


    Public Shared Function UpdateContrattoPDFtoAllegati(ByVal pathAllegato As String, ByVal nome_file As String, ByVal contratto_num As String) As Boolean

        Dim ris As Boolean = False

        Dim nome_file_ori As String = "RA_" & contratto_num & ".pdf"
        Dim tipoAllegato As String = "12" 'contratto di uscita


        Dim pAllegato As String = "/allegati_pren_cnt/" & contratto_num & "/"   'aggiornato 25.03.2022

        Dim sqlstr As String = "UPDATE contratti_prenotazioni_allegati set nome_file='" & nome_file & "', cartella='" & pAllegato & "' " &
                                                                 " WHERE num_cnt='" & contratto_num & "' and nome_file='" & nome_file_ori & "'"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim x As Integer = Cmd.ExecuteNonQuery()

            If x > 0 Then
                ris = True
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc = Nothing
        Catch ex As Exception
            'Libreria.genUserMsgBox(Page, "Errore nell'inserimento dell'allegato. " & ex.Message)
        End Try

        Return ris


    End Function

    Public Shared Function GetContrattoFirmato(ByVal numcontratto As String, ByVal iddocumento As String, Optional status As String = "2") As Boolean

        Dim ris As Boolean = False

        'verifica se presente firma 03.02.2022 e 09.02.2022
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Try


            Dim sqlStr As String = "SELECT num_contratto,firma_tablet FROM contratti WHERE num_contratto ='" & numcontratto & "' AND firma_tablet=1 AND attivo=1"

            If status = "8" Then
                sqlStr = "SELECT num_contratto,firma_tablet_rientro FROM contratti WHERE num_contratto ='" & numcontratto & "' AND firma_tablet_rientro=1 AND attivo=1"
            End If

            If iddocumento <> "" Then
                sqlStr = "SELECT num_contratto,firma_tablet FROM contratti WHERE [ID] ='" & iddocumento & "' AND firma_tablet=1 AND attivo=1"
                If status = "8" Then
                    sqlStr = "SELECT num_contratto,firma_tablet_rientro FROM contratti WHERE [ID] ='" & iddocumento & "' AND firma_tablet_rientro=1 AND attivo=1"
                End If
            End If



            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            ris = Rs1.HasRows

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
    Public Shared Sub AggiornaInviaMailContratto(idcontratto As String, status As String, statocontratto As String)
        'spostata e modificata da contratti.aspx.vb 25.02.2022
        If idcontratto = "" Then
            Exit Sub
        End If

        If status = "False" Then
            status = "0"
        End If
        If status = "True" Then
            status = "1"
        End If
        If status = "" Then
            status = "0"
        End If


        Dim sqlStr As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            sqlStr = "update contratti set invia_mail_contratto='" & status & "' WHERE id='" & idcontratto & "'"

            If statocontratto = "8" Then
                sqlStr = "update contratti set invia_mail_contratto_rientro='" & status & "' WHERE id='" & idcontratto & "'"
            End If


            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim x As Integer = Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception

        End Try




    End Sub




    Public Shared Function getStazioneVal(ByVal idstazione As String, Optional ByVal tipoVal As String = "") As String

        'RESTITUISCE se stazione è in VAL IN o OUT

        Dim ris As String = "0#0"
        Dim sqlstr As String = "Select val_da_altre_stazioni,val_verso_altre_stazioni from stazioni where [id]='" & idstazione & "'"

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Try

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows Then
                Rs.Read()

                ris = Rs("val_verso_altre_stazioni") & "#" & Rs("val_da_altre_stazioni")

            End If

            Rs.Close()
            Rs = Nothing

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception

        End Try

        Return ris



    End Function




    Public Shared Sub SetDepostitoCauzionale(iddoc As String, lst As DataList, idgruppo As String, numcalcolo As String)


    End Sub


    Public Shared Function GetNomeStazioneNoCode(ByVal nomestazione As String) As String
        Dim ris As String = ""
        If nomestazione <> "" Then
            ris = Mid(nomestazione, 4)
        End If

        Return ris


    End Function

    Public Shared Function GetEstensioneFile(ByVal nomefile As String) As String

        Dim ris As String = nomefile

        ris = StrReverse(nomefile)
        Dim x As Integer = ris.IndexOf(".")
        ris = Mid(ris, 1, x)
        ris = StrReverse(ris)
        Return ris


    End Function



    Public Shared Function getSiglaAllegato(ByVal id_tipo_allegato As String) As String

        'RESTITUISCE la sigla dell'allegato

        Dim ris As String = ""
        Dim sqlstr As String = ""

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Try

            sqlstr = "select sigla from contratti_prenotazioni_allegati_tipo where id_cnt_pren_allegati_tipo='" & id_tipo_allegato & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            ris = Cmd.ExecuteScalar


            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception

        End Try

        Return ris



    End Function



    Public Shared Function GetValoreDepositoCauzionaleDefault(ByVal idgruppo As String) As String

        Dim ris As String = "0"

        If idgruppo = "" Then
            Return ris
            Exit Function
        End If

        Dim sqlstr As String = "SELECT condizioni_righe.costo FROM condizioni_righe INNER JOIN "
        sqlstr += "condizioni_x_gruppi ON condizioni_righe.id = condizioni_x_gruppi.id_condizione "
        sqlstr += "where id_elemento = '283' and id_gruppo = '" & idgruppo & "'"


        Try
            'RESTITUISCE il valore di default del deposito cauzionale di quel gruppo
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            ris = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            ris = 0
        End Try

        Return ris



    End Function



    Public Shared Function GetOpzioneSelezionata(tb As String, id_documento As String, id_elemento As String) As Boolean

        'passato il documento e l'elemento verifica se è prepagata 28.12.2021
        Dim ris As Boolean = False

        Dim sqla As String = "SELECT selezionato FROM " & tb & " WHERE [id_documento]='" & id_documento & "' and id_elemento='" & id_elemento & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows Then
                Rs.Read()
                If Rs("selezionato") = 0 Then
                    ris = False
                Else
                    ris = True
                End If
            Else
                ris = False
            End If

            Rs.Close()
            Rs = Nothing

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception
            HttpContext.Current.Response.Write("error getOpzioneSelezionata: <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

        Return ris


    End Function

    Public Shared Function GetOpzionePrepagata(tb As String, id_documento As String, id_elemento As String) As Boolean

        'passato il documento e l'elemento verifica se è prepagata 28.12.2021
        Dim ris As Boolean = False

        Dim sqla As String = "SELECT prepagato FROM " & tb & " WHERE [id_documento]='" & id_documento & "' and id_elemento='" & id_elemento & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows Then
                Rs.Read()
                If Rs("prepagato") = 0 Then
                    ris = False
                Else
                    ris = True
                End If
            Else
                ris = False
            End If

            Rs.Close()
            Rs = Nothing

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception
            HttpContext.Current.Response.Write("error getOpzionePrepagata : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

        Return ris


    End Function

    Public Shared Function GetResponse(lst As DataList) As String
        'restituisce le stringhe dei ck abilitati
        'inserita il 20.12.2021
        Dim ris As String = ""

        Dim check_attuale As CheckBox
        Dim check_old As CheckBox
        Dim id_elemento As Label
        Dim tResponse As String = ""
        Dim tResponseOLD As String = ""

        For i = 0 To lst.Items.Count - 1
            check_attuale = lst.Items(i).FindControl("chkScegli")
            check_old = lst.Items(i).FindControl("chkOldScegli")
            id_elemento = lst.Items(i).FindControl("id_elemento")

            If check_attuale.Checked = True Then
                tResponse += id_elemento.Text & ","
            End If


            If check_old.Checked = True Then
                tResponseOLD += id_elemento.Text & "," 'creo stringa con i ck OLD
            End If

        Next

        ris = tResponse & "#" & tResponseOLD


        Return ris


    End Function

    Public Shared Function VerificaOpzione(lst As DataList, id_ele As String, tipo As String) As Boolean
        'tipo = ck:checked
        'tipo = en:enabled
        'tipo = pre:prepagato
        'tipo = vuoto:enabled

        'inserita il 09.12.2021
        Dim ris As Boolean = False

        Dim check_attuale As CheckBox
        Dim check_old As CheckBox
        Dim id_elemento As Label
        Dim prepagata As Label      '30.12.2021

        For i = 0 To lst.Items.Count - 1
            check_attuale = lst.Items(i).FindControl("chkScegli")
            check_old = lst.Items(i).FindControl("chkOldScegli")
            id_elemento = lst.Items(i).FindControl("id_elemento")
            prepagata = lst.Items(i).FindControl("prepagato")

            If id_elemento.Text = id_ele Then
                If tipo = "ck" Then
                    ris = check_attuale.Checked
                ElseIf tipo = "pre" Then
                    If prepagata.Text = "True" Then
                        ris = True
                    Else
                        ris = False
                    End If
                ElseIf tipo = "en" Then
                    ris = check_attuale.Enabled
                Else
                    ris = check_attuale.Enabled
                End If
                Exit For
            End If

        Next

        Return ris

    End Function


    Public Shared Sub SetOpzione(lst As DataList, id_ele As String, Ck_checked As Boolean, Ck_enabled As Boolean, dataON As Boolean)
        'inserita il 09.12.2021
        'dataOn = True : il valore esiste e viene ignorato Checked
        'dataOn = False : il valore non esiste ed è possibile attivare il CK

        Dim check_attuale As CheckBox
        Dim check_old As CheckBox
        Dim id_elemento As Label

        Dim ris As Boolean = False

        For i = 0 To lst.Items.Count - 1
            check_attuale = lst.Items(i).FindControl("chkScegli")
            check_old = lst.Items(i).FindControl("chkOldScegli")
            id_elemento = lst.Items(i).FindControl("id_elemento")

            If id_elemento.Text = id_ele Then
                If dataON = False Then          'dataON=false è possibile mettere il cecked a True 
                    check_attuale.Checked = Ck_checked
                End If

                check_attuale.Enabled = Ck_enabled
                Exit For
            End If

        Next

    End Sub

    Public Shared Function GetDataSql(ByVal data As String, ByVal tipotime As Integer) As String

        If tipotime = 59 Then
            GetDataSql = Right(data, 4) & "-" & Mid(data, 4, 2) & "-" & Left(data, 2) & " 23:59:59"
        ElseIf tipotime = 0 Then
            GetDataSql = Right(data, 4) & "-" & Mid(data, 4, 2) & "-" & Left(data, 2) & " 00:00:00"
        ElseIf tipotime = 99 Then   'senza ora
            GetDataSql = Right(data, 4) & "-" & Mid(data, 4, 2) & "-" & Left(data, 2)
        Else
            GetDataSql = Right(data, 4) & "-" & Mid(data, 4, 2) & "-" & Left(data, 2) & " 00:00:00"     'aggiornato 19.01.2021
        End If

    End Function
    Public Shared Function GetDataSqlCnv(ByVal data As String, ByVal tipotime As Integer) As String

        Dim strdata As String
        If tipotime = 59 Then
            strdata = Right(data, 4) & "-" & Mid(data, 4, 2) & "-" & Left(data, 2) & " 23:59:59"
        ElseIf tipotime = 0 Then
            strdata = Right(data, 4) & "-" & Mid(data, 4, 2) & "-" & Left(data, 2) & " 00:00:00"
        ElseIf tipotime = 99 Then   'senza ora
            strdata = Right(data, 4) & "-" & Mid(data, 4, 2) & "-" & Left(data, 2)
        Else
            strdata = Right(data, 4) & "-" & Mid(data, 4, 2) & "-" & Left(data, 2) & " 00:00:00"        'aggiornato 19.01.2021
        End If

        GetDataSqlCnv = "CONVERT(DATETIME,'" & strdata & "',102)"

    End Function
    Public Shared Function sql_inj(ByVal stringa As String) As Boolean
        If LCase(stringa).Contains("select ") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains("insert ") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains("create ") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains("drop ") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains(" drop ") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains("from") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains("delete ") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains("where ") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains(" or ") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains(" and ") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains("like ") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains("exec ") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains("sp_") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains("xp_") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains("sql") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains("open") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains("begin") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains(" end ") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains("declare") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains("sp_") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains("convert ") Then
            sql_inj = True
        ElseIf LCase(stringa).Contains(" convert ") Then
            sql_inj = True
        Else
            sql_inj = False
        End If
    End Function

    Public Shared Function GeneraMd5(ByVal input As String) As String
        Dim md5Hasher As New MD5CryptoServiceProvider()
        Dim data As Byte() = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input))
        Dim sBuilder As New StringBuilder()
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i
        Dim ByteStringa = Encoding.UTF8.GetBytes(sBuilder.ToString)
        GeneraMd5 = Convert.ToBase64String(ByteStringa)
    End Function

    Public Shared Function id_cliente_cash() As String
        Dim sqla As String = "SELECT ISNULL(id_ditta,0) FROM ditte WHERE [codice edp]='9999'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            id_cliente_cash = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni id_cliente_cash  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function

    Protected Shared Function get_codice_erariale(ByVal id_comune As String, ByVal id_nazione As String) As String

        Dim sqlStr As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            If id_comune <> "" Then
                sqlStr = "SELECT isnull(codice,'') FROM comuni_ares WITH(NOLOCK) WHERE id='" & id_comune & "'"
            ElseIf id_nazione <> "" Then
                sqlStr = "SELECT isnull(codErariale,'') FROM nazioni WITH(NOLOCK) WHERE id_nazione='" & id_nazione & "'"
            End If

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            get_codice_erariale = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni get_codice_erariale  : <br/>" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Public Shared Function get_comune_da_codice(ByVal id_comune As String) As String
        'inserito 18.03.2022

        Dim sqlStr As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            sqlStr = "SELECT comune FROM comuni_ares WITH(NOLOCK) WHERE id='" & id_comune & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            get_comune_da_codice = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni get_comune_da_codice  : <br/>" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function



    Protected Shared Sub Estrazione_Cognome(ByRef strCognome As String, ByRef strConsonanti As String, ByRef strVocali As String)
        strCognome = UCase(strCognome) 'trasformo tutti i caratteri in MAIUSCOLI
        strCognome = Replace(strCognome, " ", "") 'elimino eventuali spazi vuoti
        strConsonanti = ""
        strVocali = ""

        For i = 1 To Len(strCognome) 'inizio un ciclo che partedal primo carattere(i=1) e prosegue per tutta la lunghezza (LEN) della variabile strCognome
            If InStr("AEIOU", Mid(strCognome, i, 1)) Then 'Se il carattere è una vocale allora la prendo
                strVocali = strVocali + Mid(strCognome, i, 1) 'e la memorizzo (il ciclo si ripete)
            ElseIf InStr("BCDFGHJKLMNPQRSTVWXYZ", Mid(strCognome, i, 1)) Then 'se il carattere è una consonante allora
                strConsonanti = strConsonanti + Mid(strCognome, i, 1) 'la memorizzo (il ciclo si ripete)
            End If
            If (Len(strConsonanti) = 3) Then ' alla fine del ciclo, se la variabile sConsonanti ha almeno 3 caratteri allora
                Exit For 'esco dal ciclo
            End If
        Next i 'altrimenti proseguo il ciclo

        Select Case Len(strConsonanti)
            Case Is = 2
                strConsonanti = strConsonanti
                If (Len(strVocali) >= 1) Then
                    strConsonanti = strConsonanti + Mid(strVocali, 1, 1)
                End If
                If (Len(strVocali) = 0) Then
                    strConsonanti = strConsonanti + "X"
                End If
            Case Is = 1
                strConsonanti = strConsonanti
                If (Len(strVocali) >= 2) Then
                    strConsonanti = strConsonanti + Mid(strVocali, 1, 2)
                End If
                If (Len(strVocali) = 1) Then
                    strConsonanti = strConsonanti + Mid(strVocali, 1, 1) + "X"
                End If
                If (Len(strVocali) = 0) Then
                    strConsonanti = strConsonanti + "X" + "X"
                End If
            Case Is = 0
                strConsonanti = strConsonanti + Mid(strVocali, 1, 3)
                If (Len(strVocali) >= 3) Then
                    strConsonanti = Mid(strVocali, 1, 3)
                End If
                If (Len(strVocali) = 2) Then
                    strConsonanti = strVocali + "X"
                End If
                If (Len(strVocali) = 1) Then
                    strConsonanti = strVocali + "X" + "X"
                End If
                If (Len(strVocali) = 0) Then
                    strConsonanti = "X" + "X" + "X"
                End If
        End Select

        strCognome = strConsonanti
    End Sub

    Protected Shared Sub Estrazione_Nome(ByRef strNome As String, ByRef strConsonanti_Nome As String, ByRef strVocali_Nome As String)
        strNome = UCase(strNome) 'trasformo tutti i caratteri in MAIUSCOLI
        strNome = Replace(strNome, " ", "") 'elimino eventuali spazi vuoti
        strConsonanti_Nome = ""
        strVocali_Nome = ""

        For i = 1 To Len(strNome) 'inizio un ciclo che partedal primo carattere(i=1) e prosegue per tutta la lunghezza (LEN) della variabile strCognome
            If InStr("AEIOU", Mid(strNome, i, 1)) Then 'Se il carattere è una vocale allora la prendo
                strVocali_Nome = strVocali_Nome + Mid(strNome, i, 1) 'e la memorizzo (il ciclo si ripete)
            ElseIf InStr("BCDFGHJKLMNPQRSTVWXYZ", Mid(strNome, i, 1)) Then 'se il carattere è una consonante allora
                strConsonanti_Nome = strConsonanti_Nome + Mid(strNome, i, 1) 'la memorizzo (il ciclo si ripete)
            End If
        Next i 'proseguo il ciclo

        If (Len(strConsonanti_Nome) >= 4) Then ' alla fine del ciclo, se la variabile sConsonanti ha almeno 3 caratteri allora (si prendono la prima la terza e la quarta)
            strConsonanti_Nome = Mid(strConsonanti_Nome, 1, 1) & Mid(strConsonanti_Nome, 3, 2)
        End If

        Select Case Len(strConsonanti_Nome)
            Case Is = 3
                strConsonanti_Nome = Mid(strConsonanti_Nome, 1, 3)
            Case Is = 2
                strConsonanti_Nome = Mid(strConsonanti_Nome, 1, 2)
                If (Len(strVocali_Nome) >= 1) Then
                    strConsonanti_Nome = Mid(strConsonanti_Nome, 1, 2) + Mid(strVocali_Nome, 1, 1)
                End If
                If (Len(strVocali_Nome) = 0) Then
                    strConsonanti_Nome = Mid(strConsonanti_Nome, 1, 2) + "X"
                End If
            Case Is = 1
                strConsonanti_Nome = strConsonanti_Nome
                If (Len(strVocali_Nome) >= 2) Then
                    strConsonanti_Nome = strConsonanti_Nome + Mid(strVocali_Nome, 1, 2)
                End If
                If (Len(strVocali_Nome) = 1) Then
                    strConsonanti_Nome = strConsonanti_Nome + strVocali_Nome + "X"
                End If
                If (Len(strVocali_Nome) = 0) Then
                    strConsonanti_Nome = strConsonanti_Nome + "X" + "X"
                End If
            Case Is = 0
                If (Len(strVocali_Nome) >= 3) Then
                    strConsonanti_Nome = strConsonanti_Nome + Mid(strVocali_Nome, 1, 3)
                End If
                If (Len(strVocali_Nome) = 2) Then
                    strConsonanti_Nome = Mid(strVocali_Nome, 1, 2) + "X"
                End If
                If (Len(strVocali_Nome) = 1) Then
                    strConsonanti_Nome = strVocali_Nome + "X" + "X"
                End If
                If (Len(strVocali_Nome) = 0) Then
                    strConsonanti_Nome = "X" + "X" + "X"
                End If
        End Select

        strNome = strConsonanti_Nome
    End Sub

    Public Shared Function genera_codice_fiscale(ByVal strCognome As String, ByVal strNome As String, ByVal anno As String, ByVal mese As Integer, ByVal giorno As Integer, ByVal sesso As Char, ByVal id_comune As String, ByVal id_nazione As String) As String
        Dim strConsonanti As String
        Dim strConsonanti_Nome As String
        Dim strVocali_Nome As String
        Dim strVocali As String
        Dim strAnno As String
        Dim strMese As String
        Dim Mese_Estratto As String
        Dim strGiorno As String
        Dim strCodice_Senza_Controllo As String

        'Dim , strNome, strConsonanti_Nome, , , , , , , strCodice_Controllo As String

        Estrazione_Cognome(strCognome, strConsonanti, strVocali)
        strCognome = strConsonanti

        'HttpContext.Current.Trace.Write("COGNOME :" & strCognome)

        Call Estrazione_Nome(strNome, strConsonanti_Nome, strVocali_Nome)

        'HttpContext.Current.Trace.Write("NOME :" & strConsonanti_Nome)

        strNome = strConsonanti_Nome

        strAnno = Mid(anno, 3, 4)
        strMese = mese

        Select Case strMese
            Case 1
                Mese_Estratto = "A"
            Case 2
                Mese_Estratto = "B"
            Case 3
                Mese_Estratto = "C"
            Case 4
                Mese_Estratto = "D"
            Case 5
                Mese_Estratto = "E"
            Case 6
                Mese_Estratto = "H"
            Case 7
                Mese_Estratto = "L"
            Case 8
                Mese_Estratto = "M"
            Case 9
                Mese_Estratto = "P"
            Case 10
                Mese_Estratto = "R"
            Case 11
                Mese_Estratto = "S"
            Case 12
                Mese_Estratto = "T"
        End Select

        strGiorno = giorno
        Dim giorno_estratto As String = ""

        Select Case strGiorno
            Case Is = 1
                If sesso = "F" Then
                    giorno_estratto = (strGiorno + 40)
                ElseIf sesso = "M" Then
                    giorno_estratto = "0" & strGiorno
                End If
            Case Is = 2
                If sesso = "F" Then
                    giorno_estratto = (strGiorno + 40)
                ElseIf sesso = "M" Then
                    giorno_estratto = "0" & strGiorno
                End If
            Case Is = 3
                If sesso = "F" Then
                    giorno_estratto = (strGiorno + 40)
                ElseIf sesso = "M" Then
                    giorno_estratto = "0" & strGiorno
                End If
            Case Is = 4
                If sesso = "F" Then
                    giorno_estratto = (strGiorno + 40)
                ElseIf sesso = "M" Then
                    giorno_estratto = "0" & strGiorno
                End If
            Case Is = 5
                If sesso = "F" Then
                    giorno_estratto = (strGiorno + 40)
                ElseIf sesso = "M" Then
                    giorno_estratto = "0" & strGiorno
                End If
            Case Is = 6
                If sesso = "F" Then
                    giorno_estratto = (strGiorno + 40)
                ElseIf sesso = "M" Then
                    giorno_estratto = "0" & strGiorno
                End If
            Case Is = 7
                If sesso = "F" Then
                    giorno_estratto = (strGiorno + 40)
                ElseIf sesso = "M" Then
                    giorno_estratto = "0" & strGiorno
                End If
            Case Is = 8
                If sesso = "F" Then
                    giorno_estratto = (strGiorno + 40)
                ElseIf sesso = "M" Then
                    giorno_estratto = "0" & strGiorno
                End If
            Case Is = 9
                If sesso = "F" Then
                    giorno_estratto = (strGiorno + 40)
                ElseIf sesso = "M" Then
                    giorno_estratto = "0" & strGiorno
                End If
            Case Is >= 10
                If sesso = "F" Then
                    giorno_estratto = (strGiorno + 40)
                ElseIf sesso = "M" Then
                    giorno_estratto = strGiorno
                End If
        End Select

        strCodice_Senza_Controllo = strCognome + strNome + strAnno + Mese_Estratto + giorno_estratto + get_codice_erariale(id_comune, id_nazione)

        'Controllo caratteri in posizione pari

        Dim strPari As String = ""

        For i = 2 To 14
            strPari = strPari & Mid(strCodice_Senza_Controllo, i, 1)
            i = i + 1
        Next

        Dim strControllo_Pari
        Dim strNumeri_Pari As String = 0

        For i = 1 To 8
            strControllo_Pari = Mid(strPari, i, 1)
            Select Case strControllo_Pari
                Case "0"
                    strNumeri_Pari = strNumeri_Pari + 0
                Case "1"
                    strNumeri_Pari = strNumeri_Pari + 1
                Case "2"
                    strNumeri_Pari = strNumeri_Pari + 2
                Case "3"
                    strNumeri_Pari = strNumeri_Pari + 3
                Case "4"
                    strNumeri_Pari = strNumeri_Pari + 4
                Case "5"
                    strNumeri_Pari = strNumeri_Pari + 5
                Case "6"
                    strNumeri_Pari = strNumeri_Pari + 6
                Case "7"
                    strNumeri_Pari = strNumeri_Pari + 7
                Case "8"
                    strNumeri_Pari = strNumeri_Pari + 8
                Case "9"
                    strNumeri_Pari = strNumeri_Pari + 9
                Case "A"
                    strNumeri_Pari = strNumeri_Pari + 0
                Case "B"
                    strNumeri_Pari = strNumeri_Pari + 1
                Case "C"
                    strNumeri_Pari = strNumeri_Pari + 2
                Case "D"
                    strNumeri_Pari = strNumeri_Pari + 3
                Case "E"
                    strNumeri_Pari = strNumeri_Pari + 4
                Case "F"
                    strNumeri_Pari = strNumeri_Pari + 5
                Case "G"
                    strNumeri_Pari = strNumeri_Pari + 6
                Case "H"
                    strNumeri_Pari = strNumeri_Pari + 7
                Case "I"
                    strNumeri_Pari = strNumeri_Pari + 8
                Case "J"
                    strNumeri_Pari = strNumeri_Pari + 9
                Case "K"
                    strNumeri_Pari = strNumeri_Pari + 10
                Case "L"
                    strNumeri_Pari = strNumeri_Pari + 11
                Case "M"
                    strNumeri_Pari = strNumeri_Pari + 12
                Case "N"
                    strNumeri_Pari = strNumeri_Pari + 13
                Case "O"
                    strNumeri_Pari = strNumeri_Pari + 14
                Case "P"
                    strNumeri_Pari = strNumeri_Pari + 15
                Case "Q"
                    strNumeri_Pari = strNumeri_Pari + 16
                Case "R"
                    strNumeri_Pari = strNumeri_Pari + 17
                Case "S"
                    strNumeri_Pari = strNumeri_Pari + 18
                Case "T"
                    strNumeri_Pari = strNumeri_Pari + 19
                Case "U"
                    strNumeri_Pari = strNumeri_Pari + 20
                Case "V"
                    strNumeri_Pari = strNumeri_Pari + 21
                Case "W"
                    strNumeri_Pari = strNumeri_Pari + 22
                Case "X"
                    strNumeri_Pari = strNumeri_Pari + 23
                Case "Y"
                    strNumeri_Pari = strNumeri_Pari + 24
                Case "Z"
                    strNumeri_Pari = strNumeri_Pari + 25
            End Select
        Next

        'Controllo caratteri in posizione dispari

        Dim strDispari As String = ""

        For i = 1 To 15
            strDispari = strDispari & Mid(strCodice_Senza_Controllo, i, 1)
            i = i + 1
        Next

        Dim strControllo_Dispari
        Dim strNumeri_Dispari As String = 0

        For i = 1 To 8
            strControllo_Dispari = Mid(strDispari, i, 1)
            Select Case strControllo_Dispari
                Case "0"
                    strNumeri_Dispari = strNumeri_Dispari + 1
                Case "1"
                    strNumeri_Dispari = strNumeri_Dispari + 0
                Case "2"
                    strNumeri_Dispari = strNumeri_Dispari + 5
                Case "3"
                    strNumeri_Dispari = strNumeri_Dispari + 7
                Case "4"
                    strNumeri_Dispari = strNumeri_Dispari + 9
                Case "5"
                    strNumeri_Dispari = strNumeri_Dispari + 13
                Case "6"
                    strNumeri_Dispari = strNumeri_Dispari + 15
                Case "7"
                    strNumeri_Dispari = strNumeri_Dispari + 17
                Case "8"
                    strNumeri_Dispari = strNumeri_Dispari + 19
                Case "9"
                    strNumeri_Dispari = strNumeri_Dispari + 21
                Case "A"
                    strNumeri_Dispari = strNumeri_Dispari + 1
                Case "B"
                    strNumeri_Dispari = strNumeri_Dispari + 0
                Case "C"
                    strNumeri_Dispari = strNumeri_Dispari + 5
                Case "D"
                    strNumeri_Dispari = strNumeri_Dispari + 7
                Case "E"
                    strNumeri_Dispari = strNumeri_Dispari + 9
                Case "F"
                    strNumeri_Dispari = strNumeri_Dispari + 13
                Case "G"
                    strNumeri_Dispari = strNumeri_Dispari + 15
                Case "H"
                    strNumeri_Dispari = strNumeri_Dispari + 17
                Case "I"
                    strNumeri_Dispari = strNumeri_Dispari + 19
                Case "J"
                    strNumeri_Dispari = strNumeri_Dispari + 21
                Case "K"
                    strNumeri_Dispari = strNumeri_Dispari + 2
                Case "L"
                    strNumeri_Dispari = strNumeri_Dispari + 4
                Case "M"
                    strNumeri_Dispari = strNumeri_Dispari + 18
                Case "N"
                    strNumeri_Dispari = strNumeri_Dispari + 20
                Case "O"
                    strNumeri_Dispari = strNumeri_Dispari + 11
                Case "P"
                    strNumeri_Dispari = strNumeri_Dispari + 3
                Case "Q"
                    strNumeri_Dispari = strNumeri_Dispari + 6
                Case "R"
                    strNumeri_Dispari = strNumeri_Dispari + 8
                Case "S"
                    strNumeri_Dispari = strNumeri_Dispari + 12
                Case "T"
                    strNumeri_Dispari = strNumeri_Dispari + 14
                Case "U"
                    strNumeri_Dispari = strNumeri_Dispari + 16
                Case "V"
                    strNumeri_Dispari = strNumeri_Dispari + 10
                Case "W"
                    strNumeri_Dispari = strNumeri_Dispari + 22
                Case "X"
                    strNumeri_Dispari = strNumeri_Dispari + 25
                Case "Y"
                    strNumeri_Dispari = strNumeri_Dispari + 24
                Case "Z"
                    strNumeri_Dispari = strNumeri_Dispari + 23
            End Select
        Next

        Dim Totale As String
        Totale = (CInt(strNumeri_Pari) + CInt(strNumeri_Dispari)) Mod 26 'CInt trasfroma la variabile in numero INTERO per la somma

        Dim Codice_Controllo As String = ""

        Select Case Totale
            Case "0"
                Codice_Controllo = "A"
            Case "1"
                Codice_Controllo = "B"
            Case "2"
                Codice_Controllo = "C"
            Case "3"
                Codice_Controllo = "D"
            Case "4"
                Codice_Controllo = "E"
            Case "5"
                Codice_Controllo = "F"
            Case "6"
                Codice_Controllo = "G"
            Case "7"
                Codice_Controllo = "H"
            Case "8"
                Codice_Controllo = "I"
            Case "9"
                Codice_Controllo = "J"
            Case "10"
                Codice_Controllo = "K"
            Case "11"
                Codice_Controllo = "L"
            Case "12"
                Codice_Controllo = "M"
            Case "13"
                Codice_Controllo = "N"
            Case "14"
                Codice_Controllo = "O"
            Case "15"
                Codice_Controllo = "P"
            Case "16"
                Codice_Controllo = "Q"
            Case "17"
                Codice_Controllo = "R"
            Case "18"
                Codice_Controllo = "S"
            Case "19"
                Codice_Controllo = "T"
            Case "20"
                Codice_Controllo = "U"
            Case "21"
                Codice_Controllo = "V"
            Case "22"
                Codice_Controllo = "W"
            Case "23"
                Codice_Controllo = "X"
            Case "24"
                Codice_Controllo = "Y"
            Case "25"
                Codice_Controllo = "Z"
        End Select

        genera_codice_fiscale = strCodice_Senza_Controllo + Codice_Controllo
    End Function

    Public Shared Function CompareListItems(ByVal li1 As ListItem, ByVal li2 As ListItem) As Integer
        Return [String].Compare(li1.Text, li2.Text)
    End Function

    Public Shared Sub SortListBox(ByRef lista As ListBox)


        Dim t As New List(Of ListItem)()
        Dim compare As New Comparison(Of ListItem)(AddressOf CompareListItems)
        For Each lbItem As ListItem In lista.Items
            t.Add(lbItem)
        Next


        t.Sort(compare)
        lista.Items.Clear()
        lista.Items.AddRange(t.ToArray())
    End Sub


    'FUNZIONI COMUNI - SITUAZIONE STAZIONI -------------------------------------------------------------------------------------------------

    'VERSIONE VECCHIA - USATO NEI TRASFERIMENTI - NON COMMENTARE
    Public Shared Function getSituazione_x_gruppi(ByVal dataIniziale As DateTime, ByVal stazione As String, Optional ByVal gruppi_da_considerare() As String = Nothing) As Integer()

        Dim sqlStr As String = ""
        Dim situazione(900) As Integer
        Dim i As Integer
        Dim gruppi(60) As String

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Dbc1 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc1.Open()
            Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc2.Open()


            Dim data1 As String = funzioni_comuni.getDataDb_senza_orario2(dataIniziale, HttpContext.Current.Request.ServerVariables("HTTP_HOST"))



            'PER PRIMA COSA SELEZIONO I GRUPPI SE NON E' STATO PASSATO UN ELENCO GRUPPI DA CONSIDERARE ---------------------------------------
            sqlStr = "SELECT cod_gruppo FROM gruppi WITH(NOLOCK) WHERE attivo='1' ORDER BY cod_gruppo"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader



            If gruppi_da_considerare Is Nothing Then
                Rs = Cmd.ExecuteReader()

                i = 0

                Do While Rs.Read()
                    gruppi(i) = Rs("cod_gruppo")
                    i = i + 1
                Loop
                gruppi(i) = "000"

                Dbc.Close()
                Dbc.Open()
            Else
                Dim z As Integer = 0
                i = 0
                Do While gruppi_da_considerare(z) <> "000"
                    gruppi(i) = gruppi_da_considerare(z)

                    i = i + 1
                    z = z + 1
                Loop

                gruppi(i) = "000"
            End If
            '----------------------------------------------------------------------------------------------------------------------------------
            'ORA SELEZIONO PER TUTTI I GRUPPI:

            '1)PREVISTE USCITE DA PRENOTAZIONI-------------------------------------------------------------------------------------------------
            sqlStr = "SELECT ore_uscita, gruppi.cod_gruppo As gruppo FROM prenotazioni WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON ISNULL(prenotazioni.ID_GRUPPO_APP,prenotazioni.ID_GRUPPO)=gruppi.id_gruppo WHERE YEAR(PRDATA_OUT)=YEAR('" & data1 & "') AND MONTH(PRDATA_OUT)=MONTH('" & data1 & "') AND DAY(PRDATA_OUT)=DAY('" & data1 & "') AND PRID_stazione_out='" & stazione & "' AND status='0' AND attiva='1' ORDER BY gruppi.cod_gruppo"

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Rs = Cmd.ExecuteReader()

            '2)RIENTRI DA PRENOTAZIONE (presunti)----------------------------------------------------------------------------------------------

            sqlStr = "SELECT ore_rientro, gruppi.cod_gruppo As gruppo FROM prenotazioni WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON ISNULL(prenotazioni.ID_GRUPPO_APP,prenotazioni.ID_GRUPPO)=gruppi.id_gruppo WHERE YEAR(PRDATA_PR)=YEAR('" & data1 & "') AND MONTH(PRDATA_PR)=MONTH('" & data1 & "') AND DAY(PRDATA_PR)=DAY('" & data1 & "') AND PRID_stazione_pr='" & stazione & "' AND status='0' AND attiva='1' ORDER BY gruppi.cod_gruppo"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc1)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            '3)RIENTRI DA CONTRATTO (previsti)-------------------------------------------------------------------------------------------------

            sqlStr = "SELECT data_presunto_rientro, gruppi.cod_gruppo As gruppo FROM contratti WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON ISNULL(contratti.id_gruppo_app,contratti.id_gruppo_auto)=gruppi.id_gruppo WHERE YEAR(data_presunto_rientro)=YEAR('" & data1 & "') AND MONTH(data_presunto_rientro)=MONTH('" & data1 & "') AND DAY(data_presunto_rientro)=DAY('" & data1 & "') AND id_stazione_presunto_rientro='" & stazione & "' AND status='2' AND contratti.attivo='1' ORDER BY gruppi.cod_gruppo"

            'HttpContext.Current.Trace.Write("SQL: " & sqlStr)

            Dim Cmd2 As New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
            Dim Rs2 As Data.SqlClient.SqlDataReader
            Rs2 = Cmd2.ExecuteReader()

            'LEGGO PER LA PRIMA VOLTA I RESULT SET E MEMORIZZO IN VARIABILI SE VI SONO RIGHE O MENO-------------------------------------------
            i = 0
            Dim k As Integer = 1
            Dim gruppo_corrente As Boolean

            Dim previste_uscite As Boolean
            Dim rientri_prenotazioni As Boolean
            Dim rientri_contratti As Boolean

            If Rs.Read() Then
                previste_uscite = True
            Else
                previste_uscite = False
            End If

            If Rs1.Read() Then
                rientri_prenotazioni = True
            Else
                rientri_prenotazioni = False
            End If

            If Rs2.Read() Then
                rientri_contratti = True
            Else
                rientri_contratti = False
            End If

            'PER OGNI GRUPPO CREO 15 ELEMENTI NELL'ARRAY DI OUTPUT-----------------------------------------------------------------------
            'CORRISPONDENTE ALLE 15 CELLE DELLA TABELLA DOVE VIENE VISUALIZZATO IL RISULTATO---------------------------------------------
            Do While gruppi(i) <> "000"

                situazione(k) = 0       'PREVISTE USCITE DA PRENOTAZIONI 00/12
                situazione(k + 1) = 0   'PREVISTE USCITE DA PRENOTAZIONI 12/16
                situazione(k + 2) = 0   'PREVISTE USCITE DA PRENOTAZIONI 16/20
                situazione(k + 3) = 0   'PREVISTE USCITE DA PRENOTAZIONI 20/24
                situazione(k + 4) = 0   'PREVISTE USCITE DA PRENOTAZIONI 00/24

                situazione(k + 5) = 0   'RIENTRI DA PRENOTAZIONE 00/12
                situazione(k + 6) = 0   'RIENTRI DA PRENOTAZIONE 12/16
                situazione(k + 7) = 0   'RIENTRI DA PRENOTAZIONE 16/20
                situazione(k + 8) = 0   'RIENTRI DA PRENOTAZIONE 20/24
                situazione(k + 9) = 0   'RIENTRI DA PRENOTAZIONE 00/24

                situazione(k + 10) = 0  'RIENTRI DA CONTRATTO 00/12
                situazione(k + 11) = 0  'RIENTRI DA CONTRATTO 12/16
                situazione(k + 12) = 0  'RIENTRI DA CONTRATTO 16/20
                situazione(k + 13) = 0  'RIENTRI DA CONTRATTO 20/24
                situazione(k + 14) = 0  'RIENTRI DA CONTRATTO 00/24


                'AGGIORNO I VALORI
                'PREVISTE USCITE DA PRENOTAZIONI----------------------------------------------------------------------------------------

                gruppo_corrente = True

                Do While (previste_uscite And gruppo_corrente)
                    If Rs("gruppo") = gruppi(i) Then
                        'SE IL GRUPPO CORRISPONDE A QUELLO ATTUALE

                        If CInt(Rs("ore_uscita")) < 12 Then
                            situazione(k) = situazione(k) + 1
                        ElseIf CInt(Rs("ore_uscita")) >= 12 And CInt(Rs("ore_uscita")) < 16 Then
                            situazione(k + 1) = situazione(k + 1) + 1
                        ElseIf CInt(Rs("ore_uscita")) >= 16 And CInt(Rs("ore_uscita")) < 20 Then
                            situazione(k + 2) = situazione(k + 2) + 1
                        ElseIf CInt(Rs("ore_uscita")) >= 20 And CInt(Rs("ore_uscita")) <= 23 Then
                            situazione(k + 3) = situazione(k + 3) + 1
                        End If

                        situazione(k + 4) = situazione(k + 4) + 1

                        If Rs.Read() Then
                            'SE C'E' UN'ALTRA RIGA CONTINUO ALTRIMENTI BLOCCO LA RICERCA PER LE PREVISTE USCITE
                        Else
                            previste_uscite = False
                        End If
                    Else
                        'SE C'E' LA RIGA MA IL GRUPPO NON CORRISPONDE A QUELLO ATTUALE ESCO DAL CICLO
                        gruppo_corrente = False
                    End If
                Loop
                '--------------------------------------------------------------------------------------------------------------------------

                'RIENTRI DA PRENOTAZIONE---------------------------------------------------------------------------------------------------
                gruppo_corrente = True

                Do While (rientri_prenotazioni And gruppo_corrente)
                    If Rs1("gruppo") = gruppi(i) Then
                        'SE IL GRUPPO CORRISPONDE A QUELLO ATTUALE

                        If CInt(Rs1("ore_rientro")) < 12 Then
                            situazione(k + 5) = situazione(k + 5) + 1
                        ElseIf CInt(Rs1("ore_rientro")) >= 12 And CInt(Rs1("ore_rientro")) < 16 Then
                            situazione(k + 6) = situazione(k + 6) + 1
                        ElseIf CInt(Rs1("ore_rientro")) >= 16 And CInt(Rs1("ore_rientro")) < 20 Then
                            situazione(k + 7) = situazione(k + 7) + 1
                        ElseIf CInt(Rs1("ore_rientro")) >= 20 And CInt(Rs1("ore_rientro")) <= 23 Then
                            situazione(k + 8) = situazione(k + 8) + 1
                        End If
                        situazione(k + 9) = situazione(k + 9) + 1

                        If Rs1.Read() Then
                            'SE C'E' UN'ALTRA RIGA CONTINUO ALTRIMENTI BLOCCO LA RICERCA PER I RIENTRI DA PRENOTAZIONE
                        Else
                            rientri_prenotazioni = False
                        End If
                    Else
                        'SE C'E' LA RIGA MA IL GRUPPO NON CORRISPONDE A QUELLO ATTUALE ESCO DAL CICLO
                        gruppo_corrente = False
                    End If
                Loop
                '--------------------------------------------------------------------------------------------------------------------------
                'RIENTRI DA CONTRATTO------------------------------------------------------------------------------------------------------
                gruppo_corrente = True

                Do While (rientri_contratti And gruppo_corrente)
                    If Rs2("gruppo") = gruppi(i) Then
                        'SE IL GRUPPO CORRISPONDE A QUELLO ATTUALE

                        If Hour(Rs2("data_presunto_rientro")) < 12 Then
                            situazione(k + 10) = situazione(k + 10) + 1
                        ElseIf Hour(Rs2("data_presunto_rientro")) >= 12 And Hour(Rs2("data_presunto_rientro")) < 16 Then
                            situazione(k + 11) = situazione(k + 11) + 1
                        ElseIf Hour(Rs2("data_presunto_rientro")) >= 16 And Hour(Rs2("data_presunto_rientro")) < 20 Then
                            situazione(k + 12) = situazione(k + 12) + 1
                        ElseIf (Hour(Rs2("data_presunto_rientro")) >= 20 And Hour(Rs2("data_presunto_rientro")) <= 23) Then
                            situazione(k + 13) = situazione(k + 13) + 1
                        End If
                        situazione(k + 14) = situazione(k + 14) + 1

                        If Rs2.Read() Then
                            'SE C'E' UN'ALTRA RIGA CONTINUO ALTRIMENTI BLOCCO LA RICERCA PER I RIENTRI DA CONTRATTO
                        Else
                            rientri_contratti = False
                        End If
                    Else
                        'SE C'E' LA RIGA MA IL GRUPPO NON CORRISPONDE A QUELLO ATTUALE ESCO DAL CICLO
                        gruppo_corrente = False
                    End If
                Loop
                '--------------------------------------------------------------------------------------------------------------------------


                k = k + 15
                i = i + 1
            Loop

            Cmd.Dispose()
            Cmd = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Cmd2.Dispose()
            Cmd2 = Nothing

            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
            Dbc1.Close()
            Dbc1.Dispose()
            Dbc1 = Nothing
            Dbc2.Close()
            Dbc2.Dispose()
            Dbc2 = Nothing

            getSituazione_x_gruppi = situazione


        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni getSituazione_x_gruppi  : <br/>" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Public Shared Function getSituazione_x_gruppi_new(ByVal dataIniziale As DateTime, ByVal stazione As String, Optional ByVal gruppi_da_considerare() As String = Nothing) As Integer()

        Dim sqlStr As String = ""
        Dim data1 As String = funzioni_comuni.getDataDb_senza_orario2(dataIniziale, HttpContext.Current.Request.ServerVariables("HTTP_HOST"))
        Dim i As Integer
        Dim situazione(900) As Integer
        Dim gruppi(60) As String

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Dbc1 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc1.Open()
        Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc2.Open()

        'aggiunto il 31.10.2021 x Rientri ODL
        Dim Dbc4 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc4.Open()

        'Tony
        'aggiunto il 31.05.2022 x Lavaggi
        Dim Dbc5 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc5.Open()

        Try


            'PER PRIMA COSA SELEZIONO I GRUPPI SE NON E' STATO PASSATO UN ELENCO GRUPPI DA CONSIDERARE ---------------------------------------
            sqlStr = "SELECT cod_gruppo FROM gruppi WITH(NOLOCK) WHERE attivo='1' ORDER BY cod_gruppo"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader


            If gruppi_da_considerare Is Nothing Then
                Rs = Cmd.ExecuteReader()

                i = 0

                Do While Rs.Read()
                    gruppi(i) = Rs("cod_gruppo")
                    i = i + 1
                Loop
                gruppi(i) = "000"

                Dbc.Close()
                Dbc.Open()
            Else
                Dim z As Integer = 0
                i = 0
                Do While gruppi_da_considerare(z) <> "000"
                    gruppi(i) = gruppi_da_considerare(z)

                    i = i + 1
                    z = z + 1
                Loop

                gruppi(i) = "000"
            End If


            '----------------------------------------------------------------------------------------------------------------------------------
            'ORA SELEZIONO PER TUTTI I GRUPPI:

            '1)PREVISTE USCITE DA PRENOTAZIONI-------------------------------------------------------------------------------------------------
            sqlStr = "SELECT ore_uscita, gruppi.cod_gruppo As gruppo FROM prenotazioni WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) "
            sqlStr += "ON ISNULL(prenotazioni.ID_GRUPPO_APP,prenotazioni.ID_GRUPPO)=gruppi.id_gruppo WHERE YEAR(PRDATA_OUT)=YEAR('" & data1 & "') AND MONTH(PRDATA_OUT)=MONTH('" & data1 & "') AND DAY(PRDATA_OUT)=DAY('" & data1 & "') AND PRID_stazione_out='" & stazione & "' AND status='0' AND attiva='1' ORDER BY gruppi.cod_gruppo"

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Rs = Cmd.ExecuteReader()

            '2)RIENTRI DA PRENOTAZIONE (presunti)----------------------------------------------------------------------------------------------

            sqlStr = "SELECT ore_rientro, gruppi.cod_gruppo As gruppo FROM prenotazioni WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON ISNULL(prenotazioni.ID_GRUPPO_APP,prenotazioni.ID_GRUPPO)=gruppi.id_gruppo WHERE YEAR(PRDATA_PR)=YEAR('" & data1 & "') AND MONTH(PRDATA_PR)=MONTH('" & data1 & "') AND DAY(PRDATA_PR)=DAY('" & data1 & "') AND PRID_stazione_pr='" & stazione & "' AND status='0' AND attiva='1' ORDER BY gruppi.cod_gruppo"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc1)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            '3)RIENTRI DA CONTRATTO (previsti)-------------------------------------------------------------------------------------------------

            sqlStr = "SELECT data_presunto_rientro, gruppi.cod_gruppo As gruppo FROM contratti WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON ISNULL(contratti.id_gruppo_app,contratti.id_gruppo_auto)=gruppi.id_gruppo WHERE YEAR(data_presunto_rientro)=YEAR('" & data1 & "') AND MONTH(data_presunto_rientro)=MONTH('" & data1 & "') AND DAY(data_presunto_rientro)=DAY('" & data1 & "') AND id_stazione_presunto_rientro='" & stazione & "' AND status='2' AND contratti.attivo='1' ORDER BY gruppi.cod_gruppo"

            'HttpContext.Current.Trace.Write("SQL: " & sqlStr)

            Dim Cmd2 As New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
            Dim Rs2 As Data.SqlClient.SqlDataReader
            Rs2 = Cmd2.ExecuteReader()

            '4) rientri da ODL 'aggiunto il 31.10.2021 (previsti)
            'sqlStr = "SELECT ore_rientro, gruppi.cod_gruppo As gruppo FROM prenotazioni WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON ISNULL(prenotazioni.ID_GRUPPO_APP,prenotazioni.ID_GRUPPO)=gruppi.id_gruppo "
            'sqlStr = "WHERE YEAR(PRDATA_PR)=YEAR('" & data1 & "') AND MONTH(PRDATA_PR)=MONTH('" & data1 & "') AND DAY(PRDATA_PR)=DAY('" & data1 & "') "
            'sqlStr = "And PRID_stazione_pr='" & stazione & "' AND status='0' AND attiva='1' ORDER BY gruppi.cod_gruppo"

            sqlStr = "Select DatePart(Hour, odl.data_previsto_rientro) As ore_rientro, data_previsto_rientro as data_presunto_rientro, gruppi.cod_gruppo as gruppo From odl "
            sqlStr += "INNER Join veicoli ON odl.id_veicolo = veicoli.id INNER JOIN MODELLI ON veicoli.id_modello = MODELLI.ID_MODELLO INNER JOIN GRUPPI ON MODELLI.ID_Gruppo = GRUPPI.ID_gruppo "
            sqlStr += "WHERE odl.attivo = 1 And [id_stato_odl]<>9 " 'inserita la condizione <>9 e cioè se è chiuso ? 05.02.2022 email di Fco
            sqlStr += "And (Year(odl.data_previsto_rientro) = Year('" & data1 & "')) "
            sqlStr += "And (Month(odl.data_previsto_rientro) = Month('" & data1 & "')) "
            sqlStr += "And (Day(odl.data_previsto_rientro) = Day('" & data1 & "')) "
            sqlStr += "And (odl.id_stazione_previsto_rientro = '" & stazione & "') "
            sqlStr += "ORDER BY gruppi.cod_gruppo, odl.id desc, odl.data_previsto_rientro desc, id_stato_odl desc;"


            Dim Cmd4 As New Data.SqlClient.SqlCommand(sqlStr, Dbc4)
            Dim Rs4 As Data.SqlClient.SqlDataReader
            Rs4 = Cmd4.ExecuteReader()

            'Tony
            sqlStr = "SELECT  DATEPART(HOUR, lavaggi.data_presunto_rientro) as ore_rientro, GRUPPI.cod_gruppo as gruppo, lavaggi.stato, lavaggi.data_presunto_rientro, lavaggi.id From lavaggi INNER JOIN veicoli ON lavaggi.id_veicolo = veicoli.id INNER JOIN MODELLI ON veicoli.id_modello = MODELLI.ID_MODELLO INNER JOIN GRUPPI ON MODELLI.ID_Gruppo = GRUPPI.ID_gruppo WHERE (YEAR(lavaggi.data_presunto_rientro) = YEAR('" & data1 & "')) And (MONTH(lavaggi.data_presunto_rientro) = MONTH('" & data1 & "')) And (DAY(lavaggi.data_presunto_rientro) = DAY('" & data1 & "')) AND (lavaggi.id_stazione_uscita = '" & stazione & "') AND lavaggi.stato = 0 ORDER BY lavaggi.id desc, lavaggi.data_presunto_rientro desc, stato desc;"


            Dim Cmd5 As New Data.SqlClient.SqlCommand(sqlStr, Dbc5)
            Dim Rs5 As Data.SqlClient.SqlDataReader
            Rs5 = Cmd5.ExecuteReader()

            'LEGGO PER LA PRIMA VOLTA I RESULT SET E MEMORIZZO IN VARIABILI SE VI SONO RIGHE O MENO-------------------------------------------
            i = 0
            Dim k As Integer = 1
            Dim gruppo_corrente As Boolean

            Dim previste_uscite As Boolean
            Dim rientri_prenotazioni As Boolean
            Dim rientri_contratti As Boolean
            Dim rientri_ODL As Boolean 'aggiunto il 31.10.2021
            Dim rientri_LAVAGGI As Boolean 'aggiunto il 31.05.2022


            If Rs.Read() Then
                previste_uscite = True
            Else
                previste_uscite = False
            End If

            If Rs1.Read() Then
                rientri_prenotazioni = True
            Else
                rientri_prenotazioni = False
            End If

            If Rs2.Read() Then
                rientri_contratti = True
            Else
                rientri_contratti = False
            End If

            'aggiunto 31.10.2021
            If Rs4.Read() Then
                rientri_ODL = True
            Else
                rientri_ODL = False
            End If

            'Tony aggiunto 31.05.2022
            If Rs5.Read() Then
                rientri_LAVAGGI = True
            Else
                rientri_LAVAGGI = False
            End If


            'PER OGNI GRUPPO CREO 45 ELEMENTI NELL'ARRAY DI OUTPUT-----------------------------------------------------------------------
            'CORRISPONDENTE ALLE 45 CELLE DELLA TABELLA DOVE VIENE VISUALIZZATO IL RISULTATO---------------------------------------------
            Do While gruppi(i) <> "000"

                situazione(k) = 0       'PREVISTE USCITE DA PRENOTAZIONI 00/08
                situazione(k + 1) = 0   'PREVISTE USCITE DA PRENOTAZIONI 08/09
                situazione(k + 2) = 0   'PREVISTE USCITE DA PRENOTAZIONI 09/10
                situazione(k + 3) = 0   'PREVISTE USCITE DA PRENOTAZIONI 10/11
                situazione(k + 4) = 0   'PREVISTE USCITE DA PRENOTAZIONI 11/12
                situazione(k + 5) = 0   'PREVISTE USCITE DA PRENOTAZIONI 12/13
                situazione(k + 6) = 0   'PREVISTE USCITE DA PRENOTAZIONI 13/14
                situazione(k + 7) = 0   'PREVISTE USCITE DA PRENOTAZIONI 14/15
                situazione(k + 8) = 0   'PREVISTE USCITE DA PRENOTAZIONI 15/16
                situazione(k + 9) = 0   'PREVISTE USCITE DA PRENOTAZIONI 16/17
                situazione(k + 10) = 0   'PREVISTE USCITE DA PRENOTAZIONI 17/18
                situazione(k + 11) = 0   'PREVISTE USCITE DA PRENOTAZIONI 18/19
                situazione(k + 12) = 0   'PREVISTE USCITE DA PRENOTAZIONI 19/20
                situazione(k + 13) = 0   'PREVISTE USCITE DA PRENOTAZIONI 20/24
                situazione(k + 14) = 0   'PREVISTE USCITE DA PRENOTAZIONI 00/24

                situazione(k + 15) = 0   'RIENTRI DA PRENOTAZIONE 00/08
                situazione(k + 16) = 0   'RIENTRI DA PRENOTAZIONE 08/09
                situazione(k + 17) = 0   'RIENTRI DA PRENOTAZIONE 09/10
                situazione(k + 18) = 0   'RIENTRI DA PRENOTAZIONE 10/11
                situazione(k + 19) = 0   'RIENTRI DA PRENOTAZIONE 11/12
                situazione(k + 20) = 0   'RIENTRI DA PRENOTAZIONE 12/13
                situazione(k + 21) = 0   'RIENTRI DA PRENOTAZIONE 13/14
                situazione(k + 22) = 0   'RIENTRI DA PRENOTAZIONE 14/15
                situazione(k + 23) = 0   'RIENTRI DA PRENOTAZIONE 15/16
                situazione(k + 24) = 0   'RIENTRI DA PRENOTAZIONE 16/17
                situazione(k + 25) = 0   'RIENTRI DA PRENOTAZIONE 17/18
                situazione(k + 26) = 0   'RIENTRI DA PRENOTAZIONE 18/19
                situazione(k + 27) = 0   'RIENTRI DA PRENOTAZIONE 19/20
                situazione(k + 28) = 0   'RIENTRI DA PRENOTAZIONE 20/24
                situazione(k + 29) = 0   'RIENTRI DA PRENOTAZIONE 00/24

                situazione(k + 30) = 0  'RIENTRI DA CONTRATTO 00/08
                situazione(k + 31) = 0  'RIENTRI DA CONTRATTO 08/09
                situazione(k + 32) = 0  'RIENTRI DA CONTRATTO 09/10
                situazione(k + 33) = 0  'RIENTRI DA CONTRATTO 10/11
                situazione(k + 34) = 0  'RIENTRI DA CONTRATTO 11/12
                situazione(k + 35) = 0  'RIENTRI DA CONTRATTO 12/13
                situazione(k + 36) = 0  'RIENTRI DA CONTRATTO 13/14
                situazione(k + 37) = 0  'RIENTRI DA CONTRATTO 14/15
                situazione(k + 38) = 0  'RIENTRI DA CONTRATTO 15/16
                situazione(k + 39) = 0  'RIENTRI DA CONTRATTO 16/17
                situazione(k + 40) = 0  'RIENTRI DA CONTRATTO 17/18
                situazione(k + 41) = 0  'RIENTRI DA CONTRATTO 18/19
                situazione(k + 42) = 0  'RIENTRI DA CONTRATTO 19/20
                situazione(k + 43) = 0  'RIENTRI DA CONTRATTO 20/24
                situazione(k + 44) = 0  'RIENTRI DA CONTRATTO 00/24


                'AGGIORNO I VALORI
                'PREVISTE USCITE DA PRENOTAZIONI----------------------------------------------------------------------------------------

                gruppo_corrente = True

                Do While (previste_uscite And gruppo_corrente)
                    If Rs("gruppo") = gruppi(i) Then
                        'SE IL GRUPPO CORRISPONDE A QUELLO ATTUALE

                        If CInt(Rs("ore_uscita")) < 8 Then
                            situazione(k) = situazione(k) + 1
                        ElseIf CInt(Rs("ore_uscita")) >= 8 And CInt(Rs("ore_uscita")) < 9 Then
                            situazione(k + 1) = situazione(k + 1) + 1
                        ElseIf CInt(Rs("ore_uscita")) >= 9 And CInt(Rs("ore_uscita")) < 10 Then
                            situazione(k + 2) = situazione(k + 2) + 1
                        ElseIf CInt(Rs("ore_uscita")) >= 10 And CInt(Rs("ore_uscita")) < 11 Then
                            situazione(k + 3) = situazione(k + 3) + 1
                        ElseIf CInt(Rs("ore_uscita")) >= 11 And CInt(Rs("ore_uscita")) < 12 Then
                            situazione(k + 4) = situazione(k + 4) + 1
                        ElseIf CInt(Rs("ore_uscita")) >= 12 And CInt(Rs("ore_uscita")) < 13 Then
                            situazione(k + 5) = situazione(k + 5) + 1
                        ElseIf CInt(Rs("ore_uscita")) >= 13 And CInt(Rs("ore_uscita")) < 14 Then
                            situazione(k + 6) = situazione(k + 6) + 1
                        ElseIf CInt(Rs("ore_uscita")) >= 14 And CInt(Rs("ore_uscita")) < 15 Then
                            situazione(k + 7) = situazione(k + 7) + 1
                        ElseIf CInt(Rs("ore_uscita")) >= 15 And CInt(Rs("ore_uscita")) < 16 Then
                            situazione(k + 8) = situazione(k + 8) + 1
                        ElseIf CInt(Rs("ore_uscita")) >= 16 And CInt(Rs("ore_uscita")) < 17 Then
                            situazione(k + 9) = situazione(k + 9) + 1
                        ElseIf CInt(Rs("ore_uscita")) >= 17 And CInt(Rs("ore_uscita")) < 18 Then
                            situazione(k + 10) = situazione(k + 10) + 1
                        ElseIf CInt(Rs("ore_uscita")) >= 18 And CInt(Rs("ore_uscita")) < 19 Then
                            situazione(k + 11) = situazione(k + 11) + 1
                        ElseIf CInt(Rs("ore_uscita")) >= 19 And CInt(Rs("ore_uscita")) < 20 Then
                            situazione(k + 12) = situazione(k + 12) + 1
                        ElseIf CInt(Rs("ore_uscita")) >= 20 And CInt(Rs("ore_uscita")) <= 23 Then
                            situazione(k + 13) = situazione(k + 13) + 1
                        End If

                        situazione(k + 14) = situazione(k + 14) + 1

                        If Rs.Read() Then
                            'SE C'E' UN'ALTRA RIGA CONTINUO ALTRIMENTI BLOCCO LA RICERCA PER LE PREVISTE USCITE
                        Else
                            previste_uscite = False
                        End If
                    Else
                        'SE C'E' LA RIGA MA IL GRUPPO NON CORRISPONDE A QUELLO ATTUALE ESCO DAL CICLO
                        gruppo_corrente = False
                    End If
                Loop
                '--------------------------------------------------------------------------------------------------------------------------

                'RIENTRI DA PRENOTAZIONE---------------------------------------------------------------------------------------------------
                gruppo_corrente = True

                Do While (rientri_prenotazioni And gruppo_corrente)

                    If Rs1("gruppo") = gruppi(i) Then


                        'SE IL GRUPPO CORRISPONDE A QUELLO ATTUALE
                        If CInt(Rs1("ore_rientro")) < 8 Then
                            situazione(k + 15) = situazione(k + 15) + 1
                        ElseIf CInt(Rs1("ore_rientro")) >= 8 And CInt(Rs1("ore_rientro")) < 9 Then
                            situazione(k + 16) = situazione(k + 16) + 1
                        ElseIf CInt(Rs1("ore_rientro")) >= 9 And CInt(Rs1("ore_rientro")) < 10 Then
                            situazione(k + 17) = situazione(k + 17) + 1
                        ElseIf CInt(Rs1("ore_rientro")) >= 10 And CInt(Rs1("ore_rientro")) < 11 Then
                            situazione(k + 18) = situazione(k + 18) + 1
                        ElseIf CInt(Rs1("ore_rientro")) >= 11 And CInt(Rs1("ore_rientro")) < 12 Then
                            situazione(k + 19) = situazione(k + 19) + 1
                        ElseIf CInt(Rs1("ore_rientro")) >= 12 And CInt(Rs1("ore_rientro")) < 13 Then
                            situazione(k + 20) = situazione(k + 20) + 1
                        ElseIf CInt(Rs1("ore_rientro")) >= 13 And CInt(Rs1("ore_rientro")) < 14 Then
                            situazione(k + 21) = situazione(k + 21) + 1
                        ElseIf CInt(Rs1("ore_rientro")) >= 14 And CInt(Rs1("ore_rientro")) < 15 Then
                            situazione(k + 22) = situazione(k + 22) + 1
                        ElseIf CInt(Rs1("ore_rientro")) >= 15 And CInt(Rs1("ore_rientro")) < 16 Then
                            situazione(k + 23) = situazione(k + 23) + 1
                        ElseIf CInt(Rs1("ore_rientro")) >= 16 And CInt(Rs1("ore_rientro")) < 17 Then
                            situazione(k + 24) = situazione(k + 24) + 1
                        ElseIf CInt(Rs1("ore_rientro")) >= 17 And CInt(Rs1("ore_rientro")) < 18 Then
                            situazione(k + 25) = situazione(k + 25) + 1
                        ElseIf CInt(Rs1("ore_rientro")) >= 18 And CInt(Rs1("ore_rientro")) < 19 Then
                            situazione(k + 26) = situazione(k + 26) + 1
                        ElseIf CInt(Rs1("ore_rientro")) >= 19 And CInt(Rs1("ore_rientro")) < 20 Then
                            situazione(k + 27) = situazione(k + 27) + 1
                        ElseIf CInt(Rs1("ore_rientro")) >= 20 And CInt(Rs1("ore_rientro")) <= 23 Then
                            situazione(k + 28) = situazione(k + 28) + 1
                        End If
                        situazione(k + 29) = situazione(k + 29) + 1

                        If Rs1.Read() Then
                            'SE C'E' UN'ALTRA RIGA CONTINUO ALTRIMENTI BLOCCO LA RICERCA PER I RIENTRI DA PRENOTAZIONE
                        Else
                            rientri_prenotazioni = False
                        End If
                    Else
                        'SE C'E' LA RIGA MA IL GRUPPO NON CORRISPONDE A QUELLO ATTUALE ESCO DAL CICLO
                        gruppo_corrente = False
                    End If
                Loop


                ''Start ODL 31.10.2021 / 02.11.2021 spostato calcolo x ODL
                '---------------------------------------------------------------------------------------------------------------------
                'RIENTRI DA ODL ------------------------------------------------------------------------------------------------------
                gruppo_corrente = True

                Do While (rientri_ODL And gruppo_corrente)
                    If Rs4("gruppo") = gruppi(i) Then

                        'SE IL GRUPPO CORRISPONDE A QUELLO ATTUALE

                        If CInt(Rs4("ore_rientro")) < 8 Then
                            situazione(k + 15) = situazione(k + 15) + 1
                        ElseIf CInt(Rs4("ore_rientro")) >= 8 And CInt(Rs4("ore_rientro")) < 9 Then
                            situazione(k + 16) = situazione(k + 16) + 1
                        ElseIf CInt(Rs4("ore_rientro")) >= 9 And CInt(Rs4("ore_rientro")) < 10 Then
                            situazione(k + 17) = situazione(k + 17) + 1
                        ElseIf CInt(Rs4("ore_rientro")) >= 10 And CInt(Rs4("ore_rientro")) < 11 Then
                            situazione(k + 18) = situazione(k + 18) + 1
                        ElseIf CInt(Rs4("ore_rientro")) >= 11 And CInt(Rs4("ore_rientro")) < 12 Then
                            situazione(k + 19) = situazione(k + 19) + 1
                        ElseIf CInt(Rs4("ore_rientro")) >= 12 And CInt(Rs4("ore_rientro")) < 13 Then
                            situazione(k + 20) = situazione(k + 20) + 1
                        ElseIf CInt(Rs4("ore_rientro")) >= 13 And CInt(Rs4("ore_rientro")) < 14 Then
                            situazione(k + 21) = situazione(k + 21) + 1
                        ElseIf CInt(Rs4("ore_rientro")) >= 14 And CInt(Rs4("ore_rientro")) < 15 Then
                            situazione(k + 22) = situazione(k + 22) + 1
                        ElseIf CInt(Rs4("ore_rientro")) >= 15 And CInt(Rs4("ore_rientro")) < 16 Then
                            situazione(k + 23) = situazione(k + 23) + 1
                        ElseIf CInt(Rs4("ore_rientro")) >= 16 And CInt(Rs4("ore_rientro")) < 17 Then
                            situazione(k + 24) = situazione(k + 24) + 1
                        ElseIf CInt(Rs4("ore_rientro")) >= 17 And CInt(Rs4("ore_rientro")) < 18 Then
                            situazione(k + 25) = situazione(k + 25) + 1
                        ElseIf CInt(Rs4("ore_rientro")) >= 18 And CInt(Rs4("ore_rientro")) < 19 Then
                            situazione(k + 26) = situazione(k + 26) + 1
                        ElseIf CInt(Rs4("ore_rientro")) >= 19 And CInt(Rs4("ore_rientro")) < 20 Then
                            situazione(k + 27) = situazione(k + 27) + 1
                        ElseIf CInt(Rs4("ore_rientro")) >= 20 And CInt(Rs4("ore_rientro")) <= 23 Then
                            situazione(k + 28) = situazione(k + 28) + 1
                        End If

                        situazione(k + 29) = situazione(k + 29) + 1

                        If Rs4.Read() Then
                            'SE C'E' UN'ALTRA RIGA CONTINUO ALTRIMENTI BLOCCO LA RICERCA PER I RIENTRI DA ODL
                        Else
                            rientri_ODL = False
                        End If

                    Else
                        'SE C'E' LA RIGA MA IL GRUPPO NON CORRISPONDE A QUELLO ATTUALE ESCO DAL CICLO
                        gruppo_corrente = False
                    End If

                Loop
                '--------------------------------------------------------------------------------------------------------------------------
                ''END ODL inserito il 31.10.2021

                'Tony
                ''Start Lavaggi
                '---------------------------------------------------------------------------------------------------------------------                
                gruppo_corrente = True

                'response.write("rientri_LAVAGGI " & rientri_LAVAGGI & "<br>")

                'HttpContext.Current.Response.Write(gruppi(i) & "<br>")
                Do While (rientri_LAVAGGI And gruppo_corrente)
                    If Rs5("gruppo") = gruppi(i) Then
                        'HttpContext.Current.Response.Write("Rientro " & Rs5("ore_rientro") & "<br>")

                        'SE IL GRUPPO CORRISPONDE A QUELLO ATTUALE
                        If CInt(Rs5("ore_rientro")) < 8 Then
                            situazione(k + 15) = situazione(k + 15) + 1
                        ElseIf CInt(Rs5("ore_rientro")) >= 8 And CInt(Rs5("ore_rientro")) < 9 Then
                            situazione(k + 16) = situazione(k + 16) + 1
                        ElseIf CInt(Rs5("ore_rientro")) >= 9 And CInt(Rs5("ore_rientro")) < 10 Then
                            situazione(k + 17) = situazione(k + 17) + 1
                        ElseIf CInt(Rs5("ore_rientro")) >= 10 And CInt(Rs5("ore_rientro")) < 11 Then
                            situazione(k + 18) = situazione(k + 18) + 1
                        ElseIf CInt(Rs5("ore_rientro")) >= 11 And CInt(Rs5("ore_rientro")) < 12 Then
                            situazione(k + 19) = situazione(k + 19) + 1
                        ElseIf CInt(Rs5("ore_rientro")) >= 12 And CInt(Rs5("ore_rientro")) < 13 Then
                            situazione(k + 20) = situazione(k + 20) + 1
                        ElseIf CInt(Rs5("ore_rientro")) >= 13 And CInt(Rs5("ore_rientro")) < 14 Then
                            situazione(k + 21) = situazione(k + 21) + 1
                        ElseIf CInt(Rs5("ore_rientro")) >= 14 And CInt(Rs5("ore_rientro")) < 15 Then
                            situazione(k + 22) = situazione(k + 22) + 1
                        ElseIf CInt(Rs5("ore_rientro")) >= 15 And CInt(Rs5("ore_rientro")) < 16 Then
                            situazione(k + 23) = situazione(k + 23) + 1
                        ElseIf CInt(Rs5("ore_rientro")) >= 16 And CInt(Rs5("ore_rientro")) < 17 Then
                            situazione(k + 24) = situazione(k + 24) + 1
                        ElseIf CInt(Rs5("ore_rientro")) >= 17 And CInt(Rs5("ore_rientro")) < 18 Then
                            situazione(k + 25) = situazione(k + 25) + 1
                        ElseIf CInt(Rs5("ore_rientro")) >= 18 And CInt(Rs5("ore_rientro")) < 19 Then
                            situazione(k + 26) = situazione(k + 26) + 1
                        ElseIf CInt(Rs5("ore_rientro")) >= 19 And CInt(Rs5("ore_rientro")) < 20 Then
                            situazione(k + 27) = situazione(k + 27) + 1
                        ElseIf CInt(Rs5("ore_rientro")) >= 20 And CInt(Rs5("ore_rientro")) <= 23 Then
                            situazione(k + 28) = situazione(k + 28) + 1
                        End If

                        situazione(k + 29) = situazione(k + 29) + 1

                        If Rs5.Read() Then
                            'SE C'E' UN'ALTRA RIGA CONTINUO ALTRIMENTI BLOCCO LA RICERCA PER I Lavaggi
                        Else
                            rientri_LAVAGGI = False
                        End If
                    Else
                        'SE C'E' LA RIGA MA IL GRUPPO NON CORRISPONDE A QUELLO ATTUALE ESCO DAL CICLO
                        gruppo_corrente = False
                    End If

                Loop
                '--------------------------------------------------------------------------------------------------------------------------
                ''END Lavaggi

                '--------------------------------------------------------------------------------------------------------------------------
                'RIENTRI DA CONTRATTO------------------------------------------------------------------------------------------------------
                gruppo_corrente = True

                Do While (rientri_contratti And gruppo_corrente)
                    If Rs2("gruppo") = gruppi(i) Then

                        If Rs2("gruppo") = "F" Then

                            Dim iii As String
                            iii = 0
                        End If
                        'SE IL GRUPPO CORRISPONDE A QUELLO ATTUALE

                        If Hour(Rs2("data_presunto_rientro")) < 8 Then
                            situazione(k + 30) = situazione(k + 30) + 1
                        ElseIf Hour(Rs2("data_presunto_rientro")) >= 8 And Hour(Rs2("data_presunto_rientro")) < 9 Then
                            situazione(k + 31) = situazione(k + 31) + 1
                        ElseIf Hour(Rs2("data_presunto_rientro")) >= 9 And Hour(Rs2("data_presunto_rientro")) < 10 Then
                            situazione(k + 32) = situazione(k + 32) + 1
                        ElseIf Hour(Rs2("data_presunto_rientro")) >= 10 And Hour(Rs2("data_presunto_rientro")) < 11 Then
                            situazione(k + 33) = situazione(k + 33) + 1
                        ElseIf Hour(Rs2("data_presunto_rientro")) >= 11 And Hour(Rs2("data_presunto_rientro")) < 12 Then
                            situazione(k + 34) = situazione(k + 34) + 1
                        ElseIf Hour(Rs2("data_presunto_rientro")) >= 12 And Hour(Rs2("data_presunto_rientro")) < 13 Then
                            situazione(k + 35) = situazione(k + 35) + 1
                        ElseIf Hour(Rs2("data_presunto_rientro")) >= 13 And Hour(Rs2("data_presunto_rientro")) < 14 Then
                            situazione(k + 36) = situazione(k + 36) + 1
                        ElseIf Hour(Rs2("data_presunto_rientro")) >= 14 And Hour(Rs2("data_presunto_rientro")) < 15 Then
                            situazione(k + 37) = situazione(k + 37) + 1
                        ElseIf Hour(Rs2("data_presunto_rientro")) >= 15 And Hour(Rs2("data_presunto_rientro")) < 16 Then
                            situazione(k + 38) = situazione(k + 38) + 1
                        ElseIf Hour(Rs2("data_presunto_rientro")) >= 16 And Hour(Rs2("data_presunto_rientro")) < 17 Then
                            situazione(k + 39) = situazione(k + 39) + 1
                        ElseIf Hour(Rs2("data_presunto_rientro")) >= 17 And Hour(Rs2("data_presunto_rientro")) < 18 Then
                            situazione(k + 40) = situazione(k + 40) + 1
                        ElseIf Hour(Rs2("data_presunto_rientro")) >= 18 And Hour(Rs2("data_presunto_rientro")) < 19 Then
                            situazione(k + 41) = situazione(k + 41) + 1
                        ElseIf Hour(Rs2("data_presunto_rientro")) >= 19 And Hour(Rs2("data_presunto_rientro")) < 20 Then
                            situazione(k + 42) = situazione(k + 42) + 1
                        ElseIf (Hour(Rs2("data_presunto_rientro")) >= 20 And Hour(Rs2("data_presunto_rientro")) <= 23) Then
                            situazione(k + 43) = situazione(k + 43) + 1
                        End If
                        situazione(k + 44) = situazione(k + 44) + 1

                        If Rs2.Read() Then
                            'SE C'E' UN'ALTRA RIGA CONTINUO ALTRIMENTI BLOCCO LA RICERCA PER I RIENTRI DA CONTRATTO
                        Else
                            rientri_contratti = False
                        End If
                    Else
                        'SE C'E' LA RIGA MA IL GRUPPO NON CORRISPONDE A QUELLO ATTUALE ESCO DAL CICLO
                        gruppo_corrente = False
                    End If
                Loop
                '--------------------------------------------------------------------------------------------------------------------------


                k = k + 45
                i = i + 1

            Loop



            Cmd.Dispose()
            Cmd = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Cmd2.Dispose()
            Cmd2 = Nothing
            Cmd4.Dispose()  'aggiunto il 31.10.2021 x ODL
            Cmd4 = Nothing
            'Tony
            Cmd5.Dispose()  'aggiunto il 31.05.2022 x Lavaggi
            Cmd5 = Nothing

            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
            Dbc1.Close()
            Dbc1.Dispose()
            Dbc1 = Nothing
            Dbc2.Close()
            Dbc2.Dispose()
            Dbc2 = Nothing
            Dbc4.Close()     'aggiunto il 31.10.2021 x ODL
            Dbc4.Dispose()
            Dbc4 = Nothing
            'Tony
            Dbc5.Close()     'aggiunto il 31.05.2022 x Lavaggi
            Dbc5.Dispose()
            Dbc5 = Nothing



            getSituazione_x_gruppi_new = situazione

        Catch ex As Exception
            HttpContext.Current.Response.Write("Error funzioni_comuni_Situazione_x_gruppi_new " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try



    End Function

    Public Shared Function getGruppi() As String()
        Dim sqla As String = "Select cod_gruppo FROM gruppi With(NOLOCK)  WHERE attivo='1' ORDER BY cod_gruppo"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Dim gruppi(60) As String

            Dim i As Integer = 0

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
            HttpContext.Current.Response.Write("error funzioni_comuni getGruppi  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function

    Public Shared Function getDisponibilitaOdierna(ByVal dataIniziale As DateTime, ByVal stazione As String, ByVal gruppi() As String) As Integer()

        Dim disponibilita(60) As Integer
        Dim i As Integer = 0
        Dim sqlStr As String

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)



            Do While gruppi(i) <> "000"
                'sqlStr = "SELECT ISNULL(COUNT(veicoli.id),'0') As disponbilita FROM veicoli INNER JOIN modelli ON veicoli.id_modello=modelli.id_modello INNER JOIN gruppi ON modelli.id_gruppo=gruppi.id_gruppo WHERE veicoli.id_stazione='" & stazione & "' AND veicoli.disponibile_nolo='1' AND gruppi.cod_gruppo='" & gruppi(i) & "'"
                sqlStr = "SELECT ISNULL(COUNT(veicoli.id),'0') As disponbilita FROM veicoli INNER JOIN modelli ON veicoli.id_modello=modelli.id_modello INNER JOIN gruppi ON modelli.id_gruppo=gruppi.id_gruppo WHERE veicoli.id_stazione='" & stazione & "' AND (veicoli.disponibile_nolo='1' OR veicoli.da_rifornire ='1') AND gruppi.cod_gruppo='" & gruppi(i) & "'"

                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                disponibilita(i) = Cmd.ExecuteScalar

                i = i + 1
            Loop

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            getDisponibilitaOdierna = disponibilita
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni getDisponibilitaOdierna  : <br/>" & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try




    End Function
    '---------------------------------------------------------------------------------------------------------------------------------------
    Public Shared Function get_id_young_driver() As String
        Dim sqla As String = "SELECT id FROM condizioni_elementi WITH(NOLOCK) WHERE tipologia='JOUNG'"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            get_id_young_driver = Cmd.ExecuteScalar

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni get_id_young_driver  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try


    End Function

    Public Shared Function formatHtml(ByVal testo As String) As String
        testo = Replace(testo, "á", "&aacute;")
        testo = Replace(testo, "à", "&agrave;")
        testo = Replace(testo, "é", "&eacute;")
        testo = Replace(testo, "è", "&egrave;")
        testo = Replace(testo, "í", "&iacute;")
        testo = Replace(testo, "ì", "&igrave;")
        testo = Replace(testo, "ó", "&oacute;")
        testo = Replace(testo, "ò", "&ograve;")
        testo = Replace(testo, "ú", "&uacute;")
        testo = Replace(testo, "ù", "&ugrave;")

        formatHtml = testo
    End Function

    Public Shared Function getNomeOperatore(ByVal id_operatore As String) As String

        Dim sqla As String = "SELECT ISNULL(cognome,'') + ' ' + ISNULL(nome,'') FROM operatori WITH(NOLOCK) WHERE id='" & id_operatore & "'"

        Try
            If id_operatore <> "" Then
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

                getNomeOperatore = Cmd.ExecuteScalar & ""

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            Else
                getNomeOperatore = ""
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni getNomeOperatore  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function

    Public Shared Function cliente_is_agenzia_di_viaggio(ByVal id_tipo_cliente As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT agenzia_di_viaggio FROM clienti_tipologia WITH(NOLOCK) WHERE id='" & id_tipo_cliente & "'", Dbc)

        Dim test As Boolean = Cmd.ExecuteScalar

        If test Then
            cliente_is_agenzia_di_viaggio = True
        Else
            cliente_is_agenzia_di_viaggio = False
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function cliente_is_broker(ByVal id_tipo_cliente As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT broker FROM clienti_tipologia WITH(NOLOCK) WHERE id='" & id_tipo_cliente & "'", Dbc)

        Dim test As Boolean = Cmd.ExecuteScalar

        If test Then
            cliente_is_broker = True
        Else
            cliente_is_broker = False
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function is_gps(ByVal id_elemento As String) As Boolean
        'RESTITUISCE 1 SE LA TARIFFA SCELTA E' UNA TARIFFA BROKER - 0 ALTRIMENTI
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(is_gps,'False') FROM condizioni_elementi WITH(NOLOCK) WHERE id='" & id_elemento & "'", Dbc)

        is_gps = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function is_tariffa_broker(ByVal id_tariffe_righe As String) As String

        Try
            'RESTITUISCE 1 SE LA TARIFFA SCELTA E' UNA TARIFFA BROKER - 0 ALTRIMENTI
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT is_broker_prepaid FROM tariffe WITH(NOLOCK) INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa WHERE tariffe_righe.id='" & id_tariffe_righe & "'", Dbc)

            Dim test As Boolean = Cmd.ExecuteScalar

            If test Then
                is_tariffa_broker = "1"
            Else
                is_tariffa_broker = "0"
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("error is_tariffa_broker  : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try


    End Function

    Public Shared Function getDataDb_senza_orario(ByVal data As String, Optional ByVal provenienza As String = "") As String
        If provenienza = "" Then
            provenienza = HttpContext.Current.Request.ServerVariables("HTTP_HOST")
        End If
        If Trim(data) <> "" Then
            'If provenienza = "sviluppoares.sicilyrentcar.it" Or provenienza = "ares.sicilyrentcar.it" Or provenienza = "src-formazione.entermed.it" Then
            '    getDataDb_senza_orario = Year(data) & "-" & Day(data) & "-" & Month(data) & " 00:00:00"
            'Else
            '    getDataDb_senza_orario = Year(data) & "-" & Day(data) & "-" & Month(data) & " 00:00:00"
            'End If

            getDataDb_senza_orario = Year(data) & "-" & Month(data) & "-" & Day(data) & " 00:00:00" 'modificato 20.11.2020

        Else
            getDataDb_senza_orario = ""
        End If
    End Function



    Public Shared Function getDataDb_senza_orario2(ByVal data As String, Optional ByVal provenienza As String = "") As String
        If provenienza = "" Then
            provenienza = HttpContext.Current.Request.ServerVariables("HTTP_HOST")
        End If
        If Trim(data) <> "" Then
            getDataDb_senza_orario2 = Year(data) & "-" & Month(data) & "-" & Day(data) & " 00:00:00"
        Else
            getDataDb_senza_orario2 = ""
        End If
    End Function


    Public Shared Function getDataDb_senza_orario3(ByVal data As String, Optional ByVal provenienza As String = "") As String
        If provenienza = "" Then
            provenienza = HttpContext.Current.Request.ServerVariables("HTTP_HOST")
        End If
        If Trim(data) <> "" Then
            getDataDb_senza_orario3 = Year(data) & "-" & Month(data) & "-" & Day(data) & " 00:00:00" 'modificato 20.11.2020

        Else
            getDataDb_senza_orario3 = ""
        End If
    End Function



    Public Shared Function getDataDb_con_orario(ByVal data As String, Optional ByVal provenienza As String = "") As String
        If provenienza = "" Then
            provenienza = HttpContext.Current.Request.ServerVariables("HTTP_HOST")
        End If

        If Trim(data <> "") Then
            'If provenienza = "sviluppoares.sicilyrentcar.it" Or provenienza = "ares.sicilyrentcar.it" Or provenienza = "src-formazione.entermed.it" Then
            '    getDataDb_con_orario = Year(data) & "-" & Day(data) & "-" & Month(data) & " " & Hour(data) & ":" & Minute(data) & ":" & Second(data)
            'Else
            getDataDb_con_orario = Year(data) & "-" & Month(data) & "-" & Day(data) & " " & Hour(data) & ":" & Minute(data) & ":" & Second(data)
            'End If
        Else
            getDataDb_con_orario = ""
        End If
    End Function
    Public Shared Function getDataDb_con_orario2(ByVal data As String, Optional ByVal provenienza As String = "") As String
        If provenienza = "" Then
            provenienza = HttpContext.Current.Request.ServerVariables("HTTP_HOST")
        End If

        If Trim(data <> "") Then
            getDataDb_con_orario2 = Year(data) & "-" & Month(data) & "-" & Day(data) & " " & Hour(data) & ":" & Minute(data) & ":" & Second(data)
        Else
            getDataDb_con_orario2 = ""
        End If
    End Function

    Public Shared Function getDataDb_orario_senza_data(ByVal data As String, Optional ByVal provenienza As String = "") As String
        If provenienza = "" Then
            provenienza = HttpContext.Current.Request.ServerVariables("HTTP_HOST")
        End If

        If Trim(data <> "") Then
            'If provenienza = "sviluppo.sicilybycar.it" Or provenienza = "src.entermed.it" Then
            '    getDataDb_orario_senza_data = Hour(data) & ":" & Minute(data) & ":" & Second(data)
            'Else
            getDataDb_orario_senza_data = Hour(data) & ":" & Minute(data) & ":" & Second(data)
            'End If
        Else
            getDataDb_orario_senza_data = ""
        End If
    End Function
    Public Shared Function getLivelloAccesso(ByVal idOperatore As String, ByVal idFunzionalita As String) As String
        'DATO L'ID DELL'OPERATORE E L'ID DELLA FUNZIONALITA' QUESTA FUNZIONE RESTITUISCE IL LIVELLO DI ACCESSO
        '1 --> Accesso Negato
        '2 --> Sola lettura
        '3 --> Lettura/Scrittura
        Dim sqla As String = "SELECT id_livello_accesso FROM permessi_operatori WITH(NOLOCK) WHERE id_operatore='" & idOperatore & "' AND id_funzionalita='" & idFunzionalita & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim idLivello As String = Cmd.ExecuteScalar

            If idLivello = "" Then
                'SE NON VIENE TROVATO NULLA NELLA TABELLA RESTITUISCO DI DEFAULT UN "ACCESSO NEGATO"
                getLivelloAccesso = 1
            Else
                getLivelloAccesso = idLivello
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni getLivelloAccesso  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try




    End Function

    Public Shared Sub salvaWarning(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal numero_warning As String, ByVal id_utente As String, ByVal stringa As String)

        'IL TIPO IDENTIFICA DOVE VISUALIZZARE IL WARNING:
        'PICK : warning per stazione di pick up (genera prenotazioni on request)
        'DROP : warning per stazione di drop off (genera prenotazioni on request)
        'PICK INFO : warning informativi stazione di pick up (non genera prenotazioni on request)
        'DROP INFO : warning informativi stazione di drop off (non genera prenotazioni on request)

        Dim tabella As String
        Dim id_da_salvare As String

        If id_preventivo <> "" Then
            id_da_salvare = id_preventivo
            tabella = "preventivi_warning"
        ElseIf id_ribaltamento <> "" Then
            id_da_salvare = id_ribaltamento
            tabella = "ribaltamento_warning"
        ElseIf id_prenotazione <> "" Then
            id_da_salvare = id_prenotazione
            tabella = "prenotazioni_warning"
        ElseIf id_contratto <> "" Then
            id_da_salvare = id_contratto
            tabella = "contratti_warning"
        End If

        Dim testoWarning As String
        Dim tipoWarining As String = ""

        If numero_warning = "1" Then
            If stringa = "" Then
                testoWarning = "Stazione di pick up chiusa per la data di pick up specificata."
            Else
                testoWarning = stringa          'se non è vuota
            End If

            tipoWarining = "PICK"
        ElseIf numero_warning = "1a" Then
            testoWarning = "Stazione di pick up risulta chiusa  " & stringa
            tipoWarining = "PICK"
        ElseIf numero_warning = "1b" Then
            testoWarning = "Fuori orario per stazione di pick up  " & stringa
            tipoWarining = "PICK INFO"
        ElseIf numero_warning = "2" Then
            testoWarning = "Stazione di drop off chiusa per la data di drop off specificata."
            tipoWarining = "DROP"
        ElseIf numero_warning = "2a" Then
            testoWarning = "Stazione di drop off chiusa " & stringa
            tipoWarining = "DROP"
        ElseIf numero_warning = "2b" Then
            testoWarning = "Fuori orario per stazione di drop off " & stringa
            tipoWarining = "DROP INFO"
        ElseIf numero_warning = "3" Then
            testoWarning = "La stazione di pick up non permette VAL verso altre stazioni."
            tipoWarining = "PICK"
        ElseIf numero_warning = "4" Then
            testoWarning = "La stazione di drop off non accetta VAL da altre stazioni."
            tipoWarining = "DROP"
        ElseIf numero_warning = "6" Then
            testoWarning = "La stazione di drop off non accetta auto per il gruppo auto selezionato."
        ElseIf numero_warning = "7" Then
            testoWarning = "La stazione di pick up è in stop sale per il giorno specificato."
            tipoWarining = "PICK"
        ElseIf numero_warning = "8" Then
            testoWarning = "Il gruppo scelto è in stop sale per il giorno specificato."
        ElseIf numero_warning = "9" Then
            testoWarning = "Stazione Pick Up: prenotazione On Request. " & stringa
            tipoWarining = "PICK INFO"
        ElseIf numero_warning = "10" Then
            testoWarning = "Stazione Drop Off: prenotazione On Request. " & stringa
            tipoWarining = "DROP INFO"
        ElseIf numero_warning = "11" Then
            testoWarning = "Cliente in Stop Sale " & stringa
            tipoWarining = "FONTE"
        ElseIf numero_warning = "12" Then
            testoWarning = "Gruppo non vendibile (Pick-Up)."
            tipoWarining = "GRUPPO"
        ElseIf numero_warning = "13" Then
            testoWarning = "Gruppo non vendibile (Età guidatore)."
            tipoWarining = "GRUPPO"
        ElseIf numero_warning = "14" Then
            testoWarning = "Gruppo non vendibile (Drop-Off)."
            tipoWarining = "GRUPPO"
        ElseIf numero_warning = "15" Then
            testoWarning = "VAL non permesso (Drop-Off)."
            tipoWarining = "GRUPPO"
        ElseIf numero_warning = "16" Then
            testoWarning = "Gruppo in Stop Sale."
            tipoWarining = "GRUPPO"
        ElseIf numero_warning = "17" Then
            testoWarning = "Stop sale (Cliente)."
            tipoWarining = "GRUPPO"
        ElseIf numero_warning = "18" Then
            testoWarning = "Tariffa non vendibile per stazione, cliente o data massima di rientro." & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "19" Then
            testoWarning = "Sconto superiore al massimo applicabile."
            tipoWarining = "TARIFFA"
        ElseIf numero_warning = "20" Then
            testoWarning = "Tariffa inesistente. " & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "21a" Then
            testoWarning = "Gruppo auto inesistente. " & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "21b" Then
            testoWarning = "Gruppo auto da consegnare inesistente. " & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "21c" Then
            testoWarning = "Gruppo auto non più valido. " & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "21d" Then
            testoWarning = "Gruppo auto da consegnare non più valido. " & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "22" Then
            testoWarning = "Stazione di uscita inesistente. " & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "23" Then
            testoWarning = "Stazione di rientro inesistente. " & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "24" Then
            testoWarning = "Data Nascita formato non corretto. " & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "25" Then
            testoWarning = "Data Rilascio Patente formato non corretto. " & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "26" Then
            testoWarning = "Totale Importo non congruente con dato ribaltato. " & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "26a" Then
            testoWarning = "Importo Broker non congruente con dato ribaltato. " & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "26b" Then
            testoWarning = "Importo supplementi non congruente con dato ribaltato. " & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "27" Then
            testoWarning = "Codice acessorio non trovato. " & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "28" Then
            testoWarning = "Sconto su tariffa rack superiore al massimo applicabile."
            tipoWarining = "TARIFFA"
        ElseIf numero_warning = "29" Then
            testoWarning = "La tariffa venduta è PREPAGATA - La tariffa applicata è CASH." & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "30" Then
            testoWarning = "La tariffa venduta è CASH - La tariffa applicata è PREPAGATA." & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "31" Then
            testoWarning = "Data Scadenza Patente formato non corretto. " & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "32" Then
            testoWarning = "Prenotazione da cancellare non trovata - N. " & stringa
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "-1dropoff" Then
            testoWarning = "Dati Tabella Orario Stazioni Mancante per Data DropOff - Contattare Amministratore di Sistema"
            tipoWarining = "RIBALTAMENTO"
        ElseIf numero_warning = "-1pickup" Then
            testoWarning = "Dati Tabella Orario Stazioni Mancante per Data PickUp - Contattare Amministratore di Sistema"
            tipoWarining = "RIBALTAMENTO"
        End If
        Dim sqla As String = ""
        'HttpContext.Current.Trace.Write("INSERT INTO " & tabella & " (id_documento, num_calcolo, warning, id_operatore, tipo) VALUES ('" & id_da_salvare & "','" & num_calcolo & "','" & Replace(testoWarning, "'", "''") & "','" & id_utente & "','" & tipoWarining & "')")
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            sqla = "INSERT INTO " & tabella & " (id_documento, num_calcolo, warning, id_operatore, tipo) VALUES ('" & id_da_salvare & "','" & num_calcolo & "','" & Replace(testoWarning, "'", "''") & "','" & id_utente & "','" & tipoWarining & "')"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error salvaWarning funzioni comuni " & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Sub

    Public Shared Function stazione_aperta_pick_up(ByVal id_stazione As String, ByVal data As String, ByVal ore As String, ByVal minuti As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String) As String
        'HttpContext.Current.Response.Write(id_stazione & "</br>" & data & "</br>" & ore & "</br>" & minuti & "</br>")
        'HttpContext.Current.Response.end()

        'QUESTA FUNZIONE CALCOLA SE LA STAZIONE PASSATA E' APERTA COME PICK-UP. IN DETTAGLIO:
        '1) SI CONTROLLA SE LA STAZIONE HA UN ORARIO SALVATO PER LA DATA PASSATA. SE NO LA STAZIONE RISULTA CHIUSA.
        '2) CONTROLLA IL GIORNO PASSATO E' UNO DEI GIORNI FESTIVI SALVATI E SE PER L'ORARIO SELEZIONATO E' APERTA.
        '3) SE NON LO E' SI CONTROLLA NELLA TABELLA DEGLI ORARI NORMALI SE LA STAZIONE E' APERTA.
        '4) SE LA RISPOSTA E' NO NEI CASI 2 e 3 CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI FUORI ORARI PER PICK UP
        '----------------------------------
        'VALORI RESTITUITI
        '0) STAZIONE CHIUSA
        '1) NESSUN TEMPLATE DI ORARIO TROVATO PER LA DATA INDICATA
        '2) STAZIONE APERTA PER PICK-UP (INDISTINTAMENTE ORARIO FESTIVO O ORARIO NORMALE)
        '3) STAZIONE CHIUSA MA ACCETTA PRENOTAZIONI FUORI ORARIO
        '----------------------------------------------------------------------------------------------------------------------------
        'A SECONDA SE VIENE PASSATO UN ID_PREVENTIVO, UN ID_RIBALTAMENTO, UN ID_PRENOTAZIONE, UN ID_CONTRATTO SI SALVA IL RISULTATO NELLE TABELLE OPPORTUNE

        Dim sqla As String
        Dim avviso_orario_stazione As String = "Orario Stazione Mancante - Contattare Amministratore Sistema"

        Try
            Dim id_orario_stazione As String
            Dim accetta_prenotazioni_fuori_orario As Boolean
            Dim Rs As Data.SqlClient.SqlDataReader

            'CONTROLLO SE LA STAZIONE HA UN ORARIO SALVATO PER LA DATA SPECIFICATA (Restituisco 1)
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim anno As String = Year(data)
            Dim dday As String = Day(data)
            Dim mmese As String = Month(data)
            Dim datasql As String = anno & "-" & mmese & "-" & dday & " 00:00:00"

            'originale
            'sqla = "SELECT id_orario FROM stazione_orari WITH(NOLOCK) WHERE convert(datetime, '" & datasql & "',102)  "
            'sqla += "BETWEEN CAST(CAST(da_giorno AS nvarchar(4)) + '/' + CAST(da_mese AS nvarchar(4)) + '/' + '" & anno & "' AS DateTime) "
            'sqla += "And CAST(CAST(a_giorno AS nvarchar(4)) + '/' + CAST(a_mese AS nvarchar(4)) + '/' + '" & anno & "' AS DateTime) AND id_stazione='" & id_stazione & "'"

            sqla = "SELECT id_orario FROM stazione_orari WHERE " & mmese & " >= da_mese and " & mmese & " <= a_mese And " & dday & " >= da_giorno "
            sqla += "And " & dday & " <= a_giorno And id_stazione = " & id_stazione


            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            id_orario_stazione = Cmd.ExecuteScalar & ""

            Dim salvare_warning As Boolean

            If id_prenotazione <> "" Or id_preventivo <> "" Or id_ribaltamento <> "" Or id_contratto <> "" Then
                salvare_warning = True
            Else
                salvare_warning = False
            End If

            If id_orario_stazione = "" Then

                ' 'avviso con msg ed esce da tutto 20.03.2021
                'HttpContext.Current.Response.Write("1- idOrario=" & id_orario_stazione.ToString & "<br/>" & sqla & "<br/>")
                If salvare_warning Then
                    'modificata 20.03.2021
                    salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "1", id_utente, avviso_orario_stazione)
                End If
                stazione_aperta_pick_up = "1"  'NESSUN TEMPLATE DI ORARIO TROVATO PER LA DATA INDICATA (STAZIONE CHIUSA).
            Else
                'SE' E' STATO TROVATO UN ORARIO SETTIMANLE CONTROLLO PER PRIMA COSA SE LA STAZIONE HA ASSOCIATO UN ORARIO FESTIVO (CHE SIA
                'NAZIONALE (id_orario_festivita) O LOCALE (id_orario_festivita_locale) E SE LA DATA E' UN GIORNO FESTIVO IN ESSO SALVATO.
                sqla = "SELECT TOP 1 opening_hour_from,opening_minute_from,opening_hour_to,opening_minute_to,ISNULL(opening_hour_from2,'-1') As opening_hour_from2,"
                sqla += "ISNULL(opening_minute_from2,'-1') As opening_minute_from2, ISNULL(opening_hour_to2,'-1') As opening_hour_to2, "
                sqla += "ISNULL(opening_minute_to2,'-1') As opening_minute_to2 FROM stazioni WITH(NOLOCK) INNER JOIN festivita_orari WITH(NOLOCK) "
                sqla += "ON stazioni.id_orario_festivita=festivita_orari.id Or stazioni.id_orario_festivita_locale = festivita_orari.id INNER JOIN festivita_orari_righe "
                sqla += "WITH(NOLOCK) ON festivita_orari_righe.id_festivita_orari=festivita_orari.id INNER JOIN festivita WITH(NOLOCK) "
                sqla += "ON festivita_orari_righe.id_festivita=festivita.id  "
                sqla += "WHERE stazioni.id='" & id_stazione & "' AND giorno='" & Day(data) & "' AND mese='" & Month(data) & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)

                Rs = Cmd.ExecuteReader()
                Rs.Read()

                If Rs.HasRows() Then
                    Dim orario_di_apertura As String
                    'SE E' STATO TROVATO UN ORARIO FESTIVO ALLORA CONTROLLO CHE L'ORARIO SCELTO DALL'UTENTE E' INTERNO A QUESTO ORARIO
                    If (CInt(ore) > CInt(Rs("opening_hour_from")) And CInt(ore) < CInt(Rs("opening_hour_to"))) Or (CInt(ore) > CInt(Rs("opening_hour_from2")) And CInt(ore) < CInt(Rs("opening_hour_to2"))) Or (CInt(ore) = CInt(Rs("opening_hour_from")) And CInt(minuti) >= Rs("opening_minute_from")) Or (CInt(ore) = CInt(Rs("opening_hour_to")) And CInt(minuti) <= CInt(Rs("opening_minute_to"))) Or (CInt(ore) = CInt(Rs("opening_hour_from2")) And CInt(minuti) >= CInt(Rs("opening_minute_from2"))) Or (CInt(ore) = CInt(Rs("opening_hour_from2")) And CInt(minuti) >= CInt(Rs("opening_minute_from2"))) Or (CInt(ore) = CInt(Rs("opening_hour_to2")) And CInt(minuti) <= CInt(Rs("opening_minute_to2"))) Then
                        stazione_aperta_pick_up = "2" 'STAZIONE APERTA PER PICK UP
                    Else
                        'CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI FUORI ORARIO PER PICK UP
                        orario_di_apertura = " - Orari di apertura:  " & Rs("opening_hour_from") & ":" & Rs("opening_minute_from") & " - " & Rs("opening_hour_to") & ":" & Rs("opening_minute_to")
                        If Rs("opening_hour_from2") <> "-1" Then
                            orario_di_apertura = orario_di_apertura & "   " & Rs("opening_hour_from2") & ":" & Rs("opening_minute_from2") & " - " & Rs("opening_hour_to2") & ":" & Rs("opening_minute_to2")
                        End If
                        Dbc.Close()
                        Dbc.Open()
                        sqla = "SELECT pren_fuori_orario_pickup FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
                        Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                        accetta_prenotazioni_fuori_orario = Cmd.ExecuteScalar
                        If accetta_prenotazioni_fuori_orario Then
                            If salvare_warning Then
                                salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "1b", id_utente, orario_di_apertura)
                            End If
                            stazione_aperta_pick_up = "3"
                        Else
                            If salvare_warning Then
                                salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "1a", id_utente, orario_di_apertura)
                            End If
                            stazione_aperta_pick_up = "0"
                        End If
                    End If
                Else
                    'SE NON E' STATO TROVATO UN ORARIO FESTIVO O NON SIAMO IN UN GIORNO FESTIVO CONTROLLO IL NORMALE ORARIO DI STAZIONE
                    'HO GIA' MEMORIZZATO L'ORARIO SETTIAMANALE APPLICABILE IN QUESTO CASO. CONTROLLO SE PER L'ORARIO SPECIFICATO LA STAZIONE
                    'RISULTA APERTA O CHIUSA
                    Dbc.Close()
                    Dbc.Open()
                    Dim giorno_settimana As String = Weekday(data, FirstDayOfWeek.Monday)
                    sqla = "SELECT opening_hour_from,opening_minute_from,opening_hour_to,opening_minute_to,ISNULL(opening_hour_from2,'-1') As opening_hour_from2,ISNULL(opening_minute_from2,'-1') "
                    sqla += "As opening_minute_from2, ISNULL(opening_hour_to2,'-1') As opening_hour_to2, ISNULL(opening_minute_to2,'-1') As opening_minute_to2 "
                    sqla += "FROM orario_settimanale_righe WITH(NOLOCK) WHERE id_orario_settimanale='" & id_orario_stazione & "' AND '" & giorno_settimana & "' BETWEEN opening_day_from AND opening_day_to"
                    Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                    Rs = Cmd.ExecuteReader()
                    Rs.Read()

                    If Rs.HasRows() Then
                        'SE TROVO UNA RIGA CONTROLLO SE L'ORARIO E' INTERNO O ESTERNO A QUELLO DI APERTURA
                        Dim orario_di_apertura As String
                        If (CInt(ore) > CInt(Rs("opening_hour_from")) And CInt(ore) < CInt(Rs("opening_hour_to"))) Or (CInt(ore) > CInt(Rs("opening_hour_from2")) And CInt(ore) < CInt(Rs("opening_hour_to2"))) Or (CInt(ore) = CInt(Rs("opening_hour_from")) And CInt(minuti) >= Rs("opening_minute_from")) Or (CInt(ore) = CInt(Rs("opening_hour_to")) And CInt(minuti) <= CInt(Rs("opening_minute_to"))) Or (CInt(ore) = CInt(Rs("opening_hour_from2")) And CInt(minuti) >= CInt(Rs("opening_minute_from2"))) Or (CInt(ore) = CInt(Rs("opening_hour_from2")) And CInt(minuti) >= CInt(Rs("opening_minute_from2"))) Or (CInt(ore) = CInt(Rs("opening_hour_to2")) And CInt(minuti) <= CInt(Rs("opening_minute_to2"))) Then
                            stazione_aperta_pick_up = "2"
                        Else
                            'IN QUESTO CASO E' STATO RICHIESTO UN GIORNO DOVE LA STAZIONE RISULTA APERTA MA FUORI DALL'ORARIO DI LAVORO.
                            'CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI FUORI ORARIO
                            orario_di_apertura = " - Orari di apertura:  " & Rs("opening_hour_from") & ":" & Rs("opening_minute_from") & " - " & Rs("opening_hour_to") & ":" & Rs("opening_minute_to")
                            If Rs("opening_hour_from2") <> "-1" Then
                                orario_di_apertura = orario_di_apertura & "   " & Rs("opening_hour_from2") & ":" & Rs("opening_minute_from2") & " - " & Rs("opening_hour_to2") & ":" & Rs("opening_minute_to2")
                            End If
                            Dbc.Close()
                            Dbc.Open()
                            Cmd = New Data.SqlClient.SqlCommand("SELECT pren_fuori_orario_pickup FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'", Dbc)
                            accetta_prenotazioni_fuori_orario = Cmd.ExecuteScalar
                            If accetta_prenotazioni_fuori_orario Then
                                If salvare_warning Then
                                    salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "1b", id_utente, orario_di_apertura)
                                End If
                                stazione_aperta_pick_up = "3"
                            Else
                                If salvare_warning Then
                                    salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "1a", id_utente, orario_di_apertura)
                                End If
                                stazione_aperta_pick_up = "0"
                            End If
                        End If
                    Else
                        'IN QUESTO CASO E' STATO RICHIESTO UN GIORNO DOVE LA STAZIONE RISULTA CHIUSA. CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI
                        'FUORI ORARIO
                        Dbc.Close()
                        Dbc.Open()
                        sqla = "SELECT pren_fuori_orario_pickup FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
                        Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                        accetta_prenotazioni_fuori_orario = Cmd.ExecuteScalar
                        If accetta_prenotazioni_fuori_orario Then
                            If salvare_warning Then
                                salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "1b", id_utente, " - Stazione chiusa per il giorno indicato.")
                            End If
                            stazione_aperta_pick_up = "3"
                        Else
                            If salvare_warning Then
                                ' 'avviso con msg 20.03.2021
                                salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "1", id_utente, avviso_orario_stazione)

                            End If
                            stazione_aperta_pick_up = "0"
                        End If
                    End If
                End If

                Dbc.Close()
                Rs.Close()
                Dbc.Open()
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error  funzioni_comuni stazione_aperta_pick_up" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try





    End Function

    Public Shared Function stazione_aperta_drop_off(ByVal id_stazione As String, ByVal data As String, ByVal ore As String, ByVal minuti As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String) As String
        'QUESTA FUNZIONE CALCOLA SE LA STAZIONE PASSATA E' APERTA COME PICK-UP. IN DETTAGLIO:
        '1) SI CONTROLLA SE LA STAZIONE HA UN ORARIO SALVATO PER LA DATA PASSATA. SE NO LA STAZIONE RISULTA CHIUSA.
        '2) CONTROLLA IL GIORNO PASSATO E' UNO DEI GIORNI FESTIVI SALVATI E SE PER L'ORARIO SELEZIONATO E' APERTA.
        '3) SE NON LO E' SI CONTROLLA NELLA TABELLA DEGLI ORARI NORMALI SE LA STAZIONE E' APERTA.
        '4) SE LA RISPOSTA E' NO NEI CASI 2 e 3 CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI FUORI ORARI PER PICK UP
        '----------------------------------
        'VALORI RESTITUITI
        '0) STAZIONE CHIUSA
        '1) NESSUN TEMPLATE DI ORARIO TROVATO PER LA DATA INDICATA (EQUIVALENTE A STAZIONE CHIUSA MA IDENTIFICA UN POSSIBILE BUCO DI ORARIO NON COMPILATO PER LA STAZIONE)
        '2) STAZIONE APERTA PER DROP-OFF (INDISTINTAMENTE ORARIO FESTIVO O ORARIO NORMALE)
        '3) STAZIONE CHIUSA MA ACCETTA PRENOTAZIONI FUORI ORARIO
        Dim sqla As String = ""
        'aggiunto 20.03.2021
        Dim avviso_orario_stazione As String = "Orario Stazione Mancante - Contattare Amministratore Sistema"


        Try
            Dim id_orario_stazione As String
            Dim accetta_prenotazioni_fuori_orario As Boolean

            Dim salvare_warning As Boolean

            If id_prenotazione <> "" Or id_preventivo <> "" Or id_ribaltamento <> "" Or id_contratto <> "" Then
                salvare_warning = True
            Else
                salvare_warning = False
            End If

            Dim Rs As Data.SqlClient.SqlDataReader

            'CONTROLLO SE LA STAZIONE HA UN ORARIO SALVATO PER LA DATA SPECIFICATA (Restitusico 1)
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim anno As String = Year(data)
            Dim dd As String = Day(data)
            Dim mm As String = Month(data)
            Dim datasql As String = anno & "-" & mm & "-" & dd & " 00:00:00"

            'sqla = "SELECT id_orario FROM stazione_orari WITH(NOLOCK) WHERE convert(datetime, '" & datasql & "',102)  "
            'sqla += "BETWEEN CAST(CAST(da_giorno AS nvarchar(4)) + '/' + CAST(da_mese AS nvarchar(4)) + '/' + '" & anno & "' AS DateTime) "
            'sqla += "And CAST(CAST(a_giorno AS nvarchar(4)) + '/' + CAST(a_mese AS nvarchar(4)) + '/' + '" & anno & "' AS DateTime) AND id_stazione='" & id_stazione & "'"

            sqla = "SELECT id_orario FROM stazione_orari WHERE " & mm & " >= da_mese and " & mm & " <= a_mese And " & dd & " >= da_giorno "
            sqla += "And " & dd & " <= a_giorno And id_stazione = " & id_stazione




            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            id_orario_stazione = Cmd.ExecuteScalar & ""

            If id_orario_stazione = "" Then
                If salvare_warning Then
                    'HttpContext.Current.Response.Write("2-1- idOrario=" & id_orario_stazione.ToString & "<br/>" & sqla & "<br/>")
                    'modificato 20.03.2021 da 1 a -1dropoff
                    salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "1", id_utente, avviso_orario_stazione)
                End If
                stazione_aperta_drop_off = "1"  'NESSUN TEMPLATE DI ORARIO TROVATO PER LA DATA INDICATA (STAZIONE CHIUSA).
            Else
                'SE' E' STATO TROVATO UN ORARIO SETTIMANLE CONTROLLO PER PRIMA COSA SE LA STAZIONE HA ASSOCIATO UN ORARIO FESTIVO E SE LA DATA
                'E' UN GIORNO FESTIVO IN ESSO SALVATO.
                sqla = "SELECT opening_hour_from,opening_minute_from,opening_hour_to,opening_minute_to,ISNULL(opening_hour_from2,'-1') As opening_hour_from2,"
                sqla += "ISNULL(opening_minute_from2,'-1') As opening_minute_from2, ISNULL(opening_hour_to2,'-1') As opening_hour_to2, "
                sqla += "ISNULL(opening_minute_to2,'-1') As opening_minute_to2 FROM stazioni WITH(NOLOCK) INNER JOIN festivita_orari WITH(NOLOCK) "
                sqla += "ON stazioni.id_orario_festivita=festivita_orari.id INNER JOIN festivita_orari_righe WITH(NOLOCK) ON festivita_orari_righe.id_festivita_orari=festivita_orari.id "
                sqla += "INNER JOIN festivita WITH(NOLOCK) ON festivita_orari_righe.id_festivita=festivita.id  WHERE stazioni.id='" & id_stazione & "' "
                sqla += "AND giorno='" & Day(data) & "' AND mese='" & Month(data) & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)

                Rs = Cmd.ExecuteReader()
                Rs.Read()

                If Rs.HasRows() Then
                    Dim orario_di_apertura As String
                    'SE E' STATO TROVATO UN ORARIO FESTIVO ALLORA CONTROLLO CHE L'ORARIO SCELTO DALL'UTENTE E' INTERNO A QUESTO ORARIO
                    If (CInt(ore) > CInt(Rs("opening_hour_from")) And CInt(ore) < CInt(Rs("opening_hour_to"))) Or (CInt(ore) > CInt(Rs("opening_hour_from2")) And CInt(ore) < CInt(Rs("opening_hour_to2"))) Or (CInt(ore) = CInt(Rs("opening_hour_from")) And CInt(minuti) >= Rs("opening_minute_from")) Or (CInt(ore) = CInt(Rs("opening_hour_to")) And CInt(minuti) <= CInt(Rs("opening_minute_to"))) Or (CInt(ore) = CInt(Rs("opening_hour_from2")) And CInt(minuti) >= CInt(Rs("opening_minute_from2"))) Or (CInt(ore) = CInt(Rs("opening_hour_from2")) And CInt(minuti) >= CInt(Rs("opening_minute_from2"))) Or (CInt(ore) = CInt(Rs("opening_hour_to2")) And CInt(minuti) <= CInt(Rs("opening_minute_to2"))) Then
                        stazione_aperta_drop_off = "2" 'STAZIONE APERTA PER PICK UP
                    Else
                        'CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI FUORI ORARIO PER PICK UP
                        orario_di_apertura = " - Orari di apertura:  " & Rs("opening_hour_from") & ":" & Rs("opening_minute_from") & " - " & Rs("opening_hour_to") & ":" & Rs("opening_minute_to")
                        If Rs("opening_hour_from2") <> "-1" Then
                            orario_di_apertura = orario_di_apertura & "   " & Rs("opening_hour_from2") & ":" & Rs("opening_minute_from2") & " - " & Rs("opening_hour_to2") & ":" & Rs("opening_minute_to2")
                        End If
                        Dbc.Close()
                        Dbc.Open()
                        sqla = "SELECT pren_fuori_orario_dropoff FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
                        Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                        accetta_prenotazioni_fuori_orario = Cmd.ExecuteScalar
                        If accetta_prenotazioni_fuori_orario Then
                            If salvare_warning Then
                                salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "2b", id_utente, orario_di_apertura)
                            End If

                            stazione_aperta_drop_off = "3"
                        Else
                            If salvare_warning Then
                                salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "2a", id_utente, orario_di_apertura)
                            End If

                            stazione_aperta_drop_off = "0"
                        End If
                    End If
                Else
                    'SE NON E' STATO TROVATO UN ORARIO FESTIVO O NON SIAMO IN UN GIORNO FESTIVO CONTROLLO IL NORMALE ORARIO DI STAZIONE
                    'HO GIA' MEMORIZZATO L'ORARIO SETTIAMANALE APPLICABILE IN QUESTO CASO. CONTROLLO SE PER L'ORARIO SPECIFICATO LA STAZIONE
                    'RISULTA APERTA O CHIUSA
                    Dbc.Close()
                    Dbc.Open()
                    Dim giorno_settimana As String = Weekday(data, FirstDayOfWeek.Monday)
                    sqla = "SELECT opening_hour_from,opening_minute_from,opening_hour_to,opening_minute_to,ISNULL(opening_hour_from2,'-1') As opening_hour_from2,"
                    sqla += "ISNULL(opening_minute_from2,'-1') As opening_minute_from2, ISNULL(opening_hour_to2,'-1') As opening_hour_to2, "
                    sqla += "ISNULL(opening_minute_to2,'-1') As opening_minute_to2 FROM orario_settimanale_righe WITH(NOLOCK) WHERE id_orario_settimanale='" & id_orario_stazione & "' "
                    sqla += "AND '" & giorno_settimana & "' BETWEEN opening_day_from AND opening_day_to"
                    Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                    Rs = Cmd.ExecuteReader()
                    Rs.Read()

                    If Rs.HasRows() Then
                        'SE TROVO UNA RIGA CONTROLLO SE L'ORARIO E' INTERNO O ESTERNO A QUELLO DI APERTURA
                        Dim orario_di_apertura As String
                        If (CInt(ore) > CInt(Rs("opening_hour_from")) And CInt(ore) < CInt(Rs("opening_hour_to"))) Or (CInt(ore) > CInt(Rs("opening_hour_from2")) And CInt(ore) < CInt(Rs("opening_hour_to2"))) Or (CInt(ore) = CInt(Rs("opening_hour_from")) And CInt(minuti) >= Rs("opening_minute_from")) Or (CInt(ore) = CInt(Rs("opening_hour_to")) And CInt(minuti) <= CInt(Rs("opening_minute_to"))) Or (CInt(ore) = CInt(Rs("opening_hour_from2")) And CInt(minuti) >= CInt(Rs("opening_minute_from2"))) Or (CInt(ore) = CInt(Rs("opening_hour_from2")) And CInt(minuti) >= CInt(Rs("opening_minute_from2"))) Or (CInt(ore) = CInt(Rs("opening_hour_to2")) And CInt(minuti) <= CInt(Rs("opening_minute_to2"))) Then
                            stazione_aperta_drop_off = "2"
                        Else
                            'IN QUESTO CASO E' STATO RICHIESTO UN GIORNO DOVE LA STAZIONE RISULTA APERTA MA FUORI DALL'ORARIO DI LAVORO.
                            'CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI FUORI ORARIO
                            orario_di_apertura = " - Orari di apertura:  " & Rs("opening_hour_from") & ":" & Rs("opening_minute_from") & " - " & Rs("opening_hour_to") & ":" & Rs("opening_minute_to")
                            If Rs("opening_hour_from2") <> "-1" Then
                                orario_di_apertura = orario_di_apertura & "   " & Rs("opening_hour_from2") & ":" & Rs("opening_minute_from2") & " - " & Rs("opening_hour_to2") & ":" & Rs("opening_minute_to2")
                            End If
                            Dbc.Close()
                            Dbc.Open()
                            sqla = "SELECT pren_fuori_orario_dropoff FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
                            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                            accetta_prenotazioni_fuori_orario = Cmd.ExecuteScalar
                            If accetta_prenotazioni_fuori_orario Then
                                If salvare_warning Then
                                    salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "2b", id_utente, orario_di_apertura)
                                End If

                                stazione_aperta_drop_off = "3"
                            Else
                                If salvare_warning Then
                                    salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "2a", id_utente, orario_di_apertura)
                                End If

                                stazione_aperta_drop_off = "0"
                            End If
                        End If
                    Else
                        'IN QUESTO CASO E' STATO RICHIESTO UN GIORNO DOVE LA STAZIONE RISULTA CHIUSA. CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI
                        'FUORI ORARIO
                        Dbc.Close()
                        Dbc.Open()
                        sqla = "SELECT pren_fuori_orario_dropoff FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
                        Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                        accetta_prenotazioni_fuori_orario = Cmd.ExecuteScalar
                        If accetta_prenotazioni_fuori_orario Then
                            If salvare_warning Then
                                salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "2b", id_utente, " - Stazione chiusa per il giorno indicato.")
                            End If

                            stazione_aperta_drop_off = "3"
                        Else
                            If salvare_warning Then
                                HttpContext.Current.Response.Write("2-2- idOrario=" & id_orario_stazione.ToString & "<br/>" & sqla & "<br/>")
                                salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "2", id_utente, "")
                            End If

                            stazione_aperta_drop_off = "0"
                        End If
                    End If
                End If

                Dbc.Close()
                Rs.Close()
                Dbc.Open()
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error  funzioni_comuni stazione_aperta_drop_off : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function

    Public Shared Function stazione_aperta_pick_upWebSevice(ByVal id_stazione As String, ByVal data As String, ByVal ore As String, ByVal minuti As String) As String
        'QUESTA FUNZIONE CALCOLA SE LA STAZIONE PASSATA E' APERTA COME PICK-UP. IN DETTAGLIO:
        '1) SI CONTROLLA SE LA STAZIONE HA UN ORARIO SALVATO PER LA DATA PASSATA. SE NO LA STAZIONE RISULTA CHIUSA.
        '2) CONTROLLA IL GIORNO PASSATO E' UNO DEI GIORNI FESTIVI SALVATI E SE PER L'ORARIO SELEZIONATO E' APERTA.
        '3) SE NON LO E' SI CONTROLLA NELLA TABELLA DEGLI ORARI NORMALI SE LA STAZIONE E' APERTA.
        '4) SE LA RISPOSTA E' NO NEI CASI 2 e 3 CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI FUORI ORARI PER PICK UP
        '----------------------------------
        'VALORI RESTITUITI
        '0) STAZIONE CHIUSA
        '1) NESSUN TEMPLATE DI ORARIO TROVATO PER LA DATA INDICATA
        '2) STAZIONE APERTA PER PICK-UP (INDISTINTAMENTE ORARIO FESTIVO O ORARIO NORMALE)
        '3) STAZIONE CHIUSA MA ACCETTA PRENOTAZIONI FUORI ORARIO
        '----------------------------------------------------------------------------------------------------------------------------
        'A SECONDA SE VIENE PASSATO UN ID_PREVENTIVO, UN ID_RIBALTAMENTO, UN ID_PRENOTAZIONE, UN ID_CONTRATTO SI SALVA IL RISULTATO NELLE TABELLE OPPORTUNE
        Dim sqla As String = ""


        Try
            Dim id_orario_stazione As String
            Dim accetta_prenotazioni_fuori_orario As Boolean
            Dim Rs As Data.SqlClient.SqlDataReader

            'CONTROLLO SE LA STAZIONE HA UN ORARIO SALVATO PER LA DATA SPECIFICATA (Restituisco 1)
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim anno As String = Year(data)
            Dim dday As String = Day(data)
            Dim mmese As String = Month(data)

            'sqla = "SELECT id_orario FROM stazione_orari WITH(NOLOCK) WHERE convert(datetime, '" & datasql & "',102)  "
            'sqla += "BETWEEN CAST(CAST(da_giorno AS nvarchar(4)) + '/' + CAST(da_mese AS nvarchar(4)) + '/' + '" & anno & "' AS DateTime) "
            'sqla += "And CAST(CAST(a_giorno AS nvarchar(4)) + '/' + CAST(a_mese AS nvarchar(4)) + '/' + '" & anno & "' AS DateTime) AND id_stazione='" & id_stazione & "'"

            sqla = "SELECT id_orario FROM stazione_orari WHERE " & mmese & " >= da_mese and " & mmese & " <= a_mese And " & dday & " >= da_giorno "
            sqla += "And " & dday & " <= a_giorno And id_stazione = " & id_stazione


            '"SELECT id_orario FROM stazione_orari WITH(NOLOCK) WHERE (CAST('" & data & "' AS DateTime) BETWEEN CAST(CAST(da_giorno AS nvarchar(4)) + '/' + CAST(da_mese AS nvarchar(4)) + '/' + '" & anno & "' AS DateTime) AND CAST(CAST(a_giorno AS nvarchar(4)) + '/' + CAST(a_mese AS nvarchar(4)) + '/' + '" & anno & "' AS DateTime)) AND id_stazione='" & id_stazione & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            id_orario_stazione = Cmd.ExecuteScalar & ""


            If id_orario_stazione = "" Then

                stazione_aperta_pick_upWebSevice = "1"  'NESSUN TEMPLATE DI ORARIO TROVATO PER LA DATA INDICATA (STAZIONE CHIUSA).

            Else
                'SE' E' STATO TROVATO UN ORARIO SETTIMANLE CONTROLLO PER PRIMA COSA SE LA STAZIONE HA ASSOCIATO UN ORARIO FESTIVO (CHE SIA
                'NAZIONALE (id_orario_festivita) O LOCALE (id_orario_festivita_locale) E SE LA DATA E' UN GIORNO FESTIVO IN ESSO SALVATO.
                sqla = "SELECT TOP 1 opening_hour_from,opening_minute_from,opening_hour_to,opening_minute_to,ISNULL(opening_hour_from2,'-1') As opening_hour_from2,"
                sqla += "ISNULL(opening_minute_from2,'-1') As opening_minute_from2, ISNULL(opening_hour_to2,'-1') As opening_hour_to2, "
                sqla += "ISNULL(opening_minute_to2,'-1') As opening_minute_to2 FROM stazioni WITH(NOLOCK) INNER JOIN festivita_orari WITH(NOLOCK) "
                sqla += "ON stazioni.id_orario_festivita=festivita_orari.id OR stazioni.id_orario_festivita_locale = festivita_orari.id "
                sqla += "INNER JOIN festivita_orari_righe WITH(NOLOCK) ON festivita_orari_righe.id_festivita_orari=festivita_orari.id INNER JOIN festivita WITH(NOLOCK) "
                sqla += "ON festivita_orari_righe.id_festivita=festivita.id  WHERE stazioni.id='" & id_stazione & "' AND giorno='" & Day(data) & "' AND mese='" & Month(data) & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)

                Rs = Cmd.ExecuteReader()
                Rs.Read()

                If Rs.HasRows() Then
                    Dim orario_di_apertura As String
                    'SE E' STATO TROVATO UN ORARIO FESTIVO ALLORA CONTROLLO CHE L'ORARIO SCELTO DALL'UTENTE E' INTERNO A QUESTO ORARIO
                    If (CInt(ore) > CInt(Rs("opening_hour_from")) And CInt(ore) < CInt(Rs("opening_hour_to"))) Or (CInt(ore) > CInt(Rs("opening_hour_from2")) And CInt(ore) < CInt(Rs("opening_hour_to2"))) Or (CInt(ore) = CInt(Rs("opening_hour_from")) And CInt(minuti) >= Rs("opening_minute_from")) Or (CInt(ore) = CInt(Rs("opening_hour_to")) And CInt(minuti) <= CInt(Rs("opening_minute_to"))) Or (CInt(ore) = CInt(Rs("opening_hour_from2")) And CInt(minuti) >= CInt(Rs("opening_minute_from2"))) Or (CInt(ore) = CInt(Rs("opening_hour_from2")) And CInt(minuti) >= CInt(Rs("opening_minute_from2"))) Or (CInt(ore) = CInt(Rs("opening_hour_to2")) And CInt(minuti) <= CInt(Rs("opening_minute_to2"))) Then
                        stazione_aperta_pick_upWebSevice = "2" 'STAZIONE APERTA PER PICK UP
                    Else
                        'CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI FUORI ORARIO PER PICK UP
                        orario_di_apertura = " - Orari di apertura:  " & Rs("opening_hour_from") & ":" & Rs("opening_minute_from") & " - " & Rs("opening_hour_to") & ":" & Rs("opening_minute_to")
                        If Rs("opening_hour_from2") <> "-1" Then
                            orario_di_apertura = orario_di_apertura & "   " & Rs("opening_hour_from2") & ":" & Rs("opening_minute_from2") & " - " & Rs("opening_hour_to2") & ":" & Rs("opening_minute_to2")
                        End If
                        Dbc.Close()
                        Dbc.Open()
                        sqla = "SELECT pren_fuori_orario_pickup FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
                        Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                        accetta_prenotazioni_fuori_orario = Cmd.ExecuteScalar
                        If accetta_prenotazioni_fuori_orario Then

                            stazione_aperta_pick_upWebSevice = "3"
                        Else

                            stazione_aperta_pick_upWebSevice = "0"
                        End If
                    End If
                Else
                    'SE NON E' STATO TROVATO UN ORARIO FESTIVO O NON SIAMO IN UN GIORNO FESTIVO CONTROLLO IL NORMALE ORARIO DI STAZIONE
                    'HO GIA' MEMORIZZATO L'ORARIO SETTIAMANALE APPLICABILE IN QUESTO CASO. CONTROLLO SE PER L'ORARIO SPECIFICATO LA STAZIONE
                    'RISULTA APERTA O CHIUSA
                    Dbc.Close()
                    Dbc.Open()
                    Dim giorno_settimana As String = Weekday(data, FirstDayOfWeek.Monday)
                    sqla = "SELECT opening_hour_from,opening_minute_from,opening_hour_to,opening_minute_to,ISNULL(opening_hour_from2,'-1') As opening_hour_from2,"
                    sqla += "ISNULL(opening_minute_from2,'-1') As opening_minute_from2, ISNULL(opening_hour_to2,'-1') As opening_hour_to2, ISNULL(opening_minute_to2,'-1') "
                    sqla += "As opening_minute_to2 FROM orario_settimanale_righe WITH(NOLOCK) WHERE id_orario_settimanale='" & id_orario_stazione & "' "
                    sqla += "And '" & giorno_settimana & "' BETWEEN opening_day_from AND opening_day_to"

                    Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                    Rs = Cmd.ExecuteReader()
                    Rs.Read()

                    If Rs.HasRows() Then
                        'SE TROVO UNA RIGA CONTROLLO SE L'ORARIO E' INTERNO O ESTERNO A QUELLO DI APERTURA
                        Dim orario_di_apertura As String
                        If (CInt(ore) > CInt(Rs("opening_hour_from")) And CInt(ore) < CInt(Rs("opening_hour_to"))) Or (CInt(ore) > CInt(Rs("opening_hour_from2")) And CInt(ore) < CInt(Rs("opening_hour_to2"))) Or (CInt(ore) = CInt(Rs("opening_hour_from")) And CInt(minuti) >= Rs("opening_minute_from")) Or (CInt(ore) = CInt(Rs("opening_hour_to")) And CInt(minuti) <= CInt(Rs("opening_minute_to"))) Or (CInt(ore) = CInt(Rs("opening_hour_from2")) And CInt(minuti) >= CInt(Rs("opening_minute_from2"))) Or (CInt(ore) = CInt(Rs("opening_hour_from2")) And CInt(minuti) >= CInt(Rs("opening_minute_from2"))) Or (CInt(ore) = CInt(Rs("opening_hour_to2")) And CInt(minuti) <= CInt(Rs("opening_minute_to2"))) Then
                            stazione_aperta_pick_upWebSevice = "2"
                        Else
                            'IN QUESTO CASO E' STATO RICHIESTO UN GIORNO DOVE LA STAZIONE RISULTA APERTA MA FUORI DALL'ORARIO DI LAVORO.
                            'CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI FUORI ORARIO
                            orario_di_apertura = " - Orari di apertura:  " & Rs("opening_hour_from") & ":" & Rs("opening_minute_from") & " - " & Rs("opening_hour_to") & ":" & Rs("opening_minute_to")
                            If Rs("opening_hour_from2") <> "-1" Then
                                orario_di_apertura = orario_di_apertura & "   " & Rs("opening_hour_from2") & ":" & Rs("opening_minute_from2") & " - " & Rs("opening_hour_to2") & ":" & Rs("opening_minute_to2")
                            End If
                            Dbc.Close()
                            Dbc.Open()
                            sqla = "SELECT pren_fuori_orario_pickup FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
                            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                            accetta_prenotazioni_fuori_orario = Cmd.ExecuteScalar
                            If accetta_prenotazioni_fuori_orario Then

                                stazione_aperta_pick_upWebSevice = "3"
                            Else

                                stazione_aperta_pick_upWebSevice = "0"
                            End If
                        End If
                    Else
                        'IN QUESTO CASO E' STATO RICHIESTO UN GIORNO DOVE LA STAZIONE RISULTA CHIUSA. CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI
                        'FUORI ORARIO
                        Dbc.Close()
                        Dbc.Open()
                        sqla = "SELECT pren_fuori_orario_pickup FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
                        Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                        accetta_prenotazioni_fuori_orario = Cmd.ExecuteScalar
                        If accetta_prenotazioni_fuori_orario Then
                            stazione_aperta_pick_upWebSevice = "3"
                        Else
                            stazione_aperta_pick_upWebSevice = "0"
                        End If
                    End If
                End If

                Dbc.Close()
                Rs.Close()
                Dbc.Open()
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni stazione_aperta_pick_upWebSevice  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try



    End Function

    Public Shared Function stazione_aperta_drop_offWebSevice(ByVal id_stazione As String, ByVal data As String, ByVal ore As String, ByVal minuti As String) As String
        'QUESTA FUNZIONE CALCOLA SE LA STAZIONE PASSATA E' APERTA COME PICK-UP. IN DETTAGLIO:
        '1) SI CONTROLLA SE LA STAZIONE HA UN ORARIO SALVATO PER LA DATA PASSATA. SE NO LA STAZIONE RISULTA CHIUSA.
        '2) CONTROLLA IL GIORNO PASSATO E' UNO DEI GIORNI FESTIVI SALVATI E SE PER L'ORARIO SELEZIONATO E' APERTA.
        '3) SE NON LO E' SI CONTROLLA NELLA TABELLA DEGLI ORARI NORMALI SE LA STAZIONE E' APERTA.
        '4) SE LA RISPOSTA E' NO NEI CASI 2 e 3 CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI FUORI ORARI PER PICK UP
        '----------------------------------
        'VALORI RESTITUITI
        '0) STAZIONE CHIUSA
        '1) NESSUN TEMPLATE DI ORARIO TROVATO PER LA DATA INDICATA (EQUIVALENTE A STAZIONE CHIUSA MA IDENTIFICA UN POSSIBILE BUCO DI ORARIO NON COMPILATO PER LA STAZIONE)
        '2) STAZIONE APERTA PER DROP-OFF (INDISTINTAMENTE ORARIO FESTIVO O ORARIO NORMALE)
        '3) STAZIONE CHIUSA MA ACCETTA PRENOTAZIONI FUORI ORARIO
        Dim sqla As String
        Dim Rs As Data.SqlClient.SqlDataReader
        Dim id_orario_stazione As String
        Dim accetta_prenotazioni_fuori_orario As Boolean

        Try


            'CONTROLLO SE LA STAZIONE HA UN ORARIO SALVATO PER LA DATA SPECIFICATA (Restitusico 1)
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim anno As String = Year(data)
            Dim dday As String = Day(data)
            Dim mmese As String = Month(data)
            Dim datasql As String = anno & "-" & mmese & "-" & dday & " 00:00:00"
            '"SELECT id_orario FROM stazione_orari WITH(NOLOCK) WHERE (" & Day(data) & " BETWEEN da_giorno and a_giorno) AND (" & Month(data) & " between da_mese and a_mese) AND id_stazione='" & id_stazione & "'"

            sqla = "SELECT id_orario FROM stazione_orari WHERE " & mmese & " >= da_mese and " & mmese & " <= a_mese And " & dday & " >= da_giorno "
            sqla += "And " & dday & " <= a_giorno And id_stazione = " & id_stazione


            '"SELECT id_orario FROM stazione_orari WITH(NOLOCK) WHERE (CAST('" & data & "' AS DateTime) BETWEEN CAST(CAST(da_giorno AS nvarchar(4)) + '/' + CAST(da_mese AS nvarchar(4)) + '/' + '" & anno & "' AS DateTime) AND CAST(CAST(a_giorno AS nvarchar(4)) + '/' + CAST(a_mese AS nvarchar(4)) + '/' + '" & anno & "' AS DateTime)) AND id_stazione='" & id_stazione & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)


            id_orario_stazione = Cmd.ExecuteScalar & ""

            If id_orario_stazione = "" Then
                stazione_aperta_drop_offWebSevice = "1"  'NESSUN TEMPLATE DI ORARIO TROVATO PER LA DATA INDICATA (STAZIONE CHIUSA).
            Else
                'SE' E' STATO TROVATO UN ORARIO SETTIMANLE CONTROLLO PER PRIMA COSA SE LA STAZIONE HA ASSOCIATO UN ORARIO FESTIVO E SE LA DATA
                'E' UN GIORNO FESTIVO IN ESSO SALVATO.
                sqla = "SELECT opening_hour_from,opening_minute_from,opening_hour_to,opening_minute_to,ISNULL(opening_hour_from2,'-1') As opening_hour_from2,"
                sqla += "ISNULL(opening_minute_from2,'-1') As opening_minute_from2, ISNULL(opening_hour_to2,'-1') As opening_hour_to2, ISNULL(opening_minute_to2,'-1') "
                sqla += "As opening_minute_to2 FROM stazioni WITH(NOLOCK) INNER JOIN festivita_orari WITH(NOLOCK) ON stazioni.id_orario_festivita=festivita_orari.id "
                sqla += "INNER JOIN festivita_orari_righe WITH(NOLOCK) ON festivita_orari_righe.id_festivita_orari=festivita_orari.id INNER JOIN festivita WITH(NOLOCK) "
                sqla += "ON festivita_orari_righe.id_festivita=festivita.id  WHERE stazioni.id='" & id_stazione & "' AND giorno='" & Day(data) & "' AND mese='" & Month(data) & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)

                Rs = Cmd.ExecuteReader()
                Rs.Read()

                If Rs.HasRows() Then
                    Dim orario_di_apertura As String
                    'SE E' STATO TROVATO UN ORARIO FESTIVO ALLORA CONTROLLO CHE L'ORARIO SCELTO DALL'UTENTE E' INTERNO A QUESTO ORARIO
                    If (CInt(ore) > CInt(Rs("opening_hour_from")) And CInt(ore) < CInt(Rs("opening_hour_to"))) Or (CInt(ore) > CInt(Rs("opening_hour_from2")) And CInt(ore) < CInt(Rs("opening_hour_to2"))) Or (CInt(ore) = CInt(Rs("opening_hour_from")) And CInt(minuti) >= Rs("opening_minute_from")) Or (CInt(ore) = CInt(Rs("opening_hour_to")) And CInt(minuti) <= CInt(Rs("opening_minute_to"))) Or (CInt(ore) = CInt(Rs("opening_hour_from2")) And CInt(minuti) >= CInt(Rs("opening_minute_from2"))) Or (CInt(ore) = CInt(Rs("opening_hour_from2")) And CInt(minuti) >= CInt(Rs("opening_minute_from2"))) Or (CInt(ore) = CInt(Rs("opening_hour_to2")) And CInt(minuti) <= CInt(Rs("opening_minute_to2"))) Then
                        stazione_aperta_drop_offWebSevice = "2" 'STAZIONE APERTA PER PICK UP
                    Else
                        'CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI FUORI ORARIO PER PICK UP
                        orario_di_apertura = " - Orari di apertura:  " & Rs("opening_hour_from") & ":" & Rs("opening_minute_from") & " - " & Rs("opening_hour_to") & ":" & Rs("opening_minute_to")
                        If Rs("opening_hour_from2") <> "-1" Then
                            orario_di_apertura = orario_di_apertura & "   " & Rs("opening_hour_from2") & ":" & Rs("opening_minute_from2") & " - " & Rs("opening_hour_to2") & ":" & Rs("opening_minute_to2")
                        End If
                        Dbc.Close()
                        Dbc.Open()
                        sqla = "SELECT pren_fuori_orario_dropoff FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
                        Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                        accetta_prenotazioni_fuori_orario = Cmd.ExecuteScalar
                        If accetta_prenotazioni_fuori_orario Then
                            stazione_aperta_drop_offWebSevice = "3"
                        Else
                            stazione_aperta_drop_offWebSevice = "0"
                        End If
                    End If
                Else
                    'SE NON E' STATO TROVATO UN ORARIO FESTIVO O NON SIAMO IN UN GIORNO FESTIVO CONTROLLO IL NORMALE ORARIO DI STAZIONE
                    'HO GIA' MEMORIZZATO L'ORARIO SETTIAMANALE APPLICABILE IN QUESTO CASO. CONTROLLO SE PER L'ORARIO SPECIFICATO LA STAZIONE
                    'RISULTA APERTA O CHIUSA
                    Dbc.Close()
                    Dbc.Open()
                    Dim giorno_settimana As String = Weekday(data, FirstDayOfWeek.Monday)
                    sqla = "SELECT opening_hour_from,opening_minute_from,opening_hour_to,opening_minute_to,ISNULL(opening_hour_from2,'-1') As opening_hour_from2,ISNULL(opening_minute_from2,'-1') "
                    sqla += "As opening_minute_from2, ISNULL(opening_hour_to2,'-1') As opening_hour_to2, ISNULL(opening_minute_to2,'-1') As opening_minute_to2 "
                    sqla += "FROM orario_settimanale_righe WITH(NOLOCK) "
                    sqla += "WHERE id_orario_settimanale='" & id_orario_stazione & "' AND '" & giorno_settimana & "' BETWEEN opening_day_from AND opening_day_to"
                    Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                    Rs = Cmd.ExecuteReader()
                    Rs.Read()

                    If Rs.HasRows() Then
                        'SE TROVO UNA RIGA CONTROLLO SE L'ORARIO E' INTERNO O ESTERNO A QUELLO DI APERTURA
                        Dim orario_di_apertura As String
                        If (CInt(ore) > CInt(Rs("opening_hour_from")) And CInt(ore) < CInt(Rs("opening_hour_to"))) Or (CInt(ore) > CInt(Rs("opening_hour_from2")) And CInt(ore) < CInt(Rs("opening_hour_to2"))) Or (CInt(ore) = CInt(Rs("opening_hour_from")) And CInt(minuti) >= Rs("opening_minute_from")) Or (CInt(ore) = CInt(Rs("opening_hour_to")) And CInt(minuti) <= CInt(Rs("opening_minute_to"))) Or (CInt(ore) = CInt(Rs("opening_hour_from2")) And CInt(minuti) >= CInt(Rs("opening_minute_from2"))) Or (CInt(ore) = CInt(Rs("opening_hour_from2")) And CInt(minuti) >= CInt(Rs("opening_minute_from2"))) Or (CInt(ore) = CInt(Rs("opening_hour_to2")) And CInt(minuti) <= CInt(Rs("opening_minute_to2"))) Then
                            stazione_aperta_drop_offWebSevice = "2"
                        Else
                            'IN QUESTO CASO E' STATO RICHIESTO UN GIORNO DOVE LA STAZIONE RISULTA APERTA MA FUORI DALL'ORARIO DI LAVORO.
                            'CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI FUORI ORARIO
                            orario_di_apertura = " - Orari di apertura:  " & Rs("opening_hour_from") & ":" & Rs("opening_minute_from") & " - " & Rs("opening_hour_to") & ":" & Rs("opening_minute_to")
                            If Rs("opening_hour_from2") <> "-1" Then
                                orario_di_apertura = orario_di_apertura & "   " & Rs("opening_hour_from2") & ":" & Rs("opening_minute_from2") & " - " & Rs("opening_hour_to2") & ":" & Rs("opening_minute_to2")
                            End If
                            Dbc.Close()
                            Dbc.Open()
                            sqla = "SELECT pren_fuori_orario_dropoff FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
                            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                            accetta_prenotazioni_fuori_orario = Cmd.ExecuteScalar
                            If accetta_prenotazioni_fuori_orario Then
                                stazione_aperta_drop_offWebSevice = "3"
                            Else
                                stazione_aperta_drop_offWebSevice = "0"
                            End If
                        End If
                    Else
                        'IN QUESTO CASO E' STATO RICHIESTO UN GIORNO DOVE LA STAZIONE RISULTA CHIUSA. CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI
                        'FUORI ORARIO
                        Dbc.Close()
                        Dbc.Open()
                        sqla = "SELECT pren_fuori_orario_dropoff FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
                        Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                        accetta_prenotazioni_fuori_orario = Cmd.ExecuteScalar
                        If accetta_prenotazioni_fuori_orario Then
                            stazione_aperta_drop_offWebSevice = "3"
                        Else
                            stazione_aperta_drop_offWebSevice = "0"
                        End If
                    End If
                End If

                Dbc.Close()
                Rs.Close()
                Dbc.Open()
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing



        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni stazione_aperta_drop_offWebSevice  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try




    End Function

    Public Shared Function stazione_permette_VAL_verso_altre_stazioni(ByVal id_stazione As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String) As String
        'LA FUNZIONE COTNROLLA (NELLA TABELLA STAZIONI) SE LA STAZIONE PERMETTE VAL VERSO ALTRE STAZIONI
        '0) NON PERMETTE VAL VERSO ALTRE STAZIONI
        '1) PERMETTE VAL VERSO ALTRE STAZIONI
        Dim sqla As String = "SELECT val_verso_altre_stazioni FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim test As Boolean = Cmd.ExecuteScalar
            If test Then
                stazione_permette_VAL_verso_altre_stazioni = "1"
            Else
                salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "3", id_utente, "")
                stazione_permette_VAL_verso_altre_stazioni = "0"
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni stazione_permette_VAL_verso_altre_stazioni  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function

    Public Shared Function stazione_accetta_VAL_da_altre_stazioni(ByVal id_stazione As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String) As String
        'LA FUNZIONE CONTROLLA (NELLA TABELLA STAZIONI) SE LA STAZIONE ACCETTA VAL DA ALTRE STAZIONI
        '0) NON ACCETTA VAL DA ALTRE STAZIONI
        '1) ACCETTA VAL DA ALTRE STAZIONI
        Dim sqla As String = "SELECT val_da_altre_stazioni FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim test As Boolean = Cmd.ExecuteScalar
            If test Then
                stazione_accetta_VAL_da_altre_stazioni = "1"
            Else
                salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "4", id_utente, "")
                stazione_accetta_VAL_da_altre_stazioni = "0"
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni : stazione_accetta_VAL_da_altre_stazioni  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function

    Public Shared Function stazione_pick_up_on_request(ByVal id_stazione As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String) As String
        'LA FUNZIONE CONTROLLA SE LA STAZIONE ACCETTA SOLAMENTE PRENOTAZIONI ON REQUEST (PICK UP). NON E' UN WARNING BLOCCANTE (E' UN NORMALE 
        'STATO DELLA STAZIONE) E PER QUESTO IL WARNING VIENE SALVATO NELLA TABELLA DI WARNING COME INFO (IL CONTROLLO SERVIRA' UNICAMENTE PER SETTARE
        'AUTOMATICAMENTE LO STATO DELLA PRENOTAZIONE COME ON REQUEST)
        '0) PRENOTAZIONI NON ON REQUEST (PICK UP)
        '1) PRENOTAZIONI ON REQUESTO (PICK UP)
        Dim sqla As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            sqla = "SELECT solo_on_request_pickup FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim test As Boolean = Cmd.ExecuteScalar
            If test Then
                salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "9", id_utente, "")
                stazione_pick_up_on_request = "1"
            Else
                stazione_pick_up_on_request = "0"
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni stazione_pick_up_on_request : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function

    Public Shared Function stazione_drop_off_on_request(ByVal id_stazione As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String) As String
        'LA FUNZIONE CONTROLLA SE LA STAZIONE ACCETTA SOLAMENTE PRENOTAZIONI ON REQUEST (DROP OFF). NON E' UN WARNING BLOCCANTE (E' UN NORMALE 
        'STATO DELLA STAZIONE) E PER QUESTO IL WARNING VIENE SALVATO NELLA TABELLA DI WARNING COME INFO (IL CONTROLLO SERVIRA' UNICAMENTE PER SETTARE
        'AUTOMATICAMENTE LO STATO DELLA PRENOTAZIONE COME ON REQUEST)
        '0) PRENOTAZIONI NON ON REQUEST (DROP OFF)
        '1) PRENOTAZIONI ON REQUESTO (DROP OFF)

        Dim sqla As String = "SELECT solo_on_request_dropoff FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim test As Boolean = Cmd.ExecuteScalar
            If test Then
                salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "10", id_utente, "")
                stazione_drop_off_on_request = "1"
            Else
                stazione_drop_off_on_request = "0"
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni stazione_drop_off_on_request : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function

    Public Shared Function gruppo_vendibile_pick_up(ByVal id_stazione As String, ByVal id_gruppo As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String, ByVal salva_warning As Boolean) As Boolean
        'LA FUNZIONE CONTROLLA SE IL GRUPPO PASSATO E' DISPONIBILE PER IL PICK UP (INFORMAZIONE SALVATA NELLA STAZIONE).
        Dim sqla As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            sqla = "SELECT id_gruppo FROM stazioni_gruppi_non_prenotabili WITH(NOLOCK) WHERE id_stazione='" & id_stazione & "' AND id_gruppo='" & id_gruppo & "' AND tipo='PICK'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim test As String = Cmd.ExecuteScalar & ""
            If test = "" Then
                gruppo_vendibile_pick_up = True
            Else
                gruppo_vendibile_pick_up = False
                If salva_warning Then
                    salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "12", id_utente, "")
                End If
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni gruppo_vendibile_pick_up  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function

    Public Shared Function gruppo_vendibile_drop_off(ByVal id_stazione As String, ByVal id_gruppo As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String, ByVal salva_warning As Boolean) As Boolean
        'LA FUNZIONE CONTROLLA SE E' POSSIBILE CONSEGNARE IL GRUPPO AUTO SELEZIONATO NELLA STAZIONE SELEZIONATA
        '0)GRUPPO NON CONSEGNABILE 
        '1)GRUPPO CONSEGNABILE
        Dim sqla As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_gruppo FROM stazioni_gruppi_non_prenotabili WITH(NOLOCK) WHERE id_stazione='" & id_stazione & "' AND id_gruppo='" & id_gruppo & "' AND tipo='DROP'", Dbc)

            Dim test As String = Cmd.ExecuteScalar & ""

            If test = "" Then
                gruppo_vendibile_drop_off = True
            Else
                gruppo_vendibile_drop_off = False
                If salva_warning Then
                    salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "14", id_utente, "")
                End If
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni gruppo_vendibile_drop_off  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function

    Public Shared Function gruppo_vendibile_val(ByVal id_stazione As String, ByVal id_gruppo As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String, ByVal salva_warning As Boolean) As Boolean
        'LA FUNZIONE CONTROLLA SE E' POSSIBILE CONSEGNARE IL GRUPPO AUTO SELEZIONATO NELLA STAZIONE SELEZIONATA SE LA STAZIONE DI PARTENZA
        'E' DIVERSA (vAL)
        '0)GRUPPO NON CONSEGNABILE 
        '1)GRUPPO CONSEGNABILE

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_gruppo FROM stazioni_gruppi_non_prenotabili WITH(NOLOCK) WHERE id_stazione='" & id_stazione & "' AND id_gruppo='" & id_gruppo & "' AND tipo='VAL'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test = "" Then
            gruppo_vendibile_val = True
        Else
            gruppo_vendibile_val = False
            If salva_warning Then
                salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "15", id_utente, "")
            End If
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function stazioneInStopSell(ByVal idStazione As String, ByVal data As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String) As Boolean

        'CONTROLLA SE L'INTERA STAZIONE SI TROVA NELLO STATO DI STOP SELL.
        'SE NON LA SI RICHIAMA DA PREVENTIVI/PRENOTAZIONE/CONTRATTO SERVE SOLO IDSTAZIONE E DATA, GLI ALTRI DATI VERRANNO PASSATI COME
        'STRINGA VUOTA (SERVONO UNICAMETNE PER SALVARE IL WARNING IN CASO DI PRENOTAZIONE/CONTRATTO).

        Dim sqla As String = ""

        Try

            sqla = "SELECT TOP 1 stop_sell.id FROM stop_sell WITH(NOLOCK) INNER JOIN stop_sell_stazioni WITH(NOLOCK) ON stop_sell_stazioni.id_stop_sell=stop_sell.id "
            sqla += "WHERE convert(datetime,'" & data & "',102) BETWEEN convert(datetime,da_data,102) AND convert(datetime,a_data,102) "
            sqla += "And convert(date,getDate(),102)>=convert(datetime,data_efficacia,102) And tutti_gruppi='1' "
            sqla += "And tutte_fonti='1' AND tutte_stazioni='0' AND stop_sell_stazioni.id_stazione='" & idStazione & "'"



            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            'sqla = "Select TOP 1 stop_sell.id FROM stop_sell With(NOLOCK) INNER JOIN stop_sell_stazioni With(NOLOCK) On stop_sell_stazioni.id_stop_sell=stop_sell.id WHERE '" & data & "' BETWEEN da_data AND a_data AND getDate()>=data_efficacia AND tutti_gruppi='1' AND tutte_fonti='1' AND tutte_stazioni='0' AND stop_sell_stazioni.id_stazione='" & idStazione & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test <> "" Then
                stazioneInStopSell = True
                If id_preventivo <> "" Then
                    salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "7", id_utente, "")
                End If
            Else
                stazioneInStopSell = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni stazioneInStopSell : " & ex.Message & "<br/>" & sqla & "<br/>")
        End Try


    End Function

    Public Shared Function fonteInStopSell(ByVal idStazione As String, ByVal idFonte As String, ByVal data As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String) As Boolean
        'CONTROLLA SE LA FONTE E' IN STOP SELL

        Dim sqla As String = ""


        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            'CONTROLLO SE LA FONTE E' IN STOP SELL PER LA STAZIONE SPECIFICATA
            sqla = "Select TOP 1 stop_sell.id FROM stop_sell With(NOLOCK) INNER JOIN stop_sell_stazioni With(NOLOCK) On stop_sell_stazioni.id_stop_sell=stop_sell.id "
            sqla += "INNER JOIN stop_sell_fonti With(NOLOCK) On stop_sell.id=stop_sell_fonti.id_stop_sell "
            sqla += "WHERE convert(datetime,'" & data & "',102) BETWEEN convert(datetime,da_data,102) AND convert(datetime,a_data,102) "
            sqla += "And convert(datetime,getDate(),102)>=convert(datetime,data_efficacia,102) And tutte_stazioni='0' AND tutti_gruppi='1' "
            sqla += "And tutte_fonti='0' AND stop_sell_stazioni.id_stazione='" & idStazione & "' AND stop_sell_fonti.id_fonte='" & idFonte & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test <> "" Then
                fonteInStopSell = True
                If id_preventivo <> "" Then
                    salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "11", id_utente, "")
                End If
            Else
                'CONTROLLO SE LA FONTE E' IN STOP SELL PER TUTTE LE STAZIONI
                sqla = "SELECT TOP 1 stop_sell.id FROM stop_sell WITH(NOLOCK) INNER JOIN stop_sell_fonti WITH(NOLOCK) ON stop_sell.id=stop_sell_fonti.id_stop_sell "
                sqla += "WHERE convert(datetime,'" & data & "',102) BETWEEN convert(datetime,da_data,102) AND convert(datetime,a_data,102) "
                sqla += "And convert(datetime,getDate(),102)>=convert(datetime,data_efficacia,102) And tutte_stazioni='1' "
                sqla += "AND tutti_gruppi='1' AND tutte_fonti='0' AND stop_sell_fonti.id_fonte='" & idFonte & "'"

                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                test = Cmd.ExecuteScalar
                If test <> "" Then
                    fonteInStopSell = True
                    If id_preventivo <> "" Then
                        salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "11", id_utente, "")
                    End If
                Else
                    fonteInStopSell = False
                End If
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni fonteInStopSell : " & ex.Message & "<br/>" & sqla & "<br/>")
        End Try




    End Function

    Public Shared Function gruppoInStopSell(ByVal id_stazione As String, ByVal id_gruppo As String, ByVal data As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String, ByVal salva_warning As Boolean) As Boolean


        Dim sqla As String = ""
        ' HttpContext.Current.Response.Write("data : " & data & "<br/>")



        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            'IL GRUPPO E' IN STOP SELL IN DUE CASI: GRUPPO IN STOP SELL PER UNA SINGOLA STAZIONE O GRUPPO IN STOP SELL PER TUTTE LE STAZIONI
            'PER PRIMA COSA CONTROLLO SE IL GRUPPO E' IN STOP SELL PER LA STAZIONE ATTUALE
            sqla = "Select TOP 1 stop_sell.id FROM stop_sell With(NOLOCK) INNER JOIN stop_sell_gruppi With(NOLOCK) On stop_sell.id=stop_sell_gruppi.id_stop_sell "
            sqla += "INNER JOIN stop_sell_stazioni WITH(NOLOCK) ON stop_sell.id=stop_sell_stazioni.id_stop_sell "
            sqla += "WHERE convert(datetime,'" & data & "',102) BETWEEN convert(datetime,da_data,102) AND convert(datetime,a_data,102) "
            sqla += "And convert(datetime,getDate(),102)>=convert(datetime,data_efficacia,102) And tutte_fonti='1' "
            sqla += "And tutti_gruppi='0' AND tutte_stazioni='0' AND stop_sell_gruppi.id_gruppo='" & id_gruppo & "' AND stop_sell_stazioni.id_stazione='" & id_stazione & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test = "" Then
                'CONTROLLO SE IL GRUPPO E' IN STOP SELL PER TUTTE LE STAZIONI

                sqla += "Select TOP 1 stop_sell.id FROM stop_sell With(NOLOCK) INNER JOIN stop_sell_gruppi With(NOLOCK) On stop_sell.id=stop_sell_gruppi.id_stop_sell "
                sqla += "WHERE convert(datetime,'" & data & "',102) BETWEEN convert(datetime,da_data,102) AND convert(datetime,a_data,102) "
                sqla += "And convert(datetime,getDate(),102)>=convert(datetime,data_efficacia,102) "
                sqla += "And tutte_fonti='1' AND tutti_gruppi='0' AND tutte_stazioni='1' AND stop_sell_gruppi.id_gruppo='" & id_gruppo & "'"

                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                test = Cmd.ExecuteScalar

                If test = "" Then
                    gruppoInStopSell = False
                Else
                    gruppoInStopSell = True
                End If
            Else
                gruppoInStopSell = True
                If salva_warning Then
                    salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "16", id_utente, "")
                End If
            End If


            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni gruppoInStopSell : " & ex.Message & "<br/>" & sqla & "<br/>")
        End Try



    End Function

    Public Shared Function gruppoInStopSellPerFonte(ByVal id_stazione As String, ByVal id_fonte As String, ByVal id_gruppo As String, ByVal data As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String, ByVal salva_warning As Boolean) As Boolean


        Dim sqla As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            'CONTROLLO SE LA FONTE E' IN STOP SELL PER IL GRUPPO SELEZIONATO E PER LA STAZIONE SELEZIONATA
            sqla = "SELECT TOP 1 stop_sell.id FROM stop_sell WITH(NOLOCK) INNER JOIN stop_sell_gruppi WITH(NOLOCK) ON stop_sell.id=stop_sell_gruppi.id_stop_sell "
            sqla += "INNER JOIN stop_sell_stazioni WITH(NOLOCK) ON stop_sell.id=stop_sell_stazioni.id_stop_sell INNER JOIN stop_sell_fonti "
            sqla += "WITH(NOLOCK) ON stop_sell.id=stop_sell_fonti.id_stop_sell WHERE convert(datetime,'" & data & "',102) BETWEEN convert(datetime,da_data,102) AND convert(datetime,a_data,102) "
            sqla += "And convert(datetime,getDate(),102)>=convert(datetime,data_efficacia,102) And tutte_fonti='0' AND tutti_gruppi='0' AND tutte_stazioni='0' AND stop_sell_gruppi.id_gruppo='" & id_gruppo & "' "
            sqla += "And stop_sell_stazioni.id_stazione='" & id_stazione & "' AND stop_sell_fonti.id_fonte='" & id_fonte & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim test As String = Cmd.ExecuteScalar

            If test = "" Then
                'CONTROLLO SE LA FONTE E' IN STOP SELL PER IL GRUPPO SELEZIONATO PER TUTTE LE STAZIONI
                sqla = "SELECT TOP 1 stop_sell.id FROM stop_sell WITH(NOLOCK) INNER JOIN stop_sell_gruppi WITH(NOLOCK) ON stop_sell.id=stop_sell_gruppi.id_stop_sell "
                sqla += "INNER JOIN stop_sell_fonti WITH(NOLOCK) ON stop_sell.id=stop_sell_fonti.id_stop_sell WHERE convert(datetime,'" & data & "',102) BETWEEN convert(datetime,da_data,102) "
                sqla += "AND convert(datetime,a_data,102) AND convert(datetime,getDate(),102)>=convert(datetime,data_efficacia,102) AND tutte_fonti='0' AND tutti_gruppi='0' AND tutte_stazioni='1' "
                sqla += "AND stop_sell_gruppi.id_gruppo='" & id_gruppo & "' AND stop_sell_fonti.id_fonte='" & id_fonte & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                test = Cmd.ExecuteScalar

                If test = "" Then
                    gruppoInStopSellPerFonte = False
                Else
                    gruppoInStopSellPerFonte = True
                End If

            Else
                gruppoInStopSellPerFonte = True
                If salva_warning Then
                    salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "17", id_utente, "")
                End If
            End If


            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni gruppoInStopSellPerFonte : " & ex.Message & "<br/>" & sqla & "<br/>")
        End Try




    End Function

    Public Shared Function getMinGiorniNolo(ByVal id_tariffe_righe As String) As String
        Dim sqla As String = "SELECT min_giorni_nolo FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            getMinGiorniNolo = Cmd.ExecuteScalar & ""

            If getMinGiorniNolo = "" Then
                getMinGiorniNolo = "-1"
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni getMinGiorniNolo : " & ex.Message & "<br/>" & sqla & "<br/>")
        End Try


    End Function

    Public Shared Function getMaxGiorniNolo(ByVal id_tariffe_righe As String) As String

        Dim sqla As String = "SELECT max_giorni_nolo FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            getMaxGiorniNolo = Cmd.ExecuteScalar & ""

            If getMaxGiorniNolo = "" Then
                getMaxGiorniNolo = "-1"
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni getMaxGiorniNolo : " & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function


    Public Shared Function getGiorniDiNoleggio(ByVal pick_up As String, ByVal drop_off As String, ByVal ora_pick_up As String, ByVal minuti_pick_up As String, ByVal ora_drop_off As String, ByVal minuti_drop_off As String, ByVal id_tariffe_righe As String, Optional ByVal considerare_tolleranza_extra As Boolean = False) As Integer
        'RESTITUISCE I GIORNI DI NOLEGGIO DATI DATA E ORA DI PICK UP, DATA E ORA DI DROP OFF E ID DELLA TABELLA tariffe_righe (INFATTI I 
        'MINUTI DI RITARDO MASSIMO CONSENTITI PRIMA DI FAR SCATTARE IL GIORNO EXTRA DI NOLEGGIO SONO MEMORIZZATI AL LIVELLO DI ASSOCIAZIONE
        'TEMPO+KM/CONDIZIONE, QUINDI PER CALCOLARE I GIORNI DI NOLEGGIO SERVE SAPERE LA RIGA DI TARIFFA)
        'SE SPEFICIATO LA FUNZIONE CONSIDERERA' ANCHE I MINUTI DI TOLLERANZA EXTRA (PER RIENTRO AUTO DA RA).

        Dim sqla As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim tolleranza As String
            If considerare_tolleranza_extra Then
                tolleranza = "minuti_di_ritardo+tolleranza_rientro_nolo"
            Else
                tolleranza = "minuti_di_ritardo"
            End If
            sqla = "SELECT " & tolleranza & " FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim minuti_di_ritardo As Integer = Cmd.ExecuteScalar

            If pick_up = drop_off Then
                'PER PRENOTAZIONI ALL'INTERNO DELLA STESSA GIORNATA I GIORNI DI NOLEGGIO E' SEMPRE 1
                getGiorniDiNoleggio = 1
            Else
                getGiorniDiNoleggio = DateDiff(DateInterval.Day, CDate(pick_up), CDate(drop_off))
                If CInt(ora_pick_up) <= CInt(ora_drop_off) Then
                    '(ORE2*60 + MINUTI2) - (ORE1*60 + MINUTI1) = (ORE2 - ORE1)*60 + MINUTI2 - MINUT1
                    Dim minuti_extra_di_noleggio As Integer = 60 * (CInt(ora_drop_off) - CInt(ora_pick_up)) + CInt(minuti_drop_off) - CInt(minuti_pick_up)
                    If minuti_extra_di_noleggio > minuti_di_ritardo Then
                        getGiorniDiNoleggio = getGiorniDiNoleggio + 1
                    End If
                End If
            End If

            If getGiorniDiNoleggio = 0 Then
                getGiorniDiNoleggio = getGiorniDiNoleggio + 1
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni getGiorniDiNoleggio : " & ex.Message & "<br/>" & sqla & "<br/>")
        End Try



    End Function

    Public Shared Sub riporta_costi_prepagati(ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As Integer, ByVal id_gruppo As Integer, ByVal id_elemento As String, ByVal num_elemento As String, ByVal riporta_totale As String)
        'SE VIENE PASSATO L'ID ELEMENTO (ED EVENTUALMENTE IL NUMERO DI ELEMENTO) VIENE RECUPERATO DAL CALCOLO PRECEDENTE UNICAMENTE QUESTO
        Dim sqlStr As String
        Try
            Dim tabella As String
            Dim id_da_cercare As String
            If id_prenotazione <> "" Then
                tabella = "prenotazioni_costi"
                id_da_cercare = id_prenotazione
            ElseIf id_contratto <> "" Then
                tabella = "contratti_costi"
                id_da_cercare = id_contratto
            End If

            Dim condizione_elemento As String = ""
            If id_elemento <> "" Then
                condizione_elemento = "AND t1.id_elemento=" & id_elemento
                If num_elemento <> "" Then
                    condizione_elemento = condizione_elemento & " AND t1.num_elemento=" & num_elemento
                End If
            ElseIf riporta_totale Then
                condizione_elemento = "AND t1.nome_costo='" & Replace(Costanti.testo_elemento_totale, "'", "''") & "'"
            End If

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            sqlStr = "UPDATE t1 SET imponibile_scontato_prepagato=t2.imponibile_scontato_prepagato, " &
                "iva_imponibile_scontato_prepagato=t2.iva_imponibile_scontato_prepagato, imponibile_onere_prepagato=t2.imponibile_onere_prepagato," &
                "iva_onere_prepagato=t2.iva_onere_prepagato, sconto_su_imponibile_prepagato=t2.sconto_su_imponibile_prepagato FROM " & tabella & " AS t1 " &
                "INNER JOIN " & tabella & " AS t2 ON t1.id_documento=t2.id_documento AND (t1.id_elemento=t2.id_elemento OR (t1.nome_costo='" & Replace(Costanti.testo_elemento_totale, "'", "''") & "' AND t2.nome_costo='" & Replace(Costanti.testo_elemento_totale, "'", "''") & "')) AND ISNULL(t1.num_elemento,1)=ISNULL(t2.num_elemento,1) " &
                "WHERE t1.id_documento=" & id_da_cercare & " AND t1.num_calcolo=" & num_calcolo & " AND t2.num_calcolo=" & num_calcolo - 1 & "  AND (t1.prepagato='1' OR t1.nome_costo='" & Replace(Costanti.testo_elemento_totale, "'", "''") & "') " & condizione_elemento

            HttpContext.Current.Trace.Write(sqlStr)

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni riporta_costi_prepagati " & ex.Message & "<br/>" & sqlStr & "<br/>")

        End Try

    End Sub

    Protected Sub riporta_commissioni_agenzia(ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As Integer, ByVal id_gruppo As Integer)
        'SE VIENE PASSATO L'ID ELEMENTO (ED EVENTUALMENTE IL NUMERO DI ELEMENTO) VIENE RECUPERATO DAL CALCOLO PRECEDENTE UNICAMENTE QUESTO

        Dim tabella As String
        Dim id_da_cercare As String
        If id_prenotazione <> "" Then
            tabella = "prenotazioni_costi"
            id_da_cercare = id_prenotazione
        ElseIf id_contratto <> "" Then
            tabella = "contratti_costi"
            id_da_cercare = id_contratto
        End If

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        'LE COMMISSIONI LE RICOPIO SEMPRE DAI CAMPI ORIGINALI - QUESTO IN QUANTO, SE E' STATO EFFETTUATO UN CALCOLO CON GIORNI DIMINUITI E POI RITORNO AI GIORNI ORIGINALI 
        'O SUPERIORE, I CAMPI NON ORIGINALI CONTENGONO LA PERCENTUALE DEI GIORNI EFFETTIVI, NON CORRISPONDENTI ALLA COMMISSIONE PREINCASSATA DALL'AGENZIA
        Dim sqlStr As String = "UPDATE t1 SET commissioni_imponibile=t2.commissioni_imponibile_originale, " &
            "commissioni_iva=t2.commissioni_iva_originale, commissioni_imponibile_originale=t2.commissioni_imponibile_originale, commissioni_iva_originale=t2.commissioni_iva_originale " &
            "FROM " & tabella & " AS t1 " &
            "INNER JOIN " & tabella & " AS t2 ON t1.id_documento=t2.id_documento AND t1.id_gruppo=t2.id_gruppo AND t1.id_elemento=t2.id_elemento  " &
            "WHERE t1.id_documento=" & id_da_cercare & " AND t1.num_calcolo=" & num_calcolo & " AND t2.num_calcolo=" & num_calcolo - 1 & " AND t1.id_gruppo=" & id_gruppo & " AND t1.id_elemento=" & Costanti.ID_tempo_km


        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Private Function salvaRigaCalcolo(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String,
                                      ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String,
                                      ByVal id_elemento As String, ByVal num_elemento As String, ByVal nome_costo As String,
                                      ByVal valore_costo As String, ByVal valore_percentuale As String, ByVal aliquota_iva As String,
                                      ByVal codice_iva As String, ByVal iva_inclusa As String, ByVal scontabile As String, ByVal omaggiabile As String,
                                      ByVal acquistabile_nolo_in_corso As String, ByVal id_a_carico_di As String, ByVal id_metodo_stampa As String,
                                      ByVal obbligatorio As String, ByVal selezionato As String, ByVal ordine_stampa As Integer, ByVal franchigia_attiva As String,
                                      ByVal id_unita_misura As String, ByVal km_giorno_inclusi As String, ByVal packed As String, ByVal qta As String,
                                      ByVal data_aggiunta_nolo_in_corso As String, ByVal restituire_id As Boolean, ByVal prepagata As Boolean) As String

        'SALVA IN preventivi_costi IL COSTO DEL SINGOLO ACCESSORIO
        'NUM_ELEMENTO: numera un elemento qualora è presente più volte con lo stesso id (ES: YOUNG DRIVER PUO' ESSERE PRESENTE UNA VOLTA
        'PER IL PRIMO GUIDATORE O DUE VOLTE PER IL SECONDO. SE UN ELEMENTO E' PRESENTE UNA SOLA VOLTA NON E' NECESSARIO NUMERARLO)

        Dim id_da_salvare As String
        Dim tabella As String

        Dim colonna_km_inclusi As String = ""
        Dim kmGiornoInclusi As String = ""

        If id_preventivo <> "" Then
            id_da_salvare = id_preventivo
            tabella = "preventivi_costi"
        ElseIf id_ribaltamento <> "" Then
            id_da_salvare = id_ribaltamento
            tabella = "ribaltamento_costi"
        ElseIf id_prenotazione <> "" Then
            id_da_salvare = id_prenotazione
            tabella = "prenotazioni_costi"
        ElseIf id_contratto <> "" Then
            id_da_salvare = id_contratto
            tabella = "contratti_costi"

            If km_giorno_inclusi <> "" Then
                'L'INFORMAZIONE VIENE SALVATA SOLO IN FASE DI CONTRATTO
                colonna_km_inclusi = " km_giorno_inclusi,"
                kmGiornoInclusi = "'" & km_giorno_inclusi & "',"
            End If
        End If

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim a_carico_di As String

        If id_a_carico_di = "NULL" Then
            a_carico_di = "NULL"
        Else
            a_carico_di = "'" & id_a_carico_di & "'"
        End If

        Dim obblig As String
        If obbligatorio = "" Or obbligatorio = "NULL" Then
            obblig = "NULL"
        ElseIf obbligatorio = "True" Then
            obblig = "'1'"
        ElseIf obbligatorio = "False" Then
            obblig = "'0'"
        End If

        Dim scont As String

        If scontabile = "" Or scontabile = "NULL" Then
            scont = "NULL"
        ElseIf scontabile = "True" Then
            scont = "'1'"
        ElseIf scontabile = "False" Then
            scont = "'0'"
        End If

        Dim omagg As String

        If omaggiabile = "" Or omaggiabile = "NULL" Then
            omagg = "NULL"
        ElseIf omaggiabile = "True" Then
            omagg = "'1'"
        ElseIf omaggiabile = "False" Then
            omagg = "'0'"
        End If

        Dim acquistabile_nolo As String

        If acquistabile_nolo_in_corso = "" Or acquistabile_nolo_in_corso = "NULL" Then
            acquistabile_nolo = "NULL"
        ElseIf acquistabile_nolo_in_corso = "True" Then
            acquistabile_nolo = "'1'"
        ElseIf acquistabile_nolo_in_corso = "False" Then
            acquistabile_nolo = "'0'"
        End If

        Dim costo As String

        If valore_costo <> "NULL" Then
            costo = "'" & Replace(valore_costo, ",", ".") & "'"
        Else
            costo = "NULL"
        End If

        Dim percentuale As String

        If valore_percentuale <> "NULL" Then
            percentuale = "'" & Replace(valore_percentuale, ",", ".") & "'"
        Else
            percentuale = "NULL"
        End If

        Dim num_elem As String

        If num_elemento <> "" And num_elemento <> "NULL" Then
            num_elem = "'" & num_elemento & "'"
        Else
            num_elem = "NULL"
        End If

        Dim unita_misura As String

        If id_unita_misura <> "" And id_unita_misura <> "NULL" Then
            unita_misura = "'" & id_unita_misura & "'"
        Else
            unita_misura = "NULL"
        End If

        Dim pac As String
        If packed = "" Or packed = "NULL" Then
            pac = "NULL"
        ElseIf packed = "True" Then
            pac = "'1'"
        ElseIf packed = "False" Then
            pac = "'0'"
        End If

        Dim cod_iva As String
        If codice_iva = "" Or codice_iva = "NULL" Then
            cod_iva = "NULL"
        Else
            cod_iva = "'" & codice_iva & "'"
        End If

        Dim prepag As String
        If prepagata Then
            prepag = "'1'"
        Else
            prepag = "'0'"
        End If

        Dim data_nolo_in_corso1 As String = ""
        Dim data_nolo_in_corso2 As String = ""
        If data_aggiunta_nolo_in_corso <> "" Then
            data_nolo_in_corso1 = ",data_aggiunta_nolo_in_corso"
            data_nolo_in_corso2 = ",convert(datetime,'" & funzioni_comuni.GetDataSql(data_aggiunta_nolo_in_corso, 0) & "',102)"
        End If

        Dim sqlStr As String

        'aggiorna franchigia_attiva = true se id_elemento=283 Deposito cauzionale 19.01.2022
        If id_elemento = "283" Then
            franchigia_attiva = "1"
        End If


        If nome_costo = "Valore Tariffa" Then
            nome_costo = "Valore Tariffa"
        End If

        If nome_costo = "SCONTO" Then
            nome_costo = "SCONTO"
            If costo = "28.75" Then
                nome_costo = "SCONTO"
            End If
        End If


        If id_contratto <> "" Then
            sqlStr = "INSERT INTO " & tabella & " (id_documento, num_calcolo, id_gruppo, id_elemento, num_elemento, nome_costo, valore_costo,valore_percentuale, aliquota_iva, codice_iva, iva_inclusa, scontabile, omaggiabile, prepagato, acquistabile_nolo_in_corso, id_a_carico_di, id_metodo_stampa, obbligatorio, ordine_stampa, selezionato, franchigia_attiva, id_unita_misura," & colonna_km_inclusi & " qta, packed" & data_nolo_in_corso1 & ") VALUES ('" & id_da_salvare & "','" & num_calcolo & "','" & id_gruppo & "'," & id_elemento & "," & num_elem & ",'" & Replace(nome_costo, "'", "''") & "'," & costo & "," & percentuale & "," & Replace(aliquota_iva, ",", ".") & "," & cod_iva & ",'" & iva_inclusa & "'," & scont & "," & omagg & "," & prepag & "," & acquistabile_nolo & "," & a_carico_di & ",'" & id_metodo_stampa & "'," & obblig & "," & ordine_stampa & ",'" & selezionato & "'," & franchigia_attiva & "," & unita_misura & "," & kmGiornoInclusi & "'" & qta & "'," & pac & data_nolo_in_corso2 & ")"
        Else
            sqlStr = "INSERT INTO " & tabella & " (id_documento, num_calcolo, id_gruppo, id_elemento, num_elemento, nome_costo, valore_costo,valore_percentuale, aliquota_iva, codice_iva, iva_inclusa, scontabile, omaggiabile, prepagato, acquistabile_nolo_in_corso, id_a_carico_di, id_metodo_stampa, obbligatorio, ordine_stampa, selezionato, franchigia_attiva, id_unita_misura," & colonna_km_inclusi & " qta, packed" & data_nolo_in_corso1 & ") VALUES ('" & id_da_salvare & "','" & num_calcolo & "','" & id_gruppo & "'," & id_elemento & "," & num_elem & ",'" & Replace(nome_costo, "'", "''") & "'," & costo & "," & percentuale & "," & Replace(aliquota_iva, ",", ".") & "," & cod_iva & ",'" & iva_inclusa & "'," & scont & "," & omagg & "," & prepag & "," & acquistabile_nolo & "," & a_carico_di & ",'" & id_metodo_stampa & "'," & obblig & "," & ordine_stampa & ",'" & selezionato & "'," & franchigia_attiva & "," & unita_misura & "," & kmGiornoInclusi & "'" & qta & "'," & pac & data_nolo_in_corso2 & ")"
        End If


        'HttpContext.Current.Trace.Write(sqlStr)
        Try
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            'SE E' STATO RICHIESTO DI RESTITUIRE L'ID DELL'ELEMENTO APPENA INSERITO LO SELEZIONO ALTRIMENTI RESTITUISCO 0
            If restituire_id Then
                sqlStr = "SELECT @@IDENTITY FROM " & tabella & " WITH(NOLOCK)"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                salvaRigaCalcolo = Cmd.ExecuteScalar
            Else
                salvaRigaCalcolo = "0"
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni salvaRigaCalcolo " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Protected Function getDistanzaStazioni(ByVal stazione_pick_up As String, ByVal stazione_drop_off As String) As Integer
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT distanza FROM stazioni_distanza WITH(NOLOCK) WHERE ((id_stazione1='" & stazione_pick_up & "' AND id_stazione2='" & stazione_drop_off & "') OR (id_stazione1='" & stazione_drop_off & "' AND id_stazione2='" & stazione_pick_up & "'))", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test = "" Or test = "0" Then
            'IN QUESTO CASO NON HO TROVATO LA DISTANZA TRA LE STAZIONI E RESTITUISCO -1
            getDistanzaStazioni = -1
        Else
            getDistanzaStazioni = CInt(test)
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function get_elemento_fuori_orario_pickUp(ByVal ore_pick_up As Integer, ByVal minuti_pick_up As Integer) As String
        'RESTITUISCE L'ELEMENTO CONDIZIONE FUORI ORARIO IN BASE ALL'ORARIO DI PICK UP - QUESTI ELEMENTI SONO NELLA TABELLA condizioni_elementi
        'CON tipologia='FUORI'. PER QUESTI ELEMENTI NELLA TABELLA SI TROVANO VALORIZZATI I CAMPI ore_inizio_fuori_orario/minuti_inizio_fuori_orario
        '/ore_fine_fuori_orario/minuti_fine_fuori_orario CHE INDICANO LA FASCIA ORARIO (DENTRO LA QUALE CADE L'ORARIO DI PICK UP DEL VEICOLO
        'QUANDO LA STAZIONE E' CHIUSA) CHE IDENTIFICA L'ELEMENTO DA UTILIZZARE.
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "SELECT id FROM condizioni_elementi WITH(NOLOCK) WHERE tipologia='FUORI' AND" &
        "((" & ore_pick_up & " > ore_inizio_fuori_orario AND " & ore_pick_up & " < ore_fine_fuori_orario) " &
        " OR (" & ore_pick_up & "=ore_inizio_fuori_orario AND " & minuti_pick_up & ">= minuti_inizio_fuori_orario) " &
        " OR (" & ore_pick_up & "=ore_fine_fuori_orario AND " & minuti_pick_up & "<= minuti_fine_fuori_orario))"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        get_elemento_fuori_orario_pickUp = test

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getCondizioneFranchigieRidotte(ByVal id_stazione As String) As String
        'RESTITUISCE SOTTO FORMA DI CONDIZIONE DA AGGIUNGERE AD UNA WHERE GLI ID DI CONDIZIONI_ELEMENTI RELATIVI ALLE FRANCHIGIE RIDOTTE PERò
        'LA STAZIONE PASSATA COME ARGOMENTO
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_condizioni_elementi FROM condizioni_elementi_franchigia_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & id_stazione & "'", Dbc)
        Dim Rs As Data.SqlClient.SqlDataReader

        Rs = Cmd.ExecuteReader()

        getCondizioneFranchigieRidotte = ""

        Do While Rs.Read
            getCondizioneFranchigieRidotte = getCondizioneFranchigieRidotte & " OR condizioni_elementi.id='" & Rs("id_condizioni_elementi") & "'"
        Loop

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Sub aggiungi_accessorio_pieno_caburante(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String)
        Dim SqlStr As String
        Try
            'VIENE AGGIUNTO A COSTO 0 L'ACCESSORIO A SCELTA "PIENO CARBURANTE" CHE PERMETTE DI RIENTRARE SENZA IL PIENO EFFETTUATO
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            SqlStr = "SELECT condizioni_elementi.id, condizioni_elementi.descrizione, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva," &
            "scontabile, omaggiabile, acquistabile_nolo_in_corso FROM condizioni_elementi WITH(NOLOCK)" &
            "INNER JOIN aliquote_iva WITH(NOLOCK) ON condizioni_elementi.id_aliquota_iva=aliquote_iva.id " &
            "WHERE tipologia='RIMUOVI_RIFORNIMENTO'"

            Dim Cmd As New Data.SqlClient.SqlCommand(SqlStr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.Read() Then
                'IL PIENO CARBURANTE - AVENDO UN COSTO DIPENDENTE DALL'AUTOMOBILE SELEZIONATO - E' SICURAMENTE NON PREPAGATO
                salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id"), "", Rs("descrizione"), "0", "NULL", Rs("iva"), Rs("codice_iva") & "", "True", Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Costanti.id_a_carico_del_cliente, Costanti.id_valorizza_nel_contratto, "False", "0", "4", "NULL", "0", "", "True", "1", "", False, False)
            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni aggiungi_accessorio_pieno_caburante  : " & ex.Message & "<br/>" & SqlStr & "<br/>")
        End Try

    End Sub

    Public Shared Function get_spese_postali(ByVal id_ditta As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT tipo_spedizione_fattura FROM ditte WITH(NOLOCK) WHERE id_ditta='" & id_ditta & "'", Dbc)

        get_spese_postali = Cmd.ExecuteScalar & ""

        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Sub analisi_condizioni(ByVal giorni_noleggio As Integer, ByVal giorni_prepagati As Integer, ByVal prepagata As Boolean, ByVal elementi_prepagati As Collection, ByVal data_pick_up As String, ByVal ore_pick_up As Integer, ByVal minuti_pick_up As Integer, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal id_condizione_figlia As String, ByVal id_condizione_madre As String, ByVal id_gruppo As String, ByVal id_ditta As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String)

        Try
            'ANALISI DELLE RIGHE CALCOLABILI (tipo stampa 1 o 2) 
            Dim costo As String
            Dim percentuale As String
            Dim selezionato As String
            Dim id_unita_misura As String
            Dim packed As String
            Dim qta As String

            Dim id_elemento_fuori_orario_pickup As String = ""

            '---------------------------------------------------------------------------------------------------------------------------------
            'SE LA PRENOTAZIONE E' FUORI ORARIO PER PICK UP ANALIZZO ANCHE L'EVENTUALE ELEMENTO FUORI ORARIO DA UTILIZZARE
            Dim condizione_fuori_orario_pick_up As String = ""
            Dim fuori_orario_pick_up As String = stazione_aperta_pick_up(stazione_pick_up, data_pick_up, ore_pick_up, minuti_pick_up, "", "", "", "", "", "")

            'HttpContext.Current.Trace.Write(" AAA " & fuori_orario_pick_up)

            If fuori_orario_pick_up = "0" Or fuori_orario_pick_up = "1" Or fuori_orario_pick_up = "3" Then
                'IN CASO DI STAZIONE CHIUSA (0) - NESSUN TEMPLATE ORARIO TROVATO (1) PER CUI SI CONSIDERA LA STAZIONE CHIUSA - 
                'STAZIONE CHIUSA MA ACCETTA FUORI ORARIO (3) TROVO L'ELEMENTO FUORI ORARIO DA UTILIZZARE E LO AGGIUNGO AGLI ELEMENTI DA ANALIZZARE
                id_elemento_fuori_orario_pickup = get_elemento_fuori_orario_pickUp(ore_pick_up, minuti_pick_up)
                If id_elemento_fuori_orario_pickup <> "" Then
                    'E STATO RESTITUITO UN ID - CREO LA CONDIZIONE DA AGGIUNGERE ALLA SELECT 
                    condizione_fuori_orario_pick_up = " OR condizioni_elementi.id='" & id_elemento_fuori_orario_pickup & "'"
                End If
            End If
            '---------------------------------------------------------------------------------------------------------------------------------

            'SE E' STATO PASSATO L'ID DITTA CONTROLLO SE DOVREBBE ESSERE AGGIUNTO L'ACCESSORIO SPESE POSTALI (DALL'ANAGRAFICA DITTA)
            'DA RICORDARE CHE E' CERTAMENTE UN ELEMENTO OBBLIGATORIO
            Dim condizione_spese_postali As String = ""
            If id_ditta <> "" Then
                Dim spese As String = get_spese_postali(id_ditta)
                If spese = "P" Then
                    condizione_spese_postali = " OR condizioni_elementi.tipologia='SPESE_SPED_FATT'"
                End If
            End If
            '---------------------------------------------------------------------------------------------------------------------------------

            '---------------------------------------------------------------------------------------------------------------------------------
            'SE PER LA STAZIONE ATTUALE ESISTONO DELLE FRANCHIGIE RIDOTTE ANCHE AVENDO ACQUISTATO DELLE ASSICURAZIONI SI FA IN MODO DI
            'CALCOLARNE L'IMPORTO GIA' IN QUESTA FASE (IN OGNI CASO VERRANNO IMPOSTATE COME NON ATTIVE DI DEFAULT E ATTIVATE SUCCESSIVAMENTE
            'NEL MOMENTO IN CUI SI ACQUISTA L'ASSICURAZIONE RELATIVA)
            Dim condizione_franchigie_ridotte As String = getCondizioneFranchigieRidotte(stazione_pick_up)
            '---------------------------------------------------------------------------------------------------------------------------------

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            'SELEZIONO TUTTI GLI ELEMENTI TRANNE I NON OBBLIGATORI CHE NON DEVONO ESSERE VALORIZZATI (ACCESSORI RARI) 
            'QUESTO DALLA CONDIZIONE FIGLIA E GLI ELEMENTI DELLA MADRE CHE NON SONO SPECIFICATI NELLA FIGLIA
            Dim sqlStr As String, sqlStrCondizioni As String

            sqlStr = "(SELECT condizioni_righe.costi_periodi,condizioni.iva_inclusa, condizioni_elementi.id As id_elemento, condizioni_elementi.tipologia_franchigia, condizioni_righe.id_metodo_stampa,condizioni_righe.obbligatorio,condizioni_elementi.descrizione As nome_elemento, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, condizioni_elementi.scontabile, condizioni_elementi.omaggiabile, condizioni_elementi.acquistabile_nolo_in_corso ,condizioni_righe.id, condizioni_x_gruppi.id_gruppo, condizioni_righe.applicabilita_da," &
                                "condizioni_righe.applicabilita_a, condizioni_righe.id_a_carico_di,condizioni_righe.pac, condizioni_righe.id_unita_misura," &
                                "condizioni_righe.costo, condizioni_righe.tipo_costo, condizioni_elementi.tipologia, condizioni_elementi.valorizza, condizioni_unita_misura.km_divisore_num_giorni, condizioni_unita_misura.descrizione As unita_misura " &
                                "FROM condizioni_righe WITH(NOLOCK) " &
                                "INNER JOIN condizioni_elementi WITH(NOLOCK) ON condizioni_righe.id_elemento=condizioni_elementi.id " &
                                "INNER JOIN aliquote_iva WITH(NOLOCK) ON condizioni_elementi.id_aliquota_iva=aliquote_iva.id " &
                                "LEFT OUTER JOIN condizioni_x_gruppi WITH(NOLOCK) ON condizioni_x_gruppi.id_condizione=condizioni_righe.id " &
                                "LEFT OUTER JOIN condizioni_unita_misura WITH(NOLOCK) ON condizioni_righe.id_unita_misura=condizioni_unita_misura.id " &
                                "INNER JOIN condizioni WITH(NOLOCK) ON condizioni_righe.id_condizione=condizioni.id " &
                                "WHERE (condizioni_righe.id_condizione='" & id_condizione_figlia & "') AND (condizioni_x_gruppi.id_gruppo='" & id_gruppo & "' OR " &
                                "condizioni_x_gruppi.id_gruppo IS NULL) AND (condizioni_elementi.tipologia='CONDIZIONE' OR condizioni_elementi.tipologia='KM_EXTRA' " & condizione_fuori_orario_pick_up & condizione_franchigie_ridotte & condizione_spese_postali & ") AND NOT (condizioni_righe.obbligatorio='0' AND condizioni_elementi.valorizza='0')) " &
                                " UNION " &
                    "(SELECT condizioni_righe.costi_periodi,condizioni.iva_inclusa, condizioni_elementi.id As id_elemento, condizioni_elementi.tipologia_franchigia, condizioni_righe.id_metodo_stampa,condizioni_righe.obbligatorio,condizioni_elementi.descrizione As nome_elemento, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, condizioni_elementi.scontabile, condizioni_elementi.omaggiabile, condizioni_elementi.acquistabile_nolo_in_corso, condizioni_righe.id, condizioni_x_gruppi.id_gruppo, condizioni_righe.applicabilita_da," &
                                " condizioni_righe.applicabilita_a, condizioni_righe.id_a_carico_di,condizioni_righe.pac, condizioni_righe.id_unita_misura," &
                                " condizioni_righe.costo, condizioni_righe.tipo_costo, condizioni_elementi.tipologia, condizioni_elementi.valorizza, condizioni_unita_misura.km_divisore_num_giorni, condizioni_unita_misura.descrizione As unita_misura " &
                                "FROM condizioni_righe WITH(NOLOCK) " &
                                "INNER JOIN condizioni_elementi WITH(NOLOCK) ON condizioni_righe.id_elemento=condizioni_elementi.id " &
                                "INNER JOIN aliquote_iva WITH(NOLOCK) ON condizioni_elementi.id_aliquota_iva=aliquote_iva.id " &
                                "LEFT OUTER JOIN condizioni_x_gruppi WITH(NOLOCK) ON condizioni_x_gruppi.id_condizione=condizioni_righe.id " &
                                "LEFT OUTER JOIN condizioni_unita_misura WITH(NOLOCK) ON condizioni_righe.id_unita_misura=condizioni_unita_misura.id " &
                                "INNER JOIN condizioni WITH(NOLOCK) ON condizioni_righe.id_condizione=condizioni.id " &
                                " WHERE (condizioni_elementi.tipologia='CONDIZIONE' OR condizioni_elementi.tipologia='KM_EXTRA' " & condizione_fuori_orario_pick_up & condizione_franchigie_ridotte & condizione_spese_postali & ")" &
                                " AND (condizioni_righe.id_condizione='" & id_condizione_madre & "') " &
                                " AND (condizioni_x_gruppi.id_gruppo='" & id_gruppo & "' OR condizioni_x_gruppi.id_gruppo IS NULL) " &
                                " AND (NOT (condizioni_righe.obbligatorio='0' AND condizioni_elementi.valorizza='0')) " &
                                " AND (condizioni_elementi.id NOT IN (SELECT DISTINCT ISNULL(condizioni_righe.id_elemento,0) FROM condizioni_righe WITH(NOLOCK) LEFT OUTER JOIN condizioni_x_gruppi WITH(NOLOCK) ON condizioni_x_gruppi.id_condizione=condizioni_righe.id WHERE (condizioni_righe.id_condizione='" & id_condizione_figlia & "') AND (condizioni_x_gruppi.id_gruppo='" & id_gruppo & "' OR condizioni_x_gruppi.id_gruppo IS NULL)))" &
                                " )  ORDER BY condizioni_x_gruppi.id_gruppo DESC, id_elemento,applicabilita_da  "

            'x il calcolo dei costi delle condizioni 28.09.2022 salvo
            sqlStrCondizioni = "(SELECT condizioni_righe.costi_periodi,condizioni.iva_inclusa, condizioni_elementi.id As id_elemento, condizioni_elementi.tipologia_franchigia, condizioni_righe.id_metodo_stampa,condizioni_righe.obbligatorio,condizioni_elementi.descrizione As nome_elemento, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, condizioni_elementi.scontabile, condizioni_elementi.omaggiabile, condizioni_elementi.acquistabile_nolo_in_corso ,condizioni_righe.id, condizioni_x_gruppi.id_gruppo, condizioni_righe.applicabilita_da," &
                                "condizioni_righe.applicabilita_a, condizioni_righe.id_a_carico_di,condizioni_righe.pac, condizioni_righe.id_unita_misura," &
                                "condizioni_righe.costo, condizioni_righe.tipo_costo, condizioni_elementi.tipologia, condizioni_elementi.valorizza, condizioni_unita_misura.km_divisore_num_giorni, condizioni_unita_misura.descrizione As unita_misura " &
                                "condizioni_righe.costi_periodi " &
                                "FROM condizioni_righe WITH(NOLOCK) " &
                                "INNER JOIN condizioni_elementi WITH(NOLOCK) ON condizioni_righe.id_elemento=condizioni_elementi.id " &
                                "INNER JOIN aliquote_iva WITH(NOLOCK) ON condizioni_elementi.id_aliquota_iva=aliquote_iva.id " &
                                "LEFT OUTER JOIN condizioni_x_gruppi WITH(NOLOCK) ON condizioni_x_gruppi.id_condizione=condizioni_righe.id " &
                                "LEFT OUTER JOIN condizioni_unita_misura WITH(NOLOCK) ON condizioni_righe.id_unita_misura=condizioni_unita_misura.id " &
                                "INNER JOIN condizioni WITH(NOLOCK) ON condizioni_righe.id_condizione=condizioni.id " &
                                "WHERE pac='PACXXXX' and id_elemento='IDEXXXX' and (condizioni_righe.id_condizione='" & id_condizione_figlia & "') AND (condizioni_x_gruppi.id_gruppo='" & id_gruppo & "' OR " &
                                "condizioni_x_gruppi.id_gruppo IS NULL) AND (condizioni_elementi.tipologia='CONDIZIONE' OR condizioni_elementi.tipologia='KM_EXTRA' " & condizione_fuori_orario_pick_up & condizione_franchigie_ridotte & condizione_spese_postali & ") AND NOT (condizioni_righe.obbligatorio='0' AND condizioni_elementi.valorizza='0')) " &
                                " UNION " &
                    "(SELECT condizioni_righe.costi_periodi,condizioni.iva_inclusa, condizioni_elementi.id As id_elemento, condizioni_elementi.tipologia_franchigia, condizioni_righe.id_metodo_stampa,condizioni_righe.obbligatorio,condizioni_elementi.descrizione As nome_elemento, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, condizioni_elementi.scontabile, condizioni_elementi.omaggiabile, condizioni_elementi.acquistabile_nolo_in_corso, condizioni_righe.id, condizioni_x_gruppi.id_gruppo, condizioni_righe.applicabilita_da," &
                                " condizioni_righe.applicabilita_a, condizioni_righe.id_a_carico_di,condizioni_righe.pac, condizioni_righe.id_unita_misura," &
                                " condizioni_righe.costo, condizioni_righe.tipo_costo, condizioni_elementi.tipologia, condizioni_elementi.valorizza, condizioni_unita_misura.km_divisore_num_giorni, condizioni_unita_misura.descrizione As unita_misura " &
                                "FROM condizioni_righe WITH(NOLOCK) " &
                                "INNER JOIN condizioni_elementi WITH(NOLOCK) ON condizioni_righe.id_elemento=condizioni_elementi.id " &
                                "INNER JOIN aliquote_iva WITH(NOLOCK) ON condizioni_elementi.id_aliquota_iva=aliquote_iva.id " &
                                "LEFT OUTER JOIN condizioni_x_gruppi WITH(NOLOCK) ON condizioni_x_gruppi.id_condizione=condizioni_righe.id " &
                                "LEFT OUTER JOIN condizioni_unita_misura WITH(NOLOCK) ON condizioni_righe.id_unita_misura=condizioni_unita_misura.id " &
                                "INNER JOIN condizioni WITH(NOLOCK) ON condizioni_righe.id_condizione=condizioni.id " &
                                " WHERE pac='PACXXXX' and id_elemento='IDEXXXX' and (condizioni_elementi.tipologia='CONDIZIONE' OR condizioni_elementi.tipologia='KM_EXTRA' " & condizione_fuori_orario_pick_up & condizione_franchigie_ridotte & condizione_spese_postali & ")" &
                                " AND (condizioni_righe.id_condizione='" & id_condizione_madre & "') " &
                                " AND (condizioni_x_gruppi.id_gruppo='" & id_gruppo & "' OR condizioni_x_gruppi.id_gruppo IS NULL) " &
                                " AND (NOT (condizioni_righe.obbligatorio='0' AND condizioni_elementi.valorizza='0')) " &
                                " AND (condizioni_elementi.id NOT IN (SELECT DISTINCT ISNULL(condizioni_righe.id_elemento,0) FROM condizioni_righe WITH(NOLOCK) LEFT OUTER JOIN condizioni_x_gruppi WITH(NOLOCK) ON condizioni_x_gruppi.id_condizione=condizioni_righe.id WHERE (condizioni_righe.id_condizione='" & id_condizione_figlia & "') AND (condizioni_x_gruppi.id_gruppo='" & id_gruppo & "' OR condizioni_x_gruppi.id_gruppo IS NULL)))" &
                                " )  ORDER BY applicabilita_da, applicabilita_a"



            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader

            Dbc.Close()
            Dbc.Open()
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            Rs = Cmd.ExecuteReader()
            Dim giorni_nolo As Integer
            Dim prepag As Boolean
            Dim idele As String = ""
            Dim Costi_Periodi As Boolean = False 'aggiunto salvo 26.04.2023


            Dim importo As Double = 0           'aggiunto 01.10.2022 salvo
            Dim ggNoloScalare As Integer = 0    'aggiunto 01.10.2022 salvo


            Do While Rs.Read()

                Costi_Periodi = Rs("costi_periodi")


                'aggiunto 01.10.2022 salvo
                If idele = "" Then
                    idele = Rs("id_elemento")
                    importo = 0
                Else
                    If idele <> Rs("id_elemento") Then
                        idele = Rs("id_elemento")
                        importo = 0
                    End If
                End If

                If Rs("id_elemento") = "125" Then
                    idele = Rs("id_elemento")
                End If


                If Rs("id_elemento") = "180" Then
                    idele = Rs("id_elemento")
                End If

                If Rs("id_elemento") = "283" Then
                    idele = Rs("id_elemento")
                End If


                If Rs("id_elemento") = "247" Then 'protezione pneumatici
                    idele = Rs("id_elemento")
                End If

                If Rs("id_elemento") = "89" Then 'beaby seat
                    idele = Rs("id_elemento")
                End If



                'end aggiunto 01.10.2022 salvo



                giorni_nolo = giorni_noleggio
                'HttpContext.Current.Trace.Write("CIAO1 " & Rs("id_elemento") & " " & elementi_prepagati.Contains(Rs("id_elemento")))

                If prepagata AndAlso giorni_prepagati > giorni_noleggio AndAlso elementi_prepagati.Contains(Rs("id_elemento")) Then
                    'HttpContext.Current.Trace.Write("CIAO2 " & giorni_prepagati)
                    giorni_nolo = giorni_prepagati
                End If

                Dim ordine_stampa As Integer
                Dim tipologia_franchigia = Rs("tipologia_franchigia") & ""
                id_unita_misura = Rs("id_unita_misura")
                packed = Rs("pac")

                qta = "1" 'LA QUANTITA' SARA' SEMPRE 1 TRANNE NEL CASO DI ACCESSORIO GIORNALIERO

                'SE L'ELEMENTO CHE SI STA PROCESSANDO E' UN ELEMENTO FRANCHIGIA ALLORA
                '1) SE E' UNA FRANCHIGIA SETTO franchigia_attiva (della tabella preventivi_costi) A False
                '2) SE E' UNA FRANCHIGIA RIDOTTA SETTO franchigia_attiva a False
                '3) IN TUTTI GLI ALTRI I CASI IL CAMPO PUO' ESSERE LASCIATO A NULL

                If tipologia_franchigia <> "" Then
                    If tipologia_franchigia = "FRANCHIGIA" Then
                        tipologia_franchigia = "'1'"
                    ElseIf tipologia_franchigia = "FRANCHIGIA RID" Then
                        tipologia_franchigia = "'0'"
                    Else
                        tipologia_franchigia = "NULL"
                    End If
                Else
                    tipologia_franchigia = "NULL"
                End If

                If (Rs("id_a_carico_di") & "") = Costanti.id_accessorio_incluso And (Rs("id_metodo_stampa") & "") <> Costanti.id_stampa_informativa_con_valore And (Rs("id_metodo_stampa") & "") <> Costanti.id_stampa_informativa_senza_valore Then
                    'INCLUSI
                    ordine_stampa = 2
                    selezionato = "1"
                    If prepagata Then
                        'GLI ACCESSORI INCLUSI NON DEVONO ESSERE SEGNALATI COME PREPAGATI
                        prepag = False
                    End If
                ElseIf (Rs("id_a_carico_di") & "") <> Costanti.id_accessorio_incluso And (Rs("obbligatorio") & "") = "True" And (Rs("id_metodo_stampa") & "") <> Costanti.id_stampa_informativa_con_valore And (Rs("id_metodo_stampa") & "") <> Costanti.id_stampa_informativa_senza_valore Then
                    'NON INCLUSI - OBBLIGATORI
                    ordine_stampa = 3
                    selezionato = "1"
                    If prepagata Then
                        'GLI ACCESSORI OBBLIGATORI CALCOLATI IN QUESTA FASE SONO CERTAMENTE PREPAGATI COME PRIMO CALCOLO - ATTUALMENTE LE SPESE DI SPEDIZIONI POSTALI 
                        'PUO' ESSERE PREPAGATO MA NON VIENE CALCOLATO IN FASE DI RIBALTAMNETO (PUO' ESSERE PREPAGATO SOLO DA ARES)
                        'A MENO CHE NON SIA L'ELEMENTO "FUORI ORARIO" NON PRESENTE NEL CALCOLO PRECEDENTE

                        If prepagata AndAlso giorni_prepagati > 0 AndAlso id_elemento_fuori_orario_pickup <> "" AndAlso Rs("id_elemento") = id_elemento_fuori_orario_pickup AndAlso Not elementi_prepagati.Contains(Rs("id_elemento")) Then
                            prepag = False
                        Else
                            prepag = True
                        End If

                        'If Rs("tipologia") <> "SPESE_SPED_FATT" Then
                        '    prepag = True
                        'Else
                        '    prepag = False
                        'End If
                    End If
                ElseIf (Rs("id_a_carico_di") & "") <> Costanti.id_accessorio_incluso And (Rs("obbligatorio") & "") = "False" And (Rs("id_metodo_stampa") & "") <> Costanti.id_stampa_informativa_con_valore And (Rs("id_metodo_stampa") & "") <> Costanti.id_stampa_informativa_senza_valore Then
                    'NON INCLUSI - NON OBBLIGATORI
                    ordine_stampa = 4
                    selezionato = "0"
                    If prepagata AndAlso elementi_prepagati.Contains(Rs("id_elemento")) Then
                        'GLI ACCESSORI DEVONO ESSERE SEGNALATI COME PREPAGATI SOLO SE LO ERANO NEL CALCOLO PRECEDENTE (E MAI COME PRIMO CALCOLO)
                        prepag = True
                    Else
                        prepag = False
                    End If
                ElseIf (Rs("id_metodo_stampa") & "") = Costanti.id_stampa_informativa_con_valore Or (Rs("id_metodo_stampa") & "") = Costanti.id_stampa_informativa_senza_valore Then
                    'INFORMATIVA
                    ordine_stampa = 7
                    selezionato = "0"
                    If prepagata Then
                        'LE INFORMATIVE NON SONO MAI PREPAGATE
                        prepag = False
                    End If
                End If


                'calcolo degli importi secondo il nuovo calcolo 01.10.2022 salvo
                If Rs("id_elemento") = "320" Then
                    idele = Rs("id_elemento")
                End If

                If Rs("id_elemento") = "85" Then 'child seat 26.04.2023 salvo
                    idele = Rs("id_elemento")
                End If

                'end  calcolo degli importi secondo il nuovo calcolo 01.10.2022 salvo

                Dim costo_new As String = "0"

                '# salvo aggiunto x nuovo calcolo condizioni 19.07.2023
                Dim FlagNewCalcolo As Boolean = False
                Dim DataNuovoCalcoloCondizioni As String = ConfigurationManager.AppSettings("DataNuovoCalcoloCondizioni")
                'se la data di pickup è superiore alla data di inizio nuovo calcolo
                'attiva il nuovo calcolo. 
                If DataNuovoCalcoloCondizioni <> "" Then 'verifica solo se variabile piena
                    Dim dataAttivazioneGG As Integer = DateDiff("d", CDate(DataNuovoCalcoloCondizioni), CDate(data_pick_up))
                    If dataAttivazioneGG >= 0 Then
                        FlagNewCalcolo = True
                    End If
                End If
                'ATTENZIONE : Al momento il valore è sempre FALSE fino a quando non è verificato la presenza del riferimento
                'in [condizioni_righe_costi_periodi] salvo 22.07.2023
                'FlagNewCalcolo = False      'SOLO FINO A VERIFICA EFFETTUATA
                '@ end aggiunto salvo 

                'OGNI RIGA E' DA SALVARE
                If Rs("id_unita_misura") = "0" Then

                    'CASO 1: NESSUNA UNITA' DI MISURA: IN QUESTO CASO NON MI INTERESSA IL CASO "PACKED" - LA CONDIZIONE E' PER FORZA
                    'SENZA LIMITE (NON CONTROLLO I GIORNI DI NOLEGGIO O I KM)

                    'se Franchigia o deposito cauzionale

                    If Rs("tipo_costo") = "€" Then

                        If FlagNewCalcolo = True Then 'se nuovo calcolo 19.07.2023 salvo
                            'se restituisce -1 la riga non esiste e prende il valore da condizioni_righe - salvo 24.07.2023
                            costo = funzioni_comuni_new.GetCostiPeriodiElementi(idele, data_pick_up, "", Rs("applicabilita_da"), Rs("applicabilita_a"), id_condizione_figlia)
                            'se non trova valore costo=-1 recupera il valore da condizioni_righe - salvo 24.07.2023
                            If costo = -1 Then
                                costo = Rs("costo")
                            End If
                        Else
                            costo = Rs("costo")
                        End If

                        percentuale = "NULL"
                    ElseIf Rs("tipo_costo") = "%" Then
                        costo = "NULL"
                        percentuale = Rs("costo")
                    Else
                        'CASO incluso senza valore
                        costo = "0"
                        percentuale = "NULL"
                    End If

                    

                    salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", Rs("nome_elemento") & "", costo, percentuale, Rs("iva"), Rs("codice_iva") & "", Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), selezionato, ordine_stampa, tipologia_franchigia, id_unita_misura, "", packed, qta, "", False, prepag)


                ElseIf Rs("id_unita_misura") = Costanti.id_unita_misura_giorni Then
                    'CASO 2: UNITA' DI MISURA GIORNI. CONTROLLO SE IL LIMITE (GIORNI DI NOLEGGIO) E' RISPETTATO O SE LA RIGA 
                    'E' SENZA ALCUN LIMITE (SE E' SEMPRE APPLICABILE AVRO' 0 SIA IN applicabilita_da CHE IN pplicabilita_a)
                    'IN OGNI CASO SE LA CONDIZIONE E' PACKED VIENE PRESO IL VALORE IN TOTO, SE NON E' PACKED MOLTIPLICO IL VALORE
                    'PER I GIORNI DI NOLEGGIO
                    'CONTROLLO SE C'E' UNA REGOLA DI APPLICABILITA'. 

                    ''aggiunto 01.10.2022 salvo

                    If Rs("id_elemento") = "247" Then 'protezione pneumatici 19.07.2023 salvo
                        idele = Rs("id_elemento")
                    End If

                    '#Salvo aggiunto 27.04.2023-19.07.2023

                    If FlagNewCalcolo = True Then 'se nuovo calcolo 19.07.2023 salvo
                        'se restituisce -1 la riga non esiste e prende il valore da condizioni_righe - salvo 24.07.2023
                        costo_new = funzioni_comuni_new.GetCostiPeriodiElementi(idele, data_pick_up, "", Rs("applicabilita_da"), Rs("applicabilita_a"), id_condizione_figlia)
                        'se non trova valore costo=-1 recupera il valore da condizioni_righe - salvo 24.07.2023
                        If costo_new = -1 Then
                            costo_new = Rs("costo")
                        End If
                    Else
                        costo_new = Rs("costo")
                    End If

                    '@ end salvo

                    If Rs("applicabilita_da") = 0 Then

                        importo = CDbl(costo_new) * giorni_nolo     'aggiornato xnuovocalcolo

                    ElseIf Rs("applicabilita_da") = 1 Then

                        If giorni_nolo >= Rs!applicabilita_a Then

                            If packed = True Then   'se packed prende il valore totale per tutti i giorni - salvo 16.11.2022 nel caso di extra suCondizioni
                                importo = CDbl(costo_new) ' Rs!costo 'aggiornato 27.04.2023
                            Else
                                importo = CDbl(costo_new) * Rs!applicabilita_a ' Rs!costo 'aggiornato 27.04.2023
                            End If
                            ggNoloScalare = 0
                        Else
                            importo = CDbl(costo_new) * giorni_nolo 'aggiornato 27.04.2023
                        End If

                    ElseIf Rs("applicabilita_da") > 1 Then 'righe successive se presenti sullo stesso elemento 


                        If idele = Rs!id_elemento Then
                            If giorni_nolo >= Rs!applicabilita_a Then
                                importo += CDbl(costo_new) * ((Rs!applicabilita_a - Rs!applicabilita_da) + 1) 'costo per il range di giorni di questo record 01.10.2022 / 27.04.2023
                            Else
                                If giorni_nolo <= Rs!applicabilita_da Then
                                    importo += CDbl(costo_new) * ((giorni_nolo - Rs!applicabilita_da) + 1)  'aggiornato 27.04.2023

                                Else
                                    If packed = True Then       'nel caso di packed 'l'importo totale è quello del packed
                                        importo = CDbl(costo_new) 'aggiornato 27.04.2023
                                    Else
                                        importo += CDbl(costo_new) * ((giorni_nolo - Rs!applicabilita_da) + 1) 'aggiornato 27.04.2023
                                    End If
                                End If
                            End If

                        End If

                    End If
                    ''end aggiunto 01.10.2022 salvo

                    If (Rs("applicabilita_da") = "0" And Rs("applicabilita_a") = "0") Or (giorni_nolo >= Rs("applicabilita_da") And giorni_nolo <= Rs("applicabilita_a")) Or (giorni_nolo >= Rs("applicabilita_da") And Rs("applicabilita_a") = "999") Then
                        'IN QUESTO CASO HO TROVATO LA CONDIZIONE. CONTROLLO SOLO SE E' PACKED O MENO

                        If Rs("applicabilita_a") = "999" Then 'nn può essere Packed  01.10.2022 salvo
                            ' packed = False
                        End If

                        If packed = True Then ' = "True" Then '01.10.2022 salvo

                            'Se packed rimane il calcolo precedente 01.10.2022 salvo

                            If Rs("tipo_costo") = "€" Then
                                costo = CDbl(costo_new) ' Rs("costo") aggiornato 27.04.203
                                percentuale = "NULL"
                            ElseIf Rs("tipo_costo") = "%" Then
                                costo = "NULL"
                                percentuale = CDbl(costo_new) 'Rs("costo") aggiornato 27.04.2023
                            End If

                            'se si tratta di deposito cauzionale/franchigie nn moltiplica  02.10.2022 salvo
                            If Rs!id_elemento = "283" Or Rs!id_elemento = "180" Or Rs!id_elemento = "181" _
                                        Or Rs!id_elemento = "203" Or Rs!id_elemento = "204" Then
                                costo = CDbl(costo_new) ' Rs!costo 'aggiornato 27.04.2023
                            End If

                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", Rs("nome_elemento") & "", costo, percentuale, Rs("iva"), Rs("codice_iva") & "", Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), selezionato, ordine_stampa, tipologia_franchigia, id_unita_misura, "", packed, qta, "", False, prepag)

                        Else

                            qta = giorni_nolo
                            If Rs("tipo_costo") = "€" Then

                                'ricalcolo con nuovi valori x giorni se >1  - 28.09.2022 salvo 
                                'If giorni_nolo > 1 Then
                                '    costo = funzioni_comuni_new.getValoreCondizioniNew(sqlStrCondizioni, giorni_nolo, Rs!id_elemento, CDbl(Rs!costo), "False")
                                'Else
                                '    costo = CDbl(Rs("costo")) * giorni_nolo    'originale
                                'End If

                                costo = importo 'aggiunto imposta dal calcolo dei giorni 01.10.2022 salvo

                                'se si tratta di deposito cauzionale/franchigie nn moltiplica  02.10.2022 salvo
                                If Rs!id_elemento = "283" Or Rs!id_elemento = "180" Or Rs!id_elemento = "181" _
                                    Or Rs!id_elemento = "203" Or Rs!id_elemento = "204" Then
                                    costo = CDbl(costo_new) ' Rs!costo aggiornato 27.04.2023
                                End If

                                percentuale = "NULL"

                            ElseIf Rs("tipo_costo") = "%" Then
                                costo = "NULL"
                                percentuale = CDbl(costo_new) * giorni_nolo 'aggiornato 27.04.2023
                            End If

                            'Tony 12-07-2023
                            If Rs("id_elemento") = "320" Then 'Rimborso spese di rientro passeggeri
                                selezionato = "1"
                            End If
                            'FINE Tony

                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", Rs("nome_elemento") & "", costo, percentuale, Rs("iva"), Rs("codice_iva") & "", Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), selezionato, ordine_stampa, tipologia_franchigia, id_unita_misura, "", packed, qta, "", False, prepag)
                        End If
                    End If



                ElseIf Rs("id_unita_misura") = Costanti.id_unita_misura_km_tra_stazioni Then
                    'CASO 3: UNITA' DI MISURA KM TRA LE STAZIONI. DEVO PRELEVARE IL DATO DALLA TABELLA
                    'stazioni_distanza. SE NON PRESENTE DEVO RESTITUIRE UN ERRORE.

                    'CONTROLLO SE C'E' UNA REGOLA DI APPLICABILITA'. 
                    Dim km_distanza_stazioni As Integer = getDistanzaStazioni(stazione_pick_up, stazione_drop_off)
                    If (Rs("applicabilita_da") = "0" And Rs("applicabilita_a") = "0") Or (km_distanza_stazioni >= Rs("applicabilita_da") And km_distanza_stazioni <= Rs("applicabilita_a")) Or (km_distanza_stazioni >= Rs("applicabilita_da") And Rs("applicabilita_a") = "999") Then
                        'IN QUESTO CASO HO TROVATO LA CONDIZIONE. CONTROLLO SOLO SE E' PACKED O MENO. NON RIESCO SICURAMENTE AD ENTRARE
                        'SE LA CONDIZIONE HA DEI LIMITI DI APPLICABILITA' E LA DISTANZA STAZIONI NON E' STATA TROVATA SU DB (OTTENGO IL VALORE
                        '-1). NEL CASO IN CUI LA CONDIZIONI E' SENZA LIMITI MA NON E' PACKED, QUINDI DIPENDE DALLA DISTANZA TRA
                        'LE STAZIONI, NON RESTITUISCO ERRORE IN QUANTO SI PROVERA' A CERCARE PER LA TARIFFA MADRE.
                        If Rs("pac") = "True" Then
                            If Rs("tipo_costo") = "€" Then
                                costo = CDbl(costo_new) ' Rs("costo") aggiornato 27.04.2023
                                percentuale = "NULL"
                            ElseIf Rs("tipo_costo") = "%" Then
                                costo = "NULL"
                                percentuale = CDbl(costo_new) 'Rs("costo") aggiornato 27.04.2023
                            End If
                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", Rs("nome_elemento") & "", costo, percentuale, Rs("iva"), Rs("codice_iva") & "", Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), selezionato, ordine_stampa, tipologia_franchigia, id_unita_misura, "", packed, qta, "", False, prepag)
                        ElseIf Rs("pac") = "False" And km_distanza_stazioni > 0 Then
                            If Rs("tipo_costo") = "€" Then
                                costo = CDbl(costo_new) * km_distanza_stazioni 'aggiornato 27.04.2023
                                percentuale = "NULL"
                            ElseIf Rs("tipo_costo") = "%" Then
                                costo = "NULL"
                                percentuale = CDbl(costo_new) * km_distanza_stazioni 'aggiornato 27.04.2023
                            End If
                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", Rs("nome_elemento") & "", costo, percentuale, Rs("iva"), Rs("codice_iva") & "", Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), selezionato, ordine_stampa, tipologia_franchigia, id_unita_misura, "", packed, qta, "", False, prepag)
                        End If
                    End If
                ElseIf (Rs("km_divisore_num_giorni") & "") <> "" Then
                    'IN QUESTO CASO E' UN UNITA' DI MISURA RIGUARDATE I KM EXTRA - IN FASE DI RIENTRO QUESTA INFORMAZIONE VIENE USATA PER IL CALCOLO E L'INFORMATIVA VIENE RIMOSSA 
                    'MANUALMENTE DALLA LISTA
                    'PER QUESTE UNITA' DI MISURA IL SOLO CAMPO applicabilita_da VIENE LETTO - INDICA DOPO QUALE VALORE DI KM VIENE AGGIUNTO IL COSTO
                    costo = CDbl(costo_new) 'Rs("costo") 'aggiornato 27.04.2023
                    percentuale = "NULL"
                    Dim km_giorno_inclusi As String = CInt(CInt(Rs("applicabilita_da")) / Rs("km_divisore_num_giorni"))

                    salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", Rs("nome_elemento") & " oltre " & Rs("applicabilita_da") & " " & Rs("unita_misura"), costo, percentuale, Rs("iva"), Rs("codice_iva") & "", Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), selezionato, ordine_stampa, tipologia_franchigia, id_unita_misura, km_giorno_inclusi, packed, qta, "", False, prepag)
                End If
            Loop

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error  analisi_condizioni : " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Sub calcolaOnere(ByVal giorni_noleggio As Integer, ByVal giorni_prepagati As Integer, ByVal prepagata As Boolean, ByVal elementi_prepagati As Collection,
                               ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal id_condizione_figlia As String, ByVal id_condizione_madre As String,
                               ByVal id_gruppo As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String,
                               ByVal id_contratto As String, ByVal num_calcolo As String)
        Dim sqlStr As String
        Try
            'PASSO 1: CONTROLLO NELLA TABELLA STAZIONI QUALE ONERE DEVE ESSERE PAGATO

            Dim valore_trovato As Boolean = False
            Dim costo As String
            Dim percentuale As String
            Dim id_unita_misura As String
            Dim packed As String
            Dim qta As String

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_onere FROM stazioni WITH(NOLOCK) WHERE id='" & stazione_pick_up & "'", Dbc)

            Dim id_onere As String = Cmd.ExecuteScalar

            Dim giorni_nolo As Integer = giorni_noleggio
            If prepagata AndAlso giorni_prepagati > giorni_noleggio AndAlso elementi_prepagati.Contains(id_onere) Then
                giorni_nolo = giorni_prepagati
            End If

            'PASSO 2: CERCO NELLA FIGLIA E NELLA MADRE IL COSTO DELL'ELEMENTO ONERE DA UTILIZZARE
            For i = 1 To 2
                If (i = 1) Or (i = 2 And Not valore_trovato And id_condizione_madre <> "0") Then 'SE TROVO IL VALORE ONERE PER LA CONDIZIONE FIGLIA NON LA CERCO PER LA MADRE
                    If i = 1 Then
                        sqlStr = "SELECT condizioni.iva_inclusa, condizioni_righe.id_metodo_stampa,condizioni_righe.obbligatorio,condizioni_elementi.descrizione As nome_onere, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, condizioni_elementi.scontabile, condizioni_elementi.omaggiabile, condizioni_elementi.acquistabile_nolo_in_corso, condizioni_righe.id, condizioni_x_gruppi.id_gruppo, condizioni_righe.applicabilita_da," &
                        "condizioni_righe.applicabilita_a, condizioni_righe.id_a_carico_di,condizioni_righe.pac, condizioni_righe.id_unita_misura," &
                        "condizioni_righe.costo, condizioni_righe.tipo_costo FROM condizioni_righe WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON condizioni_elementi.id=condizioni_righe.id_elemento INNER JOIN aliquote_iva WITH(NOLOCK) ON condizioni_elementi.id_aliquota_iva=aliquote_iva.id LEFT OUTER JOIN condizioni_x_gruppi WITH(NOLOCK) ON " &
                        "condizioni_x_gruppi.id_condizione=condizioni_righe.id INNER JOIN condizioni WITH(NOLOCK) ON condizioni_righe.id_condizione=condizioni.id WHERE (condizioni_righe.id_condizione='" & id_condizione_figlia & "') " &
                        "AND (condizioni_righe.id_elemento='" & id_onere & "') AND (condizioni_x_gruppi.id_gruppo='" & id_gruppo & "' OR " &
                        "condizioni_x_gruppi.id_gruppo IS NULL) ORDER BY condizioni_x_gruppi.id_gruppo DESC"
                    ElseIf i = 2 Then
                        sqlStr = "SELECT condizioni.iva_inclusa, condizioni_righe.id_metodo_stampa, condizioni_righe.obbligatorio, condizioni_elementi.descrizione As nome_onere, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva,  condizioni_elementi.scontabile,condizioni_elementi.omaggiabile,condizioni_elementi.acquistabile_nolo_in_corso, condizioni_righe.id, condizioni_x_gruppi.id_gruppo, condizioni_righe.applicabilita_da," &
                        "condizioni_righe.applicabilita_a, condizioni_righe.id_a_carico_di,condizioni_righe.pac, condizioni_righe.id_unita_misura," &
                        "condizioni_righe.costo, condizioni_righe.tipo_costo FROM condizioni_righe WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON condizioni_elementi.id=condizioni_righe.id_elemento INNER JOIN aliquote_iva WITH(NOLOCK) ON condizioni_elementi.id_aliquota_iva=aliquote_iva.id LEFT OUTER JOIN condizioni_x_gruppi WITH(NOLOCK) ON " &
                        "condizioni_x_gruppi.id_condizione=condizioni_righe.id INNER JOIN condizioni WITH(NOLOCK) ON condizioni_righe.id_condizione=condizioni.id WHERE (condizioni_righe.id_condizione='" & id_condizione_madre & "') " &
                        "AND (condizioni_righe.id_elemento='" & id_onere & "') AND (condizioni_x_gruppi.id_gruppo='" & id_gruppo & "' OR " &
                        "condizioni_x_gruppi.id_gruppo IS NULL) ORDER BY condizioni_x_gruppi.id_gruppo DESC"
                    End If

                    Dbc.Close()
                    Dbc.Open()

                    Dim Rs As Data.SqlClient.SqlDataReader

                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                    Rs = Cmd.ExecuteReader()

                    Do While Rs.Read()
                        qta = "1" 'LA QUANTITA' SARA' SEMPRE 1 TRANNE NEL CASO DI COSTO GIORNALIERO NON PACKED

                        id_unita_misura = Rs("id_unita_misura")

                        packed = Rs("pac")

                        Dim ordine_stampa As Integer
                        If (Rs("id_a_carico_di") & "") = Costanti.id_accessorio_incluso Then
                            ordine_stampa = 2
                        Else
                            ordine_stampa = 3
                        End If

                        'CONTROLLO A MENO CHE NON HO TROVATO GIA' UN VALORE
                        'INFATTI DALLA QUERY AVRO' IN TESTA LE RIGHE DI CONDIZIONE CON ID_GRUPPO SPECIFICATO ED IN CODA QUELLE SENZA GRUPPO
                        If Not valore_trovato Then
                            If Rs("id_unita_misura") = "0" Then
                                'CASO 1: NESSUNA UNITA' DI MISURA: IN QUESTO CASO NON MI INTERESSA IL CASO "PACKED" - LA CONDIZIONE E' PER FORZA
                                'SENZA LIMITE (NON CONTROLLO I GIORNI DI NOLEGGIO O I KM)
                                If (Rs("id_gruppo") & "") = "" Or (Rs("id_gruppo") & "") = id_gruppo Then
                                    If Rs("tipo_costo") = "€" Then
                                        costo = Rs("costo")
                                        percentuale = "NULL"
                                    ElseIf Rs("tipo_costo") = "%" Then
                                        costo = "NULL"
                                        percentuale = Rs("costo")
                                    Else
                                        'CASO incluso senza valore
                                        costo = "0"
                                        percentuale = "NULL"
                                    End If
                                    salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, id_onere, "", Rs("nome_onere") & "", costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, prepagata)
                                    valore_trovato = True
                                End If
                            ElseIf Rs("id_unita_misura") = Costanti.id_unita_misura_giorni Then
                                'CASO 2: UNITA' DI MISURA GIORNI. CONTROLLO SE IL LIMITE (GIORNI DI NOLEGGIO) E' RISPETTATO O SE LA RIGA 
                                'E' SENZA ALCUN LIMITE (SE E' SEMPRE APPLICABILE AVRO' 0 SIA IN applicabilita_da CHE IN pplicabilita_a)
                                'IN OGNI CASO SE LA CONDIZIONE E' PACKED VIENE PRESO IL VALORE IN TOTO, SE NON E' PACKED MOLTIPLICO IL VALORE
                                'PER I GIORNI DI NOLEGGIO
                                If (Rs("id_gruppo") & "") = "" Or (Rs("id_gruppo") & "") = id_gruppo Then
                                    'CONTROLLO SE C'E' UNA REGOLA DI APPLICABILITA'. 
                                    If (Rs("applicabilita_da") = "0" And Rs("applicabilita_a") = "0") Or (giorni_nolo >= Rs("applicabilita_da") And giorni_nolo <= Rs("applicabilita_a")) Or (giorni_nolo >= Rs("applicabilita_da") And Rs("applicabilita_a") = "999") Then
                                        'IN QUESTO CASO HO TROVATO LA CONDIZIONE. CONTROLLO SOLO SE E' PACKED O MENO
                                        If Rs("pac") = "True" Then
                                            If Rs("tipo_costo") = "€" Then
                                                costo = Rs("costo")
                                                percentuale = "NULL"
                                            ElseIf Rs("tipo_costo") = "%" Then
                                                costo = "NULL"
                                                percentuale = Rs("costo")
                                            End If
                                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, id_onere, "", Rs("nome_onere") & "", costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, prepagata)
                                            valore_trovato = True
                                        Else
                                            qta = giorni_nolo
                                            If Rs("tipo_costo") = "€" Then
                                                costo = CDbl(Rs("costo")) * giorni_nolo
                                                percentuale = "NULL"
                                            ElseIf Rs("tipo_costo") = "%" Then
                                                costo = "NULL"
                                                percentuale = CDbl(Rs("costo")) * giorni_nolo
                                            End If
                                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, id_onere, "", Rs("nome_onere") & "", costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, prepagata)
                                            valore_trovato = True
                                        End If
                                    End If
                                End If
                            ElseIf Rs("id_unita_misura") = Costanti.id_unita_misura_km_tra_stazioni Then
                                'CASO 3: UNITA' DI MISURA KM TRA LE STAZIONI. DEVO PRELEVARE IL DATO DALLA TABELLA
                                'stazioni_distanza. SE NON PRESENTE DEVO RESTITUIRE UN ERRORE.
                                If (Rs("id_gruppo") & "") = "" Or (Rs("id_gruppo") & "") = id_gruppo Then
                                    'CONTROLLO SE C'E' UNA REGOLA DI APPLICABILITA'. 
                                    Dim km_distanza_stazioni As Integer = getDistanzaStazioni(stazione_pick_up, stazione_drop_off)
                                    If (Rs("applicabilita_da") = "0" And Rs("applicabilita_a") = "0") Or (km_distanza_stazioni >= Rs("applicabilita_da") And km_distanza_stazioni <= Rs("applicabilita_a")) Or (km_distanza_stazioni >= Rs("applicabilita_da") And Rs("applicabilita_a") = "999") Then
                                        'IN QUESTO CASO HO TROVATO LA CONDIZIONE. CONTROLLO SOLO SE E' PACKED O MENO. NON RIESCO SICURAMENTE AD ENTRARE
                                        'SE LA CONDIZIONE HA DEI LIMITI DI APPLICABILITA' E LA DISTANZA STAZIONI NON E' STATA TROVATA SU DB (OTTENGO IL VALORE
                                        '-1). NEL CASO IN CUI LA CONDIZIONI E' SENZA LIMITI MA NON E' PACKED, QUINDI DIPENDE DALLA DISTANZA TRA
                                        'LE STAZIONI, NON RESTITUISCO ERRORE IN QUANTO SI PROVERA' A CERCARE PER LA TARIFFA MADRE.
                                        If Rs("pac") = "True" Then
                                            If Rs("tipo_costo") = "€" Then
                                                costo = Rs("costo")
                                                percentuale = "NULL"
                                            ElseIf Rs("tipo_costo") = "%" Then
                                                costo = "NULL"
                                                percentuale = Rs("costo")
                                            End If
                                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, id_onere, "", Rs("nome_onere") & "", costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, prepagata)
                                            valore_trovato = True
                                        ElseIf Rs("pac") = "False" And km_distanza_stazioni > 0 Then
                                            If Rs("tipo_costo") = "€" Then
                                                costo = CDbl(Rs("costo")) * km_distanza_stazioni
                                                percentuale = "NULL"
                                            ElseIf Rs("tipo_costo") = "%" Then
                                                costo = "NULL"
                                                percentuale = CDbl(Rs("costo")) * km_distanza_stazioni
                                            End If
                                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, id_onere, "", Rs("nome_onere") & "", costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, prepagata)
                                            valore_trovato = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Loop
                End If
            Next

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception

            HttpContext.Current.Response.Write("error  calcolaOnere() : " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Sub

    Protected Sub calcolaVAL(ByVal giorni_noleggio As Integer, ByVal giorni_prepagati As Integer, ByVal prepagata As Boolean, ByVal elementi_prepagati As Collection, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal id_condizione_figlia As String, ByVal id_condizione_madre As String, ByVal id_gruppo As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String)
        Dim sqlStr As String
        Try
            'LA FUNZIONE SALVA IN preventivi_costi LA RIGA DEL VALORE DEL VAL
            Dim costo As String
            Dim percentuale As String
            Dim id_unita_misura As String
            Dim packed As String
            Dim qta As String

            'PASSO 1: CONTROLLO SE IL VAL DEVE ESSERE EFFETTIVAMENTE PAGATO. QUESTO VIENE FATTO NELLA FUNZIONALITA' 'DISTANZA/VAL' DENTRO
            'GESTIONE STAZIONI. LA TABELLA E' stazioni_distanza DOVE ESISTE L'INFORMAZIONI distanza TRA LE DUE STAZIONI E SE val_gratis
            'SE NON TROVO LA RIGA O IL VAL NON E' GRATIS IL VAL DEVE ESSERE CALCOLATO
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT val_gratis FROM stazioni_distanza WITH(NOLOCK) WHERE ((id_stazione1='" & stazione_pick_up & "' AND id_stazione2='" & stazione_drop_off & "') OR (id_stazione1='" & stazione_drop_off & "' AND id_stazione2='" & stazione_pick_up & "'))", Dbc)

            Dim val_gratis As String = Cmd.ExecuteScalar

            If val_gratis = "True" Then
                'IN QUESTO CASO IL VAL E' GRATUITO
                'salvaRigaCalcolo(id_preventivo, num_calcolo, id_gruppo, "NULL", "VAL", "0", "NULL", "NULL", "NULL", "", "1", "", 2)
            Else
                'IN QUESTO CASO DEVO EFFETTUARE IL CALCOLO DEL VAL (NESSUNA RIGA NON TROVATA OPPURE HO TROVATO VAL NON GRATUITO)
                'PASSO 2 : DALLA CONDIZIONE ASSOCIATA ALLA RIGA TARIFFA SELEZIONO IL VAL_ACCESSORIO DA UTILIZZARE
                Cmd = New Data.SqlClient.SqlCommand("SELECT condizioni_elementi.id FROM condizioni WITH(NOLOCK) INNER JOIN val_template WITH(NOLOCK) ON condizioni.id_template_val=val_template.id INNER JOIN val_template_righe WITH(NOLOCK) ON val_template_righe.id_val_template=val_template.id INNER JOIN condizioni_elementi WITH(NOLOCK) ON condizioni_elementi.id=val_template_righe.id_accessori_val WHERE condizioni.id='" & id_condizione_figlia & "' AND ((id_stazione1='" & stazione_pick_up & "' AND id_stazione2='" & stazione_drop_off & "') OR (id_stazione1='" & stazione_drop_off & "' AND id_stazione2='" & stazione_pick_up & "'))", Dbc)

                Dim id_elemento_val_figlia As String = Cmd.ExecuteScalar & ""
                Dim id_elemento_val_madre As String = ""

                If id_elemento_val_figlia = "" And id_condizione_madre <> "0" Then
                    'SE NON HO TROVATO L'ELEMENTO VAL CONTROLLO NELLA MADRE
                    Cmd = New Data.SqlClient.SqlCommand("SELECT condizioni_elementi.id FROM condizioni WITH(NOLOCK) INNER JOIN val_template WITH(NOLOCK) ON condizioni.id_template_val=val_template.id INNER JOIN val_template_righe WITH(NOLOCK) ON val_template_righe.id_val_template=val_template.id INNER JOIN condizioni_elementi WITH(NOLOCK) ON condizioni_elementi.id=val_template_righe.id_accessori_val WHERE condizioni.id='" & id_condizione_madre & "' AND ((id_stazione1='" & stazione_pick_up & "' AND id_stazione2='" & stazione_drop_off & "') OR (id_stazione1='" & stazione_drop_off & "' AND id_stazione2='" & stazione_pick_up & "'))", Dbc)
                    id_elemento_val_madre = Cmd.ExecuteScalar & ""
                End If

                If id_elemento_val_figlia <> "" Or id_elemento_val_madre <> "" Then
                    'PASSO 3: SELEZIONO LE RIGHE DI CONDIZIONI ASSOCIATE ALL'ELEMENTO VAL ATTUALMENTE CONSIDERATO IN MODO DA POTERLE ANALIZZARE
                    'SIA CHE L'ELEMENTO VAL L'HO TROVATO DALLA FIGLIA CHE DALLA MADRE - PER PRIMA COSA CERCO UN VALORE DALLA CONDIZIONE DELLA
                    'TARIFFA FIGLIA, SE NON LO TROVO LO CERCO NELLA MADRE
                    Dim id_elemento_val As String = id_elemento_val_figlia & id_elemento_val_madre 'SOLO UNO DEI DUE SARA' VALORIZZATO

                    Dim valore_trovato As Boolean = False

                    Dim giorni_nolo As Integer = giorni_noleggio
                    Dim elemento_prepagato As Boolean

                    'SE SIAMO IN PRIMO CALCOLO PER UNA PRENOTAZIONE PREPAGATA O SE LA PRENOTAZIONE E' PREPAGATA E L'ACCESSORIO E' PREPAGATO
                    If (prepagata And giorni_prepagati = 0) OrElse (prepagata AndAlso elementi_prepagati.Contains(id_elemento_val)) Then
                        elemento_prepagato = True
                    Else
                        elemento_prepagato = False
                    End If

                    If prepagata AndAlso giorni_prepagati > giorni_noleggio AndAlso elemento_prepagato Then
                        giorni_nolo = giorni_prepagati
                    End If



                    'LA RICERCA VIENE ESEGUITA AL MAX DUE VOLTE: LA PRIMA SULLA CONDIZIONE FIGLIA E SE NON HO TROVATO NULLA SULLA MADRE
                    For i = 1 To 2
                        If (i = 1) Or (i = 2 And Not valore_trovato And id_condizione_madre <> "0") Then 'SE TROVO IL VALORE VAL PER LA CONDIZIONE FIGLIA NON LA CERCO PER LA MADRE
                            If i = 1 Then
                                sqlStr = "SELECT condizioni.iva_inclusa, condizioni_righe.id_metodo_stampa,condizioni_righe.obbligatorio,condizioni_elementi.descrizione As nome_val, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, condizioni_elementi.scontabile, condizioni_elementi.omaggiabile, condizioni_elementi.acquistabile_nolo_in_corso, condizioni_righe.id, condizioni_x_gruppi.id_gruppo, condizioni_righe.applicabilita_da," &
                                "condizioni_righe.applicabilita_a, condizioni_righe.id_a_carico_di,condizioni_righe.pac, condizioni_righe.id_unita_misura," &
                                "condizioni_righe.costo, condizioni_righe.tipo_costo FROM condizioni_righe WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON condizioni_elementi.id=condizioni_righe.id_elemento_val INNER JOIN aliquote_iva WITH(NOLOCK) ON condizioni_elementi.id_aliquota_iva=aliquote_iva.id LEFT OUTER JOIN condizioni_x_gruppi WITH(NOLOCK) ON " &
                                "condizioni_x_gruppi.id_condizione=condizioni_righe.id INNER JOIN condizioni WITH(NOLOCK) ON condizioni_righe.id_condizione=condizioni.id WHERE (condizioni_righe.id_condizione='" & id_condizione_figlia & "') " &
                                "AND (condizioni_righe.id_elemento_val='" & id_elemento_val & "') AND (condizioni_x_gruppi.id_gruppo='" & id_gruppo & "' OR " &
                                "condizioni_x_gruppi.id_gruppo IS NULL) ORDER BY condizioni_x_gruppi.id_gruppo DESC"
                            ElseIf i = 2 Then
                                sqlStr = "SELECT condizioni.iva_inclusa, condizioni_righe.id_metodo_stampa, condizioni_righe.obbligatorio, condizioni_elementi.descrizione As nome_val, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, condizioni_elementi.scontabile,condizioni_elementi.omaggiabile, condizioni_elementi.acquistabile_nolo_in_corso, condizioni_righe.id, condizioni_x_gruppi.id_gruppo, condizioni_righe.applicabilita_da," &
                                "condizioni_righe.applicabilita_a, condizioni_righe.id_a_carico_di,condizioni_righe.pac, condizioni_righe.id_unita_misura," &
                                "condizioni_righe.costo, condizioni_righe.tipo_costo FROM condizioni_righe WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON condizioni_elementi.id=condizioni_righe.id_elemento_val INNER JOIN aliquote_iva WITH(NOLOCK) ON condizioni_elementi.id_aliquota_iva=aliquote_iva.id LEFT OUTER JOIN condizioni_x_gruppi WITH(NOLOCK) ON " &
                                "condizioni_x_gruppi.id_condizione=condizioni_righe.id INNER JOIN condizioni WITH(NOLOCK) ON condizioni_righe.id_condizione=condizioni.id WHERE (condizioni_righe.id_condizione='" & id_condizione_madre & "') " &
                                "AND (condizioni_righe.id_elemento_val='" & id_elemento_val & "') AND (condizioni_x_gruppi.id_gruppo='" & id_gruppo & "' OR " &
                                "condizioni_x_gruppi.id_gruppo IS NULL) ORDER BY condizioni_x_gruppi.id_gruppo DESC"
                            End If

                            Dim Rs As Data.SqlClient.SqlDataReader

                            Dbc.Close()
                            Dbc.Open()
                            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                            Rs = Cmd.ExecuteReader()

                            Do While Rs.Read()
                                qta = "1"  'LA QUANTITA' SARA' SEMPRE 1 TRANNE NEL CASO DI CALCOLO AL GIORNO

                                'ORDINE DI STAMPA: IL VAL PUO' ESSERE O INCLUSO (2) O NON INCLUSO MA OBBLIGATORIO (3)
                                Dim ordine_stampa As Integer

                                If (Rs("id_a_carico_di") & "") = Costanti.id_accessorio_incluso Then
                                    ordine_stampa = 2
                                Else
                                    ordine_stampa = 3
                                End If

                                id_unita_misura = Rs("id_unita_misura")

                                packed = Rs("pac")

                                If id_elemento_val = "125" Then
                                    id_elemento_val = id_elemento_val
                                End If





                                'CONTROLLO A MENO CHE NON HO TROVATO GIA' UN VALORE
                                'INFATTI DALLA QUERY AVRO' IN TESTA LE RIGHE DI CONDIZIONE CON ID_GRUPPO SPECIFICATO ED IN CODA QUELLE SENZA GRUPPO
                                If Not valore_trovato Then
                                    If Rs("id_unita_misura") = "0" Then
                                        'CASO 1: NESSUNA UNITA' DI MISURA: IN QUESTO CASO NON MI INTERESSA IL CASO "PACKED" - LA CONDIZIONE E' PER FORZA
                                        'SENZA LIMITE (NON CONTROLLO I GIORNI DI NOLEGGIO O I KM)
                                        If (Rs("id_gruppo") & "") = "" Or (Rs("id_gruppo") & "") = id_gruppo Then
                                            If Rs("tipo_costo") = "€" Then
                                                costo = Rs("costo")
                                                percentuale = "NULL"
                                            ElseIf Rs("tipo_costo") = "%" Then
                                                costo = "NULL"
                                                percentuale = Rs("costo")
                                            Else
                                                'CASO incluso senza valore
                                                costo = "0"
                                                percentuale = "NULL"
                                            End If
                                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, id_elemento_val, "", Rs("nome_val") & "", costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, elemento_prepagato)
                                            valore_trovato = True
                                        End If
                                    ElseIf Rs("id_unita_misura") = Costanti.id_unita_misura_giorni Then
                                        'CASO 2: UNITA' DI MISURA GIORNI. CONTROLLO SE IL LIMITE (GIORNI DI NOLEGGIO) E' RISPETTATO O SE LA RIGA 
                                        'E' SENZA ALCUN LIMITE (SE E' SEMPRE APPLICABILE AVRO' 0 SIA IN applicabilita_da CHE IN pplicabilita_a)
                                        'IN OGNI CASO SE LA CONDIZIONE E' PACKED VIENE PRESO IL VALORE IN TOTO, SE NON E' PACKED MOLTIPLICO IL VALORE
                                        'PER I GIORNI DI NOLEGGIO
                                        If (Rs("id_gruppo") & "") = "" Or (Rs("id_gruppo") & "") = id_gruppo Then
                                            'CONTROLLO SE C'E' UNA REGOLA DI APPLICABILITA'. 
                                            If (Rs("applicabilita_da") = "0" And Rs("applicabilita_a") = "0") Or (giorni_nolo >= Rs("applicabilita_da") And giorni_nolo <= Rs("applicabilita_a")) Or (giorni_nolo >= Rs("applicabilita_da") And Rs("applicabilita_a") = "999") Then
                                                'IN QUESTO CASO HO TROVATO LA CONDIZIONE. CONTROLLO SOLO SE E' PACKED O MENO
                                                If Rs("pac") = "True" Then
                                                    If Rs("tipo_costo") = "€" Then
                                                        costo = Rs("costo")
                                                        percentuale = "NULL"
                                                    ElseIf Rs("tipo_costo") = "%" Then
                                                        costo = "NULL"
                                                        percentuale = Rs("costo")
                                                    End If
                                                    'HttpContext.Current.Trace.Write("id_preventivo " & id_preventivo & "; id_ribaltamento " & id_ribaltamento & "; id_prenotazione " & _
                                                    '                   id_prenotazione & "; id_contratto " & id_contratto & "; num_calcolo " & num_calcolo & "; id_gruppo " & id_gruppo & _
                                                    '                   "; id_elemento_val " & id_elemento_val & "; Rs(nome_val) " & Rs("nome_val") & "; costo " & costo & "; percentuale " & percentuale & _
                                                    '                   "; Rs(iva) " & Rs("iva") & "; Rs(codice_iva) " & Rs("codice_iva") & "; Rs(iva_inclusa) " & Rs("iva_inclusa") & _
                                                    '                   "; Rs(scontabile) " & Rs("scontabile") & "; Rs(omaggiabile) " & Rs("omaggiabile") & "; Rs(acquistabile_nolo_in_corso) " & Rs("acquistabile_nolo_in_corso") & _
                                                    '                   "; Rs(id_a_carico_di) " & Rs("id_a_carico_di") & "; Rs(id_metodo_stampa) " & Rs("id_metodo_stampa") & _
                                                    '                   "; Rs(obbligatorio) " & Rs("obbligatorio"))
                                                    salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, id_elemento_val, "", Rs("nome_val") & "", costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, elemento_prepagato)
                                                    valore_trovato = True
                                                Else
                                                    qta = giorni_nolo
                                                    If Rs("tipo_costo") = "€" Then
                                                        costo = CDbl(Rs("costo")) * giorni_nolo
                                                        percentuale = "NULL"
                                                    ElseIf Rs("tipo_costo") = "%" Then
                                                        costo = "NULL"
                                                        percentuale = CDbl(Rs("costo")) * giorni_nolo
                                                    End If
                                                    salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, id_elemento_val, "", Rs("nome_val") & "", costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, elemento_prepagato)
                                                    valore_trovato = True
                                                End If
                                            End If
                                        End If
                                    ElseIf Rs("id_unita_misura") = Costanti.id_unita_misura_km_tra_stazioni Then
                                        'CASO 3: UNITA' DI MISURA KM TRA LE STAZIONI. DEVO PRELEVARE IL DATO DALLA TABELLA
                                        'stazioni_distanza. SE NON PRESENTE DEVO RESTITUIRE UN ERRORE.
                                        If (Rs("id_gruppo") & "") = "" Or (Rs("id_gruppo") & "") = id_gruppo Then
                                            'CONTROLLO SE C'E' UNA REGOLA DI APPLICABILITA'. 
                                            Dim km_distanza_stazioni As Integer = getDistanzaStazioni(stazione_pick_up, stazione_drop_off)
                                            If (Rs("applicabilita_da") = "0" And Rs("applicabilita_a") = "0") Or (km_distanza_stazioni >= Rs("applicabilita_da") And km_distanza_stazioni <= Rs("applicabilita_a")) Or (km_distanza_stazioni >= Rs("applicabilita_da") And Rs("applicabilita_a") = "999") Then
                                                'IN QUESTO CASO HO TROVATO LA CONDIZIONE. CONTROLLO SOLO SE E' PACKED O MENO. NON RIESCO SICURAMENTE AD ENTRARE
                                                'SE LA CONDIZIONE HA DEI LIMITI DI APPLICABILITA' E LA DISTANZA STAZIONI NON E' STATA TROVATA SU DB (OTTENGO IL VALORE
                                                '-1). NEL CASO IN CUI LA CONDIZIONI E' SENZA LIMITI MA NON E' PACKED, QUINDI DIPENDE DALLA DISTANZA TRA
                                                'LE STAZIONI, NON RESTITUISCO ERRORE IN QUANTO SI PROVERA' A CERCARE PER LA TARIFFA MADRE.
                                                If Rs("pac") = "True" Then
                                                    If Rs("tipo_costo") = "€" Then
                                                        costo = Rs("costo")
                                                        percentuale = "NULL"
                                                    ElseIf Rs("tipo_costo") = "%" Then
                                                        costo = "NULL"
                                                        percentuale = Rs("costo")
                                                    End If
                                                    salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, id_elemento_val, "", Rs("nome_val") & "", costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, elemento_prepagato)
                                                    valore_trovato = True
                                                ElseIf Rs("pac") = "False" And km_distanza_stazioni > 0 Then
                                                    If Rs("tipo_costo") = "€" Then
                                                        costo = CDbl(Rs("costo")) * km_distanza_stazioni
                                                        percentuale = "NULL"
                                                    ElseIf Rs("tipo_costo") = "%" Then
                                                        costo = "NULL"
                                                        percentuale = CDbl(Rs("costo")) * km_distanza_stazioni
                                                    End If
                                                    salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, id_elemento_val, "", Rs("nome_val") & "", costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, elemento_prepagato)
                                                    valore_trovato = True
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            Loop
                        End If
                    Next
                    If Not valore_trovato Then
                        'ALLA FINE DI TUTTO SE NON SONO RIUSCITO A TROVARE NULLA NE DALLA MADRE NE DALLA FIGLIA SALVO UNA RIGA DI ERRORE
                        salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, "NULL", "", "VAL - ERRORE - ELEMENTO VAL NON TROVATO", "0", "NULL", "NULL", "NULL", "False", "NULL", "NULL", "NULL", "NULL", "1", "", "0", 3, "NULL", "NULL", "", "NULL", "0", "", False, False)
                    End If
                Else
                    'IN QUESTO CASO NE PER LA MADRE NE PER LA FIGLIA HO TROVATO UN ELEMENTO VAL
                    salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, "NULL", "", "VAL - ERRORE - ELEMENTO VAL NON TROVATO", "0", "NULL", "NULL", "NULL", "False", "NULL", "NULL", "NULL", "NULL", "1", "", "0", 3, "NULL", "NULL", "", "NULL", "0", "", False, False)
                End If

            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception

            HttpContext.Current.Response.Write("error  calcolaVAL() : " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    'EDIT: LA DATA DI VENDIBILITA' NON BASTA PIU' PER CERCARE LA TARIFFA MADRE, ORA SERVE ANCHE LA DATA DI PICK UP IN QUANTO CI POSSONO ESSERE
    'SOVRAPPOSIZIONI DI DATA DI VENDIBILITA' FINCHE' QUELLE DI PICK UP NON SI INCROCINO.
    'Public Function getIdTariffeRigheMadre(ByVal id_tariffe_righe_figlia As String) As String
    '    'DATO L'ID DI TARIFFE RIGHE DELLA TARIFFA SCELTA SELEZIONO ID_TARIFFA_MADRE (SE SPECIFICATA) E DA QUESTA ID_TARIFFE_RIGHE DELLA
    '    'TARIFFA MADRE. LA RIGA DELLA TARIFFA MADRE SI TROVA CERCANDO TRA IL RANGE DI VENDIBILITA' E QUELLO DI PICK UP E IGNORANDO EVENTUALI
    '    'RESTRIZIONI DI STAZIONI/FONTI/DATA DI PICK-UP/MAX-DATA-DROP-OFF DELLA MADRE
    '    'LA FUNZIONE RESTITUISCE 0 QUALORA LA TARIFFA SCELTA NON HA MADRE OPPURE NON SI RIESCE A TROVARE UN'ID_TARIFFE_RIGHE DELLA MADRE
    '    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '    Dbc.Open()
    '    Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_tariffa_madre FROM tariffe_righe WHERE id='" & id_tariffe_righe_figlia & "'", Dbc)

    '    Dim id_tarriffa_madre As String = Cmd.ExecuteScalar & ""

    '    If id_tarriffa_madre <> "" Then
    '        Cmd = New Data.SqlClient.SqlCommand("SELECT tariffe_righe.id FROM tariffe_righe INNER JOIN tariffe ON tariffe.id=tariffe_righe.id_tariffa WHERE tariffe.id='" & id_tarriffa_madre & "' AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a", Dbc)
    '        Dim id_tariffe_righe_madre As String = Cmd.ExecuteScalar & ""

    '        If id_tariffe_righe_madre <> "" Then
    '            getIdTariffeRigheMadre = id_tariffe_righe_madre
    '        Else
    '            'LA TARIFFA MADRE NON E' VENDIBILE PER IL GIORNO ATTUALE
    '            getIdTariffeRigheMadre = "0"
    '        End If
    '    Else
    '        'NESSUNA TARIFFA MADRE (LA TARIFFA SCELTA E' GIA' UNA TARIFFA MADRE)
    '        getIdTariffeRigheMadre = "0"
    '    End If

    '    Cmd.Dispose()
    '    Cmd = Nothing
    '    Dbc.Close()
    '    Dbc.Dispose()
    '    Dbc = Nothing
    'End Function

    Public Function getIdTariffeRigheTariffaRack(ByVal id_tariffe_righe_figlia As String, ByVal data_pick_up As String, ByVal provenienza_per_data As String) As String
        'DATO L'ID DI TARIFFE RIGHE DELLA TARIFFA SCELTA SELEZIONO ID_TARIFFA_RACK (SE SPECIFICATA) E DA QUESTA ID_TARIFFE_RIGHE DELLA
        'TARIFFA RACK. LA RIGA DELLA TARIFFA RACK SI TROVA CERCANDO TRA IL RANGE DI VENDIBILITA' E QUELLO DI PICK UP E IGNORANDO EVENTUALI
        'RESTRIZIONI DI STAZIONI/FONTI/DATA DI PICK-UP/MAX-DATA-DROP-OFF DELLA MADRE
        'LA FUNZIONE RESTITUISCE 0 QUALORA LA TARIFFA SCELTA NON HA TARIFFA RACK OPPURE NON SI RIESCE A TROVARE UN'ID_TARIFFE_RIGHE DELLA MADRE

        Dim pick_up As String = getDataDb_senza_orario(data_pick_up, provenienza_per_data)

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_tariffa_madre FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe_figlia & "'", Dbc)

        Dim id_tarriffa_madre As String = Cmd.ExecuteScalar & ""

        If id_tarriffa_madre <> "" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT tariffe_righe.id FROM tariffe_righe WITH(NOLOCK) INNER JOIN tariffe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa WHERE tariffe.id='" & id_tarriffa_madre & "' AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a AND '" & pick_up & "' BETWEEN pickup_da AND pickup_a", Dbc)
            Dim id_tariffe_righe_madre As String = Cmd.ExecuteScalar & ""

            If id_tariffe_righe_madre <> "" Then
                getIdTariffeRigheTariffaRack = id_tariffe_righe_madre
            Else
                'LA TARIFFA MADRE NON E' VENDIBILE PER IL GIORNO ATTUALE
                getIdTariffeRigheTariffaRack = "0"
            End If
        Else
            'NESSUNA TARIFFA MADRE (LA TARIFFA SCELTA E' GIA' UNA TARIFFA MADRE)
            getIdTariffeRigheTariffaRack = "0"
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function getIdCondizione(ByVal id_tariffe_righe As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_condizione FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)

        getIdCondizione = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function getIdCondizioneMadre(ByVal id_tariffe_righe As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(id_condizione_madre,'0') FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)

        getIdCondizioneMadre = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function getIdTempoKm(ByVal id_tariffe_righe As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_tempo_km FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)

        getIdTempoKm = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function getIdValGps() As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM condizioni_elementi WHERE tipologia='VAL_GPS'", Dbc)

        getIdValGps = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function getIdTempoKmRack(ByVal id_tariffe_righe As String, ByVal id_contratto As String, ByVal id_prenotazione As String) As String
        'SE PER IL CONTRATTO O PRENOTAZIONE SPECIFICATO LA TARIFFA RACK E' STATA GIA' UTILIZZATA ALLORA QUESTA E' FISSATA E LA SELEZIONO
        'ALTRIMENTI LA RICERCO ALL'INTERNO DELLA TARIFFA RACK SPECIFICATA IN TARIFFE RIGHE
        'SE QUESTA VIENE TROVATA VIENE SUBITO SALVATA NELLA RIGA DI CONTRATTO/PRENOTAZIONE. SE IL DOCUMENTO VIENE CONFERMATO CON SALVATAGGIO
        'DA PARTE DELL'UTENTE, LA RACK TROVATA VERRA' UTILIZZATA PER TUTTE LE SUCCESSIVE MODIFICHE.
        Dim id_tempo_km As String
        Dim id_tariffa_rack As String
        Dim SqlStr As String

        If id_contratto <> "" Then
            SqlStr = "SELECT id_tempo_km_rack FROM contratti WITH(NOLOCK) WHERE id='" & id_contratto & "'"
        ElseIf id_prenotazione <> "" Then
            SqlStr = "SELECT id_tempo_km_rack FROM prenotazioni WITH(NOLOCK) WHERE Nr_Pren='" & id_prenotazione & "'"
        End If

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand(SqlStr, Dbc)

        id_tempo_km = Cmd.ExecuteScalar & ""

        If id_tempo_km <> "" Then
            getIdTempoKmRack = id_tempo_km
        Else
            Cmd = New Data.SqlClient.SqlCommand("SELECT id_tariffa_rack FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)
            id_tariffa_rack = Cmd.ExecuteScalar & ""
            If id_tariffa_rack <> "" Then
                SqlStr = "SELECT tariffe_righe.id_tempo_km FROM tariffe_righe WITH(NOLOCK) " &
                    "WHERE tariffe_righe.id_tariffa='" & id_tariffa_rack & "'  AND GetDate() BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " &
                    "AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a "

                Cmd = New Data.SqlClient.SqlCommand(SqlStr, Dbc)
                id_tempo_km = Cmd.ExecuteScalar & ""
                getIdTempoKmRack = id_tempo_km

                'SALVATAGGIO - SE LA RACK NON E' STATA UTILIZZATA COSA FARE? IN QUESTO MOMENTO NON LA UTILIZZO NEMMENO PER I CALCOLI SUCCESSIVI
                'SALVANDO 0 - RIMUOVERE LE 3 RIGHE SICCESSIVE SE SI VUOLE CHE PER I CALCOLI SUCCESSIVI LA RACK VENGA NUOVAMENTE CERCATA
                If id_tempo_km = "" Then
                    id_tempo_km = "0"
                End If

                If id_contratto <> "" Then
                    SqlStr = "UPDATE contratti SET id_tempo_km_rack='" & id_tempo_km & "' WHERE id='" & id_contratto & "'"
                ElseIf id_prenotazione <> "" Then
                    SqlStr = "UPDATE prenotazioni SET id_tempo_km_rack='" & id_tempo_km & "' WHERE Nr_Pren='" & id_prenotazione & "'"
                End If
                Cmd = New Data.SqlClient.SqlCommand(SqlStr, Dbc)
                Cmd.ExecuteNonQuery()
            Else
                getIdTempoKmRack = ""
            End If
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    '25/10/2018 - VECCHIA VERSIONE PRIMA DELLA MODIFICA CALCOLO GIORNI EXTRA A NOLO IN CORSO
    'Protected Sub calcola_tempo_km(ByVal giorni_noleggio As Integer, ByVal giorni_prepagati As Integer, ByVal prepagata As Boolean, ByVal giorni_noleggio_extra_rack As Integer, ByVal sconto_su_rack As String, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal id_tempo_km_figlia As String, ByVal id_tempo_km_rack As String, ByVal id_gruppo As String, ByVal id_gruppo_da_prenotazione_x_modifica_con_rack As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String)
    '    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '    Dbc.Open()
    '    Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

    '    Dim valore_trovato As Boolean = False
    '    'SE VIENE PASSATO IL PARAMENTRO id_gruppo_da_prenotazione_x_modifica_con_rack DEVO CALCOLARE, CON LA TARIFFA INIZIALE, IL COSTO DEL TEMPO
    '    'KM CON L'ID_GRUPPO_DA_PRENOTAZIONE E NON COL NUOVO GRUPPO: IL COSTO DEL NUOVO GRUPPO LO SI ESTRAPOLA DAL TEMPO+KM RACK.
    '    Dim id_gruppo_da_calcolare As String

    '    If id_gruppo_da_prenotazione_x_modifica_con_rack <> "" Then
    '        id_gruppo_da_calcolare = id_gruppo_da_prenotazione_x_modifica_con_rack
    '    Else
    '        id_gruppo_da_calcolare = id_gruppo
    '    End If

    '    Dim giorni_nolo As Integer = giorni_noleggio
    '    If giorni_prepagati > giorni_noleggio Then
    '        giorni_nolo = giorni_prepagati
    '    End If

    '    Dim sqlStr As String
    '    Dim Rs As Data.SqlClient.SqlDataReader


    '    sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " & _
    '             "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " & _
    '             "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND " & giorni_nolo - giorni_noleggio_extra_rack & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' " & _
    '             "AND NOT valore IS NULL AND valore<>0"


    '    Dbc.Close()
    '    Dbc.Open()
    '    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

    '    Rs = Cmd.ExecuteReader()

    '    Rs.Read()

    '    If Rs.HasRows() Then
    '        'IN QUESTO CASO HO TROVATO IL VALORE (IL VALORE VIENE CONSIDERATO TROVATO SOLO SE E' DIVERSO DA 0)
    '        Dim valore As Double = CDbl(Rs("valore"))
    '        Dim iva As String = Rs("iva")
    '        Dim codice_iva As String = Rs("codice_iva") & ""
    '        Dim iva_inclusa As String = Rs("iva_inclusa")
    '        Dim packed As String = Rs("pac")
    '        Dim qta As String = "1" 'LA QUANTITA' E' SEMPRE 1 TRANNE NEL CASO DI COSTO GIORNALIERO

    '        If valore <> 0 Then
    '            If Rs("gg_extra") Then
    '                'SE IL VALORE SI RIFERISCE AD UNA COLONNA GG EXTRA ALLORA IL VALORE DEVE ESSERE SOMMATO AL COSTO DEL GIORNO MASSIMO DELLA COLONNA PRECEDENTE.
    '                'AD ESEMPIO SE SIAMO NELLA COLONNA 8-999 ALLORA IL COSTO E' IL COSTO DI 7 GIORNI PIU' IL NUMERO DI GIORNI EXTRA. IN QUESTO CASO QUINDI PACKED PER LA COLONNA
    '                '8-999 SIGNIFICA RIFERITO AI GIORNI EXTRA E NON AI GIORNI DI NOLEGGIO
    '                Dim giorni_non_extra As Integer = Rs("da") - 1

    '                Rs.Close()
    '                Rs = Nothing
    '                Dbc.Close()
    '                Dbc.Open()

    '                sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " & _
    '                 "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " & _
    '                 "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND " & giorni_non_extra & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' " & _
    '                 "AND NOT valore IS NULL AND valore<>0"

    '                Dim valore_non_extra As Double = 0

    '                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '                Rs = Cmd.ExecuteReader()

    '                If Rs.Read() Then
    '                    'HO TROVATO IL COSTO DEI GIORNI NON EXTRA - EDIT: LA COLONNA GIORNI EXTRA DEVE ESSERE NON PACKED (METTO L'IF COMUNQUE PER EVITARE CONFUSIONE)
    '                    valore_non_extra = Rs("valore")
    '                    If Not Rs("pac") Then
    '                        valore_non_extra = valore_non_extra * giorni_non_extra
    '                    End If



    '                    'SE I GIORNI EXTRA SONO PACKED DEVO MOLTIPLICARE PER IL NUMERO DI GIORNI EXTRA E NON PER I GIORNI TOTALI DI NOLEGGIO
    '                    If packed = "False" Then
    '                        qta = giorni_nolo
    '                        valore = valore * ((giorni_nolo - giorni_noleggio_extra_rack) - giorni_non_extra)
    '                    End If

    '                    valore = valore + valore_non_extra
    '                Else
    '                    'NON SI TROVA IL COSTO DEI GIORNI NON EXTRA - TRATTO IL CASO COME SE FOSSE UNA COLONNA NON EXTRA (PROBABILE ERRORE DI INSERIMENTO)
    '                    If packed = "False" Then
    '                        qta = giorni_nolo
    '                        valore = valore * (giorni_nolo - giorni_noleggio_extra_rack)
    '                    End If
    '                End If
    '            Else
    '                'NON E' UN VALORE DI TIPO giorni extra
    '                If packed = "False" Then
    '                    qta = giorni_nolo
    '                    valore = valore * (giorni_nolo - giorni_noleggio_extra_rack)
    '                End If
    '            End If

    '            valore_trovato = True

    '            If (giorni_noleggio_extra_rack <> 0) And (id_gruppo_da_prenotazione_x_modifica_con_rack = "" Or id_gruppo = id_gruppo_da_prenotazione_x_modifica_con_rack) Then
    '                'NEL CASO IN CUI E' STATO PASSATO IL NUMERO DI GIORNI RACK E LA TARIFFA RACK (MODIFICA DI CONTRATTO CON PRENOTAZIONE) TROVO E AGGIUNGO
    '                'IL COSTO DEI GIORNI EXTRA - QUESTA OPERAZIONE E' EFFETTUARE SE NON VIENE CAMBIATO CONTESTUALMENTE IL GRUPPO
    '                '-IN QUESTO CASO NON MI INTERESSA CONSIDERARE SE IL COSTO SI RIFERISCE AD UNA COLONNA GIORNI EXTRA O MENO (CONSIDERANDO CHE UNA COLONNA GIORNI EXTRA NON PUO'
    '                'ESSERE PACKED); IN QUESTO CASO INFATTI NON CALCOLO IL COSTO DI X GIORNI DI NOLEGGIO BENSI' QUANTO COSTEREBBE AL GIORNO IL NOLEGGIO SE SI FOSSE UTILIZZATA 
    '                'LA TARIFFA RACK(MOLTIPLICANDO POI QUESTO VALORE PER IL NUMERO DI GIORN EXTRA) 

    '                Dim valore_rack As Double = 0

    '                Rs.Close()
    '                Rs = Nothing

    '                sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " & _
    '                "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " & _
    '                "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_nolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' AND NOT valore IS NULL AND valore<>0"

    '                Dbc.Close()
    '                Dbc.Open()
    '                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

    '                Rs = Cmd.ExecuteReader()
    '                Rs.Read()

    '                If Rs.HasRows() Then

    '                    'SE IL COSTO TROVATO E' AL GIORNO ALLORA MOLTIPLICO PER IL NUMERO DI GIORNI DI NOLEGGIO EXTRA, SE E' PACKED 
    '                    'ALLORA DIVIDO PER IL NUMERO DI GIORNI DI NOLEGGIO TOTALI (IN MODO DA AVERE IL COSTO GIORNALIERO) E POI 
    '                    'MOLTIPLICO PER IL NUMERO DI GIORNI EXTRA DESIDERATI
    '                    If Rs("pac") = "False" Then
    '                        valore_rack = CDbl(Rs("valore")) * giorni_noleggio_extra_rack
    '                    ElseIf Rs("pac") = "True" Then
    '                        valore_rack = (CDbl(Rs("valore")) / giorni_nolo) * giorni_noleggio_extra_rack
    '                    End If
    '                    'NEL CASO IN CUI VI SIA UNO SCONTO LO CALCOLO RIMUOVENDOLO DIRETTAMENTE DAL valore_rack TROVATO

    '                    valore_rack = valore_rack - (valore_rack * sconto_su_rack / 100)

    '                    'I DUE COSTI DEVONO ESSERE COERENTI: AGGIUNGO O RIMUOVO L'IVA AL VALORE RACK SE NECESSARIO
    '                    If iva_inclusa = "True" And Rs("iva_inclusa") = "False" Then
    '                        valore_rack = valore_rack + ((valore_rack * Rs("iva")) / 100)
    '                    ElseIf iva_inclusa = "False" And Rs("iva_inclusa") = "True" Then
    '                        valore_rack = valore_rack / (1 + (Rs("iva") / 100))
    '                    End If

    '                    valore = valore + valore_rack
    '                Else
    '                    'SE NON HO TROVATO IL COSTO PER I GIORNI EXTRA
    '                    valore_trovato = False
    '                End If
    '            ElseIf (giorni_noleggio_extra_rack = 0) And (id_gruppo_da_prenotazione_x_modifica_con_rack <> "" And id_gruppo <> id_gruppo_da_prenotazione_x_modifica_con_rack) Then
    '                'SECONDO CASO: NESSUNA ESTENSIONE DI GIORNI MA UPSELL DI GRUPPO - SELEZIONO IL COSTO DEL NUOVO GRUPPO USANDO LA
    '                'RACK E LO SOTTRAGGO AL COSTO DEL VECCHIO GRUPPO USANDO LA RACK - IN QUESTO CASO LO SCONTO NON VIENE CONSIDERATO
    '                'IN QUANTO NON VI E' UN'ESTENSIONE DI GIORNI
    '                '-IN QUESTO CASO DEVO TENER CONTO SE LA COLONNA E' DI TIPO GIORNI EXTRA
    '                Dim valore_rack_nuovo_gruppo As Double = 0
    '                Dim valore_rack_vecchio_gruppo As Double = 0
    '                Dim valore_rack As Double = 0
    '                Dim iva_rack As Double
    '                Dim iva_inclusa_Rack As Boolean

    '                Rs.Close()
    '                Rs = Nothing

    '                '1-Calcolo del costo col gruppo nuovo richiesto utilizzando la rack
    '                sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa,righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " & _
    '                "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " & _
    '                "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_nolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo & "' AND NOT valore IS NULL AND valore<>0"



    '                Dbc.Close()
    '                Dbc.Open()
    '                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

    '                Rs = Cmd.ExecuteReader()
    '                Rs.Read()

    '                If Rs.HasRows() Then
    '                    'SE IL COSTO TROVATO E' AL GIORNO ALLORA MOLTIPLICO PER IL NUMERO DI GIORNI DI NOLEGGIO EXTRA, SE E' PACKED 
    '                    'ALLORA DIVIDO PER IL NUMERO DI GIORNI DI NOLEGGIO TOTALI (IN MODO DA AVERE IL COSTO GIORNALIERO) E POI 
    '                    'MOLTIPLICO PER IL COSTO DI NOLEGGIO EXTRA
    '                    Dim val_trovato As Double = Rs("valore")
    '                    Dim packed_trovato As Boolean = Rs("pac")
    '                    iva_rack = Rs("iva")
    '                    iva_inclusa_Rack = Rs("iva_inclusa")

    '                    If Rs("gg_extra") Then
    '                        Dim giorni_non_extra As Integer = Rs("da") - 1

    '                        Rs.Close()
    '                        Rs = Nothing
    '                        Dbc.Close()
    '                        Dbc.Open()

    '                        sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " & _
    '                         "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " & _
    '                         "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_non_extra & " BETWEEN da AND a AND id_gruppo='" & id_gruppo & "' " & _
    '                         "AND NOT valore IS NULL AND valore<>0"

    '                        Dim valore_non_extra As Double = 0

    '                        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '                        Rs = Cmd.ExecuteReader()

    '                        If Rs.Read() Then
    '                            'HO TROVATO IL COSTO DEI GIORNI NON EXTRA 
    '                            valore_non_extra = Rs("valore")
    '                            If Not Rs("pac") Then
    '                                valore_non_extra = valore_non_extra * giorni_non_extra
    '                            End If

    '                            'SE I GIORNI EXTRA SONO PACKED DEVO MOLTIPLICARE PER IL NUMERO DI GIORNI EXTRA E NON PER I GIORNI TOTALI DI NOLEGGIO
    '                            '- EDIT: LA COLONNA GIORNI EXTRA DEVE ESSERE NON PACKED (METTO L'IF COMUNQUE PER EVITARE CONFUSIONE)
    '                            If Not packed_trovato Then
    '                                valore_rack_nuovo_gruppo = val_trovato * (giorni_nolo - giorni_non_extra)
    '                            Else
    '                                valore_rack_nuovo_gruppo = val_trovato
    '                            End If

    '                            valore_rack_nuovo_gruppo = valore_rack_nuovo_gruppo + valore_non_extra
    '                        Else
    '                            'NON SI TROVA IL COSTO DEI GIORNI NON EXTRA - TRATTO IL CASO COME SE FOSSE UNA COLONNA NON EXTRA (PROBABILE ERRORE DI INSERIMENTO)
    '                            If Not packed_trovato Then
    '                                valore_rack_nuovo_gruppo = val_trovato * giorni_nolo
    '                            ElseIf packed_trovato Then
    '                                valore_rack_nuovo_gruppo = val_trovato
    '                            End If
    '                        End If
    '                    Else
    '                        'SE NON E' UN COLONNA GIORNI EXTRA
    '                        If Not packed_trovato Then
    '                            valore_rack_nuovo_gruppo = val_trovato * giorni_nolo
    '                        ElseIf packed_trovato Then
    '                            valore_rack_nuovo_gruppo = val_trovato
    '                        End If
    '                    End If
    '                Else
    '                    valore_trovato = False
    '                End If

    '                If valore_trovato Then
    '                    'CONTINUO SOLAMENTE SE HO TROVATO UN VALORE - ALTRIMENTI C'E' GIA' QUALCOSA CHE NON VA NELLA TARIFFA
    '                    'CALCOLO IL COSTO UTLIZZANDO LA TARIFFA RACK PER IL GRUPPO SCELTO AL MOMENTO DELLA PRENOTAZIONE
    '                    Rs.Close()
    '                    Rs = Nothing

    '                    '1-Calcolo del costo col gruppo nuovo richiesto utilizzando la rack
    '                    sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa,righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " & _
    '                    "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " & _
    '                    "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_nolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_prenotazione_x_modifica_con_rack & "' AND NOT valore IS NULL AND valore<>0"

    '                    Dbc.Close()
    '                    Dbc.Open()
    '                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

    '                    Rs = Cmd.ExecuteReader()
    '                    Rs.Read()

    '                    If Rs.HasRows() Then
    '                        'SE IL COSTO TROVATO E' AL GIORNO ALLORA MOLTIPLICO PER IL NUMERO DI GIORNI DI NOLEGGIO EXTRA, SE E' PACKED 
    '                        'ALLORA DIVIDO PER IL NUMERO DI GIORNI DI NOLEGGIO TOTALI (IN MODO DA AVERE IL COSTO GIORNALIERO) E POI 
    '                        'MOLTIPLICO PER IL COSTO DI NOLEGGIO EXTRA
    '                        Dim val_trovato As Double = Rs("valore")
    '                        Dim packed_trovato As Boolean = Rs("pac")

    '                        If Rs("gg_extra") Then
    '                            Dim giorni_non_extra As Integer = Rs("da") - 1

    '                            Rs.Close()
    '                            Rs = Nothing
    '                            Dbc.Close()
    '                            Dbc.Open()

    '                            sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " & _
    '                             "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " & _
    '                             "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_non_extra & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_prenotazione_x_modifica_con_rack & "' " & _
    '                             "AND NOT valore IS NULL AND valore<>0"

    '                            Dim valore_non_extra As Double = 0

    '                            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '                            Rs = Cmd.ExecuteReader()

    '                            If Rs.Read() Then
    '                                'HO TROVATO IL COSTO DEI GIORNI NON EXTRA 
    '                                valore_non_extra = Rs("valore")
    '                                If Not Rs("pac") Then
    '                                    valore_non_extra = valore_non_extra * giorni_non_extra
    '                                End If

    '                                'SE I GIORNI EXTRA SONO PACKED DEVO MOLTIPLICARE PER IL NUMERO DI GIORNI EXTRA E NON PER I GIORNI TOTALI DI NOLEGGIO
    '                                '- EDIT: LA COLONNA GIORNI EXTRA DEVE ESSERE NON PACKED (METTO L'IF COMUNQUE PER EVITARE CONFUSIONE)
    '                                If Not packed_trovato Then
    '                                    valore_rack_vecchio_gruppo = val_trovato * (giorni_nolo - giorni_non_extra)
    '                                Else
    '                                    valore_rack_vecchio_gruppo = val_trovato
    '                                End If

    '                                valore_rack_vecchio_gruppo = valore_rack_vecchio_gruppo + valore_non_extra
    '                            Else
    '                                'NON SI TROVA IL COSTO DEI GIORNI NON EXTRA - TRATTO IL CASO COME SE FOSSE UNA COLONNA NON EXTRA (PROBABILE ERRORE DI INSERIMENTO)
    '                                If Not packed_trovato Then
    '                                    valore_rack_vecchio_gruppo = val_trovato * giorni_nolo
    '                                ElseIf packed_trovato Then
    '                                    valore_rack_vecchio_gruppo = val_trovato
    '                                End If
    '                            End If
    '                        Else
    '                            'SE NON E' UN COLONNA GIORNI EXTRA
    '                            If Not packed_trovato Then
    '                                valore_rack_vecchio_gruppo = val_trovato * giorni_nolo
    '                            ElseIf packed_trovato Then
    '                                valore_rack_vecchio_gruppo = val_trovato
    '                            End If
    '                        End If
    '                    Else
    '                        valore_trovato = False
    '                    End If

    '                    If valore_trovato Then
    '                        'A QUESTO PUNTO SOTTRAGGO I DUE VALORI E SOMMO LA DIFFERENZA AL VALORE-TARIFFA AL MOMENTO DELLA PRENOTAZIONE
    '                        'CHE INFATTI E' STATO CALCOLATO USANDO IL VECCHIO GRUPPO E LA TARIFFA USATA IN PRENOTAZIONE).
    '                        'NON ESSENDO PERMESSO IL DOWNSELL IL VALORE E' CERTAMENTE POSITIVO. - IN OGNI CASO SE IN QUALCHE CASO
    '                        'IL DOWNSELL DEVE ESSERE PERMESSO LA PROCEDURA FUNZIONA UGUALMENTE
    '                        valore_rack = valore_rack_nuovo_gruppo - valore_rack_vecchio_gruppo

    '                        'I DUE COSTI (RACK E TARIFFA AL MOMENTO DELLA PRENOTAZIONE) DEVONO ESSERE COERENTI: 
    '                        'AGGIUNGO O RIMUOVO L'IVA AL VALORE RACK SE NECESSARIO
    '                        If iva_inclusa = "True" And Not iva_inclusa_Rack Then
    '                            valore_rack = valore_rack + ((valore_rack * iva_rack) / 100)
    '                        ElseIf iva_inclusa = "False" And iva_inclusa_Rack Then
    '                            valore_rack = valore_rack / (1 + (iva_rack / 100))
    '                        End If

    '                        valore = valore + valore_rack
    '                    End If

    '                End If
    '            ElseIf (giorni_noleggio_extra_rack <> 0) And (id_gruppo_da_prenotazione_x_modifica_con_rack <> "" And id_gruppo <> id_gruppo_da_prenotazione_x_modifica_con_rack) Then
    '                'IN QUEST'ULTIMO CASO SI RICHIEDE LA VARIAZIONE SIA DEI GIORNI DI NOLEGGIO CHE DEL GRUPPO. IN QUESTO CASO SI SOTTRAE
    '                'IL COSTO DEL NUOVO GRUPPO E I NUOVI GIORNI DI NOLEGGIO USANDO LA RACK AL COSTO DEL GRUPPO DA PRENOTAZIONE COL 
    '                'NUMERO DI GIORNI DI PRENOTAZIONE SEMPRE USANDO LA RACK E SI SOMMA LA DIFFERENZA AL COSTO DEL VECCHIO GRUPPO
    '                'E VECCHI GIORNI DI PRENOTAZIONE USANDO LA TARIFFA AL MOMENTO DELLA PRENOTAZIONE (IL valore) GIA' CALCOLATO.
    '                Dim valore_rack_nuovo_gruppo As Double = 0
    '                Dim valore_rack_vecchio_gruppo As Double = 0
    '                Dim valore_rack As Double = 0
    '                Dim iva_rack As Double
    '                Dim iva_inclusa_Rack As Boolean

    '                Rs.Close()
    '                Rs = Nothing

    '                '1-Calcolo del costo col gruppo nuovo richiesto utilizzando la rack E I GIORNI DI NOLEGGIO TOTALI
    '                sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " & _
    '                "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " & _
    '                "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_nolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo & "' AND NOT valore IS NULL AND valore<>0"

    '                Dbc.Close()
    '                Dbc.Open()
    '                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

    '                Rs = Cmd.ExecuteReader()
    '                Rs.Read()

    '                If Rs.HasRows() Then
    '                    'SE IL COSTO TROVATO E' AL GIORNO ALLORA MOLTIPLICO PER IL NUMERO DI GIORNI DI NOLEGGIO EXTRA, SE E' PACKED 
    '                    'ALLORA DIVIDO PER IL NUMERO DI GIORNI DI NOLEGGIO TOTALI (IN MODO DA AVERE IL COSTO GIORNALIERO) E POI 
    '                    'MOLTIPLICO PER IL COSTO DI NOLEGGIO EXTRA
    '                    Dim val_trovato As Double = Rs("valore")
    '                    Dim packed_trovato As Boolean = Rs("pac")
    '                    iva_rack = Rs("iva")
    '                    iva_inclusa_Rack = Rs("iva_inclusa")

    '                    If Rs("gg_extra") Then
    '                        Dim giorni_non_extra As Integer = Rs("da") - 1

    '                        Rs.Close()
    '                        Rs = Nothing
    '                        Dbc.Close()
    '                        Dbc.Open()

    '                        sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " & _
    '                         "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " & _
    '                         "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_non_extra & " BETWEEN da AND a AND id_gruppo='" & id_gruppo & "' " & _
    '                         "AND NOT valore IS NULL AND valore<>0"

    '                        Dim valore_non_extra As Double = 0

    '                        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '                        Rs = Cmd.ExecuteReader()

    '                        If Rs.Read() Then
    '                            'HO TROVATO IL COSTO DEI GIORNI NON EXTRA 
    '                            valore_non_extra = Rs("valore")
    '                            If Not Rs("pac") Then
    '                                valore_non_extra = valore_non_extra * giorni_non_extra
    '                            End If

    '                            'SE I GIORNI EXTRA SONO PACKED DEVO MOLTIPLICARE PER IL NUMERO DI GIORNI EXTRA E NON PER I GIORNI TOTALI DI NOLEGGIO
    '                            '- EDIT: LA COLONNA GIORNI EXTRA DEVE ESSERE NON PACKED (METTO L'IF COMUNQUE PER EVITARE CONFUSIONE)
    '                            If Not packed_trovato Then
    '                                valore_rack_nuovo_gruppo = val_trovato * (giorni_nolo - giorni_non_extra)
    '                            Else
    '                                valore_rack_nuovo_gruppo = val_trovato
    '                            End If

    '                            valore_rack_nuovo_gruppo = valore_rack_nuovo_gruppo + valore_non_extra
    '                        Else
    '                            'NON SI TROVA IL COSTO DEI GIORNI NON EXTRA - TRATTO IL CASO COME SE FOSSE UNA COLONNA NON EXTRA (PROBABILE ERRORE DI INSERIMENTO)
    '                            If Not packed_trovato Then
    '                                valore_rack_nuovo_gruppo = val_trovato * giorni_nolo
    '                            ElseIf packed_trovato Then
    '                                valore_rack_nuovo_gruppo = val_trovato
    '                            End If
    '                        End If
    '                    Else
    '                        'SE NON E' UN COLONNA GIORNI EXTRA
    '                        If Not packed_trovato Then
    '                            valore_rack_nuovo_gruppo = val_trovato * giorni_nolo
    '                        ElseIf packed_trovato Then
    '                            valore_rack_nuovo_gruppo = val_trovato
    '                        End If
    '                    End If
    '                Else
    '                    valore_trovato = False
    '                End If

    '                If valore_trovato Then
    '                    'CONTINUO SOLAMENTE SE HO TROVATO UN VALORE - ALTRIMENTI C'E' GIA' QUALCOSA CHE NON VA NELLA TARIFFA
    '                    'CALCOLO IL COSTO UTLIZZANDO LA TARIFFA RACK PER IL GRUPPO SCELTO AL MOMENTO DELLA PRENOTAZIONE
    '                    Rs.Close()
    '                    Rs = Nothing

    '                    '1-Calcolo del costo col gruppo richiesto al momento della prenotazione utilizzando la rack
    '                    sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " & _
    '                    "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " & _
    '                    "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_nolo - giorni_noleggio_extra_rack & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_prenotazione_x_modifica_con_rack & "' AND NOT valore IS NULL AND valore<>0"
    '                    Dbc.Close()
    '                    Dbc.Open()
    '                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

    '                    Rs = Cmd.ExecuteReader()
    '                    Rs.Read()

    '                    If Rs.HasRows() Then
    '                        'SE IL COSTO TROVATO E' AL GIORNO ALLORA MOLTIPLICO PER IL NUMERO DI GIORNI DI NOLEGGIO EXTRA, SE E' PACKED 
    '                        'ALLORA DIVIDO PER IL NUMERO DI GIORNI DI NOLEGGIO TOTALI (IN MODO DA AVERE IL COSTO GIORNALIERO) E POI 
    '                        'MOLTIPLICO PER IL COSTO DI NOLEGGIO EXTRA
    '                        Dim val_trovato As Double = Rs("valore")
    '                        Dim packed_trovato As Boolean = Rs("pac")

    '                        If Rs("gg_extra") Then
    '                            Dim giorni_non_extra As Integer = Rs("da") - 1

    '                            Rs.Close()
    '                            Rs = Nothing
    '                            Dbc.Close()
    '                            Dbc.Open()

    '                            sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " & _
    '                             "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " & _
    '                             "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_non_extra & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_prenotazione_x_modifica_con_rack & "' " & _
    '                             "AND NOT valore IS NULL AND valore<>0"

    '                            Dim valore_non_extra As Double = 0

    '                            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '                            Rs = Cmd.ExecuteReader()

    '                            If Rs.Read() Then
    '                                'HO TROVATO IL COSTO DEI GIORNI NON EXTRA 
    '                                valore_non_extra = Rs("valore")
    '                                If Not Rs("pac") Then
    '                                    valore_non_extra = valore_non_extra * giorni_non_extra
    '                                End If

    '                                'SE I GIORNI EXTRA SONO PACKED DEVO MOLTIPLICARE PER IL NUMERO DI GIORNI EXTRA E NON PER I GIORNI TOTALI DI NOLEGGIO
    '                                '- EDIT: LA COLONNA GIORNI EXTRA DEVE ESSERE NON PACKED (METTO L'IF COMUNQUE PER EVITARE CONFUSIONE)
    '                                If Not packed_trovato Then
    '                                    valore_rack_vecchio_gruppo = val_trovato * (giorni_nolo - giorni_noleggio_extra_rack - giorni_non_extra)
    '                                Else
    '                                    valore_rack_vecchio_gruppo = val_trovato
    '                                End If

    '                                valore_rack_vecchio_gruppo = valore_rack_vecchio_gruppo + valore_non_extra
    '                            Else
    '                                'NON SI TROVA IL COSTO DEI GIORNI NON EXTRA - TRATTO IL CASO COME SE FOSSE UNA COLONNA NON EXTRA (PROBABILE ERRORE DI INSERIMENTO)
    '                                If Not packed_trovato Then
    '                                    valore_rack_vecchio_gruppo = val_trovato * (giorni_nolo - giorni_noleggio_extra_rack)
    '                                ElseIf packed_trovato Then
    '                                    valore_rack_vecchio_gruppo = val_trovato
    '                                End If
    '                            End If
    '                        Else
    '                            'SE NON E' UN COLONNA GIORNI EXTRA
    '                            If Not packed_trovato Then
    '                                valore_rack_vecchio_gruppo = val_trovato * (giorni_nolo - giorni_noleggio_extra_rack)
    '                            ElseIf packed_trovato Then
    '                                valore_rack_vecchio_gruppo = val_trovato
    '                            End If
    '                        End If
    '                    Else
    '                        valore_trovato = False
    '                    End If

    '                    If valore_trovato Then
    '                        'A QUESTO PUNTO SOTTRAGGO I DUE VALORI E SOMMO LA DIFFERENZA AL VALORE-TARIFFA AL MOMENTO DELLA PRENOTAZIONE
    '                        'CHE INFATTI E' STATO CALCOLATO USANDO IL VECCHIO GRUPPO E LA TARIFFA USATA IN PRENOTAZIONE).
    '                        'NON ESSENDO PERMESSO IL DOWNSELL IL VALORE E' CERTAMENTE POSITIVO. - IN OGNI CASO SE IN QUALCHE CASO
    '                        'IL DOWNSELL DEVE ESSERE PERMESSO LA PROCEDURA FUNZIONA UGUALMENTE
    '                        valore_rack = valore_rack_nuovo_gruppo - valore_rack_vecchio_gruppo

    '                        valore_rack = valore_rack - (valore_rack * sconto_su_rack / 100)

    '                        'I DUE COSTI (RACK E TARIFFA AL MOMENTO DELLA PRENOTAZIONE) DEVONO ESSERE COERENTI: 
    '                        'AGGIUNGO O RIMUOVO L'IVA AL VALORE RACK SE NECESSARIO
    '                        If iva_inclusa = "True" And Not iva_inclusa_Rack Then
    '                            valore_rack = valore_rack + ((valore_rack * iva_rack) / 100)
    '                        ElseIf iva_inclusa = "False" And iva_inclusa_Rack Then
    '                            valore_rack = valore_rack / (1 + (iva_rack / 100))
    '                        End If

    '                        valore = valore + valore_rack
    '                    End If

    '                End If
    '            End If

    '            If valore_trovato Then
    '                salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Costanti.ID_tempo_km, "", "Valore Tariffa" & "", valore, "NULL", iva, codice_iva, iva_inclusa, "True", "False", "False", Costanti.id_accessorio_incluso, "2", "True", "1", 1, "NULL", Costanti.id_unita_misura_giorni, "", packed, qta, "", False, prepagata)
    '            End If

    '        End If
    '    End If

    '    Rs.Close()
    '    Rs = Nothing


    '    If Not valore_trovato Then
    '        'ALLA FINE DI TUTTO SE NON SONO RIUSCITO A TROVARE NULLA NE DALLA MADRE NE DALLA FIGLIA SALVO UNA RIGA DI ERRORE -
    '        'SALVO L'ERRORE ANCHE SE C'E' QUALCOSA CHE NON VA (OVVERO QUALCHE VALORE NON DEFINITO) NEL CALCOLO DELLE ESTENSIONI CON 
    '        'RACK
    '        salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Costanti.ID_tempo_km, "", "ERRORE - VALORE TARIFFA NON TROVATO", "0", "NULL", "0", "NULL", "False", "True", "False", "False", "NULL", "1", "", "0", 1, "NULL", "NULL", "", "NULL", "0", "", False, prepagata)
    '    End If

    '    Cmd.Dispose()
    '    Cmd = Nothing
    '    Dbc.Close()
    '    Dbc.Dispose()
    '    Dbc = Nothing
    'End Sub

    Protected Sub calcola_tempo_km(ByVal giorni_noleggio As Integer, ByVal giorni_prepagati As Integer, ByVal prepagata As Boolean,
                                   ByVal giorni_noleggio_extra_rack As Integer, ByVal sconto_su_rack As String, ByVal stazione_pick_up As String,
                                   ByVal stazione_drop_off As String, ByVal id_tempo_km_figlia As String, ByVal id_tempo_km_rack As String,
                                   ByVal id_gruppo As String, ByVal id_gruppo_da_prenotazione_x_modifica_con_rack As String, ByVal id_preventivo As String,
                                   ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String,
                                   ByVal broker_a_carico_di As String,
                                   Optional dataPickUp As String = "0", Optional dataDropOff As String = "0", Optional tipoCli As String = "0",
                                   Optional TipoTariffa As String = "0", Optional descTariffa As String = "0", Optional idTariffa As String = "0",
                                   Optional data_creazione As String = "", Optional max_sconto_new As String = "0",
                                   Optional DaARES As Boolean = True, Optional ggExtra As String = "0", Optional ValoreTariffaOri As String = "0", Optional SetTariffaOri As Boolean = False)
        'Optional DaARES=inserito per eventuale chiamata da WebService che dovrà essere False - salvo 04.02.2023
        'Optional ggExtra=inserito per calcolo gg extra in ricalcolo  - salvo 23.02.2023


        'inseriti parametri optional ultime due righe - salvo 06.12.2022 - 04.01.2023

        'BROKER_A_CARICO_DI: STRINGA VUOTA SE NON SI TRATTA DI MODIFICA A NOLO IN CORSO PER CONTRATTO BROKER, ALTRIMENTI E' 1 SE A CARICO DEL BROKER O 2 A CARICO DEL CLIENTE


        Dim sqlStr As String

        '#aggiunto salvo 03.11.2022
        Dim gg As Integer = 0
        Dim new_Valore As Double = 0
        '@ end salvo

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

            Dim valore_trovato As Boolean = False
            'SE VIENE PASSATO IL PARAMENTRO id_gruppo_da_prenotazione_x_modifica_con_rack DEVO CALCOLARE, CON LA TARIFFA INIZIALE, IL COSTO DEL TEMPO
            'KM CON L'ID_GRUPPO_DA_PRENOTAZIONE E NON COL NUOVO GRUPPO: IL COSTO DEL NUOVO GRUPPO LO SI ESTRAPOLA DAL TEMPO+KM RACK.
            Dim id_gruppo_da_calcolare As String

            If id_gruppo_da_prenotazione_x_modifica_con_rack <> "" Then
                id_gruppo_da_calcolare = id_gruppo_da_prenotazione_x_modifica_con_rack
            Else
                id_gruppo_da_calcolare = id_gruppo
            End If

            Dim giorni_nolo As Integer = giorni_noleggio

            If giorni_prepagati > giorni_noleggio Then
                giorni_nolo = giorni_prepagati
            End If

            Dim Rs As Data.SqlClient.SqlDataReader

            Dim giorni_originale_contratto As String = ""

            If Not prepagata Then
                If id_contratto <> "" And num_calcolo > 1 And giorni_noleggio_extra_rack = 0 And broker_a_carico_di <> "1" And (id_gruppo_da_prenotazione_x_modifica_con_rack = "" Or id_gruppo = id_gruppo_da_prenotazione_x_modifica_con_rack) Then
                    'CASO ESTENIONE A NOLO IN CORSO PRENOTAZIONE NORMALE E NON PREPAGATA - NO RACK - NO BROKER CON ESTENSIONE A CARICO DEL BROKER
                    'A NOLO IN CORSO NON PUO' CAMBIARE IL GRUPPO.
                    'UTILIZZO PER L'ESTENSIONE LA STESSA COLONNA DEI DATI ORIGINALI DI NOLEGGIO PER IL CALCOLO DEI COSTO EXTRA SE I GIORNI SONO MAGGIORI

                    'PER PRIMA COSA PRELEVO I GIORNI ORIGINALI DI NOLEGGIO. INFATTI UTILIZZO IL CALCOLO CON LA COLONNA REALE SE I GIORNI DI NOLEGGIO VENGONO DIMINUITI
                    sqlStr = "Select num_contratto FROM contratti With(NOLOCK) WHERE id=" & id_contratto & " And (status=2 Or status=4 Or status=8)"
                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dim num_contratto As String = Cmd.ExecuteScalar & ""

                    If num_contratto <> "" Then
                        'IN QUESTO CASO SIAMO NEL CASO DI NOLO IN CORSO
                        'NEL CASO DI TOUR OPERATOR A CARICO DEL CLIENTE SELZIONO IL NUMERO DI GIORNI DELL'ULTIMA MODIFICA A CARICO DEL BROKER (GIORNI A CARICO DEL TOUR OPERATOR =
                        'GIORNI DI NOLEGGIO); NEL CASO IN CUI NON SI TROVA COSI' IL NUMERO DI GIORNI VENGONO CONSIEDERATO QUELLI DI USCITA

                        If broker_a_carico_di = "0" Then
                            sqlStr = "Select TOP 1 giorni FROM contratti With(NOLOCK) WHERE num_contratto='" & num_contratto & "' AND giorni_to=giorni ORDER BY id DESC"
                            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                            giorni_originale_contratto = Cmd.ExecuteScalar & ""

                            If giorni_originale_contratto = "" Then
                                sqlStr = "SELECT giorni FROM contratti WITH(NOLOCK) WHERE num_contratto='" & num_contratto & "' AND num_calcolo=1"
                                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                                giorni_originale_contratto = Cmd.ExecuteScalar
                            End If
                        Else
                            sqlStr = "SELECT giorni FROM contratti WITH(NOLOCK) WHERE num_contratto='" & num_contratto & "' AND num_calcolo=1"
                            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                            giorni_originale_contratto = Cmd.ExecuteScalar
                        End If



                        If giorni_noleggio > CInt(giorni_originale_contratto) Then
                            'IN QUESTO CASO SELEZIONO COME VALORE DA UTILIZZARE PER IL CALCOLO I GIORNI ORIGINALI A MENO CHE LA COLONNA REALE NON SIA GG_EXTRA
                            sqlStr = "SELECT ISNULL(righe_tempo_km.gg_extra,0) FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                             "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                             "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND " & giorni_nolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' " &
                             "AND NOT valore IS NULL AND valore<>0"

                            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                            Dim is_gg_extra As Boolean = Cmd.ExecuteScalar

                            If Not is_gg_extra Then
                                sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                                         "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                                         "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND " & giorni_originale_contratto & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' " &
                                         "AND NOT valore IS NULL AND valore<>0"
                            Else
                                sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                                         "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                                         "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND " & giorni_nolo - giorni_noleggio_extra_rack & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' " &
                                         "AND NOT valore IS NULL AND valore<>0"
                            End If
                        Else
                            sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                             "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                             "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND " & giorni_nolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' " &
                             "AND NOT valore IS NULL AND valore<>0"
                        End If
                    Else
                        'SIAMO QUI SE NON E' UNA MODIFICA A NOLO IN CORSO O POST NOLO (non dovrebbe succedere mai)
                        sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                             "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                             "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND " & giorni_nolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' " &
                             "AND NOT valore IS NULL AND valore<>0"
                    End If
                ElseIf id_contratto <> "" And num_calcolo > 1 And giorni_noleggio_extra_rack <> 0 And broker_a_carico_di <> "1" And (id_gruppo_da_prenotazione_x_modifica_con_rack = "" Or id_gruppo = id_gruppo_da_prenotazione_x_modifica_con_rack) Then

                    'CASO ESTENIONE A NOLO IN CORSO PRENOTAZIONE NORMALE NO PREPAGATA IN PRESENZA DI  RACK DA UTILIZZARE - NO BROKER CON ESTENSIONE A CARICO DEL BROKER
                    'A NOLO IN CORSO NON PUO' CAMBIARE IL GRUPPO.
                    'UTILIZZO PER L'ESTENSIONE LA STESSA COLONNA DEI DATI ORIGINALI DI NOLEGGIO PER IL CALCOLO DEI COSTO EXTRA SE I GIORNI SONO MAGGIORI

                    'ANCHE IN QUESTO CASO TROVO I GIORNI INIZIALI PER IL CALCOLO CHE VERRANNO PERO' USATI PER L'ESTENSIONE

                    sqlStr = "SELECT num_contratto FROM contratti WITH(NOLOCK) WHERE id=" & id_contratto & " AND (status=2 OR status=4 Or status=8)"
                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dim num_contratto As String = Cmd.ExecuteScalar & ""

                    If num_contratto <> "" Then
                        'IN QUESTO CASO SIAMO NEL CASO DI NOLO IN CORSO
                        'NEL CASO DI TOUR OPERATOR A CARICO DEL CLIENTE SELZIONO IL NUMERO DI GIORNI DELL'ULTIMA MODIFICA A CARICO DEL BROKER (GIORNI A CARICO DEL TOUR OPERATOR =
                        'GIORNI DI NOLEGGIO); NEL CASO IN CUI NON SI TROVA COSI' IL NUMERO DI GIORNI VENGONO CONSIEDERATO QUELLI DI USCITA

                        If broker_a_carico_di = "0" Then
                            sqlStr = "SELECT TOP 1 giorni FROM contratti WITH(NOLOCK) WHERE num_contratto='" & num_contratto & "' AND giorni_to=giorni ORDER BY id DESC"
                            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                            giorni_originale_contratto = Cmd.ExecuteScalar & ""

                            If giorni_originale_contratto = "" Then
                                sqlStr = "SELECT giorni FROM contratti WITH(NOLOCK) WHERE num_contratto='" & num_contratto & "' AND num_calcolo=1"
                                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                                giorni_originale_contratto = Cmd.ExecuteScalar
                            End If
                        Else
                            sqlStr = "SELECT giorni FROM contratti WITH(NOLOCK) WHERE num_contratto='" & num_contratto & "' AND num_calcolo=1"
                            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                            giorni_originale_contratto = Cmd.ExecuteScalar
                        End If


                        If giorni_noleggio > CInt(giorni_originale_contratto) Then

                        Else
                            giorni_originale_contratto = ""
                        End If

                    End If

                    sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                         "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                         "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND " & giorni_nolo - giorni_noleggio_extra_rack & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' " &
                         "AND NOT valore IS NULL AND valore<>0"

                Else
                    sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                         "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                         "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND " & giorni_nolo - giorni_noleggio_extra_rack & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' " &
                         "AND NOT valore IS NULL AND valore<>0"
                End If
            Else
                'NEL CASO DI PRENOTAZIONE PREPAGATA SI DEVE ESEGUIRE IL CALCOLO VENDENDO AL CLIENTE SEMPRE LA STESSA COLONNA DEI GIORNI PREPAGATI
                If giorni_noleggio_extra_rack = 0 And (id_gruppo_da_prenotazione_x_modifica_con_rack = "" Or id_gruppo = id_gruppo_da_prenotazione_x_modifica_con_rack) Then
                    'CASO NO RACK

                    Dim giorni_calcolo As String
                    'PER IL PRIMO CALCOLO GIORNI PREPAGATI VALE 0
                    If giorni_prepagati = 0 Then
                        giorni_calcolo = giorni_nolo
                    Else
                        giorni_calcolo = giorni_prepagati
                    End If

                    'SELEZIONO COME VALORE DA UTILIZZARE PER IL CALCOLO I GIORNI ORIGINALI A MENO CHE LA COLONNA REALE NON SIA GG_EXTRA
                    sqlStr = "SELECT ISNULL(righe_tempo_km.gg_extra,0) FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                     "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                     "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND " & giorni_nolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' " &
                     "AND NOT valore IS NULL AND valore<>0"

                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dim is_gg_extra As Boolean = Cmd.ExecuteScalar

                    If Not is_gg_extra Then
                        sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                                         "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                                         "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND " & giorni_calcolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' " &
                                         "AND NOT valore IS NULL AND valore<>0"
                    Else
                        sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                                         "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                                         "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND " & giorni_nolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' " &
                                         "AND NOT valore IS NULL AND valore<>0"
                    End If



                ElseIf giorni_noleggio_extra_rack <> 0 And broker_a_carico_di <> "1" And (id_gruppo_da_prenotazione_x_modifica_con_rack = "" Or id_gruppo = id_gruppo_da_prenotazione_x_modifica_con_rack) Then
                    'CASO RACK
                    giorni_originale_contratto = giorni_prepagati

                    sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                         "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                         "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND " & giorni_nolo - giorni_noleggio_extra_rack & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' " &
                         "AND NOT valore IS NULL AND valore<>0"
                Else
                    sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                         "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                         "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND " & giorni_nolo - giorni_noleggio_extra_rack & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' " &
                         "AND NOT valore IS NULL AND valore<>0"
                End If
            End If


            Dbc.Close()
            Dbc.Open()
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            Rs = Cmd.ExecuteReader()

            Rs.Read()

            'Dim idTariffa As String = id_tempo_km_figlia 'valore passato

            '# START INIZIO CALCOLO CON VERIFICA Salvo 10.01.2023
            'Verifica se deve effettuare il calcolo con il nuovo o il vecchio metodo
            'in funzione della data di creazione di Prenotazione o Contratto che viene passato come parametro
            Dim DataInizioNuovoCalcolo As String = ConfigurationManager.AppSettings("datanuovocalcolotariffa")
            Dim FlagOldCalcolo As Boolean = False
            'se data_creazione vuoto ??
            If data_creazione <> "" Then
                data_creazione = funzioni_comuni.getDataDb_con_orario(data_creazione)
            Else
                'da verificare la condizione
            End If

            'se la data di creazione è inferiore alla data di inizio nuovo calcolo 
            'attiva il vecchio calcolo
            If CDate(data_creazione) < CDate(DataInizioNuovoCalcolo) Then
                FlagOldCalcolo = True 'Attiva il vecchio calcolo
            End If

            'FlagOldCalcolo = True 'Attiva il vecchio calcolo  x TEST
            '@ end verifica 


            If Rs.HasRows() Then
                'IN QUESTO CASO HO TROVATO IL VALORE (IL VALORE VIENE CONSIDERATO TROVATO SOLO SE E' DIVERSO DA 0)
                Dim valore As Double = CDbl(Rs("valore"))
                Dim iva As String = Rs("iva")
                Dim codice_iva As String = Rs("codice_iva") & ""
                Dim iva_inclusa As String = Rs("iva_inclusa")
                Dim packed As String = Rs("pac")
                Dim qta As String = "1" 'LA QUANTITA' E' SEMPRE 1 TRANNE NEL CASO DI COSTO GIORNALIERO

                If valore <> 0 Then

                    If Rs("gg_extra") Then
                        'SE IL VALORE SI RIFERISCE AD UNA COLONNA GG EXTRA ALLORA IL VALORE DEVE ESSERE SOMMATO AL COSTO DEL GIORNO MASSIMO DELLA COLONNA PRECEDENTE.
                        'AD ESEMPIO SE SIAMO NELLA COLONNA 8-999 ALLORA IL COSTO E' IL COSTO DI 7 GIORNI PIU' IL NUMERO DI GIORNI EXTRA. IN QUESTO CASO QUINDI PACKED PER LA COLONNA
                        '8-999 SIGNIFICA RIFERITO AI GIORNI EXTRA E NON AI GIORNI DI NOLEGGIO
                        Dim giorni_non_extra As Integer = Rs("da") - 1

                        Rs.Close()
                        Rs = Nothing
                        Dbc.Close()
                        Dbc.Open()

                        sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                         "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                         "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND " & giorni_non_extra & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' " &
                         "AND NOT valore IS NULL AND valore<>0"

                        Dim valore_non_extra As Double = 0

                        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                        Rs = Cmd.ExecuteReader()

                        If Rs.Read() Then
                            'HO TROVATO IL COSTO DEI GIORNI NON EXTRA - EDIT: LA COLONNA GIORNI EXTRA DEVE ESSERE NON PACKED (METTO L'IF COMUNQUE PER EVITARE CONFUSIONE)
                            valore_non_extra = Rs("valore")
                            If Not Rs("pac") Then

                                gg = giorni_non_extra

                                'nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                If FlagOldCalcolo = False Then
                                    'new calcolo
                                    valore_non_extra = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                    tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                Else
                                    valore_non_extra = valore_non_extra * giorni_non_extra '(vecchio calcolo salvo 03.11.2022)
                                End If


                            End If



                            'SE I GIORNI EXTRA SONO PACKED DEVO MOLTIPLICARE PER IL NUMERO DI GIORNI EXTRA E NON PER I GIORNI TOTALI DI NOLEGGIO
                            If packed = "False" Then
                                qta = giorni_nolo

                                '# nuovo calcolo tempoKm 03.11.2022 Salvo
                                gg = ((giorni_nolo - giorni_noleggio_extra_rack) - giorni_non_extra)

                                'nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                If FlagOldCalcolo = False Then
                                    'new calcolo
                                    valore = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                            tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                Else
                                    valore = valore * ((giorni_nolo - giorni_noleggio_extra_rack) - giorni_non_extra) 'vecchio calcolo salvo 03.11.2022 
                                End If

                            End If

                            valore = valore + valore_non_extra


                        Else
                            'NON SI TROVA IL COSTO DEI GIORNI NON EXTRA - TRATTO IL CASO COME SE FOSSE UNA COLONNA NON EXTRA (PROBABILE ERRORE DI INSERIMENTO)
                            If packed = "False" Then
                                qta = giorni_nolo

                                '# nuovo calcolo tempoKm 03.11.2022 Salvo
                                gg = (giorni_nolo - giorni_noleggio_extra_rack)

                                'nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                If FlagOldCalcolo = False Then
                                    'new calcolo
                                    valore = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                           tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                Else
                                    valore = valore * (giorni_nolo - giorni_noleggio_extra_rack) 'vecchio calcolo salvo 03.11.2022 
                                End If

                            End If
                        End If
                    Else
                        'NON E' UN VALORE DI TIPO giorni extra
                        If packed = "False" Then
                            qta = giorni_nolo
                            gg = giorni_nolo - giorni_noleggio_extra_rack

                            '# Nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                            If FlagOldCalcolo = False Then

                                '#Salvo 15.05.2023 se tariffa originale passa il valore originale 
                                'senza ricalcolare
                                If SetTariffaOri = True And CInt(ggExtra) = 0 Then  'se tariffa Originale e ggExtra=0 nessuna variazione Tariffa TempoKM
                                    valore = CDbl(ValoreTariffaOri)   'tariffa originale
                                Else
                                    'nuovo calcolo senza periodi 06.12.2022 /aggiunto ggExtra 23.02.2023
                                    valore = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                                                       tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio,
                                                                                                                       idTariffa, False, "", "", max_sconto_new,, ggExtra, ValoreTariffaOri, SetTariffaOri)
                                End If

                            Else
                                valore = valore * (giorni_nolo - giorni_noleggio_extra_rack)  'vecchio calcolo 
                            End If
                            '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023

                        End If
                    End If

                    valore_trovato = True

                    If (giorni_noleggio_extra_rack <> 0) And (id_gruppo_da_prenotazione_x_modifica_con_rack = "" Or id_gruppo = id_gruppo_da_prenotazione_x_modifica_con_rack) Then

                        'NEL CASO IN CUI E' STATO PASSATO IL NUMERO DI GIORNI RACK E LA TARIFFA RACK (MODIFICA DI CONTRATTO CON PRENOTAZIONE) TROVO E AGGIUNGO
                        'IL COSTO DEI GIORNI EXTRA - QUESTA OPERAZIONE E' EFFETTUARE SE NON VIENE CAMBIATO CONTESTUALMENTE IL GRUPPO
                        '-IN QUESTO CASO NON MI INTERESSA CONSIDERARE SE IL COSTO SI RIFERISCE AD UNA COLONNA GIORNI EXTRA O MENO (CONSIDERANDO CHE UNA COLONNA GIORNI EXTRA NON PUO'
                        'ESSERE PACKED); IN QUESTO CASO INFATTI NON CALCOLO IL COSTO DI X GIORNI DI NOLEGGIO BENSI' QUANTO COSTEREBBE AL GIORNO IL NOLEGGIO SE SI FOSSE UTILIZZATA 
                        'LA TARIFFA RACK(MOLTIPLICANDO POI QUESTO VALORE PER IL NUMERO DI GIORN EXTRA) 

                        Dim valore_rack As Double = 0

                        Rs.Close()
                        Rs = Nothing


                        If giorni_originale_contratto <> "" Then
                            'IN QUESTO CASO DEVO CALCOLARE IL VALORE UTILIZZANDO LA STESSA COLONNA CORRISPONDENTI AI GIORNI DI USCITA SULLA RACK
                            sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                            "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                            "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_originale_contratto & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' AND NOT valore IS NULL AND valore<>0"
                        Else
                            sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                            "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                            "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_nolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' AND NOT valore IS NULL AND valore<>0"
                        End If


                        Dbc.Close()
                        Dbc.Open()
                        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                        Rs = Cmd.ExecuteReader()
                        Rs.Read()

                        If Rs.HasRows() Then

                            'SE IL COSTO TROVATO E' AL GIORNO ALLORA MOLTIPLICO PER IL NUMERO DI GIORNI DI NOLEGGIO EXTRA, SE E' PACKED 
                            'ALLORA DIVIDO PER IL NUMERO DI GIORNI DI NOLEGGIO TOTALI (IN MODO DA AVERE IL COSTO GIORNALIERO) E POI 
                            'MOLTIPLICO PER IL NUMERO DI GIORNI EXTRA DESIDERATI
                            If Rs("pac") = "False" Then
                                gg = giorni_noleggio_extra_rack

                                '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                If FlagOldCalcolo = False Then
                                    valore_rack = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                     tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                Else
                                    valore_rack = CDbl(Rs("valore")) * giorni_noleggio_extra_rack 'vecchio calcolo salvo 03.11.2022
                                End If
                                '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023


                            ElseIf Rs("pac") = "True" Then
                                gg = giorni_noleggio_extra_rack

                                '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                If FlagOldCalcolo = False Then
                                    valore_rack = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                     tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                Else
                                    valore_rack = (CDbl(Rs("valore")) / giorni_nolo) * giorni_noleggio_extra_rack  'vecchio calcolo salvo 03.11.2022
                                End If
                                '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023

                            End If
                            'NEL CASO IN CUI VI SIA UNO SCONTO LO CALCOLO RIMUOVENDOLO DIRETTAMENTE DAL valore_rack TROVATO

                            valore_rack = valore_rack - (valore_rack * sconto_su_rack / 100)

                            'I DUE COSTI DEVONO ESSERE COERENTI: AGGIUNGO O RIMUOVO L'IVA AL VALORE RACK SE NECESSARIO
                            If iva_inclusa = "True" And Rs("iva_inclusa") = "False" Then
                                valore_rack = valore_rack + ((valore_rack * Rs("iva")) / 100)
                            ElseIf iva_inclusa = "False" And Rs("iva_inclusa") = "True" Then
                                valore_rack = valore_rack / (1 + (Rs("iva") / 100))
                            End If

                            valore = valore + valore_rack


                        Else
                            'SE NON HO TROVATO IL COSTO PER I GIORNI EXTRA
                            valore_trovato = False
                        End If

                    ElseIf (giorni_noleggio_extra_rack = 0) And (id_gruppo_da_prenotazione_x_modifica_con_rack <> "" And id_gruppo <> id_gruppo_da_prenotazione_x_modifica_con_rack) Then


                        'SECONDO CASO: NESSUNA ESTENSIONE DI GIORNI MA UPSELL DI GRUPPO - SELEZIONO IL COSTO DEL NUOVO GRUPPO USANDO LA
                        'RACK E LO SOTTRAGGO AL COSTO DEL VECCHIO GRUPPO USANDO LA RACK - IN QUESTO CASO LO SCONTO NON VIENE CONSIDERATO
                        'IN QUANTO NON VI E' UN'ESTENSIONE DI GIORNI
                        '-IN QUESTO CASO DEVO TENER CONTO SE LA COLONNA E' DI TIPO GIORNI EXTRA
                        Dim valore_rack_nuovo_gruppo As Double = 0
                        Dim valore_rack_vecchio_gruppo As Double = 0
                        Dim valore_rack As Double = 0
                        Dim iva_rack As Double
                        Dim iva_inclusa_Rack As Boolean

                        Rs.Close()
                        Rs = Nothing

                        '1-Calcolo del costo col gruppo nuovo richiesto utilizzando la rack
                        sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa,righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                        "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                        "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_nolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo & "' AND NOT valore IS NULL AND valore<>0"



                        Dbc.Close()
                        Dbc.Open()
                        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                        Rs = Cmd.ExecuteReader()
                        Rs.Read()

                        If Rs.HasRows() Then

                            'SE IL COSTO TROVATO E' AL GIORNO ALLORA MOLTIPLICO PER IL NUMERO DI GIORNI DI NOLEGGIO EXTRA, SE E' PACKED 
                            'ALLORA DIVIDO PER IL NUMERO DI GIORNI DI NOLEGGIO TOTALI (IN MODO DA AVERE IL COSTO GIORNALIERO) E POI 
                            'MOLTIPLICO PER IL COSTO DI NOLEGGIO EXTRA
                            Dim val_trovato As Double = Rs("valore")
                            Dim packed_trovato As Boolean = Rs("pac")
                            iva_rack = Rs("iva")
                            iva_inclusa_Rack = Rs("iva_inclusa")

                            If Rs("gg_extra") Then
                                Dim giorni_non_extra As Integer = Rs("da") - 1

                                Rs.Close()
                                Rs = Nothing
                                Dbc.Close()
                                Dbc.Open()

                                sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                                 "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                                 "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_non_extra & " BETWEEN da AND a AND id_gruppo='" & id_gruppo & "' " &
                                 "AND NOT valore IS NULL AND valore<>0"

                                Dim valore_non_extra As Double = 0

                                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                                Rs = Cmd.ExecuteReader()

                                If Rs.Read() Then
                                    'HO TROVATO IL COSTO DEI GIORNI NON EXTRA 
                                    valore_non_extra = Rs("valore")
                                    If Not Rs("pac") Then
                                        gg = giorni_non_extra

                                        '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                        If FlagOldCalcolo = False Then
                                            valore_non_extra = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                     tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                        Else
                                            valore_non_extra = valore_non_extra * giorni_non_extra 'vecchio calcolo salvo 03.11.2022
                                        End If
                                        '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023


                                    End If

                                    'SE I GIORNI EXTRA SONO PACKED DEVO MOLTIPLICARE PER IL NUMERO DI GIORNI EXTRA E NON PER I GIORNI TOTALI DI NOLEGGIO
                                    '- EDIT: LA COLONNA GIORNI EXTRA DEVE ESSERE NON PACKED (METTO L'IF COMUNQUE PER EVITARE CONFUSIONE)
                                    If Not packed_trovato Then

                                        gg = (giorni_nolo - giorni_non_extra)

                                        '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                        If FlagOldCalcolo = False Then
                                            valore_rack_nuovo_gruppo = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                     tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                        Else
                                            valore_rack_nuovo_gruppo = val_trovato * (giorni_nolo - giorni_non_extra) 'vecchio calcolo salvo 03.11.2022
                                        End If
                                        '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023

                                    Else
                                        'PACKED
                                        ''IN QUESTO CASO LA TARIFFA è QUELLA PACKED QUINDI NON MOLTIPLICA PER I GIORNI SECONDO IL NUOVO CALCOLO
                                        valore_rack_nuovo_gruppo = val_trovato
                                    End If
                                    valore_rack_nuovo_gruppo = valore_rack_nuovo_gruppo + valore_non_extra

                                Else
                                    'NON SI TROVA IL COSTO DEI GIORNI NON EXTRA - TRATTO IL CASO COME SE FOSSE UNA COLONNA NON EXTRA (PROBABILE ERRORE DI INSERIMENTO)
                                    If Not packed_trovato Then

                                        gg = giorni_nolo

                                        '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                        If FlagOldCalcolo = False Then
                                            valore_rack_nuovo_gruppo = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                     tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                        Else
                                            valore_rack_nuovo_gruppo = val_trovato * giorni_nolo 'vecchio calcolo salvo 03.11.2022
                                        End If
                                        '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023

                                    ElseIf packed_trovato Then
                                        'PACKED
                                        ''IN QUESTO CASO LA TARIFFA è QUELLA PACKED QUINDI NON MOLTIPLICA PER I GIORNI SECONDO IL NUOVO CALCOLO
                                        valore_rack_nuovo_gruppo = val_trovato
                                    End If
                                End If
                            Else
                                'SE NON E' UN COLONNA GIORNI EXTRA
                                If Not packed_trovato Then
                                    gg = giorni_nolo

                                    '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                    If FlagOldCalcolo = False Then
                                        valore_rack_nuovo_gruppo = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                     tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                    Else
                                        valore_rack_nuovo_gruppo = val_trovato * giorni_nolo 'vecchio calcolo salvo 03.11.2022
                                    End If
                                    '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023

                                ElseIf packed_trovato Then
                                    'PACKED
                                    ''IN QUESTO CASO LA TARIFFA è QUELLA PACKED QUINDI NON MOLTIPLICA PER I GIORNI SECONDO IL NUOVO CALCOLO
                                    valore_rack_nuovo_gruppo = val_trovato
                                End If

                            End If
                        Else
                            valore_trovato = False
                        End If

                        If valore_trovato Then
                            'CONTINUO SOLAMENTE SE HO TROVATO UN VALORE - ALTRIMENTI C'E' GIA' QUALCOSA CHE NON VA NELLA TARIFFA
                            'CALCOLO IL COSTO UTLIZZANDO LA TARIFFA RACK PER IL GRUPPO SCELTO AL MOMENTO DELLA PRENOTAZIONE
                            Rs.Close()
                            Rs = Nothing

                            '1-Calcolo del costo col gruppo nuovo richiesto utilizzando la rack
                            sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa,righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                            "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                            "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_nolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_prenotazione_x_modifica_con_rack & "' AND NOT valore IS NULL AND valore<>0"

                            Dbc.Close()
                            Dbc.Open()
                            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                            Rs = Cmd.ExecuteReader()
                            Rs.Read()

                            If Rs.HasRows() Then
                                'SE IL COSTO TROVATO E' AL GIORNO ALLORA MOLTIPLICO PER IL NUMERO DI GIORNI DI NOLEGGIO EXTRA, SE E' PACKED 
                                'ALLORA DIVIDO PER IL NUMERO DI GIORNI DI NOLEGGIO TOTALI (IN MODO DA AVERE IL COSTO GIORNALIERO) E POI 
                                'MOLTIPLICO PER IL COSTO DI NOLEGGIO EXTRA
                                Dim val_trovato As Double = Rs("valore")
                                Dim packed_trovato As Boolean = Rs("pac")

                                If Rs("gg_extra") Then
                                    Dim giorni_non_extra As Integer = Rs("da") - 1

                                    Rs.Close()
                                    Rs = Nothing
                                    Dbc.Close()
                                    Dbc.Open()

                                    sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                                     "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                                     "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_non_extra & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_prenotazione_x_modifica_con_rack & "' " &
                                     "AND NOT valore IS NULL AND valore<>0"

                                    Dim valore_non_extra As Double = 0

                                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                                    Rs = Cmd.ExecuteReader()

                                    If Rs.Read() Then
                                        'HO TROVATO IL COSTO DEI GIORNI NON EXTRA 
                                        valore_non_extra = Rs("valore")
                                        If Not Rs("pac") Then

                                            gg = giorni_non_extra

                                            '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                            If FlagOldCalcolo = False Then
                                                valore_non_extra = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                     tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                            Else
                                                valore_non_extra = valore_non_extra * giorni_non_extra 'vecchio calcolo salvo 03.11.2022
                                            End If
                                            '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023

                                        End If

                                        'SE I GIORNI EXTRA SONO PACKED DEVO MOLTIPLICARE PER IL NUMERO DI GIORNI EXTRA E NON PER I GIORNI TOTALI DI NOLEGGIO
                                        '- EDIT: LA COLONNA GIORNI EXTRA DEVE ESSERE NON PACKED (METTO L'IF COMUNQUE PER EVITARE CONFUSIONE)
                                        If Not packed_trovato Then
                                            gg = (giorni_nolo - giorni_non_extra)

                                            '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                            If FlagOldCalcolo = False Then
                                                valore_rack_vecchio_gruppo = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                     tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022

                                            Else
                                                valore_rack_vecchio_gruppo = val_trovato * (giorni_nolo - giorni_non_extra) 'vecchio calcolo salvo 03.11.2022
                                            End If
                                            '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023

                                        Else
                                            'PACKED
                                            ''IN QUESTO CASO LA TARIFFA è QUELLA PACKED QUINDI NON MOLTIPLICA PER I GIORNI SECONDO IL NUOVO CALCOLO
                                            valore_rack_vecchio_gruppo = val_trovato
                                        End If

                                        valore_rack_vecchio_gruppo = valore_rack_vecchio_gruppo + valore_non_extra
                                    Else
                                        'NON SI TROVA IL COSTO DEI GIORNI NON EXTRA - TRATTO IL CASO COME SE FOSSE UNA COLONNA NON EXTRA (PROBABILE ERRORE DI INSERIMENTO)
                                        If Not packed_trovato Then
                                            gg = giorni_nolo

                                            '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                            If FlagOldCalcolo = False Then
                                                valore_rack_vecchio_gruppo = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                    tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                            Else
                                                valore_rack_vecchio_gruppo = val_trovato * giorni_nolo 'vecchio calcolo salvo 03.11.2022
                                            End If
                                            '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023


                                        ElseIf packed_trovato Then
                                            'PACKED
                                            ''IN QUESTO CASO LA TARIFFA è QUELLA PACKED QUINDI NON MOLTIPLICA PER I GIORNI SECONDO IL NUOVO CALCOLO
                                            valore_rack_vecchio_gruppo = val_trovato
                                        End If
                                    End If
                                Else
                                    'SE NON E' UN COLONNA GIORNI EXTRA
                                    If Not packed_trovato Then

                                        gg = giorni_nolo

                                        '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                        If FlagOldCalcolo = False Then
                                            valore_rack_vecchio_gruppo = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                     tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                        Else
                                            valore_rack_vecchio_gruppo = val_trovato * giorni_nolo 'vecchio calcolo salvo 03.11.2022
                                        End If
                                        '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023


                                    ElseIf packed_trovato Then
                                        'PACKED
                                        ''IN QUESTO CASO LA TARIFFA è QUELLA PACKED QUINDI NON MOLTIPLICA PER I GIORNI SECONDO IL NUOVO CALCOLO
                                        valore_rack_vecchio_gruppo = val_trovato
                                    End If
                                End If
                            Else
                                valore_trovato = False
                            End If

                            If valore_trovato Then
                                'A QUESTO PUNTO SOTTRAGGO I DUE VALORI E SOMMO LA DIFFERENZA AL VALORE-TARIFFA AL MOMENTO DELLA PRENOTAZIONE
                                'CHE INFATTI E' STATO CALCOLATO USANDO IL VECCHIO GRUPPO E LA TARIFFA USATA IN PRENOTAZIONE).
                                'NON ESSENDO PERMESSO IL DOWNSELL IL VALORE E' CERTAMENTE POSITIVO. - IN OGNI CASO SE IN QUALCHE CASO
                                'IL DOWNSELL DEVE ESSERE PERMESSO LA PROCEDURA FUNZIONA UGUALMENTE
                                valore_rack = valore_rack_nuovo_gruppo - valore_rack_vecchio_gruppo

                                'I DUE COSTI (RACK E TARIFFA AL MOMENTO DELLA PRENOTAZIONE) DEVONO ESSERE COERENTI: 
                                'AGGIUNGO O RIMUOVO L'IVA AL VALORE RACK SE NECESSARIO
                                If iva_inclusa = "True" And Not iva_inclusa_Rack Then
                                    valore_rack = valore_rack + ((valore_rack * iva_rack) / 100)
                                ElseIf iva_inclusa = "False" And iva_inclusa_Rack Then
                                    valore_rack = valore_rack / (1 + (iva_rack / 100))
                                End If

                                valore = valore + valore_rack
                            End If

                        End If

                    ElseIf (giorni_noleggio_extra_rack <> 0) And (id_gruppo_da_prenotazione_x_modifica_con_rack <> "" And id_gruppo <> id_gruppo_da_prenotazione_x_modifica_con_rack) Then

                        'IN QUEST'ULTIMO CASO SI RICHIEDE LA VARIAZIONE SIA DEI GIORNI DI NOLEGGIO CHE DEL GRUPPO. IN QUESTO CASO SI SOTTRAE
                        'IL COSTO DEL NUOVO GRUPPO E I NUOVI GIORNI DI NOLEGGIO USANDO LA RACK AL COSTO DEL GRUPPO DA PRENOTAZIONE COL 
                        'NUMERO DI GIORNI DI PRENOTAZIONE SEMPRE USANDO LA RACK E SI SOMMA LA DIFFERENZA AL COSTO DEL VECCHIO GRUPPO
                        'E VECCHI GIORNI DI PRENOTAZIONE USANDO LA TARIFFA AL MOMENTO DELLA PRENOTAZIONE (IL valore) GIA' CALCOLATO.
                        Dim valore_rack_nuovo_gruppo As Double = 0
                        Dim valore_rack_vecchio_gruppo As Double = 0
                        Dim valore_rack As Double = 0
                        Dim iva_rack As Double
                        Dim iva_inclusa_Rack As Boolean

                        Rs.Close()
                        Rs = Nothing

                        '1-Calcolo del costo col gruppo nuovo richiesto utilizzando la rack E I GIORNI DI NOLEGGIO TOTALI
                        sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                        "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                        "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_nolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo & "' AND NOT valore IS NULL AND valore<>0"

                        Dbc.Close()
                        Dbc.Open()
                        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                        Rs = Cmd.ExecuteReader()
                        Rs.Read()

                        If Rs.HasRows() Then
                            'SE IL COSTO TROVATO E' AL GIORNO ALLORA MOLTIPLICO PER IL NUMERO DI GIORNI DI NOLEGGIO EXTRA, SE E' PACKED 
                            'ALLORA DIVIDO PER IL NUMERO DI GIORNI DI NOLEGGIO TOTALI (IN MODO DA AVERE IL COSTO GIORNALIERO) E POI 
                            'MOLTIPLICO PER IL COSTO DI NOLEGGIO EXTRA
                            Dim val_trovato As Double = Rs("valore")
                            Dim packed_trovato As Boolean = Rs("pac")
                            iva_rack = Rs("iva")
                            iva_inclusa_Rack = Rs("iva_inclusa")

                            If Rs("gg_extra") Then
                                Dim giorni_non_extra As Integer = Rs("da") - 1

                                Rs.Close()
                                Rs = Nothing
                                Dbc.Close()
                                Dbc.Open()

                                sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                                 "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                                 "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_non_extra & " BETWEEN da AND a AND id_gruppo='" & id_gruppo & "' " &
                                 "AND NOT valore IS NULL AND valore<>0"

                                Dim valore_non_extra As Double = 0

                                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                                Rs = Cmd.ExecuteReader()

                                If Rs.Read() Then
                                    'HO TROVATO IL COSTO DEI GIORNI NON EXTRA 
                                    valore_non_extra = Rs("valore")
                                    If Not Rs("pac") Then
                                        gg = giorni_non_extra

                                        '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                        If FlagOldCalcolo = False Then
                                            valore_non_extra = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                    tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                        Else
                                            valore_non_extra = valore_non_extra * giorni_non_extra ' vecchio calcolo salvo 03.11.2022
                                        End If
                                        '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023

                                    End If

                                    'SE I GIORNI EXTRA SONO PACKED DEVO MOLTIPLICARE PER IL NUMERO DI GIORNI EXTRA E NON PER I GIORNI TOTALI DI NOLEGGIO
                                    '- EDIT: LA COLONNA GIORNI EXTRA DEVE ESSERE NON PACKED (METTO L'IF COMUNQUE PER EVITARE CONFUSIONE)
                                    If Not packed_trovato Then
                                        '# nuovo calcolo tempoKm 03.11.2022 Salvo
                                        gg = (giorni_nolo - giorni_non_extra)
                                        'new_Valore = funzioni_comuni_new.GetNewTariffaTempoKm(id_tempo_km_rack, id_gruppo, gg)
                                        '@ end fine new calcolo salvo

                                        'inserito x nuovo calcolo - salvo 06.12.2022
                                        new_Valore = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                     tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022

                                        valore_rack_nuovo_gruppo = new_Valore  'val_trovato * (giorni_nolo - giorni_non_extra) ' vecchio calcolo salvo 03.11.2022

                                        '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                        If FlagOldCalcolo = False Then
                                            valore_rack_nuovo_gruppo = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                     tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                        Else
                                            valore_rack_nuovo_gruppo = val_trovato * (giorni_nolo - giorni_non_extra) ' vecchio calcolo salvo 03.11.2022
                                        End If
                                        '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023


                                    Else
                                        valore_rack_nuovo_gruppo = val_trovato
                                    End If

                                    valore_rack_nuovo_gruppo = valore_rack_nuovo_gruppo + valore_non_extra

                                Else
                                    'NON SI TROVA IL COSTO DEI GIORNI NON EXTRA - TRATTO IL CASO COME SE FOSSE UNA COLONNA NON EXTRA (PROBABILE ERRORE DI INSERIMENTO)
                                    If Not packed_trovato Then
                                        valore_rack_nuovo_gruppo = val_trovato * giorni_nolo
                                    ElseIf packed_trovato Then
                                        valore_rack_nuovo_gruppo = val_trovato
                                    End If
                                End If
                            Else
                                'SE NON E' UN COLONNA GIORNI EXTRA
                                If Not packed_trovato Then

                                    gg = giorni_nolo

                                    '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                    If FlagOldCalcolo = False Then
                                        valore_rack_nuovo_gruppo = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                    tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                    Else
                                        valore_rack_nuovo_gruppo = val_trovato * giorni_nolo ' vecchio calcolo salvo 03.11.2022
                                    End If
                                    '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023

                                ElseIf packed_trovato Then
                                    valore_rack_nuovo_gruppo = val_trovato
                                End If
                            End If
                        Else
                            valore_trovato = False
                        End If

                        If valore_trovato Then
                            'CONTINUO SOLAMENTE SE HO TROVATO UN VALORE - ALTRIMENTI C'E' GIA' QUALCOSA CHE NON VA NELLA TARIFFA
                            'CALCOLO IL COSTO UTLIZZANDO LA TARIFFA RACK PER IL GRUPPO SCELTO AL MOMENTO DELLA PRENOTAZIONE
                            Rs.Close()
                            Rs = Nothing

                            '1-Calcolo del costo col gruppo richiesto al momento della prenotazione utilizzando la rack
                            sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                            "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                            "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_nolo - giorni_noleggio_extra_rack & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_prenotazione_x_modifica_con_rack & "' AND NOT valore IS NULL AND valore<>0"
                            Dbc.Close()
                            Dbc.Open()
                            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                            Rs = Cmd.ExecuteReader()
                            Rs.Read()

                            If Rs.HasRows() Then
                                'SE IL COSTO TROVATO E' AL GIORNO ALLORA MOLTIPLICO PER IL NUMERO DI GIORNI DI NOLEGGIO EXTRA, SE E' PACKED 
                                'ALLORA DIVIDO PER IL NUMERO DI GIORNI DI NOLEGGIO TOTALI (IN MODO DA AVERE IL COSTO GIORNALIERO) E POI 
                                'MOLTIPLICO PER IL COSTO DI NOLEGGIO EXTRA
                                Dim val_trovato As Double = Rs("valore")
                                Dim packed_trovato As Boolean = Rs("pac")

                                If Rs("gg_extra") Then
                                    Dim giorni_non_extra As Integer = Rs("da") - 1

                                    Rs.Close()
                                    Rs = Nothing
                                    Dbc.Close()
                                    Dbc.Open()

                                    sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                                     "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                                     "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_non_extra & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_prenotazione_x_modifica_con_rack & "' " &
                                     "AND NOT valore IS NULL AND valore<>0"

                                    Dim valore_non_extra As Double = 0

                                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                                    Rs = Cmd.ExecuteReader()

                                    If Rs.Read() Then
                                        'HO TROVATO IL COSTO DEI GIORNI NON EXTRA 
                                        valore_non_extra = Rs("valore")
                                        If Not Rs("pac") Then
                                            '# nuovo calcolo tempoKm 03.11.2022 Salvo
                                            gg = giorni_non_extra
                                            'new_Valore = funzioni_comuni_new.GetNewTariffaTempoKm(id_tempo_km_rack, id_gruppo_da_prenotazione_x_modifica_con_rack, gg)
                                            '@ end fine new calcolo salvo

                                            'inserito x nuovo calcolo - salvo 06.12.2022
                                            new_Valore = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                     tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022

                                            valore_non_extra = new_Valore  'valore_non_extra * giorni_non_extra 'vecchio calcolo salvo 03.11.2022

                                            '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                            If FlagOldCalcolo = False Then
                                                valore_non_extra = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                     tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022

                                            Else
                                                valore_non_extra = valore_non_extra * giorni_non_extra 'vecchio calcolo salvo 03.11.2022
                                            End If
                                            '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023

                                        End If

                                        'SE I GIORNI EXTRA SONO PACKED DEVO MOLTIPLICARE PER IL NUMERO DI GIORNI EXTRA E NON PER I GIORNI TOTALI DI NOLEGGIO
                                        '- EDIT: LA COLONNA GIORNI EXTRA DEVE ESSERE NON PACKED (METTO L'IF COMUNQUE PER EVITARE CONFUSIONE)
                                        If Not packed_trovato Then
                                            gg = (giorni_nolo - giorni_noleggio_extra_rack - giorni_non_extra)

                                            '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                            If FlagOldCalcolo = False Then
                                                valore_rack_vecchio_gruppo = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                     tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                            Else
                                                valore_rack_vecchio_gruppo = val_trovato * (giorni_nolo - giorni_noleggio_extra_rack - giorni_non_extra) 'vecchio calcolo 03.11.2022
                                            End If
                                            '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023


                                        Else
                                            valore_rack_vecchio_gruppo = val_trovato
                                        End If

                                        valore_rack_vecchio_gruppo = valore_rack_vecchio_gruppo + valore_non_extra

                                    Else
                                        'NON SI TROVA IL COSTO DEI GIORNI NON EXTRA - TRATTO IL CASO COME SE FOSSE UNA COLONNA NON EXTRA (PROBABILE ERRORE DI INSERIMENTO)
                                        If Not packed_trovato Then
                                            gg = (giorni_nolo - giorni_noleggio_extra_rack)

                                            '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                            If FlagOldCalcolo = False Then
                                                valore_rack_vecchio_gruppo = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                     tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                            Else
                                                valore_rack_vecchio_gruppo = val_trovato * (giorni_nolo - giorni_noleggio_extra_rack) 'vecchio calcolo salv 03.11.2022
                                            End If
                                            '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023


                                        ElseIf packed_trovato Then
                                            valore_rack_vecchio_gruppo = val_trovato
                                        End If
                                    End If
                                Else
                                    'SE NON E' UN COLONNA GIORNI EXTRA
                                    If Not packed_trovato Then
                                        gg = (giorni_nolo - giorni_noleggio_extra_rack)

                                        '# nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023
                                        If FlagOldCalcolo = False Then
                                            valore_rack_vecchio_gruppo = funzioni_comuni_new.GetNewTariffaTempoKmPeriodi(stazione_pick_up, stazione_drop_off, dataPickUp, dataDropOff,
                                                                                    tipoCli, id_gruppo, TipoTariffa, descTariffa, giorni_noleggio, idTariffa, False, "", "", max_sconto_new.ToString) 'nuovo calcolo senza periodi 06.12.2022
                                        Else
                                            valore_rack_vecchio_gruppo = val_trovato * (giorni_nolo - giorni_noleggio_extra_rack) 'vecchio calcolo salvo 03.11.2022
                                        End If
                                        '@ nuova condizione se nuovo o vecchio calcolo salvo 10.01.2023


                                    ElseIf packed_trovato Then
                                        valore_rack_vecchio_gruppo = val_trovato
                                    End If
                                End If
                            Else
                                valore_trovato = False
                            End If

                            If valore_trovato Then
                                'A QUESTO PUNTO SOTTRAGGO I DUE VALORI E SOMMO LA DIFFERENZA AL VALORE-TARIFFA AL MOMENTO DELLA PRENOTAZIONE
                                'CHE INFATTI E' STATO CALCOLATO USANDO IL VECCHIO GRUPPO E LA TARIFFA USATA IN PRENOTAZIONE).
                                'NON ESSENDO PERMESSO IL DOWNSELL IL VALORE E' CERTAMENTE POSITIVO. - IN OGNI CASO SE IN QUALCHE CASO
                                'IL DOWNSELL DEVE ESSERE PERMESSO LA PROCEDURA FUNZIONA UGUALMENTE
                                valore_rack = valore_rack_nuovo_gruppo - valore_rack_vecchio_gruppo

                                valore_rack = valore_rack - (valore_rack * sconto_su_rack / 100)

                                'I DUE COSTI (RACK E TARIFFA AL MOMENTO DELLA PRENOTAZIONE) DEVONO ESSERE COERENTI: 
                                'AGGIUNGO O RIMUOVO L'IVA AL VALORE RACK SE NECESSARIO
                                If iva_inclusa = "True" And Not iva_inclusa_Rack Then
                                    valore_rack = valore_rack + ((valore_rack * iva_rack) / 100)
                                ElseIf iva_inclusa = "False" And iva_inclusa_Rack Then
                                    valore_rack = valore_rack / (1 + (iva_rack / 100))
                                End If

                                valore = valore + valore_rack
                            End If

                        End If
                    End If

                    If valore_trovato Then
                        If SetTariffaOri = True Then
                            If ggExtra = "0" Then
                                valore = CDbl(ValoreTariffaOri)
                            Else
                                'ci sono giorni extra su tariffa originale:
                                'il valore è quello calcolato  salvo 07.03.2023
                                valore = valore
                            End If

                        End If
                        salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Costanti.ID_tempo_km, "", "Valore Tariffa" & "", valore, "NULL", iva, codice_iva, iva_inclusa, "True", "False", "False", Costanti.id_accessorio_incluso, "2", "True", "1", 1, "NULL", Costanti.id_unita_misura_giorni, "", packed, qta, "", False, prepagata)
                    End If

                End If
            End If

            Rs.Close()
            Rs = Nothing


            If Not valore_trovato Then
                'ALLA FINE DI TUTTO SE NON SONO RIUSCITO A TROVARE NULLA NE DALLA MADRE NE DALLA FIGLIA SALVO UNA RIGA DI ERRORE -
                'SALVO L'ERRORE ANCHE SE C'E' QUALCOSA CHE NON VA (OVVERO QUALCHE VALORE NON DEFINITO) NEL CALCOLO DELLE ESTENSIONI CON 
                'RACK
                salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Costanti.ID_tempo_km, "", "ERRORE - VALORE TARIFFA NON TROVATO", "0", "NULL", "0", "NULL", "False", "True", "False", "False", "NULL", "1", "", "0", 1, "NULL", "NULL", "", "NULL", "0", "", False, prepagata)
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception

            HttpContext.Current.Response.Write("error funzioni_comuni calcola_tempo_km() : " & ex.Message & "<br/>" & sqlStr & "<br/>")

        End Try

    End Sub

    Protected Sub calcolo_iva_e_totale_singolo_accessorio(ByVal stazione_pick_up As String, ByVal sconto As Double, ByVal id_gruppo As String, ByVal id_accessorio As String, ByVal num_elemento As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal imposta_prepagato As Boolean, ByVal tipo_commissione As String, ByVal percentuale_commissioni As String, ByVal id_fonte_commissionabile As String)       

        'SERVE PER CALCOLARE IVA E AGGIORNARE L'ONERE E IL TOTALE PER UN SINGOLO ACCESSORIO (SERVE PER JOUNG DRIVER SECONDO GUIDATORE ED
        'ACCESSORI NON VALORIZZATI INIZIALMENTE)

        Dim id_da_salvare As String
        Dim tabella As String

        If id_preventivo <> "" Then
            id_da_salvare = id_preventivo
            tabella = "preventivi_costi"
        ElseIf id_ribaltamento <> "" Then
            id_da_salvare = id_ribaltamento
            tabella = "ribaltamento_costi"
        ElseIf id_prenotazione <> "" Then
            id_da_salvare = id_prenotazione
            tabella = "prenotazioni_costi"
        ElseIf id_contratto <> "" Then
            id_da_salvare = id_contratto
            tabella = "contratti_costi"
        End If

        Dim condizione_num As String = ""
        If num_elemento <> "" And num_elemento <> "NULL" Then
            condizione_num = " AND num_elemento='" & num_elemento & "'"
        End If

        Dim tempo_km As Double = 0

        Dim totale As Double = 0
        Dim totale_imponibile As Double = 0
        Dim totale_iva As Double = 0

        Dim valore_percentuale As Double
        Dim imponibile_percentuale As Double
        Dim iva_percentuale As Double
        Dim aliquota_percentuale As Double

        Dim totale_sconto As Double = 0

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Rs As Data.SqlClient.SqlDataReader

        '0 - PER L'ACCESSORIO CONSIDERATO AGGIORNO IL VALORE DI IMPONIBILE E IL VALORE DI IVA-----------------------------------------------
        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=(valore_costo*100/(100+aliquota_iva)), iva_imponibile=(valore_costo*aliquota_iva/(100+aliquota_iva)) WHERE iva_inclusa='True' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL AND id_elemento='" & id_accessorio & "'" & condizione_num, Dbc)
        Cmd.ExecuteNonQuery()
        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=valore_costo, iva_imponibile=(valore_costo*aliquota_iva/100) WHERE iva_inclusa='False' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL AND id_elemento='" & id_accessorio & "'" & condizione_num, Dbc)
        Cmd.ExecuteNonQuery()
        '-----------------------------------------------------------------------------------------------------------------------------------

        '0A - CALCOLO DELL'EVENTUALE SCONTO RICHIESTO DALL'OPERATORE APPLICANDOLO ALL'IMPONIBILE DEGLI ELEMENTI SCONTABILI------------------
        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile_scontato=imponibile-((imponibile - ISNULL(imponibile_scontato_prepagato,0))*" & sconto & "/100) WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL AND scontabile='1' AND id_elemento='" & id_accessorio & "'" & condizione_num, Dbc)
        Cmd.ExecuteNonQuery()
        'PER GLI ELEMENTI NON SCONTABILI SETTO L'IMPONIBILE SCONTATO UGUALE ALL'IMPONIBILE
        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile_scontato=imponibile, iva_imponibile_scontato=iva_imponibile WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL AND scontabile='0' AND id_elemento='" & id_accessorio & "'" & condizione_num, Dbc)
        Cmd.ExecuteNonQuery()
        '-----------------------------------------------------------------------------------------------------------------------------------
        '0B - CALCOLO DELL'IVA SULL'IMPONIBILE SCONTATO ------------------------------------------------------------------------------------
        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET iva_imponibile_scontato=imponibile_scontato*aliquota_iva/100  WHERE (iva_inclusa='True' OR iva_inclusa='False') AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL AND scontabile='1' AND id_elemento='" & id_accessorio & "'" & condizione_num, Dbc)
        Cmd.ExecuteNonQuery()
        '-----------------------------------------------------------------------------------------------------------------------------------

        '1 - ELEMENTO SELEZIONATO (NEL CASO DI ELEMENTO GENERICO TROVO SOLO UNA RIGA, NEL CASO DI YOUNG DRIVER SECONDO GUIDATORE
        ' FACCIO IN MODO DI SELEZIONARE IN QUANTO POTREBBE ESISTERE ANCHE YOUNG DRIVER PER IL PRIMO GUIDATORE 
        Cmd = New Data.SqlClient.SqlCommand("SELECT TOP 1 ISNULL(imponibile_scontato + iva_imponibile_scontato, 0) AS valore, ISNULL(imponibile_scontato, 0) AS imponibile_scontato, ISNULL(imponibile, 0) AS imponibile, ISNULL(iva_imponibile_scontato, 0) AS iva_imponibile_scontato, ISNULL(iva_imponibile, 0) AS iva_imponibile FROM " & tabella & " WITH(NOLOCK) WHERE id_elemento='" & id_accessorio & "'" & condizione_num & " AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL", Dbc)
        Rs = Cmd.ExecuteReader
        If Rs.Read() Then
            totale_sconto = totale_sconto + (Rs("imponibile") + Rs("iva_imponibile")) - (Rs("imponibile_scontato") + Rs("iva_imponibile_scontato"))
            totale_sconto = FormatNumber(totale_sconto, 4, , , TriState.False)

            totale = totale + Rs("valore")

            totale_imponibile = totale_imponibile + Rs("imponibile_scontato")
            totale_iva = totale_iva + Rs("iva_imponibile_scontato")
        End If

        Dbc.Close()
        Dbc.Open()
        ''-------------------------------------------------------------------------------------------------------------------------------

        'TARIFFA COMMISSIONABILE: SE LA COMMISSIONE E' DA RICONOSCERE DOPO EFFETTUO IL CALCOLO DELLA COMMISSIONE SOLO SE L'ELEMENTO E' COMMISSIONABILE 
        Dim elemento_commissionabile As Boolean = False
        If tipo_commissione = "1" Then
            Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM fonti_commissionabili_x_elementi WHERE id_fonte_commissionabile='" & id_fonte_commissionabile & "' AND id_elemento_condizione='" & id_accessorio & "'", Dbc)
            Dim test As String = Cmd.ExecuteScalar & ""
            If test <> "" Then
                elemento_commissionabile = True
            End If
        End If

        '5 - ELEMENTO ONERE (SE CALCOLATO IN PERCENTUALE - SE APPLICABILE ALL'ELEMENTO) - SI APPLICA SEMPRE ALL'IMPONIBILE ED E' SEMPRE
        'E COMUNQUE IVA ESCLUSA
        Cmd = New Data.SqlClient.SqlCommand("SELECT id_onere FROM stazioni WITH(NOLOCK) WHERE id='" & stazione_pick_up & "'", Dbc)
        Dim id_onere As String = Cmd.ExecuteScalar
        Dim calcola_onere As Boolean

        Cmd = New Data.SqlClient.SqlCommand("SELECT valore_percentuale, aliquota_iva ,iva_inclusa FROM " & tabella & " WITH(NOLOCK) WHERE id_a_carico_di<>'5' AND obbligatorio='1' And id_metodo_stampa<>'3' And id_metodo_stampa<>'4'  AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_percentuale IS NULL AND id_elemento='" & id_onere & "'", Dbc)
        Rs = Cmd.ExecuteReader
        If Rs.Read() Then
            'SE E' STATO TROVATO UN ELEMENTO PERCENTUALE RECUPERO IL VALORE E CALCOLO IL VALORE PERCENTUALE IVATO
            valore_percentuale = Rs("valore_percentuale")
            aliquota_percentuale = Rs("aliquota_iva")

            'CONTROLLO SE L'ELEMENTO PERCENTUALE E' STATO SPECIFICATO NELLA TABELLA condizioni_elementi_percentuale o se in essa non è stato
            'SPECIFICATO NIENTE (IN QUESTO CASO SI INTENDE CHE SI APPLIACA A TUTTI GLI ELEMENTI QUINDI ANCHE A QUELLO PASSATO).

            Dbc.Close()
            Dbc.Open()

            Cmd = New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM condizioni_elementi_percentuale WITH(NOLOCK) WHERE id_elemento1='" & id_onere & "'", Dbc)
            Dim test As String = Cmd.ExecuteScalar & ""

            If test <> "" Then
                'IN QUESTO CASO SONO STATI SPECIFICATI DEGLI ELEMENTI. CONTROLLO SE L'ELEMENTO PASSATO E' TRA QUESTI
                Cmd = New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM condizioni_elementi_percentuale WITH(NOLOCK) WHERE id_elemento1='" & id_onere & "' AND id_elemento2='" & id_accessorio & "'", Dbc)
                test = Cmd.ExecuteScalar & ""
                If test = "" Then
                    'ONERE NON DA CALCOLARE: L'ELEMENTO NON E' TRA QUELLI SPECIFICATI NELLA TABELLA condizioni_elementi_percentuale
                    calcola_onere = False
                Else
                    'ONERE DA CALCOLARE: L'ELEMENTO E' TRA QUELLI SPECIFICATI NELLA TABELLA condizioni_elementi_percentuale
                    calcola_onere = True
                End If
            Else
                'IN QUESTO CASO L'ONERE E' DA CALCOLARE (ONERE NON SPECIFICATO IN condizioni_elementi_percentuale)
                calcola_onere = True
            End If

            If calcola_onere Then
                imponibile_percentuale = totale_imponibile * valore_percentuale / 100
                iva_percentuale = imponibile_percentuale * aliquota_percentuale / 100

                'AGGIORNO LA RIGA PERCENTUALE COL VALORE DI IMPONIBILE E DI IVA

                totale_imponibile = totale_imponibile + imponibile_percentuale
                totale_iva = totale_iva + iva_percentuale

                totale = totale + imponibile_percentuale + iva_percentuale

                Dim onere_prepagato As String = ""
                If imposta_prepagato Then
                    'SE SI STA IMPOSTANDO IL TOTALE COME PREPAGATO ALLORA E' NECESSARIO AGGIORNARE IL TOTALE AGGIUNGENDO IL COSTO DELL'ELEMENTO
                    onere_prepagato = ", imponibile_scontato_prepagato=imponibile_scontato_prepagato+" & Replace(imponibile_percentuale, ",", ".") & ", iva_imponibile_scontato_prepagato=iva_imponibile_scontato_prepagato+" & Replace(iva_percentuale, ",", ".")
                End If
                Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=imponibile+" & Replace(imponibile_percentuale, ",", ".") & ", imponibile_scontato=imponibile_scontato+" & Replace(imponibile_percentuale, ",", ".") & ",iva_imponibile=iva_imponibile+" & Replace(iva_percentuale, ",", ".") & ", iva_imponibile_scontato=iva_imponibile_scontato+" & Replace(iva_percentuale, ",", ".") & onere_prepagato & " WHERE id_elemento='" & id_onere & "' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "'", Dbc)
                Cmd.ExecuteScalar()

                'SALVO PER L'ELEMENTO L'ONERE ED IL TOTALE DELL'ONERE
                Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile_onere='" & Replace(imponibile_percentuale, ",", ".") & "', iva_onere='" & Replace(iva_percentuale, ",", ".") & "' WHERE " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "' AND " & tabella & ".id_elemento='" & id_accessorio & "'", Dbc)
                'HttpContext.Current.Trace.Write(Cmd.CommandText)
                Cmd.ExecuteNonQuery()
            End If
            '-------------------------------------------------------------------------------------------------------------------------------
        End If
        Dbc.Close()
        Dbc.Open()

        '6 - AGGIORNO LA RIGA DI SCONTO (SE DIVERSO DA 0)----------------------------------------------------------------------------------
        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=imponibile+" & Replace(totale_sconto, ",", ".") & ", imponibile_scontato=imponibile_scontato+" & Replace(totale_sconto, ",", ".") & " WHERE ordine_stampa='5' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL", Dbc)
        Cmd.ExecuteNonQuery()
        '-------------------------------------------------------------------------------------------------------------------------------

        '7 - AGGIORNO LA RIGA TOTALE----------------------------------------------------------------------------------------------------
        Dim totale_prepagato As String = ""
        If imposta_prepagato Then
            'SE SI STA IMPOSTANDO IL TOTALE COME PREPAGATO ALLORA E' NECESSARIO AGGIORNARE IL TOTALE AGGIUNGENDO IL COSTO DELL'ELEMENTO
            totale_prepagato = ", imponibile_scontato_prepagato=imponibile_scontato_prepagato+" & Replace(totale_imponibile - imponibile_percentuale, ",", ".") & ", iva_imponibile_scontato_prepagato=iva_imponibile_scontato_prepagato+" & Replace(totale_iva - iva_percentuale, ",", ".") & ",imponibile_onere_prepagato=imponibile_onere_prepagato+" & Replace(imponibile_percentuale, ",", ".") & ", iva_onere_prepagato=iva_onere_prepagato+" & Replace(iva_percentuale, ",", ".")
        End If
        Dim update_commissioni As String = ""
        If elemento_commissionabile Then
            'SE L'ELEMENTO E' COMMISSIONABILE AUMENTO, NEL TOTALE, I VALORI DELLE COMMISSIONI
            update_commissioni = ", commissioni_imponibile_originale=ISNULL(commissioni_imponibile_originale,0)+" & Replace((totale_imponibile - imponibile_percentuale) * CDbl(percentuale_commissioni) / 100, ",", ".") &
                ", commissioni_iva_originale=ISNULL(commissioni_iva_originale,0)+" & Replace((totale_iva - iva_percentuale) * CDbl(percentuale_commissioni) / 100, ",", ".")
        End If
        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET valore_costo=valore_costo+" & Replace(totale_imponibile + totale_iva, ",", ".") & ", imponibile=imponibile+" & Replace(totale_imponibile - imponibile_percentuale, ",", ".") & ",  imponibile_scontato=imponibile_scontato+" & Replace(totale_imponibile - imponibile_percentuale, ",", ".") & ",iva_imponibile=iva_imponibile+" & Replace(totale_iva - iva_percentuale, ",", ".") & ", iva_imponibile_scontato=iva_imponibile_scontato+" & Replace(totale_iva - iva_percentuale, ",", ".") & ", imponibile_onere=imponibile_onere+" & Replace(imponibile_percentuale, ",", ".") & ", iva_onere=iva_onere+" & Replace(iva_percentuale, ",", ".") & totale_prepagato & update_commissioni & " WHERE ordine_stampa='6' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL", Dbc)
        Cmd.ExecuteNonQuery()
        '------------------------------------------------------------------------------------------------------------------------------- 
        '8 - SE SIAMO NEL CASO DI ELEMENTO COMMISSIONABILE DEVO AGGIORNARE LA RIGA DELL'ELEMENTO SALVANDO LE COMMISSIONI ---------------
        If elemento_commissionabile Then
            Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET commissioni_imponibile_originale=ISNULL(imponibile_scontato,0)*" & Replace(CDbl(percentuale_commissioni), ",", ".") & "/100, commissioni_iva_originale=ISNULL(iva_imponibile_scontato,0)*" & Replace(CDbl(percentuale_commissioni), ",", ".") & "/100  WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_accessorio & "'" & condizione_num, Dbc)
            Cmd.ExecuteNonQuery()
        End If
        '-------------------------------------------------------------------------------------------------------------------------------

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

    End Sub

    Protected Sub calcolo_iva_e_totale(ByVal stazione_pick_up As String, ByVal id_tempo_km_figlia As String, ByVal id_condizione_figlia As String, ByVal id_condizione_madre As String, ByVal sconto As Double, ByVal sconto_web_prepagato_primo_calcolo As Double, ByVal tipo_sconto As String, ByVal id_gruppo As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal is_broker As Boolean, ByVal tipo_commissione As String, ByVal primo_calcolo_commissioni As Boolean)
        'SELEZIONO LE RIGHE DA preventivi_costi RELATIVI ALL'ATTUALE NUMERO DI CALCOLO DELLA PRENOTAZIONE E AL GRUPPO CONSIDERATO E LE
        'ANALIZZO PER DETERMINARE L'IVA E IL TOTALE

        Dim tabella As String
        Dim id_da_salvare As String

        Try



            If id_preventivo <> "" Then
                id_da_salvare = id_preventivo
                tabella = "preventivi_costi"
            ElseIf id_ribaltamento <> "" Then
                id_da_salvare = id_ribaltamento
                tabella = "ribaltamento_costi"
            ElseIf id_prenotazione <> "" Then
                id_da_salvare = id_prenotazione
                tabella = "prenotazioni_costi"
            ElseIf id_contratto <> "" Then
                id_da_salvare = id_contratto
                tabella = "contratti_costi"
            End If

            Dim tempo_km As Double = 0
            Dim valore_tariffa As String = ""

            Dim totale As Double = 0

            Dim totale_imponibile_scontato As Double = 0
            Dim totale_iva As Double = 0

            Dim valore_percentuale As Double
            Dim imponibile_percentuale As Double
            Dim iva_percentuale As Double
            Dim aliquota_percentuale As Double

            Dim totale_sconto As Double = 0

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Rs As Data.SqlClient.SqlDataReader

            '0 - PER OGNI RIGA DEL DETTAGLIO COSTI SALVO IL VALORE DI IMPONIBILE E IL VALORE DI IVA---------------------------------------------
            'SE ERA STATO APPLICATO UNO SCONTO SU PREPAGATO LO CONSIDERO NEL CALCOLO
            'SE IL TIPO COMMISSIONE (X AGENZIE DI VIAGGI) E' "2" (COMMISSIONE PREPAGATA) ALLORA L'IMPORTO (GIA' CALCOLATO) DEVE ESSERE SOTTRATTO AL TEMPO KM 
            'NEL CASO DI COMMISSIONI "1" (RICONOSCIUTE DOPO) I CAMPI commmissioni_imponibile E commissioni_iva IN QUESTO MOMENTO SONO SICURAMENTE A NULL (LE COMMISSIONI
            'VENGONO CALCOLATE SUCCESSIVAMENTE ALLA CHIAMATA DI QUESTO METODO)
            Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=(valore_costo*100/(100+aliquota_iva)) - ISNULL(sconto_su_imponibile_prepagato,0) - ISNULL(commissioni_imponibile,0), iva_imponibile=(valore_costo*aliquota_iva/(100+aliquota_iva)) - (ISNULL(sconto_su_imponibile_prepagato,0)*aliquota_iva/100) - ISNULL(commissioni_iva,0) WHERE iva_inclusa='True' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL", Dbc)
            Cmd.ExecuteNonQuery()
            Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=valore_costo - ISNULL(sconto_su_imponibile_prepagato,0)  - ISNULL(commissioni_imponibile,0), iva_imponibile=((valore_costo - ISNULL(sconto_su_imponibile_prepagato,0) - ISNULL(commissioni_imponibile,0))*aliquota_iva/100 ) WHERE iva_inclusa='False' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL", Dbc)
            Cmd.ExecuteNonQuery()
            '-----------------------------------------------------------------------------------------------------------------------------------

            '0A - CALCOLO DELL'EVENTUALE SCONTO RICHIESTO DALL'OPERATORE APPLICANDOLO ALL'IMPONIBILE DEGLI ELEMENTI SCONTABILI------------------
            Dim condizione1_sconto As String = ""
            Dim condizione2_sconto As String = ""
            'NEL CASO DI SCONTO SOLO SU TEMPO KM FACCIO IN MODO DI EFFETTUARE IL CALCOLO SOLO SU QUELL'ELEMENTO
            If tipo_sconto = "1" Or sconto_web_prepagato_primo_calcolo > 0 Then
                'SE VIENE PASSATO LO SCONTO PREPAGATO DA WEB PER IL PRIMO CALCOLO IL TIPO SCONTO DEVE ESSERE NECESSARIAMENTE DI TIPO 1 (SOLO SU TEMPO KM)
                condizione1_sconto = " AND " & tabella & ".id_elemento='" & Costanti.ID_tempo_km & "'"
                condizione2_sconto = " AND " & tabella & ".id_elemento<>'" & Costanti.ID_tempo_km & "'"
            ElseIf tipo_sconto = "0" Then
                condizione1_sconto = " AND scontabile='1'"
                condizione2_sconto = " AND scontabile='0'"
            End If

            Dim sconto_da_calcolare As Double = 0
            If sconto_web_prepagato_primo_calcolo > 0 Then
                sconto_da_calcolare = sconto_web_prepagato_primo_calcolo
            ElseIf sconto > 0 Then
                sconto_da_calcolare = sconto
            End If



            Dim salva_sconto_prepagato As String = ""

            If sconto_web_prepagato_primo_calcolo > 0 Then

                'la tariffa è già scontata non deve togliere ulteriore sconto - salvo 20.06.2023
                Dim sqls As String = "UPDATE " & tabella & " SET imponibile=imponibile-(0),iva_imponibile=(imponibile-(0))*aliquota_iva/100, imponibile_scontato=imponibile-(0), sconto_su_imponibile_prepagato=0 WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL " & condizione1_sconto

                'la stringa originale contiene un ulteriore calcolo di sconto che nn deve esserci - salvo 20.06.2023
                '"UPDATE " & tabella & " SET imponibile=imponibile-(imponibile*" & sconto_da_calcolare & "/100),iva_imponibile=(imponibile-(imponibile*" & sconto_da_calcolare & "/100))*aliquota_iva/100, imponibile_scontato=imponibile-(imponibile*" & sconto_da_calcolare & "/100), sconto_su_imponibile_prepagato=imponibile*" & sconto_da_calcolare & "/100 WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL " & condizione1_sconto

                Cmd = New Data.SqlClient.SqlCommand(sqls, Dbc)
                Cmd.ExecuteNonQuery()

            Else
                'salvo 01.06.2023 e 08.07.2023
                If tabella = "preventivi_costi" Then
                    If HttpContext.Current.Session("apre_preventivo") = "apre_preventivo" Or HttpContext.Current.Session("quickckin_apertura") = "apro" Then ''richiamato da apre preventivo salvo 15.06.2023 
                        'NON DEVE APPLICARE NUOVAMENTE LO SCONTO
                        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " Set imponibile_scontato=imponibile WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL " & condizione1_sconto, Dbc)
                    Else
                        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile_scontato=imponibile-((imponibile-ISNULL(imponibile_scontato_prepagato,0))*" & sconto_da_calcolare & "/100) WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL " & condizione1_sconto, Dbc)
                    End If
                    If HttpContext.Current.Session("quickckin_apertura") = "apro" And tabella = "contratti_costi" Then
                        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile_scontato=imponibile-((imponibile-ISNULL(imponibile_scontato_prepagato,0))*" & sconto_da_calcolare & "/100) WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL " & condizione1_sconto, Dbc)
                    End If

                Else
                    Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile_scontato=imponibile-((imponibile-ISNULL(imponibile_scontato_prepagato,0))*" & sconto_da_calcolare & "/100) WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL " & condizione1_sconto, Dbc)
                End If

                Cmd.ExecuteNonQuery()




            End If


            'HttpContext.Current.Trace.Write("UPDATE " & tabella & " SET imponibile_scontato=imponibile-((imponibile-ISNULL(imponibile_scontato_prepagato,0))*" & sconto & "/100) WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL " & condizione1_sconto)

            'PER GLI ELEMENTI NON SCONTABILI SETTO L'IMPONIBILE SCONTATO UGUALE ALL'IMPONIBILE
            Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile_scontato=imponibile, iva_imponibile_scontato=iva_imponibile WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL " & condizione2_sconto, Dbc)
            Cmd.ExecuteNonQuery()
            '-----------------------------------------------------------------------------------------------------------------------------------
            '0B - CALCOLO DELL'IVA SULL'IMPONIBILE SCONTATO ------------------------------------------------------------------------------------
            'anche per l'iva e per il num_calcolo 3 deve inserire lo stesso valore di iva_imponibile

            If tabella = "preventivi_costi" Then
                If num_calcolo = "3" And tabella = "preventivi_costi" Then 'qui al num_calcolo=3 deve inserire l'imponibile_scontato con lo stesso valore di imponibile 
                    'NON DEVE APPLICARE NUOVAMENTE LO SCONTO
                    Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET iva_imponibile_scontato=iva_imponibile WHERE (iva_inclusa='True' OR iva_inclusa='False') AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL" & condizione1_sconto, Dbc)
                Else
                    Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET iva_imponibile_scontato=imponibile_scontato*aliquota_iva/100  WHERE (iva_inclusa='True' OR iva_inclusa='False') AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL" & condizione1_sconto, Dbc)
                End If
                If tabella = "contratti_costi" And HttpContext.Current.Session("quickckin_apertura") = "apro" Then
                    Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET iva_imponibile_scontato=imponibile_scontato*aliquota_iva/100  WHERE (iva_inclusa='True' OR iva_inclusa='False') AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL" & condizione1_sconto, Dbc)
                End If
                HttpContext.Current.Session("quickckin_apertura") = "" 'reset session apre  preventivo salvo 08.07.2023


            Else
                Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET iva_imponibile_scontato=imponibile_scontato*aliquota_iva/100  WHERE (iva_inclusa='True' OR iva_inclusa='False') AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL" & condizione1_sconto, Dbc)
            End If

            Cmd.ExecuteNonQuery()
            '-----------------------------------------------------------------------------------------------------------------------------------


            '1 - SELEZIONO IL VALORE TARIFFA - SE NON E' STATO TROVATO (QUINDI NEMMENO NELLA MADRE) DEVO MOSTRARE UN ERRORE
            Cmd = New Data.SqlClient.SqlCommand("SELECT  imponibile, imponibile_scontato, iva_imponibile, iva_imponibile_scontato FROM " & tabella & " WITH(NOLOCK)  WHERE id_elemento='" & Costanti.ID_tempo_km & "' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "'", Dbc)
            Rs = Cmd.ExecuteReader
            If Rs.Read() Then

                'se num_calcolo =3 (o >3 ?) non deve riapplicare lo sconto salvo 01.06.2023

                Dim imp_scontato As String
                Dim iva_imp_scontato As String


                totale_sconto = CDbl(Rs("imponibile")) - CDbl(Rs("imponibile_scontato")) + CDbl(Rs("iva_imponibile")) - CDbl(Rs("iva_imponibile_scontato"))

                totale_sconto = FormatNumber(totale_sconto, 4, , , TriState.False)

                valore_tariffa = Rs("imponibile_scontato") + Rs("iva_imponibile_scontato")

                totale_imponibile_scontato = totale_imponibile_scontato + Rs("imponibile_scontato") 'sostituito dal seguente per effetto del nuovo calcolo - salvo 18.01.2023

                totale_iva = totale_iva + Rs("iva_imponibile_scontato")

                totale = valore_tariffa

            End If
            '-----------------------------------------------------------------------------------------------------------------------------------

            Dbc.Close()
            Dbc.Open()

            If valore_tariffa <> "" Then
                '2 - ELEMENTI INCLUSI - IN QUESTO MOMENTO GLI ELEMENTI INCLUSI HANNO COSTO 0 ---------------------------------------------------
                '-------------------------------------------------------------------------------------------------------------------------------

                '3 - ELEMENTI NON INCLUSI MA OBBLIGATORI (NON IN PERCENTUALE)-------------------------------------------------------------------
                Cmd = New Data.SqlClient.SqlCommand("SELECT  ISNULL(SUM(imponibile_scontato + iva_imponibile_scontato), 0) AS valore, ISNULL(SUM(imponibile_scontato), 0) AS imponibile_scontato, ISNULL(SUM(imponibile), 0) AS imponibile, ISNULL(SUM(iva_imponibile_scontato), 0) AS iva_imponibile_scontato, ISNULL(SUM(iva_imponibile), 0) AS iva_imponibile FROM " & tabella & " WITH(NOLOCK) WHERE id_a_carico_di<>'5' AND obbligatorio='1' And id_metodo_stampa<>'3' And id_metodo_stampa<>'4'  AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL", Dbc)
                Rs = Cmd.ExecuteReader
                If Rs.Read() Then

                    totale_sconto = totale_sconto + (Rs("imponibile") + Rs("iva_imponibile")) - (Rs("imponibile_scontato") + Rs("iva_imponibile_scontato"))


                    totale = totale + Rs("valore")

                    totale_imponibile_scontato = totale_imponibile_scontato + Rs("imponibile_scontato")
                    totale_iva = totale_iva + Rs("iva_imponibile_scontato")
                End If

                Dbc.Close()
                Dbc.Open()

                '-------------------------------------------------------------------------------------------------------------------------------

                '4 - ELEMENTI NON INCLUSI MA OBBLIGATORI (IN PERCENTUALE DIPENDENTI SOLO DAL VALORE TARIFFA)------------------------------------
                '-------------------------------------------------------------------------------------------------------------------------------

                '5 - ELEMENTO ONERE (SE CALCOLATO IN PERCENTUALE - RISPETTO AD ALCUNI ELEMENTI) - SI APPLICA SEMPRE ALL'IMPONIBILE ED E' SEMPRE
                'E COMUNQUE IVA ESCLUSA
                'NEL CASO DI TARIFFA BROKER: SE L'ONERE NON E' INCLUSO ALLORA SI EFFETTUANO DUE CALCOLI; PER IL VALORE TARIFFA L'ONERE DEVE ESSERE AGGIUTO AL COSTO DEL TEMPO KM
                '(IN QUANTO DEVE ESSERE A CARICO DEL TO). 
                Cmd = New Data.SqlClient.SqlCommand("SELECT id_onere FROM stazioni WITH(NOLOCK) WHERE id='" & stazione_pick_up & "'", Dbc)
                Dim id_onere As String = Cmd.ExecuteScalar

                Cmd = New Data.SqlClient.SqlCommand("SELECT valore_percentuale, aliquota_iva ,iva_inclusa FROM " & tabella & " WITH(NOLOCK) WHERE id_a_carico_di<>'" & Costanti.id_accessorio_incluso & "' AND obbligatorio='1' And id_metodo_stampa<>'" & Costanti.id_stampa_informativa_con_valore & "' And id_metodo_stampa<>'" & Costanti.id_stampa_informativa_senza_valore & "'  AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_percentuale IS NULL AND id_elemento='" & id_onere & "'", Dbc)
                Rs = Cmd.ExecuteReader
                If Rs.Read() Then
                    'SE E' STATO TROVATO UN ELEMENTO PERCENTUALE RECUPERO IL VALORE E CALCOLO IL VALORE PERCENTUALE IVATO
                    valore_percentuale = Rs("valore_percentuale")
                    aliquota_percentuale = Rs("aliquota_iva")

                    'CONTROLLO SE L'ELEMENTO PERCENTUALE E' STATO SPECIFICATO NELLA TABELLA condizioni_elementi_percentuale. SE NON E' STATO SPECIFICATO
                    'SI APPLICA AL TOTALE - ALTRIMENTI SI APPLICA AGLI ELEMENTI IVI SPECIFICIATI

                    Dbc.Close()
                    Dbc.Open()

                    'NEL CASO DI BROKER NON CONSIDERO IL VALORE TARIFFA TRA GLI ELEMENTI PER CUI CALCOLARE L'ONERE - QUESTO VERRA' AUTOMATICAMENTE AGGIUNTO AL VALORE TARIFFA
                    Dim condizione_onere_broker As String = ""
                    If is_broker Then
                        condizione_onere_broker = " AND condizioni_elementi_percentuale.id_elemento2<>'" & Costanti.ID_tempo_km & "'"
                    End If

                    Cmd = New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM condizioni_elementi_percentuale WITH(NOLOCK) WHERE id_elemento1='" & id_onere & "'", Dbc)
                    Dim test As String = Cmd.ExecuteScalar & ""

                    If test = "" Then
                        'IN QUESTO CASO L'ELEMENTO NON E' STATO SPECIFICATO - APPLICO L'ELEMENTO PERCENTUALE ALL'IMPONIBILE DEL TOTALE
                        imponibile_percentuale = totale_imponibile_scontato * valore_percentuale / 100
                        iva_percentuale = imponibile_percentuale * aliquota_percentuale / 100

                        'salvaRigaCalcolo(id_da_salvare, num_calcolo, id_gruppo,  "NULL", "TOTALE PERC", valore_percentuale, "NULL", "NULL", "True", "True", "False", "2", "2", "True", 5)

                        'SALVO PER OGNI ELEMENTO IL TOTALE DELL'ONERE
                        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile_onere= ISNULL(" & tabella & ".imponibile_scontato,0)*" & Replace(valore_percentuale / 100, ",", ".") & ", iva_onere=ISNULL(" & tabella & ".imponibile_scontato,0)*" & Replace((valore_percentuale / 100) * (aliquota_percentuale / 100), ",", ".") & " WHERE " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "'", Dbc)
                        Cmd.ExecuteNonQuery()
                    Else
                        'IN QUESTO CASO L'ELEMENTO PERCENTUALE DIPENDE DA ALCUNI DEGLI ELEMENTI SPECIFICATI - ESEGUO IL CALCOLO TENENDONE CONTO
                        'SELEZIONO IL TOTALE DEL COSTO (IMPONIBILE ED IVA) SOLO PER GLI ELEMENTI A CUI E' APPLICABILE LA PERCENTUALE
                        'NON CONSIDERO (IN FASE DI USCITA) GLI ELEMENTI INFORMATIVI. INOLTRE IN QUESTA FASE NON CONSIDERO GLI ELEMENTI A SCELTA
                        '(I NON OBBLIGATORI)
                        Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(SUM(" & tabella & ".imponibile_scontato),0) FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi_percentuale WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi_percentuale.id_elemento2 WHERE " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "' AND " & tabella & ".id_metodo_stampa<>'" & Costanti.id_stampa_informativa_con_valore & "' AND " & tabella & ".id_metodo_stampa<>'" & Costanti.id_stampa_informativa_senza_valore & "' AND obbligatorio='1' AND condizioni_elementi_percentuale.id_elemento1='" & id_onere & "' " & condizione_onere_broker, Dbc)

                        imponibile_percentuale = Cmd.ExecuteScalar * valore_percentuale / 100
                        iva_percentuale = imponibile_percentuale * aliquota_percentuale / 100

                        'SALVO PER OGNI ELEMENTO PER CUI SI CALCOLA L'ONERE IL TOTALE DELL'ONERE
                        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile_onere= ISNULL(" & tabella & ".imponibile_scontato,0)*" & Replace(valore_percentuale / 100, ",", ".") & ", iva_onere=ISNULL(" & tabella & ".imponibile_scontato,0)*" & Replace((valore_percentuale / 100) * (aliquota_percentuale / 100), ",", ".") & " WHERE " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "' AND id_elemento IN (SELECT id_elemento2 FROM condizioni_elementi_percentuale WITH(NOLOCK) WHERE id_elemento1='" & id_onere & "' " & condizione_onere_broker & ")", Dbc)
                        Cmd.ExecuteNonQuery()

                        If is_broker Then
                            'SE LA TARIFFA E' BROKER AGGIUNGO IL VALORE DELL'ONERE AL COSTO DELL'ACCESSORIO E SALVO IN UN CAMPO PARTICOLARE IL VALORE DELL'ONERE E DELLA SUA IVA
                            'PER EVENTUALI STAMPE E STATISTICHE
                            Dim imponibile_percentuale_valore_tariffa As Double
                            Dim iva_percentuale_valore_tariffa As Double
                            Dim imponibile_percentuale_scontato_valore_tariffa As Double
                            Dim iva_percentuale_scontato_valore_tariffa As Double
                            Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(SUM(" & tabella & ".imponibile_scontato),0) FROM " & tabella & " WITH(NOLOCK)  WHERE " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "' AND " & tabella & ".id_metodo_stampa<>'" & Costanti.id_stampa_informativa_con_valore & "' AND " & tabella & ".id_metodo_stampa<>'" & Costanti.id_stampa_informativa_senza_valore & "' AND obbligatorio='1' AND " & tabella & ".id_elemento='" & Costanti.ID_tempo_km & "'", Dbc)
                            imponibile_percentuale_scontato_valore_tariffa = Cmd.ExecuteScalar * valore_percentuale / 100
                            iva_percentuale_scontato_valore_tariffa = imponibile_percentuale_scontato_valore_tariffa * aliquota_percentuale / 100
                            Cmd = New Data.SqlClient.SqlCommand("SELECT ISNULL(SUM(" & tabella & ".imponibile),0) FROM " & tabella & " WITH(NOLOCK)  WHERE " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "' AND " & tabella & ".id_metodo_stampa<>'" & Costanti.id_stampa_informativa_con_valore & "' AND " & tabella & ".id_metodo_stampa<>'" & Costanti.id_stampa_informativa_senza_valore & "' AND obbligatorio='1' AND " & tabella & ".id_elemento='" & Costanti.ID_tempo_km & "'", Dbc)
                            imponibile_percentuale_valore_tariffa = Cmd.ExecuteScalar * valore_percentuale / 100
                            iva_percentuale_valore_tariffa = imponibile_percentuale_valore_tariffa * aliquota_percentuale / 100
                            'SALVO PER L'ACCESSORIO TEMPO KM L'IMPONIBILE ONERE
                            Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=imponibile+'" & Replace(imponibile_percentuale_valore_tariffa, ",", ".") & "', iva_imponibile=iva_imponibile+'" & Replace(iva_percentuale_valore_tariffa, ",", ".") & "', imponibile_scontato=imponibile_scontato+'" & Replace(imponibile_percentuale_scontato_valore_tariffa, ",", ".") & "', iva_imponibile_scontato=iva_imponibile_scontato+'" & Replace(iva_percentuale_scontato_valore_tariffa, ",", ".") & "', imponibile_onere_broker_incluso='" & Replace(imponibile_percentuale_scontato_valore_tariffa, ",", ".") & "' ,iva_onere_broker_incluso='" & Replace(iva_percentuale_scontato_valore_tariffa, ",", ".") & "' WHERE " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "' AND " & tabella & ".id_elemento='" & Costanti.ID_tempo_km & "'", Dbc)
                            Cmd.ExecuteNonQuery()

                            totale = totale + imponibile_percentuale_scontato_valore_tariffa + iva_percentuale_scontato_valore_tariffa

                            totale_imponibile_scontato = totale_imponibile_scontato + imponibile_percentuale_scontato_valore_tariffa
                            totale_iva = totale_iva + iva_percentuale_scontato_valore_tariffa
                        End If
                    End If

                    'IN OGNI CASO AGGIORNO LA RIGA PERCENTUALE COL VALORE DI IMPONIBILE E DI IVA

                    totale_imponibile_scontato = totale_imponibile_scontato + imponibile_percentuale
                    totale_iva = totale_iva + iva_percentuale

                    totale = totale + imponibile_percentuale + iva_percentuale

                    Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile='" & Replace(imponibile_percentuale, ",", ".") & "', imponibile_scontato='" & Replace(imponibile_percentuale, ",", ".") & "',iva_imponibile='" & Replace(iva_percentuale, ",", ".") & "', iva_imponibile_scontato='" & Replace(iva_percentuale, ",", ".") & "' WHERE id_elemento='" & id_onere & "' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "'", Dbc)
                    Cmd.ExecuteNonQuery()
                End If
                '-------------------------------------------------------------------------------------------------------------------------------

                Dbc.Close()
                Dbc.Open()

                '6 - SALVO LA RIGA DI SCONTO (SE DIVERSO DA 0)----------------------------------------------------------------------------------
                totale_sconto = FormatNumber(totale_sconto, 4, , , TriState.False)
                salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, "NULL", "", "SCONTO", totale_sconto, "NULL", "NULL", "NULL", "True", "True", "False", "False", "2", "2", "True", "0", 5, "NULL", "NULL", "", "NULL", "0", "", False, False)
                Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile='" & Replace(totale_sconto, ",", ".") & "', imponibile_scontato='" & Replace(totale_sconto, ",", ".") & "', iva_imponibile_scontato='0',iva_imponibile='0' WHERE ordine_stampa='5' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL", Dbc)
                Cmd.ExecuteNonQuery()
                '-------------------------------------------------------------------------------------------------------------------------------

                '7 - SALVO LA RIGA DI TOTALE----------------------------------------------------------------------------------------------------
                salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, "NULL", "", Costanti.testo_elemento_totale, totale, "NULL", "NULL", "NULL", "True", "True", "False", "False", "2", "2", "True", "0", 6, "NULL", "NULL", "", "NULL", "0", "", False, False)
                'AGGIUNGO IL COSTO IMPONIBILE E IVA PER LA RIGA TOTALE APPENA CREATA
                Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile='" & Replace(totale_imponibile_scontato - imponibile_percentuale, ",", ".") & "',  imponibile_scontato='" & Replace(totale_imponibile_scontato - imponibile_percentuale, ",", ".") & "',iva_imponibile='" & Replace(totale_iva - iva_percentuale, ",", ".") & "', iva_imponibile_scontato='" & Replace(totale_iva - iva_percentuale, ",", ".") & "', imponibile_onere='" & Replace(imponibile_percentuale, ",", ".") & "', iva_onere='" & Replace(iva_percentuale, ",", ".") & "' WHERE ordine_stampa='6' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL", Dbc)
                Cmd.ExecuteNonQuery()
                '-------------------------------------------------------------------------------------------------------------------------------
            Else
                'FARE QUALCOSA NEL CASO IN CUI IL TEMPO KM NON E' STATO TROVATO 
            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error  calcolo_iva_e_totale : " & ex.Message & "<br/>" & "<br/>")
        End Try


        'Tony 12-07-2023
        'salvo aggiunto 18.07.2023
        'se richiamato da apri preventivo non deve aggiungere il costo dell'elemento 320
        Dim ncalcolo As String = HttpContext.Current.Session("num_calcolo_preventivo")

        Dim sql320 As String

        'If HttpContext.Current.Session("apre_preventivo") = "apre_preventivo" Then
        '    HttpContext.Current.Session("apre_preventivo") = ""  'reset session apre  preventivo salvo 15.06.2023 modificato il 18.07.2023
        'Else
        'End If



        If id_preventivo <> "" Or id_prenotazione <> "" Then  'Aggiornato salvo 23.07.2023 - inserito controllo su prenotazione

            Try

                'SALVO 23.07.2023 aggiunto il gruppo nel caso di preventivi multipli
                'altrimenti aggiorna sempre tutte le righe nel caso di preventivi con gruppi multipli
                Dim Sql As String = "SELECT * FROM " & tabella & " WITH(NOLOCK) WHERE id_documento=" & id_da_salvare & " and id_elemento='320' and id_gruppo=" & id_gruppo & " "

                Dim Dbc320 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc320.Open()
                'Sql = ""
                Dim Cmd320 As New Data.SqlClient.SqlCommand(Sql, Dbc320)
                'HttpContext.Current.Response.Write(Cmd320.CommandText & "<br>")
                'HttpContext.Current.Response.End()

                Dim Rs320 As Data.SqlClient.SqlDataReader
                Rs320 = Cmd320.ExecuteReader()


                If Rs320.HasRows Then
                    Do While Rs320.Read

                        'salvo 18.07.2023 se in apertura preventivo deve aggiornare il valore dal numero di calcolo da 3 in poi
                        If HttpContext.Current.Session("apre_preventivo") = "apre_preventivo" Then
                            If Rs320("num_calcolo") > 2 Then
                                'modificata salvo 23.07.2023
                                Sql = "UPDATE " & tabella & "  SET valore_costo= valore_costo + " & Replace(Rs320("valore_costo"), ",", ".") & ", imponibile=imponibile +" & Replace(Rs320("valore_costo") / 1.22, ",", ".") & ", imponibile_scontato=imponibile_scontato +" & Replace(Rs320("valore_costo") / 1.22, ",", ".") & ", iva_imponibile = (valore_costo + " & Rs320("valore_costo") & ")- (imponibile_scontato + " & Replace(Rs320("valore_costo"), ",", ".") & "/ 1.22), iva_imponibile_scontato = (valore_costo + " & Replace(Rs320("valore_costo"), ",", ".") & ")- (imponibile_scontato + " & Replace(Rs320("valore_costo"), ",", ".") & "/ 1.22) WHERE ordine_stampa='6' AND id_documento='" & id_da_salvare & "'  AND NOT valore_costo IS NULL and num_calcolo='" & Rs320("num_calcolo") & "' AND id_gruppo=" & id_gruppo
                                Cmd320 = New Data.SqlClient.SqlCommand(Sql, Dbc320)
                                Dim rsql As Integer = Cmd320.ExecuteNonQuery()

                            End If

                        Else 'se non è in apertura preventivo o è relativo a prenotazione ma in fase di creazione allora aggiorna la riga con numero di calcolo 2

                            If Rs320("num_calcolo") = 2 Or HttpContext.Current.Session("cambiatariffanp") = "ok" Then
                                'modificata salvo 23.07.2023
                                Sql = "UPDATE " & tabella & "  SET valore_costo= valore_costo + " & Replace(Rs320("valore_costo"), ",", ".") & ", imponibile=imponibile +" & Replace(Rs320("valore_costo") / 1.22, ",", ".") & ", imponibile_scontato=imponibile_scontato +" & Replace(Rs320("valore_costo") / 1.22, ",", ".") & ", iva_imponibile = (valore_costo + " & Rs320("valore_costo") & ")- (imponibile_scontato + " & Replace(Rs320("valore_costo"), ",", ".") & "/ 1.22), iva_imponibile_scontato = (valore_costo + " & Replace(Rs320("valore_costo"), ",", ".") & ")- (imponibile_scontato + " & Replace(Rs320("valore_costo"), ",", ".") & "/ 1.22) WHERE ordine_stampa='6' AND id_documento='" & id_da_salvare & "'  AND NOT valore_costo IS NULL and num_calcolo='" & Rs320("num_calcolo") & "' AND id_gruppo=" & id_gruppo
                                Cmd320 = New Data.SqlClient.SqlCommand(Sql, Dbc320)
                                Dim rsql As Integer = Cmd320.ExecuteNonQuery()

                                '# aggiunto salvo 01.08.2023
                                If HttpContext.Current.Session("cambiatariffanp") = "ok" Then
                                    HttpContext.Current.Session("cambiatariffanp") = ""
                                End If
                                '@ end salvo 01.08.2023

                            End If

                        End If

                        'HttpContext.Current.Response.Write(Sql)
                        'HttpContext.Current.Response.End()

                    Loop

                Else

                End If

                Rs320.Close()
                Dbc320.Close()
                Rs320 = Nothing
                Dbc320 = Nothing


            Catch ex As Exception
                HttpContext.Current.Response.Write(ex.Message & " Carica Dati --- Errore contattare amministratore del sistema.")
            End Try
        End If

    End Sub

    Public Shared Sub omaggio_accessorio(ByVal omaggia As Boolean, ByVal aggiungi_costo As Boolean, ByVal rimuovi_costo_se_extra As Boolean, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazioni As String, ByVal id_contratto As String, ByVal numCalcolo As String, ByVal id_gruppoScelto As String, ByVal id_elemento As String, ByVal num_elemento_X_aggiunta_omaggio As String, ByVal tipo_commissione As String, ByVal percentuale_commissione As String, ByVal id_fonte_commissionabile As String, ByVal tipologia_franchigia As String, ByVal sottotipologia_franchigia As String)
        'OMAGGIA: True SE L'ACCESSORIO E' DA OMAGGIARE
        '         False PER RIMUOVERE L'ACCESSORIO COME OMAGGIO.
        'AGGIUNGI_COSTO : SE omaggia E' FALSE E aggiungi_costo E' TRUE DOPO AVER RIMOSSO L'OMAGGIO L'ELEMENTO VIENE AGGIUNTO COME DA PAGARE
        'RIMUOVI_COSTO : SE omaggià e' FALSE E rimuovi_costo E' TRUE, SE L'ELEMENTO E' EXTRA (valorizza='false' - utilizzabile
        'anche nel caso in cui deve essere rimosso un costo obbligatorio - basta selezionare valorizza  false in condizioni_elementi)
        ', VIENE RIMOSSO

        Dim cond_num_elemento As String = ""  'NUM ELEMENTO VIENE CONSIDERATO SOLO PER L'AGGIUNTA DELL'OMAGGIO DELL'ACCESSORIO

        If num_elemento_X_aggiunta_omaggio <> "" Then
            cond_num_elemento = " AND num_elemento='" & num_elemento_X_aggiunta_omaggio & "' "
        End If

        Dim id_da_salvare As String
        Dim tabella As String
        If id_preventivo <> "" Then
            id_da_salvare = id_preventivo
            tabella = "preventivi_costi"
        ElseIf id_ribaltamento <> "" Then
            id_da_salvare = id_ribaltamento
            tabella = "ribaltamento_costi"
        ElseIf id_prenotazioni <> "" Then
            id_da_salvare = id_prenotazioni
            tabella = "prenotazioni_costi"
        ElseIf id_contratto <> "" Then
            id_da_salvare = id_contratto
            tabella = "contratti_costi"
        End If
        If omaggia Then
            'AGGIUNGERE OMAGGIO
            'PRIMO CASO: L'ACCESSORIO NON ERA STATO SELEZIONATO. IN QUESTO CASO LO SI DEVE SOLAMENTE SELEZIONARE E SALVARE COME OMAGGIATO
            'SECONDO CASO: L'ACCESSORIO ERA STATO SELEZIONATO ED IN UN SECONDO MOMENTO SI SCEGLIE DI OMAGGIARLO. IN QUESTO CASO PRIMA LO SI 
            'RIMUOVE DAGLI ACCESSORI SELEZIONATI POI LO SI OMAGGIA E SI SEGNA COME SELEZIONATO
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT selezionato FROM " & tabella & " WITH(NOLOCK) WHERE id_elemento='" & id_elemento & "'" & cond_num_elemento & " AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & numCalcolo & "' AND id_gruppo='" & id_gruppoScelto & "'", Dbc)
            Dim selezionato As Boolean = Cmd.ExecuteScalar

            If Not selezionato Then
                Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET selezionato='1', omaggiato='1' WHERE id_elemento='" & id_elemento & "'" & cond_num_elemento & " AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & numCalcolo & "' AND id_gruppo='" & id_gruppoScelto & "'", Dbc)
                Cmd.ExecuteNonQuery()
                If tipologia_franchigia = "ASSICURAZIONE" Then
                    'AGGIORNO LE FRANCHIGIE
                    normalizza_assicurazioni(id_preventivo, id_ribaltamento, id_prenotazioni, id_contratto, numCalcolo, id_gruppoScelto, sottotipologia_franchigia, tipo_commissione, percentuale_commissione, id_fonte_commissionabile)

                    aggiorna_franchigie(id_preventivo, id_ribaltamento, id_prenotazioni, id_contratto, numCalcolo, id_gruppoScelto, sottotipologia_franchigia)
                End If
            Else
                rimuovi_costo_accessorio(id_preventivo, id_ribaltamento, id_prenotazioni, id_contratto, numCalcolo, id_gruppoScelto, id_elemento, num_elemento_X_aggiunta_omaggio, "OMAGGIO", tipo_commissione)
                Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET selezionato='1', omaggiato='1' WHERE id_elemento='" & id_elemento & "'" & cond_num_elemento & " AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & numCalcolo & "' AND id_gruppo='" & id_gruppoScelto & "'", Dbc)
                Cmd.ExecuteNonQuery()
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Else
            'RIMUOVERE OMAGGIO - VIENE SEGNATO ANCHE COME NON SELEZIONATO PERCHE', NEL CASO IN CUI SI DEVE AGGIUNGERE IL COSTO, DEVE ESSERE
            'NUOVAMENTE RICHIAMATO aggiungi_costo_accessorio - E' UN ACCESSORIO EXTRA VIENE ANCHE RIMOSSO
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET selezionato='0', omaggiato='0' WHERE id_elemento='" & id_elemento & "'" & cond_num_elemento & " AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & numCalcolo & "' AND id_gruppo='" & id_gruppoScelto & "'", Dbc)
            Cmd.ExecuteNonQuery()
            If tipologia_franchigia = "ASSICURAZIONE" Then
                'RIMUOVO LE FRANCHIGIE PARZIALI CHE POI VERRANNO RIAGGIUNTE DENTRO AGGIUNGI COSTO ACCESSORIO
                aggiorna_franchigie(id_preventivo, id_ribaltamento, id_prenotazioni, id_contratto, numCalcolo, id_gruppoScelto, sottotipologia_franchigia)
            End If

            If aggiungi_costo Then
                aggiungi_costo_accessorio(id_preventivo, id_ribaltamento, id_prenotazioni, id_contratto, numCalcolo, id_gruppoScelto, id_elemento, "", "", "", False, tipo_commissione, percentuale_commissione, id_fonte_commissionabile)
            End If

            If rimuovi_costo_se_extra Then
                Cmd = New Data.SqlClient.SqlCommand("SELECT valorizza FROM condizioni_elementi WITH(NOLOCK) WHERE id='" & id_elemento & "'", Dbc)
                Dim valorizza As Boolean = Cmd.ExecuteScalar()
                If Not valorizza Then
                    Cmd = New Data.SqlClient.SqlCommand("DELETE FROM " & tabella & " WHERE id_elemento='" & id_elemento & "'" & cond_num_elemento & " AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & numCalcolo & "' AND id_gruppo='" & id_gruppoScelto & "'", Dbc)
                    Cmd.ExecuteNonQuery()
                End If
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End If
    End Sub

    Protected Shared Sub aggiorna_costo_accessorio_giornaliero(ByVal id_elemento As String, ByVal giorni_da_calcolare As String, ByVal sconto As String, ByVal data_aggiunta As String, ByVal id_contratto As String, ByVal num_calcolo As String)
        Dim sqla As String = ""
        Dim error_n As Integer = 0
        'AGGIORNA IL COSTO DI UN ACCESSORIO GIORNALIERO ACQUISTATO A NOLO IN CORSO TENENDO IN CONSIDERAZIONE I GIORNI DA FAR PAGARE
        'AGGIORNANDO IL COSTO MEMORIZZO ANCHE LA DATA DI SALVATAGGIO DEL COSTO (SERVE NEL CASO IN CUI VENGANO ESTESI I GIORNI DI NOLEGGIO
        'PER CALCOLARE DA QUALE GIORNO DEVE ESSERE AGGIORNATO IL COSTO DELL'ACCESSORIO)
        Try
            Dim data_agg As String = funzioni_comuni.getDataDb_con_orario(data_aggiunta)
            error_n = 1
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            sqla = "UPDATE contratti_costi SET valore_costo=(valore_costo/qta)*" & giorni_da_calcolare & ", qta='" & giorni_da_calcolare & "', data_aggiunta_nolo_in_corso=convert(datetime,'" & data_agg & "',102) WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND id_unita_misura='" & Costanti.id_unita_misura_giorni & "' AND id_elemento='" & id_elemento & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()

            'E' NECESSARIO AGGIORNARE I VALORI DI IVA E DI IMPONIBILE
            error_n = 2
            '0 - IMPONIBILE E IL VALORE DI IVA IMPONIBILE ---------------------------------------------
            sqla = "UPDATE contratti_costi SET imponibile=(valore_costo*100/(100+aliquota_iva)), iva_imponibile=(valore_costo*aliquota_iva/(100+aliquota_iva)) WHERE iva_inclusa='True' AND id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "'  AND id_elemento='" & id_elemento & "' AND id_unita_misura='" & Costanti.id_unita_misura_giorni & "'"
            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()
            sqla = "UPDATE contratti_costi SET imponibile=valore_costo, iva_imponibile=(valore_costo*aliquota_iva/100) WHERE iva_inclusa='False' AND id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND id_elemento='" & id_elemento & "' AND id_unita_misura='" & Costanti.id_unita_misura_giorni & "'"
            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()
            '-----------------------------------------------------------------------------------------------------------------------------------
            error_n = 3
            '0A - AGGIORNAMENTO DELL'EVENTUALE SCONTO RICHIESTO DALL'OPERATORE APPLICANDOLO ALL'IMPONIBILE DEGLI ELEMENTI SCONTABILI------------
            sqla = "UPDATE contratti_costi SET imponibile_scontato=imponibile-(imponibile*" & sconto & "/100) WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND id_elemento='" & id_elemento & "' AND id_unita_misura='" & Costanti.id_unita_misura_giorni & "' AND scontabile='1'"
            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()
            error_n = 4
            'PER GLI ELEMENTI NON SCONTABILI SETTO L'IMPONIBILE SCONTATO UGUALE ALL'IMPONIBILE
            sqla = "UPDATE contratti_costi SET imponibile_scontato=imponibile, iva_imponibile_scontato=iva_imponibile WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND id_elemento='" & id_elemento & "' AND id_unita_misura='" & Costanti.id_unita_misura_giorni & "' AND scontabile='0'"
            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()
            '-----------------------------------------------------------------------------------------------------------------------------------
            '0B - CALCOLO DELL'IVA SULL'IMPONIBILE SCONTATO ------------------------------------------------------------------------------------
            error_n = 5
            sqla = "UPDATE contratti_costi SET iva_imponibile_scontato=imponibile_scontato*aliquota_iva/100  WHERE (iva_inclusa='True' OR iva_inclusa='False') AND id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND id_elemento='" & id_elemento & "' AND id_unita_misura='" & Costanti.id_unita_misura_giorni & "' AND scontabile='1'"
            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("error aggiorna_costo_accessorio_giornaliero (" & error_n & ")  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try




    End Sub

    Public Shared Sub aggiungi_costo_refuel(ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String, ByVal id_elemento As String, ByVal id_tabella As String)
        'AL COSTO DELL'ELEMENTO POTREBBE ESSERE NECESSARIO AGGIUNGERE IL COSTO DELL'ELEMENTO PERCENTUALE
        'NEL CASO DI REFUEL IL COSTO E' SICURAMENTE NON GIORNALIERO - DEVE ESSERE UTILZZATO L'ID DELLA TABELLA contratti_costi E NON L'ID
        'DELL'ELEMENTO IN QUANTO, NEL CASO DI CRV, L'ELEMENTO PUO' ESSERE PRESENTE PIU' VOLTE
        Dim id_da_salvare As String = id_contratto
        Dim tabella As String = "contratti_costi"

        Dim id_onere As String = "0"
        Dim imponibile_percentuale As Double = 0
        Dim iva_percentuale As Double = 0

        Dim aliquota_iva As Double

        Dim imponibile_elemento As Double = 0
        Dim iva_elemento As Double = 0

        Dim aumento_imponibile_percentuale As Double = 0
        Dim aumento_iva_percentuale As Double = 0
        Dim sconto As Double = 0

        Dim tipologia_franchigia As String

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Rs As Data.SqlClient.SqlDataReader

        '1 - RECUPERO IL COSTO DELL'ELEMENTO SCELTO E LA SUA TIPOLOGIA
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 imponibile,iva_imponibile, imponibile_scontato,  iva_imponibile_scontato, condizioni_elementi.tipologia_franchigia FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE " & tabella & ".id='" & id_tabella & "'", Dbc)
        Rs = Cmd.ExecuteReader

        If Rs.Read() Then
            tipologia_franchigia = Rs("tipologia_franchigia") & ""

            imponibile_elemento = Rs("imponibile_scontato")
            iva_elemento = Rs("iva_imponibile_scontato")

            sconto = Rs("imponibile") + Rs("iva_imponibile") - Rs("imponibile_scontato") - Rs("iva_imponibile_scontato")
        End If

        Dbc.Close()
        Dbc.Open()
        '2 - CONTROLLO SE E' STATO SPECIFICATO L'ELEMENTO PERCENTUALE DI TIPO ONERE)

        Cmd = New Data.SqlClient.SqlCommand("SELECT TOP 1 " & tabella & ".id_elemento, " & tabella & ".valore_percentuale, " & tabella & ".aliquota_iva, " & tabella & ".iva_inclusa FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE condizioni_elementi.tipologia='ONERE' AND " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "' AND NOT " & tabella & ".valore_percentuale IS NULL", Dbc)
        Rs = Cmd.ExecuteReader
        Rs.Read()

        If Rs.HasRows Then
            id_onere = Rs("id_elemento")
            Dim valore_percentuale As Double = Rs("valore_percentuale")
            aliquota_iva = Rs("aliquota_iva")

            Dbc.Close()
            Dbc.Open()

            'SE IL VALORE PERCENTUALE E' STATO SPECIFICATO CONTROLLO SE IL COSTO DELL'ELEMENTO SCELTO DEVE ESSERE MAGGIORATO CON L'ELEMENTO
            'PERCENTUALE - LO SI DEVE FARE SE L'ELEMENTO PERCENTUALE NON E' STATO SPECIFICATO DENTRO condizioni_elemento_percentuale OPPURE
            'SE E' STATO SPECIFICATO E SE I DUE ELEMETI SONO LEGATI IN QUESTA TABELLA

            Cmd = New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM condizioni_elementi_percentuale WITH(NOLOCK) WHERE id_elemento1='" & id_onere & "'", Dbc)
            Dim test As String = Cmd.ExecuteScalar & ""
            Dim test2 As String = ""

            If test <> "" Then
                'SE L'ELEMENTO PERCENTUALE E' STATO SPECIFICATO NELLA TABELLA condizioni_elementi_percentuale DEVO CONTROLLARE SE SULLO
                'ELEMENTO SELEZIONATO SI DEVE CALCOLARE LA PERCENTUALE
                Cmd = New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM condizioni_elementi_percentuale WITH(NOLOCK) WHERE id_elemento1='" & id_onere & "' AND id_elemento2='" & id_elemento & "'", Dbc)
                test2 = Cmd.ExecuteScalar & ""
            End If

            If test = "" Or test2 <> "" Then
                'SI ENTRA QUI DENTRO SE L'ELEMENTO PERCENTUALE NON E' STATO SPECIFICATO DENTRO condizioni_elementi_percentuale (test="")
                'OPPURE SE E' STATO SPECIFICATO E SULL'ELEMENTO SELEZIONATO SI DEVE CALCOLARE LA PERCENTUALE (test2 <> "")
                'IN ENTRAMBI I CASI DEVO CALCOLARE LA PERCENTUALE DELL'ONERE SULL'IMPONIBILE DELL'ELEMENTO
                aumento_imponibile_percentuale = imponibile_elemento * valore_percentuale / 100
                aumento_iva_percentuale = aumento_imponibile_percentuale * aliquota_iva / 100
            Else
                'IN QUESTO CASO SULL'ELEMENTO NON DEVE ESSERE CALCOLATO L'AUMENTO PERCENTUALE
                aumento_imponibile_percentuale = 0
                aumento_iva_percentuale = 0
            End If

        Else
            'IN QUESTO CASO NON E' STATO SPECIFICATO ALCUN ELEMENTO PERCENTUALE - SI DEVE SEMPLICAMENTE AGGIUNGERE AL TOTALE IL COSTO DELLO
            'ELEMENTO SCELTO
            aumento_imponibile_percentuale = 0
            aumento_iva_percentuale = 0

            Dbc.Close()
            Dbc.Open()
        End If

        'SE E' STATO TROVATO UN AUMENTO DELL'ONERE AGGIORNO LA RIGA DELL'ONERE E LA RIGA DELL'ELEMENTO COL VALORE DELL'ONERE
        Dim update_onere As String = ""
        If id_onere <> "0" Then
            Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=imponibile+" & Replace(aumento_imponibile_percentuale, ",", ".") & ", imponibile_scontato=imponibile_scontato+" & Replace(aumento_imponibile_percentuale, ",", ".") & ",iva_imponibile=iva_imponibile+" & Replace(aumento_iva_percentuale, ",", ".") & ", iva_imponibile_scontato=iva_imponibile_scontato+" & Replace(aumento_iva_percentuale, ",", ".") & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_onere & "'", Dbc)
            Cmd.ExecuteNonQuery()

            update_onere = ", imponibile_onere='" & Replace(aumento_imponibile_percentuale, ",", ".") & "', iva_onere='" & Replace(aumento_iva_percentuale, ",", ".") & "' "
        End If

        'AGGIORNO LA RIGA DEL TOTALE AGGIUNGENDO IL COSTO DELL'ACCESSORIO E DELL'EVENTUALE AUMENTO DELL'ONERE PERCENTUALE
        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=imponibile+" & Replace(imponibile_elemento, ",", ".") & ", imponibile_scontato=imponibile_scontato+" & Replace(imponibile_elemento, ",", ".") & ", iva_imponibile_scontato=iva_imponibile_scontato+" & Replace(iva_elemento, ",", ".") & ",iva_imponibile=iva_imponibile+" & Replace(iva_elemento, ",", ".") & ", imponibile_onere=imponibile_onere+" & Replace(aumento_imponibile_percentuale, ",", ".") & ",iva_onere=iva_onere+" & Replace(aumento_iva_percentuale, ",", ".") & ", valore_costo=valore_costo+" & Replace(imponibile_elemento + aumento_imponibile_percentuale + iva_elemento + aumento_iva_percentuale, ",", ".") & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND ordine_stampa='6'", Dbc)
        Cmd.ExecuteNonQuery()

        'AGGIORNO IL CAMPO 'SELEZIONATO' DELL'ELEMENTO SCELTO E NE APPROFITTO PER SALVARE EVENTUALMENTE IL COSTO DELL'ONERE
        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET selezionato='1' " & update_onere & " WHERE " & tabella & ".id='" & id_tabella & "'", Dbc)
        Cmd.ExecuteNonQuery()

        'AGGIORNO LO SCONTO 
        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET valore_costo=valore_costo+" & Replace(FormatNumber(sconto, 4, , , TriState.False), ",", ".") & ",imponibile=imponibile+" & Replace(FormatNumber(sconto, 4, , , TriState.False), ",", ".") & ", imponibile_scontato=imponibile_scontato+" & Replace(FormatNumber(sconto, 4, , , TriState.False), ",", ".") & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND ordine_stampa='5'", Dbc)
        Cmd.ExecuteNonQuery()
        '-----------------------------------------------------------------------------------------------------------------------------------

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Public Shared Sub aggiungi_costo_accessorio(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazioni As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String, ByVal id_elemento As String, ByVal giorni_da_calcolare_x_nolo_in_corso As String, ByVal sconto_x_nolo_in_corso As String, ByVal data_aggiunta_x_nolo_in_corso As String, ByVal imposta_prepagato As Boolean, ByVal tipo_commissione As String, ByVal percentuale_commissione As String, ByVal id_fonte_commissionabile As String)
        'AL COSTO DELL'ELEMENTO POTREBBE ESSERE NECESSARIO AGGIUNGERE IL COSTO DELL'ELEMENTO PERCENTUALE
        'SI UTILIZZA PER GLI ELEMENTI GIA' VALORIZZATI IN FASE DI RICERCA
        Dim id_da_salvare As String
        Dim tabella As String
        If id_preventivo <> "" Then
            id_da_salvare = id_preventivo
            tabella = "preventivi_costi"
        ElseIf id_ribaltamento <> "" Then
            id_da_salvare = id_ribaltamento
            tabella = "ribaltamento_costi"
        ElseIf id_prenotazioni <> "" Then
            id_da_salvare = id_prenotazioni
            tabella = "prenotazioni_costi"
        ElseIf id_contratto <> "" Then
            id_da_salvare = id_contratto
            tabella = "contratti_costi"
        End If

        'SE VIENE PASSATO IL PARAMETRO imposta_prepagato A TRUE ALLORA L'ELEMENTO DEVE ESSERE IMPOSTATO COME TALE (UTILIZZATO LA PRIMA VOLTA CHE SI IMPOSTA UNA PRENOTAZIONE COME 
        'PREPAGATA)
        Dim condizione_prepagato As String = ""
        If imposta_prepagato Then
            condizione_prepagato = ", prepagato=1, imponibile_scontato_prepagato=imponibile_scontato, iva_imponibile_scontato_prepagato=iva_imponibile_scontato, " &
                "imponibile_onere_prepagato=imponibile_onere, iva_onere_prepagato=iva_onere "
        End If

        If giorni_da_calcolare_x_nolo_in_corso <> "" Then
            'NEL CASO IN CUI L'ACCESSORIO E' DI TIPO PAGAMENTO AL GIORNO, PRIMA DI AGGIUNGERE IL COSTO DEVO AGGIORNARE IL COSTO: 
            'IL CLIENTE IN QUESTO CASO DOVRA' PAGARE SOLAMENTE IL COSTO PER I GIORNI RESTANTI DI NOLEGGIO. IN QUESTO CASO I PARAMETRI
            'GIORNO CALCOLATI DEVONO ESSERE I GIORNI CON CUI E' STATO CALCOLATO L'ACCESSORIO, MENTRE CON GIORNI DA CALCOLARE
            aggiorna_costo_accessorio_giornaliero(id_elemento, giorni_da_calcolare_x_nolo_in_corso, sconto_x_nolo_in_corso, data_aggiunta_x_nolo_in_corso, id_contratto, num_calcolo)
        End If
        '-------------------------------------------------------------------------------------------------------------------


        Dim id_onere As String = "0"
        Dim imponibile_percentuale As Double = 0
        Dim iva_percentuale As Double = 0

        Dim aliquota_iva As Double

        Dim imponibile_elemento As Double = 0
        Dim iva_elemento As Double = 0

        Dim imponibile_onere As Double = 0
        Dim iva_onere As Double = 0

        Dim aumento_imponibile_percentuale As Double = 0
        Dim aumento_iva_percentuale As Double = 0
        Dim sconto As Double = 0

        Dim tipologia_franchigia As String
        Dim sottotipologia_franchigia As String

        Dim Rs As Data.SqlClient.SqlDataReader

        '1 - RECUPERO IL COSTO DELL'ELEMENTO SCELTO E LA SUA TIPOLOGIA
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sql1 As String = "SELECT TOP 1 imponibile,iva_imponibile, imponibile_scontato, iva_imponibile_scontato, condizioni_elementi.tipologia_franchigia, condizioni_elementi.sottotipologia_franchigia, ISNULL(imponibile_onere,0) As imponibile_onere, ISNULL(iva_onere,0) As iva_onere FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_elemento & "' AND selezionato='0'"

        Dim Cmd As New Data.SqlClient.SqlCommand(sql1, Dbc)
        Rs = Cmd.ExecuteReader

        If Rs.Read() Then
            tipologia_franchigia = Rs("tipologia_franchigia") & ""
            sottotipologia_franchigia = Rs("sottotipologia_franchigia") & ""

            imponibile_elemento = Rs("imponibile_scontato")
            iva_elemento = Rs("iva_imponibile_scontato")

            imponibile_onere = Rs("imponibile_onere")
            iva_onere = Rs("iva_onere")

            sconto = Rs("imponibile") + Rs("iva_imponibile") - Rs("imponibile_scontato") - Rs("iva_imponibile_scontato")


            Dbc.Close()
            Dbc.Open()

            'TARIFFA COMMISSIONABILE: SE LA COMMISSIONE E' DA RICONOSCERE DOPO EFFETTUO IL CALCOLO DELLA COMMISSIONE SOLO SE L'ELEMENTO E' COMMISSIONABILE 
            Dim elemento_commissionabile As Boolean = False
            If tipo_commissione = "1" Then
                Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM fonti_commissionabili_x_elementi WHERE id_fonte_commissionabile='" & id_fonte_commissionabile & "' AND id_elemento_condizione='" & id_elemento & "'", Dbc)
                Dim test As String = Cmd.ExecuteScalar & ""
                If test <> "" Then
                    elemento_commissionabile = True
                End If
            End If

            '2 - CONTROLLO SE E' STATO SPECIFICATO L'ELEMENTO PERCENTUALE DI TIPO ONERE - OTTENGO IL SUO ID - SOLAMENTE SE L'IMPONIBILE DELL'ONERE
            'E' VALORIZZATO ALTRIMENTI VUOL DIRE CHE SULL'ELEMENTO NON DEVO CALCOLARE L'ONERE
            If imponibile_onere <> 0 Then
                Cmd = New Data.SqlClient.SqlCommand("SELECT TOP 1 " & tabella & ".id_elemento FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE condizioni_elementi.tipologia='ONERE' AND " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "' AND NOT " & tabella & ".valore_percentuale IS NULL", Dbc)
                Rs = Cmd.ExecuteReader
                Rs.Read()

                'NUOVA VERSIONE CON ELEMENTO PERCENTUALE PRECALCOLATO --------------------------------------------------------------------------
                If Rs.HasRows Then
                    id_onere = Rs("id_elemento")

                    'NELLA TABELLA DEI COSTI L'IMPONIBILE E L'IVA DELL'ONERE E' STATO PRECALCOLATO - I CAMPI SONO VALORIZZATI SOLAMENTE SOLAMENTE SE
                    'SULL'ELEMENTO SI DEVE PAGARE L'ONERE ALTRIMENTI I VALORI SONO 0

                    aumento_imponibile_percentuale = imponibile_onere
                    aumento_iva_percentuale = iva_onere
                Else
                    'IN QUESTO CASO NON E' STATO SPECIFICATO ALCUN ELEMENTO PERCENTUALE - SI DEVE SEMPLICAMENTE AGGIUNGERE AL TOTALE IL COSTO DELLO
                    'ELEMENTO SCELTO
                    aumento_imponibile_percentuale = 0
                    aumento_iva_percentuale = 0
                End If
                '-------------------------------------------------------------------------------------------------------------------------------

                Dbc.Close()
                Dbc.Open()
            End If

            'SE E' STATO TROVATO UN AUMENTO DELL'ONERE AGGIORNO LA RIGA DELL'ONERE
            If id_onere <> "0" Then
                Dim onere_prepagato As String = ""
                If imposta_prepagato Then
                    'SE SI STA IMPOSTANDO IL TOTALE COME PREPAGATO ALLORA E' NECESSARIO AGGIORNARE IL TOTALE AGGIUNGENDO IL COSTO DELL'ELEMENTO
                    onere_prepagato = ", imponibile_scontato_prepagato=imponibile_scontato_prepagato+" & Replace(aumento_imponibile_percentuale, ",", ".") & ", iva_imponibile_scontato_prepagato=iva_imponibile_scontato_prepagato+" & Replace(aumento_iva_percentuale, ",", ".")
                End If
                Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=imponibile+" & Replace(aumento_imponibile_percentuale, ",", ".") & ", imponibile_scontato=imponibile_scontato+" & Replace(aumento_imponibile_percentuale, ",", ".") & ",iva_imponibile=iva_imponibile+" & Replace(aumento_iva_percentuale, ",", ".") & ", iva_imponibile_scontato=iva_imponibile_scontato+" & Replace(aumento_iva_percentuale, ",", ".") & onere_prepagato & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_onere & "'", Dbc)
                Cmd.ExecuteNonQuery()
            End If

            Dim update_commissioni As String = ""

            'AGGIORNO LA RIGA DEL TOTALE AGGIUNGENDO IL COSTO DELL'ACCESSORIO E DELL'EVENTUALE AUMENTO DELL'ONERE PERCENTUALE
            Dim totale_prepagato As String = ""
            If imposta_prepagato Then
                'SE SI STA IMPOSTANDO IL TOTALE COME PREPAGATO ALLORA E' NECESSARIO AGGIORNARE IL TOTALE AGGIUNGENDO IL COSTO DELL'ELEMENTO
                totale_prepagato = ", imponibile_scontato_prepagato=imponibile_scontato_prepagato+" & Replace(imponibile_elemento, ",", ".") & ", iva_imponibile_scontato_prepagato=iva_imponibile_scontato_prepagato+" & Replace(iva_elemento, ",", ".") & ",imponibile_onere_prepagato=imponibile_onere_prepagato+" & Replace(aumento_imponibile_percentuale, ",", ".") & ", iva_onere_prepagato=iva_onere_prepagato+" & Replace(aumento_iva_percentuale, ",", ".")
            End If
            If elemento_commissionabile Then
                'SE L'ELEMENTO E' COMMISSIONABILE AUMENTO, NEL TOTALE, I VALORI DELLE COMMISSIONI
                update_commissioni = ", commissioni_imponibile_originale=ISNULL(commissioni_imponibile_originale,0)+" & Replace(imponibile_elemento * CDbl(percentuale_commissione) / 100, ",", ".") &
                    ", commissioni_iva_originale=ISNULL(commissioni_iva_originale,0)+" & Replace(iva_elemento * CDbl(percentuale_commissione) / 100, ",", ".")
            End If
            Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=imponibile+" & Replace(imponibile_elemento, ",", ".") & ", imponibile_scontato=imponibile_scontato+" & Replace(imponibile_elemento, ",", ".") & ", iva_imponibile_scontato=iva_imponibile_scontato+" & Replace(iva_elemento, ",", ".") & ",iva_imponibile=iva_imponibile+" & Replace(iva_elemento, ",", ".") & ",imponibile_onere=imponibile_onere+" & Replace(aumento_imponibile_percentuale, ",", ".") & ", iva_onere=iva_onere+" & Replace(aumento_iva_percentuale, ",", ".") & ", valore_costo=valore_costo+" & Replace(imponibile_elemento + aumento_imponibile_percentuale + iva_elemento + aumento_iva_percentuale, ",", ".") & totale_prepagato & update_commissioni & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND ordine_stampa='6'", Dbc)
            Cmd.ExecuteNonQuery()

            If elemento_commissionabile Then
                update_commissioni = ", commissioni_imponibile_originale=ISNULL(imponibile_scontato,0)*" & Replace(CDbl(percentuale_commissione), ",", ".") & "/100," &
                "commissioni_iva_originale=ISNULL(iva_imponibile_scontato,0)*" & Replace(CDbl(percentuale_commissione), ",", ".") & "/100 "
            End If

            'AGGIORNO IL CAMPO 'SELEZIONATO' DELL'ELEMENTO SCELTO - SE L'ELEMENTO E' COMMISSIONABILE NE APPROFITTO PER SALVARE L'IMPORTO NEI CAMPI NECESSARI
            Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET selezionato='1' " & condizione_prepagato & update_commissioni & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_elemento & "'", Dbc)
            Cmd.ExecuteNonQuery()

            'AGGIORNO LO SCONTO 
            Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET valore_costo=valore_costo+" & Replace(FormatNumber(sconto, 4, , , TriState.False), ",", ".") & ", imponibile=imponibile+" & Replace(FormatNumber(sconto, 4, , , TriState.False), ",", ".") & ", imponibile_scontato=imponibile_scontato+" & Replace(FormatNumber(sconto, 4, , , TriState.False), ",", ".") & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND ordine_stampa='5'", Dbc)
            Cmd.ExecuteNonQuery()

            'TEST SULL'ELEMENTO SCELTO - SE E' UN'ASSICURAZIONE DEVONO ESSERE RIMOSSE LE FRANCHIGIE E AGGIUNTE LE GENERICHE (informative)-------------------------------

            If tipologia_franchigia = "ASSICURAZIONE" Then
                'PRIMA DI AGGIORNARE LE FRANCHIGIE DEVO  CONTROLLARE SE E' NECESSARIO RIMUOVERE LA FRANCHIGIA PARZIALTE/TOTALE PRECEDENTEMENTE AGGIUNTA
                normalizza_assicurazioni(id_preventivo, id_ribaltamento, id_prenotazioni, id_contratto, num_calcolo, id_gruppo, sottotipologia_franchigia, tipo_commissione, percentuale_commissione, id_fonte_commissionabile)

                aggiorna_franchigie(id_preventivo, id_ribaltamento, id_prenotazioni, id_contratto, num_calcolo, id_gruppo, sottotipologia_franchigia)
            End If
            '-----------------------------------------------------------------------------------------------------------------------------------
        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

    End Sub

    Public Shared Sub normalizza_assicurazioni(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String, ByVal sottotipologia_franchigia As String, ByVal tipo_commissione As String, ByVal percentuale_commissione As String, ByVal id_fonte_commissionabile As String)
        If sottotipologia_franchigia = "TOTALE" Then
            'SE STO AGGIUNGENDO L'ASSICURAZIONE TOTALE CONTROLLO SE SI DEVONO RIMUOVERE LE PARZIALI
            Dim tabella As String
            Dim id_da_salvare As String
            If id_preventivo <> "" Then
                id_da_salvare = id_preventivo
                tabella = "preventivi_costi"
            ElseIf id_ribaltamento <> "" Then
                id_da_salvare = id_ribaltamento
                tabella = "ribaltamento_costi"
            ElseIf id_prenotazione <> "" Then
                id_da_salvare = id_prenotazione
                tabella = "prenotazioni_costi"
            ElseIf id_contratto <> "" Then
                id_da_salvare = id_contratto
                tabella = "contratti_costi"
            End If

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Rs As Data.SqlClient.SqlDataReader

            '1 - RECUPERO IL COSTO DELL'ELEMNTO SCELTO - SALVO ANCHE L'INFORMAZIONE SE L'ELEMENTO E' EXTRA OPPURE A SCELTA - SE L'ACCESSORIO E' PREPAGATO ALLORA NON VERRA' RIMOSSO
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_elemento, ISNULL(omaggiato,'0') As omaggiato, prepagato, id_a_carico_di, obbligatorio, sottotipologia_franchigia FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND selezionato='1' AND tipologia_franchigia='ASSICURAZIONE' AND (sottotipologia_franchigia='DANNI' OR sottotipologia_franchigia='FURTO')", Dbc)
            Rs = Cmd.ExecuteReader

            Do While Rs.Read()
                'L'ACCESSORIO DEVE ESSERE A SCELTA E NON PREPAGATO ALTRIMENTI NON DEVE ESSERE RIMOSSO
                If Not Rs("obbligatorio") And Not Rs("prepagato") And Rs("id_a_carico_di") = "2" Then
                    If Rs("omaggiato") Then
                        omaggio_accessorio(False, False, True, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", tipo_commissione, percentuale_commissione, id_fonte_commissionabile, "ASSICURAZIONE", Rs("sottotipologia_franchigia"))
                    Else
                        rimuovi_costo_accessorio(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", "SCELTA", tipo_commissione)
                    End If
                End If
            Loop

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Else
            Dim tabella As String
            Dim id_da_salvare As String
            If id_preventivo <> "" Then
                id_da_salvare = id_preventivo
                tabella = "preventivi_costi"
            ElseIf id_ribaltamento <> "" Then
                id_da_salvare = id_ribaltamento
                tabella = "ribaltamento_costi"
            ElseIf id_prenotazione <> "" Then
                id_da_salvare = id_prenotazione
                tabella = "prenotazioni_costi"
            ElseIf id_contratto <> "" Then
                id_da_salvare = id_contratto
                tabella = "contratti_costi"
            End If

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Rs As Data.SqlClient.SqlDataReader

            '1 - RECUPERO IL COSTO DELL'ELEMNTO SCELTO - SALVO ANCHE L'INFORMAZIONE SE L'ELEMENTO E' EXTRA OPPURE A SCELTA - SE L'ACCESSORIO E' PREPAGATO ALLORA NON VERRA' RIMOSSO
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_elemento, ISNULL(omaggiato,'0') As omaggiato, prepagato, id_a_carico_di, obbligatorio, sottotipologia_franchigia FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND selezionato='1' AND tipologia_franchigia='ASSICURAZIONE' AND (sottotipologia_franchigia='TOTALE')", Dbc)
            Rs = Cmd.ExecuteReader

            Do While Rs.Read()
                'L'ACCESSORIO DEVE ESSERE A SCELTA E NON PREPAGATO ALTRIMENTI NON DEVE ESSERE RIMOSSO
                If Not Rs("obbligatorio") And Not Rs("prepagato") And Rs("id_a_carico_di") = "2" Then
                    If Rs("omaggiato") Then
                        omaggio_accessorio(False, False, True, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", tipo_commissione, percentuale_commissione, id_fonte_commissionabile, "ASSICURAZIONE", Rs("sottotipologia_franchigia"))
                    Else
                        rimuovi_costo_accessorio(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", "SCELTA", tipo_commissione)
                    End If
                End If
            Loop

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End If
    End Sub

    Public Shared Sub rimuovi_costo_accessorio(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String, ByVal id_elemento As String, ByVal num_elemento As String, ByVal tipo As String, ByVal tipo_commissione As String)
        'TIPO:  SCELTA=ACCESSORIO A SCELTA (ALLA FINE DELLA PROCEDURA L'ELEMENTO VIENE IMPOSTATO COME NON SCELTO)
        '       EXTRA = ACCESSORIO EXTRA (ALLA FINE DELLA PROCEDURA LA RIGA VIENE RIMOSSA DA preventivi_costi)
        '       OMAGGIO = SE VENGO DA OMAGGIO ACCESSORIO NON DEVO IN OGNI CASO ELIMINARE LA RIGA
        'NUM_ELEMENTO: SE VIENE PASSATO VIENE RIMOSSO IL COSTO DELL'ACCESSORIO NUMERATO SECONDO IL CAMPO num_elemento (SERVE AD ESEMPIO
        'PER LO YOUNG DRIVER, NEL CASO IN CUI LO SI DEBBA RIMUOVERE SOLO PER IL PRIMO O SOLO PER IL SECONDO GUIDATORE)
        Dim id_da_salvare As String
        Dim tabella As String
        If id_preventivo <> "" Then
            id_da_salvare = id_preventivo
            tabella = "preventivi_costi"
        ElseIf id_ribaltamento <> "" Then
            id_da_salvare = id_ribaltamento
            tabella = "ribaltamento_costi"
        ElseIf id_prenotazione <> "" Then
            id_da_salvare = id_prenotazione
            tabella = "prenotazioni_costi"
        ElseIf id_contratto <> "" Then
            id_da_salvare = id_contratto
            tabella = "contratti_costi"
        End If

        Dim condizione_num As String = ""
        If num_elemento <> "" And num_elemento <> "NULL" Then
            condizione_num = " AND num_elemento='" & num_elemento & "'"
        End If

        Dim id_onere As String = "0"
        Dim imponibile_percentuale As Double = 0
        Dim iva_percentuale As Double = 0

        Dim imponibile_onere As Double = 0
        Dim iva_onere As Double = 0

        Dim aliquota_iva As Double

        Dim imponibile_elemento As Double = 0
        Dim iva_elemento As Double = 0

        Dim aumento_imponibile_percentuale As Double = 0
        Dim aumento_iva_percentuale As Double = 0

        Dim commissioni_imponibile As Double = 0
        Dim commissioni_iva As Double = 0

        Dim valorizza As Boolean

        Dim sconto As Double = 0
        Dim tipologia_franchigia As String
        Dim sottotipologia_franchigia As String

        Dim omaggiato As Boolean

        Dim ordine_stampa As String = ""

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Rs As Data.SqlClient.SqlDataReader

        '1 - RECUPERO IL COSTO DELL'ELEMNTO SCELTO - SALVO ANCHE L'INFORMAZIONE SE L'ELEMENTO E' EXTRA OPPURE A SCELTA - SE L'ACCESSORIO E' PREPAGATO ALLORA NON VERRA' RIMOSSO
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 imponibile,iva_imponibile, imponibile_scontato,  iva_imponibile_scontato, ISNULL(valorizza,'1') As valorizza, condizioni_elementi.tipologia_franchigia, condizioni_elementi.sottotipologia_franchigia, ISNULL(imponibile_onere, 0) As imponibile_onere, ISNULL(iva_onere,0) As iva_onere, ISNULL(omaggiato,0) As omaggiato, ISNULL(commissioni_imponibile_originale,0) As commissioni_imponibile_originale, ISNULL(commissioni_iva_originale,0) As commissioni_iva_originale, ordine_stampa FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_elemento & "' AND selezionato='1' AND NOT prepagato='1'" & condizione_num, Dbc)
        Rs = Cmd.ExecuteReader

        If Rs.Read() Then
            tipologia_franchigia = Rs("tipologia_franchigia") & ""
            sottotipologia_franchigia = Rs("sottotipologia_franchigia") & ""

            valorizza = Rs("valorizza")
            omaggiato = Rs("omaggiato")
            ordine_stampa = Rs("ordine_stampa")
            'ESEGUO LE OPERAZIONI SOLO SE L'ELEMENTO NON E' OMAGGIATO
            If Not omaggiato Then
                imponibile_elemento = Rs("imponibile_scontato")
                iva_elemento = Rs("iva_imponibile_scontato")

                imponibile_onere = Rs("imponibile_onere")
                iva_onere = Rs("iva_onere")

                sconto = Rs("imponibile") + Rs("iva_imponibile") - Rs("imponibile_scontato") - Rs("iva_imponibile_scontato")

                If tipo_commissione = "1" Then
                    'SE SIAMO NEL CASO DI FONTE COMMISSIONABILE CON COMMISSIONI RICONOSCIUTE SUCCESSIVAMENTE E L'ELEMENTO E' COMMISSIONABILE (LO E' SE I CAMPI RELATIVI SONO VALORIZZATI)
                    'MEMORIZZO GLI IMPORTI COMMISSIONABILI IN MODO DA POTERLI RIMUOVERE DAL TOTALE
                    commissioni_imponibile = Rs("commissioni_imponibile_originale")
                    commissioni_iva = Rs("commissioni_iva_originale")
                End If
            Else
                imponibile_elemento = 0
                iva_elemento = 0
                imponibile_onere = 0
                iva_onere = 0
                sconto = 0
            End If

            Dbc.Close()
            Dbc.Open()

            '2 - CONTROLLO SE E' STATO SPECIFICATO L'ELEMENTO PERCENTUALE DI TIPO ONERE - OTTENGO IL SUO ID - SOLAMENTE SE L'IMPONIBILE DELL'ONERE
            'E' VALORIZZATO ALTRIMENTI VUOL DIRE CHE SULL'ELEMENTO NON DEVO CALCOLARE L'ONERE
            If imponibile_onere <> 0 Then
                Cmd = New Data.SqlClient.SqlCommand("SELECT TOP 1 " & tabella & ".id_elemento FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE condizioni_elementi.tipologia='ONERE' AND " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "' AND NOT " & tabella & ".valore_percentuale IS NULL", Dbc)
                Rs = Cmd.ExecuteReader
                Rs.Read()

                'NUOVA VERSIONE CON ELEMENTO PERCENTUALE PRECALCOLATO --------------------------------------------------------------------------
                If Rs.HasRows Then
                    id_onere = Rs("id_elemento")

                    'NELLA TABELLA DEI COSTI L'IMPONIBILE E L'IVA DELL'ONERE E' STATO PRECALCOLATO - I CAMPI SONO VALORIZZATI SOLAMENTE SOLAMENTE SE
                    'SULL'ELEMENTO SI DEVE PAGARE L'ONERE ALTRIMENTI I VALORI SONO 0

                    aumento_imponibile_percentuale = imponibile_onere
                    aumento_iva_percentuale = iva_onere
                Else
                    'IN QUESTO CASO NON E' STATO SPECIFICATO ALCUN ELEMENTO PERCENTUALE - SI DEVE SEMPLICAMENTE AGGIUNGERE AL TOTALE IL COSTO DELLO
                    'ELEMENTO SCELTO
                    aumento_imponibile_percentuale = 0
                    aumento_iva_percentuale = 0
                End If
                '-------------------------------------------------------------------------------------------------------------------------------

                Dbc.Close()
                Dbc.Open()
            End If


            'SE E' STATO TROVATO UN AUMENTO DELL'ONERE AGGIORNO LA RIGA DELL'ONERE
            If id_onere <> "0" Then
                Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=imponibile-" & Replace(aumento_imponibile_percentuale, ",", ".") & ", imponibile_scontato=imponibile_scontato-" & Replace(aumento_imponibile_percentuale, ",", ".") & ",iva_imponibile=iva_imponibile-" & Replace(aumento_iva_percentuale, ",", ".") & ", iva_imponibile_scontato=iva_imponibile_scontato-" & Replace(aumento_iva_percentuale, ",", ".") & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_onere & "'", Dbc)
                Cmd.ExecuteNonQuery()
            End If

            If Not omaggiato Then
                'AGGIORNO LA RIGA DEL TOTALE RIMUOVENDO IL COSTO DELL'ACCESSORIO E DELL'EVENTUALE AUMENTO DELL'ONERE PERCENTUALE
                Dim update_commissioni As String = ""
                If tipo_commissione = "1" Then
                    'RIMUOVO DAL TOTALE LE COMMISSIONI_DELL'ELEMENTO
                    update_commissioni = ", commissioni_imponibile_originale=ISNULL(commissioni_imponibile_originale,0)-" & Replace(commissioni_imponibile, ",", ".") &
                        ",commissioni_iva_originale=ISNULL(commissioni_iva_originale,0)-" & Replace(commissioni_iva, ",", ".")
                End If
                Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=imponibile-" & Replace(imponibile_elemento, ",", ".") & ", imponibile_scontato=imponibile_scontato-" & Replace(imponibile_elemento, ",", ".") & ", iva_imponibile_scontato=iva_imponibile_scontato-" & Replace(iva_elemento, ",", ".") & ",iva_imponibile=iva_imponibile-" & Replace(iva_elemento, ",", ".") & ",imponibile_onere=imponibile_onere-" & Replace(aumento_imponibile_percentuale, ",", ".") & ",iva_onere=iva_onere-" & Replace(aumento_iva_percentuale, ",", ".") & ", valore_costo=valore_costo-" & Replace(imponibile_elemento + aumento_imponibile_percentuale + iva_elemento + aumento_iva_percentuale, ",", ".") & update_commissioni & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND ordine_stampa='6'", Dbc)
                Cmd.ExecuteNonQuery()
            End If

            'AGGIORNO IL CAMPO 'SELEZIONATO' DELL'ELEMENTO SCELTO OPPURE LO RIMUOVO - L'ELEMENTO VIENE RIMOSSO ANCHE SE VIENE PASSAATO "SCELTA"
            '(IN QUANTO ERA PRESENTE TRA GLI ELEMENTI VALORIZZATI SELEZIONABILI) MA IN REALTA' E' SALVATO IN condizioni_elementi COME 
            'NON DA VALORIZZARE (VUOL DIRE CHE L'ELEMENTO ERA STATO AGGIUNTO DAL MENU' A TENDINA DEGLI ELEMENTI EXTRA E QUINDI, RIMUOVENDOLO, LO
            'SI DEVE ANCHE CANCELLARE)
            If (tipo = "EXTRA" Or (Not valorizza And tipo <> "OMAGGIO")) And ordine_stampa <> "7" Then
                Cmd = New Data.SqlClient.SqlCommand("DELETE FROM " & tabella & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_elemento & "'" & condizione_num, Dbc)
                Cmd.ExecuteNonQuery()
            ElseIf tipo = "SCELTA" Or tipo = "OMAGGIO" Then
                Dim update_commissioni As String = ""
                If tipo_commissione = "1" Then
                    'SETTO A NULL I CAMPI DELLE COMMISSIONI - ATTUALMENTE SI POTREBBE FARE IN OGNI CASO MA PUO' DARE PROBLEMI SE POI SI RENDERANNO COMMISIONABILI GLI ELEMENTI ANCHE NEL CASO DI COMMISSIONI PREINCASSATE DALL'AGENZIA
                    update_commissioni = ", commissioni_imponibile_originale=NULL, commissioni_iva_originale=NULL "
                End If
                Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET selezionato='0' " & update_commissioni & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_elemento & "'" & condizione_num, Dbc)
                Cmd.ExecuteNonQuery()
            End If

            'AGGIORNO LO SCONTO 
            If Not omaggiato Then
                Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET valore_costo=valore_costo-" & Replace(FormatNumber(sconto, 4, , , TriState.False), ",", ".") & ",imponibile=imponibile-" & Replace(FormatNumber(sconto, 4, , , TriState.False), ",", ".") & ", imponibile_scontato=imponibile_scontato-" & Replace(FormatNumber(sconto, 4, , , TriState.False), ",", ".") & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND ordine_stampa='5'", Dbc)
                Cmd.ExecuteNonQuery()
            End If

            'SE STO RIMUOVENDO UN ELEMENTO ASSICURAZIONE FACCIO IN MODO DI VISUALIZZARE LE FRANCHIGIE GENERICHE E RIMUOVERE LE EVENTUALI FRANCHIGIE RIDOTTE
            If tipologia_franchigia = "ASSICURAZIONE" And tipo <> "OMAGGIO" Then
                aggiorna_franchigie(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, sottotipologia_franchigia)
            End If
        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Public Function esiste_young_driver_primo_guidatore(ByVal id_young_driver As String, ByVal id_gruppo As String, ByVal num_calcolo As String, ByVal id_preventivo As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal id_ribaltamento As String) As Boolean
        Dim id_da_cercare As String
        Dim tabella As String
        If id_preventivo <> "" Then
            id_da_cercare = id_preventivo
            tabella = "preventivi_costi"
        ElseIf id_ribaltamento <> "" Then
            id_da_cercare = id_ribaltamento
            tabella = "ribaltamento_costi"
        ElseIf id_prenotazione <> "" Then
            id_da_cercare = id_prenotazione
            tabella = "prenotazioni_costi"
        ElseIf id_contratto <> "" Then
            id_da_cercare = id_contratto
            tabella = "contratti_costi"
        End If


        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM " & tabella & " WITH(NOLOCK) WHERE id_elemento='" & id_young_driver & "' AND num_elemento='1' AND id_gruppo='" & id_gruppo & "' AND id_documento='" & id_da_cercare & "' AND num_calcolo='" & num_calcolo & "'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test = "" Then
            esiste_young_driver_primo_guidatore = False
        Else
            esiste_young_driver_primo_guidatore = True
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Function esiste_young_driver_secondo_guidatore(ByVal id_young_driver As String, ByVal id_gruppo As String, ByVal num_calcolo As String, ByVal id_preventivo As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal id_ribaltamento As String) As Boolean
        Dim id_da_cercare As String
        Dim tabella As String
        If id_preventivo <> "" Then
            id_da_cercare = id_preventivo
            tabella = "preventivi_costi"
        ElseIf id_ribaltamento <> "" Then
            id_da_cercare = id_ribaltamento
            tabella = "ribaltamento_costi"
        ElseIf id_prenotazione <> "" Then
            id_da_cercare = id_prenotazione
            tabella = "prenotazioni_costi"
        ElseIf id_contratto <> "" Then
            id_da_cercare = id_contratto
            tabella = "contratti_costi"
        End If


        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM " & tabella & " WITH(NOLOCK) WHERE id_elemento='" & id_young_driver & "' AND num_elemento='2' AND id_gruppo='" & id_gruppo & "' AND id_documento='" & id_da_cercare & "' AND num_calcolo='" & num_calcolo & "'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test = "" Then
            esiste_young_driver_secondo_guidatore = False
        Else
            esiste_young_driver_secondo_guidatore = True
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    'Public Function esiste_val_gps(ByVal id_gruppo As String, ByVal num_calcolo As String, ByVal id_preventivo As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal id_ribaltamento As String) As Boolean
    '    Dim id_da_cercare As String
    '    Dim tabella As String
    '    If id_preventivo <> "" Then
    '        id_da_cercare = id_preventivo
    '        tabella = "preventivi_costi"
    '    ElseIf id_ribaltamento <> "" Then
    '        id_da_cercare = id_ribaltamento
    '        tabella = "ribaltamento_costi"
    '    ElseIf id_prenotazione <> "" Then
    '        id_da_cercare = id_prenotazione
    '        tabella = "prenotazioni_costi"
    '    ElseIf id_contratto <> "" Then
    '        id_da_cercare = id_contratto
    '        tabella = "contratti_costi"
    '    End If


    '    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '    Dbc.Open()
    '    Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM " & tabella & " WITH(NOLOCK) WHERE id_elemento='" & id_young_driver & "' AND num_elemento='2' AND id_gruppo='" & id_gruppo & "' AND id_documento='" & id_da_cercare & "' AND num_calcolo='" & num_calcolo & "'", Dbc)

    '    Dim test As String = Cmd.ExecuteScalar & ""

    '    If test = "" Then
    '        esiste_young_driver_secondo_guidatore = False
    '    Else
    '        esiste_young_driver_secondo_guidatore = True
    '    End If

    '    Cmd.Dispose()
    '    Cmd = Nothing
    '    Dbc.Close()
    '    Dbc.Dispose()
    '    Dbc = Nothing
    'End Function

    Public Sub calcola_costo_joung_driver_primo_guidatore(ByVal id_accessorio As String, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal eta_primo_guidatore As String, ByVal eta_secondo_guidatore As String, ByVal id_gruppo As String, ByVal giorni_noleggio As Integer, ByVal giorni_prepagati_x_modifica As Integer, ByVal prenotazione_prepagata As Boolean, ByVal sconto As Double, ByVal id_tariffe_righe As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String, ByVal forza_aggiunzione_x_prepagato As Boolean, ByVal tipo_commissione As String, ByVal percentuale_commissionabile As String, ByVal id_fonte_commissionabile As String)
        'VIENE UTILIZZATA PER AGGIUNGERE IL COSTO DELLO JOUNG DRIVER PER IL PRIMO GUIDATORE.

        'PER PRIMA COSA SI CONTROLLA SE E' L'ACCESSORIO NON E' GIA' STATO AGGIUNTO
        Dim tabella As String
        Dim id_da_cercare As String
        If id_preventivo <> "" Then
            tabella = "preventivi_costi"
            id_da_cercare = id_preventivo
        ElseIf id_ribaltamento <> "" Then
            tabella = "ribaltamento_costi"
            id_da_cercare = id_ribaltamento
        ElseIf id_prenotazione <> "" Then
            tabella = "prenotazioni_costi"
            id_da_cercare = id_prenotazione
        ElseIf id_contratto <> "" Then
            tabella = "contratti_costi"
            id_da_cercare = id_contratto
        End If

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM " & tabella & " WITH(NOLOCK) WHERE id_documento='" & id_da_cercare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_accessorio & "' AND num_elemento='1'", Dbc)
        Dim id_young As String = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing



        If id_young = "" Then
            'PASSO 1: SE LA TARIFFA HA UNA TARIFFA MADRE ASSOCIATA RECUPERO L'id_tariffe_righe PER LA MADRE
            'Dim id_tariffe_righe_madre As String = getIdTariffeRigheMadre(id_tariffe_righe, data_pick_up, provenienza_per_data)

            'PASSO 2: RECUPERO GLI ID DELLE CONDIZIONI E DEL TEMPO KM ASSOCIATO ALLE RIGHE DELLA TARIFFA FIGLIA E, SE VIENE TROVATA, DELLA CONDIZIONE MADRE
            Dim id_condizione_figlia As String = getIdCondizione(id_tariffe_righe)
            Dim id_condizione_madre As String = getIdCondizioneMadre(id_tariffe_righe)
            Dim id_tempo_km_figlia As String = getIdTempoKm(id_tariffe_righe)
            Dim id_tempo_km_madre As String = "0"

            'PASSO 3: SI CERCA IL COSTO DELL'ACCESSORIO
            Dim elementi_prepagati As New Collection
            'SE LA PRENOTAZIONE E' PREPAGATA E GIORNI_PREPAGATI E' DIVERSO DA 0 (PRIMO CALCOLO - CALCOLO SUCCESSIVO CON FORZATURA AGGIUNZIONE ELEMENTO)
            If prenotazione_prepagata And giorni_prepagati_x_modifica > 0 Then
                elementi_prepagati = getElementiPrepagati(id_prenotazione, id_contratto, id_ribaltamento, num_calcolo - 1, id_gruppo)
            End If

            Dim trovato As Boolean = calcola_supplemento_joung_driver("primo", id_gruppo, giorni_noleggio, giorni_prepagati_x_modifica, prenotazione_prepagata, elementi_prepagati, stazione_pick_up, stazione_drop_off, eta_primo_guidatore, eta_secondo_guidatore, id_condizione_figlia, id_condizione_madre, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, forza_aggiunzione_x_prepagato)

            'PASSO 4: AGGIORNO IL COSTO PER IVA/ONERE/TOTALE (SE E' STATO TROVATO IL COSTO)
            If trovato Then
                If prenotazione_prepagata And giorni_prepagati_x_modifica > 0 Then
                    'COPIO I COSTI PREPAGATI DAL CALCOLO PRECEDENTE PER IL SINGOLO ACCESSORIO - NECESSARIO PER IL CALCOLO DELL'EVENTUALE SCONTO
                    funzioni_comuni.riporta_costi_prepagati(id_prenotazione, id_contratto, num_calcolo, id_gruppo, id_accessorio, "1", False)
                End If
                calcolo_iva_e_totale_singolo_accessorio(stazione_pick_up, sconto, id_gruppo, id_accessorio, "1", id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, False, tipo_commissione, percentuale_commissionabile, id_fonte_commissionabile)
            End If
        End If
    End Sub

    Public Sub calcola_costo_joung_driver_secondo_guidatore(ByVal id_accessorio As String, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal eta_primo_guidatore As String, ByVal eta_secondo_guidatore As String, ByVal id_gruppo As String, ByVal giorni_noleggio As String, ByVal giorni_prepagati_x_modifica As Integer, ByVal prenotazione_prepagata As Boolean, ByVal sconto As Double, ByVal id_tariffe_righe As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String, ByVal forza_aggiunzione_x_prepagato As Boolean, ByVal tipo_commissione As String, ByVal percentuale_commissionabile As String, ByVal id_fonte_commissionabile As String)
        'VIENE UTILIZZATA PER AGGIUNGERE IL COSTO DELLO JOUNG DRIVER PER IL SECONDO GUIDATORE. E' UN ACCESSORIO CHE NON VIENE CALCOLATO
        'IN FASE DI VALORIZZAZIONE DEL PREVENTIVO.

        'PER PRIMA COSA SI CONTROLLA SE E' L'ACCESSORIO NON E' GIA' STATO AGGIUNTO
        Dim tabella As String
        Dim id_da_cercare As String
        If id_preventivo <> "" Then
            tabella = "preventivi_costi"
            id_da_cercare = id_preventivo
        ElseIf id_ribaltamento <> "" Then
            tabella = "ribaltamento_costi"
            id_da_cercare = id_ribaltamento
        ElseIf id_prenotazione <> "" Then
            tabella = "prenotazioni_costi"
            id_da_cercare = id_prenotazione
        ElseIf id_contratto <> "" Then
            tabella = "contratti_costi"
            id_da_cercare = id_contratto
        End If

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM " & tabella & " WITH(NOLOCK) WHERE id_documento='" & id_da_cercare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_accessorio & "' AND num_elemento='2'", Dbc)
        Dim id_young As String = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        HttpContext.Current.Trace.Write("A 111 " & num_calcolo & " " & id_young)

        If id_young = "" Then
            'PASSO 1: SE LA TARIFFA HA UNA TARIFFA MADRE ASSOCIATA RECUPERO L'id_tariffe_righe PER LA MADRE
            'Dim id_tariffe_righe_madre As String = getIdTariffeRigheMadre(id_tariffe_righe, data_pick_up, provenienza_per_data)

            'PASSO 2: RECUPERO GLI ID DELLE CONDIZIONI E DEL TEMPO KM ASSOCIATO ALLE RIGHE DELLA TARIFFA FIGLIA E, SE E' STATO TROVATA, DELLA TARIFFA MADRE
            Dim id_condizione_figlia As String = getIdCondizione(id_tariffe_righe)
            Dim id_condizione_madre As String = getIdCondizioneMadre(id_tariffe_righe)
            Dim id_tempo_km_figlia As String = getIdTempoKm(id_tariffe_righe)
            Dim id_tempo_km_madre As String = "0"

            'PASSO 3: SI CERCA IL COSTO DELL'ACCESSORIO
            Dim elementi_prepagati As New Collection



            If prenotazione_prepagata And giorni_prepagati_x_modifica > 0 Then
                elementi_prepagati = getElementiPrepagati(id_prenotazione, id_contratto, id_ribaltamento, num_calcolo, id_gruppo)
            End If

            Dim trovato As Boolean = calcola_supplemento_joung_driver("secondo", id_gruppo, giorni_noleggio, giorni_prepagati_x_modifica, prenotazione_prepagata, elementi_prepagati, stazione_pick_up, stazione_drop_off, eta_primo_guidatore, eta_secondo_guidatore, id_condizione_figlia, id_condizione_madre, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, forza_aggiunzione_x_prepagato)

            HttpContext.Current.Trace.Write("A 222 - " & trovato & " - " & giorni_noleggio & " - " & giorni_prepagati_x_modifica)

            'PASSO 4: AGGIORNO IL COSTO PER IVA/ONERE/TOTALE (SE E' STATO TROVATO IL COSTO)

            If trovato Then
                If prenotazione_prepagata And giorni_prepagati_x_modifica > 0 Then
                    'COPIO I COSTI PREPAGATI DAL CALCOLO PRECEDENTE PER IL SINGOLO ACCESSORIO - NECESSARIO PER IL CALCOLO DELL'EVENTUALE SCONTO
                    funzioni_comuni.riporta_costi_prepagati(id_prenotazione, id_contratto, num_calcolo, id_gruppo, id_accessorio, "2", False)
                End If
                calcolo_iva_e_totale_singolo_accessorio(stazione_pick_up, sconto, id_gruppo, id_accessorio, "2", id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, False, tipo_commissione, percentuale_commissionabile, id_fonte_commissionabile)
            End If
        End If
    End Sub

    Public Sub calcola_costo_joung_driver_secondo_guidatore_primo_calcolo_prepagato(ByVal id_accessorio As String, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal eta_primo_guidatore As String, ByVal eta_secondo_guidatore As String, ByVal id_gruppo As String, ByVal giorni_noleggio As String, ByVal giorni_prepagati_x_modifica As Integer, ByVal prenotazione_prepagata As Boolean, ByVal sconto As Double, ByVal id_tariffe_righe As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String, ByVal forza_aggiunzione_x_prepagato As Boolean, ByVal tipo_commissione As String, ByVal percentuale_commissionabile As String, ByVal id_fonte_commissionabile As String)
        'VIENE UTILIZZATA PER AGGIUNGERE IL COSTO DELLO JOUNG DRIVER PER IL SECONDO GUIDATORE. E' UN ACCESSORIO CHE NON VIENE CALCOLATO
        'IN FASE DI VALORIZZAZIONE DEL PREVENTIVO.

        'PER PRIMA COSA SI CONTROLLA SE E' L'ACCESSORIO NON E' GIA' STATO AGGIUNTO
        Dim tabella As String
        Dim id_da_cercare As String
        If id_preventivo <> "" Then
            tabella = "preventivi_costi"
            id_da_cercare = id_preventivo
        ElseIf id_ribaltamento <> "" Then
            tabella = "ribaltamento_costi"
            id_da_cercare = id_ribaltamento
        ElseIf id_prenotazione <> "" Then
            tabella = "prenotazioni_costi"
            id_da_cercare = id_prenotazione
        ElseIf id_contratto <> "" Then
            tabella = "contratti_costi"
            id_da_cercare = id_contratto
        End If

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM " & tabella & " WITH(NOLOCK) WHERE id_documento='" & id_da_cercare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_accessorio & "' AND num_elemento='2'", Dbc)
        Dim id_young As String = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

        If id_young = "" Then
            'PASSO 1: SE LA TARIFFA HA UNA TARIFFA MADRE ASSOCIATA RECUPERO L'id_tariffe_righe PER LA MADRE
            'Dim id_tariffe_righe_madre As String = getIdTariffeRigheMadre(id_tariffe_righe, data_pick_up, provenienza_per_data)

            'PASSO 2: RECUPERO GLI ID DELLE CONDIZIONI E DEL TEMPO KM ASSOCIATO ALLE RIGHE DELLA TARIFFA FIGLIA E, SE E' STATO TROVATA, DELLA TARIFFA MADRE
            Dim id_condizione_figlia As String = getIdCondizione(id_tariffe_righe)
            Dim id_condizione_madre As String = getIdCondizioneMadre(id_tariffe_righe)
            Dim id_tempo_km_figlia As String = getIdTempoKm(id_tariffe_righe)
            Dim id_tempo_km_madre As String = "0"

            'PASSO 3: SI CERCA IL COSTO DELL'ACCESSORIO
            Dim elementi_prepagati As New Collection
            If prenotazione_prepagata And giorni_prepagati_x_modifica > 0 Then
                elementi_prepagati = getElementiPrepagati(id_prenotazione, id_contratto, id_ribaltamento, num_calcolo - 1, id_gruppo)
            End If
            Dim trovato As Boolean = calcola_supplemento_joung_driver("secondo", id_gruppo, giorni_noleggio, giorni_prepagati_x_modifica, prenotazione_prepagata, elementi_prepagati, stazione_pick_up, stazione_drop_off, eta_primo_guidatore, eta_secondo_guidatore, id_condizione_figlia, id_condizione_madre, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, forza_aggiunzione_x_prepagato)

            'PASSO 4: AGGIORNO IL COSTO PER IVA/ONERE/TOTALE (SE E' STATO TROVATO IL COSTO)

            If trovato Then
                If prenotazione_prepagata And giorni_prepagati_x_modifica > 0 Then
                    'COPIO I COSTI PREPAGATI DAL CALCOLO PRECEDENTE PER IL SINGOLO ACCESSORIO - NECESSARIO PER IL CALCOLO DELL'EVENTUALE SCONTO
                    funzioni_comuni.riporta_costi_prepagati(id_prenotazione, id_contratto, num_calcolo, id_gruppo, id_accessorio, "2", False)
                End If
                calcolo_iva_e_totale_singolo_accessorio(stazione_pick_up, sconto, id_gruppo, id_accessorio, "2", id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, True, tipo_commissione, percentuale_commissionabile, id_fonte_commissionabile)
            End If
        End If
    End Sub

    Public Sub aggiungi_val_gps(ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal id_gruppo As String, ByVal giorni_noleggio As Integer, ByVal giorni_prepagati_x_modifica As Integer, ByVal prepagata As Boolean, ByVal elementi_prepagati As Collection, ByVal giorni_restanti_x_nolo_in_corso As String, ByVal data_aggiunta_x_nolo_in_corso As String, ByVal sconto As Double, ByVal id_tariffe_righe As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As Integer, ByVal id_utente As String, ByVal tipo_commissione As String, ByVal percentuale_commissionabile As String, ByVal id_fonte_commissionabile As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT val_gratis FROM stazioni_distanza WITH(NOLOCK) WHERE ((id_stazione1='" & stazione_pick_up & "' AND id_stazione2='" & stazione_drop_off & "') OR (id_stazione1='" & stazione_drop_off & "' AND id_stazione2='" & stazione_pick_up & "'))", Dbc)

        Dim val_gratis As String = Cmd.ExecuteScalar

        If Not val_gratis = "True" Then
            'SELEZIONO L'ID DEL VAL GPS
            Cmd = New Data.SqlClient.SqlCommand("SELECT id FROM condizioni_elementi WHERE tipologia='VAL_GPS'", Dbc)
            Dim id_elemento As String = Cmd.ExecuteScalar & ""

            If id_elemento <> "" Then
                If prepagata And elementi_prepagati Is Nothing And giorni_prepagati_x_modifica = 0 Then
                    'QUESTA OPERAZIONE SERVE NEL CASO IN CUI QUESTA FUNZIONE VENGA CHIAMATA DALL'ESTERNO (QUINDI SENZA PASSARE LA LISTA DEGLI ELEMENTI PREPAGATI)
                    'VISTO CHE giorni_prepagati_x_modifica E' UGUALE A 0 VUOL DIRE CHE E' IL PRIMO CALCOLO - L'ACCESSORIO E' CERTAMENTE PREPAGATO
                    elementi_prepagati = New Collection
                    elementi_prepagati.Add(id_elemento, id_elemento)
                ElseIf prepagata And elementi_prepagati Is Nothing And giorni_prepagati_x_modifica > 0 Then
                    'RICHIAMATA DALL'ESTERNO MA IN MODIFICA - SI DEVE RECUPERARE IL SET DEGLI ACCESSORI PRECEDENTEMENTE PREPAGATI
                    elementi_prepagati = New Collection
                    elementi_prepagati = getElementiPrepagati(id_prenotazione, id_contratto, id_ribaltamento, num_calcolo - 1, id_gruppo)
                End If

                'If Not elementi_prepagati.Contains(id_elemento) Then
                '    prepagata = False
                'End If

                aggiungi_accessorio_obbligatorio(id_elemento, stazione_pick_up, stazione_drop_off, id_gruppo, giorni_noleggio, giorni_prepagati_x_modifica, prepagata, elementi_prepagati, giorni_restanti_x_nolo_in_corso, data_aggiunta_x_nolo_in_corso, sconto, id_tariffe_righe, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_utente, False, tipo_commissione, percentuale_commissionabile, id_fonte_commissionabile)
            End If
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Public Sub aggiungi_accessorio_obbligatorio(ByVal id_accessorio As String, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal id_gruppo As String, ByVal giorni_noleggio As String, ByVal giorni_prepagati_x_modifica As Integer, ByVal prepagata As Boolean, ByVal elementi_prepagati As Collection, ByVal giorni_restanti_x_nolo_in_corso As String, ByVal data_aggiunta_x_nolo_in_corso As String, ByVal sconto As Double, ByVal id_tariffe_righe As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String, ByVal accessorio_val As Boolean, ByVal tipo_commissione As String, ByVal percentuale_commissionabile As String, ByVal id_fonte_commissionabile As String)
        'VIENE UTILIZZATO PER AGGIUBNGERE IL COSTI DI UN ELEMENTO OBBLIGATORIO SUCCESSIVAMENTE AL CALCOLO - ES: SPESE DI SPEDIZIONE POSTALI NEL CASO
        'IN CUI LA DITTA RICHIEDA LA SPEDIZIONE DELLA FATTURA PER MEZZO POSTA

        'IL PARAMETRO accossorio_val PERMETTE DI FORZARE L'INSERIMENTO DI UN ACCESSORIO VAL ANCHE SE NON DEVE ESSERE PAGATO (caso prepagato con val non piu' necessario).
        'IN QUESTO CASO, INFATTI, LA FUNZIONE CHE SI OCCUPA DEL CALCOLO DEVE LEGGERE L'ID ELEMENTO DA UNA COLONNA DIFFERENTE RISPETTO AGLI ALTRI ELEMENTI

        Dim id_condizione_figlia As String = getIdCondizione(id_tariffe_righe)
        Dim id_condizione_madre As String = getIdCondizioneMadre(id_tariffe_righe)

        'SI CERCA IL COSTO DELL'ACCESSORIO
        Dim trovato As Boolean = calcola_accessorio_extra_o_obbligatorio(id_accessorio, id_gruppo, giorni_noleggio, giorni_prepagati_x_modifica, prepagata, elementi_prepagati, giorni_restanti_x_nolo_in_corso, data_aggiunta_x_nolo_in_corso, stazione_pick_up, stazione_drop_off, id_condizione_figlia, id_condizione_madre, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, True, accessorio_val)

        'AGGIORNO IL COSTO PER IVA/ONERE/TOTALE (SE E' STATO TROVATO IL COSTO)
        If trovato Then
            Dim primo_calcolo_prepagato As Boolean = False
            If prepagata And giorni_prepagati_x_modifica = 0 Then
                primo_calcolo_prepagato = True
            ElseIf prepagata And giorni_prepagati_x_modifica > 0 Then
                'COPIO I COSTI PREPAGATI DAL CALCOLO PRECEDENTE PER IL SINGOLO ACCESSORIO - NECESSARIO PER IL CALCOLO DELL'EVENTUALE SCONTO
                funzioni_comuni.riporta_costi_prepagati(id_prenotazione, id_contratto, num_calcolo, id_gruppo, id_accessorio, "", False)
            End If
            calcolo_iva_e_totale_singolo_accessorio(stazione_pick_up, sconto, id_gruppo, id_accessorio, "", id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, primo_calcolo_prepagato, tipo_commissione, percentuale_commissionabile, id_fonte_commissionabile)
            If primo_calcolo_prepagato Then
                'PRIMO CALCOLO - SALVO I COSTI DEL PREPAGATO
                prepagato_memorizza_costi_prepagati_x_fattura(id_prenotazione, id_ribaltamento, id_contratto, id_gruppo, num_calcolo, id_accessorio)
            End If
        End If
    End Sub

    Public Sub calcola_costo_elemento_extra(ByVal id_accessorio As String, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal eta_primo_guidatore As String, ByVal eta_secondo_guidatore As String, ByVal id_gruppo As String, ByVal giorni_noleggio As Integer, ByVal giorni_prepagati_x_modifica As Integer, ByVal prepagato As Boolean, ByVal giorni_restanti_x_nolo_in_corso As String, ByVal data_aggiunta_x_nolo_in_corso As String, ByVal sconto As Double, ByVal id_tariffe_righe As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String, ByVal tipo_commissione As String, ByVal percentuale_commissionabile As String, ByVal id_fonte_commissionabile As String)
        'VIENE UTILIZZATA PER AGGIUNGERE IL COSTO DI UN ELEMENTO EXTRA (E' UN ACCESSORIO CHE NON VIENE CALCOLATO
        'IN FASE DI VALORIZZAZIONE DEL PREVENTIVO).

        'PASSO 1: SE LA TARIFFA HA UNA TARIFFA MADRE ASSOCIATA RECUPERO L'id_tariffe_righe PER LA MADRE
        'Dim id_tariffe_righe_madre As String = getIdTariffeRigheMadre(id_tariffe_righe, data_pick_up, provenienza_per_data)

        'PASSO 2: RECUPERO GLI ID DELLE CONDIZIONI E DEL TEMPO KM ASSOCIATO ALLE RIGHE DELLA TARIFFA FIGLIA E, SE E' STATO TROVATA, DELLA TARIFFA MADRE
        Dim id_condizione_figlia As String = getIdCondizione(id_tariffe_righe)
        Dim id_condizione_madre As String = getIdCondizioneMadre(id_tariffe_righe)
        Dim id_tempo_km_figlia As String = getIdTempoKm(id_tariffe_righe)
        Dim id_tempo_km_madre As String = "0"

        'PASSO 3: SI CERCA IL COSTO DELL'ACCESSORIO  
        Dim elementi_prepagati As New Collection
        If prepagato And giorni_prepagati_x_modifica > 0 Then
            Dim ele As String = id_accessorio
            elementi_prepagati.Add(ele, ele)
        End If
        Dim trovato As Boolean = calcola_accessorio_extra_o_obbligatorio(id_accessorio, id_gruppo, giorni_noleggio, giorni_prepagati_x_modifica, prepagato, elementi_prepagati, giorni_restanti_x_nolo_in_corso, data_aggiunta_x_nolo_in_corso, stazione_pick_up, stazione_drop_off, id_condizione_figlia, id_condizione_madre, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, False)

        'PASSO 4: AGGIORNO IL COSTO PER IVA/ONERE/TOTALE (SE E' STATO TROVATO IL COSTO)
        If trovato Then
            Dim primo_calcolo_prepagato As Boolean = False
            If prepagato And giorni_prepagati_x_modifica = 0 Then
                primo_calcolo_prepagato = True
            ElseIf prepagato And giorni_prepagati_x_modifica > 0 Then
                'COPIO I COSTI PREPAGATI DAL CALCOLO PRECEDENTE PER IL SINGOLO ACCESSORIO - NECESSARIO PER IL CALCOLO DELL'EVENTUALE SCONTO
                funzioni_comuni.riporta_costi_prepagati(id_prenotazione, id_contratto, num_calcolo, id_gruppo, id_accessorio, "", False)
            End If
            calcolo_iva_e_totale_singolo_accessorio(stazione_pick_up, sconto, id_gruppo, id_accessorio, "", id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, primo_calcolo_prepagato, tipo_commissione, percentuale_commissionabile, id_fonte_commissionabile)
            If primo_calcolo_prepagato Then
                'PRIMO CALCOLO - SALVO I COSTI DEL PREPAGATO
                prepagato_memorizza_costi_prepagati_x_fattura(id_prenotazione, id_ribaltamento, id_contratto, id_gruppo, num_calcolo, id_accessorio)
            End If
        End If
    End Sub

    Public Function getQueryElementiExtra(ByVal id_tariffe_righe As String, ByVal escludi_accessori_non_vendibili_nolo_in_corso As Boolean) As String
        'PASSO 1: SE LA TARIFFA HA UNA TARIFFA MADRE ASSOCIATA RECUPERO L'id_tariffe_righe PER LA MADRE
        'Dim id_tariffe_righe_madre As String = getIdTariffeRigheMadre(id_tariffe_righe, data_pick_up, provenienza_per_data)

        'PASSO 2: RECUPERO L'ID DELLE CONDIZIONI ASSOCIATE ALLE RIGHE DELLA TARIFFA FIGLIA E, SE E' STATO TROVATA, DELLA TARIFFA MADRE
        Dim id_condizione_figlia As String = getIdCondizione(id_tariffe_righe)
        Dim id_condizione_madre As String = getIdCondizioneMadre(id_tariffe_righe)

        'DEVONO ESSERE SELEZIONATI GLI ACCESSORI (ELEMENTI NON OBBLIGATORI E NON INCLUSI) CHE SONO SETTATI COME DA NON VALORIZZARE
        'SE E' STATO SPECIFICATO ESCLUDO GLI ACCESSORI NON VENDIBILI NOLO IN CORSO

        Dim condizione As String = ""
        If escludi_accessori_non_vendibili_nolo_in_corso Then
            condizione = " AND (condizioni_elementi.acquistabile_nolo_in_corso='1')"
        End If

        getQueryElementiExtra = "SELECT DISTINCT condizioni_elementi.id, condizioni_elementi.descrizione FROM condizioni_righe WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON condizioni_righe.id_elemento=condizioni_elementi.id WHERE (condizioni_righe.id_condizione='" & id_condizione_figlia & "' OR condizioni_righe.id_condizione='" & id_condizione_madre & "') AND (condizioni_elementi.valorizza='0') AND (condizioni_righe.obbligatorio='0' AND condizioni_righe.id_a_carico_di<>'" & Costanti.id_accessorio_incluso & "')" & condizione
        getQueryElementiExtra += " ORDER BY descrizione"
    End Function

    Public Sub aggiungi_arrotondamento_prepagato(ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal id_gruppo As String, ByVal num_calcolo As String, ByVal differenza_costo As Double)
        Dim sqlStr As String
        Try
            Dim tabella As String
            Dim id_da_cercare As String
            If id_ribaltamento <> "" Then
                tabella = "ribaltamento_costi"
                id_da_cercare = id_ribaltamento
            ElseIf id_prenotazione <> "" Then
                tabella = "prenotazioni_costi"
                id_da_cercare = id_prenotazione
            ElseIf id_contratto <> "" Then
                tabella = "contratti_costi"
                id_da_cercare = id_contratto
            End If

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            sqlStr = "SELECT TOP 1 condizioni_elementi.id, condizioni_elementi.descrizione, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva FROM condizioni_elementi WITH(NOLOCK) " &
                "INNER JOIN aliquote_iva WITH(NOLOCK) ON aliquote_iva.id=condizioni_elementi.id_aliquota_iva " &
                "WHERE tipologia='ARR_PREP' AND " & Replace(differenza_costo, ",", ".") & " <= ISNULL(soglia_arrotondamento_prepagato,0)"


            Dim id_ribaltamento_costi As String = "0"
            Dim id_elemento As String = "0"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader

            If Rs.Read() Then
                'ESISTE L'ELEMENTO E' LA DIFFERENZA COSTO E' INFERIORE O UGUALE ALLA SOGLIA - E' POSSIBILE SALVARE LA RIGA
                salvaRigaCalcolo("", id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id"), "", Rs("descrizione"), differenza_costo, "NULL", Rs("iva"), Rs("codice_iva") & "", "True", False, False, False, Costanti.id_a_carico_del_cliente, Costanti.id_valorizza_nel_contratto, "True", "0", "3", "NULL", "0", "", "True", "1", "", False, True)

                calcolo_iva_e_totale_singolo_accessorio("0", 0, id_gruppo, Rs("id"), "", "", id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, True, "", "", "")

                id_elemento = Rs("id")
            End If

            'L'INTERO COSTO E' PREPAGATO - LO IMPOSTO

            Dbc.Close()
            Dbc.Open()

            sqlStr = "UPDATE " & tabella & " SET imponibile_scontato_prepagato=imponibile_scontato, iva_imponibile_scontato_prepagato=iva_imponibile_scontato," &
                "imponibile_onere_prepagato=imponibile_onere, iva_onere_prepagato=iva_onere WHERE id_documento=" & id_da_cercare & " AND num_calcolo=" & num_calcolo &
                " AND id_gruppo=" & id_gruppo & " AND id_elemento=" & id_elemento

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error  aggiungi_arrotondamento_prepagato : " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try


    End Sub

    Public Sub copia_arrotondamento_prepagato(ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal id_gruppo As String, ByVal num_calcolo As String, ByVal differenza_costo As Double)
        Dim sqlStr As String
        Try
            'DA CALCOLO PRECEDENTE - NON DEVE ESSERE IMPOSTATO COME PREPAGATO!!! (LO E' GIA') NON CONTROLLO PIU'LA SOGLIA PERCHE' COMUNQUE ERA GIA' STATO AGGIUNTO (E LA SOGLIA POTREBBE ESSERE VARIATA)
            Dim tabella As String
            Dim id_da_cercare As String
            If id_ribaltamento <> "" Then
                tabella = "ribaltamento_costi"
                id_da_cercare = id_ribaltamento
            ElseIf id_prenotazione <> "" Then
                tabella = "prenotazioni_costi"
                id_da_cercare = id_prenotazione
            ElseIf id_contratto <> "" Then
                tabella = "contratti_costi"
                id_da_cercare = id_contratto
            End If

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            sqlStr = "SELECT TOP 1 condizioni_elementi.id, condizioni_elementi.descrizione, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva FROM condizioni_elementi WITH(NOLOCK) " &
                "INNER JOIN aliquote_iva WITH(NOLOCK) ON aliquote_iva.id=condizioni_elementi.id_aliquota_iva " &
                "WHERE tipologia='ARR_PREP'"


            Dim id_ribaltamento_costi As String = "0"
            Dim id_elemento As String = "0"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader

            If Rs.Read() Then
                'ESISTE L'ELEMENTO E' LA DIFFERENZA COSTO E' INFERIORE O UGUALE ALLA SOGLIA - E' POSSIBILE SALVARE LA RIGA
                salvaRigaCalcolo("", id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id"), "", Rs("descrizione"), differenza_costo, "NULL", Rs("iva"), Rs("codice_iva") & "", "True", False, False, False, Costanti.id_a_carico_del_cliente, Costanti.id_valorizza_nel_contratto, "True", "0", "3", "NULL", "0", "", "True", "1", "", False, True)

                calcolo_iva_e_totale_singolo_accessorio("0", 0, id_gruppo, Rs("id"), "", "", id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, False, "", "", "")

                id_elemento = Rs("id")
            End If

            'L'INTERO COSTO E' PREPAGATO - LO IMPOSTO

            Dbc.Close()
            Dbc.Open()

            sqlStr = "UPDATE " & tabella & " SET imponibile_scontato_prepagato=imponibile_scontato, iva_imponibile_scontato_prepagato=iva_imponibile_scontato," &
                "imponibile_onere_prepagato=imponibile_onere, iva_onere_prepagato=iva_onere WHERE id_documento=" & id_da_cercare & " AND num_calcolo=" & num_calcolo &
                " AND id_gruppo=" & id_gruppo & " AND id_elemento=" & id_elemento

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error copia_arrotondamento_prepagato  : " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Sub

    Public Sub addebita_km_extra(ByVal id_contratto As String, ByVal numCalcolo As String, ByVal km_percorsi As Integer, ByVal num_giorni As String, ByVal sconto As String, ByVal stazione_pick_up As String)
        'IL COSTO CHILOMETRICO PER I KM EXTRA E L'INFORMAZIOE PER SAPERE QUANTI KM SONO DA ADDEBITARE SONO NELLA RIGA DI condizioni_elementi CORRISPONDENTE ALL'ELEMENTO CHE 
        'HA TIPOLOGIA=KM_EXTRA - QUESTO ELEMENTO DA INFORMATIVA PASSA AD ESSERE UN COSTO OBBLIGATORIO
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "SELECT valore_costo, km_giorno_inclusi, id_elemento, id_gruppo FROM contratti_costi WITH(NOLOCK) " &
            "WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & numCalcolo & "' AND NOT km_giorno_inclusi IS NULL"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader

        If Rs.Read() Then
            Dim km_da_addebitare As Integer = km_percorsi - Rs("km_giorno_inclusi")
            Dim importo_da_addebitare As Double = km_da_addebitare * Rs("valore_costo")
            Dim id_accessorio As String = Rs("id_elemento")
            Dim id_gruppo As String = Rs("id_gruppo")

            Dbc.Close()
            Dbc.Open()

            If km_da_addebitare <= 0 Then
                'RESTA LA SOLA INFORMATIVA NEL CASO IN CUI NON CI SONO KM DA ADDEBITARE
                'sqlStr = "DELETE FROM contratti_costi " & _
                '    "WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & numCalcolo & "' AND NOT km_giorno_inclusi IS NULL"
                'Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                'Cmd.ExecuteNonQuery()
            Else
                'LA RIGA, CHE ERA INFORMATIVA, DIVENTA UN ELEMENTO OBBLIGATORIO 
                sqlStr = "UPDATE contratti_costi SET valore_costo='" & Replace(importo_da_addebitare, ",", ".") & "', selezionato='1'," &
                    "id_metodo_stampa='" & Costanti.id_valorizza_nel_contratto & "', obbligatorio='1', qta='" & km_da_addebitare & "', ordine_stampa='3' " &
                    "WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & numCalcolo & "' AND NOT km_giorno_inclusi IS NULL"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteNonQuery()

                calcolo_iva_e_totale_singolo_accessorio(stazione_pick_up, sconto, id_gruppo, id_accessorio, "", "", "", "", id_contratto, numCalcolo, False, "", "", "")
            End If


        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Function getIsBroker(ByVal id_tariffe_righe As String) As Boolean
        'RESTITUISCE true SE LA TARIFFA RIGA SI RIFERISCE AD UNA TARIFFA BROKER, false ALTRIMENTI
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT tariffe.is_broker_prepaid FROM tariffe_righe WITH(NOLOCK) INNER JOIN tariffe WITH(NOLOCK) ON tariffe_righe.id_tariffa=Tariffe.id WHERE tariffe_righe.id='" & id_tariffe_righe & "'", Dbc)
        getIsBroker = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getElementiPrepagati(ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal id_ribaltamento As String, ByVal num_calcolo As String, ByVal id_gruppo As String) As Collection
        Dim tabella As String
        Dim id_da_cercare As String
        If id_ribaltamento <> "" Then
            tabella = "ribaltamento_costi"
            id_da_cercare = id_ribaltamento
        ElseIf id_prenotazione <> "" Then
            tabella = "prenotazioni_costi"
            id_da_cercare = id_prenotazione
        ElseIf id_contratto <> "" Then
            tabella = "contratti_costi"
            id_da_cercare = id_contratto
        End If

        Dim elementi_prepagati As New Collection

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_elemento, num_elemento FROM " & tabella & " WHERE num_calcolo=" & num_calcolo - 1 & " AND id_documento=" & id_da_cercare & " AND prepagato=1", Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader

        Try                 'aggiunto 30.07.2022 salvo 

            Rs = Cmd.ExecuteReader

            Do While Rs.Read()
                Dim ele As String = ""       'modificato 30.07.2022 salvo - EX: Dim ele As String = Rs("id_elemento")   

                If Not IsDBNull(Rs("id_elemento")) Then                 'aggiunto 30.07.2022 salvo - NOTA: si è verificato un NULL
                    ele = Rs("id_elemento")                             'aggiunto 30.07.2022 salvo
                    If (Rs("num_elemento") & "") <> "" Then
                        ele = ele & "-" & Rs("num_elemento")
                    End If
                    elementi_prepagati.Add(ele, ele)
                End If
            Loop

        Catch ex As Exception
            HttpContext.Current.Response.Write("Errore: GetElementiPrepagati: " & ex.Message & "<br/>")  'aggiunto 30.07.2022 salvo 
        End Try

        getElementiPrepagati = elementi_prepagati

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function


    Public Sub calcolaTariffa_x_gruppo(ByVal stazione_pick_up As String, ByVal data_pick_up As String, ByVal ore_pick_up As Integer,
                                       ByVal minuti_pick_up As Integer, ByVal stazione_drop_off As String, ByVal id_tariffe_righe As String,
                                       ByVal id_gruppo As String, ByVal id_gruppo_da_prenotazione_x_modifica_con_rack As String,
                                       ByVal giorni_noleggio As Integer, ByVal giorni_prepagati_x_modifica As String, ByVal prepagata As String,
                                       ByVal giorni_noleggio_extra_rack As Integer, ByVal sconto As Double, ByVal tipo_sconto As String,
                                       ByVal sconto_web_prepagato_primo_calcolo As Double, ByVal sconto_su_rack As Double, ByVal eta_primo_guidatore As String,
                                       ByVal eta_secondo_guidatore As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String,
                                       ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String,
                                       ByVal id_ditta As String, ByVal id_fonte_commissionabile As String, ByVal commissione_percentuale As String,
                                       ByVal tipo_commissione As String, ByVal primo_calcolo_commissione As Boolean, ByVal giorni_commissioni_originale As String,
                                       Optional ByVal broker_a_carico_di As String = "",
                                       Optional tipoTariffa As String = "0", Optional descTariffa As String = "0", Optional data_drop_off As String = "0",
                                       Optional tipoCli As String = "0", Optional data_creazione As String = "", Optional max_sconto_new As String = "0",
                                       Optional DaARES As Boolean = True, Optional ggEXTRA As String = "0", Optional ValoreTariffaOri As String = "0", Optional SetTariffaOri As Boolean = False)
        'Optional DaARES = aggiunto x essere chiamato da WebService 04.02.2023 se passato da WebService deve essere False
        'Optional ggEXTRA = aggiunto per calcolare i gg extra in caso di modifica al periodo
        'Optional ValoreTariffaOri per passare il valore della tariffa originale prima del ricalcolo 23.02.2023


        'inserita ultima riga Optional x calcolo nuova tariffa periodi - salvo 06.12.2022 - 04.01.2023


        If tipoCli = "0" And broker_a_carico_di <> "" Then      'aggiunto salvo 14.12.2022
            tipoCli = broker_a_carico_di
        End If


        'id_gruppo_da_prenotazione_rack: deve essere passato l'id_gruppo da prenotazione UNICAMENTE quando in fase di contratti l'utente
        'chiede di cambiare gruppo E LA TARIFFA NON E' PIU' VENDIBILE. Deve già essere certo che non si tratti di un downsell. NON chiamare
        'questa funzione se si tratta di downsell (richiesta di gruppo meno costoso di quello in fase di prenotazione)..

        'FUNZIONAMENTO PREPAGATA.
        'PASSARE IL PARAMETRO GIORNI PREPAGATI SOLO PER UNA MODIFICA DI UNA PRENOTAZIONE GIA' IMPOSTATA COME PREPAGATA (passando prepagato a true)
        'SE SI PASSANO I GIORNI PREPAGATI (MODIFICA): IN QUESTO CASO GLI ELEMENTI SETTATI COME PREPAGATI NEL CALCOLO PRECEDENTE VERRANNO CALCOLATI UTILIZZANDO I GIORNI PREPAGATI 
        'ANCHE SE I GIORNI DI NOLEGGIO DIMINUISCONO. INOLTRE VENGONO AUTOMATICAMENTE AGGIUNTI GLI EVENTUALI COSTI PREPAGATI RIMOSSI COL NUOVO CALCOLO (ES: VAL - 2° GUIDATORE ECC...)
        'PASSARE PREPAGATA TRUE E GIORNI PREPAGATI 0 SIA IN FASE DI PRIMO CALCOLO SIA PER RIAGGIORNARE I COSTI PREPAGATI (IN CASO DI MODIFICA TARIFFA/RICALCOLO PER CORREZIONE ERRORI)
        Dim elementi_prepagati As New Collection

        If prepagata And giorni_prepagati_x_modifica > 0 Then
            'IN QUESTO CASO E' UN CALCOLO SUCCESSIVO: PER UNA PREPAGATA. Recupero dal calcolo precedente gli elementi prepagat
            elementi_prepagati = getElementiPrepagati(id_prenotazione, id_contratto, id_ribaltamento, num_calcolo, id_gruppo)
        End If

        Dim is_broker As Boolean = getIsBroker(id_tariffe_righe)
        'P'0ASSO 2: RECUPERO GLI ID DELLE CONDIZIONI E DEL TEMPO KM ASSOCIATO ALLE RIGHE DELLA TARIFFA FIGLIA E, SE E' STATO TROVATA, DELLA TARIFFA MADRE
        Dim id_condizione_figlia As String = getIdCondizione(id_tariffe_righe)
        Dim id_condizione_madre As String = getIdCondizioneMadre(id_tariffe_righe)
        Dim id_tempo_km_figlia As String = getIdTempoKm(id_tariffe_righe)
        Dim id_tempo_km_rack As String = ""

        'MODIFICA CONTRATTO: PASSANDO IN NUMERO DI GIORNI DI NOLEGGIO RACK A LIVELLO DI TEMPO+KM VERRA' AGGIUNTO IL COSTO DEI GIORNI EXTRA
        'CALCOLANDOLO SULLA TARIFFA RACK. SE IN CERTI CASI SI VUOLE UTILIZZARE LA TARIFFA ORIGINARIA BASTA PASSARE 0 PER IL PARAMENTRO 
        'GIORNI_NOLEGGIO_EXTRA_RACK.  IL TEMPO KM RACK SERVE ANCHE IN CASO DI UPSELL DI GRUPPO CON TARIFFA DI PRENOTAZIONE NON PIU' VENDIBILE
        'ATTENZIONE: I GIORNI DI NOLEGGIO DA PASSARE SONO QUELLI TOTALI IN OGNI CASO.
        If giorni_noleggio_extra_rack = 0 And id_gruppo_da_prenotazione_x_modifica_con_rack = "" Then
            id_tempo_km_rack = "0"
        Else
            id_tempo_km_rack = getIdTempoKmRack(id_tariffe_righe, id_contratto, id_prenotazione)


            If id_tempo_km_rack = "" Then
                ' ESEGUO IL CALCOLO CON LA TARIFFA ORIGINIARIA SE NON E' STATA SPECIFICATA ALCUNA RACK
                id_tempo_km_rack = "0"
                giorni_noleggio_extra_rack = 0
                id_gruppo_da_prenotazione_x_modifica_con_rack = ""
            End If
        End If

        'PASSO 3: RECUPERO IL COSTO DELLA TARIFFA DAL TEMPO-KM 
        'Scrive le righe in preventivi costi !! 18.01.22
        'Dim tipoCli As String = broker_a_carico_di 'eliminato perchè parametro passato  13.12.2022

        Dim idTariffa As String = id_tariffe_righe      'aggiunto salvo 29.12.2022
        descTariffa = funzioni_comuni_new.GetCodiceTariffa(idTariffa)

        'se in ricalcolo le stazioni pickup/dropoff date/ore non cambiano non ricalcola tempoKm salvo 24.02.2023
        calcola_tempo_km(giorni_noleggio, giorni_prepagati_x_modifica, prepagata, giorni_noleggio_extra_rack, sconto_su_rack, stazione_pick_up,
                         stazione_drop_off, id_tempo_km_figlia, id_tempo_km_rack, id_gruppo, id_gruppo_da_prenotazione_x_modifica_con_rack,
                         id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, broker_a_carico_di,
                         data_pick_up, data_drop_off, tipoCli, tipoTariffa, descTariffa, idTariffa, data_creazione, max_sconto_new,, ggEXTRA, ValoreTariffaOri, SetTariffaOri)


        'PASSO 4: CALCOLO DEL VAL SE NECESSARIO
        If stazione_pick_up <> stazione_drop_off Then
            calcolaVAL(giorni_noleggio, giorni_prepagati_x_modifica, prepagata, elementi_prepagati, stazione_pick_up, stazione_drop_off, id_condizione_figlia, id_condizione_madre, id_gruppo, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo)
        End If

        'PASSO 5: CALCOLO DELL'ELEMENTO JOUNG DRIVER (PRIMO GUIDATORE)
        calcola_supplemento_joung_driver("primo", id_gruppo, giorni_noleggio, giorni_prepagati_x_modifica, prepagata, elementi_prepagati, stazione_pick_up, stazione_drop_off, eta_primo_guidatore, eta_secondo_guidatore, id_condizione_figlia, id_condizione_madre, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo)

        'PASSO 6: CALCOLO DELL'ONERE (UNICO POSSIBILE ELEMENTO PERCENTUALE)
        calcolaOnere(giorni_noleggio, giorni_prepagati_x_modifica, prepagata, elementi_prepagati, stazione_pick_up, stazione_drop_off, id_condizione_figlia, id_condizione_madre, id_gruppo, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo)

        'PASSO 7: ANALISI DELLE CONDIZIONI
        analisi_condizioni(giorni_noleggio, giorni_prepagati_x_modifica, prepagata, elementi_prepagati, data_pick_up, ore_pick_up, minuti_pick_up, stazione_pick_up, stazione_drop_off, id_condizione_figlia, id_condizione_madre, id_gruppo, id_ditta, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo)

        'PASSO 8: AGGIUNGO TRA GLI ACCESSORI IL "PIENO CARBURANTE" CHE PERMETTE DI RIENTRARE SENZA IL PIENO - QUESTO COSTO DEVE ESSERE VENDUTO
        'SOLO IN FASE DI CONTRATTO
        aggiungi_accessorio_pieno_caburante(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo)

        'RIPORTO I COSTI PREPAGATI DAL CALCOLO PRECEDENTE NEL CASO DI PREPAGATA
        If prepagata And giorni_prepagati_x_modifica > 0 Then
            funzioni_comuni.riporta_costi_prepagati(id_prenotazione, id_contratto, num_calcolo, id_gruppo, "", "", False)
        End If

        If tipo_commissione = "2" And primo_calcolo_commissione Then
            calcola_commissioni_gia_pagate(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, id_gruppo, num_calcolo, commissione_percentuale, primo_calcolo_commissione)
        ElseIf tipo_commissione = "2" And Not primo_calcolo_commissione AndAlso giorni_noleggio >= CInt(giorni_commissioni_originale) Then
            'RIPORTO LE COMMISSIONI PREINCASSATE DALL'AGENZIA DI VIAGGIO DAL CALCOLO PRECEDENTE 
            riporta_commissioni_agenzia(id_prenotazione, id_contratto, num_calcolo, id_gruppo)
        ElseIf tipo_commissione = "2" And Not primo_calcolo_commissione AndAlso giorni_noleggio < CInt(giorni_commissioni_originale) Then
            'RICALCOLO LE COMMISSIONI NEL CASO IN CUI I GIORNI SIANO DIMINUTI RISPETTOA QUANTO PRENOTATO DALL'AGENZIA 
            calcola_commissioni_gia_pagate(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, id_gruppo, num_calcolo, commissione_percentuale, primo_calcolo_commissione)
        End If

        'PASSO 9: CALCOLO DELL'IVA - SCONTO - TOTALE
        '# qui devo passare il totale dell'importo dello sconto - salvo 18.01.2023


        calcolo_iva_e_totale(stazione_pick_up, id_tempo_km_figlia, id_condizione_figlia, id_condizione_madre, sconto, sconto_web_prepagato_primo_calcolo, tipo_sconto, id_gruppo, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, is_broker, tipo_commissione, primo_calcolo_commissione)

        'RIPORTO IL PREPAGATO PER LA RIGA TOTALE - QUESTO VIENE EFFETTUATO
        If prepagata And giorni_prepagati_x_modifica > 0 Then
            funzioni_comuni.riporta_costi_prepagati(id_prenotazione, id_contratto, num_calcolo, id_gruppo, "", "", True)
        End If

        'PASSO 10: SE IL GPS E' UN ACCESSORIO OBBLIGATORIO O INCLUSO DELLA TARIFFA DEVO AGGIUNGERE IN QUESTA FASE L'EVENTUALE VAL
        If stazione_pick_up <> stazione_drop_off Then
            If gps_obbligatorio_o_incluso(id_gruppo, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo) Then
                aggiungi_val_gps(stazione_pick_up, stazione_drop_off, id_gruppo, giorni_noleggio, giorni_prepagati_x_modifica, prepagata, elementi_prepagati, "", "", sconto, id_tariffe_righe, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_utente, tipo_commissione, commissione_percentuale, id_fonte_commissionabile)
            End If
        End If

        If prepagata And giorni_prepagati_x_modifica > 0 Then
            'IN QUESTO CASO E' UN CALCOLO SUCCESSIVO PER UNA PREPAGATA. DEVO AGGIUNGERE EVENTUALI ACCESSORI OBBLIGATORI RIMOSSI NEL CALCOLO ATTUALE MA PREPAGATI NEL CALCOLO 
            'PRECEDENTE
            aggiungi_accessori_obbligatori_prepagati_calcolo_precedente(id_prenotazione, id_contratto, num_calcolo, id_gruppo, stazione_pick_up, stazione_drop_off, giorni_noleggio, giorni_prepagati_x_modifica, sconto, id_tariffe_righe, id_utente)
        ElseIf prepagata And giorni_prepagati_x_modifica = 0 Then
            'IN QUESTO CASO E' IL PRIMO CALCOLO PER UNA PREPAGATA - PER I COSTI PREPAGATI SALVO IN CAMPI PARTICOLARI L'IMPORTO PREPAGATO - NECESSARIO PER LA FATTURAZIONE FINALE
            prepagato_memorizza_costi_prepagati_x_fattura(id_prenotazione, id_ribaltamento, id_contratto, id_gruppo, num_calcolo, "")
        End If

        If tipo_commissione = "1" Then
            'COMMISSIONE PER AGENZIA DI VIAGGIO - SI PAGA SUGLI ELEMENTI COMMISSIONABILI - VA RICALCOLATO OGNI VOLTA CHE SI VARIA LA TARIFFA
            aggiorna_commissioni_da_riconoscere(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, id_gruppo, num_calcolo, id_fonte_commissionabile, commissione_percentuale)
        End If





    End Sub



    Protected Sub calcola_commissioni_gia_pagate(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal id_gruppo As Integer, ByVal num_calcolo As Integer, ByVal commissione_percentuale As Double, ByVal primo_calcolo As Boolean)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim tabella As String
        Dim id_da_cercare As String
        If id_preventivo <> "" Then
            tabella = "preventivi_costi"
            id_da_cercare = id_preventivo
        ElseIf id_prenotazione <> "" Then
            tabella = "prenotazioni_costi"
            id_da_cercare = id_prenotazione
        ElseIf id_contratto <> "" Then
            tabella = "contratti_costi"
            id_da_cercare = id_contratto
        ElseIf id_ribaltamento <> "" Then
            tabella = "ribaltamento_costi"
            id_da_cercare = id_ribaltamento
        End If

        'RIMUOVO DAL VALORE COSTO LA PERCENTUALE COMMISIONABILE - DISTINGUO IL CASO DI IVA INCLUSA DA QUELLO DI IVA ESCLUSA
        'SE E' IL PRIMO CALCOLO SALVO ANCHE NEL CAMPO ORIGINALE - QUESTO SERVE IN QUANTO, SUCCESSIVAMENTE, SE I GIORNI EFFETTIVI SONO INFERIORI AI GIORNI PRENOTATI DALL'AGENZIA
        'LE COMMISSIONI VENGONO RICALCOLATE (MA A VIDEO RESTANO VISIBILI QUELLE ORIGINALI PREINCASSATE DALL'AGENZIA), MENTRE, SE I GIORNI AUMENTANO, LE COMMISSINI VENGONO 
        'SEMPRE RIPORTATE DAI CAMPI commissioni_imponibile_originale E commissioni_iva_originale

        'UPDATE NEL CASO DI IVA INCLUSA ------------------------------------------------------------------------------------------------------------------------------------
        Dim campi_primo_calcolo As String = ""
        If primo_calcolo Then
            campi_primo_calcolo = ", commissioni_imponibile_originale=((ISNULL(valore_costo,0)*" & Replace(commissione_percentuale, ",", ".") & "/100)*100/(100+aliquota_iva))," &
            "commissioni_iva_originale=((ISNULL(valore_costo,0)*" & Replace(commissione_percentuale, ",", ".") & "/100)*aliquota_iva/(100+aliquota_iva)) "
        End If

        Dim sqlStr As String = "UPDATE " & tabella & " SET commissioni_imponibile=((ISNULL(valore_costo,0)*" & Replace(commissione_percentuale, ",", ".") & "/100)*100/(100+aliquota_iva))," &
            "commissioni_iva=((ISNULL(valore_costo,0)*" & Replace(commissione_percentuale, ",", ".") & "/100)*aliquota_iva/(100+aliquota_iva)) " & campi_primo_calcolo & " " &
            "WHERE iva_inclusa='True' AND id_documento='" & id_da_cercare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento=" & Costanti.ID_tempo_km

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        'UPDATE NEL CASO DI IVA ESCLUSA --------------------------------------------------------------------------------------------------------------------------------------
        campi_primo_calcolo = ""
        If primo_calcolo Then
            campi_primo_calcolo = ", commissioni_imponibile_originale=(ISNULL(valore_costo,0)*" & Replace(commissione_percentuale, ",", ".") & "/100)," &
            "commissioni_iva_originale=(ISNULL(valore_costo,0)*" & Replace(commissione_percentuale, ",", ".") & "/100)*aliquota_iva/100 "
        End If

        sqlStr = "UPDATE " & tabella & " SET commissioni_imponibile=(ISNULL(valore_costo,0)*" & Replace(commissione_percentuale, ",", ".") & "/100)," &
            "commissioni_iva=(ISNULL(valore_costo,0)*" & Replace(commissione_percentuale, ",", ".") & "/100)*aliquota_iva/100" & campi_primo_calcolo & " " &
            "WHERE iva_inclusa='False' AND id_documento='" & id_da_cercare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento=" & Costanti.ID_tempo_km

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        If Not primo_calcolo Then
            'SE NON E' IL PRIMO CALCOLO RIPORTO I CAMPI commissioni_imponibile_originale E commissioni_iva_originale DAL CALCOLO PRECEDENTE
            'IN QUESTA CONDIZIONE QUESTO METODO VIENE RICHIAMATO NEL CASO IN CUI I GIORNI DI CALCOLO SONO INFERIORI AI GIORNI ORIGINARIAMENTE PAGATI ALL'AGENZIA DI VIAGGIO
            sqlStr = "UPDATE t1 SET commissioni_imponibile_originale=t2.commissioni_imponibile_originale, commissioni_iva_originale=t2.commissioni_iva_originale " &
            "FROM " & tabella & " AS t1 " &
            "INNER JOIN " & tabella & " AS t2 ON t1.id_documento=t2.id_documento AND t1.id_gruppo=t2.id_gruppo AND t1.id_elemento=t2.id_elemento  " &
            "WHERE t1.id_documento=" & id_da_cercare & " AND t1.num_calcolo=" & num_calcolo & " AND t2.num_calcolo=" & num_calcolo - 1 & " AND t1.id_gruppo=" & id_gruppo & " AND t1.id_elemento=" & Costanti.ID_tempo_km
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub aggiorna_commissioni_da_riconoscere(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal id_gruppo As Integer, ByVal num_calcolo As Integer, ByVal id_fonte_commissionabile As Integer, ByVal commissione_percentuale As Double)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim tabella As String
        Dim id_da_cercare As String
        If id_preventivo <> "" Then
            tabella = "preventivi_costi"
            id_da_cercare = id_preventivo
        ElseIf id_prenotazione <> "" Then
            tabella = "prenotazioni_costi"
            id_da_cercare = id_prenotazione
        ElseIf id_contratto <> "" Then
            tabella = "contratti_costi"
            id_da_cercare = id_contratto
        ElseIf id_ribaltamento <> "" Then
            tabella = "ribaltamento_costi"
            id_da_cercare = id_ribaltamento
        End If


        Dim sqlStr As String = "UPDATE " & tabella & " SET commissioni_imponibile_originale=ISNULL(imponibile_scontato,0)*" & Replace(CDbl(commissione_percentuale), ",", ".") & "/100," &
            "commissioni_iva_originale=ISNULL(iva_imponibile_scontato,0)*" & Replace(CDbl(commissione_percentuale), ",", ".") & "/100 WHERE id_documento=" & id_da_cercare & " AND num_calcolo=" & num_calcolo &
            " AND id_gruppo=" & id_gruppo & " AND selezionato='1' AND id_elemento IN (SELECT id_elemento_condizione FROM fonti_commissionabili_x_elementi WITH(NOLOCK) WHERE id_fonte_commissionabile=" & id_fonte_commissionabile & ")"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        'AGGIORNO IL TOTALE CON LA SOMMA DELLE COMMISSIONI DEI SINGOLI ELEMENTI
        sqlStr = "UPDATE " & tabella & " SET commissioni_imponibile_originale=(SELECT SUM(ISNULL(commissioni_imponibile_originale,0)) FROM " & tabella & " WITH(NOLOCK) " &
            "WHERE id_documento=" & id_da_cercare & " AND num_calcolo=" & num_calcolo & " AND id_gruppo=" & id_gruppo & " AND selezionato='1' AND nome_costo<>'" & Replace(Costanti.testo_elemento_totale, "'", "''") & "'), " &
            "commissioni_iva_originale=(SELECT SUM(ISNULL(commissioni_iva_originale,0)) FROM " & tabella & " WITH(NOLOCK) " &
            "WHERE id_documento=" & id_da_cercare & " AND num_calcolo=" & num_calcolo & " AND id_gruppo=" & id_gruppo & " AND selezionato='1' AND nome_costo<>'" & Replace(Costanti.testo_elemento_totale, "'", "''") & "') " &
            "WHERE id_documento=" & id_da_cercare & " AND num_calcolo=" & num_calcolo & " AND id_gruppo=" & id_gruppo & " AND nome_costo='" & Replace(Costanti.testo_elemento_totale, "'", "''") & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()


        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    'Protected Sub calcola_commissioni_gia_pagate(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal id_gruppo As Integer, ByVal num_calcolo As Integer, ByVal commissione_percentuale As Double)
    '    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '    Dbc.Open()

    '    Dim tabella As String
    '    Dim id_da_cercare As String
    '    If id_preventivo <> "" Then
    '        tabella = "preventivi_costi"
    '        id_da_cercare = id_preventivo
    '    ElseIf id_prenotazione <> "" Then
    '        tabella = "prenotazioni_costi"
    '        id_da_cercare = id_prenotazione
    '    ElseIf id_contratto <> "" Then
    '        tabella = "contratti_costi"
    '        id_da_cercare = id_contratto
    '    ElseIf id_ribaltamento <> "" Then
    '        tabella = "ribaltamento_costi"
    '        id_da_cercare = id_ribaltamento
    '    End If

    '    


    '    'CALCOLO DELLE COMMISSIONI - TEMPO KM
    '    Dim sqlStr As String = "UPDATE " & tabella & " SET commissioni_imponibile=ISNULL(imponibile_scontato,0)*" & commissione_percentuale & "/100," & _
    '        "commissioni_iva=ISNULL(iva_imponibile_scontato,0)*" & commissione_percentuale & "/100, " & _
    '        "imponibile_scontato=ISNULL(imponibile_scontato,0) - ISNULL(imponibile_scontato,0)*" & commissione_percentuale & "/100," & _
    '        "imponibile= ISNULL(imponibile,0) - ISNULL(imponibile,0)*" & commissione_percentuale & "/100," & _
    '        "iva_imponibile_scontato=ISNULL(iva_imponibile_scontato,0) - ISNULL(iva_imponibile_scontato,0)*" & commissione_percentuale & "/100," & _
    '        "iva_imponibile=ISNULL(iva_imponibile,0) - ISNULL(iva_imponibile,0)*" & commissione_percentuale & "/100, " & _
    '        "imponibile_onere=ISNULL(imponibile_onere,0) - ISNULL(imponibile_onere,0)*" & commissione_percentuale & "/100, " & _
    '        "iva_onere=ISNULL(iva_onere,0) - (ISNULL(imponibile_onere,0)*" & commissione_percentuale & "/100)*aliquota_iva/100 " & _
    '        "WHERE id_documento=" & id_da_cercare & " AND num_calcolo=" & num_calcolo & " AND id_gruppo=" & id_gruppo & " AND id_elemento=" & Costanti.ID_tempo_km

    '    Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '    Cmd.ExecuteNonQuery()

    '    'AGGIORNAMENTO DEL TOTALE - PER PRIMA COSA RECUPERO I VALORI DI COMMISSIONI E COMMISSIONI IMPONIBILE (INSIEME ALL'ALIQUOTA IVA UTILIZZATA) DEL TEMPO KM E QUINDI LI 
    '    'RIMUOVO DAL TOTALE
    '    sqlStr = "SELECT ISNULL(commissioni_imponibile,0) As commissioni_imponibile, ISNULL(commissioni_onere,0) As commissioni_onere, aliquota_iva FROM " & tabella & " " & _
    '        "WHERE id_documento=" & id_da_cercare & " AND num_calcolo=" & num_calcolo & " AND id_gruppo=" & id_gruppo & " AND id_elemento=" & Costanti.ID_tempo_km
    '    Dim Rs As Data.SqlClient.SqlDataReader
    '    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '    Rs = Cmd.ExecuteReader()

    '    Dim commissioni_imponibile As Double = 0
    '    Dim commissioni_onere As Double = 0
    '    Dim aliquota_iva As Double = 0


    '    Do While Rs.Read()
    '        commissioni_imponibile = Rs("commissioni_imponibile")
    '        commissioni_onere = Rs("commissioni_onere")
    '        aliquota_iva = Rs("aliquota_iva")
    '    Loop

    '    Rs.Close()
    '    Rs = Nothing
    '    Dbc.Close()
    '    Dbc.Open()

    '    'AGGIORNO IL TOTALE (NEL TOTALE IMPONIBILE, IMPONIBILE_SCONTATO E IVA, IVA_SCONTATA SONO UGUALI)
    '    sqlStr = "UPDATE " & tabella & " SET imponibile=ISNULL(imponibile,0) - " & Replace(commissioni_imponibile, ",", ".") & "," & _
    '        "imponibile_scontato=ISNULL(imponibile_scontato,0) - " & Replace(commissioni_imponibile, ",", ".") & "," & _
    '        "iva_imponibile=ISNULL(iva_imponibile,0) - " & Replace(commissioni_imponibile, ",", ".") & "*" & Replace(aliquota_iva, ",", ".") & "/100," & _
    '        "iva_imponibile_scontato=ISNULL(iva_imponibile_scontato,0) - " & Replace(commissioni_imponibile, ",", ".") & "*" & Replace(aliquota_iva, ",", ".") & "/100," & _
    '        "imponibile_onere=ISNULL(imponibile_onere,0)-" & Replace(commissioni_onere, ",", ".") & "," & _
    '        "iva_onere=ISNULL(iva_onere,0)-" & Replace(commissioni_onere, ",", ".") & "*" & aliquota_iva & "/100 " & _
    '        "WHERE id_documento=" & id_da_cercare & " AND num_calcolo=" & num_calcolo & " AND id_gruppo=" & id_gruppo & " AND nome_costo='" & Replace(Costanti.testo_elemento_totale, "'", "''") & "'"
    '    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '    Cmd.ExecuteNonQuery()

    '    'AGGIORNO L'ELEMENTO ONERE (ANCHE IN QUESTO CASO IMPONIBILE, IMPONIBILE_SCONTATO E IVA, IVA_SCONTATA SONO UGUALI)
    '    sqlStr = "UPDATE " & tabella & " SET imponibile=ISNULL(imponibile,0) - " & Replace(commissioni_onere, ",", ".") & "," & _
    '        "imponibile_scontato=ISNULL(imponibile_scontato,0) - " & Replace(commissioni_onere, ",", ".") & "," & _
    '        "iva_imponibile=ISNULL(iva_imponibile,0) - " & Replace(commissioni_onere, ",", ".") & "*" & Replace(aliquota_iva, ",", ".") & "/100," & _
    '        "iva_imponibile_scontato=ISNULL(iva_imponibile_scontato,0) - " & Replace(commissioni_onere, ",", ".") & "*" & Replace(aliquota_iva, ",", ".") & "/100 " & _
    '        "WHERE id_documento=" & id_da_cercare & " AND num_calcolo=" & num_calcolo & " AND id_gruppo=" & id_gruppo & " AND NOT valore_percentuale IS NULL"
    '    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '    Cmd.ExecuteNonQuery()

    '    Cmd.Dispose()
    '    Cmd = Nothing
    '    Dbc.Close()
    '    Dbc.Dispose()
    '    Dbc = Nothing
    'End Sub

    'MARCO YOUNG 2
    Public Sub prepagato_memorizza_costi_prepagati_x_fattura(ByVal id_prenotazione As String, ByVal id_ribaltamento As String, ByVal id_contratto As String, ByVal id_gruppo As Integer, ByVal num_calcolo As Integer, ByVal id_elemento As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim tabella As String
        Dim id_da_cercare As String

        If id_prenotazione <> "" Then
            tabella = "prenotazioni_costi"
            id_da_cercare = id_prenotazione
        ElseIf id_contratto <> "" Then
            tabella = "contratti_costi"
            id_da_cercare = id_contratto
        ElseIf id_ribaltamento <> "" Then
            tabella = "ribaltamento_costi"
            id_da_cercare = id_ribaltamento
        End If

        Dim condizione_riga As String = ""
        If id_elemento <> "" Then
            condizione_riga = " AND id_elemento=" & id_elemento
        End If



        Dim sqlStr As String = "UPDATE " & tabella & " SET imponibile_scontato_prepagato=imponibile_scontato, iva_imponibile_scontato_prepagato=iva_imponibile_scontato," &
            "imponibile_onere_prepagato=imponibile_onere, iva_onere_prepagato=iva_onere WHERE id_documento=" & id_da_cercare & " AND num_calcolo=" & num_calcolo &
            " AND id_gruppo=" & id_gruppo & " AND (prepagato='1' OR nome_costo='" & Replace(Costanti.testo_elemento_totale, "'", "''") & "') " & condizione_riga


        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Sub aggiungi_accessori_obbligatori_prepagati_calcolo_precedente(ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As Integer, ByVal id_gruppo As String, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal giorni As Integer, ByVal giorni_prepagati As Integer, ByVal sconto As Double, ByVal id_tariffe_righe As Integer, ByVal id_utente As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim tabella As String
        Dim id_da_cercare As String

        If id_prenotazione <> "" Then
            tabella = "prenotazioni_costi"
            id_da_cercare = id_prenotazione
        ElseIf id_contratto <> "" Then
            tabella = "contratti_costi"
            id_da_cercare = id_contratto
        End If

        Dim sqlStr As String = "SELECT " & tabella & ".id_elemento, " & tabella & ".num_elemento, condizioni_elementi.tipologia, valore_costo FROM " & tabella & " WITH(NOLOCK) " &
            " INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id " &
            " WHERE " & tabella & ".id_documento=" & id_da_cercare &
            " AND " & tabella & ".num_calcolo=" & num_calcolo - 1 & " AND id_a_carico_di <> " & Costanti.id_accessorio_incluso &
            " AND obbligatorio='1' AND id_metodo_stampa<>" & Costanti.id_stampa_informativa_con_valore & " AND id_metodo_stampa<>" & Costanti.id_stampa_informativa_senza_valore &
            " AND NOT id_elemento IS NULL AND prepagato='1' " &
            " AND id_elemento + '-' + ISNULL(num_elemento,'') NOT IN (SELECT " & tabella & ".id_elemento + '-' + ISNULL(num_elemento,'') FROM " & tabella & " WHERE " & tabella & ".id_documento=" & id_da_cercare &
            " AND " & tabella & ".num_calcolo=" & num_calcolo & " AND id_a_carico_di <> " & Costanti.id_accessorio_incluso &
            " AND obbligatorio='1' AND id_metodo_stampa<>" & Costanti.id_stampa_informativa_con_valore & " AND id_metodo_stampa<>" & Costanti.id_stampa_informativa_senza_valore & " AND NOT id_elemento IS NULL)"

        'HttpContext.Current.Trace.Write(sqlStr)

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Do While Rs.Read()
            'calcola_supplemento_joung_driver
            'L'ACCESSORIO IN QUESTO CASO VIENE AGGIUNTO PERCHE' PREPAGATO, ALTRIMENTI NON SAREBBE STARTO AGGIUNTO - QUINDI, IN QUESTO CASO, IL CALCOLO VIENE EFFETTUATO
            'SOLO PER I GIORNI PREPAGATI, SENZA CONSIDERARE EVENTUALI ESTENSIONI
            If Rs("tipologia") = "JOUNG" Then

                If Rs("num_elemento") = "1" Then
                    calcola_costo_joung_driver_primo_guidatore(Rs("id_elemento"), stazione_pick_up, stazione_drop_off, 0, 0, id_gruppo, giorni_prepagati, giorni_prepagati, True, sconto, id_tariffe_righe, "", "", id_prenotazione, id_contratto, num_calcolo, id_utente, True, "", "", "")
                ElseIf Rs("num_elemento") = "2" Then
                    'MARCO - SE SI ABILITA QUESTA RIGA NON VERRANNO CALCOLATI I GIORNI EXTRA PER LO YOUNG DRIVER PREPAGATO, PERCHE' IN REALTA' L'ACCESSORIO VIENE AGGIUNTO
                    'DOPO CHE QUESTA FUNZIONE VIENE CHIAMATA. PURTROPPO QUESTA CORREZIONE FA SI CHE SE SI RIMUOVE LO YOUNG DRIVER SECONDO GUIDATORE IL COSTO NON VENGA
                    'AGGINTO AL PREPAGATO. TUTTAVIA IL COSTO PREPAGATO SU CONTRATTO VIENE SEMPRE MOSTRATO CORRETTO, CON L'ACCESSORIO CHE PERO' SCOMPARIRA' DALLA LISTA. 
                    'calcola_costo_joung_driver_secondo_guidatore(Rs("id_elemento"), stazione_pick_up, stazione_drop_off, 0, 0, id_gruppo, giorni_prepagati, giorni_prepagati, True, sconto, id_tariffe_righe, "", "", id_prenotazione, id_contratto, num_calcolo, id_utente, True, "", "", "")
                End If
            ElseIf Rs("tipologia") = "VAL" Then
                aggiungi_accessorio_obbligatorio(Rs("id_elemento"), stazione_pick_up, stazione_drop_off, id_gruppo, giorni_prepagati, giorni_prepagati, True, Nothing, "", "", sconto, id_tariffe_righe, "", "", id_prenotazione, id_contratto, num_calcolo, id_utente, True, "", "", "")
            ElseIf Rs("tipologia") = "ARR_PREP" Then
                copia_arrotondamento_prepagato("", id_prenotazione, id_contratto, id_gruppo, num_calcolo, Rs("valore_costo"))
            Else
                aggiungi_accessorio_obbligatorio(Rs("id_elemento"), stazione_pick_up, stazione_drop_off, id_gruppo, giorni_prepagati, giorni_prepagati, True, Nothing, "", "", sconto, id_tariffe_righe, "", "", id_prenotazione, id_contratto, num_calcolo, id_utente, False, "", "", "")
            End If
        Loop

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Protected Function gps_obbligatorio_o_incluso(ByVal id_gruppo As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim tabella As String
        Dim id_da_cercare As String
        If id_preventivo <> "" Then
            tabella = "preventivi_costi"
            id_da_cercare = id_preventivo
        ElseIf id_ribaltamento <> "" Then
            tabella = "ribaltamento_costi"
            id_da_cercare = id_ribaltamento
        ElseIf id_prenotazione <> "" Then
            tabella = "prenotazioni_costi"
            id_da_cercare = id_prenotazione
        ElseIf id_contratto <> "" Then
            tabella = "contratti_costi"
            id_da_cercare = id_contratto
        End If

        Dim sqlStr As String = "SELECT " & tabella & ".id FROM " & tabella & " WITH(NOLOCK) " &
            "INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id " &
            "WHERE condizioni_elementi.is_gps='1' " &
            "AND ((" & tabella & ".obbligatorio='1' AND " & tabella & ".id_a_carico_di='" & Costanti.id_a_carico_del_cliente & "' AND " & tabella & ".id_metodo_stampa='" & Costanti.id_valorizza_nel_contratto & "') " &
            "OR (" & tabella & ".id_a_carico_di='" & Costanti.id_accessorio_incluso & "')) AND " & tabella & ".id_documento='" & id_da_cercare & "' " &
            "AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "'"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""
        If test <> "" Then
            gps_obbligatorio_o_incluso = True
        Else
            gps_obbligatorio_o_incluso = False
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Function calcola_accessorio_extra_o_obbligatorio(ByVal id_accessorio As String, ByVal id_gruppo As String, ByVal giorni_noleggio As Integer, ByVal giorni_prepagati As Integer, ByVal prepagata As Boolean, ByVal elementi_prepagati As Collection, ByVal giorni_restanti_x_nolo_in_corso As String, ByVal data_aggiunta_x_nolo_in_corso As String, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal id_condizione_figlia As String, ByVal id_condizione_madre As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal accessorio_obbligatorio As Boolean, Optional ByVal accessorio_val As Boolean = False) As Boolean
        'DEVO AGGIUNGERE IL COSTO DELL'ELEMENTO EXTRA SCELTO (O DELL'ELEMENTO OBBLIGATORIO, SE IL RELATIVO PARAMETRO E' TRUE)
        'RESTITUISCE true SE IL COSTO VIENE TROVATO (O DEVE ESSER CALCOLATO), false ALTRIMENTI

        'PASSO 1: CONTROLLO NELLA TABELLA STAZIONI QUALE ONERE DEVE ESSERE PAGATO
        Dim tabella As String
        Dim id_da_cercare As String
        If id_preventivo <> "" Then
            tabella = "preventivi_costi"
            id_da_cercare = id_preventivo
        ElseIf id_ribaltamento <> "" Then
            tabella = "ribaltamento_costi"
            id_da_cercare = id_ribaltamento
        ElseIf id_prenotazione <> "" Then
            tabella = "prenotazioni_costi"
            id_da_cercare = id_prenotazione
        ElseIf id_contratto <> "" Then
            tabella = "contratti_costi"
            id_da_cercare = id_contratto
        End If

        Dim sqlStr As String
        Dim valore_trovato As Boolean = False
        Dim costo As String
        Dim percentuale As String
        Dim id_unita_misura As String
        Dim packed As String
        Dim qta As String

        Dim extra_od_obbligatorio As String
        Dim ordine_stampa As Integer

        If accessorio_obbligatorio Then
            extra_od_obbligatorio = "1"
            ordine_stampa = "3"
        Else
            extra_od_obbligatorio = "0"
            ordine_stampa = "4"
        End If

        'MEMORIZZO IL NUMERO DI GIORNI DA CALCOLARE
        Dim giorni_da_calcolare As String
        Dim giorni_nolo As Integer

        If giorni_restanti_x_nolo_in_corso <> "" Then
            'NEL CASO DI MODIFICA A NOLO IN CORSO, SE L'ACCESSORIO HA UN COSTO GIORNALIERO, VERRA' CALCOLATO IL PREZZO PER I GIORNI RESTANTI
            giorni_da_calcolare = giorni_restanti_x_nolo_in_corso
            giorni_nolo = giorni_noleggio
        Else
            giorni_da_calcolare = giorni_noleggio
            giorni_nolo = giorni_noleggio

            If prepagata AndAlso giorni_prepagati > giorni_noleggio AndAlso Not elementi_prepagati Is Nothing AndAlso elementi_prepagati.Contains(id_accessorio) Then
                giorni_nolo = giorni_prepagati
                giorni_da_calcolare = giorni_prepagati
            End If
        End If
        'PER PRIMA COSA CONTROLLO CHE L'ACCESSORIO NON SIA GIA' STATO AGGIUNTO
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM " & tabella & " WITH(NOLOCK) WHERE id_documento='" & id_da_cercare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_accessorio & "' AND selezionato='1'", Dbc)
        Dim gia_aggiunto As String = Cmd.ExecuteScalar & ""
        'PASSO 2: CERCO NELLA FIGLIA E NELLA MADRE IL COSTO DELL'ELEMENTO SCELTO - CERCO UNICAMENTE TRA GLI ELEMENTI NON OBBLIGATORI 
        '(INFATTI ANCHE SE L'ELEMENTO E' DA NON VALORIZZARE POTREBBE, PER UN GRUPPO TRA QUELLI SELEZIONATI IN FASE DI RICERCA, ESSERE
        'INCLUSO OPPURE OBBLIGATORI)
        If gia_aggiunto = "" Then
            For i = 1 To 2
                If (i = 1) Or (i = 2 And Not valore_trovato And id_condizione_madre <> "0") Then 'SE TROVO IL COSTO DELL'ELEMENTO JOUNG DRIVER PER LA CONDIZIONE FIGLIA NON LA CERCO PER LA MADRE
                    Dim colonna_elemento As String
                    If accessorio_val Then
                        colonna_elemento = "id_elemento_val"
                    Else
                        colonna_elemento = "id_elemento"
                    End If

                    If i = 1 Then
                        sqlStr = "SELECT condizioni.iva_inclusa,condizioni_righe." & colonna_elemento & ", condizioni_righe.id_metodo_stampa,condizioni_righe.obbligatorio,condizioni_elementi.descrizione As nome_elemento, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, condizioni_elementi.scontabile, condizioni_elementi.omaggiabile, condizioni_elementi.acquistabile_nolo_in_corso, condizioni_righe.id, condizioni_x_gruppi.id_gruppo, condizioni_righe.applicabilita_da," &
                        "condizioni_righe.applicabilita_a, condizioni_righe.id_a_carico_di,condizioni_righe.pac, condizioni_righe.id_unita_misura," &
                        "condizioni_righe.costo, condizioni_righe.tipo_costo FROM condizioni_righe WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON condizioni_elementi.id=condizioni_righe." & colonna_elemento & " INNER JOIN aliquote_iva WITH(NOLOCK) ON condizioni_elementi.id_aliquota_iva=aliquote_iva.id LEFT OUTER JOIN condizioni_x_gruppi WITH(NOLOCK) ON " &
                        "condizioni_x_gruppi.id_condizione=condizioni_righe.id INNER JOIN condizioni WITH(NOLOCK) ON condizioni_righe.id_condizione=condizioni.id WHERE (condizioni_righe.id_condizione='" & id_condizione_figlia & "') " &
                        "AND (condizioni_elementi.id='" & id_accessorio & "') AND condizioni_righe.obbligatorio='" & extra_od_obbligatorio & "' AND (condizioni_x_gruppi.id_gruppo='" & id_gruppo & "' OR " &
                        "condizioni_x_gruppi.id_gruppo IS NULL) ORDER BY condizioni_x_gruppi.id_gruppo DESC"
                    ElseIf i = 2 Then
                        sqlStr = "SELECT condizioni.iva_inclusa,condizioni_righe." & colonna_elemento & ", condizioni_righe.id_metodo_stampa, condizioni_righe.obbligatorio, condizioni_elementi.descrizione As nome_elemento, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva,  condizioni_elementi.scontabile,condizioni_elementi.omaggiabile, condizioni_elementi.acquistabile_nolo_in_corso, condizioni_righe.id, condizioni_x_gruppi.id_gruppo, condizioni_righe.applicabilita_da," &
                        "condizioni_righe.applicabilita_a, condizioni_righe.id_a_carico_di,condizioni_righe.pac, condizioni_righe.id_unita_misura," &
                        "condizioni_righe.costo, condizioni_righe.tipo_costo FROM condizioni_righe WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON condizioni_elementi.id=condizioni_righe." & colonna_elemento & " INNER JOIN aliquote_iva WITH(NOLOCK) ON condizioni_elementi.id_aliquota_iva=aliquote_iva.id LEFT OUTER JOIN condizioni_x_gruppi WITH(NOLOCK) ON " &
                        "condizioni_x_gruppi.id_condizione=condizioni_righe.id INNER JOIN condizioni WITH(NOLOCK) ON condizioni_righe.id_condizione=condizioni.id WHERE (condizioni_righe.id_condizione='" & id_condizione_madre & "') " &
                        "AND (condizioni_elementi.id='" & id_accessorio & "') AND condizioni_righe.obbligatorio='" & extra_od_obbligatorio & "' AND (condizioni_x_gruppi.id_gruppo='" & id_gruppo & "' OR " &
                        "condizioni_x_gruppi.id_gruppo IS NULL) ORDER BY condizioni_x_gruppi.id_gruppo DESC"
                    End If

                    'HttpContext.Current.Trace.Write(sqlStr)

                    Dbc.Close()
                    Dbc.Open()

                    Dim Rs As Data.SqlClient.SqlDataReader

                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                    Rs = Cmd.ExecuteReader()

                    Do While Rs.Read()
                        qta = "1" 'LA QUANTITA' E' SEMPRE 1 TRANNE NEL CASO DI COSTO GIORNALIERO

                        id_unita_misura = Rs("id_unita_misura")

                        packed = Rs("pac")


                        'CONTROLLO A MENO CHE NON HO TROVATO GIA' UN VALORE
                        'INFATTI DALLA QUERY AVRO' IN TESTA LE RIGHE DI CONDIZIONE CON ID_GRUPPO SPECIFICATO ED IN CODA QUELLE SENZA GRUPPO
                        If Not valore_trovato Then
                            If Rs("id_unita_misura") = "0" Then
                                'CASO 1: NESSUNA UNITA' DI MISURA: IN QUESTO CASO NON MI INTERESSA IL CASO "PACKED" - LA CONDIZIONE E' PER FORZA
                                'SENZA LIMITE (NON CONTROLLO I GIORNI DI NOLEGGIO O I KM)
                                If (Rs("id_gruppo") & "") = "" Or (Rs("id_gruppo") & "") = id_gruppo Then
                                    If Rs("tipo_costo") = "€" Then
                                        costo = Rs("costo")
                                        percentuale = "NULL"
                                    ElseIf Rs("tipo_costo") = "%" Then
                                        costo = "NULL"
                                        percentuale = Rs("costo")
                                    Else
                                        'CASO incluso senza valore
                                        costo = "0"
                                        percentuale = "NULL"
                                    End If
                                    salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs(colonna_elemento), "", Rs("nome_elemento"), costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, prepagata)
                                    valore_trovato = True
                                End If
                            ElseIf Rs("id_unita_misura") = Costanti.id_unita_misura_giorni Then
                                'CASO 2: UNITA' DI MISURA GIORNI. CONTROLLO SE IL LIMITE (GIORNI DI NOLEGGIO) E' RISPETTATO O SE LA RIGA 
                                'E' SENZA ALCUN LIMITE (SE E' SEMPRE APPLICABILE AVRO' 0 SIA IN applicabilita_da CHE IN pplicabilita_a)
                                'IN OGNI CASO SE LA CONDIZIONE E' PACKED VIENE PRESO IL VALORE IN TOTO, SE NON E' PACKED MOLTIPLICO IL VALORE
                                'PER I GIORNI DI NOLEGGIO
                                If (Rs("id_gruppo") & "") = "" Or (Rs("id_gruppo") & "") = id_gruppo Then
                                    'CONTROLLO SE C'E' UNA REGOLA DI APPLICABILITA'. 

                                    If (Rs("applicabilita_da") = "0" And Rs("applicabilita_a") = "0") Or (giorni_nolo >= Rs("applicabilita_da") And giorni_nolo <= Rs("applicabilita_a")) Or (giorni_nolo >= Rs("applicabilita_da") And Rs("applicabilita_a") = "999") Then
                                        'IN QUESTO CASO HO TROVATO LA CONDIZIONE. CONTROLLO SOLO SE E' PACKED O MENO
                                        If Rs("pac") = "True" Then
                                            If Rs("tipo_costo") = "€" Then
                                                costo = Rs("costo")
                                                percentuale = "NULL"
                                            ElseIf Rs("tipo_costo") = "%" Then
                                                costo = "NULL"
                                                percentuale = Rs("costo")
                                            End If
                                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs(colonna_elemento), "", Rs("nome_elemento"), costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, prepagata)
                                            valore_trovato = True
                                        Else
                                            qta = giorni_da_calcolare
                                            If Rs("tipo_costo") = "€" Then
                                                costo = CDbl(Rs("costo")) * giorni_da_calcolare
                                                percentuale = "NULL"
                                            ElseIf Rs("tipo_costo") = "%" Then
                                                costo = "NULL"
                                                percentuale = CDbl(Rs("costo")) * giorni_da_calcolare
                                            End If
                                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs(colonna_elemento), "", Rs("nome_elemento"), costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, data_aggiunta_x_nolo_in_corso, False, prepagata)
                                            valore_trovato = True
                                        End If
                                    End If
                                End If
                            ElseIf Rs("id_unita_misura") = Costanti.id_unita_misura_km_tra_stazioni Then
                                'CASO 3: UNITA' DI MISURA KM TRA LE STAZIONI. DEVO PRELEVARE IL DATO DALLA TABELLA
                                'stazioni_distanza. SE NON PRESENTE DEVO RESTITUIRE UN ERRORE.
                                If (Rs("id_gruppo") & "") = "" Or (Rs("id_gruppo") & "") = id_gruppo Then
                                    'CONTROLLO SE C'E' UNA REGOLA DI APPLICABILITA'. 
                                    Dim km_distanza_stazioni As Integer = getDistanzaStazioni(stazione_pick_up, stazione_drop_off)
                                    If (Rs("applicabilita_da") = "0" And Rs("applicabilita_a") = "0") Or (km_distanza_stazioni >= Rs("applicabilita_da") And km_distanza_stazioni <= Rs("applicabilita_a")) Or (km_distanza_stazioni >= Rs("applicabilita_da") And Rs("applicabilita_a") = "999") Then
                                        'IN QUESTO CASO HO TROVATO LA CONDIZIONE. CONTROLLO SOLO SE E' PACKED O MENO. NON RIESCO SICURAMENTE AD ENTRARE
                                        'SE LA CONDIZIONE HA DEI LIMITI DI APPLICABILITA' E LA DISTANZA STAZIONI NON E' STATA TROVATA SU DB (OTTENGO IL VALORE
                                        '-1). NEL CASO IN CUI LA CONDIZIONI E' SENZA LIMITI MA NON E' PACKED, QUINDI DIPENDE DALLA DISTANZA TRA
                                        'LE STAZIONI, NON RESTITUISCO ERRORE IN QUANTO SI PROVERA' A CERCARE PER LA TARIFFA MADRE.
                                        If Rs("pac") = "True" Then
                                            If Rs("tipo_costo") = "€" Then
                                                costo = Rs("costo")
                                                percentuale = "NULL"
                                            ElseIf Rs("tipo_costo") = "%" Then
                                                costo = "NULL"
                                                percentuale = Rs("costo")
                                            End If
                                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs(colonna_elemento), "", Rs("nome_elemento"), costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, prepagata)
                                            valore_trovato = True
                                        ElseIf Rs("pac") = "False" And km_distanza_stazioni > 0 Then
                                            If Rs("tipo_costo") = "€" Then
                                                costo = CDbl(Rs("costo")) * km_distanza_stazioni
                                                percentuale = "NULL"
                                            ElseIf Rs("tipo_costo") = "%" Then
                                                costo = "NULL"
                                                percentuale = CDbl(Rs("costo")) * km_distanza_stazioni
                                            End If
                                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs(colonna_elemento), "", Rs("nome_elemento"), costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, prepagata)
                                            valore_trovato = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Loop
                End If
            Next

            If valore_trovato Then
                calcola_accessorio_extra_o_obbligatorio = True
            Else
                calcola_accessorio_extra_o_obbligatorio = False
            End If
        Else
            calcola_accessorio_extra_o_obbligatorio = False 'IL COSTO E' STATO GIA' AGGIUNTO PER CUI NON FACCIO ESEGUIRE IL CALCOLO DELLA RIGA
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Function calcola_supplemento_joung_driver(ByVal guidatore As String, ByVal id_gruppo As String, ByVal giorni_noleggio As Integer, ByVal giorni_prepagati As Integer, ByVal prepagata As Boolean, ByVal elementi_prepagati As Collection, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal eta_primo_guidatore As String, ByVal eta_secondo_guidatore As String, ByVal id_condizione_figlia As String, ByVal id_condizione_madre As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, Optional ByVal aggiungi_x_prepagato As Boolean = False) As Boolean
        'DEVO AGGIUNGERE IL SUPPLEMENTO PER IL PRIMO GUIDATORE SE NON E' SODDISFATTA L'ETA' DEL PRIMO GUIDATORE
        'L'EVENTUALE SUPPLEMENTO PER IL SECONDO GUIDATORE VERRA' AGGIUNTO SE VIENE SELEZIONATO L'ACCESSORIO SECONDO GUIDATORE
        'RESTITUISCE true SE IL SUPPLEMENTO VIENE TROVATO (O DEVE ESSER CALCOLATO), false ALTRIMENTI
        'L'ACCESSORIO PUO' ESSERE AGGIUNTO A PRESCINDERE DAI CONTROLLI SULLA VENDIBILITA' IMPOSTANDO PREPAGATO=TRUE (NEL CASO DI ACCESSORIO PREPAGATO INFATTI L'ACCESSORIO VIENE
        'AGGIUNTO ANCHE SE IL GUIDATORE CAMBIA E LA SUA AGGIUNTA NON SAREBBE PIU' NECESSARIA)
        Dim gruppo_vendibile_eta As Integer = gruppo_vendibile_eta_guidatori(id_gruppo, eta_primo_guidatore, eta_secondo_guidatore, "", "", "", "", "", "", False)
        If ((gruppo_vendibile_eta = 1 Or gruppo_vendibile_eta = 3) And guidatore = "primo") Or ((gruppo_vendibile_eta = 2 Or gruppo_vendibile_eta = 3) And guidatore = "secondo") Or aggiungi_x_prepagato Then
            'PASSO 1: CONTROLLO NELLA TABELLA STAZIONI QUALE ONERE DEVE ESSERE PAGATO

            Dim sqlStr As String
            Dim valore_trovato As Boolean = False
            Dim costo As String
            Dim percentuale As String
            Dim num_elemento As String

            Dim id_unita_misura As String
            Dim packed As String
            Dim qta As String



            Dim nome_elemento As String
            If guidatore = "primo" Then
                nome_elemento = " (primo guid.)"
                num_elemento = "1"
            Else
                nome_elemento = " (secondo guid.)"
                num_elemento = "2"
            End If


            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

            'PASSO 2: CERCO NELLA FIGLIA E NELLA MADRE IL COSTO DELL'ELEMENTO JOUNG DRIVER
            For i = 1 To 2
                If (i = 1) Or (i = 2 And Not valore_trovato And id_condizione_madre <> "0") Then 'SE TROVO IL COSTO DELL'ELEMENTO JOUNG DRIVER PER LA CONDIZIONE FIGLIA NON LA CERCO PER LA MADRE
                    If i = 1 Then
                        sqlStr = "SELECT condizioni.iva_inclusa,condizioni_righe.id_elemento, condizioni_righe.id_metodo_stampa,condizioni_righe.obbligatorio,condizioni_elementi.descrizione As nome_elemento, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva,condizioni_elementi.scontabile, condizioni_elementi.omaggiabile, condizioni_elementi.acquistabile_nolo_in_corso, condizioni_righe.id, condizioni_x_gruppi.id_gruppo, condizioni_righe.applicabilita_da," &
                        "condizioni_righe.applicabilita_a, condizioni_righe.id_a_carico_di,condizioni_righe.pac, condizioni_righe.id_unita_misura," &
                        "condizioni_righe.costo, condizioni_righe.tipo_costo FROM condizioni_righe WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON condizioni_elementi.id=condizioni_righe.id_elemento INNER JOIN aliquote_iva WITH(NOLOCK) ON condizioni_elementi.id_aliquota_iva=aliquote_iva.id LEFT OUTER JOIN condizioni_x_gruppi WITH(NOLOCK) ON " &
                        "condizioni_x_gruppi.id_condizione=condizioni_righe.id INNER JOIN condizioni WITH(NOLOCK) ON condizioni_righe.id_condizione=condizioni.id WHERE (condizioni_righe.id_condizione='" & id_condizione_figlia & "') " &
                        "AND (condizioni_elementi.tipologia='JOUNG') AND (condizioni_x_gruppi.id_gruppo='" & id_gruppo & "' OR " &
                        "condizioni_x_gruppi.id_gruppo IS NULL) ORDER BY condizioni_x_gruppi.id_gruppo DESC"
                    ElseIf i = 2 Then
                        sqlStr = "SELECT condizioni.iva_inclusa,condizioni_righe.id_elemento, condizioni_righe.id_metodo_stampa, condizioni_righe.obbligatorio, condizioni_elementi.descrizione As nome_elemento, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, condizioni_elementi.scontabile,condizioni_elementi.omaggiabile, condizioni_elementi.acquistabile_nolo_in_corso, condizioni_righe.id, condizioni_x_gruppi.id_gruppo, condizioni_righe.applicabilita_da," &
                        "condizioni_righe.applicabilita_a, condizioni_righe.id_a_carico_di,condizioni_righe.pac, condizioni_righe.id_unita_misura," &
                        "condizioni_righe.costo, condizioni_righe.tipo_costo FROM condizioni_righe WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON condizioni_elementi.id=condizioni_righe.id_elemento INNER JOIN aliquote_iva WITH(NOLOCK) ON condizioni_elementi.id_aliquota_iva=aliquote_iva.id LEFT OUTER JOIN condizioni_x_gruppi WITH(NOLOCK) ON " &
                        "condizioni_x_gruppi.id_condizione=condizioni_righe.id INNER JOIN condizioni WITH(NOLOCK) ON condizioni_righe.id_condizione=condizioni.id WHERE (condizioni_righe.id_condizione='" & id_condizione_madre & "') " &
                        "AND (condizioni_elementi.tipologia='JOUNG') AND (condizioni_x_gruppi.id_gruppo='" & id_gruppo & "' OR " &
                        "condizioni_x_gruppi.id_gruppo IS NULL) ORDER BY condizioni_x_gruppi.id_gruppo DESC"
                    End If

                    Dbc.Close()
                    Dbc.Open()

                    Dim Rs As Data.SqlClient.SqlDataReader

                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                    Rs = Cmd.ExecuteReader()

                    Do While Rs.Read()
                        Dim giorni_nolo As Integer = giorni_noleggio
                        Dim elemento_prepagato As Boolean



                        If (prepagata And giorni_prepagati = 0) OrElse (prepagata AndAlso elementi_prepagati.Contains(Rs("id_elemento") & "-" & num_elemento)) Then
                            elemento_prepagato = True
                        Else
                            elemento_prepagato = False
                        End If


                        If prepagata AndAlso giorni_prepagati > giorni_noleggio AndAlso elemento_prepagato Then
                            giorni_nolo = giorni_prepagati
                        End If


                        qta = "1" 'LA QUANTITA' E' SEMPRE 1 TRANNE NEL CASO DI COSTO GIORNALIERO

                        Dim ordine_stampa As Integer
                        If (Rs("id_a_carico_di") & "") = Costanti.id_accessorio_incluso Then
                            ordine_stampa = 2
                        Else
                            ordine_stampa = 3
                        End If

                        id_unita_misura = Rs("id_unita_misura")

                        packed = Rs("pac")

                        'CONTROLLO A MENO CHE NON HO TROVATO GIA' UN VALORE
                        'INFATTI DALLA QUERY AVRO' IN TESTA LE RIGHE DI CONDIZIONE CON ID_GRUPPO SPECIFICATO ED IN CODA QUELLE SENZA GRUPPO
                        If Not valore_trovato Then
                            If Rs("id_unita_misura") = "0" Then
                                'CASO 1: NESSUNA UNITA' DI MISURA: IN QUESTO CASO NON MI INTERESSA IL CASO "PACKED" - LA CONDIZIONE E' PER FORZA
                                'SENZA LIMITE (NON CONTROLLO I GIORNI DI NOLEGGIO O I KM)
                                If (Rs("id_gruppo") & "") = "" Or (Rs("id_gruppo") & "") = id_gruppo Then
                                    If Rs("tipo_costo") = "€" Then
                                        costo = Rs("costo")
                                        percentuale = "NULL"
                                    ElseIf Rs("tipo_costo") = "%" Then
                                        costo = "NULL"
                                        percentuale = Rs("costo")
                                    Else
                                        'CASO incluso senza valore
                                        costo = "0"
                                        percentuale = "NULL"
                                    End If
                                    salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), num_elemento, Rs("nome_elemento") & nome_elemento, costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, elemento_prepagato)
                                    valore_trovato = True
                                End If
                            ElseIf Rs("id_unita_misura") = Costanti.id_unita_misura_giorni Then
                                'CASO 2: UNITA' DI MISURA GIORNI. CONTROLLO SE IL LIMITE (GIORNI DI NOLEGGIO) E' RISPETTATO O SE LA RIGA 
                                'E' SENZA ALCUN LIMITE (SE E' SEMPRE APPLICABILE AVRO' 0 SIA IN applicabilita_da CHE IN pplicabilita_a)
                                'IN OGNI CASO SE LA CONDIZIONE E' PACKED VIENE PRESO IL VALORE IN TOTO, SE NON E' PACKED MOLTIPLICO IL VALORE
                                'PER I GIORNI DI NOLEGGIO
                                If (Rs("id_gruppo") & "") = "" Or (Rs("id_gruppo") & "") = id_gruppo Then
                                    'CONTROLLO SE C'E' UNA REGOLA DI APPLICABILITA'. 
                                    If (Rs("applicabilita_da") = "0" And Rs("applicabilita_a") = "0") Or (giorni_nolo >= Rs("applicabilita_da") And giorni_nolo <= Rs("applicabilita_a")) Or (giorni_nolo >= Rs("applicabilita_da") And Rs("applicabilita_a") = "999") Then
                                        'IN QUESTO CASO HO TROVATO LA CONDIZIONE. CONTROLLO SOLO SE E' PACKED O MENO
                                        If Rs("pac") = "True" Then
                                            If Rs("tipo_costo") = "€" Then
                                                costo = Rs("costo")
                                                percentuale = "NULL"
                                            ElseIf Rs("tipo_costo") = "%" Then
                                                costo = "NULL"
                                                percentuale = Rs("costo")
                                            End If



                                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), num_elemento, Rs("nome_elemento") & nome_elemento, costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, elemento_prepagato)
                                            valore_trovato = True
                                        Else
                                            qta = giorni_nolo
                                            If Rs("tipo_costo") = "€" Then
                                                costo = CDbl(Rs("costo")) * giorni_nolo
                                                percentuale = "NULL"
                                            ElseIf Rs("tipo_costo") = "%" Then
                                                costo = "NULL"
                                                percentuale = CDbl(Rs("costo")) * giorni_nolo
                                            End If
                                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), num_elemento, Rs("nome_elemento") & nome_elemento, costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, elemento_prepagato)
                                            valore_trovato = True
                                        End If
                                    End If
                                End If
                            ElseIf Rs("id_unita_misura") = Costanti.id_unita_misura_km_tra_stazioni Then
                                'CASO 3: UNITA' DI MISURA KM TRA LE STAZIONI. DEVO PRELEVARE IL DATO DALLA TABELLA
                                'stazioni_distanza. SE NON PRESENTE DEVO RESTITUIRE UN ERRORE.
                                If (Rs("id_gruppo") & "") = "" Or (Rs("id_gruppo") & "") = id_gruppo Then
                                    'CONTROLLO SE C'E' UNA REGOLA DI APPLICABILITA'. 
                                    Dim km_distanza_stazioni As Integer = getDistanzaStazioni(stazione_pick_up, stazione_drop_off)
                                    If (Rs("applicabilita_da") = "0" And Rs("applicabilita_a") = "0") Or (km_distanza_stazioni >= Rs("applicabilita_da") And km_distanza_stazioni <= Rs("applicabilita_a")) Or (km_distanza_stazioni >= Rs("applicabilita_da") And Rs("applicabilita_a") = "999") Then
                                        'IN QUESTO CASO HO TROVATO LA CONDIZIONE. CONTROLLO SOLO SE E' PACKED O MENO. NON RIESCO SICURAMENTE AD ENTRARE
                                        'SE LA CONDIZIONE HA DEI LIMITI DI APPLICABILITA' E LA DISTANZA STAZIONI NON E' STATA TROVATA SU DB (OTTENGO IL VALORE
                                        '-1). NEL CASO IN CUI LA CONDIZIONI E' SENZA LIMITI MA NON E' PACKED, QUINDI DIPENDE DALLA DISTANZA TRA
                                        'LE STAZIONI, NON RESTITUISCO ERRORE IN QUANTO SI PROVERA' A CERCARE PER LA TARIFFA MADRE.
                                        If Rs("pac") = "True" Then
                                            If Rs("tipo_costo") = "€" Then
                                                costo = Rs("costo")
                                                percentuale = "NULL"
                                            ElseIf Rs("tipo_costo") = "%" Then
                                                costo = "NULL"
                                                percentuale = Rs("costo")
                                            End If
                                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), num_elemento, Rs("nome_elemento") & nome_elemento, costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, elemento_prepagato)
                                            valore_trovato = True
                                        ElseIf Rs("pac") = "False" And km_distanza_stazioni > 0 Then
                                            If Rs("tipo_costo") = "€" Then
                                                costo = CDbl(Rs("costo")) * km_distanza_stazioni
                                                percentuale = "NULL"
                                            ElseIf Rs("tipo_costo") = "%" Then
                                                costo = "NULL"
                                                percentuale = CDbl(Rs("costo")) * km_distanza_stazioni
                                            End If
                                            salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), num_elemento, Rs("nome_elemento") & nome_elemento, costo, percentuale, Rs("iva"), Rs("codice_iva"), Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), "1", ordine_stampa, "NULL", id_unita_misura, "", packed, qta, "", False, elemento_prepagato)
                                            valore_trovato = True
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Loop
                End If
            Next

            If valore_trovato Then
                calcola_supplemento_joung_driver = True
            Else
                calcola_supplemento_joung_driver = False
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Else
            calcola_supplemento_joung_driver = False
        End If
    End Function

    Public Shared Function gruppo_vendibile_eta_guidatori(ByVal id_gruppo As String, ByVal eta_primo_guidatore As String, ByVal eta_secondo_guidatore As String, ByVal id_preventivo As String, ByVal id_contratto As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal num_calcolo As String, ByVal id_utente As String, ByVal salva_warning As Boolean) As Integer
        'IL GRUPPO NON E' VENDIBILE SE L'ETA' NON E' RISPETTATA ALMENO PER IL PRIMO O PER IL SECONDO GUIDATORE (SE VALORIZZATI)
        '0 = NON VENDIBILE
        '1 = VENDIBILE CON JOUNG DRIVER PRIMO GUIDATORE
        '2 = VENDIBILE CON JOUNG DRIVER SECONDO GUIDATORE
        '3 = VENDIBILE CON JOUNG DRIVER ENTRAMBI GUIDATORI
        '4 = VENDIBILE
        gruppo_vendibile_eta_guidatori = -1
        If eta_primo_guidatore <> "" Or eta_secondo_guidatore <> "" Then
            Dim joung_primo As Boolean
            Dim joung_secondo As Boolean = False

            'L'INFORMAZIONE E' SALVATA NELLA TABELLA gruppi
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT eta_minima, eta_massima, ISNULL(eta_min_joung_driver,0) As eta_min_joung_driver, ISNULL(eta_max_joung_driver,0) As eta_max_joung_driver, permetti_joung_driver FROM gruppi WITH(NOLOCK) WHERE id_gruppo='" & id_gruppo & "'", Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader

            Rs.Read()
            If Trim(eta_primo_guidatore) <> "" Then
                'SE L'ETA' VIENE PASSATA DEVE ESSERE CONTROLLATA, ALTRIMENTI NON VIENE CONSIDERATA (LA SI PUO' NON SPECIFICARE
                'SOLAMENTE IN FASE DI PREVENTIVO, DALLA PRENOTAZIONE IN POI L'INFORMAZIONE E' OBBLIGATORIA)
                If CInt(eta_primo_guidatore) >= CInt(Rs("eta_minima")) And CInt(eta_primo_guidatore) <= CInt(Rs("eta_massima")) Then
                    'VIENE SODDISFATTA L'ETA' PER IL PRIMO GUIDATORE
                    joung_primo = False
                ElseIf CInt(eta_primo_guidatore) >= CInt(Rs("eta_min_joung_driver")) And CInt(eta_primo_guidatore) <= CInt(Rs("eta_max_joung_driver")) Then
                    'PER IL PRIMO GUIDATORE IL GRUPPO E' VENDIBILE CON L'OPZIONE JOUNG DRIVER
                    joung_primo = True
                Else
                    'GRUPPO AUTO NON VENDIBILE PERCHE' NON VIENE SODDISFATTA L'ETA' DEL PRIMO GUIDATORE
                    gruppo_vendibile_eta_guidatori = 0
                    If salva_warning Then
                        salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "13", id_utente, "")
                    End If
                End If
            Else
                joung_primo = False
            End If

            If gruppo_vendibile_eta_guidatori <> 0 And Trim(eta_secondo_guidatore) <> "" Then
                'SE IL PRIMO GRUPPO E' VENDIBILE L'ETA' DEL SECONDO GUIDATORE E' STATA SPECIFICATA CONTROLLO QUEST'ULTIMA
                If CInt(eta_secondo_guidatore) >= CInt(Rs("eta_minima")) And CInt(eta_secondo_guidatore) <= CInt(Rs("eta_massima")) Then
                    'VIENE SODDISFATTA L'ETA' PER IL SECONDO GUIDATORE
                    joung_secondo = False
                ElseIf CInt(eta_secondo_guidatore) >= CInt(Rs("eta_min_joung_driver")) And CInt(eta_secondo_guidatore) <= CInt(Rs("eta_max_joung_driver")) Then
                    'PER IL SECONDO GUIDATORE IL GRUPPO E' VENDIBILE CON L'OPZIONE JOUNG DRIVER
                    joung_secondo = True
                Else
                    'GRUPPO AUTO NON VENDIBILE PERCHE' NON VIENE SODDISFATTA L'ETA' DEL SECONDO GUIDATORE
                    gruppo_vendibile_eta_guidatori = 0
                    If salva_warning Then
                        salvaWarning(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, "13", id_utente, "")
                    End If
                End If
            Else
                joung_secondo = False
            End If

            If gruppo_vendibile_eta_guidatori <> 0 Then
                'RESTITUISCO IL VALORE CORRETTO IN BASE AI FLAG PRECEDENTEMENTE IMPOSTATI
                If Not joung_primo And Not joung_secondo Then
                    gruppo_vendibile_eta_guidatori = 4
                ElseIf joung_primo And Not joung_secondo Then
                    gruppo_vendibile_eta_guidatori = 1
                ElseIf Not joung_primo And joung_secondo Then
                    gruppo_vendibile_eta_guidatori = 2
                ElseIf joung_primo And joung_secondo Then
                    gruppo_vendibile_eta_guidatori = 3
                End If
            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Else
            gruppo_vendibile_eta_guidatori = 4
        End If
    End Function

    Public Shared Sub salva_warning_tariffa_non_vendibile(ByVal id_preventivo As String, ByVal id_ribaltamente As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_operatore As String, Optional ByVal stringa As String = "AAAA")
        salvaWarning(id_preventivo, id_ribaltamente, id_prenotazione, id_contratto, num_calcolo, "18", id_operatore, stringa)
    End Sub

    Public Shared Function checkMaxSconto(ByVal id_tariffe_righe As String, ByVal sconto As String, ByVal id_preventivo As String, ByVal id_ribaltamente As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_operatore As String, ByVal salva_warning As Boolean) As Double
        'CONTROLLA SE LO SCONTO PASSATO E' SUPERIORE A QUELLO MASSIMO PER LA TARIFFA E RESTITUISCE LO SCONTO MASSIMO - SU RICHIEST SALVA SU WARNING
        Dim Dbc1 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc1.Open()
        Dim Cmd1 As New Data.SqlClient.SqlCommand("SELECT tariffe.max_sconto FROM tariffe_righe WITH(NOLOCK) INNER JOIN tariffe WITH(NOLOCK) ON tariffe_righe.id_tariffa=tariffe.id WHERE tariffe_righe.id='" & id_tariffe_righe & "'", Dbc1)

        Dim max_sconto As Double = Cmd1.ExecuteScalar

        If CDbl(sconto) > max_sconto Then
            checkMaxSconto = max_sconto
            If salva_warning Then
                salvaWarning(id_preventivo, id_ribaltamente, id_prenotazione, id_contratto, num_calcolo, "19", id_operatore, "")
            End If
        Else
            'SCONTO MINORE DI QUELLO MASSIMO
            checkMaxSconto = -1
        End If

        Cmd1.Dispose()
        Cmd1 = Nothing
        Dbc1.Close()
        Dbc1.Dispose()
        Dbc1 = Nothing
    End Function

    Public Shared Function checkMaxScontoRack(ByVal id_tariffe_righe As String, ByVal sconto As String, ByVal id_preventivo As String, ByVal id_ribaltamente As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_operatore As String, ByVal salva_warning As Boolean) As Double
        'CONTROLLA SE LO SCONTO PASSATO E' SUPERIORE A QUELLO MASSIMO PER LA TARIFFA E RESTITUISCE LO SCONTO MASSIMO - SU RICHIEST SALVA SU WARNING
        Dim Dbc1 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc1.Open()
        Dim Cmd1 As New Data.SqlClient.SqlCommand("SELECT tariffe.max_sconto_rack FROM tariffe_righe WITH(NOLOCK) INNER JOIN tariffe WITH(NOLOCK) ON tariffe_righe.id_tariffa=tariffe.id WHERE tariffe_righe.id='" & id_tariffe_righe & "'", Dbc1)

        Dim max_sconto As Double = Cmd1.ExecuteScalar

        If CDbl(sconto) > max_sconto Then
            checkMaxScontoRack = max_sconto
            If salva_warning Then
                salvaWarning(id_preventivo, id_ribaltamente, id_prenotazione, id_contratto, num_calcolo, "28", id_operatore, "")
            End If
        Else
            'SCONTO MINORE DI QUELLO MASSIMO
            checkMaxScontoRack = -1
        End If

        Cmd1.Dispose()
        Cmd1 = Nothing
        Dbc1.Close()
        Dbc1.Dispose()
        Dbc1 = Nothing
    End Function

    Public Shared Function accessorioExtraNonAggiunto(ByVal id_elemento As String, ByVal id_gruppo As String, ByVal id_preventivo As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String) As Boolean
        Dim tabella As String
        Dim id_documento As String

        If id_preventivo <> "" Then
            tabella = "preventivi_costi"
            id_documento = id_preventivo
        ElseIf id_prenotazione <> "" Then
            tabella = "prenotazioni_costi"
            id_documento = id_prenotazione
        ElseIf id_contratto <> "" Then
            tabella = "contratti_costi"
            id_documento = id_contratto
        End If

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd1 As New Data.SqlClient.SqlCommand("SELECT TOP 1 id FROM " & tabella & " WITH(NOLOCK) WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_elemento & "'", Dbc)

        Dim test As String = Cmd1.ExecuteScalar & ""

        If test = "" Then
            accessorioExtraNonAggiunto = True
        Else
            accessorioExtraNonAggiunto = False
        End If


        Cmd1.Dispose()
        Cmd1 = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Sub aggiorna_franchigie(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String, ByVal sottotipologia_franchigia As String)
        'modificata in Public da Private 09.11.2021
        'RIMUOVO LE FRANCHIGIE NON RIDOTTE - AGGIUNGO LE FRANCHIGIA RIDOTTE (BASTA FARE IL NOT DEL CAMPO FRANCHIGIA_ATTIVA, IN QUANTO QUANDO E' ATTIVA
        'LA FRANCHIGIA NON E' ATTIVA LA RIDOTTA E VICEVERSA)

        Dim tabella As String
        Dim id_documento As String
        Dim sqlstr As String = ""


        If id_preventivo <> "" Then
            tabella = "preventivi_costi"
            id_documento = id_preventivo
        ElseIf id_ribaltamento <> "" Then
            tabella = "ribaltamento_costi"
            id_documento = id_ribaltamento
        ElseIf id_prenotazione <> "" Then
            tabella = "prenotazioni_costi"
            id_documento = id_prenotazione
        ElseIf id_contratto <> "" Then
            tabella = "contratti_costi"
            id_documento = id_contratto
        End If

        If sottotipologia_franchigia <> "TOTALE" Then

            Dim condizione_sottotipologia As String = ""
            If sottotipologia_franchigia <> "" Then
                condizione_sottotipologia = " AND sottotipologia_franchigia='" & sottotipologia_franchigia & "'"
            End If

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)

            Dim assicurazione_totale_prepagata As Boolean = False

            If tabella = "prenotazioni_costi" Or tabella = "contratti_costi" Then
                Dbc.Open()

                'NEL CASO DI PRENOTAZIONE O CONTRATTI DEVO CONTROLLARE E' STATA PREPAGATA LA CDR E/O LA TLR. IN CASO POSITIVO SI DEVE AGIRE SULLE EVENTUALI FRANCHIGIE PARZIALI
                'INVECE CHE SULLE FRANCHIGIE COMPLETE COME NEGLI ALTRI CASI
                sqlstr = "SELECT ISNULL(prepagato,0) FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) " &
                    "ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE tipologia_franchigia='ASSICURAZIONE' AND sottotipologia_franchigia='TOTALE' " &
                    "AND id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "'"

                Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

                assicurazione_totale_prepagata = Cmd.ExecuteScalar

                Try

                Catch ex As Exception

                End Try

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()

            End If

            If Not assicurazione_totale_prepagata Then

                Dbc.Open()

                sqlstr = "UPDATE " & tabella & " SET franchigia_attiva= 1 - franchigia_attiva FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT franchigia_attiva IS NULL " & condizione_sottotipologia

                Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

                Cmd1.ExecuteNonQuery()

                Cmd1.Dispose()
                Cmd1 = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            End If


        Else


            ''''' NEL CASO ELIMINAZIONE FRANCHIGIE


            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            'LA FRANCHGIA TOTALE CAMBIA L'ATTIVAZIONE O MENO DELLE FRANCHGIE COMPLETE FURTO E DANNI E NON TOCCA MAI LE PARZIALI. 
            'TUTTO QUESTO FUNZIONA NELL'IPOTESI CHE IL SISTEMA RIMUOVA AUTOMATICAMENTE LE EVENTUALI FRANCHIGIE PARZIALI PRECEDENTEMENTE INSERITE.

            Dim assicurazione_danni_prepagata As Boolean = False
            Dim assicurazione_furto_prepagata As Boolean = False

            If tabella = "prenotazioni_costi" Or tabella = "contratti_costi" Then
                'NEL CASO DI PRENOTAZIONE O CONTRATTI DEVO CONTROLLARE E' STATA PREPAGATA LA CDR E/O LA TLR. IN CASO POSITIVO SI DEVE AGIRE SULLE EVENTUALI FRANCHIGIE PARZIALI
                'INVECE CHE SULLE FRANCHIGIE COMPLETE COME NEGLI ALTRI CASI
                sqlstr = "SELECT ISNULL(prepagato,0) FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) " &
                    "ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE tipologia_franchigia='ASSICURAZIONE' AND sottotipologia_franchigia='DANNI' " &
                    "AND id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "'"

                Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

                assicurazione_danni_prepagata = Cmd.ExecuteScalar

                Try

                Catch ex As Exception

                End Try

                sqlstr = "SELECT ISNULL(prepagato,0) FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) " &
                    "ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE tipologia_franchigia='ASSICURAZIONE' AND sottotipologia_franchigia='FURTO' " &
                    "AND id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "'"

                Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)

                assicurazione_furto_prepagata = Cmd.ExecuteScalar

                Try

                Catch ex As Exception

                End Try

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Open()

            End If


            If Not assicurazione_danni_prepagata And Not assicurazione_furto_prepagata Then
                'se deve togliere le franchigie 09.11.2021
                sqlstr = "UPDATE " & tabella & " SET franchigia_attiva= 1 - franchigia_attiva FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT franchigia_attiva IS NULL AND (sottotipologia_franchigia='FURTO' OR sottotipologia_franchigia='DANNI') AND tipologia_franchigia='FRANCHIGIA'"
                Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

                Cmd1.ExecuteNonQuery()

                Cmd1.Dispose()
                Cmd1 = Nothing
            ElseIf assicurazione_danni_prepagata And assicurazione_furto_prepagata Then

                sqlstr = "UPDATE " & tabella & " SET franchigia_attiva= 1 - franchigia_attiva FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT franchigia_attiva IS NULL AND (sottotipologia_franchigia='FURTO' OR sottotipologia_franchigia='DANNI') AND tipologia_franchigia='FRANCHIGIA RID'"
                Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

                Cmd1.ExecuteNonQuery()

                Cmd1.Dispose()
                Cmd1 = Nothing
            ElseIf assicurazione_danni_prepagata And Not assicurazione_furto_prepagata Then

                sqlstr = "UPDATE " & tabella & " SET franchigia_attiva= 1 - franchigia_attiva FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT franchigia_attiva IS NULL AND (sottotipologia_franchigia='FURTO' AND tipologia_franchigia='FRANCHIGIA') OR (tipologia_franchigia='FRANCHIGIA RID' AND sottotipologia_franchigia='DANNI')"
                Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

                Cmd1.ExecuteNonQuery()

                Cmd1.Dispose()
                Cmd1 = Nothing
            ElseIf Not assicurazione_danni_prepagata And assicurazione_furto_prepagata Then

                sqlstr = "UPDATE " & tabella & " SET franchigia_attiva= 1 - franchigia_attiva FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT franchigia_attiva IS NULL AND (sottotipologia_franchigia='FURTO' AND tipologia_franchigia='FRANCHIGIA RID') OR (tipologia_franchigia='FRANCHIGIA' AND sottotipologia_franchigia='DANNI')"
                Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

                Cmd1.ExecuteNonQuery()

                Cmd1.Dispose()
                Cmd1 = Nothing
            End If

            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End If

    End Sub

    Public Shared Sub Aggiorna_Franchigie_Null(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String,
                                               ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String)

        Dim tabella As String
        Dim id_documento As String
        Dim sqlstr As String = ""




        Try

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            If id_preventivo <> "" Then
                tabella = "preventivi_costi"
                id_documento = id_preventivo
            ElseIf id_ribaltamento <> "" Then
                tabella = "ribaltamento_costi"
                id_documento = id_ribaltamento
            ElseIf id_prenotazione <> "" Then
                tabella = "prenotazioni_costi"
                id_documento = id_prenotazione
            ElseIf id_contratto <> "" Then
                tabella = "contratti_costi"
                id_documento = id_contratto
            Else
                Exit Sub
            End If

            sqlstr = "update " & tabella & " SET franchigia_attiva=NULL WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' "
            sqlstr += "And id_gruppo='" & id_gruppo & "' AND (id_elemento='100' or id_elemento='170') "    '

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            Cmd1.ExecuteNonQuery()

            Cmd1.Dispose()
            Cmd1 = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error aggiorna_Franchigie_null : <br/>" & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try


    End Sub



    Public Shared Sub Aggiorna_Ck(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String, ByVal id_elemento1 As String, ByVal val_elemento1 As String)

        Dim tabella As String
        Dim id_documento As String
        Dim sqlstr As String = ""

        'nessun elemento selezionato
        If id_elemento1 = "" Then
            Exit Sub
        End If

        Try

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            If id_preventivo <> "" Then
                tabella = "preventivi_costi"
                id_documento = id_preventivo
            ElseIf id_ribaltamento <> "" Then
                tabella = "ribaltamento_costi"
                id_documento = id_ribaltamento
            ElseIf id_prenotazione <> "" Then
                tabella = "prenotazioni_costi"
                id_documento = id_prenotazione
            ElseIf id_contratto <> "" Then
                tabella = "contratti_costi"
                id_documento = id_contratto
            End If

            'id_elemento=180 Franchigia Danni 
            'id_elemento=181 Franchigia Furto e inc 
            'id_elemento=203 Franchigia Danni ridotta
            'id_elemento=204 Franchigia Furto e inc ridotta


            sqlstr = "update " & tabella & " SET selezionato='" & val_elemento1 & "' WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' "

            If id_gruppo = "" Then 'aggiunto nel caso di selezione multigruppi
                sqlstr += "AND (id_elemento='" & id_elemento1 & "') "    '
            Else
                sqlstr += "And id_gruppo='" & id_gruppo & "' AND (id_elemento='" & id_elemento1 & "') "    '
            End If




            'If id_elemento1 = "248" Then 'si tratta della protezione plus deve eliminare la riga dal DB
            '    sqlstr = "DELETE FROM " & tabella & " WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' "
            '    sqlstr += "And id_gruppo='" & id_gruppo & "' AND (id_elemento='" & id_elemento1 & "') "    '
            'End If


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            Cmd1.ExecuteNonQuery()

            Cmd1.Dispose()
            Cmd1 = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error aggiorna_ck : <br/>" & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try





    End Sub

    Public Shared Sub aggiorna_deposito_cauzionale(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String, ByVal id_elemento1 As String, ByVal importo_deposito As String)
        'Visualizza righe franchigia


        If id_preventivo = "" And id_ribaltamento = "" And id_prenotazione = "" And id_contratto = "" Then
            Exit Sub '21.12.2021 1808
        End If



        Dim tabella As String
        Dim id_documento As String
        Dim sqlstr As String = ""

        'nessun elemento selezionato
        If id_elemento1 = "" Or importo_deposito = "" Then
            Exit Sub
        End If


        Try

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            If id_preventivo <> "" Then
                tabella = "preventivi_costi"
                id_documento = id_preventivo
            ElseIf id_ribaltamento <> "" Then
                tabella = "ribaltamento_costi"
                id_documento = id_ribaltamento
            ElseIf id_prenotazione <> "" Then
                tabella = "prenotazioni_costi"
                id_documento = id_prenotazione
            ElseIf id_contratto <> "" Then
                tabella = "contratti_costi"
                id_documento = id_contratto
            End If

            'id_elemento=283 Deposito Cauzionale

            If importo_deposito.IndexOf(",") > -1 Then
                importo_deposito = importo_deposito.Replace(".", "")        '#salvo aggiunto 17.05.2023
                importo_deposito = importo_deposito.Replace(",", ".")
            End If

            sqlstr = "update " & tabella & " SET franchigia_attiva=1, imponibile='" & importo_deposito & "', valore_costo='" & importo_deposito & "', imponibile_scontato='" & importo_deposito & "'"
            sqlstr += ", iva_imponibile='0',iva_imponibile_scontato='0' "
            sqlstr += "WHERE id_documento ='" & id_documento & "' AND id_elemento='" & id_elemento1 & "' "
            'aggiornato 19.01.2022 se idgruppo vuoto aggiorna ugualmente il numero di calcolo passato

            If num_calcolo <> "" Then
                sqlstr += "AND num_calcolo='" & num_calcolo & "' "
            End If

            If id_gruppo <> "" And id_gruppo <> "0" Then
                sqlstr += "And id_gruppo='" & id_gruppo & "' "
            End If


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Cmd1.ExecuteNonQuery()
            Cmd1.Dispose()
            Cmd1 = Nothing


        Catch ex As Exception
            HttpContext.Current.Response.Write("error aggiorna_deposito_cauzionale : <br/>" & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try



    End Sub

    Public Shared Sub visualizza_franchigie(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String, ByVal id_elemento1 As String, ByVal id_elemento2 As String)
        'Visualizza righe franchigia


        If id_preventivo = "" And id_ribaltamento = "" And id_prenotazione = "" And id_contratto = "" Then
            Exit Sub '21.12.2021 1808
        End If



        Dim tabella As String
        Dim id_documento As String
        Dim sqlstr As String = ""

        'nessun elemento selezionato
        If id_elemento1 = "" And id_elemento2 = "" Then
            Exit Sub
        End If
        If id_elemento2 = "" Then
            id_elemento2 = "-1"
        End If
        Try

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            If id_preventivo <> "" Then
                tabella = "preventivi_costi"
                id_documento = id_preventivo
            ElseIf id_ribaltamento <> "" Then
                tabella = "ribaltamento_costi"
                id_documento = id_ribaltamento
            ElseIf id_prenotazione <> "" Then
                tabella = "prenotazioni_costi"
                id_documento = id_prenotazione
            ElseIf id_contratto <> "" Then
                tabella = "contratti_costi"
                id_documento = id_contratto
            End If

            'id_elemento=180 Franchigia Danni 
            'id_elemento=181 Franchigia Furto e inc 
            'id_elemento=203 Franchigia Danni ridotta
            'id_elemento=204 Franchigia Furto e inc ridotta


            sqlstr = "update " & tabella & " SET franchigia_attiva=1 WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' "
            'aggiornato 05.01.2022 se idgruppo vuoto aggiorna ugualmente il numero di calcolo passato
            If id_gruppo <> "" Then
                sqlstr += "And id_gruppo='" & id_gruppo & "' AND (id_elemento='" & id_elemento1 & "' or id_elemento='" & id_elemento2 & "') "    '
            Else
                sqlstr += "And (id_elemento='" & id_elemento1 & "' or id_elemento='" & id_elemento2 & "') "    '
            End If


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Cmd1.ExecuteNonQuery()
            Cmd1.Dispose()
            Cmd1 = Nothing





        Catch ex As Exception
            HttpContext.Current.Response.Write("error visualizza_franchigie : <br/>" & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try



    End Sub

    Public Shared Sub visualizza_franchigie_Aggiorna_Costo(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String, ByVal id_elemento1 As String)
        'Visualizza righe franchigia


        'If id_preventivo = "" And id_ribaltamento = "" And id_prenotazione = "" And id_contratto = "" Then
        '    Exit Sub '21.12.2021 1808
        'End If


        Dim tabella As String
        Dim id_documento As String
        Dim sqlstr As String = ""

        'nessun elemento selezionato
        If id_elemento1 = "" Then
            Exit Sub
        End If

        Try

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            If id_preventivo <> "" Then
                tabella = "preventivi_costi"
                id_documento = id_preventivo
            ElseIf id_ribaltamento <> "" Then
                tabella = "ribaltamento_costi"
                id_documento = id_ribaltamento
            ElseIf id_prenotazione <> "" Then
                tabella = "prenotazioni_costi"
                id_documento = id_prenotazione
            ElseIf id_contratto <> "" Then
                tabella = "contratti_costi"
                id_documento = id_contratto
            End If

            'id_elemento=180 Franchigia Danni 
            'id_elemento=181 Franchigia Furto e inc 
            'id_elemento=203 Franchigia Danni ridotta
            'id_elemento=204 Franchigia Furto e inc ridotta


            sqlstr = "SELECT imponibile, imponibile_scontato, iva_imponibile, iva_imponibile_scontato FROM " & tabella & " WITH(NOLOCK)  WHERE id_elemento='" & id_elemento1 & "' AND id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader

            Dim totale_sconto As Double = 0
            Dim valore_tariffa As Double = 0
            Dim totale_imponibile_scontato As Double = 0
            Dim totale_iva As Double = 0
            Dim totale As Double = 0

            Dim imponibile As String = 0
            Dim imponibile_scontato As String = 0
            Dim iva_imponibile As String = 0
            Dim iva_imponibile_scontato As String = 0



            Rs = Cmd.ExecuteReader
            If Rs.Read() Then

                imponibile = CDbl(Rs("imponibile")).ToString
                imponibile_scontato = CDbl(Rs("imponibile_scontato")).ToString
                iva_imponibile = CDbl(Rs("iva_imponibile")).ToString
                iva_imponibile_scontato = CDbl(Rs("iva_imponibile_scontato")).ToString

                totale_sconto = CDbl(Rs("imponibile")) - CDbl(Rs("imponibile_scontato")) + CDbl(Rs("iva_imponibile")) - CDbl(Rs("iva_imponibile_scontato"))

                totale_sconto = FormatNumber(totale_sconto, 4, , , TriState.False)

                valore_tariffa = Rs("imponibile_scontato") + Rs("iva_imponibile_scontato")

                totale_imponibile_scontato = totale_imponibile_scontato + Rs("imponibile_scontato")
                totale_iva = totale_iva + Rs("iva_imponibile_scontato")

                totale = valore_tariffa

            End If
            '-----------------------------------------------------------------------------------------------------------------------------------

            Rs.Close()
            Cmd.Dispose()
            Dbc.Close()

            Dbc.Open()


            Dim sValore_Tariffa As String = totale.ToString



            sqlstr = "update " & tabella & " SET valore_costo=[valore_costo]+" & sValore_Tariffa.Replace(",", ".") & " "
            sqlstr += ", imponibile = [imponibile] + " & imponibile.Replace(",", ".") & " "
            sqlstr += ", imponibile_scontato = [imponibile_scontato] + " & imponibile_scontato.Replace(",", ".") & " "
            sqlstr += ", iva_imponibile = [iva_imponibile] + " & iva_imponibile.Replace(",", ".") & " "
            sqlstr += ", iva_imponibile_scontato = [iva_imponibile_scontato] + " & iva_imponibile_scontato.Replace(",", ".") & " "

            sqlstr += "WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' "
            sqlstr += "And id_gruppo='" & id_gruppo & "' AND nome_costo='TOTALE'"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Cmd1.ExecuteNonQuery()

            Cmd1.Dispose()
            Cmd1 = Nothing

            Dbc.Close()
            Dbc = Nothing




        Catch ex As Exception
            HttpContext.Current.Response.Write("error visualizza_franchigie Aggiorna Costo: <br/>" & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try



    End Sub



    Public Shared Sub nascondi_franchigie(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String, ByVal id_elemento1 As String, ByVal id_elemento2 As String)
        'Visualizza righe franchigia

        If id_preventivo = "" And id_ribaltamento = "" And id_prenotazione = "" And id_contratto = "" Then
            Exit Sub '21.12.2021 1808
        End If



        Dim tabella As String
        Dim id_documento As String
        Dim sqlstr As String = ""

        'nessun elemento selezionato
        If id_elemento1 = "" And id_elemento2 = "" Then
            Exit Sub
        End If

        If id_elemento2 = "" Then
            id_elemento2 = "-1"
        End If






        Try

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            If id_preventivo <> "" Then
                tabella = "preventivi_costi"
                id_documento = id_preventivo
            ElseIf id_ribaltamento <> "" Then
                tabella = "ribaltamento_costi"
                id_documento = id_ribaltamento
            ElseIf id_prenotazione <> "" Then
                tabella = "prenotazioni_costi"
                id_documento = id_prenotazione
            ElseIf id_contratto <> "" Then
                tabella = "contratti_costi"
                id_documento = id_contratto
            End If

            'id_elemento=180 Franchigia Danni 
            'id_elemento=181 Franchigia Furto e inc 
            'id_elemento=203 Franchigia Danni ridotta
            'id_elemento=204 Franchigia Furto e inc ridotta

            sqlstr = "update " & tabella & " SET franchigia_attiva=0 WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' "
            'aggiornato 05.01.2022 se idgruppo vuoto aggiorna ugualmente il numero di calcolo passato
            If id_gruppo <> "" Then
                sqlstr += "And id_gruppo='" & id_gruppo & "' AND (id_elemento='" & id_elemento1 & "' or id_elemento='" & id_elemento2 & "') "    '
            Else
                sqlstr += "And (id_elemento='" & id_elemento1 & "' or id_elemento='" & id_elemento2 & "') "    '
            End If


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Cmd1.ExecuteNonQuery()

            Cmd1.Dispose()
            Cmd1 = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error nascondi_franchigie : <br/>" & ex.Message & "<br/>" & sqlstr & "<br/>")
        End Try



    End Sub


    Public Shared Function check_upsell(ByVal id_tariffe_righe As String, ByVal id_gruppo_da_calcolare As String, ByVal id_gruppo_da_calcolare_prenotazione As String, ByVal numero_giorni As Integer, ByVal tariffa_vendibile As Boolean, ByVal id_contratto As String, Optional ByVal id_prenotazione As String = "") As Boolean
        'SE LA TARIFFA E' VENDIBILE CONTROLLO IL VALORE TARIFFA, ALTRIMENTI CONTROLLO NEL VALORE TARIFFA RACK (QUESTO E' VERO IN QUANTO
        'SE LA TARIFFA NON E' VENDIBILE, NEL CASO DI CAMBIO DI GRUPPO, VIENE CONSIDERATO IL VALORE TARIFFA RACK ANCHE SE NON VARIANO I 
        'GIORNI DI NOLEGGIO).

        Dim id_valore_tariffa As String
        If tariffa_vendibile Then
            id_valore_tariffa = getIdTempoKm(id_tariffe_righe)
        Else
            If id_contratto <> "" Then
                id_valore_tariffa = getIdTempoKmRack(id_tariffe_righe, id_contratto, "")
            Else
                id_valore_tariffa = getIdTempoKmRack(id_tariffe_righe, "", id_prenotazione)
            End If


            If id_valore_tariffa = "" Then
                'IN QUESTO CASO LA RACK NON E' STATA TROVATA - SI UTILIZZA LA TARIFFA ORIGINALE
                id_valore_tariffa = getIdTempoKm(id_tariffe_righe)
            End If
        End If

        Dim valore_nuovo_gruppo As Double
        Dim valore_gruppo_prenotazione As Double

        'NUOVO GRUPPO
        Dim sqlStr As String = "SELECT righe_tempo_km.pac, righe_tempo_km.valore FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                    "WHERE id_tempo_km='" & id_valore_tariffa & "' AND " & numero_giorni & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' AND NOT valore IS NULL AND valore<>0"

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Rs = Cmd.ExecuteReader()

        If Rs.Read() Then
            'NORMALIZZO IL VALORE CALCOLANDOLO COME COSTO GIORNALIERO
            If Rs("pac") = "False" Then
                valore_nuovo_gruppo = CDbl(Rs("valore"))
            ElseIf Rs("pac") = "True" Then
                valore_nuovo_gruppo = (CDbl(Rs("valore")) / numero_giorni)
            End If

            Rs.Close()
            Rs = Nothing
            Dbc.Close()
            Dbc.Open()

            sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                    "WHERE id_tempo_km='" & id_valore_tariffa & "' AND " & numero_giorni & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare_prenotazione & "' AND NOT valore IS NULL AND valore<>0"

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            Rs = Cmd.ExecuteReader()

            If Rs.Read() Then



                If Rs("pac") = "False" Then
                    valore_gruppo_prenotazione = CDbl(Rs("valore"))
                ElseIf Rs("pac") = "True" Then
                    valore_gruppo_prenotazione = (CDbl(Rs("valore")) / numero_giorni)
                End If

                If valore_nuovo_gruppo >= valore_gruppo_prenotazione Then
                    check_upsell = True
                Else
                    check_upsell = False
                End If
            Else
                'SE NON TROVO IL VALORE NON E' POSSIBILE ESEGUIRE IL CALCOLO - RESTITUISCO UGUALMENTE TRUE IN QUANTO L'ERRORE VIENE SEGNALATO
                'IN FASE DI CALCOLO
                check_upsell = True
            End If
        Else
            'SE NON TROVO IL VALORE NON E' POSSIBILE ESEGUIRE IL CALCOLO - RESTITUISCO UGUALMENTE TRUE IN QUANTO L'ERRORE VIENE SEGNALATO
            'IN FASE DI CALCOLO
            check_upsell = True
        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function
    Public Shared Function GetKmDisponibili(ByVal targa As String) As Boolean
        'inserita 02.06.2021 11.00
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim ris As Boolean = False
        Try
            Dim sqlStr As String = "SELECT [id],[targa],[km_attuali],[durata_mesi_leasing],[km_compresi_leasing],[data_inserimento]" &
        ",([km_compresi_leasing]/[durata_mesi_leasing]) as Km_mese " &
        ",([km_compresi_leasing]/[durata_mesi_leasing]/30) as Km_day " &
        ",getdate() as Data_Oggi " &
        ",DATEDIFF(d,[data_inserimento], getdate()) as num_giorni " &
        ",DATEDIFF(d,[data_inserimento], getdate()) * ([km_compresi_leasing]/[durata_mesi_leasing]/30) as km_restanti " &
        "From [Autonoleggio_SRC].[dbo].[veicoli] " &
        "Where targa='" & targa & "'"

            'targa = "EV 643 WY"  ' solo x test

            sqlStr = "Select veicoli.id, veicoli.targa, veicoli.km_attuali, veicoli.durata_mesi_leasing, veicoli.km_compresi_leasing, veicoli.data_inserimento, "
            sqlStr += "GETDATE() As Data_Oggi,([km_compresi_leasing]/[durata_mesi_leasing]) As Km_mese,([km_compresi_leasing]/[durata_mesi_leasing]/30) As Km_day,"
            sqlStr += "DATEDIFF(d,[data_inserimento], getdate()) As num_giorni , movimenti_targa.id_tipo_movimento, movimenti_targa.km_rientro As km_immissione_parco,"
            sqlStr += "movimenti_targa.data_rientro As Data_immissione_Parco,"
            sqlStr += "(DATEDIFF(d,[data_inserimento], getdate()) * ([km_compresi_leasing]/[durata_mesi_leasing]/30)) As km_restanti "
            sqlStr += "From veicoli INNER JOIN movimenti_targa On veicoli.id = movimenti_targa.id_veicolo "
            sqlStr += "Where (veicoli.targa = '" & targa & "') AND (movimenti_targa.id_tipo_movimento = 2)"

            Dim km_attuali As Integer = 0
            Dim km_attuali_immissione As Integer = 0
            Dim data_oggi As String = FormatDateTime(Date.Now, vbShortDate)
            Dim km_compresi_leasing As Integer = 0
            Dim durata_mesi_leasing As Integer = 0
            Dim km_mese As Integer = 0
            Dim km_day As Integer = 0

            Dim data_inserimento As String = ""
            Dim num_giorni_ad_oggi As Integer = 0

            Dim km_immissione_parco As Integer = 0
            Dim data_immissione_parco As String = ""

            Dim km_restanti_leasing As Integer = 0
            Dim km_restanti As Integer = 0
            Dim km_disponibili As Integer = 0
            Dim km_disponibili_ad_oggi As Integer = 0
            Dim km_disponibili_totali As Integer = 0

            Dim starga As String = ""

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.Read() Then

                'verificare se campo km_leasing not null 21.07.2021

                If Not IsDBNull(Rs!km_compresi_leasing) Then

                    starga = Rs!targa
                    km_attuali = Rs!km_attuali                                          'km Attuali
                    km_immissione_parco = Rs!km_immissione_parco                        'km immissione in parco auto
                    km_attuali_immissione = km_attuali - km_immissione_parco            'km effettivi_attuali senza i km al momento di immi. parco auto
                    km_compresi_leasing = Rs!km_compresi_leasing                        'km compresi in leasing
                    'km_restanti_leasing = km_compresi_leasing - km_attuali_immissione   'km restanti dal tot km leasing e i km effettivi attuali senza km imms. in ERRATO
                    km_restanti_leasing = km_compresi_leasing + km_immissione_parco - km_attuali  'km restanti dal tot km leasing e i km effettivi attuali senza km imms. in parco
                    km_disponibili_totali = km_restanti_leasing


                    durata_mesi_leasing = Rs!durata_mesi_leasing
                    km_mese = km_compresi_leasing / durata_mesi_leasing
                    km_day = km_mese / 30
                    num_giorni_ad_oggi = DateDiff("d", Rs![data_inserimento], Date.Now)
                    data_immissione_parco = Rs!Data_immissione_Parco

                    km_disponibili_ad_oggi = km_day * num_giorni_ad_oggi

                    'se i km effettivi_attuali senza i km al momento di immi. parco auto
                    'sono minori dei km compresi in leasing allora è vero che ci sono km restanti
                    km_disponibili = km_disponibili_ad_oggi - (km_attuali - km_immissione_parco)

                    If km_disponibili > 0 Then
                        ris = True
                    Else
                        ris = False
                    End If


                Else
                    ris = False
                End If

            End If
            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()

        Catch ex As Exception
            ris = False
        End Try

        Return ris


    End Function
    Public Shared Function scegli_targa_x_contratto(ByVal targa As String) As String()
        'CONTROLLA SE UNA TARGA E' COLLEGABILE AD UN CONTRATTO... RESTITUISCE NEL PRIMO POSTO L'ID DELL'AUTO (SE TROVATA), NEL SECONDO POSTO
        'L'EVENTUALE MOTIVO DEL PERCHE' NON E' POSSIBILE SELEZIOANARE IL VEICOLO (VUOTO SE E' POSSIBILE SELEZIONARLO), POI RISPETTIVAMENTE
        'SERBATOIO ATTUALE, SERBATOIO MASSIMO, KM ATTUALI, STAZIONE ATTUALE
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim risultato(13) As String
        risultato(10) = ""
        risultato(11) = ""

        '0 = id del veicolo
        '1 = messaggio di errore se targa non selezionabile
        '2 = serbatoio attuale
        '3 = serbatoio massimo
        '4 = km attuali
        '5 = stazione attuale
        '6 = id_gruppo
        '7 = modello
        '8 = gruppo
        '9 = alimentazione
        '10 = 1 se da rifornire
        '11 = 1 se da lavare
        '12 = codice carburante
        '13 = descrizione carburante

        Dim sqlStr As String = "SELECT veicoli.id, id_stazione, modelli.ID_Gruppo, gruppi.cod_gruppo, modelli.descrizione As modello, modelli.tipoCarburante, alimentazione.descrizione As alimentazione, alimentazione.cod_carb, km_attuali, serbatoio_attuale, modelli.capacita_serbatoio, venduta, venduta_da_fattura, in_vendita, disponibile_nolo, noleggiata, ISNULL(furto,'0') As furto, ISNULL(da_lavare,'0') As da_lavare, ISNULL(da_rifornire,'0') As da_rifornire FROM veicoli WITH(NOLOCK) LEFT JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello LEFT JOIN gruppi WITH(NOLOCK) ON modelli.ID_Gruppo = gruppi.ID_gruppo LEFT JOIN alimentazione WITH(NOLOCK) ON modelli.tipoCarburante=alimentazione.id WHERE veicoli.targa='" & Replace(targa, "'", "''") & "'"
        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        If Rs.Read() Then
            If Rs("disponibile_nolo") Then
                risultato(0) = Rs("id")
                risultato(1) = ""
                risultato(2) = Rs("serbatoio_attuale") & ""
                risultato(3) = Rs("capacita_serbatoio") & ""
                risultato(4) = Rs("km_attuali") & ""
                risultato(5) = Rs("id_stazione") & ""
                risultato(6) = Rs("id_gruppo") & ""
                risultato(7) = Rs("modello") & ""
                risultato(8) = Rs("cod_gruppo") & ""
                risultato(9) = Rs("tipoCarburante") & ""
                risultato(12) = Rs("cod_carb") & ""
                risultato(13) = Rs("alimentazione") & ""
            Else
                risultato(0) = Rs("id")

                If Rs("venduta") Or Rs("venduta_da_fattura") Then
                    risultato(1) = "Auto venduta"
                ElseIf Rs("in_vendita") Then
                    risultato(1) = "Auto in vendita"
                ElseIf Rs("furto") Then
                    risultato(1) = "Auto rubata"
                ElseIf Rs("noleggiata") And Rs("da_lavare") Then
                    risultato(1) = "Auto attualmente a lavaggio."
                ElseIf Rs("noleggiata") Then
                    risultato(1) = "Auto attualmente noleggiata."
                ElseIf Not Rs("da_lavare") And Rs("da_rifornire") Then
                    risultato(10) = "1"
                ElseIf Rs("da_lavare") And Not Rs("da_rifornire") Then
                    risultato(11) = "1"
                ElseIf Rs("da_lavare") And Rs("da_rifornire") Then
                    risultato(10) = "1"
                    risultato(11) = "1"
                ElseIf Not Rs("disponibile_nolo") And (Rs("id_stazione") & "") <> "" Then
                    risultato(1) = "Auto attualmente non disponibile per il noleggio - Motivo non riconosciuto. Si consiglia di contattare un amministratore."
                End If

                risultato(2) = Rs("serbatoio_attuale") & ""
                risultato(3) = Rs("capacita_serbatoio") & ""
                risultato(4) = Rs("km_attuali") & ""
                risultato(5) = Rs("id_stazione") & ""
                risultato(6) = Rs("id_gruppo") & ""
                risultato(7) = Rs("modello") & ""
                risultato(8) = Rs("cod_gruppo") & ""
                risultato(9) = Rs("tipoCarburante") & ""
                risultato(12) = Rs("cod_carb") & ""
                risultato(13) = Rs("alimentazione") & ""
            End If
        Else
            risultato(0) = ""
            risultato(1) = "Veicolo non esistente"
            risultato(2) = ""
            risultato(3) = ""
            risultato(4) = ""
            risultato(5) = ""
            risultato(6) = ""
            risultato(7) = ""
            risultato(8) = ""
            risultato(9) = ""
            risultato(12) = ""
            risultato(13) = ""
        End If

        scegli_targa_x_contratto = risultato

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
    End Function

    Public Shared Sub aggiorna_accessori_acquistabili_nolo_in_corso(ByVal idContratto As String, ByVal num_Calcolo As String)
        'PER IL CONTRATTO PASSATO COME ARGOMENTO VIENE AGGIORNATA L'INFORMAZIONE CIRCA LA VENDIBILITA' DEGLI ACCESSORI NOLO IN CORSO
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET acquistabile_nolo_in_corso=(SELECT acquistabile_nolo_in_corso FROM condizioni_elementi WITH(NOLOCK) WHERE condizioni_elementi.id=contratti_costi.id_elemento) WHERE contratti_costi.id_documento='" & idContratto & "' AND contratti_costi.num_calcolo='" & num_Calcolo & "' AND contratti_costi.obbligatorio='0' AND contratti_costi.id_a_carico_di<>'" & Costanti.id_accessorio_incluso & "'", Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Public Shared Function accessorio_acquistabile_nolo_in_corso(ByVal id_accessorio As String) As Boolean
        'RESTITUISCE L'INFORMAZIONE CIRCA LA VENDIBILITA' DI UN SINGOLO ACCESSORIO A NOLO IN CORSO
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT acquistabile_nolo_in_corso FROM condizioni_elementi WITH(NOLOCK) WHERE condizioni_elementi.id='" & id_accessorio & "'", Dbc)

        accessorio_acquistabile_nolo_in_corso = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function pieno_carburante_selezionato(ByVal id_contratto As String, ByVal num_calcolo As String) As Boolean
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(selezionato,'0') FROM contratti_costi WITH(NOLOCK) WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND id_elemento='" & get_id_pieno_carburante() & "'", Dbc)

        pieno_carburante_selezionato = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function get_id_pieno_carburante() As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM condizioni_elementi WITH(NOLOCK) WHERE tipologia='RIMUOVI_RIFORNIMENTO'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test = "" Then
            test = "0"
        End If

        get_id_pieno_carburante = test

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function get_id_servizio_rifornimento() As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM condizioni_elementi WITH(NOLOCK) WHERE NOT servizio_rifornimento_tolleranza IS NULL", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test = "" Then
            test = "0"
        End If

        get_id_servizio_rifornimento = test

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function get_id_spese_spedizione_postali() As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id FROM condizioni_elementi WITH(NOLOCK) WHERE tipologia='SPESE_SPED_FATT'", Dbc)

        Dim test As String = Cmd.ExecuteScalar & ""

        If test = "" Then
            test = "0"
        End If

        get_id_spese_spedizione_postali = test

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function getCostoCarburante_x_litro(ByVal id_stazione As String, ByVal id_alimentazione As String) As Double
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        'SELEZIONO IL COSTO PER LITRO DI CARBURANTE IN MODO DA AVERE 1 O 2 RIGHE CON IN TESTA LA RIGA PER STAZIONE ED IN CODA LA RIGA COL COSTO GENERICO
        Dim sqlStr As String = "SELECT ISNULL(costo,0) FROM alimentazione_costi_x_stazione WITH(NOLOCK) " &
        "WHERE (id_stazione='" & id_stazione & "' OR id_stazione IS NULL) AND id_alimentazione='" & id_alimentazione & "' " &
        "ORDER BY id_stazione DESC"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        getCostoCarburante_x_litro = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Sub aggiorna_costo_pieno_carburante_da_calcolo_precedente(ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_pieno_carburante As String)
        'A CONTRATTO IL CORSO NON DEVE ESSERE RICALCOLATO IL COSTO DEL PIENO CARBURANTE (ACCESSORIO) SE ERA STATO PRECEDENTEMENTE
        'SELEZIONATO, IN CASO DI RICALCOLO
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        'SELEZIONO IL COSTO PER LITRO DI CARBURANTE IN MODO DA AVERE 1 O 2 RIGHE CON IN TESTA LA RIGA PER STAZIONE ED IN CODA LA RIGA COL COSTO GENERICO
        Dim sqlStr As String = "SELECT valore_costo, imponibile, iva_imponibile, imponibile_scontato, iva_imponibile_scontato, ISNULL(imponibile_onere,0) As imponibile_onere, ISNULL(iva_onere,0) As iva_onere " &
        "FROM contratti_costi WITH(NOLOCK) WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & CInt(num_calcolo) - 1 & "' " &
        "AND id_elemento='" & id_pieno_carburante & "'"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        'RICHIAMO QUESTA PROCEDURA QUANDO SONO SICURO CHE NEL CALCOLO PRECEDENTE IL COSTO ERA STATO SELEZIONATO
        Rs.Read()

        sqlStr = "UPDATE contratti_costi SET valore_costo='" & Replace(Rs("valore_costo"), ",", ".") & "', " &
        "imponibile='" & Replace(Rs("imponibile"), ",", ".") & "', iva_imponibile='" & Replace(Rs("iva_imponibile"), ",", ".") & "'," &
        "imponibile_scontato='" & Replace(Rs("imponibile_scontato"), ",", ".") & "', iva_imponibile_scontato='" & Replace(Rs("iva_imponibile_scontato"), ",", ".") & "', " &
        "imponibile_onere='" & Replace(Rs("imponibile_onere"), ",", ".") & "', iva_onere='" & Replace(Rs("iva_onere"), ",", ".") & "' " &
        "WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' " &
        "AND id_elemento='" & id_pieno_carburante & "'"


        Rs.Close()
        Rs = Nothing

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Public Sub aggiungi_refuel_calcolo_precedente(ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String)
        Dim sqlStr As String = "SELECT id FROM condizioni_elementi WHERE tipologia='REFUEL'"
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            Dim id_elemento As String = Cmd.ExecuteScalar & ""

            'SALVATAGGIO DEI COSTI DAL CALCOLO PRECEDENTE
            sqlStr = "INSERT INTO contratti_costi (id_documento, num_calcolo, ordine_stampa, secondo_ordine_stampa, id_gruppo, id_elemento, num_elemento, nome_costo, selezionato, omaggiato, " &
        "franchigia_attiva, valore_costo, valore_percentuale, imponibile, iva_imponibile, imponibile_scontato, iva_imponibile_scontato, aliquota_iva, codice_iva, iva_inclusa, scontabile, " &
        "omaggiabile, acquistabile_nolo_in_corso, id_a_carico_di, id_metodo_stampa, obbligatorio, id_unita_misura, qta, packed, data_aggiunta_nolo_in_corso, prepagato) " &
        "(SELECT id_documento, " & num_calcolo & ", ordine_stampa, secondo_ordine_stampa, id_gruppo, id_elemento, num_elemento, nome_costo, selezionato, omaggiato, " &
        "franchigia_attiva, valore_costo, valore_percentuale, imponibile, iva_imponibile, imponibile_scontato, iva_imponibile_scontato, aliquota_iva, codice_iva, iva_inclusa, scontabile, " &
        "omaggiabile, acquistabile_nolo_in_corso, id_a_carico_di, id_metodo_stampa, obbligatorio, id_unita_misura, qta, packed, data_aggiunta_nolo_in_corso, prepagato " &
        "FROM contratti_costi WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & CInt(num_calcolo) - 1 & "' AND id_elemento='" & id_elemento & "')"

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            'Dim sconto As Integer = 0 'IN QUESTO MOMENTO L'ELEMENTO NON E' SCONTABILE, SE LO DOVESSE DIVENTARE BASTA PASSARE IL RELATIVO VALORE

            ''SALVO IL VALORE DI IMPONIBILE E IL VALORE DI IVA---------------------------------------------
            'Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET imponibile=(valore_costo*100/(100+aliquota_iva)), iva_imponibile=(valore_costo*aliquota_iva/(100+aliquota_iva)) WHERE iva_inclusa='True' AND id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND NOT valore_costo IS NULL AND id_elemento='" & id_elemento & "'", Dbc)
            'Cmd.ExecuteNonQuery()
            'Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET imponibile=valore_costo, iva_imponibile=(valore_costo*aliquota_iva/100) WHERE iva_inclusa='False' AND id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND NOT valore_costo IS NULL AND id_elemento='" & id_elemento & "'", Dbc)
            'Cmd.ExecuteNonQuery()
            ''-----------------------------------------------------------------------------------------------------------------------------------
            ''CALCOLO DELL'EVENTUALE SCONTO RICHIESTO DALL'OPERATORE APPLICANDOLO ALL'IMPONIBILE DEGLI ELEMENTI SCONTABILI------------------
            'Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET imponibile_scontato=imponibile-(imponibile*" & sconto & "/100) WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND NOT valore_costo IS NULL AND scontabile='1' AND id_elemento='" & id_elemento & "'", Dbc)
            'Cmd.ExecuteNonQuery()
            ''PER GLI ELEMENTI NON SCONTABILI SETTO L'IMPONIBILE SCONTATO UGUALE ALL'IMPONIBILE
            'Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET imponibile_scontato=imponibile, iva_imponibile_scontato=iva_imponibile WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND NOT valore_costo IS NULL AND scontabile='0' AND id_elemento='" & id_elemento & "'", Dbc)
            'Cmd.ExecuteNonQuery()
            ''-----------------------------------------------------------------------------------------------------------------------------------
            ''0B - CALCOLO DELL'IVA SULL'IMPONIBILE SCONTATO ------------------------------------------------------------------------------------
            'Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET iva_imponibile_scontato=imponibile_scontato*aliquota_iva/100  WHERE (iva_inclusa='True' OR iva_inclusa='False') AND id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND NOT valore_costo IS NULL AND scontabile='1' AND id_elemento='" & id_elemento & "'", Dbc)
            'Cmd.ExecuteNonQuery()
            ''-----------------------------------------------------------------------------------------------------------------------------------

            'UNA VOLTA CREATO L'ELEMENTO LO AGGIUNGO AL TOTALE
            'aggiungi_costo_accessorio("", "", "", id_contratto, num_calcolo, id_gruppo, id_elemento, "", "", "")

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni aggiungi_refuel_calcolo_precedente " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try



    End Sub

    Public Sub addebita_refuel(ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_stazione As String, ByVal id_gruppo As String, ByVal id_alimentazione As String, ByVal litri_da_addebitare As Integer, Optional ByVal targa_x_descrizione_costo As String = "")

        Dim sqlStr As String = "SELECT id FROM  condizioni_elementi WITH(NOLOCK) WHERE tipologia='REFUEL'"
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Try

            'L'ELEMENTO REFUEL DEVE ESISTERE. NE RECUPERO L'ID E SE NON ESISTE CREO L'ELEMENTO

            Dim id_elemento As String = Cmd.ExecuteScalar & ""

            If id_elemento = "" Then
                sqlStr = "INSERT INTO condizioni_elementi (descrizione, id_aliquota_iva, scontabile," &
            "omaggiabile, valorizza, acquistabile_nolo_in_corso, tipologia) VALUES (" &
            "'ADD.CARBURANTE','" & Costanti.id_iva_default & "','0','0','1','0','REFUEL')"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteNonQuery()

                sqlStr = "SELECT id FROM  condizioni_elementi WITH(NOLOCK) WHERE tipologia='REFUEL'"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                id_elemento = Cmd.ExecuteScalar
            End If



            'SELEZIONO IL COSTO PER LITRO DI CARBURANTE IN MODO DA AVERE 1 O 2 RIGHE CON IN TESTA LA RIGA PER STAZIONE ED IN CODA LA RIGA COL COSTO GENERICO
            sqlStr = "SELECT ISNULL(costo,0) FROM alimentazione_costi_x_stazione WITH(NOLOCK) " &
        "WHERE (id_stazione='" & id_stazione & "' OR id_stazione IS NULL) AND id_alimentazione='" & id_alimentazione & "' " &
        "ORDER BY id_stazione DESC"

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim costo_unitario As Double = Cmd.ExecuteScalar
            Dim costo As Double = (costo_unitario * litri_da_addebitare)

            'SALVO OGNI ELEMENTO REFUEL CON UN NUM_ELEMENTO UNIVOCO IN MODO DA FAR FUNZIONARE CORRETTAMENTE L'OMAGGIABILITA' DELL'ELEMENTO

            Dim num_elemento As String
            sqlStr = "SELECT ISNULL(MAX(ISNULL(num_elemento,0)),0) FROM contratti_costi WITH(NOLOCK) WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "'"
            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            num_elemento = Cmd.ExecuteScalar
            num_elemento = CInt(num_elemento) + 1

            'AGGIUNGO L'ELEMENTO ALLA TABELLA condizioni_elementi
            sqlStr = "SELECT condizioni_elementi.id, condizioni_elementi.descrizione, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva," &
            "scontabile, omaggiabile, acquistabile_nolo_in_corso FROM condizioni_elementi WITH(NOLOCK)" &
            "INNER JOIN aliquote_iva WITH(NOLOCK) ON condizioni_elementi.id_aliquota_iva=aliquote_iva.id " &
            "WHERE tipologia='REFUEL'"

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Dim id_tabella As String 'POSSONO ESISTERE PIU' ELEMENTI DEL COSTO CARBURANTE (IN CASO DI CRV) PER CUI IN QUESTO CASO SI DEVE RAGIONARE PER ID TABELLA

            If Rs.Read() Then
                id_tabella = salvaRigaCalcolo("", "", "", id_contratto, num_calcolo, id_gruppo, Rs("id"), num_elemento, Rs("descrizione") & " " & targa_x_descrizione_costo, costo, "NULL", Rs("iva"), Rs("codice_iva"), "True", Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Costanti.id_a_carico_del_cliente, Costanti.id_valorizza_nel_contratto, "True", "0", "3", "NULL", "0", "", "True", "1", "", True, False)
            End If

            Rs.Close()
            Rs = Nothing



            Dim sconto As Integer = 0 'IN QUESTO MOMENTO L'ELEMENTO NON E' SCONTABILE, SE LO DOVESSE DIVENTARE BASTA PASSARE IL RELATIVO VALORE

            'SALVO IL VALORE DI IMPONIBILE E IL VALORE DI IVA---------------------------------------------
            Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET imponibile=(valore_costo*100/(100+aliquota_iva)), iva_imponibile=(valore_costo*aliquota_iva/(100+aliquota_iva)) WHERE iva_inclusa='True' AND NOT valore_costo IS NULL AND id='" & id_tabella & "'", Dbc)
            Cmd.ExecuteNonQuery()
            Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET imponibile=valore_costo, iva_imponibile=(valore_costo*aliquota_iva/100) WHERE iva_inclusa='False' AND NOT valore_costo IS NULL AND id='" & id_tabella & "'", Dbc)
            Cmd.ExecuteNonQuery()
            '-----------------------------------------------------------------------------------------------------------------------------------
            'CALCOLO DELL'EVENTUALE SCONTO RICHIESTO DALL'OPERATORE APPLICANDOLO ALL'IMPONIBILE DEGLI ELEMENTI SCONTABILI------------------
            Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET imponibile_scontato=imponibile-(imponibile*" & sconto & "/100) WHERE  NOT valore_costo IS NULL AND scontabile='1' AND id='" & id_tabella & "'", Dbc)
            Cmd.ExecuteNonQuery()
            'PER GLI ELEMENTI NON SCONTABILI SETTO L'IMPONIBILE SCONTATO UGUALE ALL'IMPONIBILE
            Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET imponibile_scontato=imponibile, iva_imponibile_scontato=iva_imponibile WHERE NOT valore_costo IS NULL AND scontabile='0' AND id='" & id_tabella & "'", Dbc)
            Cmd.ExecuteNonQuery()
            '-----------------------------------------------------------------------------------------------------------------------------------
            '0B - CALCOLO DELL'IVA SULL'IMPONIBILE SCONTATO ------------------------------------------------------------------------------------
            Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET iva_imponibile_scontato=imponibile_scontato*aliquota_iva/100  WHERE (iva_inclusa='True' OR iva_inclusa='False') AND NOT valore_costo IS NULL AND scontabile='1' AND id='" & id_tabella & "'", Dbc)
            Cmd.ExecuteNonQuery()
            '-----------------------------------------------------------------------------------------------------------------------------------

            'UNA VOLTA CREATO L'ELEMENTO REFUEL LO AGGIUNGO AL TOTALE
            aggiungi_costo_refuel(id_contratto, num_calcolo, id_gruppo, id_elemento, id_tabella)

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni addebita_refuel  " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try
    End Sub

    Public Shared Sub aggiorna_costo_pieno_carburante(ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_stazione As String, ByVal id_alimentazione As String, ByVal capacita_serbatoio As Integer, ByVal id_pieno_carburante As String, ByVal txt_sconto As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        'SELEZIONO IL COSTO PER LITRO DI CARBURANTE IN MODO DA AVERE 1 O 2 RIGHE CON IN TESTA LA RIGA PER STAZIONE ED IN CODA LA RIGA COL COSTO GENERICO
        Dim sqlStr As String = "SELECT ISNULL(costo,0) FROM alimentazione_costi_x_stazione WITH(NOLOCK) " &
        "WHERE (id_stazione='" & id_stazione & "' OR id_stazione IS NULL) AND id_alimentazione='" & id_alimentazione & "' " &
        "ORDER BY id_stazione DESC"

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Dim costo_unitario As Double = Cmd.ExecuteScalar

        'SELEZIONO L'EVENTUALE SCONTO SU PIENO CARBURANTE
        sqlStr = "SELECT ISNULL(sconto,0) FROM alimentazione_sconto_x_stazione WITH(NOLOCK) " &
        "WHERE (id_stazione='" & id_stazione & "' OR id_stazione IS NULL) " &
        "ORDER BY id_stazione DESC"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Dim sconto_su_carburante As Double = Cmd.ExecuteScalar

        'CALCOLO IL VALORE DEL COSTO - SUL COSTO NON SI DEVE PAGARE L'ONERE (APT/DT)
        Dim costo As Double = (costo_unitario * capacita_serbatoio) - (costo_unitario * capacita_serbatoio * sconto_su_carburante / 100)

        'AGGIORNO IL COSTO IN contratti_costi
        sqlStr = "UPDATE contratti_costi SET valore_costo='" & Replace(costo, ",", ".") & "' " &
        "WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND id_elemento='" & id_pieno_carburante & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Cmd.ExecuteNonQuery()

        Dim sconto As Integer
        If txt_sconto = "" Then
            sconto = 0
        Else
            sconto = CInt(txt_sconto)
        End If

        'SALVO IL VALORE DI IMPONIBILE E IL VALORE DI IVA---------------------------------------------
        Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET imponibile=(valore_costo*100/(100+aliquota_iva)), iva_imponibile=(valore_costo*aliquota_iva/(100+aliquota_iva)) WHERE iva_inclusa='True' AND id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND NOT valore_costo IS NULL AND id_elemento='" & id_pieno_carburante & "'", Dbc)
        Cmd.ExecuteNonQuery()
        Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET imponibile=valore_costo, iva_imponibile=(valore_costo*aliquota_iva/100) WHERE iva_inclusa='False' AND id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND NOT valore_costo IS NULL AND id_elemento='" & id_pieno_carburante & "'", Dbc)
        Cmd.ExecuteNonQuery()
        '-----------------------------------------------------------------------------------------------------------------------------------

        'CALCOLO DELL'EVENTUALE SCONTO RICHIESTO DALL'OPERATORE APPLICANDOLO ALL'IMPONIBILE DEGLI ELEMENTI SCONTABILI------------------
        Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET imponibile_scontato=imponibile-(imponibile*" & sconto & "/100) WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND NOT valore_costo IS NULL AND scontabile='1' AND id_elemento='" & id_pieno_carburante & "'", Dbc)
        Cmd.ExecuteNonQuery()
        'PER GLI ELEMENTI NON SCONTABILI SETTO L'IMPONIBILE SCONTATO UGUALE ALL'IMPONIBILE
        Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET imponibile_scontato=imponibile, iva_imponibile_scontato=iva_imponibile WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND NOT valore_costo IS NULL AND scontabile='0' AND id_elemento='" & id_pieno_carburante & "'", Dbc)
        Cmd.ExecuteNonQuery()
        '-----------------------------------------------------------------------------------------------------------------------------------
        '0B - CALCOLO DELL'IVA SULL'IMPONIBILE SCONTATO ------------------------------------------------------------------------------------
        Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET iva_imponibile_scontato=imponibile_scontato*aliquota_iva/100  WHERE (iva_inclusa='True' OR iva_inclusa='False') AND id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND NOT valore_costo IS NULL AND scontabile='1' AND id_elemento='" & id_pieno_carburante & "'", Dbc)
        Cmd.ExecuteNonQuery()
        '-----------------------------------------------------------------------------------------------------------------------------------

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

    Public Shared Function contratto_settabile_void(ByVal id_contratto As String, ByVal id_stazione As String) As Boolean
        'CONTROLLA SE SONO PASSATI GLI X MINUTI SPECIFICATI NELLA TABELLA contratti_minuti_void (GENERALI O PER STAZIONE)
        'DAL MOMENTO DELLA CREAZIONE DELL'USCITA DEL VEICOLO. UTILIZZO SOLO L'ID CONTRATTO IN QUANTO TUTTE LE RIGHE DI CALCOLO CONDIVIDONO LO STESSO
        'VALORE PER data_uscita
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlStr As String = "SELECT ISNULL(minuti,0) FROM contratti_minuti_void WITH(NOLOCK) " &
        "WHERE (id_stazione='" & id_stazione & "' OR id_stazione IS NULL) " &
        "ORDER BY id_stazione DESC"
        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Dim minuti_modifica_targa As Integer = Cmd.ExecuteScalar

        sqlStr = "SELECT data_uscita FROM contratti WITH(NOLOCK) WHERE id='" & id_contratto & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Dim orario_creazione As DateTime = Cmd.ExecuteScalar
        sqlStr = "SELECT data_rientro FROM contratti WITH(NOLOCK) WHERE id='" & id_contratto & "'"
        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Dim orario_rientro As DateTime = Cmd.ExecuteScalar

        If DateDiff(DateInterval.Minute, orario_creazione, orario_rientro) > minuti_modifica_targa Then
            contratto_settabile_void = False
        Else
            contratto_settabile_void = True
        End If

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Public Shared Function targaModificabile(ByVal id_contratto As String, ByVal id_stazione As String) As Boolean
        'CONTROLLA SE SONO PASSATI GLI X MINUTI SPECIFICATI NELLA TABELLA contratti_minuti_modifica_targa (GENERALI O PER STAZIONE)
        'DAL MOMENTO DELLA CREAZIONE DEL CONTRATTO. UTILIZZO SOLO L'ID CONTRATTO IN QUANTO TUTTE LE RIGHE DI CALCOLO CONDIVIDONO LO STESSO
        'VALORE PER data_creazione
        Dim sqla As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            sqla = "SELECT ISNULL(minuti,0) FROM contratti_minuti_modifica_targa WITH(NOLOCK) " &
            "WHERE (id_stazione='" & id_stazione & "' OR id_stazione IS NULL) " &
            "ORDER BY id_stazione DESC"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim minuti_modifica_targa As Integer = Cmd.ExecuteScalar

            sqla = "SELECT data_creazione FROM contratti WITH(NOLOCK) WHERE id='" & id_contratto & "'"
            Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim orario_creazione As DateTime = Cmd.ExecuteScalar
            Dim orario_attuale As DateTime = Now()

            If DateDiff(DateInterval.Minute, orario_creazione, orario_attuale) > minuti_modifica_targa Then
                targaModificabile = False
            Else
                targaModificabile = True
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error targaModificabile : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

    End Function

    Public Shared Function getNumRifornimento(ByVal id_stazione As String, ByVal anno_rifornimento As String) As String
        'GENERA IL SUCCESSIVO NUMERO DI RIFORNIMENTO PER LA STAZIONE (UNIVOCO PER ANNO)
        Dim sqla As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            sqla = "SELECT ISNULL(MAX(num_rifornimento),0) FROM rifornimenti WITH(NOLOCK) WHERE anno_rifornimento='" & anno_rifornimento & "' AND id_stazione_out='" & id_stazione & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim numero As String = Cmd.ExecuteScalar
            numero = CInt(numero) + 1

            getNumRifornimento = numero

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try



    End Function


    Public Shared Function SalvaFatturaAres_Web(ByVal CodiceFattura As Integer, ByVal IdDitta As String, ByVal NumPrenotazione As String, ByVal miaPrenotazioneWeb As PrenotazioneWeb, ByVal id_pagamento As Integer) As Integer
        SalvaFatturaAres_Web = -1
        Dim myFattura As New Fattura
        Try
            With miaPrenotazioneWeb
                Dim CodiceIva As String = Costanti.CodiceIvaWeb
                Dim Totale As Double = Double.Parse(.CCIMPORTO)
                Dim TotaleImponibile As Double = Libreria.ArrotondaDouble(Totale / (1 + Costanti.ValoreIvaWeb / 100))
                Dim TotaleIva As Double = Totale - TotaleImponibile

                myFattura.codice_fattura = CodiceFattura
                myFattura.anno_fattura = Year(.CCDATA)
                myFattura.data_fattura = DateTime.Parse(.CCDATA)
                myFattura.tipo_fattura = TipoFattura.Prenotazione ' web valore ancora non tabellato...
                myFattura.id_riferimento = Integer.Parse(NumPrenotazione)
                myFattura.id_pagamento = id_pagamento
                myFattura.id_modalita_pagamento = Costanti.IdTipoPagamentoWeb ' già pagato.... = Rimessa Diretta
                myFattura.id_coordinata_bancaria = 0 ' valore ancora non censito... in effetti nel ribaltamento è già stato effettuato il pagamento...
                'myFattura.TotaleImponibile = TotaleImponibile
                'myFattura.TotaleIVA = TotaleIva

                myFattura.id_ditta = Integer.Parse(IdDitta)

                Dim miaditta As Ditte = Ditte.RecuperaRecordDaId(myFattura.id_ditta)
                myFattura.Intestazione = miaditta.Rag_soc
                myFattura.Indirizzo = miaditta.Indirizzo
                myFattura.Citta = miaditta.Citta
                myFattura.CAP = miaditta.Cap
                myFattura.Provincia = miaditta.provincia
                If miaditta.NAZIONE Is Nothing Then
                    myFattura.Nazione = 0
                Else
                    myFattura.Nazione = miaditta.NAZIONE
                End If

                myFattura.piva = miaditta.PIva
                myFattura.codicefiscale = miaditta.c_fis
                myFattura.mail = miaditta.EMAIL


                ' aggiungo una riga di fatturazione...
                Dim myRigaFattura As New Fattura.Fatture_riga
                myRigaFattura.id_unita_misura = 0 ' ancora non censite le unità di misura...
                myRigaFattura.quantita = 1 ' inteso come l'intero nolegio... non gli acessori ed i giorni...
                myRigaFattura.Descrizione = "INCASSO PAGAMENTO N° " & id_pagamento & " DEL " & Format(Date.Parse(.CCDATA), "dd/MM/yyyy")
                myRigaFattura.Prezzo = TotaleImponibile
                myRigaFattura.TipoSconto = TipoScontoFattura.Somma
                myRigaFattura.Sconto = 0
                ' myRigaFattura.Imponibile = TotaleImponibile ' campo calcolato!
                myRigaFattura.AliquotaIVA = Costanti.ValoreIvaWeb

                myFattura.addRigafattura(myRigaFattura)

                SalvaFatturaAres_Web = myFattura.Save()
            End With
        Catch ex As Exception
            HttpContext.Current.Response.Write("error SalvaFatturaAres_Web : <br/>" & ex.Message & "<br/>" & "" & "<br/>")
        End Try





    End Function

    Public Class PrenotazioneWeb
        Public blocco_ribaltamento As Integer
        Public richiamo_a_buon_fine As Boolean
        Public IdPrenotazione As String '[IDPREN_esterno] [int] NULL,
        Public TipoPrenotazione As TipoOperazione '[TipoPrenotazione] [int] NULL, Gestisce la modifica cencallazione nel tracciato Xml
        Public provenienza_ribaltamento As OrigineImport
        Public NumPrenotazione As String '[NUMPREN] [nchar](10) NULL, si ma deve essere un valore intero!!!!!!!!!!
        Public CodNumPrenotazione As String 'Per dollar e drifty memorizzo qui il codice prenotazione alfanumerico
        Public DataPrenotazione As String '[DATAPREN] [datetime] NULL,

        Public id_gruppo_auto As String '[id_gruppo] [int] NULL,
        Public codice_gruppo_auto As String '[cod_gruppo] [nvarchar](3) NULL,
        Public id_gruppo_da_consegnare As String '[id_gruppo_da_consegnare] [int] NULL,
        Public codice_gruppo_da_consegnare As String '[cod_gruppo_da_consegnare] [nvarchar](3) NULL,

        '[stato] [int] NULL,

        Public data_uscita As String '[data_out] [datetime] NULL,
        Public Ora_uscita As String
        Public Min_uscita As String
        Public StazionePickUp As String '[STA_OUT] [int] NULL,
        Public CodiceStazionePickUp As String '[cod_sta_out] [nvarchar](20) NULL,
        Public volo_uscita As String '[volo_out] [nvarchar](100) NULL,

        Public data_rientro As String '[data_in] [datetime] NULL,
        Public Ora_rientro As String
        Public Min_rientro As String
        Public StazioneDropOff As String '[STA_IN] [int] NULL,
        Public CodiceStazioneDropOff As String '[cod_sta_in] [nvarchar](20) NULL,
        Public volo_rientro As String '[volo_in] [nvarchar](100) NULL,

        Public Note As String

        Public id_cliente_web As String '[id_cliente_web] [int] NULL,
        Public Nome As String '[nome] [nvarchar](50) NULL,
        Public Cognome As String '[cognome] [nvarchar](50) NULL,
        Public data_nascita As String '[data_nascita] [date] NULL,
        Public LuogoNascita As String '[luogo_nascita] [nvarchar](50) NULL,
        Public CodNazioneNascita As String '[CodNazioneNascita] [nvarchar](3) NULL,
        Public CodiceFiscale As String '[codfisc] [nvarchar](20) NULL,

        Public Mail As String '[email] [nvarchar](50) NULL,
        Public Indirizzo As String '[indirizzo] [nvarchar](100) NULL,
        Public Citta As String '[citta] [nvarchar](50) NULL,
        Public cap As String '[cap] [nvarchar](20) NULL,
        Public provincia As String '[provincia] [nvarchar](50) NULL,
        Public nazione As String '[nazione] [nvarchar](50) NULL,
        Public Telefono As String '[telefono] [nvarchar](20) NULL,
        Public Fax As String '[fax] [nvarchar](50) NULL,
        Public cell As String '[cell] [nvarchar](50) NULL,
        Public PatenteNumero As String '[patente_num] [nvarchar](20) NULL,
        Public PatenteLuogo As String '[patente_ril] [nvarchar](50) NULL,
        Public PatenteDataRilascio As String '[data_pat_rilascio] [date] NULL,
        Public PatenteScadenza As String '[scad_patente] [date] NULL,

        Public Az_flag As Boolean ' [flag_azienda] [bit] NULL,
        Public Az_id As String  '[id_azienda] [int] NULL,
        Public Az_nome As String '[nome_azienda] [nvarchar](100) NULL,
        Public Az_indirizzo As String '[indirizzo_az] [nvarchar](100) NULL,
        Public Az_citta As String '[citta_az] [nvarchar](100) NULL,
        Public Az_cap As String '[cap_az] [nvarchar](50) NULL,
        Public Az_prov As String '[prov_az] [nvarchar](50) NULL,
        Public Az_tel As String '[tel_az] [nvarchar](50) NULL,
        Public Az_fax As String '[fax_az] [nvarchar](50) NULL,
        Public Az_cell As String '[cell_az] [nvarchar](50) NULL,
        Public Az_mail As String '[email_az] [nvarchar](100) NULL,
        Public Az_piva As String '[piva] [nvarchar](50) NULL,

        Public gruppi_speciali As String '[gruppi_spec] [bit] NULL,

        Public id_tour_operator As String '[id_tour_operator] [int] NULL,

        Public id_tariffa As String '[idtariffa] [int] NULL,
        Public id_tariffe_righe As String
        Public tipo_tariffa As String
        Public CodiceTariffa As String
        Public CodiceTariffaRibaltamento As String '[codtar] [nvarchar](50) NULL,
        Public Sconto As String '[sconto] [float] NULL,
        Public tipo_sconto As String
        Public codice_convenzione As String
        Public EtaPrimo As String
        Public EtaSecondo As String
        Public NumeroGiorni As String
        Public Supplementi As String '[supplementi] [nvarchar](50) NULL,

        Public Totale As String '[totale] [float] NULL,
        Public COD_CONV As String '[COD_CONV] [nvarchar](30) NULL,
        Public CCNUMAUT As String '[CCNUMAUT] [nvarchar](50) NULL,
        Public CCDATA As String '[CCDATA] [datetime] NULL,
        Public CCIMPORTO As String '[CCIMPORTO] [decimal](18, 2) NULL,
        Public CCNUMOPE As String '[CCNUMOPE] [nvarchar](50) NULL,
        Public CCRISP As String '[CCRISP] [nvarchar](50) NULL,
        Public CCTRANS As String '[CCTRANS] [nvarchar](50) NULL,
        Public CCOMPAGNIA As String '[CCOMPAGNIA] [nvarchar](50) NULL,
        Public TRANSOK As Boolean '[TRANSOK] [bit] NULL,
        Public TERMINAL_ID As String '[TERMINAL_ID] [nvarchar](50) NULL,

        Public codici_errore As String
        Public str_data_nascita As String
        Public str_data_ril_patente As String
        Public str_scad_patente As String
        Public codici_acessori_errati As String

        Public importo_a_carico_del_broker_ribaltato As Double
        Public gg_a_carico_del_broker_ribaltato As Integer

        '[id] [bigint] IDENTITY(1,1) NOT NULL,
        '[provenienza_replica] [int] NULL,

        '[impbase] [float] NULL,

        '[SCARICATADA] [int] NULL,
        '[DATA_SCARICO] [datetime] NULL,

    End Class

    Public Shared Function GeneraIdFattura_Web(ByVal AnnoFattura As Integer) As String
        GeneraIdFattura_Web = Contatori.getContatore_fatture_web(AnnoFattura)
    End Function

    Public Shared Function SalvaFattura_Web(ByVal CodiceFattura As Integer, ByVal IdDitta As String, ByVal NumPrenotazione As String, ByVal miaPrenotazioneWeb As PrenotazioneWeb, ByVal id_pagamento As Integer) As Integer
        'CREATE TABLE [dbo].[FATTURE_PAGAMENTI](
        '	[ANNO_FATT] [int] NULL, anno corrente
        '	[DATA_FATT] [datetime] NULL, 
        '	[NUM_FATT] [int] NULL, da tabella codici per fattura (riparte da 1 ogni inizio anno?)
        '	[Nr_Contratto] [int] NULL, numero contratto
        '	[TIPO] [nvarchar](5) NULL, 'XX'
        '	[TIPOLOGIA] [nvarchar](5) NULL, 'PAGAM'
        '	[NOL_AUTISTA] [bit] NOT NULL, false
        '	[IMPORTO_FATT] [money] NULL, Totale
        '	[ASALDO_FATT] [money] NULL, 0
        '	[ID_Cliente] [int] NULL, ottenuto da precedente funzione
        '	[ID_PARCO] [int] NULL, 
        '	[ID_STAZIONE_OUT] [int] NULL, stazione uscita
        '	[ID_STAZIONE_IN] [int] NULL, stazione ingresso
        '	[s_GUID] [uniqueidentifier] NULL, per repliche
        '	[USCMIN] [datetime] NULL, orario effettivo di uscita
        '	[RIEMAX] [datetime] NULL, orario effettivo di rientro
        '	[GIORNI_NOLEGGIO] [int] NULL,
        '	[KM_OUT] [int] NULL,
        '	[KM_IN] [int] NULL,
        '	[LT_IN] [int] NULL,
        '	[LT_OUT] [int] NULL,
        '	[CONTABILIZZATA] [bit] NOT NULL,
        '	[ASSFIS1] [nvarchar](50) NULL, codice iva (da tabella codici iva... di solito '021')
        '	[IMPON1] [money] NULL, imponibile???
        '	[IMPIVA1] [money] NULL, imponibile iva???
        '	[ASSFIS2] [nvarchar](50) NULL, ----- Uitlizzati se ho altri importi con iva differente
        '	[IMPON2] [money] NULL, ----
        '	[IMPIVA2] [money] NULL, ----
        '	[ASSFIS3] [nvarchar](50) NULL, ----- Uitlizzati se ho altri importi con iva differente
        '	[IMPON3] [money] NULL, ----
        '	[IMPIVA3] [money] NULL, ----
        '	[DATAMOD] [datetime] NULL,
        '	[UTEMOD] [nvarchar](10) NULL,
        '	[STA_IX] [int] NULL,
        '	[DES1] [nvarchar](50) NULL, 'INCASSO DA PAGAMENTO N° xxxxx DEL dd/mm/yyyy'
        '	[DES2] [nvarchar](50) NULL,
        '	[DES3] [nvarchar](50) NULL,
        '	[DES4] [nvarchar](50) NULL,
        '	[DES5] [nvarchar](50) NULL,
        '	[DES6] [nvarchar](50) NULL,
        '	[DES7] [nvarchar](50) NULL,
        '	[DES8] [nvarchar](50) NULL,
        '	[DES9] [nvarchar](50) NULL,
        '	[DES10] [nvarchar](50) NULL,
        '	[QCLI1] [int] NULL, 1 (= quantità venduta)
        '	[QCLI2] [int] NULL,
        '	[QCLI3] [int] NULL,
        '	[QCLI4] [int] NULL,
        '	[QCLI5] [int] NULL,
        '	[QCLI6] [int] NULL,
        '	[QCLI7] [int] NULL,
        '	[QCLI8] [int] NULL,
        '	[QCLI9] [int] NULL,
        '	[QCLI10] [int] NULL,
        '	[CCLI1] [money] NULL, Totale al netto d'iva
        '	[CCLI2] [money] NULL,
        '	[CCLI3] [money] NULL,
        '	[CCLI4] [money] NULL,
        '	[CCLI5] [money] NULL,
        '	[CCLI6] [money] NULL,
        '	[CCLI7] [money] NULL,
        '	[CCLI8] [money] NULL,
        '	[CCLI9] [money] NULL,
        '	[CCLI10] [money] NULL,
        '	[TCLI1] [money] NULL, Totale al netto d'iva
        '	[TCLI2] [money] NULL,
        '	[TCLI3] [money] NULL,
        '	[TCLI4] [money] NULL,
        '	[TCLI5] [money] NULL,
        '	[TCLI6] [money] NULL,
        '	[TCLI7] [money] NULL,
        '	[TCLI8] [money] NULL,
        '	[TCLI9] [money] NULL,
        '	[TCLI10] [money] NULL,
        '	[IVA1] [nvarchar](3) NULL, codice iva
        '	[IVA2] [nvarchar](3) NULL,
        '	[IVA3] [nvarchar](3) NULL,
        '	[IVA4] [nvarchar](3) NULL,
        '	[IVA5] [nvarchar](3) NULL,
        '	[IVA6] [nvarchar](3) NULL,
        '	[IVA7] [nvarchar](3) NULL,
        '	[IVA8] [nvarchar](3) NULL,
        '	[IVA9] [nvarchar](3) NULL,
        '	[IVA10] [nvarchar](3) NULL,
        '	[TIPO1] [nvarchar](5) NULL, NULL o stringa vuota
        '	[TIPO2] [nvarchar](5) NULL,
        '	[TIPO3] [nvarchar](5) NULL,
        '	[TIPO4] [nvarchar](50) NULL,
        '	[TIPO5] [nvarchar](5) NULL,
        '	[TIPO6] [nvarchar](5) NULL,
        '	[TIPO7] [nvarchar](5) NULL,
        '	[TIPO8] [nvarchar](5) NULL,
        '	[TIPO9] [nvarchar](5) NULL,
        '	[TIPO10] [nvarchar](5) NULL,
        '	[DAT1] [datetime] NULL, data pagamento
        '	[DAT2] [datetime] NULL,
        '	[DAT3] [datetime] NULL,
        '	[DAT4] [datetime] NULL,
        '	[DAT5] [datetime] NULL,
        '	[DAT6] [datetime] NULL,
        '	[TIP1] [int] NULL, tipo operazione da tabelle...
        '	[TIP2] [int] NULL,
        '	[TIP3] [int] NULL,
        '	[TIP4] [int] NULL,
        '	[TIP5] [int] NULL,
        '	[TIP6] [int] NULL,
        '	[MOD1] [int] NULL, modalita pagamento da tabelle...
        '	[MOD2] [int] NULL,
        '	[MOD3] [int] NULL,
        '	[MOD4] [int] NULL,
        '	[MOD5] [int] NULL,
        '	[MOD6] [int] NULL,
        '	[IMP1] [money] NULL,
        '	[IMP2] [money] NULL,
        '	[IMP3] [money] NULL,
        '	[IMP4] [money] NULL,
        '	[IMP5] [money] NULL,
        '	[IMP6] [money] NULL,
        '	[NOTE] [nvarchar](max) NULL,
        '	[ID_COMMITTENTE] [int] NULL,
        '	[ID_COMMISSIONE] [int] NULL,
        '	[DATA_FATT_COMMISSIONE] [datetime] NULL,
        '	[NUMERO_FATT_COMMISSIONE] [int] NULL,
        '	[RIF_PRENOTAZIONE] [int] NULL,
        '	[DATACRE] [datetime] NULL, utente
        '	[UTECRE] [nvarchar](15) NULL creazione
        ') ON [PRIMARY]

        SalvaFattura_Web = -1
        Dim sqlStr As String

        Dim errline As Integer = 0


        Try
            With miaPrenotazioneWeb
                Dim DataFattura As String = "convert(datetime,'" & Libreria.FormattaData(.CCDATA) & " 00:00:00',102)"
                errline = 1
                Dim AnnoFattura As Integer = Year(.CCDATA)
                errline = 2
                Dim IdFattura As String = CodiceFattura
                errline = 3
                Dim GiorniNoleggio As String = .NumeroGiorni
                If GiorniNoleggio = "" Then
                    GiorniNoleggio = "NULL"
                End If
                errline = 4
                Dim DataUscita As String = "convert(datetime,'" & Libreria.FormattaData(.data_uscita) & " 00:00:00',102)"
                errline = 5
                Dim DataRientro As String = "convert(datetime,'" & Libreria.FormattaData(.data_rientro) & " 00:00:00',102)"
                errline = 6
                Dim TipoOperazione As String = Costanti.id_pagamento_web_p1000
                errline = 7
                Dim ModalitaPagamento As String = Pagamenti.get_id_mod_pag_web(.CCOMPAGNIA)
                errline = 8
                Dim CodiceIva As String = Costanti.CodiceIvaWeb
                Dim Totale As Double = Double.Parse(.CCIMPORTO)
                Dim TotaleImponibile As Double = Libreria.ArrotondaDouble(Totale / (1 + Costanti.ValoreIvaWeb / 100))
                Dim TotaleIva As Double = Totale - TotaleImponibile
                'Trace.Write(.CCIMPORTO & " " & Totale & " " & TotaleImponibile & " " & TotaleIva)
                errline = 11
                Dim Codice_EDP As String = getCodiceEDPDaId(IdDitta)
                If Codice_EDP = "" Then
                    Codice_EDP = "NULL"
                End If
                errline = 12

                sqlStr = "INSERT INTO FATTURE_PAGAMENTI (ANNO_FATT,DATA_FATT,NUM_FATT,Nr_Contratto,TIPO,TIPOLOGIA,NOL_AUTISTA," &
                    "IMPORTO_FATT,ASALDO_FATT,ID_DITTA,ID_Cliente,ID_STAZIONE_OUT,ID_STAZIONE_IN,USCMIN,RIEMAX,GIORNI_NOLEGGIO," &
                    "CONTABILIZZATA,ASSFIS1,IMPON1,IMPIVA1,DES1,QCLI1,CCLI1,TCLI1,IVA1,DAT1,TIP1,MOD1,RIF_PRENOTAZIONE," &
                    "DATACRE,UTECRE) VALUES (" &
                    AnnoFattura & "," &
                    DataFattura & "," &
                    IdFattura & "," &
                    NumPrenotazione & "," &
                    " 'XX'," &
                    " 'PAGAM'," &
                    " 0," &
                    "'" & .Totale.Replace(",", ".") & "'," &
                    " 0," &
                    IdDitta & "," &
                    Codice_EDP & "," &
                    .CodiceStazionePickUp & "," &
                    .CodiceStazioneDropOff & "," &
                    DataUscita & "," &
                    DataRientro & "," &
                    GiorniNoleggio & "," &
                    " 0," &
                    CodiceIva & "," &
                    (TotaleImponibile & "").Replace(",", ".") & "," &
                    (TotaleIva & "").Replace(",", ".") & "," &
                    "'INCASSO PAGAMENTO N° " & id_pagamento & " DEL " & Format(Date.Parse(.CCDATA), "dd/MM/yyyy") & "'," &
                    " 1," &
                    "'" & (TotaleImponibile & "").Replace(",", ".") & "'," &
                    "'" & (TotaleImponibile & "").Replace(",", ".") & "'," &
                    CodiceIva & "," &
                    DataFattura & "," &
                    TipoOperazione & "," &
                    ModalitaPagamento & "," &
                    .NumPrenotazione & "," &
                    " convert(datetime,getdate(),102)," &
                    "'" & Libreria.formattaSqlTrim(HttpContext.Current.Request.Cookies("SicilyRentCar")("nome"), 15) & "')"

                'Trace.Write(sqlStr)

                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteNonQuery()
                Cmd.Dispose()
                Cmd = Nothing

                sqlStr = "SELECT @@IDENTITY FROM FATTURE_PAGAMENTI"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                Dim id_fattura As String = Cmd.ExecuteScalar & ""
                If id_fattura = "" Then
                    id_fattura = "-1"
                End If
                Cmd.Dispose()
                Cmd = Nothing

                SalvaFattura_Web = Integer.Parse(id_fattura)

                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

            End With
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni salva Fattura web :" & errline.ToString & " <br/> " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try


    End Function

    Public Shared Function getCodiceEDPDaId(IdDitta As String) As String
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT [CODICE EDP] FROM DITTE WHERE Id_Ditta = " & IdDitta, Dbc)

        getCodiceEDPDaId = Cmd.ExecuteScalar & ""

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function


    Public Shared Function GetMailStazione(id_stazione As Integer) As String
        Dim res As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT [email] FROM stazioni WHERE [ID] = " & id_stazione, Dbc)

            res = Cmd.ExecuteScalar & ""

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni GetMailStazione  : <br/>" & ex.Message & "<br/>" & "<br/>")

        End Try

        Return res


    End Function



End Class
