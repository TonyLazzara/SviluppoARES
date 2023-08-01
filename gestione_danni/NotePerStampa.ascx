<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NotePerStampa.ascx.vb" Inherits="gestione_danni_NotePerStampa" %>

<div id="div_edit_nota" runat="server">
    <table border="0" cellpadding="0" cellspacing="2" width="100%">
    <tr>
        <td>
            <asp:ListView ID="ListViewNote" runat="server" DataSourceID="SqlDataSource1" DataKeyNames="id" >
                <ItemTemplate>
                    <tr style="background-color:#DCDCDC;color: #000000;">
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
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr style="">
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
                    </tr>
                </AlternatingItemTemplate>
                <EmptyDataTemplate>
                      <table id="Table1" runat="server" 
                          style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                          <tr>
                              <td>
                                  Non è presente alcuna nota.
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
                                    <tr id="Tr2" runat="server">
                                        <th>Data</th>
                                        <th>Utente</th>
                                        <th>Nota</th>
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
    </table>
</div>

<asp:Label ID="lb_id_tipo" runat="server" Text='0' Visible="false" />
<asp:Label ID="lb_id_documento" runat="server" Text='0' Visible="false" />

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
