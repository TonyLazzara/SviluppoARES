<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="pagamenti.aspx.vb" Inherits="pagamenti"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<%@ Register Assembly="BoxOver" Namespace="BoxOver" TagPrefix="boxover" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <style type="text/css">
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
        .style1
        {
            width: 24px;
        }
        input[type=submit]
        {
          background-color : #369061;
          font-weight:bold;
          color:White;
        }
        .style2
        {
            width: 134px;
        }
        .style3
        {
            width: 87px;
        }
        .style4
        {
            width: 116px;
        }
        .style5
        {
            width: 81px;
        }
        .style6
        {
            width: 100px;
        }

        .testo_td{
            font-family:Verdana, Geneva, Tahoma, sans-serif;
            font-size:13px;
        }
        .testo_td_tit{
            font-family:Verdana, Geneva, Tahoma, sans-serif;
            font-size:14px;

        }
        .bordo
        {
           border: 1px solid #000000;
        }
        
        .WidthColonnaLeft
        {
           text-align: left;
           width:111px;
        }
        
        .WidthColonna2
        {
           text-align: left;
           width:300px;
        }
        
        .WidthColonna3
        {
           text-align: left;
           width:76px;
        }


    </style>

    <%--start multiple submit 23.02.2022 salvo --%>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script type="text/javascript">

        var isSubmitted = false;

        function preventMultipleSubmissions(ele, e, nrco) {
            //

            //alert(ele.value); //test
            //return false;

            var nomeele = ele.value;

            /*if (nomeele == 'Invia RA (OK)') { nomeele='Invia RA' }*/

            //se nrco=0 chiede conferma
            if (nrco == 0) {
                if (confirm('Confermi ' + nomeele + '?') == false) {
                    return false;
                }
            }

            //msg di conferma per il QuickCkIn aggiunto 12.07.2022 salvo
            if (nrco == 2) {
                if (confirm('Attenzione: sei sicuro di voler effettuare il Quick Check In?\nIl contratto non sarà più modificabile.') == false) {
                    return false;
                }
            }

            //msg di conferma per annulla QuickCkIn aggiunto 12.07.2022 salvo
            if (nrco == 3) {
                if (confirm('Attenzione: sei sicuro di voler annullare il Quick Check In?') == false) {
                    return false;
                }
            }

             //msg di conferma per CRV aggiunto 12.07.2022 salvo
            if (nrco == 5) {
                if (confirm('Attenzione: sei sicuro di voler effettuare una sostituzione del veicolo attuale?') == false) {
                    return false;
                }
            }

            //msg di annulla per CRV aggiunto 12.07.2022 salvo
            if (nrco == 6) {
                if (confirm('Attenzione: sei sicuro di voler annullare il CRV attuale?.') == false) {
                    return false;
                }
            }
            




            //} else {




                if (!isSubmitted) {

                    $('body').css('cursor', 'wait');

                    if (nomeele.indexOf("Firma Contratto") > -1 || nomeele.indexOf("Stampa Contratto") > -1 || nomeele.indexOf("Modifica") > -1 || nomeele.indexOf("Invia") > -1 || nomeele.indexOf("Pagamento") > -1 || nomeele.indexOf("Check") > -1 || nomeele.indexOf("CRV") > -1) {

                        $(ele).val('please wait...');
                        $(ele).css('cursor', 'wait');

                    } else {

                        if (ele.value == 'Salva' ) {

                            $(ele).val('please wait...');
                            $(ele).css('cursor', 'wait');
                            /*$(ele).css('width', '160px');*/
                            /*$(ele).css('visibility', 'hidden');*/

                        } else {






                                               
                        }
                    }
                                      

                    isSubmitted = true;
                    return true;

                }
                else {
                    return false;
                }

            }


        //}
    </script>
    <%--end multiple submit--%>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  

   <div id="div_principale" runat="server" visible="true">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <b>&nbsp;Pagamento <asp:Label ID="lb_tipo_pagamento" runat="server" Text="" /></b>
             <asp:Label ID="lb_id_tipo_documento" runat="server" Text="0" Visible="false" />
             <asp:Label ID="lb_num_documento" runat="server" Text="0" Visible="false" />
             <asp:Label ID="lb_id_stazione" runat="server" Text="0" Visible="false" />
             <asp:Label ID="lb_TipoPagamentoContanti" runat="server" Text="0" Visible="false" />
             <asp:Label ID="lb_complimentary_abilitato" runat="server" Text="true" Visible="false" />
             <asp:Label ID="lb_full_credit_abilitato" runat="server" Text="true" Visible="false" />
             <asp:Label ID="lb_importo_suggerito" runat="server" Text="0" Visible="false" />
           </td>
         </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444" >
         <tr>
           <td style="text-align:center">
              <asp:Label ID="txtId" runat="server" Text="Label" Visible="false"></asp:Label>
             <asp:Button ID="bt_pos" runat="server" Text="POS" />
             <asp:Button ID="bt_contanti" runat="server" Text="Contanti/Bonifico" />
             <asp:Button ID="bt_AutorizzazioneTelefonica" runat="server" Text="Autorizzazione Telefonica" /><!-- Carta di Credito Telefonico -->
             <asp:Button ID="bt_full_credit" runat="server" Text="Full Credit" Visible="False" />
             <asp:Button ID="bt_complimentary" runat="server" Text="Complimentary" />
             <asp:Button ID="bt_abbuoni" runat="server" Text="Abbuoni" Visible="False" />
             <asp:Label ID="SceltaCircuito2" runat="server" Text="Label" Visible="False"></asp:Label>
           </td>
         </tr>
        </table>
        <br />
   </div>

   <div id="div_pagamento_pulsantiera" runat="server" visible="false">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td  align="left" style="color: #FFFFFF;background-color:#444;">
             <asp:Label ID="lblCodiceStazione" runat="server" Visible="False" />
             <b>&nbsp;Stazione:</b>
             <asp:Label ID="lb_stazione" runat="server" Text='' />             
             <b>&nbsp;Documento:</b>
             &nbsp;&nbsp;<asp:Label ID="lblProvenienzaDocumento" runat="server" Text='' /><asp:Label ID="lb_documento_riferimento" runat="server" Text='' />
           </td>
         </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
        <tr>
            <td style="width:7%" valign="top">
                <asp:Label ID="Label2" runat="server" Text="Operatore:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lb_operatore" runat="server" Text="" CssClass="testo_bold"></asp:Label>                
            </td>            
        </tr> 
    </table>


    <table border="0" cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444">
        <tr id="tr_StazioneDiPagamento" runat="server">
            <td style="width: 7%;">
                <%--inserita per pagamento su stazione diversa 20.04.2022--%>
                <asp:Label ID="lbl_stazione_pagamento" runat="server" Text="Stazione: " CssClass="testo_bold"></asp:Label>&nbsp;                 
            </td>
            <td>
                <asp:DropDownList ID="ddl_stazioni_pagamento" runat="server" Enabled="true"  CssClass="ddlist"
                    AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                    DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Seleziona per modificare ...</asp:ListItem>    
                </asp:DropDownList>
            </td>
        </tr>       
        <tr>
            <td valign="top">
                <asp:Label ID="Label8" runat="server" Text="Causale:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownTipoPagamento" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="sqlTipoPagamento" DataTextField="descrizione" 
                    DataValueField="ID_TIPPag" AutoPostBack="True">
                    <asp:ListItem Value="0">Seleziona</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" id="modalita_cassa" visible="false" >
            <td valign="top">
                <asp:Label ID="Label1" runat="server" Text="Modalità:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownModalitaPagamento" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="SqlModalitaPagamento" DataTextField="descrizione" 
                    DataValueField="ID_ModPag" AutoPostBack="True">
                    <asp:ListItem Value="0" >Seleziona</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" id="modalita_carta_credito_tel" visible="false" >
            <td valign="top">
                <asp:Label ID="Label7" runat="server" Text="Modalità:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>                                                        
                 <asp:DropDownList ID="DropDownEnti" runat="server" AppendDataBoundItems="True" DataSourceID="SqlEnti" DataTextField="descrizione" DataValueField="ID_ModPag" AutoPostBack="true">
                    <asp:ListItem Value="0" >Seleziona</asp:ListItem>
                 </asp:DropDownList> 
                 &nbsp;&nbsp;<b>Tel.:</b>&nbsp;<asp:Label ID="lb_telefono" runat="server" Text="N.V." CssClass="testo_bold" />
                 &nbsp;&nbsp;<b>Cod. Esercente:</b>&nbsp;<asp:Label ID="lb_codice_esercente" runat="server" Text="N.V." CssClass="testo_bold" />
                 &nbsp;&nbsp;<b>Autorizzazione:</b>&nbsp;<asp:Label ID="lb_durata_autorizzazione" runat="server" Text="N.V." CssClass="testo_bold" />                                                                             
            </td>
        </tr>
        <tr runat="server" id="tr_autorizzazione_telefonica" visible="false" >
            <td valign="top">
                <asp:Label ID="Label10" runat="server" Text="Autorizzazione:" CssClass="testo_bold"></asp:Label>                
            </td>
            <td>
                <asp:DropDownList ID="DropDownAutorizzazioni" runat="server" AppendDataBoundItems="True" 
                        DataSourceID="SqlAutorizzazioni" DataTextField="nr_aut" DataValueField="ID_CTR" AutoPostBack="true">
                        <asp:ListItem Value="0" >Seleziona</asp:ListItem>
                </asp:DropDownList>        
                &nbsp;&nbsp;<b>Scadenza:</b>&nbsp;<asp:Label ID="lb_scadenza_autorizzazione" runat="server" Text="N.V." CssClass="testo_bold" />
                &nbsp;&nbsp;<b>Importo Autorizzato:</b>&nbsp;<asp:Label ID="lb_importo_autorizzato" runat="server" Text="N.V." CssClass="testo_bold" />        
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="Label14" runat="server" Text="Data:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_data" runat="server" Width="80px" Enabled="False"></asp:TextBox>
                <%--<a onclick="Calendar.show(document.getElementById('<%=tx_data.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox ID="tx_data" runat="server" Width="80px"></asp:TextBox></a>              
                <ajaxtoolkit:maskededitextender runat="server" Mask="99/99/9999"  
                    TargetControlID="tx_data" MaskType="Date" CultureDatePlaceholder="" 
                    CultureTimePlaceholder="" CultureDecimalPlaceholder="" 
                    CultureThousandsPlaceholder="" CultureDateFormat="" 
                    CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" 
                    ID="MaskedEditExtender4">
                </ajaxtoolkit:maskededitextender>--%>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:Label ID="Label15" runat="server" Text="Importo:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_importo" runat="server" Width="70px" onKeyPress="javascript:return filterInputDouble(event)" ></asp:TextBox>
                &nbsp;&nbsp;<asp:Button ID="btnAbbuoni" runat="server" Text="Abbuono" visible="false"/>
                <asp:Label ID="lblTipoPagamentiAbbuoni" runat="server" Text="Tipo Pagamento:" CssClass="testo_bold" Visible="False"></asp:Label>
                <asp:DropDownList ID="dropTipoPagamentoAbbuoni" runat="server" AppendDataBoundItems="True" DataSourceID="sqlTipoPagamentoAbbuoni" DataTextField="descrizione" DataValueField="ID_TIPPag" Visible="False">
                    <asp:ListItem Value="0">Seleziona</asp:ListItem>
                </asp:DropDownList> 
                &nbsp;&nbsp;
                <asp:Label ID="lblImportoAbbuoni" runat="server" Text="Importo:" CssClass="testo_bold" Visible="False"></asp:Label>               
                &nbsp;
                <asp:TextBox ID="txtImportoAbbuoni" runat="server" Width="70px" onKeyPress="javascript:return filterInputDouble(event)" Visible="False" ></asp:TextBox>
            </td>                       
        </tr>
        <tr  runat="server" id="tr_01" visible="false" >
            <td valign="top">
                <asp:Label ID="Label4" runat="server" Text="Nr° C/Credito:" CssClass="testo_bold"></asp:Label>
            </td>
            <td valign="top">
                <asp:TextBox ID="tx_titolo" runat="server" Width="180px" onKeyPress="javascript:return filterInputInt(event)" ></asp:TextBox>
                &nbsp;&nbsp;&nbsp;<asp:Label ID="Label5" runat="server" Text="Mese Scadenza:" CssClass="testo_bold"></asp:Label>
                &nbsp;<asp:TextBox ID="tx_mese_scadenza" runat="server" Width="20px" MaxLength="2" onKeyPress="javascript:return filterInputInt(event)" ></asp:TextBox>
                &nbsp;<asp:Label ID="Label9" runat="server" Text="Anno Scadenza:" CssClass="testo_bold"></asp:Label>
                &nbsp;<asp:TextBox ID="tx_anno_scadenza" runat="server" Width="20px" MaxLength="2" onKeyPress="javascript:return filterInputInt(event)" ></asp:TextBox>
            </td>
        </tr>
        <tr  runat="server" id="tr_02" visible="false" >
            <td valign="top">
                <asp:Label ID="Label6" runat="server" Text="Autorizzazione:" CssClass="testo_bold"></asp:Label>
            </td>
            <td valign="top">
                <asp:TextBox ID="tx_num_autorizzazione" runat="server" Width="70px" MaxLength="8" ></asp:TextBox>
                <%--&nbsp;&nbsp;&nbsp;<asp:Label ID="Label7" runat="server" Text="Batch:" CssClass="testo_bold"></asp:Label>
                &nbsp;<asp:TextBox ID="tx_num_batch" runat="server" Width="120px" MaxLength="20" onKeyPress="javascript:return filterInputInt(event)" ></asp:TextBox>--%>
            </td>
        </tr>
        <%--<tr id="tr_03" runat="server">
            <td valign="top">
                <asp:Label ID="Label10" runat="server" Text="Cassa:" CssClass="testo_bold"></asp:Label>
            </td>
            <td valign="top">
                <asp:TextBox ID="tx_cassa" runat="server" Width="70px" onKeyPress="javascript:return filterInputInt(event)" ></asp:TextBox>
                &nbsp;&nbsp;&nbsp;<asp:Label ID="Label11" runat="server" Text="Nr° Preautorizzazione:" CssClass="testo_bold"></asp:Label>
                &nbsp;<asp:TextBox ID="tx_num_preautorizzazione" runat="server" Width="120px" onKeyPress="javascript:return filterInputInt(event)" ></asp:TextBox>
            </td>
        </tr>
        <tr id="tr_04" runat="server">
            <td valign="top">
                <asp:Label ID="Label12" runat="server" Text="Terminal ID:" CssClass="testo_bold"></asp:Label>
            </td>
            <td valign="top">
                <asp:TextBox ID="tx_terminal_id" runat="server" Width="120px" onKeyPress="javascript:return filterInputInt(event)" ></asp:TextBox>
                &nbsp;&nbsp;&nbsp;<asp:Label ID="Label13" runat="server" Text="Data Operazione:" CssClass="testo_bold"></asp:Label>
                &nbsp;<asp:TextBox ID="tx_data_operazione" runat="server" Width="70px" Enabled="false" ></asp:TextBox>
                &nbsp;<asp:Label ID="Label17" runat="server" Text="Ora Operazione:" CssClass="testo_bold"></asp:Label>
                &nbsp;<asp:TextBox ID="tx_ora_operazione" runat="server" Width="40px" Enabled="false" ></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
            <td valign="top">
                <asp:Label ID="Label16" runat="server" Text="Note:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tx_nota" runat="server" Width="900px" Height="75px" TextMode="MultiLine" ></asp:TextBox>
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="bt_invia_mail_incasso" runat="server" Text="Invia Mail Incasso" Visible="false" ValidationGroup="SalvaRigaCartaCredito" />
                <asp:Button ID="bt_stampa_modulo" runat="server" Text="Stampa Modulo" Visible="false" ValidationGroup="SalvaRigaCartaCredito" />
                <asp:Button ID="bt_salva_riga_cassa" runat="server" Text="Salva" ValidationGroup="SalvaRigaCassa" />
                <asp:Button ID="bt_modulo_vuoto" runat="server" Text="Modulo Vuoto" Visible="false" />
                <asp:Button ID="bt_stampa_riga_cassa" runat="server" Text="Stampa Ricevuta" />
                <% If Not Session("byPassControllo") Then%>
                <asp:Button ID="bt_chiudi_riga_cassa" runat="server" Text="Chiudi" />
                <% Else%>    
                <asp:Button ID="Button2" runat="server" Text="Chiudi" Enabled="False" />
                <% End If%>
            </td>
        </tr>
    </table> 
