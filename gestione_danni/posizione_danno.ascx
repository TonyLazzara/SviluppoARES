<%@ Control Language="VB" AutoEventWireup="false"  CodeFile="posizione_danno.ascx.vb" Inherits="gestione_danni_posizione_danno" %>

<div id="div_intestazione" runat="server" visible="false">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Veicoli - Gestione Posizione Danno</b>
           </td>
         </tr>
    </table>
</div>

<div id="div_elenco_tipo_danno" runat="server" visible="false">
  <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table2">
    <tr>
      <td>
          <asp:ListView ID="listViewTipoDanni" runat="server" DataSourceID="sqlPosizioneDanno">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_bloccante" runat="server" Text='<%# Eval("bloccante") %>' Visible="false" />
                          <asp:Label ID="lb_des_bloccante" runat="server" Text='' />
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="Modifica" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="Modifica"/>
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="Elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="Elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione della posizione del danno?'));" />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_descrizione" runat="server" Text='<%# Eval("descrizione") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_bloccante" runat="server" Text='<%# Eval("bloccante") %>' Visible="false" />
                          <asp:Label ID="lb_des_bloccante" runat="server" Text='' />
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="Modifica" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="Modifica" />
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="Elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="Elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione della posizione del danno?'));" />
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr>
                          <td>
                              Non è stato restituito alcun dato.
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
                                      <th id="Th1" runat="server">
                                          Descrizione</th>
                                      <th id="Th5" runat="server">
                                          Bloccante</th>
                                      <th id="Th2" runat="server">
                                          </th>
                                      <th id="Th6" runat="server">
                                          </th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                      <tr id="Tr2" runat="server">
                          <td id="Td2" runat="server" style="">
                              <asp:DataPager ID="DataPager1" PageSize="20" runat="server">
                                  <Fields>
                                      <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                          ShowNextPageButton="true" ShowPreviousPageButton="true" ShowLastPageButton="True" />
                                  </Fields>
                              </asp:DataPager>
                          </td>
                      </tr>
                  </table>
              </LayoutTemplate>
          </asp:ListView>
      </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnNuovo" runat="server" Text="Nuovo" />
            <asp:Button ID="btnChiudi" runat="server" Text="Chiudi" />
        </td>
    </tr>
  </table>
  <br />
</div>

<div id="div_modifica_tipo_danno" runat="server" visible="false">
  <br />
  <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table3">
    <tr>
      <td>
        <asp:Label ID="lb" runat="server" Text="Posizione danno:" CssClass="testo_bold"></asp:Label>
        <asp:Label ID="lb_id" runat="server" Text="" Visible="false" />
      </td>
      <td>
        <asp:TextBox ID="tx_descrizione" runat="server" Text="" MaxLength="50" Width="340px"/>
      </td>
    </tr>
    <tr>
      <td>
        <asp:Label ID="Label1" runat="server" Text="Blocco Ready To Go:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
         <asp:DropDownList ID="DropDownBloccante" runat="server" >
                    <asp:ListItem Value="-1" Selected="True">Seleziona</asp:ListItem>
                    <asp:ListItem Value="0" >Non Bloccante</asp:ListItem>
                    <asp:ListItem Value="1" >Bloccante</asp:ListItem>
                </asp:DropDownList>
      </td>
    </tr>
    <tr>
      <td colspan="2">

          &nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="2">
            <asp:Button ID="btnModifica" runat="server" Text="Modifica" ValidationGroup="Modifica" />
            <asp:Button ID="btnChiudiModifica" runat="server" Text="Chiudi" />
        </td>
    </tr>
  </table>
  <br />
</div>

<asp:Label ID="lb_stato" runat="server" Text="0" Visible="false" />
<asp:Label ID="lb_provenienza" runat="server" Text="" Visible="false" />

<asp:SqlDataSource ID="sqlPosizioneDanno" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT * FROM [veicoli_posizione_danno] WITH(NOLOCK) ORDER BY [descrizione]">
</asp:SqlDataSource>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="Modifica" />

<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ControlToValidate="tx_descrizione" ErrorMessage="Specificare la posizione di danno." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="Modifica"></asp:RequiredFieldValidator>

<asp:CompareValidator ID="RequiredFieldValidator2" runat="server" 
    ControlToValidate="DropDownBloccante" ErrorMessage="Specificare se il danno è bloccante o meno per il l'opzione Ready To Go."
    Type="Integer" Operator="GreaterThan" ValueToCompare="-1"
    Font-Size="0pt" ValidationGroup="Modifica" > </asp:CompareValidator>

