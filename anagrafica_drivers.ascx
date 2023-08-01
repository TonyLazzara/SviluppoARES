<%@ Control Language="VB" AutoEventWireup="false" CodeFile="anagrafica_drivers.ascx.vb" Inherits="gestione_fornitori_anagrafica_drivers" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<div id="div_intestazione" runat="server" visible="false">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Drivers</b>
           </td>
         </tr>
    </table>
</div>

<div id="div_ricerca" runat="server" visible="false">
  <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Cognome:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_r_cognome" runat="server" Width="180px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label5" runat="server" Text="Nome:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_r_nome" runat="server" Width="180px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Codice Fiscale:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_r_codice_fiscale" runat="server" Width="120px"></asp:TextBox>
        </td>
        
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label14" runat="server" Text="Patente:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_r_patente" runat="server" Width="120px"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label11" runat="server" Text="Comune:" CssClass="testo_bold"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="tx_r_comune" runat="server" Width="180px"></asp:TextBox>
        </td>
        <td>
            
        </td>
        <td>
            
        </td>
    </tr>
    <tr>
      <td colspan="6" align="center">
        <asp:Button ID="bt_nuovo" runat="server" Text="Nuovo" />
        <asp:Button ID="bt_cerca" runat="server" Text="Cerca" />
        <asp:Button ID="bt_chiudi" runat="server" Text="Chiudi" />
      </td>
    </tr>
  </table>
</div>

<div id="div_elenco" runat="server" visible="false">
  <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="table2">
    <tr>
      <td>
          <asp:ListView ID="listViewElenco" runat="server" DataSourceID="sql_elenco">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_cognome" runat="server" Text='<%# Eval("cognome") %>' />
                          <asp:Label ID="lb_nome" runat="server" Text='<%# Eval("nome") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_codice_fiscale" runat="server" Text='<%# Eval("codice_fiscale") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_indirizzo" runat="server" Text='<%# Eval("indirizzo") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_comune" runat="server" Text='<%# Eval("citta") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_telefono" runat="server" Text='<%# Eval("telefono") %>' />
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="Modifica" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="Modifica"/>
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="Elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="Elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione del fornitore?'));" />
                      </td>
                  </tr>
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                          <asp:Label ID="lb_cognome" runat="server" Text='<%# Eval("cognome") %>' />
                          <asp:Label ID="lb_nome" runat="server" Text='<%# Eval("nome") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_codice_fiscale" runat="server" Text='<%# Eval("codice_fiscale") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_indirizzo" runat="server" Text='<%# Eval("indirizzo") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_comune" runat="server" Text='<%# Eval("citta") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_telefono" runat="server" Text='<%# Eval("telefono") %>' />
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="Modifica" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="Modifica"/>
                      </td>
                      <td align="center" width="40px">
                        <asp:ImageButton ID="Elimina" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="Elimina" OnClientClick="javascript: return(window.confirm('Confermi la cancellazione del fornitore?'));" />
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr>
                          <td>
                              Non è stato restituito alcun dato con i filtri impostati.
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
                                      <th>Cognome Nome</th>
                                      <th>Codice Fiscale</th>
                                      <th>Indirizzo</th>
                                      <th>Comune</th>
                                      <th>Telefono</th>
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

                                      <asp:TemplatePagerField>              
                                        <PagerTemplate>
                                            Totale Record: <asp:Label  runat="server" ID="TotalItemsLabel" Text="<%# Container.TotalRowCount%>" /> 
                                            - Pagina corrente: <asp:Label  runat="server" ID="NumPaginalabel" Text="<%# ((Container.StartRowIndex \ Container.PageSize) + 1)%>" />
                                            di: <asp:Label  runat="server" ID="TotalePagineLabel" Text="<%# System.Math.Truncate(Container.TotalRowCount / Container.PageSize + 0.9999)%>" />         
                                        </PagerTemplate>
                                       </asp:TemplatePagerField>
                                  </Fields>
                              </asp:DataPager>
                          </td>
                      </tr>
                  </table>
              </LayoutTemplate>
          </asp:ListView>
      </td>
    </tr>
  </table>
  <br />
</div>

