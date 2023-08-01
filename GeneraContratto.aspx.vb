Imports System
Imports System.IO
Imports System.Web
Imports System.Web.UI


Partial Class GeneraContratto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim err_n As Integer = 0

        Dim tipostampa As String = Request.QueryString("tipo") '10.05.2022


        Try
            ' recuper il parametri di stampa dalla Session!
            Dim mie_dati As DatiStampaContratto = Session("DatiStampaContratto")
            Session("DatiStampaContratto") = Nothing


            'TEST
            ' With mie_dati
            ' Response.Write("conducente1=" & .conducente1.ToString & "<br/>")
            ' End With
            '
            'TEST

            err_n = 1


            If mie_dati Is Nothing Then
                err_n = 2
                mie_dati = New DatiStampaContratto
                err_n = 3
                With mie_dati
                    .conducente1 = "Pippo" & vbCrLf & "Via Libertà 5555" & vbCrLf & "Palermo (PA)"
                    .conducente2 = "Pluto" & vbCrLf & "Via del Popolo 5444" & vbCrLf & "Palermo (PA)"
                    .costo = "100.00" & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf
                    .extra_e_penali = "50.00" & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf & vbCrLf
                    .fatturare_a = "ACME S.p.A."
                    .note = "Mie Note"
                    .qta = "1"
                    .ran = "2"
                    .resn = "3"
                    .rientro_previsto = "20/07/2012"
                    .totale = "150.00"
                    .uscita = Format(Now, "dd/MM/yyyy hh:mm:ss")
                    .vari = "Booo"
                    .veicolo = "Ferrari"
                    .voucher = "voucher"
                    .voucher_gg = "10"
                    .voucher_n = "1"
                End With
                err_n = 4
            Else
                err_n = 5
            End If

            Dim nazione As String = ""
            Dim id_nazione As String = ""
            If Request.QueryString("lang") <> "" Then
                nazione = Request.QueryString("lang")
                id_nazione = Request.QueryString("idlang")
            End If



            err_n = 11
            Dim StampaDocumento As ClassGeneraHtmlToPdf = New ClassGeneraHtmlToPdf
            err_n = 12


            Dim ms As MemoryStream = StampaContratto.GeneraDocumento(mie_dati, nazione, id_nazione, tipostampa)
            err_n = 13
            Trace.Write("ms: " & (ms Is Nothing))
            err_n = 14
            ' Response.Write("errN=" & err_n.ToString & "<br/>")
            ' Response.End()

            If Not ms Is Nothing Then
                Trace.Write("(1)")
                Response.Buffer = True
                Trace.Write("(2)")
                err_n = 2
                Response.Clear()
                Trace.Write("(3)")
                Response.ContentType = "application/pdf"
                Trace.Write("(4)")
                Response.AddHeader("content-disposition", "inline; filename=file.pdf")
                Trace.Write("(5)")
                err_n = 5
                ' forse se commento questa riga è meglio...
                Response.AddHeader("Content-Length", ms.GetBuffer().Length)
                Trace.Write("(6)")

                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
                Trace.Write("(7)")
                Response.OutputStream.Flush()
                Trace.Write("(8)")
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                err_n = 9
                Response.Write("err_n:" & err_n & "<br/>")

                ''Aggiornamento 11.03.2022
                'se firmato crea il pdf
                Dim numcontratto As String = Session("DatiStampaContrattoNum")
                '## 'se firma inserita crea contratto in file pdf e lo allega
                'If funzioni_comuni.GetContrattoFirmato(numcontratto, "") = True Then
                '    Dim pathpdf As String = StampaContratto.GeneraDocumentoPDF(mie_dati, numcontratto, id_nazione)
                '    If pathpdf <> "" Then
                '        Dim allega As Boolean = funzioni_comuni.AllegaContrattoDopoFirma(numcontratto)
                '        If allega = True Then

                '            Session("allegafilepdf") = "OK"
                '        Else
                '            Session("allegafilepdf") = "KO"
                '        End If
                '    End If

                'End If



                Response.End()
                Trace.Write("(9)")


                Exit Sub
            End If












        Catch ex As Exception
            'Trace.Write("Errore generazione pdf: " & ex.Message)
            'HttpContext.Current.Response.Write("Errore generazione pdf:  : <br/>err_n:" & err_n & "<br/>" & ex.Message & "<br/>" & "<br/>")
        End Try
    End Sub
End Class