</div>

   <div class="container" runat="server" id="div_contenitore_pos" visible="false">
	  <div runat="server" id="div_pos" visible="true" style="float:left; background-color:#FFFFC6; height:325px; ">
      <table style="width: 1%;" border="0">        
        <tr>
          <td style="text-align:center;background-color: #000000; color:#ffffff;">
              <asp:Label ID="lblModalita" runat="server" Text="Label" style="font-family: arial, Helvetica, sans-serif; font-size:large;"></asp:Label>          
          </td>
        </tr>
        <tr>
            <td style="text-align:center;background-color: #ffffff" class="bordo">
                <asp:Label ID="lblNumeroDocumento" runat="server" Text="Numero Documento" style="font-family: arial, Helvetica, sans-serif; font-size:large;"></asp:Label>                
            </td>
        </tr>        
        <!-- Ente -->
        <tr>
            <td>
                 <asp:ListView ID="listEnti" runat="server" DataSourceID="sqlEnti" DataKeyNames="id">
                    <ItemTemplate>
                        <tr style="background-color:#a4a4a4; color: #000000;" align="center">                 
                            <td class="testo"  style="text-align:center;" >    
                                <asp:Label ID="ID" runat="server" Text='<%# Eval("id") %>' Visible="false" />            
                                <asp:Button ID="btnEnte" runat="server" Text='<%# Eval("nome") %>' Width="230" CommandName="Modifica" />                                                                       
                            </td>                                                                                                                                            
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>                        
                        <tr style="background-color:#a4a4a4; color: #000000;" align="center">                 
                            <td class="testo"  style="text-align:center;" >      
                                <asp:Label ID="ID" runat="server" Text='<%# Eval("id") %>' Visible="false" />             
                                <asp:Button ID="btnEnte" runat="server" Text='<%# Eval("nome") %>' Width="230" CommandName="Modifica"  />
                            </td>                                                                                                                                            
                        </tr>                                                                                                                                                                          
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table2" runat="server" style="">
                            <tr>
                                <td>
                                    Non è stato restituito alcun dato.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table ID="itemPlaceholderContainer" runat="server" class="table table-bordered">
                                        <tr id="Tr3" runat="server" style="color: #FFFFFF;background-color: #000000" class="sfondo_rosso">  
                                            <th id="Th4" runat="server" align="left" class="testo_titolo" >                                        
                                                <%--<asp:LinkButton ID="LinkButton4" runat="server" CommandName="order_by_Nominativo" CssClass="testo" Style="color:White !important;">Enti</asp:LinkButton>--%>
                                                <asp:Label ID="lbletichettaEnte" runat="server" CommandName="order_by_Nominativo" CssClass="testo" Style="color:White !important; font-size:large;">Ente</asp:Label>
                                            </th>                                          
                                        </tr>
                                        <tr ID="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>                    
                        </table>
                    </LayoutTemplate>                 
                </asp:ListView>            
            </td>
        </tr>     
        <!-- Circuito -->   
        <tr>
            <td>
              <div style="margin-top:-6px;">
                 <asp:ListView ID="listCircuiti" runat="server" DataSourceID="sqlCircuiti" DataKeyNames="nome" Visible="False">
                    <ItemTemplate>
                        <tr style="background-color:#a4a4a4; color: #000000;" align="center">                 
                            <td class="testo"  style="text-align:center;" >    
                                <%--<asp:Label ID="ID" runat="server" Text='<%# Eval("id") %>' Visible="false" />            --%>
                                <asp:Button ID="btnEnte" runat="server" Text='<%# Eval("nome") %>' Width="230" CommandName="Modifica"  />                                                                      
                            </td>                                                                                                                                            
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>                        
                        <tr style="background-color:#a4a4a4; color: #000000;" align="center">                 
                            <td class="testo"  style="text-align:center;" >      
                                <%--<asp:Label ID="ID" runat="server" Text='<%# Eval("id") %>' Visible="false" />             --%>
                                <asp:Button ID="btnEnte" runat="server" Text='<%# Eval("nome") %>' Width="230" CommandName="Modifica"  />
                            </td>                                                                                                                                            
                        </tr>                                                                                                                                                                          
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table2" runat="server" style="">
                            <tr>
                                <td>
                                    Non è stato restituito alcun dato.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table ID="itemPlaceholderContainer" runat="server" class="table table-bordered">
                                        <tr id="Tr3" runat="server" style="color: #FFFFFF;background-color: #000000" class="sfondo_rosso">  
                                            <th id="Th4" runat="server" align="left" class="testo_titolo" >                                        
                                                <%--<asp:LinkButton ID="LinkButton4" runat="server" CommandName="order_by_Nominativo" CssClass="testo" Style="color:White !important;">Enti</asp:LinkButton>--%>
                                                <asp:Label ID="lbletichettaCircuito" runat="server" CommandName="order_by_Nominativo" CssClass="testo" Style="color:White !important; font-size:large;">Circuito</asp:Label>
                                            </th>                                          
                                        </tr>
                                        <tr ID="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>                    
                        </table>
                    </LayoutTemplate>                 
                </asp:ListView>
              </div>            
            </td>
        </tr>
        <!-- Funzionalità -->
        <tr>
            <td>
              <div style="margin-top:-6px;">
                <asp:ListView  ID="lisFunzionalità" runat="server" DataSourceID="sqlFunzioni" DataKeyNames="Funzione" Visible="False">
                    <ItemTemplate>
                        <tr style="background-color:#a4a4a4; color: #000000;" align="center">                 
                            <td class="testo"  style="text-align:center;" >    
                                <%--<asp:Label ID="ID" runat="server" Text='<%# Eval("id") %>' Visible="false" />            --%>
                                <asp:Button ID="btnFunzione" runat="server" Text='<%# Eval("Funzione") %>' Width="230" CommandName="Modifica"  />                                                                      
                            </td>                                                                                                                                            
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>                        
                        <tr style="background-color:#a4a4a4; color: #000000;" align="center">                 
                            <td class="testo"  style="text-align:center;" >      
                                <%--<asp:Label ID="ID" runat="server" Text='<%# Eval("id") %>' Visible="false" />             --%>
                                <asp:Button ID="btnFunzione" runat="server" Text='<%# Eval("Funzione") %>' Width="230" CommandName="Modifica"  />
                            </td>                                                                                                                                            
                        </tr>                                                                                                                                                                          
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table2" runat="server" style="">
                            <tr>
                                <td>
                                    Non è stato restituito alcun dato.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <table class="table table-bordered">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table ID="itemPlaceholderContainer" runat="server" class="table table-bordered">
                                        <tr id="Tr3" runat="server" style="color: #FFFFFF;background-color: #000000" class="sfondo_rosso">  
                                            <th id="Th4" runat="server" align="left" class="testo_titolo" >                                        
                                                <%--<asp:LinkButton ID="LinkButton4" runat="server" CommandName="order_by_Nominativo" CssClass="testo" Style="color:White !important;">Enti</asp:LinkButton>--%>
                                                <asp:Label ID="lbletichettaCircuito" runat="server" CommandName="order_by_Nominativo" CssClass="testo" Style="color:White !important; font-size:large;">Funzionalità</asp:Label>
                                            </th>                                          
                                        </tr>
                                        <tr ID="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
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
      <div runat="server" id="divImmagine" style="font-family:Arial;font-size:18px;background-color: #ffffff;width: 75%;float:left;text-align: center;margin-left: 5px;" visible="true">
        <%--Scegli l'ente con il quale operare--%>
        <img alt="CartaCredito"  src="images/CartaCredito.jpg" style="width: 450px; height: 284px" />
       <% If Not Session("byPassControllo") Then%>
        <div style="width:100%">
            <asp:Button ID="btnChiudiPos" runat="server" Text="Chiudi"  style="margin-bottom: 20px;" Width="100px" Enabled="true" />
        </div> 
        <% Else%>    
            <div style="width:100%">
            <asp:Button ID="Button1" runat="server" Text="Chiudi"  style="margin-bottom: 20px;" Width="100px" Enabled="false" />
        </div> 
        <% End If%>
        
          
      </div>
      <div runat="server" id="divIntestazione" visible="false">
        <div class="Intestazione1" style ="text-align:center;background-color: #19191b;color: White;font-weight: bold;font-family: Arial;font-size: 20px;">                
            <asp:Label ID="lblIntestazione" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblTestoCircuito" runat="server" Text="" Visible="False"></asp:Label>
        </div>
        <div runat="server" id="div1" style="font-family:auto;font-size:15px;background-color: #ffffff;width: 75%;float:left;text-align: center;margin-left: 5px;" visible="true">      
          <table cellpadding="5" cellspacing="5" style="width:100%;">                
            <tr style="text-align:left;">
                <td>
                    <asp:Label ID="lblStazione" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="lblStazioneCodice" runat="server" Text="Label" Visible="False"></asp:Label>
                    <asp:Label ID="lblStazioneID" runat="server" Text="Label" Visible="False"></asp:Label>
                    
                    <%--inserita per pagamento su stazione diversa  per Angela 02.08.2022 --%>                    
                    <asp:DropDownList ID="ddl_stazioni_pagamento2" runat="server" Enabled="true"  CssClass="ddlist"
                        AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                        DataValueField="id">
                        <asp:ListItem Selected="True" Value="0">Seleziona per modificare ...</asp:ListItem>    
                    </asp:DropDownList>                   
                </td>                                                                                                                      
             </tr>          
            </table> 
            <table cellpadding="5" cellspacing="5" style="width:100%;">                            
             <tr style="text-align:left;">
                <td class="WidthColonnaLeft">Importo:</td>
                    <td>
                        <asp:TextBox ID="txtImporto" runat="server" Width="140px" onKeyPress="javascript:return filterInputDouble(event)" ></asp:TextBox>
                        <asp:Label ID="lErrImporto" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>                                                                                                    
                    </td>                                                
             </tr>                                                  
        </table>          
      </div>  
        
        <!-- Carte -->

        <div runat="server" id="divCampiAcquistoCarte" style="font-family:auto;font-size:15px;background-color: #ffffff;width: 75%;float:left;text-align: center;margin-left: 5px;" visible="false">
             <table cellpadding="5" cellspacing="5" style="width:100%;">                 
                <tr style="text-align:left;">
                    <td class="WidthColonnaLeft">Titolare:</td>                        
                    <td>
                        <asp:TextBox ID="txtTitolareCarta" runat="server" Width="286px" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td>Numero Carta:</td>
                    <td class="WidthColonna2">
                        <asp:TextBox ID="txtNumerodiCarta" runat="server" Width="286px"></asp:TextBox>                            
                    </td> 
                    <td class="WidthColonna3">Scadenza:</td>     
                    <td>
                        <asp:TextBox ID="txtScadenzaMese" runat="server" MaxLength="2" Width="44px" onKeyPress="javascript:return filterInputDouble(event)"></asp:TextBox>
                        <asp:Label ID="Label3" runat="server" Text="/"></asp:Label>
                        <asp:TextBox ID="txtScadenzaAnno" runat="server" MaxLength="2" Width="44px" onKeyPress="javascript:return filterInputDouble(event)"></asp:TextBox>
                    </td>                           
               </tr>                                                                                                    
             </table>
        </div>

        <div runat="server" id="divCampiPreautorizzazioneCarte" style="font-family:auto;font-size:15px;background-color: #ffffff;width: 75%;float:left;text-align: center;margin-left: 5px;" visible="false">
             <table cellpadding="5" cellspacing="5" style="width:100%;">                 
                <tr style="text-align:left;">
                    <td style="width:111px !Important;">Titolare:</td>                        
                    <td>
                        <asp:TextBox ID="txtTitolareCartaPreautorizzazione" runat="server" Width="286px" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr style="text-align:left;">
                    <td >Numero Carta:</td>
                    <td class="WidthColonna2">
                        <asp:TextBox ID="txtNumerodiCartaPreautorizzazione" runat="server" Width="286px"></asp:TextBox>                            
                    </td> 
                    <td class="WidthColonna3">Scadenza:</td>     
                    <td>
                        <asp:TextBox ID="txtScadenzaMesePreautorizzazione" runat="server" MaxLength="2" Width="44px" onKeyPress="javascript:return filterInputDouble(event)"></asp:TextBox>
                        <asp:Label ID="Label13" runat="server" Text="/"></asp:Label>
                        <asp:TextBox ID="txtScadenzaAnnoPreautorizzazione" runat="server" MaxLength="2" Width="44px" onKeyPress="javascript:return filterInputDouble(event)"></asp:TextBox>
                    </td>                           
               </tr>     
               <tr style="text-align:left;">
                    <td >Preautorizzazione:</td> 
                    <td>
                        <asp:TextBox ID="txtPreautorizzazioneCarte" runat="server" MaxLength="16" Width="286px"></asp:TextBox>                        
                    </td>
                    <td class="WidthColonna3">Codice Auth</td> 
                    <td>
                        <asp:TextBox ID="txtCodiceAuthPreautorizzazione" runat="server" MaxLength="6" Width="55px"></asp:TextBox>                        
                    </td>                                                   
               </tr>       
                                                                                           
             </table>
        </div>

        <div runat="server" id="divCampiChiusuraPreautorizzazioneCarte" style="font-family:auto;font-size:15px;background-color: #ffffff;width: 75%;float:left;text-align: center;margin-left: 5px;" visible="false">
             <table cellpadding="5" cellspacing="5" style="width:100%;">                 
                <tr style="text-align:left;">
                    <td style="width: 101px;">Preautorizzazione:</td>
                    <td>
                        <%--<asp:DropDownList ID="dropChiusuraPreautorizzazione" runat="server" DataTextField="contro_cassa" DataValueField="ID" style="font:monaco,courier,monospace;"></asp:DropDownList>--%>
                        <asp:DropDownList ID="dropChiusuraPreautorizzazione" runat="server" 
                            class="form-control" style="font:monaco,courier,monospace;"
                                AppendDataBoundItems="True" DataSourceID="SqlPreautorizzazione" DataTextField="testo" 
                                DataValueField="ID_CTR" Width="50%" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lErrPreautorizzazione" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                    </td>            
                </tr>                                                                
                <tr style="text-align:left;">
                    <td style="width: 101px;">Note:</td>                        
                    <td>
                        <asp:TextBox ID="txtNoteChiusuraPreautorizzazioneCarte" runat="server" 
                            Width="573px" MaxLength="255"></asp:TextBox>
                    </td>                     
                </tr>                                                                                                                                                                                        
             </table>
        </div>

        <div runat="server" id="divCampiRimborsoCarte" style="font-family:auto;font-size:15px;background-color: #ffffff;width: 75%;float:left;text-align: center;margin-left: 5px;" visible="false">
             <table cellpadding="5" cellspacing="5" style="width:100%;">                 
                <%--<tr style="text-align:left;">
                    <td style="width: 101px;">Preautorizzazione:</td>
                    <td>
                        <asp:DropDownList ID="dropRimborsoCarte" runat="server" DataTextField="contro_cassa" DataValueField="ID" style="font:monaco,courier,monospace;"></asp:DropDownList>
                         <asp:Label ID="lErrPreautorizzazioneRimborso" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                    </td>            
                </tr>    --%>                                                            
                <tr style="text-align:left;">
                    <td style="width: 101px;">Note:</td>                        
                    <td>
                        <asp:TextBox ID="txtNoteRimborsoCarte" runat="server" Width="570px" 
                            MaxLength="255"></asp:TextBox>
                    </td>                     
                </tr>                                                                                                                                                                                        
             </table>
        </div>


        <!-- Bancomat -->

        <div runat="server" id="divCampiAcquistoBancomat" style="font-family:auto;font-size:15px;background-color: #ffffff;width: 75%;float:left;text-align: center;margin-left: 5px;" visible="false">
             <table cellpadding="5" cellspacing="5" style="width:100%; height:77px;">                             
               <tr style="text-align:left;">
                    <td class="WidthColonnaLeft">Titolare:</td>                        
                    <td style="width:300px;">
                        <asp:TextBox ID="txtTitolareCartaAcquistoBancomat" runat="server" Width="286px" MaxLength="50"></asp:TextBox>
                    </td>
                    <td class="WidthColonna3">Scadenza:</td>     
                    <td>
                        <asp:TextBox ID="txtScadenzaMeseAcquistoBancomat" runat="server" MaxLength="2" Width="44px" onKeyPress="javascript:return filterInputDouble(event)"></asp:TextBox>
                        <asp:Label ID="Label18" runat="server" Text="/"></asp:Label>
                        <asp:TextBox ID="txtScadenzaAnnoAcquistoBancomat" runat="server" MaxLength="2" Width="44px" onKeyPress="javascript:return filterInputDouble(event)"></asp:TextBox>
                    </td>  
                </tr>
                <tr style="text-align:left;">
                    <td style="width: 101px;">&nbsp;</td> 
                    <td>                        
                        <asp:Label ID="Label17" runat="server" Text=""></asp:Label>
                    </td>                                                   
               </tr>                                                                                                                                                                                     
             </table>
        </div>

        <div runat="server" id="divCampiDepositoBancomat" style="font-family:auto;font-size:15px;background-color: #ffffff;width: 75%;float:left;text-align: center;margin-left: 5px;" visible="false">
             <table cellpadding="5" cellspacing="5" style="width:100%;">                 
                <tr style="text-align:left;">
                    <td style="width: 101px;">Titolare:</td>                        
                    <td>
                        <asp:TextBox ID="txtTitolareCartaDepositoBancomat" runat="server" Width="286px" MaxLength="50"></asp:TextBox>
                    </td>
                    <td style="width: 111px;">Scadenza:</td> 
                    <td>
                        <asp:TextBox ID="txtScadenzaMeseDepositoBancomat" runat="server" MaxLength="2" Width="44px"></asp:TextBox>
                        <asp:Label ID="Label12" runat="server" Text="/"></asp:Label>
                        <asp:TextBox ID="txtScadenzaAnnoDepositoBancomat" runat="server" MaxLength="2" Width="44px"></asp:TextBox>
                    </td> 
                </tr>                
               <tr style="text-align:left;">
                    <td style="width: 111px;">Codice Auth:</td> 
                    <td>
                        <asp:TextBox ID="txtCodiceAuthDepositoBancomat" runat="server" MaxLength="6" Width="55px"></asp:TextBox>                        
                    </td> 
                    <%--<td style="width: 111px;">Codice PAN:</td> 
                    <td>
                        <asp:TextBox ID="txtCodicePANDepositoBancomat" runat="server" MaxLength="4" onKeyPress="javascript:return filterInputDouble(event)" Width="44px"></asp:TextBox>                        
                    </td> --%>                                                  
               </tr>                                                                                                    
             </table>
        </div>

        <div runat="server" id="divCampiRimborsoDepositoBancomat" style="font-family:auto;font-size:15px;background-color: #ffffff;width: 75%;float:left;text-align: center;margin-left: 5px; height:77px;" visible="false">
             <table cellpadding="5" cellspacing="5" style="width:100%;">                                                                                             
                <tr style="text-align:left;">
                    <td style="width: 111px;">Codice Auth:</td>
                    <td>                        
                        <asp:DropDownList ID="dropCodiceAuthRimborso" runat="server" 
                            class="form-control" style="font:monaco,courier,monospace;"
                                AppendDataBoundItems="True" DataSourceID="SqlDepositoBM" DataTextField="testo" 
                                DataValueField="ID_CTR" Width="50%" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                        </asp:DropDownList>
                         <asp:Label ID="lErrCodiceAuthRimborso" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                    </td>            
                </tr>    
                <tr style="text-align:left;">
                    <td style="width: 111px;">Note:</td>                        
                    <td>
                        <asp:TextBox ID="txtNoteRimborsoDepositoBancomat" runat="server" Width="600px" MaxLength="255" TextMode="SingleLine"></asp:TextBox>
                    </td>                     
                </tr>                                                                                                                                                                                        
             </table>
        </div>

        <div runat="server" id="divCampiRimborsoBancomat" style="font-family:auto;font-size:15px;background-color: #ffffff;width: 75%;float:left;text-align: center;margin-left: 5px;" visible="false">
             <table cellpadding="5" cellspacing="5" style="width:100%;">                                                                                             
                <tr style="text-align:left;">
                    <td style="width: 111px;">Note:</td>                        
                    <td>
                        <asp:TextBox ID="txtNoteRimborsoBancomat" runat="server" Width="600px" MaxLength="255" TextMode="SingleLine"></asp:TextBox>
                    </td>                     
                </tr>                                                                                                                                                                                        
             </table>
        </div>

        <div runat="server" id="divChiudiPos2" style="font-family:auto;font-size:15px;background-color: #ffffff;width: 75%;float:left;text-align: center;margin-left: 5px; height:152px;" visible="true">
            <asp:Button ID="bt_salva_riga_cassa2" runat="server" Text="Salva" style="margin-top: 120px;" Width="100px"  OnClientClick="return preventMultipleSubmissions(this, event, 1);" Visible="False"/>            
            <% If Not Session("byPassControllo") Then%>
            <asp:Button ID="btnChiudiPos2" runat="server" Text="Chiudi"  style="margin-top: 120px;" Width="100px"/>
            <% Else%>    
            <asp:Button ID="Button3" runat="server" Text="Chiudi"  style="margin-top: 120px;" Width="100px" Enabled="False" />
            <% End If%>
        </div>          
        
                                
      </div>
    </div>
   

