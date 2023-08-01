<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="prenotazioniTony.aspx.vb" Inherits="prenotazioniTony" 
    MaintainScrollPositionOnPostback="true" EnableEventValidation="true"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<%@ Register Src="/anagrafica/anagrafica_conducenti.ascx" TagName="anagrafica_conducenti" TagPrefix="uc1" %>
<%@ Register Src="/anagrafica/anagrafica_ditte.ascx" TagName="anagrafica_ditte" TagPrefix="uc1" %>
<%@ Register Src="/tabelle_pos/scambio_importo.ascx" TagName="scambio_importo" TagPrefix="uc1" %>
<%@ Register Src="/gestione_danni/gestione_note.ascx" TagName="gestione_note" TagPrefix="uc1" %>
<%@ Register Assembly="BoxOver" Namespace="BoxOver" TagPrefix="boxover" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript" language="javascript" src="/lytebox.js"></script>
     <link rel="stylesheet" href="/lytebox.css" type="text/css" media="screen" />
    
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <style type="text/css">
        .toolheader {
        background-color:#19191b;
        color:#FFFFFF;
        font-weight:bold;
        font-size:14px;
        font-family:Verdana, Geneva, Tahoma, sans-serif;
        width:400px;
        padding:6px;
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
        font-size:12px;
        font-family:Verdana, Geneva, Tahoma, sans-serif;
        background-color:#FFFFFF;
        padding:6px;
        line-height:18px;
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
            width: 125px;
        }
        .style18
        {
            width: 53px;
        }
        .style27
        {
        }
        .style28
        {
        }
        .style32
        {
            width: 128px;
        }
        .style33
        {
        }
        .style34
        {
            width: 68px;
        }
        .style36
        {
            width: 154px;
        }
        .style37
        {
            width: 170px;
        }
        .style38
        {
            width: 67px;
        }
        .style39
        {
            width: 107px;
        }
        .style41
        {
            width: 811px;
        }
        .style42
        {
            width: 134px;
        }
        .style43
        {
            width: 15px;
        }
        .style44
        {
            width: 100px;
        }
        .style45
        {
            width: 120px;
        }
        .style46
        {
        }
        .style47
        {
            width: 210px;
        }
        .style48
        {
            width: 150px;
        }
        .style49
        {
            width: 148px;
        }
        .style50
        {
        }
        .style51
        {
            width: 201px;
        }
        .style52
        {
            width: 141px;
        }
        .style53
        {
            width: 11px;
        }
        #myBody
        {
            font-weight: 700;
        }
        .style55
        {
            width: 192px;
        }
        .style56
        {
            width: 171px;
        }
        .style57
        {
            width: 186px;
        }
        .style58
        {
            width: 102px;
        }
        .style59
        {
            width: 138px;
        }
        .style60
        {
            width: 132px;
        }
         /* th{
            padding:3px;
            letter-spacing:1px;
        }
        td{
            padding:3px;
        }*/
         .ddlist{
            height :20px;
        }
        
         .ffont{
             font-family:Arial;
             font-size:12px;
         }



        </style>

          <%--start multiple submit 23.02.2022 --%>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script type="text/javascript">

        var isSubmitted = false;

        function preventMultipleSubmissions(ele, e, nrco) {


            //alert(ele.value); //test
            //return false;

            var nomeele = ele.value;

           /* if (nomeele == 'Invia RA (OK)') { nomeele='Invia RA' }*/

            if (confirm('Confermi ' + nomeele + '?') == false) {
                return false;
            } else {

                if (!isSubmitted) {

                    $('body').css('cursor', 'wait');

                    if (ele.value == 'Salva' || ele.value == 'Modifica') {

                        $(ele).val('wait...');
                        $(ele).css('cursor', 'wait');
                        
                       /* $(ele).css('width', '160px');*/
                       

                        /*$(ele).css('visibility', 'hidden');*/

                    } else {

                                               
                    }

                    isSubmitted = true;
                    return true;

                }
                else {
                    return false;
                }

            }


        }
    </script>
    <%--end multiple submit--%>


</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;" class="style1">
               <asp:Label ID="Label14" runat="server" Text="Ricerca Veloce" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
    </table>
<div runat="server" id="tab_ricerca">
    
     <table cellpadding="0" cellspacing="6" width="1024px" style="border:4px solid #444" border="0">
       <tr>
          <td class="style32">
             <asp:Label ID="Label32" runat="server" Text="Num. Prenotazione" CssClass="testo_bold"></asp:Label>
          </td>
          <td><asp:Label ID="Label43" runat="server" Text="Num. Contratto" CssClass="testo_bold"></asp:Label></td>
           <td><asp:Label ID="Label44" runat="server" Text="Num. Fattura / Anno" CssClass="testo_bold"></asp:Label></td>
       </tr>
       <tr>
          <td class="style32"><asp:TextBox ID="txtNumPrenotazione" runat="server" Width="120px"></asp:TextBox></td>
          <td class="style32"><asp:TextBox ID="txt_num_contratto" runat="server" Width="120px"></asp:TextBox></td>
           <td class="style32"><asp:TextBox ID="txt_num_fattura_anno" runat="server" Width="60px"></asp:TextBox> <%--aggiunto 07.04.2022--%>
               <asp:DropDownList ID="ddl_anno_fattura" runat="server" >
                  <%-- <asp:ListItem Value="2022">2022</asp:ListItem>
                   <asp:ListItem Value="2021">2021</asp:ListItem>
                   <asp:ListItem Value="2020">2020</asp:ListItem>
                   <asp:ListItem Value="2019">2019</asp:ListItem>
                   <asp:ListItem Value="2018">2018</asp:ListItem>
                   <asp:ListItem Value="2017">2017</asp:ListItem>
                   <asp:ListItem Value="2016">2016</asp:ListItem>
                   <asp:ListItem Value="2015">2015</asp:ListItem>
                   <asp:ListItem Value="2014">2014</asp:ListItem>
                   <asp:ListItem Value="2013">2013</asp:ListItem>--%>
               </asp:DropDownList>
               <asp:CheckBox CssClass="ffont" ID="ck_fattura_multe" runat="server"  Text="Multe"/> </td>
       </tr>
       
        <tr>
          <td align="left" ><asp:Button ID="btnRichiamaPrenotazione" runat="server" Text="Richiama Prenotazione" /></td>
           <td align="left" ><asp:Button ID="btnRichiamaContratto" runat="server" Text="Richiama Contratto" /></td>
           <td align="left" ><asp:Button ID="btnRichiamaFatturaAnno" runat="server" Text="Richiama Contratto rif.Fattura" /></td>

       </tr>



     </table>


</div>  <%--div ricerca semplice--%>

<div runat="server" id="tab_cerca_tariffe" visible="false" >
<table border="0" cellspacing="2" cellpadding="2" width="1024px" style="border:4px solid #444">
<tr>
            <td colspan="10" align="center" style="color: #FFFFFF;background-color:#444;" 
                class="style1">
               <asp:Label ID="lblTipoDocumento" runat="server" Text="Prenotazione Numero:" CssClass="testo_titolo"></asp:Label>&nbsp;
               <asp:Label ID="lblNumPrenotazione" runat="server"  CssClass="testo_titolo"></asp:Label>
               &nbsp;
               <asp:Label ID="lblNumeroVariazione" runat="server"  CssClass="testo_titolo"></asp:Label>
               &nbsp;
               <asp:Label ID="lblComplimentary" runat="server" Text="COMPLIMENTARY"  CssClass="testo_titolo" ForeColor="Red" Visible="false"></asp:Label>
               &nbsp;
               <asp:Label ID="lblFullCredit" runat="server" Text="FULL CREDIT"  CssClass="testo_titolo" ForeColor="Red" Visible="false"></asp:Label>
               &nbsp;&nbsp;&nbsp;
               <asp:Label ID="Label15" runat="server" Text="Data Prenotazione:" CssClass="testo_titolo"></asp:Label>&nbsp;
               <asp:Label ID="lblDataPrenotazione" runat="server"  CssClass="testo_titolo"></asp:Label>&nbsp;&nbsp;
               <asp:Label ID="lblDataAnnullamento" runat="server"  CssClass="testo_titolo"></asp:Label>
               <asp:Label ID="lblDataRipristino" runat="server"  CssClass="testo_titolo"></asp:Label>
               &nbsp;&nbsp;
               <asp:Label ID="lblPrenotazioneScaduta" runat="server"  CssClass="testo_titolo" Visible="false" Text="PRENOTAZIONE SCADUTA"></asp:Label>
           </td>
  </tr>
  <tr>
    <td  colspan="10" align="center" style="color: #FFFFFF;background-color:#444;" 
          class="style1">
              <asp:Label ID="lblTipoDocumento0" runat="server" Text="Operatore Creazione:" CssClass="testo_titolo"></asp:Label>
              <asp:Label ID="lblOperatoreCreazione" runat="server"  CssClass="testo_titolo"></asp:Label>&nbsp;&nbsp;
              <asp:Label ID="lblOperatoreModifica" runat="server"  CssClass="testo_titolo"></asp:Label>&nbsp;&nbsp;
              <asp:Label ID="lblOperatoreAnnullamento" runat="server"  CssClass="testo_titolo"></asp:Label>
              <asp:Label ID="lblOperatoreRipristino" runat="server"  CssClass="testo_titolo"></asp:Label>
    </td>
  </tr>
