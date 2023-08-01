<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ElencoTariffe.aspx.vb" Inherits="ElencoTariffe" title="Pagina senza titolo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:SqlDataSource ID="sqlTariffe" runat="server" ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"         
        SelectCommand="SELECT * FROM [tariffe]">
   </asp:SqlDataSource>
    <table border="0" cellpadding="2" cellspacing="2" width="1024px">
        <tr>
          <td align="left" >
               Tariffa</td>
          <td align="left" >
               Validità</td>
          <td align="left" >
               Stazione</td>
          <td align="left" >
               &nbsp;</td>
          <td align="left" >
               &nbsp;</td>
          <td align="left" >
               &nbsp;</td>
          <td align="left" >
               &nbsp;</td>
          <td align="left" >
               &nbsp;</td>
        </tr>
        <tr>
          <td align="left" >
               <asp:TextBox ID="txtCercaTarga" runat="server" Width="85px"></asp:TextBox>
          </td>
          <td align="left" >
            
               <asp:TextBox ID="txtDataImmatricolazione" runat="server" Width="70px"></asp:TextBox>
               
          </td>
          <td align="left" class="style5">
            
               &nbsp;</td>
          <td align="left" class="style6">
            
               &nbsp;</td>
          <td align="left" class="style30">
            
            <asp:Button ID="btnCercaVeicolo" runat="server" Text="Cerca" BackColor="#369061" 
                  Font-Bold="True" Font-Size="Small" ForeColor="White" />  
                         
           
           
            </td>
          <td align="left" class="style29">
            
               &nbsp;</td>
          <td align="left" class="style28">
            
               &nbsp;</td>
          <td align="left" class="style2">
              &nbsp;</td>
        </tr>
    </table>
    <br /><br />
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="id" 
        DataSourceID="sqlTariffe">
        <ItemTemplate>
            <tr style="background-color:#DCDCDC;color: #000000;">
                <td>
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                </td>
                <td>
                    <asp:Label ID="data_creazioneLabel" runat="server" 
                        Text='<%# Eval("data_creazione") %>' />
                </td>
                <td>
                    <asp:Label ID="codiceLabel" runat="server" Text='<%# Eval("codice") %>' />
                </td>
                <td>
                    <asp:Label ID="validita_dalLabel" runat="server" 
                        Text='<%# Eval("validita_dal") %>' />
                </td>
                <td>
                    <asp:Label ID="validita_alLabel" runat="server" 
                        Text='<%# Eval("validita_al") %>' />
                </td>
                <td>
                    <asp:Label ID="id_tipotariffaLabel" runat="server" 
                        Text='<%# Eval("id_tipotariffa") %>' />
                </td>
                <td>
                    <asp:Label ID="id_tempo_kmLabel" runat="server" 
                        Text='<%# Eval("id_tempo_km") %>' />
                </td>
                <td>
                    <asp:Label ID="id_condizioniLabel" runat="server" 
                        Text='<%# Eval("id_condizioni") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr style="background-color:#FFF8DC;">
                <td>
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                </td>
                <td>
                    <asp:Label ID="data_creazioneLabel" runat="server" 
                        Text='<%# Eval("data_creazione") %>' />
                </td>
                <td>
                    <asp:Label ID="codiceLabel" runat="server" Text='<%# Eval("codice") %>' />
                </td>
                <td>
                    <asp:Label ID="validita_dalLabel" runat="server" 
                        Text='<%# Eval("validita_dal") %>' />
                </td>
                <td>
                    <asp:Label ID="validita_alLabel" runat="server" 
                        Text='<%# Eval("validita_al") %>' />
                </td>
                <td>
                    <asp:Label ID="id_tipotariffaLabel" runat="server" 
                        Text='<%# Eval("id_tipotariffa") %>' />
                </td>
                <td>
                    <asp:Label ID="id_tempo_kmLabel" runat="server" 
                        Text='<%# Eval("id_tempo_km") %>' />
                </td>
                <td>
                    <asp:Label ID="id_condizioniLabel" runat="server" 
                        Text='<%# Eval("id_condizioni") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" 
                style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                <tr>
                    <td>
                        No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                        Text="Insert" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                        Text="Clear" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:TextBox ID="data_creazioneTextBox" runat="server" 
                        Text='<%# Bind("data_creazione") %>' />
                </td>
                <td>
                    <asp:TextBox ID="codiceTextBox" runat="server" Text='<%# Bind("codice") %>' />
                </td>
                <td>
                    <asp:TextBox ID="validita_dalTextBox" runat="server" 
                        Text='<%# Bind("validita_dal") %>' />
                </td>
                <td>
                    <asp:TextBox ID="validita_alTextBox" runat="server" 
                        Text='<%# Bind("validita_al") %>' />
                </td>
                <td>
                    <asp:TextBox ID="id_tipotariffaTextBox" runat="server" 
                        Text='<%# Bind("id_tipotariffa") %>' />
                </td>
                <td>
                    <asp:TextBox ID="id_tempo_kmTextBox" runat="server" 
                        Text='<%# Bind("id_tempo_km") %>' />
                </td>
                <td>
                    <asp:TextBox ID="id_condizioniTextBox" runat="server" 
                        Text='<%# Bind("id_condizioni") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%" 
                                      style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                <th runat="server">
                                    id</th>
                                <th runat="server">
                                    data_creazione</th>
                                <th runat="server">
                                    codice</th>
                                <th runat="server">
                                    validita_dal</th>
                                <th runat="server">
                                    validita_al</th>
                                <th runat="server">
                                    id_tipotariffa</th>
                                <th runat="server">
                                    id_tempo_km</th>
                                <th runat="server">
                                    id_condizioni</th>
                            </tr>
                            <tr ID="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                 <tr id="Tr3" runat="server">
                              <td id="Td2" runat="server" style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                                  <asp:DataPager ID="DataPager1" runat="server" >
                                      <Fields>
                                          <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                              ShowLastPageButton="True" />
                                      </Fields>
                                  </asp:DataPager>
                              </td>
                          </tr>
            </table>
        </LayoutTemplate>
        <EditItemTemplate>
            <tr style="background-color:#008A8C;color: #FFFFFF;">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                        Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                        Text="Cancel" />
                </td>
                <td>
                    <asp:Label ID="idLabel1" runat="server" Text='<%# Eval("id") %>' />
                </td>
                <td>
                    <asp:TextBox ID="data_creazioneTextBox" runat="server" 
                        Text='<%# Bind("data_creazione") %>' />
                </td>
                <td>
                    <asp:TextBox ID="codiceTextBox" runat="server" Text='<%# Bind("codice") %>' />
                </td>
                <td>
                    <asp:TextBox ID="validita_dalTextBox" runat="server" 
                        Text='<%# Bind("validita_dal") %>' />
                </td>
                <td>
                    <asp:TextBox ID="validita_alTextBox" runat="server" 
                        Text='<%# Bind("validita_al") %>' />
                </td>
                <td>
                    <asp:TextBox ID="id_tipotariffaTextBox" runat="server" 
                        Text='<%# Bind("id_tipotariffa") %>' />
                </td>
                <td>
                    <asp:TextBox ID="id_tempo_kmTextBox" runat="server" 
                        Text='<%# Bind("id_tempo_km") %>' />
                </td>
                <td>
                    <asp:TextBox ID="id_condizioniTextBox" runat="server" 
                        Text='<%# Bind("id_condizioni") %>' />
                </td>
            </tr>
        </EditItemTemplate>
        <SelectedItemTemplate>
            <tr style="background-color:#008A8C;font-weight: bold;color: #FFFFFF;">
                <td>
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' />
                </td>
                <td>
                    <asp:Label ID="data_creazioneLabel" runat="server" 
                        Text='<%# Eval("data_creazione") %>' />
                </td>
                <td>
                    <asp:Label ID="codiceLabel" runat="server" Text='<%# Eval("codice") %>' />
                </td>
                <td>
                    <asp:Label ID="validita_dalLabel" runat="server" 
                        Text='<%# Eval("validita_dal") %>' />
                </td>
                <td>
                    <asp:Label ID="validita_alLabel" runat="server" 
                        Text='<%# Eval("validita_al") %>' />
                </td>
                <td>
                    <asp:Label ID="id_tipotariffaLabel" runat="server" 
                        Text='<%# Eval("id_tipotariffa") %>' />
                </td>
                <td>
                    <asp:Label ID="id_tempo_kmLabel" runat="server" 
                        Text='<%# Eval("id_tempo_km") %>' />
                </td>
                <td>
                    <asp:Label ID="id_condizioniLabel" runat="server" 
                        Text='<%# Eval("id_condizioni") %>' />
                </td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
</asp:Content>

