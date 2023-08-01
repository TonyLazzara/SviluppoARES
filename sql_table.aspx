<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sql_table.aspx.vb" Inherits="sql1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

        </div>
        <br />


 <div>

     <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1"
         AutoGenerateColumns="true">



     </asp:GridView>


</div>



<asp:SqlDataSource ID="SqlDataSource1" runat="server"  ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    ></asp:SqlDataSource>








    </form>
</body>
</html>
