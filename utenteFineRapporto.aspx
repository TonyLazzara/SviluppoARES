<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuSqlInj.master" AutoEventWireup="false" CodeFile="utenteFineRapporto.aspx.vb" Inherits="utenteFineRapporto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div id="SqlInjection" runat="server" visible="true">       
        <table cellpadding="4" cellspacing="6" width="100%" style="border:4px solid #2E6D54" border="1" runat="server" id="tblFestioneFiliale">
            <tr>
                <td align=center>
                    <asp:Label ID="lblTesto" runat="server" 
                        Text="A T T E N Z I O N E  ( TENTATIVO Accesso ad ARES senza Autorizzazione )" 
                        CssClass="TestoSqlInjection"></asp:Label>
                        <br /><br />
                    <asp:Label ID="Label1" runat="server" 
                        Text="Il Sitema ha appena inviato una mail all'amministratore del sistema" 
                        CssClass="TestoSqlInjection"></asp:Label>
                </td>
            </tr>
        </table>
     </div>
</asp:Content>

