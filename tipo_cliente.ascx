<%@ Control Language="VB" AutoEventWireup="false" CodeFile="tipo_cliente.ascx.vb" Inherits="tabelle_listini_tipo_cliente" %>
<meta http-equiv="Expires" content="0" />
<meta http-equiv="Cache-Control" content="no-cache" />
<meta http-equiv="Pragma" content="no-cache" />

<%@ Register Src="/anagrafica/anagrafica_ditte.ascx" TagName="anagrafica_ditte" TagPrefix="uc1" %>

<style type="text/css">
    .style1
    {
        width: 234px;
    }
    .style2
    {
        width: 122px;
    }
    .style3
    {
        width: 248px;
    }
</style>

<uc1:anagrafica_ditte ID="anagrafica_ditte" runat="server" Visible="false" />
<table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td align="left" style="color: #FFFFFF" bgcolor="#3E6D54">
             <b>Tipologia di clienti (Fonti)</b>
           </td>
         </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444">
<tr>
  <td class="style1">
      <asp:Label ID="lblNuovaTipologiaTariffa" runat="server" Text="Tipo Cliente:" CssClass="testo_bold"></asp:Label>
  </td>
  <td class="style2">
      <asp:Label ID="lblNuovaTipologiaTariffa0" runat="server" Text="Cliente Broker:" CssClass="testo_bold"></asp:Label>
  </td>
  <td class="style2">
      <asp:Label ID="lblNuovaTipologiaTariffa2" runat="server" Text="Agenzia di Viaggio:" 
          CssClass="testo_bold"></asp:Label>
  </td>
  <td class="style3">
      <asp:Label ID="lblNuovaTipologiaTariffa1" runat="server" Text="Variazioni GG sempre a carico del TO:" CssClass="testo_bold"></asp:Label>
    </td>
  <td>
      <asp:Label ID="lblNuovaTipologiaTariffa3" runat="server" 
          Text="GG non usufruiti rimborsabili:" CssClass="testo_bold"></asp:Label>
    </td>
</tr>
<tr>
  <td class="style1">
  
      <asp:TextBox ID="txtTipoCliente" runat="server" Width="223px" MaxLength="50"></asp:TextBox>
  </td>
  <td class="style2">
  
      <asp:DropDownList ID="dropClienteBroker" runat="server">
          <asp:ListItem Selected="True" Value="-1">...</asp:ListItem>
          <asp:ListItem Value="0">No</asp:ListItem>
          <asp:ListItem Value="1">Si</asp:ListItem>
      </asp:DropDownList>
    </td>
  <td class="style2">
  
      <asp:DropDownList ID="dropAgenziaDiViaggio" runat="server">
          <asp:ListItem Selected="True" Value="-1">...</asp:ListItem>
          <asp:ListItem Value="0">No</asp:ListItem>
          <asp:ListItem Value="1">Si</asp:ListItem>
      </asp:DropDownList>
    </td>
  <td class="style3">
  
      <asp:DropDownList ID="dropEstensione" runat="server">
          <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
          <asp:ListItem Value="1">Si</asp:ListItem>
      </asp:DropDownList>
    </td>
  <td>
  
      <asp:DropDownList ID="dropGGRimborsabili" runat="server">
          <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
          <asp:ListItem Value="1">Si</asp:ListItem>
      </asp:DropDownList>
    </td>
</tr>
<tr>
  <td class="style1">
  
      <asp:Label ID="lblNuovaTipologiaTariffa4" runat="server" Text="Cliente (x Broker):" 
          CssClass="testo_bold"></asp:Label>
  </td>
  <td class="style2">
  
      &nbsp;</td>
  <td class="style2">
  
      &nbsp;</td>
  <td class="style3">
  
      &nbsp;</td>
  <td>
  
      &nbsp;</td>
</tr>
<tr>
  <td class="style1">
  
      <asp:TextBox ID="txtDitta" runat="server" Width="223px" ReadOnly="true"></asp:TextBox>
    </td>
  <td class="style2">
  
      <asp:Button ID="btnScegliDitta" runat="server" Text="Scegli" />
              </td>
  <td class="style2">
  
      <asp:Label ID="id_ditta" runat="server" Visible="false"></asp:Label>
    </td>
  <td class="style3">
  
      &nbsp;</td>
  <td>
  
      &nbsp;</td>
</tr>
<tr>
  <td colspan="5" align="center">
      <asp:Button ID="btnSalva" runat="server" Text="Salva" ValidationGroup="invia" Enabled="false"/>
      <asp:Button ID="btnAnnulla" runat="server" Text="Annulla" Visible="false" />
    </td>
