<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DettagliPagamento.ascx.vb" Inherits="gestione_danni_DettagliPagamento" %>


<style type="text/css">
    .style1
    {
        height: 22px;
    }
</style>


<div runat="server" id="tab_dettagli_pagamento" visible="true">
<table style="border:4px solid #444;width:100%;border-top:0px;padding:1px;" border="0" cellspacing="1"  >
  <tr>
     <td style="color: #FFFFFF;background-color:#444;text-align:left;" colspan="2">
         <asp:Label ID="Label23" runat="server" Text="Dettagli di pagamento" CssClass="testo_titolo"></asp:Label>
     </td>
  </tr>
  <tr runat="server" id="riga_pagamento_pos" visible="false">
    <td colspan="2">
       <table style="border-top:0px;width:100%" border="0" cellspacing="1" cellpadding="1" >
         <tr>
           <td class="style40">
             <asp:Label ID="funzione" runat="server" Text="Funzione" CssClass="testo_bold" />
           </td>
           <td class="style34">
             <asp:Label ID="Label36" runat="server" Text="Staz." CssClass="testo_bold" />
           </td>
           <td class="style5">
             <asp:Label ID="Label37" runat="server" Text="Operatore" CssClass="testo_bold" />
           </td>
           <td class="style38">
             <asp:Label ID="Label38" runat="server" Text="Cassa" CssClass="testo_bold" />
           </td>
           <td class="style36">
             <asp:Label ID="Label39" runat="server" Text="Carta" CssClass="testo_bold" />
           </td>
           <td class="style37">
             <asp:Label ID="Label40" runat="server" Text="Intestatario" CssClass="testo_bold" />
           </td>
           <td class="style39">
             <asp:Label ID="Label41" runat="server" Text="Scadenza" CssClass="testo_bold" />
           </td>
           <td>
             <asp:Label ID="Label67" runat="server" Text="Data Operazione" CssClass="testo_bold" />
             </td>
         </tr>
         <tr>
           <td class="style40">
      
                 <asp:TextBox ID="txtPOS_Funzione" runat="server" Width="88px" ReadOnly="true"></asp:TextBox>
      
           </td>
           <td class="style34">
      
                 <asp:TextBox ID="txtPOS_Stazione" runat="server" Width="40px" ReadOnly="true"></asp:TextBox>
      
           </td>
           <td class="style5">
      
                 <asp:TextBox ID="txtPOS_Operatore" runat="server" Width="160px" 
                     ReadOnly="true"></asp:TextBox>
      
             </td>
           <td class="style38">
      
                 <asp:TextBox ID="txtPOS_Cassa" runat="server" Width="40px" ReadOnly="true"></asp:TextBox>
      
             </td>
           <td class="style36">
      
                 <asp:TextBox ID="txtPOS_Carta" runat="server" Width="140px" ReadOnly="true"></asp:TextBox>
      
             </td>
           <td class="style37">
      
                 <asp:TextBox ID="txtPOS_Intestatario" runat="server" Width="160px" ReadOnly="true"></asp:TextBox>
      
             </td>
           <td class="style39">
                 <asp:TextBox ID="txtPOS_Scadenza" runat="server" Width="86px" ReadOnly="true"></asp:TextBox>
           </td>
           <td>
                 <asp:TextBox ID="txtPOS_DataOperazione" runat="server" Width="126px" 
                     ReadOnly="true"></asp:TextBox>
             </td>
         </tr>
         <tr>
           <td class="style1">
      
             <asp:Label ID="Label66" runat="server" Text="Terminal ID." CssClass="testo_bold" />
      
           </td>
           <td class="style1">
      
               </td>
           <td class="style1">
      
             <asp:Label ID="Label69" runat="server" Text="Nr. Preaut." CssClass="testo_bold" />
      
             </td>
           <td class="style1">
      
               </td>
           <td class="style1">
      
             <asp:Label ID="Label70" runat="server" Text="Scadenza Preaut." CssClass="testo_bold" />
      
             </td>
           <td class="style1">
                    <asp:Label ID="Label74" runat="server" Text="Stato" CssClass="testo_bold" />
               </td>
           <td class="style1">
               </td>
           <td class="style1">
      
             
      
             </td>
         </tr>
         <tr>
           <td class="style40">
      
                 <asp:TextBox ID="txtPOS_TerminalID" runat="server" Width="84px" ReadOnly="true"></asp:TextBox>
      
           </td>
           <td class="style34">
      
                 &nbsp;</td>
           <td class="style5">
      
                 <asp:TextBox ID="txtPOS_NrPreaut" runat="server" Width="158px" ReadOnly="true"></asp:TextBox>
      
             </td>
           <td class="style38">
      
                 &nbsp;</td>
           <td class="style36">
      
                 <asp:TextBox ID="txtPOS_ScadenzaPreaut" runat="server" Width="140px" 
                     ReadOnly="true"></asp:TextBox>
             </td>
           <td class="style37">      
                    &nbsp; <asp:TextBox ID="txtPOS_Stato" runat="server" Width="74px" 
                     ReadOnly="true"></asp:TextBox></td>
             </tr>
         </table>
    </td>
           <td class="style39">
                &nbsp;
                </td>
           <td>
                  &nbsp;   <asp:Label ID="idPagamentoExtra" runat="server" Text="" Visible="false"></asp:Label>
                
      
             </td>
   </tr>
         <tr runat="server" visible="false">
           <td class="style40">
      
             <asp:Label ID="Label77" runat="server" Text="Note" 
                   CssClass="testo_bold" />
      
             </td>
           <td class="style34">
      
               &nbsp;</td>
           <td class="style5">
      
                 &nbsp;</td>
           <td class="style38">
      
                 &nbsp;</td>
           <td class="style36">
      
                 &nbsp;</td>
           <td class="style37">
      
                 &nbsp;</td>
           <td class="style39">
                 &nbsp;</td>
           <td>
      
                 &nbsp;</td>
         </tr>
         <tr runat="server" visible="false">
           <td class="style40" colspan="8">      
                 <asp:TextBox ID="txtNote" runat="server" Width="100%" ReadOnly="true"></asp:TextBox>
             </td>
         </tr>
         <tr runat="server" visible="false">
           <td class="style33" colspan="8" align="center">
      
               <asp:Button ID="btn_chiudi" runat="server" Text="Chiudi Dettaglio" />&nbsp;<asp:Button ID="btnModificaDataPagamento" 
                   runat="server" Text="Modifica Data"  /> &nbsp;
               <asp:Button ID="btnEliminaPagamento" runat="server" Text="Elimina Pagamento"  
                   OnClientClick="javascript: return(window.confirm ('Attenzione: sei sicuro di voler eliminare definitivamente questo pagamento?.'));" />&nbsp;

               <asp:Button ID="btnVisualizzaCC" runat="server" 
                   Text="Visualizza Numero Carta" />&nbsp;
                              &nbsp;
               &nbsp;<asp:Label ID="lblPasswordCC" runat="server" CssClass="testo_bold" 
                   Text="PASSWORD: " />
               <asp:TextBox ID="txtPasswordCC" runat="server" ReadOnly="false" 
                   TextMode="Password" Width="105px"></asp:TextBox>

           </td>
         </tr>

   
   
  <tr>
    <td colspan="2">
    
        <asp:ListView ID="listPagamenti" runat="server" DataKeyNames="ID_CTR" DataSourceID="sqlDettagliPagamento">
            <ItemTemplate>
                <tr style="background-color:#DCDCDC;color: #000000;">
                    <td align="left">
                       <asp:Label ID="lblProvenienza" runat="server" CssClass="testo" Text='<%# Eval("provenienza") %>' />
                    </td>
                    <td align="left">
                       <asp:Label ID="lb_ID_ModPag" runat="server" Text='<%# Eval("ID_ModPag") %>' Visible="false" />
                       <asp:Label ID="ID_TIPPAG" runat="server" Text='<%# Eval("ID_TIPPAG") %>' Visible="false" />
                          <asp:Label ID="lb_Des_ID_ModPag" runat="server" Text='<%# Eval("Des_ID_ModPag") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                        <asp:Label ID="ID_CTRLabel" runat="server" Text='<%# Eval("ID_CTR") %>' Visible="false" />
                        <asp:Label ID="id_pos_funzioni_ares" runat="server" Text='<%# Eval("id_pos_funzioni_ares") %>' Visible="false" />
                        <asp:Label ID="funzione" runat="server" Text='<%# Eval("funzione") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                       <asp:Label ID="lblStato" runat="server" Visible="true" />
                       <asp:Label ID="preaut_aperta" runat="server" Text='<%# Eval("preaut_aperta") %>' Visible="false" />
                       <asp:Label ID="operazione_stornata" runat="server" Text='<%# Eval("operazione_stornata") %>' Visible="false" />
                    </td>
                    <td align="left">
                        <asp:Label ID="DATA_OPERAZIONELabel" runat="server" Text='<%# Eval("DATA_OPERAZIONE") %>' CssClass="testo" />
                    </td>
                    <td align="right">
                        <asp:Label ID="PER_IMPORTOLabel" runat="server" Text='<%# libreria.myFormatta(Eval("PER_IMPORTO"),"0.00") %>' CssClass="testo" />&nbsp;&nbsp;
                    </td>
                    <td align="left">
                        &nbsp;<asp:Label ID="lb_scadenza_preaut" runat="server" Text='<%# libreria.myFormatta(Eval("scadenza_preaut"),"dd/MM/yyyy") %>' CssClass="testo" />
                    </td>
                    <td align="center" runat="server" Visible='<%# lb_th_lente.text %>' >
                        <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi" />
                    </td>
                    <td id="Td3" align="center" runat="server" visible='<%# lb_th_pagamento.Text %>' >
                         <asp:Button ID="bt_pag_rds" CommandName="bt_pag_rds" runat="server" Text="RDS" visible="false" />
                         <asp:Button ID="bt_pag_ra" CommandName="bt_pag_ra" runat="server" Text="RA" visible="false" />
                         <asp:Button ID="bt_pag_rds_ra" CommandName="bt_pag_rds_ra" runat="server" Text="RDS + RA" visible="false" />
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="">
                    <td align="left">
                       <asp:Label ID="lblProvenienza" runat="server" CssClass="testo" Text='<%# Eval("provenienza") %>' />
                    </td>
                    <td align="left">
                       <asp:Label ID="lb_ID_ModPag" runat="server" Text='<%# Eval("ID_ModPag") %>' Visible="false" />
                       <asp:Label ID="ID_TIPPAG" runat="server" Text='<%# Eval("ID_TIPPAG") %>' Visible="false" />   
                       <asp:Label ID="lb_Des_ID_ModPag" runat="server" Text='<%# Eval("Des_ID_ModPag") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                        <asp:Label ID="ID_CTRLabel" runat="server" Text='<%# Eval("ID_CTR") %>' Visible="false" />
                        <asp:Label ID="id_pos_funzioni_ares" runat="server" Text='<%# Eval("id_pos_funzioni_ares") %>' Visible="false" />
                        <asp:Label ID="funzione" runat="server" Text='<%# Eval("funzione") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                       <asp:Label ID="lblStato" runat="server" Visible="true" />
                       <asp:Label ID="preaut_aperta" runat="server" Text='<%# Eval("preaut_aperta") %>' Visible="false" />
                       <asp:Label ID="operazione_stornata" runat="server" Text='<%# Eval("operazione_stornata") %>' Visible="false" />
                    </td>
                    <td align="left">
                        <asp:Label ID="DATA_OPERAZIONELabel" runat="server" Text='<%# Eval("DATA_OPERAZIONE") %>' CssClass="testo" />
                    </td>
                    <td align="right">
                        <asp:Label ID="PER_IMPORTOLabel" runat="server" Text='<%# libreria.myFormatta(Eval("PER_IMPORTO"),"0.00") %>' CssClass="testo" />&nbsp;&nbsp;
                    </td>
                    <td align="left">
                        &nbsp;<asp:Label ID="lb_scadenza_preaut" runat="server" Text='<%# libreria.myFormatta(Eval("scadenza_preaut"),"dd/MM/yyyy") %>' CssClass="testo" />
                    </td>
                    <td align="center" runat="server" Visible='<%# lb_th_lente.text %>' >
                         <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi" />
                    </td>
                    <td align="center" runat="server" visible='<%# lb_th_pagamento.Text %>' >
                         <asp:Button ID="bt_pag_rds" CommandName="bt_pag_rds" runat="server" Text="RDS" visible="false" />
                         <asp:Button ID="bt_pag_ra" CommandName="bt_pag_ra" runat="server" Text="RA" visible="false" />
                         <asp:Button ID="bt_pag_rds_ra" CommandName="bt_pag_rds_ra" runat="server" Text="RDS + RA" visible="false" />
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
                <table id="Table2" runat="server" width="100%">
                    <tr id="Tr1" runat="server">
                        <td id="Td1" runat="server">
                            <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%" 
                                      style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                <tr id="Tr2" runat="server"  style="color: #FFFFFF" bgcolor="#19191b">
                                    <th id="Th6" runat="server" align="left">
                                        <asp:Label ID="Label45" runat="server" Text="Fonte" CssClass="testo_titolo"></asp:Label>
                                    </th>
                                    <th id="Th21" runat="server" align="left">
                                        <asp:Label ID="Label38" runat="server" Text="Tipo" CssClass="testo_titolo"></asp:Label>
                                    </th>
                                    <th id="Th1" runat="server" align="left">
                                        <asp:Label ID="Label22" runat="server" Text="Modalità" CssClass="testo_titolo"></asp:Label>
                                    </th>
                                    <th id="Th2" runat="server" align="left">
                                        <asp:Label ID="Label18" runat="server" Text="Stato" CssClass="testo_titolo"></asp:Label>
                                    </th>
                                    <th id="Th3" runat="server" align="left">
                                        <asp:Label ID="Label11" runat="server" Text="Data" CssClass="testo_titolo"></asp:Label>
                                    </th>
                                    <th id="Th4" runat="server" align="left">
                                        <asp:Label ID="Label16" runat="server" Text="Importo" CssClass="testo_titolo"></asp:Label>
                                    </th>
                                    <th id="Th7" runat="server" align="left">
                                        <asp:Label ID="Label1" runat="server" Text="Scad.Preaut." CssClass="testo_titolo"></asp:Label>
                                    </th>
                                    <th id="th_lente" runat="server">
                                      
                                    </th>
                                    <th id="th_pagamento" runat="server">
                                        <asp:Label ID="Label2" runat="server" Text="Pagamento" CssClass="testo_titolo"></asp:Label>
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
    
    </td>
  </tr>
  <tr>
    <td>
    
        <asp:Label ID="Label76" runat="server" Text="Tot. Preaut." CssClass="testo_bold" />&nbsp;
        <asp:TextBox ID="txtPOS_TotPreaut" runat="server" Width="74px" ReadOnly="true"></asp:TextBox>
    </td>
    <td align="right">
        <asp:Label ID="Label75" runat="server" Text="Tot. Incassato" CssClass="testo_bold" />&nbsp;
        <asp:TextBox ID="txtPOS_TotIncassato" runat="server" Width="74px" ReadOnly="true"></asp:TextBox>
      
      </td>
    </tr>
   </table>



  </div>

