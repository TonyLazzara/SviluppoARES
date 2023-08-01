<%@ Page Language="VB" MasterPageFile="MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="tabelle_posNew.aspx.vb" Inherits="tabelle_posNew" title="" %>
<%@ Register Src="tabelle_pos/censimento_pos.ascx" TagName="censimento_pos_uc" TagPrefix="uc1" %>
<%@ Register Src="tabelle_pos/enti_proprietari.ascx" TagName="enti_proprietari" TagPrefix="uc1" %>
<%@ Register Src="tabelle_pos/Circuiti.ascx" TagName="Circuiti" TagPrefix="uc1" %>
<%@ Register Src="tabelle_pos/Acquires.ascx" TagName="Acquires" TagPrefix="uc1" %>
<%@ Register Src="tabelle_pos/TipologieErrore.ascx" TagName="TipologieErrore" TagPrefix="uc1" %>
<%@ Register Src="tabelle_pos/ActionCodes.ascx" TagName="ActionCodes" TagPrefix="uc1" %>
<%@ Register Src="tabelle_pos/Funzionalita.ascx" TagName="Funzionalita" TagPrefix="uc1" %>
<%@ Register Src="cassa/anagrafica_enti_telefonici.ascx" TagName="anagrafica_enti_telefonici" TagPrefix="uc1" %>
<%@ Register Src="cassa/anagrafica_esercenti_telefonici.ascx" TagName="anagrafica_esercenti_telefonici" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
         tr{
            font-family:Verdana, Geneva, Tahoma, sans-serif;
            font-size: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td align="left" style="color: #FFFFFF; width: 875px;" bgcolor="#444">
              <asp:Label ID="Label1" runat="server" Text="Gestione POS" CssClass="testo_titolo"></asp:Label>
              
           </td>
           <td style="color: #FFFFFF;background-color:#444;">
             <img src="punto_elenco.jpg" width="8" height="7" alt="" title="" runat="server" id="puntoTorna" visible="false" />
             &nbsp;<asp:Button ID="btnTorna" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Torna al menu" 
                    visible="false" BackColor="#444" />
           </td>
         </tr>
     </table>
<table style="color: #FFFFFF" bgcolor="#215A87" width="1024px">
      <tr>
         <td >
            <%--59--%>
                <span style="white-space: nowrap;">
                <img src="punto_elenco.jpg" width="8" height="7" alt="Censimento POS" title="Censimento POS" runat="server" id="puntoCensimentoPOS" />&nbsp;<asp:Button 
                   ID="btnCensimentoPOS" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Censimento POS" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White"  />
                </span>
            <%--63--%>
                <span style="white-space: nowrap;">
                <img src="punto_elenco.jpg" width="8" height="7" alt="Enti Proprietari" title="Enti Proprietari" runat="server" id="puntoCensimentoEntiProprietari" />
                <asp:Button ID="btnCensimentoEntiProprietari" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Enti Proprietari" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
                </span>
            <%--64--%>
                <span style="white-space: nowrap;">
                <img src="punto_elenco.jpg" width="8" height="7" alt="Censimento Circuiti" title="Censimento Circuiti" runat="server" id="puntoCensimentoCircuiti" />
                <asp:Button ID="btnCensimentoCircuiti" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Circuiti" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
                </span>            
            <%--69--%>
                <span style="white-space: nowrap;">
                <img src="punto_elenco.jpg" width="8" height="7" alt="Funzionalità" title="Funzionalità" runat="server" id="puntoFunzionalità" />
                <asp:Button ID="btnFunzionalita" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Funzionalità" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
                </span>
            <%--59--%>
                <span style="white-space: nowrap;">
                <img src="\images\punto_elenco.jpg" width="8" height="7" alt="Censimento POS" title="Censimento POS" runat="server" id="Img1" />&nbsp;<asp:Button 
                   ID="btnCensimentoPOS2" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Stazione --> POS" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White"  />
                </span> 
            <%--146--%>
                <span style="white-space: nowrap;">
                <img src="punto_elenco.jpg" width="8" height="7" alt="Enti Transazione Telefonica" title="Enti Transazione Telefonica" runat="server" id="puntoEntiTransazioneTelefono" />
                <asp:Button ID="btnEntiTransazioneTelefono" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Enti Transazione Telefonica" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
                </span>
            <%--146--%>
                <span style="white-space: nowrap;">
                <img src="punto_elenco.jpg" width="8" height="7" alt="Anagrafica Codici Esercenti Telefonici" title="Anagrafica Codici Esercenti Telefonici" runat="server" id="punto_anagrafica_esercenti_telefonici" />
                <asp:Button ID="btn_anagrafica_esercenti_telefonici" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Anagrafica Codici Esercenti Telefonici" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" />
                </span>
                
         </td>
      </tr>
  </table>
    <asp:Panel ID="PanelCensimentoPOS" runat="server" Width="100%" Visible="false">
        <uc1:censimento_pos_uc ID="censimento_pos1" runat="server" />        
    </asp:Panel>
    
    <asp:Panel ID="PanelCensimentoEntiProprietari" runat="server" Visible="False">
        <uc1:enti_proprietari ID="enti_proprietari1" runat="server"  />        
    </asp:Panel>
    <asp:Panel ID="PanelCensimentoCircuiti" runat="server" Visible="False">
        <uc1:Circuiti ID="Circuiti1" runat="server"  />        
    </asp:Panel>
    <asp:Panel ID="PanelCensimentoAcquires" runat="server" Visible="False">
        <uc1:Acquires ID="Acquires1" runat="server"  />        
    </asp:Panel>
    <asp:Panel ID="PanelCensimentoTipologieErrori" runat="server" Visible="False">
        <uc1:TipologieErrore ID="TipologieErrore1" runat="server"  />        
    </asp:Panel>
    <asp:Panel ID="PanelCensimentoActionCode" runat="server" Visible="False">
        <uc1:ActionCodes ID="ActionCodes1" runat="server"  />        
    </asp:Panel>
    <asp:Panel ID="PanelFunzionalita" runat="server" Visible="False">
        <uc1:Funzionalita ID="Funzionalita1" runat="server"  />
    </asp:Panel>
    <asp:Panel ID="PanelEntiTransazioneTelefono" runat="server" Visible="False">
        <uc1:anagrafica_enti_telefonici ID="anagrafica_enti_telefonici" runat="server"  />
    </asp:Panel>
    <asp:Panel ID="Panel_anagrafica_esercenti_telefonici" runat="server" Visible="False">
        <uc1:anagrafica_esercenti_telefonici id="anagrafica_esercenti_telefonici" runat="server" />
    </asp:Panel>
    
</asp:Content>

