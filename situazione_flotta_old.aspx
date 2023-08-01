<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="situazione_flotta_old.aspx.vb" Inherits="situazione_flotta_old"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Assembly="BoxOver" Namespace="BoxOver" TagPrefix="boxover" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <style>
        .toolheader {
        background-color:#19191b;
        color:#FFFFFF;
        font-weight:bold;
        width:400px;
        border-top: 1px solid;
	        border-top-color:#C60;
	        border-left: 1px solid;
	        border-left-color:#C60;
	        border-right: 1px solid;
	        border-right-color:#C60;
	        border-bottom: 1px solid;
	        border-bottom-color:#C60;
        }
        .toolbody {
        width:400px;
        background-color:#FFFFFF;
        filter:alpha(opacity=90);
	        -moz-opacity:.90;
	        opacity:.90;
	        border-top: 1px solid;
	        border-top-color:#C60;
	        border-left: 1px solid;
	        border-left-color:#C60;
	        border-right: 1px solid;
	        border-right-color:#C60;
	        border-bottom: 1px solid;
	        border-bottom-color:#C60;
        }
    </style> 
    <style type="text/css">
        .style1
        {
            width: 24px;
        }
        input[type=submit]
        {
          background-color : #369061;
          font-weight:bold;
          color:White;
        }
        .style2
        {
            width: 134px;
        }
        .style3
        {
            width: 87px;
        }
        .style4
        {
            width: 116px;
        }
        .style5
        {
            width: 81px;
        }
        .style6
        {
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td colspan="7" align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Previsione Stazione</b>
           </td>
         </tr>
       </table>
 <table border="0" cellpadding="0" cellspacing="0" width="1024px" style="border:4px solid #444">
         <tr>
           <td align="left" class="style3">  
               <asp:Label ID="lblStazione" runat="server" Text="Stazione:" CssClass="testo_bold"></asp:Label>
           </td>
           <td align="left" class="style2">
               <asp:DropDownList ID="dropStazioni" runat="server" 
                   DataSourceID="sqlStazioni" DataTextField="stazione" DataValueField="id" 
                   style="margin-left: 0px" AppendDataBoundItems="True">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
               </asp:DropDownList>
           </td>
           <td align="left">
               &nbsp;</td>
           <td class="style6">
             <asp:Label ID="lblDaData" runat="server" Text="Da data:" CssClass="testo_bold"></asp:Label>
           </td>
           <td class="style4">
               <asp:TextBox ID="txtDaData" runat="server" Width="74px"></asp:TextBox>
               <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtDaData">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtDaData">
                </asp:MaskedEditExtender>
           
           </td>
           <td class="style5">
             <asp:Label ID="lblAData" runat="server" Text="A data:" CssClass="testo_bold"></asp:Label>
           </td>
           <td>          
               <asp:TextBox ID="txtAData" runat="server" Width="78px"></asp:TextBox>
               <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                    TargetControlID="txtAData">
                </asp:CalendarExtender>
                <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                    TargetControlID="txtAData">
                </asp:MaskedEditExtender>
           
           &nbsp;&nbsp;
               <asp:Button ID="btnVisualizza" runat="server" Text="Visualizza" />
           
           &nbsp;&nbsp;
                          
           &nbsp;&nbsp;
                          
           </td>
         </tr>
 </table>
 
 <br />
  <boxover:BoxOver ID="boxOverNormali" runat="server" body='A' controltovalidate="lblNormale" header="Gruppi Normali" CssHeader="toolheader"  CssBody="toolbody"   />
  <boxover:BoxOver ID="boxOverSpeciali" runat="server" body='A' controltovalidate="lblSpeciale" header="Gruppi Speciali" CssHeader="toolheader"  CssBody="toolbody"   />
 <%
       If dropStazioni.SelectedValue > 0 Then     
    %>
 
 <table border="1" cellpadding="1" cellspacing="1" width="1024px" >
         <tr>
           <td align="left" bgcolor="white" class="style1" >
            
           </td>
           <td align="left" bgcolor="white" >
               <asp:Label ID="lblNormale" runat="server" Text="NORM."></asp:Label>
           </td>
           <td align="left" bgcolor="white" >
             <asp:Label ID="lblSpeciale" runat="server" Text="SPEC."></asp:Label>
           </td>
           <td align="left" bgcolor="white" >
             U
           </td>
           <td align="left" bgcolor="white" >
             G
           </td>
           <td align="left" bgcolor="white" class="style1" >
            
           </td>
         </tr>
         
         <% 
             Dim funzioni As New funzioni_comuni
       Dim dataIniziale As DateTime
       Dim dataFinale As DateTime
             Dim Data1 As DateTime
             Dim testData As DateTime
       Dim fine As Integer
             Dim giornoSuccessivo As Boolean = False
             Dim primaRiga As Boolean = True
             Dim daySucc As Integer = 0
             Dim monthSucc As Integer = 0
             Dim yearSucc As Integer = 0
             
             Dim da_stampa As String
             Dim a_stampa As String
             
             If txtDaData.Text <> "" And txtAData.Text <> "" Then
                 
                 Try
                     testData = CDate(txtDaData.Text)
                     testData = CDate(txtAData.Text)
                     
                     da_stampa = txtDaData.Text
                     a_stampa = txtAData.Text
                 Catch ex As Exception
                     txtDaData.Text = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())
                     txtAData.Text = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())
                     Libreria.genUserMsgBox(Me, "Specificare correttamente data di inizio e fine ricerca")
                 End Try
              
                 If DateDiff(DateInterval.Day, CDate(txtDaData.Text), CDate(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()))) > 0 Then
                     'SE E' STATO SETTATO UN GIORNO PRECEDENTE A OGGI SETTO COME GIORNATA INIZIALE IN GIORNO ATTUALE
                     txtDaData.Text = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())
                     
                     da_stampa = txtDaData.Text
                 ElseIf DateDiff(DateInterval.Day, CDate(txtDaData.Text), CDate(Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()))) < 0 Then
                     'SE E' STATO SETTATO UN GIORNO SUCCESSIVO 
                     giornoSuccessivo = True
                     
                     daySucc = Day(txtDaData.Text)
                     monthSucc = Month(txtDaData.Text)
                     yearSucc = Year(txtDaData.Text)
                     
                     txtDaData.Text = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now())
                 End If
                
                 dataIniziale = txtDaData.Text & " 00:00:00"
                 Data1 = txtDaData.Text & " 00:00:00"
                 fine = DateDiff(DateInterval.Day, CDate(txtDaData.Text), CDate(txtAData.Text)) + 1
           
                 dataFinale = dataIniziale.AddDays(fine - 1)
             Else
                 dataIniziale = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()) & " 00:00:00"
                 Data1 = Day(Now()) & "/" & Month(Now()) & "/" & Year(Now()) & " 00:00:00"
                 fine = DateTime.DaysInMonth(Year(Now()), Month(Now()))
                 dataFinale = dataIniziale.AddDays(fine - 1)
                 
                 da_stampa = dataIniziale
                 a_stampa = dataFinale
             End If
            
             '-----------------------------------------------------------------------------------------------------------------------------
       
       
             Dim i As Integer = 0
             Dim dispGruppo1 As Integer = getDisponibilita("Normale", dropStazioni.SelectedValue)
             Dim dispGruppo2 As Integer = getDisponibilita("Speciale", dropStazioni.SelectedValue)
             Dim dispGruppo3 As Integer = getDisponibilita("U", dropStazioni.SelectedValue)
             Dim dispGruppo4 As Integer = getDisponibilita("G", dropStazioni.SelectedValue)
                
             Do While i < fine
                 Dim tot_pr_u As Integer = 0
                 Dim tot_r_p As Integer = 0
                 Dim tot_r_c As Integer = 0
                 Dim tot_tot As Integer = 0
                 
                 Dim gruppo1(60) As Integer
                 
                 gruppo1 = getGruppo1(Data1, dropStazioni.SelectedValue)

    %>  
      
      <%  If (Not giornoSuccessivo) Or (Day(Data1) = daySucc And Month(Data1) = monthSucc And Year(Data1) = yearSucc) Then
              giornoSuccessivo = False
              Dim stazione_ss As String = dropStazioni.SelectedValue
              Dim data_ss As String = funzioni_comuni.getDataDb_senza_orario(Data1, Request.ServerVariables("HTTP_HOST"))
              %>
         <tr>  
           <td align="left" bgcolor="white" class="style1" valign="top" >  
               <a target="_blank" href='situazione_flotta_gruppi_old.aspx?giorno=<%= Day(Data1) & "/" & Month(Data1) & "/" & Year(Data1) %>&staz=<%=dropStazioni.SelectedValue %>'><%=Day(Data1)%></a>        
               <br />
           </td>
           <td align="left" >
             <table border="1" cellpadding="0" cellspacing="0">
               <tr>
                  <td width="20px" style="font-size:16px">   
                     <%--<% If primaRiga Then%> --%>
                      <b><%=dispGruppo1%></b>
                     <%--<% End If%>--%>
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
                      <asp:Label ID="Label1" runat="server" Text="PR.U." ToolTip="Previste Uscite"></asp:Label>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(1)%> <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(2)%> <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(3)%> <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(4)%> <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(5)%> <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    <asp:Label ID="Label2" runat="server" Text="R.P." ToolTip="Rientri Previsti (prenotazioni e DDT)"></asp:Label>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(6)%> <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(7)%> <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(8)%> <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(9)%> <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(10)%> <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white"> 
                    <asp:Label ID="Label3" runat="server" Text="R.C." ToolTip="Rientri da Contratto"></asp:Label>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(11)%> <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(12)%> <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(13)%> <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(14)%> <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(15)%> <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    TOT
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% dispGruppo1 = dispGruppo1 + gruppo1(6) + gruppo1(11) - gruppo1(1)%>
                     <%=dispGruppo1%> <%-- TOT. 08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% dispGruppo1 = dispGruppo1 + gruppo1(7) + gruppo1(12) - gruppo1(2)%>
                     <%=dispGruppo1%> <%-- TOT. 12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% dispGruppo1 = dispGruppo1 + gruppo1(8) + gruppo1(13) - gruppo1(3)%>
                     <%=dispGruppo1%> <%-- TOT. 16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% dispGruppo1 = dispGruppo1 + gruppo1(9) + gruppo1(14) - gruppo1(4)%>
                     <%=dispGruppo1%> <%-- TOT. 20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <b><%--<%=dispGruppo1%>--%></b> <%-- TOT. GIORN.--%>
                  </td>
               </tr>
             </table>
           </td>
           <td align="left" >
             <table border="1" cellpadding="0" cellspacing="0">
               <tr>
                  <td width="20px" style="font-size:16px">
                     <%--<% If primaRiga Then%>--%>
                      <b><%=dispGruppo2%></b>
                     <%--<% End If%>--%>
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
                    <asp:Label ID="Label4" runat="server" Text="PR.U." ToolTip="Previste Uscite"></asp:Label>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(16)%> <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(17)%> <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(18)%> <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(19)%> <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(20)%> <%--TOT--%>
                  </td>
               </tr> 
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    <asp:Label ID="Label7" runat="server" Text="R.P." ToolTip="Rientri Previsti (prenotazioni e DDT)"></asp:Label>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(21)%> <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(22)%> <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(23)%> <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(24)%> <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(25)%> <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    <asp:Label ID="Label10" runat="server" Text="R.C." ToolTip="Rientri da Contratto"></asp:Label>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(26)%> <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(27)%> <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(28)%> <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(29)%> <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(30)%> <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    TOT
                  </td>
                  <td width="20px" style="font-size:16px">
                      <% dispGruppo2 = dispGruppo2 + gruppo1(21) + gruppo1(26) - gruppo1(16)%>
                      <%=dispGruppo2%> <%-- TOT. 08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% dispGruppo2 = dispGruppo2 + gruppo1(22) + gruppo1(27) - gruppo1(17)%>
                     <%=dispGruppo2%> <%-- TOT. 12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% dispGruppo2 = dispGruppo2 + gruppo1(23) + gruppo1(28) - gruppo1(18)%>
                     <%=dispGruppo2%> <%-- TOT. 16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% dispGruppo2 = dispGruppo2 + gruppo1(24) + gruppo1(29) - gruppo1(19)%>
                     <%=dispGruppo2%> <%-- TOT. 20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <b><%--<%=dispGruppo2%>--%></b> <%-- TOT. GIORN.--%>
                  </td>
               </tr>
             </table>
           </td>
           <td align="left" >
             <table border="1" cellpadding="0" cellspacing="0">
               <tr>
                  <td width="20px" style="font-size:16px">
                     <%--<% If primaRiga Then%>--%>
                      <b><%=dispGruppo3%></b>
                     <%--<% End If%>--%>
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
                    <asp:Label ID="Label5" runat="server" Text="PR.U." ToolTip="Previste Uscite"></asp:Label>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(31)%> <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(32)%> <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(33)%> <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(34)%> <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(35)%> <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    <asp:Label ID="Label8" runat="server" Text="R.P." ToolTip="Rientri Previsti (prenotazioni e DDT)"></asp:Label>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(36)%> <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(37)%> <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(38)%> <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(39)%> <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(40)%> <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    <asp:Label ID="Label11" runat="server" Text="R.C." ToolTip="Rientri da Contratto"></asp:Label>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(41)%> <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(42)%> <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(43)%> <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(44)%> <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(45)%> <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    TOT
                  </td>
                  <td width="20px" style="font-size:16px">
                      <% dispGruppo3 = dispGruppo3 + gruppo1(36) + gruppo1(41) - gruppo1(31)%>
                      <%=dispGruppo3%> <%-- TOT. 08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% dispGruppo3 = dispGruppo3 + gruppo1(37) + gruppo1(42) - gruppo1(32)%>
                     <%=dispGruppo3%> <%-- TOT. 12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% dispGruppo3 = dispGruppo3 + gruppo1(38) + gruppo1(43) - gruppo1(33)%>
                     <%=dispGruppo3%> <%-- TOT. 16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% dispGruppo3 = dispGruppo3 + gruppo1(39) + gruppo1(44) - gruppo1(34)%>
                     <%=dispGruppo3%> <%-- TOT. 20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <b><%--<%=dispGruppo3%>--%></b> <%-- TOT. GIORN.--%>
                  </td>
               </tr>
             </table>
           </td>
           <td align="left" >
             <table border="1" cellpadding="0" cellspacing="0">
               <tr>
                  <td width="20px" style="font-size:16px">
                     <%  If primaRiga Then
                             primaRiga = False
                         End If
                     %>
                      <b><%=dispGruppo4%></b>
                     
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
                    <asp:Label ID="Label6" runat="server" Text="PR.U." ToolTip="Previste Uscite"></asp:Label>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(46)%> <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(47)%> <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(48)%> <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(49)%> <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(50)%> <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    <asp:Label ID="Label9" runat="server" Text="R.P." ToolTip="Rientri Previsti (prenotazioni e DDT)"></asp:Label>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(51)%> <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(52)%> <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(53)%> <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(54)%> <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(55)%> <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    <asp:Label ID="Label12" runat="server" Text="R.C." ToolTip="Rientri da Contratto"></asp:Label>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(56)%> <%--08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(57)%> <%--12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <%=gruppo1(58)%> <%--16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(59)%> <%--20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                    <%=gruppo1(60)%> <%--TOT--%>
                  </td>
               </tr>
               <tr>
                  <td width="20px" style="font-size:16px" bgcolor="white">
                    TOT
                  </td>
                  <td width="20px" style="font-size:16px">
                      <% dispGruppo4 = dispGruppo4 + gruppo1(51) + gruppo1(56) - gruppo1(46)%>
                      <%=dispGruppo4%> <%-- TOT. 08/12--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% dispGruppo4 = dispGruppo4 + gruppo1(52) + gruppo1(57) - gruppo1(47)%>
                     <%=dispGruppo4%> <%-- TOT. 12/16--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% dispGruppo4 = dispGruppo4 + gruppo1(53) + gruppo1(58) - gruppo1(48)%>
                     <%=dispGruppo4%> <%-- TOT. 16/20--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <% dispGruppo4 = dispGruppo4 + gruppo1(54) + gruppo1(59) - gruppo1(49)%>
                     <%=dispGruppo4%> <%-- TOT. 20/23--%>
                  </td>
                  <td width="20px" style="font-size:16px">
                     <b><%--<%=dispGruppo4%>--%></b> <%-- TOT. GIORN.--%>
                  </td>
               </tr>
             </table>
           </td>
           <td align="left" class="style1" >
                <table border="1" cellpadding="0" cellspacing="0">
                  <tr>
                    <td width="40px" style="font-size:16px" bgcolor="white" align="center">
                      <b>Tot</b>
                    </td>
                  </tr>
                  <tr>
                    <td width="20px" style="font-size:16px" align="center">
                      <%=gruppo1(5) + gruppo1(20) + gruppo1(35) + gruppo1(50)%> <%-- TOT. PREVISTE USCITE--%>
                    </td>
                  </tr>
                  <tr>
                    <td width="20px" style="font-size:16px" align="center">
                       <%=gruppo1(10) + gruppo1(25) + gruppo1(40) + gruppo1(55)%> <%-- TOT. RIENTRI PREVISTI--%>
                    </td>
                  </tr>
                  <tr>
                    <td width="20px" style="font-size:16px" align="center">
                      <%=gruppo1(15) + gruppo1(30) + gruppo1(45) + gruppo1(60)%> <%-- TOT. RIENTRI DA CONTRATTO--%>
                    </td>
                  </tr>
                  <tr>
                    <td width="20px" style="font-size:16px" align="center">
                      <%=dispGruppo1 + dispGruppo2 + dispGruppo3 + dispGruppo4%> <%-- TOT. RIENTRI DA CONTRATTO--%>
                    </td>
                  </tr>
                </table>
           </td>
         </tr>
    
      <% 
      Else
          'NEL CASO IN CUI SIA STATO SELEZIONATO UN GIORNO SUCCESSIVO AD OGGI ESEGUO IL CALCOLO dal giorno attuale fino al giorno selezionato        
          'PER IL CALCOLO DELLA DISPONIBILITA'
 
          dispGruppo1 = dispGruppo1 + gruppo1(6) + gruppo1(11) - gruppo1(1)
          dispGruppo1 = dispGruppo1 + gruppo1(7) + gruppo1(12) - gruppo1(2)
          dispGruppo1 = dispGruppo1 + gruppo1(8) + gruppo1(13) - gruppo1(3)
          dispGruppo1 = dispGruppo1 + gruppo1(9) + gruppo1(14) - gruppo1(4)
          
          dispGruppo2 = dispGruppo2 + gruppo1(21) + gruppo1(26) - gruppo1(16)
          dispGruppo2 = dispGruppo2 + gruppo1(22) + gruppo1(27) - gruppo1(17)
          dispGruppo2 = dispGruppo2 + gruppo1(23) + gruppo1(28) - gruppo1(18)
          dispGruppo2 = dispGruppo2 + gruppo1(24) + gruppo1(29) - gruppo1(19)     
             
          dispGruppo3 = dispGruppo3 + gruppo1(36) + gruppo1(41) - gruppo1(31)
          dispGruppo3 = dispGruppo3 + gruppo1(37) + gruppo1(42) - gruppo1(32)
          dispGruppo3 = dispGruppo3 + gruppo1(38) + gruppo1(43) - gruppo1(33)
          dispGruppo3 = dispGruppo3 + gruppo1(39) + gruppo1(44) - gruppo1(34)
          
          dispGruppo4 = dispGruppo4 + gruppo1(51) + gruppo1(56) - gruppo1(46)
          dispGruppo4 = dispGruppo4 + gruppo1(52) + gruppo1(57) - gruppo1(47)
          dispGruppo4 = dispGruppo4 + gruppo1(53) + gruppo1(58) - gruppo1(48)
          dispGruppo4 = dispGruppo4 + gruppo1(54) + gruppo1(59) - gruppo1(49)
          
          
      End If 'IF Not giornoSuccessivo Then
      Data1 = Data1.AddDays(1)
      i = i + 1
  Loop
    
      %>     
 </table>
  <center><a target="_blank" href="/stampe/stampa_situazione_flotta.aspx?staz=<%= dropStazioni.SelectedValue %>&da=<%= CStr(da_stampa).Replace(" 00:00:00", "") %>&a=<%= CStr(a_stampa).Replace(" 00:00:00", "") %>">Stampa Excel</a></center>
 
 <%
 End If
    %>
     
     
     <asp:SqlDataSource ID="sqlStazioni" runat="server" 
             ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
             SelectCommand="SELECT ( STR(codice) + ' - ' + nome_stazione) As stazione, [id] FROM [stazioni] ORDER BY codice"></asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlGruppi" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id_gruppo, cod_gruppo FROM gruppi ORDER BY cod_gruppo"></asp:SqlDataSource>
</asp:Content>