<tr>
  <td class="style42" valign="top">
    <asp:Label ID="Label2" runat="server" Text="Pick Up" CssClass="testo_bold"></asp:Label>
  </td>
  <td class="style15" valign="top">
        <asp:DropDownList ID="dropStazionePickUp" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
            DataValueField="id">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
      <asp:Label ID="OldStazionePickUp" runat="server" Visible="false"></asp:Label>
  </td>
  <td valign="top" class="style18">
      <a onclick="Calendar.show(document.getElementById('<%= txtDaData.ClientID%>'), '%d/%m/%Y', false)">
    <asp:TextBox runat="server" Width="80px" ID="txtDaData"></asp:TextBox>
          </a>
      <asp:Label ID="txtOldDaData" runat="server" Visible="false"></asp:Label>
       <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDaData" ID="CalendarExtender2">
        </ajaxtoolkit:CalendarExtender>--%>
        <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDaData" ID="MaskedEditExtender1">
        </ajaxtoolkit:MaskedEditExtender>
  </td>
  <td class="style42" valign="top">
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
          <asp:TextBox ID="ore1" runat="server" Width="29px" MaxLength="2" Visible="false"></asp:TextBox>
          <asp:TextBox ID="minuti1" runat="server" Width="29px" MaxLength="2" Visible="false"></asp:TextBox>
       
  </td>
  <td valign="top" colspan="6">
  
        <asp:DataList ID="listWarningPickPrenotazioni" runat="server"  
            DataSourceID="sqlWarningPickPrenotazioni" RepeatColumns="1" 
            RepeatDirection="Horizontal">
            <ItemTemplate>
                <img src="punto_elenco.jpg"  border="0" alt="" title="" width="8" height="7" />&nbsp;<asp:Label ID="warning" runat="server" Text='<%# Eval("warning") %>' CssClass="testo_bold" />&nbsp;
                <asp:Label ID="tipo" runat="server" Text='<%# Eval("tipo") %>' visible="false" />
            </ItemTemplate>
        </asp:DataList>
  
  </td>
</tr>
<tr>
  <td class="style42" valign="top">
     <asp:Label ID="Label1" runat="server" Text="Drop Off" CssClass="testo_bold"></asp:Label>
  </td>
  <td class="style15" valign="top">
    <asp:DropDownList ID="dropStazioneDropOff" runat="server" 
        AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
        DataValueField="id">
        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>    
    </asp:DropDownList>
    <asp:Label ID="OldStazioneDropOff" runat="server" Visible="false"></asp:Label>
  </td>
  <td class="style18" valign="top">
      <a onclick="Calendar.show(document.getElementById('<%= txtAData.ClientID%>'), '%d/%m/%Y', false)">
    <asp:TextBox runat="server" Width="80px" ID="txtAData" ></asp:TextBox>
          </a>
      <asp:Label ID="txtOldAData" runat="server" Visible="false"></asp:Label>
      <%--  <ajaxtoolkit:CalendarExtender  runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtAData" ID="CalendarExtender3">
        </ajaxtoolkit:CalendarExtender>--%>
        <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtAData" ID="MaskedEditExtender2">
        </ajaxtoolkit:MaskedEditExtender>
  </td>
  <td class="style42" valign="top">
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
  <td valign="top" colspan="6">
  
        <asp:DataList ID="listWarningDropPrenotazioni" runat="server"  
            DataSourceID="sqlWarningDropPrenotazioni" RepeatColumns="1" 
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
  <td class="style42" valign="top">
    <asp:Label ID="Label3" runat="server" Text="Fonte" CssClass="testo_bold"></asp:Label>
  </td>
  <td class="style15" valign="top">
      <asp:DropDownList ID="dropTipoCliente" runat="server" 
          AppendDataBoundItems="True" DataSourceID="sqlTipoClienti" 
          DataTextField="descrizione" DataValueField="id">
          <asp:ListItem Selected="True" Value="0">Generico...</asp:ListItem>
      </asp:DropDownList>
  &nbsp;</td>
  <td class="style18" valign="top">
  
    <asp:Label ID="Label78" runat="server" Text="Codice Ditta" CssClass="testo_bold"></asp:Label>
    </td>
  <td class="style42" valign="top">
    <asp:TextBox ID="txtCodiceCliente" runat="server" Width="68px"></asp:TextBox>
  </td>
  <td valign="top" class="style43">
  
      
     <asp:Label ID="Label79" runat="server" Text="Ditta" 
          CssClass="testo_bold"></asp:Label>
  
      
  </td>
  <td valign="top" colspan="3">
  
      
      <asp:Label ID="lblNomeDitta" runat="server" Text="" CssClass="testo"></asp:Label>
  
      
  </td>
  <td valign="top" class="style58">
  
      
      &nbsp;</td>
  <td valign="top">
  
      
      &nbsp;</td>
</tr>
<tr>
  <td class="style42" valign="top">
    <asp:Label ID="Label19" runat="server" Text="Età primo guid.:" CssClass="testo_bold"></asp:Label>
  </td>
  <td valign="top">
    <asp:TextBox ID="txtEtaPrimo" runat="server" Width="40px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
  </td>
  <td valign="top" colspan="2">
      <asp:Label ID="Label20" runat="server" Text="Età secondo guid.:" CssClass="testo_bold"></asp:Label>
      
    <asp:TextBox ID="txtEtaSecondo" runat="server" Width="40px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
      
    </td>

  <td valign="top" class="style43">
  
      
     <asp:Label ID="Label23" runat="server" Text="Giorni" CssClass="testo_bold"></asp:Label>
  
      
    </td>

  <td valign="top" class="style53">
  
      
              <asp:TextBox ID="txtNumeroGiorni" runat="server" Width="36px" ReadOnly="true"></asp:TextBox>
  
      
    </td>

  <td valign="top" class="style44">
  
      
     <asp:Label ID="lblGiorniTO" runat="server" Text="Giorni T.O." CssClass="testo_bold"></asp:Label>
  
      
     <asp:Label ID="lblGiorniPrepagati" runat="server" Text="GG. Prepag." CssClass="testo_bold" Visible="false"></asp:Label>
  
      
    </td>

  <td valign="top">      
      
      <asp:TextBox ID="txtNumeroGiorniTO" runat="server" Width="36px" Enabled="false"></asp:TextBox>
      <asp:TextBox ID="txtGiorniPrepagati" runat="server" Width="36px" Enabled="false" Visible="false"></asp:TextBox>
      <asp:Label ID="lblGiorniToOld" runat="server" visible="false"></asp:Label>
      
    </td>

  <td valign="top" class="style58">
  
      
         <asp:Label ID="Label132" runat="server" Text="Gruppo" CssClass="testo_bold"></asp:Label>
  
      
    </td>

  <td valign="top">  
      
          <asp:DropDownList ID="gruppoDaCalcolare" runat="server" AppendDataBoundItems="True" 
              DataSourceID="sqlGruppiAuto" DataTextField="cod_gruppo" 
              DataValueField="id_gruppo">
              <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
              <%--<asp:ListItem Value="65">E (Autom.)</asp:ListItem>--%>
          </asp:DropDownList>
    </td>

</tr>

<tr runat="server" id="riga_broker" visible="false">
  <td class="style42" valign="top">
    <asp:Label ID="Label82" runat="server" Text="Variazione a carico:" CssClass="testo_bold"></asp:Label>
  </td>
  
    <td valign="top">
      <asp:DropDownList ID="dropVariazioneACaricoDi" runat="server" AppendDataBoundItems="True" Enabled="false">
          <asp:ListItem  Value="-1">...</asp:ListItem>
          <asp:ListItem  Value="1">Broker</asp:ListItem>
          <asp:ListItem  Value="0">Cliente</asp:ListItem>
      </asp:DropDownList>
    </td>
  <td valign="top" colspan="2">
      &nbsp;</td>

  <td valign="top" class="style43">
  
      
      &nbsp;</td>

  <td valign="top" class="style53">
  
      
              &nbsp;</td>

  <td valign="top" class="style44">
  
      
      &nbsp;</td>

  <td valign="top" colspan="3">
  
      
      &nbsp;</td>
</tr>
<tr>
  <td class="style28" valign="top" colspan="10">
  
        <asp:DataList ID="listWarningDropRibaltamento" runat="server"  
            DataSourceID="sqlWarningDropRibaltamento" RepeatColumns="1" 
            RepeatDirection="Horizontal">
            <ItemTemplate>
                <img src="punto_elenco.jpg"  border="0" alt="" title="" width="8" height="7" />&nbsp;<asp:Label ID="warning" runat="server" Text='<%# Eval("warning") %>'  ForeColor="Red" CssClass="testo_bold" />&nbsp;
                <asp:Label ID="tipo" runat="server" Text='<%# Eval("tipo") %>' visible="false" />
            </ItemTemplate>
        </asp:DataList>
  
        <asp:Label ID="lbl_bloccata_ribaltamento" runat="server" CssClass="testo_bold" ForeColor="Red"  Text='Prenotazione bloccata da ribaltamento - Una modifica successiva è andata in errore. Contattare la sede.' Visible="false" />
  
  </td>
</tr>
<tr>
  <td class="style14" valign="top" colspan="10" align="center">
      <asp:Button ID="btnRicalcola" runat="server" Text="Ricalcola" Visible="false" ValidationGroup="cerca" Width="80px" />&nbsp;&nbsp;&nbsp;
      <asp:Button ID="btnRicalcolaPrepagato" runat="server" Text="Ricalcola - PREPAGATO" Visible="false" ValidationGroup="cerca" Width="186px" />
    &nbsp;<asp:Button ID="btnCerca" runat="server" Text="Cerca" ValidationGroup="cerca" />
  
  &nbsp;<asp:Button ID="btnAnnulla0" runat="server" Text="Annulla" />

  &nbsp;<asp:Button ID="btnAnnulla6" runat="server" Text="Chiudi" />

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



