<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="riepilogo_pagamenti_pos.aspx.vb" Inherits="riepilogo_pagamenti_pos" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <meta http-equiv="Expires" content="0" />
     <meta http-equiv="Cache-Control" content="no-cache" />
     <meta http-equiv="Pragma" content="no-cache" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="div_ricerca" runat="server">
<table style="border:4px solid #444; border-top:0px;" border="0" cellspacing="0" cellpadding="1" width="100%" >
  <tr>
     <td align="left" style="color: #FFFFFF;background-color:#444;" >
         <asp:Label ID="Label12" runat="server" Text="Riepilogo Pagamenti POS" CssClass="testo_titolo"></asp:Label>
     </td>
     <td align="right" style="color: #FFFFFF;background-color:#444;">
         <asp:Button ID="btnScadenziario" runat="server" Text="Scadenziario" />
         <asp:Button ID="btnRiepilogoPagPosGiornata" runat="server" Text="Pag.Pos.Giorno" />
     </td>
     <td align="right" style="color: #FFFFFF;background-color:#444;">
        <asp:Button ID="bt_cerca" runat="server" Text="Cerca" />
        <asp:Button ID="bt_stampa" runat="server" Text="Stampa" />
     </td>
  </tr>
</table>
<table style="border:4px solid #444; border-top:0px;" border="0" cellspacing="0" cellpadding="1" width="100%">
        <tr>
          <td>
              <asp:Label ID="Label4" runat="server" Text="Stazione" CssClass="testo_bold"></asp:Label>
          </td>
          <td>
              <asp:DropDownList ID="DropDownStazioni" runat="server" AutoPostBack="true" AppendDataBoundItems="True" 
                  DataSourceID="sqlStazioni" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Value="0" Selected="True">Tutte</asp:ListItem>
                </asp:DropDownList>
          </td>
          <td>
              <asp:Label ID="Label5" runat="server" Text="Contro Cassa" CssClass="testo_bold"></asp:Label>
          </td>
          <td>
              <asp:DropDownList ID="DropDownCassa" runat="server" AppendDataBoundItems="True" 
                  DataSourceID="sqlCassa" DataTextField="contro_cassa" DataValueField="cassa">
                    <asp:ListItem Value="0" Selected="True">Tutte</asp:ListItem>
                </asp:DropDownList>
          </td>
          <td>
              <asp:Label ID="Label10" runat="server" Text="Tipo Op." CssClass="testo_bold"></asp:Label>
          </td>
          <td>
              <asp:DropDownList ID="DropDownFunzioni" runat="server" AppendDataBoundItems="True" 
                  DataSourceID="SqlPOS_Funzioni" DataTextField="Funzione" DataValueField="id">
                    <asp:ListItem Value="0" Selected="True">Tutte</asp:ListItem>
                </asp:DropDownList>
          </td>
          <td>
              <asp:Label ID="Label7" runat="server" Text="Data Op." CssClass="testo_bold"></asp:Label>
          </td>
          <td>
                <asp:Label ID="Label17" runat="server" Text="Da" CssClass="testo_bold"></asp:Label>
                 <a onclick="Calendar.show(document.getElementById('<%=tx_DataDa.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="tx_DataDa" runat="server" Width="70px"></asp:TextBox>
                     </a>
               <%-- <ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                    TargetControlID="tx_DataDa" ID="CalendarExtender1">
                </ajaxtoolkit:calendarextender>--%>
                <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999"  
                    TargetControlID="tx_DataDa" MaskType="Date" CultureDatePlaceholder="" 
                    CultureTimePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureDateFormat="" 
                    CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" 
                    ID="MaskedEditExtender1">
                </ajaxtoolkit:maskededitextender>
           </td>
          <td>
                <asp:Label ID="Label19" runat="server" Text="A" CssClass="testo_bold"></asp:Label>
                 <a onclick="Calendar.show(document.getElementById('<%=tx_DataA.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="tx_DataA" runat="server" Width="70px"></asp:TextBox>
                     </a>
              <%--  <ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                    TargetControlID="tx_DataA" ID="CalendarExtender2">
                </ajaxtoolkit:calendarextender>--%>
                <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999"  
                    TargetControlID="tx_DataA" MaskType="Date" CultureDatePlaceholder="" 
                    CultureTimePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureDateFormat="" 
                    CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" 
                    ID="MaskedEditExtender2">
                </ajaxtoolkit:maskededitextender>
          </td>
        </tr>
        <tr>
          <td>
              <asp:Label ID="Label76" runat="server" Text="Nr.Preaut." CssClass="testo_bold"></asp:Label>
          </td>
          <td>
              <asp:TextBox ID="txtNrPreaut" runat="server" onKeyPress="return filterInputInt(event)" Width="100px"></asp:TextBox>
          </td>
          <td>
                <asp:Label ID="Label15" runat="server" Text="Stato Op." CssClass="testo_bold"></asp:Label>
          </td>
          <td colspan="3">
                <asp:DropDownList ID="DropDownStornata" runat="server">
                  <asp:ListItem Selected="True" Value="0">Tutti</asp:ListItem>
                  <asp:ListItem Value="1">Stornata</asp:ListItem>
                  <asp:ListItem Value="2">Non Stornata</asp:ListItem>
                  <asp:ListItem Value="3">Preaut. Aperta</asp:ListItem>
                  <asp:ListItem Value="4">Preaut. Chiusa</asp:ListItem>
                  <asp:ListItem Value="5">Preaut. Scaduta</asp:ListItem>
                  <asp:ListItem Value="6">Preaut. Scaduta/Non Incassata</asp:ListItem>
                  <asp:ListItem Value="7">Preaut. Scaduta/Aut.Negata</asp:ListItem>
                  <asp:ListItem Value="8">Preaut. Scaduta/Incass.Quando Scaduta</asp:ListItem>
              </asp:DropDownList>
          </td>
            <td>
                <asp:Label ID="Label14" runat="server" Text="Sc. Preaut." CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label20" runat="server" Text="Da" CssClass="testo_bold"></asp:Label>
                  <a onclick="Calendar.show(document.getElementById('<%=tx_ScPrDataDa.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="tx_ScPrDataDa" runat="server" Width="70px"></asp:TextBox>
                      </a>
               <%-- <ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                    TargetControlID="tx_ScPrDataDa" ID="CalendarExtender3">
                </ajaxtoolkit:calendarextender>--%>
                <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999"  
                    TargetControlID="tx_ScPrDataDa" MaskType="Date" CultureDatePlaceholder="" 
                    CultureTimePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureDateFormat="" 
                    CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" 
                    ID="MaskedEditExtender3">
                </ajaxtoolkit:maskededitextender>
           </td>
          <td>
                <asp:Label ID="Label9" runat="server" Text="A" CssClass="testo_bold"></asp:Label>
               <a onclick="Calendar.show(document.getElementById('<%=tx_ScPrDataA.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="tx_ScPrDataA" runat="server" Width="70px"></asp:TextBox>
                   </a>
              <%--  <ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                    TargetControlID="tx_ScPrDataA" ID="CalendarExtender4">
                </ajaxtoolkit:calendarextender>--%>
                <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999"  
                    TargetControlID="tx_ScPrDataA" MaskType="Date" CultureDatePlaceholder="" 
                    CultureTimePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureDateFormat="" 
                    CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" 
                    ID="MaskedEditExtender4">
                </ajaxtoolkit:maskededitextender>
          </td>
        </tr>
        <tr>
          <td>
              <asp:Label ID="Label3" runat="server" Text="Fonte" CssClass="testo_bold"></asp:Label>
            </td>
          <td>
              <asp:DropDownList ID="dropFonte" runat="server">
                  <asp:ListItem Selected="True" Value="0">Tutte</asp:ListItem>
                  <asp:ListItem Value="1">Contratto</asp:ListItem>
                  <asp:ListItem Value="2">Prenotazione</asp:ListItem>
                  <asp:ListItem Value="3">Multa</asp:ListItem>
                  <asp:ListItem Value="4">Rds</asp:ListItem>
              </asp:DropDownList>
          </td>
          <td>
              <asp:Label ID="Label75" runat="server" Text="Num." CssClass="testo_bold"></asp:Label>
            </td>
          <td>
              <asp:TextBox ID="txtNumeroFonte" runat="server" Width="78px" 
                  onKeyPress="return filterInputInt(event)"></asp:TextBox>  
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
          <td>
                &nbsp;</td>
        </tr>
      </table>
