<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preventivo_vedi_calcolo.aspx.vb" Inherits="preventivo_vedi_calcolo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
       <table style="border:4px solid #444; border-top:0px;" border="0" cellspacing="0" cellpadding="0" width="1000px" >
        <tr>
          <td align="left" valign="top">
              <asp:DataList ID="listPreventiviCosti" runat="server" DataSourceID="sqlPreventiviCosti" Width="100%">
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
                             <asp:CheckBox ID="chkOldOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />
                             <asp:CheckBox ID="chkOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />   
                             <asp:Button ID="aggiorna" runat="server" Text="Aggiorna" CommandName="Aggiorna" Visible="false"  /> 
                         </td>
                      </tr>
                  </ItemTemplate>
              </asp:DataList>
             
            </td>
           </tr>
  </table>
    </div>
    


  <asp:SqlDataSource ID="sqlPreventiviCosti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT preventivi_costi.id, gruppi.id_gruppo, gruppi.cod_gruppo As gruppo, preventivi_costi.id_elemento, preventivi_costi.nome_costo, condizioni_elementi.descrizione_lunga, ISNULL((imponibile+iva_imponibile+ISNULL(imponibile_onere,0)+ISNULL(iva_onere,0)),0) As valore_costo, ISNULL(commissioni_imponibile_originale,0)+ISNULL(commissioni_iva_originale,0) As commissioni, ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, ISNULL(imponibile_scontato,0) AS imponibile_scontato, ISNULL(imponibile_onere,0) AS imponibile_onere, ISNULL((iva_imponibile_scontato + ISNULL(iva_onere,0)),0) As iva, ISNULL(preventivi_costi.aliquota_iva,0) As aliquota_iva, id_a_carico_di, preventivi_costi.obbligatorio, preventivi_costi.id_metodo_stampa, ISNULL(preventivi_costi.selezionato,'False') As selezionato, ISNULL(preventivi_costi.omaggiato,'False') As omaggiato, ISNULL(preventivi_costi.omaggiabile,'False') As omaggiabile, ISNULL(preventivi_costi.acquistabile_nolo_in_corso,'False') As acquistabile_nolo_in_corso, ISNULL(preventivi_costi.franchigia_attiva,'False') As franchigia_attiva, id_unita_misura, packed, qta, condizioni_elementi.tipologia_franchigia, condizioni_elementi.tipologia, condizioni_elementi.servizio_rifornimento_tolleranza, preventivi_costi.num_elemento, condizioni_elementi.is_gps FROM preventivi_costi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON preventivi_costi.id_gruppo=gruppi.id_gruppo LEFT JOIN condizioni_elementi WITH(NOLOCK) ON preventivi_costi.id_elemento=condizioni_elementi.id WHERE ((id_documento = @id_preventivo) AND (num_calcolo = @num_calcolo_preventivo) AND (preventivi_costi.id_gruppo= @id_gruppo)) AND ordine_stampa<>'5' AND ISNULL(franchigia_attiva,'1')='1' AND NOT (NOT valore_percentuale IS NULL AND ordine_stampa<>'2') AND (NOT (preventivi_costi.ordine_stampa='7' AND preventivi_costi.franchigia_attiva IS NULL) OR condizioni_elementi.tipologia='KM_EXTRA') AND ISNULL(condizioni_elementi.tipologia,'')<>'RIMUOVI_RIFORNIMENTO' ORDER BY ordine_stampa,ISNULL(condizioni_elementi.ordinamento_vis,0) DESC, nome_costo ">
        <SelectParameters>
            <asp:ControlParameter ControlID="idPreventivo" Name="id_preventivo" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo_preventivo" 
                PropertyName="Text" Type="Int32" />
             <asp:ControlParameter ControlID="idGruppo" Name="id_gruppo" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
  </asp:SqlDataSource>
  
    <asp:Label ID="idPreventivo" runat="server" visible="false"></asp:Label>
    <asp:Label ID="numCalcolo" runat="server" visible="false"></asp:Label>
    <asp:Label ID="idGruppo" runat="server" visible="false"></asp:Label>
    <asp:Label ID="milli" runat="server" visible="false"></asp:Label>
    <asp:Label ID="ultimo_gruppo" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="txtNumeroGiorni" runat="server" visible="false"></asp:Label>


</form>
</body>
</html>