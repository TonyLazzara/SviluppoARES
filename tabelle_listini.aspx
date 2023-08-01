<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="tabelle_listini.aspx.vb" Inherits="tabelle_listini" %>
<%@ Register Src="/tabelle_listini/elementi_condizioni.ascx" TagName="condizioni" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_listini/elementi_macrovoci.ascx" TagName="macrovoci" TagPrefix="uc1" %>  <%--aggiunto 06.08.2022 salvo--%>
<%--<%@ Register Src="/tabelle_listini/a_carico_di.ascx" TagName="carico" TagPrefix="uc1" %>--%>
<%@ Register Src="/tabelle_listini/elementi_fuori_orario.ascx" TagName="fuori_orario" TagPrefix="uc1" %>
<%--<%@ Register Src="/tabelle_listini/unita_di_misura.ascx" TagName="unita" TagPrefix="uc1" %>--%>
<%--<%@ Register Src="/tabelle_listini/tipo_tariffa.ascx" TagName="tipo_tariffa" TagPrefix="uc1" %>--%>
<%@ Register Src="/tabelle_listini/percentuale.ascx" TagName="percentuale" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_listini/tipo_cliente.ascx" TagName="tipo_cliente" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_listini/carburante.ascx" TagName="carburante" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_listini/modifica_targa.ascx" TagName="modifica_targa" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_listini/commissioni.ascx" TagName="commissioni" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_listini/giorni_da_preautorizzare.ascx" TagName="giorni_da_preautorizzare" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_listini/sconto_su_franchigia.ascx" TagName="sconto_su_franchigia" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_listini/spese_spedizione_fattura.ascx" TagName="spese_spedizione_fattura" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_listini/addebito_accessori_persi.ascx" TagName="addebito_accessori_persi" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_listini/val_gps.ascx" TagName="val_gps" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_listini/ra_void.ascx" TagName="ra_void" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_listini/stampa_fattura.ascx" TagName="stampa_fattura" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_listini/arrotondamento_prepagato.ascx" TagName="arrotondamento_prepagato" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_listini/franchigie_parziali.ascx" TagName="franchigie_parziali" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel="StyleSheet" type="text/css" href="css/style.css" /> 
    <style type="text/css"> 
   .menu
           {
	        border:none;
	        border:0px;
	        margin:0px;
	        padding:0px;
	        font: 67.5% "Lucida Sans Unicode", "Bitstream Vera Sans", "Trebuchet Unicode MS", "Lucida Grande", "Verdana", "Helvetica", "sans-serif";
	        font-size:11px;
	        font-weight:bold;
	        }
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
                <%--120--%> 
                <img src="punto_elenco.jpg" width="8" height="7" alt="Addebito accessori persi" title="Addebito accessori persi" runat="server" id="puntoAddebitoAccessoriPersi" />  
                <asp:Button ID="btnAddebitoAccessoriPersi" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Addebito Accessori Persi" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="193px" />

                     <%--149--%> 
                 <img src="punto_elenco.jpg" width="8" height="7" alt="Arrotondamento prepagato" title="Arrotondamento prepagato" runat="server" id="ArrotondamentoPrepagato" />  
                <asp:Button ID="arrotondamento_prepagato" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Arrot. su prepagato" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="150px" />  

                <%--91--%> 
                <img src="punto_elenco.jpg" width="8" height="7" alt="Carburante" title="Carburante" runat="server" id="puntoCarburante" />  
                <asp:Button ID="btnCarburante" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Carburante" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="87px" />
                <%--40--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Condizioni" title="Condizioni" runat="server" id="puntoCondizioni" />
                <asp:Button ID="Condizioni" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Elementi condizioni" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="149px" />

                <img src="punto_elenco.jpg" width="8" height="7" alt="Condizioni" title="Macro Voci" runat="server" id="puntoMacrovoci" />
                <asp:Button ID="btn_macrovoci" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Elementi Macro Voci" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="149px" />


                <%--41--%>
