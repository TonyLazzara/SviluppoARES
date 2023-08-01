<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ricerca_veicolo.ascx.vb" Inherits="gestione_danni_ricerca_veicolo" %>


<div id="div_ricerca_targa" runat="server">
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
        <tr>
            <td>
                <asp:Label ID="Label18" runat="server" Text="Targa:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_targa_x_apertura" runat="server" Width="70px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label19" runat="server" Text="Proprietario:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownProprietario_x_apertura" runat="server" AppendDataBoundItems="True" 
                  DataSourceID="sqlProprietari" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Value="0" Selected="True">Tutti</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label23" runat="server" Text="Stazione:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownStazioni_x_apertura" runat="server" AppendDataBoundItems="True" 
                  DataSourceID="sqlStazioni" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Value="0" Selected="True">Tutte</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label24" runat="server" Text="Stato Danni:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownStatodanni" runat="server">
                    <asp:ListItem Value="0" Selected="True">Tutti</asp:ListItem>
                    <asp:ListItem Value="1" >Aperti</asp:ListItem>
                    <asp:ListItem Value="2" >Fermo Tecnico</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label25" runat="server" Text="Tipo Danno:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownTipoRecordDanno" runat="server">
                    <asp:ListItem Value="0" Selected="True">Tutti</asp:ListItem>
                    <asp:ListItem Value="1">Carrozzeria</asp:ListItem>
                    <asp:ListItem Value="2">Meccanico</asp:ListItem>
                    <asp:ListItem Value="3">Elettrico</asp:ListItem>
                    <asp:ListItem Value="4">Dotazione</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label26" runat="server" Text="Tipologia:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownTipologia" runat="server">
                    <asp:ListItem Value="0" Selected="True">Tutti</asp:ListItem>
                    <asp:ListItem Value="1">Bloccante (Gomme, Vetri,...)</asp:ListItem>
                    <asp:ListItem Value="2">Non Bloccante</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="6" align="center">
                <asp:Button ID="bt_cerca_veicoli" runat="server" Text="Cerca" />
            </td>
        </tr>
    </table>
</div>


<div id="div_elenco_veicoli" runat="server">
<table border="0" cellpadding="0" cellspacing="0" width="100%" >
      <tr>
        <td>
            <asp:ListView ID="listViewElencoVeicoli" runat="server" DataSourceID="sqlElencoVeicoli" DataKeyNames="id_veicolo">
              <ItemTemplate>
                  <tr style="background-color:#DCDCDC; color: #000000;">
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
                      <td id="Td5" align="center" width="40px" runat="server">
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>  
              </ItemTemplate>
              <AlternatingItemTemplate>
                  <tr style="">
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
                      <td id="Td5" align="center" width="40px" runat="server">
                          <asp:ImageButton ID="lente" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="lente"/>
                      </td>
                  </tr>
              </AlternatingItemTemplate>
              <EmptyDataTemplate>
                  <table id="Table1" runat="server" style="">
                      <tr style="">
                          <td>
                              Nessun veicolo trovato con i filtri applicati.
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
                                      <th>Targa</th>
                                      <th>Proprietario</th>
                                      <th>Stazione</th>
                                      <th></th>
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


<asp:Label ID="lb_sqlElencoVeicoli" runat="server" Text='' Visible="false" />

<asp:SqlDataSource ID="sqlElencoVeicoli" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="">
</asp:SqlDataSource>