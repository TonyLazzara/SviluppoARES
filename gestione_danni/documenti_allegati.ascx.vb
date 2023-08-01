


Public Class documenti_allegati
    Inherits ITabellaDB

    Protected m_id As Integer
    Protected m_id_tipo_documento_apertura As Integer?
    Protected m_anno As Integer?
    Protected m_id_stazione As Integer?
    Protected m_num_documento As Integer?
    Protected m_id_tipo_documento As Integer?
    Protected m_descrizione As String

    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property
    Public Property id_tipo_documento_apertura() As Integer?
        Get
            Return m_id_tipo_documento_apertura
        End Get
        Set(ByVal value As Integer?)
            m_id_tipo_documento_apertura = value
        End Set
    End Property
    Public Property anno() As Integer?
        Get
            Return m_anno
        End Get
        Set(ByVal value As Integer?)
            m_anno = value
        End Set
    End Property
    Public Property id_stazione() As Integer?
        Get
            Return m_id_stazione
        End Get
        Set(ByVal value As Integer?)
            m_id_stazione = value
        End Set
    End Property
    Public Property num_documento() As Integer?
        Get
            Return m_num_documento
        End Get
        Set(ByVal value As Integer?)
            m_num_documento = value
        End Set
    End Property
    Public Property id_tipo_documento() As Integer?
        Get
            Return m_id_tipo_documento
        End Get
        Set(ByVal value As Integer?)
            m_id_tipo_documento = value
        End Set
    End Property
    Public Property descrizione() As String
        Get
            Return m_descrizione
        End Get
        Set(ByVal value As String)
            m_descrizione = value
        End Set
    End Property


    Public Overrides Function SalvaRecord(Optional ByVal RecuperaId As Boolean = False) As Integer
        Dim sqlStr As String = "INSERT INTO documenti_allegati (id_tipo_documento_apertura,anno,id_stazione,num_documento,id_tipo_documento,descrizione)" & _
            " VALUES (@id_tipo_documento_apertura,@anno,@id_stazione,@num_documento,@id_tipo_documento,@descrizione)"

        'm_data_creazione = Now
        'm_id_utente = Integer.Parse(HttpContext.Current.Request.Cookies("SicilyRentCar")("idUtente"))

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_tipo_documento_apertura", System.Data.SqlDbType.Int, id_tipo_documento_apertura)
                addParametro(Cmd, "@anno", System.Data.SqlDbType.Int, anno)
                addParametro(Cmd, "@id_stazione", System.Data.SqlDbType.Int, id_stazione)
                addParametro(Cmd, "@num_documento", System.Data.SqlDbType.Int, num_documento)
                addParametro(Cmd, "@id_tipo_documento", System.Data.SqlDbType.Int, id_tipo_documento)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))

                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using

            ' -------------------------------------------------------------------------
            'recupero l'id del nuopvo record comunque...
            sqlStr = "SELECT @@IDENTITY FROM documenti_allegati"

            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                m_id = Cmd.ExecuteScalar
            End Using
        End Using

        Return m_id
    End Function

    Private Shared Function FillRecord(ByVal Rs As System.Data.SqlClient.SqlDataReader) As documenti_allegati
        Dim mio_record As documenti_allegati = New documenti_allegati
        With mio_record
            .id = getValueOrNohing(Rs("id"))
            .id_tipo_documento_apertura = getValueOrNohing(Rs("id_tipo_documento_apertura"))
            .anno = getValueOrNohing(Rs("anno"))
            .id_stazione = getValueOrNohing(Rs("id_stazione"))
            .num_documento = getValueOrNohing(Rs("num_documento"))
            .id_tipo_documento = getValueOrNohing(Rs("id_tipo_documento"))
            .descrizione = getValueOrNohing(Rs("descrizione"))
        End With
        Return mio_record
    End Function

    Public Shared Function getRecordDaId(ByVal id_record As Integer) As documenti_allegati
        Dim mio_record As documenti_allegati = Nothing

        Dim sqlStr As String = "SELECT * FROM documenti_allegati WITH(NOLOCK) WHERE id = " & id_record

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
    End Function

    Public Function AggiornaRecord() As Boolean
        AggiornaRecord = False

        Dim sqlStr As String = "UPDATE documenti_allegati SET" & _
            " id_tipo_documento_apertura = @id_tipo_documento_apertura," & _
            " anno = @anno," & _
            " id_stazione = @id_stazione," & _
            " num_documento = @num_documento," & _
            " id_tipo_documento = @id_tipo_documento," & _
            " descrizione = @descrizione" & _
            " WHERE id = @id"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                addParametro(Cmd, "@id_tipo_documento_apertura", System.Data.SqlDbType.Int, id_tipo_documento_apertura)
                addParametro(Cmd, "@anno", System.Data.SqlDbType.Int, anno)
                addParametro(Cmd, "@id_stazione", System.Data.SqlDbType.Int, id_stazione)
                addParametro(Cmd, "@num_documento", System.Data.SqlDbType.Int, num_documento)
                addParametro(Cmd, "@id_tipo_documento", System.Data.SqlDbType.Int, id_tipo_documento)
                addParametro(Cmd, "@descrizione", System.Data.SqlDbType.VarChar, Libreria.TrimSicuro(descrizione, 50))

                addParametro(Cmd, "@id", System.Data.SqlDbType.Int, id)

                Dbc.Open()
                Cmd.ExecuteNonQuery()
                AggiornaRecord = True
            End Using
        End Using
    End Function

    Public Function EliminaRecord() As Boolean
        Return EliminaRecord(Me.id)
    End Function

    Public Shared Function EliminaRecord(ByVal id_record As Integer) As Boolean
        EliminaRecord = False

        Dim sqlStr As String = "DELETE FROM documenti_allegati WHERE id = " & id_record

        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    EliminaRecord = True
                End Using
            End Using
        Catch ex As Exception
            HttpContext.Current.Trace.Write("EliminaRecord documenti_allegati: " & ex.Message)
        End Try
    End Function
