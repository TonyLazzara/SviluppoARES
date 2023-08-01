<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="tabelle_multe.aspx.vb" Inherits="tabelle_multe" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/gestione_multe/ArticoliCDS.ascx" TagName="ArticoliCDS" TagPrefix="uc1" %>
<%@ Register Src="~/gestione_multe/Enti.ascx" TagName="Enti" TagPrefix="uc1" %>
<%@ Register Src="~/gestione_multe/Provenienza.ascx" TagName="Provenienza" TagPrefix="uc1" %>
<%@ Register Src="~/gestione_multe/TipoAllegato.ascx" TagName="TipoAllegato" TagPrefix="uc1" %>
<%@ Register Src="~/gestione_multe/ModelloRicorsi.ascx" TagName="ModelloRicorsi" TagPrefix="uc1" %>
<%@ Register Src="~/gestione_multe/Casistiche.ascx" TagName="Casistiche" TagPrefix="uc1" %>
<%@ Register Src="~/gestione_multe/CausaliFattura.ascx" TagName="CausaliFattura" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <style type="text/css">
        .button 
        {
        border:none;
        border:0px;
        margin:0px;
        padding:0px;
        background: #215A87;
        text-align:left;
		}
        .button:hover {background: #444;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="color: #FFFFFF" bgcolor="#215A87" width="1024px">
     <tr>
       <td >
           <%--10--%>
             <img src="punto_elenco.jpg" width="8" height="7"  alt="ArticoliCDS" title="ArticoliCDS" />
             &nbsp;
             <asp:Button ID="btnArticoliCDS" runat="server"  CssClass="button"  
                Font-Names="Verdana" Height="21px" Text="ArticoliCDS" 
                Font-Size="Small" ForeColor="White" Width="100px" BackColor="#215A87" />
       </td>
       <td align="left" >
           <%--20--%>
             <img src="punto_elenco.jpg" width="8" height="7"  alt="Ente" title="Ente" />
             &nbsp;
             <asp:Button ID="btnEnti" runat="server" CssClass="button"  
                Font-Names="Verdana" Height="21px" Text="Enti" 
                Font-Size="Small" ForeColor="White" Width="100px" BackColor="#215A87" />
       </td>
       <td >
           <%--30--%>
             <img src="punto_elenco.jpg" width="8" height="7"  alt="Provenienza" title="Provenienza" />
             &nbsp;
             <asp:Button ID="btnProvenienza" runat="server" CssClass="button"  
                Font-Names="Verdana" Height="21px" Text="Provenienza" 
                Font-Size="Small" ForeColor="White" Width="150px" BackColor="#215A87" />
       </td>
       <td >
           <%--40--%>
             <img src="punto_elenco.jpg" width="8" height="7"  alt="TipoAllegato" title="TipoAllegato" />
             &nbsp;
             <asp:Button ID="btnTipoAllegato" runat="server" CssClass="button"  
                Font-Names="Verdana" Height="21px" Text="Tipo allegato" 
                Font-Size="Small" ForeColor="White" Width="150px" BackColor="#215A87" />
       </td>
       <td >
           <%--50--%>
             <img src="punto_elenco.jpg" width="8" height="7"  alt="ModelloRicorso" title="ModelloRicorso" />
             &nbsp;
             <asp:Button ID="btnModelloRicorso" runat="server" CssClass="button"  
                Font-Names="Verdana" Height="21px" Text="Modelli Documenti" 
                Font-Size="Small" ForeColor="White" Width="150px" BackColor="#215A87" />
       </td>
       <td >
           <%--60--%>
             <img src="punto_elenco.jpg" width="8" height="7"  alt="Casistiche" title="Casistiche" />
             &nbsp;
             <asp:Button ID="btnCasistiche" runat="server" CssClass="button"  
                Font-Names="Verdana" Height="21px" Text="Casistiche" 
                Font-Size="Small" ForeColor="White" Width="150px" BackColor="#215A87" />
       </td>
     </tr>
    </table>
    <table style="color: #FFFFFF" bgcolor="#215A87" width="1024px">
       <tr>
       <td style="width:20%">
           <%--70--%>
             <img src="punto_elenco.jpg" width="8" height="7"  alt="Causali" title="Causali" />
             &nbsp;
             <asp:Button ID="btnCausaliFattura" runat="server" CssClass="button"  
                Font-Names="Verdana" Height="21px" Text="Causali Fattura" 
                Font-Size="Small" ForeColor="White" Width="150px" BackColor="#215A87" />
       </td>
       <td style="width:20%">
           <%--80--%>
             <img src="punto_elenco.jpg" width="8" height="7"  alt="Chiudi" title="Chiudi" />
             &nbsp;
             <asp:Button ID="btnChiudi" runat="server" CssClass="button"  
                Font-Names="Verdana" Height="21px" Text="Chiudi" 
                Font-Size="Small" ForeColor="White" Width="150px" BackColor="#215A87" />
       </td>
       <td style="width:60%">
        &nbsp;
       </td>
     </tr>
    </table> 
    <br />
    <br />
    <asp:Panel ID="panelArticoliCDS" runat="server" Visible="true" Width="100%">
        <uc1:ArticoliCDS ID="ArticoliCDS1" runat="server" />
    </asp:Panel>
    <asp:Panel ID="panelEnti" runat="server" Visible="true" Width="100%">
        <uc1:Enti ID="Enti1" runat="server" />
    </asp:Panel>
    <asp:Panel ID="panelProvenienza" runat="server" Visible="true" Width="100%">
        <uc1:Provenienza ID="Provenienza1" runat="server" />
    </asp:Panel>
    <asp:Panel ID="panelTipoAllegato" runat="server" Visible="true" Width="100%">
        <uc1:TipoAllegato ID="TipoAllegato1" runat="server" />
    </asp:Panel>
    <asp:Panel ID="panelModelloRicorsi" runat="server" Visible="true" Width="100%">
        <uc1:ModelloRicorsi ID="ModelloRicorsi1" runat="server" />
    </asp:Panel>
    <asp:Panel ID="panelCasistiche" runat="server" Visible="true" Width="100%">
        <uc1:Casistiche ID="Casistiche1" runat="server" />
    </asp:Panel>
    <asp:Panel ID="panelCausaliFattura" runat="server" Visible="true" Width="100%">
        <uc1:CausaliFattura ID="CausaliFattura1" runat="server" />
    </asp:Panel>
</asp:Content>

