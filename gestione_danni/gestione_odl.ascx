<%@ Control Language="VB" AutoEventWireup="false" CodeFile="gestione_odl.ascx.vb" Inherits="gestione_danni_gestione_odl" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<%@ Register Src="edit_odl.ascx" TagName="edit_odl" TagPrefix="uc1" %>
<%@ Register Src="ricerca_veicolo.ascx" TagName="ricerca_veicolo" TagPrefix="uc1" %>


<div id="div_principale" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Gestione ODL</b>
           </td>
         </tr>
    </table>
<ajaxtoolkit:TabContainer ID="tabPanelOD" runat="server" ActiveTabIndex="1" 
        Width="100%" Visible="true">

  <ajaxtoolkit:TabPanel ID="TabRicerca" runat="server" HeaderText="Ricerca ODL">
    <HeaderTemplate>Ricerca ODL</HeaderTemplate>
    <ContentTemplate>

<div id="div_ricerca" runat="server">
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="ODL:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_ODL" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Targa:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_targa" runat="server" Width="70px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Proprietario:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                
                <asp:DropDownList ID="DropDownProprietario" runat="server" AppendDataBoundItems="True" 
                  DataSourceID="sqlProprietari" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Value="0" Selected="True">Tutti</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="ODL Data Da:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                 <a onclick="Calendar.show(document.getElementById('<%=tx_ODLDataDa.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="tx_ODLDataDa" runat="server" Width="70px"></asp:TextBox>
                     </a>
            <%--    <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_ODLDataDa" ID="CalendarExtender1">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999"  TargetControlID="tx_ODLDataDa" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender1">
                </ajaxtoolkit:MaskedEditExtender>
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="A:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <a onclick="Calendar.show(document.getElementById('<%=tx_ODLDataA.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="tx_ODLDataA" runat="server" Width="70px"></asp:TextBox>
                    </a>
                <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_ODLDataA" ID="CalendarExtender2">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" TargetControlID="tx_ODLDataA" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender2">
                </ajaxtoolkit:MaskedEditExtender>
            </td>
            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Stato ODL:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList_stato_ODL" runat="server" AppendDataBoundItems="True" 
                  DataSourceID="sqlStatoODL" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Value="0" Selected="True">Tutti</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                
            </td>
            <td>
                <asp:Label ID="lblCercaDitta" runat="server" Text="Stazione:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownStazioni" runat="server" AppendDataBoundItems="True" 
                  DataSourceID="sqlStazioni" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Value="0" Selected="True">Tutte</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                
            </td>
            <td>
                
            </td>
            <td>
                
            </td>
        </tr>
        <tr>
            <td colspan="10" align="center">
                <asp:Button ID="bt_cerca_ODL" runat="server" Text="Cerca" />
                <asp:Button ID="bt_stampa" runat="server" Text="Stampa" />
            </td>
        </tr>
    </table>
</div>

<div id="div_elenco_odl" runat="server">
<table border="0" cellpadding="0" cellspacing="0" width="100%" >
      <tr>
        <td>
            <asp:ListView ID="listViewODL" runat="server" DataSourceID="sqlODL" 
                DataKeyNames="id_odl" EnableModelValidation="True">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_id_odl" runat="server" Text='<%# Eval("id_odl") %>' Visible="false" />
                          <asp:Label ID="lb_num_odl" runat="server" Text='<%# Eval("num_odl") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                          <asp:Label ID="lb_targa" runat="server" Text='<%# Eval("targa") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_proprietario" runat="server" Text='<%# Eval("proprietario") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_nome_stazione" runat="server" Text='<%# Eval("nome_stazione") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_data" runat="server" Text='<%# libreria.myFormatta(Eval("data_odl"), "dd/MM/yyyy") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_stato_odl" runat="server" Text='<%# Eval("id_stato_odl") %>' visible="false" />
                          <asp:Label ID="lb_des_stato_odl" runat="server" Text='<%# Eval("des_stato_odl") %>' />
                      </td>
                      <td align="right">
                          <asp:Label ID="lb_preventivo" runat="server" Text='<%# libreria.myFormatta(Eval("preventivo"), "0.00") %>' />
                      </td>
                      <td align="right">
                          <asp:Label ID="lb_importo" runat="server" Text='<%# libreria.myFormatta(Eval("importo"), "0.00") %>' />
                      </td>
                      <td align="center" width="40px" runat="server" visible='<%# lb_AbilitaLente.Text %>'>
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>  
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_id_odl" runat="server" Text='<%# Eval("id_odl") %>' Visible="false" />
                          <asp:Label ID="lb_num_odl" runat="server" Text='<%# Eval("num_odl") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                          <asp:Label ID="lb_targa" runat="server" Text='<%# Eval("targa") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_proprietario" runat="server" Text='<%# Eval("proprietario") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_nome_stazione" runat="server" Text='<%# Eval("nome_stazione") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_data" runat="server" Text='<%# libreria.myFormatta(Eval("data_odl"), "dd/MM/yyyy") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_stato_odl" runat="server" Text='<%# Eval("id_stato_odl") %>' visible="false" />
                          <asp:Label ID="lb_des_stato_odl" runat="server" Text='<%# Eval("des_stato_odl") %>' />
                      </td>
                      <td align="right">
                          <asp:Label ID="lb_preventivo" runat="server" Text='<%# libreria.myFormatta(Eval("preventivo"), "0.00") %>' />
                      </td>
                      <td align="right">
                          <asp:Label ID="lb_importo" runat="server" Text='<%# libreria.myFormatta(Eval("importo"), "0.00") %>' />
                      </td>
                      <td id="Td3" align="center" width="40px" runat="server" visible='<%# lb_AbilitaLente.Text %>'>
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun ODL trovato con i filtri applicati.
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
                                <tr style="color: #FFFFFF" class="sfondo_rosso">
                                      <th>ODL</th>
                                      <th>Targa</th>
                                      <th>Proprietario</th>
                                      <th>Stazione</th>
                                      <th>Data</th>
                                      <th>Stato</th>
                                      <th>Preventivo</th>
                                      <th>Importo</th>
                                      <th id="th_lente"></th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                         <tr>
                              <td id="Td2" runat="server" style="text-align: left;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                                  <asp:DataPager ID="DataPager1" PageSize="30" runat="server"  >
                                      <Fields>
                                      <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"   />

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
          
</div>

    </ContentTemplate>
  </ajaxtoolkit:TabPanel>

  <ajaxtoolkit:TabPanel ID="tabNuovo" runat="server" HeaderText="Apertura ODL">
    <HeaderTemplate>Apertura ODL</HeaderTemplate>
    <ContentTemplate>
        <uc1:ricerca_veicolo id="ricerca_veicolo" runat="server" >
</uc1:ricerca_veicolo>
    </ContentTemplate>
  </ajaxtoolkit:TabPanel>
      
</ajaxtoolkit:TabContainer>

</div>

<div runat="server" id="div_edit_odl" visible="false" >
    <uc1:edit_odl id="edit_odl" runat="server" />
</div>


<asp:Label ID="lb_stato_form" runat="server" Text='0' visible="false"></asp:Label>

<asp:Label ID="lb_id_veicolo" runat="server" Text='0' Visible="false" />

<asp:Label ID="lb_id_evento" runat="server" Text='0' visible="false"></asp:Label>

<asp:Label ID="lb_sqlODL" runat="server" Text='' Visible="false" />
<asp:Label ID="lb_sqlElencoVeicoli" runat="server" Text='' Visible="false" />


<asp:Label ID="lb_AbilitaLente" runat="server" Text='false' Visible="false" />

<asp:Label ID="lb_si_origine" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_si_importo" runat="server" Text='0' Visible="false" />


<asp:SqlDataSource ID="sqlStazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, (codice + ' - ' + nome_stazione) descrizione FROM [stazioni] WITH(NOLOCK) WHERE [attiva] = 1 ORDER BY [codice]">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlStatoODL" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM odl_stato WITH(NOLOCK) WHERE id > 0 ORDER BY id">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlProprietari" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM proprietari_veicoli WITH(NOLOCK) ORDER BY descrizione">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlODL" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlElencoVeicoli" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="">
</asp:SqlDataSource>

