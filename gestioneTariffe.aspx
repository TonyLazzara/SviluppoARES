<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="gestioneTariffe.aspx.vb" Inherits="gestioneTariffe" title="Pagina senza titolo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<%@ Register Src="tariffe/tempo_km.ascx" TagName="Tempo_Km" TagPrefix="uc1" %>
<%@ Register Src="tariffe/condizioni.ascx" TagName="Condizioni" TagPrefix="uc1" %>
<%@ Register Src="tariffe/tariffe.ascx" TagName="tariffe" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxtoolkit:ToolkitScriptManager>
    
    <ajaxtoolkit:TabContainer ID="tabGestTariffe" runat="server" ActiveTabIndex="0" Width="100%">
        <!-- Tab Tempo-Km -->
        <ajaxtoolkit:TabPanel ID="tabDatiTempoKm" runat="server" HeaderText="Dati Tempo-Km">
            <HeaderTemplate>
                Tempo-Km
            
            
        
            
            
        
        
            
            
        
            
            
        
        
        
            
            
        
            
            
        
        
            
            
        
            
            
        
        
        
        
            
            
        
            
            
        
        
            
            
        
            
            
        
        
        
            
            
        
            
            
        
        
            
            
        
            
            
        
        
        
        
        
            
            
        
            
            
        
        
            
            
        
            
            
        
        
        
            
            
        
            
            
        
        
            
            
        
            
            
        
        
        
        
            
            
        
            
            
        
        
            
            
        
            
            
        
        
        
            
            
        
            
            
        
        
            
            
        
            
            
        
        
        
        
        
        
            
            
        
            
            
        
        
            
            
        
            
            
        
        
        
            
            
        
            
            
        
        
            
            
        
            
            
        
        
        
        
            
            
        
            
            
        
        
            
            
        
            
            
        
        
        
            
            
        
            
            
        
        
            
            
        
            
            
        
        
        
        
        
            
            
        
            
            
        
        
            
            
        
            
            
        
        
        
            
            
        
            
            
        
        
            
            
        
            
            
        
        
        
        
            
            
        
            
            
        
        
            
            
        
            
            
        
        
        
            
            
        
            
            
        
        
            
            
        
            
            
        
        
        
        
        
        
</HeaderTemplate>            
            
            








<ContentTemplate>
                <uc1:Tempo_Km ID="Tempo_Km" runat="server" />


            

            
        

            

            
        
        

            

            
        

            

            
        
        
</ContentTemplate> 
            
        
        








</ajaxtoolkit:TabPanel>
        
        <!-- Tab Condizioni -->
        <ajaxtoolkit:TabPanel ID="tabDatiCondizioni" runat="server" HeaderText="Condizioni">
            <HeaderTemplate>
                Condizioni
        
        
</HeaderTemplate>
<ContentTemplate>
                <uc1:condizioni ID="condizioni" runat="server" />

</ContentTemplate>    



</ajaxtoolkit:TabPanel>

<!-- Tab Condizioni -->
        <ajaxtoolkit:TabPanel ID="tabDatiTariffe" runat="server" HeaderText="Tariffe">
            <HeaderTemplate>
                Tariffe
        
        
</HeaderTemplate>   



<ContentTemplate>
                <uc1:tariffe ID="tariffe1" runat="server" />
        
        
</ContentTemplate>   
        


</ajaxtoolkit:TabPanel>
    </ajaxtoolkit:TabContainer>
</asp:Content>

