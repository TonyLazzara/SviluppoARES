
Partial Class gestione_multe_RicercaMulte
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            sqlElencoMulte.SelectCommand = Session("SqlAggiornaElencoMulte") 'si recupera la stringa sql per il ListViewMulte
            'ListViewMulte.DataBind()
        End If
    End Sub


    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerca.Click
        Dim Query As String = WhereQuery()
        If Query = "" Then Exit Sub

        Query = "SELECT id, Prot, Anno, NumVerbale, DataInfrazione, DataNotifica, MultaImporto, Targa, StatoAperto FROM multe WITH(NOLOCK)" & Query & " ORDER BY id DESC"

        'Trace.Write("----------------------------------------sql Ricerca: " & StrRic)
        sqlElencoMulte.SelectCommand = "" 'ho utilizzato questo stratagemma perchè quando tornavo indietro nella pagina di ricerca e i parametri di ricerca erano gli stessi non visualizzava nulla
        sqlElencoMulte.DataBind()
        sqlElencoMulte.SelectCommand = Query
        sqlElencoMulte.DataBind()
        Session("SqlAggiornaElencoMulte") = Query 'viene memorizzata la stringa sql per il ListViewMulte durante il postback
    End Sub

    Public Function WhereQuery() As String
        Dim StrRic As String = ""

        '------------verifico se devono essere filtrati i campi ID --------------------------------
        If txtDaID.Text = "" And txtAID.Text = "" Then
            'In questo caso prendo tutti gli id
            StrRic = StrRic & " WHERE dbo.multe.ID>0"
        ElseIf txtDaID.Text <> "" And txtAID.Text = "" Then
            'in questo caso non devo prendere un intervallo di valori ma un valore preciso
            Dim _daID As Integer = 0
            If IsNumeric(txtDaID.Text) = True Then
                _daID = CInt(txtDaID.Text)
                StrRic = StrRic & " WHERE  dbo.multe.ID =" & _daID
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati ID")
                Return ""
            End If
            txtAID.Text = txtDaID.Text
        Else
            'in questo caso prendo un intervallo di valori
            Dim _daID As Integer = 0
            Dim _AID As Integer = 0
            If IsNumeric(txtDaID.Text) = True Then
                _daID = CInt(txtDaID.Text)
                StrRic = StrRic & " WHERE dbo.multe.ID BETWEEN " & _daID
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati ID")
                Return ""
            End If

            If IsNumeric(txtAID.Text) = True Then
                _AID = CInt(txtAID.Text)
                StrRic = StrRic & " AND " & _AID
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati ID")
                Return ""
            End If

            If _AID < _daID Then
                Libreria.genUserMsgBox(Page, "Errore di immissione dati ID")
                Return ""
            End If
        End If

        '------------verifico se devono essere filtrati i campi Prot --------------------------------
        If txtDaProt.Text = "" And txtAProt.Text = "" Then
            'In questo caso prendo tutti gli i prot
        ElseIf txtDaProt.Text <> "" And txtAProt.Text = "" Then
            'in questo caso non devo prendere un intervallo di valori ma un valore preciso
            Dim _daProt As Integer = 0
            If IsNumeric(txtDaProt.Text) = True Then
                _daProt = CInt(txtDaProt.Text)
                StrRic = StrRic & " AND dbo.multe.Prot =" & _daProt
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati del PROTOCOLLO")
                Return ""
            End If
            txtAProt.Text = txtDaProt.Text
        Else
            'in questo caso prendo un intervallo di valori
            Dim _daProt As Integer = 0
            Dim _AProt As Integer = 0
            If IsNumeric(txtDaProt.Text) = True Then
                _daProt = CInt(txtDaProt.Text)
                StrRic = StrRic & " AND dbo.multe.Prot BETWEEN " & _daProt
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati del PROTOCOLLO")
                Return ""
            End If

            If IsNumeric(txtAProt.Text) = True Then
                _AProt = CInt(txtAProt.Text)
                StrRic = StrRic & " AND " & _AProt
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati del PROTOCOLLO")
                Return ""
            End If

            If _AProt < _daProt Then
                Libreria.genUserMsgBox(Page, "Errore di immissione dati del PROTOCOLLO")
                Return ""
            End If
        End If

        '------------verifico se devono essere filtrati i campi Anno --------------------------------
        If txtDaAnno.Text = "" And txtAAnno.Text = "" Then
            'In questo caso prendo tutti gli anni
        ElseIf txtDaAnno.Text <> "" And txtAAnno.Text = "" Then
            'in questo caso non devo prendere un intervallo di valori ma un valore preciso
            Dim _daAnno As Integer = 0
            If IsNumeric(txtDaAnno.Text) = True Then
                _daAnno = CInt(txtDaAnno.Text)
                StrRic = StrRic & " AND dbo.multe.Anno =" & _daAnno
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi all'ANNO")
                Return ""
            End If
            txtAAnno.Text = txtDaAnno.Text
        Else
            'in questo caso prendo un intervallo di valori
            Dim _daAnno As Integer = 0
            Dim _aAnno As Integer = 0
            If IsNumeric(txtDaAnno.Text) = True Then
                _daAnno = CInt(txtDaAnno.Text)
                StrRic = StrRic & " AND dbo.multe.Anno BETWEEN " & _daAnno
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi all'ANNO")
                Return ""
            End If

            If IsNumeric(txtAAnno.Text) = True Then
                _aAnno = CInt(txtAAnno.Text)
                StrRic = StrRic & " AND " & _aAnno
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi all'ANNO")
                Return ""
            End If

            If _aAnno < _daAnno Then
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi all'ANNO")
                Return ""
            End If
        End If

        '------------verifico se devono essere filtrati i campi num verbale --------------------------------
        If txtDaNumVerb.Text = "" And txtANumVerb.Text = "" Then
            'In questo caso prendo tutti gli Gli anni
        ElseIf txtDaNumVerb.Text <> "" And txtANumVerb.Text = "" Then
            'in questo caso non devo prendere un intervallo di valori ma un valore preciso
            StrRic = StrRic & " AND dbo.multe.NumVerbale = '" & txtDaNumVerb.Text & "'"
            txtANumVerb.Text = txtDaNumVerb.Text
        Else
            'in questo caso prendo un intervallo di valori
            StrRic = StrRic & " AND dbo.multe.NumVerbale BETWEEN '" & txtDaNumVerb.Text & "'"
            StrRic = StrRic & " AND '" & txtANumVerb.Text & "'"

            If txtANumVerb.Text < txtDaNumVerb.Text Then
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi al N. VERBALE")
                Return ""
            End If
        End If

        '------------verifico se devono essere filtrati i campi dell'ora infrazione --------------------------------
        ' -----------devo saperlo prima se impostare la data dell'infrazione con le ore
        Dim _daOraInfrazione As String
        Dim _daMinutiInfrazione As String
        Dim _aOraInfrazione As String
        Dim _aMinutiInfrazione As String
        'Controllo e imposto ora e minuti infrazione (DA)
        If txtDaOraInfraz.Text = "" Then
            _daOraInfrazione = ""
            _daMinutiInfrazione = ""
        Else
            If IsNumeric(Left(txtDaOraInfraz.Text, 2)) = True Then
                _daOraInfrazione = CInt(Left(txtDaOraInfraz.Text, 2))
                If _daOraInfrazione < 0 And _daOraInfrazione > 23 Then
                    Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi all'ORA dell'INFRAZIONE")
                    Return ""
                End If
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi all'ORA dell'INFRAZIONE")
                Return ""
            End If

            If IsNumeric(Right(txtDaOraInfraz.Text, 2)) = True Then
                _daMinutiInfrazione = CInt(Right(txtDaOraInfraz.Text, 2))
                If _daMinutiInfrazione < 0 And _daOraInfrazione > 59 Then
                    Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi all'ORA dell'INFRAZIONE")
                    Return ""
                End If
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi all'ORA dell'INFRAZIONE")
                Return ""
            End If
        End If

        'Controllo e imposto ora e minuti infrazione (A)
        If txtAOraInfraz.Text = "" Then
            _aOraInfrazione = ""
            _aMinutiInfrazione = ""
        Else
            If IsNumeric(Left(txtAOraInfraz.Text, 2)) = True Then
                _aOraInfrazione = CInt(Left(txtAOraInfraz.Text, 2))
                If _aOraInfrazione < 0 And _aOraInfrazione > 23 Then
                    Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi all'ORA dell'INFRAZIONE")
                    Return ""
                End If
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi all'ORA dell'INFRAZIONE")
                Return ""
            End If

            If IsNumeric(Right(txtAOraInfraz.Text, 2)) = True Then
                _aMinutiInfrazione = CInt(Right(txtAOraInfraz.Text, 2))
                If _aMinutiInfrazione < 0 And _aOraInfrazione > 59 Then
                    Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi all'ORA dell'INFRAZIONE")
                    Return ""
                End If
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi all'ORA dell'INFRAZIONE")
                Return ""
            End If
        End If

        '------------verifico se devono essere filtrati i campi Data Infrazione --------------------------------
        If txtDaDataInfraz.Text = "" And txtADataInfraz.Text = "" Then
            'In questo caso prendo tutti tutte le date notifica
        ElseIf txtDaDataInfraz.Text <> "" And txtADataInfraz.Text = "" Then
            'in questo caso non devo prendere un intervallo di valori ma un valore preciso
            Dim _daDataInfrazione As Date
            If IsDate(txtDaDataInfraz.Text) = True Then
                _daDataInfrazione = CDate(txtDaDataInfraz.Text)
                If txtDaOraInfraz.Text = "" Then
                    StrRic = StrRic & " AND dbo.multe.DataInfrazione BETWEEN CONVERT(DATETIME, ' " & Year(_daDataInfrazione) & "-"
                    StrRic = StrRic & Month(_daDataInfrazione) & "-" & Day(_daDataInfrazione) & " 00:00:00', 102)"
                    StrRic = StrRic & " AND CONVERT(DATETIME, ' " & Year(_daDataInfrazione) & "-"
                    StrRic = StrRic & Month(_daDataInfrazione) & "-" & Day(_daDataInfrazione) & " 23:59:59', 102)"

                Else
                    StrRic = StrRic & " AND dbo.multe.DataInfrazione = CONVERT(DATETIME, ' " & Year(_daDataInfrazione) & "-"
                    StrRic = StrRic & Month(_daDataInfrazione) & "-" & Day(_daDataInfrazione)
                    StrRic = StrRic & " " & _daOraInfrazione & ":" & _daMinutiInfrazione & ":00', 102)"
                End If
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi alla data di NOTIFICA")
                Return ""
            End If
            txtADataInfraz.Text = txtDaDataInfraz.Text
            txtAOraInfraz.Text = txtDaOraInfraz.Text
        Else
            'in questo caso prendo un intervallo di valori
            Dim _daDataInfrazione As Date
            Dim _aDataInfrazione As Date
            If IsDate(txtDaDataInfraz.Text) = True Then
                _daDataInfrazione = CDate(txtDaDataInfraz.Text)
                If txtDaOraInfraz.Text = "" Then
                    StrRic = StrRic & " AND dbo.multe.DataInfrazione BETWEEN CONVERT(DATETIME, ' " & Year(_daDataInfrazione) & "-"
                    StrRic = StrRic & Month(_daDataInfrazione) & "-" & Day(_daDataInfrazione) & " 00:00:00', 102)"
                Else
                    StrRic = StrRic & " AND dbo.multe.DataInfrazione BETWEEN CONVERT(DATETIME, ' " & Year(_daDataInfrazione) & "-"
                    StrRic = StrRic & Month(_daDataInfrazione) & "-" & Day(_daDataInfrazione)
                    StrRic = StrRic & " " & _daOraInfrazione & ":" & _daMinutiInfrazione & ":00', 102)"
                End If
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi alla data di NOTIFICA")
                Return ""
            End If

            If IsDate(txtADataInfraz.Text) = True Then
                _aDataInfrazione = CDate(txtADataInfraz.Text)
                If txtAOraInfraz.Text = "" Then
                    StrRic = StrRic & " AND CONVERT(DATETIME, ' " & Year(_aDataInfrazione) & "-"
                    StrRic = StrRic & Month(_aDataInfrazione) & "-" & Day(_aDataInfrazione) & " 23:59:59', 102)"
                Else
                    StrRic = StrRic & " AND CONVERT(DATETIME, ' " & Year(_aDataInfrazione) & "-"
                    StrRic = StrRic & Month(_aDataInfrazione) & "-" & Day(_aDataInfrazione)
                    StrRic = StrRic & " " & _aOraInfrazione & ":" & _aMinutiInfrazione & ":00', 102)"
                End If
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi alla data di INFRAZIONE")
                Return ""
            End If
            'verifica per eventuali incogruenze dati da inizio a fine data e ora infrazione 
            If _aDataInfrazione < _daDataInfrazione Then
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi alla data di INFRAZIONE")
                Return ""
            End If

            If _aDataInfrazione = _daDataInfrazione And txtDaOraInfraz.Text <> "" And txtAOraInfraz.Text <> "" Then
                If CInt(_aOraInfrazione) < CInt(_daOraInfrazione) Then
                    Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi alla data di INFRAZIONE")
                    Return ""
                ElseIf _daOraInfrazione = _aOraInfrazione Then
                    If CInt(_aMinutiInfrazione) < CInt(_daMinutiInfrazione) Then
                        Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi alla data di INFRAZIONE")
                        Return ""
                    End If
                End If
            End If

        End If

        '------------verifico se devono essere filtrati i campi Data Notifica --------------------------------
        If txtDaDataNotifica.Text = "" And txtADataNotifica.Text = "" Then
            'In questo caso prendo tutti tutte le date notifica
        ElseIf txtDaDataNotifica.Text <> "" And txtADataNotifica.Text = "" Then
            'in questo caso non devo prendere un intervallo di valori ma un valore preciso
            Dim _daDataNotifica As Date
            If IsDate(txtDaDataNotifica.Text) = True Then
                _daDataNotifica = CDate(txtDaDataNotifica.Text)
                StrRic = StrRic & " AND dbo.multe.DataNotifica = CONVERT(DATETIME, ' " & Year(_daDataNotifica) & "-"
                StrRic = StrRic & Month(_daDataNotifica) & "-" & Day(_daDataNotifica) & " 00:00:00', 102)"
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi alla data di NOTIFICA")
                Return ""
            End If
            txtADataNotifica.Text = txtDaDataNotifica.Text
        Else
            'in questo caso prendo un intervallo di valori
            Dim _daDataNotifica As Date
            Dim _aDataNotifica As Date
            If IsDate(txtDaDataNotifica.Text) = True Then
                _daDataNotifica = CDate(txtDaDataNotifica.Text)
                StrRic = StrRic & " AND dbo.multe.DataNotifica BETWEEN CONVERT(DATETIME, ' " & Year(_daDataNotifica) & "-"
                StrRic = StrRic & Month(_daDataNotifica) & "-" & Day(_daDataNotifica) & " 00:00:00', 102)"
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi alla data di NOTIFICA")
                Return ""
            End If

            If IsDate(txtADataNotifica.Text) = True Then
                _aDataNotifica = CDate(txtADataNotifica.Text)
                StrRic = StrRic & " AND CONVERT(DATETIME, ' " & Year(_aDataNotifica) & "-"
                StrRic = StrRic & Month(_aDataNotifica) & "-" & Day(_aDataNotifica) & " 23:59:59', 102)"
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi alla data di NOTIFICA")
                Return ""
            End If

            If _aDataNotifica < _daDataNotifica Then
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi alla data di NOTIFICA")
                Return ""
            End If
        End If

        '------------verifico se devono essere filtrati i campi della TARGA --------------------------------
        If txtDaTarga.Text = "" And txtATarga.Text = "" Then
            'In questo caso prendo tutti gli Gli anni
        ElseIf txtDaTarga.Text <> "" And txtATarga.Text = "" Then
            'in questo caso non devo prendere un intervallo di valori ma un valore preciso
            StrRic = StrRic & " AND dbo.multe.Targa = '" & txtDaTarga.Text & "'"
            txtATarga.Text = txtDaTarga.Text
        Else
            'in questo caso prendo un intervallo di valori
            StrRic = StrRic & " AND dbo.multe.Targa BETWEEN '" & txtDaTarga.Text & "'"
            StrRic = StrRic & " AND '" & txtATarga.Text & "'"

            If txtATarga.Text < txtDaTarga.Text Then
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi alla TARGA")
                Return ""
            End If
        End If

        '------------verifico se devono essere filtrati i campi Data Inserimento --------------------------------
        If txtDaDataInser.Text = "" And txtADataInser.Text = "" Then
            'In questo caso prendo tutti tutte le date di inserimento
        ElseIf txtDaDataInser.Text <> "" And txtADataInser.Text = "" Then
            'in questo caso non devo prendere un intervallo di valori ma un valore preciso
            Dim _daDataInserim As Date
            If IsDate(txtDaDataInser.Text) = True Then
                _daDataInserim = CDate(txtDaDataInser.Text)
                StrRic = StrRic & " AND dbo.multe.DataInserimento BETWEEN CONVERT(DATETIME, ' " & Year(_daDataInserim) & "-"
                StrRic = StrRic & Month(_daDataInserim) & "-" & Day(_daDataInserim) & " 00:00:00', 102)"
                StrRic = StrRic & " AND CONVERT(DATETIME, ' " & Year(_daDataInserim) & "-"
                StrRic = StrRic & Month(_daDataInserim) & "-" & Day(_daDataInserim) & " 23:59:59', 102)"
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi alla data di INSERIMENTO")
                Return ""
            End If
            txtADataInser.Text = txtDaDataInser.Text
        Else
            'in questo caso prendo un intervallo di valori
            Dim _daDataInser As Date
            Dim _aDataInser As Date
            If IsDate(txtDaDataInser.Text) = True Then
                _daDataInser = CDate(txtDaDataInser.Text)
                StrRic = StrRic & " AND dbo.multe.DataInserimento BETWEEN CONVERT(DATETIME, ' " & Year(_daDataInser) & "-"
                StrRic = StrRic & Month(_daDataInser) & "-" & Day(_daDataInser) & " 00:00:00', 102)"
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi alla data di INSERIMENTO")
                Return ""
            End If

            If IsDate(txtADataInser.Text) = True Then
                _aDataInser = CDate(txtADataInser.Text)
                StrRic = StrRic & " AND CONVERT(DATETIME, ' " & Year(_aDataInser) & "-"
                StrRic = StrRic & Month(_aDataInser) & "-" & Day(_aDataInser) & " 23:59:59', 102)"
            Else
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi alla data di INSERIMENTO")
                Return ""
            End If

            If _aDataInser < _daDataInser Then
                Libreria.genUserMsgBox(Page, "Errore di immissione dati relativi alla data di INSERIMENTO")
                Return ""
            End If
        End If

        '------------verifico se devono essere filtrati le multe chiuse --------------------------------
        If CInt(DropStatoMulta.SelectedValue) >= 0 Then
            'in questo caso vengono filtrate le multe aperte o chiuse a secondo il valore di DropStatoMulta
            StrRic = StrRic & " AND dbo.multe.StatoAperto = " & DropStatoMulta.SelectedValue
        End If

        '------------verifico se devono essere filtrati le multe in base alla casistica-------------------
        If CInt(DropCasistiche.SelectedValue) > 0 Then
            StrRic = StrRic & " AND dbo.multe.CasisticaID = " & DropCasistiche.SelectedValue
        End If

        '------------verifico se devono essere filtrati le multe in base alla presenza di inc -------------
        If CInt(DropIncassate.SelectedValue) > -1 Then 'se diverso da tutte
            If CInt(DropIncassate.SelectedValue) = 1 Then
                'nel caso di incasso
                StrRic = StrRic & " AND dbo.multe.IncassatoYesNo = " & DropIncassate.SelectedValue
            Else
                'nel caso di non incasso
                StrRic = StrRic & " AND (dbo.multe.IncassatoYesNo = " & DropIncassate.SelectedValue & " OR dbo.multe.IncassatoYesNo IS NULL)"
            End If
        End If

        '------------verifico se devono essere filtrati le multe in base al tipo di mancato incasso
        If CInt(DropTipoMancInc.SelectedValue) > 0 Then
            StrRic = StrRic & " AND dbo.multe.EnteId = " & DropTipoMancInc.SelectedValue
            'StrRic = StrRic & " AND dbo.multe.MotivoMancInc = " & DropTipoMancInc.SelectedValue
        End If

        '------------verifico se devono essere filtrati le multe in base alla presenza di fatture -------------
        If CInt(DropFatturate.SelectedValue) > -1 Then 'se diverso da tutte
            If CInt(DropFatturate.SelectedValue) = 1 Then
                'nel caso di fattura fatta
                StrRic = StrRic & " AND dbo.multe.FatturatoYesNo = " & DropFatturate.SelectedValue
            Else
                'nel caso di fattura non fatta
                StrRic = StrRic & " AND (dbo.multe.FatturatoYesNo = " & DropFatturate.SelectedValue & " OR dbo.multe.FatturatoYesNo IS NULL)"
            End If
        End If

        '------------verifico se devono essere filtrati le multe in base al ricorso fatto -------------
        If CInt(DropRicorso.SelectedValue) > -1 Then 'se diverso da tutte
            If CInt(DropRicorso.SelectedValue) = 1 Then
                'nel caso di ricorso fatto
                StrRic = StrRic & " AND dbo.multe.RicorsoYesNo = " & DropRicorso.SelectedValue
            Else
                'nel caso di ricorso non fatto
                StrRic = StrRic & " AND (dbo.multe.RicorsoYesNo = " & DropRicorso.SelectedValue & " OR dbo.multe.RicorsoYesNo IS NULL)"
            End If
        End If

        '------------verifico se devono essere filtrati le multe in base alla comunic cliente fatta -------------
        If CInt(DropComunCliente.SelectedValue) > -1 Then 'se diverso da tutte
            If CInt(DropComunCliente.SelectedValue) = 1 Then
                'nel caso di comunicaz fatta
                StrRic = StrRic & " AND dbo.multe.ComunicazClienteYesNo = " & DropComunCliente.SelectedValue
            Else
                'nel caso di comunicaz non fatta
                StrRic = StrRic & " AND (dbo.multe.ComunicazClienteYesNo = " & DropComunCliente.SelectedValue & " OR dbo.multe.ComunicazClienteYesNo IS NULL)"
            End If
        End If

        '------------verifico se devono essere filtrati le multe in base al fax fatto -------------

        '    'If CInt(DropFaxFatto.SelectedValue) = 1 Then
        '    '    'nel caso di fax fatto
        '    '    StrRic = StrRic & " AND dbo.multe.RicorsoDataFax IS NOT NULL"
        '    'Else
        '    '    'nel caso di fax non fatto
        '    '    StrRic = StrRic & " AND dbo.multe.RicorsoDataFax IS NULL"
        '    'End If

        If CInt(ddl_provenienza.SelectedValue) > 0 Then
            StrRic = StrRic & " AND dbo.multe.ProvenienzaID = " & ddl_provenienza.SelectedValue
        End If


        '------------verifico se devono essere filtrati le multe in base alla fattura del Locatore -------------
        If CInt(DropFattLocat.SelectedValue) > -1 Then 'se diverso da tutte
            If CInt(DropFattLocat.SelectedValue) = 1 Then
                'nel caso di presenza di fatture di Locatori
                StrRic = StrRic & " AND (dbo.multe.LocatoreNumFatt <> '' AND dbo.multe.LocatoreNumFatt IS NOT NULL)"
            Else
                'nel caso di non presenza di fatture di Locatori
                StrRic = StrRic & " AND (dbo.multe.LocatoreNumFatt = '' OR dbo.multe.LocatoreNumFatt IS NULL)"
            End If
        End If

        '------------verifico se devono essere filtrati le multe in base al rimborso fatto -------------
        If CInt(DropRimborsate.SelectedValue) > -1 Then 'se diverso da tutte
            If CInt(DropRimborsate.SelectedValue) = 1 Then
                'nel caso di rimborso fatto
                StrRic = StrRic & " AND dbo.multe.RimborsatoYesNo = " & DropRimborsate.SelectedValue
            Else
                'nel caso di rimborso non fatto
                StrRic = StrRic & " AND (dbo.multe.RimborsatoYesNo = " & DropRimborsate.SelectedValue & " OR dbo.multe.RimborsatoYesNo IS NULL)"
            End If
        End If

        '------------verifico se devono essere filtrati le multe in base all'eventuale multa pagata -------------
        If CInt(DropPagate.SelectedValue) > -1 Then 'se diverso da tutte
            If CInt(DropPagate.SelectedValue) = 1 Then
                'nel caso di multa pagata
                StrRic = StrRic & " AND dbo.multe.PagatoYesNo = " & DropPagate.SelectedValue
            Else
                'nel caso di multa non pagata
                StrRic = StrRic & " AND (dbo.multe.PagatoYesNo = " & DropPagate.SelectedValue & " OR dbo.multe.PagatoYesNo IS NULL)"
            End If
        End If

        '------------verifico se devono essere filtrati le multe in base al operatore selezionato ------------
        If CInt(DropOperatori.SelectedValue) > 0 Then
            StrRic = StrRic & " AND dbo.multe.UtenteID = " & DropOperatori.SelectedValue
        End If

        Return StrRic
    End Function

    Protected Sub ListViewMulte_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListViewMulte.ItemCommand
        If e.CommandName = "SelezMulta" Then
            Dim idMulta_riga As Label = e.Item.FindControl("lblIdMulta")
            Response.Redirect("gestione_multe.aspx?IdMulta=" & idMulta_riga.Text)
        End If
    End Sub

    Protected Sub btnRipulisciCampi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRipulisciCampi.Click
        txtDaID.Text = ""
        txtAID.Text = ""
        txtDaProt.Text = ""
        txtAProt.Text = ""
        txtDaAnno.Text = ""
        txtAAnno.Text = ""
        txtDaDataInser.Text = ""
        txtADataInser.Text = ""
        txtDaNumVerb.Text = ""
        txtANumVerb.Text = ""
        txtDaDataNotifica.Text = ""
        txtADataNotifica.Text = ""
        txtDaTarga.Text = ""
        txtATarga.Text = ""
        txtDaDataInfraz.Text = ""
        txtADataInfraz.Text = ""
        txtDaOraInfraz.Text = ""
        txtAOraInfraz.Text = ""
        DropStatoMulta.SelectedValue = -1
        DropCasistiche.SelectedIndex = 0
        DropIncassate.SelectedValue = -1
        DropTipoMancInc.SelectedIndex = 0
        DropFatturate.SelectedValue = -1
        DropRicorso.SelectedValue = -1
        DropComunCliente.SelectedValue = -1
        DropFaxFatto.SelectedValue = -1        'nasconto 13.05.2022
        ddl_provenienza.SelectedValue = -1      'aggiunto 13.05.2022
        DropFattLocat.SelectedValue = -1
        DropRimborsate.SelectedValue = -1
        DropPagate.SelectedValue = -1
        DropOperatori.SelectedIndex = 0
        'sqlElencoMulte.SelectCommand = "SELECT id, Prot, Anno, NumVerbale, DataInfrazione, DataNotifica, MultaImporto, targa, StatoAperto FROM multe WITH(NOLOCK) WHERE ID=0"
        'sqlElencoMulte.DataBind()
    End Sub

    Protected Sub btnNuovaMulta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuovaMulta.Click
        Response.Redirect("gestione_multe.aspx")
    End Sub

    Protected Sub btnGenProtVuoti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenProtVuoti.Click
        If txtNumProt.Text = "" Or txtNumProt.Text = "0" Or Not IsNumeric(txtNumProt.Text) Then
            Libreria.genUserMsgBox(Page, "Indicare un numero da 1 a 50")
            Exit Sub
        End If

        If CInt(txtNumProt.Text) > 50 Or CInt(txtNumProt.Text) < 1 Then
            Libreria.genUserMsgBox(Page, "Si possono generare protocolli vuoti da 1 a max 50")
            Exit Sub
        End If

        Dim Utente_id As Integer = Integer.Parse(Request.Cookies("SicilyRentCar")("idUtente"))

        Dim risposta As String = Multe.GenProtVuoti(Utente_id, CInt(txtNumProt.Text))

        If risposta <> "" Then
            Libreria.genUserMsgBox(Page, "Sono stati geneati protocolli vuoti " & risposta)
        Else
            Libreria.genUserMsgBox(Page, "Si è verificato un errore nella generazione dei protocolli vuoti")
        End If

        txtNumProt.Text = ""
    End Sub

    Protected Sub linkReportStandard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles linkReportStandard.Click
        'si impostano i dati di tutti i parametri nell'apposita classe per il salvataggio
        Dim QueryReport As String = WhereQuery() 'si recupera la query in base ai campi selezionati
        If QueryReport = "" Then Exit Sub

        Dim QueryCompleta As String = "SELECT dbo.multe.Prot, dbo.multe.DataInserimento, dbo.multe.Targa, " & _
        "dbo.multe_enti.Ente, dbo.multe.EnteComune, dbo.multe.DataNotifica, dbo.multe.NumVerbale, " & _
        "dbo.multe.ContrattoNolo, dbo.CONDUCENTI.Nominativo, dbo.multe_casistiche.Casistica, " & _
        "dbo.multe.IncassatoYesNo, dbo.multe.FatturatoYesNo, dbo.multe.LocatoreNumFatt, dbo.operatori.username " & _
        "FROM dbo.multe WITH (NOLOCK) LEFT OUTER JOIN dbo.operatori WITH (NOLOCK) ON " & _
        "dbo.multe.UtenteID = dbo.operatori.id LEFT OUTER JOIN dbo.multe_casistiche WITH (NOLOCK) " & _
        "ON dbo.multe.CasisticaID = dbo.multe_casistiche.ID LEFT OUTER JOIN dbo.CONDUCENTI WITH (NOLOCK) " & _
        "ON dbo.multe.IDConducente = dbo.CONDUCENTI.ID_CONDUCENTE LEFT OUTER JOIN dbo.multe_enti WITH (NOLOCK) " & _
        "ON dbo.multe.EnteID = dbo.multe_enti.ID" & QueryReport


        'Trace.Write("----------------------- query filtro: " & QueryReport)
        Dim myFiltro As FiltroReports = New FiltroReports
        With myFiltro
            .DaId = txtDaID.Text
            .AId = txtAID.Text
            .DaProt = txtDaProt.Text
            .AProt = txtAProt.Text
            .DaAnno = txtDaAnno.Text
            .AAnno = txtAAnno.Text
            .DaDataInserimento = txtDaDataInser.Text
            .ADataInserimento = txtADataInser.Text
            .DaNumVerbale = txtDaNumVerb.Text
            .ANumVerbale = txtANumVerb.Text
            .DaDataNotifica = txtDaDataNotifica.Text
            .ADataNotifica = txtADataNotifica.Text
            .DaTarga = txtDaTarga.Text
            .ATarga = txtATarga.Text
            .DaDataInfrazione = txtDaDataInfraz.Text
            .ADataInfrazione = txtADataInfraz.Text
            .DaOraInfrazione = txtDaOraInfraz.Text
            .AOraInfrazione = txtAOraInfraz.Text
            If DropStatoMulta.SelectedValue > 0 Then
                .StatoMulta = DropStatoMulta.SelectedItem.ToString
            Else
                .StatoMulta = ""
            End If
            If DropCasistiche.SelectedIndex > 0 Then
                .Casistica = DropCasistiche.SelectedItem.ToString
            Else
                .Casistica = ""
            End If
            If DropIncassate.SelectedValue > -1 Then
                .Incassate = DropIncassate.SelectedItem.ToString
            Else
                .Incassate = ""
            End If

            If DropTipoMancInc.SelectedValue > 0 Then
                .MotivoMancatoIncasso = DropTipoMancInc.SelectedItem.ToString
            Else
                .MotivoMancatoIncasso = ""
            End If




            If DropFatturate.SelectedValue > -1 Then
                .Fatturate = DropFatturate.SelectedItem.ToString
            Else
                .Fatturate = ""
            End If
            If DropRicorso.SelectedValue > -1 Then
                .Ricorso = DropRicorso.SelectedItem.ToString
            Else
                .Ricorso = ""
            End If
            If DropComunCliente.SelectedValue > -1 Then
                .ComunCliente = DropComunCliente.SelectedItem.ToString
            Else
                .ComunCliente = ""
            End If
            ' nascosto 13.05.2022
            If DropFaxFatto.SelectedValue > -1 Then
                .Fax = DropFaxFatto.SelectedItem.ToString
            Else
                .Fax = ""
            End If

            'Aggiunto al posto di dropfax 13.05.2022 da verificare
            'If ddl_provenienza.SelectedValue > 0 Then
            '    .provenienzaid = ddl_provenienza.SelectedItem.ToString
            'Else
            '    .provenienzaid = ""
            'End If



            If DropFattLocat.SelectedValue > -1 Then
                .ConFattLocatori = DropFattLocat.SelectedItem.ToString
            Else
                .ConFattLocatori = ""
            End If
            If DropRimborsate.SelectedValue > -1 Then
                .Rimborsate = DropRimborsate.SelectedItem.ToString
            Else
                .Rimborsate = ""
            End If
            If DropPagate.SelectedValue > -1 Then
                .Pagate = DropPagate.SelectedItem.ToString
            Else
                .Pagate = ""
            End If
            If DropOperatori.SelectedValue > 0 Then
                .Operatori = DropOperatori.SelectedItem.ToString
            Else
                .Operatori = ""
            End If
            .Query = QueryCompleta
        End With

        Dim idRecord As Integer = myFiltro.InsertFiltroReport()

        If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            Dim url_print As String = "/stampe/multe/ReportStandard.aspx?orientamento=orizzontale&margin_top=30&IdFiltroReport=" & idRecord & "&TipoStampa=0&header_html=/stampe/multe/header_ReportStandard.aspx?Titolo=" & idRecord
            Dim mio_random As String = Format((New Random).Next(), "0000000000")
            Session("url_print") = url_print
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx?a=" & mio_random & "','')", True)
            End If
        End If
    End Sub

    Protected Sub LinkReportAltriDati_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkReportAltriDati.Click
        'si impostano i dati di tutti i parametri nell'apposita classe per il salvataggio
        Dim QueryReport As String = WhereQuery() 'si recupera la query in base ai campi selezionati
        If QueryReport = "" Then Exit Sub

        Dim QueryCompleta As String = "SELECT dbo.multe.Prot, dbo.multe.DataInserimento, dbo.multe.Targa, " & _
        "dbo.multe.DataNotifica, dbo.multe.NumVerbale, dbo.multe.ContrattoNolo, dbo.CONDUCENTI.Nominativo, " & _
        "dbo.multe_casistiche.Casistica, dbo.multe_casistiche.CostoFee, dbo.multe.RicorsoYesNo, " & _
        "dbo.multe.ComunicazClienteYesNo, dbo.multe.RicorsoDataFax, dbo.multe.IncassatoYesNo, " & _
        "dbo.multe.NumTentativiIncassi, dbo.multe_TipoMancIncMulte.DescrMancIncasso, 'ft.' + CONVERT(varchar(10), " & _
        "dbo.multe.LocatoreNumFatt) + ' del ' + CONVERT(varchar(20), dbo.multe.LocatoreDataFatt, 105) " & _
        "AS FattLocatore, dbo.operatori.username, dbo.View_TotaleIncPerMulta.TotIncasso, " & _
        "SUBSTRING(dbo.View_UltFatturaMulte.fattura, 12, 30) AS UltFattura, dbo.multe.RimborsatoImporto, " & _
        "dbo.multe.PagatoImporto FROM dbo.multe WITH (NOLOCK) LEFT OUTER JOIN " & _
        "dbo.multe_TipoMancIncMulte ON dbo.multe.MotivoMancInc = dbo.multe_TipoMancIncMulte.Id LEFT OUTER JOIN " & _
        "dbo.View_UltFatturaMulte ON dbo.multe.ID = dbo.View_UltFatturaMulte.id_riferimento LEFT OUTER JOIN " & _
        "dbo.View_TotaleIncPerMulta ON dbo.multe.ID = dbo.View_TotaleIncPerMulta.N_MULTA_RIF LEFT OUTER JOIN " & _
        "dbo.operatori WITH (NOLOCK) ON dbo.multe.UtenteID = dbo.operatori.id LEFT OUTER JOIN " & _
        "dbo.multe_casistiche WITH (NOLOCK) ON dbo.multe.CasisticaID = dbo.multe_casistiche.ID LEFT OUTER JOIN " & _
        "dbo.CONDUCENTI WITH (NOLOCK) ON dbo.multe.IDConducente = dbo.CONDUCENTI.ID_CONDUCENTE" & QueryReport

        'Trace.Write("----------------------- query filtro: " & QueryReport)
        Dim myFiltro As FiltroReports = New FiltroReports
        With myFiltro
            .DaId = txtDaID.Text
            .AId = txtAID.Text
            .DaProt = txtDaProt.Text
            .AProt = txtAProt.Text
            .DaAnno = txtDaAnno.Text
            .AAnno = txtAAnno.Text
            .DaDataInserimento = txtDaDataInser.Text
            .ADataInserimento = txtADataInser.Text
            .DaNumVerbale = txtDaNumVerb.Text
            .ANumVerbale = txtANumVerb.Text
            .DaDataNotifica = txtDaDataNotifica.Text
            .ADataNotifica = txtADataNotifica.Text
            .DaTarga = txtDaTarga.Text
            .ATarga = txtATarga.Text
            .DaDataInfrazione = txtDaDataInfraz.Text
            .ADataInfrazione = txtADataInfraz.Text
            .DaOraInfrazione = txtDaOraInfraz.Text
            .AOraInfrazione = txtAOraInfraz.Text
            If DropStatoMulta.SelectedValue > 0 Then
                .StatoMulta = DropStatoMulta.SelectedItem.ToString
            Else
                .StatoMulta = ""
            End If
            If DropCasistiche.SelectedIndex > 0 Then
                .Casistica = DropCasistiche.SelectedItem.ToString
            Else
                .Casistica = ""
            End If
            If DropIncassate.SelectedValue > -1 Then
                .Incassate = DropIncassate.SelectedItem.ToString
            Else
                .Incassate = ""
            End If
            If DropTipoMancInc.SelectedValue > 0 Then
                .MotivoMancatoIncasso = DropTipoMancInc.SelectedItem.ToString
            Else
                .MotivoMancatoIncasso = ""
            End If
            If DropFatturate.SelectedValue > -1 Then
                .Fatturate = DropFatturate.SelectedItem.ToString
            Else
                .Fatturate = ""
            End If
            If DropRicorso.SelectedValue > -1 Then
                .Ricorso = DropRicorso.SelectedItem.ToString
            Else
                .Ricorso = ""
            End If
            If DropComunCliente.SelectedValue > -1 Then
                .ComunCliente = DropComunCliente.SelectedItem.ToString
            Else
                .ComunCliente = ""
            End If
            If DropFaxFatto.SelectedValue > -1 Then
                .Fax = DropFaxFatto.SelectedItem.ToString
            Else
                .Fax = ""
            End If
            If DropFattLocat.SelectedValue > -1 Then
                .ConFattLocatori = DropFattLocat.SelectedItem.ToString
            Else
                .ConFattLocatori = ""
            End If
            If DropRimborsate.SelectedValue > -1 Then
                .Rimborsate = DropRimborsate.SelectedItem.ToString
            Else
                .Rimborsate = ""
            End If
            If DropPagate.SelectedValue > -1 Then
                .Pagate = DropPagate.SelectedItem.ToString
            Else
                .Pagate = ""
            End If
            If DropOperatori.SelectedValue > 0 Then
                .Operatori = DropOperatori.SelectedItem.ToString
            Else
                .Operatori = ""
            End If
            .Query = QueryCompleta
        End With

        Dim idRecord As Integer = myFiltro.InsertFiltroReport()

        If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            Dim url_print As String = "/stampe/multe/ReportAltriDati.aspx?orientamento=orizzontale&margin_top=30&IdFiltroReport=" & idRecord & "&TipoStampa=0&header_html=/stampe/multe/header_ReportAltriDati.aspx?Titolo=" & idRecord
            Dim mio_random As String = Format((New Random).Next(), "0000000000")
            Session("url_print") = url_print
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx?a=" & mio_random & "','')", True)
            End If
        End If
    End Sub
End Class
