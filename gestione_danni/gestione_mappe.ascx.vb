Imports System.IO
Imports System.Collections.Generic
Imports System.Drawing

Partial Class gestione_danni_gestione_mappe
    Inherits System.Web.UI.UserControl

    Private filePath As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\images\Mappe\"
    Private IconPath As String = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\images\Icone\"

    Private Enum DivVisibile
        Nessuno = 0
        Ricerca = 1
        ElencoImmagini = 2
        NuovaImmagine = 4
        ModificaImmagine = 8
        ModelliAssociati = 16
        ModelliNonAssociati = 32
        Mappe = 64
        NuovaPosizoneDanno = 128
        EditModelli = 256
    End Enum

    Private Sub Visibilita(Valore As DivVisibile)
        Trace.Write("Visibilita generazione_mappa: " & Valore.ToString & " " & Valore)
        lb_stato_visibilita_old.Text = Valore

        div_ricerca.Visible = Valore And DivVisibile.Ricerca
        div_elenco_immagini.Visible = Valore And DivVisibile.ElencoImmagini
        div_nuova_immagine.Visible = Valore And DivVisibile.NuovaImmagine
        div_modifica_immagine.Visible = Valore And DivVisibile.ModificaImmagine
        div_elenco_modelli_associati.Visible = Valore And DivVisibile.ModelliAssociati
        div_elenco_modelli_non_ass.Visible = Valore And DivVisibile.ModelliNonAssociati
        div_mappe.Visible = Valore And DivVisibile.Mappe
        div_EditPosizioneDanno.Visible = Valore And DivVisibile.NuovaPosizoneDanno
        div_edit_modelli.Visible = Valore And DivVisibile.EditModelli

    End Sub

    ' ------------------------------------------------------------------------------------------------------
    ' come mantenere lo stato nei checkbox paginati senza db
    ' http://evonet.com.au/maintaining-checkbox-state-in-a-listview/

    Private ReadOnly Property IDs() As List(Of Integer)
        Get
            If Me.ViewState("IDs") Is Nothing Then
                Me.ViewState("IDs") = New List(Of Integer)()
            End If
            Return CType(Me.ViewState("IDs"), List(Of Integer))
        End Get
    End Property

    Protected Sub AddRowstoIDList()
        ' Loop through all the current items in the Listview
        For Each lvi As ListViewDataItem In listView_elenco_modelli_non_ass.Items
            ' Find the checkbox in each row
            Dim chkSelect As CheckBox = CType(lvi.FindControl("chkSelect"), CheckBox)
            ' If the checkbox is ticked then add the corresponding ID to our private
            ' list
            If (Not (chkSelect) Is Nothing) Then
                ' Get the ID from the datakeynames property
                Dim ID As Integer = Convert.ToInt32(listView_elenco_modelli_non_ass.DataKeys(lvi.DisplayIndex).Value)
                If (chkSelect.Checked AndAlso Not Me.IDs.Contains(ID)) Then
                    ' Add the ID to our list
                    Me.IDs.Add(ID)
                ElseIf (Not chkSelect.Checked AndAlso Me.IDs.Contains(ID)) Then
                    ' Not checked - remove the ID from our list
                    Me.IDs.Remove(ID)
                End If
            End If
        Next
    End Sub

    Protected Sub listView_elenco_modelli_non_ass_ItemCommand(sender As Object, e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listView_elenco_modelli_non_ass.ItemCommand
        If e.CommandName = "lente" Then
            Dim lb_id As Label = e.Item.FindControl("lb_id")
            lb_stato_visibilita.Text = lb_stato_visibilita_old.Text

            modelli.InitForm(Integer.Parse(lb_id.Text))

            Visibilita(DivVisibile.EditModelli)
        End If
    End Sub

    Protected Sub listView_elenco_modelli_non_ass_PagePropertiesChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.PagePropertiesChangingEventArgs) Handles listView_elenco_modelli_non_ass.PagePropertiesChanging
        AddRowstoIDList()
    End Sub

    Protected Sub listView_elenco_modelli_non_ass_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewSortEventArgs) Handles listView_elenco_modelli_non_ass.Sorting
        AddRowstoIDList()
    End Sub

    Protected Sub listView_elenco_modelli_non_ass_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles listView_elenco_modelli_non_ass.ItemDataBound
        ' Get each Listview Item on DataBound
        Dim lvi As ListViewDataItem = e.Item
        If (lvi.ItemType = ListViewItemType.DataItem) Then
            ' Find the checkbox in the current row
            Dim chkSelect As CheckBox = CType(lvi.FindControl("chkSelect"), CheckBox)
            ' Make sure we're referencing the correct control
            If (Not (chkSelect) Is Nothing) Then
                ' If the ID exists in our list then check the checkbox
                Dim ID As Integer = Convert.ToInt32(listView_elenco_modelli_non_ass.DataKeys(lvi.DisplayIndex).Value)
                chkSelect.Checked = Me.IDs.Contains(ID)
            End If
        End If
    End Sub

    Protected Function verificaModelliDisponibili(ListaModelli As List(Of Integer)) As Boolean
        verificaModelliDisponibili = False
        Dim sqlStr As String = ""
        For Each id_modello As Integer In ListaModelli
            If sqlStr = "" Then
                sqlStr = id_modello
            Else
                sqlStr += ", " & id_modello
            End If
        Next
        sqlStr = "SELECT DISTINCT m.descrizione FROM veicoli_img_associazione_modelli am" & _
            " INNER JOIN veicoli_img_modelli m ON am.id_img_modello = m.id" & _
            " WHERE am.id_modello IN (" & sqlStr & ")"

        Trace.Write(sqlStr)
        Dim Messaggio As String = ""
        Dim i As Integer = 0
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Using Rs = Cmd.ExecuteReader()
                    Do While Rs.Read
                        i += 1
                        If i > 15 Then
                            Messaggio += "..." & vbCrLf
                            Exit Do
                        End If
                        Messaggio += Rs("descrizione") & vbCrLf
                    Loop
                End Using
            End Using
        End Using
        If Messaggio <> "" Then
            Libreria.genUserMsgBox(Page, "Alcuni modelli selezionati sono già presenti nei seguenti macro modelli:" & vbCrLf & vbCrLf & _
                                   Messaggio & vbCrLf & _
                                   "Deselezionarli o eliminarli dalle associazioni già censite.")
        Else
            Return True
        End If
    End Function

    Protected Function salvaModelliSelezionati(ListaModelli As List(Of Integer)) As Boolean
        salvaModelliSelezionati = False
        Dim sqlStr As String = ""
        For Each id_modello As Integer In ListaModelli
            If sqlStr = "" Then
                sqlStr = " SELECT " & lb_id_img_modello.Text & ", " & id_modello
            Else
                sqlStr += " UNION ALL SELECT " & lb_id_img_modello.Text & ", " & id_modello
            End If
        Next

        sqlStr = "INSERT INTO veicoli_img_associazione_modelli (id_img_modello, id_modello)" & sqlStr

        Trace.Write(sqlStr)
        Try
            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                    salvaModelliSelezionati = True
                End Using
            End Using
        Catch ex As Exception
            Trace.Write("Errore in salvaModelliSelezionati: " & ex.Message)
        End Try
    End Function

    Protected Sub bt_aggiungi_modelli_Click(sender As Object, e As System.EventArgs) Handles bt_aggiungi_modelli.Click

        AddRowstoIDList()

        If Me.IDs.Count <= 0 Then
            Libreria.genUserMsgBox(Page, "Nessun modello selezionato")
            Return
        End If

        If Not verificaModelliDisponibili(Me.IDs) Then
            Return
        End If

        If salvaModelliSelezionati(Me.IDs) Then
            Me.IDs.Clear()
            lb_descrizione_modello.Text = "%" & tx_filtro_modello.Text.Trim & "%"
            listView_elenco_modelli_non_ass.DataBind()
            listView_modelli_associati.DataBind()
            Visibilita(DivVisibile.ModificaImmagine Or DivVisibile.ModelliAssociati)
        Else
            Libreria.genUserMsgBox(Page, "E' avvenuto un errore nell'aggiunta delle associazioni dei modelli.")
        End If
    End Sub

    Protected Sub bt_filtra_Click(sender As Object, e As System.EventArgs) Handles bt_filtra.Click
        Me.IDs.Clear()
        lb_descrizione_modello.Text = "%" & tx_filtro_modello.Text.Trim & "%"
        listView_elenco_modelli_non_ass.DataBind()
    End Sub

    Protected Sub bt_chiudi_filtro_Click(sender As Object, e As System.EventArgs) Handles bt_chiudi_filtro.Click
        Visibilita(DivVisibile.ModificaImmagine Or DivVisibile.ModelliAssociati)
    End Sub
    ' ------------------------------------------------------------------------------------------------------

    ' link http://www.emanueleferonato.com/2006/09/02/click-image-and-get-coordinates-with-javascript/
    ' http://www.shawngo.com/examples/image-map-coordinate-generator/
    ' http://www.w3schools.com/css/css_examples.asp

    Protected Sub DropDownPosizioneDanno_F_DataBind()
        DropDownPosizioneDanno_F.Items.Clear()
        DropDownPosizioneDanno_F.Items.Add(New ListItem("Seleziona...", "0"))
        DropDownPosizioneDanno_F.DataBind()
    End Sub

    Protected Sub DropDownPosizioneDanno_R_DataBind()
        DropDownPosizioneDanno_R.Items.Clear()
        DropDownPosizioneDanno_R.Items.Add(New ListItem("Seleziona...", "0"))
        DropDownPosizioneDanno_R.DataBind()
    End Sub

    Protected Sub ChiusuraEdit_posizione_danno(sender As Object, e As EventoNuovoRecord)
        If e.Valore > 0 Then
            If lb_add_posizione_origine.Text = Tipo_Img_Mappa.Fronte Then
                DropDownPosizioneDanno_F_DataBind()
                DropDownPosizioneDanno_F.SelectedValue = e.Valore
            Else
                DropDownPosizioneDanno_R_DataBind()
                DropDownPosizioneDanno_R.SelectedValue = e.Valore
            End If
        End If
        Visibilita(DivVisibile.Mappe)
    End Sub

    Protected Sub modelli_ChiusuraForm(sender As Object, e As System.EventArgs)
        Dim mio_stato As DivVisibile = Integer.Parse(lb_stato_visibilita.Text)
        Trace.Write("modelli_ChiusuraForm: " & mio_stato.ToString)
        Visibilita(mio_stato)
    End Sub

    Protected Sub modelli_AggiornaElenco(sender As Object, e As System.EventArgs)
        listView_elenco_modelli_non_ass.DataBind()
        listView_modelli_associati.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Trace.Write("----------- Page_Load gestione_mappe.ascx -------------")

        If Request.Cookies("SicilyRentCar") Is Nothing Then
            Response.Redirect("LogIn.aspx")
            Return
        End If
        'If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), PermessiUtente.GestioneMappaturaModelli) = "1" Then
        '    Visibilita(DivVisibile.Nessuno)
        '    Response.Redirect("default.aspx")
        '    Return
        'End If

        AddHandler posizione_danno.ChiusuraEdit, AddressOf ChiusuraEdit_posizione_danno
        AddHandler modelli.ChiusuraForm, AddressOf modelli_ChiusuraForm
        AddHandler modelli.AggiornaElenco, AddressOf modelli_AggiornaElenco

        Trace.Write("----------- Disegno Punti -------------")
        Try
            DisegnaPuntiMappa(Tipo_Img_Mappa.Fronte)
            DisegnaPuntiMappa(Tipo_Img_Mappa.Retro)
        Catch ex As Exception
            Trace.Write("Errore Disegno Punti: " & ex.Message)
        End Try

        If Not Page.IsPostBack Then
            bt_aggiungi_immagine.Visible = False
            bt_upload_fronte.Visible = False
            bt_upload_retro.Visible = False
            bt_modifica_immagine.Visible = False
            Visibilita(DivVisibile.Ricerca)
        End If

    End Sub

    Protected Sub bt_cerca_immagine_Click(sender As Object, e As System.EventArgs) Handles bt_cerca_immagine.Click
        lb_descrizione_immagine.Text = tx_descrizione_immagine.Text.Trim & "%"
        listViewElencoImmagini.DataBind()
        Visibilita(DivVisibile.Ricerca Or DivVisibile.ElencoImmagini)
    End Sub

    Private Sub FillMappa(mio_record As veicoli_img_modelli)
        With mio_record
            lb_id_img_modello.Text = .id
            tx_descrizione_img.Text = .descrizione
            tx_descrizione_new_immagine.Text = .descrizione
            lb_nome_img_fronte.Text = .img_fronte
            lb_nome_img_retro.Text = .img_retro
        End With
    End Sub

    Public Sub AzzeraMappa()
        Dim mio_record As veicoli_img_modelli = New veicoli_img_modelli
        With mio_record
            .id = 0
            .img_fronte = ""
            .img_retro = ""
            .descrizione = ""
        End With

        FillMappa(mio_record)
    End Sub

    Protected Sub bt_aggiungi_immagine_Click(sender As Object, e As System.EventArgs) Handles bt_aggiungi_immagine.Click
        AzzeraMappa()

        'Visibilita(DivVisibile.NuovaImmagine)
    End Sub

    Private Function generaNomeFile() As String
        ' generaNomeFile = System.Guid.NewGuid().ToString
        generaNomeFile = Format((New Random).Next(), "0000000000")
    End Function

    Protected Function VerificaUploadFile(myUpload As FileUpload, mappa As Tipo_Img_Mappa) As String
        If Not myUpload.HasFile Then
            Return "Selezionare un file da salvare per l'immagine " + mappa.ToString + "."
        End If

        Dim estensione As String = LCase(Right(myUpload.FileName, 4))

        If Not (estensione = ".jpg" Or estensione = ".png") Then
            Return "Il formato del file per l'immagine " + mappa.ToString + " deve essere (jpg) o (png)."
        End If

        If myUpload.PostedFile.ContentLength > 1000000 Then
            Return "Il file non può essere caricato perché supera 1MB!"
        End If

        Return ""
    End Function

    Protected Function UploadFile(myUpload As FileUpload, mappa As Tipo_Img_Mappa) As String

        Dim estensione As String = LCase(Right(myUpload.FileName, 4))

        Dim fileName As String = lb_id_img_modello.Text & "_" & mappa.ToString & "_" & generaNomeFile() & estensione
        Trace.Write("myUpload.FileName: " & myUpload.FileName & "UploadFile " & fileName)

        myUpload.SaveAs(filePath & fileName)

        If File.Exists(filePath & fileName) Then
            Return fileName
        End If

        Return ""
    End Function

    Protected Sub bt_upload_fronte_Click(sender As Object, e As System.EventArgs) Handles bt_upload_fronte.Click
        Dim mio_record As veicoli_img_modelli = veicoli_img_modelli.get_record_da_id(Integer.Parse(lb_id_img_modello.Text))
        If mio_record Is Nothing Then
            Libreria.genUserMsgBox(Page, "Errore nel recuperare le informazioni sull'immagine.")
            Return
        End If

        Dim messaggio As String = ""

        With mio_record
            messaggio = VerificaUploadFile(FileUpload_img_fronte, Tipo_Img_Mappa.Fronte)

            If messaggio = "" Then
                If File.Exists(filePath & .img_fronte) Then
                    File.Delete(filePath & .img_fronte)
                End If

                .img_fronte = UploadFile(FileUpload_img_fronte, Tipo_Img_Mappa.Fronte)
                If .img_fronte <> "" Then
                    .AggiornaRecord()
                    lb_nome_img_fronte.Text = .img_fronte
                    messaggio = "File salvato correttamente."
                Else
                    messaggio = "Errore nel salvataggio del file immagine Fronte."
                End If
            End If
        End With

        Libreria.genUserMsgBox(Page, messaggio)
    End Sub

    Protected Sub bt_upload_retro_Click(sender As Object, e As System.EventArgs) Handles bt_upload_retro.Click
        Dim mio_record As veicoli_img_modelli = veicoli_img_modelli.get_record_da_id(Integer.Parse(lb_id_img_modello.Text))
        If mio_record Is Nothing Then
            Libreria.genUserMsgBox(Page, "Errore nel recuperare le informazioni sull'immagine.")
            Return
        End If

        Dim messaggio As String = ""

        With mio_record
            messaggio = VerificaUploadFile(FileUpload_img_retro, Tipo_Img_Mappa.Retro)

            If messaggio = "" Then
                If File.Exists(filePath & .img_retro) Then
                    File.Delete(filePath & .img_retro)
                End If

                .img_retro = UploadFile(FileUpload_img_retro, Tipo_Img_Mappa.Retro)
                If .img_retro <> "" Then
                    .AggiornaRecord()
                    lb_nome_img_retro.Text = .img_retro
                    messaggio = "File salvato correttamente."
                Else
                    messaggio = "Errore nel salvataggio del file immagine Retro."
                End If
            End If
        End With

        Libreria.genUserMsgBox(Page, messaggio)
    End Sub

    Protected Sub bt_salva_new_immagine_Click(sender As Object, e As System.EventArgs) Handles bt_salva_new_immagine.Click
        If tx_descrizione_new_immagine.Text.Trim = "" Then
            Libreria.genUserMsgBox(Page, "Il campo descrizione modello deve essere valorizzato.")
            Return
        End If

        Dim mio_record As veicoli_img_modelli = New veicoli_img_modelli
        With mio_record
            .descrizione = tx_descrizione_new_immagine.Text

            If Not veicoli_img_modelli.verificaDescrizione(.descrizione) Then
                Libreria.genUserMsgBox(Page, "Descrizione modello già presente nel sitema." & vbCrLf & "Scegliere una descrizione differente.")
                Return
            End If

            lb_id_img_modello.Text = .SalvaRecord()

            Dim messaggio_fronte As String = VerificaUploadFile(FileUpload_new_img_fronte, Tipo_Img_Mappa.Fronte)

            If messaggio_fronte = "" Then
                .img_fronte = UploadFile(FileUpload_new_img_fronte, Tipo_Img_Mappa.Fronte)

                If .img_fronte = "" Then
                    messaggio_fronte = "Errore nel salvataggio del file immagine Fronte."
                    .img_fronte = Nothing
                Else
                    lb_nome_img_fronte.Text = .img_fronte
                End If
            Else
                .img_fronte = Nothing
            End If

            Dim messaggio_retro As String = VerificaUploadFile(FileUpload_new_img_retro, Tipo_Img_Mappa.Retro)
            If messaggio_retro = "" Then
                .img_retro = UploadFile(FileUpload_new_img_retro, Tipo_Img_Mappa.Retro)

                If .img_retro = "" Then
                    messaggio_retro = "Errore nel salvataggio del file immagine Retro."
                    .img_retro = Nothing
                Else
                    lb_nome_img_retro.Text = .img_retro
                End If
            Else
                .img_retro = Nothing
            End If

            .AggiornaRecord()

            If messaggio_fronte <> "" Or messaggio_retro <> "" Then
                Libreria.genUserMsgBox(Page, (messaggio_fronte & vbCrLf & messaggio_retro).Trim)
            End If
        End With

        tx_descrizione_img.Text = tx_descrizione_new_immagine.Text

        listView_modelli_associati.DataBind()

        listViewElencoImmagini.DataBind()

        Visibilita(DivVisibile.ModificaImmagine Or DivVisibile.ModelliAssociati)
    End Sub

    Protected Sub bt_chiudi_new_immagine_Click(sender As Object, e As System.EventArgs) Handles bt_chiudi_new_immagine.Click
        Visibilita(DivVisibile.Ricerca Or DivVisibile.ElencoImmagini)
    End Sub

    Protected Sub bt_modifica_immagine_Click(sender As Object, e As System.EventArgs) Handles bt_modifica_immagine.Click
        Dim mio_record As veicoli_img_modelli = veicoli_img_modelli.get_record_da_id(Integer.Parse(lb_id_img_modello.Text))
        With mio_record
            .descrizione = tx_descrizione_img.Text

            If Not .verificaDescrizione() Then
                Libreria.genUserMsgBox(Page, "Descrizione modello già presente nel sitema." & vbCrLf & "Scegliere una descrizione differente.")
                Return
            End If

            .AggiornaRecord()
        End With

        listViewElencoImmagini.DataBind()

        Libreria.genUserMsgBox(Page, "Descrizione modello correttamente salvata.")
    End Sub

    Protected Sub bt_chiudi_immagine_Click(sender As Object, e As System.EventArgs) Handles bt_chiudi_immagine.Click
        Visibilita(DivVisibile.Ricerca Or DivVisibile.ElencoImmagini)
    End Sub

    Protected Sub listViewElencoImmagini_ItemCommand(sender As Object, e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listViewElencoImmagini.ItemCommand
        If e.CommandName = "lente" Then
            Dim lb_id As Label = e.Item.FindControl("lb_id")

            Dim mio_record As veicoli_img_modelli = veicoli_img_modelli.get_record_da_id(Integer.Parse(lb_id.Text))

            FillMappa(mio_record)

            Visibilita(DivVisibile.ModificaImmagine Or DivVisibile.ModelliAssociati)
        ElseIf e.CommandName = "elimina" Then
            Dim lb_id As Label = e.Item.FindControl("lb_id")

            Dim mio_record As veicoli_img_modelli = veicoli_img_modelli.get_record_da_id(Integer.Parse(lb_id.Text))
            If mio_record Is Nothing Then
                Libreria.genUserMsgBox(Page, "Errore nel recuperare le informazioni del record selezionato.")
                Return
            End If

            If mio_record.id = Costanti.id_mappa_default Then
                Libreria.genUserMsgBox(Page, "Non è possibile cancellare il modello di default.")
                Return
            End If

            With mio_record
                ' elimino le immagini
                If .img_fronte IsNot Nothing AndAlso .img_fronte <> "" Then
                    File.Delete(filePath & .img_fronte)
                End If

                If .img_retro IsNot Nothing AndAlso .img_retro <> "" Then
                    File.Delete(filePath & .img_retro)
                End If

                ' elimino le associazioni con i modelli
                Dim sqlStr As String = "DELETE FROM [veicoli_img_associazione_modelli] WHERE [id_img_modello] = " & .id
                Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                    Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                        Dbc.Open()
                        Cmd.ExecuteNonQuery()
                    End Using
                End Using

                ' elimino il record
                .EliminaRecord()
            End With

            listViewElencoImmagini.DataBind()
        End If
    End Sub

    Protected Sub listView_modelli_associati_ItemCommand(sender As Object, e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listView_modelli_associati.ItemCommand
        If e.CommandName = "lente" Then
            Dim lb_id As Label = e.Item.FindControl("lb_id")

            lb_stato_visibilita.Text = lb_stato_visibilita_old.Text

            modelli.InitForm(Integer.Parse(lb_id.Text))

            Visibilita(DivVisibile.EditModelli)

        ElseIf e.CommandName = "elimina" Then
            Dim lb_id As Label = e.Item.FindControl("lb_id")

            Dim sqlStr As String = "DELETE FROM veicoli_img_associazione_modelli" & _
                " WHERE id_img_modello = " & lb_id_img_modello.Text & _
                " AND id_modello = " & lb_id.Text
            Trace.Write(sqlStr)

            Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                    Dbc.Open()
                    Cmd.ExecuteNonQuery()
                End Using
            End Using

            listView_modelli_associati.DataBind()
        End If
    End Sub

    Protected Sub bt_aggiungi_modello_Click(sender As Object, e As System.EventArgs) Handles bt_aggiungi_modello.Click
        If lb_id_img_modello.Text = Costanti.id_mappa_default Then
            Libreria.genUserMsgBox(Page, "Non è possibile associare modellia alla mappa di default.")
            Return
        End If
        tx_filtro_modello.Text = ""
        Me.IDs.Clear()
        lb_descrizione_modello.Text = "%" & tx_filtro_modello.Text.Trim & "%"
        listView_elenco_modelli_non_ass.DataBind()
        Visibilita(DivVisibile.ModelliNonAssociati)
    End Sub

    Protected Sub AzzeraCampiMappa()
        Trace.Write("AzzeraCampiMappa")
        tx_X_F.Text = 0
        tx_Y_F.Text = 0
        tx_Y_R.Text = 0
        tx_Y_R.Text = 0
        DropDownPosizioneDanno_F.SelectedValue = 0
        DropDownPosizioneDanno_R.SelectedValue = 0
    End Sub

    Protected Sub bt_mappa_modello_Click(sender As Object, e As System.EventArgs) Handles bt_mappa_modello.Click
        Dim mio_record As veicoli_img_modelli = veicoli_img_modelli.get_record_da_id(Integer.Parse(lb_id_img_modello.Text))
        With mio_record
            img_fronte.ImageUrl = "~\images\Mappe\" & .img_fronte
            img_retro.ImageUrl = "~\images\Mappe\" & .img_retro
        End With

        AzzeraCampiMappa()

        'DisegnaPuntiMappa(Tipo_Img_Mappa.Fronte)
        'DisegnaPuntiMappa(Tipo_Img_Mappa.Retro)

        Visibilita(DivVisibile.Mappe)
    End Sub

    Protected Sub bt_chiudi_mappatura_F_Click(sender As Object, e As System.EventArgs) Handles bt_chiudi_mappatura_F.Click
        Visibilita(DivVisibile.ModificaImmagine Or DivVisibile.ModelliAssociati)
    End Sub

    Protected Sub bt_salva_nuovo_punto_F_Click(sender As Object, e As System.EventArgs) Handles bt_salva_nuovo_punto_F.Click
        Dim mio_punto_mappa As veicoli_img_mappatura = New veicoli_img_mappatura
        With mio_punto_mappa
            .tipo_img = Tipo_Img_Mappa.Fronte

            .id_img_modelli = Integer.Parse(lb_id_img_modello.Text)
            .id_posizione_danno = Integer.Parse(DropDownPosizioneDanno_F.SelectedValue)
            Trace.Write("[" & tx_X_F.Text & "][" & tx_Y_F.Text & "]")
            .x = Integer.Parse(tx_X_F.Text)
            .y = Integer.Parse(tx_Y_F.Text)

            Try
                .SalvaRecord()

                AzzeraCampiMappa()

                Dim myImage As ImageButton = NuovaImmagine(mio_punto_mappa)
                div_img_fronte.Controls.Add(myImage)

            Catch ex As Exception
                Libreria.genUserMsgBox(Page, "Errore nel salvataggio del punto mappa." & vbCrLf & "Verificare che la posizione danno selezionato non sia già censito.")
            End Try

            'DisegnaPuntiMappa(Tipo_Img_Mappa.Fronte)
            'DisegnaPuntiMappa(Tipo_Img_Mappa.Retro)
        End With
    End Sub

    Protected Sub bt_chiudi_mappatura_R_Click(sender As Object, e As System.EventArgs) Handles bt_chiudi_mappatura_R.Click
        Visibilita(DivVisibile.ModificaImmagine Or DivVisibile.ModelliAssociati)
    End Sub

    Protected Sub bt_salva_nuovo_punto_R_Click(sender As Object, e As System.EventArgs) Handles bt_salva_nuovo_punto_R.Click
        Dim mio_punto_mappa As veicoli_img_mappatura = New veicoli_img_mappatura
        With mio_punto_mappa
            .tipo_img = Tipo_Img_Mappa.Retro

            .id_img_modelli = Integer.Parse(lb_id_img_modello.Text)
            .id_posizione_danno = Integer.Parse(DropDownPosizioneDanno_R.SelectedValue)
            Trace.Write("[" & tx_X_R.Text & "][" & tx_Y_R.Text & "]")
            .x = Integer.Parse(tx_X_R.Text)
            .y = Integer.Parse(tx_Y_R.Text)

            Try
                .SalvaRecord()

                AzzeraCampiMappa()

                Dim myImage As ImageButton = NuovaImmagine(mio_punto_mappa)
                div_img_retro.Controls.Add(myImage)

            Catch ex As Exception
                Libreria.genUserMsgBox(Page, "Errore nel salvataggio del punto mappa." & vbCrLf & "Verificare che la posizione danno selezionato non sia già censito.")
            End Try


            'DisegnaPuntiMappa(Tipo_Img_Mappa.Fronte)
            'DisegnaPuntiMappa(Tipo_Img_Mappa.Retro)
        End With
    End Sub

    Protected Function getDescrPosizioneDanno(id_record As Integer) As String
        Dim Valore As String = ""
        Dim sqlStr As String
        sqlStr = "SELECT descrizione FROM veicoli_posizione_danno WHERE id = " & id_record

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Valore = Cmd.ExecuteScalar & ""
            End Using
        End Using

        Return Valore
    End Function

    Protected Function getIconaRiferimento() As Bitmap
        Dim myBitmap As Bitmap = New Bitmap(IconPath & "cerchio.png")

        Return myBitmap
    End Function

    Protected Function getNomeIcona(Index As Integer) As String
        getNomeIcona = "cerchio_" & Format(Index, "00") & ".png"
    End Function

    Protected Sub SalvaImmagine(myBitmap As Bitmap, Index As Integer)
        myBitmap.Save(IconPath & getNomeIcona(Index), System.Drawing.Imaging.ImageFormat.Png)
    End Sub

    Protected Function getImage(Index As Integer) As Bitmap
        Dim myBitmap As Bitmap = getIconaRiferimento()

        Dim g As Graphics = Graphics.FromImage(myBitmap)
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias

        Dim strFormat As StringFormat = New StringFormat()
        strFormat.Alignment = StringAlignment.Center
        strFormat.LineAlignment = StringAlignment.Center
        g.DrawString(Index, New Font("Tahoma", 9), Brushes.Black, New RectangleF(0, 0, 20, 20), strFormat)

        Return myBitmap
    End Function

    Protected Function fileEsiste(Index As Integer) As Boolean
        Return File.Exists(IconPath & getNomeIcona(Index)) 
    End Function

    Protected Function creaIcona(Index As Integer) As Boolean
        Dim myBitmap As Bitmap = getImage(Index)
        SalvaImmagine(myBitmap, Index)
        Return fileEsiste(Index)
    End Function

    Protected Function NuovaImmagine(mio_record As veicoli_img_mappatura, Optional Index As Integer = -1) As ImageButton
        Dim myImage As ImageButton = New ImageButton()

        With mio_record
            myImage.ID = "myImage_" & .id & "_end"
            Trace.Write(">>>>>>>>>>>>>>>> NuovaImmagine: " & myImage.ID)
            myImage.ToolTip = getDescrPosizioneDanno(.id_posizione_danno)
            If myImage.ToolTip = "" Then
                myImage.ToolTip = "N.V."
            End If

            If Index > 0 Then
                If fileEsiste(Index) Then
                    myImage.ImageUrl = "/images/Icone/" & getNomeIcona(Index)
                Else
                    If creaIcona(Index) Then
                        myImage.ImageUrl = "/images/Icone/" & getNomeIcona(Index)
                    Else
                        myImage.ImageUrl = "/images/Icone/centro.png"
                    End If
                End If
            Else
                myImage.ImageUrl = "/images/Icone/centro.png"
            End If


            myImage.Style.Add(HtmlTextWriterStyle.Position, "absolute")
            myImage.Style.Add(HtmlTextWriterStyle.Width, Costanti.DeltaIconaX & "px")
            myImage.Style.Add(HtmlTextWriterStyle.Height, Costanti.DeltaIconaY & "px")
            myImage.Style.Add(HtmlTextWriterStyle.Left, (.x - Costanti.DeltaIconaX \ 2) & "px")
            myImage.Style.Add(HtmlTextWriterStyle.Top, (.y - Costanti.DeltaIconaY \ 2) & "px")

            AddHandler myImage.Click, AddressOf myImage_Click

            myImage.OnClientClick = "javascript:return (window.confirm('Confermi la cancellazione dell\'elemento mappato?'))"
        End With

        Return myImage
    End Function

    Protected Sub DisegnaPuntiMappa(mappa As Tipo_Img_Mappa)
        If lb_id_img_modello.Text = "" Then
            Return
        End If
        Dim mia_lista As List(Of I_veicoli_img_mappatura) = veicoli_img_mappatura.get_lista_punti_mappa(Integer.Parse(lb_id_img_modello.Text), mappa)
        Dim Index As Integer = 0
        For Each mio_record As veicoli_img_mappatura In mia_lista
            Index += 1
            Dim myImage As ImageButton = NuovaImmagine(mio_record)

            Select Case mappa
                Case Tipo_Img_Mappa.Fronte
                    div_img_fronte.Controls.Add(myImage)
                Case Tipo_Img_Mappa.Retro
                    div_img_retro.Controls.Add(myImage)
            End Select
        Next
    End Sub

    Private Sub EliminaPuntoMappa(id_record_mappa As Integer)
        veicoli_img_mappatura.EliminaRecord(id_record_mappa)
    End Sub

    Private Sub myImage_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs)
        Dim myImage As ImageButton = DirectCast(sender, ImageButton)
        Trace.Write(">>>>>>>>>>>>>>>> myImage_Click: " & myImage.ID)
        Dim Val() As String = myImage.ID.Split("_"c)

        EliminaPuntoMappa(Integer.Parse(Val(1)))

        Try
            div_img_fronte.Controls.Remove(myImage)
        Catch ex As Exception

        End Try
        Try
            div_img_retro.Controls.Remove(myImage)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Add_PosizioneDanno_F_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Add_PosizioneDanno_F.Click
        Trace.Write("Add_PosizioneDanno_F_Click")
        lb_add_posizione_origine.Text = Tipo_Img_Mappa.Fronte
        posizione_danno.InitForm(stato:=3)
        Visibilita(DivVisibile.NuovaPosizoneDanno)
    End Sub

    Protected Sub Add_PosizioneDanno_R_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Add_PosizioneDanno_R.Click
        Trace.Write("Add_PosizioneDanno_R_Click")
        lb_add_posizione_origine.Text = Tipo_Img_Mappa.Retro
        posizione_danno.InitForm(stato:=3)
        Visibilita(DivVisibile.NuovaPosizoneDanno)
    End Sub

End Class
