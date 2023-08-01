<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage2.master" AutoEventWireup="false" CodeFile="situazione_flotta_gruppi_old.aspx.vb" Inherits="situazione_flotta_gruppi_old" %>

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
          situazione = funzioni_comuni.getSituazione_x_gruppi(dataOdierna, stazione)
          
          Do While gruppi(i) <> "000"
              valoreInizialeGruppi(i) = valoreInizialeGruppi(i) + situazione(k + 14) + situazione(k + 9) - situazione(k + 4)
              
              k = k + 15
              i = i + 1
          Loop
          
          dataOdierna = dataOdierna.AddDays(1)
          giorni = DateDiff(DateInterval.Day, dataOdierna, Data1)
      Loop
     
      'QUINDI SELEZIONO LA SITUAZIONE DELLA FLOTTA PER LA GIORNATA CORRENTE.    
  
      k = 1
      i = 0
      
      Dim colonna1 As Boolean
      Dim colonna2 As Boolean
      Dim colonna3 As Boolean
      Dim colonna4 As Boolean
      
      situazione = funzioni_comuni.getSituazione_x_gruppi(Data1, stazione)
      
     
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
           <td align="left" bgcolor="white" >
             <%  If gruppi(i) <> "000" Then %>
                     <%=gruppi(i)%>
                      
                     <%  i = i + 1
                         colonna2 = True
                         %>
             <%  Else
                     nuovaRiga = False
                     colonna2 = False
                 End If
              %>  
           </td>
           <td align="left" bgcolor="white" >
             <%  If gruppi(i) <> "000" Then %>
                     <%=gruppi(i)%>

                     <%  i = i + 1
                         colonna3 = True
                         %>
             <%  Else
                     nuovaRiga = False
                     colonna3 = False
                 End If
              %>  
           </td>
           <td align="left" bgcolor="white" >
             <%  If gruppi(i) <> "000" Then%>
                     <%=gruppi(i)%>
                     
                     <%  i = i + 1
                         colonna4 = True
                         %>
             <%  Else
                     nuovaRiga = False
                     colonna4 = False
                     
                     If colonna1 = False Then
                         i = i + 4
                     ElseIf colonna2 = False Then
                         i = i + 3
                     ElseIf colonna3 = False Then
                         i = i + 2
                     Else
                         i = i + 1
                     End If
                 End If
              %>  
           </td>
         </tr>
         <tr>
           <td>
             <%If colonna1 Then%>
              <table border="1" cellpadding="0" cellspacing="0">
               <tr>
                  <td width="20px" style="font-size:16px">   
                    <b><%=valoreInizialeGruppi(i - 4)%></b>
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     00/12
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     12/16
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     16/20
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
                    PR.U.
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=situazione(k)%>     <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=situazione(k + 1)%> <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=situazione(k + 2)%> <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=situazione(k + 3)%> <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=situazione(k + 4)%> <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    R.P.
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 5)%> <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 6)%> <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 7)%> <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 8)%> <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 9)%> <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white"> 
                    R.C.
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 10)%> <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 11)%> <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 12)%>  <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 13)%>  <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 14)%>   <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    TOT
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% valoreInizialeGruppi(i - 4) = valoreInizialeGruppi(i - 4) + situazione(k + 10) + situazione(k + 5) - situazione(k)%>
                     <%=valoreInizialeGruppi(i - 4)%>      <%-- TOT. 08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px"> 
                     <% valoreInizialeGruppi(i - 4) = valoreInizialeGruppi(i - 4) + situazione(k + 11) + situazione(k + 6) - situazione(k + 1)%>
                     <%=valoreInizialeGruppi(i - 4)%>  <%-- TOT. 12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% valoreInizialeGruppi(i - 4) = valoreInizialeGruppi(i - 4) + situazione(k + 12) + situazione(k + 7) - situazione(k + 2)%>
                     <%=valoreInizialeGruppi(i - 4)%>  <%-- TOT. 16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% valoreInizialeGruppi(i - 4) = valoreInizialeGruppi(i - 4) + situazione(k + 13) + situazione(k + 8) - situazione(k + 3)%>
                     <%=valoreInizialeGruppi(i - 4)%>  <%-- TOT. 20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">  
                     <b><%=valoreInizialeGruppi(i - 4)%></b> <%-- TOT. GIORN.--%>
                  </td>
               </tr>
             </table>
            <%End If%>
           </td>
             <% k = k + 15%>
            <td>
            <%If colonna2 Then%>
              <table border="1" cellpadding="0" cellspacing="0">
               <tr>
                  <td width="20px" style="font-size:16px">   
                    <b><%=valoreInizialeGruppi(i - 3)%></b>
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     00/12
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     12/16
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     16/20
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
                    PR.U.
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k)%>      <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 1)%>  <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 2)%>   <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 3)%>   <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 4)%>   <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    R.P.
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 5)%>   <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 6)%>   <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 7)%>   <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 8)%> <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 9)%>  <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white"> 
                    R.C.
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 10)%>  <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 11)%>  <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 12)%>   <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 13)%>   <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 14)%>    <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    TOT
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% valoreInizialeGruppi(i - 3) = valoreInizialeGruppi(i - 3) + situazione(k + 10) + situazione(k + 5) - situazione(k)%>
                     <%=valoreInizialeGruppi(i - 3)%>      <%-- TOT. 08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px"> 
                     <% valoreInizialeGruppi(i - 3) = valoreInizialeGruppi(i - 3) + situazione(k + 11) + situazione(k + 6) - situazione(k + 1)%>
                     <%=valoreInizialeGruppi(i - 3)%>  <%-- TOT. 12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% valoreInizialeGruppi(i - 3) = valoreInizialeGruppi(i - 3) + situazione(k + 12) + situazione(k + 7) - situazione(k + 2)%>
                     <%=valoreInizialeGruppi(i - 3)%>  <%-- TOT. 16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% valoreInizialeGruppi(i - 3) = valoreInizialeGruppi(i - 3) + situazione(k + 13) + situazione(k + 8) - situazione(k + 3)%>
                     <%=valoreInizialeGruppi(i - 3)%>  <%-- TOT. 20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <b><%=valoreInizialeGruppi(i - 3)%></b> <%-- TOT. GIORN.--%>
                  </td>
               </tr>
             </table>
            <%End If%>
           </td>
            <% k = k + 15%>
            <td>
             <%If colonna3 Then%>
              <table border="1" cellpadding="0" cellspacing="0">
               <tr>
                  <td width="20px" style="font-size:16px">   
                    <b><%=valoreInizialeGruppi(i - 2)%></b>
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     00/12
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     12/16
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     16/20
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
                    PR.U.
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=situazione(k)%>      <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=situazione(k + 1)%>  <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 2)%>   <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 3)%>   <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 4)%>   <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    R.P.
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 5)%>   <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 6)%>   <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 7)%>   <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 8)%>   <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 9)%>   <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white"> 
                    R.C.
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 10)%>   <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 11)%>   <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 12)%>   <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 13)%>   <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 14)%>    <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    TOT
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% valoreInizialeGruppi(i - 2) = valoreInizialeGruppi(i - 2) + situazione(k + 10) + situazione(k + 5) - situazione(k)%>
                     <%=valoreInizialeGruppi(i - 2)%>      <%-- TOT. 08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px"> 
                     <% valoreInizialeGruppi(i - 2) = valoreInizialeGruppi(i - 2) + situazione(k + 11) + situazione(k + 6) - situazione(k + 1)%>
                     <%=valoreInizialeGruppi(i - 2)%>  <%-- TOT. 12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% valoreInizialeGruppi(i - 2) = valoreInizialeGruppi(i - 2) + situazione(k + 12) + situazione(k + 7) - situazione(k + 2)%>
                     <%=valoreInizialeGruppi(i - 2)%>  <%-- TOT. 16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% valoreInizialeGruppi(i - 2) = valoreInizialeGruppi(i - 2) + situazione(k + 13) + situazione(k + 8) - situazione(k + 3)%>
                     <%=valoreInizialeGruppi(i - 2)%>  <%-- TOT. 20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px"> 
                     <b><%=valoreInizialeGruppi(i - 2)%></b> <%-- TOT. GIORN.--%>
                  </td>
               </tr>
             </table>
            <%End If%>
           </td>
            <%k = k + 15%>
            <td>
             <%If colonna4 Then%>
              <table border="1" cellpadding="0" cellspacing="0">
               <tr>
                  <td width="20px" style="font-size:16px">   
                    <b><%=valoreInizialeGruppi(i - 1)%></b>
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     00/12
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     12/16
                  </td>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                     16/20
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
                    PR.U.
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=situazione(k)%>     <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 1)%>  <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 2)%>  <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 3)%>  <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 4)%>  <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    R.P.
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 5)%>  <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 6)%>  <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 7)%>  <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 8)%>  <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 9)%>  <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white"> 
                    R.C.
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 10)%>  <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 11)%>  <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 12)%>  <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 13)%>  <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=situazione(k + 14)%>  <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    TOT
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 10) + situazione(k + 5) - situazione(k)%>
                     <%=valoreInizialeGruppi(i - 1)%>      <%-- TOT. 08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px"> 
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 11) + situazione(k + 6) - situazione(k + 1)%>
                     <%=valoreInizialeGruppi(i - 1)%>  <%-- TOT. 12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 12) + situazione(k + 7) - situazione(k + 2)%>
                     <%=valoreInizialeGruppi(i - 1)%>  <%-- TOT. 16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% valoreInizialeGruppi(i - 1) = valoreInizialeGruppi(i - 1) + situazione(k + 13) + situazione(k + 8) - situazione(k + 3)%>
                     <%=valoreInizialeGruppi(i - 1)%>  <%-- TOT. 20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <b><%=valoreInizialeGruppi(i - 1)%></b> <%-- TOT. GIORN.--%>
                  </td>
               </tr>
             </table>
            <%End If%>
           </td>
           <% k = k + 15%>
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

