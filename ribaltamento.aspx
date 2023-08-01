<%@ Page Title=""  Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="ribaltamento.aspx.vb" Inherits="ribaltamento" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<%@ Register Src="ribaltamento/ribaltamento.ascx" TagName="ImportXml" TagPrefix="uc1" %>
<%@ Register Src="ribaltamento/ribaltamento.ascx" TagName="ImportWeb" TagPrefix="uc1" %>
<%@ Register Src="ribaltamento/ribaltamento.ascx" TagName="ImportR55" TagPrefix="uc1" %>
<%@ Register Src="ribaltamento/ribaltamento.ascx" TagName="ImportDollar" TagPrefix="uc1" %>
<%@ Register Src="ribaltamento/ribaltamento.ascx" TagName="ImportThrifty" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div runat="server" id="tab_import">
    <ajaxtoolkit:TabContainer ID="tabPanelImport" runat="server" ActiveTabIndex="0" Width="100%">
      <ajaxtoolkit:TabPanel ID="tabImportXml" runat="server" HeaderText="Importa XML">
        <HeaderTemplate>Importazione XML</HeaderTemplate>
        <ContentTemplate>
            <uc1:ImportXml ID="ImportXml" runat="server" />
        </ContentTemplate>
      </ajaxtoolkit:TabPanel>
      <ajaxtoolkit:TabPanel ID="TabImportWeb" runat="server" HeaderText="Importa WEB">
        <HeaderTemplate>Importazione WEB</HeaderTemplate>
        <ContentTemplate>
            <uc1:ImportWeb ID="ImportWeb" runat="server" />
        </ContentTemplate>
      </ajaxtoolkit:TabPanel>
      <ajaxtoolkit:TabPanel ID="TabImportR55" runat="server" HeaderText="Importa 55" Visible="true">
        <HeaderTemplate>Importazione R55</HeaderTemplate>
        <ContentTemplate>
            <uc1:ImportR55 ID="ImportR55" runat="server" />
        </ContentTemplate>
      </ajaxtoolkit:TabPanel>
      <ajaxtoolkit:TabPanel ID="TabImportDollar" runat="server" HeaderText="Importa Dollar">
        <HeaderTemplate>Importazione Dollar</HeaderTemplate>
        <ContentTemplate>
            <uc1:ImportDollar ID="ImportDollar" runat="server" />
        </ContentTemplate>
      </ajaxtoolkit:TabPanel>
      <ajaxtoolkit:TabPanel ID="TabImportThrifty" runat="server" HeaderText="Importa Thrifty">
        <HeaderTemplate>Importazione Thrifty</HeaderTemplate>
        <ContentTemplate>
            <uc1:ImportThrifty ID="ImportThrifty" runat="server" />
        </ContentTemplate>
      </ajaxtoolkit:TabPanel>
    </ajaxtoolkit:TabContainer>
    </div>
</asp:Content>

