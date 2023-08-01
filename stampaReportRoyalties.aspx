<%@ Page Language="VB" AutoEventWireup="false" CodeFile="stampaReportRoyalties.aspx.vb" Inherits="stampaReportContratti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    
    <% Dim apt As String = Mid(Request.QueryString("apt"), 4)
        Dim mese As String = Request.QueryString("mese")
        Dim m As String = Request.QueryString("m")
        Dim anno As String = Request.QueryString("y")
        Dim sta As String = Request.QueryString("sta")
        Dim nome_file As String = "report_royalties_" & apt.Replace(" ", "_") & "_" & mese & "_" & anno
    %>

    <% Response.ContentType = "application/vnd.ms-excel"%>
    <% Response.AddHeader("Content-Disposition", "attachment;filename=" & nome_file & ".xls")%>

    <% 'Response.AddHeader("Content-Disposition", "attachment;filename=report_royalties.xls")%>

</head>
<body>

    
      <%


          Dim apt As String = Mid(Request.QueryString("apt"), 4)
          Dim mese As String = Request.QueryString("mese")
          Dim m As String = Request.QueryString("m")
          Dim anno As String = Request.QueryString("y")
          Dim sta As String = Request.QueryString("sta")

          Dim sqlStr As String = ""
          Dim colspan As String = "11"

          Dim intestazione As String = "Sicily Rent Car Srl - Largo Lituania,11 - 90146 Palermo - P.IVA 02486830819"

          Dim condizione As String = Session("condizione")
          Session("condizione") = ""

          Dim contrec As Integer = 0
          Dim nco As String = "602200070"   'solo x test
          Dim perc_royalty As String = ""


          If sta = "14" Then   'aggiunto salvo 20.04.2023 x Milano Linate

              perc_royalty = "7"
              colspan = "13"


              'sqlStr = "Select contratti.num_contratto as N_Contratto,  contratti.data_rientro, contratti_costi.imponibile as fatturato_lordo, contratti.id as id_documento "
              'sqlStr += "From contratti INNER Join contratti_costi On contratti.id = contratti_costi.id_documento "
              'sqlStr += "Where (contratti.id_stazione_uscita = " & sta & ") And (contratti.attivo = 1) And (contratti.status = 4 Or "
              'sqlStr += "contratti.status = 6 Or contratti.status = 8) And (Month(contratti.data_rientro) = " & m & ") And (Year(contratti.data_rientro) = " & anno & ") "
              'sqlStr += "And contratti_costi.nome_costo='TOTALE' "
              'sqlStr += "ORDER BY contratti.num_contratto"


              sqlStr = "Select contratti.num_contratto As N_Contratto,  contratti.data_rientro, contratti_costi.imponibile As fatturato_netto, "
              sqlStr += "contratti_costi.iva_imponibile as iva, valore_costo As fatturato_lordo,"
              sqlStr += "contratti.id as id_documento From contratti INNER Join contratti_costi On contratti.id = contratti_costi.id_documento "
              sqlStr += "Where (contratti.id_stazione_uscita = " & sta & ") And (contratti.attivo = 1) And (contratti.status = 4 "
              sqlStr += "Or contratti.status = 6 Or contratti.status = 8) And (Month(contratti.data_rientro) = " & m & ") And (Year(contratti.data_rientro) = " & anno & ") "
              sqlStr += "And contratti_costi.nome_costo='TOTALE' ORDER BY contratti.num_contratto, contratti_costi.num_calcolo desc"



          ElseIf sta = "10" Or sta = "11" Then

              'sqlStr = "SELECT contratti.data_rientro, Fatture_nolo.num_contratto_rif AS N_Contratto, Fatture_nolo.totale_fattura AS Fatturato_Lordo, Fatture_nolo.iva AS IVA, max(Fatture_nolo.num_fattura) as n_fattura "
              'sqlStr += "From Fatture_nolo INNER JOIN contratti ON Fatture_nolo.num_contratto_rif = contratti.num_contratto INNER JOIN "
              'sqlStr += "contratti_costi ON contratti.id = contratti_costi.id_documento INNER JOIN condizioni_elementi ON contratti_costi.id_elemento = condizioni_elementi.id "
              'sqlStr += "WHERE (contratti.status = 6) AND (contratti_costi.selezionato = 1) AND (contratti_costi.id_elemento = 98) "
              'sqlStr += "And contratti.id_stazione_uscita = " & sta & " AND MONTH(contratti.data_rientro)=" & m & " and year(contratti.data_rientro)=" & anno & " "
              'sqlStr += "Group by Fatture_nolo.num_contratto_rif, contratti.data_rientro, Fatture_nolo.totale_fattura, Fatture_nolo.iva "
              'sqlStr += "order by contratti.data_rientro"

              'nuovo calcolo basato su tabella contratti 10.08.2022 salvo
              sqlStr = "Select contratti.num_contratto as N_Contratto,  contratti.data_rientro, contratti_costi.imponibile as fatturato_lordo, contratti.id as id_documento, contratti_costi.iva_imponibile as iva "
              sqlStr += "From contratti INNER Join contratti_costi On contratti.id = contratti_costi.id_documento "
              sqlStr += "Where (contratti.id_stazione_uscita = " & sta & ") And (contratti.attivo = 1) And (contratti.status = 4 Or "
              sqlStr += "contratti.status = 6 Or contratti.status = 8) And (Month(contratti.data_rientro) = " & m & ") And (Year(contratti.data_rientro) = " & anno & ") "
              sqlStr += "And contratti_costi.nome_costo='TOTALE' "
              sqlStr += "ORDER BY contratti.num_contratto, contratti_costi.num_calcolo desc"

              colspan = "12"

              If sta = "10" Then
                  perc_royalty = "6"
              Else
                  perc_royalty = "5"
              End If

          ElseIf sta = "6" Or sta = "5" Then    'Comiso o Trapani

              'calcolo basato su tabella fatture
              'sqlStr = "Select contratti.data_rientro, Fatture_nolo.num_contratto_rif As N_Contratto, sum((Fatture_nolo.totale_fattura - Fatture_nolo.iva)) As Imponibile, max(Fatture_nolo.num_fattura) as n_fattura "
              'sqlStr += "From Fatture_nolo INNER Join contratti On Fatture_nolo.num_contratto_rif = contratti.num_contratto "
              'sqlStr += "INNER Join contratti_costi On contratti.id = contratti_costi.id_documento INNER Join condizioni_elementi On contratti_costi.id_elemento = condizioni_elementi.id "
              'sqlStr += "Where(contratti.status = 6) And (contratti_costi.selezionato = 1) And (contratti_costi.id_elemento = 98) And contratti.id_stazione_uscita = " & sta & " "
              'sqlStr += "And MONTH(contratti.data_rientro)=" & m & " And year(contratti.data_rientro)=" & anno & " "
              'sqlStr += "Group by Fatture_nolo.num_contratto_rif , contratti.data_rientro "
              'sqlStr += "order by data_rientro"

              'nuovo calcolo basato su tabella contratti 10.08.2022 salvo
              sqlStr = "Select contratti.num_contratto as N_Contratto,  contratti.data_rientro, contratti_costi.imponibile as Imponibile, contratti.id as id_documento "
              sqlStr += "From contratti INNER Join contratti_costi On contratti.id = contratti_costi.id_documento "
              sqlStr += "Where (contratti.id_stazione_uscita = " & sta & ") And (contratti.attivo = 1) And (contratti.status = 4 Or "
              sqlStr += "contratti.status = 6 Or contratti.status = 8) And (Month(contratti.data_rientro) = " & m & ") And (Year(contratti.data_rientro) = " & anno & ") "
              sqlStr += "And contratti_costi.nome_costo='TOTALE' "
              sqlStr += "ORDER BY contratti.num_contratto, contratti_costi.num_calcolo desc"

              If sta = "6" Then 'Comiso
                  colspan = "10"
                  perc_royalty = "7"

              Else      'Trapani
                  colspan = "12"
                  perc_royalty = "5"
              End If


              'ElseIf sta = "5" Then    'Trapani

              'sqlStr = "Select contratti.data_rientro, Fatture_nolo.num_contratto_rif As N_Contratto, sum((Fatture_nolo.totale_fattura - Fatture_nolo.iva)) As Imponibile, max(Fatture_nolo.num_fattura) as n_fattura "
              'sqlStr += "From Fatture_nolo INNER Join contratti On Fatture_nolo.num_contratto_rif = contratti.num_contratto "
              'sqlStr += "INNER Join contratti_costi On contratti.id = contratti_costi.id_documento INNER Join condizioni_elementi On contratti_costi.id_elemento = condizioni_elementi.id "
              'sqlStr += "Where(contratti.status = 6) And (contratti_costi.selezionato = 1) And (contratti_costi.id_elemento = 98) And contratti.id_stazione_uscita = " & sta & " "
              'sqlStr += "And MONTH(contratti.data_rientro)=" & m & " And year(contratti.data_rientro)=" & anno & " "
              'sqlStr += "Group by Fatture_nolo.num_contratto_rif , contratti.data_rientro "
              'sqlStr += "order by data_rientro"

              'colspan = "11"
              'perc_royalty = "5"

          End If


          %>

      <table border="1" cellpadding="1" cellspacing="1">
         
          <tr style="background-color:#d9d9d9;">
           <td align="center" style="font-size:14px;font-weight:bold;" colspan="<%=colspan %>">
              <b><%=intestazione %> </b>
           </td>
         </tr>
          
          <tr style="background-color:#d9d9d9;">
           <td align="center" style="" colspan="<%=colspan %>">
              <b><%=apt & " - " & mese & " " & anno  %> </b>
           </td>
         </tr>
       </table>

        <table border="1" cellpadding="1" cellspacing="1">


            <% If sta = "10" Or sta = "11" Then 'Pisa e Firenze%>

           <tr style="font-weight:bold;font-size:12px;background-color:#d9d9d9;">
            <td align="center"  >
              Num.
            </td>
            <td align="center"  >
              Num. Contratto
            </td>
            <td align="center" >
              Data Rientro
            </td>
            <td align="center" >
              Fatturato Lordo
            </td>
            <td align="center">
              IVA
            </td>
            <td align="center" >
              Assicurazioni
            </td>
               <td align="center"  >
              Carburanti
            </td>
            <td align="center"  >
              Danni
            </td>
            <td align="center" >
              Commissione broker
            </td>
            <td align="center"  >
              Base di calcolo
            </td>            
            <td align="center"  >
              Royalty <% = perc_royalty %>%
            </td>            
                <td align="center"  >
              N. Fattura
            </td>

          </tr>
        

            <% elseIf sta = "6" Then 'Comiso%>

               <tr style="font-weight:bold;font-size:12px;background-color:#d9d9d9;">
            <td align="center"  >
              Num.
            </td>
            <td align="center"  >
              Num. Contratto
            </td>
            <td align="center" >
              Data Rientro
            </td>
            <td align="center" >
              Imponibile Contratto
            </td>            
            <td align="center"  >
              Carburanti
            </td>
            <td align="center"  >
              Danni
            </td>
            <td align="center" >
              Imponibile Netto
            </td>
            <td align="center"  >
              Base di calcolo 65%
            </td>            
            <td align="center"  >
              Royalty <% = perc_royalty %>%
            </td>    
                    <td align="center"  >
              N. Fattura
            </td>
          </tr>
        
            
            <% elseIf sta = "5" Then 'Trapani%>
                
           <tr style="font-weight:bold;font-size:12px;background-color:#d9d9d9;">
            <td align="center"  >
              Num.
            </td>
            <td align="center"  >
              Num. Contratto
            </td>
            <td align="center" >
              Data Rientro
            </td>
            <td align="center" >
              Imponibile Contratto
            </td>
            <td align="center">
              Carburanti
            </td>
            <td align="center" >
              Danni
            </td>
               <td align="center"  >
              Assicurazioni
            </td>
            <td align="center"  >
              Rientro Auto
            </td>
            <td align="center" >
              Oneri Aeroportuali
            </td>
            <td align="center"  >
              Base di calcolo
            </td>            
            <td align="center"  >
              Royalty <% = perc_royalty %>%
            </td>      
                <td align="center"  >
              N. Fattura
            </td>
          </tr>

        <% elseIf sta = "14" Then 'Milano Linate%>
            

            <tr style="font-weight:bold;font-size:12px;background-color:#d9d9d9;">
            <td align="center"  >
              Num.
            </td>
            <td align="center"  >
              Num. Contratto
            </td>
            <td align="center" >
              Data Rientro
            </td>
            <td align="center" >
              Fatturato Lordo
            </td>
            <td align="center" >
              IVA
            </td>
            <td align="center" >
              Fatturato Netto
            </td>
            <td align="center" >
              Noleggio
            </td>
            <td align="center"  >
              Assicurazioni
            </td>
            <td align="center">
              Carburanti
            </td>
            <td align="center" >
              Danni
            </td>
            <td align="center"  >
              Base di calcolo
            </td>            
            <td align="center"  >
              Royalty <% = perc_royalty %>%
            </td>            
            <td align="center"  >
              N. Fattura
            </td>

          </tr>


            <% End If %>





        <%
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc2.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim CmdA As New Data.SqlClient.SqlCommand("", Dbc)
            Dim CmdC As New Data.SqlClient.SqlCommand("", Dbc)
            Dim CmdD As New Data.SqlClient.SqlCommand("", Dbc)

            Dim oRS As Data.SqlClient.SqlDataReader
            oRS = Cmd.ExecuteReader()

            Dim nFattura As String = 0

            Dim fatturato_lordo As Double = 0
            Dim iva As Double = 0
            Dim base_calcolo As Double = 0
            Dim royalty As Double = 0
            Dim assicurazioni As Double = 0
            Dim carburanti As Double = 0
            Dim danni As Double = 0
            Dim imponibile_co As Double = 0
            Dim imponibile_netto As Double = 0
            Dim rientro_auto As Double = 0          'TP
            Dim oneri_apt As Double = 0           'TP

            Dim tot_fatturato_lordo As Double = 0
            Dim tot_iva As Double = 0
            Dim tot_assicurazioni As Double = 0
            Dim tot_carburanti As Double = 0
            Dim tot_danni As Double = 0
            Dim tot_base_calcolo As Double = 0
            Dim tot_royalty As Double = 0
            Dim tot_imponibile_co As Double = 0
            Dim tot_imponibile_netto As Double = 0
            Dim tot_rientro_auto As Double = 0
            Dim tot_oneri_apt As Double = 0

            'aggiunto x Milano Linate 20.04.2023
            Dim noleggio As Double = 0
            Dim tot_noleggio As Double = 0
            Dim fatturato_netto As Double = 0
            Dim tot_fatturato_netto As Double = 0


            Dim id_doc As String = ""

            Dim last_numco As String = ""
            Dim flagCalcola As Boolean = False

            While oRS.Read()


                nco = oRS!n_contratto

                'verifica
                If nco = "202302386" Then  'Or nco = "202302393" Or nco = "202302371"nco = "202302371" Or
                    nco = nco
                End If

                If last_numco = "" Then
                    flagCalcola = True
                    last_numco = nco
                Else
                    If last_numco = nco Then
                        flagCalcola = False
                    Else
                        last_numco = nco
                        flagCalcola = True
                    End If
                End If


                id_doc = oRS!id_documento

                'x routine modificata passa il numero di contratto salvo 11.10.2022
                id_doc = nco

                nFattura = 0 'oRS!n_fattura   'sempre zero nel caso di nuovo calcolo da tabella contratti

                If flagCalcola = True Then

                    contrec += 1

                    If sta = "14" Then 'Milano Linate

                        fatturato_lordo = oRS("fatturato_lordo")
                        iva = oRS("iva")

                        tot_iva += iva
                        tot_fatturato_lordo += fatturato_lordo

                        fatturato_netto = oRS!fatturato_netto
                        tot_fatturato_netto += fatturato_netto

                        carburanti = GetCostiAltri(id_doc, 1, nFattura)
                        tot_carburanti += carburanti

                        danni = GetCostiAltri(id_doc, 2, nFattura)
                        tot_danni += danni

                        assicurazioni = GetCostiAltri(id_doc, 3, nFattura)
                        tot_assicurazioni += assicurazioni

                        oneri_apt = 0
                        tot_oneri_apt += oneri_apt

                        noleggio = fatturato_netto - (carburanti + danni + assicurazioni)
                        tot_noleggio += noleggio


                        base_calcolo = fatturato_netto - (carburanti + danni) 'escluso assicurazione
                        tot_base_calcolo += base_calcolo

                        royalty = (base_calcolo * CInt(perc_royalty)) / 100
                        tot_royalty += royalty

                        nFattura = Session("numFatturaContratto")


                    ElseIf sta = "10" Or sta = "11" Then

                        'PISA e Firenze

                        fatturato_lordo = oRS("fatturato_lordo") + oRS("iva")
                        iva = oRS("iva")

                        tot_iva += iva
                        tot_fatturato_lordo += fatturato_lordo

                        'ricava costo 

                        assicurazioni = GetCostiAltri(id_doc, 3, nFattura)
                        tot_assicurazioni += assicurazioni

                        If nco = "602200341" Then
                            nco = nco
                        End If
                        carburanti = GetCostiAltri(id_doc, 1, nFattura)
                        tot_carburanti += carburanti

                        danni = GetCostiAltri(id_doc, 2, nFattura)
                        tot_danni += danni

                        base_calcolo = fatturato_lordo - oRS!iva - assicurazioni - carburanti - danni
                        tot_base_calcolo += base_calcolo

                        If sta = "10" Then
                            royalty = (base_calcolo * CInt(perc_royalty)) / 100      'Pisa
                        Else
                            royalty = (base_calcolo * CInt(perc_royalty)) / 100      'Firenze
                        End If
                        tot_royalty += royalty


                        nFattura = Session("numFatturaContratto")

                        'end Pisa e Firenze

                    ElseIf sta = "6" Then 'Comiso

                        imponibile_co = oRS!imponibile
                        tot_imponibile_co += imponibile_co

                        carburanti = GetCostiAltri(id_doc, 1, nFattura)
                        tot_carburanti += carburanti

                        danni = GetCostiAltri(id_doc, 2, nFattura)
                        tot_danni += danni

                        imponibile_netto = imponibile_co - carburanti - danni
                        tot_imponibile_netto += imponibile_netto

                        base_calcolo = imponibile_netto * 0.65
                        tot_base_calcolo += base_calcolo

                        royalty = (base_calcolo * CInt(perc_royalty)) / 100
                        tot_royalty += royalty
                        nFattura = Session("numFatturaContratto")

                    ElseIf sta = "5" Then 'Trapani

                        imponibile_co = oRS!imponibile
                        tot_imponibile_co += imponibile_co

                        carburanti = GetCostiAltri(id_doc, 1, nFattura)
                        tot_carburanti += carburanti

                        danni = GetCostiAltri(id_doc, 2, nFattura)
                        tot_danni += danni

                        assicurazioni = GetCostiAltri(id_doc, 3, nFattura)
                        tot_assicurazioni += assicurazioni

                        rientro_auto = GetCostiAltri(id_doc, 4, nFattura)
                        tot_rientro_auto += rientro_auto

                        oneri_apt = 0
                        tot_oneri_apt += oneri_apt

                        base_calcolo = imponibile_co - carburanti - danni - assicurazioni - rientro_auto
                        tot_base_calcolo += base_calcolo

                        royalty = (base_calcolo * CInt(perc_royalty)) / 100
                        tot_royalty += royalty

                        nFattura = Session("numFatturaContratto")



                    End If


                %>


             <% If sta = "14" Then 'Milano Linate%>

                    <tr>
                 <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=contrec%>
                 </td>                     
                     
                     <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=nco%>
                  </td>
                  <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=oRS("data_rientro")%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(fatturato_lordo, 2)%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(iva, 2)%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(fatturato_netto, 2)%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(noleggio, 2)%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >    
                         <%="&euro; " & FormatNumber(assicurazioni, 2) & ""%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(carburanti, 2) & ""%> 
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(danni, 2) & ""%>
                  </td>             
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(base_calcolo, 2) %>  <%--base calcolo--%>
                  </td>
             
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(royalty, 2) %>  <%--Royalty--%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="" & nFattura %>  <%--num fattura--%>
                  </td>



                 </tr>


             <% ElseIf sta = "10" Or sta = "11" Then 'Pisa e Firenze%>

                 <tr>
                 <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=contrec%>
                 </td>                     
                     
                     <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=nco%>
                  </td>
                  <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=oRS("data_rientro")%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(fatturato_lordo, 2)%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(iva, 2) & ""%> 
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(assicurazioni, 2) & ""%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >    
                         <%="&euro; " & FormatNumber(carburanti, 2) & ""%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(danni, 2) & ""%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; 0,00" %>
                  </td>
             
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(base_calcolo, 2) %>  <%--base calcolo--%>
                  </td>
             
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(royalty, 2) %>  <%--Royalty--%>
                  </td>
                        <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="" & nFattura %>  <%--num fattura--%>
                  </td>
                 </tr>

             <% elseIf sta = "6" Then 'Comiso%>
                
              <tr>
                 <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=contrec%>
                 </td>                     
                     
                     <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=nco%>
                  </td>
                  <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=oRS("data_rientro")%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(imponibile_co, 2)%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(carburanti, 2) & ""%> 
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(danni, 2) & ""%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >    
                         <%="&euro; " & FormatNumber(imponibile_netto, 2) & ""%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(base_calcolo, 2) & ""%>
                  </td>                  
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(royalty, 2) %>  <%--Royalty--%>
                  </td>
                     <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="" & nFattura %>  <%--num fattura--%>
                  </td>
                 </tr>

             <% elseIf sta = "5" Then 'Trapani%>
                

              <tr>
                 <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=contrec%>
                 </td>                     
                     
                     <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=nco%>
                  </td>
                  <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=oRS("data_rientro")%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(imponibile_co, 2)%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(carburanti, 2) & ""%> 
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(danni, 2) & ""%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >    
                         <%="&euro; " & FormatNumber(assicurazioni, 2) & ""%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(rientro_auto, 2) & ""%>
                  </td>
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(oneri_apt, 2) & ""%>
                  </td>
             
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(base_calcolo, 2) %>  <%--base calcolo--%>
                  </td>
             
                  <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="&euro; " & FormatNumber(royalty, 2) %>  <%--Royalty--%>
                  </td>
                     <td align="right" bgcolor="white" style="font-size:12px" >
                        <%="" & nFattura %>  <%--num fattura--%>
                  </td>
                 </tr>
                        
            <% End If %>

               <%


                       End If 'flagCalcola

                   End While

                   Dbc.Close()
                   Dbc.Dispose()
                   Dbc = Nothing

                   Dbc2.Close()
                   Dbc2.Dispose()
                   Dbc2 = Nothing


        %>

        <% 

            'Righe Totali


            if sta = "14" Then 'Milano Linate%>

                <tr style="font-size:12px;background-color:#ffd800;font-weight:bold;" >

                 <td align="center" >
                       <%-- <%=contrec%>--%>
                 </td>                     
                     
                 <td align="center" >
                        
                  </td>
                  <td align="center" >
                        
                  </td>
                  <td align="right" >
                        <%="&euro; " & FormatNumber(tot_fatturato_lordo, 2)%>
                  </td>
                    <td align="right" >
                        <%="&euro; " & FormatNumber(tot_iva, 2)%>
                  </td>
                  <td align="right" >
                        <%="&euro; " & FormatNumber(tot_fatturato_netto, 2)%>
                  </td>
                  <td align="right" >
                        <%="&euro; " & FormatNumber(tot_noleggio, 2)%>
                  </td>
                  <td align="right" >    
                         <%="&euro; " & FormatNumber(tot_assicurazioni, 2) & ""%>
                  </td>

                  <td align="right"  >
                        <%="&euro; " & FormatNumber(tot_carburanti, 2) & ""%> 
                  </td>
                  <td align="right"  >
                        <%="&euro; " & FormatNumber(tot_danni, 2) & ""%>
                  </td>
          
                  <td align="right"  >
                        <%="&euro; " & FormatNumber(tot_base_calcolo, 2) %>  <%--base calcolo--%>
                  </td>
             
                  <td align="right"  >
                        <%="&euro; " & FormatNumber(tot_royalty, 2) %>  <%--Royalty--%>
                  </td>
                  <td align="right"  >
                       
                  </td>


                 </tr>
            

            <% elseIf sta = "10" Or sta = "11" Then 'Pisa e Firenze%>

                 <tr style="font-size:12px;background-color:#ffd800;font-weight:bold;" >

                 <td align="center" >
                        <%--<%="" %>--%>
                 </td>                     
                     
                 <td align="center" >
                        
                  </td>
                  <td align="center" >
                        
                  </td>
                  <td align="right" >
                        <%="&euro; " & FormatNumber(tot_fatturato_lordo, 2)%>
                  </td>
                  <td align="right"  >
                        <%="&euro; " & FormatNumber(tot_iva, 2) & ""%> 
                  </td>
                  <td align="right"  >
                        <%="&euro; " & FormatNumber(tot_assicurazioni, 2) & ""%>
                  </td>
                  <td align="right" >    
                         <%="&euro; " & FormatNumber(tot_carburanti, 2) & ""%>
                  </td>
                  <td align="right" >
                        <%="&euro; " & FormatNumber(tot_danni, 2) & ""%>
                  </td>
                  <td align="right" >
                        <%="&euro; 0,00" %>
                  </td>
             
                  <td align="right"  >
                        <%="&euro; " & FormatNumber(tot_base_calcolo, 2) %>  <%--base calcolo--%>
                  </td>
             
                  <td align="right"  >
                        <%="&euro; " & FormatNumber(tot_royalty, 2) %>  <%--Royalty--%>
                  </td>
                  
                     <td align="right"  >                       
                  </td>

                 </tr>

             <% elseIf sta = "6" Then 'Comiso%>
                
              <tr style="font-size:12px;background-color:#ffd800;font-weight:bold;" >

                 <td align="center" >
                       <%-- <%=contrec%>--%>
                 </td>                     
                     
                   <td align="center" >
                        
                  </td>
                  <td align="center" >
                        
                  </td>
                  <td align="right" >
                        <%="&euro; " & FormatNumber(tot_imponibile_co, 2)%>
                  </td>
                  <td align="right" >
                        <%="&euro; " & FormatNumber(tot_carburanti, 2) & ""%> 
                  </td>
                  <td align="right" >
                        <%="&euro; " & FormatNumber(tot_danni, 2) & ""%>
                  </td>
                  <td align="right"  >    
                         <%="&euro; " & FormatNumber(tot_imponibile_netto, 2) & ""%>
                  </td>
                  <td align="right"  >
                        <%="&euro; " & FormatNumber(tot_base_calcolo, 2) & ""%>
                  </td>                  
                  <td align="right" >
                        <%="&euro; " & FormatNumber(tot_royalty, 2) %>  <%--Royalty--%>
                  </td>
                  <td align="right"  >                       
                  </td>
                 </tr>

             <% elseIf sta = "5" Then 'Trapani%>
                
             <tr style="font-size:12px;background-color:#ffd800;font-weight:bold;" >

                 <td align="center" >
                       <%-- <%=contrec%>--%>
                 </td>                     
                     
                 <td align="center" >
                        
                  </td>
                  <td align="center" >
                        
                  </td>
                  <td align="right" >
                        <%="&euro; " & FormatNumber(tot_imponibile_co, 2)%>
                  </td>
                  <td align="right"  >
                        <%="&euro; " & FormatNumber(tot_carburanti, 2) & ""%> 
                  </td>
                  <td align="right"  >
                        <%="&euro; " & FormatNumber(tot_danni, 2) & ""%>
                  </td>
                  <td align="right" >    
                         <%="&euro; " & FormatNumber(tot_assicurazioni, 2) & ""%>
                  </td>
                  <td align="right" >
                        <%="&euro; " & FormatNumber(tot_rientro_auto, 2) & ""%>
                  </td>
                  <td align="right" >
                        <%="&euro; " & FormatNumber(tot_oneri_apt, 2) & ""%>
                  </td>
             
                  <td align="right"  >
                        <%="&euro; " & FormatNumber(tot_base_calcolo, 2) %>  <%--base calcolo--%>
                  </td>
             
                  <td align="right"  >
                        <%="&euro; " & FormatNumber(tot_royalty, 2) %>  <%--Royalty--%>
                  </td>
                  <td align="right"  >                       
                  </td>

                 </tr>
            
            
            <% End If %>



        </table>

    
</body>
</html>

