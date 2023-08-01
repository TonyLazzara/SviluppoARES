<%@ Control Language="VB" AutoEventWireup="false" CodeFile="gestione_note.ascx.vb" Inherits="gestione_danni_gestione_note" %>

<style type="text/css">
    .style1
    {
        width: 653px;
    }
</style>

<div id="div_edit_nota" runat="server" style="font-family:Verdana, Geneva, Tahoma, sans-serif;">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;<asp:Label ID="lb_intestazione_note" runat="server" Text='' ></asp:Label>&nbsp;</b>
           </td>
         </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
        <tr id="tr_salva_nota" runat="server">
            <td>
              <asp:TextBox ID="tx_nota" runat="server" Width="700px" Height="38px" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td align="right"  valign="top">
               <asp:Button ID="bt_Salva" runat="server" Text="Salva Nuova Nota" ValidationGroup="Salva" />
                  <asp:Button ID="bt_Annulla" runat="server" Text="Annulla" Visible="false" />
                  <asp:Label ID="lb_id" runat="server" Text='0' Visible="false" />
            </td>
        </tr>
    <tr>
        <td colspan="2">
            <asp:ListView ID="ListViewNote" runat="server" DataSourceID="SqlDataSource1" DataKeyNames="id" >
                <ItemTemplate>
                    <tr style="background-color:#DCDCDC;color: #000000;font-size:12px;font-family:Verdana, Geneva, Tahoma, sans-serif;">
                        <td valign="top" style="width:175px">
                            <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                            <asp:Label ID="lb_data_creazione" runat="server" Text='<%# Eval("data_creazione")  %>' />
                        </td>
                        <td valign="top" style="width:200px">
                            <asp:Label ID="lb_id_utente" runat="server" Text='<%# Eval("id_utente") %>' Visible="false" />
                            <asp:Label ID="lb_utente" runat="server" Text='<%# Eval("utente") %>' />
                        </td>
                        <td valign="top">
                            <asp:Label ID="lb_nota" runat="server" Text='<%# Eval("nota") %>' />
                        </td>
                        <td valign="top" id="td_lente" runat="server" align="center" width="40px" Visible='<%# lb_lente.Text %>'>
                            <asp:ImageButton ID="lente" runat="server" ImageUrl="~/images/lente.png" style="width: 16px" CommandName="lente" />
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr >
                        <td valign="top" style="width:175px">
                            <asp:Label ID="lb_id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                            <asp:Label ID="lb_data_creazione" runat="server" Text='<%# Eval("data_creazione")  %>' />
                        </td>
                        <td valign="top" style="width:200px">
                            <asp:Label ID="lb_id_utente" runat="server" Text='<%# Eval("id_utente") %>' Visible="false" />
                            <asp:Label ID="lb_utente" runat="server" Text='<%# Eval("utente") %>' />
                        </td>
                        <td valign="top">
                            <asp:Label ID="lb_nota" runat="server" Text='<%# Eval("nota") %>' />
                        </td>
                        <td valign="top" id="td_lente" runat="server" align="center" width="40px" Visible='<%# lb_lente.Text %>'>
                            <asp:ImageButton ID="lente" runat="server" ImageUrl="~/images/lente.png" style="width: 16px" CommandName="lente" />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <EmptyDataTemplate>
                      <table id="Table1" runat="server" 
                          style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;
                                border-width:1px;font-size:12px;font-family:Verdana, Geneva, Tahoma, sans-serif;">
                          <tr>
                              <td>
                                  Non è presente alcuna nota.
                              </td>
                          </tr>
                      </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table id="Table1" runat="server" width="100%" >
                        <tr id="Tr1" runat="server">
                            <td id="Td1" runat="server">
                                <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%" 
                                    style="font-size:12px;background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;
                                        border-width:1px;font-family: Verdana, Geneva, Tahoma, sans-serif;">
                                    <tr id="Tr2" runat="server" style="color: #FFFFFF;line-height:24px;font-family: Verdana, Geneva, Tahoma, sans-serif;font-size:12px;background-color:#19191b;"> 
                                        <th>Data</th>
                                        <th>Utente</th>
                                        <th>Nota</th>
                                        <th id="th_lente" runat="server"></th>
                                    </tr>
                                    <tr ID="itemPlaceholder" runat="server" >
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="Tr3" runat="server">
                              <td id="Td2" runat="server" style="font-size:12px;text-align: left;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                                  <asp:DataPager ID="DataPager1" PageSize="150" runat="server"  >
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

<asp:Label ID="lb_id_tipo" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_id_documento" runat="server" Text='0' Visible="false" />

<asp:Label ID="lb_lente" runat="server" Text='false' Visible="false" />


<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"
    SelectCommand="SELECT n.*, (o.cognome + ' ' + o.nome) utente FROM note n WITH(NOLOCK)
        INNER JOIN operatori o WITH(NOLOCK) ON o.id = n.id_utente
        WHERE n.id_tipo = @id_tipo
        AND n.id_documento = @id_documento
        ORDER BY n.data_creazione DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="lb_id_tipo" Name="id_tipo" PropertyName="Text" Type="Int32"  />
            <asp:ControlParameter ControlID="lb_id_documento" Name="id_documento" PropertyName="Text" Type="Int32"  />
        </SelectParameters>
</asp:SqlDataSource>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" 
    DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
    ValidationGroup="Salva" />

<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ControlToValidate="tx_nota" ErrorMessage="Nessuna nota immessa." 
    Font-Size="0pt" SetFocusOnError="True" ValidationGroup="Salva"></asp:RequiredFieldValidator> 
