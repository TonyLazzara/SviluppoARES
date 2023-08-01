<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenu.master" AutoEventWireup="false" CodeFile="allegati_contratti.aspx.vb" Inherits="allegati_contratti" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <meta http-equiv="Expires" content="0" />
  <meta http-equiv="Cache-Control" content="no-cache" />
  <meta http-equiv="Pragma" content="no-cache" />
  
  
    <style type="text/css">
        .style1
        {
            width: 91px;
        }
    </style>
  
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;">
             <b>Tipo Allegati Prenotazioni/Contratti</b>
           </td>
         </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444">
<tr>
  <td class="style1">
      <asp:Label ID="lblCognome" runat="server" Text="Tipo Allegato:" CssClass="testo_bold"></asp:Label></td>
  <td>
  
      <asp:TextBox ID="txtTipoAllegato" runat="server" Width="186px" MaxLength="50"></asp:TextBox>
  </td>
</tr>
<tr>
  <td colspan="2" align="center">
                  <asp:Button ID="btnSalva" runat="server" Text="Salva" ValidationGroup="invia" />
            &nbsp;<asp:Button ID="btnCerca" runat="server" Text="Cerca" />
  &nbsp;<asp:Button ID="btnAnnulla" runat="server" Text="Annulla" Visible="false"/>
          
  </td>
</tr>
</table>
<br />
<table border="0" cellpadding="1" cellspacing="1" width="1024px" >
 <tr>
   <td>
    <asp:ListView ID="listAllegati" runat="server" DataKeyNames="id_cnt_pren_allegati_tipo" DataSourceID="sqlAllegati" >
                   <ItemTemplate>
                       <tr style="background-color:#DCDCDC;color: #000000;">
                           <td>
                               <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id_cnt_pren_allegati_tipo") %>' Visible="false" />
                               <asp:Label ID="descrizione" runat="server" Text='<%# Eval("descrizione") %>' CssClass="testo" />
                           </td>
                           <td>
                               <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" ToolTip="Modifica" />
                           </td>
                           <td>
                              <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="/images/elimina.png" CommandName="elimina" OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler cancellare l\'acquirente?'));" ToolTip="Elimina" />
                           </td>
                       </tr>
                   </ItemTemplate>
                   <AlternatingItemTemplate>
                       <tr>
                           <td>
                               <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id_cnt_pren_allegati_tipo") %>' Visible="false" />
                               <asp:Label ID="descrizione" runat="server" Text='<%# Eval("descrizione") %>' CssClass="testo" />
                           </td>
                           <td>
                               <asp:ImageButton ID="btnVedi" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" ToolTip="Modifica" />
                           </td>
                           <td>
                              <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="/images/elimina.png" CommandName="elimina" OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler cancellare l\'acquirente?'));" ToolTip="Elimina" />
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
                                               <asp:LinkButton ID="LinkButton1" runat="server" CommandName="order_by_ragsoc" CssClass="testo_titolo">Tipo Allegato</asp:LinkButton>
                                           </th>
                                           <th>
                                           </th>
                                           <th>
                                           </th>
                                       </tr>
                                       <tr ID="itemPlaceholder" runat="server">
                                       </tr>
                                   </table>
                               </td>
                           </tr>
         
                       </table>
                   </LayoutTemplate>
               </asp:ListView>
                 <br />
                 </td>

         </tr>
</table>
             
<asp:Label ID="livelloAccesso" runat="server" Visible="false"></asp:Label>
        
<asp:Label ID="veicolo" runat="server" Visible="false"></asp:Label>
<asp:Label ID="lblQuery" runat="server" Visible="false"></asp:Label>
<asp:Label ID="id_modifica" runat="server" Visible="false"></asp:Label>

<asp:Label ID="nome_file_import" runat="server" Visible="false"></asp:Label>

<asp:Label ID="lblOrderBY" runat="server" Visible="false"></asp:Label>

  <asp:SqlDataSource ID="sqlAllegati" runat="server" 
             ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
             SelectCommand="SELECT id_cnt_pren_allegati_tipo, descrizione FROM contratti_prenotazioni_allegati_tipo WITH(NOLOCK) ORDER BY descrizione"></asp:SqlDataSource>
</asp:Content>

