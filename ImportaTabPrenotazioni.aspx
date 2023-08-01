<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ImportaTabPrenotazioni.aspx.vb" Inherits="ImportaTabPrenotazioni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ListView ID="listaPrenotazioni" runat="server" DataKeyNames="id" DataSourceID="SqlElencoPrenotazioni">
        <ItemTemplate>
            <tr style="background-color:#DCDCDC;color: #000000;">
                <td>
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                    <asp:Label ID="targaLabel" runat="server" Text='<%# Eval("targa") %>' />
                </td>
                <td>
                    <asp:Label ID="marcaLabel" runat="server" Text='<%# Eval("marca") %>' />
                </td>
                <td>
                    <asp:Label ID="modelloLabel" runat="server" Text='<%# Eval("modello") %>' />
                </td>
                <td>
                    <asp:Label ID="coloreLabel" runat="server" Text='<%# Eval("colore") %>' />
                </td>
                <td>
                    <asp:Label ID="proprietarioLabel" runat="server" Text='<%# Eval("proprietario") %>' />
                </td>
                <td align="center">
                    <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" />
                </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr>
                <td>
                    <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                    <asp:Label ID="targaLabel" runat="server" Text='<%# Eval("targa") %>' />
                </td>
                <td>
                    <asp:Label ID="marcaLabel" runat="server" Text='<%# Eval("marca") %>' />
                </td>
                <td>
                    <asp:Label ID="modelloLabel" runat="server" Text='<%# Eval("modello") %>' />
                </td>
                <td>
                    <asp:Label ID="coloreLabel" runat="server" Text='<%# Eval("colore") %>' />
                </td>
                <td>
                    <asp:Label ID="proprietarioLabel" runat="server" Text='<%# Eval("proprietario") %>' />
                </td>
                <td align="center">
                    <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EmptyDataTemplate>
            <table id="Table1" runat="server" 
                style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                <tr>
                    <td>Non è stato restituito alcun dato.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <LayoutTemplate>
            <table id="Table2" runat="server" width="100%">
                <tr id="Tr1" runat="server">
                    <td id="Td1" runat="server">
                        <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%"  
                            style=" background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                <th id="Th1" runat="server" align="left">
                                    Targa</th>
                                <th id="Th2" runat="server" align="left">
                                    Marca</th>
                                <th id="Th3" runat="server" align="left">
                                    Modello</th>
                                <th id="Th5" runat="server" align="left">
                                    Colore</th>
                                <th id="Th6" runat="server" align="left">
                                    Proprietario</th>
                                <th></th>   
                            </tr>
                            <tr ID="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="Tr3" runat="server">
                    <td id="Td2" runat="server" style="text-align: left;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
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
    </asp:ListView>
    <asp:SqlDataSource ID="SqlElencoPrenotazioni" runat="server"
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"         
        SelectCommand="select 1 as id, 'MioTarga' as Targa, 'MiaMarca' as marca, 'MioModello' as modello, 'MioColore' as colore, 'MioPropietraio' as proprietario"></asp:SqlDataSource>
    <p>
    </p>
</asp:Content>

