<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="contratti_new.aspx.vb" Inherits="contratti_new" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<%@ Register Src="/tabelle_pos/scambio_importo.ascx" TagName="scambio_importo" TagPrefix="uc1" %>
<%@ Register Src="/anagrafica/anagrafica_conducenti.ascx" TagName="anagrafica_conducenti" TagPrefix="uc1" %>
<%@ Register Src="/anagrafica/anagrafica_ditte.ascx" TagName="anagrafica_ditte" TagPrefix="uc1" %>
<%@ Register Src="gestione_danni/gestione_checkin.ascx" TagName="gestione_checkin" TagPrefix="uc1" %>
<%@ Register Src="/gestione_danni/gestione_note.ascx" TagName="gestione_note" TagPrefix="uc1" %>

<%@ Register Assembly="BoxOver" Namespace="BoxOver" TagPrefix="boxover" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

     <script type="text/javascript" language="javascript" src="/lytebox.js"></script>
     <link rel="stylesheet" href="/lytebox.css" type="text/css" media="screen" />
     
     <meta http-equiv="Expires" content="0" />
     <meta http-equiv="Cache-Control" content="no-cache" />
     <meta http-equiv="Pragma" content="no-cache" />
    
     <script type="text/javascript" language="javascript">
         function onSelectedStartDate(sender, args) {
             $find("endDate").set_selectedDate(sender.get_selectedDate());
         }
     </script>
     <script type="text/javascript">
                 function filterInput(evt) {
                     var keyCode, Char, inputField;
                     var filter = '0123456789,';
                     if (window.event) {
                         keyCode = window.event.keyCode;
                         evt = window.event;
                     } else if (evt) keyCode = evt.which;
                     else return true;

                     inputField = evt.srcElement ? evt.srcElement : evt.target || evt.currentTarget;
                     if ((keyCode == null) || (keyCode == 0) || (keyCode == 8) || (keyCode == 9) || (keyCode == 13) || (keyCode == 27)) return true;
                     Char = String.fromCharCode(keyCode);
                     if (Char == '.') {
                         Char = ',';

                         if (window.event) {
                             window.event.keyCode = 44;
                         } else if (evt) evt.which = 44;
                     }

                     if ((filter.indexOf(Char) == -1)) return false;

                     var SelStart = inputField.selectionStart;
                     var SelEnd = inputField.selectionEnd;
                     var SelLenght = SelEnd - SelStart;

                     var stringaBase = inputField.value.substring(0, inputField.selectionStart);
                     stringaBase = stringaBase + inputField.value.substring(SelEnd, inputField.value.length);

                     if (Char == ',') {
                         if ((stringaBase.indexOf(Char) >= 0)) return false;
                     }

                 }
                            
                        </script>
     
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
         .style49
         {
             width: 4px;
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
         .style75
    {
             width: 232px;
         }
         </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="div_edit_danno" runat="server" visible="false">
       <uc1:gestione_checkin id="gestione_checkin" runat="server" />
    </div>   
    <div runat="server" id="tab_contratto">
    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" class="style1">
               <asp:Label ID="Label14" runat="server" Text="Contratti" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
   </table>
   <table border="0" cellspacing="2" cellpadding="2" width="1024px" style="border:4px solid #444">
      <tr>
        <td align="center" style="color: #FFFFFF;background-color:#444;" >
            <asp:Label ID="lblTipoDocumento" runat="server" Text="Contratto Numero:" CssClass="testo_titolo"></asp:Label>&nbsp;
            <asp:Label ID="lblNumContratto" runat="server"  CssClass="testo_titolo"></asp:Label>
            <asp:Label ID="lblCrv" runat="server"  CssClass="testo_titolo"></asp:Label>
            &nbsp;
            <asp:Label ID="lblComplimentary" runat="server" Text="COMPLIMENTARY"  CssClass="testo_titolo" ForeColor="Red" Visible="false"></asp:Label>
            &nbsp;
            <asp:Label ID="lblFullCredit" runat="server" Text="FULL CREDIT"  CssClass="testo_titolo" ForeColor="Red" Visible="false"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label15" runat="server" Text="Data Contratto:" CssClass="testo_titolo"></asp:Label>&nbsp;
            <asp:Label ID="lblDataContratto" runat="server"  CssClass="testo_titolo"></asp:Label>&nbsp;&nbsp;
            <asp:Label ID="lblDataAnnullamento" runat="server"  CssClass="testo_titolo"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblDaPrenotazione" runat="server" Text="Da Pren. Num.:" CssClass="testo_titolo" Visible="false"></asp:Label>&nbsp;
            <asp:LinkButton ID="lblNumPren" runat="server"  CssClass="testo_titolo" Visible="false" Text="-1"></asp:LinkButton>
        </td>
      </tr>
      <tr>
        <td align="center" style="color: #FFFFFF;background-color:#444;" >
            <asp:Label ID="lblTipoDocumento0" runat="server" Text="Operatore Creazione:" CssClass="testo_titolo"></asp:Label>
            <asp:Label ID="lblOperatoreCreazione" runat="server"  CssClass="testo_titolo"></asp:Label>&nbsp;&nbsp;
            <asp:Label ID="lblOperatoreAnnullamento" runat="server"  CssClass="testo_titolo"></asp:Label>
        </td>
      </tr>
  </table>
  <table border="0" cellspacing="2" cellpadding="2" width="1024px" style="border:4px solid #444;">
   <tr>
      <td valign="top" class="style11">
         <asp:Label ID="Label2" runat="server" Text="USCITA" CssClass="testo_bold"></asp:Label>
      </td>
      <td valign="top">
            <asp:DropDownList ID="dropStazionePickUp" runat="server" 
                AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                DataValueField="id">
               <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
            </asp:DropDownList>
      </td>
      <td valign="top">
        <asp:TextBox runat="server" Width="70px" ID="txtDaData"></asp:TextBox>
        <asp:TextBox runat="server" Width="70px" ID="txtDADataOld" Visible="false"></asp:TextBox>
          <ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" 
    Enabled="True" TargetControlID="txtDaData" ID="CalendarExtender2">
            </ajaxtoolkit:calendarextender>
          <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999" 
    MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" 
    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
    CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
    CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDaData" 
    ID="MaskedEditExtender1">
            </ajaxtoolkit:maskededitextender>
      </td>
      <td valign="top" class="style8">
              <asp:TextBox ID="txtoraPartenza" runat="server" Width="40px" ></asp:TextBox>
              <ajaxtoolkit:maskededitextender ID="MaskedEditExtender3" runat="server" 
                              TargetControlID="txtoraPartenza"
                              Mask="99:99"
                              MessageValidatorTip="true"
                              ClearMaskOnLostFocus="true"
                              OnFocusCssClass="MaskedEditFocus"
                              OnInvalidCssClass="MaskedEditError"
                              MaskType="Time"
                              CultureName="en-US">
                </ajaxtoolkit:maskededitextender>
              <asp:TextBox ID="ore1" runat="server" Width="29px" MaxLength="2" Visible="false"></asp:TextBox>
              <asp:TextBox ID="minuti1" runat="server" Width="29px" MaxLength="2" Visible="false"></asp:TextBox>
           
      </td>
      <td valign="top" class="style1">
     <asp:Label ID="Label1" runat="server" Text="RIENTRO" CssClass="testo_bold"></asp:Label>
  </td>
  <td valign="top" class="style7" colspan="3">
    <asp:DropDownList ID="dropStazioneDropOff" runat="server" 
        AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
        DataValueField="id">
        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>    
    </asp:DropDownList>
  </td>
  <td valign="top" class="style9">
    <asp:TextBox runat="server" Width="70px" ID="txtAData" ></asp:TextBox>
    <asp:TextBox runat="server" Width="70px" ID="txtADataOld" Visible="false" ></asp:TextBox>
      <ajaxtoolkit:calendarextender runat="server" Format="dd/MM/yyyy" 
    Enabled="True" TargetControlID="txtAData" ID="CalendarExtender3">
        </ajaxtoolkit:calendarextender>
      <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999" 
    MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" 
    CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
    CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
    CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtAData" 
    ID="MaskedEditExtender2">
        </ajaxtoolkit:maskededitextender>
  </td>
  <td valign="top">
     <asp:TextBox ID="txtOraRientro" runat="server" Width="40px"></asp:TextBox>
      <ajaxtoolkit:maskededitextender ID="MaskedEditExtender4" runat="server" 
                          TargetControlID="txtOraRientro"
                          Mask="99:99"
                          MessageValidatorTip="true"
                          ClearMaskOnLostFocus="true"
                          OnFocusCssClass="MaskedEditFocus"
                          OnInvalidCssClass="MaskedEditError"
                          MaskType="Time"
                          CultureName="en-US">
            </ajaxtoolkit:maskededitextender>
    <asp:TextBox ID="ore2" runat="server" Width="29px" MaxLength="2" Visible="false"></asp:TextBox>
    <asp:TextBox ID="minuti2" runat="server" Width="29px" MaxLength="2" Visible="false"></asp:TextBox>
  </td>
  </tr>
   <tr runat="server" id="riga_rientro" visible="false">
      <td valign="top" class="style11">
          &nbsp;</td>
      <td valign="top">
            &nbsp;</td>
      <td valign="top">
          &nbsp;</td>
      <td valign="top" class="style8">
              &nbsp;</td>
      <td valign="top" class="style1">
     <asp:Label ID="Label99" runat="server" Text="P.RIENTRO" CssClass="testo_bold"></asp:Label>
       </td>
  <td valign="top" class="style7" colspan="3">
    <asp:DropDownList ID="dropStazioneRientroPresunto" runat="server" Enabled="false"
        AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
        DataValueField="id">
        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>    
    </asp:DropDownList>
       </td>
  <td valign="top" class="style9">
    <asp:TextBox runat="server" Width="70px" ID="txtADataPresunto" Enabled="false" ></asp:TextBox>
      <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" 
          Enabled="True" TargetControlID="txtADataPresunto" ID="txtADataPresunto_CalendarExtender">
        </ajaxtoolkit:CalendarExtender>
        <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" 
          MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" 
          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtADataPresunto" 
          ID="txtADataPresunto_MaskedEditExtender">
        </ajaxtoolkit:MaskedEditExtender>--%>
       </td>
  <td valign="top">
     <asp:TextBox ID="txtOraRientroPresunta" runat="server" Width="40px" Enabled="false"></asp:TextBox>
      <%-- <ajaxToolkit:MaskedEditExtender ID="txtOraRientroPresunta_MaskedEditExtender" runat="server" 
                          TargetControlID="txtOraRientroPresunta"
                          Mask="99:99"
                          MessageValidatorTip="true"
                          ClearMaskOnLostFocus="true"
                          OnFocusCssClass="MaskedEditFocus"
                          OnInvalidCssClass="MaskedEditError"
                          MaskType="Time"
                          CultureName="en-US">
            </ajaxToolkit:MaskedEditExtender>--%>
    <asp:TextBox ID="ore2_presunto" runat="server" Width="29px" MaxLength="2" Visible="false"></asp:TextBox>
    <asp:TextBox ID="minuti2_presunto" runat="server" Width="29px" MaxLength="2" Visible="false"></asp:TextBox>
       </td>
  </tr>
   <tr>
      <td valign="top" class="style4">
           <asp:Label ID="Label3" runat="server" Text="Fonte" CssClass="testo_bold"></asp:Label>
      </td>
      <td valign="top" class="style5">
              <asp:DropDownList ID="dropTipoCliente" runat="server" 
                  AppendDataBoundItems="True" DataSourceID="sqlTipoClienti" 
                  DataTextField="descrizione" DataValueField="id">
                  <asp:ListItem Selected="True" Value="0">Generico...</asp:ListItem>
              </asp:DropDownList>
      </td>
      <td valign="top">
         <asp:Label ID="Label4" runat="server" Text="Gruppo" CssClass="testo_bold"></asp:Label>
      </td>
      <td valign="top" class="style13">
          <asp:DropDownList ID="gruppoDaCalcolare" runat="server" AppendDataBoundItems="True" 
              DataSourceID="sqlGruppiAuto" DataTextField="cod_gruppo" 
              DataValueField="id_gruppo">
              <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
          </asp:DropDownList>
       </td>
      
      <td>
         <asp:Label ID="Label5" runat="server" Text="Gruppo da consegnare" CssClass="testo_bold"></asp:Label>
      </td>
      <td>
          <asp:DropDownList ID="gruppoDaConsegnare" runat="server" AppendDataBoundItems="True" 
              DataSourceID="sqlGruppiAuto" DataTextField="cod_gruppo" 
              DataValueField="id_gruppo">
              <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
          </asp:DropDownList>
      </td>
      <td class="style14">
         <asp:Label ID="lblGiorniTO" runat="server" Text="Giorni T.O." CssClass="testo_bold"></asp:Label>
  
      
     <asp:Label ID="lblGiorniPrepagati" runat="server" Text="GG. Prepag." CssClass="testo_bold" Visible="false"></asp:Label>
  
      
      </td>
      <td>
         <asp:TextBox ID="txtNumeroGiorniTO" runat="server" Width="36px" visible="true" Enabled="false"></asp:TextBox>
              <asp:TextBox ID="txtGiorniPrepagati" runat="server" Width="36px" Enabled="false" Visible="false"></asp:TextBox>
  
      
      </td>
      <td class="style14">
         <asp:Label ID="Label16" runat="server" Text="Giorni" CssClass="testo_bold"></asp:Label>
  
      
      <asp:Label ID="lblGiorniToOld" runat="server" visible="false"></asp:Label>
  
      
      </td>
      <td>
         <asp:TextBox ID="txtNumeroGiorni" runat="server" Width="36px" ReadOnly="true"></asp:TextBox>
         <asp:TextBox ID="txtNumeroGiorniIniziali" runat="server" Width="36px" visible="false"></asp:TextBox>
      </td>
  </tr>
   <tr>
      <td valign="top" class="style4">
         <asp:Label ID="lblVariazioneACarico" runat="server" Text="Variazione a carico:" CssClass="testo_bold"></asp:Label>
       </td>
      <td valign="top" class="style5">
      <asp:DropDownList ID="dropVariazioneACaricoDi" runat="server" AppendDataBoundItems="True" Enabled="false">
          <asp:ListItem  Value="-1">...</asp:ListItem>
          <asp:ListItem  Value="1">Broker</asp:ListItem>
          <asp:ListItem  Value="0">Cliente</asp:ListItem>
      </asp:DropDownList>
       </td>
      <td valign="top" class="style12">
  
      
      <asp:Label ID="lblOldACaricoDi" runat="server" visible="false"></asp:Label>
  
      
       </td>
      <td valign="top" class="style13">
          &nbsp;</td>
      
      <td>
          &nbsp;</td>
      <td>
          &nbsp;</td>
      <td class="style14" colspan="2">
         <asp:Label ID="lblAbbuonaGiornoExtra" runat="server" Text="Abbuona giorno extra:" CssClass="testo_bold" Visible="false"></asp:Label>
       </td>
      <td class="style14">
          <asp:CheckBox ID="chkAbbuonaGiornoExtra" runat="server" Visible="false" />
       </td>
      <td>
          &nbsp;</td>
  </tr>
   <tr>
      <td valign="top" class="style4" colspan="10" align="center">
        <asp:Button ID="btnRicalcolaDaPrenotazione" runat="server" Text="Modifica" 
              Visible="false" ValidationGroup="cerca" Height="26px" />
        <asp:Button ID="btnRicalcolaDaPreventivo" runat="server" Text="Modifica" Visible="false" ValidationGroup="cerca" style="height: 26px" />
        
        <asp:Button ID="btnAnnullaDocumento" runat="server" Text="Annulla ed esci" />
        
                <asp:Button ID="btnCRV" runat="server" Text="CRV" Visible="false" 
              OnClientClick="javascript: return(window.confirm ('Attenzione: sei sicuro di voler effettuare una sostituzione del veicolo attuale?.'));" 
              style="height: 26px" />
                <asp:Button ID="btnAnnullaCRV" runat="server" Text="Annulla CRV" OnClientClick="javascript: return(window.confirm ('Attenzione: sei sicuro di voler annullare il CRV attuale?.'));" Visible="false" />
        <asp:Button ID="btnRicalcolaModificaContratto" runat="server" 
              Text="Modifica/Estensione" Visible="false" ValidationGroup="cerca" 
              style="text-align: left;" />
        
        <asp:Button ID="btnSalvaModifiche" runat="server" Text="Salva Modifiche" Visible="false" ValidationGroup="cerca" style="height: 26px" />
        <asp:Button ID="btnModificaTariffaAdmin" runat="server" Text="Modifica Tariffa" Visible="false" ValidationGroup="cerca" style="height: 26px" />
        
        <asp:Button ID="btnAnnullaModificaContratto" runat="server" Text="Annulla" Visible="false" style="height: 26px" />
       <asp:Button ID="btnSalvaRientro" runat="server" Text="Check In" Visible="false" />
       <asp:Button ID="bt_Gestione_RDS" runat="server" Text="Gestione RDS" 
              Visible="false" style="height: 26px; width: 125px" />
          <%--<asp:Button ID="btnSalvaRientro" runat="server" Text="Salva Rientro"  Visible="false" />--%>
        <asp:Button ID="btnGeneraContratto" runat="server" Text="Stampa Contratto Uscita"  Visible="false" />
               
        <asp:Button ID="btnGeneraContrattoRientro" runat="server" Text="Stampa Contratto Rientro"  Visible="false" />
        <asp:Button ID="bt_Check_Out" runat="server" Text="Check Out" Visible="false" />
        <asp:Button ID="btnPagamento" runat="server" Text="Pagamento" Visible="False" 
              style="text-align: left"  />
        <asp:Button ID="btnVoid" runat="server" Text="Contratto Void" visible="false" 
         OnClientClick="javascript: return(window.confirm ('Attenzione: sei sicuro di voler rendere void il contratto attuale?.'));" />
      
        <asp:Button ID="btnModificaAdmin" runat="server" Text="Modifica Admin" Visible="False"  />
        <asp:Button ID="btnAnnullaModificheAdmin" runat="server" Text="Annulla" Visible="False"  />
              
        <asp:Button ID="btnDuplicaContratto" runat="server" Text="Duplica Contratto" Visible="False"  />
      </td>
   </tr>
   <tr runat="server" id="riga_fatturazione" visible="false" >
      <td valign="top" class="style4" colspan="10" align="center" style="border:4px solid #444">
          <asp:Label ID="lblFattDaControllare" runat="server" Text="UFFICIO FATTURAZIONE - CONTRATTO DA CONTROLLARE  " Font-Size="16px" CssClass="testo_bold" ForeColor="Red" Visible="false"></asp:Label>
          <asp:Label ID="lblStatoFatturazione" runat="server" Text="" Font-Size="16px" CssClass="testo_bold" ForeColor="black"></asp:Label>
          &nbsp;&nbsp;&nbsp;
          <asp:Label ID="lblSaldo" runat="server" Text="" Font-Size="16px" CssClass="testo_bold" ForeColor="Red"></asp:Label>
          &nbsp;&nbsp;&nbsp;
          <asp:Button ID="btnDaFatturare" runat="server" Text="Da Fatturare" Visible="false" />
          <asp:Button ID="btnFattDaControllare" runat="server" Text="Fatturazione - Da Controllare" visible="false" OnClientClick="javascript: return(window.confirm ('Attenzione: il contratto verr� portato all\'attenzione dell\'ufficio fatturazione. Sei sicuro di voler continuare?'));" />
          &nbsp;
          <asp:Label ID="lblNumFattura" runat="server" Text="Num. Fattura" CssClass="testo_bold" Visible="false"></asp:Label>&nbsp;
          <asp:TextBox runat="server" Width="70px" ID="txtNumFattura" Visible="false" onKeyPress="return filterInputInt(event)"></asp:TextBox>&nbsp;
          <asp:Label ID="lblDataFattura" runat="server" Text="Data Fattura" CssClass="testo_bold" Visible="false"></asp:Label>&nbsp;
            <asp:TextBox runat="server" Width="70px" ID="txtDataFattura" Visible="false"></asp:TextBox>
          <ajaxtoolkit:calendarextender runat="server" 
    Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDataFattura" 
    ID="CalendarExtender4">
                </ajaxtoolkit:calendarextender>
          <ajaxtoolkit:maskededitextender runat="server" 
    Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" 
    CultureTimePlaceholder="" CultureDecimalPlaceholder="" 
    CultureThousandsPlaceholder="" CultureDateFormat="" 
    CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" 
    TargetControlID="txtDataFattura" ID="MaskedEditExtender7">
                </ajaxtoolkit:maskededitextender>
      
          <asp:Button ID="btnGeneraFattura" runat="server" Text="Genera Fattura" visible="false" OnClientClick="javascript: return(window.confirm ('Attenzione - Sei sicuro di voler generare la fattura per questo contratto?'));" />
          <asp:Button ID="btnStampaFatturaCliente" runat="server" Text="Stampa Fattura Cliente" Visible="false" />
          <asp:Button ID="btnStampaFatturaBroker" runat="server" Text="Stampa Fattura Broker" Visible="false" />
          <asp:Button ID="btnStampaFatturaPrepagato" runat="server" Text="Stampa Fattura Prepagato" Visible="false" />
          <asp:Button ID="btnEliminaFatture" runat="server" Text="Cancella Fatture" Visible="false" OnClientClick="javascript: return(window.confirm ('Attenzione - Sei sicuro di voler eliminare le fatture di questo contratto?'));" />
      </td>
   </tr>
   <tr>
      <td valign="top" class="style4" colspan="10" align="center">
          <asp:Button ID="btnQuickCheckIn" runat="server" Text="Quick Check In" Width="70%" visible="false" OnClientClick="javascript: return(window.confirm ('Attenzione: sei sicuro di voler effettuare il Quick Check In? Il contratto non sar� pi� modificabile.'));" />
          <asp:Button ID="btnAnnullaQuickCheckIn" runat="server" Text="Annulla Quick Check In" 
              Width="30%" visible="false" 
              OnClientClick="javascript: return(window.confirm ('Attenzione: sei sicuro di voler annullare il Quick Check In?'));" />
      </td>
   </tr>
   <tr>
      <td valign="top" class="style4" colspan="10">
      
        <asp:DataList ID="listWarning" runat="server" DataSourceID="sqlWarning" RepeatColumns="4" RepeatDirection="Horizontal">
            <ItemTemplate>
                <img src="punto_elenco.jpg"  border="0" alt="" title="" width="8" height="7" />&nbsp;<asp:Label ID="warning" runat="server" Text='<%# Eval("warning") %>'  ForeColor="Red" CssClass="testo_bold" />&nbsp;
                <asp:Label ID="tipo" runat="server" Text='<%# Eval("tipo") %>' visible="false" />
            </ItemTemplate>
        </asp:DataList>
  
      </td>
  </tr>
  </table>
    <div runat="server" id="elenco_modifiche" visible="false">   
      <table style="border:4px solid #444; border-bottom:0px; border-top:0px;" border="0" cellspacing="2" cellpadding="2" width="1024px">
        <tr>
          <td align="left" style="color: #FFFFFF;background-color:#444;" >
               <asp:Label ID="Label51" runat="server" Text="Elenco Variazioni" CssClass="testo_titolo"></asp:Label>
          </td>
        </tr>
        <tr>
          <td align="left">
            <asp:ListView ID="listModifiche" runat="server" DataKeyNames="id" DataSourceID="sqlModificheContratto" >
            <ItemTemplate>
                <tr style="background-color:#DCDCDC;color: #000000;">
                    <td align="left">
                       <a runat="server" id="vediCalcolo" href='<%# "contratto_vedi_calcolo.aspx?idCnt=" & Eval("id") & "&versione=" & Eval("num_calcolo") & "&test=" & Eval("milli") %>' rel="lyteframe" title="-" rev="width: 720px; height: 720px; scrolling: yes;" target="_blank"><asp:Image ID="image_primo_guidatore" runat="server" ImageUrl="images/lente.png" style="position:absolute; width:20px; height:20px;" /></a>
                    </td>
                    <td>
                      <asp:Label ID="numCalcoloLabel" runat="server" Text='<%# Eval("num_calcolo") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                        <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                        <asp:Label ID="presunto_rientro" runat="server" Text='<%# Eval("stazione_presunto_rientro")  %>' CssClass="testo" />
                    </td>
                    <td align="left">
                         <asp:Label ID="data_presunto_rientro" runat="server" Text='<%# Eval("data_presunto_rientro")  %>' CssClass="testo" />  
                    </td>
                    <td align="left">
                       <asp:Label ID="Label52" runat="server" Text='<%# Eval("giorni") %>'  CssClass="testo" />
                    </td>
                    <td align="left">
                       <asp:Label ID="Label57" runat="server" Text='<%# Eval("targa") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                       <asp:Label ID="operatore" runat="server" Text='<%# Eval("operatore") %>'  CssClass="testo" />
                    </td>
                    <td align="left">
                        <asp:Label ID="dataOperazione_Label" runat="server" Text='<%# Eval("data_ultima_modifica") %>' CssClass="testo" />
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="">
                    <td align="left">
                       <a runat="server" id="vediCalcolo" href='<%# "contratto_vedi_calcolo.aspx?idCnt=" & Eval("id") & "&versione=" & Eval("num_calcolo") & "&test=" & Eval("milli") %>' rel="lyteframe" title="" rev="width: 720px; height: 720px; scrolling: yes;"><asp:Image ID="image_primo_guidatore" runat="server" ImageUrl="images/lente.png" /></a>
                    </td>
                    <td>
                      <asp:Label ID="numCalcoloLabel" runat="server" Text='<%# Eval("num_calcolo") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                        <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                        <asp:Label ID="presunto_rientro" runat="server" Text='<%# Eval("stazione_presunto_rientro")  %>' CssClass="testo" />
                    </td>
                    <td align="left">
                         <asp:Label ID="data_presunto_rientro" runat="server" Text='<%# Eval("data_presunto_rientro")  %>' CssClass="testo" />  
                    </td>
                    <td align="left">
                       <asp:Label ID="Label52" runat="server" Text='<%# Eval("giorni") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                       <asp:Label ID="Label57" runat="server" Text='<%# Eval("targa") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                       <asp:Label ID="operatore" runat="server" Text='<%# Eval("operatore") %>' CssClass="testo" />
                    </td>
                    <td align="left">
                        <asp:Label ID="dataOperazione_Label" runat="server" Text='<%# Eval("data_ultima_modifica") %>' CssClass="testo" />
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
                                    </th>
                                    <th id="Th7" runat="server" align="left">
                                      <asp:Label ID="Label53" runat="server" Text="N.Op" CssClass="testo_titolo"></asp:Label>
                                    </th>
                                    <th id="Th1" runat="server" align="left" colspan="2">
                                        <asp:Label ID="Label22" runat="server" Text="Presunto Rientro" CssClass="testo_titolo"></asp:Label>
                                    </th>
                                    <th id="Th2" runat="server" align="left">
                                        <asp:Label ID="Label18" runat="server" Text="Giorni Nolo" CssClass="testo_titolo"></asp:Label>
                                    </th>
                                    <th id="Th8" runat="server" align="left">
                                        <asp:Label ID="Label56" runat="server" Text="Veicolo" CssClass="testo_titolo"></asp:Label>
                                    </th>
                                    <th id="Th3" runat="server" align="left">
                                        <asp:Label ID="Label11" runat="server" Text="Operatore" CssClass="testo_titolo"></asp:Label>
                                    </th>
                                    <th id="Th4" runat="server" align="left">
                                        <asp:Label ID="Label16" runat="server" Text="Data Operazione" CssClass="testo_titolo"></asp:Label>
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
      </table>
    </div>
  
  <div runat="server" id="conducenti">   
      <table runat="server" id="table3" style="border:4px solid #444; border-bottom:0px; border-top:0px;" border="0" cellspacing="2" cellpadding="2" width="1024px">
        <tr>
             <td align="left" style="color: #FFFFFF;background-color:#444;" >
               <asp:Label ID="Label20" runat="server" Text="Conducenti" CssClass="testo_titolo"></asp:Label>
            </td>
        </tr>
        <tr>
             <td>
                 <uc1:anagrafica_conducenti ID="anagrafica_conducenti1" runat="server" Visible="false" />
                 <uc1:anagrafica_ditte ID="anagrafica_ditte" runat="server" Visible="false" />
                <asp:Label ID="conducente_da_variare" runat="server" Visible="false"></asp:Label>
             </td>
        </tr>
      </table>
      <table style="border:4px solid #444; border-bottom:0px; border-top:0px;" border="0" cellpadding="1" cellspacing="1" width="1024px">
        <tr>
              <td valign="top">
                  <asp:Label ID="Label23" runat="server" Text="Primo Conducente:" CssClass="testo_bold" ForeColor="Red"></asp:Label>
              </td>
              <td>
                  &nbsp;
                  <asp:Button ID="btnScegliPrimoGuidatore" runat="server" Text="Scegli" />
              </td>
              <td>
                  <asp:Button ID="btnAnnullaScegliPrimoConducente" runat="server" Text="Annulla" Visible="false" />
              </td>
              <td>
                 <a runat="server" id="vediPrimoGuidatore" href="" rel="lyteframe" title="" rev="width: 740px; height: 740px; scrolling: yes;">
                  <asp:Image ID="image_primo_guidatore" runat="server" ImageUrl="images/lente.png" Height="16px" /></a>
              </td>
              <td valign="top">    
                   <asp:Label ID="Label78" runat="server" Text="Cognome" CssClass="testo_bold"></asp:Label>
              </td>
              <td valign="top">
                 <asp:TextBox ID="txtCognomePrimoConducente" runat="server" Width="170px" 
                      ReadOnly="true"></asp:TextBox>
              </td>
              <td valign="top">
                   <asp:Label ID="Label79" runat="server" Text="Nome" CssClass="testo_bold"></asp:Label>
              </td>
              <td>
                 <asp:TextBox ID="txtNomePrimoConducente" runat="server" Width="170px" 
                      ReadOnly="true"></asp:TextBox>
              </td>
              <td>
                   <asp:Label ID="Label80" runat="server" Text="Et�" CssClass="testo_bold"></asp:Label>
              </td>
              <td> 
                   <asp:TextBox ID="txtEtaPrimo" runat="server" Width="40px" Visible="true" ReadOnly="true"></asp:TextBox>
              </td>
              
          </tr>
        <tr>
              <td valign="top">
       
                  <asp:Label ID="idPrimoConducente" runat="server" Visible="false"></asp:Label>
        
              </td>
              <td>
              </td>
              <td>
                
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td valign="top">    
                   <asp:Label ID="Label85" runat="server" Text="Citt�" CssClass="testo_bold"></asp:Label>
              </td>
              <td valign="top">
                 <asp:TextBox ID="txtCittaPrimoConducente" runat="server" Width="170px" 
                      ReadOnly="true"></asp:TextBox>
              </td>
              <td valign="top">
                   <asp:Label ID="Label86" runat="server" Text="Indirizzo" CssClass="testo_bold"></asp:Label>
              </td>
              <td>
                 <asp:TextBox ID="txtIndirizzoPrimoConducente" runat="server" Width="170px" 
                      ReadOnly="true"></asp:TextBox>
              </td>
              <td>
                   &nbsp;</td>
              <td> 
                   &nbsp;</td>
              
          </tr>
        <tr>
              <td valign="top">
                  &nbsp;</td>
              <td>
              
              </td>
              <td>
                
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td valign="top">    
                   <asp:Label ID="Label87" runat="server" Text="Patente" CssClass="testo_bold"></asp:Label>
              </td>
              <td valign="top">
                 <asp:TextBox ID="txtPatentePrimoConducente" runat="server" Width="170px" 
                      ReadOnly="true"></asp:TextBox>
              </td>
              <td valign="top">
                   <asp:Label ID="Label88" runat="server" Text="Documento" CssClass="testo_bold"></asp:Label>
              </td>
              <td>
                 <asp:TextBox ID="txtDocumentoPrimoConducente" runat="server" Width="170px" 
                      ReadOnly="true"></asp:TextBox>
              </td>
              <td>
                   &nbsp;</td>
              <td> 
                   &nbsp;</td>
              
          </tr>
        <tr>
              <td valign="top">
                   <asp:Label ID="Label81" runat="server" Text="Secondo Conducente:" CssClass="testo_bold" ForeColor="Red"></asp:Label>
                   
       
              </td>
              <td>
                   &nbsp;<asp:Button ID="btnScegliSecondoConducente" runat="server" Text="Scegli" />
       
                   </td>
                            <td>
              
                   <asp:Button ID="btnAnnullaScegliSecondoConducente" runat="server" Text="Annulla" Visible="false" />
              
              </td>
              <td>
                <a runat="server" id="vediSecondoGuidatore" href="" rel="lyteframe" title="" rev="width: 740px; height: 740px; scrolling: yes;"><asp:Image ID="image_secondo_guidatore" runat="server" ImageUrl="images/lente.png" /></a>
                  <%-- <asp:ImageButton ID="vediSecondoGuidatore" runat="server" CommandName="vedi" ImageUrl="/images/lente.png" style="width: 16px" />--%>
              </td>
              <td valign="top">    
                   <asp:Label ID="Label82" runat="server" Text="Cognome" CssClass="testo_bold"></asp:Label>
              </td>
              <td valign="top">
                 <asp:TextBox ID="txtCognomeSecondoConducente" runat="server" Width="170px" 
                      ReadOnly="true"></asp:TextBox>
              </td>
              <td valign="top">
                   <asp:Label ID="Label83" runat="server" Text="Nome" CssClass="testo_bold"></asp:Label>
              </td>
              <td>
                 <asp:TextBox ID="txtNomeSecondoConducente" runat="server" Width="170px" 
                      ReadOnly="true"></asp:TextBox>
              </td>
              <td>
                   <asp:Label ID="Label84" runat="server" Text="Et�" CssClass="testo_bold"></asp:Label>
              </td>
              <td> 
                   <asp:TextBox ID="txtEtaSecondo" runat="server" Width="40px" Visible="true" ReadOnly="true"></asp:TextBox>
              </td>
              
          </tr>
        <tr>
              <td valign="top">
       
                  <asp:Label ID="idSecondoConducente" runat="server" Visible="false"></asp:Label>
       
              </td>
              <td>
                   &nbsp;</td>
              <td>
              
              </td>
              <td>
                  &nbsp;</td>
              <td valign="top">    
                   <asp:Label ID="Label89" runat="server" Text="Citt�" CssClass="testo_bold"></asp:Label>
              </td>
              <td valign="top" >
                 <asp:TextBox ID="txtCittaSecondaConducente" runat="server" Width="170px" 
                      ReadOnly="true"></asp:TextBox>
              </td>
              <td valign="top">
                   <asp:Label ID="Label91" runat="server" Text="Indirizzo" CssClass="testo_bold"></asp:Label>
              </td>
              <td>
                 <asp:TextBox ID="txtIndirizzoSecondoConducente" runat="server" Width="170px" 
                      ReadOnly="true"></asp:TextBox>
              </td>
              <td>
                   &nbsp;</td>
              <td> 
                   &nbsp;</td>
              
          </tr>
        <tr>
              <td valign="top">
       
                  <asp:Label ID="idDitta" runat="server" Visible="false"></asp:Label>
       
              </td>
              <td>
                   &nbsp;</td>
              <td>
              
              </td>
              <td class="style49">
                  &nbsp;</td>
              <td valign="top">    
                   <asp:Label ID="Label90" runat="server" Text="Patente" CssClass="testo_bold"></asp:Label>
              </td>
              <td valign="top">
                 <asp:TextBox ID="txtPatenteSecondoConducente" runat="server" Width="170px" 
                      ReadOnly="true"></asp:TextBox>
              </td>
              <td valign="top">
                   <asp:Label ID="Label92" runat="server" Text="Documento" CssClass="testo_bold"></asp:Label>
              </td>
              <td>
                 <asp:TextBox ID="txtDocumentoSecondoConducente" runat="server" Width="170px" 
                      ReadOnly="true"></asp:TextBox>
              </td>
              <td>
                   &nbsp;</td>
              <td> 
                   &nbsp;</td>
              
          </tr>
        <tr>
              <td valign="top">
                   <asp:Label ID="Label93" runat="server" Text="Fatturare A:" CssClass="testo_bold" ForeColor="Red"></asp:Label>
                   
       
              </td>
              <td>
                   &nbsp;<asp:Button ID="btnScegliDitta" runat="server" Text="Scegli" />
              </td>
              <td>
              
                   <asp:Button ID="btnAnnullaScegliDitta" runat="server"  Text="Annulla" Visible="false" />
              
              </td>
              <td>
                <a runat="server" id="vediFattturareA" href="" rel="lyteframe" title="" rev="width: 900px; height: 760px; scrolling: yes;">
                  <asp:Image ID="image_fatturare_a" runat="server" ImageUrl="images/lente.png" /></a>
                </td>
              <td valign="top">    
                   <asp:Label ID="Label94" runat="server" Text="Ditta" CssClass="testo_bold"></asp:Label>
              </td>
              <td valign="top" colspan="3">
                 <asp:TextBox ID="txtNomeDitta" runat="server" Width="435px"  ReadOnly="True"></asp:TextBox>
              </td>
              <td>
                   <asp:Label ID="Label95" runat="server" Text="EDP" CssClass="testo_bold"></asp:Label>
              </td>
              <td> 
                   <asp:TextBox ID="txtCodiceEdp" runat="server" Width="60px" Visible="true" ReadOnly="true"></asp:TextBox>
              </td>
              
          </tr>
        <tr runat="server" id="riga_commissioni" visible="false">
              <td valign="top">
                 <asp:Label ID="lblFonteCommissionabile" runat="server" Text="Fonte Commissionabile:" CssClass="testo_bold" ForeColor="Red"></asp:Label>
              </td>
              <td colspan="7">
                     <asp:DropDownList ID="dropFonteCommissionabile" runat="server" 
                         AppendDataBoundItems="True" DataSourceID="sqlFontiCommissionabili" 
                         DataTextField="rag_soc" DataValueField="id">
                         <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                     </asp:DropDownList>
                     <asp:TextBox ID="txtPercentualeCommissionabile" runat="server" Readonly="True" Width="32px"></asp:TextBox>
                     <asp:Label ID="lblPercentualeCommissionabile" runat="server" CssClass="font-bold" Text="%"></asp:Label>
                     &nbsp;<asp:DropDownList ID="dropTipoCommissione" runat="server" 
                        AppendDataBoundItems="True">
                        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                        <asp:ListItem Selected="False" Value="1">Da riconoscere</asp:ListItem>
                        <asp:ListItem Selected="False" Value="2">Preincassate</asp:ListItem>
                    </asp:DropDownList>
              &nbsp;<asp:Label ID="lblGGcommissioniOriginali" runat="server" Visible="false"></asp:Label>
    
              </td>
              <td>
                   &nbsp;</td>
              <td> 
                   &nbsp;</td>
              
          </tr>
      </table>
  </div>
  <div runat="server" id="Div1">   
    <div runat="server" id="div_crv" visible="false">   
      <table runat="server" id="table6" style="border:4px solid #444; border-bottom:0px; border-top:0px;" border="0" cellspacing="2" cellpadding="2" width="1024px">
        <tr>
             <td align="left" style="color: #FFFFFF;background-color:#444;">
               <asp:Label ID="Label63" runat="server" Text="Lista Veicoli - CRV" CssClass="testo_titolo"></asp:Label>
             </td>
        </tr>
        <tr>
          <td>
          
               <asp:ListView ID="listCrv" runat="server" DataKeyNames="id" DataSourceID="sqlListaCrv" >
                    <ItemTemplate>
                        <tr style="background-color:#DCDCDC;color: #000000;">
                            <td align="left">
                                <asp:Label ID="check_in_effettuato" runat="server" Text='<%# Eval("check_in_effettuato") %>' Visible="false" />
                                <asp:Button ID="btnCheckOut" runat="server" Text="Check Out" CommandName="checkOut" CommandArgument='<%# Eval("id_veicolo") %>' />
                                <asp:Button ID="btnCheckIn" runat="server" Text="Check In" CommandName="checkIn" CommandArgument='<%# Eval("id_veicolo") %>' />
                                <asp:Button ID="btnVediCheck" runat="server" Text="Vedi Check" CommandName="vediCheck" CommandArgument='<%# Eval("id_veicolo") %>' />
                            </td>
                            <td>
                               <asp:Button ID="btnStampaCrv" runat="server" Text='<%# Replace(Eval("num_crv"),"0","") %>' CommandName="stampa_crv" CommandArgument='<%# Replace(Eval("num_crv"),"0","") %>' ToolTip="Stampa Crv"  />
                               <asp:Label ID="lblNumCrv" runat="server" Text='<%# Replace(Eval("num_crv"),"0","") %>' visible="false" />
                            </td>
                            <td>
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                              <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo" />
                            </td>
                            <td align="left">
                                <asp:Label ID="modello" runat="server" Text='<%# Eval("modello")  %>' CssClass="testo" />
                            </td>
                            <td>
                               <asp:Label ID="Label113" runat="server" Text='<%# Eval("data_uscita")  %>' CssClass="testo" />
                            </td>
                            <td align="left">
                                <asp:Label ID="km_uscita" runat="server" Text='<%# Eval("km_uscita")  %>'  CssClass="testo" />  
                            </td>
                            <td align="left">
                               <asp:Label ID="serbatoio_uscita" runat="server" Text='<%# Eval("serbatoio_uscita") %>'  CssClass="testo" />
                               <asp:Label ID="Label106" runat="server" Text='/'  CssClass="testo" />
                               <asp:Label ID="capacita_serbatoio" runat="server" Text='<%# Eval("capacita_serbatoio") %>'  CssClass="testo" />
                            </td>
                            <td>
                               <asp:Label ID="lbl_data_sostituzione" runat="server" Text='<%# Eval("data_sostituzione")  %>' CssClass="testo" />
                            </td>
                            <td align="left">
                                <asp:Label ID="km_rientro" runat="server" Text='<%# Eval("km_rientro")  %>'  CssClass="testo" />  
                            </td>
                            <td align="left">
                               <asp:Label ID="Label117" runat="server" Text='<%# Eval("serbatoio_rientro") %>'  CssClass="testo" />
                               <asp:Label ID="Label118" runat="server" Text='/'  CssClass="testo" />
                               <asp:Label ID="Label119" runat="server" Text='<%# Eval("capacita_serbatoio") %>'  CssClass="testo" />
                            </td>
                            <td align="left">
                               <asp:Label ID="operatore0" runat="server" Text='<%# Eval("carburante") %>'  CssClass="testo" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:#DCDCDC;color: #000000;">
                            <td align="left">
                                <asp:Label ID="check_in_effettuato" runat="server" Text='<%# Eval("check_in_effettuato") %>' Visible="false" />
                                <asp:Button ID="btnCheckOut" runat="server" Text="Check Out" CommandName="checkOut" CommandArgument='<%# Eval("id_veicolo") %>' />
                                <asp:Button ID="btnCheckIn" runat="server" Text="Check In" CommandName="checkIn" CommandArgument='<%# Eval("id_veicolo") %>' />   
                                <asp:Button ID="btnVediCheck" runat="server" Text="Vedi Check" CommandName="vediCheck" CommandArgument='<%# Eval("id_veicolo") %>' />
                            </td>
                            <td>
                               <asp:Button ID="btnStampaCrv" runat="server" Text='<%# Replace(Eval("num_crv"),"0","") %>' CommandName="stampa_crv" CommandArgument='<%# Replace(Eval("num_crv"),"0","") %>' ToolTip="Stampa Crv"  />
                               <asp:Label ID="lblNumCrv" runat="server" Text='<%# Replace(Eval("num_crv"),"0","") %>' visible="false" />
                            </td>
                            <td>
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                              <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo" />
                            </td>
                            <td align="left">
                                <asp:Label ID="modello" runat="server" Text='<%# Eval("modello")  %>' CssClass="testo" />
                            </td>
                            <td>
                               <asp:Label ID="Label113" runat="server" Text='<%# Eval("data_uscita")  %>' CssClass="testo" />
                            </td>
                            <td align="left">
                                <asp:Label ID="km_uscita" runat="server" Text='<%# Eval("km_uscita")  %>'  CssClass="testo" />  
                            </td>
                            <td align="left">
                               <asp:Label ID="serbatoio_uscita" runat="server" Text='<%# Eval("serbatoio_uscita") %>'  CssClass="testo" />
                               <asp:Label ID="Label106" runat="server" Text='/'  CssClass="testo" />
                               <asp:Label ID="capacita_serbatoio" runat="server" Text='<%# Eval("capacita_serbatoio") %>'  CssClass="testo" />
                            </td>
                            <td>
                               <asp:Label ID="lbl_data_sostituzione" runat="server" Text='<%# Eval("data_sostituzione")  %>' CssClass="testo" />
                            </td>
                            <td align="left">
                                <asp:Label ID="km_rientro" runat="server" Text='<%# Eval("km_rientro")  %>'  CssClass="testo" />  
                            </td>
                            <td align="left">
                               <asp:Label ID="Label117" runat="server" Text='<%# Eval("serbatoio_rientro") %>'  CssClass="testo" />
                               <asp:Label ID="Label118" runat="server" Text='/'  CssClass="testo" />
                               <asp:Label ID="Label119" runat="server" Text='<%# Eval("capacita_serbatoio") %>'  CssClass="testo" />
                            </td>
                            <td align="left">
                               <asp:Label ID="operatore0" runat="server" Text='<%# Eval("carburante") %>'  CssClass="testo" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table7" runat="server" style="">
                            <tr>
                                <td>
                                  <asp:Label ID="operatore2" runat="server" Text='Nessun veicolo disponibile.'  
                                        CssClass="testo" />
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <table id="Table8" runat="server" width="100%">
                            <tr id="Tr4" runat="server">
                                <td id="Td3" runat="server">
                                    <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%" 
                                              
                                        style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                        <tr id="Tr5" runat="server"  style="color: #FFFFFF" bgcolor="#19191b">
                                            <th id="Th9" runat="server" align="left">
                                            </th>
                                            <th id="Th15" runat="server" align="left">
                                              <asp:Label ID="Label107" runat="server" Text="CRV" CssClass="testo_titolo"></asp:Label>
                                            </th>
                                            <th id="Th10" runat="server" align="left">
                                              <asp:Label ID="Label108" runat="server" Text="Targa" CssClass="testo_titolo"></asp:Label>
                                            </th>
                                            <th id="Th11" runat="server" align="left">
                                                <asp:Label ID="Label109" runat="server" Text="Modello" CssClass="testo_titolo"></asp:Label>
                                            </th>
                                            <th id="Th16" runat="server" align="left">
                                                <asp:Label ID="Label120" runat="server" Text="Uscita" CssClass="testo_titolo"></asp:Label>
                                            </th>
                                            <th id="Th12" runat="server" align="left">
                                                <asp:Label ID="Label110" runat="server" Text="KM" CssClass="testo_titolo"></asp:Label>
                                            </th>
                                            <th id="Th13" runat="server" align="left">
                                                <asp:Label ID="Label111" runat="server" Text="Serb." CssClass="testo_titolo"></asp:Label>
                                            </th>
                                            <th id="Th17" runat="server" align="left">
                                                <asp:Label ID="Label121" runat="server" Text="Sostituzione" CssClass="testo_titolo"></asp:Label>
                                            </th>
                                            <th id="Th18" runat="server" align="left">
                                                <asp:Label ID="Label122" runat="server" Text="KM" CssClass="testo_titolo"></asp:Label>
                                            </th>
                                            <th id="Th19" runat="server" align="left">
                                                <asp:Label ID="Label123" runat="server" Text="Serb." CssClass="testo_titolo"></asp:Label>
                                            </th>
                                            <th id="Th14" runat="server" align="left">
                                                <asp:Label ID="Label112" runat="server" Text="Alim." CssClass="testo_titolo"></asp:Label>
                                            </th>
                                        </tr>
                                        <tr ID="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Tr6" runat="server">
                                <td id="Td4" runat="server" style="">
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    
                </asp:ListView>
          
          </td>
        </tr>
      </table>
   </div>
  
      <table runat="server" id="table4" style="border:4px solid #444; border-bottom:0px; border-top:0px;" border="0" cellspacing="2" cellpadding="2" width="1024px">
        <tr>
             <td align="left" style="color: #FFFFFF;background-color:#444;" colspan="11" >
               <asp:Label ID="Label41" runat="server" Text="Veicolo" CssClass="testo_titolo"></asp:Label>
               <asp:Label ID="id_auto_selezionata" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="auto_collegata" runat="server" Visible="false"></asp:Label>
               <asp:Label ID="id_alimentazione" runat="server" Visible="false"></asp:Label>
               <asp:Label ID="id_scegli_auto_gruppo" runat="server" Text="0" Visible="false"></asp:Label>
               <asp:Label ID="id_scegli_auto_stazione" Text="0" runat="server" Visible="false"></asp:Label>
             </td>
        </tr>
        <tr runat="server" id="riga_vedi_auto" visible="false">
             <td align="left" colspan="11" >
               <asp:ListView ID="listScegliVeicolo" runat="server" DataKeyNames="id" DataSourceID="sqlScegliVeicolo" >
                    <ItemTemplate>
                        <tr style="background-color:#DCDCDC;color: #000000;">
                            <td align="left">
                                <asp:Button ID="btnScegli" runat="server" Text="Scegli" CommandName="scegli" CommandArgument='<%# Eval("targa") %>' Visible="false" />
                                <asp:Button ID="btnScegliAltroGruppo" runat="server" Text="Scegli" CommandName="scegli_cambia_gruppo" CommandArgument='<%# Eval("targa") %>' Visible="false" OnClientClick="javascript: return(window.confirm ('Attenzione: il gruppo dell\'auto selezionata � diversa da quello che dovrebbe essere consegnato. Continuando verr� collegata quest\'auto al contratto senza ricalcolare i costi col nuovo gruppo. Sei sicuro di voler continuare?'));" />
                            </td>
                            <td>
                              <asp:Label ID="id_gruppo" runat="server" Text='<%# Eval("id_gruppo")  %>' Visible="false" />
                              <asp:Label ID="cod_gruppo" runat="server" Text='<%# Eval("cod_gruppo")  %>' CssClass="testo" />
                            </td>
                            <td>
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo" />
                            </td>
                            <td align="left">
                                <asp:Label ID="modello" runat="server" Text='<%# Eval("modello")  %>' CssClass="testo" />
                            </td>
                            <td align="left">
                                 <asp:Label ID="km_attuali" runat="server" Text='<%# Eval("km_attuali")  %>' CssClass="testo" />  
                            </td>
                            <td align="left">
                               <asp:Label ID="serbatoio_attuale" runat="server" Text='<%# Eval("serbatoio_attuale") %>'  CssClass="testo" />
                               <asp:Label ID="Label55" runat="server" Text='/'  CssClass="testo" />
                               <asp:Label ID="capacita_serbatoio" runat="server" Text='<%# Eval("capacita_serbatoio") %>'  CssClass="testo" />
                            </td>
                            <td align="left">
                               <asp:Label ID="operatore" runat="server" Text='<%# Eval("carburante") %>'  CssClass="testo" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="">
                            <td align="left">
                                <asp:Button ID="btnScegli" runat="server" Text="Scegli" CommandName="scegli" CommandArgument='<%# Eval("targa") %>' Visible="false" />
                                <asp:Button ID="btnScegliAltroGruppo" runat="server" Text="Scegli" CommandName="scegli_cambia_gruppo" CommandArgument='<%# Eval("targa") %>' Visible="false" OnClientClick="javascript: return(window.confirm ('Attenzione: il gruppo dell\'auto selezionata � diversa da quello che dovrebbe essere consegnato. Continuando verr� collegata quest\'auto al contratto senza ricalcolare i costi col nuovo gruppo. Sei sicuro di voler continuare?'));" />
                            </td>
                            <td>
                              <asp:Label ID="id_gruppo" runat="server" Text='<%# Eval("id_gruppo")  %>' Visible="false" />
                              <asp:Label ID="cod_gruppo" runat="server" Text='<%# Eval("cod_gruppo")  %>' CssClass="testo" />
                            </td>
                            <td>
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo" />
                            </td>
                            <td align="left">
                                <asp:Label ID="modello" runat="server" Text='<%# Eval("modello")  %>' CssClass="testo" />
                            </td>
                            <td align="left">
                                 <asp:Label ID="km_attuali" runat="server" Text='<%# Eval("km_attuali")  %>' CssClass="testo" />  
                            </td>
                            <td align="left">
                               <asp:Label ID="serbatoio_attuale" runat="server" Text='<%# Eval("serbatoio_attuale") %>'  CssClass="testo" />
                               <asp:Label ID="Label55" runat="server" Text='/'  CssClass="testo" />
                               <asp:Label ID="capacita_serbatoio" runat="server" Text='<%# Eval("capacita_serbatoio") %>'  CssClass="testo" />
                            </td>
                            <td align="left">
                               <asp:Label ID="operatore" runat="server" Text='<%# Eval("carburante") %>'  CssClass="testo" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table1" runat="server" style="">
                            <tr>
                                <td>
                                  <asp:Label ID="operatore" runat="server" Text='Nessun veicolo disponibile.'  CssClass="testo" />
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
                                            </th>
                                            <th id="Th20" runat="server" align="left">
                                              <asp:Label ID="Label127" runat="server" Text="Gruppo" CssClass="testo_titolo"></asp:Label>
                                            </th>
                                            <th id="Th7" runat="server" align="left">
                                              <asp:Label ID="Label53" runat="server" Text="Targa" CssClass="testo_titolo"></asp:Label>
                                            </th>
                                            <th id="Th1" runat="server" align="left">
                                                <asp:Label ID="Label22" runat="server" Text="Modello" CssClass="testo_titolo"></asp:Label>
                                            </th>
                                            <th id="Th2" runat="server" align="left">
                                                <asp:Label ID="Label18" runat="server" Text="KM" CssClass="testo_titolo"></asp:Label>
                                            </th>
                                            <th id="Th3" runat="server" align="left">
                                                <asp:Label ID="Label11" runat="server" Text="Serbatoio" CssClass="testo_titolo"></asp:Label>
                                            </th>
                                            <th id="Th4" runat="server" align="left">
                                                <asp:Label ID="Label16" runat="server" Text="Alimentazione" CssClass="testo_titolo"></asp:Label>
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
            <td class="style12" valign="top">    
                <asp:Label ID="Label42" runat="server" Text="Targa" CssClass="testo_bold"></asp:Label>
            </td>
            <td class="style56" valign="top">
                <asp:TextBox ID="txtTarga" runat="server" Width="84px"></asp:TextBox>
            </td>
            <td valign="top" >
                  <asp:Button ID="btnScegliTarga" runat="server" Text="Seleziona" />
             
                  <asp:Button ID="btnAnnullaModificaTargaNoloInCorso" runat="server" Text="Annulla" Visible="false" />
                  <asp:Button ID="btnAnnullaSelezioneTargaCrv" runat="server" Text="Annulla" Visible="false" />
                  
                  <asp:Button ID="btnTrovaTarga" runat="server" Text="Trova" />
                  <asp:Button ID="btnImmissioneInParco" runat="server" 
                      Text="Imm. in parco e seleziona" Visible="false" Width="182px" />
            </td>
            <td align="left" class="style58" valign="top">
              <asp:Label ID="Label48" runat="server" Text="Gruppo:" CssClass="testo_bold"></asp:Label>
            </td>
            <td class="style60" valign="top">           
               <asp:TextBox ID="txtGruppo" runat="server" Width="40px" ReadOnly="true"></asp:TextBox>
            </td>
            <td align="left" class="style58" valign="top">
              <asp:Label ID="Label44" runat="server" Text="Modello:" CssClass="testo_bold"></asp:Label>
            </td>
            <td class="style60" valign="top">           
               <asp:TextBox ID="txtModello" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
            </td>
            <td align="left" class="style58" valign="top">
              <asp:Label ID="Label43" runat="server" Text="Km:" CssClass="testo_bold"></asp:Label>
            </td>
            <td class="style59" valign="top">           
               <asp:TextBox ID="txtKm" runat="server" Width="50px" ReadOnly="true" onKeyPress="return filterInputInt(event)"></asp:TextBox>
            </td>
            <td class="style38" valign="top">
              <asp:Label ID="Label11" runat="server" Text="Serbatoio:" CssClass="testo_bold"></asp:Label>
           </td>
           <td valign="top">           
               <asp:TextBox ID="txtSerbatoio" runat="server" Width="50px" ReadOnly="true" onKeyPress="return filterInputInt(event)"></asp:TextBox>
               <asp:Label ID="Label12" runat="server" Text="/" CssClass="testo_bold"></asp:Label>
               <asp:Label ID="lblSerbatoioMax" runat="server" CssClass="testo_bold"></asp:Label>
               &nbsp&nbsp;<asp:Label ID="lblTipoSerbatoio" runat="server" CssClass="testo_bold"></asp:Label>
           </td>
        </tr>
        <tr runat="server" visible="false" id="riga_rientro_veicolo">       
            <td valign="top" class="style12">    
                &nbsp;</td>
            <td valign="top" class="style56">
                &nbsp;</td>
            <td valign="top">
                  &nbsp;</td>
            <td align="left" class="style58">
                &nbsp;</td>
            <td class="style60">           
                &nbsp;</td>
            <td align="left" class="style58">
                &nbsp;</td>
            <td class="style60" align="right">           
                  
               <asp:Label ID="lblRientro" runat="server" Text="RIENTRO:"  CssClass="testo_bold" ForeColor="Red" Visible="True"></asp:Label>
        
            </td>
            <td align="left" class="style58">
              <asp:Label ID="Label96" runat="server" Text="Km:" CssClass="testo_bold"></asp:Label>
            </td>
            <td class="style59">           
               <asp:TextBox ID="txtKmRientro" runat="server" Width="50px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
            </td>
            <td class="style38" >
              <asp:Label ID="Label97" runat="server" Text="Serbatoio:" CssClass="testo_bold"></asp:Label>
            </td>
           <td>           
               <asp:TextBox ID="txtSerbatoioRientro" runat="server" Width="50px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
               <asp:Label ID="Label98" runat="server" Text="/" CssClass="testo_bold"></asp:Label>
               <asp:Label ID="lblSerbatoioMaxRientro" runat="server" CssClass="testo_bold"></asp:Label>
            </td>
        </tr>
        </table>
        <table runat="server" id="rifornimento" visible="false" style="border:4px solid #444; border-bottom:0px; border-top:0px;" border="0" cellspacing="2" cellpadding="2" width="1024px">
            <tr>
                 <td align="left" style="color: #FFFFFF" bgcolor="#19191b" colspan="7" >
                   <asp:Label ID="Label59" runat="server" Text="Rifornimento" CssClass="testo_titolo"></asp:Label>
                   <asp:Label ID="id_rifornimento" runat="server" Text="" visible="false"></asp:Label>
                   <asp:Label ID="in_rifornimento" runat="server" Text="" visible="false"></asp:Label>
                 </td>

            </tr>
            <tr>       
                <td  valign="top" class="style54">    
                   <asp:Label ID="Label101" runat="server" Text="Data/Ora Uscita" CssClass="testo_bold"></asp:Label>
                </td>
                <td  valign="top" class="style54">    
                   <asp:Label ID="Label61" runat="server" Text="Data/Ora Rientro" CssClass="testo_bold"></asp:Label>
                </td>
                <td  valign="top" class="style54">    
              <asp:Label ID="lblFornitore" runat="server" Text="Fornitore" CssClass="testo_bold" ></asp:Label>              
                </td>
                <td valign="top" class="style54">
                   <asp:Label ID="Label102" runat="server" Text="Conducente" CssClass="testo_bold"></asp:Label>
                </td>
                <td valign="top" class="style73">
                   <asp:Label ID="Label103" runat="server" Text="Litri riforniti" CssClass="testo_bold"></asp:Label>
                </td>
                <td valign="top" class="style74">
                   <asp:Label ID="Label104" runat="server" Text="Importo" CssClass="testo_bold"></asp:Label>
                </td>
                <td valign="top">
                   <asp:Label ID="Label105" runat="server" Text="Km Rientro" CssClass="testo_bold"></asp:Label>
                </td>
            </tr>
            <tr>       
                <td  valign="top" class="style54">    
                    <asp:TextBox ID="txtRifornimentoUscita" runat="server" Width="70px"></asp:TextBox>
                    <ajaxtoolkit:calendarextender ID="txtRifornimentoUscita_CalendarExtender" 
                        runat="server" Format="dd/MM/yyyy" 
    TargetControlID="txtRifornimentoUscita" 
    OnClientDateSelectionChanged="onSelectedStartDate">
                    </ajaxtoolkit:calendarextender>
                    <ajaxtoolkit:maskededitextender ID="txtRifornimentoUscita_MaskedEditExtender" 
                        runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                        TargetControlID="txtRifornimentoUscita">
                    </ajaxtoolkit:maskededitextender>
                
                <!-- Separatore / -->
                      <asp:Label ID="LblSeperatore" runat="server" CssClass="testo_bold" Text="/"></asp:Label>
                    <asp:TextBox ID="txtOraRifornimentoUscita" runat="server" Width="32px"></asp:TextBox>
                    <ajaxtoolkit:maskededitextender ID="txtOraRifornimentoUscita_MaskedEditExtender" 
                        runat="server" Mask="99:99" MaskType="Time" MessageValidatorTip="true" 
                        OnFocusCssClass="MaskedEditFocus" 
    TargetControlID="txtOraRifornimentoUscita">
                    </ajaxtoolkit:maskededitextender>
                </td>
                <td valign="top" class="style54">
                      <asp:TextBox ID="txtRifornimentoRientro" runat="server" Width="70px"></asp:TextBox>
                      <ajaxtoolkit:calendarextender ID="CalendarExtender1" runat="server" 
                          Format="dd/MM/yyyy" 
    TargetControlID="txtRifornimentoRientro" BehaviorID="endDate">
                      </ajaxtoolkit:calendarextender>
                      <ajaxtoolkit:maskededitextender ID="MaskedEditExtender5" runat="server" 
                          Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
                          TargetControlID="txtRifornimentoRientro">
                      </ajaxtoolkit:maskededitextender>
                
                <!-- Separatore / -->
                      <asp:Label ID="Label62" runat="server" CssClass="testo_bold" Text="/"></asp:Label>
                      <asp:TextBox ID="txtOraRifornimentoRientro" runat="server" Width="32px"></asp:TextBox>
                      <ajaxtoolkit:maskededitextender ID="MaskedEditExtender6" runat="server" 
                          Mask="99:99" MaskType="Time" MessageValidatorTip="true" 
                          OnFocusCssClass="MaskedEditFocus" 
    TargetControlID="txtOraRifornimentoRientro">
                      </ajaxtoolkit:maskededitextender>
                </td>
                <td valign="top" class="style54">
            <asp:DropDownList ID="DDLFornitore" runat="server"
                  DataSourceID="sqlFornitori" DataTextField="descrizione" DataValueField="id"
                   AppendDataBoundItems="True" >
                  <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
              </asp:DropDownList>    
                </td>
                <td  valign="top" class="style54">    
                  <asp:DropDownList ID="DDLConducenti" runat="server"
                      DataSourceID="SqlDSConducenti" DataTextField="nominativo" DataValueField="id"
                       AppendDataBoundItems="True" >
                      <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                  </asp:DropDownList>                           
                </td>
                <td valign="top" class="style73">
                    <asp:TextBox ID="txtLitriRiforniti" runat="server" 
                        onKeyPress="return filterInputDouble(event)" Width="50px"></asp:TextBox>
                </td>
                <td valign="top" class="style74">
                    <asp:TextBox ID="txtImportoRifornimento" runat="server" 
                        onKeyPress="return filterInputDouble(event)" Width="50px"></asp:TextBox>
                </td>
                 <td valign="top">
                   <asp:TextBox ID="txtKmRientroRifornimento" runat="server" 
                        onKeyPress="return filterInputInt(event)" Width="50px"></asp:TextBox>
                </td>
            </tr>
            <tr>       
                <td  valign="top" class="style72" colspan="7" align="center">    
                  <asp:Button ID="btnRegistraPieno" runat="server" Text="Registra" ValidationGroup="pieno_carburante" />
                   &nbsp;<asp:Button ID="btnNoRifornimento" runat="server" Text="Seleziona senza rifornire" OnClientClick="javascript: return(window.confirm ('Attenzione: l'auto uscir� senza il pieno effettuato. Continuando l'auto passer� nello stato disponibile nolo. Sei sicuro di voler continuare?'));" />
                   <!-- Data Movimento -->
                     <asp:RequiredFieldValidator ID="RFData" runat="server" 
                               ControlToValidate="txtRifornimentoUscita" ErrorMessage="Specificare la data di uscita dell'operazione di pieno carburante." 
                               Font-Size="0pt" SetFocusOnError="true" ValidationGroup="pieno_carburante">
                     </asp:RequiredFieldValidator>

                     <!-- Ora Movimento  -->
                     <asp:RequiredFieldValidator ID="RFOra" runat="server" 
                               ControlToValidate="txtOraRifornimentoUscita" ErrorMessage="Specificare l'orario di uscita dell'operazione di pieno carburante." 
                               Font-Size="0pt" SetFocusOnError="true" ValidationGroup="pieno_carburante">
                     </asp:RequiredFieldValidator>
                     
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                               ControlToValidate="txtRifornimentoRientro" ErrorMessage="Specificare la data di rientro dell'operazione di pieno carburante." 
                               Font-Size="0pt" SetFocusOnError="true" ValidationGroup="pieno_carburante">
                     </asp:RequiredFieldValidator>

                     <!-- Ora Movimento  -->
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                               ControlToValidate="txtOraRifornimentoRientro" ErrorMessage="Specificare l'orario di rientro dell'operazione di pieno carburante." 
                               Font-Size="0pt" SetFocusOnError="true" ValidationGroup="pieno_carburante">
                     </asp:RequiredFieldValidator>
                     <!-- Km Movimento  -->
                     <asp:RequiredFieldValidator ID="RFKm" runat="server" 
                               ControlToValidate="txtLitriRiforniti" ErrorMessage="Specificare i litri riforniti." 
                               Font-Size="0pt" SetFocusOnError="true" ValidationGroup="pieno_carburante">
                     </asp:RequiredFieldValidator>
<!-- Km Movimento  -->
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                               ControlToValidate="txtKmRientroRifornimento" ErrorMessage="Specificare i KM di rientro del movimento di rifornimento." 
                               Font-Size="0pt" SetFocusOnError="true" ValidationGroup="pieno_carburante">
                     </asp:RequiredFieldValidator>
                     <!-- Conducente  -->
                     <asp:RequiredFieldValidator ID="RFConducente" runat="server" 
                               ControlToValidate="DDLConducenti" ErrorMessage="Specificare il conducente del movimento di rifornimento." 
                               Font-Size="0pt" SetFocusOnError="true" ValidationGroup="pieno_carburante" 
                           InitialValue="0"></asp:RequiredFieldValidator>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                               ControlToValidate="DDLFornitore" ErrorMessage="Specificare il fornitore." 
                               Font-Size="0pt" SetFocusOnError="true" ValidationGroup="pieno_carburante" 
                           InitialValue="0">
                     </asp:RequiredFieldValidator>
                           
                     <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                       DisplayMode="List" ShowMessageBox="True" ShowSummary="False" 
                       ValidationGroup="pieno_carburante" />
                </td>
               
           </tr>
        </table>
        <table  runat="server" id="pulizia" visible="false" style="border:4px solid #444; border-bottom:0px; border-top:0px;" border="0" cellspacing="2" cellpadding="2" width="1024px">
            <tr>
                 <td align="left" style="color: #FFFFFF" bgcolor="#19191b" colspan="11" >
                   <asp:Label ID="Label60" runat="server" Text="Pulizia" CssClass="testo_titolo"></asp:Label>
                   <asp:Label ID="id_pulizia" runat="server" Text="" visible="false"></asp:Label>
                   <asp:Label ID="in_pulizia" runat="server" Text="" visible="false"></asp:Label>
                 </td>
            </tr>
            <tr>       
                <td class="style12" valign="top">    
                    &nbsp;</td>
                <td class="style56" valign="top">
                    &nbsp;</td>
                <td valign="top" >
                      &nbsp;</td>
                <td align="left" class="style58" valign="top">
                    &nbsp;</td>
                <td class="style60" valign="top">           
                    &nbsp;</td>
                <td align="left" class="style58" valign="top">
                    &nbsp;</td>
                <td class="style60" valign="top">           
                    &nbsp;</td>
                <td align="left" class="style58" valign="top">
                    &nbsp;</td>
                <td class="style59" valign="top">           
                    &nbsp;</td>
                <td class="style38" valign="top">
                    &nbsp;</td>
               <td valign="top">           
                   &nbsp;</td>
            </tr>
      </table>
  </div>
  <div runat="server" id="div_gps" visible="false">
    <table style="border:4px solid #444; border-bottom:0px;" border="0" cellspacing="2" cellpadding="2" width="1024px">
        <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" class="style73" >
             <asp:Label ID="Label100" runat="server" Text="GPS" CssClass="testo_titolo"></asp:Label>
          </td>
           <td align="left" style="color: #FFFFFF;background-color:#444;" class="style75">
              
           </td>
           <td align="left" style="color: #FFFFFF;background-color:#444;">
              
               &nbsp;</td>
        </tr>
        <tr>
           <td align="left" class="style73" valign="top" >
             <asp:Label ID="Label116" runat="server" Text="Codice GPS" CssClass="testo_bold"></asp:Label>
          </td>
           <td align="left" class="style75" valign="top">
               <asp:TextBox ID="txtCodiceGps" runat="server"></asp:TextBox>
                
                <asp:Button ID="btnCercaGps" Text="..." runat="server" Width="30px" />
                
                <asp:ListBox ID="listGps" runat="server" Width="200px" AutoPostBack="true" Visible="false"
                    DataTextField="codice" DataValueField="id">
                </asp:ListBox>
           </td>
           <td align="left" valign="top">
                <asp:Label ID="lblIdGps" runat="server" Visible="false"></asp:Label>
            </td>
      </tr>
    </table>
  </div>
  <div runat="server" id="tariffe_e_costi">
  <table runat="server" id="table_tariffe" style="border:4px solid #444; border-bottom:0px;" border="0" cellspacing="2" cellpadding="2" width="1024px">
      <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" colspan="7">
             <asp:Label ID="Label19" runat="server" Text="Tariffa e costi" CssClass="testo_titolo"></asp:Label>
          </td>
           <td align="left" style="color: #FFFFFF;background-color:#444;">
               &nbsp;</td>
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
          <td valign="top" class="style32" colspan="4">
              <asp:DropDownList ID="dropTariffeParticolari" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlTariffeParticolari" 
                    DataTextField="codice" DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
              </asp:DropDownList>
          </td>
          <td>
              <asp:DropDownList ID="dropTest" runat="server" AppendDataBoundItems="True">
                  <asp:ListItem Selected="True" Value="0">Test</asp:ListItem>
                  <asp:ListItem Value="1">Reale</asp:ListItem>
              </asp:DropDownList>
        
          </td>
      </tr>
      <tr>
          <td valign="top" class="style27">
              &nbsp;</td>
          <td valign="top" class="style35">
                  &nbsp;</td>
          <td valign="top" class="style30">
              <asp:Label ID="Label10" runat="server" Text="Sconto:" 
                CssClass="testo_bold"></asp:Label>
          </td>
          <td valign="top" class="style32" colspan="4">
            <asp:TextBox ID="txtSconto" runat="server" Width="32px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
              <b>&nbsp;%</b>
              <asp:DropDownList ID="dropTipoSconto" runat="server" 
            AppendDataBoundItems="True">
            <asp:ListItem Selected="True" Value="0">Elementi Scontabili</asp:ListItem>
            <asp:ListItem Selected="False" Value="1">Valore Tariffa</asp:ListItem>
        </asp:DropDownList>
              <asp:Label ID="lblMxSconto" runat="server" CssClass="testo_bold" 
                  ForeColor="Red" Text="Applicato il MASSIMO SCONTO." Visible="false"></asp:Label>
          </td>
          <td>
              &nbsp;</td>
      </tr>
      <tr>
          <td valign="top" class="style27">
     
     <asp:Label ID="Label24" runat="server" Text="Accessori Extra:" CssClass="testo_bold"></asp:Label>
  
          </td>
          <td valign="top" class="style29" colspan="4">
      <asp:DropDownList ID="dropElementiExtra" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlElementiExtra" 
            DataTextField="descrizione" DataValueField="id">
            <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
     &nbsp;<asp:Button ID="btnAggiungiExtra" runat="server" Text="Aggiungi" />
       
           </td>
          <td class="style54">
              <asp:Label ID="lblScontoGiorniExtra" runat="server" Text="Sconto giorni extra:" CssClass="testo_bold"></asp:Label>
              <boxover:boxover ID="BoxOver1" runat="server" 
    body='Sconto applicato sul costo extra da pagare estendendo i giorni di noleggio rispetto a quelli di prenotazione. Viene applicato unicamente se la tariffa venduta al momento della prenotazione non � pi� valida oppure in caso di estensione di giorni di noleggio a carico del cliente per tariffe BROKER.' 
    controltovalidate="lblScontoGiorniExtra" header="Sconto Giorni Extra" 
    CssHeader="toolheader"  CssBody="toolbody"   />
          </td>
          <td>
            <asp:TextBox ID="txtScontoRack" runat="server" Width="32px" onKeyPress="return filterInputInt(event)"></asp:TextBox><b>&nbsp;%</b>
            <asp:Label ID="lblMxScontoRack" runat="server" Text="Applicato il MASSIMO SCONTO." CssClass="testo_bold" ForeColor="Red" Visible="false"></asp:Label>
          </td>
          <td>
              &nbsp;</td>
      </tr>
      <tr>
          <td valign="top" colspan="4" align="left">
                <asp:Label ID="lblPrepagata1" runat="server" Text="Prenotazione Prepagata" Font-Size="16px" CssClass="testo_bold" ForeColor="Red" Visible="false"></asp:Label>
          &nbsp;<%--<asp:Label ID="lblPrepagata2" runat="server" Text="Da pagare:" Font-Size="16px" CssClass="testo_bold" Visible="false"></asp:Label>--%>        
                <asp:Label ID="lblDaIncassare" runat="server" ForeColor="Blue" CssClass="testo_bold" Visible="false" Font-Size="16px"></asp:Label>
                <asp:Label ID="lblEuroDaIncassare" runat="server" ForeColor="Blue" Text="�" CssClass="testo_bold" Visible="false" Font-Size="16px"></asp:Label>
          </td>
          <td valign="top" colspan="4" align="right">
              <asp:Label ID="lblMinGiorniNolo" runat="server" CssClass="testo_bold"></asp:Label>
             <asp:Button ID="btnStampaTKm" runat="server" Text="Vedi Valore Tariffa" UseSubmitBehavior="false" />
             <asp:Button ID="btnStampaCondizioni" runat="server" Text="Vedi Condizioni" UseSubmitBehavior="false" />
          </td>
          
      </tr>
      <tr>
          <td valign="top" colspan="8" align="left">
                <asp:Label ID="lblTestoDaPreautorizzare" runat="server" Text="Da Preautorizzare: " Font-Size="16px" CssClass="testo_bold" Visible="false"></asp:Label>
                <asp:DropDownList ID="lblDaPreautorizzare" runat="server" 
                    AppendDataBoundItems="True">
                </asp:DropDownList>
                <asp:Label ID="lblEuroDaPreautorizzare" runat="server" ForeColor="Blue" Text="�" CssClass="testo_bold" Visible="false" Font-Size="16px"></asp:Label>
          &nbsp;&nbsp;
                <asp:Label ID="lblTestoDaPrenotazione" runat="server" Text="Differenza da pren.: " Font-Size="16px" CssClass="testo_bold" Visible="false"></asp:Label>    
                <asp:Label ID="lblDifferenzaDaPrenotazione" runat="server" ForeColor="Blue" CssClass="testo_bold" Visible="false" Font-Size="16px"></asp:Label>
                <asp:Label ID="lblEuroDaPrenotazione" runat="server" ForeColor="Blue" Text="�" CssClass="testo_bold" Visible="false" Font-Size="16px"></asp:Label>
                &nbsp;&nbsp;
                </td>
      </tr>
      <tr>
          <td valign="top" colspan="8" align="left">
                &nbsp;</td>
      </tr>
  </table>
  
  <table style="border:4px solid #444; border-top:0px;" border="0" cellspacing="0" cellpadding="0" width="1024px" >
    <tr>
          <td align="left" width="70%" valign="top" style="width: 100%">
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
                             <asp:Label ID="Label50" runat="server" Text='Penalit�' CssClass="testo_bold" />
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
                              <boxover:BoxOver ID="BoxOver1" runat="server" body='<%# Eval("nome_costo") & " - " & Eval("descrizione_lunga") %>' controltovalidate="nome_costo" header="Descrizione" CssHeader="toolheader"  CssBody="toolbody"   />
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
                              
                              <asp:Label ID="id_penalita_da_addebitare" runat="server" Text='<%# Eval("id_penalita_da_addebitare") %>' Visible="false" />
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
              </td>
           </tr>
  </table>
  </div>
  <table runat="server" id="table5" style="border:4px solid #444; border-bottom:0px;" border="0" cellspacing="2" cellpadding="2" width="1024px">
      <tr>
        <td align="left" style="color: #FFFFFF;background-color:#444;" >
           <asp:Label ID="Label54" runat="server" Text="Note - Stampate su contratto" CssClass="testo_titolo"></asp:Label>
        </td>
      </tr>
      <tr>
        <td>
          
            <asp:TextBox ID="txtNoteContratto" runat="server" Height="70px" Width="1000px" TextMode="MultiLine"></asp:TextBox>
          
        </td>
      </tr>
      <tr>
        <td align="left" style="color: #FFFFFF;background-color:#444;" runat="server" id="intestazione_note" >
           <asp:Label ID="Label58" runat="server" Text="Note - Uso Interno" CssClass="testo_titolo"></asp:Label>
        </td>
      </tr>
      <tr>
        <td>
          
            <asp:TextBox ID="txtNoteInterne" runat="server" Height="70px" Width="1000px" TextMode="MultiLine"></asp:TextBox>
          
        </td>
      </tr>
      <tr>
        <td align="center">
            <uc1:gestione_note ID="gestione_note_contratto" runat="server" visible="false" />
        </td>
      </tr>
   </table>
  
  <table runat="server" id="dettagli_da_prenotazione" style="border:4px solid #444; border-top:0px;" border="0" cellspacing="2" cellpadding="2" width="1024px" >
      <tr>
         <td align="left" style="color: #FFFFFF;background-color:#444;" colspan="6">
             <asp:Label ID="Label17" runat="server" Text="Informazioni da prenotazione" CssClass="testo_titolo"></asp:Label>
             <asp:Label ID="id_conducente_prenotazione" runat="server" Visible="false"></asp:Label>
  
         </td>
      </tr>
      <tr>
        <td class="style27">
           <asp:Label ID="Label27" runat="server" Text="Cognome" CssClass="testo_bold"></asp:Label>
        </td>
        <td class="style28">
            <asp:TextBox ID="txtCognomeConducente" runat="server" Width="170px" ReadOnly="true"></asp:TextBox> 
        </td>
        <td class="style16">
           <asp:Label ID="Label26" runat="server" Text="Nome" CssClass="testo_bold" ></asp:Label>
        </td>
        <td class="style16">
            <asp:TextBox ID="txtNomeConducente" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
        </td>
        <td class="style15">
          <asp:Label ID="Label28" runat="server" Text="E-Mail" CssClass="testo_bold"></asp:Label>
        </td>
        <td class="style15">
            <asp:TextBox ID="txtMailConducente" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
        </td>
      </tr>
    <tr>
        <td class="style17">
          
        <asp:Label ID="Label129" runat="server" Text="Gruppo Prenotato" CssClass="testo_bold"></asp:Label>
          
        </td>
        <td class="style28">
          
            <asp:DropDownList ID="dropGruppoPrenotato" runat="server" Enabled="false"
                DataSourceID="sqlGruppiAuto" DataTextField="cod_gruppo" 
                DataValueField="id_gruppo" AppendDataBoundItems="True">
                <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
            </asp:DropDownList>
          
        </td>
        <td class="style16"> 
          
        <asp:Label ID="Label29" runat="server" Text="Gruppo da Consegnare" CssClass="testo_bold"></asp:Label>
          
        </td>
        <td class="style16"> 
          
            <asp:DropDownList ID="dropGruppoDaConsegnare" runat="server" Enabled="false"
                DataSourceID="sqlGruppiAuto" DataTextField="cod_gruppo" 
                DataValueField="id_gruppo" AppendDataBoundItems="True">
                <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
            </asp:DropDownList>
          
        </td>
        <td class="style15">
          <asp:Label ID="Label30" runat="server" Text="Numero Volo (Uscita)" CssClass="testo_bold"></asp:Label>
        </td>
        <td class="style15">
          
            <asp:TextBox ID="txtVoloOut" runat="server" Width="100px" MaxLength="10" ReadOnly="true"></asp:TextBox>
          
        </td>
      </tr>
      <tr>
        <td class="style24">
          <asp:Label ID="Label128" runat="server" Text="Numero Volo (Rientro)" 
                CssClass="testo_bold"></asp:Label>  
        </td>
        <td class="style24">
            <asp:TextBox ID="txtVoloPr" runat="server" Width="100px" MaxLength="10" ReadOnly="true"></asp:TextBox>
        </td>
        <td class="style25">
          <asp:Label ID="Label40" runat="server" Text="Numero Riferimento" CssClass="testo_bold"></asp:Label>
        </td>
        <td class="style25">
            <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                      TargetControlID="txtDataDiNascita" 
                ID="txtCercaPickUpDa0_CalendarExtender1">
            </ajaxtoolkit:CalendarExtender>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                      CultureDatePlaceholder="" CultureTimePlaceholder="" 
                      CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                      CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                      CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtDataDiNascita" 
                      ID="txtCercaPickUpDa0_MaskedEditExtender1">
            </ajaxtoolkit:MaskedEditExtender>--%><asp:TextBox ID="txtRiferimentoTO" runat="server" Width="100px" MaxLength="15" ReadOnly="true"></asp:TextBox>
         </td>
        <td class="style26">
          
           <asp:Label ID="Label64" runat="server" Text="Data di Nascita" CssClass="testo_bold"></asp:Label>  
          </td>
        <td class="style26">
           <asp:TextBox runat="server" Width="70px" ID="txtDataDiNascita" ReadOnly="true"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td class="style24">
          
          <asp:Label ID="Label124" runat="server" Text="Indirizzo" CssClass="testo_bold"></asp:Label>
        </td>
        <td class="style24">
            <asp:TextBox ID="txtIndirizzoConducente" runat="server" Width="170px"  ReadOnly="true"></asp:TextBox>
          </td>
        <td class="style25">
          
          <asp:Label ID="Label125" runat="server" Text="Riferimento Tel." CssClass="testo_bold"></asp:Label>
          </td>
        <td class="style25">
            <asp:TextBox ID="txtRifTel" runat="server" MaxLength="30" Width="100px"></asp:TextBox>
          </td>
        <td class="style26">
          
            &nbsp;</td>
        <td class="style26">
            &nbsp;</td>
      </tr>
      <tr>
          <td valign="top" colspan="6">
              <uc1:gestione_note ID="gestione_note" runat="server" />
          </td>
      </tr>
  </table>
  <table runat="server" id="dettagli_da_preventivo" style="border:4px solid #444; border-top:0px;" border="0" cellspacing="2" cellpadding="2" width="1024px" >
      <tr>
         <td align="left" style="color: #FFFFFF;background-color:#444;" class="style1" 
              colspan="6">
             <asp:Label ID="Label31" runat="server" Text="Informazioni da preventivo" CssClass="testo_titolo"></asp:Label>
         </td>
      </tr>
      <tr>
        <td class="style27">
           <asp:Label ID="Label32" runat="server" Text="Cognome" CssClass="testo_bold"></asp:Label>
        </td>
        <td class="style28">
            <asp:TextBox ID="txtCognomeConducentePreventivo" runat="server" Width="170px" ReadOnly="true"></asp:TextBox> 
        </td>
        <td class="style52">
           <asp:Label ID="Label33" runat="server" Text="Nome" CssClass="testo_bold" ></asp:Label>
        </td>
        <td class="style16">
            <asp:TextBox ID="txtNomeConducentePreventivo" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
        </td>
        <td class="style53">
          <asp:Label ID="Label39" runat="server" Text="E-Mail" CssClass="testo_bold"></asp:Label>
        </td>
        <td class="style15">
            <asp:TextBox ID="txtEmailConducentePreventivo" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
        </td>
      </tr>
    </table>
  <div runat="server" id="tab_dettagli_pagamento" visible="true">
<table style="border:4px solid #444; border-top:0px;" border="0" cellspacing="1" cellpadding="1" width="1024px" >
  <tr>
     <td align="left" style="color: #FFFFFF;background-color:#444;" colspan="2">
         <asp:Label ID="Label6" runat="server" Text="Dettagli di pagamento" CssClass="testo_titolo"></asp:Label>
     </td>
  </tr>
  <tr runat="server" id="riga_pagamento_pos" visible="false">
    <td colspan="2">
       <table style="border-top:0px;" border="0" cellspacing="1" cellpadding="1" width="100%">
         <tr>
           <td class="style40">
             <asp:Label ID="funzione" runat="server" Text="Funzione" CssClass="testo_bold" />
           </td>
           <td class="style34">
             <asp:Label ID="Label25" runat="server" Text="Staz." CssClass="testo_bold" />
           </td>
           <td class="style5">
             <asp:Label ID="Label8" runat="server" Text="Operatore" CssClass="testo_bold" />
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
           <td class="style40">
      
             <asp:Label ID="Label66" runat="server" Text="Terminal ID." CssClass="testo_bold" />
      
           </td>
           <td class="style34">
      
             <asp:Label ID="Label38" runat="server" Text="Nr. Aut." CssClass="testo_bold" />
      
           </td>
           <td class="style5">
      
             <asp:Label ID="Label69" runat="server" Text="Nr. Preaut." CssClass="testo_bold" />
      
             </td>
           <td class="style38">
      
             <asp:Label ID="Label68" runat="server" Text="Nr. Batch" CssClass="testo_bold" />
      
             </td>
           <td class="style36">
      
             <asp:Label ID="Label70" runat="server" Text="Scadenza Preaut." CssClass="testo_bold" />
      
             </td>
           <td class="style37">
      
             <asp:Label ID="Label71" runat="server" Text="Acquire Id" CssClass="testo_bold" />
             </td>
           <td class="style39">
             <asp:Label ID="Label72" runat="server" Text="Transation Type" CssClass="testo_bold" />
             </td>
           <td>
               &nbsp;</td>
         </tr>
         <tr>
           <td class="style40">
      
                 <asp:TextBox ID="txtPOS_TerminalID" runat="server" Width="84px" ReadOnly="true"></asp:TextBox>
      
           </td>
           <td class="style34">
      
                 <asp:TextBox ID="txtPOS_NrAut" runat="server" Width="65px" ReadOnly="true"></asp:TextBox>
      
           </td>
           <td class="style5">
      
                 <asp:TextBox ID="txtPOS_NrPreaut" runat="server" Width="158px" ReadOnly="true"></asp:TextBox>
      
             </td>
           <td class="style38">
      
                 <asp:TextBox ID="txtPOS_BATCH" runat="server" Width="52px" ReadOnly="true"></asp:TextBox>
      
             </td>
           <td class="style36">
      
                 <asp:TextBox ID="txtPOS_ScadenzaPreaut" runat="server" Width="140px" 
                     ReadOnly="true"></asp:TextBox>
             </td>
           <td class="style37">
      
                 <asp:TextBox ID="txtPOS_AcquireID" runat="server" Width="100px" 
                     ReadOnly="true"></asp:TextBox>
             </td>
           <td class="style39">
                 <asp:TextBox ID="txtPOS_TransationType" runat="server" Width="86px" ReadOnly="true"></asp:TextBox>
             </td>
           <td>
                 &nbsp;</td>
         </tr>
         <tr>
           <td class="style40">
      
             <asp:Label ID="Label73" runat="server" Text="Action Code" CssClass="testo_bold" />
      
           </td>
           <td class="style34">
      
             <asp:Label ID="Label74" runat="server" Text="Stato" CssClass="testo_bold" />
      
             </td>
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
           <td class="style40">
      
                 <asp:TextBox ID="txtPOS_ActionCode" runat="server" Width="65px" ReadOnly="true"></asp:TextBox>
      
             </td>
           <td class="style34">
      
                 <asp:TextBox ID="txtPOS_Stato" runat="server" Width="74px" 
                     ReadOnly="true"></asp:TextBox>
      
             </td>
           <td  class="style5" >
      
             </td>
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
           <td class="style33" colspan="8" align="center">
      
               <asp:Button ID="btnAnnulla7" runat="server" Text="Chiudi" />

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
                                    <th id="Th1" runat="server" align="left">
                                        <asp:Label ID="Label22" runat="server" Text="Tipo Op." CssClass="testo_titolo"></asp:Label>
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
</div>
  <div runat="server" id="tab_pagamento" visible="false" >
      <uc1:scambio_importo ID="Scambio_Importo1" runat="server"  />
  </div>
    <asp:Label ID="idContratto" runat="server" visible="false"></asp:Label>
    
    <asp:Label ID="contratto_num" runat="server" visible="false"></asp:Label>
    
    <asp:Label ID="numCalcolo" runat="server" visible="False"></asp:Label>
    <asp:Label ID="numCrv" runat="server" visible="false"></asp:Label>
    <asp:Label ID="idPrenotazione" runat="server" visible="false"></asp:Label>
    <asp:Label ID="idPreventivo" runat="server" visible="false"></asp:Label>
    
    <asp:Label ID="livello_accesso_sconto" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="livello_accesso_omaggi" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="livello_accesso_dettaglio_pos" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="livello_accesso_broker" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="livello_accesso_annulla_quick" runat="server" Visible="false"></asp:Label>
    
    <asp:Label ID="provenienza" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="statoContratto" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="statoModificaContratto" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="tariffa_broker" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="ultimo_gruppo" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="intestazione_informativa_da_visualizzare" runat="server" Visible="false"></asp:Label>
    
    <asp:Label ID="ditta_non_modificabile" runat="server" Visible="false"></asp:Label>
    
    <asp:Label ID="totale_prenotazione" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="prenotazione_prepagata" runat="server" Visible="false"></asp:Label>
    <%--<asp:Label ID="importo_prepagato" runat="server" Visible="false"></asp:Label>--%>
    
    <asp:Label ID="gruppo_da_calcolare_originale" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="id_stazione_drop_off_prenotazione" runat="server" Visible="false"></asp:Label>  
    <asp:Label ID="id_gruppo_auto_selezionata" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="livello_accesso_modifica_targa" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="livello_accesso_admin" runat="server" Visible="false"></asp:Label>
    
    <asp:Label ID="data_pick_up_attuale" runat="server" Visible="false"></asp:Label>
     <asp:Label ID="orario_pick_up_attuale" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="data_drop_off_attuale" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="id_stazione_drop_off_attuale" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="orario_drop_off_attuale" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="numero_giorni_attuale" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="gruppo_calcolato_attuale" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="id_tariffa_attuale" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="giorno_extra_omaggiato_attuale" runat="server" Visible="false"></asp:Label>
    
    <asp:Label ID="mod_data_drop_off" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="mod_id_stazione_drop_off" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="mod_orario_drop_off" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="mod_numero_giorni" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="mod_gruppo_calcolato" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="mod_id_tariffa" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="mod_giorno_extra_omaggiato" runat="server" Visible="false"></asp:Label>
    
    <asp:Label ID="rack_utilizzata" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="a_carico_del_broker" runat="server" Text="" Visible="false"></asp:Label>

    <asp:Label ID="lb_gestione_rds" runat="server" Text='' Visible="false"></asp:Label>
    <asp:Label ID="lb_id_evento" runat="server" Text='' Visible="false"></asp:Label>
    

    <asp:Label ID="lb_tipo_pagamento" runat="server" Text='' Visible="false"></asp:Label>
    
    <asp:Label ID="complimentary" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="full_credit" runat="server" Visible="false"></asp:Label>
  
  <asp:SqlDataSource ID="sqlStazioni" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT stazioni.id, (codice + ' ' + nome_stazione) As stazione FROM stazioni WITH(NOLOCK) ORDER BY nome_stazione">
  </asp:SqlDataSource>
    <asp:Label ID="penali2" runat="server"></asp:Label>
  <asp:SqlDataSource ID="sqlTipoClienti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT LEFT(descrizione,30) As descrizione, [id] FROM [clienti_tipologia] WITH(NOLOCK) ORDER BY [descrizione]">
  </asp:SqlDataSource>
  <asp:SqlDataSource ID="sqlGruppiAuto" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT gruppi.id_gruppo, gruppi.cod_gruppo FROM gruppi WITH(NOLOCK) WHERE attivo='1' ORDER BY cod_gruppo">
  </asp:SqlDataSource>
  
  <asp:SqlDataSource ID="sqlWarning" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT warning, tipo FROM contratti_warning WITH(NOLOCK) WHERE (([id_documento] = @id_contratto) AND ([num_calcolo] = @num_calcolo)) ORDER BY tipo, warning">
        <SelectParameters>
            <asp:ControlParameter ControlID="idContratto" Name="id_contratto" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
  </asp:SqlDataSource>
  
  <%--<asp:SqlDataSource ID="sqlContrattiCosti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT contratti_costi.id, gruppi.id_gruppo, gruppi.cod_gruppo As gruppo, contratti_costi.id_elemento, contratti_costi.nome_costo, condizioni_elementi.descrizione_lunga, ISNULL((imponibile+iva_imponibile),0) As valore_costo, ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, ISNULL(imponibile_scontato,0) AS imponibile_scontato, ISNULL(imponibile_onere,0) AS imponibile_onere, ISNULL((iva_imponibile_scontato + ISNULL(iva_onere,0)),0) As iva, ISNULL(contratti_costi.aliquota_iva,0) As aliquota_iva, id_a_carico_di, contratti_costi.obbligatorio, contratti_costi.id_metodo_stampa, ISNULL(contratti_costi.selezionato,'False') As selezionato, ISNULL(contratti_costi.omaggiato,'False') As omaggiato, ISNULL(contratti_costi.omaggiabile,'False') As omaggiabile,ISNULL(contratti_costi.acquistabile_nolo_in_corso,'False') As acquistabile_nolo_in_corso, ISNULL(contratti_costi.franchigia_attiva,'False') As franchigia_attiva, id_unita_misura, packed, qta, data_aggiunta_nolo_in_corso, condizioni_elementi.tipologia_franchigia, condizioni_elementi.servizio_rifornimento_tolleranza, contratti_costi.num_elemento, condizioni_elementi.is_gps FROM contratti_costi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON contratti_costi.id_gruppo=gruppi.id_gruppo LEFT JOIN condizioni_elementi WITH(NOLOCK) ON contratti_costi.id_elemento=condizioni_elementi.id WHERE ((id_documento = @id_contratto) AND (num_calcolo = @num_calcolo_contratto)) AND ordine_stampa<>'5' AND ISNULL(franchigia_attiva,'1')='1' ORDER BY id_gruppo, secondo_ordine_stampa, ordine_stampa,ISNULL(condizioni_elementi.ordinamento_vis,0) DESC, nome_costo ">
        <SelectParameters>
            <asp:ControlParameter ControlID="idContratto" Name="id_contratto" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo_contratto" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
  </asp:SqlDataSource>--%>   <asp:SqlDataSource ID="sqlContrattiCosti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT contratti_costi.id, GRUPPI.ID_gruppo, GRUPPI.cod_gruppo AS gruppo, contratti_costi.id_elemento, contratti_costi.nome_costo, contratti_costi.prepagato, condizioni_elementi.descrizione_lunga, ISNULL((contratti_costi.imponibile + contratti_costi.iva_imponibile + ISNULL(contratti_costi.imponibile_onere, 0)  + ISNULL(contratti_costi.iva_onere, 0)) - (ISNULL(contratti_costi.imponibile_scontato_prepagato, 0) + ISNULL(contratti_costi.iva_imponibile_scontato_prepagato, 0)  + ISNULL(contratti_costi.imponibile_onere_prepagato, 0) + ISNULL(contratti_costi.iva_onere_prepagato, 0)), 0) AS valore_costo,  ISNULL(contratti_costi.commissioni_imponibile_originale, 0) + ISNULL(contratti_costi.commissioni_iva_originale, 0) AS commissioni,  ISNULL(contratti_costi.imponibile_scontato_prepagato, 0) + ISNULL(contratti_costi.iva_imponibile_scontato_prepagato, 0)  + ISNULL(contratti_costi.imponibile_onere_prepagato, 0) + ISNULL(contratti_costi.iva_onere_prepagato, 0) AS valore_prepagato,  ISNULL(contratti_costi.imponibile + contratti_costi.iva_imponibile - contratti_costi.imponibile_scontato - contratti_costi.iva_imponibile_scontato, 0) AS sconto,  ISNULL(contratti_costi.imponibile_scontato, 0) - ISNULL(contratti_costi.imponibile_scontato_prepagato, 0) AS imponibile_scontato,  ISNULL(contratti_costi.imponibile_onere, 0) - ISNULL(contratti_costi.imponibile_onere_prepagato, 0) AS imponibile_onere,   ISNULL((contratti_costi.iva_imponibile_scontato + ISNULL(contratti_costi.iva_onere, 0)) - (ISNULL(contratti_costi.iva_imponibile_scontato_prepagato, 0)   + ISNULL(contratti_costi.iva_onere_prepagato, 0)), 0) AS iva, ISNULL(contratti_costi.aliquota_iva, 0) AS aliquota_iva, contratti_costi.id_a_carico_di,  contratti_costi.obbligatorio, contratti_costi.id_metodo_stampa, ISNULL(contratti_costi.selezionato, 'False') AS selezionato, ISNULL(contratti_costi.omaggiato, 'False')  AS omaggiato, (CASE WHEN prepagato = '1' THEN 'False' ELSE ISNULL(contratti_costi.omaggiabile, 'False') END) AS omaggiabile,  ISNULL(contratti_costi.acquistabile_nolo_in_corso, 'False') AS acquistabile_nolo_in_corso, ISNULL(contratti_costi.franchigia_attiva, 'False') AS franchigia_attiva,  contratti_costi.id_unita_misura, contratti_costi.packed, contratti_costi.qta, contratti_costi.data_aggiunta_nolo_in_corso, condizioni_elementi.tipologia_franchigia,   condizioni_elementi.tipologia, condizioni_elementi.servizio_rifornimento_tolleranza, contratti_costi.num_elemento, condizioni_elementi.is_gps,  condizioni_elementi.id_elemento_da_addebitare_se_perso As id_penalita_da_addebitare FROM contratti_costi WITH(NOLOCK) INNER JOIN  GRUPPI WITH(NOLOCK) ON contratti_costi.id_gruppo = GRUPPI.ID_gruppo LEFT JOIN condizioni_elementi WITH(NOLOCK) ON contratti_costi.id_elemento=condizioni_elementi.id WHERE    (contratti_costi.id_documento = @id_contratto) AND (contratti_costi.num_calcolo = @num_calcolo_contratto) AND (contratti_costi.ordine_stampa <> '5') AND (ISNULL(contratti_costi.franchigia_attiva, '1') = '1') AND (contratti_costi.valore_percentuale IS NULL) OR (contratti_costi.id_documento = @id_contratto) AND (contratti_costi.num_calcolo = @num_calcolo_contratto) AND (contratti_costi.ordine_stampa <> '5') AND  (ISNULL(contratti_costi.franchigia_attiva, '1') = '1') AND (NOT (contratti_costi.ordine_stampa <> '2')) ORDER BY contratti_costi.id_gruppo, contratti_costi.secondo_ordine_stampa, contratti_costi.ordine_stampa, ISNULL(condizioni_elementi.ordinamento_vis, 0) DESC,  contratti_costi.nome_costo ">
        <SelectParameters>
            <asp:ControlParameter ControlID="idContratto" Name="id_contratto" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo_contratto" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
  </asp:SqlDataSource>
 
  
  <asp:SqlDataSource ID="sqlElementiExtra" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT condizioni_elementi.id, condizioni_elementi.descrizione FROM condizioni_elementi WITH(NOLOCK) WHERE id='0'">
  </asp:SqlDataSource>
  <asp:SqlDataSource ID="sqlTariffeGeneriche" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, codice FROM tariffe WITH(NOLOCK) WHERE id='0'">
  </asp:SqlDataSource>    
  
    <asp:SqlDataSource ID="sqlTariffeParticolari" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, codice FROM tariffe WITH(NOLOCK) WHERE id='0'"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlDettagliPagamento" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT POS_Funzioni.funzione, PAGAMENTI_EXTRA.id_pos_funzioni_ares, PAGAMENTI_EXTRA.DATA_OPERAZIONE, PAGAMENTI_EXTRA.PER_IMPORTO, PAGAMENTI_EXTRA.ID_CTR, PAGAMENTI_EXTRA.preaut_aperta, PAGAMENTI_EXTRA.operazione_stornata, (CASE WHEN N_CONTRATTO_RIF IS NULL THEN 'PREN ' ELSE 'RA ' END) AS provenienza FROM PAGAMENTI_EXTRA WITH(NOLOCK) INNER JOIN POS_Funzioni WITH(NOLOCK) ON PAGAMENTI_EXTRA.id_pos_funzioni_ares=POS_funzioni.id WHERE (N_PREN_RIF = @N_PREN_RIF OR N_CONTRATTO_RIF = @N_CONTRATTO_RIF) ORDER BY PAGAMENTI_EXTRA.DATA_OPERAZIONE DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblNumPren" Name="N_PREN_RIF" 
                PropertyName="Text" Type="Int32"  ConvertEmptyStringToNull="true" />
            <asp:ControlParameter ControlID="lblNumContratto" Name="N_CONTRATTO_RIF" 
                PropertyName="Text" Type="Int32" ConvertEmptyStringToNull="true" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlModificheContratto" runat="server" ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT contratti.id, DATEPART(Ms, data_creazione) As milli, contratti.num_calcolo, (stazioni.codice + ' - ' + stazioni.nome_stazione) As stazione_presunto_rientro, giorni, data_presunto_rientro, (operatori.cognome + ' ' + operatori.nome) As operatore, data_ultima_modifica, contratti.targa FROM contratti WITH(NOLOCK) INNER JOIN stazioni WITH(NOLOCK) ON contratti.id_stazione_presunto_rientro=stazioni.id LEFT JOIN operatori WITH(NOLOCK) ON contratti.id_operatore_ultima_modifica=operatori.id WHERE num_contratto=@N_CONTRATTO_RIF AND status='2' ORDER BY num_calcolo ASC">
     <SelectParameters> 
        <asp:ControlParameter ControlID="lblNumContratto" Name="N_CONTRATTO_RIF" 
                PropertyName="Text" Type="String"  ConvertEmptyStringToNull="true" />
       <%-- <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo" 
                PropertyName="Text" Type="String" ConvertEmptyStringToNull="true" />--%>
     </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlScegliVeicolo" runat="server" ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT veicoli.id, veicoli.targa, modelli.descrizione As modello, veicoli.km_attuali, veicoli.serbatoio_attuale, modelli.capacita_serbatoio, alimentazione.descrizione As carburante, gruppi.cod_gruppo, gruppi.id_gruppo FROM veicoli WITH(NOLOCK) INNER JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello INNER JOIN gruppi ON modelli.id_gruppo=gruppi.id_gruppo LEFT JOIN alimentazione WITH(NOLOCK) ON modelli.tipoCarburante=alimentazione.id WHERE veicoli.disponibile_nolo='1' AND veicoli.id_stazione=@id_scegli_auto_stazione ORDER BY cod_gruppo">
     <SelectParameters> 
        <asp:ControlParameter ControlID="id_scegli_auto_stazione" Name="id_scegli_auto_stazione" 
                PropertyName="Text" Type="String"  ConvertEmptyStringToNull="true" />
     </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlListaCrv" runat="server" ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT contratti_crv_veicoli.id, contratti_crv_veicoli.id_veicolo, contratti_crv_veicoli.num_crv, veicoli.targa, modelli.descrizione As modello, contratti_crv_veicoli.km_uscita, contratti_crv_veicoli.serbatoio_uscita, contratti_crv_veicoli.km_rientro, contratti_crv_veicoli.serbatoio_rientro, contratti_crv_veicoli.data_uscita, contratti_crv_veicoli.data_sostituzione, modelli.capacita_serbatoio, alimentazione.descrizione As carburante, contratti_crv_veicoli.check_in_effettuato FROM contratti_crv_veicoli WITH(NOLOCK) INNER JOIN veicoli WITH(NOLOCK) ON contratti_crv_veicoli.id_veicolo=veicoli.id INNER JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello LEFT JOIN alimentazione WITH(NOLOCK) ON modelli.tipoCarburante=alimentazione.id WHERE num_contratto=@N_CONTRATTO_RIF ORDER BY num_crv ASC">
     <SelectParameters> 
        <asp:ControlParameter ControlID="lblNumContratto" Name="N_CONTRATTO_RIF" PropertyName="Text" Type="Int32" ConvertEmptyStringToNull="true" />
     </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="SqlDSConducenti" runat="server"
     ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"         
      SelectCommand="select id,nome + ' ' + cognome as nominativo from operatori WITH(NOLOCK) order by nominativo">
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlFornitori" runat="server" ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"         
       SelectCommand="SELECT id, descrizione FROM alimentazione_fornitori_x_stazione WHERE id_stazione=@id_stazione ">
         <SelectParameters>
                    <asp:ControlParameter ControlID="dropStazionePickUp" Name="id_stazione" PropertyName="SelectedValue" Type="Int32" />
         </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlFontiCommissionabili" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT rag_soc, id  FROM fonti_commissionabili WITH(NOLOCK) WHERE attiva='1' ORDER BY rag_soc"></asp:SqlDataSource>
    </asp:Content>
  