End Class

Partial Class gestione_danni_documenti_allegati
    Inherits System.Web.UI.UserControl

    'Public Sub InitForm(ByVal id_tipo_documento As Integer, ByVal id_documento As Integer, Optional ByVal abilita_modifica_note As Boolean = False)
    '    lb_id_tipo.Text = id_tipo_documento
    '    lb_id_documento.Text = id_documento

    '    lb_lente.Text = abilita_modifica_note

    '    ListViewNote.DataBind()
    'End Sub

    'Protected Sub bt_Salva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_Salva.Click
    '    Dim mia_nota As note = New note

    '    If Integer.Parse(lb_id_documento.Text) <= 0 Then
    '        Libreria.genUserMsgBox(Page, "E' necessario prima salvare il documento, prima di poter inserire una nota.")
    '        Return
    '    End If

    '    With mia_nota
    '        .id = Integer.Parse(lb_id.Text)
    '        .id_tipo = Integer.Parse(lb_id_tipo.Text)
    '        .id_documento = Integer.Parse(lb_id_documento.Text)
    '        .nota = tx_nota.Text

    '        If .id = 0 Then
    '            .SalvaRecord()
    '        Else
    '            If Boolean.Parse(lb_lente.Text) Then
    '                .AggiornaRecord()
    '            Else
    '                .SalvaRecord()
    '            End If
    '        End If
    '    End With

    '    AzzeraRecord()

    '    ListViewNote.DataBind()
    'End Sub

    'Protected Sub FillRecord(ByVal mia_nota As note)
    '    With mia_nota
    '        lb_id.Text = .id
    '        tx_nota.Text = .nota

    '        If .id = 0 Then
    '            bt_Salva.Text = "Salva Nuova Nota"
    '            bt_Annulla.Visible = False
    '        Else
    '            If Boolean.Parse(lb_lente.Text) Then
    '                bt_Salva.Text = "Modifica Nota"
    '                bt_Annulla.Visible = True
    '            Else
    '                bt_Salva.Text = "Salva Nuova Nota"
    '                bt_Annulla.Visible = False
    '            End If
    '        End If
    '    End With
    'End Sub

    'Protected Sub AzzeraRecord()
    '    Dim mia_nota As note = New note

    '    With mia_nota
    '        .id = 0
    '        '.id_tipo = Integer.Parse(lb_id_tipo.Text)
    '        '.id_documento = Integer.Parse(lb_id_documento.Text)
    '        .nota = ""
    '    End With

    '    FillRecord(mia_nota)
    'End Sub

    'Protected Sub ListViewNote_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListViewNote.DataBound
    '    Dim th_lente As Control = ListViewNote.FindControl("th_lente")
    '    If th_lente IsNot Nothing Then
    '        th_lente.Visible = Boolean.Parse(lb_lente.Text)
    '    End If
    'End Sub

    'Protected Sub ListViewNote_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles ListViewNote.ItemCommand
    '    If e.CommandName = "lente" Then
    '        Dim lb_id As Label = e.Item.FindControl("lb_id")

    '        Dim mia_nota = note.getRecordDaId(Integer.Parse(lb_id.Text))

    '        FillRecord(mia_nota)

    '    End If
    'End Sub

    'Protected Sub bt_Annulla_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bt_Annulla.Click
    '    AzzeraRecord()
    'End Sub
End Class