</div>

<div id="div_dettaglio" runat="server" visible="false">
<br />
<table style="border:4px solid #444; border-top:0px;" border="0" cellspacing="0" cellpadding="1" width="100%" >
  <tr>
     <td align="left" style="color: #FFFFFF;background-color:#444;" colspan="2">
         <asp:Label ID="Label13" runat="server" Text="Dettaglio Pagamento" CssClass="testo_titolo"></asp:Label>
     </td>
  </tr>
</table>
<table style="border:4px solid #444; border-top:0px;" border="0" cellspacing="1" cellpadding="1" width="100%">
<tr>
    <td colspan="2">
       <table style="border-top:0px;" border="0" cellspacing="1" cellpadding="1" width="100%">
         <tr>
           <td>
             <asp:Label ID="funzione" runat="server" Text="Funzione" CssClass="testo_bold" />
           </td>
           <td>
             <asp:Label ID="Label25" runat="server" Text="Staz." CssClass="testo_bold" />
           </td>
           <td>
             <asp:Label ID="Label8" runat="server" Text="Operatore" CssClass="testo_bold" />
           </td>
           <td>
             <asp:Label ID="Label34" runat="server" Text="Cassa" CssClass="testo_bold" />
           </td>
           <td>
             <asp:Label ID="Label35" runat="server" Text="Carta" CssClass="testo_bold" />
           </td>
           <td>
             <asp:Label ID="Label36" runat="server" Text="Intestatario" CssClass="testo_bold" />
           </td>
           <td>
             <asp:Label ID="Label37" runat="server" Text="Scadenza" CssClass="testo_bold" />
           </td>
           <td>
             <asp:Label ID="Label67" runat="server" Text="Data Operazione" CssClass="testo_bold" />
             </td>
         </tr>
         <tr>
           <td>
              <asp:TextBox ID="txtPOS_Funzione" runat="server" Width="88px" ReadOnly="true"></asp:TextBox>  
           </td>
           <td>
              <asp:TextBox ID="txtPOS_Stazione" runat="server" Width="40px" ReadOnly="true"></asp:TextBox>
           </td>
           <td>
              <asp:TextBox ID="txtPOS_Operatore" runat="server" Width="160px" ReadOnly="true"></asp:TextBox>
           </td>
           <td>
              <asp:TextBox ID="txtPOS_Cassa" runat="server" Width="40px" ReadOnly="true"></asp:TextBox>
           </td>
           <td>
             <asp:TextBox ID="txtPOS_Carta" runat="server" Width="140px" ReadOnly="true"></asp:TextBox>
           </td>
           <td>
             <asp:TextBox ID="txtPOS_Intestatario" runat="server" Width="160px" ReadOnly="true"></asp:TextBox>
           </td>
           <td>
             <asp:TextBox ID="txtPOS_Scadenza" runat="server" Width="86px" ReadOnly="true"></asp:TextBox>
           </td>
           <td>
             <asp:TextBox ID="txtPOS_DataOperazione" runat="server" Width="126px" ReadOnly="true"></asp:TextBox>
           </td>
         </tr>
         <tr>
           <td>
             <asp:Label ID="Label66" runat="server" Text="Terminal ID." CssClass="testo_bold" />
           </td>
           <td>
             <asp:Label ID="Label38" runat="server" Text="Nr. Aut." CssClass="testo_bold" />
           </td>
           <td>
             <asp:Label ID="Label69" runat="server" Text="Nr. Preaut." CssClass="testo_bold" />
           </td>
           <td>
             <asp:Label ID="Label68" runat="server" Text="Nr. Batch" CssClass="testo_bold" />
           </td>
           <td>
             <asp:Label ID="Label70" runat="server" Text="Scadenza Preaut." CssClass="testo_bold" />
           </td>
           <td>
             <asp:Label ID="Label71" runat="server" Text="Acquire Id" CssClass="testo_bold" />
           </td>
           <td>
             <asp:Label ID="Label72" runat="server" Text="Transation Type" CssClass="testo_bold" />
           </td>
           <td>
               &nbsp;</td>
         </tr>
         <tr>
           <td>
      
                 <asp:TextBox ID="txtPOS_TerminalID" runat="server" Width="84px" ReadOnly="true"></asp:TextBox>
      
           </td>
           <td>
      
                 <asp:TextBox ID="txtPOS_NrAut" runat="server" Width="65px" ReadOnly="true"></asp:TextBox>
      
           </td>
           <td>
      
                 <asp:TextBox ID="txtPOS_NrPreaut" runat="server" Width="158px" ReadOnly="true"></asp:TextBox>
      
             </td>
           <td>
      
                 <asp:TextBox ID="txtPOS_BATCH" runat="server" Width="52px" ReadOnly="true"></asp:TextBox>
      
             </td>
           <td>
      
                 <asp:TextBox ID="txtPOS_ScadenzaPreaut" runat="server" Width="140px" 
                     ReadOnly="true"></asp:TextBox>
             </td>
           <td>
      
                 <asp:TextBox ID="txtPOS_AcquireID" runat="server" Width="100px" 
                     ReadOnly="true"></asp:TextBox>
             </td>
           <td>
                 <asp:TextBox ID="txtPOS_TransationType" runat="server" Width="86px" ReadOnly="true"></asp:TextBox>
           </td>
           <td>
                 &nbsp;</td>
         </tr>
         <tr>
           <td>
      
             <asp:Label ID="Label73" runat="server" Text="Action Code" CssClass="testo_bold" />
      
           </td>
           <td>
      
             <asp:Label ID="Label74" runat="server" Text="Stato" CssClass="testo_bold" />
      
             </td>
           <td>
      
                 &nbsp;</td>
           <td>
      
                 &nbsp;</td>
           <td>
      
                 &nbsp;</td>
           <td>
      
                 &nbsp;</td>
           <td>
                 &nbsp;</td>
           <td>
                 &nbsp;</td>
         </tr>
         <tr>
           <td>
      
                 <asp:TextBox ID="txtPOS_ActionCode" runat="server" Width="65px" ReadOnly="true"></asp:TextBox>
      
             </td>
           <td>
      
                 <asp:TextBox ID="txtPOS_Stato" runat="server" Width="74px" 
                     ReadOnly="true"></asp:TextBox>
      
             </td>
           <td>
      
           </td>
           <td>
      
                 &nbsp;</td>
           <td>
      
                 &nbsp;</td>
           <td>
      
                 &nbsp;</td>
           <td>
                 &nbsp;</td>
           <td>
                 &nbsp;</td>
         </tr>
         <tr>
           <td colspan="8" align="center">
      
               <asp:Button ID="bt_chiudi_dettagio" runat="server" Text="Chiudi Dettaglio" />

           </td>
         </tr>
       </table>
    </td>
  </tr>
