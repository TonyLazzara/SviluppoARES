<%@ Page Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="preventivi_old.aspx.vb" Inherits="preventivi" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<%@ Register Src="/anagrafica/anagrafica_conducenti.ascx" TagName="anagrafica_conducenti" TagPrefix="uc1" %>
<%@ Register Src="/anagrafica/anagrafica_ditte.ascx" TagName="anagrafica_ditte" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_pos/scambio_importo.ascx" TagName="scambio_importo" TagPrefix="uc1" %>
<%@ Register Assembly="BoxOver" Namespace="BoxOver" TagPrefix="boxover" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
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
    </style> 
    <style type="text/css">
        .style14
        {
        }
        .style15
        {
            width: 136px;
        }
        .style21
        {
        }
        .style22
        {
            height: 16px;
        }
        .style23
        {
            width: 250px;
            height: 16px;
        }
        .style24
        {
            width: 250px;
        }
        .style25
        {
            width: 241px;
            height: 16px;
        }
        .style26
        {
            width: 241px;
        }
        .style27
        {
        }
        .style28
        {
            width: 110px;
        }
        .style30
        {
            height: 16px;
            width: 223px;
        }
        .style31
        {
            width: 223px;
        }
        .style34
        {
        }
        .style35
        {
            width: 63px;
        }
        .style37
        {
            width: 85px;
        }
        .style41
        {
            width: 161px;
        }
        .style44
        {
            width: 94px;
        }
        .style45
        {
            width: 103px;
        }
        .style46
        {
            width: 264px;
        }
        .style47
        {
        }
        .style48
        {
            width: 98px;
        }
        .style49
        {
            width: 105px;
        }
        .style51
        {
        }
        .style52
        {
            width: 56px;
        }
        .style53
        {
            width: 111px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;" class="style1">
               <asp:Label ID="Label14" runat="server" Text="Preventivi - Prenotazioni - Contratti" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
</table>
<div runat="server" id="tab_ricerca">
    
     <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444" border="0">
       <tr>
          <td class="style46">
             <asp:Label ID="Label41" runat="server" Text="Documento" CssClass="testo_bold"></asp:Label>
             <asp:Label ID="Label55" runat="server" Text="\Stato prenotazione" CssClass="testo_bold"></asp:Label>
          </td>
          <td class="style41">
             <asp:Label ID="Label15" runat="server" Text="Num. Pren. Interno" CssClass="testo_bold"></asp:Label>
          </td>
          <td class="style49">
             <asp:Label ID="Label35" runat="server" Text="Riferimento T.O." 
                  CssClass="testo_bold"></asp:Label>
          </td>
          <td class="style48">
             <asp:Label ID="Label33" runat="server" Text="Cognome" 
                  CssClass="testo_bold"></asp:Label>
           </td>
          <td class="style35">
             <asp:Label ID="Label34" runat="server" Text="Nome" 
                  CssClass="testo_bold"></asp:Label>
           </td>
          <td class="style37">
             <asp:Label ID="Label36" runat="server" Text="Pick Up Da" 
                  CssClass="testo_bold"></asp:Label>
           </td>
          <td class="style47">
             <asp:Label ID="Label37" runat="server" Text="Pick Up A" 
                  CssClass="testo_bold"></asp:Label>
           </td>
       </tr>
       <tr>
          <td class="style46">
              <asp:DropDownList ID="dropTipoDocumento" runat="server" AppendDataBoundItems="True">
                  <asp:ListItem Selected="True" Value="1">Prenotazione</asp:ListItem>
                  <asp:ListItem Value="2">Preventivo</asp:ListItem>
              </asp:DropDownList>
              <asp:DropDownList ID="dropStatoDocumento" runat="server" AppendDataBoundItems="True">
                  <asp:ListItem Selected="False" Value="Tutti">Tutti</asp:ListItem>
                  <asp:ListItem Selected="True" Value="0">Aperto</asp:ListItem>
                  <asp:ListItem Value="1">A. da rimborsare</asp:ListItem>
                  <asp:ListItem Value="2">Annullato</asp:ListItem>
                  <asp:ListItem Value="X">Rich.Annullamento</asp:ListItem>
              </asp:DropDownList>
           </td>
          <td class="style41">
              <asp:TextBox ID="txtNumPreventivo" runat="server" Width="80px"></asp:TextBox>
          </td>
          <td class="style49">
              <asp:TextBox ID="txtCercaRiferimento" runat="server" Width="80px"></asp:TextBox>
           </td>
          <td class="style48">
              <asp:TextBox ID="txtCercaCognome" runat="server" Width="120px"></asp:TextBox>
           </td>
          <td class="style35">
              <asp:TextBox ID="txtCercaNome" runat="server" Width="120px"></asp:TextBox>
           </td>
          <td class="style37">
                <asp:TextBox runat="server" Width="70px" ID="txtCercaPickUpDa"></asp:TextBox>
                <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                          TargetControlID="txtCercaPickUpDa" ID="txtDaData0_CalendarExtender">
                </ajaxtoolkit:CalendarExtender>
                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaPickUpDa" 
                          ID="txtDaData0_MaskedEditExtender">
                </ajaxtoolkit:MaskedEditExtender>
           </td>
          <td class="style47">
      <asp:TextBox runat="server" Width="70px" ID="txtCercaPickUpA"></asp:TextBox>
        <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                  TargetControlID="txtCercaPickUpA" 
                  ID="txtCercaPickUpDa0_CalendarExtender">
        </ajaxtoolkit:CalendarExtender>
        <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                  CultureDatePlaceholder="" CultureTimePlaceholder="" 
                  CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                  CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                  CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaPickUpA" 
                  ID="txtCercaPickUpDa0_MaskedEditExtender">
        </ajaxtoolkit:MaskedEditExtender>
           </td>
       </tr>
       <tr>
          <td  colspan="1">
             <asp:Label ID="Label66" runat="server" Text="Prenotazioni da ribaltamento" 
                  CssClass="testo_bold"></asp:Label>
           </td>
          <td  colspan="2">
             <asp:Label ID="Label62" runat="server" Text="Stazione di uscita" CssClass="testo_bold"></asp:Label>
           </td>
          <td class="style34" colspan="2">
             <asp:Label ID="Label63" runat="server" Text="Stazione di rientro" CssClass="testo_bold"></asp:Label>
           </td>
          <td>
             <asp:Label ID="Label38" runat="server" Text="Drop Off Da" 
                  CssClass="testo_bold"></asp:Label>
          </td>
          <td class="style47">
             <asp:Label ID="Label39" runat="server" Text="Drop Off A" 
                  CssClass="testo_bold"></asp:Label>
           </td>
       </tr>
       <tr>
          <td class="style2" colspan="1">
        <asp:DropDownList ID="cercaPrenotazioniRibaltamento" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlStazioniRibaltamento" DataTextField="stazione" 
            DataValueField="id">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
           </td>
          <td class="style2" colspan="2">
        <asp:DropDownList ID="cercaStazionePickUp" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
            DataValueField="id">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
           </td>
          <td class="style34" colspan="2">
        <asp:DropDownList ID="cercaStazioneDropOff" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
            DataValueField="id">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
           </td>
          <td>
        <asp:TextBox runat="server" Width="70px" ID="txtCercaDropOffDa"></asp:TextBox>
        <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                  TargetControlID="txtCercaDropOffDa" 
                  ID="txtCercaPickUpDa0_CalendarExtender0">
        </ajaxtoolkit:CalendarExtender>
        <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                  CultureDatePlaceholder="" CultureTimePlaceholder="" 
                  CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                  CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                  CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaDropOffDa" 
                  ID="txtCercaPickUpDa0_MaskedEditExtender0">
        </ajaxtoolkit:MaskedEditExtender>
          </td>
          <td class="style47">
        <asp:TextBox runat="server" Width="70px" ID="txtCercaDropOffA"></asp:TextBox>
        <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                  TargetControlID="txtCercaDropOffA" 
                  ID="txtCercaPickUpDa1_CalendarExtender">
        </ajaxtoolkit:CalendarExtender>
        <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                  CultureDatePlaceholder="" CultureTimePlaceholder="" 
                  CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                  CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                  CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaDropOffA" 
                  ID="txtCercaPickUpDa1_MaskedEditExtender">
        </ajaxtoolkit:MaskedEditExtender>
           </td>
       </tr>
       <tr>
          <td  colspan="1">
             <asp:Label ID="Label67" runat="server" Text="Cliente" CssClass="testo_bold"></asp:Label>
           </td>
          <td>
             <asp:Label ID="Label76" runat="server" Text="Prenot.Prepagate" CssClass="testo_bold"></asp:Label>
           </td>
          <td>
              &nbsp;</td>
          <td class="style34" colspan="2">
              &nbsp;</td>
          <td>
              &nbsp;</td>
          <td class="style47">
              &nbsp;</td>
       </tr>
       <tr>
          <td colspan="1">
              <asp:DropDownList ID="dropCercaTipoCliente" runat="server" 
                  AppendDataBoundItems="True" DataSourceID="sqlTipoClienti" 
                  DataTextField="descrizione" DataValueField="id">
                  <asp:ListItem Selected="False" Value="-1">Seleziona...</asp:ListItem>
                  <asp:ListItem Selected="False" Value="0">Nessuno</asp:ListItem>
              </asp:DropDownList>
           </td>
          <td>
              <asp:DropDownList ID="dropPrenotazioniPrepagate" runat="server" AppendDataBoundItems="True" 
                  style="margin-left: 0px">
                  <asp:ListItem Selected="True" Value="-1">Tutte</asp:ListItem>
                  <asp:ListItem Selected="False" Value="1">Si</asp:ListItem>
                  <asp:ListItem Selected="False" Value="0">No</asp:ListItem>
              </asp:DropDownList>
           </td>
          <td>
              &nbsp;</td>
          <td class="style34" colspan="2">
              &nbsp;</td>
          <td>
              &nbsp;</td>
          <td class="style47">
              &nbsp;</td>
       </tr>
       <tr>
          <td align="center" colspan="7">
              <asp:Button ID="btnCercaIniziale" runat="server" Text="Cerca" Visible="true"  
                  Width="77px" />
          &nbsp;&nbsp;<asp:Button ID="btnNuovoPreventivo" runat="server" Text="Nuovo Calcolo" />
          &nbsp; <asp:Button ID="btnRichiamaPreventivo" runat="server" 
                  Text="Richiama Documento" Visible="True" style="height: 26px" />
          </td>
       </tr>
     </table>

     <table cellpadding="0" cellspacing="0" width="100%" border="0">
       <tr>
          <td colspan="7">
            <div style="width:1024px;height: 400px; overflow:scroll;" runat="server" Visible="false" id="divPreventivi">
              <asp:ListView ID="listPreventivi" runat="server" DataKeyNames="id" DataSourceID="sqlPreventivi" >
                  <ItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000;">
                          <td align="left">
                              <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi"/>
                          </td>
                          <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="num_preventivoLabel" runat="server" Text='<%# Eval("num_preventivo") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="staz_uscitaLabel" runat="server" Text='<%# Eval("staz_uscita") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="left">
                              <asp:Label ID="data_uscita" runat="server" Text='<%# Eval("data_uscita") %>' CssClass="testo" />&nbsp;
                              <asp:Label ID="Label79" runat="server" Text='<%# Eval("ore_uscita") %>'  CssClass="testo" />
                              <asp:Label ID="Label80" runat="server" Text=':' CssClass="testo" />
                              <asp:Label ID="Label81" runat="server" Text='<%# Eval("minuti_uscita") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="staz_rientroLabel" runat="server" Text='<%# Eval("staz_rientro") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="left">                    
                              <asp:Label ID="Label82" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo" />&nbsp;
                              <asp:Label ID="Label83" runat="server" Text='<%# Eval("ore_rientro") %>'  CssClass="testo" />
                              <asp:Label ID="Label84" runat="server" Text=':' CssClass="testo" />
                              <asp:Label ID="Label85" runat="server" Text='<%# Eval("minuti_rientro") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="cod_gruppoLabel" runat="server"  Text='<%# Eval("cod_gruppo") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="Label72" runat="server"  Text='<%# Eval("tariffa") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="tipo_cliente" runat="server"  Text='<%# Eval("tipo_cliente") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="cognome_conducenteLabel" runat="server" Text='<%# Eval("cognome_conducente") %>' CssClass="testo" />&nbsp;
                              <asp:Label ID="nome_conducenteLabel" runat="server" Text='<%# Eval("nome_conducente") %>' CssClass="testo" />
                          </td>
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr style="">
                          <td align="left">
                              <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi"/>
                          </td>
                          <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="num_preventivoLabel" runat="server" Text='<%# Eval("num_preventivo") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="staz_uscitaLabel" runat="server" Text='<%# Eval("staz_uscita") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="left">
                              <asp:Label ID="data_uscita" runat="server" Text='<%# Eval("data_uscita") %>' CssClass="testo" />&nbsp;
                              <asp:Label ID="Label79" runat="server" Text='<%# Eval("ore_uscita") %>'  CssClass="testo" />
                              <asp:Label ID="Label80" runat="server" Text=':' CssClass="testo" />
                              <asp:Label ID="Label81" runat="server" Text='<%# Eval("minuti_uscita") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="staz_rientroLabel" runat="server" Text='<%# Eval("staz_rientro") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="left">                    
                              <asp:Label ID="Label82" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo" />&nbsp;
                              <asp:Label ID="Label83" runat="server" Text='<%# Eval("ore_rientro") %>'  CssClass="testo" />
                              <asp:Label ID="Label84" runat="server" Text=':' CssClass="testo" />
                              <asp:Label ID="Label85" runat="server" Text='<%# Eval("minuti_rientro") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="cod_gruppoLabel" runat="server"  Text='<%# Eval("cod_gruppo") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="Label71" runat="server"  Text='<%# Eval("tariffa") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="tipo_cliente" runat="server"  Text='<%# Eval("tipo_cliente") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="cognome_conducenteLabel" runat="server" Text='<%# Eval("cognome_conducente") %>' CssClass="testo" />&nbsp;
                              <asp:Label ID="nome_conducenteLabel" runat="server" Text='<%# Eval("nome_conducente") %>' CssClass="testo" />
                          </td>
                      </tr>
                  </AlternatingItemTemplate>
                  <EmptyDataTemplate>
                      <table runat="server" style="">
                          <tr>
                              <td>
                                  Non è stato restituito alcun dato.</td>
                          </tr>
                      </table>
                  </EmptyDataTemplate>
                  <LayoutTemplate>
                      <table runat="server" width="1660px" >
                          <tr runat="server">
                              <td runat="server">
                                  <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                      style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;font-size:small;">
                                      <tr runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                          <th></th>
                                          <th id="Th5" runat="server" align="left" width="20px">
                                             <asp:Label ID="Label57" runat="server" Text="Num." CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th id="Th7" runat="server" align="left" width="240px">
                                              <asp:Label ID="Label58" runat="server" Text="Uscita" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th id="Th1" runat="server" align="left" width="150px">
                                              <asp:Label ID="Label53" runat="server" Text="Data" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th id="Th9" runat="server" align="left" width="240px">
                                              <asp:Label ID="Label59" runat="server" Text="Rientro" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th id="Th2" runat="server" align="left" width="150px">
                                              <asp:Label ID="Label54" runat="server" Text="Data" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th id="Th10" runat="server" align="left" width="60px">
                                              <asp:Label ID="Label60" runat="server" Text="Gruppo" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th id="Th6" runat="server" align="left" width="460px">
                                              <asp:Label ID="Label69" runat="server" Text="Tariffa" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th id="Th4" runat="server" align="left" width="100px">
                                              <asp:Label ID="Label11" runat="server" Text="Tipo Cliente" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th id="Th11" runat="server" align="left" width="240px">
                                              <asp:Label ID="Label61" runat="server" Text="Conducente" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          
                                      </tr>
                                      <tr ID="itemPlaceholder" runat="server">
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                          <tr runat="server">
                              <td runat="server" style="" align="left">
                                  <asp:DataPager ID="DataPager1" runat="server" PageSize="20">
                                      <Fields>
                                          <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                                      </Fields>
                                  </asp:DataPager>
                              </td>
                          </tr>
                      </table>
                  </LayoutTemplate>
              </asp:ListView>
             </div>
           </td>
       </tr>
       <tr>
          <td colspan="7">
             <div style="width:1024px;height: 400px; overflow:scroll;" runat="server" Visible="false" id="divPrenotazioni">
              <asp:ListView ID="listPrenotazioni" runat="server" DataKeyNames="Nr_Pren" DataSourceID="sqlPrenotazioni" >
                  <ItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000;">
                          <td align="left">
                              <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi"/>
                          </td>
                          <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("Nr_pren") %>' Visible="false" />
                              <asp:Label ID="NUMPREN" runat="server" Text='<%# Eval("NUMPREN") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="Label65" runat="server" Text='<%# Eval("status") %>' CssClass="testo" />
                          </td>
                          <td align="left" >
                              <asp:Label ID="staz_uscitaLabel" runat="server"  
                                  Text='<%# Eval("staz_uscita") %>' CssClass="testo" />
                          </td>
                          <td>
                              <asp:Label ID="data_uscitaLabel" runat="server" 
                                  Text='<%# Eval("data_uscita") %>' CssClass="testo" />&nbsp;
                              <asp:Label ID="ore_uscitaLabel" runat="server" 
                                  Text='<%# Eval("ore_uscita") %>'  CssClass="testo" />
                              <asp:Label ID="Label49" runat="server" Text=':' CssClass="testo" />
                              <asp:Label ID="minuti_uscitaLabel" runat="server" 
                                  Text='<%# Eval("minuti_uscita") %>' CssClass="testo" />
                              <%--<asp:Label ID="Label50" runat="server" Text=':00' CssClass="testo" />--%>
                          </td>
                          <td align="left" >
                              <asp:Label ID="staz_rientroLabel" runat="server" 
                                  Text='<%# Eval("staz_rientro") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td>
                              <asp:Label ID="data_rientroLabel" runat="server" 
                                  Text='<%# Eval("data_rientro") %>' CssClass="testo" />&nbsp;
                              <asp:Label ID="ore_rientroLabel" runat="server" 
                                  Text='<%# Eval("ore_rientro") %>'  CssClass="testo" />
                              <asp:Label ID="Label51" runat="server" Text=':' CssClass="testo" />
                              <asp:Label ID="minuti_rientroLabel" runat="server" 
                                  Text='<%# Eval("minuti_rientro") %>' CssClass="testo" />
                              <%--<asp:Label ID="Label52" runat="server" Text=':00' CssClass="testo" />--%>
                          </td>
                          <td align="left">
                              <asp:Label ID="cod_gruppoLabel" runat="server"  Text='<%# Eval("cod_gruppo") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="tariffa" runat="server"  Text='<%# Eval("tariffa") %>' CssClass="testo" />
                          </td>
                          <td>
                              <asp:Label ID="prepagata" runat="server" Text='<%# Eval("prepagata") %>'  Visible="false" />
                              <asp:Label ID="lblPrepagato" runat="server" CssClass="testo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="tipo_cliente" runat="server"  Text='<%# Eval("tipo_cliente") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="cognome_conducenteLabel" runat="server" 
                                  Text='<%# Eval("cognome_conducente") %>' CssClass="testo" />&nbsp;
                              <asp:Label ID="nome_conducenteLabel" runat="server" 
                                  Text='<%# Eval("nome_conducente") %>' CssClass="testo" />
                          </td>
                          
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr style="">
                          <td align="left">
                              <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi"/>
                          </td>
                          <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("Nr_pren") %>' Visible="false" />
                              <asp:Label ID="NUMPREN" runat="server" 
                                  Text='<%# Eval("NUMPREN") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="Label65" runat="server" Text='<%# Eval("status") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="staz_uscitaLabel" runat="server" 
                                  Text='<%# Eval("staz_uscita") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td>
                              <asp:Label ID="data_uscitaLabel" runat="server" 
                                  Text='<%# Eval("data_uscita") %>' CssClass="testo" />&nbsp;
                              <asp:Label ID="ore_uscitaLabel" runat="server" 
                                  Text='<%# Eval("ore_uscita") %>'  CssClass="testo" />
                              <asp:Label ID="Label49" runat="server" Text=':' CssClass="testo" />
                              <asp:Label ID="minuti_uscitaLabel" runat="server" 
                                  Text='<%# Eval("minuti_uscita") %>' CssClass="testo" />
                             <%-- <asp:Label ID="Label50" runat="server" Text=':00' CssClass="testo" />--%>
                          </td>
                          <td align="left">
                              <asp:Label ID="staz_rientroLabel" runat="server" 
                                  Text='<%# Eval("staz_rientro") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                          </td>
                          <td>
                              <asp:Label ID="data_rientroLabel" runat="server" 
                                  Text='<%# Eval("data_rientro") %>' CssClass="testo" />&nbsp;
                              <asp:Label ID="ore_rientroLabel" runat="server" 
                                  Text='<%# Eval("ore_rientro") %>'  CssClass="testo" />
                              <asp:Label ID="Label51" runat="server" Text=':' CssClass="testo" />
                              <asp:Label ID="minuti_rientroLabel" runat="server" 
                                  Text='<%# Eval("minuti_rientro") %>' CssClass="testo" />
                              <%--<asp:Label ID="Label52" runat="server" Text=':00' CssClass="testo" />--%>
                          </td>
                          <td align="left">
                              <asp:Label ID="cod_gruppoLabel" runat="server"  Text='<%# Eval("cod_gruppo") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="tariffa" runat="server"  Text='<%# Eval("tariffa") %>' CssClass="testo" />
                          </td>
                          <td>
                              <asp:Label ID="prepagata" runat="server" Text='<%# Eval("prepagata") %>'  Visible="false" />
                              <asp:Label ID="lblPrepagato" runat="server" CssClass="testo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="tipo_cliente" runat="server"  Text='<%# Eval("tipo_cliente") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="cognome_conducenteLabel" runat="server" 
                                  Text='<%# Eval("cognome_conducente") %>' CssClass="testo" />&nbsp;
                              <asp:Label ID="nome_conducenteLabel" runat="server" 
                                  Text='<%# Eval("nome_conducente") %>' CssClass="testo" />
                          </td>
                          
                      </tr>
                  </AlternatingItemTemplate>
                  <EmptyDataTemplate>
                      <table runat="server" style="">
                          <tr>
                              <td>
                                  Non è stato restituito alcun dato.</td>
                          </tr>
                      </table>
                  </EmptyDataTemplate>
                  <LayoutTemplate>
                      <table runat="server" width="1780px" >
                          <tr runat="server">
                              <td runat="server">
                                  <table ID="itemPlaceholderContainer" runat="server"  border="1" 
                                      style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;font-size:small;">
                                      <tr runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                          <th></th>
                                          <th runat="server" align="left" width="20px">
                                             <asp:Label ID="Label57" runat="server" Text="Num." CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th id="Th3" runat="server" align="left" width="60px">
                                             <asp:Label ID="Label56" runat="server" Text="Stato" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th runat="server" align="left" width="240px">
                                              <asp:Label ID="Label58" runat="server" Text="Uscita" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th id="Th1" runat="server" align="left" width="150px">
                                              <asp:Label ID="Label53" runat="server" Text="Data" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th runat="server" align="left" width="240px">
                                              <asp:Label ID="Label59" runat="server" Text="Rientro" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th id="Th2" runat="server" align="left" width="150px">
                                              <asp:Label ID="Label54" runat="server" Text="Data" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th runat="server" align="left" width="60px">
                                              <asp:Label ID="Label60" runat="server" Text="Gruppo" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th id="Th6" runat="server" align="left" width="460px">
                                              <asp:Label ID="Label69" runat="server" Text="Tariffa" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th id="Th8" runat="server" align="left" width="60px">
                                              <asp:Label ID="Label75" runat="server" Text="Prepag." CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th id="Th4" runat="server" align="left" width="100px">
                                              <asp:Label ID="Label11" runat="server" Text="Tipo Cliente" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          <th runat="server" align="left" width="240px">
                                              <asp:Label ID="Label61" runat="server" Text="Conducente" CssClass="testo_titolo"></asp:Label>
                                          </th>
                                          
                                      </tr>
                                      <tr ID="itemPlaceholder" runat="server">
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                          <tr runat="server">
                              <td runat="server" style="" align="left">
                                  <asp:DataPager ID="DataPager2" runat="server" PageSize="20">
                                      <Fields>
                                          <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                                      </Fields>
                                  </asp:DataPager>
                              </td>
                          </tr>
                      </table>
                  </LayoutTemplate>
              </asp:ListView>
             </div>
           </td>
       </tr>
       </table>
</div>

<div runat="server" id="tab_cerca_tariffe" visible="false" >
<div style="position:fixed;margin-top:0px;padding-top:0px;">
<table border="0" cellspacing="2" cellpadding="2" width="1024px" style="border:4px solid #444">
<tr>
           <td colspan="8" align="center" style="color: #FFFFFF;background-color:#444;" 
               class="style1">
               <asp:Label ID="lblTipoDocumento" runat="server" Text="Preventivo Num.:" CssClass="testo_titolo"></asp:Label>&nbsp;
               <asp:Label ID="lblNumPreventivo" runat="server"  CssClass="testo_titolo"></asp:Label>
               &nbsp;&nbsp;&nbsp;
               <asp:Label ID="lblData" runat="server" Text="Data Preventivo:" CssClass="testo_titolo"></asp:Label>&nbsp;
               <asp:Label ID="lblDataPreventivo" runat="server"  CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
<tr>
  <td class="style28" valign="top">
    <asp:Label ID="Label2" runat="server" Text="Pick Up" CssClass="testo_bold"></asp:Label>
  </td>
  <td class="style15" valign="top">
        <asp:DropDownList ID="dropStazionePickUp" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
            DataValueField="id">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
  </td>
  <td valign="top" class="style44">
    <asp:TextBox runat="server" Width="70px" ID="txtDaData"></asp:TextBox>
        <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDaData" ID="CalendarExtender2">
        </ajaxtoolkit:CalendarExtender>
        <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDaData" ID="MaskedEditExtender1">
        </ajaxtoolkit:MaskedEditExtender>
  </td>
  <td valign="top" class="style45">
             
        <asp:TextBox ID="txtoraPartenza" runat="server" Width="40px" ></asp:TextBox>
             <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                          TargetControlID="txtoraPartenza"
                          Mask="99:99"
                          MessageValidatorTip="true"
                          ClearMaskOnLostFocus="true"
                          OnFocusCssClass="MaskedEditFocus"
                          OnInvalidCssClass="MaskedEditError"
                          MaskType="Time"
                          CultureName="en-US">
            </ajaxToolkit:MaskedEditExtender>

       
                 <asp:TextBox ID="ore1" runat="server" MaxLength="2" Visible="false" 
                     Width="29px"></asp:TextBox>
                 <asp:TextBox ID="minuti1" runat="server" MaxLength="2" Visible="false" 
                     Width="29px"></asp:TextBox>

       
  </td>
  <td valign="top" colspan="4">
  
        <asp:DataList ID="listWarningPickPreventivi" runat="server"  DataSourceID="sqlWarningPickPreventivi" RepeatColumns="1" 
            RepeatDirection="Horizontal">
            <ItemTemplate>
                <img src="punto_elenco.jpg"  border="0" alt="" title="" width="8" height="7" />&nbsp;<asp:Label ID="warning" runat="server" Text='<%# Eval("warning") %>' CssClass="testo_bold" />&nbsp;
                <asp:Label ID="tipo" runat="server" Text='<%# Eval("tipo") %>' visible="false" />
            </ItemTemplate>
        </asp:DataList>
  
  </td>
</tr>
<tr>
  <td class="style28" valign="top">
     <asp:Label ID="Label1" runat="server" Text="Drop Off" CssClass="testo_bold"></asp:Label>
  </td>
  <td class="style15" valign="top">
    <asp:DropDownList ID="dropStazioneDropOff" runat="server" 
        AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
        DataValueField="id">
        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>    
    </asp:DropDownList>
  </td>
  <td class="style44" valign="top">
    <asp:TextBox runat="server" Width="70px" ID="txtAData" ></asp:TextBox>
        <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtAData" ID="CalendarExtender3">
        </ajaxtoolkit:CalendarExtender>
        <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtAData" ID="MaskedEditExtender2">
        </ajaxtoolkit:MaskedEditExtender>
  </td>
  <td valign="top" class="style45">    
        <asp:TextBox ID="txtOraRientro" runat="server" Width="40px"></asp:TextBox>
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" 
                          TargetControlID="txtOraRientro"
                          Mask="99:99"
                          MessageValidatorTip="true"
                          ClearMaskOnLostFocus="true"
                          OnFocusCssClass="MaskedEditFocus"
                          OnInvalidCssClass="MaskedEditError"
                          MaskType="Time"
                          CultureName="en-US">
            </ajaxToolkit:MaskedEditExtender>
            <asp:TextBox ID="ore2" runat="server" Width="29px" MaxLength="2" Visible="false"></asp:TextBox>
            <asp:TextBox ID="minuti2" runat="server" Width="29px" MaxLength="2" Visible="false"></asp:TextBox>
  </td>
  <td valign="top" colspan="4">
  
        <asp:DataList ID="listWarningDropPreventivi" runat="server"  DataSourceID="sqlWarningDropPreventivi" RepeatColumns="1" 
            RepeatDirection="Horizontal">
            <ItemTemplate>
                <img src="punto_elenco.jpg"  border="0" alt="" title="" width="8" height="7" />&nbsp;<asp:Label ID="warning" runat="server" Text='<%# Eval("warning") %>' CssClass="testo_bold" />&nbsp;
                <asp:Label ID="tipo" runat="server" Text='<%# Eval("tipo") %>' visible="false" />
            </ItemTemplate>
        </asp:DataList>
  
        <img id="punto_fonte" runat="server" alt="" border="0" height="7" 
            src="punto_elenco.jpg" title="" visible="false" width="8" />&nbsp;
        <asp:Label ID="fonte_stop_sell" runat="server" CssClass="testo_bold" 
            ForeColor="Red" Text="La fonte è in stop sell" Visible="false" />
  
  </td>
</tr>
<tr>
  <td class="style28" valign="top">
    <asp:Label ID="Label3" runat="server" Text="Cliente" CssClass="testo_bold"></asp:Label>
  </td>
  <td class="style15" valign="top">
      <asp:DropDownList ID="dropTipoCliente" runat="server" 
          AppendDataBoundItems="True" DataSourceID="sqlTipoClienti" 
          DataTextField="descrizione" DataValueField="id">
          <asp:ListItem Selected="True" Value="0">Generico...</asp:ListItem>
      </asp:DropDownList>
  &nbsp;</td>
  <td class="style44" valign="top">
  
    <asp:Label ID="Label73" runat="server" Text="Cod.Convenzione" CssClass="testo_bold"></asp:Label>
    </td>
  <td valign="top" class="style45">
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtCodiceCliente" runat="server" Width="68px"></asp:TextBox>
    </td>
  <td valign="top" class="style52">
  
      
     <asp:Label ID="Label74" runat="server" Text="Ditta" 
          CssClass="testo_bold"></asp:Label>
  
      
    </td>
  <td valign="top" class="style51" colspan="3">
  
      
      <asp:Label ID="lblNomeDitta" runat="server" Text="" CssClass="testo"></asp:Label>
    </td>
</tr>
<tr>
  <td class="style28" valign="top">
    <asp:Label ID="Label19" runat="server" Text="Età primo guid.:" CssClass="testo_bold"></asp:Label>
  </td>
  <td valign="top" class="style15">
    <asp:TextBox ID="txtEtaPrimo" runat="server" Width="40px"></asp:TextBox>
  </td>
  <td valign="top" colspan="2">
      <asp:Label ID="Label20" runat="server" Text="Età secondo guid.:" CssClass="testo_bold"></asp:Label>
      
      <asp:TextBox ID="txtEtaSecondo" runat="server" Width="40px"></asp:TextBox>
      
    </td>
  <td valign="top">
  
      
     <asp:Label ID="Label32" runat="server" Text="Gruppo" 
          CssClass="testo_bold"></asp:Label>
  
      
    </td>
  <td valign="top" class="style53">
  
      
      <asp:DropDownList ID="gruppoVeloce" runat="server" AppendDataBoundItems="True" 
          DataSourceID="sqlGruppiAuto" DataTextField="cod_gruppo" 
          DataValueField="id_gruppo">
          <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
      </asp:DropDownList>
  
      
    </td>
  <td valign="top" class="style28">    
     <asp:Label ID="Label23" runat="server" Text="Numero Giorni" CssClass="testo_bold"></asp:Label>
  </td>
  <td valign="top">   
      <asp:TextBox ID="txtNumeroGiorni" runat="server" Width="36px"></asp:TextBox>
  </td>
</tr>
<tr>
  <td class="style14" valign="top" colspan="8" align="center">
  
      <asp:Button ID="btnCerca" runat="server" Text="Cerca" ValidationGroup="cerca" />
  
  &nbsp;<asp:Button ID="btnAnnulla0" runat="server" Text="Annulla" />
  
  </td>
</tr>
</table>
</div>
<table style="border:4px solid #444; border-top:0px;" border="0" cellspacing="1" cellpadding="1" width="1024px" runat="server" id="tab_preventivo" visible="false">
  <%--<tr>
           <td colspan="3" align="center" style="color: #FFFFFF;background-color:#444;" class="style1">
               <asp:Label ID="Label21" runat="server" Text="Preventivo Num.:" CssClass="testo_titolo"></asp:Label>&nbsp;
               <asp:Label ID="lblNumPreventivo" runat="server"  CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>--%>
  <tr>
    <td class="style23">
      
    <asp:Label ID="Label17" runat="server" Text="Cognome" CssClass="testo_bold"></asp:Label>
      
    </td>
    <td class="style25">
      
    <asp:Label ID="Label16" runat="server" Text="Nome" CssClass="testo_bold"></asp:Label>
      
    </td>
    <td class="style22">
      
    <asp:Label ID="Label18" runat="server" Text="E-Mail" CssClass="testo_bold"></asp:Label>
      
    </td>
  </tr>
  <tr>
    <td class="style24">
      
        <asp:TextBox ID="txtCognome" runat="server" Width="170px"></asp:TextBox>
      
    </td>
    <td class="style26">
      
        <asp:TextBox ID="txtNome" runat="server" Width="170px"></asp:TextBox>
      
    </td>
    <td>
      
        <asp:TextBox ID="txtMail" runat="server" Width="170px"></asp:TextBox>
      
    </td>
  </tr>
  <tr>
    <td class="style21" colspan="3" align="center">
      &nbsp;<br />
      <asp:Button ID="btnSalvaPreventivo" runat="server" Text="Salva Preventivo" />
  
        &nbsp;<asp:Button ID="btnAnnulla3" runat="server" Text="Annulla" />
  
      &nbsp;<asp:Button ID="btnVediUltimoCalcolo" runat="server" Text="Vedi dett. precedente" />
  
        &nbsp;<asp:Button ID="btnAnnulla8" runat="server" Text="Chiudi" />

        &nbsp;<asp:Button ID="btnAnnulla4" runat="server" Text="Chiudi senza salvare" />

    </td>
  </tr>
</table>
<div runat="server" id="tab_prenotazioni" visible="false">
<table style="border:4px solid #444; border-top:0px;" border="0" cellspacing="1" cellpadding="1" width="1024px" >
  <tr>
     <td colspan="2">
        <uc1:anagrafica_conducenti ID="anagrafica_conducenti" runat="server" Visible="false" />
        <uc1:anagrafica_ditte ID="anagrafica_ditte" runat="server" Visible="false" />
     </td>
  </tr>
</table>
<table style="border:4px solid #444; border-top:0px;" border="0" cellspacing="0" cellpadding="1" width="1024px" >
  <tr>
    <td class="style23">
      
    <asp:Label ID="Label27" runat="server" Text="Cognome" CssClass="testo_bold"></asp:Label>
      
    </td>
    <td class="style25">
      
    <asp:Label ID="Label26" runat="server" Text="Nome" CssClass="testo_bold"></asp:Label>
      
    </td>
    <td class="style30">
      
    <asp:Label ID="Label28" runat="server" Text="E-Mail" CssClass="testo_bold"></asp:Label>
      
    </td>
    <td class="style22">
      
      <asp:Label ID="Label31" runat="server" Text="Note" 
            CssClass="testo_bold"></asp:Label>  
      
        </td>
  </tr>
  <tr>
    <td class="style24">
        <asp:Label ID="id_conducente" runat="server" visible="false"></asp:Label>
        &nbsp;
              
        <asp:TextBox ID="txtCognomeConducente" runat="server" Width="170px"></asp:TextBox>
      
    </td>
    <td class="style26">
      
        <asp:TextBox ID="txtNomeConducente" runat="server" Width="170px"></asp:TextBox>
      
    </td>
    <td class="style31">
      
        <asp:TextBox ID="txtMailConducente" runat="server" Width="170px"></asp:TextBox>
        <asp:Button ID="btnModificaConducente" runat="server" Text="..." />
      
    </td>
    <td rowspan="5" valign="top">
      
        <asp:TextBox ID="txtNote" runat="server" Width="290px" Height="132px" 
            TextMode="MultiLine"></asp:TextBox>
        </td>
  </tr>
<tr>
    <td class="style24">
      
    <asp:Label ID="Label29" runat="server" Text="Gruppo da Consegnare" CssClass="testo_bold"></asp:Label>
      
    </td>
    <td class="style26"> 
      <asp:Label ID="Label30" runat="server" Text="Numero Volo (Uscita)" CssClass="testo_bold"></asp:Label>
    </td>
    <td class="style31">
      <asp:Label ID="Label22" runat="server" Text="Numero Volo (Rientro)" CssClass="testo_bold"></asp:Label>  
    </td>
  </tr>
  <tr>
    <td class="style24">
        &nbsp;&nbsp;
        <asp:DropDownList ID="dropGruppoDaConsegnare" runat="server" 
            DataSourceID="sqlGruppiAuto" DataTextField="cod_gruppo" DataValueField="id_gruppo">
        </asp:DropDownList>
      
      </td>
    <td class="style26">
      
        <asp:TextBox ID="txtVoloOut" runat="server" Width="100px" 
            MaxLength="10"></asp:TextBox>
      
      </td>
    <td class="style31">
      
        <asp:TextBox ID="txtVoloPr" runat="server" Width="100px" 
            MaxLength="10"></asp:TextBox>
      
      </td>
  </tr>
  <tr>
    <td class="style24">
      <asp:Label ID="Label40" runat="server" Text="Numero Riferimento" CssClass="testo_bold"></asp:Label>
      
      </td>
    <td class="style26">
      
    <asp:Label ID="Label64" runat="server" Text="Data di Nascita" CssClass="testo_bold"></asp:Label>
      
      </td>
    <td class="style31">
      
        &nbsp;</td>
  </tr>
  <tr>
    <td class="style24">
      
        &nbsp;&nbsp;
      
        <asp:TextBox ID="txtRiferimentoTO" runat="server" Width="100px" MaxLength="15"></asp:TextBox>
      
      </td>
    <td class="style26">
      
    <asp:TextBox runat="server" Width="70px" ID="txtDataDiNascita" ></asp:TextBox>
        <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                  TargetControlID="txtDataDiNascita" 
            ID="txtCercaPickUpDa0_CalendarExtender1">
        </ajaxtoolkit:CalendarExtender>
        <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                  CultureDatePlaceholder="" CultureTimePlaceholder="" 
                  CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                  CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                  CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataDiNascita" 
                  ID="txtCercaPickUpDa0_MaskedEditExtender1">
        </ajaxtoolkit:MaskedEditExtender>
           </td>
    <td class="style31">
      
        &nbsp;</td>
  </tr>
  <tr>
    <td class="style24" colspan="3">
      <asp:Label ID="Label80" runat="server" Text="Fatturare a ditta" 
            CssClass="testo_bold"></asp:Label>
      
      </td>
  </tr>
  <tr>
    <td class="style24">
      
        <asp:TextBox ID="txtNomeDitta" runat="server" Width="230px"  ReadOnly="True"></asp:TextBox>
      
      </td>
    <td class="style26">
      
        <asp:Button ID="btnModificaDitta" runat="server" Text="..." />
      </td>
    <td class="style31">
      
        <asp:Label ID="id_ditta" runat="server" visible="true"></asp:Label>
        </td>
  </tr>
  <tr>
    <td runat="server" align="center" colspan="4">
       
    &nbsp;&nbsp;
        <asp:Button ID="btnPagamento" runat="server" 
            Text="Pagamento" style="height: 26px" Visible="false" />
  
    &nbsp;<asp:Button ID="btnAggiornaPrenotazione" runat="server" 
            Text="Modifica Prenotazione" style="height: 26px" Visible="false" />
  
    &nbsp;<asp:Button ID="btnSalvaPrenotazione" runat="server" 
            Text="Salva Prenotazione" style="height: 26px" />
  
    &nbsp;<asp:Button ID="btnAnnulla7" runat="server" Text="Annulla" />
  
    &nbsp;<asp:Button ID="btnAnnulla9" runat="server" Text="Chiudi" />

    &nbsp;<%--<asp:Button ID="btnAnnulla5" runat="server" Text="Annulla" />--%><asp:Button ID="btnAnnulla6" runat="server" Text="Chiudi senza salvare" />

    </td>
  </tr>
</table>
</div>
<br />

<table border="0" cellspacing="0" cellpadding="0" width="1024px" runat="server" id="table_warning" visible="false">
  <tr>
    <td>
        <asp:Label ID="Label4" runat="server" Text="Attenzione:" ForeColor="Red" CssClass="testo_bold"></asp:Label>
        <br />
        <asp:Label ID="Label5" runat="server"  Text="Modificare i parametri di ricerca oppure continuare con il preventivo. L'eventuale prenotazione sarà ON REQUEST se sono presenti uno o più avvertimenti in rosso." CssClass="testo"></asp:Label>
    </td>
  </tr>
  <tr>
    <td align="center">
       <asp:Button ID="btnContinua" runat="server" Text="Continua ugualmente." ValidationGroup="cerca" />
    &nbsp;<asp:Button ID="btnAnnulla1" runat="server" Text="Annulla" />
  
    &nbsp;</td>
  </tr>
</table>

<table border="0" cellspacing="0" cellpadding="0" width="1024px" runat="server" id="table_tariffe" visible="false">
 <tr>
    <td>
     <asp:Label ID="Label8" runat="server" Text="Tariffe Generiche:" CssClass="testo_bold"></asp:Label>
  &nbsp; <asp:DropDownList ID="dropTariffeGeneriche" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlTariffeGeneriche" 
            DataTextField="codice" DataValueField="id">
            <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
     &nbsp;&nbsp; <asp:Label ID="Label9" runat="server" Text="Tariffe Particolari:" CssClass="testo_bold"></asp:Label>
     &nbsp;&nbsp; <asp:DropDownList ID="dropTariffeParticolari" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlTariffeParticolari" 
            DataTextField="codice" DataValueField="id">
            <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
      </asp:DropDownList>
    &nbsp; <asp:Label ID="Label10" runat="server" Text="Applica Sconto:" 
            CssClass="testo_bold"></asp:Label>
    &nbsp;
        <asp:TextBox ID="txtSconto" runat="server" Width="32px"></asp:TextBox>
        <b>%
        
        <asp:Label ID="lblMxSconto" runat="server" Text="Applicato il MASSIMO SCONTO." 
            CssClass="testo_bold" ForeColor="Red" Visible="false"></asp:Label>
        
        </td>
 </tr>
 <tr>
    <td align="center">
        &nbsp; &nbsp;</td>
 </tr>
 </table>
 <table border="0" cellspacing="0" cellpadding="0" width="1024px" runat="server" id="table_gruppi" visible="false">
 <tr>
  <td align="left">
    <asp:Label ID="Label6" runat="server" Text="Gruppi" CssClass="testo_bold"></asp:Label>
  </td>
</tr>
<tr>
  <td align="left" style="border: thin solid #000000;">
              
        <asp:DataList ID="listGruppi" runat="server"  DataSourceID="sqlGruppiAuto" 
            RepeatColumns="2" Width="100%">
            <ItemTemplate>
                <td width="5%" valign="top">
                  <asp:CheckBox ID="sel_gruppo" runat="server" />&nbsp;<asp:Label ID="gruppo" runat="server" Text='<%# Eval("cod_gruppo") %>' CssClass="testo_bold" /><asp:Label ID="id_gruppo" runat="server" Text='<%# Eval("id_gruppo") %>' visible="false" />&nbsp;
                </td>
                <td width="45%" valign="top">
                   <asp:Image ID="punto1" ImageUrl="punto_elenco.jpg" runat="server" width="8" height="7" Visible="false" />
                   <asp:Label ID="pick" runat="server" Text='Non vendibile (pick up)' ForeColor="red" CssClass="testo_bold" Visible="false" />
                   <asp:Image ID="punto2" ImageUrl="punto_elenco.jpg" runat="server" width="8" height="7" Visible="false" />
                   <asp:Label ID="drop" runat="server" Text='Non vendibile (drop off)' ForeColor="red" CssClass="testo_bold" Visible="false" />
                   <asp:Image ID="punto3" ImageUrl="punto_elenco.jpg" runat="server" width="8" height="7" Visible="false" />
                   <asp:Label ID="val" runat="server" Text='VAL non permesso (drop off)' ForeColor="red" CssClass="testo_bold" Visible="false" />
                   <asp:Image ID="punto4" ImageUrl="punto_elenco.jpg" runat="server" width="8" height="7" Visible="false" />
                   <asp:Label ID="stop_sale" runat="server" Text='Stop Sale' ForeColor="red" CssClass="testo_bold" Visible="false" />
                   <asp:Image ID="punto5" ImageUrl="punto_elenco.jpg" runat="server" width="8" height="7" Visible="false" />
                   <asp:Label ID="stop_sale_fonte" runat="server" Text='Stop sale (Fonte)' ForeColor="red" CssClass="testo_bold" Visible="false" />
                    <asp:Image ID="punto6" ImageUrl="punto_elenco.jpg" runat="server" width="8" height="7" Visible="false" />
                   <asp:Label ID="eta_guidatore" runat="server" Text='Gruppo non vendibile (Età guidatore/i)' ForeColor="red" CssClass="testo_bold" Visible="false" />
                </td>
            </ItemTemplate>
        </asp:DataList>
  
  </td>
</tr>
<tr>
  <td align="center">
    <asp:Button ID="btnProsegui" runat="server" Text="Vedi tariffe" />
  &nbsp;<asp:Button ID="btnCambiaTariffa" runat="server" Text="Cambia tariffa/sconto" Visible="false" />
  &nbsp;<asp:Button ID="btnAnnulla2" runat="server" Text="Annulla" />
  
  </td>
</tr>
</table>

<table border="0" cellspacing="0" cellpadding="0" width="1024px">
<tr runat="server" id="table_accessori_extra" visible="false">
  <td class="style27" colspan="2">
     
     <asp:Label ID="Label24" runat="server" Text="Accessori Extra:" CssClass="testo_bold"></asp:Label>
  
     &nbsp; 
      <asp:DropDownList ID="dropElementiExtra" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlElementiExtra" 
            DataTextField="descrizione" DataValueField="id">
            <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
     &nbsp;<asp:Button ID="btnAggiungiExtra" runat="server" Text="Aggiungi" />
     <br />
     &nbsp; 
  </td>
</tr>
<tr>
  <td class="style27" colspan="2">
     
        <asp:DataList ID="listWarningGruppi" runat="server"  DataSourceID="sqlWarningGruppi" RepeatColumns="3" RepeatDirection="Horizontal">
            <ItemTemplate>
                <img src="punto_elenco.jpg"  border="0" alt="" title="" width="8" height="7" />&nbsp;<asp:Label ID="warning" runat="server" Text='<%# Eval("warning") %>' ForeColor="Red" CssClass="testo_bold" />&nbsp;
                <asp:Label ID="tipo" runat="server"  visible="false" />
            </ItemTemplate>
        </asp:DataList>
  
  </td>
</tr>
<tr>
  <td align="left" width="50%" valign="top" >
      <asp:DataList ID="listPreventiviCosti" runat="server" DataSourceID="sqlPreventiviCosti" Width="100%" >
          <ItemTemplate>
              <tr runat="server" id="riga_gruppo" >
                 <td bgcolor="#19191b" style="width:100%;" colspan="6">
                      <asp:Label ID="Label7" runat="server" Text="Gruppo:" CssClass="testo"  />
                      <asp:Label ID="id_gruppoLabel" runat="server" Text='<%# Eval("id_gruppo") %>' Visible="false" />
                      <asp:Label ID="gruppo" runat="server" Text='<%# Eval("gruppo") %>' CssClass="testo_bold" />
                      &nbsp;&nbsp;&nbsp;
                      <asp:Button ID="preventivo" runat="server" Text="Preventivo" CommandName="preventivo" /> 
                      <asp:Button ID="prenotazione" runat="server" Text="Prenotazione" CommandName="prenotazione"  /> 
                      <asp:Button ID="contratto" runat="server" Text="Contratto" CommandName="contratto"  /> 
                 </td>
              </tr>
              <tr runat="server" id="riga_intestazione" >
                 <td width="6%">
                 </td>
                 <td width="50%">
                 </td>
                 <td width="18%">
                      <asp:Label ID="Label13" runat="server" Text='' CssClass="testo_bold" />&nbsp;         
                 </td>
                 <%--<td width="18%">
                     <asp:Label ID="Label11" runat="server" Text='Sconto' CssClass="testo_bold" />&nbsp;
                 </td>--%>
                 <td width="18%">
                    <asp:Label ID="Label12" runat="server" Text='Costo' CssClass="testo_bold" />&nbsp;
                 </td>
                 <td>
                   <asp:Label ID="Label21" runat="server" Text='Omaggio' CssClass="testo_bold" />
                 </td>
              </tr>
              <tr runat="server" id="riga_elementi">
                 <td width="6%" align="left">
                     <asp:CheckBox ID="chkOldScegli" runat="server" Visible="false" Checked='<%# Eval("selezionato") %>' />
                     <asp:CheckBox ID="chkScegli" runat="server" Visible="false" Checked='<%# Eval("selezionato") %>' />
                     <asp:Label ID="lblIncluso" runat="server" Text="I " ToolTip="Incluso" CssClass="testo_bold" Visible="false"  />
                     <asp:Label ID="lblObbligatorio" runat="server" Text="O " ToolTip="Obbligatorio" CssClass="testo_bold" Visible="false"  />
                     <asp:Label ID="lblInformativa" runat="server" Text="IN " ToolTip="Informativa" CssClass="testo_bold" Visible="false"  />
                 </td>
                 <td width="50%">
                      <asp:Label ID="id_preventivi_costi" runat="server" Text='<%# Eval("id") %>' visible="false" />
                      <asp:Label ID="id_elemento" runat="server" Text='<%# Eval("id_elemento") %>' visible="false" />
                      <boxover:BoxOver ID="BoxOver1" runat="server" body='<%# Eval("nome_costo") & " - " & Eval("descrizione_lunga") %>' controltovalidate="nome_costo" header="Descrizione" CssHeader="toolheader"  CssBody="toolbody"   />
                      <asp:Label ID="nome_costo" runat="server" Text='<%# Eval("nome_costo") %>' CssClass="testo" />:&nbsp;&nbsp;
                 </td>
                 <td width="18%">
                      <asp:Label ID="valore_costoLabel" runat="server" Text='<%# FormatNumber(Eval("valore_costo"),2) %>' CssClass="testo" />&nbsp;

                      <asp:Label ID="id_a_carico_di" runat="server" Text='<%# Eval("id_a_carico_di") %>' Visible="false" />
                      <asp:Label ID="obbligatorio" runat="server" Text='<%# Eval("obbligatorio") %>' Visible="false" /> 
                      <asp:Label ID="id_metodo_stampa" runat="server" Text='<%# Eval("id_metodo_stampa") %>' Visible="false" />                    
                 </td>
                 <%--<td width="18%">
                     <asp:Label ID="lblSconto" runat="server" Text='<%# FormatNumber(Eval("sconto"),2) %>' CssClass="testo" />&nbsp;
                 </td>--%>
                 <td width="18%">
                    <asp:Label ID="lblSconto" runat="server" Text='<%# FormatNumber(Eval("sconto"),2) %>' CssClass="testo" />&nbsp;
                    <asp:Label ID="costo_scontato" runat="server" Text='<%# FormatNumber(FormatNumber(Eval("valore_costo"),2) - FormatNumber(Eval("sconto"),2),2) %>' CssClass="testo" />&nbsp;
                 </td>
                 <td align="center">
                     <asp:Label ID="omaggiabile" runat="server" Text='<%# Eval("omaggiabile") %>' Visible="false" />
                     <asp:CheckBox ID="chkOldOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />
                     <asp:CheckBox ID="chkOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />   
                     <asp:Button ID="aggiorna" runat="server" Text="Aggiorna" CommandName="Aggiorna" Visible="false" /> 
                 </td>
              </tr>
          </ItemTemplate>
      </asp:DataList>
      <asp:ImageButton ID="lblFocus" runat="server" Width="0px" Height="0px" />
  
  
  </td>
  <td width="50%" valign="top" align="center" >
      <asp:DataList ID="listVecchioCalcolo" runat="server" DataSourceID="sqlUltimoPreventiviCosti" Width="95%" Visible="false">
          <ItemTemplate>
              <tr runat="server" id="riga_gruppo" >
                 <td bgcolor="#19191b" style="width:100%;" colspan="6" height="23px" align="left">
                      <asp:Label ID="Label7" runat="server" Text="Gruppo:" CssClass="testo"  />
                      <asp:Label ID="id_gruppoLabel" runat="server" Text='<%# Eval("id_gruppo") %>' Visible="false" />
                      <asp:Label ID="gruppo" runat="server" Text='<%# Eval("gruppo") %>' CssClass="testo_bold" />
                      &nbsp;&nbsp;&nbsp;
                 </td>
              </tr>
              <tr runat="server" id="riga_intestazione" >
                 <td width="6%">
                 </td>
                 <td width="50%">
                 </td>
                 <td width="18%" align="left">
                      <asp:Label ID="Label13" runat="server" Text='' CssClass="testo_bold" />&nbsp;         
                 </td>
                 <%--<td width="18%">
                     <asp:Label ID="Label11" runat="server" Text='Sconto' CssClass="testo_bold" />&nbsp;
                 </td>--%>
                 <td width="18%" align="left">
                    <asp:Label ID="Label12" runat="server" Text='Costo' CssClass="testo_bold" />&nbsp;
                 </td>
                 <td align="left">
                   <asp:Label ID="Label21" runat="server" Text='Omaggio' CssClass="testo_bold" />
                 </td>
              </tr>
              <tr runat="server" id="riga_elementi">
                 <td width="6%" align="left">
                     <asp:CheckBox ID="chkOldScegli" runat="server" Visible="false" Checked='<%# Eval("selezionato") %>' />
                     <asp:CheckBox ID="chkScegli" runat="server" Visible="false" Checked='<%# Eval("selezionato") %>' Enabled="false" />
                     <asp:Label ID="lblIncluso" runat="server" Text="I " ToolTip="Incluso" CssClass="testo_bold" Visible="false"  />
                     <asp:Label ID="lblObbligatorio" runat="server" Text="O " ToolTip="Obbligatorio" CssClass="testo_bold" Visible="false"  />
                     <asp:Label ID="lblInformativa" runat="server" Text="IN " ToolTip="Informativa" CssClass="testo_bold" Visible="false"  />
                 </td>
                 <td width="50%" align="left">
                      <asp:Label ID="id_elemento" runat="server" Text='<%# Eval("id_elemento") %>' visible="false" />
                      <asp:Label ID="nome_costo" runat="server" Text='<%# Eval("nome_costo") %>' CssClass="testo" />:&nbsp;&nbsp;
                 </td>
                 <td width="18%" align="left">
                      <asp:Label ID="valore_costoLabel" runat="server" Text='<%# FormatNumber(Eval("valore_costo"),2) %>' CssClass="testo" />&nbsp;

                      <asp:Label ID="id_a_carico_di" runat="server" Text='<%# Eval("id_a_carico_di") %>' Visible="false" />
                      <asp:Label ID="obbligatorio" runat="server" Text='<%# Eval("obbligatorio") %>' Visible="false" /> 
                      <asp:Label ID="id_metodo_stampa" runat="server" Text='<%# Eval("id_metodo_stampa") %>' Visible="false" />                    
                 </td>
                 <%--<td width="18%">
                     <asp:Label ID="lblSconto" runat="server" Text='<%# FormatNumber(Eval("sconto"),2) %>' CssClass="testo" />&nbsp;
                 </td>--%>
                 <td width="18%" align="left">
                    <asp:Label ID="lblSconto" runat="server" Text='<%# FormatNumber(Eval("sconto"),2) %>' CssClass="testo" />&nbsp;
                    <asp:Label ID="costo_scontato" runat="server" Text='<%# FormatNumber(FormatNumber(Eval("valore_costo"),2) - FormatNumber(Eval("sconto"),2),2) %>' CssClass="testo" />&nbsp;
                 </td>
                 <td align="center">
                     <asp:Label ID="omaggiabile" runat="server" Text='<%# Eval("omaggiabile") %>' Visible="false" />
                     <asp:CheckBox ID="chkOldOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />
                     <asp:CheckBox ID="chkOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />   
                 </td>
              </tr>
          </ItemTemplate>
      </asp:DataList>
  
    </td>
</tr>
<tr>
  <td align="center" colspan="2">
    <br />&nbsp;
    
  </td>
</tr>
</table>
</div>
<div runat="server" id="tab_pagamento" visible="false" >
     <uc1:scambio_importo ID="Scambio_Importo1" runat="server"  />
</div>

&nbsp;
      <asp:Label ID="gruppi_ultima_ricerca" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="tariffa_ultima_ricerca" runat="server" Visible="false"></asp:Label>
   
      <asp:Label ID="numero_prenotazione" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="idPreventivo" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="idPrenotazione" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="idContratto" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="numCalcolo" runat="server" Visible="true"></asp:Label>
      <asp:Label ID="tipo_preventivo" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="old_ultimo_gruppo" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="ultimo_gruppo" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="id_gruppo_auto_scelto" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="tariffa_broker" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="livello_accesso_sconto" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="livello_accesso_omaggi" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="livello_accesso_broker" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="query_cerca_prev" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="query_cerca_pren" runat="server" Visible="false"></asp:Label>
      
&nbsp;<asp:SqlDataSource ID="sqlStazioni" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT stazioni.id, (codice + ' ' + nome_stazione) As stazione FROM stazioni WHERE attiva='1' ORDER BY codice"></asp:SqlDataSource>
  
  
    <asp:SqlDataSource ID="sqlStazioniRibaltamento" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT stazioni.id, (codice + ' ' + nome_stazione) As stazione FROM stazioni WHERE stazione_ribaltamento='1' ORDER BY codice"></asp:SqlDataSource>
    

    <asp:SqlDataSource ID="sqlTipoClienti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT [descrizione], [id] FROM [clienti_tipologia] ORDER BY [descrizione]">
    </asp:SqlDataSource>

    

    <asp:SqlDataSource ID="sqlWarningPickPreventivi" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT warning, tipo FROM [preventivi_warning] WHERE (([id_documento] = @id_preventivo) AND ([num_calcolo] = @num_calcolo) And ((tipo='PICK') OR (tipo='PICK INFO'))) ORDER BY [warning]">
        <SelectParameters>
            <asp:ControlParameter ControlID="idPreventivo" Name="id_preventivo" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlWarningDropPreventivi" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT warning, tipo FROM [preventivi_warning] WHERE (([id_documento] = @id_preventivo) AND ([num_calcolo] = @num_calcolo) And ((tipo='DROP') OR (tipo='DROP INFO'))) ORDER BY [warning]">
        <SelectParameters>
            <asp:ControlParameter ControlID="idPreventivo" Name="id_preventivo" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlWarningGruppi" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT warning, tipo FROM [preventivi_warning] WHERE (([id_documento] = @id_preventivo) AND ([num_calcolo] = @num_calcolo) And ((tipo='GRUPPO'))) ORDER BY [warning]">
        <SelectParameters>
            <asp:ControlParameter ControlID="idPreventivo" Name="id_preventivo" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlGruppiAuto" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT gruppi.id_gruppo, gruppi.cod_gruppo FROM gruppi WHERE attivo='1' ORDER BY cod_gruppo"></asp:SqlDataSource>
        
    <asp:SqlDataSource ID="sqlElementiExtra" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT condizioni_elementi.id, condizioni_elementi.descrizione FROM condizioni_elementi WHERE id='0'"></asp:SqlDataSource>
        
    <asp:SqlDataSource ID="sqlTariffeGeneriche" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, codice FROM tariffe WHERE id='0'"></asp:SqlDataSource>
        
    <asp:SqlDataSource ID="sqlTariffeParticolari" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, codice FROM tariffe WHERE id='0'"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlPreventiviCosti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT preventivi_costi.id, gruppi.id_gruppo, gruppi.cod_gruppo As gruppo,preventivi_costi.id_elemento, preventivi_costi.nome_costo, condizioni_elementi.descrizione_lunga, ISNULL((imponibile+iva_imponibile),0) As valore_costo, ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, id_a_carico_di,preventivi_costi.obbligatorio, id_metodo_stampa, ISNULL(selezionato,'False') As selezionato, ISNULL(preventivi_costi.omaggiato,'False') As omaggiato, ISNULL(preventivi_costi.omaggiabile,'False') As omaggiabile  FROM preventivi_costi INNER JOIN gruppi ON preventivi_costi.id_gruppo=gruppi.id_gruppo LEFT JOIN condizioni_elementi ON preventivi_costi.id_elemento=condizioni_elementi.id WHERE ((id_documento = @id_preventivo) AND (num_calcolo = @num_calcolo_preventivo)) AND ordine_stampa<>'5' AND ISNULL(franchigia_attiva,'1')='1' ORDER BY id_gruppo,ordine_stampa,ISNULL(condizioni_elementi.ordinamento_vis,0) DESC, nome_costo  ">
        <SelectParameters>
            <asp:ControlParameter ControlID="idPreventivo" Name="id_preventivo" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo_preventivo" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlUltimoPreventiviCosti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT gruppi.id_gruppo, gruppi.cod_gruppo As gruppo, preventivi_costi.id_elemento, preventivi_costi.nome_costo, ISNULL((imponibile+iva_imponibile),0) As valore_costo, ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, id_a_carico_di,preventivi_costi.obbligatorio, id_metodo_stampa, ISNULL(selezionato,'False') As selezionato, ISNULL(preventivi_costi.omaggiato,'False') As omaggiato, ISNULL(preventivi_costi.omaggiabile,'False') As omaggiabile FROM preventivi_costi INNER JOIN gruppi ON preventivi_costi.id_gruppo=gruppi.id_gruppo LEFT JOIN condizioni_elementi ON preventivi_costi.id_elemento=condizioni_elementi.id WHERE ((id_documento = @id_preventivo) AND (num_calcolo = @num_calcolo_preventivo-1)) AND ordine_stampa<>'5' AND ISNULL(franchigia_attiva,'1')='1' ORDER BY id_gruppo,ordine_stampa, ISNULL(condizioni_elementi.ordinamento_vis,0) DESC, nome_costo ">
                      
        <SelectParameters>
            <asp:ControlParameter ControlID="idPreventivo" Name="id_preventivo" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo_preventivo" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlPreventivi" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT preventivi.id, preventivi.num_preventivo, (stazioni1.codice + ' ' + stazioni1.nome_stazione) As staz_uscita, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As staz_rientro, CONVERT(char(10),preventivi.data_uscita,103) As data_uscita, preventivi.ore_uscita, preventivi.minuti_uscita, CONVERT(Char(10),preventivi.data_rientro,103) As data_rientro, preventivi.ore_rientro, preventivi.minuti_rientro, gruppi.cod_gruppo, preventivi.cognome_conducente, preventivi.nome_conducente FROM preventivi INNER JOIN stazioni AS stazioni1 ON preventivi.id_stazione_uscita=stazioni1.id INNER JOIN stazioni As stazioni2 ON preventivi.id_stazione_rientro=stazioni2.id INNER JOIN gruppi ON preventivi.id_gruppo_auto=gruppi.id_gruppo WHERE NOT num_preventivo IS NULL ORDER BY preventivi.id"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlPrenotazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        
        SelectCommand="SELECT prenotazioni.Nr_pren, prenotazioni.NUMPREN, (stazioni1.codice + ' ' + stazioni1.nome_stazione) As staz_uscita, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As staz_rientro, CONVERT(char(10),prenotazioni.PRDATA_OUT,103) As data_uscita, prenotazioni.ore_uscita, prenotazioni.minuti_uscita, CONVERT(Char(10),prenotazioni.PRDATA_PR,103) As data_rientro, prenotazioni.ore_rientro, prenotazioni.minuti_rientro, gruppi.cod_gruppo, prenotazioni.cognome_conducente, prenotazioni.nome_conducente FROM prenotazioni LEFT JOIN stazioni AS stazioni1 ON prenotazioni.PRID_stazione_out=stazioni1.id LEFT JOIN stazioni As stazioni2 ON prenotazioni.PRID_stazione_pr=stazioni2.id LEFT JOIN gruppi ON prenotazioni.id_gruppo=gruppi.id_gruppo WHERE attiva='1' ORDER BY prenotazioni.Nr_Pren"></asp:SqlDataSource>
    
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                               ControlToValidate="txtDaData" ErrorMessage="Specificare la data iniziale di pick up." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="cerca"> </asp:RequiredFieldValidator>
                            
                           <%--<asp:CompareValidator ID="CompareValidator2" runat="server" 
                                ControlToValidate="txtDaData" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per la data iniziale di pick up'." Type="Date" ValidationGroup="cerca"> </asp:CompareValidator>--%>
                            
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  
                               ControlToValidate="txtAData" ErrorMessage="Specificare la data finale di pick up." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="cerca"> </asp:RequiredFieldValidator>
                            
                            <%--<asp:CompareValidator ID="CompareValidator3" runat="server" 
                                ControlToValidate="txtAData" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per la data finale di pick up'." Type="Date" ValidationGroup="cerca"> </asp:CompareValidator>--%>
                            
                           <%-- <asp:CompareValidator id="CompareValidator4" runat="server"
                                 ControlToValidate="txtDaData"
                                 ControlToCompare="txtAData"
                                 Type= "Date"
                                 Operator = "GreaterThanEqual"
                                 ErrorMessage = "Attenzione : la data finale di pick up è precedente alla data iniziale."
                                 ValidationGroup="cerca"
                                 Font-Size="0pt"> </asp:CompareValidator>--%>
                                 
    <asp:CompareValidator ID="CompareValidator7" runat="server" 
                    ControlToValidate="dropStazionePickUp" 
                    Operator="GreaterThan" Type="Integer" 
                    ValidationGroup="cerca" ValueToCompare="0" Font-Size="0pt" ErrorMessage="Specificare la stazione di pick up."> </asp:CompareValidator>
               
    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToValidate="dropStazioneDropOff" 
                    Operator="GreaterThan" Type="Integer" 
                    ValidationGroup="cerca" ValueToCompare="0" Font-Size="0pt" ErrorMessage="Specificare la stazione di drop off."> </asp:CompareValidator> 
    
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                               ControlToValidate="ore1" ErrorMessage="Specificare l'orario di inizio noleggio." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="cerca"> </asp:RequiredFieldValidator>
   
    <asp:RangeValidator ID="RangeValidator1" runat="server" 
        ControlToValidate="ore1" 
        ErrorMessage="Specificare un orario di inizio noleggio corretto." Font-Size="0pt" 
        Type="Integer" ValidationGroup="cerca" 
        MinimumValue="0" MaximumValue="23" > </asp:RangeValidator>
    
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                               ControlToValidate="ore2" ErrorMessage="Specificare l'orario di fine noleggio." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="cerca"> </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator2" runat="server" 
        ControlToValidate="ore2" 
        ErrorMessage="Specificare un orario di fine noleggio corretto." Font-Size="0pt" 
        Type="Integer" ValidationGroup="cerca" 
        MinimumValue="0" MaximumValue="23" > </asp:RangeValidator>
    
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                               ControlToValidate="minuti1" ErrorMessage="Specificare i minuti di inizio noleggio." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="cerca"> </asp:RequiredFieldValidator>
    <asp:RangeValidator ID="RangeValidator3" runat="server" 
        ControlToValidate="minuti1" 
        ErrorMessage="Specificare un valore corretto per i minuti di inizio noleggio." Font-Size="0pt" 
        Type="Integer" ValidationGroup="cerca" 
        MinimumValue="0" MaximumValue="59" > </asp:RangeValidator>     
    
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                               ControlToValidate="minuti2" ErrorMessage="Specificare i minuti di fine noleggio." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="cerca"> </asp:RequiredFieldValidator>
                      
    <asp:RangeValidator ID="RangeValidator4" runat="server" 
        ControlToValidate="minuti2" 
        ErrorMessage="Specificare un valore corretto per i minuti di fine noleggio." Font-Size="0pt" 
        Type="Integer" ValidationGroup="cerca" 
        MinimumValue="0" MaximumValue="59" > </asp:RangeValidator>   
    
    <asp:CompareValidator ID="CompareValidator5" runat="server" 
    
                                ControlToValidate="txtEtaPrimo" 
                                ValueToCompare="0"
                                Font-Size="0pt"  Operator="GreaterThan" 
                                ErrorMessage="Specificare correttamente l'età del primo conducente." Type="Integer" ValidationGroup="cerca"> </asp:CompareValidator>
    
    <asp:CompareValidator ID="CompareValidator6" runat="server" 
                                ControlToValidate="txtEtaSecondo" 
                                ValueToCompare="0"
                                Font-Size="0pt"  Operator="GreaterThan" 
                                ErrorMessage="Specificare correttamente l'età del secondo conducente." Type="Integer" ValidationGroup="cerca"> </asp:CompareValidator>
    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                ControlToValidate="txtCodiceCliente" 
                                ValueToCompare="0"
                                Font-Size="0pt"  Operator="GreaterThan" 
                                ErrorMessage="Specificare correttamente il codice cleinte." Type="Integer" ValidationGroup="cerca"> </asp:CompareValidator>
        
       <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender3"
          ControlToValidate="txtoraPartenza" Display="Dynamic" EmptyValueMessage="Specificare l'orario di pick-up."  IsValidEmpty="false"
          InvalidValueMessage="Orario di pick-up non valido" ValidationGroup="cerca" Font-Size="0pt"></ajaxToolkit:MaskedEditValidator>
       <ajaxtoolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender3" 
          ControlToValidate="txtOraRientro" Display="Dynamic" 
          EmptyValueMessage="Specificare l'orario di drop-off." InvalidValueMessage="Orario di drop-off non valido." IsValidEmpty="false"
          ValidationGroup="cerca" Font-Size="0pt"></ajaxtoolkit:MaskedEditValidator>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                   DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
                   ValidationGroup="cerca" />
    
    <asp:CompareValidator ID="CompareValidator10" runat="server" 
                                  ControlToValidate="txtSconto" 
                                  Font-Size="0pt" Operator="GreaterThanEqual" Type="Double" 
                                  ValidationGroup="cerca" ValueToCompare="0" ErrorMessage="Specificare un valore corretto per lo sconto da applicare."> </asp:CompareValidator>
  <asp:CompareValidator ID="CompareValidator11" runat="server" 
                                  ControlToValidate="txtSconto" 
                                  Font-Size="0pt" Operator="LessThanEqual" Type="Double" 
                                  ValidationGroup="cerca" ValueToCompare="100" ErrorMessage="Specificare un valore corretto per lo sconto da applicare."> </asp:CompareValidator>
  
  

</asp:Content>