<table style="border:4px solid #444; border-top:0px;" border="0" cellspacing="0" cellpadding="2" width="1024px" runat="server" id="tab_conducenti" >
  <tr>
    <td class="style50" colspan="6" style="color: #FFFFFF;background-color:#444;" valign="align" >
        <asp:Label ID="Label31" runat="server" Text="Guidatore" CssClass="testo_titolo"></asp:Label>
    &nbsp;
        
        <a id="vediPrimoGuidatore" runat="server" href="" rel="lyteframe" 
            rev="width: 740px; height: 740px; scrolling: yes;" title="">
        <asp:Image ID="image_primo_guidatore" runat="server" Visible="false" 
            ImageUrl="images/lente.png" ToolTip="Vedi dettagli del guidatore" />
            
        </a>&nbsp; 
        <asp:Button ID="btnModificaConducente" runat="server" Text="Scegli" Font-Size="X-Small"
            ToolTip="Modifica conducente" Height="18px" />
      
    </td>
  </tr>
  <tr>
    <td class="style52">
      
    <asp:Label ID="Label27" runat="server" Text="Cognome" CssClass="testo_bold"></asp:Label>
      
    </td>
    <td class="style51">
      
    <asp:Label ID="Label26" runat="server" Text="Nome" CssClass="testo_bold"></asp:Label>
      
    </td>
    <td class="style47" colspan="2">
      
    <asp:Label ID="Label28" runat="server" Text="E-Mail" CssClass="testo_bold"></asp:Label>
      
    </td>
    <td colspan="2">
      
    <asp:Label ID="Label85" runat="server" Text="Indirizzo" CssClass="testo_bold"></asp:Label>
      
      </td>
  </tr>
  <tr>
    <td class="style52" valign="top">
        &nbsp;
              
        <asp:TextBox ID="txtCognomeConducente" runat="server" Width="118px"></asp:TextBox>
      
    </td>
    <td valign="top" class="style51">
      
        <asp:TextBox ID="txtNomeConducente" runat="server" Width="121px"></asp:TextBox>
      
    </td>
    <td colspan="2" valign="top">
      
        <%--<a href="/gestione_conducenti.aspx" rel="lyteframe" title="Registrati su RentOnWeb.it" rev="width: 750px; height: 700px; scrolling: yes;"><asp:Image ID="Image1" runat="server" ImageUrl="images/aggiorna.png" /></a>--%>
              
        <asp:TextBox ID="txtMailConducente" runat="server" Width="170px"></asp:TextBox>
        
      </td>
    <td valign="top" colspan="2">
      
        <asp:TextBox ID="txtIndirizzoConducente" runat="server" Width="238px" ReadOnly="true"></asp:TextBox>
      
      </td>
  </tr>
<tr>
    <td class="style52">
      
    <asp:Label ID="Label64" runat="server" Text="Data di Nascita" CssClass="testo_bold"></asp:Label>
      
    </td>
    <td class="style51"> 
      
    <asp:Label ID="Label29" runat="server" Text="Gruppo da Consegnare" CssClass="testo_bold"></asp:Label>
      
    </td>
    <td class="style49">
      <asp:Label ID="Label30" runat="server" Text="Numero Volo (Arrivo)" CssClass="testo_bold"></asp:Label>
    </td>
    <td class="style48">
      <asp:Label ID="Label86" runat="server" Text="Numero Volo (Rientro)" 
            CssClass="testo_bold"></asp:Label>  
    </td>
    <td valign="top" class="style55">
      
      <asp:Label ID="Label40" runat="server" Text="Numero Riferimento T.O." CssClass="testo_bold"></asp:Label>
      
      </td>
    <td valign="top">
      
        <asp:Label ID="Label87" runat="server" CssClass="testo_bold" Text="Rif. Tel."></asp:Label>
    </td>
  </tr>
  <tr>
    <td class="style52">
        &nbsp;&nbsp;
              <a onclick="Calendar.show(document.getElementById('<%= txtDataDiNascita.ClientID%>'), '%d/%m/%Y', false)">
        <asp:TextBox runat="server" Width="80px" ID="txtDataDiNascita" ></asp:TextBox>
                  </a>
    <%--    <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                  TargetControlID="txtDataDiNascita" 
            ID="txtCercaPickUpDa0_CalendarExtender1">
        </ajaxtoolkit:CalendarExtender>--%>
        <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                  CultureDatePlaceholder="" CultureTimePlaceholder="" 
                  CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                  CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                  CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataDiNascita" 
                  ID="txtCercaPickUpDa0_MaskedEditExtender1">
        </ajaxtoolkit:MaskedEditExtender>
      
      </td>
    <td class="style51">
      
        <asp:DropDownList ID="dropGruppoDaConsegnare" runat="server" 
            DataSourceID="sqlGruppiAuto" DataTextField="cod_gruppo" 
            DataValueField="id_gruppo" AppendDataBoundItems="True">
            <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
        </asp:DropDownList>
      
      </td>
    <td class="style49">
      
        <asp:TextBox ID="txtVoloOut" runat="server" Width="100px" 
            MaxLength="10"></asp:TextBox>
      
      </td>
    <td class="style48">
      
        <asp:TextBox ID="txtVoloPr" runat="server" Width="100px" 
            MaxLength="10"></asp:TextBox>
      
      </td>
    <td valign="top" class="style55">
      
        <asp:TextBox ID="txtRiferimentoTO" runat="server" Width="100px" MaxLength="15"></asp:TextBox>
      &nbsp;&nbsp;&nbsp; 
      
      </td>
    <td valign="top">
      
        <asp:TextBox ID="txtRifTel" runat="server" MaxLength="30" Width="100px"></asp:TextBox>
      </td>
  </tr>
  <tr>
    <td class="style46" valign="top" colspan="6" align="center">
        <asp:Label ID="lblRiferimentoEsistente" runat="server" 
            Text="NUM. RIFERIMENTO ESISTENTE" CssClass="testo_bold" 
            ForeColor="Red" Visible="false"></asp:Label>
      
        <asp:Label ID="lblRifToOld" runat="server" Visible="false"></asp:Label>
    </td>
  </tr>
  <tr>
    <td class="style46" colspan="6">
      
      <asp:Label ID="Label80" runat="server" Text="Fatturare a ditta" 
            CssClass="testo_bold"></asp:Label>
      
        :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      
        <asp:TextBox ID="txtNomeDitta" runat="server" Width="230px"  ReadOnly="True"></asp:TextBox>
      
      &nbsp;<asp:Button ID="btnModificaDitta" runat="server" Text="..." />
      
        <asp:Label ID="id_conducente" runat="server" visible="false"></asp:Label>
      
        <asp:Label ID="id_ditta" runat="server" Visible="false"></asp:Label>
      
        &nbsp;&nbsp;&nbsp;
      
        <asp:Label ID="lblPrepagato" runat="server" Text="CLIENTE PREPAGATO" Visible="false" style="font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:14px;font-weight:bold;" ></asp:Label>  
      
      </td>
  </tr>
  <tr>
    <td id="Td1" runat="server" align="center" colspan="6">
       
        <asp:Button ID="btnStampa" runat="server" Text="Stampa"  />
  
    &nbsp;<asp:Button ID="btnPagamento" runat="server" Text="Pagamento" Visible="false" />
  
    &nbsp;<asp:Button ID="btnContratto" runat="server" Text="Contratto" Visible="false" />
  
    &nbsp;<asp:Button ID="btnSalvaModifiche" runat="server" Text="Salva modifiche" Visible="false" />
  
    &nbsp;<asp:Button ID="btnModificaDatiCliente" runat="server" Text="Modifica dati cliente" Visible="false" />
        <%--OnClientClick="return confirm('Vuoi modificare i dati cliente?');"--%>
  
    &nbsp;<asp:Button ID="btnModificaPrenotazione" runat="server" Text="Modifica Prenotazione" Visible="true" />
  
    &nbsp;<asp:Button ID="btnInviaMail" runat="server" Text="Invia Mail" />

   &nbsp;
   <asp:Label ID="lblContrattoNum" runat="server" Visible="false" Text="Contratto N." CssClass="testo_bold" Font-Size="Medium"></asp:Label>
    &nbsp;<asp:Label ID="numContratto" runat="server" Visible="false" CssClass="testo_bold" Font-Size="Medium"></asp:Label>
        &nbsp;
  
      &nbsp;&nbsp;</td>
  </tr>
</table>
<table width="1024px">
   <tr>
      <td>
         <uc1:gestione_note id="gestione_note" runat="server"></uc1:gestione_note>
      </td>
   </tr>
</table>
</div>
<table style="border:4px solid #444;" border="0" cellspacing="0" cellpadding="1" width="1024px" runat="server" id="tab_assegnazione_targa" >
   <tr>
      <td>
      
    <asp:Label ID="Label83" runat="server" Text="Veicolo" CssClass="testo_bold"></asp:Label>
      
      &nbsp;<asp:TextBox ID="txtTarga" runat="server" Width="84px" MaxLength="15"></asp:TextBox>
            &nbsp;<asp:Button ID="btnAssegnaTarga" runat="server" Text="Assegna" />
  
      &nbsp;<asp:Button ID="btnAnnullaAssegnaTarga" runat="server" Text="Annulla" Visible="false" />
  
          <asp:Label ID="lblTargaSelezionata" runat="server" Visible="false"></asp:Label>
  
      </td>
   </tr>
</table>
<br />
<table style="border:4px solid #444;" border="0" cellspacing="0" cellpadding="1" width="1024px" runat="server" id="tab_annullamento" >
   <tr>
     <td>
     
    <asp:Label ID="Label77" runat="server" Text="Motivo annullamento" CssClass="testo_bold"></asp:Label>
      
     <asp:DropDownList ID="dropMotivoAnnullamento" runat="server" 
             DataSourceID="sqlMotivoAnnullamento" DataTextField="descrizione" 
             DataValueField="id" AppendDataBoundItems="True">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>    
        </asp:DropDownList>
      
      &nbsp;<asp:Button ID="btnRipristinaPrenotazione" runat="server" 
             Text="Ripristina prenotazione" Visible="false" Width="170px" />
             
         &nbsp;<asp:Button ID="btnAnnullaPrenotazione" runat="server" 
             Text="Annulla prenotazione" Width="170px" />
             
         &nbsp;<asp:Button ID="btnRichiestaAnnullamento" runat="server" 
             Text="Richiesta di annullamento" Visible="false" Width="190px" />
     
     &nbsp;
        
        <asp:Label ID="lblRichiestaAnnullamento" runat="server" Text="RICHIESTO L'ANNULLAMENTO DELLA PRENOTAZIONE." CssClass="testo_bold" ForeColor="Red" Visible="false"></asp:Label>
        <asp:Label ID="lblPrenotazioneAnnullata" runat="server" Text="PRENOTAZIONE ANNULLATA." CssClass="testo_bold" ForeColor="Red" Visible="false"></asp:Label>
     </td>
   </tr>
</table>
<br />
&nbsp;
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
    <asp:Button ID="btnAnnulla1" runat="server" Text="Annulla" />
  
  </td>
  </tr>
</table>

