<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AccessoriSelezionati.ascx.vb" Inherits="gestione_danni_AccessoriSelezionati" %>


<%@ Register Assembly="BoxOver" Namespace="BoxOver" TagPrefix="boxover" %>

<style>
        .toolheader {
        background-color:#19191b;
        color:#FFFFFF;
        font-weight:bold;
        width:400px;
        border-top: 1px solid;
	        border-top-color:#C60;
	        border-left: 1px solid;
	        border-left-color:#C60;
	        border-right: 1px solid;
	        border-right-color:#C60;
	        border-bottom: 1px solid;
	        border-bottom-color:#C60;
        }
        .toolbody {
        width:400px;
        background-color:#FFFFFF;
        filter:alpha(opacity=90);
	        -moz-opacity:.90;
	        opacity:.90;
	        border-top: 1px solid;
	        border-top-color:#C60;
	        border-left: 1px solid;
	        border-left-color:#C60;
	        border-right: 1px solid;
	        border-right-color:#C60;
	        border-bottom: 1px solid;
	        border-bottom-color:#C60;
        }
         .style1
         {
             width: 133px;
         }
         .style4
         {
         }
         .style5
         {
             width: 131px;
         }
         .style7
         {
             width: 193px;
         }
         .style8
         {
             width: 179px;
         }
         .style9
         {
             width: 61px;
         }
         .style11
         {
             width: 118px;
         }
         .style12
         {
             width: 58px;
         }
         .style13
         {
             width: 48px;
         }
         .style14
         {
             width: 83px;
         }
         .style15
         {
             width: 262px;
         }
         .style16
         {
             width: 230px;
         }
         .style17
         {
             width: 206px;
         }
         .style18
         {
             height: 10px;
         }
         .style24
         {
             width: 206px;
             height: 7px;
         }
         .style25
         {
             width: 230px;
             height: 7px;
         }
         .style26
         {
             width: 262px;
             height: 7px;
         }
         .style27
         {
             width: 117px;
         }
         .style28
         {
             width: 222px;
         }
         .style29
         {
         }
         .style30
         {
             width: 130px;
         }
         .style32
         {
             width: 132px;
         }
         .style33
         {
             width: 182px;
         }
         .style34
         {
             width: 169px;
         }
         .style35
         {
             width: 158px;
         }
         .style36
         {
             height: 11px;
             width: 179px;
         }
         .style37
         {
             height: 11px;
             width: 89px;
         }
         .style38
         {
             height: 11px;
             width: 76px;
         }
         .style39
         {
             height: 11px;
             width: 74px;
         }
         .style40
         {
             height: 11px;
             width: 49px;
         }
         .style41
         {
             height: 11px;
             width: 14px;
         }
         .style43
         {
             width: 71px;
         }
         .style45
         {
         }
         .style46
         {
             width: 57px;
         }
         .style47
         {
             width: 196px;
         }
         .style48
         {
             width: 29px;
         }
         .style49
         {
             width: 4px;
         }
         .style50
         {
             width: 63px;
         }
         .style51
         {
             width: 134px;
         }
         .style52
         {
             width: 79px;
         }
         .style53
         {
             width: 108px;
         }
         .style54
         {
             width: 136px;
         }
         .style55
         {
             width: 186px;
         }
         .style56
         {
             width: 72px;
         }
         .style58
         {
             width: 36px;
         }
         .style59
         {
             width: 56px;
         }
         .style60
         {
             width: 102px;
         }
         .style72
         {
         }
         .style73
         {
             width: 98px;
         }
         .style74
         {
             width: 68px;
         }
 </style>

 <div runat="server" id="tariffe_e_costi">
  <table runat="server" id="table_tariffe" style="border:4px solid #444; border-bottom:0px;" border="0" cellspacing="2" cellpadding="2" width="100%">
      <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" colspan="6">
             <asp:Label ID="Label19" runat="server" Text="Tariffa e costi" CssClass="testo_titolo"></asp:Label>
          </td>
      </tr>
      <tr>
          <td valign="top" class="style27">
              <asp:Label ID="Label77" runat="server" Text="Tariffe Generiche:" CssClass="testo_bold"></asp:Label>
          </td>
          <td valign="top" class="style35">
                  <asp:DropDownList ID="dropTariffeGeneriche" runat="server" 
                        AppendDataBoundItems="True" DataSourceID="sqlTariffeGeneriche" 
                        DataTextField="codice" DataValueField="id">
                        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
           </td>
          <td valign="top" class="style30">
              <asp:Label ID="Label9" runat="server" Text="Tariffe Particolari:" CssClass="testo_bold"></asp:Label>
          </td>
          <td valign="top" class="style32">
              <asp:DropDownList ID="dropTariffeParticolari" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlTariffeParticolari" 
                    DataTextField="codice" DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
              </asp:DropDownList>
          </td>
          <td width="30%" valign="top">
                &nbsp;</td>
         
      </tr>
  </table>
  
  <table style="border:4px solid #444; border-top:0px;" border="0" cellspacing="0" cellpadding="0" width="100%" >
    <tr>
          <td align="left" width="70%" valign="top">
              <asp:DataList ID="listContrattiCosti" runat="server" DataSourceID="sqlContrattiCosti" Width="100%">
                  <ItemTemplate>
                      <tr runat="server" id="riga_gruppo" >
                         <td bgcolor="#19191b" style="width:100%;" colspan="7">
                              <asp:Label ID="Label7" runat="server" Text="Gruppo:" CssClass="testo"  />
                              <asp:Label ID="id_gruppoLabel" runat="server" Text='<%# Eval("id_gruppo") %>' Visible="false" />
                              <asp:Label ID="gruppo" runat="server" Text='<%# Eval("gruppo") %>' CssClass="testo_bold" />
                         </td>
                      </tr>
                      <tr runat="server" id="riga_intestazione_informative" visible="false">
                         <td bgcolor="#19191b"  colspan="6">
                             <asp:Label ID="Label50" runat="server" Text='Penalità' CssClass="testo_bold" />
                         </td>
                         <td bgcolor="#19191b" >
                             <asp:Label ID="Label49" runat="server" Text='Non Pag.' CssClass="testo_bold" />
                         </td>
                      </tr>
                      <tr runat="server" id="riga_intestazione">
                         <td width="5%">
                         </td>
                         <td width="52%">
                         </td>
                         <td width="10%">
                            <asp:Label ID="Label13" runat="server" Text='Sconto' CssClass="testo_bold" />&nbsp;
                         </td>
                         <td width="11%">
                            <asp:Label ID="Label46" runat="server" Text='Costo U.' CssClass="testo_bold" />&nbsp;
                         </td>
                         <td width="4%">
                            <asp:Label ID="Label47" runat="server" Text='Qta' CssClass="testo_bold"  />&nbsp;
                         </td>
                         <td width="14%">
                            <asp:Label ID="Label12" runat="server" Text='Costo' CssClass="testo_bold" />&nbsp;
                         </td>
                         <td width="4%">
                           <asp:Label ID="Label21" runat="server" Text='Omaggio' CssClass="testo_bold" />
                         </td>
                      </tr>
                      <tr runat="server" id="riga_elementi">
                         <td width="5%" align="left" >
                             <asp:CheckBox ID="chkOldScegli" runat="server" Visible="false" Checked='<%# Eval("selezionato") %>' />
                             <asp:CheckBox ID="chkScegli" runat="server" Visible="false" Checked='<%# Eval("selezionato") %>' />
                             <asp:Label ID="lblIncluso" runat="server" Text="I " ToolTip="Incluso" CssClass="testo_bold" Visible="false"  />
                             <asp:Label ID="lblObbligatorio" runat="server" Text="O " ToolTip="Obbligatorio" CssClass="testo_bold" Visible="false"  />
                             <asp:Label ID="lblInformativa" runat="server" Text="IN " ToolTip="Informativa" CssClass="testo_bold" Visible="false"  />
                         </td>
                         <td width="52%">
                              <asp:Label ID="id_preventivi_costi" runat="server" Text='<%# Eval("id") %>' visible="false" />
                              <asp:Label ID="id_elemento" runat="server" Text='<%# Eval("id_elemento") %>' visible="false" />
                              <boxover:BoxOver ID="BoxOver1" runat="server" body='<%# Eval("nome_costo") & " - " & Eval("descrizione_lunga") %>' controltovalidate="nome_costo" header="Descrizione" CssHeader="toolheader"  CssBody="toolbody"   />
                              <asp:Label ID="nome_costo" runat="server" Text='<%# Eval("nome_costo") %>' CssClass="testo" />:&nbsp;&nbsp;
                         </td>
                         <td width="10%" align="right">
                              <asp:Label ID="lblSconto" runat="server" Text='<%# FormatNumber(Eval("sconto"),2) %>' CssClass="testo"/>
                         </td>
                         <td width="11%" align="right">
                              <asp:Label ID="lbl_costo_unitario" runat="server" Visible="True" CssClass="testo" />
                              
                              <asp:Label ID="valore_costoLabel" runat="server" Text='<%# FormatNumber(Eval("valore_costo"),2) %>' Visible="false" />&nbsp;
                              <asp:Label ID="id_unita_misura" runat="server" Text='<%# Eval("id_unita_misura") %>' Visible="false" />
                              <asp:Label ID="packed" runat="server" Text='<%# Eval("packed") %>' Visible="false" />
                              <asp:Label ID="id_a_carico_di" runat="server" Text='<%# Eval("id_a_carico_di") %>' Visible="false" />
                              <asp:Label ID="obbligatorio" runat="server" Text='<%# Eval("obbligatorio") %>' Visible="false" /> 
                              <asp:Label ID="id_metodo_stampa" runat="server" Text='<%# Eval("id_metodo_stampa") %>' Visible="false" />                    
                              <asp:Label ID="franchigia_attiva" runat="server" Text='<%# Eval("franchigia_attiva") %>' Visible="false" />
                              <asp:Label ID="data_aggiunta_nolo_in_corso" runat="server" Text='<%# Eval("data_aggiunta_nolo_in_corso") %>' Visible="false" />
                              <asp:Label ID="tipologia_franchigia" runat="server" Text='<%# Eval("tipologia_franchigia") %>' Visible="false" />
                              <asp:Label ID="servizio_rifornimento_tolleranza" runat="server" Text='<%# Eval("servizio_rifornimento_tolleranza") %>' Visible="false" />
                         </td>
                         <td width="4%" align="right">
                              <asp:Label ID="qta" runat="server" Visible="True" CssClass="testo" Text='<%# Eval("qta") %>' />
                         </td>
                         <td width="14%" align="right">
                            <asp:Label ID="costo_scontato" runat="server" Text='<%# FormatNumber(FormatNumber(Eval("valore_costo"),2) - FormatNumber(Eval("sconto"),2),2) %>' CssClass="testo" />&nbsp;
                         </td>
                         <td align="center" width="4%">
                             <asp:Label ID="omaggiabile" runat="server" Text='<%# Eval("omaggiabile") %>' Visible="false" />
                             <asp:Label ID="acquistabile_nolo_in_corso" runat="server" Text='<%# Eval("acquistabile_nolo_in_corso") %>' Visible="false" />
                             <asp:CheckBox ID="chkOldOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />
                             <asp:CheckBox ID="chkOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />   
                         </td>
                      </tr>
                  </ItemTemplate>
              </asp:DataList>
            </td>
            <td width="30%" valign="top">
                &nbsp;</td>
           </tr>
  </table>
  </div>