</table>
</div>

<div id="div_elenco" runat="server">
    <br />
    <asp:ListView ID="listPagamenti" runat="server" DataKeyNames="ID_CTR" DataSourceID="sqlDettagliPagamento">
            <ItemTemplate>
                <tr style="background-color:#DCDCDC;color: #000000;">
                    <td align="left">
                       <asp:Label ID="N_CONTRATTO_RIF" runat="server" CssClass="testo" Text='<%# Eval("N_CONTRATTO_RIF") %>' />
                       <asp:Label ID="N_RDS_RIF" runat="server" CssClass="testo" Text='<%# Eval("N_RDS_RIF") %>' />
                       <asp:Label ID="N_MULTA_RIF" runat="server" CssClass="testo" Text='<%# Eval("N_MULTA_RIF") %>' />
                       <asp:Label ID="N_PREN_RIF" runat="server" CssClass="testo" Text='<%# Eval("N_PREN_RIF") %>' />
                    </td>
                    <td align="left">
                      <asp:Label ID="lblStazione" runat="server" CssClass="testo" Text='<%# Eval("stazione") %>' />
                    </td>
                    <td align="left">
                       <asp:Label ID="lblCassa" runat="server" CssClass="testo" Text='<%# Eval("cassa") %>' Visible="false" />
                       <asp:Label ID="lb_contro_cassa" runat="server" CssClass="testo" Text='<%# Eval("contro_cassa") %>' />
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
                        <asp:Label ID="PER_IMPORTOLabel" runat="server" Text='<%# libreria.myFormatta(Eval("PER_IMPORTO"), "0.00") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                        <asp:Label ID="primo_incasso_preaut" runat="server" Text='<%# libreria.myFormatta(Eval("primo_incasso_preaut"), "dd/MM/yyyy") %>' Visible="false" />
                        <asp:Label ID="lb_scadenza_preaut" runat="server" Text='<%# libreria.myFormatta(Eval("scadenza_preaut"), "dd/MM/yyyy") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                        <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi"/>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="">
                    <td align="left">
                       <asp:Label ID="N_CONTRATTO_RIF" runat="server" CssClass="testo" Text='<%# Eval("N_CONTRATTO_RIF") %>' />
                       <asp:Label ID="N_RDS_RIF" runat="server" CssClass="testo" Text='<%# Eval("N_RDS_RIF") %>' />
                       <asp:Label ID="N_MULTA_RIF" runat="server" CssClass="testo" Text='<%# Eval("N_MULTA_RIF") %>' />
                       <asp:Label ID="N_PREN_RIF" runat="server" CssClass="testo" Text='<%# Eval("N_PREN_RIF") %>' />
                    </td>
                    <td align="left">
                      <asp:Label ID="lblStazione" runat="server" CssClass="testo" Text='<%# Eval("stazione") %>' />
                    </td>
                    <td align="left">
                       <asp:Label ID="lblCassa" runat="server" CssClass="testo" Text='<%# Eval("cassa") %>' Visible="false" />
                       <asp:Label ID="lb_contro_cassa" runat="server" CssClass="testo" Text='<%# Eval("contro_cassa") %>' />
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
                        <asp:Label ID="PER_IMPORTOLabel" runat="server" Text='<%# libreria.myFormatta(Eval("PER_IMPORTO"), "0.00") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                        <asp:Label ID="primo_incasso_preaut" runat="server" Text='<%# libreria.myFormatta(Eval("primo_incasso_preaut"), "dd/MM/yyyy") %>' Visible="false" />
                        <asp:Label ID="lb_scadenza_preaut" runat="server" Text='<%# libreria.myFormatta(Eval("scadenza_preaut"), "dd/MM/yyyy") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                         <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi"/>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <EmptyDataTemplate>
                <table id="Table1" runat="server" style="">
                    <tr>
                        <td>
                            Nessun pagamento POS con i filtri applicati.
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
                                        <%--<asp:LinkButton ID="LinkButton1" runat="server" CommandName="order_by_Fonte" CssClass="testo_titolo_piccolo">Fonte</asp:LinkButton>--%>
                                        <asp:Label ID="Label45" runat="server" Text="Fonte" CssClass="testo_titolo"></asp:Label>
                                    </th>
                                    <th id="Th7" runat="server" align="left">
                                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="order_by_stazione" CssClass="testo_titolo">Stazione</asp:LinkButton>
                                    </th>
                                    <th id="Th8" runat="server" align="left">
                                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="order_by_cassa" CssClass="testo_titolo">Cassa</asp:LinkButton>
                                    </th>
                                    <th id="Th1" runat="server" align="left">
                                        <asp:LinkButton ID="LinkButton5" runat="server" CommandName="order_by_tipo_op" CssClass="testo_titolo">Tipo Op.</asp:LinkButton>
                                    </th>
                                    <th id="Th2" runat="server" align="left">
                                        <asp:Label ID="Label18" runat="server" Text="Stato" CssClass="testo_titolo"></asp:Label>
                                    </th>
                                    <th id="Th3" runat="server" align="left">
                                        <asp:LinkButton ID="LinkButton6" runat="server" CommandName="order_by_data" CssClass="testo_titolo">Data</asp:LinkButton>
                                    </th>
                                    <th id="Th4" runat="server" align="left">
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="order_by_importo" CssClass="testo_titolo">Importo</asp:LinkButton>
                                    </th>
                                    <th align="left">
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="order_by_scadenza_pre" CssClass="testo_titolo">Sc.Preaut.</asp:LinkButton>
                                    </th>
                                    <th id="Th5" runat="server">
                                      
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
                    <tr id="Tr4" runat="server">
                              <td id="Td3" runat="server" style="" align="left">
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
</div>

<asp:Label ID="lb_sqlDettagliPagamento" runat="server" Visible="false"></asp:Label>

<asp:SqlDataSource ID="sqlDettagliPagamento" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlStazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, (codice + ' - ' + nome_stazione) descrizione FROM [stazioni] WITH(NOLOCK) WHERE [attiva] = 1 ORDER BY [codice]">
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlCassa" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT cassa, contro_cassa FROM pos WITH(NOLOCK)
            WHERE (id_stazione = @id_stazione OR 0 = @id_stazione)
            ORDER BY contro_cassa">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownStazioni" Name="id_stazione" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlPOS_Funzioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, Funzione FROM POS_Funzioni WITH(NOLOCK)
            WHERE id <> 6
            ORDER BY id" >
</asp:SqlDataSource>

<asp:Label ID="livello_accesso_dettaglio_pos" runat="server" Visible="false"></asp:Label>
<asp:Label ID="lb_Riepilogo_Pagamenti_POS" runat="server" Visible="false"></asp:Label>
<asp:Label ID="lb_Riepilogo_Pagamenti_POS_Admin" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="lblOrderBY" runat="server" Visible="False"></asp:Label>

</asp:Content>

