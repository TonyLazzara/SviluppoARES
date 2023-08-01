<%@ Page Language="VB" MasterPageFile="MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="tabelle_danni.aspx.vb" Inherits="tabelle_danni" title="" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<%@ Register Src="gestione_danni/tipo_origine_danno.ascx" TagName="origine_danno" TagPrefix="uc1" %>
<%@ Register Src="gestione_danni/posizione_danno.ascx" TagName="posizione_danno" TagPrefix="uc1" %>
<%@ Register Src="gestione_danni/tipo_danno.ascx" TagName="tipo_danno" TagPrefix="uc1" %>
<%@ Register Src="gestione_danni/tipo_documento_danno.ascx" TagName="tipo_documento_danno" TagPrefix="uc1" %>
<%@ Register Src="gestione_danni/tipo_documento_danno.ascx" TagName="tipo_documento_evento_danno" TagPrefix="uc1" %>
<%@ Register Src="gestione_danni/gestione_mappe.ascx" TagName="gestione_mappe" TagPrefix="uc1" %>
<%@ Register Src="gestione_danni/tipo_attesa_manutenzione.ascx" TagName="tipo_attesa_manutenzione" TagPrefix="uc1" %>
<%@ Register Src="gestione_danni/tipo_motivo_non_addebito.ascx" TagName="tipo_motivo_non_addebito" TagPrefix="uc1" %>
<%@ Register Src="gestione_danni/sinistro_gestito_da.ascx" TagName="sinistro_gestito_da" TagPrefix="uc1" %>
<%@ Register Src="gestione_danni/sinistro_tipologia.ascx" TagName="sinistro_tipologia" TagPrefix="uc1" %>
<%@ Register Src="gestione_fornitori/anagrafica_fornitori.ascx" TagName="anagrafica_fornitori" TagPrefix="uc1" %>
<%@ Register Src="gestione_fornitori/anagrafica_drivers.ascx" TagName="anagrafica_drivers" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td align="left" style="color: #FFFFFF; width: 875px;" bgcolor="#444">
              <asp:Label ID="Label1" runat="server" Text="Tabelle Danni" CssClass="testo_titolo"></asp:Label>
              
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
         <td>
            <%--PermessiUtente.GestioneOrigineDanno--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Gestione Origine Danno" title="Gestione Origine Danno" runat="server" id="puntoGestioneOrigineDanno" />
                <asp:Button ID="btnGestioneOrigineDanno" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Origine Danno" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="110px" />
            <%--PermessiUtente.GestionePosizioneDanno--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Gestione Posizione Danno" title="Gestione Posizione Danno" runat="server" id="puntoGestionePosizioneDanno" />
                <asp:Button ID="btnGestionePosizioneDanno" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Posizione Danno" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="120px" />
            <%--PermessiUtente.GestioneTipoDanno--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Gestione Tipo Danno" title="Gestione Tipo Danno" runat="server" id="puntoGestioneTipoDanno" />
                <asp:Button ID="btnGestioneTipoDanno" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Tipo Danno" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="90px" />
            <%--PermessiUtente.GestioneTipoDocumentoDanno--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Gestione Tipo Documento Danno" title="Gestione Tipo Documento Danno" runat="server" id="puntoGestioneTipoDocumentoDanno" />
                <asp:Button ID="btnGestioneTipoDocumentoDanno" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Tipo Documento Danno" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="174px" />
            <%--PermessiUtente.GestioneTabelleRDS--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Gestione Tipo Attesa Ufficio Manutenzione" title="Gestione Tipo Attesa Ufficio Manutenzione" runat="server" id="puntoGestioneTipoAttesaUfficioManutenzione" />
                <asp:Button ID="btnGestioneTipoAttesaUfficioManutenzione" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" 
                 Text="Tipo Attesa Ufficio Manutenzione" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="245px" />
           
         </td>
      </tr>
      <tr>
         <td >
            <span style="white-space: nowrap;">
                <%--PermessiUtente.GestioneTabelleRDS--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Gestione Motivo Non Addebito Danno" title="Gestione Motivo Non Addebito Danno" runat="server" id="puntoGestioneMotivoNonAddebitoDanno" />
                <asp:Button ID="btnGestioneMotivoNonAddebitoDanno" runat="server" CssClass="menu" Font-Names="Verdana" Height="21px" Text="Motivo Non Addebito Danno" BackColor="#215A87" Font-Size="Small" ForeColor="White" Width="210px" />
                <img src="punto_elenco.jpg" width="8" height="7" alt="Gestione Tipo Documento Evento Danno" title="Gestione Tipo Documento Evento Danno" runat="server" id="puntoGestioneTipoDocumentoEventoDanno" /><asp:Button ID="btnGestioneTipoDocumentoEventoDanno" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Tipo Documento Evento Danno" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="230px" />
                <img src="punto_elenco.jpg" width="8" height="7" alt="Gestione Mappatura Modelli" title="Gestione Mappatura Modelli" runat="server" id="puntoGestioneMappaturaModelli" /><asp:Button 
                 ID="btnGestioneMappaturaModelli" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Mappatura Modelli" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="146px" />
                <img src="punto_elenco.jpg" width="8" height="7" alt="Sinistro Gestito Da" title="Sinistro: Gestito Da" runat="server" id="puntoGestitoDa" /><asp:Button 
                 ID="btnSinistroGestitoDa" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Sinistro: Gestito Da" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="154px" />
                <img src="punto_elenco.jpg" width="8" height="7" alt="Sinistro Tipologia" title="Sinistro: Tipologia" runat="server" id="puntoTIpologia" /><asp:Button 
                 ID="btnSinistroTipologia" runat="server" CssClass="menu"  Font-Names="Verdana" Height="21px" Text="Sinistro: Tipologia" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="146px" />
                   
            </span>
                    
         </td>
      </tr>
      <tr>
         <td >
            <span style="white-space: nowrap;">
                <%--PermessiUtente.GestioneFornitori coincide con GestioneODL --%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Gestione Fornitori" title="Gestione Fornitori" runat="server" id="puntoGestioneFornitori" />
                <asp:Button ID="btnGestioneFornitori" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Gestione Fornitori" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="140px" /> 
            </span>
            <span style="white-space: nowrap;">
                <%--PermessiUtente.GestioneFornitori coincide con GestioneODL --%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Gestione Drivers" title="Gestione Drivers" runat="server" id="puntoGestioneDrivers" />
                <asp:Button ID="btnGestioneDrivers" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Gestione Drivers" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="130px" /> 
            </span>
         </td>
      </tr>
  </table>
    <asp:Panel ID="PanelGestioneOrigineDanno" runat="server" Width="100%" Visible="false">
        <uc1:origine_danno ID="origine_danno" runat="server" />        
    </asp:Panel>
    <asp:Panel ID="PanelGestionePosizioneDanno" runat="server" Visible="False">
        <uc1:posizione_danno ID="posizione_danno" runat="server"  />        
    </asp:Panel>
    <asp:Panel ID="PanelGestioneTipoDanno" runat="server" Visible="False">
        <uc1:tipo_danno ID="tipo_danno" runat="server"  />        
    </asp:Panel>
    <asp:Panel ID="PanelGestioneTipoDocumentoDanno" runat="server" Visible="False">
        <uc1:tipo_documento_danno ID="tipo_documento_danno" runat="server"  />        
    </asp:Panel>
    <asp:Panel ID="PanelGestioneTipoAttesaUfficioManutenzione" runat="server" Visible="False">
        <uc1:tipo_attesa_manutenzione ID="tipo_attesa_manutenzione" runat="server"  />        
    </asp:Panel>
    <asp:Panel ID="PanelGestioneMotivoNonAddebitoDanno" runat="server" Visible="False">
        <uc1:tipo_motivo_non_addebito ID="tipo_motivo_non_addebito" runat="server"  />        
    </asp:Panel>
    <asp:Panel ID="PanelGestioneTipoDocumentoEventoDanno" runat="server" Visible="False">
        <uc1:tipo_documento_evento_danno ID="tipo_documento_evento_danno" runat="server"  />        
    </asp:Panel>
    <asp:Panel ID="PanelGestioneMappaturaModelli" runat="server" Visible="False">
        <uc1:gestione_mappe id="gestione_mappe" runat="server" />       
    </asp:Panel>
    <asp:Panel ID="PanelSinistriGestitoDa" runat="server" Visible="False">
        <uc1:sinistro_gestito_da id="gestito_da" runat="server" />       
    </asp:Panel>
    
    <asp:Panel ID="PanelSinistriTipologia" runat="server" Visible="False">
        <uc1:sinistro_tipologia id="tipologia" runat="server" />       
    </asp:Panel>

    <div id="PanelGestioneFornitori" runat="server" Visible="False">
        <uc1:anagrafica_fornitori id="anagrafica_fornitori" runat="server" />
    </div>
    <div id="PanelGestioneDrivers" runat="server" Visible="False">
        <uc1:anagrafica_drivers id="anagrafica_drivers" runat="server" />
    </div>
</asp:Content>

