
Partial Class situazione_flotta_old
    Inherits System.Web.UI.Page

    Dim gruppiNormali As New Collection
    Dim gruppiSpeciali As New Collection

    Protected Function getDisponibilita(ByVal classe As String, ByVal stazione As String) As String
        Dim condizioneGruppi As String

        If classe = "Normale" Then
            condizioneGruppi = " ("
            For i = 1 To gruppiNormali.Count
                If i > 1 Then
                    condizioneGruppi = condizioneGruppi & " OR "
                End If

                condizioneGruppi = condizioneGruppi & "gruppi.cod_gruppo='" & gruppiNormali.Item(i) & "'"
            Next
            condizioneGruppi = condizioneGruppi & ")"
        ElseIf classe = "Speciale" Then
            condizioneGruppi = " ("
            For i = 1 To gruppiSpeciali.Count
                If i > 1 Then
                    condizioneGruppi = condizioneGruppi & " OR "
                End If

                condizioneGruppi = condizioneGruppi & "gruppi.cod_gruppo='" & gruppiSpeciali.Item(i) & "'"
            Next
            condizioneGruppi = condizioneGruppi & ")"
        ElseIf classe = "G" Then
            condizioneGruppi = " (gruppi.cod_gruppo='G')"
        ElseIf classe = "U" Then
            condizioneGruppi = " (gruppi.cod_gruppo='U')"
        End If

        Dim sqlStr As String = "SELECT ISNULL(COUNT(veicoli.id),'0') As disponbilita FROM veicoli INNER JOIN modelli ON veicoli.id_modello=modelli.id_modello INNER JOIN gruppi ON modelli.id_gruppo=gruppi.id_gruppo WHERE veicoli.id_stazione='" & stazione & "' AND veicoli.disponibile_nolo='1' AND " & condizioneGruppi


        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        getDisponibilita = Cmd.ExecuteScalar

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function
    
    Protected Function getGruppo1(ByVal dataIniziale As DateTime, ByVal stazione As String) As Integer()
        'GRUPPO 1 -- GRUPPI A e B

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)

        Dim data1 As String = funzioni_comuni.getDataDb_senza_orario2(dataIniziale, Request.ServerVariables("HTTP_HOST"))

        Dim sqlStr As String = ""

        Dim gruppo(60) As Integer

        'PREVISTE USCITE DA PRENOTAZIONI--------------------------------------------------------------------------------------------------
        'GRUPPO 1
        gruppo(1) = 0   '08/12
        gruppo(2) = 0   '12/16
        gruppo(3) = 0   '16/20
        gruppo(4) = 0   '20/23
        gruppo(5) = 0   'TOT (giornaliero)
        'GRUPPO 2
        gruppo(16) = 0   '08/12
        gruppo(17) = 0   '12/16
        gruppo(18) = 0   '16/20
        gruppo(19) = 0   '20/23
        gruppo(20) = 0   'TOT (giornaliero)
        'GRUPPO 3
        gruppo(31) = 0   '08/12
        gruppo(32) = 0   '12/16
        gruppo(33) = 0   '16/20
        gruppo(34) = 0   '20/23
        gruppo(35) = 0   'TOT (giornaliero)
        'GRUPPO 4
        gruppo(46) = 0   '08/12
        gruppo(47) = 0   '12/16
        gruppo(48) = 0   '16/20
        gruppo(49) = 0   '20/23
        gruppo(50) = 0   'TOT (giornaliero)

        sqlStr = "SELECT ore_uscita, gruppi.cod_gruppo As gruppo FROM prenotazioni INNER JOIN gruppi ON ISNULL(prenotazioni.ID_GRUPPO_APP,prenotazioni.ID_GRUPPO)=gruppi.id_gruppo WHERE YEAR(PRDATA_OUT)=YEAR('" & data1 & "') AND MONTH(PRDATA_OUT)=MONTH('" & data1 & "') AND DAY(PRDATA_OUT)=DAY('" & data1 & "') AND PRID_stazione_out='" & stazione & "' AND status='0' AND attiva='1' ORDER BY PRDATA_OUT"

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Do While Rs.Read()
            If gruppiNormali.Contains(Rs("gruppo")) Then
                'GRUPPO 1
                If CInt(Rs("ore_uscita")) < 12 Then
                    gruppo(1) = gruppo(1) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 12 And CInt(Rs("ore_uscita")) < 16 Then
                    gruppo(2) = gruppo(2) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 16 And CInt(Rs("ore_uscita")) < 20 Then
                    gruppo(3) = gruppo(3) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 20 And CInt(Rs("ore_uscita")) <= 23 Then
                    gruppo(4) = gruppo(4) + 1
                End If

                gruppo(5) = gruppo(5) + 1
            ElseIf gruppiSpeciali.Contains(Rs("gruppo")) Then
                'GRUPPO 2
                If CInt(Rs("ore_uscita")) < 12 Then
                    gruppo(16) = gruppo(16) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 12 And CInt(Rs("ore_uscita")) < 16 Then
                    gruppo(17) = gruppo(17) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 16 And CInt(Rs("ore_uscita")) < 20 Then
                    gruppo(18) = gruppo(18) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 20 And CInt(Rs("ore_uscita")) <= 23 Then
                    gruppo(19) = gruppo(19) + 1
                End If

                gruppo(20) = gruppo(20) + 1
            ElseIf Rs("gruppo") = "U" Then
                'GRUPPO 3
                If CInt(Rs("ore_uscita")) < 12 Then
                    gruppo(31) = gruppo(31) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 12 And CInt(Rs("ore_uscita")) < 16 Then
                    gruppo(32) = gruppo(32) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 16 And CInt(Rs("ore_uscita")) < 20 Then
                    gruppo(33) = gruppo(33) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 20 And CInt(Rs("ore_uscita")) <= 23 Then
                    gruppo(34) = gruppo(34) + 1
                End If

                gruppo(35) = gruppo(35) + 1
            ElseIf Rs("gruppo") = "G" Then
                'GRUPPO 4
                If CInt(Rs("ore_uscita")) < 12 Then
                    gruppo(46) = gruppo(46) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 12 And CInt(Rs("ore_uscita")) < 16 Then
                    gruppo(47) = gruppo(47) + 1
                ElseIf Hour(Rs("data_uscita")) >= 16 And CInt(Rs("ore_uscita")) < 20 Then
                    gruppo(48) = gruppo(48) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 20 And CInt(Rs("ore_uscita")) <= 23 Then
                    gruppo(49) = gruppo(49) + 1
                End If

                gruppo(50) = gruppo(50) + 1
            End If
        Loop

        'RIENTRI DA PRENOTAZIONE e da DDT (presunti)-----------------------------------------------------------------------------------------------
        'GRUPPO 1
        gruppo(6) = 0   '08/12
        gruppo(7) = 0   '12/16
        gruppo(8) = 0   '16/20
        gruppo(9) = 0   '20/23
        gruppo(10) = 0   'TOT (giornaliero)
        'GRUPPO 2
        gruppo(21) = 0   '08/12
        gruppo(22) = 0   '12/16
        gruppo(23) = 0   '16/20
        gruppo(24) = 0   '20/23
        gruppo(25) = 0   'TOT (giornaliero)
        'GRUPPO 3
        gruppo(36) = 0   '08/12
        gruppo(37) = 0   '12/16
        gruppo(38) = 0   '16/20
        gruppo(39) = 0   '20/23
        gruppo(40) = 0   'TOT (giornaliero)
        'GRUPPO 4
        gruppo(51) = 0   '08/12
        gruppo(52) = 0   '12/16
        gruppo(53) = 0   '16/20
        gruppo(54) = 0   '20/23
        gruppo(55) = 0   'TOT (giornaliero)

        sqlStr = "SELECT ore_rientro, gruppi.cod_gruppo As gruppo FROM prenotazioni INNER JOIN gruppi ON ISNULL(prenotazioni.ID_GRUPPO_APP,prenotazioni.ID_GRUPPO)=gruppi.id_gruppo WHERE YEAR(PRDATA_PR)=YEAR('" & data1 & "') AND MONTH(PRDATA_PR)=MONTH('" & data1 & "') AND DAY(PRDATA_PR)=DAY('" & data1 & "') AND PRID_stazione_pr='" & stazione & "' AND status='0' AND attiva='1' ORDER BY PRDATA_PR"

        Dbc.Close()
        Dbc.Open()

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Rs = Cmd.ExecuteReader()

        Do While Rs.Read()
            If gruppiNormali.Contains(Rs("gruppo")) Then
                'GRUPPO 1
                If CInt(Rs("ore_rientro")) < 12 Then
                    gruppo(6) = gruppo(6) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 12 And CInt(Rs("ore_rientro")) < 16 Then
                    gruppo(7) = gruppo(7) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 16 And CInt(Rs("ore_rientro")) < 20 Then
                    gruppo(8) = gruppo(8) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 20 And CInt(Rs("ore_rientro")) <= 23 Then
                    gruppo(9) = gruppo(9) + 1
                End If

                gruppo(10) = gruppo(10) + 1
            ElseIf gruppiSpeciali.Contains(Rs("gruppo")) Then
                'GRUPPO 2
                If CInt(Rs("ore_rientro")) < 12 Then
                    gruppo(21) = gruppo(21) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 12 And CInt(Rs("ore_rientro")) < 16 Then
                    gruppo(22) = gruppo(22) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 16 And CInt(Rs("ore_rientro")) < 20 Then
                    gruppo(23) = gruppo(23) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 20 And CInt(Rs("ore_rientro")) <= 23 Then
                    gruppo(24) = gruppo(24) + 1
                End If

                gruppo(25) = gruppo(25) + 1
            ElseIf Rs("gruppo") = "U" Then
                'GRUPPO 3
                If CInt(Rs("ore_rientro")) < 12 Then
                    gruppo(36) = gruppo(36) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 12 And CInt(Rs("ore_rientro")) < 16 Then
                    gruppo(37) = gruppo(37) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 16 And CInt(Rs("ore_rientro")) < 20 Then
                    gruppo(38) = gruppo(38) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 20 And CInt(Rs("ore_rientro")) <= 23 Then
                    gruppo(39) = gruppo(39) + 1
                End If

                gruppo(40) = gruppo(40) + 1
            ElseIf Rs("gruppo") = "G" Then
                'GRUPPO 4
                If CInt(Rs("ore_rientro")) < 12 Then
                    gruppo(51) = gruppo(51) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 12 And CInt(Rs("ore_rientro")) < 16 Then
                    gruppo(52) = gruppo(52) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 16 And CInt(Rs("ore_rientro")) < 20 Then
                    gruppo(53) = gruppo(53) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 20 And CInt(Rs("ore_rientro")) <= 23 Then
                    gruppo(54) = gruppo(54) + 1
                End If

                gruppo(55) = gruppo(55) + 1
            End If
        Loop
        '---------------------------------------------------------------------------------------------------------------------------------

        'RIENTRI DA CONTRATTO (previsti)--------------------------------------------------------------------------------------------------
        'GRUPPO 1
        gruppo(11) = 0   '08/12
        gruppo(12) = 0   '12/16
        gruppo(13) = 0   '16/20
        gruppo(14) = 0   '20/23
        gruppo(15) = 0   'TOT (giornaliero)
        'GRUPPO 2
        gruppo(26) = 0   '08/12
        gruppo(27) = 0   '12/16
        gruppo(28) = 0   '16/20
        gruppo(29) = 0   '20/23
        gruppo(30) = 0   'TOT (giornaliero)
        'GRUPPO 3
        gruppo(41) = 0   '08/12
        gruppo(42) = 0   '12/16
        gruppo(43) = 0   '16/20
        gruppo(44) = 0   '20/23
        gruppo(45) = 0   'TOT (giornaliero)
        'GRUPPO 3
        gruppo(56) = 0   '08/12
        gruppo(57) = 0   '12/16
        gruppo(58) = 0   '16/20
        gruppo(59) = 0   '20/23
        gruppo(60) = 0   'TOT (giornaliero)


        sqlStr = "SELECT data_presunto_rientro, gruppi.cod_gruppo As gruppo FROM contratti INNER JOIN gruppi ON ISNULL(contratti.id_gruppo_app,contratti.id_gruppo_auto)=gruppi.id_gruppo WHERE YEAR(data_presunto_rientro)=YEAR('" & data1 & "') AND MONTH(data_presunto_rientro)=MONTH('" & data1 & "') AND DAY(data_presunto_rientro)=DAY('" & data1 & "') AND id_stazione_presunto_rientro='" & stazione & "' AND status='2' AND contratti.attivo='1' ORDER BY data_presunto_rientro"

        Dbc.Close()
        Dbc.Open()

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Rs = Cmd.ExecuteReader()


        Do While Rs.Read()
            'GRUPPO 1
            If gruppiNormali.Contains(Rs("gruppo")) Then
                If Hour(Rs("data_presunto_rientro")) < 12 Then
                    gruppo(11) = gruppo(11) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 12 And Hour(Rs("data_presunto_rientro")) < 16 Then
                    gruppo(12) = gruppo(12) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 16 And Hour(Rs("data_presunto_rientro")) < 20 Then
                    gruppo(13) = gruppo(13) + 1
                ElseIf (Hour(Rs("data_presunto_rientro")) >= 20 And Hour(Rs("data_presunto_rientro")) <= 23) Then
                    gruppo(14) = gruppo(14) + 1
                End If

                gruppo(15) = gruppo(15) + 1
            ElseIf gruppiSpeciali.Contains(Rs("gruppo")) Then
                'GRUPPO 2
                If Hour(Rs("data_presunto_rientro")) < 12 Then
                    gruppo(26) = gruppo(26) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 12 And Hour(Rs("data_presunto_rientro")) < 16 Then
                    gruppo(27) = gruppo(27) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 16 And Hour(Rs("data_presunto_rientro")) < 20 Then
                    gruppo(28) = gruppo(28) + 1
                ElseIf (Hour(Rs("data_presunto_rientro")) >= 20 And Hour(Rs("data_presunto_rientro")) <= 23) Then
                    gruppo(29) = gruppo(29) + 1
                End If

                gruppo(30) = gruppo(30) + 1
            ElseIf Rs("gruppo") = "U" Then
                'GRUPPO 3
                If Hour(Rs("data_rientro")) < 12 Then
                    gruppo(41) = gruppo(41) + 1
                ElseIf Hour(Rs("data_rientro")) >= 12 And Hour(Rs("data_rientro")) < 16 Then
                    gruppo(42) = gruppo(42) + 1
                ElseIf Hour(Rs("data_rientro")) >= 16 And Hour(Rs("data_rientro")) < 20 Then
                    gruppo(43) = gruppo(43) + 1
                ElseIf (Hour(Rs("data_rientro")) >= 20 And Hour(Rs("data_rientro")) <= 23) Then
                    gruppo(44) = gruppo(44) + 1
                End If

                gruppo(45) = gruppo(45) + 1
            ElseIf Rs("gruppo") = "G" Then
                'GRUPPO 4
                If Hour(Rs("data_rientro")) < 12 Then
                    gruppo(56) = gruppo(56) + 1
                ElseIf Hour(Rs("data_rientro")) >= 12 And Hour(Rs("data_rientro")) < 16 Then
                    gruppo(57) = gruppo(57) + 1
                ElseIf Hour(Rs("data_rientro")) >= 16 And Hour(Rs("data_rientro")) < 20 Then
                    gruppo(58) = gruppo(58) + 1
                ElseIf (Hour(Rs("data_rientro")) >= 20 And Hour(Rs("data_rientro")) <= 23) Then
                    gruppo(59) = gruppo(59) + 1
                End If

                gruppo(60) = gruppo(60) + 1
            End If
        Loop
        '---------------------------------------------------------------------------------------------------------------------------------

        getGruppo1 = gruppo

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Function

    Protected Function getGruppiNormaliSpeciali(ByVal tipo As String) As Collection
        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim collezione_gruppi As New Collection
        Dim sqlStr As String

        If tipo = "normali" Then
            sqlStr = "SELECT cod_gruppo FROM gruppi WHERE attivo='1' AND normale='1' AND cod_gruppo <> 'G' AND cod_gruppo <> 'U' ORDER BY cod_gruppo"
        ElseIf tipo = "speciali" Then
            sqlStr = "SELECT cod_gruppo FROM gruppi WHERE attivo='1' AND speciale='1' AND cod_gruppo <> 'G' AND cod_gruppo <> 'U' ORDER BY cod_gruppo"
        End If

        Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Dim normali As String = ""
        Dim speciali As String = ""

        Do While Rs.Read()
            collezione_gruppi.Add(Rs("cod_gruppo"), Rs("cod_gruppo"))

            'SETTAGGIO BOX OVER
            If tipo = "normali" Then
                normali = normali & Rs("cod_gruppo") & " - "
            ElseIf tipo = "speciali" Then
                speciali = speciali & Rs("cod_gruppo") & " - "
            End If
        Loop

        If tipo = "normali" Then
            boxOverNormali.Body = Left(normali, Len(normali) - 2)
        ElseIf tipo = "speciali" Then
            boxOverSpeciali.Body = Left(speciali, Len(speciali) - 2)
        End If

        getGruppiNormaliSpeciali = collezione_gruppi

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack() Then
            gruppiNormali.Clear()
            gruppiSpeciali.Clear()

            gruppiNormali = getGruppiNormaliSpeciali("normali")  'SETTA ANCHE IL BOX OVER
            gruppiSpeciali = getGruppiNormaliSpeciali("speciali")

        End If
    End Sub

End Class
