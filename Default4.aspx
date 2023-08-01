<%@ Page Language="VB" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" CodeFile="Default4.aspx.vb" Inherits="Default4" title="Pagina senza titolo" %>
<%@ Register src="tabelle_pos/Scambio_Importo.ascx" tagname="Scambio_Importo" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ListBox ID="lbLog" runat="server" Visible="False" Width="961px">
    </asp:ListBox>
    <uc1:Scambio_Importo ID="Scambio_Importo1" runat="server" />
</asp:Content>

