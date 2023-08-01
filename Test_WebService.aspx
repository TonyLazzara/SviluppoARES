<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Test_WebService.aspx.vb" Inherits="sql1" %>

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
        Font-Size="1em" ForeColor="#284775" Text="Prenota" /><br />

    <asp:Label ID="lbl_lastid" runat="server" Text="0"></asp:Label>

            <br />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

            <br /><br />
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
              <br />  <br />
            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                <br />  <br />

            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Height="200px" Width="670px" ></asp:TextBox>


        </div>

        <br />
        <div>
             <asp:Button ID="Button1" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC" 
        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" OnClick="Button1_Click"
        Font-Size="1em" ForeColor="#284775" Text="Lista EXTRA" /><br />

            <asp:Label ID="Label4" runat="server" Text="0"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox3" runat="server" placeholder="id_preventivo"></asp:TextBox> - -  <asp:TextBox ID="TextBox4" runat="server" placeholder="id_preventivo_prep"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label5" runat="server" Text=""></asp:Label>

            <br /><br />
            <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
              <br />  <br />
            <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                <br />  <br />

            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Height="200px" Width="670px" ></asp:TextBox>


        </div>








    </form>
</body>
</html>
