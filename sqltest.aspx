<%@ Page Language="VB" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="sqltest.aspx.vb" Inherits="LogIn" title="SQL" Debug="true" Buffer="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <meta http-equiv="Expires" content="0" />
   <meta http-equiv="Cache-Control" content="no-cache" />
   <meta http-equiv="Pragma" content="no-cache" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:Button ID="LginAccedi" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC" 
        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" OnClick="LginAccedi_Click"
        Font-Size="1em" ForeColor="#284775" Text="Accedi" /><br />
    <asp:Label ID="lbl_lastid" runat="server" Text="0"></asp:Label>
</asp:Content>

