<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="parcoVeicoli.aspx.vb" Inherits="parcoVeicoli2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<%@ Register Src="parco_veicoli/auto_dati_generali.ascx" TagName="dati_generali" TagPrefix="uc1" %>
<%@ Register Src="parco_veicoli/auto_accessori.ascx" TagName="auto_accessori" TagPrefix="uc1" %>
<%@ Register Src="parco_veicoli/dati_acquisto.ascx" TagName="auto_dati_acquisto" TagPrefix="uc1" %>
<%@ Register Src="parco_veicoli/auto_assicurazioni.ascx" TagName="auto_assicurazioni" TagPrefix="uc1" %>
<%@ Register Src="parco_veicoli/auto_leasing.ascx" TagName="auto_leasing" TagPrefix="uc1" %>
<%@ Register Src="parco_veicoli/auto_manutenzione.ascx" TagName="auto_manutenzione" TagPrefix="uc1" %>
<%@ Register Src="parco_veicoli/auto_dati_vendita.ascx" TagName="dati_vendita" TagPrefix="uc1" %>
<%@ Register Src="parco_veicoli/documenti.ascx" TagName="documenti" TagPrefix="uc1" %>

<%--<%@ Register Src="parco_veicoli/auto_movimenti.ascx" TagName="auto_movimento" TagPrefix="uc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <meta http-equiv="Expires" content="0" />
  <meta http-equiv="Cache-Control" content="no-cache" />
  <meta http-equiv="Pragma" content="no-cache" />
  
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<ajaxtoolkit:TabContainer ID="tabPanel1" runat="server" ActiveTabIndex="0" 
        Width="100%">
      <ajaxtoolkit:TabPanel ID="tabDatiGenerali" runat="server" HeaderText="Dati Generali"><HeaderTemplate>
            Dati Generali   
</HeaderTemplate>
<ContentTemplate>
           <uc1:dati_generali ID="dati_generali" runat="server" />


</ContentTemplate> 


</ajaxtoolkit:TabPanel>


<ajaxtoolkit:TabPanel ID="tabAccessori" runat="server" HeaderText="Accessori"><HeaderTemplate>
            Accessori     
</HeaderTemplate>
<ContentTemplate>
           <uc1:auto_accessori ID="auto_accessori" runat="server" />
</ContentTemplate>    
</ajaxtoolkit:TabPanel>

<ajaxtoolkit:TabPanel ID="tabAcquisto" runat="server" HeaderText="Accessori"><HeaderTemplate>
            Dati acquisto
</HeaderTemplate>
<ContentTemplate>
           <uc1:auto_dati_acquisto ID="auto_dati_acquisto" runat="server" >
               
           </uc1:auto_dati_acquisto>
</ContentTemplate>    








</ajaxtoolkit:TabPanel>

<ajaxtoolkit:TabPanel ID="tabVendita" runat="server" HeaderText="Dati vendita"><HeaderTemplate>
            Dati vendita
</HeaderTemplate>
<ContentTemplate>
           <uc1:dati_vendita ID="dati_vendita" runat="server" />
</ContentTemplate>    





</ajaxtoolkit:TabPanel>

<ajaxtoolkit:TabPanel ID="tabAssicurazioni" runat="server" HeaderText="Assicurazione"><HeaderTemplate>
            Assicurazione
</HeaderTemplate>
<ContentTemplate>
           <uc1:auto_assicurazioni ID="auto_assicurazioni" runat="server" />
</ContentTemplate>    






</ajaxtoolkit:TabPanel>

<ajaxtoolkit:TabPanel ID="tabLeasing" runat="server" HeaderText="Leasing e Lungo termine"><HeaderTemplate>
            Leasing e Lungo termine
</HeaderTemplate>
<ContentTemplate>
           <uc1:auto_leasing ID="auto_leasing" runat="server" >
               
           </uc1:auto_leasing>
</ContentTemplate>    




</ajaxtoolkit:TabPanel>

<ajaxtoolkit:TabPanel ID="tabManutenzione" runat="server" HeaderText="Manutenzione"><HeaderTemplate>
            Manutenzione
</HeaderTemplate>
<ContentTemplate>
           <uc1:auto_manutenzione ID="auto_manutenzione" runat="server" />

</ContentTemplate>    

</ajaxtoolkit:TabPanel>

<ajaxtoolkit:TabPanel ID="tabDocumenti" runat="server" HeaderText="Documenti"><HeaderTemplate>
            Documenti
</HeaderTemplate>
<ContentTemplate>
           <uc1:documenti ID="documenti" runat="server" />
</ContentTemplate>    



</ajaxtoolkit:TabPanel>

<%--<ajaxtoolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Manutenzione"><HeaderTemplate>
            Movimento Targa
</HeaderTemplate>
<ContentTemplate>
           <uc1:auto_movimento ID="auto_movimento" runat="server" />
</ContentTemplate>    


</ajaxtoolkit:TabPanel>--%>
     
   </ajaxtoolkit:TabContainer>
</asp:Content>
    
    



