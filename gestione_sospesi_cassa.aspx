<%@ Page Title=""  Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="gestione_sospesi_cassa.aspx.vb" Inherits="gestione_sospesi_cassa" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<%@ Register Src="~/cassa/gestione_sospesi_petty_cash.ascx" TagName="gestione_sospesi_petty_cash" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
    <uc1:gestione_sospesi_petty_cash id="gestione_sospesi_petty_cash" runat="server" />
</asp:Content>

