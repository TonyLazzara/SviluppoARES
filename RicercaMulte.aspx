<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="RicercaMulte.aspx.vb" Inherits="RicercaMulte" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="gestione_Multe/RicercaMulte.ascx" TagName="RicercaMulte" TagPrefix="uc1" %>
<%@ Register Src="gestione_Multe/RiepilogoIncassi.ascx" TagName="RiepilogoIncassi" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <meta http-equiv="Expires" content="0" />
  <meta http-equiv="Cache-Control" content="no-cache" />
  <meta http-equiv="Pragma" content="no-cache" />
  <style type="text/css">
        .style1
        {        	  
            width: 146px;
        }
        input[type=submit]
        {
          background-color : #215A87;
          font-weight:bold;
          color:White;
        }
        .style2
      {
          width: 87px;
      }
        .button 
        {
        border:none;
        border:0px;
        margin:0px;
        padding:0px;
        background: #215A87;
		}
        </style>  
    <script type="text/javascript" src="gestione_multe/InputImportoRidotto.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <table style="color: #FFFFFF" bgcolor="#215A87" width="1024px">
     <tr>
       <td align="right" style="width:2%">
           <%--10--%>
           <img src="punto_elenco.jpg" width="8" height="7"  alt="Tabelle" title="Tabelle" />
       </td>
       <td align="left" style="width:15%">
             <asp:Button ID="btnTabelle" runat="server" CssClass="button"  
                Font-Names="Verdana" Height="21px" Text="Tabelle" 
                Font-Size="Small" ForeColor="White" Width="70px" BackColor="#215A87" />
       </td>
       <td align="right" style="width:2%">
           <%--20--%>
           <img src="punto_elenco.jpg" width="8" height="7"  alt="RiepilogoIncassi" title="RiepilogoIncassi" />
       </td>
       <td align="left" style="width:25%">
             <asp:Button ID="btnRiepilogoInc" runat="server"  CssClass="button"  
                Font-Names="Verdana" Height="21px" Text="Riepilogo Incassi del giorno" 
                Font-Size="Small" ForeColor="White" Width="200px" BackColor="#215A87" />
       </td>
       <td align="right" style="width:2%">
           <%--30--%>
             <!--<img src="punto_elenco.jpg" width="8" height="7"  alt="MulteLocatori" title="MulteLocatori" />-->
       </td>
       <td align="left" style="width:5%">
             <asp:Button ID="btnMulteLocatori" runat="server"  CssClass="button"  
                Font-Names="Verdana" Height="21px" Text="Multe altri locatori" 
                Font-Size="Small" ForeColor="White" Width="150px" BackColor="#215A87" 
                 Visible="False" />
       </td>
       <td style="width:77%"> &nbsp;
       </td>
      </tr>
    </table> 
    <br />
    <div runat="server" id="VisualRicercaMulte" visible="true">
        <uc1:RicercaMulte ID="RicercaMulte" runat="server" />
    </div>
    <div runat="server" id="VisulRiepilogoInc" visible="false">
        <uc1:RiepilogoIncassi ID="RiepilogoIncassi1" runat="server" />
    </div>    
</asp:Content>

