<%@ Page Language="VB" MasterPageFile="MasterPage2.master" AutoEventWireup="true" CodeFile="LogIn.aspx.vb" Inherits="LogIn2" title="ARES - Sicily Rent Car" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div>
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" >
                  <br />
                  <table border="1" cellpadding="4" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td style="height: 146px">
                     <table border="0" cellpadding="0">
                       <tr class="sfondo_verde">
                         <td align="center" colspan="2" style="height: 26px; background-color:green" >                           
                             <asp:Label ID="Label1" runat="server" Text="Area LogIn" CssClass="testo_login"></asp:Label>
                         </td>
                       </tr>
                       <tr>
                         <td align="right">
                           <asp:Label ID="LblCodiceCliente" runat="server" CssClass="testo_bold">
                             Username:
                           </asp:Label>
                         </td>
                         <td align="right" >
                           <asp:TextBox ID="TxtCodiceCliente" runat="server" Font-Size="1em" Width="150px"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredCodiceCliente" runat="server" ControlToValidate="TxtCodiceCliente" ErrorMessage="Il codice cliente è obbligatorio." ToolTip="Il codice cliente è obbligatorio." ValidationGroup="Login1">
                             *
                           </asp:RequiredFieldValidator>
                         </td>
                       </tr>
                       <tr>
                         <td align="right">
                           <asp:Label ID="LblPassword" runat="server" CssClass="testo_bold">
                             Password:
                           </asp:Label>
                         </td>
                         <td align="right" >
                           <asp:TextBox ID="TxtPassword" runat="server" Font-Size="1em" TextMode="Password" Width="150px"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredPassword" runat="server" ControlToValidate="TxtPassword" ErrorMessage="La password è obbligatoria." ToolTip="La password è obbligatoria." ValidationGroup="Login1">
                             *
                           </asp:RequiredFieldValidator>
                         </td>
                       </tr>                       
                       <tr>
                         <td align="center" colspan="2" style="color: red">
                           <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                         </td>
                       </tr>
                       <tr>
                         <td align="right" colspan="2">
              <asp:Button ID="LginAccedi" runat="server"  Text="Accedi" />
                             <br />
                             </td>
                       </tr>                         
                         
                     </table>
                   </td>
                 </tr>
    </table>  
                  <br />
            </td>
        </tr>
    </table>  
    </div>
    
</asp:Content>