<asp:SqlDataSource ID="sqlTipoPagamento" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="">
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlModalitaPagamento" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlEnti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, nome FROM [POS_enti_proprietari] WITH(NOLOCK)   ORDER BY nome">
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlAutorizzazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlStazioni" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT stazioni.id, (codice + ' ' + nome_stazione) As stazione FROM stazioni WITH(NOLOCK) where attiva=1 ORDER BY codice">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlTipoPagamentoAbbuoni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlCircuiti" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT * from pos_circuiti WITH(NOLOCK)  ORDER BY nome">
</asp:SqlDataSource>

<asp:SqlDataSource ID="sqlFunzioni" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="">
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlPreautorizzazione" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT ID_CTR, '| ' + CONVERT(varchar, data, 120) + ' | ' + CONVERT(varchar, PER_IMPORTO)  as testo FROM PAGAMENTI_EXTRA WITH(NOLOCK)  ORDER BY ID_CTR">
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDepositoBM" runat="server" 
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT ID_CTR, '| ' + CONVERT(varchar, data, 120) + ' | ' + CONVERT(varchar, PER_IMPORTO)  as testo FROM PAGAMENTI_EXTRA WITH(NOLOCK)  ORDER BY ID_CTR">
</asp:SqlDataSource>

<asp:Label ID="IdStazione" runat="server" Text="Label" Visible="false"></asp:Label>

<asp:Label ID="lblEnteScelto" runat="server" Text="Label" Visible="false"></asp:Label>
<asp:Label ID="lblCircuitoScelto" runat="server" Text="Label" Visible="false"></asp:Label>
<asp:Label ID="lblFunzioneScelta" runat="server" Text="Label" Visible="false"></asp:Label>

</asp:Content>

