<%@ Control Language="VB" AutoEventWireup="false" CodeFile="gestione_danni.ascx.vb" Inherits="gestione_danni_gestione_danni" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<%@ Register Src="gestione_checkin.ascx" TagName="gestione_checkin" TagPrefix="uc1" %>
<%@ Register Src="~/anagrafica/anagrafica_conducenti.ascx" TagName="anagrafica_conducenti" TagPrefix="uc1" %>
<%@ Register Src="~/gestione_fornitori/anagrafica_drivers.ascx" TagName="anagrafica_drivers" TagPrefix="uc1" %>
<%@ Register Src="AccessoriSelezionati.ascx" TagName="AccessoriSelezionati" TagPrefix="uc1" %>
<%@ Register Src="DettagliPagamento.ascx" TagName="DettagliPagamento" TagPrefix="uc1" %>
<%@ Register Src="~/tabelle_pos/scambio_importo.ascx" TagName="scambio_importo" TagPrefix="uc1" %>
<%@ Register Src="ricerca_veicolo.ascx" TagName="ricerca_veicolo" TagPrefix="uc1" %>


<style type="text/css">
    .style18
    {
        width: 480px;
    }
    .style19
    {
        width: 101px;
    }
    .style20
    {
        width: 176px;
    }
    .style21
    {
        width: 76px;
    }
    </style>


<div id="div_ricerca" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Gestione RDS</b>
           </td>
           <td align="right" bgcolor="#444">
                
           </td>
         </tr>
    </table>

