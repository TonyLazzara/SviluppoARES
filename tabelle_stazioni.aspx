<%@ Page Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="tabelle_stazioni.aspx.vb" Inherits="tabelle_stazioni" title="" %>
<%@ Register Src="/tabelle_stazioni/stazioni.ascx" TagName="stazioni" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_stazioni/orari_stazioni.ascx" TagName="orari" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_stazioni/proprietari_stazioni.ascx" TagName="proprietari_stazioni" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_stazioni/zone.ascx" TagName="zone" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_stazioni/festivita.ascx" TagName="festivita" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_stazioni/orari_festivita.ascx" TagName="orari_festivita" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_stazioni/distanza_Stazioni.ascx" TagName="distanza_stazioni" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_stazioni/oneri.ascx" TagName="oneri" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel="StyleSheet" type="text/css" href="css/style.css" /> 
    <style type="text/css"> 
   .menu
           {
	        border-style: none;
            border-color: inherit;
            border-width: 0px;
            padding: 0px;
            font-size:11px;
	        font-weight:bold;
            font-style: normal;
            font-variant: normal;
            line-height: normal;
            font-family: "Lucida Sans Unicode", "Bitstream Vera Sans", "Trebuchet Unicode MS", "Lucida Grande", Verdana, Helvetica, sans-serif;
            margin-left: 0px;
            margin-right: 0px;
            margin-top: 0px;
        }
	 
	    .style11
        {
            width: 875px;
        }
	 
	</style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" class="style11">
              <asp:Label ID="Label1" runat="server" Text="Gestione Stazioni" CssClass="testo_titolo"></asp:Label>
              
           </td>
           <td style="color: #FFFFFF;background-color:#444;">
             <img src="punto_elenco.jpg" width="8" height="7" alt="" title="" runat="server" id="puntoTorna" visible="false" />
             &nbsp;<asp:Button ID="btnTorna" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Torna al menu" 
                    Width="118px" visible="false" BackColor="#444" />
           </td>
         </tr>
     </table>
  <table style="color: #FFFFFF" bgcolor="#215A87" width="1024px">
      <tr>
         <td >
            <%--55--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Censimento" title="Distanza/VAL" runat="server" id="puntoDistanza" />&nbsp;<asp:Button 
                   ID="btnDistanzaVAL" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Distanza/VAL" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="118px" />
            <%--52--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Festività" title="Festività" runat="server" id="puntoFestivita" />&nbsp;<asp:Button 
                   ID="btnFestivia" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Festività" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="90px" />
            <%--70--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Oneri" title="Oneri" runat="server" id="puntoOneri" />&nbsp;<asp:Button 
                   ID="btnOneri" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Oneri" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="70px" />
            <%--54--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Orari Festività" title="Orari Festività" runat="server" id="puntoOrariFestivita" />&nbsp;<asp:Button 
                   ID="btnOrariFestività" runat="server" CssClass="menu"
                    Font-Names="Verdana" Height="21px" Text="Orari festività" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="115px" />
            <%--50--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Orari Stazione" title="Orari Stazione"  runat="server" id="puntoOrariStazione" />&nbsp;<asp:Button 
                   ID="btnOrariStazione" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Orari stazione" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="115px" />
            <%--18--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Proprietari stazioni" title="Proprietari stazioni"  runat="server" id="puntoProprietari" />&nbsp;<asp:Button 
                 ID="btnProprietariStazioni" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Proprietari stazioni" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="160px" />
            <%--17--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Stazioni" title="Stazioni" runat="server" id="puntoStazioni" />&nbsp;<asp:Button 
                 ID="btnStazioni" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Stazioni" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="70px" />
             <%--53--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Zone Stazioni" title="Zone" runat="server" id="puntoZone" />&nbsp;<asp:Button 
                 ID="btnZone" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Zone" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="64px" />      
         </td>
      </tr>
  </table>
  <asp:Panel ID="PanelProprietariStazioni" runat="server" Visible="false" Width="100%">
    <uc1:proprietari_stazioni ID="proprietari_stazioni" runat="server" />
  </asp:Panel>
  <asp:Panel ID="PanelStazioni" runat="server" Visible="false" Width="100%">
    <uc1:stazioni ID="stazioni" runat="server" />
  </asp:Panel>
  <asp:Panel ID="PanelOrari" runat="server" Visible="false" Width="100%">  
    <uc1:orari ID="orari" runat="server" />
  </asp:Panel>
  <asp:Panel ID="PanelZone" runat="server" Visible="false" Width="100%">  
    <uc1:zone ID="zone" runat="server" />
  </asp:Panel>
  <asp:Panel ID="PanelFestivita" runat="server" Visible="false" Width="100%">  
    <uc1:festivita ID="festivita" runat="server" />
  </asp:Panel>
  <asp:Panel ID="PanelOrariFestivita" runat="server" Visible="false" Width="100%">  
    <uc1:orari_festivita ID="orari_festivita" runat="server" />
  </asp:Panel>
  <asp:Panel ID="PanelDistanzaVal" runat="server" Visible="false" Width="100%">  
    <uc1:distanza_stazioni ID="distanza_stazioni" runat="server" />
  </asp:Panel>
  <asp:Panel ID="PanelOneri" runat="server" Visible="false" Width="100%">  
    <uc1:oneri ID="oneri" runat="server" />
  </asp:Panel>
      
</asp:Content>

