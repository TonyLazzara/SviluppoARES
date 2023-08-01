Imports System.IO
Partial Class situazione_flotta
    Inherits System.Web.UI.Page

    Dim gruppiNormali As New Collection
    Dim gruppiSpeciali As New Collection

    'Tony 20/06/2022
    Dim ArrayMinGiornaliero(29) As String
    Dim ArrayData(29) As String
    Dim ArrayGruppoA(30) As String
    Dim ArrayGruppoA1(30) As String
    Dim ArrayGruppoB(30) As String
    Dim ArrayGruppoB1(30) As String
    Dim ArrayGruppoC(30) As String
    Dim ArrayGruppoD(30) As String
    Dim ArrayGruppoF(30) As String
    Dim ArrayGruppoH(30) As String
    Dim ArrayGruppoL(30) As String
    Dim ArrayGruppoM(30) As String
    Dim ArrayGruppoS(30) As String
    Dim ArrayGruppoV(30) As String
    

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
        End If
        Dim sqlStr As String 'aggiornato il 01.10.2021
        sqlStr = "SELECT ISNULL(COUNT(veicoli.id),'0') As disponbilita FROM veicoli INNER JOIN modelli ON veicoli.id_modello=modelli.id_modello INNER JOIN gruppi ON "
        sqlStr += "modelli.id_gruppo=gruppi.id_gruppo WHERE (veicoli.disponibile_nolo='1' or veicoli.da_rifornire='1') AND veicoli.id_stazione='" & stazione & "' AND " & condizioneGruppi

        'Tony 30-05-2022
        'sqlStr = "SELECT ISNULL(COUNT(veicoli.id),'0') As disponbilita FROM veicoli INNER JOIN modelli ON veicoli.id_modello=modelli.id_modello INNER JOIN gruppi ON "
        'sqlStr += "modelli.id_gruppo=gruppi.id_gruppo WHERE (veicoli.disponibile_nolo='1' or veicoli.da_rifornire='1' or veicoli.da_lavare='1') AND veicoli.id_stazione='" & stazione & "' AND " & condizioneGruppi


        'Response.Write(sqlStr & "<br/>")


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

        Dim gruppo(72) As Integer

        'PREVISTE USCITE DA PRENOTAZIONI------------ PR.U. -----------------------------------------------------
        'GRUPPO 1
        gruppo(1) = 0   '00/08
        gruppo(2) = 0   '08/09
        gruppo(3) = 0   '09/10
        gruppo(4) = 0   '10/11
        gruppo(5) = 0   '11/12
        gruppo(6) = 0   '12/13
        gruppo(7) = 0   '13/14
        gruppo(8) = 0   '14/15
        gruppo(9) = 0   '15/16
        gruppo(10) = 0  '16/17
        gruppo(11) = 0  '17/18
        gruppo(12) = 0  '18/19
        gruppo(13) = 0  '19/20
        gruppo(14) = 0  '20/24
        gruppo(15) = 0  'TOT (giornaliero)

        'GRUPPO 2
        gruppo(46) = 0   '00/08
        gruppo(47) = 0   '08/10
        gruppo(48) = 0   '10/12
        gruppo(49) = 0   '12/14
        gruppo(50) = 0   '14/16
        gruppo(51) = 0   '16/18
        gruppo(52) = 0   '18/20
        gruppo(53) = 0   '20/24
        gruppo(54) = 0   'TOT (giornaliero)

        sqlStr = "SELECT ore_uscita, gruppi.cod_gruppo As gruppo FROM prenotazioni INNER JOIN gruppi ON ISNULL(prenotazioni.ID_GRUPPO_APP,prenotazioni.ID_GRUPPO)=gruppi.id_gruppo WHERE YEAR(PRDATA_OUT)=YEAR('" & data1 & "') AND MONTH(PRDATA_OUT)=MONTH('" & data1 & "') AND DAY(PRDATA_OUT)=DAY('" & data1 & "') AND PRID_stazione_out='" & stazione & "' AND status='0' AND attiva='1' ORDER BY PRDATA_OUT"

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        Do While Rs.Read()
            If gruppiNormali.Contains(Rs("gruppo")) Then
                'GRUPPO 1
                If CInt(Rs("ore_uscita")) < 8 Then
                    gruppo(1) = gruppo(1) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 8 And CInt(Rs("ore_uscita")) < 9 Then
                    gruppo(2) = gruppo(2) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 9 And CInt(Rs("ore_uscita")) < 10 Then
                    gruppo(3) = gruppo(3) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 10 And CInt(Rs("ore_uscita")) < 11 Then
                    gruppo(4) = gruppo(4) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 11 And CInt(Rs("ore_uscita")) < 12 Then
                    gruppo(5) = gruppo(5) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 12 And CInt(Rs("ore_uscita")) < 13 Then
                    gruppo(6) = gruppo(6) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 13 And CInt(Rs("ore_uscita")) < 14 Then
                    gruppo(7) = gruppo(7) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 14 And CInt(Rs("ore_uscita")) < 15 Then
                    gruppo(8) = gruppo(8) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 15 And CInt(Rs("ore_uscita")) < 16 Then
                    gruppo(9) = gruppo(9) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 16 And CInt(Rs("ore_uscita")) < 17 Then
                    gruppo(10) = gruppo(10) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 17 And CInt(Rs("ore_uscita")) < 18 Then
                    gruppo(11) = gruppo(11) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 18 And CInt(Rs("ore_uscita")) < 19 Then
                    gruppo(12) = gruppo(12) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 19 And CInt(Rs("ore_uscita")) < 20 Then
                    gruppo(13) = gruppo(13) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 20 And CInt(Rs("ore_uscita")) <= 23 Then
                    gruppo(14) = gruppo(14) + 1
                End If

                gruppo(15) = gruppo(15) + 1
            ElseIf gruppiSpeciali.Contains(Rs("gruppo")) Then
                'GRUPPO 2
                If CInt(Rs("ore_uscita")) < 8 Then
                    gruppo(46) = gruppo(46) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 8 And CInt(Rs("ore_uscita")) < 10 Then
                    gruppo(47) = gruppo(47) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 10 And CInt(Rs("ore_uscita")) < 12 Then
                    gruppo(48) = gruppo(48) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 12 And CInt(Rs("ore_uscita")) < 14 Then
                    gruppo(49) = gruppo(49) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 14 And CInt(Rs("ore_uscita")) < 16 Then
                    gruppo(50) = gruppo(50) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 16 And CInt(Rs("ore_uscita")) < 18 Then
                    gruppo(51) = gruppo(51) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 18 And CInt(Rs("ore_uscita")) < 20 Then
                    gruppo(52) = gruppo(52) + 1
                ElseIf CInt(Rs("ore_uscita")) >= 20 And CInt(Rs("ore_uscita")) <= 23 Then
                    gruppo(53) = gruppo(53) + 1
                End If

                gruppo(54) = gruppo(54) + 1
            End If
        Loop

        'RIENTRI DA PRENOTAZIONE e da DDT (presunti)-------------- R.P. ---   e aggiunti quelli ODL 05.02.2021  -----------------------------------------------------
        'GRUPPO 1
        gruppo(16) = 0   '00/08
        gruppo(17) = 0   '08/09
        gruppo(18) = 0   '09/10
        gruppo(19) = 0   '10/11
        gruppo(20) = 0   '11/12
        gruppo(21) = 0   '12/13
        gruppo(22) = 0   '13/14
        gruppo(23) = 0   '14/15
        gruppo(24) = 0   '15/16
        gruppo(25) = 0  '16/17
        gruppo(26) = 0  '17/18
        gruppo(27) = 0  '18/19
        gruppo(28) = 0  '19/20
        gruppo(29) = 0  '20/24
        gruppo(30) = 0  'TOT (giornaliero)
        'GRUPPO 2
        gruppo(55) = 0   '00/08
        gruppo(56) = 0   '08/10
        gruppo(57) = 0   '10/12
        gruppo(58) = 0   '12/14
        gruppo(59) = 0   '14/16
        gruppo(60) = 0   '16/18
        gruppo(61) = 0   '18/20
        gruppo(62) = 0   '20/24
        gruppo(63) = 0   'TOT (giornaliero)

        sqlStr = "SELECT ore_rientro, gruppi.cod_gruppo As gruppo FROM prenotazioni INNER JOIN gruppi ON ISNULL(prenotazioni.ID_GRUPPO_APP,prenotazioni.ID_GRUPPO)=gruppi.id_gruppo WHERE YEAR(PRDATA_PR)=YEAR('" & data1 & "') AND MONTH(PRDATA_PR)=MONTH('" & data1 & "') AND DAY(PRDATA_PR)=DAY('" & data1 & "') AND PRID_stazione_pr='" & stazione & "' AND status='0' AND attiva='1' ORDER BY PRDATA_PR"

        Dbc.Close()
        Dbc.Open()

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Rs = Cmd.ExecuteReader()

        Do While Rs.Read()
            If gruppiNormali.Contains(Rs("gruppo")) Then
                'GRUPPO 1
                If CInt(Rs("ore_rientro")) < 8 Then
                    gruppo(16) = gruppo(16) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 8 And CInt(Rs("ore_rientro")) < 9 Then
                    gruppo(17) = gruppo(17) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 9 And CInt(Rs("ore_rientro")) < 10 Then
                    gruppo(18) = gruppo(18) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 10 And CInt(Rs("ore_rientro")) < 11 Then
                    gruppo(19) = gruppo(19) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 11 And CInt(Rs("ore_rientro")) < 12 Then
                    gruppo(20) = gruppo(20) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 12 And CInt(Rs("ore_rientro")) < 13 Then
                    gruppo(21) = gruppo(21) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 13 And CInt(Rs("ore_rientro")) < 14 Then
                    gruppo(22) = gruppo(22) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 14 And CInt(Rs("ore_rientro")) < 15 Then
                    gruppo(23) = gruppo(23) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 15 And CInt(Rs("ore_rientro")) < 16 Then
                    gruppo(24) = gruppo(24) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 16 And CInt(Rs("ore_rientro")) < 17 Then
                    gruppo(25) = gruppo(25) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 17 And CInt(Rs("ore_rientro")) < 18 Then
                    gruppo(26) = gruppo(26) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 18 And CInt(Rs("ore_rientro")) < 19 Then
                    gruppo(27) = gruppo(27) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 19 And CInt(Rs("ore_rientro")) < 20 Then
                    gruppo(28) = gruppo(28) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 20 And CInt(Rs("ore_rientro")) <= 23 Then
                    gruppo(29) = gruppo(29) + 1
                End If

                gruppo(30) = gruppo(30) + 1
            ElseIf gruppiSpeciali.Contains(Rs("gruppo")) Then
                'GRUPPO 2
                If CInt(Rs("ore_rientro")) < 8 Then
                    gruppo(55) = gruppo(55) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 8 And CInt(Rs("ore_rientro")) < 10 Then
                    gruppo(56) = gruppo(56) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 10 And CInt(Rs("ore_rientro")) < 12 Then
                    gruppo(57) = gruppo(57) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 12 And CInt(Rs("ore_rientro")) < 14 Then
                    gruppo(58) = gruppo(58) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 14 And CInt(Rs("ore_rientro")) < 16 Then
                    gruppo(59) = gruppo(59) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 16 And CInt(Rs("ore_rientro")) < 18 Then
                    gruppo(60) = gruppo(60) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 18 And CInt(Rs("ore_rientro")) < 20 Then
                    gruppo(61) = gruppo(61) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 20 And CInt(Rs("ore_rientro")) <= 23 Then
                    gruppo(62) = gruppo(62) + 1
                End If

                gruppo(63) = gruppo(63) + 1
            End If
        Loop


        ''### AGGIUNGE i veicoli in ODL in presunto rientro 05.02.2021 ma se lo stato non è chiuso 25.02.2021

        'sqlStr = "SELECT ore_rientro, gruppi.cod_gruppo As gruppo FROM prenotazioni INNER JOIN gruppi ON ISNULL(prenotazioni.ID_GRUPPO_APP,prenotazioni.ID_GRUPPO)=gruppi.id_gruppo WHERE YEAR(PRDATA_PR)=YEAR('" & data1 & "') AND MONTH(PRDATA_PR)=MONTH('" & data1 & "') AND DAY(PRDATA_PR)=DAY('" & data1 & "') AND PRID_stazione_pr='" & stazione & "' AND status='0' AND attiva='1' ORDER BY PRDATA_PR "

        sqlStr = "SELECT TOP(1) DATEPART(HOUR, odl.data_previsto_rientro) as ore_rientro, GRUPPI.cod_gruppo as gruppo, odl.id_stato_odl, odl.data_rientro, odl.id "
        sqlStr += "From odl INNER JOIN veicoli ON odl.id_veicolo = veicoli.id INNER JOIN "
        sqlStr += "MODELLI ON veicoli.id_modello = MODELLI.ID_MODELLO INNER JOIN "
        sqlStr += "GRUPPI ON MODELLI.ID_Gruppo = GRUPPI.ID_gruppo "
        sqlStr += "WHERE (YEAR(odl.data_previsto_rientro) = YEAR('" & data1 & "')) "
        sqlStr += "And (MONTH(odl.data_previsto_rientro) = MONTH('" & data1 & "')) "
        sqlStr += "And (DAY(odl.data_previsto_rientro) = DAY('" & data1 & "')) AND (odl.id_stazione_previsto_rientro = '" & stazione & "') "
        sqlStr += "ORDER BY odl.id desc, odl.data_previsto_rientro desc, id_stato_odl desc;"

        Dbc.Close()
        Dbc.Open()

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Rs = Cmd.ExecuteReader()

        Do While Rs.Read()

            If Rs!id_stato_odl <> 9 Then    'esclude i record dove l'ODL è chiuso stato = 9

                If gruppiNormali.Contains(Rs("gruppo")) Then
                    'GRUPPO 1
                    If CInt(Rs("ore_rientro")) < 8 Then
                        gruppo(16) = gruppo(16) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 8 And CInt(Rs("ore_rientro")) < 9 Then
                        gruppo(17) = gruppo(17) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 9 And CInt(Rs("ore_rientro")) < 10 Then
                        gruppo(18) = gruppo(18) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 10 And CInt(Rs("ore_rientro")) < 11 Then
                        gruppo(19) = gruppo(19) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 11 And CInt(Rs("ore_rientro")) < 12 Then
                        gruppo(20) = gruppo(20) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 12 And CInt(Rs("ore_rientro")) < 13 Then
                        gruppo(21) = gruppo(21) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 13 And CInt(Rs("ore_rientro")) < 14 Then
                        gruppo(22) = gruppo(22) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 14 And CInt(Rs("ore_rientro")) < 15 Then
                        gruppo(23) = gruppo(23) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 15 And CInt(Rs("ore_rientro")) < 16 Then
                        gruppo(24) = gruppo(24) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 16 And CInt(Rs("ore_rientro")) < 17 Then
                        gruppo(25) = gruppo(25) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 17 And CInt(Rs("ore_rientro")) < 18 Then
                        gruppo(26) = gruppo(26) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 18 And CInt(Rs("ore_rientro")) < 19 Then
                        gruppo(27) = gruppo(27) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 19 And CInt(Rs("ore_rientro")) < 20 Then
                        gruppo(28) = gruppo(28) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 20 And CInt(Rs("ore_rientro")) <= 23 Then
                        gruppo(29) = gruppo(29) + 1
                    End If
                    gruppo(30) = gruppo(30) + 1

                ElseIf gruppiSpeciali.Contains(Rs("gruppo")) Then

                    'GRUPPO 2
                    If CInt(Rs("ore_rientro")) < 8 Then
                        gruppo(55) = gruppo(55) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 8 And CInt(Rs("ore_rientro")) < 10 Then
                        gruppo(56) = gruppo(56) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 10 And CInt(Rs("ore_rientro")) < 12 Then
                        gruppo(57) = gruppo(57) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 12 And CInt(Rs("ore_rientro")) < 14 Then
                        gruppo(58) = gruppo(58) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 14 And CInt(Rs("ore_rientro")) < 16 Then
                        gruppo(59) = gruppo(59) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 16 And CInt(Rs("ore_rientro")) < 18 Then
                        gruppo(60) = gruppo(60) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 18 And CInt(Rs("ore_rientro")) < 20 Then
                        gruppo(61) = gruppo(61) + 1
                    ElseIf CInt(Rs("ore_rientro")) >= 20 And CInt(Rs("ore_rientro")) <= 23 Then
                        gruppo(62) = gruppo(62) + 1
                    End If

                    gruppo(63) = gruppo(63) + 1

                End If
            End If


        Loop

        ''### END AGGIUNGE i veicoli in ODL in presunto rientro 05.02.2021

        '---------------------------------------------------------------------------------------------------------------------------------
        'Tony
        ''### AGGIUNGE i veicoli a Lavaggio 30.05.2022

        'sqlStr = "SELECT TOP(1) DATEPART(HOUR, odl.data_previsto_rientro) as ore_rientro, GRUPPI.cod_gruppo as gruppo, odl.id_stato_odl, odl.data_rientro, odl.id "
        'sqlStr += "From odl INNER JOIN veicoli ON odl.id_veicolo = veicoli.id INNER JOIN "
        'sqlStr += "MODELLI ON veicoli.id_modello = MODELLI.ID_MODELLO INNER JOIN "
        'sqlStr += "GRUPPI ON MODELLI.ID_Gruppo = GRUPPI.ID_gruppo "
        'sqlStr += "WHERE (YEAR(odl.data_previsto_rientro) = YEAR('" & data1 & "')) "
        'sqlStr += "And (MONTH(odl.data_previsto_rientro) = MONTH('" & data1 & "')) "
        'sqlStr += "And (DAY(odl.data_previsto_rientro) = DAY('" & data1 & "')) AND (odl.id_stazione_previsto_rientro = '" & stazione & "') "
        'sqlStr += "ORDER BY odl.id desc, odl.data_previsto_rientro desc, id_stato_odl desc;"

        sqlStr = "SELECT  DATEPART(HOUR, lavaggi.data_presunto_rientro) as ore_rientro, GRUPPI.cod_gruppo as gruppo, lavaggi.stato, lavaggi.data_presunto_rientro, lavaggi.id From lavaggi INNER JOIN veicoli ON lavaggi.id_veicolo = veicoli.id INNER JOIN MODELLI ON veicoli.id_modello = MODELLI.ID_MODELLO INNER JOIN GRUPPI ON MODELLI.ID_Gruppo = GRUPPI.ID_gruppo WHERE (YEAR(lavaggi.data_presunto_rientro) = YEAR('" & data1 & "')) And (MONTH(lavaggi.data_presunto_rientro) = MONTH('" & data1 & "')) And (DAY(lavaggi.data_presunto_rientro) = DAY('" & data1 & "')) AND (lavaggi.id_stazione_uscita = '" & stazione & "') AND lavaggi.stato = 0 ORDER BY lavaggi.id desc, lavaggi.data_presunto_rientro desc, stato desc;"
        'Response.Write(sqlStr & "<br>")

        Dbc.Close()
        Dbc.Open()

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)

        Rs = Cmd.ExecuteReader()

        Do While Rs.Read()

            If gruppiNormali.Contains(Rs("gruppo")) Then
                'GRUPPO 1
                If CInt(Rs("ore_rientro")) < 8 Then
                    gruppo(16) = gruppo(16) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 8 And CInt(Rs("ore_rientro")) < 9 Then
                    gruppo(17) = gruppo(17) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 9 And CInt(Rs("ore_rientro")) < 10 Then
                    gruppo(18) = gruppo(18) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 10 And CInt(Rs("ore_rientro")) < 11 Then
                    gruppo(19) = gruppo(19) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 11 And CInt(Rs("ore_rientro")) < 12 Then
                    gruppo(20) = gruppo(20) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 12 And CInt(Rs("ore_rientro")) < 13 Then
                    gruppo(21) = gruppo(21) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 13 And CInt(Rs("ore_rientro")) < 14 Then
                    gruppo(22) = gruppo(22) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 14 And CInt(Rs("ore_rientro")) < 15 Then
                    gruppo(23) = gruppo(23) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 15 And CInt(Rs("ore_rientro")) < 16 Then
                    gruppo(24) = gruppo(24) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 16 And CInt(Rs("ore_rientro")) < 17 Then
                    gruppo(25) = gruppo(25) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 17 And CInt(Rs("ore_rientro")) < 18 Then
                    gruppo(26) = gruppo(26) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 18 And CInt(Rs("ore_rientro")) < 19 Then
                    gruppo(27) = gruppo(27) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 19 And CInt(Rs("ore_rientro")) < 20 Then
                    gruppo(28) = gruppo(28) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 20 And CInt(Rs("ore_rientro")) <= 23 Then
                    gruppo(29) = gruppo(29) + 1
                End If
                gruppo(30) = gruppo(30) + 1

            ElseIf gruppiSpeciali.Contains(Rs("gruppo")) Then

                'GRUPPO 2
                If CInt(Rs("ore_rientro")) < 8 Then
                    gruppo(55) = gruppo(55) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 8 And CInt(Rs("ore_rientro")) < 10 Then
                    gruppo(56) = gruppo(56) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 10 And CInt(Rs("ore_rientro")) < 12 Then
                    gruppo(57) = gruppo(57) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 12 And CInt(Rs("ore_rientro")) < 14 Then
                    gruppo(58) = gruppo(58) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 14 And CInt(Rs("ore_rientro")) < 16 Then
                    gruppo(59) = gruppo(59) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 16 And CInt(Rs("ore_rientro")) < 18 Then
                    gruppo(60) = gruppo(60) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 18 And CInt(Rs("ore_rientro")) < 20 Then
                    gruppo(61) = gruppo(61) + 1
                ElseIf CInt(Rs("ore_rientro")) >= 20 And CInt(Rs("ore_rientro")) <= 23 Then
                    gruppo(62) = gruppo(62) + 1
                End If

                gruppo(63) = gruppo(63) + 1

            End If



        Loop

            ''### END AGGIUNGE i veicoli a Lavaggio 30.05.2022

            '---------------------------------------------------------------------------------------------------------------------------------












            ''### AGGIUNGE i veicoli in Rifornimento 25.02.2021








            ''### END AGGIUNGE i veicoli in Rifornimento 25.02.2021

            '---------------------------------------------------------------------------------------------------------------------------------


            'RIENTRI DA CONTRATTO (previsti)--------------------- R.C. ---------------------------------------------------
            'GRUPPO 1
        gruppo(31) = 0   '00/08
        gruppo(32) = 0   '08/09
        gruppo(33) = 0   '09/10
        gruppo(34) = 0   '10/11
        gruppo(35) = 0   '11/12
        gruppo(36) = 0   '12/13
        gruppo(37) = 0   '13/14
        gruppo(38) = 0   '14/15
        gruppo(39) = 0   '15/16
        gruppo(40) = 0  '16/17
        gruppo(41) = 0  '17/18
        gruppo(42) = 0  '18/19
        gruppo(43) = 0  '19/20
        gruppo(44) = 0  '20/24
        gruppo(45) = 0  'TOT (giornaliero)
            'GRUPPO 2
        gruppo(64) = 0   '00/08
        gruppo(65) = 0   '08/10
        gruppo(66) = 0   '10/12
        gruppo(67) = 0   '12/14
        gruppo(68) = 0   '14/16
        gruppo(69) = 0   '16/18
        gruppo(70) = 0   '18/20
        gruppo(71) = 0   '20/24
        gruppo(72) = 0   'TOT (giornaliero)


        sqlStr = "SELECT data_presunto_rientro, gruppi.cod_gruppo As gruppo FROM contratti INNER JOIN gruppi ON ISNULL(contratti.id_gruppo_app,contratti.id_gruppo_auto)=gruppi.id_gruppo WHERE YEAR(data_presunto_rientro)=YEAR('" & data1 & "') AND MONTH(data_presunto_rientro)=MONTH('" & data1 & "') AND DAY(data_presunto_rientro)=DAY('" & data1 & "') AND id_stazione_presunto_rientro='" & stazione & "' AND status='2' AND contratti.attivo='1' ORDER BY data_presunto_rientro"


        Dbc.Close()
        Dbc.Open()

        Cmd = New Data.SqlClient.SqlCommand(sqlStr, Dbc)
        Rs = Cmd.ExecuteReader()


        Do While Rs.Read()
                'GRUPPO 1
            If gruppiNormali.Contains(Rs("gruppo")) Then
                If Hour(Rs("data_presunto_rientro")) < 8 Then
                    gruppo(31) = gruppo(31) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 8 And Hour(Rs("data_presunto_rientro")) < 9 Then
                    gruppo(32) = gruppo(32) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 9 And Hour(Rs("data_presunto_rientro")) < 10 Then
                    gruppo(33) = gruppo(33) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 10 And Hour(Rs("data_presunto_rientro")) < 11 Then
                    gruppo(34) = gruppo(34) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 11 And Hour(Rs("data_presunto_rientro")) < 12 Then
                    gruppo(35) = gruppo(35) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 12 And Hour(Rs("data_presunto_rientro")) < 13 Then
                    gruppo(36) = gruppo(36) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 13 And Hour(Rs("data_presunto_rientro")) < 14 Then
                    gruppo(37) = gruppo(37) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 14 And Hour(Rs("data_presunto_rientro")) < 15 Then
                    gruppo(38) = gruppo(38) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 15 And Hour(Rs("data_presunto_rientro")) < 16 Then
                    gruppo(39) = gruppo(39) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 16 And Hour(Rs("data_presunto_rientro")) < 17 Then
                    gruppo(40) = gruppo(40) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 17 And Hour(Rs("data_presunto_rientro")) < 18 Then
                    gruppo(41) = gruppo(41) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 18 And Hour(Rs("data_presunto_rientro")) < 19 Then
                    gruppo(42) = gruppo(42) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 19 And Hour(Rs("data_presunto_rientro")) < 20 Then
                    gruppo(43) = gruppo(43) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 20 And Hour(Rs("data_presunto_rientro")) <= 23 Then
                    gruppo(44) = gruppo(44) + 1
                    End If

                gruppo(45) = gruppo(45) + 1
            ElseIf gruppiSpeciali.Contains(Rs("gruppo")) Then
                    'GRUPPO 2
                If Hour(Rs("data_presunto_rientro")) < 8 Then
                    gruppo(64) = gruppo(64) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 8 And Hour(Rs("data_presunto_rientro")) < 10 Then
                    gruppo(65) = gruppo(65) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 10 And Hour(Rs("data_presunto_rientro")) < 12 Then
                    gruppo(66) = gruppo(66) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 12 And Hour(Rs("data_presunto_rientro")) < 14 Then
                    gruppo(67) = gruppo(67) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 14 And Hour(Rs("data_presunto_rientro")) < 16 Then
                    gruppo(68) = gruppo(68) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 16 And Hour(Rs("data_presunto_rientro")) < 18 Then
                    gruppo(69) = gruppo(69) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 18 And Hour(Rs("data_presunto_rientro")) < 20 Then
                    gruppo(70) = gruppo(70) + 1
                ElseIf Hour(Rs("data_presunto_rientro")) >= 20 And Hour(Rs("data_presunto_rientro")) <= 23 Then
                    gruppo(71) = gruppo(71) + 1
                    End If

                gruppo(72) = gruppo(72) + 1
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
            sqlStr = "SELECT cod_gruppo FROM gruppi WHERE attivo='1' AND normale='1' ORDER BY cod_gruppo"
        ElseIf tipo = "speciali" Then
            sqlStr = "SELECT cod_gruppo FROM gruppi WHERE attivo='1' AND speciale='1' ORDER BY cod_gruppo"
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

            If txtDaData.Text <> "" Then
                If txtDaData.Text <> Now Then
                    If (DateDiff("d", txtDaData.Text, Today)) > 0 Then
                        txtDaData.Text = Today
                    End If
                End If
            End If
            

        End If
    End Sub

    Private Sub btnVisualizza_Click(sender As Object, e As EventArgs) Handles btnVisualizza.Click

    End Sub

    'Tony 20/06/2022
    Protected Sub btnEsporta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEsporta.Click       
        Dim StrArrayData(2) As String
        Dim DataElaborazione2 As String

        Dim DataElaborazione As String = txtDaData.Text
        ArrayData(0) = DataElaborazione

        If txtDaData.Text <> "" And txtAData.Text <> "" And dropStazioni.SelectedValue <> 0 Then
            SvuotaArray(ArrayData)
            SvuotaArray(ArrayGruppoA)

            For m = 0 To DateDiff("d", txtDaData.Text, txtAData.Text)
                Dim k As Integer = 1
                Dim i As Integer = 0

                'PER PRIMA COSA SELEZIONO TUTTI I GRUPPI PRESENTI A SISTEMA
                Dim gruppi(60) As String
                Dim valoreInizialeGruppi(60) As Integer

                Do While i < 60
                    valoreInizialeGruppi(i) = 0
                    i = i + 1
                Loop

                gruppi = funzioni_comuni.getGruppi()

                Dim nuovaRiga As Boolean

                If gruppi(0) <> "000" Then
                    nuovaRiga = True
                Else
                    nuovaRiga = False
                End If


                Dim situazione(900) As Integer
                Dim funzioni As New funzioni_comuni

                Dim Data1 As DateTime = DataElaborazione & " 00:00:00"
                Dim stazione As Integer = dropStazioni.SelectedValue


                'SELEZIONO LA DISPONIBILITA' INIZIALE PER OGNI GRUPPO DAL GIORNO ODIERNO FINO AL GIORNO SELEZIONATO
                Dim dataOdierna As DateTime = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()) & " 00:00:00"



                Dim giorni As Integer = DateDiff(DateInterval.Day, dataOdierna, Data1)

                valoreInizialeGruppi = funzioni_comuni.getDisponibilitaOdierna(dataOdierna, stazione, gruppi)  'RESTITUISCE LA DISPONIBILITA' ALLA DATA ODIERNA

                Do While giorni > 0
                    i = 0
                    k = 1
                    situazione = funzioni_comuni.getSituazione_x_gruppi_new(dataOdierna, stazione)

                    Do While gruppi(i) <> "000"
                        valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 29) + situazione(k + 44) - situazione(k + 14)

                        k = k + 45
                        i = i + 1
                    Loop

                    dataOdierna = dataOdierna.AddDays(1)
                    giorni = DateDiff(DateInterval.Day, dataOdierna, Data1)
                Loop

                'QUINDI SELEZIONO LA SITUAZIONE DELLA FLOTTA PER LA GIORNATA CORRENTE.    

                k = 1
                i = 0

                Dim colonna1 As Boolean

                situazione = funzioni_comuni.getSituazione_x_gruppi_new(Data1, stazione)
                'For i = 0 To UBound(situazione)
                '    Response.Write(i & ") " & situazione(i) & "<br>")
                'Next
                k = 1

                'Response.Write("Data: " & DataElaborazione & "<br>")                
                For i = 0 To UBound(gruppi)

                    If gruppi(i) = "000" Then
                        Exit For
                    End If

                    SvuotaArray(ArrayMinGiornaliero)

                    'Response.Write("K=" & k)
                    'Response.Write("Gruppo: " & gruppi(i) & "<br>")

                    valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 15) + situazione(k + 30) - situazione(k)
                    'Response.Write("00/08: " & "<b>" & valoreInizialeGruppi(i) & "</b>" & " ")
                    InsArrayMinGiornaliero(valoreInizialeGruppi(i))

                    valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 16) + situazione(k + 31) - situazione(k + 1)
                    'Response.Write("08/09: " & "<b>" & valoreInizialeGruppi(i) & "</b>" & " ")
                    InsArrayMinGiornaliero(valoreInizialeGruppi(i))

                    valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 17) + situazione(k + 32) - situazione(k + 2)
                    'Response.Write("09/10: " & "<b>" & valoreInizialeGruppi(i) & "</b>" & " ")
                    InsArrayMinGiornaliero(valoreInizialeGruppi(i))

                    valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 18) + situazione(k + 33) - situazione(k + 3)
                    'Response.Write("10/11: " & "<b>" & valoreInizialeGruppi(i) & "</b>" & " ")
                    InsArrayMinGiornaliero(valoreInizialeGruppi(i))

                    valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 19) + situazione(k + 34) - situazione(k + 4)
                    'Response.Write("11/12: " & "<b>" & valoreInizialeGruppi(i) & "</b>" & " ")
                    InsArrayMinGiornaliero(valoreInizialeGruppi(i))

                    valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 20) + situazione(k + 35) - situazione(k + 5)
                    'Response.Write("12/13: " & "<b>" & valoreInizialeGruppi(i) & "</b>" & " ")
                    InsArrayMinGiornaliero(valoreInizialeGruppi(i))

                    valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 21) + situazione(k + 36) - situazione(k + 6)
                    'Response.Write("13/14: " & "<b>" & valoreInizialeGruppi(i) & "</b>" & " ")
                    InsArrayMinGiornaliero(valoreInizialeGruppi(i))

                    valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 22) + situazione(k + 37) - situazione(k + 7)
                    'Response.Write("14/15: " & "<b>" & valoreInizialeGruppi(i) & "</b>" & " ")
                    InsArrayMinGiornaliero(valoreInizialeGruppi(i))

                    valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 23) + situazione(k + 38) - situazione(k + 8)
                    'Response.Write("15/16: " & "<b>" & valoreInizialeGruppi(i) & "</b>" & " ")
                    InsArrayMinGiornaliero(valoreInizialeGruppi(i))

                    valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 24) + situazione(k + 39) - situazione(k + 9)
                    'Response.Write("16/17: " & "<b>" & valoreInizialeGruppi(i) & "</b>" & " ")
                    InsArrayMinGiornaliero(valoreInizialeGruppi(i))

                    valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 25) + situazione(k + 40) - situazione(k + 10)
                    'Response.Write("17/18: " & "<b>" & valoreInizialeGruppi(i) & "</b>" & " ")
                    InsArrayMinGiornaliero(valoreInizialeGruppi(i))

                    valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 26) + situazione(k + 41) - situazione(k + 11)
                    'Response.Write("18/19: " & "<b>" & valoreInizialeGruppi(i) & "</b>" & " ")
                    InsArrayMinGiornaliero(valoreInizialeGruppi(i))

                    valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 27) + situazione(k + 42) - situazione(k + 12)
                    'Response.Write("19/20: " & "<b>" & valoreInizialeGruppi(i) & "</b>" & " ")
                    InsArrayMinGiornaliero(valoreInizialeGruppi(i))

                    valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 28) + situazione(k + 43) - situazione(k + 13)
                    'Response.Write("20/24: " & "<b>" & valoreInizialeGruppi(i) & "</b>" & " ")
                    InsArrayMinGiornaliero(valoreInizialeGruppi(i))

                    'minimoGiornaliero(ArrayMinGiornaliero)
                    Select Case gruppi(i)
                        Case Is = "A"
                            InsArrayGruppo(gruppi(i) & ";" & minimoGiornaliero2(ArrayMinGiornaliero), ArrayGruppoA)
                        Case Is = "A1"
                            InsArrayGruppo(gruppi(i) & ";" & minimoGiornaliero2(ArrayMinGiornaliero), ArrayGruppoA1)
                        Case Is = "B"
                            InsArrayGruppo(gruppi(i) & ";" & minimoGiornaliero2(ArrayMinGiornaliero), ArrayGruppoB)
                        Case Is = "B1"
                            InsArrayGruppo(gruppi(i) & ";" & minimoGiornaliero2(ArrayMinGiornaliero), ArrayGruppoB1)
                        Case Is = "C"
                            InsArrayGruppo(gruppi(i) & ";" & minimoGiornaliero2(ArrayMinGiornaliero), ArrayGruppoC)
                        Case Is = "D"
                            InsArrayGruppo(gruppi(i) & ";" & minimoGiornaliero2(ArrayMinGiornaliero), ArrayGruppoD)
                        Case Is = "F"
                            InsArrayGruppo(gruppi(i) & ";" & minimoGiornaliero2(ArrayMinGiornaliero), ArrayGruppoF)
                        Case Is = "H"
                            InsArrayGruppo(gruppi(i) & ";" & minimoGiornaliero2(ArrayMinGiornaliero), ArrayGruppoH)
                        Case Is = "L"
                            InsArrayGruppo(gruppi(i) & ";" & minimoGiornaliero2(ArrayMinGiornaliero), ArrayGruppoL)
                        Case Is = "M"
                            InsArrayGruppo(gruppi(i) & ";" & minimoGiornaliero2(ArrayMinGiornaliero), ArrayGruppoM)
                        Case Is = "S"
                            InsArrayGruppo(gruppi(i) & ";" & minimoGiornaliero2(ArrayMinGiornaliero), ArrayGruppoS)
                        Case Is = "V"
                            InsArrayGruppo(gruppi(i) & ";" & minimoGiornaliero2(ArrayMinGiornaliero), ArrayGruppoV)
                    End Select

                    k = k + 45
                Next

                StrArrayData = Split(DataElaborazione, "/")
                Select Case StrArrayData(1)
                    Case Is = "01"
                        DataElaborazione2 = StrArrayData(0) & "-gen"
                    Case Is = "02"
                        DataElaborazione2 = StrArrayData(0) & "-feb"
                    Case Is = "03"
                        DataElaborazione2 = StrArrayData(0) & "-mar"
                    Case Is = "04"
                        DataElaborazione2 = StrArrayData(0) & "-apr"
                    Case Is = "05"
                        DataElaborazione2 = StrArrayData(0) & "-mag"
                    Case Is = "06"
                        DataElaborazione2 = StrArrayData(0) & "-giu"
                    Case Is = "07"
                        DataElaborazione2 = StrArrayData(0) & "-lug"
                    Case Is = "08"
                        DataElaborazione2 = StrArrayData(0) & "-ago"
                    Case Is = "09"
                        DataElaborazione2 = StrArrayData(0) & "-set"
                    Case Is = "10"
                        DataElaborazione2 = StrArrayData(0) & "-ott"
                    Case Is = "11"
                        DataElaborazione2 = StrArrayData(0) & "-nov"
                    Case Is = "12"
                        DataElaborazione2 = StrArrayData(0) & "-dic"
                End Select
                ArrayData(m) = DataElaborazione2
                DataElaborazione = DateAdd("d", 1, DataElaborazione)
            Next

            'For n = 0 To UBound(ArrayGruppoA)
            '    If ArrayGruppoA(n) = "" Then
            '        Exit For
            '    Else
            '        Response.Write(n & " " & ArrayGruppoA(n) & "<br>")
            '    End If
            'Next
            'For n = 0 To UBound(ArrayGruppoA1)
            '    If ArrayGruppoA1(n) = "" Then
            '        Exit For
            '    Else
            '        Response.Write(n & " " & ArrayGruppoA1(n) & "<br>")
            '    End If
            'Next
            'For n = 0 To UBound(ArrayGruppoB)
            '    If ArrayGruppoB(n) = "" Then
            '        Exit For
            '    Else
            '        Response.Write(n & " " & ArrayGruppoB(n) & "<br>")
            '    End If
            'Next
            'For n = 0 To UBound(ArrayGruppoB1)
            '    If ArrayGruppoB1(n) = "" Then
            '        Exit For
            '    Else
            '        Response.Write(n & " " & ArrayGruppoB1(n) & "<br>")
            '    End If
            'Next

            'For n = 0 To UBound(ArrayData)
            '    If ArrayData(n) = "" Then
            '        Exit For
            '    Else
            '        Response.Write(ArrayData(n) & " ")
            '    End If
            'Next

            Report()
        Else
            Dim RispostaMsgBx As String

            RispostaMsgBx = "Selezionare un Range di date e/o Stazione di previsione"
            Libreria.genUserMsgBox(Page, RispostaMsgBx)
        End If
    End Sub

    Private Sub InsArrayMinGiornaliero(ByVal cella As String)        
        For i = 0 To UBound(ArrayMinGiornaliero)
            If ArrayMinGiornaliero(i) = "" Then
                ArrayMinGiornaliero(i) = cella
                'Response.Write("cella " & cella & "--" & ArrayMinGiornaliero(i) & "--i " & i & "<br>")
                Exit For
            End If
        Next
    End Sub

    Private Sub InsArrayGruppo(ByVal cella, ByVal vettore)
        For i = 0 To UBound(vettore)
            If vettore(i) = "" Then
                vettore(i) = cella
                'Response.Write("cella " & cella & "--" & ArrayMinGiornaliero(i) & "--i " & i & "<br>")
                Exit For
            End If
        Next
    End Sub

    Private Sub InsArrayGruppoA1(ByVal cella As String)
        For i = 0 To UBound(ArrayGruppoA1)
            If ArrayGruppoA1(i) = "" Then
                ArrayGruppoA1(i) = cella
                'Response.Write("cella " & cella & "--" & ArrayMinGiornaliero(i) & "--i " & i & "<br>")
                Exit For
            End If
        Next
    End Sub

    Private Sub SvuotaArray(ByVal MiaArray)
        Dim i
        ' Verifico che MiaArray sia effettivamente un vettore.
        ' Contestualmente mi assicuro che CosaCercare non sia vuoto
        If IsArray(MiaArray) Then
            ' Faccio un ciclo per la lunghezza della nostra array
            For i = 0 To UBound(MiaArray)
                ' Svuoto Array
                MiaArray(i) = ""
            Next
        Else
            Response.Write("No Array")
        End If
    End Sub

    Protected Function minimoGiornaliero(ByVal vettore) As String
        Dim aus As String

        aus = vettore(0)
        'Response.Write("Aus " & aus & " ")
        If IsArray(vettore) Then
            For i = 0 To UBound(vettore)
                'Response.Write(i & ")" & vettore(i) & "-- ")
                If vettore(i) <> "" Then
                    If vettore(i) < aus Then
                        aus = vettore(i)
                    End If
                End If                
            Next
            Response.Write("Minimo Giornaliero " & aus & "<br><br>")
        Else
            Response.Write("No Array")
        End If
        
        'minimoGiornaliero = aus
    End Function

    Protected Function minimoGiornaliero2(ByVal vettore) As String
        Dim aus As String

        aus = vettore(0)
        'Response.Write("Aus " & aus & " ")
        If IsArray(vettore) Then
            For i = 0 To UBound(vettore)
                'Response.Write(i & ")" & vettore(i) & "-- ")
                If vettore(i) <> "" Then
                    If Int(vettore(i)) < Int(aus) Then
                        aus = vettore(i)
                    End If
                End If
            Next            
        Else
            Response.Write("No Array")
        End If

        minimoGiornaliero2 = aus
    End Function

    Protected Sub Report()
        Dim ArrayDatiStr(2) As String
        Dim StrDati(1) As String
        Dim Fine, Selezione As String

        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=" & dropStazioni.SelectedItem.Text & "_" & Now & ".xls")
        Response.ContentType = "application/vnd.xls"
        Response.Charset = "UTF-8"



        With Response
            .Write("<table border=1>")
            .Write("<tr>")

            .Write("<td>")
            .Write(" ")
            .Write("</td>")

            For i = 0 To UBound(ArrayData)                
                If ArrayData(i) <> "" Then
                    .Write("<td align=""center"">")
                    .Write(ArrayData(i))
                    .Write("</td>")
                Else
                    Exit For
                End If
            Next

            .Write("</tr>")

            'Righe successive Gruppo A           
            StrDati = split(ArrayGruppoA(0), ";")            
            .Write("<tr>")

            .Write("<td>")
            .Write(StrDati(0))
            .Write("</td>")
            .Write("<td>")
            .Write(StrDati(1))
            .Write("</td>")

            For i = 1 To UBound(ArrayGruppoA)
                If ArrayGruppoA(i) <> "" Then
                    StrDati = split(ArrayGruppoA(i), ";")
                    .Write("<td>")
                    .Write(StrDati(1))
                    .Write("</td>")
                Else
                    Exit For
                End If                
            Next
            .Write("</tr>")

            'Righe successive Gruppo A1           
            StrDati = split(ArrayGruppoA1(0), ";")
            .Write("<tr>")

            .Write("<td>")
            .Write(StrDati(0))
            .Write("</td>")
            .Write("<td>")
            .Write(StrDati(1))
            .Write("</td>")

            For i = 1 To UBound(ArrayGruppoA1)
                If ArrayGruppoA(i) <> "" Then
                    StrDati = split(ArrayGruppoA1(i), ";")
                    .Write("<td>")
                    .Write(StrDati(1))
                    .Write("</td>")
                Else
                    Exit For
                End If
            Next
            .Write("</tr>")

            'Righe successive Gruppo B          
            StrDati = split(ArrayGruppoB(0), ";")
            .Write("<tr>")

            .Write("<td>")
            .Write(StrDati(0))
            .Write("</td>")
            .Write("<td>")
            .Write(StrDati(1))
            .Write("</td>")

            For i = 1 To UBound(ArrayGruppoB)
                If ArrayGruppoB(i) <> "" Then
                    StrDati = split(ArrayGruppoB(i), ";")
                    .Write("<td>")
                    .Write(StrDati(1))
                    .Write("</td>")
                Else
                    Exit For
                End If
            Next
            .Write("</tr>")

            'Righe successive Gruppo B1           
            StrDati = split(ArrayGruppoB1(0), ";")
            .Write("<tr>")

            .Write("<td>")
            .Write(StrDati(0))
            .Write("</td>")
            .Write("<td>")
            .Write(StrDati(1))
            .Write("</td>")

            For i = 1 To UBound(ArrayGruppoB1)
                If ArrayGruppoB1(i) <> "" Then
                    StrDati = split(ArrayGruppoB1(i), ";")
                    .Write("<td>")
                    .Write(StrDati(1))
                    .Write("</td>")
                Else
                    Exit For
                End If
            Next
            .Write("</tr>")

            'Righe successive Gruppo C           
            StrDati = split(ArrayGruppoC(0), ";")
            .Write("<tr>")

            .Write("<td>")
            .Write(StrDati(0))
            .Write("</td>")
            .Write("<td>")
            .Write(StrDati(1))
            .Write("</td>")

            For i = 1 To UBound(ArrayGruppoC)
                If ArrayGruppoC(i) <> "" Then
                    StrDati = split(ArrayGruppoC(i), ";")
                    .Write("<td>")
                    .Write(StrDati(1))
                    .Write("</td>")
                Else
                    Exit For
                End If
            Next
            .Write("</tr>")

            'Righe successive Gruppo D           
            StrDati = split(ArrayGruppoD(0), ";")
            .Write("<tr>")

            .Write("<td>")
            .Write(StrDati(0))
            .Write("</td>")
            .Write("<td>")
            .Write(StrDati(1))
            .Write("</td>")

            For i = 1 To UBound(ArrayGruppoD)
                If ArrayGruppoD(i) <> "" Then
                    StrDati = split(ArrayGruppoD(i), ";")
                    .Write("<td>")
                    .Write(StrDati(1))
                    .Write("</td>")
                Else
                    Exit For
                End If
            Next
            .Write("</tr>")

            'Righe successive Gruppo F           
            StrDati = split(ArrayGruppoF(0), ";")
            .Write("<tr>")

            .Write("<td>")
            .Write(StrDati(0))
            .Write("</td>")
            .Write("<td>")
            .Write(StrDati(1))
            .Write("</td>")

            For i = 1 To UBound(ArrayGruppoF)
                If ArrayGruppoF(i) <> "" Then
                    StrDati = split(ArrayGruppoF(i), ";")
                    .Write("<td>")
                    .Write(StrDati(1))
                    .Write("</td>")
                Else
                    Exit For
                End If
            Next
            .Write("</tr>")

            'Righe successive Gruppo H           
            StrDati = split(ArrayGruppoH(0), ";")
            .Write("<tr>")

            .Write("<td>")
            .Write(StrDati(0))
            .Write("</td>")
            .Write("<td>")
            .Write(StrDati(1))
            .Write("</td>")

            For i = 1 To UBound(ArrayGruppoH)
                If ArrayGruppoH(i) <> "" Then
                    StrDati = split(ArrayGruppoH(i), ";")
                    .Write("<td>")
                    .Write(StrDati(1))
                    .Write("</td>")
                Else
                    Exit For
                End If
            Next
            .Write("</tr>")

            'Righe successive Gruppo L           
            StrDati = split(ArrayGruppoL(0), ";")
            .Write("<tr>")

            .Write("<td>")
            .Write(StrDati(0))
            .Write("</td>")
            .Write("<td>")
            .Write(StrDati(1))
            .Write("</td>")

            For i = 1 To UBound(ArrayGruppoL)
                If ArrayGruppoL(i) <> "" Then
                    StrDati = split(ArrayGruppoL(i), ";")
                    .Write("<td>")
                    .Write(StrDati(1))
                    .Write("</td>")
                Else
                    Exit For
                End If
            Next
            .Write("</tr>")

            'Righe successive Gruppo M           
            StrDati = split(ArrayGruppoM(0), ";")
            .Write("<tr>")

            .Write("<td>")
            .Write(StrDati(0))
            .Write("</td>")
            .Write("<td>")
            .Write(StrDati(1))
            .Write("</td>")

            For i = 1 To UBound(ArrayGruppoM)
                If ArrayGruppoM(i) <> "" Then
                    StrDati = split(ArrayGruppoM(i), ";")
                    .Write("<td>")
                    .Write(StrDati(1))
                    .Write("</td>")
                Else
                    Exit For
                End If
            Next
            .Write("</tr>")

            'Righe successive Gruppo S           
            StrDati = split(ArrayGruppoS(0), ";")
            .Write("<tr>")

            .Write("<td>")
            .Write(StrDati(0))
            .Write("</td>")
            .Write("<td>")
            .Write(StrDati(1))
            .Write("</td>")

            For i = 1 To UBound(ArrayGruppoS)
                If ArrayGruppoS(i) <> "" Then
                    StrDati = split(ArrayGruppoS(i), ";")
                    .Write("<td>")
                    .Write(StrDati(1))
                    .Write("</td>")
                Else
                    Exit For
                End If
            Next
            .Write("</tr>")

            'Righe successive Gruppo V           
            StrDati = split(ArrayGruppoV(0), ";")
            .Write("<tr>")

            .Write("<td>")
            .Write(StrDati(0))
            .Write("</td>")
            .Write("<td>")
            .Write(StrDati(1))
            .Write("</td>")

            For i = 1 To UBound(ArrayGruppoV)
                If ArrayGruppoV(i) <> "" Then
                    StrDati = split(ArrayGruppoV(i), ";")
                    .Write("<td>")
                    .Write(StrDati(1))
                    .Write("</td>")
                Else
                    Exit For
                End If
            Next
            .Write("</tr>")

        End With


        Response.Write("</table>")
        Response.Flush()
        Response.End()
    End Sub
End Class
