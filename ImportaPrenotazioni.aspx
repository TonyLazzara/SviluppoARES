<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ImportaPrenotazioni.aspx.vb" Inherits="ImportaPrenotazioni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" cellpadding="1" cellspacing="1" width="100%" >
         <tr>
           <td  align="left" style="color: #FFFFFF" bgcolor="#369061">
             <b>Importa Prenotazioni</b>
           </td>
         </tr>
</table>
<center>
<br />
<asp:Button ID="btnImportaWeb" runat="server" Enabled="false" Text="Importa Web" CssClass="pulsante" Width=150 />
<br /><br />
<asp:Button ID="btnImportaXml" runat="server" Enabled="false" Text="Importa Xml"  CssClass="pulsante" Width=150  /> 
<br /><br />
<asp:Button ID="btnImporta55" runat="server" Enabled="false" Text="Importa 53"  CssClass="pulsante" Width=150  />
<br /><br />
<asp:Button ID="btnImporta57" runat="server" Enabled="false" Text="Importa 54"  CssClass="pulsante" Width=150  />
<br /><br />
</center>
</asp:Content>

