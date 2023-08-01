Imports System
Imports System.Web
Imports System.Web.UI
Imports System.IO
Imports System.Collections.Generic

Public Class veicoli_intestazione_bollo
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_ContoCorrente As String
    Protected m_IntestatoA As String
    Protected m_Denominazione As String
    Protected m_PartitaIVA As String
    Protected m_Indirizzo As String
    Protected m_Comune As String
    Protected m_Provincia As String
    Protected m_CAP As String
    Protected m_tariffa As Integer
    Protected m_predefinito As Boolean

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property ContoCorrente() As String
        Get
            Return m_ContoCorrente
        End Get
        Set(ByVal value As String)
            m_ContoCorrente = value
        End Set
    End Property
    Public Property IntestatoA() As String
        Get
            Return m_IntestatoA
        End Get
        Set(ByVal value As String)
            m_IntestatoA = value
        End Set
    End Property
    Public Property Denominazione() As String
        Get
            Return m_Denominazione
        End Get
        Set(ByVal value As String)
            m_Denominazione = value
        End Set
    End Property
    Public Property PartitaIVA() As String
        Get
            Return m_PartitaIVA
        End Get
        Set(ByVal value As String)
            m_PartitaIVA = value
        End Set
    End Property
    Public Property Indirizzo() As String
        Get
            Return m_Indirizzo
        End Get
        Set(ByVal value As String)
            m_Indirizzo = value
        End Set
    End Property
    Public Property Comune() As String
        Get
            Return m_Comune
        End Get
        Set(ByVal value As String)
            m_Comune = value
        End Set
    End Property
    Public Property Provincia() As String
        Get
            Return m_Provincia
        End Get
        Set(ByVal value As String)
            m_Provincia = value
        End Set
    End Property
    Public Property CAP() As String
        Get
            Return m_CAP
        End Get
        Set(ByVal value As String)
            m_CAP = value
        End Set
    End Property
    Public Property tariffa() As Integer
        Get
            Return m_tariffa
        End Get
        Set(ByVal value As Integer)
            m_tariffa = value
        End Set
    End Property
    Public Property predefinito() As Boolean
        Get
            Return m_predefinito
        End Get
        Set(ByVal value As Boolean)
            m_predefinito = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional RecuperaId As Boolean = False) As Integer
        SalvaRecord = -1

        Dim sqlStr As String = "INSERT INTO [veicoli_intestazione_bollo] (ContoCorrente,IntestatoA,Denominazione,PartitaIVA,Indirizzo,Comune,Provincia,CAP,tariffa,predefinito)" & _
            " VALUES (@ContoCorrente,@IntestatoA,@Denominazione,@PartitaIVA,@Indirizzo,@Comune,@Provincia,@CAP,@tariffa,0)"
        HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@ContoCorrente", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(ContoCorrente, 10))
                addParametro(Cmd, "@IntestatoA", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(IntestatoA, 50))
                addParametro(Cmd, "@Denominazione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(Denominazione, 50))
                addParametro(Cmd, "@PartitaIVA", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(PartitaIVA, 20))
                addParametro(Cmd, "@Indirizzo", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(Indirizzo, 50))
                addParametro(Cmd, "@Comune", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(Comune, 50))
                addParametro(Cmd, "@Provincia", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(Provincia, 2))
                addParametro(Cmd, "@CAP", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(CAP, 5))
                addParametro(Cmd, "@predefinito", System.Data.SqlDbType.Bit, predefinito)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            If RecuperaId Then
                sqlStr = "SELECT @@IDENTITY FROM [veicoli_intestazione_bollo]"
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    SalvaRecord = Cmd.ExecuteScalar
                End Using
            End If

        End Using
    End Function

    Public Function AggiornaRecord() As Boolean
        AggiornaRecord = False

        Dim sqlStr As String = "UPDATE [veicoli_intestazione_bollo] SET" & _
            " ContoCorrente = @ContoCorrente," & _
            " IntestatoA = @IntestatoA," & _
            " Denominazione = @Denominazione," & _
            " PartitaIVA = @PartitaIVA," & _
            " Indirizzo = @Indirizzo," & _
            " Comune = @Comune," & _
            " Provincia = @Provincia," & _
            " CAP = @CAP," & _
            " tariffa = @tariffa," & _
            " predefinito = @predefinito" & _
            " WHERE id = @id"

        HttpContext.Current.Trace.Write(sqlStr)

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@ContoCorrente", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(ContoCorrente, 10))
                addParametro(Cmd, "@IntestatoA", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(IntestatoA, 50))
                addParametro(Cmd, "@Denominazione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(Denominazione, 50))
                addParametro(Cmd, "@PartitaIVA", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(PartitaIVA, 20))
                addParametro(Cmd, "@Indirizzo", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(Indirizzo, 50))
                addParametro(Cmd, "@Comune", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(Comune, 50))
                addParametro(Cmd, "@Provincia", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(Provincia, 2))
                addParametro(Cmd, "@CAP", System.Data.SqlDbType.NVarChar, Libreria.TrimSicuro(CAP, 5))
                addParametro(Cmd, "@tariffa", System.Data.SqlDbType.Int, tariffa)
                addParametro(Cmd, "@predefinito", System.Data.SqlDbType.Bit, predefinito)

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    Private Shared Function FillRecord(Rs As System.Data.SqlClient.SqlDataReader) As veicoli_intestazione_bollo
        Dim mio_record As veicoli_intestazione_bollo = New veicoli_intestazione_bollo
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .ContoCorrente = getValueOrNohing(Rs("ContoCorrente"))
            .IntestatoA = getValueOrNohing(Rs("IntestatoA"))
            .Denominazione = getValueOrNohing(Rs("Denominazione"))
            .PartitaIVA = getValueOrNohing(Rs("PartitaIVA"))
            .Indirizzo = getValueOrNohing(Rs("Indirizzo"))
            .Comune = getValueOrNohing(Rs("Comune"))
            .Provincia = getValueOrNohing(Rs("Provincia"))
            .CAP = getValueOrNohing(Rs("CAP"))
            .tariffa = getValueOrNohing(Rs("tariffa"))
            .predefinito = getValueOrNohing(Rs("predefinito"))
        End With
        Return mio_record
    End Function

    Public Shared Function RecuperaRecordDaId(id_record As Integer) As veicoli_intestazione_bollo
        RecuperaRecordDaId = Nothing
        Dim sqlStr As String = "SELECT * FROM veicoli_intestazione_bollo WHERE Id = " & id_record
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader()
                    If Rs.Read() Then
                        Return FillRecord(Rs)
                    End If
                End Using
            End Using
        End Using
    End Function

    Public Shared Function SetDefault(id_record As Integer) As Boolean
        SetDefault = False
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dim sqlStr As String = "UPDATE [veicoli_intestazione_bollo] SET [predefinito] = 0"
            Dbc.Open()
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteNonQuery()
            End Using
            sqlStr = "UPDATE [veicoli_intestazione_bollo] SET [predefinito] = 1 WHERE id = " & id_record
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Cmd.ExecuteNonQuery()
            End Using
            SetDefault = True
        End Using
    End Function

    Public Shared Function Elimina(id_record As Integer) As Boolean
        Elimina = False
        Dim sqlStr As String = "DELETE FROM [veicoli_intestazione_bollo] WHERE [id] = " & id_record
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Return (Cmd.ExecuteNonQuery() > 1)
            End Using
        End Using
    End Function
End Class

Partial Class StampaBolli
    Inherits System.Web.UI.Page

    Private Enum DivVisibile
        Nessuno = 0
        ElencoIntestazione = 1
        EditIntestazione = 2
        Bollettino = 4
    End Enum

    Private Sub Visibilita(Valore As DivVisibile)
        DivElenco.Visible = Valore And DivVisibile.ElencoIntestazione
        DivEditIntestazione.Visible = Valore And DivVisibile.EditIntestazione
        DivDatiBollettino.Visible = Valore And DivVisibile.Bollettino
    End Sub

    'Public Shared Function TestStampa() As MemoryStream
    '    Dim mio_bollettino As BolloAuto = New BolloAuto
    '    With mio_bollettino
    '        .ContoCorrente = "12345678"
    '        .Targa = "AA1234HH"
    '        .Importo = "123456,56"
    '        .ImportoTassa = "111115,557"
    '        .Interessi = "111115,557"
    '        .Sanzioni = "111115,557"

    '        .IntestatoA = "Motorizzazione Regione Sicilia via Veneziano 15 (90100)"
    '        .ScadenzaAnno = 2013
    '        .ScadenzaMese = 5
    '        .MesiValidita = 6
    '        .Riduzione = TipoRiduzione.GPLEsclusivo
    '        .CodiceFiscale = "CNCCGR66S03L959Z"
    '        .Categoria = TipoCategoria.Autoveicolo
    '        .EseguitoDa = "Cancellieri Calogero Antonio"
    '        .Residente = "Via Nicolò Palmeri 83"
    '        .Provincia = "CL"
    '        .Comune = "Vallelunga Pratameno"
    '        .CAP = "93010"
    '    End With

    '    Dim listaBolli As List(Of BolloAuto) = New List(Of BolloAuto)

    '    listaBolli.Add(mio_bollettino)
    '    listaBolli.Add(mio_bollettino)
    '    listaBolli.Add(mio_bollettino)

    '    Return StampaBolloAuto.GeneraStampa(listaBolli)
    'End Function

    Protected Sub InitCampiBollo()
        txtImportoTassa.Text = "0.00"
        txtImporto.Text = "0.00"
        txtInteressi.Text = "0.00"
        txtSanzioni.Text = "0.00"
        txtScadenzaAnno.Text = Year(Now())

        txtTarga.Text = ""
        DropDownEuro.SelectedIndex = 5
        DropDownMesiValidita.SelectedIndex = 0
        txtKW.Text = "0"

        DropDownScadenzaMese.SelectedIndex = 0
        radioCategoria.SelectedValue = 1
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            InitCampiBollo()

            Visibilita(DivVisibile.ElencoIntestazione)
        End If

        'Trace.Write("--------------------------------- Inizio")

        'Try
        '    ' genero il documento pdf a partire dallo stream html
        '    Dim ms As MemoryStream = TestStampa()

        '    If Not ms Is Nothing Then
        '        Trace.Write("--------------------------------- SI")
        '        Response.Buffer = True
        '        Response.Clear()
        '        Response.ContentType = "application/pdf"
        '        Response.AddHeader("content-disposition", "inline; filename=file.pdf")

        '        ' forse se commento questa riga è meglio...
        '        Response.AddHeader("Content-Length", ms.GetBuffer().Length)

        '        Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
        '        Response.OutputStream.Flush()
        '        Response.End()
        '    Else
        '        Trace.Write("--------------------------------- NO")
        '    End If
        'Catch ex As Exception
        '    Trace.Write("Errore:" & ex.Message)
        'End Try
    End Sub

    Protected Sub PulisciIntestazione()
        lb_id_Intestazione.Text = 0
        txtEdIntestatoA.Text = ""
        txtEdContoCorrente.Text = ""
        txtEdEseguitoDa.Text = ""
        txtEdPartitaIVA.Text = ""
        txtEdIndirizzo.Text = ""
        txtEdComune.Text = ""
        txtEdProvincia.Text = ""
        txtEdCAP.Text = ""
        rbEdTariffa.SelectedValue = 0
        lb_predefinito.Text = False
    End Sub

    Protected Function getIntestazione() As veicoli_intestazione_bollo
        Dim mio_record As veicoli_intestazione_bollo = New veicoli_intestazione_bollo
        With mio_record
            .id = Integer.Parse(lb_id_Intestazione.Text)
            .IntestatoA = txtEdIntestatoA.Text
            .ContoCorrente = txtEdContoCorrente.Text
            .Denominazione = txtEdEseguitoDa.Text
            .PartitaIVA = txtEdPartitaIVA.Text
            .Indirizzo = txtEdIndirizzo.Text
            .Comune = txtEdComune.Text
            .Provincia = txtEdProvincia.Text
            .CAP = txtEdCAP.Text
            .tariffa = rbEdTariffa.SelectedValue
            .predefinito = Boolean.Parse(lb_predefinito.Text)
        End With

        Return mio_record
    End Function

    Protected Sub FillIntestazione(mio_record As veicoli_intestazione_bollo)
        With mio_record
            lb_id_Intestazione.Text = .id
            txtEdIntestatoA.Text = .IntestatoA
            txtEdContoCorrente.Text = .ContoCorrente
            txtEdEseguitoDa.Text = .Denominazione
            txtEdPartitaIVA.Text = .PartitaIVA
            txtEdIndirizzo.Text = .Indirizzo
            txtEdComune.Text = .Comune
            txtEdProvincia.Text = .Provincia
            txtEdCAP.Text = .CAP
            rbEdTariffa.SelectedValue = .tariffa
            lb_predefinito.Text = .predefinito
        End With
    End Sub

    Protected Sub PulisciBollo()
        txtIntestatoA.Text = ""
        txtContoCorrente.Text = ""
        txtEseguitoDa.Text = ""
        txtPartitaIVA.Text = ""
        txtIndirizzo.Text = ""
        txtComune.Text = ""
        txtProvincia.Text = ""
        txtCAP.Text = ""
        rbTariffa.SelectedValue = 0
    End Sub

    Protected Sub FillBollo(mio_record As veicoli_intestazione_bollo)
        With mio_record
            txtIntestatoA.Text = .IntestatoA
            txtContoCorrente.Text = .ContoCorrente
            txtEseguitoDa.Text = .Denominazione
            txtPartitaIVA.Text = .PartitaIVA
            txtIndirizzo.Text = .Indirizzo
            txtComune.Text = .Comune
            txtProvincia.Text = .Provincia
            txtCAP.Text = .CAP
            rbTariffa.SelectedValue = .tariffa
        End With
    End Sub

    Protected Sub ListIntestazioni_ItemCommand(sender As Object, e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListIntestazioni.ItemCommand
        If e.CommandName = "Bollo" Then
            Dim lb_id As Label = e.Item.FindControl("lb_id")

            Dim mio_record As veicoli_intestazione_bollo = veicoli_intestazione_bollo.RecuperaRecordDaId(Integer.Parse(lb_id.Text))

            FillBollo(mio_record)

            InitCampiBollo()

            veicoli_intestazione_bollo.SetDefault(Integer.Parse(lb_id.Text))

            Visibilita(DivVisibile.Bollettino)
        ElseIf e.CommandName = "Lente" Then
            Dim lb_id As Label = e.Item.FindControl("lb_id")

            Dim mio_record As veicoli_intestazione_bollo = veicoli_intestazione_bollo.RecuperaRecordDaId(Integer.Parse(lb_id.Text))

            FillIntestazione(mio_record)

            Visibilita(DivVisibile.EditIntestazione)
        ElseIf e.CommandName = "Elimina" Then
            Dim lb_id As Label = e.Item.FindControl("lb_id")

            veicoli_intestazione_bollo.Elimina(Integer.Parse(lb_id.Text))

            ListIntestazioni.DataBind()
        End If
    End Sub

    Protected Sub btnChiudiEdit_Click(sender As Object, e As System.EventArgs) Handles btnChiudiEdit.Click
        Visibilita(DivVisibile.ElencoIntestazione)
    End Sub

    Protected Sub btnNuovaIntestazione_Click(sender As Object, e As System.EventArgs) Handles btnNuovaIntestazione.Click
        PulisciIntestazione()
        Visibilita(DivVisibile.EditIntestazione)
    End Sub

    Protected Sub btnSalvaIntestazione_Click(sender As Object, e As System.EventArgs) Handles btnSalvaIntestazione.Click
        Dim mio_record As veicoli_intestazione_bollo = getIntestazione()
        If mio_record.id = 0 Then ' è un nuovo record!
            mio_record.SalvaRecord()
        Else
            mio_record.AggiornaRecord()
        End If

        ListIntestazioni.DataBind()
        Visibilita(DivVisibile.ElencoIntestazione)
    End Sub

    Protected Sub btnChiudiElenco_Click(sender As Object, e As System.EventArgs) Handles btnChiudiElenco.Click
        ' dovrei chiudere il form...
        Visibilita(DivVisibile.Nessuno)
    End Sub

    Protected Sub btnChiudiStampa_Click(sender As Object, e As System.EventArgs) Handles btnChiudiStampa.Click
        Visibilita(DivVisibile.ElencoIntestazione)
    End Sub

    Protected Function CalcolaBollo(Euro As Integer, KW As Integer, Tariffa As Integer, Mesi As Integer) As Double
        If Mesi = 12 Then
            Select Case Tariffa
                Case 1
                    If KW > 100 Then
                        Select Case Euro
                            Case 0
                                CalcolaBollo = 270 + (KW - 100) * 4.0499999999999998
                            Case 1
                                CalcolaBollo = 261 + (KW - 100) * 3.9199999999999999
                            Case 2
                                CalcolaBollo = 252 + (KW - 100) * 3.7799999999999998
                            Case 3
                                CalcolaBollo = 243 + (KW - 100) * 3.6499999999999999
                            Case 4, 5, 6
                                CalcolaBollo = 232 + (KW - 100) * 3.48
                        End Select
                    Else
                        Select Case Euro
                            Case 0
                                CalcolaBollo = KW * 2.7000000000000002
                            Case 1
                                CalcolaBollo = KW * 2.6099999999999999
                            Case 2
                                CalcolaBollo = KW * 2.52
                            Case 3
                                CalcolaBollo = KW * 2.4300000000000002
                            Case 4, 5, 6
                                CalcolaBollo = KW * 2.3199999999999998
                        End Select
                    End If
                Case Else
                    If KW > 100 Then
                        Select Case Euro
                            Case 0
                                CalcolaBollo = 300 + (KW - 100) * 4.5
                            Case 1
                                CalcolaBollo = 290 + (KW - 100) * 4.3499999999999996
                            Case 2
                                CalcolaBollo = 280 + (KW - 100) * 4.2000000000000002
                            Case 3
                                CalcolaBollo = 270 + (KW - 100) * 4.0499999999999998
                            Case 4, 5, 6
                                CalcolaBollo = 258 + (KW - 100) * 3.8700000000000001
                        End Select
                    Else
                        Select Case Euro
                            Case 0
                                CalcolaBollo = KW * 3.0
                            Case 1
                                CalcolaBollo = KW * 2.8999999999999999
                            Case 2
                                CalcolaBollo = KW * 2.7999999999999998
                            Case 3
                                CalcolaBollo = KW * 2.7000000000000002
                            Case 4, 5, 6
                                CalcolaBollo = KW * 2.5800000000000001
                        End Select
                    End If
            End Select

            Return CalcolaBollo
        End If
        Select Case Tariffa
            Case 1
                If KW > 100 Then
                    Select Case Euro
                        Case 0
                            CalcolaBollo = 278 + (KW - 100) * 4.1299999999999999
                        Case 1
                            CalcolaBollo = 269 + (KW - 100) * 4.0300000000000002
                        Case 2
                            CalcolaBollo = 259 + (KW - 100) * 3.8999999999999999
                        Case 3
                            CalcolaBollo = 250 + (KW - 100) * 3.75
                        Case 4, 5, 6
                            CalcolaBollo = 239 + (KW - 100) * 3.5899999999999999
                    End Select
                Else
                    Select Case Euro
                        Case 0
                            CalcolaBollo = KW * 2.7799999999999998
                        Case 1
                            CalcolaBollo = KW * 2.6899999999999999
                        Case 2
                            CalcolaBollo = KW * 2.5899999999999999
                        Case 3
                            CalcolaBollo = KW * 2.5
                        Case 4, 5, 6
                            CalcolaBollo = KW * 2.3900000000000001
                    End Select
                End If
            Case Else
                If KW > 100 Then
                    Select Case Euro
                        Case 0
                            CalcolaBollo = 309 + (KW - 100) * 4.5899999999999999
                        Case 1
                            CalcolaBollo = 299 + (KW - 100) * 4.4800000000000004
                        Case 2
                            CalcolaBollo = 288 + (KW - 100) * 4.3300000000000001
                        Case 3
                            CalcolaBollo = 278 + (KW - 100) * 4.1699999999999999
                        Case 4, 5, 6
                            CalcolaBollo = 266 + (KW - 100) * 3.9900000000000002
                    End Select
                Else
                    Select Case Euro
                        Case 0
                            CalcolaBollo = KW * 3.0899999999999999
                        Case 1
                            CalcolaBollo = KW * 2.9900000000000002
                        Case 2
                            CalcolaBollo = KW * 2.8799999999999999
                        Case 3
                            CalcolaBollo = KW * 2.7799999999999998
                        Case 4, 5, 6
                            CalcolaBollo = KW * 2.6600000000000001
                    End Select
                End If
        End Select

        Return CalcolaBollo / 12 * Mesi
    End Function

    Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
        Dim mio_bollettino As BolloAuto = New BolloAuto
        With mio_bollettino
            .ContoCorrente = txtContoCorrente.Text
            .Targa = txtTarga.Text
            .Importo = txtImporto.Text.Replace(".", ",")
            .ImportoTassa = txtImportoTassa.Text.Replace(".", ",")
            .Interessi = txtInteressi.Text.Replace(".", ",")
            .Sanzioni = txtSanzioni.Text.Replace(".", ",")

            .IntestatoA = txtIntestatoA.Text
            .ScadenzaAnno = txtScadenzaAnno.Text
            .ScadenzaMese = DropDownScadenzaMese.SelectedValue
            .MesiValidita = DropDownMesiValidita.SelectedValue
            .Riduzione = DropDownRiduzione.SelectedIndex
            .CodiceFiscale = txtPartitaIVA.Text
            .Categoria = radioCategoria.SelectedValue
            .EseguitoDa = txtEseguitoDa.Text
            .Residente = txtIndirizzo.Text
            .Provincia = txtProvincia.Text
            .Comune = txtComune.Text
            .CAP = txtCAP.Text
        End With

        Session("bollettino_bollo_auto") = mio_bollettino

        Dim mio_random As String = Format((New Random).Next(), "0000000000")
        If (Not ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('stampe/StampaBolloAuto.aspx?a=" & mio_random & "','')", True)
        End If
    End Sub
End Class