<table border="0" cellspacing="2" cellpadding="2" width="1024px" runat="server" id="table_tariffe" visible="false">
 <tr>
    <td class="style56">
      <asp:Label ID="Label8" runat="server" Text="Tariffe Generiche:" CssClass="testo_bold"></asp:Label>
    </td>
    <td class="style57">
        <asp:DropDownList ID="dropTariffeGeneriche" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlTariffeGeneriche" 
            DataTextField="codice" DataValueField="id">
            <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
    </td>
    <td>
     
  
        <asp:Label ID="Label9" runat="server" Text="Tariffe Particolari:" CssClass="testo_bold"></asp:Label>
     &nbsp;&nbsp; <asp:DropDownList ID="dropTariffeParticolari" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlTariffeParticolari" 
            DataTextField="codice" DataValueField="id">
            <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
      </asp:DropDownList>
   
        
        </td>
 </tr>
 <tr>
    <td class="style56">
      <asp:Label ID="Label88" runat="server" Text="Convenzione:" 
             CssClass="testo_bold"></asp:Label>
        </td>
    <td class="style57">
         <asp:TextBox ID="txtCodiceSconto" runat="server" Width="144px" MaxLength="15"></asp:TextBox>
    <asp:Button ID="btnApplicaCodiceSconto" runat="server" Text="..." />
  
            </td>
    <td>
     
  
        <asp:Label ID="Label10" runat="server" Text="Applica Sconto:" 
            CssClass="testo_bold"></asp:Label>
        <asp:TextBox ID="txtSconto" runat="server" Width="32px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
        &nbsp;&nbsp;<b>%<asp:DropDownList ID="dropTipoSconto" runat="server" 
            AppendDataBoundItems="True">
            <asp:ListItem Selected="false" Value="0">Elementi Scontabili</asp:ListItem>
            <asp:ListItem Selected="true" Value="1">Valore Tariffa</asp:ListItem>
        </asp:DropDownList>
   
        
        <asp:Label ID="lblMxSconto" runat="server" Text="Applicato il MASSIMO SCONTO." 
            CssClass="testo_bold" ForeColor="Red" Visible="false"></asp:Label>
         </b>
        </td>
 </tr>
 <tr>
    <td align="left" class="style56">
     &nbsp;</td>
    <td align="right" colspan="2">
         <asp:Label ID="lblCodiceScontoAttuale" runat="server" CssClass="testo_bold" ForeColor="Red" Visible="false"></asp:Label>
         <asp:Label ID="lblScontoAttuale" runat="server" CssClass="testo_bold" ForeColor="Red" Visible="false"></asp:Label>
         <asp:Label ID="lblTipoScontoAttuale" runat="server" CssClass="testo_bold" 
            ForeColor="Red" Visible="false"></asp:Label>
        <asp:Label ID="lblMinGiorniNolo" runat="server" CssClass="testo_bold"></asp:Label>&nbsp;
        <asp:Button ID="btnStampaTKm" runat="server" Text="Vedi Valore Tariffa" UseSubmitBehavior="false" />
        <asp:Button ID="btnStampaCondizioni" runat="server" Text="Vedi Condizioni" UseSubmitBehavior="false" />
    
    </td>
 </tr>
 <tr runat="server" id="riga_commissioni" visible="false">
    <td align="left" class="style56">
     <asp:Label ID="lblFonteCommissionabile" runat="server" Text="Fonte Commissionabile:" CssClass="testo_bold"></asp:Label>
     </td>
    <td align="left" colspan="2">
         <asp:DropDownList ID="dropFonteCommissionabile" runat="server" 
             AppendDataBoundItems="True" DataSourceID="sqlFontiCommissionabili" 
             DataTextField="rag_soc" DataValueField="id">
             <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
         </asp:DropDownList>
         &nbsp;
         <asp:TextBox ID="txtPercentualeCommissionabile" runat="server" Readonly="True" Width="32px"></asp:TextBox>
         <asp:Label ID="lblPercentualeCommissionabile" runat="server" CssClass="font-bold" Text="%"></asp:Label>
         &nbsp;
        <asp:DropDownList ID="dropTipoCommissione" runat="server" 
            AppendDataBoundItems="True">
            <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
            <asp:ListItem Selected="False" Value="1">Da riconoscere</asp:ListItem>
            <asp:ListItem Selected="False" Value="2">Preincassate</asp:ListItem>
        </asp:DropDownList>&nbsp;
    
         <asp:Label ID="lblGGcommissioniOriginali" runat="server" Visible="false"></asp:Label>
    
    </td>
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
    <asp:Button ID="btnProsegui" runat="server" Text="Vedi tariffe" 
          style="height: 26px" />
  &nbsp;<asp:Button ID="btnCambiaTariffa" runat="server" Text="Cambia tariffa/sconto" Visible="false" />
  &nbsp;<asp:Button ID="btnAnnulla2" runat="server" Text="Annulla" Visible="false" />
  
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
       
       &nbsp;
       
       <asp:Button ID="btnVediUltimoCalcolo" runat="server" Text="Vedi dett. precedente" />
        
  
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
  <td align="left" valign="top" runat="server" id="colonna_list_prenotazioni">
      <asp:DataList ID="listPrenotazioniCosti" runat="server" DataSourceID="sqlPrenotazioniCosti" Width="100%">
          <ItemTemplate>
              <tr runat="server" id="riga_gruppo" >
                 <td bgcolor="#19191b" style="width:100%;" colspan="8">
                      <asp:Label ID="Label7" runat="server" Text="Gruppo:" CssClass="testo_bold_2"  />
                      <asp:Label ID="id_gruppoLabel" runat="server" Text='<%# Eval("id_gruppo") %>' Visible="false" />
                      <asp:Label ID="gruppo" runat="server" Text='<%# Eval("gruppo") %>' CssClass="testo_bold_2" />
                      &nbsp;&nbsp;&nbsp;
                      <asp:Button ID="scegli" runat="server" Text="Scegli" CommandName="scegli" Visible="false" /> 
                 </td>
              </tr>
              <tr runat="server" id="riga_intestazione">
                 <td width="4%">
                 </td>
                 <td width="2%">
                 </td>
                 <td width="48%">
                 </td>
                 <td width="12%">
                      <asp:Label ID="labelTO" runat="server" Text='Costo T.O.' CssClass="testo_bold" Visible="false"/>
                      <asp:Label ID="labelPrepagato" runat="server" Text='Prepag.' CssClass="testo_bold" Visible="false"/>
                      <asp:Label ID="labelCommissioni" runat="server" Text='Comm.' CssClass="testo_bold" ToolTip="Commissioni" Visible="false"/>
                      &nbsp;         
                 </td>
                 <td width="12%">
                      <asp:Label ID="Label13" runat="server" Text='Sconto' CssClass="testo_bold" />&nbsp;         
                 </td>
                 <%--<td width="18%">
                     <asp:Label ID="Label11" runat="server" Text='Sconto' CssClass="testo_bold" />&nbsp;
                 </td>--%>
                 <td width="14%">
                    <asp:Label ID="Label12" runat="server" Text='Costo' CssClass="testo_bold" />&nbsp;
                 </td>
                 <td>
                   <asp:Label ID="Label21" runat="server" Text='Omaggio' CssClass="testo_bold" />
                 </td>
              </tr>
              <tr runat="server" id="riga_elementi">
                 <td width="4%" align="left" >
                     <asp:CheckBox ID="chkOldScegli" runat="server" Visible="false" Checked='<%# Eval("selezionato") %>' />
                     <asp:CheckBox ID="chkScegli" runat="server" Visible="false" Checked='<%# Eval("selezionato") %>' />
                     <asp:Label ID="lblIncluso" runat="server" Text="I " ToolTip="Incluso" CssClass="testo_bold" Visible="false"  />
                     <asp:Label ID="lblObbligatorio" runat="server" Text="O " ToolTip="Obbligatorio" CssClass="testo_bold" Visible="false"  />
                     <asp:Label ID="lblInformativa" runat="server" Text="IN " ToolTip="Informativa" CssClass="testo_bold" Visible="false"  />
                 </td>
                 <td >
                      <asp:Label ID="lblPrepagato" runat="server" Text="P" ToolTip="Prepagato" CssClass="testo_bold" Visible="false"  />&nbsp;
                 </td>
                 <td width="48%">
                      <asp:Label ID="id_preventivi_costi" runat="server" Text='<%# Eval("id") %>' visible="false" />
                      <asp:Label ID="id_elemento" runat="server" Text='<%# Eval("id_elemento") %>' visible="false" />
                      <asp:Label ID="num_elemento" runat="server" Text='<%# Eval("num_elemento") %>' visible="false" />
                      <asp:Label ID="tipologia_franchigia" runat="server" Text='<%# Eval("tipologia_franchigia") %>' Visible="false" />
                      <asp:Label ID="sottotipologia_franchigia" runat="server" Text='<%# Eval("sottotipologia_franchigia") %>' Visible="false" />
                      <asp:Label ID="is_gps" runat="server" Text='<%# Eval("is_gps") %>' visible="false" />
                      <boxover:BoxOver ID="BoxOver1"  runat="server" body='<%# Eval("nome_costo") & " - " & Eval("descrizione_lunga") %>' controltovalidate="nome_costo" header="Descrizione" CssHeader="toolheader"  CssBody="toolbody"   />
                      <asp:Label ID="nome_costo" runat="server" Text='<%# Eval("nome_costo") %>' CssClass="testo" />:&nbsp;&nbsp;
                 </td>
                 <td width="12%">
                      <asp:Label ID="a_carico_to" runat="server" Text=""  CssClass="testo" Visible="false"/>
                      <asp:Label ID="costo_prepagato" runat="server" Text='<%# FormatNumber(Eval("valore_prepagato"),2) %>' CssClass="testo" Visible="false"/>
                      <asp:Label ID="lblCommissioni" runat="server" Text='<%# FormatNumber(Eval("commissioni"),2) %>' CssClass="testo" Visible="false"/>
                 </td>
                 <td width="12%">
                      <asp:Label ID="valore_costoLabel" runat="server" Text='<%# FormatNumber(Eval("valore_costo"),2) %>' CssClass="testo" Visible="false" />&nbsp;
                      <asp:Label ID="lblSconto" runat="server" Text='<%# FormatNumber(Eval("sconto"),2) %>' Visible="false" CssClass="testo" />
                      
                      <asp:Label ID="id_a_carico_di" runat="server" Text='<%# Eval("id_a_carico_di") %>' Visible="false" />
                      <asp:Label ID="obbligatorio" runat="server" Text='<%# Eval("obbligatorio") %>' Visible="false" /> 
                      <asp:Label ID="id_metodo_stampa" runat="server" Text='<%# Eval("id_metodo_stampa") %>' Visible="false" />                    
                 </td>
                 <%--<td width="18%">
                     <asp:Label ID="lblSconto" runat="server" Text='<%# FormatNumber(Eval("sconto"),2) %>' CssClass="testo" />&nbsp;
                 </td>--%>
                 <td width="14%">
                    
                    <asp:Label ID="costo_scontato" runat="server" Text='<%# FormatNumber(FormatNumber(Eval("valore_costo"),2) - FormatNumber(Eval("sconto"),2),2) %>' CssClass="testo" />&nbsp;
                  </td>
                 <td align="center">
                     <asp:Label ID="omaggiabile" runat="server" Text='<%# Eval("omaggiabile") %>' Visible="false" />
                     <asp:Label ID="prepagato" runat="server" Text='<%# Eval("prepagato") %>' Visible="false" />
                     <asp:CheckBox ID="chkOldOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />
                     <asp:CheckBox ID="chkOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />   
                     <asp:Button ID="aggiorna" runat="server" Text="Aggiorna" CommandName="Aggiorna" Visible="false" /> 
                 </td>
              </tr>
          </ItemTemplate>
      </asp:DataList>
  
  </td>
  <td align="center" valign="top" >
  
      &nbsp;</td>
