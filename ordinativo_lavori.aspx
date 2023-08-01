<%@ Page Language="VB" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" CodeFile="ordinativo_lavori.aspx.vb" Inherits="ordinativo_lavori" title="Pagina senza titolo" %>
<%@ Register Src="/ordinativo_lavori/richiesta_ordinativo.ascx" TagName="richiesta_ordinativo" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <meta http-equiv="Expires" content="0" />
  <meta http-equiv="Cache-Control" content="no-cache" />
  <meta http-equiv="Pragma" content="no-cache" />

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
	</style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <table style="color: #FFFFFF" bgcolor="#215A87" width="1024px">
         <tr>
           <td >
             <%--32--%>
                <asp:Button ID="richiestaOrdinativo" runat="server" CssClass="menu"  
                    Font-Names="Verdana" Height="21px" Text="Richiesta ordinativo" BackColor="#215A87" 
                    Font-Size="Small" ForeColor="White" Width="160px" />
           </td>
         </tr>
   </table>

<asp:Panel ID="PanelRichiestaOrdinativo" runat="server" Visible="false" Width="100%">
  <uc1:richiesta_ordinativo ID="richiesta_ordinativo" runat="server" />
</asp:Panel>

</asp:Content>

