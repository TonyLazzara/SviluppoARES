Imports System.Net.Mail
Imports System.Net


Partial Class stop_sell
    Inherits System.Web.UI.Page
    Dim mail As MailMessage 'questa dichiarazione deve essere globale


    Protected Sub PassaUnoStazioni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassaUnoStazioni.Click
        Dim k As Integer = 0
        Dim j As Integer = listStazioniSelezionate.Items.Count

        For i = 0 To listStazioni.Items.Count() - 1
            If listStazioni.Items(i).Selected Then
                listStazioniSelezionate.Items.Add(listStazioni.Items(i).Text)
                listStazioniSelezionate.Items(j).Value = listStazioni.Items(i).Value
                j = j + 1

                k = k + 1
            End If
        Next

        For i = 1 To k
            listStazioni.Items.Remove(listStazioni.SelectedItem)
        Next
    End Sub

    Protected Sub TornaUnoStazioni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TornaUnoStazioni.Click
        Dim k As Integer = 0
        Dim j As Integer = listStazioni.Items.Count

        For i = 0 To listStazioniSelezionate.Items.Count() - 1
            If listStazioniSelezionate.Items(i).Selected Then
                listStazioni.Items.Add(listStazioniSelezionate.Items(i).Text)
                listStazioni.Items(j).Value = listStazioniSelezionate.Items(i).Value
                j = j + 1

                k = k + 1
            End If
        Next

        For i = 1 To k
            listStazioniSelezionate.Items.Remove(listStazioniSelezionate.SelectedItem)
        Next
    End Sub

    Protected Sub PassaTuttiStazioni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassaTuttiStazioni.Click
        Dim j As Integer = listStazioniSelezionate.Items.Count

        For i = 0 To listStazioni.Items.Count() - 1
            listStazioniSelezionate.Items.Add(listStazioni.Items(i).Text)
            listStazioniSelezionate.Items(j).Value = listStazioni.Items(i).Value
            j = j + 1
        Next

        listStazioni.Items.Clear()
    End Sub

    Protected Sub TornaTuttiStazioni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TornaTuttiStazioni.Click
        listStazioni.Items.Clear()
        listStazioniSelezionate.Items.Clear()

        listStazioni.DataBind()
    End Sub

    Protected Sub radioSel1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radioSel1.SelectedIndexChanged
        If radioSel1.SelectedValue = "1" Then
            listStazioni.Enabled = False
            listStazioniSelezionate.Enabled = False
            PassaTuttiStazioni.Enabled = False
            PassaUnoStazioni.Enabled = False
            TornaTuttiStazioni.Enabled = False
            TornaUnoStazioni.Enabled = False
        Else
            listStazioni.Enabled = True
            listStazioniSelezionate.Enabled = True
            PassaTuttiStazioni.Enabled = True
            PassaUnoStazioni.Enabled = True
            TornaTuttiStazioni.Enabled = True
            TornaUnoStazioni.Enabled = True
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Request.Cookies("SicilyRentCar") Is Nothing Then
            Response.Redirect("LogIn.aspx")
        End If


        If Not Page.IsPostBack Then
            permesso_accesso.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.StopSale)

            If permesso_accesso.Text <> "1" Then
                query.Text = "SELECT CONVERT(char(10), da_data, 103) AS da_data, CONVERT(char(10), a_data, 103) AS a_data, descrizione, id, data_efficacia FROM stop_sell"

                listStazioni.Enabled = False
                listStazioniSelezionate.Enabled = False
                PassaTuttiStazioni.Enabled = False
                PassaUnoStazioni.Enabled = False
                TornaTuttiStazioni.Enabled = False
                TornaUnoStazioni.Enabled = False

                listGruppi.Enabled = False
                listGruppiSelezionati.Enabled = False
                PassaTuttiGruppo.Enabled = False
                PassaUnoGruppo.Enabled = False
                TornaTuttiGruppo.Enabled = False
                TornaUnoGruppo.Enabled = False

                listFonti.Enabled = False
                listFontiSelezionate.Enabled = False
                PassaTuttiFonte.Enabled = False
                PassaUnoFonte.Enabled = False
                TornaTuttiFonte.Enabled = False
                TornaUnoFonte.Enabled = False

                If permesso_accesso.Text = "2" Then
                    btnNuovoStopSall.Visible = False
                End If
            Else
                Response.Redirect("default.aspx")
            End If
            
        End If

        SqlStopSall.SelectCommand = query.Text

    End Sub

    Protected Sub PassaUnoGruppo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassaUnoGruppo.Click
        Dim k As Integer = 0
        Dim j As Integer = listGruppiSelezionati.Items.Count

        For i = 0 To listGruppi.Items.Count() - 1
            If listGruppi.Items(i).Selected Then
                listGruppiSelezionati.Items.Add(listGruppi.Items(i).Text)
                listGruppiSelezionati.Items(j).Value = listGruppi.Items(i).Value
                j = j + 1

                k = k + 1
            End If
        Next

        For i = 1 To k
            listGruppi.Items.Remove(listGruppi.SelectedItem)
        Next
    End Sub

    Protected Sub TornaUnoGruppo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TornaUnoGruppo.Click
        Dim k As Integer = 0
        Dim j As Integer = listGruppi.Items.Count

        For i = 0 To listGruppiSelezionati.Items.Count() - 1
            If listGruppiSelezionati.Items(i).Selected Then
                listGruppi.Items.Add(listGruppiSelezionati.Items(i).Text)
                listGruppi.Items(j).Value = listGruppiSelezionati.Items(i).Value
                j = j + 1

                k = k + 1
            End If
        Next

        For i = 1 To k
            listGruppiSelezionati.Items.Remove(listGruppiSelezionati.SelectedItem)
        Next
    End Sub

    Protected Sub PassaTuttiGruppo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassaTuttiGruppo.Click
        Dim j As Integer = listGruppiSelezionati.Items.Count

        For i = 0 To listGruppi.Items.Count() - 1
            listGruppiSelezionati.Items.Add(listGruppi.Items(i).Text)
            listGruppiSelezionati.Items(j).Value = listGruppi.Items(i).Value
            j = j + 1
        Next

        listGruppi.Items.Clear()
    End Sub

    Protected Sub TornaTuttiGruppo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TornaTuttiGruppo.Click
        listGruppi.Items.Clear()
        listGruppiSelezionati.Items.Clear()

        listGruppi.DataBind()
    End Sub

    Protected Sub radioSel2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radioSel2.SelectedIndexChanged
        If radioSel2.SelectedValue = "1" Then
            listGruppi.Enabled = False
            listGruppiSelezionati.Enabled = False
            PassaTuttiGruppo.Enabled = False
            PassaUnoGruppo.Enabled = False
            TornaTuttiGruppo.Enabled = False
            TornaUnoGruppo.Enabled = False
        Else
            listGruppi.Enabled = True
            listGruppiSelezionati.Enabled = True
            PassaTuttiGruppo.Enabled = True
            PassaUnoGruppo.Enabled = True
            TornaTuttiGruppo.Enabled = True
            TornaUnoGruppo.Enabled = True
        End If
    End Sub

    Protected Sub radioSel3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radioSel3.SelectedIndexChanged
        If radioSel3.SelectedValue = "1" Then
            listFonti.Enabled = False
            listFontiSelezionate.Enabled = False
            PassaTuttiFonte.Enabled = False
            PassaUnoFonte.Enabled = False
            TornaTuttiFonte.Enabled = False
            TornaUnoFonte.Enabled = False
        Else
            listFonti.Enabled = True
            listFontiSelezionate.Enabled = True
            PassaTuttiFonte.Enabled = True
            PassaUnoFonte.Enabled = True
            TornaTuttiFonte.Enabled = True
            TornaUnoFonte.Enabled = True
        End If
    End Sub

    Protected Sub PassaUnoFonte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassaUnoFonte.Click
        Dim k As Integer = 0
        Dim j As Integer = listFontiSelezionate.Items.Count

        For i = 0 To listFonti.Items.Count() - 1
            If listFonti.Items(i).Selected Then
                listFontiSelezionate.Items.Add(listFonti.Items(i).Text)
                listFontiSelezionate.Items(j).Value = listFonti.Items(i).Value
                j = j + 1

                k = k + 1
            End If
        Next

        For i = 1 To k
            listFonti.Items.Remove(listFonti.SelectedItem)
        Next
    End Sub

    Protected Sub TornaUnoFonte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TornaUnoFonte.Click
        Dim k As Integer = 0
        Dim j As Integer = listFonti.Items.Count

        For i = 0 To listFontiSelezionate.Items.Count() - 1
            If listFontiSelezionate.Items(i).Selected Then
                listFonti.Items.Add(listFontiSelezionate.Items(i).Text)
                listFonti.Items(j).Value = listFontiSelezionate.Items(i).Value
                j = j + 1

                k = k + 1
            End If
        Next

        For i = 1 To k
            listFontiSelezionate.Items.Remove(listFontiSelezionate.SelectedItem)
        Next
    End Sub

    Protected Sub PassaTuttiFonte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PassaTuttiFonte.Click
        Dim j As Integer = listFontiSelezionate.Items.Count

        For i = 0 To listFonti.Items.Count() - 1
            listFontiSelezionate.Items.Add(listFonti.Items(i).Text)
            listFontiSelezionate.Items(j).Value = listFonti.Items(i).Value
            j = j + 1
        Next

        listFonti.Items.Clear()
    End Sub

    Protected Sub TornaTuttiFonte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TornaTuttiFonte.Click
        listFonti.Items.Clear()
        listFontiSelezionate.Items.Clear()

        listFonti.DataBind()
    End Sub

    Protected Sub salva_stop_sell()
        Dim stop_da As String = stopDa.Text
        Dim stop_a As String = stopA.Text
        Dim data_efficacia As String = dataEfficacia.Text

        Dim descrizione As String = txtDescrizione.Text
        Dim tutte_stazioni As String = "0"
        Dim tutti_gruppi As String = "0"
        Dim tutte_fonti As String = "0"
        Dim sqlstr As String = ""

        Try

            If listStazioni.Items.Count = 0 Or radioSel1.SelectedValue = "1" Then
                tutte_stazioni = "1"
            End If
            If listGruppi.Items.Count = 0 Or radioSel2.SelectedValue = "1" Then
                tutti_gruppi = "1"
            End If
            If listFonti.Items.Count = 0 Or radioSel3.SelectedValue = "1" Then
                tutte_fonti = "1"
            End If

            stop_da = Year(stop_da) & "-" & Month(stop_da) & "-" & Day(stop_da) & " 00:00:00"
            stop_a = Year(stop_a) & "-" & Month(stop_a) & "-" & Day(stop_a) & " 23:59:59"
            data_efficacia = Year(data_efficacia) & "-" & Month(data_efficacia) & "-" & Day(data_efficacia) & " " & dropOre.SelectedValue & ":" & dropMinuti.SelectedValue & ":00"

            'If Request.ServerVariables("HTTP_HOST") = "sviluppoares.sicilyrentcar.it" Then
            '    stop_da = Year(stop_da) & "-" & Day(stop_da) & "-" & Month(stop_da) & " 00:00:00"
            'Else
            '    stop_da = Year(stop_da) & "-" & Month(stop_da) & "-" & Day(stop_da) & " 00:00:00"
            'End If

            'If Request.ServerVariables("HTTP_HOST") = "sviluppoares.sicilyrentcar.it" Then
            '    stop_a = Year(stop_a) & "-" & Day(stop_a) & "-" & Month(stop_a) & " 23:59:59"
            'Else
            '    stop_a = Year(stop_a) & "-" & Month(stop_a) & "-" & Day(stop_a) & " 23:59:59"
            'End If

            'If Request.ServerVariables("HTTP_HOST") = "sviluppoares.sicilyrentcar.it" Then
            '    data_efficacia = Year(data_efficacia) & "-" & Day(data_efficacia) & "-" & Month(data_efficacia) & " " & dropOre.SelectedValue & ":" & dropMinuti.SelectedValue & ":00"
            'Else
            '    data_efficacia = Year(data_efficacia) & "-" & Month(data_efficacia) & "-" & Day(data_efficacia) & " " & dropOre.SelectedValue & ":" & dropMinuti.SelectedValue & ":00"
            'End If

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
            Dim id_stop_sell As String

            If idStopSell.Text = "" Then

                sqlstr = "INSERT INTO stop_sell (da_data, a_data, tutti_gruppi, tutte_fonti, tutte_stazioni, descrizione, data_efficacia,validita) "
                sqlstr += "VALUES(convert(datetime,'" & stop_da & "',102), convert(datetime,'" & stop_a & "',102), '" & tutti_gruppi & "', '" & tutte_fonti & "', '" & tutte_stazioni & "', '" & descrizione & "',convert(datetime,'" & data_efficacia & "',102),'" & TxtValidita.Text & "')"


                Cmd = New Data.SqlClient.SqlCommand(sqlstr, Dbc)
                Cmd.ExecuteNonQuery()

                Cmd = New Data.SqlClient.SqlCommand("SELECT MAX(id) AS id_stop_sell FROM stop_sell", Dbc)

                id_stop_sell = Cmd.ExecuteScalar
                idStopSell.Text = id_stop_sell

                lblNumero.Text = " - Stop Sale Numero " & id_stop_sell
            Else



                Cmd = New Data.SqlClient.SqlCommand("UPDATE stop_sell SET tutti_gruppi='" & tutti_gruppi & "', tutte_fonti='" & tutte_fonti & "', tutte_stazioni='" & tutte_stazioni & "', da_data=convert(datetime,'" & stop_da & "',102), a_data=convert(datetime,'" & stop_a & "',102), descrizione ='" & txtDescrizione.Text & "', data_efficacia=convert(datetime,'" & data_efficacia & "',102), validita='" & TxtValidita.Text & "'  WHERE id='" & idStopSell.Text & "'", Dbc)
                Cmd.ExecuteScalar()
                Cmd = New Data.SqlClient.SqlCommand("DELETE FROM stop_sell_stazioni WHERE id_stop_sell='" & idStopSell.Text & "'", Dbc)
                Cmd.ExecuteScalar()
                Cmd = New Data.SqlClient.SqlCommand("DELETE FROM stop_sell_gruppi WHERE id_stop_sell='" & idStopSell.Text & "'", Dbc)
                Cmd.ExecuteScalar()
                Cmd = New Data.SqlClient.SqlCommand("DELETE FROM stop_sell_fonti WHERE id_stop_sell='" & idStopSell.Text & "'", Dbc)
                Cmd.ExecuteScalar()
            End If



            If tutte_stazioni = "0" Then
                For i = 0 To listStazioniSelezionate.Items.Count - 1
                    Dim id_stazioni As Integer = listStazioniSelezionate.Items(i).Value
                    Cmd = New Data.SqlClient.SqlCommand("INSERT INTO stop_sell_stazioni (id_stop_sell,id_stazione) VALUES (" & idStopSell.Text & "," & id_stazioni & ")", Dbc)
                    Cmd.ExecuteNonQuery()
                Next
            End If
            If tutti_gruppi = "0" Then
                For i = 0 To listGruppiSelezionati.Items.Count - 1
                    Dim id_gruppi As Integer = listGruppiSelezionati.Items(i).Value
                    Cmd = New Data.SqlClient.SqlCommand("INSERT INTO stop_sell_gruppi (id_stop_sell,id_gruppo) VALUES (" & idStopSell.Text & "," & id_gruppi & ")", Dbc)
                    Cmd.ExecuteNonQuery()
                Next
            End If
            If tutte_fonti = "0" Then
                For i = 0 To listFontiSelezionate.Items.Count - 1
                    Dim id_fonti As Integer = listFontiSelezionate.Items(i).Value
                    Cmd = New Data.SqlClient.SqlCommand("INSERT INTO stop_sell_fonti (id_stop_sell,id_fonte) VALUES (" & idStopSell.Text & "," & id_fonti & ")", Dbc)
                    Cmd.ExecuteNonQuery()
                Next
            End If

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            'Invio Mail   
            Dim corpoMessaggio As String = "KO" 'al momento nn inviare
            'disattivato invio emailo x test 20.06.2021
            If corpoMessaggio = "OK" Then
                Dim SQL As String
                Dim DbcGriglia As New Data.SqlClient.SqlConnection(SqlStopSall.ConnectionString)
                DbcGriglia.Open()

                SQL = "select * from stop_sell WHERE id ='" & idStopSell.Text & "'"

                Dim CmdGriglia As New Data.SqlClient.SqlCommand(SQL, DbcGriglia)
                ' Trace.Write(CmdGriglia.CommandText & "<br><br>")

                Dim RsGriglia As Data.SqlClient.SqlDataReader
                RsGriglia = CmdGriglia.ExecuteReader()
                Do While RsGriglia.Read
                    'Dichiaro e creo un nuovo messaggio
                    Dim mail As New MailMessage()

                    'Dichiato il mittente
                    mail.From = New MailAddress("ares_sbc@xinformatica.it")

                    'Dichiaro il destinatario
                    'Elenco destinatari
                    Dim DestMailStopSale As String = ConfigurationManager.AppSettings.Get("DestMailStopSale")
                    Dim StrDestMailStopSale As Array

                    'mail.To.Add("alazzara@entermed.it")
                    StrDestMailStopSale = Split(DestMailStopSale, "-")
                    For i = 0 To UBound(StrDestMailStopSale)
                        mail.To.Add(StrDestMailStopSale(i))
                    Next

                    'Dichiaro il destinatario CC
                    'Elenco destinatari CC
                    Dim DestCCMailStopSale As String = ConfigurationManager.AppSettings.Get("DestCCMailStopSale")
                    Dim StrDestCCMailStopSale As Array

                    StrDestCCMailStopSale = Split(DestCCMailStopSale, "-")
                    For i = 0 To UBound(StrDestCCMailStopSale)
                        mail.CC.Add(StrDestCCMailStopSale(i))
                    Next

                    'Dichiaro il destinatario Bcc
                    'mail.Bcc.Add("spedizione@tuttoinfanzia.it")

                    Dim SqlStazioni As String
                    Dim DbcElencoStazioni As New Data.SqlClient.SqlConnection(SqlStopSall.ConnectionString)
                    DbcElencoStazioni.Open()

                    If RsGriglia("tutte_stazioni") Then 'Tutte le stazioni
                        SqlStazioni = "SELECT nome_stazione, email FROM stazioni WHERE (email IS NOT NULL) AND (email <> '-') ORDER BY nome_stazione"
                    Else
                        SqlStazioni = "SELECT stop_sell.id, stop_sell.descrizione, stazioni.nome_stazione, stazioni.email" &
                                 " FROM stop_sell INNER JOIN" &
                                 " stop_sell_stazioni ON stop_sell.id = stop_sell_stazioni.id_stop_sell INNER JOIN" &
                                 " stazioni ON stop_sell_stazioni.id_stazione = stazioni.id" &
                                 " WHERE(stop_sell_stazioni.id_stop_sell = '" & idStopSell.Text & "')" &
                                 " ORDER BY stazioni.nome_stazione"
                    End If

                    Dim CmdElencoStazioni As New Data.SqlClient.SqlCommand(SqlStazioni, DbcElencoStazioni)
                    'Response.Write(CmdElencoStazioni.CommandText & "<br><br>")
                    'Response.End()
                    Dim RsElencoStazioni As Data.SqlClient.SqlDataReader
                    RsElencoStazioni = CmdElencoStazioni.ExecuteReader()
                    Do While RsElencoStazioni.Read
                        mail.Bcc.Add(RsElencoStazioni("email"))
                    Loop
                    RsElencoStazioni.Close()
                    DbcElencoStazioni.Close()
                    RsElencoStazioni = Nothing
                    DbcElencoStazioni = Nothing
                    CmdElencoStazioni.Dispose()

                    'Imposta l'oggetto della Mail
                    mail.Subject = RsGriglia("descrizione")

                    'Imposta la priorità  della Mail
                    mail.Priority = MailPriority.High

                    mail.IsBodyHtml = True

                    'Imposta il testo del messaggio
                    corpoMessaggio = "<b><font color=""#000000"">Prego inserire in stop sale seguenti uffici per le seguenti date:</font></b>" & "<br>"

                    If RsGriglia("tutte_stazioni") Then
                        corpoMessaggio = corpoMessaggio & "<br>" & "Tutte Le Stazioni"
                    Else
                        Dim SQL2 As String
                        Dim DbcStazioni As New Data.SqlClient.SqlConnection(SqlStopSall.ConnectionString)
                        DbcStazioni.Open()

                        SQL2 = "Select stazioni.nome_stazione " &
                                "FROM stop_sell_stazioni INNER JOIN " &
                                    "stazioni ON stop_sell_stazioni.id_stazione = stazioni.id " &
                                "WHERE(stop_sell_stazioni.id_stop_sell = '" & idStopSell.Text & "')" &
                                    "ORDER BY stazioni.nome_stazione"

                        Dim CmdStazioni As New Data.SqlClient.SqlCommand(SQL2, DbcStazioni)
                        'Response.Write(CmdStazioni.CommandText & "<br><br>")
                        'Response.End()
                        Dim RsStazioni As Data.SqlClient.SqlDataReader
                        RsStazioni = CmdStazioni.ExecuteReader()
                        Do While RsStazioni.Read
                            corpoMessaggio = corpoMessaggio & "<br>" & "- " & RsStazioni("nome_stazione")
                        Loop
                        RsStazioni.Close()
                        DbcStazioni.Close()
                        RsStazioni = Nothing
                        DbcStazioni = Nothing
                        CmdStazioni.Dispose()
                    End If

                    corpoMessaggio = corpoMessaggio & "<br>" & "<b><font color=""#000000"">Decorrenza da:" & RsGriglia("da_data") & " Fino al " & Replace(RsGriglia("a_data"), "23.59.59", "Incluso") & "</font></b>" & "<br>"

                    corpoMessaggio = corpoMessaggio & "<br>" & "<b><font color=""#000000"">Gruppi:</font></b>"
                    If RsGriglia("tutti_gruppi") Then
                        corpoMessaggio = corpoMessaggio & "<br>" & "Tutti i Gruppi"
                    Else
                        Dim SQL3 As String
                        Dim DbcGruppi As New Data.SqlClient.SqlConnection(SqlStopSall.ConnectionString)
                        DbcGruppi.Open()

                        SQL3 = "SELECT GRUPPI.cod_gruppo " &
                                "FROM stop_sell_gruppi INNER JOIN " &
                            "GRUPPI ON stop_sell_gruppi.id_gruppo = GRUPPI.ID_gruppo " &
                                "WHERE (stop_sell_gruppi.id_stop_sell = '" & idStopSell.Text & "')" &
                            "ORDER BY GRUPPI.cod_gruppo"


                        Dim CmdGruppi As New Data.SqlClient.SqlCommand(SQL3, DbcGruppi)
                        'Response.Write(CmdGruppi.CommandText & "<br><br>")
                        'Response.End()
                        Dim RsGruppi As Data.SqlClient.SqlDataReader
                        RsGruppi = CmdGruppi.ExecuteReader()
                        Do While RsGruppi.Read
                            corpoMessaggio = corpoMessaggio & "<br>" & "- " & RsGruppi("cod_gruppo")
                        Loop
                        RsGruppi.Close()
                        DbcGruppi.Close()
                        RsGruppi = Nothing
                        DbcGruppi = Nothing
                    End If

                    corpoMessaggio = corpoMessaggio & "<br><br>"

                    'corpoMessaggio = corpoMessaggio & "<br/>" & "Validità : " & RsGriglia("validita")
                    corpoMessaggio = corpoMessaggio & "<br>" & "<b><font color=""#000000"">Fonti:</font></b>"
                    If RsGriglia("tutte_fonti") Then
                        corpoMessaggio = corpoMessaggio & "<br>" & "Tutte Le Fonti"
                    Else
                        Dim SQL3 As String
                        Dim DbcFonti As New Data.SqlClient.SqlConnection(SqlStopSall.ConnectionString)
                        DbcFonti.Open()

                        SQL3 = "Select clienti_tipologia.descrizione " &
                                "FROM clienti_tipologia INNER JOIN " &
                                    "stop_sell_fonti ON clienti_tipologia.id = stop_sell_fonti.id_fonte INNER JOIN " &
                                    "stop_sell ON stop_sell_fonti.id_stop_sell = stop_sell.id " &
                                "WHERE(stop_sell.id  = '" & idStopSell.Text & "')" &
                                    "ORDER BY clienti_tipologia.descrizione"

                        Dim CmdFonti As New Data.SqlClient.SqlCommand(SQL3, DbcFonti)
                        'Response.Write(CmdFonti.CommandText & "<br><br>")
                        'Response.End()
                        Dim RsFonti As Data.SqlClient.SqlDataReader
                        RsFonti = CmdFonti.ExecuteReader()
                        Do While RsFonti.Read
                            corpoMessaggio = corpoMessaggio & "<br>" & "- " & RsFonti("descrizione")
                        Loop
                        RsFonti.Close()
                        DbcFonti.Close()
                        RsFonti = Nothing
                        DbcFonti = Nothing
                        CmdFonti.Dispose()
                    End If

                    corpoMessaggio = corpoMessaggio & "<br>"
                    corpoMessaggio = corpoMessaggio & "<br>" & "Cordiali saluti<br><br>Giuseppe Calò"
                    corpoMessaggio = corpoMessaggio & "<br>" & "Responsabile Operativo"
                    corpoMessaggio = corpoMessaggio & "<br>" & "Sicily Rent Car"
                    corpoMessaggio = corpoMessaggio & "<br>" & "Via Cinisi, 3 Villagrazia di Carini (PA)"
                    corpoMessaggio = corpoMessaggio & "<br>" & "+39 091 63.90.327 Tel."
                    corpoMessaggio = corpoMessaggio & "<br>" & "+39 091 63.90.326 Fax"
                    corpoMessaggio = corpoMessaggio & "<br>" & "E-mail: giuseppe.calo@sbc.it "

                    corpoMessaggio = corpoMessaggio & "<br><br>" & "<font size=""1"">Questo messaggio è destinato solo ai soggetti indicati nell'intestazione. Nel caso doveste riceverlo per errore siete pregati di inviare una segnalazione usando i riferimenti sopra riportati e di procedere alla distruzione dello stesso come previsto dalle norme che regolano il trattamento dei dati personali."
                    corpoMessaggio = corpoMessaggio & "<br><br>" & "La informiamo che la nostra struttura effettua un trattamento dei dati personali conforme a quanto previsto dal D.lgs.196/2003. I dati da Lei forniti sono trattati con strumenti elettronici e cartacei da personale interno e comunicati a terzi solo per obblighi di legge o dietro suo consenso. Può rivolgersi alla nostra struttura per richiedere informazioni su titolare e responsabili del trattamento e per esercitare i diritti previsti dall'Art.7 del D.lgs.196/2003.</font>"

                    'mail.Body = "In data: " & Now() & "<br><br>" & Replace(corpoMessaggio, "!", "")
                    mail.Body = Replace(corpoMessaggio, "!", "")

                    'Imposta il server smtp di posta da utilizzare        
                    Dim Smtp As New SmtpClient("smtp.xinformatica.it")

                    'Invia l'e-mail
                    'Smtp.Send(mail)

                Loop
                RsGriglia.Close()
                DbcGriglia.Close()
                RsGriglia = Nothing
                DbcGriglia = Nothing
                CmdGriglia.Dispose()
            End If

            'Fine Invio Mail

            Libreria.genUserMsgBox(Page, "Salvataggio effettuato correttamente.")

            cercaStopSell.Visible = True
            nuovoStopSell.Visible = False

            ListStopSall.DataBind()



        Catch ex As Exception
            Response.Write("Error salva_stop_sale : " & ex.Message)
            Libreria.genUserMsgBox(Page, "Errore salva_stop_sale : " & ex.Message)
        End Try






    End Sub

    Protected Sub btnSalvaStopSell_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvaStopSell.Click
        If DateDiff(DateInterval.Day, CDate(stopDa.Text), CDate(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()))) > 0 Then
            Libreria.genUserMsgBox(Page, "La data di inizio stop sale deve essere uguale o successiva alla data odierna.")
        ElseIf DateDiff(DateInterval.Day, CDate(dataEfficacia.Text), CDate(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()))) > 0 Then
            Libreria.genUserMsgBox(Page, "La data di efficacia deve essere uguale o successiva alla data odierna.")
        ElseIf listStazioniSelezionate.Items.Count = 0 And radioSel1.SelectedValue = "0" Then
            Libreria.genUserMsgBox(Page, "Impossibile salvare. Selezionare almeno una stazione o scegliere l'opzione 'Tutti'.")
        ElseIf listGruppiSelezionati.Items.Count = 0 And radioSel2.SelectedValue = "0" Then
            Libreria.genUserMsgBox(Page, "Impossibile salvare. Selezionare almeno un gruppo o scegliere l'opzione 'Tutti'.")
        ElseIf listFontiSelezionate.Items.Count = 0 And radioSel3.SelectedValue = "0" Then
            Libreria.genUserMsgBox(Page, "Impossibile salvare. Selezionare almeno una fonte o scegliere l'opzione 'Tutti'.")
        Else
            salva_stop_sell()
        End If
    End Sub

    Protected Sub btnNuovoStopSall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovoStopSall.Click
        idStopSell.Text = ""

        cercaStopSell.Visible = False
        nuovoStopSell.Visible = True
        txtDescrizione.Text = ""
        stopA.Text = ""
        stopDa.Text = ""
        dataEfficacia.Text = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())
        dropMinuti.SelectedValue = "00"
        dropOre.SelectedValue = "00"

        listStazioniSelezionate.Items.Clear()
        listStazioni.Items.Clear()
        listStazioni.DataBind()
        listStazioni.Enabled = False

        listGruppiSelezionati.Items.Clear()
        listGruppi.Items.Clear()
        listGruppi.DataBind()
        listGruppi.Enabled = False

        listFontiSelezionate.Items.Clear()
        listFonti.Items.Clear()
        listFonti.DataBind()
        listFonti.Enabled = False


        radioSel1.SelectedValue = "1"
        radioSel2.SelectedValue = "1"
        radioSel3.SelectedValue = "1"

        TxtValidita.Text = ""

        btnSalvaStopSell.Visible = True
        btnTorna.Text = "Annulla"
    End Sub

    Protected Sub btnTorna_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTorna.Click
        lblNumero.Text = ""
        cercaStopSell.Visible = True
        nuovoStopSell.Visible = False
        ListStopSall.Visible = False
    End Sub

    Protected Sub btnCercaStopSall_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCercaStopSall.Click
        query.Text = "SELECT CONVERT(char(10), da_data, 103) As da_data, CONVERT(char(10), a_data, 103) As a_data, descrizione,da_data as dd, id, data_efficacia FROM stop_sell WHERE 1=1"
        lblNoOrderBY.Text = query.Text
        Session("OrderBy") = ""

        If Trim(txtCercaStopSell.Text) <> "" Then
            Dim descrizione = Replace(txtCercaStopSell.Text, "'", "''")
            query.Text = query.Text & " AND descrizione LIKE '%" & descrizione & "%'"
        End If
        If Trim(txtNumStopSale.Text) <> "" Then
            Dim test As Integer

            Try
                test = CInt(txtNumStopSale.Text)
                query.Text = query.Text & " AND id = '" & txtNumStopSale.Text & "'"
            Catch ex As Exception
                txtNumStopSale.Text = ""
            End Try

        End If
        If Trim(txtCercaPerData.Text) <> "" Then
            Dim da_data As String = txtCercaPerData.Text
            Dim a_data As String = txtCercaPerData.Text
            If Request.ServerVariables("HTTP_HOST") = "sviluppo.sicilybycar.it" Then
                da_data = Year(da_data) & "-" & Day(da_data) & "-" & Month(da_data) & " 00:00:00"
                a_data = Year(a_data) & "-" & Day(a_data) & "-" & Month(a_data) & " 23:59:59"
            Else
                da_data = Year(da_data) & "-" & Month(da_data) & "-" & Day(da_data) & " 00:00:00"
                a_data = Year(a_data) & "-" & Month(a_data) & "-" & Day(a_data) & " 23:59:59"
            End If
            query.Text = query.Text & " AND (da_data ='" & da_data & "' OR a_data ='" & a_data & "')"
        End If
        query.Text = query.Text & " ORDER BY dd"
        'Response.Write(query.Text)
        SqlStopSall.SelectCommand = query.Text

        Session("cercastopsall") = SqlStopSall.SelectCommand

        ListStopSall.Visible = True
        ListStopSall.DataBind()

    End Sub
    Protected Sub ListStopSall_PagePropertiesChanging(ByVal sender As Object, ByVal e As PagePropertiesChangingEventArgs)
        'Clear the selected index.
        ListStopSall.SelectedIndex = -1

        Try
            SqlStopSall.SelectCommand = Session("cercastopsall")
            ListStopSall.DataBind()

        Catch ex As Exception
            Response.Write("<h1>error ListStopSall_PagePropertiesChanging :" & ex.Message & "</h1><br/>")
        End Try




    End Sub


    Protected Sub ListStopSall_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListStopSall.ItemCommand
        If e.CommandName = "modificaStopSell" Then
            cercaStopSell.Visible = False
            nuovoStopSell.Visible = True
            Dim ModDescrizione As Label = e.Item.FindControl("descrizioneLabel")
            Dim ModDaData As Label = e.Item.FindControl("da_dataLabel")
            Dim ModAData As Label = e.Item.FindControl("a_dataLabel")
            Dim id_stop_sell As Label = e.Item.FindControl("idLabel")
            Dim lblDataEfficacia As Label = e.Item.FindControl("lblDataEfficacia")

            'Valorizzo campo ValiditÃ 
            lblNumero.Text = " - Stop Sale Numero " & id_stop_sell.Text

            Dim SQL As String
            Dim Dbc As New Data.SqlClient.SqlConnection(SqlStopSall.ConnectionString)
            Dbc.Open()

            SQL = "select validita from  stop_sell WHERE id ='" & id_stop_sell.Text & "'"

            Dim Cmd As New Data.SqlClient.SqlCommand(SQL, Dbc)
            'Response.Write(CmdGriglia.CommandText & "<br><br>")
            'Response.End()            
            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows Then
                Do While Rs.Read
                    If (Rs("validita") & "") <> "" Then
                        TxtValidita.Text = Rs("validita")
                    End If
                Loop
            End If

            idStopSell.Text = id_stop_sell.Text
            txtDescrizione.Text = ModDescrizione.Text
            stopDa.Text = ModDaData.Text
            stopA.Text = ModAData.Text
            dataEfficacia.Text = Day(lblDataEfficacia.Text) & "/" & Month(lblDataEfficacia.Text) & "/" & Year(lblDataEfficacia.Text)
            'TxtValidita.Text = lblValidita.Text

            Dim ore_efficacia As String = Hour(lblDataEfficacia.Text)
            If Len(ore_efficacia) = 1 Then
                ore_efficacia = "0" & ore_efficacia
            End If

            Dim minuti_efficacia As String = Minute(lblDataEfficacia.Text)
            If Len(minuti_efficacia) = 1 Then
                minuti_efficacia = "0" & minuti_efficacia
            End If

            dropOre.Text = ore_efficacia
            dropMinuti.SelectedValue = minuti_efficacia

            btnSalvaStopSell.Visible = True

            btnTorna.Text = "Torna alla lista"

            listStazioni.Items.Clear()
            listStazioni.DataBind()
            listStazioniSelezionate.Items.Clear()
            listStazioniSelezionate.DataBind()

            listGruppi.Items.Clear()
            listGruppi.DataBind()
            listGruppiSelezionati.Items.Clear()
            listGruppiSelezionati.DataBind()

            listFonti.Items.Clear()
            listFonti.DataBind()
            listFontiSelezionate.Items.Clear()
            listFontiSelezionate.DataBind()

            FillStazioni()
            FillGruppi()
            FillFonti()

            If permesso_accesso.Text = "2" Then
                btnSalvaStopSell.Visible = False

                listStazioni.Enabled = False
                listStazioniSelezionate.Enabled = False
                listGruppi.Enabled = False
                listGruppiSelezionati.Enabled = False
                listFonti.Enabled = False
                listFontiSelezionate.Enabled = False

                PassaUnoFonte.Enabled = False
                PassaUnoGruppo.Enabled = False
                PassaUnoStazioni.Enabled = False
                PassaTuttiFonte.Enabled = False
                PassaTuttiGruppo.Enabled = False
                PassaTuttiStazioni.Enabled = False
                TornaUnoStazioni.Enabled = False
                TornaUnoGruppo.Enabled = False
                TornaUnoFonte.Enabled = False
                TornaTuttiFonte.Enabled = False
                TornaTuttiGruppo.Enabled = False
                TornaTuttiStazioni.Enabled = False

                txtDescrizione.Enabled = False
                stopDa.Enabled = False
                stopA.Enabled = False
                dataEfficacia.Enabled = False
                dropOre.Enabled = False
                dropMinuti.Enabled = False

                radioSel1.Enabled = False
                radioSel2.Enabled = False
                radioSel3.Enabled = False

                TxtValidita.Enabled = False

            End If

            'Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            'Dbc.Open()

            'Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM stop_sell WHERE id = " & id_stop_sell.Text & "", Dbc)
            'Cmd.ExecuteNonQuery()

            'Cmd.Dispose()
            'Cmd = Nothing
            'Dbc.Close()
            'Dbc.Dispose()
            'Dbc = Nothing

        ElseIf e.CommandName = "eliminaStopSell" Then
            Dim id_stop_sell As Label = e.Item.FindControl("idLabel")
            'Response.Write(id_stop_sell.Text)

            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand("DELETE FROM stop_sell_stazioni WHERE id_stop_sell = " & id_stop_sell.Text & "", Dbc)
            Cmd.ExecuteNonQuery()
            Cmd = New Data.SqlClient.SqlCommand("DELETE FROM stop_sell_gruppi WHERE id_stop_sell = " & id_stop_sell.Text & "", Dbc)
            Cmd.ExecuteNonQuery()
            Cmd = New Data.SqlClient.SqlCommand("DELETE FROM stop_sell_fonti WHERE id_stop_sell = " & id_stop_sell.Text & "", Dbc)
            Cmd.ExecuteNonQuery()
            Cmd = New Data.SqlClient.SqlCommand("DELETE FROM stop_sell WHERE id = " & id_stop_sell.Text & "", Dbc)
            Cmd.ExecuteNonQuery()

            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            ListStopSall.DataBind()

            Libreria.genUserMsgBox(Page, "Eliminazione effettuata con successo")
        ElseIf e.CommandName = "order_by_NumStopSale" Then
            Select Case Session("OrderBy")
                Case Is = ""
                    Session("OrderBy") = " ORDER BY id asc"
                Case Is = " ORDER BY id desc"
                    Session("OrderBy") = " ORDER BY id asc"
                Case Is = " ORDER BY id asc"
                    Session("OrderBy") = " ORDER BY id desc"
            End Select
            SqlStopSall.SelectCommand = lblNoOrderBY.Text & Session("OrderBy")
            'Response.Write(SqlStopSall.SelectCommand)

            Session("cercastopsall") = SqlStopSall.SelectCommand

            ListStopSall.Visible = True
            ListStopSall.DataBind()
        ElseIf e.CommandName = "order_by_DataDa" Then
            lblNoOrderBY.Text = "SELECT da_data,  a_data, descrizione,da_data as dd, id, data_efficacia FROM stop_sell WHERE 1=1 "
            Select Case Session("OrderBy")
                Case Is = ""
                    Session("OrderBy") = " ORDER BY da_data asc"
                Case Is = " ORDER BY da_data desc"
                    Session("OrderBy") = " ORDER BY da_data asc"
                Case Is = " ORDER BY da_data asc"
                    Session("OrderBy") = " ORDER BY da_data desc"
            End Select
            SqlStopSall.SelectCommand = lblNoOrderBY.Text & Session("OrderBy")
            Response.Write(SqlStopSall.SelectCommand)

            Session("cercastopsall") = SqlStopSall.SelectCommand

            ListStopSall.Visible = True
            ListStopSall.DataBind()
        End If
    End Sub

    Protected Sub FillStazioni()

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM stop_sell_stazioni WHERE id_stop_sell =" & idStopSell.Text & "", Dbc)
        Cmd.ExecuteNonQuery()

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        If Rs.Read() Then
            radioSel1.SelectedValue = 0
            Dim k As Integer = 0

            Do
                For i = 0 To listStazioni.Items.Count - 1
                    If listStazioni.Items(i).Value = Rs("id_stazione") Then
                        listStazioniSelezionate.Items.Add(listStazioni.Items(i).Text)
                        listStazioniSelezionate.Items(k).Value = Rs("id_stazione")
                        listStazioni.Items.RemoveAt(i)
                        k = k + 1
                        Exit For
                    End If
                Next
            Loop Until Not Rs.Read()

            listStazioni.Enabled = True
            listStazioniSelezionate.Enabled = True
            PassaTuttiStazioni.Enabled = True
            PassaUnoStazioni.Enabled = True
            TornaTuttiStazioni.Enabled = True
            TornaUnoStazioni.Enabled = True

        Else

            radioSel1.SelectedValue = 1

            listStazioni.Enabled = False
            listStazioniSelezionate.Enabled = False
            PassaTuttiStazioni.Enabled = False
            PassaUnoStazioni.Enabled = False
            TornaTuttiStazioni.Enabled = False
            TornaUnoStazioni.Enabled = False

        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

    End Sub

    Protected Sub FillGruppi()

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM stop_sell_gruppi WHERE id_stop_sell =" & idStopSell.Text & "", Dbc)
        Cmd.ExecuteNonQuery()

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        If Rs.Read() Then
            radioSel2.SelectedValue = 0
            Dim k As Integer = 0

            Do
                For i = 0 To listGruppi.Items.Count - 1
                    If listGruppi.Items(i).Value = Rs("id_gruppo") Then
                        listGruppiSelezionati.Items.Add(listGruppi.Items(i).Text)
                        listGruppiSelezionati.Items(k).Value = Rs("id_gruppo")
                        listGruppi.Items.RemoveAt(i)
                        k = k + 1
                        Exit For
                    End If
                Next
            Loop Until Not Rs.Read()

            listGruppi.Enabled = True
            listGruppiSelezionati.Enabled = True
            PassaTuttiGruppo.Enabled = True
            PassaUnoGruppo.Enabled = True
            TornaTuttiGruppo.Enabled = True
            TornaUnoGruppo.Enabled = True

        Else

            radioSel2.SelectedValue = 1

            listGruppi.Enabled = False
            listGruppiSelezionati.Enabled = False
            PassaTuttiGruppo.Enabled = False
            PassaUnoGruppo.Enabled = False
            TornaTuttiGruppo.Enabled = False
            TornaUnoGruppo.Enabled = False

        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

    End Sub

    Protected Sub FillFonti()

        Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM stop_sell_fonti WHERE id_stop_sell =" & idStopSell.Text & "", Dbc)
        Cmd.ExecuteNonQuery()

        Dim Rs As Data.SqlClient.SqlDataReader
        Rs = Cmd.ExecuteReader()

        If Rs.Read() Then
            radioSel3.SelectedValue = 0
            Dim k As Integer = 0

            Do
                For i = 0 To listFonti.Items.Count - 1
                    If listFonti.Items(i).Value = Rs("id_fonte") Then
                        listFontiSelezionate.Items.Add(listFonti.Items(i).Text)
                        listFontiSelezionate.Items(k).Value = Rs("id_fonte")
                        listFonti.Items.RemoveAt(i)
                        k = k + 1
                        Exit For
                    End If
                Next
            Loop Until Not Rs.Read()

            listFonti.Enabled = True
            listFontiSelezionate.Enabled = True
            PassaTuttiFonte.Enabled = True
            PassaUnoFonte.Enabled = True
            TornaTuttiFonte.Enabled = True
            TornaUnoFonte.Enabled = True

        Else

            radioSel3.SelectedValue = 1

            listFonti.Enabled = False
            listFontiSelezionate.Enabled = False
            PassaTuttiFonte.Enabled = False
            PassaUnoFonte.Enabled = False
            TornaTuttiFonte.Enabled = False
            TornaUnoFonte.Enabled = False

        End If

        Rs.Close()
        Rs = Nothing
        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing

    End Sub


    Protected Sub ListStopSall_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles ListStopSall.ItemDataBound
        If permesso_accesso.Text = "2" Then
            e.Item.FindControl("btnElimina").Visible = False
        End If
    End Sub
End Class