</tr>
<tr>
  <td align="left" valign="top">
  
      <asp:DataList ID="listVecchioCalcolo" runat="server" DataSourceID="sqlUltimoPrenotazioneCosti" Width="100%" Visible="false">
          <ItemTemplate>
              <tr runat="server" id="riga_gruppo0" >
                 <td bgcolor="#19191b" style="width:100%;" colspan="7" height="23px" align="left">
                      <asp:Label ID="Label7" runat="server" Text="Gruppo:" CssClass="testo"  />
                      <asp:Label ID="id_gruppoLabel" runat="server" Text='<%# Eval("id_gruppo") %>' Visible="false" />
                      <asp:Label ID="gruppo" runat="server" Text='<%# Eval("gruppo") %>' CssClass="testo_bold" />&nbsp;&nbsp;
                      <asp:Label ID="Label33" runat="server" Text="(dettaglio precedente)" CssClass="testo"  />
                      &nbsp;&nbsp;&nbsp;
                      <%--<asp:Button ID="preventivo" runat="server" Text="Preventivo" CommandName="preventivo" /> 
                      <asp:Button ID="prenotazione" runat="server" Text="Prenotazione" CommandName="prenotazione"  /> 
                      <asp:Button ID="contratto" runat="server" Text="Contratto" CommandName="contratto"  /> --%>
                 </td>
              </tr>
              <tr runat="server" id="riga_intestazione0">
                 <td width="6%">
                 </td>
                 <td width="2%">
                 </td>
                 <td width="48%">
                 </td> 
                 <td width="18%" align="left">
                      <asp:Label ID="Label13" runat="server" Text='Sconto' CssClass="testo_bold" />&nbsp;         
                 </td>
                 <%--<td width="18%" align="left">
                     <asp:Label ID="Label11" runat="server" Text='Sconto' CssClass="testo_bold" />&nbsp;
                 </td>--%>
                 <td width="18%" align="left">
                    <asp:Label ID="Label12" runat="server" Text='Costo' CssClass="testo_bold" />&nbsp;
                 </td>
                 <td>
                    <asp:Label ID="Label21" runat="server" Text='Omaggio' CssClass="testo_bold" />
                 </td>
              </tr>
              <tr runat="server" id="riga_elementi0">
                 <td width="6%" align="left">
                     <asp:CheckBox ID="chkOldScegli" runat="server" Visible="false" Checked='<%# Eval("selezionato") %>' />
                     <asp:CheckBox ID="chkScegli" runat="server" Visible="false" Checked='<%# Eval("selezionato") %>' Enabled="false" />
                     <asp:Label ID="lblIncluso" runat="server" Text="I " ToolTip="Incluso" CssClass="testo_bold" Visible="false"  />
                     <asp:Label ID="lblObbligatorio" runat="server" Text="O " ToolTip="Obbligatorio" CssClass="testo_bold" Visible="false"  />
                     <asp:Label ID="lblInformativa" runat="server" Text="IN " ToolTip="Informativa" CssClass="testo_bold" Visible="false"  />
                 </td>
                 <td width="2%">
                     <asp:Label ID="lblPrepagato" runat="server" Text="P" ToolTip="Informativa" CssClass="testo_bold" Visible="false"  />&nbsp;
                 </td>
                 <td width="48%" align="left">
                      <asp:Label ID="id_elemento" runat="server" Text='<%# Eval("id_elemento") %>' visible="false" />
                      <asp:Label ID="nome_costo" runat="server" Text='<%# Eval("nome_costo") %>' CssClass="testo" />:&nbsp;&nbsp;
                 </td>
                 <td width="18%" align="left">
                      <asp:Label ID="valore_costoLabel" runat="server" Text='<%# FormatNumber(Eval("valore_costo"),2) %>' CssClass="testo" Visible="false" />&nbsp;
                      <asp:Label ID="lblSconto" runat="server" Text='<%# FormatNumber(Eval("sconto"),2) %>' visible='false' CssClass="testo" />
                      <asp:Label ID="id_a_carico_di" runat="server" Text='<%# Eval("id_a_carico_di") %>' Visible="false" />
                      <asp:Label ID="obbligatorio" runat="server" Text='<%# Eval("obbligatorio") %>' Visible="false" /> 
                      <asp:Label ID="id_metodo_stampa" runat="server" Text='<%# Eval("id_metodo_stampa") %>' Visible="false" />                    
                 </td>
                 <%--<td width="18%" align="left">
                     <asp:Label ID="lblSconto" runat="server" Text='<%# FormatNumber(Eval("sconto"),2) %>' CssClass="testo" />&nbsp;
                 </td>--%>
                 <td width="18%" align="left">
                    
                    <asp:Label ID="costo_scontato" runat="server" Text='<%# FormatNumber(FormatNumber(Eval("valore_costo"),2) - FormatNumber(Eval("sconto"),2),2) %>' CssClass="testo" />&nbsp;
                
                 </td>
                 <td align="center">
                     <asp:Label ID="omaggiabile" runat="server" Text='<%# Eval("omaggiabile") %>' Visible="false" />
                     <asp:Label ID="prepagato" runat="server" Text='<%# Eval("prepagato") %>' Visible="false" />
                     <asp:CheckBox ID="chkOldOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />
                     <asp:CheckBox ID="chkOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />   
                 </td>
              </tr>
          </ItemTemplate>
      </asp:DataList>
  
  </td>
  <td align="center" valign="top" >
  
      &nbsp;</td>
</tr>
</table>