<%--                <img src="punto_elenco.jpg" width="8" height="7" alt="A carico di" title="A carico di" runat="server" id="puntoACaricoDi" />
                <asp:Button ID="a_carico_di" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Condizione a carico di" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="172px" />--%>
                 <%--40--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Commissioni" title="Commissioni" runat="server" id="puntoCommissioni" />
                <asp:Button ID="btn_commissioni" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Commissioni" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="100px" />
                
           </td>
         </tr>
         <tr>
           <td >
                <%--78--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Consegna fuori orario" title="Consegna fuori orario" runat="server" id="puntoFuoriOrario" />
                <asp:Button ID="FuoriOrario" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Consegna fuori orario" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="172px" />

                <%--96--%> 
                <img src="punto_elenco.jpg" width="8" height="7" alt="Giorni da preautorizzare" title="Giorni da preautorizzare" runat="server" id="puntoGiorniDaPreautorizzare" />  
                <asp:Button ID="btnGiorniDaPreautorizzare" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Giorni da preautorizzare" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="191px" /> 
                <%--147--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Minuti Ra Void" title="Minuti Ra Void" runat="server" id="puntoRaVoid" />
                <asp:Button ID="btnRaVoid" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Minuti RA Void" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="130px" />
                <%--78--%>
                <img src="punto_elenco.jpg" width="8" height="7" alt="Modifica Targa" title="Modifica Targa" runat="server" id="puntoModificaTarga" />
                <asp:Button ID="modificaTarga" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Minuti Modifica Targa" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="170px" />
                <%--68--%> 
                <img src="punto_elenco.jpg" width="8" height="7" alt="Percentuale" title="Percentuale" runat="server" id="puntoPercentuale" />  
                <asp:Button ID="percentuale" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Percentuale" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="96px" />
                 
                 
               
                
                     
               
                  <%--48
                <img src="punto_elenco.jpg" width="8" height="7" alt="Tipo tariffa" title="Tipo tariffa" runat="server" id="puntoTipoTariffa" />  
                <asp:Button ID="tipo_tariffa" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Tipo tariffa" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="87px" />
                  43
                <img src="punto_elenco.jpg" width="8" height="7" alt="Unità misura" title="Unità misura" runat="server" id="puntoUnitaMisura" />
                <asp:Button ID="Unita_misura" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Unità di misura condizione" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="211px" />--%>
                    
                 
           </td>
         </tr>
         <tr>
           <td >
           <%--150--%> 
                <img src="punto_elenco.jpg" width="8" height="7" alt="Franchigie Parziali" title="Franchigie Parziali" runat="server" id="punto_franchigie_parziali" />  
                <asp:Button ID="franchigie_parziali" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Franchigie Parziali" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="160px" />  

            <%--100--%> 
                <img src="punto_elenco.jpg" width="8" height="7" alt="Sconto su franchigia" title="Sconto su franchigia" runat="server" id="puntoScontoSuFranchigia" />  
                <asp:Button ID="btnScontoFranchigia" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Sconto su franchigia" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="160px" />  

                <%--115--%> 
                 <img src="punto_elenco.jpg" width="8" height="7" alt="Spese di spedizione fattura" title="Spese di spedizione fattura" runat="server" id="puntoSpeseSpedizioneFattura" />  
                <asp:Button ID="spese_spedizione_fattura" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Spese Sped. Fattura" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="156px" /> 


                <img src="punto_elenco.jpg" width="8" height="7" alt="Stampa Fattura" title="Stampa Fattura" runat="server" id="puntoStampaFattura" />  
                <asp:Button ID="btnStampaFattura" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Stampa Fattura" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="122px" />
                 <%--94--%> 
                <img src="punto_elenco.jpg" width="8" height="7" alt="Tipo cliente (Fonte)" title="Tipo cliente (Fonte)" runat="server" id="puntoTipoCliente" />  
                <asp:Button ID="tipo_cliente" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Tipo cliente" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="94px" />
                    
                <img src="punto_elenco.jpg" width="8" height="7" alt="Val Gps" title="Tipo cliente (Fonte)" runat="server" id="puntoValGps" />
                    <asp:Button ID="btn_val_gps" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Val GPS" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="78px" />  

                    
                    
                 
           </td>
         </tr>
     </table>
   
     <asp:Panel ID="PanelCondizioni" runat="server" Visible="false" Width="100%">
        <uc1:condizioni ID="listini_condizioni" runat="server" />
     </asp:Panel>

     <asp:Panel ID="PanelMacrovoci" runat="server" Visible="false" Width="100%">    <%--aggiunto salvo 06.08.2022--%>
        <uc1:macrovoci ID="listini_macrovoci" runat="server" />
     </asp:Panel>

     
    <%-- <asp:Panel ID="PanelACaricoDi" runat="server" Visible="false" Width="100%">
        <uc1:carico ID="listini_carico" runat="server" />
     </asp:Panel>--%>
     
     <asp:Panel ID="PanelFuoriOrario" runat="server" Visible="false" Width="100%">
        <uc1:fuori_orario ID="listini_fuori_orario" runat="server" />
     </asp:Panel>
     
     <%--<asp:Panel ID="PanelUnitaDiMisura" runat="server" Visible="false" Width="100%">
        <uc1:unita ID="listini_unita_di_misura" runat="server" />
     </asp:Panel>--%>
     
     <%--<asp:Panel ID="PanelTipoTariffa" runat="server" Visible="false" Width="100%">
        <uc1:tipo_tariffa ID="listini_tipo_tariffa" runat="server" />
     </asp:Panel>--%>

      <asp:Panel ID="PanelCommissioni" runat="server" Visible="false" Width="100%">
        <uc1:commissioni ID="listini_commissioni" runat="server" />
     </asp:Panel>
     
     <asp:Panel ID="PanelPercentuale" runat="server" Visible="false" Width="100%">
        <uc1:percentuale ID="listini_percentuale" runat="server" />
     </asp:Panel>
     
     <asp:Panel ID="PanelTipoClienti" runat="server" Visible="false" Width="100%">
        <uc1:tipo_cliente ID="listini_tipo_cliente" runat="server" />
     </asp:Panel>
     
     <asp:Panel ID="PanelCarburante" runat="server" Visible="false" Width="100%">
        <uc1:carburante ID="listini_carburante" runat="server" />
     </asp:Panel>
     <asp:Panel ID="PanelModificaTarga" runat="server" Visible="false" Width="100%">
        <uc1:modifica_targa ID="listini_targa" runat="server" />
     </asp:Panel>
     <asp:Panel ID="PanelGiorniDaPreautorizzare" runat="server" Visible="false" Width="100%">
        <uc1:giorni_da_preautorizzare ID="listini_giorni_da_preautorizzare" runat="server" />
     </asp:Panel>
     <asp:Panel ID="PanelScontoFranchigia" runat="server" Visible="false" Width="100%">
        <uc1:sconto_su_franchigia ID="listini_sconto_su_franchigia" runat="server" />
     </asp:Panel>
     <asp:Panel ID="PanelFranchigieParziali" runat="server" Visible="false" Width="100%">
        <uc1:franchigie_parziali ID="listini_franchigie_parziali" runat="server" />
     </asp:Panel>
     <asp:Panel ID="PanelSpeseSpedizioneFattura" runat="server" Visible="false" Width="100%">
        <uc1:spese_spedizione_fattura ID="listini_spese_spedizione_fattura" runat="server" />
     </asp:Panel>
     <asp:Panel ID="PanelAddebitoAccessoriPersi" runat="server" Visible="false" Width="100%">
        <uc1:addebito_accessori_persi ID="listini_addebito_accessori_persi" runat="server" />
     </asp:Panel>

     <asp:Panel ID="PanelValGps" runat="server" Visible="false" Width="100%">
        <uc1:val_gps ID="listini_val_gps" runat="server" />
     </asp:Panel>
     <asp:Panel ID="PanelRaVoid" runat="server" Visible="false" Width="100%">
        <uc1:ra_void ID="listini_ra_void" runat="server" />
     </asp:Panel>
     <asp:Panel ID="PanelStampaFattura" runat="server" Visible="false" Width="100%">
        <uc1:stampa_fattura ID="listini_stampa_fattura" runat="server" />
     </asp:Panel>
     <asp:Panel ID="PanelArrotondamentoPrepagato" runat="server" Visible="false" Width="100%">
        <uc1:arrotondamento_prepagato ID="listini_arrotondamento_prepagato" runat="server" />
     </asp:Panel>
</asp:Content>

