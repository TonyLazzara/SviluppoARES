Imports System.Web.Script.Serialization
Imports System.Web.Services

' Per consentire la chiamata di questo servizio Web dallo script utilizzando ASP.NET AJAX, rimuovere il commento dalla riga seguente.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WebServiceARES_SCR
    Inherits System.Web.Services.WebService


    Public percorso_x_firme_pick_up As String = "C:\inetpub\ares.sicilyrentcar.it\htdocs\firme_contratti\pick_up\"

    <WebMethod()> _
    Public Function getContrattoDaFirmare(ByVal id_stazione_uscita As String, ByVal id_tablet As String) As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim sqlStr As String = "SELECT TOP 1 num_contratto FROM contratti_richiesta_firma_pickup WITH(NOLOCK)  WHERE richiesta_letta='0' AND id_stazione_uscita=" & id_stazione_uscita & " AND id_tablet=" & id_tablet

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim test As String = Cmd.ExecuteScalar & ""

            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            If test <> "" Then
                dictionary = New Dictionary(Of String, String)(1)
                dictionary.Add("num_contratto", test)

                list.Add(dictionary)
            Else
                dictionary = New Dictionary(Of String, String)(1)
                dictionary.Add("num_contratto", "0")

                list.Add(dictionary)
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
        Catch ex As Exception
            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            dictionary.Add("num_contratto", "-1")

            list.Add(dictionary)
            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
        End Try
    End Function

    <WebMethod()> _
    Public Function getListaContratti(ByVal id_stazione_uscita As String) As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim sqlStr As String = "SELECT id, num_contratto, targa,  conducenti1.cognome As cognome_primo_conducente, conducenti1.nome As nome_primo_conducente FROM contratti WITH(NOLOCK)  LEFT JOIN conducenti As conducenti1 WITH(NOLOCK) ON contratti.id_primo_conducente=conducenti1.id_conducente WHERE status='2' AND attivo='1' AND ISNULL(firma_tablet,'0')='0' AND id_stazione_uscita=" & id_stazione_uscita

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()


            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            Do While Rs.Read()
                dictionary = New Dictionary(Of String, String)(1)
                dictionary.Add("id_contratto", Rs("id"))
                dictionary.Add("num_contratto", Rs("num_contratto"))
                dictionary.Add("conducente", Rs("cognome_primo_conducente") + " " + Rs("nome_primo_conducente"))
                dictionary.Add("targa", Rs("targa"))

                list.Add(dictionary)
            Loop

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
        Catch ex As Exception
            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            dictionary.Add("id_contratto", "-1")
            dictionary.Add("num_contratto", "")
            dictionary.Add("conducente", "")
            dictionary.Add("targa", "")

            list.Add(dictionary)
            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
        End Try
    End Function

    <WebMethod()> _
    Public Function getIdContratto(ByVal id_stazione_uscita As String, ByVal num_contratto As String) As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim sqlStr As String = "SELECT num_contratto, ISNULL(firma_tablet,'0') As firma_tablet FROM contratti WITH(NOLOCK) WHERE id_stazione_uscita=" & id_stazione_uscita & _
                " AND num_contratto='" & num_contratto.Replace("'", "''") & "' AND attivo='1' AND status<>'1' AND status<>'0' AND status<>'7'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            If Rs.Read() Then
                If Not Rs("firma_tablet") Then


                    dictionary.Add("num_contratto", Rs("num_contratto"))

                    list.Add(dictionary)
                Else

                    dictionary.Add("num_contratto", "-1")

                    list.Add(dictionary)
                End If
            Else


                dictionary.Add("num_contratto", "0")

                list.Add(dictionary)
            End If


            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
        Catch ex As Exception
            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            dictionary.Add("id_contratto", "-2")

            list.Add(dictionary)
            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
        End Try
    End Function

    <WebMethod()> _
    Public Function getDatiContratto(ByVal num_contratto As String) As String
        Try
            Dim id_staz_rientro As String
            Dim id_staz_uscita As String
            Dim orario_rientro As String
            Dim orario_uscita As String

            Dim id_cliente As String
            Dim id_primo_conducente As String
            Dim id_secondo_conducente As String

            Dim idContratto_uscita As String
            Dim numCalcolo_uscita As String

            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 * FROM contratti WITH(NOLOCK) WHERE num_contratto='" & num_contratto & "' AND attivo='1'  ORDER BY num_calcolo DESC", Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Dim status_contratto As String      'salvo 09.06.2022


            If Rs.Read() Then


                status_contratto = Rs!status        '09.06.2022


                dictionary = New Dictionary(Of String, String)(1)

                id_staz_rientro = Rs("id_stazione_presunto_rientro")
                id_staz_uscita = Rs("id_stazione_uscita")

                dictionary.Add("id_contratto", Rs("id"))

                dictionary.Add("data_uscita", Day(Rs("data_uscita")) & "/" & Month(Rs("data_uscita")) & "/" & Year(Rs("data_uscita")))

                If status_contratto = "2" Then 'se contratto aperto presunto rientro 09.06.2022 salvo


                    dictionary.Add("data_presunto_rientro", Day(Rs("data_presunto_rientro")) & "/" & Month(Rs("data_presunto_rientro")) & "/" & Year(Rs("data_presunto_rientro")))

                    If Len(Hour(Rs("data_presunto_rientro"))) = 1 Then
                        orario_rientro = "0" & Hour(Rs("data_presunto_rientro")) & ":"
                    Else
                        orario_rientro = Hour(Rs("data_presunto_rientro")) & ":"
                    End If

                    If Len(Minute(Rs("data_presunto_rientro"))) = 1 Then
                        orario_rientro = orario_rientro & "0" & Minute(Rs("data_presunto_rientro"))
                    Else
                        orario_rientro = orario_rientro & Minute(Rs("data_presunto_rientro"))
                    End If




                Else
                    'altrimenti data rientro effettiva 09.06.2022 salvo


                    If Not IsDBNull(Rs("data_rientro")) Then 'se piena

                        dictionary.Add("data_presunto_rientro", Day(Rs("data_rientro")) & "/" & Month(Rs("data_rientro")) & "/" & Year(Rs("data_rientro")))

                        If Len(Hour(Rs("data_rientro"))) = 1 Then
                            orario_rientro = "0" & Hour(Rs("data_rientro")) & ":"
                        Else
                            orario_rientro = Hour(Rs("data_rientro")) & ":"
                        End If

                        If Len(Minute(Rs("data_rientro"))) = 1 Then
                            orario_rientro = orario_rientro & "0" & Minute(Rs("data_rientro"))
                        Else
                            orario_rientro = orario_rientro & Minute(Rs("data_rientro"))
                        End If

                    Else

                        'data rientro vuota inserisce data presunto rientro

                        dictionary.Add("data_presunto_rientro", Day(Rs("data_presunto_rientro")) & "/" & Month(Rs("data_presunto_rientro")) & "/" & Year(Rs("data_presunto_rientro")))

                        If Len(Hour(Rs("data_presunto_rientro"))) = 1 Then
                            orario_rientro = "0" & Hour(Rs("data_presunto_rientro")) & ":"
                        Else
                            orario_rientro = Hour(Rs("data_presunto_rientro")) & ":"
                        End If

                        If Len(Minute(Rs("data_presunto_rientro"))) = 1 Then
                            orario_rientro = orario_rientro & "0" & Minute(Rs("data_presunto_rientro"))
                        Else
                            orario_rientro = orario_rientro & Minute(Rs("data_presunto_rientro"))
                        End If



                    End If





                End If




                If Len(Hour(Rs("data_uscita"))) = 1 Then
                    orario_uscita = "0" & Hour(Rs("data_uscita")) & ":"
                Else
                    orario_uscita = Hour(Rs("data_uscita")) & ":"
                End If

                If Len(Minute(Rs("data_uscita"))) = 1 Then
                    orario_uscita = orario_uscita & "0" & Minute(Rs("data_uscita"))
                Else
                    orario_uscita = orario_uscita & Minute(Rs("data_uscita"))
                End If

                dictionary.Add("orario_rientro", orario_rientro)
                dictionary.Add("orario_uscita", orario_uscita)
                dictionary.Add("giorni", Rs("giorni"))


                'inserisce km e litri rientro
                If status_contratto = "2" Then 'se contratto aperto presunto rientro 20.06.2022 salvo

                    'VEICOLO ----------------------------------------------------------------------------------------------------------------------
                    dictionary.Add("veicolo", Rs("targa") & " - " & Rs("modello") & " - Km: " & Rs("km_uscita") & " - Litri: " & Rs("litri_uscita") & "/" & Rs("serbatoio_max"))
                    '------------------------------------------------------------------------------------------------------------------------------

                Else

                    'se km e litri rientro non sono nulli
                    If Not IsDBNull(Rs("km_rientro")) And Not IsDBNull(Rs("litri_rientro")) Then

                        'VEICOLO ----------------------------------------------------------------------------------------------------------------------
                        dictionary.Add("veicolo", Rs("targa") & " - " & Rs("modello") & " - Km: " & Rs("km_rientro") & " - Litri: " & Rs("litri_rientro") & "/" & Rs("serbatoio_max"))
                        '------------------------------------------------------------------------------------------------------------------------------
                    Else    'se nulli mette quelli di uscita

                        'VEICOLO ----------------------------------------------------------------------------------------------------------------------
                        dictionary.Add("veicolo", Rs("targa") & " - " & Rs("modello") & " - Km: " & Rs("km_uscita") & " - Litri: " & Rs("litri_uscita") & "/" & Rs("serbatoio_max"))
                        '------------------------------------------------------------------------------------------------------------------------------

                    End If

                End If 'km e litri rientro 20.06.2022



                idContratto_uscita = Rs("id")
                numCalcolo_uscita = Rs("num_calcolo")
                id_cliente = Rs("id_cliente")
                id_primo_conducente = Rs("id_primo_conducente")
                id_secondo_conducente = Rs("id_secondo_conducente") & ""

                Dim importo_a_carico_del_broker As String = Rs("importo_a_carico_del_broker") & ""
                If importo_a_carico_del_broker = "" Then
                    importo_a_carico_del_broker = "0"
                End If

                Rs.Close()
                Rs = Nothing
                Dbc.Close()
                Dbc.Open()

                'STAZIONE DI USCITA ----------------------------------------------------------------------------------------------------------
                Cmd = New Data.SqlClient.SqlCommand("SELECT NOME_STAZIONE, codice, telefono, indirizzo, citta FROM stazioni WITH(NOLOCK) WHERE id='" & id_staz_uscita & "'", Dbc)
                Rs = Cmd.ExecuteReader()
                Rs.Read()

                dictionary.Add("stazione_uscita", Rs("codice") & " - " & Rs("nome_stazione") & " - " & Rs("indirizzo") & " - " & Rs("citta"))
                '------------------------------------------------------------------------------------------------------------------------------
                'STAZIONE DI RIENTRO ----------------------------------------------------------------------------------------------------------
                Dbc.Close()
                Dbc.Open()
                Rs.Close()

                Rs = Nothing
                Cmd = New Data.SqlClient.SqlCommand("SELECT NOME_STAZIONE, codice, telefono, indirizzo, citta FROM stazioni WITH(NOLOCK) WHERE id='" & id_staz_rientro & "'", Dbc)
                Rs = Cmd.ExecuteReader()
                Rs.Read()

                dictionary.Add("stazione_rientro", Rs("codice") & " - " & Rs("nome_stazione") & " - " & Rs("indirizzo") & " - " & Rs("citta"))
                '------------------------------------------------------------------------------------------------------------------------------
                'DATI FATTURAZIONE ------------------------------------------------------------------------------------------------------------
                Dbc.Close()
                Dbc.Open()
                Rs.Close()
                Rs = Nothing
                Cmd = New Data.SqlClient.SqlCommand("SELECT rag_soc, tel, indirizzo, ditte.cap, comuni_ares.comune As comune_ares, citta, email, Piva, c_fis, nazioni.nazione FROM ditte WITH(NOLOCK) LEFT JOIN comuni_ares WITH(NOLOCK) ON ditte.id_comune_ares=comuni_ares.id LEFT JOIN nazioni WITH(NOLOCK) ON ditte.nazione=nazioni.ID_NAZIONE WHERE id_ditta='" & id_cliente & "'", Dbc)
                Rs = Cmd.ExecuteReader()
                Rs.Read()

                If (Rs("comune_ares") & "") <> "" Then
                    dictionary.Add("fatturazione", Rs("rag_soc") & " - " & Rs("indirizzo") & vbCrLf & Rs("cap") & " " & Rs("comune_ares") & " - " & Rs("Piva") & " (P.iva) - " & Rs("nazione"))
                Else
                    dictionary.Add("fatturazione", Rs("rag_soc") & " - " & Rs("indirizzo") & vbCrLf & Rs("cap") & " " & Rs("citta") & " - " & Rs("Piva") & " (P.iva) - " & Rs("nazione"))
                End If
                '------------------------------------------------------------------------------------------------------------------------------
                'PRIMO CONDUCENTE -------------------------------------------------------------------------------------------------------------
                Dbc.Close()
                Dbc.Open()
                Cmd = New Data.SqlClient.SqlCommand("SELECT cognome, nome, telefono, CONVERT(char(10), data_nascita, 103) As data_nascita, luogo_nascita, indirizzo, cap, comuni_ares.comune As comune_ares, city, email, patente, CONVERT(char(10), scadenza_patente, 103) As scadenza_patente, CODFIS, nazioni.nazione FROM conducenti WITH(NOLOCK) LEFT JOIN comuni_ares WITH(NOLOCK) ON conducenti.id_comune_ares=comuni_ares.id LEFT JOIN nazioni WITH(NOLOCK) ON conducenti.nazione=nazioni.ID_NAZIONE WHERE id_conducente='" & id_primo_conducente & "'", Dbc)
                Rs = Cmd.ExecuteReader()
                Rs.Read()

                dictionary.Add("primo_conducente", Rs("nome") & " " & Rs("cognome") & " - " & Rs("data_nascita") & " " & Rs("luogo_nascita") & vbCrLf & Rs("indirizzo") & vbCrLf & Rs("cap") & " " & Rs("nazione"))


                'nome firma aggiunto il 29.07.2022 salvo
                Dim nomefirma As String = Rs("nome") & " " & Rs("cognome") & ""     'primo guidatore

                'dictionary.Add("nome_firma", nomefirma) 'rem perchè aggiunto sotto il 29.07.2022 salvo

                '------------------------------------------------------------------------------------------------------------------------------
                Dbc.Close()

                Dim secondoConducente As String = "" 'aggiunto 29.07.2022 salvo
                If id_secondo_conducente <> "" Then

                    Dbc.Open()

                    Cmd = New Data.SqlClient.SqlCommand("SELECT cognome, nome, telefono, CONVERT(char(10), data_nascita, 103) As data_nascita, luogo_nascita, indirizzo, cap, comuni_ares.comune As comune_ares, city, email, patente, CONVERT(char(10), scadenza_patente, 103) As scadenza_patente, CODFIS, nazioni.nazione FROM conducenti WITH(NOLOCK) LEFT JOIN comuni_ares WITH(NOLOCK) ON conducenti.id_comune_ares=comuni_ares.id LEFT JOIN nazioni WITH(NOLOCK) ON conducenti.nazione=nazioni.ID_NAZIONE WHERE id_conducente='" & id_secondo_conducente & "'", Dbc)
                    Rs = Cmd.ExecuteReader()
                    Rs.Read()

                    nomefirma += " - " & Rs("nome") & " " & Rs("cognome")       'aggiunge nome/cognome secondo guidatore 29.07.2022 salvo
                    dictionary.Add("nome_firma", nomefirma)                     'modificato il 29.07.2022 salvo

                    dictionary.Add("secondo_conducente", Rs("nome") & " " & Rs("cognome") & " - " & Rs("data_nascita") & " " & Rs("luogo_nascita") & vbCrLf & Rs("indirizzo") & vbCrLf & Rs("cap") & " " & Rs("nazione"))

                    Dbc.Close()

                Else    'se manca secondo conducente inserisce solo prima guida 'modificato il 29.07.2022 salvo

                    dictionary.Add("nome_firma", nomefirma) 'aggiunto il 29.07.2022 salvo

                End If



                'COSTO TOTALE --------------------------------------------------------------------------------------------------------------------------------------------------
                Dbc.Open()

                'OLD string 
                '"SELECT ISNULL((contratti_costi.imponibile + contratti_costi.iva_imponibile + ISNULL(contratti_costi.imponibile_onere, 0)  + ISNULL(contratti_costi.iva_onere, 0)),0) FROM contratti_costi WITH(NOLOCK) WHERE id_documento=" & idContratto_uscita & " AND nome_costo='TOTALE'"
                'modificata il 08.02.2022
                Dim sqla As String = "Select (ISNULL((contratti_costi.imponibile + contratti_costi.iva_imponibile + ISNULL(contratti_costi.imponibile_onere, 0) + ISNULL(contratti_costi.iva_onere, 0)),0)) - "
                sqla += "(ISNULL((contratti_costi.imponibile_scontato_prepagato  + contratti_costi.iva_imponibile_scontato_prepagato + ISNULL(contratti_costi.imponibile_onere_prepagato, 0) + ISNULL(contratti_costi.iva_onere_prepagato , 0)),0)) as Totale "
                sqla += "From contratti_costi WITH(NOLOCK) Where id_documento ='" & idContratto_uscita & "' AND nome_costo='TOTALE'"

                Cmd = New Data.SqlClient.SqlCommand(sqla, Dbc)
                Dim totale As String = Cmd.ExecuteScalar & ""
                totale = CDbl(totale) - CDbl(importo_a_carico_del_broker)
                If totale = "" Then
                    totale = "0"
                End If

                Dbc.Close()

                dictionary.Add("totale", FormatNumber(totale, 2))
                ' end riga TOTALE -----------------------------------------------------------------------------------------------------------------------------------------------


                'START RIGA Franchigie  -----------------------------------------------------------------------------------------------------------------------------------------------

                '# modificato il 31.01.2022 x escludere il deposito cauzionale 31.01.2022
                'così da nn comparire sul tablet al momento della firma

                Dbc.Open()

                Dim sql2 As String = "SELECT nome_costo, valore_costo FROM contratti_costi WITH(NOLOCK) "
                sql2 += "WHERE franchigia_attiva = 1 And id_documento = " & idContratto_uscita & " And num_calcolo = " & numCalcolo_uscita & " "
                sql2 += "and id_elemento<>283 " '

                Cmd = New Data.SqlClient.SqlCommand(sql2, Dbc)
                Rs = Cmd.ExecuteReader()

                Dim z As String = 0

                Do While Rs.Read()
                    z = z + 1

                    dictionary.Add("nome_franchigia_" & z, Rs("nome_costo"))
                    dictionary.Add("franchigia_" & z, FormatNumber(CDbl(Rs("valore_costo")), 2))

                Loop

                Dbc.Close()

                If z = 0 Then
                    dictionary.Add("nome_franchigia_1", "")
                    dictionary.Add("franchigia_1", "")
                    dictionary.Add("nome_franchigia_2", "")
                    dictionary.Add("franchigia_2", "")
                ElseIf z = 1 Then
                    dictionary.Add("nome_franchigia_2", "")
                    dictionary.Add("franchigia_2", "")
                End If

                'END RIGA Franchigie  -----------------------------------------------------------------------------------------------------------------------------------------------


                'PAGAMENTI --------------------------------------------------------------------------------------------------------------------------------------------------
                Dbc.Open()
                z = 0
                Dim pagamento1 As String = ""
                Dim pagamento2 As String = ""
                Dim pagamento3 As String = ""
                Dim pagamento4 As String = ""
                Dim pagamento5 As String = ""
                Dim pagamento6 As String = ""
                Dim pagamento7 As String = ""
                Dim pagamento8 As String = ""
                Dim pagamento9 As String = ""

                Dim pagamento As String = ""

                Dim contanti As String
                Dim carta As String = "CARTA"
                Dim nome_pagamento As String

                Cmd = New Data.SqlClient.SqlCommand("Select POS_Funzioni.funzione, PAGAMENTI_EXTRA.intestatario, MOD_PAG.Descrizione Des_ID_ModPag, POS_Funzioni.id As id_funzione, PAGAMENTI_EXTRA.scadenza, PAGAMENTI_EXTRA.titolo, PAGAMENTI_EXTRA.id_pos_funzioni_ares,PAGAMENTI_EXTRA.data, PAGAMENTI_EXTRA.DATA_OPERAZIONE, PAGAMENTI_EXTRA.PER_IMPORTO, PAGAMENTI_EXTRA.ID_CTR, PAGAMENTI_EXTRA.preaut_aperta, PAGAMENTI_EXTRA.NR_PREAUT, PAGAMENTI_EXTRA.operazione_stornata, (Case When N_CONTRATTO_RIF Is NULL Then 'PREN ' ELSE 'RA ' END) AS provenienza FROM PAGAMENTI_EXTRA WITH(NOLOCK) INNER JOIN POS_Funzioni WITH(NOLOCK) ON PAGAMENTI_EXTRA.id_pos_funzioni_ares=POS_funzioni.id LEFT JOIN MOD_PAG WITH(NOLOCK) ON pagamenti_extra.ID_ModPag = MOD_PAG.ID_ModPag WHERE N_CONTRATTO_RIF='" & num_contratto & "' ORDER BY DATA_OPERAZIONE DESC", Dbc)
                Rs = Cmd.ExecuteReader()

                Do While Rs.Read()
                    If Rs.HasRows() Then

                        If Rs("provenienza") = "RA " Then
                            pagamento = pagamento & Rs("data") & " "


                            If (Rs("Des_ID_ModPag") & "") <> "" Then
                                contanti = Rs("Des_ID_ModPag") & ""
                            End If
                            nome_pagamento = Rs("funzione") & ""

                            pagamento = pagamento & nome_pagamento & " "

                            pagamento = pagamento & FormatCurrency(Rs("PER_IMPORTO"), 2) & "  "
                            If Rs("id_funzione") = enum_tipo_pagamento_ares.Richiesta And (Rs("NR_PREAUT") & "") <> "" Then
                                pagamento = pagamento & " - Nr.Pr. " & Rs("NR_PREAUT") & " "
                            End If

                            If (Rs("titolo") & "") <> "" Then
                                Dim titolo As String = ""
                                With New security
                                    titolo = .decryptString(Rs("titolo"))
                                End With

                                If Len(titolo) = 16 Then


                                    titolo = "XXXX " & "XXXX " & "XXXX " & titolo.Substring(12, 4)
                                ElseIf Len(titolo) > 4 Then
                                    titolo = "XXXX " & "XXXX " & "XXXX " & titolo.Substring(Len(titolo) - 4, 4)
                                Else
                                    titolo = "XXXX"
                                End If

                                pagamento = pagamento & " - " & carta & ": " & titolo & " "
                            End If

                            If (Rs("intestatario") & "") <> "" Then
                                pagamento = pagamento & " - " & Rs("intestatario")
                            End If

                            If (Rs("Des_ID_ModPag") & "") <> "" Then

                                Dim dicitura As String = Rs("Des_ID_ModPag") & ""


                                pagamento = pagamento & "  " & dicitura
                            End If

                            If (Rs("scadenza") & "") <> "" And Rs("Des_ID_ModPag") & "" <> "BANCOMAT" And Rs("Des_ID_ModPag") & "" <> "CONTANTI" And (Rs("titolo") & "") <> "" Then
                                pagamento = pagamento & " - SCAD: " & Rs("scadenza")
                            End If

                            pagamento = pagamento & vbCrLf

                        End If

                    End If
                Loop

                If pagamento <> "" Then
                    pagamento = "PAGAMENTI" & vbCrLf & pagamento
                End If

                Dbc.Close()

                dictionary.Add("pagamento", pagamento)

                list.Add(dictionary)
                Dim j As JavaScriptSerializer = New JavaScriptSerializer()
                Return (j.Serialize(list.ToArray()))

            Else


                'RESTITUIRE ERRORE NON TROVATO
                dictionary.Add("id_contratto", "-1")
                dictionary.Add("data_presunto_rientro", "")
                dictionary.Add("data_uscita", "")
                dictionary.Add("orario_rientro", "")
                dictionary.Add("orario_uscita", "")
                dictionary.Add("giorni", "")
                dictionary.Add("veicolo", "")
                dictionary.Add("stazione_uscita", "")
                dictionary.Add("stazione_rientro", "")
                dictionary.Add("fatturazione", "")
                dictionary.Add("primo_conducente", "")
                dictionary.Add("nome_firma", "")
                dictionary.Add("secondo_conducente", "")
                dictionary.Add("totale", "")
                dictionary.Add("nome_franchigia_1", "")
                dictionary.Add("franchigia_1", "")
                dictionary.Add("nome_franchigia_2", "")
                dictionary.Add("franchigia_2", "")
                dictionary.Add("pagamento", "")

                list.Add(dictionary)
                Dim j As JavaScriptSerializer = New JavaScriptSerializer()
                Return (j.Serialize(list.ToArray()))
            End If




            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        Catch ex As Exception
            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            dictionary.Add("id_contratto", "-1")
            dictionary.Add("data_presunto_rientro", "")
            dictionary.Add("data_uscita", "")
            dictionary.Add("orario_rientro", "")
            dictionary.Add("orario_uscita", "")
            dictionary.Add("giorni", "")
            dictionary.Add("veicolo", "")
            dictionary.Add("stazione_uscita", "")
            dictionary.Add("stazione_rientro", "")
            dictionary.Add("fatturazione", "")
            dictionary.Add("primo_conducente", "")
            dictionary.Add("nome_firma", "")
            dictionary.Add("secondo conducente", "")
            dictionary.Add("nome_franchigia_1", "")
            dictionary.Add("franchigia_1", "")
            dictionary.Add("nome_franchigia_2", "")
            dictionary.Add("franchigia_2", "")
            dictionary.Add("pagamento", "")

            list.Add(dictionary)
            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
        End Try

    End Function

    <WebMethod()>
    Public Function CreaPDFTest(ByVal num_contratto As String) As String
        Dim allega As String = "OK" 'funzioni_comuni.AllegaContrattoDopoFirmaTest(num_contratto)
        Return allega

    End Function


    Function Base64ToImagePickUp(ByVal base64string As String, ByVal num_contratto As String, ByVal nome_file As String) As String

        Dim ris As String = ""

        'Setup image and get data stream together

        Dim img As System.Drawing.Image

        Dim MS As System.IO.MemoryStream = New System.IO.MemoryStream

        Dim b64 As String = base64string.Replace(" ", "+")

        Dim b() As Byte

        'Converts the base64 encoded msg to image data

        b = Convert.FromBase64String(b64)

        MS = New System.IO.MemoryStream(b)

        'creates image
        img = System.Drawing.Image.FromStream(MS)

        'If (Not Directory.Exists(percorso_x_firme_apertura & id_scheda_apertura)) Then
        '    Directory.CreateDirectory(percorso_x_firme_apertura & id_scheda_apertura)
        'End If

        'modificato il 19.12.2020 percorso a seconda del dominio
        'se si tratta della firma del rientro deve aggiungere R 10.05.2022
        percorso_x_firme_pick_up = Server.MapPath("\firme_contratti\pick_up" & "\" & num_contratto & ".png")

        'se presente la firma vuol dire che si tratta della firma di rientro 10.05.2022
        If IO.File.Exists(percorso_x_firme_pick_up) Then
            percorso_x_firme_pick_up = Server.MapPath("\firme_contratti\pick_up" & "\" & num_contratto & "_RB.png")
        End If
        img.Save(percorso_x_firme_pick_up)

        'restituisce percorso firme x verificare se si tratta della firma di uscita o di rientro 10.05.2022
        ris = percorso_x_firme_pick_up
        Return ris


    End Function

    <WebMethod()>
    Public Function salvaFirmaPickUp(ByVal encodedImage As String, ByVal num_contratto As String, ByVal nome_file As String, ByVal nome As String) As String

        Dim tipofirma As String = ""

        Try

            tipofirma = Base64ToImagePickUp(encodedImage, num_contratto, nome_file)

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String = "UPDATE contratti_richiesta_firma_pickup SET richiesta_letta='1' WHERE num_contratto='" & num_contratto & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            Dim id_tablet_firma As String = GetIdTablet(num_contratto)
            If id_tablet_firma = "" Then id_tablet_firma = "-4"

            ' 'aggiornamento senza id_tablet_firma old
            'sqlStr = "UPDATE contratti SET firma_tablet='1', nome_firma='" & nome.Replace("'", "''") & "' WHERE num_contratto='" & num_contratto & "'"
            'Else
            'aggiornamento con id_tablet_firma old

            'se si tratta della firma di rientro aggiorna 10.05.2022
            If tipofirma.IndexOf("_RB") > -1 Then 'si tratta della firma al rientro 10.05.2022
                sqlStr = "UPDATE contratti SET id_tablet_firma_rientro='" & id_tablet_firma & "', firma_tablet_rientro='1', nome_firma='" & nome.Replace("'", "''") & "' WHERE num_contratto='" & num_contratto & "'"
            Else
                sqlStr = "UPDATE contratti SET id_tablet_firma='" & id_tablet_firma & "', firma_tablet='1', nome_firma='" & nome.Replace("'", "''") & "' WHERE num_contratto='" & num_contratto & "'"
            End If


            'End If

            'nuova riga per aggiornare ID del tablet uguale a quello dello status 1
            'sqlStr = "UPDATE contratti SET firma_tablet='1', nome_firma='" & nome.Replace("'", "''") & "', contratti.id_tablet_firma = contratti_1.id_tablet_firma "
            'sqlStr += "From contratti INNER Join contratti As contratti_1 On contratti.num_contratto = contratti_1.num_contratto "
            'sqlStr += "Where (contratti_1.num_contratto ='" & num_contratto & "') And (contratti_1.status = '1')"

            Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            '## genera contratto in PDF con firma (creato su procedura btnfirma su contratto
            'e inserisce firma 10.03.2022
            'annullato il11.03.2022
            'Dim allega As Boolean = funzioni_comuni.AllegaContrattoDopoFirma(num_contratto)
            'deve rigenerare lista allegati su contratti
            ''## end su Produzione



            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            dictionary.Add("operazione_ok", "1")
            list.Add(dictionary)

            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))



        Catch ex As Exception
            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            dictionary.Add("operazione_ok", "0")
            list.Add(dictionary)

            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
        End Try
    End Function

    <WebMethod()>
    Public Function annullaFirmaPickUp(ByVal num_contratto As String) As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String = "UPDATE contratti_richiesta_firma_pickup SET richiesta_letta='1' WHERE num_contratto='" & num_contratto & "'"
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            dictionary.Add("operazione_ok", "1")
            list.Add(dictionary)

            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
        Catch ex As Exception
            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            dictionary.Add("operazione_ok", "0")
            list.Add(dictionary)

            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
        End Try

    End Function


    Function GetIdTablet(ByVal num_contratto As String) As String

        Dim ris As String = ""

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim sqlStr As String = "SELECT id_tablet_firma FROM contratti WITH(NOLOCK) WHERE status='1' AND num_contratto='" & num_contratto & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows Then
                Rs.Read()
                ris = Rs!id_tablet_firma
            Else
                ris = "-2"
            End If
            Rs.Close()
            Cmd.Dispose()
            Rs = Nothing
            Dbc.Close()
            Dbc = Nothing

        Catch ex As Exception
            ris = "-9"
        End Try

        Return ris

    End Function



End Class