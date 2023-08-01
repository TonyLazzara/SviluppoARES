<%@ Page Language="VB" AutoEventWireup="false" CodeFile="stampaReportContratti.aspx.vb" Inherits="stampaReportContratti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <% Response.ContentType = "application/vnd.ms-excel"%>
    <% Response.AddHeader("Content-Disposition", "attachment;filename=report_contratti.xls")%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <%
          Dim condizione As String = Session("condizione")
          Session("condizione") = ""

          'Dim sqlStr As String = "SELECT contratti.id, contratti.num_contratto, conducenti.cognome As cognome_conducente, conducenti.nome As nome_conducente," &
          '    "contratti.giorni, conducenti.email, conducenti.cell, conducenti.telefono, conducenti.sesso, contratti.eta_primo_guidatore, conducenti.Data_Nascita, conducenti.codfis, contratti_conducenti.city, " &
          '    "nazioni.nazione, conducenti.cap as CAP " &
          '    "FROM contratti WITH(NOLOCK) " &
          '    "LEFT JOIN stazioni AS stazioni1 WITH(NOLOCK) ON contratti.id_stazione_uscita=stazioni1.id " &
          '    "LEFT JOIN stazioni As stazioni2 WITH(NOLOCK) ON contratti.id_stazione_rientro=stazioni2.id " &
          '    "LEFT JOIN stazioni As stazioni3 WITH(NOLOCK) ON contratti.id_stazione_presunto_rientro=stazioni3.id " &
          '    "LEFT JOIN veicoli WITH(NOLOCK) ON contratti.id_veicolo=veicoli.id " &
          '    "LEFT JOIN conducenti WITH(NOLOCK) ON contratti.id_primo_conducente=conducenti.id_conducente " &
          '    "LEFT JOIN contratti_conducenti WITH(NOLOCK) ON contratti.num_contratto=contratti_conducenti.num_contratto AND num_conducente=1 " &
          '    "LEFT JOIN nazioni WITH(NOLOCK) ON conducenti.nazione=nazioni.id_nazione " &
          '    "LEFT JOIN contratti_status WITH(NOLOCK) ON contratti.status=contratti_status.id " &
          '    "LEFT JOIN clienti_tipologia WITH(NOLOCK) ON contratti.id_fonte=clienti_tipologia.id  WHERE contratti.attivo='1' " & condizione

          'ultima al 11.04.2023 salvo
          '  Dim sqlStr As String = "Select contratti.id, contratti.num_contratto, CONDUCENTI.COGNOME As cognome_conducente, CONDUCENTI.nome As nome_conducente, contratti.giorni, CONDUCENTI.EMAIL, CONDUCENTI.cell, CONDUCENTI.Telefono, CONDUCENTI.SESSO, " &
          '"contratti.eta_primo_guidatore, CONDUCENTI.Data_Nascita, CONDUCENTI.CODFIS, contratti_conducenti.City, nazioni.NAZIONE, CONDUCENTI.Cap As CAP, contratti.id_tariffa, contratti.id_tariffe_righe, tariffe_righe.id_tempo_km," &
          '"tariffe.codice as Tariffa From contratti WITH (NOLOCK) INNER Join " &
          '"tariffe_righe On contratti.id_tariffe_righe = tariffe_righe.id INNER Join " &
          '"tariffe On tariffe_righe.id_tariffa = tariffe.id LEFT OUTER Join stazioni As stazioni1 WITH (NOLOCK) ON contratti.id_stazione_uscita = stazioni1.id LEFT OUTER Join " &
          '"stazioni As stazioni2 WITH (NOLOCK) ON contratti.id_stazione_rientro = stazioni2.id LEFT OUTER Join " &
          '"stazioni As stazioni3 WITH (NOLOCK) ON contratti.id_stazione_presunto_rientro = stazioni3.id LEFT OUTER Join " &
          '"veicoli WITH (NOLOCK) ON contratti.id_veicolo = veicoli.id LEFT OUTER Join " &
          '"CONDUCENTI WITH (NOLOCK) ON contratti.id_primo_conducente = CONDUCENTI.ID_CONDUCENTE LEFT OUTER Join " &
          '"contratti_conducenti WITH (NOLOCK) ON contratti.num_contratto = contratti_conducenti.num_contratto And contratti_conducenti.num_conducente = 1 LEFT OUTER Join " &
          '"nazioni WITH (NOLOCK) ON CONDUCENTI.NAZIONE = nazioni.ID_NAZIONE LEFT OUTER Join " &
          '"contratti_status WITH (NOLOCK) ON contratti.status = contratti_status.id LEFT OUTER Join " &
          '"clienti_tipologia WITH (NOLOCK) ON contratti.id_fonte = clienti_tipologia.id " &
          '"Where contratti.attivo = '1'  " & condizione

          ''aggiornati il 11.04.2023 salvo
          'Dim sqlStr As String = "Select contratti.id, contratti.num_contratto, CONDUCENTI.COGNOME As cognome_conducente, CONDUCENTI.nome As nome_conducente, contratti.giorni,
          'CONDUCENTI.EMAIL, CONDUCENTI.cell, CONDUCENTI.Telefono, CONDUCENTI.SESSO, "
          'sqlStr += "contratti.eta_primo_guidatore, CONDUCENTI.Data_Nascita, CONDUCENTI.CODFIS, contratti_conducenti.City, nazioni.NAZIONE, CONDUCENTI.Cap As CAP, contratti.id_tariffa, contratti.id_tariffe_righe, tariffe_righe.id_tempo_km,"
          'sqlStr += "tariffe.codice As Tariffa, Ditte.Rag_soc From contratti WITH (NOLOCK) INNER Join tariffe_righe On contratti.id_tariffe_righe = tariffe_righe.id INNER Join "
          'sqlStr += "tariffe On tariffe_righe.id_tariffa = tariffe.id INNER Join DITTE On contratti.id_cliente = DITTE.Id_Ditta LEFT OUTER Join "
          'sqlStr += "clienti_tipologia WITH (NOLOCK) ON contratti.id_fonte = clienti_tipologia.id LEFT OUTER Join stazioni As stazioni1 WITH (NOLOCK) ON contratti.id_stazione_uscita = stazioni1.id LEFT OUTER Join "
          'sqlStr += "stazioni As stazioni2 WITH (NOLOCK) ON contratti.id_stazione_rientro = stazioni2.id LEFT OUTER Join stazioni As stazioni3 WITH (NOLOCK) ON contratti.id_stazione_presunto_rientro = stazioni3.id LEFT OUTER Join "
          'sqlStr += "veicoli WITH (NOLOCK) ON contratti.id_veicolo = veicoli.id LEFT OUTER Join "
          'sqlStr += "CONDUCENTI WITH (NOLOCK) ON contratti.id_primo_conducente = CONDUCENTI.ID_CONDUCENTE LEFT OUTER Join "
          'sqlStr += "contratti_conducenti WITH (NOLOCK) ON contratti.num_contratto = contratti_conducenti.num_contratto And contratti_conducenti.num_conducente = 1 LEFT OUTER Join "
          'sqlStr += "nazioni WITH (NOLOCK) ON CONDUCENTI.NAZIONE = nazioni.ID_NAZIONE LEFT OUTER Join contratti_status WITH (NOLOCK) ON contratti.status = contratti_status.id "


          'Dim sqlStr As String = "SELECT conducenti1.telefono,conducenti1.email as email_conducente, tariffe.codice as tariffa, contratti.giorni AS giorni_noleggio, conducenti1.Cap AS cap_primo_guidatore, nazioni.NAZIONE AS nazione_primo_guidatore, conducenti1.City AS city_primo_guidatore, "
          'sqlStr += "conducenti1.CODFIS AS cod_fisc_primo_guidatore, contratti.eta_primo_guidatore, conducenti1.Data_Nascita AS data_nascita_primo_guidatore, contratti.id AS id_contratto, contratti_status.descrizione AS status, contratti.num_contratto,"
          'sqlStr += "stazioni1.codice + ' ' + stazioni1.nome_stazione AS staz_uscita, stazioni2.codice + ' ' + stazioni2.nome_stazione AS staz_rientro, stazioni3.codice + ' ' + stazioni3.nome_stazione AS staz_presunto_rientro, CONVERT(char(10), "
          'sqlStr += "contratti.data_uscita, 103) AS data_uscita, DATEPART(hh, contratti.data_uscita) AS ore_uscita, DATEPART(mi, contratti.data_uscita) AS minuti_uscita, CONVERT(Char(10), contratti.data_rientro, 103) AS data_rientro, DATEPART(hh, "
          'sqlStr += "contratti.data_rientro) AS ore_rientro, DATEPART(mi, contratti.data_rientro) AS minuti_rientro, CONVERT(Char(10), contratti.data_presunto_rientro, 103) AS data_presunto_rientro, DATEPART(hh, contratti.data_presunto_rientro) "
          'sqlStr += "AS ore_presunto_rientro, DATEPART(mi, contratti.data_presunto_rientro) AS minuti_presunto_rientro, veicoli.targa AS veicolo, conducenti1.COGNOME AS cognome_primo_conducente, conducenti1.nome AS nome_primo_conducente, "
          'sqlStr += "conducenti2.COGNOME AS cognome_secondo_conducente, conducenti2.nome AS nome_secondo_conducente, contratti.prenotazione_prepagata AS prepagata, clienti_tipologia.descrizione AS tipo_cliente, contratti.rif_to,"
          'sqlStr += "GRUPPI.cod_gruppo, contratti_ditte.rag_soc, conducenti1.SESSO, conducenti1.EMAIL, conducenti1.cell FROM     stazioni AS stazioni2 WITH (NOLOCK) RIGHT OUTER JOIN "
          'sqlStr += "contratti_ditte RIGHT OUTER JOIN contratti WITH (NOLOCK) LEFT OUTER JOIN tariffe ON contratti.id_tariffa = tariffe.id LEFT OUTER JOIN "
          'sqlStr += "MODELLI INNER JOIN veicoli WITH (NOLOCK) ON MODELLI.ID_MODELLO = veicoli.id_modello INNER JOIN "
          'sqlStr += "GRUPPI ON MODELLI.ID_Gruppo = GRUPPI.ID_gruppo ON contratti.id_veicolo = veicoli.id ON contratti_ditte.num_contratto = contratti.num_contratto LEFT OUTER JOIN "
          'sqlStr += "stazioni AS stazioni1 WITH (NOLOCK) ON contratti.id_stazione_uscita = stazioni1.id ON stazioni2.id = contratti.id_stazione_rientro LEFT OUTER JOIN "
          'sqlStr += "stazioni AS stazioni3 WITH (NOLOCK) ON contratti.id_stazione_presunto_rientro = stazioni3.id LEFT OUTER JOIN nazioni INNER JOIN "
          'sqlStr += "CONDUCENTI AS conducenti1 WITH (NOLOCK) ON nazioni.ID_NAZIONE = conducenti1.NAZIONE ON contratti.id_primo_conducente = conducenti1.ID_CONDUCENTE LEFT OUTER JOIN "
          'sqlStr += "CONDUCENTI AS conducenti2 WITH (NOLOCK) ON contratti.id_secondo_conducente = conducenti2.ID_CONDUCENTE LEFT OUTER JOIN "
          'sqlStr += "contratti_status WITH (NOLOCK) ON contratti.status = contratti_status.id LEFT OUTER JOIN "
          'sqlStr += "clienti_tipologia WITH (NOLOCK) ON contratti.id_fonte = clienti_tipologia.id "

          'sqlStr += "Where contratti.attivo = '1' " & condizione



          Dim sqlStr As String = "Select  operatori.cognome + '  ' + operatori.nome AS Operatore_Creazione, contratti.num_prenotazione AS Num_Prenotazione, conducenti1.Telefono, conducenti1.EMAIL AS email_conducente, tariffe.codice AS tariffa, "
          sqlStr += "contratti.giorni As giorni_noleggio, conducenti1.Cap As cap_primo_guidatore, nazioni.NAZIONE As nazione_primo_guidatore, conducenti1.City As city_primo_guidatore, conducenti1.CODFIS As cod_fisc_primo_guidatore, "
          sqlStr += "contratti.eta_primo_guidatore, conducenti1.Data_Nascita As data_nascita_primo_guidatore, contratti.id As id_contratto, contratti_status.descrizione As status, contratti.num_contratto,"
          sqlStr += "stazioni1.codice + ' ' + stazioni1.nome_stazione AS staz_uscita, stazioni2.codice + ' ' + stazioni2.nome_stazione AS staz_rientro, stazioni3.codice + ' ' + stazioni3.nome_stazione AS staz_presunto_rientro, CONVERT(char(10), "
          sqlStr += "contratti.data_uscita, 103) AS data_uscita, DATEPART(hh, contratti.data_uscita) AS ore_uscita, DATEPART(mi, contratti.data_uscita) AS minuti_uscita, CONVERT(Char(10), contratti.data_rientro, 103) AS data_rientro,  "

          sqlStr += "DATEPART(hh,contratti.data_rientro) AS ore_rientro, DATEPART(mi, contratti.data_rientro) AS minuti_rientro, CONVERT(Char(10), contratti.data_presunto_rientro, 103) AS data_presunto_rientro, DATEPART(hh, contratti.data_presunto_rientro) "
          sqlStr += "AS ore_presunto_rientro, DATEPART(mi, contratti.data_presunto_rientro) AS minuti_presunto_rientro, veicoli.targa AS veicolo, conducenti1.COGNOME AS cognome_primo_conducente, "
          sqlStr += "conducenti1.nome AS nome_primo_conducente, conducenti2.COGNOME As cognome_secondo_conducente, conducenti2.nome As nome_secondo_conducente, contratti.prenotazione_prepagata As prepagata, "
          sqlStr += "clienti_tipologia.descrizione AS tipo_cliente, contratti.rif_to, GRUPPI.cod_gruppo, contratti_ditte.rag_soc, conducenti1.SESSO, conducenti1.EMAIL, conducenti1.cell "
          sqlStr += "From stazioni As stazioni3 WITH (NOLOCK) RIGHT OUTER Join "
          sqlStr += "nazioni INNER Join "
          sqlStr += "CONDUCENTI As conducenti1 WITH (NOLOCK) ON nazioni.ID_NAZIONE = conducenti1.NAZIONE RIGHT OUTER Join "
          sqlStr += "contratti WITH (NOLOCK) INNER Join "
          sqlStr += "operatori On contratti.id_operatore_creazione = operatori.id LEFT OUTER Join "
          sqlStr += "MODELLI INNER Join "
          sqlStr += "veicoli WITH (NOLOCK) ON MODELLI.ID_MODELLO = veicoli.id_modello INNER Join "
          sqlStr += "GRUPPI On MODELLI.ID_Gruppo = GRUPPI.ID_gruppo ON contratti.id_veicolo = veicoli.id ON conducenti1.ID_CONDUCENTE = contratti.id_primo_conducente LEFT OUTER Join "
          sqlStr += "CONDUCENTI As conducenti2 WITH (NOLOCK) ON contratti.id_secondo_conducente = conducenti2.ID_CONDUCENTE ON stazioni3.id = contratti.id_stazione_presunto_rientro LEFT OUTER Join "
          sqlStr += "stazioni As stazioni1 WITH (NOLOCK) ON contratti.id_stazione_uscita = stazioni1.id LEFT OUTER Join "
          sqlStr += "stazioni As stazioni2 WITH (NOLOCK) ON contratti.id_stazione_rientro = stazioni2.id LEFT OUTER Join "
          sqlStr += "tariffe On contratti.id_tariffa = tariffe.id LEFT OUTER Join "
          sqlStr += "contratti_ditte On contratti.num_contratto = contratti_ditte.num_contratto LEFT OUTER Join "
          sqlStr += "contratti_status WITH (NOLOCK) ON contratti.status = contratti_status.id LEFT OUTER Join "
          sqlStr += "clienti_tipologia WITH (NOLOCK) ON contratti.id_fonte = clienti_tipologia.id  "
          'sqlStr += "Where (contratti.attivo = '1') AND (contratti.status = '2') AND (contratti.data_uscita BETWEEN CONVERT(datetime, '2023-5-1 00:00:00', 102) AND CONVERT(datetime, '2023-5-18 23:59:59', 102)) AND (contratti.id_stazione_uscita = '3')
          sqlStr += "Where contratti.attivo = '1' " & condizione
          'sqlStr += "ORDER BY data_uscita DESC"




          sqlStr = sqlStr


      %>

      <table border="1" cellpadding="1" cellspacing="1">
         <tr>
           <td align="left" style="color: #FFFFFF" bgcolor="#19191b" colspan="21">
              <b>Report Contratti</b>
           </td>
         </tr>
       </table>
        <table border="1" cellpadding="1" cellspacing="1">
           <tr>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Operatore Creazione
            </td>     
            <td align="left" bgcolor="white" style="font-size:12px" >
              Da Pren. Num. Creazione
            </td>  
               
               <td align="left" bgcolor="white" style="font-size:12px" >
              Num. Contratto
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Cognome
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Nome
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Mail
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Cellulare
            </td>
               <td align="left" bgcolor="white" style="font-size:12px" >
              Telefono
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Sesso
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Eta'
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Data Nascita
            </td>
            
            <td align="left" bgcolor="white" style="font-size:12px" >
              Codice Fiscale
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Comune di residenza
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Nazione di residenza
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              CAP
            </td>

            <td align="left" bgcolor="white" style="font-size:12px" >
              Giorni Noleggio
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              T/KM
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Totale Extra
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Totale Tariffa
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Totale Incassato
            </td>

            <td align="left" bgcolor="white" style="font-size:12px" >
              Tariffa
            </td>

          </tr>
        
        <%
            Dim Dbc As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc.Open()
            Dim Dbc2 As New Data.SqlClient.SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SicilyConnectionString").ConnectionString)
            Dbc2.Open()
            Dim Cmd As New Data.SqlClient.SqlCommand(sqlStr, Dbc)
            Dim Cmd2 As New Data.SqlClient.SqlCommand("", Dbc2)
            Dim oRS As Data.SqlClient.SqlDataReader
            oRS = Cmd.ExecuteReader()

            While oRS.Read()
                %>
                 <tr>
                    <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("Operatore_Creazione")%>
                  </td>
                    <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("Num_Prenotazione")%>
                  </td>


                     <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("num_contratto")%>
                  </td>
                  <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("cognome_primo_conducente")%>
                  </td>
                  <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("nome_primo_conducente")%>
                  </td>
                  <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("email_conducente") & ""%> 
                  </td>
                  <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("cell") & ""%>
                  </td>
                     <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("telefono") & ""%>
                  </td>
                  <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("sesso") & ""%>
                  </td>
                  <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("eta_primo_guidatore") & ""%>
                  </td>
                  <td align="left" bgcolor="white" style="font-size:12px" >
                        <%= FormatDateTime(oRS("data_nascita_primo_guidatore"), vbShortDate) & ""%>
                  </td>

                  <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("cod_fisc_primo_guidatore") & ""%>
                  </td>
                  <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("city_primo_guidatore") & ""%>
                  </td>
                  <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("nazione_primo_guidatore") & ""%>
                  </td>
                     <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("cap_primo_guidatore") & ""%>
                  </td>
                  <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("giorni_noleggio")%>
                  </td>
                 
             <%

                 sqlStr = "SELECT ISNULL((contratti_costi.imponibile_scontato + contratti_costi.iva_imponibile_scontato),0) as tariffa FROM contratti_costi WITH(NOLOCK) WHERE contratti_costi.id_documento=" & oRS("id_contratto") & " AND contratti_costi.nome_costo='Valore Tariffa'"
                 Cmd2 = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                 Dim valore_tariffa As String = Cmd2.ExecuteScalar

                 sqlStr = "SELECT ISNULL((contratti_costi.imponibile_scontato + contratti_costi.iva_imponibile_scontato),0) as tariffa FROM contratti_costi WITH(NOLOCK) WHERE contratti_costi.id_documento=" & oRS("id_contratto") & " AND contratti_costi.nome_costo='TOTALE'"
                 Cmd2 = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                 Dim totale As String = Cmd2.ExecuteScalar

                 Dim accessori As String = CDbl(totale) - CDbl(valore_tariffa)

               %>
                  <td align="left" bgcolor="white" style="font-size:12px" >
                       <%= FormatNumber(CDbl(valore_tariffa), 2)%>
                  </td>
                  <td align="left" bgcolor="white" style="font-size:12px" >
                        <%= FormatNumber(CDbl(accessori), 2)%>
                  </td>
                  <td align="left" bgcolor="white" style="font-size:12px" >
                       <%= FormatNumber(CDbl(totale), 2)%>
                  </td>

                  <td align="left" bgcolor="white" style="font-size:12px" >
                       <%= GetTotaleIncassato(oRS("num_contratto"))%>
                  </td>
                  
                     <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("tariffa")%>   <%--Aggiunto salvo 14.03.2023 --%>
                  </td>
                 
                 </tr>
               <%

                   End While

                   Dbc.Close()
                   Dbc.Dispose()
                   Dbc = Nothing
                   Dbc2.Close()
                   Dbc2.Dispose()
                   Dbc2 = Nothing
        %>

             


        </table>
    </div>
    </form>
</body>
</html>