<ajaxtoolkit:TabContainer ID="tabPanelOD" runat="server" ActiveTabIndex="0" 
        Width="100%" Visible="true">

  <ajaxtoolkit:TabPanel ID="TabRicerca" runat="server" HeaderText="Ricerca RDS">
    <HeaderTemplate>Ricerca RDS</HeaderTemplate>
    <ContentTemplate>

    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444;border-bottom:none;">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="RDS:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_RDS" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Contratto:" CssClass="testo_bold"></asp:Label>
            </td>
            <td class="style21">
                <asp:TextBox ID="tx_contratto" runat="server" Width="80px"></asp:TextBox>
            </td>
            <td class="style19">
                <asp:Label ID="Label11" runat="server" Text="Proprietario:" CssClass="testo_bold"></asp:Label>
            </td>
            <td class="style20">
                
                <asp:DropDownList ID="DropDownProprietario" runat="server" AppendDataBoundItems="True" 
                  DataSourceID="sqlProprietari" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Value="0" Selected="True">Tutti</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="RDS Data Da:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                 <a onclick="Calendar.show(document.getElementById('<%=tx_RdsDataDa.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="tx_RdsDataDa" runat="server" Width="70px"></asp:TextBox>
                     </a>
           <%--     <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_RdsDataDa" ID="CalendarExtender1">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999"  TargetControlID="tx_RdsDataDa" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender1">
                </ajaxtoolkit:MaskedEditExtender>
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="A:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                 <a onclick="Calendar.show(document.getElementById('<%=tx_RdsDataA.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="tx_RdsDataA" runat="server" Width="70px"></asp:TextBox>
                     </a>
                <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_RdsDataA" ID="CalendarExtender2">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" TargetControlID="tx_RdsDataA" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender2">
                </ajaxtoolkit:MaskedEditExtender>
            </td>
            
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Stato RDS:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList_stato_rds" runat="server" AppendDataBoundItems="True" 
                  DataSourceID="sqlStatoRDS" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Value="0" Selected="True">Tutti</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Targa:" CssClass="testo_bold"></asp:Label>
            </td>
            <td class="style21">
                <asp:TextBox ID="tx_targa" runat="server" Width="70px"></asp:TextBox>
            </td>
            <td class="style19">
                <asp:Label ID="lblCercaDitta" runat="server" Text="Stazioni:" CssClass="testo_bold"></asp:Label>
            </td>
            <td class="style20">
                <asp:DropDownList ID="DropDownStazioni" runat="server" AppendDataBoundItems="True" 
                  DataSourceID="sqlStazioni" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Value="0" Selected="True">Tutte</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Pag. Data Da:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                  <a onclick="Calendar.show(document.getElementById('<%=tx_PagDataDa.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="tx_PagDataDa" runat="server" Width="70px"></asp:TextBox>
                      </a>
           <%--     <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_PagDataDa" ID="CalendarExtender3">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999"  TargetControlID="tx_PagDataDa" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender3">
                </ajaxtoolkit:MaskedEditExtender>
            </td>
            <td>
                <asp:Label ID="Label8" runat="server" Text="A:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                 <a onclick="Calendar.show(document.getElementById('<%=tx_PagDataA.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="tx_PagDataA" runat="server" Width="70px"></asp:TextBox>
                     </a>
         <%--       <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_PagDataA" ID="CalendarExtender4">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999"  TargetControlID="tx_PagDataA" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender4">
                </ajaxtoolkit:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td colspan="">
                
                <asp:Label ID="Label23" runat="server" CssClass="testo_bold" Text="Origine:"></asp:Label>
                
            </td>
            <td colspan="3">
                <asp:DropDownList ID="DropDownOrigine" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlOrigine" 
                    DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Tutte</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style19">
                <asp:Label ID="Label24" runat="server" CssClass="testo_bold" 
                    Text="Scad. Preaut Da:"></asp:Label>
            </td>
            <td class="style20">
                 <a onclick="Calendar.show(document.getElementById('<%=tx_ScadenzaPreautDaData.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="tx_ScadenzaPreautDaData" runat="server" Width="70px"></asp:TextBox>
                     </a>
               <%-- <ajaxtoolkit:CalendarExtender ID="tx_ScadenzaPreautDaData_CalendarExtender" 
                    runat="server" Enabled="True" Format="dd/MM/yyyy" 
                    TargetControlID="tx_ScadenzaPreautDaData">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender ID="tx_ScadenzaPreautDaData_MaskedEditExtender" 
                    runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="99/99/9999" MaskType="Date" TargetControlID="tx_ScadenzaPreautDaData">
                </ajaxtoolkit:MaskedEditExtender>
                <asp:Label ID="Label25" runat="server" CssClass="testo_bold" Text="A:"></asp:Label>
                <a onclick="Calendar.show(document.getElementById('<%=tx_ScadenzaPreautAData.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="tx_ScadenzaPreautAData" runat="server" Width="70px"></asp:TextBox>
                    </a>
           <%--     <ajaxtoolkit:CalendarExtender ID="tx_ScadenzaPreautAData_CalendarExtender" runat="server" 
                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="tx_ScadenzaPreautAData">
                </ajaxtoolkit:CalendarExtender>--%>

                <ajaxtoolkit:MaskedEditExtender ID="tx_ScadenzaPreautAData_MaskedEditExtender" 
                    runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                    Mask="99/99/9999" MaskType="Date" TargetControlID="tx_ScadenzaPreautAData">
                </ajaxtoolkit:MaskedEditExtender>
            </td>
            <td>
                <asp:Label ID="Label21" runat="server" Text="Perizia Data Da:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                  <a onclick="Calendar.show(document.getElementById('<%=tx_PeriziaDataDa.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="tx_PeriziaDataDa" runat="server" Width="70px"></asp:TextBox>
                      </a>
               <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_PeriziaDataDa" ID="CalendarExtender5">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999"  TargetControlID="tx_PeriziaDataDa" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender5">
                </ajaxtoolkit:MaskedEditExtender>
            </td>
            <td>
                <asp:Label ID="Label22" runat="server" Text="A:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <a onclick="Calendar.show(document.getElementById('<%=tx_PeriziaDataA.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="tx_PeriziaDataA" runat="server" Width="70px"></asp:TextBox>
                    </a>
             <%--   <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="tx_PeriziaDataA" ID="CalendarExtender6">
                </ajaxtoolkit:CalendarExtender>--%>

                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999"  TargetControlID="tx_PeriziaDataA" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" ID="MaskedEditExtender6">
                </ajaxtoolkit:MaskedEditExtender>
            </td>
        </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444; border-top:none;">
        <tr>
            <td class="style18">
                &nbsp;<b>Num. Record</b>
                <asp:Label ID="lb_NumRecord" runat="server" Text="0" ></asp:Label>
                &nbsp;&nbsp;<b>Stimato</b>
                <asp:Label ID="lb_TotStimato" runat="server" Text="0,00" ></asp:Label>
                &nbsp;&nbsp;<b>Incassato</b>
                <asp:Label ID="lb_TotIncassato" runat="server" Text="0,00" ></asp:Label>
            </td>
            <td align="right">
                <asp:Button ID="bt_cerca_rds" runat="server" Text="Cerca" />
                <asp:Button ID="bt_stampa" runat="server" Text="Stampa" />
            </td>
        </tr>
    </table>	

<div id="div_elenco_eventi" runat="server" visible="False">
<table border="0" cellpadding="0" cellspacing="0" width="100%" >
      <tr>
        <td>
            <asp:ListView ID="listViewEventiDanni" runat="server" 
                DataSourceID="sqlEventiDannoTarga" DataKeyNames="id_rds">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
                      <td>
                          <asp:Label ID="lb_id_evento_apertura" runat="server" Text='<%# Eval("id_evento") %>' Visible="false" />
                          <asp:Label ID="lb_id_rds" runat="server" Text='<%# Eval("id_rds") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_targa" runat="server" Text='<%# Eval("targa") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_proprietario" runat="server" Text='<%# Eval("proprietario") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_nome_stazione" runat="server" Text='<%# Eval("nome_stazione") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_data" runat="server" Text='<%# libreria.myFormatta(Eval("data_rds"), "dd/MM/yyyy") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_tipo_documento_apertura" runat="server" Text='<%# Eval("id_tipo_documento_apertura") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_tipo_documento_apertura" runat="server" Text='<%# Eval("des_id_tipo_documento_apertura") %>' /><asp:Label ID="lb_id_documento_apertura" runat="server" Text='<%# Eval("id_documento_apertura") %>' />
                          <asp:Label ID="lb_num_crv_apertura" runat="server" Text='<%# Eval("num_crv") %>' Visible="false" />
                      </td>
                      <td>
                          <asp:Label ID="lb_stato_rds" runat="server" Text='<%# Eval("stato_rds") %>' visible="false" />
                          <asp:Label ID="lb_des_stato_rds" runat="server" Text='<%# Eval("des_stato_rds") %>' />
                      </td>
                      <td align="right">                                                                
                            <asp:Label ID="lb_importo" runat="server" Text='<%# libreria.myFormatta(Eval("totale"), "0.00") %>' />                            
                      </td>
                      <td>
                          <asp:Label ID="lb_scadenza_preaut" runat="server" Text='<%# libreria.myFormatta(Eval("scadenza_preaut"), "dd/MM/yyyy") %>' visible="false" />
                          <asp:Label ID="lb_importoIncasso" runat="server" Text='<%# libreria.myFormatta(Eval("incasso"), "0.00") %>' />                                                    
                      </td>
                      <td align="center" width="40px" runat="server" visible='<%# lb_AbilitaLente.Text %>'>
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                      <!-- Tony 20/04/2023 -->
                      <td align="center" width="40px" runat="server" visible='<%# lb_AbilitaPagamento.Text %>'>
                          <asp:ImageButton ID="pagamento" runat="server" ImageUrl="/images/euro.png" style="width: 16px" CommandName="pagamento" />
                      </td>
                      <!-- FINE Tony -->
                  </tr>  
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
                      <td>
                          <asp:Label ID="lb_id_evento_apertura" runat="server" Text='<%# Eval("id_evento") %>' Visible="false" />
                          <asp:Label ID="lb_id_rds" runat="server" Text='<%# Eval("id_rds") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_targa" runat="server" Text='<%# Eval("targa") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_proprietario" runat="server" Text='<%# Eval("proprietario") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_nome_stazione" runat="server" Text='<%# Eval("nome_stazione") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_data" runat="server" Text='<%# libreria.myFormatta(Eval("data_rds"), "dd/MM/yyyy") %>' />
                      </td>
                      <td>
                          <asp:Label ID="lb_id_tipo_documento_apertura" runat="server" Text='<%# Eval("id_tipo_documento_apertura") %>' Visible="false" />
                          <asp:Label ID="lb_des_id_tipo_documento_apertura" runat="server" Text='<%# Eval("des_id_tipo_documento_apertura") %>' /><asp:Label ID="lb_id_documento_apertura" runat="server" Text='<%# Eval("id_documento_apertura") %>' />
                          <asp:Label ID="lb_num_crv_apertura" runat="server" Text='<%# Eval("num_crv") %>' Visible="false" />
                      </td>
                      <td>
                          <asp:Label ID="lb_stato_rds" runat="server" Text='<%# Eval("stato_rds") %>' visible="false" />
                          <asp:Label ID="lb_des_stato_rds" runat="server" Text='<%# Eval("des_stato_rds") %>' />
                      </td>
                      <td align="right">                                                                
                            <asp:Label ID="lb_importo" runat="server" Text='<%# libreria.myFormatta(Eval("totale"), "0.00") %>' />                            
                      </td>
                      <td>
                          <asp:Label ID="lb_scadenza_preaut" runat="server" Text='<%# libreria.myFormatta(Eval("scadenza_preaut"), "dd/MM/yyyy") %>' visible="false" />
                          <asp:Label ID="lb_importoIncasso" runat="server" Text='<%# libreria.myFormatta(Eval("incasso"), "0.00") %>' />                                                    
                      </td>
                      <td align="center" width="40px" runat="server" visible='<%# lb_AbilitaLente.Text %>'>
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                      <td align="center" width="40px" runat="server" visible='<%# lb_AbilitaPagamento.Text %>'>
                          <asp:ImageButton ID="pagamento" runat="server" ImageUrl="/images/euro.png" style="width: 16px" CommandName="pagamento"/>
                      </td>
                  </tr> 
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun RDS aperto con i filtri applicati.
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
                                      <th>RDS</th>
                                      <th>Targa</th>
                                      <th>Proprietario</th>
                                      <th>Stazione</th>
                                      <th>Data</th>
                                      <th>Evento</th>
                                      <th>Stato</th>
                                      <th>                                       
                                        <%--<% If DropDownList_stato_rds.SelectedValue = 6 Then%>
                                            <asp:Label ID="lblColonnaStimatoIncassato" runat="server" Text="Incassato"></asp:Label>
                                        <% Else%>                                        
                                            <asp:Label ID="Label19" runat="server" Text="Stimato"></asp:Label>
                                        <% End If%>--%>
                                        Stimato
                                      </th>
                                      <th>Incassato</th>
                                      <th id="th_lente"></th>
                                      <th id="th_pagamento"></th>
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                         <tr>
                              <td id="Td2" runat="server" style="text-align: left;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
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

  <ajaxtoolkit:TabPanel ID="tabNuovo" runat="server" HeaderText="Apertura RDS">
    <HeaderTemplate>Apertura RDS</HeaderTemplate>
    <ContentTemplate>
        <uc1:ricerca_veicolo id="ricerca_veicolo" runat="server" >
</uc1:ricerca_veicolo>
    </ContentTemplate>
  </ajaxtoolkit:TabPanel>

  <ajaxtoolkit:TabPanel ID="tabStatoUsoVeicolo" runat="server" HeaderText="Stato Veicolo">
    <HeaderTemplate>Stato Veicolo</HeaderTemplate>
    <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444;">
            <tr>
                <td class="style21">
                <asp:Label ID="Label18" runat="server" Text="Targa:" CssClass="testo_bold"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCercaTargaStato" runat="server" Width="70px"></asp:TextBox>
                    &nbsp;
                    <asp:Button ID="bt_vedi_stato" runat="server" Text="Vedi Stato Veicolo" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
  </ajaxtoolkit:TabPanel>
      
</ajaxtoolkit:TabContainer>	

</div>

<div id="div_rds_generico" runat="server" visible="false">
    <uc1:gestione_checkin id="gestione_checkin_rds_generico" runat="server" />
</div>

<div id="div_targa" runat="server" visible="false">
<table border="0" cellpadding="0" cellspacing="0" width="1024px" >
    <tr>
        <td  align="left" style="color: #FFFFFF;background-color:#444;"> 
            &nbsp;<b>Numero Documento: </b>
            <asp:Label ID="lb_num_documento1" runat="server" Text="" ></asp:Label>
            <asp:LinkButton ID="lb_num_documento" runat="server" CssClass="testo_bold" style="color: #FFFFFF" Text="" />
            &nbsp;                    
                         
            &nbsp;<b>Targa: </b><asp:Label ID="lb_targa" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Modello: </b><asp:Label ID="lb_modello" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Stazione: </b><asp:Label ID="lb_stazione" runat="server" Text="" ></asp:Label>
            &nbsp;&nbsp;<b>Km: </b><asp:Label ID="lb_km" runat="server" Text="" ></asp:Label>    
        </td>
    </tr>
</table>
</div>

<div id="div_dettaglio_rds" runat="server" visible="false">

<ajaxtoolkit:TabContainer ID="tabPanelRDS" runat="server" ActiveTabIndex="0" Width="100%" Visible="true">

<ajaxtoolkit:TabPanel ID="tabEventoDanni" runat="server" HeaderText="Evento Danni">
  <HeaderTemplate>Evento Danni</HeaderTemplate>  
    <ContentTemplate>
        <uc1:gestione_checkin id="gestione_checkin" runat="server" />
    </ContentTemplate>
</ajaxtoolkit:TabPanel>
  
<ajaxtoolkit:TabPanel ID="tabConducente" runat="server" HeaderText="Conducente">
  <HeaderTemplate>Conducenti</HeaderTemplate> 
  <ContentTemplate>
    <div id="div_anagrafica_conducenti_1" runat="server">
        <uc1:anagrafica_conducenti ID="anagrafica_conducenti_1" runat="server" /> 
    </div>
    <div id="div_anagrafica_conducenti_2" runat="server">
        <uc1:anagrafica_conducenti ID="anagrafica_conducenti_2" runat="server" /> 
    </div>
    <div id="div_drivers" runat="server">
        <uc1:anagrafica_drivers ID="anagrafica_drivers" runat="server" /> 
    </div>  
  </ContentTemplate>
</ajaxtoolkit:TabPanel>
  
<ajaxtoolkit:TabPanel ID="tabTariffa" runat="server" HeaderText="Tariffa">
  <HeaderTemplate>Tariffa</HeaderTemplate>
  <ContentTemplate>
    <uc1:AccessoriSelezionati ID="AccessoriSelezionati" runat="server" />  	
  </ContentTemplate>
</ajaxtoolkit:TabPanel>

<ajaxtoolkit:TabPanel ID="TabStatoVeicolo" runat="server" HeaderText="Stato Veicolo">
  <HeaderTemplate>Stato Veicolo</HeaderTemplate>     
    <ContentTemplate>
            <uc1:gestione_checkin id="gestione_checkin_storico" runat="server" />
    </ContentTemplate>
</ajaxtoolkit:TabPanel>

</ajaxtoolkit:TabContainer>    
</div>

<div runat="server" id="div_dettaglio_pagamento" visible="false" >
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
        <tr>
            <td colspan="8">
                <asp:Label ID="Label4" runat="server" Text="RDS:" CssClass="testo_bold"></asp:Label>
                <asp:Label ID="lb_num_rds" runat="server" Text='' CssClass="testo_bold"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label20" runat="server" Text="Stimato:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_stimato" runat="server" Width="80px" ReadOnly="true" style="text-align:right" ></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label5" runat="server" Text="IVA:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_iva" runat="server" Width="80px" ReadOnly="true" style="text-align:right"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Spese Postali:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_spese_postali" runat="server" Width="80px" ReadOnly="true" style="text-align:right"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label13" runat="server" Text="Totale RDS:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_totale_rds" runat="server" Width="80px" ReadOnly="true" style="text-align:right"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Label ID="Label14" runat="server" Text="Contratto:" CssClass="testo_bold"></asp:Label>
                <asp:Label ID="lb_num_contratto_pagamento" runat="server" Text="" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label15" runat="server" Text="Totale RA:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_totale_contratto" runat="server" Width="80px" ReadOnly="true" style="text-align:right"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="Label17" runat="server" Text="Totale:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_totale_ra_rds" runat="server" Width="80px" ReadOnly="true" style="text-align:right"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="8">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width:25%">
                &nbsp;
            </td>
            <td colspan="6" align="center">
                <asp:Button ID="bt_nuova_preautorizzazione" runat="server" Text="Nuova Preautorizzazione" />
                <asp:Button ID="bt_chiudi_pagamento" runat="server" Text="Chiudi" />
            </td>
            <td align="right" style="width:25%">
                <asp:Label ID="Label16" runat="server" Text="Tipo Pagamento:" 
                    CssClass="testo_bold" Visible="False"></asp:Label>
                <asp:DropDownList ID="DropDownSimulazione" runat="server" Visible="False">
                    <asp:ListItem Value="True" Selected="True">Simulazione</asp:ListItem>
                    <asp:ListItem Value="False">Reale</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>    
        </table>    
            </td>
        </tr>
        
    </table>
     <uc1:DettagliPagamento ID="DettagliPagamento" runat="server" />
   


</div>

<div runat="server" id="div_pagamento" visible="false" >
    <uc1:scambio_importo ID="Scambio_Importo1" runat="server"  />
</div>


<asp:Label ID="lb_stato_form" runat="server" Text='0' visible="false"></asp:Label>

<asp:Label ID="lb_id_veicolo" runat="server" Text='0' Visible="false" />

<asp:Label ID="lb_id_evento" runat="server" Text='0' visible="false"></asp:Label>

<asp:Label ID="lb_sqlEventiDannoTarga" runat="server" Text='' Visible="false" />

<asp:Label ID="lb_id_tipo_documento" runat="server" Text='' Visible="false" />
<asp:Label ID="lb_id_documento" runat="server" Text='' Visible="false" />
<asp:Label ID="lb_num_crv" runat="server" Text='' Visible="false" />

<asp:Label ID="lb_AbilitaLente" runat="server" Text='false' Visible="false" />
<asp:Label ID="lb_AbilitaPagamento" runat="server" Text='false' Visible="false" />

<asp:Label ID="lb_si_origine" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_si_importo" runat="server" Text='0' Visible="false" />

<asp:SqlDataSource ID="sqlStazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, (codice + ' - ' + nome_stazione) descrizione FROM [stazioni] WITH(NOLOCK) WHERE [attiva] = 1 ORDER BY [codice]">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlStatoRDS" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM veicoli_stato_rds WITH(NOLOCK) WHERE attivo =1 ORDER BY descrizione">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlProprietari" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM proprietari_veicoli WITH(NOLOCK) ORDER BY descrizione">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlEventiDannoTarga" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlEventiDannoStazione" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlOrigine" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione + ' (' + codice_sintetico + ')' descrizione FROM veicoli_tipo_documento_apertura_danno WITH(NOLOCK) where id > 0 and id <> 6 and id <> 100 and id <> 103 and id <> 107 and id <> 113 ORDER BY id">
</asp:SqlDataSource>

