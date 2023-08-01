<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="gestione_ditte.aspx.vb" Inherits="gestione_ditte" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<%@ Register Src="/anagrafica/anagrafica_ditte.ascx" TagName="anagrafica_ditte" TagPrefix="uc1" %>

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
    <uc1:anagrafica_ditte ID="anagrafica_ditte" runat="server" />
</asp:Content>
    



