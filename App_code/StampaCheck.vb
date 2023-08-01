Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class DatiStampaCheck
    Public PARCHEGGIO As String
    Public TRASFERIMENTO_AUTO As String
    Public MARCAMODELLO As String
    Public TARGA As String
    Public CONTRATTO As String

    Public KM_OUT As String
    Public DATAEORA_OUT As String

    Public DATAEORA_OUT_PREVISTA As String 'aggiunta 21.06.2022 salvo


    Public CARBURANTE_OUT As String
    Public NOTE_OUT As String
    Public DANNI_OUT_1 As String
    Public DANNI_OUT_2 As String
    Public DANNI_OUT_3 As String
    Public ACCESSORI_OUT_1 As String
    Public ACCESSORI_OUT_2 As String
    Public ACCESSORI_OUT_3 As String

    Public KM_IN As String
    Public DATAEORA_IN As String
    Public CARBURANTE_IN As String
    Public NOTE_IN As String
    Public DANNI_IN_1 As String
    Public DANNI_IN_2 As String
    Public DANNI_IN_3 As String
    Public ACCESSORI_IN_1 As String
    Public ACCESSORI_IN_2 As String
    Public ACCESSORI_IN_3 As String
    Public ID_VEICOLO As String
    Public ID_GRUPPO_EVENTO As String
    Public TIPO_STAMPA As String
    Public gruppo_danni_rientro As String
    Public gruppo_danni_uscita As String


End Class

