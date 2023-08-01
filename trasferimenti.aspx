<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPageNoMenuX.master" AutoEventWireup="false" CodeFile="trasferimenti.aspx.vb" Inherits="trasferimenti" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<%@ Register Src="gestione_danni/gestione_checkin.ascx" TagName="gestione_checkin" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <style type="text/css">
        .style17
        {
            width: 76px;
        }
        .style21
        {
        }
        .style22
        {
            width: 126px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="permesso_accesso" runat="server" visible="false"></asp:Label>
    <asp:Label ID="permesso_admin" runat="server" visible="false"></asp:Label>
    <asp:Label ID="id_modifica" runat="server" visible="false"></asp:Label>
    <asp:Label ID="id_stato_attuale" runat="server" visible="false"></asp:Label>
    <asp:Label ID="id_operatore" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="id_stazione_operatore" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="query_cerca" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblOrderBY" runat="server" Visible="False"></asp:Label>
    
    <asp:Label ID="query_cerca2" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="query_cerca3" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="query_cerca4" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblOrderBy2" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblOrderBy3" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblOrderBy4" runat="server" Visible="False"></asp:Label>
    
<ajaxtoolkit:TabContainer ID="tabPanel1" runat="server" ActiveTabIndex="0" 
        Width="100%">
   <ajaxtoolkit:TabPanel ID="tab_trasferimenti_da_effettuare" runat="server" HeaderText="Trasf. Da Effettuare">
     <HeaderTemplate>
            Trasferimenti Da Effettuare
     </HeaderTemplate>
     <ContentTemplate>
       <div runat="server" id="div_check_veicolo" visible="False">
       <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" >
             &nbsp;<asp:Label ID="Label35" runat="server" Text="Trasferimento Veicoli - Check Veicolo" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
       </table>
       <table runat="server" id="table4" style="border:4px solid #444; border-bottom:0px; border-top:0px;" border="0" cellspacing="2" cellpadding="2" width="100%">
              <tr runat="server">       
                <td valign="top" colspan="3" runat="server">    
                
                    <asp:Label ID="Label99" runat="server" Text="Gr.Da Trasf.:" CssClass="testo_bold"></asp:Label>
                    &nbsp;
                    <asp:Label ID="lblGruppoDaTrasferire" runat="server" CssClass="testo_bold"></asp:Label>
                </td>
                <td align="left" valign="top" runat="server">
                    &nbsp;</td>
                <td valign="top" runat="server">           
                    &nbsp;</td>
                <td align="left" valign="top" runat="server">
                    &nbsp;</td>
                <td valign="top" runat="server">           
                    <asp:Label ID="idGruppoDaTrasferire" runat="server" Visible="False"></asp:Label>
                  </td>
                <td align="left" valign="top" runat="server">
                    &nbsp;</td>
                <td valign="top" runat="server">           
                    &nbsp;</td>
                <td valign="top" runat="server">
                    &nbsp;</td>
               <td valign="top" runat="server">           
                   <asp:Label ID="idStazioneRichiesta" runat="server" Visible="False"></asp:Label>
                  </td>
            </tr>
              <tr runat="server">       
                <td valign="top" runat="server">    
                    <asp:Label ID="Label31" runat="server" Text="Targa" CssClass="testo_bold"></asp:Label>
                </td>
                <td valign="top" runat="server">
                    <asp:TextBox ID="txtTarga" runat="server" Width="84px"></asp:TextBox>
                </td>
                <td valign="top" runat="server" >
                      <asp:Button ID="btnScegliTarga" runat="server" Text="Seleziona" />
                 
                      <asp:Button ID="btnAnnullaModificaTargaNoloInCorso" runat="server" 
                          Text="Annulla" Visible="False" />
                </td>
                <td align="left" valign="top" runat="server">
                  <asp:Label ID="Label32" runat="server" Text="Gruppo:" CssClass="testo_bold"></asp:Label>
                </td>
                <td valign="top" runat="server">           
                   <asp:TextBox ID="txtGruppo" runat="server" Width="40px" ReadOnly="True"></asp:TextBox>
                </td>
                <td align="left" valign="top" runat="server">
                  <asp:Label ID="Label33" runat="server" Text="Modello:" CssClass="testo_bold"></asp:Label>
                </td>
                <td valign="top" runat="server">           
                   <asp:TextBox ID="txtModello" runat="server" Width="170px" ReadOnly="True"></asp:TextBox>
                </td>
                <td align="left" valign="top" runat="server">
                  <asp:Label ID="Label34" runat="server" Text="Km:" CssClass="testo_bold"></asp:Label>
                </td>
                <td valign="top" runat="server">           
                   <asp:TextBox ID="txtKm" runat="server" Width="50px" ReadOnly="True" 
                        onKeyPress="return filterInputInt(event)"></asp:TextBox>
                </td>
                <td valign="top" runat="server">
                  <asp:Label ID="Label11" runat="server" Text="Serbatoio:" CssClass="testo_bold"></asp:Label>
               </td>
               <td valign="top" runat="server">           
                   <asp:TextBox ID="txtSerbatoio" runat="server" Width="50px" ReadOnly="True" 
                       onKeyPress="return filterInputInt(event)"></asp:TextBox>
                   <asp:Label ID="Label12" runat="server" Text="/" CssClass="testo_bold"></asp:Label>
                   <asp:Label ID="lblSerbatoioMax" runat="server" CssClass="testo_bold"></asp:Label>
               </td>
            </tr>
            <tr runat="server" visible="False" id="riga_rientro_veicolo">       
                <td valign="top" runat="server">    
                    &nbsp;</td>
                <td valign="top" runat="server">
                    &nbsp;</td>
                <td valign="top" runat="server">
              <asp:Label ID="idVeicoloSelezionato" runat="server" Visible="False"></asp:Label>    
                </td>
                <td align="left" runat="server">
                    &nbsp;</td>
                <td runat="server">           
                    &nbsp;</td>
                <td align="left" runat="server">
                    &nbsp;</td>
                <td align="right" runat="server">           
                      
                   <asp:Label ID="lblRientro" runat="server" Text="RIENTRO:"  CssClass="testo_bold" 
                        ForeColor="Red"></asp:Label>
            
                </td>
                <td align="left" runat="server">
                  <asp:Label ID="Label96" runat="server" Text="Km:" CssClass="testo_bold"></asp:Label>
                </td>
                <td runat="server">           
                   <asp:TextBox ID="txtKmRientro" runat="server" Width="50px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
                </td>
                <td runat="server">
                  <asp:Label ID="Label97" runat="server" Text="Serbatoio:" CssClass="testo_bold"></asp:Label>
                </td>
               <td runat="server">           
                   <asp:TextBox ID="txtSerbatoioRientro" runat="server" Width="50px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
                   <asp:Label ID="Label98" runat="server" Text="/" CssClass="testo_bold"></asp:Label>
                   <asp:Label ID="lblSerbatoioMaxRientro" runat="server" CssClass="testo_bold"></asp:Label>
                </td>
            </tr>
           </table>
           <table runat="server" id="table5" style="border:4px solid #444;border-top:0px;" cellspacing="2" cellpadding="2" width="100%">
            <tr runat="server">       
                <td runat="server">    
                    <asp:Label ID="Label102" runat="server" CssClass="testo_bold" Text="Conducente"></asp:Label>
                </td>
                <td runat="server">
                    <asp:DropDownList ID="dropDrivers" runat="server"
                        AppendDataBoundItems="True" DataSourceID="sqlDrivers" DataTextField="driver" 
                        DataValueField="id">
                        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td runat="server">
                    <asp:Label ID="lblPresuntoRientro" runat="server" CssClass="testo_bold" Text="Presunto Rientro:"></asp:Label>
                </td>
                <td runat="server">
                     <a onclick="Calendar.show(document.getElementById('<%=txtPresuntoRientro.ClientID%>'), '%d/%m/%Y', false)">
                        <asp:TextBox ID="txtPresuntoRientro" runat="server" Width="70px"></asp:TextBox>
                    </a>
                     <%--<ajaxtoolkit:CalendarExtender ID="CalendarExtender7" runat="server" 
                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtPresuntoRientro">
                    </ajaxtoolkit:CalendarExtender>--%>
                    <ajaxtoolkit:MaskedEditExtender ID="MaskedEditExtender8" runat="server" 
                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtPresuntoRientro">
                    </ajaxtoolkit:MaskedEditExtender>
                    <a onclick="Calendar.show(document.getElementById('<%=txtPresuntoRientroOra.ClientID%>'), '%d/%m/%Y', false)">
                    &nbsp;<asp:TextBox ID="txtPresuntoRientroOra" runat="server" MaxLength="2" Width="20px" onKeyPress="return filterInputInt(event)" ></asp:TextBox>
                </a>
                </td>
                <td runat="server">           
                    <asp:Label ID="lblPresuntoRientro0" runat="server" CssClass="testo_bold" Text="Data Rientro:"></asp:Label>
                </td>
                <td runat="server">
                    <a onclick="Calendar.show(document.getElementById('<%=txtDataRientro.ClientID%>'), '%d/%m/%Y', false)">
                        <asp:TextBox ID="txtDataRientro" runat="server" Width="70px" ReadOnly="True" ></asp:TextBox>
                    </a>
                    &nbsp;<asp:TextBox ID="txtOraRientro" runat="server" Width="40px" 
                        ReadOnly="True"></asp:TextBox>
                </td>
                <td runat="server">           
                      
                </td>
                <td runat="server">
                 
                </td>
                <td runat="server">           
                    &nbsp;</td>
                <td runat="server">
                    &nbsp;</td>
               <td runat="server">           
                   &nbsp;</td>
            </tr>
            <tr runat="server">       
                <td valign="top" colspan="11" align="center" runat="server">    
               <asp:Button ID="btnCheckOut" runat="server" Text="Salva - Check Out" />
               &nbsp;<asp:Button ID="btnStampaFoglioTrasferimento" runat="server" Text="Foglio di Trasferimento" />
                    &nbsp;<asp:Button ID="btnChiudiCheck" runat="server" Text="Chiudi" />
                </td>
            </tr>
           </table> 
       </table>
      </div>
      <div runat="server" id="div_trasferimenti_stazione">
      <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" >
             &nbsp;<asp:Label ID="Label36" runat="server" Text="Trasferimento Veicoli - Trasferimenti da effettuare" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
       </table>
      <table cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444; border-top:0px;" border="0">
         <tr>
           <td class="style6">
               <asp:Label ID="Label101" runat="server" CssClass="testo_bold" 
                   Text="Stazione Pick Up"></asp:Label>
           </td>
           <td class="style7">
           
              <asp:Label ID="Label37" runat="server" Text="Stato" CssClass="testo_bold"></asp:Label>
           
           </td>
           <td class="style11">
              
              <asp:Label ID="Label38" runat="server" Text="Gruppo" CssClass="testo_bold"></asp:Label>
              
           </td>
           <td class="style3">
           
              <asp:Label ID="Label39" runat="server" Text="Rich. per data (Da)" 
                   CssClass="testo_bold"></asp:Label>
              
           </td>
           <td class="style10">
              
              <asp:Label ID="Label40" runat="server" Text="Rich. per data (A)" 
                   CssClass="testo_bold"></asp:Label>
              
           </td>
           <td>
           
           </td>
         </tr>
         <tr>
           <td class="style6">
                <asp:DropDownList ID="dropCercaStazionePickUpCheck" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="sqlStazioni" DataTextField="stazione" DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Tutte</asp:ListItem>
                </asp:DropDownList>
           </td>
           <td class="style7">
           
                <asp:DropDownList ID="dropCercaStatoCheck" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlTrasferimentiStatusDaStazione" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Tutti</asp:ListItem>
                </asp:DropDownList>
             </td>
           <td class="style11">
              
                <asp:DropDownList ID="dropCercaGruppoCheck" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlGruppiAuto" 
                    DataTextField="cod_gruppo" DataValueField="id_gruppo">
                    <asp:ListItem Selected="True" Value="0">Tutti</asp:ListItem>
                </asp:DropDownList>
             </td>
           <td class="style3">
           <a onclick="Calendar.show(document.getElementById('<%=txtRichiestaPerDataCheckDa.ClientID%>'), '%d/%m/%Y', false)">
                <asp:TextBox runat="server" Width="70px" ID="txtRichiestaPerDataCheckDa"></asp:TextBox>
               </a>
            <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                   TargetControlID="txtRichiestaPerDataCheckDa" 
                   ID="CalendarExtender1">
            </ajaxtoolkit:CalendarExtender>--%>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                   CultureDatePlaceholder="" CultureTimePlaceholder="" 
                   CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                   CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                   CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtRichiestaPerDataCheckDa" 
                   ID="MaskedEditExtender2">
            </ajaxtoolkit:MaskedEditExtender>
             </td>
           <td class="style10">
              <a onclick="Calendar.show(document.getElementById('<%=txtRichiestaPerDataCheckA.ClientID%>'), '%d/%m/%Y', false)">
                    <asp:TextBox runat="server" Width="70px" ID="txtRichiestaPerDataCheckA"></asp:TextBox>
                  </a>
        <%--    <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                   TargetControlID="txtRichiestaPerDataCheckA" 
                   ID="CalendarExtender3">
            </ajaxtoolkit:CalendarExtender>--%>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                   CultureDatePlaceholder="" CultureTimePlaceholder="" 
                   CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                   CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                   CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtRichiestaPerDataCheckA" 
                   ID="MaskedEditExtender3">
            </ajaxtoolkit:MaskedEditExtender>
             </td>
           <td>
           
               &nbsp;</td>
         </tr>
         <tr>
           <td class="style6">
               <asp:Label ID="Label100" runat="server" CssClass="testo_bold" 
                   Text="Stazione Richiesta"></asp:Label>
           </td>
           <td class="style7">
           
               <asp:Label ID="Label103" runat="server" CssClass="testo_bold" Text="Targa"></asp:Label>
             </td>
           <td class="style11">
              
               &nbsp;</td>
           <td class="style3">
           
               &nbsp;</td>
           <td class="style10">
              
               &nbsp;</td>
           <td>
           
               &nbsp;</td>
         </tr>
         <tr>
           <td class="style6">
                <asp:DropDownList ID="dropCercaStazioneRichiestaCheck" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="sqlStazioni" DataTextField="stazione" DataValueField="id" 
                    Height="16px">
                    <asp:ListItem Selected="True" Value="0">Tutte</asp:ListItem>
                </asp:DropDownList>
             </td>
           <td class="style7">
           
               <asp:TextBox ID="txtCercaTarga" runat="server" Width="90px"></asp:TextBox>
             </td>
           <td class="style11">
              
               &nbsp;</td>
           <td class="style3">
           
               &nbsp;</td>
           <td class="style10">
              
               &nbsp;</td>
           <td>
           
               &nbsp;</td>
         </tr>
         <tr>
           <td align="center" colspan="6">
               <asp:Button ID="btnCercaCheck" runat="server" Text="Cerca" />
           </td>
         </tr>
       </table>
       <table cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444; border-top:0px;" border="0">
         <tr>
           <td align="left">
               <asp:ListView ID="listTrasferimentiDaEffettuare" runat="server" DataSourceID="sqlTrasferimentiDaEffettuare" DataKeyNames="id">
                  <ItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000;">
                         <td align="left">
                               <asp:Label ID="num_trasferimento" runat="server" CssClass="testo_piccolo" Text='<%# Eval("num_trasferimento") %>' />
                         </td>
                         <td align="left">
                               <asp:Label ID="stazione_pick_up" runat="server" CssClass="testo_piccolo" Text='<%# Eval("stazione_pick_up") %>' />
                         </td>
                         <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="riferimento" runat="server" Text='<%# Eval("riferimento") %>' Visible="false" />
                              <asp:Label ID="id_conducente" runat="server" Text='<%# Eval("id_conducente") %>' Visible="false" />
                              <asp:Label ID="km_uscita" runat="server" Text='<%# Eval("km_uscita") %>' Visible="false" />
                              <asp:Label ID="km_rientro" runat="server" Text='<%# Eval("km_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_uscita" runat="server" Text='<%# Eval("litri_uscita") %>' Visible="false" />
                              <asp:Label ID="litri_rientro" runat="server" Text='<%# Eval("litri_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_max" runat="server" Text='<%# Eval("litri_max") %>' Visible="false" />
                              <asp:Label ID="data_presunto_rientro" runat="server" Text='<%# Eval("data_presunto_rientro") %>' Visible="false" />
                              <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' Visible="false" />
                              <asp:Label ID="id_gruppo_da_trasferire" runat="server" Text='<%# Eval("id_gruppo_da_trasferire") %>' Visible="false" />
                              <asp:Label ID="gruppo_da_trasferire" runat="server" Text='<%# Eval("gruppo_da_trasferire") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="richiesta_per_data" runat="server" Text='<%# Eval("richiesta_per_data") %>' Visible="false" />
                             <asp:Label ID="per_data" runat="server" Text='<%# Replace(Eval("richiesta_per_data"),".00.00","") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="id_status" runat="server" Text='<%# Eval("id_status") %>' Visible="false" />
                             <asp:Label ID="status" runat="server" Text='<%# Eval("status") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="richiesta_per_id_stazione" runat="server" Text='<%# Eval("richiesta_per_id_stazione") %>' Visible="false" />
                             <asp:Label ID="richiesta_per_stazione" runat="server" Text='<%# Eval("richiesta_per_stazione") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                             <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="lblModello" runat="server" Text='<%# Eval("modello") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left"> 
                              <asp:Button ID="btnCheckOut" runat="server" CommandName="check_out" 
                                  Text="Check Out" />
                              <asp:Button ID="btnVediCheck" runat="server" CommandName="vedi_check_out" 
                                  Text="Vedi" />
                          </td>
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr style="">
                         <td align="left">
                               <asp:Label ID="num_trasferimento" runat="server" CssClass="testo_piccolo" Text='<%# Eval("num_trasferimento") %>' />
                         </td>
                         <td align="left"> 
                             <asp:Label ID="stazione_pick_up" runat="server" Text='<%# Eval("stazione_pick_up") %>' CssClass="testo_piccolo" />
                         </td>
                         <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="riferimento" runat="server" Text='<%# Eval("riferimento") %>' Visible="false" />
                              <asp:Label ID="id_conducente" runat="server" Text='<%# Eval("id_conducente") %>' Visible="false" />
                              <asp:Label ID="km_uscita" runat="server" Text='<%# Eval("km_uscita") %>' Visible="false" />
                              <asp:Label ID="km_rientro" runat="server" Text='<%# Eval("km_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_uscita" runat="server" Text='<%# Eval("litri_uscita") %>' Visible="false" />
                              <asp:Label ID="litri_rientro" runat="server" Text='<%# Eval("litri_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_max" runat="server" Text='<%# Eval("litri_max") %>' Visible="false" />
                              <asp:Label ID="data_presunto_rientro" runat="server" Text='<%# Eval("data_presunto_rientro") %>' Visible="false" />
                              <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' Visible="false" />
                              <asp:Label ID="id_gruppo_da_trasferire" runat="server" Text='<%# Eval("id_gruppo_da_trasferire") %>' Visible="false" />
                              <asp:Label ID="gruppo_da_trasferire" runat="server" Text='<%# Eval("gruppo_da_trasferire") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="richiesta_per_data" runat="server" Text='<%# Eval("richiesta_per_data") %>' Visible="false" />
                             <asp:Label ID="per_data" runat="server" Text='<%# Replace(Eval("richiesta_per_data"),".00.00","") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="id_status" runat="server" Text='<%# Eval("id_status") %>' Visible="false" />
                             <asp:Label ID="status" runat="server" Text='<%# Eval("status") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="richiesta_per_id_stazione" runat="server" Text='<%# Eval("richiesta_per_id_stazione") %>' Visible="false" />
                             <asp:Label ID="richiesta_per_stazione" runat="server" Text='<%# Eval("richiesta_per_stazione") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                             <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="lblModello" runat="server" Text='<%# Eval("modello") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                            <asp:Button ID="btnCheckOut" runat="server" Text="Check Out" CommandName="check_out" />
                            <asp:Button ID="btnVediCheck" runat="server" CommandName="vedi_check_out" Text="Vedi" />
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
                                  <table ID="itemPlaceholderContainer" runat="server" border="1"  width="100%"   style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;font-size:x-small;">
                                      <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                          <th align="left">
                                              <asp:LinkButton ID="LinkButton16" runat="server" CommandName="order_by_numero" CssClass="testo_titolo_piccolo">Num.</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                              <asp:LinkButton ID="LinkButton8" runat="server" CommandName="order_by_stazione_pick_up" CssClass="testo_titolo_piccolo">Staz.Pick Up</asp:LinkButton>
                                          </th>
                                          <th id="Th5" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton6" runat="server" CommandName="order_by_gruppo" CssClass="testo_titolo_piccolo">Gr.</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton9" runat="server" CommandName="order_by_richiesta_per_data" CssClass="testo_titolo_piccolo">Rich. per data</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton1" runat="server" CommandName="order_by_status" CssClass="testo_titolo_piccolo">Status</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton10" runat="server" CommandName="order_by_richiesta_per_stazione" CssClass="testo_titolo_piccolo">Staz. Rich.</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton11" runat="server" CommandName="order_by_targa" CssClass="testo_titolo_piccolo">Targa</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton12" runat="server" CommandName="order_by_modello" CssClass="testo_titolo_piccolo">Modello</asp:LinkButton>
                                          </th>
                                          <th align="left">
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
     </ContentTemplate>  
   </ajaxtoolkit:TabPanel>
   <ajaxtoolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="Trasf. Da Effettuare">
     <HeaderTemplate>
            Trasferimenti In Ingresso
     </HeaderTemplate>
     <ContentTemplate>
        <div runat="server" id="div_check_in" visible="False">
       <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" >
             &nbsp;<asp:Label ID="Label56" runat="server" Text="Trasferimento Veicoli - Check In Veicolo" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
       </table>
       <table runat="server" id="table6" style="border:4px solid #444; border-bottom:0px; border-top:0px;" border="0" cellspacing="2" cellpadding="2" width="100%">
              <tr id="Tr5" runat="server">       
                <td id="Td12" valign="top" runat="server">    
                    <asp:Label ID="Label60" runat="server" Text="Targa" CssClass="testo_bold"></asp:Label>
                </td>
                <td id="Td13" valign="top" runat="server">
                    <asp:TextBox ID="txtTargaCheckIn" runat="server" Width="84px"></asp:TextBox>
                  </td>
                <td id="Td14" valign="top" runat="server">           
                    &nbsp;</td>
                <td id="Td15" align="left" valign="top" runat="server">
                    <asp:Label ID="Label61" runat="server" CssClass="testo_bold" Text="Gruppo:"></asp:Label>
                  </td>
                <td id="Td16" valign="top" runat="server">           
                    <asp:TextBox ID="txtGruppoCheckIn" runat="server" ReadOnly="True" Width="40px"></asp:TextBox>
                  </td>
                <td id="Td17" align="left" valign="top" runat="server">
                    <asp:Label ID="Label62" runat="server" CssClass="testo_bold" Text="Modello:"></asp:Label>
                  </td>
                <td id="Td18" valign="top" runat="server">           
                    <asp:TextBox ID="txtModelloCheckIn" runat="server" ReadOnly="True" Width="170px"></asp:TextBox>
                  </td>
                <td id="Td19" valign="top" runat="server" align="left">
                    <asp:Label ID="Label63" runat="server" CssClass="testo_bold" Text="Km:"></asp:Label>
                  </td>
               <td id="Td20" valign="top" runat="server">           
                   <asp:TextBox ID="txtKmUscitaCheckIn" runat="server" 
                       onKeyPress="return filterInputInt(event)" ReadOnly="True" Width="50px"></asp:TextBox>
                  </td>
                  <td ID="Td21" runat="server" valign="top">
                      <asp:Label ID="Label64" runat="server" CssClass="testo_bold" Text="Serbatoio:"></asp:Label>
                  </td>
                  <td ID="Td22" runat="server" valign="top">
                      <asp:TextBox ID="txtSerbatoioUscitaCheckIn" runat="server" 
                          onKeyPress="return filterInputInt(event)" ReadOnly="True" Width="50px"></asp:TextBox>
                      <asp:Label ID="Label65" runat="server" CssClass="testo_bold" Text="/"></asp:Label>
                      <asp:Label ID="lblSerbatoioMaxUscitaCheckIn" runat="server" CssClass="testo_bold"></asp:Label>
                  </td>
             </tr>
              <tr runat="server">       
                <td id="Td23" valign="top" runat="server" colspan="4">    
                    <asp:Label ID="idVeicoloSelezionatoCheckIn" runat="server" Visible="false"></asp:Label>
                  </td>
                <td id="Td27" runat="server">
                    &nbsp;</td>
                <td id="Td28" runat="server" align="left" >
                      &nbsp;</td>
                <td id="Td29" runat="server" align="right">
                    <asp:Label ID="lblRientro1" runat="server" CssClass="testo_bold" 
                        ForeColor="Red" Text="RIENTRO:"></asp:Label>
                  </td>
                <td runat="server" align="left">           
                    <asp:Label ID="Label961" runat="server" CssClass="testo_bold" Text="Km:"></asp:Label>
                  </td>
                <td runat="server">
                    <asp:TextBox ID="txtKmRientroCheckIn" runat="server" ReadOnly="true"
                        onKeyPress="return filterInputInt(event)" Width="50px"></asp:TextBox>
                  </td>
                <td runat="server">           
                    <asp:Label ID="Label971" runat="server" CssClass="testo_bold" 
                        Text="Serbatoio:"></asp:Label>
                </td>
                <td runat="server">
                    <asp:TextBox ID="txtSerbatoioRientroCheckIn" runat="server" ReadOnly="true"
                        onKeyPress="return filterInputInt(event)" Width="50px"></asp:TextBox>
                    <asp:Label ID="Label981" runat="server" CssClass="testo_bold" Text="/"></asp:Label>
                    <asp:Label ID="lblSerbatoioMaxRientroCheckIn" runat="server" 
                        CssClass="testo_bold"></asp:Label>
                </td>
            </tr>
           </table>
           <table runat="server" id="table7" style="border:4px solid #444;border-top:0px;" cellspacing="2" cellpadding="2" width="100%">
            <tr id="Tr4" runat="server">       
                <td id="Td3" runat="server" class="style12">    
                    <asp:Label ID="Label57" runat="server" CssClass="testo_bold" Text="Conducente"></asp:Label>
                </td>
                <td id="Td4" runat="server" class="style13">
                    <asp:DropDownList ID="dropConducenteCheckIn" runat="server"  Enabled="False"
                        AppendDataBoundItems="True" DataSourceID="sqlDrivers" DataTextField="driver" 
                        DataValueField="id">
                        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td id="Td5" runat="server" class="style14">
                    <asp:Label ID="Label58" runat="server" CssClass="testo_bold" Text="Presunto Rientro:"></asp:Label>
                </td>
                <td id="Td6" runat="server" class="style15">
                    <asp:TextBox ID="txtPresuntoRientroCheckIn" runat="server" Width="70px" Enabled="false"></asp:TextBox>
                   
                    &nbsp;<asp:TextBox ID="txtOraPresuntoRientroCheckIn" runat="server" MaxLength="2" Width="20px" Enabled="false"></asp:TextBox>
                </td>
                <td id="Td7" runat="server" class="style16">           
                    <asp:Label ID="Label59" runat="server" CssClass="testo_bold" 
                        Text="Data Rientro:"></asp:Label>
                </td>
                <td id="Td8" runat="server">
                    <asp:TextBox ID="txtDataRientroCheckIn" runat="server" Width="70px" ReadOnly="True" ></asp:TextBox>
                    &nbsp;<asp:TextBox ID="txtOraRientroCheckIn" runat="server" Width="40px" ReadOnly="True"></asp:TextBox>
                   
                </td>
                <td id="Td9" runat="server">           
                      
                </td>

                <td id="Td10" runat="server">
                 
                </td>

                <td id="Td11" runat="server">           
                    &nbsp;</td>
                <td id="Td26" runat="server">
                    &nbsp;</td>
               <td id="Td30" runat="server">           
                   &nbsp;</td>
            </tr>
            <tr id="Tr7" runat="server">       
                <td id="Td31" valign="top" colspan="11" align="center" runat="server">    
                    <asp:Button ID="btnCheckIn" runat="server" Text="Check In" />
                        &nbsp;<asp:Button ID="btnStampaFoglioTrasferimentoCheckIn" runat="server" 
                        Text="Foglio di Trasferimento" />
                        &nbsp;<asp:Button ID="btnChiudiCheckIn" runat="server" Text="Chiudi" />
                </td>
            </tr>
           </table> 
        </div>
     
        <div runat="server" id="div_ricerca_check_in">
      <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" >
             &nbsp;<asp:Label ID="Label29" runat="server" Text="Trasferimento Veicoli - Trasferimenti in Ingresso" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
       </table>
      <table cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444; border-top:0px;" border="0">
         <tr>
           <td class="style6">
               <asp:Label ID="Label49" runat="server" CssClass="testo_bold" Text="Stazione Pick Up"></asp:Label>
           </td>
           <td class="style7">
              <asp:Label ID="Label50" runat="server" Text="Stato" CssClass="testo_bold"></asp:Label>      
           </td>
           <td class="style11">
              <asp:Label ID="Label51" runat="server" Text="Gruppo" CssClass="testo_bold"></asp:Label>
           </td>
           <td class="style3">
              <asp:Label ID="Label52" runat="server" Text="Rich. per data (Da)" CssClass="testo_bold"></asp:Label>
           </td>
           <td class="style10">
              <asp:Label ID="Label53" runat="server" Text="Rich. per data (A)" CssClass="testo_bold"></asp:Label>
           </td>
           <td>
           
           </td>
         </tr>
         <tr>
           <td class="style6">
                <asp:DropDownList ID="dropCercaStazionePickCheckIn" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="sqlStazioni" DataTextField="stazione" DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Tutte</asp:ListItem>
                </asp:DropDownList>
           </td>
           <td class="style7">
                <asp:DropDownList ID="dropCercaStatusCheckIn" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlTrasferimentiStatusAStazione" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Tutti</asp:ListItem>
                </asp:DropDownList>
             </td>
           <td class="style11">
              
                <asp:DropDownList ID="dropCercaGruppoCheckIn" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlGruppiAuto" 
                    DataTextField="cod_gruppo" DataValueField="id_gruppo">
                    <asp:ListItem Selected="True" Value="0">Tutti</asp:ListItem>
                </asp:DropDownList>
             </td>
           <td class="style3">
            <a onclick="Calendar.show(document.getElementById('<%=txtRichiestaPerDataCheckInDa.ClientID%>'), '%d/%m/%Y', false)"> 
        <asp:TextBox runat="server" Width="70px" ID="txtRichiestaPerDataCheckInDa"></asp:TextBox>
                </a>
           <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                   TargetControlID="txtRichiestaPerDataCheckInDa" 
                   ID="CalendarExtender5">
            </ajaxtoolkit:CalendarExtender>--%>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                   CultureDatePlaceholder="" CultureTimePlaceholder="" 
                   CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                   CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                   CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtRichiestaPerDataCheckInDa" 
                   ID="MaskedEditExtender6">
            </ajaxtoolkit:MaskedEditExtender>
             </td>
           <td class="style10">
               <a onclick="Calendar.show(document.getElementById('<%=txtRichiestaPerDataCheckInA.ClientID%>'), '%d/%m/%Y', false)"> 
        <asp:TextBox runat="server" Width="70px" ID="txtRichiestaPerDataCheckInA"></asp:TextBox>
                   </a>
            <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                   TargetControlID="txtRichiestaPerDataCheckInA" 
                   ID="CalendarExtender6">
            </ajaxtoolkit:CalendarExtender>--%>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                   CultureDatePlaceholder="" CultureTimePlaceholder="" 
                   CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                   CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                   CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtRichiestaPerDataCheckInA" 
                   ID="MaskedEditExtender7">
            </ajaxtoolkit:MaskedEditExtender>
             </td>
           <td>
           
               &nbsp;</td>
         </tr>
         <tr>
           <td class="style6">
               <asp:Label ID="Label54" runat="server" CssClass="testo_bold" Text="Stazione Richiesta"></asp:Label>
           </td>
           <td class="style7">
           
               <asp:Label ID="Label55" runat="server" CssClass="testo_bold" Text="Veicolo"></asp:Label>
             </td>
           <td class="style11">
              
               &nbsp;</td>
           <td class="style3">
           
               &nbsp;</td>
           <td class="style10">
              
               &nbsp;</td>
           <td>
           
               &nbsp;</td>
         </tr>
         <tr>
           <td class="style6">
                <asp:DropDownList ID="dropCercaStazioneRichiestaCheckIn" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="sqlStazioni" DataTextField="stazione" DataValueField="id" 
                    Height="16px">
                    <asp:ListItem Selected="True" Value="0">Tutte</asp:ListItem>
                </asp:DropDownList>
             </td>
           <td class="style7">
           
               <asp:TextBox ID="txtCercaTargaCheckIn" runat="server" Width="90px"></asp:TextBox>
             </td>
           <td class="style11">
              
               &nbsp;</td>
           <td class="style3">
           
               &nbsp;</td>
           <td class="style10">
              
               &nbsp;</td>
           <td>
           
               &nbsp;</td>
         </tr>
         <tr>
           <td align="center" colspan="6">
               <asp:Button ID="btnCercaCheckIn" runat="server" Text="Cerca" />
           </td>
         </tr>
       </table>
       <table cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444; border-top:0px;" border="0">
         <tr>
           <td align="left">
               <asp:ListView ID="listTrasferimentiVersoStazione" runat="server" DataSourceID="sqlTrasferimentiVersoStazione" DataKeyNames="id">
                  <ItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000;">
                         <td align="left">
                              <asp:Label ID="num_trasferimento" runat="server" CssClass="testo_piccolo" Text='<%# Eval("num_trasferimento") %>' />
                         </td>
                         <td align="left">
                              <asp:Label ID="richiesta_per_id_stazione" runat="server"  Text='<%# Eval("richiesta_per_id_stazione") %>' Visible="false" />
                              <asp:Label ID="richiesta_per_stazione" runat="server" CssClass="testo_piccolo" Text='<%# Eval("richiesta_per_stazione") %>' />
                         </td>
                         <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="riferimento" runat="server" Text='<%# Eval("riferimento") %>' Visible="false" />
                              <asp:Label ID="id_conducente" runat="server" Text='<%# Eval("id_conducente") %>' Visible="false" />
                              <asp:Label ID="km_uscita" runat="server" Text='<%# Eval("km_uscita") %>' Visible="false" />
                              <asp:Label ID="km_rientro" runat="server" Text='<%# Eval("km_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_uscita" runat="server" Text='<%# Eval("litri_uscita") %>' Visible="false" />
                              <asp:Label ID="litri_rientro" runat="server" Text='<%# Eval("litri_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_max" runat="server" Text='<%# Eval("litri_max") %>' Visible="false" />
                              <asp:Label ID="id_gruppo_da_trasferire" runat="server" Text='<%# Eval("id_gruppo_da_trasferire") %>' Visible="false" />
                              <asp:Label ID="gruppo_da_trasferire" runat="server" Text='<%# Eval("gruppo_da_trasferire") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_uscita" runat="server" Text='<%# Replace(Eval("data_uscita") & "",".00.00","")  %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_presunto_rientro" runat="server" Text='<%# Eval("data_presunto_rientro") %>' Visible="false" />
                             <asp:Label ID="pres_rientro" runat="server" Text='<%# Replace(Eval("data_presunto_rientro"),".00.00","")  %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="id_status" runat="server" Text='<%# Eval("id_status") %>' Visible="false" />
                             <asp:Label ID="status" runat="server" Text='<%# Eval("status") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left"> 
                              <asp:Label ID="stazione_pick_up" runat="server" CssClass="testo_piccolo" Text='<%# Eval("stazione_pick_up") %>' />
                          </td>
                          <td align="left">
                             <asp:Label ID="id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                             <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left"> 
                              <asp:Label ID="lblModello" runat="server" CssClass="testo_piccolo" Text='<%# Eval("modello") %>' />
                          </td>
                          <td align="left">
                              <asp:Button ID="btnCheckIn" runat="server" CommandName="check_in" 
                                  Text="Vedi" />
                              <asp:Button ID="btnVediCheck" runat="server" CommandName="vedi_check_out" 
                                  Text="Vedi" />
                          </td>
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr style="">
                         <td align="left">
                              <asp:Label ID="num_trasferimento" runat="server" CssClass="testo_piccolo" Text='<%# Eval("num_trasferimento") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="richiesta_per_id_stazione" runat="server" Text='<%# Eval("richiesta_per_id_stazione") %>' Visible="false" />
                             <asp:Label ID="richiesta_per_stazione" runat="server" Text='<%# Eval("richiesta_per_stazione") %>' CssClass="testo_piccolo" />
                         </td>
                         <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="riferimento" runat="server" Text='<%# Eval("riferimento") %>' Visible="false" />
                              <asp:Label ID="id_conducente" runat="server" Text='<%# Eval("id_conducente") %>' Visible="false" />
                              <asp:Label ID="km_uscita" runat="server" Text='<%# Eval("km_uscita") %>' Visible="false" />
                              <asp:Label ID="km_rientro" runat="server" Text='<%# Eval("km_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_uscita" runat="server" Text='<%# Eval("litri_uscita") %>' Visible="false" />
                              <asp:Label ID="litri_rientro" runat="server" Text='<%# Eval("litri_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_max" runat="server" Text='<%# Eval("litri_max") %>' Visible="false" />
                              <asp:Label ID="id_gruppo_da_trasferire" runat="server" Text='<%# Eval("id_gruppo_da_trasferire") %>' Visible="false" />
                              <asp:Label ID="gruppo_da_trasferire" runat="server" Text='<%# Eval("gruppo_da_trasferire") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_uscita" runat="server" Text='<%# Replace(Eval("data_uscita") & "",".00.00","")  %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_presunto_rientro" runat="server" Text='<%# Eval("data_presunto_rientro") %>' Visible="false" />
                             <asp:Label ID="pres_rientro" runat="server" Text='<%# Replace(Eval("data_presunto_rientro"),".00.00","")  %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="id_status" runat="server" Text='<%# Eval("id_status") %>' Visible="false" />
                             <asp:Label ID="status" runat="server" Text='<%# Eval("status") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left"> 
                              <asp:Label ID="stazione_pick_up" runat="server" CssClass="testo_piccolo" Text='<%# Eval("stazione_pick_up") %>' />
                          </td>
                          <td align="left">
                             <asp:Label ID="id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                             <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left"> 
                              <asp:Label ID="lblModello" runat="server" CssClass="testo_piccolo" Text='<%# Eval("modello") %>' />
                          </td>
                          <td align="left">
                              <asp:Button ID="btnCheckIn" runat="server" CommandName="check_in" Text="Vedi" />
                              <asp:Button ID="btnVediCheck" runat="server" CommandName="vedi_check_out" Text="Vedi" />
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
                                  <table ID="itemPlaceholderContainer" runat="server" border="1"  width="100%"   style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;font-size:x-small;">
                                      <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                          <th align="left">
                                              <asp:LinkButton ID="LinkButton17" runat="server" CommandName="order_by_numero" CssClass="testo_titolo_piccolo">Num.</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                              <asp:LinkButton ID="LinkButton10" runat="server" CommandName="order_by_richiesta_per_stazione" CssClass="testo_titolo_piccolo">Staz. Arrivo</asp:LinkButton>
                                          </th>
                                          <th id="Th5" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton6" runat="server" CommandName="order_by_gruppo" CssClass="testo_titolo_piccolo">Gr.</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton9" runat="server" CommandName="order_by_data_uscita" CssClass="testo_titolo_piccolo">Uscita</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton14" runat="server" CommandName="order_by_presunto_arrivo" CssClass="testo_titolo_piccolo">Presunto Arrivo</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton15" runat="server" CommandName="order_by_data_rientro" CssClass="testo_titolo_piccolo">Rientro</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton1" runat="server" CommandName="order_by_status" CssClass="testo_titolo_piccolo">Status</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                              <asp:LinkButton ID="LinkButton8" runat="server" CommandName="order_by_stazione_pick_up" CssClass="testo_titolo_piccolo">Staz.Pick Up</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton11" runat="server" CommandName="order_by_targa" CssClass="testo_titolo_piccolo">Targa</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton13" runat="server" CommandName="order_by_modello" CssClass="testo_titolo_piccolo">Modello</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                          
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
     </ContentTemplate>
   </ajaxtoolkit:TabPanel>
   <ajaxtoolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Trasf. Da Effettuare">
     <HeaderTemplate>
            Richieste di Trasferimento
     </HeaderTemplate>
     <ContentTemplate>
         <div runat="server" id="div_ricerca" >
       <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" >
             &nbsp;<asp:Label ID="Label2" runat="server" Text="Trasferimento Veicoli - Richieste di Trasferimento" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
       </table>
       <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444; border-top:0px;" border="0">
         <tr>
           <td class="style6">
              <asp:Label ID="lblStazione" runat="server" Text="Stazione Richiesta" CssClass="testo_bold"></asp:Label>
           </td>
           <td class="style7">
           
              <asp:Label ID="lblStazione0" runat="server" Text="Stato" CssClass="testo_bold"></asp:Label>
           
           </td>
           <td class="style11">
              
              <asp:Label ID="lblStazione1" runat="server" Text="Gr. Rich." 
                   CssClass="testo_bold"></asp:Label>
              
           </td>
           <td class="style3">
           
              <asp:Label ID="lblStazione2" runat="server" Text="Rich. per data (Da)" 
                   CssClass="testo_bold"></asp:Label>
              
           </td>
           <td class="style10">
              
              <asp:Label ID="lblStazione3" runat="server" Text="Rich. per data (A)" 
                   CssClass="testo_bold"></asp:Label>
              
           </td>
           <td>
           
           </td>
         </tr>
         <tr>
           <td class="style6">
                <asp:DropDownList ID="dropCercaStazioneRichiesta" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                    DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Tutte</asp:ListItem>
                </asp:DropDownList>
           </td>
           <td class="style7">
           
                <asp:DropDownList ID="dropCercaStatus" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlTrasferimentiStatus" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Tutti</asp:ListItem>
                </asp:DropDownList>
             </td>
           <td class="style11">
              
                <asp:DropDownList ID="dropCercaGruppoRichiesto" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlGruppiAuto" 
                    DataTextField="cod_gruppo" DataValueField="id_gruppo">
                    <asp:ListItem Selected="True" Value="0">Tutti</asp:ListItem>
                </asp:DropDownList>
             </td>
           <td class="style3">
            <a onclick="Calendar.show(document.getElementById('<%=txtCercaRichiestaPerDataDa.ClientID%>'), '%d/%m/%Y', false)">
        <asp:TextBox runat="server" Width="70px" ID="txtCercaRichiestaPerDataDa"></asp:TextBox>
                </a>
            <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                   TargetControlID="txtCercaRichiestaPerDataDa" 
                   ID="txtCercaRichiestaPerDataDa_CalendarExtender">
            </ajaxtoolkit:CalendarExtender>--%>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                   CultureDatePlaceholder="" CultureTimePlaceholder="" 
                   CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                   CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                   CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaRichiestaPerDataDa" 
                   ID="txtCercaRichiestaPerDataDa_MaskedEditExtender">
            </ajaxtoolkit:MaskedEditExtender>
             </td>
           <td class="style10">
              <a onclick="Calendar.show(document.getElementById('<%=txtCercaRichiestaPerDataA.ClientID%>'), '%d/%m/%Y', false)">
        <asp:TextBox runat="server" Width="70px" ID="txtCercaRichiestaPerDataA"></asp:TextBox>
                  </a>
           <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                   TargetControlID="txtCercaRichiestaPerDataA" 
                   ID="txtCercaRichiestaPerDataA_CalendarExtender">
            </ajaxtoolkit:CalendarExtender>--%>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                   CultureDatePlaceholder="" CultureTimePlaceholder="" 
                   CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                   CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                   CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaRichiestaPerDataA" 
                   ID="txtCercaRichiestaPerDataA_MaskedEditExtender">
            </ajaxtoolkit:MaskedEditExtender>
             </td>
           <td>
           
               &nbsp;</td>
         </tr>
         <tr>
           <td class="style6">
              <asp:Label ID="lblStazione4" runat="server" Text="Stazione Pick Up" 
                    CssClass="testo_bold"></asp:Label>
           </td>
           <td class="style7">
           
              <asp:Label ID="lblStazione5" runat="server" Text="Data Richiesta (Da)" 
                   CssClass="testo_bold"></asp:Label>
              
             </td>
           <td class="style11">
              
              <asp:Label ID="lblStazione6" runat="server" Text="Data Richiesta (A)" 
                   CssClass="testo_bold"></asp:Label>
              
             </td>
           <td class="style3">
           
               &nbsp;</td>
           <td class="style10">
              
               &nbsp;</td>
           <td>
           
               &nbsp;</td>
         </tr>
         <tr>
           <td class="style6">
                <asp:DropDownList ID="dropCercaStazionePickUp" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                    DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Tutte</asp:ListItem>
                </asp:DropDownList>
             </td>
           <td class="style7">
              <a onclick="Calendar.show(document.getElementById('<%=txtCercaDataRichiestaDa.ClientID%>'), '%d/%m/%Y', false)">
        <asp:TextBox runat="server" Width="70px" ID="txtCercaDataRichiestaDa"></asp:TextBox>
                  </a>
          <%--  <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                   TargetControlID="txtCercaDataRichiestaDa" 
                   ID="txtCercaDataRichiestaDa_CalendarExtender">
            </ajaxtoolkit:CalendarExtender>--%>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                   CultureDatePlaceholder="" CultureTimePlaceholder="" 
                   CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                   CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                   CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaDataRichiestaDa" 
                   ID="txtCercaDataRichiestaDa_MaskedEditExtender">
            </ajaxtoolkit:MaskedEditExtender>
             </td>
           <td class="style11">
              <a onclick="Calendar.show(document.getElementById('<%=txtCercaDataRichiestaA.ClientID%>'), '%d/%m/%Y', false)">
        <asp:TextBox runat="server" Width="70px" ID="txtCercaDataRichiestaA"></asp:TextBox>
                  </a>
            <%--<ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
                   TargetControlID="txtCercaDataRichiestaA" 
                   ID="txtCercaDataRichiestaA_CalendarExtender">
            </ajaxtoolkit:CalendarExtender>--%>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" 
                   CultureDatePlaceholder="" CultureTimePlaceholder="" 
                   CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" 
                   CultureDateFormat="" CultureCurrencySymbolPlaceholder="" 
                   CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtCercaDataRichiestaA" 
                   ID="txtCercaDataRichiestaA_MaskedEditExtender">
            </ajaxtoolkit:MaskedEditExtender>
             </td>
           <td class="style3">
           
               &nbsp;</td>
           <td class="style10">
              
               &nbsp;</td>
           <td>
           
               &nbsp;</td>
         </tr>
         <tr>
           <td align="center" colspan="6">
               <asp:Button ID="btnCerca" runat="server" Text="Cerca" />
               <asp:Button ID="btnNuovaRichiesta" runat="server" Text="Nuova Richiesta" />
           </td>
         </tr>
       </table>
       <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444; border-top:0px;" border="0">
         <tr>
           <td align="left">
               <asp:ListView ID="listRichieste" runat="server" DataSourceID="sqlRichiesteStazione" DataKeyNames="id">
                  <ItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000;">
                          <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="note_admin" runat="server" Text='<%# Eval("note_admin") %>' Visible="false" />
                              <asp:Label ID="riferimento" runat="server" Text='<%# Eval("riferimento") %>' Visible="false" />
                              <asp:Label ID="id_gruppo_richiesto" runat="server" Text='<%# Eval("id_gruppo_richiesto") %>' Visible="false" />
                              <asp:Label ID="gruppoRichiesto" runat="server" Text='<%# Eval("gruppo_richiesto") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="richiesta_per_data" runat="server" Text='<%# Eval("richiesta_per_data") %>' Visible="false" />
                             <asp:Label ID="per_data" runat="server" Text='<%# Replace(Eval("richiesta_per_data"),".00.00","") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="id_status" runat="server" Text='<%# Eval("id_status") %>' Visible="false" />
                             <asp:Label ID="status" runat="server" Text='<%# Eval("status") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="richiesta_per_id_stazione" runat="server" Text='<%# Eval("richiesta_per_id_stazione") %>' Visible="false" />
                             <asp:Label ID="richiesta_per_stazione" runat="server" Text='<%# Eval("richiesta_per_stazione") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="operatore_richiesta" runat="server" Text='<%# Eval("operatore_richiesta") %>' CssClass="testo_piccolo" />
                          </td>
                          <%--<td align="left">
                             <asp:Label ID="riferimento" runat="server" Text='<%# Eval("riferimento") %>' CssClass="testo_piccolo" />
                          </td>--%>
                          <td align="left">
                             <asp:Label ID="data_richiesta" runat="server" Text='<%# Eval("data_richiesta") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left"> 
                             <asp:Label ID="stazione_pick_up" runat="server" Text='<%# Eval("stazione_pick_up") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left"> 
                             <asp:Label ID="id_gruppo_da_trasferire" runat="server" Text='<%# Eval("id_gruppo_da_trasferire") %>' Visible="false" />
                             <asp:Label ID="gruppo_da_trasferire" runat="server" Text='<%# Eval("gruppo_da_trasferire") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_gestione_admin" runat="server" Text='<%# Eval("data_gestione_admin") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="operatore_admin" runat="server" Text='<%# Eval("operatore_admin") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:ImageButton ID="vedi" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="vedi"/>
                          </td>
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr style="">
                         <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="riferimento" runat="server" Text='<%# Eval("riferimento") %>' Visible="false" />
                              <asp:Label ID="note_admin" runat="server" Text='<%# Eval("note_admin") %>' Visible="false" />
                              <asp:Label ID="id_gruppo_richiesto" runat="server" Text='<%# Eval("id_gruppo_richiesto") %>' Visible="false" />
                              <asp:Label ID="gruppoRichiesto" runat="server" Text='<%# Eval("gruppo_richiesto") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="richiesta_per_data" runat="server" Text='<%# Eval("richiesta_per_data") %>' Visible="false" />
                             <asp:Label ID="per_data" runat="server" Text='<%# Replace(Eval("richiesta_per_data"),".00.00","") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="id_status" runat="server" Text='<%# Eval("id_status") %>' Visible="false" />
                             <asp:Label ID="status" runat="server" Text='<%# Eval("status") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="richiesta_per_id_stazione" runat="server" Text='<%# Eval("richiesta_per_id_stazione") %>' Visible="false" />
                             <asp:Label ID="richiesta_per_stazione" runat="server" Text='<%# Eval("richiesta_per_stazione") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="operatore_richiesta" runat="server" Text='<%# Eval("operatore_richiesta") %>' CssClass="testo_piccolo" />
                          </td>
                          <%--<td align="left">
                             <asp:Label ID="riferimento" runat="server" Text='<%# Eval("riferimento") %>' CssClass="testo_piccolo" />
                          </td>--%>
                          <td align="left">
                             <asp:Label ID="data_richiesta" runat="server" Text='<%# Eval("data_richiesta") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left"> 
                             <asp:Label ID="stazione_pick_up" runat="server" Text='<%# Eval("stazione_pick_up") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left"> 
                             <asp:Label ID="id_gruppo_da_trasferire" runat="server" Text='<%# Eval("id_gruppo_da_trasferire") %>' Visible="false" />
                             <asp:Label ID="gruppo_da_trasferire" runat="server" Text='<%# Eval("gruppo_da_trasferire") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_gestione_admin" runat="server" Text='<%# Eval("data_gestione_admin") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="operatore_admin" runat="server" Text='<%# Eval("operatore_admin") %>' CssClass="testo_piccolo" />
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
                                &nbsp;
                              </td>
                          </tr>
                      </table>
                  </EmptyDataTemplate>
                  <LayoutTemplate>
                      <table id="Table2" runat="server" width="98%">
                          <tr id="Tr1" runat="server">
                              <td id="Td1" runat="server">
                                  <table ID="itemPlaceholderContainer" runat="server" border="1"  width="100%"   style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;font-size:x-small;">
                                      <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                          <th align="left" ID="Th5" runat="server">
                                          
                                              <asp:LinkButton ID="LinkButton6" runat="server" CommandName="order_by_gruppo" 
                                                  CssClass="testo_titolo_piccolo">Gr.</asp:LinkButton>
                                          
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton9" runat="server" CommandName="order_by_richiesta_per_data" CssClass="testo_titolo_piccolo">Rich. per data</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton1" runat="server" CommandName="order_by_status" CssClass="testo_titolo_piccolo">Status</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton10" runat="server" CommandName="order_by_richiesta_per_stazione" CssClass="testo_titolo_piccolo">Staz. Rich.</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton3" runat="server" CommandName="order_by_operatore_richiesta" CssClass="testo_titolo_piccolo">Op. Rich.</asp:LinkButton>
                                          </th>
                                          <%--<th align="left">
                                             <asp:LinkButton ID="LinkButton4" runat="server" CommandName="order_by_riferimento" CssClass="testo_titolo_piccolo">Riferimento</asp:LinkButton>
                                          </th>--%>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton2" runat="server" CommandName="order_by_data_richiesta" CssClass="testo_titolo_piccolo">Data Rich.</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton8" runat="server" CommandName="order_by_stazione_pick_up" CssClass="testo_titolo_piccolo">Staz.Pick Up</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton4" runat="server" CommandName="order_by_gruppo_da_trasferire" CssClass="testo_titolo_piccolo" ToolTip="Gruppo da trasferire">Gr.T.</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton5" runat="server" CommandName="order_by_data_gestione_admin" CssClass="testo_titolo_piccolo">Data Gest. Admin</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton7" runat="server" CommandName="order_by_operatore_admin" CssClass="testo_titolo_piccolo">Op. Admin</asp:LinkButton>
                                          </th>
                                          <th align="left">
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
    
    <div runat="server" id="div_dati" visible="False">
       <table border="0" cellpadding="0" cellspacing="0" width="1024px" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" >
             &nbsp;<asp:Label ID="Label1" runat="server" Text="Trasferimento Veicoli - Stazione - Stato: " CssClass="testo_titolo"></asp:Label>
             &nbsp;<asp:Label ID="lblStato" runat="server" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
       </table>
       <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444; border-top:0px;" border="0">
         <tr>
            <td class="style15">
              <asp:Label ID="Label44" runat="server" Text="Richiesta per Stazione" CssClass="testo_bold"></asp:Label>
            </td>
            <td class="style3">
              <asp:Label ID="Label41" runat="server" Text="Gruppo Richiesto" CssClass="testo_bold"></asp:Label>
            </td>
            <td class="style2">
              <asp:Label ID="Label42" runat="server" Text="Per data/ore:" CssClass="testo_bold"></asp:Label>
            </td>
            <td>
            
              <asp:Label ID="Label43" runat="server" Text="PREN. Riferimento/Causale" CssClass="testo_bold"></asp:Label>
            
            </td>
         </tr>
         <tr>
            <td class="style15" valign="top">
                <asp:DropDownList ID="dropStazioneRichiesta" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                    DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style3" valign="top">
                <asp:DropDownList ID="dropGruppoRichiesto" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlGruppiAuto" 
                    DataTextField="cod_gruppo" DataValueField="id_gruppo">
                    <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
                </asp:DropDownList>
             </td>
       <td class="style2" valign="top">
             <a onclick="Calendar.show(document.getElementById('<%=txtRichiestaPerData.ClientID%>'), '%d/%m/%Y', false)"> 
        <asp:TextBox runat="server" Width="70px" ID="txtRichiestaPerData"></asp:TextBox>
            </a>
           <%-- <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" TargetControlID="txtRichiestaPerData" ID="CalendarExtender2">
            </ajaxtoolkit:CalendarExtender>--%>
            <ajaxtoolkit:MaskedEditExtender runat="server" Mask="99/99/9999" MaskType="Date" CultureDatePlaceholder="" CultureTimePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureDateFormat="" CultureCurrencySymbolPlaceholder="" CultureAMPMPlaceholder="" Enabled="True" TargetControlID="txtRichiestaPerData" ID="MaskedEditExtender1">
            </ajaxtoolkit:MaskedEditExtender>
      &nbsp;<asp:TextBox ID="txtRichiestaPerOra" runat="server" Width="20px" MaxLength="2" onKeyPress="return filterInputInt(event)"></asp:TextBox>
              </td>
            <td valign="top">
            
                <asp:TextBox ID="txtRiferimento" runat="server" Height="50px" MaxLength="200" 
                    TextMode="MultiLine" Width="370px"></asp:TextBox>
             </td>
         </tr>
         <tr runat="server" id="riga_annullamento_1">
            <td class="style12" valign="top" colspan="2" runat="server">
              <asp:Label ID="Label45" runat="server" Text="Motivo Annullamento Richiesta" CssClass="testo_bold"></asp:Label>
             </td>
            <td class="style2" valign="top" runat="server">
              
            </td>
            <td valign="top" runat="server">
            
            </td>
         </tr>
         <tr runat="server" id="riga_annullamento_2">
            <td class="style5" valign="top" colspan="4" runat="server">
            
                <asp:TextBox ID="txtMotivoAnnullamento" runat="server" Height="50px" 
                    MaxLength="200" TextMode="MultiLine" Width="774px"></asp:TextBox>
             </td>
         </tr>
         <tr>
            <td class="style15">
              <asp:Label ID="lblInfoStazionePick" runat="server" CssClass="testo_bold"></asp:Label>
            </td>
            <td class="style3">
              <asp:Label ID="lblInfoGruppoPick" runat="server" CssClass="testo_bold"></asp:Label>
            </td>
            <td class="style2">
              
            </td>
            <td>
            
                &nbsp;</td>
         </tr>
         <tr>
            <td class="style15" valign="top">
              <asp:Label ID="lblStazionePickUp" runat="server" CssClass="testo"></asp:Label>    
            </td>
            <td class="style3" valign="top">
              <asp:Label ID="lblGruppoPickUp" runat="server" CssClass="testo"></asp:Label>
            </td>
            <td class="style2" valign="top">
              
                &nbsp;</td>
            <td valign="top">
              <asp:Label ID="lblNoteAdminXStazione" runat="server" CssClass="testo"></asp:Label>    
            </td>
         </tr>
         </table>
         <div runat="server" id="tab_admin">
         <table cellpadding="0" cellspacing="0" width="1024px" style="border:4px solid #444; border-top:0px; border-bottom:0px;" border="0">
           <tr>
               <td align="left" style="color: #FFFFFF;background-color:#444;" colspan="3" >
                 &nbsp;<asp:Label ID="Label3" runat="server" Text="Trasferimento Veicoli - Admin - Stato: " CssClass="testo_titolo"></asp:Label>

                   &nbsp;<asp:Label ID="lblStato2" runat="server" CssClass="testo_titolo"></asp:Label>
                 
               </td>
           </tr>
           <tr>
            <td class="style13">
              
              <asp:Label ID="Label46" runat="server" Text="Stazione di Pick Up" CssClass="testo_bold"></asp:Label>
              
            </td>
            <td class="style14">
              
              <asp:Label ID="Label47" runat="server" Text="Gruppo da Trasferire" CssClass="testo_bold"></asp:Label>
              
            </td>
            <td>
              
              <asp:Label ID="Label48" runat="server" Text="Note" 
                    CssClass="testo_bold"></asp:Label>
              
            </td>
           </tr>
           <tr>
            <td class="style13" valign="top">
              
                <asp:DropDownList ID="dropStazionePickUp" runat="server" Enabled="False"
                    AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                    DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                </asp:DropDownList>
              
            </td>
            <td class="style14" valign="top">
              
                <asp:DropDownList ID="dropGruppoDaTrasferire" runat="server" Enabled="False"
                    AppendDataBoundItems="True" DataSourceID="sqlGruppiAuto" 
                    DataTextField="cod_gruppo" DataValueField="id_gruppo">
                    <asp:ListItem Selected="True" Value="0">...</asp:ListItem>
                </asp:DropDownList>
              
            </td>
            <td>
              
                <asp:TextBox ID="txtNoteAdmin" runat="server" Height="50px" 
                    MaxLength="200" TextMode="MultiLine" Width="485px"></asp:TextBox>
              
            </td>
           </tr>
           <tr>
            <td class="style13">
              
                &nbsp;</td>
            <td class="style14">
              
                &nbsp;</td>
            <td>
              
                &nbsp;</td>
           </tr>
           </table>
           <table cellpadding="0" cellspacing="0" width="1024px" style="border:4px solid #444; border-top:0px; border-bottom:0px;" border="0" runat="server" id="Table3">
           <tr runat="server">
            <td colspan="3" runat="server">
              <asp:Label ID="Label4" runat="server" Text="Gruppi da ricercare:" CssClass="testo_bold"></asp:Label>
            </td>
           </tr>
           <tr runat="server">
            <td valign="top" width="45%" 
                
                   style="border-left-style: solid; border-left-width: thin; border-left-color: #000000; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #000000;" 
                   runat="server">
                <asp:ListBox ID="listGruppiPick" runat="server" DataSourceID="sqlGruppiAuto" 
                    DataTextField="cod_gruppo" DataValueField="id_gruppo" Height="120px" 
                    SelectionMode="Multiple" AppendDataBoundItems="True" Width="100%"></asp:ListBox>
                    <br />&nbsp;  
            </td>
            <td valign="top" width="10%" align="center" 
                
                
                   style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #000000" 
                   runat="server">
                      <asp:Button ID="PassaUnoGruppi" runat="server" Text=">" /><br />
                      <asp:Button ID="TornaUnoGruppi" runat="server" Text="<" /><br />
                      <asp:Button ID="PassaTuttiGruppi" runat="server" Text=">>" /><br />
                      <asp:Button ID="TornaTuttiGruppi" runat="server" Text="<<" />
                      </td>
             <td valign="top" width="45%" 
                   style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #000000; border-right-style: solid; border-right-width: thin; border-right-color: #000000" 
                   runat="server">
                <asp:ListBox ID="listGruppiPickSelezionati" runat="server" 
                     Height="120px" 
                    SelectionMode="Multiple" AppendDataBoundItems="True" Width="100%"></asp:ListBox>
                <br />&nbsp;
            </td>
           </tr>
           
           <tr runat="server">
            <td colspan="3" runat="server">
              <asp:Label ID="Label19" runat="server" Text="Cerca gruppi nelle seguenti stazioni:" CssClass="testo_bold"></asp:Label>
            </td>
           </tr>
           <tr runat="server">
            <td valign="top" width="45%" 
                
                   style="border-left-style: solid; border-left-width: thin; border-left-color: #000000; border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #000000;" 
                   runat="server">
                <asp:ListBox ID="listStazioniPick" runat="server" DataSourceID="sqlStazioni" 
                    DataTextField="stazione" DataValueField="id" Height="120px" 
                    SelectionMode="Multiple" AppendDataBoundItems="True" Width="100%"></asp:ListBox>
                    <br />&nbsp;  
                      
            </td>
            <td valign="top" width="10%" align="center" 
                
                
                   style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #000000" 
                   runat="server">
                      <asp:Button ID="PassaUnoStaz" runat="server" Text=">" /><br />
                      <asp:Button ID="TornaUnoStaz" runat="server" Text="<" /><br />
                      <asp:Button ID="PassaTuttiStaz" runat="server" Text=">>" /><br />
                      <asp:Button ID="TornaTuttiStaz" runat="server" Text="<<" />
                      </td>
            <td valign="top" width="45%" 
                   style="border-bottom-style: solid; border-bottom-width: thin; border-bottom-color: #000000; border-right-style: solid; border-right-width: thin; border-right-color: #000000" 
                   runat="server">
                <asp:ListBox ID="listStazioniPickSelezionate" runat="server" 
                     Height="120px" 
                    SelectionMode="Multiple" AppendDataBoundItems="True" Width="100%"></asp:ListBox>
                <br />&nbsp;
            </td>
           </tr>
           <tr runat="server">
            <td valign="top" 
                   style="border-left: thin solid #000000; border-bottom: thin solid #000000; width: 90%;" 
                   colspan="3" runat="server">
                <asp:DataList ID="listTrasferimentiAppoggio" runat="server" 
                    DataSourceID="sqlTrasferimentiAppoggio" Width="98%" 
                    RepeatDirection="Horizontal">
                   <ItemTemplate>
                    <tr runat="server" id="riga_stazione" >
                         <td bgcolor="#19191b" width="50%" >
                              <asp:Label ID="id_stazione" runat="server" Text='<%# Eval("id_stazione") %>' Visible="false" />
                              <asp:Label ID="stazione" runat="server" Text='<%# Eval("stazione") %>' CssClass="testo_bold" />
                              &nbsp;&nbsp;&nbsp;
                              <asp:Label ID="distanza" runat="server" Text='<%# Eval("distanza") %>' CssClass="testo" />
                              &nbsp;&nbsp;&nbsp;
                              
                              <asp:SqlDataSource ID="sqlTrasferimentiAppoggioGruppi" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" >
                              </asp:SqlDataSource>
                         </td>
                         <td bgcolor="#19191b" width="50%">
                            <asp:Label ID="Label7" runat="server" Text="Gruppo:" CssClass="testo"  />
                            <asp:DropDownList ID="dropGruppoDaScegliere" runat="server">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSegliGruppo" runat="server" Text="Scegli Gruppo" CommandName="scegli_gruppo" />
                         </td>
                     </tr>
                     <tr> 
                        <td  colspan="2">
                           
                        </td>
                     </tr>
                     <tr>
                        <td colspan="2">
                <asp:DataList ID="listTrasferimentiAppoggioGruppi" runat="server" 
                    DataSourceID="sqlTrasferimentiAppoggioGruppi" Width="100%" 
                    RepeatDirection="Horizontal" RepeatColumns="4">
                   <ItemTemplate> 
                      
                           <table border="1" cellpadding="0" cellspacing="0">
                            <tr>
                               <td align="left" style="color: #FFFFFF;background-color:#444;" colspan="6" >
                                 &nbsp;<asp:Label ID="Label2" runat="server" Text="Gruppo " CssClass="testo_titolo"></asp:Label>
                                 <asp:Label ID="Label28" runat="server" Text='<%# Eval("cod_gruppo") %>' CssClass="testo_titolo" />
                                   
                               </td>
                             </tr>
                           <tr>
                              <td width="20px" style="font-size:16px">   
                                 <asp:Label ID="Label5" runat="server" Text='<%# Eval("valore_iniziale") %>' CssClass="testo_bold" />
                              </td>
                              <td width="20px" bgcolor="white">
                                 <asp:Label ID="Label30" runat="server" Text='00/12' CssClass="testo" />
                              </td>
                              <td width="20px" bgcolor="white">
                                 <asp:Label ID="Label66" runat="server" Text='12/16' CssClass="testo" />
                              </td>
                              <td width="20px" bgcolor="white">
                                 <asp:Label ID="Label67" runat="server" Text='16/20' CssClass="testo" />
                              </td>
                              <td width="20px" bgcolor="white">
                                 <asp:Label ID="Label68" runat="server" Text='20/24' CssClass="testo" />
                              </td>
                              <td width="20px" bgcolor="white">
                                 <asp:Label ID="Label69" runat="server" Text='00/24' CssClass="testo" />
                              </td>
                           </tr>
                           <tr>
                              <td width="20px" bgcolor="white">
                                <asp:Label ID="Label70" runat="server" Text='PR.U.' CssClass="testo" />
                              </td>
                              <td width="20px" style="font-size:16px">
                                 <asp:Label ID="Label6" runat="server" Text='<%# Eval("pru_12") %>' CssClass="testo" />   <%--08/12--%>
                              </td>
                              <td width="20px" style="font-size:16px">
                                 <asp:Label ID="Label8" runat="server" Text='<%# Eval("pru_16") %>' CssClass="testo" /> <%--12/16--%>
                              </td>
                              <td width="20px" style="font-size:16px">
                                 <asp:Label ID="Label9" runat="server" Text='<%# Eval("pru_20") %>' CssClass="testo" /> <%--16/20--%>
                              </td>
                              <td width="20px" style="font-size:16px">
                                 <asp:Label ID="Label10" runat="server" Text='<%# Eval("pru_24") %>' CssClass="testo" /> <%--20/23--%>
                              </td>
                              <td width="20px" style="font-size:16px">
                                 <asp:Label ID="Label11" runat="server" Text='<%# Eval("pru_tot") %>' CssClass="testo" /> <%--TOT--%>
                              </td>
                           </tr>
                           <tr>
                              <td width="20px" style="font-size:16px" bgcolor="white">
                                R.P.
                              </td>
                              <td width="20px" style="font-size:16px">
                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("rp_12") %>' CssClass="testo" /> <%--08/12--%>
                              </td>
                              <td width="20px" style="font-size:16px">
                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("rp_16") %>' CssClass="testo" /> <%--12/16--%>
                              </td>
                              <td width="20px" style="font-size:16px">
                                <asp:Label ID="Label14" runat="server" Text='<%# Eval("rp_20") %>' CssClass="testo" /> <%--16/20--%>
                              </td>
                              <td width="20px" style="font-size:16px">
                                <asp:Label ID="Label15" runat="server" Text='<%# Eval("rp_24") %>' CssClass="testo" /> <%--20/23--%>
                              </td>
                              <td width="20px" style="font-size:16px">
                                <asp:Label ID="Label16" runat="server" Text='<%# Eval("rp_tot") %>' CssClass="testo" /> <%--TOT--%>
                              </td>
                           </tr>
                           <tr>
                              <td width="20px" style="font-size:16px" bgcolor="white"> 
                                R.C.
                              </td>
                              <td width="20px" style="font-size:16px">
                                <asp:Label ID="Label17" runat="server" Text='<%# Eval("rc_12") %>' CssClass="testo" /> <%--08/12--%>
                              </td>
                              <td width="20px" style="font-size:16px">
                                <asp:Label ID="Label18" runat="server" Text='<%# Eval("rc_16") %>' CssClass="testo" /> <%--12/16--%>
                              </td>
                              <td width="20px" style="font-size:16px">
                                <asp:Label ID="Label20" runat="server" Text='<%# Eval("rc_20") %>' CssClass="testo" />  <%--16/20--%>
                              </td>
                              <td width="20px" style="font-size:16px">
                                <asp:Label ID="Label21" runat="server" Text='<%# Eval("rc_24") %>' CssClass="testo" />  <%--20/23--%>
                              </td>
                              <td width="20px" style="font-size:16px">
                                <asp:Label ID="Label22" runat="server" Text='<%# Eval("rc_tot") %>' CssClass="testo" />   <%--TOT--%>
                              </td>
                           </tr>
                           <tr>
                              <td width="20px" style="font-size:16px" bgcolor="white">
                                TOT
                              </td>
                              <td width="20px" style="font-size:16px">
                                 <asp:Label ID="Label23" runat="server" Text='<%# Eval("tot_12") %>' CssClass="testo" />   <%-- TOT. 08/12--%>
                              </td>
                              <td width="20px" style="font-size:16px"> 
                                 <asp:Label ID="Label24" runat="server" Text='<%# Eval("tot_16") %>' CssClass="testo" />  <%-- TOT. 12/16--%>
                              </td>
                              <td width="20px" style="font-size:16px">
                                 <asp:Label ID="Label25" runat="server" Text='<%# Eval("tot_20") %>' CssClass="testo" />  <%-- TOT. 16/20--%>
                              </td>
                              <td width="20px" style="font-size:16px">
                                 <asp:Label ID="Label26" runat="server" Text='<%# Eval("tot_24") %>' CssClass="testo" />  <%-- TOT. 20/23--%>
                              </td>
                              <td width="20px" style="font-size:16px">  
                                 <asp:Label ID="Label27" runat="server" Text='<%# Eval("tot_tot") %>' CssClass="testo_bold" /> <%-- TOT. GIORN.--%>
                              </td>
                           </tr>
                         </table>
                        
                    </ItemTemplate>
                </asp:DataList>
                </td>
               </tr>
               </ItemTemplate>
              </asp:DataList>        
            </td>
           </tr>
         </table>
        </div>
        <table cellpadding="0" cellspacing="2" width="1024px" style="border:4px solid #444; border-top:0px;" border="0">
         <tr>
            <td align="center" valign="top" class="style4" colspan="4">
                  <asp:Button ID="btnSalvaRichiesta" runat="server" Text="Salva" />
                  &nbsp;<asp:Button ID="btnCercaGruppi" runat="server" Text="Cerca gruppi" />
                  &nbsp;<asp:Button ID="btnAccettaTrasferimento" runat="server" Text="Accetta Trasferimento" />
                  &nbsp;<asp:Button ID="btnNegaTrasferimento" runat="server" Text="Nega Trasferimento" OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler negare il trasferimento richiesto?'));" />
                  &nbsp;<asp:Button ID="btnAnnullaRichiesta" runat="server" Text="Annulla richiesta" OnClientClick="javascript: return(window.confirm ('Sei sicuro di voler annullare la richiesta di trasferimento?'));" />
                  &nbsp;
                  <asp:Button ID="btnAnnulla" runat="server" Text="Chiudi" />
             </td>
         </tr>
         </table>
      </div>
     </ContentTemplate>
   </ajaxtoolkit:TabPanel>
   <ajaxtoolkit:TabPanel ID="panel_extra" runat="server" HeaderText="Trasf.Interni/Trasferimenti">
     <HeaderTemplate>
            Trasf.Interni/Trasferimenti
     </HeaderTemplate>
     <ContentTemplate>
       <div runat="server" id="div_traferimento_interno" visible="False">
       <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" >
             &nbsp;<asp:Label ID="Label71" runat="server" Text="Trasferimento Veicoli - Check Veicolo" CssClass="testo_titolo"></asp:Label>
           </td>
         </tr>
       </table>
       <table runat="server" id="table8" style="border:4px solid #444; border-bottom:0px; border-top:0px;" border="0" cellspacing="2" cellpadding="2" width="100%">
              <tr id="Tr8" runat="server">       
                <td id="Td39" valign="top" runat="server">    
                    <asp:Label ID="Label75" runat="server" Text="Targa" CssClass="testo_bold"></asp:Label>
                </td>
                <td id="Td40" valign="top" runat="server">
                    <asp:TextBox ID="txtTargaInterno" runat="server" Width="84px"></asp:TextBox>
                </td>
                <td id="Td41" valign="top" runat="server" >
                      <asp:Button ID="btnScegliTargaInterno" runat="server" Text="Seleziona" />
                 
                      <asp:Button ID="btnAnnullaInterno" runat="server" 
                          Text="Annulla" Visible="False" />
                </td>
                <td id="Td42" align="left" valign="top" runat="server">
                  <asp:Label ID="Label76" runat="server" Text="Gruppo:" CssClass="testo_bold"></asp:Label>
                </td>
                <td id="Td43" valign="top" runat="server">           
                   <asp:TextBox ID="txtGruppoInterno" runat="server" Width="40px" ReadOnly="True"></asp:TextBox>
                </td>
                <td id="Td44" align="left" valign="top" runat="server" class="style17">
                  <asp:Label ID="Label77" runat="server" Text="Modello:" CssClass="testo_bold"></asp:Label>
                </td>
                <td id="Td45" valign="top" runat="server">           
                   <asp:TextBox ID="txtModelloInterno" runat="server" Width="170px" ReadOnly="True"></asp:TextBox>
                </td>
                <td id="Td46" align="left" valign="top" runat="server">
                  <asp:Label ID="Label78" runat="server" Text="Km:" CssClass="testo_bold"></asp:Label>
                </td>
                <td id="Td47" valign="top" runat="server">           
                   <asp:TextBox ID="txtKmInterno" runat="server" Width="50px" ReadOnly="True" 
                        onKeyPress="return filterInputInt(event)"></asp:TextBox>
                </td>
                <td id="Td48" valign="top" runat="server">
                  <asp:Label ID="lblSerbatoioInterno" runat="server" Text="Serbatoio:" CssClass="testo_bold"></asp:Label>
               </td>
               <td id="Td49" valign="top" runat="server">           
                   <asp:TextBox ID="txtSerbatoioInterno" runat="server" Width="50px" ReadOnly="True" 
                       onKeyPress="return filterInputInt(event)"></asp:TextBox>
                   <asp:Label ID="Label80" runat="server" Text="/" CssClass="testo_bold"></asp:Label>
                   <asp:Label ID="lblSerbatoioMaxInterno" runat="server" CssClass="testo_bold"></asp:Label>
               </td>
            </tr>
            <tr runat="server" visible="False" id="riga_rientro_interno">       
                <td id="Td50" valign="top" runat="server">    
                    &nbsp;</td>
                <td id="Td51" valign="top" runat="server">
                    &nbsp;</td>
                <td id="Td52" valign="top" runat="server">
                    <asp:Label ID="idTrasfInterno" runat="server" Visible="False"></asp:Label>
                </td>
                <td id="Td53" align="left" runat="server">
                    <asp:Label ID="lblRientroInterno0" runat="server" CssClass="testo_bold" 
                        ForeColor="Red" Text="RIENTRO:"></asp:Label>
                </td>
                <td id="Td54" runat="server" colspan="2" align="right">           
                    <asp:Label ID="lblDataPresuntoRientroInterno0" runat="server" 
                        CssClass="testo_bold" Text="Data Arrivo:"></asp:Label>
                </td>
                <td id="Td56" align="left" runat="server">                 
                      <a onclick="Calendar.show(document.getElementById('<%=txtDataRientroInterno.ClientID%>'), '%d/%m/%Y', false)"> 
                    <asp:TextBox ID="txtDataRientroInterno" runat="server" ReadOnly="True" Width="70px"></asp:TextBox>
                          </a>
                  <%--   <ajaxtoolkit:MaskedEditExtender ID="MaskedEditExtender10" 
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataRientroInterno">
                    </ajaxtoolkit:MaskedEditExtender>--%>


                    <asp:TextBox ID="txtOraRientroInterno" runat="server" ReadOnly="True" Width="40px"></asp:TextBox>
                    <ajaxtoolkit:MaskedEditExtender ID="MaskedEditExtender11" runat="server" 
                        CultureName="en-US" Mask="99:99" MaskType="Time" 
                        TargetControlID="txtOraRientroInterno" CultureAMPMPlaceholder="AM;PM" 
                        CultureCurrencySymbolPlaceholder="$" CultureDateFormat="MDY" 
                        CultureDatePlaceholder="/" CultureDecimalPlaceholder="." 
                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                    </ajaxtoolkit:MaskedEditExtender>
                </td>
                <td align="left" runat="server">
                  <asp:Label ID="Label196" runat="server" Text="Km:" CssClass="testo_bold"></asp:Label>
                </td>
                <td runat="server">           
                   <asp:TextBox ID="txtKmRientroInterno" runat="server" Width="50px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
                </td>
                <td runat="server">
                  <asp:Label ID="Label198" runat="server" Text="Serbatoio: " CssClass="testo_bold"></asp:Label>
                </td>
               <td runat="server">           
                   <asp:TextBox ID="txtSerbatoioRientroInterno" runat="server" Width="50px" onKeyPress="return filterInputInt(event)"></asp:TextBox>
                   <asp:Label ID="Label199" runat="server" Text="/" CssClass="testo_bold"></asp:Label>
                   <asp:Label ID="lblSerbatoioMaxRientroInterno" runat="server" CssClass="testo_bold"></asp:Label>
                </td>
            </tr>
           </table>
           <table runat="server" id="table81" style="border:4px solid #444;border-top:0px;" cellspacing="2" cellpadding="2" width="100%">
            <tr runat="server">       
                <td runat="server">    
                    <asp:Label ID="lblPresuntoRientroInterno2" runat="server" CssClass="testo_bold" 
                        Text="Staz. Uscita:"></asp:Label>
                </td>
                <td runat="server" class="style21">
                    <asp:DropDownList ID="dropStazionePickUpInterno" runat="server" 
                        AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                        DataValueField="id">
                        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td runat="server" class="style22">
                    <asp:Label ID="lblDataUscita" runat="server" CssClass="testo_bold" 
                        Text="Data Uscita:"></asp:Label>
                </td>
                <td runat="server">
                       <a onclick="Calendar.show(document.getElementById('<%=txtDataUscitaInterno.ClientID%>'), '%d/%m/%Y', false)"> 
                    <asp:TextBox ID="txtDataUscitaInterno" runat="server" Width="70px"></asp:TextBox>
                    </a>
                  <%--  <ajaxtoolkit:CalendarExtender ID="txtPresuntoRientroInterno0_CalendarExtender" 
                        runat="server" Enabled="True" Format="dd/MM/yyyy" 
                        TargetControlID="txtDataUscitaInterno">
                    </ajaxtoolkit:CalendarExtender>--%>
                    <ajaxtoolkit:MaskedEditExtender ID="txtDataUscitaInterno_MaskedEditExtender" 
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataUscitaInterno">
                    </ajaxtoolkit:MaskedEditExtender>
                    <asp:TextBox ID="txtOraUscitaInterno" runat="server" Width="40px"></asp:TextBox>
                    <ajaxtoolkit:MaskedEditExtender ID="MaskedEditExtender9" runat="server" 
                        CultureName="en-US" Mask="99:99" MaskType="Time" 
                        TargetControlID="txtOraUscitaInterno" CultureAMPMPlaceholder="AM;PM" 
                        CultureCurrencySymbolPlaceholder="$" CultureDateFormat="MDY" 
                        CultureDatePlaceholder="/" CultureDecimalPlaceholder="." 
                        CultureThousandsPlaceholder="," CultureTimePlaceholder=":" Enabled="True">
                    </ajaxtoolkit:MaskedEditExtender>
                </td>
                <td runat="server">
                    &nbsp;</td>
                <td runat="server">
                    &nbsp;</td>
            </tr>
            <tr runat="server">       
                <td runat="server">    
                    <asp:Label ID="lblPresuntoRientroInterno1" runat="server" CssClass="testo_bold" 
                        Text="Staz. Rientro:"></asp:Label>
                </td>
                <td runat="server" class="style21">
                    <asp:DropDownList ID="dropStazionePresuntoDropOffInterno" runat="server" 
                        AppendDataBoundItems="True" DataSourceID="sqlStazioni" DataTextField="stazione" 
                        DataValueField="id">
                        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td runat="server" class="style22">
                    <asp:Label ID="lblPresuntoRientroInterno0" runat="server" CssClass="testo_bold" 
                        Text="Presunto Rientro:"></asp:Label>
                </td>
                <td runat="server">
                    <a onclick="Calendar.show(document.getElementById('<%=txtPresuntoRientroInterno.ClientID%>'), '%d/%m/%Y', false)"> 
                    <asp:TextBox ID="txtPresuntoRientroInterno" runat="server" Width="70px"></asp:TextBox>
                        </a>
                   <%-- <ajaxtoolkit:CalendarExtender ID="txtPresuntoRientroInterno_CalendarExtender" 
                        runat="server" Enabled="True" Format="dd/MM/yyyy" 
                        TargetControlID="txtPresuntoRientroInterno">
                    </ajaxtoolkit:CalendarExtender>--%>
                    <ajaxtoolkit:MaskedEditExtender ID="txtPresuntoRientroInterno_MaskedEditExtender" 
                        runat="server" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtPresuntoRientroInterno">
                    </ajaxtoolkit:MaskedEditExtender>
                    <asp:TextBox ID="txtPresuntoRientroOraInterno" runat="server" Width="40px"></asp:TextBox>
                    <ajaxtoolkit:MaskedEditExtender ID="txtPresuntoRientroOraInterno_MaskedEditExtender" 
                        runat="server" CultureName="en-US" Mask="99:99" 
                        MaskType="Time" TargetControlID="txtPresuntoRientroOraInterno" 
                        CultureAMPMPlaceholder="AM;PM" CultureCurrencySymbolPlaceholder="$" 
                        CultureDateFormat="MDY" CultureDatePlaceholder="/" 
                        CultureDecimalPlaceholder="." CultureThousandsPlaceholder="," 
                        CultureTimePlaceholder=":" Enabled="True">
                    </ajaxtoolkit:MaskedEditExtender>
                </td>
                <td runat="server">
                    <asp:Label ID="Label982" runat="server" CssClass="testo_bold" Text="Conducente"></asp:Label>
                </td>
                <td runat="server">
                    <asp:DropDownList ID="dropDriversInterno" runat="server" 
                        AppendDataBoundItems="True" DataSourceID="sqlDrivers" DataTextField="driver" 
                        DataValueField="id">
                        <asp:ListItem Selected="True" Value="0">Seleziona...</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
               <tr runat="server">
                   <td runat="server">
                       <asp:Label ID="Label983" runat="server" CssClass="testo_bold" 
                           Text="Causale"></asp:Label>
                   </td>

                   <td runat="server" class="style21" colspan="2">
                       <asp:TextBox ID="txtRiferimentoInterno" runat="server" Height="50px" MaxLength="200" TextMode="MultiLine" Width="370px"></asp:TextBox>
                   </td>
                   <td runat="server" colspan="3">
                       <asp:Label ID="lblStatoAut" runat="server" CssClass="testo_bold" ForeColor="Red" Text=""></asp:Label>
                       <asp:Label ID="lblNumTrasferimento" runat="server" CssClass="testo_bold" ForeColor="white" Text="" Visible="true"></asp:Label>
                   </td>
               </tr>
               <tr runat="server">
                   <td runat="server" align="center" colspan="6" valign="top">
                       <asp:Button ID="btnAutorizzaTrasferimentoInterno" runat="server" Text="Autorizza Trasferimento" Visible="False" />
                       <asp:Button ID="btnNegaTrasferimentoInterno" runat="server" Text="Nega Trasferimento" Visible="False" />
                       
                       &nbsp;&nbsp;&nbsp;<asp:Button ID="btnCheckOutInterno" runat="server" 
                           OnClientClick="javascript: return(window.confirm ('Attenzione: sei sicuro di voler effettuare un trasferimento senza utilizzare la normale procedura di richiesta alla sede?.'));" 
                           Text="Salva - Check Out" />
                       <asp:Button ID="btnVediCheckInterno" runat="server" Text="Vedi Check" 
                           Visible="False" />
                       &nbsp;<asp:Button ID="btnStampaFoglioTrasferimentoInterno" runat="server" 
                           Text="Foglio di Trasferimento" />
                       &nbsp;<asp:Button ID="btnChiudiCheckInterno" runat="server" Text="Chiudi" />
                       <asp:Label ID="idVeicoloSelezionatoInterno" runat="server" Visible="False"></asp:Label>
                       <asp:Label ID="idGruppoInterno" runat="server" Visible="False"></asp:Label>
                       <asp:Label ID="stato_trasferimento_interno" runat="server" Visible="False"></asp:Label>
                   </td>
               </tr>
           </table> 
       </table>

           <br />

<%--Lista allegati--%>
            

<%--pannello allegati 20.09.2022 salvo--%>

   <%-- pannello allegati (visibile solo su dettaglio) <%=id_ddt.Text %>--%>

                      <%--  <ContentTemplate>--%>
                            <div id="divAllegInvioDoc" runat="server" style="height:500px;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="left" style="color: #FFFFFF" bgcolor="#444"><b>Documenti allegati </b> </td>

                                    </tr></table>
                                
                                <%--<asp:Panel ID="PanelAllegati" runat="server" ScrollBars="Auto">--%>

                                    <div id="allegati" runat="server" visible="true" style="max-height:120px">


                                         
                                <table style="margin-top:10px;">
                                    <tr>
                                        <td><asp:Label ID="lblUploaMaunDoc" runat="server" CssClass="testo_bold" ForeColor="#444"  Text="Allega documenti:"></asp:Label></td>
                                        <td>&nbsp; <asp:Label ID="lblTipoAlleg" runat="server" Text="Tipo Doc." CssClass="testo_bold"></asp:Label></td>
                                        <td >
                                            <asp:DropDownList ID="DropTipoAllegato" runat="server" AppendDataBoundItems="True" DataSourceID="sqlTipoAllegato" DataTextField="TipoAllegato" DataValueField="Id">
                                            <asp:ListItem Value="0" Selected="True">Seleziona...</asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                        <td ><asp:FileUpload ID="FileUploadAllegati" size="42" runat="server" /></td>
                                        <td>
                                            <asp:Button ID="btnMemorizzaAlleg" runat="server" Text="Memorizza allegato" ValidationGroup="Upload_Allegati"  />
                                            
                                        </td>
                                    </tr>
                                </table> 

                                    <hr style="clear:both; color:White;" />

                                        <table cellpadding="0" cellspacing="2" width="100%" style="font-size:11px;" border="0" runat="server" id="table2">
                                            <tr><td>
                                                <asp:ListView ID="ListViewAllegati" runat="server" DataSourceID="sqlAllegati" DataKeyNames="Id">
                                                    <ItemTemplate>
                                                        <tr style="background-color:#DCDCDC; color: #000000;">
                                                           <%-- <td>
                                                            </td>--%>
                                                            <td>
                                                                <asp:Label ID="lblIdAllegato" runat="server"   Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblTipo" runat="server" text='<%# Eval("TipoAllegato") %>'></asp:Label></td>
                                                            <td><asp:Label ID="lblNomeFile" runat="server"  Text='<%# Eval("NomeFile") %>'></asp:Label></td>
                                                            <td style="text-align:center;"> <asp:Label ID="lbl_dataora" runat="server" Text='<%# Eval("datacreazione") %>' ></asp:Label>
                                                            <asp:Label ID="lblPercorsoFile" runat="server"  Text='<%# Eval("PercorsoFile") %>' Visible="false"></asp:Label></td>
                                                           <td style="text-align:center;"><asp:Label ID="lbl_operatore" runat="server"  Text='<%# Eval("operatore") %>'></asp:Label></td>
                                                            <td align="center"><asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezionaAllegato"/></td>
                                                            <td align="center"><asp:ImageButton ID="EliminaAllegato" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="EliminaAllegato" OnClientClick="return confirm('Confermi eliminazione?');" />
                                                                <asp:CheckBox ID="chkAllegatoEmail" runat="server" Text="" Width="15px" Visible="false"/>
                                                            </td>
                                                           <%-- <td align="center"></td>--%>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr style=""><%--<td></td>--%>
                                                            <td><asp:Label ID="lblIdAllegato" runat="server"  Text='<%# Eval("Id") %>' Visible="false">
                                                                </asp:Label><asp:Label ID="lblTipo" runat="server"  Text='<%# Eval("TipoAllegato") %>'></asp:Label></td>
                                                            <td><asp:Label ID="lblNomeFile" runat="server" Text='<%# Eval("NomeFile") %>'></asp:Label></td>
                                                            <td style="text-align:center;"><asp:Label ID="lbl_dataora" runat="server"  Text='<%# Eval("datacreazione") %>' ></asp:Label>
                                                                <asp:Label ID="lblPercorsoFile" runat="server" Text='<%# Eval("PercorsoFile") %>' Visible="false"></asp:Label></td>
                                                             <td style="text-align:center;"><asp:Label ID="lbl_operatore" runat="server"  Text='<%# Eval("operatore") %>'></asp:Label></td>
                                                            <td align="center"><asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezionaAllegato"/></td>
                                                            <td align="center"><asp:ImageButton ID="EliminaAllegato" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="EliminaAllegato" OnClientClick="return confirm('Confermi eliminazione?');" />
                                                                <asp:CheckBox ID="chkAllegatoEmail" runat="server" Text="" Width="15px" Visible="false" />
                                                            </td>
                                                            <%--<td align="center"></td>--%>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    <EmptyDataTemplate>
                                                        <table id="Table1" runat="server" style="">
                                                            <tr>
                                                                <td class="testo_bold">Non vi sono allegati </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                    <LayoutTemplate>
                                                        <table id="Table2" runat="server" width="100%">
                                                            <tr id="Tr1" runat="server">
                                                                <td id="Td1" runat="server">
                                                                    <table ID="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:0px;font-family: Verdana, Arial, Helvetica, sans-serif;" width="100%">
                                                                        <tr id="Tr3" runat="server" style="color: #FFFFFF" class="sfondo_rosso">
                                                                             <%--<th id="Th1" runat="server" visible="false">Id</th>--%>
                                                        <th id="Th2"  style="width:200px;"  runat="server">Tipo</th>
                                                        <th id="Th3" runat="server">Nome File</th>
                                                        <th id="Th11" style="text-align:center;width:130px;" runat="server">Data e Ora</th>
                                                        <th id="Th4"  style="text-align:center;width:160px;" runat="server">Operatore</th>
                                                        <th id="Th5" runat="server"></th>
                                                        <th id="Th6" runat="server"></th>
                                                       <%-- <th id="Th7" runat="server"></th>--%>
                                                                        </tr>
                                                                        <tr ID="itemPlaceholder" runat="server"></tr>
                                                                    </table>
                                                                </td>
                                                           </tr>
                                                        </table>
                                                    </LayoutTemplate>
                                                    <SelectedItemTemplate>
                                                        <tr style="">
                                                          <%--  <td></td>--%>
                                                            <td><asp:Label ID="lblIdAllegato" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblTipo" runat="server"  Text='<%# Eval("TipoAllegato") %>'></asp:Label></td>
                                                            <td><asp:Label ID="lblNomeFile" runat="server" Text='<%# Eval("NomeFile") %>'></asp:Label></td>
                                                            <td style="text-align:center;"><asp:Label ID="lbl_dataora" runat="server"  Text='<%# Eval("datacreazione") %>' ></asp:Label>
                                                                <asp:Label ID="lblPercorsoFile" runat="server" Text='<%# Eval("PercorsoFile") %>' Visible="false"></asp:Label></td>
                                                            <td style="text-align:center;"><asp:Label ID="lbl_operatore" runat="server"  Text='<%# Eval("operatore") %>'></asp:Label></td>
                                                            <td align="center"><asp:ImageButton ID="SelezionaAllegato" runat="server" ImageUrl="/images/lente.png" style="width: 16px" CommandName="SelezionaAllegato"/></td>
                                                            <td align="center"><asp:ImageButton ID="EliminaAllegato" runat="server" ImageUrl="/images/elimina.png" style="width: 16px" CommandName="EliminaAllegato"/>
                                                                <asp:CheckBox ID="chkAllegatoEmail" runat="server" Text="" Width="15px"  Visible="false"/>
                                                            </td>
                                                            <%--<td align="center"></td>--%>
                                                        </tr>
                                                    </SelectedItemTemplate>
                                                </asp:ListView>
                                                </td></tr>
                                        </table>
                                    </div>
                               <%-- </asp:Panel>--%>

                        </div>

<%--            </ContentTemplate>--%>

    <asp:SqlDataSource ID="sqlAllegati" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="Select trasferimenti_Allegati.Id, trasferimenti_Allegati.DataCreazione, trasferimenti_TipoAllegato.TipoAllegato, trasferimenti_Allegati.NomeFile, 
                    trasferimenti_Allegati.PercorsoFile, (operatori.cognome + ' ' + operatori.nome) as operatore From trasferimenti_Allegati WITH (NOLOCK) INNER Join
           trasferimenti_TipoAllegato WITH (NOLOCK) ON trasferimenti_Allegati.IdTipoDocumento = trasferimenti_TipoAllegato.Id LEFT OUTER Join
           operatori On trasferimenti_Allegati.id_operatore = operatori.id Where trasferimenti_Allegati.Id_rif = 0">
</asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlTipoAllegato" runat="server"
    ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
    SelectCommand="SELECT [Id], [TipoAllegato] FROM [trasferimenti_TipoAllegato] WITH(NOLOCK) order by TipoAllegato">
</asp:SqlDataSource>


    <%--end pannello allegati--%>






            <%--end Lista allegati--%>









      </div>
       <div runat="server" id="div_cerca_interni">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" >
         <tr>
           <td align="left" style="color: #FFFFFF;background-color:#444;" >
             &nbsp;<asp:Label ID="Label72" runat="server" Text="Trasferimenti Interni - Trasferimenti" CssClass="testo_titolo"></asp:Label>
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
              <asp:Label ID="Label79" runat="server" Text="Gruppo" CssClass="testo_bold"></asp:Label>
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
                <asp:DropDownList ID="dropCercaStazionePickUpInterno" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="sqlStazioni" DataTextField="stazione" DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Tutte</asp:ListItem>
                </asp:DropDownList>
           </td>
           <td>
                <asp:DropDownList ID="dropCercaStatoInterno" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlTrasferimentiStatusAStazione" DataTextField="descrizione" DataValueField="id">
                    <asp:ListItem Selected="True" Value="0">Tutti</asp:ListItem>
                </asp:DropDownList>
             </td>
           <td>
              
                <asp:DropDownList ID="dropCercaGruppoAutoInterno" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="sqlGruppiAuto" 
                    DataTextField="cod_gruppo" DataValueField="id_gruppo">
                    <asp:ListItem Selected="True" Value="0">Tutti</asp:ListItem>
                </asp:DropDownList>
             </td>
           <td>
                 <a onclick="Calendar.show(document.getElementById('<%=txtCercaDataUscitaDaInterno.ClientID%>'), '%d/%m/%Y', false)"> 
                <asp:TextBox runat="server" Width="70px" ID="txtCercaDataUscitaDaInterno"></asp:TextBox>
                     </a>
            <%--    <ajaxtoolkit:CalendarExtender runat="server" Format="dd/MM/yyyy" Enabled="True" 
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
               <asp:Label ID="Label83" runat="server" CssClass="testo_bold" Text="Stazione Rientro"></asp:Label>
           </td>
           <td>
           
               <asp:Label ID="Label84" runat="server" CssClass="testo_bold" Text="Targa"></asp:Label>
             </td>
           <td>
              
               <asp:Label ID="Label984" runat="server" CssClass="testo_bold" Text="Autorizzati"></asp:Label>

               


             </td>
           <td>
           
               &nbsp;</td>
           <td>
              
               &nbsp;</td>
           <td>
           
               &nbsp;</td>
         </tr>
         <tr>
           <td>
                <asp:DropDownList ID="dropCercaStazioneRientroInterno" runat="server" AppendDataBoundItems="True" 
                    DataSourceID="sqlStazioni" DataTextField="stazione" DataValueField="id" 
                    Height="16px">
                    <asp:ListItem Selected="True" Value="0">Tutte</asp:ListItem>
                </asp:DropDownList>
             </td>
           <td>
           
               <asp:TextBox ID="txtCercaTargaInterno" runat="server" Width="90px"></asp:TextBox>
             </td>
           <td>
              
               <asp:DropDownList ID="dropAutorizzati" runat="server" AppendDataBoundItems="True">
                   <asp:ListItem Selected="True" Value="-1">Tutti</asp:ListItem>
                   <asp:ListItem  Value="2">Da Autorizzare</asp:ListItem>
                   <asp:ListItem  Value="1">Autorizzati</asp:ListItem>
                   <asp:ListItem  Value="0">Non Autorizzati</asp:ListItem>
               </asp:DropDownList>
             </td>
           <td>
           
               &nbsp;</td>
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
                <asp:Button ID="btnNuovoInterno" runat="server" Text="Nuovo Trasferimento" />
            
            </td>
          </tr>
        </table>
        <table cellpadding="0" cellspacing="2" width="100%" style="border:4px solid #444; border-top:0px;" border="0">
         <tr>
           <td align="left">
               <asp:ListView ID="listTrasferimentiInterni" runat="server" DataSourceID="sqlTrasferimentiInterni" DataKeyNames="id">
                  <ItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000;">
                          <td align="left">
                              <asp:Label ID="num_trasferimento" runat="server" CssClass="testo_piccolo" 
                                  Text='<%# Eval("num_trasferimento") %>' />
                          </td>
                          <td align="left">
                             <asp:Label ID="id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                             <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo_piccolo" />
                          </td>
                         <td align="left"> 
                              <asp:Label ID="id_stazione_pick_up" runat="server" Visible="false" Text='<%# Eval("id_stazione_uscita") %>' />
                              <asp:Label ID="stazione_pick_up" runat="server" CssClass="testo_piccolo" Text='<%# Eval("stazione_pick_up") %>' />
                          </td>
                         <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="riferimento" runat="server" Text='<%# Eval("riferimento") %>' Visible="false" />
                              <asp:Label ID="id_conducente" runat="server" Text='<%# Eval("id_conducente") %>' Visible="false" />
                              <asp:Label ID="km_uscita" runat="server" Text='<%# Eval("km_uscita") %>' Visible="false" />
                              <asp:Label ID="km_rientro" runat="server" Text='<%# Eval("km_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_uscita" runat="server" Text='<%# Eval("litri_uscita") %>' Visible="false" />
                              <asp:Label ID="litri_rientro" runat="server" Text='<%# Eval("litri_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_max" runat="server" Text='<%# Eval("litri_max") %>' Visible="false" />
                              <asp:Label ID="lblModello" runat="server"  Visible="false" Text='<%# Eval("modello") %>' />
                              <asp:Label ID="id_gruppo_da_trasferire" runat="server" Text='<%# Eval("id_gruppo_da_trasferire") %>' Visible="false" />
                              <asp:Label ID="gruppo_da_trasferire" runat="server" Text='<%# Eval("gruppo_da_trasferire") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_uscita" runat="server" Text='<%# Eval("data_uscita")  %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_presunto_rientro" runat="server" Text='<%# Eval("data_presunto_rientro") %>' Visible="false" />
                             <asp:Label ID="pres_rientro" runat="server" Text='<%# Eval("data_presunto_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="id_status" runat="server" Text='<%# Eval("id_status") %>' Visible="false" />
                             <asp:Label ID="status" runat="server" Text='<%# Eval("status") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left"> 
                             <asp:Label ID="trasf_diretto_autorizzato" runat="server" Text='<%# Eval("trasf_diretto_autorizzato") %>' Visible="false" />
                             <asp:Label ID="autorizzato" runat="server" CssClass="testo_piccolo" />
                          </td>
                          
                          <td align="left">
                              <asp:Label ID="richiesta_per_id_stazione" runat="server"  Text='<%# Eval("richiesta_per_id_stazione") %>' Visible="false" />
                              <asp:Label ID="richiesta_per_stazione" runat="server" CssClass="testo_piccolo" Text='<%# Eval("richiesta_per_stazione") %>' />
                         </td>
                         <td align="left">
                              <asp:Button ID="btnVedi" runat="server" CommandName="vedi" Text="Vedi" />
                         </td>
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr style="">
                         <td align="left">
                              <asp:Label ID="num_trasferimento" runat="server" CssClass="testo_piccolo" Text='<%# Eval("num_trasferimento") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                             <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo_piccolo" />
                          </td>
                         <td align="left"> 
                              <asp:Label ID="id_stazione_pick_up" runat="server" Visible="false" Text='<%# Eval("id_stazione_uscita") %>' />
                              <asp:Label ID="stazione_pick_up" runat="server" CssClass="testo_piccolo" Text='<%# Eval("stazione_pick_up") %>' />
                          </td>
                         <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="riferimento" runat="server" Text='<%# Eval("riferimento") %>' Visible="false" />
                              <asp:Label ID="id_conducente" runat="server" Text='<%# Eval("id_conducente") %>' Visible="false" />
                              <asp:Label ID="km_uscita" runat="server" Text='<%# Eval("km_uscita") %>' Visible="false" />
                              <asp:Label ID="km_rientro" runat="server" Text='<%# Eval("km_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_uscita" runat="server" Text='<%# Eval("litri_uscita") %>' Visible="false" />
                              <asp:Label ID="litri_rientro" runat="server" Text='<%# Eval("litri_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_max" runat="server" Text='<%# Eval("litri_max") %>' Visible="false" />
                              <asp:Label ID="lblModello" runat="server"  Visible="false" Text='<%# Eval("modello") %>' />
                              <asp:Label ID="id_gruppo_da_trasferire" runat="server" Text='<%# Eval("id_gruppo_da_trasferire") %>' Visible="false" />
                              <asp:Label ID="gruppo_da_trasferire" runat="server" Text='<%# Eval("gruppo_da_trasferire") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                            <asp:Label ID="data_uscita" runat="server" Text='<%# Eval("data_uscita")  %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_presunto_rientro" runat="server" Text='<%# Eval("data_presunto_rientro") %>' Visible="false" />
                             <asp:Label ID="pres_rientro" runat="server" Text='<%# Eval("data_presunto_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="id_status" runat="server" Text='<%# Eval("id_status") %>' Visible="false" />
                             <asp:Label ID="status" runat="server" Text='<%# Eval("status") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left"> 
                             <asp:Label ID="trasf_diretto_autorizzato" runat="server" Text='<%# Eval("trasf_diretto_autorizzato") %>' Visible="false" />
                             <asp:Label ID="autorizzato" runat="server" CssClass="testo_piccolo" />
                          </td>
                          
                          
                          <td align="left">
                             <asp:Label ID="richiesta_per_id_stazione" runat="server" Text='<%# Eval("richiesta_per_id_stazione") %>' Visible="false" />
                             <asp:Label ID="richiesta_per_stazione" runat="server" Text='<%# Eval("richiesta_per_stazione") %>' CssClass="testo_piccolo" />
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
                                  <table ID="itemPlaceholderContainer" runat="server" border="1"  width="100%"   style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;font-size:x-small;">
                                      <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                          <th align="left">
                                          
                                              <asp:LinkButton ID="LinkButton17" runat="server" CommandName="order_by_numero" 
                                                  CssClass="testo_titolo_piccolo">Num.</asp:LinkButton>
                                          
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton11" runat="server" CommandName="order_by_targa" CssClass="testo_titolo_piccolo">Targa</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                              <asp:LinkButton ID="LinkButton8" runat="server" CommandName="order_by_stazione_pick_up" CssClass="testo_titolo_piccolo">Staz.Pick Up</asp:LinkButton>
                                          </th>
                                          <th id="Th5" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton6" runat="server" CommandName="order_by_gruppo" CssClass="testo_titolo_piccolo">Gr.</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton9" runat="server" CommandName="order_by_data_uscita" CssClass="testo_titolo_piccolo">Uscita</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton14" runat="server" CommandName="order_by_presunto_arrivo" CssClass="testo_titolo_piccolo">Presunto Arrivo</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton15" runat="server" CommandName="order_by_data_rientro" CssClass="testo_titolo_piccolo">Rientro</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton1" runat="server" CommandName="order_by_status" CssClass="testo_titolo_piccolo">Status</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             
                                          </th>
                                          
                                          <th align="left">
                                              <asp:LinkButton ID="LinkButton10" runat="server" CommandName="order_by_richiesta_per_stazione" CssClass="testo_titolo_piccolo">Staz. Arrivo</asp:LinkButton>
                                          </th>
                                          <th align="left">
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
              <%-- <asp:ListView ID="listTrasferimentiInterni" runat="server" DataSourceID="sqlTrasferimentiInterni" DataKeyNames="id">
                  <ItemTemplate>
                      <tr style="background-color:#DCDCDC;color: #000000;">
                          <td align="left">
                              <asp:Label ID="num_trasferimento" runat="server" CssClass="testo_piccolo" 
                                  Text='<%# Eval("num_trasferimento") %>' />
                          </td>
                          <td align="left">
                             <asp:Label ID="id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                             <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo_piccolo" />
                          </td>
                         <td align="left"> 
                              <asp:Label ID="id_stazione_pick_up" runat="server" Visible="false" Text='<%# Eval("id_stazione_uscita") %>' />
                              <asp:Label ID="stazione_pick_up" runat="server" CssClass="testo_piccolo" Text='<%# Eval("stazione_pick_up") %>' />
                          </td>
                         <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="riferimento" runat="server" Text='<%# Eval("riferimento") %>' Visible="false" />
                              <asp:Label ID="id_conducente" runat="server" Text='<%# Eval("id_conducente") %>' Visible="false" />
                              <asp:Label ID="km_uscita" runat="server" Text='<%# Eval("km_uscita") %>' Visible="false" />
                              <asp:Label ID="km_rientro" runat="server" Text='<%# Eval("km_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_uscita" runat="server" Text='<%# Eval("litri_uscita") %>' Visible="false" />
                              <asp:Label ID="litri_rientro" runat="server" Text='<%# Eval("litri_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_max" runat="server" Text='<%# Eval("litri_max") %>' Visible="false" />
                              <asp:Label ID="lblModello" runat="server"  Visible="false" Text='<%# Eval("modello") %>' />
                              <asp:Label ID="id_gruppo_da_trasferire" runat="server" Text='<%# Eval("id_gruppo_da_trasferire") %>' Visible="false" />
                              <asp:Label ID="gruppo_da_trasferire" runat="server" Text='<%# Eval("gruppo_da_trasferire") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_uscita" runat="server" Text='<%# Eval("data_uscita")  %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_presunto_rientro" runat="server" Text='<%# Eval("data_presunto_rientro") %>' Visible="false" />
                             <asp:Label ID="pres_rientro" runat="server" Text='<%# Eval("data_presunto_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="id_status" runat="server" Text='<%# Eval("id_status") %>' Visible="false" />
                             <asp:Label ID="status" runat="server" Text='<%# Eval("status") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left"> 
                             <asp:Label ID="trasf_diretto_autorizzato" runat="server" Text='<%# Eval("trasf_diretto_autorizzato") %>' Visible="false" />
                             <asp:Label ID="autorizzato" runat="server" CssClass="testo_piccolo" />
                          </td>
                          
                          <td align="left">
                              <asp:Label ID="richiesta_per_id_stazione" runat="server"  Text='<%# Eval("richiesta_per_id_stazione") %>' Visible="false" />
                              <asp:Label ID="richiesta_per_stazione" runat="server" CssClass="testo_piccolo" Text='<%# Eval("richiesta_per_stazione") %>' />
                         </td>
                         <td align="left">
                              <asp:Button ID="btnVedi" runat="server" CommandName="vedi" Text="Vedi" />
                         </td>
                      </tr>
                  </ItemTemplate>
                  <AlternatingItemTemplate>
                      <tr style="">
                         <td align="left">
                              <asp:Label ID="num_trasferimento" runat="server" CssClass="testo_piccolo" Text='<%# Eval("num_trasferimento") %>' />
                         </td>
                         <td align="left">
                             <asp:Label ID="id_veicolo" runat="server" Text='<%# Eval("id_veicolo") %>' Visible="false" />
                             <asp:Label ID="targa" runat="server" Text='<%# Eval("targa") %>' CssClass="testo_piccolo" />
                          </td>
                         <td align="left"> 
                              <asp:Label ID="id_stazione_pick_up" runat="server" Visible="false" Text='<%# Eval("id_stazione_uscita") %>' />
                              <asp:Label ID="stazione_pick_up" runat="server" CssClass="testo_piccolo" Text='<%# Eval("stazione_pick_up") %>' />
                          </td>
                         <td align="left">
                              <asp:Label ID="idLabel" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                              <asp:Label ID="riferimento" runat="server" Text='<%# Eval("riferimento") %>' Visible="false" />
                              <asp:Label ID="id_conducente" runat="server" Text='<%# Eval("id_conducente") %>' Visible="false" />
                              <asp:Label ID="km_uscita" runat="server" Text='<%# Eval("km_uscita") %>' Visible="false" />
                              <asp:Label ID="km_rientro" runat="server" Text='<%# Eval("km_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_uscita" runat="server" Text='<%# Eval("litri_uscita") %>' Visible="false" />
                              <asp:Label ID="litri_rientro" runat="server" Text='<%# Eval("litri_rientro") %>' Visible="false" />
                              <asp:Label ID="litri_max" runat="server" Text='<%# Eval("litri_max") %>' Visible="false" />
                              <asp:Label ID="lblModello" runat="server"  Visible="false" Text='<%# Eval("modello") %>' />
                              <asp:Label ID="id_gruppo_da_trasferire" runat="server" Text='<%# Eval("id_gruppo_da_trasferire") %>' Visible="false" />
                              <asp:Label ID="gruppo_da_trasferire" runat="server" Text='<%# Eval("gruppo_da_trasferire") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                            <asp:Label ID="data_uscita" runat="server" Text='<%# Eval("data_uscita")  %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="data_presunto_rientro" runat="server" Text='<%# Eval("data_presunto_rientro") %>' Visible="false" />
                             <asp:Label ID="pres_rientro" runat="server" Text='<%# Eval("data_presunto_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                              <asp:Label ID="data_rientro" runat="server" Text='<%# Eval("data_rientro") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left">
                             <asp:Label ID="id_status" runat="server" Text='<%# Eval("id_status") %>' Visible="false" />
                             <asp:Label ID="status" runat="server" Text='<%# Eval("status") %>' CssClass="testo_piccolo" />
                          </td>
                          <td align="left"> 
                             <asp:Label ID="trasf_diretto_autorizzato" runat="server" Text='<%# Eval("trasf_diretto_autorizzato") %>' Visible="false" />
                             <asp:Label ID="autorizzato" runat="server" CssClass="testo_piccolo" />
                          </td>
                          
                          
                          <td align="left">
                             <asp:Label ID="richiesta_per_id_stazione" runat="server" Text='<%# Eval("richiesta_per_id_stazione") %>' Visible="false" />
                             <asp:Label ID="richiesta_per_stazione" runat="server" Text='<%# Eval("richiesta_per_stazione") %>' CssClass="testo_piccolo" />
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
                                  <table ID="itemPlaceholderContainer" runat="server" border="1"  width="100%"   style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;font-size:x-small;">
                                      <tr id="Tr2" runat="server" style="color: #FFFFFF" bgcolor="#19191b">
                                          <th align="left">
                                          
                                              <asp:LinkButton ID="LinkButton17" runat="server" CommandName="order_by_numero" 
                                                  CssClass="testo_titolo_piccolo">Num.</asp:LinkButton>
                                          
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton11" runat="server" CommandName="order_by_targa" CssClass="testo_titolo_piccolo">Targa</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                              <asp:LinkButton ID="LinkButton8" runat="server" CommandName="order_by_stazione_pick_up" CssClass="testo_titolo_piccolo">Staz.Pick Up</asp:LinkButton>
                                          </th>
                                          <th id="Th5" runat="server" align="left">
                                             <asp:LinkButton ID="LinkButton6" runat="server" CommandName="order_by_gruppo" CssClass="testo_titolo_piccolo">Gr.</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton9" runat="server" CommandName="order_by_data_uscita" CssClass="testo_titolo_piccolo">Uscita</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton14" runat="server" CommandName="order_by_presunto_arrivo" CssClass="testo_titolo_piccolo">Presunto Arrivo</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton15" runat="server" CommandName="order_by_data_rientro" CssClass="testo_titolo_piccolo">Rientro</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             <asp:LinkButton ID="LinkButton1" runat="server" CommandName="order_by_status" CssClass="testo_titolo_piccolo">Status</asp:LinkButton>
                                          </th>
                                          <th align="left">
                                             
                                          </th>
                                          
                                          <th align="left">
                                              <asp:LinkButton ID="LinkButton10" runat="server" CommandName="order_by_richiesta_per_stazione" CssClass="testo_titolo_piccolo">Staz. Arrivo</asp:LinkButton>
                                          </th>
                                          <th align="left">
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
               </asp:ListView>--%>
           </td>
         </tr>
       </table>
       </div>
     </ContentTemplate>
   </ajaxtoolkit:TabPanel>
</ajaxtoolkit:TabContainer>
    
    
    <asp:SqlDataSource ID="sqlRichiesteStazione" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT trasferimenti.id, trasferimenti.richiesta_per_data, trasferimenti.id_gruppo_richiesto, trasferimenti.status As id_status, trasferimenti_status.descrizione As status, gruppi1.cod_gruppo As gruppo_richiesto, gruppi2.cod_gruppo As gruppo_da_trasferire, trasferimenti.id_gruppo_da_trasferire, riferimento, (ISNULL(operatori1.cognome,'') + ' ' + ISNULL(operatori1.nome,'')) As operatore_richiesta, data_richiesta, (ISNULL(operatori2.cognome,'') + ' ' + ISNULL(operatori2.nome,'')) As operatore_admin, data_gestione_admin, (stazioni1.codice + ' ' + stazioni1.nome_stazione) As stazione_pick_up, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As richiesta_per_stazione, trasferimenti.richiesta_per_id_stazione, trasferimenti.note_admin FROM trasferimenti WITH(NOLOCK) INNER JOIN trasferimenti_status WITH(NOLOCK) ON trasferimenti.status=trasferimenti_status.id INNER JOIN operatori As operatori1 WITH(NOLOCK) ON trasferimenti.id_operatore_richiesta=operatori1.id LEFT JOIN operatori As operatori2 WITH(NOLOCK) ON trasferimenti.id_operatore_admin=operatori2.id INNER JOIN gruppi As gruppi1 WITH(NOLOCK) ON trasferimenti.id_gruppo_richiesto=gruppi1.id_gruppo LEFT JOIN gruppi As gruppi2 WITH(NOLOCK) ON trasferimenti.id_gruppo_da_trasferire=gruppi2.id_gruppo LEFT JOIN stazioni As stazioni1 WITH(NOLOCK) ON trasferimenti.id_stazione_uscita=stazioni1.id INNER JOIN stazioni As stazioni2 WITH(NOLOCK) ON trasferimenti.richiesta_per_id_stazione=stazioni2.id WHERE trasferimenti.id<>'0' ORDER BY data_richiesta DESC">      
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlTrasferimentiDaEffettuare" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT trasferimenti.id, trasferimenti.num_trasferimento, trasferimenti.id_conducente, trasferimenti.data_presunto_rientro, trasferimenti.data_rientro,  trasferimenti.km_uscita, trasferimenti.litri_uscita, trasferimenti.km_rientro, trasferimenti.litri_rientro, trasferimenti.litri_max trasferimenti.richiesta_per_data, trasferimenti.status As id_status, trasferimenti_status.descrizione As status, gruppi2.cod_gruppo As gruppo_da_trasferire, trasferimenti.id_gruppo_da_trasferire, (stazioni1.codice + ' ' + stazioni1.nome_stazione) As stazione_pick_up, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As richiesta_per_stazione, trasferimenti.richiesta_per_id_stazione, trasferimenti.note_admin, trasferimenti.riferimento, trasferimenti.id_veicolo, trasferimenti.targa, modelli.descrizione As modello FROM trasferimenti WITH(NOLOCK) INNER JOIN trasferimenti_status WITH(NOLOCK) ON trasferimenti.status=trasferimenti_status.id INNER JOIN operatori As operatori1 WITH(NOLOCK) ON trasferimenti.id_operatore_richiesta=operatori1.id LEFT JOIN operatori As operatori2 WITH(NOLOCK) ON trasferimenti.id_operatore_admin=operatori2.id LEFT JOIN gruppi As gruppi2 WITH(NOLOCK) ON trasferimenti.id_gruppo_da_trasferire=gruppi2.id_gruppo LEFT JOIN stazioni As stazioni1 WITH(NOLOCK) ON trasferimenti.id_stazione_uscita=stazioni1.id INNER JOIN stazioni As stazioni2 WITH(NOLOCK) ON trasferimenti.richiesta_per_id_stazione=stazioni2.id LEFT JOIN veicoli WITH(NOLOCK) ON trasferimenti.id_veicolo=veicoli.id LEFT JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello WHERE trasferimenti.id<>'0' ORDER BY data_richiesta DESC">      
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlTrasferimentiVersoStazione" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT trasferimenti.id, trasferimenti.num_trasferimento, trasferimenti.data_uscita, trasferimenti.data_presunto_rientro, trasferimenti.data_rientro, trasferimenti.status As id_status, trasferimenti_status.descrizione As status, gruppi2.cod_gruppo As gruppo_da_trasferire, trasferimenti.id_gruppo_da_trasferire, (stazioni1.codice + ' ' + stazioni1.nome_stazione) As stazione_pick_up, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As richiesta_per_stazione, trasferimenti.richiesta_per_id_stazione, trasferimenti.riferimento, trasferimenti.id_veicolo, trasferimenti.targa, modelli.descrizione As modello, trasferimenti.id_conducente, trasferimenti.km_uscita, trasferimenti.km_rientro, trasferimenti.litri_uscita, trasferimenti.litri_rientro, trasferimenti.litri_max FROM trasferimenti WITH(NOLOCK) INNER JOIN trasferimenti_status WITH(NOLOCK) ON trasferimenti.status=trasferimenti_status.id LEFT JOIN operatori As operatori1 WITH(NOLOCK) ON trasferimenti.id_operatore_richiesta=operatori1.id LEFT JOIN operatori As operatori2 WITH(NOLOCK) ON trasferimenti.id_operatore_admin=operatori2.id LEFT JOIN gruppi As gruppi2 WITH(NOLOCK) ON trasferimenti.id_gruppo_da_trasferire=gruppi2.id_gruppo LEFT JOIN stazioni As stazioni1 WITH(NOLOCK) ON trasferimenti.id_stazione_uscita=stazioni1.id INNER JOIN stazioni As stazioni2 WITH(NOLOCK) ON trasferimenti.richiesta_per_id_stazione=stazioni2.id LEFT JOIN veicoli WITH(NOLOCK) ON trasferimenti.id_veicolo=veicoli.id LEFT JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello WHERE trasferimenti.id_tipologia='2' ORDER BY data_richiesta DESC">      
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlTrasferimentiInterni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT trasferimenti.id, trasferimenti.num_trasferimento, trasferimenti.data_uscita, trasferimenti.data_presunto_rientro, trasferimenti.data_rientro, trasferimenti.status As id_status, trasferimenti_status.descrizione As status, gruppi2.cod_gruppo As gruppo_da_trasferire, trasferimenti.id_gruppo_da_trasferire, (stazioni1.codice + ' ' + stazioni1.nome_stazione) As stazione_pick_up, (stazioni2.codice + ' ' + stazioni2.nome_stazione) As richiesta_per_stazione, trasferimenti.richiesta_per_id_stazione, trasferimenti.riferimento, trasferimenti.id_veicolo, trasferimenti.targa, modelli.descrizione As modello, trasferimenti.id_conducente, trasferimenti.km_uscita, trasferimenti.km_rientro, trasferimenti.litri_uscita, trasferimenti.litri_rientro, trasferimenti.litri_max FROM trasferimenti WITH(NOLOCK) INNER JOIN trasferimenti_status WITH(NOLOCK) ON trasferimenti.status=trasferimenti_status.id LEFT JOIN operatori As operatori1 WITH(NOLOCK) ON trasferimenti.id_operatore_richiesta=operatori1.id LEFT JOIN operatori As operatori2 WITH(NOLOCK) ON trasferimenti.id_operatore_admin=operatori2.id LEFT JOIN gruppi As gruppi2 WITH(NOLOCK) ON trasferimenti.id_gruppo_da_trasferire=gruppi2.id_gruppo LEFT JOIN stazioni As stazioni1 WITH(NOLOCK) ON trasferimenti.id_stazione_uscita=stazioni1.id INNER JOIN stazioni As stazioni2 WITH(NOLOCK) ON trasferimenti.richiesta_per_id_stazione=stazioni2.id LEFT JOIN veicoli WITH(NOLOCK) ON trasferimenti.id_veicolo=veicoli.id LEFT JOIN modelli WITH(NOLOCK) ON veicoli.id_modello=modelli.id_modello WHERE trasferimenti.tipologia='2' ORDER BY data_richiesta DESC">      
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlGruppiAuto" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT gruppi.id_gruppo, gruppi.cod_gruppo FROM gruppi WITH(NOLOCK) WHERE attivo='1' ORDER BY cod_gruppo">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlStazioni" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT stazioni.id, (codice + ' ' + nome_stazione) As stazione FROM stazioni WITH(NOLOCK) WHERE attiva='1' ORDER BY codice">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDrivers" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT drivers.id, (drivers.cognome + ' ' + drivers.nome) As driver FROM drivers WITH(NOLOCK)ORDER BY driver">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlTrasferimentiStatus" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM trasferimenti_status WITH(NOLOCK) ORDER BY id">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlTrasferimentiStatusDaStazione" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM trasferimenti_status WITH(NOLOCK) ORDER BY id">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlTrasferimentiStatusAStazione" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT id, descrizione FROM trasferimenti_status WITH(NOLOCK) ORDER BY id">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlTrasferimentiAppoggio" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SicilyConnectionString %>" 
        SelectCommand="SELECT DISTINCT stazioni.id As id_stazione, (stazioni.codice + ' ' + stazioni.nome_stazione) As stazione, trasferimenti_appoggio.distanza FROM trasferimenti_appoggio WITH(NOLOCK) INNER JOIN stazioni WITH(NOLOCK) ON trasferimenti_appoggio.id_stazione=stazioni.id WHERE id_trasferimenti=@id_trasferimenti AND id_operatore=@id_operatore ORDER BY distanza, stazione">
      <SelectParameters> 
        <asp:ControlParameter ControlID="id_modifica" Name="id_trasferimenti" PropertyName="Text" Type="Int32" ConvertEmptyStringToNull="true" />
        <asp:ControlParameter ControlID="id_operatore" Name="id_operatore" PropertyName="Text" Type="Int32" ConvertEmptyStringToNull="true" />
      </SelectParameters>
    </asp:SqlDataSource>
    
    <div id="div_edit_danno" runat="server" visible="false">
       <uc1:gestione_checkin id="gestione_checkin" runat="server" />
    </div>   
    
    
</asp:Content>