<asp:Label ID="idContratto" runat="server" visible="false"></asp:Label>
<asp:Label ID="numCalcolo" runat="server" visible="false"></asp:Label>
<asp:Label ID="solo_selezinati" runat="server" visible="false"></asp:Label>

<asp:Label ID="tariffa_broker" runat="server" Visible="false"></asp:Label>
<asp:Label ID="a_carico_del_broker" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="statoContratto" runat="server" Text="4" Visible="false"></asp:Label>

<asp:Label ID="ultimo_gruppo" runat="server" Visible="false"></asp:Label>
<asp:Label ID="intestazione_informativa_da_visualizzare" runat="server" Visible="false"></asp:Label>

<asp:Label ID="statoModificaContratto" runat="server" Visible="false"></asp:Label>
<asp:Label ID="livello_accesso_omaggi" runat="server" Visible="false"></asp:Label>




<asp:SqlDataSource ID="sqlContrattiCosti" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT contratti_costi.id, gruppi.id_gruppo, gruppi.cod_gruppo As gruppo, contratti_costi.id_elemento, contratti_costi.nome_costo, condizioni_elementi.descrizione_lunga, ISNULL((imponibile+iva_imponibile),0) As valore_costo, ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, id_a_carico_di, contratti_costi.obbligatorio, contratti_costi.id_metodo_stampa, ISNULL(contratti_costi.selezionato,'False') As selezionato, ISNULL(contratti_costi.omaggiato,'False') As omaggiato, ISNULL(contratti_costi.omaggiabile,'False') As omaggiabile,ISNULL(contratti_costi.acquistabile_nolo_in_corso,'False') As acquistabile_nolo_in_corso, ISNULL(contratti_costi.franchigia_attiva,'False') As franchigia_attiva, id_unita_misura, packed, qta, data_aggiunta_nolo_in_corso, condizioni_elementi.tipologia_franchigia, condizioni_elementi.servizio_rifornimento_tolleranza  FROM contratti_costi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON contratti_costi.id_gruppo=gruppi.id_gruppo LEFT JOIN condizioni_elementi WITH(NOLOCK) ON contratti_costi.id_elemento=condizioni_elementi.id WHERE ((id_documento = @id_contratto) AND (num_calcolo = @num_calcolo_contratto)) AND ordine_stampa<>'5' AND ISNULL(franchigia_attiva,'1')='1' ORDER BY id_gruppo, secondo_ordine_stampa, ordine_stampa,ISNULL(condizioni_elementi.ordinamento_vis,0) DESC, nome_costo ">
    <SelectParameters>
        <asp:ControlParameter ControlID="idContratto" Name="id_contratto" PropertyName="Text" Type="Int32" />
        <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo_contratto" PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlTariffeGeneriche" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, codice FROM tariffe WITH(NOLOCK) WHERE id='0'">
</asp:SqlDataSource> 

<asp:SqlDataSource ID="sqlTariffeParticolari" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, codice FROM tariffe WITH(NOLOCK) WHERE id='0'">
</asp:SqlDataSource>
