Imports System
Imports System.IO
Imports System.Web
Imports System.Web.UI


Partial Class GeneraContrattoNew
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' recuper il parametri di stampa dalla Session!
            Dim mie_dati As DatiStampaContrattoNew = Session("DatiStampaContratto")
            Session("DatiStampaContratto") = Nothing
            If mie_dati Is Nothing Then
                mie_dati = New DatiStampaContrattoNew
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
            End If

            Dim StampaDocumento As ClassGeneraHtmlToPdf = New ClassGeneraHtmlToPdf
            Dim ms As MemoryStream = StampaContrattoNew.GeneraDocumento(mie_dati)

            Trace.Write("ms: " & (ms Is Nothing))

            If Not ms Is Nothing Then
                Trace.Write("(1)")
                Response.Buffer = True
                Trace.Write("(2)")
                Response.Clear()
                Trace.Write("(3)")
                Response.ContentType = "application/pdf"
                Trace.Write("(4)")
                Response.AddHeader("content-disposition", "inline; filename=file.pdf")
                Trace.Write("(5)")

                ' forse se commento questa riga è meglio...
                Response.AddHeader("Content-Length", ms.GetBuffer().Length)
                Trace.Write("(6)")

                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length)
                Trace.Write("(7)")
                Response.OutputStream.Flush()
                Trace.Write("(8)")
                HttpContext.Current.ApplicationInstance.CompleteRequest()
                'Response.End()
                Trace.Write("(9)")
                Exit Sub
            End If
        Catch ex As Exception
            Trace.Write("Errore generazione pdf: " & ex.Message)
        End Try
    End Sub
End Class