<asp:Label ID="lb_num_contratto" runat="server" Text='0' Visible="false"></asp:Label>
<asp:Label ID="lb_num_pren" runat="server" Text='0' Visible="false"></asp:Label>
<asp:Label ID="lb_num_rds" runat="server" Text='0' Visible="false"></asp:Label>
<asp:Label ID="lb_num_multa" runat="server" Text='0' Visible="false"></asp:Label>

<asp:Label ID="lb_th_lente" runat="server" Text='false' Visible="false"></asp:Label>
<asp:Label ID="lb_th_pagamento" runat="server" Text='false' Visible="false"></asp:Label>
<asp:Label ID="lb_PreautorizzazioneRDS" runat="server" Text='false' Visible="false"></asp:Label>
<asp:Label ID="livello_accesso_eliminare_pagamenti" runat="server" Text='false' Visible="false"></asp:Label>


<asp:SqlDataSource ID="sqlDettagliPagamento" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT MOD_PAG.ID_ModPag, ID_TIPPAG, MOD_PAG.Descrizione Des_ID_ModPag, pf.funzione, px.id_pos_funzioni_ares, px.DATA_OPERAZIONE, 
        px.PER_IMPORTO, px.ID_CTR, px.preaut_aperta, px.operazione_stornata, px.scadenza_preaut,
        (CASE WHEN px.N_CONTRATTO_RIF IS NOT NULL THEN 'RA' 
         WHEN px.N_PREN_RIF IS NOT NULL THEN 'PREN'
         WHEN px.N_RDS_RIF IS NOT NULL THEN 'RDS'
         WHEN px.N_MULTA_RIF IS NOT NULL THEN 'MULTA'
         ELSE 'NODEF' END) AS provenienza 
        FROM PAGAMENTI_EXTRA px WITH(NOLOCK) 
        INNER JOIN POS_Funzioni pf WITH(NOLOCK) ON px.id_pos_funzioni_ares = pf.id LEFT JOIN MOD_PAG WITH(NOLOCK) ON px.ID_ModPag = MOD_PAG.ID_ModPag
        WHERE (px.N_CONTRATTO_RIF = @N_CONTRATTO_RIF)
        OR (px.N_PREN_RIF = @N_PREN_RIF)
        OR (px.N_RDS_RIF = @N_RDS_RIF)
        OR (px.N_MULTA_RIF = @N_MULTA_RIF)">
    <SelectParameters>
        <asp:ControlParameter ControlID="lb_num_contratto" Name="N_CONTRATTO_RIF" PropertyName="Text" Type="Int32"  ConvertEmptyStringToNull="true" />
        <asp:ControlParameter ControlID="lb_num_pren" Name="N_PREN_RIF" PropertyName="Text" Type="Int32"  ConvertEmptyStringToNull="true" />
        <asp:ControlParameter ControlID="lb_num_rds" Name="N_RDS_RIF" PropertyName="Text" Type="Int32"  ConvertEmptyStringToNull="true" />
        <asp:ControlParameter ControlID="lb_num_multa" Name="N_MULTA_RIF" PropertyName="Text" Type="Int32"  ConvertEmptyStringToNull="true" />
    </SelectParameters>
</asp:SqlDataSource>