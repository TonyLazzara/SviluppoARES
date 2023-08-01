<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sql_codiciClienti.aspx.vb" Inherits="sql1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:800px;">
             <asp:Button ID="btn_go" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC" 
        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" OnClick="btn_go_Click"
        Font-Size="1em" ForeColor="#284775" Text="Crea Codici" />


            <asp:DropDownList ID="ddl_newcodici" runat="server">
                <asp:ListItem value="1">1</asp:ListItem>
                <asp:ListItem value="5">5</asp:ListItem>
                <asp:ListItem value="10">10</asp:ListItem>
                <asp:ListItem value="20">20</asp:ListItem>
                <asp:ListItem value="50">50</asp:ListItem>
                <asp:ListItem value="100">100</asp:ListItem>
                <asp:ListItem value="200">200</asp:ListItem>

            </asp:DropDownList>

                        <br />

            &nbsp;&nbsp;&nbsp;


                        &nbsp;&nbsp;&nbsp;
    <asp:Label ID="lbl_lastid" runat="server" Text="0"></asp:Label>

            


            <br />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

        </div>
    </form>
</body>
</html>
