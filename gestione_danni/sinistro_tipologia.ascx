<%@ Control Language="VB" AutoEventWireup="false" CodeFile="sinistro_tipologia.ascx.vb" Inherits="gestione_danni_sinistro_tipologia" %>
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

<style type="text/css">
    .style3
    {
        text-align: left;
        color: #000000;
        width: 162px;
    }
    .style4
    {
        width: 631px;
    }
    .style5
    {
        width: 68px;
    }
</style>

<table border="0" cellpadding="0" cellspacing="0" width="1024px">
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;">
             <b>Sinistri: Tipologia</b>
           </td>
         </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="1024px" style="border: 4px solid #444">
<tr>
  <td class="style3">
      <asp:Label ID="lblTipologiaSinistro" runat="server" Text="Tipologia Sinistro:" CssClass="testo_bold"></asp:Label></td>
  <td class="style4">
  
      <asp:TextBox ID="txtTipologiaSinistro" runat="server" Width="126px" MaxLength="50"></asp:TextBox>
            
  </td>
  <td class="style5">
  
      &nbsp;</td>
  <td>
  
      &nbsp;</td>
</tr>
<tr>
  <td align="center" colspan="4">
     <asp:Button ID="btnSalva" runat="server" Text="Salva" ValidationGroup="invia" Enabled="false"/>
      &nbsp;<asp:Button ID="btnCerca" runat="server" Text="Cerca" />
      &nbsp;<asp:Button ID="btnAnnulla" runat="server" Text="Annulla" Visible="false" 
          style="height: 26px" />
      &nbsp;<asp:Button ID="btnSeleziona" runat="server" Text="Seleziona" 
          Visible="false" />
      </td>
</tr>
</table>
<br />
<table border="0" cellpadding="1" cellspacing="1" width="1024px" >
 <tr>
   <td>
     <asp:ListView ID="listTipologia" runat="server" DataKeyNames="id" DataSourceID="sqlTipologia" Visible="false">
                   <ItemTemplate>
                       <tr style="background-color:#DCDCDC;color: #000000;">
                           <td>
                               <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                               <asp:Label ID="descrizioneLabel" runat="server" Text='<%# Eval("descrizione") %>' />
                           </td>
                           <td align="center">
                               <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" ToolTip="Modifica" />
                           </td>
                           <td align="center">
                               <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="/images/elimina.png" CommandName="elimina" ToolTip="Elimina" OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler cancellare l'elemento selezionato?'));" />
                           </td>
                       </tr>
                   </ItemTemplate>
                   <AlternatingItemTemplate>
                       <tr>
                           <td>
                               <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                               <asp:Label ID="descrizioneLabel" runat="server" Text='<%# Eval("descrizione") %>' />
                           </td>
                           <td align="center">
                               <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" ToolTip="Modifica" />
                           </td>
                           <td align="center">
                               <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="/images/elimina.png" CommandName="elimina" OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler cancellare l'elemento selezionato?'));" ToolTip="Elimina" />
                           </td>
                       </tr>
                   </AlternatingItemTemplate>
                   <EmptyDataTemplate>
                       <table id="Table1" runat="server" 
                           style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                           <tr>
                               <td>
                               </td>
                           </tr>
                       </table>
                   </EmptyDataTemplate>
                   <LayoutTemplate>
                       <table id="Table2" runat="server" width="100%">
                           <tr id="Tr1" runat="server">
                               <td id="Td1" runat="server">
                                   <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                       style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                       <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                           <th id="Th1" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton1" runat="server" CommandName="order_by_descrizione" CssClass="testo_titolo">Sinitro Gestito Da</asp:LinkButton>
                                           </th>
                                           <th>
                                           </th>
                                           <th></th>
                                       </tr>
                                       <tr ID="itemPlaceholder" runat="server">
                                       </tr>
                                   </table>
                               </td>
                           </tr>
                           <tr id="Tr3" runat="server">
                              <td id="Td2" runat="server" style="text-align: left;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                                  <asp:DataPager ID="DataPager1" runat="server" PageSize="20">
                                      <Fields>
                                          <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
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

<asp:Label ID="livelloAccesso" runat="server" Visible="false"></asp:Label>
<asp:Label ID="lblQuery" runat="server" Visible="false"></asp:Label>
<asp:Label ID="id_modifica" runat="server" Visible="false"></asp:Label>

<asp:Label ID="lblOrderBY" runat="server" Visible="false"></asp:Label>

<asp:SqlDataSource ID="sqlTipologia" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT id, descrizione  FROM sinistri_tipologia WITH(NOLOCK) ORDER BY descrizione"></asp:SqlDataSource>