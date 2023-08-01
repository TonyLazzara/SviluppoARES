<%@ Page Title=""  Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="gestione_mail.aspx.vb" Inherits="gestione_mail" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="div_intestazione_tipo_mail" runat="server" visible="false">
   <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Mail - Gestione Tipo Mail</b>
           </td>
         </tr>
    </table>
</div>

<div id="div_intestazione_mail" runat="server" visible="false">
  <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Mail - Gestione Mail: <asp:Label ID="lb_descrizione_tipo_mail" runat="server" Text="" /> </b>
           </td>
         </tr>
    </table>
</div>

<div id="div_modifica_tipo_mail" runat="server" visible="false">
  <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table8">
    <tr>
      <td>
        <asp:Label ID="Label12" runat="server" Text="Tipo Mail:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="tx_descrizione" runat="server"></asp:TextBox>  
        <asp:Label ID="lb_id" runat="server" Text='0' Visible="false" />
      </td>
    </tr>
    <tr>
      <td align="center" colspan="2">
            <asp:Button ID="bt_salva_modifica_tipo_mail" runat="server" Text="Salva" ValidationGroup="SalvaTipoMail" />
            <asp:Button ID="bt_chiudi_modifica_tipo_mail" runat="server" Text="Chiudi" />
      </td>
    </tr>
  </table>
</div>

<div id="div_elenco_tipo_mail" runat="server" visible="false">
  <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table5">
    <tr>
      <td colspan="2">
         <asp:ListView ID="listView_elenco_tipo_mail" runat="server" DataSourceID="sql_elenco_tipo_mail" DataKeyNames="id">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                          <asp:Label ID="lb_eliminabile" runat="server" Text='<%# Eval("eliminabile") %>' Visible="false" />
                      </td>
                      <td id="Td2" align="center" width="40px" runat="server" >
                        <asp:ImageButton ID="Lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="Lente"/>
                      </td>
                      <td id="Td3" align="center" width="40px" runat="server" >
                        <asp:ImageButton ID="Elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="Elimina" />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                          <asp:Label ID="lb_eliminabile" runat="server" Text='<%# Eval("eliminabile") %>' Visible="false" />
                      </td>
                      <td id="Td2" align="center" width="40px" runat="server" >
                        <asp:ImageButton ID="Lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="Lente"/>
                      </td>
                      <td id="Td3" align="center" width="40px" runat="server" >
                        <asp:ImageButton ID="Elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="Elimina"  />
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun Tipo Mail Censito.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                      <th id="Th2" runat="server">
                                          Tipo Mail</th>
                                      <th id="Th_lente" runat="server">
                                          </th>
                                      <th id="Th_elimina" runat="server">
                                          </th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                  </table>
              </LayoutTemplate>
          </asp:ListView>
        </td>
    </tr>
    <tr>
        <td colspan="2">&nbsp;
        </td>
    </tr>
    <tr>
      <td align="center" colspan="2">
            <asp:Button ID="bt_nuovo_tipo_mail" runat="server" Text="Nuovo" />
            <asp:Button ID="bt_chiudi_tipo_mail" runat="server" Text="Chiudi" />
      </td>
    </tr>
  </table>
</div>

<div id="div_modifica_mail" runat="server" visible="false">
  <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table2">
    <tr>
      <td>
        <asp:Label ID="Label3" runat="server" Text="Tipo Invio:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
          <asp:DropDownList ID="DropDownTipoInvio" runat="server">
            <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
            <asp:ListItem Value="1">TO</asp:ListItem>
            <asp:ListItem Value="2">CC</asp:ListItem>
            <asp:ListItem Value="3">BCC</asp:ListItem>
          </asp:DropDownList>
      </td>
    </tr>
    <tr>
      <td>
        <asp:Label ID="Label2" runat="server" Text="Nome:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="tx_nome" runat="server"></asp:TextBox>  
      </td>
    </tr>
    <tr>
      <td>
        <asp:Label ID="Label1" runat="server" Text="Mail:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="tx_mail" runat="server"></asp:TextBox>  
        <asp:Label ID="lb_id_mail" runat="server" Text='0' Visible="false" />
      </td>
    </tr>
    <tr>
      <td align="center" colspan="2">
            <asp:Button ID="bt_salva_mail" runat="server" Text="Salva" ValidationGroup="SalvaMail" />
            <asp:Button ID="bt_chiudi_mail" runat="server" Text="Chiudi" />
      </td>
    </tr>
  </table>
</div>