<div runat="server" id="tab_dettagli_pagamento" visible="true">
<table style="border:4px solid #444;" border="0" cellspacing="1" cellpadding="1" width="1024px" >
  <tr>
     <td align="left" style="color: #FFFFFF;background-color:#444;" colspan="2">
         <asp:Label ID="Label17" runat="server" Text="Dettagli di pagamento" CssClass="testo_titolo"></asp:Label>
     </td>
  </tr>
  <tr runat="server" id="riga_pagamento_pos" visible="false">
    <td colspan="2">
       <table style="border-top:0px;" border="0" cellspacing="1" cellpadding="1" width="100%">
         <tr>
           <td class="style45">
             <asp:Label ID="funzione" runat="server" Text="Funzione" CssClass="testo_bold" />
           </td>
           <td class="style34">
             <asp:Label ID="Label25" runat="server" Text="Staz." CssClass="testo_bold" />
           </td>
           <td class="style5">
             <asp:Label ID="Label65" runat="server" Text="Operatore" CssClass="testo_bold" />
           </td>
           <td class="style38">
             <asp:Label ID="Label34" runat="server" Text="Cassa" CssClass="testo_bold" />
           </td>
           <td class="style36">
             <asp:Label ID="Label35" runat="server" Text="Carta" CssClass="testo_bold" />
           </td>
           <td class="style37">
             <asp:Label ID="Label36" runat="server" Text="Intestatario" CssClass="testo_bold" />
           </td>
           <td class="style39">
             <asp:Label ID="Label37" runat="server" Text="Scadenza" CssClass="testo_bold" />
           </td>
           <td>
             <asp:Label ID="Label67" runat="server" Text="Data Operazione" CssClass="testo_bold" />
             </td>
         </tr>
         <tr>
           <td class="style45">
      
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
                 <asp:TextBox ID="txtPOS_DataOperazione" runat="server" Width="126px" ReadOnly="false"></asp:TextBox>
             </td>
         </tr>
         <tr>
           <td class="style45">
             <asp:Label ID="Label66" runat="server" Text="Terminal ID." CssClass="testo_bold" />
             
      
           </td>
           <td class="style34" >
      
               &nbsp;</td>
             <td class="style5">      
                <asp:Label ID="Label69" runat="server" Text="Nr. Preaut." CssClass="testo_bold" />      
             </td>
           <td class="style38">
      
               &nbsp;</td>
           <td class="style36">
                <asp:Label ID="Label70" runat="server" Text="Scadenza Preaut." CssClass="testo_bold" />
             </td>
           <td class="style37">
                <asp:Label ID="Label74" runat="server" Text="Stato" CssClass="testo_bold" />
           </td>
           <td class="style39">
               <asp:Label ID="Label46" runat="server" Text="Importo" CssClass="testo_bold" /></td>
           <td>
               &nbsp;
             </td>
         </tr>
         <tr>
           <td class="style45">
                 <asp:TextBox ID="txtPOS_TerminalID" runat="server" Width="140px" ReadOnly="true"></asp:TextBox>
                 
      
           </td>
           <td class="style34">
      
                 &nbsp;</td>
           <td class="style5">
      
                 <asp:TextBox ID="txtPOS_NrPreaut" runat="server" Width="158px" ReadOnly="true"></asp:TextBox>
      
             </td>
           <td class="style38">
      
                 &nbsp;</td>
           <td class="style36">
                <asp:TextBox ID="txtPOS_ScadenzaPreaut" runat="server" Width="88px" 
                     ReadOnly="true"></asp:TextBox>
             </td>
           <td class="style37">
                    <asp:TextBox ID="txtPOS_Stato" runat="server" Width="61px" 
                     ReadOnly="true"></asp:TextBox>
               
             </td>
           <td class="style39">
                 <asp:Label ID="idPagamentoExtra" runat="server" Text="" Visible="false"></asp:Label>

               <asp:TextBox ID="txt_importo" runat="server" Width="74px" 
                     ReadOnly="true"></asp:TextBox>

           </td>
           <td>
                &nbsp;
             </td>
         </tr>
         <tr runat="server" visible="true">
           <td class="style45">
      
             <asp:Label ID="Label131" runat="server" Text="Note" CssClass="testo_bold" />
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
         <tr>
           <td class="style40" colspan="8">
      
                 <asp:TextBox ID="txtPOS_Note" runat="server" Width="988px" 
                     ReadOnly="true"></asp:TextBox>
      
             </td>
         </tr>
         <tr>
           <td class="style33" colspan="8" align="center">
      
               <asp:Button ID="btnAnnulla7" runat="server" Text="Chiudi" style="background-color:#444;"/>

               <asp:Button ID="btnModificaDataPagamento" runat="server" Text="Modifica" OnClientClick="return preventMultipleSubmissions(this, event, 1);"  />   

               
               <asp:Button ID="btnEliminaPagamento" runat="server" Text="Elimina Pagamento" 
                   OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler eliminare il pagamento? Questa operazione dovrebbe essere effettuata solamente su prenotazioni da annullare o prenotazioni ripristinate allo stato originario.'));"  />
               <asp:Button ID="btnVisualizzaCC" runat="server" Text="Visualizza Numero Carta" />
           &nbsp;<asp:Label ID="lblPasswordCC" runat="server" Text="PASSWORD: " CssClass="testo_bold" />
      
                 <asp:TextBox ID="txtPasswordCC" runat="server" Width="105px" 
                     ReadOnly="false" TextMode="Password"></asp:TextBox>
           </td>
         </tr>
         <tr>
           <td colspan="8" align="center">
              <asp:Button ID="btnIncassoWeb" runat="server" Text="Incasso Web" OnClientClick="javascript: return(window.confirm ('Attenzione: sei sicuro di voler trasformare il pagamento in Pagamento Web? Operazione non reversibile.'));"  /> &nbsp;
              <asp:Button ID="btnIncassoBroker" runat="server" Text="Incasso Broker" OnClientClick="javascript: return(window.confirm ('Attenzione: sei sicuro di voler associare il pagamento al broker?'));"  /> &nbsp;
              <asp:Button ID="btnAzzeraPagamento" runat="server" Text="Azzera Pagamento" OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler azzerare il pagamento? Questa operazione dovrebbe essere effettuata solamente su prenotazioni da annullare o prenotazioni ripristinate allo stato originario.'));" />
           </td>
        </tr>
       </table>
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
                       <asp:Label ID="Label39" runat="server" Text='<%# Eval("ID_ModPag") %>' Visible="false" />
                       <asp:Label ID="ID_TIPPAG" runat="server" Text='<%# Eval("ID_TIPPAG") %>' Visible="false" />
                          <asp:Label ID="lb_Des_ID_ModPag" runat="server" Text='<%# Eval("Des_ID_ModPag") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                        <asp:Label ID="ID_CTRLabel" runat="server" Text='<%# Eval("ID_CTR") %>' Visible="false" />
                        <asp:Label ID="id_pos_funzioni_ares" runat="server" Text='<%# Eval("id_pos_funzioni_ares") %>' Visible="false" />
                        <asp:Label ID="funzione" runat="server" Text='<%# Eval("funzione") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                       <asp:Label ID="pagamento_broker" runat="server" Text='<%# Eval("pagamento_broker") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                       <asp:Label ID="lblStato" runat="server" Visible="true" />
                       <asp:Label ID="preaut_aperta" runat="server" Text='<%# Eval("preaut_aperta") %>' Visible="false" />
                       <asp:Label ID="operazione_stornata" runat="server" Text='<%# Eval("operazione_stornata") %>' Visible="false" />
                    </td>
                    <td align="left">
                        <asp:Label ID="DATA_OPERAZIONELabel" runat="server" Text='<%# Eval("DATA_OPERAZIONE") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                        <asp:Label ID="PER_IMPORTOLabel" runat="server" Text='<%# Eval("PER_IMPORTO") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                        <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi"/>
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
                       <asp:Label ID="pagamento_broker" runat="server" Text='<%# Eval("pagamento_broker") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                       <asp:Label ID="lblStato" runat="server" Visible="true" />
                       <asp:Label ID="preaut_aperta" runat="server" Text='<%# Eval("preaut_aperta") %>' Visible="false" />
                       <asp:Label ID="operazione_stornata" runat="server" Text='<%# Eval("operazione_stornata") %>' Visible="false" />
                    </td>
                    <td align="left">
                        <asp:Label ID="DATA_OPERAZIONELabel" runat="server" Text='<%# Eval("DATA_OPERAZIONE") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                        <asp:Label ID="PER_IMPORTOLabel" runat="server" Text='<%# Eval("PER_IMPORTO") %>' CssClass="testo" />
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
                                    <th id="Th22" runat="server" align="left">
                                        <asp:Label ID="Label68" runat="server" Text="A Carico" CssClass="testo_titolo"></asp:Label>
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
                </table>
            </LayoutTemplate>
            
        </asp:ListView>
    
    </td>
  </tr>
  <tr>
    <td class="style41" align="right">
    
             <asp:Label ID="Label76" runat="server" Text="Tot. Preaut." 
            CssClass="testo_bold" />
    
    &nbsp;<asp:TextBox ID="txtPOS_TotPreaut" runat="server" Width="74px" 
                     ReadOnly="true"></asp:TextBox>
      
    </td>
    <td align="right">
             <asp:Label ID="Label75" runat="server" Text="Tot. Incassato" CssClass="testo_bold" />
      
                 &nbsp;<asp:TextBox ID="txtPOS_TotIncassato" runat="server" Width="74px" 
                     ReadOnly="true"></asp:TextBox>
      
    </td>
  </tr>
</table>
</div>

