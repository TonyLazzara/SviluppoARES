<%@ Page Title=""  Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="gestione_cassa.aspx.vb" Inherits="gestione_cassa" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<%@ Register Src="~/cassa/gestione_cassa.ascx" TagName="gestione_cassa" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />   

    <script type="text/javascript">
        function MultipleSubmissions(ele, e, nrco) {
            var nomeele = ele.value;

            //se nrco=0 chiede conferma
            if (nrco == 1) {
                if (confirm('Stai per cancellare la riga ' + nomeele + '?') == false) {
                    return false;
                }
            }
        }
     </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
    <uc1:gestione_cassa id="gestione_cassa" runat="server" />
</asp:Content>