</tr>
</table> 
<br />
<table border="0" cellpadding="1" cellspacing="1" width="1024px" >
 <tr>
   <td>
    <asp:ListView ID="listTipoClienti" runat="server" DataKeyNames="id" DataSourceID="sqlTipoClienti">
                   <ItemTemplate>
                       <tr style="background-color:#DCDCDC;color: #000000;">
                           <td>
                               <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                               <asp:Label ID="descrizione" runat="server" Text='<%# Eval("descrizione") %>' CssClass="testo" />
                           </td>
                           <td>
                               <asp:Label ID="agenzia_di_viaggio" runat="server" Text='<%# Eval("agenzia_di_viaggio") %>' CssClass="testo" />
                           </td>
                            <td>
                               <asp:Label ID="broker" runat="server" Text='<%# Eval("broker") %>' CssClass="testo" />
                            </td>
                            <td>
                               <asp:Label ID="id_ditta" runat="server" Text='<%# Eval("id_ditta") %>' Visible="false" />
                               <asp:Label ID="ditta" runat="server" Text='<%# Eval("ditta") %>' CssClass="testo" />
                            </td>
                            <td>
                               <asp:Label ID="estensioni" runat="server" Text='<%# Eval("estensioni_sempre_a_carico_del_broker") %>' CssClass="testo" />
                               <asp:Label ID="estensioni_db" runat="server" Text='<%# Eval("estensioni_sempre_a_carico_del_broker") %>'  Visible="false" />
                            </td>
                            <td>
                               <asp:Label ID="rimborsabili" runat="server" Text='<%# Eval("giorni_non_usufruiti_rimborsabili") %>' CssClass="testo" />
                               <asp:Label ID="rimborsabili_db" runat="server" Text='<%# Eval("giorni_non_usufruiti_rimborsabili") %>'  Visible="false" />
                            </td>
                            <td align="center">
                               <asp:ImageButton ID="btnModifica" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" />
                            </td>
                            <td align="center">
                               <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="/images/elimina.png" CommandName="elimina" />
                            </td>
                       </tr>
                   </ItemTemplate>
                   <AlternatingItemTemplate>
                       <tr>
                           <td>
                               <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                               <asp:Label ID="descrizione" runat="server" Text='<%# Eval("descrizione") %>' CssClass="testo" />
                           </td>
                           <td>
                               <asp:Label ID="agenzia_di_viaggio" runat="server" Text='<%# Eval("agenzia_di_viaggio") %>' CssClass="testo" />
                           </td>
                           <td>
                               <asp:Label ID="broker" runat="server" Text='<%# Eval("broker") %>' CssClass="testo" />
                           </td>
                           <td>
                               <asp:Label ID="id_ditta" runat="server" Text='<%# Eval("id_ditta") %>' Visible="false" />
                               <asp:Label ID="ditta" runat="server" Text='<%# Eval("ditta") %>' CssClass="testo" />
                           </td>
                           <td>
                               <asp:Label ID="estensioni" runat="server" Text='<%# Eval("estensioni_sempre_a_carico_del_broker") %>' CssClass="testo" />
                               <asp:Label ID="estensioni_db" runat="server" Text='<%# Eval("estensioni_sempre_a_carico_del_broker") %>'  Visible="false" />
                           </td>
                           <td>
                               <asp:Label ID="rimborsabili" runat="server" Text='<%# Eval("giorni_non_usufruiti_rimborsabili") %>' CssClass="testo" />
                               <asp:Label ID="rimborsabili_db" runat="server" Text='<%# Eval("giorni_non_usufruiti_rimborsabili") %>'  Visible="false" />
                            </td>
                           <td align="center">
                               <asp:ImageButton ID="btnModifica" runat="server" ImageUrl="/images/lente.png" CommandName="vedi" />
                            </td>
                           <td align="center">
                              <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="/images/elimina.png" CommandName="elimina"  />
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
                                               <asp:Label ID="Label1" runat="server" Text="Tipo Cliente (Fonte)" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th4" runat="server" align="left">
                                               <asp:Label ID="Label4" runat="server" Text="Agenzia di Viaggio" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th3" runat="server" align="left">
                                               <asp:Label ID="Label2" runat="server" Text="Broker" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th6" runat="server" align="left">
                                               <asp:Label ID="Label6" runat="server" Text="Ditta (x Fatt.)" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th2" runat="server" align="left">
                                               <asp:Label ID="Label3" runat="server" Text="Var. RA (Broker)" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th5" runat="server" align="left">
                                               <asp:Label ID="Label5" runat="server" Text="GG non rimb." CssClass="testo_titolo"></asp:Label>
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
                 </td>

         </tr>
</table>

<asp:Label ID="livelloAccesso" runat="server" Visible="false"></asp:Label>
<asp:Label ID="idModifica" runat="server" Visible="false"></asp:Label>
<asp:SqlDataSource ID="sqlTipoClienti" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT clienti_tipologia.id, clienti_tipologia.descrizione, clienti_tipologia.broker, 
    clienti_tipologia.agenzia_di_viaggio, clienti_tipologia.estensioni_sempre_a_carico_del_broker, 
    clienti_tipologia.giorni_non_usufruiti_rimborsabili, ditte.rag_soc As ditta, ditte.id_ditta FROM clienti_tipologia WITH(NOLOCK)
    LEFT JOIN ditte ON clienti_tipologia.id_ditta=ditte.id_ditta
    ORDER BY [descrizione]"></asp:SqlDataSource>