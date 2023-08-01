<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="true" CodeFile="commissioni_operatore.aspx.vb" Inherits="commissioni_operatore" Buffer="true"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>ARES - Sicily Rent Car</title>

    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />

    <%--per accordion 08.08.2022 salvo --%>
    <link rel="stylesheet" href="include/css/uikit.css" />   
    <script type="text/javascript"  src="include/js/jquery.js"></script>
    <script type="text/javascript" src="include/js/uikit.js"></script>
    <script type="text/javascript" src="include/js/components/accordion.js"></script>



    <script type="text/javascript">

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
              
                            
   </script>


     <script type="text/javascript" src="calendar/calendar.js"></script>
     <style type="text/css">  
        .CalendarCSS  
        {  
            background-color: #3a6ea5;  
            color:Snow;            

            }  

        .tr1{
            height:26px;
        }
        .td_perc{
            width:120px;
        }
        .td_operatore{
            width:170px;
        }
           .td_date{
            width:120px;
        }

       #hideAll { position: fixed; left: 0px; right: 0px; top: 0px; bottom: 0px; background-color: white; z-index: 99; /* Higher than anything else in the document */ }

       #loading-mask { background-color: white; height: 100%; left: 0; position: fixed; top: 0; width: 100%; z-index: 9999; } 

    </style>  

     <script type="text/javascript">

         function vis(id) {
             if (id == 0){
                 document.getElementById('tr_ck_accessori').style.visibility = 'hidden';
             } else {
                 document.getElementById('tr_ck_accessori').style.visibility = 'visible';
             }             
         }

         /*window.onload = function () { document.getElementById("div_ckaccessori").style.display = "none"; } */
         jQuery('#div_ckaccessori').css("overflow-y", "scroll");

         /*window.onload = function () { document.getElementById('loading-mask').style.display = 'none'; } */

     </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
   


        <asp:UpdateProgress ID="UpdateProgress1" runat="server"  >
            <ProgressTemplate>
                <div style="position:absolute;margin-left:270px; margin-top:120px;opacity:1.9;">
                  <img alt="" src="images/progress5.gif" /></div>
            </ProgressTemplate>
         </asp:UpdateProgress>

       <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">

      <ContentTemplate>
    

     <div style="width:auto;background-color:#444;color:#ffffff;">
         <asp:Label ID="Label14" runat="server" Text="Calcolo Commissioni Operatore" CssClass="testo_titolo"></asp:Label>
     </div>

       <%--<table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
            <td align="left" style="color: #FFFFFF;background-color:#444;" class="style1">
                
            </td>
            </tr>
        </table>--%>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="min-height:auto;" >
            <tr class="tr1">
                <td>
                   <asp:Label ID="Label37" runat="server" Text="Operatore" CssClass="testo_bold  "></asp:Label>
                </td>
                <td align="center">
                   <asp:Label ID="Label2" runat="server" Text="% su Commissione" CssClass="testo_bold"></asp:Label>
                </td>
                <td align="center" style="width:160px;">
                   <asp:Label ID="Label1" runat="server" Text="% su Imponibile" CssClass="testo_bold"></asp:Label>
                </td>
                <td align="center">
                   <asp:Label ID="Label3" runat="server" Text="Da Data Pick-up" CssClass="testo_bold"></asp:Label>
                </td>
                <td align="center">
                   <asp:Label ID="Label4" runat="server" Text="A Data Pick-up" CssClass="testo_bold"></asp:Label>
                </td>
                <td align="center"><asp:Label ID="Label11" runat="server" Text="Da Data Drop Off" CssClass="testo_bold"></asp:Label></td>
                <td align="center" ><asp:Label ID="Label13" runat="server" Text="A Data Drop Off" CssClass="testo_bold"></asp:Label></td>


            </tr>

            <tr>
                <td class="td_operatore">
                  <asp:DropDownList ID="dropCercaOperatori" runat="server"
                    AppendDataBoundItems="True" DataSourceID="sqlOperatori" DataTextField="descrizione" 
                    DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                 </asp:DropDownList>
                </td>
                <td align="center" class="td_perc">
                   <asp:TextBox ID="txtPercentualeCommissione" runat="server" Width="30px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
                </td>
                <td align="center" class="td_perc">
                   <asp:TextBox ID="txtPercentualeImponibile" runat="server" Width="30px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
                </td>
                <td align="center" class="testo_bold td_date  ">
                    <a onclick="Calendar.show(document.getElementById('<%=txtCercaPickUpDa.ClientID%>'), '%d/%m/%Y', false)">
                    <asp:TextBox runat="server" Width="70px" ID="txtCercaPickUpDa" ></asp:TextBox>
                      </a>    
               
                    <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                              CultureDatePlaceholder="" CultureTimePlaceholder="" 
                              CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                              CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                              CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaDropOffDa" 
                              ID="MaskedEditExtender1">
                    </ajaxtoolkit:MaskedEditExtender>
                </td>
                <td align="center" class="testo_bold  td_date ">
                    <a onclick="Calendar.show(document.getElementById('<%=txtCercaPickUpA.ClientID%>'), '%d/%m/%Y', false)">
                    <asp:TextBox runat="server" Width="70px" ID="txtCercaPickUpA" ></asp:TextBox>
                      </a>    
              

                    <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                              CultureDatePlaceholder="" CultureTimePlaceholder="" 
                              CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                              CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                              CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaDropOffDa" 
                              ID="MaskedEditExtender2">
                    </ajaxtoolkit:MaskedEditExtender>
                </td>
                   <td align="center" class=" td_date ">
                     <a onclick="Calendar.show(document.getElementById('<%=txtCercaDropOffDa.ClientID%>'), '%d/%m/%Y', false)">
                    <asp:TextBox runat="server" Width="70px" ID="txtCercaDropOffDa" ></asp:TextBox>
                      </a>                   

                    <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                              CultureDatePlaceholder="" CultureTimePlaceholder="" 
                              CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                              CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                              CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaDropOffDa" 
                              ID="txtCercaPickUpDa0_MaskedEditExtender0">
                    </ajaxtoolkit:MaskedEditExtender>
                </td>
                <td align="center" class=" td_date ">
                    <a onclick="Calendar.show(document.getElementById('<%=txtCercaDropOffA.ClientID%>'), '%d/%m/%Y', false)">
                       <asp:TextBox runat="server" Width="70px" ID="txtCercaDropOffA" ></asp:TextBox>
                    </a>
                              
                    <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                              CultureDatePlaceholder="" CultureTimePlaceholder="" 
                              CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                              CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                              CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaDropOffA" 
                              ID="txtCercaPickUpDa1_MaskedEditExtender">
                    </ajaxtoolkit:MaskedEditExtender>
                </td>
             
            </tr>

           
            <tr >
                <td align="center" colspan="3"></td>
             
            </tr>


            <tr style="height:44px;">
              <td align="center" colspan="7">
                 <br />&nbsp;
                 <asp:Button ID="btnCerca" runat="server" Text="Cerca" Visible="True"  UseSubmitBehavior="False"  /> &nbsp;&nbsp;&nbsp;
                 <asp:Button ID="btnStampa" runat="server" Text="Stampa" Visible="True"  UseSubmitBehavior="False" />&nbsp;&nbsp;&nbsp;
                  &nbsp;<asp:Button ID="btnpulisci" runat="server" Text="Pulisci campi" Visible="True"  UseSubmitBehavior="true" OnClick="btnpulisci_Click" />

                  

              </td>
            </tr>

            <%-- START elenco ck accessori 08.08.2022 salvo --%>
            
            <tr style="vertical-align:top;height:auto;" >
              <td colspan="7" style="vertical-align:top;">            
              
              <hr />
                  <asp:Label ID="label15" runat="server" Text='Tipo Cliente' CssClass="testo" Font-Bold="true" />
                <div style="width:100%;">
                    
                      <div style="float:left;width:60%;">                          
                          <asp:CheckBoxList ID="checkTipoClienti" runat="server" DataSourceID="Sql_tipocliente" CssClass="testo" AppendDataBoundItems="true"
                        DataTextField="descrizione" DataValueField="id" RepeatColumns="4" Font-Size="8"
                        Width="100%" BorderColor="#444" BorderStyle="Solid" BorderWidth="1px">
                        
                        </asp:CheckBoxList>
                      </div>                      
                      <div style="float:left;vertical-align:bottom;">                      
                          <br />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnExportXLS" runat="server" Text="Esporta Excel" Visible="True"  UseSubmitBehavior="true" 
                          OnClick="btnExportXLS_Click" OnClientClick="return confirm('Procedo?');"/>

                          &nbsp;&nbsp;&nbsp;
                          <asp:DropDownList ID="ddl_profile" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_profile_SelectedIndexChanged">
                              <asp:ListItem Value="0">-profilo no sel -</asp:ListItem>
                              <asp:ListItem Value="1">-profilo TMP+KM</asp:ListItem>
                              <asp:ListItem Value="2">-profilo UNO</asp:ListItem>
                          </asp:DropDownList>


                          &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRiepilogoStazione" runat="server" Text="Riepilogo Stazione XLS" Visible="True"  UseSubmitBehavior="true" 
                          OnClick="btnRiepilogoStazione_Click" OnClientClick="return confirm('Procedo?');"/>

                      </div>


                 </div>

              </td>
            </tr>

            <tr style="vertical-align:top;height:auto;" runat="server" id="tr_ck_accessori" > <%--<span style="text-decoration:underline;color:cornflowerblue;">nascondi/visualizza</span>--%>
              <td colspan="7" style="vertical-align:top;">
                      <hr />            
                    <div  class="uk-accordion" data-uk-accordion="{collapse: true,showfirst: true}" style="background-color:#c8c8c6;margin-left:-15px;" >      
                      <label class="uk-accordion-title" style="background-color:#c8c8c6;border:none; font-size:12px;color:#000;font-weight:bold;">
                          Filtro Accessori: (<span style="text-decoration:underline;color:cornflowerblue;">nascondi/visualizza</span>)</label>                                           
                      <div id="div_ckaccessori" class="uk-accordion-content"  style="background-color:#c8c8c6;sc">
                          <asp:CheckBoxList ID="checkAccessori" runat="server" DataSourceID="sqlAccessori" CssClass="testo" AppendDataBoundItems="true" 
                        DataTextField="descrizione" DataValueField="id" RepeatColumns="4" Font-Size="8" Height="200" 
                        Width="100%" BorderColor="#444" BorderStyle="Solid" BorderWidth="1px">
                       <asp:ListItem Value="98" style="font-weight:bold;">Tempo + KM</asp:ListItem>
                        </asp:CheckBoxList>
                     </div>
                   </div>      
                   
            </td>
            </tr>


                                    
            <%-- END elenco ck accessori 08.08.2022 salvo --%>
            
            <tr style="height:44px;">
              <td colspan="7">
                  <hr />
                 <asp:Label ID="label12" runat="server" Text='Totale Commissioni:' CssClass="testo" Font-Bold="true" />&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="lblCommissioni" runat="server" Text='0' CssClass="testo" Font-Bold="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="label9" runat="server" Text='Totale Giorni:' CssClass="testo" Font-Bold="true" />&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="lblGiorni" runat="server" Text='0' CssClass="testo" Font-Bold="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              </td>
            </tr>
                      

            <%--ListView--%>
            <tr style="min-height:1000px;vertical-align:top;" id="trlv" runat="server" name="trlv">

              <td colspan="7">
                 <asp:ListView ID="listCommissioniOperatore" runat="server" DataKeyNames="id" DataSourceID="sqlCommissioniOperatore" Visible="true" >
                   <ItemTemplate>
                       <tr style="background-color:#DCDCDC;color: #000000;" runat="server" id="riga_contratto">
                           <td align="center">
                               <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                               <asp:Label ID="num_calcolo" runat="server" Text='<%# Eval("num_calcolo") %>' Visible="false" />
        
                               <asp:Label ID="num_contratto" runat="server" Text='<%# Eval("num_contratto") %>' CssClass="testo" />
                           </td>
                           <td align="center">
                               <asp:Label ID="giorni" runat="server" Text='<%# Eval("giorni") %>' CssClass="testo" />
                           </td>
                             <td align="center">
                             <asp:Label ID="data_uscita" runat="server" Text='<%# FormatDateTime(Eval("data_uscita"), vbShortDate) %>' CssClass="testo" />
                           </td>
                           <td align="center">
                               <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' DataFormatString="{0:d}" CssClass="testo" />
                           </td>
                           <td align="center">
                             <asp:Label ID="imponibile_totale" runat="server" Text='<%# Eval("imponibile_totale") %>' CssClass="testo" />
                           </td>
                           <td align="center">
                             <asp:Label ID="imponibile_parziale" runat="server" Text='' CssClass="testo" />
                           </td>
                           <td align="center">
                             <asp:Label ID="imponibile_netto" runat="server" Text='' CssClass="testo" />
                           </td>
                           <td align="center">
                             <asp:Label ID="imponibile_commissione" runat="server" Text='<%# Eval("imponibile_commissione") %>' CssClass="testo" />
                           </td>
                       </tr>
                   </ItemTemplate>
                   <AlternatingItemTemplate>
                       <tr  style="background-color:#DCDCDC;color: #000000;" runat="server" id="riga_contratto">
                          <td align="center">
                               <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                               <asp:Label ID="num_calcolo" runat="server" Text='<%# Eval("num_calcolo") %>' Visible="false" />

                               <asp:Label ID="num_contratto" runat="server" Text='<%# Eval("num_contratto") %>' CssClass="testo" />
                           </td>
                           <td align="center">
                               <asp:Label ID="giorni" runat="server" Text='<%# Eval("giorni") %>' CssClass="testo" />
                           </td>
                           <td align="center">
                             <asp:Label ID="data_uscita" runat="server" Text='<%# FormatDateTime(Eval("data_uscita"), vbShortDate) %>' CssClass="testo" />
                           </td>
                           <td align="center">
                             <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo" />
                           </td>
                           <td align="center">
                             <asp:Label ID="imponibile_totale" runat="server" Text='<%# Eval("imponibile_totale") %>' CssClass="testo" />
                           </td>
                           <td align="center">
                             <asp:Label ID="imponibile_parziale" runat="server" Text='' CssClass="testo" />
                           </td>
                           <td align="center">
                             <asp:Label ID="imponibile_netto" runat="server" Text='' CssClass="testo" />
                           </td>
                           <td align="center">
                             <asp:Label ID="imponibile_commissione" runat="server" Text='<%# Eval("imponibile_commissione") %>' CssClass="testo" />
                           </td>
                       </tr>
                   </AlternatingItemTemplate>
                   <EmptyDataTemplate>
                       <table id="Table1" runat="server" 
                           style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
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
                                   <table ID="itemPlaceholderContainer" runat="server" border="1" width="100%">
                                       <tr id="Tr2" runat="server" class="sfondo_rosso">
                                           <th id="Th1" runat="server" align="center">
                                            <asp:Label ID="Label1" runat="server" Text="Contratto" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th7" runat="server" align="center">
                                            <asp:Label ID="Label8" runat="server" Text="Giorni" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th8" runat="server" align="center">
                                             <asp:Label ID="Label10" runat="server" Text="Data Uscita" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th4" runat="server" align="center">
                                             <asp:Label ID="Label5" runat="server" Text="Data Rientro" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th2" runat="server" align="center">
                                             <asp:Label ID="Label2" runat="server" Text="Imponibile Contratto" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th5" runat="server" align="center">
                                             <asp:Label ID="Label6" runat="server" Text="Imponibile Parziale" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th6" runat="server" align="center">
                                             <asp:Label ID="Label7" runat="server" Text="Imponibile Netto" CssClass="testo_titolo"></asp:Label>
                                           </th>
                                           <th id="Th3" runat="server" align="center">
                                             <asp:Label ID="Label3" runat="server" Text="Commissione" CssClass="testo_titolo"></asp:Label>
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
        </table>


            <asp:SqlDataSource ID="sqlAccessori" runat="server" ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>"
                SelectCommand="SELECT [id], [descrizione] FROM [condizioni_elementi] where id<>'176' and id<>'177' and id<>'131'  and id<>'98' ORDER BY [descrizione]">
            </asp:SqlDataSource>

        <asp:SqlDataSource ID="sqlOperatori" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
            SelectCommand="SELECT id, (cognome + ' ' + nome) As descrizione FROM operatori WITH(NOLOCK) WHERE nome<>'xxx' ORDER BY cognome"></asp:SqlDataSource>

        <asp:SqlDataSource ID="sqlCommissioniOperatore" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
            SelectCommand="SELECT id, num_contratto, data_uscita, data_rientro,
            (SELECT SUM(imponibile_scontato) FROM contratti_costi WITH(NOLOCK) INNER JOIN condizioni_elementi ON contratti_costi.id_elemento=condizioni_elementi.id 
            INNER JOIN commissioni_operatore WITH(NOLOCK) ON contratti_costi.id_elemento=commissioni_operatore.id_condizioni_elementi
            WHERE contratti_costi.id_documento=contratti.id AND contratti_costi.num_calcolo=contratti.num_calcolo AND condizioni_elementi.commissione_operatore=1 AND selezionato=1 AND ISNULL(omaggiato,0)=0 AND commissioni_operatore.id_operatore=-1) As imponibile_commissione, 
            (SELECT contratti_costi_1.imponibile_scontato FROM contratti_costi As contratti_costi_1 WITH(NOLOCK) WHERE contratti_costi_1.id_documento=contratti.id AND contratti_costi_1.num_calcolo=contratti.num_calcolo AND contratti_costi_1.nome_costo='TOTALE') As imponibile_totale 
            FROM contratti WHERE (attivo=1) AND (status=4 OR status=6 OR status=8) AND id=0"></asp:SqlDataSource>


     <asp:SqlDataSource ID="Sql_tipocliente" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
            SelectCommand="SELECT id, descrizione FROM clienti_tipologia WITH(NOLOCK) ORDER BY descrizione"></asp:SqlDataSource>

<div style="width:auto;margin-top:20px;font-weight:bold;" id="divtotali" runat="server" >

    Totale Cass Periodo Selezionato <asp:label ID="txtTotPeriodo" runat="server" ></asp:label>
    <br />
    Totale Cassa Periodo Mese Precedente <asp:Label ID="txtTotMesePrev" runat="server" ></asp:Label>
    <br />
    Totale Cassa Periodo Mese/Anno Precedente <asp:label ID="txtTotMeseAnnoPrev" runat="server" ></asp:label>
    <br />

</div>





<div style="background-color:#c8c8c6;min-height:800px;width:auto;" id="div_footer" runat="server" >


</div>


  
    <script type="text/javascript">
  /*      window.onload = function () { document.getElementById("div_ckaccessori").style.display = "display"; } */
    </script>
    
          </ContentTemplate>

</asp:UpdatePanel>






</asp:Content>

