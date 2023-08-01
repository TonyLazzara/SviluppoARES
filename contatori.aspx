<%@ Page Language="VB" AutoEventWireup="true" CodeFile="contatori.aspx.vb" Inherits="contatori"  MasterPageFile="~/MasterPageNoMenu.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />
<script>
    function filterInputInt(evt) {
        var keyCode, Char, inputField;
        var filter = '0123456789';
        if (window.event) {
            keyCode = window.event.keyCode;
            evt = window.event;
        } else if (evt) keyCode = evt.which;
        else return true;

        inputField = evt.srcElement ? evt.srcElement : evt.target || evt.currentTarget;
        if ((keyCode == null) || (keyCode == 0) || (keyCode == 8) || (keyCode == 9) || (keyCode == 13) || (keyCode == 27)) return true;
        Char = String.fromCharCode(keyCode);
        if (Char == '.') {
            Char = ',';

            if (window.event) {
                window.event.keyCode = 44;
            } else if (evt) evt.which = 44;
        }

        if ((filter.indexOf(Char) == -1)) return false;

        var SelStart = inputField.selectionStart;
        var SelEnd = inputField.selectionEnd;
        var SelLenght = SelEnd - SelStart;

        var stringaBase = inputField.value.substring(0, inputField.selectionStart);
        stringaBase = stringaBase + inputField.value.substring(SelEnd, inputField.value.length);

        if (Char == ',') {
            if ((stringaBase.indexOf(Char) >= 0)) return false;
        }

    }

</script>
<style type="text/css">
    .style4
    {
        width: 222px;
    }
    .style5
    {
        width: 79px;
    }
    .style6
    {
        width: 222px;
        height: 22px;
    }
    .style7
    {
        width: 79px;
        height: 22px;
    }
    .style9
    {
        height: 22px;
    }
    .style10
    {
        width: 447px;
        height: 22px;
    }
    .style11
    {
        width: 447px;
    }
    </style>

<table border="0" cellpadding="0" cellspacing="0" width="1024px" 
    bgcolor="#444" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;">
             <b>Contatori</b>
           </td>
         </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #215a87">
<tr>
  <td align="left" class="style6">
      <asp:Label ID="lblNuovoElemento" runat="server" Text="Contatore fatturazione RA:" CssClass="testo_bold"></asp:Label>
  </td>
  <td align="left" class="style7" >
      <asp:TextBox ID="txt_cont_ra" runat="server" Width="63px" onKeyPress="return filterInputInt(event)"></asp:TextBox>

      </td>
  <td align="left" class="style10">
      <asp:Button ID="btnSalva" runat="server" Text="Modifica" ValidationGroup="invia" Enabled="false"/>
      &nbsp;<asp:Label ID="Label1" runat="server" Text="Attuale:" CssClass="testo_bold"></asp:Label>&nbsp;<asp:Label ID="num_cont_RA" runat="server" Text="" CssClass="testo_bold"></asp:Label>
    </td>
  <td align="left" class="style9">
      &nbsp;</td>     
</tr>
<tr>
  <td class="style4">
  
      &nbsp;</td>
  <td align="left" class="style5">
  
      
      </td>
      <td class="style11">
        
      </td>
      <td>
        
      </td>
</tr>
<tr>
  <td class="style1" colspan="4" align="center">
      &nbsp;&nbsp;
      <br />
             
            &nbsp;&nbsp;&nbsp;<br />
             
<asp:Label ID="livelloAccesso" runat="server" Visible="false"></asp:Label>
  </td>
</tr>
</table>
      </asp:Content>