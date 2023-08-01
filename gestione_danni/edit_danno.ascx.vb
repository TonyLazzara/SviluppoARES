Imports System.IO
Imports System.Collections.Generic
Imports AjaxControlToolkit

Partial Class gestione_danni_edit_danno
    Inherits System.Web.UI.UserControl

    Public Delegate Sub EventoChiusuraForm(ByVal sender As Object, ByVal e As EventArgs)
    Event ChiusuraForm As EventHandler

    Public Delegate Sub EventoAggiornaElenco(ByVal sender As Object, ByVal e As EventArgs)
    Event AggiornaElenco As EventHandler

    Protected Enum DivVisibile
        Nessuno = 0
        Intestazione = 1
        EditDanno = 2
        EditDocDanno = 4

    End Enum

    Protected Sub Visibilita(ByVal Valore As DivVisibile)
        'Trace.Write("Visibilita edit_danno: " & Valore.ToString)
        div_targa.Visible = Valore And DivVisibile.Intestazione

        div_mappe.Visible = Valore And (DivVisibile.EditDanno Or DivVisibile.EditDocDanno)

        div_acessori.Visible = Valore And DivVisibile.EditDanno

        div_acessori_read.Visible = Valore And (DivVisibile.EditDocDanno)

        div_elenco_documenti_F.Visible = Valore And DivVisibile.EditDocDanno
        'div_elenco_documenti_R.Visible = Valore And DivVisibile.EditDocDanno
        div_upload_meccanici.Visible = Valore And DivVisibile.EditDocDanno

        If Valore And DivVisibile.EditDocDanno Then
            btnSalvaNuovo_F.Visible = False
            'r btnSalvaNuovo_R.Visible = False
            ' bt_salva_acessori.Visible = False
            bt_salva_danno_meccanico.Visible = False

            'btnChiudiNuovo_F.Visible = False
            'btnChiudiNuovo_R.Visible = False
        Else
            btnSalvaNuovo_F.Visible = True
            'r btnSalvaNuovo_R.Visible = True
            ' bt_salva_acessori.Visible = True
            bt_salva_danno_meccanico.Visible = True

            'btnChiudiNuovo_F.Visible = True
            'btnChiudiNuovo_R.Visible = True
        End If

        If Integer.Parse(lb_stato_rds.Text) > 0 Then
            div_da_addebitare_F.Visible = Valore And DivVisibile.EditDocDanno
            'r div_da_addebitare_R.Visible = Valore And DivVisibile.EditDocDanno
            div_da_addebitare_M.Visible = Valore And DivVisibile.EditDocDanno
            div_da_addebitare_D.Visible = Valore And DivVisibile.EditDocDanno
        Else
            div_da_addebitare_F.Visible = False
            'r div_da_addebitare_R.Visible = False
            div_da_addebitare_M.Visible = False
            div_da_addebitare_D.Visible = False
        End If

    End Sub

    ' variabili di comodo per gestire il codice duplicato dei due pannelli
    Protected DropDownPosizione As DropDownList
    Protected DropDownTipoDanno As DropDownList
    Protected DropDownEntita As DropDownList
    Protected tx_descrizione As TextBox
    Protected listViewDocumenti As ListView
    Protected FileUpload1 As FileUpload
    Protected DropDownTipoDocumentoImg As DropDownList
    Protected DropDownNonAddebito As DropDownList

    Protected btnInviaFile As Button
    Protected btnModifica As Button
    '---------------------------------------------------------------------

    Protected Sub SetVariabili(Posizione As Tipo_Img_Mappa)
        'Trace.Write("SetVariabili: " & Posizione.ToString)
        Select Case Posizione
            Case Tipo_Img_Mappa.Fronte
                DropDownPosizione = DropDownPosizione_F
                'Trace.Write("DropDownPosizione.Items.count: " & DropDownPosizione.Items.Count & " - " & DropDownPosizione_F.Items.Count)
                DropDownTipoDanno = DropDownTipoDanno_F
                DropDownEntita = DropDownEntita_F
                tx_descrizione = tx_descrizione_F
                listViewDocumenti = listViewDocumenti_F
                FileUpload1 = FileUpload1_F
                DropDownTipoDocumentoImg = DropDownTipoDocumentoImg_F
                DropDownNonAddebito = DropDownNonAddebito_F

                btnInviaFile = btnInviaFile_F
                btnModifica = btnModifica_F
            Case Tipo_Img_Mappa.Retro
                'r DropDownPosizione = DropDownPosizione_R
                'r DropDownTipoDanno = DropDownTipoDanno_R
                'r  DropDownEntita = DropDownEntita_R
                'r  tx_descrizione = tx_descrizione_R
                'r listViewDocumenti = listViewDocumenti_R
                'r FileUpload1 = FileUpload1_R
                'r DropDownTipoDocumentoImg = DropDownTipoDocumentoImg_R
                'r DropDownNonAddebito = DropDownNonAddebito_R

                'r btnInviaFile = btnInviaFile_R
                'r  btnModifica = btnModifica_R
        End Select
    End Sub
    ' --------------------------------------------------------------------

    Protected Sub AbilitaControlliEdit(Valore As Boolean)
        For i As Tipo_Img_Mappa = Tipo_Img_Mappa.Fronte To Tipo_Img_Mappa.Retro
            SetVariabili(i)
            DropDownPosizione.Enabled = Valore

            DropDownTipoDanno.Enabled = Valore

            DropDownEntita.Enabled = Valore

            tx_descrizione.Enabled = Valore
            listViewDocumenti.Enabled = Valore
            FileUpload1.Enabled = Valore
            btnInviaFile.Enabled = Valore
            btnModifica.Enabled = Valore

            DropDownTipoDocumentoImg.Enabled = Valore

            lb_disabilita_mappa.Text = (Not Valore).ToString.ToLower
        Next
    End Sub

    Protected Sub my_aggiorna_form(ByVal sender As Object, ByVal e As EventoAbilita)
        'Trace.Write("my_aggiorna_form: " & e.Valore)

        AbilitaControlliEdit(e.Valore)
    End Sub

    Protected Sub DropDownPosizione_DataBind(Optional id_select_value As Integer = 0)
        ' Trace.Write("DropDownPosizione_DataBinding")
        For i As Tipo_Img_Mappa = Tipo_Img_Mappa.Fronte To Tipo_Img_Mappa.Retro
            SetVariabili(i)
            DropDownPosizione.Items.Clear()
            DropDownPosizione.Items.Add(New ListItem("Seleziona...", "0"))
            DropDownPosizione.DataBind()
            DropDownPosizione.SelectedValue = id_select_value
        Next
        'DropDownPosizione.Enabled = False   '06.12.2020

    End Sub

    Protected Sub DropDownTipoDanno_DataBind(Optional id_select_value As Integer = 0)
        'Trace.Write("DropDownTipoDanno_DataBinding")
        For i As Tipo_Img_Mappa = Tipo_Img_Mappa.Fronte To Tipo_Img_Mappa.Retro
            SetVariabili(i)
            DropDownTipoDanno.Items.Clear()
            DropDownTipoDanno.Items.Add(New ListItem("Seleziona...", "0"))
            DropDownTipoDanno.DataBind()
            DropDownTipoDanno.SelectedValue = id_select_value
        Next
    End Sub

    Protected Sub DropDownTipoDocumentoImg_DataBind(Optional id_select_value As Integer = 0)
        'Trace.Write("DropDownTipoDocumentoImg_DataBinding")
        For i As Tipo_Img_Mappa = Tipo_Img_Mappa.Fronte To Tipo_Img_Mappa.Retro
            SetVariabili(i)
            DropDownTipoDocumentoImg.Items.Clear()
            DropDownTipoDocumentoImg.Items.Add(New ListItem("Seleziona...", "0"))
            DropDownTipoDocumentoImg.DataBind()
            DropDownTipoDocumentoImg.SelectedValue = id_select_value
        Next
    End Sub

    Protected Sub listViewDocumenti_DataBind()
        'Trace.Write("listViewDocumenti_DataBind")

        listViewDocumenti_F.DataBind()
        'r listViewDocumenti_R.DataBind()
    End Sub

    Protected Sub gestVisibilita()
        If lb_id_danno.Text = "0" Then
            Visibilita(DivVisibile.EditDanno)
        Else
            Visibilita(DivVisibile.EditDocDanno)
        End If
    End Sub

    Protected Sub DisabilitaControlli(Valore As Boolean)
        For i As Tipo_Img_Mappa = Tipo_Img_Mappa.Fronte To Tipo_Img_Mappa.Retro
            SetVariabili(i)

            DropDownPosizione.Enabled = Not Valore
            DropDownTipoDanno.Enabled = Not Valore
            DropDownEntita.Enabled = Not Valore
            tx_descrizione.Enabled = Not Valore

            DropDownTipoRecordDanno.Enabled = Not Valore
            tx_descrizione_meccanico.Enabled = Not Valore

            lb_disabilita_mappa.Text = (Valore).ToString.ToLower
        Next
    End Sub

    Protected Function DeterminaTabAttivo(mio_record As veicoli_danni) As Integer
        DeterminaTabAttivo = 0
        Dim mio_macro_modello As veicoli_img_modelli = ViewState("veicoli_img_modelli")

        Dim sqlStr As String = "SELECT tipo_img FROM veicoli_img_mappatura WITH(NOLOCK)" & _
            " WHERE id_img_modelli = " & mio_macro_modello.id & _
            " AND id_posizione_danno = " & mio_record.id_posizione_danno

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Dim tipo_img As String = Cmd.ExecuteScalar & ""
                If tipo_img = "2" Then
                    Return 1
                Else
                    Return 0
                End If
            End Using
        End Using
    End Function

    Protected Sub AbilitaTab(Valore As Boolean)
        tab_fronte.Enabled = Valore
        'r tab_retro.Enabled = Valore
        tab_accessori.Enabled = Valore
        tab_meccanici.Enabled = Valore
    End Sub

    Protected Sub FillForm(mio_record As veicoli_danni)
        Select Case mio_record.tipo_record
            Case tipo_record_danni.Danno_Carrozzeria
                tab_mappe.ActiveTabIndex = DeterminaTabAttivo(mio_record)
                If tab_mappe.ActiveTabIndex = 0 Then
                    tab_fronte.Enabled = True
                Else
                    'r  tab_retro.Enabled = True
                End If

                FillDanno(mio_record)
                AzzeraDannoMeccanico()
            Case tipo_record_danni.Danno_Meccanico, tipo_record_danni.Danno_Elettrico, tipo_record_danni.Altro
                tab_mappe.ActiveTabIndex = 3

                tab_meccanici.Enabled = True

                AzzeraDanno()
                FillDannoMeccanico(mio_record)
            Case tipo_record_danni.Accessori, tipo_record_danni.Dotazione
                tab_mappe.ActiveTabIndex = 2

                tab_accessori.Enabled = True

                AzzeraDanno()
                AzzeraDannoMeccanico()

                ListViewDotazioni_read.DataBind()
                ListViewAccessori_read.DataBind()
        End Select
    End Sub

    Public Sub InitForm(ByVal id_evento As Integer, Optional ByVal id_danno As Integer = 0, Optional ByVal id_gruppo_evento As Integer = 0, Optional ByVal stato_rds As Integer = 0, Optional ByVal attesa_rds As Boolean = False)
        lb_stato_rds.Text = stato_rds
        lb_attesa_rds.text = attesa_rds

        'Trace.Write("InitForm - id_evento: " & id_evento)
        lb_id_evento.Text = id_evento
        lb_id_danno.Text = id_danno
        lb_id_gruppo_evento.Text = id_gruppo_evento

        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(id_evento)
        If mio_evento Is Nothing Then
            
            Libreria.genUserMsgBox(Page, "Evento danno con id (" & id_evento & ") non trovato.")
            Return
            Visibilita(DivVisibile.Nessuno)
        End If
        With mio_evento
            lb_id_veicolo.Text = .id_veicolo
            lb_id_tipo_documento.Text = .id_tipo_documento_apertura ' non ancora gestito !!! solo contratto per adesso!
            lb_id_documento.Text = .id_documento_apertura
            lb_id_ditta.Text = .id_ditta & ""

            Dim mio_macro_modello As veicoli_img_modelli = veicoli_img_modelli.getMacroModello(.id_veicolo)
            With mio_macro_modello
                img_fronte.ImageUrl = "~\images\Mappe\" & .img_fronte
                'r img_retro.ImageUrl = "~\images\Mappe\" & .img_retro
            End With
            ViewState("veicoli_img_modelli") = mio_macro_modello

            If id_danno = 0 Then
                
                lb_id_posizione_danno.Text = 0

                AzzeraDanno()

                AbilitaTab(True)

                DisabilitaControlli(False)

                Try
                    DisegnaPuntiMappa(mio_macro_modello.id)
                Catch ex As Exception
                    'Trace.Write("Errore Disegno Punti: " & ex.Message)
                End Try

                Visibilita(DivVisibile.EditDanno)
            Else
                Dim mio_record As veicoli_danni = veicoli_danni.getRecordDaId(id_danno)

                If mio_record.id_posizione_danno Is Nothing Then
                    lb_id_posizione_danno.Text = 0
                Else
                    lb_id_posizione_danno.Text = mio_record.id_posizione_danno
                End If


                AbilitaTab(False)

                FillForm(mio_record)

                DisabilitaControlli(True)

                Try
                    DisegnaPuntiMappa(mio_macro_modello.id, True, mio_record.id_posizione_danno)
                Catch ex As Exception
                    'Trace.Write("Errore Disegno Punti: " & ex.Message)
                End Try

                Visibilita(DivVisibile.EditDocDanno)
            End If
        End With
    End Sub

    Public Sub InitFormModifiche(ByVal id_evento As Integer, ByVal valore As String, Optional ByVal id_danno As Integer = 0, Optional ByVal id_gruppo_evento As Integer = 0, Optional ByVal stato_rds As Integer = 0, Optional ByVal attesa_rds As Boolean = False)
        'Response.Write(valore)

        lb_stato_rds.Text = stato_rds
        lb_attesa_rds.Text = attesa_rds

        'Trace.Write("InitForm - id_evento: " & id_evento)
        lb_id_evento.Text = id_evento
        lb_id_danno.Text = id_danno
        lb_id_gruppo_evento.Text = id_gruppo_evento

        Dim mio_evento As veicoli_evento_apertura_danno = veicoli_evento_apertura_danno.getRecordDaId(id_evento)
        If mio_evento Is Nothing Then

            Libreria.genUserMsgBox(Page, "Evento danno con id (" & id_evento & ") non trovato.")
            Return
            Visibilita(DivVisibile.Nessuno)
        End If
        With mio_evento
            lb_id_veicolo.Text = .id_veicolo
            lb_id_tipo_documento.Text = .id_tipo_documento_apertura ' non ancora gestito !!! solo contratto per adesso!
            lb_id_documento.Text = .id_documento_apertura
            lb_id_ditta.Text = .id_ditta & ""

            Dim mio_macro_modello As veicoli_img_modelli = veicoli_img_modelli.getMacroModello(.id_veicolo)
            With mio_macro_modello
                img_fronte.ImageUrl = "~\images\Mappe\" & .img_fronte
                'r img_retro.ImageUrl = "~\images\Mappe\" & .img_retro
            End With
            ViewState("veicoli_img_modelli") = mio_macro_modello

            If id_danno = 0 Then

                lb_id_posizione_danno.Text = 0

                AzzeraDanno()

                AbilitaTab(True)

                DisabilitaControlli(False)

                Try
                    DisegnaPuntiMappa(mio_macro_modello.id)
                Catch ex As Exception
                    'Trace.Write("Errore Disegno Punti: " & ex.Message)
                End Try

                Visibilita(DivVisibile.EditDanno)
            Else
                Dim mio_record As veicoli_danni = veicoli_danni.getRecordDaId(id_danno)

                If mio_record.id_posizione_danno Is Nothing Then
                    lb_id_posizione_danno.Text = 0
                Else
                    lb_id_posizione_danno.Text = mio_record.id_posizione_danno
                End If


                AbilitaTab(True)

                FillForm(mio_record)

                DisabilitaControlli(False)

                Try
                    DisegnaPuntiMappa(mio_macro_modello.id, True, mio_record.id_posizione_danno)
                Catch ex As Exception
                    'Trace.Write("Errore Disegno Punti: " & ex.Message)
                End Try

                Visibilita(DivVisibile.EditDocDanno)

                'RDS (Nuovo-Modifiche-Eliminazione)
                If funzioni_comuni.getLivelloAccesso(Request.Cookies("SicilyRentCar")("idUtente"), "168") = 3 Then                                        
                    DropDownTipoDanno_F.Enabled = True
                    DropDownEntita_F.Enabled = True

                    btnSalvaNuovo_F.Visible = True
                    btnSalvaNuovo_F.Text = "Modifica Danno"
                End If
            End If
        End With
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        lb_id_a_carico_del_cliente.Text = Costanti.id_a_carico_del_cliente

        If Not Page.IsPostBack Then

            DropDownPosizione_DataBind()
            DropDownTipoDanno_DataBind()
            DropDownTipoDocumentoImg_DataBind()

            DropDownNonAddebito_F.DataBind()
            'r DropDownNonAddebito_R.DataBind()
            DropDownNonAddebito_M.DataBind()

            Visibilita(DivVisibile.EditDanno)

            tab_mappe.ActiveTabIndex = 0
            Session("errorloadeditdanno") = "0"
        Else

            ''''DA VERIFICARE 27.11.2020

            Dim mio_macro_modello As veicoli_img_modelli
            If IsNothing(ViewState("veicoli_img_modelli")) Then

                mio_macro_modello = veicoli_img_modelli.getMacroModello(lb_id_veicolo.Text)
                With mio_macro_modello
                    img_fronte.ImageUrl = "~\images\Mappe\" & .img_fronte
                    'r img_retro.ImageUrl = "~\images\Mappe\" & .img_retro
                End With
                ViewState("veicoli_img_modelli") = mio_macro_modello

            End If
            Try
                mio_macro_modello = ViewState("veicoli_img_modelli")
            Catch ex As Exception
                Response.Write("mio_macro_modello: " & ex.Message & "<br/>")
            End Try


            If lb_id_posizione_danno.Text = "" Then
                lb_id_posizione_danno.Text = 0
            End If

            Try
                'Eliminato solo x test DA REINSERIRE 
                ' DisegnaPuntiMappa(1, True, Integer.Parse(lb_id_posizione_danno.Text))
                DisegnaPuntiMappa(mio_macro_modello.id, True, Integer.Parse(lb_id_posizione_danno.Text))


                Session("errorloadeditdanno") = "0"
            Catch ex As Exception
                'Trace.Write("Errore Disegno Punti: " & ex.Message)
                Response.Write("ErrorePageLoad: " & ex.Message & "<br/>")
                Session("errorloadeditdanno") = "1"
            End Try



        End If
    End Sub

    Public Sub DisegnaPuntiMappa(id_img_modello As Integer, Optional evidenzia_selezionato As Boolean = False, Optional id_posizione_danno As Integer = 0)
        'Trace.Write("DisegnaPuntiMappa: " & id_img_modello & " " & evidenzia_selezionato & " " & id_posizione_danno)

        Try
            For i As Tipo_Img_Mappa = Tipo_Img_Mappa.Fronte To Tipo_Img_Mappa.Retro
                Dim mia_lista As List(Of I_veicoli_img_mappatura) = veicoli_img_mappatura.get_lista_punti_mappa(id_img_modello, i)
                If evidenzia_selezionato Then
                    SetVariabili(i)
                    For Each mio_record As veicoli_img_mappatura In mia_lista
                        mio_record.id_posizione_danno_selezionato = id_posizione_danno
                    Next
                End If

                Select Case i
                    Case Tipo_Img_Mappa.Fronte
                        veicoli_img_mappatura_indicizzata.DisegnaSuContenitore(div_img_fronte, mia_lista, i)
                    Case Tipo_Img_Mappa.Retro
                        'r veicoli_img_mappatura_indicizzata.DisegnaSuContenitore(div_img_retro, mia_lista, i)
                End Select
            Next
        Catch ex As Exception
            Response.Write("Errore DisegnaPuntiMappa: " & ex.Message & "<br/>")
        End Try


    End Sub

    Protected Sub FillDanno(mio_record As veicoli_danni)

        For i As Tipo_Img_Mappa = Tipo_Img_Mappa.Fronte To Tipo_Img_Mappa.Retro
            SetVariabili(i)
            With mio_record
                DropDownPosizione.SelectedValue = .id_posizione_danno
                DropDownTipoDanno.SelectedValue = .id_tipo_danno
                DropDownEntita.SelectedValue = .entita_danno
                tx_descrizione.Text = .descrizione
                If .motivo_non_addebito Is Nothing Then
                    DropDownNonAddebito.SelectedValue = 0
                Else
                    DropDownNonAddebito.SelectedValue = .motivo_non_addebito
                End If
            End With

            DropDownTipoDocumentoImg.SelectedValue = 0
        Next

        'DropDownPosizione.Enabled = False '06.12.2020

    End Sub

    Protected Sub AzzeraDanno()
        Dim mio_record As veicoli_danni = New veicoli_danni
        With mio_record
            .id = 0
            .id_evento_apertura = lb_id_evento.Text
            .id_veicolo = lb_id_veicolo.Text
            .tipo_record = tipo_record_danni.Danno_Carrozzeria
            .id_posizione_danno = 0
            .id_tipo_danno = 0
            .entita_danno = 0
            .descrizione = ""
        End With

        FillDanno(mio_record)
    End Sub

    Protected Sub btnSalvaNuovo_F_Click(sender As Object, e As System.EventArgs) Handles btnSalvaNuovo_F.Click
        SetVariabili(Tipo_Img_Mappa.Fronte)

        'Tony 05/10/2022
        'btnSalvaNuovo_Click(sender, e)
        
        Select Case btnSalvaNuovo_F.Text
            Case Is = "Salva Nuovo Danno"
                btnSalvaNuovo_Click(sender, e)
            Case Is = "Modifica Danno"                
                ModificaDanno(lb_id_danno.Text, Integer.Parse(Request.Form("lblpos")), DropDownTipoDanno_F.SelectedValue, DropDownEntita_F.SelectedValue)
                Libreria.genUserMsgBox(Page, "Danno modificato correttamente.")
                RaiseEvent AggiornaElenco(Me, New EventArgs)
        End Select
        'FINE Tony
    End Sub

    ''r Protected Sub btnSalvaNuovo_R_Click(sender As Object, e As System.EventArgs) Handles btnSalvaNuovo_R.Click
    '    SetVariabili(Tipo_Img_Mappa.Retro)

    '    btnSalvaNuovo_Click(sender, e)
    'End Sub

    Protected Sub btnSalvaNuovo_Click(sender As Object, e As System.EventArgs)
        Dim mio_danno As veicoli_danni = New veicoli_danni

        'DropDownPosizione.Enabled = True    'abilita prima di recuperare valore ?? 23.06.2022

        With mio_danno
            .attivo = False
            .tipo_record = tipo_record_danni.Danno_Carrozzeria
            If Integer.Parse(lb_id_evento.Text) = 0 Then
                .id_evento_apertura = Integer.Parse(Session("IdEventoApertura_mio"))
            Else
                .id_evento_apertura = Integer.Parse(lb_id_evento.Text)
            End If

            If Integer.Parse(lb_id_veicolo.Text) = 0 Then
                .id_veicolo = Integer.Parse(Session("IdVeicolo_mio"))
            Else
                .id_veicolo = Integer.Parse(lb_id_veicolo.Text)
            End If

            '.id_posizione_danno = Integer.Parse(DropDownPosizione.SelectedValue)
            .id_posizione_danno = Integer.Parse(Request.Form("lblpos"))                 'il valore della posizione è recuperata dalla lbl perchè il ddl list viene valorizzato da java 23.06.2022 salvo
            .id_tipo_danno = Integer.Parse(DropDownTipoDanno.SelectedValue)
            .entita_danno = Integer.Parse(DropDownEntita.SelectedValue)
            .descrizione = tx_descrizione.Text
            If lb_id_ditta.Text <> "" Then
                .id_ditta = Integer.Parse(lb_id_ditta.Text)
            End If

            'Response.Write(.id_evento_apertura)
            'Response.Write(.id_veicolo)
            'Response.Write(.id_posizione_danno)
            'Response.Write(.id_tipo_danno)
            'Response.Write(.entita_danno)
            'Response.Write(.descrizione)
            'Response.Write(.id_ditta)

            'Response.End()
            .SalvaRecord()
            ' lb_id_danno.Text = .SalvaRecord()

            If Boolean.Parse(lb_attesa_rds.Text) Then
                ' ho richiamato questo modulo per aggiungere un danno in corso d'opera
                ' anche se il danno non risulta attivo ancora!!!
                ' lo diventa solo se viene salvato dal modulo di check_in!
                veicoli_gruppo_evento.AggiungiDanno(Integer.Parse(lb_id_gruppo_evento.Text), .id)
            End If
        End With

        Libreria.genUserMsgBox(Page, "Danno salvato correttamente.")

        RaiseEvent AggiornaElenco(Me, New EventArgs)

        Dim mio_macro_modello As veicoli_img_modelli = ViewState("veicoli_img_modelli")

        Try
            DisegnaPuntiMappa(mio_macro_modello.id)
        Catch ex As Exception
            'Trace.Write("Errore Disegno Punti: " & ex.Message)
        End Try

        AzzeraDanno()

        ' Visibilita(DivVisibile.EditDocDanno)
    End Sub

    Protected Sub btnChiudiNuovo_F_Click(sender As Object, e As System.EventArgs) Handles btnChiudiNuovo_F.Click
        RaiseEvent ChiusuraForm(Me, New EventArgs)
    End Sub

    ''r Protected Sub btnChiudiNuovo_R_Click(sender As Object, e As System.EventArgs) Handles btnChiudiNuovo_R.Click
    '    RaiseEvent ChiusuraForm(Me, New EventArgs)
    'End Sub

    Protected Sub btnModifica_F_Click(sender As Object, e As System.EventArgs) Handles btnModifica_F.Click
        SetVariabili(Tipo_Img_Mappa.Fronte)

        btnModifica_Click(sender, e)
    End Sub

    'r 'Protected Sub btnModifica_R_Click(sender As Object, e As System.EventArgs) Handles btnModifica_R.Click
    '    SetVariabili(Tipo_Img_Mappa.Retro)

    '    btnModifica_Click(sender, e)
    'End Sub

    Protected Sub btnModifica_Click(sender As Object, e As System.EventArgs)
        Dim mio_danno As veicoli_danni = veicoli_danni.getRecordDaId(Integer.Parse(lb_id_danno.Text))
        With mio_danno
            .id_posizione_danno = Integer.Parse(DropDownPosizione.SelectedValue)
            .id_tipo_danno = Integer.Parse(DropDownTipoDanno.SelectedValue)
            .entita_danno = Integer.Parse(DropDownEntita.SelectedValue)
            .descrizione = tx_descrizione.Text

            .AggiornaRecord()
        End With

        Libreria.genUserMsgBox(Page, "Danno modificato correttamente.")

        RaiseEvent AggiornaElenco(Me, New EventArgs)

        Dim mio_macro_modello As veicoli_img_modelli = ViewState("veicoli_img_modelli")
        lb_id_posizione_danno.Text = mio_danno.id_posizione_danno
        Try
            DisegnaPuntiMappa(mio_macro_modello.id, Not DropDownEntita_F.Enabled, mio_danno.id_posizione_danno)
        Catch ex As Exception
            'Trace.Write("Errore Disegno Punti: " & ex.Message)
        End Try
    End Sub

    Protected Sub btnChiudiModifica_F_Click(sender As Object, e As System.EventArgs) Handles btnChiudiModifica_F.Click
        RaiseEvent ChiusuraForm(Me, New EventArgs)
    End Sub

    ''r Protected Sub btnChiudiModifica_R_Click(sender As Object, e As System.EventArgs) Handles btnChiudiModifica_R.Click
    '    RaiseEvent ChiusuraForm(Me, New EventArgs)
    'End Sub

    Protected Function getModelloAutoDaId(id_veicolo As Integer) As String
        Dim sqlStr As String
        sqlStr = "SELECT m.descrizione AS modello" & _
            " FROM veicoli v WITH(NOLOCK)" & _
            " INNER JOIN modelli m WITH(NOLOCK) ON v.id_modello = m.id_modello" & _
            " WHERE v.id = '" & id_veicolo & "'"
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getModelloAutoDaId = Cmd.ExecuteScalar & ""
            End Using
        End Using
    End Function

    Protected Function getStazioneAutoDaId(id_veicolo As Integer) As String
        Dim sqlStr As String
        sqlStr = "SELECT (s.codice + ' - '+ s.nome_stazione) AS Stazione" & _
            " FROM veicoli v WITH(NOLOCK)" & _
            " INNER JOIN stazioni s WITH(NOLOCK) ON v.id_stazione = s.id" & _
            " WHERE v.id = '" & id_veicolo & "'"
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getStazioneAutoDaId = Cmd.ExecuteScalar & ""
            End Using
        End Using
    End Function

    Private Function getKmDaIdVeicolo(id_veicolo As Integer) As String
        Dim sqlStr As String
        sqlStr = "SELECT km_attuali FROM veicoli WITH(NOLOCK) where id = " & id_veicolo
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getKmDaIdVeicolo = Cmd.ExecuteScalar & ""
            End Using
        End Using
    End Function

    Private Function getTargaIdVeicolo(id_veicolo As Integer) As String
        Dim sqlStr As String
        sqlStr = "SELECT targa FROM veicoli WITH(NOLOCK) where id = " & id_veicolo
        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                getTargaIdVeicolo = Cmd.ExecuteScalar & ""
            End Using
        End Using
    End Function

    Protected Sub InitIntestazione()
        lb_modello.Text = getModelloAutoDaId(Integer.Parse(lb_id_veicolo.Text))
        lb_stazione.Text = getStazioneAutoDaId(Integer.Parse(lb_id_veicolo.Text))
        lb_km.Text = getKmDaIdVeicolo(Integer.Parse(lb_id_veicolo.Text))
        lb_targa.Text = getTargaIdVeicolo(Integer.Parse(lb_id_veicolo.Text))
    End Sub

    Protected Function generaNomeFile() As String
        generaNomeFile = System.Guid.NewGuid().ToString
    End Function

    Protected Sub btnInviaFile_F_Click(sender As Object, e As System.EventArgs) Handles btnInviaFile_F.Click
        SetVariabili(Tipo_Img_Mappa.Fronte)

        btnInviaFile_Click(sender, e)
    End Sub

    ''r Protected Sub btnInviaFile_R_Click(sender As Object, e As System.EventArgs) Handles btnInviaFile_R.Click
    '    SetVariabili(Tipo_Img_Mappa.Retro)

    '    btnInviaFile_Click(sender, e)
    'End Sub

    Protected Sub InviaFile(MioFileUpload As FileUpload, id_veicolo As Integer, id_danno As Integer, id_tipo_documento As Integer)
        Dim Messaggio As String = ""
        If MioFileUpload.HasFile Then
            Dim Estensione As String = LCase(Right(MioFileUpload.FileName, 4))
            If Estensione = ".jpg" Or Estensione = ".png" Or Estensione = ".pdf" Then
                If MioFileUpload.PostedFile.ContentLength <= 1000000 Then
                    Dim filePath As String
                    filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\images\FotoDanni\"
                    Dim NomeFile As String
                    NomeFile = generaNomeFile()

                    Dim fileNameBig As String = id_veicolo & "_" & id_danno & "_" & _
                        id_tipo_documento & "_" & NomeFile & Estensione

                    MioFileUpload.SaveAs(filePath & fileNameBig)

                    If File.Exists(filePath & fileNameBig) Then
                        Dim mia_foto_danno As veicoli_danni_foto = New veicoli_danni_foto
                        With mia_foto_danno
                            .id_danno = id_danno
                            .descrizione = MioFileUpload.FileName
                            .riferimento_foto = fileNameBig
                            .tipo_documento = id_tipo_documento

                            .SalvaRecord()
                        End With

                        Messaggio = "Immagine correttamente salvata."
                    End If
                Else
                    Messaggio = "Il file non può essere caricato perché supera 1MB!"
                End If
            Else
                Messaggio = "L'estensione dell'immagine deve essere una delle seguenti (.jpg, .png, .pdf)"
            End If
        Else
            Messaggio = "Selezionare un file da salvare."
        End If

        If Messaggio <> "" Then
            Libreria.genUserMsgBox(Page, Messaggio)
        End If
    End Sub

    Protected Sub btnInviaFile_Click(sender As Object, e As System.EventArgs)
        InviaFile(FileUpload1, Integer.Parse(lb_id_veicolo.Text), Integer.Parse(lb_id_danno.Text), DropDownTipoDocumentoImg_F.SelectedValue)

        DropDownTipoDocumentoImg.SelectedValue = 0

        listViewDocumenti_DataBind()

        'Dim Messaggio As String = ""
        'If FileUpload1.HasFile Then
        '    Dim Estensione As String = LCase(Right(FileUpload1.FileName, 4))
        '    If Estensione = ".jpg" Or Estensione = ".png" Or Estensione = ".pdf" Then
        '        Trace.Write("FileUpload1.PostedFile.ContentLength:" & FileUpload1.PostedFile.ContentLength)
        '        If FileUpload1.PostedFile.ContentLength <= 1000000 Then
        '            Dim filePath As String
        '            filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\images\FotoDanni\"
        '            Dim NomeFile As String
        '            NomeFile = generaNomeFile()

        '            Dim fileNameBig As String = lb_id_veicolo.Text & "_" & lb_id_danno.Text & "_" & _
        '                DropDownTipoDocumentoImg.SelectedValue & "_" & NomeFile & Estensione

        '            FileUpload1.SaveAs(filePath & fileNameBig)

        '            If File.Exists(filePath & fileNameBig) Then
        '                Dim mia_foto_danno As veicoli_danni_foto = New veicoli_danni_foto
        '                With mia_foto_danno
        '                    .id_danno = Integer.Parse(lb_id_danno.Text)
        '                    .descrizione = FileUpload1.FileName
        '                    .riferimento_foto = fileNameBig
        '                    .tipo_documento = DropDownTipoDocumentoImg.SelectedValue

        '                    .SalvaRecord()
        '                End With

        '                listViewDocumenti_DataBind()

        '                DropDownTipoDocumentoImg.SelectedValue = 0

        '                Messaggio = "Immagine correttamente salvata."
        '            End If
        '        Else
        '            Messaggio = "Il file non può essere caricato perché supera 1MB!"
        '        End If
        '    Else
        '        Messaggio = "L'estensione dell'immagine deve essere una delle seguenti (.jpg, .png, .pdf)"
        '    End If
        'Else
        '    Messaggio = "Selezionare un file da salvare."
        'End If

        'If Messaggio <> "" Then
        '    Libreria.genUserMsgBox(Page, Messaggio)
        'End If
    End Sub

    Protected Sub listViewDocumenti_F_ItemCommand(sender As Object, e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listViewDocumenti_F.ItemCommand
        SetVariabili(Tipo_Img_Mappa.Fronte)

        listViewDocumenti_ItemCommand(sender, e)
    End Sub

    ''r Protected Sub listViewDocumenti_R_ItemCommand(sender As Object, e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listViewDocumenti_R.ItemCommand
    '    SetVariabili(Tipo_Img_Mappa.Retro)

    '    listViewDocumenti_ItemCommand(sender, e)
    'End Sub

    Protected Sub listViewDocumenti_ItemCommand(sender As Object, e As System.Web.UI.WebControls.ListViewCommandEventArgs)
        'Trace.Write("listViewFoto_ItemCommand: " & e.CommandName)

        If e.CommandName = "elimina" Then
            Dim id_riga As Label = e.Item.FindControl("lb_id")

            Dim mia_foto As veicoli_danni_foto = veicoli_danni_foto.get_foto_da_id(Integer.Parse(id_riga.Text))

            Dim filePath As String
            filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\images\FotoDanni\"

            File.Delete(filePath & mia_foto.riferimento_foto)
            If File.Exists(filePath & mia_foto.riferimento_foto) Then
                Libreria.genUserMsgBox(Page, "Errore nella cancellazione della foto.")
                Return
            End If

            If mia_foto.CancellaFoto() Then
                Libreria.genUserMsgBox(Page, "Foto cancellata correttamente.")
            End If

            listViewDocumenti_DataBind()
        End If
    End Sub

    Protected Sub listViewDanniMeccanici_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) Handles listViewDanniMeccanici.ItemCommand
        'Trace.Write("listViewFoto_ItemCommand: " & e.CommandName)

        If e.CommandName = "elimina" Then
            Dim id_riga As Label = e.Item.FindControl("lb_id")

            Dim mia_foto As veicoli_danni_foto = veicoli_danni_foto.get_foto_da_id(Integer.Parse(id_riga.Text))

            Dim filePath As String
            filePath = HttpContext.Current.Request.MapPath(HttpContext.Current.Request.ApplicationPath) & "\images\FotoDanni\"



            File.Delete(filePath & mia_foto.riferimento_foto)
            If File.Exists(filePath & mia_foto.riferimento_foto) Then
                Libreria.genUserMsgBox(Page, "Errore nella cancellazione della foto.")

                Return
            End If

            If mia_foto.CancellaFoto() Then
                Libreria.genUserMsgBox(Page, "Foto cancellata correttamente.")
            End If

            listViewDanniMeccanici.DataBind()
        End If
    End Sub

    Protected Sub FillDannoMeccanico(mio_danno As veicoli_danni)
        With mio_danno
            lb_id_danno_meccanico.Text = .id
            DropDownTipoRecordDanno.SelectedValue = .tipo_record
            tx_descrizione_meccanico.Text = .descrizione
            If .motivo_non_addebito Is Nothing Then
                DropDownNonAddebito_M.SelectedValue = 0
            Else
                DropDownNonAddebito_M.SelectedValue = .motivo_non_addebito
            End If
        End With
    End Sub

    Protected Sub AzzeraDannoMeccanico()
        Dim mio_danno As veicoli_danni = New veicoli_danni
        With mio_danno
            .id = 0
            .tipo_record = 0
            .id_veicolo = Integer.Parse(lb_id_veicolo.Text)
            .id_posizione_danno = Nothing
            .entita_danno = Nothing
            .id_tipo_danno = Nothing
        End With

        FillDannoMeccanico(mio_danno)
    End Sub

    Protected Sub bt_salva_danno_meccanico_Click(sender As Object, e As System.EventArgs) Handles bt_salva_danno_meccanico.Click
        Dim mio_danno As veicoli_danni = New veicoli_danni
        With mio_danno
            .tipo_record = DropDownTipoRecordDanno.SelectedValue
            .id_evento_apertura = Integer.Parse(lb_id_evento.Text)
            .id_veicolo = Integer.Parse(lb_id_veicolo.Text)
            .id_posizione_danno = Nothing
            .id_tipo_danno = Nothing
            .entita_danno = Nothing
            .descrizione = tx_descrizione_meccanico.Text
            If lb_id_ditta.Text <> "" Then
                .id_ditta = Integer.Parse(lb_id_ditta.Text)
            End If

            .SalvaRecord()
            ' lb_id_danno.Text = .SalvaRecord()

            If Boolean.Parse(lb_attesa_rds.Text) Then
                ' ho richiamato questo modulo per aggiungere un danno in corso d'opera
                ' anche se il danno non risulta attivo ancora!!!
                ' lo diventa solo se viene salvato dal modulo di check_in!
                veicoli_gruppo_evento.AggiungiDanno(Integer.Parse(lb_id_gruppo_evento.Text), .id)
            End If
        End With

        AzzeraDannoMeccanico()

        Libreria.genUserMsgBox(Page, "Danno salvato correttamente.")

        RaiseEvent AggiornaElenco(Me, New EventArgs)
    End Sub

    Protected Function SalvaDotazioneAcessoriAssenti() As Boolean
        SalvaDotazioneAcessoriAssenti = False

        Dim sqlStr As String = "DELETE FROM veicoli_danni" & _
            " WHERE attivo = 0" & _
            " AND id_evento_apertura = " & lb_id_evento.Text & _
            " AND tipo_record IN (4,5)"
        ' Dotazione = 4
        ' Accessori = 5

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using

        For Each lvi As ListViewDataItem In ListViewDotazioni.Items
            Dim chkSelect As CheckBox = CType(lvi.FindControl("chkSelect"), CheckBox)
            Dim ID As Integer = Convert.ToInt32(ListViewDotazioni.DataKeys(lvi.DisplayIndex).Value)
            'Trace.Write("Dotazione: " & ID)
            If chkSelect.Checked Then
                Dim mio_danno As veicoli_danni = New veicoli_danni
                With mio_danno
                    .attivo = False
                    .tipo_record = tipo_record_danni.Dotazione
                    .id_evento_apertura = Integer.Parse(lb_id_evento.Text)
                    .id_veicolo = Integer.Parse(lb_id_veicolo.Text)
                    .id_posizione_danno = Nothing
                    .id_tipo_danno = Nothing
                    .entita_danno = Nothing
                    .descrizione = Nothing
                    .id_dotazione = ID
                    .id_acessori = Nothing
                    If lb_id_ditta.Text <> "" Then
                        .id_ditta = Integer.Parse(lb_id_ditta.Text)
                    End If

                    .SalvaRecord()

                    If Boolean.Parse(lb_attesa_rds.Text) Then
                        ' ho richiamato questo modulo per aggiungere un danno in corso d'opera
                        ' anche se il danno non risulta attivo ancora!!!
                        ' lo diventa solo se viene salvato dal modulo di check_in!
                        veicoli_gruppo_evento.AggiungiDanno(Integer.Parse(lb_id_gruppo_evento.Text), .id)
                    End If
                End With
            End If
        Next

        For Each lvi As ListViewDataItem In ListViewAcessori.Items
            Dim chkSelect As CheckBox = CType(lvi.FindControl("chkSelect"), CheckBox)
            Dim ID As Integer = Convert.ToInt32(ListViewAcessori.DataKeys(lvi.DisplayIndex).Value)
            'Trace.Write("Accessori: " & ID)

            If chkSelect.Checked Then
                Dim mio_danno As veicoli_danni = New veicoli_danni
                With mio_danno
                    .attivo = False
                    .tipo_record = tipo_record_danni.Accessori
                    .id_evento_apertura = Integer.Parse(lb_id_evento.Text)
                    .id_veicolo = Integer.Parse(lb_id_veicolo.Text)
                    .id_posizione_danno = Nothing
                    .id_tipo_danno = Nothing
                    .entita_danno = Nothing
                    .descrizione = Nothing
                    .id_dotazione = Nothing
                    .id_acessori = ID
                    If lb_id_ditta.Text <> "" Then
                        .id_ditta = Integer.Parse(lb_id_ditta.Text)
                    End If

                    .SalvaRecord()

                    If Boolean.Parse(lb_attesa_rds.Text) Then
                        ' ho richiamato questo modulo per aggiungere un danno in corso d'opera
                        ' anche se il danno non risulta attivo ancora!!!
                        ' lo diventa solo se viene salvato dal modulo di check_in!
                        veicoli_gruppo_evento.AggiungiDanno(Integer.Parse(lb_id_gruppo_evento.Text), .id)
                    End If
                End With
            End If
        Next

        SalvaDotazioneAcessoriAssenti = True
    End Function

    Protected Sub bt_salva_acessori_Click(sender As Object, e As System.EventArgs) Handles bt_salva_acessori.Click

        SalvaDotazioneAcessoriAssenti()

        RaiseEvent AggiornaElenco(Me, New EventArgs)
    End Sub

    Protected Sub bt_salva_doc_meccanico_Click(sender As Object, e As System.EventArgs) Handles bt_salva_doc_meccanico.Click

        InviaFile(FileUploadMeccanici, Integer.Parse(lb_id_veicolo.Text), Integer.Parse(lb_id_danno.Text), DropDownTipoDocMeccanico.SelectedValue)

        DropDownTipoDocMeccanico.SelectedValue = 0

        listViewDanniMeccanici.DataBind()
    End Sub

    Protected Sub ListViewDotazioni_read_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles ListViewDotazioni_read.ItemDataBound
        Dim lvi As ListViewDataItem = e.Item
        If (lvi.ItemType = ListViewItemType.DataItem) Then

            Dim lb_assente As Label = CType(lvi.FindControl("lb_assente"), Label)
            Dim lb_presente As Label = CType(lvi.FindControl("lb_presente"), Label)
            Dim lb_dotazione As Label = CType(lvi.FindControl("lb_dotazione"), Label)

            Dim chkSelect As CheckBox = CType(lvi.FindControl("chkSelect"), CheckBox)
            Dim ck_da_addebitare As CheckBox = CType(lvi.FindControl("ck_da_addebitare"), CheckBox)

            Dim lb_da_addebitare As Label = CType(lvi.FindControl("lb_da_addebitare"), Label)
            Dim lb_motivo_non_addebito As Label = CType(lvi.FindControl("lb_motivo_non_addebito"), Label)
            Dim DropDownNonAddebito As DropDownList = CType(lvi.FindControl("DropDownNonAddebito"), DropDownList)

            If lb_assente.Text Then
                lb_presente.Text = "No"
            Else
                lb_presente.Text = "Si"
            End If
            If lb_dotazione.Text = "0" Then
                chkSelect.Checked = False
            Else
                chkSelect.Checked = True
                lb_presente.Text = "Si"
            End If
            If lb_presente.Text = "Si" And chkSelect.Checked Then
                ck_da_addebitare.Enabled = True

                If lb_da_addebitare.Text = "1" Or lb_da_addebitare.Text = "True" Then
                    ck_da_addebitare.Checked = True
                End If
                If lb_motivo_non_addebito.Text <> "" Then
                    DropDownNonAddebito.SelectedValue = lb_motivo_non_addebito.Text
                Else
                    DropDownNonAddebito.SelectedValue = 0
                End If
            Else
                ck_da_addebitare.Enabled = False
                DropDownNonAddebito.Enabled = False
                DropDownNonAddebito.SelectedValue = 0
            End If
        End If
    End Sub

    Protected Sub ListViewAccessori_read_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.ListViewItemEventArgs) Handles ListViewAccessori_read.ItemDataBound
        Dim lvi As ListViewDataItem = e.Item
        If (lvi.ItemType = ListViewItemType.DataItem) Then

            Dim chkSelect As CheckBox = CType(lvi.FindControl("chkSelect"), CheckBox)
            Dim ck_da_addebitare As CheckBox = CType(lvi.FindControl("ck_da_addebitare"), CheckBox)

            Dim lb_da_addebitare As Label = CType(lvi.FindControl("lb_da_addebitare"), Label)
            Dim lb_motivo_non_addebito As Label = CType(lvi.FindControl("lb_motivo_non_addebito"), Label)
            Dim DropDownNonAddebito As DropDownList = CType(lvi.FindControl("DropDownNonAddebito"), DropDownList)

            ck_da_addebitare.Enabled = chkSelect.Checked
            DropDownNonAddebito.Enabled = chkSelect.Checked
            If chkSelect.Checked Then
                If lb_da_addebitare.Text = "1" Or lb_da_addebitare.Text = "True" Then
                    ck_da_addebitare.Checked = True
                End If
                If lb_motivo_non_addebito.Text <> "" Then
                    DropDownNonAddebito.SelectedValue = lb_motivo_non_addebito.Text
                Else
                    DropDownNonAddebito.SelectedValue = 0
                End If
            Else
                DropDownNonAddebito.SelectedValue = 0
                ck_da_addebitare.Checked = False
            End If
        End If
    End Sub

    'r 'Protected Sub bt_da_addebitare_Click(sender As Object, e As System.EventArgs) Handles bt_da_addebitare_F.Click, bt_da_addebitare_R.Click, bt_da_addebitare_M.Click
    '    veicoli_danni.DaAddebitare(Integer.Parse(lb_id_danno.Text), True, 0)

    '    RaiseEvent AggiornaElenco(Me, New EventArgs)
    'End Sub

    Protected Sub bt_da_non_addebitare_F_Click(sender As Object, e As System.EventArgs) Handles bt_da_non_addebitare_F.Click
        veicoli_danni.DaAddebitare(Integer.Parse(lb_id_danno.Text), False, Integer.Parse(DropDownNonAddebito_F.SelectedValue))

        RaiseEvent AggiornaElenco(Me, New EventArgs)
    End Sub

    ''r Protected Sub bt_da_non_addebitare_R_Click(sender As Object, e As System.EventArgs) Handles bt_da_non_addebitare_R.Click
    '    veicoli_danni.DaAddebitare(Integer.Parse(lb_id_danno.Text), False, Integer.Parse(DropDownNonAddebito_R.SelectedValue))

    '    RaiseEvent AggiornaElenco(Me, New EventArgs)
    'End Sub

    Protected Sub bt_da_non_addebitare_M_Click(sender As Object, e As System.EventArgs) Handles bt_da_non_addebitare_M.Click
        veicoli_danni.DaAddebitare(Integer.Parse(lb_id_danno.Text), False, Integer.Parse(DropDownNonAddebito_M.SelectedValue))

        RaiseEvent AggiornaElenco(Me, New EventArgs)
    End Sub

    Protected Function SalvaDotazioneAcessoriAddebitare() As Boolean
        SalvaDotazioneAcessoriAddebitare = False
        ' Dotazione = 4
        ' Accessori = 5

        Dim sqlStr As String = "UPDATE veicoli_danni SET" & _
            " da_addebitare = 0," & _
            " motivo_non_addebito = 0" & _
            " WHERE attivo = 1" & _
            " AND id_evento_apertura = " & lb_id_evento.Text & _
            " AND tipo_record IN (4,5)"

        Using Dbc As New System.Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Using Cmd As New System.Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dbc.Open()
                Cmd.ExecuteNonQuery()
            End Using
        End Using

        For Each lvi As ListViewDataItem In ListViewDotazioni_read.Items
            Dim ck_da_addebitare As CheckBox = CType(lvi.FindControl("ck_da_addebitare"), CheckBox)
            Dim lb_id_accessorio As Label = CType(lvi.FindControl("lb_id_accessorio"), Label)
            Dim DropDownNonAddebito As DropDownList = CType(lvi.FindControl("DropDownNonAddebito"), DropDownList)

            If ck_da_addebitare.Enabled Then
                veicoli_danni.DaAddebitareDotazione(Integer.Parse(lb_id_evento.Text), Integer.Parse(lb_id_accessorio.Text), ck_da_addebitare.Checked, Integer.Parse(DropDownNonAddebito.SelectedValue))
            End If
        Next

        For Each lvi As ListViewDataItem In ListViewAccessori_read.Items
            Dim ck_da_addebitare As CheckBox = CType(lvi.FindControl("ck_da_addebitare"), CheckBox)
            Dim lb_id_accessorio As Label = CType(lvi.FindControl("lb_id_accessorio"), Label)
            Dim DropDownNonAddebito As DropDownList = CType(lvi.FindControl("DropDownNonAddebito"), DropDownList)

            If ck_da_addebitare.Enabled Then
                veicoli_danni.DaAddebitareAccessorio(Integer.Parse(lb_id_evento.Text), Integer.Parse(lb_id_accessorio.Text), ck_da_addebitare.Checked, Integer.Parse(DropDownNonAddebito.SelectedValue))
            End If
        Next

        SalvaDotazioneAcessoriAddebitare = True
    End Function

    Protected Function verifica_accessori_non_addebitabile() As Boolean
        verifica_accessori_non_addebitabile = False
        Dim messaggio As String = ""

        For Each lvi As ListViewDataItem In ListViewDotazioni_read.Items
            Dim ck_da_addebitare As CheckBox = CType(lvi.FindControl("ck_da_addebitare"), CheckBox)
            Dim DropDownNonAddebito As DropDownList = CType(lvi.FindControl("DropDownNonAddebito"), DropDownList)
            Dim lb_descrizione As Label = CType(lvi.FindControl("lb_descrizione"), Label)
            ' Trace.Write(ck_da_addebitare.Checked & " " & DropDownNonAddebito.SelectedValue & " " & lb_descrizione.Text)
            If ck_da_addebitare.Enabled Then
                If Not ck_da_addebitare.Checked Then
                    If DropDownNonAddebito.SelectedValue = 0 Then
                        messaggio += "Specificare il motivo di non addebito per la dotazione (" & lb_descrizione.Text & ")" & vbCrLf
                    End If
                End If
            End If
        Next

        For Each lvi As ListViewDataItem In ListViewAccessori_read.Items
            Dim ck_da_addebitare As CheckBox = CType(lvi.FindControl("ck_da_addebitare"), CheckBox)
            Dim DropDownNonAddebito As DropDownList = CType(lvi.FindControl("DropDownNonAddebito"), DropDownList)
            Dim lb_descrizione As Label = CType(lvi.FindControl("lb_descrizione"), Label)
            ' Trace.Write(ck_da_addebitare.Checked & " " & DropDownNonAddebito.SelectedValue & " " & lb_descrizione.Text)
            If ck_da_addebitare.Enabled Then
                If Not ck_da_addebitare.Checked Then
                    If DropDownNonAddebito.SelectedValue = 0 Then
                        messaggio += "Specificare il motivo di non addebito per l'accessorio (" & lb_descrizione.Text & ")" & vbCrLf
                    End If
                End If
            End If
        Next
        ' Trace.Write("verifica_accessori_non_addebitabile - Messaggio " & messaggio)

        If messaggio <> "" Then
            Libreria.genUserMsgBox(Page, messaggio)
            Return False
        Else
            Return True
        End If
    End Function

    Protected Sub bt_da_addebitare_D_Click(sender As Object, e As System.EventArgs) Handles bt_salva_da_addebitare.Click
        If Not verifica_accessori_non_addebitabile() Then
            Return
        End If

        SalvaDotazioneAcessoriAddebitare()

        RaiseEvent AggiornaElenco(Me, New EventArgs)
    End Sub

    'Tony 06/10/2020
    Protected Sub ModificaDanno(ByVal Id As String, ByVal IdPosDanno As String, ByVal IdTipoDanno As String, ByVal EntitaDanno As String)
        Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
        Dbc.Open()

        Dim Cmd As New Data.SqlClient.SqlCommand("", Dbc)
        Dim Sql As String
        Dim Sql2 As String
        Dim SqlQuery As String

        Try
            Sql = "update  veicoli_danni set id_posizione_danno ='" & IdPosDanno & "', id_tipo_danno ='" & IdTipoDanno & "', entita_danno ='" & EntitaDanno & "' WHERE id = '" & Id & "'"

            'Response.Write(Sql & "<br>")
            'Response.End()

            Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
            Cmd.ExecuteNonQuery()

            'Sql2 = "SELECT @@IDENTITY FROM residenza_virtuale WITH(NOLOCK)"
            'Cmd = New Data.SqlClient.SqlCommand(Sql2, Dbc)
            'Session("residenza_virtuale") = Cmd.ExecuteScalar

            SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
            Cmd.CommandText = SqlQuery
            'Response.Write(Cmd.CommandText & "<br/>")
            'Response.End()
            Cmd.ExecuteNonQuery()

            Sql = "update  veicoli_gruppo_danni set id_posizione_danno ='" & IdPosDanno & "', id_tipo_danno ='" & IdTipoDanno & "', entita_danno ='" & EntitaDanno & "' WHERE id_danno = '" & Id & "'"

            'Response.Write(Sql & "<br>")
            'Response.End()

            Cmd = New Data.SqlClient.SqlCommand(Sql, Dbc)
            Cmd.ExecuteNonQuery()

            SqlQuery = "insert into query_log (data,utente,query) values('" & Now & "','" & Request.Cookies("SicilyRentCar")("nome") & "','" & Replace(Sql, "'", "''") & "')"
            Cmd.CommandText = SqlQuery
            'Response.Write(Cmd.CommandText & "<br/>")
            'Response.End()
            Cmd.ExecuteNonQuery()

        Catch ex As Exception
            HttpContext.Current.Response.Write("Modifica Danno Errore contattare amministratore del sistema. <br/>" & ex.Message & "<br/>")
        End Try

        Cmd.Dispose()
        Cmd = Nothing
        Dbc.Close()
        Dbc.Dispose()
        Dbc = Nothing
    End Sub

End Class