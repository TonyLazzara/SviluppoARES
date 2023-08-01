<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="cambiapwd.aspx.vb" Inherits="cambiapwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">
        function vertxt() {

            var us = document.getElementById("<%=txt_us.ClientID %>").value;
            var oldpwd = document.getElementById("<%=oldpwd.ClientID %>").value;
            var newpwd = document.getElementById("<%=newpwd.ClientID %>").value;
            var newpwd1 = document.getElementById("<%=newpwd1.ClientID %>").value;

            if (us == "") {
                 alert('Campo Utente vuoto!');
                document.getElementById("<%=txt_us.ClientID %>").focus();
                return false;
            }

            else if (oldpwd == "") {
                alert('Campo Password Attuale vuoto!');
                document.getElementById("<%=oldpwd.ClientID %>").focus();
                return false;
            }
            else if (newpwd == "")  {
                 alert('Campo Nuova Password vuoto!');
                document.getElementById("<%=newpwd.ClientID %>").focus();
                return false;
            }
            else if (newpwd != newpwd1) {
                alert('La nuova password ridigitata non è uguale alla nuova password !');
                document.getElementById("<%=newpwd1.ClientID %>").focus();
                return false;
            }
            else {
                if (confirm('Confermi Modifica Password ?')) {
                    return true;
                } else {
                    return false;
                }
            }
            
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
       <div style="font-family:Verdana;font-size:18px;left:30px;font-weight:bold;">
            Cambia Password<br />       <br />      
        </div>
      <div style="font-family:Verdana;font-size:12px;left:30px;">
            <asp:PlaceHolder ID="PlaceHolder2" runat="server">
           <asp:Label ID="lbl_nota" runat="server" Text="Label"></asp:Label>
                </asp:PlaceHolder>
        </div>



    <div style="font-family:Verdana;font-size:12px;left:30px;margin-top:20px;">
        <asp:PlaceHolder ID="PlaceHolder1" runat="server">
       <table style="height:200px;width:400px;">
            <tr>
               <td>Utente:</td>
                <td><asp:TextBox ID="txt_us" runat="server" TextMode="SingleLine"></asp:TextBox></td>
           </tr>
           <tr>
               <td>Password attuale:</td>
                <td><asp:TextBox ID="oldpwd" runat="server" TextMode="Password"></asp:TextBox></td>
           </tr>
            <tr>
               <td>Password nuova:</td>
                <td><asp:TextBox ID="newpwd" runat="server" TextMode="Password"></asp:TextBox></td>
           </tr>
             <tr>
               <td>Password nuova ridigita:</td>
                <td><asp:TextBox ID="newpwd1" runat="server" TextMode="Password"></asp:TextBox></td>
           </tr>
            <tr style="text-align:center;">
               
                <td colspan="2"><asp:Button ID="Button1" runat="server" Text="conferma" OnClick="Button1_Click" OnClientClick="return vertxt();" /> 
                    &nbsp&nbsp&nbsp<input type="reset" value="cancella" /></td>
           </tr>
                     
        </table>
        </asp:PlaceHolder>

        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <asp:HyperLink ID="HyperLink1" runat="server">vai alla pagina di login ed inserisci la nuova password</asp:HyperLink>


    </div>



</asp:Content>

