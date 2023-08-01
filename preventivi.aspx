<%@ Page Language="VB" AutoEventWireup="false" CodeFile="preventivi.aspx.vb" Inherits="preventivi" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<%@ Register Src="/anagrafica/anagrafica_conducenti.ascx" TagName="anagrafica_conducenti" TagPrefix="uc1" %>
<%@ Register Src="/anagrafica/anagrafica_ditte.ascx" TagName="anagrafica_ditte" TagPrefix="uc1" %>
<%@ Register Src="/gestione_danni/gestione_note.ascx" TagName="gestione_note" TagPrefix="uc1" %>

<%--'salvo 08.03.2023 10.55--%>

<%--<%@ Register Src="/tabelle_pos/scambio_importo.ascx" TagName="scambio_importo" TagPrefix="uc1" %>--%>
<%@ Register Assembly="BoxOver" Namespace="BoxOver" TagPrefix="boxover" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ARES - Sicily Rent Car</title>
    <link rel="icon" href="/img/favicon.ico" />  
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />

    <%--CALENDAR--%>
     <script type="text/javascript" src="calendar/calendar.js"></script>
     <style type="text/css">  
        .CalendarCSS  
        {background-color: #3a6ea5;  color:Snow;}  
    </style>  
    
    <%--start multiple submit 23.02.2022 --%>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script type="text/javascript">

        var isSubmitted = false;

        function preventMultipleSubmissions(ele,e) {
            if (!isSubmitted) {
                <%--$('#<%=btnSalvaPrenotazione.ClientID %>').val('salvataggio in corso...');
                $('#<%=btnSalvaPrenotazione.ClientID %>').css('cursor', 'wait');--%>
                $('body').css('cursor', 'wait');
                $(ele).val('salvataggio in corso...');
                $(ele).css('cursor', 'wait');
                isSubmitted = true;
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <%--end multiple submit--%>



        <script type="text/javascript"  src="/lytebox.js"></script>
     <link rel="stylesheet" href="/lytebox.css" type="text/css" media="screen" />
    
    <%--<script type="text/javascript" language="javascript">
        function onSelectedStartDate(sender, args) {
            $find("endDate").set_selectedDate(sender.get_selectedDate());
        }
        </script>--%>
        <script type="text/javascript" >
            function onSelectedStartDate(sender, args) {
               // $find("endDate").set_selectedDate(sender.get_selectedDate());
            }
        </script>
  <%--<script type="text/javascript" language="javascript">
      function controllo_data(stringa) {
          var espressione = /^[0-9]{2}\/[0-9]{2}\/[0-9]{4}$/;
          if (!espressione.test(stringa)) {
              return false;
          } else {
              anno = parseInt(stringa.substr(6), 10);
              mese = parseInt(stringa.substr(3, 2), 10);
              giorno = parseInt(stringa.substr(0, 2), 10);

              var data = new Date(anno, mese - 1, giorno);
              if (data.getFullYear() == anno && data.getMonth() + 1 == mese && data.getDate() == giorno) {
                  return true;
              } else {
                  return false;
              }
          }
      }

      function confronta_data(data1, data2) {
          data1str = data1.substr(6) + data1.substr(3, 2) + data1.substr(0, 2);
          data2str = data2.substr(6) + data2.substr(3, 2) + data2.substr(0, 2);
          if (data2str - data1str < 0) {
              return false;
          } else {
              return true;
          }
      }

    function giorni_differenza(data1,data2){
	        if(!controllo_data(data1) || !controllo_data(data2)){
		        return -1;
	        }

	        if(!confronta_data(data1,data2)){
		        return -1;
	        }

	        anno1 = parseInt(data1.substr(6),10);
	        mese1 = parseInt(data1.substr(3, 2),10);
	        giorno1 = parseInt(data1.substr(0, 2),10);
     
	        anno2 = parseInt(data2.substr(6),10);
	        mese2 = parseInt(data2.substr(3, 2),10);
	        giorno2 = parseInt(data2.substr(0, 2),10);

            var dataok1=new Date(anno1, mese1-1, giorno1);
	        var dataok2=new Date(anno2, mese2-1, giorno2);
	
	        differenza = dataok2-dataok1;    
	        giorni_differenza = new String(differenza/86400000);

	        return giorni_differenza;
      }
</script>--%>

    <script type="text/javascript">

        <%--function cktipocliente() {
            var dtc = document.getElementById('<%=dropTipoCliente.ClientID%>').value;
            if (dtc == "0") {
                alert('Selezionare un tipo di cliente');
                return false;
            } else {
                //return true;
            }

        }--%>

        function ckCampi() {



        <%--    var ddl_gruppi = document.getElementById('<%=listGruppi.ClientID%>');

            alert(ddl_gruppi.);
            return false;--%>

            var dtg = document.getElementById('<%=dropTariffeGeneriche.ClientID%>').value;
            var dtp = document.getElementById('<%=dropTariffeParticolari.ClientID%>').value;
            //var dfc = document.getElementById('<%=dropFonteCommissionabile.ClientID%>');
            //var dtc = document.getElementById('<%=dropTipoCommissione.ClientID%>');

            if (dtg == "0" && dtp=="0") {
                alert('Selezionare una tariffa generica oppure una tariffa particolare');
                //document.getElementById('<%=dropTariffeGeneriche.ClientID%>').focus();
                return false;
            } else {
                return true;
            }


        }



        function copia_valore(drop) {
            var valore = 0;
            valore = drop.options[drop.options.selectedIndex].value;

            var DropDownPosizione = document.getElementById('dropStazioneDropOff');

            for (i = 0; i < DropDownPosizione.options.length; i++) {
                if (DropDownPosizione.options[i].value == valore) {
                    DropDownPosizione.options[i].selected = true;
                    return false;
                }
            }

        }

        function copia_data() {
            document.getElementById('<%=txtAData.ClientID%>').value = document.getElementById('<%=txtDaData.ClientID%>').value;
        }



        function filterInputInt(evt) {
            var keyCode, Char, inputField;
            var filter = '0123456789';
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

        function filterInputDouble(evt) {
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
            if (Char == ',') {
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
    
    <link rel="StyleSheet" type="text/css" href="css/style.css" />
    <style type="text/css">
		ul {
			font-family:Arial, Verdana;
			font-size: 16px;
			margin: 0;
			padding: 0;
			list-style: none;
			/*width:1024px;*/
            height:40px;
            background-image:url('../img/bg_nav.jpg');
		}
		ul li {
			display: block;
			position: relative;
			float: left;
		}
		li ul { display: none; }
		ul li a {
			display: block;
			text-decoration: none;
			color: #FFFFFF;
			/*border-top: 1px solid #ffffff;*/
			padding: 5px 15px 5px 15px;
			/*margin-left: 1px;*/
			white-space: nowrap;
		}
		
		ul li a:hover { background: #617F8A; }
		li:hover ul { 
			display: block; 
			position: absolute;
		}
		li:hover li { 
			float: none;
			font-size: 14px;
		}
		li:hover a { background: #617F8A;color: #FFF; }
		li:hover li a:hover { background: #95A9B1; }

	</style>
	
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
            width: 100px;
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
            width: 130px;
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
        .style54
        {
            width: 190px;
        }

        .ddlist{
            height :20px;
        }

        .c_stazioni{
            width:160px;
        }

        .c_tariffa{
            width:340px;
        }
         .c_tariffa_prev{
            width:340px;
        }
        .c_prepagata{
            width:80px;
        }
        .c_targa{
            width:90px;
        }
        .c_tipocliente{
            width:130px;
        }
        .c_align{
            text-align:center;
        }
        .c_conducente{
            width:320px;
        }

        /*th{
            padding:2px;*/
            /*letter-spacing:1px;*/
        /*}*/
        /*td{
           padding:2px;
        }*/
        </style>    

        <script type="text/javascript" >
            
            function getDimensione() {
                var div_mio = document.getElementById('tab_cerca_tariffe');
                var tab_mio = document.getElementById('tabella_ricerca');

                //div_mio.setAttribute('height','auto');
                alert("[--- " + tab_mio.style.clientTop + "---]" + tab_mio.clientHeight);
            }

            function RiposizionaDiv() {
                var div_home = document.getElementById('div_home');
                var tab_ricerca = document.getElementById('tab_ricerca');
                var tab_cerca_tariffe = document.getElementById('tab_cerca_tariffe');

                var tab_preventivo = document.getElementById('tab_preventivo');
                var tab_prenotazioni = document.getElementById('tab_prenotazioni');

                var tab_pagamento = document.getElementById('tab_pagamento');
                var div_dettaglio_gruppi = document.getElementById('div_dettaglio_gruppi');

                var div_note = document.getElementById('div_note');
                

                if (tab_ricerca != null)
                    tab_ricerca.style.top = (div_home.clientHeight) + "px";

                if (tab_cerca_tariffe != null) {
                    tab_cerca_tariffe.style.top = (div_home.clientHeight) + "px";
                    
                    if (div_note != null) {
                        div_note.style.top = (div_home.clientHeight + tab_cerca_tariffe.clientHeight) + "px";
                    }

                    if (div_dettaglio_gruppi != null) {
                        div_dettaglio_gruppi.style.top = (div_home.clientHeight + tab_cerca_tariffe.clientHeight) + "px";
                    }

                    if (tab_preventivo != null) {
                        tab_preventivo.style.top = (div_home.clientHeight + tab_cerca_tariffe.clientHeight) + "px";
                    }

                    if (tab_prenotazioni != null) {
                        tab_prenotazioni.style.top = (div_home.clientHeight + tab_cerca_tariffe.clientHeight - 2) + "px";
                    }

                }

                

//                if (div_padding_superiore != null) {
//                    div_padding_superiore.style.top = (div_home.clientHeight) + "px";
//                }
                

//                var div_padding_superiore = document.getElementById('div_padding_superiore');
//                if (div_padding_superiore != null) {
//                    div_padding_superiore.style.top = tabella_ricerca.clientHeight + 100;
//                    //alert("div_padding_superiore.style.top: " + div_padding_superiore.style.top );
//                }
//                else {
//                    //alert("div_padding_superiore.style.top: NON valorizzato");
//                }
               
            }

        
        </script>
        
</head>
<body topmargin="0" onload="javascript:RiposizionaDiv();">
    <form id="form1" runat="server" >
       <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
<div id="wrapper">

<div id="div_home" style="position:fixed;margin-top:0px;padding-top:0px;z-index:100;">
        <table width="1024" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <!--<img src="img/top_70.jpg" border="0" alt="" title =""/>-->
                    <ul id="menu">
			<li><a href="default.aspx"><img src="punto_elenco.jpg"  border="0" alt="" title="" width="8" height="7" />&nbsp;&nbsp;Home</a></li>
		    <li><a href="LogOut.aspx"><img src="punto_elenco.jpg"  border="0" alt="" title="" width="8" height="7" />&nbsp;&nbsp;Log-Out</a></li> 
            <li><a href="" style="    position: absolute;top: 0;left: 650px;"><%= Request.Cookies("SicilyRentCar")("nome") & "&nbsp;" %></a></li> 
		</ul>
                </td>
            </tr>
        </table>
        
	  
	    <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
            <tr>
            <td colspan="8" align="left" style="color: #FFFFFF;background-color:#444;" class="style1">
                <asp:Label ID="Label14" runat="server" Text="Preventivi - Prenotazioni - Contratti" CssClass="testo_titolo"></asp:Label>
            </td>
            </tr>
        </table>
    </div>

<div runat="server" id="tab_ricerca" style="position:fixed;z-index:95;" >
     <table runat="server" id="tab_ricerca1" border="0" cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444;">
       <tr>
          <td class="style46">
             <asp:Label ID="Label41" runat="server" Text="Documento" CssClass="testo_bold"></asp:Label>
             <asp:Label ID="lblStato" runat="server" Text="\Stato prenotazione" CssClass="testo_bold"></asp:Label>
          </td>
          <td class="style41">
             <asp:Label ID="lblTipoNumero" runat="server" Text="Num. Pren. Interno" CssClass="testo_bold"></asp:Label>
          </td>
          <td class="style49">
             <asp:Label ID="lblRifTo" runat="server" Text="Riferimento T.O." 
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
             <asp:Label ID="Label36" runat="server" Text="Pick-Up Da" 
                  CssClass="testo_bold"></asp:Label>
           </td>
          <td class="style47">
             <asp:Label ID="Label37" runat="server" Text="Pick-Up A" 
                  CssClass="testo_bold"></asp:Label>
           </td>
       </tr>
       <tr>
          <td class="style46">
              <asp:DropDownList ID="dropTipoDocumento" runat="server" AppendDataBoundItems="True" AutoPostBack="true" class="ddlist">
                  <asp:ListItem Selected="True" Value="1">Prenotazione</asp:ListItem>
                  <asp:ListItem Value="2">Preventivo</asp:ListItem>
                  <asp:ListItem Value="3">Contratto</asp:ListItem>
              </asp:DropDownList>
              <asp:DropDownList ID="dropStatoPrenotazione" runat="server" AppendDataBoundItems="True" class="ddlist">
                  <asp:ListItem Selected="False" Value="Tutti">Tutti</asp:ListItem>
                  <asp:ListItem Selected="True" Value="0">Aperto</asp:ListItem>
                  <asp:ListItem Value="D">Aperte/Doppie</asp:ListItem>
                  <asp:ListItem Value="1">Annullate prepagate</asp:ListItem>
                  <asp:ListItem Value="2">Annullate</asp:ListItem>
                  <asp:ListItem Value="3">Contratto</asp:ListItem>
                  <asp:ListItem Value="X">Rich.Annullamento</asp:ListItem>
                  <asp:ListItem Value="4">Bloccata - RIbaltamento</asp:ListItem>
                  <asp:ListItem Value="5">Aperto da RA Void</asp:ListItem>
              </asp:DropDownList>
              <asp:DropDownList ID="dropStatoContratto" runat="server" AppendDataBoundItems="True" Visible="false" class="ddlist">
                  <asp:ListItem Selected="False" Value="Tutti">Tutti</asp:ListItem>
                  <asp:ListItem Selected="True" Value="2">Aperto</asp:ListItem>
                  <asp:ListItem Value="1">Check Out</asp:ListItem>
                  <asp:ListItem Value="3">Quick Check In</asp:ListItem>
                  <asp:ListItem Value="4">Chiuso - Da Incassare</asp:ListItem>
                  <asp:ListItem Value="8">Chiuso - Da Fatturare</asp:ListItem>
                  <asp:ListItem Value="6">Chiuso - Fatturato</asp:ListItem>
                  <asp:ListItem Value="5">CRV-Attesa sost.</asp:ListItem>
                  <asp:ListItem Value="7">Void</asp:ListItem>
                  <asp:ListItem Value="900">Broker - Pagamento Assente</asp:ListItem>
              </asp:DropDownList>
           </td>
          <td class="style41">
              <%--<asp:TextBox ID="txtNumPreventivo" runat="server" Width="80px" onKeyPress="return filterInputInt(event)"></asp:TextBox>--%>
              <asp:TextBox ID="txtCercaNumStaz" runat="server" Width="30px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
              <asp:TextBox ID="txtCercaNumInterno" runat="server" Width="50px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
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
               <a onclick="Calendar.show(document.getElementById('<%=txtCercaPickUpDa.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox runat="server" Width="70px" ID="txtCercaPickUpDa" ></asp:TextBox>
                   </a>
               <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" FirstDayOfWeek="Monday"
                          TargetControlID="txtCercaPickUpDa" ID="txtDaData0_CalendarExtender">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                          CultureDatePlaceholder="" CultureTimePlaceholder="" 
                          CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                          CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                          CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaPickUpDa" 
                          ID="txtDaData0_MaskedEditExtender">
                </ajaxtoolkit:MaskedEditExtender>
           </td>
          <td class="style47">
              <a onclick="Calendar.show(document.getElementById('<%=txtCercaPickUpA.ClientID%>'), '%d/%m/%Y', false)"> 
      <asp:TextBox runat="server" Width="70px" ID="txtCercaPickUpA"></asp:TextBox>
                  </a>
        <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" FirstDayOfWeek="Monday"
                  TargetControlID="txtCercaPickUpA" 
                  ID="txtCercaPickUpDa0_CalendarExtender">
        </ajaxtoolkit:CalendarExtender>--%>
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
             <asp:Label ID="lblDaRibaltamento" runat="server" Text="Prenotazioni da ribaltamento" CssClass="testo_bold"></asp:Label>
             <asp:Label ID="lblCercaGruppoContratto" runat="server" Text="Gruppo - Targa" CssClass="testo_bold" Visible="false"></asp:Label>
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
            <asp:DropDownList ID="cercaPrenotazioniRibaltamento" runat="server"  CssClass="ddlist"
                AppendDataBoundItems="True" DataSourceID="sqlStazioniRibaltamento" DataTextField="stazione" 
                DataValueField="id">
               <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="dropCercaGruppoContratto" runat="server" Visible="false" CssClass="ddlist"
                AppendDataBoundItems="True" DataSourceID="sqlGruppiAuto" DataTextField="cod_gruppo" 
                DataValueField="id_gruppo">
               <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtCercaTargaContratto" runat="server" Width="80px" Visible="false"></asp:TextBox>
           </td>
          <td class="style2" colspan="2">
        <asp:DropDownList ID="cercaStazionePickUp" runat="server" CssClass="ddlist"
            AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
            DataValueField="id">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
              
           </td>
          <td class="style34" colspan="2">
        <asp:DropDownList ID="cercaStazioneDropOff" runat="server" CssClass="ddlist"
            AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
            DataValueField="id">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
           </td>
          <td>
              <a onclick="Calendar.show(document.getElementById('<%=txtCercaDropOffDa.ClientID%>'), '%d/%m/%Y', false)"> 
            <asp:TextBox runat="server" Width="70px" ID="txtCercaDropOffDa"></asp:TextBox>
                  </a>
       <%--     <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                      TargetControlID="txtCercaDropOffDa" 
                      ID="txtCercaPickUpDa0_CalendarExtender0">
            </ajaxtoolkit:CalendarExtender>--%>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                      CultureDatePlaceholder="" CultureTimePlaceholder="" 
                      CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                      CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                      CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaDropOffDa" 
                      ID="txtCercaPickUpDa0_MaskedEditExtender0">
            </ajaxtoolkit:MaskedEditExtender>
          </td>
          <td class="style47">
                <a onclick="Calendar.show(document.getElementById('<%=txtCercaDropOffA.ClientID%>'), '%d/%m/%Y', false)"> 
        <asp:TextBox runat="server" Width="70px" ID="txtCercaDropOffA"></asp:TextBox>
                    </a>
     <%--   <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                  TargetControlID="txtCercaDropOffA" 
                  ID="txtCercaPickUpDa1_CalendarExtender">
        </ajaxtoolkit:CalendarExtender>--%>
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
             <asp:Label ID="lblPrepagate" runat="server" Text="Prenot.Prepagate" CssClass="testo_bold"></asp:Label>
           </td>
          <td>
              <asp:Label ID="lblNumFattura" runat="server" Text="Fattura" CssClass="testo_bold" Visible="false"
></asp:Label>

          </td>
          <td class="style34" colspan="2">
             <asp:Label ID="lblPresRientro" runat="server" Text="Stazione presunto rientro" CssClass="testo_bold" Visible="false"></asp:Label> 
             <asp:Label ID="lblCercaGruppoTargaPrenot" runat="server" Text="Gruppo - Targa" CssClass="testo_bold"></asp:Label> 
          </td>
          <td>
              <asp:Label ID="lblPresRDa" runat="server" Text="Pres.R.Da" CssClass="testo_bold" Visible="false"></asp:Label> 
          </td>
          <td class="style47">
              <asp:Label ID="lblPresRA" runat="server" Text="Pres.R.A" CssClass="testo_bold" Visible="false"></asp:Label> 
          </td>
       </tr>
       <tr>
          <td colspan="1">
              <asp:DropDownList ID="dropCercaTipoCliente" runat="server" CssClass="ddlist"
                  AppendDataBoundItems="True" DataSourceID="sqlTipoClienti" 
                  DataTextField="descrizione" DataValueField="id">
                  <asp:ListItem Selected="False" Value="-1">Seleziona...</asp:ListItem>
                  <asp:ListItem Selected="False" Value="0">Nessuno</asp:ListItem>
              </asp:DropDownList>
           </td>
          <td>
              <asp:DropDownList ID="dropPrenotazioniPrepagate" runat="server" AppendDataBoundItems="True" CssClass="ddlist"
                  style="margin-left: 0px">
                  <asp:ListItem Selected="True" Value="-1">Tutte</asp:ListItem>
                  <asp:ListItem Selected="False" Value="1">Si</asp:ListItem>
                  <asp:ListItem Selected="False" Value="0">No</asp:ListItem>
              </asp:DropDownList>
           </td>
          <td>
            <asp:TextBox ID="txtCercaNunFattura" runat="server" Width="50px" onKeyPress="return filterInputInt(event)" Visible="false"
></asp:TextBox>
            <asp:DropDownList ID="dropCercaAnnoFattura" runat="server" CssClass="ddlist"
                  AppendDataBoundItems="True" Visible="false"
>
                  <%--<asp:ListItem Selected="False">2013</asp:ListItem>
                  <asp:ListItem Selected="True">2014</asp:ListItem>
                  <asp:ListItem Selected="False">2015</asp:ListItem>
                  <asp:ListItem Selected="False">2016</asp:ListItem>
                  <asp:ListItem Selected="False">2017</asp:ListItem>
                  <asp:ListItem Selected="False">2018</asp:ListItem>
                  <asp:ListItem Selected="False">2019</asp:ListItem>
                  <asp:ListItem Selected="False">2020</asp:ListItem>
                  <asp:ListItem Selected="False">2021</asp:ListItem>
                 <asp:ListItem Selected="False">2022</asp:ListItem>
                 <asp:ListItem Selected="False">2023</asp:ListItem>--%>



              </asp:DropDownList>

          </td>
          <td class="style34" colspan="2">
            <asp:DropDownList ID="dropCercaGruppoPrenotazione" runat="server" CssClass="ddlist"
                AppendDataBoundItems="True" DataSourceID="sqlGruppiAuto" DataTextField="cod_gruppo" 
                DataValueField="id_gruppo">
               <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtCercaTargaPrenotazione" runat="server" Width="80px"></asp:TextBox>
          
           
            <asp:DropDownList ID="cercaStazionePresuntoRientro" runat="server" Visible="false" CssClass="ddlist"
                AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                DataValueField="id">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
          </td>
          <td>
               <a onclick="Calendar.show(document.getElementById('<%=txtCercaPresRDa.ClientID%>'), '%d/%m/%Y', false)"> 
                  <asp:TextBox runat="server" Width="70px" ID="txtCercaPresRDa" Visible="false"></asp:TextBox>
                   </a>
                  <%--  <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                              TargetControlID="txtCercaPresRDa" 
                              ID="CalendarExtender1">
                    </ajaxtoolkit:CalendarExtender>--%>
                    <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                              CultureDatePlaceholder="" CultureTimePlaceholder="" 
                              CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                              CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                              CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaPresRDa" 
                              ID="MaskedEditExtender5">
                    </ajaxtoolkit:MaskedEditExtender>
          </td>
          <td class="style47">
              <a onclick="Calendar.show(document.getElementById('<%=txtCercaPresRA.ClientID%>'), '%d/%m/%Y', false)"> 
                  <asp:TextBox runat="server" Width="70px" ID="txtCercaPresRA" Visible="false"></asp:TextBox>
                  </a>
                 <%--   <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                              TargetControlID="txtCercaPresRA" 
                              ID="CalendarExtender4">
                    </ajaxtoolkit:CalendarExtender>--%>
                    <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                              CultureDatePlaceholder="" CultureTimePlaceholder="" 
                              CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                              CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                              CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaPresRA" 
                              ID="MaskedEditExtender6">
                    </ajaxtoolkit:MaskedEditExtender>
          </td>
       </tr>
       <tr>
          <td  colspan="1">
             <asp:Label ID="Label23" runat="server" Text="Operatore" CssClass="testo_bold"></asp:Label>
           </td>
          <td>
          <asp:Label ID="Label45" runat="server" Text="Ditta" CssClass="testo_bold"></asp:Label>
           </td>
          <td>
              

          </td>
          <td class="style34" colspan="2">
            
          </td>
          <td colspan="1" style="text-align:left;">
             <asp:Label ID="Label25" runat="server" Text="Creazione Da" CssClass="testo_bold"></asp:Label>
          </td>
          <td style="text-align:left;" >
              <asp:Label ID="Label43" runat="server" Text="Creazione A" CssClass="testo_bold"></asp:Label>
          </td>
       </tr>
       <tr>
          <td colspan="1">
               <asp:DropDownList ID="dropCercaOperatore" runat="server"  CssClass="ddlist"
                  AppendDataBoundItems="True" DataSourceID="sqlOperatori" 
                  DataTextField="descrizione" DataValueField="id">
                  <asp:ListItem Selected="False" Value="0">Seleziona...</asp:ListItem>
              </asp:DropDownList>
           </td>
          <td>
               <asp:TextBox ID="txt_cerca_ditta" runat="server" Width="150px" ></asp:TextBox>
           </td>
          <td>
            
          </td>
          <td class="style34" colspan="2">
            
          </td>
          <td>
                <a onclick="Calendar.show(document.getElementById('<%=txtCercaCreazioneDa.ClientID%>'), '%d/%m/%Y', false)"> 
                  <asp:TextBox runat="server" Width="70px" ID="txtCercaCreazioneDa" Visible="true"></asp:TextBox>
                    </a>
                <%--    <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                              TargetControlID="txtCercaCreazioneDa" 
                              ID="CalendarExtender12">
                    </ajaxtoolkit:CalendarExtender>--%>
                    <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                              CultureDatePlaceholder="" CultureTimePlaceholder="" 
                              CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                              CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                              CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaCreazioneDa" 
                              ID="MaskedEditExtender51">
                    </ajaxtoolkit:MaskedEditExtender>
          </td>
          <td class="style47">
               <a onclick="Calendar.show(document.getElementById('<%=txtCercaCreazioneA.ClientID%>'), '%d/%m/%Y', false)"> 
                  <asp:TextBox runat="server" Width="70px" ID="txtCercaCreazioneA" Visible="true"></asp:TextBox>
                   </a>
                   <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                              TargetControlID="txtCercaCreazioneA" 
                              ID="CalendarExtender41">
                    </ajaxtoolkit:CalendarExtender>--%>
                    <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                              CultureDatePlaceholder="" CultureTimePlaceholder="" 
                              CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                              CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                              CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaCreazioneA" 
                              ID="MaskedEditExtender61">
                    </ajaxtoolkit:MaskedEditExtender>
          </td>
       </tr>
       <tr style="height:33px;">
          <td align="center" colspan="7">
              <asp:Button ID="btnCercaIniziale" runat="server" Text="Cerca" Visible="true"  Width="77px" UseSubmitBehavior="False" />
          &nbsp;&nbsp;
          <asp:Button ID="btnStampaFatturazione" runat="server" Text="Stampa Fatturazione" Visible="false"  UseSubmitBehavior="False" />
          &nbsp;&nbsp;
          <asp:Button ID="btnReportExcel" runat="server" Text="Report Excel"  UseSubmitBehavior="False" />
          &nbsp;&nbsp;
          <asp:Button ID="btnStampaPrenotazioni" runat="server" Text="Stampa" Visible="true"  
                  Width="77px" UseSubmitBehavior="False" />
          &nbsp;&nbsp;
          <asp:Button ID="btnNuovoPreventivo" runat="server" Text="Nuovo Calcolo" 
                  UseSubmitBehavior="False" />
          &nbsp; <%--<asp:Button ID="btnRichiamaPreventivo" runat="server" 
                  Text="Richiama Documento" Visible="True" style="height: 26px" 
                  UseSubmitBehavior="False" />--%>
          &nbsp; <asp:Button ID="btnPulisciCampi" runat="server" 
                  Text="Dati del Giorno" Visible="True"  Width="110px"
                  UseSubmitBehavior="False" />
          &nbsp; <asp:Button ID="btnPulisciCampi1" runat="server" 
                  Text="Pulisci Campi" Visible="True"  Width="100px"
                  UseSubmitBehavior="False" />
          </td>
       </tr>
     </table>

     <table cellpadding="0" cellspacing="0" width="100%" border="0">
       <tr>
          <td colspan="7">
            <div style="width:1024px;height: 300px; overflow:scroll;" runat="server" Visible="false" id="divPreventivi">
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
                              <asp:Label ID="cognome_conducenteLabel" runat="server" Text='<%# Eval("cognome_conducente") %>' CssClass="testo" />&nbsp;
                              <asp:Label ID="nome_conducenteLabel" runat="server" Text='<%# Eval("nome_conducente") %>' CssClass="testo" />
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
                          <td align="center">
                              <asp:Label ID="cod_gruppoLabel" runat="server"  Text='<%# Eval("cod_gruppo") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="Label72" runat="server"  Text='<%# Eval("tariffa") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="tipo_cliente" runat="server"  Text='<%# Eval("tipo_cliente") %>' CssClass="testo" />
                               <asp:Label ID="lbl_data_creazione" runat="server"  Text='<%# Eval("data_creazione") %>' CssClass="testo" Visible="false"/>
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
                              <asp:Label ID="cognome_conducenteLabel" runat="server" Text='<%# Eval("cognome_conducente") %>' CssClass="testo" />&nbsp;
                              <asp:Label ID="nome_conducenteLabel" runat="server" Text='<%# Eval("nome_conducente") %>' CssClass="testo" />
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
                          <td align="center">
                              <asp:Label ID="cod_gruppoLabel" runat="server"  Text='<%# Eval("cod_gruppo") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="Label71" runat="server"  Text='<%# Eval("tariffa") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="tipo_cliente" runat="server"  Text='<%# Eval("tipo_cliente") %>' CssClass="testo" />
                              <asp:Label ID="lbl_data_creazione" runat="server"  Text='<%# Eval("data_creazione") %>' CssClass="testo" Visible="false"/>
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
                      <table id="Table2" runat="server" width="1660px" style="min-width:1800px;">
                          <tr id="Tr1" runat="server" >
                              <td id="Td1" runat="server">
                                  <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                      style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;font-size:11px;">
                                      <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                          <th></th>
                                          <th id="Th5" runat="server" align="left" width="20px">
                                             <asp:LinkButton ID="LinkButton6" runat="server" CommandName="order_by_num" CssClass="testo_titolo">Num.</asp:LinkButton>
                                          </th>
                                          <th id="Th11" runat="server" align="left" width="240px">
                                              <asp:LinkButton ID="LinkButton9" runat="server" CommandName="order_by_conducente" CssClass="testo_titolo">Conducente</asp:LinkButton>
                                          </th>
                                          <th id="Th7" runat="server" align="left" class="c_stazioni">
                                              <asp:LinkButton ID="LinkButton1" runat="server" CommandName="order_by_uscita" CssClass="testo_titolo">Uscita</asp:LinkButton>
                                          </th>
                                          <th id="Th1" runat="server" align="left" width="150px">
                                              <asp:LinkButton ID="LinkButton2" runat="server" CommandName="order_by_data_uscita" CssClass="testo_titolo">Data</asp:LinkButton>
                                          </th>
                                          <th id="Th9" runat="server" align="left" class="c_stazioni">
                                              <asp:LinkButton ID="LinkButton3" runat="server" CommandName="order_by_rientro" CssClass="testo_titolo">Rientro</asp:LinkButton>
                                          </th>
                                          <th id="Th2" runat="server" align="left" width="150px">
                                              <asp:LinkButton ID="LinkButton4" runat="server" CommandName="order_by_data_rientro" CssClass="testo_titolo">Data</asp:LinkButton>
                                          </th>
                                          <th id="Th10" runat="server" align="center" width="60px">
                                              <asp:LinkButton ID="LinkButton5" runat="server" CommandName="order_by_gruppo" CssClass="testo_titolo">Gruppo</asp:LinkButton>
                                          </th>
                                          <th id="Th6" runat="server" align="left" class="c_tariffa_prev">
                                              <asp:LinkButton ID="LinkButton7" runat="server" CommandName="order_by_tariffa" CssClass="testo_titolo">Tariffa</asp:LinkButton>
                                          </th>
                                          <th id="Th4" runat="server" align="left" class="c_tipocliente">
                                              <asp:LinkButton ID="LinkButton8" runat="server" CommandName="order_by_tipo_cliente" CssClass="testo_titolo">Tipo Cliente</asp:LinkButton>
                                          </th>
                                          
                                          
                                      </tr>
                                      <tr ID="itemPlaceholder" runat="server">
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                          <tr id="Tr3" runat="server">
                              <td id="Td2" runat="server" style="" align="left">
                                  <asp:DataPager ID="DataPager1" runat="server" PageSize="50">
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
           </td>
       </tr>
       <tr>
          <td colspan="7">
             <div style="width:1024px;height: 300px; overflow:scroll;" runat="server" Visible="false" id="divPrenotazioni">
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
                              <asp:Label ID="cognome_conducenteLabel" runat="server" Text='<%# Eval("cognome_conducente") %>' CssClass="testo" />&nbsp;
                              <asp:Label ID="nome_conducenteLabel" runat="server" Text='<%# Eval("nome_conducente") %>' CssClass="testo" />
                          </td>

                          <td align="left">
                             <asp:Label ID="tipo_cliente" runat="server"  Text='<%# Eval("tipo_cliente") %>' CssClass="testo" />
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
                          <td align="center">
                              <asp:Label ID="cod_gruppoLabel" runat="server"  Text='<%# Eval("cod_gruppo") %>' CssClass="testo " />
                          </td>
                          <td align="left">
                              <asp:Label ID="targa_gruppo_speciale" runat="server"  Text='<%# Eval("targa_gruppo_speciale") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="tariffa" runat="server"  Text='<%# Eval("tariffa") %>' CssClass="testo" />
                          </td>
                          <td align="center"> 
                              <asp:Label ID="prepagata" runat="server" Text='<%# Eval("prepagata") %>'  Visible="false" CssClass="c_align"/>
                              <asp:Label ID="lblPrepagato" runat="server" CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="Label65" runat="server" Text='<%# Eval("status") %>' CssClass="testo" />
                          </td>
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr >
                          <td align="left">
                              <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi"/>
                          </td>
                          <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("Nr_pren") %>' Visible="false" />
                              <asp:Label ID="NUMPREN" runat="server" Text='<%# Eval("NUMPREN") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="cognome_conducenteLabel" runat="server" Text='<%# Eval("cognome_conducente") %>' CssClass="testo" />&nbsp;
                              <asp:Label ID="nome_conducenteLabel" runat="server" Text='<%# Eval("nome_conducente") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="tipo_cliente" runat="server"  Text='<%# Eval("tipo_cliente") %>' CssClass="testo" />
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
                          <td align="center">
                              <asp:Label ID="cod_gruppoLabel" runat="server"  Text='<%# Eval("cod_gruppo") %>' CssClass="testo c_align" />
                          </td>
                          <td align="left">
                              <asp:Label ID="targa_gruppo_speciale" runat="server"  Text='<%# Eval("targa_gruppo_speciale") %>' CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="tariffa" runat="server"  Text='<%# Eval("tariffa") %>' CssClass="testo" />
                          </td>
                          <td align="center">
                              <asp:Label ID="prepagata" runat="server" Text='<%# Eval("prepagata") %>'  Visible="false" CssClass="c_align"/>
                              <asp:Label ID="lblPrepagato" runat="server" CssClass="testo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="Label65" runat="server" Text='<%# Eval("status") %>' CssClass="testo" />
                          </td>    
                      </tr>
                  </AlternatingItemTemplate>
                  <EmptyDataTemplate>
                      <table id="Table3" runat="server" style="">
                          <tr>
                              <td>
                                  Non è stato restituito alcun dato.</td>
                          </tr>
                      </table>
                  </EmptyDataTemplate>
                  <LayoutTemplate>
                      <table id="Table4" runat="server" style="width:auto;min-width:1800px;" >
                          <tr id="Tr4" runat="server" >
                              <td id="Td3" runat="server">
                                  <table ID="itemPlaceholderContainer" runat="server"  border="1" 
                                      style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;font-size:small;">
                                      <tr id="Tr5" runat="server" style="color: #FFFFFF;padding:3px;" bgcolor="#19191b">
                                          <th></th>
                                          <th id="Th3" runat="server" align="left" width="80px">
                                             <asp:LinkButton ID="LinkButton9" runat="server" CommandName="order_by_num" CssClass="testo_titolo">Num.</asp:LinkButton>
                                          </th>
                                          <th id="Th16" runat="server" align="left" width="240px">
                                              <asp:LinkButton ID="LinkButton11" runat="server" CommandName="order_by_conducente" CssClass="testo_titolo">Conducente</asp:LinkButton>
                                          </th>
                                          <th id="Th4" runat="server" align="left" width="100px">
                                              <asp:LinkButton ID="LinkButton21" runat="server" CommandName="order_by_tipo_cliente" CssClass="testo_titolo">Tipo Cliente</asp:LinkButton>
                                          </th>
                                          <th id="Th12" runat="server" align="left" class="c_stazioni">
                                              <asp:LinkButton ID="LinkButton13" runat="server" CommandName="order_by_uscita" CssClass="testo_titolo">Uscita</asp:LinkButton>
                                          </th>
                                          <th id="Th1" runat="server" align="left" width="150px">
                                              <asp:LinkButton ID="LinkButton14" runat="server" CommandName="order_by_data_uscita" CssClass="testo_titolo">Data</asp:LinkButton>
                                          </th>
                                          <th id="Th13" runat="server" align="left" class="c_stazioni">
                                              <asp:LinkButton ID="LinkButton15" runat="server" CommandName="order_by_rientro" CssClass="testo_titolo">Rientro</asp:LinkButton>
                                          </th>
                                          <th id="Th2" runat="server" align="left" width="150px">
                                              <asp:LinkButton ID="LinkButton16" runat="server" CommandName="order_by_data_rientro" CssClass="testo_titolo">Data</asp:LinkButton>
                                          </th>
                                          <th id="Th14" runat="server" align="center" width="60px">
                                              <asp:LinkButton ID="LinkButton17" runat="server" CommandName="order_by_gruppo" CssClass="testo_titolo">Gruppo</asp:LinkButton>
                                          </th>
                                          <th id="Th22" runat="server" align="left" width="80px">
                                              <asp:LinkButton ID="LinkButton18" runat="server" CommandName="order_by_targa" CssClass="testo_titolo">Targa</asp:LinkButton>
                                          </th>
                                          <th id="Th6" runat="server" align="left" class="c_tariffa">
                                              <asp:LinkButton ID="LinkButton19" runat="server" CommandName="order_by_tariffa" CssClass="testo_titolo">Tariffa</asp:LinkButton>
                                          </th>
                                          <th id="Th15" runat="server" align="left" width="60px">
                                              <asp:LinkButton ID="LinkButton20" runat="server" CommandName="order_by_prepag" CssClass="testo_titolo">Prepag.</asp:LinkButton>
                                          </th>
                                          <th id="Th8" runat="server" align="left" width="100px">
                                             <asp:LinkButton ID="LinkButton12" runat="server" CommandName="order_by_stato" CssClass="testo_titolo">Stato</asp:LinkButton>
                                          </th>
                                      </tr>
                                      <tr ID="itemPlaceholder" runat="server">
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                          <tr id="Tr6" runat="server">
                              <td id="Td4" runat="server" style="font-family:Verdana, Geneva, Tahoma, sans-serif;font-size:12px;" align="left">
                                  <asp:DataPager ID="DataPager1" runat="server" PageSize="50" >
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
           </td>
       </tr>
       <tr>
                  <td colspan="7">
                     <div style="width:auto;max-width:1024px;height: 300px; overflow:scroll;" runat="server" Visible="false" id="divContratti">
                      <asp:ListView ID="listContratti" runat="server" DataKeyNames="id" DataSourceID="sqlContratti" >
                          <ItemTemplate>
                              <tr style="background-color:#DCDCDC;color: #000000;">
                                  <td align="left">
                                      <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi"/>
                                  </td>
                                  <td align="left">
                                      <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                                      <asp:Label ID="num_contratto" runat="server" Text='<%# Eval("num_contratto") %>' CssClass="testo" />
                                  </td>
                                  <td align="left">
                                      <asp:Label ID="cognome_conducenteLabel" runat="server"  Text='<%# Eval("cognome_primo_conducente") %>' CssClass="testo" />&nbsp;
                                      <asp:Label ID="nome_conducenteLabel" runat="server" Text='<%# Eval("nome_primo_conducente") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                                      <asp:Label ID="Label15" runat="server"  Text='<%# Eval("cognome_secondo_conducente") %>' CssClass="testo" />&nbsp;
                                      <asp:Label ID="Label35" runat="server" Text='<%# Eval("nome_secondo_conducente") %>' CssClass="testo" />
                                  </td>
                                  <td align="left">
                                      <asp:Label ID="Label65" runat="server" Text='<%# Eval("status") %>' CssClass="testo" />
                                  </td>
                                  <td align="left" >
                                      <asp:Label ID="staz_uscitaLabel" runat="server"  Text='<%# Eval("staz_uscita") %>' CssClass="testo" />
                                  </td>
                                  <td>
                                      <asp:Label ID="data_uscitaLabel" runat="server" Text='<%# Eval("data_uscita") %>' CssClass="testo" />&nbsp;
                                      <asp:Label ID="ore_uscitaLabel" runat="server" Text='<%# Eval("ore_uscita") %>'  CssClass="testo" />
                                      <asp:Label ID="Label49" runat="server" Text=':' CssClass="testo" />
                                      <asp:Label ID="minuti_uscitaLabel" runat="server" Text='<%# Eval("minuti_uscita") %>' CssClass="testo" />
                                  </td>
                                  <td align="left" >
                                      <asp:Label ID="staz_presunto_rientro" runat="server" Text='<%# Eval("staz_presunto_rientro") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                                  </td>
                                  <td>
                                      <asp:Label ID="Label42" runat="server" Text='<%# Eval("data_presunto_rientro") %>' CssClass="testo" />&nbsp;
                                      <asp:Label ID="ore_presunto_rientro" runat="server" Text='<%# Eval("ore_presunto_rientro") %>'  CssClass="testo" />
                                      <asp:Label ID="Label44" runat="server" Text=':' CssClass="testo" />
                                      <asp:Label ID="minuti_presunto_rientro" runat="server" Text='<%# Eval("minuti_presunto_rientro") %>' CssClass="testo" />
                                  </td>
                                  <td align="left" >
                                      <asp:Label ID="staz_rientroLabel" runat="server" Text='<%# Eval("staz_rientro") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                                  </td>
                                  <td>
                                      <asp:Label ID="data_rientroLabel" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo" />&nbsp;
                                      <asp:Label ID="ore_rientroLabel" runat="server" Text='<%# Eval("ore_rientro") %>'  CssClass="testo" />
                                      <asp:Label ID="Label51" runat="server" Text=':' CssClass="testo" />
                                      <asp:Label ID="minuti_rientroLabel" runat="server" Text='<%# Eval("minuti_rientro") %>' CssClass="testo" />
                                  </td>
                                   <td align="center">
                                     <asp:Label ID="cod_gruppoLabel" runat="server"  Text='<%# Eval("cod_gruppo") %>' CssClass="testo" />
                                    </td>
                                  <td align="left">
                                      <asp:Label ID="veicoloLabel" runat="server"  Text='<%# Eval("veicolo") %>' CssClass="testo" />
                                  </td>
                                  <td class="c_align">
                                      <asp:Label ID="prepagata" runat="server" Text='<%# Eval("prepagata") %>'  Visible="false" />
                                      <asp:Label ID="lblPrepagato" runat="server" CssClass="testo" />
                                  </td>
                                  <td align="left">
                                     <asp:Label ID="tipo_cliente" runat="server"  Text='<%# Eval("tipo_cliente") %>' CssClass="testo" />
                                  </td>
                              </tr>
                          </ItemTemplate>
                          <AlternatingItemTemplate>
                              <tr >
                                  <td align="left">
                                      <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi"/>
                                  </td>
                                  <td align="left">
                                      <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                                      <asp:Label ID="num_contratto" runat="server" Text='<%# Eval("num_contratto") %>' CssClass="testo" />
                                  </td>
                                  <td align="left">
                                      <asp:Label ID="cognome_conducenteLabel" runat="server"  Text='<%# Eval("cognome_primo_conducente") %>' CssClass="testo" />&nbsp;
                                      <asp:Label ID="nome_conducenteLabel" runat="server" Text='<%# Eval("nome_primo_conducente") %>' CssClass="testo" />&nbsp;&nbsp;
                                      <asp:Label ID="Label15" runat="server"  Text='<%# Eval("cognome_secondo_conducente") %>' CssClass="testo" />&nbsp;
                                      <asp:Label ID="Label35" runat="server" Text='<%# Eval("nome_secondo_conducente") %>' CssClass="testo" />
                                  </td>
                                  <td align="left">
                                      <asp:Label ID="Label65" runat="server" Text='<%# Eval("status") %>' CssClass="testo" />
                                  </td>
                                  <td align="left" >
                                      <asp:Label ID="staz_uscitaLabel" runat="server"  Text='<%# Eval("staz_uscita") %>' CssClass="testo" />
                                  </td>
                                  <td>
                                      <asp:Label ID="data_uscitaLabel" runat="server" Text='<%# Eval("data_uscita") %>' CssClass="testo" />&nbsp;
                                      <asp:Label ID="ore_uscitaLabel" runat="server" Text='<%# Eval("ore_uscita") %>'  CssClass="testo" />
                                      <asp:Label ID="Label49" runat="server" Text=':' CssClass="testo" />
                                      <asp:Label ID="minuti_uscitaLabel" runat="server" Text='<%# Eval("minuti_uscita") %>' CssClass="testo" />
                                  </td>
                                  <td align="left" >
                                      <asp:Label ID="staz_presunto_rientro" runat="server" Text='<%# Eval("staz_presunto_rientro") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                                  </td>
                                  <td>
                                      <asp:Label ID="Label42" runat="server" Text='<%# Eval("data_presunto_rientro") %>' CssClass="testo" />&nbsp;
                                      <asp:Label ID="ore_presunto_rientro" runat="server" Text='<%# Eval("ore_presunto_rientro") %>'  CssClass="testo" />
                                      <asp:Label ID="Label44" runat="server" Text=':' CssClass="testo" />
                                      <asp:Label ID="minuti_presunto_rientro" runat="server" Text='<%# Eval("minuti_presunto_rientro") %>' CssClass="testo" />
                                  </td>
                                  <td align="left" >
                                      <asp:Label ID="staz_rientroLabel" runat="server" Text='<%# Eval("staz_rientro") %>' CssClass="testo" />&nbsp;&nbsp;&nbsp;
                                  </td>
                                  <td>
                                      <asp:Label ID="data_rientroLabel" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo" />&nbsp;
                                      <asp:Label ID="ore_rientroLabel" runat="server" Text='<%# Eval("ore_rientro") %>'  CssClass="testo" />
                                      <asp:Label ID="Label51" runat="server" Text=':' CssClass="testo" />
                                      <asp:Label ID="minuti_rientroLabel" runat="server" Text='<%# Eval("minuti_rientro") %>' CssClass="testo" />
                                  </td>
                                   <td align="center">
                                        <asp:Label ID="cod_gruppoLabel" runat="server"  Text='<%# Eval("cod_gruppo") %>' CssClass="testo" />
                                    </td>
                                  <td align="left">
                                      <asp:Label ID="veicoloLabel" runat="server"  Text='<%# Eval("veicolo") %>' CssClass="testo" />
                                  </td>
                                  <td class="c_align">
                                      <asp:Label ID="prepagata" runat="server" Text='<%# Eval("prepagata") %>'  Visible="false" />
                                      <asp:Label ID="lblPrepagato" runat="server" CssClass="testo" />
                                  </td>
                                  <td align="left">
                                     <asp:Label ID="tipo_cliente" runat="server"  Text='<%# Eval("tipo_cliente") %>' CssClass="testo" />
                                  </td>
                              </tr>
                          </AlternatingItemTemplate>
                          <EmptyDataTemplate>
                              <table id="Table3" runat="server" style="">
                                  <tr>
                                      <td>
                                          Non è stato restituito alcun dato.</td>
                                  </tr>
                              </table>
                          </EmptyDataTemplate>
                          <LayoutTemplate>
                              <table id="Table4" runat="server"  style="width:auto;min-width:1800px;">
                                  <tr id="Tr4" runat="server">
                                      <td id="Td3" runat="server">
                                          <table ID="itemPlaceholderContainer" runat="server"  border="1" 
                                              style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;font-size:small;">
                                              <tr id="Tr5" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                                  <th></th>
                                                  <th id="Th3" runat="server" align="left" width="80px">
                                                     <asp:LinkButton ID="LinkButton9" runat="server" CommandName="order_by_num" CssClass="testo_titolo">Num.</asp:LinkButton>
                                                  </th>
                                                  <th id="Th16" runat="server" align="left" class="c_conducente">
                                                      <asp:LinkButton ID="LinkButton23" runat="server" CommandName="order_by_conducente" CssClass="testo_titolo">Conducente/i</asp:LinkButton>
                                                  </th>
                                                  <th id="Th8" runat="server" align="left" width="150px">
                                                     <asp:LinkButton ID="LinkButton24" runat="server" CommandName="order_by_stato" CssClass="testo_titolo">Stato</asp:LinkButton>
                                                  </th>
                                                  <th id="Th12" runat="server" align="left" class="c_stazioni">
                                                      <asp:LinkButton ID="LinkButton25" runat="server" CommandName="order_by_uscita" CssClass="testo_titolo">Uscita</asp:LinkButton>
                                                  </th>
                                                  <th id="Th1" runat="server" align="left" width="150px">
                                                      <asp:LinkButton ID="LinkButton26" runat="server" CommandName="order_by_data_uscita" CssClass="testo_titolo">Data</asp:LinkButton>
                                                  </th>
                                                  <th id="Th17" runat="server" align="left" class="c_stazioni">
                                                      <asp:LinkButton ID="LinkButton27" runat="server" CommandName="order_by_presunto_rientro" CssClass="testo_titolo">Presunto Rientro</asp:LinkButton>
                                                  </th>
                                                  <th id="Th18" runat="server" align="left" width="150px">
                                                      <asp:LinkButton ID="LinkButton28" runat="server" CommandName="order_by_data_presunto_rientro" CssClass="testo_titolo">Data Presunto Rientro</asp:LinkButton>
                                                  </th>
                                                  <th id="Th13" runat="server" align="left" class="c_stazioni">
                                                      <asp:LinkButton ID="LinkButton29" runat="server" CommandName="order_by_rientro" CssClass="testo_titolo">Rientro</asp:LinkButton>
                                                  </th>
                                                  <th id="Th2" runat="server" align="left" width="150px">
                                                      <asp:LinkButton ID="LinkButton30" runat="server" CommandName="order_by_data_rientro" CssClass="testo_titolo">Data Rientro</asp:LinkButton>
                                                  </th>
                                                    <th id="Th10" runat="server" align="center" width="60px">
                                                        <asp:LinkButton ID="LinkButton5" runat="server" CommandName="order_by_gruppo" CssClass="testo_titolo">Gruppo</asp:LinkButton>
                                                        </th>
                                                  <th id="Th19" runat="server" align="left" class="c_targa">
                                                      <asp:LinkButton ID="LinkButton31" runat="server" CommandName="order_by_veicolo" CssClass="testo_titolo">Targa</asp:LinkButton> 
                                                  </th>
                                                  <th id="Th15" runat="server" align="center" class="c_prepagata">
                                                      <asp:LinkButton ID="LinkButton32" runat="server" CommandName="order_by_prepag" CssClass="testo_titolo">Prepag.</asp:LinkButton> 
                                                  </th>
                                                  <th id="Th4" runat="server" align="left" class="c_tipocliente">
                                                      <asp:LinkButton ID="LinkButton33" runat="server" CommandName="order_by_tipo_cliente" CssClass="testo_titolo">Tipo Cliente</asp:LinkButton>
                                                  </th>
                                              </tr>
                                              <tr ID="itemPlaceholder" runat="server">
                                              </tr>
                                          </table>
                                      </td>
                                  </tr>
                                  <tr id="Tr6" runat="server">
                                      <td id="Td4" runat="server" style="" align="left">
                                          <asp:DataPager ID="DataPager1" runat="server" PageSize="50">
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
                   </td>
               </tr>
       </table>
</div>

<div runat="server" id="tab_cerca_tariffe" visible="false" style="position:fixed;z-index:90;background-color:#CCCC00;" >
<table runat="server" id="tabella_ricerca" border="0" cellspacing="2" cellpadding="2" width="1024px" style="border:4px solid #444;">
    <tr>
        <td colspan="8" align="center" style="color: #FFFFFF;background-color:#444;" class="style1">
            <asp:Label ID="lblTipoDocumento" runat="server" Text="Preventivo Num.:" CssClass="testo_titolo"></asp:Label>&nbsp;
            <asp:Label ID="lblNumPreventivo" runat="server"  CssClass="testo_titolo"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblData" runat="server" Text="Data Preventivo:" CssClass="testo_titolo"></asp:Label>&nbsp;
            <asp:Label ID="lblDataPreventivo" runat="server"  CssClass="testo_titolo"></asp:Label>
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblOperatore" runat="server" Text="Operatore Creazione:" CssClass="testo_titolo"></asp:Label>&nbsp;
            <asp:Label ID="lblOperatoreCreazione" runat="server"  CssClass="testo_titolo"></asp:Label>
        </td>
    </tr>
<tr>
  <td class="style28" valign="top">
    <asp:Label ID="Label2" runat="server" Text="Pick-Up" CssClass="testo_bold"></asp:Label>
  </td>
  <td class="style54" valign="top">
        <asp:DropDownList ID="dropStazionePickUp" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
            DataValueField="id">
           <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
  </td>
  <td valign="top" class="style44">
       <a onclick="Calendar.show(document.getElementById('<%=txtDaData.ClientID%>'), '%d/%m/%Y', false);" onchange="copia_data();" > 
    <asp:TextBox runat="server" Width="70px" ID="txtDaData" onchange="copia_data();"></asp:TextBox>
          </a>
<%--        <ajaxtoolkit:CalendarExtender runat="server"  BehaviorID="startDate" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtDaData" ID="CalendarExtender2"  OnClientDateSelectionChanged="onSelectedStartDate">
        </ajaxtoolkit:CalendarExtender>--%>
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
                <img src="punto_elenco.jpg"  border="0" alt="" title="" width="8" height="7" />&nbsp;<asp:Label ID="warning" runat="server" Text='<%# Eval("warning") %>' 
                    style="font-size:11px;font-family:Verdana, Geneva, Tahoma, sans-serif;font-weight:600" />&nbsp;
                <asp:Label ID="tipo" runat="server" Text='<%# Eval("tipo") %>' visible="false" />
            </ItemTemplate>
        </asp:DataList>
  
  </td>
</tr>
<tr>
  <td class="style28" valign="top">
     <asp:Label ID="Label1" runat="server" Text="Drop Off" CssClass="testo_bold"></asp:Label>
  </td>
  <td class="style54" valign="top">
    <asp:DropDownList ID="dropStazioneDropOff" runat="server" 
        AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
        DataValueField="id">
        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>    
    </asp:DropDownList>
  </td>
  <td class="style44" valign="top">
       <a onclick="Calendar.show(document.getElementById('<%=txtAData.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox runat="server" Width="70px" ID="txtAData" ></asp:TextBox>
           </a>
    <%--    <ajaxtoolkit:CalendarExtender runat="server"  BehaviorID="endDate" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtAData" ID="CalendarExtender3">
        </ajaxtoolkit:CalendarExtender>--%>
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
  
        <asp:DataList ID="listWarningDropPreventivi" runat="server"  
            DataSourceID="sqlWarningDropPreventivi" RepeatColumns="1" 
            RepeatDirection="Horizontal" >
            <ItemTemplate>
                <img src="punto_elenco.jpg"  border="0" alt="" title="" width="8" height="7" />&nbsp;
                <asp:Label ID="warning" runat="server" Text='<%# Eval("warning") %>' style="font-size:11px;font-family:Verdana, Geneva, Tahoma, sans-serif;font-weight:600" />&nbsp;
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
    <asp:Label ID="Label3" runat="server" Text="Tipologia Cliente" CssClass="testo_bold"></asp:Label>
  </td>
  <td class="style54" valign="top">
      <asp:DropDownList ID="dropTipoCliente" runat="server" 
          AppendDataBoundItems="True" DataSourceID="sqlTipoClienti" 
          DataTextField="descrizione" DataValueField="id">
          <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
      </asp:DropDownList>
  &nbsp;</td>
  <td class="style44" valign="top">
  
    <asp:Label ID="Label73" runat="server" Text="Codice Ditta" CssClass="testo_bold"></asp:Label>
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
  <td valign="top" class="style54">
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
     <asp:Label ID="labelNumeroGiorni" runat="server" Text="Numero Giorni" CssClass="testo_bold"></asp:Label>
  </td>
  <td valign="top">   
      <asp:TextBox ID="txtNumeroGiorni" runat="server" Width="36px" ReadOnly="true"></asp:TextBox>
  </td>
</tr>
<tr>
  <td class="style14" valign="top" colspan="8" align="center">
      <asp:Button ID="btnCerca" runat="server" Text="Cerca" ValidationGroup="cerca"  UseSubmitBehavior="false" />
      <asp:Button ID="btnAnnulla0" runat="server" Text="Annulla" UseSubmitBehavior="false" />
  </td>
</tr>
</table>
</div> 
<%--<tr>
           <td colspan="3" align="center" style="color: #FFFFFF;background-color:#444;" class="style1">
               <asp:Label ID="Label21" runat="server" Text="Preventivo Num.:" CssClass="testo_titolo"></asp:Label>&nbsp;
               <asp:Label ID="lblNumPreventivo" runat="server"  CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>--%>
<div runat="server" id="tab_preventivo" style='position:relative;margin-top:0px;z-index:80; top: 0px; left: 0px;' visible="false" >
<table  runat="server" id="tabella_preventivo" style="border:4px solid #444; border-top:0px;" border="0" cellspacing="1" cellpadding="1" width="1024px">
  <tr>
    <td class="style23">
      <asp:Label ID="Label17" runat="server" Text="Cognome" CssClass="testo_bold"></asp:Label> 
    </td>
    <td class="style25">
      <asp:Label ID="Label16" runat="server" Text="Nome" CssClass="testo_bold"></asp:Label> 
    </td>
    <td class="style22">
      <asp:Label ID="Label22" runat="server" Text="Telefono" CssClass="testo_bold"></asp:Label> 
    </td>
    <td class="style22">
      <asp:Label ID="Label18" runat="server" Text="E-Mail" CssClass="testo_bold"></asp:Label> 
    </td>
  </tr>
  <tr>
    <td class="style24">
        <asp:TextBox ID="txtCognome" runat="server" Width="170px" MaxLength="50"></asp:TextBox>
    </td>
    <td class="style26">
        <asp:TextBox ID="txtNome" runat="server" Width="170px" MaxLength="50"></asp:TextBox>
    </td>
    <td class="style26">
        <asp:TextBox ID="txtTelefono" runat="server" Width="170px" MaxLength="20"></asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="txtMail" runat="server" Width="170px" MaxLength="80"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td class="style21" colspan="4" align="center">
      &nbsp;<br />
      <asp:Button ID="btnSalvaPreventivo" runat="server" Text="Salva Preventivo" UseSubmitBehavior="false" />
  
        &nbsp;<asp:Button ID="btnAnnulla3" runat="server" Text="Annulla"  UseSubmitBehavior="false"/>
        
        &nbsp;<asp:Button ID="btnInviaMailPreventivo" runat="server" Text="Invia mail preventivo" UseSubmitBehavior="false" />
  
      &nbsp;<asp:Button ID="btnVediUltimoCalcolo" runat="server" Text="Vedi dett. precedente" UseSubmitBehavior="false"  Visible="false"/>
  
        &nbsp;<asp:Button ID="btnAnnulla8" runat="server" Text="Chiudi" UseSubmitBehavior="false" />

        &nbsp;<asp:Button ID="btnAnnulla4" runat="server" Text="Chiudi senza salvare" UseSubmitBehavior="false" />

    </td>
  </tr>
</table>
</div>

<div runat="server" id="tab_prenotazioni" style='position:relative;margin-top:0px;z-index:70;' visible="false" >
<table style="border:4px solid #444; border-top:0px;" border="0" cellspacing="0" cellpadding="1" width="1024px" >
  <tr>
     <td colspan="2">
        <uc1:anagrafica_conducenti ID="anagrafica_conducenti" runat="server" Visible="false" />
        <uc1:anagrafica_ditte ID="anagrafica_ditte" runat="server" Visible="false" />
     </td>
  </tr>
</table>
<table style="border:4px solid #444; border-top:0px;" border="0" cellspacing="0" cellpadding="1" width="1024px" >
  <tr>
    <td colspan="6" style="color: #FFFFFF;background-color:#444;" >
        <asp:Label ID="Label31" runat="server" Text="Guidatore" CssClass="testo_titolo"></asp:Label>
    &nbsp;
        
       <asp:Label ID="id_conducente" runat="server" Visible="false"></asp:Label>            
        <%--</a>&nbsp; --%>

        <asp:Button ID="btnModificaConducente" runat="server" Text="Scegli" Font-Size="X-Small" ToolTip="Modifica conducente" Height="18px" UseSubmitBehavior="false" />
      
    </td>
  </tr>
  <tr>
    <td>
      
    <asp:Label ID="Label27" runat="server" Text="Cognome" CssClass="testo_bold"></asp:Label>
      
    </td>
    <td>
      
    <asp:Label ID="Label26" runat="server" Text="Nome" CssClass="testo_bold"></asp:Label>
      
    </td>
    <td colspan="2">
      
    <asp:Label ID="Label28" runat="server" Text="E-Mail" CssClass="testo_bold"></asp:Label>
      
    </td>
    <td colspan="2">       
        <asp:Label ID="Label85" runat="server" Text="Indirizzo" CssClass="testo_bold"></asp:Label>      
      </td>

  </tr>
  <tr>
    <td  valign="top">
        &nbsp;
              
        <asp:TextBox ID="txtCognomeConducente" runat="server" Width="118px"></asp:TextBox>
      
    </td>
    <td valign="top">
      
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
    <td>
      
    <asp:Label ID="Label64" runat="server" Text="Data di Nascita" CssClass="testo_bold"></asp:Label>
      
    </td>
    <td> 
      
    <asp:Label ID="Label29" runat="server" Text="Gruppo da Consegnare" CssClass="testo_bold"></asp:Label>
      
    </td>
    <td>
      <asp:Label ID="Label30" runat="server" Text="Numero Volo (Arrivo)" CssClass="testo_bold"></asp:Label>
    </td>
    <td>
      <asp:Label ID="Label86" runat="server" Text="Numero Volo (Rientro)" 
            CssClass="testo_bold"></asp:Label>  
    </td>
    <td valign="top">
      <asp:Label ID="Label40" runat="server" Text="Numero Riferimento T.O." CssClass="testo_bold"></asp:Label>  
    </td>
     <td valign="top">
      <asp:Label ID="Label11" runat="server" Text="Rif. Tel." CssClass="testo_bold"></asp:Label>  
    </td>
  </tr>
  <tr>
    <td>
        &nbsp;&nbsp;
               <a onclick="Calendar.show(document.getElementById('<%=txtDataDiNascita.ClientID%>'), '%d/%m/%Y', false)"> 
        <asp:TextBox runat="server" Width="70px" ID="txtDataDiNascita" ></asp:TextBox>
                   </a>
      <%--  <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
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
    <td>
      
        <asp:DropDownList ID="dropGruppoDaConsegnare" runat="server" 
            DataSourceID="sqlGruppiAuto" DataTextField="cod_gruppo" 
            DataValueField="id_gruppo" AppendDataBoundItems="True">
            <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
        </asp:DropDownList>
      
      </td>
    <td>
      
        <asp:TextBox ID="txtVoloOut" runat="server" Width="100px" 
            MaxLength="10"></asp:TextBox>
      
      </td>
    <td>
      
        <asp:TextBox ID="txtVoloPr" runat="server" Width="100px" 
            MaxLength="10"></asp:TextBox>
      
      </td>
    <td valign="top">
      
        <asp:TextBox ID="txtRiferimentoTO" runat="server" Width="100px" MaxLength="15"></asp:TextBox>
        <asp:Label ID="lblRifToOld" runat="server" Visible="false"></asp:Label>
        &nbsp;&nbsp;&nbsp; <asp:Label ID="lblRiferimentoEsistente" runat="server" 
            Text="NUM. RIFERIMENTO ESISTENTE" CssClass="testo_bold" 
            ForeColor="Red" Visible="false"></asp:Label>
    </td>
    <td valign="top">
       <asp:TextBox ID="txtRifTel" runat="server" Width="100px" MaxLength="30"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td valign="top">
      <asp:Label ID="Label84" runat="server" Text="Note" CssClass="testo_bold"></asp:Label>  
    </td>
    <td colspan="5">
      
        <asp:TextBox ID="txtNote" runat="server" Width="748px" Height="38px" 
            TextMode="MultiLine"></asp:TextBox>
      
      </td>
  </tr>
  <tr>
    <td valign="top" colspan="6">
    </td>
  </tr>
  <tr>
    <td colspan="6">
        
        <asp:Label ID="Label80" runat="server" Text="Fatturare a ditta" CssClass="testo_bold"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtNomeDitta" runat="server" Width="230px"  ReadOnly="True"></asp:TextBox>&nbsp;&nbsp;
        <asp:Button ID="btnModificaDitta" runat="server" Text="..." UseSubmitBehavior="false" />
        <asp:Label ID="id_ditta" runat="server" visible="false"></asp:Label>
     </td>
  </tr>
  <tr>
    <td id="Td5" runat="server" align="center" colspan="6">
       
    &nbsp;&nbsp;
        <%--<asp:Button ID="btnPagamento" runat="server" Text="Pagamento" style="height: 26px" Visible="false" UseSubmitBehavior="false" />--%>
  
    &nbsp;<asp:Button ID="btnAggiornaPrenotazione" runat="server" Text="Modifica Prenotazione" style="height: 26px" Visible="false" UseSubmitBehavior="false" />
  
    &nbsp;<asp:Button ID="btnSalvaPrenotazione" runat="server" Text="Salva Prenotazione" style="height: 26px" UseSubmitBehavior="true" 
       OnClick="btnSalvaPrenotazione_Click"  OnClientClick="return preventMultipleSubmissions(this, event);" />
  
    &nbsp;<asp:Button ID="btnAnnulla7" runat="server" Text="Annulla" UseSubmitBehavior="false" />
  
    &nbsp;<asp:Button ID="btnAnnulla9" runat="server" Text="Chiudi" UseSubmitBehavior="false" />
        
    &nbsp;<%--<asp:Button ID="btnAnnulla5" runat="server" Text="Annulla" />--%><asp:Button ID="btnAnnulla6" runat="server" Text="Chiudi senza salvare" />

    </td>
  </tr>
</table>

</div>

<div runat="server" id="div_note" style="position:relative;margin-top:0px;z-index:60;" >
    <table width="1024px" runat="server" id="table_note" visible="false">
       <tr>
          <td>
             <uc1:gestione_note id="gestione_note" runat="server"></uc1:gestione_note>
          </td>
       </tr>
    </table>
</div>


<div runat="server" id="div_dettaglio_gruppi" style="position:relative;margin-top:0px;z-index:60;" >
<table border="0" cellspacing="0" cellpadding="0" width="1024px" runat="server" id="table_warning" visible="false">
  <tr>
    <td>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Attenzione:" ForeColor="Red" CssClass="testo_bold"></asp:Label>
        <br />
        <asp:Label ID="Label5" runat="server"  Text="Modificare i parametri di ricerca oppure continuare con il preventivo. L'eventuale prenotazione sarà ON REQUEST se sono presenti uno o più avvertimenti in rosso." CssClass="testo"></asp:Label>
    </td>
  </tr>
  <tr>
    <td align="center">
       <asp:Button ID="btnContinua" runat="server" Text="Continua ugualmente." ValidationGroup="cerca" UseSubmitBehavior="false" />
    &nbsp;<asp:Button ID="btnAnnulla1" runat="server" Text="Annulla" UseSubmitBehavior="false" />
  
    &nbsp;</td>
  </tr>
</table>

<%--start testo msg differenze tariffe salvo 22.12.2022--%>
    <div id="div_tariffe_diverse" runat="server">

          <asp:Label ID="lbl_msg_tariffe_diverse" runat="server" Text="" CssClass="testo_bold" ForeColor="Red" Font-Bold="true" Font-Size="Medium" Visible="false"></asp:Label>

    </div>

<%--end testo msg differenze tariffe salvo 22.12.2022--%>

<table runat="server" id="table_tariffe" border="0" cellspacing="2" cellpadding="2" width="1024px" visible="false">
 <tr>
    <td width="176px">
       <br />
       <asp:Label ID="Label8" runat="server" Text="Tariffe Generiche:" CssClass="testo_bold"></asp:Label>
    </td>
    <td> 
      <br />
      &nbsp; <asp:DropDownList ID="dropTariffeGeneriche" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlTariffeGeneriche" 
            DataTextField="codice" DataValueField="id">
            <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>
    </td>
    <td>
     <br />
    <asp:Label ID="Label9" runat="server" Text="Tariffe Particolari:" CssClass="testo_bold"></asp:Label>
     &nbsp;&nbsp; <asp:DropDownList ID="dropTariffeParticolari" runat="server" 
            AppendDataBoundItems="True" DataSourceID="sqlTariffeParticolari" 
            DataTextField="codice" DataValueField="id">
            <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
      </asp:DropDownList>
        
        </td>
 </tr>
 <tr>
    <td style="width:176px;">

    </td>
    <td> 
   
    </td>
    <td>
        <asp:Label ID="Label10" runat="server" Text="Applica Sconto:" 
            CssClass="testo_bold"></asp:Label>
       &nbsp;
        <asp:TextBox ID="txtSconto" runat="server" Width="32px" visible="true" Text="0"></asp:TextBox>
        <asp:TextBox ID="txt_sconto_new" runat="server" Width="32px" visible="true" Text="0"></asp:TextBox>

        <b>%</b>
        <asp:DropDownList ID="dropTipoSconto" runat="server" 
            AppendDataBoundItems="True">
            <asp:ListItem Selected="True" Value="0">Elementi Scontabili</asp:ListItem>
            <asp:ListItem Selected="False" Value="1">Valore Tariffa</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="lblMxSconto" runat="server" Text="Applicato il MASSIMO SCONTO." 
            CssClass="testo_bold" ForeColor="Red" Visible="false"></asp:Label>
        <asp:Label ID="lbl_valore_senza_sconto" runat="server" CssClass="testo_bold" visible="true"></asp:Label>
         <asp:Label ID="lbl_valore_con_sconto" runat="server" CssClass="testo_bold" visible="true"></asp:Label>
        <asp:Label ID="lbl_sconti_tariffe" runat="server" CssClass="testo_bold" visible="true"></asp:Label>
         <asp:Label ID="lbl_imp_sconto" runat="server" CssClass="testo_bold" visible="true"></asp:Label>
        <asp:Label ID="lbl_list_tariffe" runat="server" CssClass="testo_bold" visible="true"></asp:Label>
        <asp:Label ID="lbl_list_tariffe_tempoKM" runat="server" CssClass="testo_bold" visible="true"></asp:Label>
         <asp:Label ID="lbl_data_creazione" runat="server" CssClass="testo_bold" visible="false"></asp:Label>

        </td>
 </tr>
 <tr>
    <td align="left" width="176px">
      <asp:Label ID="lblGruppoNoTariffa" runat="server" CssClass="testo_bold" ></asp:Label>
      <asp:Label ID="lblFonteCommissionabile" runat="server" Text="Fonte Commissionabile:" CssClass="testo_bold"></asp:Label>
      <asp:ImageButton ID="btnAggiornaFontiCommissionabili" runat="server" ImageUrl="/images/ricarica.png" />
    </td>
    <td align="left">
        <asp:DropDownList ID="dropFonteCommissionabile" runat="server" AppendDataBoundItems="True" DataSourceID="sqlFontiCommissionabili" 
            DataTextField="rag_soc" DataValueField="id" >
            <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
        </asp:DropDownList>&nbsp;
        <asp:TextBox ID="txtPercentualeCommissionabile" runat="server" Width="32px" Readonly="True"></asp:TextBox>
        <asp:Label ID="lblPercentualeCommissionabile" runat="server" Text="%" CssClass="font-bold"></asp:Label>
        
        &nbsp;
        <asp:DropDownList ID="dropTipoCommissione" runat="server" 
            AppendDataBoundItems="True">
            <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
            <asp:ListItem Selected="False" Value="1">Da riconoscere</asp:ListItem>
            <asp:ListItem Selected="False" Value="2">Preincassate</asp:ListItem>
        </asp:DropDownList>&nbsp;
    </td>
    <td align="right">
        <asp:Label ID="lblMinGiorniNolo" runat="server" CssClass="testo_bold"></asp:Label>&nbsp;
        <asp:Button ID="btnStampaTKm" runat="server" Text="Vedi Tariffa" UseSubmitBehavior="false" />
        <asp:Button ID="btnStampaCondizioni" runat="server" Text="Vedi Condizioni" UseSubmitBehavior="false" />
        <asp:Label ID="lbl_new_tariffa" runat="server" CssClass="testo_bold" Visible="true"></asp:Label>&nbsp;
        
    </td>
 </tr>
 </table>

<table runat="server" id="table_gruppi" border="0" cellspacing="0" cellpadding="0" width="1024px" visible="false">
 <tr>
  <td align="left">
    <asp:Label ID="Label6" runat="server" Text="Gruppi" CssClass="testo_bold"></asp:Label>
  </td>
</tr>
<tr>
  <td align="left" style="border: thin solid #000000;">
              
        <asp:DataList ID="listGruppi" runat="server"  DataSourceID="sqlGruppiAuto" 
            RepeatColumns="3" Width="100%">
            <ItemTemplate>
                <td width="5%" valign="top">
                  <asp:CheckBox ID="old_sel_gruppo" runat="server" Visible="false"/><asp:CheckBox ID="sel_gruppo" runat="server" />&nbsp;<asp:Label ID="gruppo" runat="server" Text='<%# Eval("cod_gruppo") %>' CssClass="testo_bold" /><asp:Label ID="id_gruppo" runat="server" Text='<%# Eval("id_gruppo") %>' visible="false" />&nbsp;
                </td>
                <td width="28%" valign="top">
                   <asp:Image ID="punto1" ImageUrl="punto_elenco.jpg" runat="server" width="8" height="7" Visible="false" />
                   <asp:Label ID="pick" runat="server" Text='Non vendibile (pick-up)' ForeColor="red" CssClass="testo_bold" Visible="false" />
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
    <asp:Button ID="btnProsegui" runat="server" Text="Vedi tariffe" OnClientClick="return ckCampi();"/>
    &nbsp;<asp:Button ID="btnSalvaPrenNoTariffa" runat="server" Text="Salva Prenotazione Senza Tariffa" UseSubmitBehavior="false" Visible="false" />    
  &nbsp;<asp:Button ID="btnCambiaTariffa" runat="server" Text="Cambia tariffa/sconto" Visible="false" UseSubmitBehavior="false" />
  &nbsp;<asp:Button ID="btnAnnulla2" runat="server" Text="Annulla" UseSubmitBehavior="false" />
  
  </td>
</tr>
</table>

<table runat="server" id="table_boooo" border="0" cellspacing="0" cellpadding="0" width="1024px">
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
  <td align="left" style="width:55%;" valign="top" >
      
      <%--<asp:ImageButton ID="lblFocus" runat="server" Width="0px" Height="0px" />--%>
      <asp:Label ID="lbl_Importo_Sconto" runat="server" Text='' CssClass="testo_bold" Visible="false" />
      
      <asp:DataList ID="listPreventiviCosti" runat="server" DataSourceID="sqlPreventiviCosti" Width="100%" >
          <ItemTemplate>
              <tr runat="server" id="riga_gruppo" >
                 <td>
                   <a runat="server" id="vediCalcolo" href='<%# "preventivo_vedi_calcolo.aspx?idPrv=" & Eval("id_documento") & "&versione=" & Eval("num_calcolo") & "&idGrp=" & Eval("id_gruppo") & "&numGG=" & txtNumeroGiorni.Text %>' rel="lyteframe" title="-" rev="width: 1010px; height: 500px; scrolling: yes;" target="_blank"><asp:Image ID="image_primo_guidatore" runat="server" ImageUrl="images/lente.png" style="position:absolute; width:20px; height:20px;" /></a>
                      
                 </td>
                 <td bgcolor="#19191b" style="width:98%;" colspan="5">
                      <asp:Label ID="Label7" runat="server" Text="Gruppo:" CssClass="testo_bold_bianco" tooltip='<%# Eval("id_documento") %>' />
                     <%--<asp:Label ID="lbl_calcolo_attuale" runat="server" Text='<%# Eval("num_calcolo") %>' Visible="false" ForeColor="Red" Font-Bold="true"/> --%>
                     <asp:Label ID="id_gruppoLabel" runat="server" Text='<%# Eval("id_gruppo") %>' Visible="false" />
                      <asp:Label ID="gruppo" runat="server" Text='<%# Eval("gruppo") %>' CssClass="testo_bold_bianco" />
                      &nbsp;&nbsp;&nbsp;
                      <asp:Button ID="preventivo" runat="server" Text="Preventivo" CommandName="preventivo" /> 
                      <asp:Button ID="prenotazione" runat="server" Text="Prenotazione" CommandName="prenotazione"  /> 
                      <asp:Button ID="contratto" runat="server" Text="Contratto" CommandName="contratto"  /> 
                 </td>
              </tr>
           
              <tr runat="server" id="riga_intestazione" >
                 <td style="width:6%;">
                 </td>
                 <td style="width:50%;">
                 </td>
                 <td style="width:18%;">
                      <asp:Label ID="labelSconto" runat="server" Text='Sconto' CssClass="testo_bold" />
                      <asp:Label ID="labelCommissioni" runat="server" Text='Comm.' CssClass="testo_bold" Visible="false"/>&nbsp;         
                 </td>
                 
                 <td style="width:20%;" >
                    <asp:Label ID="Label12" runat="server" Text='Costo' CssClass="testo_bold" />&nbsp;
                 </td>
                 <td>
                   <asp:Label ID="Label21" runat="server" Text='Omaggio' CssClass="testo_bold" />
                 </td>
              </tr>

              <tr runat="server" id="riga_elementi">
                 <td style="width:6%;" align="left">
                     <asp:CheckBox ID="chkOldScegli" runat="server" Visible="false" Checked='<%# Eval("selezionato") %>' />
                     <asp:CheckBox ID="chkScegli" runat="server" Visible="false" Checked='<%# Eval("selezionato") %>' />
                     <asp:Label ID="lblIncluso" runat="server" Text="I " ToolTip="Incluso" CssClass="testo_bold" Visible="false"  />
                     <asp:Label ID="lblObbligatorio" runat="server" Text="O " ToolTip="Obbligatorio" CssClass="testo_bold" Visible="false"  />
                     <asp:Label ID="lblInformativa" runat="server" Text="IN " ToolTip="Informativa" CssClass="testo_bold" Visible="false"  />
                 </td>
                 <td style="width:61%;">
                      <asp:Label ID="id_preventivi_costi" runat="server" Text='<%# Eval("id") %>' visible="false" />
                      <asp:Label ID="id_elemento" runat="server" Text='<%# Eval("id_elemento") %>' visible="false" />
                      <asp:Label ID="num_elemento" runat="server" Text='<%# Eval("num_elemento") %>' visible="false" />
                      <asp:Label ID="tipologia_franchigia" runat="server" Text='<%# Eval("tipologia_franchigia") %>' visible="false" />
                      <asp:Label ID="sottotipologia_franchigia" runat="server" Text='<%# Eval("sottotipologia_franchigia") %>' visible="false" />
                      <asp:Label ID="is_gps" runat="server" Text='<%# Eval("is_gps") %>' visible="false" />
                      <boxover:BoxOver ID="BoxOver1" runat="server" body='<%# Eval("nome_costo") & " - " & Eval("descrizione_lunga") & " - IMPONIBILE: " & Eval("imponibile_scontato") & " - IVA: " & Eval("iva_imponibile_scontato") %>' controltovalidate="nome_costo" header="Descrizione" CssHeader="toolheader"  CssBody="toolbody"   />
                      <asp:Label ID="nome_costo" runat="server" Text='<%# Eval("nome_costo") %>' CssClass="testo" />:&nbsp;&nbsp;
                 </td>
                 <td style="width:6%;">
                     <asp:Label ID="a_carico_to" runat="server" Text=""  CssClass="testo" Visible="false"/> <!-- aggiunto x compatibilità con prenotazione per pdf 14.01.2021 -->
                      <asp:Label ID="valore_costoLabel" runat="server" Text='<%# FormatNumber(Eval("valore_costo"),2) %>' CssClass="testo" Visible="false" />&nbsp;
                      <asp:Label ID="lblSconto" runat="server" Text='<%# FormatNumber(Eval("sconto"),2) %>' CssClass="testo" Visible="true" />
                      <asp:Label ID="lblCommissioni" runat="server" Text='<%# FormatNumber(Eval("commissioni"),2) %>' CssClass="testo" Visible="false" />
                      <asp:Label ID="id_a_carico_di" runat="server" Text='<%# Eval("id_a_carico_di") %>' Visible="false" />
                      <asp:Label ID="obbligatorio" runat="server" Text='<%# Eval("obbligatorio") %>' Visible="false" /> 
                      <asp:Label ID="id_metodo_stampa" runat="server" Text='<%# Eval("id_metodo_stampa") %>' Visible="false" />                    
                 </td>
                 <%--<td width="18%">
                     <asp:Label ID="lblSconto" runat="server" Text='<%# FormatNumber(Eval("sconto"),2) %>' CssClass="testo" />&nbsp;
                 </td>--%>
                 <td style="width:18%;">
                    <asp:Label ID="costo_scontato" runat="server" Text='<%# FormatNumber(FormatNumber(Eval("valore_costo"),2) - FormatNumber(Eval("sconto"),2),2) %>' CssClass="testo" />&nbsp;
                 </td>
                 <td align="center">
                     <asp:Label ID="omaggiabile" runat="server" Text='<%# Eval("omaggiabile") %>' Visible="false" />
                     <asp:CheckBox ID="chkOldOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />
                     <asp:CheckBox ID="chkOmaggio" runat="server" Visible="false" Checked='<%# Eval("omaggiato") %>' />   
                     <asp:Button ID="aggiorna" runat="server" Text="Aggiorna" CommandName="Aggiorna" Visible="false" OnClientClick="return ckCampi();"/> 
                 </td>
              </tr>
          </ItemTemplate>
      </asp:DataList>
      <%-- POSIZIONE 1 --%>
  
  
  </td>
  <td style="width:50%;" valign="top" align="center" >
            

      <asp:DataList ID="listVecchioCalcolo" runat="server" DataSourceID="sqlUltimoPreventiviCosti" Width="95%" > <%--Visible="false"--%>
          <ItemTemplate>
              <tr runat="server" id="riga_gruppo" >
                 <td bgcolor="#19191b" style="width:100%;" colspan="6" height="23px" align="left">
                      <asp:Label ID="Label7" runat="server" Text="Gruppo:" CssClass="testo" ForeColor="white" />
                      <asp:Label ID="id_gruppoLabel" runat="server" Text='<%# Eval("id_gruppo") %>' Visible="false" />
                      <asp:Label ID="gruppo" runat="server" Text='<%# Eval("gruppo") %>' CssClass="testo_bold" ForeColor="white" />
                      &nbsp;&nbsp;&nbsp;
                     <asp:Label ID="lbl_num_calcolo" runat="server" Text='<%# Eval("num_calcolo") %>' Visible="true" Font-Bold="true" ForeColor="black"/>

                 </td>
              </tr>
              <tr runat="server" id="riga_intestazione" >
                 <td width="6%">
                 </td>
                 <td width="50%">
                 </td>
                 <td width="18%" align="left">
                      <asp:Label ID="Label13" runat="server" Text='Sconto' CssClass="testo_bold" />&nbsp;         
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
                      <asp:Label ID="valore_costoLabel" runat="server" Text='<%# FormatNumber(Eval("valore_costo"),2) %>' CssClass="testo" Visible="false" />&nbsp;
                      <asp:Label ID="lblSconto" runat="server" Text='<%# FormatNumber(Eval("sconto"),2) %>' CssClass="testo" />&nbsp;
                      <asp:Label ID="id_a_carico_di" runat="server" Text='<%# Eval("id_a_carico_di") %>' Visible="false" />
                      <asp:Label ID="obbligatorio" runat="server" Text='<%# Eval("obbligatorio") %>' Visible="false" /> 
                      <asp:Label ID="id_metodo_stampa" runat="server" Text='<%# Eval("id_metodo_stampa") %>' Visible="false" />                    
                 </td>
                 <%--<td width="18%">
                     <asp:Label ID="lblSconto" runat="server" Text='<%# FormatNumber(Eval("sconto"),2) %>' CssClass="testo" />&nbsp;
                 </td>--%>
                 <td width="18%" align="left">
                    
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

<%--<div runat="server" id="tab_pagamento" visible="false" style="position:relative;margin-top:0px;z-index:50;" >
     <uc1:scambio_importo ID="Scambio_Importo1" runat="server"  />
</div>--%>




      <asp:Label ID="gruppi_ultima_ricerca" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="tariffa_ultima_ricerca" runat="server" Visible="false"></asp:Label>
   
      <asp:Label ID="numero_prenotazione" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="idPreventivo" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="idPrenotazione" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="idContratto" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="numCalcolo" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="tipo_preventivo" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="old_ultimo_gruppo" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="ultimo_gruppo" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="id_gruppo_auto_scelto" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="tariffa_broker" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="livello_accesso_sconto" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="livello_accesso_omaggi" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="livello_accesso_broker" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="livello_accesso_fatturazione" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="query_cerca_prev" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="query_cerca_pren" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="query_cerca_cnt" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="lblAvvisoBlackList" runat="server" Visible="false"></asp:Label>
      
      <asp:Label ID="lblOrderBY_prev" runat="server" Visible="False"></asp:Label>
      <asp:Label ID="lblOrderBY_pren" runat="server" Visible="False"></asp:Label>
      <asp:Label ID="lblOrderBY_cnt" runat="server" Visible="False"></asp:Label>
      
    <asp:SqlDataSource ID="sqlStazioni" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT stazioni.id, (codice + ' ' + nome_stazione) As stazione FROM stazioni WITH(NOLOCK) WHERE attiva='1' ORDER BY codice"></asp:SqlDataSource>
  
  
    <asp:SqlDataSource ID="sqlStazioniRibaltamento" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT stazioni.id, (codice + ' ' + nome_stazione) As stazione FROM stazioni WITH(NOLOCK) WHERE stazione_ribaltamento='1' ORDER BY codice"></asp:SqlDataSource>
    

    <asp:SqlDataSource ID="sqlTipoClienti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT LEFT(descrizione,30) As descrizione , [id] FROM clienti_tipologia WITH(NOLOCK) where id<>11 and id<>8 ORDER BY [descrizione]">
    </asp:SqlDataSource>
    

    <asp:SqlDataSource ID="sqlWarningPickPreventivi" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT warning, tipo FROM preventivi_warning WITH(NOLOCK) WHERE (([id_documento] = @id_preventivo) AND ([num_calcolo] = @num_calcolo) And ((tipo='PICK') OR (tipo='PICK INFO'))) ORDER BY [warning]">
        <SelectParameters>
            <asp:ControlParameter ControlID="idPreventivo" Name="id_preventivo" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlWarningDropPreventivi" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT warning, tipo FROM preventivi_warning WITH(NOLOCK) WHERE (([id_documento] = @id_preventivo) AND ([num_calcolo] = @num_calcolo) And ((tipo='DROP') OR (tipo='DROP INFO'))) ORDER BY [warning]">
        <SelectParameters>
            <asp:ControlParameter ControlID="idPreventivo" Name="id_preventivo" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlWarningGruppi" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT warning, tipo FROM preventivi_warning WITH(NOLOCK) WHERE (([id_documento] = @id_preventivo) AND ([num_calcolo] = @num_calcolo) And ((tipo='GRUPPO'))) ORDER BY [warning]">
        <SelectParameters>
            <asp:ControlParameter ControlID="idPreventivo" Name="id_preventivo" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlFontiCommissionabili" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT rag_soc, id  FROM fonti_commissionabili WITH(NOLOCK) WHERE attiva='1' ORDER BY rag_soc"></asp:SqlDataSource>
   
    <asp:SqlDataSource ID="sqlGruppiAuto" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT gruppi.id_gruppo, gruppi.cod_gruppo FROM gruppi WITH(NOLOCK) WHERE attivo='1' ORDER BY cod_gruppo"></asp:SqlDataSource>
        
    <asp:SqlDataSource ID="sqlElementiExtra" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT condizioni_elementi.id, condizioni_elementi.descrizione FROM condizioni_elementi WITH(NOLOCK) WHERE id='0'"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlOperatori" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, (cognome + ' ' + nome) As descrizione FROM operatori WITH(NOLOCK) WHERE attivo = 1 and nome<>'xxx' ORDER BY cognome"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlTariffeGeneriche" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, codice FROM tariffe WITH(NOLOCK) WHERE id='0'"></asp:SqlDataSource>
        
    <asp:SqlDataSource ID="sqlTariffeParticolari" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, codice FROM tariffe WITH(NOLOCK) WHERE id='0'"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlPreventiviCosti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT preventivi_costi.num_calcolo, preventivi_costi.id, preventivi_Costi.id_documento, gruppi.id_gruppo, gruppi.cod_gruppo As gruppo,preventivi_costi.id_elemento, preventivi_costi.nome_costo, condizioni_elementi.descrizione_lunga, condizioni_elementi.tipologia_franchigia, condizioni_elementi.sottotipologia_franchigia, condizioni_elementi.is_gps, ISNULL((imponibile+iva_imponibile+ISNULL(imponibile_onere,0)+ISNULL(iva_onere,0)),0) As valore_costo, ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, ISNULL(commissioni_imponibile_originale,0)+ISNULL(commissioni_iva_originale,0) As commissioni, id_a_carico_di,preventivi_costi.obbligatorio, id_metodo_stampa, ISNULL(selezionato,'False') As selezionato, ISNULL(preventivi_costi.omaggiato,'False') As omaggiato, ISNULL(preventivi_costi.omaggiabile,'False') As omaggiabile, preventivi_costi.num_elemento, preventivi_costi.imponibile_scontato, preventivi_costi.iva_imponibile_scontato  FROM preventivi_costi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON preventivi_costi.id_gruppo=gruppi.id_gruppo LEFT JOIN condizioni_elementi WITH(NOLOCK) ON preventivi_costi.id_elemento=condizioni_elementi.id WHERE ((id_documento = @id_preventivo) AND (num_calcolo = @num_calcolo_preventivo)) AND ordine_stampa<>'5' AND ISNULL(franchigia_attiva,'1')='1' AND (NOT (preventivi_costi.ordine_stampa='7' AND preventivi_costi.franchigia_attiva IS NULL) OR condizioni_elementi.tipologia='KM_EXTRA') AND ISNULL(condizioni_elementi.tipologia,'')<>'RIMUOVI_RIFORNIMENTO' AND NOT (NOT valore_percentuale IS NULL AND ordine_stampa<>'2') ORDER BY id_gruppo,ordine_stampa,ISNULL(condizioni_elementi.ordinamento_vis,0) DESC, nome_costo  ">
        <SelectParameters>
            <asp:ControlParameter ControlID="idPreventivo" Name="id_preventivo" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo_preventivo" 
                PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlUltimoPreventiviCosti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT preventivi_costi.num_calcolo, gruppi.id_gruppo, gruppi.cod_gruppo As gruppo, preventivi_costi.id_elemento, preventivi_costi.nome_costo, ISNULL((imponibile+iva_imponibile+ISNULL(imponibile_onere,0)+ISNULL(iva_onere,0)),0) As valore_costo, ISNULL((imponibile+iva_imponibile-imponibile_scontato-iva_imponibile_scontato),0) As sconto, id_a_carico_di,preventivi_costi.obbligatorio, id_metodo_stampa, ISNULL(selezionato,'False') As selezionato, ISNULL(preventivi_costi.omaggiato,'False') As omaggiato, ISNULL(preventivi_costi.omaggiabile,'False') As omaggiabile,ISNULL(commissioni_imponibile,0)+ISNULL(commissioni_iva,0) As commissioni FROM preventivi_costi WITH(NOLOCK) INNER JOIN gruppi WITH(NOLOCK) ON preventivi_costi.id_gruppo=gruppi.id_gruppo LEFT JOIN condizioni_elementi WITH(NOLOCK) ON preventivi_costi.id_elemento=condizioni_elementi.id WHERE ((id_documento = 260264) AND ordine_stampa<>'5' AND ISNULL(franchigia_attiva,'1')='1' AND (NOT (preventivi_costi.ordine_stampa='7' AND preventivi_costi.franchigia_attiva IS NULL) OR condizioni_elementi.tipologia='KM_EXTRA') AND ISNULL(condizioni_elementi.tipologia,'')<>'RIMUOVI_RIFORNIMENTO' AND NOT (NOT valore_percentuale IS NULL AND ordine_stampa<>'2')) ORDER BY id_gruppo,ordine_stampa,ISNULL(condizioni_elementi.ordinamento_vis,0) DESC, nome_costo ">
                      
        <SelectParameters>
            <asp:ControlParameter ControlID="idPreventivo" Name="id_preventivo" 
                PropertyName="Text" Type="Int32" />
            <%--
                (num_calcolo = @num_calcolo_preventivo-1)
                
                <asp:ControlParameter ControlID="numCalcolo" Name="num_calcolo_preventivo" 
                PropertyName="Text" Type="Int32" />--%>
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlPreventivi" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT preventivi.data_creazione, preventivi.id, preventivi.num_preventivo, (stazioni1.codice + ' ' + stazioni1.nome_stazione) As staz_uscita, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As staz_rientro, CONVERT(char(10),preventivi.data_uscita,103) As data_uscita, preventivi.ore_uscita, preventivi.minuti_uscita, CONVERT(Char(10),preventivi.data_rientro,103) As data_rientro, preventivi.ore_rientro, preventivi.minuti_rientro, gruppi.cod_gruppo, preventivi.cognome_conducente, preventivi.nome_conducente FROM preventivi WITH(NOLOCK) INNER JOIN stazioni AS stazioni1 WITH(NOLOCK) ON preventivi.id_stazione_uscita=stazioni1.id INNER JOIN stazioni As stazioni2 WITH(NOLOCK) ON preventivi.id_stazione_rientro=stazioni2.id INNER JOIN gruppi WITH(NOLOCK) ON preventivi.id_gruppo_auto=gruppi.id_gruppo WHERE NOT num_preventivo IS NULL ORDER BY preventivi.id"></asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlPrenotazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        
        SelectCommand="SELECT prenotazioni.Nr_pren, prenotazioni.NUMPREN, (stazioni1.codice + ' ' + stazioni1.nome_stazione) As staz_uscita, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As staz_rientro, CONVERT(char(10),prenotazioni.PRDATA_OUT,103) As data_uscita, prenotazioni.ore_uscita, prenotazioni.minuti_uscita, CONVERT(Char(10),prenotazioni.PRDATA_PR,103) As data_rientro, prenotazioni.ore_rientro, prenotazioni.minuti_rientro, gruppi.cod_gruppo, prenotazioni.cognome_conducente, prenotazioni.nome_conducente, prenotazioni.targa_gruppo_speciale FROM prenotazioni WITH(NOLOCK) LEFT JOIN stazioni AS stazioni1 WITH(NOLOCK) ON prenotazioni.PRID_stazione_out=stazioni1.id LEFT JOIN stazioni As stazioni2 WITH(NOLOCK) ON prenotazioni.PRID_stazione_pr=stazioni2.id LEFT JOIN gruppi WITH(NOLOCK) ON prenotazioni.id_gruppo=gruppi.id_gruppo WHERE attiva='1' ORDER BY prenotazioni.Nr_Pren"></asp:SqlDataSource>
    

    <% Dim sqlco As String = "SELECT contratti.id, contratti_status.descrizione AS status, contratti.num_contratto, stazioni1.codice + ' ' + stazioni1.nome_stazione AS staz_uscita, stazioni2.codice + ' ' + stazioni2.nome_stazione AS staz_rientro, "
        sqlco += "stazioni3.codice + ' ' + stazioni3.nome_stazione AS staz_presunto_rientro, CONVERT(char(10), contratti.data_uscita, 103) AS data_uscita, DATEPART(hh, contratti.data_uscita) AS ore_uscita, DATEPART(mi, "
        sqlco += "contratti.data_uscita) AS minuti_uscita, CONVERT(Char(10), contratti.data_rientro, 103) AS data_rientro, DATEPART(hh, contratti.data_rientro) AS ore_rientro, DATEPART(mi, contratti.data_rientro) AS minuti_rientro, "
        sqlco += "CONVERT(Char(10), contratti.data_presunto_rientro, 103) AS data_presunto_rientro, DATEPART(hh, contratti.data_presunto_rientro) AS ore_presunto_rientro, DATEPART(mi, contratti.data_presunto_rientro) "
        sqlco += "AS minuti_presunto_rientro, veicoli.targa AS veicolo, conducenti1.COGNOME AS cognome_primo_conducente, conducenti1.nome AS nome_primo_conducente, "
        sqlco += "conducenti2.COGNOME AS cognome_secondo_conducente, conducenti2.nome AS nome_secondo_conducente, contratti.prenotazione_prepagata AS prepagata, clienti_tipologia.descrizione AS tipo_cliente, "
        sqlco += "GRUPPI.cod_gruppo "
        sqlco += "FROM contratti WITH (NOLOCK) INNER JOIN "
        sqlco += "GRUPPI ON contratti.id_gruppo_auto = GRUPPI.ID_gruppo LEFT OUTER JOIN "
        sqlco += "stazioni AS stazioni1 WITH (NOLOCK) ON contratti.id_stazione_uscita = stazioni1.id LEFT OUTER JOIN "
        sqlco += "stazioni AS stazioni2 WITH (NOLOCK) ON contratti.id_stazione_rientro = stazioni2.id LEFT OUTER JOIN "
        sqlco += "stazioni AS stazioni3 WITH (NOLOCK) ON contratti.id_stazione_presunto_rientro = stazioni3.id LEFT OUTER JOIN "
        sqlco += "veicoli WITH (NOLOCK) ON contratti.id_veicolo = veicoli.id LEFT OUTER JOIN "
        sqlco += "CONDUCENTI AS conducenti1 WITH (NOLOCK) ON contratti.id_primo_conducente = conducenti1.ID_CONDUCENTE LEFT OUTER JOIN "
        sqlco += "CONDUCENTI AS conducenti2 WITH (NOLOCK) ON contratti.id_secondo_conducente = conducenti2.ID_CONDUCENTE LEFT OUTER JOIN "
        sqlco += "contratti_status WITH (NOLOCK) ON contratti.status = contratti_status.id LEFT OUTER JOIN "
        sqlco += "clienti_tipologia WITH (NOLOCK) ON contratti.id_fonte = clienti_tipologia.id "
        sqlco += "WHERE (contratti.attivo = '1') "
        sqlco += "ORDER BY contratti.num_contratto"
     %>
    <asp:SqlDataSource ID="sqlContratti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"  
        SelectCommand="<%=sqlco  %>"></asp:SqlDataSource> 
        
   <%--"SELECT contratti.id, contratti_status.descrizione As status, contratti.num_contratto, (stazioni1.codice + ' ' + stazioni1.nome_stazione) As staz_uscita, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As staz_rientro, (stazioni3.codice + ' ' + stazioni3.nome_stazione) As staz_presunto_rientro, CONVERT(char(10),contratti.data_uscita,103) As data_uscita, DATEPART(hh,contratti.data_uscita) As ore_uscita, DATEPART(mi,contratti.data_uscita) As minuti_uscita, CONVERT(Char(10),contratti.data_rientro,103) As data_rientro, DATEPART(hh,contratti.data_rientro) As ore_rientro, DATEPART(mi,contratti.data_rientro) As minuti_rientro, CONVERT(Char(10),contratti.data_presunto_rientro,103) As data_presunto_rientro, DATEPART(hh,contratti.data_presunto_rientro) As ore_presunto_rientro, DATEPART(mi,contratti.data_presunto_rientro) As minuti_presunto_rientro, gruppi.cod_gruppo, veicoli.targa As veicolo, conducenti1.cognome As cognome_primo_conducente, conducenti1.nome As nome_primo_conducente, conducenti2.cognome As cognome_secondo_conducente, conducenti2.nome As nome_secondo_conducente, prenotazione_prepagata As prepagata, clienti_tipologia.descrizione As tipo_cliente FROM contratti WITH(NOLOCK) LEFT JOIN stazioni AS stazioni1 WITH(NOLOCK) ON contratti.id_stazione_uscita=stazioni1.id LEFT JOIN stazioni As stazioni2 WITH(NOLOCK) ON contratti.id_stazione_rientro=stazioni2.id LEFT JOIN stazioni As stazioni3 WITH(NOLOCK) ON contratti.id_stazione_presunto_rientro=stazioni3.id LEFT JOIN veicoli WITH(NOLOCK) ON contratti.id_veicolo=veicoli.id LEFT JOIN conducenti As conducenti1 WITH(NOLOCK) ON contratti.id_primo_conducente=conducenti1.id_conducente LEFT JOIN conducenti As conducenti2 WITH(NOLOCK) ON contratti.id_secondo_conducente=conducenti2.id_conducente LEFT JOIN contratti_status WITH(NOLOCK) ON contratti.status=contratti_status.id LEFT JOIN clienti_tipologia WITH(NOLOCK) ON contratti.id_fonte=clienti_tipologia.id  WHERE contratti.attivo='1' ORDER BY contratti.num_contratto ASC"--%>



    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                               ControlToValidate="txtDaData" ErrorMessage="Specificare la data iniziale di pick-up." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="cerca"> </asp:RequiredFieldValidator>
                            
                           <asp:CompareValidator ID="CompareValidator24" runat="server" 
                                ControlToValidate="txtDaData" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per la data iniziale di pick-up'." Type="Date" ValidationGroup="cerca"> </asp:CompareValidator>
                            
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  
                               ControlToValidate="txtAData" ErrorMessage="Specificare la data finale di pick-up." 
                               Font-Size="0pt" SetFocusOnError="True" ValidationGroup="cerca"> </asp:RequiredFieldValidator>
                            
                            <asp:CompareValidator ID="CompareValidator35" runat="server" 
                                ControlToValidate="txtAData" 
                                Font-Size="0pt"  Operator="DataTypeCheck"
                                ErrorMessage="Specificare un valore corretto per la data finale di pick-up'." Type="Date" ValidationGroup="cerca"> </asp:CompareValidator>
                            
                           <%-- <asp:CompareValidator id="CompareValidator4" runat="server"
                                 ControlToValidate="txtDaData"
                                 ControlToCompare="txtAData"
                                 Type= "Date"
                                 Operator = "GreaterThanEqual"
                                 ErrorMessage = "Attenzione : la data finale di pick-up è precedente alla data iniziale."
                                 ValidationGroup="cerca"
                                 Font-Size="0pt"> </asp:CompareValidator>--%>
                                 
    <asp:CompareValidator ID="CompareValidator7" runat="server" 
                    ControlToValidate="dropStazionePickUp" 
                    Operator="GreaterThan" Type="Integer" 
                    ValidationGroup="cerca" ValueToCompare="0" Font-Size="0pt" ErrorMessage="Specificare la stazione di pick-up."> </asp:CompareValidator>
        
    
    <asp:CompareValidator ID="CompareValidator3" runat="server" 
                    ControlToValidate="droptipocliente" 
                    Operator="GreaterThan" Type="Integer" 
                    ValidationGroup="cerca" ValueToCompare="0" Font-Size="0pt" ErrorMessage="Specificare una tipologia di cliente."> </asp:CompareValidator>
        
    

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
  
</div>

</form>


<div style="margin-top:800px;" runat="server" id="div_message">
    <asp:Label ID="lbl_message" runat="server" Text="" Visible="false"></asp:Label>
</div>

</body>
</html>
