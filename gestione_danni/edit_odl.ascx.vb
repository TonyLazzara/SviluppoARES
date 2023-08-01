
Public Enum enum_tipologia_movimenti
    Non_valido = 0
    Immatricolazione = 1
    Immissione_in_parco = 2
    Noleggio = 3
    Fermo_Tecnico = 4
    Rifornimento = 7
    Furto = 8
    Movimento_interno = 9
    Riparazione = 10
    Lavaggio = 11
End Enum

Public Class movimenti_targa
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_num_riferimento As Integer?
    Protected m_num_crv_contratto As Integer?
    Protected m_id_veicolo As Integer?
    Protected m_id_tipo_movimento As Integer?
    Protected m_data_uscita As DateTime?
    Protected m_id_stazione_uscita As Integer?
    Protected m_km_uscita As Integer?
    Protected m_serbatoio_uscita As Integer?
    Protected m_data_rientro As DateTime?
    Protected m_id_stazione_rientro As Integer?
    Protected m_km_rientro As Integer?

    Protected m_data_presunto_rientro As DateTime?
    Protected m_id_stazione_presunto_rientro As Integer?

    Protected m_serbatoio_rientro As Integer?
    Protected m_id_operatore As Integer?
    Protected m_data_registrazione As DateTime?
    Protected m_id_operatore_rientro As Integer?
    Protected m_data_registrazione_rientro As DateTime?
    Protected m_movimento_attivo As Boolean?

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property num_riferimento() As Integer?
        Get
            Return m_num_riferimento
        End Get
        Set(ByVal value As Integer?)
            m_num_riferimento = value
        End Set
    End Property
    Public Property num_crv_contratto() As Integer?
        Get
            Return m_num_crv_contratto
        End Get
        Set(ByVal value As Integer?)
            m_num_crv_contratto = value
        End Set
    End Property
    Public Property id_veicolo() As Integer?
        Get
            Return m_id_veicolo
        End Get
        Set(ByVal value As Integer?)
            m_id_veicolo = value
        End Set
    End Property
    Public Property id_tipo_movimento() As Integer?
        Get
            Return m_id_tipo_movimento
        End Get
        Set(ByVal value As Integer?)
            m_id_tipo_movimento = value
        End Set
    End Property
    Public Property data_uscita() As DateTime?
        Get
            Return m_data_uscita
        End Get
        Set(ByVal value As DateTime?)
            m_data_uscita = value
        End Set
    End Property
    Public Property id_stazione_uscita() As Integer?
        Get
            Return m_id_stazione_uscita
        End Get
        Set(ByVal value As Integer?)
            m_id_stazione_uscita = value
        End Set
    End Property
    Public Property km_uscita() As Integer?
        Get
            Return m_km_uscita
        End Get
        Set(ByVal value As Integer?)
            m_km_uscita = value
        End Set
    End Property
    Public Property serbatoio_uscita() As Integer?
        Get
            Return m_serbatoio_uscita
        End Get
        Set(ByVal value As Integer?)
            m_serbatoio_uscita = value
        End Set
    End Property
    Public Property data_rientro() As DateTime?
        Get
            Return m_data_rientro
        End Get
        Set(ByVal value As DateTime?)
            m_data_rientro = value
        End Set
    End Property
    Public Property id_stazione_rientro() As Integer?
        Get
            Return m_id_stazione_rientro
        End Get
        Set(ByVal value As Integer?)
            m_id_stazione_rientro = value
        End Set
    End Property

    Public Property data_presunto_rientro() As DateTime?
        Get
            Return m_data_presunto_rientro
        End Get
        Set(ByVal value As DateTime?)
            m_data_presunto_rientro = value
        End Set
    End Property
    Public Property id_stazione_presunto_rientro() As Integer?
        Get
            Return m_id_stazione_presunto_rientro
        End Get
        Set(ByVal value As Integer?)
            m_id_stazione_presunto_rientro = value
        End Set
    End Property

    Public Property km_rientro() As Integer?
        Get
            Return m_km_rientro
        End Get
        Set(ByVal value As Integer?)
            m_km_rientro = value
        End Set
    End Property
    Public Property serbatoio_rientro() As Integer?
        Get
            Return m_serbatoio_rientro
        End Get
        Set(ByVal value As Integer?)
            m_serbatoio_rientro = value
        End Set
    End Property
    Public ReadOnly Property id_operatore() As Integer?
        Get
            Return m_id_operatore
        End Get
    End Property
    Public ReadOnly Property data_registrazione() As DateTime?
        Get
            Return m_data_registrazione
        End Get
    End Property
    Public ReadOnly Property id_operatore_rientro() As Integer?
        Get
            Return m_id_operatore_rientro
        End Get
    End Property
    Public ReadOnly Property data_registrazione_rientro() As DateTime?
        Get
            Return m_data_registrazione_rientro
        End Get
    End Property
    Public Property movimento_attivo() As Boolean?
        Get
            Return m_movimento_attivo
        End Get
        Set(ByVal value As Boolean?)
            m_movimento_attivo = value
        End Set
    End Property

    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO movimenti_targa (num_riferimento,num_crv_contratto,id_veicolo,id_tipo_movimento,data_uscita,id_stazione_uscita,km_uscita,serbatoio_uscita,data_rientro,id_stazione_rientro,km_rientro,serbatoio_rientro,id_operatore,data_registrazione,id_operatore_rientro,data_registrazione_rientro,movimento_attivo,id_stazione_presunto_rientro, data_presunto_rientro)" & _
            " VALUES (@num_riferimento,@num_crv_contratto,@id_veicolo,@id_tipo_movimento,@data_uscita,@id_stazione_uscita,@km_uscita,@serbatoio_uscita,@data_rientro,@id_stazione_rientro,@km_rientro,@serbatoio_rientro,@id_operatore,@data_registrazione,@id_operatore_rientro,@data_registrazione_rientro,@movimento_attivo, @id_stazione_presunto_rientro,@data_presunto_rientro)"

        Try
            m_data_registrazione = Now
            m_id_operatore = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@num_riferimento", System.Data.SqlDbType.Int, num_riferimento)
                    addParametro(Cmd, "@num_crv_contratto", System.Data.SqlDbType.Int, num_crv_contratto)
                    addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                    addParametro(Cmd, "@id_tipo_movimento", System.Data.SqlDbType.Int, id_tipo_movimento)
                    addParametro(Cmd, "@data_uscita", System.Data.SqlDbType.DateTime, data_uscita)
                    addParametro(Cmd, "@id_stazione_uscita", System.Data.SqlDbType.Int, id_stazione_uscita)
                    addParametro(Cmd, "@km_uscita", System.Data.SqlDbType.Int, km_uscita)
                    addParametro(Cmd, "@serbatoio_uscita", System.Data.SqlDbType.Int, serbatoio_uscita)
                    addParametro(Cmd, "@data_rientro", System.Data.SqlDbType.DateTime, data_rientro)
                    addParametro(Cmd, "@id_stazione_rientro", System.Data.SqlDbType.Int, id_stazione_rientro)
                    addParametro(Cmd, "@km_rientro", System.Data.SqlDbType.Int, km_rientro)
                    addParametro(Cmd, "@serbatoio_rientro", System.Data.SqlDbType.Int, serbatoio_rientro)
                    addParametro(Cmd, "@id_operatore", System.Data.SqlDbType.Int, id_operatore)
                    addParametro(Cmd, "@data_registrazione", System.Data.SqlDbType.DateTime, data_registrazione)
                    addParametro(Cmd, "@id_operatore_rientro", System.Data.SqlDbType.Int, id_operatore_rientro)
                    addParametro(Cmd, "@data_registrazione_rientro", System.Data.SqlDbType.DateTime, data_registrazione_rientro)
                    addParametro(Cmd, "@movimento_attivo", System.Data.SqlDbType.Bit, movimento_attivo)
                    addParametro(Cmd, "@data_presunto_rientro", System.Data.SqlDbType.DateTime, data_presunto_rientro)
                    addParametro(Cmd, "@id_stazione_presunto_rientro", System.Data.SqlDbType.Int, id_stazione_presunto_rientro)

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                End Using

                ' -------------------------------------------------------------------------
                'recupero l'id del nuopvo record comunque...
                sqlStr = "SELECT @@IDENTITY FROM movimenti_targa"

                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    m_id = Cmd.ExecuteScalar
                End Using
            End Using

            Return m_id
        Catch ex As Exception
            HttpContext.Current.Response.Write("error SalvaRecord " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As movimenti_targa
        Dim mio_record As movimenti_targa = New movimenti_targa
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .num_riferimento = getValueOrNohing(Rs("num_riferimento"))
            .num_crv_contratto = getValueOrNohing(Rs("num_crv_contratto"))
            .id_veicolo = getValueOrNohing(Rs("id_veicolo"))
            .id_tipo_movimento = getValueOrNohing(Rs("id_tipo_movimento"))
            .data_uscita = getValueOrNohing(Rs("data_uscita"))
            .id_stazione_uscita = getValueOrNohing(Rs("id_stazione_uscita"))
            .km_uscita = getValueOrNohing(Rs("km_uscita"))
            .serbatoio_uscita = getValueOrNohing(Rs("serbatoio_uscita"))
            .data_rientro = getValueOrNohing(Rs("data_rientro"))
            .id_stazione_rientro = getValueOrNohing(Rs("id_stazione_rientro"))
            .data_presunto_rientro = getValueOrNohing(Rs("data_presunto_rientro"))
            .id_stazione_presunto_rientro = getValueOrNohing(Rs("id_stazione_presunto_rientro"))
            .km_rientro = getValueOrNohing(Rs("km_rientro"))
            .serbatoio_rientro = getValueOrNohing(Rs("serbatoio_rientro"))
            .m_id_operatore = getValueOrNohing(Rs("id_operatore"))
            .m_data_registrazione = getValueOrNohing(Rs("data_registrazione"))
            .m_id_operatore_rientro = getValueOrNohing(Rs("id_operatore_rientro"))
            .m_data_registrazione_rientro = getValueOrNohing(Rs("data_registrazione_rientro"))
            .movimento_attivo = getValueOrNohing(Rs("movimento_attivo"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As movimenti_targa
        Dim mio_record As movimenti_targa = Nothing

        Dim sqlStr As String = "SELECT * FROM movimenti_targa WITH(NOLOCK) WHERE id = " & id_record
        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Using Rs = Cmd.ExecuteReader
                        If Rs.Read Then
                            mio_record = FillRecord(Rs)
                        End If
                    End Using
                End Using
            End Using

            Return mio_record
        Catch ex As Exception
            HttpContext.Current.Response.Write("error getRecordDaId " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Public Function AggiornaRecord() As Boolean
        AggiornaRecord = False

        Dim sqlStr As String = "UPDATE movimenti_targa SET" &
            " num_riferimento = @num_riferimento," &
            " num_crv_contratto = @num_crv_contratto," &
            " id_veicolo = @id_veicolo," &
            " id_tipo_movimento = @id_tipo_movimento," &
            " data_uscita = @data_uscita," &
            " id_stazione_uscita = @id_stazione_uscita," &
            " km_uscita = @km_uscita," &
            " serbatoio_uscita = @serbatoio_uscita," &
            " data_rientro = @data_rientro," &
            " id_stazione_rientro = @id_stazione_rientro," &
            " data_presunto_rientro = @data_presunto_rientro," &
            " id_stazione_presunto_rientro = @id_stazione_presunto_rientro," &
            " km_rientro = @km_rientro," &
            " serbatoio_rientro = @serbatoio_rientro," &
            " id_operatore_rientro = @id_operatore_rientro," &
            " data_registrazione_rientro = @data_registrazione_rientro," &
            " movimento_attivo = @movimento_attivo" &
            " WHERE id = @id"
        Try
            m_data_registrazione_rientro = Now
            m_id_operatore_rientro = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    addParametro(Cmd, "@num_riferimento", System.Data.SqlDbType.Int, num_riferimento)
                    addParametro(Cmd, "@num_crv_contratto", System.Data.SqlDbType.Int, num_crv_contratto)
                    addParametro(Cmd, "@id_veicolo", System.Data.SqlDbType.Int, id_veicolo)
                    addParametro(Cmd, "@id_tipo_movimento", System.Data.SqlDbType.Int, id_tipo_movimento)
                    addParametro(Cmd, "@data_uscita", System.Data.SqlDbType.DateTime, data_uscita)
                    addParametro(Cmd, "@id_stazione_uscita", System.Data.SqlDbType.Int, id_stazione_uscita)
                    addParametro(Cmd, "@km_uscita", System.Data.SqlDbType.Int, km_uscita)
                    addParametro(Cmd, "@serbatoio_uscita", System.Data.SqlDbType.Int, serbatoio_uscita)
                    addParametro(Cmd, "@data_rientro", System.Data.SqlDbType.DateTime, data_rientro)
                    addParametro(Cmd, "@id_stazione_rientro", System.Data.SqlDbType.Int, id_stazione_rientro)
                    addParametro(Cmd, "@data_presunto_rientro", System.Data.SqlDbType.DateTime, data_presunto_rientro)
                    addParametro(Cmd, "@id_stazione_presunto_rientro", System.Data.SqlDbType.Int, id_stazione_presunto_rientro)
                    addParametro(Cmd, "@km_rientro", System.Data.SqlDbType.Int, km_rientro)
                    addParametro(Cmd, "@serbatoio_rientro", System.Data.SqlDbType.Int, serbatoio_rientro)
                    addParametro(Cmd, "@id_operatore_rientro", System.Data.SqlDbType.Int, id_operatore_rientro)
                    addParametro(Cmd, "@data_registrazione_rientro", System.Data.SqlDbType.DateTime, data_registrazione_rientro)
                    addParametro(Cmd, "@movimento_attivo", System.Data.SqlDbType.Bit, movimento_attivo)

                    addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    AggiornaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Response.Write("error AggiornaRecord " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try


    End Function

    Public Function EliminaRecord() As Boolean
        Return EliminaRecord(Me.id)
    End Function

    Public Shared Function EliminaRecord(ByVal id_record As Integer) As Boolean
        EliminaRecord = False

        Dim sqlStr As String = "DELETE FROM movimenti_targa WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord movimenti_targa: " & ex.Message)
        End Try
    End Function
End Class

Public Enum enum_odl_transizioni
    NonValida = 0
    Apertura = 1
    Richiesta = 2
    Autorizzo_ODL = 3
    Non_Autorizzo_ODL = 4
    Autorizzo_Preventivo = 5
    Non_Autorizzo_Preventivo = 6
    Modifica_Preventivo = 7
    Riparato = 8
    Parzialmente_Riparato = 9
    Non_Riparato = 10
    Rientro_Veicolo = 11
    Proseguo_Noleggio = 12
End Enum

Partial Class gestione_danni_edit_odl
    Inherits System.Web.UI.UserControl

    Public Delegate Sub EventoChiusuraForm(ByVal sender As Object, ByVal e As EventArgs)
    Event ChiusuraForm As EventHandler

    Public Delegate Sub EventoAggiornaElencoODL(ByVal sender As Object, ByVal e As EventArgs)
    Event AggiornaElencoODL As EventHandler

    Private Enum DivVisibile
        Nessuno = 0
        Intestazione = 1
        ElencoDanniAperti = 2
        Dettaglio = 4
        Note = 8
        Fornitori = 16
        Drivers = 32
        GestioneCheck = 64
        Edit = Intestazione Or ElencoDanniAperti Or Dettaglio Or Note
    End Enum

    Private Sub Visibilita(ByVal Valore As DivVisibile)
        Trace.Write("Visibilita gestione_danni: " & Valore.ToString & " " & Valore)

        div_targa.Visible = Valore And DivVisibile.Intestazione

        div_dettaglio_danni.Visible = Valore And DivVisibile.ElencoDanniAperti

        div_dettaglio.Visible = Valore And DivVisibile.Dettaglio

        div_nota.Visible = Valore And DivVisibile.Note

        div_fornitori.Visible = Valore And DivVisibile.Fornitori

        div_drivers.Visible = Valore And DivVisibile.Drivers

        div_gestione_checkin.Visible = Valore And DivVisibile.GestioneCheck
    End Sub

    Private Function NuovaElemento(ByVal id_transazione_odl As enum_odl_transizioni) As ListItem

        Dim nuovo_stato_odl() As Integer = {enum_odl_stato.Nuovo,
                                            enum_odl_stato.Attesa_Preventivo,
                                            enum_odl_stato.Attesa_Autorizzazione,
                                            enum_odl_stato.Attesa_Preventivo,
                                            enum_odl_stato.Non_Autorizzato,
                                            enum_odl_stato.Attesa_Riparazione,
                                            enum_odl_stato.Preventivo_Non_Accettato,
                                            enum_odl_stato.Attesa_Preventivo,
                                            enum_odl_stato.Riparato,
                                            enum_odl_stato.Parzialmente_Riparato,
                                            enum_odl_stato.Non_Riparato,
                                            enum_odl_stato.Chiuso,
                                            enum_odl_stato.Chiuso_Con_Continuazione_Noleggio}

        Return New ListItem(id_transazione_odl.ToString.Replace("_", " "), nuovo_stato_odl(id_transazione_odl))
    End Function

    Private Sub DropDown_stato_odl_DataBind(ByVal id_stato_odl As enum_odl_stato, ByVal noleggio_in_corso As Boolean)
        Trace.Write("DropDown_stato_odl_DataBind: " & id_stato_odl.ToString)

        DropDown_stato_odl.Items.Clear()

        DropDown_stato_odl.Items.Add(New ListItem("Seleziona", "0"))

        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneODLAdmin) = "3" Then
            Select Case id_stato_odl
                Case enum_odl_stato.Nuovo
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Apertura))

                Case enum_odl_stato.Attesa_Autorizzazione
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Autorizzo_ODL))
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Non_Autorizzo_ODL))

                Case enum_odl_stato.Attesa_Preventivo
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Autorizzo_Preventivo))
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Non_Autorizzo_Preventivo))

                Case enum_odl_stato.Attesa_Riparazione
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Riparato))
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Parzialmente_Riparato))
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Non_Riparato))

                Case enum_odl_stato.Preventivo_Non_Accettato
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Modifica_Preventivo))
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Rientro_Veicolo))

                Case enum_odl_stato.Non_Autorizzato, enum_odl_stato.Non_Riparato, enum_odl_stato.Parzialmente_Riparato, enum_odl_stato.Riparato
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Rientro_Veicolo))

                Case Else

            End Select
        Else
            Select Case id_stato_odl
                Case enum_odl_stato.Nuovo
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Apertura))
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Richiesta))

                Case enum_odl_stato.Attesa_Autorizzazione
                    'DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Autorizzo_ODL))
                    'DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Non_Autorizzo_ODL))

                Case enum_odl_stato.Attesa_Preventivo
                    'DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Autorizzo_Preventivo))
                    'DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Non_Autorizzo_Preventivo))

                Case enum_odl_stato.Attesa_Riparazione
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Riparato))
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Parzialmente_Riparato))
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Non_Riparato))

                Case enum_odl_stato.Preventivo_Non_Accettato
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Modifica_Preventivo))
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Rientro_Veicolo))

                Case enum_odl_stato.Non_Autorizzato, enum_odl_stato.Non_Riparato, enum_odl_stato.Parzialmente_Riparato, enum_odl_stato.Riparato
                    DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Rientro_Veicolo))

                Case Else

            End Select
        End If

        'If noleggio_in_corso Then
        '    Select Case id_stato_odl
        '        Case enum_odl_stato.Non_Autorizzato, enum_odl_stato.Preventivo_Non_Accettato, enum_odl_stato.Non_Riparato, enum_odl_stato.Parzialmente_Riparato, enum_odl_stato.Riparato
        '            DropDown_stato_odl.Items.Add(NuovaElemento(enum_odl_transizioni.Proseguo_Noleggio))
        '        Case Else

        '    End Select
        'End If
    End Sub

    Protected Function VerificaAutoInContratto(ByVal id_veicolo As Integer) As DatiContratto
        Dim mio_record As DatiContratto = Nothing
        Dim sqlStr As String
        sqlStr = "SELECT TOP 1 * FROM movimenti_targa WITH(NOLOCK)" &
            " WHERE movimento_attivo = 1" &
            " AND id_tipo_movimento = 3" &
            " AND id_veicolo = " & id_veicolo &
            " ORDER BY data_uscita DESC"
        Trace.Write("VerificaAutoInContratto: " & sqlStr)

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Using Rs = Cmd.ExecuteReader
                        If Rs.Read Then
                            Dim num_contratto As String = Rs("num_riferimento") & ""
                            Dim num_crv As String = Rs("num_crv_contratto") & ""
                            If num_crv = "" Then
                                num_crv = 0
                            End If

                            Return DatiContratto.getRecordDaNumContratto(num_contratto, num_crv)
                        Else
                            Return Nothing
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Response.Write("error VerificaAutoInContratto " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try


    End Function

    'Protected Function GestisciAutoInContratto(ByVal id_veicolo As Integer) As Boolean
    '    GestisciAutoInContratto = False

    '    Dim sqlStr As String
    '    sqlStr = "SELECT TOP 1 * FROM movimenti_targa WITH(NOLOCK)" & _
    '        " WHERE movimento_attivo = 1" & _
    '        " AND id_tipo_movimento = 3" & _
    '        " AND id_veicolo = " & id_veicolo & _
    '        " ORDER BY data_uscita DESC"
    '    Trace.Write("GestisciAutoInContratto: " & sqlStr)

    '    Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
    '        Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
    '            Dbc.Open()
    '            Using Rs = Cmd.ExecuteReader
    '                If Rs.Read Then
    '                    GestisciAutoInContratto = True
    '                    tr_noleggio_in_corso.Visible = True

    '                    lb_id_tipo_doc_apertura.Text = tipo_documento.Contratto
    '                    lb_num_documento.Text = Rs("num_riferimento") & ""
    '                    lb_num_crv.Text = Rs("num_crv_contratto") & ""
    '                    If lb_num_crv.Text = "" Then
    '                        lb_num_crv.Text = 0
    '                    End If
    '                Else
    '                    tr_noleggio_in_corso.Visible = False

    '                    lb_id_tipo_doc_apertura.Text = tipo_documento.DuranteODL
    '                    lb_num_documento.Text = 0 ' il numero ODL vine assegnato solo durante il salvataggio del record!
    '                    lb_num_crv.Text = 0
    '                End If
    '            End Using
    '        End Using
    '    End Using
    'End Function

    Protected Sub lb_num_documento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lb_num_documento.Click
        Try
            Dim id_tipo_documento As tipo_documento = tipo_documento.Contratto
            Dim id_documento As Integer = Integer.Parse(lb_num_documento.Text)
            Dim num_crv As Integer = Integer.Parse(lb_num_crv.Text)
            Dim mio_record As DatiContratto = Nothing

            Select Case id_tipo_documento
                Case tipo_documento.Contratto
                    mio_record = DatiContratto.getRecordDaNumContratto(id_documento, num_crv)

                    Session("carica_contratto_da_gestione_rds") = mio_record.id & ""

                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('contratti.aspx','','');", True)
                    End If

                Case Else
                    Libreria.genUserMsgBox(Page, "Tipo documento ancora non gestito.")
                    Return
            End Select
        Catch ex As Exception
            HttpContext.Current.Response.Write("error lb_num_documento_Click " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Public Sub InitFormNuovoODL(ByVal id_veicolo As Integer)

        Try
            lb_nuovo_danno.Text = 0 ' flag per vedere se ho inserito un nuovo danno
            lb_abilita_ck_chiusura_danno.Text = True

            AzzeraODL()

            tr_attenzione.Visible = False
            bt_modifica_form_odl.Visible = False
            bt_nuovo_danno.Visible = True ' non so se nello stato nuovo disabilitare il salvataggio di un nuovo danno!
            bt_check_out.Visible = False
            bt_check_in.Visible = False
            bt_salva_form_odl.Visible = True
            lb_th_riparato.Text = False
            ck_alvoro_eseguito.Enabled = False
            tab_rientro.Visible = False
            lb_ritirato_da.Visible = False
            DropDown_ritirato_da.Visible = False

            lb_id_veicolo.Text = id_veicolo
            Dim mio_veicolo As tabella_veicoli = tabella_veicoli.get_record_da_id(id_veicolo)
            InitIntestazione(mio_veicolo)

            ' in questo metodo valorizzo i campi usati nella riga sotto!!!
            ' cioè: lb_id_tipo_doc_apertura, lb_num_documento, lb_num_crv
            Dim noleggio_in_corso As Boolean = False
            Dim mio_record As DatiContratto = VerificaAutoInContratto(id_veicolo)
            If mio_record IsNot Nothing Then
                noleggio_in_corso = True
                tr_noleggio_in_corso.Visible = True

                With mio_record
                    lb_id_tipo_doc_apertura.Text = tipo_documento.Contratto
                    lb_num_documento.Text = .num_contratto
                    If .num_crv Is Nothing Then
                        lb_num_crv.Text = 0
                    Else
                        lb_num_crv.Text = .num_crv
                    End If

                    ' potrei aver gia aperto un ODL su questo contratto
                    ' in questo caso devo verificare se esite già un evento (=RDS) sul veicolo legato al contratto.
                    Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaDocumento(tipo_documento.Contratto, .num_contratto, .num_crv)

                    If mio_evento IsNot Nothing Then
                        lb_id_evento_apertura_danno.Text = mio_evento.id
                    End If
                End With
            Else
                tr_noleggio_in_corso.Visible = False

                lb_id_tipo_doc_apertura.Text = tipo_documento.DuranteODL
                lb_num_documento.Text = 0 ' il numero ODL vine assegnato solo durante il salvataggio del record!
                lb_num_crv.Text = 0
            End If


            lb_noleggio_in_corso.Text = noleggio_in_corso

            Trace.Write("InitFormNuovoODL: " & id_veicolo & " " & Integer.Parse(lb_id_tipo_doc_apertura.Text) & " " & Integer.Parse(lb_num_documento.Text) & " " & Integer.Parse(lb_num_crv.Text))

            Dim mio_gruppo_evento As veicoli_gruppo_evento =
            veicoli_gruppo_evento.SalvaDanniApertiAuto(id_veicolo, Integer.Parse(lb_id_tipo_doc_apertura.Text), Integer.Parse(lb_num_documento.Text), Integer.Parse(lb_num_crv.Text), 0)
            lb_id_gruppo_danni_uscita.Text = mio_gruppo_evento.id

            ' per scrupolo ripulisco l'eventuale associazione con precedente ODL... 
            ' se vi fosse un ODL ancora non chiuso 
            ' (in effetti all'ingresso nel modulo questo controllo lo faccio... ma nulla vieta che due operatori aprano un ODL nello stesso momento...)
            veicoli_gruppo_evento.PulisciAssociazioneODLSuDanni(mio_gruppo_evento.id)

            lb_id_gruppo_danni_filtro.Text = mio_gruppo_evento.id
            listViewElencoDanniAperti.DataBind()

            DropDown_stato_odl_DataBind(enum_odl_stato.Nuovo, noleggio_in_corso)

            If noleggio_in_corso Then
                VisualizzaIngressoUscita(False)

                tx_data_uscita.Text = Libreria.myFormatta(Now, "dd/MM/yyyy")
                tx_ora_uscita.Text = Libreria.myFormatta(Now, "HH:mm").Replace(".", ":")
            Else
                VisualizzaIngressoUscita(True)

                tx_data_uscita.Text = Libreria.myFormatta(Now, "dd/MM/yyyy")
                tx_ora_uscita.Text = Libreria.myFormatta(Now, "HH:mm").Replace(".", ":")
                tx_km_uscita.Text = mio_veicolo.km_attuali & ""




                DropDown_stazione_uscita.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
                DropDown_consegnato_da.SelectedValue = Request.Cookies("SicilyRentCar")("idUtente")

                If mio_veicolo.km_attuali Is Nothing Then
                    lb_km_uscita.Text = 0
                    Compare_km_uscita_1.ValueToCompare = 0
                    Compare_km_uscita_2.ValueToCompare = 0
                Else
                    lb_km_uscita.Text = mio_veicolo.km_attuali
                    Compare_km_uscita_1.ValueToCompare = mio_veicolo.km_attuali
                    Compare_km_uscita_2.ValueToCompare = mio_veicolo.km_attuali
                End If
                If mio_veicolo.capacita_serbatoio Is Nothing Then
                    lb_serbatoio.Text = "(N.V.)"
                    Compare_tx_litri_uscita_1.ValueToCompare = 200
                    Compare_tx_litri_uscita_1.ErrorMessage = "I litri di uscita non possono essere superiori a 200."
                    Compare_tx_litri_uscita_2.ValueToCompare = 200
                    Compare_tx_litri_uscita_2.ErrorMessage = "I litri di uscita non possono essere superiori a 200."
                Else
                    lb_serbatoio.Text = mio_veicolo.capacita_serbatoio
                    Compare_tx_litri_uscita_1.ValueToCompare = mio_veicolo.capacita_serbatoio
                    Compare_tx_litri_uscita_1.ErrorMessage = "I litri di uscita non possono essere superiori a " & mio_veicolo.capacita_serbatoio & "."
                    Compare_tx_litri_uscita_2.ValueToCompare = mio_veicolo.capacita_serbatoio
                    Compare_tx_litri_uscita_2.ErrorMessage = "I litri di uscita non possono essere superiori a " & mio_veicolo.capacita_serbatoio & "."
                End If
                tx_litri_uscita.Text = mio_veicolo.serbatoio_attuale 'email Fscalia 13.04.2020


            End If

            DropDown_autorizzato_da.SelectedValue = Request.Cookies("SicilyRentCar")("idUtente")


            AbilitaFormODL(True)
            AbilitaPagamenti(False)
            tx_importo_odl.Enabled = False




            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneODLAdmin) <> "3" Then
                DropDown_autorizzato_da.Enabled = False
                DropDown_stazione_uscita.Enabled = False
            End If

            DropDown_autorizzato_da.Enabled = False 'richiesta modifica con email FSCalia del Data:	13-02-2020 14:36


            gestione_note.InitForm(enum_note_tipo.note_odl, 0, False)

            Visibilita(DivVisibile.Edit)

            div_edit_danno.Visible = False


        Catch ex As Exception
            HttpContext.Current.Response.Write("error InitFormNuovoODL " & ex.Message & "<br/>" & "<br/>")
        End Try



    End Sub

    Private Sub InitElemtiFormODL(ByVal num_odl As Integer)
        lb_nuovo_danno.Text = 0 ' flag per vedere se ho inserito un nuovo danno
        lb_abilita_ck_chiusura_danno.Text = True


        Try
            Dim mio_odl As odl = odl.getRecordDaNumODL(num_odl)

            If mio_odl Is Nothing Then
                Libreria.genUserMsgBox(Page, "Documento ODL non trovato")
                Return
            End If

            FillOdl(mio_odl)

            tr_attenzione.Visible = False
            bt_check_out.Visible = True
            bt_check_in.Visible = False
            bt_salva_form_odl.Visible = True
            lb_th_riparato.Text = False
            tab_rientro.Visible = False
            ck_alvoro_eseguito.Enabled = False
            lb_ritirato_da.Visible = False
            DropDown_ritirato_da.Visible = False

            Dim stato_odl_aperto As Boolean = odl.getStatoODLAperto(mio_odl.id_stato_odl)

            If stato_odl_aperto Then
                bt_modifica_form_odl.Visible = True
                bt_nuovo_danno.Visible = True

                AbilitaFormODL(True)
                AbilitaPagamenti(False)
                tx_importo_odl.Enabled = True

                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneODLAdmin) <> "3" Then
                    DropDown_autorizzato_da.Enabled = False
                    DropDown_stazione_uscita.Enabled = False
                End If



            Else
                bt_modifica_form_odl.Visible = False
                bt_nuovo_danno.Visible = False

                AbilitaFormODL(False)
                AbilitaPagamenti(False)
                tx_importo_odl.Enabled = False
            End If

            lb_id_veicolo.Text = mio_odl.id_veicolo
            Dim mio_veicolo As tabella_veicoli = tabella_veicoli.get_record_da_id(mio_odl.id_veicolo)
            InitIntestazione(mio_veicolo)

            Dim noleggio_in_corso As Boolean = mio_odl.noleggio_in_corso
            lb_noleggio_in_corso.Text = noleggio_in_corso
            tr_noleggio_in_corso.Visible = noleggio_in_corso

            lb_id_tipo_doc_apertura.Text = SeNothingZero(mio_odl.id_tipo_doc_apertura)
            lb_num_documento.Text = SeNothingZero(mio_odl.id_doc_apertura)
            lb_num_crv.Text = SeNothingZero(mio_odl.num_crv_noleggio)

            Dim id_stato_odl As enum_odl_stato = mio_odl.id_stato_odl
            DropDown_stato_odl_DataBind(id_stato_odl, noleggio_in_corso)

            If noleggio_in_corso Then
                lb_visibile_uscita_rientro.Text = False
                VisualizzaIngressoUscita(False)

                bt_check_out.Visible = False
                bt_check_in.Visible = False
            Else
                lb_visibile_uscita_rientro.Text = True
                VisualizzaIngressoUscita(True)

                If mio_veicolo.km_attuali Is Nothing Then
                    lb_km_uscita.Text = 0
                    Compare_km_uscita_1.ValueToCompare = 0
                    Compare_km_uscita_2.ValueToCompare = 0
                Else
                    lb_km_uscita.Text = mio_veicolo.km_attuali
                    Compare_km_uscita_1.ValueToCompare = mio_veicolo.km_attuali
                    Compare_km_uscita_2.ValueToCompare = mio_veicolo.km_attuali
                End If
                If mio_veicolo.capacita_serbatoio Is Nothing Then
                    lb_serbatoio.Text = "(N.V.)"
                    Compare_tx_litri_uscita_1.ValueToCompare = 200
                    Compare_tx_litri_uscita_1.ErrorMessage = "I litri di uscita non possono essere superiori a 200."
                    Compare_tx_litri_uscita_2.ValueToCompare = 200
                    Compare_tx_litri_uscita_2.ErrorMessage = "I litri di uscita non possono essere superiori a 200."
                Else
                    lb_serbatoio.Text = mio_veicolo.capacita_serbatoio
                    Compare_tx_litri_uscita_1.ValueToCompare = mio_veicolo.capacita_serbatoio
                    Compare_tx_litri_uscita_1.ErrorMessage = "I litri di uscita non possono essere superiori a " & mio_veicolo.capacita_serbatoio & "."
                    Compare_tx_litri_uscita_2.ValueToCompare = mio_veicolo.capacita_serbatoio
                    Compare_tx_litri_uscita_2.ErrorMessage = "I litri di uscita non possono essere superiori a " & mio_veicolo.capacita_serbatoio & "."
                End If
            End If

            Select Case id_stato_odl
                Case enum_odl_stato.Attesa_Autorizzazione

                Case enum_odl_stato.Attesa_Preventivo
                    If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneODLAdmin) = "3" Then
                        AbilitaPagamenti(True)
                        tx_importo_odl.Enabled = True
                    End If

                Case enum_odl_stato.Attesa_Riparazione
                    ck_alvoro_eseguito.Enabled = True
                    lb_th_riparato.Text = True
                    If noleggio_in_corso Then
                        tab_rientro.Visible = True
                        AbilitaRientro(True)
                        tx_data_rientro.Text = Libreria.myFormatta(Now, "dd/MM/yyyy")
                        tx_ora_rientro.Text = Libreria.myFormatta(Now, "HH:mm").Replace(".", ":")
                    End If

                Case enum_odl_stato.Non_Autorizzato, enum_odl_stato.Preventivo_Non_Accettato, enum_odl_stato.Riparato, enum_odl_stato.Parzialmente_Riparato, enum_odl_stato.Non_Riparato
                    tab_rientro.Visible = True
                    AbilitaRientro(True)
                    tx_data_rientro.Text = Libreria.myFormatta(Now, "dd/MM/yyyy")
                    tx_ora_rientro.Text = Libreria.myFormatta(Now, "HH:mm").Replace(".", ":")
                    If Not noleggio_in_corso Then
                        tr_attenzione.Visible = True
                        ' valorizzo con valori di default i dati di rientro
                        DropDown_stazione_rientro.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
                        DropDown_ritirato_da.SelectedValue = Request.Cookies("SicilyRentCar")("idUtente")
                    End If

                Case enum_odl_stato.Chiuso_Con_Continuazione_Noleggio


                Case enum_odl_stato.Chiuso
                    lb_abilita_ck_chiusura_danno.Text = False
                    tab_rientro.Visible = True
                    If Not noleggio_in_corso Then
                        bt_check_in.Visible = True
                    End If

                    bt_salva_form_odl.Visible = False

                    AbilitaFormODL(False)
                    AbilitaPagamenti(False)
                    tx_importo_odl.Enabled = False
                    AbilitaRientro(False)

                Case Else
                    Err.Raise(1001, Nothing, "Errore stato odl non previsto")
            End Select

            If noleggio_in_corso Then
                If lb_id_gruppo_danni_rientro.Text <> "" AndAlso Integer.Parse(lb_id_gruppo_danni_rientro.Text) > 0 Then
                    ' rientro è valorizzato solamente se sono stati inseriti dei nuovi danni
                    lb_id_gruppo_danni_filtro.Text = lb_id_gruppo_danni_rientro.Text
                ElseIf lb_id_gruppo_danni_uscita.Text <> "" AndAlso Integer.Parse(lb_id_gruppo_danni_uscita.Text) > 0 Then
                    lb_id_gruppo_danni_filtro.Text = lb_id_gruppo_danni_uscita.Text
                Else
                    lb_id_gruppo_danni_filtro.Text = 0 ' qualcosa è andato storto!!!
                End If
            Else
                If lb_id_gruppo_danni_durante_odl.Text <> "" AndAlso Integer.Parse(lb_id_gruppo_danni_durante_odl.Text) > 0 Then
                    ' sono stati inseriti dei danni sull'ODL
                    lb_id_gruppo_danni_filtro.Text = lb_id_gruppo_danni_durante_odl.Text
                ElseIf lb_id_gruppo_danni_uscita.Text <> "" AndAlso Integer.Parse(lb_id_gruppo_danni_uscita.Text) > 0 Then
                    lb_id_gruppo_danni_filtro.Text = lb_id_gruppo_danni_uscita.Text
                Else
                    lb_id_gruppo_danni_filtro.Text = 0 ' qualcosa è andato storto!!!
                End If
            End If

            ' elimino tutti i legami con tutti i danni non attivi gestiti nelle precedenti sessioni
            veicoli_gruppo_evento.EliminaDanniNonAttivo(Integer.Parse(lb_id_gruppo_danni_filtro.Text))

            listViewElencoDanniAperti.DataBind()
            listViewElencoRDS.DataBind()
            listViewDocumenti.DataBind()

            gestione_note.InitForm(enum_note_tipo.note_odl, num_odl, False)

        Catch ex As Exception
            HttpContext.Current.Response.Write("error InitElemtiFormODL " & ex.Message & "<br/>" & "<br/>")
        End Try


    End Sub


    Public Sub InitFormODL(ByVal num_odl As Integer)
        InitElemtiFormODL(num_odl)

        Visibilita(DivVisibile.Edit)

        div_edit_danno.Visible = False

        'Tony 16-05-2023
        For i As Integer = 0 To listViewElencoDanniAperti.Items.Count - 1
            Dim chkSelezionato As CheckBox = listViewElencoDanniAperti.Items(i).FindControl("ck_chiusura_danno")
            Dim IdRDS As Label = listViewElencoDanniAperti.Items(i).FindControl("lb_id_rds")
            Dim IdEvento As Label = listViewElencoDanniAperti.Items(i).FindControl("lb_id_evento")

            'If chkSelezionato.Checked Then
            '    Response.Write(IdRDS.Text & " - " & IdEvento.Text & " Selezionato<br>")
            'Else
            '    Response.Write(IdRDS.Text & " - " & IdEvento.Text & " NON Selezionato<br>")
            'End If
        Next
        'FINE Tony
    End Sub

    Protected Sub InitIntestazione(ByVal mio_veicolo As tabella_veicoli)
        With mio_veicolo
            lb_targa.Text = .targa
            lb_modello.Text = .modello
            If lb_modello.Text = "" Then
                lb_modello.Text = "(N.V.)"
            End If
            lb_stazione.Text = .stazione
            If lb_stazione.Text = "" Then
                lb_stazione.Text = "(N.V.)"
            End If
            If .km_attuali Is Nothing Then
                lb_km.Text = "(N.V.)"
            Else
                lb_km.Text = .km_attuali
            End If
            If .proprietario Is Nothing Then
                lb_proprietario.Text = "(N.V.)"
            Else
                lb_proprietario.Text = .proprietario
            End If
            If .data_buy_back Is Nothing Then
                lb_data_buy_back.Text = "(N.V.)"
            Else
                lb_data_buy_back.Text = .data_buy_back
            End If
        End With
    End Sub

    Private Function SeNothingZero(ByVal Valore As Integer?) As Integer
        If Valore Is Nothing Then
            Return 0
        End If
        Return Valore
    End Function

    Private Sub AbilitaFormODL(ByVal Valore As Boolean)
        ' DropDown_stato_odl.Enabled = Valore

        ck_rds_attivo.Enabled = Valore

        'ck_alvoro_eseguito.Enabled = Valore

        DropDown_tipo_riparazione.Enabled = Valore
        DropDown_fornitore.Enabled = Valore
        Add_fornitore.Enabled = Valore
        DropDown_lavori_da_eseguire.Enabled = Valore
        tx_descrizione_lavori.Enabled = Valore

        DropDown_autorizzato_da.Enabled = False   'email del 13.04.2020 F.Scalia 'Valore ' campi gestiti con permesso GestioneODLAdmin
        DropDown_stazione_uscita.Enabled = Valore ' campi gestiti con permesso GestioneODLAdmin

        DropDownDrivers.Enabled = Valore
        Add_drivers.Enabled = Valore

        ' Uscita
        If lb_odl.Text <> "" Then
            tx_data_uscita.Enabled = False ' Valore
            tx_ora_uscita.Enabled = False 'Valore
            DropDown_stazione_uscita.Enabled = False 'Valore
            tx_km_uscita.Enabled = False 'Valore
            tx_litri_uscita.Enabled = False 'Valore
        Else
            tx_data_uscita.Enabled = Valore
            tx_ora_uscita.Enabled = Valore
            DropDown_stazione_uscita.Enabled = Valore
            tx_km_uscita.Enabled = Valore
            tx_litri_uscita.Enabled = Valore
        End If

        DropDown_consegnato_da.Enabled = Valore

        'aggiunto 02.12.2021
        'se lb_odl è vuota
        'Dim txt_data As TextBox = CType(div_dettaglio.FindControl("tx_km_uscita"), TextBox)
        'txt_data.Enabled = False





        ' Previsto Rientro
        tx_data_previsto_rientro.Enabled = Valore
        tx_ora_prevista_rientro.Enabled = Valore
        DropDown_stazione_previsto_rientro.Enabled = Valore






        'AbilitaRientro(Valore)

        'AbilitaPagamenti(Valore)
    End Sub

    Protected Sub AbilitaRientro(ByVal Valore As Boolean)
        ' Rientro
        tx_data_rientro.Enabled = Valore
        tx_ora_rientro.Enabled = Valore
        DropDown_stazione_rientro.Enabled = Valore
        tx_km_rientro.Enabled = Valore
        tx_litri_rientro.Enabled = Valore
        DropDown_ritirato_da.Enabled = Valore
    End Sub

    Protected Sub AbilitaPagamenti(ByVal Valore As Boolean)
        DropDown_autorizzato_pagamento.Enabled = Valore
        tx_data_autorizzato_pagamento.Enabled = Valore
        'tx_importo_odl.Enabled = Valore
    End Sub

    Private Sub FillOdl(ByVal mio_odl As odl)
        With mio_odl
            lb_id_odl.Text = .id
            lb_num_odl.Text = .num_odl
            If .num_odl = 0 Then
                lb_odl.Text = "0"
            Else
                lb_odl.Text = .num_odl
            End If

            If .data_odl Is Nothing Then
                lb_data_odl.Text = ""
            Else
                lb_data_odl.Text = .data_odl
            End If

            Dim id_stato_odl As enum_odl_stato = SeNothingZero(mio_odl.id_stato_odl)
            lb_enum_odl_stato.Text = "ODL " & id_stato_odl.ToString().Replace("_", " ")

            'lb_id_tipo_doc_apertura.Text = SeNothingZero(mio_odl.id_tipo_doc_apertura)
            'lb_num_documento.Text = SeNothingZero(mio_odl.id_doc_apertura)
            'lb_num_crv.Text = SeNothingZero(mio_odl.num_crv_noleggio)
            'lb_id_veicolo.Text = SeNothingZero(mio_odl.id_veicolo)
            'If mio_odl.noleggio_in_corso Is Nothing Then
            '    lb_noleggio_in_corso.Text = False
            'Else
            '    lb_noleggio_in_corso.Text = mio_odl.noleggio_in_corso
            'End If

            lb_id_evento_apertura_danno.Text = SeNothingZero(mio_odl.id_evento_apertura_danno)
            lb_id_gruppo_danni_uscita.Text = SeNothingZero(mio_odl.id_gruppo_danni_uscita)
            lb_id_gruppo_danni_durante_odl.Text = SeNothingZero(mio_odl.id_gruppo_danni_durante_odl)
            lb_id_gruppo_danni_rientro.Text = SeNothingZero(mio_odl.id_gruppo_danni_rientro)
            lb_id_movimento_targa.Text = SeNothingZero(mio_odl.id_movimento_targa)

            DropDown_stato_odl.SelectedValue = 0

            ' non ancora gestiti ne che senso abbia...
            ' .id_tipo_doc_apertura = 0
            ' .id_doc_apertura = 0

            ' non so che senso abbia...:
            lb_num_rds.Text = SeNothingZero(.num_rds)
            ck_rds_attivo.Checked = Not (.rds_attivo Is Nothing OrElse Not .rds_attivo)

            ck_alvoro_eseguito.Checked = Not (.lavoro_eseguito Is Nothing OrElse Not .lavoro_eseguito)
            DropDown_tipo_riparazione.SelectedValue = SeNothingZero(.id_tipo_riparazione)
            DropDown_fornitore.SelectedValue = SeNothingZero(.id_fornitore)
            DropDown_lavori_da_eseguire.SelectedValue = SeNothingZero(.id_lavoro_da_eseguire)
            tx_descrizione_lavori.Text = .descrizione_lavoro

            DropDown_autorizzato_da.SelectedValue = SeNothingZero(.id_autorizzato_da)
            DropDownDrivers.SelectedValue = SeNothingZero(.id_conducente)

            tx_data_uscita.Text = Libreria.myFormatta(.data_uscita, "dd/MM/yyyy")
            tx_data_previsto_rientro.Text = Libreria.myFormatta(.data_previsto_rientro, "dd/MM/yyyy")
            tx_data_rientro.Text = Libreria.myFormatta(.data_rientro, "dd/MM/yyyy")

            'Trace.Write(.data_uscita & " " & (Libreria.myFormatta(.data_uscita, "HH:mm")).Replace(".", ":") & " " & .data_previsto_rientro & " " & (Libreria.myFormatta(.data_previsto_rientro, "HH:mm")).Replace(".", ":"))
            tx_ora_uscita.Text = (Libreria.myFormatta(.data_uscita, "HH:mm")).Replace(".", ":")
            tx_ora_prevista_rientro.Text = (Libreria.myFormatta(.data_previsto_rientro, "HH:mm")).Replace(".", ":")
            tx_ora_rientro.Text = (Libreria.myFormatta(.data_rientro, "HH:mm")).Replace(".", ":")

            DropDown_stazione_uscita.SelectedValue = SeNothingZero(.id_stazione_uscita)
            DropDown_stazione_previsto_rientro.SelectedValue = SeNothingZero(.id_stazione_previsto_rientro)
            DropDown_stazione_rientro.SelectedValue = SeNothingZero(.id_stazione_rientro)

            tx_km_uscita.Text = .km_uscita & ""
            tx_km_rientro.Text = .km_rientro & ""

            tx_litri_uscita.Text = .litri_uscita & ""
            tx_litri_rientro.Text = .litri_rientro & ""

            DropDown_consegnato_da.SelectedValue = SeNothingZero(.id_consegnato_da)
            DropDown_ritirato_da.SelectedValue = SeNothingZero(.id_ritirato_da)

            DropDown_autorizzato_pagamento.SelectedValue = SeNothingZero(.id_autorizzato_pagamento)
            tx_data_autorizzato_pagamento.Text = .data_autorizzato_pagamento & ""
            tx_importo_odl.Text = Libreria.myFormatta(.importo, "0.00")
        End With

    End Sub

    Private Sub AzzeraODL()
        Dim mio_odl As odl = New odl
        With mio_odl
            .id = 0
            .num_odl = 0
            '.attivo = False
            .data_odl = Nothing
            .id_stato_odl = 0
            .id_tipo_doc_apertura = 0
            .id_doc_apertura = 0
            .num_crv_noleggio = 0
            .id_veicolo = 0
            .noleggio_in_corso = False
            .id_evento_apertura_danno = Nothing
            .num_rds = Nothing
            .rds_attivo = False
            .lavoro_eseguito = False
            .data_lavoro_eseguito = Nothing
            .id_tipo_riparazione = 0
            .id_fornitore = 0
            .id_lavoro_da_eseguire = 0
            .descrizione_lavoro = ""
            .data_autorizzato = Nothing
            .id_autorizzato_da = 0
            .id_conducente = 0
            .data_uscita = Nothing
            .data_previsto_rientro = Nothing
            .data_rientro = Nothing
            .id_stazione_uscita = 0
            .id_stazione_previsto_rientro = 0
            .id_stazione_rientro = 0
            .km_uscita = Nothing
            .km_rientro = Nothing
            .litri_uscita = Nothing
            .litri_rientro = Nothing
            .id_consegnato_da = 0
            .id_ritirato_da = 0
            .data_richiesta_preventivo = Nothing
            .preventivo = Nothing
            .id_autorizzato_pagamento = 0
            .data_autorizzato_pagamento = Nothing
            .importo = Nothing

            .id_gruppo_danni_uscita = Nothing
            .id_gruppo_danni_durante_odl = Nothing
            .id_gruppo_danni_rientro = Nothing
            .id_movimento_targa = Nothing

            .tipo_fattura = Nothing
            .anno_fattura = Nothing
            .codice_fattura = Nothing
            .data_fattura = Nothing
            .importo_fattura = Nothing
        End With

        FillOdl(mio_odl)
    End Sub

    Protected Sub VisualizzaIngressoUscita(ByVal Valore As Boolean)
        tr1.Visible = Valore
        tr2.Visible = Valore
        tr3.Visible = Valore
        tr4.Visible = Valore
        tr5.Visible = Valore
        tr6.Visible = Valore
        tr7.Visible = Valore
        tr8.Visible = Valore
        tr9.Visible = Valore
        tr10.Visible = Valore
        tr11.Visible = Valore
        tr12.Visible = Valore
    End Sub

    Protected Sub DropDown_fornitore_DataBind()
        DropDown_fornitore.Items.Clear()
        DropDown_fornitore.Items.Add(New ListItem("Seleziona", "0"))
        DropDown_fornitore.DataBind()
    End Sub

    Protected Sub DropDownDrivers_DataBind()
        DropDownDrivers.Items.Clear()
        DropDownDrivers.Items.Add(New ListItem("Seleziona", "0"))
        DropDownDrivers.DataBind()
    End Sub

    Protected Sub anagrafica_fornitori_ChiusuraForm(ByVal sender As Object, ByVal e As System.EventArgs)
        Visibilita(DivVisibile.Edit)
    End Sub

    Protected Sub anagrafica_fornitori_ChiusuraEdit(ByVal sender As Object, ByVal e As EventoNuovoRecord)
        DropDown_fornitore_DataBind()

        DropDown_fornitore.SelectedValue = e.Valore

        ' dovrei verificare che il fornitore sia della tipologia richiesta per l'ODL!!!!!

        Visibilita(DivVisibile.Edit)
    End Sub

    Protected Sub anagrafica_drivers_ChiusuraEdit(ByVal sender As Object, ByVal e As EventoNuovoRecord)
        DropDownDrivers_DataBind()

        DropDownDrivers.SelectedValue = e.Valore

        ' dovrei verificare che il fornitore sia della tipologia richiesta per l'ODL!!!!!

        Visibilita(DivVisibile.Edit)
    End Sub

    Protected Sub edit_danno_AggiornaElenco(ByVal sender As Object, ByVal e As System.EventArgs)
        ' ho salvato un nuovo danno, aggiorno le tabelle e valorizzo il flag nuovo_danno
        lb_nuovo_danno.Text = 1 ' ATTENZIONE in javascript falso = 0, vero qualsiasi valore diverso da zero!

        listViewElencoDanniAperti.DataBind()
        listViewElencoRDS.DataBind()
        listViewDocumenti.DataBind()
    End Sub

    Protected Sub gestione_checkin_SalvaCheckIn(ByVal sender As Object, ByVal e As System.EventArgs)
        InitElemtiFormODL(lb_num_odl.Text)

        RaiseEvent AggiornaElencoODL(Me, New System.EventArgs)

        Visibilita(DivVisibile.Edit)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        AddHandler anagrafica_fornitori.ChiusuraForm, AddressOf anagrafica_fornitori_ChiusuraForm
        AddHandler anagrafica_fornitori.ChiusuraEdit, AddressOf anagrafica_fornitori_ChiusuraEdit

        AddHandler anagrafica_drivers.ChiusuraForm, AddressOf anagrafica_fornitori_ChiusuraForm
        AddHandler anagrafica_drivers.ChiusuraEdit, AddressOf anagrafica_drivers_ChiusuraEdit

        AddHandler gestione_checkin.ChiusuraForm, AddressOf anagrafica_fornitori_ChiusuraForm
        AddHandler gestione_checkin.SalvaCheckIn, AddressOf gestione_checkin_SalvaCheckIn

        AddHandler edit_danno.AggiornaElenco, AddressOf edit_danno_AggiornaElenco

        Trace.Write("Request.CurrentExecutionFilePath: " & Request.CurrentExecutionFilePath & " " & Request.UserHostAddress() & " " & Request.UserHostName())

        If Not Page.IsPostBack Then
            DropDown_stato_odl_DataBind(enum_odl_stato.Nuovo, False)

            DropDown_tipo_riparazione.DataBind()
            DropDown_fornitore.DataBind()
            DropDown_lavori_da_eseguire.DataBind()
            DropDown_autorizzato_da.DataBind()
            DropDownDrivers.DataBind()
            DropDown_stazione_uscita.DataBind()
            DropDown_stazione_previsto_rientro.DataBind()
            DropDown_stazione_rientro.DataBind()
            DropDown_consegnato_da.DataBind()
            DropDown_ritirato_da.DataBind()
            DropDown_autorizzato_pagamento.DataBind()
        End If

    End Sub

    Protected Sub bt_chiudi_form_odl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_chiudi_form_odl.Click
        RaiseEvent ChiusuraForm(Me, New EventArgs)
    End Sub

    Protected Sub listViewElencoDanniAperti_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles listViewElencoDanniAperti.DataBound
        Dim th_lente As Control = listViewElencoDanniAperti.FindControl("th_lente")
        If th_lente IsNot Nothing Then
            th_lente.Visible = Boolean.Parse(lb_th_lente.Text)
        End If
        Dim th_da_addebitare As Control = listViewElencoDanniAperti.FindControl("th_da_addebitare")
        If th_da_addebitare IsNot Nothing Then
            th_da_addebitare.Visible = Boolean.Parse(lb_th_da_addebitare.Text)
        End If
        Dim th_chiusura_danno As Control = listViewElencoDanniAperti.FindControl("th_chiusura_danno")
        If th_chiusura_danno IsNot Nothing Then
            th_chiusura_danno.Visible = Boolean.Parse(lb_th_chiusura_danno.Text)
        End If
        Dim th_riparato As Control = listViewElencoDanniAperti.FindControl("th_riparato")
        If th_riparato IsNot Nothing Then
            th_riparato.Visible = Boolean.Parse(lb_th_riparato.Text)
        End If
    End Sub

    Protected Sub listViewElencoDanniAperti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listViewElencoDanniAperti.ItemDataBound
        Dim lvi As ListViewDataItem = e.Item
        If (lvi.ItemType = ListViewItemType.DataItem) Then

            Dim lb_id_stato As Label = CType(lvi.FindControl("lb_id_stato"), Label)
            If lb_id_stato.Text <> "" Then
                Dim lb_des_id_stato As Label = CType(lvi.FindControl("lb_des_id_stato"), Label)
                Dim id_stato As stato_danno = Integer.Parse(lb_id_stato.Text)
                If id_stato = stato_danno.aperto Then
                    lb_des_id_stato.Text = "No"
                    lb_des_id_stato.ForeColor = Drawing.Color.Red
                Else
                    lb_des_id_stato.Text = "Si"
                End If
            End If

            Dim lb_id_entita_danno As Label = CType(lvi.FindControl("lb_id_entita_danno"), Label)
            If lb_id_entita_danno.Text <> "" Then
                Dim lb_des_id_entita_danno As Label = CType(lvi.FindControl("lb_des_id_entita_danno"), Label)
                Dim id_entita_danno As Entita_Danno = Integer.Parse(lb_id_entita_danno.Text)
                lb_des_id_entita_danno.Text = id_entita_danno.ToString
            End If

            Dim lb_tipo_record As Label = CType(lvi.FindControl("lb_tipo_record"), Label)
            If lb_tipo_record.Text <> "" Then
                Dim lb_des_tipo_record As Label = CType(lvi.FindControl("lb_des_tipo_record"), Label)
                Dim lb_des_id_tipo_danno_tipo_record As Label = CType(lvi.FindControl("lb_des_id_tipo_danno_tipo_record"), Label)
                Dim lb_descrizione_danno As Label = CType(lvi.FindControl("lb_descrizione_danno"), Label)
                Dim id_tipo_record As tipo_record_danni = Integer.Parse(lb_tipo_record.Text)
                lb_des_tipo_record.Text = (id_tipo_record.ToString).Replace("_", " ")

                lb_descrizione_danno.Visible = False
                Select Case id_tipo_record
                    Case tipo_record_danni.Danno_Carrozzeria
                        lb_des_id_tipo_danno_tipo_record.Text = ""
                    Case tipo_record_danni.Danno_Elettrico, tipo_record_danni.Danno_Meccanico
                        lb_des_id_tipo_danno_tipo_record.Text = "Guasto"
                        lb_descrizione_danno.Visible = True
                    Case tipo_record_danni.Furto
                        lb_des_id_tipo_danno_tipo_record.Text = "Totale"
                    Case Else
                        lb_des_id_tipo_danno_tipo_record.Text = "Assente"
                End Select
            End If

            Dim lb_da_addebitare As Label = CType(lvi.FindControl("lb_da_addebitare"), Label)
            If lb_da_addebitare.Text <> "" Then
                Dim lb_des_da_addebitare As Label = CType(lvi.FindControl("lb_des_da_addebitare"), Label)
                If lb_da_addebitare.Text Then
                    lb_des_da_addebitare.Text = "Si"
                    lb_des_da_addebitare.ForeColor = Drawing.Color.Red
                Else
                    lb_des_da_addebitare.Text = "No"
                End If
            End If

            Dim abilita_ck_chiusura_danno As Boolean = Boolean.Parse(lb_abilita_ck_chiusura_danno.Text)
            Dim ck_chiusura_danno As CheckBox = CType(lvi.FindControl("ck_chiusura_danno"), CheckBox)
            ck_chiusura_danno.Enabled = abilita_ck_chiusura_danno
        End If
    End Sub

    Protected Sub listViewElencoDanniAperti_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listViewElencoDanniAperti.ItemCommand
        If e.CommandName = "lente" Then
            Dim lb_id_danno As Label = e.Item.FindControl("lb_id_danno")
            Dim lb_tipo_record As Label = e.Item.FindControl("lb_tipo_record")
            Dim lb_id_evento As Label = e.Item.FindControl("lb_id_evento")
            If lb_tipo_record.Text = tipo_record_danni.Furto Then
                Libreria.genUserMsgBox(Page, "Per il furto totale del mezzo non esiste un dettaglio.")
                Return
            End If

            edit_danno.InitForm(Integer.Parse(lb_id_evento.Text), Integer.Parse(lb_id_danno.Text), 0, 0)

            div_edit_danno.Visible = True
        End If
    End Sub


    Protected Sub listViewElencoRDS_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listViewElencoRDS.ItemCommand
        If e.CommandName = "lente" Then
            If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneDanni) = "1" Then
                Libreria.genUserMsgBox(Page, "Non hai diritti per visionare gli RDS.")
                Return
            End If

            Dim lb_sospeso_rds As Label = e.Item.FindControl("lb_sospeso_rds")
            If lb_sospeso_rds.Text = "" OrElse Boolean.Parse(lb_sospeso_rds.Text) Then
                Libreria.genUserMsgBox(Page, "L'RDS non è stato ancora chiuso e non è possibile visualizzarlo.")
                Return
            End If

            Dim lb_attivo As Label = e.Item.FindControl("lb_attivo")
            If lb_attivo.Text = "" OrElse Not Boolean.Parse(lb_attivo.Text) Then
                Libreria.genUserMsgBox(Page, "L'RDS non è attivo e non è possibile visualizzarlo.")
                Return
            End If

            Dim lb_id_tipo_documento_apertura As Label = e.Item.FindControl("lb_id_tipo_documento_apertura")
            Dim lb_id_rds As Label = e.Item.FindControl("lb_id_rds")


            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                Session("tipo_documento") = lb_id_tipo_documento_apertura.Text
                Session("numero_rds") = lb_id_rds.Text
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('gestione_danni.aspx','')", True)
                End If
            End If
        End If
    End Sub


    Protected Function AssociaODL(ByVal num_odl As Integer) As Boolean
        AssociaODL = False

        For i = 0 To listViewElencoDanniAperti.Items.Count - 1
            Dim lb_id_danno As Label = listViewElencoDanniAperti.Items(i).FindControl("lb_id_danno")
            Dim ck_chiusura_danno As CheckBox = listViewElencoDanniAperti.Items(i).FindControl("ck_chiusura_danno")

            If ck_chiusura_danno.Checked Then
                veicoli_danni.AssociaODL(Integer.Parse(lb_id_danno.Text), num_odl)
            Else
                veicoli_danni.AssociaODL(Integer.Parse(lb_id_danno.Text), Nothing)
            End If
        Next
        Return True
    End Function

    Protected Function SalvaChiusuraDanni() As Boolean
        SalvaChiusuraDanni = False

        For i = 0 To listViewElencoDanniAperti.Items.Count - 1
            Dim lb_id_danno As Label = listViewElencoDanniAperti.Items(i).FindControl("lb_id_danno")
            Dim ck_chiusura_danno As CheckBox = listViewElencoDanniAperti.Items(i).FindControl("ck_chiusura_danno")

            If ck_chiusura_danno.Checked Then
                Dim mio_danno As veicoli_danni = veicoli_danni.getRecordDaId(Integer.Parse(lb_id_danno.Text))



                mio_danno.ChiudiDanno()
            Else
                veicoli_danni.DeselezionaDanno(Integer.Parse(lb_id_danno.Text))
            End If
        Next
        Return True
    End Function

    Protected Function SeZeroNothing(ByVal Valore As Integer) As Integer?
        If Valore = 0 Then
            Return Nothing
        End If
        Return Valore
    End Function

    Protected Function ApriODLSuVeicolo(ByVal mio_odl As odl) As Boolean
        ApriODLSuVeicolo = False

        Dim sqlStr As String = "UPDATE veicoli SET" &
            " disponibile_nolo = 0," &
            " odl_aperto = " & mio_odl.num_odl &
            " WHERE id = " & mio_odl.id_veicolo

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                ApriODLSuVeicolo = True
            End Using
        End Using
    End Function

    Protected Function ChiudiODLSuVeicolo(ByVal mio_odl As odl) As Boolean
        ChiudiODLSuVeicolo = False

        Dim sqlStr As String = "UPDATE veicoli SET" &
            " odl_aperto = NULL " &
            " WHERE id = " & mio_odl.id_veicolo
        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)

                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    ChiudiODLSuVeicolo = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Response.Write("error ChiudiODLSuVeicolo " & ex.Message & "<br/>" & sqlStr & "<br/>")
        End Try

    End Function

    Protected Function ApriMovimentoTarga(ByVal mio_odl As odl) As Integer?
        ApriMovimentoTarga = Nothing
        Dim mio_mov As movimenti_targa = New movimenti_targa
        Try
            With mio_mov
                .num_riferimento = mio_odl.num_odl
                .num_crv_contratto = 0
                .id_veicolo = mio_odl.id_veicolo
                .id_tipo_movimento = enum_tipologia_movimenti.Riparazione
                .data_uscita = mio_odl.data_uscita
                .id_stazione_uscita = mio_odl.id_stazione_uscita
                .km_uscita = mio_odl.km_uscita
                .serbatoio_uscita = mio_odl.litri_uscita
                .movimento_attivo = True
                .data_presunto_rientro = mio_odl.data_previsto_rientro
                .id_stazione_presunto_rientro = mio_odl.id_stazione_previsto_rientro

                Return .SalvaRecord()
            End With
        Catch ex As Exception
            HttpContext.Current.Response.Write("error ApriMovimentoTarga  " & ex.Message & "<br/>" & "<br/>")
        End Try


    End Function

    Protected Function ChiudiMovimentoTarga(ByVal mio_odl As odl) As Boolean
        ChiudiMovimentoTarga = False
        Try
            Dim mio_mov As movimenti_targa = movimenti_targa.getRecordDaId(mio_odl.id_movimento_targa)

            With mio_mov
                .data_rientro = mio_odl.data_rientro
                ' questi dati sono tutti Nothing...
                .id_stazione_rientro = mio_odl.id_stazione_rientro
                .km_rientro = mio_odl.km_rientro
                .serbatoio_rientro = mio_odl.litri_rientro

                .movimento_attivo = False

                Return .AggiornaRecord()
            End With
        Catch ex As Exception
            HttpContext.Current.Response.Write("error ChiudiMovimentoTarga " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Function

    Protected Function Salva(Optional ByVal NuovoStato As Boolean = True, Optional ByVal chiudi_odl As Boolean = False) As odl

        Trace.Write("Salva: " & NuovoStato)
        Dim mio_veicolo As tabella_veicoli = tabella_veicoli.get_record_da_id(Integer.Parse(lb_id_veicolo.Text))
        Dim mio_odl As odl = Nothing
        Dim nuovo_odl As Boolean = False


        Try
            If lb_num_odl.Text = "" Or lb_num_odl.Text = "0" Then
                nuovo_odl = True
                mio_odl = New odl
                mio_odl.num_odl = Contatori.getContatore_ODL(mio_veicolo.id_stazione)
                mio_odl.id_veicolo = mio_veicolo.id

                lb_num_odl.Text = mio_odl.num_odl
            Else
                mio_odl = odl.getRecordDaNumODL(lb_num_odl.Text)
            End If

            Dim noleggio_in_corso As Boolean = Boolean.Parse(lb_noleggio_in_corso.Text)

            With mio_odl

                If .data_odl Is Nothing Then
                    .data_odl = Now
                End If

                .noleggio_in_corso = noleggio_in_corso

                If NuovoStato Then
                    Dim stato_attuale As enum_odl_stato = enum_odl_stato.Nuovo
                    If .id_stato_odl IsNot Nothing Then
                        stato_attuale = .id_stato_odl
                    End If
                    Dim nuovo_stato As enum_odl_stato = DropDown_stato_odl.SelectedValue
                    If Not noleggio_in_corso And nuovo_stato = enum_odl_stato.Chiuso Then
                        ' se ODL su veicolo in parco, per chiudere L'ODL,
                        ' devo effettuare il check in, effettundo così il rientro del veicolo.
                        ' Uso quindi questo salvataggio solamente per salvare Stazione, Data e ora.
                        ' Se non viene effetuato il Check In, l'ODL non viene chiuso!
                        .id_stato_odl = SeZeroNothing(stato_attuale)
                    Else
                        .id_stato_odl = SeZeroNothing(DropDown_stato_odl.SelectedValue)
                    End If
                    Trace.Write("stato_attuale: " & stato_attuale.ToString & " nuovo_stato: " & nuovo_stato.ToString)
                End If

                If chiudi_odl Then
                    .id_stato_odl = enum_odl_stato.Chiuso
                End If

                .id_tipo_doc_apertura = Integer.Parse(lb_id_tipo_doc_apertura.Text)

                ' -----------------------------------------------------------------------------------------------------------------
                ' se il numero documento non risulta ancora asseganto lo valorizzo dato che adesso il numero ODL è stato assegnato!
                ' questo può succedere solo per ODL su veicoli non noleggiati!
                If lb_num_documento.Text = "" OrElse Integer.Parse(lb_num_documento.Text) <= 0 Then
                    lb_num_documento.Text = .num_odl
                End If
                .id_doc_apertura = Integer.Parse(lb_num_documento.Text)
                ' -----------------------------------------------------------------------------------------------------------------

                .num_crv_noleggio = Integer.Parse(lb_num_crv.Text)
                .num_rds = Integer.Parse(lb_num_rds.Text)

                .id_gruppo_danni_uscita = SeZeroNothing(Integer.Parse(lb_id_gruppo_danni_uscita.Text))
                .id_gruppo_danni_durante_odl = SeZeroNothing(Integer.Parse(lb_id_gruppo_danni_durante_odl.Text))
                .id_gruppo_danni_rientro = SeZeroNothing(Integer.Parse(lb_id_gruppo_danni_rientro.Text))
                .id_evento_apertura_danno = SeZeroNothing(Integer.Parse(lb_id_evento_apertura_danno.Text))
                .id_movimento_targa = SeZeroNothing(lb_id_movimento_targa.Text)

                .id_stazione_uscita = SeZeroNothing(DropDown_stazione_uscita.SelectedValue)
                .id_stazione_previsto_rientro = SeZeroNothing(DropDown_stazione_previsto_rientro.SelectedValue)
                .id_stazione_rientro = SeZeroNothing(DropDown_stazione_rientro.SelectedValue)

                Dim mio_evento_apertura As veicoli_evento_apertura_danno = Nothing
                ' se lb_id_evento_apertura_danno.Text > 0 allora sull'ODL sono stati inseriti nuovi danni
                If lb_id_evento_apertura_danno.Text <> "" AndAlso Integer.Parse(lb_id_evento_apertura_danno.Text) > 0 Then

                    mio_evento_apertura = veicoli_evento_apertura_danno.getRecordDaId(lb_id_evento_apertura_danno.Text)

                    ' verifico che il numero documento sull'evento sia correttamente valorizzato!
                    ' in caso contrario lo aggiorno ed aggiorno il gruppo danni associato all'evento
                    If mio_evento_apertura.id_documento_apertura Is Nothing OrElse mio_evento_apertura.id_documento_apertura <= 0 Then
                        mio_evento_apertura.id_documento_apertura = .num_odl
                        mio_evento_apertura.AggiornaRecord()
                    End If

                    ' Aggiorno il gruppo 
                    Dim mio_gruppo_evento As veicoli_gruppo_evento = veicoli_gruppo_evento.getRecordDaId(lb_id_gruppo_danni_filtro.Text)
                    If mio_gruppo_evento.id_documento_apertura Is Nothing OrElse mio_gruppo_evento.id_documento_apertura <= 0 Then
                        mio_gruppo_evento.id_documento_apertura = .num_odl
                        mio_gruppo_evento.AggiornaRecord()
                    End If

                    ' dato che l'ODL viene salvato, l'evento di apertura danno diventa attivo
                    ' ed in questo caso è necessario assegnare un numero RDS se non già fatto!
                    If mio_evento_apertura.id_rds Is Nothing OrElse mio_evento_apertura.id_rds <= 0 Then
                        ' Attivo l'evento dei danni insieme ai danni inseriti generando il numero RDS
                        ' nota che pongo l'RDS nello stato di attesa per non farlo processare all'ufficio sinistri!
                        ' nel caso di ODL su contratto aperto, dovrei salvare l'RDS sulla stazione di rientro che NON HO!
                        ' Tranne quella presunta... ma è pur sempre presunta...!
                        If .id_stazione_uscita IsNot Nothing Then
                            mio_evento_apertura.AttivaRecord(.id_stazione_uscita, True)
                        Else
                            If mio_veicolo.id_stazione IsNot Nothing Then
                                mio_evento_apertura.AttivaRecord(mio_veicolo.id_stazione, True)
                            Else
                                mio_evento_apertura.AttivaRecord(Request.Cookies("SicilyRentCar")("stazione"), True)
                            End If
                        End If
                    Else
                        ' se vengono memorizzati nuovi danni su un evento apertura già attivo 
                        ' devo attivarli affinché vengano memorizzati con l'ODL
                        If Integer.Parse(lb_nuovo_danno.Text) > 0 Then
                            For Each lvi As ListViewDataItem In listViewElencoDanniAperti.Items
                                Dim lb_danno_attivo As Label = CType(lvi.FindControl("lb_danno_attivo"), Label)
                                Dim ID As Integer = Convert.ToInt32(listViewElencoDanniAperti.DataKeys(lvi.DisplayIndex).Value) ' id_danno
                                If Not Boolean.Parse(lb_danno_attivo.Text) Then
                                    veicoli_danni.AttivaDanno(ID)
                                End If
                            Next
                        End If
                    End If
                End If

                ' i danni sono stati salvati!
                lb_nuovo_danno.Text = 0

                ' non so che senso abbia...:
                .num_rds = SeZeroNothing(lb_num_rds.Text)

                .rds_attivo = ck_rds_attivo.Checked

                .lavoro_eseguito = ck_alvoro_eseguito.Checked
                .id_tipo_riparazione = SeZeroNothing(DropDown_tipo_riparazione.SelectedValue)
                .id_fornitore = SeZeroNothing(DropDown_fornitore.SelectedValue)
                .id_lavoro_da_eseguire = SeZeroNothing(DropDown_lavori_da_eseguire.SelectedValue)

                If tx_descrizione_lavori.Text = "" Then
                    .descrizione_lavoro = Nothing
                Else
                    .descrizione_lavoro = tx_descrizione_lavori.Text
                End If

                .id_autorizzato_da = SeZeroNothing(DropDown_autorizzato_da.SelectedValue)

                ' ---------------------------------------------------------------------
                ' in caso di nolo in corso i valori sotto saranno tutti null!!!!!!!!!
                ' ---------------------------------------------------------------------
                .id_conducente = SeZeroNothing(DropDownDrivers.SelectedValue)

                If tx_data_uscita.Text = "" Then
                    .data_uscita = Nothing
                Else
                    .data_uscita = DateTime.Parse(tx_data_uscita.Text & " " & tx_ora_uscita.Text)
                End If
                If tx_data_previsto_rientro.Text = "" Then
                    .data_previsto_rientro = Nothing
                Else
                    .data_previsto_rientro = DateTime.Parse(tx_data_previsto_rientro.Text & " " & tx_ora_prevista_rientro.Text)
                End If
                If tx_data_rientro.Text = "" Then
                    .data_rientro = Nothing
                Else
                    .data_rientro = DateTime.Parse(tx_data_rientro.Text & " " & tx_ora_rientro.Text)
                End If



                If tx_km_uscita.Text = "" Then
                    .km_uscita = Nothing
                Else
                    .km_uscita = Integer.Parse(tx_km_uscita.Text)
                End If
                If tx_km_rientro.Text = "" Then
                    .km_rientro = Nothing
                Else
                    .km_rientro = Integer.Parse(tx_km_rientro.Text)
                End If

                If tx_litri_uscita.Text = "" Then
                    .litri_uscita = Nothing
                Else
                    .litri_uscita = Integer.Parse(tx_litri_uscita.Text)
                End If
                If tx_litri_rientro.Text = "" Then
                    .litri_rientro = Nothing
                Else
                    .litri_rientro = Integer.Parse(tx_litri_rientro.Text)
                End If

                .id_consegnato_da = SeZeroNothing(DropDown_consegnato_da.SelectedValue)
                .id_ritirato_da = SeZeroNothing(DropDown_ritirato_da.SelectedValue)
                ' ---------------------------------------------------------------------
                ' ---------------------------------------------------------------------

                .id_autorizzato_pagamento = SeZeroNothing(DropDown_autorizzato_pagamento.SelectedValue)

                If tx_data_autorizzato_pagamento.Text = "" Then
                    .data_autorizzato_pagamento = Nothing
                Else
                    .data_autorizzato_pagamento = Date.Parse(tx_data_autorizzato_pagamento.Text)
                End If
                If tx_importo_odl.Text = "" Then
                    .importo = Nothing
                Else
                    .importo = Double.Parse(tx_importo_odl.Text)
                End If

                If nuovo_odl And NuovoStato Then
                    ApriODLSuVeicolo(mio_odl)
                    .id_movimento_targa = ApriMovimentoTarga(mio_odl)
                End If

                If .noleggio_in_corso And .id_stato_odl = enum_odl_stato.Chiuso Then ' solo sul noleggio la chiusura dei movimenti targa viene effettuata qui!
                    ChiudiODLSuVeicolo(mio_odl)
                    ChiudiMovimentoTarga(mio_odl)
                End If

                .SalvaRecord()

                gestione_note.InitForm(enum_note_tipo.note_odl, .num_odl, False)

                ' associo i danni selezionati con l'ODL corrente
                AssociaODL(.num_odl)

                If .lavoro_eseguito Then
                    SalvaChiusuraDanni()
                End If

                RaiseEvent AggiornaElencoODL(Me, New EventArgs)
            End With

            Return mio_odl

        Catch ex As Exception
            HttpContext.Current.Response.Write("error  Salva " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Function

    Protected Function VerificaSelezioneDanni() As String
        Dim messaggio As String = ""

        Dim danni_riparati As Boolean = False
        Dim danni_selezionati As Boolean = True
        For i = 0 To listViewElencoDanniAperti.Items.Count - 1
            Dim lb_id_danno As Label = listViewElencoDanniAperti.Items(i).FindControl("lb_id_danno")
            Dim ck_chiusura_danno As CheckBox = listViewElencoDanniAperti.Items(i).FindControl("ck_chiusura_danno")
            Dim ck_riparato As CheckBox = listViewElencoDanniAperti.Items(i).FindControl("ck_riparato")

            If ck_riparato.Checked Then
                danni_riparati = True
            End If

            If danni_selezionati And ck_riparato.Checked And Not ck_chiusura_danno.Checked Then
                danni_selezionati = False
                messaggio += "Tutti i danni che sono stati riparati devono essere selezionati." & vbCrLf
            End If
        Next

        If danni_riparati Then
            If Not ck_alvoro_eseguito.Checked Then
                messaggio += "Per salvare la riparazione dei danni selezionati, devi selezionare [Lavoro eseguito]." & vbCrLf
            End If
        End If

        Return messaggio
    End Function

    Protected Function verifica_salvataggio_odl() As Boolean
        Dim messaggio As String = ""

        Dim noleggio_in_corso As Boolean = Boolean.Parse(lb_noleggio_in_corso.Text)

        Dim id_stato_odl As enum_odl_stato = Integer.Parse(DropDown_stato_odl.SelectedValue)

        If Not noleggio_in_corso AndAlso Integer.Parse(DropDown_consegnato_da.SelectedValue) = 0 Then
            messaggio += "E' necessario specificare chi ha consegnato il veicolo." & vbCrLf
        End If

        If noleggio_in_corso Then
            ' dovrei verificare che sia stato aperto almeno un danno...
        End If

        Select Case id_stato_odl
            Case enum_odl_stato.Attesa_Autorizzazione

            Case enum_odl_stato.Attesa_Preventivo
                If tx_data_uscita.Text = "" Then
                    messaggio += "E' necessario specificare la data di uscita del veicolo." & vbCrLf
                End If
                If tx_ora_uscita.Text = "" Then
                    messaggio += "E' necessario specificare l'ora di uscita del veicolo." & vbCrLf
                End If
                'If tx_litri_uscita.Text = "" Then
                '    messaggio += "E' necessario specificare i litri di uscita del veicolo." & vbCrLf
                'End If
                If Integer.Parse(DropDown_autorizzato_da.SelectedValue) <= 0 Then
                    messaggio += "E' necessario specificare chi autorizza l'ODL." & vbCrLf
                End If

            Case enum_odl_stato.Attesa_Riparazione
                If tx_data_uscita.Text = "" Then
                    messaggio += "E' necessario specificare la data di uscita del veicolo." & vbCrLf
                End If
                If tx_ora_uscita.Text = "" Then
                    messaggio += "E' necessario specificare l'ora di uscita del veicolo." & vbCrLf
                End If
                If Integer.Parse(DropDown_autorizzato_da.SelectedValue) <= 0 Then
                    messaggio += "E' necessario specificare chi autorizza l'ODL." & vbCrLf
                End If
                If Integer.Parse(DropDown_autorizzato_pagamento.SelectedValue) <= 0 Then
                    messaggio += "E' necessario specificare chi autorizza il preventivo dell'ODL." & vbCrLf
                End If
                If tx_data_autorizzato_pagamento.Text = "" Then
                    messaggio += "E' necessario specificare la data di autorizzazione del preventivo dell'ODL." & vbCrLf
                End If
                If tx_importo_odl.Text = "" Then
                    messaggio += "E' necessario specificare l'importo del preventivo dell'ODL." & vbCrLf
                End If

            Case enum_odl_stato.Non_Autorizzato
                If Integer.Parse(DropDown_autorizzato_da.SelectedValue) <= 0 Then
                    messaggio += "E' necessario specificare chi NON autorizza l'ODL." & vbCrLf
                End If

            Case enum_odl_stato.Preventivo_Non_Accettato
                If tx_data_uscita.Text = "" Then
                    messaggio += "E' necessario specificare la data di uscita del veicolo." & vbCrLf
                End If
                If tx_ora_uscita.Text = "" Then
                    messaggio += "E' necessario specificare l'ora di uscita del veicolo." & vbCrLf
                End If
                If Integer.Parse(DropDown_autorizzato_da.SelectedValue) <= 0 Then
                    messaggio += "E' necessario specificare chi autorizza l'ODL." & vbCrLf
                End If
                If noleggio_in_corso Then ' e su questi stato che viene chiuso l'ODL
                    If tx_data_rientro.Text = "" Then
                        messaggio += "E' necessario specificare la data di rientro del veicolo." & vbCrLf
                    End If
                    If tx_ora_rientro.Text = "" Then
                        messaggio += "E' necessario specificare l'orario di rientro del veicolo." & vbCrLf
                    End If
                End If
                If Integer.Parse(DropDown_autorizzato_pagamento.SelectedValue) <= 0 Then
                    messaggio += "E' necessario specificare chi NON autorizza il preventivo dell'ODL." & vbCrLf
                End If
                If tx_data_autorizzato_pagamento.Text = "" Then
                    messaggio += "E' necessario specificare la data di NON autorizzazione del preventivo dell'ODL." & vbCrLf
                End If
                If tx_importo_odl.Text = "" Then
                    messaggio += "E' necessario specificare l'importo NON autorizzato del preventivo dell'ODL." & vbCrLf
                End If

            Case enum_odl_stato.Non_Riparato, enum_odl_stato.Parzialmente_Riparato, enum_odl_stato.Riparato
                If tx_data_uscita.Text = "" Then
                    messaggio += "E' necessario specificare la data di uscita del veicolo." & vbCrLf
                End If
                If tx_ora_uscita.Text = "" Then
                    messaggio += "E' necessario specificare l'ora di uscita del veicolo." & vbCrLf
                End If
                If noleggio_in_corso Then ' e su questi stato che viene chiuso l'ODL
                    If tx_data_rientro.Text = "" Then
                        messaggio += "E' necessario specificare la data di rientro del veicolo." & vbCrLf
                    End If
                    If tx_ora_rientro.Text = "" Then
                        messaggio += "E' necessario specificare l'orario di rientro del veicolo." & vbCrLf
                    End If
                End If
                If Integer.Parse(DropDown_autorizzato_da.SelectedValue) <= 0 Then
                    messaggio += "E' necessario specificare chi autorizza l'ODL." & vbCrLf
                End If
                If Integer.Parse(DropDown_autorizzato_pagamento.SelectedValue) <= 0 Then
                    messaggio += "E' necessario specificare chi autorizza il preventivo dell'ODL." & vbCrLf
                End If
                If tx_data_autorizzato_pagamento.Text = "" Then
                    messaggio += "E' necessario specificare la data di autorizzazione del preventivo dell'ODL." & vbCrLf
                End If
                If tx_importo_odl.Text = "" Then
                    messaggio += "E' necessario specificare l'importo del preventivo dell'ODL." & vbCrLf
                End If

                messaggio += VerificaSelezioneDanni()

            Case enum_odl_stato.Chiuso_Con_Continuazione_Noleggio
                ' nessuna regola per adesso...

            Case enum_odl_stato.Chiuso
                If tx_data_uscita.Text = "" Then
                    messaggio += "E' necessario specificare la data di uscita del veicolo." & vbCrLf
                End If
                If tx_ora_uscita.Text = "" Then
                    messaggio += "E' necessario specificare l'ora di uscita del veicolo." & vbCrLf
                End If
                If tx_data_rientro.Text = "" Then
                    messaggio += "E' necessario specificare la data di rientro del veicolo." & vbCrLf
                End If
                If tx_ora_rientro.Text = "" Then
                    messaggio += "E' necessario specificare l'orario di rientro del veicolo." & vbCrLf
                End If
                If Integer.Parse(DropDown_stazione_rientro.SelectedValue) <= 0 Then
                    messaggio += "E' necessario specificare la stazione di rientro del veicolo." & vbCrLf
                End If
                If Integer.Parse(DropDown_ritirato_da.SelectedValue) <= 0 Then
                    messaggio += "E' necessario specificare chi ha ritirato il veicolo." & vbCrLf
                End If

            Case Else
                Err.Raise(1001, Nothing, "stato odl non gestito")
        End Select

        If messaggio <> "" Then
            Libreria.genUserMsgBox(Page, messaggio)
            Return False
        Else
            Return True
        End If
    End Function

    'Tony 21-04-2023
    Private Sub InsArrayValore(ByVal NomeArray As Array, ByVal valore As String)
        For i = 0 To UBound(NomeArray)
            If NomeArray(i) = "" Then
                NomeArray(i) = valore
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
        End If
    End Sub
    'FINE Tony

    Protected Sub bt_salva_form_odl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_salva_form_odl.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneODL) < "3" Then
            Libreria.genUserMsgBox(Page, "Non hai i permessi per salvare l'ODL.")
            Return
        End If

        'Tony 21-04-2023
        Dim ArrayElencoChkRDS(100) As String
        SvuotaArray(ArrayElencoChkRDS)

        For Each lvi As ListViewDataItem In listViewElencoDanniAperti.Items
            Dim chk As CheckBox = lvi.FindControl("ck_chiusura_danno")
            Dim ID As Label = lvi.FindControl("lb_id_rds")

            If chk.Checked Then
                'Response.Write("RDS: " & ID.Text & " Chk: SI<br>")
                InsArrayValore(ArrayElencoChkRDS, ID.Text)
            Else
                'Response.Write("RDS: " & ID.Text & " Chk: NO<br>")
            End If
        Next

        'Response.Write("Stato ODL: " & DropDown_stato_odl.SelectedValue)
        If ck_alvoro_eseguito.Checked = True Then
            'Response.Write("<br>Lavoro eseguito: SI")
        Else
            'Response.Write("<br>Lavoro eseguito: NO")
        End If
        'Response.End()

        'For j = 0 To UBound(ArrayElencoChkRDS)
        '    Response.Write(ArrayElencoChkRDS(j) & "<br>")
        'Next

        
        'Tony 21-04-2023        
        Dim DbcRiparato As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        DbcRiparato.Open()

        Dim SqlRiparato As String = "select veicoli_evento_apertura_danno.*, veicoli.targa from veicoli_evento_apertura_danno, veicoli WITH(NOLOCK) WHERE veicoli_evento_apertura_danno.id_veicolo = veicoli.id and (veicoli.targa='" & lb_targa.Text & "') AND (veicoli_evento_apertura_danno.attivo = 1) and veicoli_evento_apertura_danno.da_riparare = 1 "
        
        If ArrayElencoChkRDS(0) <> "" Then
            SqlRiparato = SqlRiparato & " and (veicoli_evento_apertura_danno.id_rds='" & ArrayElencoChkRDS(0) & "'"
        End If
        For j = 1 To UBound(ArrayElencoChkRDS)
            If ArrayElencoChkRDS(j) <> "" Then
                SqlRiparato = SqlRiparato & " or veicoli_evento_apertura_danno.id_rds='" & ArrayElencoChkRDS(j) & "'"
            Else
                Exit For
            End If            
        Next
        SqlRiparato = SqlRiparato & ")"

        'Response.Write("<br>" & SqlRiparato)
        'Response.End()
        
        
        Dim CmdRiparato As New Data.SqlClient.SqlCommand(SqlRiparato, DbcRiparato)

        Try
            Dim RsRiparato As Data.SqlClient.SqlDataReader
            RsRiparato = CmdRiparato.ExecuteReader()
            If RsRiparato.HasRows Then
                Do While RsRiparato.Read
                    If DropDown_stato_odl.SelectedValue = 6 And ck_alvoro_eseguito.Checked = True Then                        

                        Dim DbcRiparato2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                        DbcRiparato2.Open()

                        Dim CmdRiparato2 As New Data.SqlClient.SqlCommand("", DbcRiparato2)
                        Dim SqlRiparato2 As String
                        Dim SqlQueryRiparato2 As String

                        Try                           
                            If RsRiparato("id_non_addebito") & "" = "" Then 'Da addebitare
                                Dim ValoreIncassato As String = ""
                                'ValoreIncassato = RsRiparato("totale") * 1.22
                                ValoreIncassato = CDbl(RsRiparato("totale")) - CDbl(RsRiparato("spese_postali"))
                                ValoreIncassato = Replace(ValoreIncassato, ",", ".")
                                'Response.Write("5- " & ValoreIncassato & "<br>")
                                'Response.End()

                                SqlRiparato2 = "update veicoli_evento_apertura_danno set da_riparare=0, incasso= '" & ValoreIncassato & "' WHERE (id_documento_apertura = '" & RsRiparato("id_documento_apertura") & "') AND (attivo = 1) "

                                'Response.Write("<br>" & SqlRiparato2)
                                'Response.End()

                                CmdRiparato2 = New Data.SqlClient.SqlCommand(SqlRiparato2, DbcRiparato2)
                                CmdRiparato2.ExecuteNonQuery()

                                SqlQueryRiparato2 = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(SqlRiparato2, "'", "''") & "')"
                                CmdRiparato2.CommandText = SqlQueryRiparato2
                                'Response.Write(CmdRiparato2.CommandText & "<br/>")
                                'Response.End()
                                CmdRiparato2.ExecuteNonQuery()
                            Else 'Non addebitare
                                SqlRiparato2 = "update veicoli_evento_apertura_danno set da_riparare=0 WHERE (id_documento_apertura = '" & RsRiparato("id_documento_apertura") & "') AND (attivo = 1) "

                                Response.Write("<br>" & SqlRiparato2)
                                'Response.End()

                                CmdRiparato2 = New Data.SqlClient.SqlCommand(SqlRiparato2, DbcRiparato2)
                                CmdRiparato2.ExecuteNonQuery()

                                SqlQueryRiparato2 = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(SqlRiparato2, "'", "''") & "')"
                                CmdRiparato2.CommandText = SqlQueryRiparato2
                                'Response.Write(CmdRiparato2.CommandText & "<br/>")
                                'Response.End()
                                CmdRiparato2.ExecuteNonQuery()
                            End If
                            
                        Catch ex As Exception
                            Libreria.genUserMsgBox(Page, ex.Message & " -- Salvataggio Riparato 2 Errore contattare amministratore del sistema.")
                        End Try

                        CmdRiparato2.Dispose()
                        CmdRiparato2 = Nothing
                        DbcRiparato2.Close()
                        DbcRiparato2.Dispose()
                        DbcRiparato2 = Nothing
                    Else
                        'Response.Write("Else")
                        'Response.End()
                    End If
                Loop
            End If
            RsRiparato.Close()
            RsRiparato = Nothing
        Catch ex As Exception
            Libreria.genUserMsgBox(Page, "Salvataggio Riparato ARES Errore contattare amministratore del sistema.")
        End Try


        CmdRiparato.Dispose()
        DbcRiparato.Close()
        CmdRiparato = Nothing
        DbcRiparato = Nothing
        'FINE Tony

        'Response.Write("Km uscita " & tx_km_uscita.Text)
        'Response.End()

        Dim nuovo_odl As Boolean = False
        If lb_id_odl.Text = "" Or lb_id_odl.Text = "0" Then
            Dim num_odl As String = odl.VerificaODLApertoSuVeicolo(Integer.Parse(lb_id_veicolo.Text))
            If num_odl <> "" Then
                Libreria.genUserMsgBox(Page, "E' gia presente un ODL:(" & num_odl & ") aperto su questo veicolo.")
                Return
            End If
            nuovo_odl = True
        End If

        Trace.Write("IDStatoODL " & lb_id_odl.Text)
        DropDown_stazione_rientro.SelectedValue = Request.Cookies("SicilyRentCar")("stazione")
        DropDown_ritirato_da.SelectedValue = Request.Cookies("SicilyRentCar")("idUtente")


        If Not verifica_salvataggio_odl() Then
            Return
        End If

        Dim mio_odl As odl = Salva()

        Dim id_stato_odl As enum_odl_stato = DropDown_stato_odl.SelectedValue

        div_edit_danno.Visible = False

        If nuovo_odl Then
            Trace.Write("ID Stato" & id_stato_odl)
            If Not Boolean.Parse(lb_noleggio_in_corso.Text) Then
                mio_odl.id_gruppo_danni_uscita = gestione_checkin.InitFormCheckOut(tipo_documento.ODL, mio_odl.num_odl, 0)

                Visibilita(DivVisibile.GestioneCheck)
            End If
        Else
            Trace.Write("ID Stato" & id_stato_odl)
            Select Case id_stato_odl
                Case enum_odl_stato.Attesa_Autorizzazione, enum_odl_stato.Attesa_Preventivo, enum_odl_stato.Attesa_Riparazione


                Case enum_odl_stato.Non_Autorizzato, enum_odl_stato.Preventivo_Non_Accettato, enum_odl_stato.Non_Riparato,
                    enum_odl_stato.Parzialmente_Riparato, enum_odl_stato.Riparato
                    Trace.Write("IN")
                    If Not Boolean.Parse(lb_noleggio_in_corso.Text) Then
                        ' ho chiuso l'odl, se ho inserito qualche danno (senza noleggio in corso!!!)
                        ' questi danni devo renderli visibili al modulo RDS
                        If lb_id_evento_apertura_danno.Text <> "" AndAlso Integer.Parse(lb_id_evento_apertura_danno.Text) > 0 Then
                            Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(lb_id_evento_apertura_danno.Text)
                            mio_evento.sospeso_rds = False
                            mio_evento.AggiornaRecord()
                        End If
                    Else
                        ' nel caso di noleggio non devo rientrare il veicolo...
                        ' ma chiudo semplicemente l'ODL... 
                        ' per tener traccia dei motivi con cui si è chiuso l'ODL effettuo un nuovo salvataggio trasparente all'utente

                        'mio_odl = Salva(True, True) ' chiudo l'odl
                        'Session("carica_contratto") = "115119"
                        'Response.Redirect("contratti.aspx")



                    End If

                Case enum_odl_stato.Chiuso
                    If Not Boolean.Parse(lb_noleggio_in_corso.Text) Then
                        ' sto chiudendo un ODL su auto in parco (non noleggio!)
                        ' l'effetiva chiusura deve essere eseguita sul check in!
                        ' ATTENZIONE: il tipo documento per il check in è tipo_documento.ODL e non tipo_documento.DuranteODL
                        ' I danni sull'ODL per veicolo in parco vengono gestiti intermanete all'interno di questo modulo
                        ' L'apertura del form check in è solamente per gestire eventuali nuovi danni 
                        ' rilevati sul veicolo al rientro dello stesso (imputabili a chi ha effettuato la riparazione!!!!)
                        gestione_checkin.InitFormCheckIn(tipo_documento.ODL, Integer.Parse(lb_num_documento.Text), Integer.Parse(lb_num_crv.Text))

                        Visibilita(DivVisibile.GestioneCheck)
                    Else
                        'Tony 11/05/2022
                        Try
                            Dim Dbc As New Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                            Dbc.Open()

                            Dim Cmd As New Data.SqlClient.SqlCommand("select * from contratti WITH(NOLOCK) WHERE num_contratto = '" & lb_num_documento.Text & "' and attivo = 1", Dbc)
                            'Response.Write(Cmd.CommandText & "<br><br>")
                            'Response.End()
                            Dim Rs As Data.SqlClient.SqlDataReader
                            Rs = Cmd.ExecuteReader()
                            If Rs.HasRows Then
                                Do While Rs.Read
                                    Session("carica_contratto") = Str(Rs("id"))
                                    Response.Redirect("contratti.aspx")
                                Loop
                            End If

                            Rs.Close()
                            Dbc.Close()
                            Rs = Nothing
                            Dbc = Nothing

                        Catch ex As Exception
                            HttpContext.Current.Response.Write(ex.Message & " Chiusura ODL --- Errore contattare amministratore del sistema.")
                        End Try
                    End If

                Case enum_odl_stato.Chiuso_Con_Continuazione_Noleggio

                Case Else
                    Err.Raise(1001, Nothing, "stato odl non gestito")
            End Select

        End If

        InitElemtiFormODL(mio_odl.num_odl)
    End Sub

    Protected Sub bt_modifica_form_odl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_modifica_form_odl.Click
        If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneODL) < "3" Then
            Libreria.genUserMsgBox(Page, "Non hai i permessi per salvare l'ODL.")
            Return
        End If

        Dim mio_odl As odl = Salva(False)

        div_edit_danno.Visible = False
    End Sub

    Protected Sub bt_stampa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_stampa.Click
        Try
            If lb_num_odl.Text = "" Or lb_num_odl.Text = "0" Then
                Libreria.genUserMsgBox(Page, "ODL ancora non salvato." & vbCrLf & "E' necessario salvare prima l'ODL per poterlo stampare.")
                Return
            End If

            If (Not Page.ClientScript.IsClientScriptBlockRegistered("NewWindow")) Then
                Dim url_print As String = "/gestione_danni/StampaODL.aspx?orientamento=verticale&num_odl=" & lb_num_odl.Text
                Dim mio_random As String = Format((New Random).Next(), "0000000000")
                Trace.Write(url_print)
                Session("url_print") = url_print
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "NewWindow", "window.open('GeneraPdf3.aspx?a=" & mio_random & "','')", True)
            End If
        Catch ex As Exception
            HttpContext.Current.Response.Write("error  bt_stampa_Click " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

    Protected Sub Add_fornitore_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Add_fornitore.Click
        anagrafica_fornitori.InitForm(stato:=2)
        Visibilita(DivVisibile.Fornitori)
    End Sub

    Protected Sub Add_drivers_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Add_drivers.Click
        anagrafica_drivers.InitForm(stato:=2)
        Visibilita(DivVisibile.Drivers)
    End Sub

    Protected Sub bt_check_out_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_check_out.Click
        gestione_checkin.InitFormCheckOut(tipo_documento.ODL, Integer.Parse(lb_num_odl.Text), 0)

        Visibilita(DivVisibile.GestioneCheck)
    End Sub

    Protected Sub bt_check_in_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_check_in.Click
        gestione_checkin.InitFormCheckIn(tipo_documento.ODL, Integer.Parse(lb_num_odl.Text), 0)

        Visibilita(DivVisibile.GestioneCheck)
    End Sub

    Protected Sub bt_nuovo_danno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_nuovo_danno.Click
        Try
            Dim mio_evento As veicoli_evento_apertura_danno = Nothing
            ' sto inserendo un nuovo danno, se non esiste l'evento lo creo
            If lb_id_evento_apertura_danno.Text = "" OrElse Integer.Parse(lb_id_evento_apertura_danno.Text) <= 0 Then
                mio_evento = New veicoli_evento_apertura_danno
                With mio_evento
                    .id_veicolo = Integer.Parse(lb_id_veicolo.Text)
                    .attivo = False

                    .id_tipo_documento_apertura = Integer.Parse(lb_id_tipo_doc_apertura.Text)
                    .id_documento_apertura = Integer.Parse(lb_num_documento.Text)
                    .num_crv = Integer.Parse(lb_num_crv.Text)

                    .data = Now
                    .nota = ""
                    .id_ditta = Nothing

                    .SalvaRecord()

                    lb_id_evento_apertura_danno.Text = .id
                End With
            End If

            If Boolean.Parse(lb_noleggio_in_corso.Text) Then
                ' solo su noleggio in corso aggiungo i danni sul gruppo danni rientro

                ' sto inserendo un nuovo danno, se non esiste il gruppo lo creo!
                Dim mio_gruppo_evento As veicoli_gruppo_evento = Nothing
                If lb_id_gruppo_danni_rientro.Text = "" OrElse Integer.Parse(lb_id_gruppo_danni_rientro.Text) <= 0 Then
                    Trace.Write("Salva Danno: " & Integer.Parse(lb_id_veicolo.Text) & " " & Integer.Parse(lb_id_tipo_doc_apertura.Text) & " " & Integer.Parse(lb_num_documento.Text) & " " & Integer.Parse(lb_num_crv.Text))

                    mio_gruppo_evento = veicoli_gruppo_evento.SalvaDanniApertiAuto(Integer.Parse(lb_id_veicolo.Text), Integer.Parse(lb_id_tipo_doc_apertura.Text), Integer.Parse(lb_num_documento.Text), Integer.Parse(lb_num_crv.Text), Integer.Parse(lb_id_evento_apertura_danno.Text))
                    lb_id_gruppo_danni_rientro.Text = mio_gruppo_evento.id
                    lb_id_gruppo_danni_filtro.Text = mio_gruppo_evento.id
                End If
            Else
                ' su ODL veicolo in parco, aggiungo invece i danni su gruppo danni durante ODL!

                ' sto inserendo un nuovo danno, se non esiste il gruppo lo creo!
                Dim mio_gruppo_evento As veicoli_gruppo_evento = Nothing
                If lb_id_gruppo_danni_durante_odl.Text = "" OrElse Integer.Parse(lb_id_gruppo_danni_durante_odl.Text) <= 0 Then
                    Trace.Write("Salva Danno: " & Integer.Parse(lb_id_veicolo.Text) & " " & Integer.Parse(lb_id_tipo_doc_apertura.Text) & " " & Integer.Parse(lb_num_documento.Text) & " " & Integer.Parse(lb_num_crv.Text))

                    mio_gruppo_evento = veicoli_gruppo_evento.SalvaDanniApertiAuto(Integer.Parse(lb_id_veicolo.Text), Integer.Parse(lb_id_tipo_doc_apertura.Text), Integer.Parse(lb_num_documento.Text), Integer.Parse(lb_num_crv.Text), Integer.Parse(lb_id_evento_apertura_danno.Text))
                    lb_id_gruppo_danni_durante_odl.Text = mio_gruppo_evento.id
                    lb_id_gruppo_danni_filtro.Text = mio_gruppo_evento.id
                End If
            End If

            edit_danno.InitForm(Integer.Parse(lb_id_evento_apertura_danno.Text), , Integer.Parse(lb_id_gruppo_danni_filtro.Text), , True)

            div_edit_danno.Visible = True


        Catch ex As Exception
            HttpContext.Current.Response.Write("error bt_nuovodanno_click " & ex.Message & "<br/>" & "<br/>")
        End Try

    End Sub

End Class
