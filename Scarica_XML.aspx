<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Scarica_XML.aspx.vb" Inherits="Scarica_XML" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<link rel="StyleSheet" type="text/css" href="css/style.css" />
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <div>  
            <table class="table table-bordered" style="background-image: url('../img/bg_nav.jpg'); width: 100%;">
                            <tr>
                                <td colspan="3" style="color: white; text-align: left;">
                                    <asp:Button ID="btnHome" runat="server" Text="HomePage" />
                                </td>
                            </tr>              
                        </table> 
                        <table class="table table-bordered" style="background-color:#444!important; width: 100%;">
                            <tr>
                                <td colspan="3" style="color: white; text-align: center;">
                                    <strong>Riferimento dati Fattura <%= Session("TipFattura")%> del (<%= Session("GiornoDaScaricare")%>)</strong>
                                </td>
                            </tr>              
                        </table> 
                          
                        <asp:ListView ID="dataListAllegati" runat="server" DataSourceID="sqlAllegati" DataKeyNames="id" style="width: 100%;">
                    <ItemTemplate>
                        <tr style="background-color:#f1f1ee; color: #000000;">
                            <td align="left" valign="middle">
                               <asp:Label ID="lblID" runat="server" Text='<%# Eval("id") %>' visible="false"/>      
                               <asp:Label ID="lblNFile" runat="server" Text='<%# Eval("nome_file") %>' visible="false"/>                         
                               <asp:Image ID="Image2" runat="server" ImageUrl="img/punto_elenco.jpg" />&nbsp;
                               <asp:HyperLink ID="lblNome" runat="server" Text='<%# Eval("nome_file") %>' Target="_blank" href='<%# Eval("nome_file") %>' Font-Bold="true"></asp:HyperLink>
                            </td>                                   
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:#f1f1ee; color:#FFFFFF;">
                            <td align="left" valign="middle">
                               <asp:Label ID="lblID" runat="server" Text='<%# Eval("id") %>' visible="false"/>
                               <asp:Label ID="lblNFile" runat="server" Text='<%# Eval("nome_file") %>' visible="false"/>
                               <asp:Image ID="Image2" runat="server" ImageUrl="img/punto_elenco.jpg" />&nbsp;
                               <asp:HyperLink ID="lblNome" runat="server" Text='<%# Eval("nome_file") %>' Target="_blank" href='<%# Eval("nome_file") %>' Font-Bold="true"></asp:HyperLink>
                            </td>                              
                        </tr>
                    </AlternatingItemTemplate>                   
                    <EmptyDataTemplate>
                        <table id="Table1" runat="server" style="">
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                       <table id="Table1" runat="server">
                           <tr id="Tr1" runat="server">
                               <td id="Td1" runat="server">
                                   <table ID="itemPlaceholderContainer" runat="server" border="1"  cellpadding="2" cellspacing="2" style="">
                                       <tr id="Tr2" runat="server" style="background-color:#444;color: white; font-family:Arial; font-size: 12pt;">
                                           <th id="Th1" runat="server">
                                              Nome File
                                           </th>                                                                                                                                                                      
                                       </tr>
                                       <tr ID="itemPlaceholder" runat="server">
                                       </tr>
                                   </table>
                               </td>
                           </tr>
                           <tr id="Tr3" runat="server">
                               <td id="Td2" runat="server" style="">
                               </td>
                           </tr>
                       </table>
                   </LayoutTemplate>
                </asp:ListView>                              
                     </div>

    <asp:SqlDataSource ID="sqlAllegati" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="">                
     </asp:SqlDataSource>

     <asp:Label ID="lblOrderBy" runat="server" visible="false"></asp:Label>    
    <asp:Label ID="lblTxtQuery" runat="server" Text="Query Txt" Visible="false"></asp:Label>
    </div>
    </form>
</body>
</html>
