<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sql1.aspx.vb" Inherits="sql1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <asp:Button ID="btn_go" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC" 
        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" OnClick="btn_go_Click"
        Font-Size="1em" ForeColor="#284775" Text="Elenco" /><br />
    <asp:Label ID="lbl_lastid" runat="server" Text="0"></asp:Label>

            <br />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

        </div>
    </form>
</body>
</html>
