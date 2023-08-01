Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports funzioni_comuni
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Security

Partial Class gestione_multe
    Inherits System.Web.UI.Page
    Dim funzioni As New funzioni_comuni
    Dim cPagamenti As New Pagamenti


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        'verifico il livello di accesso ed assegno l'esito al controllo nascosto per l'eventuale accesso al dettaglio pag
        If Request.Cookies("SicilyRentCar") Is Nothing Then
            Response.Redirect("LogIn.aspx")
        End If


        'Trace.Write("Page_Load ------------------------ gestione_multe-id multa" & Session("IdMulta"))
        AddHandler anagrafica_conducenti1.scegliConducente, AddressOf scegli_conducente
        AddHandler Scambio_Importo1.ScambioImportoClose, AddressOf ScambioImportoClose
        AddHandler Scambio_Importo1.ScambioImportoTransazioneEseguita, AddressOf ScambioImportoTransazioneEseguita
        AddHandler Scambio_Importo1.CassaPagamentoEseguito, AddressOf CassaPagamentoEseguito


        AddHandler FattureMulte1.FattureMulteClose, AddressOf FattureMulteClose
        AddHandler Enti1.EntiClose, AddressOf Enti_close

        AddHandler ArticoliCDS1.ArticoliCDSClose, AddressOf ArticoliCDS_close
        AddHandler anagrafica_ditte1.scegliDitta, AddressOf scegliDitta


        'ScriptManager.GetCurrent(Me.Page).Scripts.Add(New ScriptReference("lytebox.js"))
        AggiornaTotaleDaIncassare()
        'Trace.Write("--------------- codice utente collegato: " & Integer.Parse(Request.Cookies("SicilyRentCar")("idUtente")))
        If Not Page.IsPostBack Then


            livello_accesso_eliminare_pagamenti.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.EliminarePagamenti)

            Dim NumeroIdMulta As String = Request.QueryString("IdMulta")

            Dim numprot As String = Request.QueryString("prot")  '26.04.2022 ricerca diretta tramite numprot


            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), 93) = "1" Then
                Response.Redirect("default.aspx")
            End If
            livello_accesso_dettaglio_pos.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "90")
            livello_accesso_gestMulte.Text = funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "93")
            'disattivo il pulsante incassi se utente non autorizzato
            If livello_accesso_dettaglio_pos.Text <> "3" Then
                btnIncassi.Enabled = False
            End If

            'recupera idmulta dal numero di protocollo dell'anno corrente 
            'se numprot presente da stringa 26.04.2022
            If numprot <> "" Then
                NumeroIdMulta = funzioni_comuni_new.GetIDMultaFromProt(numprot)
            End If


            If Len(NumeroIdMulta) > 0 Then 'carica i dati memorizzati con ID passato da QueryString
                ListComuni.Visible = False
                DropEnti.DataBind()
                DropProvenienza.DataBind()
                DropCasistiche.DataBind()
                DropArtCDS.DataBind()
                DropTipoMancInc.DataBind()
                DropLocatori.DataBind()

                btnSalvaModifiche.Visible = True
                btnElimina.Visible = True
                btnAssegnaProt.Visible = False
                btnAssegnaProtVuoto.Visible = False
                btnRicercaMovim.Visible = True
                btnRicercaTuttiMov.Visible = True
                div_steps.Visible = True
                txtAnno.Enabled = False
                txtProt.Enabled = False
                txtDataInser.Enabled = False
                visualizza_movimAuto.Visible = False
                CaricaDatiMemorizzati(Integer.Parse(NumeroIdMulta))
                AggiornaListAllegatiMulte(NumeroIdMulta)
                AggiornaDatiEmail()
                If txtTarga.Text = "" Then
                    txtTarga.Enabled = True
                Else
                    txtTarga.Enabled = False 'targa non più modificabile non la registrazione prot. (l'hanno richiesto esplicitamente)
                End If

            Else 'altrimenti nuovo inserimento
                ListComuni.Visible = False
                DropEnti.DataBind()
                DropProvenienza.DataBind()
                DropCasistiche.DataBind()
                DropCasistiche.SelectedIndex = 0
                DropArtCDS.SelectedIndex = 0
                AggiornaCasistiche(CInt(DropCasistiche.SelectedValue.ToString))
                DropTipoAllegato.DataBind()
                DropTipoMancInc.SelectedIndex = 0

                btnSalvaModifiche.Visible = False
                btnElimina.Visible = False
                btnAssegnaProt.Visible = True
                btnAssegnaProtVuoto.Visible = True
                btnRicercaMovim.Visible = False
                btnRicercaTuttiMov.Visible = False
                div_steps.Visible = False
                DropStato.SelectedIndex = 0
                txtAnno.Enabled = False
                txtProt.Enabled = False
                txtDataInser.Enabled = False
                visualizza_movimAuto.Visible = False
                txtTarga.Enabled = True


                AssegnaNuovoProtocollo()
                If txtTarga.Text = "" Then
                    txtTarga.Enabled = True
                Else
                    txtTarga.Enabled = False 'targa non più modificabile non la registrazione prot. (l'hanno richiesto esplicitamente)
                End If

                '# nuovo caricamento salvo 24.03.2023
                sqlAllegati.SelectCommand = "SELECT multe_Allegati.Id, rtrim(multe_TipoAllegato.TipoAllegato) as TipoAllegato, multe_Allegati.NomeFile, multe_Allegati.PercorsoFile, " &
                "operatori.cognome + ' ' + operatori.nome AS Operatore, dataCreazione FROM multe_Allegati WITH (NOLOCK) INNER JOIN multe_TipoAllegato WITH (NOLOCK) " &
                "ON multe_Allegati.IdTipoDocumento = multe_TipoAllegato.Id LEFT OUTER JOIN  operatori ON multe_Allegati.id_operatore = operatori.id " &
                "WHERE (multe_Allegati.IdMulta =-1)"
                ListViewAllegati.DataBind()
                '@ salvo




            End If

            'di seguito predispongo traccia del testo da integrare nel ricorso "Veicolo altro luogo"
            'questo è l'unico ricorso che presenta una parte del testo variabile e non prevedibile e quindi da vedere caso per caso
            Dim testo As String = "presso la ns. stazione di noleggio sita all'Aeroporto ........., infatti, alle ore .... "
            testo = testo & "del ... veniva riconsegnato dal ns. cliente sig. ...... per la chiusura del contratto n. ....... "
            testo = testo & "e successivamente, in data .... veniva trasferita da un ns. volante presso la ns. stazione "
            testo = testo & "di .... dove in data .... alle ore .... è stata riconsegnata per un nuovo noleggio al ns. cliente sig..... "
            testo = testo & "con contratto n........ " & vbCrLf
            testo = testo & "Quanto detto sopra è provato dalle copie dei contratti che alleghiamo in copia al presente ricorso."
            txtdescrAltroLuogo.Text = testo
            'di seguito predispongo traccia del testo da integrare nel ricorso "Veicolo per furto"
            Dim testo2 As String = "Si allegano i seguenti documenti:" & vbCrLf
            testo2 = testo2 & " " & vbCrLf
            testo2 = testo2 & "denuncia di appropriazione indebita;" & vbCrLf
            testo2 = testo2 & "contratto di noleggio;" & vbCrLf
            testo2 = testo2 & "copia documenti del conducente;" & vbCrLf
            testo2 = testo2 & "copia carta di circolazione;" & vbCrLf
            testo2 = testo2 & "annotazione perdita di possesso;" & vbCrLf
            testo2 = testo2 & "verbale in originale;" & vbCrLf
            testo2 = testo2 & "copia documento di riconoscimento."
            txtAllegatiRicorso.Text = testo2

            'disattivo gli altri punsanti se l'utente è abilitato solo in lettura
            If livello_accesso_gestMulte.Text <> "3" Then
                btnNuovo.Visible = False
                btnSalvaModifiche.Visible = False
                btnElimina.Visible = False
                btnAssegnaProt.Visible = False
                btnAssegnaProtVuoto.Visible = False
                btnRicercaMovim.Enabled = False
                btnRicercaTuttiMov.Enabled = False
                btnCercaComuni.Enabled = False
                btnInviaEmail.Enabled = False
                btnMemorizzaAlleg.Enabled = False
                btnNuovaFattura.Enabled = False
                btnRicorsoPrevisto.Enabled = False
                btnComunicazCliente.Enabled = False
                btnRilevaAllegAuto.Enabled = False
                btnSelezionaConducente.Enabled = False
                CheckSalvaComeAllegato.Enabled = False
                DropEnti.Enabled = False
                DropCasistiche.Enabled = False
                ImageButtonNewEnte.Enabled = False
            End If
        Else
            sqlMovAuto.SelectCommand = Session("SqlSelectMovAuto") 'viene memorizzata la stringa sql per il ListViewMovAuto durante il postback
            'ListViewMovAuto.DataBind()
            'tabPanelSteps.ActiveTabIndex = 0
            'ListViewMailInviate.DataBind()
            'Trace.Write("-------------------------------- post back gestione multe-id multa" & Session("IdMulta"))

        End If
    End Sub

    Private Sub scegli_conducente(ByVal sender As Object, ByVal e As anagrafica_anagrafica_conducenti.ScegliConducenteEventArgs)
        GetDatiAttualiConducente(e.id_conducente)
        anagrafica_conducenti1.Visible = False
        'UpdatePanel1.Visible = True
        div_steps.Visible = True
    End Sub

    Private Sub scegliDitta(ByVal sender As Object, ByVal e As anagrafica_anagrafica_ditte.ScegliDittaEventArgs)
        txtCodClienteVendita.Text = e.id_ditta
        txtAcquirente.Text = e.ragione_sociale
        anagrafica_ditte1.Visible = False
        'UpdatePanel1.Visible = True
        div_steps.Visible = True
    End Sub

    Protected Sub DropCasistiche_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropCasistiche.SelectedIndexChanged
        'txtEnteIndir.Text = DropCasistiche.SelectedIndex.ToString()
        'txtEnteIndir.Text = DropCasistiche.SelectedValue.ToString

        If DropCasistiche.SelectedItem.ToString = "Seleziona..." Then
            ChkRinotifica.Checked = False
            ChkAddServFee.Checked = False
            ChkAddVerbale.Checked = False
            ChkIncCartaCred.Checked = False
            ChkFattura.Checked = False
            ChkRicorso.Checked = False
        Else
            AggiornaCasistiche(CInt(DropCasistiche.SelectedValue.ToString))
        End If
        SalvaModifiche(False)

    End Sub

    Public Sub AggiornaCasistiche(ByVal _id As Integer)

        Dim MyReader As SqlDataReader
        Dim MyConnection As SqlConnection = New SqlConnection

        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString
        Dim MyCommand As SqlCommand = New SqlCommand()
        MyCommand.CommandText = "Select * from multe_casistiche where ID=" & _id
        MyCommand.CommandType = CommandType.Text
        MyCommand.Connection = MyConnection

        MyCommand.Connection.Open()
        MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

        While MyReader.Read()
            ChkRinotifica.Checked = CBool(MyReader("RinotificaPrevisto").ToString)
            ChkAddServFee.Checked = CBool(MyReader("AddebFeePrevisto").ToString)
            ChkAddVerbale.Checked = CBool(MyReader("AddebVerbPrevisto").ToString)
            ChkIncCartaCred.Checked = CBool(MyReader("IncassoPrevisto").ToString)
            ChkFattura.Checked = CBool(MyReader("FatturaPrevista").ToString)
            ChkRicorso.Checked = CBool(MyReader("RicorsoPrevisto").ToString)
            txtServFeeDaInc.Text = MyReader("CostoFee")
            If MyReader("AddebVerbPrevisto") = True Then
                txtMultaDaInc.Text = txtImporto.Text
            Else
                txtMultaDaInc.Text = 0
            End If

            Dim totDaInc As Single = 0
            If IsNumeric(txtServFeeDaInc.Text) Then
                totDaInc = totDaInc + CSng(txtServFeeDaInc.Text)
            End If
            If IsNumeric(txtMultaDaInc.Text) Then
                totDaInc = totDaInc + CSng(txtMultaDaInc.Text)
            End If
            If IsNumeric(txtAltroDaInc.Text) Then
                totDaInc = totDaInc + CSng(txtAltroDaInc.Text)
            End If
            txtTotDaInc.Text = totDaInc

        End While

        MyReader.Close()

    End Sub

    Public Function GetReader(ByVal _sqlReader As String) As SqlDataReader

        Dim MyReader As SqlDataReader
        Dim MyConnection As SqlConnection = New SqlConnection

        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString
        Dim MyCommand As SqlCommand = New SqlCommand()
        MyCommand.CommandText = _sqlReader
        MyCommand.CommandType = CommandType.Text
        MyCommand.Connection = MyConnection

        MyCommand.Connection.Open()
        MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

        Return MyReader
    End Function

    Protected Sub btnAssegnaProt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssegnaProt.Click
        'mettere ulteriori verifiche
        If Not IsDate(txtNotifica.Text) Then
            Libreria.genUserMsgBox(Page, "Data notifica non corretta")
            Return
        End If

        If Not IsDate(txtDataInfrazione.Text) Then
            Libreria.genUserMsgBox(Page, "Data infrazione non corretta")
            Return
        End If

        If CDate(txtNotifica.Text) < CDate(txtDataInfrazione.Text) Then
            Libreria.genUserMsgBox(Page, "La data dell'infrazione non può essere superiore alla data della notifica")
            Return
        End If
        Dim verbaleEsistente As String = VerificaVerbaleEsistente(txtVerbale.Text)
        MemorizzaDati() 'insert
        'Trace.Write("------------------------Dopo la memorizzazione")
        btnSalvaModifiche.Visible = True
        btnElimina.Visible = True
        btnAssegnaProt.Visible = False
        btnAssegnaProtVuoto.Visible = False
        btnRicercaMovim.Visible = True
        btnRicercaTuttiMov.Visible = True
        div_steps.Visible = True
        AggiornaDatiEmail()
        If verbaleEsistente <> "" Then
            verbaleEsistente = "Dati memorizzati correttamente." & vbCrLf & "Attenzione!" & vbCrLf & verbaleEsistente
            Libreria.genUserMsgBox(Page, verbaleEsistente)
        End If

    End Sub

    Protected Sub MemorizzaDati()
        'insert
        Dim myMulte As New Multe

        With myMulte
            .UtenteID = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))
            .ProvenienzaID = DropProvenienza.SelectedValue
            .StatoAperto = True
            .EnteID = DropEnti.SelectedValue
            .EnteIndirizzo = txtEnteIndir.Text
            .EnteComune = txtEnteComune.Text
            .EnteCap = txtCap.Text
            .EnteProv = txtProv.Text
            .NumVerbale = txtVerbale.Text
            .DataNotifica = CDate(txtNotifica.Text)
            .ArticoloCDS = DropArtCDS.SelectedValue
            If txtImporto.Text = "" Then
                .MultaImporto = 0
            Else
                .MultaImporto = Double.Parse(txtImporto.Text)
            End If
            'MultaImporto = Replace(txtImporto.Text, ",", ".")
            .Targa = txtTarga.Text
            .DataInfrazione = Multe.UnisciDataOra(CDate(txtDataInfrazione.Text), Left(txtOraInfrazione.Text, 2), Right(txtOraInfrazione.Text, 2))
        End With

        Dim num_id As Integer
        num_id = myMulte.SalvaRecord()

        lblID.Text = num_id.ToString
        txtAnno.Text = myMulte.Anno
        txtProt.Text = myMulte.Prot
        txtDataInser.Text = myMulte.DataInserimento.ToString("dd/MM/yyyy")
    End Sub

    Protected Sub btnRicorsoPrevisto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRicorsoPrevisto.Click
        If DropCasistiche.SelectedIndex = 0 Then
            Libreria.genUserMsgBox(Page, "Scegliere una casistica")
            Exit Sub
        End If

        Dim myDati As StampaModelloMulte = New StampaModelloMulte

        'recupero i dati relativi al modello ricorso
        Dim ModRicorsoReader As SqlDataReader
        Dim strRd As String = "SELECT dbo.multe_ModelloRicorsi.idModello, dbo.multe_ModelloRicorsi.NomeModello, "
        strRd = strRd & "dbo.multe_ModelloRicorsi.PercRicorsoIT, dbo.multe_ModelloRicorsi.idTipoAllegato, "
        strRd = strRd & "dbo.multe_TipoAllegato.TipoAllegato FROM dbo.multe_casistiche INNER JOIN "
        strRd = strRd & "dbo.multe_ModelloRicorsi ON dbo.multe_casistiche.ModelloID = dbo.multe_ModelloRicorsi.idModello "
        strRd = strRd & "INNER JOIN dbo.multe_TipoAllegato ON dbo.multe_ModelloRicorsi.idTipoAllegato = dbo.multe_TipoAllegato.Id "
        strRd = strRd & "WHERE(dbo.multe_casistiche.ID =" & DropCasistiche.SelectedValue & ")"
        ModRicorsoReader = GetReader(strRd)

        While ModRicorsoReader.Read()
            myDati.IdModello = ModRicorsoReader("idModello")
            'myDati.NomeModello = ModRicorsoReader("NomeModello") 'preferisco non utilizzare il nome della casistica ma impostarlo come generico ricorso
            Dim dataTemp As Date = Now
            myDati.NomeModello = "Ricorso" & Year(dataTemp) & Month(dataTemp) & Day(dataTemp) & Hour(dataTemp) & Minute(dataTemp) & Second(dataTemp)
            myDati.PercorsoModello = ModRicorsoReader("PercRicorsoIT")

            If myDati.PercorsoModello = "" Then
                Libreria.genUserMsgBox(Page, "Nessun ricorso trovato rispetto alla casistica selezionata")
                Exit Sub
            End If

            myDati.TipoAllegato = ModRicorsoReader("idTipoAllegato")
            If CheckSalvaComeAllegato.Checked = True Then
                myDati.SalvaComeAllegato = True
            Else
                myDati.SalvaComeAllegato = False
            End If

        End While


        With myDati
            .IdMulta = lblID.Text
            .Prot = txtProt.Text
            .Anno = txtAnno.Text
            .Ente = DropEnti.SelectedItem.ToString
            .IndirizzoEnte = txtEnteIndir.Text
            .CapEnte = txtCap.Text
            .ComuneEnte = txtEnteComune.Text
            .ProvEnte = txtProv.Text
            '.DataInserimento = txtDataInser.Text
            .DataInserimento = Format(Now(), "dd/MM/yyyy")
            .NumVerbale = txtVerbale.Text
            .DataVerbale = txtDataInfrazione.Text
            .OraVerbale = txtOraInfrazione.Text
            .ArtCDS = DropArtCDS.SelectedItem.ToString
            .DataNotifica = txtNotifica.Text
            .Targa = txtTarga.Text
            .DataInizioNolo = txtDaDataNolo.Text
            .DataFineNolo = txtAdataNolo.Text
            .Conducente = txtConducente.Text
            .LuogoNascitaCond = txtLuogoNascitaCond.Text
            .DataNascitaCond = txtDataNascitaCond.Text
            .IndirizzoCond = txtIndirizzoCond.Text
            .ComuneComplCond = txtCapCond.Text & " " & txtComuneCond.Text & " (" & txtProvCond.Text & ")"
            .Nazione = txtNazioneCond.Text
            .PatenteCateg = txtCatPatente.Text
            .PatenteNum = txtPatente.Text
            .LuogoRilascioPatente = txtLuogoRilascioPatente.Text
            .DataRilascioPatente = txtDataRilascioPatente.Text
            .DataScadPatente = txtScadPatente.Text
            .ModelloAutoCorretto = txtModelloCorretto.Text
            .ModelloAutoErrato = txtModelloErrato.Text
            .NumPrecVerb = TxtNumPrecVerb.Text
            .DataPrecVerb = TxtDataPrecVerb.Text
            .OraPrecVerb = TxtOraPrecVerb.Text
            .DataNotificaPrecVerb = txtDataNotificaPrecVerb.Text
            .PuntiDecurtPrecVerb = txtPuntiDecurt.Text
            .NumRaccPrecVerb = txtNumRaccPrecVerb.Text
            .DataRaccPrecVerb = txtDataRaccPrecVerb.Text
            .Contratto = txtNumContratto.Text
            .IndirInfraz = txtIndirInfraz.Text
            .DescrizAltroLuogo = txtdescrAltroLuogo.Text
            .LuogoDenuncia = txtLuogoDenuncia.Text
            .DataDenuncia = txtDataDenuncia.Text
            .AllegatiRicorso = txtAllegatiRicorso.Text
        End With

        ModRicorsoReader.Close()

        'verifico se devo prendere i dati dell'acquirente della vettura
        If txtCodClienteVendita.Text <> "0" And txtCodClienteVendita.Text <> "" Then
            Dim myDittaReader As SqlDataReader
            Dim strDitta As String = "SELECT dbo.DITTE.Id_Ditta, dbo.DITTE.Rag_soc, dbo.DITTE.Indirizzo, dbo.DITTE.Citta, dbo.DITTE.provincia, "
            strDitta = strDitta & "dbo.DITTE.Cap, dbo.CONDUCENTI.Data_Nascita, dbo.CONDUCENTI.Luogo_Nascita, dbo.nazioni.NAZIONE AS NazioneResidenza "
            strDitta = strDitta & "FROM dbo.nazioni RIGHT OUTER JOIN dbo.DITTE ON dbo.nazioni.ID_NAZIONE = dbo.DITTE.NAZIONE LEFT OUTER JOIN "
            strDitta = strDitta & "dbo.CONDUCENTI ON dbo.DITTE.ID_Cliente = dbo.CONDUCENTI.ID_Cliente "
            strDitta = strDitta & "WHERE dbo.DITTE.Id_Ditta = " & Integer.Parse(txtCodClienteVendita.Text)
            myDittaReader = GetReader(strDitta)

            While myDittaReader.Read()
                myDati.Conducente = myDittaReader("Rag_soc")
                If IsDBNull(myDittaReader("Luogo_Nascita")) Then
                    myDati.LuogoNascitaCond = ""
                Else
                    myDati.LuogoNascitaCond = myDittaReader("Luogo_Nascita")
                End If

                If IsDBNull(myDittaReader("Data_Nascita")) Then
                    myDati.DataNascitaCond = ""
                Else
                    myDati.DataNascitaCond = myDittaReader("Data_Nascita")
                End If

                myDati.IndirizzoCond = myDittaReader("Indirizzo") & ""
                myDati.ComuneComplCond = myDittaReader("Cap") & " " & myDittaReader("Citta") & " (" & myDittaReader("provincia") & ")"
                If IsDBNull(myDittaReader("NazioneResidenza")) Then
                    myDati.Nazione = ""
                Else
                    myDati.Nazione = myDittaReader("NazioneResidenza")
                End If

            End While
            myDati.DataVendita = txtDataVendita.Text

            myDittaReader.Close()
        End If

        Session("StampaModelloMulte") = myDati

        Dim Generator As System.Random = New System.Random()

        Dim num_random As String = Format(Generator.Next(100000000), "000000000")

        If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraModelloMulte.aspx?a=" & num_random & "','')", True)
        End If

        AggiornaStatusRicorsoYesNo(lblID.Text)
        chkRicorsoYesNo.Checked = True
    End Sub

    Protected Sub btnSalvaModifiche_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvaModifiche.Click
        SalvaModifiche(True) ' con true da un messaggio di avvenuto salvataggio dei dati (a volte utilizzo un salvataggio obbligato senza messaggio)
    End Sub
    Protected Sub SalvaModifiche(ByVal MessYesNo As Boolean)
        If Len(txtProt.Text) = 0 Then
            Exit Sub
        End If
        'Trace.Write("prima di richiamare la funzione ------------------------ gestione_multe----....")

        If Trim(txtNotifica.Text) = "" Or Trim(txtDataInfrazione.Text) = "" Then
            Libreria.genUserMsgBox(Page, "Specificare la data di notifica e la data di infrazione.")
            Return
        End If

        If Trim(txtOraInfrazione.Text) = "" Then
            Libreria.genUserMsgBox(Page, "Specificare l'ora dell'infrazione.")
            Return
        End If

        If CDate(txtNotifica.Text) < CDate(txtDataInfrazione.Text) Then
            Libreria.genUserMsgBox(Page, "La data dell'infrazione non può essere superiore alla data della notifica")
            Return
        End If

        Dim MessagioErrore As String = ControllaDatiConducente()
        If MessagioErrore <> "" Then
            Libreria.genUserMsgBox(Page, MessagioErrore)
            Exit Sub
        End If

        AggiornaDati(MessYesNo)
        txtTarga.Enabled = False 'targa non più modificabile non la registrazione prot. (l'hanno richiesto esplicitamente)
    End Sub

    Function ControllaDatiConducente() As String
        Dim MessErrore As String = ""
        If Not IsDate(txtNotifica.Text) Or txtNotifica.Text = "" Then
            MessErrore = MessErrore & ControlChars.CrLf & "Data notifica assente o non corretta"
        End If

        If Not IsDate(txtDataInfrazione.Text) Or txtDataInfrazione.Text = "" Then
            MessErrore = MessErrore & ControlChars.CrLf & "Data infrazione assente o non corretta"
        End If

        If txtDataNascitaCond.Text <> "" Then
            If Not IsDate(txtDataNascitaCond.Text) Then
                MessErrore = MessErrore & ControlChars.CrLf & "Data nascita conducente non corretta"
            End If
        End If

        If txtDataRilascioPatente.Text <> "" Then
            If Not IsDate(txtDataRilascioPatente.Text) Then
                MessErrore = MessErrore & ControlChars.CrLf & "Data rilascio patente non corretta"
            End If
        End If

        'Trace.Write("funzione controlla dati conducente  ------------------------ gestione_multe"
        Return MessErrore
    End Function

    Protected Sub AggiornaDati(ByVal messYesNo As Boolean)
        Dim myMulte As New Multe

        With myMulte
            .ID = lblID.Text
            .ProvenienzaID = DropProvenienza.SelectedValue
            .StatoAperto = DropStato.SelectedValue
            .EnteID = DropEnti.SelectedValue
            .EnteIndirizzo = txtEnteIndir.Text
            .EnteComune = txtEnteComune.Text
            .EnteCap = txtCap.Text
            .EnteProv = txtProv.Text
            .NumVerbale = txtVerbale.Text
            .DataNotifica = CDate(txtNotifica.Text)
            .ArticoloCDS = DropArtCDS.SelectedValue
            If txtImporto.Text = "" Then
                .MultaImporto = 0
            Else
                .MultaImporto = Double.Parse(txtImporto.Text)
            End If
            'MultaImporto = Replace(txtImporto.Text, ",", ".")
            .Targa = txtTarga.Text
            .DataInfrazione = Multe.UnisciDataOra(CDate(txtDataInfrazione.Text), Left(txtOraInfrazione.Text, 2), Right(txtOraInfrazione.Text, 2))
            .CasisticaID = DropCasistiche.SelectedValue
            .IDConducente = ImpostaCampoInt(txtIDCond.Text)
            .CodCliente = ImpostaCampoInt(txtCodCliente.Text)
            .ContrattoNolo = ImpostaCampoInt(txtNumContratto.Text)
            .StazioneInizio = ImpostaCampoTesto(txtDaStazioneNolo.Text)
            .StazioneFine = ImpostaCampoTesto(txtAStazioneNolo.Text)
            Dim _datainizioNolo As Date = ImpostaCampoData(txtDaDataNolo.Text)
            Trace.Write("-------- datainizionolo: " & _datainizioNolo)

            If Len(txtDaOraNolo.Text) = 5 Then
                .DataInizioNolo = Multe.UnisciDataOra(_datainizioNolo, Left(txtDaOraNolo.Text, 2), Right(txtDaOraNolo.Text, 2))
            Else
                .DataInizioNolo = _datainizioNolo
            End If
            Trace.Write("-------- datainizionolo: " & .DataInizioNolo)
            Dim _datafineNolo As Date = ImpostaCampoData(txtAdataNolo.Text)
            If Len(txtAOraNolo.Text) = 5 Then
                .DataFineNolo = Multe.UnisciDataOra(_datafineNolo, Left(txtAOraNolo.Text, 2), Right(txtAOraNolo.Text, 2))
            Else
                .DataFineNolo = _datafineNolo
            End If

            .NumCartaCredito = ImpostaCampoTesto(txtNumeroCartaCred.Text)
            .ScadCartaCredito = ImpostaCampoTesto(txtScadCartaCred.Text)
            .AcquirenteMezzo = ImpostaCampoTesto(txtAcquirente.Text)
            .DataVenditaMezzo = ImpostaCampoData(txtDataVendita.Text)
            .NumFattVendMezzo = ImpostaCampoInt(txtNumFattVendita.Text)
            .CodClienteVendMezzo = ImpostaCampoInt(txtCodClienteVendita.Text)
            .AltroResponsMulta = ImpostaCampoTesto(txtAltroResponsabile.Text)
            .NumDocAltroCaso = ImpostaCampoTesto(txtNumDocAltro.Text)
            .DataDocAltroCaso = ImpostaCampoData(txtDataDocAltro.Text)
            .DescrizAltroCaso = ImpostaCampoTesto(txtDescrizAltro.Text)
            .Note = ImpostaCampoTesto(txtNote.Text)
            .RicorsoYesNo = chkRicorsoYesNo.Checked
            .RicorsoNumRacc = ImpostaCampoTesto(txtNumRaccomandata.Text)
            .RicorsoDataRacc = ImpostaCampoData(txtDataRacc.Text)
            .RicorsoDataFax = ImpostaCampoData(txtDataInoltroFax.Text)
            .ComunicazClienteYesNo = chkComunizYesNo.Checked
            .DataMailCliente = ImpostaCampoData(txtDataInoltroMail.Text)
            .IncassatoYesNo = chkIncassatoYesNo.Checked
            .NumTentativiIncassi = ImpostaCampoInt(txtTentativiInc.Text)
            .MotivoMancInc = ImpostaCampoInt(DropTipoMancInc.SelectedValue)
            .RimborsatoYesNo = chkRimborsatoYesNo.Checked
            .RimborsatoData = ImpostaCampoData(txtRimborsatoData.Text)
            If txtRimborsatoImporto.Text = "" Then
                .RimborsatoImporto = 0
            Else
                .RimborsatoImporto = Double.Parse(txtRimborsatoImporto.Text)
            End If
            .PagatoYesNo = ChkPagatoYesNo.Checked
            .PagatoData = ImpostaCampoData(txtPagatoData.Text)
            If txtPagatoImporto.Text = "" Then
                .PagatoImporto = 0
            Else
                .PagatoImporto = Double.Parse(txtPagatoImporto.Text)
            End If
            .FatturatoYesNo = chkFatturatoYesNo.Checked
            .LocatoreId = ImpostaCampoInt(DropLocatori.SelectedValue)
            .LocatoreNumFatt = ImpostaCampoTesto(txtNumFattTerziLoc.Text)
            .LocatoreDataFatt = ImpostaCampoData(txtDataFattTerziLoc.Text)
            'Trace.Write("controlla dati   ------------------------> gestione_multe" & .DataDocAltroCaso.ToString)

            'registra i valori dai campi db protocollo nuovi - salvo 20.12.2022
            .EnteEmail = ImpostaCampoTesto(txt_prot_enti_email.Text)
            .EnteEmailPec = ImpostaCampoTesto(txt_prot_enti_emailpec.Text)
            .EnteTel = ImpostaCampoTesto(txt_prot_enti_tel.Text)
            .EnteNotes = ImpostaCampoTesto(txt_prot_enti_notes.Text)

        End With

        Dim EsitoUpdate As Boolean
        EsitoUpdate = myMulte.AggiornaRecord()

        If EsitoUpdate And messYesNo = True = True Then
            Libreria.genUserMsgBox(Page, "Dati salvati correttamente.")
        End If

    End Sub

    Private Function ImpostaCampoInt(ByVal num As String) As Integer
        If num = "" Then
            Return Nothing
        Else
            If IsNumeric(num) Then
                Return CInt(num)
            Else
                Return 0
            End If
        End If
    End Function

    Private Function ImpostaCampoData(ByVal data As String) As Date
        If data = "" Then
            Return CDate("01/01/1800")
        Else
            If IsDate(data) Then
                Return CDate(data)
            Else
                Return CDate("01/01/1800")
            End If
        End If
    End Function

    Private Function ImpostaCampoTesto(ByVal data As String) As String
        If data = "" Then
            Return Nothing
        Else
            Return data
        End If
    End Function

    Protected Sub btnNuovo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovo.Click
        btnSalvaModifiche.Visible = False
        btnElimina.Visible = False
        btnAssegnaProt.Visible = True
        btnAssegnaProtVuoto.Visible = True
        btnRicercaMovim.Visible = False
        btnRicercaTuttiMov.Visible = False

        div_steps.Visible = False

        AzzeraCampi()
        DropCasistiche.DataBind()
        DropCasistiche.SelectedIndex = 0
        AggiornaCasistiche(CInt(DropCasistiche.SelectedValue.ToString))
        AggiornaListAllegatiMulte(0)
        txtTarga.Enabled = True 'targa non più modificabile non la registrazione prot. (l'hanno richiesto esplicitamente)
    End Sub
    Protected Sub AzzeraCampi()
        txtAnno.Text = ""
        txtProt.Text = ""
        txtDataInser.Text = ""
        DropProvenienza.SelectedIndex = 0
        DropStato.SelectedIndex = 0
        lblID.Text = ""
        DropEnti.SelectedIndex = 0
        txtEnteIndir.Text = ""
        txtEnteComune.Text = ""
        txtCap.Text = ""
        txtProv.Text = ""
        txtVerbale.Text = ""
        txtNotifica.Text = ""
        DropArtCDS.SelectedIndex = 0
        txtImporto.Text = ""
        txtTarga.Text = ""
        txtDataInfrazione.Text = ""
        txtOraInfrazione.Text = ""
        DropCasistiche.SelectedIndex = 0
        ChkRinotifica.Checked = False
        ChkAddServFee.Checked = False
        ChkAddVerbale.Checked = False
        ChkIncCartaCred.Checked = False
        ChkFattura.Checked = False
        ChkRicorso.Checked = False

        AzzeraCampiConduc_e_noleggio()

        txtAcquirente.Text = ""
        txtDataVendita.Text = ""
        txtNumFattVendita.Text = ""
        txtCodClienteVendita.Text = ""
        txtAltroResponsabile.Text = ""
        txtNumDocAltro.Text = ""
        txtDataDocAltro.Text = ""
        txtDescrizAltro.Text = ""
        txtNote.Text = ""
        chkRicorsoYesNo.Checked = False
        txtNumRaccomandata.Text = ""
        txtDataRacc.Text = ""
        txtDataInoltroFax.Text = ""
        chkComunizYesNo.Checked = False
        txtDataInoltroMail.Text = ""
        chkIncassatoYesNo.Checked = False
        txtTentativiInc.Text = ""
        DropTipoMancInc.SelectedIndex = 0
        chkRimborsatoYesNo.Checked = False
        txtRimborsatoData.Text = ""
        txtRimborsatoImporto.Text = ""
        ChkPagatoYesNo.Checked = False
        txtPagatoData.Text = ""
        txtPagatoImporto.Text = ""
        chkFatturatoYesNo.Checked = False
        DropLocatori.SelectedIndex = 0
        txtNumFattTerziLoc.Text = ""
        txtDataFattTerziLoc.Text = ""
        CheckSalvaComeAllegato.Checked = False
        txtServFeeDaInc.Text = 0
        txtMultaDaInc.Text = 0
        txtAltroDaInc.Text = 0
        txtDestinatarioMail.Text = ""
        txtPerConoscenzaMail.Text = ""
        txtOggettoMail.Text = ""
        txtTestoMail.Text = ""
    End Sub
    Protected Sub AzzeraCampiConduc_e_noleggio()
        txtConducente.Text = ""
        txtDataNascitaCond.Text = ""
        txtLuogoNascitaCond.Text = ""
        txtIndirizzoCond.Text = ""
        txtCapCond.Text = ""
        txtComuneCond.Text = ""
        txtProvCond.Text = ""
        txtNazioneCond.Text = ""
        txtPatente.Text = ""
        txtDataRilascioPatente.Text = ""
        txtLuogoRilascioPatente.Text = ""
        txtScadPatente.Text = ""
        txtCatPatente.Text = ""
        txtCodCliente.Text = ""
        txtIDCond.Text = ""
        txtNumContratto.Text = ""
        txtDaStazioneNolo.Text = ""
        txtAStazioneNolo.Text = ""
        txtDaDataNolo.Text = ""
        txtAdataNolo.Text = ""
        txtDaOraNolo.Text = ""
        txtAOraNolo.Text = ""
        txtNumeroCartaCred.Text = ""
        txtScadCartaCred.Text = ""
    End Sub
    Protected Sub btnRicercaMovim_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRicercaMovim.Click
        Dim MovimVeicoli As SqlDataReader
        Dim strRd As String = "SELECT dbo.movimenti_targa.id, dbo.tipologia_movimenti.descrzione, dbo.movimenti_targa.num_riferimento, "
        strRd = strRd & "dbo.veicoli.targa, dbo.stazioni.nome_stazione AS StazUscita, stazioni_1.nome_stazione AS StazRientro, "
        strRd = strRd & "dbo.movimenti_targa.data_uscita, dbo.movimenti_targa.data_rientro, dbo.movimenti_targa.id_tipo_movimento "
        strRd = strRd & "FROM dbo.movimenti_targa WITH(NOLOCK) INNER JOIN "
        strRd = strRd & "dbo.tipologia_movimenti WITH(NOLOCK) ON dbo.movimenti_targa.id_tipo_movimento = dbo.tipologia_movimenti.id INNER JOIN "
        strRd = strRd & "dbo.veicoli WITH(NOLOCK) ON dbo.movimenti_targa.id_veicolo = dbo.veicoli.id LEFT OUTER JOIN "
        strRd = strRd & "dbo.stazioni AS stazioni_1 WITH(NOLOCK) ON dbo.movimenti_targa.id_stazione_rientro = stazioni_1.id LEFT OUTER JOIN "
        strRd = strRd & "dbo.stazioni WITH(NOLOCK) ON dbo.movimenti_targa.id_stazione_uscita = dbo.stazioni.id "
        strRd = strRd & "WHERE (dbo.veicoli.targa = N'" & txtTarga.Text & "') "
        'strRd = strRd & "AND (dbo.movimenti_targa.data_uscita <= CONVERT(DATETIME, '2012-08-10 16:38:00', 102)) AND "
        'strRd = strRd & "(dbo.movimenti_targa.data_rientro >= CONVERT(DATETIME, '2012-08-10 16:38:00', 102) OR "
        Dim DataInfr As Date = ControllaDataOra(txtDataInfrazione.Text, txtOraInfrazione.Text)
        If DataInfr = Nothing Then
            Libreria.genUserMsgBox(Page, "Data o ora infrazione non corretta")
            Exit Sub
        End If
        strRd = strRd & "AND (dbo.movimenti_targa.data_uscita <= CONVERT(DATETIME, '" & Year(DataInfr) & "-" & Month(DataInfr)
        strRd = strRd & "-" & Day(DataInfr) & " " & Hour(DataInfr) & ":" & Minute(DataInfr) & ":00', 102)) AND "
        strRd = strRd & "(dbo.movimenti_targa.data_rientro >= CONVERT(DATETIME, '" & Year(DataInfr) & "-" & Month(DataInfr)
        strRd = strRd & "-" & Day(DataInfr) & " " & Hour(DataInfr) & ":" & Minute(DataInfr) & ":00', 102) OR "
        strRd = strRd & "dbo.movimenti_targa.data_rientro IS NULL)"

        'Response.Write(strRd)
        'Response.End()
        'Trace.Write("controlla query ricerca mov   ------------------------> gestione_multe" & strRd)
        MovimVeicoli = GetReader(strRd)

        'If MovimVeicoli.HasRows = True Then
        'End If

        Dim ContaRecord As Integer = 0
        Dim tipoMov As Integer = 0
        Dim NumDoc As Integer = 0
        Dim DataUscita As Date = Nothing
        Dim DataRientro As Date = Nothing
        Dim StazUscita As String = ""
        Dim StazRientro As String = ""

        While MovimVeicoli.Read()
            If ContaRecord > 1 Then Exit While
            ContaRecord = ContaRecord + 1
            tipoMov = MovimVeicoli("id_tipo_movimento")
            NumDoc = MovimVeicoli("num_riferimento")

            If IsDate(MovimVeicoli("data_uscita")) = True Then
                DataUscita = MovimVeicoli("data_uscita")
            End If

            If IsDate(MovimVeicoli("data_rientro")) = True Then
                DataRientro = MovimVeicoli("data_rientro")
            End If

            StazUscita = MovimVeicoli("StazUscita").ToString
            StazRientro = MovimVeicoli("StazRientro").ToString
        End While

        'Trace.Write("--------------------------------------nu record trovati" & ContaRecord)
        If ContaRecord = 1 Then 'se ne trova solo uno
            If tipoMov = 3 Then ' se un contratto
                AzzeraCampiConduc_e_noleggio()
                txtNumDocAltro.Text = ""
                txtDescrizAltro.Text = ""

                txtNumContratto.Text = NumDoc
                txtDaStazioneNolo.Text = StazUscita
                txtAStazioneNolo.Text = StazRientro
                txtDaDataNolo.Text = DataUscita
                txtAdataNolo.Text = DataRientro
                If DataUscita = Nothing Then
                    txtDaOraNolo.Text = ""
                Else
                    txtDaOraNolo.Text = Hour(DataUscita) & ":" & Minute(DataUscita)
                End If

                If DataRientro = Nothing Then
                    txtAOraNolo.Text = ""
                Else
                    txtAOraNolo.Text = Hour(DataRientro) & ":" & Minute(DataRientro)
                End If

                AssegnaConducente(NumDoc)
                SalvaModifiche(False) 'salvo i dati trovati senza dare messaggio (il parametro false utilizzato per non  dare messaggio di conferma di salvataggio dati)
            End If
            visualizza_movimAuto.Visible = False
        ElseIf ContaRecord > 1 Then
            sqlMovAuto.SelectCommand = strRd
            ListViewMovAuto.DataBind()
            visualizza_movimAuto.Visible = True
            Session("SqlSelectMovAuto") = strRd
            'viene memorizz nella sessione in modo che se avviene il postback
            'a causa di cambio pagina della listView, il datasource viene tenuto aggiornato
        Else
            Libreria.genUserMsgBox(Page, "Nessun movimento trovato. Visualizza tutti i movimenti della targa.")
            AzzeraCampiConduc_e_noleggio()
            txtNumDocAltro.Text = ""
            txtDescrizAltro.Text = ""
        End If
        MovimVeicoli.Close()
    End Sub

    Protected Sub btnCercaComuni_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCercaComuni.Click
        If txtEnteComune.Text = "" Then Exit Sub

        Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dim strQuery As String
        strQuery = "SELECT [id],[comune] FROM [comuni_ares] WITH(NOLOCK) WHERE comune LIKE '" & txtEnteComune.Text
        strQuery = strQuery & "%'  ORDER BY [comune]"
        Dim cmd As New SqlCommand(strQuery, conn)
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet()
        da.Fill(ds)

        ListComuni.DataSource = ds
        ListComuni.DataBind()
        ListComuni.Visible = True
        ListComuni.Focus()
    End Sub

    Protected Sub ListComuni_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListComuni.SelectedIndexChanged
        Dim CapProvReader As SqlDataReader
        Dim stringaReader = "Select comune, cap_comune, provincia from comuni_ares WITH(NOLOCK) where id=" & ListComuni.SelectedValue.ToString
        CapProvReader = GetReader(stringaReader)

        While CapProvReader.Read()
            txtCap.Text = CapProvReader("cap_comune").ToString
            txtProv.Text = CapProvReader("provincia").ToString
            txtEnteComune.Text = CapProvReader("comune").ToString
        End While

        CapProvReader.Close()
        ListComuni.Items.Clear()
        ListComuni.Visible = False
        txtVerbale.Focus()
    End Sub
    Private Function ControllaDataOra(ByVal data As String, ByVal orario As String) As DateTime
        If IsDate(data) = True And Len(orario) = 5 Then
            Dim ora As Integer = CInt(Left(orario, 2))
            Dim minuti As Integer = CInt(Right(orario, 2))
            If (ora >= 0 And ora < 24) And (minuti >= 0 And minuti < 60) Then
                Dim dataOra As Date = DateAdd(DateInterval.Hour, ora, CDate(data)) 'prima aggiungo l'ora alla data
                Return DateAdd(DateInterval.Minute, minuti, dataOra)
            Else
                Return Nothing
            End If
        Else
            Return Nothing
        End If
    End Function

    Protected Sub AnnullaListMovAuto()
        Session("SqlSelectMovAuto") = ""
        ListViewMovAuto.Items.Clear()
        visualizza_movimAuto.Visible = False
    End Sub

    Protected Function getDriver(ByVal num_riferimento As String) As String
        Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dim strQuery As String
        strQuery = "SELECT drivers.cognome + ' ' + drivers.nome FROM drivers WITH(NOLOCK) INNER JOIN trasferimenti WITH(NOLOCK) ON drivers.id=trasferimenti.id_conducente " &
            "INNER JOIN movimenti_targa WITH(NOLOCK) ON trasferimenti.num_trasferimento=movimenti_targa.num_riferimento WHERE movimenti_targa.num_riferimento='" & num_riferimento & "'"
        conn.Open()
        Dim cmd As New SqlCommand(strQuery, conn)

        getDriver = cmd.ExecuteScalar & ""

        cmd.Dispose()
        cmd = Nothing
        conn.Close()
        conn.Dispose()
        conn = Nothing
    End Function

    Protected Sub ListViewMovAuto_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListViewMovAuto.ItemCommand
        'sezione e.commandName=SelezMovAuto -----------------------------------------------       
        If e.CommandName = "SelezMovAuto" Then
            Dim TipoMovim As Label = e.Item.FindControl("lblDescrzMov")

            If TipoMovim.ToolTip = 3 Then 'controllo se movim di tipo contratto (ho memorizzato il tipo nel ToolTip: vedi template ListView)

                AzzeraCampiConduc_e_noleggio()
                txtNumDocAltro.Text = ""
                txtDescrizAltro.Text = ""

                Dim NumeroContratto As ImageButton = e.Item.FindControl("NumRiferimento")
                txtNumContratto.Text = NumeroContratto.AlternateText

                Dim StazUscita As Label = e.Item.FindControl("lblStazUscita")
                txtDaStazioneNolo.Text = StazUscita.Text

                Dim StazRientro As Label = e.Item.FindControl("lblStazRientro")
                txtAStazioneNolo.Text = StazRientro.Text

                Dim DataUscita As Label = e.Item.FindControl("lblDataUscita")
                If DataUscita.Text <> "" Then
                    txtDaDataNolo.Text = CDate(DataUscita.Text)
                    txtDaOraNolo.Text = Hour(CDate(DataUscita.Text)) & ":" & Minute(CDate(DataUscita.Text))
                End If

                Dim DataRientro As Label = e.Item.FindControl("lblDataRientro")
                If DataRientro.Text <> "" Then
                    txtAdataNolo.Text = CDate(DataRientro.Text)
                    txtAOraNolo.Text = Hour(CDate(DataRientro.Text)) & ":" & Minute(CDate(DataRientro.Text))
                End If

                AssegnaCartaCredito(NumeroContratto.AlternateText) 'imposta il numero e la scadenza della carta di cred
                AssegnaConducente(NumeroContratto.AlternateText) 'imposta i dati del conducente

            Else 'negli altri casi che non sono contratti di noleggio        

                Dim id_movim_auto As Label = e.Item.FindControl("lblIdMovAuto")
                Dim MovimautoReader As SqlDataReader
                Dim stringaReader = "SELECT dbo.movimenti_targa.id, dbo.movimenti_targa.num_riferimento, movimenti_targa.id_tipo_movimento, "
                stringaReader = stringaReader & "dbo.movimenti_targa.data_uscita, dbo.tipologia_movimenti.descrzione, "
                stringaReader = stringaReader & "dbo.operatori.nome, dbo.operatori.cognome "
                stringaReader = stringaReader & "FROM dbo.movimenti_targa INNER JOIN "
                stringaReader = stringaReader & "dbo.tipologia_movimenti ON dbo.movimenti_targa.id_tipo_movimento = "
                stringaReader = stringaReader & "dbo.tipologia_movimenti.id LEFT OUTER JOIN "
                stringaReader = stringaReader & "dbo.operatori ON dbo.movimenti_targa.id_operatore = dbo.operatori.id "
                stringaReader = stringaReader & "WHERE (dbo.movimenti_targa.id = " & id_movim_auto.Text & ")"

                MovimautoReader = GetReader(stringaReader)

                AzzeraCampiConduc_e_noleggio()
                txtNumDocAltro.Text = ""
                txtDescrizAltro.Text = ""

                While MovimautoReader.Read()


                    If MovimautoReader("id_tipo_movimento") <> Costanti.idMovimentoInterno Then
                        txtAltroResponsabile.Text = MovimautoReader("cognome").ToString & " " & MovimautoReader("nome").ToString
                    Else
                        txtAltroResponsabile.Text = getDriver(MovimautoReader("num_riferimento"))
                    End If

                    txtNumDocAltro.Text = MovimautoReader("num_riferimento").ToString
                    txtDataDocAltro.Text = MovimautoReader("data_uscita").ToString
                    txtDescrizAltro.Text = MovimautoReader("descrzione").ToString
                End While

                MovimautoReader.Close()
            End If
            visualizza_movimAuto.Visible = False
            SalvaModifiche(False) 'salvo i dati trovati senza dare messaggio (il parametro false utilizzato per non  dare messaggio di conferma di salvataggio dati)
        End If

        'sezione e.commandName=VisualizzaDocum -----------------------------------------------
        If e.CommandName = "VisualizzaDocum" Then

            Dim TipoMovim As Label = e.Item.FindControl("lblDescrzMov")
            If TipoMovim.ToolTip = 3 Then 'controllo se movim di tipo contratto (ho memorizzato il tipo nel ToolTip: vedi template ListView)
                Dim NumeroContratto As ImageButton = e.Item.FindControl("NumRiferimento")

                Session("carica_contratto") = GetIdContratto(NumeroContratto.AlternateText) 'mi procura l'id del contratto che è diverso dal num contratto
                'infatti nella tabella contratti ci possono essese diversi record con num_contratto uguali ma solo uno sara valido e cioè con il campo "attivo=true"

                If Session("carica_contratto") = "0" Then
                    Libreria.genUserMsgBox(Page, "Contratto inesistente")
                Else
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('contratti.aspx', 'mywindow', 'location=1,status=1,scrollbars=1, width=1100,height=800')", True)
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub btnRicercaTuttiMov_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRicercaTuttiMov.Click
        Dim strRd As String = "SELECT dbo.movimenti_targa.id, dbo.tipologia_movimenti.descrzione, dbo.movimenti_targa.num_riferimento, "
        strRd = strRd & "dbo.veicoli.targa, dbo.stazioni.nome_stazione AS StazUscita, stazioni_1.nome_stazione AS StazRientro, "
        strRd = strRd & "dbo.movimenti_targa.data_uscita, dbo.movimenti_targa.data_rientro, dbo.movimenti_targa.id_tipo_movimento "
        strRd = strRd & "FROM dbo.movimenti_targa WITH(NOLOCK) INNER JOIN "
        strRd = strRd & "dbo.tipologia_movimenti WITH(NOLOCK) ON dbo.movimenti_targa.id_tipo_movimento = dbo.tipologia_movimenti.id INNER JOIN "
        strRd = strRd & "dbo.veicoli WITH(NOLOCK) ON dbo.movimenti_targa.id_veicolo = dbo.veicoli.id LEFT OUTER JOIN "
        strRd = strRd & "dbo.stazioni AS stazioni_1 WITH(NOLOCK) ON dbo.movimenti_targa.id_stazione_rientro = stazioni_1.id LEFT OUTER JOIN "
        strRd = strRd & "dbo.stazioni WITH(NOLOCK) ON dbo.movimenti_targa.id_stazione_uscita = dbo.stazioni.id "
        strRd = strRd & "WHERE (dbo.veicoli.targa = N'" & txtTarga.Text & "') "
        strRd = strRd & "ORDER BY dbo.movimenti_targa.data_uscita DESC"

        sqlMovAuto.SelectCommand = strRd
        ListViewMovAuto.DataBind()
        visualizza_movimAuto.Visible = True
        Session("SqlSelectMovAuto") = strRd
        'viene memorizz nella sessione in modo che se avviene il postback
        'a causa di cambio pagina della listView, il datasource viene tenuto aggiornato

    End Sub
    Protected Sub AssegnaCartaCredito(ByVal contratto As Integer)
        Dim CartaCreditoReader As SqlDataReader
        Dim strRd As String = "SELECT Nr_Contratto, Titolo, Intestatario, Scadenza FROM dbo.PAGAMENTI_EXTRA WITH(NOLOCK)"
        strRd = strRd & "WHERE (Nr_Contratto = " & contratto & ")"
        CartaCreditoReader = GetReader(strRd)

        'Trace.Write("------------------------------" & strRd)
        While CartaCreditoReader.Read()
            txtNumeroCartaCred.Text = CartaCreditoReader("Titolo") & ""
            Dim ScadCC As String = CartaCreditoReader("Scadenza") & ""
            txtScadCartaCred.Text = Left(ScadCC, 2) & "." & Right(ScadCC, 2)
            Exit While 'basta solo la prima occorrenza
        End While

        CartaCreditoReader.Close()
    End Sub
    Protected Sub AssegnaConducente(ByVal contratto As Integer) 'esiste anche la sub CaricaDatiConducente con id conducente passato come parametro
        Dim ConducenteReader As SqlDataReader
        Dim strRd As String = ""
        strRd = strRd & "SELECT dbo.CONDUCENTI.ID_CONDUCENTE, dbo.CONDUCENTI.Nominativo, dbo.CONDUCENTI.COGNOME,  "
        strRd = strRd & "dbo.CONDUCENTI.Indirizzo, dbo.CONDUCENTI.City, dbo.CONDUCENTI.PROVINCIA, dbo.CONDUCENTI.Cap, "
        strRd = strRd & "dbo.CONDUCENTI.Data_Nascita, dbo.CONDUCENTI.Patente, dbo.comuni_ares.comune As luogo_nascita, dbo.CONDUCENTI.Luogo_Nascita As luogo_nascita_old, "
        strRd = strRd & "dbo.CONDUCENTI.Tipo_Patente, dbo.CONDUCENTI.Scadenza_Patente, dbo.CONDUCENTI.RILASCIATA_IL, "
        strRd = strRd & "dbo.CONDUCENTI.LUOGO_EMISSIONE, dbo.CONDUCENTI.EMAIL, dbo.nazioni.NAZIONE, dbo.DITTE.ID_Cliente, dbo.CONDUCENTI.comune_nascita_ee "
        strRd = strRd & "FROM dbo.CONDUCENTI WITH (NOLOCK) INNER JOIN dbo.contratti WITH (NOLOCK) ON "
        strRd = strRd & "dbo.CONDUCENTI.ID_CONDUCENTE = dbo.contratti.id_primo_conducente LEFT OUTER JOIN "
        strRd = strRd & "dbo.DITTE WITH (NOLOCK) ON dbo.contratti.id_cliente = dbo.DITTE.Id_Ditta LEFT OUTER JOIN "
        strRd = strRd & "dbo.nazioni WITH (NOLOCK) ON dbo.CONDUCENTI.NAZIONE = dbo.nazioni.ID_NAZIONE "
        strRd = strRd & "LEFT JOIN dbo.comuni_ares WITH(NOLOCK) ON dbo.CONDUCENTI.id_comune_ares_nascita=comuni_ares.id "
        strRd = strRd & "WHERE (dbo.contratti.num_contratto = " & contratto & ") AND (dbo.contratti.attivo=1)"

        ConducenteReader = GetReader(strRd)
        'Trace.Write("------------------------------" & strRd)
        While ConducenteReader.Read()
            txtConducente.Text = ConducenteReader("Nominativo") & ""
            txtDataNascitaCond.Text = ConducenteReader("Data_Nascita") & ""

            If (ConducenteReader("Luogo_Nascita") & "") <> "" Then
                txtLuogoNascitaCond.Text = ConducenteReader("Luogo_Nascita") & ""
            ElseIf (ConducenteReader("comune_nascita_ee") & "") <> "" Then
                txtLuogoNascitaCond.Text = ConducenteReader("comune_nascita_ee") & ""
            Else
                txtLuogoNascitaCond.Text = ConducenteReader("luogo_nascita_old") & ""
            End If

            txtIndirizzoCond.Text = ConducenteReader("Indirizzo") & ""
            txtCapCond.Text = ConducenteReader("Cap") & ""
            txtComuneCond.Text = ConducenteReader("City") & ""
            txtProvCond.Text = ConducenteReader("PROVINCIA") & ""
            txtNazioneCond.Text = ConducenteReader("NAZIONE") & ""
            txtPatente.Text = ConducenteReader("Patente") & ""
            txtDataRilascioPatente.Text = ConducenteReader("RILASCIATA_IL") & ""
            txtLuogoRilascioPatente.Text = ConducenteReader("LUOGO_EMISSIONE") & ""
            txtScadPatente.Text = ConducenteReader("Scadenza_Patente") & ""
            txtCatPatente.Text = ConducenteReader("Tipo_Patente") & ""
            txtIDCond.Text = ConducenteReader("ID_CONDUCENTE") & ""
            txtDestinatarioMail.Text = ConducenteReader("EMAIL") & ""
            txtCodCliente.Text = ConducenteReader("ID_Cliente") & ""
            Exit While 'basta la prima occorrenza (in verità dovrebbe essere unica al 100%)
        End While

        ConducenteReader.Close()
    End Sub
    Protected Sub CaricaDatiConducente(ByVal id_Conduc As Integer) 'esiste anche la sub AssegnaConducente con id contratto passato come parametro
        Dim ConducenteReader As SqlDataReader
        Dim strRd As String = ""
        'OLD
        strRd = "Select dbo.CONDUCENTI.ID_CONDUCENTE, dbo.CONDUCENTI.ID_Cliente, dbo.CONDUCENTI.Nominativo, dbo.CONDUCENTI.Indirizzo, "
        strRd += "dbo.CONDUCENTI.City, dbo.CONDUCENTI.PROVINCIA, dbo.CONDUCENTI.Cap, dbo.CONDUCENTI.Data_Nascita, "
        strRd += "dbo.CONDUCENTI.Luogo_Nascita, dbo.CONDUCENTI.Patente, dbo.CONDUCENTI.Tipo_Patente, "
        strRd += "dbo.CONDUCENTI.Scadenza_Patente, dbo.CONDUCENTI.RILASCIATA_IL, dbo.CONDUCENTI.LUOGO_EMISSIONE, dbo.CONDUCENTI.EMAIL, "
        strRd += "dbo.nazioni.NAZIONE FROM dbo.CONDUCENTI INNER JOIN dbo.nazioni On "
        strRd += "dbo.CONDUCENTI.NAZIONE = dbo.nazioni.ID_NAZIONE "

        'NEW
        strRd = "Select CONDUCENTI.ID_CONDUCENTE, CONDUCENTI.ID_Cliente, CONDUCENTI.Nominativo, CONDUCENTI.Indirizzo, CONDUCENTI.City, CONDUCENTI.PROVINCIA, CONDUCENTI.Cap, CONDUCENTI.Data_Nascita, "
        strRd += "CONDUCENTI.Luogo_Nascita, CONDUCENTI.Patente, CONDUCENTI.Tipo_Patente, CONDUCENTI.Scadenza_Patente, CONDUCENTI.RILASCIATA_IL, CONDUCENTI.LUOGO_EMISSIONE, CONDUCENTI.EMAIL,"
        strRd += "nazioni.NAZIONE As nazione, comuni_ares.comune As luogo_nascita1, CONDUCENTI.comune_nascita_ee As luogo_nascita2 "
        strRd += "From CONDUCENTI INNER Join nazioni On CONDUCENTI.NAZIONE = nazioni.ID_NAZIONE LEFT OUTER Join comuni_ares On CONDUCENTI.id_comune_ares_nascita = comuni_ares.id "

        strRd += "WHERE(dbo.CONDUCENTI.ID_CONDUCENTE = " & id_Conduc & ")"

        ConducenteReader = GetReader(strRd)

        'Trace.Write("------------------------------" & strRd)
        While ConducenteReader.Read()
            txtConducente.Text = ConducenteReader("Nominativo") & ""
            txtDataNascitaCond.Text = ConducenteReader("Data_Nascita") & ""

            ''verifica quale valore prendere a seconda di cosa è stato inserito al momento del contratto 14.01.2021
            txtLuogoNascitaCond.Text = ConducenteReader("Luogo_Nascita") & ""
            If txtLuogoNascitaCond.Text = "" Then
                txtLuogoNascitaCond.Text = ConducenteReader("Luogo_Nascita1") & ""
            End If
            If txtLuogoNascitaCond.Text = "" Then
                txtLuogoNascitaCond.Text = ConducenteReader("Luogo_Nascita2") & ""
            End If

            txtIndirizzoCond.Text = ConducenteReader("Indirizzo") & ""
            txtCapCond.Text = ConducenteReader("Cap") & ""
            txtComuneCond.Text = ConducenteReader("City") & ""
            txtProvCond.Text = ConducenteReader("PROVINCIA") & ""
            txtNazioneCond.Text = ConducenteReader("NAZIONE") & ""
            txtPatente.Text = ConducenteReader("Patente") & ""
            txtDataRilascioPatente.Text = ConducenteReader("RILASCIATA_IL") & ""
            txtLuogoRilascioPatente.Text = ConducenteReader("LUOGO_EMISSIONE") & ""
            txtScadPatente.Text = ConducenteReader("Scadenza_Patente") & ""
            txtCatPatente.Text = ConducenteReader("Tipo_Patente") & ""
            txtIDCond.Text = ConducenteReader("ID_CONDUCENTE") & ""
            txtDestinatarioMail.Text = ConducenteReader("EMAIL") & ""
            Exit While 'basta la prima occorrenza (in verità dovrebbe essere unica al 100%)
        End While

        ConducenteReader.Close()
    End Sub

    Protected Sub GetDatiAttualiConducente(ByVal id_Conduc As Integer) 'esiste anche la sub AssegnaConducente con id contratto passato come parametro
        'questa funzione è quasi uguale a CaricaDatiConducente. 
        'La diff è che questa legge il codice cliente presente nella tabella conducenti e non quello memorizzato nella tabella multe
        Dim ConducenteReader As SqlDataReader
        Dim strRd As String = "Select dbo.CONDUCENTI.ID_CONDUCENTE, dbo.CONDUCENTI.ID_Cliente, dbo.CONDUCENTI.Nominativo, dbo.CONDUCENTI.Indirizzo, "
        strRd = strRd & "dbo.CONDUCENTI.City, dbo.CONDUCENTI.PROVINCIA, dbo.CONDUCENTI.Cap, dbo.CONDUCENTI.Data_Nascita, "
        strRd = strRd & "dbo.CONDUCENTI.Luogo_Nascita, dbo.CONDUCENTI.Patente, dbo.CONDUCENTI.Tipo_Patente, "
        strRd = strRd & "dbo.CONDUCENTI.Scadenza_Patente, dbo.CONDUCENTI.RILASCIATA_IL, dbo.CONDUCENTI.LUOGO_EMISSIONE, dbo.CONDUCENTI.EMAIL, "
        strRd = strRd & "dbo.nazioni.NAZIONE FROM dbo.CONDUCENTI INNER JOIN dbo.nazioni On "
        strRd = strRd & "dbo.CONDUCENTI.NAZIONE = dbo.nazioni.ID_NAZIONE "
        strRd = strRd & "WHERE(dbo.CONDUCENTI.ID_CONDUCENTE = " & id_Conduc & ")"
        ConducenteReader = GetReader(strRd)

        'Trace.Write("------------------------------" & strRd)
        While ConducenteReader.Read()
            txtConducente.Text = ConducenteReader("Nominativo")
            txtDataNascitaCond.Text = ConducenteReader("Data_Nascita")
            txtLuogoNascitaCond.Text = ConducenteReader("Luogo_Nascita")
            txtIndirizzoCond.Text = ConducenteReader("Indirizzo")
            txtCapCond.Text = ConducenteReader("Cap")
            txtComuneCond.Text = ConducenteReader("City")
            txtProvCond.Text = ConducenteReader("PROVINCIA")
            txtNazioneCond.Text = ConducenteReader("NAZIONE")
            txtPatente.Text = ConducenteReader("Patente")
            txtDataRilascioPatente.Text = ConducenteReader("RILASCIATA_IL")
            txtLuogoRilascioPatente.Text = ConducenteReader("LUOGO_EMISSIONE")
            txtScadPatente.Text = ConducenteReader("Scadenza_Patente")
            txtCatPatente.Text = ConducenteReader("Tipo_Patente")
            txtIDCond.Text = ConducenteReader("ID_CONDUCENTE")
            txtDestinatarioMail.Text = ConducenteReader("EMAIL") & ""
            txtCodCliente.Text = ConducenteReader("ID_Cliente") & ""
            Exit While 'basta la prima occorrenza (in verità dovrebbe essere unica al 100%)
        End While

        ConducenteReader.Close()
    End Sub


    Protected Sub CaricaDatiMemorizzati(ByVal nMulta As Integer)
        Dim DataUscita As Date = Nothing
        Dim DataRientro As Date = Nothing

        AzzeraCampi()
        Dim myMulta As Multe = New Multe
        myMulta = Multe.getFullRecordsMulte(nMulta)

        txtAnno.Text = myMulta.Anno
        txtProt.Text = myMulta.Prot
        txtDataInser.Text = myMulta.DataInserimento.Date
        DropProvenienza.SelectedValue = myMulta.ProvenienzaID
        If myMulta.StatoAperto = True Then
            DropStato.SelectedIndex = 0
        Else
            DropStato.SelectedIndex = 1
        End If

        lblID.Text = myMulta.ID
        DropEnti.SelectedValue = myMulta.EnteID
        txtEnteIndir.Text = myMulta.EnteIndirizzo
        txtEnteComune.Text = myMulta.EnteComune
        txtCap.Text = myMulta.EnteCap
        txtProv.Text = myMulta.EnteProv
        txtVerbale.Text = myMulta.NumVerbale
        txtNotifica.Text = myMulta.DataNotifica
        DropArtCDS.SelectedValue = myMulta.ArticoloCDS
        txtImporto.Text = myMulta.MultaImporto
        txtTarga.Text = myMulta.Targa
        txtDataInfrazione.Text = myMulta.DataInfrazione

        If myMulta.DataInfrazione = Nothing Then
            txtOraInfrazione.Text = ""
        Else
            txtOraInfrazione.Text = Hour(myMulta.DataInfrazione) & ":" & Minute(myMulta.DataInfrazione)
        End If


        DropCasistiche.SelectedValue = myMulta.CasisticaID
        If myMulta.CasisticaID > 0 Then
            AggiornaCasistiche(myMulta.CasisticaID)
        End If

        CaricaDatiConducente(myMulta.IDConducente)
        txtCodCliente.Text = myMulta.CodCliente
        txtNumContratto.Text = myMulta.ContrattoNolo
        txtDaStazioneNolo.Text = myMulta.StazioneInizio
        txtAStazioneNolo.Text = myMulta.StazioneFine
        DataUscita = myMulta.DataInizioNolo
        DataRientro = myMulta.DataFineNolo
        txtDaDataNolo.Text = DataUscita
        txtAdataNolo.Text = DataRientro

        If DataUscita = Nothing Then
            txtDaOraNolo.Text = ""
        Else
            txtDaOraNolo.Text = Hour(DataUscita) & ":" & Minute(DataUscita)
        End If

        If DataRientro = Nothing Then
            txtAOraNolo.Text = ""
        Else
            txtAOraNolo.Text = Hour(DataRientro) & ":" & Minute(DataRientro)
        End If
        txtNumeroCartaCred.Text = myMulta.NumCartaCredito
        txtScadCartaCred.Text = myMulta.ScadCartaCredito
        txtAcquirente.Text = myMulta.AcquirenteMezzo
        txtDataVendita.Text = myMulta.DataVenditaMezzo
        txtNumFattVendita.Text = myMulta.NumFattVendMezzo
        txtCodClienteVendita.Text = myMulta.CodClienteVendMezzo
        txtAltroResponsabile.Text = myMulta.AltroResponsMulta
        txtNumDocAltro.Text = myMulta.NumDocAltroCaso
        txtDataDocAltro.Text = myMulta.DataDocAltroCaso
        txtDescrizAltro.Text = myMulta.DescrizAltroCaso
        txtNote.Text = myMulta.Note
        chkRicorsoYesNo.Checked = myMulta.RicorsoYesNo
        txtNumRaccomandata.Text = myMulta.RicorsoNumRacc
        txtDataRacc.Text = myMulta.RicorsoDataRacc
        txtDataInoltroFax.Text = myMulta.RicorsoDataFax
        chkComunizYesNo.Checked = myMulta.ComunicazClienteYesNo
        txtDataInoltroMail.Text = myMulta.DataMailCliente
        chkIncassatoYesNo.Checked = myMulta.IncassatoYesNo
        txtTentativiInc.Text = myMulta.NumTentativiIncassi
        DropTipoMancInc.SelectedValue = myMulta.MotivoMancInc
        chkRimborsatoYesNo.Checked = myMulta.RimborsatoYesNo
        txtRimborsatoData.Text = myMulta.RimborsatoData
        txtRimborsatoImporto.Text = myMulta.RimborsatoImporto
        ChkPagatoYesNo.Checked = myMulta.PagatoYesNo
        txtPagatoData.Text = myMulta.PagatoData
        txtPagatoImporto.Text = myMulta.PagatoImporto
        chkFatturatoYesNo.Checked = myMulta.FatturatoYesNo
        DropLocatori.SelectedValue = myMulta.LocatoreId
        txtNumFattTerziLoc.Text = myMulta.LocatoreNumFatt
        txtDataFattTerziLoc.Text = myMulta.LocatoreDataFatt

        'campi vuoti della scheda protocollo salvo 24.11.2022
        'e riempiti con valori dei dati dell'ente se vuoti
        txt_prot_enti_tel.Text = myMulta.EnteTel
        txt_prot_enti_email.Text = myMulta.EnteEmail
        txt_prot_enti_emailpec.Text = myMulta.EnteEmailPec
        txt_prot_enti_notes.Text = myMulta.EnteProtNotes
        '@ end aggiunta Salvo 24.11.2022

        'aggiunto 20.12.2022 salvo
        txt_enti_notes.Text = myMulta.EnteNotes





    End Sub

    Protected Sub btnSelezionaConducente_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSelezionaConducente.Click
        'UpdatePanel1.Visible = False
        div_steps.Visible = False
        anagrafica_conducenti1.Visible = True
    End Sub

    Protected Sub btnElimina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnElimina.Click
        If lblID.Text <> "" Then
            If VerificaEsistenzaIncassiMulte(lblID.Text) = True Then
                Libreria.genUserMsgBox(Page, "Impossibile eliminare la multa: ci sono incassi collegati.")
                Exit Sub
            End If
            If VerificaEsistenzaFattureMulte(lblID.Text) = True Then
                Libreria.genUserMsgBox(Page, "Impossibile eliminare la multa: ci sono fatture collegate.")
                Exit Sub
            End If

            Dim StrElim As String = "DELETE FROM multe WHERE ID=" & lblID.Text
            Trace.Write("---------------------query di eliminaz" & StrElim)
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(StrElim, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                End Using
            End Using
            'Libreria.genUserMsgBox(Page, "Multa eliminata correttamente")
            btnNuovo_Click(sender, e)
        End If
    End Sub

    Protected Sub btnMemorizzaAlleg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMemorizzaAlleg.Click
        'If FileUploadAllegati.HasFile Then
        'Try
        'FileUploadAllegati.SaveAs(Server.MapPath("gestione_multe\Allegati\2012") & "\" & FileUploadAllegati.FileName)
        'Catch ex As Exception
        'lblMessUploadFile.Text = "Errore: " & ex.Message.ToString()
        'End Try
        'End If

        Dim Messaggio As String = ""
        If FileUploadAllegati.HasFile Then
            Dim estensione As String = LCase(Right(FileUploadAllegati.FileName, 4))
            If estensione = ".jpg" Or estensione = ".png" Or estensione = ".pdf" Then
                'Trace.Write("FileUpload1.PostedFile.ContentLength:" & FileUploadAllegati.PostedFile.ContentLength)
                If FileUploadAllegati.PostedFile.ContentLength <= 2000000 Then
                    Dim filePath As String
                    filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "gestione_multe\Allegati\" _
                                & txtAnno.Text & "\"
                    Trace.Write("--------------path: " & filePath)
                    If Directory.Exists(filePath) = False Then
                        'creo la directory
                        Directory.CreateDirectory(filePath)
                    End If

                    Dim NomeFile As String
                    'NomeFile = System.Guid.NewGuid().ToString
                    Dim dataTemp As Date = Now

                    '#Aggiunto Salvo 23.03.2023
                    Dim Sigla_Allegato As String = funzioni_comuni_new.getSiglaAllegato(DropTipoAllegato.SelectedValue, "multe_TipoAllegato")
                    '@end salvo
                    Dim ttarga As String = txtTarga.Text
                    If ttarga <> "" Then
                        ttarga = txtTarga.Text.Replace(" ", "")
                    End If

                    Dim txt As TextBox = TryCast(Page.FindControl("ctl00").FindControl("ContentPlaceHolder1").FindControl("txtProt"), TextBox)
                    Dim nProt As String = txt.Text



                    NomeFile = Year(dataTemp) & Month(dataTemp) & Day(dataTemp) & Hour(dataTemp) & Minute(dataTemp) & Second(dataTemp)
                    Dim fileNameBig As String = lblID.Text.PadLeft(6, "0"c) & "_" & txtAnno.Text & "_" & DropTipoAllegato.SelectedValue.ToString.PadLeft(2, "0"c) & "_"
                    NomeFile = Sigla_Allegato & "_" & txtAnno.Text & "_" & nProt.PadLeft(6, "0"c) & estensione
                    fileNameBig = NomeFile
                    'fileNameBig = fileNameBig & Replace((DropTipoAllegato.SelectedItem.ToString), " ", "-") & "_" & NomeFile & estensione






                    'se presente lo rinomina salvo 07.04.2023
                    For x = 1 To 30
                        If File.Exists(filePath & fileNameBig) Then
                            Dim xNum As String
                            If x < 10 Then
                                xNum = "0" & x.ToString
                            Else
                                xNum = x.ToString
                            End If
                            NomeFile = Sigla_Allegato & "_" & txtAnno.Text & "_" & nProt.PadLeft(6, "0"c) & "_" & xNum & estensione
                            fileNameBig = NomeFile
                            'nome_file = siglaAllegato & "_" & id_stazione & "_" & data_allegato & "_" & xNum & "." & estensione
                        Else
                            Exit For
                        End If
                    Next


                    FileUploadAllegati.SaveAs(filePath & fileNameBig)

                    'qui il codice per registrare il percorso del file nell'apposita tabella
                    Dim my_allegatiMulte As AllegatiMulte = New AllegatiMulte
                        With my_allegatiMulte
                            .DataCreazione = Now
                            .IdTipoDocumento = DropTipoAllegato.SelectedValue
                            .NomeFile = fileNameBig
                            .PercorsoFile = filePath
                            .IdMulta = CInt(lblID.Text)
                            Trace.Write("-------------- valore lblID: " & lblID.Text)
                            my_allegatiMulte.InsertAllegatoMulta()
                        End With

                        AggiornaListAllegatiMulte(my_allegatiMulte.IdMulta)
                        DropTipoAllegato.SelectedValue = 0
                        Messaggio = "Documento salvato correttamente"

                    Else
                    Messaggio = "Il file non può essere caricato perché supera 2MB!"
                End If
            Else
                Messaggio = "L'estensione dell'immagine deve essere con estensione (jpg, png, pdf)"
            End If
        Else
            Messaggio = "Selezionare un file da salvare."
        End If

        If Messaggio <> "" Then
            Libreria.genUserMsgBox(Page, Messaggio)
        End If

    End Sub

    Protected Sub btnRilevaAllegAuto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRilevaAllegAuto.Click
        'Dim percorso As Directory
        Dim nomefile As String = ""
        Dim percorso As String = "gestione_multe\Allegati\" & txtAnno.Text
        Dim criterioRic As String = lblID.Text
        criterioRic = criterioRic.PadLeft(6, "0"c) & "*"
        For Each _file In Directory.GetFiles(Server.MapPath(percorso), criterioRic)
            nomefile = _file.ToString
            Dim estensione As String = LCase(Right(nomefile, 4))
            Trace.Write("------------------- estensione: " & estensione)
            If estensione = ".pdf" Or estensione = ".jpg" Or estensione = ".png" Then
                Dim my_allegatiMulte As AllegatiMulte = New AllegatiMulte
                Dim cartelle() As String = Split(nomefile, "\")
                Dim SoloFile As String = cartelle(cartelle.Length - 1) ' mi restituisce solo il file con l'estensione
                Dim soloPercorso As String = ""

                If my_allegatiMulte.VerificaFileSeImportatoPrima(SoloFile) = False Then 'se non esite ne db allora caricalo
                    With my_allegatiMulte
                        .DataCreazione = Now
                        .IdTipoDocumento = 7 'tipo NON DEFINITO infatto non è possibile in questa fase sapere a priori il tipo del doc.
                        'a meno che chi fa le scansioni utilizzi un formato preciso dove viene indicato anche il tipo di documento.
                        'In questo caso modificherò il codice in modo da attribuire il tipo corretto
                        For a = 0 To cartelle.Length - 2 'mi serve il percosro senza il file finale
                            soloPercorso = soloPercorso & cartelle(a) & "\"
                        Next
                        .NomeFile = SoloFile
                        .PercorsoFile = soloPercorso
                        .IdMulta = CInt(lblID.Text)
                        my_allegatiMulte.InsertAllegatoMulta()
                    End With
                    my_allegatiMulte = Nothing
                End If
            End If
        Next
        AggiornaListAllegatiMulte(CInt(lblID.Text))
    End Sub

    Protected Sub ListViewAllegati_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListViewAllegati.ItemCommand
        If e.CommandName = "SelezionaAllegato" Then
            Dim NomeFile As Label = e.Item.FindControl("lblNomeFile")
            Dim PercFile As Label = e.Item.FindControl("lblPercorsoFile")
            Dim posizione As Integer = PercFile.Text.IndexOf("gestione_multe")
            Dim newPercorso As String = Mid(Replace(PercFile.Text, "\", "/"), posizione + 1) 'restituisce una stringa a partire dalla posizione specificata dopo averla convertita
            newPercorso = newPercorso & NomeFile.Text 'aggiungo il nome del file al precorso

            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('" & newPercorso & "','')", True)
                End If
            End If
        End If
        If e.CommandName = "EliminaAllegato" Then
            Dim IdAllegatoDaEliminare As Label = e.Item.FindControl("lblIdAllegato")
            Dim my_allegatiMulte As AllegatiMulte = New AllegatiMulte
            my_allegatiMulte.id = IdAllegatoDaEliminare.Text
            my_allegatiMulte.DeleteAllegatoMulta()
            AggiornaListAllegatiMulte(CInt(lblID.Text))
        End If
    End Sub

    Protected Sub AggiornaListAllegatiMulte(ByVal id_multa As Integer)
        'aggiornato salvo 23.03.2023
        Try
            Dim sqlstr As String = "Select multe_Allegati.Id, RTRIM(multe_TipoAllegato.TipoAllegato) as TipoAllegato, multe_Allegati.NomeFile, multe_Allegati.PercorsoFile, " &
                "operatori.cognome + ' ' + operatori.nome AS Operatore, dataCreazione From multe_Allegati WITH (NOLOCK) INNER Join " &
                   "multe_TipoAllegato WITH (NOLOCK) ON multe_Allegati.IdTipoDocumento = multe_TipoAllegato.Id LEFT OUTER Join " &
                   "operatori On multe_Allegati.id_operatore = operatori.id " &
                    "Where (multe_Allegati.IdMulta = " & id_multa & ")"

            sqlAllegati.SelectCommand = sqlstr

            '"SELECT dbo.multe_Allegati.Id, dbo.multe_TipoAllegato.TipoAllegato, " &
            '    "dbo.multe_Allegati.NomeFile, dbo.multe_Allegati.PercorsoFile, dbo.multe_Allegati.IdMulta " &
            '    "FROM dbo.multe_Allegati WITH (NOLOCK) " &
            '    "INNER JOIN dbo.multe_TipoAllegato WITH (NOLOCK) ON dbo.multe_Allegati.IdTipoDocumento = " &
            '    "dbo.multe_TipoAllegato.Id WHERE dbo.multe_Allegati.IdMulta=" & id_multa

            ListViewAllegati.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnAggiornaListaAllegati_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAggiornaListaAllegati.Click
        AggiornaListAllegatiMulte(Int(lblID.Text))
    End Sub

    Protected Sub btnIncassi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIncassi.Click
        tab_incassi.Visible = True
        'UpdatePanel1.Visible = False
        div_steps.Visible = False

        Dim DatiIncasso As New tabelle_pos_Scambio_Importo.DatiInizializzazione()
        DatiIncasso.IDStazione = Request.Cookies("SicilyRentCar")("stazione")
        DatiIncasso.NumeroDocumento = lblID.Text
        DatiIncasso.TipoDocumento = tabelle_pos_Scambio_Importo.TipoDocumento.Multe
        DatiIncasso.ListaPreautorizzazioni = New System.Collections.Generic.List(Of tabelle_pos_Scambio_Importo.Preautorizzazione)

        DatiIncasso.Importo = txtTotDaInc.Text


        If dropTest.SelectedValue = "0" Then
            DatiIncasso.importo_non_modificabile_preautorizzazione = True
            DatiIncasso.TestMode = True
        ElseIf dropTest.SelectedValue = "1" Then
            DatiIncasso.importo_non_modificabile_preautorizzazione = False
            DatiIncasso.TestMode = False
        End If

        DatiIncasso.ImportoMassimoRimborsabile = 0   '<<<----- IMPOSTARE

        'Scambio_Importo1.InizializzazioneDati(DatiIncasso)
        'Tony 12/08/2022
        Session("provenienza") = "Multe"
        Session("carica_dati") = lblID.Text
        Session("DaPagare") = txtTotDaInc.Text
        Response.Redirect("pagamenti.aspx")
    End Sub
    Private Sub ScambioImportoClose(ByVal sender As Object, ByVal e As EventArgs)
        tab_incassi.Visible = False
        'UpdatePanel1.Visible = True
        div_steps.Visible = True
        'Trace.Write("----------------------http host: " & Request.ServerVariables("HTTP_HOST"))
        'Trace.Write("----------------------stazione: " & Request.Cookies("SicilyRentCar")("stazione"))
        'Trace.Write("----------------------utente: " & Request.Cookies("SicilyRentCar")("IdUtente"))
        'Trace.Write("----------------------nome: " & Request.Cookies("SicilyRentCar")("nome"))
    End Sub

    Protected Sub CassaPagamentoEseguito(ByVal sender As Object, ByVal e As EventoConOggetto)
        tab_incassi.Visible = False
        'UpdatePanel1.Visible = False
        div_steps.Visible = True

        listPagamenti.DataBind()
        Dim numTentativi As Integer = 0
        If txtTentativiInc.Text = "" Then
            numTentativi = 1
        Else
            If IsNumeric(txtTentativiInc.Text) Then
                numTentativi = CInt(txtTentativiInc.Text) + 1
            Else
                numTentativi = 1
            End If
        End If
        AggiornaStatusIncassatoYesNo(lblID.Text, numTentativi)
        chkIncassatoYesNo.Checked = True
        txtTentativiInc.Text = numTentativi
    End Sub

    Public Sub ScambioImportoTransazioneEseguita(ByVal sender As Object, ByVal e As tabelle_pos_Scambio_Importo.TransazioneEseguitaEventArgs)
        tab_incassi.Visible = True
        'UpdatePanel1.Visible = False
        div_steps.Visible = False

        Select Case e.Transazione.IDFunzione
            Case Is = enum_tipo_pagamento_ares.Richiesta
                'Dim tr As classi_pagamento.TransazionePreautorizzazione = e.Transazione
                'cPagamenti.registra_preautorizzazione(tr, "", "", lblID.Text, "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
            Case Is = enum_tipo_pagamento_ares.Vendita
                'Trace.Write("----------------------id multa: " & lblID.Text)
                '    Dim tr As classi_pagamento.TransazioneVendita = e.Transazione
                '    cPagamenti.registra_vendita(tr, "", "", lblID.Text, "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
            Case Is = enum_tipo_pagamento_ares.Integrazione
                'Dim tr As classi_pagamento.TransazioneIntegrazione = e.Transazione
                'cPagamenti.registra_integrazione(tr, "", "", lblID.Text, "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
            Case Is = enum_tipo_pagamento_ares.Chiusura
                'Dim tr As classi_pagamento.TransazioneChiusura = e.Transazione
                'cPagamenti.registra_chiusura(tr, "", "", lblID.Text, "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
            Case Is = enum_tipo_pagamento_ares.Rimborso
                'Dim tr As classi_pagamento.TransazioneRimborso = e.Transazione
                'cPagamenti.registra_rimborso(tr, "", "", lblID.Text, "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
            Case Is = enum_tipo_pagamento_ares.Storno_Ultima_Operazione
                'Dim tr As classi_pagamento.TransazioneStorno = e.Transazione
                'cPagamenti.registra_storno(tr, "", "", lblID.Text, "", Request.ServerVariables("HTTP_HOST"), Request.Cookies("SicilyRentCar")("stazione"), Request.Cookies("SicilyRentCar")("IdUtente"), Request.Cookies("SicilyRentCar")("nome"))
        End Select
        listPagamenti.DataBind()
        Dim numTentativi As Integer = 0
        If txtTentativiInc.Text = "" Then
            numTentativi = 1
        Else
            If IsNumeric(txtTentativiInc.Text) Then
                numTentativi = CInt(txtTentativiInc.Text) + 1
            Else
                numTentativi = 1
            End If
        End If
        AggiornaStatusIncassatoYesNo(lblID.Text, numTentativi)
        chkIncassatoYesNo.Checked = True
        txtTentativiInc.Text = numTentativi
    End Sub

    Protected Sub AggiornaTotaleDaIncassare()
        Dim totDaInc As Single = 0
        If IsNumeric(txtServFeeDaInc.Text) Then
            totDaInc = totDaInc + CSng(txtServFeeDaInc.Text)
        End If
        'Tony 13/05/2022
        'If IsNumeric(txtMultaDaInc.Text) Then
        '    totDaInc = totDaInc + CSng(txtMultaDaInc.Text)
        'End If
        If IsNumeric(txtImporto.Text) Then
            txtMultaDaInc.Text = txtImporto.Text
            totDaInc = totDaInc + CSng(txtImporto.Text)
        End If
        If IsNumeric(txtAltroDaInc.Text) Then
            totDaInc = totDaInc + CSng(txtAltroDaInc.Text)
        End If
        txtTotDaInc.Text = totDaInc
    End Sub

    Protected Sub btnAggiornaTotInc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAggiornaTotInc.Click
        AggiornaTotaleDaIncassare()
    End Sub

    Protected Sub listPagamenti_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listPagamenti.ItemCommand
        If e.CommandName = "vedi" Then
            tab_dettagli_pagamento.Visible = True

            Dim id_pagamento_extra As Label = e.Item.FindControl("ID_CTRLabel")
            Dim funzione As Label = e.Item.FindControl("funzione")
            Dim DATA_OPERAZIONELabel As Label = e.Item.FindControl("DATA_OPERAZIONELabel")
            Dim lblStato As Label = e.Item.FindControl("lblStato")

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand("SELECT * FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE ID_CTR='" & id_pagamento_extra.Text & "'", Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()
            Rs.Read()

            If Rs.HasRows() Then
                idPagamentoExtra.Text = id_pagamento_extra.Text
                txtPOS_Funzione.Text = funzione.Text
                txtPOS_Stazione.Text = Rs("ID_STAZIONE") & ""
                txtPOS_Cassa.Text = Rs("CASSA") & ""

                If (Rs("titolo") & "") <> "" Then
                    txtPOS_Carta.Text = "XXXX XXXX XXXX XXXX"
                    btnVisualizzaCC.Visible = True
                    lblPasswordCC.Visible = True
                    txtPasswordCC.Visible = True
                Else
                    btnVisualizzaCC.Visible = False
                    lblPasswordCC.Visible = False
                    txtPasswordCC.Visible = False
                    txtPOS_Carta.Text = ""
                End If

                txtPOS_Intestatario.Text = Rs("intestatario") & ""
                txtPOS_Scadenza.Text = Rs("scadenza") & ""
                txtPOS_NrAut.Text = Rs("nr_aut") & ""
                txtPOS_Operatore.Text = funzioni_comuni.getNomeOperatore(Rs("id_operatore_ares") & "")
                txtPOS_DataOperazione.Text = DATA_OPERAZIONELabel.Text
                txtPOS_TerminalID.Text = Rs("TERMINAL_ID") & ""
                txtPOS_BATCH.Text = Rs("NR_BATCH") & ""
                txtPOS_NrPreaut.Text = Rs("NR_PREAUT") & ""
                txtPOS_ScadenzaPreaut.Text = Rs("scadenza_preaut") & ""
                txtPOS_AcquireID.Text = Rs("acquire_id") & ""
                txtPOS_TransationType.Text = Rs("transation_type") & ""
                txtPOS_ActionCode.Text = Rs("action_code") & ""
                txtPOS_Stato.Text = lblStato.Text

                If livello_accesso_eliminare_pagamenti.Text = 3 Then
                    btnModificaDataPagamento.Visible = True
                    txtPOS_DataOperazione.ReadOnly = False
                    btnEliminaPagamento.Visible = True
                Else
                    btnModificaDataPagamento.Visible = False
                    btnEliminaPagamento.Visible = False
                    txtPOS_DataOperazione.Enabled = True
                End If


                If Rs("CASSA") & "" = "" Then
                    Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Dbc2.Open()
                    Dim Cmd2 As New Data.SqlClient.SqlCommand("SELECT * FROM stazioni WITH(NOLOCK) WHERE id='" & Rs("ID_STAZIONE") & "'", Dbc2)

                    Dim Rs2 As Data.SqlClient.SqlDataReader
                    Rs2 = Cmd2.ExecuteReader()
                    Rs2.Read()

                    If Rs2.HasRows() Then
                        txtPOS_Cassa.Text = Rs2("codice") & ""
                    End If

                End If
            End If

            Rs.Close()
            Rs = Nothing
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing
        End If
    End Sub

    Protected Sub listPagamenti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listPagamenti.ItemDataBound
        Dim preaut_aperta As Label = e.Item.FindControl("preaut_aperta")
        Dim operazione_stornata As Label = e.Item.FindControl("operazione_stornata")
        Dim lblStato As Label = e.Item.FindControl("lblStato")
        Dim id_pos_funzioni_ares As Label = e.Item.FindControl("id_pos_funzioni_ares")
        Dim btnVedi As ImageButton = e.Item.FindControl("vedi")
        Dim id_tippag As Label = e.Item.FindControl("ID_TIPPAG")
        Dim lb_Des_ID_ModPag As Label = e.Item.FindControl("lb_Des_ID_ModPag")

        If id_tippag.Text = "1011098650" And lb_Des_ID_ModPag.Text = "" Then
            lb_Des_ID_ModPag.Text = "C.CREDITO"
        End If

        If (operazione_stornata.Text & "") = "True" Then
            lblStato.Text = "STORNATA"
        Else
            If id_pos_funzioni_ares.Text = enum_tipo_pagamento_ares.Richiesta Then
                If (preaut_aperta.Text & "") = "True" Then
                    lblStato.Text = "APERTA"
                Else
                    lblStato.Text = "CHIUSA"
                End If
            End If
        End If

        If livello_accesso_dettaglio_pos.Text = "1" Then
            btnVedi.Visible = False
        End If
    End Sub

    Protected Sub btnChiudiDettPag_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudiDettPag.Click
        tab_dettagli_pagamento.Visible = False
    End Sub
    Protected Function getNazioneGuidatore(ByVal nProt As String, ByVal annoMulta As String) As String

        'restituisce nazione (16 se Italia) del conducente del contratto legato alla multa
        Dim sqla As String = "SELECT  top(1) multe.ContrattoNolo, multe.Targa, multe.Prot, multe.Anno, "
        sqla += "Multe.IDConducente, Multe.UtenteID, CONDUCENTI.NAZIONE "
        sqla += "From CONDUCENTI INNER JOIN contratti ON CONDUCENTI.ID_CONDUCENTE = contratti.id_primo_conducente "
        sqla += "RIGHT OUTER JOIN multe ON contratti.num_contratto = multe.ContrattoNolo "
        sqla += "Where Prot = '" & nProt & "' and Anno='" & annoMulta & "'"

        Dim ris As String = "16"

        Try

            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()

            Dim Cmd As New Data.SqlClient.SqlCommand(sqla, Dbc)

            Dim Rs As Data.SqlClient.SqlDataReader
            Rs = Cmd.ExecuteReader()

            If Rs.HasRows Then
                Rs.Read()
                If Rs!idconducente = 0 Then
                    ris = "16"
                Else
                    ris = Rs![nazione]
                End If

            Else

            End If

            Rs.Close()
            Cmd.Dispose()
            Cmd = Nothing
            Dbc.Close()
            Dbc.Dispose()
            Dbc = Nothing

            Return ris

        Catch ex As Exception
            Return ris
            'HttpContext.Current.Response.Write("error getNumContratto  : <br/>" & ex.Message & "<br/>" & "<br/>")
        End Try






    End Function
    Protected Sub btnInviaEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInviaEmail.Click

        Try

            'verifica nazionalità del guidatore del contratto
            Dim nProt As String = txtProt.Text
            Dim AnnoMulta As String = txtAnno.Text
            Dim idnazione As String = getNazioneGuidatore(nProt, AnnoMulta)

            'Exit Sub 'test



            'recupero smtp da webConfig
            Dim Mail_SMTP As String = "smtp.xinformatica.it" 'ConfigurationManager.AppSettings.Get("Mail_SMTP")

            'recupero mail ufficio multe da webConfig
            Dim Mail_ufficio_multe As String = "ufficiomulte@sicilyrentcar.it" 'ConfigurationManager.AppSettings.Get("Mail_ufficio_multe")

            'Dichiaro e creo un nuovo messaggio
            Dim mail As New MailMessage()

            'Dichiato il mittente
            'mail.From = New MailAddress("noreply@sbc.it") 
            mail.From = New MailAddress(Mail_ufficio_multe, "SRC Rent Car - Ufficio Multe")

            'Dichiaro il destinatario
            'mail.To.Add("dimatteo@xinformatica.it")       'TEST
            mail.To.Add(txtDestinatarioMail.Text)

            'Dichiaro il destinatario CC
            If txtPerConoscenzaMail.Text <> "" Then
                mail.CC.Add(txtPerConoscenzaMail.Text)
            End If

            'Dichiaro il destinatario Bcc
            mail.Bcc.Add(Mail_ufficio_multe) 'sostituire con quello dell'ufficio multe

            Dim corpoMessaggio As String = txtTestoMail.Text
            Dim oggmail As String = ""
            Dim corpoMessaggioITA As String
            Dim corpoMessaggioENG As String

            'ITA
            ''OK ULTIMA VERIFICA TESTI 12.01.2021
            corpoMessaggio = "Gentile cliente," & vbCrLf
            corpoMessaggio += "<br/><br/>in riferimento a quanto in oggetto siamo a trasmettere copia della documentazione relativa ad una multa elevata durante il Suo noleggio auto con la nostra compagnia." & vbCrLf
            corpoMessaggio += "<br/><br/>Distinti saluti" & vbCrLf
            'corpoMessaggio += "<br/><br/><i>Sicily Rent Car</i>" & vbCrLf
            corpoMessaggio += "<br/><br/><a href='https://www.sicilyrentcar.it/'><img src='http://ares.sicilyrentcar.it/img/SRC_logo.jpg' style='text-align:left;width:150px;' alt='' /></a>" & vbCrLf

            corpoMessaggio += "<br/>Sicily Rent Car S.r.l." & vbCrLf
            corpoMessaggio += "<br/>Largo Lituania, 11" & vbCrLf
            corpoMessaggio += "<br/>90146, Palermo Italia" & vbCrLf
            corpoMessaggio += "<br/>P.Iva 02486830819" & vbCrLf
            'corpoMessaggio += "<br/><a target='_blank' href='https://www.sicilyrentcar.it/'>www.sicilyrentcar.it</a>" & vbCrLf
            corpoMessaggio += "<br/><br/><br/>Ai sensi delle vigenti disposizioni in materia si precisa che la presente e-mail, con i suoi eventuali allegati, può contenere informazioni private e/o confidenziali ed è destinata esclusivamente ai destinatari in indirizzo. Se avete ricevuto questa e-mail per errore siete espressamente diffidati dal riprodurla in tutto od in parte o, comunque, dall'utilizzare le informazioni contenute nella stessa e nei suoi eventuali allegati. Siete, altresì, pregati di voler contattare il mittente e di distruggere ogni copia di questa e-mail." & vbCrLf
            corpoMessaggioITA = corpoMessaggio

            'ENG
            'OK ULTIMA VERIFICA TESTI 12.01.2021
            corpoMessaggio = "Dear Customer," & vbCrLf
            corpoMessaggio += "<br/><br/>with reference to the above, we are sending you a copy of the documentation relating to a traffic fine during your car rental with our company." & vbCrLf
            corpoMessaggio += "<br/><br/>Best regards" & vbCrLf
            'corpoMessaggio += "<br/><br/><i>Sicily Rent Car</i>" & vbCrLf
            corpoMessaggio += "<br/><br/><a href='https://www.sicilyrentcar.it/'><img src='http://ares.sicilyrentcar.it/img/SRC_logo.jpg' style='text-align:left;width:150px;' alt='' /></a>" & vbCrLf

            corpoMessaggio += "<br/>Sicily Rent Car S.r.l." & vbCrLf
            corpoMessaggio += "<br/>Largo Lituania, 11" & vbCrLf
            corpoMessaggio += "<br/>90146, Palermo Italia" & vbCrLf
            corpoMessaggio += "<br/>P.Iva 02486830819" & vbCrLf
            'corpoMessaggio += "<br/><a target='_blank' href='https://www.sicilyrentcar.it/'>www.sicilyrentcar.it</a>" & vbCrLf
            corpoMessaggio += "<br/><br/><br/>We inform you that this e-mail, including any attachments, may contain private and/or confidential information. If you are not the addressee or if you have received this e-mail in error, you must not use it or take any action based on this e-mail or any information herein. Please contact the sender immediately and delete any copies of this e-mail." & vbCrLf
            corpoMessaggioENG = corpoMessaggio

            'corpoMessaggio = "" 'TEST DA VERIFICARE 07.01.2021  lo prende dal campo, si dovrebbe aggiornare il testo come da file word.

            If corpoMessaggio <> "" Then

                If idnazione = "16" Then
                    oggmail = txtOggettoMail.Text

                    Dim tmail As String = txtTestoMail.Text
                    tmail = Replace(tmail, vbCrLf, "<br/>")
                    'tmail = Replace(tmail, "in riferimento", "<br/>in riferimento")
                    corpoMessaggio = tmail & vbCrLf
                    corpoMessaggio += "<br/><br/><i>Distinti saluti</i>" & vbCrLf
                    'corpoMessaggio += "<br/><br/><i>Sicily Rent Car</i>" & vbCrLf
                    corpoMessaggio += "<br/><br/><a href='https://www.sicilyrentcar.it/'><img src='http://ares.sicilyrentcar.it/img/SRC_logo.jpg' style='text-align:left;width:150px;' alt='' /></a>" & vbCrLf

                    'corpoMessaggio += "Ufficio Multe" & vbCrLf
                    'corpoMessaggio += "<br/>SICILY RENT CAR S.R.L." & vbCrLf

                    corpoMessaggio += "<br/>Sicily Rent Car S.r.l." & vbCrLf
                    corpoMessaggio += "<br/>Largo Lituania, 11" & vbCrLf
                    corpoMessaggio += "<br/>90146, Palermo Italia" & vbCrLf
                    corpoMessaggio += "<br/>P.Iva 02486830819" & vbCrLf
                    'corpoMessaggio += "<br/><a target='_blank' href='https://www.sicilyrentcar.it/'>www.sicilyrentcar.it</a>" & vbCrLf
                    corpoMessaggio += "<br/><br/><br/>Ai sensi delle vigenti disposizioni in materia si precisa che la presente e-mail, con i suoi eventuali allegati, può contenere informazioni private e/o confidenziali ed è destinata esclusivamente ai destinatari in indirizzo. Se avete ricevuto questa e-mail per errore siete espressamente diffidati dal riprodurla in tutto od in parte o, comunque, dall'utilizzare le informazioni contenute nella stessa e nei suoi eventuali allegati. Siete, altresì, pregati di voler contattare il mittente e di distruggere ogni copia di questa e-mail." & vbCrLf

                Else
                    'altre nazioni diverse da ITA
                    oggmail = txtOggettoMail.Text
                    oggmail = Replace(oggmail, "Multa", "Traffic fine")
                    corpoMessaggio = corpoMessaggioENG

                End If


            Else
                'campo testo vuoto

                If idnazione = "16" Then    'nazione guidatore IT 16
                    oggmail = txtOggettoMail.Text
                    corpoMessaggio = corpoMessaggioITA
                Else
                    'altre nazioni testo inglese
                    oggmail = txtOggettoMail.Text
                    oggmail = Replace(oggmail, "Multa", "Traffic fine")
                    corpoMessaggio = corpoMessaggioENG
                End If


            End If


            mail.Subject = oggmail


            mail.Body = Replace(corpoMessaggio, "!", "")
            'mail.Body = corpoMessaggio

            'Imposta il server smtp di posta da utilizzare        
            Dim Smtp As New Net.Mail.SmtpClient("smtp.xinformatica.it", 25)
            Smtp.Credentials = New System.Net.NetworkCredential("ares_sbc@xinformatica.it", "Sbc!2020")
            'usr = "ares_sbc@xinformatica.it"
            'pwd = "Sbc!2020"
            Smtp.Host = "smtp.xinformatica.it"
            Smtp.EnableSsl = False

            'Verificata 14.01.2021

            'con allegato
            Dim Allegati As String = ""
            Dim msgerrorall As String = ""
            Dim listAllegati As New Generic.List(Of InvioMailMulte.AllegatiMail)
            For Each elemento In ListViewAllegati.Items
                If CType(elemento.FindControl("chkAllegatoEmail"), CheckBox).Checked = True Then
                    'If Allegati = "" Then 'aggiorno l'elenco degli allegati
                    'Allegati = CType(elemento.FindControl("lblNomeFile"), Label).Text
                    'Else
                    '   Allegati = Allegati & "," & CType(elemento.FindControl("lblNomeFile"), Label).Text
                    'End If
                    Dim PathAllegMail As String = CType(elemento.FindControl("lblPercorsoFile"), Label).Text

                    'cambia i percorsi registrati nel db vecchio su server se Entermed  14.01.2021
                    If InStr(1, PathAllegMail, "E:\siti_internet\ares.sicilyrentcar.it\htdocs", 1) > 0 Then
                        PathAllegMail = Replace(PathAllegMail, "E:\siti_internet\ares.sicilyrentcar.it\htdocs\gestione_multe\Allegati\", Server.MapPath("gestione_multe\Allegati\"))
                    End If


                    Dim FileAllegMail As String = CType(elemento.FindControl("lblNomeFile"), Label).Text


                    ' Response.Write(PathAllegMail & FileAllegMail & "<br/>") 'TEST


                    If System.IO.File.Exists(PathAllegMail & FileAllegMail) Then
                        Dim attachment As New System.Net.Mail.Attachment(PathAllegMail & FileAllegMail)
                        mail.Attachments.Add(attachment)
                        Dim temp As New InvioMailMulte.AllegatiMail(FileAllegMail, PathAllegMail)
                        listAllegati.Add(temp)

                    Else
                        msgerrorall = "Attenzione!! File/s allegato/i non presente/i nella cartella: " & PathAllegMail '& FileAllegMail
                    End If


                End If
            Next


            mail.IsBodyHtml = True      'aggiunto 06.01.2021

            'Invia l'e-mail
            Smtp.Send(mail)

            If msgerrorall = "" Then
                Libreria.genUserMsgBox(Me, "Email inviata correttamente")
                'btnInviaEmail.BackColor = Drawing.Color.Green      ''Annullato da email Francesco del 07.01.2021
            Else
                Libreria.genUserMsgBox(Me, "Email inviata correttamente\n" & msgerrorall)
            End If


            'qui il codice per memorizzare la mail nella tabella InvioMail
            Dim my_InvioMail As InvioMailMulte = New InvioMailMulte

            With my_InvioMail
                .DataInvio = Now
                .Destinatario = txtDestinatarioMail.Text
                .PerConoscenza = txtPerConoscenzaMail.Text
                .Oggetto = txtOggettoMail.Text
                .Testo = txtTestoMail.Text
                .UtenteId = Integer.Parse(Request.Cookies("SicilyRentCar")("idUtente"))
                .IdMulta = CInt(lblID.Text)
                'Trace.Write("-------------- valore id utente: " & Integer.Parse(Request.Cookies("SicilyRentCar")("idUtente")))
                my_InvioMail.InsertInvioMailMulte(listAllegati)
            End With

            ListViewMailInviate.DataBind()
            AggiornaListAllegatiMulte(lblID.Text)
            AggiornaStatusInvioMail(lblID.Text)
            txtDataInoltroMail.Text = Now




        Catch ex As Exception
            'HttpContext.Current.Response.Write("error btnInviaEmail  : <br/>" & ex.Message & "<br/>" & "<br/>")
            Libreria.genUserMsgBox(Me, "Errore nell'invio della email")
        End Try



    End Sub
    Protected Sub AggiornaDatiEmail()
        txtOggettoMail.Text = "Multa - Rif. ns. prot. " & txtAnno.Text & "/" & txtProt.Text
        'txtTestoMail.Text = "Invio documenti multa - rif. ns. Prot. " & txtAnno.Text & "/" & txtProt.Text

        Dim txt As String = ""

        txt = "Gentile Cliente," & vbCrLf & vbCrLf

        txt += "in riferimento a quanto in oggetto siamo a trasmettere copia della documentazione relativa ad "
        txt += "una multa elevata durante il Suo noleggio auto con la nostra compagnia."

        txtTestoMail.Text = txt



    End Sub

    Protected Function GetIdContratto(ByVal contratto As String) As String
        Dim IdContrattoReader As SqlDataReader
        Dim strRd As String = "SELECT id FROM dbo.contratti WHERE (attivo = 1) AND (num_contratto = '" & contratto & "')"

        IdContrattoReader = GetReader(strRd)

        While IdContrattoReader.Read()
            Return CStr(IdContrattoReader("id"))
        End While

        'in caso negativo restituisco zero
        Return "0"
        IdContrattoReader.Close()
    End Function

    Protected Sub btnChiudi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChiudi.Click
        Session("ente_cerca") = ""
        Response.Redirect("RicercaMulte.aspx")
    End Sub

    Protected Sub btnNuovaFattura_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovaFattura.Click
        Dim myFattura As Fattura = New Fattura

        'si prendono i dati della ditta
        myFattura.id_ditta = 0
        If txtCodCliente.Text <> "" And txtCodCliente.Text <> "0" Then
            If IsNumeric(txtCodCliente.Text) = True Then
                myFattura.id_ditta = Integer.Parse(txtCodCliente.Text)
            End If
        Else
            If txtCodClienteVendita.Text <> "" And txtCodClienteVendita.Text <> "0" Then
                If IsNumeric(txtCodClienteVendita.Text) = True Then
                    myFattura.id_ditta = Integer.Parse(txtCodClienteVendita.Text)
                End If
            End If
        End If

        'UpdatePanel1.Visible = False
        div_steps.Visible = False
        VisualFatture.Visible = True
        FattureMulte1.InizializzaDatiFattura(lblID.Text, txtAnno.Text & "/" & txtProt.Text, myFattura, txtNumContratto.Text, txtConducente.Text, "new")





        'modalita fattura new=come nuovo inserimento edit=fattura in modifica
    End Sub

    Private Sub FattureMulteClose(ByVal sender As Object, ByVal e As EventArgs)
        'UpdatePanel1.Visible = True
        div_steps.Visible = True
        VisualFatture.Visible = False
        ListViewFattureEmesse.DataBind()
        If ListViewFattureEmesse.Items.Count > 0 Then
            chkFatturatoYesNo.Checked = True
        End If
    End Sub
    Private Sub Enti_close(ByVal sender As Object, ByVal e As gestione_multe_Enti.ScegliEnteEventArgs)
        'UpdatePanel1.Visible = True
        div_steps.Visible = True
        VisulEnti.Visible = False
        If e.IDEnte > 0 Then
            DropEnti.SelectedValue = e.IDEnte
            AggionaEnti(e.IDEnte)
        End If

    End Sub

    Private Sub ArticoliCDS_close(ByVal sender As Object, ByVal e As gestione_multe_ArticoliCDS.ScegliArticoliCDSEventArgs)
        'UpdatePanel1.Visible = True
        div_steps.Visible = True
        VisualArticoliCDS.Visible = False

        If e.IDArticoliCDS > 0 Then
            sqlAllegati.DataBind()
            DropArtCDS.Items.Clear()
            DropArtCDS.DataBind()
            DropArtCDS.SelectedValue = e.IDArticoliCDS
        End If

    End Sub
    Protected Sub AssegnaNuovoProtocollo()
        Dim myMulte As New Multe

        With myMulte
            .UtenteID = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))
            .StatoAperto = True
        End With

        Dim num_id As Integer
        num_id = myMulte.SalvaRecordVuoto()

        lblID.Text = num_id.ToString
        txtAnno.Text = myMulte.Anno
        txtProt.Text = myMulte.Prot
        txtDataInser.Text = myMulte.DataInserimento.ToString("dd/MM/yyyy")

        btnSalvaModifiche.Visible = True
        btnElimina.Visible = True
        btnAssegnaProt.Visible = False
        btnAssegnaProtVuoto.Visible = False
        btnRicercaMovim.Visible = True
        btnRicercaTuttiMov.Visible = True
        div_steps.Visible = True
        AggiornaDatiEmail()
        txtTarga.Enabled = False 'targa non più modificabile non la registrazione prot. (l'hanno richiesto esplicitamente)
    End Sub
    Protected Sub btnAssegnaProtVuoto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssegnaProtVuoto.Click
        Dim myMulte As New Multe

        With myMulte
            .UtenteID = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))
            .StatoAperto = True
        End With

        Dim num_id As Integer
        num_id = myMulte.SalvaRecordVuoto()

        lblID.Text = num_id.ToString
        txtAnno.Text = myMulte.Anno
        txtProt.Text = myMulte.Prot
        txtDataInser.Text = myMulte.DataInserimento.ToString("dd/MM/yyyy")

        btnSalvaModifiche.Visible = True
        btnElimina.Visible = True
        btnAssegnaProt.Visible = False
        btnAssegnaProtVuoto.Visible = False
        btnRicercaMovim.Visible = True
        btnRicercaTuttiMov.Visible = True
        div_steps.Visible = True
        AggiornaDatiEmail()
        txtTarga.Enabled = False 'targa non più modificabile non la registrazione prot. (l'hanno richiesto esplicitamente)
    End Sub

    Protected Sub btnComunicazCliente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnComunicazCliente.Click
        If DropCasistiche.SelectedIndex = 0 Then
            Libreria.genUserMsgBox(Page, "Scegliere una casistica")
            Exit Sub
        End If

        Dim myDati As StampaModelloMulte = New StampaModelloMulte

        'recupero i dati relativi al modello ricorso
        Dim ModRicorsoReader As SqlDataReader
        Dim strRd As String = "SELECT dbo.multe_ModelloRicorsi.idModello, dbo.multe_ModelloRicorsi.NomeModello, "
        strRd = strRd & "dbo.multe_ModelloRicorsi.PercComunicazioneIT, dbo.multe_ModelloRicorsi.PercComunicazioneUK, dbo.multe_ModelloRicorsi.idTipoAllegato, "
        strRd = strRd & "dbo.multe_TipoAllegato.TipoAllegato FROM dbo.multe_casistiche INNER JOIN "
        strRd = strRd & "dbo.multe_ModelloRicorsi ON dbo.multe_casistiche.ModelloID = dbo.multe_ModelloRicorsi.idModello "
        strRd = strRd & "INNER JOIN dbo.multe_TipoAllegato ON dbo.multe_ModelloRicorsi.idTipoAllegato = dbo.multe_TipoAllegato.Id "
        strRd = strRd & "WHERE(dbo.multe_casistiche.ID =" & DropCasistiche.SelectedValue & ")"
        ModRicorsoReader = GetReader(strRd)

        'Response.Write(strRd & "<br><br>")
        'Response.End()

        While ModRicorsoReader.Read()
            myDati.IdModello = ModRicorsoReader("idModello")
            'myDati.NomeModello = ModRicorsoReader("NomeModello") 'preferisco non mettere il tipo di doc in base alla casistica ma una comunicaz generica
            Dim dataTemp As Date = Now
            myDati.NomeModello = "ComunCliente" & Year(dataTemp) & Month(dataTemp) & Day(dataTemp) & Hour(dataTemp) & Minute(dataTemp) & Second(dataTemp)

            'Codice seguente commentato perchè non hanno voluto la possbilità di scelta della lingua manuale e automatica
            'Select Case RadioLinguaDoc.SelectedValue
            '   Case "Aut"
            'If txtNazioneCond.Text = "ITALIA" Then
            'myDati.PercorsoModello = ModRicorsoReader("PercComunicazioneIT")
            'Else
            'myDati.PercorsoModello = ModRicorsoReader("PercComunicazioneUK")
            'End If
            '    Case "IT"
            'myDati.PercorsoModello = ModRicorsoReader("PercComunicazioneIT")
            '    Case "UK"
            'myDati.PercorsoModello = ModRicorsoReader("PercComunicazioneUK")
            'End Select

            'Tony 20-02-2023            
            If DropCasistiche.SelectedValue = 22 Then
                txtNazioneCond.Text = "ITALIA"
                txtConducente.Text = txtAltroResponsabile.Text
                txtDaDataNolo.Text = txtDataDocAltro.Text
            End If
            'FINE Tony

            If txtNazioneCond.Text = "ITALIA" Then
                myDati.PercorsoModello = ModRicorsoReader("PercComunicazioneIT")
            Else
                myDati.PercorsoModello = ModRicorsoReader("PercComunicazioneUK")
            End If

            If myDati.PercorsoModello = "" Then
                Libreria.genUserMsgBox(Page, "Nessuna comunicazione trovata rispetto alla casistica e alla lingua scelta")
                Exit Sub
            End If

            myDati.TipoAllegato = ModRicorsoReader("idTipoAllegato")
            If CheckSalvaComeAllegato.Checked = True Then
                myDati.SalvaComeAllegato = True
            Else
                myDati.SalvaComeAllegato = False
            End If

        End While

        With myDati
            .IdMulta = lblID.Text
            .Prot = txtProt.Text
            .Anno = txtAnno.Text
            .Ente = DropEnti.SelectedItem.ToString
            .IndirizzoEnte = txtEnteIndir.Text
            .CapEnte = txtCap.Text
            .ComuneEnte = txtEnteComune.Text
            .ProvEnte = txtProv.Text
            '.DataInserimento = txtDataInser.Text
            .DataInserimento = Format(Now(), "dd/MM/yyyy")
            .NumVerbale = txtVerbale.Text
            .ImportoVerbale = txtImporto.Text
            .ImportoAddServFee = txtServFeeDaInc.Text
            .DataVerbale = txtDataInfrazione.Text
            .OraVerbale = txtOraInfrazione.Text
            .Targa = txtTarga.Text
            .Contratto = txtNumContratto.Text
            .DataInizioNolo = txtDaDataNolo.Text
            .DataFineNolo = txtAdataNolo.Text
            .Conducente = txtConducente.Text
            .LuogoNascitaCond = txtLuogoNascitaCond.Text
            .DataNascitaCond = txtDataNascitaCond.Text
            .IndirizzoCond = txtIndirizzoCond.Text
            .ComuneComplCond = txtCapCond.Text & " " & txtComuneCond.Text & " (" & txtProvCond.Text & ")"
            .Nazione = txtNazioneCond.Text
            .PatenteCateg = txtCatPatente.Text
            .PatenteNum = txtPatente.Text
            .LuogoRilascioPatente = txtLuogoRilascioPatente.Text
            .DataRilascioPatente = txtDataRilascioPatente.Text
            .DataScadPatente = txtScadPatente.Text
        End With

        ModRicorsoReader.Close()
        'Response.Write(myDati)
        'Response.End()


        Session("StampaModelloMulte") = myDati

        Dim Generator As System.Random = New System.Random()

        Dim num_random As String = Format(Generator.Next(100000000), "000000000")

        If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraModelloMulte.aspx?a=" & num_random & "','')", True)
        End If

        AggiornaStatusComunicClienteYesNo(lblID.Text)
        chkComunizYesNo.Checked = True
    End Sub

    Protected Sub ListViewFattureEmesse_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListViewFattureEmesse.ItemCommand
        Dim IdFatt As Label = e.Item.FindControl("ID_Fattura")
        Dim NumFatt As Label = e.Item.FindControl("lbl_codice_fattura")
        Dim DataFatt As Label = e.Item.FindControl("lbl_data_fattura")

        Dim myFattura As Fattura = New Fattura

        Select Case e.CommandName
            Case Is = "EditFatture"

                myFattura.codice_fattura = Integer.Parse(NumFatt.Text)
                myFattura.anno_fattura = Year(CDate(DataFatt.Text))

                'UpdatePanel1.Visible = False
                div_steps.Visible = False
                VisualFatture.Visible = True

                FattureMulte1.InizializzaDatiFattura(lblID.Text, txtAnno.Text & "/" & txtProt.Text, myFattura, txtNumContratto.Text, txtConducente.Text, "edit")
                'FattureMulte1.InizializzaDatiFattura(lblID.Text, "", myFattura, "", "", "edit")

            Case Is = "StampaFatture"
                Session("IdFattura") = IdFatt.Text
                Response.Redirect("gestione_fatture.aspx")

        End Select
        'If e.CommandName = "EditFatture" Then
        '    Dim NumFatt As Label = e.Item.FindControl("lbl_codice_fattura")
        '    Dim DataFatt As Label = e.Item.FindControl("lbl_data_fattura")

        '    Dim myFattura As Fattura = New Fattura
        '    myFattura.codice_fattura = Integer.Parse(NumFatt.Text)
        '    myFattura.anno_fattura = Year(CDate(DataFatt.Text))

        '    'UpdatePanel1.Visible = False
        '    div_steps.Visible = False
        '    VisualFatture.Visible = True

        '    FattureMulte1.InizializzaDatiFattura(lblID.Text, txtAnno.Text & "/" & txtProt.Text, myFattura, txtNumContratto.Text, txtConducente.Text, "edit")
        '    'FattureMulte1.InizializzaDatiFattura(lblID.Text, "", myFattura, "", "", "edit")
        'End If

    End Sub

    Protected Sub ImageButtnVisualizzaRA_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButtnVisualizzaRA.Click
        'mi procura l'id del contratto che è diverso dal num contratto
        'infatti nella tabella contratti ci possono essese diversi record con num_contratto uguali
        'ma solo uno sara valido e cioè con il campo "attivo=true"
        Session("carica_contratto") = GetIdContratto(txtNumContratto.Text)

        If Session("carica_contratto") = "0" Then
            Libreria.genUserMsgBox(Page, "Contratto inesistente")
        Else
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('contratti.aspx', 'mywindow', 'location=1,status=1,scrollbars=1, width=1100,height=800')", True)
            End If
        End If
    End Sub

    Protected Sub ImageButtonNewEnte_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonNewEnte.Click
        'UpdatePanel1.Visible = False
        div_steps.Visible = False

        'carica dati ente se presente in protocollo - salvo 24.11.2022
        If DropEnti.SelectedValue <> "0" Then
            'AggionaEnti(DropEnti.SelectedValue)
            Session("ente_cerca") = DropEnti.SelectedValue
        Else
            Session("ente_cerca") = ""
        End If

        If Session("ente_cerca") = "" And DropEnti.SelectedValue <> "0" Then
            Session("ente_cerca") = DropEnti.SelectedValue
        End If


        VisulEnti.Visible = True

    End Sub

    Protected Sub AggionaEnti(ByVal indice As Integer)
        Dim MyReader As SqlDataReader
        Dim MyConnection As SqlConnection = New SqlConnection

        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString
        Dim MyCommand As SqlCommand = New SqlCommand()
        MyCommand.CommandText = "Select ID, Ente, Indirizzo, Comune, Cap, Prov, tel, email, emailpec, notes from multe_enti WITH(NOLOCK) where ID=" & indice
        MyCommand.CommandType = CommandType.Text
        MyCommand.Connection = MyConnection

        MyCommand.Connection.Open()
        MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

        While MyReader.Read()
            txtEnteIndir.Text = MyReader("Indirizzo") & ""
            txtEnteComune.Text = MyReader("Comune") & ""
            txtCap.Text = MyReader("Cap") & ""
            txtProv.Text = MyReader("Prov") & ""

            'aggiunto salvo 24.11.2022 


            If Not IsDBNull(MyReader("email")) Then txt_prot_enti_email.Text = MyReader("email")
            If Not IsDBNull(MyReader("tel")) Then txt_prot_enti_tel.Text = MyReader("tel")
            If Not IsDBNull(MyReader("emailpec")) Then txt_prot_enti_emailpec.Text = MyReader("emailpec")
            If Not IsDBNull(MyReader("notes")) Then txt_enti_notes.Text = MyReader("notes")


        End While

        MyReader.Close()



    End Sub

    Protected Sub DropEnti_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropEnti.SelectedIndexChanged
        If DropEnti.SelectedValue.ToString = "" Then

            txtEnteIndir.Text = ""
            txtEnteComune.Text = ""
            txtCap.Text = ""
            txtProv.Text = ""
            Exit Sub
        End If
        AggionaEnti(DropEnti.SelectedValue)
    End Sub

    Protected Sub btnAlriDatiRicorso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAlriDatiRicorso.Click
        If btnAlriDatiRicorso.Text = "Visualizza ulteriori dati per ricorsi" Then
            btnAlriDatiRicorso.Text = "Nascondi ulteriori dati per ricorsi"
            txtModelloCorretto.Text = GetModelloAuto(txtTarga.Text)
            DivAltriDatiRicorso.Visible = True
        Else
            btnAlriDatiRicorso.Text = "Visualizza ulteriori dati per ricorsi"
            DivAltriDatiRicorso.Visible = False
        End If
    End Sub
    Protected Function GetModelloAuto(ByVal targa As String) As String
        Dim MyReader As SqlDataReader
        Dim MyConnection As SqlConnection = New SqlConnection

        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString
        Dim MyCommand As SqlCommand = New SqlCommand()
        Dim myStringa As String = "SELECT dbo.MODELLI.descrizione FROM dbo.veicoli INNER JOIN "
        myStringa = myStringa & "dbo.MODELLI ON dbo.veicoli.id_modello = dbo.MODELLI.ID_MODELLO "
        myStringa = myStringa & "WHERE (dbo.veicoli.targa = N'" & targa & "')"

        'Trace.Write("------------- query: " & myStringa)
        MyCommand.CommandText = myStringa
        MyCommand.CommandType = CommandType.Text
        MyCommand.Connection = MyConnection
        MyCommand.Connection.Open()
        MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

        Dim myModello As String = ""
        If MyReader.HasRows = False Then
            myModello = ""
        End If
        While MyReader.Read()
            myModello = MyReader("descrizione")
        End While

        Return myModello

        MyCommand.Dispose()
        MyConnection.Close()
    End Function

    Protected Sub btnSelezDitta_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSelezDitta.Click
        'UpdatePanel1.Visible = False
        div_steps.Visible = False
        anagrafica_ditte1.Visible = True
    End Sub

    Protected Function VerificaVerbaleEsistente(ByVal verbale As String) As String
        Dim VerbaleReader As SqlDataReader
        Dim strRd As String = "SELECT Prot, Anno FROM dbo.multe WHERE NumVerbale='" & verbale & "'"

        VerbaleReader = GetReader(strRd)

        Dim RisultatoVerifica As String = ""
        While VerbaleReader.Read()
            RisultatoVerifica = RisultatoVerifica & VerbaleReader("Prot") & "/" & VerbaleReader("Anno") & vbCrLf
        End While

        If RisultatoVerifica = "" Then
            Return ""
        Else
            Return "Sono stati trovati i seguenti protocolli con lo stesso verbale:" & vbCrLf & RisultatoVerifica
        End If
    End Function

    Public Sub AggiornaStatusRicorsoYesNo(ByVal idMulta As Integer)
        Dim sqlStr As String = "UPDATE multe SET RicorsoYesNo = 1 WHERE ID=" & idMulta
        'HttpContext.Current.Trace.Write("prima dell'update --------------------------------" & sqlStr)
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub AggiornaStatusComunicClienteYesNo(ByVal idMulta As Integer)
        Dim sqlStr As String = "UPDATE multe SET ComunicazClienteYesNo = 1 WHERE ID=" & idMulta
        'HttpContext.Current.Trace.Write("prima dell'update --------------------------------" & sqlStr)
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub AggiornaStatusInvioMail(ByVal idMulta As Integer)
        Dim dataOdierna As DateTime = Now
        Dim sqlStr As String = "UPDATE multe SET DataMailCliente = CONVERT(DATETIME, '" & Year(dataOdierna) & "-" & Month(dataOdierna) & "-" & Day(dataOdierna) & " " & Hour(dataOdierna) & ":" & Minute(dataOdierna) & ":00', 102) WHERE ID=" & idMulta
        'HttpContext.Current.Trace.Write("prima dell'update --------------------------------" & sqlStr)
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Public Sub AggiornaStatusIncassatoYesNo(ByVal idMulta As Integer, ByVal numTent As Integer) 'aggiorna anche i numeri di tentativi
        Dim sqlStr As String = "UPDATE multe SET IncassatoYesNo=1, NumTentativiIncassi=" & numTent & " WHERE ID=" & idMulta
        'HttpContext.Current.Trace.Write("prima dell'update --------------------------------" & sqlStr)
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Protected Sub ImageButtonNewArtCDS_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonNewArtCDS.Click
        'UpdatePanel1.Visible = False
        div_steps.Visible = False
        VisualArticoliCDS.Visible = True
    End Sub

    Protected Function VerificaEsistenzaIncassiMulte(ByVal idMulta As String) As Boolean
        Dim MyReader As SqlDataReader
        Dim MyConnection As SqlConnection = New SqlConnection

        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString
        Dim MyCommand As SqlCommand = New SqlCommand()
        Dim myStringa As String = "SELECT ID_CTR FROM PAGAMENTI_EXTRA WHERE N_MULTA_RIF = '" & idMulta & "'"

        'Trace.Write("------------- query: " & myStringa)
        MyCommand.CommandText = myStringa
        MyCommand.CommandType = CommandType.Text
        MyCommand.Connection = MyConnection
        MyCommand.Connection.Open()
        MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

        If MyReader.HasRows = False Then
            Return False
        Else
            Return True
        End If

        MyCommand.Dispose()
        MyConnection.Close()
    End Function

    Protected Function VerificaEsistenzaFattureMulte(ByVal idMulta As String) As Boolean
        Dim MyReader As SqlDataReader
        Dim MyConnection As SqlConnection = New SqlConnection

        MyConnection.ConnectionString = ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString
        Dim MyCommand As SqlCommand = New SqlCommand()
        Dim myStringa As String = "SELECT id FROM Fatture WHERE id_riferimento = '" & idMulta & "'"

        'Trace.Write("------------- query: " & myStringa)
        MyCommand.CommandText = myStringa
        MyCommand.CommandType = CommandType.Text
        MyCommand.Connection = MyConnection
        MyCommand.Connection.Open()
        MyReader = MyCommand.ExecuteReader(CommandBehavior.CloseConnection)

        If MyReader.HasRows = False Then
            Return False
        Else
            Return True
        End If

        MyCommand.Dispose()
        MyConnection.Close()
    End Function

    Protected Sub btnModificaDataPagamento_Click(sender As Object, e As System.EventArgs) Handles btnModificaDataPagamento.Click

        Dim strQuery As String = ""

        If Trim(txtPOS_DataOperazione.Text) = "" Then
            Libreria.genUserMsgBox(Me, "Specificare la data dell'operazione")
        Else
            Try
                Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                conn.Open()

                Dim data_pagamento As String = getDataDb_con_orario(txtPOS_DataOperazione.Text.Replace(".", ":"))
                Dim data_pagamento_no_ora As String = getDataDb_senza_orario(txtPOS_DataOperazione.Text.Replace(".", ":"))


                strQuery = "UPDATE PAGAMENTI_EXTRA SET DATA=convert(datetime,'" & data_pagamento_no_ora & "',102), DATA_OPERAZIONE=convert(datetime,'" & data_pagamento & "',102) WHERE ID_CTR=" & idPagamentoExtra.Text

                Dim cmd As New Data.SqlClient.SqlCommand(strQuery, conn)


                cmd.ExecuteNonQuery()

                Libreria.genUserMsgBox(Me, "Pagamento modificato correttamente.")

                cmd.Dispose()
                cmd = Nothing
                conn.Close()
                conn.Dispose()
                conn = Nothing

                listPagamenti.DataBind()
                txtPOS_DataOperazione.Focus()
            Catch ex As Exception
                Libreria.genUserMsgBox(Me, "Si è verificato un errore: controllare la data e l'ora specificati. ERROR SQL: " & strQuery)
            End Try



        End If
    End Sub

    Protected Sub btnEliminaPagamento_Click(sender As Object, e As System.EventArgs) Handles btnEliminaPagamento.Click
        Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        conn.Open()

        Dim strQuery As String
        strQuery = "DELETE FROM PAGAMENTI_EXTRA WHERE ID_CTR=" & idPagamentoExtra.Text

        Dim cmd As New Data.SqlClient.SqlCommand(strQuery, conn)

        Try
            cmd.ExecuteNonQuery()

            listPagamenti.DataBind()

            txtPOS_Carta.Text = ""
            idPagamentoExtra.Text = ""

            tab_dettagli_pagamento.Visible = False

            Libreria.genUserMsgBox(Me, "Pagamento eliminato correttamente.")
        Catch ex As Exception
            Libreria.genUserMsgBox(Me, "Impossibile eliminare questo pagamento")
        End Try


        cmd.Dispose()
        cmd = Nothing
        conn.Close()
        conn.Dispose()
        conn = Nothing
    End Sub

    Protected Function CheckPasswordCC() As Boolean
        Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        conn.Open()

        Dim strQuery As String
        strQuery = "SELECT id FROM operatori WHERE id=" & Request.Cookies("SicilyRentCar")("IdUtente") & " AND password='" & Trim(txtPasswordCC.Text) & "'"

        Dim cmd As New Data.SqlClient.SqlCommand(strQuery, conn)

        Dim test As String = cmd.ExecuteScalar & ""

        If test <> "" Then
            strQuery = "INSERT INTO log_cc (id_utente, id_pagamenti_extra, data, status) VALUES ('" & Request.Cookies("SicilyRentCar")("IdUtente") & "'," & idPagamentoExtra.Text & ", GetDate(),'OK')"
            cmd = New Data.SqlClient.SqlCommand(strQuery, conn)
            cmd.ExecuteNonQuery()

            CheckPasswordCC = True

            txtPasswordCC.Text = ""
        Else
            strQuery = "INSERT INTO log_cc (id_utente, id_pagamenti_extra, data, status) VALUES ('" & Request.Cookies("SicilyRentCar")("IdUtente") & "'," & idPagamentoExtra.Text & ", GetDate(),'PWD ERRATA')"
            cmd = New Data.SqlClient.SqlCommand(strQuery, conn)
            cmd.ExecuteNonQuery()

            CheckPasswordCC = False

            txtPasswordCC.Text = ""
        End If


        cmd.Dispose()
        cmd = Nothing
        conn.Close()
        conn.Dispose()
        conn = Nothing
    End Function




    Protected Sub btnVisualizzaCC_Click(sender As Object, e As System.EventArgs) Handles btnVisualizzaCC.Click
        txtPOS_Carta.Focus()

        If Trim(txtPasswordCC.Text) = "" Then
            Libreria.genUserMsgBox(Me, "Specificare la password del proprio account per poter visualizzare il numero della carta di credito.")
        ElseIf Not CheckPasswordCC() Then
            Libreria.genUserMsgBox(Me, "Attenzione: password errata. Il tentativo di visualizzare il numero della carta di credito è stato registrato.")
        Else
            salva_logCarta(txtProt.Text)

            Dim conn As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            conn.Open()

            Dim strQuery As String
            strQuery = "SELECT titolo FROM PAGAMENTI_EXTRA WITH(NOLOCK) WHERE ID_CTR='" & idPagamentoExtra.Text & "'"

            Dim cmd As New Data.SqlClient.SqlCommand(strQuery, conn)

            Dim test As String = cmd.ExecuteScalar & ""

            With New Security
                Dim arrayDataPagamento(2) As String
                arrayDataPagamento = Split(txtPOS_DataOperazione.Text, " ")

                'Response.Write("DATA " & CDate(arrayDataPagamento(0)))
                If CDate(arrayDataPagamento(0)) > "18/08/2022" Then
                    txtPOS_Carta.Text = decripta(test, 37)
                Else
                    txtPOS_Carta.Text = .decryptString(test)
                End If
            End With


            cmd.Dispose()
            cmd = Nothing
            conn.Close()
            conn.Dispose()
            conn = Nothing

            btnVisualizzaCC.Visible = False
            lblPasswordCC.Visible = False
            txtPasswordCC.Visible = False
        End If
    End Sub

    'Tony
    Protected Sub salva_logCarta(ByVal NumProtocollo As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()
        Dim Cmd As New Data.SqlClient.SqlCommand("INSERT INTO utenti_clog (id_utente, nominativo, data, pagina, ip) VALUES ('" & Request.Cookies("SicilyRentCar")("IdUtente") & "',(SELECT ISNULL(cognome,'') + ' ' + ISNULL(nome,'') FROM operatori WHERE id='" & Request.Cookies("SicilyRentCar")("IdUtente") & "'),GetDate(),'Visualizzata Carta Credito da Multe- " & NumProtocollo & "','" & Replace(Request.UserHostAddress(), "'", "''") & "')", Dbc)
        'Response.Write(Cmd.CommandText)
        Cmd.ExecuteNonQuery()

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub
End Class

