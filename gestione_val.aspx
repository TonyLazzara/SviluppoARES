<%@ Page Language="VB" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" CodeFile="gestione_val.aspx.vb" Inherits="tabelle_val" title="" %>
<%@ Register Src="/gestione_val/accessorio_val.ascx" TagName="accessorio_val" TagPrefix="uc1" %>
<%@ Register Src="/gestione_val/template_val.ascx" TagName="template_val" TagPrefix="uc1" %>
<%@ Register Src="/gestione_val/gruppi_stazioni.ascx" TagName="gruppi_stazioni" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel="StyleSheet" type="text/css" href="/css/style.css" /> 
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
              <asp:Label ID="Label1" runat="server" Text="Gestione VAL" CssClass="testo_titolo"></asp:Label>
              
           </td>
           <td style="color: #FFFFFF;background-color:#444;">
             <img src="punto_elenco.jpg" width="8" height="7" alt="" title="" runat="server" id="puntoTorna" visible="false" />
             &nbsp;<asp:Button ID="btnTorna" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Torna al menu" 
                    Width="118px" visible="false" BackColor="#444" />
           </td>
         </tr>
     </table>
  <div id="divmenu" runat="server">
  <table style="color: #FFFFFF" bgcolor="#215A87" width="1024px">
      <tr>
         <td >
            <%--55--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Accessorio VAL" title="Accessorio VAL" runat="server" id="puntoAccessorioVAL" />&nbsp;<asp:Button 
                   ID="btnAccessorioVAL" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Accessorio VAL" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="118px" />
            <%--52--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Template VAL" title="Template VAL" runat="server" id="puntoTemplateVAL" />&nbsp;<asp:Button 
                   ID="btnTemplateVAL" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Template VAL" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="107px" />
            <%--77--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Template Gruppi VAL" title="Template Gruppi VAL" runat="server" id="puntoTemplateGruppiVAL" />&nbsp;<asp:Button 
                   ID="btnTemplateGruppiVAL" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Template Gruppi VAL" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="155px" />
            <%--54--%>
                &nbsp;<%--50--%>
                &nbsp;<%--18--%>
                &nbsp;<%--17--%>
                &nbsp;<%--53--%>
                &nbsp;</td>
      </tr>
  </table>
  </div>
  <asp:Panel ID="PanelAccessorioVAL" runat="server" Visible="false" Width="100%">
    <uc1:accessorio_val ID="accessorio_val" runat="server" />
  </asp:Panel>
  <asp:Panel ID="PanelTemplateVAL" runat="server" Visible="false" Width="100%">
    <uc1:template_val ID="template_val" runat="server" />
  </asp:Panel>
  <asp:Panel ID="PanelGruppiStazioni" runat="server" Visible="false" Width="100%">
    <uc1:gruppi_stazioni ID="gruppi_stazioni" runat="server" />
  </asp:Panel>
</asp:Content>

