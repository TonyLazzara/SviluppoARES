<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sqltestGetElencoVeicoli.aspx.vb" Inherits="sqltest2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

             <asp:Button ID="LginAccedi" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC" 
        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" OnClick="LginAccedi_Click" Visible="false"
        Font-Size="1em" ForeColor="#284775" Text="Accedi" /><br />
    <asp:Label ID="lbl_lastid" runat="server" Text="0"></asp:Label>

            
             <asp:Button ID="Elenco_Veicoli" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC" 
        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" OnClick="Elenco_Veicoli_Click"
        Font-Size="1em" ForeColor="#284775" Text="Elenco Veicoli" /><br />
    <asp:Label ID="Label1" runat="server" Text="0"></asp:Label>



        </div>
    </form>
</body>
</html>