<div id="div_cerca_mail" runat="server" visible="false">
  <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table6">
    <tr>
      <td >
        <asp:Label ID="Label7" runat="server" Text="Tipo Mail:" CssClass="testo_bold"></asp:Label>
      </td>
      <td colspan="2">
          <asp:DropDownList ID="DropDown_tipo_mail" runat="server" AppendDataBoundItems="True" 
                  DataSourceID="sql_elenco_tipo_mail" DataTextField="descrizione" DataValueField="id">
            <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
          </asp:DropDownList> &nbsp;
          <asp:ImageButton ID="Aggiorna" runat="server" ImageUrl="/images/aggiorna.png" style="width: 16px" CommandName="Aggiorna"/>
        </td>
    </tr>
    <tr>
      <td style="width:20%;"> &nbsp;
      </td>
      <td align="center">
            <asp:Button ID="bt_cerca_mail" runat="server" Text="Cerca" ValidationGroup="Cerca" />
            <asp:Button ID="bt_chiudi_form" runat="server" Text="Chiudi" />
      </td>
      <td align="right" style="width:20%;"> <asp:Button ID="bt_elenco_tipo_mail" runat="server" Text="Elenco Tipi Mail" />
      </td>
    </tr>
  </table>
</div>

<div id="div_elenco_mail" runat="server" visible="false">
  <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table3">
    <tr>
      <td colspan="2">
         <asp:ListView ID="listView_elenco_mail" runat="server" DataSourceID="sql_elenco_mail" DataKeyNames="id">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_mail" runat="server" Text='<%# Eval("mail") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_nome" runat="server" Text='<%# Eval("nome") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_tipo_invio" runat="server" Text='<%# Eval("id_tipo_invio") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_tipo_invio" runat="server" Text='' />
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="Lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="Lente"/>
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="Elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="Elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione della mail?'));" />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_mail" runat="server" Text='<%# Eval("mail") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_nome" runat="server" Text='<%# Eval("nome") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_tipo_invio" runat="server" Text='<%# Eval("id_tipo_invio") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_tipo_invio" runat="server" Text='' />
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="Lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="Lente"/>
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="Elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="Elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione della mail?'));" />
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessuna Mail Censita.
                          </td>
                      </tr>
                  </table>
              </EmptyDataTemplate>
              <LayoutTemplate>
                  <table id="Table1" runat="server" width="100%">
                      <tr id="Tr1" runat="server">
                          <td id="Td1" runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                   <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                      <th>Mail</th>
                                      <th>Nominativo</th>
                                      <th>Tipo Mail</th>
                                      <th></th>
                                      <th></th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                  </table>
              </LayoutTemplate>
          </asp:ListView>
        </td>
    </tr>
    <tr>
        <td colspan="2">&nbsp;
        </td>
    </tr>
    <tr>
      <td align="center" colspan="2">
            <asp:Button ID="bt_nuova_mail" runat="server" Text="Nuovo" />
            <asp:Button ID="bt_chiudi_elenco_mail" runat="server" Text="Chiudi" />
      </td>
    </tr>
  </table>
</div>

<asp:Label ID="lb_stato_edit" runat="server" Text='0' Visible="false" />

<asp:Label ID="lb_id_tipo_mail" runat="server" Text='0' Visible="false" />

<asp:SqlDataSource ID="sql_elenco_mail" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT * FROM mail_destinatari WITH(NOLOCK) WHERE id_mail = @id_mail ORDER BY id_tipo_invio, mail">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_tipo_mail" Name="id_mail" PropertyName="Text" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sql_elenco_tipo_mail" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT * FROM mail_tipo WITH(NOLOCK) ORDER BY [descrizione]">
</asp:SqlDataSource>

<asp:ValidationSummary ID="ValidationSummary2" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="Cerca" />

<asp:CompareValidator ID="CompareValidator1" runat="server" 
    ControlToValidate="DropDown_tipo_mail" ErrorMessage="Specificare un tipo mail valido."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="Cerca" > </asp:CompareValidator>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="SalvaMail" />

<asp:CompareValidator ID="CompareValidator2" runat="server" 
    ControlToValidate="DropDownTipoInvio" ErrorMessage="Specificare il tipo invio della mail (TO, CC, BCC)."
    Type="Integer" Operator="GreaterThan" ValueToCompare="0"
    Font-Size="0pt" ValidationGroup="SalvaMail" > </asp:CompareValidator>

<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
    ControlToValidate="tx_mail" ErrorMessage="Il campo mail è obbligatorio." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="SalvaMail"></asp:RequiredFieldValidator> 

<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
    ControlToValidate="tx_mail" ErrorMessage="Immettere un valore corretto per la mail." 
    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="SalvaMail" ></asp:RegularExpressionValidator>

<asp:ValidationSummary ID="ValidationSummary3" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="SalvaTipoMail" />

<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ControlToValidate="tx_descrizione" ErrorMessage="Il campo tipo mail è obbligatorio." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="SalvaTipoMail"></asp:RequiredFieldValidator> 

</asp:Content>