<div runat="server" id="div_allegati" visible="true">
 <table style="border:4px solid #444;" border="0" cellspacing="1" cellpadding="1" width="1024px" >
   <tr>
     <td align="left" style="color: #FFFFFF;background-color:#444;" colspan="3">
         <asp:Label ID="Label41" runat="server" Text="Allegati" CssClass="testo_titolo"></asp:Label>
     </td>
  </tr>
  <tr>
    <td class="style59">
       <asp:Label ID="Label42" runat="server" Text="Tipo Allegato:" CssClass="testo_bold" />
    </td>
    <td class="style60">
      <asp:DropDownList ID="dropNuovoAllegato" runat="server" DataSourceID="sqlTipoAllegati" DataTextField="descrizione" DataValueField="id_cnt_pren_allegati_tipo" AppendDataBoundItems="True">
            <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
        </asp:DropDownList>
    </td>
    <td>
      <asp:FileUpload ID="UploadAllegati" runat="server" style="float:left;"  />
      <%--<asp:ImageButton ID="upload" runat="server" CommandName="upload" ImageUrl="images\add_file.png" Width="20px" style="float:right;position:absolute;margin-left:3px;" />--%>

        &nbsp;<asp:Button ID="upload" runat="server" Text="Salva" OnClientClick="return preventMultipleSubmissions(this, event, 1);" style="float:right;position:absolute;margin-left:3px;background-color:#e88532;" />

    </td>
  </tr>
  <tr>
     <td align="center" colspan="3">
      <br />&nbsp;
       <asp:ListView ID="dataListAllegati" runat="server" DataSourceID="sqlAllegati" EnableModelValidation="True" DataKeyNames="id_allegato" Visible="false">
                    <AlternatingItemTemplate>
                         <tr style="background-color:#f1f1ee; color: #000000;">
                          <td align="center">
                             <asp:Label ID="id_allegato" runat="server" Text='<%# Eval("id_allegato") %>' Visible="false"></asp:Label>
                              <asp:Label ID="Label41" runat="server" Text='<%# Eval("descrizione") %>' CssClass="testo"></asp:Label>
                              <asp:Label ID="cartella" runat="server" Text='<%# Eval("cartella") %>' Visible="false"></asp:Label>
                                <asp:Label ID="nome_file" runat="server" Text='<%# Eval("nome_file") %>' Visible="false"></asp:Label>
                          </td>
                          <td>
                              <asp:HyperLink ID="linkFile" runat="server" Text='<%# Eval("nome_file") %>' Target="_blank" href='<%# Eval("my_path") %>' Font-Bold="true"></asp:HyperLink>&nbsp;&nbsp;&nbsp;
                          </td>
                          <td align="center">
                              <asp:ImageButton ID="elimina" runat="server" CommandName="elimina" ImageUrl="images\elimina.png" Width="20px" OnClientClick="return confirm('Sei sicuro di voler eliminare questo allegato?.');" />
                          </td>
                         </tr>
                        </AlternatingItemTemplate>
                        <EmptyDataTemplate>
                            <table id="Table3" runat="server" 
                              class="table table-bordered">
                                <tr>
                                    <td>
                                      Nessun Allegato
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
               
                        <ItemTemplate>
                          <tr style="background-color:#f1f1ee; color: #000000;">
                              <td align="center">
                                <asp:Label ID="id_allegato" runat="server" Text='<%# Eval("id_allegato") %>' Visible="false"></asp:Label>
                               <asp:Label ID="Label41" runat="server" Text='<%# Eval("descrizione") %>' CssClass="testo"></asp:Label>
                               <asp:Label ID="cartella" runat="server" Text='<%# Eval("cartella") %>' Visible="false"></asp:Label>
                                <asp:Label ID="nome_file" runat="server" Text='<%# Eval("nome_file") %>' Visible="false"></asp:Label>
                              </td>
                              <td>
                                  <asp:HyperLink ID="linkFile" runat="server" Text='<%# Eval("nome_file") %>' Target="_blank" href='<%# Eval("my_path") %>' Font-Bold="true"></asp:HyperLink>&nbsp;&nbsp;&nbsp;
                              </td>
                              <td align="center">
                                 <asp:ImageButton ID="elimina" runat="server" CommandName="elimina" ImageUrl="images\elimina.png" Width="20px" OnClientClick="return confirm('Sei sicuro di voler eliminare questo allegato?.');" />
                              </td>
                          </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table id="Table4" runat="server" class="table table-bordered">
                                <tr id="Tr3" runat="server">
                                    <td id="Td2" runat="server">
                                        <table ID="itemPlaceholderContainer" runat="server" >
                                            <tr id="Tr4" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                              
                                                    <th id="Th2" runat="server" ><asp:Label ID="Label16" runat="server" Text='Tipo Allegato' CssClass="testo_titolo"></asp:Label></th>
                                                    <th id="Th3" runat="server" ><asp:Label ID="Label17" runat="server" Text='Allegato' CssClass="testo_titolo"></asp:Label></th>
                                                    <th width="80px"></th>
                                            </tr>
                                            <tr ID="itemPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            
                            </table>
                        </LayoutTemplate>
                
            </asp:ListView>

        

        <%--inizio nuovo ListView per allegati con selezione multipla 12.04.2022 --%>

        <asp:ListView ID="ListViewAllegati" runat="server" DataSourceID="sqlAllegati" DataKeyNames="Id_allegato" >
            <ItemTemplate>
                <tr style="background-color:#FFFFFF; color: #000000; ">
                    <td><asp:Label ID="lblIdAllegato" runat="server" Text='<%# Eval("Id_allegato") %>'></asp:Label></td>
                    <td><asp:Label ID="lblTipo" runat="server" Text='<%# Eval("descrizione") %>'></asp:Label></td>
                    <td><asp:Label ID="lblNomeFile" runat="server" Text='<%# Eval("Nome_File") %>'></asp:Label></td>
                    <td><asp:Label ID="lblPercorsoFile" runat="server" Text='<%# Eval("my_path") %>'></asp:Label></td>
                    
                    <td align="center"><asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezionaAllegato"/></td>
                    <td align="center"><asp:ImageButton ID="EliminaAllegato" OnClientClick="return preventMultipleSubmissions(this, event, 1);"  runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="EliminaAllegato"/></td>
                    <%--
                    <td align="center"><asp:CheckBox ID="chkAllegatoEmail" runat="server" Text="" Width="15px"/></td>--%>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="background-color:#FFFFFF; color: #000000;">
                    <td><asp:Label ID="lblIdAllegato" runat="server" Text='<%# Eval("Id_allegato") %>'></asp:Label></td>
                    <td><asp:Label ID="lblTipo" runat="server" Text='<%# Eval("descrizione") %>'></asp:Label></td>
                    <td><asp:Label ID="lblNomeFile" runat="server" Text='<%# Eval("Nome_File") %>'></asp:Label></td>
                    <td><asp:Label ID="lblPercorsoFile" runat="server" Text='<%# Eval("my_path") %>'></asp:Label></td>
                    
                <td align="center"><asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezionaAllegato"/></td>
                    <td align="center"><asp:ImageButton ID="EliminaAllegato" OnClientClick="return preventMultipleSubmissions(this, event, 1);" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="EliminaAllegato"/></td>
                     <%--   
                    <td align="center"><asp:CheckBox ID="chkAllegatoEmail" runat="server" Text="" Width="15px"/></td>--%>
                </tr>
            </AlternatingItemTemplate>
            <EmptyDataTemplate>
                <table id="Table1" runat="server" style="">
                    <tr>
                        <td>Non vi sono allegati collegati contratto. </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table id="Table2" runat="server" width="100%" style="font-family:Verdana;font-size:12px;">
                    <tr id="Tr1" runat="server" >
                        <td id="Td1" runat="server">
                            <table ID="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                <tr id="Tr3" runat="server" style="color: #FFFFFF;background-color:#000000;" >
                                    <th id="Th1" runat="server">Id</th>
                                    <th id="Th2" runat="server" style="width:150px;">Tipo</th>
                                    <th id="Th3" runat="server">Nome File</th>
                                    <th id="Th4" runat="server">Percorso File</th>
                                    <th id="Th6" runat="server"></th>
                                   <th id="Th5" runat="server"></th>
                                     <%--
                                    <th id="Th7" runat="server"></th>--%>
                                </tr>
                                <tr ID="itemPlaceholder" runat="server"></tr>
                            </table>
                        </td>
                    </tr>
                    
                </table>
            </LayoutTemplate>
            <SelectedItemTemplate>
                <tr style="">
                    <td><asp:Label ID="lblIdAllegato" runat="server" Width="20px"  Text='<%# Eval("Id_allegato") %>'></asp:Label></td>
                    <td><asp:Label ID="lblTipo" runat="server" Width="40px" Text='<%# Eval("descrizione") %>'></asp:Label></td>
                    <td><asp:Label ID="lblNomeFile" runat="server" Width="150px" Text='<%# Eval("Nome_File") %>'></asp:Label></td>
                    <td><asp:Label ID="lblPercorsoFile" runat="server" Width="180px" Font-Size="X-Small" Text='<%# Eval("my_path") %>'></asp:Label></td>
                    
                    <td align="center"><asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezionaAllegato"/></td>
                    <td align="center"><asp:ImageButton ID="EliminaAllegato" runat="server" OnClientClick="return preventMultipleSubmissions(this, event, 1);" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="EliminaAllegato"/></td>
                    <%--
                    <td align="center"><asp:CheckBox ID="chkAllegatoEmail" runat="server" Text="" Width="15px"/></td>--%>
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>

        <div style="margin-top:10px;font-family:Verdana;font-size:12px;">
            <asp:Label ID="lbl_allegati_inviati" runat="server" Text=""></asp:Label>
        </div>
         
        



     </td>
  </tr>
 </table>


      
         

</div>



</div>
<div runat="server" id="tab_pagamento" visible="false" >
     <uc1:scambio_importo ID="Scambio_Importo1" runat="server"  />
</div>

&nbsp;<asp:Label ID="idPrenotazione" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="numCalcolo" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="statoPrenotazione" runat="server" Visible="false"></asp:Label>
      
      <asp:Label ID="prenotazioneScaduta" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="tipo_prenotazione" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="old_ultimo_gruppo" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="ultimo_gruppo" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="id_gruppo_auto_scelto" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="livello_accesso_sconto" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="livello_accesso_omaggi" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="livello_accesso_dettaglio_pos" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="livello_accesso_modifica_broker" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="scegli_attivo" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="provenienza" runat="server" Visible="false"></asp:Label>

      <asp:Label ID="tariffa_broker" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="id_tariffa_broker" runat="server" Visible="false"></asp:Label>
      
      <asp:Label ID="livello_accesso_broker" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="livello_accesso_annulla_ripristina" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="livello_accesso_eliminare_pagamenti" runat="server" Text='' Visible="false"></asp:Label>
      
      <asp:Label ID="lb_anno_fattura" runat="server" Text="" Visible="False"></asp:Label>
      <asp:Label ID="lb_codice_fattura" runat="server" Text="" Visible="False"></asp:Label>
      <asp:Label ID="lb_tipo_fattura" runat="server" Text="" Visible="False"></asp:Label>
      <asp:Label ID="lb_id_ditta_fattura" runat="server" Text="" Visible="False"></asp:Label>
      <asp:Label ID="a_carico_del_broker" runat="server" Text="" Visible="False"></asp:Label>
      <asp:Label ID="a_carico_del_broker_ultimo_calcolo" runat="server" Text="" Visible="False"></asp:Label>
      <asp:Label ID="lblAvvisoBlackList" runat="server" Visible="false"></asp:Label>    
      
      <asp:Label ID="prenotazione_no_tariffa" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="lbl_tariffa_broker_salvata" runat="server" Visible="false"></asp:Label>
      
      <asp:Label ID="complimentary" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="full_credit" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="ricalcolaPrepagato" runat="server" Visible="false" Text=""></asp:Label>
      
