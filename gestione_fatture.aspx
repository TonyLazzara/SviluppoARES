<%@ Page Title=""  Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="gestione_fatture.aspx.vb" Inherits="gestione_fatture" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<%--
<%@ Register Src="fatture/fatture.ascx" TagName="fatture_Prenotazione" TagPrefix="uc1" %>--%>
<%@ Register Src="fatture/fatture.ascx" TagName="fatture_Noleggio" TagPrefix="uc1" %>
<%--<%@ Register Src="fatture/fatture.ascx" TagName="fatture_RDS" TagPrefix="uc1" %>--%>
<%@ Register Src="fatture/fatture.ascx" TagName="fatture_Multe" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div runat="server" id="tab_import">
    <ajaxtoolkit:TabContainer ID="tabPanelFatture" runat="server" ActiveTabIndex="3" 
            Width="100%">
      <%--<ajaxtoolkit:TabPanel ID="tabFatturePrenotazioni" runat="server" HeaderText="Fatture Prenotazioni">
        <HeaderTemplate>Fatture Prenotazioni</HeaderTemplate>
        <ContentTemplate>
            <uc1:fatture_Prenotazione ID="fatture_Prenotazione" runat="server" />
        </ContentTemplate>
      </ajaxtoolkit:TabPanel>--%>

      <ajaxtoolkit:TabPanel ID="TabFattureNoleggio" runat="server" HeaderText="Fatture Prenotazioni" Visible="false">
        <HeaderTemplate>Fatture Noleggio</HeaderTemplate>
        <ContentTemplate>
            <uc1:fatture_Noleggio ID="fatture_Noleggio" runat="server" >
                
            </uc1:fatture_Noleggio>
        </ContentTemplate>
      </ajaxtoolkit:TabPanel>
<%--
      <ajaxtoolkit:TabPanel ID="TabFattureRDS" runat="server" HeaderText="Fatture Prenotazioni">
        <HeaderTemplate>Fatture RDS</HeaderTemplate>
        <ContentTemplate>
            <uc1:fatture_RDS ID="fatture_RDS" runat="server" >
                
            </uc1:fatture_RDS>
        </ContentTemplate>
      </ajaxtoolkit:TabPanel>--%>

      <ajaxtoolkit:TabPanel ID="TabFattureMulte" runat="server" HeaderText="Fatture Prenotazioni">
        <HeaderTemplate>Fatture Multe</HeaderTemplate>
        <ContentTemplate>
            <uc1:fatture_Multe ID="fatture_Multe" runat="server" />
        </ContentTemplate>
      </ajaxtoolkit:TabPanel>
      
    </ajaxtoolkit:TabContainer>
    </div>
</asp:Content>

