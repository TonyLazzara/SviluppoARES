<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage2.master" AutoEventWireup="false" CodeFile="situazione_flotta_gruppi.aspx.vb" Inherits="situazione_flotta_gruppi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
        input[type=submit]
        {
          background-color : #369061;
          font-weight:bold;
          color:White;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td  align="left" style="color: #FFFFFF" bgcolor="#19191b">
             <b>Dettaglio disponibilità per gruppi auto - <%=Request.QueryString("giorno")%> - <%=getStazione(Request.QueryString("staz"))%></b>
           </td>
         </tr>
  </table>

  <%

      Dim k As Integer = 1
      Dim i As Integer = 0

      'PER PRIMA COSA SELEZIONO TUTTI I GRUPPI PRESENTI A SISTEMA
      Dim gruppi(60) As String
      Dim valoreInizialeGruppi(60) As Integer

      Do While i < 60
          valoreInizialeGruppi(i) = 0
          i = i + 1
      Loop

      gruppi = funzioni_comuni.getGruppi()

      Dim nuovaRiga As Boolean

      If gruppi(0) <> "000" Then
          nuovaRiga = True
      Else
          nuovaRiga = False
      End If


      Dim situazione(900) As Integer
      Dim funzioni As New funzioni_comuni

      Dim Data1 As DateTime = Request.QueryString("giorno") & " 00:00:00"
      Dim stazione As Integer = Request.QueryString("staz")

      'SELEZIONO LA DISPONIBILITA' INIZIALE PER OGNI GRUPPO DAL GIORNO ODIERNO FINO AL GIORNO SELEZIONATO
      Dim dataOdierna As DateTime = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()) & " 00:00:00"



      Dim giorni As Integer = DateDiff(DateInterval.Day, dataOdierna, Data1)

      valoreInizialeGruppi = funzioni_comuni.getDisponibilitaOdierna(dataOdierna, stazione, gruppi)  'RESTITUISCE LA DISPONIBILITA' ALLA DATA ODIERNA

      Do While giorni > 0
          i = 0
          k = 1
          situazione = funzioni_comuni.getSituazione_x_gruppi_new(dataOdierna, stazione)

          Do While gruppi(i) <> "000"
              valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 29) + situazione(k + 44) - situazione(k + 14)

              k = k + 45
              i = i + 1
          Loop

          dataOdierna = dataOdierna.AddDays(1)
          giorni = DateDiff(DateInterval.Day, dataOdierna, Data1)
      Loop

      'QUINDI SELEZIONO LA SITUAZIONE DELLA FLOTTA PER LA GIORNATA CORRENTE.    

      k = 1
      i = 0

      Dim colonna1 As Boolean

      situazione = funzioni_comuni.getSituazione_x_gruppi_new(Data1, stazione)


            %>
     
        <table border="1" cellpadding="1" cellspacing="1" width="1024px" >
         <% Do While nuovaRiga%>
         <tr>
           <td align="left" bgcolor="white" >
             <%  If gruppi(i) <> "000" Then
                     %>
                     <%=gruppi(i)%>
               
                     <%  i = i + 1
                         colonna1 = True
                      %>
             <%  Else
                     nuovaRiga = False
                     colonna1 = False
                 End If
              %> 
               
           </td>
         </tr>
         <tr>
           <td>
             <%If colonna1 Then%>
              <table border="1" cellpadding="0" cellspacing="0">
               <tr>
                  <td width="20px" style="font-size:16px" align="center">   
                    <b><%=valoreInizialeGruppi(i - 1)%></b>
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     00/08
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     08/09
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     09/10
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     10/11
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     11/12
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     12/13
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     13/14
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     14/15
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     15/16
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     16/17
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     17/18
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     18/19
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     19/20
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     20/24
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     00/24
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    <asp:Label ID="Label1" runat="server" Text="PR.U." ToolTip="Previste Uscite"></asp:Label>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <%=situazione(k)%>     <%--00/08--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <%=situazione(k + 1)%> <%--08/09--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <%=situazione(k + 2)%> <%--09/10--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <%=situazione(k + 3)%> <%--10/11--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <%=situazione(k + 4)%> <%--11/12--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <%=situazione(k + 5)%> <%--12/13--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <%=situazione(k + 6)%> <%--13/14--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <%=situazione(k + 7)%> <%--14/15--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <%=situazione(k + 8)%> <%--15/16--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <%=situazione(k + 9)%> <%--16/17--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <%=situazione(k + 10)%> <%--17/18--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <%=situazione(k + 11)%> <%--18/19--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <%=situazione(k + 12)%> <%--19/20--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <%=situazione(k + 13)%> <%--20/24--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <%=situazione(k + 14)%> <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    <asp:Label ID="Label2" runat="server" Text="R.P." ToolTip="Rientri Previsti (prenotazioni e Trasferimenti/ODL/Lavaggi)"></asp:Label>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 15)%> <%--00/08--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 16)%> <%--08/09--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 17)%> <%--09/10--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 18)%> <%--10/11--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 19)%> <%--11/12--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 20)%> <%--12/13--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 21)%> <%--13/14--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 22)%> <%--14/15--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 23)%> <%--15/16--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 24)%> <%--16/17--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 25)%> <%--17/18--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 26)%> <%--18/19--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 27)%> <%--19/20--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 28)%> <%--20/24--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 29)%> <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white"> 
                    <asp:Label ID="Label3" runat="server" Text="R.C." ToolTip="Rientri da Contratto"></asp:Label>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 30)%> <%--00/08--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 31)%> <%--08/09--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 32)%>  <%--09/10--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 33)%>  <%--10/11--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 34)%>  <%--11/12--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 35)%>  <%--12/13--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 36)%>  <%--13/14--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 37)%>  <%--14/15--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 38)%>  <%--15/16--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 39)%>  <%--16/17--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 40)%>  <%--17/18--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 41)%>  <%--18/19--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 42)%>  <%--19/20--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 43)%>  <%--20/24--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                    <%=situazione(k + 44)%>   <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    TOT
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 15) + situazione(k + 30) - situazione(k)%>
                     <%=valoreInizialeGruppi(i - 1)%>      <%-- TOT. 00/08--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 16) + situazione(k + 31) - situazione(k + 1)%>
                     <%=valoreInizialeGruppi(i - 1)%>      <%-- TOT. 08/09--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 17) + situazione(k + 32) - situazione(k + 2)%>
                     <%=valoreInizialeGruppi(i - 1)%>      <%-- TOT. 09/10--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 18) + situazione(k + 33) - situazione(k + 3)%>
                     <%=valoreInizialeGruppi(i - 1)%>      <%-- TOT. 10/11--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 19) + situazione(k + 34) - situazione(k + 4)%>
                     <%=valoreInizialeGruppi(i - 1)%>      <%-- TOT. 11/12--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 20) + situazione(k + 35) - situazione(k + 5)%>
                     <%=valoreInizialeGruppi(i - 1)%>      <%-- TOT. 12/13--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 21) + situazione(k + 36) - situazione(k + 6)%>
                     <%=valoreInizialeGruppi(i - 1)%>      <%-- TOT. 13/14--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 22) + situazione(k + 37) - situazione(k + 7)%>
                     <%=valoreInizialeGruppi(i - 1I)%>      <%-- TOT. 14/15--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 23) + situazione(k + 38) - situazione(k + 8)%>
                     <%=valoreInizialeGruppi(i - 1)%>      <%-- TOT. 15/16--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 24) + situazione(k + 39) - situazione(k + 9)%>
                     <%=valoreInizialeGruppi(i - 1)%>      <%-- TOT. 16/17--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 25) + situazione(k + 40) - situazione(k + 10)%>
                     <%=valoreInizialeGruppi(i - 1)%>      <%-- TOT. 17/18--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 26) + situazione(k + 41) - situazione(k + 11)%>
                     <%=valoreInizialeGruppi(i - 1)%>      <%-- TOT. 18/19--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 27) + situazione(k + 42) - situazione(k + 12)%>
                     <%=valoreInizialeGruppi(i - 1)%>      <%-- TOT. 19/20--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 28) + situazione(k + 43) - situazione(k + 13)%>
                     <%=valoreInizialeGruppi(i - 1)%>      <%-- TOT. 20/24--%>
                  </td>
                  <td width="20px" style="font-size:16px" align="center">  
                     <%--<b><%=valoreInizialeGruppi(i - 1)%></b> --%>
                  </td>
               </tr>
             </table>
            <%End If%>
           </td>
             <% k = k + 45%>
         </tr>
         <% Loop%>
         <tr>
           <td colspan="4" align="center">
               <a target="_blank" href="/stampe/stampa_situazione_flotta_gruppi.aspx?giorno=<%=Request.QueryString("giorno") %>&staz=<%=Request.querystring("staz") %>">Stampa Excel</a>
               <%--&nbsp;
               <a target="_blank"href="GeneraPdf.aspx?pagina=orizzontale&DocPdf=/stampe/stampa_situazione_flotta_gruppi&giorno=<%=Request.QueryString("giorno") %>&staz=<%=Request.querystring("staz") %>&pdf=pdf">Stampa Pdf</a>--%>
           </td>
         </tr>
         </table>

  
</asp:Content>

