<%@ Page Language="VB" AutoEventWireup="false" CodeFile="contratto_vedi_calcolo.aspx.vb" Inherits="contratto_vedi_calcolo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="StyleSheet" type="text/css" href="css/style.css" />  
    <title></title>
    <style type="text/css">
        .style3
        {
            width: 57px;
        }
        .style4
        {
        }
        .style5
        {
            width: 68px;
        }
        .style6
        {
            width: 155px;
        }
        .style7
        {
            width: 48px;
        }
        .style8
        {
            width: 66px;
        }
        .style9
        {
            width: 74px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <table border="0" cellspacing="2" cellpadding="2" width="1000px" style="border:4px solid #444;">
            <tr>
                <td align="center" style="color: #FFFFFF;background-color:#444;" colspan="4">
                    <asp:Label ID="lblTipoDocumento" runat="server" Text="Contratto Numero:" CssClass="testo_titolo"></asp:Label>&nbsp;
                    <asp:Label ID="lblNumContratto" runat="server"  CssClass="testo_titolo"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label15" runat="server" Text="Data Contratto:" CssClass="testo_titolo"></asp:Label>&nbsp;
                    <asp:Label ID="lblDataContratto" runat="server"  CssClass="testo_titolo"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblVariazione" runat="server" Text="Variazione:" CssClass="testo_titolo"></asp:Label>&nbsp;
                    <asp:Label ID="numVariazione" runat="server"  CssClass="testo_titolo"></asp:Label>
                    <br />&nbsp;
                    <asp:Label ID="lblDaPrenotazione" runat="server" Text="Da Pren. Num.:" CssClass="testo_titolo" Visible="false"></asp:Label>&nbsp;
                    <asp:Label ID="lblNumPren" runat="server"  CssClass="testo_titolo" Visible="false" Text="-1"></asp:Label>&nbsp;
                </td>
             </tr>
           </table>
           <table border="0" cellspacing="2" cellpadding="2" width="1000px" style="border:4px solid #444;">
           <tr>
              <td valign="top" class="style3">
                 <asp:Label ID="Label2" runat="server" Text="USCITA" CssClass="testo_bold"></asp:Label>
              </td>
              <td valign="top" class="style4">
                  <asp:Label ID="dropStazionePickUp" runat="server" Text="" CssClass="testo"></asp:Label>
              </td>
              <td valign="top" class="style5">
                <asp:Label ID="txtDaData" runat="server" Text="" CssClass="testo"></asp:Label>
              </td>
              <td valign="top" class="style6">
                 <asp:Label ID="txtoraPartenza" runat="server" Text="" CssClass="testo"></asp:Label>
                 <asp:TextBox ID="ore1" runat="server" Width="29px" MaxLength="2" Visible="false"></asp:TextBox>
                 <asp:TextBox ID="minuti1" runat="server" Width="29px" MaxLength="2" Visible="false"></asp:TextBox>  
              </td>
               <td class="style7">
               </td>
               <td class="style8">
               </td>
               <td class="style9">
                   &nbsp;</td>
               <td>
                   &nbsp;</td>
          </tr>
          <tr>
            <td valign="top" class="style3">
               <asp:Label ID="Label1" runat="server" Text="P.RIENTRO" CssClass="testo_bold"></asp:Label>
              </td>
              <td valign="top" class="style4">
                <asp:Label ID="dropStazioneDropOff" runat="server" Text="" CssClass="testo"></asp:Label>
              </td>
              <td valign="top" class="style5">
                <asp:Label ID="txtAData" runat="server" Text="" CssClass="testo"></asp:Label>
              </td>
              <td valign="top" class="style6">
                <asp:Label ID="txtOraRientro" runat="server" Text="" CssClass="testo"></asp:Label>
                <asp:TextBox ID="ore2" runat="server" Width="29px" MaxLength="2" Visible="false"></asp:TextBox>
                <asp:TextBox ID="minuti2" runat="server" Width="29px" MaxLength="2" Visible="false"></asp:TextBox>
              </td>
              <td class="style7">
               <asp:Label ID="Label16" runat="server" Text="GIORNI" CssClass="testo_bold"></asp:Label>
              </td>
              <td class="style8">
                <asp:Label ID="txtGiorni" runat="server" Text="" CssClass="testo"></asp:Label>
              </td>
              <td class="style9">
         <asp:Label ID="lblGiorniTO" runat="server" Text="Giorni T.O." CssClass="testo_bold"></asp:Label>
                  <br />
         <asp:Label ID="lblGiorniPrepagati" runat="server" Text="Giorni Prep." CssClass="testo_bold"></asp:Label>
              </td>
              <td>
                <asp:Label ID="txtNumeroGiorniTO" runat="server" Text="" CssClass="testo"></asp:Label>
                <asp:Label ID="txtGiorniPrepagati" runat="server" Text="" CssClass="testo"></asp:Label>
              </td>
          </tr>
          <tr>
            <td valign="top" class="style3">
               <asp:Label ID="Label17" runat="server" Text="TARIFFA" CssClass="testo_bold"></asp:Label>
              </td>
              <td valign="top" class="style4" colspan="3">
                <asp:Label ID="lblTariffa" runat="server" Text="" CssClass="testo"></asp:Label>
              </td>
              <td class="style7">
                  &nbsp;</td>
              <td class="style8">
                  &nbsp;</td>
              <td class="style9">
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
          </tr>
         </table>
       <table style="border:4px solid #444; border-top:0px;" border="0" cellspacing="0" cellpadding="0" width="1000px" >
        <tr>
              <td valign="top">
                <asp:Label ID="lblTestoDaPrenotazione" runat="server" Text="Differenza da pren.: " Font-Size="16px" CssClass="testo_bold" Visible="false"></asp:Label>    
                <asp:Label ID="lblDifferenzaDaPrenotazione" runat="server" ForeColor="Blue" CssClass="testo_bold" Visible="false" Font-Size="16px"></asp:Label>
                &nbsp;<asp:Label ID="lblEuroDaPrenotazione" runat="server" ForeColor="Blue" Text="€" CssClass="testo_bold" Visible="false" Font-Size="16px"></asp:Label>
          </td>
        </tr>
        <tr>
          <td align="left" valign="top">
              <asp:DataList ID="listContrattiCosti" runat="server" DataSourceID="sqlContrattiCosti" Width="100%">
                  <ItemTemplate>
                      <tr runat="server" id="riga_gruppo" >
                         <td bgcolor="#19191b" style="width:100%;" colspan="13">
                              <asp:Label ID="Label7" runat="server" Text="Gruppo:" CssClass="testo"  />
                              <asp:Label ID="id_gruppoLabel" runat="server" Text='<%# Eval("id_gruppo") %>' Visible="false" />
                              <asp:Label ID="gruppo" runat="server" Text='<%# Eval("gruppo") %>' CssClass="testo_bold" />
                         </td>
                      </tr>
                      <tr runat="server" id="riga_intestazione_informative" visible="false">
                         <td bgcolor="#19191b"  colspan="12">
                             <asp:Label ID="Label50" runat="server" Text='Penalità' CssClass="testo_bold" />
                         </td>
                         <td bgcolor="#19191b" >
                             <asp:Label ID="Label49" runat="server" Text='Non Pag.' CssClass="testo_bold" />
                         </td>
                      </tr>
                      <tr runat="server" id="riga_intestazione">
                         <td width="3%">
                         </td>
                         <td>
                         </td>
                         <td width="30%">
                         </td>
                         <td>
                           <asp:Label ID="labelTO" runat="server" Text='T.O.' CssClass="testo_bold" Visible="false"/>
                           <asp:Label ID="labelPrepagato" runat="server" Text='Prepag.' CssClass="testo_bold" Visible="false"/>
                           <asp:Label ID="labelCommissioni" runat="server" Text='Comm.' CssClass="testo_bold" ToolTip="Commissioni" Visible="false"/>&nbsp;
                         </td>
                         <td>
                            <asp:Label ID="Label13" runat="server" Text='Sconto' CssClass="testo_bold" />&nbsp;
                         </td>
                         <td>
                            <asp:Label ID="Label46" runat="server" Text='Costo U.' CssClass="testo_bold" />&nbsp;
                         </td>
                         <td>
                            <asp:Label ID="Label47" runat="server" Text='Qta' CssClass="testo_bold"  />&nbsp;
                         </td>
                         <td>
                            <asp:Label ID="Label65" runat="server" Text='Costo' CssClass="testo_bold"  />&nbsp;
                         </td>
                         <td>
                            <asp:Label ID="Label114" runat="server" Text='Onere' CssClass="testo_bold"  />&nbsp;
                         </td>
                         <td>
                            <asp:Label ID="Label115" runat="server" Text='IVA' CssClass="testo_bold"  />&nbsp;
                         </td>
                         <td>
                            <asp:Label ID="Label126" runat="server" Text='Aliq.' CssClass="testo_bold"  />&nbsp;
                         </td>
                         <td>
                            <asp:Label ID="Label12" runat="server" Text='TOT.' CssClass="testo_bold" />&nbsp;
                         </td>
                         <td width="3%">
                           <asp:Label ID="Label21" runat="server" Text='Omaggio' CssClass="testo_bold" />
                         </td>
                      </tr>
                      <tr runat="server" id="riga_elementi">
                         <td width="3%" align="left" >
                             <asp:CheckBox ID="chkOldScegli" runat="server" Visible="false" Checked='<%# Eval("selezionato") %>' />
                             <asp:CheckBox ID="chkScegli" runat="server" Visible="false" Checked='<%# Eval("selezionato") %>' />
                             <asp:Label ID="lblIncluso" runat="server" Text="I " ToolTip="Incluso" CssClass="testo_bold" Visible="false"  />
                             <asp:Label ID="lblObbligatorio" runat="server" Text="O " ToolTip="Obbligatorio" CssClass="testo_bold" Visible="false"  />
                             <asp:Label ID="lblInformativa" runat="server" Text="IN " ToolTip="Informativa" CssClass="testo_bold" Visible="false"  />
                         </td>
                         <td width="2%">
                             <asp:Label ID="lblPrepagato" runat="server" Text="P" ToolTip="Prepagato" CssClass="testo_bold" Visible="false"  />&nbsp;
                         </td>
                         <td width="30%">
                              <asp:Label ID="id_preventivi_costi" runat="server" Text='<%# Eval("id") %>' visible="false" />
                              <asp:Label ID="id_elemento" runat="server" Text='<%# Eval("id_elemento") %>' visible="false" />
                              <asp:Label ID="num_elemento" runat="server" Text='<%# Eval("num_elemento") %>' visible="false" />
                              <asp:Label ID="is_gps" runat="server" Text='<%# Eval("is_gps") %>' visible="false" />
                              <asp:Label ID="nome_costo" runat="server" Text='<%# Eval("nome_costo") %>' CssClass="testo" />:&nbsp;&nbsp;
                         </td>
                         <td>
                              <asp:Label ID="a_carico_to" runat="server" Text="" CssClass="testo" Visible="false"/>
                              <asp:Label ID="costo_prepagato" runat="server" Text='<%# FormatNumber(Eval("valore_prepagato"),2) %>' CssClass="testo" Visible="false"/>
                              <asp:Label ID="lblCommissioni" runat="server" Text='<%# FormatNumber(Eval("commissioni"),2) %>' CssClass="testo" Visible="false"/>
                         </td>
                         <td>
                              <asp:Label ID="lblSconto" runat="server" Text='<%# FormatNumber(Eval("sconto"),2) %>' CssClass="testo"/>
                         </td>
                         <td>
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
                              <asp:Label ID="tipologia" runat="server" Text='<%# Eval("tipologia") %>' Visible="false" />
                              <asp:Label ID="servizio_rifornimento_tolleranza" runat="server" Text='<%# Eval("servizio_rifornimento_tolleranza") %>' Visible="false" />
                         </td>
                         <td>
                              <asp:Label ID="qta" runat="server" Visible="True" CssClass="testo" Text='<%# Eval("qta") %>' />
                         </td>
                         <td>
                              <asp:Label ID="imponibile" runat="server" Visible="True" CssClass="testo" Text='<%# FormatNumber(Eval("imponibile_scontato"),2) %>' />
                         </td>
                         <td>
                              <asp:Label ID="onere" runat="server" Visible="True" CssClass="testo" Text='<%# FormatNumber(Eval("imponibile_onere"),2) %>' />
                         </td>
                         <td>
                              <asp:Label ID="iva" runat="server" Visible="True" CssClass="testo" Text='<%# FormatNumber(Eval("iva"),2) %>' />
                         </td>
                         <td>
                              <asp:Label ID="aliquota_iva" runat="server" Visible="True" CssClass="testo" Text='<%# Eval("aliquota_iva") & " %" %>' />
                         </td>
                         <td>
                            <asp:Label ID="costo_scontato_inv" runat="server" Text='<%# FormatNumber(Eval("valore_costo") - Eval("sconto"),2) %>' Visible="false" />
                            <asp:Label ID="costo_scontato" runat="server" Text='<%# FormatNumber(Eval("valore_costo") - Eval("sconto"),2) %>' CssClass="testo" />&nbsp;
                         </td>
                         <td align="center" width="3%">
                             <asp:Label ID="omaggiabile" runat="server" Text='<%# Eval("omaggiabile") %>' Visible="false" />
                             <asp:Label ID="acquistabile_nolo_in_corso" runat="server" Text='<%# Eval("acquistabile_nolo_in_corso") %>' Visible="false" />
                             <asp:Label ID="prepagato" runat="server" Text='<%# Eval("prepagato") %>' Visible="false" />
                             <asp:CheckBox ID="chkOldOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />
                             <asp:CheckBox ID="chkOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />   
                             <asp:Button ID="aggiorna" runat="server" Text="Aggiorna" CommandName="Aggiorna" Visible="false"  /> 
                         </td>
                      </tr>
                  </ItemTemplate>
              </asp:DataList>
              <%--<asp:DataList ID="listContrattiCosti" runat="server" DataSourceID="sqlContrattiCosti" Width="100%" Visible="false">
                  <ItemTemplate>
                      <tr runat="server" id="riga_gruppo" >
                         <td bgcolor="#19191b" style="width:100%;" colspan="8">
                              <asp:Label ID="Label7" runat="server" Text="Gruppo:" CssClass="testo"  />
                              <asp:Label ID="id_gruppoLabel" runat="server" Text='<%# Eval("id_gruppo") %>' Visible="false" />
                              <asp:Label ID="gruppo" runat="server" Text='<%# Eval("gruppo") %>' CssClass="testo_bold" />
                         </td>
                      </tr>
                      <tr runat="server" id="riga_intestazione_informative" visible="false">
                         <td bgcolor="#19191b"  colspan="7">
                             <asp:Label ID="Label50" runat="server" Text='Penalità' CssClass="testo_bold" />
                         </td>
                         <td bgcolor="#19191b" >
                             <asp:Label ID="Label49" runat="server" Text='Non Pag.' CssClass="testo_bold" />
                         </td>
                      </tr>
                      <tr runat="server" id="riga_intestazione">
                         <td width="5%">
                         </td>
                         <td width="42%">
                         </td>
                         <td width="11%">
                           <asp:Label ID="labelTO" runat="server" Text='T.O.' CssClass="testo_bold" Visible="false"/>&nbsp;
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
                         <td width="3%">
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
                         <td width="42%">
                              <asp:Label ID="id_preventivi_costi" runat="server" Text='<%# Eval("id") %>' visible="false" />
                              <asp:Label ID="id_elemento" runat="server" Text='<%# Eval("id_elemento") %>' visible="false" />
                              <asp:Label ID="nome_costo" runat="server" Text='<%# Eval("nome_costo") %>' CssClass="testo" />:&nbsp;&nbsp;
                         </td>
                         <td width="11%">
                              <asp:Label ID="a_carico_to" runat="server" Text="" CssClass="testo" Visible="false"/>
                         </td>
                         <td width="10%">
                              <asp:Label ID="lblSconto" runat="server" Text='<%# FormatNumber(Eval("sconto"),2) %>' CssClass="testo"/>
                         </td>
                         <td width="11%">
                              <asp:Label ID="lbl_costo_unitario" runat="server" Visible="True" CssClass="testo" />
                              
                              <asp:Label ID="valore_costoLabel" runat="server" Text='<%# FormatNumber(Eval("valore_costo"),2) %>' Visible="false" />&nbsp;
                              <asp:Label ID="id_unita_misura" runat="server" Text='<%# Eval("id_unita_misura") %>' Visible="false" />
                              <asp:Label ID="packed" runat="server" Text='<%# Eval("packed") %>' Visible="false" />
                              <asp:Label ID="id_a_carico_di" runat="server" Text='<%# Eval("id_a_carico_di") %>' Visible="false" />
                              <asp:Label ID="obbligatorio" runat="server" Text='<%# Eval("obbligatorio") %>' Visible="false" /> 
                              <asp:Label ID="id_metodo_stampa" runat="server" Text='<%# Eval("id_metodo_stampa") %>' Visible="false" />                    
                              <asp:Label ID="franchigia_attiva" runat="server" Text='<%# Eval("franchigia_attiva") %>' Visible="false" />
                              <asp:Label ID="data_aggiunta_nolo_in_corso" runat="server" Text='<%# Eval("data_aggiunta_nolo_in_corso") %>' Visible="false" />
                              
                         </td>
                         <td width="4%">
                              <asp:Label ID="qta" runat="server" Visible="True" CssClass="testo" Text='<%# Eval("qta") %>' />
                         </td>
                         <td width="14%">
                            <asp:Label ID="costo_scontato" runat="server" Text='<%# FormatNumber(FormatNumber(Eval("valore_costo"),2) - FormatNumber(Eval("sconto"),2),2) %>' CssClass="testo" />&nbsp;
                         </td>
                         <td align="center" width="3%">
                             <asp:Label ID="omaggiabile" runat="server" Text='<%# Eval("omaggiabile") %>' Visible="false" />
                             <asp:Label ID="acquistabile_nolo_in_corso" runat="server" Text='<%# Eval("acquistabile_nolo_in_corso") %>' Visible="false" />
                             <asp:CheckBox ID="chkOldOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />
                             <asp:CheckBox ID="chkOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />   
                         </td>
                      </tr>
                  </ItemTemplate>
              </asp:DataList>--%>
            </td>
           </tr>
  </table>
    </div>
    
    <%--<asp:SqlDataSource ID="sqlContrattiCosti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT contratti_costi.id, gruppi.id_gruppo, gruppi.cod_gruppo As gruppo, contratti_costi.id_elemento, contratti_costi.nome_costo, condizioni_elementi.descrizione_lunga, ISNULL((imponibile+iva_imponibile),0) As valore_costo, ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, id_a_carico_di, contratti_costi.obbligatorio, contratti_costi.id_metodo_stampa, ISNULL(contratti_costi.selezionato,'False') As selezionato, ISNULL(contratti_costi.omaggiato,'False') As omaggiato, ISNULL(contratti_costi.omaggiabile,'False') As omaggiabile,ISNULL(contratti_costi.acquistabile_nolo_in_corso,'False') As acquistabile_nolo_in_corso, ISNULL(contratti_costi.franchigia_attiva,'False') As franchigia_attiva, id_unita_misura, packed, qta, data_aggiunta_nolo_in_corso  FROM contratti_costi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON contratti_costi.id_gruppo=gruppi.id_gruppo LEFT JOIN condizioni_elementi WITH(NOLOCK) ON contratti_costi.id_elemento=condizioni_elementi.id WHERE ((id_documento = @id_contratto) AND (num_calcolo = @num_calcolo_contratto)) AND ordine_stampa<>'5' AND ISNULL(franchigia_attiva,'1')='1' AND NOT (contratti_costi.ordine_stampa='7' AND contratti_costi.franchigia_attiva IS NULL) AND NOT (NOT valore_percentuale IS NULL AND ordine_stampa<>'2') ORDER BY id_gruppo, secondo_ordine_stampa, ordine_stampa, ISNULL(condizioni_elementi.ordinamento_vis,0) DESC, nome_costo ">
        <SelectParameters>
            <asp:ControlParameter ControlID="idContratto" Name="id_contratto" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo_contratto" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
  </asp:SqlDataSource>--%>

  <asp:SqlDataSource ID="sqlContrattiCosti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT contratti_costi.id, gruppi.id_gruppo, gruppi.cod_gruppo As gruppo, contratti_costi.id_elemento, contratti_costi.nome_costo, contratti_costi.prepagato, condizioni_elementi.descrizione_lunga, ISNULL((imponibile+iva_imponibile+ISNULL(imponibile_onere,0)+ISNULL(iva_onere,0)) - (ISNULL(imponibile_scontato_prepagato,0)+ISNULL(iva_imponibile_scontato_prepagato,0)+ISNULL(imponibile_onere_prepagato,0)+ISNULL(iva_onere_prepagato,0)),0) As valore_costo, ISNULL(commissioni_imponibile_originale,0)+ISNULL(commissioni_iva_originale,0) As commissioni, (ISNULL(imponibile_scontato_prepagato,0)+ISNULL(iva_imponibile_scontato_prepagato,0)+ISNULL(imponibile_onere_prepagato,0)+ISNULL(iva_onere_prepagato,0)) As valore_prepagato, ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, ISNULL(imponibile_scontato,0) - ISNULL(imponibile_scontato_prepagato,0) AS imponibile_scontato, ISNULL(imponibile_onere,0) - ISNULL(imponibile_onere_prepagato,0) AS imponibile_onere, ISNULL((iva_imponibile_scontato + ISNULL(iva_onere,0))-(ISNULL(iva_imponibile_scontato_prepagato,0)+ISNULL(iva_onere_prepagato,0)),0) As iva, ISNULL(contratti_costi.aliquota_iva,0) As aliquota_iva, id_a_carico_di, contratti_costi.obbligatorio, contratti_costi.id_metodo_stampa, ISNULL(contratti_costi.selezionato,'False') As selezionato, ISNULL(contratti_costi.omaggiato,'False') As omaggiato, (CASE WHEN prepagato='1' THEN 'False' ELSE ISNULL(contratti_costi.omaggiabile,'False') END) As omaggiabile, ISNULL(contratti_costi.acquistabile_nolo_in_corso,'False') As acquistabile_nolo_in_corso, ISNULL(contratti_costi.franchigia_attiva,'False') As franchigia_attiva, id_unita_misura, packed, qta, data_aggiunta_nolo_in_corso, condizioni_elementi.tipologia_franchigia, condizioni_elementi.tipologia, condizioni_elementi.servizio_rifornimento_tolleranza, contratti_costi.num_elemento, condizioni_elementi.is_gps FROM contratti_costi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON contratti_costi.id_gruppo=gruppi.id_gruppo LEFT JOIN condizioni_elementi WITH(NOLOCK) ON contratti_costi.id_elemento=condizioni_elementi.id WHERE ((id_documento = @id_contratto) AND (num_calcolo = @num_calcolo_contratto)) AND ordine_stampa<>'5' AND ISNULL(franchigia_attiva,'1')='1' AND NOT (NOT valore_percentuale IS NULL AND ordine_stampa<>'2') ORDER BY id_gruppo, secondo_ordine_stampa, ordine_stampa,ISNULL(condizioni_elementi.ordinamento_vis,0) DESC, nome_costo ">
        <SelectParameters>
            <asp:ControlParameter ControlID="idContratto" Name="id_contratto" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo_contratto" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
  </asp:SqlDataSource>
  
    <asp:Label ID="idContratto" runat="server" visible="false"></asp:Label>
    <asp:Label ID="numCalcolo" runat="server" visible="false"></asp:Label>
    <asp:Label ID="milli" runat="server" visible="false"></asp:Label>
    <asp:Label ID="tariffa_broker" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="ultimo_gruppo" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="livello_accesso_broker" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="a_carico_del_broker" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="complimentary" runat="server" Text="" Visible="false"></asp:Label>
    
    </form>
</body>
</html>
