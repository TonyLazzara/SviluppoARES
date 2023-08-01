<%@ Page Language="VB" AutoEventWireup="false" CodeFile="stampaReportPrenotazioni.aspx.vb" Inherits="stampaReportPrenotazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <% Response.ContentType = "application/vnd.ms-excel"%>
    <% Response.AddHeader("Content-Disposition", "attachment;filename=report_prenotazioni.xls")%>
</head>
<body>
  <%--  <form id="form1" runat="server">--%>
    <div>
      <%
          Dim condizione As String = Session("condizione")
          Session("condizione") = ""

          'Dim sqlStr As String = "SELECT prenotazioni.nome_conducente, prenotazioni.cognome_conducente, prenotazioni.giorni, prenotazioni.nr_pren, prenotazioni.numpren, " &
          '" (prenotazioni.PRDATA_OUT) as pickup, (prenotazioni.[PRDATA_PR]) as dropoff, prenotazioni.DATAPREN As data_prenotazione, prenotazioni.riferimento_telefono as telefono, " &
          '"prenotazioni.mail_conducente as email " &
          '    "FROM prenotazioni With(NOLOCK) LEFT JOIN clienti_tipologia With(NOLOCK) On prenotazioni.id_fonte=clienti_tipologia.id " &
          '    "LEFT JOIN stazioni As stazioni1 With(NOLOCK) On prenotazioni.PRID_stazione_out=stazioni1.id " &
          '    "LEFT JOIN stazioni As stazioni2 With(NOLOCK) On prenotazioni.PRID_stazione_pr=stazioni2.id " &
          '    "LEFT JOIN gruppi With(NOLOCK) On prenotazioni.id_gruppo=gruppi.id_gruppo " &
          '    "LEFT JOIN prenotazioni_status With(NOLOCK) On prenotazioni.status=prenotazioni_status.id WHERE prenotazioni.attiva='1' " & condizione

          Dim sqlStr As String = "SELECT prenotazioni.nome_conducente, prenotazioni.cognome_conducente, prenotazioni.giorni, prenotazioni.Nr_Pren, prenotazioni.NUMPREN, prenotazioni.PRDATA_OUT AS pickup, prenotazioni.PRDATA_PR AS dropoff, "
          sqlStr +="prenotazioni.DATAPREN AS data_prenotazione, prenotazioni.riferimento_telefono AS telefono, "
          sqlStr += "prenotazioni.mail_conducente AS email, operatori.cognome + ' ' + operatori.nome AS operatore, clienti_tipologia.descrizione as fonte "
          sqlStr += "FROM prenotazioni WITH (NOLOCK) LEFT OUTER JOIN "
          sqlStr += "clienti_tipologia ON prenotazioni.id_fonte = clienti_tipologia.id LEFT OUTER JOIN "
          sqlStr += "operatori ON prenotazioni.id_operatore_creazione = operatori.id LEFT OUTER JOIN "
          sqlStr += "stazioni AS stazioni2 WITH (NOLOCK) ON prenotazioni.PRID_stazione_pr = stazioni2.id LEFT OUTER JOIN "
          sqlStr += "stazioni AS stazioni1 WITH (NOLOCK) ON prenotazioni.PRID_stazione_out = stazioni1.id LEFT OUTER JOIN "
          sqlStr += "clienti_tipologia AS clienti_tipologia_1 WITH (NOLOCK) ON prenotazioni.id_fonte = clienti_tipologia_1.id LEFT OUTER JOIN "
          sqlStr += "GRUPPI WITH (NOLOCK) ON prenotazioni.ID_GRUPPO = GRUPPI.ID_gruppo LEFT OUTER JOIN "
          sqlStr += "prenotazioni_status WITH (NOLOCK) ON prenotazioni.status = prenotazioni_status.id "
          sqlStr += "WHERE prenotazioni.attiva = '1' " & condizione

          sqlStr = sqlStr


      %>
      <table border="1" cellpadding="1" cellspacing="1">
         <tr>
           <td align="left" style="color: #FFFFFF" bgcolor="#19191b" colspan="14">
              <b>Report Prenotazioni</b>
           </td>
         </tr>
       </table>
        <table border="1" cellpadding="1" cellspacing="1">                       
            <tr>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Nr. Pren.
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Cognome
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Nome
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Email
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              Telefono
            </td>
         
                <td align="left" bgcolor="white" style="font-size:12px" >
              Giorni Noleggio
            </td>
            <td align="left" bgcolor="white" style="font-size:12px" >
              T/KM
            </td>
            <td align="center" bgcolor="white" style="font-size:12px" >
              Totale Extra
            </td>
            <td align="center" bgcolor="white" style="font-size:12px" >
              Totale Tariffa
            </td>
            <td align="center" bgcolor="white" style="font-size:12px" >
              Pick-Up
            </td>
            <td align="center" bgcolor="white" style="font-size:12px" >
              Drop-Off
            </td>
            <td align="center" bgcolor="white" style="font-size:12px" >
              Data Prenotazione
            </td>
            <td align="center" bgcolor="white" style="font-size:12px" >
              Fonte
            </td>
            <td align="center" bgcolor="white" style="font-size:12px" >
              Operatore Creazione
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
                        <%=oRS("numpren")%>
                  </td>
                  <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("cognome_conducente")%>
                  </td>
                  <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("nome_conducente")%>
                  </td>

                     <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("email")%>
                  </td>
                     <td align="left" bgcolor="white" style="font-size:12px" >
                        <%=oRS("telefono")%>
                  </td>




                  <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=oRS("giorni")%>
                  </td>
                 
             <%
                
                 sqlStr = "SELECT ISNULL((prenotazioni_costi.imponibile_scontato + prenotazioni_costi.iva_imponibile_scontato),0) FROM prenotazioni_costi WITH(NOLOCK) WHERE prenotazioni_costi.id_documento=" & oRS("nr_pren") & " AND prenotazioni_costi.nome_costo='Valore Tariffa'"
                 Cmd2 = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                 Dim valore_tariffa As String = Cmd2.ExecuteScalar
                 
                 sqlStr = "SELECT ISNULL((prenotazioni_costi.imponibile_scontato + prenotazioni_costi.iva_imponibile_scontato),0) FROM prenotazioni_costi WITH(NOLOCK) WHERE prenotazioni_costi.id_documento=" & oRS("nr_pren") & " AND prenotazioni_costi.nome_costo='TOTALE'"
                 Cmd2 = New Data.SqlClient.SqlCommand(sqlStr, Dbc2)
                 Dim totale As String = Cmd2.ExecuteScalar
                 
                 Dim accessori As String = CDbl(totale) - CDbl(valore_tariffa)
                 
               %>
                  <td align="center" bgcolor="white" style="font-size:12px" >
                       <%= FormatNumber(CDbl(valore_tariffa), 2)%>
                  </td>
                  <td align="center" bgcolor="white" style="font-size:12px" >
                        <%= FormatNumber(CDbl(accessori), 2)%>
                  </td>
                  <td align="center" bgcolor="white" style="font-size:12px" >
                       <%= FormatNumber(CDbl(totale), 2)%>
                  </td>



                  <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=FormatDateTime(oRS("pickup"), DateFormat.ShortDate)%>
                  </td>
                  <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=FormatDateTime(oRS("dropoff"), DateFormat.ShortDate)%>
                  </td>
                  <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=FormatDateTime(oRS("data_prenotazione"), DateFormat.ShortDate)%>
                  </td>
                  <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=oRS("fonte")%>
                  </td>
                 
                   <td align="center" bgcolor="white" style="font-size:12px" >
                        <%=oRS("operatore") %>
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
   <%-- </form>--%>
</body>
</html>
