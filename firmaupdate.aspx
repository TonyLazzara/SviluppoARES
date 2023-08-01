<%@ Page Language="VB" AutoEventWireup="true" CodeFile="firmaupdate.aspx.vb" Inherits="sqltest2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div  style="margin-top:30px;margin-left:30px;">

            Numero Contratto:&nbsp;&nbsp;<asp:TextBox ID="txt_num_contratto" runat="server"></asp:TextBox> 
           &nbsp; &nbsp;<asp:Button ID="LginAccedi" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC" 
        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" OnClick="LginAccedi_Click"
        Font-Size="1em" ForeColor="#284775" Text="Firma Contratto Rientro" />
            
        </div>

        <div  style="margin-top:30px;margin-left:30px;">
                  <asp:Label ID="lbl_msg" runat="server" Text="-"></asp:Label>
            &nbsp; &nbsp;
            <asp:HyperLink ID="HyperLink1" runat="server">v.contratto</asp:HyperLink>


            </div>


    </form>
</body>
</html>
