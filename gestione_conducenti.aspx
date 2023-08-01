<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="gestione_conducenti.aspx.vb" Inherits="gestione_conducenti" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<%@ Register Src="/anagrafica/anagrafica_conducenti.ascx" TagName="anagrafica_conducenti" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <meta http-equiv="Expires" content="0" />
  <meta http-equiv="Cache-Control" content="no-cache" />
  <meta http-equiv="Pragma" content="no-cache" />
  <style type="text/css">
        .style1
        {        	  
            width: 146px;
        }
        input[type=submit]
        {
          background-color : #369061;
          font-weight:bold;
          color:White;
        }
        .style2
      {
          width: 87px;
      }


        </style>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:anagrafica_conducenti ID="anagrafica_conducenti" runat="server" />
</asp:Content>
    
    



