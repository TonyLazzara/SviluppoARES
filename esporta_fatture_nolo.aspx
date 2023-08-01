<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="esporta_fatture_nolo.aspx.vb" Inherits="esporta_fatture_nolo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    
    <script type="text/javascript" language="javascript" src="/lytebox.js"></script>
    <style type="text/css">
        .style1
        {
            width: 234px;
        }
        .style2
        {
            width: 150px;
        }
        .style3
        {
            width: 85px;
        }
        .style5
        {
            width: 246px;
        }
        .style6
        {
            width: 132px;
        }
        .style7
        {
            width: 102px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
            <tr>
            <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;" class="style1">
                <asp:Label ID="Label14" runat="server" Text="Esportazione Fatture Contratti"></asp:Label>
            </td>
            </tr>
 </table>

 <div runat="server" id="div_cerca" visible="true">
 <table runat="server" id="tab_ricerca1" border="0" cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444;">
       <tr>
           <td class="style3">
             <asp:Label ID="Label3" runat="server" Text="Anno Fattura" CssClass="testo_bold"></asp:Label>
           </td>
           <td class="style2">
             <asp:Label ID="Label36" runat="server" Text="Numero Fattura Da" CssClass="testo_bold"></asp:Label>
           </td>
          <td class="style6">
             <asp:Label ID="Label37" runat="server" Text="Numero Fattura A" CssClass="testo_bold"></asp:Label>
           </td>
           <td class="style7">
             <asp:Label ID="Label1" runat="server" Text="Data Fattura Da" CssClass="testo_bold"></asp:Label>
           </td>
           <td class="style5">
             <asp:Label ID="Label2" runat="server" Text="Data Fattura A" CssClass="testo_bold"></asp:Label>
           </td>
       </tr>
       <tr>
           <td class="style3">
               <asp:DropDownList ID="dropAnnoFattura" runat="server" AppendDataBoundItems="True">
               </asp:DropDownList>
           </td>
           <td class="style2">
               <asp:TextBox ID="txtNumeroFatturaDa" runat="server" 
                   onKeyPress="return filterInputInt(event)" Width="60px"></asp:TextBox>
           </td>
          <td class="style6">
              <asp:TextBox ID="txtNumeroFatturaA" runat="server" 
                  onKeyPress="return filterInputInt(event)" Width="60px"></asp:TextBox>
           </td>
           <td class="style7">
                <asp:TextBox runat="server" Width="70px" ID="txtDataFatturaDa" ></asp:TextBox>
                    <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                              TargetControlID="txtDataFatturaDa" 
                              ID="CalendarExtender5">
                    </ajaxtoolkit:CalendarExtender>
                    <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                              CultureDatePlaceholder="" CultureTimePlaceholder="" 
                              CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                              CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                              CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataFatturaDa" 
                              ID="MaskedEditExtender6">
                    </ajaxtoolkit:MaskedEditExtender>
           </td>
           <td class="style5">
                <asp:TextBox runat="server" Width="70px" ID="txtDataFatturaA" ></asp:TextBox>
                    <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                              TargetControlID="txtDataFatturaA" 
                              ID="CalendarExtender3">
                    </ajaxtoolkit:CalendarExtender>
                    <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                              CultureDatePlaceholder="" CultureTimePlaceholder="" 
                              CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                              CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                              CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataFatturaA" 
                              ID="MaskedEditExtender2">
                    </ajaxtoolkit:MaskedEditExtender>
           </td>
       </tr>
       <tr>
           <td colspan="5" align="center">
             <asp:Button ID="btnCerca" runat="server" Text="Cerca" UseSubmitBehavior="False" />
           </td>
       </tr>
       <tr>
           <td colspan="5" align="center">
             <asp:Button ID="btnClientiFase1" runat="server" Text="Esporta Clienti - Controllo Dati" UseSubmitBehavior="False" />
             &nbsp;
             <asp:Button ID="btnClientiFase2" runat="server" Text="Esporta Clienti - Fase 2" UseSubmitBehavior="False" />
             &nbsp;
             <asp:Button ID="btnEsportaFatture" runat="server" Text="Esporta Fatture" UseSubmitBehavior="False" />
             &nbsp;
             <asp:Button ID="btnEsportaPagamenti" runat="server" Text="Esporta Pagamenti" UseSubmitBehavior="False" />
             &nbsp;
             <asp:Button ID="btnEsportaOttico" runat="server" Text="Esporta Ottico" UseSubmitBehavior="False" />
           </td>
       </tr>
 </table>

 <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444; border-top:0px;" border="0">
       <tr>
         <td>
             <asp:ListView ID="listDaFatturare" runat="server" DataKeyNames="id" DataSourceID="sqlFatture" >
                  <ItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000;">
                          <td align="left">
                              <asp:Label ID="id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("num_fattura") %>' CssClass="testo_piccolo" CommandArgument='<%# Eval("id") %>' CommandName="vedi_fattura"></asp:LinkButton> 
                          </td>
                          <td>
                               <asp:Label ID="lblDataFattura" runat="server" Text='<%# Eval("data_fattura") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td align="left">
                               <asp:CheckBox ID="chkCliente" runat="server" Checked='<%# Eval("fattura_cliente") %>' Visible="false"  />
                               <asp:CheckBox ID="chkBroker" runat="server" Checked='<%# Eval("fattura_broker") %>' Visible="false" />
                               <asp:CheckBox ID="chkPrepagato" runat="server" Checked='<%# Eval("fattura_costi_prepagati") %>' Visible="false" />
                               <asp:Label ID="lblTipoFattura" runat="server" Text="" CssClass="testo_piccolo" Font-Bold="true"></asp:Label>
                          </td>
                          <td align="left">
                              <asp:LinkButton ID="num_contratto" runat="server" Text='<%# Eval("num_contratto_rif") %>' CssClass="testo_piccolo" CommandName="vedi_ra"></asp:LinkButton> 
                          </td>
                          <td>
                               <asp:Label ID="lblNumPrenotazione" runat="server" Text='<%# Eval("num_prenotazione_rif") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td>
                               <asp:Label ID="lblDataPren" runat="server" Text='<%# Eval("datapren") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td>
                               <asp:Label ID="lblDitta" runat="server" Text='<%# Eval("ditta") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td>
                               <asp:Label ID="lblSaldo" runat="server" Text='<%# Eval("saldo") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr style="">
                          <td align="left">
                              <asp:Label ID="id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("num_fattura") %>' CssClass="testo_piccolo" CommandArgument='<%# Eval("id") %>' CommandName="vedi_fattura"></asp:LinkButton> 
                          </td>
                          <td>
                               <asp:Label ID="lblDataFattura" runat="server" Text='<%# Eval("data_fattura") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td align="left">
                               <asp:CheckBox ID="chkCliente" runat="server" Checked='<%# Eval("fattura_cliente") %>' Visible="false"  />
                               <asp:CheckBox ID="chkBroker" runat="server" Checked='<%# Eval("fattura_broker") %>' Visible="false" />
                               <asp:CheckBox ID="chkPrepagato" runat="server" Checked='<%# Eval("fattura_costi_prepagati") %>' Visible="false" />
                               <asp:Label ID="lblTipoFattura" runat="server" Text="" CssClass="testo_piccolo" Font-Bold="true"></asp:Label>
                          </td>
                          <td align="left">
                              <asp:LinkButton ID="num_contratto" runat="server" Text='<%# Eval("num_contratto_rif") %>'  CssClass="testo_piccolo" CommandName="vedi_ra"></asp:LinkButton> 
                          </td>
                          <td>
                               <asp:Label ID="lblNumPrenotazione" runat="server" Text='<%# Eval("num_prenotazione_rif") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td>
                               <asp:Label ID="lblDataPren" runat="server" Text='<%# Eval("datapren") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td>
                               <asp:Label ID="lblDitta" runat="server" Text='<%# Eval("ditta") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td>
                               <asp:Label ID="lblSaldo" runat="server" Text='<%# Eval("saldo") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                      </tr>
                  </AlternatingItemTemplate>
                  <EmptyDataTemplate>
                      <table id="Table1" runat="server" style="">
                          <tr>
                              <td>
                                  Non è stato restituito alcun dato.</td>
                          </tr>
                      </table>
                  </EmptyDataTemplate>
                  <LayoutTemplate>
                      <table id="Table2" runat="server" width="100%">
                          <tr id="Tr1" runat="server">
                              <td id="Td1" runat="server">
                                  <table ID="itemPlaceholderContainer" runat="server" border="1"  width="100%"   style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;font-size:small;">
                                      <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                          <th id="Th5" runat="server" align="left">
                                              <asp:Label ID="Label6" runat="server" Text="Num." CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th14" runat="server" align="left">
                                              <asp:Label ID="Label16" runat="server" Text="Data Fatt." CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th2" runat="server" align="left">
                                              <asp:Label ID="Label8" runat="server" Text="Tipo" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th1" runat="server" align="left">
                                              <asp:Label ID="Label7" runat="server" Text="Contratto" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th3" runat="server" align="left">
                                              <asp:Label ID="Label9" runat="server" Text="Num. Pren." CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th11" runat="server" align="left">
                                              <asp:Label ID="Label12" runat="server" Text="Data Pren." CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th12" runat="server" align="left">
                                              <asp:Label ID="Label13" runat="server" Text="Edp - Rag.Soc." CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th13" runat="server" align="left">
                                              <asp:Label ID="Label15" runat="server" Text="Saldo" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                      </tr>
                                      <tr ID="itemPlaceholder" runat="server">
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                          <tr id="Tr3" runat="server">
                              <td id="Td2" runat="server" style="" align="left">
                                  <asp:DataPager ID="DataPager1" runat="server" PageSize="100">
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
</div>
<div runat="server" id="div_fase2" visible="false">
  <table runat="server" id="Table3" border="0" cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444;">
       <tr>
           <td align="center">
               <asp:Button ID="btnLeggiClicon" runat="server" Text="Leggi file Risposta" />
                  &nbsp;
               <asp:Button ID="btnEseguiModifiche" runat="server" Text="Esegui Modifiche" OnClientClick="javascript: return(window.confirm ('Attenzione - Sei sicuro di voler modificare le ditte a cui fatturare come specificato in lista? L\'operazione non è reversibile.'));"/>
                  &nbsp;
               <asp:Button ID="btnAnnulla" runat="server" Text="Annulla" />
           </td>
       </tr>
       <tr>
           <td align="center">
              <asp:ListView ID="listFase2" runat="server" DataKeyNames="id_cliente_fattura" DataSourceID="sqlFase2" >
                  <ItemTemplate>
                      <tr>
                         <td align="left" style="background-color:#DCDCDC;color: #000000;">
                              <asp:Label ID="id_cliente_fattura" runat="server" Text='<%# Eval("id_cliente_fattura") %>' Visible="false"></asp:Label>
                              <asp:Label ID="id_cliente_precedente" runat="server" Text='<%# Eval("id_cliente_precedente") %>' Visible="false"></asp:Label>
                              <asp:RadioButton ID="radioClienteFattura" runat="server" GroupName="gruppo" />
                          </td>
                          <td align="left" style="background-color:#DCDCDC;color: #000000;">
                              <asp:Label ID="Label4" runat="server" Text='<%# Eval("rag_soc1") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td align="left" style="background-color:#DCDCDC;color: #000000;">
                              <asp:Label ID="Label5" runat="server" Text='<%# Eval("piva1") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td align="left" style="background-color:#DCDCDC;color: #000000;">
                              <asp:Label ID="Label10" runat="server" Text='<%# Eval("indirizzo1") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td align="left" style="background-color:#DCDCDC;color: #000000;">
                              <asp:Label ID="Label11" runat="server" Text='<%# Eval("citta1") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td align="left">
                             <asp:RadioButton ID="radioClientePrecedente" runat="server" GroupName="gruppo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="codice_edp" runat="server" Text='<%# Eval("codice_edp") %>' Visible="false"></asp:Label>
                              <asp:Label ID="cap" runat="server" Text='<%# Eval("cap") %>' Visible="false"></asp:Label>
                              <asp:Label ID="provincia" runat="server" Text='<%# Eval("provincia") %>' Visible="false"></asp:Label>
                              <asp:Label ID="id_nazione" runat="server" Text='<%# Eval("id_nazione") %>' Visible="false"></asp:Label>
                              <asp:Label ID="nazione" runat="server" Text='<%# Eval("nazione2") %>' Visible="false"></asp:Label>
                              <asp:Label ID="email" runat="server" Text='<%# Eval("email") %>' Visible="false"></asp:Label>
                              <asp:Label ID="rag_soc2" runat="server" Text='<%# Eval("rag_soc2") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td align="left">
                              <asp:Label ID="piva2" runat="server" Text='<%# Eval("piva2") %>' CssClass="testo_piccolo"></asp:Label>
                              <asp:Label ID="codice_fiscale2" runat="server" Text='<%# Eval("c_Fis2") %>' Visible="false"></asp:Label>
                          </td>
                          <td align="left">
                              <asp:Label ID="indirizzo2" runat="server" Text='<%# Eval("indirizzo2") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td align="left">
                              <asp:Label ID="citta2" runat="server" Text='<%# Eval("citta2") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr style="">
                          <td align="left" style="background-color:#DCDCDC;color: #000000;">
                              <asp:Label ID="id_cliente_fattura" runat="server" Text='<%# Eval("id_cliente_fattura") %>' Visible="false"></asp:Label>
                              <asp:Label ID="id_cliente_precedente" runat="server" Text='<%# Eval("id_cliente_precedente") %>' Visible="false"></asp:Label>
                              <asp:RadioButton ID="radioClienteFattura" runat="server" GroupName="gruppo" />
                          </td>
                          <td align="left" style="background-color:#DCDCDC;color: #000000;">
                              <asp:Label ID="Label4" runat="server" Text='<%# Eval("rag_soc1") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td align="left" style="background-color:#DCDCDC;color: #000000;">
                              <asp:Label ID="Label5" runat="server" Text='<%# Eval("piva1") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td align="left" style="background-color:#DCDCDC;color: #000000;">
                              <asp:Label ID="Label10" runat="server" Text='<%# Eval("indirizzo1") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td align="left" style="background-color:#DCDCDC;color: #000000;">
                              <asp:Label ID="Label11" runat="server" Text='<%# Eval("citta1") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td align="left">
                             <asp:RadioButton ID="radioClientePrecedente" runat="server" GroupName="gruppo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="codice_edp" runat="server" Text='<%# Eval("codice_edp") %>' Visible="false"></asp:Label>
                              <asp:Label ID="cap" runat="server" Text='<%# Eval("cap") %>' Visible="false"></asp:Label>
                              <asp:Label ID="provincia" runat="server" Text='<%# Eval("provincia") %>' Visible="false"></asp:Label>
                              <asp:Label ID="id_nazione" runat="server" Text='<%# Eval("id_nazione") %>' Visible="false"></asp:Label>
                              <asp:Label ID="nazione" runat="server" Text='<%# Eval("nazione2") %>' Visible="false"></asp:Label>
                              <asp:Label ID="email" runat="server" Text='<%# Eval("email") %>' Visible="false"></asp:Label>
                              <asp:Label ID="rag_soc2" runat="server" Text='<%# Eval("rag_soc2") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td align="left">
                              <asp:Label ID="piva2" runat="server" Text='<%# Eval("piva2") %>' CssClass="testo_piccolo"></asp:Label>
                              <asp:Label ID="codice_fiscale2" runat="server" Text='<%# Eval("c_Fis2") %>' Visible="false"></asp:Label>
                          </td>
                          <td align="left">
                              <asp:Label ID="indirizzo2" runat="server" Text='<%# Eval("indirizzo2") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                          <td align="left">
                              <asp:Label ID="citta2" runat="server" Text='<%# Eval("citta2") %>' CssClass="testo_piccolo"></asp:Label>
                          </td>
                      </tr>
                  </AlternatingItemTemplate>
                  <EmptyDataTemplate>
                      <table id="Table1" runat="server" style="">
                          <tr>
                              <td>
                                  Non è stato restituito alcun dato.</td>
                          </tr>
                      </table>
                  </EmptyDataTemplate>
                  <LayoutTemplate>
                      <table id="Table2" runat="server" width="100%">
                          <tr id="Tr1" runat="server">
                              <td id="Td1" runat="server">
                                  <table ID="itemPlaceholderContainer" runat="server" border="1"  width="100%"   style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;font-size:small;">
                                      <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                          <th id="Th5" runat="server" align="left" colspan='2'>
                                              <asp:Label ID="Label6"  runat="server" Text="Cliente attuale" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th4" runat="server" align="left" >
                                              <asp:Label ID="Label20"  runat="server" Text="P.Iva" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th6" runat="server" align="left" >
                                              <asp:Label ID="Label21"  runat="server" Text="Indirizzo" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th7" runat="server" align="left" >
                                              <asp:Label ID="Label22"  runat="server" Text="Citta" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th1" runat="server" colspan='2' align="left">
                                              <asp:Label ID="Label19" runat="server" Text="Cliente precedente" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th8" runat="server" align="left" >
                                              <asp:Label ID="Label23"  runat="server" Text="P.Iva" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th9" runat="server" align="left" >
                                              <asp:Label ID="Label24"  runat="server" Text="Indirizzo" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                          <th id="Th10" runat="server" align="left" >
                                              <asp:Label ID="Label25"  runat="server" Text="Citta" CssClass="testo_titolo_piccolo"></asp:Label>
                                          </th>
                                      </tr>
                                      <tr ID="itemPlaceholder" runat="server">
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                          <tr id="Tr3" runat="server">
                              <td id="Td2" runat="server" style="" align="left">
                                  
                              </td>
                          </tr>
                      </table>
                  </LayoutTemplate>
              </asp:ListView>
           
           </td>
       </tr>
   </table>
</div>
   <asp:SqlDataSource ID="sqlFatture" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT id, num_fattura, num_contratto_rif, fattura_cliente, fattura_broker, fattura_costi_prepagati, num_prenotazione_rif, 
          CONVERT(Char(10), prenotazioni.datapren,103) As datapren, CONVERT(Char(10), fatture_nolo.data_fattura,103) As data_fattura, CAST(cod_edp As NVARCHAR(20)) + ' - ' + intestazione As ditta, fatture_nolo.saldo
          FROM fatture_nolo WITH(NOLOCK) LEFT JOIN prenotazioni WITH(NOLOCK) ON fatture_nolo.num_prenotazione_rif=prenotazioni.numpren
          WHERE fatture_nolo.attiva=1 AND da_esportare_contabilita=1 AND ISNULL(prenotazioni.attiva,1)=1  ORDER BY num_fattura ASC"></asp:SqlDataSource>
   
   <asp:SqlDataSource ID="sqlFase2" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT ditte1.rag_soc As rag_soc1, ditte2.rag_soc As rag_soc2, ditte1.piva As piva1, ditte2.piva As piva2,
                   ditte1.indirizzo As indirizzo1, ditte2.indirizzo As indirizzo2, ditte1.citta As citta1, ditte2.citta As citta2,
                   ditte2.[CODICE EDP] As codice_edp, ditte2.cap, ditte2.provincia, ditte2.nazione As id_nazione, ditte2.email, ditte2.c_fis As c_fis2,
                   fatture_nolo_clienti_appoggio.id_cliente_fattura, fatture_nolo_clienti_appoggio.id_cliente_precedente , nazioni2.nazione As nazione2
                   FROM fatture_nolo_clienti_appoggio WITH(NOLOCK) LEFT JOIN ditte As ditte1 WITH(NOLOCK) ON fatture_nolo_clienti_appoggio.id_cliente_fattura=ditte1.id_ditta 
                   LEFT JOIN ditte As ditte2 WITH(NOLOCK) ON fatture_nolo_clienti_appoggio.id_cliente_precedente=ditte2.id_ditta 
                   LEFT JOIN nazioni As nazioni2 WITH(NOLOCK) ON ditte2.nazione=nazioni2.id_nazione"
    ></asp:SqlDataSource>

    <asp:Label ID="query_cerca" runat="server" Visible="false"></asp:Label>
</asp:Content>