Public Class StampaCheck


    Public Shared Function GeneraDocumentoPDF(mie_dati As DatiStampaCheck, numcontratto As String) As String
        'Aggiunta per generazione CkIN in PDF 31.05.2022 Salvo

        Dim pdfTemplate As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\modelli_documenti\ModuloCheck.pdf"
        Dim PdfReader As PdfReader = New PdfReader(pdfTemplate)
        Dim PdfStamper As PdfStamper = Nothing
        Dim ms As MemoryStream = Nothing

        Dim n_contratto As String = numcontratto
        Dim ris As String = ""


        Try


            'Creazione stringhe per file PDF 31.05.2022 Salvo
            Dim newDir As String = HttpContext.Current.Server.MapPath("\allegati_pren_cnt\" & n_contratto)
            Dim newFile As String = HttpContext.Current.Server.MapPath("\allegati_pren_cnt\" & n_contratto & "\CI_" & n_contratto & ".pdf") 'sigla contratto rientro RB

            'crea cartella se non esiste
            If Directory.Exists(newDir) = False Then
                Directory.CreateDirectory(newDir)
            End If

            'se file da creare esiste lo elimina e lo ricrea 
            If File.Exists(newFile) Then
                File.Delete(newFile)
            End If

            'Return False 'test
            'Exit Function 'test
            'END Creazione stringhe per file PDF 31.05.2022 Salvo


            'rem su creazione file pdf
            'ms = New MemoryStream()
            'PdfStamper = New PdfStamper(PdfReader, ms)
            'Dim pdfFormFields As AcroFields = PdfStamper.AcroFields
            'end rem su creazione file pdf


            Using file_stream As FileStream = New FileStream(newFile, FileMode.Create)
                Dim myPdfReader As PdfReader = New PdfReader(pdfTemplate)

                Using myPdfStamper As PdfStamper = New PdfStamper(myPdfReader, file_stream)

                    Dim pdfFormFields As AcroFields = myPdfStamper.AcroFields

                    With mie_dati

                        n_contratto = .CONTRATTO


                        pdfFormFields.SetField("PARCHEGGIO", .PARCHEGGIO)
                        pdfFormFields.SetField("TRASFERIMENTO AUTO", .TRASFERIMENTO_AUTO)

                        'Modificato valore del campo MARCAMODELLO 21.06.2022
                        'pdfFormFields.SetField("MARCAMODELLO", .MARCAMODELLO)
                        'pdfFormFields.SetField("TARGA", .TARGA)
                        pdfFormFields.SetField("MODELLOTARGA", .MARCAMODELLO & " / " & .TARGA)



                        pdfFormFields.SetField("CONTRATTO", .CONTRATTO)

                        pdfFormFields.SetField("KM-OUT", .KM_OUT)
                        pdfFormFields.SetField("DATAEORA-OUT", .DATAEORA_OUT)

                        pdfFormFields.SetField("PRESUNTORIENTRO", .DATAEORA_OUT_PREVISTA)        'aggiunto 21.06.2022


                        pdfFormFields.SetField("CARBURANTE-OUT", .CARBURANTE_OUT)
                        pdfFormFields.SetField("NOTE-OUT", .NOTE_OUT)
                        pdfFormFields.SetField("DANNI-OUT-1", .DANNI_OUT_1)
                        pdfFormFields.SetField("DANNI-OUT-2", .DANNI_OUT_2)
                        pdfFormFields.SetField("DANNI-OUT-3", .DANNI_OUT_3)
                        pdfFormFields.SetField("ACCESSORI-OUT-1", .ACCESSORI_OUT_1)
                        pdfFormFields.SetField("ACCESSORI-OUT-2", .ACCESSORI_OUT_2)
                        pdfFormFields.SetField("ACCESSORI-OUT-3", .ACCESSORI_OUT_3)

                        pdfFormFields.SetField("KM-IN", .KM_IN)
                        pdfFormFields.SetField("DATAEORA-IN", .DATAEORA_IN)
                        pdfFormFields.SetField("CARBURANTE-IN", .CARBURANTE_IN)
                        pdfFormFields.SetField("NOTE-IN", .NOTE_IN)
                        pdfFormFields.SetField("DANNI-IN-1", .DANNI_IN_1)
                        pdfFormFields.SetField("DANNI-IN-2", .DANNI_IN_2)
                        pdfFormFields.SetField("DANNI-IN-3", .DANNI_IN_3)
                        pdfFormFields.SetField("ACCESSORI-IN-1", .ACCESSORI_IN_1)
                        pdfFormFields.SetField("ACCESSORI-IN-2", .ACCESSORI_IN_2)
                        pdfFormFields.SetField("ACCESSORI-IN-3", .ACCESSORI_IN_3)

                        'pdfFormFields.SetField("FIRMA-IN", "firma")


                        Dim top As Integer
                        Dim left As Integer

                        Dim img As Image
                        Dim x As String
                        Dim y As String
                        Dim i As Integer = 1
                        Dim r As Integer = 1
                        Dim k As Integer = 1
                        Dim nome As String
                        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        Dbc.Open()


                        'IMPORTANTISSIMO 1 - NON RIMUOVERE ALTRIMENTI DA ERRORE
                        If .TIPO_STAMPA = "check_out" And .gruppo_danni_rientro = "" Then
                            .gruppo_danni_rientro = .gruppo_danni_uscita
                        End If

                        Dim left_fissa As Integer = 7
                        Dim top_fisso As Integer = 341

                        Dim sqlstr As String = "SELECT DISTINCT im.* FROM veicoli v WITH(NOLOCK)" &
                " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON v.id_modello = am.id_modello" &
                " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello," & Costanti.id_mappa_default & ") AND im.tipo_img = 1" &
                " INNER JOIN veicoli_danni d WITH(NOLOCK) ON d.id_posizione_danno = im.id_posizione_danno AND v.id = d.id_veicolo AND d.stato = 1" &
                " WHERE v.id = '" & .ID_VEICOLO & "' AND attivo = 1" &
                " ORDER BY im.id_posizione_danno DESC"

                        Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                        Dim id_posizione_danno As String = ""
                        Dim giapresente As String = ""

                        Dim Rs As Data.SqlClient.SqlDataReader
                        Try

                            Rs = Cmd.ExecuteReader()

                            Do While Rs.Read
                                id_posizione_danno = Rs("id_posizione_danno") & ""
                                giapresente = Get_gia_presente(id_posizione_danno, .gruppo_danni_rientro, .gruppo_danni_uscita)
                                If CInt(giapresente) > 0 Then
                                    x = Rs("x") & ""
                                    y = Rs("y") & ""
                                    If x & "" <> "" Or y & "" <> "" Then
                                        If i < 10 Then
                                            nome = "cerchio_0" & i & ".png"
                                        ElseIf i > 49 Then
                                            nome = "cerchio.png"
                                        Else
                                            nome = "cerchio_" & i & ".png"
                                        End If
                                        left = (CInt(x) - 10) * 72 / 96
                                        left = left_fissa + left


                                        top = (CInt(y) - 10) * 72 / 96
                                        top = top_fisso - top

                                        img = Image.GetInstance(New Uri(HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\images\Icone\" & nome))
                                        myPdfStamper.GetOverContent(1).AddImage(img, 15, 0, 0, 15, left, top)

                                    End If
                                End If

                                If giapresente <> -1 Then
                                    i = i + 1
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
                            HttpContext.Current.Response.Write("error GeneraDocumento StampaCheck IMPORTANTISSIMO 1  : <br/>" & ex.Message & "<br/>" & "<br/>")
                        End Try



                        If .TIPO_STAMPA = "check_out" Then

                            Try

                                Dim Dbc4 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                                Dbc4.Open()

                                Dim left_fissa_out2 As Integer = 433
                                Dim top_fisso_out2 As Integer = 341
                                Dim Cmd4 As New Data.SqlClient.SqlCommand("SELECT DISTINCT im.* FROM veicoli v WITH(NOLOCK)" &
                        " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON v.id_modello = am.id_modello" &
                        " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello," & Costanti.id_mappa_default & ") AND im.tipo_img = 1" &
                        " INNER JOIN veicoli_danni d WITH(NOLOCK) ON d.id_posizione_danno = im.id_posizione_danno AND v.id = d.id_veicolo AND d.stato = 1" &
                        " WHERE v.id = '" & .ID_VEICOLO & "' AND attivo = 1" &
                        " ORDER BY im.id_posizione_danno DESC", Dbc4)

                                Dim id_posizione_danno2 As String
                                Dim giapresente2 As String = ""
                                Dim Rs4 As Data.SqlClient.SqlDataReader
                                Rs4 = Cmd4.ExecuteReader()
                                Do While Rs4.Read


                                    id_posizione_danno2 = Rs4("id_posizione_danno") & ""
                                    giapresente2 = Get_gia_presente(id_posizione_danno2, .gruppo_danni_rientro, .gruppo_danni_uscita)
                                    If CInt(giapresente2) > 0 Then
                                        x = Rs4("x") & ""
                                        y = Rs4("y") & ""
                                        If x & "" <> "" Or y & "" <> "" Then
                                            If k < 10 Then
                                                nome = "cerchio_0" & k & ".png"
                                            ElseIf k > 49 Then
                                                nome = "cerchio.png"
                                            Else
                                                nome = "cerchio_" & k & ".png"
                                            End If
                                            left = (CInt(x) - 10) * 72 / 96
                                            left = left_fissa_out2 + left

                                            top = (CInt(y) - 10) * 72 / 96
                                            top = top_fisso_out2 - top
                                            img = Image.GetInstance(New Uri(HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\images\Icone\" & nome))

                                            myPdfStamper.GetOverContent(1).AddImage(img, 15, 0, 0, 15, left, top)

                                        End If
                                    End If

                                    If giapresente2 <> -1 Then
                                        k = k + 1
                                    End If
                                Loop
                                Rs4.Close()
                                Rs4 = Nothing
                                Cmd4.Dispose()
                                Cmd4 = Nothing
                                Dbc4.Close()
                                Dbc4.Dispose()
                                Dbc4 = Nothing

                            Catch ex As Exception
                                HttpContext.Current.Response.Write("error GeneraDocumento StampaCheck 2  : <br/>" & ex.Message & "<br/>" & "<br/>")
                            End Try


                        End If







                        ' '--------------STAMPA CHECK IN 2

                        If .TIPO_STAMPA = "check_in" Then


                            Try
                                Dim left_fissa_out As Integer = 433
                                Dim top_fisso_out As Integer = 341
                                Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                                Dbc2.Open()
                                Dim Cmd2 As New Data.SqlClient.SqlCommand("SELECT DISTINCT im.* FROM veicoli v WITH(NOLOCK)" &
                       " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON v.id_modello = am.id_modello" &
                       " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello," & Costanti.id_mappa_default & ") AND im.tipo_img = 1" &
                       " INNER JOIN veicoli_danni d WITH(NOLOCK) ON d.id_posizione_danno = im.id_posizione_danno AND v.id = d.id_veicolo AND d.stato = 1" &
                       " WHERE v.id = '" & .ID_VEICOLO & "' AND attivo = 1" &
                       " ORDER BY im.id_posizione_danno DESC", Dbc2)

                                HttpContext.Current.Trace.Write(Cmd2.CommandText)

                                Dim id_posizione_danno2 As String = ""

                                Dim Rs2 As Data.SqlClient.SqlDataReader
                                Rs2 = Cmd2.ExecuteReader()
                                Do While Rs2.Read
                                    id_posizione_danno2 = Rs2("id_posizione_danno") & ""
                                    If danno_di_questo_gruppo(id_posizione_danno2, .gruppo_danni_rientro, .gruppo_danni_uscita) Then
                                        Try

                                            x = Rs2("x") & ""
                                            y = Rs2("y") & ""
                                            If x & "" <> "" Or y & "" <> "" Then
                                                If r < 10 Then
                                                    nome = "cerchio_0" & r & ".png"
                                                ElseIf r > 49 Then
                                                    nome = "cerchio.png"
                                                Else
                                                    nome = "cerchio_" & r & ".png"
                                                End If
                                                left = (CInt(x) - 10) * 72 / 96
                                                left = left_fissa_out + left

                                                top = (CInt(y) - 10) * 72 / 96
                                                top = top_fisso_out - top
                                                img = Image.GetInstance(New Uri(HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\images\Icone\" & nome))

                                                myPdfStamper.GetOverContent(1).AddImage(img, 15, 0, 0, 15, left, top)
                                            End If
                                        Catch ex As Exception

                                        End Try
                                        r = r + 1

                                        'danni trovati

                                    Else
                                        'nessun danno


                                    End If
                                Loop
                                Rs2.Close()
                                Rs2 = Nothing
                                Cmd2.Dispose()
                                Cmd2 = Nothing
                                Dbc2.Close()
                                Dbc2.Dispose()
                                Dbc2 = Nothing



                                'stampa firma 25.05.2022 e 21.06.2022
                                Dim pathfilefirma As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\firme_contratti\pick_up\"

                                Dim filefirma As String = pathfilefirma & n_contratto & "_RB-trasp.png"
                                Dim firma_def As String = n_contratto & "_RB-trasp.png"

                                'se presente la firma di rientro la visualizza 25.05.2022
                                'firma rientro IN
                                'modificati il 21.06.2022
                                Dim xf As Integer = 690 'Left
                                Dim yf As Integer = 23  'Top
                                Dim wf As Integer = 120  'width
                                Dim hf As Integer = 80  'height

                                If File.Exists(filefirma) Then
                                    Dim img_firma As Image
                                    img_firma = Image.GetInstance(New Uri(HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\firme_contratti\pick_up\" & firma_def))
                                    myPdfStamper.GetOverContent(1).AddImage(img_firma, wf, 0, 0, hf, xf, yf)
                                    'PdfStamper.GetOverContent(1).AddImage(img_firma, 80, 0, 0, 40, 695(left), 20(top)
                                Else
                                End If

                                'inserisce x per danni 25.05.2022
                                Dim img_x As Image
                                img_x = Image.GetInstance(New Uri(HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\images\Icone\x.png"))

                                'vecchio modulo
                                'Dim xf As Integer = 646 'Left
                                'Dim yf As Integer = 52  'Top

                                'Nuovo modulo 21.06.2022
                                xf = 445 'Left
                                yf = 54  'Top
                                wf = 10 'width
                                hf = 10 'height

                                'id_posizione_danno2 = "" 'test nessun danno

                                Dim danni_checkIN As Boolean = False

                                danni_checkIN = funzioni_comuni_new.GetDanniCheckIN(n_contratto)

                                If danni_checkIN = False Then 'nessun danno trovato al rientro
                                    yf += 5
                                    myPdfStamper.GetOverContent(1).AddImage(img_x, 10, 0, 0, 10, xf, yf)
                                Else
                                    'trovati danni esistenti
                                    yf -= 5
                                    myPdfStamper.GetOverContent(1).AddImage(img_x, 10, 0, 0, 10, xf, yf)
                                End If


                            Catch ex As Exception
                                HttpContext.Current.Response.Write("error GeneraDocumento ---STAMPA CHECK IN 2  : <br/>" & ex.Message & "<br/>" & "<br/>")
                            End Try

                        End If

                        'End If

                    End With

                    myPdfStamper.FormFlattening = True
                    myPdfStamper.Close()
                End Using




                myPdfReader.Close()
            End Using

            GeneraDocumentoPDF = newFile

            'FORMULA PER CONVERTIRE I PIXEL IN PUNTI points = pixels * 72 / 96

            'PdfStamper.FormFlattening = True
            'PdfStamper.Close()

        Catch ex As Exception

            HttpContext.Current.Response.Write("Errore in GeneraDocumento:err_n_GenDOC:" & ex.Message)
            GeneraDocumentoPDF = ""
            'HttpContext.Current.Trace.Write("Errore in GeneraDocumento:" & ex.Message)
        End Try

        Return GeneraDocumentoPDF

    End Function




    Public Shared Function GeneraDocumento(mie_dati As DatiStampaCheck) As MemoryStream
        Dim pdfTemplate As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\modelli_documenti\ModuloCheck.pdf"
        Dim PdfReader As PdfReader = New PdfReader(pdfTemplate)
        Dim PdfStamper As PdfStamper = Nothing
        Dim ms As MemoryStream = Nothing


        Dim n_contratto As String = ""



        Try
            ms = New MemoryStream()

            PdfStamper = New PdfStamper(PdfReader, ms)
            Dim pdfFormFields As AcroFields = PdfStamper.AcroFields
            With mie_dati

                n_contratto = .CONTRATTO

                pdfFormFields.SetField("PARCHEGGIO", .PARCHEGGIO)
                pdfFormFields.SetField("TRASFERIMENTO AUTO", .TRASFERIMENTO_AUTO)

                'Modificato valore del campo MARCAMODELLO 21.06.2022
                'pdfFormFields.SetField("MARCAMODELLO", .MARCAMODELLO)
                'pdfFormFields.SetField("TARGA", .TARGA)
                pdfFormFields.SetField("MODELLOTARGA", .MARCAMODELLO & " / " & .TARGA)

                pdfFormFields.SetField("CONTRATTO", .CONTRATTO)

                pdfFormFields.SetField("KM-OUT", .KM_OUT)
                pdfFormFields.SetField("DATAEORA-OUT", .DATAEORA_OUT)

                pdfFormFields.SetField("PRESUNTORIENTRO", .DATAEORA_OUT_PREVISTA)        'aggiunto 21.06.2022

                pdfFormFields.SetField("CARBURANTE-OUT", .CARBURANTE_OUT)
                pdfFormFields.SetField("NOTE-OUT", .NOTE_OUT)
                pdfFormFields.SetField("DANNI-OUT-1", .DANNI_OUT_1)
                pdfFormFields.SetField("DANNI-OUT-2", .DANNI_OUT_2)
                pdfFormFields.SetField("DANNI-OUT-3", .DANNI_OUT_3)
                pdfFormFields.SetField("ACCESSORI-OUT-1", .ACCESSORI_OUT_1)
                pdfFormFields.SetField("ACCESSORI-OUT-2", .ACCESSORI_OUT_2)
                pdfFormFields.SetField("ACCESSORI-OUT-3", .ACCESSORI_OUT_3)

                pdfFormFields.SetField("KM-IN", .KM_IN)
                pdfFormFields.SetField("DATAEORA-IN", .DATAEORA_IN)
                pdfFormFields.SetField("CARBURANTE-IN", .CARBURANTE_IN)
                pdfFormFields.SetField("NOTE-IN", .NOTE_IN)
                pdfFormFields.SetField("DANNI-IN-1", .DANNI_IN_1)
                pdfFormFields.SetField("DANNI-IN-2", .DANNI_IN_2)
                pdfFormFields.SetField("DANNI-IN-3", .DANNI_IN_3)
                pdfFormFields.SetField("ACCESSORI-IN-1", .ACCESSORI_IN_1)
                pdfFormFields.SetField("ACCESSORI-IN-2", .ACCESSORI_IN_2)
                pdfFormFields.SetField("ACCESSORI-IN-3", .ACCESSORI_IN_3)

                'pdfFormFields.SetField("FIRMA-IN", "firma")


                Dim top As Integer
                Dim left As Integer

                Dim img As Image
                Dim x As String
                Dim y As String
                Dim i As Integer = 1
                Dim r As Integer = 1
                Dim k As Integer = 1
                Dim nome As String
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()


                'IMPORTANTISSIMO 1 - NON RIMUOVERE ALTRIMENTI DA ERRORE
                If .TIPO_STAMPA = "check_out" And .gruppo_danni_rientro = "" Then
                    .gruppo_danni_rientro = .gruppo_danni_uscita
                End If

                Dim left_fissa As Integer = 7
                Dim top_fisso As Integer = 341

                Dim sqlstr As String = "SELECT DISTINCT im.* FROM veicoli v WITH(NOLOCK)" &
                " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON v.id_modello = am.id_modello" &
                " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello," & Costanti.id_mappa_default & ") AND im.tipo_img = 1" &
                " INNER JOIN veicoli_danni d WITH(NOLOCK) ON d.id_posizione_danno = im.id_posizione_danno AND v.id = d.id_veicolo AND d.stato = 1" &
                " WHERE v.id = '" & .ID_VEICOLO & "' AND attivo = 1" &
                " ORDER BY im.id_posizione_danno DESC"

                Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                Dim id_posizione_danno As String = ""
                Dim giapresente As String = ""

                Dim Rs As Data.SqlClient.SqlDataReader
                Try

                    Rs = Cmd.ExecuteReader()

                    Do While Rs.Read
                        id_posizione_danno = Rs("id_posizione_danno") & ""
                        giapresente = Get_gia_presente(id_posizione_danno, .gruppo_danni_rientro, .gruppo_danni_uscita)
                        If CInt(giapresente) > 0 Then
                            x = Rs("x") & ""
                            y = Rs("y") & ""
                            If x & "" <> "" Or y & "" <> "" Then
                                If i < 10 Then
                                    nome = "cerchio_0" & i & ".png"
                                ElseIf i > 49 Then
                                    nome = "cerchio.png"
                                Else
                                    nome = "cerchio_" & i & ".png"
                                End If
                                left = (CInt(x) - 10) * 72 / 96
                                left = left_fissa + left


                                top = (CInt(y) - 10) * 72 / 96
                                top = top_fisso - top

                                img = Image.GetInstance(New Uri(HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\images\Icone\" & nome))
                                PdfStamper.GetOverContent(1).AddImage(img, 15, 0, 0, 15, left, top)

                            End If
                        End If

                        If giapresente <> -1 Then
                            i = i + 1
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
                    HttpContext.Current.Response.Write("error GeneraDocumento StampaCheck IMPORTANTISSIMO 1  : <br/>" & ex.Message & "<br/>" & "<br/>")
                End Try



                If .TIPO_STAMPA = "check_out" Then

                    Try

                        Dim Dbc4 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        Dbc4.Open()

                        Dim left_fissa_out2 As Integer = 433
                        Dim top_fisso_out2 As Integer = 341

                        Dim sqlCKOUT As String = "SELECT DISTINCT im.* FROM veicoli v WITH(NOLOCK)" &
                        " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON v.id_modello = am.id_modello" &
                        " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello," & Costanti.id_mappa_default & ") AND im.tipo_img = 1" &
                        " INNER JOIN veicoli_danni d WITH(NOLOCK) ON d.id_posizione_danno = im.id_posizione_danno AND v.id = d.id_veicolo AND d.stato = 1" &
                        " WHERE v.id = '" & .ID_VEICOLO & "' AND attivo = 1" &
                        " ORDER BY im.id_posizione_danno DESC"


                        Dim Cmd4 As New Data.SqlClient.SqlCommand(sqlCKOUT, Dbc4)

                        Dim id_posizione_danno2 As String
                        Dim giapresente2 As String = ""
                        Dim Rs4 As Data.SqlClient.SqlDataReader
                        Rs4 = Cmd4.ExecuteReader()
                        Do While Rs4.Read


                            id_posizione_danno2 = Rs4("id_posizione_danno") & ""
                            giapresente2 = Get_gia_presente(id_posizione_danno2, .gruppo_danni_rientro, .gruppo_danni_uscita)
                            If CInt(giapresente2) > 0 Then
                                x = Rs4("x") & ""
                                y = Rs4("y") & ""
                                If x & "" <> "" Or y & "" <> "" Then
                                    If k < 10 Then
                                        nome = "cerchio_0" & k & ".png"
                                    ElseIf k > 49 Then
                                        nome = "cerchio.png"
                                    Else
                                        nome = "cerchio_" & k & ".png"
                                    End If
                                    left = (CInt(x) - 10) * 72 / 96
                                    left = left_fissa_out2 + left

                                    top = (CInt(y) - 10) * 72 / 96
                                    top = top_fisso_out2 - top
                                    img = Image.GetInstance(New Uri(HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\images\Icone\" & nome))

                                    PdfStamper.GetOverContent(1).AddImage(img, 15, 0, 0, 15, left, top)
                                End If
                            End If

                            If giapresente2 <> -1 Then
                                k = k + 1
                            End If
                        Loop
                        Rs4.Close()
                        Rs4 = Nothing
                        Cmd4.Dispose()
                        Cmd4 = Nothing
                        Dbc4.Close()
                        Dbc4.Dispose()
                        Dbc4 = Nothing

                        'USCITA 21.06.2022 OUT
                        'stampa firma ck OUT rientro 25.05.2022 e 21.06.2022
                        Dim pathfilefirma As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\firme_contratti\pick_up\"

                        Dim filefirma As String = pathfilefirma & n_contratto & "-trasp.png"
                        Dim firma_def As String = n_contratto & "-trasp.png"

                        'se presente la firma di uscita la visualizza 21.06.2022
                        'firma rientro OUT
                        'modificati il 21.06.2022
                        Dim xf As Integer = 300 'Left
                        Dim yf As Integer = 23  'Top
                        Dim wf As Integer = 100  'width
                        Dim hf As Integer = 60  'height

                        If File.Exists(filefirma) Then
                            Dim img_firma As Image
                            img_firma = Image.GetInstance(New Uri(HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\firme_contratti\pick_up\" & firma_def))
                            PdfStamper.GetOverContent(1).AddImage(img_firma, wf, 0, 0, hf, xf, yf)
                            ''PdfStamper.GetOverContent(1).AddImage(img_firma, 80, 0, 0, 40, 695(left), 20(top)
                        Else
                            'firma nn presente

                        End If



                    Catch ex As Exception
                        HttpContext.Current.Response.Write("error GeneraDocumento StampaCheck 2  : <br/>" & ex.Message & "<br/>" & "<br/>")
                    End Try


                End If







                ' '--------------STAMPA CHECK IN 2

                If .TIPO_STAMPA = "check_in" Then


                    Try
                        Dim left_fissa_out As Integer = 433
                        Dim top_fisso_out As Integer = 341
                        Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        Dbc2.Open()


                        Dim sqlCKIN As String = "SELECT DISTINCT im.* FROM veicoli v WITH(NOLOCK)" &
                       " LEFT JOIN veicoli_img_associazione_modelli am WITH(NOLOCK) ON v.id_modello = am.id_modello" &
                       " INNER JOIN veicoli_img_mappatura im WITH(NOLOCK) ON im.id_img_modelli = ISNULL(am.id_img_modello," & Costanti.id_mappa_default & ") AND im.tipo_img = 1" &
                       " INNER JOIN veicoli_danni d WITH(NOLOCK) ON d.id_posizione_danno = im.id_posizione_danno AND v.id = d.id_veicolo AND d.stato = 1" &
                       " WHERE v.id = '" & .ID_VEICOLO & "' AND attivo = 1" &
                       " ORDER BY im.id_posizione_danno DESC"


                        Dim Cmd2 As New Data.SqlClient.SqlCommand(sqlCKIN, Dbc2)

                        HttpContext.Current.Trace.Write(Cmd2.CommandText)

                        Dim id_posizione_danno2 As String = ""

                        Dim Rs2 As Data.SqlClient.SqlDataReader
                        Rs2 = Cmd2.ExecuteReader()
                        Do While Rs2.Read
                            id_posizione_danno2 = Rs2("id_posizione_danno") & ""
                            If danno_di_questo_gruppo(id_posizione_danno2, .gruppo_danni_rientro, .gruppo_danni_uscita) Then
                                Try

                                    x = Rs2("x") & ""
                                    y = Rs2("y") & ""
                                    If x & "" <> "" Or y & "" <> "" Then
                                        If r < 10 Then
                                            nome = "cerchio_0" & r & ".png"
                                        ElseIf r > 49 Then
                                            nome = "cerchio.png"
                                        Else
                                            nome = "cerchio_" & r & ".png"
                                        End If
                                        left = (CInt(x) - 10) * 72 / 96
                                        left = left_fissa_out + left

                                        top = (CInt(y) - 10) * 72 / 96
                                        top = top_fisso_out - top
                                        img = Image.GetInstance(New Uri(HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\images\Icone\" & nome))

                                        PdfStamper.GetOverContent(1).AddImage(img, 15, 0, 0, 15, left, top)
                                    End If
                                Catch ex As Exception

                                End Try
                                r = r + 1

                                'danni trovati

                            Else
                                'nessun danno


                            End If
                        Loop
                        Rs2.Close()
                        Rs2 = Nothing
                        Cmd2.Dispose()
                        Cmd2 = Nothing
                        Dbc2.Close()
                        Dbc2.Dispose()
                        Dbc2 = Nothing


                        'stampa firma ck IN rientro 25.05.2022 e 21.06.2022
                        Dim pathfilefirma As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\firme_contratti\pick_up\"

                        Dim filefirma As String = pathfilefirma & n_contratto & "_RB-trasp.png"
                        Dim firma_def As String = n_contratto & "_RB-trasp.png"

                        'se presente la firma di rientro la visualizza 25.05.2022
                        'firma rientro IN
                        'modificati il 21.06.2022
                        Dim xf As Integer = 690 'Left
                        Dim yf As Integer = 23  'Top
                        Dim wf As Integer = 120  'width
                        Dim hf As Integer = 80  'height

                        'firma di rientro
                        If File.Exists(filefirma) Then
                            Dim img_firma As Image
                            img_firma = Image.GetInstance(New Uri(HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\firme_contratti\pick_up\" & firma_def))
                            PdfStamper.GetOverContent(1).AddImage(img_firma, wf, 0, 0, hf, xf, yf)
                            'PdfStamper.GetOverContent(1).AddImage(img_firma, 80, 0, 0, 40, 695(left), 20(top)

                            'nel caso di checkIN stampa anche la firma di OUT -13.07.2022 salvo NON VISUALIZZARE LA FIRMA DI USCITA
                            'firma di out 
                            'firma_def = n_contratto & "-trasp.png"
                            'xf = 300 'Left
                            'yf = 23  'Top
                            'wf = 100  'width
                            'hf = 60  'height
                            'img_firma = Image.GetInstance(New Uri(HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\firme_contratti\pick_up\" & firma_def))
                            'PdfStamper.GetOverContent(1).AddImage(img_firma, wf, 0, 0, hf, xf, yf)

                        Else

                        End If

                        'inserisce x per danni 25.05.2022
                        Dim img_x As Image
                        img_x = Image.GetInstance(New Uri(HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\images\Icone\x.png"))

                        'vecchio modulo
                        'Dim xf As Integer = 646 'Left
                        'Dim yf As Integer = 52  'Top

                        'Nuovo modulo 21.06.2022
                        xf = 445 'Left
                        yf = 54  'Top
                        wf = 10 'width
                        hf = 10 'height

                        'id_posizione_danno2 = "" 'test nessun danno

                        Dim danni_checkIN As Boolean = False

                        danni_checkIN = funzioni_comuni_new.GetDanniCheckIN(n_contratto)

                        If danni_checkIN = False Then 'nessun danno trovato al rientro
                            yf += 5
                            PdfStamper.GetOverContent(1).AddImage(img_x, 10, 0, 0, 10, xf, yf)
                        Else
                            'trovati danni esistenti
                            yf -= 5
                            PdfStamper.GetOverContent(1).AddImage(img_x, 10, 0, 0, 10, xf, yf)
                        End If

                    Catch ex As Exception
                        HttpContext.Current.Response.Write("error GeneraDocumento ---STAMPA CHECK IN 2  : <br/>" & ex.Message & "<br/>" & "<br/>")
                    End Try

                End If

                'End If

            End With
            'FORMULA PER CONVERTIRE I PIXEL IN PUNTI points = pixels * 72 / 96

            PdfStamper.FormFlattening = True
            PdfStamper.Close()

        Catch ex As Exception
            HttpContext.Current.Trace.Write("Errore in GeneraDocumento:" & ex.Message)
        End Try
        Return ms

    End Function





    Public Shared Function danno_di_questo_gruppo(ByVal id_pos_danno As String, ByVal gruppo_danni_rientro As String, ByVal gruppo_danni_uscita As String) As Boolean
        Dim sqlstr As String = "SELECT pd.descrizione des_id_posizione_danno, td.descrizione des_id_tipo_danno, gd.entita_danno, gd.descrizione descrizione_danno, gd.tipo_record, ISNULL(gd2.id,0) GiaPresente" & _
                     " FROM veicoli_gruppo_danni gd WITH(NOLOCK) " & _
             " LEFT JOIN veicoli_gruppo_danni gd2 WITH(NOLOCK) ON gd.id_danno = gd2.id_danno AND gd2.tipo_record IN (1,2,3) AND gd2.id_evento_apertura='" & gruppo_danni_uscita & "'" & _
             " LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = gd.id_tipo_danno" & _
             " LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = gd.id_posizione_danno" & _
             " WHERE gd.tipo_record IN (1,2,3) AND gd.id_evento_apertura= '" & gruppo_danni_rientro & "' AND pd.id ='" & id_pos_danno & "'" & _
             " ORDER BY gd2.id DESC, gd.tipo_record"
        Dim Dbc3 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc3.Open()
        Dim Cmd3 As New Data.SqlClient.SqlCommand(sqlstr, Dbc3)
        Dim Rs3 As Data.SqlClient.SqlDataReader
        Rs3 = Cmd3.ExecuteReader()

        If Rs3.Read() Then
            danno_di_questo_gruppo = True
        Else
            danno_di_questo_gruppo = False
        End If


        Rs3.Close()
        Rs3 = Nothing
        Cmd3.Dispose()
        Cmd3 = Nothing
        Dbc3.Close()
        Dbc3.Dispose()
        Dbc3 = Nothing
    End Function


    Public Shared Function Get_gia_presente(ByVal id_pos_danno As String, ByVal gruppo_danni_rientro As String, ByVal gruppo_danni_uscita As String) As String
        Dim sqlstr As String = "SELECT pd.descrizione des_id_posizione_danno, td.descrizione des_id_tipo_danno, gd.entita_danno, gd.descrizione descrizione_danno, gd.tipo_record, ISNULL(gd2.id,0) GiaPresente" & _
                     " FROM veicoli_gruppo_danni gd WITH(NOLOCK) " & _
             " LEFT JOIN veicoli_gruppo_danni gd2 WITH(NOLOCK) ON gd.id_danno = gd2.id_danno AND gd2.tipo_record IN (1,2,3) AND gd2.id_evento_apertura='" & gruppo_danni_uscita & "'" & _
             " LEFT JOIN veicoli_tipo_danno td WITH(NOLOCK) ON td.id = gd.id_tipo_danno" & _
             " LEFT JOIN veicoli_posizione_danno pd WITH(NOLOCK) ON pd.id = gd.id_posizione_danno" & _
             " WHERE gd.tipo_record IN (1,2,3) AND gd.id_evento_apertura= '" & gruppo_danni_rientro & "' AND pd.id ='" & id_pos_danno & "'" & _
             " ORDER BY gd2.id DESC, gd.tipo_record"
        Dim Dbc3 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc3.Open()
        Dim Cmd3 As New Data.SqlClient.SqlCommand(sqlstr, Dbc3)
        Dim Rs3 As Data.SqlClient.SqlDataReader
        Rs3 = Cmd3.ExecuteReader()

        If Rs3.Read() Then
            Get_gia_presente = Rs3("GiaPresente")
        Else
            Get_gia_presente = -1
        End If


        Rs3.Close()
        Rs3 = Nothing
        Cmd3.Dispose()
        Cmd3 = Nothing
        Dbc3.Close()
        Dbc3.Dispose()
        Dbc3 = Nothing
    End Function
End Class
