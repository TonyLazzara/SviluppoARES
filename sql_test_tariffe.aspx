<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sql_test_tariffe.aspx.vb" Inherits="sql1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-top:10px;">

            Stazione Uscita: <asp:DropDownList ID="ddl_stazione_uscita" runat="server">
                <asp:ListItem Value="2">Palermo APT</asp:ListItem>
                <asp:ListItem Value="3">Palermo Porto</asp:ListItem>
                <asp:ListItem Value="4">Catania APT</asp:ListItem>
                <asp:ListItem Value="6">Comiso APT</asp:ListItem>
                <asp:ListItem Value="7">Cinisi</asp:ListItem>
                <asp:ListItem Value="9">Milazzo Porto</asp:ListItem>
            </asp:DropDownList>

              Stazione Rientro: <asp:DropDownList ID="ddl_stazione_rientro" runat="server">
                <asp:ListItem Value="2" >Palermo APT</asp:ListItem>
                <asp:ListItem Value="3">Palermo Porto</asp:ListItem>
                <asp:ListItem Value="4">Catania APT</asp:ListItem>
                  <asp:ListItem Value="6">Comiso APT</asp:ListItem>
                <asp:ListItem Value="7">Cinisi</asp:ListItem>
                <asp:ListItem Value="9">Milazzo Porto</asp:ListItem>
            </asp:DropDownList>
                                 

             &nbsp;  &nbsp;  <asp:Label ID="lbl_oow" runat="server" Text=""></asp:Label>

            <br /><br />

            data uscita: <asp:TextBox ID="txt_data_uscita" runat="server" placeholder="GG/MM/AAAA" Width="120"></asp:TextBox>
            ora uscita: <asp:TextBox ID="txt_ora_uscita" runat="server" placeholder="HH:MM" Width="80"></asp:TextBox>
            <br /><br />
            data rientro: <asp:TextBox ID="txt_data_rientro" runat="server" placeholder="GG/MM/AAAA" Width="120"></asp:TextBox>
            ora rientro: <asp:TextBox ID="txt_ora_rientro" runat="server" placeholder="HH:MM" Width="80"></asp:TextBox>

             <br /> <br />

             <asp:Button ID="btn_go" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC" 
        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" OnClick="btn_go_Click"
        Font-Size="1em" ForeColor="#284775" Text="Visualizza" />
    
              &nbsp; &nbsp; 

            <asp:Button ID="btnrefresh" runat="server" BackColor="#FFFBFF" BorderColor="#CCCCCC" 
        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" OnClick="btnrefresh_Click"
        Font-Size="1em" ForeColor="#284775" Text="refresh page" /> 
            
            
            &nbsp;  &nbsp;  <asp:Label ID="lbl_error" runat="server" Text=""></asp:Label>
             &nbsp;  &nbsp;  <asp:Label ID="lbl_lastid" runat="server" Text=""></asp:Label>

            <br /><br />

            Start : <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            
            <br /><br />
            End: <asp:Label ID="Label2" runat="server" Text=""></asp:Label>


             <br /><br />
            Risultato Richiesta:<br /> 

            <asp:TextBox ID="txt_result" runat="server" TextMode="MultiLine" Width="800" Height="600"></asp:TextBox>

            <%--<asp:ListBox ID="ListBox1" runat="server" Width="400" Height="500">

            </asp:ListBox>--%>



        </div>
    </form>
</body>
</html>
