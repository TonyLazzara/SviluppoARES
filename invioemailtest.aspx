<%@ Page Language="VB" AutoEventWireup="false" CodeFile="invioemailtest.aspx.vb" Inherits="invioemailtest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">


         <div style="margin-bottom:30px;">
        Mittente Stazione:
            <asp:DropDownList ID="ddl_stazioni" runat="server">
                <asp:ListItem Value="system@sicilyrentcars.it">System</asp:ListItem>
                <asp:ListItem Value="booking@sicilyrentcar.it">Sede (Booking)</asp:ListItem>
                <asp:ListItem Value="palermoapt@sicilyrentcar.it">Palermo Aeroporto</asp:ListItem>
                <asp:ListItem Value="palermodt@sicilyrentcar.it">Palermo Porto</asp:ListItem>                
                 <asp:ListItem Value="cataniaaeroporto@sicilyrentcar.it">Catania Aeroporto</asp:ListItem>                
                <asp:ListItem Value="comisoapt@sicilyrentcar.it">Comiso Aeroporto</asp:ListItem>

            </asp:DropDownList>
        </div>

       <div style="margin-bottom:30px;">
         email Destinatario : <asp:TextBox ID="TextBox1" runat="server" Text="system@sicilyrentcars.it"></asp:TextBox>

        </div>

        <div style="margin-bottom:30px;">

            <asp:Button ID="btnInvioEmail" runat="server" Text="invio email" OnClick="btnInvioEmail_Click" OnClientClick="return confirm('Confermi invio?');"/>
        </div>


        <div style="margin-bottom:30px;">
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>
      
        <div style="margin-bottom:30px;">
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        </div>

    </form>
</body>
</html>

