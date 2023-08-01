﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="stampaReportCommissioniOperatore.aspx.vb" Inherits="stampaReportCommissioniOperatore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    
    <% Dim apt As String = Mid(Request.QueryString("apt"), 4)
        Dim mese As String = Request.QueryString("mese")
        Dim m As String = Request.QueryString("m")
        Dim anno As String = Request.QueryString("y")
        Dim sta As String = Request.QueryString("sta")

        Dim nome_file As String = "report_commissioni_operatori_" & mese & "_" & anno
    %>

    <% Response.ContentType = "application/vnd.ms-excel"%>
    <% Response.AddHeader("Content-Disposition", "attachment;filename=" & nome_file & ".xls")%>

    <% Response.AddHeader("Content-Type", "text/html;charset=ISO-8859-1")%>

</head>
<body>
   
      <%

          Dim operatore As String = Request.QueryString("ope")
          Dim mese As String = Request.QueryString("mese")
          Dim m As String = Request.QueryString("m")
          Dim anno As String = Request.QueryString("y")
          Dim ddt As String = Request.QueryString("ddt")
          Dim adt As String = Request.QueryString("adt")
          Dim perc_com As String = Request.QueryString("pcom")
          Dim perc_imp As String = Request.QueryString("pimp")
          Dim tipo_cliente As String = Request.QueryString("tc")        'list tipo cliente
          Dim accessori As String = Request.QueryString("cka")          'list accessori

          Dim sqlStr As String = ""
          Dim colspan As String = ""

          Dim intestazione As String = "Report Riepilogo Commissioni Operatori " '"Sicily Rent Car Srl - Largo Lituania,11 - 90146 Palermo - P.IVA 02486830819"

          Dim condizione As String = Session("condizione")
          Session("condizione") = ""

          Dim contrec As Integer = 0
          Dim nco As String = "602200070"   'solo x test
          Dim perc_royalty As String = ""

          Dim da_data As String = funzioni_comuni.GetDataSql(ddt, 0)
          Dim a_data As String = funzioni_comuni.GetDataSql(adt, 59)

          Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
          Dbc.Open()
          Dim sqlA As String

          colspan = "3"     'colspan base (num e nominativo e totale)

          Dim errline As Integer = 0

          'Aggiunto 18.08.2022 salvo
          Dim aIdElemento() As String
          Dim aImponibileAccessori() As Double
          Dim aTot_ImponibileAccessori() As Double
          Dim aNomeAccessori() As String
          Dim contAccessori As Integer = 0


          'If accessori.IndexOf("98") > -1 Then
          '    colspan = 4       'se tempo+km
          'Else
          '    colspan = 2
          'End If



          'se accessori presenti modifica colspan
          If accessori <> "" Then
              Dim cka() As String = Split(accessori, ",")
              For xcka = 0 To UBound(cka)

                  colspan += 1

                  If cka(xcka) = "98" Then
                      colspan += 1
                  End If

              Next
          End If


          %>


      <table border="1" cellpadding="1" cellspacing="1">
         
          <%--<tr style="background-color:#d9d9d9;">
           <td align="center" style="font-size:14px;font-weight:bold;" colspan="<%=colspan %>">
              <b><%=intestazione %> </b>
           </td>
         </tr>
          --%>


          <tr style="background-color:#d9d9d9;">
           <td align="center" style="" colspan="<%=colspan %>">
              <b>Riepilogo Commissioni Operatori <%= ddt & " - " & adt %> </b>
           </td>
         </tr>
       </table>

        <table border="1" cellpadding="1" cellspacing="1">            

           <tr style="font-weight:bold;font-size:14px;background-color:#d9d9d9;">
            <td align="center" >
              Num.
            </td>
            <td align="left"  >
              Operatore
            </td>
            <td align="left" style="width:100px;" >
              TOTALE
            </td>
              <%-- <% If instr(1, accessori, "98", 1) > 0 Then %>
        
                    <td align="center" >
                      Importo Tempo+KM 
                    </td>
                    <td align="center" >
                      Giorni Tempo+KM 
                    </td>            

                <% end if %>--%>

           <%--se presenti accessori crea colonne--%>
            <% If accessori <> "" Then
                    Dim cka() As String = Split(accessori, ",")
                    For xcka = 0 To UBound(cka)%>
                        <td align = "center" style="width:100px;" >
                            <% =funzioni_comuni_new.GetNomeElemento(cka(xcka), True)%> 
                        </td>            
                        <% if cka(xcka) = "98" Then %>

                         <td align = "center" style="width:100px;" >
                            <% ="Giorni Tempo+KM"%>  
                        </td>   

                        <% End if %>


               <%
                   Next
                    End if %>
           <%-- end se presenti accessori crea colonne--%>

          </tr>


        <%


            Dim nominativo As String = ""
            Dim id_documento As String = ""
            Dim num_calcolo As String = ""
            Dim id_operatore As String = ""


            'sqlA = "Select contratti.id_operatore_creazione, operatori.cognome, operatori.nome "
            'sqlA += "From contratti INNER Join operatori On contratti.id_operatore_creazione = operatori.id "
            'sqlA += "Where (contratti.attivo = 1) And (contratti.status = 4 Or contratti.status = 6 Or contratti.status = 8) "
            'sqlA += "And (contratti.data_uscita BETWEEN Convert(DateTime, '" & da_data & "', 102) And CONVERT(DATETIME, '" & a_data & "', 102))  "

            sqlA = "SELECT id, cognome, nome FROM operatori WITH(NOLOCK) WHERE nome<>'xxx' " ' and no_commissioni=0"


            'se selezionato l'operatore

            If operatore <> "0" Then
                sqlA += "And [id] = '" & operatore & "' "
            End If

            'se selezionato tipo_cliente (multiplo)  id_fonte per la crezione lista no
            'If tipo_cliente <> "" Then
            '    Dim ttcc() As String = Split(tipo_cliente, ",")
            '    For xtc = 0 To UBound(ttcc)
            '        sqlA += "And id_fonte=" & ttcc(xtc) & " "
            '    Next
            'End If

            sqlA += "ORDER BY operatori.cognome, operatori.nome"

            Try



                Dim CmdA As New Data.SqlClient.SqlCommand(sqlA, Dbc)
                Dim oRSA As Data.SqlClient.SqlDataReader
                oRSA = CmdA.ExecuteReader()

                errline = 1

                Dim listOperatori() As String
                Dim idlistOperatori() As String
                Dim contOperatori As Integer = -1

                If oRSA.HasRows = True Then

                    contOperatori += 1      'elenco operatori trovato

                    errline = 2
                    While oRSA.Read()  'per ogni contratto di quell'operatore dell'elenco

                        If contOperatori = 0 Then
                            ReDim listOperatori(0)
                            ReDim idlistOperatori(0)
                        Else
                            ReDim Preserve listOperatori(contOperatori)
                            ReDim Preserve idlistOperatori(contOperatori)
                        End If
                        listOperatori(contOperatori) = oRSA!cognome & " " & oRSA!nome
                        idlistOperatori(contOperatori) = oRSA!id

                        contOperatori += 1

                    End While
                End If
                errline = 3
                oRSA.Close()
                oRSA = Nothing
                CmdA.Dispose()
                CmdA = Nothing

                errline = 4


                'crea lista contratti del periodo selezionato
                sqlStr = "Select ID, num_contratto, data_uscita, data_rientro, contratti.giorni, num_calcolo, 0 "
                sqlStr += "As imponibile_commissione, 0 As imponibile_totale FROM contratti WHERE (attivo=1) And (status=4 Or status=6 Or status=8) And (contratti.data_rientro "
                sqlStr += "BETWEEN Convert(DateTime, '" & da_data & "', 102) And CONVERT(DATETIME, '" & a_data & "', 102)) "
                sqlStr += "ORDER BY data_rientro,num_contratto"


                'se selezionato tipo_cliente (multiplo)  id_fonte
                If tipo_cliente <> "" Then
                    Dim ttcc() As String = Split(tipo_cliente, ",")
                    For xtc = 0 To UBound(ttcc)
                        sqlStr += "And id_fonte=" & ttcc(xtc) & " "
                    Next
                End If

                Dim aNco() As String
                Dim aIDco() As String
                Dim aNum_Calcolo() As String
                Dim aNum_Giorni() As String
                Dim flag_contratti As Boolean = False
                Dim contContratti As Integer = -1

                Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
                Dim oRS As Data.SqlClient.SqlDataReader

                oRS = Cmd.ExecuteReader()
                If oRS.HasRows = True Then
                    flag_contratti = True
                    While oRS.Read()  'per ogni contratto dell'elenco

                        contContratti += 1      'elenco trovato

                        If contContratti = 0 Then
                            ReDim aNco(0)
                            ReDim aIDco(0)
                            ReDim aNum_Calcolo(0)
                            ReDim aNum_Giorni(0)
                        Else
                            ReDim Preserve aNco(contContratti)
                            ReDim Preserve aIDco(contContratti)
                            ReDim Preserve aNum_Calcolo(contContratti)
                            ReDim Preserve aNum_Giorni(contContratti)
                        End If

                        aNco(contContratti) = oRS!num_contratto
                        aIDco(contContratti) = oRS!id
                        aNum_Calcolo(contContratti) = oRS!num_calcolo
                        aNum_Giorni(contContratti) = oRS!giorni

                        contContratti += 1

                    End While
                End If
                oRS.Close()
                Cmd.Dispose()
                oRS = Nothing
                Cmd = Nothing
                'end lista contratti di quel periodo

                ' Exit Sub 'test



                If flag_contratti = True Then

                    'ciclo per ogni Operatore
                    If contOperatori > -1 Then

                        For xop = 0 To UBound(listOperatori)

                            contOperatori = xop

                            contAccessori = 0   'reset

                            ' x ogni operatore crea array di totale x ogni singolo accessorio se selezionato
                            'riepilogo array lista accessori selezionati in fase di ricerca
                            If accessori <> "" Then
                                Dim cka() As String = Split(accessori, ",")
                                For xcka = 0 To UBound(cka)
                                    If xcka = 0 Then
                                        ReDim aTot_ImponibileAccessori(0)
                                    Else
                                        ReDim Preserve aTot_ImponibileAccessori(contAccessori)
                                    End If
                                    aTot_ImponibileAccessori(contAccessori) = 0     'imposta a zero il valore del totale su singolo array
                                    contAccessori += 1
                                Next

                            End If
                            'END  x ogni operatore crea array di totale x ogni singolo accessorio se selezionato

                            'ciclo x ogni operatore
                            id_operatore = idlistOperatori(xop)
                            nominativo = listOperatori(xop)

                            errline = 6

                            Dim imponibile_tmpKM As Double = 0
                            Dim tot_imponibile_tmpKM As Double = 0

                            Dim gg_tmpKM As Integer = 0
                            Dim tot_gg_tmpKM As Integer = 0

                            Dim flag_importo_accessori As Boolean = False
                            Dim flag_importo_commissioni As Boolean = False

                            'cicla x ogni contratto
                            For xco = 0 To UBound(aNco)

                                'reset flag
                                flag_importo_accessori = False
                                flag_importo_commissioni = False

                                contrec += 1

                                nco = aNco(xco)
                                id_documento = aIDco(xco)
                                num_calcolo = aNum_Calcolo(xco)

                                'Verifica se c'è commissione su quel contratto 
                                Dim sqlStr1 As String = ""
                                sqlStr1 = "SELECT TOP 1 commissioni_operatore.id FROM commissioni_operatore WITH(NOLOCK) INNER JOIN condizioni_elementi WITH(NOLOCK) "
                                sqlStr1 += "On commissioni_operatore.id_condizioni_elementi=condizioni_elementi.id "
                                sqlStr1 += "WHERE id_operatore='" & id_operatore & "' AND num_contratto='" & nco & "' AND condizioni_elementi.commissione_operatore=1"

                                Dim Dbc1 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                                Dbc1.Open()
                                Dim Cmd1 As New Data.SqlClient.SqlCommand(sqlStr1, Dbc1)
                                Dim Rs1 As Data.SqlClient.SqlDataReader
                                errline = 8
                                Rs1 = Cmd1.ExecuteReader()
                                errline = 9
                                Dim commissioni As Boolean = Rs1.HasRows
                                Rs1.Close()
                                Cmd1.Dispose()
                                Dbc1.Close()
                                Rs1 = Nothing
                                Cmd1 = Nothing
                                Dbc1 = Nothing

                                'se c'è procede x recuperare imponibile
                                If commissioni = True Then

                                    'reset variabili per questo contratto
                                    imponibile_tmpKM = 0
                                    gg_tmpKM = 0

                                    'verifica se deve calcolare tmp+km e giorni
                                    Dim flag_calcola_tmpKm As Boolean = False
                                    If nco = "112207408" Then 'senza tmp+km
                                        System.Threading.Thread.Sleep(10)
                                    End If

                                    If nco = "112207427" Then 'tempo+km 
                                        System.Threading.Thread.Sleep(10)
                                    End If

                                    'recupera valore imponibile totale
                                    Dim sqlStr6 As String = "SELECT contratti_costi.imponibile_scontato FROM contratti_costi WITH(NOLOCK) WHERE contratti_costi.id_documento=" & id_documento & " "
                                    sqlStr6 += "And contratti_costi.num_calcolo=" & num_calcolo & " And contratti_costi.nome_costo='TOTALE'"

                                    Dim Dbc6 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                                    Dbc6.Open()
                                    Dim Cmd6 = New Data.SqlClient.SqlCommand(sqlStr6, Dbc6)
                                    Dim imponibile_totale As Double = Cmd6.ExecuteScalar() & ""
                                    Cmd6.Dispose()
                                    Cmd6 = Nothing
                                    Dbc6.Close()

                                    'recupera altri valori imponibile
                                    Dim sqlStr2 As String = ""
                                    Dim sqlStr7 As String = ""
                                    Dim flag98 As Boolean = False

                                    'sqlStr7 = "SELECT SUM(imponibile_scontato)  as imponibile_commissione, id_elemento FROM contratti_costi WITH(NOLOCK) INNER JOIN condizioni_elementi ON contratti_costi.id_elemento=condizioni_elementi.id "
                                    'sqlStr7 += "INNER JOIN commissioni_operatore WITH(NOLOCK) ON contratti_costi.id_elemento=commissioni_operatore.id_condizioni_elementi "
                                    'sqlStr7 += "WHERE contratti_costi.id_documento=" & id_documento & " AND contratti_costi.num_calcolo=" & num_calcolo & " "
                                    'sqlStr7 += "AND condizioni_elementi.commissione_operatore=1 AND selezionato=1 AND ISNULL(omaggiato,0)=0 "
                                    'sqlStr7 += "AND commissioni_operatore.id_operatore=" & id_operatore & " "
                                    'sqlStr7 += "AND commissioni_operatore.num_contratto=" & nco
                                    'sqlStr7 = "GROUP BY contratti_costi.id_elemento, contratti.giorni"

                                    sqlStr7 = "Select SUM(contratti_costi.imponibile_scontato) As imponibile_commissione, contratti_costi.id_elemento as id_ele, contratti.giorni as n_giorni "
                                    sqlStr7 += "From contratti_costi WITH (NOLOCK) INNER Join condizioni_elementi On contratti_costi.id_elemento = condizioni_elementi.id INNER Join "
                                    sqlStr7 += "commissioni_operatore WITH (NOLOCK) ON contratti_costi.id_elemento = commissioni_operatore.id_condizioni_elementi INNER Join contratti On contratti_costi.id_documento = contratti.id "
                                    sqlStr7 += "Where (contratti_costi.id_documento = " & id_documento & ") And (contratti_costi.num_calcolo = " & num_calcolo & ") "
                                    sqlStr7 += "And (condizioni_elementi.commissione_operatore = 1) And (contratti_costi.selezionato = 1) "
                                    sqlStr7 += "And (ISNULL(contratti_costi.omaggiato, 0) = 0) And (commissioni_operatore.id_operatore = " & id_operatore & ") "
                                    sqlStr7 += "And (commissioni_operatore.num_contratto = " & nco & ") "
                                    sqlStr7 += "And contratti_costi.id_elemento='98' "
                                    sqlStr7 += "Group By contratti_costi.id_elemento, contratti.giorni"

                                    'imponibile commissisone per tempo+km
                                    Dim Dbc7 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
                                    Dbc7.Open()
                                    Dim Cmd7 = New Data.SqlClient.SqlCommand(sqlStr7, Dbc7)
                                    'Dim imponibile_commissione = Cmd7.ExecuteScalar & ""
                                    Dim Rs7 As Data.SqlClient.SqlDataReader
                                    Rs7 = Cmd7.ExecuteReader()
                                    Dim imponibile_commissione As Double = 0
                                    Dim xcka As Integer = 0

                                    If Rs7.HasRows Then
                                        Rs7.Read()
                                        imponibile_commissione = Rs7!imponibile_commissione
                                        imponibile_tmpKM = imponibile_commissione
                                        gg_tmpKM = Rs7!n_giorni
                                    Else
                                        imponibile_commissione = 0
                                        imponibile_tmpKM = 0
                                        gg_tmpKM = 0
                                    End If

                                    tot_gg_tmpKM += gg_tmpKM
                                    tot_imponibile_tmpKM += imponibile_tmpKM

                                    Rs7.Close()

                                    Cmd7.Dispose()
                                    Cmd7 = Nothing
                                    Dbc7.Close()


                                    'calcoli x commissione
                                    'lblCommissioni.Text = CDbl(lblCommissioni.Text) + ((CDbl(imponibile_commissione.Text) * txtPercentualeImponibile.Text) / 100) * txtPercentualeCommissione.Text / 100
                                    'imponibile_parziale.Text = FormatNumber(CDbl(imponibile_commissione.Text), 2)
                                    'imponibile_netto.Text = FormatNumber(CDbl(imponibile_commissione.Text * txtPercentualeImponibile.Text) / 100, 2)
                                    ' imponibile_commissione.Text = FormatNumber((((CDbl(imponibile_commissione.Text) * txtPercentualeImponibile.Text) / 100) * txtPercentualeCommissione.Text / 100), 2)

                                    'deve recuperare gli importi per i singoli accessori selezionati di quel contratto 18.08.2022 
                                    'ma per calcolarli il contratto deve avere un numero di giorni valido
                                    If accessori <> "" Then

                                        flag_importo_accessori = False

                                        contAccessori = 0 'reset

                                        Dim cka() As String = Split(accessori, ",")
                                        For xcka = 0 To UBound(cka)
                                            'crea array per ogni singolo accessorio di quel contratto

                                            If xcka = 0 Then
                                                ReDim aIdElemento(0)
                                                ReDim aImponibileAccessori(0)
                                            Else
                                                ReDim Preserve aIdElemento(contAccessori)
                                                ReDim Preserve aImponibileAccessori(contAccessori)
                                            End If


                                            If cka(xcka) = "98" Then
                                                aImponibileAccessori(contAccessori) = imponibile_tmpKM 'funzioni_comuni_new.GetCostoAccessorio(id_documento, num_calcolo, cka(xcka), id_operatore, nco)
                                                imponibile_commissione = aImponibileAccessori(contAccessori)
                                                'imponibile_tmpKM = FormatNumber(CDbl(imponibile_commissione), 2)
                                                'gg_tmpKM = Rs7!n_giorni
                                            Else
                                                aImponibileAccessori(contAccessori) = funzioni_comuni_new.GetCostoAccessorio(id_documento, num_calcolo, cka(xcka), id_operatore, nco)
                                                contAccessori = contAccessori
                                                aTot_ImponibileAccessori(contAccessori) += aImponibileAccessori(contAccessori)
                                            End If

                                            flag_importo_accessori = True

                                            contAccessori += 1
                                        Next

                                        'riempie i totali

                                    End If

                                End If

                                'solo x test elenco
                                Dim vedi_righe_singole As Boolean = False

                                If vedi_righe_singole = True Then

                                    If flag_importo_commissioni = True Or flag_importo_accessori = True Then

                                    %>

                                 <tr style = "font-size:12px;background-color:#fff;font-weight:bold;" >

                                     <td align = "center" >                                   
                                         <%= nco%>
                                     </td>                     
                     
                                     <td align="left" >
                         
                                     </td>
                  
                                      <td align="right" >
                                            <%="&euro; " & FormatNumber(imponibile_tmpKM, 2)%>
                                      </td>
                                      <td align="right"  >
                                            <%= FormatNumber(gg_tmpKM, 0) & ""%> 
                                      </td>
                  
                                         <%-- se presenti accessori crea colonne per i totali --%>
                                        <% 'ma per calcolarli il contratto deve avere un numero di giorni valido

                                            If accessori <> "" Then
                                                Dim cka() As String = Split(accessori, ",")
                                                For xcka = 0 To UBound(cka)%>
                                                    <td align = "right" >
                                                        <% ="&euro; " & FormatNumber(aImponibileAccessori(xcka), 2)%> 
                                                    </td>            
                                       <% Next
                                           End If %>
                         
                                     </tr>
                               <%

                                           End If 'se test visualizza le righe del singolo contratto

                                       End If 'vedi righe singole



                                   Next xco 'x ogni contratto per quell'operatore


        %>

        

       <%--     'Righe Totali per singolo operatore --%>

      <% If contContratti > -1 And contOperatori > -1 Then ' And tot_imponibile_tmpKM > 0 %> 

                 <tr style="font-size:12px;background-color:#fff;font-weight:bold;" >

                 <td align="center" >
                        <%=contOperatori + 1%>
                 </td>                     
                     
                 <td align="left" >
                         <%=nominativo %>
                  </td>
                  
                    <%-- <% If  instr(1, accessori, "98", 1) > 0 Then %>

                      <td align="right" >
                            <%="&euro; " & FormatNumber(tot_imponibile_tmpKM, 2)%>
                      </td>
                      <td align="right"  >
                            <%= FormatNumber(tot_gg_tmpKM, 0) & ""%> 
                      </td>
                  <% End If %>--%>


                     <%--se presenti accessori crea colonne per i totali --%>
                    <% If accessori <> "" Then

                            Dim cka() As String = Split(accessori, ",")

                            'colonna totali calcolo
                            Dim totale_tiga As Double = 0
                            For xcka = 0 To UBound(cka)
                                If cka(xcka) = "98" Then
                                    totale_tiga += tot_imponibile_tmpKM
                                Else
                                    totale_tiga += aTot_ImponibileAccessori(xcka)
                                End If
                            Next

                    %>

                             <td align = "right" >
                                    <%= "&euro; " & FormatNumber(totale_tiga, 2) %>
                            </td>

                    <%

                        For xcka = 0 To UBound(cka)%>                

                              <% If cka(xcka) = "98" Then %>

                                    <td align="right" >
                                        <%="&euro; " & FormatNumber(tot_imponibile_tmpKM, 2)%>
                                    </td>
                     
                                    <td align = "center" style="width:100px;" >
                                        <%= FormatNumber(tot_gg_tmpKM, 0) & ""%> 
                                    </td>   

                             <% Else %>
                     
                                    <td align = "right" >
                                        <% ="&euro; " & FormatNumber(aTot_ImponibileAccessori(xcka), 2)%> 
                                    </td>     
                     
                             <% End If %>

                       <%
                               Next

                           End If %>

                   <%-- end se presenti accessori crea colonne--%>

                 </tr>

                <% 'reset array Totali %>

           <% End If 'se contrec righe totali 

                           Next xop 'ciclo x ogni operatore

                           'END Righe Totali generali

                       End If 'se operatori presenti

                   End If 'se contratti presenti per il periodo selezionato

               Catch ex As Exception
                   Response.Write("errore while :" & ex.Message & " linerr: " & errline & "<br/>")
               End Try



   %> 
 

  </table>
 
<%
    Dbc.Close()
    Dbc.Dispose()
    Dbc = Nothing

%>
    
</body>
</html>

