Imports System.Net
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data
Imports System.Threading
Imports System.Web.Script.Serialization
Imports System.Web.Services

Partial Class sql1
    Inherits System.Web.UI.Page

    Dim stazione_inizio_noleggio As String = 3
    Dim data_inizio_noleggio As String = "28/06/2021"
    Dim ora_inizio_noleggio As String = "11:00"
    Dim stazione_fine_noleggio As String = "3"
    Dim data_fine_noleggio As String = "30/06/2021"
    Dim ora_fine_noleggio As String = "11:00"
    Dim eta As String = "31"
    Dim id_tip_cliente As Integer = 0
    Dim lingua As String = "it"
    Dim cod_promo As String = ""

    Dim result As String = "test" '
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lbl_lastid.Text = Date.Now.ToString


    End Sub




    Protected Sub btn_go_Click(sender As Object, e As EventArgs)


        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dim sql As String
        Dim Cmd As New Data.SqlClient.SqlCommand()

        Try
            'Label1.Text = Date.Now.ToString

            Dim sdate As String = Date.Now.Hour & "h : " & Date.Now.Minute & "m : " & Date.Now.Second & "s : " & Date.Now.Millisecond & "ms"

            Label1.Text = "Start: " & sdate.ToString



            Session("chiamata_sito_web") = "1"

            If Request.QueryString("id") = "1" Then
                result = getElencoVeicoli(stazione_inizio_noleggio, data_inizio_noleggio, ora_inizio_noleggio, stazione_fine_noleggio, data_fine_noleggio, ora_fine_noleggio, eta, id_tip_cliente, lingua, cod_promo)
            End If

            TextBox1.Text = result

            lbl_lastid.Text = "operazione effettuata:" & "<br/>" & sql & "<br/>"

            'Label2.Text = Date.Now.ToString
            Dim edate As String = Date.Now.Hour & "h : " & Date.Now.Minute & "m : " & Date.Now.Second & "s : " & Date.Now.Millisecond & "ms"
            Label2.Text = "End : " & edate.ToString


            Label3.Text = ""











        Catch ex As Exception
            ' HttpContext.Current.Response.Write("UPDATE SQL: " & ex.Message & "<br/>" & sql & "<br/>")
            lbl_lastid.Text = "UPDATE SQL: " & ex.Message & "<br/>" & sql & "<br/>"
        End Try

        'Label1.Text = Date.Now.ToString


        Exit Sub
    End Sub


    ' Per consentire la chiamata di questo servizio Web dallo script utilizzando ASP.NET AJAX, rimuovere il commento dalla riga seguente.
    Function controllo_VAL(ByVal stazione As String) As Boolean


        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT val_verso_altre_stazioni  FROM Stazioni WHERE id = '" & stazione & "'", Dbc)


        controllo_VAL = Cmd.ExecuteScalar



        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc = Nothing



    End Function

    Function controllo_VAL_OFF(ByVal stazione_off As String) As Boolean


            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT val_da_altre_stazioni  FROM Stazioni WHERE id = '" & stazione_off & "'", Dbc)


            controllo_VAL_OFF = Cmd.ExecuteScalar



            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc = Nothing



        End Function

        Private Function control_gruppi(ByVal id_stop_sell As String, ByVal stazione As String, ByVal data_inizio_completa As String) As String

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            'Tony Modifica in data 12-10-2015
            'Dim Cmd As New Data.SqlClient.SqlCommand("SELECT stop_sell_gruppi.id_gruppo,stop_sell_stazioni.id_stazione FROM stop_sell INNER JOIN stop_sell_gruppi ON stop_sell.id = stop_sell_gruppi.id_stop_sell LEFT JOIN stop_sell_stazioni ON stop_sell.id = stop_sell_stazioni.id_stop_sell WHERE stop_sell.id =" & id_stop_sell & "", Dbc)
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT stop_sell.id, stop_sell_gruppi.id_gruppo,stop_sell_stazioni.id_stazione FROM stop_sell INNER JOIN stop_sell_gruppi ON stop_sell.id = stop_sell_gruppi.id_stop_sell LEFT JOIN stop_sell_stazioni ON stop_sell.id = stop_sell_stazioni.id_stop_sell WHERE '" & data_inizio_completa & "' BETWEEN stop_sell.da_data AND stop_sell.a_data", Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            Do While Rs.Read()
                If Rs.HasRows Then

                    If Rs("ID_gruppo") & "" <> "" And Rs("id_stazione") & "" <> "" Then

                        If Rs("id_stazione") = stazione Then



                            control_gruppi = control_gruppi & " AND Gruppi.ID_gruppo not in (SELECT id_gruppo FROM stop_sell_gruppi INNER JOIN stop_sell_stazioni ON stop_sell_gruppi.id_stop_sell = stop_sell_stazioni.id_stop_sell WHERE id_stazione = '" & Rs("id_stazione") & "' AND stop_sell_gruppi.id_stop_sell='" & id_stop_sell & "') "

                        End If
                    Else
                        If Rs("ID_gruppo") & "" <> "" Then

                            control_gruppi = control_gruppi & " AND Gruppi.ID_gruppo <> '" & Rs("ID_gruppo") & "'  "
                        End If
                    End If

                End If

            Loop
            Rs.Close()

            Cmd.Dispose()
            Dbc.Dispose()
            Dbc.Close()
            Rs = Nothing
            Cmd = Nothing
            Dbc = Nothing


        End Function

        'Inserita 04/06/2015 -- modificata 20/07/2015
        Function get_tariffa_x_righe(ByVal data_uscita As String, ByVal data_rientro As String, ByVal giorni_nolo As String, ByVal stazione As String, ByVal stazioneOff As String, ByVal IdTipCliente As Integer, ByVal cod_promo As String) As String

            Dim Risultato, RisultatoParticolare As String

            Dim pickup_da_temp As Date = data_uscita
            Dim pickup_a_temp As Date = data_rientro

            Dim pickup_da As String = pickup_da_temp.Year & "-" & pickup_da_temp.Day & "-" & pickup_da_temp.Month & " 00:00:00"
            Dim pickup_a As String = pickup_a_temp.Year & "-" & pickup_a_temp.Day & "-" & pickup_a_temp.Month & " 00:00:00"

            Try
                If cod_promo & "" <> "" Then
                    'Dim Sql = "SELECT top(1) tariffe_righe.id FROM tariffe WITH(NOLOCK) INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa WHERE  tariffe.attiva='1' AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a  AND  tariffe.codicepromozionale='" & cod_promo & "' ORDER BY codice"
                    Dim Sql = "SELECT top(1) tariffe_righe.id FROM tariffe WITH(NOLOCK) INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa WHERE  tariffe.attiva='1' AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a  AND '" & pickup_da_temp & "' BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a AND  tariffe.codicepromozionale='" & cod_promo & "' ORDER BY codice"

                    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()
                    ''Response.Write("SQL: " & sql)            

                    Dim Cmd As New Data.SqlClient.SqlCommand(Sql, Dbc)

                    'Risultato = Cmd.ExecuteScalar

                    Dim Rs As Data.SqlClient.SqlDataReader
                    Rs = Cmd.ExecuteReader()

                    Rs.Read()
                    If Rs.HasRows Then
                        get_tariffa_x_righe = Rs(0)
                    Else
                        get_tariffa_x_righe = "0"
                    End If
                Else
                    Dim condizione_id_prev As String = ""
                    'If id_prev <> 0 Then
                    '    condizione_id_prev = " AND tariffe.id=" & id_prev
                    'End If

                    'QUERY: MODIFICARE LA QUERY ANCHE IN PRENOTAZIONI - CONTRATTI (DENTRO LA FUNZIONE tariffa_vendibile)

                    'PARTE 1: SELEZIONO LE TARIFFE GENERICHE (PER CUI TUTTE LE FONTI SONO POSSIBILI - TUTTE LE STAZIONI SONO POSSIBILI)
                    'COME ID SELEZIONO DIRETTAMENTE LA RIGA DI tariffe_righe DA CUI POI POSSO SUBITO OTTENERE LA CONDIZIONE E IL TEMPO-KM
                    'DA UTILIZZARE

                    'Dim sql As String = " select id from tariffe_righe where id_tariffa = 8 and vendibilita_da <= GETDATE() and vendibilita_a >= GETDATE() and pickup_da <= '" & pickup_da & "' and pickup_a >= '" & pickup_da & "' AND ISNULL(min_giorni_nolo,0) <= '" & giorni_nolo & "' AND ISNULL(max_giorni_nolo,99) >= '" & giorni_nolo & "'"

                    'Dim Sql = "(SELECT tariffe_righe.id,tariffe.codice FROM tariffe WITH(NOLOCK) " & _
                    '"INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " & _
                    '"WHERE tariffe.attiva='1' " & condizione_id_prev & " AND '" & pickup_da_temp & "' BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " & _
                    '"AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a " & _
                    '"AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_X_stazioni WITH(NOLOCK) WHERE id_tariffa=tariffe.id) " & _
                    '"AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_X_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id) " & _
                    '"AND '" & pickup_a_temp & "' <= ISNULL(max_data_rientro, '3000-12-12 23:59:59')) ORDER BY codice"

                    Dim Sql = "(SELECT top(1) tariffe_righe.id FROM tariffe WITH(NOLOCK) " &
               "INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
               "WHERE (tariffe.is_web = '1' AND tariffe.is_web_prepagato = '0') AND tariffe.attiva='1' " & condizione_id_prev & " AND '" & pickup_da_temp & "' BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " &
               "AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a " &
               "AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_X_stazioni WITH(NOLOCK) WHERE id_tariffa=tariffe.id) " &
               "AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_X_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id) " &
               "AND '" & pickup_a_temp & "' <= ISNULL(max_data_rientro, '3000-12-12 23:59:59')) ORDER BY codice"

                    Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc.Open()
                    ''Response.Write("SQL: " & sql)            


                    Dim Cmd As New Data.SqlClient.SqlCommand(Sql, Dbc)

                    Risultato = Cmd.ExecuteScalar

                    'SE SPECIFICATO, NEL CASO DI TARIFFA PARTICOLARE, SELEZIONO IL NOME PER FONTE
                    Dim condizione_nome_tariffa_fonte As String = "NULL"
                    If IdTipCliente > 0 Then
                        condizione_nome_tariffa_fonte = "(SELECT nome_tariffa FROM tariffe_X_fonti WHERE tariffe_x_fonti.id_tariffa=tariffe.id AND id_tipologia_cliente='" & IdTipCliente & "')"
                    End If
                    Dim SqlTariffeParticolari = "SELECT top(1) tariffe_righe.id FROM tariffe WITH(NOLOCK) " &
                "INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
                "WHERE (tariffe.is_web = '1' AND tariffe.is_web_prepagato = '0') AND tariffe.attiva='1' " & condizione_id_prev & " AND '" & pickup_da_temp & "' BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " &
                "AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a " &
                "AND '" & pickup_a_temp & "' <= ISNULL(max_data_rientro, '3000-12-12 23:59:59') " &
                "AND ( (" &
                "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & stazione & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND " &
                "(tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & stazioneOff & "' AND tipo='DROP' AND id_tariffa=tariffe.id))) " &
                " OR " &
                "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & stazione & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND " &
                "(tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='DROP' AND id_tariffa=tariffe.id))) " &
                " OR " &
                "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & stazioneOff & "' AND tipo='DROP' AND id_tariffa=tariffe.id)) AND " &
                "(tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='PICK' AND id_tariffa=tariffe.id))) ) " 'MODIFICARE LA QUERY ANCHE IN RIBALTAMENTO.ASPX

                    If IdTipCliente > 0 Then
                        'SE E' STATA SELEZIONATA UNA FONTE CONTROLLO ANCHE TRA I REQUISITI PER FONTE DELLE TARIFFE
                        'UNENDO IN OR (L'ULTIMO) LA POSSIBILITA CHE LA TARIFFA NON SPECIFICHI ALCUNA STAZIONE MA SPECIFICHI UNA O PIU' FONTI
                        SqlTariffeParticolari = SqlTariffeParticolari & "" &
                    "AND (tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tipologia_cliente='" & IdTipCliente & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da))" &
                    "OR tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id)) " &
                    "OR (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='PICK' AND id_tariffa=tariffe.id) AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='DROP' AND id_tariffa=tariffe.id) AND tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tipologia_cliente='" & IdTipCliente & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da)) )) ORDER BY codice"
                    Else
                        'SE NON E' STATA SELEZIONATA UNA FONTE PRELEVO SOLAMENTE LE TARIFFE CHE NON HANNO ALCUN PREREQUISITO DI FONTE
                        SqlTariffeParticolari = SqlTariffeParticolari & "" &
                    "AND (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id))) ORDER BY codice"
                    End If

                    Dim DbcParticolare As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    DbcParticolare.Open()

                    HttpContext.Current.Trace.Write("SQL: " & Sql)
                    'HttpContext.Current.'Response.Write("SQL: " & Sql & "<br/>")



                    Dim CmdParticolare As New Data.SqlClient.SqlCommand(SqlTariffeParticolari, DbcParticolare)

                    RisultatoParticolare = CmdParticolare.ExecuteScalar

                    CmdParticolare.Dispose()
                    CmdParticolare = Nothing
                    DbcParticolare.Close()
                    DbcParticolare.Dispose()
                    DbcParticolare = Nothing

                    If RisultatoParticolare <> "" Then
                        get_tariffa_x_righe = RisultatoParticolare
                    Else
                        get_tariffa_x_righe = Risultato
                    End If
                    ''Response.Write("Get Tariffa: " & get_tariffa_x_righe)
                    'Response.End()
                End If

            Catch ex As Exception
                'Response.Write("Get Tariffa get_tariffa_x_righe error : " & ex.Message & "<br/>")
            End Try






        End Function

        'Inserita 04/06/2015 -- modificata 21/07/2015
        Function get_tariffa_x_righe_prepagato(ByVal data_uscita As String, ByVal data_rientro As String, ByVal giorni_nolo As String, ByVal stazione As String, ByVal stazioneOff As String, ByVal IdTipCliente As Integer, ByVal cod_promo As String) As String
            Dim Risultato, RisultatoParticolare As String

            Dim pickup_da_temp As Date = data_uscita
            Dim pickup_a_temp As Date = data_rientro

            Dim pickup_da As String = pickup_da_temp.Year & "-" & pickup_da_temp.Day & "-" & pickup_da_temp.Month & " 00:00:00"
            Dim pickup_a As String = pickup_a_temp.Year & "-" & pickup_a_temp.Day & "-" & pickup_a_temp.Month & " 00:00:00"
            If cod_promo & "" <> "" Then
                'Dim Sql = "SELECT top(1) tariffe_righe.id FROM tariffe WITH(NOLOCK) INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa WHERE  tariffe.attiva='1' AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a  AND  tariffe.codicepromozionale='" & cod_promo & "' ORDER BY codice"
                Dim Sql = "SELECT top(1) tariffe_righe.id FROM tariffe WITH(NOLOCK) INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa WHERE  tariffe.attiva='1' AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a  AND '" & pickup_da_temp & "' BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a AND  tariffe.codicepromozionale='" & cod_promo & "' ORDER BY codice"


                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                ''Response.Write("SQL: " & sql)


                Dim Cmd As New Data.SqlClient.SqlCommand(Sql, Dbc)

                'Risultato = Cmd.ExecuteScalar

                Dim Rs As Data.SqlClient.SqlDataReader
                Rs = Cmd.ExecuteReader()

                Rs.Read()
                If Rs.HasRows Then
                    get_tariffa_x_righe_prepagato = Rs(0)
                Else
                    get_tariffa_x_righe_prepagato = "0"
                End If
            Else
                Dim condizione_id_prev As String = ""
                'If id_prev <> 0 Then
                '    condizione_id_prev = " AND tariffe.id=" & id_prev
                'End If

                'QUERY: MODIFICARE LA QUERY ANCHE IN PRENOTAZIONI - CONTRATTI (DENTRO LA FUNZIONE tariffa_vendibile)

                'PARTE 1: SELEZIONO LE TARIFFE GENERICHE (PER CUI TUTTE LE FONTI SONO POSSIBILI - TUTTE LE STAZIONI SONO POSSIBILI)
                'COME ID SELEZIONO DIRETTAMENTE LA RIGA DI tariffe_righe DA CUI POI POSSO SUBITO OTTENERE LA CONDIZIONE E IL TEMPO-KM
                'DA UTILIZZARE

                'Dim sql As String = " select id from tariffe_righe where id_tariffa = 16 and vendibilita_da <= GETDATE() and vendibilita_a >= GETDATE() and pickup_da <= '" & pickup_da & "' and pickup_a >= '" & pickup_da & "' AND ISNULL(min_giorni_nolo,0) <= '" & giorni_nolo & "' AND ISNULL(max_giorni_nolo,99) >= '" & giorni_nolo & "'"

                'Dim Sql = "(SELECT tariffe_righe.id,tariffe.codice FROM tariffe WITH(NOLOCK) " & _
                '"INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " & _
                '"WHERE tariffe.attiva='1' " & condizione_id_prev & " AND '" & pickup_da_temp & "' BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " & _
                '"AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a " & _
                '"AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_X_stazioni WITH(NOLOCK) WHERE id_tariffa=tariffe.id) " & _
                '"AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_X_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id) " & _
                '"AND '" & pickup_a_temp & "' <= ISNULL(max_data_rientro, '3000-12-12 23:59:59')) ORDER BY codice"

                Dim Sql = "(SELECT top(1) tariffe_righe.id FROM tariffe WITH(NOLOCK) " &
               "INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
               "WHERE (tariffe.is_web = '1' AND tariffe.is_web_prepagato = '1') AND tariffe.attiva='1' " & condizione_id_prev & " AND '" & pickup_da_temp & "' BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " &
               "AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a " &
               "AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_X_stazioni WITH(NOLOCK) WHERE id_tariffa=tariffe.id) " &
               "AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_X_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id) " &
               "AND '" & pickup_a_temp & "' <= ISNULL(max_data_rientro, '3000-12-12 23:59:59')) ORDER BY codice"

                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                ''Response.Write("SQL: " & sql)


                Dim Cmd As New Data.SqlClient.SqlCommand(Sql, Dbc)

                Risultato = Cmd.ExecuteScalar

                'SE SPECIFICATO, NEL CASO DI TARIFFA PARTICOLARE, SELEZIONO IL NOME PER FONTE
                Dim condizione_nome_tariffa_fonte As String = "NULL"
                If IdTipCliente > 0 Then
                    condizione_nome_tariffa_fonte = "(SELECT nome_tariffa FROM tariffe_X_fonti WHERE tariffe_x_fonti.id_tariffa=tariffe.id AND id_tipologia_cliente='" & IdTipCliente & "')"
                End If
                Dim SqlTariffeParticolari = "SELECT top(1) tariffe_righe.id FROM tariffe WITH(NOLOCK) " &
                "INNER JOIN tariffe_righe WITH(NOLOCK) ON tariffe.id=tariffe_righe.id_tariffa " &
                "WHERE (tariffe.is_web = '1' AND tariffe.is_web_prepagato = '1') AND tariffe.attiva='1' " & condizione_id_prev & " AND '" & pickup_da_temp & "' BETWEEN tariffe_righe.pickup_da AND tariffe_righe.pickup_a " &
                "AND GetDate() BETWEEN tariffe_righe.vendibilita_da AND tariffe_righe.vendibilita_a " &
                "AND '" & pickup_a_temp & "' <= ISNULL(max_data_rientro, '3000-12-12 23:59:59') " &
                "AND ( (" &
                "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & stazione & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND " &
                "(tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & stazioneOff & "' AND tipo='DROP' AND id_tariffa=tariffe.id))) " &
                " OR " &
                "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & stazione & "' AND tipo='PICK' AND id_tariffa=tariffe.id)) AND " &
                "(tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='DROP' AND id_tariffa=tariffe.id))) " &
                " OR " &
                "((tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE id_stazione='" & stazioneOff & "' AND tipo='DROP' AND id_tariffa=tariffe.id)) AND " &
                "(tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='PICK' AND id_tariffa=tariffe.id))) ) " 'MODIFICARE LA QUERY ANCHE IN RIBALTAMENTO.ASPX

                If IdTipCliente > 0 Then
                    'SE E' STATA SELEZIONATA UNA FONTE CONTROLLO ANCHE TRA I REQUISITI PER FONTE DELLE TARIFFE
                    'UNENDO IN OR (L'ULTIMO) LA POSSIBILITA CHE LA TARIFFA NON SPECIFICHI ALCUNA STAZIONE MA SPECIFICHI UNA O PIU' FONTI
                    SqlTariffeParticolari = SqlTariffeParticolari & "" &
                    "AND (tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tipologia_cliente='" & IdTipCliente & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da))" &
                    "OR tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id)) " &
                    "OR (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='PICK' AND id_tariffa=tariffe.id) AND tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_stazioni WITH(NOLOCK) WHERE tipo='DROP' AND id_tariffa=tariffe.id) AND tariffe.id IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tipologia_cliente='" & IdTipCliente & "' AND id_tariffa=tariffe.id AND (valido_da IS NULL OR getDate()>=valido_da)) )) ORDER BY codice"
                Else
                    'SE NON E' STATA SELEZIONATA UNA FONTE PRELEVO SOLAMENTE LE TARIFFE CHE NON HANNO ALCUN PREREQUISITO DI FONTE
                    SqlTariffeParticolari = SqlTariffeParticolari & "" &
                    "AND (tariffe.id NOT IN (SELECT DISTINCT id_tariffa FROM tariffe_x_fonti WITH(NOLOCK) WHERE id_tariffa=tariffe.id))) ORDER BY codice"
                End If

                Dim DbcParticolare As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                DbcParticolare.Open()
                ''Response.Write("SQL: " & sql)



                Dim CmdParticolare As New Data.SqlClient.SqlCommand(SqlTariffeParticolari, DbcParticolare)

                RisultatoParticolare = CmdParticolare.ExecuteScalar

                CmdParticolare.Dispose()
                CmdParticolare = Nothing
                DbcParticolare.Close()
                DbcParticolare.Dispose()
                DbcParticolare = Nothing

                If RisultatoParticolare <> "" Then
                    get_tariffa_x_righe_prepagato = RisultatoParticolare
                Else
                    get_tariffa_x_righe_prepagato = Risultato
                End If
            End If

        End Function

        'Inserita 04/06/2015
        Function nuovo_preventivo() As String
            Dim Risultato As String
            Try

                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO preventivi_web (id_operatore_creazione,data_creazione) VALUES('" & Costanti.costanti_web.id_operatore_web & "',convert(date,getDate(),102))", Dbc)
                Cmd.ExecuteNonQuery()

                Cmd = New Data.SqlClient.SqlCommand("SELECT @@IDENTITY FROM preventivi_web WITH(NOLOCK)", Dbc)

                Risultato = Cmd.ExecuteScalar

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing


            Catch ex As Exception
                Risultato = "-1"
            End Try
            nuovo_preventivo = Risultato



        End Function

        'Inserita 04/06/2015
        Function nuovo_preventivo_prepagato() As String
            Dim Risultato As String
            Try
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO preventivi_web (id_operatore_creazione,data_creazione) VALUES('5',convert(date,getDate(),102))", Dbc)
                ''Response.Write(Cmd.CommandText)
                'Response.End()
                Cmd.ExecuteNonQuery()

                Cmd = New Data.SqlClient.SqlCommand("SELECT @@IDENTITY FROM preventivi_web WITH(NOLOCK)", Dbc)

                Risultato = Cmd.ExecuteScalar

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            Catch ex As Exception
                Risultato = "-1"
            End Try


            nuovo_preventivo_prepagato = Risultato
        End Function

        'Inserita 04/06/2015
        Sub calcolaTariffa_x_gruppo(ByVal stazione_pick_up As String, ByVal data_pick_up As String, ByVal ore_pick_up As Integer, ByVal minuti_pick_up As Integer, ByVal stazione_drop_off As String, ByVal id_tariffe_righe As String, ByVal id_gruppo As String, ByVal id_gruppo_da_prenotazione_x_modifica_con_rack As String, ByVal giorni_noleggio As Integer, ByVal giorni_prepagati_x_modifica As String, ByVal prepagata As String, ByVal giorni_noleggio_extra_rack As Integer, ByVal sconto As Double, ByVal tipo_sconto As String, ByVal sconto_web_prepagato_primo_calcolo As Double, ByVal sconto_su_rack As Double, ByVal eta_primo_guidatore As String, ByVal eta_secondo_guidatore As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String, ByVal id_ditta As String, ByVal id_fonte_commissionabile As String, ByVal commissione_percentuale As String, ByVal tipo_commissione As String, ByVal primo_calcolo_commissione As Boolean, ByVal giorni_commissioni_originale As String)
            'id_gruppo_da_prenotazione_rack: deve essere passato l'id_gruppo da prenotazione UNICAMENTE quando in fase di contratti l'utente
            'chiede di cambiare gruppo E LA TARIFFA NON E' PIU' VENDIBILE. Deve già essere certo che non si tratti di un downsell. NON chiamare
            'questa funzione se si tratta di downsell (richiesta di gruppo meno costoso di quello in fase di prenotazione)..

            'FUNZIONAMENTO PREPAGATA.
            'PASSARE IL PARAMETRO GIORNI PREPAGATI SOLO PER UNA MODIFICA DI UNA PRENOTAZIONE GIA' IMPOSTATA COME PREPAGATA (passando prepagato a true)
            'SE SI PASSANO I GIORNI PREPAGATI (MODIFICA): IN QUESTO CASO GLI ELEMENTI SETTATI COME PREPAGATI NEL CALCOLO PRECEDENTE VERRANNO CALCOLATI UTILIZZANDO I GIORNI PREPAGATI 
            'ANCHE SE I GIORNI DI NOLEGGIO DIMINUISCONO. INOLTRE VENGONO AUTOMATICAMENTE AGGIUNTI GLI EVENTUALI COSTI PREPAGATI RIMOSSI COL NUOVO CALCOLO (ES: VAL - 2° GUIDATORE ECC...)
            'PASSARE PREPAGATA TRUE E GIORNI PREPAGATI 0 SIA IN FASE DI PRIMO CALCOLO SIA PER RIAGGIORNARE I COSTI PREPAGATI (IN CASO DI MODIFICA TARIFFA/RICALCOLO PER CORREZIONE ERRORI)
            Dim elementi_prepagati As New Collection

            Try
                If prepagata And giorni_prepagati_x_modifica > 0 Then
                    'IN QUESTO CASO E' UN CALCOLO SUCCESSIVO: PER UNA PREPAGATA. Recupero dal calcolo precedente gli elementi prepagat
                    elementi_prepagati = getElementiPrepagati(id_prenotazione, id_contratto, id_ribaltamento, num_calcolo, id_gruppo)
                End If

                Dim is_broker As Boolean = getIsBroker(id_tariffe_righe)
                'P'0ASSO 2: RECUPERO GLI ID DELLE CONDIZIONI E DEL TEMPO KM ASSOCIATO ALLE RIGHE DELLA TARIFFA FIGLIA E, SE E' STATO TROVATA, DELLA TARIFFA MADRE
                Dim id_condizione_figlia As String = getIdCondizione(id_tariffe_righe)
                Dim id_condizione_madre As String = getIdCondizioneMadre(id_tariffe_righe)

                'HttpContext.Current.Trace.Write("Id Tariffa: " & id_tariffe_righe)
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
                    End If
                End If

                'PASSO 3: RECUPERO IL COSTO DELLA TARIFFA DAL TEMPO-KM
                calcola_tempo_km(giorni_noleggio, giorni_prepagati_x_modifica, prepagata, giorni_noleggio_extra_rack, sconto_su_rack, stazione_pick_up, stazione_drop_off, id_tempo_km_figlia, id_tempo_km_rack, id_gruppo, id_gruppo_da_prenotazione_x_modifica_con_rack, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo)

            If Session("chiamata_sito_web") = 1 Then Exit Sub  'chiamata da home page 11.06.2021


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
                    prepagato_memorizza_costi_prepagati_x_fattura(id_preventivo, id_prenotazione, id_ribaltamento, id_contratto, id_gruppo, num_calcolo, "")
                End If

                If tipo_commissione = "1" Then
                    'COMMISSIONE PER AGENZIA DI VIAGGIO - SI PAGA SUGLI ELEMENTI COMMISSIONABILI - VA RICALCOLATO OGNI VOLTA CHE SI VARIA LA TARIFFA
                    aggiorna_commissioni_da_riconoscere(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, id_gruppo, num_calcolo, id_fonte_commissionabile, commissione_percentuale)
                End If
            Catch ex As Exception
                'Response.Write("calcolaTariffa_x_gruppo Error : " & ex.Message & "<br/>")
            End Try


        End Sub

        'Inserita 04/06/2015
        Function getElementiPrepagati(ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal id_ribaltamento As String, ByVal num_calcolo As String, ByVal id_gruppo As String) As Collection
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


            Try
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_elemento, num_elemento FROM " & tabella & " WHERE num_calcolo=" & num_calcolo - 1 & " AND id_documento=" & id_da_cercare & " AND id_gruppo=" & id_gruppo & " AND prepagato=1", Dbc)
                Dim Rs As Data.SqlClient.SqlDataReader
                Rs = Cmd.ExecuteReader

                Do While Rs.Read()
                    Dim ele As String = Rs("id_elemento")
                    If (Rs("num_elemento") & "") <> "" Then
                        ele = ele & "-" & Rs("num_elemento")
                    End If
                    elementi_prepagati.Add(ele, ele)
                Loop

                getElementiPrepagati = elementi_prepagati

                Rs.Close()
                Rs = Nothing
                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            Catch ex As Exception
                'Response.Write("getElementiPrepagati error : " & ex.Message & "<br/>")
            End Try

        End Function

        'Inserita 04/06/2015
        Function getIsBroker(ByVal id_tariffe_righe As String) As Boolean
            'RESTITUISCE true SE LA TARIFFA RIGA SI RIFERISCE AD UNA TARIFFA BROKER, false ALTRIMENTI
            Try
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("SELECT tariffe.is_broker_prepaid FROM tariffe_righe WITH(NOLOCK) INNER JOIN tariffe WITH(NOLOCK) ON tariffe_righe.id_tariffa=Tariffe.id WHERE tariffe_righe.id='" & id_tariffe_righe & "'", Dbc)
                getIsBroker = Cmd.ExecuteScalar

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            Catch ex As Exception
                'Response.Write("getIsBroker error : " & ex.Message & "<br/>")
            End Try

        End Function

        'Inserita 04/06/2015
        Function getIdCondizione(ByVal id_tariffe_righe As String) As String
            Try
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_condizione FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)

                getIdCondizione = Cmd.ExecuteScalar & ""

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            Catch ex As Exception
                'Response.Write("getIdCondizione error : " & ex.Message & "<br/>")
            End Try

        End Function

        'Inserita 04/06/2015
        Function getIdCondizioneMadre(ByVal id_tariffe_righe As String) As String
            Try
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(id_condizione_madre,'0') FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)

                getIdCondizioneMadre = Cmd.ExecuteScalar & ""

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            Catch ex As Exception
                'Response.Write("getIdCondizioneMadre error : " & ex.Message & "<br/>")
            End Try

        End Function

        'Inserita 04/06/2015
        Function getIdTempoKm(ByVal id_tariffe_righe As String) As String
            Try
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim Cmd As New Data.SqlClient.SqlCommand("SELECT id_tempo_km FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)

                getIdTempoKm = Cmd.ExecuteScalar & ""

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            Catch ex As Exception
                'Response.Write("getIdTempoKm error : " & ex.Message & "<br/>")
            End Try

        End Function

        'Inserita 04/06/2015
        Function getIdTempoKmRack(ByVal id_tariffe_righe As String, ByVal id_contratto As String, ByVal id_prenotazione As String) As String
            'SE PER IL CONTRATTO O PRENOTAZIONE SPECIFICATO LA TARIFFA RACK E' STATA GIA' UTILIZZATA ALLORA QUESTA E' FISSATA E LA SELEZIONO
            'ALTRIMENTI LA RICERCO ALL'INTERNO DELLA TARIFFA RACK SPECIFICATA IN TARIFFE RIGHE
            'SE QUESTA VIENE TROVATA VIENE SUBITO SALVATA NELLA RIGA DI CONTRATTO/PRENOTAZIONE. SE IL DOCUMENTO VIENE CONFERMATO CON SALVATAGGIO
            'DA PARTE DELL'UTENTE, LA RACK TROVATA VERRA' UTILIZZATA PER TUTTE LE SUCCESSIVE MODIFICHE.
            Dim id_tempo_km As String
            Dim id_tariffa_rack As String
            Dim SqlStr As String

            Try
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
            Catch ex As Exception
                'Response.Write("getIdTempoKmRack error : " & ex.Message & "<br/>")
            End Try

        End Function

        'Inserita 04/06/2015
        Sub calcola_tempo_km(ByVal giorni_noleggio As Integer, ByVal giorni_prepagati As Integer, ByVal prepagata As Boolean, ByVal giorni_noleggio_extra_rack As Integer, ByVal sconto_su_rack As String, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal id_tempo_km_figlia As String, ByVal id_tempo_km_rack As String, ByVal id_gruppo As String, ByVal id_gruppo_da_prenotazione_x_modifica_con_rack As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String)

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

                Dim sqlStr As String
                Dim Rs As Data.SqlClient.SqlDataReader

                sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa, righe_tempo_km.gg_extra, righe_tempo_km.da FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                         "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                         "WHERE id_tempo_km='" & id_tempo_km_figlia & "' AND " & giorni_nolo - giorni_noleggio_extra_rack & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' " &
                         "AND NOT valore IS NULL AND valore<>0"


                Dbc.Close()
                Dbc.Open()
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                'HttpContext.Current.Trace.Write("SQLFilippo: " & Cmd.CommandText)
                Rs = Cmd.ExecuteReader()

                Rs.Read()

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
                                    valore_non_extra = valore_non_extra * giorni_non_extra
                                End If



                                'SE I GIORNI EXTRA SONO PACKED DEVO MOLTIPLICARE PER IL NUMERO DI GIORNI EXTRA E NON PER I GIORNI TOTALI DI NOLEGGIO
                                If packed = "False" Then
                                    qta = giorni_nolo
                                    valore = valore * ((giorni_nolo - giorni_noleggio_extra_rack) - giorni_non_extra)
                                End If

                                valore = valore + valore_non_extra
                            Else
                                'NON SI TROVA IL COSTO DEI GIORNI NON EXTRA - TRATTO IL CASO COME SE FOSSE UNA COLONNA NON EXTRA (PROBABILE ERRORE DI INSERIMENTO)
                                If packed = "False" Then
                                    qta = giorni_nolo
                                    valore = valore * (giorni_nolo - giorni_noleggio_extra_rack)
                                End If
                            End If
                        Else
                            'NON E' UN VALORE DI TIPO giorni extra
                            If packed = "False" Then
                                qta = giorni_nolo
                                valore = valore * (giorni_nolo - giorni_noleggio_extra_rack)
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

                            sqlStr = "SELECT righe_tempo_km.pac, righe_tempo_km.valore, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, tempo_km.iva_inclusa FROM righe_tempo_km WITH(NOLOCK) INNER JOIN tempo_km WITH(NOLOCK) ON righe_tempo_km.id_tempo_km=tempo_km.id " &
                            "INNER JOIN aliquote_iva WITH(NOLOCK) ON tempo_km.id_aliquota_iva=aliquote_iva.id " &
                            "WHERE id_tempo_km='" & id_tempo_km_rack & "' AND " & giorni_nolo & " BETWEEN da AND a AND id_gruppo='" & id_gruppo_da_calcolare & "' AND NOT valore IS NULL AND valore<>0"

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
                                    valore_rack = CDbl(Rs("valore")) * giorni_noleggio_extra_rack
                                ElseIf Rs("pac") = "True" Then
                                    valore_rack = (CDbl(Rs("valore")) / giorni_nolo) * giorni_noleggio_extra_rack
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
                                            valore_non_extra = valore_non_extra * giorni_non_extra
                                        End If

                                        'SE I GIORNI EXTRA SONO PACKED DEVO MOLTIPLICARE PER IL NUMERO DI GIORNI EXTRA E NON PER I GIORNI TOTALI DI NOLEGGIO
                                        '- EDIT: LA COLONNA GIORNI EXTRA DEVE ESSERE NON PACKED (METTO L'IF COMUNQUE PER EVITARE CONFUSIONE)
                                        If Not packed_trovato Then
                                            valore_rack_nuovo_gruppo = val_trovato * (giorni_nolo - giorni_non_extra)
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
                                        valore_rack_nuovo_gruppo = val_trovato * giorni_nolo
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
                                                valore_non_extra = valore_non_extra * giorni_non_extra
                                            End If

                                            'SE I GIORNI EXTRA SONO PACKED DEVO MOLTIPLICARE PER IL NUMERO DI GIORNI EXTRA E NON PER I GIORNI TOTALI DI NOLEGGIO
                                            '- EDIT: LA COLONNA GIORNI EXTRA DEVE ESSERE NON PACKED (METTO L'IF COMUNQUE PER EVITARE CONFUSIONE)
                                            If Not packed_trovato Then
                                                valore_rack_vecchio_gruppo = val_trovato * (giorni_nolo - giorni_non_extra)
                                            Else
                                                valore_rack_vecchio_gruppo = val_trovato
                                            End If

                                            valore_rack_vecchio_gruppo = valore_rack_vecchio_gruppo + valore_non_extra
                                        Else
                                            'NON SI TROVA IL COSTO DEI GIORNI NON EXTRA - TRATTO IL CASO COME SE FOSSE UNA COLONNA NON EXTRA (PROBABILE ERRORE DI INSERIMENTO)
                                            If Not packed_trovato Then
                                                valore_rack_vecchio_gruppo = val_trovato * giorni_nolo
                                            ElseIf packed_trovato Then
                                                valore_rack_vecchio_gruppo = val_trovato
                                            End If
                                        End If
                                    Else
                                        'SE NON E' UN COLONNA GIORNI EXTRA
                                        If Not packed_trovato Then
                                            valore_rack_vecchio_gruppo = val_trovato * giorni_nolo
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
                                            valore_non_extra = valore_non_extra * giorni_non_extra
                                        End If

                                        'SE I GIORNI EXTRA SONO PACKED DEVO MOLTIPLICARE PER IL NUMERO DI GIORNI EXTRA E NON PER I GIORNI TOTALI DI NOLEGGIO
                                        '- EDIT: LA COLONNA GIORNI EXTRA DEVE ESSERE NON PACKED (METTO L'IF COMUNQUE PER EVITARE CONFUSIONE)
                                        If Not packed_trovato Then
                                            valore_rack_nuovo_gruppo = val_trovato * (giorni_nolo - giorni_non_extra)
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
                                        valore_rack_nuovo_gruppo = val_trovato * giorni_nolo
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
                                                valore_non_extra = valore_non_extra * giorni_non_extra
                                            End If

                                            'SE I GIORNI EXTRA SONO PACKED DEVO MOLTIPLICARE PER IL NUMERO DI GIORNI EXTRA E NON PER I GIORNI TOTALI DI NOLEGGIO
                                            '- EDIT: LA COLONNA GIORNI EXTRA DEVE ESSERE NON PACKED (METTO L'IF COMUNQUE PER EVITARE CONFUSIONE)
                                            If Not packed_trovato Then
                                                valore_rack_vecchio_gruppo = val_trovato * (giorni_nolo - giorni_noleggio_extra_rack - giorni_non_extra)
                                            Else
                                                valore_rack_vecchio_gruppo = val_trovato
                                            End If

                                            valore_rack_vecchio_gruppo = valore_rack_vecchio_gruppo + valore_non_extra
                                        Else
                                            'NON SI TROVA IL COSTO DEI GIORNI NON EXTRA - TRATTO IL CASO COME SE FOSSE UNA COLONNA NON EXTRA (PROBABILE ERRORE DI INSERIMENTO)
                                            If Not packed_trovato Then
                                                valore_rack_vecchio_gruppo = val_trovato * (giorni_nolo - giorni_noleggio_extra_rack)
                                            ElseIf packed_trovato Then
                                                valore_rack_vecchio_gruppo = val_trovato
                                            End If
                                        End If
                                    Else
                                        'SE NON E' UN COLONNA GIORNI EXTRA
                                        If Not packed_trovato Then
                                            valore_rack_vecchio_gruppo = val_trovato * (giorni_nolo - giorni_noleggio_extra_rack)
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

                        If prepagata = False Then                   'se trova i valori del costo 
                            Session("costo_banco") = valore
                        Else
                            Session("costo_web") = valore
                        End If

                        'salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Costanti.ID_tempo_km, "", "Valore Tariffa" & "", valore, "NULL", iva, codice_iva, iva_inclusa, "True", "False", "False", Costanti.id_accessorio_incluso, "2", "True", "1", 1, "NULL", Costanti.id_unita_misura_giorni, "", packed, qta, "", False, prepagata)

                        If Session("chiamata_sito_web") = 1 Then Exit Sub 'se trova i valori del costo esce dalla funzione

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
                'Response.Write("calcolo_tempo_km error : " & ex.Message & "<br/>")
            End Try



        End Sub

        'Inserita 04/06/2015
        Sub calcolaVAL(ByVal giorni_noleggio As Integer, ByVal giorni_prepagati As Integer, ByVal prepagata As Boolean, ByVal elementi_prepagati As Collection, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal id_condizione_figlia As String, ByVal id_condizione_madre As String, ByVal id_gruppo As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String)
            'LA FUNZIONE SALVA IN preventivi_web_costi LA RIGA DEL VALORE DEL VAL
            Dim costo As String
            Dim percentuale As String
            Dim id_unita_misura As String
            Dim packed As String
            Dim qta As String
            Try
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

                        Dim sqlStr As String

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
                'Response.Write("calcolaVAL error : " & ex.Message & "<br/>")
            End Try

        End Sub

        'Inserita 04/06/2015
        Function calcola_supplemento_joung_driver(ByVal guidatore As String, ByVal id_gruppo As String, ByVal giorni_noleggio As Integer, ByVal giorni_prepagati As Integer, ByVal prepagata As Boolean, ByVal elementi_prepagati As Collection, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal eta_primo_guidatore As String, ByVal eta_secondo_guidatore As String, ByVal id_condizione_figlia As String, ByVal id_condizione_madre As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, Optional ByVal aggiungi_x_prepagato As Boolean = False) As Boolean


            'DEVO AGGIUNGERE IL SUPPLEMENTO PER IL PRIMO GUIDATORE SE NON E' SODDISFATTA L'ETA' DEL PRIMO GUIDATORE
            'L'EVENTUALE SUPPLEMENTO PER IL SECONDO GUIDATORE VERRA' AGGIUNTO SE VIENE SELEZIONATO L'ACCESSORIO SECONDO GUIDATORE
            'RESTITUISCE true SE IL SUPPLEMENTO VIENE TROVATO (O DEVE ESSER CALCOLATO), false ALTRIMENTI
            'L'ACCESSORIO PUO' ESSERE AGGIUNTO A PRESCINDERE DAI CONTROLLI SULLA VENDIBILITA' IMPOSTANDO PREPAGATO=TRUE (NEL CASO DI ACCESSORIO PREPAGATO INFATTI L'ACCESSORIO VIENE
            'AGGIUNTO ANCHE SE IL GUIDATORE CAMBIA E LA SUA AGGIUNTA NON SAREBBE PIU' NECESSARIA)
            Try
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

            Catch ex As Exception
                'Response.Write("calcola_supplemento_joung_driver error : " & ex.Message & "<br/>")
            End Try


        End Function

        'Inserita 04/06/2015
        Sub calcolaOnere(ByVal giorni_noleggio As Integer, ByVal giorni_prepagati As Integer, ByVal prepagata As Boolean, ByVal elementi_prepagati As Collection, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal id_condizione_figlia As String, ByVal id_condizione_madre As String, ByVal id_gruppo As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String)
            'PASSO 1: CONTROLLO NELLA TABELLA STAZIONI QUALE ONERE DEVE ESSERE PAGATO
            Dim sqlStr As String
            Dim valore_trovato As Boolean = False
            Dim costo As String
            Dim percentuale As String
            Dim id_unita_misura As String
            Dim packed As String
            Dim qta As String
            Try
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
                'Response.Write("calcola_onere error : " & ex.Message & "<br/>")
            End Try

        End Sub

        'Inserita 04/06/2015
        Sub analisi_condizioni(ByVal giorni_noleggio As Integer, ByVal giorni_prepagati As Integer, ByVal prepagata As Boolean, ByVal elementi_prepagati As Collection, ByVal data_pick_up As String, ByVal ore_pick_up As Integer, ByVal minuti_pick_up As Integer, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal id_condizione_figlia As String, ByVal id_condizione_madre As String, ByVal id_gruppo As String, ByVal id_ditta As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String)
            'ANALISI DELLE RIGHE CALCOLABILI (tipo stampa 1 o 2) 
            Dim costo As String
            Dim percentuale As String
            Dim selezionato As String
            Dim id_unita_misura As String
            Dim packed As String
            Dim qta As String
            Dim sqlStr As String = ""
            Dim contError As Integer = 0


            Try
                '---------------------------------------------------------------------------------------------------------------------------------
                'SE LA PRENOTAZIONE E' FUORI ORARIO PER PICK UP ANALIZZO ANCHE L'EVENTUALE ELEMENTO FUORI ORARIO DA UTILIZZARE
                Dim condizione_fuori_orario_pick_up As String = ""
                Dim fuori_orario_pick_up As String = stazione_aperta_pick_up(stazione_pick_up, data_pick_up, ore_pick_up, minuti_pick_up)

                'HttpContext.Current.Trace.Write(" AAA " & fuori_orario_pick_up)

                If fuori_orario_pick_up = "0" Or fuori_orario_pick_up = "1" Or fuori_orario_pick_up = "3" Then
                    'IN CASO DI STAZIONE CHIUSA (0) - NESSUN TEMPLATE ORARIO TROVATO (1) PER CUI SI CONSIDERA LA STAZIONE CHIUSA - 
                    'STAZIONE CHIUSA MA ACCETTA FUORI ORARIO (3) TROVO L'ELEMENTO FUORI ORARIO DA UTILIZZARE E LO AGGIUNGO AGLI ELEMENTI DA ANALIZZARE
                    condizione_fuori_orario_pick_up = get_elemento_fuori_orario_pickUp(ore_pick_up, minuti_pick_up)
                    If condizione_fuori_orario_pick_up <> "" Then
                        'E STATO RESTITUITO UN ID - CREO LA CONDIZIONE DA AGGIUNGERE ALLA SELECT 
                        condizione_fuori_orario_pick_up = " OR condizioni_elementi.id='" & condizione_fuori_orario_pick_up & "'"
                    End If
                    contError = 1
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
                    contError = 2
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

                sqlStr = "(SELECT condizioni.iva_inclusa, condizioni_elementi.id As id_elemento, condizioni_elementi.tipologia_franchigia, condizioni_righe.id_metodo_stampa,condizioni_righe.obbligatorio,condizioni_elementi.descrizione As nome_elemento, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, condizioni_elementi.scontabile, condizioni_elementi.omaggiabile, condizioni_elementi.acquistabile_nolo_in_corso ,condizioni_righe.id, condizioni_x_gruppi.id_gruppo, condizioni_righe.applicabilita_da," &
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
                    "(SELECT condizioni.iva_inclusa, condizioni_elementi.id As id_elemento, condizioni_elementi.tipologia_franchigia, condizioni_righe.id_metodo_stampa,condizioni_righe.obbligatorio,condizioni_elementi.descrizione As nome_elemento, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva, condizioni_elementi.scontabile, condizioni_elementi.omaggiabile, condizioni_elementi.acquistabile_nolo_in_corso, condizioni_righe.id, condizioni_x_gruppi.id_gruppo, condizioni_righe.applicabilita_da," &
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
                                " )  ORDER BY condizioni_x_gruppi.id_gruppo DESC"

                Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dim Rs As Data.SqlClient.SqlDataReader

                Dbc.Close()
                Dbc.Open()
                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                Rs = Cmd.ExecuteReader()
                Dim giorni_nolo As Integer
                Dim prepag As Boolean

                Do While Rs.Read()
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
                    '1) SE E' UNA FRANCHIGIA SETTO franchigia_attiva (della tabella preventivi_web_costi) A False
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
                        contError = 3
                    ElseIf (Rs("id_a_carico_di") & "") <> Costanti.id_accessorio_incluso And (Rs("obbligatorio") & "") = "True" And (Rs("id_metodo_stampa") & "") <> Costanti.id_stampa_informativa_con_valore And (Rs("id_metodo_stampa") & "") <> Costanti.id_stampa_informativa_senza_valore Then
                        'NON INCLUSI - OBBLIGATORI
                        ordine_stampa = 3
                        selezionato = "1"
                        If prepagata Then
                            'GLI ACCESSORI OBBLIGATORI CALCOLATI IN QUESTA FASE SONO CERTAMENTE PREPAGATI COME PRIMO CALCOLO - ATTUALMENTE LE SPESE DI SPEDIZIONI POSTALI 
                            'PUO' ESSERE PREPAGATO MA NON VIENE CALCOLATO IN FASE DI RIBALTAMNETO (PUO' ESSERE PREPAGATO SOLO DA ARES)
                            prepag = True
                            'If Rs("tipologia") <> "SPESE_SPED_FATT" Then
                            '    prepag = True
                            'Else
                            '    prepag = False
                            'End If
                        End If
                        contError = 4
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
                        contError = 5
                    End If

                    'OGNI RIGA E' DA SALVARE
                    If Rs("id_unita_misura") = "0" Then
                        'CASO 1: NESSUNA UNITA' DI MISURA: IN QUESTO CASO NON MI INTERESSA IL CASO "PACKED" - LA CONDIZIONE E' PER FORZA
                        'SENZA LIMITE (NON CONTROLLO I GIORNI DI NOLEGGIO O I KM)
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
                        contError = 6
                        salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", Rs("nome_elemento") & "", costo, percentuale, Rs("iva"), Rs("codice_iva") & "", Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), selezionato, ordine_stampa, tipologia_franchigia, id_unita_misura, "", packed, qta, "", False, prepag)
                        contError = 7
                    ElseIf Rs("id_unita_misura") = Costanti.id_unita_misura_giorni Then
                        'CASO 2: UNITA' DI MISURA GIORNI. CONTROLLO SE IL LIMITE (GIORNI DI NOLEGGIO) E' RISPETTATO O SE LA RIGA 
                        'E' SENZA ALCUN LIMITE (SE E' SEMPRE APPLICABILE AVRO' 0 SIA IN applicabilita_da CHE IN pplicabilita_a)
                        'IN OGNI CASO SE LA CONDIZIONE E' PACKED VIENE PRESO IL VALORE IN TOTO, SE NON E' PACKED MOLTIPLICO IL VALORE
                        'PER I GIORNI DI NOLEGGIO
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
                                contError = 8
                                salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", Rs("nome_elemento") & "", costo, percentuale, Rs("iva"), Rs("codice_iva") & "", Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), selezionato, ordine_stampa, tipologia_franchigia, id_unita_misura, "", packed, qta, "", False, prepag)
                                contError = 9
                            Else
                                qta = giorni_nolo
                                If Rs("tipo_costo") = "€" Then
                                    costo = CDbl(Rs("costo")) * giorni_nolo
                                    percentuale = "NULL"
                                ElseIf Rs("tipo_costo") = "%" Then
                                    costo = "NULL"
                                    percentuale = CDbl(Rs("costo")) * giorni_nolo
                                End If
                                contError = 10
                                salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", Rs("nome_elemento") & "", costo, percentuale, Rs("iva"), Rs("codice_iva") & "", Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), selezionato, ordine_stampa, tipologia_franchigia, id_unita_misura, "", packed, qta, "", False, prepag)
                                contError = 11
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
                                    costo = Rs("costo")
                                    percentuale = "NULL"
                                ElseIf Rs("tipo_costo") = "%" Then
                                    costo = "NULL"
                                    percentuale = Rs("costo")
                                End If
                                contError = 12
                                salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", Rs("nome_elemento") & "", costo, percentuale, Rs("iva"), Rs("codice_iva") & "", Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), selezionato, ordine_stampa, tipologia_franchigia, id_unita_misura, "", packed, qta, "", False, prepag)
                                contError = 13
                            ElseIf Rs("pac") = "False" And km_distanza_stazioni > 0 Then
                                If Rs("tipo_costo") = "€" Then
                                    costo = CDbl(Rs("costo")) * km_distanza_stazioni
                                    percentuale = "NULL"
                                ElseIf Rs("tipo_costo") = "%" Then
                                    costo = "NULL"
                                    percentuale = CDbl(Rs("costo")) * km_distanza_stazioni
                                End If
                                contError = 14
                                salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", Rs("nome_elemento") & "", costo, percentuale, Rs("iva"), Rs("codice_iva") & "", Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), selezionato, ordine_stampa, tipologia_franchigia, id_unita_misura, "", packed, qta, "", False, prepag)
                                contError = 15
                            End If
                        End If

                    ElseIf (Rs("km_divisore_num_giorni") & "") <> "" Then
                        'IN QUESTO CASO E' UN UNITA' DI MISURA RIGUARDATE I KM EXTRA - IN FASE DI RIENTRO QUESTA INFORMAZIONE VIENE USATA PER IL CALCOLO E L'INFORMATIVA VIENE RIMOSSA 
                        'MANUALMENTE DALLA LISTA
                        'PER QUESTE UNITA' DI MISURA IL SOLO CAMPO applicabilita_da VIENE LETTO - INDICA DOPO QUALE VALORE DI KM VIENE AGGIUNTO IL COSTO
                        costo = Rs("costo")
                        percentuale = "NULL"
                        Dim km_giorno_inclusi As String = CInt(CInt(Rs("applicabilita_da")) / Rs("km_divisore_num_giorni"))
                        contError = 16
                        salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", Rs("nome_elemento") & " oltre " & Rs("applicabilita_da") & " " & Rs("unita_misura"), costo, percentuale, Rs("iva"), Rs("codice_iva") & "", Rs("iva_inclusa"), Rs("scontabile"), Rs("omaggiabile"), Rs("acquistabile_nolo_in_corso"), Rs("id_a_carico_di"), Rs("id_metodo_stampa"), Rs("obbligatorio"), selezionato, ordine_stampa, tipologia_franchigia, id_unita_misura, km_giorno_inclusi, packed, qta, "", False, prepag)
                        contError = 17

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
                'Response.Write("analisi_condizioni error : " & contError & ex.Message & "<br/>" & sqlStr & "<br/>")

            End Try



        End Sub

        'Inserita 04/06/2015
        Sub aggiungi_accessorio_pieno_caburante(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String)
            'VIENE AGGIUNTO A COSTO 0 L'ACCESSORIO A SCELTA "PIENO CARBURANTE" CHE PERMETTE DI RIENTRARE SENZA IL PIENO EFFETTUATO
            Dim SqlStr As String = "SELECT condizioni_elementi.id, condizioni_elementi.descrizione, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva," &
                "scontabile, omaggiabile, acquistabile_nolo_in_corso FROM condizioni_elementi WITH(NOLOCK)" &
                "INNER JOIN aliquote_iva WITH(NOLOCK) ON condizioni_elementi.id_aliquota_iva=aliquote_iva.id " &
                "WHERE tipologia='RIMUOVI_RIFORNIMENTO'"
            Try
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()



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
                'Response.Write("aggiungi_accessorio_pieno_caburante error : " & ex.Message & "<br/>" & SqlStr & "<br/>")
            End Try

        End Sub

        'Inserita 04/06/2015
        Sub calcola_commissioni_gia_pagate(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal id_gruppo As Integer, ByVal num_calcolo As Integer, ByVal commissione_percentuale As Double, ByVal primo_calcolo As Boolean)
            Dim sqlStr As String = ""

            Try
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                Dim tabella As String
                Dim id_da_cercare As String
                If id_preventivo <> "" Then
                    tabella = "preventivi_web_costi"
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

                sqlStr = "UPDATE " & tabella & " SET commissioni_imponibile=((ISNULL(valore_costo,0)*" & Replace(commissione_percentuale, ",", ".") & "/100)*100/(100+aliquota_iva))," &
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
            Catch ex As Exception
                'Response.Write("calcola_commissioni_gia_pagate error : " & ex.Message & "<br/>" & sqlStr & "<br/>")
            End Try


        End Sub

        'Inserita 04/06/2015
        Sub calcolo_iva_e_totale(ByVal stazione_pick_up As String, ByVal id_tempo_km_figlia As String, ByVal id_condizione_figlia As String, ByVal id_condizione_madre As String, ByVal sconto As Double, ByVal sconto_web_prepagato_primo_calcolo As Double, ByVal tipo_sconto As String, ByVal id_gruppo As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal is_broker As Boolean, ByVal tipo_commissione As String, ByVal primo_calcolo_commissioni As Boolean)
            'SELEZIONO LE RIGHE DA preventivi_web_costi RELATIVI ALL'ATTUALE NUMERO DI CALCOLO DELLA PRENOTAZIONE E AL GRUPPO CONSIDERATO E LE
            'ANALIZZO PER DETERMINARE L'IVA E IL TOTALE
            Dim id_da_salvare As String
            Dim tabella As String
            Dim sqlstr As String = ""

            Try
                If id_preventivo <> "" Then
                    id_da_salvare = id_preventivo
                    tabella = "preventivi_web_costi"
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
                sqlstr = "UPDATE " & tabella & " SET imponibile=(valore_costo*100/(100+aliquota_iva)) - ISNULL(sconto_su_imponibile_prepagato,0) - ISNULL(commissioni_imponibile,0), iva_imponibile=(valore_costo*aliquota_iva/(100+aliquota_iva)) - (ISNULL(sconto_su_imponibile_prepagato,0)*aliquota_iva/100) - ISNULL(commissioni_iva,0) WHERE iva_inclusa='True' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL"
                Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                Cmd.ExecuteNonQuery()
                sqlstr = "UPDATE " & tabella & " SET imponibile=valore_costo - ISNULL(sconto_su_imponibile_prepagato,0)  - ISNULL(commissioni_imponibile,0), iva_imponibile=((valore_costo - ISNULL(sconto_su_imponibile_prepagato,0) - ISNULL(commissioni_imponibile,0))*aliquota_iva/100 ) WHERE iva_inclusa='False' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL"
                Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
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
                    sqlstr = "UPDATE " & tabella & " SET imponibile=imponibile-(imponibile*" & sconto_da_calcolare & "/100),iva_imponibile=(imponibile-(imponibile*" & sconto_da_calcolare & "/100))*aliquota_iva/100, imponibile_scontato=imponibile-(imponibile*" & sconto_da_calcolare & "/100), sconto_su_imponibile_prepagato=imponibile*" & sconto_da_calcolare & "/100 WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL " & condizione1_sconto
                    Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                    Cmd.ExecuteNonQuery()
                Else
                    sqlstr = "UPDATE " & tabella & " SET imponibile_scontato=imponibile-((imponibile-ISNULL(imponibile_scontato_prepagato,0))*" & sconto_da_calcolare & "/100) WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL " & condizione1_sconto
                    Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                    Cmd.ExecuteNonQuery()
                End If


                'HttpContext.Current.Trace.Write("UPDATE " & tabella & " SET imponibile_scontato=imponibile-((imponibile-ISNULL(imponibile_scontato_prepagato,0))*" & sconto & "/100) WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL " & condizione1_sconto)

                'PER GLI ELEMENTI NON SCONTABILI SETTO L'IMPONIBILE SCONTATO UGUALE ALL'IMPONIBILE
                sqlstr = "UPDATE " & tabella & " SET imponibile_scontato=imponibile, iva_imponibile_scontato=iva_imponibile WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL " & condizione2_sconto
                Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                Cmd.ExecuteNonQuery()
                '-----------------------------------------------------------------------------------------------------------------------------------
                '0B - CALCOLO DELL'IVA SULL'IMPONIBILE SCONTATO ------------------------------------------------------------------------------------
                sqlstr = "UPDATE " & tabella & " SET iva_imponibile_scontato=imponibile_scontato*aliquota_iva/100  WHERE (iva_inclusa='True' OR iva_inclusa='False') AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL" & condizione1_sconto
                Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                Cmd.ExecuteNonQuery()
                '-----------------------------------------------------------------------------------------------------------------------------------


                '1 - SELEZIONO IL VALORE TARIFFA - SE NON E' STATO TROVATO (QUINDI NEMMENO NELLA MADRE) DEVO MOSTRARE UN ERRORE
                sqlstr = "SELECT  imponibile, imponibile_scontato, iva_imponibile, iva_imponibile_scontato FROM " & tabella & " WITH(NOLOCK)  WHERE id_elemento='" & Costanti.ID_tempo_km & "' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                Rs = Cmd.ExecuteReader
                If Rs.Read() Then
                    totale_sconto = CDbl(Rs("imponibile")) - CDbl(Rs("imponibile_scontato")) + CDbl(Rs("iva_imponibile")) - CDbl(Rs("iva_imponibile_scontato"))

                    totale_sconto = FormatNumber(totale_sconto, 4, , , TriState.False)

                    valore_tariffa = Rs("imponibile_scontato") + Rs("iva_imponibile_scontato")

                    totale_imponibile_scontato = totale_imponibile_scontato + Rs("imponibile_scontato")
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
                    sqlstr = "SELECT  ISNULL(SUM(imponibile_scontato + iva_imponibile_scontato), 0) AS valore, ISNULL(SUM(imponibile_scontato), 0) AS imponibile_scontato, ISNULL(SUM(imponibile), 0) AS imponibile, ISNULL(SUM(iva_imponibile_scontato), 0) AS iva_imponibile_scontato, ISNULL(SUM(iva_imponibile), 0) AS iva_imponibile FROM " & tabella & " WITH(NOLOCK) WHERE id_a_carico_di<>'5' AND obbligatorio='1' And id_metodo_stampa<>'3' And id_metodo_stampa<>'4'  AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL"
                    Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
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
                    sqlstr = "SELECT id_onere FROM stazioni WITH(NOLOCK) WHERE id='" & stazione_pick_up & "'"
                    Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                    Dim id_onere As String = Cmd.ExecuteScalar
                    sqlstr = "SELECT valore_percentuale, aliquota_iva ,iva_inclusa FROM " & tabella & " WITH(NOLOCK) WHERE id_a_carico_di<>'" & Costanti.id_accessorio_incluso & "' AND obbligatorio='1' And id_metodo_stampa<>'" & Costanti.id_stampa_informativa_con_valore & "' And id_metodo_stampa<>'" & Costanti.id_stampa_informativa_senza_valore & "'  AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_percentuale IS NULL AND id_elemento='" & id_onere & "'"
                    Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
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
                        sqlstr = "SELECT TOP 1 id FROM condizioni_elementi_percentuale WITH(NOLOCK) WHERE id_elemento1='" & id_onere & "'"
                        Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                        Dim test As String = Cmd.ExecuteScalar & ""

                        If test = "" Then
                            'IN QUESTO CASO L'ELEMENTO NON E' STATO SPECIFICATO - APPLICO L'ELEMENTO PERCENTUALE ALL'IMPONIBILE DEL TOTALE
                            imponibile_percentuale = totale_imponibile_scontato * valore_percentuale / 100
                            iva_percentuale = imponibile_percentuale * aliquota_percentuale / 100

                            'salvaRigaCalcolo(id_da_salvare, num_calcolo, id_gruppo,  "NULL", "TOTALE PERC", valore_percentuale, "NULL", "NULL", "True", "True", "False", "2", "2", "True", 5)

                            'SALVO PER OGNI ELEMENTO IL TOTALE DELL'ONERE
                            sqlstr = "UPDATE " & tabella & " SET imponibile_onere= ISNULL(" & tabella & ".imponibile_scontato,0)*" & Replace(valore_percentuale / 100, ",", ".") & ", iva_onere=ISNULL(" & tabella & ".imponibile_scontato,0)*" & Replace((valore_percentuale / 100) * (aliquota_percentuale / 100), ",", ".") & " WHERE " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "'"
                            Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                            Cmd.ExecuteNonQuery()
                        Else
                            'IN QUESTO CASO L'ELEMENTO PERCENTUALE DIPENDE DA ALCUNI DEGLI ELEMENTI SPECIFICATI - ESEGUO IL CALCOLO TENENDONE CONTO
                            'SELEZIONO IL TOTALE DEL COSTO (IMPONIBILE ED IVA) SOLO PER GLI ELEMENTI A CUI E' APPLICABILE LA PERCENTUALE
                            'NON CONSIDERO (IN FASE DI USCITA) GLI ELEMENTI INFORMATIVI. INOLTRE IN QUESTA FASE NON CONSIDERO GLI ELEMENTI A SCELTA
                            '(I NON OBBLIGATORI)
                            sqlstr = "SELECT ISNULL(SUM(" & tabella & ".imponibile_scontato),0) FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi_percentuale WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi_percentuale.id_elemento2 WHERE " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "' AND " & tabella & ".id_metodo_stampa<>'" & Costanti.id_stampa_informativa_con_valore & "' AND " & tabella & ".id_metodo_stampa<>'" & Costanti.id_stampa_informativa_senza_valore & "' AND obbligatorio='1' AND condizioni_elementi_percentuale.id_elemento1='" & id_onere & "' " & condizione_onere_broker
                            Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)

                            imponibile_percentuale = Cmd.ExecuteScalar * valore_percentuale / 100
                            iva_percentuale = imponibile_percentuale * aliquota_percentuale / 100

                            'SALVO PER OGNI ELEMENTO PER CUI SI CALCOLA L'ONERE IL TOTALE DELL'ONERE
                            sqlstr = "UPDATE " & tabella & " SET imponibile_onere= ISNULL(" & tabella & ".imponibile_scontato,0)*" & Replace(valore_percentuale / 100, ",", ".") & ", iva_onere=ISNULL(" & tabella & ".imponibile_scontato,0)*" & Replace((valore_percentuale / 100) * (aliquota_percentuale / 100), ",", ".") & " WHERE " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "' AND id_elemento IN (SELECT id_elemento2 FROM condizioni_elementi_percentuale WITH(NOLOCK) WHERE id_elemento1='" & id_onere & "' " & condizione_onere_broker & ")"
                            Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                            Cmd.ExecuteNonQuery()

                            If is_broker Then
                                'SE LA TARIFFA E' BROKER AGGIUNGO IL VALORE DELL'ONERE AL COSTO DELL'ACCESSORIO E SALVO IN UN CAMPO PARTICOLARE IL VALORE DELL'ONERE E DELLA SUA IVA
                                'PER EVENTUALI STAMPE E STATISTICHE
                                Dim imponibile_percentuale_valore_tariffa As Double
                                Dim iva_percentuale_valore_tariffa As Double
                                Dim imponibile_percentuale_scontato_valore_tariffa As Double
                                Dim iva_percentuale_scontato_valore_tariffa As Double
                                sqlstr = "SELECT ISNULL(SUM(" & tabella & ".imponibile_scontato),0) FROM " & tabella & " WITH(NOLOCK)  WHERE " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "' AND " & tabella & ".id_metodo_stampa<>'" & Costanti.id_stampa_informativa_con_valore & "' AND " & tabella & ".id_metodo_stampa<>'" & Costanti.id_stampa_informativa_senza_valore & "' AND obbligatorio='1' AND " & tabella & ".id_elemento='" & Costanti.ID_tempo_km & "'"
                                Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                                imponibile_percentuale_scontato_valore_tariffa = Cmd.ExecuteScalar * valore_percentuale / 100
                                iva_percentuale_scontato_valore_tariffa = imponibile_percentuale_scontato_valore_tariffa * aliquota_percentuale / 100
                                sqlstr = "SELECT ISNULL(SUM(" & tabella & ".imponibile),0) FROM " & tabella & " WITH(NOLOCK)  WHERE " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "' AND " & tabella & ".id_metodo_stampa<>'" & Costanti.id_stampa_informativa_con_valore & "' AND " & tabella & ".id_metodo_stampa<>'" & Costanti.id_stampa_informativa_senza_valore & "' AND obbligatorio='1' AND " & tabella & ".id_elemento='" & Costanti.ID_tempo_km & "'"
                                Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                                imponibile_percentuale_valore_tariffa = Cmd.ExecuteScalar * valore_percentuale / 100
                                iva_percentuale_valore_tariffa = imponibile_percentuale_valore_tariffa * aliquota_percentuale / 100
                                'SALVO PER L'ACCESSORIO TEMPO KM L'IMPONIBILE ONERE
                                sqlstr = "UPDATE " & tabella & " SET imponibile=imponibile+'" & Replace(imponibile_percentuale_valore_tariffa, ",", ".") & "', iva_imponibile=iva_imponibile+'" & Replace(iva_percentuale_valore_tariffa, ",", ".") & "', imponibile_scontato=imponibile_scontato+'" & Replace(imponibile_percentuale_scontato_valore_tariffa, ",", ".") & "', iva_imponibile_scontato=iva_imponibile_scontato+'" & Replace(iva_percentuale_scontato_valore_tariffa, ",", ".") & "', imponibile_onere_broker_incluso='" & Replace(imponibile_percentuale_scontato_valore_tariffa, ",", ".") & "' ,iva_onere_broker_incluso='" & Replace(iva_percentuale_scontato_valore_tariffa, ",", ".") & "' WHERE " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "' AND " & tabella & ".id_elemento='" & Costanti.ID_tempo_km & "'"
                                Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
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
                        sqlstr = "UPDATE " & tabella & " SET imponibile='" & Replace(imponibile_percentuale, ",", ".") & "', imponibile_scontato='" & Replace(imponibile_percentuale, ",", ".") & "',iva_imponibile='" & Replace(iva_percentuale, ",", ".") & "', iva_imponibile_scontato='" & Replace(iva_percentuale, ",", ".") & "' WHERE id_elemento='" & id_onere & "' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "'"
                        Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                        Cmd.ExecuteNonQuery()
                    End If
                    '-------------------------------------------------------------------------------------------------------------------------------

                    Dbc.Close()
                    Dbc.Open()

                    '6 - SALVO LA RIGA DI SCONTO (SE DIVERSO DA 0)----------------------------------------------------------------------------------
                    totale_sconto = FormatNumber(totale_sconto, 4, , , TriState.False)
                    salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, "NULL", "", "SCONTO", totale_sconto, "NULL", "NULL", "NULL", "True", "True", "False", "False", "2", "2", "True", "0", 5, "NULL", "NULL", "", "NULL", "0", "", False, False)
                    sqlstr = "UPDATE " & tabella & " SET imponibile='" & Replace(totale_sconto, ",", ".") & "', imponibile_scontato='" & Replace(totale_sconto, ",", ".") & "', iva_imponibile_scontato='0',iva_imponibile='0' WHERE ordine_stampa='5' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL"
                    Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                    Cmd.ExecuteNonQuery()
                    '-------------------------------------------------------------------------------------------------------------------------------

                    '7 - SALVO LA RIGA DI TOTALE----------------------------------------------------------------------------------------------------
                    salvaRigaCalcolo(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, "NULL", "", Costanti.testo_elemento_totale, totale, "NULL", "NULL", "NULL", "True", "True", "False", "False", "2", "2", "True", "0", 6, "NULL", "NULL", "", "NULL", "0", "", False, False)
                    'AGGIUNGO IL COSTO IMPONIBILE E IVA PER LA RIGA TOTALE APPENA CREATA
                    sqlstr = "UPDATE " & tabella & " SET imponibile='" & Replace(totale_imponibile_scontato - imponibile_percentuale, ",", ".") & "',  imponibile_scontato='" & Replace(totale_imponibile_scontato - imponibile_percentuale, ",", ".") & "',iva_imponibile='" & Replace(totale_iva - iva_percentuale, ",", ".") & "', iva_imponibile_scontato='" & Replace(totale_iva - iva_percentuale, ",", ".") & "', imponibile_onere='" & Replace(imponibile_percentuale, ",", ".") & "', iva_onere='" & Replace(iva_percentuale, ",", ".") & "' WHERE ordine_stampa='6' AND id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT valore_costo IS NULL"
                    Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
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
                'Response.Write("calcolo_iva_e_totale error : " & ex.Message & "<br/>" & sqlstr & "<br/>")
            End Try

        End Sub

        'Inserita 04/06/2015
        Sub riporta_commissioni_agenzia(ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As Integer, ByVal id_gruppo As Integer)

            'SE VIENE PASSATO L'ID ELEMENTO (ED EVENTUALMENTE IL NUMERO DI ELEMENTO) VIENE RECUPERATO DAL CALCOLO PRECEDENTE UNICAMENTE QUESTO
            Dim sqlStr As String = ""
            Dim tabella As String
            Dim id_da_cercare As String

            Try
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
                sqlStr = "UPDATE t1 SET commissioni_imponibile=t2.commissioni_imponibile_originale, " &
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
            Catch ex As Exception
                'Response.Write("riporta_commissioni_agenzia error : " & ex.Message & "<br/>" & sqlStr & "<br/>")
            End Try


        End Sub

        'Inserita 04/06/2015
        Function gps_obbligatorio_o_incluso(ByVal id_gruppo As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String) As Boolean
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim tabella As String
            Dim id_da_cercare As String
            If id_preventivo <> "" Then
                tabella = "preventivi_web_costi"
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

        'Inserita 04/06/2015
        Sub aggiungi_val_gps(ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal id_gruppo As String, ByVal giorni_noleggio As Integer, ByVal giorni_prepagati_x_modifica As Integer, ByVal prepagata As Boolean, ByVal elementi_prepagati As Collection, ByVal giorni_restanti_x_nolo_in_corso As String, ByVal data_aggiunta_x_nolo_in_corso As String, ByVal sconto As Double, ByVal id_tariffe_righe As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As Integer, ByVal id_utente As String, ByVal tipo_commissione As String, ByVal percentuale_commissionabile As String, ByVal id_fonte_commissionabile As String)
            Dim sqlstr As String = ""

            Try
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                sqlstr = "SELECT val_gratis FROM stazioni_distanza WITH(NOLOCK) WHERE ((id_stazione1='" & stazione_pick_up & "' AND id_stazione2='" & stazione_drop_off & "') OR (id_stazione1='" & stazione_drop_off & "' AND id_stazione2='" & stazione_pick_up & "'))"
                Dim Cmd As New Data.SqlClient.SqlCommand(sqlstr, Dbc)

                Dim val_gratis As String = Cmd.ExecuteScalar

                If Not val_gratis = "True" Then
                    'SELEZIONO L'ID DEL VAL GPS
                    sqlstr = "SELECT id FROM condizioni_elementi WHERE tipologia='VAL_GPS'"
                    Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
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
                        aggiungi_accessorio_obbligatorio(id_elemento, stazione_pick_up, stazione_drop_off, id_gruppo, giorni_noleggio, giorni_prepagati_x_modifica, prepagata, elementi_prepagati, giorni_restanti_x_nolo_in_corso, data_aggiunta_x_nolo_in_corso, sconto, id_tariffe_righe, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_utente, False, tipo_commissione, percentuale_commissionabile, id_fonte_commissionabile)
                    End If
                End If

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            Catch ex As Exception
                'Response.Write("aggiungi_val_gps error : " & ex.Message & "<br/>" & sqlstr & "<br/>")
            End Try

        End Sub

        'Inserita 04/06/2015
        Sub aggiungi_accessorio_obbligatorio(ByVal id_accessorio As String, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal id_gruppo As String, ByVal giorni_noleggio As String, ByVal giorni_prepagati_x_modifica As Integer, ByVal prepagata As Boolean, ByVal elementi_prepagati As Collection, ByVal giorni_restanti_x_nolo_in_corso As String, ByVal data_aggiunta_x_nolo_in_corso As String, ByVal sconto As Double, ByVal id_tariffe_righe As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String, ByVal accessorio_val As Boolean, ByVal tipo_commissione As String, ByVal percentuale_commissionabile As String, ByVal id_fonte_commissionabile As String)
            'VIENE UTILIZZATO PER AGGIUBNGERE IL COSTI DI UN ELEMENTO OBBLIGATORIO SUCCESSIVAMENTE AL CALCOLO - ES: SPESE DI SPEDIZIONE POSTALI NEL CASO
            'IN CUI LA DITTA RICHIEDA LA SPEDIZIONE DELLA FATTURA PER MEZZO POSTA

            'IL PARAMETRO accossorio_val PERMETTE DI FORZARE L'INSERIMENTO DI UN ACCESSORIO VAL ANCHE SE NON DEVE ESSERE PAGATO (caso prepagato con val non piu' necessario).
            'IN QUESTO CASO, INFATTI, LA FUNZIONE CHE SI OCCUPA DEL CALCOLO DEVE LEGGERE L'ID ELEMENTO DA UNA COLONNA DIFFERENTE RISPETTO AGLI ALTRI ELEMENTI
            Try
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
                        prepagato_memorizza_costi_prepagati_x_fattura(id_preventivo, id_prenotazione, id_ribaltamento, id_contratto, id_gruppo, num_calcolo, id_accessorio)
                    End If
                End If
            Catch ex As Exception
                'Response.Write("aggiungi_accessorio_obbligatorio error : " & ex.Message & "<br/>")
            End Try




        End Sub

        'Inserita 04/06/2015
        Sub aggiungi_accessori_obbligatori_prepagati_calcolo_precedente(ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As Integer, ByVal id_gruppo As String, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal giorni As Integer, ByVal giorni_prepagati As Integer, ByVal sconto As Double, ByVal id_tariffe_righe As Integer, ByVal id_utente As String)

            Dim sqlStr As String
            Try
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

                sqlStr = "SELECT " & tabella & ".id_elemento, " & tabella & ".num_elemento, condizioni_elementi.tipologia, valore_costo FROM " & tabella & " WITH(NOLOCK) " &
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
                            calcola_costo_joung_driver_primo_guidatore(Rs("id_elemento"), stazione_pick_up, stazione_drop_off, 0, 0, id_gruppo, giorni_prepagati, giorni_prepagati, True, sconto, id_tariffe_righe, "", "", id_prenotazione, "", num_calcolo, id_utente, True, "", "", "")
                        ElseIf Rs("num_elemento") = "2" Then
                            calcola_costo_joung_driver_secondo_guidatore(Rs("id_elemento"), stazione_pick_up, stazione_drop_off, 0, 0, id_gruppo, giorni_prepagati, giorni_prepagati, True, sconto, id_tariffe_righe, "", "", id_prenotazione, "", num_calcolo, id_utente, True, "", "", "")
                        End If
                    ElseIf Rs("tipologia") = "VAL" Then
                        aggiungi_accessorio_obbligatorio(Rs("id_elemento"), stazione_pick_up, stazione_drop_off, id_gruppo, giorni_prepagati, giorni_prepagati, True, Nothing, "", "", sconto, id_tariffe_righe, "", "", id_prenotazione, id_contratto, num_calcolo, id_utente, True, "", "", "")
                    ElseIf Rs("tipologia") = "ARR_PREP" Then
                        copia_arrotondamento_prepagato("", id_prenotazione, id_contratto, id_gruppo, num_calcolo, Rs("valore_costo"))
                        'aggiungi_arrotondamento_prepagato("", id_prenotazione, id_contratto, id_gruppo, num_calcolo, Rs("valore_costo"))
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
            Catch ex As Exception
                'Response.Write("aggiungi_accessori_obbligatori_prepagati_calcolo_precedente error : " & ex.Message & "<br/>" & sqlStr & "<br/>")
            End Try

        End Sub

        'Inserita 04/06/2015
        Sub prepagato_memorizza_costi_prepagati_x_fattura(ByVal id_preventivo As String, ByVal id_prenotazione As String, ByVal id_ribaltamento As String, ByVal id_contratto As String, ByVal id_gruppo As Integer, ByVal num_calcolo As Integer, ByVal id_elemento As String)

            Dim sqlStr As String = ""
            Try
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
                ElseIf id_preventivo <> "" Then
                    tabella = "preventivi_web_costi"
                    id_da_cercare = id_preventivo
                End If

                Dim condizione_riga As String = ""
                If id_elemento <> "" Then
                    condizione_riga = " AND id_elemento=" & id_elemento
                End If

                sqlStr = "UPDATE " & tabella & " SET imponibile_scontato_prepagato=imponibile_scontato, iva_imponibile_scontato_prepagato=iva_imponibile_scontato," &
                    "imponibile_onere_prepagato=imponibile_onere, iva_onere_prepagato=iva_onere WHERE id_documento=" & id_da_cercare & " AND num_calcolo=" & num_calcolo &
                    " AND id_gruppo=" & id_gruppo & " AND (prepagato='1' OR nome_costo='" & Replace(Costanti.testo_elemento_totale, "'", "''") & "') " & condizione_riga
                'HttpContext.Current.Trace.Write(sqlStr)
                Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteNonQuery()

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            Catch ex As Exception
                'Response.Write("prepagato_memorizza_costi_prepagati_x_fattura error : " & ex.Message & "<br/>" & sqlStr & "<br/>")
            End Try



        End Sub

        'Inserita 04/06/2015
        Sub aggiorna_commissioni_da_riconoscere(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal id_gruppo As Integer, ByVal num_calcolo As Integer, ByVal id_fonte_commissionabile As Integer, ByVal commissione_percentuale As Double)
            Dim sqlStr As String
            Try
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                Dim tabella As String
                Dim id_da_cercare As String
                If id_preventivo <> "" Then
                    tabella = "preventivi_web_costi"
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


                sqlStr = "UPDATE " & tabella & " SET commissioni_imponibile_originale=ISNULL(imponibile_scontato,0)*" & Replace(CDbl(commissione_percentuale), ",", ".") & "/100," &
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
            Catch ex As Exception
                'Response.Write("aggiorna_commissioni_da_riconoscere error : " & ex.Message & "<br/>" & sqlStr & "<br/>")
            End Try

        End Sub

        'Inserita 04/06/2015
        Function salvaRigaCalcolo(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String, ByVal id_elemento As String, ByVal num_elemento As String, ByVal nome_costo As String, ByVal valore_costo As String, ByVal valore_percentuale As String, ByVal aliquota_iva As String, ByVal codice_iva As String, ByVal iva_inclusa As String, ByVal scontabile As String, ByVal omaggiabile As String, ByVal acquistabile_nolo_in_corso As String, ByVal id_a_carico_di As String, ByVal id_metodo_stampa As String, ByVal obbligatorio As String, ByVal selezionato As String, ByVal ordine_stampa As Integer, ByVal franchigia_attiva As String, ByVal id_unita_misura As String, ByVal km_giorno_inclusi As String, ByVal packed As String, ByVal qta As String, ByVal data_aggiunta_nolo_in_corso As String, ByVal restituire_id As Boolean, ByVal prepagata As Boolean) As String
            'SLAVA IN preventivi_web_costi IL COSTO DEL SINGOLO ACCESSORIO
            'NUM_ELEMENTO: numera un elemento qualora è presente più volte con lo stesso id (ES: YOUNG DRIVER PUO' ESSERE PRESENTE UNA VOLTA
            'PER IL PRIMO GUIDATORE O DUE VOLTE PER IL SECONDO. SE UN ELEMENTO E' PRESENTE UNA SOLA VOLTA NON E' NECESSARIO NUMERARLO)

            Dim id_da_salvare As String
            Dim tabella As String

            Dim colonna_km_inclusi As String = ""
            Dim kmGiornoInclusi As String = ""

            If id_preventivo <> "" Then
                id_da_salvare = id_preventivo
                tabella = "preventivi_web_costi"
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
                data_nolo_in_corso2 = ",'" & funzioni_comuni.getDataDb_con_orario(data_aggiunta_nolo_in_corso) & "'"
            End If



        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()


        Dim sqlStr As String

            If id_contratto <> "" Then
                sqlStr = "INSERT INTO " & tabella & " (id_documento, num_calcolo, id_gruppo, id_elemento, num_elemento, nome_costo, valore_costo,valore_percentuale, aliquota_iva, codice_iva, iva_inclusa, scontabile, omaggiabile, prepagato, acquistabile_nolo_in_corso, id_a_carico_di, id_metodo_stampa, obbligatorio, ordine_stampa, selezionato, franchigia_attiva, id_unita_misura," & colonna_km_inclusi & " qta, packed" & data_nolo_in_corso1 & ") VALUES ('" & id_da_salvare & "','" & num_calcolo & "','" & id_gruppo & "'," & id_elemento & "," & num_elem & ",'" & Replace(nome_costo, "'", "''") & "'," & costo & "," & percentuale & "," & Replace(aliquota_iva, ",", ".") & "," & cod_iva & ",'" & iva_inclusa & "'," & scont & "," & omagg & "," & prepag & "," & acquistabile_nolo & "," & a_carico_di & ",'" & id_metodo_stampa & "'," & obblig & "," & ordine_stampa & ",'" & selezionato & "'," & franchigia_attiva & "," & unita_misura & "," & kmGiornoInclusi & "'" & qta & "'," & pac & data_nolo_in_corso2 & ")"
            Else
                sqlStr = "INSERT INTO " & tabella & " (id_documento, num_calcolo, id_gruppo, id_elemento, num_elemento, nome_costo, valore_costo,valore_percentuale, aliquota_iva, codice_iva, iva_inclusa, scontabile, omaggiabile, prepagato, acquistabile_nolo_in_corso, id_a_carico_di, id_metodo_stampa, obbligatorio, ordine_stampa, selezionato, franchigia_attiva, id_unita_misura," & colonna_km_inclusi & " qta, packed" & data_nolo_in_corso1 & ") VALUES ('" & id_da_salvare & "','" & num_calcolo & "','" & id_gruppo & "'," & id_elemento & "," & num_elem & ",'" & Replace(nome_costo, "'", "''") & "'," & costo & "," & percentuale & "," & Replace(aliquota_iva, ",", ".") & "," & cod_iva & ",'" & iva_inclusa & "'," & scont & "," & omagg & "," & prepag & "," & acquistabile_nolo & "," & a_carico_di & ",'" & id_metodo_stampa & "'," & obblig & "," & ordine_stampa & ",'" & selezionato & "'," & franchigia_attiva & "," & unita_misura & "," & kmGiornoInclusi & "'" & qta & "'," & pac & data_nolo_in_corso2 & ")"
            End If




        'HttpContext.Current.Trace.Write(sqlStr)

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
        SqlConnection.ClearAllPools()
    End Function

        'Inserita 04/06/2015
        Function getDistanzaStazioni(ByVal stazione_pick_up As String, ByVal stazione_drop_off As String) As Integer
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

        'Inserita 04/06/2015
        Function gruppo_vendibile_eta_guidatori(ByVal id_gruppo As String, ByVal eta_primo_guidatore As String, ByVal eta_secondo_guidatore As String, ByVal id_preventivo As String, ByVal id_contratto As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal num_calcolo As String, ByVal id_utente As String, ByVal salva_warning As Boolean) As Integer
            'IL GRUPPO NON E' VENDIBILE SE L'ETA' NON E' RISPETTATA ALMENO PER IL PRIMO O PER IL SECONDO GUIDATORE (SE VALORIZZATI)
            '0 = NON VENDIBILE
            '1 = VENDIBILE CON JOUNG DRIVER PRIMO GUIDATORE
            '2 = VENDIBILE CON JOUNG DRIVER SECONDO GUIDATORE
            '3 = VENDIBILE CON JOUNG DRIVER ENTRAMBI GUIDATORI
            '4 = VENDIBILE
            gruppo_vendibile_eta_guidatori = -1
            Try
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
            Catch ex As Exception
                'Response.Write("gruppo_vendibile_eta_guidatori error : " & ex.Message & "<br/>")
            End Try

        End Function

        'Inserita 04/06/2015
        Function stazione_aperta_pick_up(ByVal id_stazione As String, ByVal data As String, ByVal ore As String, ByVal minuti As String) As String
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

            Dim id_orario_stazione As String
            Dim accetta_prenotazioni_fuori_orario As Boolean
            Dim Rs As Data.SqlClient.SqlDataReader
            Dim SqlStr As String = ""

            Try
                'CONTROLLO SE LA STAZIONE HA UN ORARIO SALVATO PER LA DATA SPECIFICATA (Restitusico 1)
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                Dim anno As String = Year(data)
                'SqlStr = "SELECT id_orario FROM stazione_orari WITH(NOLOCK) WHERE (CAST('" & data & "' AS DateTime) BETWEEN CAST(CAST(da_giorno AS nvarchar(4)) + '/' + CAST(da_mese AS nvarchar(4)) + '/' + '" & anno & "' AS DateTime) AND CAST(CAST(a_giorno AS nvarchar(4)) + '/' + CAST(a_mese AS nvarchar(4)) + '/' + '" & anno & "' AS DateTime)) AND id_stazione='" & id_stazione & "'"
                SqlStr = "SELECT id_orario FROM stazione_orari WITH(NOLOCK) WHERE (" & Day(data) & " BETWEEN da_giorno and a_giorno) AND (" & Month(data) & " between da_mese and a_mese) AND id_stazione='" & id_stazione & "'"

                Dim Cmd As New Data.SqlClient.SqlCommand(SqlStr, Dbc)

                id_orario_stazione = Cmd.ExecuteScalar & ""

                If id_orario_stazione = "" Then

                    stazione_aperta_pick_up = "1"  'NESSUN TEMPLATE DI ORARIO TROVATO PER LA DATA INDICATA (STAZIONE CHIUSA).
                Else
                    'SE' E' STATO TROVATO UN ORARIO SETTIMANLE CONTROLLO PER PRIMA COSA SE LA STAZIONE HA ASSOCIATO UN ORARIO FESTIVO (CHE SIA
                    'NAZIONALE (id_orario_festivita) O LOCALE (id_orario_festivita_locale) E SE LA DATA E' UN GIORNO FESTIVO IN ESSO SALVATO.
                    SqlStr = "SELECT TOP 1 opening_hour_from,opening_minute_from,opening_hour_to,opening_minute_to,ISNULL(opening_hour_from2,'-1') As opening_hour_from2,ISNULL(opening_minute_from2,'-1') As opening_minute_from2, ISNULL(opening_hour_to2,'-1') As opening_hour_to2, ISNULL(opening_minute_to2,'-1') As opening_minute_to2 FROM stazioni WITH(NOLOCK) INNER JOIN festivita_orari WITH(NOLOCK) ON stazioni.id_orario_festivita=festivita_orari.id OR stazioni.id_orario_festivita_locale = festivita_orari.id INNER JOIN festivita_orari_righe WITH(NOLOCK) ON festivita_orari_righe.id_festivita_orari=festivita_orari.id INNER JOIN festivita WITH(NOLOCK) ON festivita_orari_righe.id_festivita=festivita.id  WHERE stazioni.id='" & id_stazione & "' AND giorno='" & Day(data) & "' AND mese='" & Month(data) & "'"
                    Cmd = New Data.SqlClient.SqlCommand(SqlStr, Dbc)

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
                            SqlStr = "SELECT pren_fuori_orario_pickup FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
                            Cmd = New Data.SqlClient.SqlCommand(SqlStr, Dbc)
                            accetta_prenotazioni_fuori_orario = Cmd.ExecuteScalar
                            If accetta_prenotazioni_fuori_orario Then

                                stazione_aperta_pick_up = "3"
                            Else

                                stazione_aperta_pick_up = "0"
                            End If
                        End If
                    Else
                        'SE NON E' STATO TROVATO UN ORARIO FESTIVO O NON SIAMO IN UN GIORNO FESTIVO CONTROLLO IL NORMALE ORARIO DI STAZIONE
                        'HO GIA' MEMORIZZATO L'ORARIO SETTIAMANALE APPLICABILE IN QUESTO CASO. CONTROLLO SE PER L'ORARIO SPECIFICATO LA STAZIONE
                        'RISULTA APERTA O CHIUSA
                        Dbc.Close()
                        Dbc.Open()
                        Dim giorno_settimana As String = Weekday(data, 1)
                        SqlStr = "SELECT opening_hour_from,opening_minute_from,opening_hour_to,opening_minute_to,ISNULL(opening_hour_from2,'-1') As opening_hour_from2,ISNULL(opening_minute_from2,'-1') As opening_minute_from2, ISNULL(opening_hour_to2,'-1') As opening_hour_to2, ISNULL(opening_minute_to2,'-1') As opening_minute_to2 FROM orario_settimanale_righe WITH(NOLOCK) WHERE id_orario_settimanale='" & id_orario_stazione & "' AND '" & giorno_settimana & "' BETWEEN opening_day_from AND opening_day_to"
                        Cmd = New Data.SqlClient.SqlCommand(SqlStr, Dbc)
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
                                SqlStr = "SELECT pren_fuori_orario_pickup FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'"
                                Cmd = New Data.SqlClient.SqlCommand(SqlStr, Dbc)
                                accetta_prenotazioni_fuori_orario = Cmd.ExecuteScalar
                                If accetta_prenotazioni_fuori_orario Then

                                    stazione_aperta_pick_up = "3"
                                Else

                                    stazione_aperta_pick_up = "0"
                                End If
                            End If
                        Else
                            'IN QUESTO CASO E' STATO RICHIESTO UN GIORNO DOVE LA STAZIONE RISULTA CHIUSA. CONTROLLO SE LA STAZIONE ACCETTA PRENOTAZIONI
                            'FUORI ORARIO
                            Dbc.Close()
                            Dbc.Open()
                            Cmd = New Data.SqlClient.SqlCommand("SELECT pren_fuori_orario_pickup FROM stazioni WITH(NOLOCK) WHERE id='" & id_stazione & "'", Dbc)
                            accetta_prenotazioni_fuori_orario = Cmd.ExecuteScalar
                            If accetta_prenotazioni_fuori_orario Then

                                stazione_aperta_pick_up = "3"
                            Else

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
                'Response.Write("stazione_aperta_pick_up error : " & ex.Message & "<br/>" & SqlStr & "<br/>")
            End Try



        End Function

        'Inserita 04/06/2015
        Function get_elemento_fuori_orario_pickUp(ByVal ore_pick_up As Integer, ByVal minuti_pick_up As Integer) As String
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

        'Inserita 04/06/2015
        Function get_spese_postali(ByVal id_ditta As String) As String
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT tipo_spedizione_fattura FROM ditte WITH(NOLOCK) WHERE id_ditta='" & id_ditta & "'", Dbc)

            get_spese_postali = Cmd.ExecuteScalar & ""

            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End Function

        'Inserita 04/06/2015
        Function getCondizioneFranchigieRidotte(ByVal id_stazione As String) As String
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

        'Inserita 04/06/2015
        Public Function calcola_accessorio_extra_o_obbligatorio(ByVal id_accessorio As String, ByVal id_gruppo As String, ByVal giorni_noleggio As Integer, ByVal giorni_prepagati As Integer, ByVal prepagata As Boolean, ByVal elementi_prepagati As Collection, ByVal giorni_restanti_x_nolo_in_corso As String, ByVal data_aggiunta_x_nolo_in_corso As String, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal id_condizione_figlia As String, ByVal id_condizione_madre As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal accessorio_obbligatorio As Boolean, Optional ByVal accessorio_val As Boolean = False) As Boolean
            'DEVO AGGIUNGERE IL COSTO DELL'ELEMENTO EXTRA SCELTO (O DELL'ELEMENTO OBBLIGATORIO, SE IL RELATIVO PARAMETRO E' TRUE)
            'RESTITUISCE true SE IL COSTO VIENE TROVATO (O DEVE ESSER CALCOLATO), false ALTRIMENTI

            'PASSO 1: CONTROLLO NELLA TABELLA STAZIONI QUALE ONERE DEVE ESSERE PAGATO
            Dim tabella As String
            Dim id_da_cercare As String
            Dim sqlStr As String = ""


            Try
                If id_preventivo <> "" Then
                    tabella = "preventivi_web_costi"
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
                sqlStr = "SELECT id FROM " & tabella & " WITH(NOLOCK) WHERE id_documento='" & id_da_cercare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_accessorio & "' AND selezionato='1'"
                Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
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
            Catch ex As Exception
                'Response.Write("calcola_accessorio_extra_o_obbligatorio error : " & ex.Message & "<br/>" & sqlStr & "<br/>")
            End Try

        End Function

        'Inserita 04/06/2015
        Sub calcolo_iva_e_totale_singolo_accessorio(ByVal stazione_pick_up As String, ByVal sconto As Double, ByVal id_gruppo As String, ByVal id_accessorio As String, ByVal num_elemento As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal imposta_prepagato As Boolean, ByVal tipo_commissione As String, ByVal percentuale_commissioni As String, ByVal id_fonte_commissionabile As String)
            'SERVE PER CALCOLARE IVA E AGGIORNARE L'ONERE E IL TOTALE PER UN SINGOLO ACCESSORIO (SERVE PER JOUNG DRIVER SECONDO GUIDATORE ED
            'ACCESSORI NON VALORIZZATI INIZIALMENTE)

            Dim id_da_salvare As String
            Dim tabella As String

            If id_preventivo <> "" Then
                id_da_salvare = id_preventivo
                tabella = "preventivi_web_costi"
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

        'Inserita 04/06/2015
        Sub calcola_costo_joung_driver_primo_guidatore(ByVal id_accessorio As String, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal eta_primo_guidatore As String, ByVal eta_secondo_guidatore As String, ByVal id_gruppo As String, ByVal giorni_noleggio As Integer, ByVal giorni_prepagati_x_modifica As Integer, ByVal prenotazione_prepagata As Boolean, ByVal sconto As Double, ByVal id_tariffe_righe As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String, ByVal forza_aggiunzione_x_prepagato As Boolean, ByVal tipo_commissione As String, ByVal percentuale_commissionabile As String, ByVal id_fonte_commissionabile As String)
            'VIENE UTILIZZATA PER AGGIUNGERE IL COSTO DELLO JOUNG DRIVER PER IL PRIMO GUIDATORE.

            'PER PRIMA COSA SI CONTROLLA SE E' L'ACCESSORIO NON E' GIA' STATO AGGIUNTO
            Dim tabella As String
            Dim id_da_cercare As String
            If id_preventivo <> "" Then
                tabella = "preventivi_web_costi"
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

        'Inserita 04/06/2015
        Sub calcola_costo_joung_driver_secondo_guidatore(ByVal id_accessorio As String, ByVal stazione_pick_up As String, ByVal stazione_drop_off As String, ByVal eta_primo_guidatore As String, ByVal eta_secondo_guidatore As String, ByVal id_gruppo As String, ByVal giorni_noleggio As String, ByVal giorni_prepagati_x_modifica As Integer, ByVal prenotazione_prepagata As Boolean, ByVal sconto As Double, ByVal id_tariffe_righe As String, ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_utente As String, ByVal forza_aggiunzione_x_prepagato As Boolean, ByVal tipo_commissione As String, ByVal percentuale_commissionabile As String, ByVal id_fonte_commissionabile As String)
            'VIENE UTILIZZATA PER AGGIUNGERE IL COSTO DELLO JOUNG DRIVER PER IL SECONDO GUIDATORE. E' UN ACCESSORIO CHE NON VIENE CALCOLATO
            'IN FASE DI VALORIZZAZIONE DEL PREVENTIVO.

            'PER PRIMA COSA SI CONTROLLA SE E' L'ACCESSORIO NON E' GIA' STATO AGGIUNTO
            Dim tabella As String
            Dim id_da_cercare As String
            If id_preventivo <> "" Then
                tabella = "preventivi_web_costi"
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
                    calcolo_iva_e_totale_singolo_accessorio(stazione_pick_up, sconto, id_gruppo, id_accessorio, "2", id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, False, tipo_commissione, percentuale_commissionabile, id_fonte_commissionabile)
                End If
            End If
        End Sub

        'Inserita 04/06/2015
        Sub copia_arrotondamento_prepagato(ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal id_gruppo As String, ByVal num_calcolo As String, ByVal differenza_costo As Double)
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
            Dim sqlStr As String = "SELECT TOP 1 condizioni_elementi.id, condizioni_elementi.descrizione, aliquote_iva.aliquota As iva, aliquote_iva.codice_iva FROM condizioni_elementi WITH(NOLOCK) " &
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
        End Sub

        'Inserita 04/06/2015
        Sub salvaWarning(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal numero_warning As String, ByVal id_utente As String, ByVal stringa As String)
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
                testoWarning = "Stazione di pick up chiusa per la data di pick up specificata."
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
            End If

            'HttpContext.Current.Trace.Write("INSERT INTO " & tabella & " (id_documento, num_calcolo, warning, id_operatore, tipo) VALUES ('" & id_da_salvare & "','" & num_calcolo & "','" & Replace(testoWarning, "'", "''") & "','" & id_utente & "','" & tipoWarining & "')")

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO " & tabella & " (id_documento, num_calcolo, warning, id_operatore, tipo) VALUES ('" & id_da_salvare & "','" & num_calcolo & "','" & Replace(testoWarning, "'", "''") & "','" & id_utente & "','" & tipoWarining & "')", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End Sub

        'Inserita 05/06/2015
        Function getGiorniDiNoleggio(ByVal pick_up As String, ByVal drop_off As String, ByVal ora_pick_up As String, ByVal minuti_pick_up As String, ByVal ora_drop_off As String, ByVal minuti_drop_off As String, ByVal id_tariffe_righe As String, Optional ByVal considerare_tolleranza_extra As Boolean = False) As Integer
            'RESTITUISCE I GIORNI DI NOLEGGIO DATI DATA E ORA DI PICK UP, DATA E ORA DI DROP OFF E ID DELLA TABELLA tariffe_righe (INFATTI I 
            'MINUTI DI RITARDO MASSIMO CONSENTITI PRIMA DI FAR SCATTARE IL GIORNO EXTRA DI NOLEGGIO SONO MEMORIZZATI AL LIVELLO DI ASSOCIAZIONE
            'TEMPO+KM/CONDIZIONE, QUINDI PER CALCOLARE I GIORNI DI NOLEGGIO SERVE SAPERE LA RIGA DI TARIFFA)
            'SE SPEFICIATO LA FUNZIONE CONSIDERERA' ANCHE I MINUTI DI TOLLERANZA EXTRA (PER RIENTRO AUTO DA RA).
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim tolleranza As String
            If considerare_tolleranza_extra Then
                tolleranza = "minuti_di_ritardo+tolleranza_rientro_nolo"
            Else
                tolleranza = "minuti_di_ritardo"
            End If

            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT " & tolleranza & " FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)

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
        End Function

        'Inserita 11/06/2015
        Function getPrezzo(ByVal Id As String, ByVal IdGruppo As String) As String

        Dim sqlstr As String = ""

        Try
            'Inizio 11/06/2015
            Dim Risultato As String
            Dim DbcPrezzo As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            DbcPrezzo.Open()
            sqlstr = "SELECT *,ISNULL((imponibile+iva_imponibile+ISNULL(imponibile_onere,0)+ISNULL(iva_onere,0)),0) As valore_costo, " &
            "ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto FROM preventivi_web_costi " &
            "WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON preventivi_web_costi.id_gruppo=gruppi.id_gruppo WHERE id_documento='" & Id & "' " &
            "And Gruppi.id_gruppo = '" & IdGruppo & "' AND preventivi_web_costi.nome_costo = 'TOTALE' ORDER BY id DESC"

            Dim CmdPrezzo As New Data.SqlClient.SqlCommand(sqlstr, DbcPrezzo)
            'Dim CmdPrezzo As New Data.SqlClient.SqlCommand("SELECT  valore_costo, ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto FROM preventivi_web_costi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON preventivi_web_costi.id_gruppo=gruppi.id_gruppo WHERE id_documento='" & Id & "'AND preventivi_web_costi.nome_costo = 'TOTALE' ", DbcPrezzo)

            Dim RsPrezzo As Data.SqlClient.SqlDataReader
            RsPrezzo = CmdPrezzo.ExecuteReader()
            Dim prezzo As Double = 0
            RsPrezzo.Read()
            prezzo = FormatNumber(FormatNumber(RsPrezzo("valore_costo"), 2) - FormatNumber(RsPrezzo("sconto"), 2), 2)
            Risultato = prezzo


            CmdPrezzo.Dispose()
            CmdPrezzo = Nothing
            DbcPrezzo.Close()
            DbcPrezzo.Dispose()
            DbcPrezzo = Nothing

            getPrezzo = Risultato

        Catch ex As Exception
            Return "Errore"
            End Try
        End Function

        'Inserita 12/06/2015
        Public Shared Sub aggiungi_costo_accessorio(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazioni As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String, ByVal id_elemento As String, ByVal giorni_da_calcolare_x_nolo_in_corso As String, ByVal sconto_x_nolo_in_corso As String, ByVal data_aggiunta_x_nolo_in_corso As String, ByVal imposta_prepagato As Boolean, ByVal tipo_commissione As String, ByVal percentuale_commissione As String, ByVal id_fonte_commissionabile As String)
            'AL COSTO DELL'ELEMENTO POTREBBE ESSERE NECESSARIO AGGIUNGERE IL COSTO DELL'ELEMENTO PERCENTUALE
            'SI UTILIZZA PER GLI ELEMENTI GIA' VALORIZZATI IN FASE DI RICERCA
            Dim id_da_salvare As String
            Dim tabella As String
            If id_preventivo <> "" Then
                id_da_salvare = id_preventivo
                tabella = "preventivi_web_costi"
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
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 imponibile,iva_imponibile, imponibile_scontato,  iva_imponibile_scontato, condizioni_elementi.tipologia_franchigia, condizioni_elementi.sottotipologia_franchigia, ISNULL(imponibile_onere,0) As imponibile_onere, ISNULL(iva_onere,0) As iva_onere FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_elemento & "' AND selezionato='0'", Dbc)
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
                    tabella = "preventivi_web_costi"
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
                    If Not Rs("obbligatorio") And Rs("id_a_carico_di") = "2" Then
                        If Rs("omaggiato") Then
                            'omaggio_accessorio(False, False, True, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", tipo_commissione, percentuale_commissione, id_fonte_commissionabile, "ASSICURAZIONE", Rs("sottotipologia_franchigia"))
                            'rimuovi_costo_accessorio(id_preventivo, "", "", "", "1", id_gruppo, Rs("id_elemento"), "", "", "", True, "0", "0", "0")
                        Else
                            'rimuovi_costo_accessorio(Id, "", "", "", "1", id_gruppoScelto, id_elemento, "", "", "", prepagato, "0", "0", "0")
                            rimuovi_costo_accessorio(id_preventivo, "", "", "", "1", id_gruppo, Rs("id_elemento"), "", "", "", True, "0", "0", "0")
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
                    tabella = "preventivi_web_costi"
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
                    If Not Rs("obbligatorio") And Rs("id_a_carico_di") = "2" Then
                        If Rs("omaggiato") Then
                            'omaggio_accessorio(False, False, True, id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, Rs("id_elemento"), "", tipo_commissione, percentuale_commissione, id_fonte_commissionabile, "ASSICURAZIONE", Rs("sottotipologia_franchigia"))
                            'rimuovi_costo_accessorio(id_preventivo, "", "", "", "1", id_gruppo, Rs("id_elemento"), "", "", "", True, "0", "0", "0")
                        Else
                            rimuovi_costo_accessorio(id_preventivo, "", "", "", "1", id_gruppo, Rs("id_elemento"), "", "", "", True, "0", "0", "0")
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

        'Inserita 12/06/2015
        Shared Sub aggiorna_costo_accessorio_giornaliero(ByVal id_elemento As String, ByVal giorni_da_calcolare As String, ByVal sconto As String, ByVal data_aggiunta As String, ByVal id_contratto As String, ByVal num_calcolo As String)
            'AGGIORNA IL COSTO DI UN ACCESSORIO GIORNALIERO ACQUISTATO A NOLO IN CORSO TENENDO IN CONSIDERAZIONE I GIORNI DA FAR PAGARE
            'AGGIORNANDO IL COSTO MEMORIZZO ANCHE LA DATA DI SALVATAGGIO DEL COSTO (SERVE NEL CASO IN CUI VENGANO ESTESI I GIORNI DI NOLEGGIO
            'PER CALCOLARE DA QUALE GIORNO DEVE ESSERE AGGIORNATO IL COSTO DELL'ACCESSORIO)
            Dim data_agg As String = funzioni_comuni.getDataDb_con_orario(data_aggiunta)

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET valore_costo=(valore_costo/qta)*" & giorni_da_calcolare & ", qta='" & giorni_da_calcolare & "', data_aggiunta_nolo_in_corso='" & data_agg & "' WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND id_unita_misura='" & Costanti.id_unita_misura_giorni & "' AND id_elemento='" & id_elemento & "'", Dbc)
            Cmd.ExecuteNonQuery()

            'E' NECESSARIO AGGIORNARE I VALORI DI IVA E DI IMPONIBILE

            '0 - IMPONIBILE E IL VALORE DI IVA IMPONIBILE ---------------------------------------------
            Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET imponibile=(valore_costo*100/(100+aliquota_iva)), iva_imponibile=(valore_costo*aliquota_iva/(100+aliquota_iva)) WHERE iva_inclusa='True' AND id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "'  AND id_elemento='" & id_elemento & "' AND id_unita_misura='" & Costanti.id_unita_misura_giorni & "'", Dbc)
            Cmd.ExecuteNonQuery()
            Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET imponibile=valore_costo, iva_imponibile=(valore_costo*aliquota_iva/100) WHERE iva_inclusa='False' AND id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND id_elemento='" & id_elemento & "' AND id_unita_misura='" & Costanti.id_unita_misura_giorni & "'", Dbc)
            Cmd.ExecuteNonQuery()
            '-----------------------------------------------------------------------------------------------------------------------------------

            '0A - AGGIORNAMENTO DELL'EVENTUALE SCONTO RICHIESTO DALL'OPERATORE APPLICANDOLO ALL'IMPONIBILE DEGLI ELEMENTI SCONTABILI------------
            Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET imponibile_scontato=imponibile-(imponibile*" & sconto & "/100) WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND id_elemento='" & id_elemento & "' AND id_unita_misura='" & Costanti.id_unita_misura_giorni & "' AND scontabile='1'", Dbc)
            Cmd.ExecuteNonQuery()
            'PER GLI ELEMENTI NON SCONTABILI SETTO L'IMPONIBILE SCONTATO UGUALE ALL'IMPONIBILE
            Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET imponibile_scontato=imponibile, iva_imponibile_scontato=iva_imponibile WHERE id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND id_elemento='" & id_elemento & "' AND id_unita_misura='" & Costanti.id_unita_misura_giorni & "' AND scontabile='0'", Dbc)
            Cmd.ExecuteNonQuery()
            '-----------------------------------------------------------------------------------------------------------------------------------
            '0B - CALCOLO DELL'IVA SULL'IMPONIBILE SCONTATO ------------------------------------------------------------------------------------
            Cmd = New Data.SqlClient.SqlCommand("UPDATE contratti_costi SET iva_imponibile_scontato=imponibile_scontato*aliquota_iva/100  WHERE (iva_inclusa='True' OR iva_inclusa='False') AND id_documento='" & id_contratto & "' AND num_calcolo='" & num_calcolo & "' AND id_elemento='" & id_elemento & "' AND id_unita_misura='" & Costanti.id_unita_misura_giorni & "' AND scontabile='1'", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End Sub

        'Inserita 12/06/2015
        Private Shared Sub aggiorna_franchigie(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazione As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String, ByVal sottotipologia_franchigia As String)
            'RIMUOVO LE FRANCHIGIE NON RIDOTTE - AGGIUNGO LE FRANCHIGIA RIDOTTE (BASTA FARE IL NOT DEL CAMPO FRANCHIGIA_ATTIVA, IN QUANTO QUANDO E' ATTIVA
            'LA FRANCHIGIA NON E' ATTIVA LA RIDOTTA E VICEVERSA)

            Dim tabella As String
            Dim id_documento As String

            If id_preventivo <> "" Then
                tabella = "preventivi_web_costi"
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
                    Dim sqlStr As String = "SELECT ISNULL(prepagato,0) FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) " &
                        "ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE tipologia_franchigia='ASSICURAZIONE' AND sottotipologia_franchigia='TOTALE' " &
                        "AND id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "'"

                    Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

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

                    Dim Cmd1 As New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET franchigia_attiva= 1 - franchigia_attiva FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT franchigia_attiva IS NULL " & condizione_sottotipologia, Dbc)

                    Cmd1.ExecuteNonQuery()

                    Cmd1.Dispose()
                    Cmd1 = Nothing
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing
                End If


            Else
                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()

                'LA FRANCHGIA TOTALE CAMBIA L'ATTIVAZIONE O MENO DELLE FRANCHGIE COMPLETE FURTO E DANNI E NON TOCCA MAI LE PARZIALI. 
                'TUTTO QUESTO FUNZIONA NELL'IPOTESI CHE IL SISTEMA RIMUOVA AUTOMATICAMENTE LE EVENTUALI FRANCHIGIE PARZIALI PRECEDENTEMENTE INSERITE.

                Dim assicurazione_danni_prepagata As Boolean = False
                Dim assicurazione_furto_prepagata As Boolean = False

                If tabella = "prenotazioni_costi" Or tabella = "contratti_costi" Then
                    'NEL CASO DI PRENOTAZIONE O CONTRATTI DEVO CONTROLLARE E' STATA PREPAGATA LA CDR E/O LA TLR. IN CASO POSITIVO SI DEVE AGIRE SULLE EVENTUALI FRANCHIGIE PARZIALI
                    'INVECE CHE SULLE FRANCHIGIE COMPLETE COME NEGLI ALTRI CASI
                    Dim sqlStr As String = "SELECT ISNULL(prepagato,0) FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) " &
                        "ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE tipologia_franchigia='ASSICURAZIONE' AND sottotipologia_franchigia='DANNI' " &
                        "AND id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "'"

                    Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

                    assicurazione_danni_prepagata = Cmd.ExecuteScalar

                    Try

                    Catch ex As Exception

                    End Try

                    sqlStr = "SELECT ISNULL(prepagato,0) FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) " &
                        "ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE tipologia_franchigia='ASSICURAZIONE' AND sottotipologia_franchigia='FURTO' " &
                        "AND id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "'"

                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

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
                    Dim Cmd1 As New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET franchigia_attiva= 1 - franchigia_attiva FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT franchigia_attiva IS NULL AND (sottotipologia_franchigia='FURTO' OR sottotipologia_franchigia='DANNI') AND tipologia_franchigia='FRANCHIGIA'", Dbc)

                    Cmd1.ExecuteNonQuery()

                    Cmd1.Dispose()
                    Cmd1 = Nothing
                ElseIf assicurazione_danni_prepagata And assicurazione_furto_prepagata Then
                    Dim Cmd1 As New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET franchigia_attiva= 1 - franchigia_attiva FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT franchigia_attiva IS NULL AND (sottotipologia_franchigia='FURTO' OR sottotipologia_franchigia='DANNI') AND tipologia_franchigia='FRANCHIGIA RID'", Dbc)

                    Cmd1.ExecuteNonQuery()

                    Cmd1.Dispose()
                    Cmd1 = Nothing
                ElseIf assicurazione_danni_prepagata And Not assicurazione_furto_prepagata Then
                    Dim Cmd1 As New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET franchigia_attiva= 1 - franchigia_attiva FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT franchigia_attiva IS NULL AND (sottotipologia_franchigia='FURTO' AND tipologia_franchigia='FRANCHIGIA') OR (tipologia_franchigia='FRANCHIGIA RID' AND sottotipologia_franchigia='DANNI')", Dbc)

                    Cmd1.ExecuteNonQuery()

                    Cmd1.Dispose()
                    Cmd1 = Nothing
                ElseIf Not assicurazione_danni_prepagata And assicurazione_furto_prepagata Then
                    Dim Cmd1 As New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET franchigia_attiva= 1 - franchigia_attiva FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_documento & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND NOT franchigia_attiva IS NULL AND (sottotipologia_franchigia='FURTO' AND tipologia_franchigia='FRANCHIGIA RID') OR (tipologia_franchigia='FRANCHIGIA' AND sottotipologia_franchigia='DANNI')", Dbc)

                    Cmd1.ExecuteNonQuery()

                    Cmd1.Dispose()
                    Cmd1 = Nothing
                End If



                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing
            End If

        End Sub

        'Inserita 16/06/2015
        Shared Sub rimuovi_costo_accessorio(ByVal id_preventivo As String, ByVal id_ribaltamento As String, ByVal id_prenotazioni As String, ByVal id_contratto As String, ByVal num_calcolo As String, ByVal id_gruppo As String, ByVal id_elemento As String, ByVal giorni_da_calcolare_x_nolo_in_corso As String, ByVal sconto_x_nolo_in_corso As String, ByVal data_aggiunta_x_nolo_in_corso As String, ByVal imposta_prepagato As Boolean, ByVal tipo_commissione As String, ByVal percentuale_commissione As String, ByVal id_fonte_commissionabile As String)
            'TIPO:  SCELTA=ACCESSORIO A SCELTA (ALLA FINE DELLA PROCEDURA L'ELEMENTO VIENE IMPOSTATO COME NON SCELTO)
            '       EXTRA = ACCESSORIO EXTRA (ALLA FINE DELLA PROCEDURA LA RIGA VIENE RIMOSSA DA preventivi_costi)
            '       OMAGGIO = SE VENGO DA OMAGGIO ACCESSORIO NON DEVO IN OGNI CASO ELIMINARE LA RIGA
            'NUM_ELEMENTO: SE VIENE PASSATO VIENE RIMOSSO IL COSTO DELL'ACCESSORIO NUMERATO SECONDO IL CAMPO num_elemento (SERVE AD ESEMPIO
            'PER LO YOUNG DRIVER, NEL CASO IN CUI LO SI DEBBA RIMUOVERE SOLO PER IL PRIMO O SOLO PER IL SECONDO GUIDATORE)
            'Dim id_da_salvare As String
            'Dim tabella As String
            'If id_preventivo <> "" Then
            '    id_da_salvare = id_preventivo
            '    tabella = "preventivi_web_costi"
            'ElseIf id_ribaltamento <> "" Then
            '    id_da_salvare = id_ribaltamento
            '    tabella = "ribaltamento_costi"
            'ElseIf id_prenotazione <> "" Then
            '    id_da_salvare = id_prenotazione
            '    tabella = "prenotazioni_costi"
            'ElseIf id_contratto <> "" Then
            '    id_da_salvare = id_contratto
            '    tabella = "contratti_costi"
            'End If

            'Dim condizione_num As String = ""
            'If num_elemento <> "" And num_elemento <> "NULL" Then
            '    condizione_num = " AND num_elemento='" & num_elemento & "'"
            'End If

            'Dim id_onere As String = "0"
            'Dim imponibile_percentuale As Double = 0
            'Dim iva_percentuale As Double = 0

            'Dim imponibile_onere As Double = 0
            'Dim iva_onere As Double = 0

            'Dim aliquota_iva As Double

            'Dim imponibile_elemento As Double = 0
            'Dim iva_elemento As Double = 0

            'Dim aumento_imponibile_percentuale As Double = 0
            'Dim aumento_iva_percentuale As Double = 0

            'Dim commissioni_imponibile As Double = 0
            'Dim commissioni_iva As Double = 0

            'Dim valorizza As Boolean

            'Dim sconto As Double = 0
            'Dim tipologia_franchigia As String
            'Dim sottotipologia_franchigia As String

            'Dim omaggiato As Boolean

            'Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            'Dbc.Open()
            'Dim Rs As Data.SqlClient.SqlDataReader

            '1 - RECUPERO IL COSTO DELL'ELEMNTO SCELTO - SALVO ANCHE L'INFORMAZIONE SE L'ELEMENTO E' EXTRA OPPURE A SCELTA - SE L'ACCESSORIO E' PREPAGATO ALLORA NON VERRA' RIMOSSO
            'Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 imponibile,iva_imponibile, imponibile_scontato,  iva_imponibile_scontato, ISNULL(valorizza,'1') As valorizza, condizioni_elementi.tipologia_franchigia, condizioni_elementi.sottotipologia_franchigia, ISNULL(imponibile_onere, 0) As imponibile_onere, ISNULL(iva_onere,0) As iva_onere, ISNULL(omaggiato,0) As omaggiato, ISNULL(commissioni_imponibile_originale,0) As commissioni_imponibile_originale, ISNULL(commissioni_iva_originale,0) As commissioni_iva_originale FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_elemento & "' AND NOT prepagato='1'" & condizione_num, Dbc)
            'Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 imponibile,iva_imponibile, imponibile_scontato,  iva_imponibile_scontato, ISNULL(valorizza,'1') As valorizza, condizioni_elementi.tipologia_franchigia, condizioni_elementi.sottotipologia_franchigia, ISNULL(imponibile_onere, 0) As imponibile_onere, ISNULL(iva_onere,0) As iva_onere, ISNULL(omaggiato,0) As omaggiato, ISNULL(commissioni_imponibile_originale,0) As commissioni_imponibile_originale, ISNULL(commissioni_iva_originale,0) As commissioni_iva_originale FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_elemento & "' " & condizione_num, Dbc)
            'Rs = Cmd.ExecuteReader

            'If Rs.Read() Then
            '    tipologia_franchigia = Rs("tipologia_franchigia") & ""
            '    sottotipologia_franchigia = Rs("sottotipologia_franchigia") & ""

            '    valorizza = Rs("valorizza")
            '    omaggiato = Rs("omaggiato")
            '    ESEGUO LE OPERAZIONI SOLO SE L'ELEMENTO NON E' OMAGGIATO
            '    If Not omaggiato Then
            '        imponibile_elemento = Rs("imponibile_scontato")
            '        iva_elemento = Rs("iva_imponibile_scontato")

            '        imponibile_onere = Rs("imponibile_onere")
            '        iva_onere = Rs("iva_onere")

            '        sconto = Rs("imponibile") + Rs("iva_imponibile") - Rs("imponibile_scontato") - Rs("iva_imponibile_scontato")

            '        If tipo_commissione = "1" Then
            '            SE SIAMO NEL CASO DI FONTE COMMISSIONABILE CON COMMISSIONI RICONOSCIUTE SUCCESSIVAMENTE E L'ELEMENTO E' COMMISSIONABILE (LO E' SE I CAMPI RELATIVI SONO VALORIZZATI)
            '            MEMORIZZO GLI IMPORTI COMMISSIONABILI IN MODO DA POTERLI RIMUOVERE DAL TOTALE
            '            commissioni_imponibile = Rs("commissioni_imponibile_originale")
            '            commissioni_iva = Rs("commissioni_iva_originale")
            '        End If
            '    Else
            '        imponibile_elemento = 0
            '        iva_elemento = 0
            '        imponibile_onere = 0
            '        iva_onere = 0
            '        sconto = 0
            '    End If

            '    Dbc.Close()
            '    Dbc.Open()

            '    2 - CONTROLLO SE E' STATO SPECIFICATO L'ELEMENTO PERCENTUALE DI TIPO ONERE - OTTENGO IL SUO ID - SOLAMENTE SE L'IMPONIBILE DELL'ONERE
            '    E() ' VALORIZZATO ALTRIMENTI VUOL DIRE CHE SULL'ELEMENTO NON DEVO CALCOLARE L'ONERE
            '    If imponibile_onere <> 0 Then
            '        Cmd = New Data.SqlClient.SqlCommand("SELECT TOP 1 " & tabella & ".id_elemento FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE condizioni_elementi.tipologia='ONERE' AND " & tabella & ".id_documento='" & id_da_salvare & "' AND " & tabella & ".num_calcolo='" & num_calcolo & "' AND " & tabella & ".id_gruppo='" & id_gruppo & "' AND NOT " & tabella & ".valore_percentuale IS NULL", Dbc)
            '        Rs = Cmd.ExecuteReader
            '        Rs.Read()

            '        NUOVA VERSIONE CON ELEMENTO PERCENTUALE PRECALCOLATO --------------------------------------------------------------------------
            '        If Rs.HasRows Then
            '            id_onere = Rs("id_elemento")

            '            NELLA TABELLA DEI COSTI L'IMPONIBILE E L'IVA DELL'ONERE E' STATO PRECALCOLATO - I CAMPI SONO VALORIZZATI SOLAMENTE SOLAMENTE SE
            '            SULL() 'ELEMENTO SI DEVE PAGARE L'ONERE ALTRIMENTI I VALORI SONO 0

            '            aumento_imponibile_percentuale = imponibile_onere
            '            aumento_iva_percentuale = iva_onere
            '        Else
            '            IN QUESTO CASO NON E' STATO SPECIFICATO ALCUN ELEMENTO PERCENTUALE - SI DEVE SEMPLICAMENTE AGGIUNGERE AL TOTALE IL COSTO DELLO
            '            ELEMENTO(SCELTO)
            '            aumento_imponibile_percentuale = 0
            '            aumento_iva_percentuale = 0
            '        End If
            '        -------------------------------------------------------------------------------------------------------------------------------

            '        Dbc.Close()
            '        Dbc.Open()
            '    End If


            '    'SE E' STATO TROVATO UN AUMENTO DELL'ONERE AGGIORNO LA RIGA DELL'ONERE
            '    If id_onere <> "0" Then
            '        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=imponibile-" & Replace(aumento_imponibile_percentuale, ",", ".") & ", imponibile_scontato=imponibile_scontato-" & Replace(aumento_imponibile_percentuale, ",", ".") & ",iva_imponibile=iva_imponibile-" & Replace(aumento_iva_percentuale, ",", ".") & ", iva_imponibile_scontato=iva_imponibile_scontato-" & Replace(aumento_iva_percentuale, ",", ".") & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_onere & "'", Dbc)
            '        Cmd.ExecuteNonQuery()
            '    End If

            '    Dim onere_prepagato As String = ""
            '    SE(E) ' STATO TROVATO UN AUMENTO DELL'ONERE AGGIORNO LA RIGA DELL'ONERE
            '    If id_onere <> "0" Then
            '        If imposta_prepagato Then
            '            SE SI STA IMPOSTANDO IL TOTALE COME PREPAGATO ALLORA E' NECESSARIO AGGIORNARE IL TOTALE AGGIUNGENDO IL COSTO DELL'ELEMENTO
            '            onere_prepagato = ", imponibile_scontato_prepagato=imponibile_scontato_prepagato-" & Replace(aumento_imponibile_percentuale, ",", ".") & ", iva_imponibile_scontato_prepagato=iva_imponibile_scontato_prepagato-" & Replace(aumento_iva_percentuale, ",", ".")
            '        End If
            '        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=imponibile-" & Replace(aumento_imponibile_percentuale, ",", ".") & ", imponibile_scontato=imponibile_scontato-" & Replace(aumento_imponibile_percentuale, ",", ".") & ",iva_imponibile=iva_imponibile-" & Replace(aumento_iva_percentuale, ",", ".") & ", iva_imponibile_scontato=iva_imponibile_scontato-" & Replace(aumento_iva_percentuale, ",", ".") & onere_prepagato & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_onere & "'", Dbc)
            '        Cmd.ExecuteNonQuery()
            '    End If

            '    If Not omaggiato Then
            '        AGGIORNO LA RIGA DEL TOTALE RIMUOVENDO IL COSTO DELL'ACCESSORIO E DELL'EVENTUALE AUMENTO DELL'ONERE PERCENTUALE
            '        Dim update_commissioni As String = ""
            '        If tipo_commissione = "1" Then
            '            RIMUOVO DAL TOTALE LE COMMISSIONI_DELL'ELEMENTO
            '            update_commissioni = ", commissioni_imponibile_originale=ISNULL(commissioni_imponibile_originale,0)-" & Replace(commissioni_imponibile, ",", ".") & _
            '                ",commissioni_iva_originale=ISNULL(commissioni_iva_originale,0)-" & Replace(commissioni_iva, ",", ".")
            '        End If
            '        If imposta_prepagato Then
            '            SE SI STA IMPOSTANDO IL TOTALE COME PREPAGATO ALLORA E' NECESSARIO AGGIORNARE IL TOTALE AGGIUNGENDO IL COSTO DELL'ELEMENTO
            '            onere_prepagato = ", imponibile_scontato_prepagato=imponibile_scontato_prepagato-" & Replace(aumento_imponibile_percentuale, ",", ".") & ", iva_imponibile_scontato_prepagato=iva_imponibile_scontato_prepagato-" & Replace(aumento_iva_percentuale, ",", ".")
            '        End If
            '        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=imponibile-" & Replace(imponibile_elemento, ",", ".") & ", imponibile_scontato=imponibile_scontato-" & Replace(imponibile_elemento, ",", ".") & ", iva_imponibile_scontato=iva_imponibile_scontato-" & Replace(iva_elemento, ",", ".") & ",iva_imponibile=iva_imponibile-" & Replace(iva_elemento, ",", ".") & ",imponibile_onere=imponibile_onere-" & Replace(aumento_imponibile_percentuale, ",", ".") & ",iva_onere=iva_onere-" & Replace(aumento_iva_percentuale, ",", ".") & ", valore_costo=valore_costo-" & Replace(imponibile_elemento + aumento_imponibile_percentuale + iva_elemento + aumento_iva_percentuale, ",", ".") & onere_prepagato & update_commissioni & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND ordine_stampa='6'", Dbc)
            '        Cmd.ExecuteNonQuery()
            '    End If

            '    AGGIORNO IL CAMPO 'SELEZIONATO' DELL'ELEMENTO SCELTO OPPURE LO RIMUOVO - L'ELEMENTO VIENE RIMOSSO ANCHE SE VIENE PASSAATO "SCELTA"
            '    (IN QUANTO ERA PRESENTE TRA GLI ELEMENTI VALORIZZATI SELEZIONABILI) MA IN REALTA' E' SALVATO IN condizioni_elementi COME 
            '    NON DA VALORIZZARE (VUOL DIRE CHE L'ELEMENTO ERA STATO AGGIUNTO DAL MENU' A TENDINA DEGLI ELEMENTI EXTRA E QUINDI, RIMUOVENDOLO, LO
            '    SI DEVE ANCHE CANCELLARE)
            '    If tipo = "EXTRA" Or (Not valorizza And tipo <> "OMAGGIO") Then
            '        Cmd = New Data.SqlClient.SqlCommand("DELETE FROM " & tabella & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_elemento & "'" & condizione_num, Dbc)
            '        Cmd.ExecuteNonQuery()
            '    ElseIf tipo = "SCELTA" Or tipo = "OMAGGIO" Then
            '        Dim update_commissioni As String = ""
            '        If tipo_commissione = "1" Then
            '            SETTO A NULL I CAMPI DELLE COMMISSIONI - ATTUALMENTE SI POTREBBE FARE IN OGNI CASO MA PUO' DARE PROBLEMI SE POI SI RENDERANNO COMMISIONABILI GLI ELEMENTI ANCHE NEL CASO DI COMMISSIONI PREINCASSATE DALL'AGENZIA
            '            update_commissioni = ", commissioni_imponibile_originale=NULL, commissioni_iva_originale=NULL "
            '        End If
            '        27-10-2015
            '        If imposta_prepagato Then
            '            SE SI STA IMPOSTANDO IL TOTALE COME PREPAGATO ALLORA E' NECESSARIO AGGIORNARE IL TOTALE AGGIUNGENDO IL COSTO DELL'ELEMENTO
            '            onere_prepagato = ", imponibile_scontato_prepagato=imponibile_scontato-" & Replace(FormatNumber(sconto, 4, , , TriState.False), ",", ".") & ", iva_imponibile_scontato_prepagato=iva_imponibile_scontato_prepagato-" & Replace(aumento_iva_percentuale, ",", ".")
            '        End If
            '        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET selezionato='0',prepagato='0' " & onere_prepagato & update_commissioni & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_elemento & "'" & condizione_num, Dbc)
            '        Cmd.ExecuteNonQuery()
            '    End If

            '    AGGIORNO LO SCONTO 
            '    If Not omaggiato Then
            '        Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET valore_costo=valore_costo-" & Replace(FormatNumber(sconto, 4, , , TriState.False), ",", ".") & ",imponibile=imponibile-" & Replace(FormatNumber(sconto, 4, , , TriState.False), ",", ".") & ", imponibile_scontato=imponibile_scontato-" & Replace(FormatNumber(sconto, 4, , , TriState.False), ",", ".") & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND ordine_stampa='5'", Dbc)
            '        Cmd.ExecuteNonQuery()
            '    End If

            '    SE STO RIMUOVENDO UN ELEMENTO ASSICURAZIONE FACCIO IN MODO DI VISUALIZZARE LE FRANCHIGIE GENERICHE E RIMUOVERE LE EVENTUALI FRANCHIGIE RIDOTTE
            '    If tipologia_franchigia = "ASSICURAZIONE" And tipo <> "OMAGGIO" Then
            '        aggiorna_franchigie(id_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_gruppo, sottotipologia_franchigia)
            '    End If
            'End If

            'Rs.Close()
            'Rs = Nothing
            'Cmd.Dispose()
            'Cmd = Nothing
            'Dbc.Close()
            'Dbc.Dispose()
            'Dbc = Nothing



            'AL COSTO DELL'ELEMENTO POTREBBE ESSERE NECESSARIO AGGIUNGERE IL COSTO DELL'ELEMENTO PERCENTUALE
            'SI UTILIZZA PER GLI ELEMENTI GIA' VALORIZZATI IN FASE DI RICERCA
            Dim id_da_salvare As String
            Dim tabella As String
            If id_preventivo <> "" Then
                id_da_salvare = id_preventivo
                tabella = "preventivi_web_costi"
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
                condizione_prepagato = ", prepagato=0, imponibile_scontato_prepagato=imponibile_scontato, iva_imponibile_scontato_prepagato=iva_imponibile_scontato, " &
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
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT TOP 1 imponibile,iva_imponibile, imponibile_scontato,  iva_imponibile_scontato, condizioni_elementi.tipologia_franchigia, condizioni_elementi.sottotipologia_franchigia, ISNULL(imponibile_onere,0) As imponibile_onere, ISNULL(iva_onere,0) As iva_onere FROM " & tabella & " WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) ON " & tabella & ".id_elemento=condizioni_elementi.id WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_elemento & "'", Dbc)
            Rs = Cmd.ExecuteReader

            If Rs.Read() Then
                tipologia_franchigia = Rs("tipologia_franchigia") & ""
                sottotipologia_franchigia = Rs("sottotipologia_franchigia") & ""

                imponibile_elemento = Rs("imponibile_scontato")
                iva_elemento = Rs("iva_imponibile_scontato")

                imponibile_onere = Rs("imponibile_onere")
                iva_onere = Rs("iva_onere")

                sconto = Rs("imponibile") + Rs("iva_imponibile") - Rs("imponibile_scontato") - Rs("iva_imponibile_scontato")
            End If

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
                    onere_prepagato = ", imponibile_scontato_prepagato=imponibile_scontato_prepagato-" & Replace(aumento_imponibile_percentuale, ",", ".") & ", iva_imponibile_scontato_prepagato=iva_imponibile_scontato_prepagato-" & Replace(aumento_iva_percentuale, ",", ".")
                End If
                Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=imponibile-" & Replace(aumento_imponibile_percentuale, ",", ".") & ", imponibile_scontato=imponibile_scontato-" & Replace(aumento_imponibile_percentuale, ",", ".") & ",iva_imponibile=iva_imponibile-" & Replace(aumento_iva_percentuale, ",", ".") & ", iva_imponibile_scontato=iva_imponibile_scontato-" & Replace(aumento_iva_percentuale, ",", ".") & onere_prepagato & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_onere & "'", Dbc)
                Cmd.ExecuteNonQuery()
            End If

            Dim update_commissioni As String = ""

            'AGGIORNO LA RIGA DEL TOTALE AGGIUNGENDO IL COSTO DELL'ACCESSORIO E DELL'EVENTUALE AUMENTO DELL'ONERE PERCENTUALE
            Dim totale_prepagato As String = ""
            If imposta_prepagato Then
                'SE SI STA IMPOSTANDO IL TOTALE COME PREPAGATO ALLORA E' NECESSARIO AGGIORNARE IL TOTALE AGGIUNGENDO IL COSTO DELL'ELEMENTO
                totale_prepagato = ", imponibile_scontato_prepagato=imponibile_scontato_prepagato-" & Replace(imponibile_elemento, ",", ".") & ", iva_imponibile_scontato_prepagato=iva_imponibile_scontato_prepagato-" & Replace(iva_elemento, ",", ".") & ",imponibile_onere_prepagato=imponibile_onere_prepagato-" & Replace(aumento_imponibile_percentuale, ",", ".") & ", iva_onere_prepagato=iva_onere_prepagato-" & Replace(aumento_iva_percentuale, ",", ".")
            End If
            If elemento_commissionabile Then
                'SE L'ELEMENTO E' COMMISSIONABILE AUMENTO, NEL TOTALE, I VALORI DELLE COMMISSIONI
                update_commissioni = ", commissioni_imponibile_originale=ISNULL(commissioni_imponibile_originale,0)-" & Replace(imponibile_elemento * CDbl(percentuale_commissione) / 100, ",", ".") &
                    ", commissioni_iva_originale=ISNULL(commissioni_iva_originale,0)-" & Replace(iva_elemento * CDbl(percentuale_commissione) / 100, ",", ".")
            End If
            Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET imponibile=imponibile-" & Replace(imponibile_elemento, ",", ".") & ", imponibile_scontato=imponibile_scontato-" & Replace(imponibile_elemento, ",", ".") & ", iva_imponibile_scontato=iva_imponibile_scontato-" & Replace(iva_elemento, ",", ".") & ",iva_imponibile=iva_imponibile-" & Replace(iva_elemento, ",", ".") & ",imponibile_onere=imponibile_onere-" & Replace(aumento_imponibile_percentuale, ",", ".") & ", iva_onere=iva_onere-" & Replace(aumento_iva_percentuale, ",", ".") & ", valore_costo=valore_costo-" & Replace(imponibile_elemento + aumento_imponibile_percentuale + iva_elemento + aumento_iva_percentuale, ",", ".") & totale_prepagato & update_commissioni & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND ordine_stampa='6'", Dbc)
            Cmd.ExecuteNonQuery()

            If elemento_commissionabile Then
                update_commissioni = ", commissioni_imponibile_originale=ISNULL(imponibile_scontato,0)*" & Replace(CDbl(percentuale_commissione), ",", ".") & "/100," &
                "commissioni_iva_originale=ISNULL(iva_imponibile_scontato,0)*" & Replace(CDbl(percentuale_commissione), ",", ".") & "/100 "
            End If

            'AGGIORNO IL CAMPO 'SELEZIONATO' DELL'ELEMENTO SCELTO - SE L'ELEMENTO E' COMMISSIONABILE NE APPROFITTO PER SALVARE L'IMPORTO NEI CAMPI NECESSARI
            Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET selezionato='0' " & condizione_prepagato & update_commissioni & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND id_elemento='" & id_elemento & "'", Dbc)
            Cmd.ExecuteNonQuery()

            'AGGIORNO LO SCONTO 
            Cmd = New Data.SqlClient.SqlCommand("UPDATE " & tabella & " SET valore_costo=valore_costo-" & Replace(FormatNumber(sconto, 4, , , TriState.False), ",", ".") & ", imponibile=imponibile-" & Replace(FormatNumber(sconto, 4, , , TriState.False), ",", ".") & ", imponibile_scontato=imponibile_scontato-" & Replace(FormatNumber(sconto, 4, , , TriState.False), ",", ".") & " WHERE id_documento='" & id_da_salvare & "' AND num_calcolo='" & num_calcolo & "' AND id_gruppo='" & id_gruppo & "' AND ordine_stampa='5'", Dbc)
            Cmd.ExecuteNonQuery()

            'TEST SULL'ELEMENTO SCELTO - SE E' UN'ASSICURAZIONE DEVONO ESSERE RIMOSSE LE FRANCHIGIE E AGGIUNTE LE GENERICHE (informative)-------------------------------

            If tipologia_franchigia = "ASSICURAZIONE" Then
                aggiorna_franchigie(id_preventivo, id_ribaltamento, id_prenotazioni, id_contratto, num_calcolo, id_gruppo, sottotipologia_franchigia)
            End If
            '-----------------------------------------------------------------------------------------------------------------------------------

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End Sub

        'Inserita 19/06/2015
        Function NewCodiceEDP() As String
            Dim sqlStr As String

            Dim Dbc As Data.SqlClient.SqlConnection = Nothing
            Dim myTrans As Data.SqlClient.SqlTransaction = Nothing
            Dim Cmd As Data.SqlClient.SqlCommand = Nothing

            Dim NomeTabella As String = "codice_cliente"
            Dim NomeCampo As String = "codice_cliente"

            Try
                sqlStr = "SELECT TOP 1 " & NomeCampo & " FROM " & NomeTabella ' " ORDER BY " & NomeCampo
                'HttpContext.Current.Trace.Write("GeneraCodiceEDP: " & sqlStr)

                Dbc = New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                myTrans = Dbc.BeginTransaction()

                Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc, myTrans)
                Dim contatore As String = Cmd.ExecuteScalar & ""
                Cmd.Dispose()
                Cmd = Nothing

                If contatore <> "" Then
                    sqlStr = "DELETE FROM " & NomeTabella & " WHERE  " & NomeCampo & " = " & contatore
                    'HttpContext.Current.Trace.Write("GeneraCodiceEDP: " & sqlStr)

                    Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc, myTrans)

                    Cmd.ExecuteNonQuery()
                End If

                myTrans.Commit()

                NewCodiceEDP = contatore

            Catch ex As Exception
                myTrans.Rollback()
                NewCodiceEDP = "-1"
                'HttpContext.Current.Trace.Write("GeneraCodiceEDP: Rollback " & ex.Message)
            Finally
                If Not Cmd Is Nothing Then
                    Cmd.Dispose()
                    Cmd = Nothing
                End If

                If Not myTrans Is Nothing Then
                    myTrans.Dispose()
                    myTrans = Nothing
                End If

                If Not Dbc Is Nothing Then
                    Dbc.Close()
                    Dbc.Dispose()
                    Dbc = Nothing
                End If
            End Try
        End Function



    '######## ----------------------------------------------------------------------------------------------
    'Funzioni Pubbliche

    Public Function HelloWorld() As String
        Return "Hello World in Produzione"
    End Function

    ' _
    'Public Function Somma(ByVal num1 As Short, ByVal num2 As Short) As Short
    '    Return num1 + num2
    'End Function


    Public Function getListaStazioni() As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim sqlStr As String = "SELECT nome_stazione + ' - ' + indirizzo AS stazione, id, attiva FROM stazioni WHERE (id > 1) AND (attiva = 'True') "
            'Return sqlStr

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()


            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            Do While Rs.Read()
                dictionary = New Dictionary(Of String, String)(1)
                dictionary.Add("id", Rs("id"))
                dictionary.Add("stazione", Rs("stazione"))
                'dictionary.Add("conducente", Rs("cognome_primo_conducente") + " " + Rs("nome_primo_conducente"))
                'dictionary.Add("targa", Rs("targa"))

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

            dictionary.Add("id", "-1")
            'dictionary.Add("stazione", "")
            dictionary.Add("stazione", Left(ex.Message.ToString, 100))  'SOLO X TEST
            'dictionary.Add("conducente", "")
            'dictionary.Add("targa", "")

            list.Add(dictionary)
            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
            'Return "Vuota"
        End Try


    End Function



    Public Function getListaStazioni2() As String
        Dim xmlres As String = ""
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim sqlStr As String = "SELECT nome_stazione + ' - ' + indirizzo AS stazione, id, attiva FROM stazioni WHERE (id > 1) AND (attiva = 'True') "
            'Return sqlStr

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()


            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)


            Do While Rs.Read()
                'dictionary = New Dictionary(Of String, String)(1)
                'dictionary.Add("id", Rs("id"))
                'dictionary.Add("stazione", Rs("stazione"))
                'dictionary.Add("conducente", Rs("cognome_primo_conducente") + " " + Rs("nome_primo_conducente"))
                'dictionary.Add("targa", Rs("targa"))
                If xmlres = "" Then
                    xmlres = "nessuna"
                Else
                    xmlres += "," & Rs!stazione.ToString
                End If
                'list.Add(dictionary)
            Loop

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            'Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return xmlres   '(j.Serialize(list.ToArray()))

        Catch ex As Exception
            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            ' dictionary.Add("id", "-1")
            'dictionary.Add("stazione", "")
            ' dictionary.Add("stazione", Left(ex.Message.ToString, 100))  'SOLO X TEST
            'dictionary.Add("conducente", "")
            'dictionary.Add("targa", "")


            xmlres = Left(ex.Message.ToString, 100)
            'list.Add(dictionary)
            'Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return xmlres '(j.Serialize(list.ToArray()))
            'Return "Vuota"
        End Try
    End Function




    Public Function getListaTipClienti() As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim sqlStr As String = "SELECT descrizione, id FROM clienti_tipologia WITH(NOLOCK) "
            'Return sqlStr

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()


            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            Do While Rs.Read()
                dictionary = New Dictionary(Of String, String)(1)
                dictionary.Add("id", Rs("id"))
                dictionary.Add("descrizione", Rs("descrizione"))
                'dictionary.Add("conducente", Rs("cognome_primo_conducente") + " " + Rs("nome_primo_conducente"))
                'dictionary.Add("targa", Rs("targa"))

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

            dictionary.Add("id", "-1")
            dictionary.Add("descrizione", "")
            'dictionary.Add("conducente", "")
            'dictionary.Add("targa", "")

            list.Add(dictionary)
            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
            'Return "Vuota"
        End Try
    End Function


    Public Function getStopSale(ByVal data_inizio_noleggio As String) As String
        Try
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim sqlStr As String = "SELECT stop_sell.id as id_stop_sell,* FROM stop_sell LEFT JOIN stop_sell_fonti ON stop_sell.id = stop_sell_fonti.id_stop_sell LEFT JOIN clienti_tipologia ON stop_sell_fonti.id_fonte = clienti_tipologia.id WHERE '" & data_inizio_noleggio & "'  BETWEEN da_data AND a_data"
            'Return sqlStr

            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Cmd.ExecuteNonQuery()
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()


            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            Do While Rs.Read()
                dictionary = New Dictionary(Of String, String)(1)
                dictionary.Add("id", Rs("id"))
                dictionary.Add("stazione", Rs("stazione"))
                'dictionary.Add("conducente", Rs("cognome_primo_conducente") + " " + Rs("nome_primo_conducente"))
                'dictionary.Add("targa", Rs("targa"))

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

            dictionary.Add("id", "-1")
            dictionary.Add("stazione", "")
            'dictionary.Add("conducente", "")
            'dictionary.Add("targa", "")

            list.Add(dictionary)
            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
            'Return "Errore"
        End Try
    End Function


    Public Function getElencoVeicoli(ByVal stazione_inizio_noleggio As String, ByVal data_inizio_noleggio As String, ByVal ora_inizio_noleggio As String, ByVal stazione_fine_noleggio As String, ByVal data_fine_noleggio As String, ByVal ora_fine_noleggio As String, ByVal eta As String, ByVal id_tip_cliente As Integer, ByVal lingua As String, ByVal cod_promo As String) As String

        Dim QueryError As String = ""
        Dim contError As Integer = 0
        Dim sqlstr As String = ""
        Dim result As String = ""
        Try
            '-- Inzio pulizia tabella Preventi_web e preventivi_web_costi
            Dim UltimoIdGiornata As String
            Dim SqlPulisciDB As String = "SELECT * FROM preventivi_web  WITH(NOLOCK)  WHERE (data_creazione  < convert(datetime, convert(varchar(20), year(getdate())) + '-' + convert(varchar(20), month(getdate())) + '-' + convert(varchar(20), day(getdate())), 102)) order by id desc"

            Dim DbcPulisciDB As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            DbcPulisciDB.Open()

            Dim CmdPulisciDB = New Data.SqlClient.SqlCommand(SqlPulisciDB, DbcPulisciDB)

            Dim RsPulisciDB As Data.SqlClient.SqlDataReader
            RsPulisciDB = CmdPulisciDB.ExecuteReader()

            RsPulisciDB.Read()
            If RsPulisciDB.HasRows Then
                UltimoIdGiornata = RsPulisciDB(0)

                Dim SqlPulisciDB3 As String = "DELETE FROM preventivi_web_costi WHERE (id_documento < =" & UltimoIdGiornata & ")"
                QueryError = SqlPulisciDB3

                Dim DbcPulisciDB3 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                DbcPulisciDB3.Open()

                Dim CmdPulisciDB3 = New Data.SqlClient.SqlCommand(SqlPulisciDB3, DbcPulisciDB3)


                CmdPulisciDB3.ExecuteNonQuery()

                CmdPulisciDB3.Dispose()
                CmdPulisciDB3 = Nothing

                DbcPulisciDB3.Close()
                DbcPulisciDB3.Dispose()
                DbcPulisciDB3 = Nothing
            End If

            CmdPulisciDB.Dispose()
            CmdPulisciDB = Nothing

            DbcPulisciDB.Close()
            DbcPulisciDB.Dispose()
            DbcPulisciDB = Nothing
            contError = 100
            Dim SqlPulisciDB2 As String = "delete FROM preventivi_web  WHERE (data_creazione  < convert(datetime, convert(varchar(20), year(getdate())) + '-' + convert(varchar(20), month(getdate())) + '-' + convert(varchar(20), day(getdate())), 102))"
            QueryError = SqlPulisciDB2

            Dim DbcPulisciDB2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            DbcPulisciDB2.Open()

            Dim CmdPulisciDB2 = New Data.SqlClient.SqlCommand(SqlPulisciDB2, DbcPulisciDB2)

            CmdPulisciDB2.ExecuteNonQuery()

            CmdPulisciDB2.Dispose()
            CmdPulisciDB2 = Nothing

            DbcPulisciDB2.Close()
            DbcPulisciDB2.Dispose()
            DbcPulisciDB2 = Nothing

            '-- FINE pulizia tabelle

            Dim StazioneInStopSale As Boolean = False
            Dim Query As String = ""
            Dim controllo_Gruppo As String = ""
            Dim controllo_Stazione As String = ""

            Dim stazione2 As String = stazione_inizio_noleggio
            Dim data_inizio_completa As String = data_inizio_noleggio
            'Dim dtinisql As String = Right(data_inizio_completa, 4) & "-" & Mid(data_inizio_completa, 4, 2) & "-" & Left(data_inizio_completa, 2)
            Dim dtinisql As String = Year(data_inizio_completa) & "-" & Month(data_inizio_completa) & "-" & Day(data_inizio_completa) '& " 00:00:00"

            Dim ora_uscita_da_splittare As String = ora_inizio_noleggio
            Dim ora_uscita_splittata As String() = ora_uscita_da_splittare.Split(":")
            Dim ora_inizio2 As String = ora_uscita_splittata(0)
            Dim minuti_inizio2 As String = ora_uscita_splittata(1)

            Dim stazione_off2 As String = stazione_fine_noleggio
            Dim data_fine_completa As String = data_fine_noleggio
            'Dim dtendsql As String = Right(data_inizio_completa, 4) & "-" & Mid(data_inizio_completa, 4, 2) & "-" & Left(data_inizio_completa, 2)
            Dim dtendsql As String = Year(data_fine_completa) & "-" & Month(data_fine_completa) & "-" & Day(data_fine_completa) '& " 23:59:00"

            Dim ora_rientro_da_splittare As String = ora_fine_noleggio
            Dim ora_rientro_splittata As String() = ora_rientro_da_splittare.Split(":")
            Dim ora_fine2 As String = ora_rientro_splittata(0)
            Dim minuti_fine2 As String = ora_rientro_splittata(1)

            Dim numero_giorni As Integer = funzioni_comuni.getGiorniDiNoleggio(data_inizio_completa, data_fine_completa, ora_uscita_splittata(0), ora_uscita_splittata(1), ora_rientro_splittata(0), ora_rientro_splittata(1), "8")

            Dim etaConducente As String = eta

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()


            Dim sqla As String = "" '"SELECT stop_sell.id as id_stop_sell,* FROM stop_sell LEFT JOIN stop_sell_fonti ON stop_sell.id = stop_sell_fonti.id_stop_sell LEFT JOIN clienti_tipologia ON stop_sell_fonti.id_fonte = clienti_tipologia.id WHERE (convert(date,'2020-10-26',102) BETWEEN da_data AND a_data) or (convert(date,'2020-10-26',102) < da_data and convert(date,'2020-10-27',102) >= da_data)"
            sqla = "SELECT stop_sell.id as id_stop_sell,* FROM stop_sell LEFT JOIN stop_sell_fonti ON stop_sell.id = stop_sell_fonti.id_stop_sell "
            sqla += "LEFT JOIN clienti_tipologia ON stop_sell_fonti.id_fonte = clienti_tipologia.id WHERE (convert(date,'" & dtinisql & "',102) "
            sqla += "BETWEEN da_data And a_data) Or (convert(date,'" & dtinisql & "',102) < da_data and convert(date,'" & dtendsql & "',102) >= da_data)"

            'Dim Cmd As New Data.SqlClient.SqlCommand("SELECT stop_sell.id as id_stop_sell,* FROM stop_sell LEFT JOIN stop_sell_fonti ON stop_sell.id = stop_sell_fonti.id_stop_sell LEFT JOIN clienti_tipologia ON stop_sell_fonti.id_fonte = clienti_tipologia.id WHERE (convert(date,'" & dtinisql & "',102)  BETWEEN da_data AND a_data) or (convert(date,'" & dtinisql & "',102) < da_data and convert(date,'" & dtendsql & "',102) >= da_data)", Dbc)
            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)
            'QueryError = "stop_sell: --> " & sqla
            contError = 1

            Dim Rs As Data.SqlClient.SqlDataReader

            Dim stazione_aperta_pickup As String = funzioni_comuni.stazione_aperta_pick_upWebSevice(stazione2, data_inizio_completa, ora_inizio2, minuti_inizio2)
            contError = 10
            Dim stazione_aperta_dropOff As String = funzioni_comuni.stazione_aperta_drop_offWebSevice(stazione_off2, data_fine_completa, ora_fine2, minuti_fine2)

            QueryError = "Stazione OUT:" & stazione_aperta_pickup & " StazioneIN: " & stazione_aperta_dropOff
            contError = 11

            If stazione_aperta_pickup = "0" Or stazione_aperta_pickup = "1" Or stazione_aperta_dropOff = "0" Or stazione_aperta_dropOff = "1" Then

                Return "Errore Stazione Chiusa o non configurata"

            Else

                'QueryError = "IN Else"
                Dim stazione_off As String = stazione_fine_noleggio
                'Dim response_VAL As Boolean
                'Dim response_VAL_OFF As Boolean
                Dim eta_x_young As String = eta
                Dim stazione As String = stazione_inizio_noleggio

                '--------------------------------------------------------------------------------------------------
                Dim tutte_stazioni As String = "False"
                Dim tutte_fonti As String = ""
                Dim tutti_gruppi As String = ""
                Dim id_stop_sell As String = ""
                '--------------------------------------------------------------------------------------------------                

                contError = 3

                Rs = Cmd.ExecuteReader()

                Dim fonte As String = ""

                contError = 40 '& " - " & Cmd.CommandText.ToString

                Do While Rs.Read()

                    contError = 41

                    QueryError = "IN Do While"
                    If Rs("tutte_stazioni") = "True" Then
                        fonte = Rs("id_fonte") & ""
                        If Rs("id_fonte") & "" = "3" Then

                            id_stop_sell = Rs("id_stop_sell") & ""

                            tutte_stazioni = Rs("tutte_stazioni") & ""
                            tutti_gruppi = Rs("tutti_gruppi") & ""
                        End If

                        If Rs("tutte_fonti") <> "False" And fonte = "" Then
                            id_stop_sell = Rs("id_stop_sell") & ""

                            tutte_stazioni = Rs("tutte_stazioni") & ""
                            tutti_gruppi = Rs("tutti_gruppi") & ""
                        End If
                        contError = 42
                    Else
                        contError = 421
                        Dim Dbc2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        Dbc2.Open()
                        contError = 422
                        sqlstr = "SELECT stop_sell.id as id_stop_sell,stop_sell.*,stop_sell_stazioni.id_stazione "
                        sqlstr += "FROM stop_sell,stop_sell_stazioni WHERE (stop_sell.id = stop_sell_stazioni.id_stop_sell) "
                        sqlstr += "And ((convert(date,'" & dtinisql & "',102)  BETWEEN da_data AND a_data) "
                        sqlstr += "Or (convert(date,'" & dtinisql & "',102) < da_data and convert(date,'" & dtendsql & "',102) >= da_data)) "
                        sqlstr += "And (id_stazione = " & stazione2 & ")"

                        Dim Cmd2 As New Data.SqlClient.SqlCommand(sqlstr, Dbc2)
                        QueryError = sqlstr 'Cmd2.CommandText
                        contError = 43
                        Dim Rs2 As Data.SqlClient.SqlDataReader
                        Rs2 = Cmd2.ExecuteReader()
                        Rs2.Read()
                        If Rs2.HasRows Then
                            If stazione2 = Rs2("id_stazione") Then
                                id_stop_sell = Rs2("id_stop_sell") & ""

                                tutte_stazioni = Rs2("tutte_stazioni") & ""
                                tutti_gruppi = Rs2("tutti_gruppi") & ""
                            End If
                        End If
                        contError = 44
                    End If
                Loop
                contError = 45
                'Rs.Read()
                'If Rs.HasRows Then

                '    fonte = Rs("id_fonte") & ""
                '    If Rs("id_fonte") & "" = "3" Then


                '        id_stop_sell = Rs("id_stop_sell") & ""

                '        tutte_stazioni = Rs("tutte_stazioni") & ""
                '        tutti_gruppi = Rs("tutti_gruppi") & ""
                '    End If

                '    If Rs("tutte_fonti") <> "False" And fonte = "" Then
                '        id_stop_sell = Rs("id_stop_sell") & ""


                '        tutte_stazioni = Rs("tutte_stazioni") & ""
                '        tutti_gruppi = Rs("tutti_gruppi") & ""

                '    End If

                'End If

                Rs.Close()

                Cmd.Dispose()
                Dbc.Dispose()
                Dbc.Close()

                contError = 51
                '-----------------------------------------------------------------------------------------------                                             
                Dim list As New List(Of Dictionary(Of String, String))()
                Dim dictionary As New Dictionary(Of String, String)(1)


                If id_stop_sell <> "" And tutte_stazioni = "True" And tutti_gruppi = "True" Then
                    dictionary = New Dictionary(Of String, String)(1)
                    dictionary.Add("Stazione in STOP SALE", "Lista Vuota")
                    list.Add(dictionary)

                Else
                    If id_stop_sell <> "" And tutti_gruppi = "True" Then
                        Dim DbcStopSale As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        DbcStopSale.Open()
                        Dim sqlStrStopSale As String = "select id_stazione from stop_sell_stazioni where id_stop_sell = " & id_stop_sell
                        QueryError = sqlStrStopSale
                        'Return sqlStrStopSale

                        Dim CmdStopSale As New Data.SqlClient.SqlCommand(sqlStrStopSale, DbcStopSale)
                        CmdStopSale.ExecuteNonQuery()
                        Dim RsStopSale As Data.SqlClient.SqlDataReader
                        RsStopSale = CmdStopSale.ExecuteReader()

                        Do While RsStopSale.Read()
                            If RsStopSale("id_stazione") = stazione Then
                                StazioneInStopSale = True
                            End If
                        Loop

                        CmdStopSale.Dispose()
                        CmdStopSale = Nothing
                        DbcStopSale.Close()
                        DbcStopSale.Dispose()
                        DbcStopSale = Nothing

                        If StazioneInStopSale Then
                            dictionary = New Dictionary(Of String, String)(1)
                            dictionary.Add("Stazione in STOP SALE", "Lista Vuota")
                            list.Add(dictionary)
                        End If
                    End If

                    If StazioneInStopSale = False Then
                        '22-02-2018
                        If id_stop_sell <> "" Then
                            controllo_Gruppo = ""
                            Dim DbcStopSale As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                            DbcStopSale.Open()
                            sqlstr = "SELECT stop_sell.id as id_stop_sell,* FROM stop_sell LEFT JOIN stop_sell_fonti ON stop_sell.id = stop_sell_fonti.id_stop_sell "
                            sqlstr += "LEFT JOIN clienti_tipologia On stop_sell_fonti.id_fonte = clienti_tipologia.id "
                            sqlstr += "WHERE (convert(date,'" & dtinisql & "',102)  BETWEEN da_data AND a_data) "
                            sqlstr += "Or (convert(date,'" & dtinisql & "',102) < da_data and convert(date,'" & dtendsql & "',102) >= da_data)"

                            Dim CmdStopSale As New Data.SqlClient.SqlCommand(sqlstr, DbcStopSale)

                            Dim RsStopSale As Data.SqlClient.SqlDataReader
                            RsStopSale = CmdStopSale.ExecuteReader()
                            Do While RsStopSale.Read()
                                controllo_Gruppo = controllo_Gruppo & " AND Gruppi.ID_gruppo not in (SELECT id_gruppo FROM stop_sell_gruppi INNER JOIN stop_sell_stazioni ON stop_sell_gruppi.id_stop_sell = stop_sell_stazioni.id_stop_sell WHERE id_stazione = '" & stazione & "' AND stop_sell_gruppi.id_stop_sell='" & RsStopSale("id_stop_sell") & "') "
                                ''Response.Write(controllo_Gruppo)
                                'Response.End()
                                'End If
                            Loop
                            RsStopSale.Close()

                            CmdStopSale.Dispose()
                            DbcStopSale.Dispose()
                            DbcStopSale.Close()

                            'controllo_Gruppo = control_gruppi(id_stop_sell, stazione, data_inizio_completa) & ""
                            ''Response.Write(controllo_Gruppo)
                            '  Response.End()
                            ' End If
                        End If

                        'Modifica 13/08/2015
                        Dim id_tariffe_righe As String = get_tariffa_x_righe(data_inizio_completa, data_fine_completa, numero_giorni, stazione_inizio_noleggio, stazione_fine_noleggio, id_tip_cliente, cod_promo)
                        Dim id_tariffe_righe_prep As String = get_tariffa_x_righe_prepagato(data_inizio_completa, data_fine_completa, numero_giorni, stazione_inizio_noleggio, stazione_fine_noleggio, id_tip_cliente, cod_promo)

                        Dim NumMinGiornoNoleggio, NumMaxGiornoNoleggio As Integer

                        'Modifica 14-02-2016 Inizio
                        Dim SqlNumGiorniMax As String = "select tariffe_righe.max_giorni_nolo from tariffe_righe WITH(NOLOCK) where id =" & id_tariffe_righe
                        QueryError = SqlNumGiorniMax

                        Dim DbcNumGiorniMax As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        DbcNumGiorniMax.Open()

                        Dim CmdNumGiorniMax = New Data.SqlClient.SqlCommand(SqlNumGiorniMax, DbcNumGiorniMax)

                        Dim RsNumGiorniMax As Data.SqlClient.SqlDataReader
                        RsNumGiorniMax = CmdNumGiorniMax.ExecuteReader()

                        RsNumGiorniMax.Read()
                        If RsNumGiorniMax.HasRows Then
                            If RsNumGiorniMax(0) & "" = "" Then
                                NumMaxGiornoNoleggio = numero_giorni
                            Else
                                NumMaxGiornoNoleggio = RsNumGiorniMax(0)
                            End If
                        End If

                        'Modifica 14-02-2016 Fine



                        Dim SqlNumGiorniMin As String = "select tariffe_righe.min_giorni_nolo from tariffe_righe WITH(NOLOCK) where id =" & id_tariffe_righe

                        Dim DbcNumGiorniMin As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        DbcNumGiorniMin.Open()

                        Dim CmdNumGiorniMin = New Data.SqlClient.SqlCommand(SqlNumGiorniMin, DbcNumGiorniMin)

                        Dim RsNumGiorniMin As Data.SqlClient.SqlDataReader
                        RsNumGiorniMin = CmdNumGiorniMin.ExecuteReader()

                        RsNumGiorniMin.Read()
                        If RsNumGiorniMin.HasRows Then
                            If RsNumGiorniMin(0) & "" = "" Then
                                NumMinGiornoNoleggio = 0
                            Else
                                NumMinGiornoNoleggio = RsNumGiorniMin(0)
                            End If

                            If numero_giorni < NumMinGiornoNoleggio Then
                                dictionary = New Dictionary(Of String, String)(1)
                                dictionary.Add("Necessario Num Min Giorni di Noleggio", "Lista Vuota")
                                list.Add(dictionary)

                                CmdNumGiorniMin.Dispose()
                                CmdNumGiorniMin = Nothing

                                DbcNumGiorniMin.Close()
                                DbcNumGiorniMin.Dispose()
                                DbcNumGiorniMin = Nothing
                            ElseIf numero_giorni > NumMaxGiornoNoleggio Then
                                dictionary = New Dictionary(Of String, String)(1)
                                dictionary.Add("Superato Num Max Giorni di Noleggio", "Lista Vuota")
                                list.Add(dictionary)

                                CmdNumGiorniMax.Dispose()
                                CmdNumGiorniMax = Nothing

                                DbcNumGiorniMax.Close()
                                DbcNumGiorniMax.Dispose()
                                DbcNumGiorniMax = Nothing
                            Else
                                Query = "SELECT GRUPPI.ID_gruppo, GRUPPI.cod_gruppo, GRUPPI.CodDollar, GRUPPI.CodThrifty, Gruppi.permetti_joung_driver,Gruppi.eta_max_joung_driver ,Gruppi.eta_min_joung_driver ,GRUPPI.ETA_MINIMA,GRUPPI.ETA_MASSIMA, MODELLI.bag_max, GRUPPI.descrizione, MODELLI.note,MODELLI.note_eng, GRUPPI.nome_immagine, GRUPPI.classe, MODELLI.descrizione AS descrizione_modelli, MODELLI.cavalli, MODELLI.KW, MODELLI.capacita_serbatoio, MODELLI.note AS note_modelli, MODELLI.TipoCarburante, MODELLI.num_porte, MODELLI.num_posti, MODELLI.cambio_automatico, MODELLI.Euro, MODELLI.cod_mod, alimentazione.descrizione AS nome_carburante, GRUPPI.ID_MODELLO FROM alimentazione INNER JOIN MODELLI ON alimentazione.id = MODELLI.TipoCarburante RIGHT OUTER JOIN GRUPPI ON MODELLI.ID_MODELLO = GRUPPI.ID_MODELLO"
                                Query = Query & "  WHERE (GRUPPI.attivo = 'True') AND  Gruppi.ID_gruppo not in (SELECT id_gruppo FROM stazioni_gruppi_non_prenotabili WHERE id_stazione = '" & stazione2 & "' AND tipo = 'PICK') "
                                Query = Query & controllo_Gruppo & ""
                                '07 Marzo 2018
                                If tutte_stazioni Then
                                    Query = Query & " AND Gruppi.ID_gruppo not in (SELECT id_gruppo FROM stop_sell_gruppi  WHERE  stop_sell_gruppi.id_stop_sell='" & id_stop_sell & "') "
                                End If
                                Query = Query & " AND not (permetti_joung_driver='False' AND (" & CInt(eta) & " < ETA_MINIMA OR " & CInt(eta) & " > ETA_MASSIMA )) "
                                Query = Query & " AND not (permetti_joung_driver = 'True' AND (" & CInt(eta) & " < eta_min_joung_driver OR " & CInt(eta) & " > eta_max_joung_driver )) "
                                If stazione2 <> stazione_off2 Then
                                    Query = Query & " AND Gruppi.ID_gruppo not in ( SELECT id_gruppo FROM stazioni_gruppi_non_prenotabili WHERE id_stazione = '" & stazione2 & "' AND tipo = 'VAL_PICK')"
                                    Query = Query & " AND Gruppi.ID_gruppo not in ( SELECT id_gruppo FROM stazioni_gruppi_non_prenotabili WHERE id_stazione ='" & stazione_off2 & "' AND tipo = 'VAL')"

                                End If

                                Query = Query & " ORDER BY GRUPPI.cod_gruppo"


                                Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                                Dbc2.Open()
                                sqlstr = Query
                                QueryError = sqlstr
                                'test
                                'Return sqlstr
                                'Exit Function
                                'test


                                Dim Cmd2 As New Data.SqlClient.SqlCommand(sqlstr, Dbc2)
                                Cmd2.ExecuteNonQuery()
                                Dim Rs2 As Data.SqlClient.SqlDataReader
                                Rs2 = Cmd2.ExecuteReader()


                                Do While Rs2.Read()
                                    dictionary = New Dictionary(Of String, String)(1)

                                    dictionary.Add("ID_gruppo", Rs2("ID_gruppo"))
                                    dictionary.Add("cod_gruppo", Rs2("cod_gruppo"))
                                    dictionary.Add("permetti_joung_driver", Rs2("permetti_joung_driver"))
                                    dictionary.Add("eta_max_joung_driver", Rs2("eta_max_joung_driver"))
                                    dictionary.Add("eta_min_joung_driver", Rs2("eta_min_joung_driver"))
                                    dictionary.Add("ETA_MINIMA", Rs2("ETA_MINIMA"))
                                    dictionary.Add("ETA_MASSIMA", Rs2("ETA_MASSIMA"))
                                    dictionary.Add("bag_max", Rs2("bag_max") & "")
                                    dictionary.Add("descrizione", Rs2("descrizione") & "")
                                    If lingua = "ITA" Then
                                        dictionary.Add("note", Rs2("note") & "")
                                    Else
                                        dictionary.Add("note", Rs2("note_eng") & "")
                                    End If
                                    dictionary.Add("nome_immagine", Rs2("nome_immagine") & "")
                                    dictionary.Add("classe", Rs2("classe") & "")
                                    dictionary.Add("descrizione_modelli", Rs2("descrizione_modelli") & "")
                                    dictionary.Add("cavalli", Rs2("cavalli") & "")
                                    dictionary.Add("KW", Rs2("KW") & "")
                                    dictionary.Add("capacita_serbatoio", Rs2("capacita_serbatoio") & "")
                                    dictionary.Add("note_modelli", Rs2("note_modelli") & "")
                                    dictionary.Add("TipoCarburante", Rs2("TipoCarburante") & "")
                                    dictionary.Add("num_porte", Rs2("num_porte") & "")
                                    dictionary.Add("num_posti", Rs2("num_posti") & "")
                                    dictionary.Add("cambio_automatico", Rs2("cambio_automatico") & "")
                                    dictionary.Add("Euro", Rs2("Euro") & "")
                                    dictionary.Add("cod_mod", Rs2("cod_mod") & "")
                                    dictionary.Add("nome_carburante", Rs2("nome_carburante") & "")
                                    dictionary.Add("ID_MODELLO", Rs2("ID_MODELLO"))
                                    dictionary.Add("Codice Promozione", cod_promo & "")

                                    'Inzio 04/06/2015               
                                    'Dim id_tariffe_righe As String = get_tariffa_x_righe(data_inizio_completa, data_fine_completa, numero_giorni, stazione_inizio_noleggio, stazione_fine_noleggio, id_tip_cliente, cod_promo)
                                    'Dim id_tariffe_righe_prep As String = get_tariffa_x_righe_prepagato(data_inizio_completa, data_fine_completa, numero_giorni, stazione_inizio_noleggio, stazione_fine_noleggio, id_tip_cliente, cod_promo)

                                    'dictionary = New Dictionary(Of String, String)(1)
                                    dictionary.Add("ID_TARIFFA_RIGHE", id_tariffe_righe)
                                    'list.Add(dictionary)

                                    'dictionary = New Dictionary(Of String, String)(1)
                                    dictionary.Add("ID_TARIFFA_RIGHE_PREPAGATO", id_tariffe_righe_prep)
                                    'list.Add(dictionary)

                                    Dim id_gruppo_rack As String = ""
                                    Dim giorni_prepagati_x_modifica As String = "0"
                                    Dim prepagata As Double = False
                                    Dim prepagata_si As Double = True
                                    Dim giorni_noleggio_extra_rack As Integer = 0
                                    Dim tipo_Sconto As String = "0"
                                    Dim sconto As Double = 0
                                    Dim sconto_web_prepagato_primo_calcolo As Double = 0
                                    Dim sconto_su_rack As Double = 0
                                    Dim id_ribaltamento As String = ""
                                    Dim id_prenotazione As String = ""
                                    Dim id_contratto As String = ""
                                    Dim num_calcolo As String = "1"
                                    Dim id_utente As String = "5"
                                    Dim id_ditta As String = ""
                                    Dim id_fonte_commissionabile As String = ""
                                    Dim commissione_percentuale As String = ""
                                    Dim tipo_commissione As String = ""
                                    Dim primo_calcolo_commissione As Boolean = True
                                    Dim giorni_commissioni_originale As String = ""

                                    Dim Id_nuovo_preventivo As String
                                    Dim Id_nuovo_preventivo_prepagato As String

                                    If (id_tariffe_righe & "") <> "" Or (id_tariffe_righe_prep & "") <> "" Then


                                        ''TEST - rimossi x verificare tempi risposta 10.06.2021
                                        'se chiamata da home page crea IDPreventivo
                                        If Session("chiamata_sito_web") = 1 Then
                                            Id_nuovo_preventivo = nuovo_preventivo()
                                            Id_nuovo_preventivo_prepagato = nuovo_preventivo_prepagato()

                                        End If
                                        If Session("chiamata_sito_web") = 2 Then
                                            Id_nuovo_preventivo = TextBox3.Text
                                            Id_nuovo_preventivo_prepagato = "10016360"
                                        End If


                                        calcolaTariffa_x_gruppo(stazione2, data_inizio_completa, ora_inizio2, minuti_inizio2, stazione_off2, id_tariffe_righe, Rs2("ID_gruppo"), id_gruppo_rack, numero_giorni, giorni_prepagati_x_modifica, prepagata, giorni_noleggio_extra_rack, sconto, tipo_Sconto, sconto_web_prepagato_primo_calcolo, sconto_su_rack, etaConducente, etaConducente, Id_nuovo_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_utente, id_ditta, id_fonte_commissionabile, commissione_percentuale, tipo_commissione, primo_calcolo_commissione, giorni_commissioni_originale)

                                        calcolaTariffa_x_gruppo(stazione2, data_inizio_completa, ora_inizio2, minuti_inizio2, stazione_off2, id_tariffe_righe_prep, Rs2("ID_gruppo"), id_gruppo_rack, numero_giorni, giorni_prepagati_x_modifica, prepagata_si, giorni_noleggio_extra_rack, sconto, tipo_Sconto, sconto_web_prepagato_primo_calcolo, sconto_su_rack, etaConducente, etaConducente, Id_nuovo_preventivo_prepagato, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_utente, id_ditta, id_fonte_commissionabile, commissione_percentuale, tipo_commissione, primo_calcolo_commissione, giorni_commissioni_originale)

                                        If Session("chiamata_sito_web") = 2 Then
                                            Session("id_preventivo") = Id_nuovo_preventivo
                                            Session("id_preventivo_prepagato") = Id_nuovo_preventivo_prepagato
                                            Exit Function
                                        End If



                                        'Inzio 05/06/2015      
                                        dictionary.Add("Id Preventivo", Id_nuovo_preventivo)
                                        dictionary.Add("Id Preventivo Prepagato", Id_nuovo_preventivo_prepagato)
                                        dictionary.Add("Stazione Uscita", stazione2)
                                        dictionary.Add("Data PikUp", data_inizio_completa)
                                        dictionary.Add("Ora PikUp", ora_inizio2)
                                        dictionary.Add("Minuti PikUp", minuti_inizio2)
                                        dictionary.Add("Stazione Entrata", stazione_off2)
                                        dictionary.Add("Data DropOff", data_fine_completa)
                                        dictionary.Add("Ora DropOff", ora_fine2)
                                        dictionary.Add("Minuti DropOff", minuti_fine2)
                                        dictionary.Add("Tariffa Riga", id_tariffe_righe)
                                        dictionary.Add("Tariffa RigaPrepagata", id_tariffe_righe_prep)
                                        dictionary.Add("Id Gruppo", Rs2("ID_gruppo"))
                                        dictionary.Add("Id Gruppo Rack", id_gruppo_rack)
                                        dictionary.Add("Numero Giorni", numero_giorni)
                                        dictionary.Add("Età", etaConducente)
                                        dictionary.Add("Giorni Prepagata modifica", giorni_prepagati_x_modifica)

                                        dictionary.Add("Paga al Banco", Session("costo_banco"))
                                        dictionary.Add("Paga On-line", Session("costo_web"))

                                        'dictionary.Add("Paga al Banco", getPrezzo(Id_nuovo_preventivo, Rs2("ID_gruppo")))
                                        'dictionary.Add("Paga On-line", getPrezzo(Id_nuovo_preventivo_prepagato, Rs2("ID_gruppo")))


                                        'Fine 05/06/2015

                                    Else
                                        'dictionary = New Dictionary(Of String, String)(1)
                                        dictionary.Add("Entrato", "No")
                                        'list.Add(dictionary)
                                    End If
                                    list.Add(dictionary)
                                Loop
                                Cmd2.Dispose()
                                Cmd2 = Nothing
                                Dbc2.Close()
                                Dbc2.Dispose()
                                Dbc2 = Nothing

                                SqlConnection.ClearAllPools()


                            End If
                        End If
                        'Fine Modifica 13/08/2015
                    End If
                End If

                Dim j As JavaScriptSerializer = New JavaScriptSerializer()
                Return (j.Serialize(list.ToArray()))

                Label2.Text = Date.Now.Hour & ":" & Date.Now.Minute & ":" & Date.Now.Second & ":" & Date.Now.Millisecond


            End If
        Catch ex As Exception
            Return ex.Message & " QueryError: (" & contError & ")  " & QueryError
        End Try
    End Function


    Public Function getElencoVeicoli2(ByVal stazione_inizio_noleggio As String, ByVal data_inizio_noleggio As String, ByVal ora_inizio_noleggio As String, ByVal stazione_fine_noleggio As String, ByVal data_fine_noleggio As String, ByVal ora_fine_noleggio As String, ByVal eta As String, ByVal id_tip_cliente As Integer, ByVal lingua As String, ByVal cod_promo As String) As String
        Dim QueryError As String = ""
        Dim contError As Integer = 0
        Dim sqlstr As String = ""

        Dim result As String = ""


        Dim list As New List(Of Dictionary(Of String, String))()
        Dim dictionary As New Dictionary(Of String, String)(1)
        Dim stazione2 As String = stazione_inizio_noleggio
        Dim data_inizio_completa As String = data_inizio_noleggio
        'Dim dtinisql As String = Right(data_inizio_completa, 4) & "-" & Mid(data_inizio_completa, 4, 2) & "-" & Left(data_inizio_completa, 2)
        Dim dtinisql As String = Year(data_inizio_completa) & "-" & Month(data_inizio_completa) & "-" & Day(data_inizio_completa) '& " 00:00:00"

        Dim ora_uscita_da_splittare As String = ora_inizio_noleggio
        Dim ora_uscita_splittata As String() = ora_uscita_da_splittare.Split(":")
        Dim ora_inizio2 As String = ora_uscita_splittata(0)
        Dim minuti_inizio2 As String = ora_uscita_splittata(1)

        Dim stazione_off2 As String = stazione_fine_noleggio
        Dim data_fine_completa As String = data_fine_noleggio
        'Dim dtendsql As String = Right(data_inizio_completa, 4) & "-" & Mid(data_inizio_completa, 4, 2) & "-" & Left(data_inizio_completa, 2)
        Dim dtendsql As String = Year(data_fine_completa) & "-" & Month(data_fine_completa) & "-" & Day(data_fine_completa) '& " 23:59:00"

        Dim ora_rientro_da_splittare As String = ora_fine_noleggio
        Dim ora_rientro_splittata As String() = ora_rientro_da_splittare.Split(":")
        Dim ora_fine2 As String = ora_rientro_splittata(0)
        Dim minuti_fine2 As String = ora_rientro_splittata(1)

        Dim numero_giorni As Integer = funzioni_comuni.getGiorniDiNoleggio(data_inizio_completa, data_fine_completa, ora_uscita_splittata(0), ora_uscita_splittata(1), ora_rientro_splittata(0), ora_rientro_splittata(1), "8")

        Dim etaConducente As String = eta
        Try
            sqlstr = "SELECT GRUPPI.ID_gruppo, GRUPPI.cod_gruppo, GRUPPI.CodDollar, GRUPPI.CodThrifty, "
            sqlstr += "Gruppi.permetti_joung_driver,Gruppi.eta_max_joung_driver ,Gruppi.eta_min_joung_driver ,"
            sqlstr += "GRUPPI.ETA_MINIMA,GRUPPI.ETA_MASSIMA, MODELLI.bag_max, GRUPPI.descrizione, "
            sqlstr += "MODELLI.note,MODELLI.note_eng, GRUPPI.nome_immagine, GRUPPI.classe, "
            sqlstr += "MODELLI.descrizione AS descrizione_modelli, MODELLI.cavalli, MODELLI.KW, "
            sqlstr += "MODELLI.capacita_serbatoio, MODELLI.note AS note_modelli, MODELLI.TipoCarburante, "
            sqlstr += "MODELLI.num_porte, MODELLI.num_posti, MODELLI.cambio_automatico, MODELLI.Euro, "
            sqlstr += "MODELLI.cod_mod, alimentazione.descrizione AS nome_carburante, GRUPPI.ID_MODELLO "
            sqlstr += "FROM alimentazione INNER JOIN MODELLI ON alimentazione.id = MODELLI.TipoCarburante "
            sqlstr += "RIGHT OUTER JOIN GRUPPI ON MODELLI.ID_MODELLO = GRUPPI.ID_MODELLO  "
            sqlstr += "WHERE (GRUPPI.attivo = 'True') AND  Gruppi.ID_gruppo not in "
            sqlstr += "(SELECT id_gruppo FROM stazioni_gruppi_non_prenotabili WHERE id_stazione = '3' AND tipo = 'PICK')  "
            sqlstr += "AND Gruppi.ID_gruppo not in (SELECT id_gruppo FROM stop_sell_gruppi INNER JOIN stop_sell_stazioni "
            sqlstr += "ON stop_sell_gruppi.id_stop_sell = stop_sell_stazioni.id_stop_sell WHERE id_stazione = '3' "
            sqlstr += "AND stop_sell_gruppi.id_stop_sell='644')  AND not (permetti_joung_driver='False' "
            sqlstr += "AND (31 < ETA_MINIMA OR 31 > ETA_MASSIMA ))  AND not (permetti_joung_driver = 'True' "
            sqlstr += "AND (31 < eta_min_joung_driver OR 31 > eta_max_joung_driver ))  ORDER BY GRUPPI.cod_gruppo"

            Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc2.Open()


            Dim Cmd2 As New Data.SqlClient.SqlCommand(sqlstr, Dbc2)
            Cmd2.ExecuteNonQuery()
            Dim Rs2 As Data.SqlClient.SqlDataReader
            Rs2 = Cmd2.ExecuteReader()


            Do While Rs2.Read()

                result += "ID_gruppo" & ":" & Rs2("ID_gruppo")
                result += "cod_gruppo" & ":" & Rs2("cod_gruppo")
                result += "permetti_joung_driver" & ":" & Rs2("permetti_joung_driver")
                result += "eta_max_joung_driver" & ":" & Rs2("eta_max_joung_driver")
                result += "eta_min_joung_driver" & ":" & Rs2("eta_min_joung_driver")
                result += "ETA_MINIMA" & ":" & Rs2("ETA_MINIMA")
                result += "ETA_MASSIMA" & ":" & Rs2("ETA_MASSIMA")
                result += "bag_max" & ":" & Rs2("bag_max")
                result += "descrizione" & ":" & Rs2("descrizione")
                If lingua = "ITA" Then
                    result += "note" & ":" & Rs2("note")
                Else
                    result += "note" & ":" & Rs2("note_eng")
                End If
                result += "nome_immagine" & ":" & Rs2("nome_immagine")
                result += "classe" & ":" & Rs2("classe")
                result += "descrizione_modelli" & ":" & Rs2("descrizione_modelli")
                result += "cavalli" & ":" & Rs2("cavalli")
                result += "KW" & ":" & Rs2("KW")
                result += "capacita_serbatoio" & ":" & Rs2("capacita_serbatoio")
                result += "note_modelli" & ":" & Rs2("note_modelli")
                result += "TipoCarburante" & ":" & Rs2("TipoCarburante")
                result += "num_porte" & ":" & Rs2("num_porte")
                result += "num_posti" & ":" & Rs2("num_posti")
                result += "cambio_automatico" & ":" & Rs2("cambio_automatico")
                result += "Euro" & ":" & Rs2("Euro")
                result += "cod_mod" & ":" & Rs2("cod_mod")
                result += "nome_carburante" & ":" & Rs2("nome_carburante")
                result += "ID_MODELLO" & ":" & Rs2("ID_MODELLO")
                result += "Codice Promozione" & ":" & cod_promo

                'Inzio 04/06/2015               
                'Dim id_tariffe_righe As String = get_tariffa_x_righe(data_inizio_completa, data_fine_completa, numero_giorni, stazione_inizio_noleggio, stazione_fine_noleggio, id_tip_cliente, cod_promo)
                'Dim id_tariffe_righe_prep As String = get_tariffa_x_righe_prepagato(data_inizio_completa, data_fine_completa, numero_giorni, stazione_inizio_noleggio, stazione_fine_noleggio, id_tip_cliente, cod_promo)

                'dictionary = New Dictionary(Of String, String)(1)
                Dim id_tariffe_righe As String = "765"
                result += "ID_TARIFFA_RIGHE" & ":" & id_tariffe_righe
                'list.Add(dictionary)
                Dim id_tariffe_righe_prep As String = "766"
                'dictionary = New Dictionary(Of String, String)(1)
                result += "ID_TARIFFA_RIGHE_PREPAGATO" & ":" & id_tariffe_righe_prep
                'dictionary.Add("ID_TARIFFA_RIGHE_PREPAGATO", id_tariffe_righe_prep)
                'list.Add(dictionary)

                Dim id_gruppo_rack As String = ""
                Dim giorni_prepagati_x_modifica As String = "0"
                Dim prepagata As Double = False
                Dim prepagata_si As Double = True
                Dim giorni_noleggio_extra_rack As Integer = 0
                Dim tipo_Sconto As String = "0"
                Dim sconto As Double = 0
                Dim sconto_web_prepagato_primo_calcolo As Double = 0
                Dim sconto_su_rack As Double = 0
                Dim id_ribaltamento As String = ""
                Dim id_prenotazione As String = ""
                Dim id_contratto As String = ""
                Dim num_calcolo As String = "1"
                Dim id_utente As String = "5"
                Dim id_ditta As String = ""
                Dim id_fonte_commissionabile As String = ""
                Dim commissione_percentuale As String = ""
                Dim tipo_commissione As String = ""
                Dim primo_calcolo_commissione As Boolean = True
                Dim giorni_commissioni_originale As String = ""

                Dim Id_nuovo_preventivo As String
                Dim Id_nuovo_preventivo_prepagato As String

                If (id_tariffe_righe & "") <> "" Or (id_tariffe_righe_prep & "") <> "" Then
                    Id_nuovo_preventivo = nuovo_preventivo()
                    Id_nuovo_preventivo_prepagato = nuovo_preventivo_prepagato()

                    'calcolaTariffa_x_gruppo(stazione2, data_inizio_completa, ora_inizio2, minuti_inizio2, stazione_off2, id_tariffe_righe, Rs2("ID_gruppo"), id_gruppo_rack, numero_giorni, giorni_prepagati_x_modifica, prepagata, giorni_noleggio_extra_rack, sconto, tipo_Sconto, sconto_web_prepagato_primo_calcolo, sconto_su_rack, etaConducente, etaConducente, Id_nuovo_preventivo, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_utente, id_ditta, id_fonte_commissionabile, commissione_percentuale, tipo_commissione, primo_calcolo_commissione, giorni_commissioni_originale)
                    'calcolaTariffa_x_gruppo(stazione2, data_inizio_completa, ora_inizio2, minuti_inizio2, stazione_off2, id_tariffe_righe_prep, Rs2("ID_gruppo"), id_gruppo_rack, numero_giorni, giorni_prepagati_x_modifica, prepagata_si, giorni_noleggio_extra_rack, sconto, tipo_Sconto, sconto_web_prepagato_primo_calcolo, sconto_su_rack, etaConducente, etaConducente, Id_nuovo_preventivo_prepagato, id_ribaltamento, id_prenotazione, id_contratto, num_calcolo, id_utente, id_ditta, id_fonte_commissionabile, commissione_percentuale, tipo_commissione, primo_calcolo_commissione, giorni_commissioni_originale)

                    'Inzio 05/06/2015      

                    result += "Id Preventivo" & ":" & Id_nuovo_preventivo
                    result += "Id Preventivo Prepagato" & ":" & Id_nuovo_preventivo_prepagato
                    result += "Stazione Uscita" & ":" & stazione_inizio_noleggio
                    result += "Data PikUp" & ":" & data_inizio_noleggio
                    result += "Ora PikUp" & ":" & ora_inizio_noleggio
                    result += "Minuti PikUp" & ":" & minuti_inizio2
                    result += "Stazione Entrata" & ":" & stazione_fine_noleggio
                    result += "Data DropOff" & ":" & data_fine_noleggio

                    result += "Ora DropOff" & ":" & ora_fine2

                    result += "Minuti DropOff" & ":" & minuti_fine2
                    result += "Tariffa Riga" & ":" & id_tariffe_righe
                    result += "Tariffa RigaPrepagata" & ":" & id_tariffe_righe_prep
                    result += "Id Gruppo" & ":" & Rs2("ID_gruppo")
                    result += "Id Gruppo Rack" & ":" & id_gruppo_rack
                    result += "Numero Giorni" & ":" & "1"
                    result += "Età" & ":" & etaConducente
                    result += "Giorni Prepagata modifica" & ":" & giorni_prepagati_x_modifica
                    result += "Paga al Banco" & ":" & getPrezzo(Id_nuovo_preventivo, Rs2("ID_gruppo"))
                    result += "Paga On-line" & ":" & getPrezzo(Id_nuovo_preventivo_prepagato, Rs2("ID_gruppo"))
                    'Fine 05/06/2015

                Else
                    'dictionary = New Dictionary(Of String, String)(1)
                    result += "Entrato" & ":" & "No"
                    'list.Add(dictionary)
                End If
                'list.Add(dictionary)
            Loop
            Cmd2.Dispose()
            Cmd2 = Nothing
            Dbc2.Close()
            Dbc2.Dispose()
            Dbc2 = Nothing



            'Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return result '(j.Serialize(list.ToArray()))

        Catch ex As Exception
            Return ex.Message & " QueryError: (" & contError & ")  " & QueryError
        End Try
    End Function




    Public Function getListaExtra(ByVal IdPrepagato As String, ByVal lingua As String) As String



        'azzerare session ("chiamata_sito_web") per la registrazione del calcolo tariffe 11.06.2021
        'in modo da registrare i dati sul db
        'e richiamare calcolo x tariffe passando il valore dell'id preventivo che è stato passato
        If Session("chiamata_sito_web") = 2 Then




        End If






        Dim sqlstr As String = "SELECT condizioni_elementi.descrizione_en,preventivi_web_costi.num_calcolo, preventivi_web_costi.id, preventivi_web_costi.id_documento, gruppi.id_gruppo, gruppi.cod_gruppo As gruppo,preventivi_web_costi.id_elemento, preventivi_web_costi.nome_costo, condizioni_elementi.descrizione_lunga, condizioni_elementi.descrizione_lunga_eng, condizioni_elementi.tipologia_franchigia, condizioni_elementi.sottotipologia_franchigia, condizioni_elementi.is_gps as isGps, ISNULL((imponibile+iva_imponibile+ISNULL(imponibile_onere,0)+ISNULL(iva_onere,0)),0) As valore_costo, ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, ISNULL(commissioni_imponibile_originale,0)+ISNULL(commissioni_iva_originale,0) As commissioni, id_a_carico_di,preventivi_web_costi.obbligatorio, id_metodo_stampa, ISNULL(selezionato,'False') As selezionato, ISNULL(preventivi_web_costi.omaggiato,'False') As omaggiato, ISNULL(preventivi_web_costi.omaggiabile,'False') As omaggiabile, preventivi_web_costi.num_elemento, preventivi_web_costi.imponibile_scontato, preventivi_web_costi.iva_imponibile_scontato, condizioni_elementi.id_categoria_elemento, condizioni_elementi.ordine_elemento, categorie_condizioni_elementi.descrizione as categoria, categorie_condizioni_elementi.des_ita, categorie_condizioni_elementi.des_eng   FROM categorie_condizioni_elementi, preventivi_web_costi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON preventivi_web_costi.id_gruppo=gruppi.id_gruppo LEFT JOIN condizioni_elementi WITH(NOLOCK) ON preventivi_web_costi.id_elemento=condizioni_elementi.id WHERE ((id_documento = '" & IdPrepagato & "') AND (num_calcolo = '1'))  AND (condizioni_elementi.valorizza = 'True') AND obbligatorio='False' AND id_a_carico_di='2' AND id_metodo_stampa='2' AND ISNULL(condizioni_elementi.tipologia,'') <>'RIMUOVI_RIFORNIMENTO'   and categorie_condizioni_elementi.id = condizioni_elementi.id_categoria_elemento ORDER BY id_gruppo,ordine_stampa,ISNULL(condizioni_elementi.ordinamento_vis,0) DESC"
        Try
            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)


            Dim DbcExtra As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            DbcExtra.Open()
            'Dim CmdExtra As New Data.SqlClient.SqlCommand("SELECT condizioni_elementi.descrizione_en,preventivi_web_costi.num_calcolo, preventivi_web_costi.id, preventivi_web_costi.id_documento, gruppi.id_gruppo, gruppi.cod_gruppo As gruppo,preventivi_web_costi.id_elemento, preventivi_web_costi.nome_costo, condizioni_elementi.descrizione_lunga, condizioni_elementi.descrizione_lunga_eng, condizioni_elementi.tipologia_franchigia, condizioni_elementi.sottotipologia_franchigia, condizioni_elementi.is_gps as isGps, ISNULL((imponibile+iva_imponibile+ISNULL(imponibile_onere,0)+ISNULL(iva_onere,0)),0) As valore_costo, ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, ISNULL(commissioni_imponibile_originale,0)+ISNULL(commissioni_iva_originale,0) As commissioni, id_a_carico_di,preventivi_web_costi.obbligatorio, id_metodo_stampa, ISNULL(selezionato,'False') As selezionato, ISNULL(preventivi_web_costi.omaggiato,'False') As omaggiato, ISNULL(preventivi_web_costi.omaggiabile,'False') As omaggiabile, preventivi_web_costi.num_elemento, preventivi_web_costi.imponibile_scontato, preventivi_web_costi.iva_imponibile_scontato  FROM preventivi_web_costi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON preventivi_web_costi.id_gruppo=gruppi.id_gruppo LEFT JOIN condizioni_elementi WITH(NOLOCK) ON preventivi_web_costi.id_elemento=condizioni_elementi.id WHERE ((id_documento = '" & IdPrepagato & "') AND (num_calcolo = '1'))  AND (condizioni_elementi.valorizza = 'True') AND obbligatorio='False' AND id_a_carico_di='2' AND id_metodo_stampa='2' AND ISNULL(condizioni_elementi.tipologia,'') <>'RIMUOVI_RIFORNIMENTO' AND condizioni_elementi.id <> '207' AND condizioni_elementi.id <> '206'  ORDER BY id_gruppo,ordine_stampa,ISNULL(condizioni_elementi.ordinamento_vis,0) DESC", DbcExtra)
            'Dim CmdExtra As New Data.SqlClient.SqlCommand("SELECT condizioni_elementi.descrizione_en,preventivi_web_costi.num_calcolo, preventivi_web_costi.id, preventivi_web_costi.id_documento, gruppi.id_gruppo, gruppi.cod_gruppo As gruppo,preventivi_web_costi.id_elemento, preventivi_web_costi.nome_costo, condizioni_elementi.descrizione_lunga, condizioni_elementi.descrizione_lunga_eng, condizioni_elementi.tipologia_franchigia, condizioni_elementi.sottotipologia_franchigia, condizioni_elementi.is_gps as isGps, ISNULL((imponibile+iva_imponibile+ISNULL(imponibile_onere,0)+ISNULL(iva_onere,0)),0) As valore_costo, ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, ISNULL(commissioni_imponibile_originale,0)+ISNULL(commissioni_iva_originale,0) As commissioni, id_a_carico_di,preventivi_web_costi.obbligatorio, id_metodo_stampa, ISNULL(selezionato,'False') As selezionato, ISNULL(preventivi_web_costi.omaggiato,'False') As omaggiato, ISNULL(preventivi_web_costi.omaggiabile,'False') As omaggiabile, preventivi_web_costi.num_elemento, preventivi_web_costi.imponibile_scontato, preventivi_web_costi.iva_imponibile_scontato, condizioni_elementi.id_categoria_elemento, condizioni_elementi.ordine_elemento, categorie_condizioni_elementi.descrizione as categoria, categorie_condizioni_elementi.des_ita, categorie_condizioni_elementi.des_eng   FROM categorie_condizioni_elementi, preventivi_web_costi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON preventivi_web_costi.id_gruppo=gruppi.id_gruppo LEFT JOIN condizioni_elementi WITH(NOLOCK) ON preventivi_web_costi.id_elemento=condizioni_elementi.id WHERE ((id_documento = '" & IdPrepagato & "') AND (num_calcolo = '1'))  AND (condizioni_elementi.valorizza = 'True') AND obbligatorio='False' AND id_a_carico_di='2' AND id_metodo_stampa='2' AND ISNULL(condizioni_elementi.tipologia,'') <>'RIMUOVI_RIFORNIMENTO' AND condizioni_elementi.id <> '207' AND condizioni_elementi.id <> '206'  and categorie_condizioni_elementi.id = condizioni_elementi.id_categoria_elemento ORDER BY id_gruppo,ordine_stampa,ISNULL(condizioni_elementi.ordinamento_vis,0) DESC", DbcExtra)

            Dim CmdExtra As New Data.SqlClient.SqlCommand(sqlstr, DbcExtra)

            'Return "Sql " & CmdExtra.CommandText

            Dim RsExtra As Data.SqlClient.SqlDataReader
            RsExtra = CmdExtra.ExecuteReader()

            Do While RsExtra.Read()
                dictionary = New Dictionary(Of String, String)(1)
                If lingua = "ITA" Then
                    dictionary.Add("Extra_nome", RsExtra("nome_costo") & "")
                Else
                    dictionary.Add("Extra_nome", RsExtra("descrizione_en") & "")
                End If
                If lingua = "ITA" Then
                    dictionary.Add("Extra_nome_lungo", RsExtra("descrizione_lunga") & "")
                Else
                    dictionary.Add("Extra_nome_lungo", RsExtra("descrizione_lunga_eng") & "")
                End If
                dictionary.Add("Extra_id_elemento", RsExtra("id_elemento"))
                dictionary.Add("Extra_selezionato", RsExtra("selezionato"))
                dictionary.Add("Extra_valore", RsExtra("valore_costo"))
                dictionary.Add("Extra_sconto", RsExtra("sconto"))
                dictionary.Add("Extra_IsGps", RsExtra("isGps") & "")
                'dictionary.Add("Extra_Categoria", RsExtra("categoria") & "")
                If lingua = "ITA" Then
                    dictionary.Add("Extra_Categoria", RsExtra("des_ita") & "")
                Else
                    dictionary.Add("Extra_Categoria", RsExtra("des_eng") & "")
                End If
                dictionary.Add("Extra_Ordine", RsExtra("ordine_elemento") & "")
                list.Add(dictionary)
            Loop

            CmdExtra.Dispose()
            CmdExtra = Nothing
            DbcExtra.Close()
            DbcExtra.Dispose()
            DbcExtra = Nothing


            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
        Catch ex As Exception
            Return "Errore Lista Extra " & ex.Message & " : " & sqlstr
        End Try
    End Function


    Public Function getListaCondizioniInclusi(ByVal Id As String, ByVal lingua As String) As String

        Dim sqlstr As String = ""

        Try
            'Inizio 08/06/2015
            Dim id_a_carico_di As String = "2"
            Dim id_a_carico_utente As String = "5"
            Dim id_elemento_escluso As String = "98"
            Dim id_metodo_stampa As String = "2"
            Dim id_operatore_web As String = "1"
            Dim id_tariffa As String = "8"

            Dim DbcCondizioni As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            DbcCondizioni.Open()

            sqlstr = "SELECT condizioni_elementi.descrizione_en,nome_costo, ISNULL((imponibile+iva_imponibile+ISNULL(imponibile_onere,0)+ISNULL(iva_onere,0)),0) As valore_costo, ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto FROM preventivi_web_costi,condizioni_elementi WITH(NOLOCK) WHERE preventivi_web_costi.nome_costo =  condizioni_elementi.descrizione and ((id_documento = '" & Id & "') AND id_elemento > 0 AND id_elemento <> '" & id_elemento_escluso & "' AND obbligatorio='True' AND id_metodo_stampa= '" & id_metodo_stampa & "') ORDER BY nome_costo ASC"

            Dim CmdCondizioni As New Data.SqlClient.SqlCommand(sqlstr, DbcCondizioni)

            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)

            'dictionary = New Dictionary(Of String, String)(1)
            'dictionary.Add("SQL:", CmdCondizioni.CommandText)
            'list.Add(dictionary)            

            Dim RsCondizioni As Data.SqlClient.SqlDataReader
            RsCondizioni = CmdCondizioni.ExecuteReader()

            Do While RsCondizioni.Read()
                dictionary = New Dictionary(Of String, String)(1)
                If lingua = "ITA" Then
                    dictionary.Add("Condizione_nome", RsCondizioni("nome_costo") & "")
                Else
                    dictionary.Add("Condizione_nome", RsCondizioni("descrizione_en") & "")
                End If
                dictionary.Add("Condizione_valore", RsCondizioni("valore_costo"))
                dictionary.Add("Condizione_sconto", RsCondizioni("sconto"))
                list.Add(dictionary)
            Loop


            CmdCondizioni.Dispose()
            CmdCondizioni = Nothing
            DbcCondizioni.Close()
            DbcCondizioni.Dispose()
            DbcCondizioni = Nothing


            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
        Catch ex As Exception
            Return "Errore" & ": " & sqlstr
        End Try

    End Function


    Public Function getListaFranchigie(ByVal Id As String, ByVal lingua As String) As String

        Dim sqlstr As String = ""

        Try
            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)


            'Inizio 11/06/2015
            Dim id_a_carico_di As String = "2"
            Dim id_a_carico_utente As String = "5"
            Dim id_elemento_escluso As String = "98"
            Dim id_metodo_stampa As String = "2"
            Dim id_operatore_web As String = "1"
            Dim id_tariffa As String = "8"

            Dim DbcFranchiggie As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            DbcFranchiggie.Open()
            sqlstr = "SELECT condizioni_elementi.descrizione_en,nome_costo, ISNULL((imponibile+iva_imponibile+ISNULL(imponibile_onere,0)+ISNULL(iva_onere,0)),0) As valore_costo, ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto FROM preventivi_web_costi,condizioni_elementi WITH(NOLOCK) WHERE preventivi_web_costi.nome_costo =  condizioni_elementi.descrizione and ((id_documento = '" & Id & "') AND id_elemento > 0 AND id_elemento <> '" & id_elemento_escluso & "' AND franchigia_attiva = 'True')  ORDER BY nome_costo ASC"
            Dim CmdFranchiggie As New Data.SqlClient.SqlCommand(sqlstr, DbcFranchiggie)

            'Return "Sql " & CmdFranchiggie.CommandText

            Dim RsFranchiggie As Data.SqlClient.SqlDataReader
            RsFranchiggie = CmdFranchiggie.ExecuteReader()

            Do While RsFranchiggie.Read()
                dictionary = New Dictionary(Of String, String)(1)
                If lingua = "ITA" Then
                    dictionary.Add("Franchigie_nome", RsFranchiggie("nome_costo") & "")
                Else
                    dictionary.Add("Franchigie_nome", RsFranchiggie("descrizione_en") & "")
                End If
                dictionary.Add("Franchigie_valore", RsFranchiggie("valore_costo"))
                dictionary.Add("Franchigie_sconto", RsFranchiggie("sconto"))
                list.Add(dictionary)
            Loop


            CmdFranchiggie.Dispose()
            CmdFranchiggie = Nothing
            DbcFranchiggie.Close()
            DbcFranchiggie.Dispose()
            DbcFranchiggie = Nothing


            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
        Catch ex As Exception
            Return "Errore" & ": " & sqlstr
        End Try
    End Function


    Public Function getNomiFranchigie(ByVal Id As String, ByVal lingua As String) As String

        Dim sqlstr As String = ""

        Try
            Dim list As New List(Of Dictionary(Of String, String))()
            Dim dictionary As New Dictionary(Of String, String)(1)


            'Inizio 24/10/2016            

            Dim DbcFranchiggie As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            DbcFranchiggie.Open()
            sqlstr = "SELECT condizioni_elementi.descrizione,condizioni_elementi.descrizione_en,condizioni_elementi.ordinamento_vis from condizioni_elementi  WITH(NOLOCK) where 1=1 and condizioni_elementi.tipologia_franchigia = 'FRANCHIGIA' ORDER BY id ASC"
            Dim CmdFranchiggie As New Data.SqlClient.SqlCommand(sqlstr, DbcFranchiggie)

            'Return "Sql " & CmdFranchiggie.CommandText

            Dim RsFranchiggie As Data.SqlClient.SqlDataReader
            RsFranchiggie = CmdFranchiggie.ExecuteReader()

            Do While RsFranchiggie.Read()
                dictionary = New Dictionary(Of String, String)(1)
                If lingua = "ITA" Then
                    dictionary.Add("Franchigie_nome", RsFranchiggie("descrizione") & "")
                Else
                    dictionary.Add("Franchigie_nome", RsFranchiggie("descrizione_en") & "")
                End If
                dictionary.Add("Franchigie_ordine", RsFranchiggie("ordinamento_vis"))
                list.Add(dictionary)
            Loop

            CmdFranchiggie.Dispose()
            CmdFranchiggie = Nothing
            DbcFranchiggie.Close()
            DbcFranchiggie.Dispose()
            DbcFranchiggie = Nothing

            Dim j As JavaScriptSerializer = New JavaScriptSerializer()
            Return (j.Serialize(list.ToArray()))
        Catch ex As Exception
            Return "Errore in Nomi Franchigie : " & sqlstr
        End Try
    End Function


    Public Function putAccessorio(ByVal Id As String, ByVal id_gruppoScelto As String, ByVal id_elemento As String, ByVal prepagato As Boolean, ByVal gps As Boolean, ByVal stazione As String, ByVal stazione_off As String, ByVal numero_giorni As String) As String
        Try
            Dim sconto As Double = 0
            Dim id_tariffa As String = "8"

            aggiungi_costo_accessorio(Id, "", "", "", "1", id_gruppoScelto, id_elemento, "", "", "", prepagato, "0", "0", "0")

            'SE E' STATO AGGINTO IL GPS AGGIUNGO EVENTUALMENTE IL VAL
            If gps Then
                If stazione <> stazione_off Then
                    If prepagato Then
                        aggiungi_val_gps(stazione, stazione_off, id_gruppoScelto, numero_giorni, 0, True, Nothing, "", "", sconto, "16", Id, "", "", "", "1", "NULL", "0", "0", "0")
                    Else
                        aggiungi_val_gps(stazione, stazione_off, id_gruppoScelto, numero_giorni, 0, False, Nothing, "", "", sconto, "8", Id, "", "", "", "1", "NULL", "0", "0", "0")

                    End If
                End If
            End If


            Dim DbcExtraTot As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            DbcExtraTot.Open()
            Dim CmdExtraTot As New Data.SqlClient.SqlCommand("SELECT preventivi_web_costi.valore_costo  FROM preventivi_web_costi WITH(NOLOCK)  WHERE (id_documento = '" & Id & "') AND nome_costo='TOTALE'", DbcExtraTot)

            Dim Totale As String

            Totale = CmdExtraTot.ExecuteScalar

            CmdExtraTot.Dispose()
            CmdExtraTot = Nothing
            DbcExtraTot.Close()
            DbcExtraTot.Dispose()
            DbcExtraTot = Nothing

            If gps Then
                Return "Ok Inserito GPS" & "--" & Totale
            Else
                Return "Ok Inserito" & "--" & Totale
            End If
        Catch ex As Exception
            Return "Errore"
        End Try
    End Function


    Public Function RimuoviAccessorio(ByVal Id As String, ByVal id_gruppoScelto As String, ByVal id_elemento As String, ByVal prepagato As Boolean, ByVal gps As Boolean, ByVal stazione As String, ByVal stazione_off As String, ByVal numero_giorni As String) As String
        Try
            Dim sconto As Double = 0
            Dim id_tariffa As String = "8"

            'rimuovi_costo_accessorio(Id, "", "", "", "1", id_gruppoScelto, id_elemento, "", "SCELTA", "0", prepagato)
            rimuovi_costo_accessorio(Id, "", "", "", "1", id_gruppoScelto, id_elemento, "", "", "", prepagato, "0", "0", "0")

            'SE E' STATO RIMOSSO IL GPS RIMUOVO L'EVENTUALE VAL
            If gps Then
                'rimuovi_costo_accessorio(Id, "", "", "", "1", id_gruppoScelto, "73", "", "EXTRA", "0", prepagato)
                rimuovi_costo_accessorio(Id, "", "", "", "1", id_gruppoScelto, "73", "", "", "", prepagato, "0", "0", "0")
                ''SE E' STATO AGGINTO IL GPS AGGIUNGO EVENTUALMENTE IL VAL
                'If gps Then
                '    If stazione <> stazione_off Then
                '        If prepagato Then
                '            aggiungi_val_gps(stazione, stazione_off, id_gruppoScelto, numero_giorni, 0, True, Nothing, "", "", sconto, "16", Id, "", "", "", "1", "NULL", "0", "0", "0")
                '        Else
                '            aggiungi_val_gps(stazione, stazione_off, id_gruppoScelto, numero_giorni, 0, False, Nothing, "", "", sconto, "8", Id, "", "", "", "1", "NULL", "0", "0", "0")

                '        End If
                '    End If
                'End If
            End If


            Dim DbcExtraTot As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            DbcExtraTot.Open()
            Dim CmdExtraTot As New Data.SqlClient.SqlCommand("SELECT preventivi_web_costi.valore_costo  FROM preventivi_web_costi WITH(NOLOCK)  WHERE (id_documento = '" & Id & "') AND nome_costo='TOTALE'", DbcExtraTot)

            Dim Totale As String

            Totale = CmdExtraTot.ExecuteScalar

            CmdExtraTot.Dispose()
            CmdExtraTot = Nothing
            DbcExtraTot.Close()
            DbcExtraTot.Dispose()
            DbcExtraTot = Nothing

            If gps Then
                Return "Ok Rimosso GPS" & "--" & Totale
            Else
                Return "Ok Rimosso" & "--" & Totale
            End If
        Catch ex As Exception
            Return "Errore"
        End Try
    End Function


    Public Function putDatiPrenotazione(ByVal Id As String, ByVal data_inizio_completa As String, ByVal data_fine_completa As String, ByVal prepagata As Boolean, ByVal ora_inizio As String, ByVal minuti_inizio As String, ByVal ora_fine As String, ByVal minuti_fine As String, ByVal id_tariffe_righe As String, ByVal id_gruppo As String, ByVal stazione As String, ByVal stazione_off As String, ByVal nome As String, ByVal cognome As String, ByVal data_nascita As String, ByVal email As String, ByVal indirizzo As String, ByVal telefono As String, ByVal num_volo_arrivo As String, ByVal num_volo_partenza As String, ByVal eta As String, ByVal numero_giorni As String) As String
        Dim sqla As String = ""
        Dim num_prenotazione As Integer = 0

        Try

            Dim data_uscita As String = data_inizio_completa
            Dim data_rientro As String = data_fine_completa

            'data_uscita = getDataDb_senza_orario(data_uscita, Request.ServerVariables("HTTP_HOST"))
            'data_rientro = getDataDb_senza_orario(data_rientro, Request.ServerVariables("HTTP_HOST"))

            Dim tipo_cliente As String = "3"

            Dim DbcTariffa As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            DbcTariffa.Open()
            Dim CmdTariffa As New Data.SqlClient.SqlCommand("SELECT id_tariffa  FROM tariffe_righe WITH(NOLOCK)  WHERE (id = '" & id_tariffe_righe & "')", DbcTariffa)

            Dim id_tariffa As String = CmdTariffa.ExecuteScalar

            CmdTariffa.Dispose()
            CmdTariffa = Nothing
            DbcTariffa.Close()
            DbcTariffa.Dispose()
            DbcTariffa = Nothing


            'Dim id_tariffa As String
            'If prepagata Then
            '    id_tariffa = id_tariffe_righe_prepagate
            'Else
            '    id_tariffa = id_tariffe_righe
            'End If

            'Dim id_tariffe_righe As String = lbl_id_tariffe_righe.Text

            Dim tipo_tariffa As String = "fonte"
            Dim pren_no_tariffa As String = "0"
            Dim prora_out As String  'ORARIO USCITA
            Dim prora_pr As String   'ORARIO PRESUNTO RIENTRO
            prora_out = "1900-01-01 " & ora_inizio & ":" & minuti_inizio
            prora_pr = "1900-01-01 " & ora_fine & ":" & minuti_fine
            Dim codice_tariffa As String = "8"

            Dim DbcTariffa2 As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            DbcTariffa2.Open()
            Dim CmdTariffa2 As New Data.SqlClient.SqlCommand("SELECT  tariffe.CODTAR  FROM tariffe WITH(NOLOCK)  WHERE  (tariffe.id = '" & id_tariffa & "')", DbcTariffa2)

            Dim cod As String = CmdTariffa2.ExecuteScalar

            CmdTariffa2.Dispose()
            CmdTariffa2 = Nothing
            DbcTariffa2.Close()
            DbcTariffa2.Dispose()
            DbcTariffa2 = Nothing


            If data_nascita = "" Then
                data_nascita = "NULL"
            Else
                data_nascita = funzioni_comuni.GetDataSql(data_nascita, 0)
            End If

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT ISNULL(MAX(NUMPREN),0) FROM prenotazioni WITH(NOLOCK) WHERE codice_provenienza='" & stazione & "'", Dbc)

            num_prenotazione = Cmd.ExecuteScalar + 1

            If num_prenotazione = 1 Then
                'SE E' LA PRIMA PRENOTAZIONE DELLA STAZIONE RESTITUISCO IL PRIMO NUMERO
                num_prenotazione = CInt(Left(stazione, 2) & "000001")
            End If
            Cmd.Dispose()


            Dim TAR_VAL_DAL As String
            Dim TAR_VAL_AL As String
            Cmd = New Data.SqlClient.SqlCommand("SELECT vendibilita_da FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)
            TAR_VAL_DAL = Cmd.ExecuteScalar

            Cmd = New Data.SqlClient.SqlCommand("SELECT vendibilita_A FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'", Dbc)
            TAR_VAL_AL = Cmd.ExecuteScalar

            TAR_VAL_DAL = Year(TAR_VAL_DAL) & "-" & Month(TAR_VAL_DAL) & "-" & Day(TAR_VAL_DAL) & " 00:00:00"
            TAR_VAL_AL = Year(TAR_VAL_AL) & "-" & Month(TAR_VAL_AL) & "-" & Day(TAR_VAL_AL) & " 00:00:00"

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            Dim volo_out As String = Replace(num_volo_arrivo, "'", "''")
            Dim volo_pr As String = Replace(num_volo_partenza, "'", "''")
            Dim id_operatore As String = Costanti.costanti_web.id_operatore_web

            Dim sqlStr2 As String = "INSERT INTO prenotazioni (NUMPREN,num_calcolo,status,attiva,provenienza,codice_provenienza,PRID_stazione_out,PRID_stazione_pr,"

            sqlStr2 += "PRDATA_OUT,ore_uscita,minuti_uscita,PRDATA_PR,ore_rientro,minuti_rientro,ID_GRUPPO,ID_GRUPPO_APP,id_conducente,nome_conducente,"
            sqlStr2 += "cognome_conducente,eta_primo_guidatore,eta_secondo_guidatore,data_nascita,mail_conducente,indirizzo_conducente,riferimento_telefono,"
            sqlStr2 += "id_fonte,codice_edp,id_cliente,id_tariffa,id_tariffe_righe,tipo_tariffa,pren_broker_no_tariffa,sconto_applicato, tipo_sconto,"

            sqlStr2 += "id_preventivo,id_operatore_creazione,DATAPREN,PRORA_OUT,PRORA_PR,giorni,CODTAR,codice,"

            sqlStr2 += "TAR_VAL_DAL,TAR_VAL_AL,N_VOLOOUT,N_VOLOPR,"
            sqlStr2 += "IMPORTO_PREVENTIVO, importo_a_carico_del_broker, rif_to, giorni_to, id_fonte_commissionabile, tipo_commissione, commissione_percentuale, giorni_commissioni)"
            sqlStr2 += " VALUES "
            sqlStr2 += "('" & num_prenotazione & "','1',0,'1','web','" & stazione & "','" & stazione & "','" & stazione_off & "',"
            sqlStr2 += "convert(date,'" & data_uscita & "',102),'" & ora_inizio & "','" & minuti_inizio & "',convert(date,'" & data_rientro & "',102),"
            sqlStr2 += "'" & ora_fine & "','" & minuti_fine & "','" & id_gruppo & "','" & id_gruppo & "',NULL,'" & Replace(nome, "'", "''") & "',"
            sqlStr2 += "'" & Replace(cognome, "'", "''") & "','" & eta & "','',convert(date,'" & data_nascita & "',102),'" & Replace(email, "'", "''") & "','" & Replace(indirizzo, "'", "''") & "','" & Replace(telefono, "'", "''") & "',"
            sqlStr2 += "'" & tipo_cliente & "',NULL,'1'," & id_tariffa & "," & id_tariffe_righe & ",'" & tipo_tariffa & "'," & pren_no_tariffa & ",'0','0',"

            sqlStr2 += "'" & Id & "','" & id_operatore & "',convert(date,GetDate(),102),'" & prora_out & "','" & prora_pr & "','" & numero_giorni & "','" & cod & "','" & cod & "',"
            sqlStr2 += "convert(date,'" & TAR_VAL_DAL & "',102),convert(date,'" & TAR_VAL_AL & "',102),'" & volo_out & "','" & volo_pr & "',"
            sqlStr2 += "NULL,NULL,'',NULL,NULL,NULL,NULL,NULL)"

            sqla = sqlStr2

            'HttpContext.Current.Trace.Write("sqlStr2 " & sqlStr2)


            ''Response.Write(sqlStr)
            'Response.End()
            Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc2.Open()
            Dim Cmd2 = New Data.SqlClient.SqlCommand(sqlStr2, Dbc2)
            Dim x2 As Integer = Cmd2.ExecuteNonQuery()
            If x2 = 0 Then
                'num_prenotazione = "02"
            End If

            Cmd2.Dispose()
            Cmd2 = Nothing

            Dbc2.Close()
            Dbc2.Dispose()
            Dbc2 = Nothing

            'Dim Sql3 = "SELECT @@IDENTITY FROM prenotazioni WITH(NOLOCK)"
            Dim Sql3 = "SELECT MAX(Nr_Pren) FROM prenotazioni WITH(NOLOCK)"
            'HttpContext.Current.Trace.Write("Sql3 " & Sql3)
            sqla = Sql3



            Dim Dbc3 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc3.Open()
            Dim Cmd3 = New Data.SqlClient.SqlCommand(Sql3, Dbc3)

            Dim id_prenotazione As String = Cmd3.ExecuteScalar

            Cmd3.Dispose()
            Cmd3 = Nothing

            Dbc3.Close()
            Dbc3.Dispose()
            Dbc3 = Nothing


            'SALVO LA RIGA DI CALCOLO E DI WARNING NELLE TABELLE PRENOTAZIONI_COSTI E PRENOTAZIONI_WARNING
            Dim sqlStr = "INSERT INTO prenotazioni_costi (id_documento, num_calcolo, ordine_stampa, id_gruppo, id_elemento, num_elemento, nome_costo," &
                "selezionato,omaggiato, prepagato, valore_costo,valore_percentuale,imponibile,iva_imponibile,imponibile_scontato,iva_imponibile_scontato," &
                "imponibile_onere, iva_onere, aliquota_iva,iva_inclusa,scontabile,omaggiabile,acquistabile_nolo_in_corso,id_a_carico_di,id_metodo_stampa,obbligatorio, franchigia_attiva, id_unita_misura, qta, packed,imponibile_scontato_prepagato,iva_imponibile_scontato_prepagato,imponibile_onere_prepagato,iva_onere_prepagato, commissioni_imponibile,sconto_su_imponibile_prepagato, commissioni_iva, commissioni_imponibile_originale, commissioni_iva_originale) " &
                "(SELECT '" & id_prenotazione & "','1', ordine_stampa, id_gruppo,id_elemento,num_elemento,nome_costo," &
                "selezionato,omaggiato,prepagato, valore_costo,valore_percentuale,imponibile,iva_imponibile,imponibile_scontato,iva_imponibile_scontato," &
                "imponibile_onere, iva_onere, aliquota_iva,iva_inclusa,scontabile,omaggiabile,acquistabile_nolo_in_corso,id_a_carico_di,id_metodo_stampa,obbligatorio, franchigia_attiva, id_unita_misura,qta, packed,imponibile_scontato_prepagato,iva_imponibile_scontato_prepagato,imponibile_onere_prepagato, commissioni_imponibile,iva_onere_prepagato,sconto_su_imponibile_prepagato, commissioni_iva, commissioni_imponibile_originale, commissioni_iva_originale " &
                "FROM preventivi_web_costi WITH(NOLOCK) WHERE id_documento='" & Id & "' AND num_calcolo='1')"

            sqla = sqlStr

            'HttpContext.Current.Trace.Write("sqlStr " & sqlStr)

            Dim Dbc4 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc4.Open()
            Dim Cmd4 = New Data.SqlClient.SqlCommand(sqlStr, Dbc4)
            Dim x4 As Integer = Cmd4.ExecuteNonQuery()
            If x4 = 0 Then
                'num_prenotazione = sqla
            End If

            Cmd4.Dispose()
            Cmd4 = Nothing

            Dbc4.Close()
            Dbc4.Dispose()
            Dbc4 = Nothing

            Return num_prenotazione

            'Response.Redirect("riepilogo.aspx?prenotazione=" & id_preventivo & "&num=" & id_prenotazione)           
        Catch ex As Exception
            Return ex.Message & "<br/>" & sqla & "<br/>"
        End Try



    End Function


    Public Function putDatiPrenotazionePrepagata(ByVal Id As String, ByVal data_inizio_completa As String, ByVal data_fine_completa As String, ByVal prepagata As Boolean, ByVal ora_inizio As String, ByVal minuti_inizio As String, ByVal ora_fine As String, ByVal minuti_fine As String, ByVal id_tariffe_righe As String, ByVal id_gruppo As String, ByVal stazione As String, ByVal stazione_off As String, ByVal nome As String, ByVal cognome As String, ByVal data_nascita As String, ByVal email As String, ByVal indirizzo As String, ByVal telefono As String, ByVal num_volo_arrivo As String, ByVal num_volo_partenza As String, ByVal eta As String, ByVal numero_giorni As String, ByVal importo_prepagato As String) As String
            Dim sqla As String = ""
            Dim sqlStr2 As String = ""

            Try

                Dim data_uscita As String = data_inizio_completa
                Dim data_rientro As String = data_fine_completa

                'data_uscita = getDataDb_senza_orario(data_uscita, Request.ServerVariables("HTTP_HOST"))
                'data_rientro = getDataDb_senza_orario(data_rientro, Request.ServerVariables("HTTP_HOST"))

                Dim tipo_cliente As String = "3"



                Dim DbcTariffa As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                DbcTariffa.Open()
                sqla = "SELECT id_tariffa  FROM tariffe_righe WITH(NOLOCK)  WHERE (id = '" & id_tariffe_righe & "')"
                Dim CmdTariffa As New Data.SqlClient.SqlCommand(sqla, DbcTariffa)


                Dim id_tariffa As String = CmdTariffa.ExecuteScalar

                CmdTariffa.Dispose()
                CmdTariffa = Nothing
                DbcTariffa.Close()
                DbcTariffa.Dispose()
                DbcTariffa = Nothing

                Dim tipo_tariffa As String = "fonte"
                Dim pren_no_tariffa As String = "0"
                Dim prora_out As String  'ORARIO USCITA
                Dim prora_pr As String   'ORARIO PRESUNTO RIENTRO
                prora_out = "1900-01-01 " & ora_inizio & ":" & minuti_inizio
                prora_pr = "1900-01-01 " & ora_fine & ":" & minuti_fine
                Dim codice_tariffa As String = "8"
                Dim cod As String = "Web"
                If data_nascita = "" Then
                    data_nascita = "NULL"
                Else
                    data_nascita = funzioni_comuni.GetDataSql(data_nascita, 0)
                End If

                Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc.Open()
                sqlStr2 = "SELECT ISNULL(MAX(NUMPREN),0) FROM prenotazioni WITH(NOLOCK) WHERE codice_provenienza='" & stazione & "'"
                Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr2, Dbc)

                Dim num_prenotazione As Integer = Cmd.ExecuteScalar + 1

                If num_prenotazione = 1 Then
                    'SE E' LA PRIMA PRENOTAZIONE DELLA STAZIONE RESTITUISCO IL PRIMO NUMERO
                    num_prenotazione = CInt(Left(stazione, 2) & "000001")
                End If
                Cmd.Dispose()


                Dim TAR_VAL_DAL As String
                Dim TAR_VAL_AL As String
                sqlStr2 = "SELECT vendibilita_da FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr2, Dbc)
                TAR_VAL_DAL = Cmd.ExecuteScalar
                sqlStr2 = "SELECT vendibilita_A FROM tariffe_righe WITH(NOLOCK) WHERE id='" & id_tariffe_righe & "'"
                Cmd = New Data.SqlClient.SqlCommand(sqlStr2, Dbc)
                TAR_VAL_AL = Cmd.ExecuteScalar

                'TAR_VAL_DAL = "'" & funzioni_comuni.getDataDb_senza_orario(TAR_VAL_DAL) & "'"

                'TAR_VAL_AL = "'" & funzioni_comuni.getDataDb_senza_orario(TAR_VAL_AL) & "'"

                TAR_VAL_DAL = Year(TAR_VAL_DAL) & "-" & Month(TAR_VAL_DAL) & "-" & Day(TAR_VAL_DAL) & " 00:00:00"

                TAR_VAL_AL = Year(TAR_VAL_AL) & "-" & Month(TAR_VAL_AL) & "-" & Day(TAR_VAL_AL) & " 00:00:00"

                Cmd.Dispose()
                Cmd = Nothing
                Dbc.Close()
                Dbc.Dispose()
                Dbc = Nothing

                'Registrazione Dati Clinte in Ditte ---------------------------------------------------
                'Dim CodiceEDP As String = NewCodiceEDP()
                'Dim NomeCognome = nome & " " & cognome

                'Dim comune_ares As String = "NULL"
                'Dim nazione As String = "NULL"
                'Dim citta As String = "NULL"

                'Dim sqlStrClientiDitte As String = "INSERT INTO DITTE (ID_Cliente,[CODICE EDP],Rag_soc,Indirizzo,Citta,id_comune_ares,provincia,Cap,NAZIONE,PIva,c_fis, tour_op, invio_email, invio_email_cc, invio_email_statement, email)" & _
                '   " VALUES (" & CodiceEDP & "," & CodiceEDP & ",'" & Replace(NomeCognome, "'", "''") & "','" & Replace(indirizzo, "'", "''") & "','" & Replace(citta, "'", "''") & "'," & comune_ares & ",'',''," & nazione & ",'','','0','0','0','0','" & Replace(email, "'", "''") & "')"

                ''HttpContext.Current.Trace.Write("sqlStrClientiDitte: " & sqlStrClientiDitte)

                'Dim Dbc5 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                'Dbc5.Open()
                'Dim Cmd5 = New Data.SqlClient.SqlCommand(sqlStrClientiDitte, Dbc5)

                'Cmd5.ExecuteNonQuery()

                'Cmd5 = New Data.SqlClient.SqlCommand("SELECT max(id_ditta) FROM ditte WITH(NOLOCK)", Dbc5)

                ''HttpContext.Current.Trace.Write("sqlStrClientiDitte: " & Cmd5.CommandText)

                'Dim id_ditta As String = Cmd5.ExecuteScalar

                'Cmd5.Dispose()
                'Cmd5 = Nothing

                'Dbc5.Close()
                'Dbc5.Dispose()
                'Dbc5 = Nothing

                Dim id_ditta As String = "1"

                Dim volo_out As String = Replace(num_volo_arrivo, "'", "''")
                Dim volo_pr As String = Replace(num_volo_partenza, "'", "''")
                Dim id_operatore As String = Costanti.costanti_web.id_operatore_web

                'Dim sqlStr2 As String = "INSERT INTO prenotazioni (NUMPREN,num_calcolo,status,attiva,provenienza,codice_provenienza,PRID_stazione_out,PRID_stazione_pr," & _
                '    "PRDATA_OUT,ore_uscita,minuti_uscita,PRDATA_PR,ore_rientro,minuti_rientro,ID_GRUPPO,ID_GRUPPO_APP,id_conducente,nome_conducente," & _
                '    "cognome_conducente,eta_primo_guidatore,eta_secondo_guidatore,data_nascita,mail_conducente,indirizzo_conducente,riferimento_telefono,id_fonte,codice_edp,id_cliente,id_tariffa,id_tariffe_righe,tipo_tariffa,pren_broker_no_tariffa,sconto_applicato, tipo_sconto," & _
                '    "id_preventivo,id_operatore_creazione,DATAPREN,PRORA_OUT,PRORA_PR,giorni,CODTAR,codice,TAR_VAL_DAL,TAR_VAL_AL,N_VOLOOUT,N_VOLOPR," & _
                '    "IMPORTO_PREVENTIVO, importo_a_carico_del_broker, rif_to, giorni_to, id_fonte_commissionabile, tipo_commissione, commissione_percentuale, giorni_commissioni)" & _
                '    " VALUES " & _
                '        "('" & num_prenotazione & "','1',0,'1','web','" & stazione & "','" & stazione & "','" & stazione_off & "'," & _
                '        "'" & data_uscita & "','" & ora_inizio & "','" & minuti_inizio & "','" & data_rientro & "'," & _
                '        "'" & ora_fine & "','" & minuti_fine & "','" & id_gruppo & "'," & _
                '        id_gruppo & ",NULL," & _
                '        "'" & Replace(nome, "'", "''") & "','" & Replace(cognome, "'", "''") & "'," & _
                '        "'" & eta & "','','" & data_nascita & "','" & Replace(email, "'", "''") & "','" & Replace(indirizzo, "'", "''") & "','" & Replace(telefono, "'", "''") & "'," & _
                '        "'" & tipo_cliente & "',NULL,NULL," & id_tariffa & "," & id_tariffe_righe & ",'" & tipo_tariffa & "'," & pren_no_tariffa & ",'0','0','" & id_operatore & "'," & _
                '        " NULL,GetDate()," & _
                '        "'" & prora_out & "','" & prora_pr & "','" & numero_giorni & "','" & cod & "','" & cod & "'," & _
                '        TAR_VAL_DAL & "," & TAR_VAL_AL & ",'" & volo_out & "'," & _
                '        "'" & volo_pr & "',NULL,NULL,'',NULL,NULL,NULL,NULL,NULL)"


                sqlStr2 = "INSERT INTO prenotazioni (NUMPREN,num_calcolo,status,attiva,provenienza,codice_provenienza,PRID_stazione_out,PRID_stazione_pr," &
          "PRDATA_OUT,ore_uscita,minuti_uscita,PRDATA_PR,ore_rientro,minuti_rientro,ID_GRUPPO,ID_GRUPPO_APP,id_conducente,nome_conducente," &
          "cognome_conducente,eta_primo_guidatore,eta_secondo_guidatore,data_nascita,mail_conducente,indirizzo_conducente,riferimento_telefono,id_fonte,codice_edp,id_cliente,id_tariffa,id_tariffe_righe,tipo_tariffa,pren_broker_no_tariffa,sconto_applicato, tipo_sconto," &
          "id_preventivo,id_operatore_creazione,DATAPREN,PRORA_OUT,PRORA_PR,giorni,CODTAR,codice,TAR_VAL_DAL,TAR_VAL_AL,N_VOLOOUT,N_VOLOPR," &
          "IMPORTO_PREVENTIVO, importo_a_carico_del_broker, rif_to, giorni_to, id_fonte_commissionabile, tipo_commissione, commissione_percentuale, giorni_commissioni, prepagata, importo_prepagato, giorni_prepagati, sconto_web_prepagato, id_gruppo_originale_prepagato)" &
          " VALUES " &
          "('" & num_prenotazione & "','1',0,'1','web','" & stazione & "','" & stazione & "','" & stazione_off & "'," &
          "convert(date,'" & data_uscita & "',102),'" & ora_inizio & "','" & minuti_inizio & "',convert(date,'" & data_rientro & "',102)," &
          "'" & ora_fine & "','" & minuti_fine & "','" & id_gruppo & "'," &
          id_gruppo & ",NULL," &
          "'" & Replace(nome, "'", "''") & "','" & Replace(cognome, "'", "''") & "'," &
          "'" & eta & "','',convert(date,'" & data_nascita & "',102),'" & Replace(email, "'", "''") & "','" & Replace(indirizzo, "'", "''") & "','" & Replace(telefono, "'", "''") & "'," &
          "'" & tipo_cliente & "',NULL," & id_ditta & "," & id_tariffa & "," & id_tariffe_righe & ",'" & tipo_tariffa & "'," & pren_no_tariffa & ",'0','0','" & id_operatore & "'," &
          " NULL,convert(date,GetDate(),102)," &
          "'" & prora_out & "','" & prora_pr & "','" & numero_giorni & "','" & cod & "','" & cod & "'," &
          "convert(Date,'" & TAR_VAL_DAL & "',102),convert(date,'" & TAR_VAL_AL & "',102),'" & volo_out & "'," &
          "'" & volo_pr & "',NULL,NULL,'',NULL,NULL,NULL,NULL,NULL,'True','" & importo_prepagato & "','" & numero_giorni & "',NULL" & ",'" & id_gruppo & "')"

                'HttpContext.Current.Trace.Write("sqlStr2 " & sqlStr2)


                ''Response.Write(sqlStr)
                'Response.End()
                Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc2.Open()
                Dim Cmd2 = New Data.SqlClient.SqlCommand(sqlStr2, Dbc2)
                Cmd2.ExecuteNonQuery()

                Cmd2.Dispose()
                Cmd2 = Nothing

                Dbc2.Close()
                Dbc2.Dispose()
                Dbc2 = Nothing

                'Dim Sql3 As String = "SELECT @@IDENTITY FROM prenotazioni WITH(NOLOCK)"
                sqlStr2 = "SELECT MAX(Nr_Pren) FROM prenotazioni WITH(NOLOCK)"
                'HttpContext.Current.Trace.Write("Sql3 " & Sql3)
                Dim Dbc3 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc3.Open()
                Dim Cmd3 = New Data.SqlClient.SqlCommand(sqlStr2, Dbc3)
                Dim id_prenotazione As String = Cmd3.ExecuteScalar

                Cmd3.Dispose()
                Cmd3 = Nothing

                Dbc3.Close()
                Dbc3.Dispose()
                Dbc3 = Nothing


                'SALVO LA RIGA DI CALCOLO E DI WARNING NELLE TABELLE PRENOTAZIONI_COSTI E PRENOTAZIONI_WARNING
                sqlStr2 = "INSERT INTO prenotazioni_costi (id_documento, num_calcolo, ordine_stampa, id_gruppo, id_elemento, num_elemento, nome_costo," &
                "selezionato,omaggiato, prepagato, valore_costo,valore_percentuale,imponibile,iva_imponibile,imponibile_scontato,iva_imponibile_scontato," &
                "imponibile_onere, iva_onere, aliquota_iva,iva_inclusa,scontabile,omaggiabile,acquistabile_nolo_in_corso,id_a_carico_di,id_metodo_stampa,obbligatorio, franchigia_attiva, id_unita_misura, qta, packed,imponibile_scontato_prepagato,iva_imponibile_scontato_prepagato,imponibile_onere_prepagato,iva_onere_prepagato, commissioni_imponibile,sconto_su_imponibile_prepagato, commissioni_iva, commissioni_imponibile_originale, commissioni_iva_originale) " &
                "(SELECT '" & id_prenotazione & "','1', ordine_stampa, id_gruppo,id_elemento,num_elemento,nome_costo," &
                "selezionato,omaggiato,prepagato, valore_costo,valore_percentuale,imponibile,iva_imponibile,imponibile_scontato,iva_imponibile_scontato," &
                "imponibile_onere, iva_onere, aliquota_iva,iva_inclusa,scontabile,omaggiabile,acquistabile_nolo_in_corso,id_a_carico_di,id_metodo_stampa,obbligatorio, franchigia_attiva, id_unita_misura,qta, packed,imponibile_scontato_prepagato,iva_imponibile_scontato_prepagato,imponibile_onere_prepagato, commissioni_imponibile,iva_onere_prepagato,sconto_su_imponibile_prepagato, commissioni_iva, commissioni_imponibile_originale, commissioni_iva_originale FROM preventivi_web_costi WITH(NOLOCK) WHERE id_documento='" & Id & "' AND num_calcolo='1')"

                'HttpContext.Current.Trace.Write("sqlStr " & sqlStr)

                Dim Dbc4 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc4.Open()
                Dim Cmd4 = New Data.SqlClient.SqlCommand(sqlStr2, Dbc4)

                Cmd4.ExecuteNonQuery()

                Cmd4.Dispose()
                Cmd4 = Nothing

                Dbc4.Close()
                Dbc4.Dispose()
                Dbc4 = Nothing



                'REGISTRAZIONE DEL PAGAMENTO --------------------------------------------------------
                Dim Nr_Contratto As String = Contatori.getContatore_pagamenti_extra(stazione)
                Dim dnow As String = Year(Date.Now) & "-" & Month(Date.Now) & "-" & Day(Date.Now) & " 00:00:00.000" '  funzioni_comuni.GetDataSql()

                sqlStr2 = "INSERT INTO pagamenti_extra (Nr_Contratto,data,id_tippag, id_pos_funzioni_ares, importo, ID_STAZIONE, " &
                "cassa, intestatario, scadenza, nr_aut," &
                "tipsegno, DATACRE, UTECRE, N_PREN_RIF," &
                " NR_BATCH,PER_IMPORTO, CONTABILIZZATO,TRANSATION_TYPE, CARD_TYPE, operazione_stornata, acquire_id, action_code, DATA_OPERAZIONE,TentativiPOS)" &
                 " VALUES " &
                "('" & Nr_Contratto & "',convert(datetime,'" & dnow & "',102),'1011098650','4','0.00','" & stazione & "','','','','0000023','1',convert(datetime,GetDate(),102),'web','" & num_prenotazione & "','000018'," & importo_prepagato & ",'0','MAG','2','0','00000000002','000',convert(datetime,'" & funzioni_comuni.getDataDb_senza_orario(Now) & "',102),'1')"

                'HttpContext.Current.Trace.Write("Sql Pagamento Extra: " & sqlPagamentoExtra)

                Dim Dbc6 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc6.Open()
                Dim Cmd6 = New Data.SqlClient.SqlCommand(sqlStr2, Dbc6)

                Cmd6.ExecuteNonQuery()

                Cmd6.Dispose()
                Cmd6 = Nothing

                Dbc6.Close()
                Dbc6.Dispose()
                Dbc6 = Nothing

                sqlStr2 = "SELECT max(ID_CTR) FROM pagamenti_extra WITH(NOLOCK)"
                'Dim Sql3 = "SELECT MAX(Nr_Pren) FROM prenotazioni WITH(NOLOCK)"
                'HttpContext.Current.Trace.Write("sqlStrMaxId: " & sqlStrMaxId)

                Dim Dbc7 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Dbc7.Open()
                Dim Cmd7 = New Data.SqlClient.SqlCommand(sqlStr2, Dbc7)
                Dim id_pagamenti_extra As String = Cmd7.ExecuteScalar

                Cmd7.Dispose()
                Cmd7 = Nothing

                Dbc7.Close()
                Dbc7.Dispose()
                Dbc7 = Nothing

                ''FATTURAZIONE DEL PREPAGAMENTO
                ''HttpContext.Current.Trace.Write("A: ")
                'Dim miaPrenotazioneWeb As funzioni_comuni.PrenotazioneWeb = New funzioni_comuni.PrenotazioneWeb
                'With miaPrenotazioneWeb
                '    'HttpContext.Current.Trace.Write("B: ")
                '    .CCDATA = Format(Now(), "dd/MM/yyyy")
                '    .NumeroGiorni = numero_giorni
                '    .data_uscita = data_uscita
                '    .data_rientro = data_rientro
                '    .CCOMPAGNIA = "DEFAULT"
                '    .CCIMPORTO = importo_prepagato
                '    .Totale = importo_prepagato
                '    .CodiceStazionePickUp = stazione
                '    .CodiceStazioneDropOff = stazione_off
                '    .NumPrenotazione = num_prenotazione

                '    Dim AnnoFattura As Integer = Year(.CCDATA)
                '    Dim CodiceFattura As Integer = funzioni_comuni.GeneraIdFattura_Web(AnnoFattura) ' attenzione sino a quando non inserisco il record se uso l'algoritmo del max per anno non va bene... tranne a bloccare la tabella
                '    Dim id_pagamento As String = id_pagamenti_extra
                '    'HttpContext.Current.Trace.Write("C: ")
                '    funzioni_comuni.SalvaFattura_Web(CodiceFattura, id_ditta, num_prenotazione, miaPrenotazioneWeb, id_pagamento)
                '    'funzioni_comuni.SalvaFatturaAres_Web(CodiceFattura, id_ditta, num_prenotazione, miaPrenotazioneWeb, id_pagamento)
                'End With

                Return num_prenotazione

                'Response.Redirect("riepilogo.aspx?prenotazione=" & id_preventivo & "&num=" & id_prenotazione)           
            Catch ex As Exception
                Return "error:" & sqlStr2 ' ex.Message
            End Try
        End Function


    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Session("chiamata_sito_web") = 2


        Dim sdate As String = Date.Now.Hour & "h : " & Date.Now.Minute & "m : " & Date.Now.Second & "s : " & Date.Now.Millisecond & "ms"

        Label4.Text = "Start: " & sdate.ToString

        Dim id_preventivo As String = TextBox3.Text 'Session("id_preventivo")


        If Request.QueryString("id") = "1" Then
            result = getElencoVeicoli(stazione_inizio_noleggio, data_inizio_noleggio, ora_inizio_noleggio, stazione_fine_noleggio, data_fine_noleggio, ora_fine_noleggio, eta, id_tip_cliente, lingua, cod_promo)
            result = getListaExtra(id_preventivo, lingua)
        End If

        TextBox2.Text = result




        Dim edate As String = Date.Now.Hour & "h : " & Date.Now.Minute & "m : " & Date.Now.Second & "s : " & Date.Now.Millisecond & "ms"

        Label5.Text = "End  : " & edate.ToString


    End Sub




End Class


