<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="commissioni_stazione.aspx.vb" Inherits="commissioni_stazione" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


     <link rel="StyleSheet" type="text/css" href="../css/style.css" /> 

    <title>ARES - Sicily Rent Car</title>
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

       <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
            <td align="left" style="color: #FFFFFF;background-color:#444;" class="style1">
                <asp:Label ID="Label14" runat="server" Text="Calcolo Commissioni Stazione" CssClass="testo_titolo"></asp:Label>
            </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" >
            <tr>
                <td>
                   <asp:Label ID="Label37" runat="server" Text="Stazione" CssClass="testo_bold"></asp:Label>
                </td>
                <td align="center">
                   <asp:Label ID="Label2" runat="server" Text="Percentuale Commissione" CssClass="testo_bold"></asp:Label>
                </td>
                <td align="center">
                   <asp:Label ID="Label1" runat="server" Text="Percentuale su Imponibile" CssClass="testo_bold"></asp:Label>
                </td>
                <td align="center">
                   <asp:Label ID="Label3" runat="server" Text="Da Data Drop Off" CssClass="testo_bold"></asp:Label>
                </td>
                <td align="center">
                   <asp:Label ID="Label4" runat="server" Text="A Data Drop Off" CssClass="testo_bold"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                  <asp:DropDownList ID="dropCercaStazionePickUp" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                    DataValueField="id">
                   <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                 </asp:DropDownList>
                </td>
                <td align="center">
                   <asp:TextBox ID="txtPercentualeCommissione" runat="server" Width="30px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
                </td>
                <td align="center">
                   <asp:TextBox ID="txtPercentualeImponibile" runat="server" Width="30px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
                </td>
                <td align="center">
                    <a onclick="Calendar.show(document.getElementById('<%=txtCercaDropOffDa.ClientID%>'), '%d/%m/%Y', false)">
                    <asp:TextBox runat="server" Width="70px" ID="txtCercaDropOffDa"></asp:TextBox></a>
                    
                    <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
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
                <td align="center">
                    <a onclick="Calendar.show(document.getElementById('<%=txtCercaDropOffA.ClientID%>'), '%d/%m/%Y', false)">
                    <asp:TextBox runat="server" Width="70px" ID="txtCercaDropOffA"></asp:TextBox></a>
                   <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
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
              <td align="center" colspan="5">
                 <br />&nbsp;
                 <asp:Button ID="btnCerca" runat="server" Text="Cerca" Visible="True"  UseSubmitBehavior="False" /> &nbsp;
                 <asp:Button ID="btnStampa" runat="server" Text="Stampa Risultato Ricerca" Visible="True"  UseSubmitBehavior="False" />

                 

              </td>
            </tr>
            
            
            <tr>
              <td align="left" colspan="5">

                  <asp:DropDownList ID="ddl_mese" runat="server">
                      <asp:ListItem Value="1">Gennaio</asp:ListItem>
                      <asp:ListItem Value="2">Febbraio</asp:ListItem>
                      <asp:ListItem Value="3">Marzo</asp:ListItem>
                      <asp:ListItem Value="4">Aprile</asp:ListItem>
                      <asp:ListItem Value="5">Maggio</asp:ListItem>
                      <asp:ListItem Value="6">Giugno</asp:ListItem>
                      <asp:ListItem Value="7">Luglio</asp:ListItem>
                      <asp:ListItem Value="8">Agosto</asp:ListItem>
                      <asp:ListItem Value="9">Settembre</asp:ListItem>
                      <asp:ListItem Value="10">Ottobre</asp:ListItem>
                      <asp:ListItem Value="11">Novembre</asp:ListItem>
                      <asp:ListItem Value="12">Dicembre</asp:ListItem>
                  </asp:DropDownList>
                  <asp:DropDownList ID="ddl_anno" runat="server">

                  </asp:DropDownList>

                  
                  <asp:Button ID="btnStampaRoyalty" runat="server" Text="Stampa Royalties" Visible="True"  UseSubmitBehavior="False" OnClientClick="confirm('Procedo?');"/>

                 

              </td>

            </tr>
            
             <tr>
              <td align="left" colspan="5">
                  <br />                

              </td>

            </tr>
            
            <tr>
              <td colspan="5">
                 <asp:Label ID="label12" runat="server" Text='Totale Commissioni:' CssClass="testo" Font-Bold="true" />&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="lblCommissioni" runat="server" Text='0' CssClass="testo" Font-Bold="true" />
              </td>
            </tr>




            <tr>
              <td colspan="5">
                 <asp:ListView ID="listCommissioniStazione" runat="server" DataKeyNames="id" DataSourceID="sqlCommissioniStazione" Visible="true" >
                   <ItemTemplate>
                       <tr style="background-color:#DCDCDC;color: #000000;">
                           <td align="center">
                               <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                               <asp:Label ID="nazione" runat="server" Text='<%# Eval("nazione") %>' Visible="false" />
                               <asp:Label ID="num_calcolo" runat="server" Text='<%# Eval("num_calcolo") %>' Visible="false" />

                               <asp:Label ID="num_contratto" runat="server" Text='<%# Eval("num_contratto") %>' CssClass="testo" />
                                
                           </td>
                           <td align="center">
                               <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo" />
                           </td>
                           <td align="center">
                             <asp:Label ID="imponibile_totale" runat="server" Text='<%# Eval("imponibile_totale") %>' CssClass="testo" />
                             <asp:Label ID="imponibile_totale_originale" runat="server" visible="false" />
                           </td>
                           <td align="center">
                             <asp:Label ID="imponibile_parziale" runat="server" Text='' CssClass="testo" />
                             <asp:Label ID="imponibile_parziale_originale" runat="server" visible="false" />
                           </td>
                           <td align="center">
                             <asp:Label ID="imponibile_netto" runat="server" Text='' CssClass="testo" />
                             <asp:Label ID="imponibile_netto_originale" runat="server" visible="false" />
                           </td>
                           <td align="center">
                             <asp:Label ID="imponibile_commissione" runat="server" Text='<%# Eval("imponibile_commissione") %>' CssClass="testo" />
                             <asp:Label ID="imponibile_commissione_originale" runat="server" visible="false" />
                           </td>
                       </tr>
                   </ItemTemplate>
                   <AlternatingItemTemplate>
                       <tr>
                          <td align="center">
                               <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                               <asp:Label ID="nazione" runat="server" Text='<%# Eval("nazione") %>' Visible="false" />
                               <asp:Label ID="num_calcolo" runat="server" Text='<%# Eval("num_calcolo") %>' Visible="false" />
        
                               <asp:Label ID="num_contratto" runat="server" Text='<%# Eval("num_contratto") %>' CssClass="testo" />
                           </td>
                           <td align="center">
                             <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo" />
                           </td>
                           <td align="center">
                             <asp:Label ID="imponibile_totale" runat="server" Text='<%# Eval("imponibile_totale") %>' CssClass="testo" />
                             <asp:Label ID="imponibile_totale_originale" runat="server" visible="false" />
                           </td>
                           <td align="center">
                             <asp:Label ID="imponibile_parziale" runat="server" Text='' CssClass="testo" />
                             <asp:Label ID="imponibile_parziale_originale" runat="server" visible="false" />
                           </td>
                           <td align="center">
                             <asp:Label ID="imponibile_netto" runat="server" Text='' CssClass="testo" />
                             <asp:Label ID="imponibile_netto_originale" runat="server" visible="false" />
                           </td>
                           <td align="center">
                             <asp:Label ID="imponibile_commissione" runat="server" Text='<%# Eval("imponibile_commissione") %>' CssClass="testo" />
                             <asp:Label ID="imponibile_commissione_originale" runat="server" visible="false" />
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

        <asp:SqlDataSource ID="sqlStazioni" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
            SelectCommand="SELECT stazioni.id, (codice + ' ' + nome_stazione) As stazione FROM stazioni WITH(NOLOCK) WHERE attiva='1' ORDER BY codice"></asp:SqlDataSource>

        <asp:SqlDataSource ID="sqlCommissioniStazione" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
            SelectCommand="SELECT id, num_contratto, data_rientro, 
            (SELECT SUM(imponibile_scontato) FROM contratti_costi WITH(NOLOCK) INNER JOIN condizioni_elementi ON contratti_costi.id_elemento=condizioni_elementi.id 
            WHERE contratti_costi.id_documento=contratti.id AND contratti_costi.num_calcolo=contratti.num_calcolo AND condizioni_elementi.commissione_stazione=1 AND selezionato=1 AND ISNULL(omaggiato,0)=0) As imponibile_commissione, 
            (SELECT contratti_costi_1.imponibile_scontato FROM contratti_costi As contratti_costi_1 WITH(NOLOCK) WHERE contratti_costi_1.id_documento=contratti.id AND contratti_costi_1.num_calcolo=contratti.num_calcolo AND contratti_costi_1.nome_costo='TOTALE') As imponibile_totale 
            FROM contratti WHERE (attivo=1) AND (status=4 OR status=6 OR status=8) AND id=0"></asp:SqlDataSource>

</asp:Content>