&nbsp;<asp:SqlDataSource ID="sqlStazioni" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT stazioni.id, (codice + ' ' + nome_stazione) As stazione FROM stazioni WITH(NOLOCK) ORDER BY nome_stazione"></asp:SqlDataSource>

    

    <asp:SqlDataSource ID="sqlTipoClienti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT LEFT(descrizione,30) As descrizione, [id] FROM clienti_tipologia WITH(NOLOCK) ORDER BY [descrizione]">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlTipoAllegati" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id_cnt_pren_allegati_tipo, descrizione FROM contratti_prenotazioni_allegati_tipo WITH(NOLOCK) ORDER BY descrizione">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlAllegati" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id_allegato, nome_file, cartella, (cartella + nome_file) as my_path, descrizione FROM contratti_prenotazioni_allegati WITH(NOLOCK) 
        INNER JOIN contratti_prenotazioni_allegati_tipo WITH(NOLOCK) ON contratti_prenotazioni_allegati.id_cnt_pren_allegati_tipo=contratti_prenotazioni_allegati_tipo.id_cnt_pren_allegati_tipo WHERE num_pren='0'">
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlWarningPickPrenotazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT warning, tipo FROM prenotazioni_warning WITH(NOLOCK) WHERE (([id_documento] = @id_prenotazione) AND ([num_calcolo] = @num_calcolo) And ((tipo='PICK') OR (tipo='PICK INFO'))) ORDER BY [warning]">
        <SelectParameters>
            <asp:ControlParameter ControlID="idPrenotazione" Name="id_prenotazione" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlWarningDropPrenotazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT warning, tipo FROM prenotazioni_warning WITH(NOLOCK) WHERE (([id_documento] = @id_prenotazione) AND ([num_calcolo] = @num_calcolo) And ((tipo='DROP') OR (tipo='DROP INFO'))) ORDER BY [warning]">
        <SelectParameters>
            <asp:ControlParameter ControlID="idPrenotazione" Name="id_prenotazione" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlWarningDropRibaltamento" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT warning, tipo FROM prenotazioni_warning WITH(NOLOCK) WHERE (([id_documento] = @id_prenotazione) AND ([num_calcolo] = @num_calcolo) And (tipo='RIBALTAMENTO')) ORDER BY [warning]">
        <SelectParameters>
            <asp:ControlParameter ControlID="idPrenotazione" Name="id_prenotazione" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlWarningGruppi" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT warning, tipo FROM prenotazioni_warning WITH(NOLOCK) WHERE (([id_documento] = @id_prenotazione) AND ([num_calcolo] = @num_calcolo) And ((tipo='GRUPPO'))) ORDER BY [warning]">
        <SelectParameters>
            <asp:ControlParameter ControlID="idPrenotazione" Name="id_prenotazione" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlGruppiAuto" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT gruppi.id_gruppo, gruppi.cod_gruppo FROM gruppi WITH(NOLOCK) WHERE attivo='1' ORDER BY cod_gruppo"></asp:SqlDataSource>
        
    <asp:SqlDataSource ID="sqlElementiExtra" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT condizioni_elementi.id, condizioni_elementi.descrizione FROM condizioni_elementi WITH(NOLOCK) WHERE id='0' order by descrizione"></asp:SqlDataSource>
        
    <asp:SqlDataSource ID="sqlTariffeGeneriche" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, codice FROM tariffe WITH(NOLOCK) WHERE id='0'"></asp:SqlDataSource>
        
    <asp:SqlDataSource ID="sqlTariffeParticolari" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, codice FROM tariffe WITH(NOLOCK) WHERE id='0'"></asp:SqlDataSource>
        
    <asp:SqlDataSource ID="sqlMotivoAnnullamento" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM prenotazioni_motivo_annullamento WITH(NOLOCK) ORDER BY id"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlPrenotazioniCosti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT prenotazioni_costi.id, gruppi.id_gruppo, gruppi.cod_gruppo As gruppo, prenotazioni_costi.id_elemento, prenotazioni_costi.nome_costo, condizioni_elementi.descrizione_lunga, condizioni_elementi.tipologia_franchigia, condizioni_elementi.sottotipologia_franchigia, ISNULL((imponibile+iva_imponibile+ISNULL(imponibile_onere,0)+ISNULL(iva_onere,0)) - (ISNULL(imponibile_scontato_prepagato,0)+ISNULL(iva_imponibile_scontato_prepagato,0)+ISNULL(imponibile_onere_prepagato,0)+ISNULL(iva_onere_prepagato,0)),0) As valore_costo, (ISNULL(imponibile_scontato_prepagato,0)+ISNULL(iva_imponibile_scontato_prepagato,0)+ISNULL(imponibile_onere_prepagato,0)+ISNULL(iva_onere_prepagato,0)) As valore_prepagato ,ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, ISNULL(commissioni_imponibile_originale,0)+ISNULL(commissioni_iva_originale,0) As commissioni, id_a_carico_di, prenotazioni_costi.obbligatorio, prenotazioni_costi.id_metodo_stampa, ISNULL(prenotazioni_costi.selezionato,'False') As selezionato, ISNULL(prenotazioni_costi.omaggiato,'False') As omaggiato, (CASE WHEN prepagato='1' THEN 'False' ELSE ISNULL(prenotazioni_costi.omaggiabile,'False') END) As omaggiabile, prepagato, condizioni_elementi.is_gps, prenotazioni_costi.num_elemento  FROM prenotazioni_costi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON prenotazioni_costi.id_gruppo=gruppi.id_gruppo LEFT JOIN condizioni_elementi WITH(NOLOCK) ON prenotazioni_costi.id_elemento=condizioni_elementi.id WHERE ((id_documento = @id_prenotazione) AND (num_calcolo = @num_calcolo_prenotazione)) AND ordine_stampa<>'5' AND ISNULL(franchigia_attiva,'1')='1' AND (NOT (prenotazioni_costi.ordine_stampa='7' AND prenotazioni_costi.franchigia_attiva IS NULL) OR condizioni_elementi.tipologia='KM_EXTRA') AND ISNULL(condizioni_elementi.tipologia,'')<>'RIMUOVI_RIFORNIMENTO' AND NOT (NOT valore_percentuale IS NULL AND ordine_stampa<>'2') ORDER BY id_gruppo,ordine_stampa,ISNULL(condizioni_elementi.ordinamento_vis,0) DESC, nome_costo ">
        <SelectParameters>
            <asp:ControlParameter ControlID="idPrenotazione" Name="id_prenotazione" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo_prenotazione" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlFontiCommissionabili" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT rag_soc, id  FROM fonti_commissionabili WITH(NOLOCK) WHERE attiva='1' ORDER BY rag_soc"></asp:SqlDataSource>
   
    <asp:SqlDataSource ID="sqlUltimoPrenotazioneCosti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT  gruppi.id_gruppo, gruppi.cod_gruppo As gruppo,  prenotazioni_costi.id_elemento,  prenotazioni_costi.nome_costo, ISNULL((imponibile+iva_imponibile+ISNULL(imponibile_onere,0)+ISNULL(iva_onere,0)) - (ISNULL(imponibile_scontato_prepagato,0)+ISNULL(iva_imponibile_scontato_prepagato,0)+ISNULL(imponibile_onere_prepagato,0)+ISNULL(iva_onere_prepagato,0)),0) As valore_costo, (ISNULL(imponibile_scontato_prepagato,0)+ISNULL(iva_imponibile_scontato_prepagato,0)+ISNULL(imponibile_onere_prepagato,0)+ISNULL(iva_onere_prepagato,0)) As valore_prepagato, ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, ISNULL(commissioni_imponibile_originale,0)+ISNULL(commissioni_iva_originale,0) As commissioni, prenotazioni_costi.id_a_carico_di,  prenotazioni_costi.obbligatorio,  prenotazioni_costi.id_metodo_stampa, ISNULL( prenotazioni_costi.selezionato,'False') As selezionato, ISNULL( prenotazioni_costi.omaggiato,'False') As omaggiato, (CASE WHEN prepagato='1' THEN 'False' ELSE ISNULL(prenotazioni_costi.omaggiabile,'False') END) As omaggiabile, prepagato FROM prenotazioni_costi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON prenotazioni_costi.id_gruppo=gruppi.id_gruppo LEFT JOIN condizioni_elementi WITH(NOLOCK) ON prenotazioni_costi.id_elemento=condizioni_elementi.id WHERE ((id_documento = @id_prenotazione) AND (num_calcolo = @num_calcolo_prenotazione-1)) AND ordine_stampa<>'5' AND ISNULL(franchigia_attiva,'1')='1' AND (NOT (prenotazioni_costi.ordine_stampa='7' AND prenotazioni_costi.franchigia_attiva IS NULL) OR condizioni_elementi.tipologia='KM_EXTRA') AND ISNULL(condizioni_elementi.tipologia,'')<>'RIMUOVI_RIFORNIMENTO' AND NOT (NOT valore_percentuale IS NULL AND ordine_stampa<>'2') ORDER BY id_gruppo,ordine_stampa, ISNULL(condizioni_elementi.ordinamento_vis,0) DESC, nome_costo ">
        <SelectParameters>
            <asp:ControlParameter ControlID="idPrenotazione" Name="id_prenotazione" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo_prenotazione" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    
    <asp:SqlDataSource ID="sqlDettagliPagamento" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        
        SelectCommand="SELECT MOD_PAG.ID_ModPag, MOD_PAG.Descrizione Des_ID_ModPag, ID_TIPPAG, POS_Funzioni.funzione,pagamento_broker, PAGAMENTI_EXTRA.id_pos_funzioni_ares, PAGAMENTI_EXTRA.DATA_OPERAZIONE, PAGAMENTI_EXTRA.PER_IMPORTO, PAGAMENTI_EXTRA.ID_CTR, PAGAMENTI_EXTRA.preaut_aperta, PAGAMENTI_EXTRA.operazione_stornata, (CASE WHEN N_CONTRATTO_RIF IS NULL THEN 'PREN ' ELSE 'RA ' END) AS provenienza FROM PAGAMENTI_EXTRA WITH(NOLOCK) INNER JOIN POS_Funzioni WITH(NOLOCK) ON PAGAMENTI_EXTRA.id_pos_funzioni_ares=POS_funzioni.id LEFT JOIN MOD_PAG WITH(NOLOCK) ON pagamenti_extra.ID_ModPag = MOD_PAG.ID_ModPag WHERE (N_PREN_RIF = @N_PREN_RIF)">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblNumPrenotazione" Name="N_PREN_RIF" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                               ControlToValidate="txtDaData" ErrorMessage="Specificare la data iniziale di pick up." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="cerca"> </asp:RequiredFieldValidator>
                            
                            <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                ControlToValidate="txtDaData" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per la data iniziale di pick up'." Type="Date" ValidationGroup="cerca"> </asp:CompareValidator>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                               ControlToValidate="txtAData" ErrorMessage="Specificare la data finale di pick up." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="cerca"> </asp:RequiredFieldValidator>
                            
                            <asp:CompareValidator ID="CompareValidator3" runat="server" 
                                ControlToValidate="txtAData" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per la data finale di pick up'." Type="Date" ValidationGroup="cerca"> </asp:CompareValidator>
                            
                            <%--<asp:CompareValidator id="CompareValidator4" runat="server"
                                 ControlToValidate="txtAData"
                                 ControlToCompare="txtDaData"
                                 Type= "Date"
                                 Operator = "GreaterThanEqual"
                                 ErrorMessage = "Attenzione : la data finale di pick up è precedente alla data iniziale."
                                 ValidationGroup="cerca"
                                 Font-Size="0pt"> </asp:CompareValidator>
                                 --%>
                                 
                                 
    <asp:CompareValidator ID="CompareValidator4" runat="server" 
                    ControlToValidate="dropVariazioneACaricoDi" 
                    Operator="NotEqual"  Type="String" 
                    ValidationGroup="cerca" ValueToCompare="-1" Font-Size="0pt" ErrorMessage="Specificare se la variazione e' a carico del cliente o a carico del broker."> </asp:CompareValidator>
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
     <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender3"
          ControlToValidate="txtoraPartenza" Display="Dynamic" EmptyValueMessage="Specificare l'orario di pick-up." IsValidEmpty="false"
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


