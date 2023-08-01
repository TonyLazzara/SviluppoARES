<%@ Page Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="tabelle.aspx.vb" Inherits="tabelleAuto" title="" %>
<%@ Register Src="/tabelle_auto/accessori.ascx" TagName="accessori" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_auto/alimentazione.ascx" TagName="alimentazione" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_auto/enti_finanziatori.ascx" TagName="enti_finanziatori" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_auto/gruppi_auto.ascx" TagName="gruppi_auto" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_auto/marche.ascx" TagName="marche" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_auto/proprietari.ascx" TagName="proprietari" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_auto/modelli.ascx" TagName="modelli" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_auto/bolle.ascx" TagName="bolle" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_auto/compagnie_assicurative.ascx" TagName="compagnie_assicurative" TagPrefix="uc1" %>
<%--<%@ Register Src="/tabelle_auto/ammortamenti.ascx" TagName="ammortamenti" TagPrefix="uc1" %>--%>
<%@ Register Src="/tabelle_auto/venditori.ascx" TagName="venditori" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_auto/tipo_riparazioni.ascx" TagName="tipo_riparazioni" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_auto/acquirenti_veicoli.ascx" TagName="acquirenti_veicoli" TagPrefix="uc1" %>
<%--<%@ Register Src="/tabelle_auto/tipo_acquirente.ascx" TagName="tipo_acquirente" TagPrefix="uc1" %>--%>
<%@ Register Src="/tabelle_auto/colori.ascx" TagName="colori" TagPrefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
   
    <style type="text/css">   
       tr{
            font-family:Verdana, Geneva, Tahoma, sans-serif;
            font-size: 12px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="color: #FFFFFF" bgcolor="#215A87" width="1024px">
         <tr>
           <td >
               <%--10--%>
                 <img src="punto_elenco.jpg" width="8" height="7"  alt="Accessori" title="Accessori" />&nbsp;<asp:Button ID="Accessori" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Accessori" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="76px" />
                 <%--35--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Acquirenti" title="Acquirenti" />&nbsp;<asp:Button ID="btnAcquirenti" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Acquirenti" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="81px" />
                <%--11--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Alimentazione" title="Alimentazione" />&nbsp;<asp:Button ID="btnAlimentazione" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Alimentazione" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="107px" />
              <%--23--%>
                 <%-- <img src="punto_elenco.jpg" width="8" height="7" alt="Ammortamenti" title="Ammortamenti" />&nbsp;<asp:Button ID="btnAmmortamenti" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Ammortamenti" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="118px" />     --%>          
                <%--22--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Bolle" title="Bolle" />&nbsp;<asp:Button 
                   ID="btnBolle" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Tipo Bolle" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="81px" />
                <%--37--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Colori" title="Colori" />&nbsp;<asp:Button ID="btnColori" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Colori" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="53px" />
                <%--19--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Compagnie assicurative" title="Compagnie assicurative" />&nbsp;<asp:Button ID="btnCompagnieAssicurative" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Compagnie assicurative" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="181px" />
                 <%--12--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Enti finanziatori" title="Enti finanziatori" />&nbsp;<asp:Button ID="EntiFinanziatori" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Enti finanziatori" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="121px" />
                <%--13--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Gruppi auto" title="Gruppi auto" />&nbsp;<asp:Button ID="GruppiAuto" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Gruppi auto" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="91px" />   
                <%--14--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Marche" title="Marche" />&nbsp;<asp:Button ID="btnMarche" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Marche" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="60px" />                     
            </td>
         </tr>
         <tr>
           <td >   
               <%--16--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Modelli" title="Modelli" />&nbsp;<asp:Button ID="btnModelli" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Modelli" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="63px" />
               <%--15--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Proprietari veicoli" title="Proprietari veicoli" />&nbsp;<asp:Button ID="btnProprietari" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Proprietari veicoli" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="133px" />
                <%--29--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Riparazioni" title="Riparazioni" />&nbsp;<asp:Button ID="btniparazioni" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Riparazioni" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="87px" />          
               <%--36--%>
                <%--<img src="punto_elenco.jpg" width="8" height="7" alt="Tipo Acquirente" title="Tipo Acquirente" />&nbsp;<asp:Button ID="btnTipoAcquirente" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Tipo Acquirente" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="122px" />--%>                  
               <%--28--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Venditori" title="Venditori" />&nbsp;<asp:Button ID="btnVenditori" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Venditori" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="68px" />                                                                                                    
           </td>
         </tr>
</table>
<asp:Panel ID="PanelAccessori" runat="server" Visible="false" Width="100%">
  <uc1:accessori ID="auto_accessori" runat="server" />
</asp:Panel>
<asp:Panel ID="PanelAlimentazione" runat="server" Visible="false" Width="100%">
  <uc1:alimentazione ID="auto_alimentazione" runat="server" />
</asp:Panel>
<asp:Panel ID="PanelEnti" runat="server" Visible="false" Width="100%">
  <uc1:enti_finanziatori ID="enti_finanziatori" runat="server" />
</asp:Panel>
<asp:Panel ID="PanelGruppi" runat="server" Visible="false" Width="100%">
  <uc1:gruppi_auto ID="gruppi_auto" runat="server" />
</asp:Panel>
<asp:Panel ID="PanelMarche" runat="server" Visible="false" Width="100%">
  <uc1:marche ID="marche" runat="server" />
</asp:Panel>
<asp:Panel ID="PanelProprietari" runat="server" Visible="false" Width="100%">
  <uc1:proprietari ID="proprietari" runat="server" />
</asp:Panel>
<asp:Panel ID="PanelModelli" runat="server" Visible="false" Width="100%">
  <uc1:modelli ID="modelli" runat="server" />
</asp:Panel>
<asp:Panel ID="PanelCompagnieAssicurative" runat="server" Visible="false" Width="100%">  
  <uc1:compagnie_assicurative ID="compagnie_assicurative" runat="server" />
</asp:Panel>
<asp:Panel ID="PanelBolle" runat="server" Visible="false" Width="100%">  
  <uc1:bolle ID="bolle" runat="server" />
</asp:Panel>
<%--<asp:Panel ID="PanelAmmortaemtni" runat="server" Visible="false" Width="100%">  
  <uc1:ammortamenti ID="ammortamenti" runat="server" />
</asp:Panel>--%>
<asp:Panel ID="PanelVenditori" runat="server" Visible="false" Width="100%">  
  <uc1:venditori ID="venditori" runat="server" />
</asp:Panel>
<asp:Panel ID="PanelRiparazioni" runat="server" Visible="false" Width="100%">  
  <uc1:tipo_riparazioni ID="tipo_riparazioni" runat="server" />
</asp:Panel>
<asp:Panel ID="PanelAcquirenti" runat="server" Visible="false" Width="100%">  
  <uc1:acquirenti_veicoli ID="acquirenti_veicoli" runat="server" />
</asp:Panel>
<%--<asp:Panel ID="PanelTipoAcquirente" runat="server" Visible="false" Width="100%">  
  <uc1:tipo_acquirente ID="tipo_acquirente" runat="server" />
</asp:Panel>--%>
<asp:Panel ID="PanelColori" runat="server" Visible="false" Width="100%">  
  <uc1:colori ID="colori" runat="server" />
</asp:Panel>

</asp:Content>

