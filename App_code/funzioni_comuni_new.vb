Imports System.Data
Imports System.IO

'Funzioni SALVO

Public Class funzioni_comuni_new



    Public Shared Function GetCostoSconto(iddocumento As String, num_calcolo As String, sconto As String) As String
        'aggiunto salvo 08.07.2023
        Dim ris As String = "0"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String = "SELECT [valore_costo] FROM contratti_costi where id_documento='" & iddocumento & "' and nome_costo='SCONTO' and num_calcolo='" & num_calcolo & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows Then
                Rs.Read()

                If Not IsDBNull(Rs!valore_costo) Then
                    ris = Rs!valore_costo

                End If

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

    Public Shared Function GetCostoFO(idTariffaRiga As String, id_elemento As String) As String
        'aggiunto salvo 04.07.2023
        Dim ris As String = "0"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String = "SELECT [costo] FROM [Autonoleggio_SRC].[dbo].[condizioni_righe] where id_elemento='" & id_elemento & "' order by id desc" ' and id_condizione='" & idTariffaRiga & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows Then
                Rs.Read()

                If Not IsDBNull(Rs!costo) Then
                    ris = Rs!costo

                End If

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

    Public Shared Function GetIdCondizione(idTariffaRiga As String) As String
        'aggiunto salvo 04.07.2023
        Dim ris As String = "0"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String = "SELECT [id_condizione] FROM [tariffe_righe] WITH(NOLOCK) WHERE id='" & idTariffaRiga & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows Then
                Rs.Read()

                If Not IsDBNull(Rs!id_condizione) Then
                    ris = Rs!id_condizione

                End If

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
    Public Shared Function get_elemento_fuori_orario_pickUp_new(ByVal ore_pick_up As Integer, ByVal minuti_pick_up As Integer) As Array
        'aggiunto salvo 04.07.2023

        Dim ris(1) As String
        ris(0) = "0"
        ris(1) = "0"

        Try

            'RESTITUISCE L'ELEMENTO CONDIZIONE FUORI ORARIO IN BASE ALL'ORARIO DI PICK UP - QUESTI ELEMENTI SONO NELLA TABELLA condizioni_elementi
            'CON tipologia='FUORI'. PER QUESTI ELEMENTI NELLA TABELLA SI TROVANO VALORIZZATI I CAMPI ore_inizio_fuori_orario/minuti_inizio_fuori_orario
            '/ore_fine_fuori_orario/minuti_fine_fuori_orario CHE INDICANO LA FASCIA ORARIO (DENTRO LA QUALE CADE L'ORARIO DI PICK UP DEL VEICOLO
            'QUANDO LA STAZIONE E' CHIUSA) CHE IDENTIFICA L'ELEMENTO DA UTILIZZARE.
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim sqlStr As String = "SELECT * FROM condizioni_elementi WITH(NOLOCK) WHERE tipologia='FUORI' AND" &
        "((" & ore_pick_up & " > ore_inizio_fuori_orario AND " & ore_pick_up & " < ore_fine_fuori_orario) " &
        " OR (" & ore_pick_up & "=ore_inizio_fuori_orario AND " & minuti_pick_up & ">= minuti_inizio_fuori_orario) " &
        " OR (" & ore_pick_up & "=ore_fine_fuori_orario AND " & minuti_pick_up & "<= minuti_fine_fuori_orario))"

            'TONY 04-07-2023
            'HttpContext.Current.Response.Write(sqlstr & "</br>")
            'HttpContext.Current.Response.end()

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows Then
                Rs.Read()

                If Not IsDBNull(Rs!id) Then
                    ris(0) = Rs!id
                    ris(1) = Rs!descrizione
                End If

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
    Shared Function getVerificaValoriList(tbl As String, id_doc As String, nro As String) As Array

        Dim ris(5) As String
        ris(0) = "" 'listaTariffe
        ris(1) = "" 'listaTempoKM
        ris(2) = "" 'ListaPeriodi
        ris(3) = "" 'gruppo
        ris(4) = "" 'ListaNomi
        ris(5) = "" 'ListaSconti


        Dim sqls As String = "select *,"
        sqls += "list_sconti from " & tbl
        If tbl = "prenotazioni" Then
            sqls += " where nr_pren = " & id_doc & " order by num_calcolo desc"
        Else
            If tbl = "preventivi" Then
                If id_doc <> "" Then
                    sqls += " where id = " & id_doc & " order by id desc"
                End If
            Else
                sqls += " where num_preventivo = " & nro & " order by id desc"
            End If

        End If

        Try

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqls, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows Then
                Rs.Read()
                If Not IsDBNull(Rs!list_id_tariffe) Then
                    ris(0) = Rs!list_id_tariffe
                    ris(1) = Rs!list_tempokm
                    ris(2) = Rs!list_periodi_tariffe
                    If tbl <> "preventivi" Then
                        If tbl = "prenotazioni" Then
                            ris(3) = Rs!id_gruppo
                        Else
                            ris(3) = Rs!id_gruppo_auto
                        End If
                    End If



                    ris(4) = Rs!list_nomi_tariffe
                    ris(5) = Rs!list_sconti
                End If


            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            ris = ris

        End Try

        Return ris



    End Function


    Shared Function GetListTariffePDF(tbl As String, id_documento As String) As Boolean
        'salvo 16.06.2023
        Dim ris As Boolean = False

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlstr As String = "select * from " & tbl & " "
        If tbl = "prenotazioni" Then
            sqlstr += "Where nr_pren = '" & id_documento & "' "
        Else
            sqlstr += "Where id = '" & id_documento & "' "
        End If



        Try

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            ris = Rs1.HasRows

            If Rs1.HasRows Then
                Rs1.Read()

                If IsDBNull(Rs1!list_id_tariffe) Then
                    ris = False
                Else
                    ris = True
                End If


            End If

            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception
            HttpContext.Current.Response.Write("error SalvaListTariffe: " & ex.Message & sqlstr & "</br>")
        End Try

        Return ris

    End Function
    Shared Function VerificaListTariffePDF(tbl As String, id_documento As String) As Boolean
        'salvo 16.06.2023
        Dim ris As Boolean = False

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlstr As String = "select list_id_tariffe from " & tbl & " "
        If tbl = "prenotazioni" Then
            sqlstr += "Where nr_pren = '" & id_documento & "' "
        Else
            sqlstr += "Where id = '" & id_documento & "' "
        End If

        Try

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            ris = Rs1.HasRows

            If Rs1.HasRows Then
                Rs1.Read()

                If IsDBNull(Rs1!list_id_tariffe) Then
                    ris = False
                Else
                    ris = True
                End If

            End If

            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("error SalvaListTariffe: " & ex.Message & sqlstr & "</br>")
        End Try

        Return ris

    End Function




    Shared Function SalvaListTariffe(tbl As String, id_documento As String, list_tariffe As String,
                                     list_tempoKM As String, list_periodi As String, list_nomi As String, list_Sconti As String) As String
        'salvo 16.06.2023
        Dim ris As String = ""

        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim sqlstr As String = "UPDATE " & tbl & " SET list_id_tariffe='" & list_tariffe & "', list_tempokm='" & list_tempoKM & "',"
        sqlstr += "list_periodi_tariffe ='" & list_periodi & "', list_nomi_tariffe='" & list_nomi & "', list_sconti='" & list_Sconti & "' "

        If tbl = "prenotazioni" Then
            sqlstr += "Where nr_pren = '" & id_documento & "' "
        Else
            sqlstr += "Where id = '" & id_documento & "' "
        End If



        Try

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            ris = Cmd1.ExecuteNonQuery()

            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception
            HttpContext.Current.Response.Write("error SalvaListTariffe: " & ex.Message & sqlstr & "</br>")
        End Try

        Return ris

    End Function

    Shared Function GetImponibilePreventiviCosti(id_documento As String, num_calcolo As String) As Array

        Dim ris(1) As String
        ris(0) = "0"
        ris(1) = "0"

        Dim sqlstr As String = "SELECT  imponibile_scontato, iva_imponibile_scontato FROM preventivi_costi WITH(NOLOCK) " &
            "WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' "

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris(0) = Rs1!imponibile_scontato
                ris(1) = Rs1!iva_imponibile_scontato
            End If

            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("GetCostiPeriodiElementi error: " & ex.Message & "<br/>")
        End Try

        Return ris


    End Function

    Public Shared Function GetCostiPeriodiElementi(id_elemento As String, data_pickup As String, data_dropoff As String, applicabilita_da As String, applicabilita_a As String, id_condizione As String) As String

        Dim ris As String = "-1"

        '' Dim sqlstr As String = "select valore_costo  from preventivi_costi " &
        ' "where id_documento ='" & id_preventivo & "' and num_calcolo='" & num_calcolo & "' and nome_costo='TOTALE'"


        Dim data_calcolo As String = CDate(data_pickup).Year & "-" & CDate(data_pickup).Month & "-" & CDate(data_pickup).Day & " 00:00:00"

        Dim sqlstr As String = "" '"SELECT  [id]   ,[periodo_da] ,[periodo_a] ,[applicabilita_da],[applicabilita_a] ,[id_elemento] ,[costo]"
        'sqlstr += "From [Autonoleggio_SRC].[dbo].[condizioni_righe_costi_periodi]  "
        'sqlstr += "where id_elemento='" & id_elemento & "'and convert(datetime,'" & data_calcolo & "',102) >= periodo_da  "
        'sqlstr += "And applicabilita_da ='" & applicabilita_da & "' "

        If id_elemento = "85" Then
            id_elemento = "85"
        End If

        sqlstr = "Select [id],[periodo_da] ,[periodo_a] ,[applicabilita_da],[applicabilita_a] ,[id_elemento] ,[costo] From [Autonoleggio_SRC].[dbo].[condizioni_righe_costi_periodi]  " &
        "where id_elemento='" & id_elemento & "' and periodo_da  <= convert(datetime,'" & data_calcolo & "',102) AND id_condizione='" & id_condizione & "' order by periodo_da desc, id desc"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris = Rs1!costo

            End If

            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception
            HttpContext.Current.Response.Write("GetCostiPeriodiElementi error: " & ex.Message & "<br/>")

        End Try

        Return ris


    End Function


    Public Shared Function UpdatePreventiviCostiNumCalcolo(id_preventivo As String, num_calcolo_old As String, num_calcolo_new As String) As Integer

        Dim ris As Integer = 0

        '' Dim sqlstr As String = "select valore_costo  from preventivi_costi " &
        ' "where id_documento ='" & id_preventivo & "' and num_calcolo='" & num_calcolo & "' and nome_costo='TOTALE'"

        Dim sqlstr As String = "UPDATE preventivi SET num_calcolo='" & num_calcolo_new & "' "
        sqlstr += "Where (preventivi.id = '" & id_preventivo & "') "
        sqlstr += "And preventivi.num_calcolo = '" & num_calcolo_old & "' "

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            ris = Cmd1.ExecuteNonQuery()

            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception
            HttpContext.Current.Response.Write("error FCN_UpdatePreventiviCostiNumCalcolo: " & ex.Message & sqlstr & "</br>")
        End Try

        Return ris


    End Function

    Public Shared Function UpdateTest() As Integer

        Dim ris As Integer = 0

        '' Dim sqlstr As String = "select valore_costo  from preventivi_costi " &
        ' "where id_documento ='" & id_preventivo & "' and num_calcolo='" & num_calcolo & "' and nome_costo='TOTALE'"

        Dim sqlstr As String = "update preventivi_costi set valore_costo=390  where id_documento=260264 and (nome_costo='TOTALE' or nome_costo='Valore Tariffa')"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            ris = Cmd1.ExecuteNonQuery()

            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception
            HttpContext.Current.Response.Write("error FCN_UpdatePreventiviCostiNumCalcolo: " & ex.Message & sqlstr & "</br>")
        End Try

        Return ris


    End Function

    Public Shared Function DeletePreventiviCostiNumCalcolo(id_preventivo As String, num_calcolo As String) As Integer

        Dim ris As Integer = 0

        '' Dim sqlstr As String = "select valore_costo  from preventivi_costi " &
        ' "where id_documento ='" & id_preventivo & "' and num_calcolo='" & num_calcolo & "' and nome_costo='TOTALE'"

        Dim sqlstr As String = "delete From preventivi_costi "
        sqlstr += "Where (preventivi_costi.id_documento = '" & id_preventivo & "') "
        sqlstr += "And preventivi_costi.num_calcolo = '" & num_calcolo & "' "

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            ris = Cmd1.ExecuteNonQuery()

            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception
            HttpContext.Current.Response.Write("error FCN_DeletePreventiviCostiNumCalcolo: " & ex.Message & sqlstr & "</br>")
        End Try

        Return ris


    End Function



    Public Shared Function GetTotaleUltimoPreventivoValido(id_preventivo As String, num_calcolo As String) As String

        Dim ris As String = "0"

        '' Dim sqlstr As String = "select valore_costo  from preventivi_costi " &
        ' "where id_documento ='" & id_preventivo & "' and num_calcolo='" & num_calcolo & "' and nome_costo='TOTALE'"

        Dim sqlstr As String = "Select preventivi_costi.valore_costo, preventivi_costi.num_calcolo, preventivi.num_preventivo "
        sqlstr += "From preventivi_costi INNER Join preventivi On preventivi_costi.id_documento = preventivi.id "
        sqlstr += "Where (preventivi.num_preventivo = '" & id_preventivo & "') And (preventivi_costi.nome_costo = 'TOTALE') "
        sqlstr += "And preventivi_costi.num_calcolo = '" & num_calcolo & "' "
        sqlstr += "Order By preventivi_costi.num_calcolo DESC"



        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()

                ris = Rs1!valore_costo

            End If

            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception

        End Try

        Return ris


    End Function


    Public Shared Function GetIdTempoKmPren(num_prenotazione As String) As Array


        Dim ris(3) As String

        Dim sqlstr As String = "SELECT prenotazioni.id_tariffa as id_tar, prenotazioni.id_tariffe_righe as id_tar_righe, tariffe.codice as codice, tariffe_righe.max_sconto as max_sconto " &
            "FROM prenotazioni INNER JOIN tariffe_righe ON prenotazioni.id_tariffe_righe = tariffe_righe.id INNER JOIN " &
             "tariffe ON tariffe_righe.id_tariffa = tariffe.id " &
            "WHERE (prenotazioni.NUMPREN = '" & num_prenotazione & "')"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows = True Then
                Rs1.Read()
                ris(0) = Rs1!id_tar
                ris(1) = Rs1!id_tar_righe
                ris(2) = Rs1!codice
                ris(3) = Rs1!max_sconto
            Else
                ris(0) = "0"
                ris(1) = "0"
                ris(2) = "0"
                ris(3) = "0"

            End If

            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception
            ris(0) = "0"
            ris(1) = "0"
            ris(2) = "0"
            ris(3) = "0"
        End Try

        Return ris


    End Function



    Public Shared Function GetTariffaUnica(id_tariffa As String, data_pickup As String, data_dropoff As String) As Boolean


        Dim ris As Boolean = False

        Dim dtpkup As String = funzioni_comuni_new.GetDataSqlNew(data_pickup, 0)
        Dim dtdropoff As String = funzioni_comuni_new.GetDataSqlNew(data_dropoff, 59)

        Dim sqlstr As String = "select vendibilita_da,vendibilita_a,pickup_da,pickup_a  from tariffe_righe where id='" & id_tariffa & "' " &
            "And Convert(DateTime,'" & dtpkup & "',102) between Convert(DateTime,'" & dtpkup & "',102) and Convert(DateTime,'" & dtdropoff & "',102)" &
            "and Convert(DateTime,'" & dtdropoff & "',102) between Convert(DateTime,'" & dtpkup & "',102) and Convert(DateTime,'" & dtdropoff & "',102) "

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            ris = Rs1.Read

            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception

        End Try

        Return ris


    End Function


    Public Shared Function GetDataSqlNew(ByVal data As String, Optional tipotime As Integer = 0) As String
        'aggiornato 28.02.2023
        Dim ris As String = data

        If tipotime = 59 Then
            ris = CDate(data).Year & "-" & CDate(data).Month & "-" & CDate(data).Day & " 23:59:59"
        ElseIf tipotime = 0 Then
            ris = CDate(data).Year & "-" & CDate(data).Month & "-" & CDate(data).Day & " 00:00:00"
        ElseIf tipotime = 99 Then   'senza ora
            ris = CDate(data).Year & "-" & CDate(data).Month & "-" & CDate(data).Day
        Else
            ris = CDate(data).Year & "-" & CDate(data).Month & "-" & CDate(data).Day & " 00:00:00"
        End If

        Return ris


    End Function


    Public Shared Function GetUltimoValoreTariffaValidoPren(id_prenotazione As String, Optional num_calcolo As String = "") As String

        Dim ris As String = "0"

        Dim sqlstr As String = "select imponibile, iva_imponibile, nome_costo, valore_costo  from prenotazioni_costi " &
        "where id_documento ='" & id_prenotazione & "' and num_calcolo='" & num_calcolo & "' and (id_elemento='98')"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()

                ris = Rs1!valore_costo

            End If

            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception

        End Try

        Return ris


    End Function
    Public Shared Function GetUltimiGiorniNolo(num_contratto As String, num_calcolo As String) As String

        Dim ris As String = "0"
        'inserita condizione Valore_tariffa in modo da prendere il valore corretto dei giorni di noleggio 31.05.2023
        Dim sqlstr As String = "SELECT  contratti_costi.qta, contratti_costi.id_documento , contratti_costi.valore_costo, contratti_costi.nome_costo, contratti_costi.num_calcolo, contratti.attivo " &
        "From contratti_costi INNER JOIN contratti ON contratti_costi.id_documento = contratti.id " &
        "WHERE   (contratti.num_contratto ='" & num_contratto & "') AND (contratti_costi.num_calcolo = '" & num_calcolo & "') AND nome_costo like 'Valore Tariffa' " &
        "order by id_documento desc, num_calcolo"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()

                ris = Rs1!qta

            End If

            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception

        End Try

        Return ris


    End Function
    Public Shared Function GetUltimoValoreTariffaValido(num_contratto As String, Optional num_calcolo As String = "") As String

        Dim ris As String = "0"

        Dim sqlstr As String = "SELECT  contratti_costi.id_documento , contratti_costi.valore_costo, contratti_costi.nome_costo, contratti_costi.num_calcolo, contratti.attivo " &
        "From contratti_costi INNER JOIN contratti ON contratti_costi.id_documento = contratti.id " &
        "WHERE   (contratti.num_contratto ='" & num_contratto & "') AND (contratti_costi.num_calcolo > -1) AND (contratti_costi.id_elemento ='98') " &
        "order by id_documento desc, num_calcolo"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()

                ris = Rs1!valore_costo

            End If

            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception

        End Try

        Return ris


    End Function

    Public Shared Function VerificaDatiConducenteFattura(id_documento As String, num_calcolo As String) As String

        Dim ris As String = "0"

        '''' DA VERIFICARE
        Return ris
        Exit Function
        Dim sqlstr As String = "select valore_costo from prenotazioni_costi where id_documento=" & id_documento & " and num_calcolo=" & num_calcolo & "and nome_costo like 'SCONTO'"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()

                ris = Rs1!valore_costo

            End If

            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception

        End Try

        Return ris


    End Function

    Public Shared Function GetImportoScontoPreventivo(id_documento As String, num_calcolo As String) As String 'salvo 14.04.2023

        Dim ris As String = "0"

        Dim sqlstr As String = "select valore_costo from preventivi_costi where id_documento=" & id_documento & " and num_calcolo=" & num_calcolo & "and nome_costo like 'SCONTO'"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()

                ris = Rs1!valore_costo

            End If

            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception

        End Try

        Return ris


    End Function

    Public Shared Function GetImportoScontoPrenotazione(id_documento As String, num_calcolo As String) As String

        Dim ris As String = "0"

        Dim sqlstr As String = "select valore_costo from prenotazioni_costi where id_documento=" & id_documento & " and num_calcolo=" & num_calcolo & "and nome_costo like 'SCONTO'"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()

                ris = Rs1!valore_costo

            End If

            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception

        End Try

        Return ris


    End Function

    Public Shared Function GetMaxScontoTariffa(id_tariffa As String, id_tempo_km As String) As String

        Dim ris As String = "0"

        Dim sqlstr As String = "select max_sconto from tariffe_righe where id_tempo_km = " & id_tempo_km & " and id=" & id_tariffa & " "

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()

                ris = Trim(Rs1!max_sconto)

            End If

            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception

        End Try

        Return ris


    End Function



    Public Shared Function AggiornaCostiSconto(id_doc As String, num_calcolo As String, tipo_doc As String, valore_senza_sconto As Double, valore_con_sconto As Double) As String
        Dim ris As String = ""

        Dim tbl As String
        If tipo_doc = "P" Then      'x Preventivi
            tbl = "preventivi_costi"
        ElseIf tipo_doc = "C" Then  'x Contratti
            tbl = "contratti_costi"
        ElseIf tipo_doc = "R" Then 'x Reservation/Prenotazioni
            tbl = "prenotazioni_costi"
        End If



        Dim valore_costo As String = FormatNumber(valore_senza_sconto - valore_con_sconto, 2).ToString
        ris = valore_costo 'restituisce l'importo dello sconto

        valore_costo = valore_costo.Replace(".", "")  '#salvo aggiunto 17.05.2023  (andava in errore perchè migliaia)

        valore_costo = valore_costo.Replace(",", ".")
        Dim imp As String = valore_costo


        Dim sqlstr As String = "update " & tbl & " set valore_costo='" & imp & "',imponibile='" & imp & "', imponibile_scontato='" & imp & "' " &
                "WHERE id_documento='" & id_doc & "' AND nome_costo='SCONTO' AND num_calcolo='" & num_calcolo & "'"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim x As Integer = Cmd.ExecuteNonQuery()

            'If x > 0 Then
            '    'ris = True
            'End If

            Cmd.Dispose() '
            Cmd = Nothing
            Dbc.Close()
            Dbc = Nothing
        Catch ex As Exception
            ris = 0
            HttpContext.Current.Response.Write("error AggiornaCostiSconto: " & ex.Message & sqlstr & "</br>")

        End Try

        Return ris


    End Function








    Public Shared Function getDataCreazione(numdoc As String, tipodoc As String) As String

        Dim ris As String = ""
        Dim sqla As String = ""
        Dim tb As String = ""

        If tipodoc = "CONT" Then
            tb = "contratti"
            sqla = "SELECT data_creazione FROM " & tb & " WITH(NOLOCK) WHERE num_contratto='" & numdoc & "' AND attivo='1' order by data_creazione"
        ElseIf tipodoc = "PREN" Then
            tb = "prenotazioni"
            sqla = "SELECT data_creazione FROM " & tb & " WITH(NOLOCK) WHERE [Nr_Pren]='" & numdoc & "' AND attiva='1' order by data_creazione"
        ElseIf tipodoc = "PREV" Then
            tb = "preventivi"
            sqla = "SELECT data_creazione FROM " & tb & " WITH(NOLOCK) WHERE num_preventivo='" & numdoc & "' order by data_creazione"
        End If

        Try

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows Then
                Rs.Read()
                If Not IsDBNull(Rs!data_creazione) Then
                    ris = Rs!data_creazione
                End If

            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("error GetDataCreazione  : <br/>" & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

        Return ris

    End Function

    Public Shared Function GetCodiceTariffa(idtariffa As String) As String

        Dim ris As String = ""

        Dim sqlstr As String = "Select tariffe_righe.id, tariffe_righe.id_tariffa, tariffe_righe.id_tempo_km,  tariffe.codice "
        sqlstr += "From tariffe_righe INNER Join tariffe On tariffe_righe.id_tariffa = tariffe.id "
        sqlstr += "Where (tariffe_righe.id = '" & idtariffa & "')"

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()

                ris = Trim(Rs1!codice)

            End If

            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception

        End Try

        Return ris


    End Function


    Public Shared Function getGiorniDiNoleggioNew(ByVal pick_up As String, ByVal drop_off As String, ByVal ora_pick_up As String,
                                                  ByVal minuti_pick_up As String, ByVal ora_drop_off As String, ByVal minuti_drop_off As String,
                                                  ByVal id_tariffe_righe As String,
                                                  Optional ByVal considerare_tolleranza_extra As Boolean = False) As Integer

        'RESTITUISCE I GIORNI DI NOLEGGIO DATI DATA E ORA DI PICK UP, DATA E ORA DI DROP OFF E ID DELLA TABELLA tariffe_righe (INFATTI I 
        'MINUTI DI RITARDO MASSIMO CONSENTITI PRIMA DI FAR SCATTARE IL GIORNO EXTRA DI NOLEGGIO SONO MEMORIZZATI AL LIVELLO DI ASSOCIAZIONE
        'TEMPO+KM/CONDIZIONE, QUINDI PER CALCOLARE I GIORNI DI NOLEGGIO SERVE SAPERE LA RIGA DI TARIFFA)
        'SE SPEFICIATO LA FUNZIONE CONSIDERERA' ANCHE I MINUTI DI TOLLERANZA EXTRA (PER RIENTRO AUTO DA RA).

        Dim sqla As String = ""
        Dim ris As Integer = 0

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
                ris = 1
            Else
                ris = DateDiff(DateInterval.Day, CDate(pick_up), CDate(drop_off))
                If CInt(ora_pick_up) <= CInt(ora_drop_off) Then
                    '(ORE2*60 + MINUTI2) - (ORE1*60 + MINUTI1) = (ORE2 - ORE1)*60 + MINUTI2 - MINUT1
                    Dim minuti_extra_di_noleggio As Integer = 60 * (CInt(ora_drop_off) - CInt(ora_pick_up)) + CInt(minuti_drop_off) - CInt(minuti_pick_up)
                    If minuti_extra_di_noleggio > minuti_di_ritardo Then
                        ris = ris + 1
                    End If
                End If
            End If

            If ris = 0 Then
                ris = ris + 1
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

        Catch ex As Exception

            HttpContext.Current.Response.Write("error funzioni_comuni_new getGiorniDiNoleggioNew : " & ex.Message & "<br/>" & sqla & "<br/>")
        End Try

        Return ris




    End Function




    Public Shared Function GetIDContrattoFromNum(nco As String) As Array

        'verifica se presente firma 14.12.2022 
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim ris(2) As String

        Try

            Dim sqlStr As String = "select top(1) id, num_calcolo, giorni from contratti where num_contratto = '" & nco & "' order by num_calcolo desc"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()

                ris(0) = Rs1!id
                ris(1) = Rs1!num_calcolo
                ris(2) = Rs1!giorni

            Else

                ris(0) = "0"
                ris(1) = "0"
                ris(2) = "0"
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


    Public Shared Function GetNewTariffaTempoKmPeriodiPDFTariffe(idStazionePickUp As String, idStazioneDropOff As String, dataPickup As String, dataDropOff As String, tipoCli As String,
                                                       idGruppo As String, TipoTariffa As String, descTariffa As String, idTariffa As String, giorniNoleggio As String, Optional mensile As Boolean = False) As String


        'Salvo dal 17.11.2022
        Dim ris As String


        Dim sqlstr As String

        Try


            'recupera i parametri per il calcolo



            'Dim idStazionePickUp As String = dropStazionePickUp.SelectedValue
            'Dim idStazioneDropOff As String = dropStazioneDropOff.SelectedValue

            'Dim dataPickup As String = txtDaData.Text   '"21/12/2022"
            'Dim dataDropOff As String = txtAData.Text   '"24/12/2022"

            Dim gnolo As Integer = CInt(giorniNoleggio) 'DateDiff("d", CDate(dataPickup), CDate(dataDropOff))
            'If gnolo = 0 Then
            '    gnolo = 1 'aggiornato salvo 10.02.2023
            'End If

            'Dim tipoCli As String = dropTipoCliente.SelectedValue 
            'Dim descTariffa As String

            'Dim idGruppo As String = "24"
            Dim id_tariffe_righe As String

            'verifica se tariffe generiche o tariffe particolari 
            'e ricava valore dal dropdown
            'Dim TipoTariffa As String
            'If dropTariffeGeneriche.SelectedValue <> "0" Then
            '    id_tariffe_righe = dropTariffeGeneriche.SelectedValue
            '    descTariffa = dropTariffeGeneriche.SelectedItem.ToString
            '    TipoTariffa = "G"
            'ElseIf dropTariffeParticolari.SelectedValue <> "0" Then
            '    id_tariffe_righe = dropTariffeParticolari.SelectedValue
            '    descTariffa = dropTariffeParticolari.SelectedItem.ToString
            '    TipoTariffa = "P"
            'End If
            descTariffa = Replace(descTariffa, "'", "''")


            'ciclo per verificare id_tariffa per quel giorno
            'se cambia tariffa calcola sempre dal giorno 1 


            Dim dataDropOff_SQL As String = Year(CDate(dataDropOff)) & "-" & Month(dataDropOff) & "-" & Day(dataDropOff)

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim idTariffaCicloAttuale As String
            Dim idTariffaCicloAltra As String
            Dim nGiornoCalcolo As Integer = 0




            Dim aIdTariffa() As String      'id della tariffa per i giorni Array
            Dim aNumGiorni() As String      'numero di giorni per quella tariffa Array
            Dim aIdCondizioneMadre() As String
            Dim aIdCondizione() As String
            Dim aIdTempoKM() As String

            Dim contaTariffe As Integer = 0

            For xg = 0 To (gnolo - 1)

                Dim gVerifica As String = DateAdd("d", xg, CDate(dataPickup))

                Dim giornoNolo As String = Year(CDate(gVerifica)) & "-" & Month(gVerifica) & "-" & Day(gVerifica)


                'If TipoTariffa = "G" Then

                '    sqlstr = "(SELECT tariffe_righe.max_sconto, tariffe_righe.id_tempo_km,tariffe_righe.id_condizione_madre, tariffe_righe.id_condizione, tariffe_righe.id,tariffe.codice FROM tariffe WITH(NOLOCK) INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
                '    "WHERE tariffe.attiva='1' AND convert(datetime,'" & giornoNolo & " 00:00:00',102) BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a AND GetDate() " &
                '    "BETWEEN tariffe_righe.vendibilita_da And tariffe_righe.vendibilita_a And tariffe.id Not IN (SELECT DISTINCT id_tariffa FROM tariffe_X_stazioni WITH(NOLOCK) " &
                '    "WHERE id_tariffa=tariffe.id) And tariffe.id Not IN (SELECT DISTINCT id_tariffa FROM tariffe_X_fonti WITH(NOLOCK) " &
                '    "WHERE id_tariffa=tariffe.id) And convert(datetime,'" & dataDropOff_SQL & " 00:00:00',102) <= ISNULL(max_data_rientro, '3000-12-12 23:59:59') "
                '    sqlstr += "AND codice Like '" & descTariffa & "') ORDER BY codice " 'And tariffe_righe.id='" & idTariffa & "') " 'codice Like '" & descTariffa & "' salvo modificato 25.01.2023

                'Else
                '    sqlstr = "Select tariffe_righe.max_sconto, tariffe_righe.id_tempo_km, tariffe_righe.id_condizione_madre, tariffe_righe.id_condizione, tariffe_righe.id, ISNULL((Select nome_tariffa FROM tariffe_X_fonti " &
                '    "WHERE tariffe_x_fonti.id_tariffa = tariffe.id And id_tipologia_cliente ='" & tipoCli & "'),tariffe.codice) As codice FROM tariffe " &
                '    "WITH(NOLOCK) INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
                '    "WHERE tariffe.attiva ='1' And Convert(DateTime,'" & giornoNolo & " 00:00:00',102) BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " &
                '    "And GetDate() BETWEEN tariffe_righe.vendibilita_da And tariffe_righe.vendibilita_a " &
                '    "And convert(datetime,'" & dataDropOff_SQL & " 00:00:00',102) <= ISNULL(max_data_rientro, '3000-12-12 23:59:59') " &
                '    "And ( (((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                '    "WHERE id_stazione ='" & idStazionePickUp & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND (tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                '    "WHERE id_stazione='" & idStazioneDropOff & "' AND tipo='DROP' AND id_tariffa=tariffe.id)))  OR ((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                '    "WHERE id_stazione ='" & idStazionePickUp & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                '    "WHERE tipo='DROP' AND id_tariffa=tariffe.id)))  OR ((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                '    "WHERE id_stazione ='" & idStazioneDropOff & "' AND tipo='DROP' AND id_tariffa=tariffe.id)) AND (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                '    "WHERE tipo='PICK' AND id_tariffa=tariffe.id))) ) AND (tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) " &
                '    "WHERE id_tipologia_cliente ='" & tipoCli & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da))OR tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) " &
                '    "WHERE id_tariffa=tariffe.id)) Or (tariffe.id Not IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                '    "WHERE tipo ='PICK' AND id_tariffa=tariffe.id) AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                '    "WHERE tipo='DROP' AND id_tariffa=tariffe.id) AND tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) " &
                '    "WHERE id_tipologia_cliente ='" & tipoCli & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da)) )) "
                '    sqlstr += "AND codice Like '" & descTariffa & "'"
                '    '"And tariffe_righe.id='" & idTariffa & "' " 'codice Like '" & descTariffa & "'" ' AND tariffe_righe.id='" & idTariffa & "' " 'salvo modificato 25.01.2023

                'End If

                If TipoTariffa = "G" Then

                    sqlstr = "(SELECT tariffe_righe.max_sconto, tariffe_righe.id_tempo_km,tariffe_righe.id_condizione_madre, tariffe_righe.id_condizione, tariffe_righe.id,tariffe.codice FROM tariffe WITH(NOLOCK) INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
                    "WHERE tariffe.attiva='1' AND convert(datetime,'" & giornoNolo & " 00:00:00',102) BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a AND GetDate() " &
                    "BETWEEN tariffe_righe.vendibilita_da And tariffe_righe.vendibilita_a And tariffe.id Not IN (SELECT DISTINCT id_tariffa FROM tariffe_X_stazioni WITH(NOLOCK) " &
                    "WHERE id_tariffa=tariffe.id) And tariffe.id Not IN (SELECT DISTINCT id_tariffa FROM tariffe_X_fonti WITH(NOLOCK) " &
                    "WHERE id_tariffa=tariffe.id) And convert(datetime,'" & dataDropOff_SQL & " 00:00:00',102) <= ISNULL(max_data_rientro, '3000-12-12 23:59:59') "
                    sqlstr += "AND codice Like '" & Trim(descTariffa) & "') ORDER BY codice " 'And tariffe_righe.id='" & idTariffa & "') " 'codice Like '" & descTariffa & "' salvo modificato 25.01.2023

                Else

                    sqlstr = "Select tariffe_righe.max_sconto, tariffe_righe.id_tempo_km, tariffe_righe.id_condizione_madre, tariffe_righe.id_condizione, tariffe_righe.id, ISNULL((Select nome_tariffa FROM tariffe_X_fonti " &
                    "WHERE tariffe_x_fonti.id_tariffa = tariffe.id And id_tipologia_cliente ='" & tipoCli & "'),tariffe.codice) As codice FROM tariffe " &
                    "WITH(NOLOCK) INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
                    "WHERE tariffe.attiva ='1' And Convert(DateTime,'" & giornoNolo & " 00:00:00',102) BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a "
                    '"And GetDate() BETWEEN tariffe_righe.vendibilita_da And tariffe_righe.vendibilita_a "
                    'ho rimosso la vendibilità per recuperare la tariffa in qualsiasi periodo 14.05.2023 salvo
                    sqlstr += "And Convert(DateTime,'" & giornoNolo & " 00:00:00',102) BETWEEN tariffe_righe.vendibilita_da And tariffe_righe.vendibilita_a "
                    'riga sopra modificata
                    sqlstr += "And convert(datetime,'" & dataDropOff_SQL & " 00:00:00',102) <= ISNULL(max_data_rientro, '3000-12-12 23:59:59') " &
                    "And ( (((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                    "WHERE id_stazione ='" & idStazionePickUp & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND (tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                    "WHERE id_stazione='" & idStazioneDropOff & "' AND tipo='DROP' AND id_tariffa=tariffe.id)))  OR ((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                    "WHERE id_stazione ='" & idStazionePickUp & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                    "WHERE tipo='DROP' AND id_tariffa=tariffe.id)))  OR ((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                    "WHERE id_stazione ='" & idStazioneDropOff & "' AND tipo='DROP' AND id_tariffa=tariffe.id)) AND (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                    "WHERE tipo='PICK' AND id_tariffa=tariffe.id))) ) AND (tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) " &
                    "WHERE id_tipologia_cliente ='" & tipoCli & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da))OR tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) " &
                    "WHERE id_tariffa=tariffe.id)) Or (tariffe.id Not IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                    "WHERE tipo ='PICK' AND id_tariffa=tariffe.id) AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                    "WHERE tipo='DROP' AND id_tariffa=tariffe.id) AND tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) " &
                    "WHERE id_tipologia_cliente ='" & tipoCli & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da)) )) "
                    sqlstr += "AND (codice Like '" & Trim(descTariffa) & "' OR tariffe_righe.id='" & idTariffa & "')"   'bisogna verificare se deve essere filtrato x codice o per id tariffa 06.02.2023
                    'sqlstr += "And tariffe_righe.id='" & idTariffa & "' " 'codice Like '" & descTariffa & "'" ' AND tariffe_righe.id='" & idTariffa & "' " 'salvo modificato 25.01.2023

                End If

                Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                Dim Rs1 As Data.SqlClient.SqlDataReader
                Rs1 = Cmd1.ExecuteReader()

                If Rs1.HasRows Then

                    While Rs1.Read

                        idTariffaCicloAttuale = Rs1!id

                        If idTariffaCicloAttuale <> idTariffaCicloAltra Then
                            'riparte dal primo giorno il calcolo di quella tariffa

                            If idTariffaCicloAltra <> "" Then 'alla prima assegnazione di tariffa non registra giorni totali
                                'se cambia registra il nGiornoCalcolo che si è raggiunto per quella tariffa
                                If contaTariffe = 0 Then
                                    ReDim aNumGiorni(0)
                                Else
                                    ReDim Preserve aNumGiorni(contaTariffe)
                                End If
                                aNumGiorni(contaTariffe) = nGiornoCalcolo

                                'se diversa 
                                contaTariffe += 1  'e incrementa il conteggio delle tariffe x gli array
                            End If

                            nGiornoCalcolo = 1

                            'Assegna valori ad array
                            If contaTariffe = 0 Then
                                ReDim aIdTariffa(0)
                                ReDim aIdCondizioneMadre(0)
                                ReDim aIdCondizione(0)
                                ReDim aIdTempoKM(0)
                            Else
                                ReDim Preserve aIdTariffa(contaTariffe)
                                ReDim Preserve aIdCondizioneMadre(contaTariffe)
                                ReDim Preserve aIdCondizione(contaTariffe)
                                ReDim Preserve aIdTempoKM(contaTariffe)
                            End If
                            aIdTariffa(contaTariffe) = idTariffaCicloAttuale
                            aIdCondizioneMadre(contaTariffe) = "0" 'Rs1!id_condizione_madre
                            aIdCondizione(contaTariffe) = "0" 'Rs1!id_condizione
                            aIdTempoKM(contaTariffe) = Rs1!id_tempo_km

                            If idTariffaCicloAltra = "" Then
                                idTariffaCicloAltra = idTariffaCicloAttuale 'assegna tariffa
                                'senza incrementare contaTariffe
                                'assegna 1 giorno all'array Numgiorni se prima tariffa
                                ReDim aNumGiorni(0)
                                aNumGiorni(0) = 1
                            Else
                                idTariffaCicloAltra = idTariffaCicloAttuale 'assegna tariffa
                                ReDim Preserve aNumGiorni(contaTariffe) 'imposta i giorni sulla nuova tariffa
                                aNumGiorni(contaTariffe) = nGiornoCalcolo

                            End If

                        Else

                            'stessa tariffa incrementa giorno per quella tariffa
                            nGiornoCalcolo += 1

                            'e registra su array
                            ReDim Preserve aNumGiorni(contaTariffe)
                            aNumGiorni(contaTariffe) = nGiornoCalcolo


                        End If

                        'assegna giorno totale se la tariffa è diversa





                        Dim ngiornoNolo As Integer = (xg + 1)

                        'Response.Write("giorno di nolo in sequenza: " & ngiornoNolo & " - numero giorno calcolato su tariffa:" & nGiornoCalcolo & " - " & giornoNolo & " - idTariffa: " & Rs1!id & "<br/>")
                        'Response.Write("id_condizione_madre: " & Rs1!id_condizione_madre & " - id_condizione: " & Rs1!id_condizione & "<br/>")



                        'recupera il valore dell'importo in funzione del giorno da calcolare


                    End While

                End If
                Rs1.Close()
                Rs1 = Nothing
                Cmd1.Dispose()
                Cmd1 = Nothing


            Next 'recupero id_tariffa per qual giorno


            Dbc.Close()
            Dbc = Nothing

            idTariffaCicloAltra = ""
            idTariffaCicloAttuale = ""


            Dim ValoreImporto As Double = 0

            'crea string per stampa PDF

            Dim ParametriPDF As String = ""
            Dim ListIdTempoKm As String = ""

            ''calcola gli importi per le tariffe trovate
            For xt = 0 To UBound(aIdTempoKM)

                Dim tkm As String = aIdTempoKM(xt)

                If ListIdTempoKm = "" Then
                    ListIdTempoKm = tkm
                Else
                    ListIdTempoKm += "," & tkm
                End If

            Next

            ParametriPDF = ListIdTempoKm

            'creo la lista di ID_Condizione
            Dim ListIdCondizione As String = ""
            For xt = 0 To UBound(aIdCondizione)

                Dim IdCondizione As String = aIdCondizione(xt)

                If ListIdCondizione = "" Then
                    ListIdCondizione = IdCondizione
                Else
                    ListIdCondizione += "," & IdCondizione
                End If

            Next
            ParametriPDF += "&idco=" & ListIdCondizione

            'creo la lista di ID_CondizioneMadre
            Dim ListIdCondizioneMadre As String = ""
            For xt = 0 To UBound(aIdCondizioneMadre)

                Dim IdCondizioneMadre As String = aIdCondizioneMadre(xt)

                If ListIdCondizioneMadre = "" Then
                    ListIdCondizioneMadre = IdCondizioneMadre
                Else
                    ListIdCondizioneMadre += "," & IdCondizioneMadre
                End If

            Next
            ParametriPDF += "&idcom=" & ListIdCondizioneMadre


            'creo la lista di id_tariffa
            Dim ListIdTariffa As String = ""
            For xt = 0 To UBound(aIdTariffa)

                Dim IdTar As String = aIdTariffa(xt)

                If ListIdTariffa = "" Then
                    ListIdTariffa = IdTar
                Else
                    ListIdTariffa += "," & IdTar
                End If

            Next
            ParametriPDF += "&idtar=" & ListIdTariffa

            ris = ParametriPDF        'restituisce la lista delle tariffe interessate x la stampa in pdf

        Catch ex As Exception

            WriteLogError("GetNewTariffaTempoKmPeriodiPDFTariffe Error: " & ex.Message)


        End Try


        Return ris


    End Function


    Public Shared Function GetNewTariffaTempoKmPeriodi(idStazionePickUp As String, idStazioneDropOff As String, dataPickup As String, dataDropOff As String, tipoCli As String,
                                                       idGruppo As String, TipoTariffa As String, descTariffa As String, giorniNoleggio As String, idTariffa As String,
                                                       mensile As Boolean, oraPickUp As String, oraDropOff As String, max_sconto As String, Optional DaARES As Boolean = True,
                                                       Optional ggExtra As String = "0", Optional ValoreTariffaOri As String = "0", Optional SetTariffaOri As Boolean = False) As Double


        'Salvo dal 17.11.2022
        Dim ris As Double = 0
        Dim sqlstr As String
        Dim errorline As Integer = 0

        '# aggiunto salvo 28.02.2023
        'verifica se periodo nolo all'interno di unica tariffa (default FALSE)
        Dim FlagTariffaUnica As Boolean = False
        Dim giornoStart As Integer = 1
        '@end salvo

        Try
            'recupera i parametri per il calcolo
            'giorni di noleggio totali
            Dim gnolo As Integer = CInt(giorniNoleggio) ' DateDiff("d", CDate(dataPickup), CDate(dataDropOff))

            '#salvo aggiunto 23.02.2023
            If CDbl(ggExtra) > 0 Then

                'verifica se periodo nolo all'interno di unica tariffa (default FALSE)
                FlagTariffaUnica = funzioni_comuni_new.GetTariffaUnica(idTariffa, dataPickup, dataDropOff) '# aggiunto salvo 28.02.2023

                If FlagTariffaUnica = True Then
                    'se tariffa unica il calcolo dei giorni è in sequenza dal primo

                    'la tariffa originale è presente e  ci sono ggextra
                    'deve effettuare il calcolo solo x i ggextra
                    'e il valore sommarlo a quello della tariffa originale - salvo 07.03.2023
                    If CDbl(ValoreTariffaOri) > 0 And CInt(ggExtra) Then
                        'il calcolo viene effettuato spostando la data di pickup
                        gnolo = ggExtra
                        dataPickup = DateAdd("d", -CDbl(ggExtra), CDate(dataDropOff))
                    End If



                Else
                    'se si tratta di tariffe diverse riparte dal primo giorno della tariffa
                    'di riferimento del primo giorno extra
                    gnolo = ggExtra
                    'cambia la data di pickup per il solo calcolo dei giorni extra
                    'che diventa la data di dropoff - i ggextra
                    dataPickup = DateAdd("d", -CDbl(ggExtra), CDate(dataDropOff))
                End If



            End If
            '@end salvo

            Dim id_tariffe_righe As String

            descTariffa = Replace(descTariffa, "'", "''")

            'ciclo per verificare id_tariffa per quel giorno
            'se cambia tariffa calcola sempre dal giorno 1 

            errorline = 1

            Dim dataDropOff_SQL As String = Year(CDate(dataDropOff)) & "-" & Month(dataDropOff) & "-" & Day(dataDropOff)

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim idTariffaCicloAttuale As String = ""
            Dim idTariffaCicloAltra As String = ""
            Dim nGiornoCalcolo As Integer = 0

            Dim aIdTariffa() As String      'id della tariffa per i giorni Array
            Dim aNumGiorni() As String      'numero di giorni per quella tariffa Array
            Dim aIdCondizioneMadre() As String
            Dim aIdCondizione() As String
            Dim aIdTempoKM() As String

            Dim aIdTariffaPeriodo() As String 'aggiunto salvo 16.06.2023 per il range di pickup di quella tariffa
            Dim aIdTariffaNome() As String 'aggiunto salvo 16.06.2023 per il nome di quella tariffa

            errorline = 2

            Dim contaTariffe As Integer = 0

            For xg = 0 To (gnolo - 1)


                errorline = 3

                Dim gVerifica As String = DateAdd("d", xg, CDate(dataPickup))


                errorline = 4
                Dim giornoNolo As String = Year(CDate(gVerifica)) & "-" & Month(gVerifica) & "-" & Day(gVerifica)

                errorline = 5


                'se la descrizione tariffa contiene (PREN) o (RA) sostituisce con % - salvo 13.12.2022
                If descTariffa.IndexOf("(PREN)") > -1 Then
                    descTariffa = Trim(Replace(descTariffa, " (PREN)", ""))
                End If
                errorline = 6
                If descTariffa.IndexOf("(RA)") > -1 Then
                    descTariffa = Trim(Replace(descTariffa, " (RA)", ""))
                End If

                errorline = 7
                'descTariffa = "Web prepagata CTA"       'test

                If TipoTariffa = "G" Then

                    sqlstr = "(SELECT tariffe_righe.pickup_da as PickUpDa, tariffe_righe.pickup_a as PickUpA, tariffe_righe.max_sconto, tariffe_righe.id_tempo_km,tariffe_righe.id_condizione_madre, tariffe_righe.id_condizione, tariffe_righe.id,tariffe.codice FROM tariffe WITH(NOLOCK) INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
                    "WHERE tariffe.attiva='1' AND convert(datetime,'" & giornoNolo & " 00:00:00',102) BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a AND GetDate() " &
                    "BETWEEN tariffe_righe.vendibilita_da And tariffe_righe.vendibilita_a And tariffe.id Not IN (SELECT DISTINCT id_tariffa FROM tariffe_X_stazioni WITH(NOLOCK) " &
                    "WHERE id_tariffa=tariffe.id) And tariffe.id Not IN (SELECT DISTINCT id_tariffa FROM tariffe_X_fonti WITH(NOLOCK) " &
                    "WHERE id_tariffa=tariffe.id) And convert(datetime,'" & dataDropOff_SQL & " 00:00:00',102) <= ISNULL(max_data_rientro, '3000-12-12 23:59:59') "
                    sqlstr += "AND codice Like '" & Trim(descTariffa) & "') ORDER BY codice " 'And tariffe_righe.id='" & idTariffa & "') " 'codice Like '" & descTariffa & "' salvo modificato 25.01.2023

                Else
                    sqlstr = "Select tariffe_righe.pickup_da as PickUpDa, tariffe_righe.pickup_a as PickUpA, tariffe_righe.max_sconto, tariffe_righe.id_tempo_km, tariffe_righe.id_condizione_madre, tariffe_righe.id_condizione, tariffe_righe.id, ISNULL((Select nome_tariffa FROM tariffe_X_fonti " &
                    "WHERE tariffe_x_fonti.id_tariffa = tariffe.id And id_tipologia_cliente ='" & tipoCli & "'),tariffe.codice) As codice FROM tariffe " &
                    "WITH(NOLOCK) INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
                    "WHERE tariffe.attiva ='1' And Convert(DateTime,'" & giornoNolo & " 00:00:00',102) BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a "
                    '"And GetDate() BETWEEN tariffe_righe.vendibilita_da And tariffe_righe.vendibilita_a "
                    sqlstr += "And Convert(DateTime,'" & giornoNolo & " 00:00:00',102) BETWEEN tariffe_righe.vendibilita_da And tariffe_righe.vendibilita_a "
                    'riga sopra modificata
                    sqlstr += "And convert(datetime,'" & dataDropOff_SQL & " 00:00:00',102) <= ISNULL(max_data_rientro, '3000-12-12 23:59:59') " &
                    "And ( (((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                    "WHERE id_stazione ='" & idStazionePickUp & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND (tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                    "WHERE id_stazione='" & idStazioneDropOff & "' AND tipo='DROP' AND id_tariffa=tariffe.id)))  OR ((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                    "WHERE id_stazione ='" & idStazionePickUp & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                    "WHERE tipo='DROP' AND id_tariffa=tariffe.id)))  OR ((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                    "WHERE id_stazione ='" & idStazioneDropOff & "' AND tipo='DROP' AND id_tariffa=tariffe.id)) AND (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                    "WHERE tipo='PICK' AND id_tariffa=tariffe.id))) ) AND (tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) " &
                    "WHERE id_tipologia_cliente ='" & tipoCli & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da))OR tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) " &
                    "WHERE id_tariffa=tariffe.id)) Or (tariffe.id Not IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                    "WHERE tipo ='PICK' AND id_tariffa=tariffe.id) AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) " &
                    "WHERE tipo='DROP' AND id_tariffa=tariffe.id) AND tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) " &
                    "WHERE id_tipologia_cliente ='" & tipoCli & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da)) )) "
                    sqlstr += "AND (codice Like '" & Trim(descTariffa) & "' OR tariffe_righe.id='" & idTariffa & "')"   'bisogna verificare se deve essere filtrato x codice o per id tariffa 06.02.2023
                    'sqlstr += "And tariffe_righe.id='" & idTariffa & "' " 'codice Like '" & descTariffa & "'" ' AND tariffe_righe.id='" & idTariffa & "' " 'salvo modificato 25.01.2023

                End If


                '(Date.Now.ToString & "-GetNewTariffaTempoKMPeriodi SQL: " & sqlstr)  'x Test

                errorline = 8
                Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                Dim Rs1 As Data.SqlClient.SqlDataReader
                Rs1 = Cmd1.ExecuteReader()

                errorline = 9

                If Rs1.HasRows Then

                    While Rs1.Read

                        errorline = 10
                        idTariffaCicloAttuale = Rs1!id

                        If idTariffaCicloAttuale <> idTariffaCicloAltra Then
                            'riparte dal primo giorno il calcolo di quella tariffa

                            errorline = 11

                            If idTariffaCicloAltra <> "" Then 'alla prima assegnazione di tariffa non registra giorni totali
                                'se cambia registra il nGiornoCalcolo che si è raggiunto per quella tariffa
                                If contaTariffe = 0 Then
                                    ReDim aNumGiorni(0)
                                Else
                                    ReDim Preserve aNumGiorni(contaTariffe)
                                End If
                                aNumGiorni(contaTariffe) = nGiornoCalcolo

                                'se diversa 
                                contaTariffe += 1  'e incrementa il conteggio delle tariffe x gli array
                                'SPOSTATA SOTTO
                            End If

                            errorline = 12
                            nGiornoCalcolo = 1

                            'Assegna valori ad array
                            If contaTariffe = 0 Then
                                ReDim aIdTariffa(0)
                                ReDim aIdCondizioneMadre(0)
                                ReDim aIdCondizione(0)
                                ReDim aIdTempoKM(0)
                                ReDim aIdTariffaPeriodo(0) 'aggiunto 16.06.2023
                                ReDim aIdTariffaNome(0) 'aggiunto 16.06.2023

                            Else
                                ReDim Preserve aIdTariffa(contaTariffe)
                                ReDim Preserve aIdCondizioneMadre(contaTariffe)
                                ReDim Preserve aIdCondizione(contaTariffe)
                                ReDim Preserve aIdTempoKM(contaTariffe)
                                ReDim Preserve aIdTariffaPeriodo(contaTariffe) 'aggiunto 16.06.2023
                                ReDim Preserve aIdTariffaNome(contaTariffe) 'aggiunto 16.06.2023


                            End If
                            aIdTariffa(contaTariffe) = idTariffaCicloAttuale

                            'aggiunto salvo 16.06.2023
                            aIdTariffaPeriodo(contaTariffe) = FormatDateTime(Rs1!PickUpDa, vbShortDate) & "-" & FormatDateTime(Rs1!PickUpA, vbShortDate)
                            aIdTariffaNome(contaTariffe) = Trim(Rs1!codice)
                            '@end salvo



                            aIdCondizioneMadre(contaTariffe) = "0" 'Rs1!id_condizione_madre
                            aIdCondizione(contaTariffe) = "0" 'Rs1!id_condizione

                            If IsDBNull(Rs1!id_tempo_km) Then       'se null perchè nn trovata
                                aIdTempoKM(contaTariffe) = 0
                            Else
                                aIdTempoKM(contaTariffe) = Rs1!id_tempo_km
                            End If

                            errorline = 13


                            If idTariffaCicloAltra = "" Then
                                idTariffaCicloAltra = idTariffaCicloAttuale 'assegna tariffa
                                'senza incrementare contaTariffe
                                'assegna 1 giorno all'array Numgiorni se prima tariffa
                                ReDim aNumGiorni(0)
                                aNumGiorni(0) = 1

                                'deve incrementare la tariffa che ha creato 22.02.2023 salvo
                                'contaTariffe += 1  'e incrementa il conteggio delle tariffe x gli array SPOSTATA DA SOPRA

                            Else
                                idTariffaCicloAltra = idTariffaCicloAttuale 'assegna tariffa
                                ReDim Preserve aNumGiorni(contaTariffe) 'imposta i giorni sulla nuova tariffa
                                aNumGiorni(contaTariffe) = nGiornoCalcolo

                            End If
                            errorline = 14
                        Else

                            errorline = 15
                            'stessa tariffa incrementa giorno per quella tariffa
                            nGiornoCalcolo += 1
                            errorline = 16
                            'e registra su array
                            ReDim Preserve aNumGiorni(contaTariffe)
                            aNumGiorni(contaTariffe) = nGiornoCalcolo


                        End If
                        errorline = 17
                        'assegna giorno totale se la tariffa è diversa
                        Dim ngiornoNolo As Integer = (xg + 1)

                        'Response.Write("giorno di nolo in sequenza: " & ngiornoNolo & " - numero giorno calcolato su tariffa:" & nGiornoCalcolo & " - " & giornoNolo & " - idTariffa: " & Rs1!id & "<br/>")
                        'Response.Write("id_condizione_madre: " & Rs1!id_condizione_madre & " - id_condizione: " & Rs1!id_condizione & "<br/>")

                    End While

                End If
                errorline = 18
                Rs1.Close()
                Rs1 = Nothing
                Cmd1.Dispose()
                Cmd1 = Nothing

            Next 'recupero id_tariffa per qual giorno

            errorline = 19
            Dbc.Close()
            Dbc = Nothing

            idTariffaCicloAltra = ""
            idTariffaCicloAttuale = ""

            Dim ValoreImporto As Double = 0

            errorline = 20

            ''calcola gli importi per le tariffe trovate se chiamata da ARES
            'le nasconde se chiamata da webservice
            If DaARES = True Then
                HttpContext.Current.Session("valore_preventivo") = "0"  'reset session salvo 19.01.2023
                HttpContext.Current.Session("perc_sconto_tariffa_tutte") = ""       'reset elenco sconti tariffe
                HttpContext.Current.Session("perc_sconto_tariffa") = ""
                HttpContext.Current.Session("list_tariffe") = ""
                HttpContext.Current.Session("list_tariffe_tempoKM") = ""
                HttpContext.Current.Session("list_tariffe_periodo") = ""
                HttpContext.Current.Session("list_tariffe_nome") = ""
            End If



            errorline = 21

            'WriteLogError("GetNewTariffaTempoKmPeriodi: UBOUNDIDtariffa: " & UBound(aIdTariffa).ToString)   'TEST

            If Not IsNothing(aIdTariffa) Then 'se non trova la tariffa corrispondente restituisce ZERO - 21.12.2022

                errorline = 22

                For xt = 0 To UBound(aIdTariffa)
                    errorline = 23
                    'Response.Write("idTariffa:" & aIdTariffa(xt) & " - id_condizione_madre: " & aIdCondizioneMadre(xt) & " - id_condizione: " & aIdCondizione(xt) & "<br/>")
                    'Response.Write("idTempoKM:" & aIdTempoKM(xt) & " - numGiorni:" & aNumGiorni(xt) & "<br/>")

                    Dim imp As Double = funzioni_comuni_new.GetNewTariffaTempoKm(aIdTempoKM(xt), idGruppo, aNumGiorni(xt), False, max_sconto, DaARES, idTariffa)

                    'Response.Write("Importo tariffa :" & imp.ToString & "<br/>")
                    errorline = 24
                    'assegna a session generale il totale senza sconto x le tariffe 19.01.2023
                    If DaARES = True Then
                        HttpContext.Current.Session("valore_preventivo") = CDbl(HttpContext.Current.Session("valore_preventivo")) + CDbl(HttpContext.Current.Session("valore_preventivo_tariffa"))
                    End If


                    ValoreImporto += imp
                    If DaARES = True Then
                        'HttpContext.Current.Session("perc_sconto_tariffa_tutte") += " (" & aIdTariffa(xt) & "-" & HttpContext.Current.Session("perc_sconto_tariffa") & ") "
                        'HttpContext.Current.Session("perc_sconto_tariffa_tutte") += "," & HttpContext.Current.Session("perc_sconto_tariffa")
                    End If
                    '

                    errorline = 25
                    If DaARES = True Then
                        If xt = 0 Then
                            HttpContext.Current.Session("perc_sconto_tariffa_tutte") = HttpContext.Current.Session("perc_sconto_tariffa")
                            HttpContext.Current.Session("list_tariffe_tempoKM") = aIdTempoKM(xt)
                            HttpContext.Current.Session("list_tariffe") = aIdTariffa(xt)
                            HttpContext.Current.Session("list_tariffe_periodo") = aIdTariffaPeriodo(xt)
                            HttpContext.Current.Session("list_tariffe_nome") = aIdTariffaNome(xt)
                        Else
                            HttpContext.Current.Session("perc_sconto_tariffa_tutte") += "," & HttpContext.Current.Session("perc_sconto_tariffa")
                            HttpContext.Current.Session("list_tariffe_tempoKM") += "," & aIdTempoKM(xt)
                            HttpContext.Current.Session("list_tariffe") += "," & aIdTariffa(xt)
                            HttpContext.Current.Session("list_tariffe_periodo") += "," & aIdTariffaPeriodo(xt)
                            HttpContext.Current.Session("list_tariffe_nome") += "," & aIdTariffaNome(xt)

                        End If
                    End If

                    ' WriteLogError("GetNewTariffaTempoKmPeriodi: TROVATA la tariffa corrispondente: " & aIdTempoKM(xt))   'TEST
                    errorline = 26

                Next

            Else

                'WriteLogError("GetNewTariffaTempoKmPeriodi: NON ROVATA la tariffa corrispondente restituisce ZERO")  'TEST

            End If

            '#se ggextra significa che è un ricalcolo salvo 23.02.2023
            'deve aggiungere la tariffa del periodo originale
            'se si tratta di tariffe diverse 28.02.2023
            'se tariffa ori e gg extra 07.03.2023 salvo
            If DaARES = True Then
                If (ggExtra <> "0" And FlagTariffaUnica = False) Or (ggExtra <> "0" And CDbl(ValoreTariffaOri > 0)) Then
                    ValoreImporto += CDbl(ValoreTariffaOri)
                End If
            End If

            ris = ValoreImporto

            If DaARES = True Then
                HttpContext.Current.Session("valore_preventivo_finale") = ValoreImporto '.ToString
            End If

        Catch ex As Exception

            WriteLogError("GetNewTariffaTempoKmPeriodi ERRORE: ( " & errorline & " )" & ex.Message)  'TEST
            ris = ris

        End Try

        Return ris

    End Function

    Public Shared Sub WriteLogError(msg_error As String)

        Dim nomeFileLog As String = "log_error_" & FormatDateTime(Date.Now, vbShortDate) & "-" & FormatDateTime(Date.Now, vbLongTime)
        nomeFileLog = nomeFileLog.Replace("/", "-")
        nomeFileLog = nomeFileLog.Replace(":", "")


        Dim Generator As System.Random = New System.Random()
        Dim num_random As String = Format(Generator.Next(1999), "0000")

        nomeFileLog += "-" & num_random
        nomeFileLog += ".txt"


        Dim pathFileLog As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "_log_error\"

        Try
            File.Create(pathFileLog + nomeFileLog).Close()

            Dim Stream As StreamWriter = New StreamWriter(pathFileLog + nomeFileLog, True)
            Stream.WriteLine(msg_error)
            Stream.Close()


        Catch ex As Exception

        End Try


    End Sub

    Public Shared Function GetDatiEnteMulte(ID_ente As String) As Array
        'verifica se presente nominativo in ditte altrimenti lo crea 
        'e restituisce nuovo ID

        Dim ris(9) As String
        Dim sqlstr As String = ""
        Dim flagDitta As Boolean = False

        Try



            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            sqlstr = "SELECT id,ente,indirizzo,comune,cap,prov,[tel],[email],[emailpec],[notes] FROM [Autonoleggio_SRC].[dbo].[multe_enti] where id=" & ID_ente & ""


            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris(0) = Rs1!tel & ""
                ris(1) = Rs1!email & ""
                ris(2) = Rs1!emailpec & ""
                ris(3) = Rs1!notes & ""
                ris(4) = Rs1!id & ""
                ris(5) = Rs1!ente & ""
                ris(6) = Rs1!indirizzo & ""
                ris(7) = Rs1!comune & ""
                ris(8) = Rs1!cap & ""
                ris(9) = Rs1!prov & ""

            Else
                ris(0) = ""
                ris(1) = ""
                ris(2) = ""
                ris(3) = ""
                ris(4) = ""
                ris(5) = ""
                ris(6) = ""
                ris(7) = ""
                ris(8) = ""
                ris(9) = ""
            End If

            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing



            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception
            ris(0) = ""
            ris(1) = ""
            ris(2) = ""
            ris(3) = ""
            ris(4) = ""
            ris(5) = ""
            ris(6) = ""
            ris(7) = ""
            ris(8) = ""
            ris(9) = ""

        End Try

        Return ris


    End Function


    Public Shared Function GetNewTariffaTempoKm(id_tempo_km_figlia As String, id_gruppo_da_calcolare As String,
                                                gg As Integer, mensile As Boolean, sconto_da_form As String, Optional DaARES As Boolean = True, Optional id_tariffa As String = "") As Double


        'Salvo 25.10.2022 aggiornato 18.01.2023 con parametro sconto obbligatorio

        Dim ris As Double = 0
        Dim valore As Double = 0

        Dim sqlstr As String = "Select righe_tempo_km.max_sconto,righe_tempo_km.pac, righe_tempo_km.valore,righe_tempo_km.da,righe_tempo_km.a FROM righe_tempo_km " &
                            "With(NOLOCK) INNER JOIN tempo_km With(NOLOCK) On righe_tempo_km.id_tempo_km=tempo_km.id " &
                         "INNER JOIN aliquote_iva With(NOLOCK) On tempo_km.id_aliquota_iva=aliquote_iva.id " &
                         "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND id_gruppo='" & id_gruppo_da_calcolare & "' " &
                         "AND NOT valore IS NULL AND valore<>0 AND valore<>0 AND " & gg & " >= da order by righe_tempo_km.da "


        'la seguente stringa è quella nel caso del rack
        'sqlstr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa FROM righe_tempo_km 
        '        WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
        '         "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
        '        "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_nolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' AND NOT valore IS NULL AND valore<>0"


        '# inserito per verificare sconto di quella riga tariffa salvo 11.02.2023
        'se parametro idtariffa è errato lo sconto sarà sempre zero
        Dim Max_sconto_tariffa As String = GetMaxScontoTariffa(id_tariffa, id_tempo_km_figlia)

        '@end salvo


        'WriteLogError("GetNewTariffaTempKm : " & sqlstr) 'test

        Dim aDa() As Integer
        Dim aA() As Integer
        Dim aVal() As Double
        Dim conta As Integer = 0

        Dim aMax_Sconto() As String  'aggiunto salvo 18.01.2023
        Dim MaxScontoTariffa As String 'aggiunto salvo 18.01.2023 in questo momento valido per tutti gli elementi della tariffa

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then

                While Rs1.Read

                    If conta = 0 Then
                        ReDim aDa(0)
                        ReDim aA(0)
                        ReDim aVal(0)
                        ReDim aMax_Sconto(0)
                    Else
                        ReDim Preserve aDa(conta)
                        ReDim Preserve aA(conta)
                        ReDim Preserve aVal(conta)
                        ReDim Preserve aMax_Sconto(conta)
                    End If

                    aDa(conta) = Rs1!da
                    aA(conta) = Rs1!a
                    aVal(conta) = Rs1!valore
                    aMax_Sconto(conta) = Max_sconto_tariffa ' Rs1!max_sconto ''aggiunto per lo sconto di ogni singola riga/tariffa 18.01.2023

                    conta += 1

                End While

            End If
            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing

            'calcolo valore 
            Dim restogg As Integer = gg
            Dim ggcalcolo As Integer = 0
            Dim importo As Double = 0

            MaxScontoTariffa = aMax_Sconto(0) 'al momento assegna lo sconto per tutte le voci della tariffa - salvo 18.01.2023 

            For x = 0 To UBound(aVal)

                '1) 7-2= 5 giorni restanti
                '2) 7-5= 2 giorni restanti
                '3) 7-7= 0 giorni restanti

                importo = 0
                ggcalcolo = 0


                If aA(x) <= gg Then

                    If restogg = 0 Then
                        ggcalcolo = (aA(x) - aDa(x)) + 1
                        'restogg = ggcalcolo - aA(x)      'verifica i giorni restanti
                        importo = ggcalcolo * aVal(x)
                        valore += importo

                    Else

                        If restogg = aA(x) Then
                            ggcalcolo = (aA(x) - aDa(x) + 1)
                            importo = aVal(x) * ggcalcolo
                            valore += importo
                            restogg -= aA(x)      'verifica i giorni restanti
                        ElseIf restogg > aA(x) Then
                            ggcalcolo = (aA(x) - aDa(x) + 1)
                            importo = aVal(x) * ggcalcolo
                            valore += importo
                            restogg -= aA(x)      'verifica i giorni restanti
                        ElseIf restogg < aA(x) Then
                            ggcalcolo = ((aA(x) - aDa(x)) + 1)
                            importo = aVal(x) * ggcalcolo
                            valore += importo
                            restogg -= aA(x)      'verifica i giorni restanti

                        End If

                    End If

                Else
                    ggcalcolo = (gg - aDa(x)) + 1
                    importo = aVal(x) * ggcalcolo
                    valore += importo
                    restogg = 0
                End If

            Next

            '# Esiste uno sconto da form deve applicare lo sconto della riga salvo 18.01.2023
            If sconto_da_form <> "0" Then
                'se lo sconto da form è maggiore a quello della tariffa
                'applica il massimo sconto previsto per quella tariffa

                'assegna a session il totale senza sconto x questa tariffa 19.01.2023
                If DaARES = True Then
                    HttpContext.Current.Session("valore_preventivo_tariffa") = valore
                End If



                If CDbl(sconto_da_form) > CDbl(MaxScontoTariffa) Then
                    valore = valore - (valore * CDbl(MaxScontoTariffa) / 100)     'NON può assegnare lo sconto da form e assegna il massimo possibile di quella tariffa
                Else
                    valore = valore - (valore * CDbl(sconto_da_form) / 100)     'può assegnare lo sconto da form
                End If

                If DaARES = True Then
                    HttpContext.Current.Session("valore_scontato_tariffa") = valore
                End If


            Else
                If DaARES = True Then
                    HttpContext.Current.Session("valore_preventivo_tariffa") = valore
                End If


            End If


            If DaARES = True Then
                HttpContext.Current.Session("perc_sconto_tariffa") = MaxScontoTariffa
            End If



            ris = valore

        Catch ex As Exception
            WriteLogError("GetNewTariffaTempKm ERROR : " & ex.Message) 'test


        End Try

        Return ris


    End Function

    Public Shared Function GetNewTariffaTempoKmPDF(id_tempo_km_figlia As String, id_gruppo_da_calcolare As String, gg As Integer, Optional mensile As Boolean = False) As Double
        'Salvo 07.12.2022

        Dim ris As Double = 0
        Dim valore As Double = 0

        Dim sqlstr As String = "Select righe_tempo_km.pac, righe_tempo_km.valore,righe_tempo_km.da,righe_tempo_km.a FROM righe_tempo_km " &
                            "With(NOLOCK) INNER JOIN tempo_km With(NOLOCK) On righe_tempo_km.id_tempo_km=tempo_km.id " &
                         "INNER JOIN aliquote_iva With(NOLOCK) On tempo_km.id_aliquota_iva=aliquote_iva.id " &
                         "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND id_gruppo='" & id_gruppo_da_calcolare & "' " &
                         "AND NOT valore IS NULL AND valore<>0 AND valore<>0 AND " & gg & " >= da order by righe_tempo_km.da"

        'la seguente stringa è quella nel caso del rack
        'sqlstr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa FROM righe_tempo_km 
        '        WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
        '         "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
        '        "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_nolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' AND NOT valore IS NULL AND valore<>0"


        Dim aDa() As Integer
        Dim aA() As Integer
        Dim aVal() As Double
        Dim conta As Integer = 0

        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then

                While Rs1.Read

                    If conta = 0 Then
                        ReDim aDa(0)
                        ReDim aA(0)
                        ReDim aVal(0)
                    Else
                        ReDim Preserve aDa(conta)
                        ReDim Preserve aA(conta)
                        ReDim Preserve aVal(conta)
                    End If

                    aDa(conta) = Rs1!da
                    aA(conta) = Rs1!a
                    aVal(conta) = Rs1!valore
                    conta += 1

                End While

            End If
            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            Dbc.Close()
            Dbc = Nothing


            'calcolo valore 
            Dim restogg As Integer = gg
            Dim ggcalcolo As Integer = 0
            Dim importo As Double = 0

            For x = 0 To UBound(aVal)

                '1) 7-2= 5 giorni restanti
                '2) 7-5= 2 giorni restanti
                '3) 7-7= 0 giorni restanti

                importo = 0
                ggcalcolo = 0


                If aA(x) <= gg Then

                    If restogg = 0 Then
                        ggcalcolo = (aA(x) - aDa(x)) + 1
                        'restogg = ggcalcolo - aA(x)      'verifica i giorni restanti
                        importo = ggcalcolo * aVal(x)
                        valore += importo

                    Else

                        If restogg = aA(x) Then
                            ggcalcolo = (aA(x) - aDa(x) + 1)
                            importo = aVal(x) * ggcalcolo
                            valore += importo
                            restogg -= aA(x)      'verifica i giorni restanti
                        ElseIf restogg > aA(x) Then
                            ggcalcolo = (aA(x) - aDa(x) + 1)
                            importo = aVal(x) * ggcalcolo
                            valore += importo
                            restogg -= aA(x)      'verifica i giorni restanti
                        ElseIf restogg < aA(x) Then
                            ggcalcolo = ((aA(x) - aDa(x)) + 1)
                            importo = aVal(x) * ggcalcolo
                            valore += importo
                            restogg -= aA(x)      'verifica i giorni restanti

                        End If

                    End If

                Else
                    ggcalcolo = (gg - aDa(x)) + 1
                    importo = aVal(x) * ggcalcolo
                    valore += importo
                    restogg = 0
                End If

            Next

            ris = valore



        Catch ex As Exception

        End Try

        Return ris


    End Function


    Public Shared Function VerificaNominativoDitta(nominativo As String, id_ditta_fattura As String) As String
        'verifica se presente nominativo in ditte altrimenti lo crea 
        'e restituisce nuovo ID

        Dim ris As String = "0"
        Dim sqlstr As String = ""
        Dim flagDitta As Boolean = False

        Try

            If nominativo.IndexOf("1-") > -1 Or nominativo.IndexOf("2-") > -1 Then
                nominativo = nominativo.Substring(3)
            End If

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            'sqlstr = "select id_ditta, id_cliente from ditte where rag_soc='" & Trim(nominativo) & "'"
            sqlstr = "select id_ditta, id_cliente from ditte where id_cliente='" & id_ditta_fattura & "'"
            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris = Rs1!Id_ditta
                flagDitta = True
            End If
            Rs1.Close()
            Rs1 = Nothing
            Cmd1.Dispose()
            Cmd1 = Nothing
            If flagDitta = False Then 'la crea recuperando i dati dal conducente 

                'recupera i dati da CONDUCENTI e li inserisce in DITTE 11.10.2022 salvo
                'id_ditta_fattura
                flagDitta = False
                Dim insertNewDitta As Integer = InsertNuovaDittaDaGuidatore(id_ditta_fattura, nominativo)
                ris = insertNewDitta.ToString
            End If
            Dbc.Close()
            Dbc = Nothing
        Catch ex As Exception
            ris = "-1"
        End Try

        Return ris



    End Function


    Public Shared Function InsertNuovaDittaDaGuidatore(id_conducente As String, nominativo As String) As String

        Dim ris As String = "0"
        Dim sqlstr As String = ""

        Try

            sqlstr = "insert into DITTE (Rag_soc, id_conducente, id_cliente, indirizzo, cap, citta, id_comune_ares, provincia, nazione, c_fis, tel, fax, email) " &
                "(select nominativo, id_conducente, id_conducente, indirizzo,cap,city,id_comune_ares, PROVINCIA, nazione, codfis, telefono, fax ,EMAIL " &
                "From conducenti Where id_conducente = '" & id_conducente & "')"

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim x As Integer = Cmd.ExecuteNonQuery()
            Cmd.Dispose() '
            Cmd = Nothing

            System.Threading.Thread.Sleep(500)     'attende per la registrazione della nuova ditta


            If x > 0 Then

                'aggiorna codice edp
                sqlstr = "update ditte set [CODICE EDP]='" & id_conducente & "' where id_conducente='" & id_conducente & "'"
                Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                Dim x1 As Integer = Cmd1.ExecuteNonQuery()
                Cmd1.Dispose() '
                Cmd1 = Nothing

                System.Threading.Thread.Sleep(500)     'attende per la registrazione della nuova ditta

                'recupera id_ditta appena creata
                sqlstr = "select max(id_ditta) from DITTE where id_conducente='" & id_conducente & "'" ' order by id_ditta desc"
                Dim Cmda = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                Dim id_ditta As String = Cmda.ExecuteScalar & ""

                ris = id_ditta

                Cmda.Dispose() '
                Cmda = Nothing

            End If


            Dbc.Close()
            Dbc = Nothing


        Catch ex As Exception
            HttpContext.Current.Response.Write("error funzioni_comuni InsertNuovaDittaDaGuidatore : " & ex.Message & "<br/>")
        End Try

        Return ris


    End Function








    Public Shared Function GetGuidatoriContratto(ra As String) As Array

        'verifica se presente firma 25.05.2022
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim ris(3) As String

        Try

            Dim sqlStr As String = "SELECT contratti.id, contratti.num_contratto, contratti.attivo, contratti.id_primo_conducente, contratti.id_secondo_conducente, contratti.id_cliente, CONDUCENTI.Nominativo AS Primo_Guidatore, " &
            "CONDUCENTI_1.Nominativo AS Secondo_Guidatore FROM contratti INNER JOIN CONDUCENTI ON contratti.id_primo_conducente = CONDUCENTI.ID_CONDUCENTE LEFT OUTER JOIN " &
            "CONDUCENTI AS CONDUCENTI_1 ON contratti.id_secondo_conducente = CONDUCENTI_1.ID_CONDUCENTE " &
            "WHERE (contratti.num_contratto = '" & ra & "') AND (contratti.attivo = 1)"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                While Rs1.Read()
                    ris(0) = Rs1!primo_guidatore
                    ris(1) = Rs1!id_primo_conducente
                    If Not IsDBNull(Rs1!secondo_guidatore) Then
                        ris(2) = Rs1!secondo_guidatore
                        ris(3) = Rs1!id_secondo_conducente
                    Else
                        ris(2) = ""
                        ris(3) = ""
                    End If
                End While
            Else
                ris(0) = ""
                ris(1) = ""
                ris(2) = ""
                ris(3) = ""
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




    '04.10.2022 salvo
    Public Shared Function getFatturaByDoc2(id_ra, id_pre, id_multa) As String

        Dim ris As String = ""
        Dim tipo_doc As String = ""
        If Not IsDBNull(id_ra) Then
            ris = id_ra
            tipo_doc = "ra"
        ElseIf Not IsDBNull(id_pre) Then
            ris = id_pre
            tipo_doc = "pre"
        ElseIf Not IsDBNull(id_multa) Then
            ris = id_multa
            tipo_doc = "multa"

        End If

        Dim sqlstr As String = ""


        Try

            If ris <> "" Then


                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                If tipo_doc = "ra" Then

                    sqlstr = "SELECT [id], [num_fattura], data_fattura  FROM [Autonoleggio_SRC].[dbo].[Fatture_nolo]  where num_contratto_rif='" & id_ra & "'"

                    Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                    Dim Rs As Data.SqlClient.SqlDataReader
                    Rs = Cmd.ExecuteReader()

                    If Rs.HasRows() Then
                        Rs.Read()
                        ris = Rs!id & "/" & id_ra & "/ra"
                    Else
                        ris = "-"
                    End If


                    Rs.Close()
                    Cmd.Dispose()
                    Rs = Nothing
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc = Nothing



                ElseIf tipo_doc = "pre" Then

                    sqlstr = "SELECT Fatture_nolo.[id], Fatture_nolo.num_fattura, Fatture_nolo.data_fattura FROM Fatture_nolo INNER JOIN " &
                         "contratti ON Fatture_nolo.num_contratto_rif = contratti.num_contratto WHERE (contratti.num_prenotazione = '" & id_pre & "')	"

                    Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                    Dim Rs As Data.SqlClient.SqlDataReader
                    Rs = Cmd.ExecuteReader()

                    If Rs.HasRows() Then
                        Rs.Read()
                        ris = Rs!id & "/" & id_pre & "/pre"
                    Else
                        ris = "-"
                    End If

                    Rs.Close()
                    Cmd.Dispose()
                    Rs = Nothing
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc = Nothing

                ElseIf tipo_doc = "multa" Then

                    sqlstr = "select [id],codice_fattura, anno_fattura from fatture where id_riferimento='" & id_multa & "'"


                    Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                    Dim Rs As Data.SqlClient.SqlDataReader
                    Rs = Cmd.ExecuteReader()

                    If Rs.HasRows() Then
                        Rs.Read()
                        ris = Rs!id & "/" & Rs!anno_fattura & "/multa"
                    Else
                        ris = "-"
                    End If


                    Rs.Close()
                    Cmd.Dispose()
                    Rs = Nothing
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc = Nothing


                End If


            End If



        Catch ex As Exception

        End Try

        Return ris



    End Function

    Public Shared Function getFatturaByDoc3(id_ra, id_pre, id_multa) As String

        Dim ris As String = ""
        Dim tipo_doc As String = ""
        If Not IsDBNull(id_ra) Then
            ris = id_ra
            tipo_doc = "ra"
        ElseIf Not IsDBNull(id_pre) Then
            ris = id_pre
            tipo_doc = "pre"
        ElseIf Not IsDBNull(id_multa) Then
            ris = id_multa
            tipo_doc = "multa"

        End If

        Dim sqlstr As String = ""


        Try

            If ris <> "" Then


                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                If tipo_doc = "ra" Then

                    sqlstr = "SELECT [id], [num_fattura], data_fattura, intestazione  FROM [Autonoleggio_SRC].[dbo].[Fatture_nolo]  where num_contratto_rif='" & id_ra & "'"

                    Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                    Dim Rs As Data.SqlClient.SqlDataReader
                    Rs = Cmd.ExecuteReader()

                    If Rs.HasRows() Then
                        Rs.Read()
                        ris = Rs!intestazione
                    Else
                        ris = "-"
                    End If


                    Rs.Close()
                    Cmd.Dispose()
                    Rs = Nothing
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc = Nothing



                ElseIf tipo_doc = "pre" Then

                    sqlstr = "SELECT Fatture_nolo.[id], Fatture_nolo.num_fattura, Fatture_nolo.data_fattura,  Fatture_nolo.intestazione FROM Fatture_nolo INNER JOIN " &
                         "contratti ON Fatture_nolo.num_contratto_rif = contratti.num_contratto WHERE (contratti.num_prenotazione = '" & id_pre & "')	"

                    Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                    Dim Rs As Data.SqlClient.SqlDataReader
                    Rs = Cmd.ExecuteReader()

                    If Rs.HasRows() Then
                        Rs.Read()
                        ris = Rs!intestazione
                    Else
                        ris = "-"
                    End If

                    Rs.Close()
                    Cmd.Dispose()
                    Rs = Nothing
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc = Nothing

                ElseIf tipo_doc = "multa" Then

                    sqlstr = "select [id],codice_fattura, anno_fattura,  Fatture.intestazione  from fatture where id_riferimento='" & id_multa & "'"


                    Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                    Dim Rs As Data.SqlClient.SqlDataReader
                    Rs = Cmd.ExecuteReader()

                    If Rs.HasRows() Then
                        Rs.Read()
                        ris = Rs!intestazione
                    Else
                        ris = "-"
                    End If


                    Rs.Close()
                    Cmd.Dispose()
                    Rs = Nothing
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc = Nothing


                End If


            End If



        Catch ex As Exception

        End Try

        Return ris



    End Function

    '@end salvo


    Public Shared Function getFatturaByDoc(id_ra, id_pre, id_multa) As String

        Dim ris As String = ""
        Dim tipo_doc As String = ""
        If Not IsDBNull(id_ra) Then
            ris = id_ra
            tipo_doc = "ra"
        ElseIf Not IsDBNull(id_pre) Then
            ris = id_pre
            tipo_doc = "pre"
        ElseIf Not IsDBNull(id_multa) Then
            ris = id_multa
            tipo_doc = "multa"

        End If

        Dim sqlstr As String = ""


        Try

            If ris <> "" Then


                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                If tipo_doc = "ra" Then

                    sqlstr = "SELECT [num_fattura], data_fattura  FROM [Autonoleggio_SRC].[dbo].[Fatture_nolo]  where num_contratto_rif='" & id_ra & "'"

                    Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                    Dim Rs As Data.SqlClient.SqlDataReader
                    Rs = Cmd.ExecuteReader()

                    If Rs.HasRows() Then
                        Rs.Read()
                        ris = Rs!num_fattura & "/" & Year(Rs!data_fattura)
                    Else
                        ris = "-"
                    End If


                    Rs.Close()
                    Cmd.Dispose()
                    Rs = Nothing
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc = Nothing



                ElseIf tipo_doc = "pre" Then

                    sqlstr = "SELECT Fatture_nolo.num_fattura, Fatture_nolo.data_fattura FROM Fatture_nolo INNER JOIN " &
                         "contratti ON Fatture_nolo.num_contratto_rif = contratti.num_contratto WHERE (contratti.num_prenotazione = '" & id_pre & "')	"

                    Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                    Dim Rs As Data.SqlClient.SqlDataReader
                    Rs = Cmd.ExecuteReader()

                    If Rs.HasRows() Then
                        Rs.Read()
                        ris = Rs!num_fattura & "/" & Year(Rs!data_fattura)
                    Else
                        ris = "-"
                    End If


                    Rs.Close()
                    Cmd.Dispose()
                    Rs = Nothing
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc = Nothing



                ElseIf tipo_doc = "multa" Then

                    sqlstr = "select codice_fattura, anno_fattura from fatture where id_riferimento='" & id_multa & "'"


                    Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                    Dim Rs As Data.SqlClient.SqlDataReader
                    Rs = Cmd.ExecuteReader()

                    If Rs.HasRows() Then
                        Rs.Read()
                        ris = Rs!codice_fattura & "/" & Rs!anno_fattura
                    Else
                        ris = "-"
                    End If


                    Rs.Close()
                    Cmd.Dispose()
                    Rs = Nothing
                    Cmd = Nothing
                    Dbc.Close()
                    Dbc = Nothing






                End If







            End If



        Catch ex As Exception

        End Try

        Return ris



    End Function



    '04.10.2022
    Public Shared Function getImpostaBollo(id_riga As String, id_tipo As String) As Boolean
        Dim ris As Boolean = False

        Dim sqlstr As String = "SELECT sum([Imponibile]) as totale FROM [Autonoleggio_SRC].[dbo].[Fatture_riga] " &
            "where id_fattura=" & id_riga & " and AliquotaIVA=0"

        If id_tipo = "nolo" Then
            sqlstr = "Select sum([totale]) As totale FROM [Autonoleggio_SRC].[dbo].[fatture_nolo_righe] " &
            "where id_fattura = " & id_riga & " And aliquota_iva = 0"
        End If

        Try

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows() Then
                Rs.Read()

                If Rs!totale > 77.47 Then       'imposta di bollo da inserire
                    ris = True
                End If
            End If

            Rs.Close()
            Cmd.Dispose()
            Rs = Nothing
            Cmd = Nothing
            Dbc.Close()
            Dbc = Nothing



        Catch ex As Exception

        End Try

        Return ris


    End Function



    '28.09.2022
    Public Shared Function getValoreCondizioniNew(sqlstr As String, ggNolo As Integer, id_elemento As String, costo As Double, pac As String) As String

        Dim ris As Double = costo


        Dim importo As Double = 0
        Dim sPac As String

        If pac = "True" Then
            sPac = "1"
        Else
            sPac = "0"
        End If

        Dim ggNoloScalare As Integer = 0


        Try

            sqlstr = sqlstr.Replace("IDEXXXX", id_elemento)
            sqlstr = sqlstr.Replace("PACXXXX", sPac)


            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows() Then
                While Rs.Read

                    'i giorni sono 8 
                    'da 1 a 7 prende il primo valore
                    'da 8 prende il secondo e lo somma
                    If ggNolo > Rs!applicabilita_a Then
                        If Rs!applicabilita_a > 0 Then
                            importo = CDbl(Rs("costo")) * (ggNolo - Rs!applicabilita_a)
                            ggNoloScalare = ggNolo - Rs!applicabilita_a         'giorni che rimangono per il calcolo del secondo record

                        Else
                            importo = CDbl(Rs("costo")) * ggNolo    'moltiplica x il massimo del primo record
                            'applicabilità =0 deve uscire da While ?????
                        End If
                    Else

                        If ggNoloScalare = 0 Then
                            importo = CDbl(Rs("costo")) * ggNolo     'in questo caso è dentro il primo range
                            Exit While
                        Else
                            importo += CDbl(Rs("costo")) * ggNoloScalare      'in questo caso moltiplica per i giorni restanti
                        End If



                    End If



                End While

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

        ris = importo.ToString

        Return ris



    End Function



    '27.09.2022
    Public Shared Function getValoreTariffeNew(id_stazione As String, tipo_cliente As String, pickup As String, dropoff As String, id_gruppo As String, id_elemento As String, nome_tariffa As String) As Double

        Dim ris As Double = 0
        Dim sqlstr As String = ""

        Dim data_pickup As String = funzioni_comuni.GetDataSql(pickup, 0)
        Dim data_dropoff As String = funzioni_comuni.GetDataSql(dropoff, 0)

        'verifica quali tariffe utilizzare durante il periodo pickup/dropoff
        'e se il periodo rientra in unica tariffa
        Dim id_tariffa() As String
        Dim cont_Tariffe As Integer = 0

        Dim sqlTariffePeriodo As String = ""


        Try


            'verifica quante tariffe nel periodo selezionato
            sqlTariffePeriodo = "Select tariffe_righe.id, ISNULL((Select nome_tariffa FROM tariffe_X_fonti "
            sqlTariffePeriodo += "WHERE tariffe_x_fonti.id_tariffa = tariffe.id And id_tipologia_cliente ='" & tipo_cliente & "'),tariffe.codice) "
            sqlTariffePeriodo += "As codice FROM tariffe WITH(NOLOCK) INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa "
            sqlTariffePeriodo += "WHERE tariffe.attiva ='1' AND convert(datetime,'" & data_pickup & "',102) BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a "
            sqlTariffePeriodo += "And GetDate() BETWEEN tariffe_righe.vendibilita_da And tariffe_righe.vendibilita_a "
            sqlTariffePeriodo += "And convert(datetime,'" & data_dropoff & "',102) <= ISNULL(max_data_rientro, '3000-12-12 23:59:59') "
            sqlTariffePeriodo += "And ( (((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) "
            sqlTariffePeriodo += "WHERE id_stazione ='" & id_stazione & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) "
            sqlTariffePeriodo += "And (tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) "
            sqlTariffePeriodo += "WHERE id_stazione ='" & id_stazione & "' AND tipo='DROP' AND id_tariffa=tariffe.id)))  "
            sqlTariffePeriodo += "Or ((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) "
            sqlTariffePeriodo += "WHERE id_stazione ='" & id_stazione & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) "
            sqlTariffePeriodo += "And (tariffe.id Not IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) "
            sqlTariffePeriodo += "WHERE tipo ='DROP' AND id_tariffa=tariffe.id))) "
            sqlTariffePeriodo += "Or ((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) "
            sqlTariffePeriodo += "WHERE id_stazione ='" & id_stazione & "' AND tipo='DROP' "
            sqlTariffePeriodo += "And id_tariffa = tariffe.id)) And (tariffe.id Not IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) "
            sqlTariffePeriodo += "WHERE tipo ='PICK' "
            sqlTariffePeriodo += "And id_tariffa = tariffe.id))) ) And (tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) "
            sqlTariffePeriodo += "WHERE id_tipologia_cliente ='" & tipo_cliente & "' "
            sqlTariffePeriodo += "And id_tariffa = tariffe.id And (valido_da Is NULL Or getDate() >= valido_da))Or tariffe.id Not IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) "
            sqlTariffePeriodo += "WHERE id_tariffa = tariffe.id)) Or (tariffe.id Not IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) "
            sqlTariffePeriodo += "WHERE tipo ='PICK' AND id_tariffa=tariffe.id) "
            sqlTariffePeriodo += "And tariffe.id Not IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) "
            sqlTariffePeriodo += "WHERE tipo ='DROP' AND id_tariffa=tariffe.id) AND tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) "
            sqlTariffePeriodo += "WHERE id_tipologia_cliente='" & tipo_cliente & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da)) )) "
            sqlTariffePeriodo += "and codice like '" & Replace(nome_tariffa, "'", "''") & "'"
            sqlTariffePeriodo += "ORDER BY codice"

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmdt As New Data.SqlClient.SqlCommand(sqlTariffePeriodo, Dbc)
            Dim Rst As Data.SqlClient.SqlDataReader
            Rst = Cmdt.ExecuteReader()

            If Rst.HasRows Then
                Rst.Read()
                ReDim id_tariffa(0)
                id_tariffa(0) = Rst!id          'id della tariffa in relazione al periodo
            End If
            Rst.Close()
            Cmdt.Dispose()
            Dbc.Close()
            Dbc = Nothing
            Rst = Nothing
            Return ris
            Exit Function



            sqlstr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, "
            sqlstr += "tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) "
            sqlstr += "ON righe_tempo_km.id_tempo_km=tempo_km.id INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id "
            sqlstr += "WHERE id_tempo_km='78' AND 16 BETWEEN da AND a AND id_gruppo='25' AND NOT valore IS NULL AND valore<>0"
            Dim id_condizione_figlia As String = funzioni_comuni.getIdCondizione(id_tariffa(cont_Tariffe))
            Dim id_condizione_madre As String = funzioni_comuni.getIdCondizioneMadre(id_tariffa(cont_Tariffe))
            Dim id_tempo_km_figlia As String = funzioni_comuni.getIdTempoKm(id_tariffa(cont_Tariffe))



            'verifica i giorni il range dei giorni e crea Array

            sqlstr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da, righe_tempo_km.a "
            sqlstr += "FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id INNER JOIN aliquote_iva "
            sqlstr += "WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id "
            sqlstr += "WHERE id_tempo_km='" & id_elemento & "' AND da>0 AND id_gruppo='" & id_gruppo & "' AND NOT valore IS NULL AND valore<>0 "
            sqlstr += "order by da"

            sqlstr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da, righe_tempo_km.a "
            sqlstr += "FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id INNER JOIN aliquote_iva "
            sqlstr += "WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id "
            sqlstr += "WHERE id_tempo_km='" & id_elemento & "' AND da>0 AND id_gruppo='" & id_gruppo & "' AND NOT valore IS NULL AND valore<>0 "
            sqlstr += "order by da"



            Dim rangeGG_da() As String
            Dim rangeGG_a() As String
            Dim valore_rangeGG() As String
            Dim cont_rangeGG As Integer = 0

            'verifica le tariffe dei giorni di quei periodi in relazione alla tariffa e al gruppo





            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                While Rs1.Read
                    If cont_rangeGG = 0 Then
                        ReDim rangeGG_da(0)
                        ReDim rangeGG_a(0)
                        ReDim valore_rangeGG(0)
                    Else
                        ReDim Preserve rangeGG_da(cont_rangeGG)
                        ReDim Preserve rangeGG_a(cont_rangeGG)
                        ReDim Preserve valore_rangeGG(cont_rangeGG)
                    End If
                    rangeGG_da(cont_rangeGG) = Rs1!da
                    rangeGG_a(cont_rangeGG) = Rs1!a
                    valore_rangeGG(cont_rangeGG) = Rs1!valore
                    cont_rangeGG += 1
                End While


            End If

            Rs1.Close()
            Cmd1.Dispose()
            Dbc.Close()
            Rs1 = Nothing
            Cmd1 = Nothing
            Dbc = Nothing

            'calcola gli importi considerando il range dei giorni
            Dim xg As Integer
            Dim x As Integer

            'numero di giorni totali passati nGiorni 
            '16 
            Dim valore As Double = 0
            Dim nGiorni As Integer = 1
            Dim gg As Integer = nGiorni
            For xg = 1 To nGiorni
                For x = 0 To UBound(rangeGG_da)

                    gg = nGiorni - CInt(rangeGG_da(x))
                    valore = valore_rangeGG(x) * CInt(rangeGG_da(x))

                Next

            Next























        Catch ex As Exception
        End Try


        Return ris



    End Function



    '21.09.2022
    Public Shared Function getSiglaAllegato(id As String, tbl As String) As String


        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim ris As String = "NN"

        Try

            Dim sqlStr As String = ""

            If tbl = "veicoli_allegati_tipo" Then
                sqlStr = "SELECT [sigla]  FROM " & tbl & " where id_allegati_tipo='" & id & "';"
            Else
                sqlStr = "SELECT [sigla]  FROM " & tbl & " where id='" & id & "';"
            End If



            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris = Rs1!sigla
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





    Public Shared Function GetCostoAccessorio(id_documento As String, num_calcolo As String, id_elemento As String, id_operatore As String, nco As String) As Double

        'verifica se presente firma 25.05.2022
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim ris As Double = 0

        Try

            Dim sqlStr As String = "Select contratti_costi.id_elemento, contratti_costi.imponibile_scontato as imponibile_commissione, contratti_costi.nome_costo FROM contratti_costi With(NOLOCK) "
            sqlStr += "WHERE  contratti_costi.id_documento = " & id_documento & " And contratti_costi.num_calcolo = " & num_calcolo & " "
            sqlStr += "And (contratti_costi.id_elemento = " & id_elemento & ") And (contratti_costi.selezionato = 1) "


            sqlStr = "Select SUM(contratti_costi.imponibile_scontato) As imponibile_commissione, contratti_costi.id_elemento as id_ele, contratti.giorni as n_giorni "
            sqlStr += "From contratti_costi WITH (NOLOCK) INNER Join condizioni_elementi On contratti_costi.id_elemento = condizioni_elementi.id INNER Join "
            sqlStr += "commissioni_operatore WITH (NOLOCK) ON contratti_costi.id_elemento = commissioni_operatore.id_condizioni_elementi INNER Join contratti On contratti_costi.id_documento = contratti.id "
            sqlStr += "Where (contratti_costi.id_documento = " & id_documento & ") And (contratti_costi.num_calcolo = " & num_calcolo & ") "
            sqlStr += "And (condizioni_elementi.commissione_operatore = 1) And (contratti_costi.selezionato = 1) "
            sqlStr += "And (ISNULL(contratti_costi.omaggiato, 0) = 0) And (commissioni_operatore.id_operatore = " & id_operatore & ") "
            sqlStr += "And (commissioni_operatore.num_contratto = " & nco & ")  And ( contratti_costi.id_elemento = " & id_elemento & ") "
            sqlStr += "Group By contratti_costi.id_elemento, contratti.giorni"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris = Rs1!imponibile_commissione
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



    Public Shared Function GetNomeElemento(id_elemento As String, Optional html As Boolean = False) As String

        'verifica se presente firma 25.05.2022
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim ris As String = ""

        Try

            Dim sqlStr As String = "Select rtrim(descrizione) as descrizione from condizioni_elementi where id = '" & id_elemento & "'"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris = Rs1!descrizione
                If html = True Then ris = ris.Replace("à", "&agrave;")
                If ris.Length > 25 Then
                    Dim a As Integer = ris.IndexOf(" ", 25)
                    If a > -1 Then
                        ris = ris.Substring(0, a) & "<br/>" & ris.Substring(a + 1)
                    End If
                End If



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


    Public Shared Function getPivaEE(nco As String) As String
        'recupera partita iva estera x fattura 10.08.2022

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim ris As String = ""

        Try

            Dim sqlStr As String = "SeLECT [id_ditta],[num_contratto_rif],[provincia],[piva],[codice_fiscale],[conducente],[cod_iva],[iva] "
            sqlStr += "From [Autonoleggio_SRC].[dbo].[Fatture_nolo] "
            sqlStr += "where num_contratto_rif='" & nco & "'"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                If Rs1!provincia = "EE" Then
                    ris = Rs1!id_ditta
                End If
            End If
            Rs1.Close()
            Cmd1.Dispose()

            If ris <> "" Then
                sqlStr = "SELECT [PIva_ESTERA] FROM [Autonoleggio_SRC].[dbo].[DITTE] where Id_Ditta=" & ris & ""
                Dim Cmd2 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dim Rs2 As Data.SqlClient.SqlDataReader
                Rs2 = Cmd2.ExecuteReader()

                If Rs2.HasRows Then
                    Rs2.Read()
                    ris = Rs2!piva_estera
                    If ris = "-" Then ris = ""  '

                Else

                End If

                Rs2.Close()
                Cmd2.Dispose()
                Rs2 = Nothing
                Cmd2 = Nothing

            End If


            Dbc.Close()
            Rs1 = Nothing
            Cmd1 = Nothing
            Dbc = Nothing

        Catch ex As Exception

        End Try

        Return ris


    End Function



    Public Shared Function GetNomeMese(imese As String) As String

        Dim ris As String = ""
        Dim idmese As Integer = CInt(imese)

        If idmese = 1 Then
            ris = "Gennaio"
        ElseIf idmese = 2 Then
            ris = "Febbraio"
        ElseIf idmese = 3 Then
            ris = "Marzo"
        ElseIf idmese = 4 Then
            ris = "Aprile"
        ElseIf idmese = 5 Then
            ris = "Maggio"
        ElseIf idmese = 6 Then
            ris = "Giugno"
        ElseIf idmese = 7 Then
            ris = "Luglio"
        ElseIf idmese = 8 Then
            ris = "Agosto"
        ElseIf idmese = 9 Then
            ris = "Settembre"
        ElseIf idmese = 10 Then
            ris = "Ottobre"
        ElseIf idmese = 11 Then
            ris = "Novembre"
        ElseIf idmese = 12 Then
            ris = "Dicembre"
        End If

        Return ris


    End Function

    Public Shared Function GetDatiRoyalty(sta As String, mese As String, anno As String) As Boolean

        'aggiunta 04.08.2022 salvo
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim ris As Boolean = False

        Try

            Dim sqlStr As String = ""
            sqlStr = "SELECT contratti.data_rientro, Fatture_nolo.num_contratto_rif AS N_Contratto, Fatture_nolo.totale_fattura AS Fatturato_Lordo, Fatture_nolo.iva AS IVA "
            sqlStr += "From Fatture_nolo INNER JOIN contratti ON Fatture_nolo.num_contratto_rif = contratti.num_contratto INNER JOIN "
            sqlStr += "contratti_costi ON contratti.id = contratti_costi.id_documento INNER JOIN condizioni_elementi ON contratti_costi.id_elemento = condizioni_elementi.id "
            sqlStr += "WHERE (contratti.status = 6) AND (contratti_costi.selezionato = 1) AND (contratti_costi.id_elemento = 98) "
            sqlStr += "And contratti.id_stazione_uscita = " & sta & " AND MONTH(contratti.data_rientro)=" & mese & " and year(contratti.data_rientro)=" & anno & " "
            sqlStr += "order by contratti.data_rientro"


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

    Public Shared Function GetNumContrattoFromID(idc As String) As String

        'verifica se presente firma 25.05.2022
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim ris As String = "0"

        Try

            Dim sqlStr As String = "select num_contratto from contratti where id = '" & idc & "'"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris = Rs1!num_contratto
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
    Public Shared Function GetContrattoFirmato(ByVal numcontratto As String, ByVal iddocumento As String, Optional status As String = "2") As Boolean
        'aggiunto 13.07.2022 salvo
        'se risulta da db false ma file presente allora aggiorna db

        Dim ris As Boolean = False


        If numcontratto = "" And iddocumento <> "" Then
            'recupera numcontratto
            numcontratto = GetNumContrattoFromID(iddocumento)
            If numcontratto = "0" Then numcontratto = ""
        End If


        'nessun id e nessun numero
        If numcontratto = "" And iddocumento = "" Then
            Return ris
            Exit Function
        End If




        'verifica se presente firma 03.02.2022 e 09.02.2022
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Try


            Dim sqlStr As String = "SELECT num_contratto,firma_tablet FROM contratti WHERE num_contratto ='" & numcontratto & "' AND firma_tablet=1 AND attivo=1"

            ''se status=8 (chiuso da fatturare) o 4=(chiuso da incassare) 14.07.2022
            If status = "8" Or status = "4" Then
                sqlStr = "SELECT num_contratto,firma_tablet_rientro FROM contratti WHERE num_contratto ='" & numcontratto & "' AND firma_tablet_rientro=1 AND attivo=1"
            End If

            If iddocumento <> "" Then
                sqlStr = "SELECT num_contratto,firma_tablet FROM contratti WHERE [ID] ='" & iddocumento & "' AND firma_tablet=1 AND attivo=1"
                If status = "8" Or status = "4" Then
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


            'ris = true (presente nel DB)
            'ris = false (NON presente nel DB)


            Dim pathfilefirma As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\firme_contratti\pick_up\"
            Dim pathfilefirmaDEF As String = ""

            'se risulta da db false ma il file firma è presente allora aggiorna db 13.07.2022
            If ris = False Then 'NON presente nel DB

                'verifica se presente file firma
                If status = "8" Or status = "4" Then
                    pathfilefirmaDEF = pathfilefirma & numcontratto & "_RB-trasp.png"
                    sqlStr = "UPDATE contratti SET firma_tablet_rientro='1' WHERE num_contratto='" & numcontratto & "'"
                Else
                    pathfilefirmaDEF = pathfilefirma & numcontratto & "-trasp.png"
                    sqlStr = "UPDATE contratti SET firma_tablet='1' WHERE num_contratto='" & numcontratto & "'"
                End If

                'Non presente nel DB ma presente il file firma 15.07.2022 salvo
                'probabile errore in fase di registrazione su DB
                If IO.File.Exists(pathfilefirmaDEF) = True Then  'è presente il file firma

                    'verifica che sia di lunghezza >0
                    'verifica la lunghezza 13.06.2022 salvo
                    Dim fi As FileInfo = New FileInfo(pathfilefirmaDEF)
                    Dim fileSizeInBytes As Long = fi.Length

                    If fileSizeInBytes = 0 Then

                        'la lunghezza è zero
                        IO.File.Delete(pathfilefirmaDEF) 'lo elimina perchè a ZERO

                        'se si tratta di rientro posso utilizzare il file di uscita NO perchè nn firmato 15.07.2022
                        If status = "8" Or status = "4" Then
                            'NON COPIARE FIRMA USCITA come Firma Rientro 19.07.2022 salvo
                            'If IO.File.Exists(pathfilefirma & numcontratto & "-trasp.png") Then
                            '    IO.File.Copy(pathfilefirma & numcontratto & "-trasp.png", pathfilefirma & numcontratto & "_RB-trasp.png")
                            '    Dim fi2 As FileInfo = New FileInfo(pathfilefirma & numcontratto & "_RB-trasp.png")
                            '    fileSizeInBytes = fi2.Length
                            'End If
                        End If
                    Else
                    End If

                    'lunghezza ok
                    'se tutto corretto aggiorna il db 13.07.2022 salvo
                    If fileSizeInBytes > 0 Then
                        Dbc.Open()
                        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                        Cmd.ExecuteNonQuery()

                        Cmd.Dispose()
                        Cmd = Nothing
                        Dbc.Close()
                        Dbc = Nothing
                    End If

                End If



            Else 'se  presente nel db

                'RIS da DB = true
                '--> registrato su DB(true) ma file firma non presente 14.07.2022


                'solo nel caso che è presente nel db come firma di rientro ma non c'è il file firma di rientro 19.07.2022
                'e se c'è il file di uscita lo ricopia nella firma di rientro 15.07.2022 / 19.07.2022 salvo
                'confermato il 20.07.2022

                If status = "8" Or status = "4" Then 'se rientro 

                    '23.07.2022 salvo
                    'c'è stata la registrazione sul DB da parte del webservice 
                    'e se non c'è il file firma di rientro (quello ricevuto dalla firma tablet) per probabile errore di trasmissione file
                    'numcontratto_RB.png copia quello di uscita originale e trasp in quello di rientro
                    'NOTA: la conversione avviene al momento della stampa contratto

                    If IO.File.Exists(pathfilefirma & numcontratto & "_RB.png") = False Then    'e NON numcontratto_RB-trasp.png
                        'verifica se c'è file firma di uscita originale trasp e lo ricopia come file firma x il rientro 23.07.2022
                        If IO.File.Exists(pathfilefirma & numcontratto & "-trasp.png") Then
                            'copia quello trasparente di uscita
                            IO.File.Copy(pathfilefirma & numcontratto & "-trasp.png", pathfilefirma & numcontratto & "_RB-trasp.png")
                            Threading.Thread.Sleep(300)
                            'e copia quello ricevuto dal tablet al momento della firma in uscita
                            IO.File.Copy(pathfilefirma & numcontratto & ".png", pathfilefirma & numcontratto & "_RB.png")
                            Threading.Thread.Sleep(300)
                        Else
                            sqlStr = "UPDATE contratti SET firma_tablet_rientro='0' WHERE num_contratto='" & numcontratto & "'"
                            ris = False 'cambia il ris in true perchè ha copiato il file firma di rientro
                        End If

                        'sul DB è presente ma manca il file firma 19.07.2022
                        'aggiorna db senza firma
                        sqlStr = "UPDATE contratti SET firma_tablet_rientro='0' WHERE num_contratto='" & numcontratto & "'"

                        Try
                            Dbc.Open()
                            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                            Cmd.ExecuteNonQuery()

                            Cmd.Dispose()
                            Cmd = Nothing
                            Dbc.Close()
                            Dbc = Nothing

                            ris = False         'firma mancante

                        Catch ex As Exception

                        End Try

                    Else 'se presente il file RB_trasp



                        'If IO.File.Exists(pathfilefirma & numcontratto & "-trasp.png") = False Then
                        'sqlStr = "UPDATE contratti SET firma_tablet='0' WHERE num_contratto='" & numcontratto & "'"
                        'ris = False 'cambia il ris in true perchè ha copiato il file firma di rientro
                        'End If

                    End If 'se presente il file RB_trasp


                Else 'se Aperto

                    If status = "2" Then ' se aperto ed è sviluppo

                        'se sviluppo o localhost e sul db presente 
                        'crea firma da filetest 20.05.2022
                        Dim url As String = HttpContext.Current.Request.UrlReferrer.AbsoluteUri
                        Dim sviluppo As Boolean = False
                        Dim id_tablet As String = ""


                        If url.IndexOf("sviluppo.sicilyrentcar") > -1 Then
                            sviluppo = True
                        End If
                        If url.IndexOf("localhost") > -1 Then
                            sviluppo = True
                        End If

                        'If sviluppo = True Then




                        '    If IO.File.Exists(pathfilefirma & "firmatest-trasp.png") = True Then
                        '        IO.File.Copy(pathfilefirma & "firmatest-trasp.png", pathfilefirma & numcontratto & ".png")
                        '        IO.File.Copy(pathfilefirma & "firmatest-trasp.png", pathfilefirma & numcontratto & "-trasp.png")

                        '        'aggiorna db 

                        '        sqlStr = "UPDATE contratti SET firma_tablet='1' WHERE num_contratto='" & numcontratto & "'"

                        '        Try
                        '            Dbc.Open()
                        '            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                        '            Cmd.ExecuteNonQuery()

                        '            Cmd.Dispose()
                        '            Cmd = Nothing
                        '            Dbc.Close()
                        '            Dbc = Nothing

                        '            ris = False         'firma mancante

                        '        Catch ex As Exception

                        '        End Try



                        '    End If


                        'End If
                        '' se Sviluppo crea file uscita se nel db è true 20.07.2022


                    End If




                End If 'se status=8 o status=4


            End If

        Catch ex As Exception

        End Try

        Return ris




    End Function




    Public Shared Function GetKmDisponibili_new(ByVal targa As String) As Boolean
        'inserita in funzioni_comuni_new il 29.06.2022
        'spostata da funzioni_comuni

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

                    'km restanti dal tot km leasing e i km effettivi attuali senza km imms. in parco
                    km_restanti_leasing = km_compresi_leasing + km_immissione_parco - km_attuali
                    km_disponibili_totali = km_restanti_leasing

                    'questa parte da verificare se necessaria e in che caso

                    If km_disponibili_totali < 0 Then
                        km_disponibili = km_disponibili_totali
                    Else
                        durata_mesi_leasing = Rs!durata_mesi_leasing
                        km_mese = km_compresi_leasing / durata_mesi_leasing
                        km_day = km_mese / 30
                        num_giorni_ad_oggi = DateDiff("d", Rs![data_inserimento], Date.Now)
                        data_immissione_parco = Rs!Data_immissione_Parco

                        km_disponibili_ad_oggi = km_day * num_giorni_ad_oggi

                        'se i km effettivi_attuali senza i km al momento di immi. parco auto
                        'sono minori dei km compresi in leasing allora è vero che ci sono km restanti
                        km_disponibili = km_disponibili_ad_oggi - (km_attuali - km_immissione_parco)  '????
                    End If


                    'calcola da km disponibili : ho cambiato da km disponibili a km_disponibili_totali dal 29.06.2022
                    'da verificare con Francesco 
                    ' If km_disponibili_totali > 0 Then
                    If km_disponibili > 0 Then
                        ris = True
                    Else
                        ris = False
                    End If

                Else
                    'nessun valore nel campo km compresi della scheda veicoli--> leasing a lungo termine 30.06.2022
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


    Public Shared Function UserMsgBox(ByVal F As System.Web.UI.Page, ByVal sMsg As String) As Boolean

        Dim ris As Boolean = False

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

            ris = True


        Catch ex As Exception

        End Try

        'Return EnterpriseServices
        Return ris



    End Function



    Public Shared Function getTestoMailImportante(idstazione As String) As String
        Dim ris As String = ""

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()


        Try

            'Dim sqlStr As String = "select testo_mail from stazioni where id_stazione= '" & idstazione & "'"
            Dim sqlStr As String = "select testo_mail from stazioni where id= '" & idstazione & "'"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris = Rs1("testo_mail")
            End If

            Rs1.Close()
            Cmd1.Dispose()
            Dbc.Close()
            Rs1 = Nothing
            Cmd1 = Nothing
            Dbc = Nothing

        Catch ex As Exception
            'HttpContext.Current.Response.Write("Errore in getFirmaUscita::" & "<br/>" & ex.Message)
        End Try

        Return ris





    End Function

    Public Shared Function UpdateDbFirma(numco As String, pathfirma As String, tipoco As String, status As String, Optional id_tablet As String = "0") As Boolean
        'tipoco = RA (uscita)  - RB (rientro)

        Dim ris As Boolean = False

        Dim pathfilefirma As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\firme_contratti\pick_up\"

        Dim filefirma As String = pathfilefirma & numco & ".png"
        Dim filefirmaRA As String = pathfilefirma & numco & "-trasp.png"
        Dim filefirmaRB As String = pathfilefirma & numco & "_RB-trasp.png"
        Dim campoFirmaTablet As String = "firma_tablet"
        Dim campoIdTablet As String = "id_tablet_firma"
        If status = "8" Then
            campoFirmaTablet = "firma_tablet_rientro"
            campoIdTablet = "id_tablet_firma_rientro"
        End If


        Try
            'aggiorna campo su db
            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim sqlstr As String = "update contratti set " & campoFirmaTablet & "='1', " & campoIdTablet & "='" & id_tablet & "' WHERE num_contratto='" & numco & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
            Dim x As Integer = Cmd.ExecuteNonQuery()

            'If x > 0 Then
            '    'ris = True
            'End If

            Cmd.Dispose() '
            Cmd = Nothing
            Dbc.Close()
            Dbc = Nothing

        Catch ex As Exception

        End Try

        Return ris

    End Function



    Public Shared Function VerificaFileFirma(numco As String, pathfirma As String, status As String) As Boolean

        Dim ris As Boolean = False

        Dim pathfilefirma As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\firme_contratti\pick_up\"

        Dim filefirma As String = pathfilefirma & numco & ".png"
        Dim filefirmaRA As String = pathfilefirma & numco & "-trasp.png"
        Dim filefirmaRB As String = pathfilefirma & numco & "_RB-trasp.png"

        If status = "8" Then
            filefirma = filefirmaRB     'rientro
        Else
            filefirma = filefirmaRA     'uscita
        End If

        Dim campoFirmaTablet As String = "firma_tablet"
        Dim campoIdTablet As String = "id_tablet_firma"

        If status = "8" Then
            campoFirmaTablet = "firma_tablet_rientro"
            campoIdTablet = "id_tablet_firma_rientro"
        End If

        Dim id_Tablet As String = "0"

        Try
            'verifica se presente il file
            If IO.File.Exists(filefirma) = False Then

                If status = "8" Or status = "4" Then
                    'verifica se file firma di uscita presente 
                    'If IO.File.Exists(filefirmaRA) = True Then
                    '    'NON lo copia per il rientro 19.07.2022 salvo
                    '    'IO.File.Copy(filefirmaRA, filefirmaRB)
                    '    'Threading.Thread.Sleep(1000)

                    'End If

                    ris = False         'manca il file firma 19.07.2022

                Else

                End If

            Else 'firma trasp presente

                ris = True ' 13.07.2022 file firma trasp presente

            End If


            'se ris = true aggiorna comunque db
            If (status = "8" Or status = "4") And ris = True Then

                'recupera idTablet
                id_Tablet = GetIdTabletFirma(numco)

                'aggiorna campo su db
                Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim sqlstr As String = "update contratti set " & campoFirmaTablet & "='1', " & campoIdTablet & "='" & id_Tablet & "' WHERE num_contratto='" & numco & "'"

                Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                Dim x As Integer = Cmd.ExecuteNonQuery()

                'If x > 0 Then
                '    'ris = True
                'End If

                Cmd.Dispose() '
                Cmd = Nothing
                Dbc.Close()
                Dbc = Nothing

            End If 'se ris = true aggiorna comunque db


        Catch ex As Exception

        End Try




        Return ris


    End Function


    Public Shared Function getFirmaUscita(numco As String) As Boolean

        'verifica se presente firma da DB rientro 09.06.2022

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim ris As Boolean = False

        Try

            Dim sqlStr As String = "Select firma_tablet from contratti where num_contratto= '" & numco & "' and attivo=1"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris = Rs1("firma_tablet")
            End If


            Rs1.Close()
            Cmd1.Dispose()
            Dbc.Close()
            Rs1 = Nothing
            Cmd1 = Nothing
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("Errore in getFirmaUscita::" & "<br/>" & ex.Message)
        End Try

        Return ris


    End Function
    Public Shared Function getFirmaRientro(numco As String) As Boolean


        'verifica sul db se presente firma rientro 09.06.2022 
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim ris As Boolean = False

        Try

            Dim sqlStr As String = "select firma_tablet_rientro from contratti where num_contratto= '" & numco & "' and attivo=1"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris = Rs1("firma_tablet_rientro")
            End If

            Rs1.Close()
            Cmd1.Dispose()
            Dbc.Close()
            Rs1 = Nothing
            Cmd1 = Nothing
            Dbc = Nothing

        Catch ex As Exception
            HttpContext.Current.Response.Write("Errore in getFirmaRientro::" & "<br/>" & ex.Message)
        End Try

        Return ris


    End Function
    Public Shared Function AllegaCkIn(numcontratto As String, Optional tipo As String = "uscita", Optional CkIn As Boolean = False) As Boolean
        Dim ris As Boolean = False

        Try
            'riempie la session che è stat valorizzata da pulsante firma contratto
            Dim mie_dati As DatiStampaContratto = HttpContext.Current.Session("DatiStampaContrattoPostFirma")
            Dim nazione As String = HttpContext.Current.Session("DatiStampaContrattoPostFirmaLang")

            'genera il contratto

            Dim newDir As String = HttpContext.Current.Server.MapPath("\allegati_pren_cnt\" & numcontratto)
            Dim newFile As String = HttpContext.Current.Server.MapPath("\allegati_pren_cnt\" & numcontratto & "\CI_" & numcontratto & ".pdf")




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
                Dim insertAllegati As Boolean = insertCkINPDFtoAllegati("/allegati_pren_cnt/" & numcontratto & "/", numcontratto, tipo)
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


    Public Shared Function insertCkINPDFtoAllegati(ByVal pathAllegato As String, ByVal contratto_num As String, Optional tipo As String = "uscita") As Boolean
        Dim ris As Boolean = False
        Dim nome_file As String = "CI_" & contratto_num & ".pdf"
        Dim tipoAllegato As String = "12" 'contratto di uscita

        tipoAllegato = "19" 'ckIn 01.06.2022



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

            Dim sqlstr As String = "INSERT INTO contratti_prenotazioni_allegati (nome_file, cartella, num_cnt, id_cnt_pren_allegati_tipo,nome_file_operatore) " &
                                                                     " VALUES ('" & nome_file & "','" & pathAllegato & "','" & contratto_num & "','" & tipoAllegato & "','contratto')"


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
            HttpContext.Current.Response.Write("Errore nell'inserimento dell'allegato.:" & "<br/>" & ex.Message)
        End Try

        Return ris


    End Function




    Public Shared Function GetDanniCheckIN(numco As String) As Boolean

        'verifica se danni al rientro  25.05.2022
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim ris As Boolean = False


        Try


            Dim sqlStr As String = "Select [id],[attivo],[sospeso_rds],[id_veicolo],[id_tipo_documento_apertura],[id_documento_apertura] ,[num_crv]" &
            ",[data] From [Autonoleggio_SRC].[dbo].[veicoli_evento_apertura_danno]  Where attivo = 1 And id_documento_apertura ='" & numco & "'"

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
    Public Shared Function GetNumContratto(idc As String) As String
        'verifica se presente firma 25.05.2022
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim ris As String = "0"

        Try


            Dim sqlStr As String = "select num_contratto from contratti where id = '" & idc & "' and attivo=1"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()
                ris = Rs1!num_contratto
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
    Public Shared Function GetIDMultaFromProt(numprot As String) As String



        Dim ris As String = ""

        Dim anno As String = Date.Now.Year.ToString 'anno corrente


        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()


        Dim sqlStr As String = "select id from multe where prot= '" & numprot & "' AND anno='" & anno & "';"

        Try




            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()

            If Rs1.HasRows Then
                Rs1.Read()

                ris = Rs1!id

            End If

            Rs1.Close()
            Cmd1.Dispose()
            Rs1 = Nothing
            Cmd1 = Nothing


            Dbc.Close()
            Dbc = Nothing

        Catch ex As Exception

        End Try

        Return ris




    End Function


    Public Shared Function GetIdTabletFirma(ByVal numcontratto As String) As String

        Dim ris As String = "0"

        'verifica se presente firma 03.02.2022 e 09.02.2022
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Try

            Dim sqlStr As String = "SELECT id_tablet_firma FROM contratti WHERE num_contratto ='" & numcontratto & "' AND attivo=1"

            Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Rs1 As Data.SqlClient.SqlDataReader
            Rs1 = Cmd1.ExecuteReader()
            If Rs1.HasRows Then
                Rs1.Read()
                ris = Rs1!id_tablet_firma
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

End Class
