<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="autorizzazione.aspx.vb" Inherits="autorizzazione" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<script type="text/javascript" src="js/InputImportoRidotto.js"></script>
  <script type="text/javascript" src="js/InputNumerico.js"></script>--%>
  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  
  <table border="0" cellpadding="0" cellspacing="0" width="100%" runat="server" id="tab_titolo" height="25px">
    <tr>
      <td colspan="8" align="left" style="color: #FFFFFF" bgcolor="#cccc00" >
        <b>&nbsp;Gestione Autorizzazione</b>
      </td>
    </tr>
  </table>

<div id="Pulsanti" runat="server" visible="true">
  <table cellpadding="4" cellspacing="6" width="100%" style="border:4px solid #669999" border="1" runat="server" id="table1">      
  <tr>           
    <td align="center" >
      <asp:Button ID="btnPulsante" runat="server" Text="OFF" Height="80px" 
            Width="160px"  BackColor="Red" />
      <asp:Button ID="Button1" runat="server" Text="Stampa" Height="80px" 
            Width="160px"  BackColor="Red"  
            UseSubmitBehavior="False" />      
    </td>          
  </tr>
  </table>
</div>


</asp:Content>

