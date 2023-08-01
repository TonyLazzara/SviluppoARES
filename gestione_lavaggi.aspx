<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="gestione_lavaggi.aspx.vb" Inherits="gestione_lavaggi" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<%@ Register Src="gestione_danni/gestione_checkin.ascx" TagName="gestione_checkin" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
         <link rel="StyleSheet" type="text/css" href="css/style.css" />

    <script type="text/javascript">
        function nocalendar() {
            alert('Data disabilitata!')
            return false;
        }
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
 

    <asp:Label ID="permesso_accesso" runat="server" visible="false"></asp:Label>
    <asp:Label ID="permesso_admin" runat="server" visible="false"></asp:Label>
    <asp:Label ID="id_modifica" runat="server" visible="false"></asp:Label>
    <asp:Label ID="id_operatore" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="id_stazione_operatore" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="query_cerca" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblOrderBY" runat="server" Visible="False"></asp:Label>

    <div runat="server" id="div_lavaggi">
    <div runat="server" id="div_check" visible="false">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" >
             &nbsp;<asp:Label ID="Label71" runat="server" Text="Gestione Lavaggi - Check Veicolo" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
       </table>
       <table runat="server" id="table8" style="border:4px solid #444; border-bottom:0px; border-top:0px;" border="0" cellspacing="2" cellpadding="2" width="100%">
              <tr id="Tr8" runat="server">       
                <td id="Td39" valign="top" runat="server">    
                    <asp:Label ID="Label75" runat="server" Text="Targa" CssClass="testo_bold"></asp:Label>
                </td>
                <td id="Td40" valign="top" runat="server">
                    <asp:TextBox ID="txtTarga" runat="server" Width="84px"></asp:TextBox>
                </td>
                <td id="Td41" valign="top" runat="server" >
                      <asp:Button ID="btnScegliTarga" runat="server" Text="Seleziona" />
                 
                      <asp:Button ID="btnAnnulla" runat="server" Text="Annulla" Visible="False" />
                </td>
                <td id="Td42" align="left" valign="top" runat="server">
                  <asp:Label ID="Label76" runat="server" Text="Gruppo:" CssClass="testo_bold"></asp:Label>
                </td>
                <td id="Td43" valign="top" runat="server">           
                   <asp:TextBox ID="txtGruppo" runat="server" Width="40px" ReadOnly="True"></asp:TextBox>
                </td>
                <td id="Td44" align="left" valign="top" runat="server" class="style17">
                  <asp:Label ID="Label77" runat="server" Text="Modello:" CssClass="testo_bold"></asp:Label>
                </td>
                <td id="Td45" valign="top" runat="server">           
                   <asp:TextBox ID="txtModello" runat="server" Width="170px" ReadOnly="True"></asp:TextBox>
                </td>
                <td id="Td46" align="left" valign="top" runat="server">
                  <asp:Label ID="Label78" runat="server" Text="Km:" CssClass="testo_bold"></asp:Label>
                </td>
                <td id="Td47" valign="top" runat="server">           
                   <asp:TextBox ID="txtKm" runat="server" Width="50px" ReadOnly="True" 
                        onKeyPress="return filterInputInt(event)"></asp:TextBox>
                </td>
                <td id="Td48" valign="top" runat="server">
                  <asp:Label ID="lblSerbatoio" runat="server" Text="Serbatoio:" CssClass="testo_bold"></asp:Label>
               </td>
               <td id="Td49" valign="top" runat="server">           
                   <asp:TextBox ID="txtSerbatoio" runat="server" Width="50px" ReadOnly="True" 
                       onKeyPress="return filterInputInt(event)"></asp:TextBox>
                   <asp:Label ID="Label80" runat="server" Text="/" CssClass="testo_bold"></asp:Label>
                   <asp:Label ID="lblSerbatoioMax" runat="server" CssClass="testo_bold"></asp:Label>
               </td>
            </tr>
            <tr runat="server" visible="False" id="riga_rientro_interno">       
                <td id="Td50" valign="top" runat="server">    
                    &nbsp;</td>
                <td id="Td51" valign="top" runat="server">
                    &nbsp;</td>
                <td id="Td52" valign="top" runat="server">
                    <asp:Label ID="idLavaggio" runat="server" Visible="False"></asp:Label>
                </td>
                <td id="Td53" align="left" runat="server">
                    <asp:Label ID="lblRientroInterno0" runat="server" CssClass="testo_bold" 
                        ForeColor="Red" Text="RIENTRO:"></asp:Label>
                </td>
                <td id="Td54" runat="server" colspan="2" align="right">           
                    <asp:Label ID="lblDataPresuntoRientroInterno0" runat="server" 
                        CssClass="testo_bold" Text="Data Rientro:"></asp:Label>
                </td>
                <td id="Td56" align="left" runat="server">              
                    <a onclick="Calendar.show(document.getElementById('<%=txtDataRientro.ClientID%>'), '%d/%m/%Y', false)"> 
                    <asp:TextBox ID="txtDataRientro" runat="server" ReadOnly="True" Width="70px"></asp:TextBox></a>
                     <ajaxtoolkit:MaskedEditExtender ID="MaskedEditExtender10" 
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataRientro">
                     </ajaxtoolkit:MaskedEditExtender>
                        
                    <asp:TextBox ID="txtOraRientro" runat="server" ReadOnly="True" Width="40px"></asp:TextBox>
                          
                     <ajaxtoolkit:MaskedEditExtender ID="MaskedEditExtender11" runat="server" 
                        CultureName="en-US" Mask="99:99" MaskType="Time" 
                        TargetControlID="txtOraRientro" CultureAMPMPlaceholder="AM;PM" 
                        CultureCurrencySymbolPlaceholder="$" CultureDateFormat="MDY" 
                        CultureDatePlaceholder="/" CultureDecimalPlaceholder="." 
                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                      </ajaxtoolkit:MaskedEditExtender>
                </td>
                <td id="Td1" align="left" runat="server">
                  <asp:Label ID="Label196" runat="server" Text="Km:" CssClass="testo_bold"></asp:Label>
                </td>
                <td id="Td2" runat="server">           
                   <asp:TextBox ID="txtKmRientro" runat="server" Width="50px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
                </td>
                <td id="Td3" runat="server">
                  <asp:Label ID="Label198" runat="server" Text="Serbatoio:" CssClass="testo_bold"></asp:Label>
                </td>
               <td id="Td4" runat="server">           
                   <asp:TextBox ID="txtSerbatoioRientro" runat="server" Width="50px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
                   <asp:Label ID="Label199" runat="server" Text="/" CssClass="testo_bold"></asp:Label>
                   <asp:Label ID="lblSerbatoioMaxRientro" runat="server" CssClass="testo_bold"></asp:Label>
                </td>
            </tr>
           </table>
           <table runat="server" id="table81" style="border:4px solid #444;border-top:0px;" cellspacing="2" cellpadding="2" width="100%">
            <tr id="Tr1" runat="server">       
                <td id="Td5" runat="server">    
                    <asp:Label ID="lblPresuntoRientro2" runat="server" CssClass="testo_bold" 
                        Text="Staz. Uscita:"></asp:Label>
                </td>
                <td id="Td6" runat="server" class="style21">
                    <asp:DropDownList ID="dropStazionePickUp" runat="server" 
                        AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                        DataValueField="id">
                        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td id="Td7" runat="server" class="style22">
                    <asp:Label ID="lblDataUscita" runat="server" CssClass="testo_bold" 
                        Text="Data Uscita:"></asp:Label>
                </td>
                <td id="Td8" runat="server">
                    <%--<a onclick="Calendar.show(document.getElementById('<%=txtDataUscita.ClientID%>'), '%d/%m/%Y', false)"> --%>
                    <asp:TextBox ID="txtDataUscita" runat="server" Width="70px"></asp:TextBox>
                       <%-- </a>--%>
                  <%--  <ajaxtoolkit:CalendarExtender ID="txtDataUscita_CalendarExtender" 
                        runat="server" Enabled="True" Format="dd/MM/yyyy" 
                        TargetControlID="txtDataUscita">
                    </ajaxtoolkit:CalendarExtender>--%>
                    <ajaxtoolkit:MaskedEditExtender ID="txtDataUscita_MaskedEditExtender" 
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataUscita">
                    </ajaxtoolkit:MaskedEditExtender>

                    <asp:TextBox ID="txtOraUscita" runat="server" Width="40px"></asp:TextBox>
                    <ajaxtoolkit:MaskedEditExtender ID="MaskedEditExtender9" runat="server" 
                        CultureName="en-US" Mask="99:99" MaskType="Time" 
                        TargetControlID="txtOraUscita" CultureAMPMPlaceholder="AM;PM" 
                        CultureCurrencySymbolPlaceholder="$" CultureDateFormat="MDY" 
                        CultureDatePlaceholder="/" CultureDecimalPlaceholder="." 
                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                    </ajaxtoolkit:MaskedEditExtender>
                </td>
                <td id="Td9" runat="server">
                    <asp:Label ID="Label983" runat="server" CssClass="testo_bold" Text="Autolavaggio"></asp:Label>
                </td>
                <td id="Td10" runat="server">
                    <asp:DropDownList ID="dropAutolavaggio" runat="server" 
                        AppendDataBoundItems="True" DataSourceID="sqlAutolavaggio" DataTextField="descrizione" 
                        DataValueField="id">
                        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="Tr2" runat="server">       
                <td id="Td11" runat="server">    
                    <asp:Label ID="Label984" runat="server" CssClass="testo_bold" Text="Tipologia"></asp:Label>
                </td>
                <td id="Td12" runat="server" class="style21">
                    <asp:DropDownList ID="dropTipologia" runat="server" 
                        AppendDataBoundItems="True" DataSourceID="sqlTipologia" DataTextField="descrizione" 
                        DataValueField="id">
                        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td id="Td13" runat="server" class="style22">
                    <asp:Label ID="lblPresuntoRientroInterno0" runat="server" CssClass="testo_bold" 
                        Text="Presunto Rientro:"></asp:Label>
                </td>
                <td id="Td14" runat="server">
                    <a onclick="Calendar.show(document.getElementById('<%=txtPresuntoRientro.ClientID%>'), '%d/%m/%Y', false)"> 
                    <asp:TextBox ID="txtPresuntoRientro" runat="server" Width="70px"></asp:TextBox>
                        </a>
             <%--       <ajaxtoolkit:CalendarExtender ID="txtPresuntoRientro_CalendarExtender" 
                        runat="server" Enabled="True" Format="dd/MM/yyyy" 
                        TargetControlID="txtPresuntoRientro">
                    </ajaxtoolkit:CalendarExtender>--%>
                    <ajaxtoolkit:MaskedEditExtender ID="txtPresuntoRientro_MaskedEditExtender" 
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtPresuntoRientro">
                    </ajaxtoolkit:MaskedEditExtender>
                    <asp:TextBox ID="txtPresuntoRientroOra" runat="server" Width="40px"></asp:TextBox>
                    <ajaxtoolkit:MaskedEditExtender ID="txtPresuntoRientroOra_MaskedEditExtender" 
                        runat="server" CultureName="en-US" Mask="99:99" 
                        MaskType="Time" TargetControlID="txtPresuntoRientroOra" 
                        CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="$" 
                        CultureDateFormat="MDY" CultureDatePlaceholder="/" 
                        CultureDecimalPlaceholder="." CultureThousandsPlaceholder="," 
                        CultureTimePlaceholder=":" Enabled="True">
                     </ajaxtoolkit:MaskedEditExtender>
                </td>
                <td id="Td15" runat="server">
                    <asp:Label ID="Label982" runat="server" CssClass="testo_bold" Text="Conducente"></asp:Label>
                </td>
                <td id="Td16" runat="server">
                    <asp:DropDownList ID="dropDrivers" runat="server" 
                        AppendDataBoundItems="True" DataSourceID="sqlDrivers" DataTextField="driver" 
                        DataValueField="id">
                        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
             <tr id="Tr4" runat="server">
                   <td id="Td20" runat="server" align="center" colspan="6" valign="top">

                       
                       &nbsp;&nbsp;&nbsp;<asp:Button ID="btnCheckOut" runat="server" Text="Salva - Check Out" />
                       <asp:Button ID="btnCheckIn" runat="server" Text="Check In" Visible="False" />
                       <asp:Button ID="btnVediCheckInterno" runat="server" Text="Vedi Check" Visible="False" />
                       &nbsp;<asp:Button ID="btnChiudiCheck" runat="server" Text="Chiudi" />
                       <asp:Label ID="idVeicoloSelezionato" runat="server" Visible="False"></asp:Label>
                       <asp:Label ID="idGruppo" runat="server" Visible="False"></asp:Label>
                       <asp:Label ID="stato_lavaggio" runat="server" Visible="False"></asp:Label>
                   </td>
               </tr>
           </table> 
       </div>
       <div runat="server" id="div_cerca_interni">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" >
             &nbsp;<asp:Label ID="Label72" runat="server" Text="Gestione Lavaggi" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
        </table>
        <table cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444; border-top:0px;border-bottom:0px;" border="0">
         <tr>
           <td>
               <asp:Label ID="Label73" runat="server" CssClass="testo_bold" Text="Stazione Uscita"></asp:Label>
           </td>
           <td>
              <asp:Label ID="Label74" runat="server" Text="Stato" CssClass="testo_bold"></asp:Label>      
           </td>
           <td>
              <asp:Label ID="Label81" runat="server" Text="Data Uscita (Da)" CssClass="testo_bold"></asp:Label>
           </td>
           <td>
              <asp:Label ID="Label82" runat="server" Text="Data Uscita (A)" CssClass="testo_bold"></asp:Label>
           </td>
           <td>
           
           </td>
         </tr>
         <tr>
           <td>
                <asp:DropDownList ID="dropCercaStazionePickUp" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="sqlStazioni" DataTextField="stazione" DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Tutte</asp:ListItem>
                </asp:DropDownList>
           </td>
           <td>
                <asp:DropDownList ID="dropCercaStato" runat="server" 
                    AppendDataBoundItems="True">
                    <asp:ListItem Selected="True" Value="-1">Tutti</asp:ListItem>
                    <asp:ListItem Selected="False" Value="0">Aperto</asp:ListItem>
                    <asp:ListItem Selected="False" Value="1">Chiuso</asp:ListItem>
                </asp:DropDownList>
             </td>
           <td>
               <a onclick="Calendar.show(document.getElementById('<%=txtCercaDataUscitaDaInterno.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox runat="server" Width="70px" ID="txtCercaDataUscitaDaInterno"></asp:TextBox>
                   </a>
               <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                       TargetControlID="txtCercaDataUscitaDaInterno" 
                       ID="CalendarExtender4">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                       CultureDatePlaceholder="" CultureTimePlaceholder="" 
                       CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                       CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                       CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaDataUscitaDaInterno" 
                       ID="MaskedEditExtender4">
                </ajaxtoolkit:MaskedEditExtender>
             </td>
           <td>  
               <a onclick="Calendar.show(document.getElementById('<%=txtCercaDataUscitaAInterno.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox runat="server" Width="70px" ID="txtCercaDataUscitaAInterno"></asp:TextBox>
                   </a>
                <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                       TargetControlID="txtCercaDataUscitaAInterno" 
                       ID="CalendarExtender8">
                </ajaxtoolkit:CalendarExtender>--%>
                <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                       CultureDatePlaceholder="" CultureTimePlaceholder="" 
                       CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                       CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                       CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaDataUscitaAInterno" 
                       ID="MaskedEditExtender5">
                </ajaxtoolkit:MaskedEditExtender>
             </td>
           <td>
           
               &nbsp;</td>
         </tr>
         <tr>
           <td>
           
               <asp:Label ID="Label84" runat="server" CssClass="testo_bold" Text="Veicolo"></asp:Label>
           </td>
           <td>
           
                    <asp:Label ID="Label985" runat="server" CssClass="testo_bold" 
                   Text="Tipologia"></asp:Label>
                </td>
           <td>
           
                    <asp:Label ID="Label986" runat="server" CssClass="testo_bold" 
                   Text="Autolavaggio"></asp:Label>
                </td>
           <td>
              
               &nbsp;</td>
           <td>
           
               &nbsp;</td>
         </tr>
         <tr>
           <td>
           
               <asp:TextBox ID="txtCercaTargaInterno" runat="server" Width="90px"></asp:TextBox>
             </td>
           <td>
           
                    <asp:DropDownList ID="dropCercaTipologia" runat="server" 
                        AppendDataBoundItems="True" DataSourceID="sqlTipologia" DataTextField="descrizione" 
                        DataValueField="id">
                        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                </td>
           <td>
           
                    <asp:DropDownList ID="dropCercaAutolavaggio" runat="server" 
                        AppendDataBoundItems="True" DataSourceID="sqlAutolavaggioTutti" DataTextField="descrizione" 
                        DataValueField="id">
                        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                </td>
           <td>
              
               &nbsp;</td>
           <td>
           
               &nbsp;</td>
         </tr>
       </table>
        <table cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444; border-top:0px;" border="0">
          <tr>
            <td align="center">
            
                <asp:Button ID="btnCercaInterni" runat="server" Text="Cerca" />&nbsp;
                <asp:Button ID="btnNuovoLavaggio" runat="server" Text="Nuovo Lavaggio" />
            
            </td>
          </tr>
        </table>

        <!-- Elenco Lavaggi -->
        <table cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444; border-top:0px;" border="0">
         <tr>
           <td align="left">
               <asp:ListView ID="listLavaggi" runat="server" DataSourceID="sqlLavaggi" DataKeyNames="id">
                  <ItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000;">
                          <td align="left">
                              <asp:Label ID="num_lavaggio" runat="server"  Text='<%# Eval("num_lavaggio") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="stato" runat="server" Text='<%# Eval("stato") %>' />

                              <asp:Label ID="id_stato" runat="server" Text='<%# Eval("stato") %>' visible="false" />
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="id_conducente" runat="server" Text='<%# Eval("id_conducente") %>' Visible="false" />
                              <asp:Label ID="km_uscita" runat="server" Text='<%# Eval("km_uscita") %>' Visible="false" />
                              <asp:Label ID="km_rientro" runat="server" Text='<%# Eval("km_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_uscita" runat="server" Text='<%# Eval("litri_uscita") %>' Visible="false" />
                              <asp:Label ID="litri_rientro" runat="server" Text='<%# Eval("litri_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_max" runat="server" Text='<%# Eval("litri_max") %>' Visible="false" />
                              <asp:Label ID="lblModello" runat="server"  Visible="false" Text='<%# Eval("modello") %>' />
                              <asp:Label ID="gruppo" runat="server"  Visible="false" Text='<%# Eval("gruppo") %>' />
                              <asp:Label ID="id_lavaggi_autolavaggio" runat="server"  Visible="false" Text='<%# Eval("id_lavaggi_autolavaggio") %>' />
                              <asp:Label ID="id_lavaggi_tipologia" runat="server"  Visible="false" Text='<%# Eval("id_lavaggi_tipologia") %>' />
                          </td>
                         <td align="left">
                             <asp:Label ID="id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                             <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="cod_gruppo" runat="server" Text='<%# Eval("cod_gruppo") %>' CssClass="testo_piccolo"  />
                          </td>
                        <td align="left">
                             <asp:Label ID="tipologia" runat="server" Text='<%# Eval("tipologia") %>' CssClass="testo_piccolo"  />
                          </td>
                        <td align="left">
                             <asp:Label ID="autolavaggio" runat="server" Text='<%# Eval("autolavaggio") %>' CssClass="testo_piccolo"  />
                          </td>
                         <td align="left"> 
                              <asp:Label ID="id_stazione_uscita" runat="server" Visible="false" Text='<%# Eval("id_stazione_uscita") %>' />
                              <asp:Label ID="stazione_uscita" runat="server" CssClass="testo_piccolo" Text='<%# Eval("stazione_uscita") %>' />
                          </td>
                          <td align="left">
                            <asp:Label ID="data_uscita" runat="server" Text='<%# Eval("data_uscita")  %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo_piccolo" />
                              <asp:Label ID="data_presunto_rientro" runat="server" Text='<%# Eval("data_presunto_rientro") %>' Visible="false" />
                          </td>
                         
                          <td align="left">
                              <asp:Button ID="btnVedi" runat="server" CommandName="vedi" Text="Vedi" />
                          </td>
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr style="">
                         <td align="left">
                              <asp:Label ID="num_lavaggio" runat="server"  Text='<%# Eval("num_lavaggio") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="stato" runat="server" Text='<%# Eval("stato") %>'  />

                              <asp:Label ID="id_stato" runat="server" Text='<%# Eval("stato") %>' visible="false" />
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="id_conducente" runat="server" Text='<%# Eval("id_conducente") %>' Visible="false" />
                              <asp:Label ID="km_uscita" runat="server" Text='<%# Eval("km_uscita") %>' Visible="false" />
                              <asp:Label ID="km_rientro" runat="server" Text='<%# Eval("km_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_uscita" runat="server" Text='<%# Eval("litri_uscita") %>' Visible="false" />
                              <asp:Label ID="litri_rientro" runat="server" Text='<%# Eval("litri_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_max" runat="server" Text='<%# Eval("litri_max") %>' Visible="false" />
                              <asp:Label ID="lblModello" runat="server"  Visible="false" Text='<%# Eval("modello") %>' />
                              <asp:Label ID="gruppo" runat="server"  Visible="false" Text='<%# Eval("gruppo") %>' />
                              <asp:Label ID="id_lavaggi_autolavaggio" runat="server"  Visible="false" Text='<%# Eval("id_lavaggi_autolavaggio") %>' />
                              <asp:Label ID="id_lavaggi_tipologia" runat="server"  Visible="false" Text='<%# Eval("id_lavaggi_tipologia") %>' />
                          </td>
                         <td align="left">
                             <asp:Label ID="id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                             <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="cod_gruppo" runat="server" Text='<%# Eval("cod_gruppo") %>' CssClass="testo_piccolo"  />
                          </td>
                         <td align="left">
                             <asp:Label ID="tipologia" runat="server" Text='<%# Eval("tipologia") %>' CssClass="testo_piccolo"  />
                          </td>
                        <td align="left">
                             <asp:Label ID="autolavaggio" runat="server" Text='<%# Eval("autolavaggio") %>' CssClass="testo_piccolo"  />
                          </td>
                         <td align="left"> 
                              <asp:Label ID="id_stazione_uscita" runat="server" Visible="false" Text='<%# Eval("id_stazione_uscita") %>' />
                              <asp:Label ID="stazione_uscita" runat="server" CssClass="testo_piccolo" Text='<%# Eval("stazione_uscita") %>' />
                          </td>
                          <td align="left">
                            <asp:Label ID="data_uscita" runat="server" Text='<%# Eval("data_uscita")  %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo_piccolo" />
                              <asp:Label ID="data_presunto_rientro" runat="server" Text='<%# Eval("data_presunto_rientro") %>' Visible="false" />
                          </td>                         
                          <td align="left">
                              <asp:Button ID="btnVedi" runat="server" CommandName="vedi" Text="Vedi" />
                          </td>
                      </tr>
                  </AlternatingItemTemplate>
                  <EmptyDataTemplate>
                      <table id="Table1" runat="server" style="">
                          <tr>
                              <td>
                               &nbsp;
                              </td>
                          </tr>
                      </table>
                  </EmptyDataTemplate>
                  <LayoutTemplate>
                      <table id="Table2" runat="server" width="100%">
                          <tr id="Tr1" runat="server">
                              <td id="Td1" runat="server">
                                  <table ID="itemPlaceholderContainer" runat="server" border="1"  width="100%"   
                                      style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;
                                        border-style:none;border-width:1px;" class="testo_piccolo">
                                      <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                          <th align="left">                                          
                                              <asp:LinkButton ID="LinkButton17" runat="server" CommandName="order_by_numero" 
                                                  CssClass="testo_titolo_piccolo">Num.</asp:LinkButton>                                          
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton1" runat="server" CommandName="order_by_status" CssClass="testo_titolo_piccolo">Stato</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton11" runat="server" CommandName="order_by_targa" CssClass="testo_titolo_piccolo">Targa</asp:LinkButton>
                                          </th>
                                           <th align="left">
                                             <asp:LinkButton ID="LinkButton4" runat="server" CommandName="order_by_targa" CssClass="testo_titolo_piccolo">Gruppo</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton2" runat="server" CommandName="order_by_status" CssClass="testo_titolo_piccolo">Tipologia</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton3" runat="server" CommandName="order_by_status" CssClass="testo_titolo_piccolo">Autolavaggio</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                              <asp:LinkButton ID="LinkButton8" runat="server" CommandName="order_by_stazione_pick_up" CssClass="testo_titolo_piccolo">Staz.Uscita</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton9" runat="server" CommandName="order_by_data_uscita" CssClass="testo_titolo_piccolo">Uscita</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton15" runat="server" CommandName="order_by_data_rientro" CssClass="testo_titolo_piccolo">Rientro</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                              Pulsante
                                          </th>
                                      </tr>
                                      <tr ID="itemPlaceholder" runat="server">
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                          <tr id="Tr3" runat="server">
                              <td id="Td2" runat="server" style="" align="left">
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
           </td>
         </tr>
       </table>
      </div>
       </div>    
      <div id="div_edit_danno" runat="server" visible="false">
       <uc1:gestione_checkin id="gestione_checkin" runat="server" />
     </div>  

       <asp:SqlDataSource ID="sqlStazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT stazioni.id, (codice + ' ' + nome_stazione) As stazione FROM stazioni WITH(NOLOCK) WHERE attiva='1' ORDER BY codice">
       </asp:SqlDataSource>


       <asp:SqlDataSource ID="sqlGruppiAuto" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT gruppi.id_gruppo, gruppi.cod_gruppo FROM gruppi WITH(NOLOCK) WHERE attivo='1' ORDER BY cod_gruppo">
       </asp:SqlDataSource>

       <asp:SqlDataSource ID="sqlDrivers" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT drivers.id, (drivers.cognome + ' ' + drivers.nome) As driver FROM drivers WITH(NOLOCK)ORDER BY driver">
       </asp:SqlDataSource>

       <asp:SqlDataSource ID="sqlTipologia" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM lavaggi_tipologia WITH(NOLOCK) ORDER BY descrizione">
       </asp:SqlDataSource>

       <asp:SqlDataSource ID="sqlAutolavaggio" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM lavaggi_autolavaggio WITH(NOLOCK) WHERE id_stazione=0">
       </asp:SqlDataSource>

       <asp:SqlDataSource ID="sqlAutolavaggioTutti" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM lavaggi_autolavaggio WITH(NOLOCK) where [id] <> 2 and [id] <> 3  and [id] <> 5  and [id] <> 6 ORDER BY descrizione">
       </asp:SqlDataSource>

       <asp:SqlDataSource ID="sqlLavaggi" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT lavaggi.id, lavaggi.num_lavaggio, lavaggi.data_uscita, lavaggi.data_rientro, lavaggi.stato As id_status, lavaggi.data_presunto_rientro, lavaggi.gruppo,
        (stazioni1.codice + ' ' + stazioni1.nome_stazione) As stazione_uscita, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As stazione_rientro, lavaggi.id_veicolo, 
        lavaggi.targa, modelli.descrizione As modello, lavaggi.id_conducente, lavaggi.km_uscita, lavaggi.km_rientro, lavaggi.litri_uscita, lavaggi.litri_rientro,
        lavaggi.litri_max FROM lavaggi WITH(NOLOCK) 
        LEFT JOIN stazioni As stazioni1 WITH(NOLOCK) ON lavaggi.id_stazione_uscita=stazioni1.id 
        LEFT JOIN stazioni As stazioni2 WITH(NOLOCK) ON lavaggi.id_stazione_rientro=stazioni2.id 
        LEFT JOIN veicoli WITH(NOLOCK) ON lavaggi.id_veicolo=veicoli.id 
        LEFT JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello WHERE lavaggi.id>0 ORDER BY data_uscita DESC">      
       </asp:SqlDataSource>
</asp:Content>


