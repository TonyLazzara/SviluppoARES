<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="gestione_fatture_nolo.aspx.vb" Inherits="gestione_fatture_nolo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    
     <script type="text/javascript" language="javascript" src="/lytebox.js"></script>
     <link rel="stylesheet" href="/lytebox.css" type="text/css" media="screen" />
    <style type="text/css">
        .style1
        {
            width: 263px;
        }
        .style2
        {
            width: 141px;
        }
        .style3
        {
            width: 127px;
        }
        .style4
        {
            width: 84px;
        }
        .style5
        {
            width: 85px;
        }
        .style6
        {
            width: 92px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
            <tr>
            <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;" class="testo_bold_bianco">

                <asp:Label ID="Label14" runat="server" Text="Fatturazione Contratti"></asp:Label>
            </td>
            </tr>
 </table>
 <table runat="server" id="tab_ricerca1" border="0" cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444;" class="testo_bold_nero">
       <tr>
           <td class="style2">
             <asp:Label ID="Label3" runat="server" Text="Stazione Out" CssClass="testo_bold"></asp:Label>
           </td>
           <td class="style3">
             <asp:Label ID="Label38" runat="server" Text="Stato" CssClass="testo_bold"></asp:Label>
           </td>
           <td class="style4">
             <asp:Label ID="Label1" runat="server" Text="Rientro Da" CssClass="testo_bold"></asp:Label>
           </td>
           <td class="style5">
             <asp:Label ID="Label2" runat="server" Text="Rientro A" CssClass="testo_bold"></asp:Label>
           </td>
           <td class="style4">
             <asp:Label ID="Label36" runat="server" Text="Uscita Da" CssClass="testo_bold"></asp:Label>
           </td>
          <td class="style6">
             <asp:Label ID="Label37" runat="server" Text="Uscita A" CssClass="testo_bold"></asp:Label>
           </td>
          <td>
             <asp:Label ID="Label39" runat="server" Text="Da Controllare" CssClass="testo_bold"></asp:Label>
           </td>
       </tr>
       <tr>
           <td class="style2">
              <asp:DropDownList ID="cercaStazionePickUp" runat="server" 
                AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                DataValueField="id">
               <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
             </asp:DropDownList>
           </td>
           <td class="style3">
              <asp:DropDownList ID="cercaStato" runat="server" 
                AppendDataBoundItems="True">
               <asp:ListItem Value="0">Tutti</asp:ListItem>
               <asp:ListItem Value="3">Da Incassare</asp:ListItem>
               <asp:ListItem Value="4">Da Incassare Broker</asp:ListItem>
               <asp:ListItem Selected="True" Value="1">Da Fatturare</asp:ListItem>
               <asp:ListItem Value="2">Non Fatturare</asp:ListItem>
             </asp:DropDownList>
           </td>
           <td class="style4">
               <a onclick="Calendar.show(document.getElementById('<%=txtRientroDa.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox runat="server" Width="70px" ID="txtRientroDa" ></asp:TextBox></a>
                    <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                              TargetControlID="txtRientroDa" 
                              ID="CalendarExtender5">
                    </ajaxtoolkit:CalendarExtender>--%>
                    <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                              CultureDatePlaceholder="" CultureTimePlaceholder="" 
                              CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                              CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                              CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtRientroDa" 
                              ID="MaskedEditExtender6">
                    </ajaxtoolkit:MaskedEditExtender>
           </td>
           <td class="style5">              
               <a onclick="Calendar.show(document.getElementById('<%=txtRientroA.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox runat="server" Width="70px" ID="txtRientroA" ></asp:TextBox></a>
                    <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                              TargetControlID="txtRientroA" 
                              ID="CalendarExtender3">
                    </ajaxtoolkit:CalendarExtender>--%>
                    <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                              CultureDatePlaceholder="" CultureTimePlaceholder="" 
                              CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                              CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                              CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtRientroA" 
                              ID="MaskedEditExtender2">
                    </ajaxtoolkit:MaskedEditExtender>
           </td>
           <td class="style4">
                <a onclick="Calendar.show(document.getElementById('<%=txtUscitaDa.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox runat="server" Width="70px" ID="txtUscitaDa"></asp:TextBox></a>
                   <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                              TargetControlID="txtUscitaDa" 
                              ID="CalendarExtender1">
                    </ajaxtoolkit:CalendarExtender>--%>
                    <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                              CultureDatePlaceholder="" CultureTimePlaceholder="" 
                              CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                              CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                              CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtUscitaDa" 
                              ID="MaskedEditExtender1">
                    </ajaxtoolkit:MaskedEditExtender>
           </td>
          <td class="style6">
               <a onclick="Calendar.show(document.getElementById('<%=txtUscitaA.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox runat="server" Width="70px" ID="txtUscitaA" ></asp:TextBox></a>
                   <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                              TargetControlID="txtUscitaA" 
                              ID="CalendarExtender2">
                    </ajaxtoolkit:CalendarExtender>--%>
                    <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                              CultureDatePlaceholder="" CultureTimePlaceholder="" 
                              CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                              CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                              CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtUscitaA" 
                              ID="MaskedEditExtender3">
                    </ajaxtoolkit:MaskedEditExtender>
          </td>
          <td>
              <asp:DropDownList ID="dropDaControllare" runat="server" 
                AppendDataBoundItems="True">
               <asp:ListItem Selected="True" Value="0">Tutti</asp:ListItem>
               <asp:ListItem Value="1">Probabile Mensile</asp:ListItem>
               <asp:ListItem Value="2">Broker: GG minori di GG voucher</asp:ListItem>
               <asp:ListItem Value="3">Prepagati: GG minori di GG voucher</asp:ListItem>
               <asp:ListItem Value="4">Non Incassati</asp:ListItem>
               <asp:ListItem Value="5">Saldo maggiore di 0</asp:ListItem>
               <asp:ListItem Value="6">Saldo minore di 0</asp:ListItem>
               <asp:ListItem Value="7">Con CRV</asp:ListItem>
               <asp:ListItem Value="8">Gruppo cons. diverso da gruppo pren.</asp:ListItem>
               <asp:ListItem Value="9">Gruppo cons. diverso da gruppo fatt.</asp:ListItem>
               <asp:ListItem Value="10">Fattura x mail: mail errata/assente</asp:ListItem>
               <asp:ListItem Value="11">Abbuono da controllare</asp:ListItem>
               <asp:ListItem Value="12">Stazione: richiesta controllo</asp:ListItem>
               <asp:ListItem Value="13">Chiusura 0,05 cent.</asp:ListItem>
               <asp:ListItem Value="14">Anagrafica T.O. assente</asp:ListItem>
             </asp:DropDownList>
           </td>
       </tr>
       <tr>
           <td colspan="7" align="center">
             <asp:Button ID="btnCerca" runat="server" Text="Cerca" UseSubmitBehavior="False" />
             &nbsp;
             <asp:Button ID="btnStampaFatturazione" runat="server" Text="Stampa Fatturazione" UseSubmitBehavior="False" />
           &nbsp;<asp:Button ID="btnFattDaControllare" runat="server" Text="Genera Fatture" OnClientClick="javascript: return(window.confirm ('Attenzione: verranno generate le fatture per i contratti da fatturare consierando unicamente l\'intervallo di date di uscita specificato. Sei sicuro di voler continuare?'));" />
                 <asp:Label ID="lblDataFattura" runat="server" Text="Data Fattura" CssClass="testo_bold" Visible="false"></asp:Label>&nbsp;
                <a onclick="Calendar.show(document.getElementById('<%=txtDataFattura.ClientID%>'), '%d/%m/%Y', false)">
                 <asp:TextBox runat="server" Width="70px" ID="txtDataFattura" Visible="true"></asp:TextBox>
               </a>
               <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                              TargetControlID="txtDataFattura" 
                              ID="CalendarExtender4">
                    </ajaxtoolkit:CalendarExtender>--%>
                    <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                              CultureDatePlaceholder="" CultureTimePlaceholder="" 
                              CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                              CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                              CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataFattura" 
                              ID="MaskedEditExtender4">
                    </ajaxtoolkit:MaskedEditExtender>
           </td>
       </tr>
 </table>
 <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444; border-top:0px;" border="0">
       <tr>
         <td>
             <asp:ListView ID="listDaFatturare" runat="server" DataKeyNames="id" DataSourceID="sqlDaFatturare" >
                  <ItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000;">
                          <td align="left">
                              <asp:Label ID="id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <a runat="server" id="vediCalcolo" href='<%# "contratto_vedi_calcolo.aspx?idCnt=" & Eval("id") & "&versione=" & Eval("num_calcolo") & "&test=" & Eval("milli") %>' rel="lyteframe" title="-" rev="width: 1000px; height: 720px; scrolling: yes;" target="_blank"><asp:Image ID="img_cnt" runat="server" ImageUrl="images/lente.png" /></a>
                              <asp:LinkButton ID="num_contratto" runat="server" Text='<%# Eval("num_contratto") %>' CssClass="testo_piccolo" CommandName="vedi_ra" CommandArgument='<%# Eval("num_contratto") %>'></asp:LinkButton> 
                              <asp:Label ID="Label5" runat="server" Text='-' CssClass="testo" />
                              <asp:Label ID="status" runat="server" Text='<%# Eval("status") %>' CssClass="testo_bold_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="gruppo_fatturare" runat="server" Text='<%# Eval("gruppo_fatturare") %>' CssClass="testo_piccolo" />
                              <asp:Label ID="Label4" runat="server" Text='/' CssClass="testo" />
                              <asp:Label ID="gruppo_consegnare" runat="server" Text='<%# Eval("gruppo_consegnare") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="cod_tar" runat="server" Text='<%# Eval("codtar") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="num_crv" runat="server" Text='<%# Eval("num_crv") %>' Visible="false" />
                              <asp:CheckBox ID="chk_crv" runat="server" Enabled="false" />
                          </td>
                          <td align="left">
                              <asp:Label ID="rds" runat="server" Text='<%# Eval("rds") %>' Visible="false" />
                              <asp:CheckBox ID="chk_rds" runat="server" Enabled="false" />
                          </td>
                          <td align="left">
                              <asp:CheckBox ID="chkNonFatturare" runat="server" Enabled="false" Checked='<%# Eval("non_fatturare") %>' />
                          </td>
                          <td align="left">
                              <asp:Label ID="giorni" runat="server" Text='<%# Eval("giorni") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="giorni_cliente" runat="server" Text='<%# Eval("giorni") - Eval("giorni_to")  %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="giorni_prepagati" runat="server" Text='<%#  Eval("giorni_prepagati") %>' CssClass="testo_piccolo" Visible="false" />
                              <asp:Label ID="giorni_to" runat="server" Text='<%#  Eval("giorni_to") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <a runat="server" id="vediFattturareA" href='<%# "contratto_vedi_ditta.aspx?idDitta=" & Eval("id_cliente") & "&num_contratto=" & Eval("num_contratto") %>' rel="lyteframe" title="" rev="width: 900px; height: 760px; scrolling: yes;">
                                 <asp:Image ID="image_fatturare_a" runat="server" ImageUrl="images/lente.png" />
                              </a>
                              <asp:Label ID="codice_edp" runat="server" Text='<%#  Eval("codice_edp") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="costo_totale" runat="server" Text='<%# Eval("costo_totale") %>' Visible="false"  />
                             <asp:Label ID="costo_tempo_km" runat="server" Text='<%# Eval("costo_tempo_km") %>' Visible="false"  />
                             <asp:Label ID="totale_incassato" runat="server" Text='<%# Eval("totale_incassato") %>' Visible="false"  />
                             <asp:Label ID="totale_incassato_no_broker" runat="server" Text='<%# Eval("totale_incassato_no_broker") %>' Visible="false"  />
                             <asp:Label ID="importo_cliente" runat="server" Text='' CssClass="testo_piccolo" />
                             <asp:Label ID="totale_abbuoni" runat="server" Text='<%# Eval("totale_abbuoni") %>' Visible="false"  />
                          </td>
                          <td>
                             <asp:Label ID="importo_a_carico_del_broker" runat="server" Text='<%# Eval("importo_a_carico_del_broker") %>'  CssClass="testo_piccolo" />
                          </td>
                          <td>
                             <asp:Label ID="saldo" runat="server" Text=''  CssClass="testo_piccolo" />
                          </td>
                          <%--<td align="left">
                              <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi"/>
                          </td>--%>
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr style="">
                          <td align="left">
                              <asp:Label ID="id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <a runat="server" id="vediCalcolo" href='<%# "contratto_vedi_calcolo.aspx?idCnt=" & Eval("id") & "&versione=" & Eval("num_calcolo") & "&test=" & Eval("milli") %>' rel="lyteframe" title="-" rev="width: 1000px; height: 720px; scrolling: yes;" target="_blank"><asp:Image ID="img_cnt" runat="server" ImageUrl="images/lente.png" /></a>
                              <asp:LinkButton ID="num_contratto" runat="server" Text='<%# Eval("num_contratto") %>' CssClass="testo_piccolo" CommandName="vedi_ra"></asp:LinkButton> 
                              <asp:Label ID="Label5" runat="server" Text='-' CssClass="testo" />
                              <asp:Label ID="status" runat="server" Text='<%# Eval("status") %>' CssClass="testo_bold_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="gruppo_fatturare" runat="server" Text='<%# Eval("gruppo_fatturare") %>' CssClass="testo_piccolo" />
                              <asp:Label ID="Label4" runat="server" Text='/' CssClass="testo" />
                              <asp:Label ID="gruppo_consegnare" runat="server" Text='<%# Eval("gruppo_consegnare") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="cod_tar" runat="server" Text='<%# Eval("codtar") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="num_crv" runat="server" Text='<%# Eval("num_crv") %>' Visible="false" />
                              <asp:CheckBox ID="chk_crv" runat="server" Enabled="false" />
                          </td>
                          <td align="left">
                              <asp:Label ID="rds" runat="server" Text='<%# Eval("rds") %>' Visible="false"  />
                              <asp:CheckBox ID="chk_rds" runat="server" Enabled="false" />
                          </td>
                          <td align="left">
                              <asp:CheckBox ID="chkNonFatturare" runat="server" Enabled="false" Checked='<%# Eval("non_fatturare") %>' />
                          </td>
                          <td align="left">
                              <asp:Label ID="giorni" runat="server" Text='<%# Eval("giorni") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="giorni_cliente" runat="server" Text='<%# Eval("giorni") - Eval("giorni_to")  %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="giorni_prepagati" runat="server" Text='<%#  Eval("giorni_prepagati") %>' CssClass="testo_piccolo" Visible="false" />
                              <asp:Label ID="giorni_to" runat="server" Text='<%#  Eval("giorni_to") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <a runat="server" id="vediFattturareA" href='<%# "contratto_vedi_ditta.aspx?idDitta=" & Eval("id_cliente") & "&num_contratto=" & Eval("num_contratto") %>' rel="lyteframe" title="" rev="width: 900px; height: 760px; scrolling: yes;">
                                 <asp:Image ID="image_fatturare_a" runat="server" ImageUrl="images/lente.png" />
                              </a>
                              <asp:Label ID="codice_edp" runat="server" Text='<%#  Eval("codice_edp") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="costo_totale" runat="server" Text='<%# Eval("costo_totale") %>' Visible="false"  />
                             <asp:Label ID="costo_tempo_km" runat="server" Text='<%# Eval("costo_tempo_km") %>' Visible="false"  />
                             <asp:Label ID="totale_incassato" runat="server" Text='<%# Eval("totale_incassato") %>' Visible="false"  />
                             <asp:Label ID="totale_incassato_no_broker" runat="server" Text='<%# Eval("totale_incassato_no_broker") %>' Visible="false"  />
                             <asp:Label ID="totale_abbuoni" runat="server" Text='<%# Eval("totale_abbuoni") %>' Visible="false"  />
                              
                             <asp:Label ID="importo_cliente" runat="server" Text='' CssClass="testo_piccolo" />
                          </td>
                          <td>
                             <asp:Label ID="importo_a_carico_del_broker" runat="server" Text='<%# Eval("importo_a_carico_del_broker") %>'  CssClass="testo_piccolo" />
                          </td>
                          <td>
                             <asp:Label ID="saldo" runat="server" Text=''  CssClass="testo_piccolo" />
                          </td>
                          <%--<td align="left">
                              <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi"/>
                          </td>--%>
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
                      <table id="Table2" runat="server" width="100%" class="testo_bold_nero">
                          <tr id="Tr1" runat="server">
                              <td id="Td1" runat="server">
                                  <table ID="itemPlaceholderContainer" runat="server" border="1"  width="100%"   style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;font-size:small;">
                                      <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                          <th id="Th5" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton6" runat="server" CommandName="order_by_num_contratto" CssClass="testo_titolo_piccolo">Num. Ra.-Stato</asp:LinkButton>
                                          </th>
                                          <th id="Th1" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton1" runat="server" CommandName="order_by_gruppo_fatturare" CssClass="testo_titolo_piccolo">GP. F/C</asp:LinkButton>
                                          </th>
                                          <th id="Th6" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton5" runat="server" CommandName="order_by_cod_tar" CssClass="testo_titolo_piccolo">Tariffa</asp:LinkButton>
                                          </th>
                                          <th id="Th8" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton8" runat="server" CommandName="order_by_crv" CssClass="testo_titolo_piccolo">CRV</asp:LinkButton>
                                          </th>
                                          <th id="Th9" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton9" runat="server" CommandName="order_by_rds" CssClass="testo_titolo_piccolo">RDS</asp:LinkButton>
                                          </th>
                                          <th id="Th13" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton13" runat="server" CommandName="order_by_non_fatturare" CssClass="testo_titolo_piccolo">N.FATT.</asp:LinkButton>
                                          </th>
                                          <th id="Th2" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton2" runat="server" CommandName="order_by_gg" CssClass="testo_titolo_piccolo">GG</asp:LinkButton>
                                          </th>
                                          <th id="Th3" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton3" runat="server" CommandName="order_by_gg_cl" CssClass="testo_titolo_piccolo">GG CL</asp:LinkButton>
                                          </th>
                                          <th id="Th4" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton4" runat="server" CommandName="order_by_gg_to" CssClass="testo_titolo_piccolo">GG TO</asp:LinkButton>
                                          </th>
                                          <th id="Th7" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton7" runat="server" CommandName="order_by_ditta" CssClass="testo_titolo_piccolo">Ditta</asp:LinkButton>
                                          </th>
                                          <th id="Th10" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton10" runat="server" CommandName="order_by_importo_cliente" CssClass="testo_titolo_piccolo">Imp.Cli.</asp:LinkButton>
                                          </th>
                                          <th id="Th11" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton11" runat="server" CommandName="order_by_importo_to" CssClass="testo_titolo_piccolo">Imp.TO</asp:LinkButton>
                                          </th>
                                          <th id="Th12" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton12" runat="server" CommandName="order_by_saldo" CssClass="testo_titolo_piccolo">Saldo Cli.</asp:LinkButton>
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
 
 
 <asp:SqlDataSource ID="sqlStazioni" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT stazioni.id, (codice + ' ' + nome_stazione) As stazione FROM stazioni WITH(NOLOCK) WHERE attiva='1' ORDER BY codice"></asp:SqlDataSource>
 
 <asp:SqlDataSource ID="sqlDaFatturare" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT contratti.id,  contratti.num_contratto, contratti.num_calcolo, prenotazione_prepagata As prepagata, contratti.giorni, DATEPART(Ms, contratti.data_creazione) As milli,
                   ISNULL(giorni_to,0) As giorni_to,gruppi1.cod_gruppo As gruppo_fatturare, contratti.codice_edp, contratti.id_cliente,
                   ISNULL(gruppi2.cod_gruppo, gruppi1.cod_gruppo) As gruppo_consegnare, contratti.codtar, contratti.num_crv,
                  (SELECT ISNULL(SUM(per_importo),0) FROM pagamenti_extra WITH(NOLOCK) WHERE 
                     (N_CONTRATTO_RIF=contratti.num_contratto OR N_PREN_RIF=contratti.num_prenotazione) AND 
                     (id_pos_funzioni_ares='3' OR id_pos_funzioni_ares='4' OR id_pos_funzioni_ares='5' OR 
                      id_pos_funzioni_ares='7' OR id_pos_funzioni_ares='11' OR id_pos_funzioni_ares='12' OR
                      id_pos_funzioni_ares='15' OR id_pos_funzioni_ares='16' OR id_pos_funzioni_ares='8' OR 
                      id_pos_funzioni_ares='9') AND operazione_stornata='0') As totale_incassato,
                   (SELECT ISNULL(SUM(per_importo),0) FROM pagamenti_extra WITH(NOLOCK) WHERE 
                   (N_CONTRATTO_RIF=contratti.num_contratto OR N_PREN_RIF=contratti.num_prenotazione) AND
                   (id_pos_funzioni_ares='3' OR id_pos_funzioni_ares='4' OR id_pos_funzioni_ares='5' OR id_pos_funzioni_ares='7' OR id_pos_funzioni_ares='15' 
                   OR id_pos_funzioni_ares='16' OR id_pos_funzioni_ares='8'
                   OR id_pos_funzioni_ares='9') AND operazione_stornata='0' AND pagamento_broker='0') As totale_incassato_no_broker,
                   (contratti_costi1.imponibile_scontato+contratti_costi1.iva_imponibile_scontato+ISNULL
                     (contratti_costi1.imponibile_onere,0)+ISNULL(contratti_costi1.iva_onere,0)) As costo_totale,
                   (contratti_costi2.imponibile_scontato+contratti_costi2.iva_imponibile_scontato+ISNULL
                     (contratti_costi2.imponibile_onere,0)+ISNULL(contratti_costi2.iva_onere,0)) As costo_tempo_km,
                   ISNULL(contratti.importo_a_carico_del_broker,0) As importo_a_carico_del_broker, 
                   ISNULL(giorni_prepagati,0) As giorni_prepagati,
                   ISNULL((SELECT TOP 1 id FROM veicoli_evento_apertura_danno WHERE id_tipo_documento_apertura='1' AND id_documento_apertura=contratti.num_contratto 
                    AND (stato_rds<>'2' AND stato_rds<>'6' AND stato_rds<>'7')),0) As rds
                   FROM contratti WITH(NOLOCK) 
                   LEFT JOIN contratti_status WITH(NOLOCK) ON contratti.status=contratti_status.id 
                   LEFT JOIN clienti_tipologia WITH(NOLOCK) ON contratti.id_fonte=clienti_tipologia.id 
                   LEFT JOIN gruppi As gruppi1 WITH(NOLOCK) ON contratti.id_gruppo_auto=gruppi1.id_gruppo 
                   LEFT JOIN gruppi As gruppi2 WITH(NOLOCK) ON contratti.id_gruppo_app=gruppi2.id_gruppo 
                   LEFT JOIN contratti_costi As contratti_costi1 WITH(NOLOCK) ON contratti_costi1.id_documento=contratti.id AND contratti_costi1.num_calcolo=contratti.num_calcolo AND contratti_costi1.nome_costo='totale'
                   LEFT JOIN contratti_costi As contratti_costi2 WITH(NOLOCK) ON contratti_costi2.id_documento=contratti.id AND contratti_costi2.num_calcolo=contratti.num_calcolo AND contratti_costi2.id_elemento='98' 
                   WHERE contratti.attivo='1' AND contratti.status='8' AND contratti.id='0'"></asp:SqlDataSource>
 
 <asp:Label ID="query_cerca" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="lblOrderBY" runat="server" Visible="False"></asp:Label>
 
</asp:Content>