<div id="div_modifica" runat="server" visible="false">
  <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
    <tr>
      <td>
        <asp:Label ID="lb_id" runat="server" Text="" Visible="false" />
        <asp:Label ID="lb" runat="server" Text="Cognome:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="tx_cognome" runat="server" Text="" MaxLength="50" Width="210px"/>
      </td>
      <td>
        <asp:Label ID="Label12" runat="server" Text="Nome:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="tx_nome" runat="server" Text="" MaxLength="50" Width="210px"/>
      </td>
    </tr>
    <tr>
      <td>
        <asp:Label ID="Label7" runat="server" Text="Data Nascita:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
          <a onclick="Calendar.show(document.getElementById('<%=tx_data_nascita.ClientID%>'), '%d/%m/%Y', false)"> 
        <asp:TextBox ID="tx_data_nascita" runat="server" Text=""  Width="70px"/></a>
      <%--  <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_data_nascita" ID="CalendarExtender1">
        </ajaxtoolkit:CalendarExtender>--%>
        <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999"  TargetControlID="tx_data_nascita" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender1">
        </ajaxtoolkit:MaskedEditExtender>
      </td>
      <td>
        <asp:Label ID="Label17" runat="server" Text="Comune Nascita:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="tx_citta_nascita" runat="server" Text="" MaxLength="50" Width="210px"/>
      </td>
    </tr>
    <tr>
      <td>
        <asp:Label ID="Label18" runat="server" Text="Codice Fiscale:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="tx_codice_fiscale" runat="server" Text="" MaxLength="20"  Width="180px"/>
      </td>
      <td></td>
      <td></td>
    </tr>
    <tr>
      <td>
        <asp:Label ID="Label8" runat="server" Text="Indirizzo:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="tx_indirizzo" runat="server" Text="" MaxLength="50" Width="210px"/>
      </td>
      <td>
        <asp:Label ID="Label9" runat="server" Text="Comune:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="tx_comune" runat="server" Text="" MaxLength="50" Width="210px"/>
      </td>
    </tr>
    <tr>
      <td>
        <asp:Label ID="Label6" runat="server" Text="CAP:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="tx_cap" runat="server" Text="" MaxLength="5" Width="40px"/>
      </td>
      <td>
        <asp:Label ID="Label10" runat="server" Text="Provincia (sigla):" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="tx_provincia" runat="server" Text="" MaxLength="2" Width="30px"/>
      </td>
    </tr>
    <tr>
      <td>
        <asp:Label ID="Label13" runat="server" Text="Telefono:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="tx_telefono" runat="server" Text="" MaxLength="20" Width="180px"/>
      </td>
      <td>
          &nbsp;</td>
      <td>
          &nbsp;</td>
    </tr>
    <tr>
      <td>
        <asp:Label ID="Label1" runat="server" Text="Patente:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="tx_patente" runat="server" Text="" MaxLength="30" Width="180px"/>
      </td>
      <td>
        <asp:Label ID="Label15" runat="server" Text="Tipo Patente:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="tx_tipo_patente" runat="server" Text="" MaxLength="5" Width="70px"/>
      </td>
    </tr>
    <tr>
      <td>
        <asp:Label ID="Label21" runat="server" Text="Luogo Emissione:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
        <asp:TextBox ID="tx_luogo_emissione" runat="server" Text="" MaxLength="50" Width="210px"/>
      </td>
      <td>
        <asp:Label ID="Label22" runat="server" Text="Scadenza Patente:" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
          <a onclick="Calendar.show(document.getElementById('<%=tx_scadenza_patente.ClientID%>'), '%d/%m/%Y', false)"> 
        <asp:TextBox ID="tx_scadenza_patente" runat="server" Text=""  Width="70px"/></a>
    <%--    <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_scadenza_patente" ID="CalendarExtender2">
        </ajaxtoolkit:CalendarExtender>--%>
        <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999"  TargetControlID="tx_scadenza_patente" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender2">
        </ajaxtoolkit:MaskedEditExtender>
      </td>
    </tr>
    <tr>
      <td colspan="4">

          &nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="4">
            <asp:Button ID="btnSeleziona" runat="server" Text="Seleziona" ValidationGroup="Modifica" Visible="false" />
            <asp:Button ID="btnModifica" runat="server" Text="Modifica" ValidationGroup="Modifica" />
            <asp:Button ID="btnChiudiModifica" runat="server" Text="Chiudi" />
        </td>
    </tr>
  </table>
  <br />
</div>

<asp:Label ID="lb_stato" runat="server" Text="0" Visible="false" />
<asp:Label ID="lb_provenienza" runat="server" Text="" Visible="false" />

<asp:Label ID="lb_sql_elenco" runat="server" Text="" Visible="false" />

<asp:SqlDataSource ID="sql_elenco" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="">
</asp:SqlDataSource>


<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="Modifica" />

<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ControlToValidate="tx_cognome" ErrorMessage="Specificare il cognome del guidatore." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="Modifica"></asp:RequiredFieldValidator>


